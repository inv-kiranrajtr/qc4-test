using log4net;
using Macromill.QCWeb.Batch.Report;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.Tabulation;
using Qc4Launcher.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Common.Constants;
using XlPageOrientation = Macromill.QCWeb.Common.XlPageOrientation;
using XlPaperSize = Macromill.QCWeb.Common.XlPaperSize;
using StatusCode = Macromill.QCWeb.ReportRequest.StatusCode;
using OutputType = Macromill.QCWeb.ReportRequest.OutputType;
using TableType = Macromill.QCWeb.ReportRequest.TableType;
using TableOrientation = Macromill.QCWeb.ReportRequest.TableOrientation;
using XlChartType = Macromill.QCWeb.Common.XlChartType;
using FileType = Macromill.QCWeb.ReportRequest.FileType;
using KeyItemInformation = Macromill.QCWeb.ReportRequest.KeyItemInformation;
using AxesGroupInformation = Macromill.QCWeb.ReportRequest.AxesGroupInformation;
using AxesInformation = Macromill.QCWeb.ReportRequest.AxesInformation;
using Vb = Microsoft.VisualBasic;
using System.Collections.Specialized;
using System.ComponentModel;
using DocumentFormat.OpenXml.Packaging;
using Dr = DocumentFormat.OpenXml.Packaging;
using SpreadSheet = DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic;
using Macromill.QCWeb.ReportRequest;
using System.Drawing;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using Qc4Launcher.Summary.OpenXml;
using NPOI.SS.Formula;

namespace Qc4Launcher.Logic.Cross_Report
{
    public class ReportCreatorXML
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string TEMPLATE_NAME = "Report_Template.xlt";
        private static string BOOK_NAME = "Report.xltx";
        private static string TRANSPOSE_TEMPLATE_NAME = "ReportPortrait.xlt";
        private static string FORMAT_TEMPLATE_NAME = "ReportFormat.xlt";
        private static string TRANSPOSE_FORMAT_TEMPLATE_NAME = "ReportPortraitFormat.xlt";
        private static string PARTS_PRESENTATION_PATH = @"PPTemplates\parts.pptx";
        private static string NUMERIC_DATALABEL_NUMBER_FMT = "0.0;;";
        private static string MINBASE_MSG = "n = 30 以上";
        public static int TAB_COLOR_BLUE = 0XF1D9C5;
        public static int TAB_COLOR_RED = 0XDBDCF2;
        private static int MaxRowsCount = 1048575;
        private static int MaxColumnsCount = 16382;
        private static int MAX_SHEETS_COUNT = int.MaxValue;
        private static int SA_MA_STD_LCol = 10;
        private static int SA_MA_WT_STD_LCol = 12;
        private static int N_STD_LCol = 15;
        private static int SA_MA_SIG_LCol = 11;
        private static int SA_MA_WT_SIG_LCol = 12;
        private static int N_SIG_LCol = 16;
        private static int SrcGraphOffset = 1219199;//1116457;
        private static double DefColWidth = 9.33203125;
        public static double LegendWidthDef = 78;
        private string WorkingSheet;
        private string TemplateBook;
        private string TransposeTemplateBook;
        private string FormatBook;
        private string TransposeFormatBook;
        private XlFileFormat FileFormat;
        public bool onlySigPage;
        public Microsoft.Office.Interop.Excel.Application xlApp;
        private SummaryCreatorXml summaryCreatorXml = null;
        private NPOICrossCreator nPOICrossCreator;
        private string wbs;
        private string BookPSWD;
        private string SheetPSWD;
        public CrossTabulationQC QC;
        public double progressAvailable;
        public double progressAvailableRpt;
        public double progressAvailableSig;
        public double currentProgress;
        private string TemplateDirectoryPath;
        private Reportset ThisReportset;
        private string OutputDirectoryPath;
        private string prfix;
        private string FormatSheet;
        private string DoubleFormatSheet;
        private string TransposeFormatSheet;
        private string TransposeDoubleFormatSheet;
        private string GTFormatSheet;
        private string GTTransposeFormatSheet;
        private string tmp = null;
        public OutputCross CurrentOutput = null;
        private Hashtable ThisReportBooks;
        public BackgroundWorker bgWorker;
        public List<string> outputFiles;
        public bool IsCheckRefineCondition = false;
        private Dictionary<string, double> colWidthMap;

        public void CreateReport(Macromill.QCWeb.Batch.Report.Outputs Outputs, string bookPSWD, string sheetPSWD, string outputDirectoryPath, bool isCheckRefineCondition,
           string templateDirectoryPath, Microsoft.Office.Interop.Excel.Application xlAppG, BackgroundWorker bgWorker, DoWorkEventArgs bgWorkerArg, Reportset reportset, bool onlySigPageP = false,
           CrossTabulationQC QC = null, double progressAvailable = 0, double curProgres = 0, string qc4FileName = null, List<string> outputFiles = null)
        {
            Output tmpOutput = null;
            Output preOutput = null;
            StatusCode tmpStatus = 0;
            Output QuestionnaireOutput = null;
            int i;
            BookPSWD = bookPSWD;
            SheetPSWD = sheetPSWD;
            IsCheckRefineCondition = isCheckRefineCondition;
            TemplateDirectoryPath = templateDirectoryPath;
            ThisReportset = reportset;
            OutputDirectoryPath = outputDirectoryPath;
            prfix = NPOICrossCreator.getPrefix(qc4FileName);
            this.progressAvailable = progressAvailable;
            this.currentProgress = curProgres;
            this.QC = QC;
            string path = null;
            string pathSig = null;
            string selectedFilePath = null;
            string Prefix = null;
            xlApp = xlAppG;
            this.bgWorker = bgWorker;
            if (outputFiles == null) outputFiles = new List<string>();
            this.outputFiles = outputFiles;
            colWidthMap = new Dictionary<string, double>();

            try
            {
                double currentprogressGrp = currentProgress;
                double progressStepGrp = progressAvailable / Outputs.Count;
                for (i = 0; i <= Outputs.Count - 1; i++)
                {
                    tmpOutput = Outputs[i];
                    tmpOutput.StartTime = DateTime.Now;
                    tmpOutput.Status = StatusCode.Running;
                    OutputCross CurrentOutput = (OutputCross)tmpOutput;
                    CrossTable tmpTable = (CrossTable)CurrentOutput.Tables[0];
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

                    Prefix = ThisReportset.DivName + ThisReportset.FileNamePrefix;
                    path = CrossReportHelper.GetPath(outputDirectoryPath, CurrentOutput, "CrossReportOutput", reportset, xlApp,
                                                                            qc4FileName, filenameSuffix, Prefix);
                    if (preOutput != null) { preOutput.Status = tmpStatus; }
                    NPOICrossCreator crossCreatorSig = new NPOICrossCreator();
                    switch (tmpOutput.OutputType)
                    {
                        case OutputType.Cross:
                            // ' クロス
                            if ((CurrentOutput.SignificanceTestOne || CurrentOutput.SignificanceTestFive || CurrentOutput.SignificanceTestTen)
                                                            && (!CurrentOutput.SignificanceTestOne || !CurrentOutput.SignificanceTestFive || !CurrentOutput.SignificanceTestTen))
                            {
                                progressAvailableRpt = progressStepGrp * 0.6;
                                progressAvailableSig = progressStepGrp * 0.4;
                            }
                            else
                            {
                                progressAvailableRpt = progressStepGrp;
                            }
                            using (SpreadsheetDocument document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook))
                            {
                                CreateCrossReport(CurrentOutput, document);
                            }
                            if (bgWorker.CancellationPending) return;
                            updateProgress(currentProgress, LocalResource.PB_EXCEL_GEN);
                            if ((CurrentOutput.SignificanceTestOne || CurrentOutput.SignificanceTestFive || CurrentOutput.SignificanceTestTen)
                            && (!CurrentOutput.SignificanceTestOne || !CurrentOutput.SignificanceTestFive || !CurrentOutput.SignificanceTestTen))
                            {
                                Prefix = CurrentOutput.ParentReportset.DivName + CurrentOutput.ExcelBookNamePrefix;
                                pathSig = CrossReportHelper.GetPath(outputDirectoryPath, CurrentOutput, "CrossReportOutput", reportset, xlApp,
                                                                            qc4FileName, filenameSuffix, Prefix + "_ps");

                                using (SpreadsheetDocument document = SpreadsheetDocument.Create(pathSig, SpreadsheetDocumentType.Workbook))
                                {
                                    CreateCrossSignificance(CurrentOutput, document);
                                }
                            }
                            if (bgWorker.CancellationPending) return;
                            break;
                        default:
                            break;
                            //' 無視する
                    }
                    tmpOutput.EndTime = DateTime.Now;
                    if (i == Outputs.Count - 1)
                    {
                        tmpOutput.Status = tmpStatus;
                        preOutput = null;
                    }
                    else
                    {
                        preOutput = tmpOutput;
                    }
                    if (bgWorker.CancellationPending) return;
                    currentprogressGrp += progressStepGrp;
                    currentProgress = currentprogressGrp;
                    updateProgress(currentProgress, LocalResource.PB_EXCEL_GEN);
                }
                if (selectedFilePath == null)
                {
                    bool isMaximizeWindow = outputDirectoryPath == null ? true : false;
                    OpenXmlHelper.SaveWorkBookCross(path, ("Cj_PWhxRo7Q8" + (char)2), ref xlApp, isMaximizeWindow);
                    if (pathSig != null)
                        OpenXmlHelper.SaveWorkBookCross(pathSig, ("Cj_PWhxRo7Q8" + (char)2), ref xlApp, isMaximizeWindow);
                }
                try
                {
                    if (outputDirectoryPath == null)
                    {
                        String directoryPath = Path.GetDirectoryName(path);
                        if (Directory.Exists(directoryPath))
                        {
                            var di = new DirectoryInfo(directoryPath);
                            if (di.Attributes.HasFlag(FileAttributes.ReadOnly))
                                di.Attributes &= ~FileAttributes.ReadOnly;
                        }
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                _log.Info(ex.StackTrace);
                foreach (var x in ThisReportBooks.Values)
                {
                    ReportBook rb = (ReportBook)x;
                    //foreach (Workbook wb in rb.Books())
                    //{
                    //    try
                    //    {
                    //        wb.Close(false);
                    //    }
                    //    catch (Exception ex1)
                    //    {
                    //    }
                    //}
                }
                try
                {
                    //TemplateBook.Close(false);
                }
                catch (Exception ex2)
                {
                }
                try
                {
                    //FormatBook.Close(false);
                }
                catch (Exception ex2)
                {
                }
                throw ex;
            }
            finally
            {

            }
        }
        public void CreateCrossReport(OutputCross Output, SpreadsheetDocument document)
        {
            int SigCutColumn = 0;
            int PCutColumn = 0;
            string tmpFormatSheet = null;
            string FormatSheetName = null;
            bool HasWeightColumn = false;
            int maxAxisCnt = 0;
            int[] MaxAxesCountArray; // int
            bool HasWeightBack = false;
            bool HasWeight = false;
            Hashtable CutRowsCol = null;
            Hashtable CutColumnsCol = null;
            string FormatRangeNamePrefix = null;
            string wb = null;
            CrossTable tmpTable = null;
            string sht = null;
            Array v = null; //string
            Array DataValue = null; //Variant
            Array Ranking = null; //int
            Array HatchingColorIndex = null; // XlColorIndex
            Array ArrowEnd = null; //Variant
            Array SigTestMarking = null;  //string
            Array ContentsValue = null;  //string
            Array HyperLinkTargetCells = null; // Range
            bool isN = false;
            bool isMA = false;
            bool isSA = false;
            int i = 0;
            int j = 0;
            TableType tmpTableType = 0;
            string tableTypeBuf = null;
            string StartCell = null;
            string TableRange = null;
            string SourceRange = null;
            string tmpRange = null;
            int r = 0;
            int c = 0;
            int n = 0;
            //' 横％表用
            string ColumnClusterGraphSpace = null;
            string ColumnClusterGraphLegendArea = null;
            string BarStackedGraphSpace = null;
            string BarStackedGraphLegendArea = null;
            string BarStackedGraphPlotArea = null;
            // ' 縦％表用
            string BarClusterGraphSpace = null;
            string BarClusterGraphPlotArea = null;
            string BarClusterGraphLegendArea = null;
            string ColumnStackedGraphSpace = null;
            string ColumnStackedGraphLegendArea = null;
            string ColumnStackedGraphPlotArea = null;
            bool DefHasNA = false;
            bool DefHasIV = false;
            int NAColumnIndex = 0;
            int IVColumnIndex = 0;
            bool CutMedian = false;
            int MedIdx = 0;
            string tmpRange2 = null;
            bool CreateGraph = false;
            List<int> LinesIndexList = null;
            bool HasLines = false;
            bool isSideGraph = false;
            string[] tmpBuf;
            int x = 0;
            string chtObjs = null;
            string chtObj = null;
            string sCol = null;
            string ps = null;
            string Axis = null;
            Array tmpVal;//int
            string ContentsTempSheet = null;
            string MinBaseBox = null;
            string g = null;
            string b = null;
            string LegendSeriesText = null;
            double d = 0;
            Array s = null;  //Shape;
            int cnt = 0;
            string l = null;
            string p = null;
            double lt = 0;
            double pt = 0;
            double o = 0;
            int clrIdx;
            double tmpTop = 0;
            double tmpLeft = 0;
            int SuperfluousColumn = 0;
            int tmpColumn = 0;
            int OverRowsCount = 0;
            int OverColumnsCount = 0;
            int RowNum;
            double diff = 0;
            string tempTable = null;
            int GraphStartCol = 0;
            int GraphColCount = 0;
            int AvgCol = 7;
            int tableStartRow = 0;

            WorksheetPart worksheetIndexPart = null;
            bool isGlobalMode = QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? false : true;
            ReportTemplate reportTemplate = new ReportTemplate();
            ReportPortraitTemplate reportPortraitTemplate = new ReportPortraitTemplate();
            nPOICrossCreator = new NPOICrossCreator();
            CurrentOutput = Output;
            nPOICrossCreator.CurrentOutput = Output;
            nPOICrossCreator.BookPSWD = BookPSWD;
            nPOICrossCreator.SheetPSWD = SheetPSWD;
            nPOICrossCreator.xlApp = xlApp;
            nPOICrossCreator.bgWorker = bgWorker;

            HasWeightBack = Output.ShowPreWBTotal;
            MaxAxesCountArray = new int[Output.Tables.Count];

            if (Output.Orientation == TableOrientation.Landscape)
                reportTemplate.GenerateWorkbookPart(document.AddWorkbookPart());
            else
                reportPortraitTemplate.GenerateWorkbookPart(document.AddWorkbookPart());

            for (i = 0; i < Output.Tables.Count; i++)
            {
                tmpTable = (CrossTable)Output.Tables[i];
                MaxAxesCountArray[i] = nPOICrossCreator.GetMaxAxesCount(tmpTable);
                maxAxisCnt = maxAxisCnt | MaxAxesCountArray[i];
            }
            HasWeightColumn = true;// ' 便宜的にTrue
            if (Output.Orientation == TableOrientation.Landscape)
            {
                wb = FormatBook;
            }
            else
            {
                wb = TransposeFormatBook;
            }

            if (Output.OutputNPerTable)
            {
                FormatSheetName = "Cross_NP_Std";
            }
            else if (Output.OutputNTable)
            {
                FormatSheetName = "Cross_N_Std";
            }
            else if (Output.OutputPerTable)
            {
                FormatSheetName = "Cross_P_Std";
            }
            if ((maxAxisCnt & 2) == 2)
            { //' 三重あり
                if (Output.Orientation == TableOrientation.Landscape)
                {
                    if (FormatSheet == null)
                    {
                        FormatSheet = FormatSheetName;
                        AdjustFormat(document, FormatSheet, null, 2, ref PCutColumn, HasWeightColumn);
                    }
                }
                else
                {
                    if (TransposeFormatSheet == null)
                    {
                        AdjustFormat(document, TransposeFormatSheet, null, 2, ref PCutColumn, HasWeightColumn);
                        TransposeFormatSheet = FormatSheetName;
                    }
                }
            }
            if ((maxAxisCnt & 1) == 1)
            {// ' 二重あり
                if (Output.Orientation == TableOrientation.Landscape)
                {
                    if (DoubleFormatSheet == null)
                    {
                        if ((maxAxisCnt & 2) == 0)
                        {
                            DoubleFormatSheet = FormatSheetName;
                        }
                        else
                        {
                            DoubleFormatSheet = FormatSheet;
                        }
                        AdjustFormat(document, DoubleFormatSheet, null, 1, ref PCutColumn, HasWeightColumn, FormatSheet != null);
                    }
                }
                else
                {
                    if (TransposeDoubleFormatSheet == null)
                    {
                        if ((maxAxisCnt & 2) == 0)
                        {
                            TransposeDoubleFormatSheet = FormatSheetName;
                        }
                        else
                        {
                            TransposeDoubleFormatSheet = TransposeFormatSheet;
                        }
                        //AdjustFormat(document, TransposeDoubleFormatSheet, null, 1, ref PCutColumn, HasWeightColumn, TransposeFormatSheet != null, true);
                    }
                }
            }
            DefHasNA = Output.ShowNAAtItem;
            DefHasIV = Output.ShowIVAtItem;
            CutMedian = Output.ParentRequest.ShowMedian & Output.WBOn;
            if (Output.OutputNPerTable)
            {
                tmpTableType = TableType.NPer;
                tableTypeBuf = LocalResource.REPORT_NP_KEYWORD;
            }
            else if (Output.OutputNTable)
            {
                tmpTableType = TableType.N;
                tableTypeBuf = LocalResource.REPORT_N_KEYWORD;
            }
            else if (Output.OutputPerTable)
            {
                tmpTableType = TableType.Per;
                tableTypeBuf = LocalResource.REPORT_P_KEYWORD;
            }
            ContentsTempSheet = "INDEX";
            worksheetIndexPart = OpenXmlHelper.GetWorksheetPartByName(document, ContentsTempSheet);
            double progressStep = progressAvailableRpt / Output.Tables.Count;
            for (i = 0; i < Output.Tables.Count; i++)
            {
                if (bgWorker.CancellationPending) return;
                updateProgress(currentProgress, String.Format(LocalResource.PB_EXCEL_GEN_TABLE, (i + 1), Output.Tables.Count));
                currentProgress += progressStep;

                tmpTable = (CrossTable)Output.Tables[i];
                isN = (tmpTable.Question.QuestionType & QuestionType.N) == QuestionType.N;
                isMA = (tmpTable.Question.QuestionType & QuestionType.MA) == QuestionType.MA;
                HasWeight = nPOICrossCreator.GetHasWeight(tmpTable);
                switch (tmpTable.Question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N))
                {
                    case QuestionType.SA:
                    case QuestionType.MA:
                        FormatRangeNamePrefix = "SA_MA";
                        break;
                    case QuestionType.N:
                        FormatRangeNamePrefix = "N";
                        break;
                    default:
                        throw new Exception(LocalResource.REPORT_UNJUST_QUESTION_TYPE_MESSAGE);
                }
                if (HasWeight) { FormatRangeNamePrefix = FormatRangeNamePrefix + "_WT"; }
                nPOICrossCreator.GetCutRowsAndColumns(tmpTable, HasWeightBack, HasWeight, MaxAxesCountArray[i], ref CutRowsCol, ref CutColumnsCol, ref MedIdx, true, CutMedian);
                if (DefHasNA)
                {
                    NAColumnIndex = tmpTable.GetTableValueColumnIndexMaximum - (NPOICrossCreator.ToInt(HasWeight) & 2) - (NPOICrossCreator.ToInt(DefHasIV) & 1) - (tmpTable.Question.SubTotalCnt);
                    if (CutColumnsCol.ContainsKey(NAColumnIndex)) { NAColumnIndex = 0; }
                }
                if (DefHasIV)
                {
                    IVColumnIndex = tmpTable.GetTableValueColumnIndexMaximum - (NPOICrossCreator.ToInt(HasWeight) & 2) - (tmpTable.Question.SubTotalCnt);
                    if (CutColumnsCol.ContainsKey(IVColumnIndex)) { IVColumnIndex = 0; }
                }
                if (Output.Orientation == TableOrientation.Landscape)
                {
                    if (MaxAxesCountArray[i] == 2)
                    {
                        tempTable = "Cross_Triple_Std" + (isN ? "_N" : "");
                        CreateNewSheet(tmpTable, worksheetIndexPart, ContentsTempSheet, ref ContentsValue, ref HyperLinkTargetCells, tmpTableType,
                                       reportTemplate, document, tempTable, i);
                        tmpFormatSheet = FormatSheet;
                    }
                    else
                    {
                        tempTable = "Cross_Double_Std" + (isN ? "_N" : "");
                        CreateNewSheet(tmpTable, worksheetIndexPart, ContentsTempSheet, ref ContentsValue, ref HyperLinkTargetCells, tmpTableType,
                                       reportTemplate, document, tempTable, i);
                        tmpFormatSheet = DoubleFormatSheet;
                    }
                }
                else
                {
                    if (HasWeight)
                    {
                        tempTable = "Cross_WT_Template";
                        CreateNewSheetPortrait(tmpTable, worksheetIndexPart, ContentsTempSheet, ref ContentsValue, ref HyperLinkTargetCells, tmpTableType,
                                       reportPortraitTemplate, document, tempTable, i);
                    }
                    else
                    {
                        tempTable = "Cross_Template";
                        CreateNewSheetPortrait(tmpTable, worksheetIndexPart, ContentsTempSheet, ref ContentsValue, ref HyperLinkTargetCells, tmpTableType,
                                      reportPortraitTemplate, document, tempTable, i);
                    }
                    if (MaxAxesCountArray[i] == 2)
                    {
                        tmpFormatSheet = TransposeFormatSheet;
                    }
                    else
                    {
                        tmpFormatSheet = TransposeDoubleFormatSheet;
                    }
                }
                tmp = LocalResource.CR_FILTER_PREFIX + Output.LocalizedFilteringExpression;
                if (tmp != null && tmp.Length > 0)
                {

                }
                StartCell = "OutputStart";
                ColumnClusterGraphSpace = null;
                ColumnClusterGraphLegendArea = null;
                BarStackedGraphSpace = null;
                BarStackedGraphLegendArea = null;
                BarStackedGraphPlotArea = null;
                BarClusterGraphSpace = null;
                BarClusterGraphPlotArea = null;
                BarClusterGraphLegendArea = null;
                ColumnStackedGraphSpace = null;
                ColumnStackedGraphLegendArea = null;
                ColumnStackedGraphPlotArea = null;
                SourceRange = null;
                LinesIndexList = null;
                HasLines = false;
                OverRowsCount = 0;
                OverColumnsCount = 0;
                CreateGraph = false;
                GTTable tmpGTTable = tmpTable;
                Dr.DrawingsPart drawingPart = null;
                WorksheetPart worksheetPart = null;
                if (isN)
                    tableStartRow = 27;
                else
                    tableStartRow = 27;
                if (Output.Orientation == TableOrientation.Landscape)
                {
                    Hashtable WholeRowColRef = null;// only for ref 
                    bool CheckOverRowTmpRef = false; // only for ref 
                    nPOICrossCreator.CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking,
                        ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking, 1 + 1,
                        1 + MaxAxesCountArray[i], HasWeight, isN, tmpTableType, MaxRowsCount,
                        MaxColumnsCount, ref CheckOverRowTmpRef, WholeRowColRef, ref OverRowsCount, ref OverColumnsCount, true);
                    if (OverColumnsCount > 0)
                    {
                        throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                    }
                    if (tmpTableType == TableType.Per & !isN)// top chart
                    {
                        if (OverRowsCount + OverColumnsCount == 0)
                        {
                            CreateGraph = true;
                            LinesIndexList = GetLinesIndexList(tmpTable, CutRowsCol, 1 + 1);
                            if (LinesIndexList == null || LinesIndexList.Count == 0)
                            {
                            }
                            else
                            {
                                HasLines = true;
                            }
                        }
                    }
                    if (tmpTableType == TableType.Per & !isMA) // side chart
                    {
                        if (OverRowsCount + OverColumnsCount == 0)
                        {
                            CreateGraph = true;
                            isSideGraph = true;
                        }
                    }
                    RowNum = 2;

                    worksheetPart = OpenXmlHelper.GetWorksheetPartByName(document, tempTable);

                    SpreadSheet.Drawing drawing = new SpreadSheet.Drawing() { Id = "rId1" };
                    worksheetPart.Worksheet.Append(drawing);
                    drawingPart = worksheetPart.AddNewPart<DrawingsPart>("rId1");

                    FormatLandscapeTable(ref document, worksheetPart, maxAxisCnt, tmpTable, sht, ref tempTable, ref RowNum, i, ref GraphStartCol,
                                         ref GraphColCount, CutRowsCol, CutColumnsCol, PCutColumn, tmpFormatSheet, FormatRangeNamePrefix, tmpTableType
                                         , HasWeight, StartCell, isN, ref AvgCol, tableStartRow, isSideGraph, null, true, CutMedian, MedIdx);
                    if (bgWorker.CancellationPending) return;


                    tmp = LocalResource.CR_FILTER_PREFIX + Output.LocalizedFilteringExpression;
                    if (tmp != null && tmp.Length > 0 && IsCheckRefineCondition)
                    {
                        CrossReportHelper.InserStringValue(worksheetPart, tmp, 6, 2);
                    }
                    bool isThreeWay = false;
                    for (int idx = 0; idx <= tmpTable.AxesGroups.Count - 1; idx++)
                    {
                        if (tmpTable.AxesGroups[idx].Count == 2)
                        {
                            isThreeWay = true;
                            break;
                        }
                    }

                    if (NPOICrossCreator.checkSimpleAggr(tmpTable)
                      && !string.IsNullOrEmpty(tmpTable.Question.QNumber)
                      //&& !string.IsNullOrEmpty(tmpTable.Question.TableHeading) Redmine ID : #211718 
                      && tmpTable.AxesGroups.Count > 1)
                    {
                        ContentsValue.SetValue(tmpTable.Question.QNumber, i, 1);
                        ContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                        ContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                    }
                    else
                    {
                        ContentsValue.SetValue(tmpTable.Question.Name, i, 1);
                        ContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                        ContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                    }
                    ContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                    var range = "'" + tempTable + "'!$A$1";
                    HyperLinkTargetCells.SetValue(range, i, 4);


                    int withStartCellColumnsCount = v.GetUpperBound(1);
                    double width;
                    SuperfluousColumn = 2 + withStartCellColumnsCount;
                    tmpColumn = 19;
                    if (tmpColumn > SuperfluousColumn) { SuperfluousColumn = tmpColumn; }

                    if (SuperfluousColumn <= MaxColumnsCount)
                    {
                        SpreadSheet.Columns columns = worksheetPart.Worksheet.Elements<SpreadSheet.Columns>().FirstOrDefault();
                        width = OpenXmlHelper.GetColumn(worksheetPart, 1).Width.Value;
                        CrossReportHelper.SetColumnWidth(columns, SuperfluousColumn, width);
                    }
                    if (Output.WBOn)
                    {
                        string wbstr = LocalResource.WEIGHT_BACK.Insert(LocalResource.WEIGHT_BACK.Length - 1, "[" + tmpTable.Question.WBValue + "]") + '\u2009'
                                        + (tmpTable.Question.TabulateFullQuantity ? LocalResource.WB_TOTAL_NUMBER_BASE : string.Empty);
                        if (Output.Orientation == TableOrientation.Landscape)
                        {
                            v.SetValue(wbstr, 1 + 1, 1);
                            if (CurrentOutput.ShowPreWBTotal)
                            {
                                SpreadSheet.Columns colms = worksheetPart.Worksheet.Elements<SpreadSheet.Columns>().FirstOrDefault();
                                CrossReportHelper.SetColumnWidth(colms, 4, isGlobalMode ? DefColWidth + 3 : DefColWidth);
                                if (isThreeWay)
                                {
                                    CrossReportHelper.SetColumnWidth(colms, 4, 26.7);
                                    CrossReportHelper.SetColumnWidth(colms, 5, isGlobalMode ? DefColWidth + 3 : DefColWidth);
                                }
                            }
                        }
                        else
                        {
                            v.SetValue(wbstr, 2, 1);
                        }
                    }
                    else if (tmpTable.Question.TabulateFullQuantity)
                    {
                        if (Output.Orientation == TableOrientation.Landscape)
                        {
                            v.SetValue(LocalResource.WB_TOTAL_NUMBER_BASE, 1 + 1, 1);
                        }
                        else
                        {
                            v.SetValue(LocalResource.WB_TOTAL_NUMBER_BASE, 2, 1);
                        }
                    }

                    if (isSideGraph)
                    {
                        int dstCol = GraphStartCol;

                        SpreadSheet.Columns colms = worksheetPart.Worksheet.Elements<SpreadSheet.Columns>().FirstOrDefault();
                        int colLmt = CurrentOutput.ShowPreWBTotal ? GraphColCount : GraphColCount + 1;

                        for (int k = 2; k <= colLmt; k++)
                        {
                            width = OpenXmlHelper.GetColumn(worksheetPart, k).Width.Value;
                            CrossReportHelper.SetColumnWidth(colms, dstCol, width);
                            dstCol += 1;
                        }
                        CrossReportHelper.SetColumnWidth(colms, dstCol, DefColWidth);
                        if (CurrentOutput.ShowPreWBTotal)
                        {
                            CrossReportHelper.SetColumnWidth(colms, dstCol, isGlobalMode ? DefColWidth + 3 : DefColWidth);
                            CrossReportHelper.SetColumnWidth(colms, dstCol + 1, DefColWidth);
                        }
                    }

                    int row = Information.LBound(v, 1) + tableStartRow;
                    int fCol = Information.LBound(v, 2) + 1;
                    bool simpleAggr = NPOICrossCreator.checkSimpleAggr(tmpTable);
                    CrossReportHelper.PutValue(worksheetPart, row, fCol, ref v);
                    if (isSideGraph)
                        CrossReportHelper.PutGraphTableValues(worksheetPart, row, GraphStartCol, ref v, GraphColCount + 1);
                    row = Information.LBound(DataValue, 1) + tableStartRow;
                    fCol = Information.LBound(DataValue, 2) + 1;
                    CrossReportHelper.PutValue(worksheetPart, row, fCol, ref DataValue);
                    if (isSideGraph)
                    {
                        CrossReportHelper.PutGraphTableValues(worksheetPart, row, GraphStartCol + Information.LBound(DataValue, 2) - 1, ref DataValue, GraphColCount + 1);
                        SpreadSheet.Row r1 = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)tableStartRow + 2);
                        SpreadSheet.Cell c1 = OpenXmlHelper.GetCell(r1, tableStartRow + 2, GraphStartCol);
                        c1.CellValue = new SpreadSheet.CellValue("");
                    }
                    _log.Info("Auto fit started");
                    int lCol = Information.UBound(DataValue, 2) + 1;
                    int fRow = row;
                    int lstRow = (int)worksheetPart.Worksheet.Descendants<SpreadSheet.Row>().LastOrDefault().RowIndex.Value - 1;
                    //OpenXmlHelper.AutoFitColumn(document.WorkbookPart, worksheetPart, fCol, lCol, fRow, lstRow);
                    _log.Info("Auto fit completed");

                    if (BarStackedGraphPlotArea != null)
                    {

                    }

                    if (CreateGraph)
                    {
                        int firstRow, lastRow, firstCol, lastCol;
                        firstCol = (DataValue.GetLowerBound(1) + (NPOICrossCreator.ToInt(HasWeightBack) & 1) + 2);
                        lastCol = firstCol + tmpTable.SectorsCount + (NPOICrossCreator.ToInt(NAColumnIndex > 0) & 1) + (NPOICrossCreator.ToInt(IVColumnIndex > 0) & 1) - 1;
                        string graphFCol = null, graphLCol = null, graphFRow = null, graphLRow = null;
                        string lineColour = null;
                        if (Output.Orientation == TableOrientation.Landscape)
                        {
                            if (!isN && (!NPOICrossCreator.checkSimpleAggr(tmpTable as CrossTable) || tmpTable.AxesGroups.Count == 1)) //Top Chart
                            {
                                firstRow = Information.LBound(v, 1) + tableStartRow + 2;
                                lastRow = firstRow;
                                graphFCol = (firstCol - 2).ToString(); graphLCol = (lastCol).ToString();
                                graphFRow = "11"; graphLRow = (firstRow - 3).ToString();
                                ChartPart chartPart = drawingPart.AddNewPart<ChartPart>("rId0");
                                DrawingPart.GenerateGraphDrawingsPart(drawingPart, graphFRow, graphLRow, graphFCol, graphLCol, "rId0"
                                                                       , "ColCluster", HasWeightBack, isGlobalMode);

                                DrawingPart.GenerateColumClusterAndLineGraph(worksheetPart, tmpTable, tmpFormatSheet, lineColour, LinesIndexList, ref v, HasLines,
                                                                            chartPart, firstRow, lastRow, firstCol, lastCol, isN, tempTable, i, MaxAxesCountArray);
                                // Combo line series / LegendLine chart removed — plain horizontal bar only (GWS).
                            }

                            if (!isMA)  // side chart
                            {
                                graphFCol = (GraphStartCol + GraphColCount).ToString(); graphLCol = (GraphStartCol + GraphColCount + 10).ToString();
                                if (!isN)
                                {
                                    SpreadSheet.Row rw = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row - 1);
                                    SpreadSheet.Cell cell = OpenXmlHelper.GetCell(rw, row - 1, GraphStartCol + GraphColCount + 11);
                                    SpreadSheet.Cell cell1 = OpenXmlHelper.GetCell(rw, row - 1, GraphStartCol + GraphColCount + 10);
                                    cell1.CellValue = new SpreadSheet.CellValue(cell.CellValue.InnerText);
                                    cell.CellValue = new SpreadSheet.CellValue("");
                                }
                                firstRow = Information.LBound(v, 1) + tableStartRow;
                                lastRow = (int)worksheetPart.Worksheet.Descendants<SpreadSheet.Row>().LastOrDefault().RowIndex.Value - 1;
                                var rgb = Color.FromArgb(OutputUtil.LINE_COLOR);
                                lineColour = rgb.R.ToString("X2") + rgb.G.ToString("X2") + rgb.B.ToString("X2");
                                ChartPart chartPart = drawingPart.AddNewPart<ChartPart>("rId3");
                                DrawingPart.GenerateGraphDrawingsPart(drawingPart, isN ? (firstRow - 1).ToString() : firstRow.ToString(), lastRow.ToString(), graphFCol, graphLCol, "rId3"
                                                                      , isN ? "BarCluster" : "BarStacked", isGlobalMode: isGlobalMode);
                                if (isN)
                                {
                                    firstCol = lastCol = AvgCol;
                                    SpreadSheet.Row seriesRow = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)firstRow);
                                    SpreadSheet.Cell seriesCell = OpenXmlHelper.GetCell(seriesRow, firstRow, AvgCol);
                                    firstRow += 2;

                                    DrawingPart.GenerateBarClusterGraph(worksheetPart, tmpTable, tmpFormatSheet, lineColour, seriesCell.CellValue.InnerText,
                                                                    chartPart, firstRow, lastRow, firstCol, lastCol, isN, tempTable);
                                    AvgCol = 7;
                                }
                                else
                                {
                                    DrawingPart.GenerateBarStackedGraph(worksheetPart, tmpTable, tmpFormatSheet, lineColour,
                                                                 chartPart, firstRow, lastRow, firstCol, lastCol - tmpTable.Question.SubTotalCnt, isN, tempTable);

                                    chartPart = drawingPart.AddNewPart<ChartPart>("rId4");
                                    DrawingPart.GenerateGraphDrawingsPart(drawingPart, (firstRow - 1).ToString(), firstRow.ToString(), (GraphStartCol + GraphColCount - 1).ToString(), graphLCol, "rId4"
                                                                      , isN ? "BarCluster" : "BarStackedLegend");
                                    DrawingPart.GenerateBarStackedLegend(worksheetPart, tmpTable, tmpFormatSheet, lineColour, ref LegendSeriesText,
                                                                   chartPart, firstRow, lastRow, firstCol, lastCol - tmpTable.Question.SubTotalCnt, isN, tempTable);

                                    SetChartHeight(drawingPart, LegendSeriesText);
                                    LegendSeriesText = null;
                                }
                            }
                        }
                    }

                    if (!isN)
                    {
                        if (Output.MarkingRanking) { RankMarking(ref Ranking, drawingPart, tableStartRow); }
                        if (Output.MarkingColoring) { Hatching(ref HatchingColorIndex, worksheetPart, document, tableStartRow); }
                        if (Output.MarkingSignificance) { SignificanceTestMarking(ref SigTestMarking, worksheetPart, document, tableStartRow); }
                    }

                    if (!isN)
                    {
                        string srcRowOfst = null, srcRow = null, dstRowOfst = null, dstRow = null, minBaseRowOfst = null, minBaseRow = null;
                        string srcColOfst = "0", srcCol = "1", dstColOfst = "695325", dstCol = "1";
                        MinBaseBox = null;
                        s = Array.CreateInstance(typeof(string), 0);
                        cnt = 0; string msg = null;

                        if (Output.MarkingColoring)
                        {
                            var value = LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_CAPTION;
                            if (!isGlobalMode)
                                DrawingPart.GenerateMarkingColoring(drawingPart, Output, value, 28);
                            else
                                DrawingPart.GenerateMarkingColoring(drawingPart, Output, value, 28, srcCol = "1", srcColOfst = "0", srcRow = "27", srcRowOfst = "609600", dstCol = "2", dstColOfst = "450000", dstRow = "27", dstRowOfst = "1447800");
                            cnt++;
                        }
                        if (Output.MarkingRanking)
                        {
                            if (cnt == 1)
                            {
                                srcRowOfst = "38100"; srcRow = "24"; dstRowOfst = "533400"; dstRow = "27";
                            }
                            else
                            {
                                srcRowOfst = "609600"; srcRow = "27"; dstRowOfst = "1447800"; dstRow = "27";
                            }
                            if (!isGlobalMode)
                                DrawingPart.GenerateMarkingRanking(drawingPart, srcRowOfst, srcRow, dstRowOfst, dstRow, srcColOfst, srcCol, dstColOfst, dstCol);
                            else
                                DrawingPart.GenerateMarkingRankingGlobal(drawingPart, srcRowOfst, srcRow, dstRowOfst, dstRow, srcColOfst, srcCol, "300000", "2");

                            cnt++;
                        }

                        if (Output.MarkingSignificance)
                        {
                            int u = -1;
                            v = "".Split();

                            if (cnt == 0)
                            {
                                srcRowOfst = !isGlobalMode ? "625554" : "525554"; srcRow = "27"; dstRowOfst = !isGlobalMode ? "1447800" : "1547800"; dstRow = "27";
                            }
                            else if (cnt == 1)
                            {
                                srcRowOfst = "54054"; srcRow = !isGlobalMode ? "24" : "23"; dstRowOfst = "533400"; dstRow = "27";
                            }
                            else
                            {
                                srcRowOfst = "54054"; srcRow = !isGlobalMode ? "16" : "15"; dstRowOfst = "76200"; dstRow = "23";
                            }
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
                            var val = System.Text.RegularExpressions.Regex.Unescape(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_TO_WHOLE_CAPTION) + "\n" + String.Join("\n", (string[])v);
                            DrawingPart.GenerateSignificanceTestLegend(drawingPart, srcRowOfst, srcRow, dstRowOfst, dstRow, val, 28);
                            cnt++;
                        }
                        string wbString = "";
                        if (Output.MarkingRanking || Output.MarkingColoring || Output.MarkingSignificance || Output.MarkingAscending)
                        {
                            if (Output.MinSamplesCountOnMarking >= 0)
                            {
                                _log.Info("WithStartCell.Worksheet : " + tempTable);

                                if (Output.WBOn)
                                {
                                    wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_AFTER_WB;
                                }
                                if (Output.WBOn && Output.ShowPreWBTotal && Output.PreWbBase)
                                {
                                    wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_BEFORE_WB;
                                }
                                msg = string.Format(LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_PROMPT,
                                    wbString, Output.MinSamplesCountOnMarking.ToString());

                                if (cnt == 1)
                                {
                                    minBaseRowOfst = "380846"; minBaseRow = "27";
                                }
                                else if (cnt == 2)
                                {
                                    minBaseRowOfst = "37946"; minBaseRow = "22";
                                }
                                else if (cnt == 3)
                                {
                                    minBaseRowOfst = "37946"; minBaseRow = simpleAggr ? "14" : "13";
                                }
                                DrawingPart.GenerateMinBaseTextShape(drawingPart, msg, minBaseRowOfst, minBaseRow);
                            }
                        }
                        else
                        {
                            //msg = MINBASE_MSG;
                            msg = string.Format(LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_PROMPT,
                                                  wbString, CurrentOutput.MinSamplesCountOnMarking.ToString());
                            minBaseRowOfst = "380846"; minBaseRow = "27";
                            DrawingPart.GenerateMinBaseTextShape(drawingPart, msg, minBaseRowOfst, minBaseRow);
                        }
                    }
                }
                else
                {
                    //Potrait cross report implementaion
                    bool WholeRowColRef = false;// only for ref 
                    bool CheckOverRowTmpRef = false; // only for ref 
                    nPOICrossCreator.CreatePortraitCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking,
                        ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking, 1 + 1,
                        1 + MaxAxesCountArray[i], HasWeightColumn, HasWeight, isN, tmpTableType, MaxRowsCount,
                        MaxColumnsCount, ref CheckOverRowTmpRef, ref WholeRowColRef, ref OverRowsCount, ref OverColumnsCount);

                    if (tmpTableType == TableType.Per & !isN)
                    {
                        if (OverRowsCount + OverColumnsCount == 0)
                        {
                            CreateGraph = false;
                            LinesIndexList = GetLinesIndexList(tmpTable, CutRowsCol, 1 + 1);
                            if (LinesIndexList == null || LinesIndexList.Count == 0)
                            {
                            }
                            else
                            {
                                HasLines = true;
                            }
                        }
                    }
                    if (tmpTableType == TableType.Per & !isMA) // side chart
                    {
                        if (OverRowsCount + OverColumnsCount == 0)
                        {
                            CreateGraph = false;
                            isSideGraph = false;
                        }
                    }
                    RowNum = 2;

                    worksheetPart = OpenXmlHelper.GetWorksheetPartByName(document, tempTable);
                    if (!isN)
                    {
                        SpreadSheet.Drawing drawing = new SpreadSheet.Drawing() { Id = "rId1" };
                        worksheetPart.Worksheet.Append(drawing);
                        drawingPart = worksheetPart.AddNewPart<DrawingsPart>("rId1");
                    }
                    FormatPortraitTable(ref document, worksheetPart, maxAxisCnt, tmpTable, sht, ref tempTable, ref RowNum, i, ref GraphStartCol,
                                         ref GraphColCount, CutRowsCol, CutColumnsCol, PCutColumn, tmpFormatSheet, FormatRangeNamePrefix, tmpTableType
                                         , HasWeight, StartCell, isN, ref AvgCol, ref tableStartRow, ref isSideGraph, null, true, CutMedian, MedIdx);
                    if (bgWorker.CancellationPending) return;


                    tmp = LocalResource.CR_FILTER_PREFIX + Output.LocalizedFilteringExpression;
                    if (tmp != null && tmp.Length > 0 && IsCheckRefineCondition)
                    {
                        CrossReportHelper.InserStringValue(worksheetPart, tmp, 6, 2);
                    }

                    if (NPOICrossCreator.checkSimpleAggr(tmpTable)
                     && !string.IsNullOrEmpty(tmpTable.Question.QNumber)
                     //&& !string.IsNullOrEmpty(tmpTable.Question.TableHeading) Redmine ID : #211718 
                     && tmpTable.AxesGroups.Count > 1)
                    {
                        ContentsValue.SetValue(tmpTable.Question.QNumber, i, 1);
                        ContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                        ContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                    }
                    else
                    {
                        ContentsValue.SetValue(tmpTable.Question.Name, i, 1);
                        ContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                        ContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                    }

                    ContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                    var range = "'" + tempTable + "'!$A$1";
                    HyperLinkTargetCells.SetValue(range, i, 4);

                    if (Output.WBOn)
                    {
                        string wbstr = LocalResource.WEIGHT_BACK.Insert(LocalResource.WEIGHT_BACK.Length - 1, "[" + tmpTable.Question.WBValue + "]") + '\u2009'
                                        + (tmpTable.Question.TabulateFullQuantity ? LocalResource.WB_TOTAL_NUMBER_BASE : string.Empty);
                        CrossReportHelper.InserStringValue(worksheetPart, wbstr, 29, isSideGraph ? 15 : 2);
                    }
                    else if (tmpTable.Question.TabulateFullQuantity)
                    {
                        CrossReportHelper.InserStringValue(worksheetPart, LocalResource.WB_TOTAL_NUMBER_BASE, 29, isSideGraph ? 15 : 2);
                    }

                    int row = tableStartRow;
                    int fCol = Information.LBound(v, 2) + 1;
                    bool simpleAggr = NPOICrossCreator.checkSimpleAggr(tmpTable);
                    CrossReportHelper.PutPortraitTableValue(worksheetPart, row, fCol, ref v, simpleAggr: simpleAggr, isN: isN, isSideTable: isSideGraph);
                    if (isSideGraph)
                        CrossReportHelper.PutPortraitTableValue(worksheetPart, row, 15, ref v, simpleAggr: simpleAggr, isN: isN, isSideTable: isSideGraph, thisIsSideGraph: true);

                    row = Information.LBound(DataValue, 1) + (NPOICrossCreator.checkSimpleAggr(tmpTable) ? tableStartRow - 2 : tableStartRow);
                    fCol = Information.LBound(DataValue, 2) + 1;
                    CrossReportHelper.PutDataValue(worksheetPart, row, fCol, ref DataValue, thisHasSideGraph: isSideGraph);
                    if (isSideGraph)
                    {
                        CrossReportHelper.PutDataValue(worksheetPart, row, 17, ref DataValue);
                        CreateGraph = true;
                    }

                    if (CreateGraph)
                    {
                        string lineColour = null;
                        DrawingPart.GenerateGraphDrawingsPart(drawingPart, (ReportPortraitHelper.BarClusterGraph.Bar_Cluster_Start_Row == 31 ? "29" : "30"), ReportPortraitHelper.BarClusterGraph.Bar_Cluster_End_Row.ToString(), "3", "12", "rId1", "BarClusterPotrait");
                        ChartPart chartPart = drawingPart.AddNewPart<ChartPart>("rId1");
                        // Plain horizontal bar only — combo scatter/line overlay removed for GWS.
                        DrawingPart.GenerateBarClusterGraphPotrait(worksheetPart, tmpTable, tmpFormatSheet, lineColour, "12", chartPart, ReportPortraitHelper.BarClusterGraph.Bar_Cluster_Start_Row, ReportPortraitHelper.BarClusterGraph.Bar_Cluster_End_Row, 4, 13, false, tempTable);
                    }


                    if (!isN)
                    {
                        if (Output.MarkingRanking)
                        {
                            if (!isSideGraph)
                                RankMarkingPortrait(ref Ranking, drawingPart, tableStartRow, simpleAggr: simpleAggr);
                            else
                                RankMarkingPortrait(ref Ranking, drawingPart, tableStartRow, 13, simpleAggr);
                        }

                        if (Output.MarkingColoring)
                        {
                            if (!isSideGraph)
                                PortraitHatching(ref HatchingColorIndex, worksheetPart, document, tableStartRow, simpleAggr: simpleAggr);
                            else
                                PortraitHatching(ref HatchingColorIndex, worksheetPart, document, tableStartRow, 14, simpleAggr: simpleAggr);
                        }
                        if (Output.MarkingSignificance)
                        {
                            if (!isSideGraph)
                                PortraitSignificanceTestMarking(ref SigTestMarking, worksheetPart, document, tableStartRow, simpleAggr: simpleAggr);
                            else
                                PortraitSignificanceTestMarking(ref SigTestMarking, worksheetPart, document, tableStartRow, 14, simpleAggr: simpleAggr);
                        }
                    }

                    if (!isN)
                    {
                        string srcRowOfst, srcRow, dstRowOfst, dstRow, minBaseRowOfst = null, minBaseRow = null;
                        string srcColOfst, srcCol, dstColOfst, dstCol;
                        MinBaseBox = null;
                        s = Array.CreateInstance(typeof(string), 0);
                        cnt = 0; string msg = null;

                        if (Output.MarkingColoring)
                        {
                            if (!isGlobalMode)
                            {
                                srcCol = CreateGraph ? "14" : "1"; srcColOfst = "0"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = simpleAggr ? "141124" : "188749";
                                dstCol = CreateGraph ? "14" : "1"; dstColOfst = "990600"; dstRow = "28"; dstRowOfst = simpleAggr ? "836449" : "312574";
                            }
                            else
                            {
                                srcCol = CreateGraph ? "14" : "1"; srcColOfst = "0"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = simpleAggr ? "141124" : "250000";
                                dstCol = CreateGraph ? "14" : "1"; dstColOfst = "1250000"; dstRow = "28"; dstRowOfst = simpleAggr ? "950000" : "500000";
                            }
                            var value = LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_CAPTION;//nPOICrossCreator.GetReportKeyword(ReportMessageIndex.ReportMarkingLegendRateDifferenceCaptionIndex);
                            DrawingPart.GenerateMarkingColoring(drawingPart, Output, value, 28, srcCol, srcColOfst, srcRow, srcRowOfst, dstCol, dstColOfst, dstRow, dstRowOfst);
                            cnt++;
                        }
                        if (Output.MarkingRanking)
                        {
                            if (cnt == 1)
                            {
                                if (!isGlobalMode)
                                {
                                    srcCol = CreateGraph ? "14" : "1"; srcColOfst = "1066800"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = simpleAggr ? "141124" : "188749";
                                    dstCol = CreateGraph ? "14" : "1"; dstColOfst = "1762125"; dstRow = "28"; dstRowOfst = simpleAggr ? "836449" : "312574";
                                }
                                else
                                {
                                    srcCol = CreateGraph ? "14" : "1"; srcColOfst = "1300000"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = simpleAggr ? "141124" : "250000";
                                    dstCol = CreateGraph ? "14" : "1"; dstColOfst = "2250000"; dstRow = "28"; dstRowOfst = simpleAggr ? "950000" : "500000";
                                }
                            }
                            else
                            {
                                if (!isGlobalMode)
                                {
                                    srcCol = CreateGraph ? "14" : "1"; srcColOfst = "0"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = simpleAggr ? "141124" : "188749";
                                    dstCol = CreateGraph ? "14" : "1"; dstColOfst = "695325"; dstRow = "28"; dstRowOfst = simpleAggr ? "836449" : "312574";
                                }
                                else
                                {
                                    srcCol = CreateGraph ? "14" : "1"; srcColOfst = "0"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = simpleAggr ? "141124" : "250000";
                                    dstCol = CreateGraph ? "14" : "1"; dstColOfst = "1250000"; dstRow = "28"; dstRowOfst = simpleAggr ? "950000" : "500000";
                                }

                            }

                            if (isGlobalMode)
                                DrawingPart.GenerateMarkingRankingGlobal(drawingPart, srcRowOfst, srcRow, dstRowOfst, dstRow, srcColOfst, srcCol, dstColOfst, dstCol);
                            else
                                DrawingPart.GenerateMarkingRanking(drawingPart, srcRowOfst, srcRow, dstRowOfst, dstRow, srcColOfst, srcCol, dstColOfst, dstCol);
                            cnt++;
                        }

                        if (Output.MarkingSignificance)
                        {
                            int u = -1;
                            v = "".Split();

                            if (cnt == 0)
                            {
                                if (!isGlobalMode)
                                {
                                    srcCol = CreateGraph ? "14" : "1"; srcColOfst = "0"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = "1060847";
                                    dstCol = CreateGraph ? "14" : "1"; dstColOfst = "1070600"; dstRow = "28"; dstRowOfst = simpleAggr ? "836449" : "536449";
                                }
                                else
                                {
                                    srcCol = CreateGraph ? "14" : "1"; srcColOfst = "0"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = "250000";
                                    dstCol = CreateGraph ? "14" : "1"; dstColOfst = "1250000"; dstRow = "28"; dstRowOfst = simpleAggr ? "950000" : "500000";
                                }
                            }
                            else if (cnt == 1)
                            {
                                if (!isGlobalMode)
                                {
                                    srcCol = CreateGraph ? "14" : "1"; srcColOfst = "1066800"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = "141124";
                                    dstCol = CreateGraph ? "14" : "1"; dstColOfst = "2127647"; dstRow = "28"; dstRowOfst = simpleAggr ? "836449" : "312574";
                                }
                                else
                                {
                                    srcCol = CreateGraph ? "14" : "1"; srcColOfst = "1300000"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = "250000";
                                    dstCol = CreateGraph ? "14" : "1"; dstColOfst = "2600000"; dstRow = "28"; dstRowOfst = simpleAggr ? "950000" : "500000";

                                }
                            }
                            else
                            {
                                if (!isGlobalMode)
                                {
                                    srcCol = CreateGraph ? "14" : "1"; srcColOfst = "1838325"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = simpleAggr ? "139933" : "187558";
                                    dstCol = CreateGraph ? "14" : "1"; dstColOfst = "2999172"; dstRow = "28"; dstRowOfst = simpleAggr ? "836449" : "312574";
                                }
                                else
                                {
                                    srcCol = CreateGraph ? "14" : "1"; srcColOfst = "2300000"; srcRow = simpleAggr ? "27" : "26"; srcRowOfst = simpleAggr ? "139933" : "250000";
                                    dstCol = CreateGraph ? "15" : "2"; dstColOfst = "250000"; dstRow = "28"; dstRowOfst = simpleAggr ? "950000" : "500000";
                                }
                            }
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
                            var val = System.Text.RegularExpressions.Regex.Unescape(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_TO_WHOLE_CAPTION) + "\n" + String.Join("\n", (string[])v);//nPOICrossCreator.GetUnescapedReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeCaptionIndex) + "\n" + String.Join("\n", (string[])v);
                            DrawingPart.GenerateSignificanceTestLegend(drawingPart, srcRowOfst, srcRow, dstRowOfst, dstRow, val, 28, srcCol, srcColOfst, dstCol, dstColOfst);
                            cnt++;
                        }
                        string wbString = "";
                        if (Output.MarkingRanking || Output.MarkingColoring || Output.MarkingSignificance || Output.MarkingAscending)
                        {
                            if (Output.MinSamplesCountOnMarking >= 0)
                            {
                                _log.Info("WithStartCell.Worksheet : " + tempTable);

                                if (Output.WBOn)
                                {
                                    wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_AFTER_WB;//nPOICrossCreator.GetReportKeyword(ReportMessageIndex.ReportMarkingLegendMinBaseAfterWB);
                                }
                                if (Output.WBOn && Output.ShowPreWBTotal && Output.PreWbBase)
                                {
                                    wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_BEFORE_WB;//nPOICrossCreator.GetReportKeyword(ReportMessageIndex.ReportMarkingLegendMinBaseBeforeWB);
                                }
                                msg = string.Format(LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_PROMPT,//nPOICrossCreator.GetReportKeyword(ReportMessageIndex.ReportMarkingLegendMinBasePromptIndex,
                                    wbString, Output.MinSamplesCountOnMarking.ToString());


                                minBaseRowOfst = "46200"; minBaseRow = "26"; srcCol = CreateGraph ? "14" : "1"; ; srcColOfst = "9525";
                                DrawingPart.GenerateMinBaseTextShape(drawingPart, msg, minBaseRowOfst, minBaseRow, srcCol, srcColOfst);
                            }
                        }
                        else
                        {
                            //msg = MINBASE_MSG;
                            msg = string.Format(LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_PROMPT,//nPOICrossCreator.GetReportKeyword(ReportMessageIndex.ReportMarkingLegendMinBasePromptIndex,
                                                  wbString, CurrentOutput.MinSamplesCountOnMarking.ToString());
                            minBaseRowOfst = "76200"; minBaseRow = "26"; srcCol = CreateGraph ? "14" : "1"; ; srcColOfst = "9525";
                            DrawingPart.GenerateMinBaseTextShape(drawingPart, msg, minBaseRowOfst, minBaseRow, srcCol, srcColOfst);
                        }
                    }
                }
                isSideGraph = false;
            }
            var wrksheet = OpenXmlHelper.GetWorksheetPartByName(document, "INDEX");
            PutContents(wrksheet, ref ContentsValue, ref HyperLinkTargetCells, 10);
        }
        public void CreateCrossSignificance(OutputCross Output, SpreadsheetDocument document)
        {
            int SigCutColumn = 0;
            int PCutColumn = 0;
            string StartCell = null;
            string tmpFormatSheet = null;
            string NPerFormatSheet = null;
            string NPerDoubleFormatSheet = null;
            string NFormatSheet = null;
            string NDoubleFormatSheet = null;
            string PerFormatSheet = null;
            string PerDoubleFormatSheet = null;
            string SigTestPerFormatSheet = null;
            string SigTestPerDoubleFormatSheet = null;
            string FormatSheet = null;
            bool HasWeightColumn = false;
            int m = 0;
            bool HasWeightBack;
            bool HasWeight;
            Hashtable CutRowsCol = null;
            Hashtable CutColumnsCol = null;
            string FormatRangeNamePrefix;
            string tempTable = null;
            string ContentsTempSheet = null;
            CrossTable tmpTable = null;
            string ReportTitle;
            List<string> NPerBooks = null;
            List<string> NBooks = null;
            List<string> PerBooks = null;
            List<string> SigTestBooks = null;
            Array NPerContentsValue = null;  //string
            Array NContentsValue = null; // string
            Array PerContentsValue = null;  //string
            Array SigTestContentsValue = null; // string
            Array NPerHyperLinkTargetCells = null; // Range
            Array NHyperLinkTargetCells = null;  //Range
            Array PerHyperLinkTargetCells = null; // Range
            Array SigTestHyperLinkTargetCells = null;  //Range
            Array ContentsValue = null;  //string
            Array HyperLinkTargetCells = null; // Range
            string NPerContentsSheet = null;
            string NContentsSheet = null;
            string PerContentsSheet = null;
            string SigTestContentsSheet = null;
            List<string> NPerOrgSheets = null;
            List<string> NOrgSheets = null;
            List<string> PerOrgSheets = null;
            List<string> SigTestOrgSheets = null;
            //Workbook NewBook = null;
            string sht = null;
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
            bool CheckOverRow = false;
            bool CheckOverColumn = false;

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
            int GraphStartCol = 0;
            string errBuf;
            int maxAxisCnt = 0;
            int RowNum = 2;
            int GraphColCount = 0;
            int AvgCol = 0;
            int tableStartRow = 0;
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

            WorksheetPart worksheetIndexPart = null;
            WorksheetPart worksheetPart = null;
            summaryCreatorXml = new SummaryCreatorXml();
            nPOICrossCreator = new NPOICrossCreator();
            ReportSigTemplate reportSigTemplate = new ReportSigTemplate();
            ReportPortraitSigTemplate reportPortraitSigTemplate = new ReportPortraitSigTemplate();
            CurrentOutput = Output;
            onlySigPage = true;
            nPOICrossCreator.onlySigPage = true;
            nPOICrossCreator.CurrentOutput = Output;
            nPOICrossCreator.BookPSWD = BookPSWD;
            nPOICrossCreator.SheetPSWD = SheetPSWD;
            nPOICrossCreator.xlApp = xlApp;
            nPOICrossCreator.bgWorker = bgWorker;
            ContentsTempSheet = "INDEX";
            try
            {
                if (CurrentOutput.Orientation == TableOrientation.Landscape)
                    reportSigTemplate.GenerateWorkbookPart(document.AddWorkbookPart());
                else
                    reportPortraitSigTemplate.GenerateWorkbookPart(document.AddWorkbookPart());
                worksheetIndexPart = OpenXmlHelper.GetWorksheetPartByName(document, ContentsTempSheet);

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
                    maxAxisCnt = maxAxisCnt | MaxAxesCountArray[i];
                    if (!HasWeightColumn) { HasWeightColumn = nPOICrossCreator.GetHasWeight(tmpTable); }
                }

                if (HasOutputPerTable || SigTestOn)
                {
                    if ((maxAxisCnt & 2) == 2)
                    { //' 三重あり
                        //if (HasOutputPerTable) { PerFormatSheet = FormatBookWorksheets.Item["P_Std"]; }
                        if (SigTestOn) { SigTestPerFormatSheet = "P_Sig"; }
                        AdjustFormat(document, FormatSheet, SigTestPerFormatSheet, 2, ref SigCutColumn, HasWeightColumn);
                    }
                    if ((maxAxisCnt & 1) == 1)
                    { //' 二重あり
                        if ((m & 2) == 0)
                        {
                            //if (HasOutputPerTable) { PerDoubleFormatSheet = FormatBookWorksheets.Item["P_Std"]; }
                            if (SigTestOn) { SigTestPerDoubleFormatSheet = "P_Sig"; }
                        }
                        else
                        {
                            if (HasOutputPerTable)
                            {

                            }
                            if (SigTestOn)
                            {
                                SigTestPerDoubleFormatSheet = SigTestPerFormatSheet;
                            }
                        }
                        if (IsOrientationLandscape)
                            AdjustFormat(document, FormatSheet, SigTestPerDoubleFormatSheet, 2, ref SigCutColumn, HasWeightColumn);
                        // AdjustFormat(PerDoubleFormatSheet, SigTestPerDoubleFormatSheet, 1, HasWeightColumn, PerFormatSheet != null || SigTestPerFormatSheet != null);
                    }
                }
                ReportTitle = CurrentOutput.ParentRequest.Title;
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
                    if (bgWorker.CancellationPending) return;
                    updateProgress(currentProgress, String.Format(LocalResource.PB_EXCEL_GEN_TABLE, (i + 1), CurrentOutput.Tables.Count));
                    currentProgress += progressStep;
                    tmpTable = (CrossTable)CurrentOutput.Tables[i];
                    isN = (tmpTable.Question.QuestionType & QuestionType.N) == QuestionType.N;
                    HasWeight = nPOICrossCreator.GetHasWeight(tmpTable);
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
                    //nPOICrossCreator.GetCutRowsAndColumns(tmpTable, HasWeightBack, HasWeight, MaxAxesCountArray[i],
                    //                                      ref CutRowsCol, ref CutColumnsCol, ref MedIdx, true, CutMedian);
                    nPOICrossCreator.GetCutRowsAndColumns(tmpTable, HasWeightBack, HasWeight,
                        MaxAxesCountArray[i], ref CutRowsCol, ref CutColumnsCol, ref medIdx, false, CutMedian, WholeRowCol);
                    if (SigTestOn)
                    {
                        if (NPOICrossCreator.checkSimpleAggr(tmpTable) && !isN)
                        {
                            FormatRangeNamePrefix = FormatRangeNamePrefix + "_NP";
                        }
                        if (Output.Orientation == TableOrientation.Landscape)
                        {
                            if (MaxAxesCountArray[i] == 2)
                            {
                                tempTable = "TripleSignificanceTest";
                                CreateNewSheet(tmpTable, worksheetIndexPart, ContentsTempSheet, ref SigTestContentsValue, ref HyperLinkTargetCells, TableType.SignificanceTest,
                                               null, document, tempTable, i, SigTestOn);
                                tmpFormatSheet = FormatSheet;
                            }
                            else
                            {
                                tempTable = "DoubleSignificanceTest";
                                CreateNewSheet(tmpTable, worksheetIndexPart, ContentsTempSheet, ref SigTestContentsValue, ref HyperLinkTargetCells, TableType.SignificanceTest,
                                               null, document, tempTable, i, SigTestOn);
                                tmpFormatSheet = DoubleFormatSheet;
                            }
                        }
                        else
                        {
                            if (MaxAxesCountArray[i] == 2)
                            {
                                tempTable = "TripleSignificanceTest";
                                CreateNewSheetPortrait(tmpTable, worksheetIndexPart, ContentsTempSheet, ref SigTestContentsValue, ref HyperLinkTargetCells, TableType.SignificanceTest,
                                               null, document, tempTable, i, SigTestOn);
                                tmpFormatSheet = FormatSheet;
                            }
                            else
                            {
                                tempTable = "DoubleSignificanceTest";
                                CreateNewSheetPortrait(tmpTable, worksheetIndexPart, ContentsTempSheet, ref SigTestContentsValue, ref HyperLinkTargetCells, TableType.SignificanceTest,
                                               null, document, tempTable, i, SigTestOn);
                                tmpFormatSheet = DoubleFormatSheet;
                            }
                        }
                        RowNum = 1;
                        worksheetPart = OpenXmlHelper.GetWorksheetPartByName(document, tempTable);
                        //CreateNewSheet(tmpTable, worksheetIndexPart, ContentsTempSheet, ref SigTestContentsValue, ref HyperLinkTargetCells,
                        //            null, document, tempTable, i, SigTestOn);        
                        CrossCreator crossCreator = new CrossCreator();
                        if (IsOrientationLandscape)
                        {
                            CheckOverRow = true;
                            CheckOverColumn = false;
                            OverColumnsCount = 0;
                            int OverRowssCountTmpRef = 0; // only for ref 
                            nPOICrossCreator.CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd,
                                ref SigTestMarking, 2, //wt
                                1 + MaxAxesCountArray[i], HasWeight, isN, TableType.SignificanceTest
                                 , MaxRowsCount, MaxColumnsCount, ref CheckOverRow, WholeRowCol, ref OverRowssCountTmpRef, ref OverColumnsCount);
                            if (OverColumnsCount > 0)
                            {
                                tableTypeBuf = LocalResource.REPORT_SIGNIFICANCE_TEST_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                            }
                        }
                        else
                        {
                            //Potrait cross report implementaion
                            bool WholeRowColRef = false;// only for ref 
                            bool CheckOverRowTmpRef = false; // only for ref 
                            OverColumnsCount = 0;
                            int OverRowssCountTmpRef = 0;
                            nPOICrossCreator.CreatePortraitCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking,
                        ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking, 1 + 1,
                        1 + MaxAxesCountArray[i], HasWeightColumn, HasWeight, isN, TableType.SignificanceTest, MaxRowsCount,
                        MaxColumnsCount, ref CheckOverRowTmpRef, ref WholeRowColRef, ref OverRowssCountTmpRef, ref OverColumnsCount);
                        }
                        if (NPOICrossCreator.checkSimpleAggr(tmpTable) && !string.IsNullOrEmpty(tmpTable.Question.QNumber) /*&& !string.IsNullOrEmpty(tmpTable.Question.TableHeading)*/ && tmpTable.AxesGroups.Count > 1)
                        {
                            SigTestContentsValue.SetValue(tmpTable.Question.QNumber, i, 1);
                            SigTestContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                            SigTestContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                        }
                        else
                        {
                            SigTestContentsValue.SetValue(tmpTable.Question.Name, i, 1);
                            SigTestContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                            SigTestContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                        }
                        if (CheckOverRow || CheckOverColumn)
                        {
                            xlApp.DisplayAlerts = false;
                            //sht.Delete();
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

                                FormatLandscapeTable(ref document, worksheetPart, maxAxisCnt, tmpTable, sht, ref tempTable, ref RowNum, i, ref GraphStartCol,
                                      ref GraphColCount, CutRowsCol, CutColumnsCol, SigCutColumn, FormatSheet, FormatRangeNamePrefix, TableType.SignificanceTest
                                      , HasWeight, StartCell, isN, ref AvgCol, 0, false, ContentsTempSheet, true, CutMedian, medIdx);
                            }
                            else
                            {
                                bool isSideGraph = false;
                                FormatPortraitTable(ref document, worksheetPart, maxAxisCnt, tmpTable, sht, ref tempTable, ref RowNum, i, ref GraphStartCol,
                                         ref GraphColCount, CutRowsCol, CutColumnsCol, PCutColumn, FormatSheet, FormatRangeNamePrefix, TableType.SignificanceTest
                                         , HasWeight, StartCell, isN, ref AvgCol, ref tableStartRow, ref isSideGraph, null, true, CutMedian, medIdx);
                            }

                            SigTestContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                            HyperLinkTargetCells.SetValue("\'" + tempTable + "\'!$A$1", i, 4);

                            if (IsOrientationLandscape)
                            {
                                int row = Information.LBound(v, 1) + 1;
                                int fCol = Information.LBound(v, 2) + 2;
                                CrossReportHelper.PutValue(worksheetPart, row, fCol, ref v);
                                row = Information.LBound(DataValue, 1) + 1;
                                fCol = Information.LBound(DataValue, 2) + 2;
                                CrossReportHelper.PutValue(worksheetPart, row, fCol, ref DataValue);
                            }
                            else
                            {
                                bool isSimpleAggr = NPOICrossCreator.checkSimpleAggr(tmpTable);
                                int row = Information.LBound(v, 1) + 1;
                                int fCol = Information.LBound(v, 2) + 2;
                                CrossReportHelper.PutPortraitTableValue(worksheetPart, row, fCol, ref v, simpleAggr: isSimpleAggr, isN: isN, isSigTest: true);
                                row = Information.LBound(DataValue, 1) + 2;
                                fCol = Information.LBound(DataValue, 2) + 2;
                                CrossReportHelper.PutDataValue(worksheetPart, row, fCol, ref DataValue);
                            }
                        }
                    }
                }
                int strtRow = 14;
                summaryCreatorXml.PutContents(worksheetIndexPart, ref SigTestContentsValue, ref HyperLinkTargetCells, strtRow);
            }
            catch (Exception ex)
            {

            }
        }
        private void PutContents(WorksheetPart worksheetPart,
              ref Array ContentsValue
            , ref Array HyperlinkTargetCells, int row //Excel.Range 
           )
        {
            int i, j;
            int r = 0;
            SpreadSheet.Hyperlinks hyperlinks = new SpreadSheet.Hyperlinks();
            CrossReportHelper.PutValue(worksheetPart, row, 2, ref ContentsValue);
            for (i = HyperlinkTargetCells.GetLowerBound(0); i <= HyperlinkTargetCells.GetUpperBound(0); i++)
            {
                r = r + 1;
                for (j = HyperlinkTargetCells.GetLowerBound(1); j <= HyperlinkTargetCells.GetUpperBound(1); j++)
                {
                    if (HyperlinkTargetCells.GetValue(i, j) != null)
                    {
                        SpreadSheet.Hyperlink hyperlink = new SpreadSheet.Hyperlink() { Reference = "E" + (row + i), Location = (string)HyperlinkTargetCells.GetValue(i, j) };
                        hyperlinks.Append(hyperlink);
                    }
                }
            }

            var mergeCells = worksheetPart.Worksheet.Descendants<SpreadSheet.MergeCells>().FirstOrDefault();
            worksheetPart.Worksheet.InsertAfter(hyperlinks, mergeCells);
        }
        private void SetChartHeight(DrawingsPart drawingsPart,
                                   string LegendSeriesText)
        {
            int legendLine = LegendSeriesText == null ? 0 : GetLegendLine(LegendSeriesText);
            Xdr.TwoCellAnchor twoCellAnchor = OpenXmlHelper.GetLastCellAnchor(drawingsPart);
            legendLine = legendLine >= 18 ? 18 : legendLine;
            if (legendLine > 1)
                twoCellAnchor.FromMarker.RowOffset.Text = (SrcGraphOffset - (308990 * (legendLine - 1))).ToString();
        }
        private int GetLegendLine(string legend)
        {
            return (int)OpenXmlHelper.FindLine(legend, LegendWidthDef);
        }
        public void FormatLandscapeTable(ref SpreadsheetDocument reportDocument, WorksheetPart worksheetPart, int maxAxisCnt,
              CrossTable Table
             , string TemplateSheet, ref string tempTable, ref int RowNum, int sheetNum, ref int GraphStartCol, ref int GraphColCount
             , Hashtable CutRowsCol, Hashtable CutColumnsCol, int CutColumn
             , string FormatSheet, string FormatRangeNamePrefix
             , TableType TableType, bool HasWeight
             , string StartCell, bool isN, ref int AvgCol, int tableStartRow, bool isSideGraph = false
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
            string tmp;
            int idx = 0;
            int tmpR = 0;
            int y = 0;
            int tmpY = 0;
            int fRow = 0;
            int lRow = 0;
            int TableRowCount = 0;
            Array tmpBuf; //string
            string tmpAddress;
            bool f = false;
            bool IsSigTest = false;
            TableType tType;
            int dd = 0;
            string tmpHeaderRange;
            string rng = null;
            ReportPerTable reportPerTable = new ReportPerTable();
            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            HasNAColumn = CurrentOutput.ShowNAAtItem;
            HasIVColumn = CurrentOutput.ShowIVAtItem;
            HasNARow = CurrentOutput.ShowNAAtAxis;
            HasIVRow = CurrentOutput.ShowIVAtAxis;
            d = 1 + (NPOICrossCreator.ToInt(!isN & TableType == TableType.NPer) & 1);
            d2 = d + (NPOICrossCreator.ToInt(!isN & IsSigTest) & 1);
            //WorksheetPart worksheetPart = SummaryHelper.GetWorksheetPartByName(SummaryDocument, TemplateSheet);
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

                if (c + ItemSectorsCount - 1 > MaxColumnsCount)
                {
                    ItemSectorsCount = MaxColumnsCount - c + 1;
                    CutNAColumn = HasNAColumn;
                    CutIVColumn = HasIVColumn;
                    CutWTColumns = HasWeight;
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
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (NPOICrossCreator.ToInt(HasWeight) & 2)))
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
            fRow = RowNum;
            if (FormatSheet == "Cross_P_Std")
            {
                reportPerTable.GenerateCrossPerTable(CurrentOutput, ref worksheetPart, ref RowNum, maxAxisCnt, CutNAColumn, CutIVColumn, ref AvgCol, isSideGraph, tableStartRow,
                                                 ItemSectorsCount, Table, HasNARow, HasIVRow, CutRowsCol, FormatRangeNamePrefix, CutMedian, ref GraphColCount, ref GraphStartCol);
                SetSheetProperty(reportDocument, worksheetPart, Table, sheetNum, IsSigTest, ref tempTable);
            }
            else if (FormatSheet == "P_Sig")
            {
                ReportPerSigTable reportPerSigTable = new ReportPerSigTable();
                reportPerSigTable.GenerateCrossPerSigTable(CurrentOutput, ref worksheetPart, ref RowNum, maxAxisCnt, CutNAColumn, CutIVColumn,
                                                 ItemSectorsCount, Table, HasNARow, HasIVRow, CutRowsCol, FormatRangeNamePrefix, CutMedian);
                SetSheetProperty(reportDocument, worksheetPart, Table, sheetNum, IsSigTest, ref tempTable);
            }
            lRow = RowNum;
            TableRowCount = lRow - fRow;
            if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                {
                    tmpAddress = "'" + ContentsSheet + "'!$A$1";
                    tmpBuf = Array.CreateInstance(typeof(string), new int[] { TableRowCount, 1 },
                        new int[] { 1, 0 });

                    for (i = 1; i <= TableRowCount - 1; i++)
                    {
                        tmpBuf.SetValue("", i, 0);
                    }
                    tmpBuf.SetValue(LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME, TableRowCount, 0);

                    SpreadSheet.Hyperlinks hyperlinks = worksheetPart.Worksheet.Descendants<SpreadSheet.Hyperlinks>().FirstOrDefault();
                    SpreadSheet.Hyperlink hyperlink = new SpreadSheet.Hyperlink() { Reference = "A1:A" + TableRowCount, Location = "\'INDEX\'!$A$1", Display = "\'INDEX\'!$A$1" };
                    hyperlinks.Append(hyperlink);

                    SpreadSheet.Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)TableRowCount);
                    SpreadSheet.Cell cell = OpenXmlHelper.GetCell(row, TableRowCount, 1);
                    object val = tmpBuf.GetValue(TableRowCount, 0);
                    cell.CellValue = new SpreadSheet.CellValue((string)val);
                    cell.DataType = SpreadSheet.CellValues.String;
                }
            }
        }

        public void FormatPortraitTable(ref SpreadsheetDocument reportDocument, WorksheetPart worksheetPart, int maxAxisCnt,
               CrossTable Table
             , string TemplateSheet, ref string tempTable, ref int RowNum, int sheetNum, ref int GraphStartCol, ref int GraphColCount
             , Hashtable CutRowsCol, Hashtable CutColumnsCol, int CutColumn
             , string FormatSheet, string FormatRangeNamePrefix
             , TableType TableType, bool HasWeight
             , string StartCell, bool isN, ref int AvgCol, ref int tableStartRow, ref bool isSideGraph
             , string ContentsSheet = null
             , bool isReport = false
             , bool CutMedian = false, int MedIdx = -1
             , Hashtable WholeRowCol = null,
                        Array Ranking = null,
                                    Array TableValue = null)
        {
            bool HasNARow = false;
            bool HasIVRow = false;
            bool HasNAColumn = false;
            bool HasIVColumn = false;
            int d = 0;
            bool CutNARow = false;
            bool CutIVRow = false;
            //bool CutWTRows = false;
            int ItemSectorsCount = 0;
            int[] SectorsCount = new int[2];
            bool CutNA = false;
            bool CutIV = false;
            bool IsSigTest = false;
            int fRow = 0;
            int lRow = 0;
            int TableRowCount = 0;
            Array tmpBuf;
            string tmpAddress;
            TableType tType;

            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            HasNARow = CurrentOutput.ShowNAAtItem;
            HasIVRow = CurrentOutput.ShowIVAtItem;
            HasNAColumn = CurrentOutput.ShowNAAtAxis;
            HasIVColumn = CurrentOutput.ShowIVAtAxis;

            d = 1 + (NPOICrossCreator.ToInt(!isN) & ((NPOICrossCreator.ToInt(tType == TableType.NPer) & 1) + (NPOICrossCreator.ToInt(IsSigTest) & 1)));

            if (isN)
            {
                if (HasNARow)
                {
                    CutNARow = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum);
                }
                if (CutMedian)
                {
                    CutMedian = MedIdx >= Table.GetTableValueColumnIndexMinimum;
                }
            }
            else
            {
                CutMedian = false;
                ItemSectorsCount = Table.SectorsCount;
                if (HasNARow && !CutNARow)
                {
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (NPOICrossCreator.ToInt(HasWeight) & 2) - (Table.Question.SubTotalCnt)))
                    {
                        CutNARow = true;
                    }
                }
            }

            fRow = RowNum;
            if (FormatSheet == "Cross_P_Std")
            {
                ReportPortraitPerTable reportPortraitPerTable = new ReportPortraitPerTable();
                reportPortraitPerTable.GenerateCrossPerTable(CurrentOutput, ref worksheetPart, ref RowNum, maxAxisCnt, CutNARow, CutIVRow, ref AvgCol, ref isSideGraph, ref tableStartRow,
                                                 ItemSectorsCount, Table, HasNARow, HasIVRow, CutRowsCol, FormatRangeNamePrefix, CutMedian, ref GraphColCount, ref GraphStartCol, isReport);
                SetSheetProperty(reportDocument, worksheetPart, Table, sheetNum, IsSigTest, ref tempTable);
            }
            else if (FormatSheet == "P_Sig")
            {
                ReportPortraitPerSigTable reportPerSigTable = new ReportPortraitPerSigTable();
                reportPerSigTable.GenerateCrossPerSigTable(CurrentOutput, ref worksheetPart, ref RowNum, maxAxisCnt, CutNARow, CutIVRow, ref AvgCol, ref isSideGraph, ref tableStartRow,
                                                 ItemSectorsCount, Table, HasNARow, HasIVRow, CutRowsCol, FormatRangeNamePrefix, CutMedian, ref GraphColCount, ref GraphStartCol, isReport);
                SetSheetProperty(reportDocument, worksheetPart, Table, sheetNum, IsSigTest, ref tempTable);
            }

            lRow = RowNum;
            TableRowCount = lRow - fRow;
            if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                {
                    tmpAddress = "'" + ContentsSheet + "'!$A$1";
                    tmpBuf = Array.CreateInstance(typeof(string), new int[] { TableRowCount, 1 },
                        new int[] { 1, 0 });

                    for (int i = 1; i <= TableRowCount - 1; i++)
                    {
                        tmpBuf.SetValue("", i, 0);
                    }
                    tmpBuf.SetValue(LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME, TableRowCount, 0);

                    //SpreadSheet.Hyperlinks hyperlinks = worksheetPart.Worksheet.Descendants<SpreadSheet.Hyperlinks>().FirstOrDefault();
                    //SpreadSheet.Hyperlink hyperlink = new SpreadSheet.Hyperlink() { Reference = "A1:A" + TableRowCount, Location = "\'INDEX\'!$A$1", Display = "\'INDEX\'!$A$1" };
                    //hyperlinks.Append(hyperlink);

                    SpreadSheet.Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)TableRowCount);
                    SpreadSheet.Cell cell = OpenXmlHelper.GetCell(row, TableRowCount, 1);
                    object val = tmpBuf.GetValue(TableRowCount, 0);
                    cell.CellValue = new SpreadSheet.CellValue((string)val);
                    cell.DataType = SpreadSheet.CellValues.String;
                }
            }
        }

        public void Hatching(ref Array HatchingColorIndex, WorksheetPart worksheetPart, SpreadsheetDocument document, int tableStartRow)
        {
            int r, c; string rgb = null;
            for (r = HatchingColorIndex.GetLowerBound(0); r <= HatchingColorIndex.GetUpperBound(0); r++)
            {
                for (c = HatchingColorIndex.GetLowerBound(1); c <= HatchingColorIndex.GetUpperBound(1); c++)
                {
                    switch (HatchingColorIndex.GetValue(r, c))
                    {
                        case null:
                            break;
                        default:
                            var clr = System.Drawing.Color.FromArgb((int)HatchingColorIndex.GetValue(r, c));
                            rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                            SpreadSheet.Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)(r + tableStartRow));
                            SpreadSheet.Cell cell = OpenXmlHelper.GetCell(row, r + tableStartRow, 1 + c);
                            OpenXmlHelper.FillCellForgroundColor(document, cell, rgb);
                            break;
                    }
                }
            }
            SummaryCreatorXml.FillColors.Clear();
        }

        public void PortraitHatching(ref Array HatchingColorIndex, WorksheetPart worksheetPart, SpreadsheetDocument document, int tableStartRow, int columnStart = 1, bool simpleAggr = false)
        {
            int r, c; string rgb = null;
            for (r = HatchingColorIndex.GetLowerBound(0); r <= HatchingColorIndex.GetUpperBound(0); r++)
            {
                for (c = HatchingColorIndex.GetLowerBound(1); c <= HatchingColorIndex.GetUpperBound(1); c++)
                {
                    switch (HatchingColorIndex.GetValue(r, c))
                    {
                        case null:
                            break;
                        default:
                            var clr = System.Drawing.Color.FromArgb((int)HatchingColorIndex.GetValue(r, c));
                            rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                            SpreadSheet.Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)(simpleAggr ? (r - 3 + tableStartRow) : (r + tableStartRow)));
                            SpreadSheet.Cell cell = OpenXmlHelper.GetCell(row, simpleAggr ? (r - 3 + tableStartRow) : (r + tableStartRow), columnStart + c);
                            OpenXmlHelper.FillCellForgroundColor(document, cell, rgb);
                            break;
                    }
                }
            }
            SummaryCreatorXml.FillColors.Clear();
        }
        public void SignificanceTestMarking(ref Array SigTestMarking, WorksheetPart worksheetPart, SpreadsheetDocument document, int tableStartRow)
        {
            int y, x;
            string buf;
            string fmt;
            bool isGlobalMode = QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? false : true;
            for (y = SigTestMarking.GetLowerBound(0); y <= SigTestMarking.GetUpperBound(0); y++)
            {
                for (x = SigTestMarking.GetLowerBound(1); x <= SigTestMarking.GetUpperBound(1); x++)
                {
                    buf = (string)SigTestMarking.GetValue(y, x);
                    if (null != buf && buf.Length > 0)
                    {
                        if (isGlobalMode)
                        {
                            buf = NPOICrossCreator.loadGlobalModeSymbol(buf);
                        }
                        SpreadSheet.Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)(y + tableStartRow));
                        SpreadSheet.Cell cell = OpenXmlHelper.GetCell(row, y + tableStartRow, 1 + x);
                        OpenXmlHelper.SetSignificanceTestMarking(document, cell, buf);
                    }
                }
            }
        }

        public void PortraitSignificanceTestMarking(ref Array SigTestMarking, WorksheetPart worksheetPart, SpreadsheetDocument document, int tableStartRow, int columnStart = 1, bool simpleAggr = false)
        {
            int y, x;
            string buf;
            string fmt;
            bool isGlobalMode = QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? false : true;
            for (y = SigTestMarking.GetLowerBound(0); y <= SigTestMarking.GetUpperBound(0); y++)
            {
                for (x = SigTestMarking.GetLowerBound(1); x <= SigTestMarking.GetUpperBound(1); x++)
                {
                    buf = (string)SigTestMarking.GetValue(y, x);
                    if (null != buf && buf.Length > 0)
                    {
                        if (isGlobalMode)
                        {
                            buf = NPOICrossCreator.loadGlobalModeSymbol(buf);
                        }
                        SpreadSheet.Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)(simpleAggr ? (y - 3 + tableStartRow) : (y + tableStartRow)));
                        SpreadSheet.Cell cell = OpenXmlHelper.GetCell(row, simpleAggr ? (y - 3 + tableStartRow) : (y + tableStartRow), columnStart + x);
                        OpenXmlHelper.SetSignificanceTestMarking(document, cell, buf);
                    }
                }
            }
        }
        public void RankMarking(ref Array Ranking, Dr.DrawingsPart drawingsPart, int tableStartRow)
        {
            int r, c;
            string rgb;
            Color clr;
            for (r = Ranking.GetLowerBound(0); r <= Ranking.GetUpperBound(0); r++)
            {
                for (c = Ranking.GetLowerBound(1); c <= Ranking.GetUpperBound(1); c++)
                {
                    switch (Ranking.GetValue(r, c))
                    {
                        case 1:
                            clr = Color.FromArgb(0XFF);
                            //clr = 0XFF;//red
                            break;
                        case 2:
                            clr = Color.FromArgb(0XFF0000);
                            //clr = 0XFF0000;//blue
                            break;
                        case 3:
                            clr = Color.FromArgb(0X339966);
                            //clr = 0X339966;
                            break;
                        default:
                            continue;
                    }
                    rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                    DrawingPart.GenerateDrawingShapeOval(drawingsPart, (r + tableStartRow - 1).ToString(), c.ToString(), rgb);
                }
            }
        }

        public void RankMarkingPortrait(ref Array Ranking, Dr.DrawingsPart drawingsPart, int tableStartRow, int columnStart = 0, bool simpleAggr = true)
        {
            int r, c;
            string rgb;
            Color clr;
            for (r = Ranking.GetLowerBound(0); r <= Ranking.GetUpperBound(0); r++)
            {
                for (c = Ranking.GetLowerBound(1); c <= Ranking.GetUpperBound(1); c++)
                {
                    switch (Ranking.GetValue(r, c))
                    {
                        case 1:
                            clr = Color.FromArgb(0XFF);
                            //clr = 0XFF;//red
                            break;
                        case 2:
                            clr = Color.FromArgb(0XFF0000);
                            //clr = 0XFF0000;//blue
                            break;
                        case 3:
                            clr = Color.FromArgb(0X339966);
                            //clr = 0X339966;
                            break;
                        default:
                            continue;
                    }
                    rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                    DrawingPart.GenerateDrawingShapeOval(drawingsPart, simpleAggr ? ((r - (Ranking.GetLowerBound(0))) + tableStartRow).ToString() : (r + tableStartRow - 1).ToString(), (columnStart + c).ToString(), rgb);
                }
            }
        }

        public static void ArrayPreserve(ref Array v, Type type, int u)
        {
            Array t = (Array)v.Clone();
            v = Array.CreateInstance(type, u + 1);
            t.CopyTo(v, 0);
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
            uint formatId = 169;
            bool RedrawBorder;
            int[] weightStyleIndexArray = null;

            int[] numericStyleIndexArray = null;
            IsPortrait = CurrentOutput.Orientation == TableOrientation.Portrait;
            SpreadSheet.NumberingFormats numberingFormats = document.WorkbookPart.WorkbookStylesPart.Stylesheet.NumberingFormats;
            SpreadSheet.CellFormats cellFormats = document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats;

            if (SigTestFormatSheet == null)
            {
                weightStyleIndexArray = new int[] { 57, 56, 42, 38 };
                endIdx = 4;
            }
            else
            {
                weightStyleIndexArray = new int[] { 167, 63, 32, 20, 64 };
                endIdx = 5;
            }
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
                fmt = NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                SetNumberFormat(cellFormats, numberingFormats, weightStyleIndexArray, ref formatId, fmt, endIdx);
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
                if (SigTestFormatSheet == null)
                    numericStyleIndexArray = new int[] { 137, 143, 149, 155 };
                else
                    numericStyleIndexArray = new int[] { 184, 190, 196, 202 };

                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Summary);
                fmt = NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 4);
            }
            if (CurrentOutput.ParentRequest.ShowAverage)
            {
                if (SigTestFormatSheet == null)
                    numericStyleIndexArray = new int[] { 138, 144, 150, 156 };
                else
                    numericStyleIndexArray = new int[] { 185, 191, 197, 203 };

                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Average);
                fmt = NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 4);
            }
            if (CurrentOutput.ParentRequest.ShowStdev)
            {
                if (SigTestFormatSheet == null)
                    numericStyleIndexArray = new int[] { 139, 145, 151, 157 };
                else
                    numericStyleIndexArray = new int[] { 186, 192, 198, 204 };

                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Stdev);
                fmt = NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 4);
            }
            if (CurrentOutput.ParentRequest.ShowMinimum)
            {
                if (SigTestFormatSheet == null)
                    numericStyleIndexArray = new int[] { 140, 146, 152, 158 };
                else
                    numericStyleIndexArray = new int[] { 187, 193, 199, 205 };

                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Minimum);
                fmt = NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 4);
            }
            if (CurrentOutput.ParentRequest.ShowMaximum)
            {
                if (SigTestFormatSheet == null)
                    numericStyleIndexArray = new int[] { 141, 147, 153, 159 };
                else
                    numericStyleIndexArray = new int[] { 188, 194, 200, 206 };

                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Maximum);
                fmt = NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 4);
            }
            if (CurrentOutput.ParentRequest.ShowMedian)
            {
                if (SigTestFormatSheet == null)
                    numericStyleIndexArray = new int[] { 142, 148, 154, 160 };
                else
                    numericStyleIndexArray = new int[] { 189, 195, 201, 207 };
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Median);
                fmt = NumberFormat(ref fmt, FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp);
                SetNumberFormat(cellFormats, numberingFormats, numericStyleIndexArray, ref formatId, fmt, 4);
            }
        }
        public void SetNumberFormat(SpreadSheet.CellFormats cellFormats, SpreadSheet.NumberingFormats numberingFormats,
                                    int[] StyleIndexArray, ref uint formatId, string fmt, int endIdx)
        {
            for (int idx = 0; idx < endIdx; idx++)
            {
                SpreadSheet.CellFormat cellFormat = (SpreadSheet.CellFormat)cellFormats.ElementAt(StyleIndexArray[idx]);
                SpreadSheet.NumberingFormat numberingFormat = new SpreadSheet.NumberingFormat() { NumberFormatId = formatId, FormatCode = fmt };
                numberingFormats.Append(numberingFormat);
                cellFormat.NumberFormatId = (uint)formatId;
                formatId++;
            }
        }
        private void updateProgress(double currentProgress, string v)
        {
            if (null != QC)
            {
                QC.updateProgress(currentProgress, v);
            }
        }
        public string NumberFormat(ref string fmt, string FormatSheet, string SigTestFormatSheet
              , string[] NamesArray, int NumDigitsAfterDecimal
              , string Suffix = null, bool IsWeight = false)
        {
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
            return fmt;
        }

        private void CreateNewSheet(
               CrossTable Table, WorksheetPart worksheetIndexPart
              , string ContentsSheet
              , ref Array ContentsValue  //string 
              , ref Array HyperlinkTargetCells //Range  
               , TableType TableType
               , ReportTemplate reportTemplate, SpreadsheetDocument document, string tmpTableName, int idx
               , bool SigON = false
              )
        {

            int i;
            int MinIndex;
            int MaxIndex = -1;
            CrossTable tmpTable;
            CrossTable tmpNextTable;

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
                if (SigON)
                {
                    int[] styleIndexArray = new int[] { 79, 100, 101, 102, 80, 103, 104, 105, 81, 82, 89, 90, 106, 108, 108, 108, 107, 111, 109, 109, 83, 84, 84, 85, 87, 115, 115, 92, 88, 98, 98, 93 };
                    AdjustContentsSignificanceTestSheet(worksheetIndexPart, ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, TableType, styleIndexArray, MinIndex, MaxIndex);
                }
                else
                {
                    int[] styleIndexArray = new int[] { 115, 176, 113, 116, 177, 114, 111, 110, 109, 105, 104, 103, 102, 101, 100 };
                    AdjustContentsSheet(ContentsSheet, worksheetIndexPart, ref ContentsValue, ref HyperlinkTargetCells, styleIndexArray, MinIndex, MaxIndex);
                }
            }
            if (SigON)
            {
                GenerateSigWorkSheet(reportTemplate, document, tmpTableName, idx);
            }
            else
                GenerateWorkSheet(reportTemplate, document, tmpTableName, idx);
        }
        private void CreateNewSheetPortrait(
               CrossTable Table, WorksheetPart worksheetIndexPart
              , string ContentsSheet
              , ref Array ContentsValue  //string 
              , ref Array HyperlinkTargetCells //Range  
               , TableType TableType
               , ReportPortraitTemplate reportTemplate, SpreadsheetDocument document, string tmpTableName, int idx
               , bool SigON = false
              )
        {

            int i;
            int MinIndex;
            int MaxIndex = -1;
            CrossTable tmpTable;
            CrossTable tmpNextTable;

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
                if (SigON)
                {
                    int[] styleIndexArray = new int[] { 3, 121, 122, 123, 4, 124, 125, 126, 5, 6, 2, 1, 127, 129, 129, 129, 128, 131, 133, 133, 7, 8, 8, 9, 12, 117, 117, 13, 14, 119, 119, 15 };
                    AdjustContentsSignificanceTestSheet(worksheetIndexPart, ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, TableType, styleIndexArray, MinIndex, MaxIndex);
                }
                else
                {
                    int[] styleIndexArray = new int[] { 163, 202, 165, 164, 203, 166, 4, 5, 6, 11, 12, 13, 14, 15, 16 };
                    AdjustContentsSheet(ContentsSheet, worksheetIndexPart, ref ContentsValue, ref HyperlinkTargetCells, styleIndexArray, MinIndex, MaxIndex);
                }
            }
            if (SigON)
            {
                GeneratePortraitSigWorkSheet(reportTemplate, document, tmpTableName, idx);
            }
            else
                GenerateWorkSheet(reportTemplate, document, tmpTableName, idx);
        }

        public void AdjustContentsSignificanceTestSheet(WorksheetPart worksheetPart, string ContentsSheet
      , ref Array ContentsValue, ref Array HyperlinkTargetCells
      , TableType TableType, int[] styleIndexArray
      , int MinIndex = 0, int MaxIndex = 0)
        {
            CrossTable tmpTable;
            string buf;
            int c;
            int cnt = 0;
            int n;
            Array v;
            int u;
            bool IsSigTest;
            int rowNum = 8;
            int startCell;
            int styleIndex = 0;
            bool multiShape = false;
            bool isGlobalMode = QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? false : true;
            SpreadSheet.MergeCells mergeCells = new SpreadSheet.MergeCells();
            SpreadSheet.MergeCell mergeCell = null;
            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tmpTable = (CrossTable)CurrentOutput.Tables[0];
            SummaryNPFormat summaryNPFormat = new SummaryNPFormat();
            SummaryCreatorXml summaryCreatorXml = new SummaryCreatorXml();
            SpreadSheet.SheetData sheetData = worksheetPart.Worksheet.Elements<SpreadSheet.SheetData>().First();
            SpreadSheet.SheetDimension sheetDimension = worksheetPart.Worksheet.Elements<SpreadSheet.SheetDimension>().First();
            SpreadSheet.SheetViews sheetViews = new SpreadSheet.SheetViews();
            DrawingsPart drawingsPart = worksheetPart.AddNewPart<DrawingsPart>("rId2");

            SpreadSheet.Row row1 = new SpreadSheet.Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D, CustomHeight = true };
            SpreadSheet.Row row2 = new SpreadSheet.Row() { RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D, CustomHeight = true };
            SpreadSheet.Row row3 = new SpreadSheet.Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D, CustomHeight = true };
            SpreadSheet.Row row4 = new SpreadSheet.Row() { RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D, CustomHeight = true };
            SpreadSheet.Row row5 = new SpreadSheet.Row() { RowIndex = (UInt32Value)5U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D, CustomHeight = true };
            SpreadSheet.Row row6 = new SpreadSheet.Row() { RowIndex = (UInt32Value)6U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D, CustomHeight = true };
            SpreadSheet.Row row7 = new SpreadSheet.Row() { RowIndex = (UInt32Value)7U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D, CustomHeight = true };
            SpreadSheet.Row row8 = new SpreadSheet.Row() { RowIndex = (UInt32Value)8U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 6D, CustomHeight = true };
            sheetData.Append(row1);
            sheetData.Append(row2);
            sheetData.Append(row3);
            sheetData.Append(row4);
            sheetData.Append(row5);
            sheetData.Append(row6);
            sheetData.Append(row7);
            sheetData.Append(row8);

            //if (isGlobalMode)
            //{
            //    double version = Convert.ToDouble(xlApp.Version);
            //    if (version >= 16.0)
            //    {
            //        SpreadSheet.Columns columns = worksheetPart.Worksheet.Elements<SpreadSheet.Columns>().FirstOrDefault();
            //        SpreadSheet.Column column4 = new SpreadSheet.Column() { Min = (uint)4, Max = (uint)4, Width = 55.83203125D, CustomWidth = true };
            //        SpreadSheet.Column column5 = new SpreadSheet.Column() { Min = (uint)5, Max = (uint)5, Width = 18, CustomWidth = true };
            //        columns.Append(column4);
            //        columns.Append(column5);
            //    }
            //    else
            //    {
            //        SpreadSheet.Columns columns = worksheetPart.Worksheet.Elements<SpreadSheet.Columns>().FirstOrDefault();
            //        SpreadSheet.Column column4 = new SpreadSheet.Column() { Min = (uint)4, Max = (uint)4, Width = 55.83203125D, CustomWidth = true };
            //        SpreadSheet.Column column5 = new SpreadSheet.Column() { Min = (uint)5, Max = (uint)5, Width = 18, CustomWidth = true };
            //        columns.Append(column4);
            //        columns.Append(column5);
            //    }
            //}

            summaryNPFormat.GenerateTitleBox(drawingsPart, CurrentOutput.ParentRequest.Title);
            if (tmpTable.KeyItem != null)
            {
                int r = 6; ;
                v = new string[2, 2];
                v.SetValue(LocalResource.REPORT_CLASSIFICATION_ITEM_KEYWORD, 0, 0);
                v.SetValue(LocalResource.REPORT_SECTOR_KEYWORD, 1, 0);
                KeyItemInformation WithtmpTable = tmpTable.KeyItem;
                v.SetValue(WithtmpTable.Name + ":" + WithtmpTable.Description, 0, 1);
                v.SetValue(WithtmpTable.SectorNumber + ":" + WithtmpTable.SectorDescription, 1, 1);

                while (r <= 7)
                {
                    SpreadSheet.Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)r);//new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                    startCell = 2;
                    while (startCell <= 5)
                    {
                        SpreadSheet.Cell cell62 = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + r, StyleIndex = (uint)styleIndexArray[styleIndex] };
                        row.Append(cell62);
                        startCell++; styleIndex++;
                    }
                    mergeCell = new SpreadSheet.MergeCell() { Reference = "C" + r + ":E" + r };
                    mergeCells.Append(mergeCell);
                    r++;
                }
                SummaryHelper.PutValue(worksheetPart, 6, 2, ref v);
            }
            if (IsSigTest || CurrentOutput.MarkingSignificance)
            {
                cnt++;
                multiShape = true;
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
                    var val = System.Text.RegularExpressions.Regex.Unescape(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_CAPTION) + "\n" + String.Join("\n", (string[])v);
                    summaryNPFormat.GenerateSignificanceTestLegend(drawingsPart, val, rowNum, crossSignificance: true);
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
                    var val = System.Text.RegularExpressions.Regex.Unescape(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_TO_WHOLE_CAPTION) + "\n" + String.Join("\n", (string[])v);
                    summaryNPFormat.GenerateSignificanceTestLegend(drawingsPart, val, rowNum, crossSignificance: true);
                }
            }
            if (CurrentOutput.MarkingColoring && !IsSigTest)
            {
                cnt++;
                summaryNPFormat.GenerateMarkingColoring(drawingsPart, CurrentOutput, LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_CAPTION,
                                                        rowNum, multiShape, crossSignificance: true);
            }
            string srcRowOfst = null, srcRow = null, dstRowOfst = null, dstRow = null, minBaseRowOfst = null, minBaseRow = null;
            string srcColOfst, srcCol, dstColOfst, dstCol;
            if (CurrentOutput.MarkingRanking && !IsSigTest)
            {
                if (cnt == 1)
                {
                    srcCol = "3"; srcColOfst = !isGlobalMode ? "1143000" : "1935480"; srcRow = !isGlobalMode ? rowNum.ToString() : "8"; srcRowOfst = "0";
                    dstCol = "3"; dstColOfst = !isGlobalMode ? "1838325" : "2830805"; dstRow = !isGlobalMode ? (rowNum + 1).ToString() : "8"; dstRowOfst = !isGlobalMode ? "0" : "832485";
                }
                else
                {
                    srcCol = "3"; srcColOfst = !isGlobalMode ? "76200" : "646760"; srcRow = !isGlobalMode ? rowNum.ToString() : "8"; srcRowOfst = "0";
                    dstCol = "3"; dstColOfst = !isGlobalMode ? "771525" : "1542085"; dstRow = !isGlobalMode ? (rowNum + 1).ToString() : "8"; dstRowOfst = !isGlobalMode ? "0" : "832485";
                }
                if (!isGlobalMode)
                {
                    DrawingPart.GenerateMarkingRanking(drawingsPart, srcRowOfst, srcRow, dstRowOfst, dstRow, srcColOfst, srcCol, dstColOfst, dstCol);
                }
                else
                {
                    DrawingPart.GenerateMarkingRankingGlobal(drawingsPart, srcRowOfst, srcRow, dstRowOfst, dstRow, srcColOfst, srcCol, dstColOfst, dstCol);
                }
                cnt++;
            }
            rowNum++;
            buf = CurrentOutput.LocalizedFilteringExpression;
            if (buf.Length != 0)
            {
                v = new string[2];
                v.SetValue(LocalResource.REPORT_FILTER_CRITERION_KEYWORD, 0);
                v.SetValue(buf, 1);
                SpreadSheet.Row row9 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = !isGlobalMode ? 66D : 74D, CustomHeight = true };
                SpreadSheet.Cell cell91 = new SpreadSheet.Cell() { CellReference = "B" + rowNum, StyleIndex = (uint)styleIndexArray[8], CellValue = new SpreadSheet.CellValue((string)v.GetValue(0)), DataType = SpreadSheet.CellValues.String };
                row9.Append(cell91);
                SpreadSheet.Cell cell92 = new SpreadSheet.Cell() { CellReference = "C" + rowNum, StyleIndex = (uint)styleIndexArray[9], CellValue = new SpreadSheet.CellValue((string)v.GetValue(1)), DataType = SpreadSheet.CellValues.String };
                row9.Append(cell92);
                sheetData.Append(row9);
            }
            else
            {
                SpreadSheet.Row row9 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = !isGlobalMode ? 66D : 74D, CustomHeight = true };
                sheetData.Append(row9);
            }

            rowNum++;
            SpreadSheet.Row row10 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
            SpreadSheet.Cell cell101 = new SpreadSheet.Cell() { CellReference = "B" + rowNum, StyleIndex = (uint)styleIndexArray[10], DataType = SpreadSheet.CellValues.String };
            row10.Append(cell101);
            SpreadSheet.Cell cell102 = new SpreadSheet.Cell() { CellReference = "E" + rowNum, StyleIndex = (uint)styleIndexArray[11], DataType = SpreadSheet.CellValues.String };
            row10.Append(cell102);
            sheetData.Append(row10);

            if (CurrentOutput.WBOn)
            {
                string wb = LocalResource.WEIGHT_BACK;
                string msg = wb.Insert(wb.Length - 1, "[" + tmpTable.Question.WBValue + "]") + '\u2009'
                                + (tmpTable.Question.TabulateFullQuantity ? LocalResource.WB_TOTAL_NUMBER_BASE : string.Empty);
                SpreadSheet.Cell cell = SummaryHelper.GetCell(worksheetPart, rowNum, 2);
                cell.CellValue = new SpreadSheet.CellValue(msg);
            }
            else if (tmpTable.Question.TabulateFullQuantity)
            {
                string msg = LocalResource.WB_TOTAL_NUMBER_BASE;
                SpreadSheet.Cell cell = SummaryHelper.GetCell(worksheetPart, rowNum, 2);
                cell.CellValue = new SpreadSheet.CellValue(msg);
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
                        wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_BEFORE_WB;
                    }
                    string msg = string.Format(LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_PROMPT,
                        wbString, CurrentOutput.MinSamplesCountOnMarking.ToString());
                    SpreadSheet.Cell cell = SummaryHelper.GetCell(worksheetPart, rowNum, 5);
                    cell.CellValue = new SpreadSheet.CellValue(msg);
                }
            }

            rowNum++; styleIndex = 12; int limit = rowNum + 1;
            while (rowNum <= limit)
            {
                SpreadSheet.Row row11 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                startCell = 2;
                while (startCell <= 5)
                {
                    SpreadSheet.Cell cell112 = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = SpreadSheet.CellValues.String };
                    row11.Append(cell112);
                    styleIndex++; startCell++;
                }
                sheetData.Append(row11);
                rowNum++;
            }

            mergeCell = new SpreadSheet.MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(2) + (limit - 1) + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(2) + limit };
            mergeCells.Append(mergeCell);
            mergeCell = new SpreadSheet.MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(5) + (limit - 1) + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(5) + limit };
            mergeCells.Append(mergeCell);
            mergeCell = new SpreadSheet.MergeCell() { Reference = "C" + (limit - 1) + ":" + "C" + limit };
            mergeCells.Append(mergeCell);
            mergeCell = new SpreadSheet.MergeCell() { Reference = "D" + (limit - 1) + ":" + "D" + limit };
            mergeCells.Append(mergeCell);

            c = CurrentOutput.Tables.Count;
            ContentsValue = Array.CreateInstance(typeof(string),
                new int[] { MaxIndex - MinIndex + 1, 4 },
                new int[] { MinIndex, 1 });
            n = MaxIndex - MinIndex + 1;
            HyperlinkTargetCells = Array.CreateInstance(typeof(string),
                new int[] { ContentsValue.GetUpperBound(0) - ContentsValue.GetLowerBound(0) + 1, ContentsValue.GetUpperBound(1) - 4 + 1 },
                new int[] { ContentsValue.GetLowerBound(0), 4 });

            v = Array.CreateInstance(typeof(string), new int[] { ContentsValue.GetUpperBound(1) }, new int[] { 1 });
            v.SetValue(LocalResource.REPORT_LAYOUT_QUESTION_NUMBER_COLUMN_CAPTION, 1);
            v.SetValue(LocalResource.REPORT_LAYOUT_TABLE_HEADING_COLUMN_CAPTION, 2);
            v.SetValue(LocalResource.REPORT_LAYOUT_QC3_DESCRIPTION_2COLUMN_CAPTION, 3);
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
            for (int col = 2; col <= 5; col++)
            {
                SpreadSheet.Cell cell = SummaryHelper.GetCell(worksheetPart, limit - 1, col);
                cell.CellValue = new SpreadSheet.CellValue((string)v.GetValue((col - 1)));
            }

            startCell = 2;
            SpreadSheet.Row row13 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = 3D, CustomHeight = true };
            while (startCell <= 5)
            {
                SpreadSheet.Cell cell132 = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = SpreadSheet.CellValues.String };
                row13.Append(cell132);
                styleIndex++; startCell++;
            }
            sheetData.Append(row13);

            int numRows = n;
            int cnts = 1, styIdx;
            startCell = 2; rowNum++; limit = rowNum;

            if (numRows > 1)
            {
                while (cnts < numRows)
                {
                    styIdx = styleIndex;
                    SpreadSheet.Row row14 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 22.5D, CustomHeight = true };
                    while (startCell <= 5)
                    {
                        SpreadSheet.Cell cell = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styIdx], DataType = SpreadSheet.CellValues.String };
                        row14.Append(cell); styIdx++; startCell++;
                    }
                    //mergeCell = new SpreadSheet.MergeCell() { Reference = "C" + rowNum + ":" + "C" + rowNum };
                    //mergeCells.Append(mergeCell);
                    //mergeCell = new SpreadSheet.MergeCell() { Reference = "D" + rowNum + ":" + "D" + rowNum };
                    //mergeCells.Append(mergeCell);
                    startCell = 2; rowNum++; cnts++;
                    sheetData.Append(row14);
                }
                styleIndex += 4;
                SpreadSheet.Row row15 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 22.5D, CustomHeight = true };
                while (startCell <= 5)
                {
                    SpreadSheet.Cell cell = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = SpreadSheet.CellValues.String };
                    row15.Append(cell); styleIndex++; startCell++;
                }
                //mergeCell = new SpreadSheet.MergeCell() { Reference = "C" + rowNum + ":" + "C" + rowNum };
                //mergeCells.Append(mergeCell);
                //mergeCell = new SpreadSheet.MergeCell() { Reference = "D" + rowNum + ":" + "D" + rowNum };
                //mergeCells.Append(mergeCell);
                sheetData.Append(row15);
            }
            else
            {
                styleIndex += 4; startCell = 2;
                SpreadSheet.Row row15 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 22.5D, CustomHeight = true };
                while (startCell <= 5)
                {
                    SpreadSheet.Cell cell = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = SpreadSheet.CellValues.String };
                    row15.Append(cell); styleIndex++; startCell++;
                }
                //mergeCell = new SpreadSheet.MergeCell() { Reference = "C" + rowNum + ":" + "C" + rowNum };
                //mergeCells.Append(mergeCell);
                //mergeCell = new SpreadSheet.MergeCell() { Reference = "D" + rowNum + ":" + "D" + rowNum };
                //mergeCells.Append(mergeCell);
                sheetData.Append(row15);
            }
            SpreadSheet.SheetView sheetView = new SpreadSheet.SheetView() { ShowGridLines = false, TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            SpreadSheet.Pane pane = new SpreadSheet.Pane() { VerticalSplit = limit - 1, TopLeftCell = "A" + limit, ActivePane = SpreadSheet.PaneValues.BottomLeft, State = SpreadSheet.PaneStateValues.Frozen };
            SpreadSheet.Selection selection = new SpreadSheet.Selection() { Pane = SpreadSheet.PaneValues.BottomLeft, ActiveCell = "A1", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1" } };
            sheetView.Append(pane);
            sheetView.Append(selection);
            sheetViews.Append(sheetView);
            worksheetPart.Worksheet.InsertAfter(sheetViews, sheetDimension);
            worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }
        private List<int> GetLinesIndexList(CrossTable Table
              , Hashtable CutRowsCol, int HeaderRowsCount
              )
        {
            OutputCross tmpOutput;
            Array LinesIndexArray; //int
            List<bool> tmpCol;
            List<int> res;
            int i = 0;
            int j = 0;
            int r = 0;
            int addRowsCount = 0;
            int k = 0; int kIndex = 0;
            int rIndex = 0;
            int addRowsIndex = 0;
            bool DefHasNAAtAxis;
            bool DefHasIVAtAxis;
            tmpOutput = (OutputCross)Table.ParentOutput;
            DefHasNAAtAxis = tmpOutput.ShowNAAtAxis;
            DefHasIVAtAxis = tmpOutput.ShowIVAtAxis;
            addRowsCount = (CrossCreator.ToInt(DefHasNAAtAxis) & 1) + (CrossCreator.ToInt(DefHasIVAtAxis) & 1);
            switch (addRowsCount)
            {
                case 0:
                    addRowsIndex = 2;
                    break;
                case 1:
                    addRowsIndex = 1;
                    break;
                case 2:
                    addRowsIndex = 0;
                    break;
            }
            LinesIndexArray = Table.LineChartRowsArray;
            if (LinesIndexArray == null || LinesIndexArray.Length == 0)
            {
                return new List<int>();
            }
            tmpCol = new List<bool>();
            for (i = 1; i <= Table.AxesGroups.Count; i++)
            {
                AxesInformation WithAxesGroup = Table.AxesGroups[i - 1];
                if (WithAxesGroup.Count == 1)
                {  //' 二重クロス
                    for (j = 1; j <= WithAxesGroup[0].SectorsCount + addRowsCount; j++)
                    { //' 無回答/非該当を含む
                        r = r + 1;
                        rIndex = rIndex + 1;
                        tmpCol.Add(Table.ExistLineChartIndex(rIndex));
                    }
                    for (kIndex = 1; kIndex <= addRowsIndex; kIndex++)
                    {
                        rIndex = rIndex + 1;
                    }
                }
                else
                {   // ' 三重クロス
                    for (j = 1; j <= WithAxesGroup[0].SectorsCount + addRowsCount; j++)
                    {
                        for (k = 1; k <= WithAxesGroup[1].SectorsCount + addRowsCount; k++)
                        {
                            r = r + 1;
                            rIndex = rIndex + 1;
                            tmpCol.Add(Table.ExistLineChartIndex(rIndex));
                        }
                        for (kIndex = 1; kIndex <= addRowsIndex; kIndex++)
                        {
                            rIndex = rIndex + 1;
                        }
                    }
                    for (kIndex = 1; kIndex <= addRowsIndex; kIndex++)
                    {
                        for (k = 1; k <= WithAxesGroup[1].SectorsCount + addRowsCount + addRowsIndex; k++)
                        {
                            rIndex = rIndex + 1;
                        }
                    }
                }
            }
            for (i = Table.AxesGroups.Count; i >= 1; i--)
            {
                AxesInformation WithAxesGroup = Table.AxesGroups[i - 1];
                if (WithAxesGroup.Count == 1)
                { //' 二重クロス
                    r = r - (WithAxesGroup[0].SectorsCount + addRowsCount);
                    tmpCol.Insert(r, false); // ' 小計行
                }
                else
                {  //  ' 三重クロス
                    for (j = WithAxesGroup[0].SectorsCount + addRowsCount; j >= 1; j--)
                    {
                        r = r - (WithAxesGroup[1].SectorsCount + addRowsCount);
                        tmpCol.Insert(r, false);
                    }
                    tmpCol.Insert(r, false);
                }
                if (i == 1) { tmpCol.Insert(r, false); }// ' 全体(GT)行 Mantis#2506契機で追加
            }

            int[] CutRowsColListOrderd = new int[CutRowsCol.Count];
            CutRowsCol.Values.CopyTo(CutRowsColListOrderd, 0);
            Array.Sort(CutRowsColListOrderd);

            for (i = CutRowsColListOrderd.Length - 1; i >= 0; i--)
            {
                r = (int)CutRowsColListOrderd[i] - HeaderRowsCount + 1;
                if ((r - 1) < tmpCol.Count && (r - 1 >= 0))
                {
                    tmpCol.RemoveAt(r - 1);
                }
            }
            res = new List<int>();
            for (i = 1; i <= tmpCol.Count(); i++)
            {
                if (tmpCol[i - 1]) { res.Add(i); }
            }
            return res;

        }

        private void AdjustContentsSheet(string ContentsSheet, WorksheetPart worksheetIndexPart
        , ref Array ContentsValue, ref Array HyperlinkTargetCells, int[] styleIndexArray,
         int MinIndex = 0, int MaxIndex = 0)
        {
            CrossTable tmpTable;
            string buf;
            int c;
            int n;
            Array v;
            int u;
            int rowNum = 7;
            int startCell;
            int styleIndex = 0;

            //int[] styleIndexArray = new int[] { 115, 115, 113, 116, 116, 114, 111, 110, 109, 105, 104, 103, 102, 101, 100 };

            SpreadSheet.MergeCells mergeCells = new SpreadSheet.MergeCells();
            SpreadSheet.MergeCell mergeCell = null;
            tmpTable = (CrossTable)CurrentOutput.Tables[0];
            SpreadSheet.SheetData sheetData = worksheetIndexPart.Worksheet.Elements<SpreadSheet.SheetData>().First();
            DrawingsPart drawingsPart = worksheetIndexPart.AddNewPart<DrawingsPart>("rId1");
            DrawingPart.GenerateTitleBox(drawingsPart, CurrentOutput.ParentRequest.Title);
            int limit = rowNum;
            while (rowNum <= 8)
            {
                SpreadSheet.Row row1 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                CreateCells(row1, ref rowNum, ref styleIndex, ref styleIndexArray);
                sheetData.Append(row1);
            }
            mergeCell = new SpreadSheet.MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(2) + limit + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(2) + (limit + 1) };
            mergeCells.Append(mergeCell);
            mergeCell = new SpreadSheet.MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(3) + limit + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(3) + (limit + 1) };
            mergeCells.Append(mergeCell);
            mergeCell = new SpreadSheet.MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(4) + limit + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(4) + (limit + 1) };
            mergeCells.Append(mergeCell);
            mergeCell = new SpreadSheet.MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(5) + limit + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(5) + (limit + 1) };
            mergeCells.Append(mergeCell);

            SpreadSheet.Row row9 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = 3D, CustomHeight = true };
            CreateCells(row9, ref rowNum, ref styleIndex, ref styleIndexArray);
            sheetData.Append(row9);

            c = CurrentOutput.Tables.Count;
            ContentsValue = Array.CreateInstance(typeof(string),
                new int[] { MaxIndex - MinIndex + 1, 4 },
                new int[] { MinIndex, 1 });
            n = MaxIndex - MinIndex + 1;
            HyperlinkTargetCells = Array.CreateInstance(typeof(string),
                new int[] { ContentsValue.GetUpperBound(0) - ContentsValue.GetLowerBound(0) + 1, ContentsValue.GetUpperBound(1) - 4 + 1 },
                new int[] { ContentsValue.GetLowerBound(0), 4 });

            v = Array.CreateInstance(typeof(string), new int[] { 4 }, new int[] { 1 });
            v.SetValue(LocalResource.REPORT_LAYOUT_QUESTION_NUMBER_COLUMN_CAPTION, 1);
            v.SetValue(LocalResource.REPORT_LAYOUT_TABLE_HEADING_COLUMN_CAPTION, 2);
            v.SetValue(LocalResource.REPORT_LAYOUT_QC3_DESCRIPTION_2COLUMN_CAPTION, 3);
            v.SetValue(LocalResource.REPORT_PAGE_KEYWORD, 4);

            SpreadSheet.Row row = OpenXmlHelper.GetRow(worksheetIndexPart.Worksheet, (uint)limit);
            for (int col = 2; col <= 5; col++)
            {
                SpreadSheet.Cell cell = OpenXmlHelper.GetCell(row, limit, col);
                cell.CellValue = new SpreadSheet.CellValue((string)v.GetValue(col - 1));
                cell.DataType = SpreadSheet.CellValues.String;
            }

            int numRows = n;
            int cnts = 1, styIdx;
            startCell = 2; limit = rowNum;
            if (numRows > 1)
            {
                while (cnts < numRows)
                {
                    styIdx = styleIndex;
                    SpreadSheet.Row row14 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D, CustomHeight = false };
                    while (startCell <= 5)
                    {
                        SpreadSheet.Cell cell = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styIdx], DataType = SpreadSheet.CellValues.String };
                        row14.Append(cell);
                        if (startCell == 3)
                        {
                            startCell++;
                            cell = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styIdx], DataType = SpreadSheet.CellValues.String };
                            row14.Append(cell);
                        }
                        styIdx++; startCell++;
                    }
                    startCell = 2; rowNum++; cnts++;
                    sheetData.Append(row14);
                }
                styleIndex += 3;
                SpreadSheet.Row row15 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D, CustomHeight = false };
                while (startCell <= 5)
                {
                    SpreadSheet.Cell cell = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = SpreadSheet.CellValues.String };
                    row15.Append(cell);
                    if (startCell == 3)
                    {
                        startCell++;
                        cell = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = SpreadSheet.CellValues.String };
                        row15.Append(cell);
                    }
                    styleIndex++; startCell++;
                }
                sheetData.Append(row15);
            }
            else
            {
                styleIndex += 3; startCell = 2;
                SpreadSheet.Row row15 = new SpreadSheet.Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D, CustomHeight = false };
                while (startCell <= 5)
                {
                    SpreadSheet.Cell cell = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = SpreadSheet.CellValues.String };
                    row15.Append(cell);
                    if (startCell == 3)
                    {
                        startCell++;
                        cell = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = SpreadSheet.CellValues.String };
                        row15.Append(cell);
                    }
                    styleIndex++; startCell++;
                }
                sheetData.Append(row15);
            }
            worksheetIndexPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }

        private void CreateCells(SpreadSheet.Row row, ref int rowNum, ref int styleIndex, ref int[] styleIndexArray)
        {
            int startCell = 2;
            while (startCell <= 5)
            {
                SpreadSheet.Cell cell1 = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex] };
                row.Append(cell1);
                if (startCell == 3)
                {
                    startCell++;
                    SpreadSheet.Cell cell2 = new SpreadSheet.Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex] };
                    row.Append(cell2);
                }
                startCell++; styleIndex++;
            }
            rowNum++;
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
        private void GenerateSigWorkSheet(ReportTemplate reportTemplate, SpreadsheetDocument document, string tempTableName, int i)
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            WorksheetPart worksheetPart = null;
            SpreadSheet.Sheets sheets = workbookPart.Workbook.Sheets;
            SpreadSheet.Sheet lastSheet = sheets.Descendants<SpreadSheet.Sheet>().LastOrDefault();
            uint sheetId = i == 0 ? 3 : lastSheet.SheetId.Value + 1;
            string id = i == 0 ? "rId3" : "rId" + (Convert.ToInt32(Regex.Match(lastSheet.Id.Value, @"\d+").Value) + 1);
            SpreadSheet.Sheet sheet = null;
            ReportSigTemplate reportSigTemplate = new ReportSigTemplate();

            if (tempTableName == "DoubleSignificanceTest")
            {
                sheet = new SpreadSheet.Sheet() { Name = tempTableName, SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportSigTemplate.GenerateWorksheetDoubleSigPart(worksheetPart);
            }
            else if (tempTableName == "TripleSignificanceTest")
            {
                sheet = new SpreadSheet.Sheet() { Name = tempTableName, SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportSigTemplate.GenerateWorksheetTripleSigPart(worksheetPart);
            }
        }

        private void GenerateWorkSheet(ReportTemplate reportTemplate, SpreadsheetDocument document, string tempTableName, int i)
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            WorksheetPart worksheetPart = null;
            SpreadSheet.Sheets sheets = workbookPart.Workbook.Sheets;
            SpreadSheet.Sheet lastSheet = sheets.Descendants<SpreadSheet.Sheet>().LastOrDefault();
            uint sheetId = i == 0 ? 3 : lastSheet.SheetId.Value + 1;
            string id = i == 0 ? "rId3" : "rId" + (Convert.ToInt32(Regex.Match(lastSheet.Id.Value, @"\d+").Value) + 1);
            SpreadSheet.Sheet sheet = null;

            if (tempTableName == "Cross_Triple_Std")
            {
                sheet = new SpreadSheet.Sheet() { Name = "Cross_Triple_Std", SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportTemplate.GenerateWorksheetCrossTripleStd(worksheetPart);
            }
            else if (tempTableName == "Cross_Triple_Std_N")
            {
                sheet = new SpreadSheet.Sheet() { Name = "Cross_Triple_Std_N", SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportTemplate.GenerateWorksheetCrossTripleStdN(worksheetPart);
            }
            else if (tempTableName == "Cross_Double_Std")
            {
                sheet = new SpreadSheet.Sheet() { Name = "Cross_Double_Std", SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportTemplate.GenerateWorksheetCrossDoubleStd(worksheetPart);
            }
            else if (tempTableName == "Cross_Double_Std_N")
            {
                sheet = new SpreadSheet.Sheet() { Name = "Cross_Double_Std_N", SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportTemplate.GenerateWorksheetCrossDoubleStdN(worksheetPart);
            }
            else if (tempTableName == "Cross_Triple_SigTest")
            {
                sheet = new SpreadSheet.Sheet() { Name = "Cross_Triple_SigTest", SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportTemplate.GenerateWorksheetCrossDoubleSigTest(worksheetPart);
            }
            else if (tempTableName == "Cross_Double_SigTest")
            {
                sheet = new SpreadSheet.Sheet() { Name = "Cross_Double_SigTest", SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportTemplate.GenerateWorksheetCrossTripleSigTest(worksheetPart);
            }
        }

        private void GenerateWorkSheet(ReportPortraitTemplate reportTemplate, SpreadsheetDocument document, string tempTableName, int i)
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            WorksheetPart worksheetPart = null;
            SpreadSheet.Sheets sheets = workbookPart.Workbook.Sheets;
            SpreadSheet.Sheet lastSheet = sheets.Descendants<SpreadSheet.Sheet>().LastOrDefault();
            uint sheetId = i == 0 ? 3 : lastSheet.SheetId.Value + 1;
            string id = i == 0 ? "rId3" : "rId" + (Convert.ToInt32(Regex.Match(lastSheet.Id.Value, @"\d+").Value) + 1);
            SpreadSheet.Sheet sheet = null;

            if (tempTableName == "Cross_WT_Template")
            {
                sheet = new SpreadSheet.Sheet() { Name = "Cross_WT_Template", SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportTemplate.GenerateWorksheetCrossWeightTemplate(worksheetPart);
            }
            else if (tempTableName == "Cross_Template")
            {
                sheet = new SpreadSheet.Sheet() { Name = "Cross_Template", SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportTemplate.GenerateWorksheetCrossTemplate(worksheetPart);
            }
        }

        private void GeneratePortraitSigWorkSheet(ReportPortraitTemplate reportTemplate, SpreadsheetDocument document, string tempTableName, int i)
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            WorksheetPart worksheetPart = null;
            SpreadSheet.Sheets sheets = workbookPart.Workbook.Sheets;
            SpreadSheet.Sheet lastSheet = sheets.Descendants<SpreadSheet.Sheet>().LastOrDefault();
            uint sheetId = i == 0 ? 3 : lastSheet.SheetId.Value + 1;
            string id = i == 0 ? "rId3" : "rId" + (Convert.ToInt32(Regex.Match(lastSheet.Id.Value, @"\d+").Value) + 1);
            SpreadSheet.Sheet sheet = null;
            ReportPortraitSigTemplate reportSigTemplate = new ReportPortraitSigTemplate();

            if (tempTableName == "DoubleSignificanceTest")
            {
                sheet = new SpreadSheet.Sheet() { Name = tempTableName, SheetId = sheetId, Id = "rId" + id };
                sheets.Append(sheet);
                worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
                reportSigTemplate.GenerateWorksheetDoubleSigPart(worksheetPart);
            }
            //else if (tempTableName == "TripleSignificanceTest")
            //{
            //    sheet = new SpreadSheet.Sheet() { Name = tempTableName, SheetId = sheetId, Id = "rId" + id };
            //    sheets.Append(sheet);
            //    worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + id);
            //    reportSigTemplate.GenerateWorksheetTripleSigPart(worksheetPart);
            //}
        }

        public void SetSheetProperty(SpreadsheetDocument document, WorksheetPart worksheetPart, Table Table, int sheetNum,
                                     bool signOn, ref string tempTable)
        {
            string tabClr = null;
            System.Drawing.Color clr;
            ReportBook rb = null;
            string wb = null;
            Macromill.QCWeb.ReportRequest.KeyItemInformation tmpKeyItem = null;
            GTTable tmpGTTable = null;
            CrossTable tmpCRTable = null;
            FAListTable tmpFATable = null;
            string QID = null;
            string QDesc = null;
            string QNumber = null;
            string res = null;
            string ReportTitle = null;
            string header = null;
            string tableHeading = null;
            string narrowingCondtion = null;
            string n = null;
            string k;
            int j = 0;
            int MAX_SHEET_NAME_LENGTH = 31;
            OrderedDictionary kDic = new OrderedDictionary();
            ReportTemplate reportTemplate = new ReportTemplate();

            if (typeof(Table).IsAssignableFrom(typeof(GTTable)))
            { // ' GT/Cross
                tmpGTTable = (GTTable)Table;
                tmpCRTable = Table as CrossTable;
                Macromill.QCWeb.ReportRequest.QuestionInformation WithQ = tmpGTTable.Question;
                QID = WithQ.Name;
                QDesc = WithQ.Description;
                QNumber = WithQ.QNumber; ;
                tmpKeyItem = tmpGTTable.KeyItem;
                tableHeading = WithQ.TableHeading;
                narrowingCondtion = WithQ.NarrowingCondition;

            }
            if (!signOn)
            {
                switch (tmpCRTable.Question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N))
                {
                    case QuestionType.SA:
                    case QuestionType.MA:

                        clr = System.Drawing.Color.FromArgb(TAB_COLOR_BLUE);
                        tabClr = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                        break;
                    case QuestionType.N:
                        clr = System.Drawing.Color.FromArgb(TAB_COLOR_RED);
                        tabClr = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                        break;
                }
                SpreadSheet.TabColor tabColor = new SpreadSheet.TabColor() { Rgb = tabClr };
                var sheetProperty = worksheetPart.Worksheet.Descendants<SpreadSheet.SheetProperties>().FirstOrDefault();
                sheetProperty.Append(tabColor);
            }
            if (QID != null)
            {
                if (tmpGTTable != null && !signOn)
                {
                    CrossReportHelper.InserStringValue(worksheetPart, QID, 2, 2);
                    if (!NPOICrossCreator.checkSimpleAggr(tmpCRTable) || string.IsNullOrEmpty(tableHeading) || tmpCRTable.AxesGroups.Count == 1)
                    {
                        CrossReportHelper.InserStringValue(worksheetPart, QDesc, 4, 2);
                    }
                    CrossReportHelper.InserStringValue(worksheetPart, tableHeading, 3, 2);
                    //if (tmp != null && tmp.Length > 0)
                    //{
                    //    CrossReportHelper.InserStringValue(worksheetPart, tmp, 6, 2);
                    //}
                    CrossReportHelper.InserStringValue(worksheetPart, narrowingCondtion, 7, 2);

                    if (tmpKeyItem != null)
                    {
                        string clas = LocalResource.REPORT_CLASSIFICATION_ITEM_KEYWORD
                              + " = " + tmpKeyItem.Name + ":" + tmpKeyItem.Description
                              + "\n"
                              + LocalResource.REPORT_SECTOR_KEYWORD
                              + " = "
                              + tmpKeyItem.SectorNumber + ":" + tmpKeyItem.SectorDescription;
                        if (clas.Length > 32767)
                            clas = clas.Substring(0, 32766);
                        CrossReportHelper.InserStringValue(worksheetPart, clas, 5, 2);
                    }
                }
                n = sheetNum + 1 + "(" + QID + ")";

                SpreadSheet.Sheet sheet = document.WorkbookPart.Workbook.Descendants<SpreadSheet.Sheet>().LastOrDefault();
                sheet.Name = n;
                tempTable = n;
            }
        }
    }
}
