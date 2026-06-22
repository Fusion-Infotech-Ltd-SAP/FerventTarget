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
    class SAPItemGroup
    {
        private AccessFileViewModel accessFVM;
        public SAPItemGroup()
        {
            accessFVM = Global.objFun.GetAccessFile();
            Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        }


        public ItemGroupMaster ItemGroupMaster = new ItemGroupMaster();
        String BeforeUpdateItemName = "";
        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;

            //Create Item Group //After Save
            if (BusinessObjectInfo.BeforeAction == false && BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD)
            {
                switch (BusinessObjectInfo.FormTypeEx)
                {
                    case "63":
                        {
                            bool Success = false;
                            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(BusinessObjectInfo.FormUID);

                            SAPbouiCOM.EditText item_group_nameSC = (SAPbouiCOM.EditText)oform.Items.Item("6").Specific;

                            //Item Category radio
                            SAPbouiCOM.OptionBtn service = (SAPbouiCOM.OptionBtn)oform.Items.Item("2000").Specific;
                            SAPbouiCOM.OptionBtn Material = (SAPbouiCOM.OptionBtn)oform.Items.Item("2001").Specific;

                            ItemGroupMaster.item_group_name = item_group_nameSC.Value.ToString();

                            SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            string sqlQuery = string.Format(" select {0}ItmsGrpCod{0} from OITB WHERE {0}ItmsGrpNam{0} ='" + ItemGroupMaster.item_group_name + "'", '"');
                            businessObject.DoQuery(sqlQuery);

                            if (businessObject.RecordCount > 0)
                            {
                                // Get the first Row value
                                if (!businessObject.EoF)
                                {
                                    string ItmsGrpCod = businessObject.Fields.Item("ItmsGrpCod").Value.ToString();
                                    ItemGroupMaster.custom_item_group_code = ItmsGrpCod;
                                }
                            }


                            if (service.Selected)
                            {
                                ItemGroupMaster.custom_item_catagory_code = "1";
                                ItemGroupMaster.custom_item_catagory_name = "Service";
                            }
                            else if (Material.Selected)
                            {
                                ItemGroupMaster.custom_item_catagory_code = "2";
                                ItemGroupMaster.custom_item_catagory_name = "Material";
                            }

                            ///Created/Updated By and Name
                            string CurrentUser = Application.SBO_Application.Company.UserName;
                            ItemGroupMaster.user_id = CurrentUser.ToString();
                            string sqlUname = string.Format("SELECT {0}U_NAME{0} FROM {0}OUSR{0} WHERE {0}USER_CODE{0} = '" + ItemGroupMaster.user_id + "'", '"');
                            businessObject.DoQuery(sqlUname);
                            ItemGroupMaster.user_name = businessObject.Fields.Item("U_NAME").Value.ToString();
                            ///

                            //ERP-Next Default
                            ItemGroupMaster.parent_item_group = "";
                            ItemGroupMaster.is_group = "0";

                            ReturnData data = new ReturnData();
                            Success = SaveAndUpdateItemGroup(ItemGroupMaster, data);

                            if (Success == true && data.cost_center_name != "")
                            {
                                Application.SBO_Application.SetStatusBarMessage("Api Msg:" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Medium, false);
                            }
                            else
                            {
                                Application.SBO_Application.SetStatusBarMessage("Error!" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                                Application.SBO_Application.MessageBox("Item Group Name: " + ItemGroupMaster.item_group_name + " — not synced with ERP-Next.");

                                //BubbleEvent = false; //Bubble Event-> To stop Adding item on Item Master on SAP B1
                            }
                            break;
                        }
                }
            }
            //Update Item Group
            else if (BusinessObjectInfo.BeforeAction == true && BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE)
            {
                switch (BusinessObjectInfo.FormTypeEx)
                {
                    case "63":
                        {
                            bool Success = false;
                            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(BusinessObjectInfo.FormUID);

                            SAPbouiCOM.EditText item_group_nameSC = (SAPbouiCOM.EditText)oform.Items.Item("6").Specific;

                            //Item Category radio
                            SAPbouiCOM.OptionBtn service = (SAPbouiCOM.OptionBtn)oform.Items.Item("2000").Specific;
                            SAPbouiCOM.OptionBtn Material = (SAPbouiCOM.OptionBtn)oform.Items.Item("2001").Specific;

                            SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            string sqlQuery = string.Format(" select {0}ItmsGrpCod{0} from OITB WHERE {0}ItmsGrpNam{0} ='" + BeforeUpdateItemName + "'", '"');
                            businessObject.DoQuery(sqlQuery);

                            if (businessObject.RecordCount > 0)
                            {
                                // Get the first Row value
                                if (!businessObject.EoF)
                                {
                                    string ItmsGrpCod = businessObject.Fields.Item("ItmsGrpCod").Value.ToString();
                                    ItemGroupMaster.custom_item_group_code = ItmsGrpCod;
                                }
                            }

                            ItemGroupMaster.item_group_name = item_group_nameSC.Value.ToString();

                            if (service.Selected)
                            {
                                ItemGroupMaster.custom_item_catagory_code = "1";
                                ItemGroupMaster.custom_item_catagory_name = "Service";
                            }
                            else if (Material.Selected)
                            {
                                ItemGroupMaster.custom_item_catagory_code = "2";
                                ItemGroupMaster.custom_item_catagory_name = "Material";
                            }


                            ///Created/Updated By and Name
                            string CurrentUser = Application.SBO_Application.Company.UserName;
                            ItemGroupMaster.user_id = CurrentUser.ToString();
                            string sqlUname = string.Format("SELECT {0}U_NAME{0} FROM {0}OUSR{0} WHERE {0}USER_CODE{0} = '" + ItemGroupMaster.user_id + "'", '"');
                            businessObject.DoQuery(sqlUname);
                            ItemGroupMaster.user_name = businessObject.Fields.Item("U_NAME").Value.ToString();
                            ///
                            //ERP-Next Default
                            ItemGroupMaster.parent_item_group = "";
                            ItemGroupMaster.is_group = "0";

                            ReturnData data = new ReturnData();
                            Success = SaveAndUpdateItemGroup(ItemGroupMaster, data);

                            if (Success == true && data.cost_center_name != "")
                            {
                                Application.SBO_Application.SetStatusBarMessage("Api Msg:" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Medium, false);
                            }
                            else
                            {
                                Application.SBO_Application.SetStatusBarMessage("Error!" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                                Application.SBO_Application.MessageBox("Item Group Updation failed — not synced with ERP-Next.");

                                BubbleEvent = false; //Bubble Event-> To stop Adding item on Item Master on SAP B1
                            }
                            break;
                        }
                }

            }

            //Update Item Group : Form Load After
            if (BusinessObjectInfo.BeforeAction == false && BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD)
            {
                switch (BusinessObjectInfo.FormTypeEx)
                {
                    case "63":
                        {
                            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(BusinessObjectInfo.FormUID);

                            SAPbouiCOM.EditText item_group_nameSC = (SAPbouiCOM.EditText)oform.Items.Item("6").Specific;
                            ItemGroupMaster.item_group_name = item_group_nameSC.Value.ToString();
                            BeforeUpdateItemName = ItemGroupMaster.item_group_name;
                            break;
                        }
                }
            }

        }

        public bool SaveAndUpdateItemGroup(ItemGroupMaster ItemGroupMaster, ReturnData returnData)
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
                var myContent = JsonConvert.SerializeObject(ItemGroupMaster);
                complete_url = accessFVM.IPPort + "api/method/fusion_hr.controllers.api_controllers.post.item_group_controller.update_or_create_item_group";
                //api / method / fusion_hr.controllers.api_controllers.post.item_group_controller.update_or_create_item_group

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
                    returnData.item_group_name = jsonData.item_group_name;
                    if (returnData.item_group_name != "" && jsonData.status == "created"
                        || returnData.item_group_name != "" && jsonData.status == "updated"
                        || returnData.item_group_name != "" && jsonData.status == "unchanged")
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
