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
    public class SAPSalesOrder
    {
        public SAPSalesOrder()
        {
            Application.SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
        }

        private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (pVal.FormTypeEx == "139" && pVal.EventType != SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD)
                {
                    SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.GetFormByTypeAndCount(pVal.FormType, pVal.FormTypeCount);
                    int currentPane = oForm.PaneLevel;
                    if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD && pVal.BeforeAction == true)
                    {
                        oForm.PaneLevel = 8;

                        SAPbouiCOM.Item StVAT = oForm.Items.Add("ST_TDIS", SAPbouiCOM.BoFormItemTypes.it_STATIC); // we are going to create
                        SAPbouiCOM.Item osrc = oForm.Items.Item("2000002091");
                        StVAT.Top = osrc.Top + 20;
                        StVAT.Height = osrc.Height;
                        StVAT.Width = osrc.Width;
                        StVAT.Left = osrc.Left;
                        StVAT.FromPane = 8;
                        StVAT.ToPane = 8;

                        //property static field
                        SAPbouiCOM.StaticText osvat = ((SAPbouiCOM.StaticText)(StVAT.Specific));
                        osvat.Caption = "APP Total Discount";

                        SAPbouiCOM.Item CbTDS = oForm.Items.Add("ET_TDIS", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                        SAPbouiCOM.Item orsc1 = oForm.Items.Item("2000002092");
                        CbTDS.Top = orsc1.Top + 20;
                        CbTDS.Height = orsc1.Height;
                        CbTDS.Width = orsc1.Width;
                        CbTDS.Left = orsc1.Left;
                        CbTDS.FromPane = 8;
                        CbTDS.ToPane = 8;
                        CbTDS.Enabled = false;

                        oForm.PaneLevel = 1;

                        //specific property for edit text.
                        SAPbouiCOM.EditText oedtds = (SAPbouiCOM.EditText)(CbTDS.Specific);
                        oedtds.Item.Enabled = false; // to disABLE A FIELD.
                        oedtds.DataBind.SetBound(true, "ORDR", "U_APRTDISC"); //TO SAVE THE VALUE IN TABLE

                        //Total TDS Amount
                        SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;
                        oMatrix.Columns.Item("U_APRVUPRC").Editable = false;
                        oMatrix.Columns.Item("U_APRVDISC").Editable = false;
                    }
                    else if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_LOST_FOCUS
                         && !pVal.BeforeAction
                         && pVal.ItemUID == "4")
                    {
                        try
                        {
                            if (oForm.Items.Item("ET_TDIS").Enabled == false)
                            {
                                oForm.Items.Item("ET_TDIS").Enabled = true;
                            }
                            string GetDiscount = ((SAPbouiCOM.EditText)oForm.Items.Item("24").Specific).Value;
                            ((SAPbouiCOM.EditText)oForm.Items.Item("ET_TDIS").Specific).Value = GetDiscount;
                            if (oForm.Items.Item("ET_TDIS").Enabled == true)
                            {
                                oForm.Items.Item("ET_TDIS").Enabled = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Application.SBO_Application.SetStatusBarMessage("Error - " + ex.ToString(), SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                        }
                    }

                    //---------------->Not Mandatory
                    else if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST
                             && !pVal.BeforeAction
                             && pVal.ItemUID == "4")
                    {
                        SAPbouiCOM.Folder oFolder = (SAPbouiCOM.Folder)oForm.Items.Item("112").Specific;
                        oFolder.Select();
                    }
                    else if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_CLICK
                         && !pVal.BeforeAction
                         && pVal.ItemUID == "2013")
                    {
                        try
                        {
                           if(oForm.Items.Item("ET_TDIS").Enabled == false)
                           {
                                oForm.Items.Item("ET_TDIS").Enabled = true;
                           }
                           string GetDiscount = ((SAPbouiCOM.EditText)oForm.Items.Item("24").Specific).Value;
                           ((SAPbouiCOM.EditText)oForm.Items.Item("ET_TDIS").Specific).Value = GetDiscount;
                            if (oForm.Items.Item("ET_TDIS").Enabled == true)
                            {
                                oForm.Items.Item("ET_TDIS").Enabled = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Application.SBO_Application.SetStatusBarMessage("Error - " + ex.ToString(), SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                        }
                    }
                    //---------------->Not Mandatory

                    else if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_GOT_FOCUS
                        && !pVal.BeforeAction
                        //&& pVal.FormTypeEx == "139" 
                        && pVal.ItemUID == "38"
                        && pVal.ColUID == "1")
                    {
                        try
                        {
                            SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;

                            int rowCount = oMatrix.RowCount;

                            if (rowCount >= 1)
                            {
                                oForm.Freeze(true);
                                int initialitation = pVal.Row;
                                for (int i = initialitation; i <= rowCount; i++)
                                {
                                    string unitPrice = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("14").Cells.Item(i).Specific).Value;
                                    string discount = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("15").Cells.Item(i).Specific).Value;

                                    decimal unitPriceD = 0;
                                    if (unitPrice.Contains("BDT"))
                                    {
                                        unitPrice = unitPrice.Replace("BDT", "").Replace(",", "").Trim();
                                        unitPriceD = Math.Round(Convert.ToDecimal(unitPrice), 4);
                                    }

                                    decimal discountD = 0;
                                    discountD = Math.Round(Convert.ToDecimal(discount), 4);
                                    //if (discount.Contains("BDT"))
                                    //{
                                    //    discount = discount.Replace("BDT", "").Replace(",", "").Trim();

                                    //}

                                    if (oMatrix.Columns.Item("U_APRVUPRC").Editable == false)
                                    {
                                        oMatrix.Columns.Item("U_APRVUPRC").Editable = true;
                                        oMatrix.Columns.Item("U_APRVDISC").Editable = true;
                                    }

                                    //// Set values in matrix(solving Focus Issue)
                                    oMatrix.SetCellWithoutValidation(i, "U_APRVUPRC", unitPriceD.ToString("F4"));
                                    oMatrix.SetCellWithoutValidation(i, "U_APRVDISC", discountD.ToString("F4"));

                                    if (oMatrix.Columns.Item("U_APRVUPRC").Editable == true)
                                    {
                                        oMatrix.Columns.Item("U_APRVUPRC").Editable = false;
                                        oMatrix.Columns.Item("U_APRVDISC").Editable = false;
                                    }
                                }
                                oForm.Freeze(false);
                            }
                        }
                        catch (Exception)
                        {
                            oForm.Freeze(false);
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
