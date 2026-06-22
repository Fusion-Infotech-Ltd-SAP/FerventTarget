using Newtonsoft.Json;
using SAPbouiCOM.Framework;
using System;
using System.IO;
using System.Net;
using Target.Model;

namespace Target
{
    class SAPCustomerGroup
    {
        private AccessFileViewModel accessFVM;
        public SAPCustomerGroup()
        {
            accessFVM = Global.objFun.GetAccessFile();
            Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        }
        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;

            if (BusinessObjectInfo.BeforeAction == false && BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD
              || BusinessObjectInfo.BeforeAction == false && BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE)
            {
                switch (BusinessObjectInfo.FormTypeEx)
                {
                    case "174":
                        {
                            bool Success = false;
                            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(BusinessObjectInfo.FormUID);

                            SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            string sqlCusGrp = string.Format("select {0}GroupCode{0}, {0}GroupName{0} from {0}OCRG{0} WHERE {0}GroupType{0} = 'C'", '"');
                            businessObject.DoQuery(sqlCusGrp);


                            while (!businessObject.EoF)
                            {
                                CustomerGroupMaster CustomerGroupMaster = new CustomerGroupMaster();
                                CustomerGroupMaster.custom_customer_group_code = businessObject.Fields.Item("GroupCode").Value.ToString();
                                CustomerGroupMaster.customer_group_name = businessObject.Fields.Item("GroupName").Value.ToString();
                                CustomerGroupMaster.is_group = "0";

                                ReturnData data = new ReturnData();
                                Success = SaveAndUpdateBPGroupMaster(CustomerGroupMaster, data);

                                if (Success == true && data.item_code != "")
                                {
                                    Application.SBO_Application.SetStatusBarMessage("Api Msg:" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Medium, false);
                                }
                                else
                                {
                                    Application.SBO_Application.SetStatusBarMessage("Error!-" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                                }
                                businessObject.MoveNext();

                            }
                        }
                        break;
                }
            }
        }

        public bool SaveAndUpdateBPGroupMaster(CustomerGroupMaster CustomerGroupMaster, ReturnData returnData)
        {
            string complete_url = "", msg = "";
            bool isSuccess = false;
            Stream dataStream;
            StreamReader reader;
            HttpWebRequest HTTP_Request;
            HttpWebResponse HTTP_Response;
            try
            {
                var myContent = JsonConvert.SerializeObject(CustomerGroupMaster);
                complete_url = accessFVM.IPPort + "api/method/fusion_hr.controllers.api_controllers.post.customer_group_controller.create_or_update_customer_group";
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

                dynamic obj = JsonConvert.DeserializeObject(msg);
                string RetStatus = obj.message.status;
                string ReturnMsg = obj.message.message;

                if (RetStatus != null)
                {
                    if (RetStatus == "created" || RetStatus == "updated" || RetStatus == "unchanged")
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                    returnData.status = RetStatus;
                    returnData.ReturnMsg = ReturnMsg;
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
                    isSuccess = false;
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