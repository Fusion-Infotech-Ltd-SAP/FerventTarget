using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Target
{
    [FormAttribute("Target.Location", "Location.b1f")]
    class Location : UserFormBase
    {
        public Location()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("stCode").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("etCode").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("stName").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("etName").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("cmTyeCod").Specific));
            this.ComboBox0.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.ComboBox0_ComboSelectAfter);
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("stLocTye").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("etTypNam").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("stParent").Specific));
            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("cmParent").Specific));
            this.ComboBox1.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.ComboBox1_ComboSelectAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button0.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button0_PressedBefore);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("etDocE").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("stLocTpN").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("stPNam").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("etPNam").Specific));
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

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.ComboBox ComboBox1;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.EditText EditText3;

        private void ComboBox0_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            //throw new System.NotImplementedException();
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
            SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_MD_LOCATION");
            SAPbouiCOM.ComboBox comboBox = (SAPbouiCOM.ComboBox)oform.Items.Item("cmTyeCod").Specific;
            // Get the selected value
            string TyeCode = comboBox.Selected.Value;
            string Name = "";
            if (TyeCode != "")
            {
                SAPbobsCOM.Recordset oRecordset = null;
                oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sqlQuery = string.Format("SELECT A.{0}Code{0},A.{0}Name{0}  from {0}@FIL_MD_LOCTYPE{0} A Where A.{0}Code{0}='" + TyeCode + "' ", '"');
                oRecordset.DoQuery(sqlQuery);
                if(oRecordset.RecordCount > 0)
                {
                    //SAPbouiCOM.EditText oedtTypNam;
                    //oedtTypNam = (SAPbouiCOM.EditText)oform.Items.Item("etTypNam").Specific;
                    Name = oRecordset.Fields.Item("Name").Value.ToString();
                    //oedtTypNam.Value = Name;
                    oDBH.SetValue("U_LOTYNAME", 0, Name);
                }



                string sqlQueryp = string.Format("SELECT B.{0}Code{0},B.{0}Name{0}  from {0}@FIL_MD_LOCTYPE{0} A inner join {0}@FIL_MD_LOCATION{0} B on A.{0}U_PARENT{0}=B.{0}U_LOTYCODE{0} Where A.{0}Code{0}='" + TyeCode + "' ", '"');

                SAPbouiCOM.ComboBox ocmbDept = (SAPbouiCOM.ComboBox)oform.Items.Item("cmParent").Specific;   //object defining- Define a combo box
                Global.objFun.setComboBoxWithoutDescription(ocmbDept, sqlQueryp);
            }

        }

        private void Button0_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
            ValidateForm(ref oform, ref BubbleEvent);

        }
        private bool ValidateForm(ref SAPbouiCOM.Form pForm, ref bool BubbleEvent)
        {
            string code = pForm.DataSources.DBDataSources.Item("@FIL_MD_LOCATION").GetValue("Code", 0);
            if (pForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                if (code == "")
                {
                    BubbleEvent = false;
                    //ShowError("Please select Product No");
                    Application.SBO_Application.StatusBar.SetText("Error : Code not null", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    pForm.ActiveItem = "etCode";
                    return BubbleEvent;
                }
                else
                {
                    SAPbobsCOM.Recordset oRecordset = null;
                    oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string sqlQuery = string.Format("SELECT Count(*) {0}Count{0}  from {0}@FIL_MD_LOCATION{0} A Where A.{0}Code{0}='" + code + "' ", '"');
                    oRecordset.DoQuery(sqlQuery);

                    if (oRecordset.RecordCount > 0)
                    {
                        int Count = Convert.ToInt32(oRecordset.Fields.Item("Count").Value.ToString());
                        if (Count > 0)
                        {
                            BubbleEvent = false;
                            //ShowError("Please select Product No");
                            Application.SBO_Application.StatusBar.SetText("Error : Duplicate Code Not Allowe", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            pForm.ActiveItem = "etCode";
                            return BubbleEvent;
                        }

                    }
                }
            }
            if (pForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                if (code == "")
                {
                    BubbleEvent = false;
                    //ShowError("Please select Product No");
                    Application.SBO_Application.StatusBar.SetText("Error : Code not null", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    pForm.ActiveItem = "etCode";
                    return BubbleEvent;
                }
            }
            return BubbleEvent;
        }
        private SAPbouiCOM.StaticText StaticText5;

        private void ComboBox1_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            //throw new System.NotImplementedException();
            SAPbouiCOM.Form oform = Application.SBO_Application.Forms.Item(pVal.FormUID);
            SAPbouiCOM.DBDataSource oDBH = (SAPbouiCOM.DBDataSource)oform.DataSources.DBDataSources.Item("@FIL_MD_LOCATION");
            SAPbouiCOM.ComboBox comboBox = (SAPbouiCOM.ComboBox)oform.Items.Item("cmParent").Specific;
            // Get the selected value
            string ParentCode = comboBox.Selected.Value;
            string Name = "";
            if (ParentCode != "")
            {
                SAPbobsCOM.Recordset oRecordset = null;
                oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sqlQuery = string.Format("SELECT A.{0}Code{0},A.{0}Name{0}  from {0}@FIL_MD_LOCATION{0} A Where A.{0}Code{0}='" + ParentCode + "' ", '"');
                oRecordset.DoQuery(sqlQuery);
                if (oRecordset.RecordCount > 0)
                {
                    //SAPbouiCOM.EditText oedtTypNam;
                    //oedtTypNam = (SAPbouiCOM.EditText)oform.Items.Item("etTypNam").Specific;
                    Name = oRecordset.Fields.Item("Name").Value.ToString();
                    //oedtTypNam.Value = Name;
                    oDBH.SetValue("U_PARNTNAME", 0, Name);
                }

            }
        }

        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText EditText4;
    }
}
