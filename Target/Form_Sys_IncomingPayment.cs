using System;
using System.Collections.Generic;
using System.Linq;
using SAPbouiCOM.Framework;


using Nancy.Json;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using Target.Model;

namespace Target
{
   public class Form_Sys_IncomingPayment
    {
        private AccessFileViewModel accessFVM;
        public Form_Sys_IncomingPayment()
        {
            accessFVM = Global.objFun.GetAccessFile("FERVENT_MOBILEAPPS");
            Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        }
        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (BusinessObjectInfo.FormTypeEx == "170" && BusinessObjectInfo.BeforeAction == false && BusinessObjectInfo.ActionSuccess == true &&
                    (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD ||
                     BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE))
                {
                    string docEntry = "";

                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(BusinessObjectInfo.ObjectKey);

                    System.Xml.XmlNode node = xmlDoc.SelectSingleNode("//DocEntry");

                    if (node != null)
                    {
                        docEntry = node.InnerText.Trim();
                    }

                    if (string.IsNullOrEmpty(docEntry))
                    {
                        Application.SBO_Application.SetStatusBarMessage("DocEntry not found.", SAPbouiCOM.BoMessageTime.bmt_Short, true);
                        return;
                    }


                    SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string sql =
                        "SELECT \"DocNum\", \"CardCode\", \"U_NOTIFYSND\" AS \"isChecked\" " +
                        "FROM \"ORCT\" " +
                        "WHERE \"DocEntry\" = " + docEntry;

                    rs.DoQuery(sql);


                    if (rs.RecordCount > 0)
                    {
                        string dealerCode = rs.Fields.Item("CardCode").Value.ToString().Trim();

                        string docNum = rs.Fields.Item("DocNum").Value.ToString().Trim();

                        string isChecked = rs.Fields.Item("isChecked").Value.ToString().Trim();

                        if (isChecked == "N" ||
                            string.IsNullOrEmpty(isChecked))
                        {
                            IncomingPaymentNotification incomingPaymentNotification = new IncomingPaymentNotification();
                            IncomingPaymentReturnData returnData = new IncomingPaymentReturnData();
                            string message = "";


                            if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD)
                            {
                                message = "Incoming Payment No : " + docNum + " has been created successfully for Dealer : " + dealerCode + " ";
                            }

                            else if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE)
                            {
                                message = "Incoming Payment No : " + docNum + " has been updated successfully for Dealer : " + dealerCode + " ";
                            }

                            incomingPaymentNotification.user_id = dealerCode;
                            incomingPaymentNotification.message = message;

                            bool success = SendIncomingPaymentNotification(incomingPaymentNotification, returnData);

                            if (success)
                            {
                                string updateSql =
                                    "UPDATE \"ORCT\" " +
                                    "SET \"U_NOTIFYSND\" = 'Y' " +
                                    "WHERE \"DocEntry\" = " + docEntry;

                                rs.DoQuery(updateSql);

                                Application.SBO_Application.SetStatusBarMessage(
                                    message,
                                    SAPbouiCOM.BoMessageTime.bmt_Short,
                                    false);
                            }
                            else
                            {
                                Application.SBO_Application.SetStatusBarMessage(
                                    "Notification failed: " + returnData.message,
                                    SAPbouiCOM.BoMessageTime.bmt_Short,
                                    true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(
                    "Incoming Payment Notification Error: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                    true);
            }
        }


        public bool SendIncomingPaymentNotification(IncomingPaymentNotification incomingPaymentNotification, IncomingPaymentReturnData returnData)
        {
            string complete_url = "", msg = "";
            bool isSuccess = false;
            Stream dataStream;
            StreamReader reader;
            HttpWebRequest HTTP_Request;
            HttpWebResponse HTTP_Response;
            //List<ReturnData> returnDatas = new List<ReturnData();
            try
            {
                var myContent = JsonConvert.SerializeObject(incomingPaymentNotification);
                complete_url = accessFVM.IPPort + "api/v1/notification/send-notification";
                //api / method / fusion_hr.controllers.api_controllers.post.item_controller.update_or_create_item
                HTTP_Request = (HttpWebRequest)HttpWebRequest.Create(complete_url);
                HTTP_Request.Method = "POST";
                HTTP_Request.ContentType = "application/json";
                HTTP_Request.Headers.Add("access-key", accessFVM.Authorization);

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

                returnData = (new JavaScriptSerializer()).Deserialize<IncomingPaymentReturnData>(msg);
                if (returnData != null)
                {
                    //var parsed = JsonConvert.DeserializeObject<Dictionary<string, ReturnData>>(msg);
                    // var jsonData = parsed["message"];

                    if (returnData.status == 200)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                }
                else
                {
                    isSuccess = false;
                    returnData.message = msg;
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

                returnData.message = message;
                isSuccess = false;
                return isSuccess;
            }
            return isSuccess;
        }
    }
}
