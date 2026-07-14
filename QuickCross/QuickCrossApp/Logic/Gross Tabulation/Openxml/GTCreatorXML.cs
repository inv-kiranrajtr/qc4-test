using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using log4net;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.VisualBasic;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Common.Constants;
using static Qc4Launcher.Logic.Gross_Tabulation.GrossTabulationQc;
using Constants = Microsoft.VisualBasic.Constants;
using VBA = Microsoft.VisualBasic;
using A = DocumentFormat.OpenXml.Drawing;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using NPOI.OpenXmlFormats.Shared;
using Qc4Launcher.Util;

namespace Qc4Launcher.Logic.Gross_Tabulation.Openxml
{
    internal class GTCreatorXML
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const string TEMPLATE_NAME = "GT.xlsx";
        private const string TRANSPOSE_TEMPLATE_NAME = "GT.xlsx";
        private const string SA_MA_NP = "SA_MA_NP";
        private const string SA_MA_N = "SA_MA_N";
        private const string SA_MA_P = "SA_MA_P";
        private const string SAM_MAM_NP = "SAM_MAM_NP";
        private const string SAM_MAM_N = "SAM_MAM_N";
        private const string SAM_MAM_P = "SAM_MAM_P";
        private const string N = "N";
        private const string NM = "NM";
        private const string Standard = "Standard";
        private const string Weight = "Weight";
        private const string SignificanceTest = "SignificanceTest";
        private const string Hybrid = "Hybrid";
        private const long AVERAGE_COLUMN_INDEX = 6;
        /// <summary>
        /// TEMP: skip Graph sheet and all chart/drawing generation to diagnose Google Sheets open failures.
        /// Set to false to restore graphs.
        /// </summary>
        private const bool DisableGraphsForGwsDiagnostics = true;
        public static int LegendWidthDef = 119;
        public static int DataLabelHeightDef = 3;
        public static double NumericLabelRowHt = 0;
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

        //New Implementaion 
        private static ExecuteStaticMethod ExecuteStaticMethod = new ExecuteStaticMethod();
        private static string BookPSWD;
        private static string SheetPSWD;
        //public static string ThisLocationCode;
        private static bool ResumeError;
        CreateFormatTables createFormatTables = null;
        CreateFormatWBTables createFormatWBTables = null;

        CrossCreator CrossCreator = new CrossCreator();

        internal void CreateGT(SpreadsheetDocument package, OutputGT Output, string bookPSWD, string sheetPSWD, string templateDirectoryPath,
             string lccd, string outPutFileName, string[] tableKeys,
             OnWorkerMethodCompleteDelegate OnWorkerComplete, int fileNo, ref double currentProgress, double allocatedProgress, double HideChartDescriptionMaxPercent)
        {
            BookPSWD = bookPSWD;
            SheetPSWD = sheetPSWD;

            const double MATRIX_BARCLUSTER_LINE_HEIGHT = 6;
            string TempPath;
            string FormatPath;
            string ReportTitle;
            Array ContentsValue = null;
            Array PageSetupContentsValue = null;
            Array TableStringValue = null;
            Array DataValue = null;
            Array Ranking = null;
            Array OptionNumbers = null;
            Array OptionNumbersTop = null;
            string PageSetupContentsSheet = null;
            string PageSetupTemplateSheet = null;
            string PageSetupSigTestTemplateSheet = null;
            string GraphSheet = null;
            string FormatSheet = null;
            string SigTestFormatSheet = null;
            string ContentsSheet = null;
            string TemplateSheet = null;
            string SigTestTemplateSheet = null;
            string NPerStartCell = null;
            string NStartCell = null;
            string PerStartCell = null;
            string SigTestStartCell = null;
            string NPerPageSetupStartCell = null;
            string NPageSetupStartCell = null;
            string PerPageSetupStartCell = null;
            string SigTestPageSetupStartCell = null;
            string tmpStartCell;
            string tmpPageSetupStartCell;
            string TableRange = null;
            string GraphRange = null;
            int PerFirstRow = 0;
            int PerEndRow = 0;
            int PerLastCol = 0;
            int PerFirstCol = 0;
            bool SigTestPageSetupOn = false;
            string GraphStartCell = null;
            string tmpChartObject = null;
            string perSheetName = null;
            string chtObjs = null;
            long i;
            string strIdx;
            //string GrphIndx = "0001";
            long j;
            DrawingsPart GraphDrawingsPart = null;
            DrawingsPart IndexDrawingsPart = null;
            int ChartCount = 0;
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
            bool HasWeightColumn = false;
            bool RunSignificanceTest = false;
            long AddColumnCount;
            string fmt;
            Array HyperlinkTargetCells;
            Array PageSetupHyperlinkTargetCells = null;
            string tmpAddress = null;
            bool CutPreWB;
            bool IsLastTable;
            long u = 0;
            Macromill.QCWeb.Batch.Report.Request tmpRequest;
            bool PageSetupOn;
            Collection GraphSourceRangeCol;
            Collection GraphTableRangeCol;
            Collection nCol;
            Collection ChartObjectCol;
            bool IsNPerSourceRange;
            bool IsNSourceRange;   // RATだけ
            long NPerRemainedPageSetupRowsCount;
            long NRemainedPageSetupRowsCount;
            long PerRemainedPageSetupRowsCount;
            long SigTestRemainedPageSetupRowsCount;
            bool isMatrix = false;
            bool isN = false;
            long n;
            KeyItemInformation KeyItemInfo;
            string KeyItemName = null;
            string filenameSuffix = string.Empty;
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
            bool CreateGraph = false;
            NumericLabelRowHt = 0;

            try
            {

                _log.Info("Excel base book added");

                CutMedian = Output.ParentRequest.ShowMedian & Output.WBOn;

                Reportset reportset = (Reportset)Output.ParentReportset;
                OrgProcName = RunningProcName;
                RunningProcName = "GTCreator.CreateGT";
                CurrentOutput = Output;

                createFormatWBTables = new CreateFormatWBTables();
                createFormatTables = new CreateFormatTables();

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
                    CreateGTSub(package, Output, Tables, ref HasWeight, ref HasWeightColumn, ref UseWeightFormat, ref isMatrix, ref isN, ref CreateGraph, ref FormatSheet
                           , ref RunSignificanceTest, ref HasMatrixBetweenChildSigTest, ref HasMatrixBetweenSectorSigTest, ref HasNormalSigTest
                           , PageSetupOn, ref SigTestPageSetupOn, ref ContentsSheet, ref TemplateSheet, ref SigTestTemplateSheet
                           , ref PageSetupContentsSheet, ref PageSetupTemplateSheet, ref PageSetupSigTestTemplateSheet, ref GraphSheet);
                    header = OutputUtil.GetAdjustedHeader(ReportTitle);

                    Sheets sheets = package.WorkbookPart.Workbook.Sheets;
                    Sheet lastSheet = sheets.Descendants<Sheet>().LastOrDefault();
                    uint sheetId = lastSheet.SheetId.Value;
                    string id = "rId" + Convert.ToInt32(Regex.Match(lastSheet.Id.Value, @"\d+").Value);

                    if (Output.SignificanceTest && (SigTestTemplateSheet == "Standard" || SigTestTemplateSheet == "WT"))
                    {
                        sheetId++; id = "rId" + sheetId;
                        string reportKeyWord = LocalResource.REPORT_CROSS_SIGNIFICANCE_TEST_SHEET_NAME;//ExecuteStaticMethod.GetMessage(ReportMessageIndex.ReportCrossSignificanceTestSheetNameIndex, lccd).Description;     
                        CreateSheet(package, SigTestTemplateSheet, reportKeyWord, sheetId, id);
                        SigTestStartCell = reportKeyWord;
                        if (PageSetupOn & Output.PageSetupSignificanceTestTable)
                        {
                            reportKeyWord = LocalResource.REPORT_CROSS_PAGE_SETUP_SHEET_SUFFIX;
                        }
                    }

                    if (Output.OutputPerTable)
                    {
                        sheetId++; id = "rId" + sheetId;
                        string reportKeyWord = LocalResource.REPORT_CROSS_P_SHEET_NAME;
                        CreateSheet(package, TemplateSheet, reportKeyWord, sheetId, id);
                        PerStartCell = reportKeyWord;
                        if (PageSetupOn & Output.PageSetupPerTable)
                        {
                            if (Output.OutputNTable & Output.PageSetupNTable)
                            {

                            }
                            else if (Output.OutputNPerTable & Output.PageSetupNPerTable)
                            {

                            }
                            reportKeyWord = LocalResource.REPORT_CROSS_PAGE_SETUP_SHEET_SUFFIX;
                        }
                    }

                    if (Output.OutputNTable)
                    {
                        sheetId++; id = "rId" + sheetId;
                        string reportKeyWord = LocalResource.REPORT_CROSS_N_SHEET_NAME;
                        NStartCell = reportKeyWord;
                        CreateSheet(package, TemplateSheet, reportKeyWord, sheetId, id);
                        if (PageSetupOn & Output.PageSetupNTable)
                        {
                            if (Output.OutputNPerTable & Output.PageSetupNPerTable)
                            {

                            }
                            reportKeyWord = LocalResource.REPORT_CROSS_PAGE_SETUP_SHEET_SUFFIX;
                        }
                    }

                    if (Output.OutputNPerTable)
                    {
                        sheetId++; id = "rId" + sheetId;
                        string reportKeyWord = LocalResource.REPORT_CROSS_NP_SHEET_NAME;
                        NPerStartCell = reportKeyWord;
                        CreateSheet(package, TemplateSheet, reportKeyWord, sheetId, id);
                        if (PageSetupOn & Output.PageSetupNPerTable)
                        {
                            reportKeyWord = LocalResource.REPORT_CROSS_PAGE_SETUP_SHEET_SUFFIX;
                        }
                    }

                    if (CreateGraph)
                    {
                        string reportKeyWord = LocalResource.REPORT_CROSS_GRAPH_SHEET_NAME;
                        var sheet = GetSheetByName(package, GraphSheet);
                        sheet.Name = reportKeyWord;
                        GraphStartCell = reportKeyWord;
                        WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(package, GraphStartCell);
                        GraphDrawingsPart = worksheetPart.AddNewPart<DrawingsPart>("rId2");
                        ChartCount = 1;
                    }

                    if (Output.SignificanceTest && SigTestTemplateSheet != "Standard" && SigTestTemplateSheet != "WT")
                    {
                        string reportKeyWord = LocalResource.REPORT_CROSS_SIGNIFICANCE_TEST_SHEET_NAME;//ExecuteStaticMethod.GetMessage(ReportMessageIndex.ReportCrossSignificanceTestSheetNameIndex, lccd).Description;
                        Sheet sheet = GetSheetByName(package, SigTestTemplateSheet);
                        sheet.Name = reportKeyWord;
                        SigTestStartCell = reportKeyWord;
                        if (PageSetupOn & Output.PageSetupSignificanceTestTable)
                        {
                            reportKeyWord = LocalResource.REPORT_CROSS_PAGE_SETUP_SHEET_SUFFIX;
                        }
                    }

                    // フォーマットブックの必要シートへの参照を取得
                    {
                        if (FormatSheet == null)
                        {
                            FormatSheet = "Standard";
                            if (Output.SignificanceTest)
                                SigTestFormatSheet = "SignificanceTest";
                        }
                        else if (Output.SignificanceTest)
                            SigTestFormatSheet = "Hybrid";
                    }

                    CutPreWB = CurrentOutput.WBOn & !CurrentOutput.ShowPreWBTotal;
                    // 無駄なカラムの削除
                    if (Output.SignificanceTest)
                    {
                        if (Output.Orientation == TableOrientation.Landscape)
                        {
                            if (!(HasNormalSigTest || HasMatrixBetweenChildSigTest))
                            {
                                HasLetterColumn = true;
                            }
                        }
                        else
                        {
                            if (!(HasNormalSigTest || HasMatrixBetweenSectorSigTest))
                            {
                                HasLetterColumn = true;
                            }
                        }
                    }

                    AdjustLandscapeFormat(package, Output, FormatSheet, CutPreWB, HasWeightColumn, UseWeightFormat);

                    n = (int)((Math.Log(withBlock.Count) / Math.Log(10)) + 1);
                    if (n < 4)
                        n = 4;
                    fmt = new String('0', Convert.ToInt32(n));

                    ContentsValue = Array.CreateInstance(typeof(string), // ReDim ContentsValue(1& To .Count, 1& To 6&)
                    new int[] { withBlock.Count, 8 },
                    new int[] { 1, 1 });

                    HyperlinkTargetCells = Array.CreateInstance(typeof(object), // ReDim HyperlinkTargetCells(1& To .Count, 3& To 6&)
                    new int[] { withBlock.Count, 6 }, // new int[] { 1st element length, 2nd element length }
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
                    int g = 0;
                    for (i = 1; i <= withBlock.Count; i++)
                    {
                        //IsQCM = false;
                        #region Progress Bar Implementation
                        double progressChildPerc = (double)i / withBlock.Count * 100;
                        childProgress = allocatedProgress * progressChildPerc / 100;
                        UpdProgress = currentProgress + childProgress;
                        OnWorkerComplete(Convert.ToInt32(UpdProgress), String.Format(LocalResource.PB_GT_GENE_RPTS_TABLE, fileNo, i, withBlock.Count));
                        #endregion


                        tmpGTTable = (GTTable)withBlock[(int)i - 1];
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
                                //            Information.Err().Raise(Constants.vbObjectError + 200 &, RunningProcName, WorkingBook.yoyoReportKeyword(ReportMessageIndex.ReportGetArraySizeFailedMessageIndex));
                            }


                            // 目次での該当行の内容

                            ContentsValue.SetValue(withBlock1.Question.Name, i, 1);   // 質問ID
                            ContentsValue.SetValue(withBlock1.Question.TableHeading, i, 2);    // 質問文
                            ContentsValue.SetValue(withBlock1.Question.Description, i, 3);

                            if (Output.OutputNPerTable)
                                ContentsValue.SetValue("Table" + strIdx, i, 4);
                            if (Output.OutputNTable)
                                ContentsValue.SetValue("NTable" + strIdx, i, 5);
                            if (Output.OutputPerTable)
                                ContentsValue.SetValue("PTable" + strIdx, i, 6);
                            if (Output.SignificanceTest)
                                ContentsValue.SetValue("TTable" + strIdx, i, 7);
                            if (!(withBlock1.Chart == null))
                            {
                                if (withBlock1.Chart.ChartType != 0)
                                {
                                    ContentsValue.SetValue("Graph" + strIdx, i, 8);
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
                                        CreateNormalGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, HasWeight, TableType.NPer, CutMedian: CutMedian, MedIdx: MedIdx);
                                        // データ出力
                                        u = Information.UBound(TableStringValue, 2);
                                        tmpStartCell = null;
                                        tmpPageSetupStartCell = NPerPageSetupStartCell;

                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell; //tmpAddress = tmpPageSetupStartCell.Address(External:=True)
                                        OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, 0
                                                , TableOrientation.Portrait, strIdx
                                                , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.NPer
                                                , FormatSheet, ref TableRange, ref NPerStartCell, ref NPerPageSetupStartCell, IsLastTable
                                                , ref NPerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = tmpAddress; // Set tmpPageSetupStartCell = Me.Application.Range(tmpAddress)
                                        ResumeContinue = true;

                                        if (tmpStartCell == NPerStartCell)
                                        {
                                            NPerStartCell = null;
                                            if (i == 1)
                                            {
                                                if (NStartCell == null && PerStartCell == null && SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                }
                                                else
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 3);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 4);
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
                                        }
                                        if (!(tmpPageSetupStartCell == null))
                                        {
                                            f = !(NPerPageSetupStartCell == null);
                                            if (f)
                                                f = (NPerPageSetupStartCell == tmpPageSetupStartCell);
                                            if (f)
                                            {
                                                if (i == 1)
                                                {

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
                                        CreateNormalGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, HasWeight, TableType.N, CutMedian: CutMedian, MedIdx: MedIdx);
                                        // データ出力
                                        u = Information.UBound(TableStringValue, 2);
                                        tmpStartCell = null;
                                        tmpPageSetupStartCell = NPageSetupStartCell;
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell; //tmpAddress = tmpPageSetupStartCell.Address(External: true);

                                        OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, 0
                                                , TableOrientation.Portrait, strIdx
                                                , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.N
                                                , FormatSheet, ref TableRange, ref NStartCell, ref NPageSetupStartCell, IsLastTable
                                                , ref NRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = tmpAddress;
                                        ResumeContinue = true;
                                        if (tmpStartCell == NStartCell)
                                        {
                                            NStartCell = null;
                                            if (i == 1)
                                            {
                                                if (SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                }
                                                else
                                                {
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 4);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 5);
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
                                                f = NPageSetupStartCell == tmpPageSetupStartCell;
                                            if (f)
                                            {
                                                if (i == 1)
                                                {
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
                                        CreateNormalGTArray(tmpGTTable, rl, ru, cl, cu
                                        , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, HasNA, HasIV, NAIdx, IVIdx
                                        , AddColumnCount, HasWeight, TableType.Per, CutMedian: CutMedian, MedIdx: MedIdx);
                                        // データ出力
                                        u = Information.UBound(TableStringValue, 2);
                                        tmpStartCell = null;
                                        tmpPageSetupStartCell = PerPageSetupStartCell;
                                        perSheetName = PerStartCell;

                                        //Get firstRow
                                        WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(package, PerStartCell);
                                        Row lRow = worksheetPart.Worksheet.Descendants<Row>().LastOrDefault();
                                        PerFirstRow = lRow == null ? 5 : ((int)lRow.RowIndex.Value + 4);

                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                        OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, 0
                                            , TableOrientation.Portrait, strIdx
                                            , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.Per
                                            , FormatSheet, ref TableRange, ref PerStartCell, ref PerPageSetupStartCell, IsLastTable
                                            , ref PerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = tmpAddress;
                                        ResumeContinue = true;
                                        if (tmpStartCell == PerStartCell)
                                        {
                                            PerStartCell = null;
                                            if (i == 1)
                                            {
                                                if (SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                }
                                                else
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 5);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 6);
                                            if (PerStartCell == null & !IsLastTable)
                                            {
                                                GraphStartCell = null;
                                                tmpNextTable = (GTTable)Tables[(int)i];
                                            }
                                            if (Information.UBound(TableStringValue, 2) < u)
                                            {
                                            }
                                            else if ((TableRange != null))
                                            {
                                                {
                                                    nCol = new VBA.Collection();
                                                    PerEndRow = (int)worksheetPart.Worksheet.Descendants<Row>().LastOrDefault().RowIndex.Value - 1;
                                                    if ((withBlock1.Question.QuestionType & (QuestionType.SA)) != 0)
                                                        PerEndRow = PerEndRow - (tmpGTTable.Question.SubTotalCnt + ((HasWeight ? 2 : 0) * 1));
                                                    else
                                                        PerEndRow = PerEndRow - (HasWeight ? 2 : 0) * 1;
                                                    PerFirstCol = Information.LBound(TableStringValue, 2) + 1;
                                                    PerLastCol = Information.UBound(TableStringValue, 2) + 1;
                                                    int r1 = ((Output.WBOn && Output.ShowPreWBTotal) ? PerFirstRow + 2 : PerFirstRow + 1);
                                                    Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)r1);
                                                    var columnValue = OpenXmlHelper.GetCell(row, r1, PerLastCol).CellValue.InnerText;//1020;//withBlock2.Cells.Item(0, withBlock2.Columns.Count).Value;
                                                    if (columnValue == null)
                                                        columnValue = "0";
                                                    nCol.Add(Math.Round(Convert.ToDouble(columnValue)));
                                                    var dataRow = Information.LBound(DataValue, 1) - 1;
                                                    PerFirstRow = PerFirstRow + ((Output.WBOn && Output.ShowPreWBTotal) ? dataRow : (dataRow - 1));
                                                }
                                            }
                                        }
                                        if (!(tmpPageSetupStartCell == null))
                                        {
                                            f = !(PerPageSetupStartCell == null);
                                            if (f)
                                                f = PerPageSetupStartCell == tmpPageSetupStartCell;
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
                                        CreateNormalGTArray(tmpGTTable, rl, ru, cl, cu
                                        , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, HasNA, HasIV, NAIdx, IVIdx
                                        , AddColumnCount, HasWeight, TableType.SignificanceTest, CutMedian: CutMedian, MedIdx: MedIdx);
                                        // データ出力
                                        u = Information.UBound(TableStringValue, 2);
                                        tmpStartCell = null;
                                        tmpPageSetupStartCell = SigTestPageSetupStartCell;
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
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
                                        OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, 0
                                                , TableOrientation.Portrait, strIdx
                                                , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.SignificanceTest
                                                , SigTestFormatSheet, ref TableRange, ref SigTestStartCell, ref SigTestPageSetupStartCell, IsLastSigTestTable
                                                , ref SigTestRemainedPageSetupRowsCount, false, CutMedian, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = tmpAddress;
                                        ResumeContinue = true;
                                        if (tmpStartCell == SigTestStartCell)
                                        {
                                            SigTestStartCell = null;
                                            if (IsFirstSigTestTable)
                                            {
                                                if (NPerStartCell == null && NStartCell == null && PerStartCell == null)
                                                {
                                                    ResumeContinue = false;
                                                }
                                                else
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            HyperlinkTargetCells.SetValue(TableRange, i, 7);
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
                                                f = SigTestPageSetupStartCell == tmpPageSetupStartCell;
                                            if (f)
                                            {
                                                if (IsFirstSigTestTable)
                                                {
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
                                        tmpStartCell = null;
                                        tmpPageSetupStartCell = NPerPageSetupStartCell;

                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                        OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, 0
                                                , Output.Orientation, strIdx
                                                , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.NPer
                                                , FormatSheet, ref TableRange, ref NPerStartCell, ref NPerPageSetupStartCell, IsLastTable
                                                , ref NPerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = tmpAddress;
                                        ResumeContinue = true;
                                        if (tmpStartCell == NPerStartCell)
                                        {
                                            NPerStartCell = null;
                                            if (i == 1)
                                            {
                                                if (NStartCell == null & PerStartCell == null & SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;

                                                }
                                                else
                                                {

                                                }
                                            };
                                        }
                                        else
                                        {
                                            NotRevise = true;
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 3);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 4);
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
                                                f = NPerPageSetupStartCell == tmpPageSetupStartCell;
                                            if (f)
                                            {
                                                if (i == 1)
                                                {

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
                                        tmpStartCell = null;
                                        tmpPageSetupStartCell = NPageSetupStartCell;
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                        OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, 0
                                                , Output.Orientation, strIdx
                                                , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.N
                                                , FormatSheet, ref TableRange, ref NStartCell, ref NPageSetupStartCell, IsLastTable
                                                , ref NRemainedPageSetupRowsCount, false, CutMedian, false, NotRevise, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = tmpAddress;
                                        ResumeContinue = true;
                                        if (tmpStartCell == NStartCell)
                                        {
                                            NStartCell = null;
                                            if (i == 1)
                                            {
                                                if (SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;

                                                }
                                                else
                                                {
                                                }
                                            };
                                        }
                                        else
                                        {
                                            NotRevise = true;
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 4);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 5);

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
                                                f = NPageSetupStartCell == tmpPageSetupStartCell;
                                            if (f)
                                            {
                                                if (i == 1)
                                                {

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
                                        tmpStartCell = null;
                                        tmpPageSetupStartCell = PerPageSetupStartCell;
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                        OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, 0
                                                , Output.Orientation, strIdx
                                                , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                                , withBlock1.Question.QuestionType, TableType.Per
                                                , FormatSheet, ref TableRange, ref PerStartCell, ref PerPageSetupStartCell, IsLastTable
                                                , ref PerRemainedPageSetupRowsCount, false, CutMedian, false, NotRevise, OptionNumbers: OptionNumbers);
                                        if (!(tmpPageSetupStartCell == null))
                                            tmpPageSetupStartCell = tmpAddress;
                                        ResumeContinue = true;
                                        if (tmpStartCell == PerStartCell)
                                        {
                                            PerStartCell = null;
                                            if (i == 1)
                                            {
                                                if (SigTestStartCell == null)
                                                {
                                                    ResumeContinue = false;

                                                }
                                                else
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            //HyperlinkTargetCells.SetValue(tmpStartCell, i, 5);
                                            HyperlinkTargetCells.SetValue(TableRange, i, 6);
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
                                                f = PerPageSetupStartCell == tmpPageSetupStartCell;
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
                                    if (Output.Orientation == TableOrientation.Portrait)
                                        CreatePortraitGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.NPer, CutMedian: CutMedian, MedIdx: MedIdx);
                                    else
                                        CreateLandscapeGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, ref OptionNumbersTop, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.NPer, CutMedian: CutMedian, MedIdx: MedIdx);
                                    u = Information.UBound(TableStringValue, 2);
                                    tmpStartCell = null;
                                    tmpPageSetupStartCell = NPerPageSetupStartCell;
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.NPer
                                            , FormatSheet, ref TableRange, ref NPerStartCell, ref NPerPageSetupStartCell, IsLastTable
                                            , ref NPerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers, OptionNumbersTop: OptionNumbersTop);

                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = tmpAddress;
                                    ResumeContinue = true;
                                    if (tmpStartCell == NPerStartCell)
                                    {
                                        NPerStartCell = null;
                                        if (i == 1)
                                        {
                                            if (NStartCell == null && PerStartCell == null && SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;

                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 3);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 4);
                                        if (NPerStartCell == null & !IsLastTable)
                                        {
                                            if (PerStartCell == null) GraphStartCell = null;
                                            tmpNextTable = (GTTable)Tables[(int)i];
                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {

                                        }
                                        if (!(TableRange == null))
                                            IsQCM = (withBlock1.Chart.ChartType & XlChartType.QCM) == XlChartType.QCM;
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(NPerPageSetupStartCell == null);
                                        if (f)
                                            f = NPerPageSetupStartCell == tmpPageSetupStartCell;
                                        if (f)
                                        {
                                            if (i == 1)
                                            {

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
                                    if (Output.Orientation == TableOrientation.Portrait)
                                        CreatePortraitGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.N, CutMedian: CutMedian, MedIdx: MedIdx);
                                    else
                                        CreateLandscapeGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, ref OptionNumbersTop, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.N, CutMedian: CutMedian, MedIdx: MedIdx);
                                    u = Information.UBound(TableStringValue, 2);
                                    tmpStartCell = null;
                                    tmpPageSetupStartCell = null;
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.N
                                            , FormatSheet, ref TableRange, ref NStartCell, ref NPageSetupStartCell, IsLastTable
                                            , ref NRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers, OptionNumbersTop: OptionNumbersTop);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = tmpAddress;
                                    ResumeContinue = true;
                                    if (tmpStartCell == NStartCell)
                                    {
                                        NStartCell = null;
                                        if (i == 1)
                                        {
                                            if (SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;

                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 4);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 5);
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
                                            f = NPageSetupStartCell == tmpPageSetupStartCell;
                                        if (f)
                                        {
                                            if (i == 1)
                                            {

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
                                    if (Output.Orientation == TableOrientation.Portrait)
                                        CreatePortraitGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.Per, CutMedian: CutMedian, MedIdx: MedIdx);
                                    else
                                        CreateLandscapeGTArray(tmpGTTable, rl, ru, cl, cu
            , ref TableStringValue, ref DataValue, ref Ranking, ref OptionNumbers, ref OptionNumbersTop, HasNA, HasIV, NAIdx, IVIdx
            , AddColumnCount, ro, co, TableType.Per, CutMedian: CutMedian, MedIdx: MedIdx);
                                    u = Information.UBound(TableStringValue, 2);
                                    tmpStartCell = null;

                                    //Get FirstRow
                                    WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(package, PerStartCell);
                                    Row lRow = worksheetPart.Worksheet.Descendants<Row>().LastOrDefault();
                                    PerFirstRow = lRow == null ? 5 : ((int)lRow.RowIndex.Value + 4);

                                    tmpPageSetupStartCell = null;
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);

                                    OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.Per
                                            , FormatSheet, ref TableRange, ref PerStartCell, ref PerPageSetupStartCell, IsLastTable
                                            , ref PerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers, OptionNumbersTop: OptionNumbersTop);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = tmpAddress;
                                    ResumeContinue = true;
                                    if (tmpStartCell == PerStartCell)
                                    {
                                        PerStartCell = null;
                                        if (i == 1)
                                        {
                                            if (SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;
                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 5);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 6);
                                        if (PerStartCell == null & !IsLastTable)
                                        {
                                            GraphStartCell = null;
                                            tmpNextTable = (GTTable)Tables[(int)i];
                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {

                                        }
                                        else if ((TableRange != null))
                                        {
                                            GraphSourceRangeCol = new Collection();
                                            GraphTableRangeCol = new Collection();
                                            nCol = new Collection();

                                            IsQCM = (withBlock1.Chart.ChartType & XlChartType.QCM) == XlChartType.QCM;
                                            if (!IsQCM)
                                            {
                                                {
                                                    var withBlock2 = TableRange;

                                                    if (Output.Orientation == TableOrientation.Portrait)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        PerEndRow = (int)worksheetPart.Worksheet.Descendants<Row>().LastOrDefault().RowIndex.Value - 1;
                                                        var lCol = Information.UBound(TableStringValue, 2) + 1;
                                                        PerFirstCol = lCol - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(HasIV) & 1) - (CrossCreator.ToInt(HasNA) & 1) - tmpGTTable.SectorsCount + 1;
                                                        PerLastCol = (PerFirstCol + tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)) - 1;

                                                        switch (tmpGTTable.Chart.ChartType)
                                                        {
                                                            case XlChartType.xlBarStacked:
                                                            case XlChartType.xlColumnStacked:   // RNK
                                                                {
                                                                    int val = 0;
                                                                    try { val = Convert.ToInt32(DataValue.GetValue(DataValue.GetLowerBound(0), Output.ShowPreWBTotal ? (DataValue.GetLowerBound(1) + 1) : DataValue.GetLowerBound(1))); } catch { }
                                                                    nCol.Add(val.ToString("0"));
                                                                    break;
                                                                }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var withBlock2 = TableRange;
                                                if (Output.Orientation == TableOrientation.Portrait)
                                                {

                                                }
                                                else
                                                {
                                                    PerEndRow = (int)worksheetPart.Worksheet.Descendants<Row>().LastOrDefault().RowIndex.Value - 1;
                                                    var lCol = Information.UBound(TableStringValue, 2) + 1;
                                                    PerFirstCol = lCol - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(HasIV) & 1) - (CrossCreator.ToInt(HasNA) & 1) - tmpGTTable.SectorsCount + 1;
                                                    PerLastCol = (PerFirstCol + tmpGTTable.SectorsCount + (CrossCreator.ToInt(HasNA) & 1)) - 1;

                                                }
                                            }
                                        }
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(PerPageSetupStartCell == null);
                                        if (f)
                                            f = PerPageSetupStartCell == tmpPageSetupStartCell;
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
                                if (!(SigTestStartCell == null) && tmpGTTable.SignificancetestCode != SignificanceTestCode.Off)
                                {
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
                                    tmpStartCell = null;
                                    tmpPageSetupStartCell = null;
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
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
                                    OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, withBlock1.SectorsCount, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.SignificanceTest
                                            , SigTestFormatSheet, ref TableRange, ref SigTestStartCell, ref SigTestPageSetupStartCell, IsLastSigTestTable
                                            , ref SigTestRemainedPageSetupRowsCount, false, CutMedian, HasLetterColumn, false, OptionNumbers: OptionNumbers, OptionNumbersTop: OptionNumbersTop);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = tmpAddress;
                                    ResumeContinue = true;
                                    if (tmpStartCell == SigTestStartCell)
                                    {
                                        SigTestStartCell = null;
                                        if (IsFirstSigTestTable)
                                        {
                                            if (NPerStartCell == null && NStartCell == null && PerStartCell == null)
                                            {
                                                ResumeContinue = false;

                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        HyperlinkTargetCells.SetValue(TableRange, i, 7);
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
                                            f = SigTestPageSetupStartCell == tmpPageSetupStartCell;
                                        if (f)
                                        {
                                            if (IsFirstSigTestTable)
                                            {

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
                                    tmpStartCell = null;
                                    tmpPageSetupStartCell = NPerPageSetupStartCell;

                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.NPer
                                            , FormatSheet, ref TableRange, ref NPerStartCell, ref NPerPageSetupStartCell, IsLastTable
                                            , ref NPerRemainedPageSetupRowsCount, CutMedian: CutMedian, OptionNumbers: OptionNumbers);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = tmpAddress;
                                    ResumeContinue = true;
                                    if (tmpStartCell == NPerStartCell)
                                    {
                                        NPerStartCell = null;
                                        if (i == 1)
                                        {
                                            if (NStartCell == null & PerStartCell == null & SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;
                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        NotRevise = true;
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 3);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 4);
                                        if (NPerStartCell == null & !IsLastTable)
                                        {
                                            if (PerStartCell == null)
                                                GraphStartCell = null;
                                            tmpNextTable = (GTTable)Tables[(int)i];

                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {

                                        }
                                        else if ((withBlock1.Question.QuestionType & QuestionType.Ratio) == QuestionType.Ratio)
                                        {

                                        }
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(NPerPageSetupStartCell == null);
                                        if (f)
                                            f = NPerPageSetupStartCell == tmpPageSetupStartCell;
                                        if (f)
                                        {
                                            if (i == 1)
                                            {

                                            }
                                            else
                                            {

                                            }
                                            NPerPageSetupStartCell = null;
                                        }
                                        else
                                        {
                                            PageSetupHyperlinkTargetCells.SetValue(tmpPageSetupStartCell, i, 3);
                                            if (NPerPageSetupStartCell == null && !IsLastTable)
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
                                    tmpStartCell = null;
                                    tmpPageSetupStartCell = NPageSetupStartCell;

                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.N
                                            , FormatSheet, ref TableRange, ref NStartCell, ref NPageSetupStartCell, IsLastTable
                                            , ref NRemainedPageSetupRowsCount, false, CutMedian, false, NotRevise, OptionNumbers: OptionNumbers);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = tmpAddress;
                                    ResumeContinue = true;
                                    if (tmpStartCell == NStartCell)
                                    {
                                        NStartCell = null;
                                        if (i == 1)
                                        {
                                            if (SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;
                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        NotRevise = true;
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 4);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 5);
                                        if (NStartCell == null && !IsLastTable)
                                        {
                                            tmpNextTable = (GTTable)Tables[(int)i];

                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {

                                        }
                                        else if ((withBlock1.Question.QuestionType & QuestionType.Ratio) == QuestionType.Ratio)
                                        {
                                        }
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(NPageSetupStartCell == null);
                                        if (f)
                                            f = NPageSetupStartCell == tmpPageSetupStartCell;
                                        if (f)
                                        {
                                            if (i == 1)
                                            {
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
                                    tmpStartCell = null;
                                    tmpPageSetupStartCell = PerPageSetupStartCell;

                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
                                    OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.Per
                                            , FormatSheet, ref TableRange, ref PerStartCell, ref PerPageSetupStartCell, IsLastTable
                                            , ref PerRemainedPageSetupRowsCount, false, CutMedian, false, NotRevise, OptionNumbers: OptionNumbers);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = tmpAddress;
                                    ResumeContinue = true;
                                    if (tmpStartCell == PerStartCell)
                                    {
                                        PerStartCell = null;
                                        if (i == 1)
                                        {
                                            if (SigTestStartCell == null)
                                            {
                                                ResumeContinue = false;

                                            }
                                            else
                                            {
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //HyperlinkTargetCells.SetValue(tmpStartCell, i, 5);
                                        HyperlinkTargetCells.SetValue(TableRange, i, 6);
                                        if (PerStartCell == null && !IsLastTable)
                                        {
                                            GraphStartCell = null;
                                            tmpNextTable = (GTTable)Tables[(int)i];

                                        }
                                        if (Information.UBound(TableStringValue, 2) < u)
                                        {

                                        }
                                        else if ((withBlock1.Question.QuestionType & QuestionType.Ratio) == QuestionType.Ratio)
                                            SetRatSourceRange(tmpGTTable, package, PerStartCell, ref PerFirstRow, ref PerEndRow, ref PerLastCol, ref PerFirstCol, TableRange, ref GraphSourceRangeCol, ref GraphTableRangeCol, ref nCol);
                                    }
                                    if (!(tmpPageSetupStartCell == null))
                                    {
                                        f = !(PerPageSetupStartCell == null);
                                        if (f)
                                            f = PerPageSetupStartCell == tmpPageSetupStartCell;
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
                                if (!(SigTestStartCell == null) && tmpGTTable.SignificancetestCode == SignificanceTestCode.BetweenChildQuestions)
                                {
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
                                    tmpStartCell = null;
                                    tmpPageSetupStartCell = SigTestPageSetupStartCell;
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpAddress = tmpPageSetupStartCell;//tmpAddress = tmpPageSetupStartCell.Address(External: true);
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
                                    OutputData(package, Output, HasWeightColumn, tmpGTTable, ref TableStringValue, ref DataValue, ref Ranking, 0, withBlock1.ChildQuestionsCount
                                            , Output.Orientation, strIdx
                                            , CutNA, CutIV, CutPreWB, HasWeight, UseWeightFormat
                                            , withBlock1.Question.QuestionType, TableType.SignificanceTest
                                            , SigTestFormatSheet, ref TableRange, ref SigTestStartCell, ref SigTestPageSetupStartCell, IsLastSigTestTable
                                            , ref SigTestRemainedPageSetupRowsCount, false, CutMedian, OptionNumbers: OptionNumbers);
                                    if (!(tmpPageSetupStartCell == null))
                                        tmpPageSetupStartCell = tmpAddress;
                                    ResumeContinue = true;
                                    if (tmpStartCell == SigTestStartCell)
                                    {
                                        SigTestStartCell = null;
                                        if (IsFirstSigTestTable)
                                        {
                                            if (NPerStartCell == null & NStartCell == null & PerStartCell == null)
                                            {
                                                ResumeContinue = false;
                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        HyperlinkTargetCells.SetValue(TableRange, i, 7);
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
                                            f = SigTestPageSetupStartCell == tmpPageSetupStartCell;
                                        if (f)
                                        {
                                            if (IsFirstSigTestTable)
                                            {
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
                            else
                            {
                                // エラースロー
                                //Information.Err().Raise(Constants.vbObjectError + 100 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportUnjustQuestionTypeMessageIndex));
                            }
                            int chartObj = 0;
                            ChartObjectCol = new Collection();
                            tmpChartObject = null;
                            WorksheetPart worksheetGraphPart = null;
                            // TEMP GWS diagnostics: skip all graph generation
                            if (!DisableGraphsForGwsDiagnostics && !(GraphStartCell == null || withBlock1.Chart == null || withBlock1.Chart.ChartType == 0))
                            {
                                chartObj = 0;
                                int qcmNum = 1;
                                string drawingFromRow;
                                string drawingToRow;
                                string chtCaption;
                                string chtType;
                                string plotby;
                                string tmpPlotby;
                                string subTitle = null;
                                string graphIdxRow;
                                bool isQCM = false;
                                bool isNumeric = false;
                                Xdr.TwoCellAnchor twoCellAnchor;
                                string QuestionDescription = String.Empty;
                                if ((withBlock1.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                                {
                                    if (!String.IsNullOrEmpty(withBlock1.Question.TableHeading))
                                    {
                                        QuestionDescription = withBlock1.Question.TableHeading;
                                    }
                                }
                                else
                                {
                                    if (String.IsNullOrEmpty(withBlock1.Question.TableHeading))
                                    {
                                        QuestionDescription = withBlock1.Question.Description != null ? withBlock1.Question.Description : string.Empty;
                                    }
                                    else
                                    {
                                        QuestionDescription = withBlock1.Question.TableHeading + (withBlock1.Question.Description != null ? " \n【" + withBlock1.Question.Description + "】" : string.Empty);
                                    }
                                }
                                chtCaption = "[" + withBlock1.Question.Name + "]" + QuestionDescription;
                                worksheetGraphPart = OpenXmlHelper.GetWorksheetPartByName(package, GraphStartCell);
                                chtType = (withBlock1.Chart.ChartType & ~(XlChartType.QCM | XlChartType.RAT)).ToString();
                                do
                                {
                                    if (isMatrix)
                                    {
                                        if (isN)
                                        {
                                            if ((withBlock1.Question.QuestionType & QuestionType.Ratio) == 0)
                                                break;
                                            switch (chtType)
                                            {
                                                case "xlPie":
                                                    {
                                                        break;
                                                    }

                                                case "xlColumnClustered":
                                                    {
                                                        break;
                                                    }

                                                case "xlBarClustered":
                                                    {
                                                        break;
                                                    }

                                                default:
                                                    {
                                                        goto ExitDo; //Exit Do
                                                    }
                                            }
                                            isNumeric = true;
                                            plotby = Output.Orientation == TableOrientation.Portrait ? "xlRows" : "xlColumns";
                                            errdesc = Constants.vbNullString;
                                            CreateChartObject(package, GraphStartCell, PerFirstCol, PerLastCol, PerFirstRow, PerStartCell, ref ChartCount,
                                             PerEndRow, GraphDrawingsPart, HasWeight, tmpGTTable, chtObjs, chtCaption, chtType, GraphSheet, ref GraphRange
                                               , ref NamesArray, ref errdesc, false, plotby, false, string.Empty, nCol[1].ToString(), 50, isNumeric, 100, "0.0", HideChartDescriptionMaxPercent: HideChartDescriptionMaxPercent);
                                            chartObj++;
                                            break;
                                        }

                                        if ((withBlock1.Question.QuestionType & QuestionType.Rank) == QuestionType.Rank)
                                        {
                                            switch (chtType)
                                            {
                                                case "xlBarStacked":
                                                    {
                                                        break;
                                                    }

                                                case "xlColumnStacked":
                                                    {
                                                        break;
                                                    }

                                                default:
                                                    {
                                                        goto ExitDo; //Exit Do
                                                    }
                                            }

                                            plotby = Output.Orientation == TableOrientation.Portrait ? "xlColumns" : "xlRows";
                                            errdesc = Constants.vbNullString;
                                            CreateChartObject(package, GraphStartCell, PerFirstCol, PerLastCol, PerFirstRow, PerStartCell, ref ChartCount,
                                           PerEndRow, GraphDrawingsPart, HasWeight, tmpGTTable, chtObjs, chtCaption, chtType
                                           , GraphSheet, ref GraphRange, ref NamesArray, ref errdesc, false, plotby, true, string.Empty, nCol[1].ToString()
                                              , 50, false, 100, "0\"%\"", false, Output.Orientation == TableOrientation.Portrait, HideChartDescriptionMaxPercent: HideChartDescriptionMaxPercent);
                                            chartObj++;
                                            if (tmpChartObject == null)
                                            {
                                                if (OutputUtil.StrPtr(errdesc) != 0)
                                                {
                                                    ResumeError = true;
                                                    //Information.Err().Raise(Constants.vbObjectError + 910 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                                }

                                                break;
                                            }

                                            if (chtType == "xlBarStacked")
                                            {

                                            }
                                            break;
                                        }

                                        #region #OutputFormatting - Qc4 Changes
                                        bool IsDummyMAMatrix = false;
                                        if (((withBlock1.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent) && ((withBlock1.Question.QuestionType & QuestionType.SA) == QuestionType.SA) && ((chtType == "xlBarClustered") || (chtType == "xlColumnClustered")))
                                            IsDummyMAMatrix = true;
                                        #endregion

                                        if (((withBlock1.Question.QuestionType & QuestionType.SA) == QuestionType.SA) && !IsDummyMAMatrix)
                                        {
                                            // SAマト
                                            if (!IsQCM)
                                            {
                                                switch (chtType)
                                                {
                                                    case "xlLine":
                                                        {
                                                            break;
                                                        }

                                                    case "xlBarStacked100":
                                                        {
                                                            break;
                                                        }

                                                    case "xlColumnStacked100":
                                                        {
                                                            break;
                                                        }

                                                    default:
                                                        {
                                                            goto ExitDo; //Exit Do
                                                        }
                                                }
                                                isQCM = false;
                                                plotby = Output.Orientation == TableOrientation.Portrait ? "xlRows" : "xlColumns";
                                                errdesc = Constants.vbNullString;
                                                CreateChartObject(package, GraphStartCell, PerFirstCol, PerLastCol, PerFirstRow, PerStartCell, ref ChartCount,
                                                PerEndRow, GraphDrawingsPart, HasWeight, tmpGTTable, chtObjs, chtCaption, chtType
                                                , GraphSheet, ref GraphRange, ref NamesArray, ref errdesc, isQCM, plotby, true
                                                , string.Empty, "-1", 50, false, (chtType == "xlLine" ? 100 : 1), chtType == "xlLine" ? @"0""%""" : "0%", HideChartDescriptionMaxPercent: HideChartDescriptionMaxPercent);
                                                chartObj++;
                                                if (tmpChartObject == null)
                                                {
                                                    if (OutputUtil.StrPtr(errdesc) != 0)
                                                    {
                                                        ResumeError = true;
                                                    }

                                                    break;
                                                }

                                                if (chtType == "xlBarStacked100")
                                                {

                                                }
                                                else
                                                    //AdjustOverlap(tmpChartObject);
                                                    ChartObjectCol.Add(tmpChartObject);
                                                break;
                                            }
                                            // QCM
                                            tmpPlotby = "xlRows";
                                            switch (chtType)
                                            {
                                                case "xlPie":
                                                    {
                                                        break;
                                                    }

                                                case "xlBarStacked100":
                                                    {
                                                        // QCM横帯
                                                        tmpPlotby = "xlColumns";
                                                        break;
                                                    }

                                                case "xlColumnStacked100":
                                                    {
                                                        // QCM縦帯
                                                        tmpPlotby = "xlColumns";
                                                        break;
                                                    }

                                                case "xlBarClustered":
                                                    {
                                                        break;
                                                    }

                                                case "xlColumnClustered":
                                                    {
                                                        break;
                                                    }

                                                default:
                                                    {
                                                        goto ExitDo; //Exit Do
                                                    }
                                            }

                                            if (GraphDrawingsPart.WorksheetDrawing == null)
                                                drawingFromRow = "2";
                                            else
                                            {
                                                twoCellAnchor = GTHelper.GetLastCellAnchor(GraphDrawingsPart);
                                                drawingFromRow = (int.Parse(twoCellAnchor.ToMarker.RowId.Text) + 2).ToString();
                                            }
                                            qcmNum = 1;
                                            plotby = Output.Orientation == TableOrientation.Portrait ? tmpPlotby == "xlColumns" ? "xlRows" : "xlColumns" : tmpPlotby;
                                            for (int lRow = (int)(PerEndRow - tmpGTTable.ChildQuestionsCount + 1); lRow <= PerEndRow; lRow++, qcmNum++)
                                            {
                                                WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(package, PerStartCell);
                                                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)lRow);
                                                var cellVal = OpenXmlHelper.GetCell(row, lRow, 3).CellValue;
                                                subTitle = cellVal == null ? "" : cellVal.InnerText;
                                                var val = OpenXmlHelper.GetCell(row, lRow, PerFirstCol - 1).CellValue.InnerText;
                                                nCol.Add(Math.Round(Convert.ToDouble(val)));
                                                isQCM = true;
                                                errdesc = Constants.vbNullString;

                                                CreateChartObject(package, GraphStartCell, PerFirstCol, PerLastCol, PerFirstRow, PerStartCell, ref ChartCount,
                                               lRow, GraphDrawingsPart, HasWeight, tmpGTTable, chtObjs, chtCaption, chtType
                                               , GraphSheet, ref GraphRange, ref NamesArray, ref errdesc, isQCM, plotby, tmpPlotby == "xlColumns"
                                                , subTitle, nCol[1].ToString()
                                                , tmpPlotby == "xlColumns" ? 50 : 40, false
                                                , tmpPlotby == "xlColumns" ? 1 : 100, tmpPlotby == "xlColumns" ? "0%" : @"0""%""", HideChartDescriptionMaxPercent: HideChartDescriptionMaxPercent);

                                                InsertGraphIndex(GraphDrawingsPart, worksheetGraphPart, strIdx + Interaction.IIf(IsQCM, "_" + System.Convert.ToString(qcmNum), ""));
                                                chartObj++;
                                            }
                                            twoCellAnchor = GTHelper.GetLastCellAnchor(GraphDrawingsPart);
                                            drawingToRow = (int.Parse(twoCellAnchor.ToMarker.RowId.Text)).ToString();
                                            GraphRange = "\'" + GraphStartCell + "\'!B" + (int.Parse(drawingFromRow) + 1) + ":" + "B" + drawingToRow;
                                            break;
                                        }
                                        if (!IsQCM)
                                        {
                                            switch (chtType)
                                            {
                                                case "xlBarClustered":
                                                    {
                                                        break;
                                                    }

                                                case "xlColumnClustered":
                                                    {
                                                        break;
                                                    }

                                                case "xlLine":
                                                    {
                                                        break;
                                                    }

                                                default:
                                                    {
                                                        goto ExitDo; //Exit Do
                                                    }
                                            }
                                            plotby = Output.Orientation == TableOrientation.Portrait ? "xlRows" : "xlColumns";
                                            errdesc = Constants.vbNullString;
                                            isQCM = false;
                                            CreateChartObject(package, GraphStartCell, PerFirstCol, PerLastCol, PerFirstRow, PerStartCell, ref ChartCount,
                                               PerEndRow, GraphDrawingsPart, HasWeight, tmpGTTable, chtObjs, chtCaption, chtType
                                               , GraphSheet, ref GraphRange, ref NamesArray, ref errdesc, isQCM, plotby, true, "", "-1", 40, HideChartDescriptionMaxPercent: HideChartDescriptionMaxPercent);
                                            chartObj++;
                                            if (tmpChartObject == null)
                                            {
                                                if (OutputUtil.StrPtr(errdesc) != 0)
                                                {
                                                    ResumeError = true;
                                                }

                                                break;
                                            }
                                            break;
                                        }
                                        // QCM
                                        switch (chtType)
                                        {
                                            case "xlPie":
                                                {
                                                    break;
                                                }

                                            case "xlBarStacked100":
                                                {
                                                    // QCM横帯
                                                    tmpPlotby = "xlColumns";
                                                    break;
                                                }

                                            case "xlColumnStacked100":
                                                {
                                                    // QCM縦帯
                                                    tmpPlotby = "xlColumns";
                                                    break;
                                                }

                                            case "xlBarClustered":
                                                {
                                                    break;
                                                }

                                            case "xlColumnClustered":
                                                {
                                                    break;
                                                }

                                            default:
                                                {
                                                    goto ExitDo; //Exit Do
                                                }
                                        }

                                        if (GraphDrawingsPart.WorksheetDrawing == null)
                                            drawingFromRow = "2";
                                        else
                                        {
                                            twoCellAnchor = GTHelper.GetLastCellAnchor(GraphDrawingsPart);
                                            drawingFromRow = (int.Parse(twoCellAnchor.ToMarker.RowId.Text) + 2).ToString();
                                        }
                                        qcmNum = 1;
                                        plotby = Output.Orientation == TableOrientation.Portrait ? "xlColumns" : "xlRows";
                                        for (int lRow = (int)(PerEndRow - tmpGTTable.ChildQuestionsCount + 1); lRow <= PerEndRow; lRow++, qcmNum++)
                                        {
                                            WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(package, PerStartCell);
                                            Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)lRow);
                                            var cellVal = OpenXmlHelper.GetCell(row, lRow, 3).CellValue;
                                            subTitle = cellVal == null ? "" : cellVal.InnerText;
                                            var val = OpenXmlHelper.GetCell(row, lRow, PerFirstCol - 1).CellValue.InnerText;
                                            nCol.Add(Math.Round(Convert.ToDouble(val)));
                                            isQCM = true;
                                            errdesc = Constants.vbNullString;
                                            CreateChartObject(package, GraphStartCell, PerFirstCol, PerLastCol, PerFirstRow, PerStartCell, ref ChartCount,
                                           lRow, GraphDrawingsPart, HasWeight, tmpGTTable, chtObjs, chtCaption, chtType
                                           , GraphSheet, ref GraphRange, ref NamesArray, ref errdesc, isQCM, plotby, false, subTitle, nCol[qcmNum].ToString(), 40, HideChartDescriptionMaxPercent: HideChartDescriptionMaxPercent);

                                            InsertGraphIndex(GraphDrawingsPart, worksheetGraphPart, strIdx + Interaction.IIf(IsQCM, "_" + System.Convert.ToString(qcmNum), ""));
                                            chartObj++;
                                        }

                                        twoCellAnchor = GTHelper.GetLastCellAnchor(GraphDrawingsPart);
                                        drawingToRow = (int.Parse(twoCellAnchor.ToMarker.RowId.Text)).ToString();
                                        GraphRange = "\'" + GraphStartCell + "\'!B" + (int.Parse(drawingFromRow) + 1) + ":" + "B" + drawingToRow;
                                        break;
                                    }
                                    if ((withBlock1.Question.QuestionType & QuestionType.SA) == QuestionType.SA)
                                    {
                                        // SA
                                        plotby = "xlColumns";
                                        switch (chtType)
                                        {
                                            case "xlPie":
                                                {
                                                    break;
                                                }

                                            case "xlBarStacked100":
                                                {
                                                    // 横帯
                                                    plotby = "xlRows";
                                                    break;
                                                }

                                            case "xlColumnStacked100":
                                                {
                                                    // 縦帯
                                                    plotby = "xlRows";
                                                    break;
                                                }

                                            case "xlBarClustered":
                                                {
                                                    break;
                                                }

                                            case "xlColumnClustered":
                                                {
                                                    break;
                                                }

                                            default:
                                                {
                                                    goto ExitDo; //Exit Do
                                                }
                                        }

                                        errdesc = Constants.vbNullString;
                                        CreateChartObject(package, GraphStartCell, PerFirstCol, PerLastCol, PerFirstRow, PerStartCell, ref ChartCount,
                                          PerEndRow, GraphDrawingsPart, HasWeight,
                                        tmpGTTable, chtObjs, chtCaption, chtType
                                        , GraphSheet, ref GraphRange, ref NamesArray, ref errdesc, false, plotby, plotby == "xlRows", "", nCol[1].ToString()
                                        , plotby == "xlRows" ? 50 : 40, false
                                        , plotby == "xlRows" ? 1 : 100, plotby == "xlRows" ? "0%" : @"0""%""", HideChartDescriptionMaxPercent: HideChartDescriptionMaxPercent);
                                        chartObj++;
                                        if (tmpChartObject == null)
                                        {
                                            if (OutputUtil.StrPtr(errdesc) != 0)
                                            {
                                                ResumeError = true;
                                                //Information.Err().Raise(Constants.vbObjectError + 960 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                            }

                                            break;
                                        }

                                        ChartObjectCol.Add(tmpChartObject);
                                        break;
                                    }
                                    //MA
                                    switch (chtType)
                                    {

                                        case "xlBarClustered":
                                        case "xlColumnClustered":

                                        case "xlPie": // 円 #OutputFormatting - Because of question type change for subtotal
                                        case "xlColumnStacked100":    // 縦帯 #OutputFormatting - Because of question type change for subtotal
                                        case "xlBarStacked100":   // 横帯 #OutputFormatting - Because of question type change for subtotal
                                            {
                                                break;
                                            }

                                        default:
                                            {
                                                goto ExitDo; //Exit Do
                                            }
                                    }
                                    errdesc = Constants.vbNullString;
                                    CreateChartObject(package, GraphStartCell, PerFirstCol, PerLastCol, PerFirstRow, PerStartCell, ref ChartCount,
                                          PerEndRow, GraphDrawingsPart, HasWeight,
                                        tmpGTTable, chtObjs, chtCaption, chtType
                                        , GraphSheet, ref GraphRange, ref NamesArray, ref errdesc, false, "xlColumns", false, "", nCol[1].ToString()
                                        , 40, HideChartDescriptionMaxPercent: HideChartDescriptionMaxPercent);
                                    chartObj++;
                                    if (tmpChartObject == null)
                                    {
                                        if (OutputUtil.StrPtr(errdesc) != 0)
                                        {
                                            ResumeError = true;
                                            //Information.Err().Raise(Constants.vbObjectError + 970 &, RunningProcName, ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportOutputGraphDetailErrorMessageIndex, tmpGTTable.Question.Name, errdesc));
                                        }

                                        break;
                                    }
                                } while (!true);
                            ExitDo:;
                            }
                            if (chartObj > 0)
                            {
                                int qcmNum = chartObj;
                                if (qcmNum < 2)
                                    InsertGraphIndex(GraphDrawingsPart, worksheetGraphPart, strIdx + Interaction.IIf(IsQCM, "_" + System.Convert.ToString(qcmNum), ""));
                                ResumeContinue = true;
                                ResumeContinue = false;
                            }
                            HyperlinkTargetCells.SetValue(GraphRange, i, 8);
                            GraphRange = null;
                        }
                    }
                    currentProgress = UpdProgress;
                    PutContents(package, ContentsSheet, ref ContentsValue, HyperlinkTargetCells);
                    if (PageSetupOn)
                        PutContents(package, PageSetupContentsSheet, ref PageSetupContentsValue, PageSetupHyperlinkTargetCells);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        public void InsertGraphIndex(DrawingsPart drawingsPart, WorksheetPart worksheetPart, string GrphIndx)
        {
            int graphIdxRow;
            string graphIndex = "Graph" + GrphIndx;

            Row lRow = worksheetPart.Worksheet.Descendants<Row>().LastOrDefault();
            if (lRow == null)
                graphIdxRow = 2;
            else
            {
                Xdr.TwoCellAnchor twoCellAnchor = drawingsPart.WorksheetDrawing.Descendants<Xdr.TwoCellAnchor>().LastOrDefault();
                graphIdxRow = int.Parse(twoCellAnchor.FromMarker.RowId.Text);
            }

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
            Row row2 = new Row() { RowIndex = (uint)graphIdxRow, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            Cell cell22 = new Cell() { CellReference = "B" + graphIdxRow, StyleIndex = (UInt32Value)1U, CellValue = new CellValue(graphIndex), DataType = CellValues.String };

            row2.Append(cell22);
            sheetData.Append(row2);
        }

        private void PutContents(SpreadsheetDocument document, string ContentsSheet
  , ref Array ContentsValue, Array HyperlinkTargetCells)
        {
            GTTable tmpTable;
            string buf;
            //Excel.GroupObject g;
            //Excel.TextBox b;
            double r;
            double d;
            //Excel.Shape tmpS;
            //Excel.Shape[] s = null;
            long cnt = 0;
            //long i;
            //long j;
            Array v;
            double h;
            double delH = 0;
            long u;
            string OrgProcName;
            int rowNum = 5;
            int startCell;
            int styleIndex = 0;
            int i, j;
            int[] styleIndexArray = null;
            WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(document, ContentsSheet);
            SheetDimension sheetDimension = worksheetPart.Worksheet.Elements<SheetDimension>().First();
            SheetViews sheetViews = new SheetViews();

            if (CurrentOutput.WBOn && CurrentOutput.ShowPreWBTotal)
            {
                styleIndexArray = new int[]{ 286,287,287,287,287,287,287,289,290,290,290,290,290,290,291,292,293,294,295,296,296,296,296,296,297,298,299,299,299,299,299,300,301,302,302,302,302,302,303,
                                            304,305,306,306,306,306,307,308,309,310,310,310,310,311};
            }
            else
            {
                styleIndexArray = new int[] {344,345,345,345,345,345,345,347,348,348,348,348,348,348,349,350,351,352,353,354,354,354,354,354,355,356,357,357,357,357,357,358,359,360,360,360,360,360,361,
                                            362,363,364,364,364,364,365,366,367,368,368,368,368,369};
            }
            MergeCells mergeCells = new MergeCells();
            MergeCell mergeCell = null;

            var drawingsPart = worksheetPart.AddNewPart<DrawingsPart>("rId1");
            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.PutContents";
            {
                DrawingPart drawing = new DrawingPart();
                var withBlock = ContentsSheet;
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                Row rowA = new Row() { RowIndex = (uint)1, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                Cell cellA1 = new Cell() { CellReference = "A1", StyleIndex = (uint)1U };
                rowA.Append(cellA1);
                sheetData.Append(rowA);

                drawing.GenerateTitleBox(drawingsPart, CurrentOutput.ParentRequest.Title);
                tmpTable = (GTTable)CurrentOutput.Tables[0];
                if (tmpTable.KeyItem != null)
                {
                    rowNum++;
                    v = new string[2, 2];
                    v.SetValue(LocalResource.REPORT_CLASSIFICATION_ITEM_KEYWORD, 0, 0);
                    v.SetValue(LocalResource.REPORT_SECTOR_KEYWORD, 1, 0);
                    {
                        var withBlock1 = tmpTable.KeyItem;
                        v.SetValue(withBlock1.Name + ":" + withBlock1.Description, 0, 1);
                        v.SetValue(withBlock1.SectorNumber + ":" + withBlock1.SectorDescription, 1, 1);
                    }
                    while (rowNum <= 7)
                    {
                        Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                        startCell = 2;
                        while (startCell <= 9)
                        {
                            Cell cell62 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex] };
                            row6.Append(cell62);
                            if (startCell == 3)
                            {
                                startCell++;
                                cell62 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex] };
                                row6.Append(cell62);
                            }
                            startCell++; styleIndex++;
                        }
                        mergeCell = new MergeCell() { Reference = "C" + rowNum + ":I" + rowNum };
                        mergeCells.Append(mergeCell);
                        rowNum++;
                        sheetData.Append(row6);
                    }

                    Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = 6D, CustomHeight = true };
                    sheetData.Append(row8);
                    GTHelper.InsertValues(ref v, document, ContentsSheet, 6, 2);
                }

                {
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
                        var val = System.Text.RegularExpressions.Regex.Unescape(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_CAPTION) + Constants.vbLf + Strings.Join((object[])v, Constants.vbLf);
                        drawing.GenerateSignificanceTestLegend(drawingsPart, val, rowNum);
                    }

                    if (CurrentOutput.MarkingRanking)
                    {
                        drawing.GenerateRankingMarkingLegend(drawingsPart, rowNum);
                    }
                }
                rowNum++;
                buf = CurrentOutput.LocalizedFilteringExpression;
                if (Strings.Len(buf) != 0)
                {
                    v = new string[2];
                    v.SetValue(LocalResource.REPORT_FILTER_CRITERION_KEYWORD, 0);
                    v.SetValue(buf, 1);
                    Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = 66D, CustomHeight = true };
                    Cell cell91 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (uint)styleIndexArray[14], CellValue = new CellValue((string)v.GetValue(0)), DataType = CellValues.String };
                    row9.Append(cell91);
                    Cell cell92 = new Cell() { CellReference = "C" + rowNum, StyleIndex = (uint)styleIndexArray[15], CellValue = new CellValue((string)v.GetValue(1)), DataType = CellValues.String };
                    row9.Append(cell92);
                    sheetData.Append(row9);
                }
                else
                {
                    Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = 66D, CustomHeight = true };
                    sheetData.Append(row9);
                }

                rowNum++;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                Cell cell101 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (uint)styleIndexArray[16], DataType = CellValues.String };
                row10.Append(cell101);
                Cell cell102 = new Cell() { CellReference = "H" + rowNum, StyleIndex = (uint)styleIndexArray[17], DataType = CellValues.String };
                row10.Append(cell102);
                sheetData.Append(row10);

                if (CurrentOutput.WBOn)
                {
                    string wb = LocalResource.WEIGHT_BACK;
                    string msg = wb.Insert(wb.Length - 1, "[" + tmpTable.Question.WBValue + "]") + '\u2009'
                                    + (tmpTable.Question.TabulateFullQuantity ? LocalResource.WB_TOTAL_NUMBER_BASE : string.Empty);
                    Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowNum);
                    Cell cell = OpenXmlHelper.GetCell(row, rowNum, 2);
                    cell.CellValue = new CellValue(msg);
                }
                else if (tmpTable.Question.TabulateFullQuantity)
                {
                    string msg = LocalResource.WB_TOTAL_NUMBER_BASE;
                    Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowNum);
                    Cell cell = OpenXmlHelper.GetCell(row, rowNum, 2);
                    cell.CellValue = new CellValue(msg);
                }

                {
                    if (CurrentOutput.MarkingRanking || CurrentOutput.MarkingColoring || CurrentOutput.MarkingSignificance || CurrentOutput.MarkingAscending || CurrentOutput.SignificanceTest)
                    {
                        if (CurrentOutput.MinSamplesCountOnMarking > 0)
                        {
                            string reportKeyWord = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_PROMPT;
                            Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowNum);
                            Cell cell = OpenXmlHelper.GetCell(row, rowNum, 8);
                            cell.CellValue = new CellValue(reportKeyWord);
                        }
                    }
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
                            Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowNum);
                            Cell cell = OpenXmlHelper.GetCell(row, rowNum, 8);
                            cell.CellValue = new CellValue(msg);
                        }
                    }
                }

                rowNum++; styleIndex = 18; int limit = rowNum + 1;
                while (rowNum <= limit)
                {
                    Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                    startCell = 2;
                    while (startCell <= 9)
                    {
                        Cell cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                        row11.Append(cell112);
                        if (startCell == 3)
                        {
                            startCell++;
                            cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                            row11.Append(cell112);
                        }
                        styleIndex++; startCell++;
                    }
                    sheetData.Append(row11);
                    rowNum++;
                }
                startCell = 2;
                while (startCell <= 9)
                {
                    mergeCell = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + (limit - 1) + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + limit };
                    mergeCells.Append(mergeCell); startCell++;
                }

                v = Array.CreateInstance(typeof(object),
                new int[] { Information.UBound(ContentsValue, 2) },
                new int[] { 1 });
                v.SetValue(LocalResource.REPORT_LAYOUT_QUESTION_NUMBER_COLUMN_CAPTION, 1);
                v.SetValue(LocalResource.REPORT_LAYOUT_TABLE_HEADING_COLUMN_CAPTION, 2);
                v.SetValue(LocalResource.REPORT_LAYOUT_QC3_DESCRIPTION_2COLUMN_CAPTION, 3);
                v.SetValue(LocalResource.REPORT_NP_TABLE_KEYWORD, 4);
                v.SetValue(LocalResource.REPORT_N_TABLE_KEYWORD, 5);
                v.SetValue(LocalResource.REPORT_P_TABLE_KEYWORD, 6);
                v.SetValue(LocalResource.REPORT_SIGNIFICANCE_TEST_KEYWORD, 7);
                v.SetValue(LocalResource.REPORT_GRAPH_KEYWORD, 8);

                for (int col = 2; col <= 9; col++)
                {
                    Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)limit - 1);
                    Cell cell = OpenXmlHelper.GetCell(row, limit - 1, col);
                    //cell.CellValue = new CellValue((string)v.GetValue((col - 1)));
                    CellValue data = new CellValue((string)v.GetValue((col - 1)))
                    {
                        Space = SpaceProcessingModeValues.Preserve
                    };
                    cell.CellValue = data;
                }

                startCell = 2;
                Row row13 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = 3D, CustomHeight = true };
                while (startCell <= 9)
                {
                    Cell cell132 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                    row13.Append(cell132);
                    if (startCell == 3)
                    {
                        startCell++;
                        cell132 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                        row13.Append(cell132);
                    }
                    styleIndex++; startCell++;
                }
                sheetData.Append(row13);

                if (mergeCell != null)
                    worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);

                int numRows = Information.UBound(HyperlinkTargetCells, 1);
                int cnts = 1, styIdx;
                startCell = 2; rowNum++; limit = rowNum;

                if (numRows > 1)
                {
                    while (cnts < numRows)
                    {
                        styIdx = styleIndex;
                        Row row14 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                        while (startCell <= 9)
                        {
                            Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styIdx], DataType = CellValues.String };
                            row14.Append(cell);
                            if (startCell == 3)
                            {
                                startCell++;
                                cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styIdx], DataType = CellValues.String };
                                row14.Append(cell);
                            }

                            styIdx++; startCell++;
                        }
                        startCell = 2; rowNum++; cnts++;
                        sheetData.Append(row14);
                    }
                    styleIndex += 7;
                    Row row15 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                    while (startCell <= 9)
                    {
                        Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                        row15.Append(cell);
                        if (startCell == 3)
                        {
                            startCell++;
                            cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                            row15.Append(cell);
                        }
                        styleIndex++; startCell++;
                    }
                    sheetData.Append(row15);
                }
                else
                {
                    styleIndex += 7; startCell = 2;
                    Row row15 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                    while (startCell <= 9)
                    {
                        Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                        row15.Append(cell);
                        if (startCell == 3)
                        {
                            startCell++;
                            cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                            row15.Append(cell);
                        }
                        styleIndex++; startCell++;
                    }
                    sheetData.Append(row15);
                }

                {
                    int sRow = limit;
                    Hyperlinks hyperlinks = worksheetPart.Worksheet.Descendants<Hyperlinks>().FirstOrDefault(); ;// new Hyperlinks();

                    for (i = 1; i <= Information.UBound(ContentsValue, 1); i++, sRow++)
                    {
                        for (int col = 2; col <= 4; col++)
                        {
                            Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                            Cell cell = OpenXmlHelper.GetCell(row, sRow, col);
                            //cell.CellValue = new CellValue((string)ContentsValue.GetValue(i, (col-1)));
                            CellValue data = new CellValue((string)ContentsValue.GetValue(i, (col - 1)))
                            {
                                Space = SpaceProcessingModeValues.Preserve
                            };
                            cell.CellValue = data;
                        }
                    }
                    sRow = limit;
                    int numberOfhyperLink = 0;
                    for (i = 1; i <= Information.UBound(ContentsValue, 1); i++, sRow++)
                    {
                        for (j = Information.LBound(HyperlinkTargetCells, 2); j <= Information.UBound(HyperlinkTargetCells, 2); j++)
                        {
                            if (!(HyperlinkTargetCells.GetValue(i, j) == null))
                            {
                                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                                Cell cell = OpenXmlHelper.GetCell(row, sRow, (j + 1));
                                //cell.CellValue = new CellValue((string)ContentsValue.GetValue(i, j));
                                CellValue data = new CellValue((string)ContentsValue.GetValue(i, j))
                                {
                                    Space = SpaceProcessingModeValues.Preserve
                                };
                                cell.CellValue = data;
                                if (numberOfhyperLink < 64530)
                                {
                                    Hyperlink hyperlink = new Hyperlink() { Reference = cell.CellReference, Location = (string)HyperlinkTargetCells.GetValue(i, j), Display = (string)ContentsValue.GetValue(i, j) };
                                    hyperlinks.Append(hyperlink);
                                    numberOfhyperLink++;
                                }
                            }
                        }
                    }
                    SheetView sheetView = new SheetView() { ShowGridLines = false, TabSelected = true, WorkbookViewId = (UInt32Value)0U };
                    Pane pane = new Pane() { VerticalSplit = limit - 1, TopLeftCell = "A" + limit, ActivePane = PaneValues.BottomLeft, State = PaneStateValues.Frozen };
                    Selection selection = new Selection() { Pane = PaneValues.BottomLeft, ActiveCell = "A1", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1" } };

                    sheetView.Append(pane);
                    sheetView.Append(selection);
                    sheetViews.Append(sheetView);
                    worksheetPart.Worksheet.InsertAfter(sheetViews, sheetDimension);
                    //worksheetPart.Worksheet.InsertAfter(hyperlinks, mergeCells);
                    //withBlock1.EntireRow.AutoFit();
                }
            }
            RunningProcName = OrgProcName;
        }
        private void OutputData(SpreadsheetDocument package, OutputGT output, bool HasWeightColumn, GTTable Table
        , ref Array TableStringValue, ref Array DataValue, ref Array Ranking
        , long SectorsCount, long ChildQuestionsCount
        , TableOrientation Orientation, string FormattedIndex
        , bool CutNA, bool CutIV, bool CutPreWB, bool HasWeight
        , bool UseWeightFormat
        , QuestionType QuestionType, TableType TableType
        , string FormatSheet, ref string TableRange, ref string StartCell, ref string PageSetupStartCell
        , bool IsLastTable
        , ref long RemainedPageSetupRowsCount
        , bool IsReport = false, bool CutMedian = false
        , bool HasLetterColumn = false
        , bool NotRevise = false, Array OptionNumbers = null, Array OptionNumbersTop = null)
        {
            string TableCaption = string.Empty;
            string GraphCaption;

            string RangeNameLeftPart = string.Empty;
            string RangeNameRightPart = string.Empty;
            string BaseRangeName;
            string FormatRangeName;
            bool isMatrix;
            bool isN;
            bool CutWT = false;
            long WorkRowsCount;
            long rs;
            long cs;
            long rc;
            long cc;
            long d;
            long r;
            long c;
            long clr;
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
            //Excel.Worksheet PageSetupSheet;
            long i;

            long CutColumnsCount = 0;
            bool OnlyCutWTColumn = false;
            bool IsSigTest;
            long diff;
            bool CutLetterRow = false;
            bool hasSubTotalCount = false;
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
            WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(package, StartCell);

            if (Orientation == TableOrientation.Landscape)
            {
                if (!isN)
                {
                    HasNA = CurrentOutput.ShowNAAtItem & !CutNA;
                    HasIV = CurrentOutput.ShowIVAtItem & !CutIV;
                    SecColsCount = SectorsCount + (CrossCreator.ToInt(HasNA) & 1) + (CrossCreator.ToInt(HasIV) & 1) + (CrossCreator.ToInt(HasWeight) & 2);
                }
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

            if (isMatrix)
            {
                CutLetterRow = IsSigTest;
                {
                    //var withBlock1 = FormatRange;
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


                    }
                    if (Orientation == TableOrientation.Portrait)
                    {

                        if (!(PageSetupStartCell == null))
                        {
                            MaxSectorsCountPerPage = MaxColumnsCountPerPage - FixedColumnIndex;
                            PagesCount = (ChildQuestionsCount - 1) / MaxSectorsCountPerPage + 1;
                            LastPageColumnsCount = (ChildQuestionsCount - 1) % MaxSectorsCountPerPage + 1;
                        }
                    }
                    else
                    {
                        if (!isN)
                        {
                            if (!(PageSetupStartCell == null))
                            {
                                MaxSectorsCountPerPage = MaxColumnsCountPerPage - FixedColumnIndex;
                                PagesCount = (SecColsCount - 1) / MaxSectorsCountPerPage + 1;
                                LastPageColumnsCount = (SecColsCount - 1) % MaxSectorsCountPerPage + 1;
                            }
                        }
                    }
                }
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        if (!isN)
                        {

                        }
                    }
                }
                if (ChildQuestionsCount > 3)
                {

                }
                else if (ChildQuestionsCount < 3)
                {

                }
            }

            long withBlockSubT = 0;
            if (Table.Question.SubTotalCnt > 0)
            {
                if (!isMatrix)
                {
                    hasSubTotalCount = true;
                    withBlockSubT = Table.Question.SubTotalCnt + (HasWeight ? 2 : 0) * d;//Total row count -
                }
                else
                {
                    hasSubTotalCount = true;
                    withBlockSubT = Table.Question.SubTotalCnt + (HasWeight ? 2 : 0);//Total coloum count -
                }
            }

            Row lRow = worksheetPart.Worksheet.Descendants<Row>().LastOrDefault();
            int sRow = lRow == null ? 2 : ((int)lRow.RowIndex.Value + 1);

            if (output.WBOn && !CutPreWB)
            {
                if (FormatSheet == Standard)
                {
                    switch (FormatRangeName)
                    {
                        case SA_MA_NP:
                            createFormatWBTables.CreateNperTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, withBlockSubT);
                            break;
                        case SA_MA_N:
                            createFormatWBTables.CreateNTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, withBlockSubT);
                            break;
                        case SA_MA_P:
                            createFormatWBTables.CreatePTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, withBlockSubT);
                            break;
                        case SAM_MAM_NP:
                            createFormatWBTables.CreateMatrixNPTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, ChildQuestionsCount, withBlockSubT);
                            break;
                        case SAM_MAM_N:
                            createFormatWBTables.CreateMatrixNTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, ChildQuestionsCount, withBlockSubT);
                            break;
                        case SAM_MAM_P:
                            createFormatWBTables.CreateMatrixPTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, ChildQuestionsCount, withBlockSubT);
                            break;
                        case N:
                            createFormatWBTables.CreateNumericTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, CutMedian);
                            break;
                        case NM:
                            createFormatWBTables.CreateNumericMatrixTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, ChildQuestionsCount, CutMedian);
                            break;
                    }
                }
                else if (FormatSheet == Weight)
                {
                    switch (FormatRangeName)
                    {
                        case SA_MA_NP:
                            createFormatWBTables.CreateNperWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, HasWeightColumn, withBlockSubT);
                            break;
                        case SA_MA_N:
                            createFormatWBTables.CreateNWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, HasWeightColumn, withBlockSubT);
                            break;
                        case SA_MA_P:
                            createFormatWBTables.CreatePWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, HasWeightColumn, withBlockSubT);
                            break;
                        case SAM_MAM_NP:
                            createFormatWBTables.CreateMatrixNPWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, ChildQuestionsCount, HasWeightColumn, withBlockSubT);
                            break;
                        case SAM_MAM_N:
                            createFormatWBTables.CreateMatrixNWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, ChildQuestionsCount, HasWeightColumn, withBlockSubT);
                            break;
                        case SAM_MAM_P:
                            createFormatWBTables.CreateMatrixPWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, ChildQuestionsCount, HasWeightColumn, withBlockSubT);
                            break;
                        case N:
                            createFormatWBTables.CreateNumericWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, HasWeightColumn, CutMedian);
                            break;
                        case NM:
                            createFormatWBTables.CreateNumericMatrixWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, ChildQuestionsCount, HasWeightColumn, CutMedian);
                            break;
                    }
                }
                else if (FormatSheet == SignificanceTest)
                {
                    switch (FormatRangeName)
                    {
                        case SA_MA_P:
                            createFormatWBTables.CreatePSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, withBlockSubT);
                            break;
                        case SAM_MAM_P:
                            createFormatWBTables.CreateMatrixPSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, ChildQuestionsCount, HasLetterColumn, CutLetterRow, withBlockSubT, HasWeightColumn);
                            break;
                        case NM:
                            createFormatWBTables.CreateNumericMatrixSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, ChildQuestionsCount, CutMedian);
                            break;
                    }
                }
                else if (FormatSheet == Hybrid)
                {
                    switch (FormatRangeName)
                    {
                        case SA_MA_P:
                            createFormatWBTables.CreatePHybridSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, HasWeightColumn, withBlockSubT);
                            break;
                        case SAM_MAM_P:
                            createFormatWBTables.CreateMatrixPHybridSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, ChildQuestionsCount, HasLetterColumn, CutLetterRow, HasWeightColumn, withBlockSubT);
                            break;
                        case NM:
                            createFormatWBTables.CreateNumericMatrixHybridSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, ChildQuestionsCount, HasWeightColumn, CutMedian);
                            break;
                    }
                }
            }
            else
            {
                if (FormatSheet == Standard)
                {
                    switch (FormatRangeName)
                    {
                        case SA_MA_NP:
                            createFormatTables.CreateNperTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, withBlockSubT);
                            break;
                        case SA_MA_N:
                            createFormatTables.CreateNTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, withBlockSubT);
                            break;
                        case SA_MA_P:
                            createFormatTables.CreatePTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, withBlockSubT);
                            break;
                        case SAM_MAM_NP:
                            createFormatTables.CreateMatrixNPTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, ChildQuestionsCount, withBlockSubT);
                            break;
                        case SAM_MAM_N:
                            createFormatTables.CreateMatrixNTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, ChildQuestionsCount, withBlockSubT);
                            break;
                        case SAM_MAM_P:
                            createFormatTables.CreateMatrixPTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, ChildQuestionsCount, withBlockSubT);
                            break;
                        case N:
                            createFormatTables.CreateNumericTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, CutMedian);
                            break;
                        case NM:
                            createFormatTables.CreateNumericMatrixTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, ChildQuestionsCount, CutMedian);
                            break;
                    }
                }
                else if (FormatSheet == Weight)
                {
                    switch (FormatRangeName)
                    {
                        case SA_MA_NP:
                            createFormatTables.CreateNperWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, HasWeightColumn, withBlockSubT);
                            break;
                        case SA_MA_N:
                            createFormatTables.CreateNWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, HasWeightColumn, withBlockSubT);
                            break;
                        case SA_MA_P:
                            createFormatTables.CreatePWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, HasWeightColumn, withBlockSubT);
                            break;
                        case SAM_MAM_NP:
                            createFormatTables.CreateMatrixNPWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, ChildQuestionsCount, HasWeightColumn, withBlockSubT);
                            break;
                        case SAM_MAM_N:
                            createFormatTables.CreateMatrixNWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, ChildQuestionsCount, HasWeightColumn, withBlockSubT);
                            break;
                        case SAM_MAM_P:
                            createFormatTables.CreateMatrixPWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, ChildQuestionsCount, HasWeightColumn, withBlockSubT);
                            break;
                        case N:
                            createFormatTables.CreateNumericWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, HasWeightColumn, CutMedian);
                            break;
                        case NM:
                            createFormatTables.CreateNumericMatrixWTTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, ChildQuestionsCount, HasWeightColumn, CutMedian);
                            break;
                    }
                }
                else if (FormatSheet == SignificanceTest)
                {
                    switch (FormatRangeName)
                    {
                        case SA_MA_P:
                            createFormatTables.CreatePSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, withBlockSubT);
                            break;
                        case SAM_MAM_P:
                            createFormatTables.CreateMatrixPSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, ChildQuestionsCount, HasLetterColumn, CutLetterRow, withBlockSubT, HasWeightColumn);
                            break;
                        case NM:
                            createFormatTables.CreateNumericMatrixSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, ChildQuestionsCount, CutMedian);
                            break;
                    }
                }
                else if (FormatSheet == Hybrid)
                {
                    switch (FormatRangeName)
                    {
                        case SA_MA_P:
                            createFormatTables.CreatePHybridSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, HasWeightColumn, withBlockSubT);
                            break;
                        case SAM_MAM_P:
                            createFormatTables.CreateMatrixPHybridSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, CutWT, SectorsCount, d, sRow, ChildQuestionsCount, HasLetterColumn, CutLetterRow, HasWeightColumn, withBlockSubT);
                            break;
                        case NM:
                            createFormatTables.CreateNumericMatrixHybridSigTable(worksheetPart, FormatSheet, Orientation, CutIV, CutNA, SectorsCount, d, sRow, output, ChildQuestionsCount, HasWeightColumn, CutMedian);
                            break;
                    }
                }
            }
            {
                int row = Information.LBound(TableStringValue, 1) + sRow;
                int fCol = Information.LBound(TableStringValue, 2) + 1;
                GTHelper.InsertValues(ref TableStringValue, package, StartCell, row, fCol);
                Row row1 = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                Cell cell = OpenXmlHelper.GetCell(row1, sRow, 2);
                cell.CellValue = new CellValue(TableCaption);
                cell.DataType = CellValues.String;

                row = Information.LBound(DataValue, 1) + sRow;
                fCol = Information.LBound(DataValue, 2) + 1;
                GTHelper.InsertValues(ref DataValue, package, StartCell, row, fCol);
            }
            {
                int fRow = Information.LBound(TableStringValue, 1) + (sRow + 2);
                int lstRow = (int)worksheetPart.Worksheet.Descendants<Row>().LastOrDefault().RowIndex.Value - 1;
                int lCol = Information.UBound(TableStringValue, 2) + 1;
                TableRange = "\'" + StartCell + "\'!B" + fRow + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lCol) + lstRow;

                if (isMatrix || isN)
                {
                    int styleIdx = (CurrentOutput.WBOn && CurrentOutput.ShowPreWBTotal) ? 322 : 378;
                    GTHelper.DrawEdgeLeft(worksheetPart, fRow, lstRow, lCol, styleIdx);
                }
            }
            if (!IsReport)
            {

                try
                {
                    Row row = null;
                    int rowNum = sRow + 4;
                    int fCol = Information.LBound(TableStringValue, 2) + 1;
                    int lCol = Information.UBound(TableStringValue, 2) + 1;
                    double nHt = isN ? NumericLabelRowHt : 0;
                    if (isMatrix)
                    {
                        //AutoFitGTLabelTop                      
                        row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowNum);
                        OpenXmlHelper.AutofitRow(worksheetPart, row, fCol, lCol, NumericRowHt: nHt);
                    }
                    else if (isN)
                    {
                        rowNum = rowNum - 1;
                        row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowNum);
                        OpenXmlHelper.AutofitRow(worksheetPart, row, fCol, lCol, NumericRowHt: nHt);
                    }
                    if (isN && NumericLabelRowHt == 0) { NumericLabelRowHt = Convert.ToDouble(row.Height.InnerText); }
                    //AutoFitDescCol
                    int add1 = isN && !isMatrix ? 1 : 0;
                    row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)((rowNum - 3) + add1));
                    OpenXmlHelper.AutofitRow(worksheetPart, row, 2, 2, true);

                    //AutoFitGTLabelThirdColums
                    int lstRow = (int)worksheetPart.Worksheet.Descendants<Row>().LastOrDefault().RowIndex.Value - 1;
                    for (int fRow = rowNum - 1; fRow <= lstRow; fRow++)
                    {
                        row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)fRow);
                        OpenXmlHelper.AutofitRow(worksheetPart, row, 3, 3);
                    }
                }
                catch (Exception e)
                {
                    _log.Error(e.Message + "\n" + e.StackTrace);
                }
            }
            if (!IsSigTest)
            {
                int fCol = Information.LBound(DataValue, 2) + 1;
                int lCol = Information.UBound(DataValue, 2) + 1;
                int fRow = Information.LBound(DataValue, 1) + sRow + (isMatrix || isN ? 0 : 1);
                int lstRow = (int)worksheetPart.Worksheet.GetFirstChild<SheetData>().Elements<Row>().LastOrDefault().RowIndex.Value - 1;
                OpenXmlHelper.AutoFitColumn(package.WorkbookPart, worksheetPart, fCol, lCol, fRow, lstRow);
            }
        }

        private void AdjustLandscapeFormat(SpreadsheetDocument document, OutputGT output, string FormatSheet
       , bool CutPreWB, bool HasWeightColumn, bool UseWeightFormat
       , bool IsReport = false)
        {
            string[] tmpNamesArray = null;
            //object tmpName;
            long tmp;
            string tmpSuffix;
            string fmt = null;
            //XlDeleteShiftDirection tmpDelShift;
            bool IsWeight = false;
            string OrgProcName;
            int formatId = 172;
            CellFormat cellFormat = null;
            NumberingFormat numberingFormat = null;
            NumberingFormats numberingFormats = document.WorkbookPart.WorkbookStylesPart.Stylesheet.NumberingFormats;
            CellFormats cellFormats = document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats;
            //OrgProcName = RunningProcName;
            //RunningProcName = "GTCreator.AdjustLandscapeFormat";
            // フォーマットシートのWB前全体行またはWB前全体列の削除
            if (CutPreWB)
            {
                if (IsReport)
                {

                }
                else
                {

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

                //NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);

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
                // NumFormat(ref FormatSheet, ref SigTestFormatSheet, ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);
            }
            // フォーマットシートの数値回答集計表フォーマットの調整
            TotalColumnIndex = (FormatSheet == "Standard" ? (5 - 1 - 1) : FormatSheet == "Hybrid" ? (7 - 1 - 1) : (6 - 1 - 1));
            AverageColumnIndex = 0;
            tmpNamesArray = Strings.Split("N NM");
            //tmpDelShift = XlDeleteShiftDirection.xlShiftToLeft;
            if (CurrentOutput.ParentRequest.ShowMedian)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Median);
                tmpSuffix = "_Median";
                fmt = NumFormat(ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);

                cellFormat = (CellFormat)cellFormats.ElementAt(output.WBOn ? 321 : 376);
                numberingFormat = new NumberingFormat() { NumberFormatId = (uint)formatId, FormatCode = fmt };
                numberingFormats.Append(numberingFormat);
                cellFormat.NumberFormatId = (uint)formatId;
                formatId++;
            }
            else
            {

            }
            if (CurrentOutput.ParentRequest.ShowMaximum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Maximum);
                tmpSuffix = "_Maximum";
                fmt = NumFormat(ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);

                cellFormat = (CellFormat)cellFormats.ElementAt(output.WBOn ? 320 : 375);
                numberingFormat = new NumberingFormat() { NumberFormatId = (uint)formatId, FormatCode = fmt };
                numberingFormats.Append(numberingFormat);
                cellFormat.NumberFormatId = (uint)formatId;
                formatId++;
            }
            else
            {

            }
            if (CurrentOutput.ParentRequest.ShowMinimum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Minimum);
                tmpSuffix = "_Minimum";
                fmt = NumFormat(ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);

                cellFormat = (CellFormat)cellFormats.ElementAt(output.WBOn ? 319 : 374);
                numberingFormat = new NumberingFormat() { NumberFormatId = (uint)formatId, FormatCode = fmt };
                numberingFormats.Append(numberingFormat);
                cellFormat.NumberFormatId = (uint)formatId;
                formatId++;
            }
            else
            {

            }
            if (CurrentOutput.ParentRequest.ShowStdev)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Stdev);
                tmpSuffix = "_Deviation";
                fmt = NumFormat(ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);

                cellFormat = (CellFormat)cellFormats.ElementAt(output.WBOn ? 318 : 373);
                numberingFormat = new NumberingFormat() { NumberFormatId = (uint)formatId, FormatCode = fmt };
                numberingFormats.Append(numberingFormat);
                cellFormat.NumberFormatId = (uint)formatId;
                formatId++;
            }
            else
            {

            }
            if (CurrentOutput.ParentRequest.ShowAverage)
            {
                AverageColumnIndex = (FormatSheet == "Standard" ? (7 - 1) : FormatSheet == "Hybrid" ? (9 - 1) : (8 - 1));
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Average);
                tmpSuffix = "_Average";
                fmt = NumFormat(ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);

                cellFormat = (CellFormat)cellFormats.ElementAt(output.WBOn ? 317 : 372);
                numberingFormat = new NumberingFormat() { NumberFormatId = (uint)formatId, FormatCode = fmt };
                numberingFormats.Append(numberingFormat);
                cellFormat.NumberFormatId = (uint)formatId;
                formatId++;
            }
            else
            {

            }
            if (CurrentOutput.ParentRequest.ShowSummary)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Summary);
                tmpSuffix = "_Summary";
                fmt = NumFormat(ref fmt, tmp, IsWeight, tmpNamesArray, tmpSuffix);

                cellFormat = (CellFormat)cellFormats.ElementAt(output.WBOn ? 316 : 371);
                numberingFormat = new NumberingFormat() { NumberFormatId = (uint)formatId, FormatCode = fmt };
                numberingFormats.Append(numberingFormat);
                cellFormat.NumberFormatId = (uint)formatId;
                formatId++;
            }
            else
            {

            }
            if (!CurrentOutput.ParentRequest.ShowParameter)
            {

            }
            return;
        }

        private string NumFormat(ref string fmt, long tmp, bool IsWeight, string[] tmpNamesArray, string tmpSuffix)
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
            return fmt;
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
                string QuestionDescription = String.Empty;
                if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    if (!String.IsNullOrEmpty(Table.Question.TableHeading))
                    {
                        QuestionDescription = Table.Question.TableHeading;
                    }
                }
                else if (String.IsNullOrEmpty(Table.Question.TableHeading))
                {
                    QuestionDescription = Table.Question.Description != null ? Table.Question.Description : string.Empty;
                }
                else
                {
                    QuestionDescription = Table.Question.TableHeading + (Table.Question.Description != null ? " \n【" + Table.Question.Description + "】" : string.Empty);
                }
                TableStringValue.SetValue(Table.Question.Name + Space(1) + QuestionDescription, 1, 1);
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
                string QuestionDescription = String.Empty;
                if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    if (!String.IsNullOrEmpty(Table.Question.TableHeading))
                    {
                        QuestionDescription = Table.Question.TableHeading;
                    }
                }
                else if (String.IsNullOrEmpty(Table.Question.TableHeading))
                {
                    QuestionDescription = Table.Question.Description != null ? Table.Question.Description : string.Empty;
                }
                else
                {
                    QuestionDescription = Table.Question.TableHeading + (Table.Question.Description != null ? " \n【" + Table.Question.Description + "】" : string.Empty);
                }
                TableStringValue.SetValue(Table.Question.Name + Space(1) + QuestionDescription, 1, 1);
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
                string QuestionDescription = String.Empty;
                if (String.IsNullOrEmpty(Table.Question.TableHeading))
                {
                    QuestionDescription = Table.Question.Description != null ? Table.Question.Description : string.Empty;
                }
                else
                {
                    QuestionDescription = Table.Question.TableHeading + (Table.Question.Description != null ? " \n【" + Table.Question.Description + "】" : string.Empty);
                }
                TableStringValue.SetValue(Table.Question.Name + Space(1) + QuestionDescription, 1, 1);
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
        private void CreateGTSub(SpreadsheetDocument package, OutputGT Output, List<GTTable> Tables
                            , ref bool HasWeight, ref bool HasWeightColumn, ref bool UseWeightFormat
                            , ref bool isMatrix, ref bool isN, ref bool CreateGraph
                            , ref string FormatSheet
                            , ref bool RunSignificanceTest, ref bool HasMatrixBetweenChildSigTest, ref bool HasMatrixBetweenSectorSigTest, ref bool HasNormalSigTest
                            , bool PageSetupOn, ref bool SigTestPageSetupOn
                            , ref string ContentsSheet, ref string TemplateSheet
                            , ref string SigTestTemplateSheet, ref string PageSetupContentsSheet
                            , ref string PageSetupTemplateSheet, ref string PageSetupSigTestTemplateSheet
                            , ref string GraphSheet)
        {
            GTTable tmpGTTable;
            string SigTestTemplateSheetName = null;
            string tmpSheetName = null;
            string PageSetupSheetBaseName = null;


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
                                FormatSheet = "Weight"; //Commented on 09-9-2019 as a part of #OutputFormatting
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
                                CreateGraph = withBlock1.Chart.ChartType != 0;
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
                //var withBlock = NewBook.Worksheets;

                ContentsSheet = "INDEX";
                if (HasWeightColumn)
                {
                    TemplateSheet = "WT";
                    if (RunSignificanceTest)
                        SigTestTemplateSheetName = "WT_SignificanceTest";
                }
                else
                {
                    TemplateSheet = "Standard";
                    if (RunSignificanceTest)
                        SigTestTemplateSheetName = "SignificanceTest";
                }
                if (RunSignificanceTest)
                {
                    if (OutputUtil.StrPtr(SigTestTemplateSheetName) != 0)
                    {
                        SigTestTemplateSheet = SigTestTemplateSheetName;

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
                            SigTestTemplateSheetName = TemplateSheet;
                            SigTestTemplateSheet = SigTestTemplateSheetName;
                        }
                    }
                }
                if (PageSetupOn)
                {
                    string reportKeyWord = LocalResource.REPORT_GT_PAGE_SETUP_SHEET_SUFFIX;
                    PageSetupContentsSheet = ContentsSheet + reportKeyWord;
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
                    PageSetupTemplateSheet = (TemplateSheet == "Standard" ? "" : TemplateSheet) + PageSetupSheetBaseName;
                    MaxRowsCountPerPage = 1;
                    MaxColumnsCountPerPage = 1;
                    DefHeight = 1;

                    if (SigTestPageSetupOn)
                    {
                        if (SigTestTemplateSheetName == TemplateSheet)
                        {
                            PageSetupSigTestTemplateSheet = tmpSheetName + PageSetupSheetBaseName;
                        }
                        else
                        {
                            PageSetupSigTestTemplateSheet = SigTestTemplateSheetName + PageSetupSheetBaseName;
                        }
                        MaxColumnsCountPerPage = 1;
                    }
                }

                // TEMP GWS diagnostics: do not create Graph sheet / drawings / charts
                if (DisableGraphsForGwsDiagnostics)
                {
                    CreateGraph = false;
                    GraphSheet = null;
                }

                if (CreateGraph) GraphSheet = "Graph";

                // 不要シートを削除
                tmpDic = new Dictionary<string, string>();

                if (!(ContentsSheet == null))
                    tmpDic.Add(ContentsSheet, string.Empty);
                if (!(TemplateSheet == null))
                    tmpDic.Add(TemplateSheet, string.Empty);
                if (!(SigTestTemplateSheet == null) && SigTestTemplateSheet != "Standard" && SigTestTemplateSheet != "WT")
                    tmpDic.Add(SigTestTemplateSheet, string.Empty);
                if (!(PageSetupTemplateSheet == null))
                    tmpDic.Add(PageSetupTemplateSheet, string.Empty);
                if (!(PageSetupContentsSheet == null))
                    tmpDic.Add(PageSetupContentsSheet, string.Empty);
                if (!(PageSetupSigTestTemplateSheet == null))
                    tmpDic.Add(PageSetupSigTestTemplateSheet, string.Empty);
                if (!(GraphSheet == null))
                    tmpDic.Add(GraphSheet, string.Empty);

                NewBook newBook = new NewBook();
                newBook.CreateWorkbookPart(package.AddWorkbookPart(), tmpDic, Output);

            }
        }
        private void CreateChartObject(SpreadsheetDocument document, string GraphStartCell, int perFirstCol, int PerLastCol, int perStartRow, string perSheetName, ref int ChartCount,
        int PerEndRow, DrawingsPart DrawingsPart, bool HasWeight, GTTable Table
        , string ChartObjs
       , string ChartCaption, string ChartType
       , string GraphSheet, ref string GraphRange
       , ref string[] NamesArray
       , ref string ErrorDesc, bool isQCM = false
       , string plotby = "", bool HasLegend = false
       , string SubTitle = ""
       , string num = "-1"
       , long GapWidth = 50
       , bool isNumeric = false
       , double ScaleNumber = 100
       , string ValueNumberFormat = "0\"%\""
       , bool IsReport = false
       , bool SetXValues = false
       , double HideChartDescriptionMaxPercent = -1
       )
        {
            // TEMP GWS diagnostics: skip creating any chart/drawing objects
            if (DisableGraphsForGwsDiagnostics)
                return;

            int chartAreaHeight;
            int rowCount = 0;

            string lineColour = null;
            string CreateChartObject = null;
            string NumberFormat = "";
            string drawingFromRow;
            string drawingToRow;
            string OrgProcName;
            string LegendSeriesText = null;

            bool reverseOn = false;
            bool HasLeaderLines = false;
            bool HasSeriesLines = false;
            bool HideZero = false;
            bool isMatrix = false;

            MsoGradientStyle GradStyle;
            Array v = null;
            ChartPart chartPartObject = null;

            OrgProcName = RunningProcName;
            RunningProcName = "GTCreator.CreateChartObject";
            GradStyle = Table.Chart.GradientStyle;

            DrawingPart drawing = new DrawingPart();
            isMatrix = (Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent ? true : false;
            WorksheetPart worksheetPerPart = OpenXmlHelper.GetWorksheetPartByName(document, perSheetName);

            if (isMatrix && isQCM)
                rowCount = PerLastCol - perFirstCol;
            else if (isMatrix && ChartType == "xlBarStacked")
                rowCount = (PerLastCol - Table.Question.SubTotalCnt) - (perFirstCol - 1);
            else if (isMatrix || isNumeric)
                rowCount = PerEndRow - (PerEndRow - Table.ChildQuestionsCount);
            else
                rowCount = ChartType == "xlBarStacked100" ? 1 : PerEndRow - perStartRow;

            if (DrawingsPart.WorksheetDrawing == null)
            {
                drawingFromRow = "2";
                drawingToRow = "30";
            }
            else
            {
                Xdr.TwoCellAnchor twoCellAnchor = GTHelper.GetLastCellAnchor(DrawingsPart);
                drawingFromRow = (int.Parse(twoCellAnchor.ToMarker.RowId.Text) + 3).ToString();
                drawingToRow = (int.Parse(drawingFromRow) + 28).ToString();
            }

            NumberFormat = "0.0" + ((Table.Chart.ChartType & XlChartType.RAT) == XlChartType.RAT ? "" : "\"%\"");
            if (IsReport)
            {
                lineColour = "";
            }
            else
            {
                var rgb = Util.Constants.GT.GTGraphBorder; //LINE_COLOR
                lineColour = rgb.R.ToString("X2") + rgb.G.ToString("X2") + rgb.B.ToString("X2");
            }

            switch (ChartType)
            {
                case "xlPie":
                    {
                        double HideChartDescriptionMaxPercentValue = (Table.Chart.ChartType & XlChartType.RAT) == XlChartType.RAT ? Convert.ToDouble(HideChartDescriptionMaxPercent / 10) : HideChartDescriptionMaxPercent;
                        isMatrix = ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent ? true : false);
                        drawing.GenerateDrawingsPart(DrawingsPart, drawingFromRow, drawingToRow, "rId" + ChartCount);
                        ChartPart chartPart = DrawingsPart.AddNewPart<ChartPart>("rId" + ChartCount);
                        ChartCount++;
                        chartPartObject = drawing.GeneratePieChart(worksheetPerPart, chartPart, HideChartDescriptionMaxPercentValue, perStartRow, PerEndRow, perFirstCol, PerLastCol, lineColour, perSheetName,
                                                 ChartCaption, SubTitle, num, NumberFormat, Table, GapWidth, isMatrix, isQCM);

                        // Leader lines break Google Sheets import; keep disabled for GWS compatibility
                        HasLeaderLines = false;
                        break;
                    }

                case "xlColumnClustered":
                    {
                        isMatrix = ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent ? true : false);
                        drawing.GenerateDrawingsPart(DrawingsPart, drawingFromRow, drawingToRow, "rId" + ChartCount);
                        ChartPart chartPart = DrawingsPart.AddNewPart<ChartPart>("rId" + ChartCount);
                        ChartCount++;
                        drawing.GenerateColumnClusteredChart(worksheetPerPart, chartPart, perStartRow, ((Table.Question.QuestionType & (QuestionType.SA)) != 0 ? (PerEndRow + Table.Question.SubTotalCnt) : PerEndRow), perFirstCol, PerLastCol, lineColour, perSheetName,
                                                 ChartCaption, SubTitle, num, ValueNumberFormat, Table, GapWidth, ChartType, isMatrix, isQCM, isNumeric);
                        SetChartHeight(DrawingsPart, chartPart, ref drawingFromRow, ref drawingToRow, rowCount, DataLabelHeightDef: DataLabelHeightDef);
                        break;
                    }

                case "xlBarClustered":
                    {
                        int DataLabelHeightDef = 0;
                        isMatrix = ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent ? true : false);
                        drawing.GenerateDrawingsPart(DrawingsPart, drawingFromRow, drawingToRow, "rId" + ChartCount);
                        ChartPart chartPart = DrawingsPart.AddNewPart<ChartPart>("rId" + ChartCount);
                        ChartCount++;
                        chartPartObject = drawing.GenerateBarClusteredChart(worksheetPerPart, chartPart, perStartRow, ((Table.Question.QuestionType & (QuestionType.SA)) != 0 ? (PerEndRow + Table.Question.SubTotalCnt) : PerEndRow), perFirstCol, PerLastCol, lineColour, perSheetName,
                                                 ChartCaption, SubTitle, num, ValueNumberFormat, Table, GapWidth, ChartType, isMatrix, isQCM, isNumeric);
                        if (isMatrix && !isQCM && !isNumeric)
                        {
                            int colDiff = PerLastCol - perFirstCol;
                            DataLabelHeightDef += (colDiff / 2) + 1;
                        }
                        else
                            DataLabelHeightDef = 3;


                        SetChartHeight(DrawingsPart, chartPartObject, ref drawingFromRow, ref drawingToRow, rowCount, DataLabelHeightDef: DataLabelHeightDef);
                        break;
                    }

                case "xlColumnStacked":
                    {
                        if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                        {
                            drawing.GenerateDrawingsPart(DrawingsPart, drawingFromRow, drawingToRow, "rId" + ChartCount);
                            ChartPart chartPart = DrawingsPart.AddNewPart<ChartPart>("rId" + ChartCount);
                            ChartCount++;
                            drawing.GenerateColumnStackedChart(worksheetPerPart, chartPart, perStartRow, PerEndRow, perFirstCol, PerLastCol, lineColour, perSheetName,
                                                     ChartCaption, SubTitle, num, ValueNumberFormat, Table, GapWidth, ChartType);
                        }
                        HasSeriesLines = true;
                        HideZero = true;
                        break;
                    }

                case "xlBarStacked":
                    {
                        //#OutputFormatting - Removing SubTotal From Graph
                        if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                        {
                            drawing.GenerateDrawingsPart(DrawingsPart, drawingFromRow, drawingToRow, "rId" + ChartCount);
                            ChartPart chartPart = DrawingsPart.AddNewPart<ChartPart>("rId" + ChartCount);
                            ChartCount++;
                            chartPartObject = drawing.GenerateBarStackedChart(worksheetPerPart, chartPart, perStartRow, PerEndRow, perFirstCol, PerLastCol, lineColour, perSheetName,
                                                     ChartCaption, SubTitle, num, ValueNumberFormat, Table, GapWidth, ChartType, ref LegendSeriesText);
                            SetChartHeight(DrawingsPart, chartPartObject, ref drawingFromRow, ref drawingToRow, rowCount, LegendSeriesText);
                        }

                        break;
                    }

                case "xlColumnStacked100":
                    {
                        //#OutputFormatting - Removing SubTotal From Graph

                        isMatrix = ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent ? true : false);
                        drawing.GenerateDrawingsPart(DrawingsPart, drawingFromRow, drawingToRow, "rId" + ChartCount);
                        ChartPart chartPart = DrawingsPart.AddNewPart<ChartPart>("rId" + ChartCount);
                        ChartCount++;
                        drawing.GenerateColumnStacked100Chart(worksheetPerPart, chartPart, perStartRow, PerEndRow, perFirstCol, PerLastCol, lineColour, perSheetName,
                                                 ChartCaption, SubTitle, num, ValueNumberFormat, Table, GapWidth, ChartType, isMatrix);
                        HasSeriesLines = (Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
                        HideZero = true;
                        break;
                    }

                case "xlBarStacked100":
                    {
                        //#OutputFormatting - Removing SubTotal From Graph
                        string legendseriesOriginal = null;
                        string longest = "";
                        int choiceCount = 0;
                        isMatrix = ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent ? true : false);
                        drawing.GenerateDrawingsPart(DrawingsPart, drawingFromRow, drawingToRow, "rId" + ChartCount);
                        ChartPart chartPart = DrawingsPart.AddNewPart<ChartPart>("rId" + ChartCount);
                        ChartCount++;
                        chartPartObject = drawing.GenerateBarStacked100Chart(worksheetPerPart, chartPart, perStartRow, PerEndRow, perFirstCol, PerLastCol, lineColour, perSheetName,
                                                 ChartCaption, SubTitle, num, ValueNumberFormat, Table, GapWidth, ChartType, ref LegendSeriesText, ref legendseriesOriginal, ref longest, ref choiceCount, isMatrix);

                        if (isMatrix)
                            SetChartHeight(DrawingsPart, chartPartObject, ref drawingFromRow, ref drawingToRow, rowCount, LegendSeriesText);
                        else
                            SetChartHeightBarStacked(DrawingsPart, chartPartObject, ref drawingFromRow, ref drawingToRow, rowCount, LegendSeriesText, legendseriesOriginal, longest, choiceCount);
                        break;
                    }

                case "xlLine":
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
            if (!isQCM)
                GraphRange = "\'" + GraphStartCell + "\'!B" + (int.Parse(drawingFromRow) + 1) + ":" + "B" + drawingToRow;
            ExitProc:;
            RunningProcName = OrgProcName;
        }
        private int GetTitleLine(string title)
        {
            return Regex.Matches(title, "\n").Count + 1;
        }
        private int GetLegendLine(string legend)
        {
            return (int)OpenXmlHelper.FindLine(legend, LegendWidthDef);
        }
        /// <summary>
        /// Method to set the chart height of the Bar Stacked chart
        /// </summary>
        /// <param name="drawingsPart">Drawing part object</param>
        /// <param name="chartPartObject">Chart part object</param>
        /// <param name="drawingFromRow">Start Row of the drawing</param>
        /// <param name="drawingToRow">End Row of the drawing</param>
        /// <param name="rowCount"></param>
        /// <param name="LegendSeriesText"></param>
        /// <param name="legendSeriesOriginal"></param>
        /// <param name="longest">Longest text</param>
        /// <param name="choiceCount">Choice count</param>
        /// <param name="DataLabelHeightDef"></param>
        private void SetChartHeightBarStacked(DrawingsPart drawingsPart, ChartPart chartPartObject, ref string drawingFromRow,
                                       ref string drawingToRow, int rowCount, string LegendSeriesText = null, string legendSeriesOriginal = "", string longest = "", int choiceCount = 0, int DataLabelHeightDef = 3)
        {
            //Title height
            int titleCharacterLimit = 85;
            int characterLimit = 80;
            string title = chartPartObject.ChartSpace.Descendants<C.Chart>().FirstOrDefault().Title.InnerText;
            int line = GetTitleLine(title);
            if (title != "")
            {
                var titleText = title.Split('\n');
                foreach (string value in titleText)
                {
                    line += value.Length / titleCharacterLimit;
                    ;
                }
            }
            line = (line + 1);

            //Legend Series Height

            int legendLine = 6;
            int longestLen = longest.Length;
            if (longestLen < (characterLimit / 2))
            {
                int hegt = longestLen;
                for (int h=0;h< choiceCount;h++)
                {
                    hegt += longestLen;
                    if (hegt > characterLimit)
                    {
                        legendLine += 1;
                        hegt = 0;
                        h--;
                    }
                    else if (hegt == characterLimit)
                    {
                        legendLine += 1;
                        hegt = 0;
                    }
                }
                //legendLine = legendSeriesOriginal == null ? 0 : (LegendSeriesText.Length / characterLimit) + 1;
                if (legendLine > 3 && (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP"))
                {
                    legendLine += 1;
                }
                if (legendLine == 6)
                    legendLine = 4;
            }
            else
            {
                legendLine = choiceCount > 9 ? 8 : choiceCount;
                if ((QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP"))
                {
                    legendLine += 2;
                }
            }

            Xdr.TwoCellAnchor twoCellAnchor = OpenXmlHelper.GetLastCellAnchor(drawingsPart);
            drawingFromRow = twoCellAnchor.FromMarker.RowId.Text;
            //Setting up total Height
            drawingToRow = (int.Parse(drawingFromRow) + line + legendLine + 6).ToString();
            twoCellAnchor.ToMarker.RowId.Text = drawingToRow;
        }

        private void SetChartHeight(DrawingsPart drawingsPart, ChartPart chartPartObject, ref string drawingFromRow,
                                           ref string drawingToRow, int rowCount, string LegendSeriesText = null, int DataLabelHeightDef = 3)
        {
            int line = GetTitleLine(chartPartObject.ChartSpace.Descendants<C.Chart>().FirstOrDefault().Title.InnerText);
            int legendLine = LegendSeriesText == null ? 0 : GetLegendLine(LegendSeriesText);
            Xdr.TwoCellAnchor twoCellAnchor = OpenXmlHelper.GetLastCellAnchor(drawingsPart);
            drawingFromRow = twoCellAnchor.FromMarker.RowId.Text;
            drawingToRow = (int.Parse(drawingFromRow) + (rowCount * DataLabelHeightDef) + (legendLine == 1 ? 5 : 6) + line + legendLine).ToString();
            twoCellAnchor.ToMarker.RowId.Text = drawingToRow;
        }
        private bool GetHasWeight(GTTable Table)
        {
            bool GetHasWeight = false;
            {
                var withBlock = Table;
                if ((withBlock.Question.QuestionType & (QuestionType.SA | QuestionType.MA)) == 0)
                    goto ExitProc;
                return Table.Question.HasWeight || Table.Question.HasCount;
            }

        ExitProc:
            return GetHasWeight;
        }
        private void CreateSheet(SpreadsheetDocument document, string sheetName, string newName, uint sheetId, string id)
        {
            NewBook newBook = new NewBook();
            WorkbookPart workbookPart = document.WorkbookPart;
            WorksheetPart worksheetPart;
            Sheets sheets = workbookPart.Workbook.Sheets;
            Sheet sheet;

            switch (sheetName)
            {
                case "Standard":
                    sheet = new Sheet() { Name = newName, SheetId = sheetId, Id = id };
                    sheets.InsertAfter(sheet, sheets.FirstChild);
                    worksheetPart = workbookPart.AddNewPart<WorksheetPart>(id);
                    newBook.GenerateWorksheetPart9Content(worksheetPart);
                    break;
                case "WT":
                    sheet = new Sheet() { Name = newName, SheetId = sheetId, Id = id };
                    sheets.InsertAfter(sheet, sheets.FirstChild);
                    worksheetPart = workbookPart.AddNewPart<WorksheetPart>(id);
                    newBook.GenerateWorksheetPart12Content(worksheetPart);
                    break;
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

            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, "//");
            TemplatePath = OutputUtil.BuildPath(d, n, "//");
            RunningProcName = OrgProcName;
            return TemplatePath;
        }
        private bool SetRatSourceRange(GTTable Table, SpreadsheetDocument package, string PerStartCell, ref int PerFirstRow, ref int PerEndRow, ref int PerLastCol, ref int PerFirstCol, string TableRange
       , ref Collection GraphSourceRangeCol
       , ref Collection GraphTableRangeCol
       , ref Collection nCol)
        {
            bool SetRatSourceRange = false;
            int r1 = 0;
            if ((CurrentOutput.Orientation == TableOrientation.Portrait && AverageRowIndex > 2) || (CurrentOutput.Orientation == TableOrientation.Landscape && AverageColumnIndex > 3))
            {
                GraphSourceRangeCol = new VBA.Collection();
                GraphTableRangeCol = new VBA.Collection();
                nCol = new VBA.Collection();
                if (CurrentOutput.Orientation == TableOrientation.Portrait)
                {
                    {

                    }
                }
                else
                {
                    WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(package, PerStartCell);
                    Row lRow = worksheetPart.Worksheet.Descendants<Row>().LastOrDefault();
                    PerFirstRow = (int)(lRow.RowIndex.Value - Table.ChildQuestionsCount) - 2;
                    PerEndRow = (PerFirstRow + Table.ChildQuestionsCount - 1) + 2;
                    PerFirstCol = 2;
                    PerLastCol = (int)((CurrentOutput.WBOn && CurrentOutput.ShowPreWBTotal) ? (AverageColumnIndex + 2) : (AverageColumnIndex + 1));
                    r1 = (int)((CurrentOutput.WBOn && CurrentOutput.ShowPreWBTotal) ? (TotalColumnIndex + 2) : (TotalColumnIndex + 1));
                    Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)(PerFirstRow + 2));
                    var columnValue = OpenXmlHelper.GetCell(row, PerFirstRow + 2, r1).CellValue.InnerText;
                    nCol.Add(Math.Round(Convert.ToDouble(columnValue)));
                }
                GraphTableRangeCol.Add(TableRange);
                SetRatSourceRange = true;
            }
            return SetRatSourceRange;
        }

        private Sheet GetSheetByName(SpreadsheetDocument document, string sheetName)
        {
            IEnumerable<Sheet> sheets =
               document.WorkbookPart.Workbook.GetFirstChild<Sheets>().
               Elements<Sheet>().Where(s => s.Name == sheetName);

            if (sheets?.Count() == 0)
            {
                return null;
            }

            string relationshipId = sheets?.First().SheetId;

            var Sheet = document.WorkbookPart.Workbook.Descendants<Sheet>()
                               .FirstOrDefault(s => s.SheetId == relationshipId);

            return Sheet;
        }
    }
}
