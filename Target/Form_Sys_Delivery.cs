using System;
using System.Collections.Generic;
using System.Linq;
using SAPbouiCOM.Framework;

namespace Target
{
   
    class Form_Sys_Delivery
    {
        public Form_Sys_Delivery()
        {
           Application.SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
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


                    else if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST  && pVal.ItemUID == "STSCDBTN")
                    {

                        if(pVal.BeforeAction == true)
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
    
    }
}
