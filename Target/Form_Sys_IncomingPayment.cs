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
        private AccessFileViewModel erpNextAccessFVM;
        public Form_Sys_IncomingPayment()
        {
            accessFVM = Global.objFun.GetAccessFile("FERVENT_MOBILEAPPS");
            erpNextAccessFVM = Global.objFun.GetAccessFile("FERVENT_ERPNext");
            Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        }

        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (BusinessObjectInfo.FormTypeEx == "170" &&
                    BusinessObjectInfo.BeforeAction == false &&
                    BusinessObjectInfo.ActionSuccess == true &&
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
                        Application.SBO_Application.SetStatusBarMessage(
                            "DocEntry not found.",
                            SAPbouiCOM.BoMessageTime.bmt_Short,
                            true);

                        return;
                    }


                    SAPbobsCOM.Recordset rs =
                        (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(
                            SAPbobsCOM.BoObjectTypes.BoRecordset);

                    string sql =
                        "SELECT \"DocNum\", \"CardCode\", \"DocDate\", \"DocTotal\", \"U_NOTIFYSND\" AS \"isChecked\" " +
                        "FROM \"ORCT\" " +
                        "WHERE \"DocEntry\" = " + docEntry;

                    rs.DoQuery(sql);


                    if (rs.RecordCount > 0)
                    {
                        string dealerCode = rs.Fields.Item("CardCode").Value.ToString().Trim();
                        string docNum = rs.Fields.Item("DocNum").Value.ToString().Trim();
                        DateTime docDate = Convert.ToDateTime(rs.Fields.Item("DocDate").Value);
                        decimal amount = Convert.ToDecimal(rs.Fields.Item("DocTotal").Value);
                        string isChecked = rs.Fields.Item("isChecked").Value.ToString().Trim();


                        if (isChecked == "N" || string.IsNullOrEmpty(isChecked))
                        {
                            // ================= MOBILE APP API ================= //

                            IncomingPaymentNotification incomingPaymentNotification =
                                new IncomingPaymentNotification();

                            IncomingPaymentReturnData returnData =
                                new IncomingPaymentReturnData();


                            string formattedAmount =
                                amount.ToString(
                                    "#,##0.00",
                                    System.Globalization.CultureInfo.InvariantCulture);

                            string formattedDate =
                                docDate.ToString("dd-MM-yy");


                            string message =
                                "Greetings from FERVENT\r\n" +
                                "Payment Received Amount: Tk " +
                                formattedAmount +
                                " Ref:" +
                                docNum +
                                " Date:" +
                                formattedDate;


                            incomingPaymentNotification.user_id = dealerCode;
                            incomingPaymentNotification.message = message;


                            bool mobileSuccess =
                                SendIncomingPaymentNotification(
                                    incomingPaymentNotification,
                                    returnData);


                            // ================= ERPNEXT API ================= //

                            IncomingPaymentNotification.IncomingPaymentERPNextNotification erpNotification =
                                new IncomingPaymentNotification.IncomingPaymentERPNextNotification();

                            IncomingPaymentERPNextReturnData erpReturnData =
                                new IncomingPaymentERPNextReturnData();


                            erpNotification.type = "Incoming Payments";
                            erpNotification.greetings = "Greetings from FERVENT Payment Received Amount";
                            erpNotification.refchalan = docNum;
                            erpNotification.dealer_code = dealerCode;
                            erpNotification.date = docDate.ToString("yyyy-MM-dd");
                            erpNotification.amount = amount;


                            // ================= ERPNEXT POST ================= //

                            bool erpSuccess =
                                SendIncomingPaymentERPNextNotification(
                                    erpNotification,
                                    erpReturnData);


                            // ================= BOTH SUCCESS ================= //

                            if (mobileSuccess && erpSuccess)
                            {
                                string updateSql =
                                    "UPDATE \"ORCT\" " +
                                    "SET \"U_NOTIFYSND\" = 'Y' " +
                                    "WHERE \"DocEntry\" = " + docEntry;

                                rs.DoQuery(updateSql);


                                string erpName = "";

                                if (erpReturnData.message != null)
                                {
                                    erpName = erpReturnData.message.name;
                                }


                                Application.SBO_Application.SetStatusBarMessage(
                                    message,
                                    SAPbouiCOM.BoMessageTime.bmt_Short,
                                    false);
                            }
                            else
                            {
                                string failedMessage = "";

                                if (!mobileSuccess && !erpSuccess)
                                {
                                    failedMessage =
                                        "Mobile and ERPNext notifications failed.";
                                }
                                else if (!mobileSuccess)
                                {
                                    failedMessage =
                                        "Mobile notification failed.";
                                }
                                else
                                {
                                    failedMessage =
                                        "ERPNext notification failed.";
                                }


                                Application.SBO_Application.SetStatusBarMessage(
                                    failedMessage +
                                    " Notification status remains N.",
                                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                                    true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(
                    "Incoming Payment Notification Error: " +
                    ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                    true);
            }
        }
        //private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        //{
        //    BubbleEvent = true;

        //    try
        //    {
        //        if (BusinessObjectInfo.FormTypeEx == "170" && BusinessObjectInfo.BeforeAction == false && BusinessObjectInfo.ActionSuccess == true &&
        //            (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD ||
        //             BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE))
        //        {
        //            string docEntry = "";

        //            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
        //            xmlDoc.LoadXml(BusinessObjectInfo.ObjectKey);

        //            System.Xml.XmlNode node = xmlDoc.SelectSingleNode("//DocEntry");

        //            if (node != null)
        //            {
        //                docEntry = node.InnerText.Trim();
        //            }

        //            if (string.IsNullOrEmpty(docEntry))
        //            {
        //                Application.SBO_Application.SetStatusBarMessage("DocEntry not found.", SAPbouiCOM.BoMessageTime.bmt_Short, true);
        //                return;
        //            }


        //            SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
        //            string sql =
        //                    "SELECT \"DocNum\", \"CardCode\", \"DocDate\", \"DocTotal\", \"U_NOTIFYSND\" AS \"isChecked\" " +
        //                    "FROM \"ORCT\" " +
        //                    "WHERE \"DocEntry\" = " + docEntry;

        //            rs.DoQuery(sql);


        //            if (rs.RecordCount > 0)
        //            {
        //                string dealerCode = rs.Fields.Item("CardCode").Value.ToString().Trim();
        //                string docNum = rs.Fields.Item("DocNum").Value.ToString().Trim();
        //                DateTime docDate = Convert.ToDateTime(rs.Fields.Item("DocDate").Value);
        //                decimal amount = Convert.ToDecimal(rs.Fields.Item("DocTotal").Value);
        //                string isChecked = rs.Fields.Item("isChecked").Value.ToString().Trim();

        //                if (isChecked == "N" ||string.IsNullOrEmpty(isChecked))
        //                {
        //                    // ================= MOBILE APP API =================//

        //                    IncomingPaymentNotification incomingPaymentNotification = new IncomingPaymentNotification();
        //                    IncomingPaymentReturnData returnData = new IncomingPaymentReturnData();
        //                    string message = "";


        //                    if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD)
        //                    {
        //                        message = "Incoming Payment No : " + docNum + " has been created successfully for Dealer : " + dealerCode + " ";
        //                    }

        //                    else if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE)
        //                    {
        //                        message = "Incoming Payment No : " + docNum + " has been updated successfully for Dealer : " + dealerCode + " ";
        //                    }

        //                    incomingPaymentNotification.user_id = dealerCode;
        //                    incomingPaymentNotification.message = message;

        //                    bool mobileSuccess = SendIncomingPaymentNotification(incomingPaymentNotification, returnData);


        //                    // ================= ERPNEXT API =================

        //                    IncomingPaymentNotification.IncomingPaymentERPNextNotification erpNotification = new IncomingPaymentNotification.IncomingPaymentERPNextNotification();
        //                    IncomingPaymentERPNextReturnData erpReturnData = new IncomingPaymentERPNextReturnData();

        //                    erpNotification.type = "Incoming Payments";
        //                    erpNotification.greetings = "Greetings from FERVENT Payment Received Amount";
        //                    erpNotification.refchalan = docNum;
        //                    erpNotification.dealer_code = dealerCode;
        //                    erpNotification.date = docDate.ToString("yyyy-MM-dd");
        //                    erpNotification.amount = amount;

        //                    // ================= ERPNEXT POST =================

        //                    bool erpSuccess = SendIncomingPaymentERPNextNotification(erpNotification,erpReturnData);

        //                    if (mobileSuccess && erpSuccess)
        //                    {
        //                        string updateSql =
        //                            "UPDATE \"ORCT\" " +
        //                            "SET \"U_NOTIFYSND\" = 'Y' " +
        //                            "WHERE \"DocEntry\" = " + docEntry;

        //                        rs.DoQuery(updateSql);

        //                        string erpName = "";

        //                        if (erpReturnData.message != null)
        //                        {
        //                            erpName = erpReturnData.message.name;
        //                        }

        //                        Application.SBO_Application.SetStatusBarMessage(
        //                            "Incoming Payment notifications sent successfully. ERPNext Name: " + erpName,
        //                            SAPbouiCOM.BoMessageTime.bmt_Short,
        //                            false);
        //                    }
        //                    else
        //                    {
        //                        string failedMessage = "";

        //                        if (!mobileSuccess && !erpSuccess)
        //                        {
        //                            failedMessage = "Mobile and ERPNext notifications failed.";
        //                        }
        //                        else if (!mobileSuccess)
        //                        {
        //                            failedMessage = "Mobile notification failed.";
        //                        }
        //                        else
        //                        {
        //                            failedMessage = "ERPNext notification failed.";
        //                        }

        //                        Application.SBO_Application.SetStatusBarMessage(
        //                            failedMessage + " Notification status remains N.",
        //                            SAPbouiCOM.BoMessageTime.bmt_Medium,
        //                            true);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Application.SBO_Application.SetStatusBarMessage(
        //            "Incoming Payment Notification Error: " + ex.Message,
        //            SAPbouiCOM.BoMessageTime.bmt_Medium,
        //            true);
        //    }
        //}
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

        public bool SendIncomingPaymentERPNextNotification(IncomingPaymentNotification.IncomingPaymentERPNextNotification erpNotification,IncomingPaymentERPNextReturnData returnData)
        {
            bool isSuccess = false;

            try
            {
                string completeUrl =
                    erpNextAccessFVM.IPPort.TrimEnd('/') +
                    "/api/method/fusion_hr.controllers.api_controllers.post.sap_notification_log.create_sap_notification_log";

                string myContent = JsonConvert.SerializeObject(erpNotification);
                HttpWebRequest HTTP_Request = (HttpWebRequest)HttpWebRequest.Create(completeUrl);

                HTTP_Request.Method = "POST";
                HTTP_Request.ContentType = "application/json";
                HTTP_Request.Accept = "application/json";
                HTTP_Request.Headers.Add("Authorization", erpNextAccessFVM.Authorization);

                using (StreamWriter streamWriter = new StreamWriter(HTTP_Request.GetRequestStream()))
                {
                    streamWriter.Write(myContent);
                    streamWriter.Flush();
                }

                using (HttpWebResponse HTTP_Response =(HttpWebResponse)HTTP_Request.GetResponse())
                using (StreamReader reader = new StreamReader(HTTP_Response.GetResponseStream()))
                {
                    string msg = reader.ReadToEnd();

                    returnData = JsonConvert.DeserializeObject<IncomingPaymentERPNextReturnData>(msg);

                    if (returnData != null &&
                        returnData.message != null &&
                        returnData.message.status == "created")
                    {
                        string status = returnData.message.status;
                        string name = returnData.message.name;
                        isSuccess = true;
                    }
                }
            }
            catch (WebException ex)
            {
                string message = "";

                if (ex.Response != null)
                {
                    try
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                        using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            string errorJson = reader.ReadToEnd();
                            message =
                                "HTTP Error " +
                                (int)httpResponse.StatusCode +
                                " - " +
                                httpResponse.StatusDescription +
                                " | Response: " +
                                errorJson;
                        }
                    }
                    catch (Exception readEx)
                    {
                        message = "Failed to read error response: " +readEx.Message;
                    }
                }
                else
                {
                    message = ex.Message;
                }

                Application.SBO_Application.SetStatusBarMessage(
                    "ERPNext API Error for Incoming Payment No : " +
                    erpNotification.refchalan +
                    " - " +
                    message,
                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                    true);

                isSuccess = false;
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(
                    "ERPNext API Error for Incoming Payment No : " +
                    erpNotification.refchalan +
                    " - " +
                    ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                    true);

                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
