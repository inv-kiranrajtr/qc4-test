using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using QC4Common.DB;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using log4net;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;
using ProgressBar = Qc4Launcher.Forms.ProgressBar;
using System.Reflection;
using System.Windows.Interop;
using Macromill.QCWeb.COMOperate;
using System.Diagnostics;
using System.Windows;
using System.IO;
using System.ComponentModel;

namespace ExcelAddIn.Common
{

    public class FAList
    {
        private const String TEMPLATE_NAME = "FA.xlsx";
        public static Excel.Worksheet FASheet;
        private static QCWebException ex;
        private static QCWebException exception;
        private static List<QuestionSettings> faQuestions;
        private static List<QuestionSettings> faAddQuestions;
        private static List<string> variables;
        private static List<string> columns;
        private static List<decimal> ids;
        private static decimal qcwebId;
        private static decimal faScenarioHeaderId;
        private static Questions questions;
        private static bool[] conditionArray = null;
        private static string addedItemsQTypeBuffer;
        private static List<Data> surveyRootPath;
        private static string keyItemId;
        private static string faFirstColumn;
        private static string prevFaVariable;
        private static string prevKeyItemId;
        private static List<string> prevAddtionalVariables = new List<string>();
        private static double progressCount;
        private static int rCount;
        private static double progressInc;
        private static string[][,] resultjagArray;
        private static Questions.Question groupingQ;
        private static List<string[,]> retArrayList = new List<string[,]>();
        public static decimal ScenarioTotalizationId { get; private set; }
        private static string Extension { get; set; }
        private static string TargetPath { get; set; }
        private static string SourcePath { get; set; }
        private static string[][,] dtReadTable;
        private static DataTable dtReadTable1;
        private static string tableName = "answers";
        private static string errorMessage = string.Empty;
        private static bool varInvalid = false;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static Dictionary<String,int> FAsheetDict=new Dictionary<String,int>();

        public FAList(Excel.Worksheet worksheet, Excel.Workbook workbook)
        {
            FASheet = worksheet;
            GetTableName(workbook);
            FASheet.Change += OnFAListCellChanged;
        }


        private void OnFAListCellChanged(Excel.Range ChangedCell)
        {
            // 対象となるFA情報を取得する
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        public static void FAExecClick(Worksheet sht, Workbook excelWorkbook = null, bool? isSort = null, IntPtr? active = null, string qcFileName = "")
        {
            try
            {
                Workbook wbk = excelWorkbook == null ? Globals.ThisAddIn.Application.ActiveWorkbook : excelWorkbook;
                if (active == null && Util.checkUnprocessedNewQuestionDialog(wbk, new IntPtr(sht.Application.Hwnd)))
                    return;
               // using (new QC4Common.Logic.SingleGlobalInstance(10, wbk)) //10ms timeout on global lock
                {
                    FASheet = sht;
                    GetTableName(wbk);
                    ProgressBar progress = new ProgressBar(sht);
                    WindowInteropHelper wih = new WindowInteropHelper(progress);
                    if (active != null)
                    {
                        wih.Owner = (IntPtr)active;
                        SetParent(wih.Handle, (IntPtr)active);
                    }
                    else
                    {
                        wih.Owner = new IntPtr(sht.Application.Hwnd);
                        active = new IntPtr(sht.Application.Hwnd);
                        SetParent(wih.Handle, (IntPtr)sht.Application.Hwnd);
                        sht.Application.Interactive = false;
                    }
                    new Thread(() => FAExecution(sht, progress, wbk, isSort, qcFileName, active)).Start();
                    progress.ShowDialog();
                }
            }
            catch { }

        }

        public static void GetTableName(Excel.Workbook book)
        {
            if (DBHelper.checkAfterProcess(book))
            {
                tableName = "data_after_process";
            }
            else
            {
                tableName = "answers";
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void FAExecution(Worksheet sht, ProgressBar progress, Workbook wbk = null, bool? isSort = null, string qcFileName = "", IntPtr? active = null) 
        {
            Excel.Application targetApp = new Excel.Application();
            Excel.Workbooks targetBooks = targetApp.Workbooks;
            Excel.Workbook targetBook = null;
            Excel.Sheets targetBook_sheets = null;
            Excel.Worksheet sourceSheet = null;
            try
            {
                progress.OnWorkerMethodComplete(0, AddinResource.PROGRESS_MESSAGE_1);
                sht.Application.EnableEvents = false;
                int endRow = sht.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                int startRow = Constants.FA.FastartRow;
                int OnofstartColumn = Constants.FA.OnoffstartColumn;
                Excel.Range OnoffstartCell = sht.Cells[startRow, OnofstartColumn];
                int OnoffendColumn = OnofstartColumn;
                Excel.Range OnoffvarendCell = sht.Cells[endRow, OnoffendColumn];
                Excel.Range OnoffRange = sht.Range[OnoffstartCell, OnoffvarendCell];


                foreach (Excel.Range Crt in OnoffRange)
                {
                    if (Crt.Text == "○" || Crt.Text == "On" || Crt.Text == AddinResource.CELL_ON)
                    {
                        Definitions.fAOfList.Add(Crt.Text);
                    }
                    if (Crt.Text == "×" || Crt.Text == "Off" || Crt.Text == AddinResource.CELL_OFF)
                    {
                        continue;
                    }
                }
                if (Definitions.fAOfList.Count == 0)
                {
                    //if no rows for FA creation
                    progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_7, IsForceStop: true);
                    MessageDialog.ErrorOk(AddinResource.CHECK_FALIST_VALIDATION, (IntPtr)active);
                    sht.Application.EnableEvents = true;
                    return;
                }
                SourcePath = DB.DBHelper.GetConnectionString(wbk);
                object varName = sht.Range["C5"].Value;
                if (varName != null && varName.ToString() != "" && IsExistDataForDivision(varName.ToString()))
                {
                    progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_7, IsForceStop: true);
                    MessageDialog.ErrorOk(AddinResource.FA_NO_DATA, (IntPtr)active);
                    return;
                }
                Range endCell = ExcelUtil.EndxlUp(sht.Range[Constants.FA.FAStartAddress]);
                Excel.Range range = sht.get_Range(Constants.FA.FAStartAddress, endCell);
                Excel.Range start = sht.Cells[2, 2];
                Excel.Range end = ExcelUtil.EndxlRight(start);

              
                //invalid variable checking
                if (isSort == null)
                    varInvalid = InvalidVariableCheck(range, sht, isSort);
                if (varInvalid)
                {
                    MessageDialog.ErrorOk(rCount + errorMessage);
                    progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_7, IsForceStop: true); 
                    varInvalid = false;
                    rCount = 0;
                    Definitions.fAVariable.Clear();
                    Definitions.fAAddtionalVariable.Clear();
                    return;
                }
                progress.OnWorkerMethodComplete(5, AddinResource.PROGRESS_MESSAGE_2);
                if (isSort == null)
                {
                    sourceSheet = ExcelUtil.GetWorksheetByIndex(1);
                    sourceSheet.Application.EnableEvents = false;
                    sourceSheet.Application.ScreenUpdating = false;
                    sourceSheet.Application.DisplayAlerts = false;
                    sourceSheet.Application.Calculation = Excel.XlCalculation.xlCalculationManual;
                }
                string tempPath = GetTemplatePath(TEMPLATE_NAME);
                string rowsWithoutFa = null;
                int countFAOfList = Definitions.fAOfList.Count;
              
                if (qcFileName != "")
                {
                    string tempFolder = tempPath.Substring(0, tempPath.LastIndexOf('\\')) + "\\FAOutputForSTD";
                    string fileDirectory = "";
                    if (Directory.Exists(tempFolder))
                    {
                        try
                        {
                            Directory.Delete(tempFolder, true);
                            Directory.CreateDirectory(tempFolder);
                        }
                        catch { }
                    }
                    else
                        Directory.CreateDirectory(tempFolder);
                    int fi = 0;
                    while (fi != -1)
                    {
                        fi++;
                        try
                        {
                            fileDirectory = tempFolder + "\\FA" + fi;
                            if (!Directory.Exists(tempFolder + "\\FA" + fi))
                            {
                                Directory.CreateDirectory(tempFolder + "\\FA" + fi);
                                fi = -1;
                            }
                        }
                        catch { }
                    }
                    string filepath = fileDirectory + "\\" + qcFileName + "_" + (DateTime.Now.ToString("yyyyMMdd_HHmm")) + "_FA.xlsx";
                    try
                    {
                        System.IO.File.Copy(tempPath, filepath);
                        targetBook = targetBooks.Add(filepath);
                    }
                    catch (Exception ex)
                    {
                        targetBook = targetBooks.Add(tempPath);
                    }
                }
                else
                    targetBook = targetBooks.Add(tempPath);//creating excel for FAList creation

                targetBook_sheets = targetBook.Worksheets;
              
                progress.OnWorkerMethodComplete(8, AddinResource.PROGRESS_MESSAGE_3);
                prevFaVariable = null;
                prevKeyItemId = null;
                prevAddtionalVariables.Clear();
                progressCount = 10;
                rCount = 0;
                double p = (90 / (Definitions.fAOfList.Count));
                if (p <= 5)
                    progressInc = 5;
                else
                    progressInc = Math.Round(p);

                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_4);
                foreach (Excel.Range targetCell in range.Cells)
                {
                    rCount++;
                    Definitions.fAOfList.Clear();
                    Definitions.fACriteria.Clear();
                    Definitions.fAVariable.Clear();
                    Definitions.fAAddtionalVariable.Clear();
                    faQuestions = new List<QuestionSettings>();
                    faAddQuestions = new List<QuestionSettings>();
                    // clear values here
                    if (String.IsNullOrEmpty(targetCell.Value2)) // to do off check
                    {
                        continue;
                    }
                    if (null != keyItemId)
                        keyItemId = null;

                    if (null != faFirstColumn)
                        faFirstColumn = null;

                    if (targetCell.Value2 == "○" || targetCell.Value2 == "On" || targetCell.Value2 == AddinResource.CELL_ON)
                    {
                        Range conditionVariable = targetCell.Offset[0, 1]; // condition variable
                        Range faVariableStart = targetCell.Offset[0, 2];
                        Range faVariableEnd = targetCell.Offset[0, 31];
                        Range faVariables = sht.Range[faVariableStart, faVariableEnd];// Fa variable
                        if (progressInc >= 5)
                        {
                            if (progressCount > 95)
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_1); 
                            else if (progressCount > 80 && progressCount <= 95)
                                progress.OnWorkerMethodComplete(++progressCount, AddinResource.PROGRESS_MESSAGE_5_1); 
                            else
                            {
                                progressCount = progressCount + Math.Round(progressInc / 5);
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_1); 
                            }
                        }

                        foreach (Excel.Range faVariable in faVariables.Cells)
                        {
                            if (!String.IsNullOrEmpty(faVariable.Text))
                            {
                                Definitions.fAVariable.Add(faVariable.Text); //  Need proper checking like null/empty
                            }
                        }
                        if (Definitions.fAVariable.Count == 0)
                        {
                            //if no FA variables selected in the row
                            rowsWithoutFa += " Row" + rCount + ",";
                            if (sourceSheet != null)
                            {
                                sourceSheet.Application.EnableEvents = true;
                                sourceSheet.Application.ScreenUpdating = true;
                                sourceSheet.Application.DisplayAlerts = true;
                                sourceSheet.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                            }
                            progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_5);
                            continue;
                        }

                        Range addOnVariableStart = targetCell.Offset[0, 32];
                        Range addOnVariableEnd = targetCell.Offset[0, 62];
                        Range addOnVariables = sht.Range[addOnVariableStart, addOnVariableEnd];// add-on variable
                        if (progressInc > 5)
                        {
                            if (progressCount > 95)
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_2);
                            else if (progressCount > 80 && progressCount <= 95)
                                progress.OnWorkerMethodComplete(++progressCount, AddinResource.PROGRESS_MESSAGE_5_2);
                            else
                            {
                                progressCount = progressCount + Math.Round(progressInc / 5);
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_2);
                            }
                        }

                        foreach (Excel.Range addOnVariable in addOnVariables.Cells)
                        {
                            if (!String.IsNullOrEmpty(addOnVariable.Text))
                            {
                                Definitions.fAAddtionalVariable.Add(addOnVariable.Text); //  Need proper checking like null/empty
                            }
                        }

                        Range criteriaStart = targetCell.Offset[0, 1];
                        Range CriteriaEnd = targetCell.Offset[0, 1];
                        Range Criteria = sht.Range[criteriaStart, CriteriaEnd];// criteria

                        if (progressInc > 5)
                        {
                            if (progressCount > 95)
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_3);
                            else if (progressCount > 80 && progressCount <= 95)
                                progress.OnWorkerMethodComplete(++progressCount, AddinResource.PROGRESS_MESSAGE_5_3);
                            else
                            {
                                progressCount = progressCount + Math.Round(progressInc / 5);
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_3);
                            }
                        }

                        foreach (Excel.Range Criteriavar in Criteria.Cells)
                        {
                            if (!String.IsNullOrEmpty(Criteriavar.Text))
                            {
                                keyItemId = Criteriavar.Text; //  Need proper checking like null/empty
                            }
                        }
                        Range FAStart = targetCell.Offset[0, 2];
                        Range FAEnd = targetCell.Offset[0, 2];
                        Range FAFirst = sht.Range[FAStart, CriteriaEnd];// first Fa variable

                        foreach (Excel.Range FAFirstvar in FAFirst.Cells)
                        {
                            if (!String.IsNullOrEmpty(FAFirstvar.Text))
                            {
                                faFirstColumn = FAFirstvar.Text; //  Need proper checking like null/empty
                            }
                        }

                        if ((prevKeyItemId == keyItemId) && (prevFaVariable == faFirstColumn) && Definitions.fAAddtionalVariable.SequenceEqual(prevAddtionalVariables))
                        {
                            if (progressCount > 95)
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_5);
                            else if (progressCount > 80 && progressCount <= 95)
                                progress.OnWorkerMethodComplete(++progressCount, AddinResource.PROGRESS_MESSAGE_5_5);
                            else
                            {
                                progressCount = progressCount + Math.Round(progressInc / 5);
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_5);
                            }
                            sourceSheet.Application.EnableEvents = true;
                            sourceSheet.Application.ScreenUpdating = true;
                            sourceSheet.Application.DisplayAlerts = true;
                            sourceSheet.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                            continue;
                        }

                        if (keyItemId != null) //FaList with criteria variable
                        {
                            if (progressInc > 5)
                            {
                                if (progressCount > 95)
                                    progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_4);
                                else if (progressCount > 80 && progressCount <= 95)
                                    progress.OnWorkerMethodComplete(++progressCount, AddinResource.PROGRESS_MESSAGE_5_4);
                                else
                                {
                                    progressCount = progressCount + Math.Round(progressInc / 5);
                                    progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_4);
                                }
                            }
                            List<FAListTabulation.QuestionInformation> targetFaList = GetFaItemInfoList(qcwebId, faScenarioHeaderId, surveyRootPath, questions, sht);
                            List<FAListTabulation.QuestionInformation> addFaList =
                                   GetFaAddItemInfoList(qcwebId, (decimal)ScenarioTotalizationId, surveyRootPath, questions);
                           
                            FAListTabulation.QuestionInformation sampleIdInfo = GetFaSampleIdInfoList(qcwebId, (decimal)ScenarioTotalizationId, surveyRootPath, questions);
                            if (addFaList.Count != Definitions.fAAddtionalVariable.Count)//handling new questions in additional FA variables
                            {
                                DBHelper.CheckIfColumnExists(wbk, faAddQuestions, out variables, out columns, out ids);
                                for (int i = 0; i < faAddQuestions.Count; i++)
                                {
                                    List<Data> faDataList = new List<Data>();
                                    if (faAddQuestions[i].QuestionFlag == "New")
                                    {
                                        for (int j = 0; j < sampleIdInfo.Datas.Count; j++)
                                        {
                                            faDataList.Add(null);
                                        }

                                        FAListTabulation.QuestionInformation newfaAddInfo = new FAListTabulation.QuestionInformation(faAddQuestions[i].Question, faAddQuestions[i].TableHeading, faDataList);
                                        addFaList.Add(newfaAddInfo);
                                    }
                                }
                            }
                            addFaList.Add(sampleIdInfo);

                            //keyItemId details
                            FAListTabulation.QuestionInformation groupingItemInfo =
                                GetGroupingItemInfo(qcwebId, keyItemId, surveyRootPath, questions);
                            if (progressCount > 95)
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_5);
                            else if (progressCount > 80 && progressCount <= 95)
                                progress.OnWorkerMethodComplete(++progressCount, AddinResource.PROGRESS_MESSAGE_5_5);
                            else
                            {
                                progressCount = progressCount + Math.Round(progressInc / 5);
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_5);
                            }

                            if (targetFaList.Count != Definitions.fAVariable.Count)//handling new questions in FA variables
                            {
                                DBHelper.CheckIfColumnExists(wbk, faQuestions, out variables, out columns, out ids);
                                for (int i = 0; i < faQuestions.Count; i++)
                                {
                                    List<Data> faDataList = new List<Data>();
                                    if (faQuestions[i].QuestionFlag == "New")
                                    {
                                        for (int j = 0; j < sampleIdInfo.Datas.Count; j++)
                                        {
                                            faDataList.Add(null);
                                        }
                                        FAListTabulation.QuestionInformation newfaInfo = new FAListTabulation.QuestionInformation(faQuestions[i].Question, faQuestions[i].TableHeading, faDataList);
                                        targetFaList.Add(newfaInfo);
                                    }
                                }
                            }
                           
                            QCWebException ex = FAListTabulation.GetFAListArray(targetFaList, addFaList, groupingItemInfo
                                                                                       , out resultjagArray, out addedItemsQTypeBuffer
                                                                                       , conditionArray);
                            if (ex != null) throw ex;
                            if (null != retArrayList)
                                retArrayList.Clear();
                            foreach (string[,] arr in resultjagArray)
                            {
                                retArrayList.Add(arr);
                            }
                            DownloadFASheet(retArrayList, keyItemId, faFirstColumn, groupingItemInfo.SectorInformations, groupingItemInfo.Description, targetBook, sourceSheet, addFaList, targetFaList, targetApp, isSort);
                        }
                        //FaList without criteria variable
                        else
                        {
                            if (progressInc > 5)
                            {
                                if (progressCount > 95)
                                    progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_4);
                                else if (progressCount > 80 && progressCount <= 95)
                                    progress.OnWorkerMethodComplete(++progressCount, AddinResource.PROGRESS_MESSAGE_5_4);
                                else
                                {
                                    progressCount = progressCount + Math.Round(progressInc / 5);
                                    progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_4);
                                }
                            }
                            string[,] resultArray;
                            List<FAListTabulation.QuestionInformation> targetFaList = GetFaItemInfoList(qcwebId, faScenarioHeaderId, surveyRootPath, questions, sht);
                            List<FAListTabulation.QuestionInformation> addFaList =
                                    GetFaAddItemInfoList(qcwebId, (decimal)ScenarioTotalizationId, surveyRootPath, questions);
                           
                            FAListTabulation.QuestionInformation sampleIdInfo = GetFaSampleIdInfoList(qcwebId, (decimal)ScenarioTotalizationId, surveyRootPath, questions);
                            if (addFaList.Count != Definitions.fAAddtionalVariable.Count) //handling new questions in additional FA variables
                            {
                                DBHelper.CheckIfColumnExists(wbk, faAddQuestions, out variables, out columns, out ids);
                                for (int i = 0; i < faAddQuestions.Count; i++)
                                {
                                    List<Data> faDataList = new List<Data>();
                                    if (faAddQuestions[i].QuestionFlag == "New")
                                    {
                                        for (int j = 0; j < sampleIdInfo.Datas.Count; j++)
                                        {
                                            faDataList.Add(null);
                                        }

                                        FAListTabulation.QuestionInformation newfaAddInfo = new FAListTabulation.QuestionInformation(faAddQuestions[i].Question, faAddQuestions[i].TableHeading, faDataList);
                                        addFaList.Add(newfaAddInfo);
                                    }
                                }
                            }
                            addFaList.Add(sampleIdInfo);
                            if (progressCount > 95)
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_5);
                            else if (progressCount > 80 && progressCount <= 95)
                                progress.OnWorkerMethodComplete(++progressCount, AddinResource.PROGRESS_MESSAGE_5_5);
                            else
                            {
                                progressCount = progressCount + Math.Round(progressInc / 5);
                                progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_5_5);
                            }
                            if (targetFaList.Count != Definitions.fAVariable.Count) //handling new questions in FA variables
                            {
                                DBHelper.CheckIfColumnExists(wbk, faQuestions, out variables, out columns, out ids);
                                for (int i = 0; i < faQuestions.Count; i++)
                                {
                                    List<Data> faDataList = new List<Data>();
                                    if (faQuestions[i].QuestionFlag == "New")
                                    {
                                        for (int j = 0; j < sampleIdInfo.Datas.Count; j++)
                                        {
                                            faDataList.Add(null);
                                        }
                                        FAListTabulation.QuestionInformation newfaInfo = new FAListTabulation.QuestionInformation(faQuestions[i].Question, faQuestions[i].TableHeading, faDataList);
                                        targetFaList.Add(newfaInfo);
                                    }
                                }
                            }
                           
                            QCWebException ex = FAListTabulation.GetFAListArray(targetFaList, addFaList, out resultArray
                                                                       , out addedItemsQTypeBuffer, conditionArray);
                            if (ex != null) throw ex;
                            DownloadFASheetNonCriteria(resultArray, faFirstColumn, targetBook, sourceSheet, targetFaList, addFaList, targetApp, isSort);

                        }
                        prevFaVariable = faFirstColumn;
                        prevKeyItemId = keyItemId;
                        prevAddtionalVariables = new List<string>(Definitions.fAAddtionalVariable);
                    }
                    else
                        continue;

                }

                //display FA sheet
                if (rowsWithoutFa != null)
                {
                    rowsWithoutFa = rowsWithoutFa.Trim(',');
                    string[] rowList = rowsWithoutFa.Split(',');
                    if (rowList.Count() == countFAOfList)
                    {
                        progress.OnWorkerMethodComplete(progressCount, AddinResource.PROGRESS_MESSAGE_7, retainThread: true);
                        if (System.Windows.Forms.DialogResult.OK == MessageDialog.InfoResult(AddinResource.CHECK_FALIST_VALIDATION + " in " + rowsWithoutFa))
                        {
                            targetApp.Visible = false;
                            targetBook.Close(false);
                            progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_7, IsForceStop: true);
                            return;
                        }
                    }
                    else
                    {
                        progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_6, retainThread: true);
                        if (System.Windows.Forms.DialogResult.OK == MessageDialog.InfoResult(AddinResource.CHECK_FALIST_VALIDATION + " in " + rowsWithoutFa))
                        {
                            targetApp.Visible = true;
                            DeleteExtraSheet(targetApp,targetBook);
                            foreach (Worksheet sheet in targetBook.Worksheets)
                            {
                                if (keyItemId == null || keyItemId == "")
                                {
                                    sheet.Select();
                                    Range ExcelRange = sheet.get_Range("C5");
                                    ExcelRange.Select();
                                }
                                else
                                {
                                    sheet.Select();
                                    Range ExcelRange = sheet.get_Range("C7");
                                    ExcelRange.Select();
                                }
                            }
                            targetBook_sheets[1].select();
                            progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_7, IsForceStop: true);
                            SetForegroundWindow((IntPtr)targetApp.Hwnd);
                            targetApp.WindowState = XlWindowState.xlMaximized;
                        }
                    }
                }
                else
                {
                    progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_6, retainThread: true);
                    targetApp.Visible = true;
                    DeleteExtraSheet(targetApp,targetBook);
                    foreach (Worksheet sheet in targetBook.Worksheets)
                    {
                        if (keyItemId == null || keyItemId == "")
                        {
                            sheet.Select();
                            Range ExcelRange = sheet.get_Range("C5");
                            ExcelRange.Select();
                        }
                        else
                        {
                            sheet.Select();
                            Range ExcelRange = sheet.get_Range("C7");
                            ExcelRange.Select();
                        }
                    }
                    targetBook.Worksheets[1].select();
                    progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_7, IsForceStop: true);
                    SetForegroundWindow((IntPtr)targetApp.Hwnd);
                    targetApp.WindowState = XlWindowState.xlMaximized;
                }
            }
            catch (QCWebException ex)
            {
                progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_7, IsForceStop: true);
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            catch (QCWebBusinessException)
            {
                progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_7, IsForceStop: true);
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            catch (Exception ex)
            {
                // FA集計処理に失敗しました。QCWEBID:{0},FAシナリオヘッダーID:{1}
                //throw new QCWebException("QCS0501006009", new string[] { qcwebId.ToString(), faScenarioHeaderId.ToString() }
                //                         , Macromill.QCWeb.Logic.GlobalsConstant.LogLevel.FATAL, ex);
                progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_MESSAGE_7, IsForceStop: true);
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            finally
            {
                varInvalid = false;
                rCount = 0;
                if (sourceSheet != null)
                {
                    sourceSheet.Application.EnableEvents = true;
                    sourceSheet.Application.ScreenUpdating = true;
                    sourceSheet.Application.DisplayAlerts = true;
                    sourceSheet.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                }
                if (sht != null)
                    sht.Application.EnableEvents = true;
                try
                {
                    COMWholeOperate.releaseComObject(ref targetBook_sheets);
                    COMWholeOperate.releaseComObject(ref targetBook);
                    COMWholeOperate.releaseComObject(ref targetBooks);
                    COMWholeOperate.releaseComObject(ref targetApp);
                }
                catch { }
            }
            FAsheetDict.Clear();//Clearing the Dictionary used for fixing 224064
        }


        private static bool IsExistDataForDivision(string keyItemId)
        {
            using (SQLiteConnection dbSource = DB.DBHelper.GetConnection(SourcePath))
            {
                dbSource.Open();
                int cnt = 0;
                string col = "";
                string sql = "select COUNT(id) from question where variable = '" + keyItemId + "' AND question_flag = 'An'; ";
                if (DB.DBHelper.ExecuteScalar(sql, dbSource)>0)
                {
                    sql = "select id from question where variable = '" + keyItemId + "';";
                    col = "q_" + DB.DBHelper.ExecuteScalar(sql, dbSource).ToString();
                    sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('multivariate') WHERE name='" + col + "'";
                    cnt = DB.DBHelper.ExecuteScalar(sql, dbSource);
                }
                else
                {
                    sql = "select id from question where variable = '" + keyItemId + "';";
                    col = "q_" + DB.DBHelper.ExecuteScalar(sql, dbSource).ToString();
                    sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('" + tableName + "') WHERE name='" + col + "'";
                    cnt = DB.DBHelper.ExecuteScalar(sql, dbSource);
                }
                return cnt == 0;
            }
        }

        //to produce FA output without criteria variable
        private static void DownloadFASheetNonCriteria(string[,] retArrayList, string faFirstColumn, Workbook targetBook, Worksheet sourceSheet, List<FAListTabulation.QuestionInformation> targetFaList, List<FAListTabulation.QuestionInformation> addFaList, Excel.Application targetApp, bool? isSort = null)
        {
            try
            {
                Excel.Worksheet tmpSheet = targetBook.Worksheets[2];
                if (targetBook == null) return;
                DataTable dtTable = new DataTable();
                dtTable.Clear();
                string[,] item = retArrayList;
                for (int k = 0; k < item.GetLength(1); k++)
                {
                    dtTable.Columns.Add();
                }
                int sampleIdIdx = item.GetLength(1) - 1; //getting SampleID as first column
                for (int j = 0; j < item.GetLength(0); j++)
                {
                    //if (worker.CancellationPending) return;
                    DataRow row = dtTable.NewRow();
                    for (int k = 0; k < item.GetLength(1); k++)
                    {
                        if (k == 0)
                        {
                            row[k] = item[j, sampleIdIdx];
                        }
                        else
                        {
                            row[k] = item[j, k - 1];
                        }
                    }

                    dtTable.Rows.Add(row);
                }

                object[,] arr = new object[dtTable.Rows.Count, dtTable.Columns.Count];
                for (int r = 0; r < dtTable.Rows.Count; r++)
                {
                    //if (worker.CancellationPending) return;
                    DataRow dr = dtTable.Rows[r];
                    for (int c = 0; c < dtTable.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                        if (arr[r, c].ToString().Contains("="))
                        {
                            arr[r, c] = "'" + arr[r, c].ToString();
                        }
                    }
                }

                string Fa = Convert.ToString(arr[0, 1]);
                //formatting FA output sheet
                tmpSheet.Copy(Type.Missing, targetBook.Sheets[targetBook.Sheets.Count]); // copy
                Excel.Worksheet sheet = targetBook.Sheets[targetBook.Sheets.Count];//targetBook.Worksheets[1];

                //if (worker.CancellationPending) return;
                string sheetName = "" + faFirstColumn;
                //Fix added for handling the duplicate names for excel sheet Redmine:#224064
                if (FAsheetDict.ContainsKey(sheetName))
                {
                    int keyIncValue = FAsheetDict[sheetName] + 1;
                    FAsheetDict[sheetName] = keyIncValue;//Updating the sheetname count by 1 to the Dictionary
                    sheetName = sheetName + " (" + keyIncValue + ")";
                }
                else
                {
                    FAsheetDict.Add(sheetName, 1);//Adding the sheetname with count 1 to the Dictionary
                }
                sheet.Name = sheetName.Length <= 28 ? sheetName : sheetName.Substring(0, 28) + "...";
                sheet.Cells.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                sheet.Cells.Font.Size = 9;
                Range line = (Range)sheet.Cells[4, 10];
                line.EntireRow.Delete(Excel.XlDirection.xlUp); // to align the heading of FAList
                Excel.Range c1 = (Excel.Range)sheet.Cells[4, 2];
                Excel.Range c2 = (Excel.Range)sheet.Cells[4 + dtTable.Rows.Count - 1, dtTable.Columns.Count + 1];
                Excel.Range range = sheet.get_Range(c1, c2);
                BorderForCells(range);
                range.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                Excel.Range c11 = (Excel.Range)sheet.Cells[4, 2];
                Excel.Range c12 = (Excel.Range)sheet.Cells[4, dtTable.Columns.Count + 1];
                Excel.Range range1 = sheet.get_Range(c11, c12);
                range1.Interior.Color = System.Drawing.Color.FromArgb(0, 112, 192);
                range1.Font.Color = Excel.XlRgbColor.rgbGhostWhite;

                for (int c = 0; c < dtTable.Columns.Count; c++)
                {
                    if (Convert.ToString(arr[0, c]).Contains("回答日時"))
                    {
                        Excel.Range c21 = (Excel.Range)sheet.Cells[5, c + 2];
                        Excel.Range c22 = (Excel.Range)sheet.Cells[4 + dtTable.Rows.Count - 1, c + 2];
                        Excel.Range range2 = sheet.get_Range(c21, c22);
                        range2.NumberFormat = "yyyy/MM/dd HH:mm:ss";
                    }
                }
                for (int c = targetFaList.Count + 1; c < dtTable.Columns.Count; c++)
                {
                    string colName = Convert.ToString(arr[0, c]);
                    foreach (FAListTabulation.QuestionInformation qn in addFaList)
                    {
                        if (colName.Contains(qn.Description) && qn.QuestionType == QuestionType.N)
                        {
                            Excel.Range c41 = (Excel.Range)sheet.Cells[5, c + 2];
                            Excel.Range c42 = (Excel.Range)sheet.Cells[4 + dtTable.Rows.Count - 1, c + 2];
                            Excel.Range range2 = sheet.get_Range(c41, c42);
                            range2.NumberFormat = "General";
                        }
                    }
                }

                //if (worker.CancellationPending) return;
                sheet.Cells[2, 2] = AddinResource.FA_ResultString_3;
                sheet.Cells[2, 3] = dtTable.Rows.Count - 1;
                sheet.Cells[2, 2].Font.Color = Excel.XlRgbColor.rgbGhostWhite;
                sheet.Cells[2, 2].Interior.Color = System.Drawing.Color.FromArgb(0, 112, 192);
                Excel.Range c31 = (Excel.Range)sheet.Cells[2, 2];
                Excel.Range c32 = (Excel.Range)sheet.Cells[2, 3];
                Excel.Range rangeHead = sheet.get_Range(c31, c32);
                rangeHead.Borders.Color = System.Drawing.Color.FromArgb(166, 166, 166);
                rangeHead.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                range.Value2 = arr;
                range.Columns.WrapText = false;
                range.Columns.AutoFit();
                bool? isChecked = isSort == null ? Globals.Ribbons.qc4Ribbon.checkBoxSort.Checked : isSort;
                if (isChecked == true && dtTable.Rows.Count != 1)
                {
                    SortAddonVariables(sheet, range, targetFaList.Count, addFaList.Count, dtTable.Rows.Count, 4);
                }
                int f = 0;
                Excel.Range rangeMax = sheet.get_Range(c31, c2);
                Range lastCol = null;
                foreach (Range col in rangeMax.Columns)
                {
                    if (f == 0)
                        col.ColumnWidth = 7.5;
                    else if (col.ColumnWidth > 57.1)
                        col.ColumnWidth = 57.1;
                    f++;
                    lastCol = col;
                }
                range.Columns.WrapText = true;
                lastCol = lastCol.Next;
                rangeMax.Rows.AutoFit();
                Excel.Range dhEndMost = lastCol.End[Excel.XlDirection.xlToRight];
                sheet.get_Range(lastCol, dhEndMost).EntireColumn.ColumnWidth = 57.1;                
                sheet.Range["B2"].ColumnWidth = 13;
                if (sourceSheet != null)
                {
                    sourceSheet.Application.EnableEvents = true;
                    sourceSheet.Application.ScreenUpdating = true;
                    sourceSheet.Application.DisplayAlerts = true;
                    sourceSheet.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        //to produce FA output with criteria variable
        private static void DownloadFASheet(List<string[,]> retArrayList, string keyItemId, string faFirstColumn, List<FAListTabulation.SectorInformation> sectorInformations, string Description, Workbook targetBook, Worksheet sourceSheet, List<FAListTabulation.QuestionInformation> addFaList, List<FAListTabulation.QuestionInformation> targetFaList, Excel.Application targetApp, bool? isSort = null)
        {
            try
            {
                Excel.Worksheet tmpSheet = targetBook.Worksheets[2];
                if (targetBook == null) return;
                int sheetCount = 1;
                int sheetCountinc = 1;
                for (int i = 0; i < retArrayList.Count; i++)
                {
                    //if (worker.CancellationPending) return;
                    DataTable dtTable = new DataTable();
                    dtTable.Clear();
                    string[,] item = retArrayList[i];

                    for (int k = 0; k < item.GetLength(1); k++)
                    {
                        dtTable.Columns.Add();
                    }
                    int sampleIdIdx = item.GetLength(1) - 1; //getting SampleID as first column
                    for (int j = 0; j < item.GetLength(0); j++)
                    {
                        DataRow row = dtTable.NewRow();
                        for (int k = 0; k < item.GetLength(1); k++)
                        {
                            if (k == 0)
                            {
                                row[k] = item[j, sampleIdIdx];
                            }
                            else
                            {
                                row[k] = item[j, k - 1];
                            }

                        }
                        dtTable.Rows.Add(row);
                    }

                    object[,] arr = new object[dtTable.Rows.Count, dtTable.Columns.Count];
                    for (int r = 0; r < dtTable.Rows.Count; r++)
                    {
                        DataRow dr = dtTable.Rows[r];
                        for (int c = 0; c < dtTable.Columns.Count; c++)
                        {
                            arr[r, c] = dr[c];
                            //arr[r,c] = CellValueTruncate(arr[r,c].ToString(), 255);
                            if (arr[r, c].ToString().Contains("="))
                            {
                                arr[r, c] = "'" + arr[r, c].ToString();
                            }
                        }
                    }

                    //if (worker.CancellationPending) return;
                    string var = Convert.ToString(arr[0, 1]);
                    //formatting FA output sheet
                    tmpSheet.Copy(Type.Missing, targetBook.Sheets[targetBook.Sheets.Count]);
                    Excel.Worksheet sheet = targetBook.Sheets[targetBook.Sheets.Count];
                    sheet.Cells.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                    sheet.Cells.Font.Size = 9;
                    Range line = (Range)sheet.Rows[5];
                    line.Insert(); // to align the heading of FAList
                    Excel.Range c1 = (Excel.Range)sheet.Cells[6, 2];
                    Excel.Range c2 = (Excel.Range)sheet.Cells[6 + dtTable.Rows.Count - 1, dtTable.Columns.Count + 1];
                    Excel.Range range = sheet.get_Range(c1, c2);
                    BorderForCells(range);
                    range.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                    Excel.Range c11 = (Excel.Range)sheet.Cells[6, 2];
                    Excel.Range c12 = (Excel.Range)sheet.Cells[6, dtTable.Columns.Count + 1];
                    Excel.Range range1 = sheet.get_Range(c11, c12);
                    range1.Interior.Color = System.Drawing.Color.FromArgb(0, 112, 192);
                    range1.Font.Color = Excel.XlRgbColor.rgbGhostWhite;

                    for (int c = 0; c < dtTable.Columns.Count; c++)
                    {
                        if (Convert.ToString(arr[0, c]).Contains("回答日時"))
                        {
                            Excel.Range c21 = (Excel.Range)sheet.Cells[7, c + 2];
                            Excel.Range c22 = (Excel.Range)sheet.Cells[5 + dtTable.Rows.Count, c + 2];
                            Excel.Range range2 = sheet.get_Range(c21, c22);
                            range2.NumberFormat = "yyyy/MM/dd HH:mm:ss";
                        }
                    }
                    for (int c = targetFaList.Count + 1; c < dtTable.Columns.Count; c++)
                    {
                        string colName = Convert.ToString(arr[0, c]);
                        foreach (FAListTabulation.QuestionInformation qn in addFaList)
                        {
                            if (colName.Contains(qn.Description) && qn.QuestionType == QuestionType.N)
                            {
                                Excel.Range c41 = (Excel.Range)sheet.Cells[7, c + 2];
                                Excel.Range c42 = (Excel.Range)sheet.Cells[5 + dtTable.Rows.Count, c + 2];
                                Excel.Range range2 = sheet.get_Range(c41, c42);
                                range2.NumberFormat = "General";
                            }
                        }
                    }
                    //if (worker.CancellationPending) return;
                    string sheetName = "" + keyItemId + "(" + (sheetCount++) + ")" + "" + "x" + "" + faFirstColumn;
                    //Fix added for handling the duplicate names for excel sheet Redmine:#224064
                    if (FAsheetDict.ContainsKey(sheetName))
                    {
                        int keyIncValue = FAsheetDict[sheetName] + 1;
                        FAsheetDict[sheetName] = keyIncValue;//Updating the sheetname count by 1 to the Dictionary
                        sheetName = sheetName + " (" + keyIncValue + ")";
                    }
                    else
                    {
                        FAsheetDict.Add(sheetName, 1);//Adding the sheetname with count 1 to the Dictionary
                    }

                    sheet.Name = sheetName.Length <= 28 ? sheetName : sheetName.Substring(0, 28) + "...";
                    sheet.Cells[2, 2] = AddinResource.FA_ResultString_1;
                    sheet.Cells[3, 2] = AddinResource.FA_ResultString_2;
                    sheet.Cells[4, 2] = AddinResource.FA_ResultString_3;
                    sheet.Cells[2, 3] = keyItemId + ":" + Description;
                    sheet.Cells[3, 3] = "" + sheetCountinc++ + ": " + sectorInformations[i].Description;
                    sheet.Cells[4, 3] = dtTable.Rows.Count - 1;
                    sheet.Range["B2"].WrapText = true;
                    sheet.Range["B4"].WrapText = true;
                    
                    Excel.Range c31 = (Excel.Range)sheet.Cells[2, 2];
                    Excel.Range c32 = (Excel.Range)sheet.Cells[4, 3];
                    Excel.Range rangeHead1 = sheet.get_Range(c31, c32);
                    rangeHead1.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    BorderForCells(rangeHead1);
                    Excel.Range c33 = (Excel.Range)sheet.Cells[4, 2];
                    Excel.Range rangeHead2 = sheet.get_Range(c31, c33);
                    rangeHead2.Interior.Color = System.Drawing.Color.FromArgb(0, 112, 192);
                    rangeHead2.Font.Color = Excel.XlRgbColor.rgbGhostWhite;
                    range.Value2 = arr;
                    range.Columns.WrapText = false;
                    range.Columns.AutoFit();
                    bool? isChecked = isSort == null ? Globals.Ribbons.qc4Ribbon.checkBoxSort.Checked : isSort;
                    if (isChecked == true && dtTable.Rows.Count != 1)
                    {
                        SortAddonVariables(sheet, range, targetFaList.Count, addFaList.Count, dtTable.Rows.Count, 6);
                    }

                    Excel.Range rangeMax = sheet.get_Range(c31, c2);
                    int f = 0;
                    Range lastCol = null;
                    foreach (Range col in rangeMax.Columns)
                    {
                        if (f == 0)
                            col.ColumnWidth = 7.5;
                        else if (col.ColumnWidth > 57.1)
                            col.ColumnWidth = 57.1;
                        f++;
                        lastCol = col;
                    }
                    range.Columns.WrapText = true;
                    lastCol = lastCol.Next;
                    rangeMax.Rows.AutoFit();
                    Excel.Range dhEndMost = lastCol.End[Excel.XlDirection.xlToRight];
                    sheet.get_Range(lastCol, dhEndMost).EntireColumn.ColumnWidth = 57.1;
                    sheet.Range["B2"].ColumnWidth = 13;
                    if (sourceSheet != null)
                    {
                        sourceSheet.Application.EnableEvents = true;
                        sourceSheet.Application.ScreenUpdating = true;
                        sourceSheet.Application.DisplayAlerts = true;
                        sourceSheet.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                    }
                    sheet.Rows[3].WrapText = true;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        public static string CellValueTruncate(string cellValue, int length)
        {
            if (cellValue.Length > length)
            {
                cellValue = cellValue.Substring(0, length) + "...";
            }
            return cellValue;
        }

        private static void BorderForCells(Excel.Range range)
        {
            range.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThin;
            range.Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.Color.FromArgb(166, 166, 166);
            range.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThin;
            range.Borders[Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.Color.FromArgb(166, 166, 166);
            range.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThin;
            range.Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.Color.FromArgb(166, 166, 166);
            range.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThin;
            range.Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.Color.FromArgb(166, 166, 166);
            range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders[XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlHairline;
            range.Borders[XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.Color.FromArgb(166, 166, 166);
            range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders[XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlHairline;
            range.Borders[XlBordersIndex.xlInsideVertical].Color = System.Drawing.Color.FromArgb(166, 166, 166);
        }

        private static void DeleteExtraSheet(Excel.Application targetApp, Excel.Workbook targetBook)
        {
            try
            {
                targetApp.DisplayAlerts = false;
                Worksheet wrksheet1 = targetBook.Worksheets["FA-Temp1"];
                Worksheet wrksheet2 = targetBook.Worksheets["FA-Temp2"];

                if (wrksheet1 == null || wrksheet2 == null)
                { return; }
                targetBook.Activate();
                wrksheet1.Activate();
                wrksheet1.Delete();
                wrksheet2.Activate();
                wrksheet2.Delete();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            finally
            {
                targetApp.DisplayAlerts = true;
            }
        }


        private static bool InvalidVariableCheck(Excel.Range range, Worksheet sht, bool? isSort = null)
        {
            foreach (Excel.Range targetCell in range.Cells)
            {
                rCount++;
                Definitions.fAVariable.Clear();
                Definitions.fAAddtionalVariable.Clear();
                if (targetCell.Value2 == "○" || targetCell.Value2 == "On")
                {
                    Range conditionVariable = targetCell.Offset[0, 1]; // condition variable
                    if (conditionVariable.Text != "" && !Definitions.VariableDictionary.ContainsKey(conditionVariable.Text))
                    {
                        varInvalid = true;
                        errorMessage = AddinResource.ERR_MSG_INVALID_ITEM;
                        targetCell.Offset[0, 1].Activate();
                        return varInvalid;
                    }

                    Range faVariableStart = targetCell.Offset[0, 2];
                    Range faVariableEnd = targetCell.Offset[0, 31];
                    Range faVariables = sht.Range[faVariableStart, faVariableEnd];// Fa variable
                    foreach (Excel.Range faVariable in faVariables.Cells)
                    {
                        if (!String.IsNullOrEmpty(faVariable.Text))
                        {
                            if (!Definitions.VariableDictionary.ContainsKey(faVariable.Text))
                            {
                                varInvalid = true;
                                errorMessage = AddinResource.ERR_MSG_INVALID_ITEM;
                                faVariable.Activate();
                                return varInvalid;
                            }
                            else
                            {
                                QuestionSettings variableDetails = Definitions.VariableDictionary[faVariable.Text];
                                if (variableDetails.AnswerType != QuestionType.FA.ToString())
                                {
                                    varInvalid = true;
                                    errorMessage = AddinResource.ERR_MSG_INVALID_FA_ITEM;
                                    faVariable.Activate();
                                    return varInvalid;
                                }
                            }
                        }
                    }
                    Range addOnVariableStart = targetCell.Offset[0, 32];
                    Range addOnVariableEnd = targetCell.Offset[0, 62];
                    Range addOnVariables = sht.Range[addOnVariableStart, addOnVariableEnd];// add-on variable
                    foreach (Excel.Range addOnVariable in addOnVariables.Cells)
                    {
                        if (!String.IsNullOrEmpty(addOnVariable.Text))
                        {
                            if (!Definitions.VariableDictionary.ContainsKey(addOnVariable.Text))
                            {
                                varInvalid = true;
                                errorMessage = AddinResource.ERR_MSG_INVALID_ITEM;
                                addOnVariable.Activate();
                                return varInvalid;
                            }
                        }
                    }
                }
                else
                    continue;
            }
            return varInvalid;
        }

        private static string GetTemplatePath(string templateName)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + templateName;
        }

        private static void SortAddonVariables(Excel.Worksheet faSheet, Excel.Range range, int faCount, int addfaCount, int rows, int start)
        {
            faSheet.Sort.SortFields.Clear();
            for (int i = faCount + 2; i <= faCount + addfaCount; i++)
            {
                Excel.Range r1 = (Excel.Range)faSheet.Cells[start, i + 1];
                Excel.Range r2 = (Excel.Range)faSheet.Cells[start + (rows - 1), i + 1];
                Excel.Range r = (Excel.Range)faSheet.get_Range(r1, r2);
                if (r.Find("DK") != null)
                    faSheet.Sort.SortFields.Add(Key: r, SortOn: XlSortOn.xlSortOnValues, Order: XlSortOrder.xlAscending, DataOption: XlSortDataOption.xlSortTextAsNumbers);
                else
                    faSheet.Sort.SortFields.Add(Key: r, SortOn: XlSortOn.xlSortOnValues, Order: XlSortOrder.xlAscending, DataOption: XlSortDataOption.xlSortNormal);
            }
            faSheet.Sort.SetRange(range);
            faSheet.Sort.MatchCase = false;
            faSheet.Sort.Header = Excel.XlYesNoGuess.xlYes;
            faSheet.Sort.Orientation = Excel.XlSortOrientation.xlSortColumns;
            faSheet.Sort.SortMethod = Excel.XlSortMethod.xlPinYin;
            faSheet.Sort.Apply();
        }

        private static QuestionType getQType(string questionType)
        {

            switch (questionType)
            {
                case "SA":
                    return QuestionType.SA;
                case "FA":
                    return QuestionType.FA;
                case "MA":
                    return QuestionType.MA;
                case "N":
                    return QuestionType.N;
                default:
                    return QuestionType.FA;
            }

        }


        private static FAListTabulation.QuestionInformation GetGroupingItemInfo(decimal qcwebId, string keyItemId, List<Data> surveyRootPath, Questions questions)
        {
            QuestionSettings questionDetails = Definitions.VariableDictionary[keyItemId];
            Common.FAChanges swap = new FAChanges();
            QuestionType qType = getQType(questionDetails.AnswerType);
            using (SQLiteConnection dbSource = DB.DBHelper.GetConnection(SourcePath))
            {
                try
                {
                    dbSource.Open();
                    System.Data.DataTable dtReadTable1 = new System.Data.DataTable();
                    System.Data.DataTable dtReadTableqstn = new System.Data.DataTable();
                    string sqlquestion = "SELECT id FROM question where variable='" + keyItemId + "'";
                    dtReadTableqstn = DB.DBHelper.GetDataTable(sqlquestion, dbSource);
                    dtReadTable1 = DB.DBHelper.GetDataTable(sqlquestion, dbSource);
                    if (dtReadTableqstn.Rows.Count == 0)
                    {
                        //return;
                    }
                    sqlquestion = "SELECT COUNT(id) FROM question where variable='" + keyItemId + "' AND question_flag = 'An';";
                    if (DB.DBHelper.ExecuteScalar(sqlquestion, dbSource) > 0)
                    {
                        string id = dtReadTableqstn.Rows[0]["id"].ToString();
                        string sql = "SELECT m.q_" + id + " FROM multivariate m join " + tableName+" a on a.sort_no=m.sort_no;";
                        dtReadTable1 = DB.DBHelper.GetDataTable(sql, dbSource);
                    }
                    else
                    {
                        string id = dtReadTableqstn.Rows[0]["id"].ToString();
                        string sql = "SELECT q_" + id + " FROM " + tableName;
                        dtReadTable1 = DB.DBHelper.GetDataTable(sql, dbSource);
                    }
                    if (dtReadTable1.Rows.Count == 0)
                    {
                        //return;
                    }
                    List<Data> groupingDataList = ReadTextFile.ReadDataTable(dtReadTable1, qType, null, out ex);
                    FAListTabulation.QuestionInformation groupingQuestionInformation =
                new FAListTabulation.QuestionInformation(qType, questionDetails.Variable
                                                                                        , questionDetails.Question
                                                                                        , groupingDataList);

                    int i = 0;
                    foreach (string cat in questionDetails.Choices)
                    {
                        groupingQuestionInformation.AddSectorInformation(
                     new FAListTabulation.SectorInformation(i++, cat));

                    }

                    return groupingQuestionInformation;
                }

                finally
                {
                    dbSource.Close();

                }
            }
        }


        private static List<FAListTabulation.QuestionInformation> GetFaAddItemInfoList(decimal qcwebId, decimal scenarioTotalizationId, List<Data> surveyRootPath, Questions questions)
        {
            List<FAListTabulation.QuestionInformation> retList = new List<FAListTabulation.QuestionInformation>();

            foreach (string variableName in Definitions.fAAddtionalVariable)
            {
                if (!Definitions.VariableDictionary.ContainsKey(variableName))
                {
                    return null;
                }
                QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];
                if (questionDetails != null)
                {
                    faAddQuestions.Add(questionDetails);
                }

                Common.FAChanges swap = new FAChanges();
                QuestionType qType = getQType(questionDetails.AnswerType);
                using (SQLiteConnection dbSource = DB.DBHelper.GetConnection(SourcePath))
                {
                    try
                    {
                        dbSource.Open();
                        System.Data.DataTable dtReadTable1 = new System.Data.DataTable();
                        System.Data.DataTable dtReadTableqstn = new System.Data.DataTable();
                        string sqlquestion = "SELECT id FROM question where variable='" + variableName + "'";
                        if (variableName == "SAMPLEID")
                        {
                            string sql = "SELECT sample_id FROM " + tableName;
                            dtReadTable1 = DB.DBHelper.GetDataTable(sql, dbSource);
                            if (dtReadTable1.Rows.Count == 0)
                            {
                                //return;
                            }
                        }
                        else if (questionDetails.QuestionFlag == "An")
                        {
                            dtReadTableqstn = DB.DBHelper.GetDataTable(sqlquestion, dbSource);
                            dtReadTable1 = DB.DBHelper.GetDataTable(sqlquestion, dbSource); 
                            if (dtReadTableqstn.Rows.Count == 0)
                            {
                                //return;
                            }
                            else
                            {
                                string id = dtReadTableqstn.Rows[0]["id"].ToString();
                                string sql = "SELECT q_" + id + " FROM multivariate m join " + tableName + " a on a.sort_no = m.sort_no ";
                                dtReadTable1 = DB.DBHelper.GetDataTable(sql, dbSource);
                            }
                        }
                        else
                        {
                            dtReadTableqstn = DB.DBHelper.GetDataTable(sqlquestion, dbSource);
                            dtReadTable1 = DB.DBHelper.GetDataTable(sqlquestion, dbSource);
                            if (dtReadTableqstn.Rows.Count == 0)
                            {
                                //return;
                            }
                            else
                            {
                                string id = dtReadTableqstn.Rows[0]["id"].ToString();
                                string sql = "SELECT q_" + id + " FROM " + tableName;
                                dtReadTable1 = DB.DBHelper.GetDataTable(sql, dbSource);
                                if (dtReadTable1.Rows.Count == 0)
                                {
                                    //return;
                                }
                            }
                        }

                        List<Data> faDataList = ReadTextFile.ReadDataTable(dtReadTable1, qType, null, out ex);
                        //FAListTabulation.QuestionInformation addInfo =
                        //	new FAListTabulation.QuestionInformation(qType, questionDetails.Question, faDataList);
                        FAListTabulation.QuestionInformation addInfo =
                           new FAListTabulation.QuestionInformation(qType, questionDetails.TableHeading, questionDetails.Question, faDataList);

                        int i = 0;
                        foreach (string cat in questionDetails.Choices)
                        {
                            addInfo.AddSectorInformation(new FAListTabulation.SectorInformation(i++, cat));
                        }
                        retList.Add(addInfo);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                    finally
                    {
                        dbSource.Close();

                    }
                }
            }
            return retList;
        }


        private static FAListTabulation.QuestionInformation GetFaSampleIdInfoList(decimal qcwebId, decimal scenarioTotalizationId, List<Data> surveyRootPath, Questions questions)
        {
            QuestionSettings questionDetails = Definitions.VariableDictionary["SAMPLEID"];
            Common.FAChanges swap = new FAChanges();
            QuestionType qType = getQType(questionDetails.AnswerType);
            using (SQLiteConnection dbSource = DB.DBHelper.GetConnection(SourcePath))
            {
                try
                {
                    dbSource.Open();
                    System.Data.DataTable dtReadTable1 = new System.Data.DataTable();
                    string sql = "SELECT sample_id FROM " + tableName;
                    dtReadTable1 = DB.DBHelper.GetDataTable(sql, dbSource);
                    if (dtReadTable1.Rows.Count == 0)
                    {
                        //return;
                    }

                    List<Data> faDataList = ReadTextFile.ReadDataTable(dtReadTable1, qType, null, out ex);
                    //FAListTabulation.QuestionInformation addInfo = new FAListTabulation.QuestionInformation(qType, "回答者ID", faDataList);
                    FAListTabulation.QuestionInformation addInfo =
                           new FAListTabulation.QuestionInformation(qType, questionDetails.TableHeading, questionDetails.Question, faDataList);
                    return addInfo;
                }
                finally
                {
                    dbSource.Close();

                }
            }
        }


        private static List<FAListTabulation.QuestionInformation> GetFaItemInfoList(decimal qcwebId, decimal faScenarioHeaderId, List<Data> surveyRootPath, Questions questions, Excel.Worksheet sheet = null)
        {
            List<FAListTabulation.QuestionInformation> retList = new List<FAListTabulation.QuestionInformation>();
            foreach (string variableName in Definitions.fAVariable)
            {
                if (!Definitions.VariableDictionary.ContainsKey(variableName))
                {
                    return null;
                }
                QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];
                if (questionDetails != null)
                {
                    faQuestions.Add(questionDetails);
                }
                Common.FAChanges swap = new FAChanges();
                QuestionType qType = getQType(questionDetails.AnswerType);
                using (SQLiteConnection dbSource = DB.DBHelper.GetConnection(SourcePath))
                {
                    try
                    {
                        dbSource.Open();

                        System.Data.DataTable dtReadTable1 = new System.Data.DataTable();
                        System.Data.DataTable dtReadTableqstn = new System.Data.DataTable();
                        string sqlquestion = "SELECT id FROM question where variable='" + variableName + "'";
                        dtReadTableqstn = DB.DBHelper.GetDataTable(sqlquestion, dbSource);
                        dtReadTable1 = DB.DBHelper.GetDataTable(sqlquestion, dbSource);
                        if (dtReadTableqstn.Rows.Count == 0)
                        {
                            //return;
                        }
                        else
                        {
                            string id = dtReadTableqstn.Rows[0]["id"].ToString();
                            string sql = "";
                            if (variableName == "SAMPLEID")
                                sql = "SELECT sample_id FROM " + tableName;
                            else if(questionDetails.QuestionFlag=="An")
                                sql = "SELECT q_" + id + " FROM multivariate m join " + tableName + " a on a.sort_no = m.sort_no ";
                            else
                                sql = "SELECT q_" + id + " FROM " + tableName;
                            dtReadTable1 = DB.DBHelper.GetDataTable(sql, dbSource);
                            if (dtReadTable1.Rows.Count == 0)
                            {
                                //return;
                            }
                            //DataTable dtReadTable1 = null;
                            List<Data> faDataList = ReadTextFile.ReadDataTable(dtReadTable1, qType, null, out ex);
                            FAListTabulation.QuestionInformation faInfo = new FAListTabulation.QuestionInformation(questionDetails.Question, questionDetails.TableHeading, faDataList);

                            retList.Add(faInfo);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                    finally
                    {
                        dbSource.Close();

                    }
                }
            }
            return retList;
        }


        internal static void SheetBeforeDoubleClick(Worksheet sheet, Range target)
        {
            if (IsExecColum(sheet, target))
            {
                ExecToggle(sheet, target);
            }
        }

        private static bool IsExecColum(Worksheet sheet, Range target)
        {
            if (GetExecCell(target).Column == target.Column)
            {
                return true;
            }
            return false;
        }

        internal static void ExecToggle(Worksheet sheet, Range target, bool dClickFlg = true)
        {
            Excel.Application application = sheet.Application;
            bool eEFlag = application.EnableEvents;
            application.EnableEvents = false;
            Range Exec_Cell = GetExecCell(target);

            if (Exec_Cell.End[XlDirection.xlToRight].Column == ExcelUtil.EndColumn(sheet))
            {

                if (Exec_Cell.Value2 != "")
                {
                    QC4Common.Util.ExcelUtil.ClearContents(Exec_Cell);
                }
            }
            else
            {
                if (null == Exec_Cell.Value2 || string.IsNullOrEmpty(Exec_Cell.Value2.ToString()))//|| AddinResource.MarkOFF == Exec_Cell.Value2)
                {
                    Exec_Cell.Value2 = AddinResource.MarkWhiteCircle;
                }
                else
                {
                    if (dClickFlg)
                    {
                        if (target.Value2 == AddinResource.MarkWhiteCircle)
                            Exec_Cell.Value2 = AddinResource.MarkOFF;
                        else
                            Exec_Cell.Value2 = AddinResource.MarkWhiteCircle;
                    }
                    else
                    {
                        if (target.Column != 2)
                            Exec_Cell.Value2 = AddinResource.MarkWhiteCircle;
                    }
                }
            }
            application.EnableEvents = eEFlag;
        }

        private static Range GetExecCell(Range target)
        {
            int col = 2;
            Worksheet targetSheet = target.Worksheet;
            if (targetSheet.CodeName.Equals(Common.Constants.SheetType.GTCounting))
            {
                col = 2;
            }
            return targetSheet.Cells[target.Row, col];
        }

        private void clearFaSheetDict()
        {
            FAsheetDict.Clear();
        }

    }
}

