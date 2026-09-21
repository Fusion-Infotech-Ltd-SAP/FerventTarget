using Nancy.Json;
using Newtonsoft.Json;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Target.Model;

namespace Target
{
    class SAPCostCenter
    {
        private AccessFileViewModel accessFVM;
        public SAPCostCenter()
        {
            accessFVM = Global.objFun.GetAccessFile("FERVENT_ERPNext");
            Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        }
        //public void GetAccessFile()
        //{
        //    accessFVM = (new AccessFileViewModel
        //    {
        //        //IPPort = "http://103.238.155.192/"
        //        //                ,
        //        //Authorization = "token e225ce004aaf20b:4e3c871b2e4f71a"token 3b5f85f6d1f6f43:cc4fd3ade8b6f78
        //        // Authorization = "token 3b5f85f6d1f6f43:fb0916603dd6b7c"
        //        IPPort = "http://103.238.155.65:3038/"
        //        ,
        //        Authorization = "token 3b5f85f6d1f6f43:cc4fd3ade8b6f78"
        //    });
        //}
        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;

            if (BusinessObjectInfo.BeforeAction == true && BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD
              || BusinessObjectInfo.BeforeAction == true && BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE)
            {
                switch (BusinessObjectInfo.FormTypeEx)
                {
                    case "810":
                        {
                            bool Success = false;
                            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(BusinessObjectInfo.FormUID);

                            //Cost Center
                            SAPbouiCOM.EditText cost_center_numberSC = (SAPbouiCOM.EditText)oform.Items.Item("5").Specific;
                            // Name
                            SAPbouiCOM.EditText cost_center_nameSC = (SAPbouiCOM.EditText)oform.Items.Item("6").Specific;
                            //Dimention
                            SAPbouiCOM.ComboBox custom_typeSC = (SAPbouiCOM.ComboBox)oform.Items.Item("2003").Specific;

                            //IsActive
                            int disabled = ((SAPbouiCOM.CheckBox)oform.Items.Item("540002011").Specific).Checked ? 0 : 1;

                            CostCenterMaster CostCenterMaster = new CostCenterMaster();
                            CostCenterMaster.cost_center_number = cost_center_numberSC.Value.ToString();
                            CostCenterMaster.cost_center_name = cost_center_nameSC.Value.ToString();
                            CostCenterMaster.disabled = disabled;


                            string custom_typeTrim = custom_typeSC.Value.TrimStart(' ');
                            CostCenterMaster.custom_type = custom_typeTrim;

                            ///Created/Updated By and Name
                            SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            string CurrentUser = Application.SBO_Application.Company.UserName;
                            CostCenterMaster.user_id = CurrentUser.ToString();
                            string sqlUname = string.Format("SELECT {0}U_NAME{0} FROM {0}OUSR{0} WHERE {0}USER_CODE{0} = '" + CostCenterMaster.user_id + "'", '"');
                            businessObject.DoQuery(sqlUname);
                            CostCenterMaster.user_name = businessObject.Fields.Item("U_NAME").Value.ToString();
                            ///

                            ReturnData data = new ReturnData();
                            int customTypeInt;
                            int.TryParse(CostCenterMaster.custom_type, out customTypeInt);
                            if (customTypeInt == 2 || customTypeInt == 3)
                            {
                                Success = SaveAndUpdateCostCenter(CostCenterMaster, data);


                                if (Success == true && data.cost_center_name != "")
                                {
                                    Application.SBO_Application.SetStatusBarMessage("Api Msg:" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Medium, false);
                                }
                                else
                                {
                                    Application.SBO_Application.SetStatusBarMessage("Error!" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                                    Application.SBO_Application.MessageBox("Cost Center Creation OR Updation failed — not synced with ERP-Next.");

                                    //Not working
                                    BubbleEvent = false; //Bubble Event-> To stop Adding item on Item Master on SAP B1
                                }
                            }

                            break;
                        }
                }
            }
        }

        public bool SaveAndUpdateCostCenter(CostCenterMaster CostCenterMaster, ReturnData returnData)
        {
            string complete_url = "", msg = "";
            bool isSuccess = false;
            Stream dataStream;
            StreamReader reader;
            HttpWebRequest HTTP_Request;
            HttpWebResponse HTTP_Response;
            List<ReturnData> returnDatas = new List<ReturnData>();
            try
            {
                var myContent = JsonConvert.SerializeObject(CostCenterMaster);
                complete_url = accessFVM.IPPort + "api/method/fusion_hr.controllers.api_controllers.post.cost_center_controller.create_or_update_cost_center";
                HTTP_Request = (HttpWebRequest)HttpWebRequest.Create(complete_url);
                HTTP_Request.Method = "POST";
                HTTP_Request.ContentType = "application/json";
                HTTP_Request.Headers.Add("Authorization", accessFVM.Authorization);

                using (var streamWriter = new StreamWriter(HTTP_Request.GetRequestStream()))
                {
                    streamWriter.Write(myContent);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                HTTP_Response = (HttpWebResponse)HTTP_Request.GetResponse();

                dataStream = HTTP_Response.GetResponseStream();
                reader = new StreamReader(dataStream);
                msg = reader.ReadToEnd();

                returnDatas = (new JavaScriptSerializer()).Deserialize<List<ReturnData>>(msg);
                if (returnDatas != null)
                {
                    var parsed = JsonConvert.DeserializeObject<Dictionary<string, ReturnData>>(msg);
                    var jsonData = parsed["message"];
                    returnData.cost_center_name = jsonData.cost_center_name;
                    if (returnData.cost_center_name != "" && jsonData.status == "created"
                        || returnData.cost_center_name != "" && jsonData.status == "updated"
                        || returnData.cost_center_name != "" && jsonData.status == "unchanged")
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                    returnData.status = jsonData.status;
                    returnData.ReturnMsg = jsonData.message;
                }
                else
                {
                    isSuccess = false;
                    returnData.ReturnMsg = msg;
                }
            }
            catch (WebException ex)
            {

                string message = "";
                if (ex.Response != null)
                {
                    try
                    {
                        var httpResponse = (HttpWebResponse)ex.Response;
                        using (var reader2 = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            string errorJson = reader2.ReadToEnd();

                            // Compose a full message including status code and response
                            message = $"HTTP Error {(int)httpResponse.StatusCode} - {httpResponse.StatusDescription}\n" +
                                      $"Response:\n{errorJson}";
                        }
                    }
                    catch (Exception readEx)
                    {
                        message = "Failed to read error response: " + readEx.Message;
                    }

                }

                returnData.ReturnMsg = message;
                isSuccess = false;
                return isSuccess;
            }
            return isSuccess;
        }
    }
}
