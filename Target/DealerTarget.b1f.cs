using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Target
{
    [FormAttribute("Target.DealerTarget", "DealerTarget.b1f")]
    class DealerTarget : UserFormBase
    {
        public DealerTarget()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("stDerCod").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("stSlpCod").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("etDerCod").Specific));
            this.EditText0.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText0_ChooseFromListAfter);
            this.EditText0.ChooseFromListBefore += new SAPbouiCOM._IEditTextEvents_ChooseFromListBeforeEventHandler(this.EditText0_ChooseFromListBefore);
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("etLocCod").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("etSlpCod").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("stDerNam").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("etDerNam").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("stSlpNam").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("etSlpNam").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Button0.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button0_PressedBefore);
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("mtxDetls").Specific));
            this.Matrix0.LostFocusAfter += new SAPbouiCOM._IMatrixEvents_LostFocusAfterEventHandler(this.Matrix0_LostFocusAfter);
           
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("btnFlUp").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
            this.Button1.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button1_PressedBefore);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.DataLoadAfter += new DataLoadAfterHandler(this.Form_DataLoadAfter);

        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText EditText4;
     
        private void EditText0_ChooseFromListBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            //throw new System.NotImplementedException();
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
            //if (oform.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || oform.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
            //{
            //    ValidateCFL(ref oform, ref BubbleEvent);
            //}

            //if (pVal.InnerEvent == true && pVal.ItemUID == "etDerCod")
            //{
            //    SAPbobsCOM.Recordset oRecordset = null;
            //    string oFYR = oform.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR").GetValue("U_FYR", 0);
            //    if (oFYR != "")
            //    {

            //        oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            //        string sqlQuery = string.Format("SELECT A.{0}U_CARDCODE{0},A.{0}U_CARDNAME{0}  from {0}@FIL_DD_DEALERTAR{0} A Where A.{0}U_FYR{0}='" + oFYR + "' ", '"');
            //        oRecordset.DoQuery(sqlQuery);
            //    }


            //    SAPbouiCOM.ChooseFromList ocfl = (SAPbouiCOM.ChooseFromList)oform.ChooseFromLists.Item("CFL_OCRD");
            //    oCons = null;
            //    oCon = null;
            //    ocfl.SetConditions(oCons);
            //    oCons = ocfl.GetConditions();

            //    oCon = oCons.Add();
            //    oCon.Alias = "CardCode";
            //    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_CONTAIN;
            //    oCon.CondVal = "105";
            //    ocfl.SetConditions(oCons);

            //}
        }
        private bool ValidateCFL(ref SAPbouiCOM.Form pForm, ref bool BubbleEvent)
        {
            string oFYR = pForm.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR").GetValue("U_FYR", 0);
            if (oFYR == "")
            {
                Global.objFun.ShowError("Select Fiscal Year");
                pForm.ActiveItem = "cbFYR";
                return BubbleEvent = false;
            }
            return BubbleEvent;
        }
        private void EditText0_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            //throw new System.NotImplementedException();
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item("FIL_FRM_DD_DEALERTAR"); // DEFINE A FORM
            SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR");
            if (oform.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || oform.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
            {
                SAPbouiCOM.ISBOChooseFromListEventArg cflEvent = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal; //Assign a cfl and call event
                string Uid = cflEvent.ChooseFromListUID;  // cfl unique id
                string strCode = "";

                SAPbouiCOM.DataTable dtbCFL = cflEvent.SelectedObjects;
                if (!dtbCFL.IsEmpty)
                {
                    strCode = dtbCFL.GetValue("CardCode", 0).ToString();

                    SAPbobsCOM.Recordset oRecordset = null;
                    oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string sqlQuery = string.Format("select A.{0}CardCode{0},A.{0}CardName{0},B.{0}Code{0} {0}AreaCode{0},B.{0}Name{0} {0}AreaName{0},C.{0}U_EMPID{0}  {0}SlpCode{0},C.{0}U_EMPNAME{0} {0}SlpName{0} from {0}OCRD{0} A inner join {0}@FIL_MD_LOCATION{0} B on A.{0}U_LOCCODE{0} = B.{0}Code{0}  inner join {0}@FIL_MD_LOCWEMP{0} C on B.{0}Code{0} = C.{0}U_LOCCODE{0} and C.{0}U_STATUS{0} = 'Y' where A.{0}CardCode{0} = '" + strCode + "' ", '"');

                    oRecordset.DoQuery(sqlQuery);
                    SAPbouiCOM.EditText oSlpCode = (SAPbouiCOM.EditText)oform.Items.Item("etSlpCod").Specific;
                    SAPbouiCOM.EditText oSlpName = (SAPbouiCOM.EditText)oform.Items.Item("etSlpNam").Specific;
                    if (oRecordset.RecordCount > 0)
                    {
                        string Name = oRecordset.Fields.Item("CardName").Value.ToString();
                        oDBH.SetValue("U_CARDCODE", 0, strCode);
                        oDBH.SetValue("U_CARDNAME", 0, Name);
                        oDBH.SetValue("U_LOCCODE", 0, oRecordset.Fields.Item("AreaCode").Value.ToString());
                        oDBH.SetValue("U_LOCNAME", 0, oRecordset.Fields.Item("AreaName").Value.ToString());
                        oSlpCode.Value = oRecordset.Fields.Item("SlpCode").Value.ToString();
                        oSlpName.Value = oRecordset.Fields.Item("SlpName").Value.ToString();
                    }
                    // SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)oform.Items.Item("mtxDetls").Specific;
                    //Global.objFun.DisablePastMonthsRows(omat);
                }
            }
        }

        private SAPbouiCOM.Button Button0;

        private void Button0_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
            if (oform.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || oform.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
            {
                ValidateForm(ref oform, ref BubbleEvent);
            }

        }
        private bool ValidateForm(ref SAPbouiCOM.Form pForm, ref bool BubbleEvent)
        {
            string oCARDCODE = pForm.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR").GetValue("U_CARDCODE", 0);
            string oFYR = pForm.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR").GetValue("U_FYR", 0);
            if (oCARDCODE == "")
            {
                Global.objFun.ShowError("Select Dealer");
                pForm.ActiveItem = "etDerCod";
                return BubbleEvent = false;
            }
            else if (oFYR == "")
            {
                Global.objFun.ShowError("Select Fiscal Year");
                pForm.ActiveItem = "cbFYR";
                return BubbleEvent = false;
            }


            if (pForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                if (oCARDCODE != "")
                {
                    SAPbobsCOM.Recordset oRecordset1 = null;
                    oRecordset1 = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string sqlQuery1 = string.Format("SELECT {0}DocEntry{0} from {0}@FIL_DD_DEALERTAR{0} Where {0}U_FYR{0}='" + oFYR + "' and  {0}U_CARDCODE{0}='" + oCARDCODE + "' ", '"');

                    oRecordset1.DoQuery(sqlQuery1);
                    if (oRecordset1.RecordCount > 0)
                    {
                        Global.objFun.ShowError("Error: Can not allow duplicate Dealer Target for Selected Fiscal Year.");
                        // pForm.ActiveItem = "eteDate";
                        return BubbleEvent = false;
                    }
                }
            }


            return BubbleEvent;
        }
        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
            SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR");
            SAPbouiCOM.DBDataSource oDBL = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_DR_DEALERTAR");   //DEFINE  DATASOURCES.1
            if (oform.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                //ods.SetValue("DocNum", 0, Global.objFun.GetCodeGeneration("@FIL_DD_DEALERTAR").ToString());
                Global.objFun.RefreshDealer(oform);
                //-----------------------Load------------------------------------------
                            string strqry = string.Format("SELECT A.{0}ItmsGrpCod{0},A.{0}ItmsGrpNam{0}  from {0}OITB{0} A Where A.{0}U_TARGET{0}='Y' ", '"');


                SAPbobsCOM.Recordset ors = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                ors.DoQuery(strqry); //WILL CALL THE RECORDSET AND TO PASS THE STRING WHERE DECLARED THE QUERY.

                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)oform.Items.Item("mtxDetls").Specific;//define matrix


                omat.FlushToDataSource(); // move the data from matrix to dbdatasource
                omat.Clear();// clear or remove from matrix
                oDBL.Clear(); // clear or remove from dbdatasource(in backend table)
                for (int irow = 1; irow <= ors.RecordCount; irow++) //ToString load a data from record set to dbdatasource
                {
                    oDBL.InsertRecord(irow - 1);
                    oDBL.SetValue("LineId", irow - 1, irow.ToString());
                    oDBL.SetValue("U_ITMGRP", irow - 1, ors.Fields.Item("ItmsGrpCod").Value.ToString());
                    oDBL.SetValue("U_ITMGRPNM", irow - 1, ors.Fields.Item("ItmsGrpNam").Value.ToString());
                    //to move recordset
                    ors.MoveNext();
                }
                omat.LoadFromDataSource();
                omat.AutoResizeColumns();
            }
            //if (oform.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || oform.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
            //{
            //    Global.objFun.RefreshDealer(oform);

            //}
            if (oform.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
            {
                string strCode = oDBH.GetValue("U_CARDCODE", 0);
                if (strCode != "")
                {
                    Global.objFun.SetByDealer(oform, oDBH);
                }

            }

        }

        private SAPbouiCOM.Matrix Matrix0;

        private void Matrix0_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Form oform = (SAPbouiCOM.Form)Application.SBO_Application.Forms.Item("FIL_FRM_DD_DEALERTAR");
            try
            {
                oform.Freeze(true);
                /*                SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_DD_SALESTAR"); */  //DEFINE header DATASOURCES.
                SAPbouiCOM.DBDataSource oDBL = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_DR_DEALERTAR");   //DEFINE matrix DATASOURCES.
                SAPbouiCOM.Matrix omat;//MATRIX DECLARING
                                       //define temporary variable.
                double dbSjul = 0.0; double dbSaug = 0.0; double dbSsep = 0.0; double dbSoct = 0.0;
                double dbSnov = 0.0; double dbSdec = 0.0; double dbSjan = 0.0; double dbSfeb = 0.0;
                double dbSmar = 0.0; double dbSapr = 0.0; double dbSmay = 0.0; double dbSjun = 0.0;
                double dbStotal = 0.0;


                omat = (SAPbouiCOM.Matrix)oform.Items.Item("mtxDetls").Specific; //assign /define a matrix
                //Total Sales 
                if (pVal.ColUID == "mSjul" || pVal.ColUID == "mSaug" || pVal.ColUID == "mSsep" || pVal.ColUID == "mSoct" || pVal.ColUID == "mSnov" || pVal.ColUID == "mSdec" || pVal.ColUID == "mSjan" || pVal.ColUID == "mSfeb" || pVal.ColUID == "mSmar" || pVal.ColUID == "mSapr" || pVal.ColUID == "mSmay" || pVal.ColUID == "mSjun")
                {
                    omat.FlushToDataSource();
                    //Get the value from Quantity
                    dbSjul = Convert.ToDouble(oDBL.GetValue("U_SJUL", pVal.Row - 1).ToString());
                    dbSaug = Convert.ToDouble(oDBL.GetValue("U_SAUG", pVal.Row - 1).ToString());
                    dbSsep = Convert.ToDouble(oDBL.GetValue("U_SSEP", pVal.Row - 1).ToString());
                    dbSoct = Convert.ToDouble(oDBL.GetValue("U_SOCT", pVal.Row - 1).ToString());

                    dbSnov = Convert.ToDouble(oDBL.GetValue("U_SNOV", pVal.Row - 1).ToString());
                    dbSdec = Convert.ToDouble(oDBL.GetValue("U_SDEC", pVal.Row - 1).ToString());
                    dbSjan = Convert.ToDouble(oDBL.GetValue("U_SJAN", pVal.Row - 1).ToString());
                    dbSfeb = Convert.ToDouble(oDBL.GetValue("U_SFEB", pVal.Row - 1).ToString());

                    dbSmar = Convert.ToDouble(oDBL.GetValue("U_SMAR", pVal.Row - 1).ToString());
                    dbSapr = Convert.ToDouble(oDBL.GetValue("U_SAPR", pVal.Row - 1).ToString());
                    dbSmay = Convert.ToDouble(oDBL.GetValue("U_SMAY", pVal.Row - 1).ToString());
                    dbSjun = Convert.ToDouble(oDBL.GetValue("U_SJUN", pVal.Row - 1).ToString());


                    dbStotal = dbSjul + dbSaug + dbSsep + dbSoct + dbSnov + dbSdec + dbSjan + dbSfeb + dbSmar + dbSapr + dbSmay + dbSjun;
                    oDBL.SetValue("U_STARGET", pVal.Row - 1, dbStotal.ToString()); // seting the data or value using db datasource.
                    omat.LoadFromDataSource();
                }

                double dbCjul = 0.0; double dbCaug = 0.0; double dbCsep = 0.0; double dbCoct = 0.0;
                double dbCnov = 0.0; double dbCdec = 0.0; double dbCjan = 0.0; double dbCfeb = 0.0;
                double dbCmar = 0.0; double dbCapr = 0.0; double dbCmay = 0.0; double dbCjun = 0.0;
                double dbCtotal = 0.0;
                //Total Collection
                if (pVal.ColUID == "mCjul" || pVal.ColUID == "mCaug" || pVal.ColUID == "mCsep" || pVal.ColUID == "mCoct" || pVal.ColUID == "mCnov" || pVal.ColUID == "mCdec" || pVal.ColUID == "mCjan" || pVal.ColUID == "mCfeb" || pVal.ColUID == "mCmar" || pVal.ColUID == "mCapr" || pVal.ColUID == "mCmay" || pVal.ColUID == "mCjun")
                {
                    omat.FlushToDataSource();
                    //Get the value from Quantity
                    dbCjul = Convert.ToDouble(oDBL.GetValue("U_CJUL", pVal.Row - 1).ToString());
                    dbCaug = Convert.ToDouble(oDBL.GetValue("U_CAUG", pVal.Row - 1).ToString());
                    dbCsep = Convert.ToDouble(oDBL.GetValue("U_CSEP", pVal.Row - 1).ToString());
                    dbCoct = Convert.ToDouble(oDBL.GetValue("U_COCT", pVal.Row - 1).ToString());

                    dbCnov = Convert.ToDouble(oDBL.GetValue("U_CNOV", pVal.Row - 1).ToString());
                    dbCdec = Convert.ToDouble(oDBL.GetValue("U_CDEC", pVal.Row - 1).ToString());
                    dbCjan = Convert.ToDouble(oDBL.GetValue("U_CJAN", pVal.Row - 1).ToString());
                    dbCfeb = Convert.ToDouble(oDBL.GetValue("U_CFEB", pVal.Row - 1).ToString());

                    dbCmar = Convert.ToDouble(oDBL.GetValue("U_CMAR", pVal.Row - 1).ToString());
                    dbCapr = Convert.ToDouble(oDBL.GetValue("U_CAPR", pVal.Row - 1).ToString());
                    dbCmay = Convert.ToDouble(oDBL.GetValue("U_CMAY", pVal.Row - 1).ToString());
                    dbCjun = Convert.ToDouble(oDBL.GetValue("U_CJUN", pVal.Row - 1).ToString());


                    dbCtotal = dbCjul + dbCaug + dbCsep + dbCoct + dbCnov + dbCdec + dbCjan + dbCfeb + dbCmar + dbCapr + dbCmay + dbCjun;
                    oDBL.SetValue("U_CTARGET", pVal.Row - 1, dbCtotal.ToString()); // seting the data or value using db datasource.
                    omat.LoadFromDataSource();
                }

                oform.Freeze(false);
            }
            catch (Exception)
            {
                oform.Freeze(false);
            }
        }


        private SAPbouiCOM.Button Button1;


        private void Button1_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            Global.currentProcess = OpenDialog.GetCurrentProcessID();
            if (OpenDialog.GetExcelFile(ref Global.FilePath, ref Global.FileName))
            {
                if (Global.FilePath != null && Global.FilePath != "")
                {
                    //int length = 27;
                    //FilePath = FilePath.Substring(FilePath.Length - length); 
                    if (Global.FileName != "DealerTargetUploadFile.xlsx")
                    {
                        Global.objFun.ShowError("Error: Please Select Specific Excel File.");
                        BubbleEvent = false;
                    }
                }
            }
            //OpenDialog.SelectAndReadExcel();
            //string filePath = Global.FilePath;
            //if (filePath != null && filePath != "")
            //{
            //    int length = 27;
            //    filePath = filePath.Substring(filePath.Length - length); ;
            //    if (filePath != "DealerTargetUploadFile.xlsx")
            //    {
            //        Global.objFun.ShowError("Error: Please Select Specific Excel File.");
            //        BubbleEvent = false;
            //    }
            //}

        }

        private void Button1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                DataSet ds = ReadExcelFile.ReadExcelFileForm();
                Global.objFun.ShowError("Processing......................");
                if (ds.Tables.Count > 0)
                {
                    DataTable dtTable = new DataTable();
                    DataColumn dc = new DataColumn("FYR", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CARDCODE", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CARDNAME", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("LOCCODE", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("LOCNAME", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("ITMGRP", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("ITMGRPNM", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("STARGET", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CTARGET", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SJUL", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CJUL", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SAUG", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CAUG", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SSEP", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CSEP", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SOCT", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("COCT", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SNOV", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CNOV", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SDEC", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CDEC", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SJAN", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CJAN", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SFEB", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CFEB", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SMAR", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CMAR", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SAPR", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CAPR", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SMAY", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CMAY", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("SJUN", typeof(String));
                    dtTable.Columns.Add(dc);

                    dc = new DataColumn("CJUN", typeof(String));
                    dtTable.Columns.Add(dc);

                    foreach (DataTable dt in ds.Tables)
                    {
                        if (dt.TableName == "Table1")
                        {
                            foreach (DataRow dtRow in dt.Rows)
                            {
                                DataRow NewRow = dtTable.NewRow();
                                NewRow[0] = dtRow.ItemArray[0].ToString();
                                NewRow[1] = dtRow.ItemArray[1].ToString();
                                NewRow[2] = dtRow.ItemArray[2].ToString();
                                NewRow[3] = dtRow.ItemArray[3].ToString();
                                NewRow[4] = dtRow.ItemArray[4].ToString();
                                NewRow[5] = dtRow.ItemArray[5].ToString();
                                NewRow[6] = dtRow.ItemArray[6].ToString();
                                NewRow[7] = dtRow.ItemArray[7].ToString();
                                NewRow[8] = dtRow.ItemArray[8].ToString();
                                NewRow[9] = dtRow.ItemArray[9].ToString();
                                NewRow[10] = dtRow.ItemArray[10].ToString();
                                NewRow[11] = dtRow.ItemArray[11].ToString();
                                NewRow[12] = dtRow.ItemArray[12].ToString();
                                NewRow[13] = dtRow.ItemArray[13].ToString();
                                NewRow[14] = dtRow.ItemArray[14].ToString();
                                NewRow[15] = dtRow.ItemArray[15].ToString();
                                NewRow[16] = dtRow.ItemArray[16].ToString();
                                NewRow[17] = dtRow.ItemArray[17].ToString();
                                NewRow[18] = dtRow.ItemArray[18].ToString();
                                NewRow[19] = dtRow.ItemArray[19].ToString();
                                NewRow[20] = dtRow.ItemArray[20].ToString();
                                NewRow[21] = dtRow.ItemArray[21].ToString();
                                NewRow[22] = dtRow.ItemArray[22].ToString();
                                NewRow[23] = dtRow.ItemArray[23].ToString();
                                NewRow[24] = dtRow.ItemArray[24].ToString();
                                NewRow[25] = dtRow.ItemArray[25].ToString();
                                NewRow[26] = dtRow.ItemArray[26].ToString();
                                NewRow[27] = dtRow.ItemArray[27].ToString();
                                NewRow[28] = dtRow.ItemArray[28].ToString();
                                NewRow[29] = dtRow.ItemArray[29].ToString();
                                NewRow[30] = dtRow.ItemArray[30].ToString();
                                NewRow[31] = dtRow.ItemArray[31].ToString();
                                NewRow[32] = dtRow.ItemArray[32].ToString();
                                dtTable.Rows.Add(NewRow);

                            }
                        }

                        var groupedData = dtTable.AsEnumerable()
                        .GroupBy(row => new
                        {
                            FYR = row.Field<string>("FYR"),
                            CARDCODE = row.Field<string>("CARDCODE")
                        })
                        .Select(group => new
                        {
                            FYR = group.Key.FYR,
                            CARDCODE = group.Key.CARDCODE
                        });

                        foreach (var group in groupedData)
                        {
                            //Master
                            string Fyr = group.FYR;
                            string CardCode = group.CARDCODE;
                            string CardName = "";
                            string LocCode = "";
                            string LocName = "";

                            SAPbobsCOM.Recordset oReEdit = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                            // Check if record exists
                            string checkQuery = string.Format("SELECT {0}DocEntry{0} from {0}@FIL_DD_DEALERTAR{0} Where {0}U_FYR{0}='" + Fyr + "' and {0}U_CARDCODE{0}='" + CardCode + "'", '"');
                            oReEdit.DoQuery(checkQuery);

                            if (oReEdit.RecordCount > 0)
                            {
                                int DocEntry = Convert.ToInt32(oReEdit.Fields.Item("DocEntry").Value.ToString());
                                if (DocEntry > 0)
                                {
                                    // Proceed with delete
                                    string deleteHeader = string.Format("DELETE from {0}@FIL_DD_DEALERTAR{0} Where {0}DocEntry{0}='" + DocEntry + "'", '"');
                                    oReEdit.DoQuery(deleteHeader);
                                    string deleteRow = string.Format("DELETE from {0}@FIL_DR_DEALERTAR{0} Where {0}DocEntry{0}='" + DocEntry + "'", '"');
                                    oReEdit.DoQuery(deleteRow);
                                    Global.objFun.ShowError("Error: Record Deleted Successfully.");
                                }
                            }

                            SAPbobsCOM.Recordset oRecordset = null;
                            oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            string sqlQuery = string.Format("SELECT A.{0}CardCode{0},A.{0}CardName{0},A.{0}U_LOCCODE{0}  from {0}OCRD{0} A Where A.{0}CardCode{0}='" + CardCode + "' ", '"');
                            oRecordset.DoQuery(sqlQuery);
                            if (oRecordset.RecordCount > 0)
                            {
                                CardName = oRecordset.Fields.Item("CardName").Value.ToString();
                                LocCode = oRecordset.Fields.Item("U_LOCCODE").Value.ToString();

                                SAPbobsCOM.Recordset oReLo = null;
                                oReLo = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                string sqlQuLo = string.Format("SELECT A.{0}Code{0},A.{0}Name{0}  from {0}@FIL_MD_LOCATION{0} A Where A.{0}Code{0}='" + LocCode + "' ", '"');
                                oReLo.DoQuery(sqlQuLo);
                                if(oReLo.RecordCount > 0)
                                {
                                    LocName = oReLo.Fields.Item("Name").Value.ToString();
                                }


                                //Get UDO Name By GeneralServic 
                                SAPbobsCOM.GeneralService oGeneralService;
                                //Get UDO master object data
                                SAPbobsCOM.GeneralData oGeneralData;

                                //Get UDO Child object data
                                SAPbobsCOM.GeneralData oChild;
                                //Get UDO Child collection object data
                                SAPbobsCOM.GeneralDataCollection oChildren;

                                SAPbobsCOM.GeneralDataParams oGeneralParams;

                                //SAPbobsCOM.CompanyService sCmp = null;
                                //sCmp =Program.oCompany.GetCompanyService();


                                SAPbobsCOM.CompanyService sCmp = null;
                                sCmp = Global.ocomp.GetCompanyService();

                                // SBO_Company.StartTransaction();

                                oGeneralService = sCmp.GetGeneralService("FIL_UDO_DD_DEALERTAR");

                                oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                                oGeneralData.SetProperty("U_FYR", Fyr);
                                oGeneralData.SetProperty("U_CARDCODE", CardCode);
                                oGeneralData.SetProperty("U_CARDNAME", CardName);
                                oGeneralData.SetProperty("U_LOCCODE", LocCode);
                                oGeneralData.SetProperty("U_LOCNAME", LocName);

                                //Details
                                var filteredRows = dtTable.AsEnumerable()
                                .Where(row => row.Field<string>("FYR") == Fyr
                                 && row.Field<string>("CARDCODE") == CardCode
                                  );

                                oChildren = oGeneralData.Child("FIL_DR_DEALERTAR");
                                foreach (var row in filteredRows)
                                {
                                    oChild = oChildren.Add();
                                    string ItmsGrpCod = row.Field<string>("ITMGRP").Trim();
                                    string ItmsGrpNam = "";
                                    oChild.SetProperty("U_ITMGRP", ItmsGrpCod);

                                    SAPbobsCOM.Recordset oRe = null;
                                    oRe = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                    string sqlQ = string.Format("SELECT A.{0}ItmsGrpCod{0},A.{0}ItmsGrpNam{0}  from {0}OITB{0} A Where A.{0}ItmsGrpCod{0}='" + ItmsGrpCod + "' ", '"');
                                    oRe.DoQuery(sqlQ);
                                    if (oRe.RecordCount > 0)
                                    {
                                        ItmsGrpNam = oRe.Fields.Item("ItmsGrpNam").Value.ToString();
                                    }

                                    oChild.SetProperty("U_ITMGRPNM", ItmsGrpNam);
                                    oChild.SetProperty("U_STARGET", row.Field<string>("STARGET"));
                                    oChild.SetProperty("U_CTARGET", row.Field<string>("CTARGET"));
                                    oChild.SetProperty("U_SJUL", row.Field<string>("SJUL"));
                                    oChild.SetProperty("U_CJUL", row.Field<string>("CJUL"));
                                    oChild.SetProperty("U_SAUG", row.Field<string>("SAUG"));
                                    oChild.SetProperty("U_CAUG", row.Field<string>("CAUG"));
                                    oChild.SetProperty("U_SSEP", row.Field<string>("SSEP"));
                                    oChild.SetProperty("U_CSEP", row.Field<string>("CSEP"));

                                    oChild.SetProperty("U_SOCT", row.Field<string>("SOCT"));
                                    oChild.SetProperty("U_COCT", row.Field<string>("COCT"));
                                    oChild.SetProperty("U_SNOV", row.Field<string>("SNOV"));
                                    oChild.SetProperty("U_CNOV", row.Field<string>("CNOV"));
                                    oChild.SetProperty("U_SDEC", row.Field<string>("SDEC"));
                                    oChild.SetProperty("U_CDEC", row.Field<string>("CDEC"));
                                    oChild.SetProperty("U_SJAN", row.Field<string>("SJAN"));
                                    oChild.SetProperty("U_CJAN", row.Field<string>("CJAN"));

                                    oChild.SetProperty("U_SFEB", row.Field<string>("SFEB"));
                                    oChild.SetProperty("U_CFEB", row.Field<string>("CFEB"));
                                    oChild.SetProperty("U_SMAR", row.Field<string>("SMAR"));
                                    oChild.SetProperty("U_CMAR", row.Field<string>("CMAR"));
                                    oChild.SetProperty("U_SAPR", row.Field<string>("SAPR"));
                                    oChild.SetProperty("U_CAPR", row.Field<string>("CAPR"));
                                    oChild.SetProperty("U_SMAY", row.Field<string>("SMAY"));
                                    oChild.SetProperty("U_CMAY", row.Field<string>("CMAY"));

                                    oChild.SetProperty("U_SJUN", row.Field<string>("SJUN"));
                                    oChild.SetProperty("U_CJUN", row.Field<string>("CJUN"));

                                }

                                oGeneralParams = oGeneralService.Add(oGeneralData);

                                String sDocEntry = "";
                                sDocEntry = oGeneralParams.GetProperty("DocEntry").ToString();
                                if (sDocEntry != "")
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralData);
                                    Global.objFun.ShowSuccess("Save successfully.");
                                }
                                else
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralData);
                                    Global.objFun.ShowError("Error:" + Global.ocomp.GetLastErrorDescription());
                                }
                            }
                            else
                            {
                                Global.objFun.ShowError("Error: Please Entry Proper Location");
                            }
                        }
                    }
                }
                else
                {
                    Global.objFun.ShowError("Error: Please Select Specefic Excel File with Data.");
                }

            }
            catch (Exception)
            {
                Global.objFun.ShowError("Error:" + Global.ocomp.GetLastErrorDescription());
            }


        }

        private void Form_DataLoadAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            try
            {
                SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
                string val = oform.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR").GetValue("DocEntry", 0);
                int DocEntry = string.IsNullOrEmpty(val) ? 0 : Convert.ToInt32(val);
                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)oform.Items.Item("mtxDetls").Specific;

                string strqry = string.Format("SELECT A.{0}ItmsGrpCod{0},A.{0}ItmsGrpNam{0} FROM {0}OITB{0} A left outer join {0}@FIL_DR_DEALERTAR{0} B on B.{0}U_ITMGRP{0} = A.{0}ItmsGrpCod{0} and B.{0}DocEntry{0} = '" + DocEntry + "' WHERE A.{0}U_TARGET{0}='Y' and ifnull(B.{0}U_ITMGRP{0},'')= ''", '"');


                SAPbobsCOM.Recordset ors =
                    (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                ors.DoQuery(strqry);


                SAPbouiCOM.DBDataSource oDBL =
                    oform.DataSources.DBDataSources.Item("@FIL_DR_DEALERTAR");

                omat.FlushToDataSource();

                int existingRows = oDBL.Size;


                int i = 0;

                while (!ors.EoF)
                {
                    string grpCode = ors.Fields.Item("ItmsGrpCod").Value.ToString().Trim();

                    bool isDuplicate = false;

                    for (int j = 0; j < oDBL.Size; j++)
                    {
                        string existing = oDBL.GetValue("U_ITMGRP", j).Trim();

                        if (existing == grpCode)
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    if (!isDuplicate && !string.IsNullOrWhiteSpace(grpCode))
                    {
                        oDBL.InsertRecord(existingRows + i);

                        oDBL.SetValue("LineId", existingRows + i, (existingRows + i + 1).ToString());
                        oDBL.SetValue("U_ITMGRP", existingRows + i, grpCode);
                        oDBL.SetValue("U_ITMGRPNM", existingRows + i,
                            ors.Fields.Item("ItmsGrpNam").Value.ToString());

                        i++;
                    }

                    ors.MoveNext();
                }

                omat.LoadFromDataSource();
                omat.AutoResizeColumns();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
