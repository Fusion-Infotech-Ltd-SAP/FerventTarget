using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbouiCOM.Framework;
using System.Runtime.InteropServices;
using Target.Model;


namespace Target
{
    class GlobalFunction
    {
        public bool registerUDO(string UDOCode, string UDOName, SAPbobsCOM.BoUDOObjType UDOType, string[,] findAliasNDescription, string parentTableName, string childTable1 = "", string childTable2 = "", string childTable3 = "", string childTable4 = "", string childTable5 = "", string childTable6 = "", string childTable7 = "", string childTable8 = "", SAPbobsCOM.BoYesNoEnum LogOption = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum DefFormOption = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum MenuItem = SAPbobsCOM.BoYesNoEnum.tNO, string MenuCaption = "", string FatherMenuId = "", int Position = 0)
        {
            bool registerUDORet = false;
            try
            {
                registerUDORet = false;

                SAPbobsCOM.UserObjectsMD v_udoMD;
                v_udoMD = (SAPbobsCOM.UserObjectsMD)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);
                //update part
                //if(v_udoMD.GetByKey("Udo1"))
                //{

                //}
                //  v_udoMD = Global.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);
                v_udoMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tNO;
                v_udoMD.CanClose = SAPbobsCOM.BoYesNoEnum.tNO;
                v_udoMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO;
                v_udoMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.CanLog = LogOption;
                v_udoMD.CanYearTransfer = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.ManageSeries = SAPbobsCOM.BoYesNoEnum.tYES;
                v_udoMD.Code = UDOCode;
                v_udoMD.Name = UDOName;
                v_udoMD.TableName = parentTableName;
                if (LogOption == SAPbobsCOM.BoYesNoEnum.tYES)
                {
                    v_udoMD.LogTableName = "A" + parentTableName;
                }

                if (DefFormOption == SAPbobsCOM.BoYesNoEnum.tYES)
                {

                    v_udoMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tYES;
                    v_udoMD.MenuItem = SAPbobsCOM.BoYesNoEnum.tYES;
                    v_udoMD.MenuCaption = MenuCaption;
                    v_udoMD.FatherMenuID = Convert.ToInt32(FatherMenuId);
                    v_udoMD.Position = Position;
                }

                v_udoMD.ObjectType = UDOType;
                for (int i = 0, loopTo = findAliasNDescription.GetLength(0) - 1; i <= loopTo; i++)
                {
                    if (i > 0)
                        v_udoMD.FindColumns.Add();
                    v_udoMD.FindColumns.ColumnAlias = findAliasNDescription[i, 0];
                    v_udoMD.FindColumns.ColumnDescription = findAliasNDescription[i, 1];
                }

                if (!string.IsNullOrEmpty(childTable1))
                {
                    v_udoMD.ChildTables.TableName = childTable1;
                    v_udoMD.ChildTables.Add();
                }

                if (!string.IsNullOrEmpty(childTable2))
                {
                    v_udoMD.ChildTables.TableName = childTable2;
                    v_udoMD.ChildTables.Add();
                }

                if (!string.IsNullOrEmpty(childTable3))
                {
                    v_udoMD.ChildTables.TableName = childTable3;
                    v_udoMD.ChildTables.Add();
                }

                if (!string.IsNullOrEmpty(childTable4))
                {
                    v_udoMD.ChildTables.TableName = childTable4;
                    v_udoMD.ChildTables.Add();
                }
                if (!string.IsNullOrEmpty(childTable5))
                {
                    v_udoMD.ChildTables.TableName = childTable5;
                    v_udoMD.ChildTables.Add();
                }
                if (!string.IsNullOrEmpty(childTable6))
                {
                    v_udoMD.ChildTables.TableName = childTable6;
                    v_udoMD.ChildTables.Add();
                }
                if (!string.IsNullOrEmpty(childTable7))
                {
                    v_udoMD.ChildTables.TableName = childTable7;
                    v_udoMD.ChildTables.Add();
                }
                if (!string.IsNullOrEmpty(childTable8))
                {
                    v_udoMD.ChildTables.TableName = childTable8;
                    v_udoMD.ChildTables.Add();
                }
                //update alon
                //if(v_udoMD.Update()==0)
                //{

                //}

                if (v_udoMD.Add() == 0)
                {
                    registerUDORet = true;
                    Application.SBO_Application.StatusBar.SetText("Successfully Registered UDO >" + UDOCode + ">" + UDOName + ".", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                }
                else
                {
                    //  string str = Global.oCompany.GetLastErrorDescription().ToString();
                    //  Application.SBO_Application.StatusBar.SetText("Failed to Register UDO >" + UDOCode + ">" + UDOName + " >" + Global.oCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    registerUDORet = false;
                }

                Marshal.ReleaseComObject(v_udoMD);
                //    v_udoMD = default;
                GC.Collect();
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
            }

            return registerUDORet;
        }

        public bool CreateTable(string TableName, string TableDescription, SAPbobsCOM.BoUTBTableType TableType)
        {
            bool RET;
            int intRetCode;
            SAPbobsCOM.UserTablesMD objUserTableMD;  // this is achived through DI API - DATA INTERFACE-  APPLICATION PROGRAMMING INTERFACE
            objUserTableMD = (SAPbobsCOM.UserTablesMD)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
            //  objUserTableMD = Global.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
            try
            {
                if (!objUserTableMD.GetByKey(TableName))
                {
                    objUserTableMD.TableName = TableName;
                    objUserTableMD.TableDescription = TableDescription;
                    objUserTableMD.TableType = TableType;
                    objUserTableMD.Archivable = SAPbobsCOM.BoYesNoEnum.tNO;


                    intRetCode = objUserTableMD.Add();
                    if (intRetCode == 0)
                    {
                        //   ShowSuccessMessage(TableName + " Table Created Successfully.");
                        RET = true;
                    }
                    else
                    {
                        //  ShowSuccessMessage(TableName + " Table Not Created Successfully.");
                        RET = false;
                    }
                }
                else
                {
                    RET = false;
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(TableName + " Table is not Created Successfully." + ex.Message);
            }
            finally
            {
                Marshal.ReleaseComObject(objUserTableMD);
                GC.Collect();
            }

            return false;
        }
        //SAME FUNCTION FOR LOAD A coMBOBOX AT RUNTIME USING QUERY METHOD
        public bool setComboBoxValue(SAPbouiCOM.ComboBox oComboBox, string strQry)
        {
            bool flag;
            try
            {

                int count = oComboBox.ValidValues.Count;//0
                if (count > 0)
                {
                    while (true)
                    {
                        if (count <= 0)
                        {
                            break;
                        }
                        oComboBox.ValidValues.Remove(count - 1, SAPbouiCOM.BoSearchKey.psk_Index);
                        count--;
                    }
                }
                //IN VS-CORE DOTNET WE USE DATASET- AS LIKE SAME FUNCTIONALITY, IT WILL USING RECORDSET IN SAP
                SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string str = Convert.ToString(oComboBox.ValidValues.Count);
                if (oComboBox.ValidValues.Count == 0)
                {

                    businessObject.DoQuery(strQry); //doquery means- executinga query
                    businessObject.MoveFirst();
                    int num2 = businessObject.RecordCount - 1; //linelevel count 
                    int num = 0;
                    while (true)
                    {
                        int num3 = num2;
                        if (num > num3)
                        {
                            break;
                        }
                        oComboBox.ValidValues.Add(Convert.ToString(businessObject.Fields.Item(0).Value), Convert.ToString(businessObject.Fields.Item(1).Value));

                        businessObject.MoveNext(); // it will move to next cursor to recordset
                        num++;
                    }
                }
                oComboBox.ExpandType = SAPbouiCOM.BoExpandType.et_ValueDescription;
                oComboBox.Item.DisplayDesc = true;
                flag = true;
            }
            catch (Exception exception1)
            {

                Application.SBO_Application.StatusBar.SetText("setComboBoxValue Function Failed:" + exception1.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                flag = true;

            }
            return flag;
        }
        public bool setComboBoxWithoutDescription(SAPbouiCOM.ComboBox oComboBox, string strQry)
        {
            bool flag;
            try
            {

                int count = oComboBox.ValidValues.Count;//0
                if (count > 0)
                {
                    while (true)
                    {
                        if (count <= 0)
                        {
                            break;
                        }
                        oComboBox.ValidValues.Remove(count - 1, SAPbouiCOM.BoSearchKey.psk_Index);
                        count--;
                    }
                }
                //IN VS-CORE DOTNET WE USE DATASET- AS LIKE SAME FUNCTIONALITY, IT WILL USING RECORDSET IN SAP
                SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string str = Convert.ToString(oComboBox.ValidValues.Count);
                if (oComboBox.ValidValues.Count == 0)
                {

                    businessObject.DoQuery(strQry); //doquery means- executinga query
                    businessObject.MoveFirst();
                    int num2 = businessObject.RecordCount - 1; //linelevel count 
                    int num = 0;
                    while (true)
                    {
                        int num3 = num2;
                        if (num > num3)
                        {
                            break;
                        }
                        oComboBox.ValidValues.Add(Convert.ToString(businessObject.Fields.Item(0).Value), Convert.ToString(businessObject.Fields.Item(1).Value));

                        businessObject.MoveNext(); // it will move to next cursor to recordset
                        num++;
                    }
                }
                oComboBox.ExpandType = SAPbouiCOM.BoExpandType.et_ValueDescription;
                //oComboBox.Item.DisplayDesc = true;
                flag = true;
            }
            catch (Exception exception1)
            {

                Application.SBO_Application.StatusBar.SetText("setComboBoxValue Function Failed:" + exception1.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                flag = true;

            }
            return flag;
        }
        public int GetCodeGeneration(string TableName)
        {
            int num;
            try
            {
                SAPbobsCOM.Recordset businessObject = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                // businessObject.DoQuery("Select IFNULL(Max(IFNULL(\"DocEntry\",0)),0) + 1 Code From \"" + Global.oCompany.CompanyDB + "\".\"" + TableName.Trim().ToString().Replace("[", "").Replace("]", "") + "\"");
                //   if (Global.CF.IsSAPHANA() == true)
                //  {
                //     businessObject.DoQuery(@"Select IFNULL(Max(IFNULL(""DocEntry"",0)),0) + 1 Code From " + TableName.Trim().ToString());
                //  }
                //  else
                //  {
                string sqlQuery = string.Format("SELECT ifnull(Max({0}DocEntry{0}),0) + 1 as  {0}Code{0} from {0}" + TableName.Trim().ToString() + "{0}", '"');
                businessObject.DoQuery(sqlQuery);
                //  }
                //num=Convert.ToInt32(businessObject.Fields.Item("Code").Value)-vb.net
                num = Convert.ToInt32(businessObject.Fields.Item("Code").Value.ToString());//c# -we need to convert from object to String , then able to change whatver type of data required.

            }
            catch (Exception exception1)
            {

                Application.SBO_Application.StatusBar.SetText("GetCodeGeneration Function Failed:" + exception1.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                num = -1;

            }
            return num;
        }
        public void ShowError(string ErrorMessage)
        {
            Application.SBO_Application.StatusBar.SetText(ErrorMessage, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
        }
        public void ShowSuccess(string ErrorMessage)
        {
            Application.SBO_Application.StatusBar.SetText(ErrorMessage, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
        }

        public void addField(string TableName, string ColumnName, string ColDescription, SAPbobsCOM.BoFieldTypes FieldType, int Size, SAPbobsCOM.BoFldSubTypes SubType, string ValidValues, string ValidDescriptions, string SetValidValue)
        {
            int intLoop;
            //Array strValue, strDesc;
            string[] strValue, strDesc;
            SAPbobsCOM.UserFieldsMD objUserFieldMD; //Declare a variable which supports udf creation,userdefined object of sap b1
            objUserFieldMD = (SAPbobsCOM.UserFieldsMD)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields); //assigning user defined object
            // objUserFieldMD = Global.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
            try
            {
                //"1,2,3,4,5"->value
                //"Apple,Oppo,vivo,samsung"
                strValue = ValidValues.Split(Convert.ToChar(","));
                strDesc = ValidDescriptions.Split(Convert.ToChar(","));
                if (strValue.GetLength(0) != strDesc.GetLength(0))
                {
                    throw new Exception("Invalid Valid Values");
                }

                if (!isColumnExist(TableName, ColumnName))
                {

                    objUserFieldMD.TableName = TableName;
                    objUserFieldMD.Name = ColumnName;
                    objUserFieldMD.Description = ColDescription;
                    objUserFieldMD.Type = FieldType;
                    if (FieldType != SAPbobsCOM.BoFieldTypes.db_Numeric)
                    {
                        objUserFieldMD.Size = Size;
                    }
                    else
                    {
                        objUserFieldMD.EditSize = Size;
                    }

                    objUserFieldMD.SubType = SubType;
                    if (strValue.Length > 1)
                    {
                        var loopTo = strValue.GetLength(0) - 1;
                        for (intLoop = 0; intLoop <= loopTo; intLoop++)
                        {
                            objUserFieldMD.ValidValues.Value = strValue[intLoop];
                            objUserFieldMD.ValidValues.Description = strDesc[intLoop];
                            objUserFieldMD.ValidValues.Add();
                        }

                        // objUserFieldMD.DefaultValue = SetValidValue;
                    }
                    else if (SetValidValue.Length > 0)
                    {
                        objUserFieldMD.DefaultValue = SetValidValue;
                    }
                    // objUserFieldMD.LinkedTable=""
                    //objUserFieldMD.LinkedUDO=""
                    //  objUserFieldMD.
                    // objUserFieldMD.Browser

                    if (objUserFieldMD.Add() != 0)
                    {
                        Application.SBO_Application.StatusBar.SetText(Global.ocomp.GetLastErrorDescription());
                    }
                    else
                    {
                        Application.SBO_Application.StatusBar.SetText(objUserFieldMD.Name + " Created Successfully.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_None);
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
            }
            finally
            {
                Marshal.ReleaseComObject(objUserFieldMD);
                GC.Collect(); //temp file 
            }
        }
        public void DeleteRow(SAPbouiCOM.Matrix oMatrix, SAPbouiCOM.DBDataSource oDBDSDetail)
        {
            try
            {
                oMatrix.FlushToDataSource();
                int visualRowCount = oMatrix.VisualRowCount; //5
                int rowNum = 1;
                while (true)
                {
                    int num3 = visualRowCount; //5
                    if (rowNum >= num3) //5=>5
                    {
                        oDBDSDetail.RemoveRecord(oDBDSDetail.Size - 1);
                        oMatrix.LoadFromDataSource();
                        break;
                    }

                    oMatrix.GetLineData(rowNum);
                    oDBDSDetail.Offset = rowNum - 1;
                    oDBDSDetail.SetValue("LineId", oDBDSDetail.Offset, Convert.ToString(rowNum));
                    oMatrix.SetLineData(rowNum);
                    oMatrix.FlushToDataSource();
                    rowNum++;
                }
                //1Item1
                //2Item2
                //3Item3
                //4Iten5
            }
            catch (Exception exception1)
            {

                Application.SBO_Application.StatusBar.SetText("DeleteRow  Method Failed:" + exception1.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

            }
        }

        private bool isColumnExist(string TableName, string ColumnName)
        {

            // RECORD SET - TO HOLD THE COLLECTION OF DATA-WHICH EXECUTED FROM SQL OR HANA DATABASES / DATA SERVER
            SAPbobsCOM.Recordset objRecordSet;
            string strTemp = "";
            objRecordSet = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            // objRecordSet = Global.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            try
            {
                strTemp = "SELECT COUNT(*) FROM CUFD WHERE  \"TableID\" = '" + TableName + "' AND  \"AliasID\"  = '" + ColumnName + "'";
                //if (Global.CF.IsSAPHANA() == false)
                //{
                //    strTemp = "SELECT COUNT(*) FROM CUFD WHERE   TableID = '" + TableName + "' AND AliasID = '" + ColumnName + "'";
                //}
                //else
                //{
                //    strTemp = "SELECT COUNT(*) FROM CUFD WHERE  \"TableID\" = '" + TableName + "' AND  \"AliasID\"  = '" + ColumnName + "'";
                //}
                //   SAPbobsCOM RECORSET.DOQUERY(PASS THE QUERY) // SYNTX FOR CALLING THE RECORDESET AND EXECUTION OF QUERIES
                objRecordSet.DoQuery(strTemp); //EXECTION OF QUERY STATEMENT

                if (Convert.ToInt16(objRecordSet.Fields.Item(0).Value) == 0)  // CONVERISION IS TAKES BECAUSE RECORD IS TYPE OF OBJECT
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Marshal.ReleaseComObject(objRecordSet);
                GC.Collect();
            }
        }
        SAPbouiCOM.EditText omatcol;
        public void SetNewLine(SAPbouiCOM.Matrix oMatrix, SAPbouiCOM.DBDataSource oDBDSDetail, int RowID = 1, string ColumnUID = "")
        {
            try
            {

                if (ColumnUID != "")
                {
                    omatcol = (SAPbouiCOM.EditText)oMatrix.Columns.Item(ColumnUID).Cells.Item(RowID).Specific;
                }

                if (ColumnUID.Equals(""))  //no column assign ; eventhough no values exist in previous column then also can add new lines.
                {
                    oMatrix.FlushToDataSource();
                    oMatrix.AddRow(1, -1);
                    oDBDSDetail.InsertRecord(oDBDSDetail.Size);
                    oDBDSDetail.Offset = oMatrix.VisualRowCount - 1;
                    oDBDSDetail.SetValue("LineId", oDBDSDetail.Offset, Convert.ToString(oMatrix.VisualRowCount));
                    oMatrix.SetLineData(oMatrix.VisualRowCount);
                    oMatrix.FlushToDataSource();
                }
                else if (oMatrix.VisualRowCount <= 0)  //1st time row creation
                {
                    oMatrix.FlushToDataSource();
                    oMatrix.AddRow(1, -1);//1-1=4
                    oDBDSDetail.InsertRecord(oDBDSDetail.Size);
                    oDBDSDetail.Offset = oMatrix.VisualRowCount - 1; //starting index from 0
                    oDBDSDetail.SetValue("LineId", oDBDSDetail.Offset, Convert.ToString(oMatrix.VisualRowCount));
                    oMatrix.SetLineData(oMatrix.VisualRowCount);
                    oMatrix.FlushToDataSource();
                }
                else if (!(omatcol.Value).Equals("") && (RowID == oMatrix.VisualRowCount))  // column assigned ; only add a row when present column value is not null.
                {
                    oMatrix.FlushToDataSource();
                    oMatrix.AddRow(1, -1);
                    oDBDSDetail.InsertRecord(oDBDSDetail.Size);
                    oDBDSDetail.Offset = oMatrix.VisualRowCount - 1;
                    oDBDSDetail.SetValue("LineId", oDBDSDetail.Offset, Convert.ToString(oMatrix.VisualRowCount));
                    oMatrix.SetLineData(oMatrix.VisualRowCount);
                    //oMatrix.LoadFromDataSource();
                    oMatrix.FlushToDataSource();
                }
            }
            catch (Exception)
            {

            }
        }

        //private static void AddRow(ref Form pForm)
        //{
        //    Matrix oMatrix = (Matrix)pForm.Items.Item("MTX_01").Specific;
        //    DBDataSource oDataSource = pForm.DataSources.DBDataSources.Item("@FFS_MR_SLSTRGET");
        //    oMatrix.FlushToDataSource();
        //    if (oDataSource.Size > 0)
        //    {
        //        if (oDataSource.GetValue("U_ITEMCODE", oDataSource.Size - 1) == "")
        //            oDataSource.RemoveRecord(oDataSource.Size - 1);
        //    }
        //    oDataSource.InsertRecord(oDataSource.Size);
        //    oDataSource.Offset = oDataSource.Size - 1;
        //    oDataSource.SetValue("U_VISORDER", oDataSource.Offset, oDataSource.Size.ToString());
        //    oMatrix.Clear();
        //    oMatrix.LoadFromDataSource();
        //    oMatrix.AutoResizeColumns();
        //}

        public bool LoadSeriesAndSetDocNum(SAPbouiCOM.Form oForm, string comboItemId, string udoId, string dbDataSource, DateTime? postingDate = null)
        {
            SAPbouiCOM.ComboBox oCombo = null;
            SAPbobsCOM.Recordset rs = null;

            try
            {
                oForm.Freeze(true);

                try
                {
                    oCombo = (SAPbouiCOM.ComboBox)oForm.Items.Item(comboItemId).Specific;
                }
                catch (Exception ex)
                {
                    Application.SBO_Application.SetStatusBarMessage($"Error: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Long, true);
                }

                DateTime postDate = postingDate ?? DateTime.Today;
                string date = postDate.ToString("yyyyMMdd");

                oCombo.ExpandType = SAPbouiCOM.BoExpandType.et_DescriptionOnly;
                oCombo.Item.DisplayDesc = true;

                while (oCombo.ValidValues.Count > 0)
                {
                    oCombo.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
                }

                oCombo.ValidValues.LoadSeries(udoId, SAPbouiCOM.BoSeriesMode.sf_View);

                rs = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                rs.DoQuery(string.Format(@"
                            SELECT TOP 1 T1.""Series""
                            FROM ""OFPR"" T0
                            INNER JOIN ""NNM1"" T1
                                ON T0.""Indicator"" = T1.""Indicator""
                            WHERE '{0}' BETWEEN T0.""F_RefDate"" AND T0.""T_RefDate""
                              AND T1.""ObjectCode"" = '{1}'
                              AND T1.""Locked"" = 'N'
                            ORDER BY T0.""AbsEntry"" DESC, T1.""Series""",
                                    date,
                                    udoId.Replace("'", "''")));

                if (rs.EoF)
                {
                    Application.SBO_Application.SetStatusBarMessage("No active series found.", SAPbouiCOM.BoMessageTime.bmt_Long, true);
                    return false;
                }
                string seriesValue = rs.Fields.Item("Series").Value.ToString();

                try
                {
                    oCombo.Select(seriesValue, SAPbouiCOM.BoSearchKey.psk_ByValue);
                }
                catch (Exception ex)
                {
                    Application.SBO_Application.SetStatusBarMessage($"Series exists in database but could not be loaded into the ComboBox. {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Long, true);
                    return false;
                }

                long nextDocNo = oForm.BusinessObject.GetNextSerialNumber(seriesValue, udoId);
                oForm.DataSources.DBDataSources.Item(dbDataSource).SetValue("DocNum", 0, nextDocNo.ToString());

                return true;
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                return false;
            }
            finally
            {
                oForm.Freeze(false);
            }
        }
        //public bool LoadComboBoxSeries(SAPbouiCOM.ComboBox oComboBox, string UDOID)  // tow generate a series in document type UDO.- paarmeter will be combobox and the UDO ID.
        //{
        //    bool flag;
        //    try
        //    {
        //        oComboBox.ExpandType = SAPbouiCOM.BoExpandType.et_DescriptionOnly;
        //        oComboBox.ValidValues.LoadSeries(UDOID, SAPbouiCOM.BoSeriesMode.sf_Add);  // ONLY TO LOAD A COMBOBOX
        //        oComboBox.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
        //        oComboBox.Item.DisplayDesc = true;
        //        flag = true;
        //    }
        //    catch (Exception)
        //    {
        //        Application.SBO_Application.SetStatusBarMessage("error");
        //        flag = false;

        //    }
        //    return flag;
        //}
        public void DisablePastMonthsRows(SAPbouiCOM.Matrix omatrix)
        {
            try
            {
                int currentMonth = DateTime.Now.Month; // Get current month (1=Jan, 2=Feb, ... 12=Dec)
                currentMonth = currentMonth + 6;
                int mSjul = 1; int mSaug = 2; int mSsep = 3; int mSoct = 4; int mSnov = 5; int mSdec = 6;
                int mSjan = 7; int mSfeb = 8; int mSmar = 9; int mSapr = 10; int mSmay = 11; int mSjun = 12;
                int loggedInUser2 = Global.ocomp.UserSignature;
                //for (int i = 1; i <= omatrix.VisualRowCount; i++) // Loop through matrix rows
                //{
                if (loggedInUser2 != 1)
                {
                    if (mSjan <= currentMonth)
                    {
                        omatrix.Columns.Item("mSjan").Editable = false; // Disable column
                        omatrix.Columns.Item("mCjan").Editable = false;
                    }
                    if (mSfeb <= currentMonth)
                    {
                        omatrix.Columns.Item("mSfeb").Editable = false;
                        omatrix.Columns.Item("mCfeb").Editable = false;
                    }
                    if (mSmar <= currentMonth)
                    {
                        omatrix.Columns.Item("mSmar").Editable = false;
                        omatrix.Columns.Item("mCmar").Editable = false;
                    }
                    if (mSapr <= currentMonth)
                    {
                        omatrix.Columns.Item("mSapr").Editable = false;
                        omatrix.Columns.Item("mCapr").Editable = false;
                    }
                    if (mSmay <= currentMonth)
                    {
                        omatrix.Columns.Item("mSmay").Editable = false;
                        omatrix.Columns.Item("mCmay").Editable = false;
                    }
                    if (mSjun <= currentMonth)
                    {
                        omatrix.Columns.Item("mSjun").Editable = false;
                        omatrix.Columns.Item("mCjun").Editable = false;
                    }
                    if (mSjul <= currentMonth)
                    {
                        omatrix.Columns.Item("mSjul").Editable = false;
                        omatrix.Columns.Item("mCjul").Editable = false;
                    }
                    if (mSaug <= currentMonth)
                    {
                        omatrix.Columns.Item("mSaug").Editable = false;
                        omatrix.Columns.Item("mCaug").Editable = false;
                    }
                    if (mSsep <= currentMonth)
                    {
                        omatrix.Columns.Item("mSsep").Editable = false;
                        omatrix.Columns.Item("mCsep").Editable = false;
                    }
                    if (mSoct <= currentMonth)
                    {
                        omatrix.Columns.Item("mSoct").Editable = false;
                        omatrix.Columns.Item("mCoct").Editable = false;
                    }
                    if (mSnov <= currentMonth)
                    {
                        omatrix.Columns.Item("mSnov").Editable = false;
                        omatrix.Columns.Item("mCnov").Editable = false;
                    }
                    if (mSdec <= currentMonth)
                    {
                        omatrix.Columns.Item("mSdec").Editable = false;
                        omatrix.Columns.Item("mCdec").Editable = false;
                    }
                }

                //}
            }
            catch (Exception ex)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Error: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }
        public void EnablePastMonthsRows(SAPbouiCOM.Matrix omatrix)
        {
            try
            {
                int currentMonth = DateTime.Now.Month; // Get current month (1=Jan, 2=Feb, ... 12=Dec)
                currentMonth = currentMonth + 6;
                int mSjul = 1; int mSaug = 2; int mSsep = 3; int mSoct = 4; int mSnov = 5; int mSdec = 6;
                int mSjan = 7; int mSfeb = 8; int mSmar = 9; int mSapr = 10; int mSmay = 11; int mSjun = 12;
                int loggedInUser2 = Global.ocomp.UserSignature;
                //for (int i = 1; i <= omatrix.VisualRowCount; i++) // Loop through matrix rows
                //{
                if (loggedInUser2 != 1)
                {
                    if (mSjan <= currentMonth)
                    {
                        omatrix.Columns.Item("mSjan").Editable = true; // Disable column
                        omatrix.Columns.Item("mCjan").Editable = true;
                    }
                    if (mSfeb <= currentMonth)
                    {
                        omatrix.Columns.Item("mSfeb").Editable = true;
                        omatrix.Columns.Item("mCfeb").Editable = true;
                    }
                    if (mSmar <= currentMonth)
                    {
                        omatrix.Columns.Item("mSmar").Editable = true;
                        omatrix.Columns.Item("mCmar").Editable = true;
                    }
                    if (mSapr <= currentMonth)
                    {
                        omatrix.Columns.Item("mSapr").Editable = true;
                        omatrix.Columns.Item("mCapr").Editable = true;
                    }
                    if (mSmay <= currentMonth)
                    {
                        omatrix.Columns.Item("mSmay").Editable = true;
                        omatrix.Columns.Item("mCmay").Editable = true;
                    }
                    if (mSjun <= currentMonth)
                    {
                        omatrix.Columns.Item("mSjun").Editable = true;
                        omatrix.Columns.Item("mCjun").Editable = true;
                    }
                    if (mSjul <= currentMonth)
                    {
                        omatrix.Columns.Item("mSjul").Editable = true;
                        omatrix.Columns.Item("mCjul").Editable = true;
                    }
                    if (mSaug <= currentMonth)
                    {
                        omatrix.Columns.Item("mSaug").Editable = true;
                        omatrix.Columns.Item("mCaug").Editable = true;
                    }
                    if (mSsep <= currentMonth)
                    {
                        omatrix.Columns.Item("mSsep").Editable = true;
                        omatrix.Columns.Item("mCsep").Editable = true;
                    }
                    if (mSoct <= currentMonth)
                    {
                        omatrix.Columns.Item("mSoct").Editable = true;
                        omatrix.Columns.Item("mCoct").Editable = true;
                    }
                    if (mSnov <= currentMonth)
                    {
                        omatrix.Columns.Item("mSnov").Editable = true;
                        omatrix.Columns.Item("mCnov").Editable = true;
                    }
                    if (mSdec <= currentMonth)
                    {
                        omatrix.Columns.Item("mSdec").Editable = true;
                        omatrix.Columns.Item("mCdec").Editable = true;
                    }
                }

                //}
            }
            catch (Exception ex)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Error: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }
        public void Refresh(SAPbouiCOM.Form pForm)
        {
            SAPbouiCOM.EditText oSlpCode = (SAPbouiCOM.EditText)pForm.Items.Item("etSlpCod").Specific;
            SAPbouiCOM.EditText oSlpName = (SAPbouiCOM.EditText)pForm.Items.Item("etSlpNam").Specific;
            SAPbouiCOM.EditText oASM = (SAPbouiCOM.EditText)pForm.Items.Item("etASM").Specific;
            SAPbouiCOM.EditText oRSM = (SAPbouiCOM.EditText)pForm.Items.Item("etRSM").Specific;
            oSlpCode.Value = "";
            oSlpName.Value = "";
            oASM.Value = "";
            oRSM.Value = "";
        }
        public void RefreshDealer(SAPbouiCOM.Form pForm)
        {
            SAPbouiCOM.EditText oSlpCode = (SAPbouiCOM.EditText)pForm.Items.Item("etSlpCod").Specific;
            SAPbouiCOM.EditText oSlpName = (SAPbouiCOM.EditText)pForm.Items.Item("etSlpNam").Specific;
            oSlpCode.Value = "";
            oSlpName.Value = "";
        }
        public void SetByLocation(SAPbouiCOM.Form pForm, SAPbouiCOM.DBDataSource oDBH)
        {
           string strCode= oDBH.GetValue("U_LOCCODE", 0);
            SAPbobsCOM.Recordset oRecordset = null;
            oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string sqlQuery = string.Format("select A.{0}Code{0},A.{0}Name{0},B.{0}U_EMPID{0},B.{0}U_EMPNAME{0},C.{0}Name{0} || '-' || C.{0}Code{0}   {0}ASM{0},D.{0}Name{0} || '-' || D.{0}Code{0}   {0}RSM{0} from {0}@FIL_MD_LOCATION{0} A inner join {0}@FIL_MD_LOCWEMP{0} B on A.{0}Code{0} = B.{0}U_LOCCODE{0} and B.{0}U_STATUS{0} = 'Y' inner join {0}@FIL_MD_LOCATION{0} C on A.{0}U_PARENT{0} = C.{0}Code{0} inner join {0}@FIL_MD_LOCATION{0} D on C.{0}U_PARENT{0} = D.{0}Code{0} where A.{0}Code{0} = '" + strCode + "' ", '"');

            oRecordset.DoQuery(sqlQuery);
            SAPbouiCOM.EditText oSlpCode = (SAPbouiCOM.EditText)pForm.Items.Item("etSlpCod").Specific;
            SAPbouiCOM.EditText oSlpName = (SAPbouiCOM.EditText)pForm.Items.Item("etSlpNam").Specific;
            SAPbouiCOM.EditText oASM = (SAPbouiCOM.EditText)pForm.Items.Item("etASM").Specific;
            SAPbouiCOM.EditText oRSM = (SAPbouiCOM.EditText)pForm.Items.Item("etRSM").Specific;
            if (oRecordset.RecordCount > 0)
            {
                oSlpCode.Value = oRecordset.Fields.Item("U_EMPID").Value.ToString();
                oSlpName.Value = oRecordset.Fields.Item("U_EMPNAME").Value.ToString();
                oASM.Value = oRecordset.Fields.Item("ASM").Value.ToString();
                oRSM.Value = oRecordset.Fields.Item("RSM").Value.ToString();
            }
        }
        public void SetByDealer(SAPbouiCOM.Form pForm, SAPbouiCOM.DBDataSource oDBH)
        {
            string strCode = oDBH.GetValue("U_CARDCODE", 0);
            SAPbobsCOM.Recordset oRecordset = null;
            oRecordset = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string sqlQuery = string.Format("select A.{0}CardCode{0},A.{0}CardName{0},B.{0}Code{0} {0}AreaCode{0},B.{0}Name{0} {0}AreaName{0},C.{0}U_EMPID{0}  {0}SlpCode{0},C.{0}U_EMPNAME{0} {0}SlpName{0} from {0}OCRD{0} A inner join {0}@FIL_MD_LOCATION{0} B on A.{0}U_LOCCODE{0} = B.{0}Code{0}  inner join {0}@FIL_MD_LOCWEMP{0} C on B.{0}Code{0} = C.{0}U_LOCCODE{0} and C.{0}U_STATUS{0} = 'Y' where A.{0}CardCode{0} = '" + strCode + "' ", '"');

            oRecordset.DoQuery(sqlQuery);
            SAPbouiCOM.EditText oSlpCode = (SAPbouiCOM.EditText)pForm.Items.Item("etSlpCod").Specific;
            SAPbouiCOM.EditText oSlpName = (SAPbouiCOM.EditText)pForm.Items.Item("etSlpNam").Specific;
            if (oRecordset.RecordCount > 0)
            {
                oSlpCode.Value = oRecordset.Fields.Item("SlpCode").Value.ToString();
                oSlpName.Value = oRecordset.Fields.Item("SlpName").Value.ToString();
            }
        }

        public AccessFileViewModel GetAccessFile(string Project)
        {
            AccessFileViewModel accessFVM;

            SAPbobsCOM.Recordset rtc = (SAPbobsCOM.Recordset)Global.ocomp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            //  string Project = "FERVENT_ERPNext";

            string sqlUname = string.Format("SELECT {0}U_IPPORT{0} {0}IPPort{0},{0}Name{0} {0}Token{0} FROM {0}@FIL_TOKEN{0} WHERE {0}U_PROJECT{0} = '" + Project + "'", '"');
            rtc.DoQuery(sqlUname);


            return accessFVM = (new AccessFileViewModel
            {
                //IPPort = "http://103.238.155.192/"
                //                ,
                //Authorization = "token e225ce004aaf20b:4e3c871b2e4f71a"token 3b5f85f6d1f6f43:cc4fd3ade8b6f78
                // Authorization = "token 3b5f85f6d1f6f43:fb0916603dd6b7c"
                IPPort = rtc.Fields.Item("IPPort").Value.ToString()
                ,
                Authorization = rtc.Fields.Item("Token").Value.ToString()
            });
        }
    }
}
