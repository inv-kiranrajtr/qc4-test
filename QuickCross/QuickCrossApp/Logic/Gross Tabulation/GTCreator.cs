using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.Tabulation;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Common.Constants;
using XlFileFormat = Microsoft.Office.Interop.Excel.XlFileFormat;
using XlPageOrientation = Macromill.QCWeb.Common.XlPageOrientation;
using XlPaperSize = Macromill.QCWeb.Common.XlPaperSize;
using Excel = Microsoft.Office.Interop.Excel;
using Qc4Launcher.Logic;
using Microsoft.Office.Interop.Excel;
using VBA = Microsoft.VisualBasic;
using Constants = Microsoft.VisualBasic.Constants;
using CrossCreator = Qc4Launcher.Logic.CrossCreator;
using Tables = Macromill.QCWeb.Batch.Report.Tables;
using System.Drawing;
using XlChartType = Macromill.QCWeb.Common.XlChartType;
using log4net;
using System.Reflection;
using static Qc4Launcher.Logic.Gross_Tabulation.GrossTabulationQc;
using System.IO;
using System.ComponentModel;

namespace Qc4Launcher.Classes.Gross_Tabulation
{
    internal class GTCreator
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const string TEMPLATE_NAME = "GT.xlt";
        private const string TRANSPOSE_TEMPLATE_NAME = "GT.xlt";
        private const string FORMAT_TEMPLATE_NAME = "GTFormat.xlt";
        private const string WB_FORMAT_TEMPLATE_NAME = "GTWBFormat.xlt";
        private const string TRANSPOSE_FORMAT_TEMPLATE_NAME = "GTPortraitFormat.xlt";
        private const string TRANSPOSE_WB_FORMAT_TEMPLATE_NAME = "GTWBPortraitFormat.xlt";
        private const long AVERAGE_COLUMN_INDEX = 6;
        private OutputGT CurrentOutput;
        private long AverageColumnIndex;
        private long TotalColumnIndex;
        private long TotalRowIndex;
        private long AverageRowIndex;

        private const long MAX_ROWS_COUNT_PER_PAGE_AT_GRAPH = 41;
        private const double DEFAULT_HEIGHT_WIDTH_RATIO = 0.558620689655172;

        private long MaxRowsCountPerPage;
        private long MaxColumnsCountPerPage;
        //private long SigTestMaxColumnsCountPerPage;
        private long DefHeight;

        private string RunningProcName = string.Empty;
        private static string TemplateDirectoryPath;
        //private string OutputDirectoryPath;

        //New Implementaion 
        private static ExecuteStaticMethod ExecuteStaticMethod = new ExecuteStaticMethod();
        private Application xlApp;
        public BackgroundWorker bgWorker;
        private static Workbook WorkingBook;
        //private static Worksheet WorkingSheet;
        private Workbooks wbs;
        private Sheets wss;
        private static string BookPSWD;
        private static string SheetPSWD;
        //public static string ThisLocationCode;
        private static bool ResumeError;
        public List<string> outputFiles;

        CrossCreator CrossCreator = new CrossCreator();

        //internal void CreateGT(OutputGT Output, string bookPSWD, string sheetPSWD, string outputDirectoryPath, string templateDirectoryPath, string lccd, Application xlApplication, string outPutFileName)
        internal void CreateGT(OutputGT Output, string bookPSWD, string sheetPSWD, string templateDirectoryPath,
            string lccd, Application xlApplication, BackgroundWorker bgWorker, string outPutFileName, string[] tableKeys,
            OnWorkerMethodCompleteDelegate OnWorkerComplete, int fileNo, ref double currentProgress, double allocatedProgress, List<string> outputFiles = null)
        {
            BookPSWD = bookPSWD;
            SheetPSWD = sheetPSWD;
            //OutputDirectoryPath = outputDirectoryPath;
            TemplateDirectoryPath = templateDirectoryPath;

            const double MATRIX_BARCLUSTER_LINE_HEIGHT = 6;
            string TempPath;
            string FormatPath;
            Workbook NewBook;
            Workbook FormatBook = null;
            Workbook baseBook = null;
            Worksheet ContentsSheet = null;
            Worksheet TemplateSheet = null;
            Worksheet SigTestTemplateSheet = null;
            // Dim PageSetupSheetBaseName As String
            Worksheet PageSetupContentsSheet = null;
            Worksheet PageSetupTemplateSheet = null;
            Worksheet PageSetupSigTestTemplateSheet = null;
            Worksheet GraphSheet = null;
            Worksheet FormatSheet = null;
            Worksheet SigTestFormatSheet = null;
            string ReportTitle;
            Array ContentsValue = null;
            Array PageSetupContentsValue = null;
            Array TableStringValue = null;
            Array DataValue = null;
            Array Ranking = null;
            Array OptionNumbers = null;
            Array OptionNumbersTop = null;
            long i;
            string strIdx;
            long j;
            List<GTTable> Tables;
            GTTable tmpGTTable;
            GTTable tmpNextTable;
            long r;
            //long c;
            //long c2;
            long x = 0;
            long y;
            long rl;
            long ru;
            long ro;
            long cl;
            long cu;
            long co;
            bool HasWeight = false;
            bool DefHasNA;
            bool DefHasIV;
            bool HasNA;
            bool HasIV;
            long NAIdx;
            long IVIdx;
            long MedIdx = -1;
            bool CutNA;
            bool CutIV;
            bool CutMedian;
            bool f;
            //bool f2;
            //MethodResult res;
            bool HasWeightColumn = false;
            bool RunSignificanceTest = false;
            //bool CreateGraph = false;
            long AddColumnCount;
            // Dim sh As Object
            Range NPerStartCell = null;
            Range NStartCell = null;
            Range PerStartCell = null;
            Range SigTestStartCell = null;
            Range NPerPageSetupStartCell = null;
            Range NPageSetupStartCell = null;
            Range PerPageSetupStartCell = null;
            Range SigTestPageSetupStartCell = null;
            bool SigTestPageSetupOn = false;
            Range GraphStartCell = null;
            string fmt;
            Array HyperlinkTargetCells;
            Array PageSetupHyperlinkTargetCells = null;
            Range tmpStartCell;
            Range tmpPageSetupStartCell;
            string tmpAddress = null;
            bool CutPreWB;
            bool IsLastTable;
            Worksheet WorkingSheet = null;
            long u = 0;
            Macromill.QCWeb.Batch.Report.Request tmpRequest;
            bool PageSetupOn;
            Range TableRange;
            ChartObject tmpChartObject;
            Range tmpGraphSourceRange;
            Collection GraphSourceRangeCol;
            Collection GraphTableRangeCol;
            Collection nCol;
            Collection ChartObjectCol;
            Excel.Range tmpRange;
            bool IsNPerSourceRange;
            bool IsNSourceRange;   // RATだけ
            long NPerRemainedPageSetupRowsCount;
            long NRemainedPageSetupRowsCount;
            long PerRemainedPageSetupRowsCount;
            long SigTestRemainedPageSetupRowsCount;
            bool isMatrix = false;
            bool isN = false;
            long n;
            Excel.ChartObjects chtObjs = null;
            KeyItemInformation KeyItemInfo;
            string KeyItemName = null;
            string filenameSuffix = string.Empty;
            XlFileFormat xlFmt = XlFileFormat.xlOpenXMLWorkbook;
            string header;
            bool ResumeContinue;
            string[] NamesArray;
            bool noReset;
            bool IsQCM = false;
            string tmp;
            bool UseWeightFormat = false;
            bool HasNormalSigTest = false;
            bool HasMatrixBetweenChildSigTest = false;
            bool HasMatrixBetweenSectorSigTest = false;
            bool HasLetterColumn = false;
            bool AddLetterColumn;
            // Dim SigTestTemplateSheetName As String
            bool IsFirstSigTestTable;
            bool IsLastSigTestTable;
            bool IsNextLastSigTestTable;
            GTTable NextSigTestTable;
            bool NotRevise;

            string errdesc;
            string OrgProcName;
            //ErrorStruct[] Errors;
            //long ErrorsCount;
            bool CreateGraph = false;
            try
            {
                //xlApp = new Application();
                ////xlApp.Visible = true;
                xlApp = xlApplication;
                this.bgWorker = bgWorker;
                if (outputFiles == null) outputFiles = new List<string>();
                this.outputFiles = outputFiles;
                wbs = xlApp.Workbooks;
                baseBook = wbs.Add(OutputUtil.BaseTemplatePath(TemplateDirectoryPath, xlApp.PathSeparator));
                _log.Info("Excel base book added");
                xlApp.Calculation = XlCalculation.xlCalculationManual;
                WorkingBook = baseBook;
                wss = WorkingBook.Worksheets;
                WorkingSheet = wss.Item[1];
                CutMedian = Output.ParentRequest.ShowMedian & Output.WBOn;

                Reportset reportset = (Reportset)Output.ParentReportset;
                OrgProcName = RunningProcName;
                RunningProcName = "GTCreator.CreateGT";
                CurrentOutput = Output;

                TempPath = TemplatePath(Output.Orientation, xlFmt);
                FormatPath = FormatTemplatePath(Output.WBOn, Output.Orientation);


                if (Strings.Len(FileSystem.Dir(TempPath)) == 0)
                {
                }
                if (Strings.Len(FileSystem.Dir(FormatPath)) == 0)
                {
                }

                xlApp.PrintCommunication = false;
                xlApp.DisplayAlerts = false;
                FormatBook = wbs.Add(FormatPath);
                _log.Info("Excel format book added");
                FormatBook.Unprotect(BookPSWD);
                NewBook = null;
                if (!Util.CommonFunction.ActivationKeyChecking())
                {
                    string tempFolder = Path.Combine(Path.GetTempPath(), "QC4") + "\\GTOutputForSTD";//TempPath.Substring(0, TempPath.LastIndexOf('\\')) + "\\GTOutputForSTD";
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
                            fileDirectory = tempFolder + "\\GT" + fi;
                            if (!Directory.Exists(tempFolder + "\\GT" + fi))
                            {
                                Directory.CreateDirectory(tempFolder + "\\GT" + fi);
                                fi = -1;
                            }
                        }
                        catch { }
                    }
                    string filename = Qc4Launcher.Util.Definiotion.SelectedFile;
                    filename = filename.Split('_')[0];
                    string filepath = fileDirectory + "\\" + filename + "_" + (DateTime.Now.ToString("yyyyMMdd_HHmm")) + "_GT.xltx";
                    try
                    {
                        System.IO.File.Copy(TempPath, filepath);
                        NewBook = wbs.Add(filepath);
                    }
                    catch (Exception ex)
                    {
                        NewBook = wbs.Add(TempPath);
                    }
                }
                else
                    NewBook = wbs.Add(TempPath);
                _log.Info("Template book added");
                NewBook.Unprotect(BookPSWD);


                tmpGTTable = (GTTable)Output.Tables[0];
                KeyItemInfo = tmpGTTable.KeyItem;

                if (!(KeyItemInfo == null))
                    KeyItemName = KeyItemInfo.Name;
                if (Strings.Len(KeyItemName) > 0)
                {
                    n = reportset.KeyItemMaxSectorNumber(KeyItemName);
                    if (n > 0) n = (int)(Math.Log(n) / Math.Log(10)) + 1;
                    fmt = new string('0', (Int32)n);
                    filenameSuffix = "_" + KeyItemName + "_" + KeyItemInfo.SectorNumber.ToString(fmt);
                }
                tmpRequest = (Macromill.QCWeb.Batch.Report.Request)Output.ParentRequest;

                {
                    var withBlock = tmpRequest;
                    // 調査タイトル
                    ReportTitle = withBlock.Title;
                    // 無回答表示
                    // DefHasNA = .ShowNAAtItem
                    DefHasNA = Output.ShowNAAtItem;
                    // 非該当表示
                    // DefHasIV = .ShowIVAtItem
                    DefHasIV = Output.ShowIVAtItem;
                }

                PageSetupOn = Output.PageSetup;

                Tables = new List<GTTable>();
                foreach (string tableKey in tableKeys)
                    foreach (string eTableKey in Output.Tables.Keys)
                    {
                        if (eTableKey == tableKey)
                            Tables.Add((GTTable)Output.Tables[eTableKey]);
                    }

                {
                    var withBlock = Tables;
                    CreateGTSub(Output, Tables, ref HasWeight, ref HasWeightColumn, ref UseWeightFormat, ref isMatrix, ref isN, ref CreateGraph, FormatBook, ref FormatSheet
                             , ref RunSignificanceTest, ref HasMatrixBetweenChildSigTest, ref HasMatrixBetweenSectorSigTest, ref HasNormalSigTest
                             , PageSetupOn, ref SigTestPageSetupOn, NewBook, ref ContentsSheet, ref TemplateSheet, ref SigTestTemplateSheet
                             , ref PageSetupContentsSheet, ref PageSetupTemplateSheet, ref PageSetupSigTestTemplateSheet, ref GraphSheet, ref WorkingSheet);
                    header = OutputUtil.GetAdjustedHeader(ReportTitle);
                    // 必要シートを生成
                    if (Output.OutputNPerTable)
                    {
                        NPerStartCell = TemplateSheet.Range["A1"];
                        string reportKeyWord = LocalResource.REPORT_CROSS_NP_SHEET_NAME;
                        NPerStartCell.Worksheet.Name = reportKeyWord;
                        NPerStartCell.Worksheet.Unprotect(SheetPSWD);
                        NPerStartCell.Worksheet.PageSetup.CenterHeader = header;
                        if (PageSetupOn & Output.PageSetupNPerTable)
                        {
                            NPerPageSetupStartCell = PageSetupTemplateSheet.Range["A1"];
                            reportKeyWord = LocalResource.REPORT_CROSS_PAGE_SETUP_SHEET_SUFFIX;
                            NPerPageSetupStartCell.Worksheet.Name = NPerStartCell.Worksheet.Name + " " + reportKeyWord;
                            NPerPageSetupStartCell.Worksheet.Unprotect(SheetPSWD);
                            NPerPageSetupStartCell.Worksheet.PageSetup.CenterHeader = header;
                        }
                    }
                    if (Output.OutputNTable)
                    {
                        if (Output.OutputNPerTable)
                        {
                            NPerStartCell.Worksheet.Copy(Type.Missing, NPerStartCell.Worksheet);
                            NStartCell = NPerStartCell.Worksheet.Next.Range["A1"];
                        }
                        else NStartCell = TemplateSheet.Range["A1"];

                        string reportKeyWord = LocalResource.REPORT_CROSS_N_SHEET_NAME;
                        NStartCell.Worksheet.Name = reportKeyWord;
                        NStartCell.Worksheet.Unprotect(SheetPSWD);
                        NStartCell.Worksheet.PageSetup.CenterHeader = header;
                        if (PageSetupOn & Output.PageSetupNTable)
                        {
                            if (Output.OutputNPerTable & Output.PageSetupNPerTable)
                            {
                                NPerPageSetupStartCell.Worksheet.Copy(Type.Missing, NPerPageSetupStartCell.Worksheet);
                                NPageSetupStartCell = NPerPageSetupStartCell.Worksheet.Next.Range["A1"];
                            }
                            else NPageSetupStartCell = PageSetupTemplateSheet.Range["A1"];

                            reportKeyWord = LocalResource.REPORT_CROSS_PAGE_SETUP_SHEET_SUFFIX;
                            NPageSetupStartCell.Worksheet.Name = NStartCell.Worksheet.Name + " " + reportKeyWord;
                            NPageSetupStartCell.Worksheet.Unprotect(SheetPSWD);
                            NPageSetupStartCell.Worksheet.PageSetup.CenterHeader = header;
                        }
                    }
                    if (Output.OutputPerTable)
                    {
                        if (Output.OutputNTable)
                        {
                            NStartCell.Worksheet.Copy(Type.Missing, NStartCell.Worksheet);
                            PerStartCell = NStartCell.Worksheet.Next.Range["A1"];
                        }
                        else if (Output.OutputNPerTable)
                        {
                            NPerStartCell.Worksheet.Copy(Type.Missing, NPerStartCell.Worksheet);
                            PerStartCell = NPerStartCell.Worksheet.Next.Range["A1"];
                        }
                        else PerStartCell = TemplateSheet.Range["A1"];

                        string reportKeyWord = LocalResource.REPORT_CROSS_P_SHEET_NAME;
                        PerStartCell.Worksheet.Name = reportKeyWord;
                        PerStartCell.Worksheet.Unprotect(SheetPSWD);
                        PerStartCell.Worksheet.PageSetup.CenterHeader = header;
                        if (PageSetupOn & Output.PageSetupPerTable)
                        {
                            if (Output.OutputNTable & Output.PageSetupNTable)
                            {
                                NPageSetupStartCell.Worksheet.Copy(Type.Missing, NPageSetupStartCell.Worksheet);
                                PerPageSetupStartCell = NPageSetupStartCell.Worksheet.Next.Range["A1"];
                            }
                            else if (Output.OutputNPerTable & Output.PageSetupNPerTable)
                            {
                                NPerPageSetupStartCell.Worksheet.Copy(Type.Missing, NPerPageSetupStartCell.Worksheet);
                                PerPageSetupStartCell = NPerPageSetupStartCell.Worksheet.Next.Range["A1"];
                            }
                            else PerPageSetupStartCell = PageSetupTemplateSheet.Range["A1"];

                            reportKeyWord = LocalResource.REPORT_CROSS_PAGE_SETUP_SHEET_SUFFIX;
                            PerPageSetupStartCell.Worksheet.Name = PerStartCell.Worksheet.Name + " " + reportKeyWord;
                            PerPageSetupStartCell.Worksheet.Unprotect(SheetPSWD);
                            PerPageSetupStartCell.Worksheet.PageSetup.CenterHeader = header;
                        }
                    }
                    if (CreateGraph)
                    {
                        string reportKeyWord = LocalResource.REPORT_CROSS_GRAPH_SHEET_NAME;
                        GraphSheet.Name = reportKeyWord;
                        GraphStartCell = GraphSheet.Range["A1"];
                        chtObjs = GraphSheet.ChartObjects();
                        GraphSheet.Unprotect(SheetPSWD);
                        GraphSheet.PageSetup.CenterHeader = header;
                    }
                    if (Output.SignificanceTest)
                    {
                        SigTestStartCell = SigTestTemplateSheet.Range["A1"];

                        SigTestTemplateSheet.Name = ExecuteStaticMethod.GetMessage(ReportMessageIndex.ReportCrossSignificanceTestSheetNameIndex, lccd).Description;
                        SigTestTemplateSheet.Unprotect(SheetPSWD);
                        SigTestTemplateSheet.PageSetup.CenterHeader = header;
                        if (PageSetupOn & Output.PageSetupSignificanceTestTable)
                        {
                            SigTestPageSetupStartCell = PageSetupSigTestTemplateSheet.Range["A1"];
                            string reportKeyWord = LocalResource.REPORT_CROSS_PAGE_SETUP_SHEET_SUFFIX;
                            SigTestPageSetupStartCell.Worksheet.Name = SigTestPageSetupStartCell.Worksheet.Name + " " + reportKeyWord;
                            SigTestPageSetupStartCell.Worksheet.Unprotect(SheetPSWD);
                            SigTestPageSetupStartCell.Worksheet.PageSetup.CenterHeader = header;
                        }
                    }

                    // フォーマットブックの必要シートへの参照を取得
                    {
                        var withBlock1 = FormatBook.Worksheets;
                        if (FormatSheet == null)
                        {
                            FormatSheet = withBlock1.Item["Standard"];
                            if (Output.SignificanceTest)
                                SigTestFormatSheet = withBlock1.Item["SignificanceTest"];
                        }
                        else if (Output.SignificanceTest)
                            SigTestFormatSheet = withBlock1.Item["Hybrid"];
                    }
                    CutPreWB = CurrentOutput.WBOn & !CurrentOutput.ShowPreWBTotal;
                    FormatSheet.Unprotect(SheetPSWD);
                    if (Output.SignificanceTest)
                        SigTestFormatSheet.Unprotect(SheetPSWD);
                    // 無駄なカラムの削除
                    if (Output.SignificanceTest)
                    {
                        HasLetterColumn = true;
                        if (Output.Orientation == TableOrientation.Landscape)
                        {
                            if (!(HasNormalSigTest || HasMatrixBetweenChildSigTest))
                            {
                                HasLetterColumn = false;
                                SigTestFormatSheet.Columns.Item[4 + (CrossCreator.ToInt(SigTestFormatSheet.Name == "Hybrid") & 1)].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                            }
                        }
                        else
                        {
                            if (!(HasNormalSigTest || HasMatrixBetweenSectorSigTest))
                            {
                                HasLetterColumn = false;
                                SigTestFormatSheet.Columns.Item[4 + (CrossCreator.ToInt(SigTestFormatSheet.Name == "Hybrid") & 1)].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                            }
                        }

                    }

                    if (!HasWeightColumn)
                    {
                        if (FormatSheet.Name == "Weight")
                        {
                            FormatSheet.Columns.Item[4].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                            if (Output.SignificanceTest)
                                SigTestFormatSheet.Columns.Item[4].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                        }
                    }

                    if (Output.Orientation == TableOrientation.Landscape)
                        AdjustLandscapeFormat(FormatSheet, SigTestFormatSheet, CutPreWB, HasWeightColumn, UseWeightFormat);
                    else
                        AdjustPortraitFormat(FormatSheet, SigTestFormatSheet, CutPreWB, HasWeightColumn);

                    // fmt = String$(Int(Log(.Count) / Log(10)) + 1&, "0")
                    n = (int)((Math.Log(withBlock.Count) / Math.Log(10)) + 1);
                    if (n < 4)
                        n = 4;
                    fmt = new String('0', Convert.ToInt32(n));

                    ContentsValue = Array.CreateInstance(typeof(string), // ReDim ContentsValue(1& To .Count, 1& To 6&)
                    new int[] { withBlock.Count, 6 },
                    new int[] { 1, 1 });

                    HyperlinkTargetCells = Array.CreateInstance(typeof(object), // ReDim HyperlinkTargetCells(1& To .Count, 3& To 6&)
                    new int[] { withBlock.Count, 6 - 3 + 1 }, // new int[] { 1st element length, 2nd element length }
                    new int[] { 1, 3 }); // new int[] { Starting index of 1st element , Starting index of 2nd element });

                    if (PageSetupOn)
                    {
                        PageSetupContentsValue = Array.CreateInstance(typeof(string), //ReDim PageSetupContentsValue(1 & To.Count, 1 & To 6 &)
                        new int[] { withBlock.Count, 6 },
                        new int[] { 1, 1 });

                        PageSetupHyperlinkTargetCells = Array.CreateInstance(typeof(object), // ReDim PageSetupHyperlinkTargetCells(1& To .Count, 3& To 5&)
                        new int[] { withBlock.Count, 5 - 3 + 1 },
                        new int[] { 1, 3 });
                    }

                    NPerRemainedPageSetupRowsCount = MaxRowsCountPerPage;
                    NRemainedPageSetupRowsCount = MaxRowsCountPerPage;
                    PerRemainedPageSetupRowsCount = MaxRowsCountPerPage;
                    SigTestRemainedPageSetupRowsCount = MaxRowsCountPerPage;
                    IsNPerSourceRange = !(NPerStartCell == null) && PerStartCell == null;
                    IsNSourceRange = !(NStartCell == null) && NPerStartCell == null && PerStartCell == null;
                    IsFirstSigTestTable = true;

                    #region Progress Bar Implementation
                    double childProgress = 0; double UpdProgress = 0;
                    #endregion
                    for (i = 1; i <= withBlock.Count; i++)
                    {
                        if (bgWorker.CancellationPending) return;
                        #region Progress Bar Implementation
                        double progressChildPerc = (double)i / withBlock.Count * 100;
                        childProgress = allocatedProgress * progressChildPerc / 100;
                        UpdProgress = currentProgress + childProgress;
                        OnWorkerComplete(Convert.ToInt32(UpdProgress), String.Format(LocalResource.PB_GT_GENE_RPTS_TABLE, fileNo, i, withBlock.Count));
                        #endregion

                        tmpGTTable = (GTTable)withBlock[(int)i - 1];
                        _log.Info("EtmpGTTable:" + tmpGTTable.Question.Name);
                        strIdx = i.ToString(fmt); // Format(i, fmt)
                        IsLastTable = i == withBlock.Count;
                        MedIdx = -1;
                        // ウエイト値の有無をチェック
                        HasWeight = GetHasWeight(tmpGTTable);
                        {
                            var withBlock1 = tmpGTTable;
                            AddColumnCount = 0;
                            if (HasWeightColumn)
                            {
                                AddColumnCount = 1;
                                if ((withBlock1.Question.QuestionType & QuestionType.MatrixParent) == 0 | Output.Orientation == TableOrientation.Portrait)
                                {
                                    if ((withBlock1.Question.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                                    {
                                        if (HasWeight)
                                            AddColumnCount = 0;
                                    }
                                }
                            }
                            // データ内容が取得できなければエラースロー
                            rl = withBlock1.GetTableValueRowIndexMinimum;
                            ru = withBlock1.GetTableValueRowIndexMaximum;
                            cl = withBlock1.GetTableValueColumnIndexMinimum;
                            cu = withBlock1.GetTableValueColumnIndexMaximum;


                            if (rl == -1 || ru == -1 || cl == -1 || ru == -1) // Doubted on cu
                            {
                            }


                            // 目次での該当行の内容
                            ContentsValue.SetValue(withBlock1.Question.Name, i, 1);   // 質問ID
                            ContentsValue.SetValue(withBlock1.Question.Description, i, 2);    // 質問文
                            if (Output.OutputNPerTable)
                                ContentsValue.SetValue("Table" + strIdx, i, 3);
                            if (Output.OutputNTable)
                                ContentsValue.SetValue("NTable" + strIdx, i, 4);
                            if (Output.OutputPerTable)
                                ContentsValue.SetValue("PTable" + strIdx, i, 5);
                            if (!(withBlock1.Chart == null))
                            {
                                if ((withBlock1.Chart.ChartType == XlChartType.xlBarStacked100 || withBlock1.Chart.ChartType == XlChartType.xlColumnStacked100) && withBlock1.SectorsCount > 255)
                                    withBlock1.Chart.ChartType = 0;
                                if (withBlock1.Chart.ChartType != 0)
                                {
                                    ContentsValue.SetValue("Graph" + strIdx, i, 6);
                                }
                            }
                            if (PageSetupOn)
                            {
                                PageSetupContentsValue.SetValue(withBlock1.Question.Name, i, 1);   // 質問ID
                                PageSetupContentsValue.SetValue(withBlock1.Question.Description, i, 2);    // 質問文
                                if (Output.OutputNPerTable)
                                    PageSetupContentsValue.SetValue("Table" + strIdx, i, 3);
                                if (Output.OutputNTable)
                                    PageSetupContentsValue.SetValue("NTable" + strIdx, i, 4);
                                if (Output.OutputPerTable)
                                    PageSetupContentsValue.SetValue("PTable" + strIdx, i, 5);
                            }
                            HasNA = DefHasNA;
                            HasIV = DefHasIV;
                            CutNA = !HasNA;
                            CutIV = !HasIV;
                            isMatrix = (withBlock1.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
                            isN = (withBlock1.Question.QuestionType & QuestionType.N) == QuestionType.N;
                            TableRange = null;
                            GraphSourceRangeCol = null;
                            GraphTableRangeCol = null;
                            nCol = null;

                            NamesArray = Strings.Split("");
                            noReset = false;
                            if (!isMatrix)
                            {
                                if ((withBlock1.Question.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                                {
                                    // SA/MA標準では縦％表
                                    // 無回答/非該当の出力チェック
                                    // 無回答出力設定でも0の場合は出力しない (元データにはある)
                                    if (DefHasNA)
                                    {
                                        NAIdx = (ru + System.Convert.ToInt64(DefHasIV) + (CrossCreator.ToInt(HasWeight) & -2)) - (withBlock1.Question.SubTotalCnt); //NAIdx = ru + CLng(DefHasIV) + (HasWeight And -2&)
                                        HasNA = withBlock1.ParentRequest.ShowZeroNAIV | System.Convert.ToDouble(withBlock1.TableValue(Convert.ToInt32(NAIdx), Convert.ToInt32(cu))) != 0;
                                        CutNA = !HasNA;
                                    }
                                    else
                                        NAIdx = (rl - 1) - (withBlock1.Question.SubTotalCnt);
                                    // 非該当出力設定でも0の場合は出力しない (元データにはある)
                                    if (DefHasIV)
                                    {
                                        IVIdx = ru + (CrossCreator.ToInt(HasWeight) & -2);
                                        HasIV = withBlock1.ParentRequest.ShowZeroNAIV | System.Convert.ToDouble(withBlock1.TableValue(Convert.ToInt32(IVIdx), Convert.ToInt32(cu))) != 0;
                                        CutIV = !HasIV;
                                    }
                                    else
                                        IVIdx = rl - 1;
                                    //MedIdx = ru - (CrossCreator.ToInt(DefHasIV) & 1) - (CrossCreator.ToInt(DefHasNA) & 1);
                                    if (!(NPerStartCell == null))
                                    {
                                        if (bgWorker.CancellationPending) return;
                                        CreateNormalGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, HasWeight, TableType.NPer, CutMedian: CutMedian, MedIdx: MedIdx);
                                        // データ出力
                                        u = Information.UBound(TableStringValue, 2);
                                        tmpStartCell = NPerStartCell;
                                        tmpPageSetupStartCell = NPerPageSetupStartCell;

                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell.Address; //tmpAddress = tmpPageSetupStartCell.Address(External:=True)
                                        OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, 0
                                                , TableOrientation.Portrait, strIdx
                                                , CutNA, CutIV, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.NPer
                                                , FormatSheet, ref NPerStartCell, ref NPerPageSetupStartCell, IsLastTable
                                                , WorkingSheet, ref TableRange, ref NPerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = xlApp.Range[tmpAddress]; // Set tmpPageSetupStartCell = Me.Application.Range(tmpAddress)
                                        ResumeContinue = true;

                                        if (tmpStartCell == NPerStartCell)
                                        {
                                            NPerStartCell = null;
                                            if (i == 1)
                                            {
                                                if (NStartCell == null && PerStartCell == null && SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                    //Information.Err().Raise(Constants.vbObjectError + 1000 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                                }
                                                else
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    tmpStartCell.Worksheet.Delete();
                                                }
                                            }

                                        }
                                        else
                                        {
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 3);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 3);
                                            if (NPerStartCell == null && !IsLastTable)
                                            {
                                                if (PerStartCell == null)
                                                {
                                                    GraphStartCell = null;
                                                    tmpNextTable = (GTTable)Tables[(int)i];
                                                }
                                            }
                                            if (Information.UBound(TableStringValue, 2) < u)
                                            {
                                            }
                                            else if (IsNPerSourceRange)
                                            {
                                                if (!(TableRange == null))
                                                {
                                                    {
                                                        var withBlock2 = TableRange.Rows.Item[3 + (CrossCreator.ToInt(Output.ShowPreWBTotal) & 1)].Resize(withBlock1.SectorsCount + (CrossCreator.ToInt(HasNA) & 1));
                                                        tmpGraphSourceRange = xlApp.Union(withBlock2.Columns.Item[2], withBlock2.Columns.Item(withBlock2.Columns.Count));

                                                        GraphSourceRangeCol = new VBA.Collection();
                                                        nCol = new VBA.Collection();
                                                        GraphSourceRangeCol.Add(tmpGraphSourceRange);
                                                        GraphTableRangeCol.Add(TableRange);
                                                        nCol.Add(withBlock2.Cells.Item[0, withBlock2.Columns.Count - 1].Value.ToString("0"));
                                                    }
                                                }
                                            }
                                        }
                                        if (!(tmpPageSetupStartCell == null))
                                        {
                                            f = !(NPerPageSetupStartCell == null);
                                            if (f)
                                                f = (NPerPageSetupStartCell.Address == tmpPageSetupStartCell.Address);
                                            if (f)
                                            {
                                                if (i == 1)
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    NPerPageSetupStartCell.Worksheet.Delete();
                                                }
                                                else
                                                {
                                                };
                                                NPerPageSetupStartCell = null;
                                            }
                                            else
                                            {
                                                PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 3); if (NPerPageSetupStartCell == null & !IsLastTable)
                                                {
                                                    tmpNextTable = (GTTable)Tables[(int)i];
                                                }
                                            }
                                        }
                                        ResumeContinue = false;
                                    }
                                    //DoEvents();
                                    if (!(NStartCell == null))
                                    {
                                        if (bgWorker.CancellationPending) return;
                                        CreateNormalGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, HasWeight, TableType.N, CutMedian: CutMedian, MedIdx: MedIdx);
                                        // データ出力
                                        u = Information.UBound(TableStringValue, 2);
                                        tmpStartCell = NStartCell;
                                        tmpPageSetupStartCell = NPageSetupStartCell;
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell.Address; //tmpAddress = tmpPageSetupStartCell.Address(External: true);

                                        OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, 0
                                                , TableOrientation.Portrait, strIdx
                                                , CutNA, CutIV, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.N
                                                , FormatSheet, ref NStartCell, ref NPageSetupStartCell, IsLastTable
                                                , WorkingSheet, ref TableRange, ref NRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                        ResumeContinue = true;
                                        if (tmpStartCell == NStartCell)
                                        {
                                            NStartCell = null;
                                            if (i == 1)
                                            {
                                                if (SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                    //Information.Err().Raise(Constants.vbObjectError + 1010, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                                }
                                                else
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    tmpStartCell.Worksheet.Delete();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 4);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 4);
                                            if (NStartCell == null & !IsLastTable)
                                            {
                                                tmpNextTable = (GTTable)Tables[(int)i];
                                            }
                                            if (Information.UBound(TableStringValue, 2) < u)
                                            {
                                            }
                                        }
                                        if (!(tmpPageSetupStartCell == null))
                                        {
                                            f = !(NPageSetupStartCell == null);
                                            if (f)
                                                f = NPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                            if (f)
                                            {
                                                if (i == 1)
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    NPageSetupStartCell.Worksheet.Delete();
                                                }
                                                else
                                                {
                                                };
                                                NPageSetupStartCell = null;
                                            }
                                            else
                                            {
                                                PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 4);
                                                if (NPageSetupStartCell == null & !IsLastTable)
                                                {
                                                    tmpNextTable = (GTTable)Tables[(int)i];
                                                }
                                            }
                                        }
                                        ResumeContinue = false;
                                    }
                                    //DoEvents();
                                    if (!(PerStartCell == null))
                                    {
                                        if (bgWorker.CancellationPending) return;
                                        CreateNormalGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, HasWeight, TableType.Per, CutMedian: CutMedian, MedIdx: MedIdx);
                                        // データ出力
                                        u = Information.UBound(TableStringValue, 2);
                                        tmpStartCell = PerStartCell;
                                        tmpPageSetupStartCell = PerPageSetupStartCell;

                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                        OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, 0
                                            , TableOrientation.Portrait, strIdx
                                            , CutNA, CutIV, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.Per
                                            , FormatSheet, ref PerStartCell, ref PerPageSetupStartCell, IsLastTable
                                            , WorkingSheet, ref TableRange, ref PerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                        ResumeContinue = true;
                                        if (tmpStartCell == PerStartCell)
                                        {
                                            PerStartCell = null;
                                            if (i == 1)
                                            {
                                                if (SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                    //Information.Err().Raise(Constants.vbObjectError + 1020 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                                }
                                                else
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    tmpStartCell.Worksheet.Delete();
                                                }
                                            }

                                        }
                                        else
                                        {
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 5);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 5);
                                            if (PerStartCell == null & !IsLastTable)
                                            {
                                                GraphStartCell = null;
                                                tmpNextTable = (GTTable)Tables[(int)i];
                                            }
                                            if (Information.UBound(TableStringValue, 2) < u)
                                            {
                                            }
                                            else if (!(TableRange == null))
                                            {
                                                {
                                                    var withBlock2 = TableRange.Rows.Item[3 + (CrossCreator.ToInt(Output.ShowPreWBTotal) & 1)].Resize(withBlock1.SectorsCount + (CrossCreator.ToInt(HasNA) & 1));
                                                    GraphSourceRangeCol = new VBA.Collection();
                                                    GraphTableRangeCol = new VBA.Collection();
                                                    nCol = new VBA.Collection();
                                                    tmpGraphSourceRange = xlApp.Union(withBlock2.Columns.Item[2], withBlock2.Columns.Item[withBlock2.Columns.Count]);
                                                    GraphSourceRangeCol.Add(tmpGraphSourceRange);
                                                    GraphTableRangeCol.Add(TableRange);
                                                    var columnValue = withBlock2.Cells.Item(0, withBlock2.Columns.Count).Value;
                                                    if (columnValue == null)
                                                    {
                                                        columnValue = 0;
                                                    }

                                                    nCol.Add(columnValue.ToString("0"));
                                                }
                                            }
                                        }
                                        if (!(tmpPageSetupStartCell == null))
                                        {
                                            f = !(PerPageSetupStartCell == null);
                                            if (f)
                                                f = PerPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                            if (f)
                                            {
                                                if (i == 1)
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    PerPageSetupStartCell.Worksheet.Delete();
                                                }
                                                else
                                                {
                                                }
                                                PerPageSetupStartCell = null;
                                            }
                                            else
                                            {
                                                /*GTCreator.cls Line:3760*/
                                                PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 5);
                                                if (PerPageSetupStartCell == null && !IsLastTable)
                                                {
                                                    tmpNextTable = (GTTable)Tables[(int)i];
                                                }
                                            }
                                        }
                                        ResumeContinue = false;
                                    }
                                    //DoEvents();
                                    if (!(SigTestStartCell == null) && tmpGTTable.SignificancetestCode == SignificanceTestCode.BetweenSectors)
                                    {
                                        if (bgWorker.CancellationPending) return;
                                        CreateNormalGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, HasWeight, TableType.SignificanceTest, CutMedian: CutMedian, MedIdx: MedIdx);
                                        // データ出力
                                        u = Information.UBound(TableStringValue, 2);
                                        tmpStartCell = SigTestStartCell;
                                        tmpPageSetupStartCell = SigTestPageSetupStartCell;
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                        IsLastSigTestTable = true;
                                        IsNextLastSigTestTable = true;
                                        NextSigTestTable = null;
                                        for (j = i; j <= Tables.Count - 1; j++)
                                        {
                                            tmpNextTable = (GTTable)Tables[(int)j];
                                            if (tmpNextTable.SignificancetestCode != SignificanceTestCode.Off)
                                            {
                                                if (IsLastSigTestTable)
                                                {
                                                    IsLastSigTestTable = false;
                                                    NextSigTestTable = tmpNextTable;
                                                }
                                                else
                                                {
                                                    IsNextLastSigTestTable = false;
                                                    break;
                                                }
                                            }
                                        }
                                        OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, 0
                                                , TableOrientation.Portrait, strIdx
                                                , CutNA, CutIV, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.SignificanceTest
                                                , SigTestFormatSheet, ref SigTestStartCell, ref SigTestPageSetupStartCell, IsLastSigTestTable
                                                , WorkingSheet, ref TableRange, ref SigTestRemainedPageSetupRowsCount, false, CutMedian, HasLetterColumn, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                        ResumeContinue = true;
                                        if (tmpStartCell == SigTestStartCell)
                                        {
                                            SigTestStartCell = null;
                                            if (IsFirstSigTestTable)
                                            {
                                                if (NPerStartCell == null && NStartCell == null && PerStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                    //Information.Err().Raise(Constants.vbObjectError + 1030 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                                }
                                                else
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    tmpStartCell.Worksheet.Delete();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (SigTestStartCell == null && !IsLastSigTestTable)
                                            {
                                            }
                                            if (Information.UBound(TableStringValue, 2) < u)
                                            {
                                            }
                                        }
                                        if (!(tmpPageSetupStartCell == null))
                                        {
                                            f = !(SigTestPageSetupStartCell == null);
                                            if (f)
                                                f = SigTestPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                            if (f)
                                            {
                                                if (IsFirstSigTestTable)
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    SigTestPageSetupStartCell.Worksheet.Delete();
                                                }
                                                else
                                                {
                                                }
                                                SigTestPageSetupStartCell = null;
                                            }
                                            else if (SigTestPageSetupStartCell == null && !IsLastSigTestTable)
                                            {
                                            }
                                        }
                                        if (IsLastSigTestTable)
                                        {
                                            SigTestStartCell = null;
                                            SigTestPageSetupStartCell = null;
                                        }
                                        ResumeContinue = false;
                                        IsFirstSigTestTable = false;
                                    }
                                    //DoEvents();
                                }
                                else if (isN)
                                {
                                    // 数値回答質問
                                    // 無回答/非該当の出力チェック
                                    // 無回答出力設定でも0の場合は出力しない (元データにはある)
                                    if (DefHasNA)
                                    {
                                        NAIdx = (cu + System.Convert.ToInt64(DefHasIV)) - (withBlock1.Question.SubTotalCnt);
                                        //HasNA = (withBlock1.ParentRequest.ShowZeroNAIV || System.Convert.ToDouble(withBlock1.TableValue((Int32)ru, (Int32)NAIdx)) != 0);
                                        HasNA = true;
                                        CutNA = !HasNA;
                                    }
                                    else
                                        NAIdx = (cl - 1) - (withBlock1.Question.SubTotalCnt);
                                    // 非該当出力設定でも0の場合は出力しない (元データにはある)
                                    if (DefHasIV)
                                    {
                                        IVIdx = cu;
                                        HasIV = (withBlock1.ParentRequest.ShowZeroNAIV || System.Convert.ToDouble(withBlock1.TableValue((Int32)ru, (Int32)IVIdx)) != 0);
                                        CutIV = !HasIV;
                                    }
                                    else
                                        IVIdx = cl - 1;

                                    MedIdx = cu - (CrossCreator.ToInt(DefHasIV) & 1) - (CrossCreator.ToInt(DefHasNA) & 1);
                                    // 出力する配列の生成
                                    ro = 1;
                                    co = 2;
                                    if (Output.Orientation == TableOrientation.Portrait)
                                        CreatePortraitGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.N, CutMedian: CutMedian, MedIdx: MedIdx);
                                    else
                                        CreateLandscapeGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, ref OptionNumbersTop, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.N, CutMedian: CutMedian, MedIdx: MedIdx);
                                    u = Information.UBound(TableStringValue, 2);
                                    NotRevise = false;
                                    if (!(NPerStartCell == null))
                                    {
                                        tmpStartCell = NPerStartCell;
                                        tmpPageSetupStartCell = NPerPageSetupStartCell;

                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                        OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, 0
                                                , Output.Orientation, strIdx
                                                , CutNA, CutIV, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.NPer
                                                , FormatSheet, ref NPerStartCell, ref NPerPageSetupStartCell, IsLastTable
                                                , WorkingSheet, ref TableRange, ref NPerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                        ResumeContinue = true;
                                        if (tmpStartCell == NPerStartCell)
                                        {
                                            NPerStartCell = null;
                                            if (i == 1)
                                            {
                                                if (NStartCell == null & PerStartCell == null & SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                    //Information.Err().Raise(Constants.vbObjectError + 1000, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                                }
                                                else
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    tmpStartCell.Worksheet.Delete();
                                                }
                                            };
                                        }
                                        else
                                        {
                                            NotRevise = true;
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 3);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 3);
                                            if (NPerStartCell == null & !IsLastTable)
                                            {
                                                tmpNextTable = (GTTable)Tables[(int)i];

                                            }
                                            if (Information.UBound(TableStringValue, 2) < u)
                                            {
                                            }
                                        }
                                        if (!(tmpPageSetupStartCell == null))
                                        {
                                            f = !(NPerPageSetupStartCell == null);
                                            if (f)
                                                f = NPerPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                            if (f)
                                            {
                                                if (i == 1)
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    NPerPageSetupStartCell.Worksheet.Delete();
                                                }
                                                else
                                                {
                                                }
                                                NPerPageSetupStartCell = null;
                                            }
                                            else
                                            {
                                                PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 3);
                                                if (NPerPageSetupStartCell == null & !IsLastTable)
                                                {
                                                    tmpNextTable = (GTTable)Tables[(int)i];
                                                }
                                            }
                                        }
                                        ResumeContinue = false;
                                    }
                                    //DoEvents();
                                    if (!(NStartCell == null))
                                    {
                                        tmpStartCell = NStartCell;
                                        tmpPageSetupStartCell = NPageSetupStartCell;
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                        OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, 0
                                                , Output.Orientation, strIdx
                                                , CutNA, CutIV, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.N
                                                , FormatSheet, ref NStartCell, ref NPageSetupStartCell, IsLastTable
                                                , WorkingSheet, ref TableRange, ref NRemainedPageSetupRowsCount, false, CutMedian, false, NotRevise, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                        ResumeContinue = true;
                                        if (tmpStartCell == NStartCell)
                                        {
                                            NStartCell = null;
                                            if (i == 1)
                                            {
                                                if (SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                    //Information.Err().Raise(Constants.vbObjectError + 1010 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                                }
                                                else
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    tmpStartCell.Worksheet.Delete();
                                                }
                                            };
                                        }
                                        else
                                        {
                                            NotRevise = true;
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 4);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 4);

                                            if (NStartCell == null & !IsLastTable)
                                            {
                                                tmpNextTable = (GTTable)Tables[(int)i];
                                            }
                                            if (Information.UBound(TableStringValue, 2) < u)
                                            {
                                            }
                                        }
                                        if (!(tmpPageSetupStartCell == null))
                                        {
                                            f = !(NPageSetupStartCell == null);
                                            if (f)
                                                f = NPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                            if (f)
                                            {
                                                if (i == 1)
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    NPageSetupStartCell.Worksheet.Delete();
                                                }
                                                else
                                                {
                                                }
                                                NPageSetupStartCell = null;
                                            }
                                            else
                                            {
                                                PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 4);
                                                if (NPageSetupStartCell == null & !IsLastTable)
                                                {
                                                    tmpNextTable = (GTTable)Tables[(int)i];
                                                }
                                            }
                                        }
                                        ResumeContinue = false;
                                    }
                                    //DoEvents();
                                    if (!(PerStartCell == null))
                                    {
                                        tmpStartCell = PerStartCell;
                                        tmpPageSetupStartCell = PerPageSetupStartCell;
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                        OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, 0
                                                , Output.Orientation, strIdx
                                                , CutNA, CutIV, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.Per
                                                , FormatSheet, ref PerStartCell, ref PerPageSetupStartCell, IsLastTable
                                                , WorkingSheet, ref TableRange, ref PerRemainedPageSetupRowsCount, false, CutMedian, false, NotRevise, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                        ResumeContinue = true;
                                        if (tmpStartCell == PerStartCell)
                                        {
                                            PerStartCell = null;
                                            if (i == 1)
                                            {
                                                if (SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                    //Information.Err().Raise(Constants.vbObjectError + 1020 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                                }
                                                else
                                                {
                                                    xlApp.DisplayAlerts = false;
                                                    tmpStartCell.Worksheet.Delete();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 5);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 5);
                                            if (PerStartCell == null & !IsLastTable)
                                            {
                                                tmpNextTable = (GTTable)Tables[(int)i];

                                            }
                                            if (Information.UBound(TableStringValue, 2) < u)
                                            {
                                            }
                                        }
                                        if (!(tmpPageSetupStartCell == null))
                                        {
                                            f = !(PerPageSetupStartCell == null);
                                            if (f)
                                                f = PerPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                            if (f)
                                            {
                                                if (i == 1)
                                                {
                                                }
                                                else
                                                {
                                                }
                                                PerPageSetupStartCell = null;
                                            }
                                            else
                                            {
                                                PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 5);
                                                if (PerPageSetupStartCell == null & !IsLastTable)
                                                {
                                                    tmpNextTable = (GTTable)Tables[(int)i];
                                                }
                                            }
                                        }
                                        ResumeContinue = false;
                                    }
                                    //DoEvents();
                                }
                                else
                                {
                                }
                            }
                            else if ((withBlock1.Question.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                            {
                                // SA/MAマトリクス
                                //ro = 2 + (CrossCreator.ToInt(HasWeight) & 1);
                                ro = 2 + 1; // WT row cahnge
                                co = 2;
                                // 無回答/非該当の出力チェック
                                // 無回答出力設定でも0の場合は出力しない (元データにはある)
                                if (DefHasNA)
                                {
                                    NAIdx = (cu + System.Convert.ToInt64(DefHasIV) + (CrossCreator.ToInt(HasWeight) & -2)) - withBlock1.Question.SubTotalCnt;
                                    if (withBlock1.ParentRequest.ShowZeroNAIV)
                                        HasNA = true;
                                    else
                                    {
                                        HasNA = false;
                                        for (j = rl + ro; j <= ru; j++)
                                        {
                                            if (System.Convert.ToDouble(withBlock1.TableValue((int)j, (int)NAIdx)) != 0)
                                            {
                                                HasNA = true;
                                                break;
                                            }
                                        }
                                        CutNA = !HasNA;
                                    }
                                }
                                else
                                    NAIdx = (cl - 1) - withBlock1.Question.SubTotalCnt;
                                //MedIdx = cu - (CrossCreator.ToInt(DefHasIV) & 1) - (CrossCreator.ToInt(DefHasNA) & 1);
                                // 非該当出力設定でも0の場合は出力しない (元データにはある)
                                if (DefHasIV)
                                {
                                    IVIdx = cu + (CrossCreator.ToInt(HasWeight) & -2);
                                    if (withBlock1.ParentRequest.ShowZeroNAIV)
                                        HasIV = true;
                                    else
                                    {
                                        HasIV = false;
                                        for (j = rl + ro; j <= ru; j++)
                                        {
                                            if (System.Convert.ToDouble(withBlock1.TableValue((int)j, (int)IVIdx)) != 0)
                                            {
                                                HasIV = true;
                                                break;
                                            }
                                        }
                                        CutIV = !HasIV;
                                    }
                                }
                                else
                                    IVIdx = cl - 1;
                                // 出力する配列の生成
                                if (!(NPerStartCell == null))
                                {
                                    if (bgWorker.CancellationPending) return;
                                    if (Output.Orientation == TableOrientation.Portrait)
                                        CreatePortraitGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.NPer, CutMedian: CutMedian, MedIdx: MedIdx);
                                    else
                                        CreateLandscapeGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, ref OptionNumbersTop, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.NPer, CutMedian: CutMedian, MedIdx: MedIdx);
                                    u = Information.UBound(TableStringValue, 2);
                                    tmpStartCell = NPerStartCell;
                                    tmpPageSetupStartCell = NPerPageSetupStartCell;
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.NPer
                                            , FormatSheet, ref NPerStartCell, ref NPerPageSetupStartCell, IsLastTable
                                            , WorkingSheet, ref TableRange, ref NPerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers, OptionNumbersTop: OptionNumbersTop);

                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                    ResumeContinue = true;
                                    if (tmpStartCell == NPerStartCell)
                                    {
                                        NPerStartCell = null;
                                        if (i == 1)
                                        {
                                            if (NStartCell == null && PerStartCell == null && SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;
                                                //Information.Err().Raise(Constants.vbObjectError + 1000 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                            }
                                            else
                                            {
                                                xlApp.DisplayAlerts = false;
                                                tmpStartCell.Worksheet.Delete();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 3);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 3);
                                        if (NPerStartCell == null & !IsLastTable)
                                        {
                                            if (PerStartCell == null) GraphStartCell = null;
                                            tmpNextTable = (GTTable)Tables[(int)i];

                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {

                                        }
                                        else if (IsNPerSourceRange)
                                        {
                                            if (!(TableRange == null))
                                            {
                                                GraphSourceRangeCol = new VBA.Collection();
                                                GraphTableRangeCol = new VBA.Collection();
                                                nCol = new VBA.Collection();
                                                IsQCM = (withBlock1.Chart.ChartType & XlChartType.QCM) == XlChartType.QCM;
                                                if (!IsQCM)
                                                {
                                                    {
                                                        var withBlock2 = TableRange;
                                                        tmpGraphSourceRange = withBlock2.Range["B2"];
                                                        if (Output.Orientation == TableOrientation.Portrait)
                                                        {
                                                            tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2, withBlock2.Columns.Count - tmpGTTable.ChildQuestionsCount + 1].Resize(ColumnSize: tmpGTTable.ChildQuestionsCount));
                                                            {
                                                                var withBlock3 = withBlock2.Rows.Item[withBlock2.Rows.Count - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(HasIV) & 2) - (CrossCreator.ToInt(HasNA) & 2) - tmpGTTable.SectorsCount * 2 + 1].Resize(tmpGTTable.SectorsCount * 2 + (CrossCreator.ToInt(HasNA) & 2)).Cells;
                                                                NamesArray = new string[withBlock3.Rows.Count / 2 - 1 + 1];
                                                                for (n = 1; n <= withBlock3.Rows.Count - 1; n += 2)
                                                                {
                                                                    NamesArray[n / 2] = withBlock3.Item(n, 2).Value;
                                                                    tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock3.Item[n + 1, 2]);
                                                                }
                                                            }
                                                            {
                                                                var withBlock3 = withBlock2.Cells.Item[withBlock2.Rows.Count - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(HasIV) & 2) - (CrossCreator.ToInt(HasNA) & 2) - tmpGTTable.SectorsCount * 2 + 1, withBlock2.Columns.Count - tmpGTTable.ChildQuestionsCount + 1].Resize(tmpGTTable.SectorsCount * 2 + (CrossCreator.ToInt(HasNA) & 2), tmpGTTable.ChildQuestionsCount).Rows;
                                                                for (n = 2; n <= withBlock3.Rows.Count; n += 2)
                                                                    tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock3.Item[n]);
                                                            }
                                                            switch (tmpGTTable.Chart.ChartType)
                                                            {
                                                                case XlChartType.xlBarStacked:
                                                                case XlChartType.xlColumnStacked   // RNK
                                                               :
                                                                    {
                                                                        nCol.Add(withBlock2.Cells.Item[TotalRowIndex, withBlock2.Columns.Count - tmpGTTable.ChildQuestionsCount + 1].Value.ToString("0"));
                                                                        break;
                                                                    }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            NamesArray = new string[tmpGTTable.ChildQuestionsCount - 1 & +1];
                                                            j = 0;
                                                            for (n = withBlock2.Rows.Count - tmpGTTable.ChildQuestionsCount * 2 + 1; n <= withBlock2.Rows.Count - 1; n += 2)
                                                            {
                                                                NamesArray[j] = withBlock2.Item[n, 2].Value;
                                                                j = j + 1;
                                                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[n + 1, 2]);
                                                            }
                                                            {
                                                                var withBlock3 = withBlock2.Columns.Item[withBlock2.Columns.Count - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(HasIV) & 1) - (CrossCreator.ToInt(HasNA) & 1) - tmpGTTable.SectorsCount + 1].Resize(ColumnSize: tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)).Rows;
                                                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock3.Item[2]);

                                                                for (n = withBlock3.Count - tmpGTTable.ChildQuestionsCount * 2 + 2; n <= withBlock3.Count; n += 2)
                                                                    tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock3.Item[n]);
                                                            }
                                                            switch (tmpGTTable.Chart.ChartType)
                                                            {
                                                                case XlChartType.xlBarStacked:
                                                                case XlChartType.xlColumnStacked   // RNK
                                                               :
                                                                    {
                                                                        //nCol.Add(withBlock2.Cells.Item[3 + (CrossCreator.ToInt(HasWeight) & 1), TotalColumnIndex].Value.ToString("0"));
                                                                        nCol.Add(withBlock2.Cells.Item[3 + 1, TotalColumnIndex].Value.ToString("0"));// WT row cahnge
                                                                        break;
                                                                    }
                                                            }
                                                        }
                                                    }
                                                    GraphSourceRangeCol.Add(tmpGraphSourceRange);
                                                    GraphTableRangeCol.Add(TableRange);
                                                }
                                                else
                                                {
                                                    var withBlock2 = TableRange;
                                                    noReset = Output.Orientation == TableOrientation.Portrait;
                                                    for (n = 1; n <= tmpGTTable.ChildQuestionsCount; n++)
                                                    {
                                                        if (Output.Orientation == TableOrientation.Portrait)
                                                        {
                                                            tmpGraphSourceRange = withBlock2.Range["B2"];
                                                            tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2, withBlock2.Columns.Count - tmpGTTable.ChildQuestionsCount + n]);
                                                            {
                                                                var withBlock3 = withBlock2.Rows.Item[withBlock2.Rows.Count - (CrossCreator.ToInt(HasWeight) & 4) - (CrossCreator.ToInt(HasIV) & 2) - (CrossCreator.ToInt(HasNA) & 2) - tmpGTTable.SectorsCount * 2 + 1].Resize(tmpGTTable.SectorsCount * 2 + (CrossCreator.ToInt(HasNA) & 2)).Cells;
                                                                if (Information.UBound(NamesArray) == -1)
                                                                    NamesArray = new string[withBlock3.Rows.Count / 2 - 1 + 1];
                                                                for (x = 1; x <= withBlock3.Rows.Count - 1; x += 2)
                                                                {
                                                                    NamesArray[x / 2] = withBlock3.Item[x, 2].Value;
                                                                    tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock3.Item[x + 1, 2]);
                                                                }
                                                            }
                                                            {
                                                                var withBlock3 = withBlock2.Cells.Item[withBlock2.Rows.Count - (CrossCreator.ToInt(HasWeight) & 4) - (CrossCreator.ToInt(HasIV) & 2) - (CrossCreator.ToInt(HasNA) & 2) - tmpGTTable.SectorsCount * 2 + 1, withBlock2.Columns.Count - tmpGTTable.ChildQuestionsCount + n].Resize(tmpGTTable.SectorsCount * 2 + (CrossCreator.ToInt(HasNA) & 2));
                                                                for (x = 2; x <= withBlock3.Rows.Count; x += 2)
                                                                    tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock3.Item[x, 1]);

                                                                nCol.Add(withBlock3.Item(0, 1).Value.ToString("0"));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            tmpGraphSourceRange = withBlock2.Item[withBlock2.Rows.Count - tmpGTTable.ChildQuestionsCount * 2 - 1 + n * 2, 2];
                                                            {
                                                                var withBlock3 = withBlock2.Columns.Item[withBlock2.Columns.Count - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(HasIV) & 1) - (CrossCreator.ToInt(HasNA) & 1) - tmpGTTable.SectorsCount + 1].Resize(ColumnSize: tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)).Rows;
                                                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange
                                                                          , withBlock3.Item(2)
                                                                          , withBlock3.Item(withBlock3.Count - tmpGTTable.ChildQuestionsCount * 2 + n * 2));
                                                                nCol.Add(withBlock3.Cells.Item(withBlock3.Count - tmpGTTable.ChildQuestionsCount * 2 - 1 + n * 2, 0).Value.ToString("0"));
                                                            }
                                                        }
                                                        GraphSourceRangeCol.Add(tmpGraphSourceRange);
                                                        GraphTableRangeCol.Add(TableRange);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(NPerPageSetupStartCell == null);
                                        if (f)
                                            f = NPerPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                        if (f)
                                        {
                                            if (i == 1)
                                            {
                                                xlApp.DisplayAlerts = false;
                                                NPerPageSetupStartCell.Worksheet.Delete();
                                            }
                                            else
                                            {
                                            }
                                            NPerPageSetupStartCell = null;
                                        }
                                        else
                                        {
                                            PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 3);
                                            if (NPerPageSetupStartCell == null & !IsLastTable)
                                            {
                                                tmpNextTable = (GTTable)Tables[(int)i];
                                            }
                                        }
                                    }
                                    ResumeContinue = false;
                                }
                                //DoEvents();
                                if (!(NStartCell == null))
                                {
                                    if (bgWorker.CancellationPending) return;
                                    if (Output.Orientation == TableOrientation.Portrait)
                                        CreatePortraitGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.N, CutMedian: CutMedian, MedIdx: MedIdx);
                                    else
                                        CreateLandscapeGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, ref OptionNumbersTop, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.N, CutMedian: CutMedian, MedIdx: MedIdx);
                                    u = Information.UBound(TableStringValue, 2);
                                    tmpStartCell = NStartCell;
                                    tmpPageSetupStartCell = NPageSetupStartCell;
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.N
                                            , FormatSheet, ref NStartCell, ref NPageSetupStartCell, IsLastTable
                                            , WorkingSheet, ref TableRange, ref NRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers, OptionNumbersTop: OptionNumbersTop);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                    ResumeContinue = true;
                                    if (tmpStartCell == NStartCell)
                                    {
                                        NStartCell = null;
                                        if (i == 1)
                                        {
                                            if (SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;
                                                //Information.Err().Raise(Constants.vbObjectError + 1010, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                            }
                                            else
                                            {
                                                xlApp.DisplayAlerts = false;
                                                tmpStartCell.Worksheet.Delete();
                                            }
                                        }

                                    }
                                    else
                                    {
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 4);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 4);
                                        if (NStartCell == null & !IsLastTable)
                                        {
                                            tmpNextTable = (GTTable)Tables[(int)i];
                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {
                                        }
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(NPageSetupStartCell == null);
                                        if (f)
                                            f = NPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                        if (f)
                                        {
                                            if (i == 1)
                                            {
                                                xlApp.DisplayAlerts = false;
                                                NPageSetupStartCell.Worksheet.Delete();
                                            }
                                            else
                                            {
                                            }
                                            NPageSetupStartCell = null;
                                        }
                                        else
                                        {
                                            PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 4);
                                            if (NPageSetupStartCell == null & !IsLastTable)
                                            {
                                                tmpNextTable = (GTTable)Tables[(int)i];

                                            }
                                        }
                                    }
                                    ResumeContinue = false;
                                }
                                //DoEvents();
                                if (!(PerStartCell == null))
                                {
                                    if (bgWorker.CancellationPending) return;
                                    if (Output.Orientation == TableOrientation.Portrait)
                                        CreatePortraitGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.Per, CutMedian: CutMedian, MedIdx: MedIdx);
                                    else
                                        CreateLandscapeGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, ref OptionNumbersTop, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.Per, CutMedian: CutMedian, MedIdx: MedIdx);
                                    u = Information.UBound(TableStringValue, 2);
                                    tmpStartCell = PerStartCell;
                                    tmpPageSetupStartCell = PerPageSetupStartCell;
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);

                                    OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.Per
                                            , FormatSheet, ref PerStartCell, ref PerPageSetupStartCell, IsLastTable
                                            , WorkingSheet, ref TableRange, ref PerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers, OptionNumbersTop: OptionNumbersTop);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                    ResumeContinue = true;
                                    if (tmpStartCell == PerStartCell)
                                    {
                                        PerStartCell = null;
                                        if (i == 1)
                                        {
                                            if (SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;
                                                //Information.Err().Raise(Constants.vbObjectError + 1020 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                            }
                                            else
                                            {
                                                xlApp.DisplayAlerts = false;
                                                tmpStartCell.Worksheet.Delete();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 5);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 5);
                                        if (PerStartCell == null & !IsLastTable)
                                        {
                                            GraphStartCell = null;
                                            tmpNextTable = (GTTable)Tables[(int)i];

                                            //Information.Err().Raise(Constants.vbObjectError + 320 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(IIf(i + 1 & == Tables.Count, ReportMessageIndex_ReportRowsCountOverDetailPMessageIndex, ReportMessageIndex_ReportRowsCountOverDetailPOnAfterMessageIndex)
                                            //       , tmpNextTable.Question.Name));
                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {
                                            //Information.Err().Raise(Constants.vbObjectError + 420 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailPMessageIndex, withBlock1.Question.Name));
                                        }
                                        else if (!(TableRange == null))
                                        {
                                            GraphSourceRangeCol = new Collection();
                                            GraphTableRangeCol = new Collection();
                                            nCol = new Collection();

                                            IsQCM = (withBlock1.Chart.ChartType & XlChartType.QCM) == XlChartType.QCM;
                                            if (!IsQCM)
                                            {
                                                {
                                                    var withBlock2 = TableRange;
                                                    tmpGraphSourceRange = withBlock2.Range["B2"];
                                                    if (Output.Orientation == TableOrientation.Portrait)
                                                    {
                                                        n = withBlock2.Columns.Count - tmpGTTable.ChildQuestionsCount + 1;
                                                        tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2, n].Resize(ColumnSize: tmpGTTable.ChildQuestionsCount));
                                                        {
                                                            var withBlock3 = withBlock2.Rows.Item[withBlock2.Rows.Count - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(HasIV) & 1) - (CrossCreator.ToInt(HasNA) & 1) - tmpGTTable.SectorsCount + 1].Resize(tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)).Columns;
                                                            tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock3.Item[2]);
                                                            tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock3.Item(n).Resize(ColumnSize: tmpGTTable.ChildQuestionsCount));
                                                        }
                                                        switch (tmpGTTable.Chart.ChartType)
                                                        {
                                                            case XlChartType.xlBarStacked:
                                                            case XlChartType.xlColumnStacked:   // RNK
                                                                {
                                                                    nCol.Add(withBlock2.Cells.Item[TotalRowIndex, withBlock2.Columns.Count - tmpGTTable.ChildQuestionsCount + 1].Value.ToString("0"));
                                                                    break;
                                                                }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        n = withBlock2.Rows.Count - tmpGTTable.ChildQuestionsCount + 1;
                                                        tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[n, 2].Resize(tmpGTTable.ChildQuestionsCount));
                                                        {
                                                            var withBlock3 = withBlock2.Columns.Item[withBlock2.Columns.Count - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(HasIV) & 1) - (CrossCreator.ToInt(HasNA) & 1) - tmpGTTable.SectorsCount + 1].Resize(ColumnSize: tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)).Rows;
                                                            tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock3.Item[2]);
                                                            tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock3.Item[n].Resize(tmpGTTable.ChildQuestionsCount));
                                                        }
                                                        switch (tmpGTTable.Chart.ChartType)
                                                        {
                                                            case XlChartType.xlBarStacked:
                                                            case XlChartType.xlColumnStacked:   // RNK
                                                                {
                                                                    string rngVal = Convert.ToString(withBlock2.Cells.Item[n, TotalColumnIndex].Value);
                                                                    //nCol.Add(rngVal.ToString("0"));                                                                   
                                                                    if (rngVal != null)
                                                                        nCol.Add(withBlock2.Cells.Item[n, TotalColumnIndex].Value.ToString("0"));
                                                                    else
                                                                        nCol.Add(0.ToString("0"));
                                                                    break;
                                                                }
                                                        }
                                                    }
                                                }
                                                GraphSourceRangeCol.Add(tmpGraphSourceRange);
                                                GraphTableRangeCol.Add(TableRange);
                                            }
                                            else
                                            {
                                                var withBlock2 = TableRange;
                                                for (n = 1; n <= tmpGTTable.ChildQuestionsCount; n++)
                                                {
                                                    if (Output.Orientation == TableOrientation.Portrait)
                                                    {
                                                        tmpGraphSourceRange = withBlock2.Item[2, withBlock2.Columns.Count - tmpGTTable.ChildQuestionsCount + n];
                                                        {
                                                            var withBlock3 = withBlock2.Rows.Item[withBlock2.Rows.Count - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(HasIV) & 1) - (CrossCreator.ToInt(HasNA) & 1) - tmpGTTable.SectorsCount + 1].Resize(tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)).Columns;
                                                            tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange
                                                            , withBlock3.Item[2]
                                                            , withBlock3.Item[withBlock3.Count - tmpGTTable.ChildQuestionsCount + n]);
                                                            nCol.Add(withBlock3.Cells.Item(0, withBlock3.Count - tmpGTTable.ChildQuestionsCount + n).Value.ToString("0"));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        tmpGraphSourceRange = withBlock2.Item[withBlock2.Rows.Count - tmpGTTable.ChildQuestionsCount + n, 2];
                                                        {
                                                            var withBlock3 = withBlock2.Columns.Item[withBlock2.Columns.Count - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(HasIV) & 1) - (CrossCreator.ToInt(HasNA) & 1) - tmpGTTable.SectorsCount + 1].Resize(ColumnSize: tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)).Rows;
                                                            tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange
                                                            , withBlock3.Item[2]
                                                            , withBlock3.Item[withBlock3.Count - tmpGTTable.ChildQuestionsCount + n]);
                                                            nCol.Add(withBlock3.Cells.Item(withBlock3.Count - tmpGTTable.ChildQuestionsCount + n, 0).Value.ToString("0"));
                                                        }
                                                    }
                                                    GraphSourceRangeCol.Add(tmpGraphSourceRange);
                                                    GraphTableRangeCol.Add(TableRange);
                                                }
                                            }
                                        }
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(PerPageSetupStartCell == null);
                                        if (f)
                                            f = PerPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                        if (f)
                                        {
                                            if (i == 1)
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 321 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportRowsCountOverPageSetuppedPMessageIndex));
                                                xlApp.DisplayAlerts = false;
                                                PerPageSetupStartCell.Worksheet.Delete();
                                            }
                                            else
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 321 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastTable, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedPMessageIndex
                                                //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedPOnAfterMessageIndex)
                                                //       , withBlock1.Question.Name));
                                            }
                                            PerPageSetupStartCell = null;
                                        }
                                        else
                                        {
                                            PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 5);
                                            if (PerPageSetupStartCell == null & !IsLastTable)
                                            {
                                                tmpNextTable = (GTTable)Tables[(int)i];
                                                //Information.Err().Raise(Constants.vbObjectError + 321 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(IIf(i + 1 & == Tables.Count, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedPMessageIndex
                                                //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedPOnAfterMessageIndex)
                                                //       , tmpNextTable.Question.Name));
                                            }
                                        }
                                    }
                                    ResumeContinue = false;
                                }
                                //DoEvents();
                                if (!(SigTestStartCell == null) && tmpGTTable.SignificancetestCode != SignificanceTestCode.Off)
                                {
                                    if (bgWorker.CancellationPending) return;
                                    if (Output.Orientation == TableOrientation.Portrait)
                                    {
                                        AddLetterColumn = tmpGTTable.SignificancetestCode == SignificanceTestCode.BetweenChildQuestions & (HasNormalSigTest | HasMatrixBetweenSectorSigTest);
                                        CreatePortraitGTArray(tmpGTTable, rl, ru, cl, cu
                                                , ref TableStringValue, ref DataValue, ref Ranking, HasNA, HasIV, NAIdx, IVIdx
                                                , AddColumnCount + (CrossCreator.ToInt(AddLetterColumn) & 1), ro, co, TableType.SignificanceTest, false, CutMedian, MedIdx, AddLetterColumn);
                                    }
                                    else
                                    {
                                        AddLetterColumn = tmpGTTable.SignificancetestCode == SignificanceTestCode.BetweenSectors & (HasNormalSigTest | HasMatrixBetweenChildSigTest);
                                        CreateLandscapeGTArray(tmpGTTable, rl, ru, cl, cu
                                                , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, ref OptionNumbersTop, HasNA, HasIV, NAIdx, IVIdx
                                                , AddColumnCount + (CrossCreator.ToInt(AddLetterColumn) & 1), ro, co, TableType.SignificanceTest, false, CutMedian, MedIdx, AddLetterColumn);
                                    }
                                    u = Information.UBound(TableStringValue, 2);
                                    tmpStartCell = SigTestStartCell;
                                    tmpPageSetupStartCell = SigTestPageSetupStartCell;
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    IsLastSigTestTable = true;
                                    IsNextLastSigTestTable = true;
                                    NextSigTestTable = null;
                                    for (j = i; j <= Tables.Count - 1; j++)
                                    {
                                        tmpNextTable = (GTTable)Tables[(int)j];
                                        if (tmpNextTable.SignificancetestCode != SignificanceTestCode.Off)
                                        {
                                            if (IsLastSigTestTable)
                                            {
                                                IsLastSigTestTable = false;
                                                NextSigTestTable = tmpNextTable;
                                            }
                                            else
                                            {
                                                IsNextLastSigTestTable = false;
                                                break;
                                            }
                                        }
                                    }
                                    OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.SignificanceTest
                                            , SigTestFormatSheet, ref SigTestStartCell, ref SigTestPageSetupStartCell, IsLastSigTestTable
                                            , WorkingSheet, ref TableRange, ref SigTestRemainedPageSetupRowsCount, false, CutMedian, HasLetterColumn, OptionNumbers: OptionNumbers, OptionNumbersTop: OptionNumbersTop);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                    ResumeContinue = true;
                                    if (tmpStartCell == SigTestStartCell)
                                    {
                                        SigTestStartCell = null;
                                        if (IsFirstSigTestTable)
                                        {
                                            if (NPerStartCell == null && NStartCell == null && PerStartCell == null)
                                            {
                                                ResumeContinue = false;
                                                //Information.Err().Raise(Constants.vbObjectError + 1030 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                            }
                                            else
                                            {
                                                xlApp.DisplayAlerts = false;
                                                tmpStartCell.Worksheet.Delete();
                                            }
                                        }
                                        //Information.Err().Raise(Constants.vbObjectError + 330 &, RunningProcName
                                        //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastSigTestTable, ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestMessageIndex
                                        //                               , ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestOnAfterMessageIndex)
                                        //       , withBlock1.Question.Name));
                                    }
                                    else
                                    {
                                        if (SigTestStartCell == null && !IsLastSigTestTable)
                                        {
                                            //Information.Err().Raise(Constants.vbObjectError + 330 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(IIf(IsNextLastSigTestTable, ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestMessageIndex
                                            //                        , ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestOnAfterMessageIndex)
                                            //       , NextSigTestTable.Question.Name));
                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {

                                            //Information.Err().Raise(Constants.vbObjectError + 430 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailSignificanceTestMessageIndex, withBlock1.Question.Name));
                                        }
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(SigTestPageSetupStartCell == null);
                                        if (f)
                                            f = SigTestPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                        if (f)
                                        {
                                            if (IsFirstSigTestTable)
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 331 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportRowsCountOverPageSetuppedSignificanceTestMessageIndex));
                                                xlApp.DisplayAlerts = false;
                                                SigTestPageSetupStartCell.Worksheet.Delete();
                                            }
                                            else
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 331 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastSigTestTable, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedSignificanceTestMessageIndex
                                                //                               , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedSignificanceTestOnAfterMessageIndex)
                                                //       , withBlock1.Question.Name));
                                            }
                                            SigTestPageSetupStartCell = null;
                                        }
                                        else if (SigTestPageSetupStartCell == null && !IsLastSigTestTable)
                                        {
                                            //Information.Err().Raise(Constants.vbObjectError + 331 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(IIf(IsNextLastSigTestTable, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedSignificanceTestMessageIndex
                                            //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedSignificanceTestOnAfterMessageIndex)
                                            //       , NextSigTestTable.Question.Name));
                                        }
                                    }
                                    if (IsLastSigTestTable)
                                    {
                                        SigTestStartCell = null;
                                        SigTestPageSetupStartCell = null;
                                    }
                                    ResumeContinue = false;
                                    IsFirstSigTestTable = false;
                                }
                                //DoEvents();
                            }
                            else if (isN)
                            {
                                // Nマトリクス
                                ro = 2;
                                co = 2;
                                // 無回答/非該当の出力チェック
                                // 無回答出力設定でも0の場合は出力しない (元データにはある)
                                if (DefHasNA)
                                {
                                    NAIdx = (cu + System.Convert.ToInt64(DefHasIV)) - withBlock1.Question.SubTotalCnt;
                                    //if (withBlock1.ParentRequest.ShowZeroNAIV)
                                    if (true)
                                        HasNA = true;
                                    else
                                    {
                                        HasNA = false;
                                        for (j = rl + ro; j <= ru; j++)
                                        {
                                            if (System.Convert.ToDouble(withBlock1.TableValue((int)j, (int)NAIdx)) != 0)
                                            {
                                                HasNA = true;
                                                break;
                                            }
                                        }
                                    }
                                    CutNA = !HasNA;
                                }
                                else
                                    NAIdx = (cl - 1) - withBlock1.Question.SubTotalCnt;

                                MedIdx = cu - (CrossCreator.ToInt(DefHasIV) & 1) - (CrossCreator.ToInt(DefHasNA) & 1);
                                // 非該当出力設定でも0の場合は出力しない (元データにはある)
                                if (DefHasIV)
                                {
                                    IVIdx = cu;
                                    if (withBlock1.ParentRequest.ShowZeroNAIV)
                                        HasIV = true;
                                    else
                                    {
                                        HasIV = false;
                                        for (j = rl + ro; j <= ru; j++)
                                        {
                                            if (System.Convert.ToDouble(withBlock1.TableValue((int)j, (int)IVIdx)) != 0)
                                            {
                                                HasIV = true;
                                                break;
                                            }
                                        }
                                    }
                                    CutIV = !HasIV;
                                }
                                else
                                    IVIdx = cl - 1;
                                // 出力する配列の生成
                                if (!(NPerStartCell == null) || !(NStartCell == null) || !(PerStartCell == null))
                                {
                                    if (Output.Orientation == TableOrientation.Portrait)
                                        CreatePortraitGTArray(tmpGTTable, rl, ru, cl, cu
                                    , ref TableStringValue, ref DataValue, ref Ranking, HasNA, HasIV, NAIdx, IVIdx
                                    , AddColumnCount, ro, co, TableType.N, CutMedian: CutMedian, MedIdx: MedIdx);
                                    else
                                        CreateLandscapeGTArray(tmpGTTable, rl, ru, cl, cu
     , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, ref OptionNumbersTop, HasNA, HasIV, NAIdx, IVIdx, AddColumnCount, ro, co, TableType.N, CutMedian: CutMedian, MedIdx: MedIdx);
                                    u = Information.UBound(TableStringValue, 2);
                                }

                                NotRevise = false;
                                if (!(NPerStartCell == null))
                                {
                                    if (bgWorker.CancellationPending) return;
                                    tmpStartCell = NPerStartCell;
                                    tmpPageSetupStartCell = NPerPageSetupStartCell;

                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.NPer
                                            , FormatSheet, ref NPerStartCell, ref NPerPageSetupStartCell, IsLastTable
                                            , WorkingSheet, ref TableRange, ref NPerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                    ResumeContinue = true;
                                    if (tmpStartCell == NPerStartCell)
                                    {
                                        NPerStartCell = null;
                                        if (i == 1)
                                        {
                                            if (NStartCell == null & PerStartCell == null & SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;
                                                //Information.Err().Raise(Constants.vbObjectError + 1000 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                            }
                                            else
                                            {
                                                xlApp.DisplayAlerts = false;
                                                tmpStartCell.Worksheet.Delete();
                                            }
                                        }
                                        //Information.Err().Raise(Constants.vbObjectError + 300 &, RunningProcName
                                        //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastTable, ReportMessageIndex_ReportRowsCountOverDetailNPMessageIndex, ReportMessageIndex_ReportRowsCountOverDetailNPOnAfterMessageIndex)
                                        //       , withBlock1.Question.Name));
                                    }
                                    else
                                    {
                                        NotRevise = true;
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 3);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 3);
                                        if (NPerStartCell == null & !IsLastTable)
                                        {
                                            if (PerStartCell == null)
                                                GraphStartCell = null;
                                            tmpNextTable = (GTTable)Tables[(int)i];

                                            //Information.Err().Raise(Constants.vbObjectError + 300 &, RunningProcName
                                            //   , ThisWorkbook.yoyoReportKeyword(IIf(i + 1 == Tables.Count, ReportMessageIndex_ReportRowsCountOverDetailNPMessageIndex, ReportMessageIndex_ReportRowsCountOverDetailNPOnAfterMessageIndex)
                                            //   , tmpNextTable.Question.Name));
                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {
                                            //Information.Err().Raise(Constants.vbObjectError + 400 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailNPMessageIndex, withBlock1.Question.Name));
                                        }
                                        else if ((withBlock1.Question.QuestionType & QuestionType.Ratio) == QuestionType.Ratio)
                                        {
                                            if (IsNPerSourceRange)
                                                SetRatSourceRange(tmpGTTable, TableRange, ref GraphSourceRangeCol, ref GraphTableRangeCol, ref nCol);
                                        }
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(NPerPageSetupStartCell == null);
                                        if (f)
                                            f = NPerPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                        if (f)
                                        {
                                            if (i == 1)
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 301 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportRowsCountOverPageSetuppedNPMessageIndex));
                                                //NPerPageSetupStartCell.Application.DisplayAlerts = false;
                                                //NPerPageSetupStartCell.Worksheet.Delete();
                                            }
                                            else
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 301 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastTable, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedNPMessageIndex
                                                //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedNPOnAfterMessageIndex)
                                                //       , withBlock1.Question.Name));
                                            }
                                            NPerPageSetupStartCell = null;
                                        }
                                        else
                                        {
                                            PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 3);
                                            if (NPerPageSetupStartCell == null && !IsLastTable)
                                            {
                                                tmpNextTable = (GTTable)Tables[(int)i];
                                                //Information.Err().Raise(Constants.vbObjectError + 301 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(IIf(i + 1 & == Tables.Count, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedNPMessageIndex
                                                //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedNPOnAfterMessageIndex)
                                                //       , tmpNextTable.Question.Name));
                                            }
                                        }
                                    }
                                    ResumeContinue = false;
                                }
                                //DoEvents();
                                if (!(NStartCell == null))
                                {
                                    if (bgWorker.CancellationPending) return;
                                    tmpStartCell = NStartCell;
                                    tmpPageSetupStartCell = NPageSetupStartCell;

                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.N
                                            , FormatSheet, ref NStartCell, ref NPageSetupStartCell, IsLastTable
                                            , WorkingSheet, ref TableRange, ref NRemainedPageSetupRowsCount, false, CutMedian, false, NotRevise, OptionNumbers: OptionNumbers);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                    ResumeContinue = true;
                                    if (tmpStartCell == NStartCell)
                                    {
                                        NStartCell = null;
                                        if (i == 1)
                                        {
                                            if (SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;
                                                //Information.Err().Raise(Constants.vbObjectError + 1010 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                            }
                                            else
                                            {
                                                xlApp.DisplayAlerts = false;
                                                tmpStartCell.Worksheet.Delete();
                                            }
                                        }
                                        //Information.Err().Raise(Constants.vbObjectError + 310 &, RunningProcName
                                        //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastTable, ReportMessageIndex_ReportRowsCountOverDetailNMessageIndex, ReportMessageIndex_ReportRowsCountOverDetailNOnAfterMessageIndex)
                                        //       , withBlock1.Question.Name));
                                    }
                                    else
                                    {
                                        NotRevise = true;
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 4);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 4);
                                        if (NStartCell == null && !IsLastTable)
                                        {
                                            tmpNextTable = (GTTable)Tables[(int)i];
                                            //Information.Err().Raise(Constants.vbObjectError + 310 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(IIf(i + 1 & == Tables.Count, ReportMessageIndex_ReportRowsCountOverDetailNMessageIndex, ReportMessageIndex_ReportRowsCountOverDetailNOnAfterMessageIndex)
                                            //       , tmpNextTable.Question.Name));
                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {
                                            //Information.Err().Raise(Constants.vbObjectError + 410 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailNMessageIndex, withBlock1.Question.Name));
                                        }
                                        else if ((withBlock1.Question.QuestionType & QuestionType.Ratio) == QuestionType.Ratio)
                                        {
                                            if (IsNSourceRange)
                                                SetRatSourceRange(tmpGTTable, TableRange, ref GraphSourceRangeCol, ref GraphTableRangeCol, ref nCol);
                                        }
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(NPageSetupStartCell == null);
                                        if (f)
                                            f = NPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                        if (f)
                                        {
                                            if (i == 1)
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 311 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportRowsCountOverPageSetuppedNMessageIndex));
                                                //NPageSetupStartCell.Application.DisplayAlerts = false;
                                                //NPageSetupStartCell.Worksheet.Delete();
                                            }
                                            else
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 311 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastTable, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedNMessageIndex
                                                //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedNOnAfterMessageIndex)
                                                //       , withBlock1.Question.Name));
                                            }
                                            NPageSetupStartCell = null;
                                        }
                                        else
                                        {
                                            PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 4);
                                            if (NPageSetupStartCell == null & !IsLastTable)
                                            {
                                                tmpNextTable = (GTTable)Tables[(int)i];
                                                //Information.Err().Raise(Constants.vbObjectError + 311 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(IIf(i + 1 & == Tables.Count, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedNMessageIndex
                                                //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedNOnAfterMessageIndex)
                                                //       , tmpNextTable.Question.Name));
                                            }
                                        }
                                    }
                                    ResumeContinue = false;
                                }
                                //DoEvents();
                                if (!(PerStartCell == null))
                                {
                                    if (bgWorker.CancellationPending) return;
                                    tmpStartCell = PerStartCell;
                                    tmpPageSetupStartCell = PerPageSetupStartCell;

                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.Per
                                            , FormatSheet, ref PerStartCell, ref PerPageSetupStartCell, IsLastTable
                                            , WorkingSheet, ref TableRange, ref PerRemainedPageSetupRowsCount, false, CutMedian, false, NotRevise, OptionNumbers: OptionNumbers);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                    ResumeContinue = true;
                                    if (tmpStartCell == PerStartCell)
                                    {
                                        PerStartCell = null;
                                        if (i == 1)
                                        {
                                            if (SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;
                                                //Information.Err().Raise(Constants.vbObjectError + 1020 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                            }
                                            else
                                            {
                                                xlApp.DisplayAlerts = false;
                                                tmpStartCell.Worksheet.Delete();
                                            }
                                        }
                                        //Information.Err().Raise(Constants.vbObjectError + 320 &, RunningProcName
                                        //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastTable, ReportMessageIndex_ReportRowsCountOverDetailPMessageIndex, ReportMessageIndex_ReportRowsCountOverDetailPOnAfterMessageIndex)
                                        //       , withBlock1.Question.Name));
                                    }
                                    else
                                    {
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 5);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 5);
                                        if (PerStartCell == null && !IsLastTable)
                                        {
                                            GraphStartCell = null;
                                            tmpNextTable = (GTTable)Tables[(int)i];

                                            //Information.Err().Raise(Constants.vbObjectError + 320 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(IIf(i + 1 & == Tables.Count, ReportMessageIndex_ReportRowsCountOverDetailPMessageIndex, ReportMessageIndex_ReportRowsCountOverDetailPOnAfterMessageIndex)
                                            //       , tmpNextTable.Question.Name));
                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {
                                            //Information.Err().Raise(Constants.vbObjectError + 420 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailPMessageIndex, withBlock1.Question.Name));
                                        }
                                        else if ((withBlock1.Question.QuestionType & QuestionType.Ratio) == QuestionType.Ratio)
                                            SetRatSourceRange(tmpGTTable, TableRange, ref GraphSourceRangeCol, ref GraphTableRangeCol, ref nCol);
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(PerPageSetupStartCell == null);
                                        if (f)
                                            f = PerPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                        if (f)
                                        {
                                            if (i == 1)
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 321 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportRowsCountOverPageSetuppedPMessageIndex));
                                                //PerPageSetupStartCell.Application.DisplayAlerts = false;
                                                //PerPageSetupStartCell.Worksheet.Delete();
                                            }
                                            else
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 321 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastTable, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedPMessageIndex
                                                //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedPOnAfterMessageIndex)
                                                //       , withBlock1.Question.Name));
                                            }
                                            PerPageSetupStartCell = null;
                                        }
                                        else
                                        {
                                            PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 5);
                                            if (PerPageSetupStartCell == null & !IsLastTable)
                                            {
                                                tmpNextTable = (GTTable)Tables[(int)i];

                                                //Information.Err().Raise(Constants.vbObjectError + 321 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(IIf(i + 1 & == Tables.Count, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedPMessageIndex
                                                //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedPOnAfterMessageIndex)
                                                //       , tmpNextTable.Question.Name));
                                            }
                                        }
                                    }
                                    ResumeContinue = false;
                                }
                                //DoEvents();
                                if (!(SigTestStartCell == null) && tmpGTTable.SignificancetestCode == SignificanceTestCode.BetweenChildQuestions)
                                {
                                    if (bgWorker.CancellationPending) return;
                                    if (Output.Orientation == TableOrientation.Portrait)
                                    {
                                        AddLetterColumn = HasNormalSigTest | HasMatrixBetweenSectorSigTest;
                                        CreatePortraitGTArray(tmpGTTable, rl, ru, cl, cu
                                                , ref TableStringValue, ref DataValue, ref Ranking, HasNA, HasIV, NAIdx, IVIdx
                                                , AddColumnCount + (CrossCreator.ToInt(AddLetterColumn) & 1), ro, co, TableType.SignificanceTest, false, CutMedian, MedIdx, AddLetterColumn);
                                    }
                                    else
                                        CreateLandscapeGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, ref OptionNumbersTop, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.SignificanceTest, CutMedian: CutMedian, MedIdx: MedIdx);
                                    u = Information.UBound(TableStringValue, 2);
                                    tmpStartCell = SigTestStartCell;
                                    tmpPageSetupStartCell = SigTestPageSetupStartCell;
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell.Address;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    IsLastSigTestTable = true;
                                    IsNextLastSigTestTable = true;
                                    NextSigTestTable = null;
                                    for (j = i; j <= Tables.Count - 1; j++)
                                    {
                                        tmpNextTable = (GTTable)Tables[(int)j];
                                        if (tmpNextTable.SignificancetestCode != SignificanceTestCode.Off)
                                        {
                                            if (IsLastSigTestTable)
                                            {
                                                IsLastSigTestTable = false;
                                                NextSigTestTable = tmpNextTable;
                                            }
                                            else
                                            {
                                                IsNextLastSigTestTable = false;
                                                break;
                                            }
                                        }
                                    }
                                    OutputData(tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.SignificanceTest
                                            , SigTestFormatSheet, ref SigTestStartCell, ref SigTestPageSetupStartCell, IsLastSigTestTable
                                            , WorkingSheet, ref TableRange, ref SigTestRemainedPageSetupRowsCount, false, CutMedian, HasLetterColumn, OptionNumbers: OptionNumbers);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = xlApp.Range[tmpAddress];
                                    ResumeContinue = true;
                                    if (tmpStartCell == SigTestStartCell)
                                    {
                                        SigTestStartCell = null;
                                        if (IsFirstSigTestTable)
                                        {
                                            if (NPerStartCell == null & NStartCell == null & PerStartCell == null)
                                            {
                                                ResumeContinue = false;
                                                //Information.Err().Raise(Constants.vbObjectError + 1030 &, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex));
                                            }
                                            else
                                            {
                                                xlApp.DisplayAlerts = false;
                                                tmpStartCell.Worksheet.Delete();
                                            }
                                        }
                                        //Information.Err().Raise(Constants.vbObjectError + 330 &, RunningProcName
                                        //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastSigTestTable, ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestMessageIndex
                                        //                               , ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestOnAfterMessageIndex)
                                        //       , withBlock1.Question.Name));
                                    }
                                    else
                                    {
                                        if (SigTestStartCell == null && !IsLastSigTestTable)
                                        {
                                            //Information.Err().Raise(Constants.vbObjectError + 330 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(IIf(IsNextLastSigTestTable, ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestMessageIndex
                                            //                        , ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestOnAfterMessageIndex)
                                            //       , NextSigTestTable.Question.Name));
                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {
                                            //Information.Err().Raise(Constants.vbObjectError + 430 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailSignificanceTestMessageIndex, withBlock1.Question.Name));
                                        }
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(SigTestPageSetupStartCell == null);
                                        if (f)
                                            f = SigTestPageSetupStartCell.Address == tmpPageSetupStartCell.Address;
                                        if (f)
                                        {
                                            if (IsFirstSigTestTable)
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 331 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportRowsCountOverPageSetuppedSignificanceTestMessageIndex));
                                                xlApp.DisplayAlerts = false;
                                                SigTestPageSetupStartCell.Worksheet.Delete();
                                            }
                                            else
                                            {
                                                //Information.Err().Raise(Constants.vbObjectError + 331 &, RunningProcName
                                                //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastSigTestTable, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedSignificanceTestMessageIndex
                                                //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedSignificanceTestOnAfterMessageIndex)
                                                //       , withBlock1.Question.Name));
                                            }
                                            SigTestPageSetupStartCell = null;
                                        }
                                        else if (SigTestPageSetupStartCell == null && !IsLastSigTestTable)
                                        {
                                            //Information.Err().Raise(Constants.vbObjectError + 331 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(IIf(IsNextLastSigTestTable, ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedSignificanceTestMessageIndex
                                            //                        , ReportMessageIndex_ReportRowsCountOverDetailPageSetuppedSignificanceTestOnAfterMessageIndex)
                                            //       , NextSigTestTable.Question.Name));
                                        }
                                    }
                                    if (IsLastSigTestTable)
                                    {
                                        SigTestStartCell = null;
                                        SigTestPageSetupStartCell = null;
                                    }
                                    ResumeContinue = false;
                                    IsFirstSigTestTable = false;
                                }
                                //DoEvents();
                            }
                            else
                            {
                                // エラースロー
                                //Information.Err().Raise(Constants.vbObjectError + 100 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportUnjustQuestionTypeMessageIndex));
                            }
                            _log.Info("table created chart started:" + tmpGTTable.Question.Name);
                            ChartObjectCol = new Collection();
                            tmpChartObject = null;
                            if (!(GraphSourceRangeCol == null || GraphStartCell == null || GraphSourceRangeCol.Count == 0 || withBlock1.Chart == null || withBlock1.Chart.ChartType == 0))
                            {
                                // グラフ設定とGraphStartCellの更新
                                string chtCaption;
                                Excel.XlChartType chtType;
                                XlRowCol plotby;
                                XlRowCol tmpPlotby;
                                chtCaption = "[" + withBlock1.Question.Name + "]" + withBlock1.Question.Description;
                                chtType = (Microsoft.Office.Interop.Excel.XlChartType)(withBlock1.Chart.ChartType & ~(XlChartType.QCM | XlChartType.RAT));
                                do
                                {
                                    if (bgWorker.CancellationPending) return;
                                    if (isMatrix)
                                    {
                                        if (isN)
                                        {
                                            if ((withBlock1.Question.QuestionType & QuestionType.Ratio) == 0)
                                                break;
                                            // RAT
                                            switch (chtType)
                                            {
                                                case Excel.XlChartType.xlPie:
                                                    {
                                                        break;
                                                    }

                                                case Excel.XlChartType.xlColumnClustered:
                                                    {
                                                        break;
                                                    }

                                                case Excel.XlChartType.xlBarClustered:
                                                    {
                                                        break;
                                                    }

                                                default:
                                                    {
                                                        goto ExitDo; //Exit Do
                                                    }
                                            }

                                            plotby = Output.Orientation == TableOrientation.Portrait ? XlRowCol.xlRows : XlRowCol.xlColumns;
                                            errdesc = Constants.vbNullString;
                                            tmpChartObject = CreateChartObject(
                                                 tmpGTTable, chtObjs, chtCaption, chtType, (Range)GraphSourceRangeCol[1], GraphSheet
                                               , ref NamesArray, ref errdesc, plotby, false, string.Empty, Convert.ToInt64(nCol[1]), 50, false, 100, "0.0");

                                            if (tmpChartObject == null)
                                            {
                                                if (OutputUtil.StrPtr(errdesc) != 0)
                                                {
                                                    ResumeError = true;
                                                    //Information.Err().Raise(Constants.vbObjectError + 900 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                                }

                                                break;
                                            }

                                            if (chtType == Excel.XlChartType.xlBarClustered)
                                            {
                                                tmpRange = (Range)GraphTableRangeCol[1];
                                                {
                                                    var withBlock2 = tmpRange;
                                                    if (Output.Orientation == TableOrientation.Portrait)
                                                        FitChartHeightToRangeWidth(tmpChartObject, withBlock2.Item[1, 3].Resize(ColumnSize: withBlock2.Columns.Count - 2));
                                                    else
                                                        FitChartHeightToRangeHeight(tmpChartObject, withBlock2.Item[3, 1].Resize(withBlock2.Rows.Count - 2));
                                                }
                                            }
                                            else if (chtType != Excel.XlChartType.xlPie)
                                                AdjustOverlap(tmpChartObject);
                                            ChartObjectCol.Add(tmpChartObject);
                                            break;
                                        }

                                        if ((withBlock1.Question.QuestionType & QuestionType.Rank) == QuestionType.Rank)
                                        {
                                            switch (chtType)
                                            {
                                                case Excel.XlChartType.xlBarStacked:
                                                    {
                                                        break;
                                                    }

                                                case Excel.XlChartType.xlColumnStacked:
                                                    {
                                                        break;
                                                    }

                                                default:
                                                    {
                                                        goto ExitDo; //Exit Do
                                                    }
                                            }

                                            plotby = Output.Orientation == TableOrientation.Portrait ? XlRowCol.xlColumns : XlRowCol.xlRows;
                                            errdesc = Constants.vbNullString;
                                            tmpChartObject = CreateChartObject(tmpGTTable, chtObjs, chtCaption, chtType
                                              , (Range)GraphSourceRangeCol[1], GraphSheet, ref NamesArray, ref errdesc, plotby, true, string.Empty, Convert.ToInt64(nCol[1])
                                              , 50, false, 100, "0\"%\"", false, Output.Orientation == TableOrientation.Portrait);
                                            if (tmpChartObject == null)
                                            {
                                                if (OutputUtil.StrPtr(errdesc) != 0)
                                                {
                                                    ResumeError = true;
                                                    //Information.Err().Raise(Constants.vbObjectError + 910 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                                }

                                                break;
                                            }

                                            if (chtType == Excel.XlChartType.xlBarStacked)
                                            {
                                                tmpRange = (Range)GraphTableRangeCol[1];
                                                {
                                                    var withBlock2 = tmpRange;
                                                    if (Output.Orientation == TableOrientation.Portrait)
                                                        FitChartHeightToRangeHeight(tmpChartObject, withBlock2.Rows.Item[withBlock2.Rows.Count - (int)(tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasWeight) & 2) + (CrossCreator.ToInt(HasIV) & 1) + (CrossCreator.ToInt(HasNA) & 1)) * (int)(Interaction.IIf(IsNPerSourceRange, 2, 1)) + 1].Resize((tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)) * (int)(Interaction.IIf(IsNPerSourceRange, 2, 1))));
                                                    else
                                                        FitChartHeightToRangeWidth(tmpChartObject, withBlock2.Columns.Item[withBlock2.Columns.Count - (tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasWeight) & 2) + (CrossCreator.ToInt(HasIV) & 1) + (CrossCreator.ToInt(HasNA) & 1)) + 1].Resize(ColumnSize: tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)));
                                                }
                                            }
                                            else
                                                AdjustOverlap(tmpChartObject);
                                            ChartObjectCol.Add(tmpChartObject);
                                            break;
                                        }

                                        #region #OutputFormatting - Qc4 Changes
                                        bool IsDummyMAMatrix = false;
                                        if (((withBlock1.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent) && ((withBlock1.Question.QuestionType & QuestionType.SA) == QuestionType.SA) && ((chtType == Excel.XlChartType.xlBarClustered) || (chtType == Excel.XlChartType.xlColumnClustered)))
                                            IsDummyMAMatrix = true;
                                        #endregion

                                        if (((withBlock1.Question.QuestionType & QuestionType.SA) == QuestionType.SA) && !IsDummyMAMatrix)
                                        {
                                            // SAマト
                                            if (!IsQCM)
                                            {
                                                switch (chtType)
                                                {
                                                    case Excel.XlChartType.xlLine:
                                                        {
                                                            break;
                                                        }

                                                    case Excel.XlChartType.xlBarStacked100:
                                                        {
                                                            break;
                                                        }

                                                    case Excel.XlChartType.xlColumnStacked100:
                                                        {
                                                            break;
                                                        }
                                                    //case Excel.XlChartType.xlBarClustered: // #NewImplementation #OutputFormatting
                                                    //    {
                                                    //        break;
                                                    //    }
                                                    //case Excel.XlChartType.xlColumnClustered:
                                                    //    {
                                                    //        break;
                                                    //    }
                                                    default:
                                                        {
                                                            goto ExitDo; //Exit Do
                                                        }
                                                }

                                                plotby = Output.Orientation == TableOrientation.Portrait ? XlRowCol.xlRows : XlRowCol.xlColumns;
                                                errdesc = Constants.vbNullString;
                                                tmpChartObject = CreateChartObject(tmpGTTable, chtObjs, chtCaption, chtType
                                                , (Range)GraphSourceRangeCol[1], GraphSheet, ref NamesArray, ref errdesc, plotby, true
                                                , string.Empty, -1, 50, false, (chtType == Excel.XlChartType.xlLine ? 100 : 1), chtType == Excel.XlChartType.xlLine ? @"0""%""" : "0%");
                                                if (tmpChartObject == null)
                                                {
                                                    if (OutputUtil.StrPtr(errdesc) != 0)
                                                    {
                                                        ResumeError = true;
                                                        //Information.Err().Raise(Constants.vbObjectError + 920 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                                    }

                                                    break;
                                                }

                                                if (chtType == Excel.XlChartType.xlBarStacked100)
                                                {
                                                    tmpRange = (Range)GraphTableRangeCol[1];
                                                    {
                                                        var withBlock2 = tmpRange;
                                                        if (Output.Orientation == TableOrientation.Portrait)
                                                            FitChartHeightToRangeWidth(tmpChartObject, withBlock2.Item[1, withBlock2.Columns.Count - tmpGTTable.ChildQuestionsCount + 1].Resize(ColumnSize: tmpGTTable.ChildQuestionsCount));
                                                        else
                                                            FitChartHeightToRangeHeight(tmpChartObject, withBlock2.Item[withBlock2.Rows.Count - tmpGTTable.ChildQuestionsCount * (int)(Interaction.IIf(IsNPerSourceRange, 2, 1)) + 1, 1].Resize(tmpGTTable.ChildQuestionsCount * (int)(Interaction.IIf(IsNPerSourceRange, 2, 1))));
                                                    }
                                                }
                                                else
                                                    AdjustOverlap(tmpChartObject);
                                                ChartObjectCol.Add(tmpChartObject);
                                                break;
                                            }
                                            // QCM
                                            tmpPlotby = XlRowCol.xlRows;
                                            switch (chtType)
                                            {
                                                case Excel.XlChartType.xlPie:
                                                    {
                                                        break;
                                                    }

                                                case Excel.XlChartType.xlBarStacked100:
                                                    {
                                                        // QCM横帯
                                                        tmpPlotby = XlRowCol.xlColumns;
                                                        break;
                                                    }

                                                case Excel.XlChartType.xlColumnStacked100:
                                                    {
                                                        // QCM縦帯
                                                        tmpPlotby = XlRowCol.xlColumns;
                                                        break;
                                                    }

                                                case Excel.XlChartType.xlBarClustered:
                                                    {
                                                        break;
                                                    }

                                                case Excel.XlChartType.xlColumnClustered:
                                                    {
                                                        break;
                                                    }

                                                default:
                                                    {
                                                        goto ExitDo; //Exit Do
                                                    }
                                            }

                                            plotby = Output.Orientation == TableOrientation.Portrait ? tmpPlotby == XlRowCol.xlColumns ? XlRowCol.xlRows : XlRowCol.xlColumns : tmpPlotby;
                                            for (n = 1; n <= GraphSourceRangeCol.Count; n++)
                                            {
                                                if (bgWorker.CancellationPending) return;
                                                if (noReset)
                                                {
                                                    tmpGraphSourceRange = (Range)GraphSourceRangeCol[n];
                                                    {
                                                        var withBlock2 = tmpGraphSourceRange.Areas;
                                                        if (withBlock2.Item[1].Columns.Count == 1)
                                                            tmp = withBlock2.Item[2].Range["A1"].Value;
                                                        else
                                                            tmp = withBlock2.Item[1].Range["B1"].Value;
                                                    }
                                                }
                                                else
                                                {
                                                    tmpRange = (Range)GraphSourceRangeCol[n];
                                                    {
                                                        var withBlock2 = tmpRange.Areas;
                                                        tmpGraphSourceRange = (Range)withBlock2[2];
                                                        for (j = 3; j <= withBlock2.Count; j++)
                                                            tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[(int)j]);
                                                        tmp = withBlock2.Item[1].Value;
                                                    }
                                                }

                                                errdesc = Constants.vbNullString;
                                                tmpChartObject = CreateChartObject(
                                                  tmpGTTable, chtObjs, chtCaption, chtType, tmpGraphSourceRange
                                                , GraphSheet, ref NamesArray, ref errdesc, plotby, tmpPlotby == XlRowCol.xlColumns
                                                , tmp, Convert.ToInt64(nCol[n])
                                                , tmpPlotby == XlRowCol.xlColumns ? 50 : 40, tmpPlotby == XlRowCol.xlColumns
                                                , tmpPlotby == XlRowCol.xlColumns ? 1 : 100, tmpPlotby == XlRowCol.xlColumns ? "0%" : @"0""%""");

                                                if (tmpChartObject == null)
                                                {
                                                    for (j = ChartObjectCol.Count; j >= 1; j += -1)
                                                    {
                                                        tmpChartObject = (ChartObject)ChartObjectCol[j];
                                                        tmpChartObject.Delete();
                                                        ChartObjectCol.Remove((int)j);
                                                    }
                                                    if (OutputUtil.StrPtr(errdesc) != 0)
                                                    {
                                                        ResumeError = true;
                                                        //Information.Err().Raise(Constants.vbObjectError + 930 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                                    }

                                                    break;
                                                }
                                                // If tmpChartObject.Chart.Axes(xlCategory).ReversePlotOrder Then
                                                switch (chtType)
                                                {
                                                    case Excel.XlChartType.xlBarStacked100:
                                                    case Excel.XlChartType.xlBarClustered:
                                                        {
                                                            tmpRange = (Range)GraphSourceRangeCol[n];
                                                            {
                                                                var withBlock2 = tmpRange.Areas;
                                                                if (chtType == Excel.XlChartType.xlBarStacked100)
                                                                {
                                                                    if (Output.Orientation == TableOrientation.Portrait)
                                                                    {
                                                                        if (noReset)
                                                                            tmpRange = withBlock2.Item[1].Columns.Count == 1 ? withBlock2.Item[2].Range["A1"] : withBlock2.Item[1].Range["B1"];
                                                                        else
                                                                            tmpRange = withBlock2.Item[1];
                                                                        FitChartHeightToRangeWidth(tmpChartObject, tmpRange);
                                                                    }
                                                                    else
                                                                    {
                                                                        tmpRange = withBlock2.Item[1].Resize[IsNPerSourceRange ? 2 : 1];
                                                                        FitChartHeightToRangeHeight(tmpChartObject, tmpRange);
                                                                    }
                                                                }
                                                                else if (Output.Orientation == TableOrientation.Portrait)
                                                                {
                                                                    if (noReset)
                                                                        tmpRange = tmpRange.Worksheet.Range[withBlock2.Item[withBlock2.Item[1].Columns.Count == 1 ? 3 : 2].Item[0, 1], withBlock2.Item[withBlock2.Count]];
                                                                    else
                                                                        tmpRange = tmpRange.Worksheet.Range[withBlock2.Item[2], withBlock2.Item[withBlock2.Count]];
                                                                    FitChartHeightToRangeHeight(tmpChartObject, tmpRange);
                                                                }
                                                                else
                                                                {
                                                                    tmpRange = withBlock2.Item[2];
                                                                    FitChartHeightToRangeWidth(tmpChartObject, tmpRange);
                                                                }
                                                            }

                                                            break;
                                                        }

                                                    case Excel.XlChartType.xlColumnStacked100:
                                                    case Excel.XlChartType.xlColumnClustered:
                                                        {
                                                            AdjustOverlap(tmpChartObject);
                                                            break;
                                                        }
                                                }

                                                ChartObjectCol.Add(tmpChartObject);
                                                //DoEvents();
                                            }

                                            break;
                                        }
                                        // MAマト
                                        if (!IsQCM)
                                        {
                                            switch (chtType)
                                            {
                                                case Excel.XlChartType.xlBarClustered:
                                                    {
                                                        break;
                                                    }

                                                case Excel.XlChartType.xlColumnClustered:
                                                    {
                                                        break;
                                                    }

                                                case Excel.XlChartType.xlLine:
                                                    {
                                                        break;
                                                    }

                                                default:
                                                    {
                                                        goto ExitDo; //Exit Do
                                                    }
                                            }

                                            plotby = Output.Orientation == TableOrientation.Portrait ? XlRowCol.xlRows : XlRowCol.xlColumns;
                                            errdesc = Constants.vbNullString;
                                            tmpChartObject = CreateChartObject(tmpGTTable, chtObjs, chtCaption, chtType
                                            , (Range)GraphSourceRangeCol[1], GraphSheet, ref NamesArray, ref errdesc, plotby, true, "", -1, 40);
                                            if (tmpChartObject == null)
                                            {
                                                if (OutputUtil.StrPtr(errdesc) != 0)
                                                {
                                                    ResumeError = true;
                                                    //Information.Err().Raise(Constants.vbObjectError + 940 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                                }

                                                break;
                                            }

                                            if (chtType == Excel.XlChartType.xlBarClustered)
                                                FitChartHeight(tmpChartObject, MATRIX_BARCLUSTER_LINE_HEIGHT * withBlock1.ChildQuestionsCount * (withBlock1.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)));
                                            else
                                                AdjustOverlap(tmpChartObject);
                                            ChartObjectCol.Add(tmpChartObject);
                                            break;
                                        }
                                        // QCM
                                        switch (chtType)
                                        {
                                            case Excel.XlChartType.xlBarClustered:
                                                {
                                                    break;
                                                }

                                            case Excel.XlChartType.xlColumnClustered:
                                                {
                                                    break;
                                                }

                                            default:
                                                {
                                                    goto ExitDo; //Exit Do
                                                }
                                        }

                                        plotby = Output.Orientation == TableOrientation.Portrait ? XlRowCol.xlColumns : XlRowCol.xlRows;
                                        for (n = 1; n <= GraphSourceRangeCol.Count; n++)
                                        {
                                            if (noReset)
                                            {
                                                tmpGraphSourceRange = (Range)GraphSourceRangeCol[n];
                                                {
                                                    var withBlock2 = tmpGraphSourceRange.Areas;
                                                    if (withBlock2.Item[1].Columns.Count == 1)
                                                        tmp = withBlock2.Item[2].Range["A1"].Value;
                                                    else
                                                        tmp = withBlock2.Item[1].Range["B1"].Value;
                                                }
                                            }
                                            else
                                            {
                                                tmpRange = (Range)GraphSourceRangeCol[n];
                                                {
                                                    var withBlock2 = tmpRange.Areas;
                                                    tmpGraphSourceRange = withBlock2.Item[2];

                                                    for (j = 3; j <= withBlock2.Count; j++)
                                                        tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[(int)j]);

                                                    tmp = withBlock2.Item[1].Value;
                                                }
                                            }

                                            errdesc = Constants.vbNullString;
                                            tmpChartObject = CreateChartObject(tmpGTTable, chtObjs, chtCaption, chtType
                                              , tmpGraphSourceRange, GraphSheet, ref NamesArray, ref errdesc, plotby, false
                                              , tmp, Convert.ToInt64(nCol[n]), 40);

                                            if (tmpChartObject == null)
                                            {
                                                for (j = ChartObjectCol.Count; j >= 1; j += -1)
                                                {
                                                    tmpChartObject = (ChartObject)ChartObjectCol[j];
                                                    tmpChartObject.Delete();
                                                    ChartObjectCol.Remove((int)j);
                                                }

                                                if (OutputUtil.StrPtr(errdesc) != 0)
                                                {
                                                    ResumeError = true;
                                                    //Information.Err().Raise(Constants.vbObjectError + 950 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                                }

                                                break;
                                            }

                                            if (chtType == Excel.XlChartType.xlBarClustered)
                                            {
                                                tmpRange = (Range)GraphSourceRangeCol[n];
                                                if (Output.Orientation == TableOrientation.Portrait)
                                                {
                                                    {
                                                        var withBlock2 = tmpRange.Areas;
                                                        tmpRange = tmpRange.Worksheet.Range[withBlock2.Item[2], withBlock2.Item[withBlock2.Count]];
                                                    }

                                                    FitChartHeightToRangeHeight(tmpChartObject, tmpRange);
                                                }
                                                else
                                                {
                                                    tmpRange = tmpRange.Areas.Item[2];
                                                    FitChartHeightToRangeWidth(tmpChartObject, tmpRange);
                                                }
                                            }
                                            else
                                                AdjustOverlap(tmpChartObject);
                                            ChartObjectCol.Add(tmpChartObject);
                                            //DoEvents();
                                        }

                                        break;
                                    }

                                    if (isN)
                                        break;
                                    if ((withBlock1.Question.QuestionType & QuestionType.SA) == QuestionType.SA)
                                    {
                                        // SA
                                        plotby = XlRowCol.xlColumns;
                                        switch (chtType)
                                        {
                                            case Excel.XlChartType.xlPie:
                                                {
                                                    break;
                                                }

                                            case Excel.XlChartType.xlBarStacked100:
                                                {
                                                    // 横帯
                                                    plotby = XlRowCol.xlRows;
                                                    break;
                                                }

                                            case Excel.XlChartType.xlColumnStacked100:
                                                {
                                                    // 縦帯
                                                    plotby = XlRowCol.xlRows;
                                                    break;
                                                }

                                            case Excel.XlChartType.xlBarClustered:
                                                {
                                                    break;
                                                }

                                            case Excel.XlChartType.xlColumnClustered:
                                                {
                                                    break;
                                                }

                                            default:
                                                {
                                                    goto ExitDo; //Exit Do
                                                }
                                        }

                                        errdesc = Constants.vbNullString;
                                        tmpChartObject = CreateChartObject(
                                        tmpGTTable, chtObjs, chtCaption, chtType, (Range)GraphSourceRangeCol[1]
                                        , GraphSheet, ref NamesArray, ref errdesc, plotby, plotby == XlRowCol.xlRows, "", Convert.ToInt64(nCol[1])
                                        , plotby == XlRowCol.xlRows ? 50 : 40, plotby == XlRowCol.xlRows
                                        , plotby == XlRowCol.xlRows ? 1 : 100, plotby == XlRowCol.xlRows ? "0%" : @"0""%""");

                                        if (tmpChartObject == null)
                                        {
                                            if (OutputUtil.StrPtr(errdesc) != 0)
                                            {
                                                ResumeError = true;
                                                //Information.Err().Raise(Constants.vbObjectError + 960 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                            }

                                            break;
                                        }
                                        // If tmpChartObject.Chart.Axes(xlCategory).ReversePlotOrder Then
                                        switch (chtType)
                                        {
                                            case Excel.XlChartType.xlBarStacked100:
                                            case Excel.XlChartType.xlBarClustered:
                                                {
                                                    tmpRange = (Range)GraphSourceRangeCol[1];
                                                    {
                                                        var withBlock2 = tmpRange.Areas;
                                                        tmpRange = withBlock2.Item[withBlock2.Count];
                                                    }

                                                    if (chtType == Excel.XlChartType.xlBarStacked100)
                                                    {
                                                        tmpRange = tmpRange.Columns.Item[tmpRange.Columns.Count];
                                                        FitChartHeightToRangeWidth(tmpChartObject, tmpRange);
                                                    }
                                                    else
                                                        FitChartHeightToRangeHeight(tmpChartObject, tmpRange);
                                                    break;
                                                }

                                            case Excel.XlChartType.xlColumnStacked100:
                                            case Excel.XlChartType.xlColumnClustered:
                                                {
                                                    AdjustOverlap(tmpChartObject);
                                                    break;
                                                }
                                        }

                                        ChartObjectCol.Add(tmpChartObject);
                                        break;
                                    }
                                    //// MA
                                    switch (chtType)
                                    {

                                        case Excel.XlChartType.xlBarClustered:
                                        case Excel.XlChartType.xlColumnClustered:

                                        case Excel.XlChartType.xlPie: // 円 #OutputFormatting - Because of question type change for subtotal
                                        case Excel.XlChartType.xlColumnStacked100:    // 縦帯 #OutputFormatting - Because of question type change for subtotal
                                        case Excel.XlChartType.xlBarStacked100:   // 横帯 #OutputFormatting - Because of question type change for subtotal
                                            {
                                                break;
                                            }

                                        default:
                                            {
                                                goto ExitDo; //Exit Do
                                            }
                                    }

                                    errdesc = Constants.vbNullString;
                                    tmpChartObject = CreateChartObject(
                                    tmpGTTable, chtObjs, chtCaption, chtType, (Range)GraphSourceRangeCol[1]
                                    , GraphSheet, ref NamesArray, ref errdesc, XlRowCol.xlColumns, false, "", Convert.ToInt64(nCol[1]), 40);
                                    if (tmpChartObject == null)
                                    {
                                        if (OutputUtil.StrPtr(errdesc) != 0)
                                        {
                                            ResumeError = true;
                                            //Information.Err().Raise(Constants.vbObjectError + 970 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                        }

                                        break;
                                    }

                                    if (chtType == Excel.XlChartType.xlBarClustered)
                                    {
                                        tmpRange = (Range)GraphSourceRangeCol[1];
                                        {
                                            var withBlock2 = tmpRange.Areas;
                                            tmpRange = withBlock2.Item[withBlock2.Count];
                                        }
                                        FitChartHeightToRangeHeight(tmpChartObject, tmpRange);
                                    }
                                    else
                                        AdjustOverlap(tmpChartObject);
                                    ChartObjectCol.Add(tmpChartObject);
                                }
                                while (!true);
                            ExitDo:;
                                //break;
                                //}
                            }
                            if (ChartObjectCol.Count > 0)
                            {
                                double h;
                                foreach (var tmpChartObject1 in GraphSheet.ChartObjects())
                                    tmpChartObject1.Placement = XlPlacement.xlFreeFloating;
                                h = 0;
                                for (j = 1; j <= ChartObjectCol.Count; j++)
                                {
                                    tmpChartObject = (ChartObject)ChartObjectCol[j];
                                    h = h + GraphStartCell.Resize[2].Height + tmpChartObject.Height;
                                }
                                ResumeContinue = true;
                                if (GraphStartCell.Top + h > GraphSheet.Rows.Height)
                                {
                                    GraphStartCell = null;
                                    for (j = ChartObjectCol.Count; j >= 1; j += -1)
                                    {
                                        tmpChartObject = (ChartObject)ChartObjectCol[j];
                                        tmpChartObject.Delete();
                                    }
                                    //Information.Err().Raise(Constants.vbObjectError + 500 &, RunningProcName
                                    //       , ThisWorkbook.yoyoReportKeyword(IIf(IsLastTable, ReportMessageIndex_ReportOutputGraphErrorMessageIndex
                                    //                        , ReportMessageIndex_ReportOutputGraphOnAfterErrorMessageIndex)
                                    //       , withBlock1.Question.Name));
                                }
                                else
                                {
                                    Range[] topBottomCells = new Range[2];

                                    for (j = 1; j <= ChartObjectCol.Count; j++)
                                    {
                                        if (bgWorker.CancellationPending) return;
                                        tmpChartObject = (ChartObject)ChartObjectCol[j];
                                        h = GraphStartCell.Resize[2].Height + tmpChartObject.Height;
                                        x = System.Convert.ToInt64(RoundUp(h / GraphStartCell.Height));    // 必要行数
                                        y = System.Convert.ToInt64(RoundUp(x / (double)MAX_ROWS_COUNT_PER_PAGE_AT_GRAPH)); // 必要ページ数
                                        if (y == 1)
                                        {
                                            if (x < MAX_ROWS_COUNT_PER_PAGE_AT_GRAPH)
                                                GraphStartCell.Range["A2"].EntireRow.Resize[MAX_ROWS_COUNT_PER_PAGE_AT_GRAPH - x].Delete(XlDeleteShiftDirection.xlShiftUp);
                                        }
                                        else
                                            GraphStartCell.Range["A2"].EntireRow.Resize[x - MAX_ROWS_COUNT_PER_PAGE_AT_GRAPH].Insert(XlInsertShiftDirection.xlShiftDown);
                                        {
                                            var withBlock2 = GraphStartCell.Range["B2"];
                                            withBlock2.Value = "Graph" + strIdx + Interaction.IIf(IsQCM, "_" + System.Convert.ToString(j), "");
                                            withBlock2.NumberFormat = "\"[\"@\"]\"";
                                        }

                                        tmpChartObject.Top = GraphStartCell.Range["B3"].Top;

                                        if (j == 1)
                                        {

                                            topBottomCells[0] = tmpChartObject.TopLeftCell;
                                            topBottomCells[1] = tmpChartObject.BottomRightCell;

                                            //Range rGraphArea = GraphStartCell.Worksheet.Range[tmpChartObject.TopLeftCell, tmpChartObject.BottomRightCell];
                                            //rGraphArea = rGraphArea.Resize[rGraphArea.Rows.Count, rGraphArea.Columns.Count - 1];
                                            //HyperlinkTargetCells.SetValue(rGraphArea, i, 6);

                                        }
                                        else if (j > 1)
                                        {
                                            topBottomCells[1] = tmpChartObject.BottomRightCell; // Update last index
                                        }

                                        if (j < ChartObjectCol.Count)
                                            GraphStartCell = GraphStartCell.Item[x + 1, 1];
                                    }

                                    Range rGraphArea = GraphStartCell.Worksheet.Range[topBottomCells[0], topBottomCells[1]];
                                    rGraphArea = rGraphArea.Resize[rGraphArea.Rows.Count, rGraphArea.Columns.Count - 1];
                                    HyperlinkTargetCells.SetValue(rGraphArea, i, 6);


                                    if (!IsLastTable)
                                    {
                                        r = GraphStartCell.Row + x;
                                        if (r > GraphSheet.Rows.Count - 2)
                                        {
                                            GraphStartCell = null;
                                            tmpNextTable = (GTTable)Tables[(int)i];
                                            //Information.Err().Raise(Constants.vbObjectError + 510 &, RunningProcName
                                            //       , ThisWorkbook.yoyoReportKeyword(IIf(i + 1 & == Tables.Count, ReportMessageIndex_ReportOutputGraphErrorMessageIndex
                                            //                        , ReportMessageIndex_ReportOutputGraphOnAfterErrorMessageIndex)
                                            //       , tmpNextTable.Question.Name));
                                        }
                                        else
                                            GraphStartCell = GraphStartCell.Item[x + 1, 1];
                                    }
                                }
                                ResumeContinue = false;
                            }
                        }
                    }

                    #region Progress Bar Implementation
                    currentProgress = UpdProgress;
                    #endregion
                }
                _log.Info("Tables and chart created");
                PutContents(ContentsSheet, ref ContentsValue, HyperlinkTargetCells, WorkingSheet);
                if (PageSetupOn)
                    PutContents(PageSetupContentsSheet, ref PageSetupContentsValue, PageSetupHyperlinkTargetCells, WorkingSheet);
                if (!(NewBook == null))
                {
                    xlApp.DisplayAlerts = false;
                    WorkingSheet.Delete();
                    if (!(GraphSheet == null))
                    {
                        if (GraphSheet.ChartObjects().Count == 0)
                            GraphSheet.Delete();
                    }
                    //On Error Resume Next
                    //this.Application.Goto(NewBook.Worksheets.Item(1).Range("A1"));
                    //On Error GoTo 0
                    //SaveBook(NewBook, filenameSuffix, xlFmt);
                    SaveBook(NewBook, FormatBook, xlFmt, outPutFileName);

                }
                //if (res != RaisedError)
                //    res = Successful;
                //ExitProc:
                //    ;
                //On Error Resume Next

                // 終了時処理
                //if (!(FormatBook == null))
                //    FormatBook.Close(false);
                //CreateGT = res;
                //if (res == RaisedError)
                //    PutErrorsInformation(Errors);
                //RunningProcName = OrgProcName;
                //return;

                //ErrHdl:
                //    ;
                //    if (IsDebug)
                //    {
                //        Debug.Print(RunningProcName);
                //        Debug.Print(Information.Err().Number, Information.Err().Description);
                //        System.Diagnostics.Debugger.Break();
                //        //Resume
                //    }
                //    if (ResumeContinue)
                //        ResumeError = true;
                //    if (ResumeError)
                //    {
                //        res = RaisedError;
                //        PushError(Information.Err(), Errors, ErrorsCount);
                //        //Resume Next
                //    }
                //    else
                //    {
                //        res = Uncontinuable;
                //        ThisWorkbook.AsynchronousForm.SetError(Information.Err());
                //        Resume ExitProc
                //    }
            }
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
                COMWholeOperate.releaseComObject<Worksheet>(ref WorkingSheet);
                COMWholeOperate.releaseComObject(ref wss);
                COMWholeOperate.releaseComObject(ref FormatBook);
                COMWholeOperate.releaseComObject<Workbook>(ref baseBook);
                COMWholeOperate.releaseComObject<Workbook>(ref WorkingBook);
                COMWholeOperate.releaseComObject<Workbooks>(ref wbs);
                //if (xlAppChart == null)
                //{
                //    COMWholeOperate.releaseComObject<Application>(ref xlApp);
                //}
                GC.Collect();
            }
        }

        private string TemplatePath(TableOrientation Orientation = TableOrientation.Landscape, XlFileFormat FileFormat = XlFileFormat.xlOpenXMLWorkbook)
        {
            string TemplatePath = null;
            string d;
            string n;
            string OrgProcName;
            OrgProcName = RunningProcName; // Not sure
            RunningProcName = "GTCreator.TemplatePath";

            if (Orientation != TableOrientation.Portrait) Orientation = TableOrientation.Landscape;
            FileFormat = (XlFileFormat)CurrentOutput.ParentRequest.ExcelFileFormat;
            if (FileFormat != XlFileFormat.xlExcel8) FileFormat = XlFileFormat.xlOpenXMLWorkbook;

            if (Orientation == TableOrientation.Landscape)
            {
                n = TEMPLATE_NAME;
            }
            else
            {
                n = TRANSPOSE_TEMPLATE_NAME;
            }

            if (FileFormat == XlFileFormat.xlOpenXMLWorkbook) n = n + "x";

            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, xlApp.PathSeparator);
            TemplatePath = OutputUtil.BuildPath(d, n, xlApp.PathSeparator);
            RunningProcName = OrgProcName;
            return TemplatePath;
        }

        private string FormatTemplatePath(bool HasWeightBack = false, TableOrientation Orientation = TableOrientation.Landscape)
        {
            string d;
            string n;
            string OrgProcName;
            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.FormatTemplatePath";

            if (Orientation != TableOrientation.Portrait) Orientation = TableOrientation.Landscape;
            if (Orientation == TableOrientation.Landscape)
            {
                if (HasWeightBack)
                {
                    n = WB_FORMAT_TEMPLATE_NAME;
                }
                else
                {
                    n = FORMAT_TEMPLATE_NAME;
                }
            }
            else
            {
                if (HasWeightBack)
                {
                    n = TRANSPOSE_WB_FORMAT_TEMPLATE_NAME;
                }
                else
                {
                    n = TRANSPOSE_FORMAT_TEMPLATE_NAME;
                }
            }

            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, xlApp.PathSeparator);
            string FormatTemplatePath = OutputUtil.BuildPath(d, n, xlApp.PathSeparator);
            RunningProcName = OrgProcName;
            return FormatTemplatePath;
        }

        private void CreateGTSub(OutputGT Output, List<GTTable> Tables
                            , ref bool HasWeight, ref bool HasWeightColumn, ref bool UseWeightFormat
                            , ref bool isMatrix, ref bool isN, ref bool CreateGraph
                            , Excel.Workbook FormatBook, ref Excel.Worksheet FormatSheet
                            , ref bool RunSignificanceTest, ref bool HasMatrixBetweenChildSigTest, ref bool HasMatrixBetweenSectorSigTest, ref bool HasNormalSigTest
                            , bool PageSetupOn, ref bool SigTestPageSetupOn
                            , Excel.Workbook NewBook, ref Excel.Worksheet ContentsSheet, ref Excel.Worksheet TemplateSheet
                            , ref Excel.Worksheet SigTestTemplateSheet, ref Excel.Worksheet PageSetupContentsSheet
                            , ref Excel.Worksheet PageSetupTemplateSheet, ref Excel.Worksheet PageSetupSigTestTemplateSheet
                            , ref Excel.Worksheet GraphSheet, ref Excel.Worksheet WorkingSheet
                            )
        {
            GTTable tmpGTTable;
            string SigTestTemplateSheetName = null;
            string tmpSheetName = null;
            string PageSetupSheetBaseName;
            Dictionary<string, string> tmpDic;
            {
                var withBlock = Tables;
                // ウエイト列が必要かどうか、項目間検定を行うかどうか、グラフを作成するかどうかをチェック
                for (int i = 1; i <= withBlock.Count; i++)
                {
                    tmpGTTable = (GTTable)withBlock[i - 1];
                    {
                        var withBlock1 = tmpGTTable;
                        HasWeight = GetHasWeight(tmpGTTable);
                        isMatrix = (withBlock1.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
                        isN = (withBlock1.Question.QuestionType & QuestionType.N) == QuestionType.N;
                        if (FormatSheet == null)
                        {
                            //FormatSheet = FormatBook.Worksheets.Item["Weight"]; // #OutputFormatting
                            if (HasWeight)
                            {
                                FormatSheet = FormatBook.Worksheets.Item["Weight"]; //Commented on 09-9-2019 as a part of #OutputFormatting
                                UseWeightFormat = true;
                            }
                        }

                        if (!HasWeightColumn)
                        {
                            if (HasWeight)
                            {
                                if ((withBlock1.Question.QuestionType & QuestionType.MatrixParent) == 0 | Output.Orientation == TableOrientation.Portrait)
                                    HasWeightColumn = true;
                            }
                        }

                        if (isMatrix)
                        {
                            switch (withBlock1.SignificancetestCode)
                            {
                                case SignificanceTestCode.BetweenChildQuestions:
                                    {
                                        HasMatrixBetweenChildSigTest = true;
                                        break;
                                    }

                                case SignificanceTestCode.BetweenSectors:
                                    {
                                        if (!isN)
                                            HasMatrixBetweenSectorSigTest = true;
                                        break;
                                    }
                            }
                        }
                        else if (withBlock1.SignificancetestCode == SignificanceTestCode.BetweenSectors)
                        {
                            if (!isN)
                                HasNormalSigTest = true;
                        }
                        if (!RunSignificanceTest)
                        {

                            if (HasMatrixBetweenChildSigTest || HasMatrixBetweenSectorSigTest || HasNormalSigTest)
                            {
                                RunSignificanceTest = true;
                            }

                        }
                        if (!CreateGraph)
                        {
                            if (!(withBlock1.Chart == null))
                            {
                                bool ischrt = true;
                                if ((withBlock1.Chart.ChartType == XlChartType.xlBarStacked100 || withBlock1.Chart.ChartType == XlChartType.xlColumnStacked100) && withBlock1.SectorsCount > 255)
                                    ischrt = false;
                                CreateGraph = withBlock1.Chart.ChartType != 0 && ischrt;
                            }
                        }
                        // If HasWeightColumn And RunSignificanceTest And CreateGraph Then Exit For

                        if (!(!HasWeightColumn || !CreateGraph || !HasMatrixBetweenChildSigTest || !HasMatrixBetweenSectorSigTest || !HasNormalSigTest))
                        {
                            goto ExitForLoop;
                        }

                    }
                }

            ExitForLoop:


                if (!(!PageSetupOn || !RunSignificanceTest || !Output.PageSetupSignificanceTestTable))
                {
                    SigTestPageSetupOn = true;
                }

                // AddColumnCount = HasWeightColumn And 1&
                // 作成するブックの必要シートへの参照を取得
                if (CreateGraph)
                {
                    if (!Output.OutputPerTable)
                        CreateGraph = Output.OutputNPerTable;
                }
            }

            {
                var withBlock = NewBook.Worksheets;

                ContentsSheet = withBlock.Item["INDEX"];
                if (HasWeightColumn)
                {
                    TemplateSheet = withBlock.Item["WT"];
                    if (RunSignificanceTest)
                        SigTestTemplateSheetName = "WT_SignificanceTest";
                }
                else
                {
                    TemplateSheet = withBlock.Item["Standard"];
                    if (RunSignificanceTest)
                        SigTestTemplateSheetName = "SignificanceTest";
                }
                if (RunSignificanceTest)
                {
                    if (OutputUtil.StrPtr(SigTestTemplateSheetName) != 0)
                    {
                        SigTestTemplateSheet = withBlock.Item[SigTestTemplateSheetName];

                        if (Output.Orientation == TableOrientation.Landscape)
                        {
                            if (!(HasNormalSigTest || HasMatrixBetweenChildSigTest))
                            {
                                tmpSheetName = SigTestTemplateSheetName;
                                SigTestTemplateSheetName = Constants.vbNullString;
                            }
                        }
                        else
                        {
                            if (!(HasNormalSigTest || HasMatrixBetweenSectorSigTest))
                            {
                                tmpSheetName = SigTestTemplateSheetName;
                                SigTestTemplateSheetName = Constants.vbNullString;
                            }
                        }
                    }
                    if (OutputUtil.StrPtr(SigTestTemplateSheetName) == 0)
                    {
                        if (!(SigTestTemplateSheet == null))
                        {
                            SigTestTemplateSheetName = TemplateSheet.Name;
                            TemplateSheet.Copy(Type.Missing, SigTestTemplateSheet);
                            SigTestTemplateSheet = SigTestTemplateSheet.Next;
                        }
                    }
                }
                if (PageSetupOn)
                {
                    ContentsSheet.Copy(Type.Missing, ContentsSheet);
                    PageSetupContentsSheet = ContentsSheet.Next;
                    string reportKeyWord = LocalResource.REPORT_GT_PAGE_SETUP_SHEET_SUFFIX;
                    PageSetupContentsSheet.Name = ContentsSheet.Name + reportKeyWord;
                    switch (Output.PaperSize)
                    {
                        case XlPaperSize.xlPaperA3:
                            {
                                PageSetupSheetBaseName = "A3";
                                break;
                            }

                        case XlPaperSize.xlPaperB4:
                            {
                                PageSetupSheetBaseName = "B4";
                                break;
                            }

                        default:
                            {
                                PageSetupSheetBaseName = "A4";
                                break;
                            }
                    }
                    if (Output.PaperOrientation == XlPageOrientation.xlPortrait)
                        PageSetupSheetBaseName = PageSetupSheetBaseName + "Portrait";
                    else
                        PageSetupSheetBaseName = PageSetupSheetBaseName + "Landscape";
                    PageSetupTemplateSheet = withBlock.Item[(TemplateSheet.Name == "Standard" ? "" : TemplateSheet.Name) + PageSetupSheetBaseName];
                    MaxRowsCountPerPage = (long)PageSetupTemplateSheet.Range["A1"].Value;
                    MaxColumnsCountPerPage = (long)PageSetupTemplateSheet.Range["B1"].Value;
                    DefHeight = (long)PageSetupTemplateSheet.Rows.Item[1].Height;
                    // If RunSignificanceTest Then
                    if (SigTestPageSetupOn)
                    {
                        if (SigTestTemplateSheetName == TemplateSheet.Name)
                        {
                            PageSetupSigTestTemplateSheet = withBlock.Item[tmpSheetName + PageSetupSheetBaseName];
                            PageSetupTemplateSheet.Copy(Type.Missing, PageSetupSigTestTemplateSheet);
                            PageSetupSigTestTemplateSheet = PageSetupSigTestTemplateSheet.Next;
                        }
                        else
                        {
                            PageSetupSigTestTemplateSheet = withBlock.Item[SigTestTemplateSheetName + PageSetupSheetBaseName];
                        }
                        MaxColumnsCountPerPage = (long)PageSetupSigTestTemplateSheet.Range["B1"].Value;

                    }
                    //if (CreateGraph) GraphSheet = withBlock.Item["Graph"];
                    WorkingSheet = withBlock.Add();
                }

                if (CreateGraph) GraphSheet = withBlock.Item["Graph"];

                // 不要シートを削除
                xlApp.DisplayAlerts = false;
                //tmpDic = (Dictionary<string, string>)OutputUtil.CreateObject("Scripting.Dictionary");
                tmpDic = new Dictionary<string, string>();

                if (!(ContentsSheet == null))
                    tmpDic.Add(ContentsSheet.Name, string.Empty);
                if (!(TemplateSheet == null))
                    tmpDic.Add(TemplateSheet.Name, string.Empty);
                if (!(SigTestTemplateSheet == null))
                    tmpDic.Add(SigTestTemplateSheet.Name, string.Empty);
                if (!(PageSetupTemplateSheet == null))
                    tmpDic.Add(PageSetupTemplateSheet.Name, string.Empty);
                if (!(PageSetupContentsSheet == null))
                    tmpDic.Add(PageSetupContentsSheet.Name, string.Empty);
                if (!(PageSetupSigTestTemplateSheet == null))
                    tmpDic.Add(PageSetupSigTestTemplateSheet.Name, string.Empty);
                if (!(GraphSheet == null))
                    tmpDic.Add(GraphSheet.Name, string.Empty);
                if (!(WorkingSheet == null))
                    tmpDic.Add(WorkingSheet.Name, string.Empty);
                foreach (Worksheet worksheet in NewBook.Sheets)
                {
                    if (!tmpDic.ContainsKey(worksheet.Name))
                        worksheet.Delete();
                }
            }

        }

        private void AdjustLandscapeFormat(Worksheet FormatSheet, Worksheet SigTestFormatSheet
        , bool CutPreWB, bool HasWeightColumn, bool UseWeightFormat
        , bool IsReport = false)
        {
            string[] tmpNamesArray = null;
            //object tmpName;
            long tmp;
            string tmpSuffix;
            string fmt = null;
            XlDeleteShiftDirection tmpDelShift;
            bool IsWeight = false;
            string OrgProcName;
            //OrgProcName = RunningProcName;
            //RunningProcName = "GTCreator.AdjustLandscapeFormat";
            // フォーマットシートのWB前全体行またはWB前全体列の削除
            if (CutPreWB)
            {
                if (IsReport)
                {
                    tmpDelShift = XlDeleteShiftDirection.xlShiftToLeft;
                    //tmpName = "PreWBColumn";
                    DeleteName(ref FormatSheet, ref SigTestFormatSheet, "PreWBColumn", tmpDelShift);
                    tmpNamesArray = Strings.Split("SAM_MAM_NP_PreWBColumn SAM_MAM_N_PreWBColumn SAM_MAM_P_PreWBColumn "
                                  + "N_PreWBColumn NM_PreWBColumn");
                    foreach (var tmpName in tmpNamesArray)
                        FormatSheet.Names.Item(System.Convert.ToString(tmpName)).Delete();
                }
                else
                {
                    tmpDelShift = XlDeleteShiftDirection.xlShiftUp;
                    tmpNamesArray = Strings.Split("SA_MA_NP_PreWBRow SA_MA_N_PreWBRow SA_MA_P_PreWBRow");
                    foreach (var tmpName in tmpNamesArray)

                        DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpName, tmpDelShift);

                    tmpNamesArray = Strings.Split("SAM_MAM_NP_PreWBColumn SAM_MAM_N_PreWBColumn SAM_MAM_P_PreWBColumn "
                                     + "N_PreWBColumn NM_PreWBColumn");
                    tmpDelShift = XlDeleteShiftDirection.xlShiftToLeft;
                    foreach (var tmpName in tmpNamesArray)
                        DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpName, tmpDelShift);
                }
            }
            // ウエイト値、加重平均のセルの書式設定
            // If HasWeightColumn Then
            if (UseWeightFormat)
            {
                tmp = CurrentOutput.ParentRequest.WeightNumDigitsAfterDecimal;
                if (IsReport)
                    tmpNamesArray = Strings.Split("SAM_MAM_NP_WeightRow SAM_MAM_N_WeightRow SAM_MAM_P_WeightRow");
                else if (HasWeightColumn)
                    tmpNamesArray = Strings.Split("SA_MA_NP_WeightColumn SA_MA_N_WeightColumn SA_MA_P_WeightColumn "
        + "SAM_MAM_NP_WeightRow SAM_MAM_N_WeightRow SAM_MAM_P_WeightRow");
                else
                    tmpNamesArray = Strings.Split("SAM_MAM_NP_WeightRow SAM_MAM_N_WeightRow SAM_MAM_P_WeightRow");
                tmpSuffix = "";
                IsWeight = true;

                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);

                IsWeight = false;
                tmp = CurrentOutput.ParentRequest.WeightAverageNumDigitsAfterDecimal;

                if (IsReport || !HasWeightColumn)
                {
                    tmpNamesArray = Strings.Split("SAM_MAM_NP SAM_MAM_N SAM_MAM_P");
                }
                else
                {
                    tmpNamesArray = Strings.Split("SA_MA_NP SA_MA_N SA_MA_P SAM_MAM_NP SAM_MAM_N SAM_MAM_P");
                }


                tmpSuffix = "_WeightAverage";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            // フォーマットシートの数値回答集計表フォーマットの調整
            TotalColumnIndex = FormatSheet.Range["NM_PopulationColumn"].Column - 1 - 1;
            AverageColumnIndex = 0;
            tmpNamesArray = Strings.Split("N NM");
            tmpDelShift = XlDeleteShiftDirection.xlShiftToLeft;
            if (CurrentOutput.ParentRequest.ShowMedian)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Median);
                tmpSuffix = "_Median";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                //tmpName = "N_MedianColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "N_MedianColumn", tmpDelShift);
                //tmpName = "NM_MedianColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "NM_MedianColumn", tmpDelShift);
            }
            if (CurrentOutput.ParentRequest.ShowMaximum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Maximum);
                tmpSuffix = "_Maximum";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                //tmpName = "N_MaximumColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "N_MaximumColumn", tmpDelShift);
                //tmpName = "NM_MaximumColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "NM_MaximumColumn", tmpDelShift);
            }
            if (CurrentOutput.ParentRequest.ShowMinimum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Minimum);
                tmpSuffix = "_Minimum";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                //tmpName = "N_MinimumColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "N_MinimumColumn", tmpDelShift);
                //tmpName = "NM_MinimumColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "NM_MinimumColumn", tmpDelShift);
            }
            if (CurrentOutput.ParentRequest.ShowStdev)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Stdev);
                tmpSuffix = "_Deviation";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                //tmpName = "N_DeviationColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "N_DeviationColumn", tmpDelShift);

                //tmpName = "NM_DeviationColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "NM_DeviationColumn", tmpDelShift);
            }
            if (CurrentOutput.ParentRequest.ShowAverage)
            {
                AverageColumnIndex = FormatSheet.Range["NM_AverageColumn"].Column - 1;
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Average);
                tmpSuffix = "_Average";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                //tmpName = "N_AverageColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "N_AverageColumn", tmpDelShift);
                //tmpName = "NM_AverageColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "NM_AverageColumn", tmpDelShift);
            }
            if (CurrentOutput.ParentRequest.ShowSummary)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Summary);
                tmpSuffix = "_Summary";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                //tmpName = "N_SummaryColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "N_SummaryColumn", tmpDelShift);
                //tmpName = "NM_SummaryColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "NM_SummaryColumn", tmpDelShift);

                AverageColumnIndex = AverageColumnIndex - 1;
            }
            if (!CurrentOutput.ParentRequest.ShowParameter)
            {
                //tmpName = "N_PopulationColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "N_PopulationColumn", tmpDelShift);
                //tmpName = "NM_PopulationColumn";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, "NM_PopulationColumn", tmpDelShift);

                AverageColumnIndex = AverageColumnIndex - 1;
            }
            //RunningProcName = OrgProcName;
            return;
            //NumFormat:
            //    ;
            //    switch (tmp)
            //    {
            //        case 0:
            //            {
            //                fmt = "0";
            //                break;
            //            }

            //        case long n when (1 <= tmp && tmp <= 5) :
            //            {
            //                fmt = "0." + new String('0',Convert.ToInt32(tmp));
            //                break;
            //            }

            //        default:
            //            {
            //                fmt = "0.0";
            //                break;
            //            }
            //    }
            //    if (IsWeight)
            //        fmt =Strings.Replace(FormatSheet.Range[tmpNamesArray[0] + tmpSuffix].Cells.Item[1].NumberFormat, "0.0", fmt);
            //    foreach (var tmpName in tmpNamesArray)
            //    {
            //        FormatSheet.Range[System.Convert.ToString(tmpName) + tmpSuffix].NumberFormat = fmt;
            //        if (CurrentOutput.SignificanceTest)
            //            SigTestFormatSheet.Range[System.Convert.ToString(tmpName) + tmpSuffix].NumberFormat = fmt;
            //    }
            //    return;
            //DeleteName:
            //    ;
            //    {
            //        var withBlock = FormatSheet.Names.Item(System.Convert.ToString(tmpName));
            //        if (tmpDelShift == XlDeleteShiftDirection.xlShiftUp)
            //            withBlock.RefersToRange.EntireRow.Delete(tmpDelShift);
            //        else
            //            withBlock.RefersToRange.Delete(tmpDelShift);
            //        withBlock.Delete();
            //    }
            //    if (CurrentOutput.SignificanceTest)
            //    {
            //        {
            //            var withBlock = SigTestFormatSheet.Names.Item(System.Convert.ToString(tmpName));
            //            if (tmpDelShift == XlDeleteShiftDirection.xlShiftUp)
            //                withBlock.RefersToRange.EntireRow.Delete(tmpDelShift);
            //            else
            //                withBlock.RefersToRange.Delete(tmpDelShift);
            //            withBlock.Delete();
            //        }
            //    }
            //    return;
        }

        private void DeleteName(ref Worksheet FormatSheet, ref Worksheet SigTestFormatSheet, string tmpName, XlDeleteShiftDirection tmpDelShift)
        {
            {
                var withBlock = FormatSheet.Names.Item(Convert.ToString(tmpName));
                if (tmpDelShift == XlDeleteShiftDirection.xlShiftUp)
                    withBlock.RefersToRange.EntireRow.Delete(tmpDelShift);
                else
                    withBlock.RefersToRange.Delete(tmpDelShift);
                withBlock.Delete();
            }
            if (CurrentOutput.SignificanceTest)
            {
                {
                    var withBlock = SigTestFormatSheet.Names.Item(Convert.ToString(tmpName));
                    if (tmpDelShift == XlDeleteShiftDirection.xlShiftUp)
                        withBlock.RefersToRange.EntireRow.Delete(tmpDelShift);
                    else
                        withBlock.RefersToRange.Delete(tmpDelShift);
                    withBlock.Delete();
                }
            }
        }

        private void DeleteName(ref Worksheet FormatSheet, ref Worksheet SigTestFormatSheet, string[] tmpNamesArray, string tmpSuffix)
        {
            foreach (string tmpName in tmpNamesArray)
            {
                {
                    var withBlock = FormatSheet.Names.Item(System.Convert.ToString(tmpName) + tmpSuffix);
                    withBlock.RefersToRange.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                    withBlock.Delete();
                }

                if (CurrentOutput.SignificanceTest)
                {
                    {
                        var withBlock = SigTestFormatSheet.Names.Item(System.Convert.ToString(tmpName) + tmpSuffix);
                        withBlock.RefersToRange.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                        withBlock.Delete();
                    }
                }

            }
        }

        private void NumFormat(ref Worksheet FormatSheet, ref Worksheet SigTestFormatSheet, ref string fmt, long tmp, bool IsWeight, string[] tmpNamesArray, string tmpSuffix)
        {
            switch (tmp)
            {
                case 0:
                    {
                        fmt = "0";
                        break;
                    }

                case long n when (1 <= tmp && tmp <= 5):
                    {
                        fmt = "0." + new String('0', Convert.ToInt32(tmp));
                        break;
                    }

                default:
                    {
                        fmt = "0.0";
                        break;
                    }
            }
            if (IsWeight)
                fmt = Strings.Replace(FormatSheet.Range[tmpNamesArray[0] + tmpSuffix].Cells.Item[1].NumberFormat, "0.0", fmt);
            foreach (var tmpName in tmpNamesArray)
            {
                FormatSheet.Range[System.Convert.ToString(tmpName) + tmpSuffix].NumberFormat = fmt;
                if (CurrentOutput.SignificanceTest)
                    SigTestFormatSheet.Range[System.Convert.ToString(tmpName) + tmpSuffix].NumberFormat = fmt;
            }
        }

        private void AdjustPortraitFormat(Worksheet FormatSheet, Worksheet SigTestFormatSheet, bool CutPreWB, bool HasWeightColumn, bool IsReport = false)
        {
            string[] tmpNamesArray;
            // Warning!!! Optional parameters not supported
            //string tmpName=string.Empty;
            long tmp;
            string tmpSuffix = string.Empty;
            string fmt = string.Empty;
            bool IsWeight = false;
            string OrgProcName;
            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.AdjustPortraitFormat";
            // フォーマットシートのWB前全体行の削除
            if (CutPreWB)
            {
                if (IsReport)
                {
                    tmpNamesArray = Strings.Split("PreWBRows");
                    DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpNamesArray, tmpSuffix);
                    tmpNamesArray = Strings.Split("SA_MA_NP SA_MA_N SA_MA_P SAM_MAM_NP SAM_MAM_N SAM_MAM_P N NM");
                    tmpSuffix = "_PreWBRow";
                    foreach (var tmpName in tmpNamesArray)
                    {
                        FormatSheet.Names.Item(tmpName + tmpSuffix).Delete();
                    }

                }
                else
                {
                    tmpNamesArray = Strings.Split("SA_MA_NP SA_MA_N SA_MA_P SAM_MAM_NP SAM_MAM_N SAM_MAM_P N NM");
                    tmpSuffix = "_PreWBRow";
                    DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpNamesArray, tmpSuffix);
                }

            }

            //  ウエイト値、加重平均のセルの書式設定
            if (HasWeightColumn)
            {
                tmpNamesArray = Strings.Split("SA_MA_NP SA_MA_N SA_MA_P SAM_MAM_NP SAM_MAM_N SAM_MAM_P");
                tmp = CurrentOutput.ParentRequest.WeightNumDigitsAfterDecimal;
                tmpSuffix = "_WeightColumn";
                IsWeight = true;
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
                IsWeight = false;
                tmp = CurrentOutput.ParentRequest.WeightAverageNumDigitsAfterDecimal;
                tmpSuffix = "_WeightAverage";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }

            //  フォーマットシートの数値回答集計表フォーマットの調整
            TotalRowIndex = FormatSheet.Range["NM_PopulationRow"].Row - 1 - FormatSheet.Range["NM_DataColumns"].Row + 1;
            AverageRowIndex = 0;
            tmpNamesArray = Strings.Split("N NM");

            if (CurrentOutput.ParentRequest.ShowMedian)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Median);
                tmpSuffix = "_Median";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                tmpSuffix = "_MedianRow";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpNamesArray, tmpSuffix);
            }

            if (CurrentOutput.ParentRequest.ShowMaximum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Maximum);
                tmpSuffix = "_Maximum";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                tmpSuffix = "_MaximumRow";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpNamesArray, tmpSuffix);
            }

            if (CurrentOutput.ParentRequest.ShowMinimum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Minimum);
                tmpSuffix = "_Minimum";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                tmpSuffix = "_MinimumRow";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpNamesArray, tmpSuffix);
            }

            if (CurrentOutput.ParentRequest.ShowStdev)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Stdev);
                tmpSuffix = "_Deviation";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                tmpSuffix = "_DeviationRow";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpNamesArray, tmpSuffix);
            }

            if (CurrentOutput.ParentRequest.ShowAverage)
            {
                AverageRowIndex = FormatSheet.Range["NM_AverageRow"].Row - FormatSheet.Range["NM_DataColumns"].Row + 1;
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Average);
                tmpSuffix = "_Average";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                tmpSuffix = "_AverageRow";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpNamesArray, tmpSuffix);
            }

            if (CurrentOutput.ParentRequest.ShowSummary)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Summary);
                tmpSuffix = "_Summary";
                NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            else
            {
                tmpSuffix = "_SummaryRow";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpNamesArray, tmpSuffix);
                AverageRowIndex = AverageRowIndex - 1;
            }

            if (!CurrentOutput.ParentRequest.ShowParameter)
            {
                tmpSuffix = "_PopulationRow";
                DeleteName(ref FormatSheet, ref SigTestFormatSheet, tmpNamesArray, tmpSuffix);
                AverageRowIndex = AverageRowIndex - 1;
            }
            RunningProcName = OrgProcName;
        }

        private void CreateNormalGTArray(GTTable Table
        , long RowIndexMin, long RowIndexMax
        , long ColumnIndexMin, long ColumnIndexMax
        , ref Array TableStringValue, ref Array DataValue, ref Array Ranking, ref Array OptionNumbers
        , bool HasNA, bool HasIV, long NAIdx, long IVIdx
        , long AddColumnCount, bool HasWeight
        , TableType TableType, bool IsReport = false
        , bool CutMedian = false, long MedIdx = -1)
        {
            const long DATA_OFFSET_ROWS_COUNT = 1;
            const long DATA_OFFSET_COLUMNS_COUNT = 3;
            bool HasPreWBTotal;
            string buf = string.Empty;
            long RowsCount;
            long ColumnsCount;
            bool f;
            bool f2;
            long r;
            long c = 0;
            long x;
            long y;
            object tmp;
            bool IsSigTest;
            string OrgProcName;

            long tmpRowIndexFrom;
            long tmpRowIndexTo;
            long tmpColumnIndexFrom;
            long tmpColumnIndexTo;
            object[,] tmpTableValue;
            double[,] tmpPercentValue;
            object[,] tmpSignificanceTestCharacters;
            int[,] tmpRank;

            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.CreateNormalGTArray";
            // 出力表の文字列型データ配列のサイズ定義
            // ※文字列型データはString型配列で、数値型データはVariant型配列で、別途に処理する
            // パフォーマンスが出ないようならシート単位などでの一括出力に変更するが、まずは安全面を優先する
            IsSigTest = TableType == TableType.SignificanceTest;
            if (IsSigTest)
            {
                if (Table.SignificancetestCode != SignificanceTestCode.BetweenSectors)
                {
                    //Err().Raise vbObjectError + 100 &, RunningProcName _
                    //           , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportSignificanceTestSetupIsInconsistencyMessageIndex)
                }
            }

            RowsCount = RowIndexMax - RowIndexMin + 1;
            ColumnsCount = ColumnIndexMax - ColumnIndexMin + 1 + AddColumnCount;
            if (!HasNA & NAIdx >= RowIndexMin)
                RowsCount = RowsCount - 1;
            if (!HasIV & IVIdx >= RowIndexMin)
                RowsCount = RowsCount - 1;
            if (CutMedian)
                CutMedian = MedIdx >= RowIndexMin;
            if (CutMedian)
                RowsCount = RowsCount - 1;
            if (IsReport)
            {
                RowsCount = RowsCount + System.Convert.ToInt64(CutMedian);
            }
            else
            {
                switch (Table.Question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N))
                {
                    case QuestionType.SA:
                        {
                            buf = LocalResource.REPORT_SA_DESCRIPTION_KEYWORD;
                            break;
                        }

                    case QuestionType.MA:
                        {
                            buf = LocalResource.REPORT_MA_DESCRIPTION_KEYWORD;
                            break;
                        }

                    case QuestionType.N:
                        {
                            buf = LocalResource.REPORT_NA_DESCRIPTION_KEYWORD;
                            break;
                        }

                    default:
                        {
                            //Information.Err().Raise(Constants.vbObjectError + 100 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportUnjustQuestionTypeMessageIndex));
                            break;
                        }
                }
                if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                    buf = buf == LocalResource.REPORT_NA_DESCRIPTION_KEYWORD ? LocalResource.REPORT_N_MATRIX_DESCRIPTION_KEYWORD : buf + LocalResource.REPORT_MATRIX_DESCRIPTION_KEYWORD;
            }

            // ReDim TableStringValue(1& To (Not IsReport And 2&) + RowsCount, 1& To ColumnsCount + (TableType = TableType_NPer And 1&) + (IsSigTest And 2&))

            int Obj1param1 = (CrossCreator.ToInt((!IsReport)) & 2) + Convert.ToInt32(RowsCount);
            int Obj1param2 = Convert.ToInt32(ColumnsCount) + (CrossCreator.ToInt(TableType == TableType.NPer) & 1) + (CrossCreator.ToInt(IsSigTest) & 2);

            TableStringValue = Array.CreateInstance(typeof(string),
            new int[] { Obj1param1, Obj1param2 }, //length
            new int[] { 1, 1 }); //starting index


            //ReDim DataValue((Not IsReport And 2 &) +DATA_OFFSET_ROWS_COUNT + 1 & To UBound(TableStringValue, 1 &) _
            //          , ColumnsCount + (IsSigTest And 1 &) To UBound(TableStringValue, 2 &) -(IsSigTest And 1 &))

            Obj1param1 = TableStringValue.GetUpperBound(0);//TableStringValue.GetUpperBound(1);
            Obj1param2 = TableStringValue.GetUpperBound(1) - (CrossCreator.ToInt(IsSigTest) & 1); //Obj1param2 = TableStringValue.GetUpperBound(2) - (CrossCreator.ToInt(IsSigTest) & 1);


            int Obj2param1 = (CrossCreator.ToInt(!IsReport) & 2) + Convert.ToInt32(DATA_OFFSET_ROWS_COUNT) + 1;
            int Obj2param2 = Convert.ToInt32(ColumnsCount) + (CrossCreator.ToInt(IsSigTest) & 1);


            DataValue = Array.CreateInstance(typeof(object),
            new int[] { (Obj1param1 - Obj2param1) + 1, (Obj1param2 - Obj2param2) + 1 }, //length
            new int[] { Obj2param1, Obj2param2 }); //starting index

            OptionNumbers = Array.CreateInstance(typeof(object),
            new int[] { (Obj1param1 - Obj2param1) + 1, 1 }, //length
            new int[] { Obj2param1, 1 });

            //ReDim Ranking(LBound(DataValue, 1) To UBound(DataValue, 1), LBound(DataValue, 2) To UBound(DataValue, 2))

            //Obj1param1 = DataValue.GetUpperBound(1);
            //Obj1param2 = DataValue.GetUpperBound(2);

            //Obj2param1 = DataValue.GetLowerBound(1);
            //Obj2param2 = DataValue.GetLowerBound(2);

            Obj1param1 = DataValue.GetUpperBound(0);
            Obj1param2 = DataValue.GetUpperBound(1);

            Obj2param1 = DataValue.GetLowerBound(0);
            Obj2param2 = DataValue.GetLowerBound(1);


            Ranking = Array.CreateInstance(typeof(object),
        new int[] { (Obj1param1 - Obj2param1) + 1, (Obj1param2 - Obj2param2) + 1 }, //length
        new int[] { Obj2param1, Obj2param2 }); //starting index


            if (IsReport)
            {
                if (CurrentOutput.WBOn)
                    TableStringValue.SetValue(LocalResource.REPORT_MARKING_LEGEND_WEIGHTBACK_ON_PROMPT, 1, 1);
            }
            else
            {
                TableStringValue.SetValue(Table.Question.Name + Space(1) + Table.Question.Description, 1, 1);
                TableStringValue.SetValue(buf, 3, 2);
            }
            // 列見出し
            x = Information.LBound(DataValue, 2);
            y = 1 + (CrossCreator.ToInt(!IsReport) & 2);
            TableStringValue.SetValue((TableType != TableType.Per && TableType != TableType.SignificanceTest) ?
                        LocalResource.REPORT_GT_N_COLUMN_CAPTION : LocalResource.REPORT_GT_P_COLUMN_CAPTION, y, x);
            if (TableType == TableType.NPer)
            {
                x = x + 1;
                TableStringValue.SetValue(LocalResource.REPORT_GT_P_COLUMN_CAPTION, y, x);
            }
            if (IsSigTest)
            {
                x = x + 1;
                TableStringValue.SetValue(LocalResource.REPORT_SIGNIFICANCE_TEST_ROW_COLUMN_CAPTION, y, x);
            }
            // 行見出し
            y = Information.LBound(DataValue, 1) - 1;
            for (r = RowIndexMin + DATA_OFFSET_ROWS_COUNT; r <= RowIndexMax; r++)
            {
                do
                {
                    if (r == NAIdx)
                    {
                        if (!HasNA) goto EndDoWhile1;
                    }
                    else if (r == IVIdx)
                    {
                        if (!HasIV) goto EndDoWhile1;
                    }
                    else if (r == MedIdx)
                    {
                        if (CutMedian) goto EndDoWhile1;
                    }

                    y = y + 1;
                    x = 0;
                    for (c = ColumnIndexMin; c <= ColumnIndexMax - 1; c++)
                    {
                        x = x + 1;
                        buf = Table.TableValue(Convert.ToInt32(r), Convert.ToInt32(c), true);
                        if (Strings.Len(buf) > 0)
                        {
                            if (c == (Table.GetTableValueColumnIndexMinimum + 2) && Table.Question.HasCount) continue; //#OutputFormatting
                            TableStringValue.SetValue(buf, y, x);
                            if (x == 1)
                            {
                                OptionNumbers.SetValue(buf, y, 1);
                            }
                        }
                        if (IsSigTest)
                        {
                            if (c == ColumnIndexMin + 1)
                            {
                                buf = Table.SignificanceTestCharacters(Convert.ToInt32(r), Convert.ToInt32(c));
                                if (Strings.Len(buf) > 0)
                                    TableStringValue.SetValue(buf, y, (Information.LBound(DataValue, 2) - 1));
                            }
                        }
                    }
                }
                while (!true);

            EndDoWhile1:;
            }
            // 性能対策 start
            tmpRowIndexFrom = RowIndexMin + DATA_OFFSET_ROWS_COUNT;
            tmpRowIndexTo = RowIndexMax;
            tmpColumnIndexFrom = c;
            tmpColumnIndexTo = c;
            tmpTableValue = Table.TableValueByMatrix(Convert.ToInt32(tmpRowIndexFrom), Convert.ToInt32(tmpRowIndexTo), Convert.ToInt32(tmpColumnIndexFrom), Convert.ToInt32(tmpColumnIndexTo));
            tmpPercentValue = Table.PercentValueByMatrix(Convert.ToInt32(tmpRowIndexFrom), Convert.ToInt32(tmpRowIndexTo), Convert.ToInt32(tmpColumnIndexFrom), Convert.ToInt32(tmpColumnIndexTo));
            tmpSignificanceTestCharacters = Table.SignificanceTestCharactersByMatrix(Convert.ToInt32(tmpRowIndexFrom), Convert.ToInt32(tmpRowIndexTo), Convert.ToInt32(tmpColumnIndexFrom), Convert.ToInt32(tmpColumnIndexTo));
            tmpRank = Table.RankByMatrix(Convert.ToInt32(tmpRowIndexFrom), Convert.ToInt32(tmpRowIndexTo), Convert.ToInt32(tmpColumnIndexFrom), Convert.ToInt32(tmpColumnIndexTo));
            // 性能対策 end
            // データ
            HasPreWBTotal = CurrentOutput.ShowPreWBTotal;
            y = Information.LBound(DataValue, 1) - 1;
            for (r = RowIndexMin + DATA_OFFSET_ROWS_COUNT; r <= RowIndexMax; r++)
            {
                do
                {
                    if (r == NAIdx)
                    {
                        if (!HasNA) goto DoWhileEnd2;
                    }
                    else if (r == IVIdx)
                    {
                        if (!HasIV) goto DoWhileEnd2;
                    }
                    else if (r == MedIdx)
                    {
                        if (CutMedian) goto DoWhileEnd2;
                    }

                    y = y + 1;
                    f2 = false;
                    f = HasPreWBTotal & r == RowIndexMin + DATA_OFFSET_ROWS_COUNT;
                    if (!f)
                        f = r == RowIndexMin + DATA_OFFSET_ROWS_COUNT + (CrossCreator.ToInt(HasPreWBTotal) & 1);
                    if (!f)
                    {
                        if (HasWeight)
                        {
                            f = r >= RowIndexMax - 1;
                            f2 = r == RowIndexMax;
                        }
                    }

                    x = Information.LBound(DataValue, 2);
                    // buf = Table.TableValue(r, c)
                    // If Not IsNumeric(buf) Then buf = "0"
                    // tmp = Table.TableValue(r, c)
                    tmp = tmpTableValue[r, c];
                    if (Information.IsNumeric(tmp))
                        tmp = Convert.ToDouble(tmp);
                    switch (TableType)
                    {
                        case TableType.NPer:
                            {
                                DataValue.SetValue(tmp, y, x);
                                if (!f)
                                    DataValue.SetValue(tmpPercentValue[r, c], y, x + 1);
                                break;
                            }

                        case TableType.N:
                            {
                                DataValue.SetValue(tmp, y, x);
                                break;
                            }

                        case TableType.Per:
                        case TableType.SignificanceTest:
                            {
                                if (f)
                                    DataValue.SetValue(tmp, y, x);
                                else
                                {
                                    DataValue.SetValue(tmpPercentValue[r, c], y, x);
                                    if (IsSigTest)
                                    {
                                        buf = Convert.ToString(tmpSignificanceTestCharacters[r, c]);
                                        if (Strings.Len(buf) > 0)
                                            TableStringValue.SetValue(buf, y, x + 1);
                                    }
                                }
                                break;
                            }
                    }
                    Ranking.SetValue(tmpRank[r, c], y, x);
                }
                while (!true);// WB前全体// 全体// 統計量母数か加重平均// 加重平均
            DoWhileEnd2:;
            }
            RunningProcName = OrgProcName;
        }

        private string Space(int count = 1)
        {
            string space = " ";
            string retVal = string.Empty;
            for (int i = 1; i <= count; i++)
            {
                retVal = retVal + space;
            }
            return retVal;
        }

        private void OutputData(GTTable Table
        , ref Array TableStringValue, ref Array DataValue, ref Array Ranking
        , long SectorsCount, long ChildQuestionsCount
        , TableOrientation Orientation, string FormattedIndex
        , bool CutNA, bool CutIV, bool HasWeight
        , bool UseWeightFormat
        , QuestionType QuestionType, TableType TableType
        , Excel.Worksheet FormatSheet, ref Excel.Range StartCell, ref Excel.Range PageSetupStartCell
        , bool IsLastTable, Excel.Worksheet WorkingSheet
        , ref Excel.Range TableRange, ref long RemainedPageSetupRowsCount
        , bool IsReport = false, bool CutMedian = false
        , bool HasLetterColumn = false
        , bool NotRevise = false, Array OptionNumbers = null, Array OptionNumbersTop = null)
        {
            //CutIV = false;

            string TableCaption = string.Empty;
            string GraphCaption;

            string RangeNameLeftPart = string.Empty;
            string RangeNameRightPart = string.Empty;
            string BaseRangeName;
            string FormatRangeName;
            bool isMatrix;
            bool isN;
            bool CutWT = false;
            Excel.Range FormatRange;
            Excel.Range WorkRange;
            long WorkRowsCount;

            Excel.Range tmpRange;
            Excel.Range SectorRange;
            Excel.Range DataRange;
            Excel.Range NARange;
            Excel.Range IVRange;
            Excel.Range MedRange;
            Excel.Range WTAveRange;
            long rs;
            long cs;
            long rc;
            long cc;
            XlDeleteShiftDirection DelShiftDirection;
            XlInsertShiftDirection InsShiftDirection;
            long d;
            long r;
            long c;
            long clr;
            Shape o;
            long FixedColumnIndex = 0;
            long MaxSectorsCountPerPage = 0;
            long PagesCount;  // 折り返し表数
            long LastPageColumnsCount = 0;
            long PageSetupRowsCount;  // 折り返しトータル行数
            double PageSetupHeight;   // 折り返しトータル行高
            string tmp;
            long n;   // 折り返し後の既定行高換算行数
            long x;   // シート上のページ数
            double RemainedHeight;
            double PageRangeHeight;
            long PageRowsCount;
            Excel.Worksheet PageSetupSheet;
            long i;

            long CutColumnsCount = 0;
            bool OnlyCutWTColumn = false;
            bool IsSigTest;
            long diff;
            bool CutLetterRow;

            bool HasNA;
            bool HasIV;
            long SecColsCount = 0;

            Array tmpValue;
            Array tmpStrValue;

            string OrgProcName;
            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.OutputData";

            isMatrix = (QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
            isN = (QuestionType & QuestionType.N) == QuestionType.N;
            IsSigTest = TableType == TableType.SignificanceTest;

            if (Orientation == TableOrientation.Landscape)
            {
                if (!isN)
                {
                    HasNA = CurrentOutput.ShowNAAtItem & !CutNA;
                    HasIV = CurrentOutput.ShowIVAtItem & !CutIV;
                    SecColsCount = SectorsCount + (CrossCreator.ToInt(HasNA) & 1) + (CrossCreator.ToInt(HasIV) & 1) + (CrossCreator.ToInt(HasWeight) & 2);
                }
            }

            if (StartCell.Row + 1 + Information.UBound(TableStringValue, 1) > StartCell.Worksheet.Rows.Count)
                // Err().Raise vbObjectError + 100&, RunningProcName, ThisWorkbook.GetMessage(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex)
                goto ExitProc;
            if (StartCell.Column + Information.UBound(TableStringValue, 2) > StartCell.Worksheet.Columns.Count)
            {
                // ReDim Preserve TableStringValue(1& To UBound(TableStringValue, 1&), 1& To StartCell.Worksheet.Columns.Count - StartCell.Column)
                CutColumnsCount = Information.UBound(TableStringValue, 2) - (StartCell.Worksheet.Columns.Count - StartCell.Column);
                if (CutColumnsCount == 1)
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        if (!isN & HasWeight)
                            CutColumnsCount = 2;
                    }
                }
                //ReDim Preserve TableStringValue(1 & To UBound(TableStringValue, 1 &), 1 & To UBound(TableStringValue, 2 &) - CutColumnsCount)


                //TableStringValue = Array.CreateInstance(typeof(object),
                //new int[] { TableStringValue.GetUpperBound(1), TableStringValue.GetUpperBound(2) },
                //new int[] { 1, 1 });

                TableStringValue = Array.CreateInstance(typeof(string),
        new int[] { TableStringValue.GetUpperBound(0), TableStringValue.GetUpperBound(1) },
        new int[] { 1, 1 });

                //ReDim Preserve DataValue(LBound(DataValue, 1 &) To UBound(DataValue, 1 &), LBound(DataValue, 2 &) To UBound(DataValue, 2 &) - CutColumnsCount)

                //int obj1Item1 = DataValue.GetUpperBound(1);
                //int obj1Item2 = DataValue.GetUpperBound(2) - Convert.ToInt32(CutColumnsCount);

                //int obj2Item1 = DataValue.GetLowerBound(1);
                //int obj2Item2 = DataValue.GetLowerBound(2);


                int obj1Item1 = DataValue.GetUpperBound(0);
                int obj1Item2 = DataValue.GetUpperBound(1) - Convert.ToInt32(CutColumnsCount);

                int obj2Item1 = DataValue.GetLowerBound(0);
                int obj2Item2 = DataValue.GetLowerBound(1);


                DataValue = Array.CreateInstance(typeof(object),
                new int[] { obj1Item1 - obj2Item1 + 1, obj1Item2 - obj2Item2 + 1 },
                new int[] { obj2Item1, obj2Item2 });

            }
            if (!IsReport)
            {
                TableCaption = "Table" + FormattedIndex;
                switch (TableType)
                {
                    case TableType.N:
                        {
                            TableCaption = "N" + TableCaption;
                            break;
                        }

                    case TableType.Per:
                        {
                            TableCaption = "P" + TableCaption;
                            break;
                        }

                    case TableType.SignificanceTest:
                        {
                            TableCaption = "T" + TableCaption;
                            break;
                        }
                }
            }

            // 雛形のコピーと調整
            //if (!IsReport || !isN)
            if (!isN)
            {
                CutMedian = false;
            }

            d = 1;
            if (isMatrix)
            {
                if ((QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                {
                    RangeNameLeftPart = "SAM_MAM_";
                    if (TableType == TableType.NPer)
                        d = 2;
                    d = d + (CrossCreator.ToInt(IsSigTest) & 1);
                }
                else if (isN)
                    RangeNameLeftPart = "NM";
                else
                {
                    // エラースロー
                    //Information.Err().Raise(Constants.vbObjectError + 100 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportUnjustQuestionTypeMessageIndex));
                }
            }
            else if ((QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                RangeNameLeftPart = "SA_MA_";
            else if (isN)
                RangeNameLeftPart = "N";
            else
            {
                // エラースロー
                //Information.Err().Raise(Constants.vbObjectError + 100 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportUnjustQuestionTypeMessageIndex));
            }
            if (!isN)
            {
                switch (TableType)
                {
                    case TableType.NPer:
                        {
                            RangeNameRightPart = "NP";
                            break;
                        }

                    case TableType.N:
                        {
                            RangeNameRightPart = "N";
                            break;
                        }

                    case TableType.Per:
                    case TableType.SignificanceTest:
                        {
                            RangeNameRightPart = "P";
                            break;
                        }
                }
            }
            BaseRangeName = RangeNameLeftPart + RangeNameRightPart;
            FormatRangeName = BaseRangeName;
            if (IsReport)
            {
                if (Orientation == TableOrientation.Portrait)
                {
                    if (HasWeight)
                        FormatRangeName = FormatRangeName + "_WT";
                }
            }

            FormatRange = FormatSheet.Range[FormatRangeName];

            if (Orientation == TableOrientation.Landscape)
            {
                DelShiftDirection = XlDeleteShiftDirection.xlShiftToLeft;
                InsShiftDirection = XlInsertShiftDirection.xlShiftToRight;
            }
            else
            {
                DelShiftDirection = XlDeleteShiftDirection.xlShiftUp;
                InsShiftDirection = XlInsertShiftDirection.xlShiftDown;
            }

            if (!isN)
            {
                CutWT = UseWeightFormat & !HasWeight;
                if (Orientation == TableOrientation.Landscape)
                {
                    if (!CutWT)
                    {
                        if (CutColumnsCount >= 2)
                        {
                            if (HasWeight)
                            {
                                CutWT = true;
                                OnlyCutWTColumn = true;
                                CutColumnsCount = CutColumnsCount - 2;
                            }
                        }
                    }
                }
            }
            if (Orientation == TableOrientation.Landscape)
            {
                if (CutColumnsCount >= 1)
                {
                    if (!CutIV)
                    {
                        CutIV = true;
                        CutColumnsCount = CutColumnsCount - 1;
                    }
                }
                if (CutColumnsCount >= 1)
                {
                    if (!CutNA)
                    {
                        CutNA = true;
                        CutColumnsCount = CutColumnsCount - 1;
                    }
                }
                if (CutColumnsCount >= 1)
                    SectorsCount = SectorsCount - CutColumnsCount;
            }
            else if (CutColumnsCount >= 1)
                ChildQuestionsCount = ChildQuestionsCount - CutColumnsCount;

            // 作業シートで調整 (直接だと、出せるはずなのにテンプレートのサイズで出せないとかありえるから)
            WorkingSheet.UsedRange.Clear();
            WorkingSheet.DrawingObjects().Delete();
            {
                var withBlock = FormatRange.EntireRow;
                string namesf = withBlock.Worksheet.Name;
                withBlock.Copy(WorkingSheet.Range["A1"]);
                WorkRowsCount = withBlock.Rows.Count;
                WorkRange = WorkingSheet.Range["A1"].Resize[WorkRowsCount, withBlock.Columns.Count];
            }
            PagesCount = 1;
            {
                var withBlock = WorkRange;
                // 不要行列の削除または必要行列の追加
                if (CutWT)
                {
                    {
                        var withBlock1 = FormatRange.Worksheet;
                        if (Orientation == TableOrientation.Landscape)
                            tmpRange = withBlock1.Range[withBlock1.Range[BaseRangeName + "_PopulationColumn"]
                                          , withBlock1.Range[BaseRangeName + "_WeightAverageColumn"]];

                        else if (!IsReport)
                            tmpRange = withBlock1.Range[withBlock1.Range[BaseRangeName + "_PopulationRow"]
                                          , withBlock1.Range[BaseRangeName + "_WeightAverageRow"]];
                        else
                            tmpRange = null;
                    }
                    if (!(tmpRange == null))
                    {
                        {
                            var withBlock1 = tmpRange;
                            rs = withBlock1.Row - FormatRange.Row + 1;
                            cs = withBlock1.Column - FormatRange.Column + 1;
                            rc = withBlock1.Rows.Count;
                            cc = withBlock1.Columns.Count;
                        }
                        WTAveRange = withBlock.Item[rs, cs].Resize[rc, cc];
                        if (DelShiftDirection == XlDeleteShiftDirection.xlShiftUp)
                            WorkRowsCount = WorkRowsCount - rc;
                        WTAveRange.Delete(DelShiftDirection);
                    }
                    //if (!OnlyCutWTColumn)
                    //{
                    //    if (Orientation == TableOrientation.Landscape)
                    //    {
                    //        {
                    //            var withBlock1 = FormatRange;
                    //            rs = withBlock1.Worksheet.Range[BaseRangeName + "_WeightRow"].Row - withBlock1.Row + 1;
                    //        }
                    //        WorkRowsCount = WorkRowsCount - 1;
                    //        withBlock.Item[rs, 1].EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                    //        if (IsReport)
                    //        {
                    //            cs = FormatRange.Worksheet.Range[BaseRangeName + "_DataRows"].Column;
                    //            withBlock.Item[rs - 1, cs].VerticalAlignment = Excel.Constants.xlBottom;
                    //        }
                    //    }
                    //    else if (IsReport)
                    //    {
                    //        cs = FormatRange.Worksheet.Range[BaseRangeName + "_WeightColumn"].Column;
                    //        withBlock.Columns.Item[cs].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    //    }
                    //}
                }
                if (CutIV)
                {
                    {
                        var withBlock1 = FormatRange.Worksheet;
                        if (Orientation == TableOrientation.Landscape)
                            tmpRange = withBlock1.Range[BaseRangeName + "_InvalidColumn"];
                        else
                            tmpRange = withBlock1.Range[BaseRangeName + "_InvalidRow"];
                    }
                    {
                        var withBlock1 = tmpRange;
                        rs = withBlock1.Row - FormatRange.Row + 1;
                        cs = withBlock1.Column - FormatRange.Column + 1;
                        rc = withBlock1.Rows.Count;
                        cc = withBlock1.Columns.Count;
                    };
                    IVRange = withBlock.Item[rs, cs].Resize(rc, cc);

                    if (DelShiftDirection == XlDeleteShiftDirection.xlShiftUp)
                        WorkRowsCount = WorkRowsCount - rc;
                    IVRange.Delete(DelShiftDirection);
                }
                if (CutNA)
                {
                    {
                        var withBlock1 = FormatRange.Worksheet;
                        if (Orientation == TableOrientation.Landscape)
                            tmpRange = withBlock1.Range[BaseRangeName + "_NoAnswerColumn"];
                        else
                            tmpRange = withBlock1.Range[BaseRangeName + "_NoAnswerRow"];
                    }
                    {
                        var withBlock1 = tmpRange;
                        rs = withBlock1.Row - FormatRange.Row + 1;
                        cs = withBlock1.Column - FormatRange.Column + 1;
                        rc = withBlock1.Rows.Count;
                        cc = withBlock1.Columns.Count;
                    }
                    NARange = withBlock.Item[rs, cs].Resize(rc, cc);
                    if (DelShiftDirection == XlDeleteShiftDirection.xlShiftUp)
                        WorkRowsCount = WorkRowsCount - rc;
                    NARange.Delete(DelShiftDirection);
                }
                if (isN)
                {
                    if (CutMedian)
                    {
                        {
                            var withBlock1 = FormatRange.Worksheet;
                            if (Orientation == TableOrientation.Landscape)
                                tmpRange = withBlock1.Range[BaseRangeName + "_MedianColumn"];
                            else
                                tmpRange = withBlock1.Range[BaseRangeName + "_MedianRow"];
                        }
                        {
                            var withBlock1 = tmpRange;
                            rs = withBlock1.Row - FormatRange.Row + 1;
                            cs = withBlock1.Column - FormatRange.Column + 1;
                            rc = withBlock1.Rows.Count;
                            cc = withBlock1.Columns.Count;
                        }
                        MedRange = withBlock.Item[rs, cs].Resize(rc, cc);
                        if (DelShiftDirection == XlDeleteShiftDirection.xlShiftUp)
                            WorkRowsCount = WorkRowsCount - rc;
                        MedRange.Delete(DelShiftDirection);
                    }
                    if (!(PageSetupStartCell == null))
                    {
                        if (Orientation == TableOrientation.Landscape)
                        {
                            FixedColumnIndex = 1 & +TotalColumnIndex;
                            MaxSectorsCountPerPage = MaxColumnsCountPerPage - FixedColumnIndex;
                            PagesCount = (1 + Information.UBound(TableStringValue, 2) - Information.LBound(TableStringValue, 2) + 1 - FixedColumnIndex - 1) / MaxSectorsCountPerPage + 1;
                            LastPageColumnsCount = (1 + Information.UBound(TableStringValue, 2) - Information.LBound(TableStringValue, 2) + 1 - FixedColumnIndex - 1) % MaxSectorsCountPerPage + 1;
                        }
                    }
                }
                else
                {
                    {
                        var withBlock1 = FormatRange.Worksheet;
                        if (Orientation == TableOrientation.Landscape)
                            tmpRange = withBlock1.Range[BaseRangeName + "_SectorColumns"];
                        else
                            tmpRange = withBlock1.Range[BaseRangeName + "_SectorRows"];
                    }
                    {
                        var withBlock1 = tmpRange;
                        rs = withBlock1.Row - FormatRange.Row + 1;
                        cs = withBlock1.Column - FormatRange.Column + 1;
                        rc = withBlock1.Rows.Count;
                        cc = withBlock1.Columns.Count;
                    }
                    SectorRange = withBlock.Item[rs, cs].Resize(rc, cc);
                    if (SectorsCount > 2)
                    {
                        if (Orientation == TableOrientation.Landscape)
                        {
                            {
                                var withBlock1 = SectorRange.Columns.Item[2];
                                withBlock1.Copy();
                                tmpRange = withBlock1.Resize(ColumnSize: SectorsCount - 2);  // Set tmpRange = .Resize(, SectorsCount - 2&)
                            }
                        }
                        else
                        {
                            var withBlock1 = SectorRange.EntireRow.Item[d + 1].Resize(d);
                            withBlock1.Copy();
                            tmpRange = withBlock1.Resize((SectorsCount - 2) * d);
                        }
                        if (InsShiftDirection == XlInsertShiftDirection.xlShiftDown)
                        {
                            WorkRowsCount = WorkRowsCount + tmpRange.Rows.Count;
                        }

                        tmpRange.Insert(InsShiftDirection);
                        xlApp.CutCopyMode = (XlCutCopyMode)1;
                    }
                    else if (SectorsCount < 2)
                    {
                        if (Orientation == TableOrientation.Landscape)
                            tmpRange = SectorRange.EntireColumn.Item[2].Resize(ColumnSize: 2 - SectorsCount); // Set tmpRange = SectorRange.EntireColumn.Item(2).Resize(, 2& - SectorsCount)
                        else
                            tmpRange = SectorRange.EntireRow.Item[d + 1].Resize((2 + (-SectorsCount)) * d);

                        if (DelShiftDirection == XlDeleteShiftDirection.xlShiftUp)
                            WorkRowsCount = WorkRowsCount - tmpRange.Rows.Count;
                        tmpRange.Delete(DelShiftDirection);
                    }
                }
                if (isMatrix)
                {
                    {
                        var withBlock1 = FormatRange.Worksheet;
                        CutLetterRow = IsSigTest;
                        if (CutLetterRow)
                        {
                            if (Orientation == TableOrientation.Landscape)
                            {

                                if (isN || (Table.SignificancetestCode == SignificanceTestCode.BetweenSectors))
                                {
                                    CutLetterRow = false;
                                }
                            }
                            else
                                CutLetterRow = Table.SignificancetestCode == SignificanceTestCode.BetweenSectors;
                        }
                        if (CutLetterRow)
                        {
                            tmpRange = withBlock1.Range[BaseRangeName + "_LetterRow"];
                            rs = tmpRange.Row - FormatRange.Row + 1;

                            if (!(Orientation == TableOrientation.Landscape) || !(UseWeightFormat) || !(!HasWeight))
                            {

                            }
                            else
                            {
                                //  rs = rs - 1;
                            }


                            WorkRange.Rows.Item[rs].EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                        if (Orientation == TableOrientation.Portrait)
                        {
                            tmpRange = withBlock1.Range[BaseRangeName + "_DataColumns"];
                            if (!(PageSetupStartCell == null))
                            {
                                FixedColumnIndex = tmpRange.Column - 1;
                                MaxSectorsCountPerPage = MaxColumnsCountPerPage - FixedColumnIndex;
                                PagesCount = (ChildQuestionsCount - 1) / MaxSectorsCountPerPage + 1;
                                LastPageColumnsCount = (ChildQuestionsCount - 1) % MaxSectorsCountPerPage + 1;
                            }
                        }
                        else
                        {
                            tmpRange = withBlock1.Range[BaseRangeName + "_DataRows"];
                            if (!isN)
                            {
                                if (!(PageSetupStartCell == null))
                                {
                                    FixedColumnIndex = withBlock1.Range[BaseRangeName + "_TotalDescription"].Column;
                                    MaxSectorsCountPerPage = MaxColumnsCountPerPage - FixedColumnIndex;
                                    PagesCount = (SecColsCount - 1) / MaxSectorsCountPerPage + 1;
                                    LastPageColumnsCount = (SecColsCount - 1) % MaxSectorsCountPerPage + 1;
                                }
                            }
                        }
                    }
                    {
                        var withBlock1 = tmpRange;
                        rs = withBlock1.Row - FormatRange.Row + 1;
                        if (Orientation == TableOrientation.Landscape)
                        {
                            if (!isN)
                            {
                                // if (UseWeightFormat & !HasWeight)
                                //  rs = rs - 1;
                            }
                        }
                        cs = withBlock1.Column - FormatRange.Column + 1;
                        rc = withBlock1.Rows.Count;
                        cc = withBlock1.Columns.Count;
                    }
                    DataRange = withBlock.Item[rs - (CrossCreator.ToInt(CutLetterRow) & 1), cs].Resize(rc, cc);
                    if (ChildQuestionsCount > 3)
                    {
                        if (Orientation == TableOrientation.Landscape)
                        {
                            {
                                var withBlock1 = DataRange.EntireRow.Resize[d];
                                withBlock1.Copy();
                                WorkRowsCount = WorkRowsCount + (ChildQuestionsCount - 3) * d;
                                withBlock1.Resize[(ChildQuestionsCount - 3) * d].Insert(XlInsertShiftDirection.xlShiftDown);
                            }
                        }
                        else
                        // With DataRange.EntireColumn.Resize(, 1)
                        {
                            var withBlock1 = DataRange;
                            {
                                var withBlock2 = withBlock1.Rows.Item[1].Resize(withBlock1.Worksheet.Rows.Count - withBlock1.Row + 1).Columns.Item[1];
                                withBlock2.Copy();
                                withBlock2.Resize(ColumnSize: ChildQuestionsCount - 3).Insert(XlInsertShiftDirection.xlShiftToRight);
                            }
                        }
                        xlApp.CutCopyMode = (XlCutCopyMode)1;
                    }
                    else if (ChildQuestionsCount < 3)
                    {
                        if (Orientation == TableOrientation.Landscape)
                        {
                            WorkRowsCount = WorkRowsCount - (3 - ChildQuestionsCount) * d;
                            DataRange.EntireRow.Resize[(3 - ChildQuestionsCount) * d].Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                        else
                            DataRange.EntireColumn.Resize[null, (3 - ChildQuestionsCount)].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    }
                }
                // 作業セル範囲の再設定 (念のため)
                {
                    var withBlock1 = withBlock.Worksheet;
                    WorkRange = withBlock1.Rows.Resize[WorkRowsCount];
                }
            }
            // フォーマットのコピー
            {
                var withBlock = WorkRange;
                if (withBlock.Count == StartCell.Worksheet.Rows.Count)
                {
                    withBlock.Item[1].Copy(StartCell);
                    withBlock.Item[2].Resize(withBlock.Count - 1).Copy(StartCell.Range["A2"]);
                }
                else
                    withBlock.Copy(StartCell);
            }
            // 出力値の投入
            // If Not IsReport Then StartCell.Range("B2").Value = TableCaption
            if (!IsReport)
                OutputUtil.PutValue(StartCell.Range["B2"], ref TableCaption);
            if (IsReport)
                tmpRange = StartCell.Range["B1"];
            else
                tmpRange = StartCell.Range["B3"];
            {
                var withBlock = tmpRange.Resize[Information.UBound(TableStringValue, 1), Information.UBound(TableStringValue, 2)];
                if (IsReport)
                    TableRange = withBlock.Cells;
                else
                    TableRange = withBlock.Range["A3"].Resize[withBlock.Rows.Count - 2, withBlock.Columns.Count];
                // .Value = TableStringValue
                OutputUtil.PutValue(withBlock.Cells, ref TableStringValue);
                if (OptionNumbers != null)
                {
                    var withBlock2 = withBlock.Item[Information.LBound(OptionNumbers, 1), 1]
                        .Resize[Information.UBound(OptionNumbers, 1) - Information.LBound(OptionNumbers, 1) + 1, 1];
                    OutputUtil.PutValue(withBlock2.Cells, ref OptionNumbers);
                }
                if (OptionNumbersTop != null)
                {
                    var withBlock2 = withBlock.Item[3, Information.LBound(OptionNumbersTop, 2)]
                        .Resize[1, Information.UBound(OptionNumbersTop, 2) - Information.LBound(OptionNumbersTop, 2) + 1];
                    OutputUtil.PutValue(withBlock2.Cells, ref OptionNumbersTop);
                }
                if (!IsReport)
                {
                    if (isMatrix)
                    {
                        try
                        {
                            // for (int k = 4; k <= withBlock.Rows.Count; k++)
                            OutputUtil.AutoFitEx(withBlock.Rows.Item[4], xlApp, WorkingSheet, Util.Constants.ExcelRowMaxHeight);
                            Range labelRange =
                                withBlock.Worksheet.Range[
                                withBlock.Item[Information.LBound(DataValue, 1), 1],
                                withBlock.Item[Information.UBound(DataValue, 1), 2]
                                ];
                            OutputUtil.AutoFitExCrossLabel(labelRange, xlApp, WorkingSheet, Util.Constants.ExcelRowMaxHeight);
                        }
                        catch (Exception e)
                        {
                            _log.Error(e.Message + "\n" + e.StackTrace);
                        }
                        //OutputUtil.AutoFitEx(withBlock.Rows.Item[4], xlApp, WorkingSheet, Util.Constants.ExcelRowMaxHeight);
                    }
                    else if (isN)
                    {
                        OutputUtil.AutoFitEx(withBlock.Rows.Item[3 + (CrossCreator.ToInt(Orientation == TableOrientation.Portrait) & 1)], xlApp, WorkingSheet, Util.Constants.ExcelRowMaxHeight);
                    }
                    else //#OutputFormatting - new implementation for autofit in SA/MA table type
                    {
                        try
                        {
                            for (int k = 1; k <= withBlock.Rows.Count; k++)
                                OutputUtil.AutoFitEx(withBlock.Rows.Item[k], xlApp, WorkingSheet, Util.Constants.ExcelRowMaxHeight);
                        }
                        catch (Exception e)
                        {
                            _log.Error(e.Message + "\n" + e.StackTrace);
                        }
                    }

                    OutputUtil.AdjustQuestionSentanceRowHeight(TableRange, WorkingSheet, xlApp);

                }
                //PutValue .Worksheet.Range(.Item(LBound(DataValue, 1 &), LBound(DataValue, 2 &)), .Item(UBound(DataValue, 1 &), UBound(DataValue, 2 &))), DataValue, NotRevise
                OutputUtil.PutValue(withBlock.Worksheet.Range[withBlock.Item[Information.LBound(DataValue, 1), Information.LBound(DataValue, 2)], withBlock.Item[Information.UBound(DataValue, 1), Information.UBound(DataValue, 2)]], ref DataValue, NotRevise);

                int ColLenAutoFitCol = Information.UBound(DataValue, 2);
                int ColLenAutoFitRow = Information.UBound(DataValue, 1);
                if (HasWeight)
                {
                    if (isMatrix)
                    {
                        ColLenAutoFitCol -= 2;
                    }
                    else
                    {
                        ColLenAutoFitRow -= 2;
                    }
                }
                // for 178447
                if (!IsSigTest)
                {
                    OutputUtil.AutofitColumn(withBlock.Worksheet.Range[withBlock.Item[Information.LBound(DataValue, 1), Information.LBound(DataValue, 2)], withBlock.Item[ColLenAutoFitRow, ColLenAutoFitCol]]);
                }

                if (HasWeight)
                {
                    diff = 1;
                    if (Orientation == TableOrientation.Landscape)
                    {
                        if (!IsSigTest || !(Table.SignificancetestCode == SignificanceTestCode.BetweenSectors))
                        {

                        }
                        else
                        {
                            diff = 2;
                        }

                        {
                            var withBlock1 = withBlock.Item[Information.LBound(DataValue, 1) - diff, Information.LBound(DataValue, 2) + (CrossCreator.ToInt(CurrentOutput.ShowPreWBTotal) & 1) + 1].Resize(ColumnSize: SectorsCount);
                            withBlock1.Value = withBlock1.Value;
                        }
                    }
                    else
                    {
                        if (HasLetterColumn)
                            diff = 2;
                        {
                            var withBlock1 = withBlock.Item[Information.LBound(DataValue, 1) + (CrossCreator.ToInt(CurrentOutput.ShowPreWBTotal) & 1) + 1, Information.LBound(DataValue, 2) - diff].Resize(SectorsCount * d);
                            withBlock1.Value = withBlock1.Value;
                        }
                    }
                }
                tmpRange = TableRange;

                if (isN & Orientation == TableOrientation.Portrait)
                {
                    if (!IsReport | !isMatrix)
                        tmpRange = tmpRange.Range["B1"].Resize[tmpRange.Rows.Count, tmpRange.Columns.Count - 1];
                }
                {
                    var withBlock1 = tmpRange.Borders;
                    {
                        var withBlock2 = withBlock1.Item[XlBordersIndex.xlEdgeRight];
                        withBlock2.LineStyle = XlLineStyle.xlContinuous;
                        withBlock2.Weight = XlBorderWeight.xlThin;
                        withBlock2.Color = Util.Constants.GT.GTColBorder.ToArgb();
                    }
                    {
                        var withBlock2 = withBlock1.Item[XlBordersIndex.xlEdgeBottom];
                        withBlock2.LineStyle = XlLineStyle.xlContinuous;
                        withBlock2.Weight = XlBorderWeight.xlThin;
                        withBlock2.Color = Util.Constants.GT.GTColBorder.ToArgb();
                    }
                    if (Table.Question.SubTotalCnt > 0)
                    {
                        if (!isMatrix)
                        {
                            var withBlockSubT = tmpRange.Rows.Item[1].Resize[tmpRange.Rows.Count - (Table.Question.SubTotalCnt + (HasWeight ? 2 : 0)) * d];
                            var withBlockBorder = withBlockSubT.Borders;
                            var withBlock2 = withBlockBorder.Item[XlBordersIndex.xlEdgeBottom];
                            withBlock2.LineStyle = XlLineStyle.xlContinuous;
                            withBlock2.Weight = XlBorderWeight.xlThin;
                            withBlock2.Color = Util.Constants.GT.GTColBorder.ToArgb();
                        }
                        else
                        {
                            var withBlockSubT = tmpRange.Columns.Item[1].Resize[ColumnSize: tmpRange.Columns.Count - (Table.Question.SubTotalCnt + (HasWeight ? 2 : 0))];
                            var withBlockBorder = withBlockSubT.Borders;
                            var withBlock2 = withBlockBorder.Item[XlBordersIndex.xlEdgeRight];
                            withBlock2.LineStyle = XlLineStyle.xlContinuous;
                            withBlock2.Weight = XlBorderWeight.xlThin;
                            withBlock2.Color = Util.Constants.GT.GTColBorder.ToArgb();
                        }
                    }
                }
                // マーキング
                if (CurrentOutput.MarkingRanking)
                {
                    for (r = Information.LBound(DataValue, 1); r <= Information.UBound(DataValue, 1); r++)
                    {
                        do
                        {
                            for (c = Information.LBound(DataValue, 2); c <= Information.UBound(DataValue, 2); c++)
                            {
                                do
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
                                            clr = Information.RGB(0x33, 0x99, 0x66);
                                            break;
                                        default:
                                            continue;
                                    }

                                    //o = withBlock.Worksheet.Ovals.Add(withBlock.Item(r, c).Left + 2.25, withBlock.Item(r, c).Top + 2.25, 6, 6);

                                    o = withBlock.Worksheet.Shapes.AddShape(
                      Microsoft.Office.Core.MsoAutoShapeType.msoShapeOval, withBlock.Item[r, c].Left + 2.25, withBlock.Item[r, c].Top + 2.25, 6, 6
                      );
                                    o.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                    o.Fill.ForeColor.RGB = (int)clr;
                                    o.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                    o.Line.Weight = 1;
                                    o.Line.ForeColor.RGB = 0XFFFFFF; //white

                                    //{
                                    //    var withBlock1 = o.ShapeRange;
                                    //    {
                                    //        var withBlock2 = withBlock1.Fill;
                                    //        withBlock2.Visible = msoTrue;
                                    //        withBlock2.ForeColor.RGB = clr;
                                    //    }

                                    //    {
                                    //        var withBlock2 = withBlock1.Line;
                                    //        withBlock2.Visible = msoTrue;
                                    //        withBlock2.Weight = 1#;
                                    //withBlock2.ForeColor.RGB = vbWhite;
                                    //    }
                                    //}

                                }
                                while (!true);
                            }
                        }
                        while (!true);
                    }
                }
                if (IsReport)
                    goto ExitProc;
                // 次の開始位置への参照を取得
                if (!IsLastTable)
                {
                    r = withBlock.Row + withBlock.Rows.Count;
                    if (r <= withBlock.Worksheet.Rows.Count)
                        StartCell = StartCell.EntireColumn.Cells.Item[r, 1];
                    else
                        StartCell = null;
                }
                else
                    StartCell = null;

                if (PageSetupStartCell == null)
                    goto ExitProc;

                rs = PageSetupStartCell.Row;
                PageSetupSheet = PageSetupStartCell.Worksheet;
                r = rs;
                if (RemainedPageSetupRowsCount < MaxRowsCountPerPage)
                {
                    PageSetupHeight = withBlock.Item[-1, 1].Resize(2).Height + withBlock.Height;
                    if (PagesCount > 1)
                        PageSetupHeight = PageSetupHeight + (withBlock.Rows.Item[2].Height * 2 + TableRange.Height) * (PagesCount - 1);
                    n = Convert.ToInt64(Math.Ceiling(PageSetupHeight / DefHeight));
                    if (n > RemainedPageSetupRowsCount)
                    {
                        PageSetupSheet.Rows.Item[r].Resize(RemainedPageSetupRowsCount).Delete(XlDeleteShiftDirection.xlShiftUp);
                        RemainedPageSetupRowsCount = MaxRowsCountPerPage;
                    }
                }
                for (i = 1; i <= PagesCount; i++)
                {
                    RemainedHeight = DefHeight * RemainedPageSetupRowsCount;
                    PageRangeHeight = TableRange.Height;
                    PageRowsCount = TableRange.Rows.Count;
                    if (i == 1)
                    {
                        PageRangeHeight = PageRangeHeight + withBlock.Item[-1, 1].Resize(4).Height; // 見出し部分
                        PageRowsCount = PageRowsCount + 4;
                    }
                    else
                    {
                        PageRangeHeight = PageRangeHeight + withBlock.Item[2, 1].Height; // (前表から)の行
                        PageRowsCount = PageRowsCount + 1;
                    }
                    if (i < PagesCount)
                    {
                        PageRangeHeight = PageRangeHeight + withBlock.Item[2, 1].Height; // (後表へ)の行
                        PageRowsCount = PageRowsCount + 1;
                    }
                    n = System.Convert.ToInt64(Math.Ceiling(PageRangeHeight / DefHeight));
                    if (n > RemainedPageSetupRowsCount)
                    {
                        if (RemainedPageSetupRowsCount < MaxRowsCountPerPage)
                        {
                            PageSetupSheet.Rows.Item[r].Resize(RemainedPageSetupRowsCount).Delete(XlDeleteShiftDirection.xlShiftUp);
                            RemainedPageSetupRowsCount = MaxRowsCountPerPage;
                        }
                        x = System.Convert.ToInt64(Math.Ceiling(PageRangeHeight / (DefHeight * MaxRowsCountPerPage)));
                        if (x > 1)
                        {
                            {
                                var withBlock1 = PageSetupSheet.Rows;
                                withBlock1.Item[withBlock1.Count - (x - 1) * MaxRowsCountPerPage + 1].Resize((x - 1) * MaxRowsCountPerPage).Delete(XlDeleteShiftDirection.xlShiftUp);
                                withBlock1.Item[r + (CrossCreator.ToInt(RemainedPageSetupRowsCount % MaxRowsCountPerPage == 0) & 1)].Resize((x - 1) * MaxRowsCountPerPage).Insert(XlInsertShiftDirection.xlShiftDown);
                            }
                        }
                        RemainedPageSetupRowsCount = MaxRowsCountPerPage * x;
                    }
                    if (n > PageRowsCount)
                        PageSetupSheet.Rows.Item[r + (CrossCreator.ToInt(RemainedPageSetupRowsCount % MaxRowsCountPerPage == 0) & 1)].Resize(n - PageRowsCount).Delete(XlDeleteShiftDirection.xlShiftUp);
                    else if (n < PageRowsCount)
                    {
                        {
                            var withBlock1 = PageSetupSheet.Rows;
                            withBlock1.Item[withBlock1.Count - (PageRowsCount - n) + 1].Resize(PageRowsCount - n).Delete(XlDeleteShiftDirection.xlShiftUp);
                            withBlock1.Item[r + (CrossCreator.ToInt(RemainedPageSetupRowsCount % MaxRowsCountPerPage == 0) & 1)].Resize(PageRowsCount - n).Insert(XlInsertShiftDirection.xlShiftDown);
                        }
                    }
                    r = r + PageRowsCount;
                    RemainedPageSetupRowsCount = RemainedPageSetupRowsCount - n;
                    if (RemainedPageSetupRowsCount == 0)
                        RemainedPageSetupRowsCount = MaxRowsCountPerPage;
                }
                PageSetupStartCell = PageSetupSheet.Cells.Item[rs, 1];
                if (PagesCount == 1)
                {
                    // 折り返しなし
                    OutputUtil.CopyRow(withBlock.Item[-1, 1].Resize(2 + withBlock.Rows.Count), PageSetupStartCell);
                    {
                        var withBlock1 = PageSetupStartCell.Range["B3"];
                        withBlock1.MergeArea.UnMerge();
                        withBlock1.Resize[withBlock1.Rows.Count, MaxColumnsCountPerPage - 1].Merge();
                    }
                }
                else
                {
                    // 折り返しあり
                    {
                        var withBlock1 = WorkRange.Columns;
                        withBlock1.Item[FixedColumnIndex + 1].Resize(ColumnSize: withBlock1.Worksheet.Columns.Count - FixedColumnIndex).Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    }
                    {
                        var withBlock1 = WorkRange.Range["B3"];
                        withBlock1.MergeArea.UnMerge();
                        withBlock1.Resize[withBlock1.Rows.Count, MaxColumnsCountPerPage - 1].Merge();
                    }
                    {
                        var withBlock1 = withBlock.Item[-1, 0].Resize(2 + withBlock.Rows.Count, FixedColumnIndex);
                        tmpValue = withBlock1.Value;

                        tmpStrValue = Array.CreateInstance(typeof(object), // ReDim tmpStrValue(1 To .Rows.Count, 3 To 3)
                        new int[] { withBlock1.Rows.Count, 3 - 3 + 1 },
                        new int[] { 1, 3 });

                        for (i = 1; i <= Information.UBound(tmpStrValue, 1); i++)
                        {
                            string tmpVal = Convert.ToString(tmpValue.GetValue(i, 3));
                            tmpStrValue.SetValue(tmpVal, i, 3);
                            tmpValue.SetValue(null, i, 3);
                        }
                        {
                            var withBlock2 = WorkRange.Range["A1"].Resize[withBlock1.Rows.Count, withBlock1.Columns.Count];
                            OutputUtil.PutValue(withBlock2.Cells, ref tmpValue);
                            OutputUtil.PutValue(withBlock2.Columns.Item(3), ref tmpStrValue);
                        }
                    }
                    // WorkRange.Copy .Item(-1, 0).Resize((2 + .Rows.Count) * PagesCount).EntireRow
                    // WorkRange.Copy PageSetupStartCell.Resize((2 + .Rows.Count) * PagesCount).EntireRow
                    WorkRange.Copy(PageSetupStartCell.EntireRow);
                    {
                        var withBlock1 = WorkRange.Item[4];
                        withBlock1.Copy();
                        withBlock1.Insert(XlInsertShiftDirection.xlShiftDown);
                        xlApp.CutCopyMode = (XlCutCopyMode)1;
                    }
                    {
                        var withBlock1 = WorkRange.Cells.Item[4, MaxColumnsCountPerPage];
                        withBlock1.HorizontalAlignment = Excel.Constants.xlRight;

                        string reportKeyWord1 = LocalResource.REPORT_TO_AFTER_TABLE_MARK_AT_TURN_KEYWORD;
                        OutputUtil.PutValue(withBlock1.Cells, ref reportKeyWord1);
                    }
                    // WorkRange.Cells.Item(5, FixedColumnIndex + 1&).Value = ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportFromBeforeTableMarkAtTurnKeywordIndex)
                    string reportKeyWord = LocalResource.REPORT_FROM_BEFORE_TABLE_MARK_AT_TURN_KEYWORD;
                    OutputUtil.PutValue(WorkRange.Cells.Item[5, FixedColumnIndex + 1], ref reportKeyWord);
                    WorkRange.Item[4].Resize(WorkRange.Count - 3).Copy(PageSetupStartCell.Item[3 + withBlock.Rows.Count].Resize(withBlock.Rows.Count * (PagesCount - 1)).EntireRow);
                    {
                        var withBlock1 = TableRange;
                        tmpRange = withBlock1.Item[1, FixedColumnIndex + 1 - 1].Resize(withBlock1.Rows.Count, withBlock1.Columns.Count - FixedColumnIndex + 1);
                    }
                    for (i = 1; i <= PagesCount - 1; i++)
                    {
                        CopyWithRowHeight(tmpRange.Item[1, (i - 1) * MaxSectorsCountPerPage + 1].Resize[tmpRange.Rows.Count, MaxSectorsCountPerPage], PageSetupStartCell.Item[2 + withBlock.Rows.Count * (i - 1) + 1 + 2, FixedColumnIndex + 1]);
                        PageSetupStartCell.Item[2 + withBlock.Rows.Count * (i - 1) + 1 + 2, MaxColumnsCountPerPage].Resize(withBlock.Rows.Count - 2).Borders.Item(XlBordersIndex.xlEdgeRight).Weight = XlBorderWeight.xlThin;
                    }
                    CopyWithRowHeight(tmpRange.Item[1, (PagesCount - 1) * MaxSectorsCountPerPage + 1].Resize(tmpRange.Rows.Count, LastPageColumnsCount), PageSetupStartCell.Item[2 + withBlock.Rows.Count * (PagesCount - 1) + 1 + 2, FixedColumnIndex + 1]);
                }
                // 次の開始位置への参照を取得
                if (!IsLastTable)
                {
                    r = PageSetupStartCell.Row + 2 & +withBlock.Rows.Count * PagesCount;
                    if (r <= PageSetupSheet.Rows.Count)
                        PageSetupStartCell = PageSetupStartCell.EntireColumn.Cells.Item[r, 1];
                    else
                        PageSetupStartCell = null;
                }
                else
                    PageSetupStartCell = null;
            }

        ExitProc:
            ;
            RunningProcName = OrgProcName;
        }

        private void CopyWithRowHeight(Range Source, Range Destination)
        {
            long limRowsCnt;
            long limClmsCnt;
            //long i;
            if (Source == null)
                return;
            Source = Source.Areas.Item[1];

            if (Source.Columns.Count == Source.Worksheet.Columns.Count)
            {
                CopyRow(Source, Destination);
                return;
            }

            if ((Destination == null) || (Destination.Worksheet.ProtectContents))
            {
                return;
            }

            Destination = Destination.Areas.Item[1].Range["A1"];
            {
                var withBlock = Destination;
                limRowsCnt = withBlock.Worksheet.Rows.Count - withBlock.Row + 1;
                limClmsCnt = withBlock.Worksheet.Columns.Count - withBlock.Column + 1;
            }
            if (Source.Rows.Count > limRowsCnt)
                Source = Source.Resize[limRowsCnt];
            if (Source.Columns.Count > limClmsCnt)
                Source = Source.Resize[null, limClmsCnt];
            Source.Copy(Destination);
            //Excel.Application.CutCopyMode = (XlCutCopyMode)1;
            {
                var withBlock = Source.Rows;
                for (int i = 1; i <= withBlock.Count; i++)
                    Destination.Rows.Item[i].RowHeight = withBlock.Item[i].RowHeight;
            }
        }

        private void CopyRow(Range SourceRow, Range DestinationRow)
        {
            long c;
            long i;

            if ((SourceRow == null) || (DestinationRow == null) || (DestinationRow.Worksheet.ProtectContents))
            {
                return;
            }

            // 256列まで情報を持つシートで2003形式への行コピー時に行高さを維持するためのおまじない
            {
                var withBlock = SourceRow.Worksheet.UsedRange;
                c = withBlock.Columns[withBlock.Columns.Count].Column;
            }
            SourceRow = SourceRow.Areas.Item[1].EntireRow;
            DestinationRow = DestinationRow.Areas.Item[1].EntireRow.Item[1];

            if (c > DestinationRow.Columns.Count)
            {
                {
                    var withBlock = DestinationRow;
                    SourceRow.Resize[null, withBlock.Columns.Count].Copy(withBlock.Item[1]);
                    CopyRowHeight(SourceRow.Rows, withBlock.Rows, 0);
                }
            }
            else
                SourceRow.Copy(DestinationRow);
        }

        private void CopyRowHeight(Range SourceRow, Range DestinationRow, double OrgRowHeight)
        {
            long c;
            long x;
            Excel.Range SourceRowHighPart;
            Excel.Range SourceRowLowPart;
            Excel.Range DestinationRowHighPart;
            Excel.Range DestinationRowLowPart;
            object tmpRowHeight;
            if (OrgRowHeight == 0)
                OrgRowHeight = DestinationRow.Item[1].RowHeight;
            tmpRowHeight = SourceRow.RowHeight;


            if (tmpRowHeight == null)
            {
                c = SourceRow.Count;
                x = c / 2;
                {
                    var withBlock = SourceRow;
                    SourceRowHighPart = withBlock.Resize[x];
                    SourceRowLowPart = withBlock.Item[x + 1].Resize(c - x);
                }
                {
                    var withBlock = DestinationRow;
                    DestinationRowHighPart = withBlock.Resize[x];
                    DestinationRowLowPart = withBlock.Item[x + 1].Resize(c - x);
                }
                CopyRowHeight(SourceRowHighPart, DestinationRowHighPart, OrgRowHeight);
                CopyRowHeight(SourceRowLowPart, DestinationRowLowPart, OrgRowHeight);
            }
            else if ((double)tmpRowHeight != OrgRowHeight)
            {
                DestinationRow.RowHeight = tmpRowHeight;
            }
        }

        private void CreatePortraitGTArray(GTTable Table
        , long RowIndexMin, long RowIndexMax
        , long ColumnIndexMin, long ColumnIndexMax
        , ref Array TableStringValue, ref Array DataValue, ref Array Ranking
        , bool HasNA, bool HasIV, long NAIdx, long IVIdx
        , long AddColumnCount
        , long DataOffsetRow, long DataOffsetColumn
        , TableType TableType, bool IsReport = false
        , bool CutMedian = false, long MedIdx = -1
        , bool AddLetterColumn = false)
        {
            bool HasPreWBTotal;
            long WtColumnIdx;
            string buf = string.Empty;
            long RowsCount;
            long ColumnsCount;
            long r;
            long c;
            bool f;
            bool f2 = false;
            long x;
            long y;
            long d;
            long DataRowsCount;
            bool isMatrix;
            bool isN = false;
            long s = 0;
            long e = 0;
            bool IsSigTest;
            SignificanceTestCode SigCode = SignificanceTestCode.Off;
            object tmp;
            string OrgProcName;

            long tmpRowIndexFrom;
            long tmpRowIndexTo;
            long tmpColumnIndexFrom;
            long tmpColumnIndexTo;
            object[,] tmpTableValue;
            double[,] tmpPercentValue;
            object[,] tmpSignificanceTestCharacters;
            int[,] tmpRank;

            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.CreatePortraitGTArray";
            IsSigTest = TableType == TableType.SignificanceTest;
            if (IsSigTest)
            {
                SigCode = Table.SignificancetestCode;
                if (SigCode == SignificanceTestCode.Off)
                {
                    //Information.Err().Raise(Constants.vbObjectError + 100 &, RunningProcName
                    //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportSignificanceTestSetupIsInconsistencyMessageIndex));
                }
            }
            else
                AddLetterColumn = false;
            if (AddColumnCount <= 0)
            {
                AddColumnCount = 0;
                AddLetterColumn = false;
            }
            bool hasWeight = GetHasWeight(Table);
            WtColumnIdx = ColumnIndexMin + 2;
            // 出力表の文字列型データ配列のサイズ定義
            RowsCount = ColumnIndexMax - ColumnIndexMin + 1;
            ColumnsCount = RowIndexMax - RowIndexMin + 1 + AddColumnCount + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenSectors) & 1);
            isMatrix = (Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
            if (!HasNA && NAIdx >= ColumnIndexMin)
                RowsCount = RowsCount - 1;
            if (!HasIV && IVIdx >= ColumnIndexMin)
                RowsCount = RowsCount - 1;
            if (CutMedian)
                CutMedian = MedIdx >= RowIndexMin;
            if (CutMedian)
                RowsCount = RowsCount - 1;
            if (IsReport)
            {
                RowsCount = RowsCount + System.Convert.ToInt64(CutMedian);
            }

            d = 1;
            switch (Table.Question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N))
            {
                case QuestionType.SA:
                    {
                        if (!IsReport)
                            buf = LocalResource.REPORT_SA_DESCRIPTION_KEYWORD;
                        d = d + (CrossCreator.ToInt(IsSigTest) & 1);
                        break;
                    }

                case QuestionType.MA:
                    {
                        if (!IsReport)
                            buf = LocalResource.REPORT_MA_DESCRIPTION_KEYWORD;
                        d = d + (CrossCreator.ToInt(IsSigTest) & 1);
                        break;
                    }

                case QuestionType.N:
                    {
                        if (!IsReport)
                            buf = LocalResource.REPORT_NA_DESCRIPTION_KEYWORD;
                        isN = true;
                        break;
                    }

                default:
                    {
                        //Information.Err().Raise(Constants.vbObjectError + 100 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportUnjustQuestionTypeMessageIndex));
                        break;
                    }
            }
            if (SigCode == SignificanceTestCode.BetweenSectors && isN || SigCode == SignificanceTestCode.BetweenChildQuestions && !isMatrix)
            {
                //Information.Err().Raise(Constants.vbObjectError + 110 &, RunningProcName
                //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportSignificanceTestSetupIsInconsistencyMessageIndex));
            }
            if (isMatrix)
            {
                if (!IsReport)
                    buf = buf == LocalResource.REPORT_NA_DESCRIPTION_KEYWORD ? LocalResource.REPORT_N_MATRIX_DESCRIPTION_KEYWORD : buf + LocalResource.REPORT_MATRIX_DESCRIPTION_KEYWORD;
                if (TableType == TableType.NPer)
                    d = d + 1;
            }
            HasPreWBTotal = CurrentOutput.ShowPreWBTotal;
            if (IsSigTest)
            {
                DataRowsCount = (RowsCount - DataOffsetColumn - 1 - (CrossCreator.ToInt(HasPreWBTotal) & 1)) * d + 1 + (CrossCreator.ToInt(HasPreWBTotal) & 1);
                RowsCount = DataOffsetColumn + DataRowsCount + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenChildQuestions) & 1) + (CrossCreator.ToInt(isN) & 1);
                s = ColumnIndexMin + DataOffsetColumn + (CrossCreator.ToInt(HasPreWBTotal) & 1) + 1;
                e = ColumnIndexMax;
            }
            else
            {
                DataRowsCount = RowsCount - DataOffsetColumn;
                if (d == 2)
                {
                    DataRowsCount = DataRowsCount + Table.SectorsCount + (CrossCreator.ToInt(HasNA) & 1) + (CrossCreator.ToInt(HasIV) & 1);
                    s = ColumnIndexMin + DataOffsetColumn + (CrossCreator.ToInt(HasPreWBTotal) & 1) + 1;
                    e = s + Table.SectorsCount + (CrossCreator.ToInt(HasNA) & 1) + (CrossCreator.ToInt(HasIV) & 1) - 1;
                }
                RowsCount = DataOffsetColumn + DataRowsCount;
                ColumnsCount = ColumnsCount + (CrossCreator.ToInt(isN) & CrossCreator.ToInt(!isMatrix) & 1);
            }

            TableStringValue = Array.CreateInstance(typeof(string),
            new int[] { CrossCreator.ToInt(!IsReport) & 2, (int)ColumnsCount },
            new int[] { 1, 1 });

            // 出力表の各配列の要素を取得
            if (IsReport)
            {
                if (CurrentOutput.WBOn)
                    TableStringValue.SetValue(LocalResource.REPORT_MARKING_LEGEND_WEIGHTBACK_ON_PROMPT, 2, 1);
            }
            else
            {
                TableStringValue.SetValue(Table.Question.Name + Space(1) + Table.Question.Description, 1, 1);
                TableStringValue.SetValue(buf, 3, 2);
            }
            // 列見出し
            y = CrossCreator.ToInt(!IsReport) & 2;
            for (c = ColumnIndexMin; c <= ColumnIndexMin + DataOffsetColumn - 1; c++)
            {
                f = c != WtColumnIdx | AddColumnCount - (CrossCreator.ToInt(AddLetterColumn) & 1) == 0;
                if (f)
                {
                    y = y + 1;
                    x = DataOffsetRow + AddColumnCount + (CrossCreator.ToInt(isN) & CrossCreator.ToInt(!isMatrix) & 1) + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenSectors) & 1);
                    for (r = RowIndexMin + DataOffsetRow; r <= RowIndexMax; r++)
                    {
                        x = x + 1;
                        buf = Table.TableValue((Int32)r, (Int32)c, true);
                        if (Strings.Len(buf) > 0)
                        {
                            if (r == (Table.GetTableValueRowIndexMinimum + 1) && Table.Question.HasCount) { continue; } //#OutputFormatting
                            TableStringValue.SetValue(buf, y, x);
                        }

                        if (SigCode == SignificanceTestCode.BetweenChildQuestions)
                        {
                            if (c == ColumnIndexMin + 1)
                            {
                                buf = Table.SignificanceTestCharacters((Int32)r, (Int32)c);
                                if (Strings.Len(buf) > 0)
                                    TableStringValue.SetValue(buf, y + 1, x);
                            }
                        }
                    }
                }
            }
            // 行見出し
            y = (CrossCreator.ToInt(!IsReport) & 2) + DataOffsetColumn + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenChildQuestions) & 1);
            if (isMatrix)
            {
                if (isN)
                {
                    if (SigCode == SignificanceTestCode.BetweenChildQuestions)
                        TableStringValue.SetValue(LocalResource.REPORT_SIGNIFICANCE_TEST_ROW_COLUMN_CAPTION, Information.UBound(TableStringValue, 1), 2);
                }
            }
            for (c = ColumnIndexMin + DataOffsetColumn; c <= ColumnIndexMax; c++)
            {
                if (c == NAIdx)
                {
                    f = HasNA;
                }
                else if (c == IVIdx)
                {
                    f = HasIV;
                }
                else if (c == MedIdx)
                {
                    f = !CutMedian;
                }
                else
                {
                    f = true;
                }

                if (f)
                {
                    y = y + 1;
                    x = CrossCreator.ToInt(isN) & CrossCreator.ToInt(!isMatrix) & 1;
                    for (r = RowIndexMin; r <= RowIndexMin + DataOffsetRow - 1; r++)
                    {
                        x = x + 1;
                        buf = Table.TableValue((int)r, (int)c, true);
                        if (Strings.Len(buf) > 0)
                            TableStringValue.SetValue(buf, y, x);
                        if (SigCode == SignificanceTestCode.BetweenSectors)
                        {
                            if (x == 2)
                            {
                                buf = Table.SignificanceTestCharacters((int)r, (int)c);
                                if (Strings.Len(buf) > 0)
                                    TableStringValue.SetValue(buf, y, DataOffsetRow + AddColumnCount + 1);
                            }
                        }
                    }

                    switch (c)
                    {
                        case long g when (s <= c && c <= e):
                            {
                                y = y + d - 1;
                                break;
                            }
                    }
                }
            }

            //ReDim DataValue((Not IsReport And 2 &) +DataOffsetColumn + (SigCode = SignificanceTestCode_BetweenChildQuestions And 1 &) +1 & To UBound(TableStringValue, 1 &) _
            //          , 1 & +DataOffsetRow - 1 & +AddColumnCount + (SigCode = SignificanceTestCode_BetweenSectors And 1 &) +1 & +(isN And Not isMatrix And 1 &) To UBound(TableStringValue, 2 &))

            int arr1Obj1 = Information.UBound(TableStringValue, 1);
            int arr1Obj2 = Information.UBound(TableStringValue, 2);
            int arr2Obj1 = (int)((CrossCreator.ToInt(!IsReport) & 2) + DataOffsetColumn + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenChildQuestions) & 1) + 1);
            int arr2Obj2 = (int)(1 + DataOffsetRow - 1 + AddColumnCount + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenSectors) & 1) + 1 + (CrossCreator.ToInt(isN) & CrossCreator.ToInt(!isMatrix) & 1));

            DataValue = Array.CreateInstance(typeof(object),
            new int[] { arr1Obj1 - arr2Obj1 + 1, arr1Obj2 - arr2Obj2 + 1 },
            new int[] { arr2Obj1, arr2Obj2 });


            arr1Obj1 = Information.UBound(DataValue, 1);
            arr1Obj2 = Information.UBound(DataValue, 2);
            arr2Obj1 = Information.LBound(DataValue, 1);
            arr2Obj2 = Information.LBound(DataValue, 2);

            Ranking = Array.CreateInstance(typeof(object),
            new int[] { arr1Obj1 - arr2Obj1 + 1, arr1Obj2 - arr2Obj2 + 1 },
            new int[] { arr2Obj1, arr2Obj2 });

            // 性能対策 start
            tmpRowIndexFrom = RowIndexMin + DataOffsetRow;
            tmpRowIndexTo = RowIndexMax;
            tmpColumnIndexFrom = ColumnIndexMin + DataOffsetColumn;
            tmpColumnIndexTo = ColumnIndexMax;
            tmpTableValue = Table.TableValueByMatrix((int)tmpRowIndexFrom, (int)tmpRowIndexTo, (int)tmpColumnIndexFrom, (int)tmpColumnIndexTo);
            tmpPercentValue = Table.PercentValueByMatrix((int)tmpRowIndexFrom, (int)tmpRowIndexTo, (int)tmpColumnIndexFrom, (int)tmpColumnIndexTo);
            tmpSignificanceTestCharacters = Table.SignificanceTestCharactersByMatrix((int)tmpRowIndexFrom, (int)tmpRowIndexTo, (int)tmpColumnIndexFrom, (int)tmpColumnIndexTo);
            tmpRank = Table.RankByMatrix((int)tmpRowIndexFrom, (int)tmpRowIndexTo, (int)tmpColumnIndexFrom, (int)tmpColumnIndexTo);
            // 性能対策 end
            // データ
            y = (CrossCreator.ToInt(!IsReport) & 2) + DataOffsetColumn + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenChildQuestions) & 1);
            for (c = ColumnIndexMin + DataOffsetColumn; c <= ColumnIndexMax; c++)
            {
                if (c == NAIdx)
                {
                    f = HasNA;
                }
                else if (c == IVIdx)
                {
                    f = HasIV;
                }
                else if (c == MedIdx)
                {
                    f = !CutMedian;
                }
                else
                {
                    f = true;
                }

                if (f)
                {
                    y = y + 1;
                    if (TableType != TableType.N)
                    {
                        f2 = false;
                        f = HasPreWBTotal & c == ColumnIndexMin + DataOffsetColumn; // WB前全体
                        if (!f)
                            f = c == ColumnIndexMin + DataOffsetColumn + (CrossCreator.ToInt(HasPreWBTotal) & 1);// 全体
                        if (!f)
                        {
                            if (hasWeight)
                            {
                                f = c >= ColumnIndexMax - 1;    // 統計量母数か加重平均
                                f2 = c == ColumnIndexMax; // 加重平均
                            }
                        }
                    }
                    x = DataOffsetRow + AddColumnCount + (CrossCreator.ToInt(isN) & CrossCreator.ToInt(!isMatrix) & 1) + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenSectors) & 1);
                    for (r = RowIndexMin + DataOffsetRow; r <= RowIndexMax; r++)
                    {
                        x = x + 1;
                        switch (TableType)
                        {
                            case TableType.NPer // 必ずSA/MAマトリクス
                           :
                                {
                                    tmp = tmpTableValue[r, c];
                                    if (Information.IsNumeric(tmp))
                                        tmp = Convert.ToDouble(tmp);
                                    DataValue.SetValue(tmp, y, x);
                                    if (!f)
                                        DataValue.SetValue(tmpPercentValue[r, c], y + 1, x);
                                    break;
                                }

                            case TableType.N    // 数値回答集計時はここ
                     :
                                {
                                    tmp = tmpTableValue[r, c];
                                    if (Information.IsNumeric(tmp))
                                        tmp = Convert.ToDouble(tmp);
                                    DataValue.SetValue(tmp, y, x);
                                    break;
                                }

                            case TableType.Per  // 必ずSA/MAマトリクス
                     :
                                {
                                    if (f)
                                    {
                                        tmp = tmpTableValue[r, c];
                                        if (Information.IsNumeric(buf))
                                            tmp = Convert.ToDouble(tmp);
                                        DataValue.SetValue(tmp, y, x);
                                    }
                                    else
                                        DataValue.SetValue(tmpPercentValue[r, c], y, x);
                                    break;
                                }

                            case TableType.SignificanceTest:
                                {
                                    if (isN)
                                    {
                                        tmp = tmpTableValue[r, c];
                                        if (Information.IsNumeric(tmp))
                                            tmp = Convert.ToDouble(tmp);
                                        DataValue.SetValue(tmp, y, x);
                                        if (SigCode == SignificanceTestCode.BetweenChildQuestions)
                                        {
                                            if (c == AVERAGE_COLUMN_INDEX - (CrossCreator.ToInt(!HasPreWBTotal) & 1))
                                            {
                                                tmp = tmpSignificanceTestCharacters[r, c];
                                                if (Strings.Len(tmp) > 0)
                                                    DataValue.SetValue(tmp, Information.UBound(DataValue, 1), x);
                                            }
                                        }
                                    }
                                    else if (f)
                                    {
                                        tmp = tmpTableValue[r, c];
                                        if (Information.IsNumeric(buf))
                                            tmp = Convert.ToDouble(tmp);
                                        DataValue.SetValue(tmp, y, x);
                                        if (f2)
                                        {
                                            tmp = tmpSignificanceTestCharacters[r, c];
                                            if (Strings.Len(tmp) > 0)
                                                DataValue.SetValue(tmp, y + 1, x);
                                        }
                                    }
                                    else
                                    {
                                        DataValue.SetValue(tmpPercentValue[r, c], y, x);
                                        tmp = tmpSignificanceTestCharacters[r, c];
                                        if (Strings.Len(tmp) > 0)
                                            DataValue.SetValue(tmp, y + 1, x);
                                    }
                                    break;
                                }
                        }
                        Ranking.SetValue(tmpRank[r, c], y, x);
                    }
                    switch (c)
                    {
                        case long g when (s <= c && c <= e):
                            {
                                y = y + d - 1;
                                break;
                            }
                    }
                }
            }
            RunningProcName = OrgProcName;
        }

        private void CreateLandscapeGTArray(GTTable Table
        , long RowIndexMin, long RowIndexMax
        , long ColumnIndexMin, long ColumnIndexMax
        , ref Array TableStringValue, ref Array DataValue, ref Array Ranking, ref Array OptionNumbers, ref Array OptionNumbersTop
        , bool HasNA, bool HasIV, long NAIdx, long IVIdx
        , long AddColumnCount
        , long DataOffsetRow, long DataOffsetColumn
        , TableType TableType, bool IsReport = false, bool CutMedian = false, long MedIdx = -1
        , bool AddLetterColumn = false)
        {
            bool HasPreWBTotal;
            long WtColumnIdx;
            string buf = string.Empty;
            long RowsCount;
            long ColumnsCount;
            long r;
            long c;
            bool f;
            bool f2;
            long x;
            long y;
            long d;
            long DataRowsCount;
            object tmp;
            bool IsSigTest;
            SignificanceTestCode SigCode = SignificanceTestCode.Off;
            bool isN = false;
            bool isMatrix = false;
            string OrgProcName;

            long tmpRowIndexFrom;
            long tmpRowIndexTo;
            long tmpColumnIndexFrom;
            long tmpColumnIndexTo;
            object[,] tmpTableValue;
            double[,] tmpPercentValue;
            object[,] tmpSignificanceTestCharacters;
            int[,] tmpRank;

            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.CreateLandscapeGTArray";
            IsSigTest = TableType == TableType.SignificanceTest;
            if (IsSigTest)
            {
                SigCode = Table.SignificancetestCode;
                if (SigCode == SignificanceTestCode.Off)
                {
                    //Information.Err().Raise(Constants.vbObjectError + 100 &, RunningProcName
                    //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportSignificanceTestSetupIsInconsistencyMessageIndex));
                }
            }
            else
                AddLetterColumn = false;
            if (AddColumnCount <= 0)
            {
                AddColumnCount = 0;
                AddLetterColumn = false;
            }
            bool hasWeight = GetHasWeight(Table);
            WtColumnIdx = ColumnIndexMin + 2;
            // 出力表の文字列型データ配列のサイズ定義
            RowsCount = RowIndexMax - RowIndexMin + 1;
            ColumnsCount = ColumnIndexMax - ColumnIndexMin + 1 + AddColumnCount + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenChildQuestions) & 1);
            if (!HasNA & NAIdx >= ColumnIndexMin)
                ColumnsCount = ColumnsCount - 1;
            if (!HasIV & IVIdx >= ColumnIndexMin)
                ColumnsCount = ColumnsCount - 1;
            if (CutMedian)
                CutMedian = MedIdx >= ColumnIndexMin;
            if (CutMedian)
                ColumnsCount = ColumnsCount - 1;
            if (IsReport)
            {
                ColumnsCount = ColumnsCount + System.Convert.ToInt64(CutMedian);
            }
            d = 1;
            switch (Table.Question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N))
            {
                case QuestionType.SA:
                    {
                        if (!IsReport)
                            buf = LocalResource.REPORT_SA_DESCRIPTION_KEYWORD;
                        d = d + (CrossCreator.ToInt(IsSigTest) & 1);
                        break;
                    }

                case QuestionType.MA:
                    {
                        if (!IsReport)
                            buf = LocalResource.REPORT_MA_DESCRIPTION_KEYWORD;
                        d = d + (CrossCreator.ToInt(IsSigTest) & 1);
                        break;
                    }

                case QuestionType.N:
                    {
                        if (!IsReport)
                            buf = LocalResource.REPORT_NA_DESCRIPTION_KEYWORD;
                        if (SigCode == SignificanceTestCode.BetweenChildQuestions)
                            ColumnsCount = ColumnsCount + 1;
                        isN = true;
                        break;
                    }

                default:
                    {
                        //Information.Err().Raise(Constants.vbObjectError + 100 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportUnjustQuestionTypeMessageIndex));
                        break;
                    }
            }
            if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                if (!IsReport)
                    buf = buf == LocalResource.REPORT_NA_DESCRIPTION_KEYWORD ? LocalResource.REPORT_N_MATRIX_DESCRIPTION_KEYWORD : buf + LocalResource.REPORT_MATRIX_DESCRIPTION_KEYWORD;
                if (TableType == TableType.NPer)
                    d = d + 1;
                isMatrix = true;
            }
            if (SigCode == SignificanceTestCode.BetweenSectors && isN || SigCode == SignificanceTestCode.BetweenChildQuestions && !isMatrix)
            {
                //Information.Err().Raise(Constants.vbObjectError + 110 &, RunningProcName
                //       , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportSignificanceTestSetupIsInconsistencyMessageIndex));
            }

            DataRowsCount = (RowsCount - DataOffsetRow) * d;
            RowsCount = DataOffsetRow + DataRowsCount + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenSectors) & 1);

            // ReDim TableStringValue(1& To (Not IsReport And 2&) + RowsCount, 1& To ColumnsCount)
            int array1Obj1 = (int)((CrossCreator.ToInt(!IsReport) & 2) + RowsCount);
            int array1Obj2 = (int)ColumnsCount;
            int array2Obj1 = 1;
            int array2Obj2 = 1;

            TableStringValue = Array.CreateInstance(typeof(string),
            new int[] { array1Obj1 - array2Obj1 + 1, array1Obj2 - array2Obj2 + 1 },
            new int[] { array2Obj1, array2Obj2 });

            array1Obj1 = Information.UBound(TableStringValue, 1);
            array1Obj2 = Information.UBound(TableStringValue, 2);
            array2Obj1 = (int)((CrossCreator.ToInt(!IsReport) & 2) + DataOffsetRow + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenSectors) & 1) + 1);
            array2Obj2 = (int)(1 + DataOffsetColumn - 1 + AddColumnCount + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenChildQuestions) & 1) + 1);

            DataValue = Array.CreateInstance(typeof(object),
            new int[] { array1Obj1 - array2Obj1 + 1, array1Obj2 - array2Obj2 + 1 },
            new int[] { array2Obj1, array2Obj2 });

            OptionNumbers = Array.CreateInstance(typeof(object),
            new int[] { array1Obj1 - array2Obj1 + 1, 1 }, //length
            new int[] { array2Obj1, 1 });

            OptionNumbersTop = Array.CreateInstance(typeof(object),
            new int[] { 1, array1Obj2 - array2Obj2 + 1 }, //length
            new int[] { 3, array2Obj2 });

            // 出力表の各配列の要素を取得
            if (IsReport)
            {
                if (CurrentOutput.WBOn)
                    TableStringValue.SetValue(LocalResource.REPORT_MARKING_LEGEND_WEIGHTBACK_ON_PROMPT, DataOffsetRow, 1);
            }
            else
            {
                TableStringValue.SetValue(Table.Question.Name + Space(1) + Table.Question.Description, 1, 1);
                TableStringValue.SetValue(buf, 3, 2);
            }
            // 列見出し
            y = CrossCreator.ToInt(!IsReport) & 2;
            if (isMatrix)
            {
                if (isN)
                {
                    if (SigCode == SignificanceTestCode.BetweenChildQuestions)
                        TableStringValue.SetValue(LocalResource.REPORT_SIGNIFICANCE_TEST_ROW_COLUMN_CAPTION, y + 2, Information.UBound(TableStringValue, 2));
                }
            }
            for (r = RowIndexMin; r <= RowIndexMin + DataOffsetRow - 1; r++)
            {
                y = y + 1;
                x = DataOffsetColumn + AddColumnCount + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenChildQuestions) & 1);
                for (c = ColumnIndexMin + DataOffsetColumn; c <= ColumnIndexMax; c++)
                {
                    if (c == NAIdx)
                        f = HasNA;
                    else if (c == IVIdx)
                        f = HasIV;
                    else if (c == MedIdx)
                        f = !CutMedian;
                    else
                        f = true;

                    if (f)
                    {
                        x = x + 1;
                        buf = Table.TableValue((int)r, (int)c, true);
                        if (Strings.Len(buf) > 0)
                        {
                            if (r == (Table.GetTableValueRowIndexMinimum + 2) && Table.Question.HasCount) { continue; } //#OutputFormatting
                            TableStringValue.SetValue(buf, y, x);
                            if (y == 3)
                            {
                                OptionNumbersTop.SetValue(buf, y, x);
                            }
                        }
                        if (SigCode == SignificanceTestCode.BetweenSectors)
                        {
                            if (r == RowIndexMin + 1)
                            {
                                buf = Table.SignificanceTestCharacters((int)r, (int)c);
                                if (Strings.Len(buf) > 0)
                                    TableStringValue.SetValue(buf, (CrossCreator.ToInt(!IsReport) & 2) + DataOffsetRow + 1, x);
                            }
                        }
                    }
                }
            }
            // 行見出し
            y = (CrossCreator.ToInt(!IsReport) & 2) + DataOffsetRow + 1 + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenSectors) & 1) - d;
            for (r = RowIndexMin + DataOffsetRow; r <= RowIndexMax; r++)
            {
                y = y + d;
                x = 0;
                for (c = ColumnIndexMin; c <= ColumnIndexMin + DataOffsetColumn - 1; c++)
                {
                    f = c != WtColumnIdx | AddColumnCount - (CrossCreator.ToInt(AddLetterColumn) & 1) == 0;
                    if (f)
                    {
                        x = x + 1;
                        buf = Table.TableValue((int)r, (int)c, true);
                        if (Strings.Len(buf) > 0)
                        {
                            //if (r == (Table.GetTableValueRowIndexMinimum + 1) && Table.Question.HasCount) { continue; } //#OutputFormatting
                            TableStringValue.SetValue(buf, y, x);
                            if (x == 1)
                            {
                                OptionNumbers.SetValue(buf, y, 1);
                            }
                        }
                        if (SigCode == SignificanceTestCode.BetweenChildQuestions)
                        {
                            if (c == ColumnIndexMin + 1)
                            {
                                buf = Table.SignificanceTestCharacters((int)r, (int)c);
                                if (Strings.Len(buf) > 0)
                                    TableStringValue.SetValue(buf, y, DataOffsetColumn + AddColumnCount + 1);
                            }
                        }
                    }
                }
            }

            //ReDim DataValue((Not IsReport And 2 &) +DataOffsetRow + (SigCode = SignificanceTestCode_BetweenSectors And 1 &) +1 & To UBound(TableStringValue, 1 &) _
            //          , 1 & +DataOffsetColumn - 1 & +AddColumnCount + (SigCode = SignificanceTestCode_BetweenChildQuestions And 1 &) +1 & To UBound(TableStringValue, 2 &))


            // ReDim Ranking(LBound(DataValue, 1) To UBound(DataValue, 1), LBound(DataValue, 2) To UBound(DataValue, 2))

            array1Obj1 = Information.UBound(DataValue, 1);
            array1Obj2 = Information.UBound(DataValue, 2);
            array2Obj1 = Information.LBound(DataValue, 1);
            array2Obj2 = Information.LBound(DataValue, 2);

            Ranking = Array.CreateInstance(typeof(object),
            new int[] { array1Obj1 - array2Obj1 + 1, array1Obj2 - array2Obj2 + 1 },
            new int[] { array2Obj1, array2Obj2 });

            // 性能対策 start
            tmpRowIndexFrom = RowIndexMin + DataOffsetRow;
            tmpRowIndexTo = RowIndexMax;
            tmpColumnIndexFrom = ColumnIndexMin + DataOffsetColumn;
            tmpColumnIndexTo = ColumnIndexMax;
            tmpTableValue = Table.TableValueByMatrix((int)tmpRowIndexFrom, (int)tmpRowIndexTo, (int)tmpColumnIndexFrom, (int)tmpColumnIndexTo);
            tmpPercentValue = Table.PercentValueByMatrix((int)tmpRowIndexFrom, (int)tmpRowIndexTo, (int)tmpColumnIndexFrom, (int)tmpColumnIndexTo);
            tmpSignificanceTestCharacters = Table.SignificanceTestCharactersByMatrix((int)tmpRowIndexFrom, (int)tmpRowIndexTo, (int)tmpColumnIndexFrom, (int)tmpColumnIndexTo);
            tmpRank = Table.RankByMatrix((int)tmpRowIndexFrom, (int)tmpRowIndexTo, (int)tmpColumnIndexFrom, (int)tmpColumnIndexTo);
            // 性能対策 end
            // データ
            HasPreWBTotal = CurrentOutput.ShowPreWBTotal;
            y = (CrossCreator.ToInt(!IsReport) & 2) + DataOffsetRow + 1 + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenSectors) & 1) - d;
            for (r = RowIndexMin + DataOffsetRow; r <= RowIndexMax; r++)
            {
                y = y + d;
                x = DataOffsetColumn + AddColumnCount + (CrossCreator.ToInt(SigCode == SignificanceTestCode.BetweenChildQuestions) & 1);
                for (c = ColumnIndexMin + DataOffsetColumn; c <= ColumnIndexMax; c++)
                {
                    if (c == NAIdx)
                        f = HasNA;
                    else if (c == IVIdx)
                        f = HasIV;
                    else if (c == MedIdx)
                        f = !CutMedian;
                    else
                        f = true;

                    if (f)
                    {
                        x = x + 1;
                        switch (TableType)
                        {
                            case TableType.NPer // 必ずSA/MAマトリクス
                           :
                                {
                                    f2 = false;
                                    f = HasPreWBTotal && c == ColumnIndexMin + DataOffsetColumn; // WB前全体
                                    if (!f)
                                        f = c == ColumnIndexMin + DataOffsetColumn + (CrossCreator.ToInt(HasPreWBTotal) & 1);// 全体
                                    if (!f)
                                    {
                                        if (hasWeight)
                                        {
                                            f = c >= ColumnIndexMax - 1;    // 統計量母数か加重平均
                                            f2 = c == ColumnIndexMax; // 加重平均
                                        }
                                    }
                                    tmp = tmpTableValue[r, c];
                                    if (Information.IsNumeric(tmp))
                                        tmp = Convert.ToDouble(tmp);
                                    DataValue.SetValue(tmp, y + (CrossCreator.ToInt(f2) & 1), x);
                                    if (!f)
                                        // DataValue(y + 1&, x) = Table.PercentValue(r, c)
                                        DataValue.SetValue(tmpPercentValue[r, c], y + 1, x);
                                    break;
                                }

                            case TableType.N    // 数値回答集計時はここ
                     :
                                {
                                    tmp = tmpTableValue[r, c];
                                    if (Information.IsNumeric(tmp))
                                        tmp = Convert.ToDouble(tmp);
                                    DataValue.SetValue(tmp, y, x);
                                    break;
                                }

                            case TableType.Per  // 必ずSA/MAマトリクス
                     :
                                {
                                    f = HasPreWBTotal & c == ColumnIndexMin + DataOffsetColumn; // WB前全体
                                    if (!f)
                                        f = c == ColumnIndexMin + DataOffsetColumn + (CrossCreator.ToInt(HasPreWBTotal) & 1);// 全体
                                    if (!f)
                                    {
                                        if (hasWeight)
                                            f = c >= ColumnIndexMax - 1;// 統計量母数か加重平均
                                    }
                                    if (f)
                                    {
                                        tmp = tmpTableValue[r, c];
                                        if (Information.IsNumeric(tmp))
                                            tmp = Convert.ToDouble(tmp);
                                        DataValue.SetValue(tmp, y, x);
                                    }
                                    else
                                        DataValue.SetValue(tmpPercentValue[r, c], y, x);
                                    break;
                                }

                            case TableType.SignificanceTest:
                                {
                                    if (isN)
                                    {
                                        tmp = tmpTableValue[r, c];
                                        if (Information.IsNumeric(tmp))
                                            tmp = Convert.ToDouble(tmp);
                                        DataValue.SetValue(tmp, y, x);
                                        if (SigCode == SignificanceTestCode.BetweenChildQuestions)
                                        {
                                            if (c == AVERAGE_COLUMN_INDEX - (CrossCreator.ToInt(!HasPreWBTotal) & 1))
                                            {
                                                tmp = tmpSignificanceTestCharacters[r, c];
                                                if (Strings.Len(tmp) > 0)
                                                    DataValue.SetValue(tmp, y, ColumnsCount);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        f = HasPreWBTotal && c == ColumnIndexMin + DataOffsetColumn; // WB前全体
                                        f2 = false;
                                        if (!f)
                                            f = c == ColumnIndexMin + DataOffsetColumn + (CrossCreator.ToInt(HasPreWBTotal) & 1);// 全体
                                        if (!f)
                                        {
                                            if (hasWeight)
                                            {
                                                f = c >= ColumnIndexMax - 1;    // 統計量母数か加重平均
                                                f2 = c == ColumnIndexMax;
                                            }
                                        }
                                        if (f)
                                        {
                                            tmp = tmpTableValue[r, c];
                                            if (Information.IsNumeric(tmp))
                                                tmp = Convert.ToDouble(tmp);
                                            DataValue.SetValue(tmp, y, x);
                                            if (f2)
                                            {
                                                tmp = tmpSignificanceTestCharacters[r, c];
                                                if (Strings.Len(tmp) > 0)
                                                    DataValue.SetValue(tmp, y + 1, x);
                                            }
                                        }
                                        else
                                        {
                                            DataValue.SetValue(tmpPercentValue[r, c], y, x);
                                            tmp = tmpSignificanceTestCharacters[r, c];
                                            if (Strings.Len(tmp) > 0)
                                                DataValue.SetValue(tmp, y + 1, x);
                                        }
                                    }
                                    break;
                                }
                        }
                        Ranking.SetValue(tmpRank[r, c], y, x);
                    }
                }
            }
            RunningProcName = OrgProcName;
        }

        private bool SetRatSourceRange(GTTable Table, Range TableRange
        , ref Collection GraphSourceRangeCol
        , ref Collection GraphTableRangeCol
        , ref Collection nCol)
        {
            bool SetRatSourceRange = false;
            Excel.Range tmpGraphSourceRange;
            if (TableRange == null)
                return false;
            if ((CurrentOutput.Orientation == TableOrientation.Portrait && AverageRowIndex > 2) || (CurrentOutput.Orientation == TableOrientation.Landscape && AverageColumnIndex > 3))
            {
                GraphSourceRangeCol = new VBA.Collection();
                GraphTableRangeCol = new VBA.Collection();
                nCol = new VBA.Collection();
                if (CurrentOutput.Orientation == TableOrientation.Portrait)
                {
                    {
                        var withBlock = TableRange.Columns;
                        {
                            var withBlock1 = withBlock.Item[withBlock.Count - Table.ChildQuestionsCount + 1].Resize(ColumnSize: Table.ChildQuestionsCount).Rows;
                            tmpGraphSourceRange = xlApp.Union(withBlock1.Item[2], withBlock1.Item[AverageRowIndex]);
                            nCol.Add(withBlock1.Cells.Item(TotalRowIndex, 1).Value.ToString("0"));
                        }
                    }
                }
                else
                {
                    var withBlock = TableRange.Rows.Item[3].Resize(Table.ChildQuestionsCount).Columns;
                    tmpGraphSourceRange = xlApp.Union(withBlock.Item[2], withBlock.Item[AverageColumnIndex]);
                    nCol.Add(withBlock.Cells.Item(1, TotalColumnIndex).Value.ToString("0"));
                }
                GraphSourceRangeCol.Add(tmpGraphSourceRange);
                GraphTableRangeCol.Add(TableRange);
                SetRatSourceRange = true;
            }
            return SetRatSourceRange;
        }

        private Excel.ChartObject CreateChartObject(GTTable Table
        , Excel.ChartObjects ChartObjs
        , string ChartCaption, Excel.XlChartType ChartType
        , Excel.Range SourceRange, Excel.Worksheet GraphSheet
        , ref string[] NamesArray
        , ref string ErrorDesc
        , XlRowCol plotby = XlRowCol.xlRows, bool HasLegend = false
        , string SubTitle = ""
        , long num = -1
        , long GapWidth = 50
        , bool NoCategoryLabel = false
        , double ScaleNumber = 100
        , string ValueNumberFormat = "0\"%\""
        , bool IsReport = false
        , bool SetXValues = false
        )
        {
            // Const MAX_CHART_OBJECT_HEIGHT As Double = 450#
            Excel.ChartObject tmpChartObject;
            Excel.SeriesCollection chtSeriesCol;
            Excel.Points chtPoints = null;
            Excel.Axis chtAxis;
            Excel.ChartGroups chtGrps;
            // Dim TitleBottom As Double
            double t;
            double l;
            double d;
            double h;
            bool reverseOn = false;
            bool HasLeaderLines = false;
            bool HasSeriesLines = false;
            MsoGradientStyle GradStyle;
            //long i;
            Array v = null;
            bool HideZero = false;
            string OrgProcName;
            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.CreateChartObject";
            GradStyle = Table.Chart.GradientStyle;
            ChartObject CreateChartObject = null;
            switch (ChartType)
            {
                case Excel.XlChartType.xlPie:
                    {
                        //#OutputFormatting - Removing SubTotal From Graph
                        if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                        {
                            var withBlock2 = SourceRange.Areas;
                            Range tmpGraphSourceRange = withBlock2[1].Resize[ColumnSize: withBlock2[1].Columns.Count - Table.Question.SubTotalCnt];
                            for (int j = 2; j <= withBlock2.Count; j++)
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[j].Resize[ColumnSize: withBlock2[2].Columns.Count - Table.Question.SubTotalCnt]);

                            SourceRange = tmpGraphSourceRange;
                        }
                        else
                        {
                            var withBlock2 = SourceRange.Areas;
                            Range tmpGraphSourceRange = withBlock2[1].Resize[RowSize: withBlock2[1].Rows.Count - Table.Question.SubTotalCnt];
                            for (int j = 2; j <= withBlock2.Count; j++)
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[j].Resize[RowSize: withBlock2[2].Rows.Count - Table.Question.SubTotalCnt]);
                            //SourceRange = tmpGraphSourceRange.Columns.Item[2];
                            SourceRange = tmpGraphSourceRange;
                        }

                        HasLeaderLines = (Table.Chart.ChartType & XlChartType.RAT) == 0;
                        break;
                    }

                case Excel.XlChartType.xlColumnClustered:
                    {
                        break;
                    }

                case Excel.XlChartType.xlBarClustered:
                    {
                        reverseOn = true;
                        break;
                    }

                case Excel.XlChartType.xlColumnStacked:
                    {
                        //#OutputFormatting - Removing SubTotal From Graph
                        if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                        {
                            var withBlock2 = SourceRange.Areas;
                            Range tmpGraphSourceRange = withBlock2[1];
                            if (withBlock2.Count > 2)
                            {
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2]);
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[3].Resize[ColumnSize: withBlock2[3].Columns.Count - Table.Question.SubTotalCnt]);
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[4].Resize[ColumnSize: withBlock2[4].Columns.Count - Table.Question.SubTotalCnt]);
                            }
                            else
                            {
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2].Resize[ColumnSize: withBlock2[2].Columns.Count - Table.Question.SubTotalCnt]);
                            }
                            SourceRange = tmpGraphSourceRange;
                        }
                        else
                        {
                            var withBlock2 = SourceRange.Areas;
                            Range tmpGraphSourceRange = withBlock2[1].Resize[RowSize: withBlock2[1].Rows.Count - Table.Question.SubTotalCnt];
                            for (int j = 2; j <= withBlock2.Count; j++)
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[j].Resize[RowSize: withBlock2[2].Rows.Count - Table.Question.SubTotalCnt]);
                            SourceRange = tmpGraphSourceRange;
                        }
                        HasSeriesLines = true;
                        HideZero = true;
                        break;
                    }

                case Excel.XlChartType.xlBarStacked:
                    {
                        //#OutputFormatting - Removing SubTotal From Graph
                        if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                        {
                            var withBlock2 = SourceRange.Areas;
                            Range tmpGraphSourceRange = withBlock2[1];
                            if (withBlock2.Count > 2)
                            {
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2]);
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[3].Resize[ColumnSize: withBlock2[3].Columns.Count - Table.Question.SubTotalCnt]);
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[4].Resize[ColumnSize: withBlock2[4].Columns.Count - Table.Question.SubTotalCnt]);
                            }
                            else
                            {
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2].Resize[ColumnSize: withBlock2[2].Columns.Count - Table.Question.SubTotalCnt]);
                            }
                            SourceRange = tmpGraphSourceRange;
                        }
                        else
                        {
                            var withBlock2 = SourceRange.Areas;
                            Range tmpGraphSourceRange = withBlock2[1].Resize[RowSize: withBlock2[1].Rows.Count - Table.Question.SubTotalCnt];
                            for (int j = 2; j <= withBlock2.Count; j++)
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[j].Resize[RowSize: withBlock2[2].Rows.Count - Table.Question.SubTotalCnt]);
                            SourceRange = tmpGraphSourceRange;
                        }

                        reverseOn = true;
                        HasSeriesLines = true;
                        HideZero = true;
                        break;
                    }

                case Excel.XlChartType.xlColumnStacked100:
                    {
                        //#OutputFormatting - Removing SubTotal From Graph
                        if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                        {
                            var withBlock2 = SourceRange.Areas;
                            Range tmpGraphSourceRange = withBlock2[1];
                            if (withBlock2.Count > 2)
                            {
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2]);
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[3].Resize[ColumnSize: withBlock2[3].Columns.Count - Table.Question.SubTotalCnt]);
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[4].Resize[ColumnSize: withBlock2[4].Columns.Count - Table.Question.SubTotalCnt]);
                            }
                            else
                            {
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2].Resize[ColumnSize: withBlock2[2].Columns.Count - Table.Question.SubTotalCnt]);
                            }
                            SourceRange = tmpGraphSourceRange;
                        }
                        else
                        {
                            var withBlock2 = SourceRange.Areas;
                            Range tmpGraphSourceRange = withBlock2[1].Resize[RowSize: withBlock2[1].Rows.Count - Table.Question.SubTotalCnt];
                            for (int j = 2; j <= withBlock2.Count; j++)
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[j].Resize[RowSize: withBlock2[2].Rows.Count - Table.Question.SubTotalCnt]);
                            SourceRange = tmpGraphSourceRange;
                        }

                        HasSeriesLines = (Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
                        HideZero = true;
                        break;
                    }

                case Excel.XlChartType.xlBarStacked100:
                    {
                        //#OutputFormatting - Removing SubTotal From Graph
                        if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                        {
                            var withBlock2 = SourceRange.Areas;
                            Range tmpGraphSourceRange = withBlock2[1];
                            if (withBlock2.Count > 2)
                            {
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2]);
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[3].Resize[ColumnSize: withBlock2[3].Columns.Count - Table.Question.SubTotalCnt]);
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[4].Resize[ColumnSize: withBlock2[4].Columns.Count - Table.Question.SubTotalCnt]);
                            }
                            else
                            {
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[2].Resize[ColumnSize: withBlock2[2].Columns.Count - Table.Question.SubTotalCnt]);
                            }
                            SourceRange = tmpGraphSourceRange;
                        }
                        else
                        {
                            var withBlock2 = SourceRange.Areas;
                            Range tmpGraphSourceRange = withBlock2[1].Resize[RowSize: withBlock2[1].Rows.Count - Table.Question.SubTotalCnt];
                            for (int j = 2; j <= withBlock2.Count; j++)
                                tmpGraphSourceRange = xlApp.Union(tmpGraphSourceRange, withBlock2.Item[j].Resize[RowSize: withBlock2[2].Rows.Count - Table.Question.SubTotalCnt]);
                            SourceRange = tmpGraphSourceRange;
                        }

                        reverseOn = true;
                        HasSeriesLines = (Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
                        HideZero = true;
                        break;
                    }

                case Excel.XlChartType.xlLine:
                    {
                        GradStyle = 0;
                        break;
                    }

                default:
                    {
                        goto ExitProc;
                        //break;
                    }
            }
            {
                var withBlock = GraphSheet.Range["B1"];
                if (IsReport)
                {
                    withBlock.Worksheet.UsedRange.Clear();
                    tmpChartObject = ChartObjs.Add(withBlock.Left, withBlock.Top, 480, 480 * DEFAULT_HEIGHT_WIDTH_RATIO);
                }
                else
                    tmpChartObject = ChartObjs.Add(withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Width * DEFAULT_HEIGHT_WIDTH_RATIO);
            }
            {
                var withBlock = tmpChartObject.Chart;
                withBlock.ChartType = ChartType;
                //On Error Resume Next
                withBlock.SetSourceData(SourceRange, plotby);

                if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    if (SourceRange.Areas.Count < 3 && checkEmptyChoices(SourceRange.Areas.Item[1], XlSearchOrder.xlByRows))
                    {
                        if (ChartType == Excel.XlChartType.xlPie || ChartType == Excel.XlChartType.xlColumnClustered || ChartType == Excel.XlChartType.xlBarClustered)
                        {
                            Excel.Series s1 = withBlock.SeriesCollection(1);
                            s1.XValues = SourceRange.Areas.Item[1];
                            s1.Values = SourceRange.Areas.Item[2];
                        }
                    }
                }
                else
                {
                    bool hasEmptyChoice = false;
                    if (SourceRange.Areas.Count > 1)
                    {
                        hasEmptyChoice = checkEmptyChoices(SourceRange.Areas.Item[1], XlSearchOrder.xlByRows);
                    }
                    else
                    {
                        hasEmptyChoice = checkEmptyChoices(SourceRange.Columns.Item[1], XlSearchOrder.xlByRows);
                    }
                    if ((ChartType == Excel.XlChartType.xlBarStacked100 || ChartType == Excel.XlChartType.xlColumnStacked100) && hasEmptyChoice)
                    {
                        int sCnt = withBlock.SeriesCollection().Count;
                        for (int j = 1; j <= SourceRange.Rows.Count; j++)
                        {
                            string name;
                            Range sVal;

                            if (SourceRange.Areas.Count > 1)
                            {
                                Range val = SourceRange.Areas.Item[1];
                                Range val2 = SourceRange.Areas.Item[2];
                                name = val.Rows.Item[j].Value2;
                                sVal = val2.Rows.Item[j];
                            }
                            else
                            {
                                Range val = SourceRange.Rows.Item[j];
                                name = val.Columns.Item[1].Value2;
                                sVal = val.Columns.Item[2];
                            }
                            Excel.Series s1;
                            if (j > sCnt)
                            {
                                s1 = withBlock.SeriesCollection().NewSeries();
                            }
                            else
                            {
                                s1 = withBlock.SeriesCollection(j);
                            }
                            s1.Name = name;
                            s1.Values = sVal;
                        }
                    }
                    else if (hasEmptyChoice)
                    {
                        Excel.Series s1 = withBlock.SeriesCollection(1);
                        if (SourceRange.Areas.Count > 1)
                        {
                            s1.XValues = SourceRange.Areas.Item[1];
                            s1.Values = SourceRange.Areas.Item[2];
                        }
                        else
                        {
                            s1.XValues = SourceRange.Columns.Item[1];
                            s1.Values = SourceRange.Columns.Item[2];
                        }
                    }
                }

                if (Information.Err().Number != 0)
                {
                    ErrorDesc = Information.Err().Description;
                    tmpChartObject.Delete();
                    return CreateChartObject;
                }
                //On Error GoTo 0
                {
                    var withBlock1 = withBlock.ChartArea;
                    if (IsReport)
                    {
                        withBlock1.Border.LineStyle = XlLineStyle.xlLineStyleNone;
                        withBlock1.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;
                    }
                    else
                    {
                        // .Format.Line.ForeColor.RGB = vbBlack
                        //withBlock1.Format.Line.ForeColor.RGB = Color.Black.ToArgb(); //LINE_COLOR
                        withBlock1.Format.Line.ForeColor.RGB = Util.Constants.GT.GTGraphBorder.ToArgb(); //LINE_COLOR
                        withBlock1.Format.Line.Weight = (float)XlBorderWeight.xlHairline;
                    }
                    withBlock1.AutoScaleFont = false;
                    {
                        var withBlock2 = withBlock1.Font;
                        //On Error Resume Next
                        //withBlock2.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                        withBlock2.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                        //On Error GoTo 0
                        withBlock2.Size = ChartType == Excel.XlChartType.xlPie ? 10 : 8;
                        withBlock2.Bold = Microsoft.Office.Core.MsoTriState.msoFalse;
                    }
                }
                if (IsReport)
                    withBlock.HasTitle = false;
                else
                    SetChartTitle(tmpChartObject.Chart, ChartCaption, SubTitle, num);
                withBlock.HasLegend = HasLegend;
                if (HasLegend)
                {
                    {
                        var withBlock1 = withBlock.Legend;
                        withBlock1.Position = XlLegendPosition.xlLegendPositionTop;

                        //withBlock1.Border.LineStyle = XlLineStyle.xlContinuous;
                        //withBlock1.Border.Color = Color.Black;//LINE_COLOR
                        //withBlock1.Border.Color = Util.Constants.GT.GTGraphBorder;
                        //withBlock1.Border.Weight = XlBorderWeight.xlHairline;

                        withBlock1.Border.LineStyle = XlLineStyle.xlLineStyleNone;

                        if (IsReport)
                            withBlock1.Interior.Color = Color.White;//vbWhite;
                    }
                }
                if (ChartType == Excel.XlChartType.xlPie)
                    withBlock.ApplyDataLabels(HasLeaderLines: HasLeaderLines, ShowCategoryName: true, ShowValue: true);
                else
                    withBlock.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowValue);
                {
                    var withBlock1 = withBlock.PlotArea;
                    if (ChartType == Excel.XlChartType.xlPie)
                        withBlock1.Border.LineStyle = XlLineStyle.xlLineStyleNone;
                    else
                    {
                        withBlock1.Border.LineStyle = XlLineStyle.xlContinuous;
                        //withBlock1.Border.Color = Color.Black;//LINE_COLOR
                        withBlock1.Border.Color = Util.Constants.GT.GTGraphBorder;
                        withBlock1.Border.Weight = XlBorderWeight.xlHairline;
                    }
                    if (IsReport)
                        withBlock1.Interior.Color = Color.White;//vbWhite;
                    else
                        withBlock1.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;
                }
                chtSeriesCol = withBlock.SeriesCollection();
                {
                    var withBlock1 = chtSeriesCol;
                    int clusterChertColor = Convert.ToInt32(Util.Constants.GTGraphColorIndex.WIDTH_STICK_M);
                    if (ChartType == Excel.XlChartType.xlPie)
                    {
                        {
                            var withBlock2 = withBlock1.Item(1);
                            //withBlock2.Format.Line.ForeColor.RGB = Color.Black.ToArgb();//LINE_COLOR
                            withBlock2.Format.Line.ForeColor.RGB = Util.Constants.GT.GTGraphBorder.ToArgb();//LINE_COLOR
                            withBlock2.Format.Line.Weight = (float)XlBorderWeight.xlHairline;
                            {
                                var withBlock3 = withBlock2.DataLabels();
                                withBlock3.NumberFormat = "0.0" + ((Table.Chart.ChartType & XlChartType.RAT) == XlChartType.RAT ? "" : "\"%\""); // TO DO .NumberFormat = "0.0" & IIf((Table.Chart.ChartType And XlChartType_RAT), "", """%""")
                                withBlock3.Separator = Space(1);
                            }
                            chtPoints = withBlock2.Points();
                            //if (Information.UBound(NamesArray) == chtPoints.Count - 1) //#OutputFormatting
                            //    withBlock2.XValues = NamesArray;
                        }
                    }
                    else
                        for (int i = 1; i <= withBlock1.Count; i++)
                        {
                            {
                                var withBlock2 = withBlock1.Item(i);
                                if (SetXValues)
                                {
                                    chtPoints = withBlock2.Points();
                                    if (Information.UBound(NamesArray) == chtPoints.Count - 1)
                                        withBlock2.XValues = NamesArray;
                                }
                                else if (Information.UBound(NamesArray) == chtSeriesCol.Count - 1)
                                    withBlock2.Name = NamesArray[i - 1];
                                if (ChartType == Excel.XlChartType.xlLine)
                                    //withBlock2.Border.ColorIndex = Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount);
                                    withBlock2.Border.Color = ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)];
                                else
                                {
                                    //withBlock2.Format.Line.ForeColor.RGB = Color.Black.ToArgb();//LINE_COLOR;
                                    withBlock2.Format.Line.ForeColor.RGB = Util.Constants.GT.GTGraphBorder.ToArgb();//LINE_COLOR;
                                    withBlock2.Format.Line.Weight = (float)XlBorderWeight.xlHairline;

                                    //24 to do
                                    if (Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount) == clusterChertColor &&
                                       (ChartType == Excel.XlChartType.xlBarClustered || ChartType == Excel.XlChartType.xlColumnClustered) &&
                                       Table.Question.SubTotalCnt > 0
                                        )
                                    {
                                        withBlock2.Interior.Color = 0xF2F2F2;
                                        Points points = withBlock2.Points();
                                        for (int idx = points.Count - Table.Question.SubTotalCnt + 1; idx <= points.Count; idx++)
                                        {
                                            Excel.Point point = withBlock2.Points(idx);
                                            point.Interior.Color = 0xECDFE4;
                                        }
                                    }
                                    else
                                        withBlock2.Interior.Color = ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)];
                                }
                                if (GradStyle != 0)
                                    withBlock2.Fill.OneColorGradient((Microsoft.Office.Core.MsoGradientStyle)GradStyle, Table.Chart.GradientVariant, (float)0.9);
                                if (HideZero)
                                    SetHideZeroNumberFormat(chtSeriesCol.Item(i));
                            }
                        }
                }
                if (ChartType == Excel.XlChartType.xlPie)
                {
                    Series xlSeries = withBlock.SeriesCollection(1);
                    v = (xlSeries.Values as object) as Array;

                    //v = withBlock.SeriesCollection(1).Values;
                    {
                        var withBlock1 = chtPoints;
                        for (int i = 1; i <= withBlock1.Count; i++)
                        {
                            {
                                var withBlock2 = withBlock1.Item(i);
                                bool hasDataLabel = withBlock2.HasDataLabel;
                                dynamic vVal = v.GetValue(i);
                                if (hasDataLabel && (Convert.ToDouble(vVal) <= Table.HideChartDescriptionMaxPercent))
                                    withBlock2.DataLabel.Delete();

                                //withBlock2.Interior.ColorIndex = Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount);
                                //int itemIndex = Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount);
                                //int color = ColorPallet.colorIndex[itemIndex];
                                //withBlock2.Interior.Color = ColorPallet.colorIndex[itemIndex];
                                //withBlock2.Interior.Color = Color.FromArgb(color);

                                withBlock2.Interior.Color = ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)];

                                if (Table.Chart.GradientStyle != 0)
                                    withBlock2.Fill.OneColorGradient((Microsoft.Office.Core.MsoGradientStyle)(Table.Chart.GradientStyle), Table.Chart.GradientVariant, (float)0.9);
                            }
                        }
                        AdjustOverlap(tmpChartObject);
                    }
                    CreateChartObject = tmpChartObject;
                    goto ExitProc;
                }
                chtAxis = withBlock.Axes(XlAxisType.xlCategory);
                {
                    var withBlock1 = chtAxis;
                    withBlock1.ReversePlotOrder = reverseOn;
                    if (NoCategoryLabel)
                        withBlock1.TickLabelPosition = XlTickLabelPosition.xlTickLabelPositionNone;
                    withBlock1.MajorTickMark = XlTickMark.xlTickMarkInside;
                    //withBlock1.Format.Line.ForeColor.RGB = Color.Black.ToArgb();//LINE_COLOR
                    withBlock1.Format.Line.ForeColor.RGB = Util.Constants.GT.GTGraphBorder.ToArgb();//LINE_COLOR
                    withBlock1.Format.Line.Weight = (float)XlBorderWeight.xlHairline;
                }
                chtAxis = withBlock.Axes(XlAxisType.xlValue);
                {
                    var withBlock1 = chtAxis;
                    switch (ValueNumberFormat)
                    {
                        case "0%":
                        case "0\"%\"":
                            {
                                withBlock1.MaximumScale = 1 * ScaleNumber;
                                withBlock1.MinimumScale = 0;
                                withBlock1.MajorUnit = 0.2 * ScaleNumber;
                                break;
                            }
                    }
                    withBlock1.MajorGridlines.Border.LineStyle = XlLineStyle.xlDot;
                    withBlock1.MajorGridlines.Border.Color = Util.Constants.GT.GTGraphBorder.ToArgb();
                    withBlock1.TickLabels.NumberFormat = ValueNumberFormat;
                    withBlock1.MajorTickMark = XlTickMark.xlTickMarkInside;
                    // .Format.Line.ForeColor.RGB = vbBlack
                    //withBlock1.Format.Line.ForeColor.RGB = Color.Black.ToArgb();/*LINE_COLOR*/
                    withBlock1.Format.Line.ForeColor.RGB = Util.Constants.GT.GTGraphBorder.ToArgb();/*LINE_COLOR*/
                    withBlock1.Format.Line.Weight = (float)XlBorderWeight.xlHairline;
                }
                if (ChartType != Excel.XlChartType.xlLine)
                {
                    chtGrps = withBlock.ChartGroups();
                    {
                        var withBlock1 = chtGrps.Item(1);
                        withBlock1.GapWidth = (int)GapWidth;
                        if (HasSeriesLines)
                        {
                            withBlock1.HasSeriesLines = true;
                            withBlock1.SeriesLines.Border.LineStyle = Microsoft.Office.Core.XlConstants.xlGray50;
                        }
                    }
                }

                if (ChartType == Excel.XlChartType.xlBarStacked100 || ChartType == Excel.XlChartType.xlColumnStacked100) //#OutputFormatting 
                {
                    chtAxis = withBlock.Axes(XlAxisType.xlCategory);
                    chtAxis.HasTitle = true;
                    //chtAxis.AxisTitle.Text = Table.Question.Description;
                    chtAxis.AxisTitle.Text = Table.Question.TableHeading; // Table heading is used as graph description in the case of GT
                    chtAxis.HasMajorGridlines = false;
                    chtAxis.HasMinorGridlines = false;
                }

                if (ChartType == Excel.XlChartType.xlColumnClustered)
                {
                    chtAxis = withBlock.Axes(XlAxisType.xlCategory);
                    chtAxis.TickLabels.Orientation = XlTickLabelOrientation.xlTickLabelOrientationVertical;
                }


                withBlock.PlotArea.Border.LineStyle = XlLineStyle.xlLineStyleNone; //#OutputFormatting

            }
            CreateChartObject = tmpChartObject;
        ExitProc:;
            RunningProcName = OrgProcName;
            return CreateChartObject;
        }

        public static bool checkEmptyChoices(Excel.Range range, Excel.XlSearchOrder sOrdr)
        {
            Range find = range.Find("", Type.Missing,
                                Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole,
                                sOrdr, Excel.XlSearchDirection.xlNext, false,
                                Type.Missing, Type.Missing);
            return find != null;
        }


        private void SetChartTitle(Excel.Chart Chart, string Title, string SubTitle, long num)
        {
            const int MAX_TITLE_LENGTH = 255;
            const int MAX_PART_LENGTH = 50;
            const string LEADER = "...";
            string numBuf = null;
            int requiredLength;
            int usableLength;
            int d;
            int r;
            int tl;
            int sl;
            string OrgProcName;
            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.SetChartTitle";
            if (num >= 0)
                numBuf = Constants.vbLf + "(n=" + System.Convert.ToString(num) + ")";
            tl = Strings.Len(Title);
            sl = Strings.Len(SubTitle);
            requiredLength = Strings.Len(numBuf) + (CrossCreator.ToInt(sl > 0) & 2);
            usableLength = MAX_TITLE_LENGTH - requiredLength;
            d = tl + sl - usableLength;
            if (d > 0)
            {
                if (tl > MAX_PART_LENGTH & sl > MAX_PART_LENGTH)
                {
                    r = d * sl / (tl + sl);
                    if (r > 0)
                        sl = (int)(Interaction.IIf(sl - r <= Strings.Len(LEADER), 1, sl - r)) + Strings.Len(LEADER);
                    if (tl > usableLength - sl)
                    {
                        tl = (int)(Interaction.IIf(usableLength - sl <= Strings.Len(LEADER), 1, usableLength - sl)) + Strings.Len(LEADER);
                        Title = Strings.Left(Title, (int)tl - Strings.Len(LEADER)) + LEADER;
                    }
                    sl = usableLength - tl;
                    if (sl != Strings.Len(SubTitle))
                        SubTitle = Strings.Left(SubTitle, (int)sl - Strings.Len(LEADER)) + LEADER;
                }
                else if (Strings.Len(Title) > MAX_PART_LENGTH)
                    Title = Strings.Left(Title, Strings.Len(Title) - (int)d - Strings.Len(LEADER)) + LEADER;
                else
                    SubTitle = Strings.Left(SubTitle, Strings.Len(SubTitle) - (int)d - Strings.Len(LEADER)) + LEADER;
            }

            if (Strings.Len(SubTitle) > 0)
                SubTitle = "[" + SubTitle + "]";
            {
                var withBlock = Chart;
                withBlock.HasTitle = true;
                {
                    var withBlock1 = withBlock.ChartTitle;
                    withBlock1.HorizontalAlignment = Excel.Constants.xlLeft;
                    withBlock1.Text = Title + SubTitle + numBuf;
                    withBlock1.Characters.Font.Size = 10;
                    withBlock1.Left = withBlock1.Top;
                }
            }
            RunningProcName = OrgProcName;
        }

        private void SetHideZeroNumberFormat(Excel.Series Series)
        {
            Excel.DataLabels dls;
            string fmt = string.Empty;
            string[] buf = null;
            //Excel.Point p;
            Excel.DataLabel dl;
            if (Series == null)
                return;
            dls = Series.DataLabels();
            fmt = dls.NumberFormat;
            // ;がエスケープされているようなケースはないものとする
            if (fmt == ";;;")
            {
                foreach (var p in Series.Points())
                {
                    dl = p.DataLabel;
                    fmt = dl.NumberFormat;
                    AdjustFormat(ref buf, ref fmt);//GoSub AdjustFormat
                    dl.NumberFormat = fmt;
                }
            }
            else
            {
                AdjustFormat(ref buf, ref fmt);//GoSub AdjustFormat
                dls.NumberFormat = fmt;
            }
        }

        private void AdjustFormat(ref string[] buf, ref string fmt)
        {
            buf = Strings.Split(fmt, ";", 2);
            var oldBuf = buf;
            buf = new string[3];
            if (oldBuf != null)
                Array.Copy(oldBuf, buf, Math.Min(3, oldBuf.Length));
            buf[1] = Constants.vbNullString;
            fmt = Strings.Join(buf, ";");
        }

        private void AdjustOverlap(Excel.ChartObject chtObj)
        {
            const double MAX_CHART_OBJECT_HEIGHT = 450;
            const double VERTICAL_MARGIN = 3.75;     // 5px
            const double HORIZONTAL_MARGIN = 3.75;   // 5px
            double TitleBottom = 0;
            double o;
            double tmpHeight;
            double h;
            Excel.SeriesCollection ss;
            Excel.Points ps;
            VBA.Collection dLbls;
            Excel.DataLabel lbl1;
            Excel.DataLabel lbl2;
            double t;
            double l;
            double d;
            //long i;
            //long j;
            bool existIntersect;
            double t1;
            double t2;
            double b1;
            double b2;
            double l1;
            double l2;
            double r1;
            double r2;
            string OrgProcName;
            OrgProcName = RunningProcName;
            RunningProcName = "GlobalMember.AdjustOverlap";
            chtObj.Parent.Activate();
            {
                var withBlock = chtObj.Chart;
                withBlock.ChartTitle.Top=2;
                if (withBlock.HasTitle)
                    TitleBottom = withBlock.ChartTitle.Top + withBlock.ChartTitle.Height;
                if (withBlock.HasLegend)
                {
                    if (withBlock.Legend.Position == XlLegendPosition.xlLegendPositionTop)
                    {
                        t = withBlock.Legend.Top + withBlock.Legend.Height;
                        if (t > TitleBottom)
                            TitleBottom = t;
                    }
                }
                if (withBlock.ChartType == Excel.XlChartType.xlPie)
                {
                    ss = withBlock.SeriesCollection();
                    ps = ss.Item(1).Points();
                    dLbls = new Collection();
                    {
                        var withBlock1 = ps;
                        for (int i = 1; i <= withBlock1.Count; i++)
                        {
                            if (withBlock1.Item(i).HasDataLabel)
                                dLbls.Add(withBlock1.Item(i).DataLabel);
                        }
                    }

                    xlApp.ScreenUpdating = true;
                    xlApp.ScreenUpdating = false;
                    t = withBlock.PlotArea.Top;
                    {
                        var withBlock1 = dLbls;
                        existIntersect = false;
                        for (int i = 1; i <= withBlock1.Count; i++)
                        {
                            lbl1 = (DataLabel)withBlock1[i];
                            if (lbl1.Top < t)
                                t = lbl1.Top;
                            if (!existIntersect)
                            {
                                {
                                    var withBlock2 = lbl1;
                                    t1 = withBlock2.Top + VERTICAL_MARGIN;
                                    b1 = withBlock2.Top + withBlock2.Height - VERTICAL_MARGIN;
                                    l1 = withBlock2.Left + HORIZONTAL_MARGIN;
                                    r1 = withBlock2.Left + withBlock2.Width - HORIZONTAL_MARGIN;
                                }

                                for (int j = i + 1; j <= withBlock1.Count; j++)
                                {
                                    lbl2 = (DataLabel)withBlock1[j];
                                    {
                                        var withBlock2 = lbl2;
                                        t2 = withBlock2.Top + VERTICAL_MARGIN;
                                        b2 = withBlock2.Top + withBlock2.Height - VERTICAL_MARGIN;
                                        l2 = withBlock2.Left + HORIZONTAL_MARGIN;
                                        r2 = withBlock2.Left + withBlock2.Width - HORIZONTAL_MARGIN;
                                    }

                                    if (((t1 - t2) * (b1 - t2) < 0) || ((t1 - b2) * (b1 - b2) < 0) || ((t2 - t1) * (b2 - t1) < 0) || ((t2 - b1) * (b2 - b1) < 0))
                                    {
                                        if (((l1 - l2) * (r1 - l2) < 0) || ((l1 - r2) * (r1 - r2) < 0) || ((l2 - l1) * (r2 - l1) < 0) || ((l2 - r1) * (r2 - r1) < 0))
                                        {
                                            existIntersect = true;
                                        }

                                    }
                                }
                            }
                        }
                    }

                    if (existIntersect)
                    {
                        if (t >= TitleBottom)
                            t = TitleBottom - 10;
                    }

                    //if (t >= TitleBottom)
                    //break;
                    do
                    {
                        {
                            var withBlock1 = withBlock.PlotArea;
                            //d = TitleBottom - t;
                            d = 10;
                            h = withBlock1.Height - d;
                            if (h >= chtObj.Height * 0.55)
                            {
                                t = withBlock1.Top + d / 2;
                                l = withBlock1.Left + d / 2;
                                withBlock1.Height = h;
                                withBlock1.Width = h;
                                withBlock1.Left = l;
                                withBlock1.Top = t;
                            }
                            else
                            {
                                h = withBlock1.Height;
                                l = withBlock1.Left;
                                tmpHeight = chtObj.Height + d;
                                if (tmpHeight > MAX_CHART_OBJECT_HEIGHT)
                                    tmpHeight = MAX_CHART_OBJECT_HEIGHT;
                                chtObj.Height = tmpHeight;
                                withBlock1.Height = h;
                                withBlock1.Left = l;
                                //withBlock1.Top = tmpHeight - withBlock1.Height;
                                break;
                            }
                        }
                    }
                    while (true)// データラベル間の被りがあったら、強制的に調整する
        ;
                }
                else
                {
                    var withBlock1 = withBlock.PlotArea;
                    if (withBlock1.Top >= TitleBottom)
                        goto ExitProc;
                    xlApp.ScreenUpdating = true;
                    xlApp.ScreenUpdating = false;
                    o = TitleBottom - withBlock1.Top;
                    h = withBlock1.Height;
                    tmpHeight = chtObj.Height + o;
                    switch (chtObj.Chart.ChartType)
                    {
                        case Excel.XlChartType.xlBarClustered:
                        case Excel.XlChartType.xlBarStacked:
                        case Excel.XlChartType.xlBarStacked100:
                            {
                                break;
                            }

                        default:
                            {
                                if (tmpHeight > MAX_CHART_OBJECT_HEIGHT)
                                    tmpHeight = MAX_CHART_OBJECT_HEIGHT;
                                break;
                            }
                    }
                    chtObj.Height = tmpHeight;
                    withBlock1.Height = h;
                    withBlock1.Top = tmpHeight - withBlock1.Height;
                }
            }

        ExitProc:
            ;
            RunningProcName = OrgProcName;
        }

        private void FitChartHeightToRangeWidth(Excel.ChartObject chtObj, Excel.Range rng)
        {
            string OrgProcName;
            OrgProcName = RunningProcName;
            RunningProcName = "GlobalMember.FitChartHeightToRangeWidth";
            FitChartHeight(chtObj, rng.Columns.Count * 35);
            RunningProcName = OrgProcName;
        }

        private void FitChartHeight(Excel.ChartObject chtObj, double h)
        {
            double t;
            double o; // Offset
            double d; // Distance
            string OrgProcName;
            OrgProcName = RunningProcName;
            RunningProcName = "GlobalMember.FitChartHeight";
            // 高さを揃える
            chtObj.Parent.Activate();
            {
                var withBlock = chtObj.Chart;
                {
                    var withBlock1 = withBlock.PlotArea;
                    xlApp.ScreenUpdating = true;
                    xlApp.ScreenUpdating = false;
                    // .InsideHeight = hにする
                    t = withBlock1.Top;
                    o = withBlock1.Height - withBlock1.InsideHeight;
                    d = h + o - withBlock1.InsideHeight;
                }
                withBlock.Parent.Height = withBlock.Parent.Height + d;
                {
                    var withBlock1 = withBlock.PlotArea;
                    xlApp.ScreenUpdating = true;
                    xlApp.ScreenUpdating = false;
                    withBlock1.Top = t;
                    o = withBlock1.Height - withBlock1.InsideHeight;
                    withBlock1.Height = h + o;
                }
                AdjustOverlap(chtObj);
            }
            RunningProcName = OrgProcName;
        }

        private void FitChartHeightToRangeHeight(Excel.ChartObject chtObj, Excel.Range rng)
        {
            string OrgProcName;
            OrgProcName = RunningProcName;
            RunningProcName = "GlobalMember.FitChartHeightToRangeHeight";
            FitChartHeight(chtObj, rng.Rows.Count * 35);
            RunningProcName = OrgProcName;
        }

        private double RoundUp(double value)
        {
            double result = 0;
            try
            {
                result = Math.Ceiling(value);
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        private void PutContents(Worksheet ContentsSheet
    , ref Array ContentsValue, Array HyperlinkTargetCells, Worksheet workingSheet)
        {
            GTTable tmpTable;
            string buf;
            Excel.GroupObject g;
            Excel.TextBox b;
            double r;
            double d;
            Excel.Shape tmpS;
            Excel.Shape[] s = null;
            long cnt = 0;
            long i;
            long j;
            Array v;
            double h;
            double delH = 0;
            long u;
            string OrgProcName;
            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.PutContents";
            {
                var withBlock = ContentsSheet;
                withBlock.Unprotect(SheetPSWD);
                withBlock.Rectangles("TitleBox").Text = CurrentOutput.ParentRequest.Title;
                tmpTable = (GTTable)CurrentOutput.Tables[0];
                if (tmpTable.KeyItem == null)
                {
                    {
                        var withBlock1 = withBlock.Names.Item("KeyItemInformation");
                        {
                            var withBlock2 = withBlock1.RefersToRange.EntireRow;
                            delH = withBlock2.Height;
                            withBlock2.Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                        withBlock1.Delete();
                    }
                }
                else
                {
                    v = new string[2, 2];
                    v.SetValue(LocalResource.REPORT_CLASSIFICATION_ITEM_KEYWORD, 0, 0);
                    v.SetValue(LocalResource.REPORT_SECTOR_KEYWORD, 1, 0);
                    {
                        var withBlock1 = tmpTable.KeyItem;
                        v.SetValue(withBlock1.Name + ":" + withBlock1.Description, 0, 1);
                        v.SetValue(withBlock1.SectorNumber + ":" + withBlock1.SectorDescription, 1, 1);
                    }
                    {
                        var withBlock1 = withBlock.Range["KeyItemInformation"].Range["B1:C2"];
                        // .Value = v
                        OutputUtil.PutValue(withBlock1.Cells, ref v);
                        OutputUtil.AutoFitEx(withBlock1.Rows, xlApp, workingSheet, Util.Constants.ExcelRowMaxHeight);
                    }
                }
                buf = CurrentOutput.LocalizedFilteringExpression;
                if (Strings.Len(buf) == 0)
                {
                    {
                        var withBlock1 = withBlock.Names.Item("Criteria");
                        withBlock1.RefersToRange.Clear();
                        withBlock1.Delete();
                    }
                }
                else
                {
                    v = new string[2];
                    v.SetValue(LocalResource.REPORT_FILTER_CRITERION_KEYWORD, 0);
                    v.SetValue(buf, 1);
                    {
                        var withBlock1 = withBlock.Range["Criteria"];
                        // .Value = v
                        OutputUtil.PutValue(withBlock1.Cells, ref v);
                        {
                            var withBlock2 = withBlock1.EntireRow;
                            h = withBlock2.RowHeight;
                            withBlock2.AutoFit();
                            if (withBlock2.RowHeight < h)
                                withBlock2.RowHeight = h;
                        }
                    }
                }
                if (CurrentOutput.WBOn)
                {
                    string reportKeyWord = LocalResource.REPORT_MARKING_LEGEND_WEIGHTBACK_ON_PROMPT;
                    OutputUtil.PutValue(withBlock.Range["WBPrompt"], ref reportKeyWord);
                }
                {
                    //if (CurrentOutput.MarkingRanking || CurrentOutput.MarkingColoring || CurrentOutput.MarkingSignificance || CurrentOutput.MarkingAscending || CurrentOutput.SignificanceTest)
                    //{
                    //    if (CurrentOutput.MinSamplesCountOnMarking > 0)
                    //    {
                    //        string reportKeyWord = LocalResource.ReportMarkingLegendMinBasePromptIndex);
                    //        OutputUtil.PutValue(withBlock.Range["MinBase"], ref reportKeyWord);
                    //    }
                    //}
                    if (CurrentOutput.MarkingRanking || CurrentOutput.MarkingColoring || CurrentOutput.MarkingSignificance || CurrentOutput.MarkingAscending || CurrentOutput.SignificanceTest)
                    {
                        if (CurrentOutput.MinSamplesCountOnMarking > 0)
                        {
                            string wbString = "";
                            if (CurrentOutput.WBOn)
                            {
                                wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_AFTER_WB;
                            }
                            if (CurrentOutput.WBOn && CurrentOutput.ShowPreWBTotal && CurrentOutput.PreWbBase)
                            {
                                wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_BEFORE_WB;
                            }
                            string msg = string.Format(LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_PROMPT,
                                wbString, CurrentOutput.MinSamplesCountOnMarking.ToString());
                            OutputUtil.PutValue(ContentsSheet.Range["MinBase"], ref msg);
                        }
                    }
                }
                g = withBlock.GroupObjects("RankingMarkingLegend");
                b = withBlock.TextBoxes("SignificanceTestLegend");
                r = g.Left + g.Width;
                d = b.Left - r;
                r = b.Left + b.Width;
                {
                    var withBlock1 = withBlock.Shapes;
                    tmpS = withBlock1.Item("SignificanceTestLegend");
                    if (CurrentOutput.SignificanceTest)
                    {
                        u = -1;
                        v = Strings.Split("");
                        if (CurrentOutput.SignificanceTestOne)
                        {
                            if (u < 1)
                            {
                                u = u + 1;
                                var oldV = v;
                                v = new string[u + 1];
                                if (oldV != null)
                                    Array.Copy(oldV, v, Math.Min(u + 1, oldV.Length));
                                v.SetValue(Strings.StrConv(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_AT_1CAPTION, (VbStrConv)((int)Constants.vbLowerCase & CrossCreator.ToInt(u == 1))), u);
                            }
                        }
                        if (CurrentOutput.SignificanceTestFive)
                        {
                            if (u < 1)
                            {
                                u = u + 1;
                                var oldV = v;
                                v = new string[u + 1];
                                if (oldV != null)
                                    Array.Copy(oldV, v, Math.Min(u + 1, oldV.Length));
                                v.SetValue(Strings.StrConv(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_AT_5CAPTION, (VbStrConv)((int)Constants.vbLowerCase & CrossCreator.ToInt(u == 1))), u);
                            }
                        }
                        if (CurrentOutput.SignificanceTestTen)
                        {
                            if (u < 1)
                            {
                                u = u + 1;
                                var oldV = v;
                                v = new string[u + 1];
                                if (oldV != null)
                                    Array.Copy(oldV, v, Math.Min(u + 1, oldV.Length));
                                v.SetValue(Strings.StrConv(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_AT_10CAPTION, (VbStrConv)((int)Constants.vbLowerCase & CrossCreator.ToInt(u == 1))), u);
                            }
                        }
                        tmpS.DrawingObject.Text = System.Text.RegularExpressions.Regex.Unescape(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_CAPTION) + Constants.vbLf + Strings.Join((object[])v, Constants.vbLf);
                        var oldS = s;
                        s = new Excel.Shape[cnt + 1];
                        if (oldS != null)
                            Array.Copy(oldS, s, Math.Min(cnt + 1, oldS.Length));
                        s[cnt] = tmpS;
                        cnt = cnt + 1;
                    }
                    else
                        tmpS.Delete();
                    tmpS = withBlock1.Item("RankingMarkingLegend");

                    if (CurrentOutput.MarkingRanking)
                    {
                        tmpS.TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_CAPTION;
                        withBlock1.Item("Rank1Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_1ST_CAPTION;
                        withBlock1.Item("Rank2Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_2ND_CAPTION;
                        withBlock1.Item("Rank3Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_3RD_CAPTION;
                        var oldS = s;
                        s = new Excel.Shape[cnt + 1];
                        if (oldS != null)
                            Array.Copy(oldS, s, Math.Min(cnt + 1, oldS.Length));
                        s[cnt] = tmpS;
                        cnt = cnt + 1;
                    }
                    else
                        tmpS.Delete();
                    tmpS = null;
                    for (i = 0; i <= cnt - 1; i++)
                    {
                        s[i].Left = (float)r - s[i].Width;
                        s[i].Top = s[i].Top - (float)delH;
                        r = s[i].Left - d;
                    }
                }
                {
                    var withBlock1 = withBlock.Range["Contents"];
                    if (Information.UBound(ContentsValue, 1) > withBlock1.Rows.Count)
                    {
                        withBlock1.EntireRow.Item[2].Copy();
                        withBlock1.EntireRow.Item[2].Resize(Information.UBound(ContentsValue, 1) - withBlock1.Rows.Count).Insert(XlInsertShiftDirection.xlShiftDown);
                    }
                    else if (Information.UBound(ContentsValue, 1) < withBlock1.Rows.Count)
                        withBlock1.EntireRow.Resize[withBlock1.Rows.Count - Information.UBound(ContentsValue, 1)].Delete(XlDeleteShiftDirection.xlShiftUp);
                }
                v = Array.CreateInstance(typeof(object),
                new int[] { Information.UBound(ContentsValue, 2) },
                new int[] { 1 });
                v.SetValue(LocalResource.REPORT_LAYOUT_QUESTION_NUMBER_COLUMN_CAPTION, 1);
                v.SetValue(LocalResource.REPORT_LAYOUT_QC3_DESCRIPTION_2COLUMN_CAPTION, 2);
                v.SetValue(LocalResource.REPORT_NP_TABLE_KEYWORD, 3);
                v.SetValue(LocalResource.REPORT_N_TABLE_KEYWORD, 4);
                v.SetValue(LocalResource.REPORT_P_TABLE_KEYWORD, 5);
                v.SetValue(LocalResource.REPORT_GRAPH_KEYWORD, 6);

                OutputUtil.PutValue(withBlock.Range["ContentsCaption"], ref v);
                {
                    var withBlock1 = withBlock.Range["Contents"];
                    OutputUtil.PutValue(withBlock1.Resize[withBlock1.Rows.Count, Information.LBound(HyperlinkTargetCells, 2) - 1], ref ContentsValue);
                    for (i = 1; i <= Information.UBound(ContentsValue, 1); i++)
                    {
                        for (j = Information.LBound(HyperlinkTargetCells, 2); j <= Information.UBound(HyperlinkTargetCells, 2); j++)
                        {
                            if (!(HyperlinkTargetCells.GetValue(i, j) == null))
                            {
                                Range tempRange = (Range)HyperlinkTargetCells.GetValue(i, j);
                                //withBlock1.Worksheet.Hyperlinks.Add(withBlock1.Item[i, j], "", "'" + tempRange.Worksheet.Name + "'!" + tempRange.Address, null, ContentsValue.GetValue(i, j));

                                string tmp = "'" + tempRange.Worksheet.Name + "'!" + tempRange.Address;
                                object contentValue = ContentsValue.GetValue(i, j);
                                withBlock1.Worksheet.Hyperlinks.Add(withBlock1.Item[i, j], "", tmp, "", contentValue);
                            }
                        }
                    }
                    withBlock1.EntireRow.AutoFit();
                }
            }
            RunningProcName = OrgProcName;
        }

        private void SaveBook(Workbook Book, Workbook FormatBook, XlFileFormat FileFormat, string outPutFileName)
        {
            try { FormatBook.Close(false); } catch { }
            try
            {
                WorkingBook.Close(false);
            }
            catch { }

            Worksheet sh;
            Sheets WithBookWorksheets = Book.Worksheets;
            if (WithBookWorksheets.Count == 1)
            {
                //Book.Close(false);
                //return;
            }


            for (int i = 1; i <= WithBookWorksheets.Count; i++)//#OutputFormatting // For Column height/width issue fix
            {
                try
                {
                    //if (WithBookWorksheets[i].Name != "INDEX" && WithBookWorksheets[i].Name != "Graph")
                    //{
                    //    //foreach (Range row in WithBookWorksheets[i].UsedRange.Rows)
                    //    //{
                    //    //    OutputUtil.AutoFitEx(row, row.Application);
                    //    //}
                    //}
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message + "\n" + ex.StackTrace);
                }
            }


            for (int i = WithBookWorksheets.Count; i >= 1; i--)
            {
                sh = WithBookWorksheets.Item[i];
                if (sh.Visible == XlSheetVisibility.xlSheetVisible)
                {
                    xlApp.Goto(sh.Range["A1"]);
                }
            }


            if (outPutFileName != null)
            {
                //string pAth = null;
                //do
                //{
                //    pAth = OutputUtil.BuildPath(OutputDirectoryPath, outPutFileName, xlApp.PathSeparator);
                //} while (File.Exists(pAth));

                Book.CheckCompatibility = false;
                //Book.Activate();
                //Book.SaveAs(pAth, FileFormat, AccessMode: XlSaveAsAccessMode.xlNoChange);
                if (!System.IO.Directory.Exists(outPutFileName.Substring(0, outPutFileName.LastIndexOf('\\'))))
                    throw new FileNotFoundException();
                Book.SaveAs(outPutFileName, FileFormat, AccessMode: XlSaveAsAccessMode.xlNoChange);
                Book.Close(false);
                outputFiles.Add(outPutFileName);
                _log.Info("Book saved");
            }

        }

        private bool GetHasWeight(GTTable Table)
        {
            bool GetHasWeight = false;
            {
                var withBlock = Table;
                if ((withBlock.Question.QuestionType & (QuestionType.SA | QuestionType.MA)) == 0)
                    goto ExitProc;
                return Table.Question.HasWeight || Table.Question.HasCount;
                //if ((withBlock.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                //    GetHasWeight = !OutputUtil.IsNumeric(withBlock.TableValue(withBlock.GetTableValueRowIndexMinimum + 2, withBlock.GetTableValueColumnIndexMinimum));
                //else
                //    GetHasWeight = Strings.Len(withBlock.TableValue(withBlock.GetTableValueRowIndexMinimum, withBlock.GetTableValueColumnIndexMinimum + 2)) == 0;
            }

        ExitProc:
            return GetHasWeight;
        }

    }// End Class
}
