using CsvHelper;
using log4net;
using Qc4Launcher.DB;
using Qc4Launcher.Model;
using QcWebCommon.Sheets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Qc4Launcher.Util.Constants;
using static Qc4Launcher.Util.Enums;
using Excel = Microsoft.Office.Interop.Excel;
using Qc4Launcher.Forms;
using System.Runtime.InteropServices;
using Constant = ExcelAddIn.Common.Constants;
using QuestionSettings = QC4Common.Model.QuestionSettings;
using System.Windows.Threading;
using NPOI.SS.UserModel;

namespace Qc4Launcher.Util
{
    public class DataImportHelper
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal delegate void OnWorkerMethodCompleteDelegate(double value, string status);

        internal event OnWorkerMethodCompleteDelegate OnWorkerComplete = null;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        internal static int UpdateProgressStartPercentage = 2;
        internal static int UpdateProgressEndPercentage = 95;
        Excel.Workbook OpenWbk = null;
        Excel.Application xlApp = null;
        Microsoft.Office.Core.MsoAutomationSecurity autoMationSecurity;
        public List<string> TempColumnNames = new List<string>();

        private int UpdateProgressMakeDtAllocate
        {
            get { return UpdateProgressLimit * 20 / 100; }
        }

        private int UpdateProgressLimit
        {
            get { return UpdateProgressEndPercentage - UpdateProgressStartPercentage; }
        }

        private int UpdateProgressParseLimit
        {
            get { return UpdateProgressLimit - UpdateProgressMakeDtAllocate; }
        }

        private bool CreateTempDataImportTable(SQLiteCommand sQLiteCommand, List<string> columnNames)
        {
            try
            {
                string sSql = "CREATE TABLE IF NOT EXISTS `" + DataImportSettings.DataImportSourceTempTable + "`";
                sSql += "(";

                int ColumnLimit = columnNames.Count;
                if (ColumnLimit > DBSettings.MaxNoOfColumnInsertInBulk) ColumnLimit = DBSettings.MaxNoOfColumnInsertInBulk;

                for (int i = 0; i <= ColumnLimit - 1; i++)
                {
                    sSql += "`" + columnNames[i] + "` " + DataImportSettings.TempTableVarcharDataType + " ";
                    if (ColumnLimit > (i + 1))
                    {
                        sSql += ",";
                    }
                }
                sSql += ")";

                sQLiteCommand.CommandText = sSql;
                sQLiteCommand.ExecuteNonQuery();

                for (int i = ColumnLimit; i <= columnNames.Count - 1; i++)
                {
                    TempTableAddNewColumn(sQLiteCommand, columnNames[i]); // adding new column if not exist
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        private bool TempTableAddNewColumn(SQLiteCommand sQLiteCommand, string columnName)
        {
            try
            {
                string sSql = "ALTER TABLE `" + DataImportSettings.DataImportSourceTempTable + "` ADD ";
                sSql += " `" + columnName + "` " + DataImportSettings.TempTableVarcharDataType + " ";
                sQLiteCommand.CommandText = sSql;
                sQLiteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool DropTempTable(string DbPath)
        {
            try
            {
                using (SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(DbPath)))
                {
                    con.Open();
                    using (SQLiteCommand sQLiteCommand = con.CreateCommand())
                    {
                        string sSql = "DROP TABLE IF EXISTS " + DataImportSettings.DataImportSourceTempTable;
                        sQLiteCommand.CommandText = sSql;
                        sQLiteCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        private bool DropTempTable(SQLiteCommand sQLiteCommand)
        {
            string sSql = "DROP TABLE IF EXISTS " + DataImportSettings.DataImportSourceTempTable;
            sQLiteCommand.CommandText = sSql;
            sQLiteCommand.ExecuteNonQuery();
            return true;
        }

        private bool TempTableValueInsert(SQLiteCommand sQLiteCommand, List<KeyValueObject> keyValueObjects)
        {
            int ColumnLimit = keyValueObjects.Count;
            int givenCount = keyValueObjects.Count;
            int columnNum = 0;
            string sSql = "";
            int rowid = 0;
            while (givenCount != 0)
            {
                if (givenCount > DBSettings.MaxNoOfColumnInsertInBulk)
                {
                    ColumnLimit = DBSettings.MaxNoOfColumnInsertInBulk;
                    givenCount = givenCount - DBSettings.MaxNoOfColumnInsertInBulk;
                }
                else
                {
                    ColumnLimit = givenCount;
                    givenCount = 0;
                }
                string columns = "";
                string values = "";

                sQLiteCommand.Parameters.Clear();
                if (sSql == "")
                {
                    for (int i = 0; i <= ColumnLimit - 1; i++)
                    {
                        string paramName = "@param" + (i + 1);
                        columns = columns + "`" + keyValueObjects[columnNum].Key + "`";
                        values = values + paramName;
                        string value = keyValueObjects[columnNum].Value == null ? "" : keyValueObjects[columnNum].Value;
                        sQLiteCommand.Parameters.Add(new SQLiteParameter(paramName, value));
                        if (ColumnLimit > (i + 1))
                        {
                            columns = columns + ",";
                            values = values + ",";
                        }
                        columnNum++;
                    }
                    sSql = " Insert Into `" + DataImportSettings.DataImportSourceTempTable + "`";
                    sSql += "(";
                    sSql += columns;
                    sSql += ")";
                    sSql += " Values(";
                    sSql += values;
                    sSql += ");SELECT last_insert_rowid();";
                    sQLiteCommand.CommandText = sSql;
                    rowid = Convert.ToInt32(sQLiteCommand.ExecuteScalar());
                }
                else
                {
                    sSql = " Update `" + DataImportSettings.DataImportSourceTempTable + "` Set  ";
                    for (int i = 0; i <= ColumnLimit - 1; i++)
                    {
                        string paramName = "@param" + i;
                        sSql += " `" + keyValueObjects[columnNum].Key + "` = " + paramName + " ";

                        sQLiteCommand.Parameters.Add(new SQLiteParameter(paramName, keyValueObjects[columnNum].Value));
                        if (ColumnLimit > (i + 1))
                        {
                            sSql += ",";
                        }
                        columnNum++;
                    }
                    sSql += " where `" + DataImportSettings.DataImportSourceTempTable + "`.ROWID = " + rowid;
                    sQLiteCommand.CommandText = sSql;
                    sQLiteCommand.ExecuteNonQuery();
                }
            }
            return true;
        }

        private DataTable GetDataFromTempImportTable(string DbPath, bool limitRows = false, int rowLimit = 100)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(DbPath)))
            {
                try
                {
                    dbSource.Open();
                    string sql;
                    sql = "Select * from " + DataImportSettings.DataImportSourceTempTable;

                    if (limitRows)
                    {
                        sql += " Limit " + rowLimit;
                    }
                    dt = DBHelper.GetDataTable(sql, dbSource);
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace);
                    dt = new DataTable();
                }
                finally
                {
                    dbSource.Close();
                }
            }
            return dt;
        }

        public bool InsertSourceFileContentsToTempTable(string DbPath, string filePath, string colDelimitter, Encoding encoding, char? enclosingChar, bool limitRowInsert = false, int rowLimitInsert = 100)
        {
            bool isSuccess = false;
            FileUtil fileUtil = new FileUtil();
            int rowNum = 1;
            try
            {
                int tempPerc = UpdateProgressStartPercentage + (UpdateProgressParseLimit * 10 / 100);
                OnWorkerComplete(tempPerc, LocalResource.IM_PROGRESSBAR_READING);
                filePath = GetTempSourceFilePath(filePath);

                int orgLineCount = File.ReadAllLines(@filePath).Count();
                int limitLineCount = orgLineCount;
                if (limitRowInsert) if (limitLineCount > rowLimitInsert) limitLineCount = rowLimitInsert + 1; //+1 means +header row
                tempPerc = tempPerc + (UpdateProgressParseLimit * 10 / 100);
                OnWorkerComplete(tempPerc, String.Format(LocalResource.IM_PROGRESSBAR_CACHEING_LINES, limitLineCount - 1));
                double incRate = (double)limitLineCount / orgLineCount;
                int columnCount = -1;

                List<List<KeyValueObject>> keyValueObjectsList = new List<List<KeyValueObject>>();
                using (var reader = new StreamReader(@filePath, encoding))
                using (var csvReader = new CsvReader(reader))
                {
                    if (!(colDelimitter != null && colDelimitter.Length > 0))
                    {
                        colDelimitter = DatatableSettings.DummyColumnSpecifier;
                    }
                    csvReader.Configuration.Delimiter = colDelimitter;
                    csvReader.Configuration.Encoding = encoding;
                    if (enclosingChar != null)
                    {
                        csvReader.Configuration.IgnoreQuotes = false;
                        csvReader.Configuration.Quote = (char)enclosingChar;
                    }
                    else
                    {
                        csvReader.Configuration.IgnoreQuotes = true;
                    }
                    csvReader.Configuration.BadDataFound = null;
                    csvReader.Configuration.HasHeaderRecord = false;
                    csvReader.Configuration.IgnoreBlankLines = false;

                    List<string> columnNames = new List<string>();

                    //transaction start
                    using (SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(DbPath)))
                    {
                        con.Open();
                        using (SQLiteTransaction tr = con.BeginTransaction())
                        {
                            using (SQLiteCommand sQLiteCommand = con.CreateCommand())
                            {
                                sQLiteCommand.Transaction = tr;
                                try
                                {
                                    double inputProcessed = -1;
                                    int childProgress = 0;

                                    while (csvReader.Read())
                                    {

                                        if (inputProcessed > 1)
                                        {
                                            double progressChildPerc = inputProcessed / (limitLineCount) * 100;
                                            childProgress = Convert.ToInt32(70 * progressChildPerc / 100); //70% allocated for reading
                                            var ProgressVal = tempPerc + childProgress;
                                            OnWorkerComplete(ProgressVal, String.Format(LocalResource.IM_PROGRESSBAR_CACHEING_DATA, (int)inputProcessed + "/" + (limitLineCount - 1)));
                                        }
                                        inputProcessed += incRate;

                                        List<KeyValueObject> keyValueObjects = new List<KeyValueObject>();
                                        string value = null;
                                        int columnIndex = 0;
                                        for (int i = 0; csvReader.TryGetField<string>(i, out value); i++)
                                        {
                                            if (rowNum == 1 && value == null)
                                            {
                                                columnCount = i;
                                                break;
                                            }
                                            if (columnCount != -1 && i == columnCount)
                                                break;
                                            if (rowNum == 1)
                                            {
                                                columnNames.Add(fileUtil.GetNewColumnName(columnNames, value));
                                            }
                                            else
                                            {
                                                KeyValueObject keyValueObject = new KeyValueObject();
                                                if ((columnIndex + 1) > columnNames.Count)
                                                {
                                                    string newColumnName = fileUtil.GetNewColumnName(columnNames);
                                                    columnNames.Add(newColumnName);
                                                    TempTableAddNewColumn(sQLiteCommand, newColumnName); // adding new column if not exist
                                                }

                                                keyValueObject.Key = columnNames[columnIndex];
                                                keyValueObject.Value = value;
                                                keyValueObjects.Add(keyValueObject);
                                            }
                                            columnIndex++;
                                        }

                                        if (rowNum > 1)
                                        {
                                            if (limitRowInsert)
                                            {
                                                if (rowNum <= (rowLimitInsert + 1))
                                                {
                                                    keyValueObjectsList.Add(keyValueObjects);
                                                }
                                            }
                                            else
                                            {
                                                keyValueObjectsList.Add(keyValueObjects);
                                            }
                                        }
                                        else
                                        {
                                            DropTempTable(sQLiteCommand);
                                            if (!CreateTempDataImportTable(sQLiteCommand, columnNames)) // creating temp table.
                                            {
                                                throw new Exception("Failed to create temp table");
                                            }
                                        }

                                        if ((rowNum % DBSettings.BulkDataInsertMaxRecordPerTrans) == 0)
                                        {
                                            if (InsertToTempTable(sQLiteCommand, keyValueObjectsList))
                                            {
                                                keyValueObjectsList.Clear();
                                            }
                                            else
                                            {
                                                throw new Exception("Failed to insert data to temp table");
                                            }
                                        }

                                        rowNum++;
                                    }

                                    if (InsertToTempTable(sQLiteCommand, keyValueObjectsList))
                                    {
                                        keyValueObjectsList.Clear();
                                    }
                                    else
                                    {
                                        throw new Exception("Failed to insert data to temp table");
                                    }
                                    tr.Commit();
                                    OnWorkerComplete(UpdateProgressStartPercentage + UpdateProgressParseLimit, LocalResource.IM_PROGRESSBAR_COMPLETING_CACHEING);
                                }
                                catch (Exception e)
                                {
                                    _log.LogError(e.Message + "\n" + e.StackTrace);
                                    tr.Rollback();
                                    throw;
                                }
                                finally
                                {
                                    OnWorkerComplete(UpdateProgressStartPercentage + UpdateProgressParseLimit, LocalResource.IM_PROGRESSBAR_COMPLETING_CACHEING);
                                }
                            }
                        }
                    }
                }
                _log.Info("Inserted " + (rowNum - 1).ToString() + " records from source file");
                isSuccess = true;
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                OnWorkerComplete(UpdateProgressEndPercentage, LocalResource.IM_PROGRESSBAR_ENDING);
            }

            return isSuccess;
        }

        private bool InsertToTempTable(SQLiteCommand sQLiteCommand, List<List<KeyValueObject>> keyValueObjectsList)
        {
            bool isSuccess = true;

            foreach (List<KeyValueObject> keyValueObjects in keyValueObjectsList)
            {
                TempTableValueInsert(sQLiteCommand, keyValueObjects);
            }

            return isSuccess;
        }


        public bool InsertExcelContentsToTempTable(string DbPath, string filePath, int sheetNo = 1, int skipRows = 0, bool limitRowInsert = false, int rowLimitInsert = 100)
        {
            bool Issuccess = true;
            Excel.Workbook xlWorkbook = OpenWbk;
            Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[sheetNo];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            FileUtil fileUtil = new FileUtil();

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            List<List<KeyValueObject>> keyValueObjectsList = new List<List<KeyValueObject>>();
            try
            {
                object[,] data = xlRange.Value;
                int noDataCount = 0;
                for (int i = rowCount; i >= (skipRows + 1); i--)
                {
                    List<string> colData = new List<string>();
                    for (int j = 1; j <= colCount; j++)
                    {
                        string columnData = Convert.ToString(data[i, j]);
                        colData.Add(columnData.Trim());
                    }
                    if (colData.Any(x => x != ""))
                        break;
                    noDataCount++;
                }
                rowCount -= noDataCount;

                int limitLineCount = rowCount;
                if (limitRowInsert && limitLineCount > rowLimitInsert) limitLineCount = rowLimitInsert + 1; //+1 means +header row
                int tempPerc = UpdateProgressStartPercentage + (UpdateProgressParseLimit * 10 / 100);
                OnWorkerComplete(tempPerc, String.Format(LocalResource.IM_PROGRESSBAR_CACHEING_ROWS, (limitLineCount - 1)));
                double incRate = (double)limitLineCount / rowCount;
                long startRow = xlRange.Row - 1;
                int starttCol = xlRange.Column - 1;
                Excel.Range firstFind = null;
                string[] excelErrors = { QC4Common.Common.Constants.ExcelDiv, "#NAME?", "#VALUE!", "#REF!", "#NUM!", "#NULL!" };
                foreach (string a in excelErrors)
                {
                    string errorText = a;
                    Excel.Range result = xlRange.Find(What: errorText, LookAt: Excel.XlLookAt.xlWhole, SearchDirection: Excel.XlSearchDirection.xlNext);
                    while (result != null)
                    {
                        if (firstFind == null)
                        {
                            firstFind = result;
                        }
                        else if (result.get_Address(Excel.XlReferenceStyle.xlA1)
                              == firstFind.get_Address(Excel.XlReferenceStyle.xlA1))
                        {
                            break;
                        }
                        string errorValue = result.Text;
                        data[result.Row - startRow, result.Column - starttCol] = errorValue;

                        result = xlRange.FindNext(result);
                    }
                    firstFind = null;
                }

                List<string> columnNames = new List<string>();
                using (SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(DbPath)))
                {
                    con.Open();
                    using (SQLiteTransaction tr = con.BeginTransaction())
                    {
                        using (SQLiteCommand sQLiteCommand = con.CreateCommand())
                        {
                            sQLiteCommand.Transaction = tr;
                            try
                            {
                                double inputProcessed = -1;
                                int childProgress = 0;

                                int rowNum = 1;
                                for (int i = (skipRows + 1); i <= rowCount; i++)
                                {
                                    if (inputProcessed > 1)
                                    {
                                        double progressChildPerc = inputProcessed / (limitLineCount) * 100;
                                        childProgress = Convert.ToInt32(80 * progressChildPerc / 100); //80% allocated for reading
                                        var ProgressVal = tempPerc + childProgress;
                                        OnWorkerComplete(ProgressVal, String.Format(LocalResource.IM_PROGRESSBAR_CACHEING_DATA, (int)inputProcessed + "/" + (limitLineCount - 1)));
                                    }
                                    inputProcessed += incRate;

                                    List<KeyValueObject> keyValueObjects = new List<KeyValueObject>();
                                    int columnIndex = 0;
                                    for (int j = 1; j <= colCount; j++)
                                    {
                                        string columnData = Convert.ToString(data[i, j]);
                                        if (i == (skipRows + 1)) // First row - Taking as header
                                        {
                                            columnNames.Add(fileUtil.GetNewColumnName(columnNames, columnData));
                                        }
                                        else
                                        {
                                            KeyValueObject keyValueObject = new KeyValueObject();
                                            if ((columnIndex + 1) > columnNames.Count)
                                            {
                                                string newColumnName = fileUtil.GetNewColumnName(columnNames);
                                                columnNames.Add(newColumnName);
                                                TempTableAddNewColumn(sQLiteCommand, newColumnName);
                                            }

                                            keyValueObject.Key = columnNames[columnIndex];
                                            keyValueObject.Value = columnData;
                                            keyValueObjects.Add(keyValueObject);
                                        }
                                        columnIndex++;
                                    }

                                    if (rowNum > 1)
                                    {
                                        if (limitRowInsert)
                                        {
                                            if (rowNum <= (rowLimitInsert + 1))
                                            {
                                                keyValueObjectsList.Add(keyValueObjects);
                                            }
                                        }
                                        else
                                        {
                                            keyValueObjectsList.Add(keyValueObjects);
                                        }
                                    }
                                    else
                                    {
                                        DropTempTable(sQLiteCommand);
                                        if (!CreateTempDataImportTable(sQLiteCommand, columnNames)) // creating temp table.
                                        {
                                            throw new Exception("Failed to create temp table");
                                        }
                                    }

                                    if ((rowNum % DBSettings.BulkDataInsertMaxRecordPerTrans) == 0)
                                    {
                                        if (InsertToTempTable(sQLiteCommand, keyValueObjectsList))
                                        {
                                            keyValueObjectsList.Clear();
                                        }
                                        else
                                        {
                                            throw new Exception("Failed to insert data to temp table");
                                        }
                                    }

                                    rowNum++;
                                }

                                if (InsertToTempTable(sQLiteCommand, keyValueObjectsList))
                                {
                                    keyValueObjectsList.Clear();
                                }
                                else
                                {
                                    throw new Exception("Failed to insert data to temp table");
                                }
                                tr.Commit();
                                OnWorkerComplete(UpdateProgressStartPercentage + UpdateProgressParseLimit, LocalResource.IM_PROGRESSBAR_COMPLETING_CACHEING);
                            }
                            catch (Exception e)
                            {
                                _log.LogError(e.Message + "\n" + e.StackTrace);
                                tr.Rollback();
                                throw;
                            }
                            finally
                            {
                                OnWorkerComplete(UpdateProgressEndPercentage, LocalResource.IM_PROGRESSBAR_ENDING);
                            }
                        }
                    }
                }
                Issuccess = true;
            }
            catch (Exception ex)
            {
                Issuccess = false;
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                xlApp.AutomationSecurity = autoMationSecurity;
                xlApp.EnableEvents = true;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }

            return Issuccess;
        }

        public DataTable ReadDataFromExcel(string filePath, int sheetNo = 1, int skipRows = 0, bool limitRowCount = false, int rowLimitUpto = 100)
        {
            DataTable dt = null;
            ReadSourceEntity readSourceEntity = MakeReadSourceEntity(filePath, sheetNo, skipRows, limitRowCount, rowLimitUpto);
            dt = MakeDataTable(readSourceEntity);
            return dt;
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        bool IsFirst = true;
        private void BringExcelWindowToFront()
        {
            if (xlApp != null && IsFirst)
            {
                try
                {
                    SetForegroundWindow((IntPtr)xlApp.Hwnd); IsFirst = false;
                }
                catch { }
            }// Note Hwnd is declared as int
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            BringExcelWindowToFront();
        }
        private ReadSourceEntity MakeReadSourceEntity(string filePath, int sheetNo = 1, int skipRows = 0, bool limitRowInsert = false, int rowLimitUpto = 100)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            OnWorkerComplete(UpdateProgressStartPercentage, LocalResource.IM_PROGRESSBAR_OPENING_FILE);
            xlApp = new Excel.Application();
            autoMationSecurity = xlApp.AutomationSecurity;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityForceDisable;
            xlApp.EnableEvents = false;
            dispatcherTimer.Start();
            OpenXL(filePath, xlApp);
            if (OpenWbk == null)
            {
                dispatcherTimer.Stop();
                IsFirst = true;
                xlApp.AutomationSecurity = autoMationSecurity;
                xlApp.EnableEvents = true;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
                OnWorkerComplete(UpdateProgressStartPercentage + UpdateProgressParseLimit, LocalResource.IM_PROGRESSBAR_LOADING);
                return null;
            }
            dispatcherTimer.Stop();
            IsFirst = true;
            Excel.Workbook xlWorkbook = OpenWbk;
            Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[sheetNo];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            FileUtil fileUtil = new FileUtil();
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            List<List<string>> keyValueObjectsList = new List<List<string>>();
            List<string> columnNames = new List<string>();

            try
            {
                object[,] data = xlRange.Value;

                int noDataCount = 0;
                for (int i = rowCount; i >= (skipRows + 1); i--)
                {
                    List<string> colData = new List<string>();
                    for (int j = 1; j <= colCount; j++)
                    {
                        string columnData = Convert.ToString(data[i, j]);
                        colData.Add(columnData.Trim());
                    }
                    if (colData.Any(x => x != ""))
                        break;
                    noDataCount++;
                }
                rowCount -= noDataCount;

                int limitLineCount = rowCount;
                int tempPerc = UpdateProgressStartPercentage + (UpdateProgressParseLimit * 10 / 100);
                OnWorkerComplete(tempPerc, String.Format(LocalResource.IM_PROGRESSBAR_READING_LINE, (limitLineCount - 1)));
                double incRate = (double)limitLineCount / rowCount;

                int rowNum = 1;

                double inputProcessed = -1;
                int childProgress = 0;
                TempColumnNames = new List<string>();

                for (int i = (skipRows + 1); i <= rowCount; i++)
                {
                    if (inputProcessed > 1)
                    {
                        double progressChildPerc = inputProcessed / (limitLineCount) * 100;
                        childProgress = Convert.ToInt32(80 * progressChildPerc / 100);
                        var ProgressVal = tempPerc + childProgress;
                        OnWorkerComplete(ProgressVal, String.Format(LocalResource.IM_PROGRESSBAR_READING_ROW, (int)inputProcessed + "/" + (limitLineCount - 1)));
                    }
                    inputProcessed += incRate;

                    List<string> keyValueObjects = new List<string>();
                    int columnIndex = 0;
                    for (int j = 1; j <= colCount; j++)
                    {
                        string columnData = Convert.ToString(data[i, j]);
                        if (i == (skipRows + 1))
                        {
                            string colName = fileUtil.GetNewColumnName(columnNames, columnData);
                            columnNames.Add(colName);
                            TempColumnNames.Add((columnData == null || columnData.Trim().Length == 0) ? "" : colName);
                        }
                        else
                        {
                            string keyValueObject = null;
                            if ((columnIndex + 1) > columnNames.Count)
                            {
                                string newColumnName = fileUtil.GetNewColumnName(columnNames);
                                columnNames.Add(newColumnName);
                                TempColumnNames.Add("");
                            }

                            keyValueObject = columnData;
                            keyValueObjects.Add(keyValueObject);
                        }
                        columnIndex++;
                    }

                    if (rowNum > 1)
                    {
                        keyValueObjectsList.Add(keyValueObjects);
                    }

                    rowNum++;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                OnWorkerComplete(UpdateProgressStartPercentage + UpdateProgressParseLimit, LocalResource.IM_PROGRESSBAR_LOADING);
            }

            ReadSourceEntity readSourceEntity = new ReadSourceEntity()
            {
                Columns = columnNames,
                Contents = keyValueObjectsList
            };

            return readSourceEntity;
        }

        private void OpenXL(string filePath, Excel.Application xlApp)
        {
            try
            {
                OpenWbk = null;
                OpenWbk = xlApp.Workbooks.Open(filePath);
            }
            catch (Exception ex)
            {
                if (ex.Message != "Exception from HRESULT: 0x800A03EC")
                {
                    MessageDialog.ErrorOk(LocalResource.IM_INCORRECT_PASSWORD);
                }
            }
        }

        // Read from source file
        public DataTable ReadDataFromSource(string filePath, string colDelimitter, Encoding encoding, char? enclosingChar, bool limitRowCount = false, int rowLimitUpto = 100)
        {
            DataTable dt = null;
            ReadSourceEntity readSourceEntity = MakeReadSourceEntity(filePath, colDelimitter, encoding, enclosingChar, limitRowCount, rowLimitUpto);
            dt = MakeDataTable(readSourceEntity);
            return dt;
        }

        private ReadSourceEntity MakeReadSourceEntity(string filePath, string colDelimitter, Encoding encoding, char? enclosingChar, bool limitRowCount = false, int rowLimitUpto = 100)
        {
            FileUtil fileUtil = new FileUtil();
            int rowNum = 1;
            List<List<string>> keyValueObjectsList = new List<List<string>>();
            List<string> columnNames = new List<string>();
            try
            {
                int tempPerc = UpdateProgressStartPercentage + (UpdateProgressParseLimit * 10 / 100);
                OnWorkerComplete(tempPerc, LocalResource.IM_PROGRESSBAR_READING);
                filePath = GetTempSourceFilePath(filePath);

                int orgLineCount = File.ReadAllLines(@filePath).Count();
                int limitLineCount = orgLineCount;
                if (limitRowCount) if (limitLineCount > rowLimitUpto) limitLineCount = rowLimitUpto + 1; //+1 means +header row
                tempPerc = tempPerc + (UpdateProgressParseLimit * 10 / 100);
                OnWorkerComplete(tempPerc, String.Format(String.Format(LocalResource.IM_PROGRESSBAR_READING_LINE, limitLineCount - 1)));
                double incRate = (double)limitLineCount / orgLineCount;
                int columnCount = -1;
                TempColumnNames = new List<string>();

                using (var reader = new StreamReader(@filePath, encoding))
                {
                    using (var csvReader = new CsvReader(reader))
                    {
                        if (enclosingChar != null)
                        {
                            csvReader.Configuration.IgnoreQuotes = false;
                            csvReader.Configuration.Quote = (char)enclosingChar;
                        }
                        else
                        {
                            csvReader.Configuration.Quote = Convert.ToChar(@"\");
                            csvReader.Configuration.IgnoreQuotes = true;
                        }

                        if (!(colDelimitter != null && colDelimitter.Length > 0))
                            colDelimitter = DatatableSettings.DummyColumnSpecifier;

                        csvReader.Configuration.Delimiter = colDelimitter;

                        csvReader.Configuration.Encoding = encoding;
                        csvReader.Configuration.BadDataFound = null;
                        csvReader.Configuration.HasHeaderRecord = false;
                        csvReader.Configuration.IgnoreBlankLines = false;

                        double inputProcessed = -1;
                        int childProgress = 0;

                        while (csvReader.Read())
                        {
                            if (inputProcessed > 1)
                            {
                                double progressChildPerc = inputProcessed / (limitLineCount) * 100;
                                childProgress = Convert.ToInt32(70 * progressChildPerc / 100); //70% allocated for reading
                                var ProgressVal = tempPerc + childProgress;
                                OnWorkerComplete(ProgressVal, String.Format(LocalResource.IM_PROGRESSBAR_PARSING_LINE, (int)inputProcessed + "/" + (limitLineCount - 1)));
                            }

                            inputProcessed += incRate;

                            List<string> keyValueObjects = new List<string>();
                            string value = null;
                            int columnIndex = 0;
                            for (int i = 0; csvReader.TryGetField<string>(i, out value); i++)
                            {
                                if (rowNum == 1 && value == null)
                                {
                                    columnCount = i;
                                    break;
                                }
                                if (columnCount != -1 && i == columnCount)
                                    break;
                                if (rowNum == 1)
                                {
                                    string colName = fileUtil.GetNewColumnName(columnNames, value);
                                    columnNames.Add(colName);
                                    TempColumnNames.Add((value == null || value.Trim().Length == 0) ? "" : colName);
                                }
                                else
                                {
                                    string keyValueObject = null;
                                    if ((columnIndex + 1) > columnNames.Count)
                                    {
                                        string newColumnName = fileUtil.GetNewColumnName(columnNames);
                                        columnNames.Add(newColumnName);
                                        TempColumnNames.Add("");
                                    }

                                    keyValueObject = value;
                                    keyValueObjects.Add(keyValueObject);
                                }
                                columnIndex++;
                            }

                            if (rowNum > 1)
                            {
                                if (limitRowCount)
                                {
                                    if (rowNum <= (rowLimitUpto + 1))
                                    {
                                        keyValueObjectsList.Add(keyValueObjects);
                                    }
                                }
                                else
                                {
                                    keyValueObjectsList.Add(keyValueObjects);
                                }
                            }

                            rowNum++;
                        }
                    }
                }
                _log.Info("Found " + (rowNum - 1).ToString() + " records in source file");

                OnWorkerComplete(UpdateProgressStartPercentage + UpdateProgressParseLimit, LocalResource.IM_PROGRESSBAR_LOADING);

                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }

            ReadSourceEntity readSourceEntity = new ReadSourceEntity()
            {
                Columns = columnNames,
                Contents = keyValueObjectsList
            };
            return readSourceEntity;
        }

        private string GetTempSourceFilePath(string filePath)
        {
            string tempFilePath = Path.GetTempPath() + PathName.TempDataImportSourcePath + Path.GetFileName(filePath);
            Directory.CreateDirectory(Path.GetDirectoryName(tempFilePath));
            File.Copy(filePath, tempFilePath, true);
            filePath = tempFilePath;
            return filePath;
        }

        internal string GetTempDestFilePath(string filePath)
        {
            string tempFilePath = Path.GetTempPath() + PathName.TempDataImportDestPath + Path.GetFileName(filePath);
            Directory.CreateDirectory(Path.GetDirectoryName(tempFilePath));
            File.Copy(filePath, tempFilePath, true);
            filePath = tempFilePath;
            return filePath;
        }


        //Make Datatable from entity
        private DataTable MakeDataTable(ReadSourceEntity readSourceEntity)
        {
            if (readSourceEntity != null)
            {
                DataTable dt = new DataTable();
                int tempPerc = UpdateProgressStartPercentage + UpdateProgressParseLimit + (UpdateProgressMakeDtAllocate * 5 / 100);
                OnWorkerComplete(tempPerc, LocalResource.IM_PROGRESSBAR_LOADING_COL);

                int inputProcessed = 1; int childProgress = 0;
                foreach (string columnName in readSourceEntity.Columns)
                {
                    double progressChildPerc = (double)inputProcessed / readSourceEntity.Columns.Count * 100;
                    childProgress = Convert.ToInt32((UpdateProgressMakeDtAllocate * 30 / 100) * progressChildPerc / 100);
                    var ProgressVal = tempPerc + childProgress;
                    OnWorkerComplete(ProgressVal, String.Format(LocalResource.IM_PROGRESSBAR_BUILDING_COL, inputProcessed + "/" + readSourceEntity.Columns.Count));
                    inputProcessed++;

                    dt.Columns.Add(columnName, typeof(string));
                }
                tempPerc = tempPerc + childProgress;
                OnWorkerComplete(tempPerc, LocalResource.IM_PROGRESSBAR_LOADING_ROWS);

                inputProcessed = 1; childProgress = 0;
                foreach (List<string> rowlist in readSourceEntity.Contents) //row
                {
                    double progressChildPerc = (double)inputProcessed / readSourceEntity.Contents.Count * 100;
                    childProgress = Convert.ToInt32((UpdateProgressMakeDtAllocate * 60 / 100) * progressChildPerc / 100);
                    var ProgressVal = tempPerc + childProgress;
                    OnWorkerComplete(ProgressVal, String.Format(LocalResource.IM_PROGRESSBAR_BUILDING_ROW, inputProcessed + "/" + readSourceEntity.Contents.Count));
                    inputProcessed++;

                    DataRow dataRow = dt.NewRow();
                    for (int i = 0; i <= rowlist.Count - 1; i++) //column
                    {
                        dataRow[i] = Convert.ToString(rowlist[i]);
                    }
                    dt.Rows.Add(dataRow);
                }

                tempPerc = tempPerc + childProgress + (UpdateProgressMakeDtAllocate * 5 / 100);
                OnWorkerComplete(tempPerc, LocalResource.IM_PROGRESSBAR_COMPLETING);
                return dt;
            }
            return null;
        }

        //For Venn Diagram counts
        public int GetAnswerCount(SQLiteConnection sQLiteConnection)
        {
            int count = 0;
            try
            {
                DataTable dataTble = DBHelper.GetDataTable("Select count(*) from answers", sQLiteConnection);
                count = Convert.ToInt32(dataTble.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return count;
        }

        public int GetAfterProcessCount(SQLiteConnection sQLiteConnection)
        {
            int count = 0;
            try
            {
                DataTable dataTble = DBHelper.GetDataTable("Select count(*) from data_after_process", sQLiteConnection);
                count = Convert.ToInt32(dataTble.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return count;
        }

        private int GetJoinedAnswerCount(SQLiteConnection sQLiteConnection, string key1Dest, string key1Source, bool IsKey2Exist, string key2Dest = null, string key2Source = null)
        {
            int count = 0;
            try
            {
                string sSql = " Select count(*) from ";
                sSql += " answers a, " + DataImportSettings.DataImportSourceTempTable + " t ";

                sSql += GetQueryWithnullChecking(key1Dest, key2Dest, key1Source, key2Source, sQLiteConnection, IsKey2Exist);
                
                DataTable dataTble = DBHelper.GetDataTable(sSql, sQLiteConnection);
                count = Convert.ToInt32(dataTble.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return count;
        }

        private string GetQuery(string key1Dest, string key2Dest, string key1Source, string key2Source, SQLiteConnection sQLiteConnection, bool isKey2Exist)
        {
            string sSql = "";
            bool IsKey1Exist = false;
            if (key1Dest != ComboBoxSettings.NoneText && key1Source != ComboBoxSettings.NoneText)
            {
                IsKey1Exist = true;
                sSql += " where a.`temprysfiltered` = t.`temprysfiltered` ";
            }
            if (isKey2Exist)
            {
                if (IsKey1Exist)
                    sSql += "and ";
                else
                    sSql += " where ";

                sSql += " a.`temprysfiltered1` = t.`temprysfiltered1` ";
            }
            return sSql;
        }

        private string GetQueryWithnullChecking(string key1Dest, string key2Dest, string key1Source, string key2Source, SQLiteConnection sQLiteConnection, bool isKey2Exist)
        {
            string sSql = "";
            bool IsKey1Exist = false;
            if (key1Dest != ComboBoxSettings.NoneText && key1Source != ComboBoxSettings.NoneText)
            {
                IsKey1Exist = true;
                sSql += " where a.`temprysfiltered` = t.`temprysfiltered` ";
                sSql += " and a.`temprysfiltered` != '' and a.`temprysfiltered` is not null ";
            }
            if (isKey2Exist)
            {
                if (IsKey1Exist)
                    sSql += "and ";
                else
                    sSql += " where ";

                sSql += " a.`temprysfiltered1` = t.`temprysfiltered1` ";
                sSql += " and a.`temprysfiltered1` != '' and a.`temprysfiltered1` is not null ";
            }
            return sSql;
        }

        private int GetUnJoinedAnswerCount(SQLiteConnection sQLiteConnection, string key1Dest, string key1Source, bool IsKey2Exist, string key2Dest = null, string key2Source = null)
        {
            int count = 0;
            try
            {
                string sSql = " Select count(*) from ";
                sSql += " answers ";
                sSql += " Where RowId not in ( ";
                sSql += " Select a.RowId from ";
                sSql += " answers a, " + DataImportSettings.DataImportSourceTempTable + " t ";

                
                sSql += GetQueryWithnullChecking(key1Dest, key2Dest, key1Source, key2Source, sQLiteConnection, IsKey2Exist);

                sSql += " ) ";

                DataTable dataTble = DBHelper.GetDataTable(sSql, sQLiteConnection);
                count = Convert.ToInt32(dataTble.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return count;
        }

        private int GetUnJoinedBPSourceCount(SQLiteConnection sQLiteConnection, string key1Dest, string key1Source, bool IsKey2Exist, string key2Dest = null, string key2Source = null)
        {
            int count = 0;
            try
            {
                string sSql = " Select count(*) from ";
                sSql += " `" + DataImportSettings.DataImportSourceTempTable + "` ";
                sSql += " Where RowId not in ( ";
                sSql += " Select t.RowId from ";
                sSql += " answers a, `" + DataImportSettings.DataImportSourceTempTable + "` t ";


                
                sSql += GetQueryWithnullChecking(key1Dest, key2Dest, key1Source, key2Source, sQLiteConnection, IsKey2Exist);


                sSql += " ) ";

                DataTable dataTble = DBHelper.GetDataTable(sSql, sQLiteConnection);
                count = Convert.ToInt32(dataTble.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return count;
        }

        private int GetJoinedAfterProcessCount(SQLiteConnection sQLiteConnection, string key1Dest, string key1Source, bool IsKey2Exist, string key2Dest = null, string key2Source = null)
        {
            int count = 0;
            try
            {
                string sSql = " Select count(*) from ";
                sSql += " (Select t1.* from  data_after_process a1, answers t1  where  a1.`sort_no` = t1.`sort_no`) a, " + DataImportSettings.DataImportSourceTempTable + " t ";


                sSql += GetQueryWithnullChecking(key1Dest, key2Dest, key1Source, key2Source, sQLiteConnection, IsKey2Exist);

                DataTable dataTble = DBHelper.GetDataTable(sSql, sQLiteConnection);
                count = Convert.ToInt32(dataTble.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return count;
        }

        private int GetUnJoinedAfterProcessCount(SQLiteConnection sQLiteConnection, string key1Dest, string key1Source, bool IsKey2Exist, string key2Dest = null, string key2Source = null)
        {
            int count = 0;
            try
            {
                string sSql = " Select count(*) from ";
                sSql += " data_after_process ";
                sSql += " Where sort_no not in ( ";
                sSql += " Select a.sort_no from ";
                sSql += " (Select t1.* from  data_after_process a1, answers t1  where  a1.`sort_no` = t1.`sort_no`) a, " + DataImportSettings.DataImportSourceTempTable + " t ";


                sSql += GetQueryWithnullChecking(key1Dest, key2Dest, key1Source, key2Source, sQLiteConnection, IsKey2Exist);

                sSql += " ) ";

                DataTable dataTble = DBHelper.GetDataTable(sSql, sQLiteConnection);
                count = Convert.ToInt32(dataTble.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return count;
        }

        private int GetUnJoinedAfterProcessSourceCount(SQLiteConnection sQLiteConnection, string key1Dest, string key1Source, bool IsKey2Exist, string key2Dest = null, string key2Source = null)
        {
            int count = 0;
            try
            {
                string sSql = " Select count(*) from ";
                sSql += " `" + DataImportSettings.DataImportSourceTempTable + "` ";
                sSql += " Where RowId not in ( ";
                sSql += " Select t.RowId from ";
                sSql += " (Select t1.* from  data_after_process a1, answers t1  where  a1.`sort_no` = t1.`sort_no`) a, `" + DataImportSettings.DataImportSourceTempTable + "` t ";


                sSql += GetQueryWithnullChecking(key1Dest, key2Dest, key1Source, key2Source, sQLiteConnection, IsKey2Exist);

                sSql += " ) ";

                DataTable dataTble = DBHelper.GetDataTable(sSql, sQLiteConnection);
                count = Convert.ToInt32(dataTble.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return count;
        }

        public DataTable GetJoinedSourceFileDataBProcess(SQLiteConnection sQLiteConnection, string key1Dest, string key1Source, bool IsKey2Exist, string key2Dest = null, string key2Source = null,List<string> tempColumns=null)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder columns = new StringBuilder();
                int colCount = tempColumns.Count;
                for (int i = 0; i < colCount; i++)
                {
                    if (i == (colCount - 1))
                        columns.Append("t.`" + tempColumns[i] + "`");
                    else
                        columns.Append("t.`" + tempColumns[i] + "`,");
                }
                string sSql = " Select "+ columns.ToString() + " from ";
                sSql += " answers a, " + DataImportSettings.DataImportSourceTempTable + " t ";


                sSql += GetQueryWithnullChecking(key1Dest, key2Dest, key1Source, key2Source, sQLiteConnection, IsKey2Exist);


                dt = DBHelper.GetDataTable(sSql, sQLiteConnection);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return dt;
        }

        public DataTable GetJoinedSourceFileDataAProcess(SQLiteConnection sQLiteConnection, string key1Dest, string key1Source, bool IsKey2Exist, string key2Dest = null, string key2Source = null)
        {
            DataTable dt = new DataTable();
            try
            {
                string sSql = " Select t.* from ";
                sSql += " data_after_process a, " + DataImportSettings.DataImportSourceTempTable + " t ";


                sSql += GetQueryWithnullChecking(key1Dest, key2Dest, key1Source, key2Source, sQLiteConnection, IsKey2Exist);

                sSql += " limit 100 ";

                dt = DBHelper.GetDataTable(sSql, sQLiteConnection);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return dt;
        }

        public int GetSourceFileCount(SQLiteConnection sQLiteConnection)
        {
            int count = 0;
            try
            {
                DataTable dataTble = DBHelper.GetDataTable("Select count(*) from " + DataImportSettings.DataImportSourceTempTable + "", sQLiteConnection);
                count = Convert.ToInt32(dataTble.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            return count;
        }

        public static string GetMASplittingQueryMax(string columnName, string tableName, string notApplicable, int rowCount)
        {
            string sSql = "SELECT MAX(maxValues) from (SELECT  CAST(replace(str, rtrim(str, replace(str, ',', '')), '') as INT) as maxValues  FROM ";
            sSql += "(SELECT rtrim(`" + columnName + "`, ',') as str From `" + tableName + "` ) ";
            if (notApplicable != "")
                sSql += "where maxValues != '" + notApplicable.Replace("'", "''") + "')";
            else
                sSql += ")";
            return sSql;
        }

        internal VennDiagramCounts GetVennDiagramCounts(SQLiteConnection sQLiteConnection, string key1Dest, string key1Source, bool IsKey2Exist, string key2Dest = null, string key2Source = null)
        {
            return new VennDiagramCounts()
            {
                DestUnJoinedBP = GetUnJoinedAnswerCount(sQLiteConnection, key1Dest, key1Source, IsKey2Exist, key2Dest, key2Source),
                DestUnJoinedAP = GetUnJoinedAfterProcessCount(sQLiteConnection, key1Dest, key1Source, IsKey2Exist, key2Dest, key2Source),
                JoinedBP = GetJoinedAnswerCount(sQLiteConnection, key1Dest, key1Source, IsKey2Exist, key2Dest, key2Source),
                JoinedAP = GetJoinedAfterProcessCount(sQLiteConnection, key1Dest, key1Source, IsKey2Exist, key2Dest, key2Source),
                SourceUnJoinedBP = GetUnJoinedBPSourceCount(sQLiteConnection, key1Dest, key1Source, IsKey2Exist, key2Dest, key2Source),
                SourceUnJoinedAP = GetUnJoinedAfterProcessSourceCount(sQLiteConnection, key1Dest, key1Source, IsKey2Exist, key2Dest, key2Source)
            };
        }

        internal static string GetMASplittingQueryMax(string columnName, string tableName, string notApplicable, int rowCount, bool isFile2Exist, ColumnImportSettings importSettings, SQLiteConnection sQLiteConnection)
        {
            string sSql = " (Select t.* from ";
            sSql += " answers a, " + DataImportSettings.DataImportSourceTempTable + " t ";



            sSql += GetQuery1(importSettings.DestinationFileKey1, importSettings.DestinationFileKey2, importSettings.SourceFileKey1, importSettings.SourceFileKey2, isFile2Exist);

            sSql += " ) ";

            string nsSql = "SELECT MAX(maxValues) from (SELECT  CAST(replace(str, rtrim(str, replace(str, ',', '')), '') as INT) as maxValues  FROM ";
            nsSql += "(SELECT rtrim(`" + columnName + "`, ',') as str From " + sSql + " ) ";
            if (notApplicable != "")
                nsSql += "where maxValues != '" + notApplicable.Replace("'", "''") + "')";
            else
                nsSql += ")";
            return nsSql;
        }
        public static string GetQuery1(string key1Dest, string key2Dest, string key1Source, string key2Source, bool isKey2Exist)
        {
            string sSql = "";
            bool IsKey1Exist = false;
            if (key1Dest != ComboBoxSettings.NoneText && key1Source != ComboBoxSettings.NoneText)
            {
                IsKey1Exist = true;
                sSql += " where a.`temprysfiltered` = t.`temprysfiltered` ";
                sSql += " and a.`temprysfiltered` != '' and a.`temprysfiltered` is not null ";
            }
            if (isKey2Exist)
            {
                if (IsKey1Exist)
                    sSql += "and ";
                else
                    sSql += " where ";

                    sSql += " a.`temprysfiltered1` = t.`temprysfiltered1` ";
                sSql += " and a.`temprysfiltered1` != '' and a.`temprysfiltered1` is not null ";
            }
            return sSql;
        }
    }


    public class KeyValueObject
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    internal class ReadSourceEntity
    {
        public List<string> Columns { get; set; }
        public List<List<string>> Contents { get; set; }
    }

    internal class VennDiagramCounts
    {
        public int DestUnJoinedBP { get; set; } = 0;
        public int DestUnJoinedAP { get; set; } = 0;

        public int JoinedBP { get; set; } = 0;
        public int JoinedAP { get; set; } = 0;

        public int SourceUnJoinedBP { get; set; } = 0;
        public int SourceUnJoinedAP { get; set; } = 0;
    }

}
