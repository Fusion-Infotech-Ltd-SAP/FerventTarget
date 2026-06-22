using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Target
{
    [FormAttribute("Target.Form_No_SOList", "Form_No_SOList.b1f")]
    class Form_No_SOList : UserFormBase
    {
        private SAPbouiCOM.Grid GRID01;
        private SAPbouiCOM.Button BTSELECT, Cancel;
        public Form_No_SOList()
        {
        }


        public override void OnInitializeComponent()
        {
            this.GRID01 = ((SAPbouiCOM.Grid)(this.GetItem("GRID01").Specific));
            this.BTSELECT = ((SAPbouiCOM.Button)(this.GetItem("BTSELECT").Specific));
            this.BTSELECT.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.BTSELECT_PressedBefore);
            this.BTSELECT.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.BTSELECT_PressedAfter);
            this.Cancel = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.OnCustomInitialize();

        }


        internal static void LoadSOList(ref SAPbouiCOM.Form pForm,string strDocEntry)
        {
            int count = strDocEntry.Split(',').Length;
            pForm.Freeze(true);
            SAPbouiCOM.Grid oGrid = (SAPbouiCOM.Grid)pForm.Items.Item("GRID01").Specific;

            if (count == 1){

                string qStr = @"
                           SELECT T1.""ItemCode"", T1.""Dscription"",T1.""Quantity"" As ""OrderQuantity"",
                           T1.""unitMsr"", T1.""WhsCode"",T0.""CardCode"",T0.""CardName"",T0.""DocNum"" As ""Order No"", T0.""DocEntry"", T1.""LineNum"", T0.""ObjType"", null AS ""SDocEntry"",null AS ""SDocNum"", null AS ""SObject"",
                           null As ""SLineId"", 
                           ((T1.""Quantity"" + ifnull(T5.""Return"",0) + ifnull(T5.""ReturnRq"",0) + ifnull(T5.""Credit"",0)) - Ifnull(T5.""ScheduledQty"", 0)) AS ""ScheduleQty"",
                           Ifnull(T5.""ScheduledQty"", 0) AS ""ScheduledQty"",
                           ((T1.""Quantity"" - Ifnull(T5.""ScheduledQty"", 0)) * (T1.""Price"" - (T1.""Price"" * T0.""DiscPrcnt"" / 100))) AS ""RowTotal"",
                           T1.""ShipDate"", T1.""Price"", T1.""PriceBefDi"",
                           T0.""BPLId"", T0.""BPLName"", T4.""SlpCode"", T4.""SlpName"",
                           T1.""DiscPrcnt"" AS ""DiscountRowLevel"", T0.""DiscPrcnt"" AS ""Discount"", T1.""TaxCode"",T2.""OnHand"", 
                           (((T1.""Quantity"" + ifnull(T5.""Return"",0) + ifnull(T5.""ReturnRq"",0) + ifnull(T5.""Credit"",0) - Ifnull(T5.""ScheduledQty"", 0)) * T1.""Price"") + ((T1.""VatSum"" / T1.""Quantity"") * (T1.""Quantity"" + ifnull(T5.""Return"",0) + ifnull(T5.""ReturnRq"",0) + ifnull(T5.""Credit"",0) - Ifnull(T5.""ScheduledQty"", 0)))) AS ""LineTotal"",
                           T3.""CntctCode"", T3.""Name"",
                           ((T1.""VatSum"" / T1.""Quantity"") * (T1.""Quantity"" + ifnull(T5.""Return"",0) + ifnull(T5.""ReturnRq"",0) + ifnull(T5.""Credit"",0) - IFNULL(T5.""ScheduledQty"", 0))) AS ""LineWiseTax"",
                           0 As ""deliverdQty"",
                           ((T1.""Quantity"" + ifnull(T5.""Return"",0)  + ifnull(T5.""ReturnRq"",0) + ifnull(T5.""Credit"",0)) - Ifnull(T5.""ScheduledQty"", 0)) As ""OpenDlvQty"",T0.""U_Site_Del"" As ""SiteDelvery"",T0.""Comments""

                           

                    FROM ""ORDR"" T0
                    INNER JOIN ""RDR1"" T1 ON T1.""DocEntry"" = T0.""DocEntry""
                    INNER JOIN ""OITM"" T2 ON T2.""ItemCode"" = T1.""ItemCode""
                    LEFT OUTER JOIN ""OCPR"" T3 ON T3.""CntctCode"" = T0.""CntctCode""
                    LEFT OUTER JOIN ""OSLP"" T4 ON T4.""SlpCode"" = T0.""SlpCode""
                    LEFT OUTER JOIN (
                         SELECT 
                            T0.""U_BASENTRY""  AS ""BaseEntry"",
                            T0.""U_BASETYPE""  AS ""BaseType"",
                            T0.""U_BASELINE""  AS ""BaseLine"",
                            IFNULL(SUM(T0.""U_SCHEDQTY""), 0) AS ""ScheduledQty"",
                            IFNULL(SUM(T1.""DeliveryQty""), 0) As ""DeliveryQty"",
                            IFNULL(SUM(T1.""Return""), 0) As ""Return"",
                            IFNULL(SUM(T1.""Credit""), 0) As ""Credit"",
                            IFNULL(SUM(T1.""ReturnRq""), 0) As ""ReturnRq""

                        FROM ""@FIL_DR_DELRSCHD"" T0
                            LEFT OUTER JOIN(
                                        SELECT T0.""U_BASENTRY"", 
                                        T0.""U_BASELINE"", 
                                        T0.""U_BASETYPE"",
                                        IFNULL(SUM(T0.""Quantity""), 0) AS ""DeliveryQty"",
                                        IFNULL(SUM(T1.""ReturnQty""), 0) AS ""Return"",
                                        IFNULL(SUM(T3.""CreditQty""), 0) AS ""Credit"",
                                        IFNULL(SUM(T5.""ReturnRqQty""), 0) AS ""ReturnRq""

                                FROM ""DLN1"" T0
                                INNER JOIN ""ODLN"" OD on OD.""DocEntry"" = T0.""DocEntry"" AND OD.""CANCELED"" = 'N'
                                LEFT OUTER JOIN(
                                    SELECT ""BaseEntry"", ""BaseLine"", ""BaseType"", 
                                            SUM(""Quantity"") AS ""ReturnQty"" 
                                    FROM ""RDN1""
                                    GROUP BY ""BaseEntry"", ""BaseLine"", ""BaseType""
                                ) T1  ON T1.""BaseEntry"" = T0.""DocEntry""   AND T1.""BaseLine"" = T0.""LineNum""  AND T1.""BaseType"" = T0.""ObjType""

                                LEFT OUTER JOIN ""INV1"" T2   ON T2.""BaseEntry"" = T0.""DocEntry"" AND T2.""BaseLine"" = T0.""LineNum"" AND T2.""BaseType"" = T0.""ObjType""
                                LEFT OUTER JOIN ""OINV"" OI on OI.""DocEntry"" = T2.""DocEntry"" AND OI.""CANCELED"" = 'N'
                                LEFT OUTER JOIN(
                                    SELECT ""BaseEntry"", ""BaseLine"", ""BaseType"", 
                                            SUM(""Quantity"") AS ""CreditQty"" 
                                    FROM ""RIN1""
                                    GROUP BY ""BaseEntry"", ""BaseLine"", ""BaseType""
                                ) T3   ON T3.""BaseEntry"" = T2.""DocEntry""  AND T3.""BaseLine"" = T2.""LineNum"" AND T3.""BaseType"" = T2.""ObjType""

                               LEFT OUTER JOIN (
					                        SELECT T4.""BaseEntry"", T4.""BaseLine"", T4.""BaseType"", SUM(T5.""Quantity"") AS ""ReturnRqQty""
                                            FROM ""RRR1"" T4
                                            LEFT OUTER JOIN ""RDN1"" T5 ON T5.""BaseEntry"" = T4.""DocEntry""  AND T5.""BaseLine"" = T4.""LineNum"" AND T5.""BaseType"" = T4.""ObjType""
                                            GROUP BY  T4.""BaseEntry"", T4.""BaseLine"",  T4.""BaseType""
					                    ) T5 ON T5.""BaseEntry"" = T0.""DocEntry"" AND T5.""BaseLine"" = T0.""LineNum"" AND T5.""BaseType"" = T0.""ObjType""
                                GROUP BY  T0.""U_BASENTRY"",  T0.""U_BASELINE"", T0.""U_BASETYPE""
                            ) T1 ON T1.""U_BASENTRY"" = T0.""DocEntry"" AND T1.""U_BASETYPE"" = T0.""Object"" AND T1.""U_BASELINE"" = T0.""LineId""
                            GROUP BY  T0.""U_BASENTRY"",T0.""U_BASETYPE"",T0.""U_BASELINE""
                         ) T5 ON T5.""BaseEntry"" = T1.""DocEntry"" AND T5.""BaseType"" = T1.""ObjType"" AND T5.""BaseLine"" = T1.""LineNum""
                    WHERE T0.""DocEntry"" IN ({0}) AND T0.""DocStatus"" = 'O'
                    And   ((T1.""Quantity"" + ifnull(T5.""Return"",0)  +   ifnull(T5.""ReturnRq"",0) + ifnull(T5.""Credit"",0)) - Ifnull(T5.""ScheduledQty"", 0)) > 0
                    ORDER BY T0.""DocNum"", T0.""DocEntry"", T1.""LineNum"";
                ";
                oGrid.DataTable.Clear();
                qStr = string.Format(qStr, strDocEntry);
                oGrid.DataTable.ExecuteQuery(qStr);

                if (oGrid.DataTable.IsEmpty)
                {
                    oGrid.DataTable.Clear();
                    Global.objFun.ShowError("No Pending Items");
                    pForm.Freeze(false);
                    return;
                }

            }

            else
            {

                string qStr = @"
                                SELECT 
                                   x.""ItemCode"", x.""Dscription"",  x.""unitMsr"", x.""WhsCode"", x.""Quantity"" AS ""OrderQuantity"",
                                   x.""CardCode"",x.""CardName"",x.""DocNum"" As ""Order No"", x.""DocEntry"",  x.""LineNum"",x.""ObjType"", null AS ""SDocEntry"",  null As ""SLineId"",null AS ""SDocNum"", null AS ""SObject"",
                                   x.""ScheduleQty"" AS ""ScheduleQty"",
                                   x.""ScheduledQty"" AS ""ScheduledQty"",
                                   x.""ShipDate"", x.""PriceBefDi"",
                                   (x.""PriceBefDi"" - (x.""PriceBefDi"" * ((((x.""ScheduleQty"" * x.""PriceBefDi"") - x.""RowTotal"") / (x.""ScheduleQty"" * x.""PriceBefDi"")) * 100) / 100)) As ""Price"", 
                                   ((((x.""ScheduleQty"" * x.""PriceBefDi"") - x.""RowTotal"") / (x.""ScheduleQty"" * x.""PriceBefDi"")) * 100) AS ""DiscountRowLevel"",
                                   (x.""RowTotal"" + x.""LineWiseTax"") AS ""LineTotal"",
                                   x.""BPLId"", x.""BPLName"", x.""SlpCode"",  x.""SlpName"",
                                   0 AS ""Discount"", x.""LineTotal"", x.""TaxCode"",x.""OnHand"", x.""CntctCode"", x.""Name"",x.""LineWiseTax"",x.""deliverdQty"",x.""OpenDlvQty"",x.""SiteDelvery"",x.""Comments""

                                FROM (
                                       SELECT 
                                           T0.""CardCode"",T0.""CardName"",T0.""DocNum"", T0.""DocEntry"", T1.""LineNum"", T0.""ObjType"", T1.""ItemCode"", T1.""Dscription"",
                                           T1.""unitMsr"",T1.""Quantity"" As ""Quantity"",
                                          ((T1.""Quantity"" + ifnull(T5.""Return"",0) + ifnull(T5.""ReturnRq"",0) + ifnull(T5.""Credit"",0)) - Ifnull(T5.""ScheduledQty"", 0)) AS ""ScheduleQty"",
                                           IFNULL(T5.""ScheduledQty"", 0) AS ""ScheduledQty"",
                                           T1.""ShipDate"",  T1.""Price"",T1.""PriceBefDi"",
                                           ((T1.""VatSum"" / T1.""Quantity"") * (T1.""Quantity"" + ifnull(T5.""Return"",0) + ifnull(T5.""ReturnRq"",0) + ifnull(T5.""Credit"",0) - IFNULL(T5.""ScheduledQty"", 0))) AS ""LineWiseTax"", 
                                           ((T1.""Quantity"" + ifnull(T5.""Return"",0) + ifnull(T5.""ReturnRq"",0) + ifnull(T5.""Credit"",0) - IFNULL(T5.""ScheduledQty"", 0)) * (T1.""Price"" - (T1.""Price"" * T0.""DiscPrcnt"" / 100))) AS ""RowTotal"",
                                           T1.""WhsCode"", T0.""BPLId"", T0.""BPLName"",T4.""SlpCode"", T4.""SlpName"", 
                                           T1.""DiscPrcnt"" AS ""DiscountRowLevel"",
                                           T0.""DiscPrcnt"" AS ""Discount"",
                                           T1.""LineTotal"", T1.""TaxCode"",T2.""OnHand"",T3.""CntctCode"",T3.""Name"",
                                           0 As ""deliverdQty"",
                                          ((T1.""Quantity"" + ifnull(T5.""Return"",0) + ifnull(T5.""ReturnRq"",0) + ifnull(T5.""Credit"",0)) - Ifnull(T5.""ScheduledQty"", 0)) As ""OpenDlvQty"",T0.""U_Site_Del"" As ""SiteDelvery"",T0.""Comments""

                                       FROM ""ORDR"" T0
                                       INNER JOIN ""RDR1"" T1 ON T1.""DocEntry"" = T0.""DocEntry""
                                       INNER JOIN ""OITM"" T2 ON T2.""ItemCode"" = T1.""ItemCode""
                                       LEFT OUTER JOIN ""OCPR"" T3 ON T3.""CntctCode"" = T0.""CntctCode""
                                       LEFT OUTER JOIN ""OSLP"" T4 ON T4.""SlpCode"" = T0.""SlpCode""
                                      LEFT OUTER JOIN (
                                                SELECT 
                                                T0.""U_BASENTRY""  AS ""BaseEntry"",
                                                T0.""U_BASETYPE""  AS ""BaseType"",
                                                T0.""U_BASELINE""  AS ""BaseLine"",
                                                IFNULL(SUM(T0.""U_SCHEDQTY""), 0) AS ""ScheduledQty"",
                                                IFNULL(SUM(T1.""DeliveryQty""), 0) As ""DeliveryQty"",
                                                IFNULL(SUM(T1.""Return""), 0) As ""Return"",
                                                IFNULL(SUM(T1.""Credit""), 0) As ""Credit"",
                                                IFNULL(SUM(T1.""ReturnRq""), 0) As ""ReturnRq""

                                            FROM ""@FIL_DR_DELRSCHD"" T0
                                            LEFT OUTER JOIN(
                                                         SELECT T0.""U_BASENTRY"", 
                                                                T0.""U_BASELINE"", 
                                                                T0.""U_BASETYPE"",
                                                                IFNULL(SUM(T0.""Quantity""), 0) AS ""DeliveryQty"",
                                                                IFNULL(SUM(T1.""ReturnQty""), 0) AS ""Return"",
                                                                IFNULL(SUM(T3.""CreditQty""), 0) AS ""Credit"",
                                                                IFNULL(SUM(T5.""ReturnRqQty""), 0) AS ""ReturnRq""

                                                        FROM ""DLN1"" T0
                                                        INNER JOIN ""ODLN"" OD on OD.""DocEntry"" = T0.""DocEntry"" AND OD.""CANCELED"" = 'N'
                                                        LEFT OUTER JOIN(
                                                            SELECT ""BaseEntry"", ""BaseLine"", ""BaseType"", 
                                                                    SUM(""Quantity"") AS ""ReturnQty"" 
                                                            FROM ""RDN1""
                                                            GROUP BY ""BaseEntry"", ""BaseLine"", ""BaseType""
                                                        ) T1  ON T1.""BaseEntry"" = T0.""DocEntry""   AND T1.""BaseLine"" = T0.""LineNum""  AND T1.""BaseType"" = T0.""ObjType""

                                                        LEFT OUTER JOIN ""INV1"" T2   ON T2.""BaseEntry"" = T0.""DocEntry"" AND T2.""BaseLine"" = T0.""LineNum"" AND T2.""BaseType"" = T0.""ObjType""
                                                        LEFT OUTER JOIN ""OINV"" OI on OI.""DocEntry"" = T2.""DocEntry"" AND OI.""CANCELED"" = 'N'
                                                        LEFT OUTER JOIN(
                                                            SELECT ""BaseEntry"", ""BaseLine"", ""BaseType"", 
                                                                    SUM(""Quantity"") AS ""CreditQty"" 
                                                            FROM ""RIN1""
                                                            GROUP BY ""BaseEntry"", ""BaseLine"", ""BaseType""
                                                        ) T3   ON T3.""BaseEntry"" = T2.""DocEntry""  AND T3.""BaseLine"" = T2.""LineNum"" AND T3.""BaseType"" = T2.""ObjType""

                                                        LEFT OUTER JOIN (
					                                                SELECT T4.""BaseEntry"", T4.""BaseLine"", T4.""BaseType"", SUM(T5.""Quantity"") AS ""ReturnRqQty""
                                                                    FROM ""RRR1"" T4
                                                                    LEFT OUTER JOIN ""RDN1"" T5 ON T5.""BaseEntry"" = T4.""DocEntry""  AND T5.""BaseLine"" = T4.""LineNum"" AND T5.""BaseType"" = T4.""ObjType""
                                                                    GROUP BY  T4.""BaseEntry"", T4.""BaseLine"",  T4.""BaseType""
					                                            ) T5 ON T5.""BaseEntry"" = T0.""DocEntry"" AND T5.""BaseLine"" = T0.""LineNum"" AND T5.""BaseType"" = T0.""ObjType""

                                                        GROUP BY  T0.""U_BASENTRY"",  T0.""U_BASELINE"", T0.""U_BASETYPE""
                                                    ) T1 ON T1.""U_BASENTRY"" = T0.""DocEntry"" AND T1.""U_BASETYPE"" = T0.""Object"" AND T1.""U_BASELINE"" = T0.""LineId""
                                                GROUP BY  T0.""U_BASENTRY"",T0.""U_BASETYPE"",T0.""U_BASELINE""
                                                ) T5 ON T5.""BaseEntry"" = T1.""DocEntry"" AND T5.""BaseType"" = T1.""ObjType"" AND T5.""BaseLine"" = T1.""LineNum""
                                       WHERE T0.""DocEntry"" IN ({0}) AND T0.""DocStatus"" = 'O' 
                                        And   ((T1.""Quantity"" + ifnull(T5.""Return"",0) +  ifnull(T5.""ReturnRq"",0)  + ifnull(T5.""Credit"",0)) - Ifnull(T5.""ScheduledQty"", 0)) > 0
                                       ORDER BY T0.""DocNum"", T0.""DocEntry"", T1.""LineNum""
                                ) x
                                ORDER BY x.""DocNum"", x.""DocEntry"", x.""LineNum"";
                                ";
                               oGrid.DataTable.Clear();
                               qStr = string.Format(qStr, strDocEntry);
                               oGrid.DataTable.ExecuteQuery(qStr);
                                if (oGrid.DataTable.IsEmpty)
                                {
                                    oGrid.DataTable.Clear();
                                    Global.objFun.ShowError("No Pending Items");
                                    pForm.Freeze(false);
                                    return;
                                }

                
            }
           

            int rowCount = oGrid.DataTable.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                oGrid.Rows.SelectedRows.Add(i);
            }
            pForm.Freeze(false);
           
        }



        internal static void LoadScheduleList(ref SAPbouiCOM.Form pForm, string strDocEntry)
        {
            int count = strDocEntry.Split(',').Length;
            pForm.Freeze(true);
            SAPbouiCOM.Grid oGrid = (SAPbouiCOM.Grid)pForm.Items.Item("GRID01").Specific;

            try
            {
                if(count == 1)
                {

                    string qStr = @"
                                SELECT T1.""ItemCode"", T1.""Dscription"", ifnull((SD.""U_DLVRYQTY""),0) AS ""OpenDlvQty"",T1.""unitMsr"",SD.""U_WHSCODE"" AS ""WhsCode"",
                                       SM.""U_CARDCODE"" As ""CardCode"",SM.""U_CARDNAME"" As ""CardName"",T0.""DocNum"" As ""Order No"",T1.""Quantity"" AS ""OrderQuantity"",
                                       IFNULL(SD.""U_SCHEDQTY"",0) AS ""ScheduleQty"",SM.""DocEntry"" AS ""SDocEntry"",
                                       SM.""DocNum"" AS ""SDocNum"", SD.""Object"" AS ""SObject"", T0.""DocEntry"",
                                       SD.""LineId"" As ""SLineId"",T1.""LineNum"", T0.""ObjType"", 
                                       IFNULL(T5.""ScheduledQty"", 0) AS ""ScheduledQty"",
                                       ((SD.""U_SCHEDQTY"") * (SD.""U_PRICEAFDI"" - (SD.""U_PRICEAFDI"" * T0.""DiscPrcnt"" / 100))) AS ""RowTotal"",
                                       T1.""ShipDate"", SD.""U_PRICEAFDI"" AS ""Price"", T1.""PriceBefDi"", 
                                       T0.""BPLId"", T0.""BPLName"", T4.""SlpCode"", T4.""SlpName"",
                                       SD.""U_DISCPERC"" AS ""DiscountRowLevel"", SM.""U_DISPRCNT"" AS ""Discount"", T1.""TaxCode"", T2.""OnHand"", 
                                       (((SD.""U_SCHEDQTY"") * SD.""U_PRICEAFDI"") + ((SD.""U_TAXAMNT"" / SD.""U_SCHEDQTY"") * (SD.""U_SCHEDQTY""))) AS ""LineTotal"",
                                       T3.""CntctCode"", T3.""Name"",
                                       ((SD.""U_TAXAMNT"" / SD.""U_SCHEDQTY"") * ifnull((SD.""U_DLVRYQTY""),0)) AS ""LineWiseTax"",
                                       ifnull(SD.""U_ALDLYQTY"",0) AS ""deliverdQty"",
                                       SM.""U_STEDLVRY"" AS ""SiteDelvery"", T0.""Comments""
                                FROM ""@FIL_DH_DELRSCHD"" SM
                                INNER JOIN ""@FIL_DR_DELRSCHD"" SD ON SD.""DocEntry"" = SM.""DocEntry""
                                INNER JOIN ""RDR1"" T1 ON T1.""ItemCode"" = SD.""U_ITEMCODE"" AND T1.""DocEntry"" = SD.""U_BASENTRY"" AND T1.""LineNum"" = SD.""U_BASELINE""
                                INNER JOIN ""ORDR"" T0 ON T0.""DocEntry"" = T1.""DocEntry""
                                INNER JOIN ""OITM"" T2 ON T2.""ItemCode"" = T1.""ItemCode""
                                LEFT OUTER JOIN ""OCPR"" T3 ON T3.""CntctCode"" = T0.""CntctCode""
                                LEFT OUTER JOIN ""OSLP"" T4 ON T4.""SlpCode"" = T0.""SlpCode""
                                LEFT OUTER JOIN (
                                    SELECT 
                                        T0.""U_BASENTRY"" AS ""BaseEntry"",
                                        T0.""U_BASETYPE"" AS ""BaseType"",
                                        T0.""U_BASELINE"" AS ""BaseLine"",
                                        Ifnull(Sum(T0.""U_SCHEDQTY""),0) AS ""ScheduledQty"",
                                        IFNULL(SUM(T1.""Quantity""),0) AS ""DeliveryQty""
                                    FROM ""@FIL_DR_DELRSCHD"" T0
                                    LEFT OUTER JOIN ""DLN1"" T1 ON T1.""U_BASENTRY"" = T0.""DocEntry"" AND T1.""U_BASETYPE"" = T0.""Object"" AND T1.""U_BASELINE"" = T0.""LineId""
                                    GROUP BY T0.""U_BASENTRY"", T0.""U_BASETYPE"", T0.""U_BASELINE""
                                ) T5 ON T5.""BaseEntry"" = T1.""DocEntry"" AND T5.""BaseType"" = T1.""ObjType"" AND T5.""BaseLine"" = T1.""LineNum""
                                WHERE SM.""DocEntry"" IN ({0}) AND SM.""Status"" = 'O'   AND ifnull(SD.""U_SCHEDQTY"",0) > ifnull(SD.""U_ALDLYQTY"",0)
                                ORDER BY T0.""DocNum"", T0.""DocEntry"", T1.""LineNum"";
                            ";
                    oGrid.DataTable.Clear();
                    qStr = string.Format(qStr, strDocEntry);
                    oGrid.DataTable.ExecuteQuery(qStr);
                    if (oGrid.DataTable.IsEmpty)
                    {
                        oGrid.DataTable.Clear();
                        Global.objFun.ShowError("No Pending Items");
                        pForm.Freeze(false);
                        return;
                    }
                }

                else
                {

                    string qStr = @"
                                SELECT  x.""ItemCode"", x.""Dscription"",x.""OpenDlvQty"",x.""unitMsr"",x.""WhsCode"",
                                   x.""CardCode"",x.""CardName"",x.""DocNum"" as ""Order No"",  x.""Quantity"" As ""OrderQuantity"",x.""ScheduleQty"" AS ""ScheduleQty"",x.""SDocEntry"",x.""SDocNum"",x.""SObject"",x. ""SLineId"", x.""DocEntry"",  x.""LineNum"",  x.""ObjType"",
                                   x.""ScheduledQty"" AS ""ScheduledQty"",
                                   x.""ShipDate"", x.""PriceBefDi"",
                                   (x.""PriceBefDi"" - (x.""PriceBefDi"" * ((((x.""ScheduleQty"" * x.""PriceBefDi"") - x.""RowTotal"") / (x.""ScheduleQty"" * x.""PriceBefDi"")) * 100) / 100)) As ""Price"", 
                                   ((((x.""ScheduleQty"" * x.""PriceBefDi"") - x.""RowTotal"") / (x.""ScheduleQty"" * x.""PriceBefDi"")) * 100) AS ""DiscountRowLevel"",
                                   (x.""RowTotal"" + x.""LineWiseTax"") AS ""LineTotal"",
                                   x.""BPLId"", x.""BPLName"", x.""SlpCode"",  x.""SlpName"",
                                   0 AS ""Discount"", x.""TaxCode"",x.""OnHand"", x.""CntctCode"", x.""Name"",x.""LineWiseTax"",x.""deliverdQty"",x.""SiteDelvery"",x.""Comments""

                                FROM (
                                       SELECT 
                                            SM.""U_CARDCODE"" As ""CardCode"",SM.""U_CARDNAME"" As ""CardName"",T0.""DocNum"", SM.""DocEntry"" AS ""SDocEntry"",
                                            SM.""DocNum"" AS ""SDocNum"", SD.""Object"" AS ""SObject"",T0.""DocEntry"",
                                           SD.""LineId"" As ""SLineId"",T1.""LineNum"", T0.""ObjType"", T1.""ItemCode"", T1.""Dscription"",
                                           T1.""unitMsr"", T1.""Quantity"" AS ""Quantity"",
                                           SD.""U_SCHEDQTY"" AS ""ScheduleQty"",
                                           IFNULL(T5.""ScheduledQty"", 0) AS ""ScheduledQty"",
                                           ((SD.""U_SCHEDQTY"") * (SD.""U_PRICEAFDI"" - (SD.""U_PRICEAFDI"" * SM.""U_DISPRCNT"" / 100))) AS ""RowTotal"",
                                           T1.""ShipDate"", T1.""Price"", T1.""PriceBefDi"", SD.""U_WHSCODE"" AS ""WhsCode"",
                                           T0.""BPLId"", T0.""BPLName"", T4.""SlpCode"", T4.""SlpName"",
                                           SD.""U_DISCPERC"" AS ""DiscountRowLevel"",SM.""U_DISPRCNT"" AS ""Discount"", T1.""TaxCode"", T2.""OnHand"", 
                                           (((SD.""U_SCHEDQTY"") * SD.""U_PRICEAFDI"") + ((SD.""U_TAXAMNT"" / SD.""U_SCHEDQTY"") * (SD.""U_SCHEDQTY""))) AS ""LineTotal"",
                                           T3.""CntctCode"", T3.""Name"",
                                           ((SD.""U_TAXAMNT"" / SD.""U_SCHEDQTY"") * ifnull((SD.""U_DLVRYQTY""),0)) AS ""LineWiseTax"",
                                           ifnull(SD.""U_ALDLYQTY"",0) AS ""deliverdQty"",
                                           ifnull((SD.""U_DLVRYQTY""),0) AS ""OpenDlvQty"", SM.""U_STEDLVRY"" AS ""SiteDelvery"", T0.""Comments""

                                        FROM ""@FIL_DH_DELRSCHD"" SM
                                        INNER JOIN ""@FIL_DR_DELRSCHD"" SD ON SD.""DocEntry"" = SM.""DocEntry""
                                        INNER JOIN ""RDR1"" T1 ON T1.""ItemCode"" = SD.""U_ITEMCODE"" AND T1.""DocEntry"" = SD.""U_BASENTRY"" AND T1.""LineNum"" = SD.""U_BASELINE""
                                        INNER JOIN ""ORDR"" T0 ON T0.""DocEntry"" = T1.""DocEntry""
                                        INNER JOIN ""OITM"" T2 ON T2.""ItemCode"" = T1.""ItemCode""
                                        LEFT OUTER JOIN ""OCPR"" T3 ON T3.""CntctCode"" = T0.""CntctCode""
                                        LEFT OUTER JOIN ""OSLP"" T4 ON T4.""SlpCode"" = T0.""SlpCode""
                                        LEFT OUTER JOIN (
                                                 SELECT 
                                                   T0. ""U_BASENTRY"" AS ""BaseEntry"",
                                                   T0.""U_BASETYPE"" AS ""BaseType"",
                                                   T0.""U_BASELINE"" AS ""BaseLine"",
                                                   Ifnull(Sum(T0.""U_SCHEDQTY""),0) AS ""ScheduledQty"",
                                                   Ifnull(Sum(T1.""Quantity""),0) As ""DeliveryQty""
                                                 FROM ""@FIL_DR_DELRSCHD"" T0
                                                 Left outer join ""DLN1"" T1 ON T1.""U_BASENTRY"" = T0.""DocEntry"" and  T1.""U_BASETYPE"" =  T0.""Object"" and  T1.""U_BASELINE"" = T0.""LineId""
                                                 GROUP BY T0.""U_BASENTRY"", T0.""U_BASETYPE"", T0.""U_BASELINE""
                                            ) T5 ON T5.""BaseEntry"" = T1.""DocEntry"" AND T5.""BaseType"" = T1.""ObjType"" AND T5.""BaseLine"" = T1.""LineNum""
                                        WHERE SM.""DocEntry"" IN ({0}) AND SM.""Status"" = 'O'  AND ifnull(SD.""U_SCHEDQTY"",0) > ifnull(SD.""U_ALDLYQTY"",0)
                                       ORDER BY T0.""DocNum"", T0.""DocEntry"", T1.""LineNum""
                                ) x
                                ORDER BY x.""DocNum"", x.""DocEntry"", x.""LineNum"";
                                ";
                    oGrid.DataTable.Clear();
                    qStr = string.Format(qStr, strDocEntry);
                    oGrid.DataTable.ExecuteQuery(qStr);
                    if (oGrid.DataTable.IsEmpty)
                    {
                        oGrid.DataTable.Clear();
                        Global.objFun.ShowError("No Pending Items");
                        pForm.Freeze(false);
                        return;
                    }



                }

                int rowCount = oGrid.DataTable.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    oGrid.Rows.SelectedRows.Add(i);
                }
                pForm.Freeze(false);
            }

            catch(Exception)
            {
                pForm.Freeze(false);
            }

        }


        private void BTSELECT_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item("FIL_FRM_NO_SOLIST");
            SAPbouiCOM.Grid oGrid = (SAPbouiCOM.Grid)oForm.Items.Item("GRID01").Specific;

            SAPbouiCOM.SelectedRows selRows = oGrid.Rows.SelectedRows;

            if(selRows.Count == 0)
            {
                Application.SBO_Application.MessageBox("No rows selected!");
                return;
            }



            var Data = new List<(string CardCode, string CardName, string SDocEntry, string SDocNum, string SObject, int SLineId,string DocEntry, string DocNum, int LineNum, string ItemCode, string Dscription,
                       string UnitMsr, double Quantity, double ScheduleQty, string ShipDate, double OnHand, string TaxCode,
                       double Price, double PriceBefDi, double DiscountRowLevel,double Discount, double LineTotal, string WhsCode, string ObjType,string CntctCode , 
                       string Name,int BPLId, string BPLName, string  SlpCode, string SlpName,double LineWiseTax,double deliverdQty, double OpenDlvQty,string SiteDelvery, string Comments)> ();


            for (int i = 0; i < selRows.Count; i++)
            {
                int dtRow = selRows.Item(i, SAPbouiCOM.BoOrderType.ot_SelectionOrder);

                var SDocEntry = oGrid.DataTable.GetValue("SDocEntry", dtRow)?.ToString() ?? null;
                var CardCode = oGrid.DataTable.GetValue("CardCode", dtRow)?.ToString();
                var CardName = oGrid.DataTable.GetValue("CardName", dtRow)?.ToString();
                var docEntry = oGrid.DataTable.GetValue("DocEntry", dtRow)?.ToString();
                var SDocNum = oGrid.DataTable.GetValue("SDocNum", dtRow)?.ToString();
                var SObject = oGrid.DataTable.GetValue("SObject", dtRow)?.ToString();
                int.TryParse(oGrid.DataTable.GetValue("SLineId", dtRow)?.ToString(), out int SLineId);
                var docNum = oGrid.DataTable.GetValue("Order No", dtRow)?.ToString();
                var itemCode = oGrid.DataTable.GetValue("ItemCode", dtRow)?.ToString();
                var dscription = oGrid.DataTable.GetValue("Dscription", dtRow)?.ToString();
                var unitMsr = oGrid.DataTable.GetValue("unitMsr", dtRow)?.ToString();
                var shipDate = oGrid.DataTable.GetValue("ShipDate", dtRow)?.ToString();
                var whsCode = oGrid.DataTable.GetValue("WhsCode", dtRow)?.ToString();
                var taxCode = oGrid.DataTable.GetValue("TaxCode", dtRow)?.ToString();
                var ObjType = oGrid.DataTable.GetValue("ObjType", dtRow)?.ToString();
                var CntctCode = oGrid.DataTable.GetValue("CntctCode", dtRow)?.ToString();
                var Name = oGrid.DataTable.GetValue("Name", dtRow)?.ToString();
                var BPLName = oGrid.DataTable.GetValue("BPLName", dtRow)?.ToString();
                var SlpCode = oGrid.DataTable.GetValue("SlpCode", dtRow)?.ToString();
                var SlpName = oGrid.DataTable.GetValue("SlpName", dtRow)?.ToString();
                var SiteDelvery = oGrid.DataTable.GetValue("SiteDelvery", dtRow)?.ToString();
                var Comments = oGrid.DataTable.GetValue("Comments", dtRow)?.ToString();
                int.TryParse(oGrid.DataTable.GetValue("LineNum", dtRow)?.ToString(), out int lineNum);
                int.TryParse(oGrid.DataTable.GetValue("BPLId", dtRow)?.ToString(), out int BPLId);
                double.TryParse(oGrid.DataTable.GetValue("OrderQuantity", dtRow)?.ToString(), out double quantity);
                double.TryParse(oGrid.DataTable.GetValue("ScheduleQty", dtRow)?.ToString(), out double ScheduleQty);
                double.TryParse(oGrid.DataTable.GetValue("Price", dtRow)?.ToString(), out double price);
                double.TryParse(oGrid.DataTable.GetValue("DiscountRowLevel", dtRow)?.ToString(), out double DiscountRowLevel);
                double.TryParse(oGrid.DataTable.GetValue("Discount", dtRow)?.ToString(), out double Discount);
                double.TryParse(oGrid.DataTable.GetValue("OnHand", dtRow)?.ToString(), out double onHand);
                double.TryParse(oGrid.DataTable.GetValue("LineTotal", dtRow)?.ToString(), out double lineTotal);
                double.TryParse(oGrid.DataTable.GetValue("PriceBefDi", dtRow)?.ToString(), out double PriceBefDi);
                double.TryParse(oGrid.DataTable.GetValue("LineWiseTax", dtRow)?.ToString(), out double LineWiseTax);
                double.TryParse(oGrid.DataTable.GetValue("deliverdQty", dtRow)?.ToString(), out double deliverdQty);
                double.TryParse(oGrid.DataTable.GetValue("OpenDlvQty", dtRow)?.ToString(), out double OpenDlvQty);

                Data.Add((CardCode, CardName, SDocEntry, SDocNum, SObject, SLineId,docEntry, docNum, lineNum, itemCode, dscription, unitMsr, quantity, ScheduleQty, shipDate, onHand, taxCode, price, PriceBefDi, DiscountRowLevel, Discount, lineTotal, whsCode, ObjType, CntctCode, Name, BPLId, BPLName, SlpCode, SlpName, LineWiseTax, deliverdQty, OpenDlvQty, SiteDelvery, Comments));

            }
          
            if (Data[0].SDocEntry == null || Data[0].SDocEntry == "")
            {
                oForm.Close();
                Form_UDO_DeliverySchedule.ItemList(Data);
            }

            else
            {
                oForm.Close();
                Form_No_DeliverySchedule.ItemList(Data);
            }
           

        }


        public override void OnInitializeFormEvents()
        {
        }

        private void BTSELECT_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item("FIL_FRM_NO_SOLIST");
            SAPbouiCOM.Grid oGrid = (SAPbouiCOM.Grid)oForm.Items.Item("GRID01").Specific;

            SAPbouiCOM.SelectedRows selRows = oGrid.Rows.SelectedRows;


            List<string> checkList = new List<string>();

            for (int i = 0; i < selRows.Count; i++)
            {
                int dtRow = selRows.Item(i, SAPbouiCOM.BoOrderType.ot_RowOrder);

                string docEntry = oGrid.DataTable.GetValue("DocEntry", dtRow)?.ToString();
                string lineNum = oGrid.DataTable.GetValue("LineNum", dtRow)?.ToString();
                var itemCode = oGrid.DataTable.GetValue("ItemCode", dtRow)?.ToString();
                var docNum = oGrid.DataTable.GetValue("Order No", dtRow)?.ToString();

                string key = docEntry + "-" + lineNum;

                if (checkList.Contains(key))
                {
                    Global.objFun.ShowError(  $"Duplicate Rows Found! DocNum: {docNum}, ItemCode: {itemCode}");
                    BubbleEvent = false;
                }

                checkList.Add(key);
            }

        }

        private void OnCustomInitialize()
        {

        }

    }
}
