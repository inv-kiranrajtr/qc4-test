using CsvHelper;
using log4net;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static Qc4Launcher.Util.Constants;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;
using ProgressBarForm = Qc4Launcher.Forms.ProgressBar;

namespace Qc4Launcher.Util
{
    public class FileUtil
    {
        private ProgressBarForm progress = null;
        Logger.Log LogObj;
        public delegate void OnWorkerMethodCompleteDelegate(double value, string status);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        Dictionary<string,int> PostFixNo = new Dictionary<string, int>();

        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public FileUtil()
        {
            LogObj = new Logger.Log();
        }

        public void Load(ref Excel.Application excelApp, ref Excel.Workbook excelWorkbook, string SelectedFile)
        {
            OnWorkerComplete(10, "Start loading file");
            try
            {
                OnWorkerComplete(11, "");
                LogObj.WriteLog("LaunchExcel", "Create excel application");
                OnWorkerComplete(15, "");
                excelApp = new Excel.Application(); //Marshal.GetActiveObject("Excel.Application") as Excel.Application;
                OnWorkerComplete(30, "");
                LogObj.WriteLog("LaunchExcel", "Excel Application Version " + excelApp.Version);
                OnWorkerComplete(31, "");
                LogObj.WriteLog("LaunchExcel", "Excel Application Name " + excelApp.Name);
                OnWorkerComplete(33, "");
                LogObj.WriteLog("LaunchExcel", "Excel Application OS " + excelApp.OperatingSystem);
                OnWorkerComplete(35, "");
                LogObj.WriteLog("LaunchExcel", "Excel Application ProductCode " + excelApp.ProductCode);
                OnWorkerComplete(37, "");
                LogObj.WriteLog("LaunchExcel", "Excel Application Build " + excelApp.Build.ToString());
                OnWorkerComplete(39, "");
                excelApp.DisplayAlerts = false;
                char a = (char)14;
                string pswrd = "MacroMill" + a + "!3";
                LogObj.WriteLog("LaunchExcel", "Open QC4 File in Excel");
                OnWorkerComplete(40, "");
                OnWorkerComplete(50, "");
                excelWorkbook = excelApp.Workbooks.Open(SelectedFile, 0, false, 5, pswrd, "", true,
                   Excel.XlPlatform.xlWindows, "\t", false, false, 0, false, 1, 0);
                OnWorkerComplete(90, "File Loading almost completed");
                LogObj.WriteLog("LaunchExcel", "Unprotect with Password");
                OnWorkerComplete(92, "File Loading completed");
                excelWorkbook.Unprotect(pswrd);
                OnWorkerComplete(94, "File Loading completed");
                LogObj.WriteLog("LaunchExcel", "Display Excel window");
                excelWorkbook.Saved = true;
                OnWorkerComplete(96, "File Loading completed");
                LogObj.WriteLog("LaunchExcel", "Add Excel Close Event Listener");
                OnWorkerComplete(98, "File Loading completed");
                LogObj.WriteLog("LaunchExcel", "Excel launcher returning");
                OnWorkerComplete(100, "");
            }
            catch (Exception ex)
            {
                excelWorkbook = null;
                LogObj.WriteLog("LaunchExcel", "Exception in Excel Launching----- Message - " + ex.Message, Logger.Log.Level.Error);
                MessageDialog.ErrorOk(LocalResource.MSG_FILE_CANNOT_PROCESS);
                OnWorkerComplete(100, "File loading failed");
            }
        }

        public void AddWorkBook(string SelectedFile, Excel.Application excelApp = null)
        {
            OnWorkerComplete(10, "Start loading file");
            try
            {
                if (excelApp == null)
                {
                    excelApp = new Excel.Application();
                }
                Excel.Workbook excelWorkbook;
                LogObj.WriteLog("LaunchExcel", "Excel Application Version " + excelApp.Version);
                LogObj.WriteLog("LaunchExcel", "Excel Application Name " + excelApp.Name);
                LogObj.WriteLog("LaunchExcel", "Excel Application OS " + excelApp.OperatingSystem);
                LogObj.WriteLog("LaunchExcel", "Excel Application ProductCode " + excelApp.ProductCode);
                LogObj.WriteLog("LaunchExcel", "Excel Application Build " + excelApp.Build.ToString());
                excelApp.DisplayAlerts = false;
                char a = (char)14;
                string pswrd = "MacroMill" + a + "!3";
                LogObj.WriteLog("LaunchExcel", "Open QC4 File in Excel");
                excelWorkbook = excelApp.Workbooks.Open(SelectedFile, 0, false, 5, pswrd, "", true,
                   Excel.XlPlatform.xlWindows, "\t", false, false, 0, false, 1, 0);
                LogObj.WriteLog("LaunchExcel", "Unprotect with Password");
                excelWorkbook.Unprotect(pswrd);
                LogObj.WriteLog("LaunchExcel", "Display Excel window");
                excelWorkbook.Saved = true;
                LogObj.WriteLog("LaunchExcel", "Add Excel Close Event Listener");
                LogObj.WriteLog("LaunchExcel", "Excel launcher returning");
            }
            catch (Exception ex)
            {
                LogObj.WriteLog("LaunchExcel", "Exception in Excel Launching----- Message - " + ex.Message, Logger.Log.Level.Error);
            }
        }


        internal void SaveFile(Excel.Workbook workbook, string path, System.Windows.Window ParentWindow)
        {
            progress = new ProgressBarForm();
            progress.Owner = ParentWindow;
            this.OnWorkerComplete += new FileUtil.OnWorkerMethodCompleteDelegate(OnWorkerMethodComplete);
            OnWorkerComplete(10, "Start saving file");
            new Thread(() => SaveFile(workbook, path)).Start();
            OnWorkerComplete(100, "File Saved");
            progress.ShowDialog();
        }

        internal static void SaveFile(Excel.Workbook workbook, string path)
        {
            //string destPath = System.IO.Path.GetDirectoryName(path) + "\\" + System.IO.Path.GetFileNameWithoutExtension(path) + ".qc4";
            string ext = ".qc4";
            string destPath = System.IO.Path.GetDirectoryName(path);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            string fullPath = destPath + "\\" + fileName + ext;
            if (File.Exists(fullPath))
            {
                fullPath = GenerateFileName(1, ext, fileName, destPath);
            }
            workbook.SaveAs(fullPath);
        }

        internal static string GenerateFileName(int count, string ext, string fileName, string filePath)
        {
            string fullPath = filePath + "\\" + fileName + "(" + (count++) + ")" + ext;
            if (File.Exists(fullPath))
            {
                fullPath = GenerateFileName(count, ext, fileName, filePath);
            }
            return fullPath;
        }

        private void OnWorkerMethodComplete(double value, string status)
        {
            progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new System.Action(
            delegate ()
            {
                progress.UpdateProgressBar(value, status);
            }
            ));
        }

        public void UpdateProgressBar(double value, string status = "")
        {
            OnWorkerMethodComplete(value, status);
        }

        internal static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = System.IO.Path.Combine(destDirName, file.Name);
                try
                {
                    file.CopyTo(temppath, true);
                }
                catch (Exception ex)
                {
                    if (!(ex is System.IO.IOException))
                    {
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                }
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private DataTable ReadDataFromText(string filePath, char colDelimitter, Encoding encoding, string quoteType)
        {

            DataTable dtCsv = new DataTable();
            string Fulltext;
            using (StreamReader sr = new StreamReader(filePath, encoding))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(colDelimitter); //split each row with comma to get individual values  
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j]); //add headers  
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = rowValues[k].ToString();
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                }
            }
            return dtCsv;
        }

        public string GetNewColumnName(DataTable dt, string text = DatatableSettings.DummyColumnName, int postFix = 1)
        {
            if (text == null || text.Trim().Length == 0) text = DatatableSettings.DummyColumnName;

            if (dt.Columns.Contains(text))
            {
                if (dt.Columns.Contains(text + " - " + postFix.ToString()))
                {
                    if (PostFixNo.ContainsKey(text))
                    {
                        postFix = PostFixNo[text] + 1;
                        PostFixNo[text] = PostFixNo[text] + 1;
                    }
                    else
                    {
                        postFix++;
                        PostFixNo.Add(text, postFix);
                    }
                    return GetNewColumnName(dt, text, postFix);
                }
                else
                {
                    return text + " - " + postFix.ToString();
                }
            }
            else
            {
                return text;
            }
        }

        public string GetNewColumnName(List<string> columnNames, string text = DatatableSettings.DummyColumnName, int postFix = 1)
        {
            if (text == null || text.Trim().Length == 0) text = DatatableSettings.DummyColumnName;

            if (columnNames.Contains(text))
            {
                if (columnNames.Contains(text + " - " + postFix.ToString()))
                {
                    if (PostFixNo.ContainsKey(text))
                    {
                        postFix = PostFixNo[text] + 1;
                        PostFixNo[text] = PostFixNo[text] + 1;
                    }
                    else
                    {
                        postFix++;
                        PostFixNo.Add(text, postFix);
                    }
                    return GetNewColumnName(columnNames, text, postFix);
                }
                else
                {
                    return text + " - " + postFix.ToString();
                }
            }
            else
            {
                return text;
            }
        }

        public DataTable ReadDataFromExcel(string filePath, int sheetNo = 1, int skipRows = 0, ProgressBarForm pb = null)
        {
            DataTable dtExcel = new DataTable();

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            var autoMationSecurity = xlApp.AutomationSecurity;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityForceDisable;
            xlApp.EnableEvents = false;

            Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Worksheet xlWorksheet = xlWorkbook.Sheets[sheetNo];
            Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            try
            {
                object[,] data = xlRange.Value;
                for (int i = (skipRows + 1); i <= rowCount; i++)
                {
                    if (i == (skipRows + 1)) // First row - Taking as header
                    {
                        for (int j = 1; j <= colCount; j++) //
                        {
                            if (data[i, j] == null)
                            {
                                dtExcel.Columns.Add(GetNewColumnName(dtExcel, DatatableSettings.DummyColumnName, 1), typeof(string));
                            }
                            else
                            {
                                if (dtExcel.Columns.Contains(Convert.ToString(data[i, j])))
                                {
                                    dtExcel.Columns.Add(GetNewColumnName(dtExcel, Convert.ToString(data[i, j])));
                                }
                                else
                                {
                                    dtExcel.Columns.Add(Convert.ToString(data[i, j]));
                                }

                                //var dateFormat = xlRange.Columns[j].NumberFormat;
                                //try
                                //{
                                //    DateTime systime = DateTime.Now.ToString(dateFormat);
                                //}
                                //catch (Exception ex)
                                //{

                                //}

                            }
                        }
                    }
                    else
                    {
                        DataRow dr = dtExcel.NewRow();
                        for (int j = 1; j <= colCount; j++) //
                        {
                            dr[j - 1] = Convert.ToString(data[i, j]);
                        }
                        dtExcel.Rows.Add(dr);
                    }

                }
            }
            catch (Exception ex)
            {
                dtExcel = new DataTable();
                LogObj.WriteLog("ReadDataFromExcel", ex.Message, Logger.Log.Level.Error);
                MessageBox.Show(LocalResource.FILE_CANNOT_READ, "QuickCross"); // Could not read file
            }
            finally
            {
                xlApp.AutomationSecurity = autoMationSecurity;
                xlApp.EnableEvents = true;
                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //rule of thumb for releasing com objects:
                //  never use two dots, all COM objects must be referenced and released individually
                //  ex: [somthing].[something].[something] is bad

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release

                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);

                if (pb != null)
                {
                    pb.Dispatcher.Invoke(() =>
                    {
                        pb.Close();
                    });
                }
            }

            return dtExcel;

        }

        public DataTable ReadDataFromSheet(Workbook xlWorkbook, string sheetName, int skipRows = 0)
        {
            DataTable dtExcel = new DataTable();
            Worksheet xlWorksheet = ExcelUtil.GetWorkSheetBySheetName(xlWorkbook, sheetName);
            Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            try
            {
                object[,] data = xlRange.Value2;
                for (int i = (skipRows + 1); i <= rowCount; i++)
                {
                    if (i == (skipRows + 1)) // First row - Taking as header
                    {
                        for (int j = 1; j <= colCount; j++) //
                        {
                            if (data[i, j] == null)
                            {
                                dtExcel.Columns.Add(GetNewColumnName(dtExcel));
                            }
                            else
                            {
                                if (dtExcel.Columns.Contains(Convert.ToString(data[i, j])))
                                {
                                    dtExcel.Columns.Add(GetNewColumnName(dtExcel, Convert.ToString(data[i, j])));
                                }
                                else
                                {
                                    dtExcel.Columns.Add(Convert.ToString(data[i, j]));
                                }
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = dtExcel.NewRow();
                        for (int j = 1; j <= colCount; j++) //
                        {
                            dr[j - 1] = Convert.ToString(data[i, j]);
                        }
                        dtExcel.Rows.Add(dr);
                    }

                }
            }
            catch (Exception ex)
            {
                dtExcel = new DataTable();
                LogObj.WriteLog("ReadDataFromSheet", ex.Message, Logger.Log.Level.Error);
            }
            finally
            {
                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);
            }

            return dtExcel;

        }

        public DataTable ReadDataFromSheet(Worksheet xlWorksheet, int skipRows = 0, bool limitColumns = false, int limitColumnsUpto = 10)
        {
            DataTable dtExcel = new DataTable();
            Excel.Range targetCell = xlWorksheet.Cells[1, 2];
            Excel.Range lastDataCell = ExcelUtil.EndxlUp(targetCell);
            Excel.Range xlRange = xlWorksheet.Range[targetCell.Offset[0, -1], lastDataCell.Offset[0,1034]];

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            int insertedColumns = 0;
            try
            {
                object[,] data = xlRange.Value2;
                for (int i = (skipRows + 1); i <= rowCount; i++)
                {
                    if (i == (skipRows + 1)) // First row - Taking as header
                    {
                        for (int j = 1; j <= colCount; j++) //
                        {
                            if (limitColumns && insertedColumns >= limitColumnsUpto)
                                break;
                            if (data[i, j] == null)
                            {
                                dtExcel.Columns.Add(GetNewColumnName(dtExcel));
                            }
                            else
                            {
                                if (dtExcel.Columns.Contains(Convert.ToString(data[i, j])))
                                {
                                    dtExcel.Columns.Add(GetNewColumnName(dtExcel, Convert.ToString(data[i, j])));
                                }
                                else
                                {
                                    dtExcel.Columns.Add(Convert.ToString(data[i, j]));
                                }
                                insertedColumns++;
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = dtExcel.NewRow();
                        insertedColumns = 0;
                        for (int j = 1; j <= colCount; j++) //
                        {
                            if (limitColumns && insertedColumns >= limitColumnsUpto)
                                break;
                            dr[j - 1] = Convert.ToString(data[i, j]);
                            insertedColumns++;
                        }
                        dtExcel.Rows.Add(dr);
                    }

                }
            }
            catch (Exception ex)
            {
                dtExcel = new DataTable();
                LogObj.WriteLog("ReadDataFromSheet", ex.Message, Logger.Log.Level.Error);
            }
            finally
            {
                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Marshal.ReleaseComObject(xlRange);
            }

            return dtExcel;
        }

        public DataTable ReadDataFromExcel(string filePath, string sheetCodeName, int skipRows = 0)
        {
            DataTable dtExcel = new DataTable();
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            xlApp.EnableEvents = false;
            Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Worksheet xlWorksheet = ExcelUtil.GetWorkSheetByCodeName(xlWorkbook, sheetCodeName);
            Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            try
            {
                object[,] data = xlRange.Value2;
                for (int i = (skipRows + 1); i <= rowCount; i++)
                {
                    if (i == (skipRows + 1)) // First row - Taking as header
                    {
                        for (int j = 1; j <= colCount; j++) //
                        {
                            if (data[i, j] == null)
                            {
                                dtExcel.Columns.Add(GetNewColumnName(dtExcel));
                            }
                            else
                            {
                                if (dtExcel.Columns.Contains(Convert.ToString(data[i, j])))
                                {
                                    dtExcel.Columns.Add(GetNewColumnName(dtExcel, Convert.ToString(data[i, j])));
                                }
                                else
                                {
                                    dtExcel.Columns.Add(Convert.ToString(data[i, j]));
                                }
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = dtExcel.NewRow();
                        for (int j = 1; j <= colCount; j++) //
                        {
                            dr[j - 1] = Convert.ToString(data[i, j]);
                        }
                        dtExcel.Rows.Add(dr);
                    }

                }
            }
            catch (Exception ex)
            {
                dtExcel = new DataTable();
                LogObj.WriteLog("ReadDataFromExcel", ex.Message, Logger.Log.Level.Error);
            }
            finally
            {
                xlApp.EnableEvents = true;

                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //rule of thumb for releasing com objects:
                //  never use two dots, all COM objects must be referenced and released individually
                //  ex: [somthing].[something].[something] is bad

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }

            return dtExcel;

        }


        public DataTable ReadDataFromSource(string filePath, string colDelimitter, Encoding encoding, char? enclosingChar)
        {
            var dt = new DataTable();
            try
            {
                using (var reader = new StreamReader(@filePath, encoding))
                using (var csvReader = new CsvReader(reader))
                {
                    csvReader.Configuration.Delimiter = colDelimitter;
                    csvReader.Configuration.Encoding = encoding;
                    if (enclosingChar != null)
                    {
                        csvReader.Configuration.Quote = (char)enclosingChar;
                    }
                    csvReader.Configuration.BadDataFound = null;
                    // Do any configuration to `CsvReader` before creating CsvDataReader.
                    using (var dr = new CsvDataReader(csvReader))
                    {
                        dt.Load(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                LogObj.WriteLog("ReadDataFromSource", ex.Message, Logger.Log.Level.Error);
                dt = new DataTable();
                //MessageBox.Show("Invalid file data", "Warning");
            }

            return dt;
        }

        public List<List<KeyValueObject>> ReadDataFromSource2(string filePath, string colDelimitter, Encoding encoding, char? enclosingChar)
        {
            List<List<KeyValueObject>> keyValueObjectsList = new List<List<KeyValueObject>>();
            int rowNum = 1;
            try
            {
                using (var reader = new StreamReader(@filePath, encoding))
                using (var csvReader = new CsvReader(reader))
                {
                    csvReader.Configuration.Delimiter = colDelimitter;
                    csvReader.Configuration.Encoding = encoding;
                    if (enclosingChar != null)
                    {
                        csvReader.Configuration.Quote = (char)enclosingChar;
                    }
                    csvReader.Configuration.BadDataFound = null;
                    csvReader.Configuration.HasHeaderRecord = false;

                    // Do any configuration to `CsvReader` before creating CsvDataReader.
                    List<string> columnNames = new List<string>();

                    while (csvReader.Read())
                    {
                        List<KeyValueObject> keyValueObjects = new List<KeyValueObject>();
                        string value = null;
                        int columnIndex = 0;
                        for (int i = 0; csvReader.TryGetField<string>(i, out value); i++)
                        {

                            if (rowNum == 1)
                            {
                                columnNames.Add(GetNewColumnName(columnNames, value));
                            }
                            else
                            {
                                KeyValueObject keyValueObject = new KeyValueObject();
                                if ((columnIndex + 1) > columnNames.Count)
                                {
                                    string newColumnName = GetNewColumnName(columnNames);
                                    columnNames.Add(newColumnName);
                                }

                                keyValueObject.Key = columnNames[columnIndex];
                                keyValueObject.Value = value;
                                keyValueObjects.Add(keyValueObject);
                            }
                            columnIndex++;
                        }

                        //keyValueObjectsList.Add(keyValueObjects);

                        if (rowNum == 100000)
                        {
                            //Do Something
                        }

                        rowNum++;
                    }

                    // Do something

                    //
                }

                LogObj.WriteLog("ReadDataFromSource2", "Found " + rowNum.ToString() + " records");
            }
            catch (Exception ex)
            {
                LogObj.WriteLog("ReadDataFromSource2", ex.StackTrace);
            }
            return keyValueObjectsList;
        }


        //public List<List<KeyValueObject>> ReadDataFromSource2(string filePath, string colDelimitter, Encoding encoding, char? enclosingChar)
        //{
        //    List<List<KeyValueObject>> keyValueObjectsList = new List<List<KeyValueObject>>();
        //    int rowNum = 1;
        //    try
        //    {
        //        using (var reader = new StreamReader(@filePath, encoding))
        //        using (var csvReader = new CsvReader(reader))
        //        {
        //            csvReader.Configuration.Delimiter = colDelimitter;
        //            csvReader.Configuration.Encoding = encoding;
        //            if (enclosingChar != null)
        //            {
        //                csvReader.Configuration.Quote = (char)enclosingChar;
        //            }
        //            csvReader.Configuration.BadDataFound = null;
        //            // Do any configuration to `CsvReader` before creating CsvDataReader.
        //            List<string> columnNames = new List<string>();

        //            while (csvReader.Read())
        //            {
        //                List<KeyValueObject> keyValueObjects = new List<KeyValueObject>();

        //                string value = null;
        //                int columnIndex = 0;
        //                for (int i = 0; csvReader.TryGetField<string>(i, out value); i++)
        //                {

        //                    if (rowNum == 1)
        //                    {
        //                        columnNames.Add(GetNewColumnName(columnNames, value));
        //                    }
        //                    else
        //                    {
        //                        KeyValueObject keyValueObject = new KeyValueObject();
        //                        if ((columnIndex + 1) > columnNames.Count)
        //                        {
        //                            string newColumnName = GetNewColumnName(columnNames);
        //                            columnNames.Add(newColumnName);
        //                        }

        //                        keyValueObject.Key = columnNames[columnIndex];
        //                        keyValueObject.Value = value;
        //                        keyValueObjects.Add(keyValueObject);
        //                    }
        //                    columnIndex++;
        //                }
        //                try
        //                {
        //                    keyValueObjectsList.Add(keyValueObjects);
        //                }
        //                catch (Exception e)
        //                {
        //                    if (e.Message.Contains("System.OutOfMemoryException"))
        //                    {
        //                        LogObj.WriteLog("ReadDataFromSource2", "Read from File - Out of memory while exceeding : " + keyValueObjectsList.Count + " records");
        //                        keyValueObjectsList = new List<List<KeyValueObject>>();
        //                        keyValueObjectsList.Add(keyValueObjects);
        //                    }
        //                    else
        //                    {
        //                        throw;
        //                    }
        //                }
        //                rowNum++;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogObj.WriteLog("ReadDataFromSource2", ex.Message + "/n" + ex.StackTrace);
        //    }
        //    return keyValueObjectsList;
        //}



        //public DataTable ReadDataFromSource(string filePath, string colDelimitter, Encoding encoding, char? enclosingChar)
        //{
        //    return Classes.TextParser.ExecuteParse(filePath,colDelimitter,encoding,enclosingChar);
        //}


        public string ReadAllTextFromFile(string filePath, Encoding encoding)
        {
            string fileText = File.ReadAllText(@filePath, encoding);
            return fileText;
        }

        public string ReadAllTextFromFile(string filePath, Encoding encoding, int skipLines, int takeLines)
        {
            var lines = File.ReadLines(@filePath, encoding).Skip(skipLines).Take(takeLines).ToArray(); // Take line limits row count
            string fileText = string.Join("\n", lines);
            return fileText;
        }


    }
}
