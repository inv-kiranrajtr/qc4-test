using log4net;
using Macromill.QCWeb.Batch.Report;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
//using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
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
using XlFileFormat = Microsoft.Office.Interop.Excel.XlFileFormat;
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

namespace Qc4Launcher.Logic
{
    public class ReportCreator
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string TEMPLATE_NAME = "Report_Template.xlt";
        private static string BOOK_NAME = "Report.xltx";
        private static string TRANSPOSE_TEMPLATE_NAME = "ReportPortrait.xlt";
        private static string FORMAT_TEMPLATE_NAME = "ReportFormat.xlt";
        private static string TRANSPOSE_FORMAT_TEMPLATE_NAME = "ReportPortraitFormat.xlt";
        private static string PARTS_PRESENTATION_PATH = @"PPTemplates\parts.pptx";
        private static string NUMERIC_DATALABEL_NUMBER_FMT = "0.0;;";
        public static int TAB_COLOR_BLUE = 0XF1D9C5;
        public static int TAB_COLOR_RED = 0XDBDCF2;

        private static int MAX_SHEETS_COUNT = int.MaxValue;
        private Worksheet WorkingSheet;
        private Workbook TemplateBook;
        private Workbook TransposeTemplateBook;
        private Workbook FormatBook;
        private Workbook TransposeFormatBook;
        private XlFileFormat FileFormat;
        private Application xlApp;
        private CrossCreator crossCreator;
        private Workbooks wbs;
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
        private Worksheet FormatSheet;
        private Worksheet DoubleFormatSheet;
        private Worksheet TransposeFormatSheet;
        private Worksheet TransposeDoubleFormatSheet;
        private Worksheet GTFormatSheet;
        private Worksheet GTTransposeFormatSheet;
        private Hashtable ThisReportBooks;
        public BackgroundWorker bgWorker;
        public List<string> outputFiles;

        private Dictionary<string, double> colWidthMap;

        public void CreateReport(Outputs Outputs, string bookPSWD, string sheetPSWD, string outputDirectoryPath,
            string templateDirectoryPath, Application xlAppG, BackgroundWorker bgWorker, DoWorkEventArgs bgWorkerArg, Reportset reportset,
            CrossTabulationQC QC = null, double progressAvailable = 0, double curProgres = 0, string qc4FileName = null, List<string> outputFiles = null)
        {
            Output tmpOutput = null;
            Output preOutput = null;
            StatusCode tmpStatus = 0;
            Output QuestionnaireOutput = null;
            int i;
            BookPSWD = bookPSWD;
            SheetPSWD = sheetPSWD;
            TemplateDirectoryPath = templateDirectoryPath;
            ThisReportset = reportset;
            OutputDirectoryPath = outputDirectoryPath;
            prfix = NPOICrossCreator.getPrefix(qc4FileName);
            this.progressAvailable = progressAvailable;
            this.currentProgress = curProgres;
            this.QC = QC;
            //res  MethodResult
            //result  MethodResult

            // On Error GoTo ErrHdl
            xlApp = xlAppG;
            //xlApp.Visible = true;
            //xlApp.ScreenUpdating = true;
            this.bgWorker = bgWorker;
            if (outputFiles == null) outputFiles = new List<string>();
            this.outputFiles = outputFiles;
            colWidthMap = new Dictionary<string, double>();
            try
            {
                Initialize();
                xlApp.Calculation = XlCalculation.xlCalculationManual;
                double currentprogressGrp = currentProgress;
                double progressStepGrp = progressAvailable / Outputs.Count;
                List<string> outputFileSig = new List<string>();
                for (i = 0; i <= Outputs.Count - 1; i++)
                {
                    tmpOutput = Outputs[i];
                    tmpOutput.StartTime = DateTime.Now;
                    tmpOutput.Status = StatusCode.Running;
                    if (preOutput != null) { preOutput.Status = tmpStatus; }
                    //     res = Successful;
                    InitializeOutput();
                    CrossCreator crossCreatorSig = new CrossCreator();
                    switch (tmpOutput.OutputType)
                    {
                        case OutputType.Cross:
                            // ' クロス
                            OutputCross CurrentOutput = (OutputCross)tmpOutput;
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
                            CreateCross(CurrentOutput);
                            if (bgWorker.CancellationPending) return;
                            updateProgress(currentProgress, LocalResource.PB_EXCEL_GEN);
                            if ((CurrentOutput.SignificanceTestOne || CurrentOutput.SignificanceTestFive || CurrentOutput.SignificanceTestTen)
                            && (!CurrentOutput.SignificanceTestOne || !CurrentOutput.SignificanceTestFive || !CurrentOutput.SignificanceTestTen))
                            {
                                //currentProgress += progressAvailableRpt;
                                double prgsmvmt = 0;
                                crossCreatorSig.CreateCross(tmpOutput, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1),
                                    outputDirectoryPath, templateDirectoryPath, xlApp, bgWorker, bgWorkerArg,out prgsmvmt,true, QC: QC,
                                    progressAvailable: progressAvailableSig, currentProgress: currentProgress, qc4FileName: qc4FileName, outputFiles: outputFiles, 
                                    outputFileSig:outputFileSig);
                            }
                            if (bgWorker.CancellationPending) return;
                            FinalizeOutput();
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
                Finalise(outputFileSig);

            }
            catch (Exception ex)
            {
                _log.Info(ex.StackTrace);
                foreach (var x in ThisReportBooks.Values)
                {
                    ReportBook rb = (ReportBook)x;
                    foreach (Workbook wb in rb.Books())
                    {
                        try
                        {
                            wb.Close(false);
                        }
                        catch (Exception ex1)
                        {
                        }
                    }
                }
                try
                {
                    TemplateBook.Close(false);
                }
                catch (Exception ex2)
                {
                }
                try
                {
                    FormatBook.Close(false);
                }
                catch (Exception ex2)
                {
                }
                throw ex;
            }
            finally
            {


                COMWholeOperate.releaseComObject<Worksheet>(ref FormatSheet);
                COMWholeOperate.releaseComObject<Worksheet>(ref DoubleFormatSheet);
                COMWholeOperate.releaseComObject<Worksheet>(ref TransposeFormatSheet);
                COMWholeOperate.releaseComObject<Worksheet>(ref TransposeDoubleFormatSheet);
                COMWholeOperate.releaseComObject<Worksheet>(ref GTFormatSheet);
                COMWholeOperate.releaseComObject<Worksheet>(ref WorkingSheet);

                COMWholeOperate.releaseComObject<Workbook>(ref TemplateBook);
                //COMWholeOperate.releaseComObject<Workbook>(ref TransposeTemplateBook);
                COMWholeOperate.releaseComObject<Workbook>(ref FormatBook);
                //COMWholeOperate.releaseComObject<Workbook>(ref TransposeFormatBook);


                COMWholeOperate.releaseComObject<Workbooks>(ref wbs);
                //COMWholeOperate.releaseComObject<Application>(ref xlApp);
                GC.Collect();
            }
        }


        public void InitializeOutput()
        {
            string FormatPath;
            if (FormatBook != null) { return; }
            FormatPath = FormatTemplatePath();
            FormatBook = wbs.Add(FormatPath);
            //FormatPath = FormatTemplatePath(TableOrientation.Portrait);
            //TransposeFormatBook = wbs.Add(FormatPath);
            FormatBook.Unprotect(BookPSWD);
            //TransposeFormatBook.Unprotect(BookPSWD);
        }

        private void updateProgress(double currentProgress, string v)
        {
            if (null != QC)
            {
                QC.updateProgress(currentProgress, v);
            }
        }

        private string FormatTemplatePath(TableOrientation Orientation = TableOrientation.Landscape)
        {
            string d;
            string n;
            if (Orientation != TableOrientation.Portrait) { Orientation = TableOrientation.Landscape; }
            if (Orientation == TableOrientation.Landscape)
            {
                n = FORMAT_TEMPLATE_NAME;
            }
            else
            {
                n = TRANSPOSE_FORMAT_TEMPLATE_NAME;
            }
            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, xlApp.PathSeparator);
            return OutputUtil.BuildPath(d, n, xlApp.PathSeparator);
        }


        private string TemplatePath(TableOrientation Orientation = TableOrientation.Landscape)
        {
            string d;
            string n;
            if (Orientation != TableOrientation.Portrait) { Orientation = TableOrientation.Landscape; }
            FileFormat = (XlFileFormat)ThisReportset.ParentRequest.ExcelFileFormat;
            if (FileFormat != XlFileFormat.xlExcel8 || ((ThisReportset.FileType & FileType.Excel) == 0))
            {
                FileFormat = XlFileFormat.xlOpenXMLWorkbook;
            }
            if (Orientation == TableOrientation.Landscape)
            {
                n = TEMPLATE_NAME;
            }
            else
            {
                n = TRANSPOSE_TEMPLATE_NAME;
            }
            if (FileFormat == XlFileFormat.xlOpenXMLWorkbook) { n = n + "x"; }
            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, xlApp.PathSeparator);
            return OutputUtil.BuildPath(d, n, xlApp.PathSeparator);
        }


        private string ReportBookPath(TableOrientation Orientation = TableOrientation.Landscape)
        {
            string d;
            string n;
            n = BOOK_NAME;
            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, xlApp.PathSeparator);
            return OutputUtil.BuildPath(d, n, xlApp.PathSeparator);
        }

        private void Initialize()
        {
            string TempPath;
            string pw;
            //xlApp = new Application();
            //xlApp.Visible = true;
            wbs = xlApp.Workbooks;
            //xlApp.ScreenUpdating = false;
            //xlApp.PrintCommunication = false;
            //xlApp.DisplayAlerts = false;
            ThisReportBooks = new Hashtable();
            TempPath = TemplatePath();
            TemplateBook = wbs.Add(TempPath);
            //TempPath = TemplatePath(TableOrientation.Portrait);
            //TransposeTemplateBook = wbs.Add(TempPath);
            TemplateBook.Unprotect(BookPSWD);
            WorkingSheet = TemplateBook.Worksheets.Add();
            //TransposeTemplateBook.Unprotect(BookPSWD);
        }

        public void CreateCross(OutputCross Output)
        {
            Worksheet tmpFormatSheet = null;
            string FormatSheetName = null;
            bool HasWeightColumn = false;
            int m = 0;
            int[] MaxAxesCountArray; // int
            bool HasWeightBack = false;
            bool HasWeight = false;
            Hashtable CutRowsCol = null;
            Hashtable CutColumnsCol = null;
            string FormatRangeNamePrefix = null;
            Workbook wb = null;
            CrossTable tmpTable = null;
            Worksheet sht = null;
            Array v = null; //string
            Array DataValue = null; //Variant
            Array Ranking = null; //int
            Array HatchingColorIndex = null; // XlColorIndex
            Array ArrowEnd = null; //Variant
            Array SigTestMarking = null;  //string
            bool isN = false;
            bool isMA = false;
            bool isSA = false;
            int i = 0;
            int j = 0;
            TableType tmpTableType = 0;
            string tableTypeBuf = null;
            Range StartCell = null;
            Range TableRange = null;
            Range SourceRange = null;
            Range tmpRange = null;
            int r = 0;
            int c = 0;
            int n = 0;
            //' 横％表用
            Range ColumnClusterGraphSpace = null;
            Range ColumnClusterGraphLegendArea = null;
            Range BarStackedGraphSpace = null;
            Range BarStackedGraphLegendArea = null;
            Range BarStackedGraphPlotArea = null;
            // ' 縦％表用
            Range BarClusterGraphSpace = null;
            Range BarClusterGraphPlotArea = null;
            Range BarClusterGraphLegendArea = null;
            Range ColumnStackedGraphSpace = null;
            Range ColumnStackedGraphLegendArea = null;
            Range ColumnStackedGraphPlotArea = null;
            bool DefHasNA = false;
            bool DefHasIV = false;
            int NAColumnIndex = 0;
            int IVColumnIndex = 0;
            bool CutMedian = false;
            int MedIdx = 0;
            Range tmpRange2 = null;
            bool CreateGraph = false;
            List<int> LinesIndexList = null;
            bool HasLines = false;
            string[] tmpBuf;
            int x = 0;
            ChartObjects chtObjs = null;
            ChartObject chtObj = null;
            SeriesCollection sCol = null;
            Points ps = null;
            Axis Axis = null;
            Array tmpVal;//int
            Worksheet ContentsTempSheet = null;
            TextBox MinBaseBox = null;
            GroupObject g = null;
            TextBox b = null;
            double d = 0;
            Array s = null;  //Shape;
            int cnt = 0;
            Shape l = null;
            Shape p = null;
            double lt = 0;
            double pt = 0;
            double o = 0;
            int clrIdx;
            double tmpTop = 0;
            double tmpLeft = 0;
            int SuperfluousColumn = 0;
            int tmpColumn = 0;
            string tmp = null;
            int OverRowsCount = 0;
            int OverColumnsCount = 0;
            double diff = 0;


            //    res  MethodResult
            //    Errors()  ErrorStruct, ErrorsCount  int

            //    On Error GoTo ErrHdl
            crossCreator = new CrossCreator();
            crossCreator.CurrentOutput = Output;
            crossCreator.WorkingSheet = WorkingSheet;
            crossCreator.BookPSWD = BookPSWD;
            crossCreator.SheetPSWD = SheetPSWD;
            crossCreator.xlApp = xlApp;
            crossCreator.WorkingBook = TemplateBook;
            crossCreator.wbs = wbs;
            crossCreator.WorkingSheet = WorkingSheet;
            crossCreator.bgWorker = bgWorker;

            HasWeightBack = Output.ShowPreWBTotal;
            MaxAxesCountArray = new int[Output.Tables.Count];

            for (i = 0; i < Output.Tables.Count; i++)
            {
                tmpTable = (CrossTable)Output.Tables[i];
                MaxAxesCountArray[i] = crossCreator.GetMaxAxesCount(tmpTable);
                m = m | MaxAxesCountArray[i];
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
            Sheets WithWbWorksheets = wb.Worksheets;
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
            if ((m & 2) == 2)
            { //' 三重あり
                if (Output.Orientation == TableOrientation.Landscape)
                {
                    if (FormatSheet == null)
                    {
                        FormatSheet = WithWbWorksheets.Item[FormatSheetName];
                        crossCreator.AdjustFormat(FormatSheet, null, 2, HasWeightColumn);
                    }
                }
                else
                {
                    if (TransposeFormatSheet == null)
                    {
                        TransposeFormatSheet = WithWbWorksheets.Item[FormatSheetName];
                        crossCreator.AdjustFormat(TransposeFormatSheet, null, 2, HasWeightColumn);
                    }
                }
            }
            if ((m & 1) == 1)
            {// ' 二重あり
                if (Output.Orientation == TableOrientation.Landscape)
                {
                    if (DoubleFormatSheet == null)
                    {
                        if ((m & 2) == 0)
                        {
                            DoubleFormatSheet = WithWbWorksheets.Item[FormatSheetName];
                        }
                        else
                        {
                            FormatSheet.Copy(After: FormatSheet);
                            DoubleFormatSheet = FormatSheet.Next;
                        }
                        crossCreator.AdjustFormat(DoubleFormatSheet, null, 1, HasWeightColumn, FormatSheet != null);
                    }
                }
                else
                {
                    if (TransposeDoubleFormatSheet == null)
                    {
                        if ((m & 2) == 0)
                        {
                            TransposeDoubleFormatSheet = WithWbWorksheets.Item[FormatSheetName];
                        }
                        else
                        {
                            TransposeFormatSheet.Copy(After: TransposeFormatSheet);
                            TransposeDoubleFormatSheet = TransposeFormatSheet.Next;
                        }
                        crossCreator.AdjustFormat(TransposeDoubleFormatSheet, null, 1, HasWeightColumn, TransposeFormatSheet != null, true);
                    }
                }
            }
            DefHasNA = Output.ShowNAAtItem;
            DefHasIV = Output.ShowIVAtItem;
            CutMedian = Output.ParentRequest.ShowMedian & Output.WBOn;
            //CutMedian = false; // 167853 - show median allways 
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
            ContentsTempSheet = TemplateBook.Worksheets.Item["INDEX"];
            g = ContentsTempSheet.GroupObjects("RateDifferenceLegend");
            b = ContentsTempSheet.TextBoxes("SignificanceTestLegend");
            d = b.Left - g.Left - g.Width;
            g = null;
            b = null;

            double progressStep = progressAvailableRpt / Output.Tables.Count;
            for (i = 0; i < Output.Tables.Count; i++)
            {
                if (bgWorker.CancellationPending) return;
                updateProgress(currentProgress, String.Format(LocalResource.PB_EXCEL_GEN_TABLE, (i + 1), Output.Tables.Count));
                currentProgress += progressStep;

                tmpTable = (CrossTable)Output.Tables[i];
                isN = (tmpTable.Question.QuestionType & QuestionType.N) == QuestionType.N;
                isMA = (tmpTable.Question.QuestionType & QuestionType.MA) == QuestionType.MA;
                HasWeight = crossCreator.GetHasWeight(tmpTable);
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
                crossCreator.GetCutRowsAndColumns(tmpTable, HasWeightBack, HasWeight, MaxAxesCountArray[i], ref CutRowsCol, ref CutColumnsCol, ref MedIdx, true, CutMedian);
                if (DefHasNA)
                {
                    NAColumnIndex = tmpTable.GetTableValueColumnIndexMaximum - (CrossCreator.ToInt(HasWeight) & 2) - (CrossCreator.ToInt(DefHasIV) & 1) - (tmpTable.Question.SubTotalCnt);
                    if (CutColumnsCol.ContainsKey(NAColumnIndex)) { NAColumnIndex = 0; }
                }
                if (DefHasIV)
                {
                    IVColumnIndex = tmpTable.GetTableValueColumnIndexMaximum - (CrossCreator.ToInt(HasWeight) & 2) - (tmpTable.Question.SubTotalCnt);
                    if (CutColumnsCol.ContainsKey(IVColumnIndex)) { IVColumnIndex = 0; }
                }
                if (Output.Orientation == TableOrientation.Landscape)
                {
                    if (MaxAxesCountArray[i] == 2)
                    {
                        sht = AddSheet(tmpTable, TemplateBook.Worksheets.Item["Cross_Triple_Std" + (isN ? "_N" : "")]);
                        tmpFormatSheet = FormatSheet;
                    }
                    else
                    {
                        sht = AddSheet(tmpTable, TemplateBook.Worksheets.Item["Cross_Double_Std" + (isN ? "_N" : "")]);
                        tmpFormatSheet = DoubleFormatSheet;
                    }
                }
                else
                {
                    if (HasWeight)
                    {
                        sht = AddSheet(tmpTable, TransposeTemplateBook.Worksheets.Item["Cross_WT_Template"]);
                    }
                    else
                    {
                        sht = AddSheet(tmpTable, TransposeTemplateBook.Worksheets.Item["Cross_Template"]);
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
                //tmp = tmpTable.Comment;
                //if (tmp != null && tmp.Length > 0)
                //{
                //    Range WithshtRange = sht.Range["Comment"];
                //    OutputUtil.PutValue(WithshtRange.Cells, ref tmp);
                //    OutputUtil.AutoFitEx(WithshtRange.MergeArea.Rows, true, xlApp);
                //}
                //else
                //{
                //    Name WithshtRange = sht.Names.Item("Comment");
                //    WithshtRange.RefersToRange.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                //    WithshtRange.Delete();
                //}
                tmp = LocalResource.CR_FILTER_PREFIX + Output.LocalizedFilteringExpression;
                if (tmp != null && tmp.Length > 0)
                {
                    Range WithshtRange = sht.Range["Filter"];
                    OutputUtil.PutValue(WithshtRange.Cells, ref tmp);
                    //OutputUtil.AutoFitEx(WithshtRange.MergeArea.Rows, xlApp, WorkingSheet);
                }
                //else
                //{
                //    Name WithshtRange = sht.Names.Item("Criteria");
                //    WithshtRange.RefersToRange.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                //    WithshtRange.Delete();
                //}
                StartCell = sht.Range["OutputStart"];
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
                int BarGrpahStartCol = 0;
                if (Output.Orientation == TableOrientation.Landscape)
                {
                    Hashtable WholeRowColRef = null;// only for ref 
                    bool CheckOverRowTmpRef = false; // only for ref 
                    crossCreator.CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking,
                        ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking, 1 + 1,
                        1 + MaxAxesCountArray[i], HasWeight, isN, tmpTableType, sht.Rows.Count - StartCell.Row,
                        sht.Columns.Count - 1, ref CheckOverRowTmpRef, WholeRowColRef, ref OverRowsCount, ref OverColumnsCount, true);
                    if (OverColumnsCount > 0)
                    {
                        throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                        // Err().Raise vbObjectError + 400&, RunningProcName _
                        //         , ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailMessageIndex, tmpTable.Question.Name, tableTypeBuf)
                    }
                    if (tmpTableType == TableType.Per & !isN)// top chart
                    {
                        if (OverRowsCount + OverColumnsCount == 0)
                        {
                            // switch (tmpTable.Chart.ChartType)
                            // {
                            //    case XlChartType.xlColumnClustered:
                            //   case XlChartType.xlBarStacked100:
                            CreateGraph = true;
                            r = StartCell.Row;
                            LinesIndexList = GetLinesIndexList(tmpTable, CutRowsCol, 1 + 1);
                            if (LinesIndexList == null || LinesIndexList.Count == 0)
                            {
                            }
                            else
                            {
                                HasLines = true;
                            }
                            //' 縦棒＋折れ線のときは凡例スペースを確保する
                            // if (HasLines)
                            {
                                tmpRange = tmpFormatSheet.Range["ColumnClusterWithLineGraphSpace"].EntireRow;
                                tmpRange2 = tmpFormatSheet.Range["ColumnClusterGraphLegendArea"];
                            }
                            //else
                            //{
                            //    tmpRange = tmpFormatSheet.Range["ColumnClusterGraphSpace"].EntireRow;
                            //}
                            tmpRange.Copy();
                            StartCell.EntireRow.Insert(XlInsertShiftDirection.xlShiftDown);
                            xlApp.CutCopyMode = (XlCutCopyMode)1;
                            ColumnClusterGraphSpace = sht.Rows.Item[r].Resize[tmpRange.Count];
                            if (HasLines)
                            {
                                ColumnClusterGraphLegendArea = sht.Cells.Item[r, tmpRange2.Column].Resize(tmpRange2.Rows.Count, tmpRange2.Columns.Count);
                            }
                            //         break;
                            // }
                        }
                    }
                    crossCreator.FormatLandscapeTable(tmpTable, sht, CutRowsCol, CutColumnsCol, tmpFormatSheet, FormatRangeNamePrefix, tmpTableType
                                            , HasWeight, StartCell, isN, null, true, CutMedian, MedIdx);
                    if (bgWorker.CancellationPending) return;

                    if (tmpTableType == TableType.Per & !isMA) // side chart
                    {
                        if (OverRowsCount + OverColumnsCount == 0)
                        {
                            // switch (tmpTable.Chart.ChartType)
                            // {
                            //     case XlChartType.xlColumnClustered:
                            //   case XlChartType.xlBarStacked100:
                            CreateGraph = true;
                            int tableColCnt = tmpTable.GetTableValueColumnIndexMaximum - tmpTable.GetTableValueColumnIndexMinimum + 1  // to do hasweigth cehck
                                  - CutColumnsCol.Count;
                            int gapBwTables = 3;
                            int legendLength = isN ? 0 : 2; // to do
                            BarGrpahStartCol = tableColCnt + gapBwTables;
                            r = StartCell.Offset[-legendLength, BarGrpahStartCol].Row;
                            //tmpRange = tmpFormatSheet.Range["BarStackedGraphSpace"].EntireRow;
                            int BarStackedGraphSpaceLength = tmpFormatSheet.Range["BarStackedGraphSpace"].Rows.Count;
                            int BarStackedGraphSpaceRow = tmpFormatSheet.Range["BarStackedGraphSpace"].Row + (isN ? 2 : 0); // to do
                            int BarStackedGraphPlotAreaLength = tmpFormatSheet.Range["BarStackedGraphPlotArea"].Columns.Count;
                            int BarStackedGraphPlotAreaCol = tmpFormatSheet.Range["BarStackedGraphPlotArea"].Column;
                            tmpRange = tmpFormatSheet.Range[tmpFormatSheet.Cells[BarStackedGraphSpaceRow, 1], tmpFormatSheet.Cells[BarStackedGraphSpaceRow + BarStackedGraphSpaceLength, BarStackedGraphPlotAreaCol + BarStackedGraphPlotAreaLength]]; // to do
                            tmpRange.Copy();
                            StartCell.Offset[-legendLength, BarGrpahStartCol].PasteSpecial(); //Insert(XlInsertShiftDirection.xlShiftDown);
                            xlApp.CutCopyMode = (XlCutCopyMode)1;

                            BarStackedGraphSpace = sht.Rows.Item[r].Resize(tmpRange.Count);
                            Range WithtmpFormatSheet = tmpFormatSheet.Range["BarStackedGraphLegendArea"];
                            r = WithtmpFormatSheet.Row - tmpRange.Row + 1;
                            c = WithtmpFormatSheet.Column + BarGrpahStartCol;
                            BarStackedGraphLegendArea = BarStackedGraphSpace.Cells.Item[r, c].Resize(WithtmpFormatSheet.Rows.Count, WithtmpFormatSheet.Columns.Count);
                            WithtmpFormatSheet = tmpFormatSheet.Range["BarStackedGraphPlotArea"];
                            r = WithtmpFormatSheet.Row - tmpRange.Row + 1;
                            c = WithtmpFormatSheet.Column + BarGrpahStartCol;
                            BarStackedGraphPlotArea = BarStackedGraphSpace.Cells.Item[r, c].Resize(WithtmpFormatSheet.Rows.Count, WithtmpFormatSheet.Columns.Count);
                            n = tmpTable.GetTableValueRowIndexMaximum - tmpTable.GetTableValueRowIndexMinimum + 1
                                - 1 - 1 - CutRowsCol.Count;
                            if (!isN)
                            {
                                Range percentageSymbol = sht.Cells[BarStackedGraphPlotArea.Row - 1, BarStackedGraphPlotArea.Column + BarStackedGraphPlotAreaLength - (CrossCreator.ToInt(Output.ShowPreWBTotal) & 1)];
                                percentageSymbol.Value2 = "(%)";
                                percentageSymbol.HorizontalAlignment = XlHAlign.xlHAlignRight;
                                percentageSymbol.VerticalAlignment = XlVAlign.xlVAlignBottom;
                            }
                            if (n > BarStackedGraphPlotArea.Rows.Count)
                            {
                                Range WithBarStackedGraphPlotArea = BarStackedGraphPlotArea.Rows.Item[2];
                                WithBarStackedGraphPlotArea.Copy();
                                WithBarStackedGraphPlotArea.Resize[n - BarStackedGraphPlotArea.Rows.Count].Insert(XlInsertShiftDirection.xlShiftDown);
                                xlApp.CutCopyMode = (XlCutCopyMode)1;
                            }
                            else if (n < BarStackedGraphPlotArea.Rows.Count)
                            {
                                BarStackedGraphPlotArea.Rows.Item[2].Resize(BarStackedGraphPlotArea.Rows.Count - n).Delete(XlDeleteShiftDirection.xlShiftUp);
                            }
                            //         break;
                            //}
                        }
                    }
                }
                else
                {    //' Output.Orientation = TableOrientation_Portrait
                }
                xlApp.ScreenUpdating = true;
                Range WithStartCell = StartCell.Range["B1"].Resize[v.GetUpperBound(0), v.GetUpperBound(1)];
                TableRange = WithStartCell.Cells;
                SuperfluousColumn = WithStartCell.Column + WithStartCell.Columns.Count;
                Range WithQuestionDesc = WithStartCell.Worksheet.Range["QuestionDescription"].MergeArea;
                tmpColumn = WithQuestionDesc.Column + WithQuestionDesc.Columns.Count;
                if (tmpColumn > SuperfluousColumn) { SuperfluousColumn = tmpColumn; }
                Range WithWorksheetCol = WithStartCell.Worksheet.Columns;
                if (SuperfluousColumn <= WithWorksheetCol.Count)
                {
                    WithWorksheetCol.Item[SuperfluousColumn].ColumnWidth = WithWorksheetCol.Item[1].ColumnWidth;
                }
                if (Output.WBOn)
                {
                    if (Output.Orientation == TableOrientation.Landscape)
                    {
                        v.SetValue(LocalResource.REPORT_MARKING_LEGEND_WEIGHTBACK_ON_PROMPT, 1 + 1, 1);
                    }
                    else
                    {
                        //' v(1& + MaxAxesCountArray(i), 1) = ThisWorkbook.yoyoReportKeyword(ReportMessageIndex_ReportMarkingLegendWeightbackOnPromptIndex)
                        v.SetValue(LocalResource.REPORT_MARKING_LEGEND_WEIGHTBACK_ON_PROMPT, 2, 1);
                    }
                }
                OutputUtil.PutValue(WithStartCell.Cells, ref v);
                Range dataRange = WithStartCell.Worksheet.Range[WithStartCell.Item[DataValue.GetLowerBound(0),
                    DataValue.GetLowerBound(1)], WithStartCell.Item[WithStartCell.Rows.Count, WithStartCell.Columns.Count]];
                OutputUtil.PutValue(dataRange, ref DataValue);
                _log.Info("Auto fit started");
                CrossCreator.AutoFit(dataRange, colWidthMap);
                _log.Info("Auto fit started complted");
                _log.Info("AutoFitEx start");
                Range labelRange = WithStartCell.Worksheet.Range[WithStartCell.Item[DataValue.GetLowerBound(0) + 1,
                    1], WithStartCell.Item[WithStartCell.Rows.Count, DataValue.GetLowerBound(1) - 1]];
                OutputUtil.AutoFitEx(WithStartCell.Worksheet.Rows.Item[2], xlApp, WorkingSheet, CrossCreator.ROW_MAX_HEIGHT);
                OutputUtil.AutoFitEx(WithStartCell.Worksheet.Rows.Item[3], xlApp, WorkingSheet, CrossCreator.ROW_MAX_HEIGHT);
                OutputUtil.AutoFitEx(WithStartCell.Worksheet.Rows.Item[4], xlApp, WorkingSheet, CrossCreator.ROW_MAX_HEIGHT);
                OutputUtil.AutoFitEx(WithStartCell.Worksheet.Rows.Item[6], xlApp, WorkingSheet, CrossCreator.ROW_MAX_HEIGHT);
                OutputUtil.AutoFitEx(WithStartCell.Worksheet.Rows.Item[7], xlApp, WorkingSheet, CrossCreator.ROW_MAX_HEIGHT);
                //OutputUtil.AutoFitExCrossLabel(labelRange, xlApp, WorkingSheet, CrossCreator.ROW_MAX_HEIGHT);
                _log.Info("AutoFitEx completed");
                if (HasWeight)
                {
                    if (Output.Orientation == TableOrientation.Landscape)
                    {
                        Range WithResize = WithStartCell.Item[DataValue.GetLowerBound(0) - 1, DataValue.GetLowerBound(1) + (CrossCreator.ToInt(Output.ShowPreWBTotal) & 1) + 1].Resize(ColumnSize: tmpTable.SectorsCount);
                        WithResize.Value = WithResize.Value;
                    }
                    else
                    {
                        Range WithResize = WithStartCell.Item[DataValue.GetLowerBound(0) + (CrossCreator.ToInt(Output.ShowPreWBTotal) & 1) + 1, DataValue.GetLowerBound(1) - 1].Resize[tmpTable.SectorsCount * (1 + (CrossCreator.ToInt(!isN) & 1))];
                        WithResize.Value = WithResize.Value;
                    }
                }
                if (BarStackedGraphPlotArea != null) // side chart
                {
                    tmpRange = WithStartCell.Worksheet.Range[WithStartCell.Item[DataValue.GetLowerBound(0), 1], WithStartCell.Item[WithStartCell.Rows.Count, DataValue.GetLowerBound(1) + (CrossCreator.ToInt(HasWeightBack) & 1)]];
                    //tmpRange2 = BarStackedGraphPlotArea.EntireRow.Range["B1"];
                    tmpRange2 = BarStackedGraphPlotArea.EntireRow.Cells[BarGrpahStartCol + 2]; // to do
                    tmpRange.Copy(tmpRange2);

                    Range totalArea = WithStartCell.Worksheet.Range[WithStartCell.Item[DataValue.GetLowerBound(0) - 1, DataValue.GetLowerBound(1)]
                         , WithStartCell.Item[DataValue.GetLowerBound(0) - 1, DataValue.GetLowerBound(1) + (CrossCreator.ToInt(HasWeightBack) & 1)]];
                    Range totalArea1 = BarStackedGraphPlotArea.EntireRow.Offset[-1].Cells[BarGrpahStartCol + 1 + DataValue.GetLowerBound(1)];
                    totalArea.Copy(totalArea1);
                    totalArea1.Resize[ColumnSize: 1 + (CrossCreator.ToInt(HasWeightBack) & 1)].Borders.Item[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlLineStyleNone;
                    //totalArea = totalArea.Resize[ColumnSize: 1 + (CrossCreator.ToInt(HasWeightBack) & 1)];
                    //totalArea.Value = value2;


                    Range WithtmpRangeCols = tmpRange.Columns;
                    for (j = 1; j <= WithtmpRangeCols.Count; j++)
                    {
                        tmpRange2.Columns.Item[j].ColumnWidth = WithtmpRangeCols.Item[j].ColumnWidth;
                    }
                    tmpRange2.ColumnWidth = tmpRange.ColumnWidth;
                    Border WithtmpRange2Resize = tmpRange2.Resize[tmpRange.Rows.Count, tmpRange.Columns.Count].Borders.Item[XlBordersIndex.xlEdgeBottom];
                    WithtmpRange2Resize.LineStyle = XlLineStyle.xlContinuous;
                    WithtmpRange2Resize.Weight = XlBorderWeight.xlThin;
                    WithtmpRange2Resize.Color = CrossCreator.BORDER_COLOR;
                    tmpRange = tmpRange2.Item[1, tmpRange.Columns.Count].Resize(tmpRange.Rows.Count);
                    Range WithtmpRange = tmpRange.Rows;
                    for (j = 2; j <= WithtmpRange.Count - 1; j++)
                    {
                        XlBorderWeight borderWeight = (XlBorderWeight)WithtmpRange.Item[j].Borders.Item[XlBordersIndex.xlEdgeBottom].Weight;
                        if (borderWeight == XlBorderWeight.xlHairline)
                        {
                            BarStackedGraphPlotArea.Rows.Item[j].Borders.Item[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlLineStyleNone;
                        }
                    }
                }
                else if (BarClusterGraphPlotArea != null)
                {// not using 
                    tmpRange = WithStartCell.Worksheet.Range[WithStartCell.Item[DataValue.GetLowerBound(0) + (CrossCreator.ToInt(HasWeightBack) & 1) + 1, 1], WithStartCell.Item[WithStartCell.Rows.Count - (CrossCreator.ToInt(HasWeight) & 2), DataValue.GetLowerBound(1) - 1]];
                    tmpRange2 = BarClusterGraphPlotArea.EntireRow.Range["B1"];
                    tmpRange.Copy(tmpRange2);
                    Border WithtmpRange2Resize = tmpRange2.Resize[tmpRange.Rows.Count, tmpRange.Columns.Count].Borders.Item[XlBordersIndex.xlEdgeBottom];
                    WithtmpRange2Resize.LineStyle = XlLineStyle.xlContinuous;
                    WithtmpRange2Resize.Weight = XlBorderWeight.xlThin;
                    WithtmpRange2Resize.Color = CrossCreator.BORDER_COLOR;
                    tmpRange = tmpRange2.Item[1, tmpRange.Columns.Count].Resize(tmpRange.Rows.Count);
                    Range WithtmpRange = tmpRange.Rows;
                    for (j = 1; j <= WithtmpRange.Count - 1; j++)
                    {
                        BarClusterGraphPlotArea.Rows.Item[j].Borders.Item(XlBordersIndex.xlEdgeBottom).Weight = WithtmpRange.Item[j].Borders.Item(XlBordersIndex.xlEdgeBottom).Weight;
                    }
                }
                if (CreateGraph)
                {
                    chtObjs = WithStartCell.Worksheet.ChartObjects();
                    if (Output.Orientation == TableOrientation.Landscape)
                    {
                        if (!isN && (!NPOICrossCreator.checkSimpleAggr(tmpTable as CrossTable) || tmpTable.AxesGroups.Count == 1)) // top chart
                        {
                            //switch (tmpTable.Chart.ChartType)
                            //{
                            // case XlChartType.xlColumnClustered:
                            // case XlChartType.xlBarStacked100:
                            SourceRange = WithStartCell.Item[DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) + (CrossCreator.ToInt(HasWeightBack) & 1) + 1]
                                                // SourceRange = WithStartCell.Item[DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) + 1 + 1] // to do why hasweight no affect
                                                .Resize(ColumnSize: tmpTable.SectorsCount + (CrossCreator.ToInt(NAColumnIndex > 0) & 1) + (CrossCreator.ToInt(IVColumnIndex > 0) & 1));
                            tmpRange = SourceRange.Item[1, 0].Resize(ColumnSize: SourceRange.Columns.Count + 1);
                            tmpRange = xlApp.Intersect(tmpRange.EntireColumn, ColumnClusterGraphSpace);
                            chtObj = chtObjs.Add(tmpRange.Left, tmpRange.Top - 12, tmpRange.Width + 12, tmpRange.Height + 24);
                            // With chtObj
                            chtObj.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;
                            Chart WithchtObjChart = chtObj.Chart;
                            ChartArea WithchtObjChartArea = WithchtObjChart.ChartArea;
                            WithchtObjChartArea.Border.LineStyle = XlLineStyle.xlLineStyleNone;
                            WithchtObjChartArea.AutoScaleFont = false;
                            Font WithWithchtObjChartAreaFont = WithchtObjChartArea.Font;
                            try
                            {
                                WithWithchtObjChartAreaFont.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                            }
                            catch (Exception ex) { };// to do 
                            WithWithchtObjChartAreaFont.Size = 8;
                            WithWithchtObjChartAreaFont.Bold = Microsoft.Office.Core.MsoTriState.msoFalse;
                            WithchtObjChart.SetSourceData(SourceRange, XlRowCol.xlRows);
                            WithchtObjChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered;
                            sCol = WithchtObjChart.SeriesCollection();
                            //sCol.Item(1).Name = Convert.ToString(v.GetValue(2 + 1, 1));
                            if (HasLines)
                            {
                                sCol.Item(1).Name = Convert.ToString(v.GetValue(2 + 1, 1));
                                for (j = 0; j < LinesIndexList.Count; j++)
                                {
                                    if (Convert.ToInt32(LinesIndexList[j]) >= 2 || Convert.ToInt32(LinesIndexList[j]) <= v.GetUpperBound(0) - (1 + 1))
                                    {
                                        x = Convert.ToInt32(LinesIndexList[j]) + 1 + 1;
                                        if (MaxAxesCountArray[i] == 2)
                                        {
                                            if ((v.GetValue(x, 3)) != null)
                                            {
                                                tmpBuf = new string[1];
                                                tmpBuf[0] = Convert.ToString(v.GetValue(x, 2));
                                            }
                                            else
                                            {
                                                tmpBuf = new string[2];
                                                tmpBuf[1] = Convert.ToString(v.GetValue(x, 3));
                                                for (x = x; x >= 1 + 1 + 1; x--)
                                                {
                                                    if (v.GetValue(x, 2) != null)
                                                    { // to do length cehck
                                                        tmpBuf[0] = Convert.ToString(v.GetValue(x, 2));
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tmpBuf = new string[1];
                                            tmpBuf[0] = Convert.ToString(v.GetValue(x, 2));
                                        }
                                        sCol.Add(SourceRange.Rows.Item[(LinesIndexList[j])]);
                                        Series WithsCol1 = sCol.Item(sCol.Count);
                                        WithsCol1.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
                                        WithsCol1.Name = OutputUtil.RemoveLeadingSpclChar(String.Join(" - ", tmpBuf));
                                        WithsCol1.MarkerStyle = XlMarkerStyle.xlMarkerStyleSquare;
                                        WithsCol1.MarkerSize = 6;
                                        WithsCol1.MarkerBackgroundColor = ColorPallet.colorIndex[ColorPallet.colorLineIndex[(j) % ColorPallet.colorLineIndex.Length]];
                                        WithsCol1.MarkerForegroundColor = 0xFFFFFF;
                                        WithsCol1.Format.Line.Weight = 0.75F;
                                        WithsCol1.Smooth = false;
                                        WithsCol1.Shadow = false;
                                    }
                                }
                            }
                            xlApp.ScreenUpdating = true;
                            xlApp.ScreenUpdating = false;
                            WithchtObjChart.HasTitle = false;
                            WithchtObjChart.HasLegend = sCol.Count > 1;
                            PlotArea WithPlotArea = WithchtObjChart.PlotArea;
                            if (WithchtObjChart.HasLegend)
                            {
                                Legend WithLegendCl = WithchtObjChart.Legend;
                                WithLegendCl.Position = XlLegendPosition.xlLegendPositionLeft;
                                WithLegendCl.Width = OutputUtil.MAX_LEGEND_WIDTH;
                                // WithLegendCl.Border.LineStyle = XlLineStyle.xlContinuous;
                                // WithLegendCl.Border.Color = OutputUtil.LINE_COLOR;
                                // WithLegendCl.Border.Weight = XlBorderWeight.xlHairline;
                            }
                            //WithPlotArea.Interior.ColorIndex = tmpTable.Chart.InteriorColorIndex;
                            if (tmpTable.Chart.InteriorGradientStyle != 0)
                            {
                                WithPlotArea.Fill.OneColorGradient((Microsoft.Office.Core.MsoGradientStyle)tmpTable.Chart.InteriorGradientStyle, tmpTable.Chart.GradientVariant, (float)0.9);
                            }
                            WithPlotArea.Border.LineStyle = XlLineStyle.xlLineStyleNone;
                            Axis = WithchtObjChart.Axes(XlAxisType.xlCategory);
                            Axis.MajorTickMark = XlTickMark.xlTickMarkNone;
                            Axis.TickLabelPosition = XlTickLabelPosition.xlTickLabelPositionNone;
                            Axis.Border.Color = OutputUtil.LINE_COLOR;

                            Axis = WithchtObjChart.Axes(XlAxisType.xlValue);
                            Axis.MinimumScale = 0;
                            Axis.MaximumScale = 100;
                            Axis.MajorUnit = 20;
                            Axis.MajorTickMark = XlTickMark.xlTickMarkInside;
                            Axis.TickLabels.NumberFormat = @"0""%""";
                            Axis.Border.Color = OutputUtil.LINE_COLOR;
                            Border WithMajorGridlinesBorder = Axis.MajorGridlines.Border;
                            WithMajorGridlinesBorder.ColorIndex = 15;
                            WithMajorGridlinesBorder.Weight = XlBorderWeight.xlHairline;
                            WithMajorGridlinesBorder.LineStyle = XlLineStyle.xlDot;

                            WithchtObjChart.ChartGroups(1).GapWidth = 60;
                            Series WithsCol = sCol.Item(1);
                            WithsCol.Format.Line.ForeColor.RGB = OutputUtil.LINE_COLOR;
                            WithsCol.Format.Line.Weight = OutputUtil.LINE_WEIGHT;// (float)0.75;
                            //WithsCol.Interior.ColorIndex = tmpTable.Chart.SeriesColorIndex(0);// to do
                            WithsCol.Interior.Color = 0xF2F2F2;
                            if (tmpTable.Question.SubTotalCnt > 0)
                            {
                                Points points = WithsCol.Points();
                                for (int idx = points.Count - tmpTable.Question.SubTotalCnt + 1; idx <= points.Count; idx++)
                                {
                                    Point point = WithsCol.Points(idx);
                                    point.Interior.Color = 0xECDFE4;
                                }
                            }
                            if (tmpTable.Chart.GradientStyle != 0)
                            {
                                //WithsCol.Fill.OneColorGradient((Microsoft.Office.Core.MsoGradientStyle)tmpTable.Chart.GradientStyle, tmpTable.Chart.GradientVariant, (float)0.9);
                            }
                            WithsCol.ApplyDataLabels();
                            WithsCol.DataLabels().Separator = " ";


                            for (j = 2; j <= sCol.Count; j++)
                            {
                                if (tmpTable.Chart.SeriesCount <= 1)
                                {
                                    x = 0;
                                }
                                else
                                {
                                    x = (j - 2) % (ColorPallet.colorLineIndex.Length);
                                }
                                Border WithBorder = sCol.Item(j).Border;
                                //WithBorder.Color = ColorPallet.colorIndex[ColorPallet.colorLineIndex[x]];
                                WithBorder.Color = ColorPallet.colorIndex[ColorPallet.colorLineIndex[x]];
                                //ColorPallet.colorIndex[ColorPallet.colorLineIndex[(j - 2) % ColorPallet.colorLineIndex.Length]];
                            }
                            OutputUtil.FitChartWidthAndPositionToRangeTop(chtObj, SourceRange.Rows.Item[-1], xlApp, tmpTable.SectorsCount);
                            if (WithchtObjChart.HasLegend)
                            {
                                Legend WithLegendCl = WithchtObjChart.Legend;
                                double WithPlotAreaWidth = WithPlotArea.Width;
                                double WithPlotAreaLeft = WithPlotArea.Left;
                                double WithchtObjChartParentLeft = WithchtObjChart.Parent.Left;
                                double WithLegendClWidth = WithLegendCl.Width;
                                double legendOverlap = WithLegendCl.Left + WithLegendCl.Width - WithPlotAreaLeft;
                                double legndWidth = WithchtObjChartParentLeft - OutputUtil.CHART_LEFT;

                                WithchtObjChart.Parent.Width = WithchtObjChart.Parent.Width + legndWidth;
                                WithchtObjChart.Parent.Left = WithchtObjChartParentLeft - legndWidth;
                                WithPlotArea.Width = WithPlotAreaWidth;
                                WithPlotArea.Left = WithPlotAreaLeft + legndWidth;

                                double WithLegendClWidthNew = WithLegendClWidth + legndWidth;
                                if (legendOverlap > 0)
                                {
                                    WithLegendClWidthNew = WithLegendClWidthNew - legendOverlap;
                                }
                                WithLegendCl.Width = WithLegendClWidthNew;
                            }

                            //if (WithchtObjChart.HasLegend)
                            //{
                            //    diff = 0;
                            //    Legend WithLegendCl2 = WithchtObjChart.Legend;
                            //    if (chtObj.Top + OutputUtil.OVER_2007_CHART_AREA_TOP + WithLegendCl2.Top + WithLegendCl2.Height
                            //            > ColumnClusterGraphLegendArea.Top + ColumnClusterGraphLegendArea.Height)
                            //    {

                            //        WithLegendCl2.Left = ColumnClusterGraphLegendArea.Left - chtObj.Left - OutputUtil.OVER_2007_CHART_AREA_LEFT;
                            //        WithLegendCl2.Width = ColumnClusterGraphLegendArea.Width;
                            //        WithLegendCl2.Top = ColumnClusterGraphLegendArea.Top - chtObj.Top - OutputUtil.OVER_2007_CHART_AREA_TOP;

                            //        if (WithLegendCl2.Height > ColumnClusterGraphLegendArea.Height)
                            //        {
                            //            diff = WithLegendCl2.Height - ColumnClusterGraphLegendArea.Height;
                            //            WithLegendCl2.Height = ColumnClusterGraphLegendArea.Height;
                            //        }
                            //    }
                            //    if (diff > 0)
                            //    {
                            //        PlotArea WithPlotArea1 = WithchtObjChart.PlotArea;
                            //        WithPlotArea1.Top = WithPlotArea1.Top - diff;
                            //        WithPlotArea1.Height = WithPlotArea1.Height + diff;
                            //    }
                            //}
                            if (FileFormat == XlFileFormat.xlExcel8)
                            {
                                PlotArea WithPlotArea2 = WithchtObjChart.PlotArea;
                                WithPlotArea2.Width = WithPlotArea2.Width + 2 * 0.75;
                            }
                            //break;
                        }
                        // case XlChartType.xlBarStacked100:
                        //' 要素名の設定は不要
                        if (!isMA)  // side chart
                        {
                            int avgCol = 2;
                            tmpRange = WithStartCell.Item[1, DataValue.GetLowerBound(1) + (CrossCreator.ToInt(HasWeightBack) & 1) + 1 + (isN ? avgCol : 0)]
                            .Resize(ColumnSize: (isN ? 1 : (tmpTable.SectorsCount - tmpTable.Question.SubTotalCnt) + (CrossCreator.ToInt(NAColumnIndex > 0) & 1) + (CrossCreator.ToInt(IVColumnIndex > 0) & 1))); // to do
                            tmpRange2 = WithStartCell.Worksheet.Range[tmpRange.Rows.Item[DataValue.GetLowerBound(0)], tmpRange.Rows.Item[v.GetUpperBound(0)]];
                            SourceRange = xlApp.Union(tmpRange, tmpRange2);
                            if (!Output.ShowPreWBTotal)
                            {
                                Range WithBarPlot = BarStackedGraphPlotArea.Columns.Item[2];
                                WithBarPlot.Copy();
                                WithBarPlot.Insert(XlInsertShiftDirection.xlShiftToRight);
                                xlApp.CutCopyMode = (XlCutCopyMode)1;
                            }
                            tmpRange = WithStartCell.Worksheet.Range[BarStackedGraphLegendArea, BarStackedGraphPlotArea];
                            chtObj = chtObjs.Add(tmpRange.Left - 12, tmpRange.Top - 12, tmpRange.Width + 24, tmpRange.Height + 24);
                            chtObj.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;
                            Chart WithchtObjChartStacked = chtObj.Chart;
                            ChartArea WithWithchtObjChartStackedArea = WithchtObjChartStacked.ChartArea;
                            WithWithchtObjChartStackedArea.Border.LineStyle = XlLineStyle.xlLineStyleNone;
                            WithWithchtObjChartStackedArea.AutoScaleFont = false;
                            Font WithFont = WithWithchtObjChartStackedArea.Font;
                            try
                            {
                                WithFont.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                            }
                            catch (Exception ex) { };// to do 
                            WithFont.Size = 8;
                            WithFont.Bold = Microsoft.Office.Core.MsoTriState.msoFalse;
                            WithchtObjChartStacked.SetSourceData(SourceRange, XlRowCol.xlColumns);
                            if (isN)
                            {
                                SeriesCollection sss = WithchtObjChartStacked.SeriesCollection();
                                sss.Item(1).Values = tmpRange2;
                                sss.Item(1).Name = OutputUtil.RemoveLeadingSpclChar(SourceRange.Item[1].Value2);
                                //WithchtObjChartStacked.SetElement(Microsoft.Office.Core.MsoChartElementType.msoElementChartTitleNone);
                            }
                            WithchtObjChartStacked.ChartType = isN ? Microsoft.Office.Interop.Excel.XlChartType.xlBarClustered : Microsoft.Office.Interop.Excel.XlChartType.xlBarStacked100;
                            xlApp.ScreenUpdating = true;
                            xlApp.ScreenUpdating = false;
                            try
                            {
                                WithchtObjChartStacked.ChartTitle.Delete();
                            }
                            catch (Exception ex) { };
                            WithchtObjChartStacked.HasTitle = false;
                            WithchtObjChartStacked.HasLegend = true;
                            Legend WithLegend = WithchtObjChartStacked.Legend;
                            WithLegend.Position = XlLegendPosition.xlLegendPositionTop;
                            WithLegend.Border.LineStyle = XlLineStyle.xlLineStyleNone;
                            //WithLegend.Border.Color = OutputUtil.LINE_COLOR;
                            //WithLegend.Border.Weight = XlBorderWeight.xlHairline;

                            PlotArea WithPlotAreaSt = WithchtObjChartStacked.PlotArea;
                            WithPlotAreaSt.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;

                            Axis = WithchtObjChartStacked.Axes(XlAxisType.xlCategory);
                            Axis.ReversePlotOrder = true;
                            Axis.MajorTickMark = XlTickMark.xlTickMarkNone;
                            Axis.TickLabelPosition = XlTickLabelPosition.xlTickLabelPositionNone;
                            Axis.Delete();

                            Axis = WithchtObjChartStacked.Axes(XlAxisType.xlValue);
                            if (!isN)
                            {
                                Axis.MinimumScale = 0;
                                Axis.MaximumScale = 1;
                                Axis.MajorUnit = 0.2;
                            }
                            else
                            {
                                Axis.MinimumScale = 0;
                            }
                            Axis.MajorTickMark = XlTickMark.xlTickMarkInside;
                            Axis.TickLabelPosition = XlTickLabelPosition.xlTickLabelPositionNone;
                            //Axis.TickLabels.NumberFormat = @"0""%""";
                            Border WithMajorGridlinesBorderSt = Axis.MajorGridlines.Border;
                            WithMajorGridlinesBorderSt.ColorIndex = 15;
                            WithMajorGridlinesBorderSt.Weight = XlBorderWeight.xlHairline;
                            WithMajorGridlinesBorderSt.LineStyle = XlLineStyle.xlDot;
                            Axis.MajorGridlines.Delete();


                            WithchtObjChartStacked.ChartGroups(1).GapWidth = 30;
                            sCol = WithchtObjChartStacked.SeriesCollection();
                            for (j = 1; j <= sCol.Count; j++)
                            {
                                Series WithsCol = sCol.Item(j);
                                WithsCol.Format.Line.ForeColor.RGB = OutputUtil.LINE_COLOR;
                                WithsCol.Format.Line.Weight = OutputUtil.LINE_WEIGHT;
                                // WithsCol.Interior.ColorIndex = tmpTable.Chart.SeriesColorIndex((j - 1) % tmpTable.Chart.SeriesCount);
                                WithsCol.Interior.Color = isN ? 0xF2F2F2 : ColorPallet.colorIndex[tmpTable.Chart.SeriesColorIndex((j - 1) % tmpTable.Chart.SeriesCount)];
                                if (tmpTable.Chart.GradientStyle != 0)
                                {
                                    WithsCol.Fill.OneColorGradient((Microsoft.Office.Core.MsoGradientStyle)tmpTable.Chart.GradientStyle, tmpTable.Chart.GradientVariant, (float)0.9);
                                }
                                WithsCol.ApplyDataLabels();
                                if (isN)
                                {
                                    DataLabels dls = sCol.Item(j).DataLabels();
                                    dls.NumberFormat = NUMERIC_DATALABEL_NUMBER_FMT;
                                }
                                OutputUtil.SetHideZeroNumberFormat(sCol.Item(j));
                                WithsCol.DataLabels().Separator = " ";
                            }
                            OutputUtil.FitChartHeightAndPositionToRangeRight(chtObj, BarStackedGraphPlotArea.Columns.Item[0], xlApp);
                            OutputUtil.FitChartWidth(chtObj, BarStackedGraphPlotArea.Width, xlApp, tmpTable.SectorsCount);
                            WithLegend = WithchtObjChartStacked.Legend;
                            //if (chtObj.Top + OutputUtil.OVER_2007_CHART_AREA_TOP + WithLegend.Top + WithLegend.Height
                            //        > BarStackedGraphLegendArea.Top + BarStackedGraphLegendArea.Height)
                            //{

                            //    WithLegend.Left = BarStackedGraphLegendArea.Left - chtObj.Left - OutputUtil.OVER_2007_CHART_AREA_LEFT;
                            //    WithLegend.Width = BarStackedGraphLegendArea.Width;
                            //    WithLegend.Top = BarStackedGraphLegendArea.Top - chtObj.Top - OutputUtil.OVER_2007_CHART_AREA_TOP;

                            //    if (WithLegend.Height > BarStackedGraphLegendArea.Height)
                            //    {
                            //        WithLegend.Height = BarStackedGraphLegendArea.Height;
                            //    }
                            //}

                            if (FileFormat == XlFileFormat.xlExcel8)
                            {
                                PlotArea WithPlotArea2 = WithchtObjChartStacked.PlotArea;
                                WithPlotArea2.Left = WithPlotArea2.Width + 1 * 0.75;
                                WithPlotArea2.Width = WithPlotArea2.Width + 1 * 0.75;
                                WithPlotArea2.Height = WithPlotArea2.Width + 1 * 0.75;
                            }
                            //        break;
                            //}
                        }
                    }
                    else
                    {  //  ' Output.Orientation = TableOrientation_Portrait
                    }
                }
                //            ' 凡例 (凡例調整はOutput単位でやっておいてから、コピーするだけにした方がよい)
                if (!isN)
                {
                    if (Output.MarkingRanking) { crossCreator.RankMarking(WithStartCell.Cells, ref Ranking); }
                    if (Output.MarkingColoring) { crossCreator.Hatching(WithStartCell.Cells, ref HatchingColorIndex); }
                    //if( Output.MarkingAscending ){ crossCreator.AscendingMarking WithStartCell.Cells, ArrowEnd}
                    if (Output.MarkingSignificance) { crossCreator.SignificanceTestMarking(WithStartCell.Cells, ref SigTestMarking); }
                }
                if (!isN)
                {
                    MinBaseBox = null;
                    //Array.Clear(s, s.GetLowerBound(0), s.Length);
                    //Erase s;
                    s = Array.CreateInstance(typeof(Shape), 0);
                    cnt = 0;
                    if (Output.MarkingRanking || Output.MarkingColoring || Output.MarkingSignificance || Output.MarkingAscending)
                    {

                        if (Output.MinSamplesCountOnMarking >= 0)
                        {
                            _log.Info("WithStartCell.Worksheet : " + WithStartCell.Worksheet.Name);
                            MinBaseBox = WithStartCell.Worksheet.TextBoxes("MinBase");
                            string wbString = "";
                            if (Output.WBOn)
                            {
                                wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_AFTER_WB;
                            }
                            if (Output.WBOn && Output.ShowPreWBTotal && Output.PreWbBase)
                            {
                                wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_BEFORE_WB;
                            }
                            string msg = string.Format(LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_PROMPT,
                                wbString, Output.MinSamplesCountOnMarking.ToString());
                            MinBaseBox.Text = msg;
                        }
                        else
                        {
                            Shapes WithShapes = WithStartCell.Worksheet.Shapes;
                            WithShapes.Item("MinBase").Delete();
                        }
                    }
                    if (Output.MarkingColoring)
                    {
                        CrossCreator.ArrayPreserve(ref s, typeof(Shape), cnt);
                        Shapes WithShapes = WithStartCell.Worksheet.Shapes;
                        s.SetValue(WithShapes.Item("RateDifferenceLegend"), cnt);
                        ((Shape)s.GetValue(cnt)).TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_CAPTION;
                        l = WithShapes.Item("Level2HighLabel");
                        p = WithShapes.Item("Level2HighPalette");
                        lt = l.Top;
                        pt = p.Top;
                        o = WithShapes.Item("Level1HighLabel").Top - lt;
                        if (Output.MarkingColoringLevel2High)
                        {
                            l.TextEffect.Text = string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
                                                  , (" " + Output.Level2Percent).ToString().Substring(Output.Level2Percent.ToString().Length - 1));
                            clrIdx = Output.Level2HighColorIndex;
                            if (clrIdx < 0)
                            {
                                clrIdx = Convert.ToInt32(TemplateBook.Colors[2]);// ' 白
                            }

                            p.Fill.ForeColor.RGB = clrIdx;
                            lt = lt + o;
                            pt = pt + o;
                        }
                        else
                        {
                            l.Delete();
                            p.Delete();
                        }
                        l = WithShapes.Item("Level1HighLabel");
                        p = WithShapes.Item("Level1HighPalette");
                        if (Output.MarkingColoringLevel1High)
                        {
                            l.TextEffect.Text = string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
                                                  , (" " + Output.Level1Percent).ToString().Substring(Output.Level1Percent.ToString().Length - 1));


                            clrIdx = Output.Level1HighColorIndex;
                            if (clrIdx < 0)
                            {
                                clrIdx = Convert.ToInt32(TemplateBook.Colors[2]);// ' 白
                            }
                            p.Fill.ForeColor.RGB = clrIdx;
                            l.Top = (float)lt;
                            p.Top = (float)pt;
                            lt = lt + o;
                            pt = pt + o;
                        }
                        else
                        {
                            l.Delete();
                            p.Delete();
                        }
                        l = WithShapes.Item("Level1LowLabel");
                        p = WithShapes.Item("Level1LowPalette");
                        if (Output.MarkingColoringLevel1Low)
                        {
                            l.TextEffect.Text = string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                                                    , (" " + Output.Level1Percent).ToString().Substring(Output.Level1Percent.ToString().Length - 1));

                            clrIdx = Output.Level1LowColorIndex;
                            if (clrIdx < 0)
                            {
                                clrIdx = Convert.ToInt32(TemplateBook.Colors[2]);// ' 白
                            }
                            p.Fill.ForeColor.RGB = clrIdx;
                            l.Top = (float)lt;
                            p.Top = (float)pt;
                            lt = lt + o; ;
                            pt = pt + o;
                        }
                        else
                        {
                            l.Delete();
                            p.Delete();
                        }
                        l = WithShapes.Item("Level2LowLabel");
                        p = WithShapes.Item("Level2LowPalette");
                        if (Output.MarkingColoringLevel2Low)
                        {
                            l.TextEffect.Text = string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
, (" " + Output.Level2Percent).ToString().Substring(Output.Level2Percent.ToString().Length - 1));
                            clrIdx = Output.Level2LowColorIndex;
                            if (clrIdx < 0)
                            {
                                clrIdx = Convert.ToInt32(TemplateBook.Colors[2]);// ' 白
                            }
                            p.Fill.ForeColor.RGB = clrIdx;
                            l.Top = (float)lt;
                            p.Top = (float)pt;
                        }
                        else
                        {
                            l.Delete();
                            p.Delete();
                        }
                        cnt = cnt + 1;
                    }
                    else
                    {
                        Shapes WithShapes = WithStartCell.Worksheet.Shapes;
                        WithShapes.Item("RateDifferenceLegend").Delete();
                    }
                    if (Output.MarkingRanking)
                    {
                        CrossCreator.ArrayPreserve(ref s, typeof(Shape), cnt);
                        Shapes WithShapes = WithStartCell.Worksheet.Shapes;
                        s.SetValue(WithShapes.Item("RankingMarkingLegend"), cnt);
                        ((Shape)s.GetValue(cnt)).TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_CAPTION;
                        WithShapes.Item("Rank1Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_1ST_CAPTION;
                        WithShapes.Item("Rank2Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_2ND_CAPTION;
                        WithShapes.Item("Rank3Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_3RD_CAPTION;
                        cnt = cnt + 1;
                    }
                    else
                    {
                        Shapes WithShapes = WithStartCell.Worksheet.Shapes;
                        WithShapes.Item("RankingMarkingLegend").Delete();
                    }
                    if (Output.MarkingSignificance)
                    {
                        CrossCreator.ArrayPreserve(ref s, typeof(Shape), cnt);
                        s.SetValue(WithStartCell.Worksheet.Shapes.Item("SignificanceTestLegend"), cnt);
                        cnt = cnt + 1;
                    }
                    else
                    {
                        Shapes WithShapes = WithStartCell.Worksheet.Shapes;
                        WithShapes.Item("SignificanceTestLegend").Delete();
                    }
                    tmpTop = WithStartCell.Offset[1].Top;
                    tmpLeft = WithStartCell.Left;
                    if (cnt > 0)
                    {
                        for (j = 0; j <= cnt - 1; j++)
                        {
                            tmpTop = tmpTop - ((Shape)s.GetValue(j)).Height - d;
                            ((Shape)s.GetValue(j)).Top = (float)tmpTop;
                            ((Shape)s.GetValue(j)).Left = (float)tmpLeft;
                        }
                    }
                    if (MinBaseBox != null)
                    {
                        tmpTop = tmpTop - MinBaseBox.Height - d;
                        MinBaseBox.Top = tmpTop;
                        MinBaseBox.Left = tmpLeft;
                    }

                }
                //        if( Output.SignificanceTest ){
                //            '
                //        }
                ReportBook(tmpTable.KeyItem).Sources().Add(tmpTable.Question.Name, tmpTable.Question.Description,
    TableRange, tmpTable.KeyItem, tmpTable.Comment, chtObj, tmpTable.Question.QuestionType, null,
    Output.LocalizedFilteringExpression);
            }
            //    if( res <> RaisedError ){ res = Successful}
            //    CreateCross = res
            //    if( res = RaisedError ){ PutErrorsInformation Errors}
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
            //        Resume }
            //    }else{
            //        With Err()
            //            .Raise .Number, .Source, .Description, .HelpFile, .HelpContext
            //        End With
            //    }        

        }

        public Worksheet AddSheet(Table Table, Worksheet TemplateSheet)
        {
            ReportBook rb = null;
            Workbook wb = null;
            Macromill.QCWeb.ReportRequest.KeyItemInformation tmpKeyItem = null;
            GTTable tmpGTTable = null;
            CrossTable tmpCRTable = null;
            FAListTable tmpFATable = null;
            string QID = null;
            string QDesc = null;
            string QNumber = null;
            Worksheet res = null;
            string ReportTitle = null;
            string header = null;
            string tableHeading = null;
            string narrowingCondtion = null;
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
                //Case TypeOf Table Is QCWebReportBatch.FAListTable //'FA
                //    Set tmpFATable = Table
                //    With tmpFATable
                //        QID = .TopItemName
                //        QDesc = .TableValue(.GetTableValueRowIndexMinimum, .GetTableValueColumnIndexMinimum, true)
                //    End With
                //    Set tmpKeyItem = tmpFATable.KeyItem
            }
            rb = ReportBook(tmpKeyItem);
            wb = rb.LastBook();
            if (wb.Worksheets.Count > MAX_SHEETS_COUNT)
            {
                wb = wbs.Add(ReportBookPath());
                //TemplateBook.Worksheets.Item["INDEX"].Copy();
                //wb = xlApp.ActiveWorkbook;
                InitializeContentsSheet(wb.Worksheets.Item[1], tmpKeyItem);
                rb.Add(wb);
            }
            Sheets WithWorkShts = wb.Worksheets;
            TemplateSheet.Copy(After: WithWorkShts.Item[WithWorkShts.Count]);
            res = WithWorkShts.Item[WithWorkShts.Count];
            ReportTitle = Table.ParentRequest.Title;
            header = OutputUtil.GetAdjustedHeader(ReportTitle);
            res.Unprotect(SheetPSWD);
            res.PageSetup.CenterHeader = header;

            switch (tmpCRTable.Question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N))
            {
                case QuestionType.SA:
                case QuestionType.MA:
                    res.Tab.Color = ReportCreator.TAB_COLOR_BLUE;
                    break;
                case QuestionType.N:
                    res.Tab.Color = ReportCreator.TAB_COLOR_RED;
                    break;
            }

            if (QID != null)
            {
                if (NPOICrossCreator.checkSimpleAggr(tmpCRTable)
                    && !string.IsNullOrEmpty(QNumber)
                    && !string.IsNullOrEmpty(tableHeading)
                    && tmpCRTable.AxesGroups.Count > 1)
                {
                    OutputUtil.AddSheetCustomProperty(res, "QuestionID", QID);
                    OutputUtil.AddSheetCustomProperty(res, "QuestionDescription", tableHeading);
                    OutputUtil.AddSheetCustomProperty(res, "QNumber", QNumber);
                }
                else
                {
                    OutputUtil.AddSheetCustomProperty(res, "QuestionID", QID);
                    OutputUtil.AddSheetCustomProperty(res, "QuestionDescription", QDesc);
                    OutputUtil.AddSheetCustomProperty(res, "QNumber", QID);
                }
                if (tmpGTTable != null)
                {
                    OutputUtil.PutValue(res.Range["QuestionID"], ref QID);
                    if (!NPOICrossCreator.checkSimpleAggr(tmpCRTable) || string.IsNullOrEmpty(tableHeading) || tmpCRTable.AxesGroups.Count == 1)
                    {
                        OutputUtil.PutValueLong(res.Range["QuestionDescription"], ref QDesc);
                    }
                    OutputUtil.PutValueLong(res.Range["TableHeading"], ref tableHeading);
                    OutputUtil.PutValueLong(res.Range["Narrow"], ref narrowingCondtion); // narro
                    if (tmpKeyItem != null)
                    {
                        string clas = LocalResource.REPORT_CLASSIFICATION_ITEM_KEYWORD
                              + " = " + tmpKeyItem.Name + ":" + tmpKeyItem.Description
                              + "\n"
                              + LocalResource.REPORT_SECTOR_KEYWORD
                              + " = "
                              + tmpKeyItem.SectorNumber + ":" + tmpKeyItem.SectorDescription;
                        OutputUtil.PutValueLong(res.Range["classification"], ref clas); // classi
                    }
                }
            }
            return res;
        }






        public ReportBook ReportBook(Macromill.QCWeb.ReportRequest.KeyItemInformation KeyItemInfo)
        {
            string tmpKey;
            Workbook wb;
            ReportBook rb;
            int n = 0;
            string fmt;
            if (KeyItemInfo == null)
            {
                if (ThisReportBooks.Count == 0)
                {
                    wb = wbs.Add(ReportBookPath());
                    //TemplateBook.Worksheets.Item["INDEX"].Copy(wb.Worksheets[1]);
                    InitializeContentsSheet(wb.Worksheets.Item[1], KeyItemInfo);
                    rb = new ReportBook();
                    rb.Add(wb);
                    ThisReportBooks.Add("0", rb);
                }
                rb = (ReportBook)ThisReportBooks["0"];
            }
            else
            {
                //n = ThisReportset.KeyItemMaxSectorNumber(KeyItemInfo.Name);
                //if (n > 0) { n = (int)(Math.Log(n) / Math.Log(10)) + 1; }
                fmt = new string('0', 4);
                tmpKey = KeyItemInfo.Name + "_" + KeyItemInfo.SectorNumber.ToString(fmt);
                if (ThisReportBooks.ContainsKey(tmpKey))
                {
                    rb = (ReportBook)ThisReportBooks[tmpKey];
                }
                else
                {
                    wb = wbs.Add(ReportBookPath());
                    //TemplateBook.Worksheets.Item["INDEX"].Copy();
                    //wb = xlApp.ActiveWorkbook;
                    InitializeContentsSheet(wb.Worksheets.Item[1], KeyItemInfo);
                    rb = new ReportBook();
                    rb.Add(wb);
                    ThisReportBooks.Add(tmpKey, rb);
                }
            }
            return rb;
        }






        public void InitializeContentsSheet(Worksheet ContentsSheet, Macromill.QCWeb.ReportRequest.KeyItemInformation KeyItemInfo)
        {
            Array v; // string
            Array caps; // string
            ContentsSheet.Name = LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME;
            ContentsSheet.Unprotect(SheetPSWD);
            ContentsSheet.Rectangles("TitleBox").Text = ThisReportset.ParentRequest.Title;
            if (KeyItemInfo == null)
            {
                //Name WithContentsSheetName = ContentsSheet.Names.Item("KeyItemInformation");
                //WithContentsSheetName.RefersToRange.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                //WithContentsSheetName.Delete();
            }
            else
            {
                //v = Array.CreateInstance(typeof(string), new int[] { 2, 2 }, new int[] { 0, 0 });
                //v.SetValue(LocalResource.ReportClassificationItemKeywordIndex), 0, 0);
                //v.SetValue(LocalResource.ReportSectorKeywordIndex), 1, 0);
                //v.SetValue(KeyItemInfo.Name + ":" + KeyItemInfo.Description, 0, 1);
                //v.SetValue(KeyItemInfo.SectorNumber + ":" + KeyItemInfo.SectorDescription, 1, 1);
                //Range WithContentsSheet = ContentsSheet.Range["KeyItemInformation"].Range["B1:C2"];
                //OutputUtil.PutValue(WithContentsSheet.Cells, ref v);
                //OutputUtil.AutoFitEx(WithContentsSheet.Rows, xlApp);
            }
            caps = Array.CreateInstance(typeof(string), new int[] { 3 }, new int[] { 1 });
            caps.SetValue(LocalResource.REPORT_LAYOUT_QUESTION_NUMBER_COLUMN_CAPTION, 1);
            caps.SetValue(LocalResource.REPORT_LAYOUT_QC3_DESCRIPTION_2COLUMN_CAPTION, 2);
            caps.SetValue(LocalResource.REPORT_PAGE_KEYWORD, 3);
            OutputUtil.PutValue(ContentsSheet.Range["ContentsCaption"], ref caps);
            Shapes WithContentsSheetShapes = ContentsSheet.Shapes;
            WithContentsSheetShapes.Item("RankingMarkingLegend").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_CAPTION;
            WithContentsSheetShapes.Item("Rank1Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_1ST_CAPTION;
            WithContentsSheetShapes.Item("Rank2Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_2ND_CAPTION;
            WithContentsSheetShapes.Item("Rank3Label").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_3RD_CAPTION;
            WithContentsSheetShapes.Item("RateDifferenceLegend").TextEffect.Text = LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_CAPTION;
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


        private void Finalise(List<string> outputFileSig = null)
        {
            Array keys = Array.CreateInstance(typeof(string), ThisReportBooks.Count); // Variant
            Array itms = Array.CreateInstance(typeof(ReportBook), ThisReportBooks.Count); //Variant
            string tmpSuffix;
            int i = 0;
            string tmpPrefix = null;
            ReportBook rb = null;
            // var x  ;
            if ((ThisReportset.FileType & FileType.Excel) == FileType.Excel)
            {
                ThisReportBooks.Keys.CopyTo(keys, 0);
                if ((string)keys.GetValue(0) == "0")
                {
                    rb = (ReportBook)ThisReportBooks["0"];
                    selectIndexSheet(rb, xlApp);
                }
                else
                {
                    ThisReportBooks.Values.CopyTo(itms, 0);
                    for (i = 0; i < keys.Length; i++)
                    {
                        rb = (ReportBook)itms.GetValue(i);
                        selectIndexSheet(rb, xlApp);
                    }
                }
            }


            tmpPrefix = ThisReportset.DivName + ThisReportset.FileNamePrefix;
            if ((ThisReportset.FileType & FileType.Excel) == FileType.Excel)
            {
                ThisReportBooks.Keys.CopyTo(keys, 0);
                if ((string)keys.GetValue(0) == "0")
                {
                    rb = (ReportBook)ThisReportBooks["0"];
                    SaveBook(rb, tmpPrefix, outputFileSig: outputFileSig);
                }
                else
                {
                    ThisReportBooks.Values.CopyTo(itms, 0);
                    for (i = 0; i < keys.Length; i++)
                    {
                        tmpSuffix = "_" + keys.GetValue(i);
                        rb = (ReportBook)itms.GetValue(i);
                        SaveBook(rb, tmpPrefix, tmpSuffix);
                    }
                }
            }
            else
            {
                foreach (var x in ThisReportBooks.Values)
                {
                    rb = (ReportBook)x;
                    rb.CloseAllBooks();
                }
            }
            ThisReportBooks = null;
            WorkingSheet = null;
            TemplateBook.Close(false);
            TemplateBook = null;
            //TransposeTemplateBook.Close(false);
            //TransposeTemplateBook = null;
            FinalizeOutput();
            //if( PartsPresentation != null ){
            //    PartsPresentation.Saved = msoTrue
            //    PartsPresentation.Close
            //    Set PartsPresentation = null
            //}
            //xlApp.Visible = true;
        }


        private void selectIndexSheet(ReportBook ReportBook,
            Application xlApp)
        {
            Workbook wb;
            Worksheet sh;
            int i;
            int j;
            for (i = 0; i < ReportBook.Books().Count(); i++)
            {
                wb = ReportBook.Item(i);
                PutContents(wb);
                Sheets WithBookWorksheets = wb.Worksheets;
                for (j = WithBookWorksheets.Count; j >= 1; j--)
                {
                    sh = WithBookWorksheets.Item[j];
                    if (sh.Visible == XlSheetVisibility.xlSheetVisible)
                    {
                        xlApp.Goto(sh.Range["A1"]);
                    }
                }
            }
        }


        private void FinalizeOutput()
        {

            if (FormatBook == null) { return; }
            FormatBook.Close(false);
            FormatBook = null;
            //TransposeFormatBook.Close(false);
            // TransposeFormatBook = null;
            FormatSheet = null;
            DoubleFormatSheet = null;
            TransposeFormatSheet = null;
            TransposeDoubleFormatSheet = null;
            GTFormatSheet = null;
            GTTransposeFormatSheet = null;

        }


        private void SaveBook(ReportBook ReportBook
              , string Prefix, string Suffix = "", List<string> outputFileSig = null)
        {
            Workbook wb;
            string ext;
            string n;
            string p;
            Worksheet sh;
            int i;
            int j = 0;
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
            List<string> otptPath = new List<string>();
            for (i = 0; i < ReportBook.Books().Count(); i++)
            {
                wb = ReportBook.Item(i);
                ext = FileFormat == XlFileFormat.xlExcel8 ? "xls" : "xlsx";
                j = 0;
                if (OutputDirectoryPath != null)
                {
                    do
                    {
                        n = Prefix + (j > 0 ? "_" + j : "") + Suffix + (ReportBook.Books().Count() > 1 ? "_" + (i + 1) : "") + "." + ext;
                        j = j + 1;
                        p = OutputUtil.BuildPath(OutputPath, n, xlApp.PathSeparator);
                    } while (File.Exists(p));
                    wb.CheckCompatibility = false;
                    wb.BuiltinDocumentProperties["Author"] = "MACROMILL, INC.";
                    wb.SaveAs(p, FileFormat, AccessMode: XlSaveAsAccessMode.xlNoChange);
                    outputFiles.Add(p);
                }
                else
                {
                    do
                    {
                        n = Prefix + (j > 0 ? "_" + j : "") + Suffix + "." + ext;
                        j = j + 1;
                        p = OutputUtil.BuildPath(OutputPath, n, xlApp.PathSeparator);
                    } while (File.Exists(p));
                    _log.Info(p);

                    wb.CheckCompatibility = false;
                    wb.SaveAs(p, FileFormat, AccessMode: XlSaveAsAccessMode.xlNoChange);
                    wb.Close(false);
                    //Workbook wbn = wbs.Add(p);
                    //wbn.BuiltinDocumentProperties["Author"] = "MACROMILL, INC.";
                    //wbn.Unprotect(BookPSWD);
                    otptPath.Add(p);
                }
            }
            if (OutputDirectoryPath != null)
            {
                ReportBook.CloseAllBooks();
            }
            else
            {
                string oldPth = null;
                if (outputFileSig != null)
                {
                    foreach (string pth in outputFileSig)
                    {
                        if (oldPth != null)
                        {
                            File.Copy(pth, oldPth, true);
                        }
                        else
                        {
                            oldPth = pth;
                        }
                        Workbook wbn = wbs.Add(oldPth);
                        wbn.BuiltinDocumentProperties["Author"] = "MACROMILL, INC.";
                        wbn.Unprotect(BookPSWD);
                    }
                }
                oldPth = null;
                foreach (string pth in otptPath)
                {
                    if (oldPth != null)
                    {
                        File.Copy(pth, oldPth, true);
                    }
                    else
                    {
                        oldPth = pth;
                    }
                    Workbook wbn = wbs.Add(oldPth);
                    wbn.BuiltinDocumentProperties["Author"] = "MACROMILL, INC.";
                    wbn.Unprotect(BookPSWD);
                }
            }
        }


        private void PutContents(Workbook Book)
        {
            xlApp.ScreenUpdating = true;
            int MAX_SHEET_NAME_LENGTH = 31;
            Worksheet ContentsSheet;
            Array ContentsValue; // string()  
            int i = 0;
            int j = 0;
            Worksheet sh;
            string QID = null;
            string QDesc = null;
            string QNumber = null;
            string n = null;
            OrderedDictionary nDic; // ' Scripting.Dictionary
            string k;
            OrderedDictionary kDic;  // ' Scripting.Dictionary
            Array ns = Array.CreateInstance(typeof(string), 0); ;//()  //Variant
            Array shs = Array.CreateInstance(typeof(Worksheet), 0); ;//() // Variant
            Sheets WithWorksheets = Book.Worksheets;
            ContentsSheet = WithWorksheets.Item[LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME];
            if (ContentsSheet.Index > 1) { ContentsSheet.Move(WithWorksheets.Item[1]); }
            nDic = new OrderedDictionary();
            kDic = new OrderedDictionary();
            for (i = 2; i <= WithWorksheets.Count; i++)
            {
                sh = WithWorksheets.Item[i];
                QID = string.Empty;
                foreach (CustomProperty p in sh.CustomProperties)
                {
                    if (p.Name == "QuestionID")
                    {
                        QID = GetSheetCustomProperty(p);
                        break;
                    }
                }
                if (QID != string.Empty)
                {
                    n = i - 1 + "(" + QID + ")";
                    try
                    {
                        k = Vb.Strings.StrConv(n, Vb.VbStrConv.Wide | Vb.VbStrConv.Uppercase);
                    }
                    catch (Exception ex)
                    {
                        k = Vb.Strings.StrConv(n, Vb.VbStrConv.Uppercase);
                    }
                    //j = 1;
                    //while (kDic.ContainsKey(k))
                    //{
                    //    j = j + 1;
                    //    n = QID + "~" + j;
                    //    try
                    //    {
                    //        k = Vb.Strings.StrConv(n, Vb.VbStrConv.Wide | Vb.VbStrConv.Uppercase);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        k = Vb.Strings.StrConv(n, Vb.VbStrConv.Uppercase);
                    //    }
                    //}
                    if (n.Length > MAX_SHEET_NAME_LENGTH)
                    {
                        j = 0;
                        do
                        {
                            j = j + 1;
                            n = "@" + j;
                            try
                            {
                                k = Vb.Strings.StrConv(n, Vb.VbStrConv.Wide | Vb.VbStrConv.Uppercase);
                            }
                            catch (Exception ex)
                            {
                                k = Vb.Strings.StrConv(n, Vb.VbStrConv.Uppercase);
                            }
                        } while (kDic.Contains(k));
                    }
                    nDic.Add(n, sh);
                    kDic.Add(k, true);
                }
            }
            Worksheet WithContentsSheet = ContentsSheet;
            if (nDic.Count > 3)
            {
                Range WithRange2 = WithContentsSheet.Range["Contents"].Rows.Item[2];
                WithRange2.Copy();
                WithRange2.Resize[nDic.Count - 3].Insert(XlInsertShiftDirection.xlShiftDown);
                xlApp.CutCopyMode = (XlCutCopyMode)1;
            }
            else if (nDic.Count < 3)
            {
                WithContentsSheet.Range["Contents"].Rows.Item[2].Resize[3 - nDic.Count].Delete(XlDeleteShiftDirection.xlShiftUp);
                if (nDic.Count == 1) { WithContentsSheet.Range["Contents"].Borders.Item[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin; }
            }
            ContentsValue = Array.CreateInstance(typeof(string),
                                   new int[] { nDic.Count, 2 },
                                   new int[] { 1, 1 });

            ns = Array.CreateInstance(typeof(string), nDic.Count); ;//()  //Variant
            shs = Array.CreateInstance(typeof(Worksheet), nDic.Count); ;//() // Variant
            nDic.Keys.CopyTo(ns, 0);
            nDic.Values.CopyTo(shs, 0);
            j = 0;

            Range WithRange = WithContentsSheet.Range["Contents"];
            for (i = 0; i < ns.Length; i++)
            {
                j = j + 1;
                n = (string)ns.GetValue(i);
                sh = (Worksheet)shs.GetValue(i);
                foreach (CustomProperty p in sh.CustomProperties)
                {
                    switch (p.Name)
                    {
                        case "QuestionID":
                            QID = GetSheetCustomProperty(p);
                            break;
                        case "QuestionDescription":
                            QDesc = GetSheetCustomProperty(p);
                            break;
                        case "QNumber":
                            QNumber = GetSheetCustomProperty(p);
                            break;
                    }
                    p.Delete();
                }
                ContentsValue.SetValue(QNumber, j, 1);
                ContentsValue.SetValue(QDesc, j, 2);
                sh.Name = n;
                WithRange.Item[j, 3].Hyperlinks.Add(WithRange.Item[j, 3], "", "'" + sh.Name + "'!$A$1", TextToDisplay: "TABLE[" + QID + "]");
            }
            OutputUtil.PutValue(WithRange.Resize[ColumnSize: 2], ref ContentsValue);
            Shapes WithShapes = WithContentsSheet.Shapes;
            WithShapes.Item("SignificanceTestLegend").Delete();
            WithShapes.Item("RateDifferenceLegend").Delete();
            WithShapes.Item("RankingMarkingLegend").Delete();
            WithShapes.Item("MinBase").Delete();
            Name WithNames = WithContentsSheet.Names.Item("LegendTemplateSpace");
            WithNames.RefersToRange.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
            WithNames.Delete();
        }

        public string GetSheetCustomProperty(CustomProperty p)
        {
            string v = "";
            try
            {
                v = p.Value;
            }
            catch { }
            if (v == null) { v = ""; }
            return v;
        }
    }
}
