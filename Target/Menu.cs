using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target
{
    class Menu
    {
        public void BasicStart()
        {
            CompanyConnection(); //1)Company connection 
            CreateMainMenu("43520", "FIL_MN_TARGET", "Sales Target", 13, 2, true);//parent 2 step
            CreateMainMenu("2048", "MN_DELIVERY", "Delivery Schedule", 3, 1, true);//parent 2 step
            CreateMainMenu("FIL_MN_TARGET", "FIL_MASTER", "Master", 0, 2, false);
            //String Menu
            CreateMainMenu("FIL_MASTER", "FIL_LOCTYPE", "Location Type", 0, 1, false);
            CreateMainMenu("FIL_MASTER", "FIL_LOCATION", "Location", 1, 1, false);
            CreateMainMenu("FIL_MASTER", "FIL_LOCEMPMAPPING", "Location Employee Mapping", 2, 1, false);
            //
            CreateMainMenu("FIL_MN_TARGET", "FIL_TRANSACTION", "Transaction", 1, 2, false);
            //String Menu
            CreateMainMenu("FIL_TRANSACTION", "FIL_AREATARGET", "Area Target", 0, 1, false);
            CreateMainMenu("FIL_TRANSACTION", "FIL_DEALERTARGET", "Dealer Target", 1, 1, false);
            //CreateMainMenu("SSM", "SSMS", "Setup", 0, 2, false);//parent 2 step
            //CreateMainMenu("SSMS", "VEHM", "Addon Setup", 0, 1, false);  //setup(No UDO)
            //string loggedInUser = Global.ocomp.UserName;
            //int loggedInUser2 = Global.ocomp.UserSignature;
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if(pVal.BeforeAction && pVal.MenuUID == "FIL_LOCTYPE")
                {
                    string formUID = "FIL_FRM_MD_LOCTYPE"; // Unique ID for the form
                                                            // Check if the form is already open
                    if (IsFormOpen(formUID))
                    {
                        Global.G_UI_Application.Forms.Item(formUID).Select();
                        Global.G_UI_Application.StatusBar.SetText("Form is already open.",
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        return;
                    }

                    LocationTypes activeForm = new LocationTypes();
                    activeForm.Show();
                    SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.Item("FIL_FRM_MD_LOCTYPE"); //form defining assigin
                    if (ofrm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                    {
                        string sqlQuerybpl = string.Format("SELECT {0}Code{0},{0}Name{0} FROM {0}@FIL_MD_LOCTYPE{0}", '"');
                        SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbPart").Specific;   //object defining- Define a combo box
                        Global.objFun.setComboBoxWithoutDescription(ocmbDept, sqlQuerybpl);
                    }
                    SAPbouiCOM.EditText oedtetDocE, oedtetCode;
                    oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                    oedtetDocE.Item.Visible = false;
                    oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                    oedtetCode.Item.Enabled = true;
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "FIL_LOCATION")
                {
                    string formUID = "FIL_FRM_MD_LOCATION"; // Unique ID for the form
                                                           // Check if the form is already open
                    if (IsFormOpen(formUID))
                    {
                        Global.G_UI_Application.Forms.Item(formUID).Select();
                        Global.G_UI_Application.StatusBar.SetText("Form is already open.",
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        return;
                    }

                    Locations activeForm = new Locations();
                    activeForm.Show();
                    SAPbouiCOM.Form ofrm = Application.SBO_Application.Forms.ActiveForm;
                    //SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.Item("FIL_FRM_MD_LOCATION"); //form defining assigin
                    if (ofrm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                    {
                        string sqlQuerybpl = string.Format("SELECT {0}Code{0},{0}Name{0} FROM {0}@FIL_MD_LOCTYPE{0}", '"');
                        SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cmTyeCod").Specific;   //object defining- Define a combo box
                        Global.objFun.setComboBoxWithoutDescription(ocmbDept, sqlQuerybpl);
                    }
                    SAPbouiCOM.EditText oedtetDocE, oedtetCode;
                    oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                    oedtetDocE.Item.Visible = false;
                    oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                    oedtetCode.Item.Enabled = true;

                }
                else if (pVal.BeforeAction && pVal.MenuUID == "FIL_LOCEMPMAPPING")
                {
                    string formUID = "FIL_FRM_MD_LOCWEMP"; // Unique ID for the form
                                                             // Check if the form is already open
                    if (IsFormOpen(formUID))
                    {
                        Global.G_UI_Application.Forms.Item(formUID).Select();
                        Global.G_UI_Application.StatusBar.SetText("Form is already open.",
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        return;
                    }

                    LocationEmpMapping activeForm = new LocationEmpMapping();
                    activeForm.Show();

                    SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.Item("FIL_FRM_MD_LOCWEMP"); //form defining assigin
                    if (ofrm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                    {
                        string sqlQuerybpl = string.Format("SELECT {0}Code{0},{0}Name{0} FROM {0}@FIL_MD_LOCTYPE{0}", '"');
                        SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbTypCod").Specific;   //object defining- Define a combo box
                        Global.objFun.setComboBoxWithoutDescription(ocmbDept, sqlQuerybpl);
                        //Sales Person
                        string sqlQuerySP = string.Format("SELECT {0}SlpCode{0},{0}SlpName{0} FROM {0}OSLP{0}", '"');
                        SAPbouiCOM.ComboBox ocmbSP = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbSlpCod").Specific;   //object defining- Define a combo box
                        Global.objFun.setComboBoxWithoutDescription(ocmbSP, sqlQuerySP);
                    }
                    SAPbouiCOM.EditText oedtetDocE, oedtetCode;
                    oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                    oedtetDocE.Item.Visible = false;
                    oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                    oedtetCode.Item.Enabled = false;
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "FIL_AREATARGET")
                {
                    string formUID = "FIL_FRM_DD_SALESTAR"; // Unique ID for the form
                                                            // Check if the form is already open
                    if (IsFormOpen(formUID))
                    {
                        Global.G_UI_Application.Forms.Item(formUID).Select();
                        Global.G_UI_Application.StatusBar.SetText("Form is already open.",
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        return;
                    }

                    AreaTarget activeForm = new AreaTarget();
                    activeForm.Show();
                    SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.Item("FIL_FRM_DD_SALESTAR"); //form defining assigin
                    try
                    {
                        ofrm.Freeze(true);
                        if (ofrm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                        {
                            string sqlQuerybpl = string.Format("SELECT distinct {0}Category{0},{0}Category{0} as {0}CategoryName{0} FROM {0}OFPR{0}", '"');
                            SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;   //object defining- Define a combo box
                            Global.objFun.setComboBoxValue(ocmbDept, sqlQuerybpl);

                            //string sqlQuery = string.Format("SELECT A.{0}Code{0},A.{0}Name{0}  from {0}@FIL_MD_LOCATION{0} A Where A.{0}U_LOTYCODE{0}='105' ", '"');
                            //SAPbouiCOM.ComboBox ocmbLocCod = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbLocCod").Specific;
                            //Global.objFun.setComboBoxWithoutDescription(ocmbLocCod, sqlQuery);
                            //----------------------------------------Matrix Data Load------------------------------------------
                            string strqry = string.Format("SELECT A.{0}ItmsGrpCod{0},A.{0}ItmsGrpNam{0}  from {0}OITB{0} A Where A.{0}U_TARGET{0}='Y' ", '"');


                            SAPbobsCOM.Recordset ors = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            ors.DoQuery(strqry); //WILL CALL THE RECORDSET AND TO PASS THE STRING WHERE DECLARED THE QUERY.

                            SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;//define matrix
                            SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_SALESTAR");   //DEFINE  DATASOURCES.1
                            SAPbouiCOM.DBDataSource oDBL = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DR_SALESTAR");   //DEFINE  DATASOURCES.1

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
                        SAPbouiCOM.EditText oedtetDocE;
                        oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocEny").Specific;
                        oedtetDocE.Item.Visible = false;
                        ofrm.Freeze(false);
                    }
                    catch(Exception)
                    {
                        ofrm.Freeze(false);
                    }

                }
                else if (pVal.BeforeAction && pVal.MenuUID == "FIL_DEALERTARGET")
                {
                    string formUID = "FIL_FRM_DD_DEALERTAR"; // Unique ID for the form
                                                            // Check if the form is already open
                    if (IsFormOpen(formUID))
                    {
                        Global.G_UI_Application.Forms.Item(formUID).Select();
                        Global.G_UI_Application.StatusBar.SetText("Form is already open.",
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        return;
                    }

                    DealerTarget activeForm = new DealerTarget();
                    activeForm.Show();
                    SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.Item("FIL_FRM_DD_DEALERTAR"); //form defining assigin
                    try
                    {
                        ofrm.Freeze(true);
                        if (ofrm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                        {
                            string sqlQuerybpl = string.Format("SELECT distinct {0}Category{0},{0}Category{0} as {0}CategoryName{0} FROM {0}OFPR{0}", '"');
                            SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;   //object defining- Define a combo box
                            Global.objFun.setComboBoxValue(ocmbDept, sqlQuerybpl);
                            //----------------------------------------Matrix Data Load----------------//--------------------------
                            string strqry = string.Format("SELECT A.{0}ItmsGrpCod{0},A.{0}ItmsGrpNam{0}  from {0}OITB{0} A Where A.{0}U_TARGET{0}='Y' ", '"');


                            SAPbobsCOM.Recordset ors = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            ors.DoQuery(strqry); //WILL CALL THE RECORDSET AND TO PASS THE STRING WHERE DECLARED THE QUERY.

                            SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;//define matrix
                            SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR");   //DEFINE  DATASOURCES.1
                            SAPbouiCOM.DBDataSource oDBL = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DR_DEALERTAR");   //DEFINE  DATASOURCES.1

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
                        SAPbouiCOM.EditText oedtetDocE;
                        oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocEny").Specific;
                        oedtetDocE.Item.Visible = false;
                        ofrm.Freeze(false);
                    }
                    catch (Exception)
                    {
                        ofrm.Freeze(false);
                    }

                }

                else if (pVal.BeforeAction && pVal.MenuUID == "MN_DELIVERY")
                {
                    string formUID = "FIL_FRM_DH_DELRSCHD"; // Unique ID for the form
                                                            // Check if the form is already open
                    if (IsFormOpen(formUID))
                    {
                        Global.G_UI_Application.Forms.Item(formUID).Select();
                        Global.G_UI_Application.StatusBar.SetText("Form is already open.",
                            SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        return;
                    }

                    Form_UDO_DeliverySchedule activeForm = new Form_UDO_DeliverySchedule();
                    activeForm.Show();

                    SAPbouiCOM.Form oform = (SAPbouiCOM.Form)Application.SBO_Application.Forms.Item(formUID);


                    oform.Freeze(true);
                    oform.Settings.MatrixUID = "MTX01";
                    oform.Settings.Enabled = true;

                    oform.Freeze(false);


                    try
                    {
                       // oform.Freeze(false);
                        if (oform.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                        {
                            //SAPbouiCOM.ComboBox ocmb = (SAPbouiCOM.ComboBox)oform.Items.Item("CBSERIES").Specific;                                                                         //1 step do generate series:
                            //Global.objFun.LoadComboBoxSeries(ocmb, "FIL_D_DELRSCHD");
                            //string oComValue = ocmb.Selected.Value;
                            //long DocNo = oform.BusinessObject.GetNextSerialNumber(oComValue, "FIL_D_DELRSCHD");

                            //oform.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").SetValue("DocNum", 0, DocNo.ToString());

                            SAPbouiCOM.ComboBox ocmb = null;

                            if (oform.Items.Item("CBSERIES") != null)
                            {
                                ocmb = (SAPbouiCOM.ComboBox)oform.Items.Item("CBSERIES").Specific;
                            }

                            if (ocmb == null)
                            {
                                throw new Exception("CBSERIES not found.");
                            }

                            Global.objFun.LoadComboBoxSeries(ocmb, "FIL_D_DELRSCHD");

                            if (ocmb.ValidValues.Count > 0 && ocmb.Selected == null)
                            {
                                ocmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                            }

                            if (ocmb.Selected == null)
                            {
                                throw new Exception("No series selected.");
                            }

                            string seriesValue = ocmb.Selected.Value;
                            long docNo = oform.BusinessObject.GetNextSerialNumber(seriesValue, "FIL_D_DELRSCHD");
                            oform.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").SetValue("DocNum", 0, docNo.ToString());

                            oform.Items.Item("ETDOCNUM").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Add, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                            oform.Items.Item("CBSTATUS").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Add, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                            SAPbouiCOM.Button OclsBtn = (SAPbouiCOM.Button)oform.Items.Item("CLSBTN").Specific;
                            OclsBtn.Item.Enabled = false;
                            SAPbouiCOM.Button oPost = (SAPbouiCOM.Button)oform.Items.Item("BTPSOT").Specific;
                            oPost.Item.Enabled = false;
                        }

                     
                        ////Current Date
                        SAPbobsCOM.Recordset rSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);


                        ((SAPbouiCOM.EditText)oform.Items.Item("ETPOSTDATE").Specific).Value = DateTime.Now.ToString("yyyyMMdd");
                        ((SAPbouiCOM.EditText)oform.Items.Item("ETSCHDATE").Specific).Value = DateTime.Now.ToString("yyyyMMdd");
                        ((SAPbouiCOM.EditText)oform.Items.Item("ETDOCDATE").Specific).Value = DateTime.Now.ToString("yyyyMMdd");
                     
                        SAPbouiCOM.ComboBox oCombox = (SAPbouiCOM.ComboBox)oform.Items.Item("CBSTATUS").Specific;
                        oCombox.ValidValues.Add("", "");
                        oCombox.ValidValues.Add("O", "Open");
                        oCombox.ValidValues.Add("C", "Close");



                        SAPbouiCOM.ComboBox oCombo = (SAPbouiCOM.ComboBox)oform.Items.Item("CBSHIPTYPE").Specific;
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

                        SAPbouiCOM.EditText bpCode = (SAPbouiCOM.EditText)oform.Items.Item("ETBPCODE").Specific; // define the BP Code EditText
                        bpCode.Active = true;


                        string userName = Application.SBO_Application.Company.UserName.ToString();
                        string qstr = "SELECT IFNULL(\"Fax\", '') AS \"IsAllow\" FROM \"OUSR\" WHERE \"USER_CODE\" = '" + userName.Replace("'", "''") + "'";

                        rSet.DoQuery(qstr);

                        SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)oform.Items.Item("MTX01").Specific;

                        string isAllow = "";
                        if (rSet.RecordCount > 0)
                        {
                            isAllow = (rSet.Fields.Item("IsAllow").Value ?? "").ToString().Trim().ToUpperInvariant();
                        }

                        if (isAllow == "YES")
                        {
                            SAPbouiCOM.Button AddBtn = (SAPbouiCOM.Button)oform.Items.Item("1").Specific;
                            AddBtn.Item.Enabled = false;
                            MTX01.Columns.Item("CLSCHEDQTY").Editable = false;
                            MTX01.Columns.Item("CLDLVRYQTY").Editable = true;
                        }

                        else if (isAllow == "NO")
                        {
                            MTX01.Columns.Item("CLSCHEDQTY").Editable = true;
                            MTX01.Columns.Item("CLDLVRYQTY").Editable = false;
                        }
                        else
                        {
                            oform.Items.Item("BTPSOT").Enabled = false;
                            oform.Items.Item("1").Enabled = false;
                            oform.Items.Item("CLSBTN").Enabled = false;
                            MTX01.Columns.Item("CLSCHEDQTY").Editable = false;
                            MTX01.Columns.Item("CLDLVRYQTY").Editable = false;

                        }



                    }

                    catch (Exception ex)
                    {
                        oform.Freeze(false);
                    }


                }
                //Add Form Mode Menu
                else if (!pVal.BeforeAction && pVal.MenuUID == "1282")
                {
                    SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;
                    string formtype = ofrm.UniqueID.ToString();
                    switch (formtype)
                    {
                        case "FIL_FRM_DH_DELRSCHD":
                            {

                                if (ofrm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                                {
                                    SAPbouiCOM.ComboBox ocmb = null;

                                    if (ofrm.Items.Item("CBSERIES") != null)
                                    {
                                        ocmb = (SAPbouiCOM.ComboBox)ofrm.Items.Item("CBSERIES").Specific;
                                    }

                                    if (ocmb == null)
                                    {
                                        throw new Exception("CBSERIES not found.");
                                    }

                                    Global.objFun.LoadComboBoxSeries(ocmb, "FIL_D_DELRSCHD");

                                    if (ocmb.ValidValues.Count > 0 && ocmb.Selected == null)
                                    {
                                        ocmb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                                    }

                                    if (ocmb.Selected == null)
                                    {
                                        throw new Exception("No series selected.");
                                    }

                                    string seriesValue = ocmb.Selected.Value;
                                    long docNo = ofrm.BusinessObject.GetNextSerialNumber(seriesValue, "FIL_D_DELRSCHD");
                                    ofrm.DataSources.DBDataSources.Item("@FIL_DH_DELRSCHD").SetValue("DocNum", 0, docNo.ToString());

                                    SAPbouiCOM.Button oPost = (SAPbouiCOM.Button)ofrm.Items.Item("BTPSOT").Specific;
                                    oPost.Item.Enabled = false;
                                    ofrm.Title = "Delivery Schedule/Order";
                                    SAPbouiCOM.EditText scheduledQtyHeader = (SAPbouiCOM.EditText)ofrm.Items.Item("ETTLQTY").Specific;
                                    scheduledQtyHeader.Value = "";
                                }

                                SAPbobsCOM.Recordset rSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                                //Date AUtoFillup Start
                                ((SAPbouiCOM.EditText)ofrm.Items.Item("ETPOSTDATE").Specific).Value = DateTime.Now.ToString("yyyyMMdd");
                                ((SAPbouiCOM.EditText)ofrm.Items.Item("ETSCHDATE").Specific).Value = DateTime.Now.ToString("yyyyMMdd");
                                ((SAPbouiCOM.EditText)ofrm.Items.Item("ETDOCDATE").Specific).Value = DateTime.Now.ToString("yyyyMMdd");
                                //Date AUtoFillup End

                                //CardCode Active Start
                                SAPbouiCOM.EditText bpCode = (SAPbouiCOM.EditText)ofrm.Items.Item("ETBPCODE").Specific; // define the BP Code EditText
                                bpCode.Active = true;
                                //CardCode Active End


                                string userName = Application.SBO_Application.Company.UserName.ToString();
                                string qstr = "SELECT IFNULL(\"Fax\", '') AS \"IsAllow\" FROM \"OUSR\" WHERE \"USER_CODE\" = '" + userName.Replace("'", "''") + "'";

                                rSet.DoQuery(qstr);


                                SAPbouiCOM.Matrix MTX01 = (SAPbouiCOM.Matrix)ofrm.Items.Item("MTX01").Specific;

                                string isAllow = "";
                                if (rSet.RecordCount > 0)
                                {
                                    isAllow = (rSet.Fields.Item("IsAllow").Value ?? "").ToString().Trim().ToUpperInvariant();
                                }

                                if (isAllow == "YES")
                                {
                                    SAPbouiCOM.Button AddBtn = (SAPbouiCOM.Button)ofrm.Items.Item("1").Specific;
                                    AddBtn.Item.Enabled = false;
                                    MTX01.Columns.Item("CLSCHEDQTY").Editable = false;
                                    MTX01.Columns.Item("CLDLVRYQTY").Editable = true;
                                }
                                
                                else if(isAllow == "NO")
                                {
                                    ofrm.Items.Item("BTPSOT").Enabled = false;
                                    MTX01.Columns.Item("CLSCHEDQTY").Editable = true;
                                    MTX01.Columns.Item("CLDLVRYQTY").Editable = false;
                                }
                                else
                                {
                                    ofrm.Items.Item("BTPSOT").Enabled = false;
                                    ofrm.Items.Item("1").Enabled = false;
                                    ofrm.Items.Item("CLSBTN").Enabled = false;
                                    MTX01.Columns.Item("CLSCHEDQTY").Editable = false;
                                    MTX01.Columns.Item("CLDLVRYQTY").Editable = false;

                                }
                            
                                break;
                            }

                        case "FIL_FRM_MD_LOCTYPE":
                            {

                                if (ofrm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                                {
                                    string sqlQuerybpl = string.Format("SELECT {0}Code{0},{0}Name{0} FROM {0}@FIL_MD_LOCTYPE{0}", '"');
                                    SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbPart").Specific;   //object defining- Define a combo box
                                    Global.objFun.setComboBoxValue(ocmbDept, sqlQuerybpl);
                                }
                                SAPbouiCOM.EditText oedtetDocE;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                                oedtetDocE.Item.Visible = false;
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = true;

                                break;
                            }
                        case "FIL_FRM_MD_LOCATION":
                            {
                                if (ofrm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                                {
                                    string sqlQuerybpl = string.Format("SELECT {0}Code{0},{0}Name{0} FROM {0}@FIL_MD_LOCTYPE{0}", '"');
                                    SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cmTyeCod").Specific;   //object defining- Define a combo box
                                    Global.objFun.setComboBoxValue(ocmbDept, sqlQuerybpl);
                                }
                                SAPbouiCOM.EditText oedtetDocE, oedtetCode;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                                oedtetDocE.Item.Visible = false;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = true;
                                break;
                            }
                        case "FIL_FRM_MD_LOCWEMP":
                            {
                                if (ofrm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                                {
                                    string sqlQuerybpl = string.Format("SELECT {0}Code{0},{0}Name{0} FROM {0}@FIL_MD_LOCTYPE{0}", '"');
                                    SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbTypCod").Specific;   //object defining- Define a combo box
                                    Global.objFun.setComboBoxWithoutDescription(ocmbDept, sqlQuerybpl);

                                    //Sales Person
                                    string sqlQuerySP = string.Format("SELECT {0}SlpCode{0},{0}SlpName{0} FROM {0}OSLP{0}", '"');
                                    SAPbouiCOM.ComboBox ocmbSP = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbSlpCod").Specific;   //object defining- Define a combo box
                                    Global.objFun.setComboBoxWithoutDescription(ocmbSP, sqlQuerySP);
                                }
                                SAPbouiCOM.EditText oedtetDocE, oedtetCode;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                                oedtetDocE.Item.Visible = false;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                break;
                            }
                        case "FIL_FRM_DD_SALESTAR":
                            {
                                try
                                {
                                    ofrm.Freeze(true);
                                        string sqlQuerybpl = string.Format("SELECT distinct {0}Category{0},{0}Category{0} as {0}CategoryName{0} FROM {0}OFPR{0}", '"');
                                        SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;   //object defining- Define a combo box
                                        Global.objFun.setComboBoxValue(ocmbDept, sqlQuerybpl);

                                        //string sqlQuery = string.Format("SELECT A.{0}Code{0},A.{0}Name{0}  from {0}@FIL_MD_LOCATION{0} A Where A.{0}U_LOTYCODE{0}='105' ", '"');
                                        //SAPbouiCOM.ComboBox ocmbLocCod = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbLocCod").Specific;
                                        //Global.objFun.setComboBoxWithoutDescription(ocmbLocCod, sqlQuery);
                                        //----------------------------------------Matrix Data Load------------------------------------------
                                        string strqry = string.Format("SELECT A.{0}ItmsGrpCod{0},A.{0}ItmsGrpNam{0}  from {0}OITB{0} A Where A.{0}U_TARGET{0}='Y' ", '"');


                                        SAPbobsCOM.Recordset ors = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                        ors.DoQuery(strqry); //WILL CALL THE RECORDSET AND TO PASS THE STRING WHERE DECLARED THE QUERY.

                                        SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;//define matrix
                                        SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_SALESTAR");   //DEFINE  DATASOURCES.1
                                        SAPbouiCOM.DBDataSource oDBL = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DR_SALESTAR");   //DEFINE  DATASOURCES.1

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
                                    
                                    SAPbouiCOM.EditText oedtetDocE;
                                    oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocEny").Specific;
                                    oedtetDocE.Item.Visible = false;

                                    SAPbouiCOM.EditText oedtetDocNum, oeditLocCod;
                                    SAPbouiCOM.ComboBox oeditFYR;
                                    oedtetDocNum = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                    oedtetDocNum.Item.Enabled = false;
                                    oeditLocCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etLocCod").Specific;
                                    oeditLocCod.Item.Enabled = true;
                                    oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                    oeditFYR.Item.Enabled = true;

                                    Global.objFun.Refresh(ofrm);
                                    Global.objFun.EnablePastMonthsRows(omat);
                                    ofrm.Freeze(false);
                                }
                                catch(Exception)
                                {
                                    ofrm.Freeze(false);
                                }

                                break;
                            }
                        case "FIL_FRM_DD_DEALERTAR":
                            {
                                try
                                {
                                    ofrm.Freeze(true);
                                    string sqlQuerybpl = string.Format("SELECT distinct {0}Category{0},{0}Category{0} as {0}CategoryName{0} FROM {0}OFPR{0}", '"');
                                    SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;   //object defining- Define a combo box
                                    Global.objFun.setComboBoxValue(ocmbDept, sqlQuerybpl);
                                    //----------------------------------------Matrix Data Load------------------------------------------
                                    string strqry = string.Format("SELECT A.{0}ItmsGrpCod{0},A.{0}ItmsGrpNam{0}  from {0}OITB{0} A Where A.{0}U_TARGET{0}='Y' ", '"');


                                    SAPbobsCOM.Recordset ors = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                    ors.DoQuery(strqry); //WILL CALL THE RECORDSET AND TO PASS THE STRING WHERE DECLARED THE QUERY.

                                    SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;//define matrix
                                    SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR");   //DEFINE  DATASOURCES.1
                                    SAPbouiCOM.DBDataSource oDBL = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DR_DEALERTAR");   //DEFINE  DATASOURCES.1

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

                                    SAPbouiCOM.EditText oedtetDocE;
                                    oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocEny").Specific;
                                    oedtetDocE.Item.Visible = false;
                                    SAPbouiCOM.EditText oedtetDocENum, oeditDerCod;
                                    SAPbouiCOM.ComboBox oeditFYR;
                                    oedtetDocENum = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                    oedtetDocENum.Item.Enabled = false;
                                    oeditDerCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etDerCod").Specific;
                                    oeditDerCod.Item.Enabled = true;
                                    oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                    oeditFYR.Item.Enabled = true;
                                    Global.objFun.RefreshDealer(ofrm);
                                    Global.objFun.EnablePastMonthsRows(omat);
                                    ofrm.Freeze(false);
                                }
                                catch (Exception)
                                {
                                    ofrm.Freeze(false);
                                }

                                break;
                            }
                    }



                }
                //Find Mode
                else if (!pVal.BeforeAction && pVal.MenuUID == "1281")
                {
                    SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;
                    string formtype = ofrm.UniqueID.ToString();
                    switch (formtype)
                    {

                        case "FIL_FRM_DH_DELRSCHD":
                            {
                                var cb = (SAPbouiCOM.ComboBox)ofrm.Items.Item("CBSTATUS").Specific;
                                cb.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

                                break;
                            }
                        case "FIL_FRM_MD_LOCTYPE":
                            {
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = true;
                                //oedtetName = (SAPbouiCOM.EditText)ofrm.Items.Item("etName").Specific;
                                //oedtetName.Item.Enabled = false;
                                break;
                            }
                        case "FIL_FRM_MD_LOCATION":
                            {
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = true;
                                break;
                            }
                        case "FIL_FRM_MD_LOCWEMP":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oedtetCode;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                                oedtetDocE.Item.Visible = false;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = true;
                                break;
                            }
                        case "FIL_FRM_DD_SALESTAR":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oeditLocCod;
                                SAPbouiCOM.ComboBox oeditFYR;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                oedtetDocE.Item.Enabled = true;
                                oeditLocCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etLocCod").Specific;
                                oeditLocCod.Item.Enabled = true;
                                oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                oeditFYR.Item.Enabled = true;
                                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;
                                Global.objFun.DisablePastMonthsRows(omat);
                                Global.objFun.Refresh(ofrm);
                                break;
                            }
                        case "FIL_FRM_DD_DEALERTAR":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oeditDerCod;
                                SAPbouiCOM.ComboBox oeditFYR;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                oedtetDocE.Item.Enabled = true;
                                oeditDerCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etDerCod").Specific;
                                oeditDerCod.Item.Enabled = true;
                                oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                oeditFYR.Item.Enabled = true;
                                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;
                                Global.objFun.DisablePastMonthsRows(omat);
                                Global.objFun.RefreshDealer(ofrm);
                                break;
                            }
                    }
                }
                //First
                else if (!pVal.BeforeAction && pVal.MenuUID == "1288")
                {
                    SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;
                    string formtype = ofrm.UniqueID.ToString();
                    switch (formtype)
                    {
                        case "FIL_FRM_MD_LOCTYPE":
                            {
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                break;
                            }
                        case "FIL_FRM_MD_LOCATION":
                            {
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_MD_LOCATION");
                                string TyeCode = ofrm.DataSources.DBDataSources.Item("@FIL_MD_LOCATION").GetValue("U_LOTYCODE", 0);

                                if (TyeCode != "")
                                {
                                    string sqlQueryp = string.Format("SELECT B.{0}Code{0},B.{0}Name{0}  from {0}@FIL_MD_LOCTYPE{0} A inner join {0}@FIL_MD_LOCATION{0} B on A.{0}U_PARENT{0}=B.{0}U_LOTYCODE{0} Where A.{0}Code{0}='" + TyeCode + "' ", '"');

                                    SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cmParent").Specific;   //object defining- Define a combo box
                                    Global.objFun.setComboBoxWithoutDescription(ocmbDept, sqlQueryp);
                                }
                                break;
                            }
                        case "FIL_FRM_MD_LOCWEMP":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oedtetCode;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                                oedtetDocE.Item.Visible = false;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                break;
                            }
                        case "FIL_FRM_DD_SALESTAR":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oeditLocCod;
                                SAPbouiCOM.ComboBox oeditFYR;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                oedtetDocE.Item.Enabled = false;
                                oeditLocCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etLocCod").Specific;
                                oeditLocCod.Item.Enabled = false;
                                oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                oeditFYR.Item.Enabled = false;
                                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;
                                Global.objFun.DisablePastMonthsRows(omat);
                                SAPbouiCOM.DBDataSource ods = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_SALESTAR");
                                Global.objFun.SetByLocation(ofrm, ods);
                                break;
                            }
                        case "FIL_FRM_DD_DEALERTAR":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oeditDerCod;
                                SAPbouiCOM.ComboBox oeditFYR;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                oedtetDocE.Item.Enabled = false;
                                oeditDerCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etDerCod").Specific;
                                oeditDerCod.Item.Enabled = false;
                                oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                oeditFYR.Item.Enabled = false;
                                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;
                                Global.objFun.DisablePastMonthsRows(omat);
                                SAPbouiCOM.DBDataSource ods = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR");
                                Global.objFun.SetByDealer(ofrm, ods);
                                break;
                            }
                    }
                }
                //Previous
                else if (!pVal.BeforeAction && pVal.MenuUID == "1289")
                {
                    SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;
                    string formtype = ofrm.UniqueID.ToString();
                    switch (formtype)
                    {
                        case "FIL_FRM_MD_LOCTYPE":
                            {
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                break;
                            }
                        case "FIL_FRM_MD_LOCATION":
                            {
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_MD_LOCATION");
                                string TyeCode = ofrm.DataSources.DBDataSources.Item("@FIL_MD_LOCATION").GetValue("U_LOTYCODE", 0);

                                if (TyeCode != "")
                                {
                                    string sqlQueryp = string.Format("SELECT B.{0}Code{0},B.{0}Name{0}  from {0}@FIL_MD_LOCTYPE{0} A inner join {0}@FIL_MD_LOCATION{0} B on A.{0}U_PARENT{0}=B.{0}U_LOTYCODE{0} Where A.{0}Code{0}='" + TyeCode + "' ", '"');

                                    SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cmParent").Specific;   //object defining- Define a combo box
                                    Global.objFun.setComboBoxWithoutDescription(ocmbDept, sqlQueryp);
                                }
                                break;
                            }
                        case "FIL_FRM_MD_LOCWEMP":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oedtetCode;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                                oedtetDocE.Item.Visible = false;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                break;
                            }
                        case "FIL_FRM_DD_SALESTAR":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oeditLocCod;
                                SAPbouiCOM.ComboBox oeditFYR;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                oedtetDocE.Item.Enabled = false;
                                oeditLocCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etLocCod").Specific;
                                oeditLocCod.Item.Enabled = false;
                                oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                oeditFYR.Item.Enabled = false;
                                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;
                                Global.objFun.DisablePastMonthsRows(omat);
                                SAPbouiCOM.DBDataSource ods = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_SALESTAR");
                                Global.objFun.SetByLocation(ofrm, ods);
                                break;
                            }
                        case "FIL_FRM_DD_DEALERTAR":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oeditDerCod;
                                SAPbouiCOM.ComboBox oeditFYR;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                oedtetDocE.Item.Enabled = false;
                                oeditDerCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etDerCod").Specific;
                                oeditDerCod.Item.Enabled = false;
                                oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                oeditFYR.Item.Enabled = false;
                                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;
                                Global.objFun.DisablePastMonthsRows(omat);
                                SAPbouiCOM.DBDataSource ods = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR");
                                Global.objFun.SetByDealer(ofrm, ods);
                                break;
                            }
                    }
                }
                //Next
                else if (!pVal.BeforeAction && pVal.MenuUID == "1290")
                {
                    SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;
                    string formtype = ofrm.UniqueID.ToString();
                    switch (formtype)
                    {
                        case "FIL_FRM_MD_LOCTYPE":
                            {
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                break;
                            }
                        case "FIL_FRM_MD_LOCATION":
                            {
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_MD_LOCATION");
                                string TyeCode = ofrm.DataSources.DBDataSources.Item("@FIL_MD_LOCATION").GetValue("U_LOTYCODE", 0);

                                if (TyeCode != "")
                                {
                                    string sqlQueryp = string.Format("SELECT B.{0}Code{0},B.{0}Name{0}  from {0}@FIL_MD_LOCTYPE{0} A inner join {0}@FIL_MD_LOCATION{0} B on A.{0}U_PARENT{0}=B.{0}U_LOTYCODE{0} Where A.{0}Code{0}='" + TyeCode + "' ", '"');

                                    SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cmParent").Specific;   //object defining- Define a combo box
                                    Global.objFun.setComboBoxWithoutDescription(ocmbDept, sqlQueryp);
                                }
                                break;
                            }
                        case "FIL_FRM_MD_LOCWEMP":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oedtetCode;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                                oedtetDocE.Item.Visible = false;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                break;
                            }
                        case "FIL_FRM_DD_SALESTAR":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oeditLocCod;
                                SAPbouiCOM.ComboBox oeditFYR;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                oedtetDocE.Item.Enabled = false;
                                oeditLocCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etLocCod").Specific;
                                oeditLocCod.Item.Enabled = false;
                                oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                oeditFYR.Item.Enabled = false;
                                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;
                                Global.objFun.DisablePastMonthsRows(omat);
                                SAPbouiCOM.DBDataSource ods = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_SALESTAR");
                                Global.objFun.SetByLocation(ofrm, ods);
                                break;
                            }
                        case "FIL_FRM_DD_DEALERTAR":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oeditDerCod;
                                SAPbouiCOM.ComboBox oeditFYR;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                oedtetDocE.Item.Enabled = false;
                                oeditDerCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etDerCod").Specific;
                                oeditDerCod.Item.Enabled = false;
                                oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                oeditFYR.Item.Enabled = false;
                                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;
                                Global.objFun.DisablePastMonthsRows(omat);
                                SAPbouiCOM.DBDataSource ods = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR");
                                Global.objFun.SetByDealer(ofrm, ods);
                                break;
                            }
                    }
                }
                //Last
                else if (!pVal.BeforeAction && pVal.MenuUID == "1291")
                {
                    SAPbouiCOM.Form ofrm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;
                    string formtype = ofrm.UniqueID.ToString();
                    switch (formtype)
                    {
                        case "FIL_FRM_MD_LOCTYPE":
                            {
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                break;
                            }
                        case "FIL_FRM_MD_LOCATION":
                            {
                                SAPbouiCOM.EditText oedtetCode;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_MD_LOCATION");
                                string TyeCode = ofrm.DataSources.DBDataSources.Item("@FIL_MD_LOCATION").GetValue("U_LOTYCODE", 0);

                                if (TyeCode != "")
                                {
                                    string sqlQueryp = string.Format("SELECT B.{0}Code{0},B.{0}Name{0}  from {0}@FIL_MD_LOCTYPE{0} A inner join {0}@FIL_MD_LOCATION{0} B on A.{0}U_PARENT{0}=B.{0}U_LOTYCODE{0} Where A.{0}Code{0}='" + TyeCode + "' ", '"');

                                    SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cmParent").Specific;   //object defining- Define a combo box
                                    Global.objFun.setComboBoxWithoutDescription(ocmbDept, sqlQueryp);
                                }

                                break;
                            }
                        case "FIL_FRM_MD_LOCWEMP":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oedtetCode;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocE").Specific;
                                oedtetDocE.Item.Visible = false;
                                oedtetCode = (SAPbouiCOM.EditText)ofrm.Items.Item("etCode").Specific;
                                oedtetCode.Item.Enabled = false;
                                break;
                            }
                        case "FIL_FRM_DD_SALESTAR":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oeditLocCod;
                                SAPbouiCOM.ComboBox oeditFYR;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                oedtetDocE.Item.Enabled = false;
                                oeditLocCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etLocCod").Specific;
                                oeditLocCod.Item.Enabled = false;
                                oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                oeditFYR.Item.Enabled = false;
                                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;
                                Global.objFun.DisablePastMonthsRows(omat);
                                SAPbouiCOM.DBDataSource ods = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_SALESTAR");
                                Global.objFun.SetByLocation(ofrm, ods);
                                break;
                            }
                        case "FIL_FRM_DD_DEALERTAR":
                            {
                                SAPbouiCOM.EditText oedtetDocE, oeditDerCod;
                                SAPbouiCOM.ComboBox oeditFYR;
                                oedtetDocE = (SAPbouiCOM.EditText)ofrm.Items.Item("etDocNum").Specific;
                                oedtetDocE.Item.Enabled = false;
                                oeditDerCod = (SAPbouiCOM.EditText)ofrm.Items.Item("etDerCod").Specific;
                                oeditDerCod.Item.Enabled = false;
                                oeditFYR = (SAPbouiCOM.ComboBox)ofrm.Items.Item("cbFYR").Specific;
                                oeditFYR.Item.Enabled = false;
                                SAPbouiCOM.Matrix omat = (SAPbouiCOM.Matrix)ofrm.Items.Item("mtxDetls").Specific;
                                Global.objFun.DisablePastMonthsRows(omat);
                                SAPbouiCOM.DBDataSource ods = (SAPbouiCOM.DBDataSource)ofrm.DataSources.DBDataSources.Item("@FIL_DD_DEALERTAR");
                                Global.objFun.SetByDealer(ofrm, ods);
                                break;
                            }
                    }
                }


                else if (pVal.BeforeAction && pVal.MenuUID == "1293")
                {
                    SAPbouiCOM.Form oForm = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;
                    string formtype = oForm.UniqueID.ToString();
                    switch (formtype)
                    {
                        case "FIL_FRM_DH_DELRSCHD":
                            {
                                DeleteSelectedRowAndSyncDraft();
                                break;
                            }
                        //case "FIL_FRM_SCAMEND":
                        //    {
                        //        DeleteSelectedRowAndSyncDraft();
                        //        break;
                        //    }
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

        private void DeleteSelectedRowAndSyncDraft()
        {
            //var oForm = Application.SBO_Application.Forms.ActiveForm;
            //var oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("MTX01").Specific;
            //var ds = oForm.DataSources.DBDataSources.Item("@FIL_DR_DELRSCHD");

            //oForm.Freeze(true);
            //try
            //{
            //    int selRow = oMatrix.GetNextSelectedRow(0, SAPbouiCOM.BoOrderType.ot_RowOrder);
            //    if (selRow <= 0) return;                 // ✅ nothing selected

             
            //    oMatrix.FlushToDataSource();

            //    int dsIndex = selRow - 1;                // ✅ matrix(1..n) -> ds(0..n-1)
            //    if (dsIndex < 0 || dsIndex >= ds.Size) return;

            //    ds.RemoveRecord(dsIndex);

            //    // ✅ Renumber VisOrder (recommended). Avoid changing LineId.
            //    for (int i = 1; i <= ds.Size; i++)
            //    {
            //        ds.SetValue("LineId", i, (i + 1).ToString());
            //    }

            //    oMatrix.LoadFromDataSource();
            //}
            //finally
            //{
            //    oForm.Freeze(false);
            //}
        }




        public bool IsFormOpen(string formUID)
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
        private void CompanyConnection()
        {

            try
            {
                string cookie;
                string connStr;
                // Global.ocomp.
                Global.ocomp = new SAPbobsCOM.Company();
                cookie = Global.ocomp.GetContextCookie();
                //    Global.oCompany = new SAPbobsCOM.Company();
                //   cookie =Global.oCompany.GetContextCookie();
                connStr = Application.SBO_Application.Company.GetConnectionContext(cookie);
                Global.ocomp.SetSboLoginContext(connStr);
                ////   if (Global.CF.IsSAPHANA())
                ////  {
                ////   Global.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB;
                //// }
                //// else
                //// {
                //Global.ocomp.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;
                // }
                // Global.oCompany.Connect();
                Global.G_UI_Application = Application.SBO_Application;
                Global.ocomp = (SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany(); // Reassign the ocomp with the session we conencted with sap b1
                                                                                                       // sErrorMsg = Global.oCompany.GetLastErrorDescription();
                Application.SBO_Application.StatusBar.SetText("Sales Target Addon Connected Successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch
            {
                Application.SBO_Application.MessageBox(Global.ocomp.GetLastErrorDescription().ToString(), 1, "OK", "", "");
            }
        }
        public void CreateMainMenu(string ParentMenuID, string MenuID, string MenuName, int Position, int imenutype, bool flgimg) // POP UP- PARENT
        {
            try
            {
                SAPbouiCOM.Menus oMenus = null; // Define a variable to "menus"
                SAPbouiCOM.MenuItem oMenuItem = null; // Define a variable to MenuItem

                oMenus = Application.SBO_Application.Menus;  // Assign a SAP menu

                SAPbouiCOM.MenuCreationParams oCreationPackage = null;   //Define a variable to menu creating parameter
                oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
                oMenuItem = Application.SBO_Application.Menus.Item(ParentMenuID); // "43520" moudles'  //assign a Parent menu




                switch (imenutype)
                {
                    case 2:
                        {
                            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
                            break;
                        }
                    case 1:
                        {
                            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                            break;
                        }
                    case 3:
                        {
                            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_SEPERATOR;
                            break;
                        }
                }

                oCreationPackage.UniqueID = MenuID;
                oCreationPackage.String = MenuName;
                oCreationPackage.Enabled = true;
                oCreationPackage.Position = Position;  //postion is integer and it start from 0 value

                //string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath).ToString();
                string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath).ToString();
                //string Img = string.Concat(path, @"\BANKREC1.png");
                //oCreationPackage.Image = Img;
                if (flgimg == true)
                {
                    if (MenuID == "FIL_MN_TARGET")
                    {
                        string Bank = string.Concat(path, @"\OJ91D303.JPG");
                        oCreationPackage.Image = Bank;
                    }

                }
                oMenus = oMenuItem.SubMenus;

                try
                {
                    //  If the menu already exists this code will fail
                    oMenus.AddEx(oCreationPackage);
                }
                catch (Exception)
                {

                }
            }
            catch
            {

            }
        }
        private static void AddRow_Item(SAPbouiCOM.Form pForm)
        {
            pForm.Freeze(true);
            SAPbouiCOM.DBDataSource oDataSource = pForm.DataSources.DBDataSources.Item("@FIL_NDR_STOREREQ");
            SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)pForm.Items.Item("mtxDet").Specific;
            oMatrix.FlushToDataSource();
            oDataSource.InsertRecord(oDataSource.Size);
            if (oDataSource.Size > 1)
                for (int i = oDataSource.Size - 2; i >= 0; i--)
                {
                    if (oDataSource.GetValue("U_ITEMCODE", i) == "")
                        oDataSource.RemoveRecord(i);
                    else
                        break;
                }
            for (int i = 0; i < oDataSource.Size; i++)
            {
                oDataSource.SetValue("LineId", i, (i + 1).ToString());
            }
            oMatrix.Clear();
            oMatrix.LoadFromDataSource();
            pForm.Freeze(false);
        }

    }
}
