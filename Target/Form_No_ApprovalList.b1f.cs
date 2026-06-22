using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Target
{
    [FormAttribute("Target.Form_No_ApprovalList", "Form_No_ApprovalList.b1f")]
    class Form_No_ApprovalList : UserFormBase
    {
        public Form_No_ApprovalList()
        {
        }

        private SAPbouiCOM.Button Ok;
        private SAPbouiCOM.Grid GRID01;

        public override void OnInitializeComponent()
        {
            this.GRID01 = ((SAPbouiCOM.Grid)(this.GetItem("GRID01").Specific));
            this.Ok = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.OnCustomInitialize();

        }

        internal static void ApprovalList(ref SAPbouiCOM.Form pForm, string strDocEntry)
        {
            try
            {
                SAPbouiCOM.Grid oGrid = (SAPbouiCOM.Grid)pForm.Items.Item("GRID01").Specific;
                string qStr = $@"
                        SELECT 
                            T1.""LineId"" AS ""SL"",
                            T4.""U_NAME"" AS ""User"",
                            CASE
                                WHEN T1.""U_STATUS"" = 'A' THEN 'Approved'
                                WHEN T1.""U_STATUS"" = 'P' THEN 'Pending'
                                WHEN T1.""U_STATUS"" = 'R' THEN 'Rejected'
                                ELSE ''
                            END AS ""Status""
                        FROM ""@FIL_DH_APPDECSN"" T0
                        INNER JOIN ""@FIL_DR_APPDECSN"" T1 ON T1.""DocEntry"" = T0.""DocEntry""
                        INNER JOIN ""OWST"" T2 ON T2.""WstCode"" = T1.""U_STAGEID""
                        INNER JOIN ""WST1"" T3 ON T3.""WstCode"" = T2.""WstCode""
                        INNER JOIN ""OUSR"" T4 ON T4.""USERID"" = T3.""UserID""
                        WHERE T0.""U_DOCENTRY"" = '{strDocEntry}'
                          AND T0.""DocEntry"" = (
                              SELECT MAX(""DocEntry"")
                              FROM ""@FIL_DH_APPDECSN""
                              WHERE ""U_DOCENTRY"" = '{strDocEntry}'
                          )
                        ORDER BY T1.""LineId"";";

                oGrid.DataTable.Clear();
                oGrid.DataTable.ExecuteQuery(qStr);
            }

            catch (Exception)
            {
               
            }

          
        }

        public override void OnInitializeFormEvents()
        {
        }

        private void OnCustomInitialize()
        {

        }

        
    }
}
