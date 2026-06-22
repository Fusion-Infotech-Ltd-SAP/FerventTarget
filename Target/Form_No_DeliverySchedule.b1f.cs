using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.IO;
using System.Reflection;
using System.Xml;


namespace Target
{
    [FormAttribute("Target.Form_No_DeliverySchedule", "Form_No_DeliverySchedule.b1f")]
    class Form_No_DeliverySchedule : UserFormBase
    {

        private SAPbouiCOM.Button BTPSOT;
        //public Form_No_DeliverySchedule()
        //{
        //}

        private SAPbouiCOM.Matrix MTX01;
        private SAPbouiCOM.StaticText STLENDTIME, STLENDDATE, STLDSTTIME, STLDSTDATE, STSUPVNAME, STTRNSPNAM, STVEHOWNRS;
        private SAPbouiCOM.EditText ETLENDTIME, ETLENDDATE, ETLDSTTIME, ETLDSTDATE, ETSUPVNAME, ETTRNSPNAM, ETVEHOWNRS;

     
        public override void OnInitializeComponent()
        {
            this.BTPSOT = ((SAPbouiCOM.Button)(this.GetItem("BTPSOT").Specific));
            this.BTPSOT.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.BTPSOT_PressedAfter);
            this.BTPSOT.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.BTPSOT_PressedBefore);
            this.MTX01 = ((SAPbouiCOM.Matrix)(this.GetItem("MTX01").Specific));
            this.MTX01.KeyDownAfter += new SAPbouiCOM._IMatrixEvents_KeyDownAfterEventHandler(this.MTX01_KeyDownAfter);
            this.MTX01.ChooseFromListAfter += new SAPbouiCOM._IMatrixEvents_ChooseFromListAfterEventHandler(this.Matrix0_ChooseFromListAfter);
            this.STVEHOWNRS = ((SAPbouiCOM.StaticText)(this.GetItem("STOWNSHP").Specific));
            this.STTRNSPNAM = ((SAPbouiCOM.StaticText)(this.GetItem("STTNSNAME").Specific));
            this.STSUPVNAME = ((SAPbouiCOM.StaticText)(this.GetItem("STSUPNAME").Specific));
            this.STLDSTDATE = ((SAPbouiCOM.StaticText)(this.GetItem("STLDATE").Specific));
            this.STLDSTTIME = ((SAPbouiCOM.StaticText)(this.GetItem("STLTIME").Specific));
            this.STLENDDATE = ((SAPbouiCOM.StaticText)(this.GetItem("STLEDATE").Specific));
            this.STLENDTIME = ((SAPbouiCOM.StaticText)(this.GetItem("STLETIME").Specific));
            this.ETVEHOWNRS = ((SAPbouiCOM.EditText)(this.GetItem("ETOWNSHP").Specific));
            this.ETTRNSPNAM = ((SAPbouiCOM.EditText)(this.GetItem("ETTNSNAME").Specific));
            this.ETSUPVNAME = ((SAPbouiCOM.EditText)(this.GetItem("ETSUPNAME").Specific));
            this.ETLDSTDATE = ((SAPbouiCOM.EditText)(this.GetItem("ETLDATE").Specific));
            this.ETLDSTTIME = ((SAPbouiCOM.EditText)(this.GetItem("ETLTIME").Specific));
            this.ETLENDDATE = ((SAPbouiCOM.EditText)(this.GetItem("ETLEDATE").Specific));
            this.ETLENDTIME = ((SAPbouiCOM.EditText)(this.GetItem("ETLETIME").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("ETVEHICLNO").Specific));
            this.EditText0.LostFocusAfter += new SAPbouiCOM._IEditTextEvents_LostFocusAfterEventHandler(this.EditText0_LostFocusAfter);
            this.OnCustomInitialize();

        }
        public override void OnInitializeFormEvents()
        {
        }


        private void MTX01_KeyDownAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {

                if (pVal.ColUID == "CLWHSCODE" || pVal.ColUID == "CLDLVRYQTY" || pVal.ColUID == "CLSCHEDQTY" || pVal.ColUID  == "CLDELVDATE")
                {
                    MoveMatrixRow(pVal, "MTX01", pVal.ColUID);
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(
                    "You are already in the last row.",
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
            }

        }

        private void MoveMatrixRow(SAPbouiCOM.SBOItemEventArg pVal, string matrixID, string colUID)
        {
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);
            SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item(matrixID).Specific;

            int row = pVal.Row;

            // Down Arrow
            if (pVal.CharPressed == 40)
            {
                oMatrix.Columns.Item(colUID).Cells.Item(row + 1).Click();
            }

            // Up Arrow
            if (pVal.CharPressed == 38 && row > 1)
            {
                oMatrix.Columns.Item(colUID).Cells.Item(row - 1).Click();
            }
        }

        private void EditText0_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);

                SAPbouiCOM.EditText oEdit =  (SAPbouiCOM.EditText)oForm.Items.Item("ETVEHICLNO").Specific;

                if (!string.IsNullOrEmpty(oEdit.Value))
                {
                    oEdit.Value = oEdit.Value.ToUpper();
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(
                    "Error: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }

        }



        private void Matrix0_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                SAPbouiCOM.ISBOChooseFromListEventArg cflArg = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);
                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("MTX01").Specific;
                SAPbouiCOM.DBDataSource DBDataSourceLine = oForm.DataSources.DBDataSources.Item("@FIL_DR_DELRSCHD");


                SAPbouiCOM.DataTable oDataTable = cflArg.SelectedObjects;

                if (oDataTable.Rows.Count > 0)
                {
                    string WhsCode = oDataTable.GetValue("WhsCode", 0).ToString();
                    int row = pVal.Row;

                    SAPbouiCOM.EditText txtItemCode = (SAPbouiCOM.EditText)oMatrix.Columns.Item("CLITEMCODE").Cells.Item(row).Specific;
                    string itemCode = txtItemCode.Value.Trim();

                    oMatrix.SetCellWithoutValidation(row, "CLWHSCODE", WhsCode);

                    string sql = $@"
                                SELECT T0.""OnHand"", T0.""WhsCode""
                                FROM OITW T0
                                WHERE T0.""ItemCode"" = '{itemCode}'
                                  AND T0.""WhsCode"" = '{WhsCode}'";

                    SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    rs.DoQuery(sql);

                    if (!rs.EoF)
                    {
                        string onHand = rs.Fields.Item("OnHand").Value.ToString();
                        oMatrix.SetCellWithoutValidation(row, "CLREMARKS", onHand);
                    }
                    else
                    {
                        oMatrix.SetCellWithoutValidation(row, "CLREMARKS", "0");
                    }
                }

            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText("CFL Error: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }

        }

      

        internal static void ItemList(List<(string CardCode, string CardName, string SDocEntry, string SDocNum, string SObject, int SLineId,string DocEntry, string DocNum, int LineNum, string ItemCode, string Dscription,
                         string UnitMsr, double Quantity, double ScheduleQty, string ShipDate, double OnHand, string TaxCode, double Price, double PriceBefDi,
                          double DiscountRowLevel, double Discount, double LineTotal, string WhsCode, string ObjType, string CntctCode, string Name, int BPLId, string BPLName, string SlpCode, string SlpName, double LineWiseTax, double deliverdQty, double OpenDlvQty, string SiteDelvery, string Comments)> data)
        {
            //Form_No_DeliverySchedule UDoList = new Form_No_DeliverySchedule();
            //UDoList.Show();

 
            string formUID = "FIL_FRM_NO_DELRSCHD"; // Unique ID for the form
                                                    // Check if the form is already open
            if (IsFormOpen(formUID))
            {
                Global.G_UI_Application.Forms.Item(formUID).Select();
                Global.G_UI_Application.StatusBar.SetText("Form is already open.",
                    SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                return;
            }

            Form_No_DeliverySchedule UDoList = new Form_No_DeliverySchedule();
            UDoList.Show();


            SAPbobsCOM.Recordset rSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.ActiveForm;
            SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)oForm.Items.Item("MTX01").Specific;
            SAPbouiCOM.DBDataSource ODataSource = oForm.DataSources.DBDataSources.Item("@FIL_DR_DELRSCHD");

            SAPbouiCOM.Column oColumn = MTX01.Columns.Item("CLDLVRYQTY");
            oColumn.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;


            oForm.Freeze(true);


            try
            {

                string CntctCode = data[0].CntctCode;
                string CardCode = data[0].CardCode;
                string CardName = data[0].CardName;
                string Name = data[0].Name;
                string BPLId = data[0].BPLId.ToString();
                string BPLName = data[0].BPLName;
                string SlpCode = data[0].SlpCode;
                string SlpName = data[0].SlpName;
                string Discount = data[0].Discount.ToString();
                string SiteDelvery = data[0].SiteDelvery.ToString();
                string Comments = data[0].Comments.ToString();
                string SiteDelivery = data[0].SiteDelvery.ToString();

                string qsTr = $"select \"AliasName\" from \"OCRD\" where \"CardCode\" = '{CardCode}'";
                rSet.DoQuery(qsTr);
                ((SAPbouiCOM.EditText)oForm.Items.Item("ETBPCODE").Specific).Value = CardCode;
                ((SAPbouiCOM.EditText)oForm.Items.Item("ETNAME").Specific).Value = CardName;
                ((SAPbouiCOM.EditText)oForm.Items.Item("ETSHIPTO").Specific).Value = rSet.Fields.Item("AliasName").Value.ToString();


                SAPbouiCOM.EditText ETPOSTDATE = (SAPbouiCOM.EditText)oForm.Items.Item("ETPOSTDATE").Specific;
                ETPOSTDATE.Active = true;
                ETPOSTDATE.String = "W";

                SAPbouiCOM.EditText ETSCHDATE = (SAPbouiCOM.EditText)oForm.Items.Item("ETSCHDATE").Specific;
                ETSCHDATE.Active = true;
                ETSCHDATE.String = "W";

                SAPbouiCOM.EditText ETDOCDATE = (SAPbouiCOM.EditText)oForm.Items.Item("ETDOCDATE").Specific;
                ETDOCDATE.Active = true;
                ETDOCDATE.String = "W";


                SAPbouiCOM.ComboBox oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item("CBSHIPTYPE").Specific;
                while (oCombo.ValidValues.Count > 0)
                    oCombo.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
                oCombo.ValidValues.Add("", "");
                string qStr = "SELECT \"TrnspCode\",\"TrnspName\" FROM OSHP ORDER BY \"TrnspCode\"";
                rSet.DoQuery(qStr);
                while (!rSet.EoF)
                {
                    string code = rSet.Fields.Item("TrnspCode").Value.ToString();
                    string name = rSet.Fields.Item("TrnspName").Value.ToString();
                    oCombo.ValidValues.Add(code, name);
                    rSet.MoveNext();
                }
                oCombo.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

                SAPbouiCOM.ComboBox comboBox = (SAPbouiCOM.ComboBox)oForm.Items.Item("ETCNTCTPRS").Specific;
                SAPbouiCOM.ComboBox cbBranch = (SAPbouiCOM.ComboBox)oForm.Items.Item("CBBRANCH").Specific;
                SAPbouiCOM.ComboBox cbSlpCode = (SAPbouiCOM.ComboBox)oForm.Items.Item("ETSLPCODE").Specific;
                ((SAPbouiCOM.EditText)oForm.Items.Item("ETDISPRCNT").Specific).Value = Discount;
                ((SAPbouiCOM.EditText)oForm.Items.Item("ETSTEDLVRY").Specific).Value = SiteDelvery;



                for (int i = comboBox.ValidValues.Count - 1; i >= 0; i--)
                {
                    comboBox.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);
                }
                comboBox.ValidValues.Add(CntctCode, Name);
                comboBox.Select(CntctCode, SAPbouiCOM.BoSearchKey.psk_ByValue);


                for (int i = cbBranch.ValidValues.Count - 1; i >= 0; i--)
                {
                    cbBranch.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);
                }
                cbBranch.ValidValues.Add(BPLId, BPLName);
                cbBranch.Select(BPLId, SAPbouiCOM.BoSearchKey.psk_ByValue);


                for (int i = cbSlpCode.ValidValues.Count - 1; i >= 0; i--)
                {
                    cbSlpCode.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);
                }
                cbSlpCode.ValidValues.Add(SlpCode, SlpName);
                cbSlpCode.Select(SlpCode, SAPbouiCOM.BoSearchKey.psk_ByValue);

                ODataSource.Clear();

                double ScheduledQty = 0;
                double taxamount = 0;
                double lineTotal = 0;
                HashSet<string> uniqueDocNums = new HashSet<string>();

                for (int i = 0; i < data.Count; i++)
                {
                    ODataSource.InsertRecord(ODataSource.Size);
                    ODataSource.SetValue("LineId", ODataSource.Size - 1, (i + 1).ToString());
                    ODataSource.SetValue("U_ITEMCODE", ODataSource.Size - 1, data[i].ItemCode);
                    ODataSource.SetValue("U_ITEMNAME", ODataSource.Size - 1, data[i].Dscription);
                    ODataSource.SetValue("U_UOM", ODataSource.Size - 1, data[i].UnitMsr);
                    ODataSource.SetValue("U_SCHEDQTY", ODataSource.Size - 1, data[i].ScheduleQty.ToString());
                    ScheduledQty += data[i].OpenDlvQty;
                    ODataSource.SetValue("U_DLVRYQTY", ODataSource.Size - 1, data[i].OpenDlvQty.ToString());
                    ODataSource.SetValue("U_ORDERQTY", ODataSource.Size - 1, data[i].Quantity.ToString());
                    ODataSource.SetValue("U_ALDLYQTY", ODataSource.Size - 1, data[i].deliverdQty.ToString());
                    ODataSource.SetValue("U_OPDLYQTY", ODataSource.Size - 1, data[i].OpenDlvQty.ToString());
                    ODataSource.SetValue("U_DELVDATE", ODataSource.Size - 1, DateTime.Parse(data[i].ShipDate).ToString("yyyyMMdd"));
                    ODataSource.SetValue("U_INSTOCK", ODataSource.Size - 1, data[i].OnHand.ToString());
                    ODataSource.SetValue("U_TAXCODE", ODataSource.Size - 1, data[i].TaxCode.ToString());
                    ODataSource.SetValue("U_UNTPRICE", ODataSource.Size - 1, data[i].PriceBefDi.ToString());
                    ODataSource.SetValue("U_PRICEAFDI", ODataSource.Size - 1, data[i].Price.ToString());
                    ODataSource.SetValue("U_TAXAMNT", ODataSource.Size - 1, data[i].LineWiseTax.ToString());
                    taxamount += data[i].LineWiseTax;
                    ODataSource.SetValue("U_DISCPERC", ODataSource.Size - 1, data[i].DiscountRowLevel.ToString());
                    ODataSource.SetValue("U_LINETOTAL", ODataSource.Size - 1, data[i].LineTotal.ToString());
                    lineTotal += data[i].LineTotal;
                    ODataSource.SetValue("U_WHSCODE", ODataSource.Size - 1, data[i].WhsCode.ToString());
                    ODataSource.SetValue("U_BASENTRY", ODataSource.Size - 1, data[i].DocEntry.ToString());
                    ODataSource.SetValue("U_BASETYPE", ODataSource.Size - 1, data[i].ObjType.ToString());
                    ODataSource.SetValue("U_BASEREF", ODataSource.Size - 1, data[i].DocNum.ToString());
                    uniqueDocNums.Add(data[i].DocNum.ToString());
                    ODataSource.SetValue("U_BASELINE", ODataSource.Size - 1, data[i].LineNum.ToString());
                    ODataSource.SetValue("DocEntry", ODataSource.Size - 1, data[i].SDocEntry.ToString());
                    ODataSource.SetValue("VisOrder", ODataSource.Size - 1, data[i].SLineId.ToString());
                    ODataSource.SetValue("LogInst", ODataSource.Size - 1, data[i].SDocNum.ToString());
                    ODataSource.SetValue("Object", ODataSource.Size - 1, data[i].SObject.ToString());
                }

                string concatenatedBaseRef = $"{Comments}  Based on Sales Order {string.Join(",", uniqueDocNums)}";
                SAPbouiCOM.EditText totalScheduledQty = (SAPbouiCOM.EditText)oForm.Items.Item("ETTLQTY").Specific;
                SAPbouiCOM.EditText TaxAmount = (SAPbouiCOM.EditText)oForm.Items.Item("ETVATSUM").Specific;
                SAPbouiCOM.EditText LineTotal = (SAPbouiCOM.EditText)oForm.Items.Item("ETTBDISCNT").Specific;
                SAPbouiCOM.EditText DocTotalHeader = (SAPbouiCOM.EditText)oForm.Items.Item("ETDOCTOTAL").Specific;
                SAPbouiCOM.EditText DiscountHeader = (SAPbouiCOM.EditText)oForm.Items.Item("ETDISPRCNT").Specific;
                SAPbouiCOM.EditText TbDiscountHeader = (SAPbouiCOM.EditText)oForm.Items.Item("ETTBDISCNT").Specific;
                SAPbouiCOM.EditText Remarks = (SAPbouiCOM.EditText)oForm.Items.Item("ETREMARKS").Specific;

                Remarks.Value = concatenatedBaseRef;
                totalScheduledQty.Value = ScheduledQty.ToString();
                TaxAmount.Value = taxamount.ToString();
                LineTotal.Value = (lineTotal - taxamount).ToString();

                double lineTotalValue = double.Parse(LineTotal.Value);
                double taxAmountValue = double.Parse(TaxAmount.Value);
                double totalScheduledQtyValue = double.Parse(totalScheduledQty.Value);

                double discountValue = double.Parse(Discount) / 100 * lineTotalValue;
                double docTotal = (lineTotalValue - discountValue) + taxAmountValue;
                DocTotalHeader.Value = docTotal.ToString("F2");


                MTX01.LoadFromDataSource();


            }
            catch (Exception)
            {

            }
            finally
            {
                oForm.Freeze(false);
            }
        }




        public static bool IsFormOpen(string formUID)
        {
            try
            {
                foreach (SAPbouiCOM.Form form in Application.SBO_Application.Forms)
                {
                    if (form.UniqueID == formUID)
                    {
                        return true; // Form is already open (SAPbouiCOM.Form)Application.SBO_Application.Forms
                    }
                }
            }
            catch (Exception ex)
            {
                Global.G_UI_Application.StatusBar.SetText("Error checking form: " + ex.Message,
                   SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            return false; // Form is not open
        }
        private void BTPSOT_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);

            if (oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_VEHICLNO", 0) == "")
            {

                Global.objFun.ShowError("Please enter Vehicle No");
                oForm.ActiveItem = "ETVEHICLNO";
                BubbleEvent = false;
            }

            else if (oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_DRVRNAME", 0) == "")
            {

                Global.objFun.ShowError("Please enter Driver Name");
                oForm.ActiveItem = "ETDRVRNAME";
                BubbleEvent = false;
            }

            else if (oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_CELLPHNO", 0) == "")
            {
                BubbleEvent = false;
                Global.objFun.ShowError("Please enter Driver Contact No");
                oForm.ActiveItem = "ETCELLPHNO";
                BubbleEvent = false;
            }

            else if (oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_SHIPTYPE", 0) == "")
            {
                Global.objFun.ShowError("Please enter Driver Shipping Type");
                oForm.ActiveItem = "CBSHIPTYPE";
                BubbleEvent = false;
            }

            string lTime = ((SAPbouiCOM.EditText)oForm.Items.Item("ETLTIME").Specific).Value.Trim();
            string leTime = ((SAPbouiCOM.EditText)oForm.Items.Item("ETLETIME").Specific).Value.Trim();

            if (!string.IsNullOrEmpty(lTime))
            {
                if (lTime.Length != 4 ||
                    !int.TryParse(lTime, out int t) ||
                    int.Parse(lTime.Substring(0, 2)) > 23 ||
                    int.Parse(lTime.Substring(2, 2)) > 59)
                {
                    Application.SBO_Application.StatusBar.SetText(
                        "Invalid time format. Valid time fomat like 1243 or 0230 or 0708 or 2358.",
                        SAPbouiCOM.BoMessageTime.bmt_Short,
                        SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.ActiveItem = "ETLTIME";
                    BubbleEvent = false;
                }
            }

            if (!string.IsNullOrEmpty(leTime))
            {
                if (leTime.Length != 4 ||
                    !int.TryParse(leTime, out int t) ||
                    int.Parse(leTime.Substring(0, 2)) > 23 ||
                    int.Parse(leTime.Substring(2, 2)) > 59)
                {
                    Application.SBO_Application.StatusBar.SetText(
                        "Invalid time format. Valid time fomat like 1243 or 0230 or 0708 or 2358.",
                        SAPbouiCOM.BoMessageTime.bmt_Short,
                        SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    oForm.ActiveItem = "ETLETIME";
                    BubbleEvent = false;
                }
            }

        }

        private void OnCustomInitialize()
        {

        }


        private void BTPSOT_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);

                if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_VIEW_MODE)
                {
                    Application.SBO_Application.StatusBar.SetText("Please wait... Processing", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                    SAPbobsCOM.Documents oDelivery = (SAPbobsCOM.Documents)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDeliveryNotes);
                    SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)oForm.Items.Item("MTX01").Specific;
                    SAPbouiCOM.DBDataSource oDataSource = oForm.DataSources.DBDataSources.Item("@FIL_DR_DELRSCHD");
                    string bpcode = ((SAPbouiCOM.EditText)oForm.Items.Item("ETBPCODE").Specific).Value;
                    //  string Branch = ((SAPbouiCOM.EditText)oForm.Items.Item("CBBRANCH").Specific).Value;
                    string discount = ((SAPbouiCOM.EditText)oForm.Items.Item("ETDISPRCNT").Specific).Value;
                    //  string DocNum = ((SAPbouiCOM.EditText)oForm.Items.Item("ETDOCNUM").Specific).Value;
                    //  string remarks = ((SAPbouiCOM.EditText)oForm.Items.Item("ETREMARKS").Specific).Value;
                    string siteDlvry = ((SAPbouiCOM.EditText)oForm.Items.Item("ETSTEDLVRY").Specific).Value;
                    string TrackingNo = ((SAPbouiCOM.EditText)oForm.Items.Item("ETVEHICLNO").Specific).Value;
                    string DriverName = ((SAPbouiCOM.EditText)oForm.Items.Item("ETDRVRNAME").Specific).Value;
                    string CellPhnNo = ((SAPbouiCOM.EditText)oForm.Items.Item("ETCELLPHNO").Specific).Value;
                    string ShipType = ((SAPbouiCOM.ComboBox)oForm.Items.Item("CBSHIPTYPE").Specific).Value;
                    string ShipTo = ((SAPbouiCOM.EditText)oForm.Items.Item("ETSHIPTO").Specific).Value;


                    string VicleOwnerShip = ((SAPbouiCOM.EditText)oForm.Items.Item("ETOWNSHP").Specific).Value;
                    string TransportName = ((SAPbouiCOM.EditText)oForm.Items.Item("ETTNSNAME").Specific).Value;
                    string SupervisorName = ((SAPbouiCOM.EditText)oForm.Items.Item("ETSUPNAME").Specific).Value;
                    string StartDate = string.IsNullOrWhiteSpace(((SAPbouiCOM.EditText)oForm.Items.Item("ETLDATE").Specific).Value) ? null : DateTime.ParseExact(((SAPbouiCOM.EditText)oForm.Items.Item("ETLDATE").Specific).Value, "yyyyMMdd", null).ToString("yyyyMMdd");
                    //string StartDate = DateTime.ParseExact(((SAPbouiCOM.EditText)oForm.Items.Item("ETLDATE").Specific).Value, "yyyyMMdd", null).ToString("yyyyMMdd");
                    string StartTime = ((SAPbouiCOM.EditText)oForm.Items.Item("ETLTIME").Specific).Value;
                    //  string EndDate = DateTime.ParseExact(((SAPbouiCOM.EditText)oForm.Items.Item("ETLEDATE").Specific).Value, "yyyyMMdd", null).ToString("yyyyMMdd");
                    string EndDate = string.IsNullOrWhiteSpace(((SAPbouiCOM.EditText)oForm.Items.Item("ETLEDATE").Specific).Value) ? null : DateTime.ParseExact(((SAPbouiCOM.EditText)oForm.Items.Item("ETLEDATE").Specific).Value, "yyyyMMdd", null).ToString("yyyyMMdd");
                    string EndTime = ((SAPbouiCOM.EditText)oForm.Items.Item("ETLETIME").Specific).Value;




                    oDelivery.CardCode = bpcode;
                    oDelivery.BPL_IDAssignedToInvoice = 1;
                    oDelivery.TrackingNumber = TrackingNo;
                    oDelivery.UserFields.Fields.Item("U_DRIVERNAME").Value = DriverName;
                    oDelivery.TransportationCode = Convert.ToInt32(ShipType);
                    oDelivery.UserFields.Fields.Item("U_CELLPHNO").Value = CellPhnNo;
                    oDelivery.UserFields.Fields.Item("U_Site_Del").Value = siteDlvry;
                    oDelivery.UserFields.Fields.Item("U_VCLONRSHP").Value = VicleOwnerShip;
                    oDelivery.UserFields.Fields.Item("U_TNSNAME").Value = TransportName;
                    oDelivery.UserFields.Fields.Item("U_SUPNAME").Value = SupervisorName;
                    if (!string.IsNullOrWhiteSpace(StartDate)) oDelivery.UserFields.Fields.Item("U_LDATE").Value = new DateTime(Convert.ToInt32(StartDate.Substring(0, 4)), Convert.ToInt32(StartDate.Substring(4, 2)), Convert.ToInt32(StartDate.Substring(6, 2)));
                    //  oDelivery.UserFields.Fields.Item("U_LDATE").Value = new DateTime(Convert.ToInt32(StartDate.Substring(0, 4)), Convert.ToInt32(StartDate.Substring(4, 2)), Convert.ToInt32(StartDate.Substring(6, 2)));
                    oDelivery.UserFields.Fields.Item("U_LTIME").Value = StartTime;
                    if (!string.IsNullOrWhiteSpace(EndDate)) oDelivery.UserFields.Fields.Item("U_LEDATE").Value = new DateTime(Convert.ToInt32(EndDate.Substring(0, 4)), Convert.ToInt32(EndDate.Substring(4, 2)), Convert.ToInt32(EndDate.Substring(6, 2)));
                    // oDelivery.UserFields.Fields.Item("U_LEDATE").Value = new DateTime(Convert.ToInt32(EndDate.Substring(0, 4)), Convert.ToInt32(EndDate.Substring(4, 2)), Convert.ToInt32(EndDate.Substring(6, 2))); ;
                    oDelivery.UserFields.Fields.Item("U_LETIME").Value = EndTime;
                    oDelivery.Address2 = ShipTo;
                    oDelivery.DiscountPercent = Convert.ToDouble(discount);


                    for (int i = 1; i <= MTX01.RowCount; i++)
                    {

                        string itemCode = ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLITEMCODE").Cells.Item(i).Specific).Value;
                        string WhsCode = ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLWHSCODE").Cells.Item(i).Specific).Value;
                        //Location
                        SAPbobsCOM.Recordset rsLoc = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        rsLoc.DoQuery($@"SELECT ""Location"" FROM ""OWHS"" WHERE ""WhsCode"" = '{WhsCode}'");

                        double quantity = 0, AldlvQty = 0, DlvryQty = 0;
                        double.TryParse(((SAPbouiCOM.EditText)MTX01.Columns.Item("CLSCHEDQTY").Cells.Item(i).Specific).Value, out quantity);
                        double.TryParse(((SAPbouiCOM.EditText)MTX01.Columns.Item("CLALDLYQTY").Cells.Item(i).Specific).Value, out AldlvQty);
                        double.TryParse(((SAPbouiCOM.EditText)MTX01.Columns.Item("CLDLVRYQTY").Cells.Item(i).Specific).Value, out DlvryQty);

                        string unitPrice = ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLUNTPRICE").Cells.Item(i).Specific).Value;
                        string discountprcnt = ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLDISCPERC").Cells.Item(i).Specific).Value;

                        double TotalQty = quantity - AldlvQty;

                        if (TotalQty > 0 && DlvryQty > 0)
                        {
                            string BaseEntry = oDataSource.GetValue("U_BASENTRY", i - 1).ToString();
                            string baseType = oDataSource.GetValue("U_BASETYPE", i - 1).ToString();
                            string BaseLine = oDataSource.GetValue("U_BASELINE", i - 1).ToString();
                            string BaseRef = oDataSource.GetValue("U_BASEREF", i - 1).ToString();


                            string SchDocNum = oDataSource.GetValue("LogInst", i - 1).ToString();
                            string LineId = oDataSource.GetValue("VisOrder", i - 1).ToString();
                            string SchDocEntry = oDataSource.GetValue("DocEntry", i - 1).ToString();
                            string SchObject = oDataSource.GetValue("Object", i - 1).ToString();


                            oDelivery.Lines.ItemCode = itemCode;
                            oDelivery.Lines.Quantity = DlvryQty;
                            oDelivery.Lines.UnitPrice = Convert.ToDouble(unitPrice);
                            oDelivery.Lines.DiscountPercent = Convert.ToDouble(discountprcnt);
                            oDelivery.Lines.WarehouseCode = WhsCode;

                            oDelivery.Lines.LocationCode = Convert.ToInt32(rsLoc.Fields.Item("Location").Value);

                            oDelivery.Lines.BaseEntry = Convert.ToInt32(BaseEntry);
                            oDelivery.Lines.BaseType = Convert.ToInt32(baseType);
                            oDelivery.Lines.BaseLine = Convert.ToInt32(BaseLine);

                            oDelivery.Lines.UserFields.Fields.Item("U_BASENTRY").Value = SchDocEntry;
                            oDelivery.Lines.UserFields.Fields.Item("U_BASELINE").Value = LineId;
                            oDelivery.Lines.UserFields.Fields.Item("U_BASENUM").Value = SchDocNum;
                            oDelivery.Lines.UserFields.Fields.Item("U_BASETYPE").Value = SchObject;

                            oDelivery.Lines.Add();
                        }
                    }

                    if (oDelivery.Add() != 0)
                    {
                        Application.SBO_Application.StatusBar.SetText(Global.ocomp.GetLastErrorDescription());

                        Global.objFun.ShowError(Global.ocomp.GetLastErrorDescription());

                    }
                    else
                    {

                        int TransferEntry = int.Parse(Global.ocomp.GetNewObjectKey());
                        Global.objFun.ShowSuccess("Delivery Posted Successfully. ");
                        Global.G_UI_Application.ActivateMenuItem("1304");
                        oForm.Items.Item("2").Click();

                        Application.SBO_Application.OpenForm(SAPbouiCOM.BoFormObjectEnum.fo_DeliveryNotes, "", TransferEntry.ToString());

                    }
                }

                else
                {
                    Global.objFun.ShowError("Update the Document First");
                }

            }

            catch(Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText("Error : " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }

        }

        private SAPbouiCOM.EditText EditText0;
    }
}
