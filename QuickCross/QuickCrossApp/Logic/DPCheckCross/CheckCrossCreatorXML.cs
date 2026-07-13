using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using log4net;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.VisualBasic;
using Qc4Launcher.Logic.Cross_Report;
using Qc4Launcher.Logic.DPCheckList;
using Qc4Launcher.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Common.Constants;

namespace Qc4Launcher.Logic.DPCheckCross
{
    public class CheckCrossCreatorXML
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public OutputCross CurrentOutput = null;
        //public Worksheet WorkingSheet;
        //public Workbook WorkingBook;
        public Microsoft.Office.Interop.Excel.Application xlApp;
        //public Workbooks wbs;
        //private Sheets wss;
        public string ThisLocationCode;
        public ExecuteStaticMethod ExecuteStaticMethod = new ExecuteStaticMethod();
        private static double COL_MIN_WIDTH = 42;
        private static double COL_MIN_COLWIDTH = 8.5;
        public static double ROW_MAX_HEIGHT = 408.75;
        private static int MaxRowsCount = 1048575;
        private static int MaxColumnsCount = 16382;
        private static int SA_MA_STD_LCol = 10;
        private static int SA_MA_WT_STD_LCol = 12;
        private static int N_STD_LCol = 15;
        private static int SA_MA_SIG_LCol = 11;
        private static int SA_MA_WT_SIG_LCol = 12;
        private static int N_SIG_LCol = 16;
        //public static string TEMPLATE_NAME = "Cross.xlt";
        //public static string TRANSPOSE_TEMPLATE_NAME = "CrossPortrait.xlt";
        //public static string INDIVIDUAL_TEMPLATE_NAME = "CrossNP.xlt";
        //public static string INDIVIDUAL_TEMPLATE_NAME_NP = "Cross_np.xltx";
        //public static string INDIVIDUAL_TEMPLATE_NAME_N = "Cross_n.xltx";
        //public static string INDIVIDUAL_TEMPLATE_NAME_P = "Cross_p.xltx";
        //public static string INDIVIDUAL_TEMPLATE_NAME_T = "Cross_ps.xltx";
        //public static string TRANSPOSE_INDIVIDUAL_TEMPLATE_NAME = "CrossNPPortrait.xlt";
        //public static string REPORT_TEMPLATE_NAME = "Report.xlt";
        //public static string TRANSPOSE_REPORT_TEMPLATE_NAME = "ReportPortrait.xlt";

        //public static string FORMAT_TEMPLATE_NAME = "CrossFormat.xlt";
        //public static string TRANSPOSE_FORMAT_TEMPLATE_NAME = "CrossPortraitFormat.xlt";
        //public static string INDIVIDUAL_FORMAT_TEMPLATE_NAME = "CrossNPFormat.xlt";
        //public static string TRANSPOSE_INDIVIDUAL_FORMAT_TEMPLATE_NAME = "CrossNPPortraitFormat.xlt";
        //public static string REPORT_FORMAT_TEMPLATE_NAME = "ReportFormat.xlt";
        //public static string TRANSPOSE_REPORT_FORMAT_TEMPLATE_NAME = "ReportPortraitFormat.xlt";
        public static string CHECK_CROSS_SHEET = "Check Cross";
        //public static string CELL_RANGE_MULTI = "B3:R3";
        //public static string CELL_RANGE_SINGLE_3 = "C3:R3";
        //public static string CELL_RANGE_SINGLE_4 = "D4:R4";
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
        NPOICrossCreator nPOICrossCreator = null;
        double ProgressBarMovement = 0;


        public void CreateCheckCross(Output Output, string bookPSWD, string sheetPSWD, string outputDirectoryPath,
            string templateDirectoryPath, Microsoft.Office.Interop.Excel.Application xlAppG, BackgroundWorker bgWorker, DoWorkEventArgs bgWorkerArg,
            out double ProgressBarMovement,
            bool onlySigPageP = false, bool checkCrossP = false,
            bool checkListP = false, CrossTabulationQC QC = null, double progressAvailable = 0, double currentProgress = 0,
            CheckCrossQC CQC = null, List<int> checkCrsLnLstP = null, string qc4FileName = null, List<string> outputFiles = null,
            List<string> outputFileSig = null)
        {
            ProgressBarMovement = this.ProgressBarMovement;
            // XlFileFormat xlFmt = XlFileFormat.xlOpenXMLWorkbook;
            nPOICrossCreator = new NPOICrossCreator();
            Reportset reportset = (Reportset)Output.ParentReportset;
            CurrentOutput = (OutputCross)Output;
            nPOICrossCreator.CurrentOutput = (OutputCross)Output;
            BookPSWD = bookPSWD;
            SheetPSWD = sheetPSWD;
            OutputDirectoryPath = outputDirectoryPath;
            prfix = NPOICrossCreator.getPrefix(qc4FileName);
            TemplateDirectoryPath = templateDirectoryPath;
            this.progressAvailable = progressAvailable;
            this.currentProgress = currentProgress;
            this.QC = QC;
            this.CQC = CQC;
            string FormatBook = null;
            string baseBook = null;
            //wbs = null;
            //wss = null;
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
                string sheetName = LocalResource.REPORT_CHECK_LIST_BOOK_NAME_PREFIX;
                string path = CrossReportHelper.GetPath(outputDirectoryPath, CurrentOutput, "CheckCrossOutput", reportset, xlApp,
                                                                            qc4FileName, null, sheetName);
                //@"D:\checkcross\output\" + sheetName;
                _log.Info("Excel base book added");
             
                CrossTable tmpTable = (CrossTable)Output.Tables[0];
                Macromill.QCWeb.ReportRequest.KeyItemInformation KeyItemInfo = tmpTable.KeyItem;
                string KeyItemName = string.Empty;
                string filenameSuffix = null;
                if (KeyItemInfo != null)
                    KeyItemName = KeyItemInfo.Name;
                if (KeyItemName.Length > 0)
                {
                    string fmt = new string('0', 4);
                    filenameSuffix = "_" + KeyItemName + "_" + KeyItemInfo.SectorNumber.ToString(fmt);
                }
                if (bgWorker.CancellationPending) { return; }
                _log.Info("Excel format book added");
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi && !onlySigPage)
                {
                    using (SpreadsheetDocument document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook))
                    {
                        CreateStandardCheckCross(document,filenameSuffix);
                    }
                    ProgressBarMovement = this.ProgressBarMovement;
                }
                else
                {
                    // CreateIndividualCross(FormatBook, xlFmt, filenameSuffix);
                }
                OpenXmlHelper.SaveWorkBook(path, ("Cj_PWhxRo7Q8" + (char)2), xlApp);
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
                    //FormatBook.Close(false);
                }
                catch (Exception ex)
                {
                }
                try
                {
                    //baseBook.Close(false);
                }
                catch (Exception ex)
                {
                }
                try
                {
                    //FormatBook.Close(false);
                }
                catch (Exception ex)
                {
                }
                //COMWholeOperate.releaseComObject<Worksheet>(ref WorkingSheet);
                //COMWholeOperate.releaseComObject(ref wss);
                //COMWholeOperate.releaseComObject(ref FormatBook);
                //COMWholeOperate.releaseComObject<Workbook>(ref baseBook);
                //COMWholeOperate.releaseComObject<Workbook>(ref WorkingBook);
                //COMWholeOperate.releaseComObject<Workbooks>(ref wbs);
                GC.Collect();
            }
        }
        private void CreateStandardCheckCross(SpreadsheetDocument document, string Suffix)
        {
            _log.Info("Create standard cross");
            string NewBook = null;
            string newBookWShhets = null;
            try
            {
                int SigCutColumn = 0;
                int NPCutColumn = 0;
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
                string ContentsSheet = null;
                string TemplateSheet = null;
                string SigTestTemplateSheet = null;
                string FormatSheet = null;
                string SigTestFormatSheet = null;
                string PageSetupTemplateSheet = null;
                string PageSetupSigTestTemplateSheet = null;
                string NPerSheet = null;
                string NSheet = null;
                string PerSheet = null;
                string SigTestSheet = null;
                string PageSetupContentsSheet;
                string PageSetupNPerSheet = null;
                string PageSetupNSheet = null;
                string PageSetupPerSheet = null;
                string PageSetupSigTestSheet = null;
                string NPerStartCell = null;
                string NStartCell = null;
                string PerStartCell = null;
                string SigTestStartCell = null;
                string PageSetupNPerStartCell = null;
                string PageSetupNStartCell = null;
                string PageSetupPerStartCell = null;
                string PageSetupSigTestStartCell = null;
                string tmpSheet = null;
                string tmpPageSetupSheet = null;
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
                int RowNum = 1;
                //Dim res MethodResult
                // ErrorStruct[] Errors;
                int ErrorsCount;
                //On Error GoTo ErrHdl
                //NewBook = wbs.Add(TempPath);
                _log.Info("Template book added");
                //NewBook.Unprotect(BookPSWD);
                //NewBook.Unprotect(BookPSWD);
                CheckCrossTemplate checkCrossTemplate = new CheckCrossTemplate();
                PageSetupOn = CurrentOutput.PageSetup & CurrentOutput.Orientation == TableOrientation.Landscape;
                bool CutMedian = CurrentOutput.ParentRequest.ShowMedian & CurrentOutput.WBOn;
                if (PageSetupOn)
                {
                    //switch (CurrentOutput.PaperSize)
                    //{
                    //    case XlPaperSize.xlPaperA3:
                    //        PageSetupSheetBaseName = "A3";
                    //        break;
                    //    case XlPaperSize.xlPaperB4:
                    //        PageSetupSheetBaseName = "B4";
                    //        break;
                    //    default:
                    //        PageSetupSheetBaseName = "A4";
                    //        break;
                    //}
                    //if (CurrentOutput.PaperOrientation == XlPageOrientation.xlPortrait)
                    //{
                    //    PageSetupSheetBaseName = PageSetupSheetBaseName + "Portrait";
                    //}
                    //else
                    //{
                    //    PageSetupSheetBaseName = PageSetupSheetBaseName + "Landscape";
                    //}
                }
                if ((CurrentOutput.SignificanceTestOne || CurrentOutput.SignificanceTestFive || CurrentOutput.SignificanceTestTen)
                   && (!CurrentOutput.SignificanceTestOne || !CurrentOutput.SignificanceTestFive || !CurrentOutput.SignificanceTestTen))
                {
                    SigTestOn = true;
                }

                Hashtable tmpCol = new Hashtable();
                int[] MaxAxesCountArray = new int[CurrentOutput.Tables.Count];
                checkCrossTemplate.GenerateWorkbookPart(document.AddWorkbookPart());
                for (i = 0; i < CurrentOutput.Tables.Count; i++)
                {
                    tmpTable = (CrossTable)CurrentOutput.Tables[i];
                    MaxAxesCountArray[i] = nPOICrossCreator.GetMaxAxesCount(tmpTable);
                    if (MaxAxesCount < 2) MaxAxesCount = MaxAxesCountArray[i];
                    if (!HasWeightColumn) HasWeightColumn = nPOICrossCreator.GetHasWeight(tmpTable);
                }
               
                if (CurrentOutput.Orientation == TableOrientation.Landscape)
                {
                    tmp = MaxAxesCount == 2 ? "Triple" : "double";
                    if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                    {
                        TemplateSheet = tmp + "Standard";
                        tmpCol.Add(TemplateSheet, string.Empty);
                        if (PageSetupOn)
                        {
                          
                        }
                    }
                    if (SigTestOn)
                    {
                        SigTestTemplateSheet = tmp + "SigTest";
                        tmpCol.Add(SigTestTemplateSheet, string.Empty);
                        if (CurrentOutput.PageSetupSignificanceTestTable)
                        {
        
                        }
                    }
                }
                else
                {
                    TemplateSheet = "Template";
                    if (PageSetupOn)
                    {
                        PageSetupTemplateSheet = PageSetupSheetBaseName;
                    }
                    if (SigTestOn)
                    {
                       
                    }
                    if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                    {
                        tmpCol.Add(TemplateSheet, string.Empty);
                        if (PageSetupTemplateSheet != null)
                        {
                            tmpCol.Add(PageSetupTemplateSheet, string.Empty);
                        }
                    }
                    else
                    {
                        TemplateSheet = null;
                        PageSetupTemplateSheet = null;
                    }
                }

                           
                ReportTitle = CurrentOutput.ParentRequest.Title;
                header = OutputUtil.GetAdjustedHeader(ReportTitle);
                if (TemplateSheet != null)
                {
                    //TemplateSheet.Unprotect(SheetPSWD);
                    //TemplateSheet.PageSetup.CenterHeader = header;
                }
                if (PageSetupTemplateSheet != null)
                {
                    //PageSetupTemplateSheet.Unprotect(SheetPSWD);
                    //PageSetupTemplateSheet.PageSetup.CenterHeader = header;
                }
                if (SigTestTemplateSheet != null)
                {
                    //SigTestTemplateSheet.Unprotect(SheetPSWD);
                    //SigTestTemplateSheet.PageSetup.CenterHeader = header;
                }
                if (PageSetupOn)
                {
                    //ContentsSheet.Copy(Type.Missing, ContentsSheet);
                    PageSetupContentsSheet = ContentsSheet;
                    tmp = nPOICrossCreator.GetReportKeyword(ReportMessageIndex.ReportCrossPageSetupSheetSuffixIndex);
                    PageSetupContentsSheet = nPOICrossCreator.GetReportKeyword(ReportMessageIndex.ReportCrossContentsSheetNameIndex) + tmp;
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
                        NPerSheet = nPOICrossCreator.GetReportKeyword(ReportMessageIndex.ReportCrossNPSheetNameIndex);
                    }
                    else
                    {
                        NPerSheet = CHECK_CROSS_SHEET; // to do
                    }
                    NPerStartCell = "A1";
                    tmpSheet = NPerSheet;
                    if (PageSetupOn)
                    {
                        if (CurrentOutput.PageSetupNPerTable)
                        {
                            NPerRemainedRowsCount = MaxRowsCountPerPage;
                            PageSetupNPerSheet = PageSetupTemplateSheet;
                            PageSetupNPerSheet = NPerSheet + tmp;
                            PageSetupNPerStartCell = "A1";
                            tmpPageSetupSheet = PageSetupNPerSheet;
                        }
                    }
                }
                if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                {
                    FormatSheet = "Standard";
                }
                if (SigTestOn)
                {
                    SigTestFormatSheet = "SignificanceTest";
                }
                GenerateWorkSheet(checkCrossTemplate, document, tmpSheet, i);
                WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(document, tmpSheet);
                AdjustFormat(document, FormatSheet, null, 2, ref NPCutColumn, HasWeightColumn);
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
                    RowNum += 1;
                   
                    updateProgress(currentProgress, String.Format(LocalResource.PB_EXCEL_GEN_TABLE, (i + 1), CurrentOutput.Tables.Count));
                   
                    currentProgress += progressStep;
                    this.ProgressBarMovement = currentProgress;
                    if (bgWorker.CancellationPending) return;
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
                    //ContentsValue.SetValue(tmpTable.Question.Name, i + 1, 1);
                    //ContentsValue.SetValue(tmpTable.Question.Description, i + 1, 2);
                    if (PageSetupOn)
                    {
                       // PageSetupContentsValue.SetValue(ContentsValue.GetValue(i + 1, 1), i + 1, 1);
                       // PageSetupContentsValue.SetValue(ContentsValue.GetValue(i + 1, 2), i + 1, 2);
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
                            //Err().Raise vbObjectError + 100 &, RunningProcName, ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportUnjustQuestionTypeMessageIndex)
                            break;
                    }
                    HasWeight = nPOICrossCreator.GetHasWeight(tmpTable);
                    if (SigTestOn) WholeRowCol = new Hashtable();
                    int medIdx = -1;
                    int checkCrsLn = 0;
                    if (checkCross)
                    {
                        checkCrsLn = checkCrsLnLst[i];
                    }
                    nPOICrossCreator.GetCutRowsAndColumns(tmpTable, HasShowPreWBTotal, HasWeight, MaxAxesCount, ref CutRowsCol, ref CutColumnsCol, ref medIdx, false, CutMedian, WholeRowCol , isCheckCross : true);
                    _log.Info("GetCutRowsAndColumns completed");
                    _log.Info("OutputTable NP started");
                    OutputTable(tmpTable, worksheetPart,ref RowNum,NPCutColumn, ref NPerStartCell,document, TableType.NPer, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                                  , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupNPerStartCell
                                  , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, FormatSheet, ref NPerSheet, ref PageSetupNPerSheet
                                    , ref ContentsValue, ref HyperlinkTargetCells, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                                  , strIdx, TemplateSheet, ContentsSheet, HasWeightColumn, PageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref NPerRemainedRowsCount
                                  , DefLineHeight, CutMedian: CutMedian, medIdx: medIdx, checkCrsLn: checkCrsLn
                                  //,Errors, ErrorsCount, res
                                  );
                    //        DoEvents
                    //        ' N•\
                }
            }
            catch (Exception ex)
            {
                try
                {
                    //FormatBook.Close(NewBook);
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
        private void OutputTable(CrossTable Table, WorksheetPart worksheetPart, ref int RowNum, int NPCutColumn, ref string StartCell, SpreadsheetDocument document
      , TableType TableType, bool isN, string FormatRangeNamePrefix
      , bool HasWeight, TableOrientation Orientation, bool PageSetupOn
      , Hashtable CutRowsCol, Hashtable CutColumnsCol, int AxesCount, int MaxAxesCount
      , ref string PageSetupStartCell, bool IsFirstTable, bool NextIsLast
    , CrossTable NextTable, string FormatSheet
    , ref string Sheet, ref string PageSetupSheet, ref Array ContentsValue, ref Array HyperlinkTargetCells
    , ref Array PageSetupContentsValue, ref Array PageSetupHyperlinkTargetCells, string TableIndex
    , string TemplateSheet, string ContentsSheet, bool HasWeightColumn
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
            int i;
            int dataFirstRow = 0;
            int dataLastRow=0;
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

                nPOICrossCreator.CreateTurnedLandscapeCrossArray(Table, CutRowsCol, CutColumnsCol, ref TableValue, ref Ranking, ref HatchingColorIndex,
                     ref ArrowEnd, ref SigTestMarking
                                                   , 2 // wt
                                                   , 1 + AxesCount, HasWeight, isN
                                                    , TableType, MaxRowsCount
                                                   , MaxColumnsCount, MaxAxesCount - AxesCount
                                                   , ref PagesCount, ref PageRowsCount, WholeRowCol);
                _log.Info("CreateTurnedLandscapeCrossArray completed");

                CheckOverRow = TableValue.Length == 0;
            }
            else
            {
                CheckOverRow = true;
                CheckOverColumn = true;
            }
            if (CheckOverRow || CheckOverColumn)
            {
                StartCell = null;
                PageSetupStartCell = null;
                if (IsFirstTable)
                {
                    Sheet = null;
                    if (PageSetupSheet != null)
                    {
                        // PageSetupSheet.Delete();
                        PageSetupSheet = null;
                    }
                    if (f)
                    {
                        if (CheckOverRow)
                        {
                            //Err().Raise vbObjectError + 100 &, RunningProcName _
                            //           , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex)
                        }
                        else
                        {
                            //Err().Raise vbObjectError + 101 &, RunningProcName _
                            //           , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverAtOneTableFailedMessageIndex)
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
                                //    Err().Raise vbObjectError + 150 &, RunningProcName _
                                //               , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNPMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 151 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailNPWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //   Err().Raise vbObjectError + 150 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNPOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 151 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailNPOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                    case TableType.N:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                                //  Err().Raise vbObjectError + 160 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //  Err().Raise vbObjectError + 161 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailNWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //   Err().Raise vbObjectError + 160 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 161 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailNOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                    case TableType.Per:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                                // Err().Raise vbObjectError + 170 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailPMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                // Err().Raise vbObjectError + 171 &, RunningProcName _
                                //            , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailPWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //  Err().Raise vbObjectError + 170 &, RunningProcName _
                                //            , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailPOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //  Err().Raise vbObjectError + 171 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailPOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                    case TableType.SignificanceTest:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                                //   Err().Raise vbObjectError + 180 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 181 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailSignificanceTestWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //  Err().Raise vbObjectError + 180 &, RunningProcName _
                                //           , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 181 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailSignificanceTestOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                }
                if (f)
                {
                    
                }
            }

            else
            {
                if (Orientation == TableOrientation.Landscape)
                {
                    dataFirstRow = RowNum;
                    FormatLandscapeTable(Table, ref document, worksheetPart, MaxAxesCount, StartCell, ref RowNum, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType
                                        , HasWeight, StartCell, isN, NPCutColumn, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: TableValue, CutMedian: CutMedian, MedIdx: medIdx);
                    _log.Info("FormatTurnedLandscapeTable completed");
                    if (checkCross)
                    {
                        OpenXmlHelper.PutValueToSingleCell(worksheetPart, dataFirstRow + 3, 1, checkCrsLn.ToString());
                        OpenXmlHelper.AutoFitColumn(document.WorkbookPart, worksheetPart, 1, 1, dataFirstRow + 3, dataFirstRow + 3, checkCross);
                    }
                    OpenXmlHelper.PutValueToSingleCell(worksheetPart, dataFirstRow, 2, TableIndex);
                    int row = Information.LBound(TableValue, 1) + dataFirstRow;
                    int fCol = Information.LBound(TableValue, 2) + 1;
                    CrossReportHelper.PutValue(worksheetPart, row, fCol, ref TableValue);
                    Row colrows;
                    colrows = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    OpenXmlHelper.AutofitRow(worksheetPart, colrows, fCol, 18,true);


                    row += 2;
                    int lstcolumn = Information.UBound(TableValue, 2) + 1;
                    colrows = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    for (int col = 4; col <= lstcolumn; col++)
                    {
                        OpenXmlHelper.AutofitRow(worksheetPart, colrows, col, col);
                    }
                    //int mfrow = Information.LBound(TableValue, 1) + (6+dataFirstRow);
                    //int lrow = Information.UBound(TableValue, 1);
                    int add = isN ? 3 : 4;
                    row += add;
                    int lstRow = (int)worksheetPart.Worksheet.Descendants<Row>().LastOrDefault().RowIndex.Value - 1;
                    

                    

                    //row Height autoadjustnment as per word height
                    for (int cnt = row; cnt <= lstRow; cnt++)
                    {
                        Row mgrow = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)cnt);
                        OpenXmlHelper.AutofitRow(worksheetPart, mgrow, 3, 3);
                    }

                    
                }
            }
        }
        public void AdjustFormat(SpreadsheetDocument document,
           string FormatSheet, string SigTestFormatSheet
          , int MaxAxesCount, ref int CutColumn, bool HasWeightColumn, bool OnlyCutTriple = false, bool ExtendRowHeight = false)
        {
            string[] tmpNamesArray;
            string tmpName;
            int tmp;
            string Suffix;
            string tmpSuffix;
            string fmt = null;
            bool IsPortrait;
            //XlDeleteShiftDirection tmpDelShift;
            int i, endIdx = 0;
            uint formatId = 168;
            bool RedrawBorder;
            int[] weightStyleIndexArray = null;

            int[] numericStyleIndexArray = null;
            IsPortrait = CurrentOutput.Orientation == TableOrientation.Portrait;
            ReportCreatorXML reportCreatorXML = new ReportCreatorXML();
            NumberingFormats numberingFormats = document.WorkbookPart.WorkbookStylesPart.Stylesheet.NumberingFormats;
            CellFormats cellFormats = document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats;

            //if (SigTestFormatSheet == null)
            //{
            //    weightStyleIndexArray = new int[] { 57, 56, 42, 38 };
            //    endIdx = 4;
            //}
            //else
            //{
                weightStyleIndexArray = new int[] { 3,7 };
                endIdx = 2;
            //}
            if (MaxAxesCount == 1)
            {
                if (IsPortrait)
                {

                }
                else
                {
                    CutColumn++;
                }
            }
            if (OnlyCutTriple) { return; }
            //' フォーマットシートのWB前全体列またはWB前全体行の削除
            if (!CurrentOutput.ShowPreWBTotal)
            {
                if (IsPortrait)
                {

                }
                else
                {
                    CutColumn++;
                }
            }
            if (IsPortrait)
            {
                if (!CurrentOutput.ShowIVAtItem)
                {
                    tmpSuffix = "_InvalidRow";
                }
                if (!CurrentOutput.ShowNAAtItem)
                {
                    tmpSuffix = "_NoAnswerRow";
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

                    CutColumn++;
                }
                if (!CurrentOutput.ShowNAAtItem)
                {
                    tmpSuffix = "_NoAnswerColumn";

                    CutColumn++;
                }
                else
                {
                    RedrawBorder = false;
                }
            }
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
                tmp = CurrentOutput.ParentRequest.WeightAverageNumDigitsAfterDecimal;
                Suffix = "_WT_WeightAverage";
                fmt = reportCreatorXML.NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                reportCreatorXML.SetNumberFormat(cellFormats, numberingFormats, weightStyleIndexArray, ref formatId, fmt, endIdx);
            }
            else if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                //' 縦％表の場合はウエイト列削除
                if (IsPortrait)
                {

                }
            }
            tmpSuffix = IsPortrait ? "Row" : "Column";
            tmpNamesArray = "N".Split();

            if (CurrentOutput.ParentRequest.ShowSummary)
            {
                numericStyleIndexArray = new int[] { 125,131,137};
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Summary);
                fmt = reportCreatorXML.NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                reportCreatorXML.SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 3);
            }
            if (CurrentOutput.ParentRequest.ShowAverage)
            {
                numericStyleIndexArray = new int[] { 126,132,138};
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Average);
                fmt = reportCreatorXML.NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                reportCreatorXML.SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 3);
            }
            if (CurrentOutput.ParentRequest.ShowStdev)
            {
                numericStyleIndexArray = new int[] { 127,133,139};
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Stdev);
                fmt = reportCreatorXML.NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                reportCreatorXML.SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 3);
            }
            if (CurrentOutput.ParentRequest.ShowMinimum)
            {
                numericStyleIndexArray = new int[] { 128,134,140};
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Minimum);
                fmt = reportCreatorXML.NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                reportCreatorXML.SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 3);
            }
            if (CurrentOutput.ParentRequest.ShowMaximum)
            {
                numericStyleIndexArray = new int[] { 129,135,141};
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Maximum);
                fmt = reportCreatorXML.NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                reportCreatorXML.SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 3);
            }
            if (CurrentOutput.ParentRequest.ShowMedian)
            {
                numericStyleIndexArray = new int[] { 130,136,142};
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Median);
                fmt = reportCreatorXML.NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                reportCreatorXML.SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 3);
            }
        }

        private void GenerateWorkSheet(CheckCrossTemplate checkCrossTemplate,SpreadsheetDocument document, string tempTableName, int i)
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            WorksheetPart worksheetPart = null;
            Sheets sheets = workbookPart.Workbook.Sheets;
            string id = "rId2";
            Sheet sheet = null;

            sheet = new Sheet() { Name = tempTableName, SheetId = 1, Id = id };
            sheets.Append(sheet);
            worksheetPart = workbookPart.AddNewPart<WorksheetPart>(id);
            checkCrossTemplate.GenerateWorksheetDoubleStandard(worksheetPart);

        }

        public void FormatLandscapeTable(CrossTable Table, ref SpreadsheetDocument reportDocument, WorksheetPart worksheetPart, int maxAxisCnt
             , string TemplateSheet, ref int RowNum
             , Hashtable CutRowsCol, Hashtable CutColumnsCol
             , string FormatSheet, string FormatRangeNamePrefix
             , TableType TableType, bool HasWeight
             , string StartCell, bool isN, int CutColumn
             , string ContentsSheet = null
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
            string tmpRange;
            string tmpRange2;
            string tmpTableRows;
            string TableRows = null;
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
            string tmpHeaderRange;
            string rng = null;
            CheckCrossNPTable checkCrossNPTable = new CheckCrossNPTable();

            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            HasNAColumn = CurrentOutput.ShowNAAtItem;
            HasIVColumn = CurrentOutput.ShowIVAtItem;
            HasNARow = CurrentOutput.ShowNAAtAxis;
            HasIVRow = CurrentOutput.ShowIVAtAxis;
            d = 1 + (NPOICrossCreator.ToInt(!isN & TableType == TableType.NPer) & 1);
            d2 = d + (NPOICrossCreator.ToInt(!isN & IsSigTest) & 1);
            if (IsSigTest && NPOICrossCreator.checkSimpleAggr(Table) && !isN)
            {
                d = d + 1;
            }
            //Application Application = WorkingBook.Application;
            if (isN)
            {
                if (HasNAColumn)
                {
                    CutNAColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (NPOICrossCreator.ToInt(HasIVColumn) & 1));
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

                if (FormatSheet == "Cross_P_Std")
                {
                    if (FormatRangeNamePrefix == "SA_MA")
                        c = SA_MA_STD_LCol - CutColumn;
                    else if (FormatRangeNamePrefix == "SA_MA_WT")
                        c = SA_MA_WT_STD_LCol - CutColumn;
                    else if (FormatRangeNamePrefix == "N")
                        c = N_STD_LCol - CutColumn;
                }
                else if (FormatSheet == "P_Sig")
                {
                    if (FormatRangeNamePrefix == "SA_MA")
                        c = SA_MA_STD_LCol - CutColumn;
                    else if (FormatRangeNamePrefix == "SA_MA_WT")
                        c = SA_MA_WT_STD_LCol - CutColumn;
                    else if (FormatRangeNamePrefix == "N")
                        c = N_STD_LCol - CutColumn;
                }
                if (HasNAColumn && !CutNAColumn)
                {
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (NPOICrossCreator.ToInt(HasWeight) & 2) - (NPOICrossCreator.ToInt(HasIVColumn) & 1) - (Table.Question.SubTotalCnt)))
                    {
                        CutNAColumn = true;
                    }
                    else if (c + ItemSectorsCount + 1 - 1 > MaxColumnsCount)
                    {
                        CutNAColumn = true;
                        CutIVColumn = HasIVColumn;
                        CutWTColumns = HasWeight;
                    }
                }
                if (HasIVColumn && !CutIVColumn)
                {
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (NPOICrossCreator.ToInt(HasWeight) & 2) - (Table.Question.SubTotalCnt)))
                    {
                        CutIVColumn = true;
                    }
                    else if (c + ItemSectorsCount + (NPOICrossCreator.ToInt(HasNAColumn && !CutNAColumn) & 1) + 1 - 1 > MaxColumnsCount)
                    {
                        CutIVColumn = true;
                        CutWTColumns = HasWeight;
                    }
                }
                if (HasWeight && !CutWTColumns)
                {
                    if (c + ItemSectorsCount + (NPOICrossCreator.ToInt(HasNAColumn && !CutNAColumn) & 1) + (NPOICrossCreator.ToInt(HasIVColumn && !CutIVColumn) & 1) + 2 - 1 > MaxColumnsCount)
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

            checkCrossNPTable.GenerateCrossPerTable(CurrentOutput, ref worksheetPart, ref RowNum, maxAxisCnt, CutNAColumn, CutIVColumn,
                                               ItemSectorsCount, Table, HasNARow, HasIVRow, CutRowsCol, FormatRangeNamePrefix, CutMedian);
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
    }
}
