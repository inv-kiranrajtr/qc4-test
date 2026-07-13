using log4net;
using Macromill.QCWeb.Batch.Report;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
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
using XlPageOrientation = Macromill.QCWeb.Common.XlPageOrientation;
using XlPaperSize = Macromill.QCWeb.Common.XlPaperSize;
using System.ComponentModel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using Microsoft.VisualBasic;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text.RegularExpressions;
using Qc4Launcher.Logic.Cross_Report;

namespace Qc4Launcher.Summary.OpenXml
{
    public class SummaryCreatorXml
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public OutputCross CurrentOutput = null;
        public bool CurrentOutputShowPreWBTotal = false;
        public string WorkingSheet;
        public string WorkingBook;
        public Microsoft.Office.Interop.Excel.Application xlApp;
        public string wbs;
        private string wss;
        public string ThisLocationCode;
        public ExecuteStaticMethod ExecuteStaticMethod = new ExecuteStaticMethod();
        private static double COL_MIN_WIDTH = 42;
        private static int MaxRowsCount = 1048575;
        private static int MaxColumnsCount = 16382;
        private static double COL_MIN_COLWIDTH = 8.5;
        private static int ROW_COUNT_BW_TABLES = 3;
        private static int SA_MA_STD_LCol = 10;
        private static int SA_MA_WT_STD_LCol = 12;
        private static int N_STD_LCol = 15;
        private static int SA_MA_SIG_LCol = 11;
        private static int SA_MA_WT_SIG_LCol = 13;
        private static int N_SIG_LCol = 17;
        private static string TOTAL_TABLE_NAME = "ベース";
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
        public void CreateSummary(Output Output, string bookPSWD, string sheetPSWD, string outputDirectoryPath,
          string templateDirectoryPath, Microsoft.Office.Interop.Excel.Application xlAppG, BackgroundWorker bgWorker, bool onlySigPageP = false, bool checkCrossP = false,
          SummaryTabulation QC = null, double progressAvailable = 0, double currentProgress = 0, List<string> outputFiles = null)
        {
            Reportset reportset = (Reportset)Output.ParentReportset;
            CurrentOutput = (OutputCross)Output;
            BookPSWD = bookPSWD;
            SheetPSWD = sheetPSWD;
            OutputDirectoryPath = outputDirectoryPath;
            TemplateDirectoryPath = templateDirectoryPath;
            this.progressAvailable = progressAvailable;
            this.currentProgress = currentProgress;
            this.QC = QC;
            string FormatBook = null;
            string baseBook = null;
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

                _log.Info("Excel base book added");

                WorkingBook = baseBook;

                CrossTable tmpTable = (CrossTable)Output.Tables[0];
                Macromill.QCWeb.ReportRequest.KeyItemInformation KeyItemInfo = tmpTable.KeyItem;
                string KeyItemName = string.Empty;
                string filenameSuffix = null;
                if (KeyItemInfo != null)
                    KeyItemName = KeyItemInfo.Name;
                if (KeyItemName.Length > 0)
                {
                    string fmt = new string('0', 3);
                    filenameSuffix = "_" + KeyItemName + "_" + KeyItemInfo.SectorNumber.ToString(fmt);
                }
                _log.Info("Excel format book added");

                CreateIndividualSummary(FormatBook, filenameSuffix);
            }
            catch (Exception ex)
            {
                _log.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
        private void CreateIndividualSummary(string FormatBook, string Suffix)
        {
            string NFormatSheet = null;
            string NDoubleFormatSheet = null;
            string PerFormatSheet = null;
            string PerDoubleFormatSheet = null;
            string SigTestPerFormatSheet = null;
            string SigTestPerDoubleFormatSheet = null;
            string FormatSheet = null;
            bool HasWeightColumn = false;
            int maxAxisCnt = 0;
            int NCutColumn = 0;
            int PCutColumn = 0;
            bool HasWeightBack;
            bool HasWeight;
            Hashtable CutRowsCol = null;
            Hashtable CutColumnsCol = null;
            string FormatRangeNamePrefix;

            CrossTable tmpTable;
            string ReportTitle;
            List<string> NPerBooks = null;
            List<string> NBooks = null;
            List<string> PerBooks = null;
            List<string> SigTestBooks = null;
            Array NContentsValue = null; // string
            Array PerContentsValue = null;  //string
            Array SigTestContentsValue = null; // string
            Array NHyperLinkTargetCells = null;  //Range
            Array PerHyperLinkTargetCells = null; // Range
            Array SigTestHyperLinkTargetCells = null;  //Range
            string NContentsSheet = null;
            string PerContentsSheet = null;
            string SigTestContentsSheet = null;
            List<string> NOrgSheets = null;
            List<string> PerOrgSheets = null;
            List<string> SigTestOrgSheets = null;
            string sht = null;
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
            int NRowNum = 1;
            int PRowNum = 1;
            int SRowNum = 1;
            int FirstRow = 1;
            bool SigTestOn = false;
            Hashtable WholeRowCol = null;
            bool CheckOverRow;
            bool CheckOverColumn;
            SpreadsheetDocument SummaryN = null;
            SpreadsheetDocument SummaryP = null;
            SpreadsheetDocument SummarySig = null;
            WorksheetPart worksheetPartIndexN = null;
            WorksheetPart worksheetPartIndexP = null;
            WorksheetPart worksheetPartIndexSig = null;
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
            string defaultPath = "";
            string selectedPath;
            string summaryNPath = null;
            string summaryPerPath = null;
            string summarySigPath = null;

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
                    if (!HasWeightColumn) { HasWeightColumn = GetHasWeight(tmpTable); }
                }

                if (true)
                {
                    if ((maxAxisCnt & 2) == 2)
                    {// ' 三重あり
                        NFormatSheet = "N_Std";
                        AdjustFormat(NFormatSheet, null, 2, ref NCutColumn, HasWeightColumn);
                    }
                    if ((maxAxisCnt & 1) == 1)
                    { //' 二重あり
                        if ((maxAxisCnt & 2) == 0)
                        {
                            NDoubleFormatSheet = "N_Std";
                        }
                        AdjustFormat(NDoubleFormatSheet, null, 1, ref NCutColumn, HasWeightColumn, NFormatSheet != null);
                    }
                }
                if (true)
                {
                    if ((maxAxisCnt & 2) == 2)
                    { //' 三重あり
                        if (HasOutputPerTable) { PerFormatSheet = "P_Std"; }
                        if (SigTestOn) { SigTestPerFormatSheet = "P_Sig"; }
                        AdjustFormat(PerFormatSheet, SigTestPerFormatSheet, 2, ref NCutColumn, HasWeightColumn);
                    }
                    if ((maxAxisCnt & 1) == 1)
                    { //' 二重あり
                        if ((maxAxisCnt & 2) == 0)
                        {
                            if (true) { PerDoubleFormatSheet = "P_Std"; }
                            if (SigTestOn) { SigTestPerDoubleFormatSheet = "P_Sig"; }
                        }
                        AdjustFormat(PerDoubleFormatSheet, SigTestPerDoubleFormatSheet, 1, ref PCutColumn, HasWeightColumn, PerFormatSheet != null || SigTestPerFormatSheet != null);
                    }
                }
                ReportTitle = CurrentOutput.ParentRequest.Title;
                if (HasOutputNTable)
                {
                    NOverRowsQs = new string[0];
                    if (IsOrientationPortrait)
                    {
                        NOverColumnsQs = new string[0];
                    }
                }
                if (HasOutputPerTable)
                {
                    PerOverRowsQs = new string[0];
                    if (IsOrientationPortrait)
                    {
                        PerOverColumnsQs = new string[0];
                    }
                }
                if (SigTestOn)
                {
                    SigTestOverRowsQs = new string[0];
                    if (IsOrientationPortrait)
                    {
                        SigTestOverColumnsQs = new string[0];
                    }
                }
                /************************************************
                   * Total table changes
                   * *********************************************/
                bool useSameSheet = false;
                int numDigit = 0;
                string rMainTable = null;
                string rTotalTable = null;
                string shtTempN = null;
                string shtTempP = null;
                string shtTempS = null;
                string summaryName = null;
                string summaryType = null;
                int rowCount = 0;
                int rowCountSig = 0;
                int tbIndex = -1;
                int[] styleIndexArray = null;
                double progressStep = progressAvailable / CurrentOutput.Tables.Count;
                WorksheetPart worksheetPart = null;

                for (i = 0; i < CurrentOutput.Tables.Count; i++)
                {
                    if (bgWorker.CancellationPending) return;
                    updateProgress(currentProgress, String.Format(LocalResource.PB_EXCEL_GEN_TABLE, (i + 1), CurrentOutput.Tables.Count));
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
                    }

                    if (HasWeight) { FormatRangeNamePrefix = FormatRangeNamePrefix + "_WT"; }
                    if (SigTestOn) { WholeRowCol = new Hashtable(); }
                    int medIdx = -1;
                    GetCutRowsAndColumns(tmpTable, HasWeightBack, HasWeight,
                        MaxAxesCountArray[i], ref CutRowsCol, ref CutColumnsCol, ref medIdx, false, false, WholeRowCol);
                    /************************************************
                    * Total table changes
                    * *********************************************/

                    int totTabStartIndex = rowCount + ROW_COUNT_BW_TABLES;
                    string totTabStartCell = "A" + totTabStartIndex.ToString();
                    totTabStartIndex++;
                    string totTabValStartCell = "C" + totTabStartIndex.ToString();

                    int totTabStartIndexSig = rowCountSig + ROW_COUNT_BW_TABLES;
                    string totTabStartCellSig = "A" + totTabStartIndexSig.ToString();
                    totTabStartIndexSig++;
                    string totTabValStartCellSig = "C" + totTabStartIndexSig.ToString();
                    tmpPrefix = CurrentOutput.ParentReportset.DivName + CurrentOutput.ExcelBookNamePrefix;

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
                        if (summaryNPath == null)
                        {
                            defaultPath = OpenXmlHelper.GetDefaultPath(xlApp, "Summary_n", "SummaryOutput", "SummaryOutputForSTD");
                            selectedPath = SummaryHelper.GetSelectedPath(xlApp, OutputDirectoryPath, tmpPrefix + "_n", (i > 1 ? "_" + i : "") + Suffix);
                            summaryNPath = selectedPath == null ? defaultPath : selectedPath;
                        }
                        using (SummaryN = (SummaryN == null) ? SpreadsheetDocument.Create(summaryNPath, SpreadsheetDocumentType.Workbook)
                                            : SpreadsheetDocument.Open(summaryNPath, true))
                        {
                            numDigit = 0;
                            if (!useSameSheet)
                            {
                                CreateNewSheet(ref SummaryN, tmpTable, ref sht, ref NContentsSheet, ref NContentsValue, ref NHyperLinkTargetCells, ref NOrgSheets, false, TableType.N, tbIndex + 1);
                                shtTempN = sht;
                                NRowNum = 1;
                                FirstRow = NRowNum;
                            }
                            else
                            {
                                NRowNum += 2;
                                sht = shtTempN;
                                FirstRow = NRowNum;
                            }
                            worksheetPartIndexN = OpenXmlHelper.GetWorksheetPartByName(SummaryN, "INDEX");
                            worksheetPart = OpenXmlHelper.GetWorksheetPartByName(SummaryN, sht);
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
                                                        , MaxRowsCount, MaxColumnsCount, ref CheckOverRow, WholeRowColRef, ref OverRowssCountTmpRef, ref OverColumnsCount);
                                if (OverColumnsCount > 0)
                                {
                                    tableTypeBuf = LocalResource.REPORT_N_KEYWORD;
                                    throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));

                                }
                            }
                            else
                            {
                                CheckOverRow = true;
                                CheckOverColumn = true;
                            }
                            if (!useSameSheet)
                            {
                                NContentsValue.SetValue(summaryName, tbIndex, 1);
                                NContentsValue.SetValue(tmpTable.Question.TableHeading, tbIndex, 2);
                                NContentsValue.SetValue(tmpTable.Question.Description, tbIndex, 3);
                            }
                            if (CheckOverRow || CheckOverColumn)
                            {

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
                                    numDigit = 0;
                                    styleIndexArray = new int[] { 92, 97, 101 };
                                    NumberFormat(SummaryN, styleIndexArray, FormatSheet, null, tmpNamesArray, 0, colName);
                                }
                                else if ("CNT" == summaryType || "AVG" == summaryType || "SD" == summaryType || "MIN" == summaryType || "MAX" == summaryType || "MED" == summaryType)
                                {
                                    numDigit = 2;
                                    styleIndexArray = new int[] { 205, 206, 207 };
                                    NumberFormat(SummaryN, styleIndexArray, FormatSheet, null, tmpNamesArray, 2, colName);
                                }
                                else
                                {
                                    numDigit = 0;
                                    styleIndexArray = new int[] { 7, 50, 11 };
                                    NumberFormat(SummaryN, styleIndexArray, FormatSheet, null, tmpNamesArray, 0, colName);
                                }

                                if (IsOrientationLandscape)
                                {
                                    FormatLandscapeTable(ref SummaryN, worksheetPart, maxAxisCnt, tmpTable, sht, ref NRowNum, numDigit, summaryType, CutRowsCol, CutColumnsCol, NCutColumn, FormatSheet, FormatRangeNamePrefix, TableType.N
                                                       , HasWeight, rMainTable, isN, ref rowCount, NContentsSheet, false, false, -1, null, useSameSheet);
                                }

                                if (bgWorker.CancellationPending) return;
                                int row = Information.LBound(v, 1) + FirstRow;
                                int fCol = Information.LBound(v, 2) + 2;
                                SummaryHelper.PutValue(worksheetPart, row, fCol, ref v);
                                row = Information.LBound(DataValue, 1) + FirstRow;
                                fCol = Information.LBound(DataValue, 2) + 2;
                                SummaryHelper.PutValue(worksheetPart, row, fCol, ref DataValue);
                                _log.Info("Auto fit started");
                                fCol = Information.LBound(DataValue, 2) + 2;
                                int lCol = Information.UBound(DataValue, 2) + 2;
                                int fRow = Information.LBound(DataValue, 1) + FirstRow;
                                int lstRow = (int)worksheetPart.Worksheet.Descendants<Row>().LastOrDefault().RowIndex.Value - 1;
                                OpenXmlHelper.AutoFitColumn(SummaryN.WorkbookPart, worksheetPart, fCol, lCol, fRow, lstRow);
                                _log.Info("Auto fit started complted");

                                if (!isN)
                                {
                                    if (IsMarkingColoring) { Hatching(ref HatchingColorIndex, worksheetPart, SummaryN); }

                                    if ("AVG" == summaryType)
                                    {
                                        if (IsMarkingSignificance) { SignificanceTestMarking(ref SigTestMarking, worksheetPart, SummaryN); }
                                    }
                                }
                                if (!useSameSheet)
                                {
                                    NContentsValue.SetValue("TABLE[" + summaryName + "]", tbIndex, 4);
                                    NHyperLinkTargetCells.SetValue("\'" + sht + "\'!$A$1", tbIndex, 4);
                                }
                            }
                        }
                    }
                    if (HasOutputPerTable)
                    {
                        /************************************************
                            * Total table changes
                            * *********************************************/
                        if (summaryPerPath == null)
                        {
                            defaultPath = OpenXmlHelper.GetDefaultPath(xlApp, "Summary_p", "SummaryOutput", "SummaryOutputForSTD");
                            selectedPath = SummaryHelper.GetSelectedPath(xlApp, OutputDirectoryPath, tmpPrefix + "_p", (i > 1 ? "_" + i : "") + Suffix);
                            summaryPerPath = selectedPath == null ? defaultPath : selectedPath;
                        }
                        using (SummaryP = (SummaryP == null) ? SpreadsheetDocument.Create(summaryPerPath, SpreadsheetDocumentType.Workbook)
                                                                : SpreadsheetDocument.Open(summaryPerPath, true))
                        {
                            numDigit = 1;
                            if (!useSameSheet)
                            {
                                CreateNewSheet(ref SummaryP, tmpTable, ref sht, ref PerContentsSheet, ref PerContentsValue, ref PerHyperLinkTargetCells, ref PerOrgSheets, false, TableType.Per, tbIndex + 1);
                                shtTempN = sht;
                                PRowNum = 1;
                                FirstRow = PRowNum;
                            }
                            else
                            {
                                PRowNum += 2;
                                sht = shtTempN;
                                FirstRow = PRowNum;
                            }
                            // Added for - when value izero then set "-"
                            isN2 = false;
                            if ("SUM" == summaryType || "AVG" == summaryType || "SD" == summaryType || "MIN" == summaryType || "MAX" == summaryType || "MED" == summaryType)
                            {
                                isN2 = true;
                            }
                            worksheetPart = OpenXmlHelper.GetWorksheetPartByName(SummaryP, sht);
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
                                                        , MaxRowsCount, MaxColumnsCount, ref CheckOverRow, WholeRowColRef, ref OverRowssCountTmpRef, ref OverColumnsCount, isN2);
                                if (OverColumnsCount > 0)
                                {
                                    tableTypeBuf = LocalResource.REPORT_P_KEYWORD;
                                    throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                                }
                            }
                            else
                            {
                                CheckOverRow = true;
                                CheckOverColumn = true;
                            }
                            if (!useSameSheet)
                            {
                                PerContentsValue.SetValue(summaryName, tbIndex, 1);
                                PerContentsValue.SetValue(tmpTable.Question.TableHeading, tbIndex, 2);
                                PerContentsValue.SetValue(tmpTable.Question.Description, tbIndex, 3);
                            }
                            if (CheckOverRow || CheckOverColumn)
                            {

                                PerContentsValue.SetValue("Error", tbIndex, 4);
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
                                    FormatSheet = PerFormatSheet;
                                }
                                else
                                {
                                    FormatSheet = PerDoubleFormatSheet;
                                }

                                string[] tmpNamesArray = "SA_MA".Split();
                                string colName = "_SectorColumns";
                                if (useSameSheet)
                                {
                                    numDigit = 0;
                                    styleIndexArray = new int[] { 92, 97, 101 };
                                    NumberFormat(SummaryP, styleIndexArray, FormatSheet, null, tmpNamesArray, 0, colName);
                                }
                                else if ("SUM" == summaryType)
                                {
                                    numDigit = 0;
                                    styleIndexArray = new int[] { 7, 50, 11 };
                                    NumberFormat(SummaryP, styleIndexArray, FormatSheet, null, tmpNamesArray, 0, colName);
                                }
                                else if ("CNT" == summaryType || "AVG" == summaryType || "SD" == summaryType || "MIN" == summaryType || "MAX" == summaryType || "MED" == summaryType)
                                {
                                    numDigit = 2;
                                    styleIndexArray = new int[] { 205, 206, 207 };
                                    NumberFormat(SummaryP, styleIndexArray, FormatSheet, null, tmpNamesArray, 2, colName);
                                }

                                if (IsOrientationLandscape)
                                {
                                    FormatLandscapeTable(ref SummaryP, worksheetPart, maxAxisCnt, tmpTable, sht, ref PRowNum, numDigit, summaryType, CutRowsCol, CutColumnsCol, PCutColumn, FormatSheet, FormatRangeNamePrefix, TableType.Per
                                                  , HasWeight, rMainTable, isN, ref rowCount, PerContentsSheet, false, false, -1, null, useSameSheet);
                                }

                                if (bgWorker.CancellationPending) return;
                                int row = Information.LBound(v, 1) + FirstRow;
                                int fCol = Information.LBound(v, 2) + 2;
                                SummaryHelper.PutValue(worksheetPart, row, fCol, ref v);
                                row = Information.LBound(DataValue, 1) + FirstRow;
                                fCol = Information.LBound(DataValue, 2) + 2;
                                SummaryHelper.PutValue(worksheetPart, row, fCol, ref DataValue);
                                _log.Info("Auto fit started");
                                fCol = Information.LBound(DataValue, 2) + 2;
                                int lCol = Information.UBound(DataValue, 2) + 2;
                                int fRow = Information.LBound(DataValue, 1) + FirstRow;
                                int lstRow = (int)worksheetPart.Worksheet.Descendants<Row>().LastOrDefault().RowIndex.Value - 1;
                                OpenXmlHelper.AutoFitColumn(SummaryP.WorkbookPart, worksheetPart, fCol, lCol, fRow, lstRow);
                                _log.Info("Auto fit started complted");

                                if (!isN)
                                {
                                    if (IsMarkingColoring) { Hatching(ref HatchingColorIndex, worksheetPart, SummaryP); }
                                    if (IsMarkingSignificance) { SignificanceTestMarking(ref SigTestMarking, worksheetPart, SummaryP); }
                                }
                                if (!useSameSheet)
                                {
                                    PerContentsValue.SetValue("TABLE[" + summaryName + "]", tbIndex, 4);
                                    PerHyperLinkTargetCells.SetValue("\'" + sht + "\'!$A$1", tbIndex, 4);
                                }
                            }
                        }
                    }
                    if (SigTestOn)
                    {
                        /************************************************
                            * Total table changes
                            * *********************************************/
                        if (summarySigPath == null)
                        {
                            defaultPath = OpenXmlHelper.GetDefaultPath(xlApp, "Summary_ps", "SummaryOutput", "SummaryOutputForSTD");
                            selectedPath = SummaryHelper.GetSelectedPath(xlApp, OutputDirectoryPath, tmpPrefix + "_ps", (i > 1 ? "_" + i : "") + Suffix);
                            summarySigPath = selectedPath == null ? defaultPath : selectedPath;
                        }
                        using (SummarySig = (SummarySig == null) ? SpreadsheetDocument.Create(summarySigPath, SpreadsheetDocumentType.Workbook)
                                            : SpreadsheetDocument.Open(summarySigPath, true))
                        {
                            numDigit = 1;
                            if (!useSameSheet)
                            {
                                CreateNewSheet(ref SummarySig, tmpTable, ref sht, ref SigTestContentsSheet, ref SigTestContentsValue, ref SigTestHyperLinkTargetCells, ref SigTestOrgSheets, true, TableType.SignificanceTest, tbIndex + 1);
                                shtTempN = sht;
                                SRowNum = 1;
                                FirstRow = SRowNum;
                            }
                            else
                            {
                                SRowNum += 2;
                                sht = shtTempN;
                                FirstRow = SRowNum;
                            }
                            worksheetPart = OpenXmlHelper.GetWorksheetPartByName(SummarySig, sht);
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
                                     , MaxRowsCount, MaxColumnsCount, ref CheckOverRow, WholeRowCol, ref OverRowssCountTmpRef, ref OverColumnsCount, isN2);
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

                            }
                            if (!useSameSheet)
                            {
                                SigTestContentsValue.SetValue(summaryName, tbIndex, 1);
                                SigTestContentsValue.SetValue(tmpTable.Question.TableHeading, tbIndex, 2);
                                SigTestContentsValue.SetValue(tmpTable.Question.Description, tbIndex, 3);
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
                                if (useSameSheet)
                                {
                                    numDigit = 0;
                                    styleIndexArray = new int[] { 128, 200, 198 };
                                    NumberFormat(SummarySig, styleIndexArray, FormatSheet, null, tmpNamesArray, 0, colName);
                                }
                                else if ("CNT" == summaryType || "AVG" == summaryType || "SD" == summaryType || "MIN" == summaryType || "MAX" == summaryType || "MED" == summaryType)
                                {
                                    numDigit = 2;
                                    styleIndexArray = new int[] { 205, 206, 207 };
                                    NumberFormat(SummarySig, styleIndexArray, FormatSheet, null, tmpNamesArray, 2, colName);
                                }
                                else if ("SUM" == summaryType)
                                {
                                    numDigit = 0;
                                    styleIndexArray = new int[] { 208, 209, 210 };
                                    NumberFormat(SummarySig, styleIndexArray, FormatSheet, null, tmpNamesArray, 0, colName);
                                }

                                if (IsOrientationLandscape)
                                {
                                    FormatLandscapeTable(ref SummarySig, worksheetPart, maxAxisCnt, tmpTable, sht, ref SRowNum, numDigit, summaryType, CutRowsCol, CutColumnsCol, NCutColumn, FormatSheet, FormatRangeNamePrefix, TableType.SignificanceTest
                                                     , HasWeight, rMainTable, isN, ref rowCountSig, SigTestContentsSheet, false, false, -1, WholeRowCol, useSameSheet);
                                }

                                if (bgWorker.CancellationPending) return;
                                int row = Information.LBound(v, 1) + FirstRow;
                                int fCol = Information.LBound(v, 2) + 2;
                                SummaryHelper.PutValue(worksheetPart, row, fCol, ref v);
                                row = Information.LBound(DataValue, 1) + FirstRow;
                                fCol = Information.LBound(DataValue, 2) + 2;
                                SummaryHelper.PutValue(worksheetPart, row, fCol, ref DataValue);
                                _log.Info("Auto fit started");
                                fCol = Information.LBound(DataValue, 2) + 2;
                                int lCol = Information.UBound(DataValue, 2) + 2;
                                int fRow = Information.LBound(DataValue, 1) + FirstRow;
                                int lstRow = (int)worksheetPart.Worksheet.Descendants<Row>().LastOrDefault().RowIndex.Value - 1;
                                OpenXmlHelper.AutoFitColumn(SummarySig.WorkbookPart, worksheetPart, fCol, lCol, fRow, lstRow);
                                _log.Info("Auto fit completed");

                                if (!useSameSheet)
                                {
                                    SigTestContentsValue.SetValue("TABLE[" + summaryName + "]", tbIndex, 4);
                                    SigTestHyperLinkTargetCells.SetValue("\'" + sht + "\'!$A$1", tbIndex, 4);
                                }
                            }
                        }
                    }
                }
                CrossTable tmpTbl;
                int strtRow = 11;
                WorksheetPart worksheetPartIndex = null;
                if (HasOutputNTable)
                {
                    using (SummaryN = SpreadsheetDocument.Open(summaryNPath, true))
                    {
                        worksheetPartIndex = OpenXmlHelper.GetWorksheetPartByName(SummaryN, "INDEX");
                        tmpTbl = (CrossTable)CurrentOutput.Tables[0];
                        if (tmpTbl.KeyItem != null)
                            strtRow = 14;
                        PutContents(worksheetPartIndex, ref NContentsValue, ref NHyperLinkTargetCells, strtRow);
                    }
                    if (OutputDirectoryPath == null)
                        OpenXmlHelper.SaveWorkBook(summaryNPath, BookPSWD, xlApp);
                }
                if (HasOutputPerTable)
                {
                    using (SummaryP = SpreadsheetDocument.Open(summaryPerPath, true))
                    {
                        worksheetPartIndex = OpenXmlHelper.GetWorksheetPartByName(SummaryP, "INDEX");
                        tmpTbl = (CrossTable)CurrentOutput.Tables[0];
                        if (tmpTbl.KeyItem != null)
                            strtRow = 14;
                        PutContents(worksheetPartIndex, ref PerContentsValue, ref PerHyperLinkTargetCells, strtRow);
                    }
                    if (OutputDirectoryPath == null)
                        OpenXmlHelper.SaveWorkBook(summaryPerPath, BookPSWD, xlApp);
                }
                if (SigTestOn)
                {
                    using (SummarySig = SpreadsheetDocument.Open(summarySigPath, true))
                    {
                        worksheetPartIndex = OpenXmlHelper.GetWorksheetPartByName(SummarySig, "INDEX");
                        tmpTbl = (CrossTable)CurrentOutput.Tables[0];
                        if (tmpTbl.KeyItem != null)
                            strtRow = 14;
                        PutContents(worksheetPartIndex, ref SigTestContentsValue, ref SigTestHyperLinkTargetCells, strtRow);
                    }
                    if (OutputDirectoryPath == null)
                        OpenXmlHelper.SaveWorkBook(summarySigPath, BookPSWD, xlApp);
                }
                try
                {
                    String directoryPath = Path.GetDirectoryName(defaultPath);
                    if (Directory.Exists(directoryPath))
                    {
                        var di = new DirectoryInfo(directoryPath);
                        if (di.Attributes.HasFlag(FileAttributes.ReadOnly))
                            di.Attributes &= ~FileAttributes.ReadOnly;
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                try
                {
                    String directoryPath = Path.GetDirectoryName(defaultPath);
                    if (Directory.Exists(directoryPath))
                    {
                        var di = new DirectoryInfo(directoryPath);
                        if (di.Attributes.HasFlag(FileAttributes.ReadOnly))
                            di.Attributes &= ~FileAttributes.ReadOnly;
                    }
                }
                catch { }
                throw ex;
            }
            finally
            {

                GC.Collect();
            }
        }
        public void SignificanceTestMarking(ref Array SigTestMarking, WorksheetPart worksheetPart, SpreadsheetDocument document)
        {
            int y, x;
            string buf;
            string fmt;
            for (y = SigTestMarking.GetLowerBound(0); y <= SigTestMarking.GetUpperBound(0); y++)
            {
                for (x = SigTestMarking.GetLowerBound(1); x <= SigTestMarking.GetUpperBound(1); x++)
                {
                    buf = (string)SigTestMarking.GetValue(y, x);
                    if (null != buf && buf.Length > 0)
                    {
                        Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)y + 1);
                        Cell cell = OpenXmlHelper.GetCell(row, y + 1, 2 + x);
                        OpenXmlHelper.SetSignificanceTestMarking(document, cell, buf);
                    }
                }
            }
        }

        public static Dictionary<string, UInt32Value> FillColors = new Dictionary<string, UInt32Value>();
        public void Hatching(ref Array HatchingColorIndex, WorksheetPart worksheetPart, SpreadsheetDocument document)
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
                            Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)r + 1);
                            Cell cell = OpenXmlHelper.GetCell(row, r + 1, 2 + c);
                            OpenXmlHelper.FillCellForgroundColor(document, cell, rgb);
                            break;
                    }
                }
            }
            FillColors.Clear();
        }

        public void SetSignificanceTestMarking(SpreadsheetDocument document, Cell cell, string buf)
        {
            WorkbookStylesPart styles = document.WorkbookPart.WorkbookStylesPart;
            CellFormats cellFormats = styles.Stylesheet.CellFormats;
            CellFormat oldCellFormat = (CellFormat)cellFormats.ChildElements[(int)cell.StyleIndex.Value];
            NumberingFormats numberingFormats = styles.Stylesheet.NumberingFormats;
            uint formatId = numberingFormats.Descendants<NumberingFormat>().LastOrDefault().NumberFormatId.Value + 1;
            string formatCode = numberingFormats.Descendants<NumberingFormat>().Where(p => p.NumberFormatId == oldCellFormat.NumberFormatId).FirstOrDefault().FormatCode;
            string fmt = @"""" + buf + @"""" + formatCode;
            NumberingFormat numberingFormat = new NumberingFormat() { NumberFormatId = formatId, FormatCode = fmt };
            numberingFormats.Append(numberingFormat);
            numberingFormats.Count = numberingFormats.Count + 1;
            Alignment alignment = new Alignment() { Horizontal = oldCellFormat.Alignment.Horizontal, Vertical = oldCellFormat.Alignment.Vertical, WrapText = oldCellFormat.Alignment.WrapText };
            CellFormat newCellFormat = new CellFormat()
            {
                NumberFormatId = formatId,
                FontId = oldCellFormat.FontId,
                FillId = oldCellFormat.FillId,
                BorderId = oldCellFormat.BorderId,
                FormatId = oldCellFormat.FormatId,
                ApplyNumberFormat = oldCellFormat.ApplyNumberFormat,
                ApplyFill = oldCellFormat.ApplyFill,
                ApplyBorder = oldCellFormat.ApplyBorder,
                ApplyAlignment = oldCellFormat.ApplyAlignment
            };
            newCellFormat.Append(alignment);
            cellFormats.Append(newCellFormat);
            cellFormats.Count = cellFormats.Count + 1;
            cell.StyleIndex = cellFormats.Count - 1;
        }
        private void CreateNewSheet(ref SpreadsheetDocument SummaryDocument
             , CrossTable Table
             , ref string NewSheetName
             , ref string ContentsSheet
             , ref Array ContentsValue  //string 
             , ref Array HyperlinkTargetCells  //Range 
             , ref List<string> OrgSheets
             , bool IsSigTest = false
             , TableType TableType = 0
             , int shtNumber = 0
             )
        {
            int MAX_SHEETS_COUNT = 250;
            int MAX_SHEET_NAME_LENGTH = 31;
            int MaxAxesCount;
            string wb = null;
            string TemplateSheet;
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
                tmp = (MaxAxesCount == 2 ? "Triple" : "Double");
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
                MaxIndex = 1;
            }

            string[] values = Table.Question.SummaryTableName.Split('&');
            n = (0 != shtNumber ? shtNumber.ToString() : "") + "【" + values[2] + "】";
            TemplateSheet = TempSheetName;

            i = 1;
            while (n.Length <= MAX_SHEET_NAME_LENGTH)
            {
                try
                {
                    NewSheetName = n;
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

                    }
                    catch (Exception)
                    { }
                } while (sh != null);
                NewSheetName = n;
            }

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
                CreateNewBook(ref SummaryDocument, TempSheetName, NewSheetName, ref ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, ref OrgSheets, MinIndex, MaxIndex, TableType);
            }
            else
            {
                CreateSheet(ref SummaryDocument, TemplateSheet, NewSheetName, TableType);
            }

            ReportTitle = CurrentOutput.ParentRequest.Title;
            header = OutputUtil.GetAdjustedHeader(ReportTitle);
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

        private void CreateNewBook(
              ref SpreadsheetDocument SummaryDocument, string TempSheetName, string NewSheetName
            , ref string ContentsSheet
            , ref Array ContentsValue  //string 
            , ref Array HyperlinkTargetCells  //Range 
            , ref List<string> OrgSheets
            , int MinIndex
            , int MaxIndex
            , TableType TableType
            )
        {
            string wb;
            string tmp;
            int[] styleIndexArray = new int[] { 77, 138, 139, 140, 76, 141, 142, 143, 75, 74, 73, 72, 134, 144, 144, 136, 135, 146, 146, 137, 71, 70, 70, 69
                                                ,66,149,149,65,64,133,133,63,208,209,209,211,212,213,213,214,215,216,216,217};
            ContentsSheet = "INDEX";
            ContentsSheet = LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME;
            if (TableType == TableType.N)
                Summary_n.GenerateWorkbookPart(SummaryDocument.AddWorkbookPart(), TempSheetName, NewSheetName);
            if (TableType == TableType.Per)
                Summary_p.GenerateWorkbookPart(SummaryDocument.AddWorkbookPart(), TempSheetName, NewSheetName);
            if (TableType == TableType.SignificanceTest)
                Summary_ps.GenerateWorkbookPart(SummaryDocument.AddWorkbookPart(), TempSheetName, NewSheetName);
            WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(SummaryDocument, ContentsSheet);
            AdjustContentsSheet(worksheetPart, ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, TableType, styleIndexArray, MinIndex, MaxIndex);
        }

        private void CreateSheet(ref SpreadsheetDocument SummaryDocument, string sheetName, string NewSheetName, TableType tableType)
        {
            WorkbookPart workbookPart = SummaryDocument.WorkbookPart;
            Sheets sheets = workbookPart.Workbook.Sheets;
            Sheet lastSheet = sheets.Descendants<Sheet>().LastOrDefault();
            uint sheetId = lastSheet.SheetId.Value + 1;
            string id = "rId" + (Convert.ToInt32(Regex.Match(lastSheet.Id.Value, @"\d+").Value) + 1);
            Sheet sheet = new Sheet() { Name = NewSheetName, SheetId = sheetId, Id = id };
            sheets.Append(sheet);
            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>(id);

            switch (sheetName)
            {
                case "TripleStandard":
                    if (tableType == TableType.N)
                        Summary_n.GenerateWorksheetTripleStandard(worksheetPart);
                    if (tableType == TableType.Per)
                        Summary_p.GenerateWorksheetTripleStandard(worksheetPart);
                    if (tableType == TableType.SignificanceTest)
                        Summary_ps.GenerateWorksheetTripleStandard(worksheetPart);
                    break;
                case "DoubleStandard":
                    if (tableType == TableType.N)
                        Summary_n.GenerateWorksheetDoubleStandard(worksheetPart);
                    if (tableType == TableType.Per)
                        Summary_p.GenerateWorksheetDoubleStandard(worksheetPart);
                    if (tableType == TableType.SignificanceTest)
                        Summary_ps.GenerateWorksheetDoubleStandard(worksheetPart);
                    break;
                case "TripleSignificanceTest":
                    if (tableType == TableType.N)
                        Summary_n.GenerateWorksheetTripleSignificanceTest(worksheetPart);
                    if (tableType == TableType.Per)
                        Summary_p.GenerateWorksheetTripleSignificanceTest(worksheetPart);
                    if (tableType == TableType.SignificanceTest)
                        Summary_ps.GenerateWorksheetTripleSignificanceTest(worksheetPart);
                    break;
                case "DoubleSignificanceTest":
                    if (tableType == TableType.N)
                        Summary_n.GenerateWorksheetDoubleSignificanceTest(worksheetPart);
                    if (tableType == TableType.Per)
                        Summary_p.GenerateWorksheetDoubleSignificanceTest(worksheetPart);
                    if (tableType == TableType.SignificanceTest)
                        Summary_ps.GenerateWorksheetDoubleSignificanceTest(worksheetPart);
                    break;
            }
        }

        public bool GetHasWeight(CrossTable Table)
        {
            return false;
        }
        private void updateProgress(double currentProgress, string v)
        {
            if (null != QC)
            {
                QC.updateProgress(currentProgress, v);
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

            r = 2; // include wt
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
              , ref bool CheckOverRow
              , Hashtable WholeRowCol
              , ref int OverRowsCount
              , ref int OverColumnsCount
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
            DataValue = Array.CreateInstance(typeof(object), new int[] { RowsCount - (CaptionRowsCount + DataOffsetRow + 1) + 1, ColumnsCount - (DataOffsetColumn + (ToInt(IsSigTest) & 1) + 1) + 1 },
                new int[] { CaptionRowsCount + DataOffsetRow + 1, DataOffsetColumn + (ToInt(IsSigTest) & 1) + 1 });
            Ranking = Array.CreateInstance(typeof(int), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 }, new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });

            HatchingColorIndex = Array.CreateInstance(typeof(int?), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 },
                new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });

            ArrowEnd = Array.CreateInstance(typeof(object), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 },
                new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });

            SigTestMarking = Array.CreateInstance(typeof(string), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 },
                new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });

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
                                        buf = tmpTableValue[y, x];
                                        if (buf != null && OutputUtil.IsNumeric(buf))
                                        {
                                            DataValue.SetValue(tmpPercentValue[y, x], r, c);
                                        }
                                        else
                                        {
                                            DataValue.SetValue(buf, r, c);
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
                                                    clr = IsMarkingColoringLevel2High ? tmpLevel1HighColorIndex : tmpLevel2HighColorIndex;
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
                                                    clr = IsMarkingColoringLevel2Low ? tmpLevel1LowColorIndex : tmpLevel2LowColorIndex;
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
            for (int i = SigTestMarking.GetLowerBound(0); i <= SigTestMarking.GetUpperBound(0); i++)
            {
                for (int j = SigTestMarking.GetLowerBound(1); j <= SigTestMarking.GetUpperBound(1); j++)
                {
                    switch (SigTestMarking.GetValue(i, j))
                    {
                        case "▲":
                            SigTestMarking.SetValue(LocalResource.MARKING_SYMBOL_3PLUS, i, j);
                            break;
                        case "▼":
                            SigTestMarking.SetValue(LocalResource.MARKING_SYMBOL_3MINUZ, i, j);
                            break;
                        case "△":
                            SigTestMarking.SetValue(LocalResource.MARKING_SYMBOL_2PLUS, i, j);
                            break;
                        case "▽":
                            SigTestMarking.SetValue(LocalResource.MARKING_SYMBOL_2MINUZ, i, j);
                            break;
                        case "∴":
                            SigTestMarking.SetValue(LocalResource.MARKING_SYMBOL_1PLUS, i, j);
                            break;
                        case "∵":
                            SigTestMarking.SetValue(LocalResource.MARKING_SYMBOL_1MINUZ, i, j);
                            break;
                    }
                }
            }
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
        public void FormatLandscapeTable(ref SpreadsheetDocument SummaryDocument, WorksheetPart worksheetPart, int maxAxisCnt, CrossTable Table
             , string TemplateSheet, ref int RowNum, int numDigit, string summaryType
             , Hashtable CutRowsCol, Hashtable CutColumnsCol, int CutColumn
             , string FormatSheet, string FormatRangeNamePrefix
             , TableType TableType, bool HasWeight
             , string StartCell, bool isN
             , ref int rowCount
             , string ContentsSheet = null
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
            int colVal = 0;
            int TableRowCount = 0;
            Array tmpBuf; //string
            string tmpAddress;
            bool f = false;
            bool IsSigTest = false;
            TableType tType;
            int dd = 0;
            string tmpHeaderRange;
            string rng = null;
            SummaryNPFormat summaryNPFormat = new SummaryNPFormat();
            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            HasNAColumn = CurrentOutput.ShowNAAtItem;
            HasIVColumn = CurrentOutput.ShowIVAtItem;
            HasNARow = CurrentOutput.ShowNAAtAxis;
            HasIVRow = CurrentOutput.ShowIVAtAxis;
            d = 1 + (ToInt(!isN & TableType == TableType.NPer) & 1);
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
                if (CutMedian)
                {
                    CutMedian = IsReport && (MedIdx >= Table.GetTableValueColumnIndexMinimum);
                }
            }
            else
            {
                CutMedian = false;
                ItemSectorsCount = Table.SectorsCount;

                if (FormatSheet == "N_Std" || FormatSheet == "P_Std")
                {
                    if (FormatRangeNamePrefix == "SA_MA")
                        c = SA_MA_STD_LCol - CutColumn;
                    else if (FormatRangeNamePrefix == "SA_MA_WT")
                        c = SA_MA_WT_STD_LCol - CutColumn;
                    else if (FormatRangeNamePrefix == "N")
                        c = N_STD_LCol - CutColumn;
                }
                else if (FormatSheet == "N_Sig" || FormatSheet == "P_Sig")
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
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2) - (ToInt(HasIVColumn) & 1)))
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
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2)))
                    {
                        CutIVColumn = true;
                    }
                    else if (c + ItemSectorsCount + (ToInt(HasNAColumn && !CutNAColumn) & 1) + 1 - 1 > MaxColumnsCount)
                    {
                        CutIVColumn = true;
                        CutWTColumns = HasWeight;
                    }
                }
                if (HasWeight && !CutWTColumns)
                {
                    if (c + ItemSectorsCount + (ToInt(HasNAColumn && !CutNAColumn) & 1) + (ToInt(HasIVColumn && !CutIVColumn) & 1) + 2 - 1 > MaxColumnsCount)
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
            if (FormatSheet == "N_Std")
            {
                summaryNPFormat.CreateNperTable(CurrentOutput, worksheetPart, ref RowNum, maxAxisCnt, CurrentOutputShowPreWBTotal, CutNAColumn, CutIVColumn,
                                                 ItemSectorsCount, Table, numDigit, CutRowsCol);
            }
            else if (FormatSheet == "P_Std")
            {
                summaryNPFormat.CreatePerTable(CurrentOutput, worksheetPart, ref RowNum, maxAxisCnt, CurrentOutputShowPreWBTotal, CutNAColumn, CutIVColumn,
                                                ItemSectorsCount, Table, numDigit, summaryType, CutRowsCol);
            }
            else if (FormatSheet == "P_Sig")
            {
                summaryNPFormat.CreateSigTable(CurrentOutput, worksheetPart, ref RowNum, maxAxisCnt, CurrentOutputShowPreWBTotal, CutNAColumn, CutIVColumn,
                                                ItemSectorsCount, Table, numDigit, summaryType, CutRowsCol);
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

                    if (isTotalTable)
                    {
                        tmpBuf.SetValue(TOTAL_TABLE_NAME, 1, 0);
                        Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)fRow);
                        Cell cell = OpenXmlHelper.GetCell(row, fRow, 1);
                        object val = tmpBuf.GetValue(1, 0);
                        CellValue data = new CellValue((string)val)
                        {

                            Space = SpaceProcessingModeValues.Preserve
                        };
                        cell.CellValue = data;
                        cell.DataType = CellValues.String;
                    }
                    else
                    {
                        tmpBuf.SetValue(LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME, TableRowCount, 0);
                        Hyperlinks hyperlinks = worksheetPart.Worksheet.Descendants<Hyperlinks>().FirstOrDefault();
                        Hyperlink hyperlink = new Hyperlink() { Reference = "A1:A" + TableRowCount, Location = "\'INDEX\'!$A$1", Display = "\'INDEX\'!$A$1" };
                        hyperlinks.Append(hyperlink);
                        Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)lRow - 1);
                        Cell cell = OpenXmlHelper.GetCell(row, lRow - 1, 1);
                        object val = tmpBuf.GetValue(lRow - 1, 0);
                        CellValue data = new CellValue((string)val)
                        {
                            Space = SpaceProcessingModeValues.Preserve
                        };
                        cell.CellValue = data;
                        cell.DataType = CellValues.String;
                    }
                }
            }
        }
        public void AdjustContentsSheet(WorksheetPart worksheetPart, string ContentsSheet
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
            int rowNum = 5;
            int startCell;
            int styleIndex = 0;
            bool multiShape = false;

            MergeCells mergeCells = new MergeCells();
            MergeCell mergeCell = null;
            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tmpTable = (CrossTable)CurrentOutput.Tables[0];
            SummaryNPFormat summaryNPFormat = new SummaryNPFormat();
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
            SheetDimension sheetDimension = worksheetPart.Worksheet.Elements<SheetDimension>().First();
            SheetViews sheetViews = new SheetViews();
            DrawingsPart drawingsPart = worksheetPart.AddNewPart<DrawingsPart>("rId2");

            Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D };
            Row row2 = new Row() { RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D };
            Row row3 = new Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D };
            Row row4 = new Row() { RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D };
            Row row5 = new Row() { RowIndex = (UInt32Value)5U, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 11.25D };
            sheetData.Append(row1);
            sheetData.Append(row2);
            sheetData.Append(row3);
            sheetData.Append(row4);
            sheetData.Append(row5);

            summaryNPFormat.GenerateTitleBox(drawingsPart, CurrentOutput.ParentRequest.Title);
            if (tmpTable.KeyItem != null)
            {
                rowNum++;
                v = new string[2, 2];
                v.SetValue(LocalResource.REPORT_CLASSIFICATION_ITEM_KEYWORD, 0, 0);
                v.SetValue(LocalResource.REPORT_SECTOR_KEYWORD, 1, 0);
                KeyItemInformation WithtmpTable = tmpTable.KeyItem;
                v.SetValue(WithtmpTable.Name + ":" + WithtmpTable.Description, 0, 1);
                v.SetValue(WithtmpTable.SectorNumber + ":" + WithtmpTable.SectorDescription, 1, 1);

                while (rowNum <= 7)
                {
                    Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                    startCell = 2;
                    while (startCell <= 5)
                    {
                        Cell cell62 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex] };
                        row6.Append(cell62);
                        startCell++; styleIndex++;
                    }
                    mergeCell = new MergeCell() { Reference = "C" + rowNum + ":E" + rowNum };
                    mergeCells.Append(mergeCell);
                    rowNum++;
                    sheetData.Append(row6);
                }

                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = 6D, CustomHeight = true };
                sheetData.Append(row8);
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
                    summaryNPFormat.GenerateSignificanceTestLegend(drawingsPart, val, rowNum);
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
                    summaryNPFormat.GenerateSignificanceTestLegend(drawingsPart, val, rowNum);
                }
            }
            if (CurrentOutput.MarkingColoring && !IsSigTest)
            {
                cnt++;
                summaryNPFormat.GenerateMarkingColoring(drawingsPart, CurrentOutput, LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_CAPTION,
                                                        rowNum, multiShape);
            }
            string srcRowOfst = null, srcRow = null, dstRowOfst = null, dstRow = null, minBaseRowOfst = null, minBaseRow = null;
            string srcColOfst, srcCol, dstColOfst, dstCol;
            if (CurrentOutput.MarkingRanking)
            {
                if (cnt == 1)
                {
                    srcCol = "3"; srcColOfst = "1143000"; srcRow = rowNum.ToString(); srcRowOfst = "0";
                    dstCol = "3"; dstColOfst = "1838325"; dstRow = (rowNum + 1).ToString(); dstRowOfst = "0";
                }
                else
                {
                    srcCol = "3"; srcColOfst = "76200"; srcRow = rowNum.ToString(); srcRowOfst = "0";
                    dstCol = "3"; dstColOfst = "771525"; dstRow = (rowNum + 1).ToString(); dstRowOfst = "0";
                }
                DrawingPart.GenerateMarkingRanking(drawingsPart, srcRowOfst, srcRow, dstRowOfst, dstRow, srcColOfst, srcCol, dstColOfst, dstCol);
                cnt++;
            }
            rowNum++;
            buf = CurrentOutput.LocalizedFilteringExpression;
            if (buf.Length != 0)
            {
                v = new string[2];
                v.SetValue(LocalResource.REPORT_FILTER_CRITERION_KEYWORD, 0);
                v.SetValue(buf, 1);
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = 77D, CustomHeight = true };
                Cell cell91 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (uint)styleIndexArray[8], CellValue = new CellValue((string)v.GetValue(0)), DataType = CellValues.String };
                row9.Append(cell91);
                Cell cell92 = new Cell() { CellReference = "C" + rowNum, StyleIndex = (uint)styleIndexArray[9], CellValue = new CellValue((string)v.GetValue(1)), DataType = CellValues.String };
                row9.Append(cell92);
                sheetData.Append(row9);
            }
            else
            {
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = 77D, CustomHeight = true };
                sheetData.Append(row9);
            }

            rowNum++;
            Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
            Cell cell101 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (uint)styleIndexArray[10], DataType = CellValues.String };
            row10.Append(cell101);
            Cell cell102 = new Cell() { CellReference = "E" + rowNum, StyleIndex = (uint)styleIndexArray[11], DataType = CellValues.String };
            row10.Append(cell102);
            sheetData.Append(row10);

            if (CurrentOutput.WBOn)
            {
                string wb = LocalResource.WEIGHT_BACK;
                string msg = wb.Insert(wb.Length - 1, "[" + tmpTable.Question.WBValue + "]") + '\u2009'
                                + (tmpTable.Question.TabulateFullQuantity ? LocalResource.WB_TOTAL_NUMBER_BASE : string.Empty);
                Cell cell = SummaryHelper.GetCell(worksheetPart, rowNum, 2);
                CellValue data = new CellValue(msg)
                {
                    Space = SpaceProcessingModeValues.Preserve
                };
                cell.CellValue = data;
            }
            else if (tmpTable.Question.TabulateFullQuantity)
            {
                string msg = LocalResource.WB_TOTAL_NUMBER_BASE;
                Cell cell = SummaryHelper.GetCell(worksheetPart, rowNum, 2);
                CellValue data = new CellValue(msg)
                {
                    Space = SpaceProcessingModeValues.Preserve
                };
                cell.CellValue = data;
            }

            if (CurrentOutput.MarkingRanking || CurrentOutput.MarkingColoring || CurrentOutput.MarkingSignificance ||
                    CurrentOutput.MarkingAscending || IsSigTest)
            {
                if (CurrentOutput.MinSamplesCountOnMarking >= 0)
                {
                    string wbString = "";
                    if (CurrentOutput.WBOn)
                    {
                        wbString = LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_BEFORE_WB;
                    }
                    string msg = string.Format(LocalResource.REPORT_MARKING_LEGEND_MIN_BASE_PROMPT,
                        wbString, CurrentOutput.MinSamplesCountOnMarking.ToString());
                    Cell cell = SummaryHelper.GetCell(worksheetPart, rowNum, 5);
                    CellValue data = new CellValue(msg)
                    {
                        Space = SpaceProcessingModeValues.Preserve
                    };
                    cell.CellValue = data;
                }
            }

            rowNum++; styleIndex = 12; int limit = rowNum + 1;
            while (rowNum <= limit)
            {
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" } };
                startCell = 2;
                while (startCell <= 5)
                {
                    Cell cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                    row11.Append(cell112);
                    styleIndex++; startCell++;
                }
                sheetData.Append(row11);
                rowNum++;
            }
            OpenXmlHelper.GetColumn(worksheetPart, 5).Width = 11;
            mergeCell = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(2) + (limit - 1) + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(2) + limit };
            mergeCells.Append(mergeCell);
            mergeCell = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(5) + (limit - 1) + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(5) + limit };
            mergeCells.Append(mergeCell);
            mergeCell = new MergeCell() { Reference = "C" + (limit - 1) + ":" + "C" + limit };
            mergeCells.Append(mergeCell);
            mergeCell = new MergeCell() { Reference = "D" + (limit - 1) + ":" + "D" + limit };
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
                Cell cell = SummaryHelper.GetCell(worksheetPart, limit - 1, col);
                CellValue data = new CellValue((string)v.GetValue((col - 1)))
                {
                    Space = SpaceProcessingModeValues.Preserve
                };
                cell.CellValue = data;
                //cell.CellValue = new CellValue((string)v.GetValue((col - 1)));
            }

            startCell = 2;
            Row row13 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:7" }, Height = 3D, CustomHeight = true };
            while (startCell <= 5)
            {
                Cell cell132 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                row13.Append(cell132);
                styleIndex++; startCell++;
            }
            sheetData.Append(row13);

            int numRows = n;
            int cnts = 1, styIdx;
            startCell = 2; rowNum++; limit = rowNum;

            if (numRows > 1)
            {
                styIdx = IsSigTest ? 40 : 36;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 22.5D, CustomHeight = true };
                while (startCell <= 5)
                {
                    Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styIdx], DataType = CellValues.String };
                    row11.Append(cell); styIdx++; startCell++;
                }
                startCell = 2; rowNum++; cnts++;
                sheetData.Append(row11);
                while (cnts < numRows)
                {
                    styIdx = styleIndex;
                    Row row14 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 22.5D, CustomHeight = true };
                    while (startCell <= 5)
                    {
                        Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styIdx], DataType = CellValues.String };
                        row14.Append(cell); styIdx++; startCell++;
                    }
                    startCell = 2; rowNum++; cnts++;
                    sheetData.Append(row14);
                }
                styleIndex += 4;
                Row row15 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 22.5D, CustomHeight = true };
                while (startCell <= 5)
                {
                    Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                    row15.Append(cell); styleIndex++; startCell++;
                }
                sheetData.Append(row15);
            }
            else
            {
                styleIndex += IsSigTest ? 11 : 8;
                startCell = 2;
                Row row15 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:5" }, Height = 22.5D, CustomHeight = true };
                if (!IsSigTest)
                {
                    while (startCell <= 5)
                    {
                        Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex], DataType = CellValues.String };
                        row15.Append(cell); styleIndex++; startCell++;
                    }
                }
                else
                {
                    int[] styleIndexArray1 = new int[] { 211, 133, 133, 63 };
                    int val = 0;
                    while (startCell <= 5)
                    {
                        Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray1[val], DataType = CellValues.String };
                        row15.Append(cell); styleIndex++; startCell++; val++;
                    }
                }
                sheetData.Append(row15);
            }
            SheetView sheetView = new SheetView() { ShowGridLines = false, TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            Pane pane = new Pane() { VerticalSplit = limit - 1, TopLeftCell = "A" + limit, ActivePane = PaneValues.BottomLeft, State = PaneStateValues.Frozen };
            Selection selection = new Selection() { Pane = PaneValues.BottomLeft, ActiveCell = "A1", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1" } };
            sheetView.Append(pane);
            sheetView.Append(selection);
            sheetViews.Append(sheetView);
            worksheetPart.Worksheet.InsertAfter(sheetViews, sheetDimension);
            worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }
        public void AdjustFormat(
   string FormatSheet, string SigTestFormatSheet
         , int MaxAxesCount, ref int CutColumn, bool HasWeightColumn, bool OnlyCutTriple = false, bool ExtendRowHeight = false)
        {
            string[] tmpNamesArray;
            string tmpName;
            int tmp;
            string Suffix;
            string tmpSuffix;
            bool IsPortrait;
            int i;
            bool RedrawBorder;
            IsPortrait = CurrentOutput.Orientation == TableOrientation.Portrait;

            if (MaxAxesCount == 1 && !IsPortrait)
            {
                CutColumn++;
            }
            if (OnlyCutTriple) { return; }
            //' フォーマットシートのWB前全体列またはWB前全体行の削除
            if (!CurrentOutputShowPreWBTotal && !IsPortrait)
            {
                CutColumn++;
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
        }
        public void PutContents(WorksheetPart worksheetPart,
               ref Array ContentsValue
             , ref Array HyperlinkTargetCells, int row //Excel.Range 
             , List<Worksheet> OrgSheets = null)
        {
            int i, j;
            int r = 0;
            Hyperlinks hyperlinks = new Hyperlinks();
            SummaryHelper.PutValue(worksheetPart, row, 2, ref ContentsValue);

            for (i = HyperlinkTargetCells.GetLowerBound(0); i <= HyperlinkTargetCells.GetUpperBound(0); i++)
            {
                r = r + 1;
                for (j = HyperlinkTargetCells.GetLowerBound(1); j <= HyperlinkTargetCells.GetUpperBound(1); j++)
                {
                    if (HyperlinkTargetCells.GetValue(i, j) != null)
                    {
                        Hyperlink hyperlink = new Hyperlink() { Reference = "E" + (row + i), Location = (string)HyperlinkTargetCells.GetValue(i, j) };
                        hyperlinks.Append(hyperlink);
                    }
                }
            }
            var mergeCells = worksheetPart.Worksheet.Descendants<MergeCells>().FirstOrDefault();
            worksheetPart.Worksheet.InsertAfter(hyperlinks, mergeCells);
        }
        private void NumberFormat(SpreadsheetDocument document, int[] styleIndexArray,
        string FormatSheet, string SigTestFormatSheet
             , string[] NamesArray, int NumDigitsAfterDecimal
             , string Suffix = null, bool IsWeight = false)
        {
            WorkbookStylesPart workbookStylesPart = document.WorkbookPart.WorkbookStylesPart;
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

            foreach (int styleIdx in styleIndexArray)
            {
                SummaryHelper.SetNumberFormat(workbookStylesPart, styleIdx, fmt);
            }
        }

        public static void ArrayPreserve(ref Array v, Type type, int u)
        {
            Array t = (Array)v.Clone();
            v = Array.CreateInstance(type, u + 1);
            t.CopyTo(v, 0);
        }
        public static int ToInt(bool test)
        {
            return test ? -1 : 0;
        }
    }
}
