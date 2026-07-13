using log4net;
using Macromill.QCWeb.Batch.Report;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using Qc4Launcher.Logic.DPCheckList;
using Qc4Launcher.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Common.Constants;
using XlFileFormat = Microsoft.Office.Interop.Excel.XlFileFormat;
using XlPageOrientation = Macromill.QCWeb.Common.XlPageOrientation;
using XlPaperSize = Macromill.QCWeb.Common.XlPaperSize;

namespace Qc4Launcher.Logic
{
    public class CrossCreator
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public OutputCross CurrentOutput = null;
        public Worksheet WorkingSheet;
        public Workbook WorkingBook;
        public Application xlApp;
        public Workbooks wbs;
        private Sheets wss;
        public string ThisLocationCode;
        public ExecuteStaticMethod ExecuteStaticMethod = new ExecuteStaticMethod();
        private static double COL_MIN_WIDTH = 42;
        private static double COL_MIN_COLWIDTH = 8.5;
        public static double ROW_MAX_HEIGHT = 408.75;

        public static string TEMPLATE_NAME = "Cross.xlt";
        public static string TRANSPOSE_TEMPLATE_NAME = "CrossPortrait.xlt";
        public static string INDIVIDUAL_TEMPLATE_NAME = "CrossNP.xlt";
        public static string INDIVIDUAL_TEMPLATE_NAME_NP = "Cross_np.xltx";
        public static string INDIVIDUAL_TEMPLATE_NAME_N = "Cross_n.xltx";
        public static string INDIVIDUAL_TEMPLATE_NAME_P = "Cross_p.xltx";
        public static string INDIVIDUAL_TEMPLATE_NAME_T = "Cross_ps.xltx";
        public static string TRANSPOSE_INDIVIDUAL_TEMPLATE_NAME = "CrossNPPortrait.xlt";
        public static string REPORT_TEMPLATE_NAME = "Report.xlt";
        public static string TRANSPOSE_REPORT_TEMPLATE_NAME = "ReportPortrait.xlt";

        public static string FORMAT_TEMPLATE_NAME = "CrossFormat.xlt";
        public static string TRANSPOSE_FORMAT_TEMPLATE_NAME = "CrossPortraitFormat.xlt";
        public static string INDIVIDUAL_FORMAT_TEMPLATE_NAME = "CrossNPFormat.xlt";
        public static string TRANSPOSE_INDIVIDUAL_FORMAT_TEMPLATE_NAME = "CrossNPPortraitFormat.xlt";
        public static string REPORT_FORMAT_TEMPLATE_NAME = "ReportFormat.xlt";
        public static string TRANSPOSE_REPORT_FORMAT_TEMPLATE_NAME = "ReportPortraitFormat.xlt";
        public static string CHECK_CROSS_SHEET = "Check Cross";
        public static string CELL_RANGE_MULTI = "B3:R3";
        public static string CELL_RANGE_SINGLE_3 = "C3:R3";
        public static string CELL_RANGE_SINGLE_4 = "D4:R4";
        public string BookPSWD;
        public string SheetPSWD;
        public CrossTabulationQC QC;
        public CheckCrossQC CQC;
        public double progressAvailable;
        public double currentProgress;
        private string OutputDirectoryPath;
        private string prfix;
        public string TemplateDirectoryPath;
        public bool onlySigPage;
        private bool checkCross = false;
        private bool checkList = false;
        public static int BORDER_COLOR = 0XA9A9A9;
        private Dictionary<string, double> colWidthMap;
        private List<int> checkCrsLnLst;
        public BackgroundWorker bgWorker;
        public List<string> outputFiles;
        public List<string> outputFileSig;

        public string GetReportKeyword(ReportMessageIndex Index, params string[] replaceWords)
        {
            String joinedReplaceWords;
            if (replaceWords.Length > 0)
            {
                joinedReplaceWords = string.Join("\t", replaceWords);
                return ExecuteStaticMethod.GetReportKeyword(Index, ThisLocationCode, joinedReplaceWords);
            }
            else
            {
                return ExecuteStaticMethod.GetReportKeyword(Index, ThisLocationCode);
            }
        }

        public string GetUnescapedReportKeyword(ReportMessageIndex Index, params string[] replaceWords)
        {
            String joinedReplaceWords;
            if (replaceWords.Length > 0)
            {
                joinedReplaceWords = string.Join("\v", replaceWords);
                return ExecuteStaticMethod.GetReportKeyword(Index, ThisLocationCode, joinedReplaceWords, true);
            }
            else
            {
                return ExecuteStaticMethod.GetReportKeyword(Index, ThisLocationCode, null, true);
            }
        }
        double ProgressBarMovement = 0;
        public void CreateCross(Output Output, string bookPSWD, string sheetPSWD, string outputDirectoryPath,
            string templateDirectoryPath, Application xlAppG, BackgroundWorker bgWorker, DoWorkEventArgs bgWorkerArg,
           out double  ProgressBarMovement,
            bool onlySigPageP = false, bool checkCrossP = false,
            bool checkListP = false, CrossTabulationQC QC = null, double progressAvailable = 0, double currentProgress = 0,
            CheckCrossQC CQC = null, List<int> checkCrsLnLstP = null, string qc4FileName = null, List<string> outputFiles = null,
            List<string> outputFileSig = null)
        {
            ProgressBarMovement = 0;
            ProgressBarMovement = this.ProgressBarMovement;
            XlFileFormat xlFmt = XlFileFormat.xlOpenXMLWorkbook;
            Reportset reportset = (Reportset)Output.ParentReportset;
            CurrentOutput = (OutputCross)Output;
            BookPSWD = bookPSWD;
            SheetPSWD = sheetPSWD;
            OutputDirectoryPath = outputDirectoryPath;
            prfix = NPOICrossCreator.getPrefix(qc4FileName);
            TemplateDirectoryPath = templateDirectoryPath;
            this.progressAvailable = progressAvailable;
            this.currentProgress = currentProgress;
            this.QC = QC;
            this.CQC = CQC;
            Workbook FormatBook = null;
            Workbook baseBook = null;
            wbs = null;
            wss = null;
            onlySigPage = onlySigPageP;
            xlApp = xlAppG;
            this.bgWorker = bgWorker;
            checkCross = checkCrossP;
            checkList = checkListP;
            checkCrsLnLst = checkCrsLnLstP;
            colWidthMap = new Dictionary<string, double>();
            if (outputFiles == null) outputFiles = new List<string>();
            this.outputFiles = outputFiles;
            this.outputFileSig = outputFileSig;

            try
            {
                wbs = xlApp.Workbooks;
                baseBook = wbs.Add(OutputUtil.BaseTemplatePath(TemplateDirectoryPath, xlApp.PathSeparator));
                foreach (Worksheet sh in baseBook.Worksheets)
                    sh.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                _log.Info("Excel base book added");
                xlApp.Calculation = XlCalculation.xlCalculationManual;
                WorkingBook = baseBook;
                wss = baseBook.Worksheets;
                WorkingSheet = wss.Item[1];
                string TempPath = TemplatePath(ref xlFmt, CurrentOutput.Orientation);
                string FormatPath = FormatTemplatePath(CurrentOutput.Orientation);
                CrossTable tmpTable = (CrossTable)Output.Tables[0];
                Macromill.QCWeb.ReportRequest.KeyItemInformation KeyItemInfo = tmpTable.KeyItem;
                string KeyItemName = string.Empty;
                string filenameSuffix = null;
                if (KeyItemInfo != null)
                    KeyItemName = KeyItemInfo.Name;
                if (KeyItemName.Length > 0)
                {
                    //int n = reportset.KeyItemMaxSectorNumber(KeyItemName);
                    //if (n > 0) n = (int)(Math.Log(n) / Math.Log(10)) + 1;
                    string fmt = new string('0', 4);
                    filenameSuffix = "_" + KeyItemName + "_" + KeyItemInfo.SectorNumber.ToString(fmt);
                }
                FormatBook = wbs.Add(FormatPath);
                foreach (Worksheet sh in FormatBook.Worksheets)
                    sh.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                _log.Info("Excel format book added");
                FormatBook.Unprotect(BookPSWD);
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi && !onlySigPage)
                {
                    CreateStandardCross(TempPath, FormatBook, xlFmt, filenameSuffix);
                    ProgressBarMovement = this.ProgressBarMovement;
                }
                else
                {
                    CreateIndividualCross(FormatBook, xlFmt, filenameSuffix);
                    ProgressBarMovement = this.ProgressBarMovement;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
                try
                {
                    FormatBook.Close(false);
                }
                catch (Exception ex)
                {
                }
                try
                {
                    baseBook.Close(false);
                }
                catch (Exception ex)
                {
                }
                try
                {
                    FormatBook.Close(false);
                }
                catch (Exception ex)
                {
                }
                COMWholeOperate.releaseComObject<Worksheet>(ref WorkingSheet);
                COMWholeOperate.releaseComObject(ref wss);
                COMWholeOperate.releaseComObject(ref FormatBook);
                COMWholeOperate.releaseComObject<Workbook>(ref baseBook);
                COMWholeOperate.releaseComObject<Workbook>(ref WorkingBook);
                COMWholeOperate.releaseComObject<Workbooks>(ref wbs);
                GC.Collect();
            }
        }

        private void CreateStandardCross(string TempPath, Workbook FormatBook, XlFileFormat FileFormat, string Suffix)
        {
            _log.Info("Create standard cross");
            Workbook NewBook = null;
            Sheets newBookWShhets = null;
            // Application Application = null;
            try
            {
                CrossTable tmpTable;
                CrossTable tmpNextTable;
                bool HasWeightColumn = false;
                int MaxAxesCount = 0;
                bool isN = false;
                bool HasWeight;
                string FormatRangeNamePrefix = null;
                Hashtable CutRowsCol = null;
                Hashtable CutColumnsCol = null;

                string ReportTitle;
                string header;

                int n;
                string fmt;
                string strIdx;

                bool PageSetupOn;
                Array ContentsValue = null;
                Array HyperlinkTargetCells = null; //Range
                Array PageSetupContentsValue = null; //string
                Array PageSetupHyperlinkTargetCells = null; //range

                string PageSetupSheetBaseName = null;
                Worksheet ContentsSheet = null;
                Worksheet TemplateSheet = null;
                Worksheet SigTestTemplateSheet = null;
                Worksheet FormatSheet = null;
                Worksheet SigTestFormatSheet = null;
                Worksheet PageSetupTemplateSheet = null;
                Worksheet PageSetupSigTestTemplateSheet = null;
                Worksheet NPerSheet = null;
                Worksheet NSheet = null;
                Worksheet PerSheet = null;
                Worksheet SigTestSheet = null;
                Worksheet PageSetupContentsSheet;
                Worksheet PageSetupNPerSheet = null;
                Worksheet PageSetupNSheet = null;
                Worksheet PageSetupPerSheet = null;
                Worksheet PageSetupSigTestSheet = null;
                Range NPerStartCell = null;
                Range NStartCell = null;
                Range PerStartCell = null;
                Range SigTestStartCell = null;
                Range PageSetupNPerStartCell = null;
                Range PageSetupNStartCell = null;
                Range PageSetupPerStartCell = null;
                Range PageSetupSigTestStartCell = null;
                Worksheet tmpSheet = null;
                Worksheet tmpPageSetupSheet = null;
                string tmp = null;
                int i;
                int j;
                double[] h;
                double maxH;
                int PageSetupColumnsCountPerPage = 0;
                int MaxRowsCountPerPage = 0;
                int NPerRemainedRowsCount = 0;
                int NRemainedRowsCount = 0;
                int PerRemainedRowsCount = 0;
                int SigTestRemainedRowsCount = 0;
                double DefLineHeight = 0;
                bool SigTestOn = false;
                int SigTestPageSetupColumnsCountPerPage = 0;
                Hashtable WholeRowCol = null;

                bool HasShowPreWBTotal = false;
                TableOrientation tmpOrientation;
                int tmpTablesCount;

                //Dim res MethodResult
                // ErrorStruct[] Errors;
                int ErrorsCount;
                //On Error GoTo ErrHdl
                NewBook = wbs.Add(TempPath);
                foreach (Worksheet sh in NewBook.Worksheets)
                    sh.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                _log.Info("Template book added");
                NewBook.Unprotect(BookPSWD);

                PageSetupOn = CurrentOutput.PageSetup & CurrentOutput.Orientation == TableOrientation.Landscape;
                bool CutMedian = CurrentOutput.ParentRequest.ShowMedian & CurrentOutput.WBOn;
                if (PageSetupOn)
                {
                    switch (CurrentOutput.PaperSize)
                    {
                        case XlPaperSize.xlPaperA3:
                            PageSetupSheetBaseName = "A3";
                            break;
                        case XlPaperSize.xlPaperB4:
                            PageSetupSheetBaseName = "B4";
                            break;
                        default:
                            PageSetupSheetBaseName = "A4";
                            break;
                    }
                    if (CurrentOutput.PaperOrientation == XlPageOrientation.xlPortrait)
                    {
                        PageSetupSheetBaseName = PageSetupSheetBaseName + "Portrait";
                    }
                    else
                    {
                        PageSetupSheetBaseName = PageSetupSheetBaseName + "Landscape";
                    }
                }
                if ((CurrentOutput.SignificanceTestOne || CurrentOutput.SignificanceTestFive || CurrentOutput.SignificanceTestTen)
                    && (!CurrentOutput.SignificanceTestOne || !CurrentOutput.SignificanceTestFive || !CurrentOutput.SignificanceTestTen))
                {
                    SigTestOn = true;
                }

                Hashtable tmpCol = new Hashtable();
                int[] MaxAxesCountArray = new int[CurrentOutput.Tables.Count];
                for (i = 0; i < CurrentOutput.Tables.Count; i++)
                {
                    tmpTable = (CrossTable)CurrentOutput.Tables[i];
                    MaxAxesCountArray[i] = GetMaxAxesCount(tmpTable);
                    if (MaxAxesCount < 2) MaxAxesCount = MaxAxesCountArray[i];
                    if (!HasWeightColumn) HasWeightColumn = GetHasWeight(tmpTable);
                }



                //With NewBook.Worksheets
                newBookWShhets = NewBook.Worksheets;
                ContentsSheet = newBookWShhets.Item["INDEX"];
                ContentsSheet.Name = LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME;
                tmpCol.Add(ContentsSheet.Name, string.Empty);
                AdjustContentsSheet(null, ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, (TableType)(Convert.ToInt16(TableType.SignificanceTest) & ToInt(SigTestOn)));
                _log.Info("AdjustContentsSheet completed");
                if (CurrentOutput.Orientation == TableOrientation.Landscape)
                {
                    tmp = MaxAxesCount == 2 ? "Triple" : "double";
                    if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                    {
                        TemplateSheet = newBookWShhets.Item[tmp + "Standard"];
                        tmpCol.Add(TemplateSheet.Name, string.Empty);
                        if (PageSetupOn)
                        {
                            PageSetupTemplateSheet = newBookWShhets.Item[tmp + PageSetupSheetBaseName];
                            DefLineHeight = PageSetupTemplateSheet.Rows.Item[1].Height;
                            MaxRowsCountPerPage = PageSetupTemplateSheet.Range["A1"].Value;
                            PageSetupColumnsCountPerPage = PageSetupTemplateSheet.Range["B1"].Value;
                            tmpCol.Add(PageSetupTemplateSheet.Name, string.Empty);
                        }
                    }
                    if (SigTestOn)
                    {
                        SigTestTemplateSheet = newBookWShhets.Item[tmp + "SigTest"];
                        tmpCol.Add(SigTestTemplateSheet.Name, string.Empty);
                        if (CurrentOutput.PageSetupSignificanceTestTable)
                        {
                            PageSetupSigTestTemplateSheet = newBookWShhets.Item[tmp + "SigTest" + PageSetupSheetBaseName];
                            if (DefLineHeight == 0) { DefLineHeight = PageSetupSigTestTemplateSheet.Rows.Item[1].Height; }
                            if (MaxRowsCountPerPage == 0) { MaxRowsCountPerPage = PageSetupSigTestTemplateSheet.Range["A1"].Value; }
                            SigTestPageSetupColumnsCountPerPage = PageSetupSigTestTemplateSheet.Range["B1"].Value;
                            tmpCol.Add(PageSetupSigTestTemplateSheet.Name, string.Empty);
                        }
                    }
                }
                else
                {
                    TemplateSheet = newBookWShhets.Item["Template"];
                    if (PageSetupOn)
                    {
                        PageSetupTemplateSheet = newBookWShhets.Item[PageSetupSheetBaseName];
                    }
                    if (SigTestOn)
                    {
                        TemplateSheet.Copy(Type.Missing, newBookWShhets.Item[newBookWShhets.Count]);
                        SigTestTemplateSheet = newBookWShhets.Item[newBookWShhets.Count];
                        tmpCol.Add(SigTestTemplateSheet.Name, string.Empty);
                        if (CurrentOutput.PageSetupSignificanceTestTable)
                        {
                            PageSetupTemplateSheet.Copy(Type.Missing, SigTestTemplateSheet);
                            PageSetupSigTestTemplateSheet = SigTestTemplateSheet.Next;
                            tmpCol.Add(PageSetupSigTestTemplateSheet.Name, string.Empty);
                        }
                    }
                    if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                    {
                        tmpCol.Add(TemplateSheet.Name, string.Empty);
                        if (PageSetupTemplateSheet != null)
                        {
                            tmpCol.Add(PageSetupTemplateSheet.Name, string.Empty);
                        }
                    }
                    else
                    {
                        TemplateSheet = null;
                        PageSetupTemplateSheet = null;
                    }
                }

                foreach (Worksheet sht in newBookWShhets)
                {
                    if (!tmpCol.ContainsKey(sht.Name))
                    {
                        sht.Delete();
                    }
                }
                //    On Error GoTo ErrHdl
                ReportTitle = CurrentOutput.ParentRequest.Title;
                header = OutputUtil.GetAdjustedHeader(ReportTitle);
                if (TemplateSheet != null)
                {
                    TemplateSheet.Unprotect(SheetPSWD);
                    TemplateSheet.PageSetup.CenterHeader = header;
                }
                if (PageSetupTemplateSheet != null)
                {
                    PageSetupTemplateSheet.Unprotect(SheetPSWD);
                    PageSetupTemplateSheet.PageSetup.CenterHeader = header;
                }
                if (SigTestTemplateSheet != null)
                {
                    SigTestTemplateSheet.Unprotect(SheetPSWD);
                    SigTestTemplateSheet.PageSetup.CenterHeader = header;
                }
                if (PageSetupOn)
                {
                    ContentsSheet.Copy(Type.Missing, ContentsSheet);
                    PageSetupContentsSheet = ContentsSheet.Next;
                    tmp = LocalResource.REPORT_CROSS_PAGE_SETUP_SHEET_SUFFIX;
                    PageSetupContentsSheet.Name = LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME + tmp;
                    PageSetupContentsValue = Array.CreateInstance(typeof(string),
                        new int[] { ContentsValue.GetUpperBound(0), ContentsValue.GetUpperBound(1) },
                        new int[] { 1, 1 });

                    PageSetupContentsValue = Array.CreateInstance(typeof(string),
                        new int[] { HyperlinkTargetCells.GetUpperBound(0), HyperlinkTargetCells.GetUpperBound(1) },
                        new int[] { 1, HyperlinkTargetCells.GetLowerBound(1) });

                }
                if (CurrentOutput.OutputNPerTable)
                {
                    NPerSheet = TemplateSheet;
                    if (!checkCross)
                    {
                        NPerSheet.Name = LocalResource.REPORT_CROSS_NP_SHEET_NAME;
                    }
                    else
                    {
                        NPerSheet.Name = CHECK_CROSS_SHEET; // to do
                    }
                    NPerStartCell = NPerSheet.Range["A1"];
                    tmpSheet = NPerSheet;
                    if (PageSetupOn)
                    {
                        if (CurrentOutput.PageSetupNPerTable)
                        {
                            NPerRemainedRowsCount = MaxRowsCountPerPage;
                            PageSetupNPerSheet = PageSetupTemplateSheet;
                            PageSetupNPerSheet.Name = NPerSheet.Name + tmp;
                            PageSetupNPerStartCell = PageSetupNPerSheet.Range["A1"];
                            tmpPageSetupSheet = PageSetupNPerSheet;
                        }
                    }
                }
                if (CurrentOutput.OutputNTable)
                {
                    if (tmpSheet == null)
                    {
                        NSheet = TemplateSheet;
                    }
                    else
                    {
                        TemplateSheet.Copy(Type.Missing, tmpSheet);
                        NSheet = tmpSheet.Next;
                    }
                    NSheet.Name = LocalResource.REPORT_CROSS_N_SHEET_NAME;
                    NStartCell = NSheet.Range["A1"];
                    tmpSheet = NSheet;
                    if (PageSetupOn)
                    {
                        if (CurrentOutput.PageSetupNTable)
                        {
                            NRemainedRowsCount = MaxRowsCountPerPage;
                            if (tmpPageSetupSheet == null)
                            {
                                PageSetupNSheet = PageSetupTemplateSheet;
                            }
                            else
                            {
                                PageSetupTemplateSheet.Copy(Type.Missing, tmpPageSetupSheet);
                                PageSetupNSheet = tmpPageSetupSheet.Next;
                            }
                            PageSetupNSheet.Name = NSheet.Name + tmp;
                            PageSetupNStartCell = PageSetupNSheet.Range["A1"];
                            tmpPageSetupSheet = PageSetupNSheet;
                        }
                    }
                }
                if (CurrentOutput.OutputPerTable)
                {
                    if (tmpSheet == null)
                    {
                        PerSheet = TemplateSheet;
                    }
                    else
                    {
                        TemplateSheet.Copy(Type.Missing, tmpSheet);
                        PerSheet = tmpSheet.Next;
                    }
                    PerSheet.Name = LocalResource.REPORT_CROSS_P_SHEET_NAME;
                    PerStartCell = PerSheet.Range["A1"];
                    tmpSheet = PerSheet;
                    if (PageSetupOn)
                    {
                        if (CurrentOutput.PageSetupPerTable)
                        {
                            PerRemainedRowsCount = MaxRowsCountPerPage;
                            if (tmpPageSetupSheet == null)
                            {
                                PageSetupPerSheet = PageSetupTemplateSheet;
                            }
                            else
                            {
                                PageSetupTemplateSheet.Copy(Type.Missing, tmpPageSetupSheet);
                                PageSetupPerSheet = tmpPageSetupSheet.Next;
                            }
                            PageSetupPerSheet.Name = PerSheet.Name + tmp;
                            PageSetupPerStartCell = PageSetupPerSheet.Range["A1"];
                        }
                    }
                }
                if (SigTestOn)
                {
                    SigTestSheet = SigTestTemplateSheet;
                    SigTestSheet.Name = LocalResource.REPORT_CROSS_SIGNIFICANCE_TEST_SHEET_NAME;
                    SigTestStartCell = SigTestSheet.Range["A1"];
                    if (CurrentOutput.PageSetupSignificanceTestTable)
                    {
                        SigTestRemainedRowsCount = MaxRowsCountPerPage;
                        PageSetupSigTestSheet = PageSetupSigTestTemplateSheet;
                        PageSetupSigTestSheet.Name = SigTestSheet.Name + tmp;
                        PageSetupSigTestStartCell = PageSetupSigTestSheet.Range["A1"];
                    }
                }
                if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                {
                    FormatSheet = FormatBook.Worksheets.Item["Standard"];
                }
                if (SigTestOn)
                {
                    SigTestFormatSheet = FormatBook.Worksheets.Item["SignificanceTest"];
                }
                AdjustFormat(FormatSheet, SigTestFormatSheet, MaxAxesCount, HasWeightColumn);
                _log.Info("AdjustFormat completed");

                HasShowPreWBTotal = CurrentOutput.ShowPreWBTotal;
                tmpOrientation = CurrentOutput.Orientation;
                tmpTablesCount = CurrentOutput.Tables.Count;

                n = (int)(Math.Log(CurrentOutput.Tables.Count) / Math.Log(10)) + 1;

                if (n < 3)
                {
                    n = 3;
                }
                fmt = new string('0', n);
                double progressStep = progressAvailable / CurrentOutput.Tables.Count;
                for (i = 0; i < CurrentOutput.Tables.Count; i++)
                {
                    this.ProgressBarMovement = currentProgress;
                    if (bgWorker.CancellationPending) return;
                    updateProgress(currentProgress, String.Format(LocalResource.PB_EXCEL_GEN_TABLE, (i + 1), CurrentOutput.Tables.Count));
                    currentProgress += progressStep;
                    strIdx = (i + 1).ToString(fmt);// string.Format(fmt, i + 1);// to do
                    tmpTable = (CrossTable)CurrentOutput.Tables[i];
                    _log.Info("Table *** : " + tmpTable.Question.Name + " - " + tmpTable.AxesGroups.ToString());
                    if (i < CurrentOutput.Tables.Count - 1)
                    {
                        tmpNextTable = (CrossTable)CurrentOutput.Tables[i + 1];
                    }
                    else
                    {
                        tmpNextTable = null;
                    }
                    ContentsValue.SetValue(tmpTable.Question.Name, i + 1, 1);
                    ContentsValue.SetValue(tmpTable.Question.Description, i + 1, 2);
                    if (PageSetupOn)
                    {
                        PageSetupContentsValue.SetValue(ContentsValue.GetValue(i + 1, 1), i + 1, 1);
                        PageSetupContentsValue.SetValue(ContentsValue.GetValue(i + 1, 2), i + 1, 2);
                    }
                    switch (tmpTable.Question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N))
                    {
                        case QuestionType.SA:
                        case QuestionType.MA:
                            FormatRangeNamePrefix = "SA_MA";
                            isN = false;
                            break;
                        case QuestionType.N:
                            FormatRangeNamePrefix = "N";
                            isN = true;
                            break;
                        default:
                            break;
                    }
                    HasWeight = GetHasWeight(tmpTable);
                    if (SigTestOn) WholeRowCol = new Hashtable();
                    int medIdx = -1;
                    int checkCrsLn = 0;
                    if (checkCross)
                    {
                        checkCrsLn = checkCrsLnLst[i];
                    }
                    GetCutRowsAndColumns(tmpTable, HasShowPreWBTotal, HasWeight, MaxAxesCount, ref CutRowsCol, ref CutColumnsCol, ref medIdx, false, CutMedian, WholeRowCol);
                    _log.Info("GetCutRowsAndColumns completed");
                    _log.Info("OutputTable NP started");
                    OutputTable(tmpTable, ref NPerStartCell, TableType.NPer, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                                  , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupNPerStartCell
                                  , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, FormatSheet, ref NPerSheet, ref PageSetupNPerSheet
                                    , ref ContentsValue, ref HyperlinkTargetCells, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                                  , strIdx, TemplateSheet, ContentsSheet, HasWeightColumn, PageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref NPerRemainedRowsCount
                                  , DefLineHeight, CutMedian: CutMedian, medIdx: medIdx, checkCrsLn: checkCrsLn
                                  //,Errors, ErrorsCount, res
                                  );
                    //        DoEvents
                    //        ' N•\
                    _log.Info("OutputTable NP completed");
                    _log.Info("OutputTable N started");
                    OutputTable(tmpTable, ref NStartCell, TableType.N, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                                      , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupNStartCell
                                      , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, FormatSheet, ref NSheet, ref PageSetupNSheet
                                        , ref ContentsValue, ref HyperlinkTargetCells, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                                      , strIdx, TemplateSheet, ContentsSheet, HasWeightColumn, PageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref NRemainedRowsCount
                                      , DefLineHeight, CutMedian: CutMedian, medIdx: medIdx
                                      //, Errors, ErrorsCount, res
                                      );
                    //        DoEvents
                    //        ' “•\
                    _log.Info("OutputTable N completed");
                    _log.Info("OutputTable P started");
                    OutputTable(tmpTable, ref PerStartCell, TableType.Per, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                              , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupPerStartCell
                              , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, FormatSheet, ref PerSheet, ref PageSetupPerSheet
                              , ref ContentsValue, ref HyperlinkTargetCells, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                              , strIdx, TemplateSheet, ContentsSheet, HasWeightColumn, PageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref PerRemainedRowsCount
                              , DefLineHeight, CutMedian: CutMedian, medIdx: medIdx
                                    // , Errors, ErrorsCount, res
                                    );
                    //        DoEvents
                    //        ' ŒŸ’è•\
                    _log.Info("OutputTable P completed");
                    _log.Info("OutputTable Test started");
                    OutputTable(tmpTable, ref SigTestStartCell, TableType.SignificanceTest, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                                      , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupSigTestStartCell
                                      , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, SigTestFormatSheet, ref SigTestSheet, ref PageSetupSigTestSheet
                                        , ref ContentsValue, ref HyperlinkTargetCells, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                                      , strIdx, SigTestTemplateSheet, ContentsSheet, HasWeightColumn, SigTestPageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref SigTestRemainedRowsCount
                                      , DefLineHeight
                                      //, Errors, ErrorsCount, res
                                      , WholeRowCol, CutMedian: CutMedian, medIdx: medIdx
                                      );
                    //        DoEvents
                    _log.Info("OutputTable Test completed");

                }
                PutContents(ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, xlApp);
                _log.Info("PutContents  completed");
                ////    if( Not PageSetupContentsSheet == null ){
                //        PutContents PageSetupContentsSheet, PageSetupContentsValue, PageSetupHyperlinkTargetCells
                //    }
                SaveBook(NewBook, CurrentOutput.ParentReportset.DivName + CurrentOutput.ExcelBookNamePrefix, xlApp, FormatBook, Suffix, FileFormat);
                _log.Info("SaveBook  completed");
                //    if( res<> RaisedError ){ res = Successful
                //    CreateStandardCross = res
                //    if( res = RaisedError ){ PutErrorsInformation Errors
                //    RunningProcName = OrgProcName
                //    Exit Function
                //ErrHdl:
                //if( IsDebug ){
                //    Debug.Print RunningProcName
                //    Debug.Print Err().Number, Err().Description
                //    Stop
                //    Resume
                //}
                //    if( ResumeError ){
                //        res = RaisedError
                //        PushError Err(), Errors, ErrorsCount
                //        Resume Next
                //    }else{
                //        With Err()
                //            .Raise.Number, .Source, .Description, .HelpFile, .HelpContext
                //        End With
                //    }
                //End Function
            }
            catch (Exception ex)
            {
                try
                {
                    FormatBook.Close(NewBook);
                }
                catch (Exception ex2) { }
                _log.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
                COMWholeOperate.releaseComObject(ref newBookWShhets);
                COMWholeOperate.releaseComObject(ref NewBook);
                GC.Collect();
            }
        }

        private void updateProgress(double currentProgress, string v)
        {
            if (null != QC)
            {
                QC.updateProgress(currentProgress, v);
            }
            else if (null != CQC)
            {
                CQC.updateProgress(currentProgress, v);
            }
        }

        private void OutputTable(CrossTable Table, ref Range StartCell
              , TableType TableType, bool isN, string FormatRangeNamePrefix
              , bool HasWeight, TableOrientation Orientation, bool PageSetupOn
              , Hashtable CutRowsCol, Hashtable CutColumnsCol, int AxesCount, int MaxAxesCount
              , ref Range PageSetupStartCell, bool IsFirstTable, bool NextIsLast
            , CrossTable NextTable, Worksheet FormatSheet
            , ref Worksheet Sheet, ref Worksheet PageSetupSheet, ref Array ContentsValue, ref Array HyperlinkTargetCells
            , ref Array PageSetupContentsValue, ref Array PageSetupHyperlinkTargetCells, string TableIndex
            , Worksheet TemplateSheet, Worksheet ContentsSheet, bool HasWeightColumn
            , int PageSetupColumnsCountPerPage, int MaxRowsCountPerPage, ref int RemainedRowsCount, double DefLineHeight
            //, ByRef Errors() As ErrorStruct, ByRef ErrorsCount As Long, ByRef res As MethodResult _
            , Hashtable WholeRowCol = null, bool CutMedian = false, int medIdx = 0, int checkCrsLn = 0)
        {
            string tmp = null;
            Array TableValue = null; // objec
            string[,] TableStringValue = null;// string
            object[] DataValue;// object
            Array Ranking = null; // int
            Array HatchingColorIndex = null; //XlColorIndex
            Array ArrowEnd = null;// object
            Array SigTestMarking = null; // string
            int PagesCount = 0;
            int PageRowsCount = 0;
            Array h;
            double maxH;
            int idx = 0;
            bool f;
            int i; ;
            bool CheckOverRow;
            bool CheckOverColumn = false;
            bool IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            if (StartCell == null) return;

            f = true;
            if (Orientation == TableOrientation.Landscape)
            {
                switch (TableType)
                {
                    case TableType.NPer:
                        if (CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable || CurrentOutput.SignificanceTest)
                        {
                            f = false;
                        }
                        break;
                    case TableType.SignificanceTest:
                        if (CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                        {
                            f = false;
                        }
                        break;
                }
            }
            else
            {
                switch (TableType)
                {
                    case TableType.SignificanceTest:
                        if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                            f = false;
                        break;
                    case TableType.NPer:
                        if (CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                            f = false;
                        break;
                }
            }
            if (!isN)
            {
                switch (TableType)
                {
                    case TableType.NPer:
                        tmp = "_NP";
                        break;
                    case TableType.N:
                        tmp = "_N";
                        break;
                    case TableType.Per:
                    case TableType.SignificanceTest:
                        tmp = "_P";
                        break;
                }
                if (TableType == TableType.SignificanceTest && NPOICrossCreator.checkSimpleAggr(Table))
                {
                    tmp = "_NP";
                }
                FormatRangeNamePrefix = FormatRangeNamePrefix + tmp;
                if (HasWeight)
                {
                    FormatRangeNamePrefix = FormatRangeNamePrefix + "_WT";
                }
            }
            switch (TableType)
            {
                case TableType.NPer:
                    tmp = "";
                    idx = 4; break;
                case TableType.N:
                    tmp = "N";
                    idx = 5; break;
                case TableType.Per:
                    tmp = "P";
                    idx = 6; break;
                case TableType.SignificanceTest:
                    tmp = "T";
                    idx = 0; break;
            }
            TableIndex = "[" + tmp + "TABLE" + TableIndex + "]";
            if (Orientation == TableOrientation.Landscape)
            {

                CreateTurnedLandscapeCrossArray(Table, CutRowsCol, CutColumnsCol, ref TableValue, ref Ranking, ref HatchingColorIndex,
                    ref ArrowEnd, ref SigTestMarking
                                                  , 2 // wt
                                                  , 1 + AxesCount, HasWeight, isN
                                                   , TableType, StartCell.Worksheet.Rows.Count - StartCell.Row - 1
                                                  , StartCell.Worksheet.Columns.Count - 2, MaxAxesCount - AxesCount
                                                  , ref PagesCount, ref PageRowsCount, WholeRowCol);
                _log.Info("CreateTurnedLandscapeCrossArray completed");

                CheckOverRow = TableValue.Length == 0;
            }
            else
            {
                CheckOverRow = true;
                CheckOverColumn = true;
                //CreatePortraitCrossArray(Table, CutRowsCol, CutColumnsCol, TableStringValue, DataValue, Ranking, HatchingColorIndex, ArrowEnd, SigTestMarking 
                //                       , 1  +(ToInt(HasWeight) & 1 ), 1 & +AxesCount, HasWeightColumn, HasWeight, isN 
                //                , TableType, StartCell.Worksheet.Rows.Count - StartCell.Row - 1  
                //                       , StartCell.Worksheet.Columns.Count - 1 , CheckOverRow, CheckOverColumn);
            }
            if (CheckOverRow || CheckOverColumn)
            {
                StartCell = null;
                PageSetupStartCell = null;
                if (IsFirstTable)
                {
                    //Me.Application.DisplayAlerts = false;
                    Sheet.Delete();
                    Sheet = null;
                    if (PageSetupSheet != null)
                    {
                        PageSetupSheet.Delete();
                        PageSetupSheet = null;
                    }
                    if (f)
                    {
                        if (CheckOverRow)
                        {
                        }
                        else
                        {
                        }
                    }
                }
                switch (TableType)
                {
                    case TableType.NPer:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                            }
                            else
                            {
                            }
                        }
                        break;
                    case TableType.N:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                            }
                            else
                            {
                            }
                        }
                        break;
                    case TableType.Per:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                            }
                            else
                            {
                            }
                        }
                        break;
                    case TableType.SignificanceTest:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                            }
                            else
                            {
                            }
                        }
                        break;
                }
                if (f)
                {
                    ContentsValue.SetValue(String.Empty, Table.Index + 1, 1);
                    ContentsValue.SetValue(String.Empty, Table.Index + 1, 2);
                    if (PageSetupOn)
                    {
                        PageSetupContentsValue.SetValue(String.Empty, Table.Index + 1, 1);
                        PageSetupContentsValue.SetValue(String.Empty, Table.Index + 1, 2);
                    }
                }
            }


            else
            {    //' 出せた場合
                if (Orientation == TableOrientation.Landscape)
                {
                    //FormatTurnedLandscapeTable(Table, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix
                    //                                    , TableType, HasWeight, StartCell.Worksheet.Columns.Count - 2
                    //                                     , PagesCount, StartCell, isN, AxesCount, WholeRowCol);

                    FormatLandscapeTable(Table, StartCell.Worksheet, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType
                                        , HasWeight, StartCell, isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: TableValue, CutMedian: CutMedian, MedIdx: medIdx);
                    _log.Info("FormatTurnedLandscapeTable completed");
                    if (checkCross)
                    {
                        StartCell.Range["A5"].Value = checkCrsLn;
                    }
                    OutputUtil.PutValue(StartCell.Range["B2"], ref TableIndex);
                    StartCell.Range["B2"].NumberFormat = "@";
                    Range resizRnge = StartCell.Range["B3"].Resize[TableValue.GetUpperBound(0), TableValue.GetUpperBound(1)];
                    StartCell.Range["B3"].NumberFormat = "@";
                    Range resizRngeHyper = StartCell.Range["B5"].Resize[TableValue.GetUpperBound(0) - 2, TableValue.GetUpperBound(1)];
                    
                    Range dataRange = resizRnge.Worksheet.Range[resizRnge.Item[Ranking.GetLowerBound(0),
                        Ranking.GetLowerBound(1)], resizRnge.Item[resizRnge.Rows.Count, resizRnge.Columns.Count]];

                    OutputUtil.ConvertAndPutValue(resizRnge, TableValue, dataRange, Ranking);

                    Range labelRange = resizRnge.Worksheet.Range[
                        resizRnge.Item[Ranking.GetLowerBound(0) + (TableType == TableType.NPer && !isN ? 2 : 1), 1],
                        resizRnge.Item[resizRnge.Rows.Count, Ranking.GetLowerBound(1) - (TableType == TableType.SignificanceTest ? 2 : 1)]
                        ];

                    _log.Info("Auto fit started");
                    if (!IsSigTest)
                    {
                        AutoFit(dataRange, colWidthMap);
                    }
                    _log.Info("Auto fit started complted");
                    if (!isN)
                    {
                        if (CurrentOutput.MarkingRanking && TableType != TableType.SignificanceTest)
                        {
                            RankMarking(resizRnge.Cells, ref Ranking);
                            _log.Info("Rank Marking completed");
                        }
                        if (CurrentOutput.MarkingColoring && TableType != TableType.SignificanceTest)
                        {
                            Hatching(resizRnge.Cells, ref HatchingColorIndex);
                            _log.Info("Color Marking completed");
                        }
                        if (CurrentOutput.MarkingAscending && TableType != TableType.SignificanceTest)
                        {
                            //AscendingMarking(resizRnge.Cells, ref ArrowEnd);
                        }
                        if (CurrentOutput.MarkingSignificance && TableType != TableType.N)
                        {
                            SignificanceTestMarking(resizRnge.Cells, ref SigTestMarking);
                            _log.Info("SignificanceTest Marking completed");
                        }
                    }
                    _log.Info("Marking completed");
                    //       ' オートフィット
                    //ReDim h(1 To PagesCount)
                    //h = Array.CreateInstance(typeof(double), new int[] { PagesCount }, new int[] { 1 });// new double[PagesCount];
                    //maxH = 0;
                    //for (i = 1; i <= PagesCount; i++)
                    //{
                    _log.Info("AutoFitEx start");
                    OutputUtil.AutoFitEx(resizRnge.Rows.Item[1], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                    OutputUtil.AutoFitEx(resizRnge.Rows.Item[3], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                    OutputUtil.AutoFitExCrossLabel(labelRange, xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                    _log.Info("AutoFitEx completed");
                    //h.SetValue(itemWith.RowHeight, i);
                    //if ((double)h.GetValue(i) > maxH)
                    //{
                    //    maxH = (double)h.GetValue(i);
                    //}
                    //resizRnge.Rows.Item[2 + (i - 1) * (PageRowsCount + 2) + 1].RowHeight = maxH;
                    //}
                    //for (i = 1; i <= PagesCount; i++)
                    //{
                    //    if ((double)h.GetValue(i) < maxH)
                    //    {
                    //    }
                    //}
                    if (idx > 0)
                    {
                        ContentsValue.SetValue(TableIndex, Table.Index + 1, idx);
                        HyperlinkTargetCells.SetValue(resizRngeHyper, Table.Index + 1, idx);
                    }
                    if (resizRnge.Row + resizRnge.Rows.Count < resizRnge.Worksheet.Rows.Count)
                    {
                        StartCell = resizRnge.Item[resizRnge.Rows.Count + 1, 1].EntireRow.Range["A1"];
                    }
                    else
                    {
                        StartCell = null;
                        if (NextTable != null)
                        {
                            switch (TableType)
                            {
                                case TableType.NPer:
                                    if (NextIsLast)
                                    {
                                    }
                                    else
                                    {
                                    }
                                    break;
                                case TableType.N:
                                    if (NextIsLast)
                                    {
                                    }
                                    else
                                    {
                                    }
                                    break;
                                case TableType.Per:
                                    if (NextIsLast)
                                    {
                                    }
                                    else
                                    {
                                    }
                                    break;
                                case TableType.SignificanceTest:
                                    if (NextIsLast)
                                    {
                                    }
                                    else
                                    {
                                    }
                                    break;
                            }
                        }
                    }
                    if (PageSetupStartCell != null)
                    {
                        //PageSetupLandscapeTable(Table, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType, HasWeight
                        //                      , PageSetupColumnsCountPerPage - 1, ref PageSetupStartCell, ref PageSetupSheet, isN
                        //                       , AxesCount, MaxAxesCount, ref RemainedRowsCount
                        //                      , MaxRowsCountPerPage, DefLineHeight, maxH, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                        //                      , IsFirstTable, NextTable, TableIndex,
                        //                      //Errors, ErrorsCount, res, 
                        //                      WholeRowCol);
                    }
                }
                else
                {
                    // FormatPortraitTable Table, TemplateSheet, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix _
                    //                   , TableType, HasWeight, StartCell, isN, ContentsSheet, MaxAxesCount - AxesCount
                    // PutValue StartCell.Range["B2"), TableIndex
                    Range resizRnge = StartCell.Range["B3"].Resize[TableStringValue.GetUpperBound(0), TableStringValue.GetUpperBound(1)];
                    // PutValue.Cells, TableStringValue
                    // PutValue.Worksheet.Range[.Item(LBound(DataValue, 1), LBound(DataValue, 2)), .Item(.Rows.Count, .Columns.Count)), DataValue
                    if (HasWeight)
                    {

                        //Range itemWith = resizRnge.Item[DataValue.GetLowerBound(0) + (CurrentOutput.ShowPreWBTotal & 1) + 1, DataValue.GetLowerBound(1) - 1 &].Resize[Table.SectorsCount * (1 + (!isN && 1))];
                        //                   itemWith.Value = itemWith.Value;
                    }
                    if (!isN)
                    {
                        if (CurrentOutput.MarkingRanking)
                        {
                            //RankMarking resizRnge .Cells, Ranking
                        }
                        if (CurrentOutput.MarkingColoring)
                        {
                            // Hatching .Cells, HatchingColorIndex
                        }
                        if (CurrentOutput.MarkingAscending)
                        {
                            //AscendingMarking .Cells, ArrowEnd
                        }
                        //  ' if (CurrentOutput.MarkingSignificance { SignificanceTestMarking .Cells, SigTestMarking
                    }
                    if (CurrentOutput.MarkingSignificance)
                    {
                        //  SignificanceTestMarking .Cells, SigTestMarking
                    }
                    //   ' AutoFitEx .Rows.Item(3&).Resize(1& + AxesCount)
                    if (AxesCount == 1)
                    {
                        // AutoFitEx .Rows.Item[4];
                    }

                    else
                    {
                        resizRnge.Rows.Item[4].RowHeight = resizRnge.Rows.Item[4].RowHeight * 2;
                        // AutoFitEx.Rows.Item[5]
                    }
                    if (idx > 0)
                    {
                        ContentsValue.SetValue(TableIndex, Table.Index + 1, idx);
                        HyperlinkTargetCells.SetValue(StartCell, Table.Index + 1, idx);
                    }
                    if (resizRnge.Row + resizRnge.Rows.Count < resizRnge.Worksheet.Rows.Count)
                    {
                        StartCell = resizRnge.Item[resizRnge.Rows.Count + 1, 1].EntireRow.Range("A1");
                    }
                    else
                    {
                        StartCell = null;
                        if (NextTable != null)
                        {
                            switch (TableType)
                            {
                                case TableType.NPer:
                                    if (NextIsLast)
                                    {
                                    }
                                    else
                                    {
                                    }
                                    break;
                                case TableType.N:
                                    if (NextIsLast)
                                    {
                                    }
                                    else
                                    {
                                    }
                                    break;
                                case TableType.Per:
                                    if (NextIsLast)
                                    {
                                    }
                                    else
                                    {
                                    }
                                    break;
                                case TableType.SignificanceTest:
                                    if (NextIsLast)
                                    {
                                    }
                                    else
                                    {
                                    }
                                    break;
                            }
                        }
                    }

                }
            }
            //ExitProc:
            //    RunningProcName = OrgProcName
            //    Exit Sub
            //ErrHdl:
            //if (IsDebug {
            //    Debug.Print RunningProcName
            //    Debug.Print Err().Number, Err().Description
            //    Stop
            //    Resume
            //}
            //    if (ResumeError {
            //        res = RaisedError
            //        PushError Err(), Errors, ErrorsCount
            //        Resume }
            //    }else{
            //        With Err()
            //            .Raise.Number, .Source, .Description, .HelpFile, .HelpContext
            //       End With
            //    }
        }

        private void CreateTurnedLandscapeCrossArray(CrossTable Table
            , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , ref Array TableValue
              , ref Array Ranking, ref Array HatchingColorIndex, ref Array ArrowEnd, ref Array SigTestMarking
              , int DataOffsetRow, int DataOffsetColumn
              , bool HasWeight, bool isN
              , TableType TableType, int MaxRowsCount, int ColumnsCountPerPage
              , int AddColumnsCount, ref int PagesCount, ref int PageRowsCount
              , Hashtable WholeRowCol = null)
        {
            int CaptionRowsCount = 0;
            int SectorsCountPerPage;
            int RowsCount = 0;
            int ColumnsCount = 0;
            int TotalRowsCount;
            int d = 0, d2 = 0;
            int PreWBColumnIndex;
            int x, y;
            int r, c;
            int i;
            bool isNew;
            Array tmp;
            string tmpBuf;
            object buf;
            int? clr = 0;
            bool f;
            bool IsSigTest;
            int lc = 0;
            TableType tType;

            int u;
            int[] tmpArrowEnd = new int[2];
            DataMarking reverseSide;

            int tmpY = 0;
            int tmpR = 0;
            bool IsShowPreWBTotal;
            bool IsMarkingSignificance;
            bool IsMarkingRanking;
            bool IsMarkingColoring;
            bool IsMarkingcending;
            bool IsMarkingColoringLevel2High;
            bool IsMarkingColoringLevel1High;
            bool IsMarkingColoringLevel2Low;
            bool IsMarkingColoringLevel1Low;
            int tmpLevel2HighColorIndex;
            int tmpLevel1HighColorIndex;
            int tmpLevel2LowColorIndex;
            int tmpLevel1LowColorIndex;
            int tmpRowIndexFrom;
            int tmpRowIndexTo;
            int tmpColumnIndexFrom;
            int tmpColumnIndexTo;
            object[,] tmpTableValue;
            double[,] tmpPercentValue;
            object[,] tmpSignificanceTestCharacters;
            object[,] tmpSignificanceMark;
            int[,] tmpRank;
            bool[,] tmpColoringLevel2High;
            bool[,] tmpColoringLevel1High;
            bool[,] tmpColoringLevel2Low;
            bool[,] tmpColoringLevel1Low;

            IsShowPreWBTotal = CurrentOutput.ShowPreWBTotal;
            IsMarkingSignificance = CurrentOutput.MarkingSignificance;
            IsMarkingRanking = CurrentOutput.MarkingRanking;
            IsMarkingColoring = CurrentOutput.MarkingColoring;
            IsMarkingcending = CurrentOutput.MarkingAscending;
            IsMarkingColoringLevel2High = CurrentOutput.MarkingColoringLevel2High;
            IsMarkingColoringLevel1High = CurrentOutput.MarkingColoringLevel1High;
            IsMarkingColoringLevel2Low = CurrentOutput.MarkingColoringLevel2Low;
            IsMarkingColoringLevel1Low = CurrentOutput.MarkingColoringLevel1Low;
            tmpLevel2HighColorIndex = CurrentOutput.Level2HighColorIndex;
            tmpLevel1HighColorIndex = CurrentOutput.Level1HighColorIndex;
            tmpLevel2LowColorIndex = CurrentOutput.Level2LowColorIndex;
            tmpLevel1LowColorIndex = CurrentOutput.Level1LowColorIndex;

            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            //tType = TableType And Not TableType.SignificanceTest
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest)
            {
                tType = TableType.Per;
            }
            GetRequiredRowsColsCountLandscape(Table, CutRowsCol, CutColumnsCol, DataOffsetRow, AddColumnsCount, TableType
                                             , isN, ref d, ref CaptionRowsCount, ref RowsCount, ref ColumnsCount, ref d2, WholeRowCol);
            SectorsCountPerPage = ColumnsCountPerPage - DataOffsetColumn - AddColumnsCount - (ToInt(IsSigTest) & 1);

            PagesCount = (ColumnsCount - DataOffsetColumn - AddColumnsCount - (ToInt(IsSigTest) & 1) - 1 - (ToInt(IsShowPreWBTotal) & 1) - 1) / (SectorsCountPerPage - 1 - (ToInt(IsShowPreWBTotal) & 1)) + 1;
            PageRowsCount = RowsCount - CaptionRowsCount;
            TotalRowsCount = RowsCount + (PagesCount - 1) * (PageRowsCount + 2);
            if (TotalRowsCount > MaxRowsCount)
            {
                TableValue = new object[0, 0];
                return;
            }
            if (IsSigTest)
            {
                if (isN)
                {
                    if (PagesCount > 1)
                    {
                        lc = (ColumnsCount - 1) % ColumnsCountPerPage + 1;
                    }
                    else
                    {
                        lc = ColumnsCount;
                    }
                }
            }
            if (PagesCount > 1)
            {
                ColumnsCount = ColumnsCountPerPage;
            }
            TableValue = Array.CreateInstance(typeof(object), new int[] { TotalRowsCount, ColumnsCount }, new int[] { 1, 1 });

            // ReDim Ranking(CaptionRowsCount +DataOffsetRow + 1 & To TotalRowsCount, 
            //DataOffsetColumn + AddColumnsCount + (IsSigTest And 1 &) +1 & To ColumnsCount)
            Ranking = Array.CreateInstance(typeof(int),
                new int[] { TotalRowsCount - (CaptionRowsCount + DataOffsetRow + 1) + 1,
                ColumnsCount - (DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1) + 1) + 1 },
                new int[] { CaptionRowsCount + DataOffsetRow + 1, DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1) + 1 });

            // HatchingColorIndex(LBound(Ranking, 1) To TotalRowsCount, LBound(Ranking, 2) To ColumnsCount)
            HatchingColorIndex = Array.CreateInstance(typeof(int?),
                new int[] { (int)TotalRowsCount - Ranking.GetLowerBound(0) + 1, (int)ColumnsCount - Ranking.GetLowerBound(1) + 1 },
                new int[] { Ranking.GetLowerBound(0), Ranking.GetLowerBound(1) });

            //ReDim ArrowEnd(LBound(Ranking, 1) To TotalRowsCount, LBound(Ranking, 2) To ColumnsCount)
            ArrowEnd = Array.CreateInstance(typeof(object),
                new int[] { (int)TotalRowsCount - Ranking.GetLowerBound(0) + 1, (int)ColumnsCount - Ranking.GetLowerBound(1) + 1 },
                new int[] { Ranking.GetLowerBound(0), Ranking.GetLowerBound(1) });

            //ReDim SigTestMarking(LBound(Ranking, 1) To TotalRowsCount, LBound(Ranking, 2) To ColumnsCount)
            SigTestMarking = Array.CreateInstance(typeof(string),
                new int[] { (int)TotalRowsCount - Ranking.GetLowerBound(0) + 1, (int)ColumnsCount - Ranking.GetLowerBound(1) + 1 },
                new int[] { Ranking.GetLowerBound(0), Ranking.GetLowerBound(1) });
            if (String.IsNullOrEmpty(Table.Question.TableHeading))
            {
                tmpBuf = Table.Question.Name + " " + Table.Question.Description;
            }
            else
            {
                tmpBuf = Table.Question.Name + " " + Table.Question.TableHeading + "\n[" + Table.Question.Description + "]";
            }
            OutputUtil.AddPrefix(ref tmpBuf, true);
            TableValue.SetValue(tmpBuf, 1, 1);
            PreWBColumnIndex = Table.GetTableValueColumnIndexMinimum + DataOffsetColumn;
            TableValue.SetValue(Table.Question.NarrowingCondition, CaptionRowsCount + 1, PreWBColumnIndex);
            // ReDim tmp(Table.GetTableValueColumnIndexMinimum To PreWBColumnIndex -1 &)
            tmp = Array.CreateInstance(typeof(string),
                new int[] { (int)(PreWBColumnIndex - 1) - Table.GetTableValueColumnIndexMinimum + 1 },
                new int[] { Table.GetTableValueColumnIndexMinimum });

            r = CaptionRowsCount + DataOffsetRow;
            for (y = Table.GetTableValueRowIndexMinimum + DataOffsetRow; y <= Table.GetTableValueRowIndexMaximum; y++)
            {
                if (CutRowsCol.ContainsKey(y))
                {
                    if (tmp.GetValue(tmp.GetLowerBound(0)) == null)
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            tmp.SetValue((Table.TableValue(y, x, true)), x);
                            string tmpArrStr = (string)tmp.GetValue(x);
                            OutputUtil.AddPrefix(ref tmpArrStr, true);
                            tmp.SetValue(tmpArrStr, x);
                        }
                    }
                }
                else
                {
                    r = r + 1;
                    c = 0;
                    if (tmp.GetValue(tmp.GetLowerBound(0)) == null)
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)

                        {
                            c = c + 1;
                            tmpBuf = Table.TableValue(y, x, true);
                            OutputUtil.AddPrefix(ref tmpBuf, true);
                            TableValue.SetValue(tmpBuf, r, c);
                        }
                    }
                    else
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            c = c + 1;
                            tmpBuf = Table.TableValue(y, x, true);
                            OutputUtil.AddPrefix(ref tmpBuf, true);
                            if (null != tmpBuf && tmpBuf.Length > 0)
                            {
                                tmp.SetValue(tmpBuf, x);
                            }
                            TableValue.SetValue(tmp.GetValue(x), r, c);
                        }
                    }
                    if (IsSigTest)
                    {
                        x = tmp.GetUpperBound(0);
                        tmpBuf = Table.SignificanceTestCharacters(y, x);
                        if (null == tmpBuf || tmpBuf.Length == 0)
                        {
                            x = x - 1;
                            tmpBuf = Table.SignificanceTestCharacters(y, x);
                        }
                        OutputUtil.AddPrefix(ref tmpBuf, true);
                        if (null != tmpBuf && tmpBuf.Length > 0)
                        {
                            c = c + AddColumnsCount + 1;
                            TableValue.SetValue(tmpBuf, r, c);
                        }
                    }
                    for (i = 2; i <= PagesCount; i++)
                    {
                        c = 0;
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            c = c + 1;
                            TableValue.SetValue(TableValue.GetValue(r, c), r + (PageRowsCount + 2) * (i - 1), c);
                        }
                    }
                    tmp = Array.CreateInstance(typeof(string),
                        new int[] { tmp.GetUpperBound(0) - tmp.GetLowerBound(0) + 1 },
                        new int[] { tmp.GetLowerBound(0) });
                    if (IsSigTest)
                    {
                        r = r + (WholeRowCol.ContainsKey(y) ? d : d2) - 1;
                    }
                    else
                    {
                        r = r + d - 1;
                    }
                }
            }
            if (IsSigTest)
            {
                if (isN)
                {
                    r = CaptionRowsCount + (PagesCount - 1) * (PageRowsCount + 2) + 1;
                    TableValue.SetValue(LocalResource.REPORT_SIGNIFICANCE_TEST_ROW_COLUMN_CAPTION, r, TableValue.GetUpperBound(1));
                }
            }
            i = 0;
            c = DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1);
            u = Table.GetTableValueRowIndexMinimum + DataOffsetRow - 1;
            for (x = PreWBColumnIndex; x <= Table.GetTableValueColumnIndexMaximum; x++)
            {
                if (!CutColumnsCol.ContainsKey(x))
                {
                    c = c + 1;
                    isNew = c - DataOffsetColumn - AddColumnsCount - (ToInt(IsSigTest) & 1) > SectorsCountPerPage;
                    if (isNew)
                    {
                        c = DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1) + 1;
                        i = i + 1;
                    }
                    r = CaptionRowsCount + i * (PageRowsCount + 2);
                    if (isNew)
                    {
                        TableValue.SetValue(LocalResource.REPORT_TO_AFTER_TABLE_MARK_AT_TURN_KEYWORD, r - 1, ColumnsCountPerPage);
                        TableValue.SetValue(LocalResource.REPORT_FROM_BEFORE_TABLE_MARK_AT_TURN_KEYWORD, r, c);
                        if (IsShowPreWBTotal)
                        {
                            TableValue.SetValue(TableValue.GetValue(CaptionRowsCount + 1, c), r + 1, c);
                            c = c + 1;
                        }
                        TableValue.SetValue(TableValue.GetValue(CaptionRowsCount + 1, c), r + 1, c);
                        c = c + 1;
                    }
                    for (y = Table.GetTableValueRowIndexMinimum; y <= u; y++)
                    {
                        tmpBuf = Table.TableValue(y, x, true);
                        r = r + 1;
                        if (!HasWeight || y != u || !OutputUtil.IsNumeric(tmpBuf))
                        {
                            OutputUtil.AddPrefix(ref tmpBuf, true);
                            TableValue.SetValue(tmpBuf, r, c);
                        }
                        else
                        {
                            if (y == (Table.GetTableValueRowIndexMinimum + 1) && Table.Question.HasCount) { continue; }
                            TableValue.SetValue(Convert.ToDouble(tmpBuf), r, c);
                        }
                    }
                }
            }
            // ' 性能対策 start
            tmpRowIndexFrom = Table.GetTableValueRowIndexMinimum + DataOffsetRow;
            tmpRowIndexTo = Table.GetTableValueRowIndexMaximum;
            tmpColumnIndexFrom = PreWBColumnIndex;
            tmpColumnIndexTo = Table.GetTableValueColumnIndexMaximum;
            tmpTableValue = Table.TableValueByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpPercentValue = Table.PercentValueByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpSignificanceTestCharacters =
                Table.SignificanceTestCharactersByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpSignificanceMark = Table.SignificanceMarkByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpRank = Table.RankByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel2High = Table.ColoringLevel2HighByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel1High = Table.ColoringLevel1HighByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel2Low = Table.ColoringLevel2LowByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel1Low = Table.ColoringLevel1LowByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            //    ' 性能対策 end
            //    ' データ
            i = 0;
            c = DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1);
            for (x = PreWBColumnIndex; x <= Table.GetTableValueColumnIndexMaximum; x++)
            {
                if (!(CutColumnsCol.ContainsKey(x)))
                {
                    c = c + 1;
                    isNew = c - DataOffsetColumn - AddColumnsCount - (ToInt(IsSigTest) & 1) > SectorsCountPerPage;
                    if (isNew)
                    {
                        c = DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1) + (ToInt(IsShowPreWBTotal) & 1) + 1 + 1;
                        i = i + 1;
                    }
                    r = CaptionRowsCount + DataOffsetRow + i * (PageRowsCount + 2);
                    for (y = Table.GetTableValueRowIndexMinimum + DataOffsetRow;
                         y <= Table.GetTableValueRowIndexMaximum; y++)
                    {
                        if (!(CutRowsCol.ContainsKey(y)))
                        {
                            r = r + 1;
                            f = false;
                            if (HasWeight)
                            {
                                switch (Table.GetTableValueColumnIndexMaximum - x)
                                {
                                    case 0: //' 加重平均
                                        buf = tmpTableValue[y, x];
                                        if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                        TableValue.SetValue(buf, r + d - 1, c);
                                        if (IsSigTest)
                                        {
                                            if (!(WholeRowCol.ContainsKey(y)))
                                            {
                                                buf = tmpSignificanceTestCharacters[y, x];
                                                if (null != buf && ((string)buf).Length > 0)
                                                {
                                                    TableValue.SetValue(buf, r + d2 - 1, c);
                                                }
                                            }
                                        }
                                        else if (IsMarkingSignificance)
                                        {
                                            //SigTestMarking.SetValue(tmpSignificanceMark[y, x], r + d - 1, c);
                                        }
                                        f = true; break;
                                    case 1:// & ' 加重平均母数
                                        buf = tmpTableValue[y, x];
                                        if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                        TableValue.SetValue(buf, r, c);
                                        f = true; break;
                                }
                            }
                            if (!f)
                            {
                                if (isN || x - PreWBColumnIndex <= (ToInt(IsShowPreWBTotal) & 1))
                                {  // ' WB前全体/全体
                                    buf = tmpTableValue[y, x];
                                    if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                    TableValue.SetValue(buf, r, c);
                                    if (isN)
                                    {
                                        if (IsSigTest)
                                        {
                                            if (!(WholeRowCol.ContainsKey(y)))
                                            {
                                                buf = tmpSignificanceTestCharacters[y, x];
                                                if (null != buf && ((string)buf).Length > 0)
                                                {
                                                    TableValue.SetValue(buf, r + (PagesCount - i - 1) * (PageRowsCount + 2), lc);
                                                }
                                            }
                                        }
                                        else if (IsMarkingSignificance)
                                        {
                                            SigTestMarking.SetValue(tmpSignificanceMark[y, x], r, c);
                                        }
                                    }
                                }
                                else
                                {
                                    if (tType == TableType.Per)
                                    {
                                        TableValue.SetValue(tmpPercentValue[y, x], r, c);
                                    }
                                    else
                                    {
                                        buf = tmpTableValue[y, x];
                                        if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                        TableValue.SetValue(buf, r, c);
                                        if (tType == TableType.NPer)
                                        {
                                            TableValue.SetValue(tmpPercentValue[y, x], r + 1, c);
                                        }
                                    }
                                    if (IsSigTest)
                                    {
                                        if (!(WholeRowCol.ContainsKey(y)))
                                        {
                                            buf = tmpSignificanceTestCharacters[y, x];
                                            if (null != buf && ((string)buf).Length > 0) { TableValue.SetValue(buf, r + d2 - 1, c); }
                                        }
                                    }
                                    //                                ' ランキング
                                    if (IsMarkingRanking) { Ranking.SetValue(tmpRank[y, x], r, c); }
                                    //                                ' ハッチング
                                    if (IsMarkingColoring)
                                    {
                                        clr = -1;

                                        if (IsMarkingColoringLevel2High)
                                        {
                                            if ((bool)tmpColoringLevel2High[y, x]) { clr = tmpLevel2HighColorIndex; }
                                        }
                                        if (clr == -1)
                                        {
                                            if (IsMarkingColoringLevel1High)
                                            {
                                                if ((bool)tmpColoringLevel1High[y, x]) { clr = tmpLevel1HighColorIndex; }
                                            }
                                        }
                                        if (clr == -1)
                                        {
                                            if (IsMarkingColoringLevel2Low)
                                            {
                                                if ((bool)tmpColoringLevel2Low[y, x]) { clr = tmpLevel2LowColorIndex; }
                                            }
                                        }
                                        if (clr == -1)
                                        {
                                            if (IsMarkingColoringLevel1Low)
                                            {
                                                if ((bool)tmpColoringLevel1Low[y, x]) { clr = tmpLevel1LowColorIndex; }
                                            }
                                        }

                                        if (clr < 0)
                                            clr = null;

                                        HatchingColorIndex.SetValue(clr, r, c);
                                        if (tType == TableType.NPer) { HatchingColorIndex.SetValue(clr, r + 1, c); }
                                        if (IsSigTest)
                                        {
                                            if (!WholeRowCol.ContainsKey(y))
                                            {
                                                HatchingColorIndex.SetValue(clr, r + d2 - 1, c);
                                            }
                                        }
                                    }
                                    if (IsMarkingcending)
                                    {
                                        if (!(ArrowEnd.GetValue(r, c).GetType().IsArray))
                                        {
                                            if (Table.IsArrowEnd(y, x, out reverseSide))
                                            {
                                                if (IsSigTest)
                                                {
                                                    tmpR = r + (WholeRowCol.ContainsKey(y) ? d : d2) - 1;//' 全体行のことはないけど、形をそろえる
                                                }
                                                else
                                                {
                                                    tmpR = r + d - 1;
                                                }
                                                for (tmpY = y + 1; y <= Table.GetTableValueRowIndexMaximum; y++)
                                                {
                                                    if (!(CutRowsCol.ContainsKey(tmpY)))
                                                    {
                                                        if (IsSigTest)
                                                        {
                                                            tmpR = tmpR + (WholeRowCol.ContainsKey(tmpY) ? d : d2);
                                                        }
                                                        else
                                                        {
                                                            tmpR = tmpR + d;
                                                        }
                                                        if (!Table.IsArrowShaft(tmpY, x))
                                                        {
                                                            // 最小ベースで出さないものは、ここではじく
                                                            if (Table.AscendingMarking(tmpY, x) == reverseSide)
                                                            {
                                                                tmpArrowEnd[1] = c;
                                                                if (reverseSide == DataMarking.AscendingStart)
                                                                {
                                                                    tmpArrowEnd[0] = r;
                                                                    ArrowEnd.SetValue(tmpArrowEnd, tmpR, c);
                                                                }
                                                                else if (reverseSide == DataMarking.AscendingEnd)
                                                                {
                                                                    tmpArrowEnd[0] = tmpR;
                                                                    ArrowEnd.SetValue(tmpArrowEnd, r, c);
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //                                ' 全体との差の検定
                                    if (IsMarkingSignificance) { SigTestMarking.SetValue(tmpSignificanceMark[y, x], r + (ToInt(tType == TableType.NPer) & 1), c); }
                                }
                            }
                            if (IsSigTest)
                            {
                                r = r + (WholeRowCol.ContainsKey(y) ? d : d2) - 1;
                            }
                            else
                            {
                                r = r + d - 1;
                            }
                        }
                    }
                }
            }
            //    ' WB前全体と全体のコピー
            for (i = 2; i <= PagesCount; i++)
            {
                r = CaptionRowsCount + DataOffsetRow;
                for (y = Table.GetTableValueRowIndexMinimum + DataOffsetRow;
                     y <= Table.GetTableValueRowIndexMaximum; y++)
                {
                    if (!CutRowsCol.ContainsKey(y))
                    {
                        r = r + 1;
                        c = DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1);
                        for (x = Table.GetTableValueColumnIndexMinimum + DataOffsetColumn;
                             x <= Table.GetTableValueColumnIndexMinimum + DataOffsetColumn + (ToInt(IsShowPreWBTotal) & 1); x++)
                        {
                            c = c + 1;
                            TableValue.SetValue(TableValue.GetValue(r, c), r + (i - 1) * (PageRowsCount + 2), c);
                        }
                        if (IsSigTest)
                        {
                            r = r + (WholeRowCol.ContainsKey(y) ? d : d2) - 1;
                        }
                        else
                        {
                            r = r + d - 1;
                        }
                    }
                }
            }
        }

        public static int ToInt(bool test)
        {
            return test ? -1 : 0;
        }

        private void AdjustContentsSheet(List<Workbook> Books, Worksheet ContentsSheet
      , ref Array ContentsValue, ref Array HyperlinkTargetCells
      , TableType TableType
      , int MinIndex = 0, int MaxIndex = 0)
        {
            CrossTable tmpTable;
            string buf;
            GroupObject g;
            TextBox b;
            double r;
            double d;
            Shape tmpS, tmpS2;
            Shape[] s = new Shape[1];
            ; int cnt = 0;
            int i;
            double t1, t2;
            int c;
            int n;
            Array v;
            int clrIdx;
            double h;
            double delH = 0;
            int u;
            bool IsSigTest;
            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            ContentsSheet.Unprotect(SheetPSWD);
            ContentsSheet.Rectangles("TitleBox").Text = CurrentOutput.ParentRequest.Title;
            tmpTable = (CrossTable)CurrentOutput.Tables[0];
            if (tmpTable.KeyItem == null)
            {
                Name WithContentsSheetItem = ContentsSheet.Names.Item("KeyItemInformation");
                Range WithWithContentsSheetItemRef = WithContentsSheetItem.RefersToRange.EntireRow;
                //delH = WithWithContentsSheetItemRef.Height;
                WithWithContentsSheetItemRef.Clear();
                WithContentsSheetItem.Delete();
            }
            else
            {
                v = new string[2, 2];
                v.SetValue(LocalResource.REPORT_CLASSIFICATION_ITEM_KEYWORD, 0, 0);
                v.SetValue(LocalResource.REPORT_SECTOR_KEYWORD, 1, 0);
                KeyItemInformation WithtmpTable = tmpTable.KeyItem;
                v.SetValue(WithtmpTable.Name + ":" + WithtmpTable.Description, 0, 1);
                v.SetValue(WithtmpTable.SectorNumber + ":" + WithtmpTable.SectorDescription, 1, 1);

                Range WithContentsSheetRng = ContentsSheet.Range["KeyItemInformation"].Range["B1:C2"];
                OutputUtil.PutValue(WithContentsSheetRng.Cells, ref v);
                //OutputUtil.AutoFitEx(WithContentsSheetRng.Rows, xlApp, WorkingSheet);
            }
            buf = CurrentOutput.LocalizedFilteringExpression;
            if (null == buf || buf.Length == 0)
            {
                Name WithContentsSheetNams = ContentsSheet.Names.Item("Criteria");
                WithContentsSheetNams.RefersToRange.Clear();
                WithContentsSheetNams.Delete();
            }
            else
            {
                v = new string[2];
                v.SetValue(LocalResource.REPORT_FILTER_CRITERION_KEYWORD, 0);
                v.SetValue(buf, 1);
                double MAXROW_HEIGHT_INDEX_TITLE = 66;
                Range WithContentsSheetRng = ContentsSheet.Range["Criteria"];
                OutputUtil.PutValue(WithContentsSheetRng.Cells, ref v);
                Range WithContentsSheetRngER = WithContentsSheetRng.EntireRow;
                h = WithContentsSheetRngER.RowHeight;
                WithContentsSheetRngER.AutoFit();
                if (WithContentsSheetRngER.RowHeight < h) { WithContentsSheetRngER.RowHeight = h; }
                else if (WithContentsSheetRngER.RowHeight > MAXROW_HEIGHT_INDEX_TITLE) { WithContentsSheetRngER.RowHeight = MAXROW_HEIGHT_INDEX_TITLE; }
            }
            if (CurrentOutput.WBOn)
            {
                string msg = LocalResource.REPORT_MARKING_LEGEND_WEIGHTBACK_ON_PROMPT;
                OutputUtil.PutValue(ContentsSheet.Range["WBPrompt"], ref msg);
            }
            if (CurrentOutput.MarkingRanking || CurrentOutput.MarkingColoring || CurrentOutput.MarkingSignificance ||
                     CurrentOutput.MarkingAscending || IsSigTest)
            {
                if (CurrentOutput.MinSamplesCountOnMarking >= 0)
                {
                    string wbString = "";
                    if (CurrentOutput.WBOn)
                    {
                        wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_AFTER_WB;
                    }
                    if (CurrentOutput.WBOn && CurrentOutput.ShowPreWBTotal && CurrentOutput.PreWbBase)
                    {
                        wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_AFTER_WB;
                    }
                    string msg = string.Format(LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_PROMPT,
                        wbString, CurrentOutput.MinSamplesCountOnMarking.ToString());
                    OutputUtil.PutValue(ContentsSheet.Range["MinBase"], ref msg);
                }
            }
            g = ContentsSheet.GroupObjects("RateDifferenceLegend");
            b = ContentsSheet.TextBoxes("SignificanceTestLegend");
            r = g.Left + g.Width;
            d = b.Left - r;
            r = b.Left + b.Width;
            Shapes WithShapes = ContentsSheet.Shapes;
            tmpS = WithShapes.Item("SignificanceTestLegend");
            if (IsSigTest || CurrentOutput.MarkingSignificance)
            {
                u = -1;
                v = "".Split();
                if (IsSigTest)
                {
                    if (CurrentOutput.SignificanceTestOne)
                    {
                        if (u < 1)
                        {
                            u = u + 1;
                            ArrayPreserve(ref v, typeof(string), u);
                            v.SetValue(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_AT_1CAPTION, u);
                        }
                    }
                    if (CurrentOutput.SignificanceTestFive)
                    {
                        if (u < 1)
                        {
                            u = u + 1;
                            ArrayPreserve(ref v, typeof(string), u);
                            string msg = u == 1 ? LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_AT_5CAPTION.ToLower() : LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_AT_5CAPTION;
                            v.SetValue(msg, u);
                        }
                    }
                    if (CurrentOutput.SignificanceTestTen)
                    {
                        if (u < 1)
                        {
                            u = u + 1;
                            ArrayPreserve(ref v, typeof(string), u);
                            string msg = u == 1 ? LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_AT_10CAPTION.ToLower() : LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_AT_10CAPTION;
                            v.SetValue(msg, u);
                        }
                    }
                    tmpS.DrawingObject.Text = LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_CAPTION + "\n" + String.Join("\n", (string[])v);
                }
                else
                {
                    if (CurrentOutput.MarkingSignificanceOne)
                    {
                        u = u + 1;
                        ArrayPreserve(ref v, typeof(string), u);
                        v.SetValue(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_TO_WHOLE_AT_1CAPTION, u);
                    }
                    if (CurrentOutput.MarkingSignificanceFive)
                    {
                        u = u + 1;
                        ArrayPreserve(ref v, typeof(string), u);
                        v.SetValue(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_TO_WHOLE_AT_5CAPTION, u);
                    }
                    if (CurrentOutput.MarkingSignificanceTen)
                    {
                        u = u + 1;
                        ArrayPreserve(ref v, typeof(string), u);
                        v.SetValue(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_TO_WHOLE_AT_10CAPTION, u);
                    }
                    tmpS.DrawingObject.Text = System.Text.RegularExpressions.Regex.Unescape(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_TO_WHOLE_CAPTION) + "\n" + String.Join("\n", (string[])v);
                }
                Array.Resize(ref s, cnt + 1);
                s[cnt] = tmpS;
                cnt = cnt + 1;
            }
            else
            {
                tmpS.Delete();
            }
            tmpS = WithShapes.Item("RateDifferenceLegend");
            if (CurrentOutput.MarkingColoring)
            {
                tmpS.TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_CAPTION;
                Array.Resize(ref s, cnt + 1);
                s[cnt] = tmpS;
                cnt = cnt + 1;
            }
            else
            {
                tmpS.Delete();
            }
            tmpS = WithShapes.Item("RankingMarkingLegend");
            if (CurrentOutput.MarkingRanking)
            {
                tmpS.TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_CAPTION;
                WithShapes.Item("Rank1Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_1ST_CAPTION;
                WithShapes.Item("Rank2Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_2ND_CAPTION;
                WithShapes.Item("Rank3Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_3RD_CAPTION;
                Array.Resize(ref s, cnt + 1);
                s[cnt] = tmpS;
                cnt = cnt + 1;
            }
            else
            {
                tmpS.Delete();
            }
            tmpS = null;
            for (i = 0; i <= cnt - 1; i++)
            {
                s[i].Left = (float)r - s[i].Width;
                s[i].Top = s[i].Top - (float)delH;
                r = s[i].Left - (float)d;
            }
            if (CurrentOutput.MarkingColoring)
            {
                t1 = WithShapes.Item("Level2HighLabel").Top;
                t2 = WithShapes.Item("Level2HighPalette").Top;
                d = WithShapes.Item("Level1HighLabel").Top - t1;
                tmpS = WithShapes.Item("Level2HighLabel");
                tmpS2 = WithShapes.Item("Level2HighPalette");
                if (CurrentOutput.MarkingColoringLevel2High)
                {
                    tmpS.TextEffect.Text = string.Format(
                          LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
                       , (" " + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                    clrIdx = CurrentOutput.Level2HighColorIndex;
                    if (clrIdx < 0)
                    {
                        clrIdx = Convert.ToInt32(WorkingBook.Colors[2]);// ' 白
                    }
                    tmpS2.Fill.ForeColor.RGB = clrIdx;
                    tmpS.Top = (float)t1;
                    tmpS2.Top = (float)t2;
                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.TextEffect.Text = "";
                    //  tmpS2.Delete();
                }
                tmpS = WithShapes.Item("Level1HighLabel");
                tmpS2 = WithShapes.Item("Level1HighPalette");
                if (CurrentOutput.MarkingColoringLevel1High)
                {
                    tmpS.TextEffect.Text = string.Format(
                         LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
                         , (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                    clrIdx = CurrentOutput.Level1HighColorIndex;
                    if (clrIdx < 0)
                    {
                        clrIdx = Convert.ToInt32(WorkingBook.Colors[2]);// ' 白
                    }
                    tmpS2.Fill.ForeColor.RGB = clrIdx;
                    tmpS.Top = (float)t1;
                    tmpS2.Top = (float)t2;
                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.TextEffect.Text = "";
                    //tmpS2.Delete();
                }
                tmpS = WithShapes.Item("Level1LowLabel");
                tmpS2 = WithShapes.Item("Level1LowPalette");
                if (CurrentOutput.MarkingColoringLevel1Low)
                {
                    tmpS.TextEffect.Text = string.Format(
                          LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                           , (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                    clrIdx = CurrentOutput.Level1LowColorIndex;
                    if (clrIdx < 0)
                    {
                        clrIdx = Convert.ToInt32(WorkingBook.Colors[2]);// ' 白
                    }
                    tmpS2.Fill.ForeColor.RGB = clrIdx;
                    tmpS.Top = (float)t1;
                    tmpS2.Top = (float)t2;
                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.TextEffect.Text = "";
                    // tmpS2.Delete();
                }
                tmpS = WithShapes.Item("Level2LowLabel");
                tmpS2 = WithShapes.Item("Level2LowPalette");
                if (CurrentOutput.MarkingColoringLevel2Low)
                {
                    tmpS.TextEffect.Text = string.Format(
                           LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                           , (" " + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                    clrIdx = CurrentOutput.Level2LowColorIndex;
                    if (clrIdx < 0)
                    {
                        clrIdx = Convert.ToInt32(WorkingBook.Colors[2]);// ' 白
                    }
                    tmpS2.Fill.ForeColor.RGB = clrIdx;
                    tmpS.Top = (float)t1;
                    tmpS2.Top = (float)t2;
                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.TextEffect.Text = "";
                    // tmpS2.Delete();
                }
            }

            c = CurrentOutput.Tables.Count;
            if (Books == null)
            {    //' 1シート複数クロス
                ContentsValue = Array.CreateInstance(typeof(string),
                    new int[] { c, 6 },
                    new int[] { 1, 1 });
                n = c;
            }
            else
            {   // ' 1シート1クロス
                ContentsValue = Array.CreateInstance(typeof(string),
                    new int[] { MaxIndex - MinIndex + 1, 4 },
                    new int[] { MinIndex, 1 });
                n = MaxIndex - MinIndex + 1;
            }
            HyperlinkTargetCells = Array.CreateInstance(typeof(Range),
                new int[] { ContentsValue.GetUpperBound(0) - ContentsValue.GetLowerBound(0) + 1, ContentsValue.GetUpperBound(1) - 4 + 1 },
                new int[] { ContentsValue.GetLowerBound(0), 4 });

            Range rowWith = ContentsSheet.Range["Contents"].EntireRow.Item[2];
            if (n > 3)
            {
                rowWith.Copy();
                rowWith.Resize[n - 3].Insert(XlInsertShiftDirection.xlShiftDown);
                xlApp.CutCopyMode = (XlCutCopyMode)1;
            }
            else if (n < 3)
            {
                rowWith.Resize[3 - n].Delete(XlDeleteShiftDirection.xlShiftUp);
                if (n < 2)
                {
                    Border borderWith = ContentsSheet.Range["Contents"].Borders.Item[XlBordersIndex.xlEdgeBottom];
                    borderWith.LineStyle = XlLineStyle.xlContinuous;
                    borderWith.Weight = XlBorderWeight.xlThin;
                }
            }
            v = Array.CreateInstance(typeof(string), new int[] { ContentsValue.GetUpperBound(1) }, new int[] { 1 });
            v.SetValue(LocalResource.REPORT_LAYOUT_QUESTION_NUMBER_COLUMN_CAPTION, 1);
            v.SetValue(LocalResource.REPORT_LAYOUT_QC3_DESCRIPTION_2COLUMN_CAPTION, 2);
            if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == FileType.Report && !onlySigPage)
            {
                v.SetValue(LocalResource.REPORT_PAGE_KEYWORD, 4);
            }
            else if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
            {
                switch (TableType)
                {
                    case TableType.NPer:
                        v.SetValue(LocalResource.REPORT_CROSS_NP_SHEET_NAME, 4);
                        break;
                    case TableType.N:
                        v.SetValue(LocalResource.REPORT_CROSS_N_SHEET_NAME, 4);
                        break;
                    case TableType.Per:
                        v.SetValue(LocalResource.REPORT_CROSS_P_SHEET_NAME, 4);
                        break;
                    case TableType.SignificanceTest:
                        v.SetValue(LocalResource.REPORT_CROSS_SIGNIFICANCE_TEST_SHEET_NAME, 4);
                        break;
                }
            }
            else
            {
                v.SetValue(LocalResource.REPORT_CROSS_NP_SHEET_NAME, 4);
                v.SetValue(LocalResource.REPORT_CROSS_N_SHEET_NAME, 5);
                v.SetValue(LocalResource.REPORT_CROSS_P_SHEET_NAME, 6);
            }
            OutputUtil.PutValue(ContentsSheet.Range["ContentsCaption"], ref v);
        }

        public static void ArrayPreserve(ref Array v, Type type, int u)
        {
            Array t = (Array)v.Clone();
            v = Array.CreateInstance(type, u + 1);
            t.CopyTo(v, 0);
        }

        private void GetRequiredRowsColsCountLandscape(CrossTable Table
      , Hashtable CutRowsCol, Hashtable CutColumnsCol
      , int DataOffsetRow, int AddColumnsCount
      , TableType TableType, bool isN
      , ref int d, ref int CaptionRowsCount
      , ref int RequiredRowsCount, ref int RequiredColumnsCount
      , ref int d2, Hashtable WholeRowCol = null)
        {
            int RowsCount;
            int ColumnsCount;
            int DataRowsCount;
            int r;
            bool IsSigTest;
            int i;
            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;

            //d = 1 & +(Not isN And(TableType And TableType_NPer) = TableType_NPer And 1 &)
            //d2 = d + (Not isN And IsSigTest And 1 &)

            d = 1 + (ToInt(!isN & ((TableType & TableType.NPer) == TableType.NPer)) & 1);
            d2 = d + (ToInt(!isN & IsSigTest) & 1);
            if (IsSigTest && NPOICrossCreator.checkSimpleAggr(Table) && !isN)
            {
                d = d + 1;
            }

            RowsCount = Table.GetTableValueRowIndexMaximum - Table.GetTableValueRowIndexMinimum + 1 - CutRowsCol.Count;
            ColumnsCount = Table.GetTableValueColumnIndexMaximum - Table.GetTableValueColumnIndexMinimum + 1 - CutColumnsCol.Count + AddColumnsCount;
            if (IsSigTest && !NPOICrossCreator.checkSimpleAggr(Table))
            {
                r = Table.GetTableValueRowIndexMinimum + DataOffsetRow; //' GTs
                DataRowsCount = 0;
                for (i = r + 1; i <= Table.GetTableValueRowIndexMaximum; i++)
                {
                    if (!(CutRowsCol.ContainsKey(i)))
                    {
                        DataRowsCount = DataRowsCount + (WholeRowCol.ContainsKey(i) ? d : d2);
                    }
                }
            }
            else
            {
                DataRowsCount = (RowsCount - DataOffsetRow) * d;
            }
            if ((Table.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                CaptionRowsCount = 2 + (ToInt(CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single) & 2);
                RequiredRowsCount = CaptionRowsCount + DataOffsetRow + DataRowsCount;
            }
            else
            {
                CaptionRowsCount = 0;
                RequiredRowsCount = DataOffsetRow + DataRowsCount;
            }
            RequiredColumnsCount = ColumnsCount + (ToInt(IsSigTest) & (1 + (ToInt(isN) & 1)));
        }

        public int GetMaxAxesCount(CrossTable Table)
        {
            int res;
            int i;
            res = 1;
            AxesGroupInformation WithTableAxesGroups = Table.AxesGroups;
            for (i = 1; i <= WithTableAxesGroups.Count; i++)
            {
                if (WithTableAxesGroups[i - 1].Count == 2)
                {
                    res = 2;
                    break;
                }
            }
            return res;
        }

        public bool GetHasWeight(CrossTable Table)
        {
            return Table.Question.HasWeight || Table.Question.HasCount;
        }

        public void AdjustFormat(
        Worksheet FormatSheet, Worksheet SigTestFormatSheet
              , int MaxAxesCount, bool HasWeightColumn, bool OnlyCutTriple = false, bool ExtendRowHeight = false)
        {
            string[] tmpNamesArray;
            string tmpName;
            int tmp;
            string Suffix;
            string tmpSuffix;
            string fmt;
            bool IsPortrait;
            XlDeleteShiftDirection tmpDelShift;
            int i;
            Range tmpRange;
            bool RedrawBorder;
            IsPortrait = CurrentOutput.Orientation == TableOrientation.Portrait;
            if (!OnlyCutTriple)
            {
                if (FormatSheet != null)
                {
                    FormatSheet.Unprotect(SheetPSWD);
                }
                if (SigTestFormatSheet != null)
                {
                    SigTestFormatSheet.Unprotect(SheetPSWD);
                }
                if (!CurrentOutput.ParentRequest.MergeAxis)
                {
                    if (FormatSheet != null)
                    {
                        Range WithFormatSheetRange = FormatSheet.Range["MergedCells"];
                        WithFormatSheetRange.UnMerge();
                        WithFormatSheetRange.WrapText = false;
                    }
                    if (CurrentOutput.SignificanceTest)
                    {
                        if (SigTestFormatSheet != null)
                        {
                            Range WithSigTestFormatSheetRange = SigTestFormatSheet.Range["MergedCells"];
                            WithSigTestFormatSheetRange.UnMerge();
                            WithSigTestFormatSheetRange.WrapText = false;

                        }
                    }
                }
            }
            if (MaxAxesCount == 1)
            {
                if (IsPortrait)
                {
                    DeleteName(FormatSheet, SigTestFormatSheet, "TripleRows", XlDeleteShiftDirection.xlShiftUp);
                    if (ExtendRowHeight)
                    {
                        Range WithFormatSheetRange = FormatSheet.Range["DoubleRows"];
                        WithFormatSheetRange.RowHeight = WithFormatSheetRange.RowHeight * 2;
                    }
                }
                else
                {
                    DeleteName(FormatSheet, SigTestFormatSheet, "TripleColumn", XlDeleteShiftDirection.xlShiftToLeft);
                }
            }
            if (OnlyCutTriple) { return; }
            //' フォーマットシートのWB前全体列またはWB前全体行の削除
            if (!CurrentOutput.ShowPreWBTotal)
            {
                if (IsPortrait)
                {
                    DeleteName(FormatSheet, SigTestFormatSheet, "PreWBRows", XlDeleteShiftDirection.xlShiftUp);
                }
                else
                {
                    DeleteName(FormatSheet, SigTestFormatSheet, "PreWBColumn", XlDeleteShiftDirection.xlShiftToLeft);
                }
            }
            // ' 不要な無回答/非該当行列の削除
            if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
            {
                tmpNamesArray = "SA_MA_NP SA_MA_N SA_MA_P SA_MA_NP_WT SA_MA_N_WT SA_MA_P_WT N".Split();
            }
            else
            {
                tmpNamesArray = "SA_MA SA_MA_WT N".Split();
                if (SigTestFormatSheet != null)
                {
                    tmpNamesArray = "SA_MA SA_MA_WT SA_MA_NP SA_MA_WT_NP N".Split();
                }
            }
            RedrawBorder = !CurrentOutput.ShowIVAtItem;
            if (IsPortrait)
            {
                if (!CurrentOutput.ShowIVAtItem)
                {
                    tmpSuffix = "_InvalidRow";
                    for (i = 0; i <= tmpNamesArray.GetUpperBound(0); i++)
                    {
                        DeleteName(FormatSheet, SigTestFormatSheet, tmpNamesArray[i] + tmpSuffix, XlDeleteShiftDirection.xlShiftUp
                              , RedrawBorder & !tmpNamesArray[i].EndsWith("_WT"));
                    }
                }
                if (!CurrentOutput.ShowNAAtItem)
                {
                    tmpSuffix = "_NoAnswerRow";
                    for (i = 0; i <= tmpNamesArray.GetUpperBound(0); i++)
                    {
                        DeleteName(FormatSheet, SigTestFormatSheet, tmpNamesArray[i] + tmpSuffix, XlDeleteShiftDirection.xlShiftUp
                             , RedrawBorder & !tmpNamesArray[i].EndsWith("_WT") && !tmpNamesArray[i].StartsWith("SA_"));
                    }
                }
                else
                {
                    RedrawBorder = false;
                }
            }
            else
            {
                if (!CurrentOutput.ShowIVAtItem)
                {
                    tmpSuffix = "_InvalidColumn";
                    for (i = 0; i <= tmpNamesArray.GetUpperBound(0); i++)
                    {
                        DeleteName(FormatSheet, SigTestFormatSheet, tmpNamesArray[i] + tmpSuffix, XlDeleteShiftDirection.xlShiftToLeft
                            , RedrawBorder & !tmpNamesArray[i].EndsWith("_WT"));
                    }
                }
                if (!CurrentOutput.ShowNAAtItem)
                {
                    tmpSuffix = "_NoAnswerColumn";
                    for (i = 0; i <= tmpNamesArray.GetUpperBound(0); i++)
                    {
                        DeleteName(FormatSheet, SigTestFormatSheet, tmpNamesArray[i] + tmpSuffix, XlDeleteShiftDirection.xlShiftToLeft
                               , true);
                    }
                }
                else
                {
                    RedrawBorder = false;
                }
            }
            //' ウエイト値、加重平均のセルの書式設定
            if (HasWeightColumn)
            {
                tmp = CurrentOutput.ParentRequest.WeightNumDigitsAfterDecimal;
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                {
                    tmpNamesArray = "SA_MA".Split();
                }
                else
                {
                    tmpNamesArray = "SA_MA_NP SA_MA_N SA_MA_P".Split();
                }
                Suffix = "_WT_Weight" + (IsPortrait ? "Column" : "Row");
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix, true);
                tmp = CurrentOutput.ParentRequest.WeightAverageNumDigitsAfterDecimal;
                Suffix = "_WT_WeightAverage";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
            }
            else if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                //' 縦％表の場合はウエイト列削除
                if (IsPortrait)
                {
                    DeleteName(FormatSheet, SigTestFormatSheet, "WeightColumn", XlDeleteShiftDirection.xlShiftToLeft);
                }
            }
            // ' フォーマットシートの数値回答集計表フォーマットの調整
            tmpSuffix = IsPortrait ? "Row" : "Column";
            tmpDelShift = IsPortrait ? XlDeleteShiftDirection.xlShiftUp : XlDeleteShiftDirection.xlShiftToLeft;
            tmpNamesArray = "N".Split();
            if (CurrentOutput.ParentRequest.ShowMedian)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Median);
                Suffix = "_Median";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Median" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (CurrentOutput.ParentRequest.ShowMaximum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Maximum);
                Suffix = "_Maximum";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Maximum" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (CurrentOutput.ParentRequest.ShowMinimum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Minimum);
                Suffix = "_Minimum";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Minimum" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (CurrentOutput.ParentRequest.ShowStdev)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Stdev);
                Suffix = "_Deviation";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Deviation" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (CurrentOutput.ParentRequest.ShowAverage)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Average);
                Suffix = "_Average";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Average" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (CurrentOutput.ParentRequest.ShowSummary)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Summary);
                Suffix = "_Summary";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Summary" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (!CurrentOutput.ParentRequest.ShowParameter)
            {
                tmpName = "N_Population" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
        }

        private void DeleteName(
                Worksheet FormatSheet, Worksheet SigTestFormatSheet
              , string DeleteName, XlDeleteShiftDirection DelShift
              , bool RedrawBorder = false)
        {
            Range tmpRange = null;
            if (FormatSheet != null)
            {
                Name WithFormatSheetName = FormatSheet.Names.Item(DeleteName);
                if (RedrawBorder)
                {
                    Range WithWithFormatSheetNameRefersToRange = WithFormatSheetName.RefersToRange;
                    if (DelShift == XlDeleteShiftDirection.xlShiftUp)
                    {
                        tmpRange = WithWithFormatSheetNameRefersToRange.Rows.Item[0];
                    }
                    else
                    {
                        tmpRange = WithWithFormatSheetNameRefersToRange.Columns.Item[0];
                    }
                }
                if (DelShift == XlDeleteShiftDirection.xlShiftUp)
                {
                    WithFormatSheetName.RefersToRange.EntireRow.Delete(DelShift);
                }
                else
                {
                    WithFormatSheetName.RefersToRange.Delete(DelShift);
                }
                WithFormatSheetName.Delete();
                if (RedrawBorder)
                {
                    Border WithtmpRangeBorder = tmpRange.Borders.Item[DelShift == XlDeleteShiftDirection.xlShiftUp ? XlBordersIndex.xlEdgeBottom : XlBordersIndex.xlEdgeRight];
                    WithtmpRangeBorder.LineStyle = XlLineStyle.xlContinuous;
                    WithtmpRangeBorder.Weight = XlBorderWeight.xlThin;
                    WithtmpRangeBorder.Color = BORDER_COLOR;
                }
            }
            if (CurrentOutput.SignificanceTest)
            {
                if (SigTestFormatSheet != null)
                {
                    Name WithSigTestFormatSheetName = SigTestFormatSheet.Names.Item(DeleteName);
                    if (RedrawBorder)
                    {
                        Range WithWithSigTestFormatSheetName = WithSigTestFormatSheetName.RefersToRange;
                        if (DelShift == XlDeleteShiftDirection.xlShiftUp)
                        {
                            tmpRange = WithWithSigTestFormatSheetName.Rows.Item[0];
                        }
                        else
                        {
                            tmpRange = WithWithSigTestFormatSheetName.Columns.Item[0];
                        }
                    }
                    if (DelShift == XlDeleteShiftDirection.xlShiftUp)
                    {
                        WithSigTestFormatSheetName.RefersToRange.EntireRow.Delete(DelShift);
                    }
                    else
                    {
                        WithSigTestFormatSheetName.RefersToRange.Delete(DelShift);
                    }
                    WithSigTestFormatSheetName.Delete();
                    if (RedrawBorder)
                    {
                        Border WithtmpRangeBorder = tmpRange.Borders.Item[DelShift == XlDeleteShiftDirection.xlShiftUp ? XlBordersIndex.xlEdgeBottom : XlBordersIndex.xlEdgeRight];

                        WithtmpRangeBorder.LineStyle = XlLineStyle.xlContinuous;
                        WithtmpRangeBorder.Weight = XlBorderWeight.xlThin;
                        WithtmpRangeBorder.Color = BORDER_COLOR;
                    }
                }
            }
        }

        private void NumberFormat(
                Worksheet FormatSheet, Worksheet SigTestFormatSheet
              , string[] NamesArray, int NumDigitsAfterDecimal
              , string Suffix = null, bool IsWeight = false)
        {
            string fmt;
            string n;
            if (NumDigitsAfterDecimal == 0)
            {
                fmt = "0";
            }
            else if (NumDigitsAfterDecimal >= 1 || NumDigitsAfterDecimal <= 5)
            {
                fmt = "0." + new String('0', NumDigitsAfterDecimal);
            }
            else
            {
                fmt = "0.0";
            }
            if (IsWeight)
            {
                if (FormatSheet != null)
                {
                    fmt = FormatSheet.Range[NamesArray[0] + Suffix].Cells.Item[1].NumberFormat.Replace("0.0", fmt);
                }
                else if (SigTestFormatSheet != null)
                {
                    fmt = SigTestFormatSheet.Range[NamesArray[0] + Suffix].Cells.Item[1].NumberFormat.Replace("0.0", fmt);
                }
            }
            foreach (string tmpName in NamesArray)
            {
                n = tmpName + Suffix;
                if (FormatSheet != null)
                {
                    FormatSheet.Range[n].NumberFormat = fmt;
                }
                if (CurrentOutput.SignificanceTest)
                {
                    if (SigTestFormatSheet != null)
                    {
                        SigTestFormatSheet.Range[n].NumberFormat = fmt;
                    }
                }
            }
        }

        public void GetCutRowsAndColumns(CrossTable Table
              , bool HasWeightBack, bool HasWeight, int MaxAxesCount
              , ref Hashtable CutRowsCol, ref Hashtable CutColumnsCol, ref int MedIdx // ref int MedIdx   = -1
              , bool IsReport = false
              , bool CutMedian = false
              , Hashtable WholeRowCol = null)
        {
            bool DefHasNAAtItem;
            bool DefHasIVAtItem;
            bool DefHasNAAtAxis;
            bool DefHasIVAtAxis;
            int tmpIdx;
            int PreSummaryRowIndex;
            int i, j;
            int x;
            int r;
            int PreWBColumnIndex;
            int LastColumnIndex;
            bool CheckZero;
            bool CutNA;
            bool CutIV;

            CutRowsCol = new Hashtable();
            CutColumnsCol = new Hashtable();
            DefHasNAAtItem = CurrentOutput.ShowNAAtItem;
            DefHasIVAtItem = CurrentOutput.ShowIVAtItem;
            DefHasNAAtAxis = CurrentOutput.ShowNAAtAxis;
            DefHasIVAtAxis = CurrentOutput.ShowIVAtAxis;

            PreWBColumnIndex = Table.GetTableValueColumnIndexMinimum + MaxAxesCount + 1;
            LastColumnIndex = Table.GetTableValueColumnIndexMaximum;
            CheckZero = !Table.ParentRequest.ShowZeroNAIV;
            bool isN = (Table.Question.QuestionType & QuestionType.N) == QuestionType.N;
            if (DefHasNAAtItem && !isN)
            {
                if (CheckZero)
                {
                    tmpIdx = LastColumnIndex - (ToInt(HasWeight) & 2) - (ToInt(DefHasIVAtItem) & 1) - (Table.Question.SubTotalCnt);
                    CutNA = Convert.ToDouble(Table.TableValue(2, tmpIdx)) == 0;
                    if (CutNA) { CutColumnsCol.Add(tmpIdx, tmpIdx); }
                }
            }
            if (DefHasIVAtItem)
            {
                if (CheckZero)
                {
                    tmpIdx = LastColumnIndex - (ToInt(HasWeight) & 2) - (Table.Question.SubTotalCnt);
                    CutIV = Convert.ToDouble(Table.TableValue(2, tmpIdx)) == 0;
                    if (CutIV) { CutColumnsCol.Add(tmpIdx, tmpIdx); }
                }
            }
            //if (IsReport)
            //{
            MedIdx = Table.GetTableValueColumnIndexMinimum - 1;
            if ((Table.Question.QuestionType & QuestionType.N) == QuestionType.N)
            {
                if (CutMedian)
                {
                    MedIdx = LastColumnIndex - (ToInt(DefHasIVAtItem) & 1) - (ToInt(DefHasNAAtItem) & 1) - (Table.Question.SubTotalCnt);
                    CutColumnsCol.Add(MedIdx, MedIdx);
                }
            }
            //}

            //r = (ToInt(HasWeight) & 1) + 1;
            r = 2;
            if (NPOICrossCreator.checkSimpleAggr(Table))
            {
                CutRowsCol.Add(3, 3);
            }
            else
            {
                CutRowsCol.Add(r, r);
            }
            // if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
            PreSummaryRowIndex = r;
            for (i = 1; i <= Table.AxesGroups.Count; i++)
            {
                AxesInformation WithTableAxesGroup = Table.AxesGroups[i - 1];
                if (WithTableAxesGroup.Count == 1)
                { //' 二重クロス
                    r = r + 1;  //' 小計行
                    if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
                    for (j = PreWBColumnIndex; j <= LastColumnIndex; j++)
                    {
                        if (Table.TableValue(r, j) != Table.TableValue(PreSummaryRowIndex, j))
                        {
                            PreSummaryRowIndex = r;
                            break;
                        }
                    }
                    if (!WithTableAxesGroup.ShowTotal) { CutRowsCol.Add(r, r); }
                    r = r + WithTableAxesGroup[0].SectorsCount;
                    if (DefHasNAAtAxis)
                    {
                        r = r + 1;
                        if (CheckZero)
                        {
                            CutNA = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                            if (CutNA) { CutRowsCol.Add(r, r); }
                        }
                    }
                    if (DefHasIVAtAxis)
                    {
                        r = r + 1;
                        if (CheckZero)
                        {
                            CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                            if (CutIV) { CutRowsCol.Add(r, r); }
                        }
                    }
                }
                else
                {    //' 三重クロス
                    r = r + 1;
                    if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
                    if (!WithTableAxesGroup.ShowTotal) { CutRowsCol.Add(r, r); }
                    for (x = 1; x <= WithTableAxesGroup[0].SectorsCount; x++)
                    {
                        r = r + 1;  //' 二重クロスの小計行
                        if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
                        r = r + WithTableAxesGroup[1].SectorsCount;
                        if (DefHasNAAtAxis)
                        {
                            r = r + 1;
                            if (CheckZero)
                            {
                                CutNA = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                if (CutNA) { CutRowsCol.Add(r, r); }
                            }
                        }
                        if (DefHasIVAtAxis)
                        {
                            r = r + 1;
                            if (CheckZero)
                            {
                                CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                if (CutIV) { CutRowsCol.Add(r, r); }
                            }
                        }
                    }
                    if (DefHasNAAtAxis)
                    {
                        r = r + 1;
                        if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
                        if (CheckZero)
                        {
                            CutNA = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                            if (CutNA)
                            {
                                CutRowsCol.Add(r, r);
                                for (x = 1; x <= WithTableAxesGroup[1].SectorsCount; x++)
                                {
                                    r = r + 1;
                                    CutRowsCol.Add(r, r);
                                }
                                r = r + 1;
                                CutRowsCol.Add(r, r);
                                if (DefHasIVAtAxis)
                                {
                                    r = r + 1;
                                    CutRowsCol.Add(r, r);
                                }
                            }
                            else
                            {
                                r = r + WithTableAxesGroup[1].SectorsCount + 1;
                                CutNA = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                if (CutNA) { CutRowsCol.Add(r, r); }
                                if (DefHasIVAtAxis)
                                {
                                    r = r + 1;
                                    CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                    if (CutIV) { CutRowsCol.Add(r, r); }
                                }
                            }
                        }
                        else
                        {
                            r = r + WithTableAxesGroup[1].SectorsCount + 1 + (ToInt(DefHasIVAtAxis) & 1);
                        }
                    }
                    if (DefHasIVAtAxis)
                    {
                        r = r + 1;
                        if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
                        if (CheckZero)
                        {
                            CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                            if (CutIV)
                            {
                                CutRowsCol.Add(r, r);
                                for (x = 1; x <= WithTableAxesGroup[1].SectorsCount; x++)
                                {
                                    r = r + 1;
                                    CutRowsCol.Add(r, r);
                                }
                                if (DefHasIVAtAxis)
                                {
                                    r = r + 1;
                                    CutRowsCol.Add(r, r);
                                }
                                r = r + 1;
                                CutRowsCol.Add(r, r);
                            }
                            else
                            {
                                r = r + WithTableAxesGroup[1].SectorsCount;
                                if (DefHasNAAtAxis)
                                {
                                    r = r + 1;
                                    CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                    if (CutIV) { CutRowsCol.Add(r, r); }
                                }
                                r = r + 1;
                                CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                if (CutIV) { CutRowsCol.Add(r, r); }
                            }
                        }
                        else
                        {
                            r = r + WithTableAxesGroup[1].SectorsCount + (ToInt(DefHasNAAtAxis) & 1) + 1;
                        }
                    }
                }
            }
        }


        private void FormatTurnedLandscapeTable(CrossTable Table
              , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , Worksheet FormatSheet, string FormatRangeNamePrefix
              , TableType TableType, bool HasWeight
              , int ColumnsCountPerPage, int PagesCount
              , Range StartCell, bool isN, int AxesCount
              , Hashtable WholeRowCol = null)
        {
            List<Range> tmpCol = null;
            Range FormattedRange;


            FormatTurnedLandscapeTableSub(Table, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType, HasWeight, isN
                                        , AxesCount, ColumnsCountPerPage, PagesCount, ref tmpCol, 0, WholeRowCol);
            FormattedRange = WorkingSheet.Range[tmpCol[0], tmpCol[tmpCol.Count - 1]];
            OutputUtil.CopyRow(FormattedRange, StartCell);
        }


        private void FormatTurnedLandscapeTableSub(CrossTable Table
              , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , Worksheet FormatSheet, string FormatRangeNamePrefix
              , TableType TableType, bool HasWeight
              , bool isN, int AxesCount
              , int ColumnsCountPerPage, int PagesCount
              , ref List<Range> PartsFormatRangeCol
              , double HeaderRowHeight = 0
              , Hashtable WholeRowCol = null)
        {
            Worksheet SubWorkingSheet;
            bool RedrawBorder;
            bool HasNAColumn;
            bool HasIVColumn;
            bool HasNARow;
            bool HasIVRow;
            int d, d2;
            bool CutNAColumn = false;
            bool CutIVColumn = false;
            int ItemSectorsCount = 0;
            int[] SectorsCount = new int[2];
            int rs, cs;
            int rc, cc;
            int SectorsCountPerPage;
            Range tmpRange;
            Range TableHeaderRange;
            Range TableRange;
            Range BodyRange; ; ;
            Range PagingRange;
            Range SectorColumns = null;
            Range NAColumn = null;
            Range IVColumn = null;
            Range WTColumns = null;
            bool CutNA = false;
            bool CutIV = false;
            int r, c;
            int y, x;
            int n;
            int i;
            int tmp;
            int tmpR;
            int RemainedSectorsCount;
            bool RemainedNA;
            bool RemainedIV;
            bool RemainedWTPopulation;
            bool RemainedWTAverage;
            int tmpCount;
            bool IsSigTest;
            TableType tType;

            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            SubWorkingSheet = WorkingSheet.Parent.Worksheets.Item(2);
            PartsFormatRangeCol = new List<Range>();
            WorkingSheet.UsedRange.Clear();
            WorkingSheet.DrawingObjects().Delete();
            SubWorkingSheet.UsedRange.Clear();
            HasNAColumn = CurrentOutput.ShowNAAtItem;
            HasIVColumn = CurrentOutput.ShowIVAtItem;
            HasNARow = CurrentOutput.ShowNAAtAxis;
            HasIVRow = CurrentOutput.ShowIVAtAxis;
            d = 1 + (ToInt(!isN & tType == TableType.NPer) & 1);
            d2 = d + (ToInt(!isN & IsSigTest) & 1);

            if (isN)
            {
                if (HasNAColumn)
                {
                    CutNAColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasIVColumn) & 1));
                }
                if (HasIVColumn)
                {
                    CutIVColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum);
                }
            }
            else
            {
                ItemSectorsCount = Table.SectorsCount;
                if (HasNAColumn)
                {
                    CutNAColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2) - (ToInt(HasIVColumn) & 1) - (Table.Question.SubTotalCnt));
                }
                if (HasIVColumn)
                {
                    CutIVColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2) - (Table.Question.SubTotalCnt));
                }
                if (!HasWeight)
                {
                    if (!HasIVColumn || CutIVColumn)
                    {
                        if (!HasNAColumn || CutNAColumn)
                        {
                            RedrawBorder = true;
                        }
                    }
                }
            }
            tmpRange = FormatSheet.Range[FormatRangeNamePrefix + "_Header"];
            OutputUtil.CopyRow(tmpRange, SubWorkingSheet.Range["A1"]);
            TableHeaderRange = xlApp.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_Table"]);
            rs = TableHeaderRange.Row - tmpRange.Row + 1;
            cs = TableHeaderRange.Column;
            rc = TableHeaderRange.Rows.Count;
            cc = TableHeaderRange.Columns.Count;
            TableHeaderRange = SubWorkingSheet.Cells.Item[rs, cs].Resize(rc, cc);
            if (HeaderRowHeight > 0)
            {
                TableHeaderRange.EntireRow.Item[1].RowHeight = HeaderRowHeight;
            }
            PagingRange = xlApp.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_PagingArea"]);
            cs = PagingRange.Column;
            cc = PagingRange.Columns.Count;
            PagingRange = SubWorkingSheet.Cells.Item[rs, cs].Resize(rc, cc);
            if (isN)
            {
                if (CutIVColumn)
                {
                    IVColumn = xlApp.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_InvalidColumn"]);
                    IVColumn = SubWorkingSheet.Cells.Item[rs, IVColumn.Column].Resize(rc);
                    IVColumn.Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    IVColumn = null;
                }
                if (CutNAColumn)
                {
                    NAColumn = xlApp.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_NoAnswerColumn"]);
                    NAColumn = SubWorkingSheet.Cells.Item[rs, NAColumn.Column].Resize(rc);
                    NAColumn.Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    NAColumn = null;
                }
            }
            else
            {
                SectorColumns = xlApp.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_SectorColumns"]);
                cs = SectorColumns.Column;
                cc = SectorColumns.Columns.Count;
                SectorColumns = SubWorkingSheet.Cells.Item[rs, cs].Resize(rc, cc);
            }
            if (!HasNAColumn || CutNAColumn)
            {
            }
            else
            {
                NAColumn = xlApp.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_NoAnswerColumn"]);
                NAColumn = SubWorkingSheet.Cells.Item[rs, NAColumn.Column].Resize(rc);
            }
            if (!HasIVColumn || CutIVColumn)
            {
            }
            else
            {
                IVColumn = xlApp.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_InvalidColumn"]);
                IVColumn = SubWorkingSheet.Cells.Item[rs, IVColumn.Column].Resize(rc);
            }
            if (HasWeight)
            {
                WTColumns = FormatSheet.Range[FormatSheet.Range[FormatRangeNamePrefix + "_PopulationColumn"],
                    FormatSheet.Range[FormatRangeNamePrefix + "_WeightAverageColumn"]];
                WTColumns = xlApp.Intersect(tmpRange, WTColumns);
                cs = WTColumns.Column;
                cc = WTColumns.Columns.Count;
                WTColumns = SubWorkingSheet.Cells.Item[rs, cs].Resize(rc, cc);
            }
            Range WithFormatSheetRange = FormatSheet.Range[FormatRangeNamePrefix + (AxesCount == 1 ? "_Double" : "_Triple")];
            OutputUtil.CopyRow(WithFormatSheetRange.Rows, TableHeaderRange.Item[TableHeaderRange.Rows.Count + 1, 1]);
            rc = WithFormatSheetRange.Rows.Count;
            if (isN)
            {
                if (CutIVColumn)
                {
                    IVColumn = xlApp.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_InvalidColumn"]);
                    IVColumn = TableHeaderRange.EntireRow.Cells.Item[TableHeaderRange.Rows.Count + 1, IVColumn.Column].Resize(rc);
                    IVColumn.Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    IVColumn = null;
                }
                if (CutNAColumn)
                {
                    NAColumn = xlApp.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_NoAnswerColumn"]);
                    NAColumn = TableHeaderRange.EntireRow.Cells.Item[TableHeaderRange.Rows.Count + 1, NAColumn.Column].Resize(rc);
                    NAColumn.Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    NAColumn = null;
                }
            }
            TableRange = TableHeaderRange.Resize[TableHeaderRange.Rows.Count + rc];
            BodyRange = TableHeaderRange.Rows.Item[TableHeaderRange.Rows.Count + 1].Resize(rc).Cells;
            PagingRange = PagingRange.Resize[PagingRange.Rows.Count + rc];
            if (isN)
            {
                if (!IsSigTest)
                {
                    if (CutIVColumn || !(HasIVColumn & CutNAColumn))
                    {
                        PagingRange.Borders.Item[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
                    }
                }
            }
            if (SectorColumns != null)
            {
                SectorColumns = SectorColumns.Resize[SectorColumns.Rows.Count + rc];
            }
            if (NAColumn != null)
            {
                NAColumn = NAColumn.Resize[NAColumn.Rows.Count + rc];
            }
            if (IVColumn != null)
            {
                IVColumn = IVColumn.Resize[IVColumn.Rows.Count + rc];
            }
            if (WTColumns != null)
            {
                WTColumns = WTColumns.Resize[WTColumns.Rows.Count + rc];
            }
            y = 2; // wt ' ヘッダの下端インデックス
            if (AxesCount == 1)
            {  //' 二重クロス
                y = y + 1;  //' 小計行
                r = 1;
                if (CutRowsCol.ContainsKey(y))
                {
                    BodyRange.Rows.Item[r].Resize(d).EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                }
                else
                {
                    r = r + d;
                }
                r = r + d2;//  ' 2つ目の選択肢行
                AxesInformation tmpAxGrp = Table.AxesGroups[0];
                SectorsCount[0] = tmpAxGrp[0].SectorsCount;
                y = y + SectorsCount[0];
                if (HasNARow)
                {
                    y = y + 1;
                    if (!(CutRowsCol.ContainsKey(y)))
                    {  // ' 無回答出力
                        SectorsCount[0] = SectorsCount[0] + 1;
                    }
                }
                if (HasIVRow)
                {
                    y = y + 1;
                    if (!(CutRowsCol.ContainsKey(y)))
                    {   //' 非該当出力
                        SectorsCount[0] = SectorsCount[0] + 1;
                    }
                }
                if (SectorsCount[0] > 3)
                {
                    Range WithBodyRangeRows = BodyRange.Rows.Item[r].Resize(d2).EntireRow();
                    WithBodyRangeRows.Copy();
                    WithBodyRangeRows.Resize[(SectorsCount[0] - 3) * d2].Insert(XlInsertShiftDirection.xlShiftDown);
                    xlApp.CutCopyMode = (XlCutCopyMode)1;
                }
                else if (SectorsCount[0] < 3)
                {
                    BodyRange.Rows.Item[r].Resize[(3 - SectorsCount[0]) * d2].EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                    if (SectorsCount[0] < 2)
                    {
                        Border WithBodyRange = BodyRange.Borders.Item[XlBordersIndex.xlEdgeBottom];
                        WithBodyRange.LineStyle = XlLineStyle.xlContinuous;
                        WithBodyRange.Weight = XlBorderWeight.xlThin;
                    }
                }
            }
            else
            {   // ' 三重クロス
                tmpRange = BodyRange.Rows.Item[d + 3 * d2 + 1].Resize(d + 3 * d2);
                AxesInformation tmpAxGrp1 = Table.AxesGroups[0];
                SectorsCount[0] = tmpAxGrp1[0].SectorsCount;
                SectorsCount[1] = tmpAxGrp1[1].SectorsCount;
                n = SectorsCount[1] + (ToInt(HasNARow) & 1) + (ToInt(HasIVRow) & 1) + 1;
                y = y + SectorsCount[0] * n;
                if (HasNARow)
                {
                    if (!(CutRowsCol.ContainsKey(y + 1)))
                    { //' 無回答出力
                        SectorsCount[0] = SectorsCount[0] + 1;
                    }
                    else
                    {
                        CutNA = true;
                    }
                    y = y + n;
                }
                if (HasIVRow)
                {
                    if (!(CutRowsCol.ContainsKey(y + 1)))
                    {//  ' 非該当出力
                        SectorsCount[0] = SectorsCount[0] + 1;
                    }
                    else
                    {
                        CutIV = true;
                    }
                    y = y + n;
                }
                if (SectorsCount[0] < 3)
                {
                    tmpRange.Resize[tmpRange.Rows.Count * (3 - SectorsCount[0])].EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                }
                SectorsCount[1] = n - 1;
                if (SectorsCount[1] > 3)
                {
                    i = 3;
                    for (r = 3 * d + 7 * d2 + 1; r >= d + d2 + 1; r = r + -(d + 3 * d2))
                    {
                        if (i <= SectorsCount[0])
                        {
                            Range WithBodyRange = BodyRange.Rows.Item[r].Resize(d2).EntireRow;
                            WithBodyRange.Copy();
                            WithBodyRange.Resize[(SectorsCount[1] - 3) * d2].EntireRow.Insert(XlInsertShiftDirection.xlShiftDown);
                            xlApp.CutCopyMode = (XlCutCopyMode)1;
                        }
                        i = i - 1;
                    }
                }
                else if (SectorsCount[1] < 3)
                {
                    i = 3;
                    for (r = 3 * d + 7 * d2 + 1; r >= d + d2 + 1; r = r + -(d + 3 * d2))
                    {
                        if (i <= SectorsCount[0])
                        {
                            BodyRange.Rows.Item[r].Resize((3 - SectorsCount[1]) * d2).EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                        i = i - 1;
                    }
                }
                if (SectorsCount[0] > 3)
                {
                    Range WithtmpRange = tmpRange.EntireRow;
                    WithtmpRange.Copy();
                    WithtmpRange.Resize[WithtmpRange.Rows.Count * (SectorsCount[0] - 3)].Insert(XlInsertShiftDirection.xlShiftDown);
                    xlApp.CutCopyMode = (XlCutCopyMode)1;
                }
                r = SectorsCount[0] * (d + SectorsCount[1] * d2) + 1;
                for (i = y - (ToInt(CutIV) & n) - (ToInt(CutNA) & n); i >= 1 + 1 + 1; i--) //wt
                {
                    tmp = i;
                    if (CutNA & !CutIV)
                    {
                        if (i > y - n) { tmp = (i + n); }
                    }
                    if (IsSigTest)
                    {
                        r = r - (WholeRowCol.ContainsKey(tmp) ? d : d2);
                    }
                    else
                    {
                        r = r - d;
                    }
                    if (CutRowsCol.ContainsKey(tmp))
                    {
                        if (IsSigTest)
                        {
                            BodyRange.Rows.Item[r].Resize((WholeRowCol.ContainsKey(tmp) ? d : d2)).EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                        else
                        {
                            BodyRange.Rows.Item[r].Resize(d).EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                    }
                }
                Border WithBodyRangeBorders = BodyRange.Borders.Item[XlBordersIndex.xlEdgeBottom];
                WithBodyRangeBorders.LineStyle = XlLineStyle.xlContinuous;
                WithBodyRangeBorders.Weight = XlBorderWeight.xlThin;
            }
            SectorsCountPerPage = ColumnsCountPerPage - PagingRange.Column + 2;
            Range WithSubWorkingSheetRange = SubWorkingSheet.Range[SubWorkingSheet.Range["A1"], TableRange].EntireRow;
            WithSubWorkingSheetRange.Copy(WorkingSheet.Range["A1"]);
            rc = WithSubWorkingSheetRange.Rows.Count;
            cs = PagingRange.Column;
            cc = PagingRange.Columns.Count;
            WorkingSheet.Cells.Item[PagingRange.Row, cs].Resize(PagingRange.Rows.Count).Columns.Resize(Type.Missing, cc).Delete(XlDeleteShiftDirection.xlShiftToLeft);
            rs = TableRange.Row;
            rc = TableRange.Rows.Count;
            if (PagesCount > 1)
            {
                WorkingSheet.Rows.Item[rs + rc].Resize(2).EntireRow.RowHeight = 11.25; //' 15px
                WorkingSheet.Cells.Item[rs + rc, ColumnsCountPerPage].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight;
                WorkingSheet.Rows.Item[rs].Resize(rc).Copy(WorkingSheet.Rows.Item[rs + rc + 2]);
                if (PagesCount > 2)
                {
                    WorkingSheet.Rows.Item[rs + rc].Resize(2 + rc).Copy(WorkingSheet.Rows.Item[rs + rc + 2 + rc].Resize((PagesCount - 2) * (2 + rc)));
                }
            }
            tmpR = 1;
            if (isN)
            {
                for (i = 1; i <= PagesCount; i++)
                {
                    cs = (i - 1) * SectorsCountPerPage + 1;
                    if (i < PagesCount)
                    {
                        cc = SectorsCountPerPage;
                    }
                    else
                    {
                        cc = (PagingRange.Columns.Count - 1) % SectorsCountPerPage + 1;
                    }
                    tmpRange = WorkingSheet.Cells.Item[rs, PagingRange.Column].Resize(rc, cc);
                    PagingRange.Columns.Item[cs].Resize(Type.Missing, cc).Copy(tmpRange);
                    if (i < PagesCount)
                    {
                        Border WithtmpRangeBorders = tmpRange.Borders.Item[XlBordersIndex.xlEdgeRight];
                        WithtmpRangeBorders.LineStyle = XlLineStyle.xlContinuous;
                        WithtmpRangeBorders.Weight = XlBorderWeight.xlThin;
                    }

                    tmpRange = WorkingSheet.Range[WorkingSheet.Cells.Item[tmpR, 1], tmpRange];
                    if (i < PagesCount)
                    {
                        tmpRange = tmpRange.Resize[tmpRange.Rows.Count + 1];
                    }
                    tmpR = tmpR + tmpRange.Rows.Count;
                    PartsFormatRangeCol.Add(tmpRange);
                    rs = rs + rc + 2;
                }
            }
            else
            {
                RemainedSectorsCount = ItemSectorsCount;
                RemainedNA = NAColumn != null;
                RemainedIV = IVColumn != null;
                RemainedWTPopulation = HasWeight;
                RemainedWTAverage = HasWeight;
                for (i = 1; i <= PagesCount; i++)
                {
                    cs = PagingRange.Column;
                    tmpCount = SectorsCountPerPage;
                    cc = 0;
                    tmpRange = null;
                    if (RemainedSectorsCount > 0)
                    {
                        tmpRange = WorkingSheet.Cells.Item[rs, cs];
                        RemainedSectorsCount = RemainedSectorsCount - tmpCount;
                        if (RemainedSectorsCount >= 0)
                        {
                            cc = tmpCount;
                        }
                        else
                        {
                            cc = RemainedSectorsCount + tmpCount;
                            RemainedSectorsCount = 0;
                        }
                        tmpCount = tmpCount - cc;
                        tmpRange = tmpRange.Resize[rc, cc];
                        SectorColumns.Columns.Item[2].Copy(tmpRange);
                        cs = cs + cc;
                        if (RemainedSectorsCount > 0 || tmpCount <= 0 || !RemainedNA)
                        {
                            Border WithtmpRangeBorders = tmpRange.Borders.Item[XlBordersIndex.xlEdgeRight];
                            WithtmpRangeBorders.LineStyle = XlLineStyle.xlContinuous;
                            WithtmpRangeBorders.Weight = XlBorderWeight.xlThin;
                        }
                    }
                    if (tmpCount > 0)
                    {
                        if (RemainedNA)
                        {
                            tmpRange = WorkingSheet.Cells.Item[rs, cs];
                            cc = 1;
                            tmpCount = tmpCount - cc;
                            RemainedNA = false;
                            tmpRange = tmpRange.Resize[rc, cc];
                            cs = cs + cc;
                            NAColumn.Copy(tmpRange);
                        }
                    }
                    if (tmpCount > 0)
                    {
                        if (RemainedIV)
                        {
                            tmpRange = WorkingSheet.Cells.Item[rs, cs];
                            cc = 1;
                            tmpCount = tmpCount - cc;
                            RemainedIV = false;
                            tmpRange = tmpRange.Resize[rc, cc];
                            cs = cs + cc;
                            IVColumn.Copy(tmpRange);
                        }
                    }
                    if (tmpCount > 0)
                    {
                        if (RemainedWTPopulation)
                        {
                            tmpRange = WorkingSheet.Cells.Item[rs, cs];
                            if (tmpCount >= 2)
                            {
                                cc = 2;
                                RemainedWTAverage = false;
                            }
                            else
                            {
                                cc = 1;
                            }
                            tmpCount = tmpCount - cc;
                            RemainedWTPopulation = false;
                            tmpRange = tmpRange.Resize[rc, cc];
                            WTColumns.Resize[Type.Missing, cc].Copy(tmpRange);
                            if (cc == 1)
                            {
                                Border WithtmpRangeBorders = tmpRange.Borders.Item[XlBordersIndex.xlEdgeRight];
                                WithtmpRangeBorders.LineStyle = XlLineStyle.xlContinuous;
                                WithtmpRangeBorders.Weight = XlBorderWeight.xlThin;
                            }
                        }
                        else if (RemainedWTAverage)
                        {
                            tmpRange = WorkingSheet.Cells.Item[rs, cs];
                            cc = 1;
                            RemainedWTAverage = false;
                            tmpRange = tmpRange.Resize[rc, cc];
                            WTColumns.Columns.Item[2].Copy(tmpRange);
                        }
                    }
                    tmpRange = WorkingSheet.Range[WorkingSheet.Cells.Item[tmpR, 1], tmpRange];
                    if (i < PagesCount)
                    {
                        tmpRange = tmpRange.Resize[tmpRange.Rows.Count + 1];
                    }
                    tmpR = tmpR + tmpRange.Rows.Count;
                    PartsFormatRangeCol.Add(tmpRange);
                    rs = rs + rc + 2;
                }
            }
        }


        public void RankMarking(Range rng, ref Array Ranking)
        {
            int r, c;
            int clr;
            Shape o;
            for (r = Ranking.GetLowerBound(0); r <= Ranking.GetUpperBound(0); r++)
            {
                for (c = Ranking.GetLowerBound(1); c <= Ranking.GetUpperBound(1); c++)
                {
                    switch (Ranking.GetValue(r, c))
                    {
                        case 1:
                            clr = 0XFF;//red
                            break;
                        case 2:
                            clr = 0XFF0000;//blue
                            break;
                        case 3:
                            clr = 0X339966;
                            break;
                        default:
                            continue;
                    }
                    o = rng.Worksheet.Shapes.AddShape(
                        Microsoft.Office.Core.MsoAutoShapeType.msoShapeOval, rng.Item[r, c].Left + 2.25, rng.Item[r, c].Top + 2.25, 6, 6
                        );
                    o.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                    o.Fill.ForeColor.RGB = clr;
                    o.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                    o.Line.Weight = 0.75f;
                    o.Line.ForeColor.RGB = 0XFFFFFF; //white
                    COMWholeOperate.releaseComObject<Shape>(ref o);
                }
            }
        }

        public void Hatching(Range rng, ref Array HatchingColorIndex)
        {
            int r, c;
            for (r = HatchingColorIndex.GetLowerBound(0); r <= HatchingColorIndex.GetUpperBound(0); r++)
            {
                for (c = HatchingColorIndex.GetLowerBound(1); c <= HatchingColorIndex.GetUpperBound(1); c++)
                {
                    switch (HatchingColorIndex.GetValue(r, c))
                    {
                        case null:
                            break;
                        default:
                            rng.Item[r, c].Interior.Color = HatchingColorIndex.GetValue(r, c);
                            break;
                    }
                }
            }
        }

        public void AscendingMarking(Range rng, ref Array ArrowEnd)
        {
            int r, c;
            int targetR, targetC;
            double startX = 0, startY = 0;
            double endX = 0, endY = 0;
            int[] tmpArrowEnd;
            Range sCell = null;
            Range eCell;
            Line arrow;
            for (r = ArrowEnd.GetLowerBound(0); r <= ArrowEnd.GetUpperBound(0); r++)
            {
                for (c = ArrowEnd.GetLowerBound(1); c <= ArrowEnd.GetUpperBound(1); c++)
                {
                    if ((ArrowEnd.GetValue(r, c).GetType().IsArray))
                    {
                        tmpArrowEnd = (int[])ArrowEnd.GetValue(r, c);
                        targetR = tmpArrowEnd[0];
                        targetC = tmpArrowEnd[1];
                        if (targetC == c)
                        { //' 横％表
                            if (targetR != r)
                            {
                                sCell = rng.Item[r, c];
                                eCell = rng.Item[targetR, c];
                                startX = sCell.Left + sCell.Width / 4;
                                endX = startX;
                                if (targetR > r)
                                {
                                    startY = sCell.Top;
                                    endY = eCell.Top + eCell.Height;
                                }
                                else if (targetR < r)
                                {
                                    startY = eCell.Top + eCell.Height;
                                    endY = sCell.Top;
                                }
                            }
                        }
                        else if (targetR == r)
                        {    //' 縦％表
                            if (targetC != c)
                            {
                                sCell = rng.Item[r, c];
                                eCell = rng.Item[targetR, c];
                                startY = sCell.Top + sCell.Height * 3 / 4 + 1.5;
                                endY = startY;
                                if (targetC > c)
                                {
                                    startX = sCell.Left;
                                    endX = eCell.Left + eCell.Width;
                                }
                                else if (targetR < c)
                                {
                                    startX = eCell.Left + eCell.Width;
                                    endX = sCell.Left;
                                }
                            }
                        }
                        if (sCell != null)
                        {
                            arrow = (Line)rng.Worksheet.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeLineCallout1, (float)startX, (float)startY, (float)endX, (float)endY);
                            //arrow = rng.Worksheet.Lines. Add(startX, startY, endX, endY);
                            arrow.ShapeRange.Line.EndArrowheadStyle = Microsoft.Office.Core.MsoArrowheadStyle.msoArrowheadTriangle;
                            arrow.ShapeRange.Line.Weight = targetC == c ? 3 : 2;
                            arrow.ShapeRange.Line.ForeColor.RGB = 0XFF9900;
                            arrow.ShapeRange.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                            sCell = null;
                            eCell = null;
                        }
                    }
                }
            }
        }


        private void PageSetupLandscapeTable(CrossTable Table
              , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , Worksheet FormatSheet, string FormatRangeNamePrefix
              , TableType TableType, bool HasWeight
              , int ColumnsCountPerPage
              , ref Range StartCell, ref Worksheet Sheet, bool isN
              , int AxesCount, int MaxAxesCount
              , ref int RemainedRowsCount, int MaxRowsCountPerPage, double DefLineHeight, double HeaderRowHeight
              , ref Array ContentsValue, ref Array HyperlinkTargetCells
              , bool IsFirstTable, CrossTable NextTable, string TableIndex
              // , ref Errors()  ErrorStruct, ref ErrorsCount  int, ref res  MethodResult 
              , Hashtable WholeRowCol = null)
        {
            List<Range> tmpCol = null;
            Array TableValue = null;
            Array Ranking = null;
            Array HatchingColorIndex = null;
            Array ArrowEnd = null;
            Array SigTestMarking = null;
            int PagesCount = 0;
            int PageRowsCount = 0;
            Range FormattedRange;
            Range PageTableRange;
            int i;
            int sr;
            int r;
            bool f;
            int idx = 0;
            int num;
            double RemainedHeight;
            double RangeHeight;
            int RangeRowsCount;
            int tmpCount;
            int n;

            // On Error GoTo ErrHdl
            f = true;
            switch (TableType)
            {
                case TableType.NPer:
                    if (CurrentOutput.OutputNTable && CurrentOutput.PageSetupNTable
                            || CurrentOutput.OutputPerTable && CurrentOutput.PageSetupPerTable
                            || CurrentOutput.SignificanceTest && CurrentOutput.PageSetupSignificanceTestTable)
                    {
                        f = false;

                    }
                    break;
                case TableType.SignificanceTest:
                    if (CurrentOutput.OutputNTable && CurrentOutput.PageSetupNTable
                   || CurrentOutput.OutputPerTable && CurrentOutput.PageSetupPerTable)
                    {
                        f = false;
                    }
                    break;
            }
            switch (TableType)
            {
                case TableType.NPer:
                    idx = 4;
                    num = 50;
                    break;
                case TableType.N:
                    idx = 5;
                    num = 60; break;
                case TableType.Per:
                    idx = 6;
                    num = 70; break;
                case TableType.SignificanceTest:
                    idx = 0;
                    num = 80; break;
            }
            CreateTurnedLandscapeCrossArray(Table, CutRowsCol, CutColumnsCol, ref TableValue, ref Ranking, ref HatchingColorIndex,
                ref ArrowEnd, ref SigTestMarking
                                          , 2, //wt
                1 + AxesCount, HasWeight, isN
                                  , TableType, StartCell.Worksheet.Rows.Count - StartCell.Row - 1
                                  , ColumnsCountPerPage, MaxAxesCount - AxesCount
                                  , ref PagesCount, ref PageRowsCount, WholeRowCol);
            if (TableValue.GetUpperBound(0) == -1)
            { // to do
                StartCell = null;
                if (IsFirstTable)
                {
                    //Me.Application.DisplayAlerts = false;
                    Sheet.Delete();
                    Sheet = null;
                    if (f)
                    {
                    }
                }
                else
                {
                }
                if (f)
                {
                    ContentsValue.SetValue(null, Table.Index + 1, 1);
                    ContentsValue.SetValue(null, Table.Index + 1, 2);
                }
                return;
            }

            FormatTurnedLandscapeTableSub(Table, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType, HasWeight, isN
                                            , AxesCount, ColumnsCountPerPage, PagesCount, ref tmpCol, HeaderRowHeight, WholeRowCol);
            FormattedRange = WorkingSheet.Range[tmpCol[0], tmpCol[tmpCol.Count - 1]];
            sr = StartCell.Row;
            r = sr;

            if (RemainedRowsCount < MaxRowsCountPerPage)
            {
                RangeHeight = FormattedRange.Height;
                tmpCount = (int)Math.Ceiling(RangeHeight / DefLineHeight);
                if (tmpCount > RemainedRowsCount)
                {
                    Sheet.Rows.Item[r].Resize[RemainedRowsCount].Delete(XlDeleteShiftDirection.xlShiftUp);
                    RemainedRowsCount = MaxRowsCountPerPage;
                }
            }
            for (i = 0; i < tmpCol.Count(); i++)
            {
                PageTableRange = tmpCol[i];
                RemainedHeight = DefLineHeight * RemainedRowsCount;
                RangeHeight = PageTableRange.Height;
                RangeRowsCount = PageTableRange.Rows.Count;
                tmpCount = (int)Math.Ceiling(RangeHeight / DefLineHeight);
                if (tmpCount > RemainedRowsCount)
                {
                    if (RemainedRowsCount < MaxRowsCountPerPage)
                    {
                        Sheet.Rows.Item[r].Resize[RemainedRowsCount].Delete(XlDeleteShiftDirection.xlShiftUp);
                        RemainedRowsCount = MaxRowsCountPerPage;
                    }
                    n = (int)Math.Ceiling(RangeHeight / (DefLineHeight * MaxRowsCountPerPage));
                    if (n > 1)
                    {
                        Sheet.Rows.Item[Sheet.Rows.Count - (n - 1) * MaxRowsCountPerPage + 1].Resize((n - 1) * MaxRowsCountPerPage).Delete(XlDeleteShiftDirection.xlShiftUp);
                        Sheet.Rows.Item[r + (ToInt(RemainedRowsCount % MaxRowsCountPerPage == 0) & 1)].Resize[(n - 1) * MaxRowsCountPerPage].Insert(XlInsertShiftDirection.xlShiftDown);
                    }
                    RemainedRowsCount = MaxRowsCountPerPage * n;
                }
                if (tmpCount > RangeRowsCount)
                {
                    Sheet.Rows.Item[r + (ToInt(RemainedRowsCount % MaxRowsCountPerPage == 0) & 1)].Resize(tmpCount - RangeRowsCount).Delete(XlDeleteShiftDirection.xlShiftUp);
                }
                else if (tmpCount < RangeRowsCount)
                {
                    Sheet.Rows.Item[Sheet.Rows.Count - (RangeRowsCount - tmpCount) + 1].Resize[RangeRowsCount - tmpCount].Delete(XlDeleteShiftDirection.xlShiftUp);
                    Sheet.Rows.Item[r + (ToInt(RemainedRowsCount % MaxRowsCountPerPage == 0) & 1)].Resize[RangeRowsCount - tmpCount].Insert(XlInsertShiftDirection.xlShiftDown);
                }
                r = r + RangeRowsCount;
                RemainedRowsCount = RemainedRowsCount - tmpCount;
                if (RemainedRowsCount == 0) { RemainedRowsCount = MaxRowsCountPerPage; }
            }
            StartCell = Sheet.Cells.Item[sr, 1];
            OutputUtil.CopyRow(FormattedRange, StartCell);
            OutputUtil.PutValue(StartCell.Range["B2"], ref TableIndex);
            Range WithStartCellRange = StartCell.Range["B3"].Resize[TableValue.GetUpperBound(0), TableValue.GetUpperBound(1)];
            WithStartCellRange.Value = TableValue;
            if (!isN)
            {
                if (CurrentOutput.MarkingRanking && TableType != TableType.SignificanceTest) { RankMarking(WithStartCellRange.Cells, ref Ranking); }
                if (CurrentOutput.MarkingColoring && TableType != TableType.SignificanceTest) { Hatching(WithStartCellRange.Cells, ref HatchingColorIndex); }
                // if (CurrentOutput.MarkingAscending && TableType != TableType.SignificanceTest) { AscendingMarking(WithStartCellRange.Cells, ref ArrowEnd); }
                if (CurrentOutput.MarkingSignificance) { SignificanceTestMarking(WithStartCellRange.Cells, ref SigTestMarking); }
            }
            if (idx > 0)
            {
                ContentsValue.SetValue(TableIndex, Table.Index + 1, idx);
                HyperlinkTargetCells.SetValue(StartCell, Table.Index + 1, idx);
            }
            if (WithStartCellRange.Row + WithStartCellRange.Rows.Count < WithStartCellRange.Worksheet.Rows.Count)
            {
                StartCell = WithStartCellRange.Item[WithStartCellRange.Rows.Count + 1, 1].EntireRow.Range("A1");
            }
            else
            {
                StartCell = null;
                if (NextTable != null)
                {
                }
            }
            // End With
            //ExitProc:
            //    RunningProcName = OrgProcName
            //    Exit Sub
            //ErrHdl:
            //if( IsDebug ){
            //    Debug.Print RunningProcName
            //    Debug.Print Err().Number, Err().Description
            //    Stop
            //    Resume
            //}
            //    if( ResumeError ){
            //        res = RaisedError
            //        PushError Err(), Errors, ErrorsCount
            //        Resume }
            //    }else{
            //        With Err()
            //            .Raise .Number, .Source, .Description, .HelpFile, .HelpContext
            //        End With
            //    }
        }


        public void SignificanceTestMarking(Range rng, ref Array SigTestMarking)
        {
            int y, x;
            Range c;
            string buf;
            string fmt;
            for (y = SigTestMarking.GetLowerBound(0); y <= SigTestMarking.GetUpperBound(0); y++)
            {
                for (x = SigTestMarking.GetLowerBound(1); x <= SigTestMarking.GetUpperBound(1); x++)
                {
                    buf = (string)SigTestMarking.GetValue(y, x);
                    if (null != buf && buf.Length > 0)
                    {
                        c = rng.Item[y, x];
                        fmt = @"""" + buf + @"""" + c.NumberFormat;
                        c.NumberFormat = fmt;
                    }
                }
            }
        }


        public string TemplatePathIndividual(string templateName, TableOrientation Orientation = TableOrientation.Landscape)
        {
            string d;
            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, xlApp.PathSeparator);
            return OutputUtil.BuildPath(d, templateName, xlApp.PathSeparator);
        }

        public string TemplatePath(ref XlFileFormat FileFormat, TableOrientation Orientation = TableOrientation.Landscape, bool checkCross = false)
        {
            if (FileFormat == null) { FileFormat = XlFileFormat.xlOpenXMLWorkbook; }
            string d;
            string n;

            if (Orientation != TableOrientation.Portrait) { Orientation = TableOrientation.Landscape; }
            FileFormat = (XlFileFormat)CurrentOutput.ParentRequest.ExcelFileFormat;
            if (FileFormat != XlFileFormat.xlExcel8) { FileFormat = XlFileFormat.xlOpenXMLWorkbook; }
            if (checkCross)
            {
                n = CheckListCreator.TEMPLATE_NAME_GT;
            }
            else if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        n = TEMPLATE_NAME;
                    }
                    else
                    {
                        n = TRANSPOSE_TEMPLATE_NAME;
                    }
                }
                else
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        n = INDIVIDUAL_TEMPLATE_NAME;
                    }
                    else
                    {
                        n = TRANSPOSE_INDIVIDUAL_TEMPLATE_NAME;
                    }
                }
            }
            else
            {
                if (Orientation == TableOrientation.Landscape)
                {
                    n = REPORT_TEMPLATE_NAME;
                }
                else
                {
                    n = TRANSPOSE_REPORT_TEMPLATE_NAME;
                }
            }
            if (FileFormat == XlFileFormat.xlOpenXMLWorkbook) { n = n + "x"; }
            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, xlApp.PathSeparator);
            return OutputUtil.BuildPath(d, n, xlApp.PathSeparator);
        }

        public string FormatTemplatePath(TableOrientation Orientation = TableOrientation.Landscape)
        {
            string d;
            string n;
            if (Orientation != TableOrientation.Portrait) { Orientation = TableOrientation.Landscape; }
            if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        n = FORMAT_TEMPLATE_NAME;
                    }
                    else
                    {
                        n = TRANSPOSE_FORMAT_TEMPLATE_NAME;
                    }
                }
                else
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        n = INDIVIDUAL_FORMAT_TEMPLATE_NAME;
                    }
                    else
                    {
                        n = TRANSPOSE_INDIVIDUAL_FORMAT_TEMPLATE_NAME;
                    }
                }
            }
            else
            {
                if (Orientation == TableOrientation.Landscape)
                {
                    n = REPORT_FORMAT_TEMPLATE_NAME;
                }
                else
                {
                    n = TRANSPOSE_REPORT_FORMAT_TEMPLATE_NAME;
                }
            }
            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, xlApp.PathSeparator);
            return OutputUtil.BuildPath(d, n, xlApp.PathSeparator);
        }



        private static void PutContents(
                Worksheet ContentsSheet, ref Array ContentsValue
              , ref Array HyperlinkTargetCells //Excel.Range 
              , Application Application
              , List<Worksheet> OrgSheets = null)
        {
            string OrgProcName;
            int i, j;
            int r = 0;
            // Worksheet sht  ;
            Sheets shts;
            //  Object sh  ;
            string n;
            Worksheet tmpSht;
            string tmp;
            Range WithContentsSheet = ContentsSheet.Range["Contents"];
            OutputUtil.PutValue(WithContentsSheet.Cells, ref ContentsValue);
            for (i = HyperlinkTargetCells.GetLowerBound(0); i <= HyperlinkTargetCells.GetUpperBound(0); i++)
            {
                r = r + 1;
                for (j = HyperlinkTargetCells.GetLowerBound(1); j <= HyperlinkTargetCells.GetUpperBound(1); j++)
                {
                    if (HyperlinkTargetCells.GetValue(i, j) != null)
                    {
                        Range tmpRng = (Range)HyperlinkTargetCells.GetValue(i, j);
                        tmp = "'" + tmpRng.Worksheet.Name + "'!" + tmpRng.Address;
                        WithContentsSheet.Item[r, j].Hyperlinks.Add(WithContentsSheet.Item[r, j], "", tmp);
                    }
                }
            }
            if (OrgSheets != null)
            {  //  ' 1シート1クロス
                Application.DisplayAlerts = false;
                foreach (Worksheet sht in OrgSheets)
                {
                    if (sht.Name != ContentsSheet.Name) { sht.Delete(); }
                }
                shts = ContentsSheet.Parent.Sheets;
                foreach (Worksheet sh in shts)
                {
                    tmpSht = null;
                    n = sh.Name + "~2";
                    try
                    {
                        tmpSht = shts.Item[n];
                    }
                    catch (Exception ex)
                    {

                    }
                    if (tmpSht != null)
                    {
                        sh.Name = sh.Name + "~1";
                    }
                    tmpSht = null;
                }
            }
        }

        private void SaveBook(Workbook Book
              , string Prefix,
            Application xlApp,
           Workbook FormatBook,
            string Suffix = ""
              , XlFileFormat FileFormat = XlFileFormat.xlOpenXMLWorkbook)
        {
            try { FormatBook.Close(false); } catch { }
            try
            {
                WorkingBook.Close(false);
            }
            catch { }

            string ext;
            string n;
            string p;
            Worksheet sh;
            int i;
            Sheets WithBookWorksheets = Book.Worksheets;
            if (WithBookWorksheets.Count == 1)
            {
                // Book.Close(false);
                //return;
            }
            for (i = WithBookWorksheets.Count; i >= 1; i--)
            {
                sh = WithBookWorksheets.Item[i];
                if (sh.Name == "INDEX" && checkCross)
                {
                    sh.Delete();
                }
                else
                {
                    if (sh.Visible == XlSheetVisibility.xlSheetVisible)
                    {
                        if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                        {
                            xlApp.Goto(sh.Range["A1"]);
                            if (checkCross)
                            {
                                sh.Range["A1"].EntireColumn.AutoFit();
                            }
                        }
                    }
                }
            }
            if (checkList || checkCross)
            {
                sh = WithBookWorksheets[1];
                if (sh.Name == CHECK_CROSS_SHEET)
                {
                    if (!checkList)
                    {
                        XlFileFormat fmt = XlFileFormat.xlOpenXMLWorkbook;
                        string TempPath = TemplatePath(ref fmt, checkCross: true);
                        Workbook NewBook;
                        NewBook = wbs.Add(TempPath);
                        sh.Copy(After: NewBook.Worksheets[1]);
                        NewBook.Worksheets[1].Delete();
                        xlApp.Goto(NewBook.Worksheets[1].Range["A1"]);
                    }
                    else
                    {
                        sh.Copy(After: wbs[1].Worksheets[1]);
                        xlApp.Goto(wbs[1].Worksheets[1].Range["A1"]);
                    }
                }
                try { Book.Close(false); } catch { }
            }

            if (!string.IsNullOrEmpty(prfix))
            {
                Prefix = prfix + Prefix;
            }
            string OutputPath = OutputDirectoryPath;
            if (OutputPath == null)
            {
                OutputPath = Path.Combine(Path.GetTempPath(), "QC4", "output");
                GlobalMethodClass.GuaranteeDirectoryExist(OutputPath);
            }
            ext = FileFormat == XlFileFormat.xlExcel8 ? "xls" : "xlsx";
            if (OutputDirectoryPath != null)
            {
                do
                {
                    n = Prefix + (i > 0 ? (OutputDirectoryPath != null ? "_" : "") + i : "") + Suffix + "." + ext;
                    i = i + 1;
                    p = OutputUtil.BuildPath(OutputPath, n, xlApp.PathSeparator);
                } while (File.Exists(p));

                Book.CheckCompatibility = false;
                Book.BuiltinDocumentProperties["Author"] = "MACROMILL, INC.";
                Book.SaveAs(p, FileFormat, AccessMode: XlSaveAsAccessMode.xlNoChange);
                Book.Close(false);
                outputFiles.Add(p);
            }
            else
            {
                if (!checkList && !checkCross)
                {
                    if (onlySigPage)
                    {
                        int j = 0;
                        do
                        {
                            n = Prefix + (j > 0 ? "_" + j : "") +  "." + ext;
                            j = j + 1;
                            p = OutputUtil.BuildPath(OutputPath, n, xlApp.PathSeparator);
                        } while (File.Exists(p));
                        _log.Info(p);
                        Book.CheckCompatibility = false;
                        Book.SaveAs(p, FileFormat, AccessMode: XlSaveAsAccessMode.xlNoChange);
                        Book.Close(false);
                        outputFileSig.Add(p);
                    }
                    else
                    {
                        n = Prefix + "." + ext;
                        p = OutputUtil.BuildPath(OutputPath, n, xlApp.PathSeparator);
                        Book.CheckCompatibility = false;
                        Book.SaveAs(p, FileFormat, AccessMode: XlSaveAsAccessMode.xlNoChange);
                        Book.Close(false);
                        Workbook wbn = wbs.Add(p);
                        wbn.BuiltinDocumentProperties["Author"] = "MACROMILL, INC.";
                        wbn.Unprotect(BookPSWD);
                    }
                }
            }
        }


        private void selectIndexSheet(Workbook Book,
            Application xlApp)
        {
            Worksheet sh;
            int i;
            Sheets WithBookWorksheets = Book.Worksheets;
            for (i = WithBookWorksheets.Count; i >= 1; i--)
            {
                sh = WithBookWorksheets.Item[i];
                if (sh.Visible == XlSheetVisibility.xlSheetVisible)
                {
                    xlApp.Goto(sh.Range["A1"]);
                }
            }
        }


        private void CreateIndividualCross(Workbook FormatBook, XlFileFormat FileFormat, string Suffix)
        {
            Worksheet NPerFormatSheet = null;
            Worksheet NPerDoubleFormatSheet = null;
            Worksheet NFormatSheet = null;
            Worksheet NDoubleFormatSheet = null;
            Worksheet PerFormatSheet = null;
            Worksheet PerDoubleFormatSheet = null;
            Worksheet SigTestPerFormatSheet = null;
            Worksheet SigTestPerDoubleFormatSheet = null;
            Worksheet FormatSheet = null;
            bool HasWeightColumn = false;
            int m = 0;
            bool HasWeightBack;
            bool HasWeight;
            Hashtable CutRowsCol = null;
            Hashtable CutColumnsCol = null;
            string FormatRangeNamePrefix;

            CrossTable tmpTable;
            string ReportTitle;
            List<Workbook> NPerBooks = null;
            List<Workbook> NBooks = null;
            List<Workbook> PerBooks = null;
            List<Workbook> SigTestBooks = null;
            Array NPerContentsValue = null;  //string
            Array NContentsValue = null; // string
            Array PerContentsValue = null;  //string
            Array SigTestContentsValue = null; // string
            Array NPerHyperLinkTargetCells = null; // Range
            Array NHyperLinkTargetCells = null;  //Range
            Array PerHyperLinkTargetCells = null; // Range
            Array SigTestHyperLinkTargetCells = null;  //Range
            Worksheet NPerContentsSheet = null;
            Worksheet NContentsSheet = null;
            Worksheet PerContentsSheet = null;
            Worksheet SigTestContentsSheet = null;
            List<Worksheet> NPerOrgSheets = null;
            List<Worksheet> NOrgSheets = null;
            List<Worksheet> PerOrgSheets = null;
            List<Worksheet> SigTestOrgSheets = null;
            //Workbook NewBook = null;
            Worksheet sht = null;
            Array v = null;  //string
            Array DataValue = null;//  Variant
            Array Ranking = null;
            Array HatchingColorIndex = null;  //XlColorIndex
            Array ArrowEnd = null;  //Variant
            Array SigTestMarking = null; // string
            bool isN;
            string tmpPrefix;
            int i;
            bool SigTestOn = false;
            Hashtable WholeRowCol = null;
            bool CheckOverRow;
            bool CheckOverColumn;

            Array NPerOverRowsQs = null; // string
            Array NOverRowsQs = null;//string
            Array PerOverRowsQs = null;//string
            Array SigTestOverRowsQs = null;//string
            Array NPerOverColumnsQs = null;//string
            Array NOverColumnsQs = null; //string
            Array PerOverColumnsQs = null;//string
            Array SigTestOverColumnsQs = null;//string
            int NPerTablesCount;
            int NTablesCount;
            int PerTablesCount;
            int SigTestTablesCount;
            int n1 = 0;
            int n2 = 0; int n = 0;
            string errBuf;


            bool HasOutputNPerTable = false;
            bool HasOutputNTable = false;
            bool HasOutputPerTable = false;
            bool IsOrientationLandscape;
            bool IsOrientationPortrait;
            bool IsMarkingSignificance;
            bool IsMarkingRanking;
            bool IsMarkingColoring;
            bool IsMarkingAscending;
            int OverColumnsCount;
            string tableTypeBuf;
            try
            {
                if (!onlySigPage)
                {
                    HasOutputNPerTable = CurrentOutput.OutputNPerTable;
                    HasOutputNTable = CurrentOutput.OutputNTable;
                    HasOutputPerTable = CurrentOutput.OutputPerTable;
                }
                IsOrientationLandscape = CurrentOutput.Orientation == TableOrientation.Landscape;
                IsOrientationPortrait = CurrentOutput.Orientation == TableOrientation.Portrait;
                IsMarkingSignificance = CurrentOutput.MarkingSignificance;
                IsMarkingRanking = CurrentOutput.MarkingRanking;
                IsMarkingColoring = CurrentOutput.MarkingColoring;
                IsMarkingAscending = CurrentOutput.MarkingAscending;
                bool CutMedian = CurrentOutput.ParentRequest.ShowMedian & CurrentOutput.WBOn;
                // '性能対策 end
                HasWeightBack = CurrentOutput.ShowPreWBTotal;
                //Application Application = WorkingBook.Application; ;
                if ((CurrentOutput.SignificanceTestOne || CurrentOutput.SignificanceTestFive || CurrentOutput.SignificanceTestTen)
                    && (!CurrentOutput.SignificanceTestOne || !CurrentOutput.SignificanceTestFive || !CurrentOutput.SignificanceTestTen))
                {
                    SigTestOn = true;
                }
                int[] MaxAxesCountArray = new int[CurrentOutput.Tables.Count];
                for (i = 0; i <= MaxAxesCountArray.GetUpperBound(0); i++)
                {
                    tmpTable = (CrossTable)CurrentOutput.Tables[i];
                    MaxAxesCountArray[i] = GetMaxAxesCount(tmpTable);
                    m = m | MaxAxesCountArray[i];
                    if (!HasWeightColumn) { HasWeightColumn = GetHasWeight(tmpTable); }
                }
                Sheets FormatBookWorksheets = FormatBook.Worksheets;
                if (HasOutputNPerTable)
                {
                    if ((m & 2) == 2)
                    { //' 三重あり
                        NPerFormatSheet = FormatBookWorksheets.Item["NP_Std"];
                        AdjustFormat(NPerFormatSheet, null, 2, HasWeightColumn);
                    }
                    if ((m & 1) == 1)
                    { //' 二重あり
                        if ((m & 2) == 0)
                        {
                            NPerDoubleFormatSheet = FormatBookWorksheets.Item["NP_Std"];
                        }
                        else
                        {
                            NPerFormatSheet.Copy(After: NPerFormatSheet);
                            NPerDoubleFormatSheet = NPerFormatSheet.Next;
                        }
                        AdjustFormat(NPerDoubleFormatSheet, null, 1, HasWeightColumn, NPerFormatSheet != null);
                    }
                }
                if (HasOutputNTable)
                {
                    if ((m & 2) == 2)
                    {// ' 三重あり
                        NFormatSheet = FormatBookWorksheets.Item["N_Std"];
                        AdjustFormat(NFormatSheet, null, 2, HasWeightColumn);
                    }
                    if ((m & 1) == 1)
                    { //' 二重あり
                        if ((m & 2) == 0)
                        {
                            NDoubleFormatSheet = FormatBookWorksheets.Item["N_Std"];
                        }
                        else
                        {
                            NFormatSheet.Copy(After: NFormatSheet);
                            NDoubleFormatSheet = NFormatSheet.Next;
                        }
                        AdjustFormat(NDoubleFormatSheet, null, 1, HasWeightColumn, NFormatSheet != null);
                    }
                }
                if (HasOutputPerTable || SigTestOn)
                {
                    if ((m & 2) == 2)
                    { //' 三重あり
                        if (HasOutputPerTable) { PerFormatSheet = FormatBookWorksheets.Item["P_Std"]; }
                        if (SigTestOn) { SigTestPerFormatSheet = FormatBookWorksheets.Item["P_Sig"]; }
                        AdjustFormat(PerFormatSheet, SigTestPerFormatSheet, 2, HasWeightColumn);
                    }
                    if ((m & 1) == 1)
                    { //' 二重あり
                        if ((m & 2) == 0)
                        {
                            if (HasOutputPerTable) { PerDoubleFormatSheet = FormatBookWorksheets.Item["P_Std"]; }
                            if (SigTestOn) { SigTestPerDoubleFormatSheet = FormatBookWorksheets.Item["P_Sig"]; }
                        }
                        else
                        {
                            if (HasOutputPerTable)
                            {
                                PerFormatSheet.Copy(After: PerFormatSheet);
                                PerDoubleFormatSheet = PerFormatSheet.Next;
                            }
                            if (SigTestOn)
                            {
                                SigTestPerFormatSheet.Copy(After: SigTestPerFormatSheet);
                                SigTestPerDoubleFormatSheet = SigTestPerFormatSheet.Next;
                            }
                        }
                        AdjustFormat(PerDoubleFormatSheet, SigTestPerDoubleFormatSheet, 1, HasWeightColumn, PerFormatSheet != null || SigTestPerFormatSheet != null);
                    }
                }
                ReportTitle = CurrentOutput.ParentRequest.Title;
                if (HasOutputNPerTable)
                {
                    NPerOverRowsQs = new string[0];// "".Split();
                    if (IsOrientationPortrait)
                    {
                        NPerOverColumnsQs = new string[0];//"".Split();
                    }
                }
                if (HasOutputNTable)
                {
                    NOverRowsQs = new string[0];//"".Split();
                    if (IsOrientationPortrait)
                    {
                        NOverColumnsQs = new string[0];//"".Split();
                    }
                }
                if (HasOutputPerTable)
                {
                    PerOverRowsQs = new string[0];//"".Split();
                    if (IsOrientationPortrait)
                    {
                        PerOverColumnsQs = new string[0];//"".Split();
                    }
                }
                if (SigTestOn)
                {
                    SigTestOverRowsQs = new string[0];//"".Split();
                    if (IsOrientationPortrait)
                    {
                        SigTestOverColumnsQs = new string[0];//"".Split();
                    }
                }

                double progressStep = progressAvailable / CurrentOutput.Tables.Count;
                for (i = 0; i < CurrentOutput.Tables.Count; i++)
                {
                    this.ProgressBarMovement = currentProgress;
                    if (bgWorker.CancellationPending) return;
                    updateProgress(currentProgress, String.Format(LocalResource.PB_EXCEL_GEN_TABLE, (i + 1), CurrentOutput.Tables.Count));
                    currentProgress += progressStep;
                    tmpTable = (CrossTable)CurrentOutput.Tables[i];
                    isN = (tmpTable.Question.QuestionType & QuestionType.N) == QuestionType.N;
                    HasWeight = GetHasWeight(tmpTable);
                    switch (tmpTable.Question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N))
                    {
                        case QuestionType.SA:
                        case QuestionType.MA:
                            FormatRangeNamePrefix = "SA_MA";
                            break;
                        case QuestionType.N:
                            FormatRangeNamePrefix = "N";
                            break;
                        default: throw new Exception(LocalResource.REPORT_UNJUST_QUESTION_TYPE_MESSAGE);
                    }

                    if (HasWeight) { FormatRangeNamePrefix = FormatRangeNamePrefix + "_WT"; }
                    if (SigTestOn) { WholeRowCol = new Hashtable(); }
                    int medIdx = -1;
                    GetCutRowsAndColumns(tmpTable, HasWeightBack, HasWeight,
                        MaxAxesCountArray[i], ref CutRowsCol, ref CutColumnsCol, ref medIdx, false, CutMedian, WholeRowCol);
                    if (HasOutputNPerTable)
                    {
                        CreateNewSheet(ref NPerBooks, TemplatePathIndividual(INDIVIDUAL_TEMPLATE_NAME_NP), tmpTable, ref sht, ref NPerContentsSheet, ref NPerContentsValue, ref NPerHyperLinkTargetCells, ref NPerOrgSheets, false, TableType.NPer);

                        if (IsOrientationLandscape)
                        {
                            CheckOverRow = true;
                            CheckOverColumn = false;
                            OverColumnsCount = 0;
                            Hashtable WholeRowColRef = null;// only for ref 
                            int OverRowssCountTmpRef = 0; // only for ref 
                            CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                   , 2, //wt
                                   1 + MaxAxesCountArray[i], HasWeight, isN, TableType.NPer
                                    , sht.Rows.Count - 1, sht.Columns.Count - 2, ref CheckOverRow, WholeRowColRef, ref OverRowssCountTmpRef, ref OverColumnsCount);
                            if (OverColumnsCount > 0)
                            {
                                // ResumeError = true;
                                tableTypeBuf = LocalResource.REPORT_NP_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                            }
                        }
                        else
                        {
                            CheckOverRow = true;
                            CheckOverColumn = true;
                            //CreatePortraitCrossArray tmpTable, CutRowsCol, CutColumnsCol, v, DataValue, Ranking, HatchingColorIndex, ArrowEnd, SigTestMarking _
                            //                       , 1& + (HasWeight And 1&), 1& + MaxAxesCountArray(i), HasWeightColumn, HasWeight, isN _
                            //                       , TableType_NPer, sht.Rows.Count - 1&, sht.Columns.Count - 2&, CheckOverRow, CheckOverColumn
                        }
                        if (NPOICrossCreator.checkSimpleAggr(tmpTable) && !string.IsNullOrEmpty(tmpTable.Question.QNumber) && !string.IsNullOrEmpty(tmpTable.Question.TableHeading))
                        {
                            NPerContentsValue.SetValue(tmpTable.Question.QNumber, i, 1);
                            NPerContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                        }
                        else
                        {
                            NPerContentsValue.SetValue(tmpTable.Question.Name, i, 1);
                            NPerContentsValue.SetValue(tmpTable.Question.Description, i, 2);
                        }
                        if (CheckOverRow || CheckOverColumn)
                        {
                            xlApp.DisplayAlerts = false;
                            sht.Delete();
                            NPerContentsValue.SetValue("Error", i, 4);
                        }

                        if (CheckOverRow)
                        {
                            ArrayPreserve(ref NPerOverRowsQs, typeof(string), NPerOverRowsQs.GetUpperBound(0));
                            NPerOverRowsQs.SetValue("'" + tmpTable.Question.Name + "'", NPerOverRowsQs.GetUpperBound(0) - 1);
                        }
                        else if (CheckOverColumn)
                        {
                            ArrayPreserve(ref NPerOverColumnsQs, typeof(string), NPerOverColumnsQs.GetUpperBound(0));
                            NPerOverColumnsQs.SetValue("'" + tmpTable.Question.Name + "'", NPerOverColumnsQs.GetUpperBound(0) - 1);
                        }
                        else
                        {
                            if (MaxAxesCountArray[i] == 2)
                            {
                                FormatSheet = NPerFormatSheet;
                            }
                            else
                            {
                                FormatSheet = NPerDoubleFormatSheet;
                            }
                            if (IsOrientationLandscape)
                            {
                                FormatLandscapeTable(tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.NPer
                                                   , HasWeight, sht.Range["A1"], isN, NPerContentsSheet, CutMedian: CutMedian, MedIdx: medIdx);
                            }
                            else
                            {
                                //FormatPortraitTable tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType_NPer _
                                //                  , HasWeight, sht.Range("A1"), isN, NPerContentsSheet
                            }

                            if (bgWorker.CancellationPending) return;
                            Range WithshtResize = sht.Range["C2"].Resize[v.GetUpperBound(0), v.GetUpperBound(1)];
                            OutputUtil.PutValue(WithshtResize.Cells, ref v);
                            Range dataRange = WithshtResize.Worksheet.Range[WithshtResize.Item[DataValue.GetLowerBound(0),
                                DataValue.GetLowerBound(1)], WithshtResize.Item[WithshtResize.Rows.Count, WithshtResize.Columns.Count]];
                            OutputUtil.PutValue(dataRange, ref DataValue);
                            _log.Info("Auto fit started");
                            AutoFit(dataRange, colWidthMap);
                            _log.Info("Auto fit started complted");

                            _log.Info("AutoFitEx start");
                            Range labelRange = WithshtResize.Worksheet.Range[WithshtResize.Item[DataValue.GetLowerBound(0) + (isN ? 1 : 2),
                                1], WithshtResize.Item[WithshtResize.Rows.Count, DataValue.GetLowerBound(1) - 1]];
                            OutputUtil.AutoFitEx(WithshtResize.Rows.Item[1], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitEx(WithshtResize.Rows.Item[2], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitEx(WithshtResize.Rows.Item[3], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitExCrossLabel(labelRange, xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            _log.Info("AutoFitEx completed");

                            if (HasWeight)
                            {
                                if (IsOrientationLandscape)
                                {
                                    Range tmpRange = WithshtResize.Item[DataValue.GetLowerBound(0) - 1, DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1) + 1].Resize(ColumnSize: tmpTable.SectorsCount);

                                    tmpRange.Value = tmpRange.Value;
                                }
                                else
                                {
                                    Range tmpRange = WithshtResize.Item[DataValue.GetLowerBound(0) + (ToInt(HasWeightBack) & 1) + 1, DataValue.GetLowerBound(1) - 1].Resize(tmpTable.SectorsCount * (1 + (ToInt(!isN) & 1)));
                                    tmpRange.Value = tmpRange.Value;
                                }
                            }

                            if (IsOrientationPortrait)
                            {
                                if (MaxAxesCountArray[i] == 1)
                                {
                                    OutputUtil.AutoFitEx(WithshtResize.Rows.Item[5], xlApp);
                                }
                                else
                                {
                                    WithshtResize.Rows.Item[5].RowHeight = WithshtResize.Rows.Item[5].RowHeight * 2;
                                    OutputUtil.AutoFitEx(WithshtResize.Rows.Item[6], xlApp);
                                }
                            }
                            if (!isN)
                            {
                                if (IsMarkingRanking) { RankMarking(WithshtResize.Cells, ref Ranking); }
                                if (IsMarkingColoring) { Hatching(WithshtResize.Cells, ref HatchingColorIndex); }
                                //if (IsMarkingAscending) { AscendingMarking(WithshtResize.Cells, ref ArrowEnd); }
                                if (IsMarkingSignificance) { SignificanceTestMarking(WithshtResize.Cells, ref SigTestMarking); }
                            }
                            NPerContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                            NPerHyperLinkTargetCells.SetValue(sht.Range["A1"], i, 4);
                        }
                    }
                    if (HasOutputNTable)
                    {
                        CreateNewSheet(ref NBooks, TemplatePathIndividual(INDIVIDUAL_TEMPLATE_NAME_N), tmpTable, ref sht, ref NContentsSheet, ref NContentsValue, ref NHyperLinkTargetCells, ref NOrgSheets, false, TableType.N);

                        if (IsOrientationLandscape)
                        {
                            CheckOverRow = true;
                            CheckOverColumn = false;
                            OverColumnsCount = 0;
                            Hashtable WholeRowColRef = null;// only for ref 
                            int OverRowssCountTmpRef = 0; // only for ref 
                            CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                                    , 2 //wt
                                                    , 1 + MaxAxesCountArray[i], HasWeight, isN, TableType.N
                                                    , sht.Rows.Count - 1, sht.Columns.Count - 2, ref CheckOverRow, WholeRowColRef, ref OverRowssCountTmpRef, ref OverColumnsCount);
                            if (OverColumnsCount > 0)
                            {
                                //ResumeError = true
                                tableTypeBuf = LocalResource.REPORT_N_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                            }
                        }
                        else
                        {
                            CheckOverRow = true;
                            CheckOverColumn = true;
                            //CreatePortraitCrossArray(tmpTable, CutRowsCol, CutColumnsCol, v, DataValue, Ranking, HatchingColorIndex, ArrowEnd, SigTestMarking 
                            //                       , 1 + (ToInt(HasWeight) & 1), 1 + MaxAxesCountArray[i], HasWeightColumn, HasWeight, isN 
                            //                       , TableType.N, sht.Rows.Count - 1, sht.Columns.Count - 2, CheckOverRow, CheckOverColumn);
                        }
                        if (NPOICrossCreator.checkSimpleAggr(tmpTable) && !string.IsNullOrEmpty(tmpTable.Question.QNumber) && !string.IsNullOrEmpty(tmpTable.Question.TableHeading))
                        {
                            NContentsValue.SetValue(tmpTable.Question.QNumber, i, 1);
                            NContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                        }
                        else
                        {
                            NContentsValue.SetValue(tmpTable.Question.Name, i, 1);
                            NContentsValue.SetValue(tmpTable.Question.Description, i, 2);
                        }
                        if (CheckOverRow || CheckOverColumn)
                        {
                            xlApp.DisplayAlerts = false;
                            sht.Delete();
                            NContentsValue.SetValue("Error", i, 4);
                        }
                        if (CheckOverRow)
                        {
                            ArrayPreserve(ref NOverRowsQs, typeof(string), NOverRowsQs.GetUpperBound(0));
                            NOverRowsQs.SetValue("'" + tmpTable.Question.Name + "'", NOverRowsQs.GetUpperBound(0) - 1);

                        }
                        else if (CheckOverColumn)
                        {
                            ArrayPreserve(ref NOverColumnsQs, typeof(string), NOverColumnsQs.GetUpperBound(0));
                            NOverColumnsQs.SetValue("'" + tmpTable.Question.Name + "'", NOverColumnsQs.GetUpperBound(0) - 1);
                        }
                        else
                        {
                            if (MaxAxesCountArray[i] == 2)
                            {
                                FormatSheet = NFormatSheet;
                            }
                            else
                            {
                                FormatSheet = NDoubleFormatSheet;
                            }
                            if (IsOrientationLandscape)
                            {
                                FormatLandscapeTable(tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.N
                                                   , HasWeight, sht.Range["A1"], isN, NContentsSheet, CutMedian: CutMedian, MedIdx: medIdx);
                            }
                            else
                            {
                                // FormatPortraitTable(tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.N 
                                //                  , HasWeight, sht.Range["A1"], isN, NContentsSheet);
                            }

                            if (bgWorker.CancellationPending) return;
                            Range WithShtRangeResize = sht.Range["C2"].Resize[v.GetUpperBound(0), v.GetUpperBound(1)];
                            OutputUtil.PutValue(WithShtRangeResize.Cells, ref v);
                            //OutputUtil.PutValue(WithShtRangeResize.Worksheet.Range[WithShtRangeResize.Item[DataValue.GetLowerBound(0), DataValue.GetLowerBound(1)],
                            //WithShtRangeResize.Item[WithShtRangeResize.Rows.Count, WithShtRangeResize.Columns.Count]], ref DataValue);
                            Range dataRange = WithShtRangeResize.Worksheet.Range[WithShtRangeResize.Item[DataValue.GetLowerBound(0),
                                DataValue.GetLowerBound(1)], WithShtRangeResize.Item[WithShtRangeResize.Rows.Count, WithShtRangeResize.Columns.Count]];
                            OutputUtil.PutValue(dataRange, ref DataValue);
                            _log.Info("Auto fit started");
                            AutoFit(dataRange, colWidthMap);
                            _log.Info("Auto fit started complted");

                            _log.Info("AutoFitEx start");
                            Range labelRange = WithShtRangeResize.Worksheet.Range[WithShtRangeResize.Item[DataValue.GetLowerBound(0) + 1,
                                1], WithShtRangeResize.Item[WithShtRangeResize.Rows.Count, DataValue.GetLowerBound(1) - 1]];
                            OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[1], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[2], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[3], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitExCrossLabel(labelRange, xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            _log.Info("AutoFitEx completed");

                            if (HasWeight)
                            {
                                if (IsOrientationLandscape)
                                {
                                    Range tmpRange = WithShtRangeResize.Item[DataValue.GetLowerBound(0) - 1, DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1) + 1].Resize(ColumnSize: tmpTable.SectorsCount);
                                    tmpRange.Value = tmpRange.Value;
                                }
                                else
                                {
                                    Range tmpRange = WithShtRangeResize.Item[DataValue.GetLowerBound(0) + (ToInt(HasWeightBack) & 1) + 1, DataValue.GetLowerBound(1) - 1].Resize(tmpTable.SectorsCount);
                                    tmpRange.Value = tmpRange.Value;
                                }
                            }
                            if (IsOrientationPortrait)
                            {
                                if (MaxAxesCountArray[i] == 1)
                                {
                                    OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[5], xlApp);
                                }
                                else
                                {
                                    WithShtRangeResize.Rows.Item[5].RowHeight = WithShtRangeResize.Rows.Item[5].RowHeight * 2;
                                    OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[6], xlApp);
                                }
                            }
                            if (!isN)
                            {
                                if (IsMarkingRanking) { RankMarking(WithShtRangeResize.Cells, ref Ranking); }
                                if (IsMarkingColoring) { Hatching(WithShtRangeResize.Cells, ref HatchingColorIndex); }
                                //if (IsMarkingAscending) { AscendingMarking(WithShtRangeResize.Cells, ref ArrowEnd); }
                                //if (IsMarkingSignificance) { SignificanceTestMarking(WithShtRangeResize.Cells, ref SigTestMarking); }
                            }

                            NContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                            NHyperLinkTargetCells.SetValue(sht.Range["A1"], i, 4);
                        }
                    }
                    if (HasOutputPerTable)
                    {
                        CreateNewSheet(ref PerBooks, TemplatePathIndividual(INDIVIDUAL_TEMPLATE_NAME_P), tmpTable, ref sht, ref PerContentsSheet, ref PerContentsValue, ref PerHyperLinkTargetCells, ref PerOrgSheets, false, TableType.Per);

                        if (IsOrientationLandscape)
                        {
                            CheckOverRow = true;
                            CheckOverColumn = false;
                            OverColumnsCount = 0;
                            Hashtable WholeRowColRef = null;// only for ref 
                            int OverRowssCountTmpRef = 0; // only for ref 
                            CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                                    , 2 //wt
                                                    , 1 + MaxAxesCountArray[i], HasWeight, isN, TableType.Per
                                                    , sht.Rows.Count - 1, sht.Columns.Count - 2, ref CheckOverRow, WholeRowColRef, ref OverRowssCountTmpRef, ref OverColumnsCount);
                            if (OverColumnsCount > 0)
                            {
                                // ResumeError = true
                                tableTypeBuf = LocalResource.REPORT_P_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                            }
                        }
                        else
                        {
                            CheckOverRow = true;
                            CheckOverColumn = true;
                            //CreatePortraitCrossArray tmpTable, CutRowsCol, CutColumnsCol, v, DataValue, Ranking, HatchingColorIndex, ArrowEnd, SigTestMarking _
                            //                       , 1& + (HasWeight And 1&), 1& + MaxAxesCountArray(i), HasWeightColumn, HasWeight, isN _
                            //                       , TableType_Per, sht.Rows.Count - 1&, sht.Columns.Count - 2&, CheckOverRow, CheckOverColumn
                        }
                        if (NPOICrossCreator.checkSimpleAggr(tmpTable) && !string.IsNullOrEmpty(tmpTable.Question.QNumber) && !string.IsNullOrEmpty(tmpTable.Question.TableHeading))
                        {
                            PerContentsValue.SetValue(tmpTable.Question.QNumber, i, 1);
                            PerContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                        }
                        else
                        {
                            PerContentsValue.SetValue(tmpTable.Question.Name, i, 1);
                            PerContentsValue.SetValue(tmpTable.Question.Description, i, 2);
                        }
                        if (CheckOverRow || CheckOverColumn)
                        {
                            xlApp.DisplayAlerts = false;
                            sht.Delete();
                            PerContentsValue.SetValue("Error", i, 4);
                        }
                        if (CheckOverRow)
                        {
                            ArrayPreserve(ref PerOverRowsQs, typeof(string), PerOverRowsQs.GetUpperBound(0));
                            PerOverRowsQs.SetValue("'" + tmpTable.Question.Name + "'", PerOverRowsQs.GetUpperBound(0) - 1);
                        }
                        else if (CheckOverColumn)
                        {
                            ArrayPreserve(ref PerOverColumnsQs, typeof(string), PerOverColumnsQs.GetUpperBound(0));
                            PerOverColumnsQs.SetValue("'" + tmpTable.Question.Name + "'", PerOverColumnsQs.GetUpperBound(0) - 1);
                        }
                        else
                        {
                            if (MaxAxesCountArray[i] == 2)
                            {
                                FormatSheet = PerFormatSheet;
                            }
                            else
                            {
                                FormatSheet = PerDoubleFormatSheet;
                            }
                            if (IsOrientationLandscape)
                            {
                                FormatLandscapeTable(tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.Per
                                                   , HasWeight, sht.Range["A1"], isN, PerContentsSheet, CutMedian: CutMedian, MedIdx: medIdx);
                            }
                            else
                            {
                                // FormatPortraitTable tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType_Per _
                                //                   , HasWeight, sht.Range("A1"), isN, PerContentsSheet
                            }

                            if (bgWorker.CancellationPending) return;
                            Range WithShtRangeResize = sht.Range["C2"].Resize[v.GetUpperBound(0), v.GetUpperBound(1)];
                            OutputUtil.PutValue(WithShtRangeResize.Cells, ref v);
                            Range dataRange = WithShtRangeResize.Worksheet.Range[WithShtRangeResize.Item[DataValue.GetLowerBound(0),
                                DataValue.GetLowerBound(1)], WithShtRangeResize.Item[WithShtRangeResize.Rows.Count, WithShtRangeResize.Columns.Count]];
                            OutputUtil.PutValue(dataRange, ref DataValue);
                            _log.Info("Auto fit started");
                            AutoFit(dataRange, colWidthMap, "per");
                            _log.Info("Auto fit started complted");

                            _log.Info("AutoFitEx start");
                            Range labelRange = WithShtRangeResize.Worksheet.Range[WithShtRangeResize.Item[DataValue.GetLowerBound(0) + 1,
                                1], WithShtRangeResize.Item[WithShtRangeResize.Rows.Count, DataValue.GetLowerBound(1) - 1]];
                            OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[1], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[2], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[3], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitExCrossLabel(labelRange, xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            _log.Info("AutoFitEx completed");

                            if (HasWeight)
                            {
                                if (IsOrientationLandscape)
                                {
                                    Range tmpRange = WithShtRangeResize.Item[DataValue.GetLowerBound(0) - 1, DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1) + 1].Resize(ColumnSize: tmpTable.SectorsCount);
                                    tmpRange.Value = tmpRange.Value;

                                }
                                else
                                {
                                    Range tmpRange = WithShtRangeResize.Item[DataValue.GetLowerBound(0) + (ToInt(HasWeightBack) & 1) + 1, DataValue.GetLowerBound(1) - 1].Resize(tmpTable.SectorsCount);
                                    tmpRange.Value = tmpRange.Value;
                                }
                            }
                            if (IsOrientationPortrait)
                            {
                                if (MaxAxesCountArray[i] == 1)
                                {
                                    OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[5], xlApp);
                                }
                                else
                                {
                                    WithShtRangeResize.Rows.Item[5].RowHeight = WithShtRangeResize.Rows.Item[5].RowHeight * 2;
                                    OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[6], xlApp);
                                }
                            }
                            if (!isN)
                            {
                                if (IsMarkingRanking) { RankMarking(WithShtRangeResize.Cells, ref Ranking); }
                                if (IsMarkingColoring) { Hatching(WithShtRangeResize.Cells, ref HatchingColorIndex); }
                                //if (IsMarkingAscending) { AscendingMarking(WithShtRangeResize.Cells, ref ArrowEnd); }
                                if (IsMarkingSignificance) { SignificanceTestMarking(WithShtRangeResize.Cells, ref SigTestMarking); }
                            }
                            PerContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                            PerHyperLinkTargetCells.SetValue(sht.Range["A1"], i, 4);
                        }
                    }
                    //        DoEvents
                    if (SigTestOn)
                    {
                        if (NPOICrossCreator.checkSimpleAggr(tmpTable) && !isN)
                        {
                            FormatRangeNamePrefix = FormatRangeNamePrefix + "_NP";
                        }
                        CreateNewSheet(ref SigTestBooks, TemplatePathIndividual(INDIVIDUAL_TEMPLATE_NAME_T), tmpTable, ref sht, ref SigTestContentsSheet, ref SigTestContentsValue, ref SigTestHyperLinkTargetCells, ref SigTestOrgSheets, true, TableType.SignificanceTest);
                        if (IsOrientationLandscape)
                        {
                            CheckOverRow = true;
                            CheckOverColumn = false;
                            OverColumnsCount = 0;
                            int OverRowssCountTmpRef = 0; // only for ref 
                            CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd,
                                ref SigTestMarking, 2, //wt
                                1 + MaxAxesCountArray[i], HasWeight, isN, TableType.SignificanceTest
                                 , sht.Rows.Count - 1, sht.Columns.Count - 2, ref CheckOverRow, WholeRowCol, ref OverRowssCountTmpRef, ref OverColumnsCount);
                            if (OverColumnsCount > 0)
                            {
                                tableTypeBuf = LocalResource.REPORT_SIGNIFICANCE_TEST_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                            }
                        }
                        else
                        {
                            CheckOverRow = true;
                            CheckOverColumn = true;
                            //  CreatePortraitCrossArray tmpTable, CutRowsCol, CutColumnsCol, v, DataValue, Ranking, HatchingColorIndex, ArrowEnd, SigTestMarking _
                            //                          , 1& + (HasWeight And 1&), 1& + MaxAxesCountArray(i), HasWeightColumn, HasWeight, isN _
                            //                        , TableType_SignificanceTest, sht.Rows.Count - 1&, sht.Columns.Count - 2&, CheckOverRow, CheckOverColumn
                        }
                        if (NPOICrossCreator.checkSimpleAggr(tmpTable) && !string.IsNullOrEmpty(tmpTable.Question.QNumber) && !string.IsNullOrEmpty(tmpTable.Question.TableHeading))
                        {
                            SigTestContentsValue.SetValue(tmpTable.Question.QNumber, i, 1);
                            SigTestContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                        }
                        else
                        {
                            SigTestContentsValue.SetValue(tmpTable.Question.Name, i, 1);
                            SigTestContentsValue.SetValue(tmpTable.Question.Description, i, 2);
                        }
                        if (CheckOverRow || CheckOverColumn)
                        {
                            xlApp.DisplayAlerts = false;
                            sht.Delete();
                            SigTestContentsValue.SetValue("Error", i, 4);
                        }
                        if (CheckOverRow)
                        {
                            ArrayPreserve(ref SigTestOverRowsQs, typeof(string), SigTestOverRowsQs.GetUpperBound(0));
                            SigTestOverRowsQs.SetValue("'" + tmpTable.Question.Name + "'", SigTestOverRowsQs.GetUpperBound(0) - 1);
                        }
                        else if (CheckOverColumn)
                        {
                            ArrayPreserve(ref SigTestOverColumnsQs, typeof(string), SigTestOverColumnsQs.GetUpperBound(0));
                            SigTestOverColumnsQs.SetValue("'" + tmpTable.Question.Name + "'", SigTestOverColumnsQs.GetUpperBound(0) - 1);
                        }
                        else
                        {
                            if (MaxAxesCountArray[i] == 2)
                            {
                                FormatSheet = SigTestPerFormatSheet;
                            }
                            else
                            {
                                FormatSheet = SigTestPerDoubleFormatSheet;
                            }
                            if (IsOrientationLandscape)
                            {
                                FormatLandscapeTable(tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.SignificanceTest
                                                 , HasWeight, sht.Range["A1"], isN, SigTestContentsSheet, false, CutMedian, medIdx, WholeRowCol);
                            }
                            else
                            {
                                //FormatPortraitTable tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType_SignificanceTest _
                                //                  , HasWeight, sht.Range("A1"), isN, SigTestContentsSheet
                            }

                            if (bgWorker.CancellationPending) return;
                            Range WithShtRangeResize = sht.Range["C2"].Resize[v.GetUpperBound(0), v.GetUpperBound(1)];
                            OutputUtil.PutValue(WithShtRangeResize.Cells, ref v);
                            Range dataRange = WithShtRangeResize.Worksheet.Range[WithShtRangeResize.Item[DataValue.GetLowerBound(0),
                                DataValue.GetLowerBound(1)], WithShtRangeResize.Item[WithShtRangeResize.Rows.Count, WithShtRangeResize.Columns.Count]];
                            OutputUtil.PutValue(dataRange, ref DataValue);
                            _log.Info("Auto fit started");
                            //AutoFit(dataRange, colWidthMap);

                            _log.Info("AutoFitEx start");
                            Range labelRange = WithShtRangeResize.Worksheet.Range[WithShtRangeResize.Item[DataValue.GetLowerBound(0) + 1,
                                1], WithShtRangeResize.Item[WithShtRangeResize.Rows.Count, DataValue.GetLowerBound(1) - 2]];
                            OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[1], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[2], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[3], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitExCrossLabel(labelRange, xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            _log.Info("AutoFitEx completed");

                            _log.Info("Auto fit started complted");
                            if (HasWeight)
                            {
                                if (IsOrientationLandscape)
                                {
                                    Range tmpRange = WithShtRangeResize.Item[DataValue.GetLowerBound(0) - 1, DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1) + 1].Resize(ColumnSize: tmpTable.SectorsCount);
                                    tmpRange.Value = tmpRange.Value;
                                }
                                else
                                {
                                    Range tmpRange = WithShtRangeResize.Item[DataValue.GetLowerBound(0) + (ToInt(HasWeightBack) & 1) + 1, DataValue.GetLowerBound(1) - 1].Resize(tmpTable.SectorsCount * (1 + (ToInt(!isN) & 1)));
                                    tmpRange.Value = tmpRange.Value;
                                }
                            }
                            if (IsOrientationPortrait)
                            {
                                if (MaxAxesCountArray[i] == 1)
                                {
                                    OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[5], xlApp);
                                }
                                else
                                {
                                    WithShtRangeResize.Rows.Item[5].RowHeight = WithShtRangeResize.Rows.Item[5].RowHeight * 2;
                                    OutputUtil.AutoFitEx(WithShtRangeResize.Rows.Item[6], xlApp);
                                }
                            }
                            if (!isN)
                            {
                                //if (IsMarkingRanking) { RankMarking(WithShtRangeResize.Cells, ref Ranking); }
                                //if (IsMarkingColoring) { Hatching(WithShtRangeResize.Cells, ref HatchingColorIndex); }
                                //if (IsMarkingAscending) { AscendingMarking(WithShtRangeResize.Cells, ref ArrowEnd); }
                            }
                            SigTestContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                            SigTestHyperLinkTargetCells.SetValue(sht.Range["A1"], i, 4);
                        }
                    }
                    //        DoEvents
                }
                if (HasOutputNPerTable)
                {
                    n1 = NPerOverRowsQs.GetUpperBound(0);
                    if (IsOrientationPortrait)
                    {
                        n2 = NPerOverColumnsQs.GetUpperBound(0) + 1;
                    }
                    n = n1 + n2;
                    errBuf = (n1 > 0) ? LocalResource.REPORT_ROWS_COUNT_OVER_KEYWORD : ""
                    + ((n1 > 0 && n2 > 0) ? LocalResource.REPORT_AND_CONJUNCTION : "")
                    + ((n2 > 0) ? LocalResource.REPORT_COLUMNS_COUNT_OVER_KEYWORD : "");
                    if (n == CurrentOutput.Tables.Count)
                    {
                        if (NPerBooks != null)
                        {
                            for (i = NPerBooks.Count; i >= 1; i--)
                            {
                                NPerBooks[i - 1].Close(false);
                            }
                            NPerBooks = null;
                        }
                        if (NBooks != null || PerBooks != null || SigTestBooks != null)
                        {

                            throw new Exception(string.Format(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_NP_MESSAGE, errBuf));
                        }
                        else
                        {
                            throw new Exception(string.Format(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_ERROR_MESSAGE, errBuf));
                        }
                    }
                    else
                    {
                        if (n > 0)
                        {
                            if (n1 > 0)
                            {
                                throw new Exception(string.Format(LocalResource.REPORT_ROWS_COUNT_OVER_INDIVIDUAL_CROSSES_NP_MESSAGE, String.Join(" , ", NPerOverRowsQs)));
                            }
                            if (n2 > 0)
                            {
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_INDIVIDUAL_CROSSES_NP_MESSAGE, String.Join(" , ", NPerOverColumnsQs)));
                            }
                        }
                        PutContents(NPerContentsSheet, ref NPerContentsValue, ref NPerHyperLinkTargetCells, xlApp, NPerOrgSheets);
                    }
                }
                if (HasOutputNTable)
                {
                    n1 = NOverRowsQs.GetUpperBound(0);
                    if (IsOrientationPortrait)
                    {
                        n2 = NOverColumnsQs.GetUpperBound(0) + 1;
                    }
                    n = n1 + n2;
                    errBuf = (n1 > 0 ? LocalResource.REPORT_ROWS_COUNT_OVER_KEYWORD : "")
                           + (n1 > 0 && n2 > 0 ? LocalResource.REPORT_AND_CONJUNCTION : "")
                           + (n2 > 0 ? LocalResource.REPORT_COLUMNS_COUNT_OVER_KEYWORD : "");
                    if (n == CurrentOutput.Tables.Count)
                    {
                        if (NBooks != null)
                        {
                            for (i = NBooks.Count; i >= 1; i--)
                            {
                                NBooks[i - 1].Close(false);
                            }
                            NBooks = null;
                        }
                        if (NPerBooks != null || PerBooks != null || SigTestBooks != null)
                        {
                            throw new Exception(string.Format(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_NP_MESSAGE, errBuf));
                        }
                        else
                        {
                            throw new Exception(string.Format(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_ERROR_MESSAGE, errBuf));
                        }
                    }
                    else
                    {
                        if (n > 0)
                        {
                            if (n1 > 0)
                            {
                                throw new Exception(string.Format(LocalResource.REPORT_ROWS_COUNT_OVER_INDIVIDUAL_CROSSES_NP_MESSAGE, String.Join(" , ", NOverRowsQs)));
                            }
                            if (n2 > 0)
                            {
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_INDIVIDUAL_CROSSES_NP_MESSAGE, String.Join(" , ", NOverColumnsQs)));
                            }
                        }
                        PutContents(NContentsSheet, ref NContentsValue, ref NHyperLinkTargetCells, xlApp, NOrgSheets);
                    }
                }
                if (HasOutputPerTable)
                {
                    n1 = PerOverRowsQs.GetUpperBound(0);
                    if (IsOrientationPortrait)
                    {
                        n2 = PerOverColumnsQs.GetUpperBound(0) + 1;
                    }
                    n = n1 + n2;
                    errBuf = (n1 > 0 ? LocalResource.REPORT_ROWS_COUNT_OVER_KEYWORD : "")
                            + (n1 > 0 && n2 > 0 ? LocalResource.REPORT_AND_CONJUNCTION : "")
                              + (n2 > 0 ? LocalResource.REPORT_COLUMNS_COUNT_OVER_KEYWORD : "");
                    if (n == CurrentOutput.Tables.Count)
                    {
                        if (PerBooks != null)
                        {
                            for (i = PerBooks.Count; i >= 1; i--)
                            {
                                PerBooks[i - 1].Close(false);
                            }
                            PerBooks = null;
                        }
                        if (NPerBooks != null || NBooks != null || SigTestBooks != null)
                        {
                            throw new Exception(string.Format(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_P_MESSAGE,""));
                        }
                        else
                        {
                            throw new Exception(string.Format(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_ERROR_MESSAGE,""));
                        }
                    }
                    else
                    {
                        if (n > 0)
                        {
                            if (n1 > 0)
                            {
                                throw new Exception(string.Format(LocalResource.REPORT_ROWS_COUNT_OVER_INDIVIDUAL_CROSSES_P_MESSAGE,""));
                            }
                            if (n2 > 0)
                            {
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_INDIVIDUAL_CROSSES_P_MESSAGE,""));
                            }
                        }
                        PutContents(PerContentsSheet, ref PerContentsValue, ref PerHyperLinkTargetCells, xlApp, PerOrgSheets);
                    }
                }
                if (SigTestOn)
                {
                    n1 = SigTestOverRowsQs.GetUpperBound(0);
                    if (IsOrientationPortrait)
                    {
                        n2 = SigTestOverColumnsQs.GetUpperBound(0) + 1;
                    }
                    n = n1 + n2;
                    errBuf = (n1 > 0 ? LocalResource.REPORT_ROWS_COUNT_OVER_KEYWORD : "")
                           + (n1 > 0 && n2 > 0 ? LocalResource.REPORT_AND_CONJUNCTION : "")
                           + (n2 > 0 ? LocalResource.REPORT_COLUMNS_COUNT_OVER_KEYWORD : "");
                    if (n == CurrentOutput.Tables.Count)
                    {
                        if (SigTestBooks != null)
                        {
                            for (i = SigTestBooks.Count; i >= 1; i--)
                            {
                                SigTestBooks[i - 1].Close(false);
                            }
                            SigTestBooks = null;
                        }
                        if (NPerBooks != null || NBooks != null || PerBooks != null)
                        {
                            throw new Exception(string.Format(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_SIGNIFICANCE_TEST_MESSAGE,""));
                        }
                        else
                        {                            
                            throw new Exception(string.Format(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_ERROR_MESSAGE,""));
                        }
                    }
                    else
                    {
                        if (n > 0)
                        {
                            if (n1 > 0)
                            {
                                throw new Exception(string.Format(LocalResource.REPORT_ROWS_COUNT_OVER_INDIVIDUAL_CROSSES_SIGNIFICANCE_TEST_MESSAGE,""));
                            }
                            if (n2 > 0)
                            {
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_INDIVIDUAL_CROSSES_SIGNIFICANCE_TEST_MESSAGE,""));
                            }
                        }
                        PutContents(SigTestContentsSheet, ref SigTestContentsValue, ref SigTestHyperLinkTargetCells, xlApp, SigTestOrgSheets);
                    }
                }
                //if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0)
                //{
                tmpPrefix = CurrentOutput.ParentReportset.DivName + CurrentOutput.ExcelBookNamePrefix;
                if (NPerBooks != null)
                {
                    foreach (Workbook NewBook in NPerBooks)
                    {
                        selectIndexSheet(NewBook, xlApp);
                    }
                }
                if (NBooks != null)
                {
                    foreach (Workbook NewBook in NBooks)
                    {
                        selectIndexSheet(NewBook, xlApp);
                    }
                }
                if (PerBooks != null)
                {
                    foreach (Workbook NewBook in PerBooks)
                    {
                        selectIndexSheet(NewBook, xlApp);
                    }
                }
                if (SigTestBooks != null)
                {
                    foreach (Workbook NewBook in SigTestBooks)
                    {
                        selectIndexSheet(NewBook, xlApp);
                    }
                }


                if (NPerBooks != null)
                {
                    i = 0;
                    foreach (Workbook NewBook in NPerBooks)
                    {
                        i = i + 1;
                        SaveBook(NewBook, tmpPrefix + "_np", xlApp, FormatBook, Suffix + (NPerBooks.Count > 1 ? "_" + i : ""), FileFormat);
                    }
                }
                if (NBooks != null)
                {
                    i = 0;
                    foreach (Workbook NewBook in NBooks)
                    {
                        i = i + 1;
                        SaveBook(NewBook, tmpPrefix + "_n", xlApp, FormatBook, Suffix + (NBooks.Count > 1 ? "_" + i : ""), FileFormat);
                    }
                }
                if (PerBooks != null)
                {
                    i = 0;
                    foreach (Workbook NewBook in PerBooks)
                    {
                        i = i + 1;
                        SaveBook(NewBook, tmpPrefix + "_p", xlApp, FormatBook, Suffix + (PerBooks.Count > 1 ? "_" + i : ""), FileFormat);
                    }
                }
                if (SigTestBooks != null)
                {
                    i = 0;
                    foreach (Workbook NewBook in SigTestBooks)
                    {
                        i = i + 1;
                        SaveBook(NewBook, tmpPrefix + "_ps", xlApp, FormatBook, Suffix + (SigTestBooks.Count > 1 ? "_" + i : ""), FileFormat);
                    }
                }
                //}
                //   CreateIndividualCross = res;
                //      if( res == RaisedError ){ PutErrorsInformation Errors}
            }
            catch (Exception ex)
            {
                closeAllBook(NPerBooks);
                closeAllBook(NBooks);
                closeAllBook(PerBooks);
                closeAllBook(SigTestBooks);
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void closeAllBook(List<Workbook> books)
        {
            if (books != null)
            {
                foreach (Workbook NewBook in books)
                {
                    selectIndexSheet(NewBook, xlApp);
                    try
                    {
                        NewBook.Close(NewBook);
                    }
                    catch (Exception ex2) { }
                }
            }
        }

        private Workbook CreateNewSheet(ref List<Workbook> Books
              , string TempPath
              , CrossTable Table
              , ref Worksheet NewSheet
              , ref Worksheet ContentsSheet
              , ref Array ContentsValue  //string 
              , ref Array HyperlinkTargetCells  //Range 
              , ref List<Worksheet> OrgSheets
              , bool IsSigTest = false
              , TableType TableType = 0
              )
        {
            int MAX_SHEETS_COUNT = int.MaxValue;
            int MAX_SHEET_NAME_LENGTH = 31;
            int MaxAxesCount;
            Workbook wb = null;
            Worksheet TemplateSheet;
            string tmp;
            string TempSheetName;
            string ReportTitle;
            string header;
            string n;
            Object sh;
            int i;
            int MinIndex;
            int MaxIndex;
            CrossTable tmpTable;
            CrossTable tmpNextTable;


            if (CurrentOutput.Orientation == TableOrientation.Landscape)
            {
                MaxAxesCount = GetMaxAxesCount(Table);
                tmp = (MaxAxesCount == 2 ? "Triple" : "double");
                TempSheetName = tmp + (IsSigTest ? "SignificanceTest" : "Standard");
            }
            else
            {
                TempSheetName = "Template";
            }

            if (Table.Index == 0)
            {
                MaxIndex = -1;
            }
            else
            {
                MaxIndex = ContentsValue.GetUpperBound(0);
            }
            if (Table.Index > MaxIndex)
            {

                MinIndex = Table.Index;
                MaxIndex = MinIndex + MAX_SHEETS_COUNT - 1;

                if (MaxIndex >= CurrentOutput.Tables.Count - 1)
                {
                    MaxIndex = CurrentOutput.Tables.Count - 1;
                }
                else
                {

                    for (MaxIndex = MinIndex + MAX_SHEETS_COUNT - 1; MaxIndex >= MinIndex; MaxIndex--)
                    {
                        tmpTable = (CrossTable)CurrentOutput.Tables[MaxIndex];
                        tmpNextTable = (CrossTable)CurrentOutput.Tables[MaxIndex + 1];
                        if ((tmpTable.Question.QuestionType & QuestionType.MatrixChild) == 0) { break; }
                        if ((tmpNextTable.Question.QuestionType & QuestionType.MatrixChild) == 0) { break; }
                        if (tmpTable.SetNo != tmpNextTable.SetNo) { break; }
                        if (tmpTable.ParentQNo != tmpNextTable.ParentQNo) { break; }
                    }
                    if (MaxIndex < MinIndex)
                    {
                        MaxIndex = MinIndex + MAX_SHEETS_COUNT - 1;
                    }
                }
                wb = CreateNewBook(ref Books, TempPath, ref ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, ref OrgSheets, MinIndex, MaxIndex, TableType);
            }
            else
            {
                wb = Books[Books.Count - 1];
            }
            n = wb.Worksheets.Count - 2 + "(" + Table.Question.Name + ")";
            TemplateSheet = wb.Worksheets.Item[TempSheetName];
            TemplateSheet.Copy(After: wb.Worksheets.Item[wb.Worksheets.Count]);
            NewSheet = wb.Worksheets.Item[wb.Worksheets.Count];

            //i = 1;
            //while (n.Length <= MAX_SHEET_NAME_LENGTH)
            //{
            //    try
            //    {
            //        NewSheet.Name = n;
            //    }
            //    catch (Exception ex)
            //    {
            //        i = i + 1;
            //        n = Table.Question.Name + "~" + i;
            //        continue;
            //    }
            //    break;
            //}

            if (n.Length > MAX_SHEET_NAME_LENGTH)
            {
                i = 0;
                do
                {
                    i = i + 1;
                    n = "@" + i;
                    sh = null;
                    try
                    {
                        sh = wb.Sheets.Item[n];
                    }
                    catch (Exception ex)
                    { }
                } while (sh != null);
            }
            NewSheet.Name = n;
            ReportTitle = CurrentOutput.ParentRequest.Title;
            header = OutputUtil.GetAdjustedHeader(ReportTitle);
            NewSheet.Unprotect(SheetPSWD);
            NewSheet.PageSetup.CenterHeader = header;

            NewSheet.PageSetup.PaperSize = (Microsoft.Office.Interop.Excel.XlPaperSize)CurrentOutput.PaperSize;
            NewSheet.PageSetup.Orientation = (Microsoft.Office.Interop.Excel.XlPageOrientation)CurrentOutput.PaperOrientation;

            return wb;
        }


        private Workbook CreateNewBook(
                ref List<Workbook> Books
              , string TempPath
              , ref Worksheet ContentsSheet
              , ref Array ContentsValue  //string 
              , ref Array HyperlinkTargetCells  //Range 
              , ref List<Worksheet> OrgSheets
              , int MinIndex
              , int MaxIndex
              , TableType TableType
              )
        {
            Workbook wb;
            string tmp;
            //Application Application = WorkingBook.Application;
            //Application.Visible = true;
            if (Books == null) { Books = new List<Workbook>(); }
            if (Books.Count > 0)
            {
                PutContents(ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, xlApp, OrgSheets);
            }
            wb = wbs.Add(TempPath);
            wb.Unprotect(BookPSWD);

            ContentsSheet = wb.Worksheets.Item["INDEX"];
            ContentsSheet.Name = LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME;

            if (CurrentOutput.Orientation == TableOrientation.Landscape)
            {
                tmp = ((TableType & TableType.SignificanceTest) == 0 ? "SignificanceTest" : "Standard");
                xlApp.DisplayAlerts = false;
                wb.Worksheets.Item["Triple" + tmp].Delete();
                wb.Worksheets.Item["double" + tmp].Delete();
            }

            OrgSheets = new List<Worksheet>();
            foreach (Worksheet sh in wb.Worksheets)
            {
                OrgSheets.Add(sh);
            }
            AdjustContentsSheet(Books, ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, TableType, MinIndex, MaxIndex);
            Books.Add(wb);
            return wb;
        }



        public void CreateLandscapeCrossArray(CrossTable Table
              , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , ref Array TableStringValue  //string
              , ref Array DataValue  //Variant 
              , ref Array Ranking  //int
              , ref Array HatchingColorIndex  //Excel.XlColorIndex
              , ref Array ArrowEnd  //Variant
              , ref Array SigTestMarking  //string 
              , int DataOffsetRow, int DataOffsetColumn
              , bool HasWeight, bool isN
              , TableType TableType, int MaxRowsCount, int MaxColumnsCount
              , ref bool CheckOverRow  // = false 
              , Hashtable WholeRowCol // = null 
              , ref int OverRowsCount //= 0
              , ref int OverColumnsCount //= 0
              , bool IsReport = false
            )
        {
            int RowsCount = 0;
            int ColumnsCount = 0;
            int d = 0;
            int d2 = 0;
            int CaptionRowsCount = 0;
            int PreWBColumnIndex = 0;
            int x = 0;
            int y = 0;
            int r = 0;
            int c = 0;
            Array tmp = null; //string
            string tmpBuf = String.Empty;
            object buf;
            int? clr = 0;
            bool f = false;
            bool IsSigTest = false;
            TableType tType;
            int dd = 0;
            int[] tmpArrowEnd = new int[5]; ;
            DataMarking reverseSide;
            int tmpY = 0;
            int tmpR = 0;
            bool IsShowPreWBTotal = false;
            bool IsMarkingSignificance = false;
            bool IsMarkingRanking = false;
            bool IsMarkingColoring = false;
            bool IsMarkingAscending = false;
            bool IsMarkingColoringLevel2High = false;
            bool IsMarkingColoringLevel1High = false;
            bool IsMarkingColoringLevel2Low = false;
            bool IsMarkingColoringLevel1Low = false;
            int tmpLevel2HighColorIndex = 0;
            int tmpLevel1HighColorIndex = 0;
            int tmpLevel2LowColorIndex = 0;
            int tmpLevel1LowColorIndex = 0;
            int tmpRowIndexFrom = 0;
            int tmpRowIndexTo = 0;
            int tmpColumnIndexFrom = 0;
            int tmpColumnIndexTo = 0;
            string[,] tmpTableValue;
            double[,] tmpPercentValue;
            string[,] tmpSignificanceTestCharacters;
            string[,] tmpSignificanceMark;
            int[,] tmpRank;
            bool[,] tmpColoringLevel2High;
            bool[,] tmpColoringLevel1High;
            bool[,] tmpColoringLevel2Low;
            bool[,] tmpColoringLevel1Low;

            IsShowPreWBTotal = CurrentOutput.ShowPreWBTotal;
            IsMarkingSignificance = CurrentOutput.MarkingSignificance;
            IsMarkingRanking = CurrentOutput.MarkingRanking;
            IsMarkingColoring = CurrentOutput.MarkingColoring;
            IsMarkingAscending = CurrentOutput.MarkingAscending;
            IsMarkingColoringLevel2High = CurrentOutput.MarkingColoringLevel2High;
            IsMarkingColoringLevel1High = CurrentOutput.MarkingColoringLevel1High;
            IsMarkingColoringLevel2Low = CurrentOutput.MarkingColoringLevel2Low;
            IsMarkingColoringLevel1Low = CurrentOutput.MarkingColoringLevel1Low;
            tmpLevel2HighColorIndex = CurrentOutput.Level2HighColorIndex;
            tmpLevel1HighColorIndex = CurrentOutput.Level1HighColorIndex;
            tmpLevel2LowColorIndex = CurrentOutput.Level2LowColorIndex;
            tmpLevel1LowColorIndex = CurrentOutput.Level1LowColorIndex;
            //'性能対策 end

            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            GetRequiredRowsColsCountLandscape(Table, CutRowsCol, CutColumnsCol, DataOffsetRow, 0, TableType
                                            , isN, ref d, ref CaptionRowsCount, ref RowsCount, ref ColumnsCount, ref d2, WholeRowCol);
            if (RowsCount > MaxRowsCount)
            {
                if (CheckOverRow) { return; }
                OverRowsCount = RowsCount - MaxRowsCount;
                RowsCount = MaxRowsCount;
            }
            CheckOverRow = false;
            if (ColumnsCount > MaxColumnsCount)
            {
                OverColumnsCount = ColumnsCount - MaxColumnsCount;
                ColumnsCount = MaxColumnsCount;
            }
            TableStringValue = Array.CreateInstance(typeof(string), new int[] { RowsCount, ColumnsCount }, new int[] { 1, 1 });
            if ((Table.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                {
                    TableStringValue.SetValue(Table.Question.Name, 1, 1);
                    if (NPOICrossCreator.checkSimpleAggr(Table))
                    {
                        if (Table.AxesGroups.Count > 1)
                        {
                            TableStringValue.SetValue(Table.Question.TableHeading, 2, 1);
                        }
                        else
                        {
                            TableStringValue.SetValue(Table.Question.Description, 3, 2);
                        }
                    }
                    else
                    {
                        TableStringValue.SetValue(Table.Question.TableHeading, 2, 1);
                        TableStringValue.SetValue(Table.Question.Description, 3, 2);
                    }
                    TableStringValue.SetValue(Table.Question.NarrowingCondition, CaptionRowsCount + 1, 1);
                }
                else
                {
                    TableStringValue.SetValue(Table.Question.Name + " " + Table.Question.TableHeading + "\n[" + Table.Question.Description + "]", 1, 1);
                }
            }

            PreWBColumnIndex = Table.GetTableValueColumnIndexMinimum + DataOffsetColumn;
            //ReDim DataValue(CaptionRowsCount + DataOffsetRow + 1& To RowsCount, 
            //DataOffsetColumn + (IsSigTest And 1&) + 1& To ColumnsCount)
            DataValue = Array.CreateInstance(typeof(object), new int[] { RowsCount - (CaptionRowsCount + DataOffsetRow + 1) + 1, ColumnsCount - (DataOffsetColumn + (ToInt(IsSigTest) & 1) + 1) + 1 },
                new int[] { CaptionRowsCount + DataOffsetRow + 1, DataOffsetColumn + (ToInt(IsSigTest) & 1) + 1 });
            //ReDim Ranking(LBound(DataValue, 1) To UBound(DataValue, 1), LBound(DataValue, 2) To UBound(DataValue, 2))
            Ranking = Array.CreateInstance(typeof(int), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 }, new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });

            //ReDim HatchingColorIndex(LBound(DataValue, 1) To UBound(DataValue, 1), LBound(DataValue, 2) To UBound(DataValue, 2))
            HatchingColorIndex = Array.CreateInstance(typeof(int), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 },
                new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });

            //ReDim ArrowEnd(LBound(DataValue, 1) To UBound(DataValue, 1), LBound(DataValue, 2) To UBound(DataValue, 2))
            ArrowEnd = Array.CreateInstance(typeof(object), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 },
                new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });

            //ReDim SigTestMarking(LBound(DataValue, 1) To UBound(DataValue, 1), LBound(DataValue, 2) To UBound(DataValue, 2))
            SigTestMarking = Array.CreateInstance(typeof(string), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 },
                new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });

            //ReDim tmp(Table.GetTableValueColumnIndexMinimum To PreWBColumnIndex - 1&)
            tmp = Array.CreateInstance(typeof(string), new int[] { PreWBColumnIndex - 1 - Table.GetTableValueColumnIndexMinimum + 1 },
                new int[] { Table.GetTableValueColumnIndexMinimum });

            //' 行見出し
            r = CaptionRowsCount + DataOffsetRow;
            for (y = Table.GetTableValueRowIndexMinimum + DataOffsetRow; y <= Table.GetTableValueRowIndexMaximum; y++)
            {
                if (CutRowsCol.ContainsKey(y))
                {
                    if (tmp.GetValue(tmp.GetLowerBound(0)) == null)
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            tmp.SetValue(Table.TableValue(y, x, true), x);
                        }
                    }
                }
                else
                {
                    if (IsSigTest)
                    {
                        dd = WholeRowCol.ContainsKey(y) ? d : d2;
                    }
                    else
                    {
                        dd = d;
                    }
                    r = r + 1;
                    if (r + dd - 1 > RowsCount) { break; }
                    c = 0;
                    if ((tmp.GetValue(tmp.GetLowerBound(0))) == null)
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            c = c + 1;
                            TableStringValue.SetValue(Table.TableValue(y, x, true), r, c);
                        }
                    }
                    else
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            c = c + 1;
                            tmpBuf = Table.TableValue(y, x, true);
                            if (null != tmpBuf && tmpBuf.Length > 0) { tmp.SetValue(tmpBuf, x); }
                            TableStringValue.SetValue(tmp.GetValue(x), r, c);
                        }
                    }
                    if (IsSigTest)
                    {
                        x = tmp.GetUpperBound(0);
                        tmpBuf = Table.SignificanceTestCharacters(y, x);
                        if (null == tmpBuf || tmpBuf.Length == 0)
                        {
                            x = x - 1;
                            tmpBuf = Table.SignificanceTestCharacters(y, x);
                        }
                        if (null != tmpBuf && tmpBuf.Length > 0)
                        {
                            c = c + 1;
                            TableStringValue.SetValue(tmpBuf, r, c);
                        }
                    }
                    tmp = Array.CreateInstance(typeof(string), new int[] { tmp.GetUpperBound(0) - tmp.GetLowerBound(0) + 1 },
                                    new int[] { tmp.GetLowerBound(0) });
                    r = r + dd - 1;
                }
            }
            //' 列見出し
            if (IsSigTest)
            {
                if (isN)
                {
                    TableStringValue.SetValue(LocalResource.REPORT_SIGNIFICANCE_TEST_ROW_COLUMN_CAPTION, CaptionRowsCount + 1, TableStringValue.GetUpperBound(1));
                }
            }
            c = DataOffsetColumn + (ToInt(IsSigTest) & 1);
            for (x = PreWBColumnIndex; x <= Table.GetTableValueColumnIndexMaximum; x++)
            {
                if (!(CutColumnsCol.Contains(x)))
                {
                    c = c + 1;
                    if (c > ColumnsCount) { break; }
                    r = CaptionRowsCount;
                    for (y = Table.GetTableValueRowIndexMinimum;
                         y <= Table.GetTableValueRowIndexMinimum + DataOffsetRow - 1; y++)
                    {
                        r = r + 1;
                        if (y == (Table.GetTableValueRowIndexMinimum + 1) && Table.Question.HasCount) { continue; }
                        TableStringValue.SetValue(Table.TableValue(y, x, true), r, c);
                    }
                }
            }
            if (IsReport)
            {
                int c1 = DataOffsetColumn + (ToInt(IsSigTest) & 1) + 1;
                int r1 = CaptionRowsCount + 1;
                TableStringValue.SetValue(Table.TableValue(0, PreWBColumnIndex, true), r1 + 1, c1);
                TableStringValue.SetValue(null, r1, c1);
                if (IsShowPreWBTotal)
                {
                    TableStringValue.SetValue(Table.TableValue(0, PreWBColumnIndex + 1, true), r1 + 1, c1 + 1);
                    TableStringValue.SetValue(null, r1, c1 + 1);
                }
            }
            //' 性能対策 start
            tmpRowIndexFrom = Table.GetTableValueRowIndexMinimum + DataOffsetRow;
            tmpRowIndexTo = Table.GetTableValueRowIndexMaximum;
            tmpColumnIndexFrom = PreWBColumnIndex;
            tmpColumnIndexTo = Table.GetTableValueColumnIndexMaximum;
            tmpTableValue = Table.TableValueByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpPercentValue = Table.PercentValueByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpSignificanceTestCharacters =
                Table.SignificanceTestCharactersByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpSignificanceMark = Table.SignificanceMarkByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpRank = Table.RankByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel2High = Table.ColoringLevel2HighByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel1High = Table.ColoringLevel1HighByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel2Low = Table.ColoringLevel2LowByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel1Low = Table.ColoringLevel1LowByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            //' 性能対策 end
            //' データ
            r = CaptionRowsCount + DataOffsetRow;
            for (y = Table.GetTableValueRowIndexMinimum + DataOffsetRow;
                 y <= Table.GetTableValueRowIndexMaximum; y++)
            {
                if (!CutRowsCol.ContainsKey(y))
                {
                    if (IsSigTest)
                    {
                        dd = (WholeRowCol.ContainsKey(y) ? d : d2);
                    }
                    else
                    {
                        dd = d;
                    }
                    r = r + 1;
                    if (r + dd - 1 > RowsCount) { break; }
                    c = DataOffsetColumn + (ToInt(IsSigTest) & 1);
                    for (x = PreWBColumnIndex; x <= Table.GetTableValueColumnIndexMaximum; x++)
                    {
                        if (!CutColumnsCol.ContainsKey(x))
                        {
                            c = c + 1;
                            if (c > ColumnsCount) { break; }
                            f = false;
                            if (HasWeight)
                            {
                                switch (Table.GetTableValueColumnIndexMaximum - x)
                                {
                                    case 0:
                                        buf = tmpTableValue[y, x];
                                        if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                        DataValue.SetValue(buf, r + d - 1, c);
                                        if (IsSigTest)
                                        {
                                            if (!(WholeRowCol.ContainsKey(y)))
                                            {
                                                buf = tmpSignificanceTestCharacters[y, x];
                                                if (null != buf && ((string)buf).Length > 0)
                                                {
                                                    DataValue.SetValue(buf, r + d2 - 1, c);
                                                }
                                            }
                                        }
                                        else if (IsMarkingSignificance)
                                        {
                                            //SigTestMarking.SetValue(tmpSignificanceMark[y, x], r + d - 1, c);
                                        }
                                        f = true;
                                        break;
                                    case 1:
                                        buf = tmpTableValue[y, x];
                                        if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                        DataValue.SetValue(buf, r, c);
                                        f = true;
                                        break;
                                }
                            }
                            if (!f)
                            {
                                if (isN || x - PreWBColumnIndex <= (ToInt(IsShowPreWBTotal) & 1))
                                {  // ' WB前全体/全体
                                    buf = tmpTableValue[y, x];
                                    if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                    DataValue.SetValue(buf, r, c);
                                    if (isN)
                                    {
                                        if (IsSigTest)
                                        {
                                            if (!(WholeRowCol.ContainsKey(y)))
                                            {
                                                buf = tmpSignificanceTestCharacters[y, x];
                                                if (null != buf && ((string)buf).Length > 0)
                                                {
                                                    DataValue.SetValue(buf, r, DataValue.GetUpperBound(1));
                                                }
                                            }
                                        }
                                        else if (IsMarkingSignificance)
                                        {
                                            SigTestMarking.SetValue(tmpSignificanceMark[y, x], r, c);
                                        }
                                    }
                                }
                                else
                                {
                                    if (tType == TableType.Per)
                                    {
                                        DataValue.SetValue(tmpPercentValue[y, x], r, c);
                                    }
                                    else
                                    {
                                        buf = tmpTableValue[y, x];
                                        if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                        DataValue.SetValue(buf, r, c);
                                        if (tType == TableType.NPer)
                                        {
                                            DataValue.SetValue(tmpPercentValue[y, x], r + 1, c);
                                        }
                                    }
                                    if (IsSigTest)
                                    {
                                        if (!(WholeRowCol.ContainsKey(y)))
                                        {
                                            buf = tmpSignificanceTestCharacters[y, x];
                                            if (null != buf && ((string)buf).Length > 0)
                                            { DataValue.SetValue(buf, r + d2 - 1, c); }
                                        }
                                    }
                                    if (IsMarkingRanking) { Ranking.SetValue(tmpRank[y, x], r, c); }
                                    if (IsMarkingColoring)
                                    {
                                        clr = -1;
                                        if (IsMarkingColoringLevel2High)
                                        {
                                            if (tmpColoringLevel2High[y, x])
                                            {
                                                clr = tmpLevel2HighColorIndex;
                                            }
                                        }
                                        if (clr == -1)
                                        {
                                            if (IsMarkingColoringLevel1High)
                                            {
                                                if (tmpColoringLevel1High[y, x])
                                                {
                                                    clr = tmpLevel1HighColorIndex;
                                                }
                                            }
                                        }
                                        if (clr == -1)
                                        {
                                            if (IsMarkingColoringLevel2Low)
                                            {
                                                if (tmpColoringLevel2Low[y, x])
                                                {
                                                    clr = tmpLevel2LowColorIndex;
                                                }
                                            }
                                        }
                                        if (clr == -1)
                                        {
                                            if (IsMarkingColoringLevel1Low)
                                            {
                                                if (tmpColoringLevel1Low[y, x])
                                                {
                                                    clr = tmpLevel1LowColorIndex;
                                                }
                                            }
                                        }

                                        if (clr < 0)
                                            clr = null;

                                        HatchingColorIndex.SetValue(clr, r, c);
                                        if (TableType == TableType.NPer) { HatchingColorIndex.SetValue(clr, r + 1, c); }
                                        if (IsSigTest)
                                        {
                                            if (!(WholeRowCol.ContainsKey(y)))
                                            {
                                                HatchingColorIndex.SetValue(clr, r + d2 - 1, c);
                                            }
                                        }
                                    }
                                    if (IsMarkingAscending)
                                    {
                                        if (ArrowEnd.GetValue(r, c).GetType().IsArray)
                                        {
                                            if (Table.IsArrowEnd(y, x, out reverseSide))
                                            {
                                                tmpR = r + dd - 1;
                                                for (tmpY = y + 1; tmpY <= Table.GetTableValueRowIndexMaximum; tmpY++)
                                                {
                                                    if (!CutRowsCol.ContainsKey(tmpY))
                                                    {
                                                        if (IsSigTest)
                                                        {
                                                            tmpR = tmpR + (WholeRowCol.ContainsKey(tmpY) ? d : d2);
                                                        }
                                                        else
                                                        {
                                                            tmpR = tmpR + d;
                                                        }
                                                    }
                                                    if (!Table.IsArrowShaft(tmpY, x))
                                                    {
                                                        if (Table.AscendingMarking(tmpY, x) == reverseSide)
                                                        {
                                                            tmpArrowEnd.SetValue(c, 1);
                                                            if (reverseSide == DataMarking.AscendingStart)
                                                            {
                                                                tmpArrowEnd.SetValue(r, 0);
                                                                ArrowEnd.SetValue(tmpArrowEnd, tmpR, c);
                                                            }
                                                            else if (reverseSide == DataMarking.AscendingEnd)
                                                            {
                                                                tmpArrowEnd.SetValue(tmpR, 0);
                                                                ArrowEnd.SetValue(tmpArrowEnd, r, c);
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //                            ' 全体との差の検定
                                    if (IsMarkingSignificance)
                                    {
                                        SigTestMarking.SetValue(tmpSignificanceMark[y, x], r + (ToInt(tType == TableType.NPer) & 1), c);
                                    }
                                }
                            }
                        }
                    }
                    r = r + dd - 1;
                }
            }
        }



        public void FormatLandscapeTable(CrossTable Table
              , Worksheet TemplateSheet
              , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , Worksheet FormatSheet, string FormatRangeNamePrefix
              , TableType TableType, bool HasWeight
              , Range StartCell, bool isN
              , Worksheet ContentsSheet = null
              , bool IsReport = false
              , bool CutMedian = false, int MedIdx = -1
              , Hashtable WholeRowCol = null,
                        Array Ranking = null,
                                    Array TableValue = null)
        {
            bool RedrawBorder = false;
            bool HasNAColumn = false;
            bool HasIVColumn = false;
            bool HasNARow = false;
            bool HasIVRow = false;
            int d = 0;
            int d2 = 0;
            bool CutNAColumn = false;
            bool CutIVColumn = false;
            bool CutWTColumns = false;
            int ItemSectorsCount = 0;
            int[] SectorsCount = new int[2]; //int
            int c = 0;
            int r = 0;
            int i = 0;
            int n = 0;
            Range tmpRange;
            Range tmpRange2;
            Range tmpTableRows;
            Range TableRows = null;
            bool CutNA = false;
            bool CutIV = false;
            int tmp;
            int idx = 0;
            int tmpR = 0;
            int y = 0;
            int tmpY = 0;
            Array tmpBuf; //string
            string tmpAddress;
            bool f = false;
            bool IsSigTest = false;
            TableType tType;
            int dd = 0;
            Range tmpHeaderRange;
            Range rng = null;

            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            HasNAColumn = CurrentOutput.ShowNAAtItem;
            HasIVColumn = CurrentOutput.ShowIVAtItem;
            HasNARow = CurrentOutput.ShowNAAtAxis;
            HasIVRow = CurrentOutput.ShowIVAtAxis;
            d = 1 + (ToInt(!isN & TableType == TableType.NPer) & 1);
            d2 = d + (ToInt(!isN & IsSigTest) & 1);
            if (IsSigTest && NPOICrossCreator.checkSimpleAggr(Table) && !isN)
            {
                d = d + 1;
            }
            //Application Application = WorkingBook.Application;
            if (isN)
            {
                if (HasNAColumn)
                {
                    CutNAColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasIVColumn) & 1));
                }
                if (HasIVColumn)
                {
                    CutIVColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum);
                }
                if (CutMedian)
                {
                    //CutMedian = IsReport && (MedIdx >= Table.GetTableValueColumnIndexMinimum);
                    CutMedian = (MedIdx >= Table.GetTableValueColumnIndexMinimum);
                }
            }
            else
            {
                CutMedian = false;
                ItemSectorsCount = Table.SectorsCount;

                c = FormatSheet.Range[FormatRangeNamePrefix + "_SectorColumns"].Column;
                //if (c + ItemSectorsCount - 1 > TemplateSheet.Columns.Count)
                //{
                //    ItemSectorsCount = TemplateSheet.Columns.Count - c + 1;
                //    CutNAColumn = HasNAColumn;
                //    CutIVColumn = HasIVColumn;
                //    CutWTColumns = HasWeight;
                //}
                if (HasNAColumn && !CutNAColumn)
                {
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2) - (ToInt(HasIVColumn) & 1) - (Table.Question.SubTotalCnt)))
                    {
                        CutNAColumn = true;
                    }
                    else if (c + ItemSectorsCount + 1 - 1 > TemplateSheet.Columns.Count)
                    {
                        CutNAColumn = true;
                        CutIVColumn = HasIVColumn;
                        CutWTColumns = HasWeight;
                    }
                }
                if (HasIVColumn && !CutIVColumn)
                {
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2) - (Table.Question.SubTotalCnt)))
                    {
                        CutIVColumn = true;
                    }
                    else if (c + ItemSectorsCount + (ToInt(HasNAColumn && !CutNAColumn) & 1) + 1 - 1 > TemplateSheet.Columns.Count)
                    {
                        CutIVColumn = true;
                        CutWTColumns = HasWeight;
                    }
                }
                if (HasWeight && !CutWTColumns)
                {
                    if (c + ItemSectorsCount + (ToInt(HasNAColumn && !CutNAColumn) & 1) + (ToInt(HasIVColumn && !CutIVColumn) & 1) + 2 - 1 > TemplateSheet.Columns.Count)
                    {
                        CutWTColumns = true;
                    }
                }
                if (!HasWeight || CutWTColumns)
                {
                    if (!HasIVColumn || CutIVColumn)
                    {
                        if (!HasNAColumn || CutNAColumn)
                        {
                            RedrawBorder = true;
                        }
                    }
                }
            }

            //' ヘッダ
            _log.Debug("UsedRange clear start");
            WorkingSheet.UsedRange.EntireRow.Delete();
            _log.Debug("UsedRange clear complted");
            WorkingSheet.DrawingObjects().Delete();
            _log.Debug("find last column");
            Range WithFormatSheetHeader = FormatSheet.Range[FormatRangeNamePrefix + "_Header"].EntireRow;
            c = 0;
            for (i = 20; i >= 3 + ToInt(IsReport || CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi); i--)
            {
                tmpRange = WithFormatSheetHeader.Cells.Item[WithFormatSheetHeader.Count, i];
                if (((XlLineStyle)tmpRange.Borders.Item[XlBordersIndex.xlEdgeRight].LineStyle) == XlLineStyle.xlContinuous)
                {
                    c = tmpRange.Column;
                    break;
                }
            }
            _log.Debug("find last column complted");
            WithFormatSheetHeader.Copy(WorkingSheet.Range["A1"]);
            _log.Debug("Header copied");
            //strat
            int captionCnt = 5;
            if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
            {
                captionCnt--;
            }
            tmpRange = WorkingSheet.Range["A1"].Item[1 + (ToInt(!IsReport) & captionCnt), 1].EntireRow.Resize[WithFormatSheetHeader.Count - (ToInt(!IsReport) & captionCnt)];
            if (c >= 3 + ToInt(IsReport || CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi))
            {
                tmpHeaderRange = tmpRange.Worksheet.Range[tmpRange.Columns.Item[3 + ToInt(IsReport || CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)], tmpRange.Columns.Item[c]].Columns;
            }
            else
            {
                tmpHeaderRange = null;
            }
            r = WithFormatSheetHeader.Count + 1; // ' ヘッダの次行

            f = true;
            if (CutWTColumns)
            {
                c = FormatSheet.Range[FormatRangeNamePrefix + "_PopulationColumn"].Column;
                WorkingSheet.Columns.Item[c].Resize(ColumnSize: 2).Delete(XlDeleteShiftDirection.xlShiftToLeft);
                Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                WithtmpRangeBrder.Color = BORDER_COLOR;
                _log.Debug("deleted _PopulationColumn header column complted");
            }
            else if (HasWeight)
            {
                f = false;
            }
            if (CutIVColumn)
            {
                c = FormatSheet.Range[FormatRangeNamePrefix + "_InvalidColumn"].Column;
                WorkingSheet.Columns.Item[c].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                if (f)
                {
                    Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                    WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                    WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                    WithtmpRangeBrder.Color = BORDER_COLOR;
                }
                _log.Debug("deleted _InvalidColumn  header column complted");
            }
            else if (HasIVColumn)
            {
                f = false;
            }
            if (CutNAColumn)
            {
                c = FormatSheet.Range[FormatRangeNamePrefix + "_NoAnswerColumn"].Column;
                WorkingSheet.Columns.Item[c].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                if (true)
                {
                    Border WithtmpRangeBrder = tmpRange.Columns.Item[c].Borders.Item[XlBordersIndex.xlEdgeLeft];
                    WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                    WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                    WithtmpRangeBrder.Color = BORDER_COLOR;
                }
                _log.Debug("deleted _NoAnswerColumn header column complted");
            }
            else if (HasNAColumn)
            {
                f = false;
            }
            if (isN)
            {
                if (CutMedian)
                {
                    c = FormatSheet.Range[FormatRangeNamePrefix + "_MedianColumn"].Column;
                    WorkingSheet.Columns.Item[c].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    if (f)
                    {
                        Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                        WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                        WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                        WithtmpRangeBrder.Color = BORDER_COLOR;
                    }
                }
                else if (Table.ParentRequest.ShowMedian)
                {
                    f = false;
                }
            }
            else
            {
                c = FormatSheet.Range[FormatRangeNamePrefix + "_SectorColumns"].Column + 1;
                if (ItemSectorsCount != 2)
                {
                    Range WithWorkingSheet;
                    if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == FileType.Report && !onlySigPage)
                    {
                        WithWorkingSheet = WorkingSheet.Columns.Item[c];
                    }
                    else
                    {
                        WithWorkingSheet = WorkingSheet.Range[ColumnIndexToColumnLetter(c) + 5 + ":" + ColumnIndexToColumnLetter(c) + 9];
                    }
                    if (ItemSectorsCount > 2)
                    {

                        WithWorkingSheet.Copy();
                        WithWorkingSheet.Resize[ColumnSize: ItemSectorsCount - 2].Insert(XlInsertShiftDirection.xlShiftToRight);
                        xlApp.CutCopyMode = XlCutCopyMode.xlCopy;
                    }
                    else
                    {
                        WithWorkingSheet.Resize[ColumnSize: 2 - ItemSectorsCount].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                        if (f) { RedrawBorder = true; }
                    }
                }
                if (ItemSectorsCount > 2)
                {
                    Border WithtmpRangeBrder = tmpRange.Columns.Item[c].Resize[ColumnSize: ItemSectorsCount - 1].Borders.Item[XlBordersIndex.xlInsideVertical];
                    WithtmpRangeBrder.Weight = XlBorderWeight.xlHairline;
                    WithtmpRangeBrder.Color = BORDER_COLOR;
                }
                if (RedrawBorder)
                {
                    Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1 + ItemSectorsCount - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                    WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                    WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                    WithtmpRangeBrder.Color = BORDER_COLOR;
                }
                if (Table.Question.SubTotalCnt > 0)
                {
                    Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1 + ItemSectorsCount - (HasNAColumn && !CutNAColumn ? 0 : 1) - Table.Question.SubTotalCnt].Borders.Item[XlBordersIndex.xlEdgeRight];
                    WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                    WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                    WithtmpRangeBrder.Color = BORDER_COLOR;
                }
                _log.Debug("adjust _SectorColumns header column complted");
            }
            //end
            y = 2;// wt ' ヘッダの下端インデックス
            if ((CutRowsCol.ContainsKey(NPOICrossCreator.checkSimpleAggr(Table) ? y + 1 : y)))
            {
                WorkingSheet.Rows.Item[r].Resize[d].Delete(XlDeleteShiftDirection.xlShiftUp);
                r = r - d;
            }
            int starttempRow = r;
            bool firstDouble = true;
            int firstDoublerow = 0;
            int firstDoublerowCnt = 0;
            bool firstTripple = true;
            int firstTripplerow = 0;
            int firstTripplerowCnt = 0;

            for (idx = 0; idx <= Table.AxesGroups.Count - 1; idx++)
            {
                if (Table.AxesGroups[idx].Count == 1 && firstDouble || Table.AxesGroups[idx].Count == 2 && firstTripple)
                {
                    tmpRange = FormatSheet.Range[FormatRangeNamePrefix + (Table.AxesGroups[idx].Count == 1 ? "_Double" : "_Triple")].EntireRow;
                    Range WithWorkingSheetRows = WorkingSheet.Rows;
                    tmpRange.Copy(WithWorkingSheetRows.Item[r]);
                    tmpRange = WithWorkingSheetRows.Item[r].Resize[tmpRange.Count];
                    tmpTableRows = WithWorkingSheetRows.Worksheet.Range[WithWorkingSheetRows.Item[1], tmpRange];
                    if (Table.AxesGroups[idx].Count == 1)
                    {
                        firstDoublerow = r;
                        firstDoublerowCnt = tmpRange.Count;
                        firstDouble = false;
                        r = r + firstDoublerowCnt;
                    }
                    else
                    {
                        firstTripplerow = r;
                        firstTripplerowCnt = tmpRange.Count;
                        firstTripple = false;
                        r = r + firstTripplerowCnt;
                    }
                    f = true;
                    if (CutWTColumns)
                    {
                        c = FormatSheet.Range[FormatRangeNamePrefix + "_PopulationColumn"].Column;
                        tmpRange.Columns.Item[c].Resize[ColumnSize: 2].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                        Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                        WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                        WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                        WithtmpRangeBrder.Color = BORDER_COLOR;
                        _log.Debug("delete _PopulationColumn tmp rows complted");

                    }
                    else if (HasWeight)
                    {
                        f = false;
                    }
                    if (CutIVColumn)
                    {
                        c = FormatSheet.Range[FormatRangeNamePrefix + "_InvalidColumn"].Column;
                        tmpRange.Columns.Item[c].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                        if (f)
                        {
                            Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                            WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                            WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                            WithtmpRangeBrder.Color = BORDER_COLOR;
                        }
                        _log.Debug("delete _InvalidColumn tmp rows complted");

                    }
                    else if (HasIVColumn)
                    {
                        f = false;
                    }
                    if (CutNAColumn)
                    {
                        c = FormatSheet.Range[FormatRangeNamePrefix + "_NoAnswerColumn"].Column;
                        tmpRange.Columns.Item[c].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                        if (ItemSectorsCount > 1)
                        {
                            Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                            WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                            WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                            WithtmpRangeBrder.Color = BORDER_COLOR;
                        }
                        else
                        { // 168977 - handling single choice
                            Border WithtmpRangeBrder = tmpRange.Columns.Item[c].Borders.Item[XlBordersIndex.xlEdgeLeft];
                            WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                            WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                            WithtmpRangeBrder.Color = BORDER_COLOR;
                        }
                        _log.Debug("delete _NoAnswerColumn tmp rows complted");
                    }
                    else if (HasNAColumn)
                    {
                        f = false;
                    }
                    if (isN)
                    {
                        if (CutMedian)
                        {
                            c = FormatSheet.Range[FormatRangeNamePrefix + "_MedianColumn"].Column;
                            tmpRange.Columns.Item[c].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                            if (f)
                            {
                                Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                                WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                                WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                                WithtmpRangeBrder.Color = BORDER_COLOR;
                            }
                        }
                        else if (Table.ParentRequest.ShowMedian)
                        {
                            f = false;
                        }
                    }
                    else
                    {
                        c = FormatSheet.Range[FormatRangeNamePrefix + "_SectorColumns"].Column + 1;
                        if (ItemSectorsCount != 2)
                        {
                            Range WithtmpRangeColumns = tmpRange.Columns.Item[c];
                            if (ItemSectorsCount > 2)
                            {
                                WithtmpRangeColumns.Copy();
                                WithtmpRangeColumns.Resize[ColumnSize: ItemSectorsCount - 2].Insert(XlInsertShiftDirection.xlShiftToRight);
                                xlApp.CutCopyMode = XlCutCopyMode.xlCopy;
                            }
                            else
                            {
                                WithtmpRangeColumns.Resize[ColumnSize: 2 - ItemSectorsCount].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                                if (ItemSectorsCount < 2 && HasWeight)
                                {// 168977 - handling single choice
                                    Border WithtmpRangeBrder = tmpRange.Columns.Item[c + 1].Borders.Item[XlBordersIndex.xlEdgeTop];
                                    WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                                    WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                                    WithtmpRangeBrder.Color = BORDER_COLOR;
                                }
                            }
                        }
                        if (ItemSectorsCount > 2)
                        {
                            Border WithtmpRangeBrder = tmpRange.Columns.Item[c].Resize[ColumnSize: ItemSectorsCount - 1].Borders.Item[XlBordersIndex.xlInsideVertical];
                            WithtmpRangeBrder.Weight = XlBorderWeight.xlHairline;
                            WithtmpRangeBrder.Color = BORDER_COLOR;
                        }
                        if (RedrawBorder)
                        {
                            Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1 + ItemSectorsCount - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                            WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                            WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                            WithtmpRangeBrder.Color = BORDER_COLOR;
                        }

                        if (Table.Question.SubTotalCnt > 0)
                        {
                            Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1 + ItemSectorsCount - (HasNAColumn && !CutNAColumn ? 0 : 1) - Table.Question.SubTotalCnt].Borders.Item[XlBordersIndex.xlEdgeRight];
                            WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                            WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                            WithtmpRangeBrder.Color = BORDER_COLOR;
                        }
                        _log.Debug("adjust _SectorColumns tmp rows complted");

                    }
                    r++;
                }
            }
            //r--;


            for (idx = 0; idx <= Table.AxesGroups.Count - 1; idx++)
            {
                Range WithWorkingSheetRows = WorkingSheet.Rows;
                tmpRange = WithWorkingSheetRows.Item[Table.AxesGroups[idx].Count == 1 ? firstDoublerow : firstTripplerow]
                    .Resize[Table.AxesGroups[idx].Count == 1 ? firstDoublerowCnt : firstTripplerowCnt];
                tmpRange.Copy(WithWorkingSheetRows.Item[r]);
                tmpRange = WithWorkingSheetRows.Item[r].Resize[tmpRange.Count];
                tmpTableRows = WithWorkingSheetRows.Worksheet.Range[WithWorkingSheetRows.Item[1], tmpRange];
                _log.Debug("copy tmp rows complted");
                //    ' 行
                if (Table.AxesGroups[idx].Count == 1)
                { //' 二重クロス
                    y = y + 1;  //' 小計行インデックス
                    tmpR = r;
                    if (Table.AxesGroups[idx][0].SectorsCount > 0)
                    {
                        if (CutRowsCol.ContainsKey(y))
                        {
                            WorkingSheet.Rows.Item[r].Resize[d].Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                        else
                        {
                            r = r + d;
                        }
                    }

                    r = r + d2; //' 2つ目の選択肢行
                    AxesInformation tmpAxes = Table.AxesGroups[idx];
                    SectorsCount[0] = tmpAxes[0].SectorsCount;
                    y = y + SectorsCount[0];
                    if (HasNARow)
                    {
                        y = y + 1;
                        if (!(CutRowsCol.ContainsKey(y)))
                        { //' 無回答出力
                            SectorsCount[0] = SectorsCount[0] + 1;
                        }
                    }
                    if (HasIVRow)
                    {
                        y = y + 1;
                        if (!(CutRowsCol.ContainsKey(y)))
                        { // ' 非該当出力
                            SectorsCount[0] = SectorsCount[0] + 1;
                        }
                    }
                    if (SectorsCount[0] > 3)
                    {
                        Range WithWorkingSheetRowsTmp = WorkingSheet.Rows.Item[r].Resize[d2];
                        WithWorkingSheetRowsTmp.Copy();
                        WithWorkingSheetRowsTmp.Resize[(SectorsCount[0] - 3) * d2].Insert(XlInsertShiftDirection.xlShiftDown);
                        xlApp.CutCopyMode = XlCutCopyMode.xlCopy;
                    }
                    else if (SectorsCount[0] < 3)
                    {
                        WorkingSheet.Rows.Item[r].Resize[(3 - SectorsCount[0]) * d2].Delete(XlDeleteShiftDirection.xlShiftUp);
                        if (SectorsCount[0] < 2)
                        {
                            if (tmpHeaderRange != null)
                            {
                                rng = xlApp.Intersect(tmpHeaderRange.EntireColumn, tmpRange);
                                if (rng != null)
                                {
                                    Border WithtmpRangeBrder = rng.Borders.Item[XlBordersIndex.xlEdgeBottom];
                                    WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                                    WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                                    WithtmpRangeBrder.Color = BORDER_COLOR;
                                }
                            }
                        }
                    }
                    _log.Debug("adjust _SectorColumns rows complted");
                }
                else
                {   // ' 三重クロス
                    y = y + 1;
                    if ((CutRowsCol.ContainsKey(y)))
                    {
                        WorkingSheet.Rows.Item[r].Resize[d].Delete(XlDeleteShiftDirection.xlShiftUp);
                        tmpR = r;
                        tmpY = y + 1;
                    }
                    else
                    {
                        r = r + d;
                        tmpR = r - d;
                        tmpY = y;
                    }
                    tmpRange2 = WorkingSheet.Rows.Item[r + d + 3 * d2].Resize[d + 3 * d2];
                    AxesInformation tmpAxes2 = Table.AxesGroups[idx];
                    SectorsCount[0] = tmpAxes2[0].SectorsCount;
                    n = tmpAxes2[1].SectorsCount + (ToInt(HasNARow) & 1) + (ToInt(HasIVRow) & 1) + 1;
                    y = y + SectorsCount[0] * n;
                    if (HasNARow)
                    {
                        if (!(CutRowsCol.ContainsKey(y + 1)))
                        { // ' 無回答出力
                            SectorsCount[0] = SectorsCount[0] + 1;
                        }
                        else
                        {
                            CutNA = true;
                        }
                        y = y + n;
                    }
                    if (HasIVRow)
                    {
                        if (!(CutRowsCol.ContainsKey(y + 1)))
                        { // ' 非該当出力
                            SectorsCount[0] = SectorsCount[0] + 1;
                        }
                        else
                        {
                            CutIV = true;
                        }
                        y = y + n;
                    }
                    if (SectorsCount[0] < 3)
                    {
                        tmpRange2.Resize[tmpRange2.Rows.Count * (3 - SectorsCount[0])].Delete(XlDeleteShiftDirection.xlShiftUp);
                        tmpRange2 = null;
                        if (SectorsCount[0] < 2)
                        {
                            if (tmpHeaderRange != null)
                            {
                                rng = xlApp.Intersect(tmpHeaderRange.EntireColumn, tmpRange);
                                if (rng != null)
                                {
                                    Border WithtmpRangeBrder = rng.Borders.Item[XlBordersIndex.xlEdgeBottom];
                                    WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                                    WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                                    WithtmpRangeBrder.Color = BORDER_COLOR;
                                }
                            }
                        }
                    }
                    //        ' 内側(表側)の選択肢数(便宜)
                    SectorsCount[1] = n - 1;
                    if (SectorsCount[1] > 3)
                    {
                        for (i = r + 3 * d + 7 * d2; i >= r + d + d2; i = i - (d + 3 * d2))
                        {
                            if (bgWorker.CancellationPending) return;
                            WithWorkingSheetRows = WorkingSheet.Rows.Item[i].Resize[d2];
                            WithWorkingSheetRows.Copy();
                            WithWorkingSheetRows.Resize[(SectorsCount[1] - 3) * d2].Insert(XlInsertShiftDirection.xlShiftDown);
                            xlApp.CutCopyMode = XlCutCopyMode.xlCopy;
                        }
                    }
                    else if (SectorsCount[1] < 3)
                    {
                        for (i = r + 3 * d + 7 * d2; i >= r + d + d2; i = i - (d + 3 * d2))
                        {
                            WorkingSheet.Rows.Item[i].Resize[(3 - SectorsCount[1]) * d2].Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                        if (tmpHeaderRange != null)
                        {
                            rng = xlApp.Intersect(tmpHeaderRange.EntireColumn, tmpRange);
                            if (rng != null)
                            {
                                Border WithtmpRangeBrder = rng.Borders.Item[XlBordersIndex.xlEdgeBottom];
                                WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                                WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                                WithtmpRangeBrder.Color = BORDER_COLOR;
                            }
                        }
                    }
                    if (SectorsCount[0] > 3)
                    {
                        tmpRange2.Copy();
                        tmpRange2.Resize[tmpRange2.Rows.Count * (SectorsCount[0] - 3)].Insert(XlInsertShiftDirection.xlShiftDown);
                        xlApp.CutCopyMode = XlCutCopyMode.xlCopy;
                    }
                    r = r + SectorsCount[0] * (SectorsCount[1] * d2 + d);
                    f = true;
                    for (i = y - (ToInt(CutIV) & n) - (ToInt(CutNA) & n); i >= tmpY; i--)
                    {
                        tmp = i;
                        if (CutNA && !CutIV)
                        {
                            if (i > y - n)
                            {
                                tmp = i + n;
                            }
                        }
                        if (IsSigTest)
                        {
                            dd = WholeRowCol.ContainsKey(tmp) ? d : d2;
                        }
                        else
                        {
                            dd = d;
                        }
                        r = r - dd;
                        if (CutRowsCol.ContainsKey(tmp))
                        {
                            WorkingSheet.Rows.Item[r].Resize[dd].Delete(XlDeleteShiftDirection.xlShiftUp);
                            if (tmpHeaderRange != null)
                            {
                                rng = xlApp.Intersect(tmpHeaderRange.EntireColumn, WorkingSheet.Rows.Item[r - 1]);
                                if (rng != null)
                                {
                                    Border WithtmpRangeBrder = rng.Borders.Item[XlBordersIndex.xlEdgeBottom];
                                    WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                                    WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                                    WithtmpRangeBrder.Color = BORDER_COLOR;
                                }
                            }
                        }
                        else
                        {
                            f = false;
                        }
                    }
                    _log.Debug("adjust _SectorColumns rows complted");
                    if (bgWorker != null && bgWorker.CancellationPending) return;
                }
                r = tmpR + tmpRange.Rows.Count; //' 次行
                if (StartCell.Row + tmpTableRows.Count - 1 <= StartCell.Worksheet.Rows.Count)
                {
                    TableRows = tmpTableRows;
                }
                else
                {
                    if (idx == 0) { TableRows = tmpTableRows; }
                    break;
                }
            }

            WorkingSheet.Rows.Item[starttempRow].Resize[firstDoublerowCnt + firstTripplerowCnt + (firstDoublerowCnt > 0 && firstTripplerowCnt > 0 ? 2 : 1)].Delete(XlDeleteShiftDirection.xlShiftUp);

            if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi && !onlySigPage)
            {
                Range WithWorkingSheet1 = WorkingSheet.Range[CELL_RANGE_MULTI];
                WithWorkingSheet1.Merge();
            }
            else if ((Table.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                Range WithWorkingSheet1 = WorkingSheet.Range[CELL_RANGE_SINGLE_3];
                WithWorkingSheet1.Merge();
                WithWorkingSheet1 = WorkingSheet.Range[CELL_RANGE_SINGLE_4];
                WithWorkingSheet1.Merge();
            }

            n = StartCell.Row + TableRows.Count - 1;
            if (n > StartCell.Worksheet.Rows.Count)
            { // ' 行数超過
                TableRows = TableRows.Resize[TableRows.Rows.Count - (n - StartCell.Worksheet.Rows.Count)];
            }
            if (TableRows.Count == StartCell.Worksheet.Rows.Count)
            {

                OutputUtil.CopyRow(TableRows.Item[1], StartCell);
                OutputUtil.CopyRow(TableRows.Item[2].Resize(TableRows.Count - 1), StartCell.Range["A2"]);
            }
            else
            {


                //Range resizRnge = TableRows.Rows.Range["B3"].Resize[TableValue.GetUpperBound(0), TableValue.GetUpperBound(1)];
                //if (!isN)
                //{
                //    if (CurrentOutput.MarkingRanking && TableType != TableType.SignificanceTest)
                //    {
                //       _log.Info("Rank Marking start");
                //        RankMarking(resizRnge.Cells, ref Ranking);
                //        _log.Info("Rank Marking completed");
                //    }
                //}

                OutputUtil.CopyRow(TableRows.Rows, StartCell);
            }
            if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                {
                    tmpAddress = "'" + ContentsSheet.Name + "'!$A$1";
                    tmpBuf = Array.CreateInstance(typeof(string), new int[] { TableRows.Count, 1 },
                        new int[] { 1, 0 });

                    for (i = 1; i <= TableRows.Count - 1; i++)
                    {
                        tmpBuf.SetValue("", i, 0);
                    }
                    tmpBuf.SetValue(LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME, TableRows.Count, 0);
                    Range WithStartCell = StartCell.EntireRow.Range["A1"].Resize[TableRows.Count];
                    WithStartCell.Hyperlinks.Add(WithStartCell.Cells, "", tmpAddress);
                    OutputUtil.PutValue(WithStartCell.Cells, ref tmpBuf);
                    WithStartCell.Font.Size = 7;
                    WithStartCell.Borders.Item[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
                }
            }
        }

        public string ColumnIndexToColumnLetter(int colIndex)
        {
            int div = colIndex;
            string colLetter = String.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }
            return colLetter;
        }

        public static void AutoFit(Range dataRange, Dictionary<string, double> colWidthMap, string prefix = "")
        {
            foreach (Range col in dataRange.Columns)
            {
                col.AutoFit();
                string name = prefix + "_" + col.Worksheet.Name + "_" + col.Column;
                double width = 0;
                colWidthMap.TryGetValue(name, out width);
                if (width > 0 && col.ColumnWidth < width)
                {
                    col.ColumnWidth = width;
                }
                else if (col.Width < COL_MIN_WIDTH)
                {
                    col.ColumnWidth = COL_MIN_COLWIDTH;
                }
                else
                {
                    colWidthMap[name] = col.ColumnWidth;
                }
            }
        }
    }
}
