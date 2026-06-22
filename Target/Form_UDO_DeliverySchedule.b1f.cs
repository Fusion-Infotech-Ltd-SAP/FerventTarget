using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Target
{
    [FormAttribute("Target.Form_UDO_DeliverySchedule", "Form_UDO_DeliverySchedule.b1f")]
    class Form_UDO_DeliverySchedule : UserFormBase
    {

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.StaticText STBPCODE, STNAME, STCNTCTPRS, STBRANCH, STDOCNUM, STSTATUS, STPOSTDATE, STSCHDATE, STDOCDATE, STTBDISCNT, STDISPRCNT, STVATSUM, STDOCTOTAL, STTLQTY, STREMARKS, STSLPCODE, STVEHOWNRS, STTRNSPNAM, STSUPVNAME, STLDSTDATE, STLDSTTIME, STLENDDATE, STLENDTIME;
        private SAPbouiCOM.EditText ETBPCODE, ETNAME, ETDOCNUM, ETPOSTDATE, ETSCHDATE, ETDOCDATE, ETTBDISCNT, ETDISPRCNT, ETVATSUM, ETDOCTOTAL, ETTLQTY, ETREMARKS, ETDOCENTRY, ETVEHOWNRS, ETTRNSPNAM, ETSUPVNAME, ETLDSTDATE, ETLDSTTIME, ETLENDDATE, ETLENDTIME;

        private SAPbouiCOM.Matrix MTX01;
        private SAPbouiCOM.Button Add, Cancel, BTCOPY, CLSBTN, BTNRVRS;
        private SAPbouiCOM.ComboBox CBSTATUS, CBBRANCH, CBSERIES, ETCNTCTPRS, ETSLPCODE;
        private SAPbouiCOM.LinkedButton ETLNBTN;

        private SAPbouiCOM.BoFormMode _beforeActionMode;

        private bool isButton = false;

        public Form_UDO_DeliverySchedule()
        {
        }

        public override void OnInitializeComponent()
        {
            this.STBPCODE = ((SAPbouiCOM.StaticText)(this.GetItem("STBPCODE").Specific));
            this.STNAME = ((SAPbouiCOM.StaticText)(this.GetItem("STNAME").Specific));
            this.ETBPCODE = ((SAPbouiCOM.EditText)(this.GetItem("ETBPCODE").Specific));
            this.ETBPCODE.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.ETBPCODE_ChooseFromListAfter);
            this.ETNAME = ((SAPbouiCOM.EditText)(this.GetItem("ETNAME").Specific));
            this.STCNTCTPRS = ((SAPbouiCOM.StaticText)(this.GetItem("STCNTCTPRS").Specific));
            this.STBRANCH = ((SAPbouiCOM.StaticText)(this.GetItem("STBRANCH").Specific));
            this.STDOCNUM = ((SAPbouiCOM.StaticText)(this.GetItem("STDOCNUM").Specific));
            this.STSTATUS = ((SAPbouiCOM.StaticText)(this.GetItem("STSTATUS").Specific));
            this.ETDOCNUM = ((SAPbouiCOM.EditText)(this.GetItem("ETDOCNUM").Specific));
            this.STPOSTDATE = ((SAPbouiCOM.StaticText)(this.GetItem("STPOSTDATE").Specific));
            this.ETPOSTDATE = ((SAPbouiCOM.EditText)(this.GetItem("ETPOSTDATE").Specific));
            this.STSCHDATE = ((SAPbouiCOM.StaticText)(this.GetItem("STSCHDATE").Specific));
            this.ETSCHDATE = ((SAPbouiCOM.EditText)(this.GetItem("ETSCHDATE").Specific));
            this.STDOCDATE = ((SAPbouiCOM.StaticText)(this.GetItem("STDOCDATE").Specific));
            this.ETDOCDATE = ((SAPbouiCOM.EditText)(this.GetItem("ETDOCDATE").Specific));
            this.STTBDISCNT = ((SAPbouiCOM.StaticText)(this.GetItem("STTBDISCNT").Specific));
            this.ETTBDISCNT = ((SAPbouiCOM.EditText)(this.GetItem("ETTBDISCNT").Specific));
            this.STDISPRCNT = ((SAPbouiCOM.StaticText)(this.GetItem("STDISPRCNT").Specific));
            this.ETDISPRCNT = ((SAPbouiCOM.EditText)(this.GetItem("ETDISPRCNT").Specific));
            this.ETVATSUM = ((SAPbouiCOM.EditText)(this.GetItem("ETVATSUM").Specific));
            this.STVATSUM = ((SAPbouiCOM.StaticText)(this.GetItem("STVATSUM").Specific));
            this.STDOCTOTAL = ((SAPbouiCOM.StaticText)(this.GetItem("STDOCTOTAL").Specific));
            this.ETDOCTOTAL = ((SAPbouiCOM.EditText)(this.GetItem("ETDOCTOTAL").Specific));
            this.STTLQTY = ((SAPbouiCOM.StaticText)(this.GetItem("STTLQTY").Specific));
            this.ETTLQTY = ((SAPbouiCOM.EditText)(this.GetItem("ETTLQTY").Specific));
            this.STREMARKS = ((SAPbouiCOM.StaticText)(this.GetItem("STSTEDLVRY").Specific));
            this.ETREMARKS = ((SAPbouiCOM.EditText)(this.GetItem("ETSTEDLVRY").Specific));
            this.STSLPCODE = ((SAPbouiCOM.StaticText)(this.GetItem("STSLPCODE").Specific));
            this.Add = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Add.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Add_PressedAfter);
            this.Add.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Add_PressedBefore);
            this.Cancel = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.MTX01 = ((SAPbouiCOM.Matrix)(this.GetItem("MTX01").Specific));
            this.MTX01.KeyDownAfter += new SAPbouiCOM._IMatrixEvents_KeyDownAfterEventHandler(this.MTX01_KeyDownAfter);
            this.MTX01.ChooseFromListAfter += new SAPbouiCOM._IMatrixEvents_ChooseFromListAfterEventHandler(this.MTX01_ChooseFromListAfter);
            //              this.MTX01.KeyDownAfter += new SAPbouiCOM._IMatrixEvents_KeyDownAfterEventHandler(this.MTX01_KeyDownAfter);
            this.MTX01.LostFocusAfter += new SAPbouiCOM._IMatrixEvents_LostFocusAfterEventHandler(this.MTX01_LostFocusAfter);
            this.BTCOPY = ((SAPbouiCOM.Button)(this.GetItem("BTCOPY").Specific));
            this.BTCOPY.ChooseFromListAfter += new SAPbouiCOM._IButtonEvents_ChooseFromListAfterEventHandler(this.BTCOPY_ChooseFromListAfter);
            this.BTCOPY.ChooseFromListBefore += new SAPbouiCOM._IButtonEvents_ChooseFromListBeforeEventHandler(this.BTCOPY_ChooseFromListBefore);
            this.CBSTATUS = ((SAPbouiCOM.ComboBox)(this.GetItem("CBSTATUS").Specific));
            this.CBBRANCH = ((SAPbouiCOM.ComboBox)(this.GetItem("CBBRANCH").Specific));
            this.CBSERIES = ((SAPbouiCOM.ComboBox)(this.GetItem("CBSERIES").Specific));
            this.ETDOCENTRY = ((SAPbouiCOM.EditText)(this.GetItem("ETDOCENTRY").Specific));
            this.ETCNTCTPRS = ((SAPbouiCOM.ComboBox)(this.GetItem("ETCNTCTPRS").Specific));
            this.ETSLPCODE = ((SAPbouiCOM.ComboBox)(this.GetItem("ETSLPCODE").Specific));
            this.ETLNBTN = ((SAPbouiCOM.LinkedButton)(this.GetItem("ETLNBTN").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("STREMARKS").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("ETREMARKS").Specific));
            this.CLSBTN = ((SAPbouiCOM.Button)(this.GetItem("CLSBTN").Specific));
            this.CLSBTN.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.CLSBTN_PressedAfter);
            this.CLSBTN.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.CLSBTN_PressedBefore);
            this.BTNRVRS = ((SAPbouiCOM.Button)(this.GetItem("BTNRVRS").Specific));
            this.BTNRVRS.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.BTNRVRS_PressedBefore);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("BTPSOT").Specific));
            this.Button0.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button0_PressedBefore);
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("STVEHICLNO").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("ETVEHICLNO").Specific));
            this.EditText1.LostFocusAfter += new SAPbouiCOM._IEditTextEvents_LostFocusAfterEventHandler(this.EditText1_LostFocusAfter);
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("STDRVRNAME").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("ETDRVRNAME").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("STCELLPHNO").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("ETCELLPHNO").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("STSHIPTYPE").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("CBSHIPTYPE").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("STSHIPTO").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("ETSHIPTO").Specific));
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
            this.OnCustomInitialize();

        }


        public override void OnInitializeFormEvents()
        {
            this.RightClickBefore += new SAPbouiCOM.Framework.FormBase.RightClickBeforeHandler(this.Form_RightClickBefore);
            this.DataLoadAfter += new SAPbouiCOM.Framework.FormBase.DataLoadAfterHandler(this.Form_DataLoadAfter);
            this.DataAddAfter += new SAPbouiCOM.Framework.FormBase.DataAddAfterHandler(this.Form_DataAddAfter);
            this.LoadAfter += new LoadAfterHandler(this.Form_LoadAfter);

        }


        private void Form_RightClickBefore(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item("FIL_FRM_DH_DELRSCHD");
            BubbleEvent = true;
            oForm.EnableMenu("1284", false);
            oForm.EnableMenu("1285", false);
            oForm.EnableMenu("1286", false);
            oForm.EnableMenu("772", true);
            oForm.EnableMenu("784", true);
            // oForm.EnableMenu("1293", true);

        }

        private void MTX01_KeyDownAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {

                if (pVal.ColUID == "CLWHSCODE" || pVal.ColUID == "CLDLVRYQTY" || pVal.ColUID == "CLSCHEDQTY" || pVal.ColUID == "CLDELVDATE")
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

        private void EditText1_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);

                SAPbouiCOM.EditText oEdit = (SAPbouiCOM.EditText)oForm.Items.Item("ETVEHICLNO").Specific;

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



        private void Form_LoadAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);


        }

        private void Form_DataLoadAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);
            try
            {
                FormMode(oForm);
                oForm.Freeze(true);

                SAPbobsCOM.Recordset rSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbobsCOM.Recordset rSet1 = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbobsCOM.Recordset rSet2 = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbouiCOM.ComboBox oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item("CBBRANCH").Specific;
                SAPbouiCOM.ComboBox CntractPrsn = (SAPbouiCOM.ComboBox)oForm.Items.Item("ETCNTCTPRS").Specific;
                SAPbouiCOM.ComboBox CbSalesPrsn = (SAPbouiCOM.ComboBox)oForm.Items.Item("ETSLPCODE").Specific;
                string brancId = oCombo.Value;
                string CntctCode = CntractPrsn.Value;
                string SlpCode = CbSalesPrsn.Value;


                if (!string.IsNullOrEmpty(brancId))
                {
                    string qStr = $"SELECT \"BPLId\",\"BPLName\" FROM OBPL WHERE \"BPLId\" = {brancId}";
                    rSet.DoQuery(qStr);

                    for (int i = oCombo.ValidValues.Count - 1; i >= 0; i--)
                    {
                        oCombo.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);
                    }

                    while (!rSet.EoF)
                    {
                        string id = rSet.Fields.Item("BPLId").Value.ToString();
                        string name = rSet.Fields.Item("BPLName").Value.ToString();

                        oCombo.ValidValues.Add(id, name);
                        oCombo.Select(id, SAPbouiCOM.BoSearchKey.psk_ByValue);

                        rSet.MoveNext();
                    }
                }

                if (!string.IsNullOrEmpty(CntctCode))
                {
                    string qStr1 = $"SELECT \"CntctCode\",\"Name\" FROM OCPR where \"CntctCode\" = {CntctCode}";
                    rSet1.DoQuery(qStr1);

                    for (int i = CntractPrsn.ValidValues.Count - 1; i >= 0; i--)
                    {
                        CntractPrsn.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);
                    }

                    while (!rSet1.EoF)
                    {
                        string id = rSet1.Fields.Item("CntctCode").Value.ToString();
                        string name = rSet1.Fields.Item("Name").Value.ToString();

                        CntractPrsn.ValidValues.Add(id, name);
                        CntractPrsn.Select(id, SAPbouiCOM.BoSearchKey.psk_ByValue);

                        rSet1.MoveNext();
                    }

                }

                if (!string.IsNullOrEmpty(SlpCode))
                {
                    string qStr2 = $"SELECT \"SlpCode\",\"SlpName\" FROM OSLP where \"SlpCode\" = {SlpCode}";
                    rSet2.DoQuery(qStr2);

                    for (int i = CbSalesPrsn.ValidValues.Count - 1; i >= 0; i--)
                    {
                        CbSalesPrsn.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);
                    }

                    while (!rSet2.EoF)
                    {
                        string id = rSet2.Fields.Item("SlpCode").Value.ToString();
                        string name = rSet2.Fields.Item("SlpName").Value.ToString();

                        CbSalesPrsn.ValidValues.Add(id, name);
                        CbSalesPrsn.Select(id, SAPbouiCOM.BoSearchKey.psk_ByValue);

                        rSet2.MoveNext();
                    }
                }





                if (oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_APRVSTTS", 0).Trim().ToString() == "P")
                {
                    oForm.Title = "Delivery Schedule/Order [Pending]";
                }
                else if (oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_APRVSTTS", 0).Trim().ToString() == "A")
                {
                    oForm.Title = "Delivery Schedule/Order [Approved]";
                }
                else if (oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_APRVSTTS", 0).Trim().ToString() == "R" && oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("Canceled", 0).Trim().ToString() == "N")
                {
                    oForm.Title = "Delivery Schedule/Order [Rejected]";
                }
                else
                {
                    oForm.Title = "Delivery Schedule/Order";
                }

                if (oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("Canceled", 0).Trim().ToString() == "Y")
                {
                    oForm.Title = "Delivery Schedule/Order [Canceled]";
                }

                SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)oForm.Items.Item("MTX01").Specific;

                double totalScheduledQty = 0;

                for (int i = 0; i < MTX01.RowCount; i++)
                {
                    SAPbouiCOM.EditText rowScheduledQtyCell = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLSCHEDQTY").Cells.Item(i + 1).Specific;


                    if (!string.IsNullOrEmpty(rowScheduledQtyCell.Value))
                    {
                        double rowScheduledQty = 0;
                        if (double.TryParse(rowScheduledQtyCell.Value, out rowScheduledQty))
                        {
                            totalScheduledQty += rowScheduledQty;
                        }
                    }

                }

                SAPbouiCOM.EditText scheduledQtyHeader = (SAPbouiCOM.EditText)oForm.Items.Item("ETTLQTY").Specific;
                scheduledQtyHeader.Value = totalScheduledQty.ToString();


                //  SAPbobsCOM.Recordset rSet3 = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string userName = Application.SBO_Application.Company.UserName.ToString();
                string qstr = $"select \"Fax\" As \"IsAllow\" from \"OUSR\" where \"USER_CODE\" = '" + userName + "'";
                rSet.DoQuery(qstr);

                string IsAllow = rSet.Fields.Item("IsAllow").Value.ToString();
                //  SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)oForm.Items.Item("MTX01").Specific;

                string isAllow = (rSet.Fields.Item("IsAllow").Value ?? "").ToString().Trim().ToUpperInvariant();

                if (isAllow == "YES")
                {
                    SAPbouiCOM.Button AddBtn = (SAPbouiCOM.Button)oForm.Items.Item("1").Specific;
                    AddBtn.Item.Enabled = false;
                    oForm.Items.Item("CLSBTN").Enabled = false;
                    MTX01.Columns.Item("CLSCHEDQTY").Editable = false;
                    MTX01.Columns.Item("CLDLVRYQTY").Editable = true;
                }

                else if (isAllow == "NO")
                {
                    oForm.Items.Item("BTPSOT").Enabled = false;
                    MTX01.Columns.Item("CLSCHEDQTY").Editable = true;
                    MTX01.Columns.Item("CLDLVRYQTY").Editable = false;
                }
                else
                {
                    oForm.Items.Item("BTPSOT").Enabled = false;
                    oForm.Items.Item("1").Enabled = false;
                    oForm.Items.Item("CLSBTN").Enabled = false;
                    MTX01.Columns.Item("CLSCHEDQTY").Editable = false;
                    MTX01.Columns.Item("CLDLVRYQTY").Editable = false;

                }
                oForm.Freeze(false);
            }

            catch (Exception ex)
            {
                oForm.Freeze(false);
            }

        }


        private void FormMode(SAPbouiCOM.Form pForm)
        {
            try
            {
                pForm.Items.Item("ETDOCNUM").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                pForm.Items.Item("CBSTATUS").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                pForm.Items.Item("ETBPCODE").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                pForm.Items.Item("MTX01").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                //  pForm.Items.Item("MTX01").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Add, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
                pForm.Items.Item("ETPOSTDATE").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False);



                if (pForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("Status", 0).Trim() == "O")
                {
                    pForm.Items.Item("ETSCHDATE").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
                    pForm.Items.Item("ETDOCDATE").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
                    pForm.Items.Item("MTX01").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
                }



                if (pForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("Status", 0).Trim() == "C"
                    || pForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_APRVSTTS", 0).Trim() == "P"
                     || pForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_APRVSTTS", 0).Trim() == "R"
                     || pForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("Status", 0).Trim() == "C"
                     || pForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("Canceled", 0).Trim() == "Y" || pForm.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                {
                    pForm.Items.Item("CLSBTN").Enabled = false;
                    pForm.Items.Item("BTCOPY").Enabled = false;
                    pForm.Items.Item("BTPSOT").Enabled = false;
                }


                if (pForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("Status", 0).Trim() == "O")
                {
                    pForm.Items.Item("BTCOPY").Enabled = false;

                }

                if (pForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_APRVSTTS", 0).Trim() == "R")
                {
                    pForm.Items.Item("MTX01").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
                }


            }
            catch (Exception exception1)
            {

                Application.SBO_Application.StatusBar.SetText("DeleteRow  Method Failed:" + exception1.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

            }
        }

        private void MTX01_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
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

        private void ETBPCODE_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
                SAPbouiCOM.ISBOChooseFromListEventArg cflEvent = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                string Uid = cflEvent.ChooseFromListUID;  // cfl unique id

                SAPbouiCOM.DataTable dtbCFL = cflEvent.SelectedObjects;

                if (!dtbCFL.IsEmpty)
                {

                    ((SAPbouiCOM.EditText)oform.Items.Item("ETBPCODE").Specific).Value = dtbCFL.GetValue("CardCode", 0).ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("ETNAME").Specific).Value = dtbCFL.GetValue("CardName", 0).ToString();
                    ((SAPbouiCOM.EditText)oform.Items.Item("ETSHIPTO").Specific).Value = dtbCFL.GetValue("AliasName", 0).ToString();
                }
            }
            catch (Exception)
            {

            }

        }


        private void BTCOPY_ChooseFromListBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);
            oForm.Freeze(true);

            // Get the ChooseFromList object for the button clicked
            SAPbouiCOM.ChooseFromList oCfl = oForm.ChooseFromLists.Item(((SAPbouiCOM.Button)oForm.Items.Item(pVal.ItemUID).Specific).ChooseFromListUID);

            // Create conditions for filtering the CFL
            SAPbouiCOM.Conditions oCons = new SAPbouiCOM.Conditions();
            SAPbouiCOM.Condition oCon;

            // Add condition for CardCode
            oCon = oCons.Add();
            oCon.Alias = "CardCode";
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
            oCon.CondVal = oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_CARDCODE", 0);

            // Add condition for DocStatus (must be "O" for Open)
            oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
            oCon = oCons.Add();
            oCon.Alias = "DocStatus";
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
            oCon.CondVal = "O";


            oCfl.SetConditions(oCons);


            oForm.Freeze(false);
        }



        private void BTCOPY_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
                SAPbouiCOM.ISBOChooseFromListEventArg cflEvent = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                string Uid = cflEvent.ChooseFromListUID;

                SAPbouiCOM.DataTable oDataTable = cflEvent.SelectedObjects;

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

                    Form_No_SOList.LoadSOList(ref cForm, strDocEntry);
                }
            }
            catch (Exception) { }

        }


        internal static void ItemList(List<(string CradCode, string CradName, string SDocEntry, string SDocNum, string SObject, int SLineId, string DocEntry, string DocNum, int LineNum, string ItemCode, string Dscription,
                          string UnitMsr, double Quantity, double ScheduleQty, string ShipDate, double OnHand, string TaxCode, double Price, double PriceBefDi,
                           double DiscountRowLevel, double Discount, double LineTotal, string WhsCode, string ObjType, string CntctCode, string Name, int BPLId, string BPLName, string SlpCode, string SlpName, double LineWiseTax, double deliverdQty, double OpenDlvQty, string SiteDelvery, string Comments)> data)
        {
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item("FIL_FRM_DH_DELRSCHD");
            SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)oForm.Items.Item("MTX01").Specific;
            SAPbouiCOM.DBDataSource ODataSource = oForm.DataSources.DBDataSources.Item("@FIL_DR_DELRSCHD");
            SAPbouiCOM.Column oColumn = MTX01.Columns.Item("CLDLVRYQTY");
            oColumn.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;

            oForm.Freeze(true);


            try
            {

                string CntctCode = data[0].CntctCode;
                string Name = data[0].Name;
                string BPLId = data[0].BPLId.ToString();
                string BPLName = data[0].BPLName;
                string SlpCode = data[0].SlpCode;
                string SlpName = data[0].SlpName;
                string Discount = data[0].Discount.ToString();
                string SiteDelvery = data[0].SiteDelvery.ToString();
                string Comments = data[0].Comments.ToString();
                SAPbouiCOM.ComboBox comboBox = (SAPbouiCOM.ComboBox)oForm.Items.Item("ETCNTCTPRS").Specific;
                SAPbouiCOM.ComboBox cbBranch = (SAPbouiCOM.ComboBox)oForm.Items.Item("CBBRANCH").Specific;
                SAPbouiCOM.ComboBox cbSlpCode = (SAPbouiCOM.ComboBox)oForm.Items.Item("ETSLPCODE").Specific;
                SAPbouiCOM.EditText etDiscount = (SAPbouiCOM.EditText)oForm.Items.Item("ETDISPRCNT").Specific;
                etDiscount.Value = Discount;
                SAPbouiCOM.EditText EtSteDlvry = (SAPbouiCOM.EditText)oForm.Items.Item("ETSTEDLVRY").Specific;
                EtSteDlvry.Value = SiteDelvery;



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
                    ScheduledQty += data[i].ScheduleQty;
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



        private void MTX01_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);


            if (pVal.ColUID == "CLSCHEDQTY")
            {
                try
                {
                    //  oForm.Freeze(true);
                    SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)oForm.Items.Item("MTX01").Specific;

                    int rowIndex = pVal.Row;
                    SAPbobsCOM.Recordset rSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    SAPbouiCOM.EditText scheduledQtyCell = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLSCHEDQTY").Cells.Item(rowIndex).Specific;
                    SAPbouiCOM.EditText OrderQuantity = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLORDERQTY").Cells.Item(rowIndex).Specific;
                    SAPbouiCOM.EditText PriceAfDi = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLPRICEADI").Cells.Item(rowIndex).Specific;
                    SAPbouiCOM.EditText TaxAmount = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLTAXAMNT").Cells.Item(rowIndex).Specific;
                    SAPbouiCOM.EditText LineTotal = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLLINETOTL").Cells.Item(rowIndex).Specific;
                    SAPbouiCOM.EditText OpnforDlvry = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLOPDLYQTY").Cells.Item(rowIndex).Specific;
                    SAPbouiCOM.EditText DeliveryQty = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLDLVRYQTY").Cells.Item(rowIndex).Specific;

                    SAPbouiCOM.EditText BaseEntry = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLBASENTRY").Cells.Item(rowIndex).Specific;
                    SAPbouiCOM.EditText LineNum = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLBASELINE").Cells.Item(rowIndex).Specific;
                    SAPbouiCOM.EditText ItemCode = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLITEMCODE").Cells.Item(rowIndex).Specific;

                    double scheduledQtyValue = double.Parse(scheduledQtyCell.Value);
                    double PriceafDisValue = double.Parse(PriceAfDi.Value);

                    string qStr = $"Select \"Quantity\", \"VatSum\" from \"RDR1\" Where \"DocEntry\" = {BaseEntry.Value} And \"ItemCode\" = '{ItemCode.Value}' And \"LineNum\" = {LineNum.Value}";
                    rSet.DoQuery(qStr);

                    double Quantity = double.Parse(rSet.Fields.Item("Quantity").Value.ToString());
                    double VatSum = double.Parse(rSet.Fields.Item("VatSum").Value.ToString());
                    double calculatedTaxAmount = (VatSum / Quantity) * scheduledQtyValue;


                    DeliveryQty.Value = scheduledQtyValue.ToString();
                    OpnforDlvry.Value = scheduledQtyValue.ToString();
                    TaxAmount.Value = calculatedTaxAmount.ToString();



                    LineTotal.Value = ((scheduledQtyValue * PriceafDisValue) + calculatedTaxAmount).ToString();
                    double totalScheduledQty = 0;
                    double totalLineTotalMinusTax = 0;
                    double totalTaxAmount = 0;
                    oForm.Freeze(true);
                    for (int i = 0; i < MTX01.RowCount; i++)
                    {
                        SAPbouiCOM.EditText rowScheduledQtyCell = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLSCHEDQTY").Cells.Item(i + 1).Specific;
                        SAPbouiCOM.EditText rowTaxCell = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLTAXAMNT").Cells.Item(i + 1).Specific;
                        SAPbouiCOM.EditText rowLineCell = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLLINETOTL").Cells.Item(i + 1).Specific;
                        SAPbouiCOM.EditText OpenDelivery = (SAPbouiCOM.EditText)MTX01.Columns.Item("CLOPDLYQTY").Cells.Item(i + 1).Specific;

                        double quantity = Convert.ToDouble(((SAPbouiCOM.EditText)MTX01.Columns.Item("CLSCHEDQTY").Cells.Item(i + 1).Specific).Value);
                        double AldlvQty = Convert.ToDouble(((SAPbouiCOM.EditText)MTX01.Columns.Item("CLALDLYQTY").Cells.Item(i + 1).Specific).Value);


                        if (!string.IsNullOrEmpty(rowScheduledQtyCell.Value))
                        {
                            double rowScheduledQty = 0;
                            if (double.TryParse(rowScheduledQtyCell.Value, out rowScheduledQty))
                            {
                                totalScheduledQty += rowScheduledQty;
                            }
                        }


                        if (!string.IsNullOrEmpty(rowLineCell.Value) && !string.IsNullOrEmpty(rowTaxCell.Value))
                        {
                            double rowLineTotal = 0;
                            double rowTaxAmount = 0;

                            if (double.TryParse(rowLineCell.Value, out rowLineTotal) && double.TryParse(rowTaxCell.Value, out rowTaxAmount))
                            {
                                totalLineTotalMinusTax += (rowLineTotal - rowTaxAmount);
                                totalTaxAmount += rowTaxAmount;
                            }
                        }

                        //  double TotalOpnDlvy = quantity - AldlvQty;
                        //   OpenDelivery.Value = TotalOpnDlvy.ToString();




                    }

                    SAPbouiCOM.EditText scheduledQtyHeader = (SAPbouiCOM.EditText)oForm.Items.Item("ETTLQTY").Specific;
                    SAPbouiCOM.EditText TbDiscountHeader = (SAPbouiCOM.EditText)oForm.Items.Item("ETTBDISCNT").Specific;
                    SAPbouiCOM.EditText TaxAmounttHeader = (SAPbouiCOM.EditText)oForm.Items.Item("ETVATSUM").Specific;
                    SAPbouiCOM.EditText DiscountHeader = (SAPbouiCOM.EditText)oForm.Items.Item("ETDISPRCNT").Specific;
                    SAPbouiCOM.EditText DocTotalHeader = (SAPbouiCOM.EditText)oForm.Items.Item("ETDOCTOTAL").Specific;

                    scheduledQtyHeader.Value = totalScheduledQty.ToString();
                    TbDiscountHeader.Value = totalLineTotalMinusTax.ToString();
                    TaxAmounttHeader.Value = totalTaxAmount.ToString();

                    double discountValue = double.Parse(DiscountHeader.Value) / 100;
                    double tbDiscountValue = double.Parse(TbDiscountHeader.Value);
                    double taxAmountValue = double.Parse(TaxAmounttHeader.Value);

                    DocTotalHeader.Value = ((tbDiscountValue - (tbDiscountValue * discountValue)) + taxAmountValue).ToString("F2");


                    MTX01.FlushToDataSource();
                    MTX01.LoadFromDataSource();
                    oForm.Freeze(false);

                }

                catch (Exception)
                {
                    oForm.Freeze(false);
                }
            }


        }


        private void Add_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Form oForms = Application.SBO_Application.Forms.Item(pVal.FormUID);
            _beforeActionMode = oForms.Mode;
            SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)oForms.Items.Item("MTX01").Specific;

            try
            {
                oForms.Freeze(true);

                for (int row = 1; row <= MTX01.RowCount; row++)
                {
                    MTX01.Columns.Item("CLSCHEDQTY").Cells.Item(row).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    Application.SBO_Application.SendKeys("{TAB}");
                }
            }
            finally
            {
                oForms.Freeze(false);
            }

            if (oForms.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || oForms.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
            {
                try
                {
                    if (oForms.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                    {
                        oForms.Freeze(true);
                        ApporvalValidationCheck(oForms, ref BubbleEvent);
                        oForms.Freeze(false);
                    }

                    if (oForms.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE && isButton == false)
                    {
                        oForms.Freeze(true);
                        try
                        {
                            SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForms.Items.Item("MTX01").Specific;
                            SAPbouiCOM.DBDataSource oDS = oForms.DataSources.DBDataSources.Item("@FIL_DR_DELRSCHD");

                            string docEntry = ((SAPbouiCOM.EditText)oForms.Items.Item("ETDOCENTRY").Specific).Value.Trim();
                            if (string.IsNullOrEmpty(docEntry)) return;

                            SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                            bool isMismatch = false;

                            for (int i = 1; i <= oMatrix.RowCount; i++)
                            {
                                string mQtyStr = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("CLSCHEDQTY").Cells.Item(i).Specific).Value.Trim();
                                string lineId = oDS.GetValue("LineId", i - 1).Trim();
                                if (string.IsNullOrEmpty(lineId))
                                    continue;

                                string qsTr = $@"
                                            SELECT IFNULL(""U_SCHEDQTY"",0) AS ""U_SCHEDQTY""
                                            FROM ""@FIL_DR_DELRSCHD""
                                            WHERE ""DocEntry"" = {docEntry}
                                              AND ""LineId""  = {lineId}";
                                rs.DoQuery(qsTr);

                                string dQtyStr = "0";
                                if (!rs.EoF)
                                {
                                    dQtyStr = rs.Fields.Item("U_SCHEDQTY").Value.ToString().Trim();
                                }

                                double mQty = 0, dQty = 0;
                                double.TryParse(mQtyStr, out mQty);
                                double.TryParse(dQtyStr, out dQty);

                                if (mQty != dQty)
                                {
                                    isMismatch = true;
                                    break;
                                }
                            }

                            if (isMismatch)
                            {
                                ApporvalValidationCheck(oForms, ref BubbleEvent);
                            }
                        }
                        catch (Exception ex)
                        {
                            oForms.Freeze(false);
                        }
                        finally
                        {
                            oForms.Freeze(false);
                        }
                    }

                }
                catch (Exception ex)
                {

                }

            }


        }
        private void Add_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Form oForms = Application.SBO_Application.Forms.Item(pVal.FormUID);
            if (pVal.ActionSuccess == true && _beforeActionMode != SAPbouiCOM.BoFormMode.fm_FIND_MODE)
            {
                ApporvalData(oForms);
            }
            else
            {
                oForms.Freeze(false);

            }
        }

        private static void ApporvalValidationCheck(SAPbouiCOM.Form oForms, ref bool BubbleEvent)
        {
            SAPbobsCOM.Recordset rSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string Object = "FIL_D_DELRSCHD";
            string UserId = Global.G_UI_Application.Company.UserName;

            string QsTr =
                    $"SELECT distinct  T9.\"Fax\", CAST(T4.\"QString\" AS NVARCHAR(5000)) AS \"QString\" " +
                    "FROM \"@FIL_MH_APPTMPLT\" T0 " +
                    "INNER JOIN \"@FIL_MR_APPTMPLO\" T1 ON T1.\"Code\" = T0.\"Code\" " +
                    "INNER JOIN \"@FIL_MR_APPTMPLD\" T2 ON T2.\"Code\" = T0.\"Code\" " +
                    "INNER JOIN \"@FIL_MR_APPTMPLQ\" T3 ON T3.\"Code\" = T0.\"Code\" " +
                    "INNER JOIN \"@FIL_MR_APPTMPLS\" T5 ON T5.\"Code\" = T0.\"Code\" " +
                    "LEFT OUTER JOIN \"OUQR\" T4 ON T4.\"IntrnalKey\" = T3.\"U_QUERYID\" " +
                    "INNER JOIN \"OWST\" T7 ON T7.\"WstCode\" = T5.\"U_WSTCODE\" " +
                    "INNER JOIN \"WST1\" T6 ON T7.\"WstCode\" = T6.\"WstCode\" " +
                    "INNER JOIN \"OUSR\" T8 ON T8.\"USERID\" = T6.\"UserID\" " +
                     "INNER JOIN \"OUSR\" T9 ON T9.\"USERID\" = T1.\"U_USERID\" " +
                    $"WHERE T9.\"USER_CODE\" = '" + UserId + "' AND T2.\"U_OBJTYPE\" = '" + Object + "' AND T0.\"U_ACTIVE\" = 'Y'";


            rSet.DoQuery(QsTr);

            while (!rSet.EoF)
            {
                string originalQuery = rSet.Fields.Item("QString").Value.ToString();
                //string Code = rSet.Fields.Item("Code").Value.ToString();
                //string userId = rSet.Fields.Item("UserId").Value.ToString();
                //string Stage = rSet.Fields.Item("Stage").Value.ToString();
                //string user_Code = rSet.Fields.Item("USER_CODE").Value.ToString();
                string Fax = rSet.Fields.Item("Fax").Value.ToString().Trim().ToUpper();
                string resolvedQuery = ResolveQueryPlaceholders(oForms, originalQuery);

                SAPbobsCOM.Recordset tempSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                tempSet.DoQuery(resolvedQuery);

                if (tempSet.RecordCount > 0)
                {
                    string result = tempSet.Fields.Item(0).Value.ToString().Trim().ToUpper();

                    if (result == "TRUE" && Fax == "NO")
                    {

                        int ans = Application.SBO_Application.MessageBox("Do you want to send the document for approval?.", 1, "Yes", "No");

                        if (ans != 1)
                        {

                            Application.SBO_Application.StatusBar.SetText("Approval Process Cancel.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                            BubbleEvent = false;
                            break;
                        }
                    }
                }

                rSet.MoveNext();
            }
        }


        private static void ApporvalData(SAPbouiCOM.Form oForms)
        {

            try
            {
                SAPbobsCOM.Recordset rSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbobsCOM.Recordset rSet1 = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbobsCOM.Recordset rSetUpdate = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbouiCOM.EditText oEditText = (SAPbouiCOM.EditText)oForms.Items.Item("ETTLQTY").Specific;
                oEditText.Value = "";

                if (oForms.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || oForms.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                {
                    if (oForms.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                    {
                        string docEntry = Global.dlvDocEntry;

                        oForms.Freeze(true);
                        oForms.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE;
                        ((SAPbouiCOM.IEditText)oForms.Items.Item("ETDOCNUM").Specific).Value = docEntry;
                        oForms.Items.Item("1").Click();
                    }


                    string Object = oForms.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("Object", 0);
                    string UserId = oForms.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("UserSign", 0);
                    string bpcodes = ((SAPbouiCOM.EditText)oForms.Items.Item("ETBPCODE").Specific).Value;
                    string approveReqr = oForms.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_APPRRQRD", 0);
                    string approveStatus = oForms.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("U_APRVSTTS", 0);

                    if (Object != "" && UserId != "")
                    {
                        if (approveReqr == "" || approveStatus == "" || approveStatus == "R")
                        {
                            string QsTr =
                           $"SELECT  T0.\"Code\",T8.\"USER_CODE\", T4.\"QString\", T4.\"IntrnalKey\",T6.\"UserID\" AS \"UserId\",T2.\"U_OBJTYPE\" AS \"ObjType\" ,T5.\"U_WSTCODE\" AS \"Stage\", T7.\"MaxReqr\",T7.\"MaxRejReqr\" " +
                           "FROM \"@FIL_MH_APPTMPLT\" T0 " +
                           "INNER JOIN \"@FIL_MR_APPTMPLO\" T1 ON T1.\"Code\" = T0.\"Code\" " +
                           "INNER JOIN \"@FIL_MR_APPTMPLD\" T2 ON T2.\"Code\" = T0.\"Code\" " +
                           "INNER JOIN \"@FIL_MR_APPTMPLQ\" T3 ON T3.\"Code\" = T0.\"Code\" " +
                           "INNER JOIN \"@FIL_MR_APPTMPLS\" T5 ON T5.\"Code\" = T0.\"Code\" " +
                           "LEFT OUTER JOIN \"OUQR\" T4 ON T4.\"IntrnalKey\" = T3.\"U_QUERYID\" " +
                           "INNER JOIN \"OWST\" T7 ON T7.\"WstCode\" = T5.\"U_WSTCODE\" " +
                           "INNER JOIN \"WST1\" T6 ON T7.\"WstCode\" = T6.\"WstCode\" " +
                           "INNER JOIN \"OUSR\" T8 ON T8.\"USERID\" = T6.\"UserID\" " +
                           $"WHERE T1.\"U_USERID\" = '" + UserId + "' AND T2.\"U_OBJTYPE\" = '" + Object + "'  AND T0.\"U_ACTIVE\" = 'Y' ORDER BY T5.\"LineId\"";


                            rSet.DoQuery(QsTr);

                            bool matchFound = false;
                            string resultQuery = "";
                            string CodeValue = "";
                            string userIdValue = "";
                            string StageValue = "";
                            string userCode = "";


                            while (!rSet.EoF)
                            {
                                string originalQuery = rSet.Fields.Item("QString").Value.ToString();
                                string Code = rSet.Fields.Item("Code").Value.ToString();
                                string userId = rSet.Fields.Item("UserId").Value.ToString();
                                string Stage = rSet.Fields.Item("Stage").Value.ToString();
                                string user_Code = rSet.Fields.Item("USER_CODE").Value.ToString();
                                string resolvedQuery = ResolveQueryPlaceholders(oForms, originalQuery);

                                SAPbobsCOM.Recordset tempSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                tempSet.DoQuery(resolvedQuery);

                                if (tempSet.RecordCount > 0)
                                {
                                    string result = tempSet.Fields.Item(0).Value.ToString().Trim().ToUpper();

                                    if (result == "TRUE")
                                    {
                                        matchFound = true;
                                        resultQuery = resolvedQuery;
                                        CodeValue = Code;
                                        userIdValue = userId;
                                        userCode = user_Code;
                                        StageValue = Stage;
                                        break;

                                    }
                                }

                                rSet.MoveNext();
                            }

                            if (matchFound)
                            {
                                string ApprovalData = $@"
                                            SELECT
                                                X.""Code"",
                                                X.""USER_CODE"",
                                                Q.""QString"",
                                                Q.""IntrnalKey"",
                                                X.""UserId"",
                                                X.""ObjType"",
                                                X.""Stage"",
                                                X.""MaxReqr"",
                                                X.""MaxRejReqr""
                                            FROM (
                                                SELECT DISTINCT
                                                    T0.""Code"",
                                                    T8.""USER_CODE"",
                                                    T3.""U_QUERYID""     AS ""IntrnalKey"",
                                                    T6.""UserID""        AS ""UserId"",
                                                    T2.""U_OBJTYPE""     AS ""ObjType"",
                                                    T5.""U_WSTCODE""     AS ""Stage"",
                                                    T7.""MaxReqr"",
                                                    T7.""MaxRejReqr"",
                                                    T5.""LineId""
                                                FROM ""@FIL_MH_APPTMPLT"" T0
                                                INNER JOIN ""@FIL_MR_APPTMPLO"" T1 ON T1.""Code"" = T0.""Code""
                                                INNER JOIN ""@FIL_MR_APPTMPLD"" T2 ON T2.""Code"" = T0.""Code""
                                                INNER JOIN ""@FIL_MR_APPTMPLQ"" T3 ON T3.""Code"" = T0.""Code""
                                                INNER JOIN ""@FIL_MR_APPTMPLS"" T5 ON T5.""Code"" = T0.""Code""
                                                INNER JOIN ""OWST"" T7 ON T7.""WstCode"" = T5.""U_WSTCODE""
                                                INNER JOIN ""WST1"" T6 ON T7.""WstCode"" = T6.""WstCode""
                                                INNER JOIN ""OUSR"" T8 ON T8.""USERID"" = T6.""UserID""
                                                WHERE T0.""Code"" = '{CodeValue}'
                                                  AND T2.""U_OBJTYPE"" = '{Object}'
                                            ) X
                                            LEFT OUTER JOIN ""OUQR"" Q ON Q.""IntrnalKey"" = X.""IntrnalKey""
                                            ORDER BY X.""LineId""";

                                rSet1.DoQuery(ApprovalData);

                                string Code = CodeValue;
                                string Orginator = userIdValue;
                                string DocEntry = oForms.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("DocEntry", 0);
                                string DocNum = oForms.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("DocNum", 0);
                                string ObjType = oForms.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("Object", 0);
                                string Stage = StageValue;

                                //For Master Date Save
                                SAPbobsCOM.CompanyService oCompanyService = Global.ocomp.GetCompanyService();
                                SAPbobsCOM.GeneralService oGeneralService = oCompanyService.GetGeneralService("FIL_D_APPDECSN");
                                SAPbobsCOM.GeneralData oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                                // Fill data
                                oGeneralData.SetProperty("U_APPTCODE", Code);
                                oGeneralData.SetProperty("U_OWNERID", Orginator);
                                oGeneralData.SetProperty("U_DOCENTRY", DocEntry);
                                oGeneralData.SetProperty("U_OBJTYPE", ObjType);
                                oGeneralData.SetProperty("U_CURSTAGE", Stage);
                                oGeneralData.SetProperty("U_STATUS", "P");


                                string qStr1 = $"UPDATE \"@FIL_DH_DELRSCHD\" AS H " +
                                                  $"SET " +
                                                  $"H.\"U_APPRRQRD\" = 'Y',H.\"U_APRVSTTS\" = 'P'  " +
                                                  $"WHERE H.\"DocEntry\" = " + DocEntry + "";
                                rSetUpdate.DoQuery(qStr1);

                                //For Message
                                string docNum = DocNum.ToString();

                                SAPbobsCOM.CompanyService cmpSrv = Global.ocomp.GetCompanyService();
                                var msgSrv = (SAPbobsCOM.MessagesService)cmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.MessagesService);
                                var msg = (SAPbobsCOM.Message)msgSrv.GetDataInterface(SAPbobsCOM.MessagesServiceDataInterfaces.msdiMessage);

                                msg.Subject = "Request for Approving Document Generation";
                                msg.Text = $"Delivery Schedule created from Custom Form. DocNum: {docNum}";
                                msg.Priority = SAPbobsCOM.BoMsgPriorities.pr_High;

                                var r = msg.RecipientCollection.Add();
                                r.UserType = SAPbobsCOM.BoMsgRcpTypes.rt_InternalUser;
                                r.UserCode = userCode;
                                r.SendInternal = SAPbobsCOM.BoYesNoEnum.tYES;


                                msgSrv.SendMessage(msg);



                                //For Message End

                                //for CHild
                                while (!rSet1.EoF)
                                {
                                    SAPbobsCOM.GeneralDataCollection oChild = oGeneralData.Child("FIL_DR_APPDECSN");
                                    SAPbobsCOM.GeneralData oChildData = oChild.Add();
                                    oChildData.SetProperty("U_STAGEID", rSet1.Fields.Item("Stage").Value.ToString());
                                    oChildData.SetProperty("U_USERID", rSet1.Fields.Item("UserId").Value.ToString());
                                    oChildData.SetProperty("U_STATUS", "P");
                                    rSet1.MoveNext();
                                }

                                rSet1.MoveFirst();
                                //for CHild
                                while (!rSet1.EoF)
                                {
                                    SAPbobsCOM.GeneralDataCollection oChild1 = oGeneralData.Child("FIL_DR_APPDECSS");
                                    SAPbobsCOM.GeneralData oChildData1 = oChild1.Add();
                                    oChildData1.SetProperty("U_STAGEID", rSet1.Fields.Item("Stage").Value.ToString());
                                    oChildData1.SetProperty("U_STATUS", "P");
                                    oChildData1.SetProperty("U_MAXREQR", rSet1.Fields.Item("MaxReqr").Value.ToString());
                                    oChildData1.SetProperty("U_MAXRJREQ", rSet1.Fields.Item("MaxRejReqr").Value.ToString());
                                    rSet1.MoveNext();
                                }


                                //    oGeneralService.

                                try
                                {
                                    oGeneralService.Add(oGeneralData);
                                    Application.SBO_Application.StatusBar.SetText($"Aprroval Data Saved Successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                                }
                                catch (Exception comEx)
                                {
                                    Application.SBO_Application.StatusBar.SetText($"COMException: {comEx.Message}", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                }

                            }
                            else
                            {
                                //  Global.objFun.ShowSuccess("No query returned TRUE");
                                // BubbleEvent = false;


                            }
                        }
                        else
                        {
                            //  Global.objFun.ShowSuccess("Alreday Send to Aprroval");
                            //   BubbleEvent = false;

                        }
                    }
                    else
                    {
                        //  Global.objFun.ShowSuccess("Data Not Found");
                        //  BubbleEvent = false;
                    }
                    Global.G_UI_Application.ActivateMenuItem("1304");
                    oForms.Freeze(false);

                }
            }
            catch(Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText("Error : " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        public static string ResolveQueryPlaceholders(SAPbouiCOM.Form oForm, string query)
        {

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\$\[\@[^]]+\]");
            var matches = regex.Matches(query);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string placeholder = match.Value;

                string clean = placeholder.Replace("$[", "").Replace("]", "");


                string[] parts = clean.Split('.');
                // var parts = clean.Split(new string[] { "\".\"" }, StringSplitOptions.None);

                if (parts.Length >= 2)
                {
                    string tableName = parts[0];
                    string fieldName = parts[1];

                    try
                    {
                        string value = oForm.DataSources.DBDataSources.Item(tableName).GetValue(fieldName, 0).Trim();

                        bool isString = !double.TryParse(value, out _) || placeholder.ToLower().Contains(".alpha");

                        string formattedValue = isString ? $"'{value}'" : value;

                        query = query.Replace(placeholder, formattedValue);
                    }
                    catch
                    {
                        Application.SBO_Application.StatusBar.SetText($"Could not resolve placeholder: {placeholder}", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    }
                }
            }

            return query;
        }


        //private static bool ValidateForm(ref SAPbouiCOM.Form pForm, ref bool BubbleEvent)
        //{
        //    try
        //    {

        //        if (!ValidateMatrix(ref pForm, ref BubbleEvent))
        //        {
        //            BubbleEvent = false;
        //            return BubbleEvent;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Global.objFun.ShowError(ex.Message);
        //        BubbleEvent = false;
        //    }
        //    return BubbleEvent;
        //}

        //private static bool ValidateMatrix(ref SAPbouiCOM.Form pForm, ref bool BubbleEvent)
        //{
        //     SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)pForm.Items.Item("MTX01").Specific;
        //    SAPbouiCOM.DBDataSource oDataSource = pForm.DataSources.DBDataSources.Item("@FIL_DR_DELRSCHD");
        //    SAPbobsCOM.Recordset rSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
        //    oMatrix.FlushToDataSource();
        //    for (int i = oDataSource.Size - 1; i >= 0; i--)
        //    {

        //        double schedQty = Convert.ToDouble(oDataSource.GetValue("U_SCHEDQTY", i));
        //        double orderQty = Convert.ToDouble(oDataSource.GetValue("U_ORDERQTY", i));
        //        string baseLine = Convert.ToString(oDataSource.GetValue("U_BASELINE", i));
        //        string  ItemCode = Convert.ToString(oDataSource.GetValue("U_ITEMCODE", i));
        //        string BaseEntry = Convert.ToString(oDataSource.GetValue("U_BASENTRY", i));
        //        string DocNum = ((SAPbouiCOM.EditText)pForm.Items.Item("ETDOCNUM").Specific).Value.Trim();

        //        string qStr = $@"
        //                SELECT IFNULL(SUM(T0.""U_SCHEDQTY""), 0) AS ""Quantity""
        //                FROM ""@FIL_DR_DELRSCHD"" T0
        //                INNER JOIN ""@FIL_DH_DELRSCHD"" T2 on T0.""DocEntry"" = T2.""DocEntry""
        //                INNER JOIN ""RDR1"" T1 ON T1.""DocEntry"" = T0.""U_BASENTRY""
        //                                       AND T1.""ItemCode"" = T0.""U_ITEMCODE""
        //                                       AND T1.""LineNum"" = T0.""U_BASELINE""
        //                WHERE T2.""DocNum"" <> '{DocNum}'
        //                  AND T0.""U_BASENTRY"" = '{BaseEntry}' 
        //                  AND T0.""U_ITEMCODE"" = '{ItemCode}' 
        //                  AND T0.""U_BASELINE"" = '{baseLine}';
        //           ";
        //        rSet.DoQuery(qStr);

        //        if (rSet.RecordCount > 0)
        //        {
        //            double quantity = Convert.ToDouble(rSet.Fields.Item("Quantity").Value.ToString());
        //            //double SoOpenQty = Convert.ToDouble(rSet.Fields.Item("SoOpenQty").Value.ToString());
        //            double QuantitySum = quantity + schedQty;
        //            if (QuantitySum > orderQty)
        //            {
        //                string errorMessage = $"Row {i}: Schedule quantity cannot be greater than order quantity. " +
        //                            $"Current Schedule Quantity: {schedQty}, Already Scheduled Quantity: {quantity}";
        //                Global.objFun.ShowError(errorMessage);
        //                BubbleEvent = false;
        //                return false;
        //            }
        //            //else if(QuantitySum > SoOpenQty)
        //            //{
        //            //    string errorMessage = $"Row {i}: Schedule quantity cannot be greater than SO open Qty = {SoOpenQty}" +
        //            //                $" Current Schedule Quantity: {schedQty}, Already Scheduled Quantity: {quantity}";
        //            //    Global.objFun.ShowError(errorMessage);
        //            //    BubbleEvent = false;
        //            //    return false;
        //            //}

        //        }
        //    }
        //    return true;
        //}




        private void Form_DataAddAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            SAPbouiCOM.Form oForms = Application.SBO_Application.Forms.Item(pVal.FormUID);

            Global.dlvDocEntry = oForms.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("DocNum", 0);

        }


        private void CLSBTN_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            SAPbouiCOM.Form oForm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;

            if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
            {
                int ret = Application.SBO_Application.MessageBox("Do you really want to Close?", 2, "Yes", "No");

                if (ret != 1)
                {
                    BubbleEvent = false;
                    return;
                }

                SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)oForm.Items.Item("MTX01").Specific;
                oForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                bool needUpdate = false;
                bool allZero = true;

                oForm.Freeze(true);
                for (int i = 1; i <= MTX01.RowCount; i++)
                {
                    double quantity = Convert.ToDouble(
                        ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLSCHEDQTY").Cells.Item(i).Specific).Value);

                    double AldlvQty = Convert.ToDouble(
                        ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLALDLYQTY").Cells.Item(i).Specific).Value);

                    if (AldlvQty != 0) allZero = false;

                    if (AldlvQty < quantity)
                    {
                        needUpdate = true;
                        ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLSCHEDQTY").Cells.Item(i).Specific).Active = true;
                        ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLSCHEDQTY").Cells.Item(i).Specific).Value = AldlvQty.ToString();
                        Application.SBO_Application.SendKeys("{TAB}");
                        ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLDELVDATE").Cells.Item(i).Specific).Active = true;
                    }
                }


                var ds = oForm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD");
                var cbStatus = (SAPbouiCOM.ComboBox)oForm.Items.Item("CBSTATUS").Specific;

                if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                    oForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;

                if (needUpdate && !allZero)
                {

                    oForm.Freeze(true);
                    isButton = true;
                    ds.SetValue("Canceled", 0, "N");
                    cbStatus.Select("C", SAPbouiCOM.BoSearchKey.psk_ByValue);
                    oForm.Items.Item("1").Click();
                    oForm.Freeze(false);

                }
                else
                {

                    oForm.Freeze(true);
                    isButton = true;
                    ds.SetValue("Canceled", 0, "Y");
                    cbStatus.Select("C", SAPbouiCOM.BoSearchKey.psk_ByValue);
                    oForm.Items.Item("1").Click();
                    oForm.Freeze(false);
                }
                oForm.Freeze(false);
            }
        }

        private void CLSBTN_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Global.G_UI_Application.ActivateMenuItem("1304");
        }

        private void BTNRVRS_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Form oFrom = Application.SBO_Application.Forms.Item(pVal.FormUID);

            string DocEntry = oFrom.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").GetValue("DocEntry", 0);
            Form_No_ApprovalList AprList = new Form_No_ApprovalList();
            AprList.Show();
            SAPbouiCOM.Form cForm = Application.SBO_Application.Forms.Item("FIL_FRM_NO_APRVLIST");

            Form_No_ApprovalList.ApprovalList(ref cForm, DocEntry);

        }

        private void Button0_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
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


        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);

                if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                {
                    Application.SBO_Application.StatusBar.SetText("Please wait... Processing", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                    SAPbobsCOM.Documents oDelivery = (SAPbobsCOM.Documents)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDeliveryNotes);
                    SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)oForm.Items.Item("MTX01").Specific;
                    SAPbouiCOM.DBDataSource oDataSource = oForm.DataSources.DBDataSources.Item("@FIL_DR_DELRSCHD");
                    string bpcode = ((SAPbouiCOM.EditText)oForm.Items.Item("ETBPCODE").Specific).Value;
                    //  string Branch = ((SAPbouiCOM.EditText)oForm.Items.Item("CBBRANCH").Specific).Value;
                    string discount = ((SAPbouiCOM.EditText)oForm.Items.Item("ETDISPRCNT").Specific).Value;

                    string remarks = ((SAPbouiCOM.EditText)oForm.Items.Item("ETREMARKS").Specific).Value;
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
                    oDelivery.UserFields.Fields.Item("U_LTIME").Value = StartTime;
                    if (!string.IsNullOrWhiteSpace(EndDate)) oDelivery.UserFields.Fields.Item("U_LEDATE").Value = new DateTime(Convert.ToInt32(EndDate.Substring(0, 4)), Convert.ToInt32(EndDate.Substring(4, 2)), Convert.ToInt32(EndDate.Substring(6, 2)));
                    oDelivery.UserFields.Fields.Item("U_LETIME").Value = EndTime;
                    oDelivery.Address2 = ShipTo;
                    oDelivery.DiscountPercent = Convert.ToDouble(discount);


                    for (int i = 1; i <= MTX01.RowCount; i++)
                    {
                        string DocNum = ((SAPbouiCOM.EditText)oForm.Items.Item("ETDOCNUM").Specific).Value;
                        string itemCode = ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLITEMCODE").Cells.Item(i).Specific).Value;
                        string WhsCode = ((SAPbouiCOM.EditText)MTX01.Columns.Item("CLWHSCODE").Cells.Item(i).Specific).Value;

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

                            string DeliveryBaseEntry = oDataSource.GetValue("DocEntry", i - 1).ToString();
                            string LineId = oDataSource.GetValue("LineId", i - 1).ToString();
                            string Object = oDataSource.GetValue("Object", i - 1).ToString();


                            oDelivery.Lines.ItemCode = itemCode;
                            oDelivery.Lines.Quantity = DlvryQty;
                            oDelivery.Lines.UnitPrice = Convert.ToDouble(unitPrice);
                            oDelivery.Lines.DiscountPercent = Convert.ToDouble(discountprcnt);
                            oDelivery.Lines.WarehouseCode = WhsCode;


                            oDelivery.Lines.LocationCode = Convert.ToInt32(rsLoc.Fields.Item("Location").Value);

                            oDelivery.Lines.BaseEntry = Convert.ToInt32(BaseEntry);
                            oDelivery.Lines.BaseType = Convert.ToInt32(baseType);
                            oDelivery.Lines.BaseLine = Convert.ToInt32(BaseLine);

                            //SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            //rs.DoQuery($@"SELECT ""LocCode"" FROM ""RDR1"" WHERE ""DocEntry""={BaseEntry} AND ""LineNum""={BaseLine}");
                            //oDelivery.Lines.LocationCode = Convert.ToInt32(rs.Fields.Item("LocCode").Value);

                            oDelivery.Lines.UserFields.Fields.Item("U_BASENTRY").Value = DeliveryBaseEntry;
                            oDelivery.Lines.UserFields.Fields.Item("U_BASELINE").Value = LineId;
                            oDelivery.Lines.UserFields.Fields.Item("U_BASENUM").Value = DocNum;
                            oDelivery.Lines.UserFields.Fields.Item("U_BASETYPE").Value = Object;
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

                        if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                        {
                            oForm.Items.Item("BTCOPY").Enabled = false;
                            ((SAPbouiCOM.EditText)oForm.Items.Item("ETVEHICLNO").Specific).Value = "";
                            ((SAPbouiCOM.EditText)oForm.Items.Item("ETDRVRNAME").Specific).Value = "";
                            ((SAPbouiCOM.EditText)oForm.Items.Item("ETCELLPHNO").Specific).Value = "";
                            ((SAPbouiCOM.ComboBox)oForm.Items.Item("CBSHIPTYPE").Specific).Select("", SAPbouiCOM.BoSearchKey.psk_ByValue);

                            ((SAPbouiCOM.EditText)oForm.Items.Item("ETOWNSHP").Specific).Value = "";
                            ((SAPbouiCOM.EditText)oForm.Items.Item("ETTNSNAME").Specific).Value = "";
                            ((SAPbouiCOM.EditText)oForm.Items.Item("ETSUPNAME").Specific).Value = "";
                            ((SAPbouiCOM.EditText)oForm.Items.Item("ETLDATE").Specific).Value = "";
                            ((SAPbouiCOM.EditText)oForm.Items.Item("ETLTIME").Specific).Value = "";
                            ((SAPbouiCOM.EditText)oForm.Items.Item("ETLEDATE").Specific).Value = "";
                            ((SAPbouiCOM.EditText)oForm.Items.Item("ETLETIME").Specific).Value = "";

                            oForm.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE;
                        }

                        Application.SBO_Application.OpenForm(SAPbouiCOM.BoFormObjectEnum.fo_DeliveryNotes, "", TransferEntry.ToString()

                        );


                    }
                }

                else
                {
                    Global.objFun.ShowError("Update the Document First");
                }
            }

            catch(Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText("Error : " + ex.Message,SAPbouiCOM.BoMessageTime.bmt_Short,SAPbouiCOM.BoStatusBarMessageType.smt_Error);

            }
        }

        private void OnCustomInitialize()
        {

        }


    }
}
