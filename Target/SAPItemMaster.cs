
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
    class SAPItemMaster
    {
        private AccessFileViewModel accessFVM;
        public SAPItemMaster()
        {
            accessFVM = Global.objFun.GetAccessFile("FERVENT_ERPNext");
            Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        }
        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;

            if (BusinessObjectInfo.BeforeAction == true && BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD
              || BusinessObjectInfo.BeforeAction == true && BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE)
            {
                switch (BusinessObjectInfo.FormTypeEx)
                {
                    case "150":
                        {
                            bool Success = false;
                            SAPbouiCOM.Form oform3 = Application.SBO_Application.Forms.Item(BusinessObjectInfo.FormUID);

                            SAPbouiCOM.EditText itmCode = (SAPbouiCOM.EditText)oform3.Items.Item("5").Specific;
                            SAPbouiCOM.EditText itmName = (SAPbouiCOM.EditText)oform3.Items.Item("7").Specific;
                            SAPbouiCOM.ComboBox itmGrp = (SAPbouiCOM.ComboBox)oform3.Items.Item("39").Specific;
                            SAPbouiCOM.EditText invUOM = (SAPbouiCOM.EditText)oform3.Items.Item("251").Specific;

                            ItemMaster itemMaster = new ItemMaster();

                            ///Created/Updated By and Name
                            SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            string CurrentUser = Application.SBO_Application.Company.UserName;
                            itemMaster.user_id = CurrentUser.ToString();
                            string sqlUname = string.Format("SELECT {0}U_NAME{0} FROM {0}OUSR{0} WHERE {0}USER_CODE{0} = '" + itemMaster.user_id + "'", '"');
                            businessObject.DoQuery(sqlUname);
                            itemMaster.user_name = businessObject.Fields.Item("U_NAME").Value.ToString();
                            ///


                            itemMaster.item_code = itmCode.Value.ToString();
                            itemMaster.item_name = itmName.Value.ToString();
                            // itemMaster.item_group = "Admin - General Store";
                            itemMaster.item_group = itmGrp.Selected.Description.Trim().ToString();
                            itemMaster.custom_inventory_uom = invUOM.Value.ToString();

                            string sqlQuery = string.Format("SELECT {0}OnHand{0} FROM {0}OITM{0} WHERE {0}ItemCode{0} = '" + itemMaster.item_code + "'", '"');
                            businessObject.DoQuery(sqlQuery);


                            if (businessObject.RecordCount > 0)
                            {
                                // Get the first Row value
                                if (!businessObject.EoF)
                                {
                                    string onHandValue = businessObject.Fields.Item("OnHand").Value.ToString();
                                    itemMaster.custom_on_hand = onHandValue;
                                }
                            }
                            else
                            {
                                itemMaster.custom_on_hand = "0.00";
                            }

                            ReturnData data = new ReturnData();
                            Success = SaveAndUpdateItemMaster(itemMaster, data);

                            if (Success == true && data.item_code != "")
                            {
                                Application.SBO_Application.SetStatusBarMessage("Api Msg:" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Medium, false);
                            }
                            else
                            {
                                Application.SBO_Application.SetStatusBarMessage("Error!" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                                Application.SBO_Application.MessageBox("Item Creation OR Updation failed — not synced with ERP-Next.");

                                BubbleEvent = false; //Bubble Event-> To stop Adding item on Item Master on SAP B1
                            }
                            break;
                        }
                }
            }
 
        }

        public bool SaveAndUpdateItemMaster(ItemMaster itemMaster, ReturnData returnData)
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
                var myContent = JsonConvert.SerializeObject(itemMaster);
                complete_url = accessFVM.IPPort + "api/method/fusion_hr.controllers.api_controllers.post.item_controller.update_or_create_item";
                //api / method / fusion_hr.controllers.api_controllers.post.item_controller.update_or_create_item
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
                    returnData.item_code = jsonData.item_code;
                    if (returnData.item_code != "" && jsonData.status == "created"
                        || returnData.item_code != "" && jsonData.status == "updated"
                        || returnData.item_code != "" && jsonData.status == "unchanged")
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

        //public bool UpdateItemMaster(ItemMaster itemMaster, ReturnData returnData)
        //{
        //    string complete_url = "", msg = "";
        //    bool isSuccess = false;
        //    Stream dataStream;
        //    StreamReader reader;
        //    HttpWebRequest HTTP_Request;
        //    HttpWebResponse HTTP_Response;
        //    List<ReturnData> returnDatas = new List<ReturnData>();
        //    try
        //    {
        //        var myContent = JsonConvert.SerializeObject(itemMaster);
        //        complete_url = accessFVM.IPPort + "api/resource/Item/" + itemMaster.item_code;
        //        HTTP_Request = (HttpWebRequest)HttpWebRequest.Create(complete_url);
        //        HTTP_Request.Method = "PUT";
        //        HTTP_Request.ContentType = "application/json";
        //        HTTP_Request.Headers.Add("Authorization", accessFVM.Authorization);

        //        using (var streamWriter = new StreamWriter(HTTP_Request.GetRequestStream()))
        //        {
        //            streamWriter.Write(myContent);
        //            streamWriter.Flush();
        //            streamWriter.Close();
        //        }
        //        HTTP_Response = (HttpWebResponse)HTTP_Request.GetResponse();

        //        dataStream = HTTP_Response.GetResponseStream();
        //        reader = new StreamReader(dataStream);
        //        msg = reader.ReadToEnd();

        //        returnDatas = (new JavaScriptSerializer()).Deserialize<List<ReturnData>>(msg);
        //        if (returnDatas != null)
        //        {

        //            var parsed = JsonConvert.DeserializeObject<Dictionary<string, ReturnData>>(msg);
        //            var jsonData = parsed["data"];
        //            returnData.item_code = jsonData.item_code;
        //            if (returnData.item_code != "")
        //            {
        //                isSuccess = true;
        //            }
        //            else
        //            {
        //                isSuccess = false;
        //            }
        //            returnData.ReturnMsg = msg;
        //        }
        //        else
        //        {
        //            isSuccess = false;
        //            returnData.ReturnMsg = msg;
        //        }
        //    }
        //    catch (WebException ex)
        //    {

        //        string message = "";
        //        if (ex.Response != null)
        //        {
        //            try
        //            {
        //                var httpResponse = (HttpWebResponse)ex.Response;
        //                using (var reader2 = new StreamReader(httpResponse.GetResponseStream()))
        //                {
        //                    string errorJson = reader2.ReadToEnd();

        //                    // Compose a full message including status code and response
        //                    message = $"HTTP Error {(int)httpResponse.StatusCode} - {httpResponse.StatusDescription}\n" +
        //                              $"Response:\n{errorJson}";
        //                }
        //            }
        //            catch (Exception readEx)
        //            {
        //                message = "Failed to read error response: " + readEx.Message;
        //            }

        //        }

        //        returnData.ReturnMsg = message;
        //        isSuccess = false;
        //        return isSuccess;
        //    }
        //    return isSuccess;
        //}
    }
}
