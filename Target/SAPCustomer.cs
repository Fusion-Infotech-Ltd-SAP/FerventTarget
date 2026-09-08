using Newtonsoft.Json;
using SAPbouiCOM.Framework;
using System;
using System.IO;
using System.Net;
using Target.Model;

namespace Target
{
    class SAPCustomer
    {
        private AccessFileViewModel accessFVM;
        public SAPCustomer()
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
                    case "134":
                        {
                            bool Success = false;
                            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(BusinessObjectInfo.FormUID);

                            //Type 
                            SAPbouiCOM.ComboBox ISCustomer = (SAPbouiCOM.ComboBox)oform.Items.Item("40").Specific;
                            string ISCustomerY = ISCustomer.Selected.Description.Trim();

                            if (ISCustomerY == "Customer")
                            {
                                SAPbouiCOM.DBDataSource oDBS = oform.DataSources.DBDataSources.Item("OCRD"); // or "@YOUR_UDT"

                                string Latitude = oDBS.GetValue("U_LATITUDE", 0);
                                string Longitude = oDBS.GetValue("U_LONGITUDE", 0);
                                string Radius = oDBS.GetValue("U_RADIUS", 0);
                                string LocationCode = oDBS.GetValue("U_LOCCODE", 0);
                                string Locationname = oDBS.GetValue("U_LOCNAME", 0);

                                SAPbouiCOM.EditText BpCode = (SAPbouiCOM.EditText)oform.Items.Item("5").Specific;
                                SAPbouiCOM.EditText BpName = (SAPbouiCOM.EditText)oform.Items.Item("7").Specific;
                                SAPbouiCOM.EditText StartDate = (SAPbouiCOM.EditText)oform.Items.Item("10002058").Specific;
                                SAPbouiCOM.EditText EndDate = (SAPbouiCOM.EditText)oform.Items.Item("10002055").Specific;

                                SAPbouiCOM.ComboBox BpGroup = (SAPbouiCOM.ComboBox)oform.Items.Item("16").Specific;
                                string BpGroupCode = BpGroup.Selected.Description.Trim(); //"Corporate Customers";

                                SAPbobsCOM.Recordset businessObject2 = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                SAPbouiCOM.ComboBox BpType = (SAPbouiCOM.ComboBox)oform.Items.Item("362").Specific;
                                SAPbouiCOM.ComboBox SalesPerson = (SAPbouiCOM.ComboBox)oform.Items.Item("52").Specific;

                                string sqlQuery = string.Format("SELECT T2.{0}ExtEmpNo{0} FROM {0}OSLP{0} T1 LEFT OUTER JOIN {0}OHEM{0} T2 On T1.{0}SlpCode{0} = T2.{0}salesPrson{0} WHERE T1.{0}SlpCode{0} = '" + SalesPerson.Selected.Value.Trim() + "'", '"');
                                businessObject2.DoQuery(sqlQuery);
                                string SalesPersonCode = "";
                                if (businessObject2.RecordCount > 0)
                                {
                                    // Get the first Row value
                                    if (!businessObject2.EoF)
                                    {
                                        string ExtEmpNo = businessObject2.Fields.Item("ExtEmpNo").Value.ToString();
                                        SalesPersonCode = ExtEmpNo;//;
                                    }
                                }

                                SAPbouiCOM.OptionBtn StatusActive = (SAPbouiCOM.OptionBtn)oform.Items.Item("10002044").Specific;

                                CustomerMaster CustomerMaster = new CustomerMaster();
                                CustomerMaster.custom_customer_code = BpCode.Value;
                                CustomerMaster.customer_name = BpName.Value;
                                switch (BpType.Value)
                                {
                                    case "C":
                                        CustomerMaster.customer_type = "Company";
                                        break;
                                    case "I":
                                        CustomerMaster.customer_type = "Private";
                                        break;
                                    case "G":
                                        CustomerMaster.customer_type = "Government";
                                        break;
                                    case "E":
                                        CustomerMaster.customer_type = "Employee";
                                        break;
                                    default:
                                        break;
                                }
                                CustomerMaster.customer_group = BpGroupCode;

                                if (StatusActive.Selected)
                                {
                                    CustomerMaster.custom_status = "Active";
                                    CustomerMaster.custom_start_date = StartDate.Value;
                                    CustomerMaster.custom_end_date = EndDate.Value;
                                }
                                else { CustomerMaster.custom_status = "Inactive"; }

                                CustomerMaster.custom_sales_person = SalesPersonCode;
                                CustomerMaster.custom_location_code = LocationCode;
                                CustomerMaster.custom_location = Locationname;
                                CustomerMaster.custom_latitude = Latitude;
                                CustomerMaster.custom_longitude = Longitude;
                                CustomerMaster.custom_radius = Radius;

                                ///Created/Updated By and Name
                                SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                string CurrentUser = Application.SBO_Application.Company.UserName;
                                CustomerMaster.user_id = CurrentUser.ToString();
                                string sqlUname = string.Format("SELECT {0}U_NAME{0} FROM {0}OUSR{0} WHERE {0}USER_CODE{0} = '" + CustomerMaster.user_id + "'", '"');
                                businessObject.DoQuery(sqlUname);
                                CustomerMaster.user_name = businessObject.Fields.Item("U_NAME").Value.ToString();

                                ReturnData data = new ReturnData();
                                Success = SaveAndUpdateBPMaster(CustomerMaster, data);

                                if (Success == true && data.item_code != "")
                                {
                                    Application.SBO_Application.SetStatusBarMessage("Api Msg:" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Medium, false);
                                }
                                else
                                {
                                    Application.SBO_Application.SetStatusBarMessage("Error!-" + data.ReturnMsg, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                                    Application.SBO_Application.MessageBox("Customer Creation OR Updation failed — not synced with ERP-Next.");

                                    BubbleEvent = false; //Bubble Event-> To stop Adding item on Item Master on SAP B1
                                }
                                break;
                            }
                            break;
                        }
                }
            }

        }

        public bool SaveAndUpdateBPMaster(CustomerMaster CustomerMaster, ReturnData returnData)
        {
            string complete_url = "", msg = "";
            bool isSuccess = false;
            Stream dataStream;
            StreamReader reader;
            HttpWebRequest HTTP_Request;
            HttpWebResponse HTTP_Response;
            try
            {
                var myContent = JsonConvert.SerializeObject(CustomerMaster);
                complete_url = accessFVM.IPPort + "api/method/fusion_hr.controllers.api_controllers.post.customer_controller.create_or_update_customer";
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
                //string Retcustomer_code = obj.message.custom_customer_code;

                if (RetStatus != null)
                {
                    //returnData.custom_customer_code = Retcustomer_code;
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