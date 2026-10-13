using System;
using System.Collections.Generic;
using System.Linq;
using SAPbouiCOM.Framework;

using System.Text;
using Nancy.Json;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using Target.Model;

namespace Target
{

    class Form_Sys_Delivery
    {
        private AccessFileViewModel accessFVM;
        private AccessFileViewModel erpNextAccessFVM;

        public Form_Sys_Delivery()
        {
            accessFVM = Global.objFun.GetAccessFile("FERVENT_MOBILEAPPS");
            erpNextAccessFVM = Global.objFun.GetAccessFile("FERVENT_ERPNext");
            Application.SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
            Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        }

        public void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (pVal.FormTypeEx == "140" && pVal.EventType != SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD)
                {
                    SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.GetFormByTypeAndCount(pVal.FormType, pVal.FormTypeCount);
                    int currentPane = oForm.PaneLevel;

                    if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD && pVal.BeforeAction == true)
                    {

                        SAPbouiCOM.Item ScheduleBtn = oForm.Items.Add("STSCDBTN", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
                        SAPbouiCOM.Item CopyFromBtn = oForm.Items.Item("10000330");

                        // Positioning the new button based on the existing one
                        ScheduleBtn.Top = CopyFromBtn.Top;
                        ScheduleBtn.Height = CopyFromBtn.Height;
                        ScheduleBtn.Width = CopyFromBtn.Width + 10;
                        ScheduleBtn.Left = CopyFromBtn.Left - 150;

                        SAPbouiCOM.Button ScheduleButtonSpecific = (SAPbouiCOM.Button)ScheduleBtn.Specific;
                        ScheduleButtonSpecific.Caption = "Copy from Schedules";

                        SAPbouiCOM.ChooseFromListCollection oCfls = oForm.ChooseFromLists;
                        SAPbouiCOM.ChooseFromListCreationParams oCFLCreationParams = (SAPbouiCOM.ChooseFromListCreationParams)Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);

                        oCFLCreationParams.MultiSelection = true;
                        oCFLCreationParams.ObjectType = "FIL_D_DELRSCHD";
                        oCFLCreationParams.UniqueID = "CFL_FIL_DH_DELRSCHD";
                        oCfls.Add(oCFLCreationParams);
                        ScheduleButtonSpecific.ChooseFromListUID = "CFL_FIL_DH_DELRSCHD";
                    }


                    else if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST && pVal.ItemUID == "STSCDBTN")
                    {

                        if (pVal.BeforeAction == true)
                        {
                            SAPbouiCOM.ChooseFromList oCfl = oForm.ChooseFromLists.Item(((SAPbouiCOM.Button)oForm.Items.Item(pVal.ItemUID).Specific).ChooseFromListUID);

                            SAPbouiCOM.Conditions oCons = new SAPbouiCOM.Conditions();
                            SAPbouiCOM.Condition oCon;

                            oCon = oCons.Add();
                            oCon.Alias = "Status";
                            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            oCon.CondVal = "O";

                            oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                            oCon = oCons.Add();
                            oCon.Alias = "U_APRVSTTS";
                            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_IS_NULL;
                            oCon.CondVal = "";

                            oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                            oCon = oCons.Add();
                            oCon.Alias = "U_CARDCODE";
                            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            oCon.CondVal = oForm.DataSources.DBDataSources.Item("ODLN").GetValue("CardCode", 0);


                            // OR


                            oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;

                            oCon = oCons.Add();
                            oCon.Alias = "U_CARDCODE";
                            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            oCon.CondVal = oForm.DataSources.DBDataSources.Item("ODLN").GetValue("CardCode", 0);

                            oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                            oCon = oCons.Add();
                            oCon.Alias = "Status";
                            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            oCon.CondVal = "O";

                            oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                            oCon = oCons.Add();
                            oCon.Alias = "U_APRVSTTS";
                            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            oCon.CondVal = "A";

                            oCfl.SetConditions(oCons);
                        }
                        else
                        {
                            try
                            {
                                SAPbouiCOM.DataTable oDataTable = ((SAPbouiCOM.IChooseFromListEvent)pVal).SelectedObjects;

                                if (oDataTable != null)
                                {
                                    string strDocEntry = "";
                                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                                    {
                                        if (strDocEntry != "")
                                            strDocEntry = strDocEntry + "," + oDataTable.GetValue("DocEntry", i).ToString();
                                        else
                                            strDocEntry = oDataTable.GetValue("DocEntry", i).ToString();
                                    }

                                    Form_No_SOList SoList = new Form_No_SOList();
                                    SoList.Show();
                                    SAPbouiCOM.Form cForm = Application.SBO_Application.Forms.Item("FIL_FRM_NO_SOLIST");

                                    Form_No_SOList.LoadScheduleList(ref cForm, strDocEntry);
                                }
                            }
                            catch (Exception ex) { }
                        }

                    }

                }
            }

            catch (Exception ex)
            {

                Application.SBO_Application.SetStatusBarMessage("Error - " + ex.ToString(), SAPbouiCOM.BoMessageTime.bmt_Medium, true);
            }

        }

        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (BusinessObjectInfo.FormTypeEx == "140" && BusinessObjectInfo.BeforeAction == false && BusinessObjectInfo.ActionSuccess == true &&
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
                        "SELECT \"DocNum\", \"CardCode\", \"DocDate\", \"TrackNo\", \"U_CELLPHNO\" AS \"DriverContactNumber\", \"U_NOTIFYSND\" AS \"isChecked\" " +
                        "FROM \"ODLN\" " +
                        "WHERE \"DocEntry\" = " + docEntry;

                    rs.DoQuery(sql);


                    if (rs.RecordCount > 0)
                    {
                        string dealerCode = rs.Fields.Item("CardCode").Value.ToString().Trim();
                        string docNum = rs.Fields.Item("DocNum").Value.ToString().Trim();
                        DateTime docDate = Convert.ToDateTime(rs.Fields.Item("DocDate").Value);
                        string vehicleNo = rs.Fields.Item("TrackNo").Value.ToString().Trim();
                        string driverContact = rs.Fields.Item("DriverContactNumber").Value.ToString().Trim();
                        string isChecked = rs.Fields.Item("isChecked").Value.ToString().Trim();


                        if (isChecked == "N" || string.IsNullOrEmpty(isChecked))
                        {
                            // ================= MOBILE APP API ================= //

                            DeliveryNotification deliveryNotification = new DeliveryNotification();

                            DeliveryReturnData returnData = new DeliveryReturnData();


                            // ================= ERPNEXT API ================= //

                            DeliveryNotification.DeliveryChallanNotification challanNotification =
                                new DeliveryNotification.DeliveryChallanNotification();

                            DeliveryERPNextReturnData erpReturnData =
                                new DeliveryERPNextReturnData();

                            challanNotification.type = "Delevery Chalan";
                            challanNotification.greetings = "Greetings from FERVENT";
                            challanNotification.refchalan = docNum;
                            challanNotification.dealer_code = dealerCode;
                            challanNotification.date = docDate.ToString("yyyy-MM-dd");
                            challanNotification.vehicle_no = vehicleNo;
                            challanNotification.driver_contact = driverContact;

                            challanNotification.items =
                                new List<DeliveryNotification.DeliveryChallanNotification.Item>();


                            // ================= ITEM QUERY ================= //

                            SAPbobsCOM.Recordset rsItem =
                                (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(
                                    SAPbobsCOM.BoObjectTypes.BoRecordset);

                            string itemSql =
                                "SELECT \"Dscription\", \"Quantity\" " +
                                "FROM \"DLN1\" " +
                                "WHERE \"DocEntry\" = " + docEntry;

                            rsItem.DoQuery(itemSql);


                            StringBuilder productMessage =
                                new StringBuilder();


                            while (!rsItem.EoF)
                            {
                                string itemDescription =
                                    rsItem.Fields.Item("Dscription").Value.ToString().Trim();

                                decimal qty =
                                    Convert.ToDecimal(
                                        rsItem.Fields.Item("Quantity").Value);


                                // ================= MOBILE PRODUCT ================= //

                                if (productMessage.Length > 0)
                                {
                                    productMessage.Append("; ");
                                }

                                productMessage.Append(
                                    itemDescription +
                                    " - " +
                                    qty.ToString("0.##") +
                                    " PCS");


                                // ================= ERPNEXT ITEM ================= //

                                challanNotification.items.Add(
                                    new DeliveryNotification.DeliveryChallanNotification.Item
                                    {
                                        item_description = itemDescription,
                                        qty = qty
                                    });


                                rsItem.MoveNext();
                            }


                            // ================= MOBILE MESSAGE ================= //

                            string message =
                                "Greetings from FERVENT" + Environment.NewLine +
                                "Chalan No: " + docNum + Environment.NewLine +
                                "Product: " + productMessage.ToString() + ";" + Environment.NewLine +
                                "Vehicle No: " + vehicleNo + Environment.NewLine +
                                "Driver Contact: " + driverContact;


                            deliveryNotification.user_id = dealerCode;
                            deliveryNotification.message = message;


                            // ================= MOBILE POST ================= //

                            bool mobileSuccess =
                                SendDeliveryNotification(
                                    deliveryNotification,
                                    returnData);


                            // ================= ERPNEXT POST ================= //

                            bool erpSuccess =
                                SendDeliveryERPNextNotification(
                                    challanNotification,
                                    erpReturnData);


                            // ================= BOTH SUCCESS ================= //

                            if (mobileSuccess && erpSuccess)
                            {
                                string updateSql =
                                    "UPDATE \"ODLN\" " +
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
                                    failedMessage + " Notification status remains N.",
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
                    "Delivery Notification Error: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                    true);
            }
        }

        //private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        //{
        //    BubbleEvent = true;

        //    try
        //    {
        //        if (BusinessObjectInfo.FormTypeEx == "140" && BusinessObjectInfo.BeforeAction == false && BusinessObjectInfo.ActionSuccess == true &&
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
        //                    "SELECT \"DocNum\", \"CardCode\", \"DocDate\", \"TrackNo\", \"U_CELLPHNO\" AS \"DriverContactNumber\", \"U_NOTIFYSND\" AS \"isChecked\" " +
        //                    "FROM \"ODLN\" " +
        //                    "WHERE \"DocEntry\" = " + docEntry;

        //            rs.DoQuery(sql);

        //            if (rs.RecordCount > 0)
        //            {
        //                string dealerCode = rs.Fields.Item("CardCode").Value.ToString().Trim();
        //                string docNum = rs.Fields.Item("DocNum").Value.ToString().Trim();
        //                DateTime docDate = Convert.ToDateTime(rs.Fields.Item("DocDate").Value);
        //                string vehicleNo = rs.Fields.Item("TrackNo").Value.ToString().Trim();
        //                string driverContact = rs.Fields.Item("DriverContactNumber").Value.ToString().Trim();
        //                string isChecked = rs.Fields.Item("isChecked").Value.ToString().Trim();

        //                if (isChecked == "N" || string.IsNullOrEmpty(isChecked))
        //                {
        //                    // ================= MOBILE APP API ================= //
        //                    DeliveryNotification deliveryNotification = new DeliveryNotification();
        //                    DeliveryReturnData returnData = new DeliveryReturnData();
        //                    string message = "";


        //                    if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD)
        //                    {
        //                        message = "Delivery No : " + docNum + " has been created successfully for Dealer : " + dealerCode + " ";
        //                    }

        //                    else if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE)
        //                    {
        //                        message = "Delivery No : " + docNum + " has been updated successfully for Dealer : " + dealerCode + " ";
        //                    }

        //                    deliveryNotification.user_id = dealerCode;
        //                    deliveryNotification.message = message;

        //                    bool mobileSuccess = SendDeliveryNotification(deliveryNotification, returnData);

        //                    // ================= ERPNEXT API ================= //

        //                    DeliveryNotification.DeliveryChallanNotification challanNotification = new DeliveryNotification.DeliveryChallanNotification();
        //                    DeliveryERPNextReturnData erpReturnData = new DeliveryERPNextReturnData();

        //                    challanNotification.type = "Delevery Chalan";
        //                    challanNotification.greetings = "Greetings from FERVENT";
        //                    challanNotification.refchalan = docNum;
        //                    challanNotification.dealer_code = dealerCode;
        //                    challanNotification.date = docDate.ToString("yyyy-MM-dd");
        //                    challanNotification.vehicle_no = vehicleNo;
        //                    challanNotification.driver_contact = driverContact;
        //                    challanNotification.items = new List<DeliveryNotification.DeliveryChallanNotification.Item>();

        //                    SAPbobsCOM.Recordset rsItem = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

        //                    string itemSql =
        //                        "SELECT \"Dscription\", \"Quantity\" " +
        //                        "FROM \"DLN1\" " +
        //                        "WHERE \"DocEntry\" = " + docEntry;

        //                    rsItem.DoQuery(itemSql);

        //                    while (!rsItem.EoF)
        //                    {
        //                        challanNotification.items.Add(new DeliveryNotification.DeliveryChallanNotification.Item
        //                        {
        //                            item_description = rsItem.Fields.Item("Dscription").Value.ToString().Trim(),
        //                            qty = Convert.ToDecimal(rsItem.Fields.Item("Quantity").Value)
        //                        });

        //                        rsItem.MoveNext();
        //                    }


        //                    bool erpSuccess = SendDeliveryERPNextNotification(challanNotification, erpReturnData);


        //                    if (mobileSuccess && erpSuccess)
        //                    {
        //                        string updateSql =
        //                            "UPDATE \"ODLN\" " +
        //                            "SET \"U_NOTIFYSND\" = 'Y' " +
        //                            "WHERE \"DocEntry\" = " + docEntry;

        //                        rs.DoQuery(updateSql);

        //                        Application.SBO_Application.SetStatusBarMessage(message,SAPbouiCOM.BoMessageTime.bmt_Short,false);
        //                    }
        //                    else
        //                    {
        //                        string failedMessage = "";

        //                        if (!mobileSuccess && !erpSuccess)
        //                            failedMessage = "Mobile and ERPNext notifications failed.";
        //                        else if (!mobileSuccess)
        //                            failedMessage = "Mobile notification failed.";
        //                        else
        //                            failedMessage = "ERPNext notification failed.";

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
        //            "Delivery Notification Error: " + ex.Message,
        //            SAPbouiCOM.BoMessageTime.bmt_Medium,
        //            true);
        //    }
        //}


        public bool SendDeliveryNotification(DeliveryNotification deliveryNotification, DeliveryReturnData returnData)
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
                var myContent = JsonConvert.SerializeObject(deliveryNotification);
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

                returnData = (new JavaScriptSerializer()).Deserialize<DeliveryReturnData>(msg);
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

        public bool SendDeliveryERPNextNotification(DeliveryNotification.DeliveryChallanNotification challanNotification, DeliveryERPNextReturnData returnData)
        {
            bool isSuccess = false;

            try
            {
                string completeUrl =
                    erpNextAccessFVM.IPPort.TrimEnd('/') +
                    "/api/method/fusion_hr.controllers.api_controllers.post.sap_notification_log.create_sap_notification_log";

                string myContent = JsonConvert.SerializeObject(challanNotification);

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

                using (HttpWebResponse HTTP_Response = (HttpWebResponse)HTTP_Request.GetResponse())
                using (StreamReader reader = new StreamReader(HTTP_Response.GetResponseStream()))
                {
                    string msg = reader.ReadToEnd();

                    returnData = JsonConvert.DeserializeObject<DeliveryERPNextReturnData>(msg);

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
                        message = "Failed to read error response: " + readEx.Message;
                    }
                }
                else
                {
                    message = ex.Message;
                }

                Application.SBO_Application.SetStatusBarMessage(
                    "ERPNext API Error: " + message,
                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                    true);

                isSuccess = false;
            }
            catch (Exception ex)
            {
               
                Application.SBO_Application.SetStatusBarMessage(
                    "ERPNext API Error: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                    true);

                isSuccess = false;
            }

            return isSuccess;
        }

    }
}
