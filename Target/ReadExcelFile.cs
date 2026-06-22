using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Target
{
    class ReadExcelFile
    {
        public static DataSet ReadExcelFileForm()
        {
            // Set the License ContextD:\SAP B1 Add On Development\Fervent
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //string filePath = MapPath("SAP B1 Add On Development/Fervent/AreaTargetUploadFile.xlsx");//xlsx
            string filePath = Global.FilePath + "\\" + Global.FileName;

            //string filePath = MapPath("FIL_DatabaseDesign.xls");
            Global.FilePath = "";
            Global.FileName = "";
            DataSet ds = new DataSet();

            if (filePath != null && filePath != "")
            {
                using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(filePath)))
                {
                    var worksheets = package.Workbook.Worksheets;
                    if (worksheets.Count == 0)
                    {
                        MessageBox.Show("No worksheets found in the Excel file.");
                        //return;
                    }
                    for (int i = 0; i < worksheets.Count; i++)
                    {
                        DataTable table = new DataTable();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[i];
                        //string SheetName=worksheet.Name;
                        //SheetName = firstName + lastName;
                        table = GetExcelToDataTable(worksheet);
                        ds.Tables.Add(table);
                    }
                }
            }
            return ds;
        }

        public static DataTable GetExcelToDataTable(ExcelWorksheet worksheet)
        {
            DataTable table = new DataTable();
            bool hasHeader = true;
            //worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
            foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                table.Columns.Add(hasHeader ? firstRowCell.Text : $"Column {firstRowCell.Start.Column}");
            }

            for (int rowNumber = hasHeader ? 2 : 1; rowNumber <= worksheet.Dimension.End.Row; rowNumber++)
            {
                var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.End.Column];
                DataRow newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                table.Rows.Add(newRow);
            }
            return table;
        }
        public static string MapPath(string relativePath)
        {
            //Get the base directory where the executable is located
            //string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string rootPath = Path.GetPathRoot(Application.StartupPath);
            //string basePath = Application.StartupPath;
            //string rootPath2=System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            //string rootPath3 = System.IO.Path.GetDirectoryName(
            // System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            //string xxx = Environment.CurrentDirectory;
            //string xxx2 = System.IO.Directory.GetCurrentDirectory();
            //string xxx3 = System.IO.Path.GetDirectoryName(
            //         System.Reflection.Assembly.GetExecutingAssembly().Location);
            // Combine the base path with the relative path
            // return Path.Combine(basePath, relativePath.TrimStart('~', '/').Replace('/', '\\'));
            //string projectPath = Directory.GetCurrentDirectory();
            ////string projectPath = AppDomain.CurrentDomain.BaseDirectory;

            //// Assuming XML file is in the project folder
            //string xmlFilePath = Path.Combine(projectPath, FileName);
            string filePath = Path.Combine(rootPath, relativePath.Replace('/', '\\'));
            //filePath = filePath.Remove(67, 10);
            //filePath = filePath.Replace('bin', 'Debug');
            //filePath = filePath.Replace('Debug', '');
            // string filePath = Path.Combine(rootPath,"FIL_DatabaseDesign.xls");
            return filePath;
        }
    }
}
