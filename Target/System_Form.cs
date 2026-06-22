using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbouiCOM.Framework;

namespace Target
{
    class System_Form
    {
          public System_Form()
            {
                Application.SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
            }

            //public string formnum = "998";
            //public string spos = "86";
            //public string epos = "46";
            //public string db = "ORDR";

            private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
            {
                BubbleEvent = true;

            if (pVal.FormTypeEx == "998" && pVal.EventType == SAPbouiCOM.BoEventTypes.et_CLICK && pVal.BeforeAction == false)
            {
                try
                {
                    // Check if data insertion is needed
                    SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);
                    string value = oForm.UniqueID;
                    if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                    {
                        Application.SBO_Application.StatusBar.SetText("UI Changed successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                }
                catch (Exception ex)
                {
                    Application.SBO_Application.MessageBox("Error: " + ex.Message);
                }

            }

        }

    }
}
