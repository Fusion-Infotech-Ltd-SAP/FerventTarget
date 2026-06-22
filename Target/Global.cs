using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Target
{
    class Global
    {
        public static SAPbouiCOM.Application G_UI_Application;
        public static SAPbobsCOM.Company ocomp; // Varible for company 
        public static GlobalFunction objFun = new GlobalFunction();
        public static string FilePath;
        public static string FileName;
        public static int currentProcess;
        public static OpenFileDialog legacyFileDialog = new OpenFileDialog();
        public static string legacyFileName;
        public static string DocEntrys;
        public static string dlvDocEntry;
    }
}
