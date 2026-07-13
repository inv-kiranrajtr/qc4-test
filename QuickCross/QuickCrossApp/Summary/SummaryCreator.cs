using log4net;
using Macromill.QCWeb.Batch.Report;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using Qc4Launcher.Util;
using Qc4Launcher.Logic;
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
using System.ComponentModel;

namespace Qc4Launcher.Summary
{
    public class SummaryCreator
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public OutputCross CurrentOutput = null;
        public bool CurrentOutputShowPreWBTotal = false;
        public Worksheet WorkingSheet;
        public Workbook WorkingBook;
        public Application xlApp;
        public Workbooks wbs;
        private Sheets wss;
        public string ThisLocationCode;
        public ExecuteStaticMethod ExecuteStaticMethod = new ExecuteStaticMethod();
        private static double COL_MIN_WIDTH = 42;
        private static double COL_MIN_COLWIDTH = 8.5;
        private static int ROW_COUNT_BW_TABLES = 3;
        private static string TOTAL_TABLE_NAME = "ベース";

        public static string TEMPLATE_NAME = "Cross.xlt";
        public static string TRANSPOSE_TEMPLATE_NAME = "CrossPortrait.xlt";
        public static string INDIVIDUAL_TEMPLATE_NAME = "CrossNP.xlt";
        public static string INDIVIDUAL_TEMPLATE_NAME_NP = "Cross_np.xltx";
        public static string INDIVIDUAL_TEMPLATE_NAME_N = "Summary_n.xltx";
        public static string INDIVIDUAL_TEMPLATE_NAME_P = "Summary_p.xltx";
        public static string INDIVIDUAL_TEMPLATE_NAME_T = "Summary_ps.xltx";
        public static string TRANSPOSE_INDIVIDUAL_TEMPLATE_NAME = "CrossNPPortrait.xlt";
        public static string REPORT_TEMPLATE_NAME = "Report.xlt";
        public static string TRANSPOSE_REPORT_TEMPLATE_NAME = "ReportPortrait.xlt";

        public static string FORMAT_TEMPLATE_NAME = "CrossFormat.xlt";
        public static string TRANSPOSE_FORMAT_TEMPLATE_NAME = "CrossPortraitFormat.xlt";
        public static string INDIVIDUAL_FORMAT_TEMPLATE_NAME = "CrossNPFormat.xlt";
        public static string TRANSPOSE_INDIVIDUAL_FORMAT_TEMPLATE_NAME = "CrossNPPortraitFormat.xlt";
        public static string REPORT_FORMAT_TEMPLATE_NAME = "ReportFormat.xlt";
        public static string TRANSPOSE_REPORT_FORMAT_TEMPLATE_NAME = "ReportPortraitFormat.xlt";
        public static string CELL_RANGE_SINGLE_3 = "C3:R3";
        public static string CELL_RANGE_SINGLE_4 = "D4:R4";
        public string BookPSWD;
        public string SheetPSWD;
        public SummaryTabulation QC;
        public double progressAvailable;
        public double currentProgress;
        private string OutputDirectoryPath;
        public string TemplateDirectoryPath;
        public bool onlySigPage;
        private bool checkCross = false;
        public static int BORDER_COLOR = 0XA9A9A9;
        public static int TOATL_BG_COLOR = 0XEAEAEA;
        public static int TOATL_FONT_COLOR = 0X000000;
        public static int FIRST_TABLE_BG_COLOR = 0XF2F2F2;
        private Dictionary<string, double> colWidthMap;
        private Dictionary<string, double> colWidthMapSig;
        public BackgroundWorker bgWorker;
        public List<string> outputFiles;

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

        public void CreateCross(Output Output, string bookPSWD, string sheetPSWD, string outputDirectoryPath, 
            string templateDirectoryPath, Application xlAppG, BackgroundWorker bgWorker, bool onlySigPageP = false, bool checkCrossP = false,
            SummaryTabulation QC = null, double progressAvailable = 0, double currentProgress = 0, List<string> outputFiles = null)
        {

            XlFileFormat xlFmt = XlFileFormat.xlOpenXMLWorkbook;
            Reportset reportset = (Reportset)Output.ParentReportset;
            CurrentOutput = (OutputCross)Output;
            //CurrentOutputShowPreWBTotal = CurrentOutput.ShowPreWBTotal;
            BookPSWD = bookPSWD;
            SheetPSWD = sheetPSWD;
            OutputDirectoryPath = outputDirectoryPath;
            TemplateDirectoryPath = templateDirectoryPath;
            this.progressAvailable = progressAvailable;
            this.currentProgress = currentProgress;
            this.QC = QC;
            Workbook FormatBook = null;
            Workbook baseBook = null;
            wbs = null;
            wss = null;
            onlySigPage = onlySigPageP;
            xlApp = xlAppG;
            this.bgWorker = bgWorker;
            checkCross = checkCrossP;
            colWidthMap = new Dictionary<string, double>();
            colWidthMapSig = new Dictionary<string, double>();
            if (outputFiles == null) outputFiles = new List<string>();
            this.outputFiles = outputFiles;
            try
            {
                //if (xlAppChart == null)
                //{
                //    xlApp = new Application();
                //xlApp.ScreenUpdating = true;
                //xlApp.Visible = true;
                //    //xlApp.ScreenUpdating = true;
                //}
                //else
                //{
                //    xlApp = xlAppChart;
                //    xlApp.ScreenUpdating = false;
                //    // xlApp.Visible = true;
                //    // xlApp.ScreenUpdating = true;
                //}
                wbs = xlApp.Workbooks;
                baseBook = wbs.Add(OutputUtil.BaseTemplatePath(TemplateDirectoryPath, xlApp.PathSeparator));
                _log.Info("Excel base book added");
                //if (xlAppChart == null)
                //{
                xlApp.Calculation = XlCalculation.xlCalculationManual;
                //    xlApp.EnableEvents = false;
                //    xlApp.DisplayStatusBar = false;
                //    xlApp.PrintCommunication = false;
                //    xlApp.DisplayAlerts = false;
                //}
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
                    string fmt = new string('0', 3);
                    filenameSuffix = "_" + KeyItemName + "_" + KeyItemInfo.SectorNumber.ToString(fmt);
                }
                FormatBook = wbs.Add(FormatPath);
                _log.Info("Excel format book added");
                FormatBook.Unprotect(BookPSWD);
                //if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi && !onlySigPage)
                //{
                //    CreateStandardCross(TempPath, FormatBook, xlFmt, filenameSuffix);
                //}
                //else
                //{
                //    CreateIndividualCross(FormatBook, xlFmt, filenameSuffix);
                //}

                CreateIndividualCross(FormatBook, xlFmt, filenameSuffix);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.StackTrace);
                throw ex;
            }
            finally
            {
                try
                {
                    FormatBook.Close(false);
                }
                catch (Exception)
                {
                }
                try
                {
                    baseBook.Close(false);
                }
                catch (Exception)
                {
                }
                try
                {
                    FormatBook.Close(false);
                }
                catch (Exception)
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

        private void updateProgress(double currentProgress, string v)
        {
            if (null != QC)
            {
                QC.updateProgress(currentProgress, v);
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
                delH = WithWithContentsSheetItemRef.Height;
                WithWithContentsSheetItemRef.Delete(XlDeleteShiftDirection.xlShiftUp);
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
                //OutputUtil.AutoFitEx(WithContentsSheetRng.Rows, xlApp);
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
                        //wbString = LocalResourceReportMarkingLegendMinBaseAfterWB);
                        wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_BEFORE_WB;
                    }
                    //if (CurrentOutput.WBOn && CurrentOutput.ShowPreWBTotal && CurrentOutput.PreWbBase)
                    //{
                    //    wbString = LocalResourceReportMarkingLegendMinBaseBeforeWB);
                    //}
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
                    tmpS.DrawingObject.Text = System.Text.RegularExpressions.Regex.Unescape(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_CAPTION) + "\n" + String.Join("\n", (string[])v);
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

                bool showLevel1High = true;
                bool showLevel2Low = true;
                if (CurrentOutput.Level1Percent == CurrentOutput.Level2Percent)
                {
                    if (CurrentOutput.MarkingColoringLevel2High)
                    {
                        showLevel1High = false;
                    }
                    if (CurrentOutput.MarkingColoringLevel1Low)
                    {
                        showLevel2Low = false;
                    }
                }
                if (CurrentOutput.MarkingColoringLevel2High)
                {
                    tmpS.TextEffect.Text = string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
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
                if (CurrentOutput.MarkingColoringLevel1High && showLevel1High)
                {
                    tmpS.TextEffect.Text = string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
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
                    tmpS.TextEffect.Text = string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
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
                if (CurrentOutput.MarkingColoringLevel2Low && showLevel2Low)
                {
                    tmpS.TextEffect.Text = string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
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

            RowsCount = Table.GetTableValueRowIndexMaximum - Table.GetTableValueRowIndexMinimum + 1 - CutRowsCol.Count;
            ColumnsCount = Table.GetTableValueColumnIndexMaximum - Table.GetTableValueColumnIndexMinimum + 1 - CutColumnsCol.Count + AddColumnsCount;
            if (IsSigTest)
            {
                r = Table.GetTableValueRowIndexMinimum + DataOffsetRow; //' GTs
                DataRowsCount = d;
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
            //return Table.Question.HasWeight || Table.Question.HasCount;
            return false;
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
            bool IsPortrait;
            XlDeleteShiftDirection tmpDelShift;
            int i;
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
            if (!CurrentOutputShowPreWBTotal)
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
                               , RedrawBorder & !tmpNamesArray[i].EndsWith("_WT") && !tmpNamesArray[i].StartsWith("SA_"));
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
            if (DefHasNAAtItem)
            {
                //if (CheckZero)
                //{
                //    tmpIdx = LastColumnIndex - (ToInt(HasWeight) & 2) - (ToInt(DefHasIVAtItem) & 1);
                //    CutNA = Convert.ToDouble(Table.TableValue(1 + (ToInt(HasWeight) & 1), tmpIdx)) == 0;
                //    if (CutNA) { CutColumnsCol.Add(tmpIdx, tmpIdx); }
                //}
            }
            if (DefHasIVAtItem)
            {
                if (CheckZero)
                {
                    tmpIdx = LastColumnIndex - (ToInt(HasWeight) & 2);
                    CutIV = Convert.ToDouble(Table.TableValue(1 + (ToInt(HasWeight) & 1), tmpIdx)) == 0;
                    if (CutIV) { CutColumnsCol.Add(tmpIdx, tmpIdx); }
                }
            }
            if (IsReport)
            {
                MedIdx = Table.GetTableValueColumnIndexMinimum - 1;
                if ((Table.Question.QuestionType & QuestionType.N) == QuestionType.N)
                {
                    if (CutMedian)
                    {
                        MedIdx = LastColumnIndex - (ToInt(DefHasIVAtItem) & 1) - (ToInt(DefHasNAAtItem) & 1);
                        CutColumnsCol.Add(MedIdx, MedIdx);
                    }
                }
            }

            //r = (ToInt(HasWeight) & 1) + 1;
            r = 2; // include wt
            // if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
            PreSummaryRowIndex = r;
            CutRowsCol.Add(r, r);
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
                            // +1 added libs
                            string sss = Table.TableValue(r, PreWBColumnIndex + 1);
                            CutNA = false;
                            if ("-" == sss || Convert.ToDouble(sss) == 0)
                            {
                                CutNA = true;
                            }
                            //CutNA = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
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

        public string TemplatePath(ref XlFileFormat FileFormat, TableOrientation Orientation = TableOrientation.Landscape)
        {
            //if (FileFormat == null) { FileFormat = XlFileFormat.xlOpenXMLWorkbook; }
            string d;
            string n;

            if (Orientation != TableOrientation.Portrait) { Orientation = TableOrientation.Landscape; }
            FileFormat = (XlFileFormat)CurrentOutput.ParentRequest.ExcelFileFormat;
            if (FileFormat != XlFileFormat.xlExcel8) { FileFormat = XlFileFormat.xlOpenXMLWorkbook; }
            if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
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
                    catch (Exception)
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
                        }
                    }
                }
            }

            if (OutputDirectoryPath != null)
            {
                ext = FileFormat == XlFileFormat.xlExcel8 ? "xls" : "xlsx";
                do
                {
                    n = Prefix + (i > 0 ? "_" + i : "") + Suffix + "." + ext;
                    i = i + 1;
                    p = OutputUtil.BuildPath(OutputDirectoryPath, n, xlApp.PathSeparator);
                } while (File.Exists(p));

                Book.CheckCompatibility = false;
                //Book.Activate();
                Book.SaveAs(p, FileFormat, AccessMode: XlSaveAsAccessMode.xlNoChange);
                Book.Close(false);
                outputFiles.Add(p);
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
            Array NContentsValue = null; // string
            Array PerContentsValue = null;  //string
            Array SigTestContentsValue = null; // string
            Array NHyperLinkTargetCells = null;  //Range
            Array PerHyperLinkTargetCells = null; // Range
            Array SigTestHyperLinkTargetCells = null;  //Range
            Worksheet NContentsSheet = null;
            Worksheet PerContentsSheet = null;
            Worksheet SigTestContentsSheet = null;
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
            bool isN2;
            string tmpPrefix;
            int i;
            bool SigTestOn = false;
            Hashtable WholeRowCol = null;
            bool CheckOverRow;
            bool CheckOverColumn;

            Array NOverRowsQs = null;//string
            Array PerOverRowsQs = null;//string
            Array SigTestOverRowsQs = null;//string
            Array NOverColumnsQs = null; //string
            Array PerOverColumnsQs = null;//string
            Array SigTestOverColumnsQs = null;//string
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
                // '性能対策 end
                HasWeightBack = CurrentOutputShowPreWBTotal;
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
                
                //if (HasOutputNTable)
                if (true)
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
                //if (HasOutputPerTable || SigTestOn)
                if (true)
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
                            /*if (HasOutputPerTable)*/
                            if (true) { PerDoubleFormatSheet = FormatBookWorksheets.Item["P_Std"]; }
                            if (SigTestOn) { SigTestPerDoubleFormatSheet = FormatBookWorksheets.Item["P_Sig"]; }
                        }
                        else
                        {
                            /*if (HasOutputPerTable)*/
                            if (true)
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

                /************************************************
                     * Total table changes
                     * *********************************************/
                bool useSameSheet = false;
                Range rMainTable = null;
                Range rTotalTable = null;
                Worksheet shtTempN = null;
                Worksheet shtTempP = null;
                Worksheet shtTempS = null;
                string summaryName = null;
                string summaryType = null;
                int rowCount = 0;
                int rowCountSig = 0;

                int tbIndex = -1;
                double progressStep = progressAvailable / CurrentOutput.Tables.Count;
                for (i = 0; i < CurrentOutput.Tables.Count; i++)
                {
                    if (bgWorker.CancellationPending) return;
                    updateProgress(currentProgress, String.Format(LocalResource.PB_EXCEL_GEN_TABLE, (i + 1) , CurrentOutput.Tables.Count));
                    currentProgress += progressStep;
                    tmpTable = (CrossTable)CurrentOutput.Tables[i];
                    string[] values = tmpTable.Question.SummaryTableName.Split('&');
                    summaryName = values[2];
                    summaryType = values[1];
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
                            //Err().Raise vbObjectError + 100 &, RunningProcName, ThisWorkbook.GetffReportKeyword(ReportMessageIndex_ReportUnjustQuestionTypeMessageIndex)
                    }

                    if (HasWeight) { FormatRangeNamePrefix = FormatRangeNamePrefix + "_WT"; }
                    if (SigTestOn) { WholeRowCol = new Hashtable(); }
                    int medIdx = -1;
                    GetCutRowsAndColumns(tmpTable, HasWeightBack, HasWeight,
                        MaxAxesCountArray[i], ref CutRowsCol, ref CutColumnsCol, ref medIdx, false, false, WholeRowCol);


                    /************************************************
                     * Total table changes
                     * *********************************************/

                    //int totTabStartIndex = tmpTable.GetTableValueRowIndexMaximum + 10;
                    int totTabStartIndex = rowCount + ROW_COUNT_BW_TABLES;
                    string totTabStartCell = "A" + totTabStartIndex.ToString();
                    totTabStartIndex++;
                    string totTabValStartCell = "C" + totTabStartIndex.ToString();

                    //int totTabStartIndexSig = tmpTable.GetTableValueRowIndexMaximum + totTabStartIndex - 10;
                    int totTabStartIndexSig = rowCountSig + ROW_COUNT_BW_TABLES;
                    string totTabStartCellSig = "A" + totTabStartIndexSig.ToString();
                    totTabStartIndexSig++;
                    string totTabValStartCellSig = "C" + totTabStartIndexSig.ToString();

                    if (tmpTable.Question.SummaryTableName.EndsWith("_TT"))
                    {
                        useSameSheet = true;
                    }
                    else
                    {
                        useSameSheet = false;
                        tbIndex++;
                    }

                    if (HasOutputNTable)
                    {
                        /************************************************
                            * Total table changes
                            * *********************************************/
                        if (!useSameSheet)
                        {
                            CreateNewSheet(ref NBooks, TemplatePathIndividual(INDIVIDUAL_TEMPLATE_NAME_N), tmpTable, ref sht, ref NContentsSheet, ref NContentsValue, ref NHyperLinkTargetCells, ref NOrgSheets, false, TableType.N, tbIndex + 1);
                            rMainTable = sht.Range["A1"];
                            rTotalTable = sht.Range["C2"];
                            shtTempN = sht;
                        }
                        else
                        {
                            sht = shtTempN;
                            rMainTable = sht.Range[totTabStartCell];
                            rTotalTable = sht.Range[totTabValStartCell];
                        }

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
                                //Err().Raise vbObjectError + 400&, RunningProcName _
                                //          , ThisWorkbook.GetReportffKeyword(ReportMessageIndex_ReportColumnsCountOverDetailMessageIndex, tmpTable.Question.Name, tableTypeBuf)
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
                        if (!useSameSheet)
                        {
                            NContentsValue.SetValue(summaryName, tbIndex, 1);
                            NContentsValue.SetValue(tmpTable.Question.Description, tbIndex, 2);
                        }
                        if (CheckOverRow || CheckOverColumn)
                        {
                            xlApp.DisplayAlerts = false;
                            sht.Delete();
                            NContentsValue.SetValue("Error", tbIndex, 4);
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

                            string[] tmpNamesArray = "SA_MA".Split();
                            string colName = "_SectorColumns";
                            if (useSameSheet)
                            {
                                NumberFormat(FormatSheet, null, tmpNamesArray, 0, colName);
                            }
                            else if ("CNT" == summaryType || "AVG" == summaryType || "SD" == summaryType || "MIN" == summaryType || "MAX" == summaryType || "MED" == summaryType)
                            {
                                NumberFormat(FormatSheet, null, tmpNamesArray, 2, colName);
                            }
                            else
                            {
                                NumberFormat(FormatSheet, null, tmpNamesArray, 0, colName);
                            }


                            if (IsOrientationLandscape)
                            {
                                FormatLandscapeTable(tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.N
                                                   , HasWeight, rMainTable, isN, ref rowCount, NContentsSheet, false, false, -1, null, useSameSheet);
                            }
                            else
                            {
                                // FormatPortraitTable(tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.N 
                                //                  , HasWeight, sht.Range["A1"], isN, NContentsSheet);
                            }

                            if (bgWorker.CancellationPending) return;
                            Range WithShtRangeResize = rTotalTable.Resize[v.GetUpperBound(0), v.GetUpperBound(1)];
                            OutputUtil.PutValue(WithShtRangeResize.Cells, ref v);
                            Range dataRange = WithShtRangeResize.Worksheet.Range[WithShtRangeResize.Item[DataValue.GetLowerBound(0), DataValue.GetLowerBound(1)],
                                WithShtRangeResize.Item[WithShtRangeResize.Rows.Count, WithShtRangeResize.Columns.Count]];
                            OutputUtil.PutValue(dataRange, ref DataValue);
                            _log.Info("Auto fit started");
                            AutoFit(dataRange, colWidthMap);
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
                                if ("AVG" == summaryType)
                                {
                                    if (IsMarkingSignificance) { SignificanceTestMarking(WithShtRangeResize.Cells, ref SigTestMarking); }
                                }
                            }
                            if (!useSameSheet)
                            {
                                NContentsValue.SetValue("TABLE[" + summaryName + "]", tbIndex, 4);
                                NHyperLinkTargetCells.SetValue(sht.Range["A1"], tbIndex, 4);
                            }
                        }
                    }
                    if (HasOutputPerTable)
                    {
                        /************************************************
                            * Total table changes
                            * *********************************************/
                        if (!useSameSheet)
                        {
                            CreateNewSheet(ref PerBooks, TemplatePathIndividual(INDIVIDUAL_TEMPLATE_NAME_P), tmpTable, ref sht, ref PerContentsSheet, ref PerContentsValue, ref PerHyperLinkTargetCells, ref PerOrgSheets, false, TableType.Per, tbIndex + 1);
                            rMainTable = sht.Range["A1"];
                            rTotalTable = sht.Range["C2"];
                            shtTempP = sht;
                        }
                        else
                        {
                            sht = shtTempP;
                            rMainTable = sht.Range[totTabStartCell];
                            rTotalTable = sht.Range[totTabValStartCell];
                        }

                        // Added for - when value izero then set "-"
                        isN2 = false;
                        if ("SUM" == summaryType || "AVG" == summaryType || "SD" == summaryType || "MIN" == summaryType || "MAX" == summaryType || "MED" == summaryType)
                        {
                            isN2 = true;
                        }

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
                                                    , sht.Rows.Count - 1, sht.Columns.Count - 2, ref CheckOverRow, WholeRowColRef, ref OverRowssCountTmpRef, ref OverColumnsCount, isN2);
                            if (OverColumnsCount > 0)
                            {
                                // ResumeError = true
                                tableTypeBuf = LocalResource.REPORT_P_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                                //tableTypeBuf = ThisWorkbook.GetReportKeffyword(ReportMessageIndex_ReportPKeywordIndex)
                                //        Err().Raise vbObjectError + 400&, RunningProcName _
                                //                  , ThisWorkbook.GetReportKffeyword(ReportMessageIndex_ReportColumnsCountOverDetailMessageIndex, tmpTable.Question.Name, tableTypeBuf)
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
                        if (!useSameSheet)
                        {
                            PerContentsValue.SetValue(summaryName, tbIndex, 1);
                            PerContentsValue.SetValue(tmpTable.Question.Description, tbIndex, 2);
                        }
                        if (CheckOverRow || CheckOverColumn)
                        {
                            xlApp.DisplayAlerts = false;
                            sht.Delete();
                            PerContentsValue.SetValue("Error", tbIndex, 4);
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

                            string[] tmpNamesArray = "SA_MA".Split();
                            string colName = "_SectorColumns";
                            if (useSameSheet || "SUM" == summaryType)
                            {
                                NumberFormat(FormatSheet, null, tmpNamesArray, 0, colName);
                            }
                            else if ("CNT" == summaryType || "AVG" == summaryType || "SD" == summaryType || "MIN" == summaryType || "MAX" == summaryType || "MED" == summaryType)
                            {
                                NumberFormat(FormatSheet, null, tmpNamesArray, 2, colName);
                            }
                            else
                            {
                                NumberFormat(FormatSheet, null, tmpNamesArray, 1, colName);
                            }


                            if (IsOrientationLandscape)
                            {
                                FormatLandscapeTable(tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.Per
                                                   , HasWeight, rMainTable, isN, ref rowCount, PerContentsSheet, false, false, -1, null, useSameSheet);
                            }
                            else
                            {
                                // FormatPortraitTable tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType_Per _
                                //                   , HasWeight, sht.Range("A1"), isN, PerContentsSheet
                            }

                            if (bgWorker.CancellationPending) return;
                            Range WithShtRangeResize = rTotalTable.Resize[v.GetUpperBound(0), v.GetUpperBound(1)];
                            OutputUtil.PutValue(WithShtRangeResize.Cells, ref v);
                            Range dataRange = WithShtRangeResize.Worksheet.Range[WithShtRangeResize.Item[DataValue.GetLowerBound(0), DataValue.GetLowerBound(1)],
                                WithShtRangeResize.Item[WithShtRangeResize.Rows.Count, WithShtRangeResize.Columns.Count]];
                            OutputUtil.PutValue(dataRange, ref DataValue);
                            _log.Info("Auto fit started");
                            AutoFit(dataRange, colWidthMap);
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
                            if (!useSameSheet)
                            {
                                PerContentsValue.SetValue("TABLE[" + summaryName + "]", tbIndex, 4);
                                PerHyperLinkTargetCells.SetValue(sht.Range["A1"], tbIndex, 4);
                            }
                        }
                    }
                    //        DoEvents
                    if (SigTestOn)
                    {
                        //CreateNewSheet(ref SigTestBooks, TemplatePathIndividual(INDIVIDUAL_TEMPLATE_NAME_T), tmpTable, ref sht, ref SigTestContentsSheet, ref SigTestContentsValue, ref SigTestHyperLinkTargetCells, ref SigTestOrgSheets, true, TableType.SignificanceTest);
                        /************************************************
                            * Total table changes
                            * *********************************************/
                        if (!useSameSheet)
                        {
                            CreateNewSheet(ref SigTestBooks, TemplatePathIndividual(INDIVIDUAL_TEMPLATE_NAME_T), tmpTable, ref sht, ref SigTestContentsSheet, ref SigTestContentsValue, ref SigTestHyperLinkTargetCells, ref SigTestOrgSheets, true, TableType.SignificanceTest, tbIndex + 1);
                            rMainTable = sht.Range["A1"];
                            rTotalTable = sht.Range["C2"];
                            shtTempS = sht;
                        }
                        else
                        {
                            sht = shtTempS;
                            rMainTable = sht.Range[totTabStartCellSig];
                            rTotalTable = sht.Range[totTabValStartCellSig];
                        }

                        // Added for - when value izero then set "-"
                        isN2 = false;
                        if ("SUM" == summaryType || "AVG" == summaryType || "SD" == summaryType || "MIN" == summaryType || "MAX" == summaryType || "MED" == summaryType)
                        {
                            isN2 = true;
                        }

                        if (IsOrientationLandscape)
                        {
                            CheckOverRow = true;
                            CheckOverColumn = false;
                            OverColumnsCount = 0;
                            int OverRowssCountTmpRef = 0; // only for ref 
                            CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd,
                                ref SigTestMarking, 2, //wt
                                1 + MaxAxesCountArray[i], HasWeight, isN, TableType.SignificanceTest
                                 , sht.Rows.Count - 1, sht.Columns.Count - 2, ref CheckOverRow, WholeRowCol, ref OverRowssCountTmpRef, ref OverColumnsCount, isN2);
                            if (OverColumnsCount > 0)
                            {
                                tableTypeBuf = LocalResource.REPORT_SIGNIFICANCE_TEST_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                                //esumeError = true
                                //   tableTypeBuf = ThisWorkbook.GetReportffKeyword(ReportMessageIndex_ReportSignificanceTestKeywordIndex)
                                //  Err().Raise vbObjectError + 400&, RunningProcName _
                                //          , ThisWorkbook.GetReportKffeyword(ReportMessageIndex_ReportColumnsCountOverDetailMessageIndex, tmpTable.Question.Name, tableTypeBuf)
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
                        if (!useSameSheet)
                        {
                            SigTestContentsValue.SetValue(summaryName, tbIndex, 1);
                            SigTestContentsValue.SetValue(tmpTable.Question.Description, tbIndex, 2);
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

                            string[] tmpNamesArray = "SA_MA".Split();
                            string colName = "_SectorColumns";
                            if (useSameSheet || "SUM" == summaryType)
                            {
                                NumberFormat(FormatSheet, null, tmpNamesArray, 0, colName);
                            }
                            else if ("CNT" == summaryType || "AVG" == summaryType || "SD" == summaryType || "MIN" == summaryType || "MAX" == summaryType || "MED" == summaryType)
                            {
                                NumberFormat(FormatSheet, null, tmpNamesArray, 2, colName);
                            }
                            else
                            {
                                NumberFormat(FormatSheet, null, tmpNamesArray, 1, colName);
                            }

                            if (IsOrientationLandscape)
                            {
                                FormatLandscapeTable(tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.SignificanceTest
                                                 , HasWeight, rMainTable, isN, ref rowCountSig, SigTestContentsSheet, false, false, -1, WholeRowCol, useSameSheet);
                            }
                            else
                            {
                                //FormatPortraitTable tmpTable, sht, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType_SignificanceTest _
                                //                  , HasWeight, sht.Range("A1"), isN, SigTestContentsSheet
                            }

                            if (bgWorker.CancellationPending) return;
                            Range WithShtRangeResize = rTotalTable.Resize[v.GetUpperBound(0), v.GetUpperBound(1)];
                            OutputUtil.PutValue(WithShtRangeResize.Cells, ref v);
                            Range dataRange = WithShtRangeResize.Worksheet.Range[WithShtRangeResize.Item[DataValue.GetLowerBound(0), DataValue.GetLowerBound(1)],
                                WithShtRangeResize.Item[WithShtRangeResize.Rows.Count, WithShtRangeResize.Columns.Count]];
                            OutputUtil.PutValue(dataRange, ref DataValue);
                            _log.Info("Auto fit started");
                            AutoFit(dataRange, colWidthMapSig);
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
                            if (!useSameSheet)
                            {
                                SigTestContentsValue.SetValue("TABLE[" + summaryName + "]", tbIndex, 4);
                                SigTestHyperLinkTargetCells.SetValue(sht.Range["A1"], tbIndex, 4);
                            }
                        }
                    }
                    //        DoEvents
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
                            //   ResumeError = true
                            // Err().Raise vbObjectError + 1200&, RunningProcName _
                            //         , ThisWorkbook.GetReffportKeyword(ReportMessageIndex_ReportOutputIndividualCrossNMessageIndex, errBuf)
                            throw new Exception(string.Format(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_NP_MESSAGE, errBuf));
                        }
                        else
                        {
                            //Err().Raise vbObjectError + 1000&, RunningProcName _
                            //        , ThisWorkbook.GetReportffKeyword(ReportMessageIndex_ReportOutputIndividualCrossErrorMessageIndex, errBuf)
                            throw new Exception(string.Format(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_ERROR_MESSAGE, errBuf));
                        }
                    }
                    else
                    {
                        if (n > 0)
                        {
                            if (n1 > 0)
                            {
                                //ResumeError = true
                                //Err().Raise vbObjectError + 1210&, RunningProcName _
                                //        , ThisWorkbook.GetRffeportKeyword(ReportMessageIndex_ReportRowsCountOverIndividualCrossesNMessageIndex, Join(NOverRowsQs, " , "))
                                throw new Exception(string.Format(LocalResource.REPORT_ROWS_COUNT_OVER_INDIVIDUAL_CROSSES_NP_MESSAGE, String.Join(" , ", NOverRowsQs)));
                            }
                            if (n2 > 0)
                            {
                                //     ResumeError = true
                                //      Err().Raise vbObjectError + 1220&, RunningProcName _
                                //             , ThisWorkbook.GetReportffKeyword(ReportMessageIndex_ReportColumnsCountOverIndividualCrossesNMessageIndex, Join(NOverColumnsQs, " , "))
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
                            //ResumeError = true
                            //Err().Raise vbObjectError + 1300 &, RunningProcName _
                            //         , ThisWorkbook.GetReportffKeyword(ReportMessageIndex_ReportOutputIndividualCrossPMessageIndex, errBuf)
                            throw new Exception(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_P_MESSAGE);
                        }
                        else
                        {
                            //  Err().Raise vbObjectError + 1000 &, RunningProcName _
                            //              , ThisWorkbook.GetReportKffeyword(ReportMessageIndex_ReportOutputIndividualCrossErrorMessageIndex, errBuf)
                            throw new Exception(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_ERROR_MESSAGE);
                        }
                    }
                    else
                    {
                        if (n > 0)
                        {
                            if (n1 > 0)
                            {
                                //ResumeError = true
                                //      Err().Raise vbObjectError + 1310 &, RunningProcName _
                                //               , ThisWorkbook.GetReporfftKeyword(ReportMessageIndex_ReportRowsCountOverIndividualCrossesPMessageIndex, Join(PerOverRowsQs, " , "))
                                throw new Exception(LocalResource.REPORT_ROWS_COUNT_OVER_INDIVIDUAL_CROSSES_P_MESSAGE);
                            }
                            if (n2 > 0)
                            {
                                //    ResumeError = true
                                //          Err().Raise vbObjectError + 1320 &, RunningProcName _
                                //                   , ThisWorkbook.GetReporfftKeyword(ReportMessageIndex_ReportColumnsCountOverIndividualCrossesPMessageIndex, Join(PerOverColumnsQs, " , "))
                                throw new Exception(LocalResource.REPORT_COLUMNS_COUNT_OVER_INDIVIDUAL_CROSSES_P_MESSAGE);
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
                            //    ResumeError = true
                            //  Err().Raise vbObjectError + 1400&, RunningProcName _
                            //          , ThisWorkbook.GetRepffortKeyword(ReportMessageIndex_ReportOutputIndividualCrossSignificanceTestMessageIndex, errBuf)
                            throw new Exception(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_SIGNIFICANCE_TEST_MESSAGE);
                        }
                        else
                        {
                            //Err().Raise vbObjectError + 1000&, RunningProcName _
                            //        , ThisWorkbook.GetReportffKeyword(ReportMessageIndex_ReportOutputIndividualCrossErrorMessageIndex, errBuf)
                            throw new Exception(LocalResource.REPORT_OUTPUT_INDIVIDUAL_CROSS_ERROR_MESSAGE);
                        }
                    }
                    else
                    {
                        if (n > 0)
                        {
                            if (n1 > 0)
                            {
                                //ResumeError = true
                                // Err().Raise vbObjectError + 1410&, RunningProcName _
                                //        , ThisWorkbook.GetRepoffrtKeyword(ReportMessageIndex_ReportRowsCountOverIndividualCrossesSignificanceTestMessageIndex, Join(SigTestOverRowsQs, " , "))
                                throw new Exception(LocalResource.REPORT_ROWS_COUNT_OVER_INDIVIDUAL_CROSSES_SIGNIFICANCE_TEST_MESSAGE);
                            }
                            if (n2 > 0)
                            {
                                //    ResumeError = true
                                //  Err().Raise vbObjectError + 1420&, RunningProcName _
                                //          , ThisWorkbook.GetReporfftKeyword(ReportMessageIndex_ReportColumnsCountOverIndividualCrossesSignificanceTestMessageIndex, Join(SigTestOverColumnsQs, " , "))
                                throw new Exception(LocalResource.REPORT_COLUMNS_COUNT_OVER_INDIVIDUAL_CROSSES_SIGNIFICANCE_TEST_MESSAGE);
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
                        SaveBook(NewBook, tmpPrefix + "_np", xlApp, FormatBook, (i > 1 ? "_" + i : "") + Suffix, FileFormat);
                    }
                }
                if (NBooks != null)
                {
                    i = 0;
                    foreach (Workbook NewBook in NBooks)
                    {
                        i = i + 1;
                        SaveBook(NewBook, tmpPrefix + "_n", xlApp, FormatBook, (i > 1 ? "_" + i : "") + Suffix, FileFormat);
                    }
                }
                if (PerBooks != null)
                {
                    i = 0;
                    foreach (Workbook NewBook in PerBooks)
                    {
                        i = i + 1;
                        SaveBook(NewBook, tmpPrefix + "_p", xlApp, FormatBook, (i > 1 ? "_" + i : "") + Suffix, FileFormat);
                    }
                }
                if (SigTestBooks != null)
                {
                    i = 0;
                    foreach (Workbook NewBook in SigTestBooks)
                    {
                        i = i + 1;
                        SaveBook(NewBook, tmpPrefix + "_ps", xlApp, FormatBook, (i > 1 ? "_" + i : "") + Suffix, FileFormat);
                    }
                }
                //}
                //   CreateIndividualCross = res;
                //      if( res == RaisedError ){ PutErrorsInformation Errors}
            }
            catch (Exception ex)
            {
                //closeAllBook(NPerBooks);
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
                    catch (Exception) { }
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
              , int shtNumber = 0
              )
        {
            int MAX_SHEETS_COUNT = 250;
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
            //if (Table.Index > MaxIndex)
            if (MaxIndex == -1)
            {

                MinIndex = Table.Index;
                MaxIndex = MinIndex + MAX_SHEETS_COUNT - 1;

                int tablesCnt = 0;
                CrossTable tbTmp;
                for (i = 0; i < CurrentOutput.Tables.Count; i++)
                {
                    tbTmp = (CrossTable)CurrentOutput.Tables[i];
                    if (!tbTmp.Question.SummaryTableName.EndsWith("_TT"))
                    {
                        tablesCnt++;
                    }
                }
                if (MaxIndex >= tablesCnt - 1)
                {
                    MaxIndex = tablesCnt - 1;
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
            string[] values = Table.Question.SummaryTableName.Split('&');
            n = (0 != shtNumber ? shtNumber.ToString() : "") + "【" + values[2] + "】";
            TemplateSheet = wb.Worksheets.Item[TempSheetName];
            TemplateSheet.Copy(After: wb.Worksheets.Item[wb.Worksheets.Count]);
            NewSheet = wb.Worksheets.Item[wb.Worksheets.Count];

            i = 1;
            while (n.Length <= MAX_SHEET_NAME_LENGTH)
            {
                try
                {
                    NewSheet.Name = n;
                }
                catch (Exception)
                {
                    i = i + 1;
                    n = Table.Question.Name + "~" + i;
                    continue;
                }
                break;
            }

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
                    catch (Exception)
                    { }
                } while (sh != null);
                NewSheet.Name = n;
            }
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
              , bool isN2 = false

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

            IsShowPreWBTotal = CurrentOutputShowPreWBTotal;
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
                    string[] values = Table.Question.SummaryTableName.Split('&');
                    if (Table.Question.SummaryTableName.EndsWith("_TT"))
                    {
                        TableStringValue.SetValue(values[2].Substring(0, values[2].Length - 3), 1, 1);
                    }
                    else
                    {
                        TableStringValue.SetValue(values[2], 1, 1);
                    }
                    TableStringValue.SetValue(Table.Question.TableHeading, 2, 1);
                    TableStringValue.SetValue(Table.Question.Description, 3, 2);
                    TableStringValue.SetValue(Table.Question.NarrowingCondition, CaptionRowsCount + 1, 1);
                }
                else
                {
                    TableStringValue.SetValue(Table.Question.Name + " " + Table.Question.Description, 1, 1);
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
            HatchingColorIndex = Array.CreateInstance(typeof(int?), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 },
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
                if (!(CutColumnsCol.Contains(y)))
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
                                    // If total table exists
                                    string[] values = Table.Question.SummaryTableName.Split('&');
                                    if (values[0] == "1")
                                    {
                                        buf = "-";
                                    }
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
                                        buf = tmpPercentValue[y, x];
                                        if (isN2 && OutputUtil.IsNumeric(buf) && Convert.ToDouble(buf) == 0)
                                        {
                                            DataValue.SetValue("-", r, c);
                                        }
                                        else
                                        {
                                            DataValue.SetValue(tmpPercentValue[y, x], r, c);
                                        }
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
              , ref int rowCount
              , Worksheet ContentsSheet = null
              , bool IsReport = false
              , bool CutMedian = false, int MedIdx = -1
              , Hashtable WholeRowCol = null
              , bool isTotalTable = false)
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
            string tmp;
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
                    CutMedian = IsReport && (MedIdx >= Table.GetTableValueColumnIndexMinimum);
                }
            }
            else
            {
                CutMedian = false;
                ItemSectorsCount = Table.SectorsCount;

                c = FormatSheet.Range[FormatRangeNamePrefix + "_SectorColumns"].Column;
                if (c + ItemSectorsCount - 1 > TemplateSheet.Columns.Count)
                {
                    ItemSectorsCount = TemplateSheet.Columns.Count - c + 1;
                    CutNAColumn = HasNAColumn;
                    CutIVColumn = HasIVColumn;
                    CutWTColumns = HasWeight;
                }
                if (HasNAColumn && !CutNAColumn)
                {
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2) - (ToInt(HasIVColumn) & 1)))
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
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2)))
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
            for (i = 20; i >= 3 + ToInt(IsReport); i--)
            {
                tmpRange = WithFormatSheetHeader.Cells.Item[WithFormatSheetHeader.Count, i];
                if (((XlLineStyle)tmpRange.Borders.Item[XlBordersIndex.xlEdgeRight].LineStyle) == XlLineStyle.xlContinuous)
                {
                    c = tmpRange.Column;
                    break;
                }
            }
            WithFormatSheetHeader.Copy(WorkingSheet.Range["A1"]);
            int captionCnt = 5;
            if(CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi){ 
                captionCnt--;
            }
            tmpRange = WorkingSheet.Range["A1"].Item[1 + (ToInt(!IsReport) & captionCnt), 1].EntireRow.Resize[WithFormatSheetHeader.Count - (ToInt(!IsReport) & captionCnt)];
            if (c >= 3 + ToInt(IsReport))
            {
                tmpHeaderRange = tmpRange.Worksheet.Range[tmpRange.Columns.Item[3 + ToInt(IsReport)], tmpRange.Columns.Item[c]].Columns;
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
            }
            else if (HasIVColumn)
            {
                f = false;
            }
            if (CutNAColumn)
            {
                c = FormatSheet.Range[FormatRangeNamePrefix + "_NoAnswerColumn"].Column;
                WorkingSheet.Columns.Item[c].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                if (f)
                {
                    Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                    WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                    WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                    WithtmpRangeBrder.Color = BORDER_COLOR;
                }
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
                    Range WithWorkingSheet = WorkingSheet.Range[ColumnIndexToColumnLetter(c) + 5 + ":" + ColumnIndexToColumnLetter(c) + 9];
                    
                    if (ItemSectorsCount > 2)
                    {
                        WithWorkingSheet.Copy();
                        WithWorkingSheet.Resize[ColumnSize: ItemSectorsCount - 2].Insert(XlInsertShiftDirection.xlShiftToRight);
                        xlApp.CutCopyMode = XlCutCopyMode.xlCopy;
                    }
                    else
                    {
                        if (Table.GetTableValueColumnIndexMaximum < 4)
                        {
                            WithWorkingSheet.Resize[ColumnSize: 2].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                            RedrawBorder = true;
                        }
                        else
                        {
                            WithWorkingSheet.Resize[ColumnSize: 2 - ItemSectorsCount].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                        }
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
            }
            y = 2;// wt ' ヘッダの下端インデックス
            if ((CutRowsCol.ContainsKey(y)))
            {
                WorkingSheet.Rows.Item[r].Resize[d].Delete(XlDeleteShiftDirection.xlShiftUp);
                r = r - d;
            }
            for (idx = 0; idx <= Table.AxesGroups.Count - 1; idx++)
            {
                tmpRange = FormatSheet.Range[FormatRangeNamePrefix + (Table.AxesGroups[idx].Count == 1 ? "_Double" : "_Triple")].EntireRow;
                Range WithWorkingSheetRows = WorkingSheet.Rows;
                tmpRange.Copy(WithWorkingSheetRows.Item[r]);
                tmpRange = WithWorkingSheetRows.Item[r].Resize[tmpRange.Count];
                tmpTableRows = WithWorkingSheetRows.Worksheet.Range[WithWorkingSheetRows.Item[1], tmpRange];
                f = true;
                if (CutWTColumns)
                {
                    c = FormatSheet.Range[FormatRangeNamePrefix + "_PopulationColumn"].Column;
                    tmpRange.Columns.Item[c].Resize[ColumnSize: 2].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                    WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                    WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                    WithtmpRangeBrder.Color = BORDER_COLOR;
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
                }
                else if (HasIVColumn)
                {
                    f = false;
                }
                if (CutNAColumn)
                {
                    c = FormatSheet.Range[FormatRangeNamePrefix + "_NoAnswerColumn"].Column;
                    tmpRange.Columns.Item[c].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    if (f)
                    {
                        Border WithtmpRangeBrder = tmpRange.Columns.Item[c - 1].Borders.Item[XlBordersIndex.xlEdgeRight];
                        WithtmpRangeBrder.LineStyle = XlLineStyle.xlContinuous;
                        WithtmpRangeBrder.Weight = XlBorderWeight.xlThin;
                        WithtmpRangeBrder.Color = BORDER_COLOR;
                    }
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
                            if (Table.GetTableValueColumnIndexMaximum < 4)
                            {
                                WithtmpRangeColumns.Resize[ColumnSize: 2].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                                RedrawBorder = true;
                            }
                            else
                            {
                                WithtmpRangeColumns.Resize[ColumnSize: 2 - ItemSectorsCount].Delete(XlDeleteShiftDirection.xlShiftToLeft);
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
                }
                //    ' 行
                if (Table.AxesGroups[idx].Count == 1)
                { //' 二重クロス
                    y = y + 1;  //' 小計行インデックス
                    tmpR = r;
                    if ((CutRowsCol.ContainsKey(y)))
                    {
                        WorkingSheet.Rows.Item[r].Resize[d].Delete(XlDeleteShiftDirection.xlShiftUp);
                    }
                    else
                    {
                        r = r + d;
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
                }
                else
                {   // ' 三重クロス
                    y = y + 1;
                    if ((CutRowsCol.ContainsKey(y)))
                    {
                        WorkingSheet.Rows.Item[r].Resize[d].Delete(XlDeleteShiftDirection.xlShiftUp);
                        tmpR = r;

                    }
                    else
                    {
                        r = r + d;
                        tmpR = r - d;
                    }
                    tmpRange2 = WorkingSheet.Rows.Item[r + d + 3 * d2].Resize[d + 3 * d2];
                    AxesInformation tmpAxes2 = Table.AxesGroups[idx];
                    SectorsCount[0] = tmpAxes2[0].SectorsCount;
                    n = tmpAxes2[1].SectorsCount + (ToInt(HasNARow) & 1) + (ToInt(HasIVRow) & 1) + 1;
                    tmpY = y;
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
                        tmp = Convert.ToString(i);
                        if (CutNA && !CutIV)
                        {
                            if (i > y - n)
                            {
                                tmp = Convert.ToString(i + n);
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
                                rng = xlApp.Intersect(tmpHeaderRange.EntireColumn, WorkingSheet.Rows.Item[r]);
                                if (rng != null)
                                {
                                    Border WithtmpRangeBrder = rng.Borders.Item[XlBordersIndex.xlEdgeTop];
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
                if (bgWorker.CancellationPending) return;
            }

            Range WithWorkingSheet1 = WorkingSheet.Range[CELL_RANGE_SINGLE_3];
            WithWorkingSheet1.Merge();
            WithWorkingSheet1 = WorkingSheet.Range[CELL_RANGE_SINGLE_4];
            WithWorkingSheet1.Merge();

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
                    Range WithStartCell = StartCell.EntireRow.Range["A1"].Resize[TableRows.Count];
                    if (isTotalTable)
                    {
                        tmpBuf.SetValue(TOTAL_TABLE_NAME, 1, 0);
                        OutputUtil.PutValue(WithStartCell.Cells, ref tmpBuf);
                        WithStartCell.Font.Size = 4.5;
                        WithStartCell.Font.Color = TOATL_FONT_COLOR;
                        WithStartCell.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
                        WithStartCell.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        WithStartCell.EntireRow.Interior.Color = TOATL_BG_COLOR;
                        WithStartCell.Borders.Color = TOATL_BG_COLOR;
                    }
                    else
                    {
                        tmpBuf.SetValue(LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME, TableRows.Count, 0);
                        WithStartCell.Hyperlinks.Add(WithStartCell.Cells, "", tmpAddress);
                        OutputUtil.PutValue(WithStartCell.Cells, ref tmpBuf);
                        WithStartCell.Font.Size = 5.5;
                        WithStartCell.Borders.Item[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
                        WithStartCell.Interior.Color = FIRST_TABLE_BG_COLOR;
                    }
                }
            }
            rowCount = TableRows.Count;
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
        public static void AutoFit(Range dataRange, Dictionary<string, double> colWidthMap)
        {
            foreach (Range col in dataRange.Columns)
            {
                col.AutoFit();
                string name = col.Worksheet.Name + "_" + col.Column;
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
