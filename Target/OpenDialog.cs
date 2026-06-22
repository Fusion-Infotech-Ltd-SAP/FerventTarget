using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Target
{
    class OpenDialog
    {

        //private Thread myThread;
        //private string strSelectedPath;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        public static int GetCurrentProcessID()
        {
            IntPtr hWnd = GetForegroundWindow();
            GetWindowThreadProcessId(hWnd, out int processID);
            return processID;
        }
        //public static void SelectAndReadExcel()
        //{
        //    Thread myThread = new Thread(new ThreadStart(ShowFileDialog));
        //    myThread.SetApartmentState(ApartmentState.STA);
        //    myThread.Start();
        //    myThread.Join();
        //}

        //public static void ShowFileDialog()
        //{
        //    OpenFileDialog ofd = new OpenFileDialog
        //    {
        //        Filter = "Excel Files|*.xls;*.xlsx",
        //        Title = "Select Excel File"
        //    };

        //    if (ofd.ShowDialog() == DialogResult.OK)
        //    {
        //        Global.FilePath = ofd.FileName;
        //        //ofd.FileName.
        //        //ReadExcelData(filePath);
        //    }
        //}
        public static bool GetFile(ref string filePath, ref string fileName, ref string fileExt)
        {
            try
            {
                Global.legacyFileDialog.Filter = "JPEGs|*.jpg|GIFs|*.gif|Bitmaps|*.bmp";
                Global.legacyFileDialog.Multiselect = false;
                Global.legacyFileDialog.CheckFileExists = false;
                Global.legacyFileDialog.CheckPathExists = true;
                Global.legacyFileDialog.RestoreDirectory = true;

                fileName = SelectFile();


                if (File.Exists(fileName))
                {
                  FileInfo fi = new FileInfo(fileName);
                  filePath = fi.DirectoryName;
                  fileName = Path.GetFileNameWithoutExtension(fi.Name);
                  fileExt = fi.Extension;
                  return true;
                }
                else
                {
                  return false;
                }
                   
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Global.legacyFileDialog.Dispose();
            }
        }
        public static bool GetExcelFile(ref string filePath, ref string fileName)
        {
            try
            {
               Global.legacyFileDialog.DefaultExt = "*.*";
                // legacyFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                Global.legacyFileDialog.Filter = "All Files|*.*|Excel Files 97-2003|*.xls|Excel Files 2007|*.xlsx|PDF Files (*.pdf)|*.pdf";
                Global.legacyFileDialog.Multiselect = false;
                Global.legacyFileDialog.CheckFileExists = false;
                Global.legacyFileDialog.CheckPathExists = true;
                // legacyFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Global.legacyFileDialog.RestoreDirectory = true;

                fileName = SelectFile();
                if (File.Exists(fileName))
                {
                    FileInfo fi = new FileInfo(fileName);
                    filePath = fi.DirectoryName;
                    fileName = fi.Name;
                    Global.legacyFileDialog.Dispose();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Global.legacyFileDialog.Dispose();
            }
        }

        public static string SelectFile()
        {
            Thread showFolderBrowserThread;
            try
            {
                showFolderBrowserThread = new Thread(new ThreadStart(ShowFolderBrowser));

                if (showFolderBrowserThread.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    showFolderBrowserThread.SetApartmentState(ApartmentState.STA);
                    showFolderBrowserThread.Start();
                    showFolderBrowserThread.Join();
                }
                else if (showFolderBrowserThread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    showFolderBrowserThread = new Thread(new ThreadStart(ShowFolderBrowser));
                    showFolderBrowserThread.SetApartmentState(ApartmentState.STA);
                    showFolderBrowserThread.Start();
                    showFolderBrowserThread.Join();
                }

                while (showFolderBrowserThread.ThreadState == System.Threading.ThreadState.Running)
                {
                    Application.DoEvents();
                }

                if (!string.IsNullOrEmpty(Global.legacyFileName))
                {
                    return Global.legacyFileName;
                }
            }
            catch (Exception ex)
            {
                Global.G_UI_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }

            return "";
        }
        public static void ShowFolderBrowser()
        {
            try
            {
                Global.legacyFileName = "";
                Process myProcess = Process.GetProcessById(Global.currentProcess);

                // Show the file dialog
                DialogResult ret = Global.legacyFileDialog.ShowDialog(new WindowWrapper(myProcess.MainWindowHandle));

                if (ret == DialogResult.OK)
                {
                    Global.legacyFileName = Global.legacyFileDialog.FileName;
                }
                else
                {
                    Application.ExitThread();
                }
            }
            catch (Exception ex)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                Global.legacyFileName = "";
            }
            finally
            {
                Global.legacyFileDialog.Dispose();
                GC.Collect();
            }
        }
        public class WindowWrapper : IWin32Window
        {
            private IntPtr _hwnd;

            public WindowWrapper(IntPtr handle)
            {
                _hwnd = handle;
            }

            public IntPtr Handle
            {
                get { return _hwnd; }
            }
        }
    }
}
