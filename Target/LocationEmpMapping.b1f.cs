using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Target
{
    [FormAttribute("Target.LocationEmpMapping", "LocationEmpMapping.b1f")]
    class LocationEmpMapping : UserFormBase
    {
        public LocationEmpMapping()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("stSlpCod").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("stSlpNam").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("etSlpNam").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("stTypCod").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("cbTypCod").Specific));
            this.ComboBox0.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.ComboBox0_ComboSelectAfter);
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("stTypNam").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("etTypNam").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("stLocCod").Specific));
            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("cbLocCod").Specific));
            this.ComboBox1.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.ComboBox1_ComboSelectAfter);
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("stLocNam").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("etLocNam").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("stsDate").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("etsDate").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("steDate").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("eteDate").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("stStatus").Specific));
            this.ComboBox2 = ((SAPbouiCOM.ComboBox)(this.GetItem("cbStatus").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Button0.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button0_PressedBefore);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("etCode").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("etDocE").Specific));
            this.ComboBox3 = ((SAPbouiCOM.ComboBox)(this.GetItem("cbSlpCod").Specific));
            this.ComboBox3.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.ComboBox3_ComboSelectAfter);
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("stCode").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {

        }
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.ComboBox ComboBox1;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.ComboBox ComboBox2;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.EditText EditText7;
        //Location Type
        private void ComboBox0_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            //throw new System.NotImplementedException();
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
            SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP");
            SAPbouiCOM.ComboBox comboBox = (SAPbouiCOM.ComboBox)oform.Items.Item("cbTypCod").Specific;
            // Get the selected value
            string TyeCode = comboBox.Selected.Value;
            string Name = "";
            if (TyeCode != "")
            {
                SAPbobsCOM.Recordset oRecordset = null;
                oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sqlQuery = string.Format("SELECT A.{0}Code{0},A.{0}Name{0}  from {0}@FIL_MD_LOCTYPE{0} A Where A.{0}Code{0}='" + TyeCode + "' ", '"');
                oRecordset.DoQuery(sqlQuery);
                if (oRecordset.RecordCount > 0)
                {
                    //SAPbouiCOM.EditText oedtTypNam;
                    //oedtTypNam = (SAPbouiCOM.EditText)oform.Items.Item("etTypNam").Specific;
                    Name = oRecordset.Fields.Item("Name").Value.ToString();
                    //oedtTypNam.Value = Name;
                    oDBH.SetValue("U_LOTYNAME", 0, Name);
                }



                string sqlQueryp = string.Format("SELECT B.{0}Code{0},B.{0}Name{0}  from {0}@FIL_MD_LOCTYPE{0} A inner join {0}@FIL_MD_LOCATION{0} B on A.{0}Code{0}=B.{0}U_LOTYCODE{0} Where A.{0}Code{0}='" + TyeCode + "' ", '"');

                SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)oform.Items.Item("cbLocCod").Specific;   //object defining- Define a combo box
                //ocmbDept.ValidValues.Add("", "");
                //ocmbDept.Select("");
                oDBH.SetValue("U_LOCNAME", 0, "");
                Global.objFun.setComboBoxWithoutDescription(ocmbDept, sqlQueryp);
            }

        }

        private SAPbouiCOM.ComboBox ComboBox3;
        //Sale Person
        private void ComboBox3_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            //throw new System.NotImplementedException();
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
            SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP");
            SAPbouiCOM.ComboBox comboBox = (SAPbouiCOM.ComboBox)oform.Items.Item("cbSlpCod").Specific;
            // Get the selected value
            string SlpCode = comboBox.Selected.Value;
            string SlpName = "";
            if (SlpCode != "")
            {
                SAPbobsCOM.Recordset oRecordset = null;
                oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sqlQuery = string.Format("SELECT A.{0}SlpCode{0},A.{0}SlpName{0}  from {0}OSLP{0} A Where A.{0}SlpCode{0}='" + SlpCode + "' ", '"');
                oRecordset.DoQuery(sqlQuery);
                if (oRecordset.RecordCount > 0)
                {
                    //SAPbouiCOM.EditText oedtTypNam;
                    //oedtTypNam = (SAPbouiCOM.EditText)oform.Items.Item("etTypNam").Specific;
                    SlpName = oRecordset.Fields.Item("SlpName").Value.ToString();
                    //oedtTypNam.Value = Name;
                    oDBH.SetValue("U_EMPNAME", 0, SlpName);
                }
            }

        }
        //Location
        private void ComboBox1_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
            SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP");
            SAPbouiCOM.ComboBox comboBox = (SAPbouiCOM.ComboBox)oform.Items.Item("cbLocCod").Specific;
            // Get the selected value
            string LocCode = comboBox.Selected.Value;
            string LocName = "";
            if (LocCode != "")
            {
                SAPbobsCOM.Recordset oRecordset = null;
                oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sqlQuery = string.Format("SELECT A.{0}Code{0},A.{0}Name{0}  from {0}@FIL_MD_LOCATION{0} A Where A.{0}Code{0}='" + LocCode + "' ", '"');
                oRecordset.DoQuery(sqlQuery);
                if (oRecordset.RecordCount > 0)
                {
                    //SAPbouiCOM.EditText oedtTypNam;
                    //oedtTypNam = (SAPbouiCOM.EditText)oform.Items.Item("etTypNam").Specific;
                    LocName = oRecordset.Fields.Item("Name").Value.ToString();
                    //oedtTypNam.Value = Name;
                    oDBH.SetValue("U_LOCNAME", 0, LocName);
                }
            }

        }

        private void Button0_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
            if (oform.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || oform.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
            {
                ValidateForm(ref oform, ref BubbleEvent);
            }
            if (oform.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                SAPbouiCOM.DBDataSource ods = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP");
                ods.SetValue("Code", 0, Global.objFun.GetCodeGeneration("@FIL_MD_LOCWEMP").ToString());
            }
        }
        private bool ValidateForm(ref SAPbouiCOM.Form pForm, ref bool BubbleEvent)
        {
            DateTime fromDate, toDate;
            string sfromDate = "";
            string stoDate="";
            string oCode = pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("Code", 0);
            string oFromDate = pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_STDATE", 0);
            string oToDate = pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_EDDATE", 0);
            string oEMPID = pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_EMPID", 0);
            //int oEMPID;
            //if (pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_EMPID", 0) != "")
            //{
            //    oEMPID = Convert.ToInt32(pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_EMPID", 0));
            //}
            string oLOTYCODE = pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_LOTYCODE", 0);
            string oLOCCODE = pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_LOCCODE", 0);

            if (pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_EMPID", 0) == "")
            {
                Global.objFun.ShowError("Select Sales Person");
                pForm.ActiveItem = "cbSlpCod";
                return BubbleEvent = false;
            }
            else if (pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_EMPNAME", 0) == "")
            {
                Global.objFun.ShowError("Enter Sales Person");
                pForm.ActiveItem = "etSlpNam";
                return BubbleEvent = false;
            }
            else if (oLOTYCODE == "")
            {
                Global.objFun.ShowError("Select Location Type");
                pForm.ActiveItem = "cbTypCod";
                return BubbleEvent = false;
            }
            else if (pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_LOTYNAME", 0) == "")
            {
                Global.objFun.ShowError("Enter Location Type");
                pForm.ActiveItem = "etTypNam";
                return BubbleEvent = false;
            }
            else if (oLOCCODE == "")
            {
                Global.objFun.ShowError("Select Location");
                pForm.ActiveItem = "cbLocCod";
                return BubbleEvent = false;
            }
            else if (pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_LOCNAME", 0) == "")
            {
                Global.objFun.ShowError("Enter Location");
                pForm.ActiveItem = "etLocNam";
                return BubbleEvent = false;
            }
            else if (oFromDate == "")
            {
                Global.objFun.ShowError("Enter Start Date");
                pForm.ActiveItem = "etsDate";
                return BubbleEvent = false;
            }
            else if (oToDate == "")
            {
                Global.objFun.ShowError("Enter End Date");
                pForm.ActiveItem = "eteDate";
                return BubbleEvent = false;
            }
            else if (pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_STATUS", 0) == "")
            {
                Global.objFun.ShowError("Select Status");
                pForm.ActiveItem = "cbStatus";
                return BubbleEvent = false;
            }
            else if (oFromDate != "" && oToDate != "")
            {
                fromDate = new DateTime(Convert.ToInt32(oFromDate.Substring(0, 4)), Convert.ToInt32(oFromDate.Substring(4, 2)), Convert.ToInt32(oFromDate.Substring(6, 2)));
                sfromDate = fromDate.ToString("yyyy-MM-dd");
                toDate = new DateTime(Convert.ToInt32(oToDate.Substring(0, 4)), Convert.ToInt32(oToDate.Substring(4, 2)), Convert.ToInt32(oToDate.Substring(6, 2)));
                stoDate = toDate.ToString("yyyy-MM-dd");
                if (toDate < fromDate)
                {
                    Global.objFun.ShowError("Error: 'To Date' cannot be earlier than 'From Date'.");
                    pForm.ActiveItem = "eteDate";
                    return BubbleEvent = false;
                }
            }

            //if (oFromDate != "" && oToDate != "")
            //{
            //    fromDate = new DateTime(Convert.ToInt32(oFromDate.Substring(0, 4)), Convert.ToInt32(oFromDate.Substring(4, 2)), Convert.ToInt32(oFromDate.Substring(6, 2)));

            //    toDate = new DateTime(Convert.ToInt32(oToDate.Substring(0, 4)), Convert.ToInt32(oToDate.Substring(4, 2)), Convert.ToInt32(oToDate.Substring(6, 2)));
            //    if (toDate < fromDate)
            //    {
            //        Global.objFun.ShowError("Error: 'To Date' cannot be earlier than 'From Date'.");
            //        pForm.ActiveItem = "eteDate";
            //        return BubbleEvent = false;
            //    }
            //}
            //string inputDate = "20240205";
            //if (DateTime.TryParseExact(oFromDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            //{
            //    sfromDate = parsedDate.ToString("yyyy-MM-dd");
            //   // Console.WriteLine(formattedDate); // Output: 2024-02-05
            //}
            //else
            //{
            //    Console.WriteLine("Invalid date format");
            //}

            //sfromDate = fromDate.ToString("yyyy-MM-dd");
            //sfromDate = Convert.ToDateTime(fromDate).ToString("yyyy/MM/dd");
            //sfromDate = fromDate;
            //stoDate = toDate.ToString("yyyy-MM-dd");

            if (pForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                SAPbobsCOM.Recordset oRecordset = null;
                oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                //string sqlQuery = string.Format("SELECT {0}Code{0} from {0}@FIL_MD_LOCWEMP{0} Where {0}Code{0}<>'" + oCode + "' and {0}U_EMPID{0}='" + oEMPID + "' and {0}U_LOTYCODE{0}='" + oLOTYCODE + "' and {0}U_LOCCODE{0}='" + oLOCCODE + "' and {0}U_STATUS{0}='Y'", '"');
                string sqlQuery = string.Format("SELECT {0}Code{0} from {0}@FIL_MD_LOCWEMP{0} Where {0}Code{0}<>'" + oCode + "' and {0}U_EMPID{0}='" + oEMPID + "' and {0}U_STATUS{0}='Y'", '"');
                oRecordset.DoQuery(sqlQuery);
                if (oRecordset.RecordCount > 0)
                {
                    Global.objFun.ShowError("Error: Can not allow Duplicate Active Location Mapping.");
                    // pForm.ActiveItem = "eteDate";
                    return BubbleEvent = false;
                }

                //sqlQuery = string.Format("SELECT {0}Code{0} from {0}@FIL_MD_LOCWEMP{0} Where {0}Code{0}<>'" + oCode + "' and {0}U_EMPID{0}='" + oEMPID + "' and {0}U_LOTYCODE{0}='" + oLOTYCODE + "' and {0}U_LOCCODE{0}='" + oLOCCODE + "' and TO_DATE("+ oFromDate + ", 'YYYY-MM-DD') BETWEEN  TO_DATE({0}U_STDATE{0}, 'YYYY-MM-DD') and TO_DATE({0}U_EDDATE{0}, 'YYYY-MM-DD') ", '"');TO_CHAR

            }
            SAPbobsCOM.Recordset oRecordset1 = null;
            oRecordset1 = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string sqlQuery1 = string.Format("SELECT {0}Code{0} from {0}@FIL_MD_LOCWEMP{0} Where {0}Code{0}<>'" + oCode + "' and  {0}U_LOTYCODE{0}='" + oLOTYCODE + "' and {0}U_LOCCODE{0}='" + oLOCCODE + "' and " + oFromDate + " BETWEEN  TO_CHAR({0}U_STDATE{0}, 'YYYYMMDD') and TO_CHAR({0}U_EDDATE{0}, 'YYYYMMDD') ", '"');
            oRecordset1.DoQuery(sqlQuery1);
            if (oRecordset1.RecordCount > 0)
            {
                Global.objFun.ShowError("Error: Can not allow Same Date Range for Selected Start Date.");
                // pForm.ActiveItem = "eteDate";
                return BubbleEvent = false;
            }
            SAPbobsCOM.Recordset oRecordset2 = null;
            oRecordset2 = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string sqlQuery2 = string.Format("SELECT {0}Code{0} from {0}@FIL_MD_LOCWEMP{0} Where {0}Code{0}<>'" + oCode + "' and  {0}U_LOTYCODE{0}='" + oLOTYCODE + "' and {0}U_LOCCODE{0}='" + oLOCCODE + "' and " + oToDate + " BETWEEN  TO_CHAR({0}U_STDATE{0}, 'YYYYMMDD') and TO_CHAR({0}U_EDDATE{0}, 'YYYYMMDD') ", '"');
            oRecordset2.DoQuery(sqlQuery2);
            if (oRecordset2.RecordCount > 0)
            {
                Global.objFun.ShowError("Error: Can not allow Same Date Range for Selected End Date.");
                // pForm.ActiveItem = "eteDate";
                return BubbleEvent = false;
            }
            //select * from "@FIL_MD_LOCWEMP" where "Code" <> '' and "U_EMPID" = '' and "U_LOTYCODE" = '' and "U_LOCCODE" = '' and "U_STATUS" = 'Y'
            if (pForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
            {
               
                if(pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCWEMP").GetValue("U_STATUS", 0) == "N" && oToDate != "")
                {
                    toDate = new DateTime(Convert.ToInt32(oToDate.Substring(0, 4)), Convert.ToInt32(oToDate.Substring(4, 2)), Convert.ToInt32(oToDate.Substring(6, 2)));
                    DateTime currentDate = DateTime.Now;
                    if (toDate > currentDate)
                    {
                        Global.objFun.ShowError("Error: When Status is InActive than 'End Date' cannot be earlier than 'Current Date'.");
                        pForm.ActiveItem = "eteDate";
                        return BubbleEvent = false;
                    }
                }
            }
           
            return BubbleEvent;
        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            //SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);

        }

        private SAPbouiCOM.StaticText StaticText9;
    }
}
