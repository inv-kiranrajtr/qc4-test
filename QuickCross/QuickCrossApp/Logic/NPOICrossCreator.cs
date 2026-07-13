using log4net;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Dml.Spreadsheet;
using NPOI.SS;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;
using NPOI.Util;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using QC4Common.Classes.HatchColor;
using Qc4Launcher.Logic.DPCheckList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Common.Constants;
using OfficeOpenXml.Style;
using Qc4Launcher.Logic.CombineBanner;

namespace Qc4Launcher.Logic
{
    class NPOICrossCreator
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ExecuteStaticMethod ExecuteStaticMethod = new ExecuteStaticMethod();
        public OutputCross CurrentOutput = null;
        CellRangeAddress insertValuerangeAddress = null;
        public int StdNumericCutCol = 0;
        public int SigNumericCutCol = 0;
        public int CutColoums = 0;
        public int flag = 0;
        public string ThisLocationCode;
        public int numberOfHyperLink = 0;
        public double progressAvailable;
        public double currentProgress;
        private string OutputDirectoryPath;
        private string prfix;
        public string TemplateDirectoryPath;
        public bool onlySigPage;
        private bool checkCross = false;
        private bool checkList = false;
        public string BookPSWD;
        public string SheetPSWD;
        public CrossTabulationQC QC;
        public CheckCrossQC CQC;
        public Microsoft.Office.Interop.Excel.Application xlApp;
        public BackgroundWorker bgWorker;
        public List<string> outputFiles;
        public Array hyperlinks;
        public static string TEMPLATE_NAME = "Cross.xls";
        public static string TRANSPOSE_TEMPLATE_NAME = "CrossPortrait.xls";
        public static string CHECK_CROSS_SHEET = "Check Cross";
        public static string CELL_RANGE_MULTI = "B3:R3";
        public static string CELL_RANGE_SINGLE_3 = "C3:R3";
        public static string CELL_RANGE_SINGLE_4 = "D4:R4";
        public static string CELL_TEMP_RANGE_1 = "B7:C8";
        public static string CELL_TEMP_RANGE_2 = "B9:B12";
        public static string INDIVIDUAL_TEMPLATE_NAME = "CrossNP.xlt";
        public static string TRANSPOSE_INDIVIDUAL_TEMPLATE_NAME = "CrossNPPortrait.xlt";
        public static string INDIVIDUAL_TEMPLATE_NAME_NP = "Cross_np.xltx";
        public static string INDIVIDUAL_TEMPLATE_NAME_N = "Cross_n.xltx";
        public static string INDIVIDUAL_TEMPLATE_NAME_P = "Cross_p.xltx";
        public static string INDIVIDUAL_TEMPLATE_NAME_T = "Cross_ps.xltx";
        public static string INDIVIDUAL_FORMAT_TEMPLATE_NAME = "CrossNPFormat.xlsx";
        public static string TRANSPOSE_INDIVIDUAL_FORMAT_TEMPLATE_NAME = "CrossNPPortraitFormat.xlsx";
        public static string TRANSPOSE_INDIVIDUAL_FORMAT_TEMPLATE_NAME_MS_2013 = "CrossNPPortraitFormat_ms2013.xlsx";
        public static int XLSX_MAX_ROW = 1048576;
        public static int XLSX_MAX_COUMN = 16384;
        public static bool Tripple = false;
        public static bool ShouldCombineBanners { get; set; } = false;

        public IDictionary<XSSFCellStyle, XSSFCellStyle> quotedStyles = new Dictionary<XSSFCellStyle, XSSFCellStyle>(); // to do : style hashcode not considering workbook
        public IDictionary<string, IDictionary<XSSFCellStyle, XSSFCellStyle>> hatchedStyles = new Dictionary<string, IDictionary<XSSFCellStyle, XSSFCellStyle>>();
        public IDictionary<string, IDictionary<XSSFCellStyle, XSSFCellStyle>> sigTestStyles = new Dictionary<string, IDictionary<XSSFCellStyle, XSSFCellStyle>>();

        public void CreateCross(Output Output, string bookPSWD, string sheetPSWD, string outputDirectoryPath,
            string templateDirectoryPath, Microsoft.Office.Interop.Excel.Application xlAppG, BackgroundWorker bgWorker, DoWorkEventArgs bgWorkerArg,
            bool onlySigPageP = false, bool checkCrossP = false,
            bool checkListP = false, CrossTabulationQC QC = null, double progressAvailable = 0,
            double currentProgress = 0, CheckCrossQC CQC = null, string qc4FileName = null, List<string> outputFiles = null, bool combineBanners = false)
        {
            XlFileFormat xlFmt = XlFileFormat.xlOpenXMLWorkbook;
            Reportset reportset = (Reportset)Output.ParentReportset;
            CurrentOutput = (OutputCross)Output;
            BookPSWD = bookPSWD;
            SheetPSWD = sheetPSWD;
            OutputDirectoryPath = outputDirectoryPath;
            prfix = getPrefix(qc4FileName);
            TemplateDirectoryPath = templateDirectoryPath;
            this.progressAvailable = progressAvailable;
            this.currentProgress = currentProgress;
            this.QC = QC;
            this.CQC = CQC;
            ShouldCombineBanners = combineBanners;
            XSSFWorkbook formatBook = null;
            XSSFWorkbook baseBook = null;
            onlySigPage = onlySigPageP;
            xlApp = xlAppG;
            checkCross = checkCrossP;
            checkList = checkListP;
            this.bgWorker = bgWorker;
            if (outputFiles == null) outputFiles = new List<string>();
            this.outputFiles = outputFiles;

            string TempPath = TemplatePath(ref xlFmt, CurrentOutput.Orientation);
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

            if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi && !onlySigPage)
            {
                CreateStandardCross(TempPath, formatBook, xlFmt, filenameSuffix);
            }
            else
            {
                string FormatPath = FormatTemplatePath(CurrentOutput.Orientation);
                CreateIndividualCross(FormatPath, TempPath, xlFmt, filenameSuffix);
            }
        }

        public static string getPrefix(string qc4FileName)
        {
            if (string.IsNullOrEmpty(qc4FileName))
            {
                return "";
            }
            string currDateTimeFmt = DateTime.Now.ToString("yyyyMMdd_HHmm");
            string[] fnme = qc4FileName.Split('_');
            return fnme[0] + "_" + currDateTimeFmt + "_";
        }

        public string FormatTemplatePath(TableOrientation Orientation = TableOrientation.Landscape)
        {
            string d;
            string n = "";
            if (Orientation != TableOrientation.Portrait) { Orientation = TableOrientation.Landscape; }
            if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                {
                    //    if (Orientation == TableOrientation.Landscape)
                    //    {
                    //        n = FORMAT_TEMPLATE_NAME;
                    //    }
                    //    else
                    //    {
                    //        n = TRANSPOSE_FORMAT_TEMPLATE_NAME;
                    //    }
                }
                else
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        n = INDIVIDUAL_FORMAT_TEMPLATE_NAME;
                    }
                    else
                    {
                        double version = Convert.ToDouble(xlApp.Version);
                        if (version >= 16.0)
                        {
                            n = TRANSPOSE_INDIVIDUAL_FORMAT_TEMPLATE_NAME;
                        }
                        else
                        {
                            n = TRANSPOSE_INDIVIDUAL_FORMAT_TEMPLATE_NAME_MS_2013;
                        }
                    }
                }
            }
            //else
            //{
            //    if (Orientation == TableOrientation.Landscape)
            //    {
            //        n = REPORT_FORMAT_TEMPLATE_NAME;
            //    }
            //    else
            //    {
            //        n = TRANSPOSE_REPORT_FORMAT_TEMPLATE_NAME;
            //    }
            //}
            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, xlApp.PathSeparator);
            return OutputUtil.BuildPath(d, n, xlApp.PathSeparator);
        }


        private void CreateStandardCross(string TempPath, XSSFWorkbook crossFormat, XlFileFormat FileFormat, string Suffix)
        {
            _log.Info("Create standard cross");

            try
            {

                int sheetIdx = 0;
                XSSFWorkbook FormatBook = null;
                //XSSFWorkbook newBook = null;
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

                Array ContentsValue = null;
                Array HyperlinkTargetCells = null; //Range
                Array HyperlinkTargetSheets = null; //string
                Array PageSetupContentsValue = null; //string
                Array PageSetupHyperlinkTargetCells = null; //range

                bool PageSetupOn;
                string PageSetupSheetBaseName = null;
                ISheet ContentsSheet = null;
                ISheet TemplateSheet = null;
                ISheet SigTestTemplateSheet = null;
                ISheet FormatSheet = null;
                ISheet SigTestFormatSheet = null;
                ISheet NPerSheet = null;
                ISheet PageSetupTemplateSheet = null;
                ISheet PageSetupSigTestTemplateSheet = null;
                ISheet NSheet = null;
                ISheet PerSheet = null;
                ISheet SigTestSheet = null;
                ISheet PageSetupContentsSheet;
                ISheet PageSetupNPerSheet = null;
                ISheet PageSetupNSheet = null;
                ISheet PageSetupPerSheet = null;
                ISheet PageSetupSigTestSheet = null;
                XSSFSheet SignificanceTestSheet = null;
                XSSFSheet StandardSheet = null;
                CellRangeAddress NPerStartCell = null;
                CellRangeAddress NStartCell = null;
                CellRangeAddress PerStartCell = null;
                CellRangeAddress SigTestStartCell = null;
                CellRangeAddress PageSetupNPerStartCell = null;
                CellRangeAddress PageSetupNStartCell = null;
                CellRangeAddress PageSetupPerStartCell = null;
                CellRangeAddress PageSetupSigTestStartCell = null;
                string tmp = null;
                int i;
                int flg = 0;
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

                //int ErrorsCount;
                using (FileStream file = new FileStream(TempPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    FormatBook = new XSSFWorkbook(file);
                }
                _log.Info("Template book added");
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
                //With newBook.Worksheets
                tmpCol.Add("INDEX", string.Empty);
                ContentsSheet = FormatBook.GetSheet("INDEX");
                AdjustContentsSheet(FormatBook, null, ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, ref HyperlinkTargetSheets, (TableType)(Convert.ToInt16(TableType.SignificanceTest) & ToInt(SigTestOn)));
                _log.Info("AdjustContentsSheet completed");
                if (CurrentOutput.Orientation == TableOrientation.Landscape)
                {
                    tmp = MaxAxesCount == 2 ? "Triple" : "double";
                    if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                    {
                        TemplateSheet = FormatBook.GetSheet(tmp + "Standard");
                        tmpCol.Add(TemplateSheet.SheetName, string.Empty);
                        FormatBook.SetSheetName(tmp == "Triple" ? 1 : 0, "Standard");
                        StandardSheet = (XSSFSheet)FormatBook.GetSheet("Standard");
                        tmpCol.Add("Standard", string.Empty);
                    }
                    if (SigTestOn)
                    {
                        SigTestTemplateSheet = FormatBook.GetSheet(tmp + "SigTest");
                        tmpCol.Add(SigTestTemplateSheet.SheetName, string.Empty);
                        FormatBook.SetSheetName(tmp == "Triple" ? 3 : 2, "SignificanceTest");
                        SignificanceTestSheet = (XSSFSheet)FormatBook.GetSheet("SignificanceTest");
                        tmpCol.Add("SignificanceTest", string.Empty);
                    }
                }
                else
                {
                    TemplateSheet = FormatBook.GetSheet("Template");
                    if (SigTestOn)
                    {
                        SigTestTemplateSheet = (XSSFSheet)FormatBook.GetSheet("Template");
                        //TemplateSheet.Copy(Type.Missing, newBookWShhets.Item[newBookWShhets.Count]);
                        //SigTestTemplateSheet = newBookWShhets.Item[newBookWShhets.Count];
                        SignificanceTestSheet = (XSSFSheet)FormatBook.GetSheet("SignificanceTest");
                        tmpCol.Add("SignificanceTest", string.Empty);
                        //tmpCol.Add(SigTestTemplateSheet.SheetName, string.Empty);
                    }

                    if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                    {
                        StandardSheet = (XSSFSheet)FormatBook.GetSheet("Standard");
                        tmpCol.Add("Standard", string.Empty);
                        tmpCol.Add(TemplateSheet.SheetName, string.Empty);
                        if (PageSetupTemplateSheet != null)
                        {
                            tmpCol.Add(PageSetupTemplateSheet.SheetName, string.Empty);
                        }
                    }
                    else
                    {
                        TemplateSheet = null;
                        PageSetupTemplateSheet = null;
                    }
                }

                while (sheetIdx < FormatBook.NumberOfSheets)
                {
                    if (!tmpCol.Contains(FormatBook.GetSheetName(sheetIdx)))
                    {
                        FormatBook.RemoveSheetAt(sheetIdx);
                    }
                    else
                    {
                        sheetIdx++;
                    }
                }

                if (SigTestOn == true)
                {
                    FormatBook.SetSheetOrder(FormatBook.GetSheetAt(3).SheetName, (FormatBook.NumberOfSheets - 1));
                }
                if (CurrentOutput.Orientation == TableOrientation.Landscape)
                    sheetIdx = SigTestOn == true ? 4 : 2;
                else
                    sheetIdx = SigTestOn == true ? 3 : 2;
                if (CurrentOutput.OutputNPerTable)
                {
                    if (!checkCross)
                    {
                        FormatBook.CloneSheet(FormatBook.GetSheetIndex(TemplateSheet));
                        FormatBook.SetSheetName(sheetIdx, LocalResource.REPORT_CROSS_NP_SHEET_NAME);
                    }
                    else
                    {
                        FormatBook.SetSheetName(FormatBook.GetSheetIndex(TemplateSheet), CHECK_CROSS_SHEET);
                    }
                    NPerStartCell = CellRangeAddress.ValueOf("A1");
                    NPerSheet = FormatBook.GetSheetAt(sheetIdx);
                    sheetIdx++;
                }

                if (CurrentOutput.OutputNTable)
                {
                    if (!checkCross)
                    {
                        FormatBook.CloneSheet(FormatBook.GetSheetIndex(TemplateSheet));
                        FormatBook.SetSheetName(sheetIdx, LocalResource.REPORT_CROSS_N_SHEET_NAME);
                    }
                    else
                    {
                        FormatBook.SetSheetName(FormatBook.GetSheetIndex(TemplateSheet), CHECK_CROSS_SHEET);
                    }
                    NStartCell = CellRangeAddress.ValueOf("A1");
                    NSheet = FormatBook.GetSheetAt(sheetIdx);
                    sheetIdx++;
                }

                if (CurrentOutput.OutputPerTable)
                {
                    if (!checkCross)
                    {
                        FormatBook.CloneSheet(FormatBook.GetSheetIndex(TemplateSheet));
                        FormatBook.SetSheetName(sheetIdx, LocalResource.REPORT_CROSS_P_SHEET_NAME);
                    }
                    else
                    {
                        FormatBook.SetSheetName(FormatBook.GetSheetIndex(TemplateSheet), CHECK_CROSS_SHEET);
                    }
                    PerStartCell = CellRangeAddress.ValueOf("A1");
                    PerSheet = FormatBook.GetSheetAt(sheetIdx);
                    sheetIdx++;
                }
                FormatBook.RemoveSheetAt((FormatBook.NumberOfSheets - 1));

                if (SigTestOn && CurrentOutput.Orientation == TableOrientation.Landscape)
                {
                    sheetIdx = FormatBook.NumberOfSheets - 1;
                    FormatBook.SetSheetOrder(FormatBook.GetSheetAt(3).SheetName, (FormatBook.NumberOfSheets - 1));
                    FormatBook.SetSheetName(sheetIdx, LocalResource.REPORT_CROSS_SIGNIFICANCE_TEST_SHEET_NAME);
                    SigTestSheet = FormatBook.GetSheetAt(sheetIdx);
                    SigTestStartCell = CellRangeAddress.ValueOf("A1");
                }
                else if (SigTestOn && CurrentOutput.Orientation == TableOrientation.Portrait)
                {
                    FormatBook.CloneSheet(FormatBook.GetSheetIndex(TemplateSheet));
                    FormatBook.SetSheetName(sheetIdx, LocalResource.REPORT_CROSS_SIGNIFICANCE_TEST_SHEET_NAME);
                    SigTestSheet = FormatBook.GetSheetAt(sheetIdx);
                    SigTestStartCell = CellRangeAddress.ValueOf("A1");
                }
                //NpoiHelper.saveTmp(FormatBook, true);
                AdjustFormat(FormatBook, StandardSheet, SignificanceTestSheet, MaxAxesCount, HasWeightColumn);
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
                numberOfHyperLink = 0;
                for (i = 0; i < CurrentOutput.Tables.Count; i++)
                {
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
                    if (checkSimpleAggr(tmpTable)
                        && !string.IsNullOrEmpty(tmpTable.Question.QNumber)
                        //&& !string.IsNullOrEmpty(tmpTable.Question.TableHeading)   Redmine ID : #211718 
                        && tmpTable.AxesGroups.Count > 1)
                    {
                        ContentsValue.SetValue(tmpTable.Question.QNumber, i + 1, 1);
                        ContentsValue.SetValue(tmpTable.Question.TableHeading, i + 1, 2);
                        ContentsValue.SetValue(tmpTable.Question.Description, i + 1, 3);
                    }
                    else
                    {
                        ContentsValue.SetValue(tmpTable.Question.Name, i + 1, 1);
                        ContentsValue.SetValue(tmpTable.Question.TableHeading, i + 1, 2);
                        ContentsValue.SetValue(tmpTable.Question.Description, i + 1, 3);
                    }
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
                            //Err().Raise vbObjectError + 100 &, RunningProcName, ThisWorkbook.LocalResource_ReportUnjustQuestionTypeMessageIndex)
                            break;
                    }
                    HasWeight = GetHasWeight(tmpTable);
                    if (SigTestOn) WholeRowCol = new Hashtable();
                    int medIdx = -1;
                    GetCutRowsAndColumns(tmpTable, HasShowPreWBTotal, HasWeight, MaxAxesCount, ref CutRowsCol, ref CutColumnsCol, ref medIdx, false, CutMedian, WholeRowCol);
                    _log.Info("GetCutRowsAndColumns completed");
                    _log.Info("OutputTable NP started");
                    OutputTable(FormatBook, StandardSheet, tmpTable, StdNumericCutCol, ref NPerStartCell, TableType.NPer, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                                  , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupNPerStartCell
                                  , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, FormatSheet, ref NPerSheet, ref PageSetupNPerSheet
                                    , ref ContentsValue, ref HyperlinkTargetCells, ref HyperlinkTargetSheets, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                                  , strIdx, TemplateSheet, ContentsSheet, HasWeightColumn, PageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref NPerRemainedRowsCount
                                  , DefLineHeight, CutMedian: CutMedian, medIdx: medIdx
                                  //,Errors, ErrorsCount, res
                                  );
                    //    //        DoEvents
                    //    //        ' N•\
                    _log.Info("OutputTable NP completed");
                    _log.Info("OutputTable N started");
                    if (bgWorker.CancellationPending) return;
                    OutputTable(FormatBook, StandardSheet, tmpTable, StdNumericCutCol, ref NStartCell, TableType.N, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                                      , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupNStartCell
                                      , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, FormatSheet, ref NSheet, ref PageSetupNSheet
                                        , ref ContentsValue, ref HyperlinkTargetCells, ref HyperlinkTargetSheets, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                                      , strIdx, TemplateSheet, ContentsSheet, HasWeightColumn, PageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref NRemainedRowsCount
                                      , DefLineHeight, CutMedian: CutMedian, medIdx: medIdx
                                      //, Errors, ErrorsCount, res
                                      );
                    //DoEvents
                    //        ' “•\
                    _log.Info("OutputTable N completed");
                    _log.Info("OutputTable Test started");
                    if (bgWorker.CancellationPending) return;
                    OutputTable(FormatBook, StandardSheet, tmpTable, StdNumericCutCol, ref PerStartCell, TableType.Per, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                             , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupPerStartCell
                             , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, FormatSheet, ref PerSheet, ref PageSetupPerSheet
                             , ref ContentsValue, ref HyperlinkTargetCells, ref HyperlinkTargetSheets, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                             , strIdx, TemplateSheet, ContentsSheet, HasWeightColumn, PageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref PerRemainedRowsCount
                             , DefLineHeight, CutMedian: CutMedian, medIdx: medIdx
                                   // , Errors, ErrorsCount, res
                                   );
                    //        DoEvents
                    //        ' ŒŸ’è•\
                    //if(SigTestOn == true)
                    _log.Info("OutputTable P completed");
                    _log.Info("OutputTable Test started");
                    if (bgWorker.CancellationPending) return;
                    OutputTable(FormatBook, SignificanceTestSheet, tmpTable, SigNumericCutCol, ref SigTestStartCell, TableType.SignificanceTest, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                                      , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupSigTestStartCell
                                      , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, SigTestFormatSheet, ref SigTestSheet, ref PageSetupSigTestSheet
                                        , ref ContentsValue, ref HyperlinkTargetCells, ref HyperlinkTargetSheets, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                                      , strIdx, SigTestTemplateSheet, ContentsSheet, HasWeightColumn, SigTestPageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref SigTestRemainedRowsCount
                                      , DefLineHeight
                                      //, Errors, ErrorsCount, res
                                      , WholeRowCol, CutMedian: CutMedian, medIdx: medIdx
                                      );
                    //        DoEvents
                    _log.Info("OutputTable Test completed");
                    if (bgWorker.CancellationPending) return;
                }
                PutContents(ContentsSheet as XSSFSheet, ref ContentsValue, ref HyperlinkTargetCells, ref HyperlinkTargetSheets);
                if (SigTestOn == true)
                {
                    FormatBook.RemoveSheetAt(0);
                    FormatBook.RemoveSheetAt(0);
                    //FormatBook.RemoveSheetAt(0);
                }
                else
                {
                    //FormatBook.RemoveSheetAt(0);
                    FormatBook.RemoveSheetAt(0);
                }

                if (OutputDirectoryPath != null)
                {
                    SaveBook(FormatBook, CurrentOutput.ParentReportset.DivName + CurrentOutput.ExcelBookNamePrefix, Suffix, FileFormat, isMultiCross: true);
                }
                else
                {
                    SaveWorkBook(FormatBook, Prefix: CurrentOutput.ExcelBookNamePrefix, isMultiCross: true);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
                throw ex;
            }
        }

        public void AdjustFormat(XSSFWorkbook FormatBook,
        ISheet FormatSheet, ISheet SigTestFormatSheet
              , int MaxAxesCount, bool HasWeightColumn, bool OnlyCutTriple = false, bool ExtendRowHeight = false)
        {
            string[] tmpNamesArray;
            string tmpName;
            int tmp;
            string Suffix;
            string tmpSuffix;
            string fmt;
            bool IsPortrait;
            int i;
            bool RedrawBorder;
            string XlDeleteShiftDirection = "left";
            CutColoums = 0;
            int wtCutCol = 0;
            int numericCutCol = 0;

            IsPortrait = CurrentOutput.Orientation == TableOrientation.Portrait;

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

            if (MaxAxesCount == 1)
            {
                if (IsPortrait)
                {
                    //if (FormatSheet != null)
                    //{
                    //    String area = GetNamedRange(FormatSheet as XSSFSheet, "TripleRows").RefersToFormula;
                    //    FormatBook.RemoveName(GetNamedRange(FormatSheet as XSSFSheet, "TripleRows"));
                    //    foreach (String tripleRowsArea in area.Split(',').Reverse())
                    //    {
                    //        CellRangeAddress range = CellRangeAddress.ValueOf(tripleRowsArea);
                    //        //NpoiHelper.ShiftRow(FormatSheet, range.FirstRow + 1, FormatSheet.LastRowNum, -1);
                    //        NpoiHelper.ShiftRowUp(FormatSheet as XSSFSheet, range.FirstRow + 1);
                    //        IRow row = FormatSheet.GetRow(range.FirstRow + 1);
                    //        for (int n = 1; n <= 3; n++)
                    //        {
                    //            CellRangeAddress cellRange = new CellRangeAddress(range.FirstRow - 1, range.FirstRow, row.LastCellNum - n, row.LastCellNum - n);
                    //            NpoiHelper.UnmergeSelectedRegion(FormatSheet as XSSFSheet, cellRange.FormatAsString());
                    //        }
                    //    }
                    //}
                    //if (ExtendRowHeight)
                    //{
                    //    //Range WithFormatSheetRange = FormatSheet.Range["DoubleRows"];
                    //    //WithFormatSheetRange.RowHeight = WithFormatSheetRange.RowHeight * 2;
                    //}
                }
            }
            //NpoiHelper.saveTmp(FormatBook,true);
            if (IsPortrait)
            {
                if (!CurrentOutput.ShowIVAtItem)
                {
                    //    NpoiHelper.saveTmp(FormatBook,true);
                    //    tmpSuffix = "_InvalidRow";
                    //    var arrayReverse = tmpNamesArray.Reverse();
                    //    foreach (String value in arrayReverse)
                    //    {
                    //        String rangeInvalidRow = GetNamedRange(FormatSheet as XSSFSheet,value + tmpSuffix).RefersToFormula;
                    //        int invLast = CellRangeAddress.ValueOf(rangeInvalidRow).LastRow;
                    //        int invFirst = CellRangeAddress.ValueOf(rangeInvalidRow).FirstRow;
                    //        int numberOfRows = CellRangeAddress.ValueOf(rangeInvalidRow).LastRow - CellRangeAddress.ValueOf(rangeInvalidRow).FirstRow + 1;
                    //        //NpoiHelper.RemoveNumberOfRows(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(rangeInvalidRow).LastRow + 1, numberOfRows);
                    //        NpoiHelper.ShiftRowUp(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(rangeInvalidRow).LastRow + 1,numberOfRows);
                    //        int a = FormatSheet.LastRowNum;
                    //        NpoiHelper.saveTmp(FormatBook,true);
                    //    }
                }

                if (!CurrentOutput.ShowNAAtItem)
                {
                    tmpSuffix = "_NoAnswerRow";
                    var arrayReverse = tmpNamesArray.Reverse();
                    String rangeNoAnswerRow;
                    foreach (String value in arrayReverse)
                    {
                        if (FormatSheet != null)
                        {
                            rangeNoAnswerRow = GetNamedRange(FormatSheet as XSSFSheet, value + tmpSuffix).RefersToFormula;
                            int maxRow = CellRangeAddress.ValueOf(rangeNoAnswerRow).LastRow - CellRangeAddress.ValueOf(rangeNoAnswerRow).FirstRow + 1;
                            if (value.Equals("N"))
                                FormatSheet.ShiftRows(CellRangeAddress.ValueOf(rangeNoAnswerRow).LastRow + 1, FormatSheet.LastRowNum + 2, -maxRow);
                            else
                                FormatSheet.ShiftRows(CellRangeAddress.ValueOf(rangeNoAnswerRow).LastRow + 1, FormatSheet.LastRowNum, -maxRow);
                        }

                        if (SigTestFormatSheet != null)
                        {
                            rangeNoAnswerRow = GetNamedRange(SigTestFormatSheet as XSSFSheet, value + tmpSuffix).RefersToFormula;
                            int maxRow = CellRangeAddress.ValueOf(rangeNoAnswerRow).LastRow - CellRangeAddress.ValueOf(rangeNoAnswerRow).FirstRow + 1;
                            if (value.Equals("N"))
                                SigTestFormatSheet.ShiftRows(CellRangeAddress.ValueOf(rangeNoAnswerRow).LastRow + 1, SigTestFormatSheet.LastRowNum + 2, -maxRow);
                            else
                                SigTestFormatSheet.ShiftRows(CellRangeAddress.ValueOf(rangeNoAnswerRow).LastRow + 1, SigTestFormatSheet.LastRowNum, -maxRow);
                        }
                    }
                }
            }
            else
            {
                if (!CurrentOutput.ShowIVAtItem)
                {
                    tmpSuffix = "_InvalidColumn";
                    //for (i = 0; i <= tmpNamesArray.GetUpperBound(0); i++)
                    //{
                    //    //NpoiHelper.deleteSingleColumnName(FormatSheet as XSSFSheet, tmpNamesArray[i] + tmpSuffix);
                    //    //DeleteName(FormatSheet, SigTestFormatSheet, 
                    //     //   tmpNamesArray[i] + tmpSuffix, RedrawBorder & !tmpNamesArray[i].EndsWith("_WT"));
                    //}
                }
                if (!CurrentOutput.ShowNAAtItem)
                {
                    tmpSuffix = "_NoAnswerColumn";
                    for (i = 0; i <= tmpNamesArray.GetUpperBound(0); i++)
                    {
                        if (FormatSheet != null)
                        {
                            XSSFName name = GetNamedRange(FormatSheet as XSSFSheet, tmpNamesArray[i] + tmpSuffix);
                            if (null == name) continue;
                            CellRangeAddress cellRangeAddressNumeric = CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpNamesArray[i] + tmpSuffix).RefersToFormula);
                            CellRangeAddress cellRange = new CellRangeAddress(cellRangeAddressNumeric.FirstRow, cellRangeAddressNumeric.LastRow, cellRangeAddressNumeric.FirstColumn - 1, cellRangeAddressNumeric.LastColumn - 1);
                            NpoiHelper.deleteSingleColumnName(FormatSheet as XSSFSheet, cellRange);
                        }
                        if (SigTestFormatSheet != null)
                        {
                            XSSFName name = GetNamedRange(SigTestFormatSheet as XSSFSheet, tmpNamesArray[i] + tmpSuffix);
                            if (null == name) continue;
                            CellRangeAddress cellRangeAddressNumeric = CellRangeAddress.ValueOf(GetNamedRange(SigTestFormatSheet as XSSFSheet, tmpNamesArray[i] + tmpSuffix).RefersToFormula);
                            CellRangeAddress cellRange = new CellRangeAddress(cellRangeAddressNumeric.FirstRow, cellRangeAddressNumeric.LastRow, cellRangeAddressNumeric.FirstColumn - 1, cellRangeAddressNumeric.LastColumn - 1);
                            NpoiHelper.deleteSingleColumnName(SigTestFormatSheet as XSSFSheet, cellRange);
                        }
                    }
                    wtCutCol++;
                }
                else
                {
                    RedrawBorder = false;
                }
            }

            if (!CurrentOutput.ShowPreWBTotal)
            {
                if (IsPortrait)
                {
                    if (FormatSheet != null)
                    {
                        String area = GetNamedRange(FormatSheet as XSSFSheet, "PreWBRows").RefersToFormula;
                        FormatBook.RemoveName(GetNamedRange(FormatSheet as XSSFSheet, "PreWBRows"));
                        foreach (String areaPreWBRows in area.Split(',').Reverse())
                        {
                            //NpoiHelper.RemoveNumberOfRows(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(areaPreWBRows).FirstRow + 1, 1);
                            FormatSheet.ShiftRows(CellRangeAddress.ValueOf(areaPreWBRows).FirstRow + 1, FormatSheet.LastRowNum, -1);
                        }
                    }

                    if (SigTestFormatSheet != null)
                    {
                        String area = GetNamedRange(SigTestFormatSheet as XSSFSheet, "PreWBRows").RefersToFormula;
                        foreach (String areaPreWBRows in area.Split(',').Reverse())
                        {
                            SigTestFormatSheet.ShiftRows(CellRangeAddress.ValueOf(areaPreWBRows).FirstRow + 1, SigTestFormatSheet.LastRowNum, -1);
                            //NpoiHelper.RemoveNumberOfRows(SigTestFormatSheet as XSSFSheet, CellRangeAddress.ValueOf(areaPreWBRows).FirstRow + 1, 1);
                        }
                    }
                }
                else
                {
                    if (FormatSheet != null)
                    {
                        NpoiHelper.deleteColumn(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, "PreWBColumn").RefersToFormula).FirstColumn);
                        FormatBook.RemoveName(GetNamedRange(FormatSheet as XSSFSheet, "PreWBColumn"));
                    }
                    if (SigTestFormatSheet != null)
                    {
                        NpoiHelper.deleteColumn(SigTestFormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(SigTestFormatSheet as XSSFSheet, "PreWBColumn").RefersToFormula).FirstColumn);
                    }
                    CutColoums++;
                    numericCutCol++;
                    wtCutCol++;
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
                //NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, wtCutCol, tmp, Suffix, true);
                tmp = CurrentOutput.ParentRequest.WeightAverageNumDigitsAfterDecimal;
                Suffix = "_WT_WeightAverage";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, wtCutCol, tmp, Suffix);
            }

            tmpSuffix = IsPortrait ? "Row" : "Column";
            tmpNamesArray = "N".Split();

            if (!CurrentOutput.ParentRequest.ShowParameter)
            {
                tmpName = "N_Population" + tmpSuffix;
                if (FormatSheet != null)
                    NpoiHelper.deleteColumn(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                if (SigTestFormatSheet != null)
                    NpoiHelper.deleteColumn(SigTestFormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                numericCutCol++;
            }
            if (CurrentOutput.ParentRequest.ShowSummary)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Summary);
                Suffix = "_Summary";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, numericCutCol, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Summary" + tmpSuffix;
                if (FormatSheet != null)
                    NpoiHelper.deleteColumn(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                if (SigTestFormatSheet != null)
                    NpoiHelper.deleteColumn(SigTestFormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                numericCutCol++;
            }
            if (CurrentOutput.ParentRequest.ShowAverage)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Average);
                Suffix = "_Average";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, numericCutCol, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Average" + tmpSuffix;
                if (FormatSheet != null)
                    NpoiHelper.deleteColumn(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                if (SigTestFormatSheet != null)
                    NpoiHelper.deleteColumn(SigTestFormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                numericCutCol++;
            }
            if (CurrentOutput.ParentRequest.ShowStdev)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Stdev);
                Suffix = "_Deviation";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, numericCutCol, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Deviation" + tmpSuffix;
                if (FormatSheet != null)
                    NpoiHelper.deleteColumn(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                if (SigTestFormatSheet != null)
                    NpoiHelper.deleteColumn(SigTestFormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                numericCutCol++;
            }
            if (CurrentOutput.ParentRequest.ShowMinimum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Minimum);
                Suffix = "_Minimum";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, numericCutCol, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Minimum" + tmpSuffix;
                if (FormatSheet != null)
                    NpoiHelper.deleteColumn(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                if (SigTestFormatSheet != null)
                    NpoiHelper.deleteColumn(SigTestFormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                numericCutCol++;
            }
            if (CurrentOutput.ParentRequest.ShowMaximum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Maximum);
                Suffix = "_Maximum";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, numericCutCol, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Maximum" + tmpSuffix;
                if (FormatSheet != null)
                    NpoiHelper.deleteColumn(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                if (SigTestFormatSheet != null)
                    NpoiHelper.deleteColumn(SigTestFormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                numericCutCol++;
            }
            if (CurrentOutput.ParentRequest.ShowMedian)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Median);
                Suffix = "_Median";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, numericCutCol, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Median" + tmpSuffix;
                if (FormatSheet != null)
                    NpoiHelper.deleteColumn(FormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                if (SigTestFormatSheet != null)
                    NpoiHelper.deleteColumn(SigTestFormatSheet as XSSFSheet, CellRangeAddress.ValueOf(GetNamedRange(FormatSheet as XSSFSheet, tmpName).RefersToFormula).FirstColumn);
                numericCutCol++;
            }
        }

        private void NumberFormat(
               ISheet FormatSheet, ISheet SigTestFormatSheet
             , string[] NamesArray, int numericCutCol, int NumDigitsAfterDecimal
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

            foreach (string tmpName in NamesArray)
            {
                n = tmpName + Suffix;
                if (FormatSheet != null)
                {
                    string nameR = GetNamedRange(FormatSheet as XSSFSheet, n).RefersToFormula;
                    string[] names = nameR.Split(',');
                    foreach (string s in names)
                    {
                        CellRangeAddress cellRangeAddress = CellRangeAddress.ValueOf(s);
                        AddFormatToCell(FormatSheet as XSSFSheet, cellRangeAddress, fmt, numericCutCol, tmpName);
                    }
                }
                if (CurrentOutput.SignificanceTest)
                {
                    if (SigTestFormatSheet != null)
                    {
                        string nameR = GetNamedRange(SigTestFormatSheet as XSSFSheet, n).RefersToFormula;
                        string[] names = nameR.Split(',');
                        foreach (string s in names)
                        {
                            CellRangeAddress cellRangeAddress = CellRangeAddress.ValueOf(s);
                            AddFormatToCell(SigTestFormatSheet as XSSFSheet, cellRangeAddress, fmt, numericCutCol, tmpName);
                        }
                    }
                }
            }
        }

        private void AddFormatToCell(XSSFSheet sheet, CellRangeAddress cellRangeAddress, string fmt, int lastCol, string tmpName)
        {
            int rangeRow = cellRangeAddress.FirstRow;
            int rangeCol = (cellRangeAddress.FirstColumn > lastCol ? cellRangeAddress.FirstColumn - lastCol : lastCol - cellRangeAddress.FirstColumn);
            for (int sRow = rangeRow; sRow <= cellRangeAddress.LastRow;)
            {
                XSSFCell c = sheet.GetRow(sRow).GetCell(rangeCol) as XSSFCell;
                XSSFDataFormat dt = sheet.Workbook.CreateDataFormat() as XSSFDataFormat;
                XSSFCellStyle st = sheet.Workbook.CreateCellStyle() as XSSFCellStyle;
                st.CloneStyleFrom(c.CellStyle);
                string dtf = st.GetDataFormatString();
                dtf = fmt;
                st.DataFormat = dt.GetFormat(dtf);
                c.CellStyle = st;
                sRow = tmpName == "SA_MA_NP" ? sRow + 2 : sRow + 1;
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

        private void AdjustContentsSheet(XSSFWorkbook FormatBook, List<XSSFWorkbook> Books, ISheet ContentsSheet
      , ref Array ContentsValue, ref Array HyperlinkTargetCells, ref Array HyperlinkTargetSheets
      , TableType TableType
      , int MinIndex = 0, int MaxIndex = 0)
        {
            CrossTable tmpTable;
            string buf;
            long d;
            XSSFSimpleShape tmpS, tmpS2;
            long t1, t2;
            int c;
            int n;
            Array v;
            int clrIdx;
            int u;
            bool IsSigTest;
            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            bool globalMode = QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? false : true;
            XSSFDrawing drawing = (XSSFDrawing)ContentsSheet.DrawingPatriarch;

            XSSFSimpleShape sigMainS = drawing.GetShapes().ElementAt(1) as XSSFSimpleShape;
            IEG_Anchor sigMainSAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(1);
            XSSFSimpleShape RateDifferenceLegend = drawing.GetShapes().ElementAt(2) as XSSFSimpleShape;
            IEG_Anchor RateDifferenceLegendAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(2);
            XSSFSimpleShape Level2HighLabel = drawing.GetShapes().ElementAt(3) as XSSFSimpleShape;
            IEG_Anchor Level2HighLabelAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(3);
            XSSFSimpleShape Level2HighPalette = drawing.GetShapes().ElementAt(4) as XSSFSimpleShape;
            IEG_Anchor Level2HighPaletteAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(4);
            XSSFSimpleShape Level1HighLabel = drawing.GetShapes().ElementAt(5) as XSSFSimpleShape;
            IEG_Anchor Level1HighLabelAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(5);
            XSSFSimpleShape Level1HighPalette = drawing.GetShapes().ElementAt(6) as XSSFSimpleShape;
            IEG_Anchor Level1HighPaletteAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(6);
            XSSFSimpleShape Level1LowLabel = drawing.GetShapes().ElementAt(7) as XSSFSimpleShape;
            IEG_Anchor Level1LowLabelAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(7);
            XSSFSimpleShape Level1LowPalette = drawing.GetShapes().ElementAt(8) as XSSFSimpleShape;
            IEG_Anchor Level1LowPaletteAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(8);
            XSSFSimpleShape Level2LowLabel = drawing.GetShapes().ElementAt(9) as XSSFSimpleShape;
            IEG_Anchor Level2LowLabelAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(9);
            XSSFSimpleShape Level2LowPalette = drawing.GetShapes().ElementAt(10) as XSSFSimpleShape;
            IEG_Anchor Level2LowPaletteAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(10);
            XSSFSimpleShape RankingMarkingLegend = drawing.GetShapes().ElementAt(11) as XSSFSimpleShape;
            IEG_Anchor tRankingMarkingLegendAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(11);
            XSSFSimpleShape Rank1Oval = drawing.GetShapes().ElementAt(12) as XSSFSimpleShape;
            IEG_Anchor Rank1OvalAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(12);
            XSSFSimpleShape Rank1Label = drawing.GetShapes().ElementAt(13) as XSSFSimpleShape;
            IEG_Anchor Rank1LabelAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(13);
            XSSFSimpleShape Rank2Oval = drawing.GetShapes().ElementAt(14) as XSSFSimpleShape;
            IEG_Anchor Rank2OvalAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(14);
            XSSFSimpleShape Rank2Label = drawing.GetShapes().ElementAt(15) as XSSFSimpleShape;
            IEG_Anchor Rank2LabelAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(15);
            XSSFSimpleShape Rank3Oval = drawing.GetShapes().ElementAt(16) as XSSFSimpleShape;
            IEG_Anchor Rank3OvalAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(16);
            XSSFSimpleShape Rank3Label = drawing.GetShapes().ElementAt(17) as XSSFSimpleShape;
            IEG_Anchor Rank3LabelAncr = drawing.GetCTDrawing().CellAnchors.ElementAt(17);

            XSSFFont xfLegnd = new XSSFFont();
            xfLegnd.FontName = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            xfLegnd.FontHeightInPoints = !globalMode ? 9 : 8;

            foreach (XSSFSimpleShape shape in drawing.GetShapes())
            {
                XSSFRichTextString richString = new XSSFRichTextString(CurrentOutput.ParentRequest.Title);
                XSSFFont xf = new XSSFFont();
                xf.FontName = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                xf.FontHeightInPoints = 10;
                richString.ApplyFont(xf);
                shape.SetText(richString);
                shape.VerticalAlignment = VerticalAlignment.Center;
                shape.TextParagraphs[0].TextAlign = TextAlign.CENTER;
                break;
            }
            tmpTable = (CrossTable)CurrentOutput.Tables[0];
            if (tmpTable.KeyItem == null)
            {
                RemoveNamedRange((XSSFSheet)ContentsSheet, "KeyItemInformation", true);
            }
            else
            {
                v = new string[2, 2];
                v.SetValue(LocalResource.REPORT_CLASSIFICATION_ITEM_KEYWORD, 0, 0);
                v.SetValue(LocalResource.REPORT_SECTOR_KEYWORD, 1, 0);
                KeyItemInformation WithtmpTable = tmpTable.KeyItem;
                v.SetValue(WithtmpTable.Name + ":" + WithtmpTable.Description, 0, 1);
                v.SetValue(WithtmpTable.SectorNumber + ":" + WithtmpTable.SectorDescription, 1, 1);

                CellRangeAddress WithContentsSheetRng = CellRangeAddress.ValueOf(GetNamedRange(ContentsSheet as XSSFSheet, "KeyItemInformation").RefersToFormula);
                NpoiHelper.PutValue(ContentsSheet as XSSFSheet, WithContentsSheetRng.FormatAsString(), v);
            }
            buf = CurrentOutput.LocalizedFilteringExpression;
            if (null == buf || buf.Length == 0)
            {
                RemoveNamedRange((XSSFSheet)ContentsSheet, "Criteria1");
            }
            else
            {
                v = new string[2];
                v.SetValue(LocalResource.REPORT_FILTER_CRITERION_KEYWORD, 0);
                v.SetValue(buf, 1);
                //double MAXROW_HEIGHT_INDEX_TITLE = 66;
                CellRangeAddress WithContentsSheetRng = CellRangeAddress.ValueOf(GetNamedRange((XSSFSheet)ContentsSheet, "Criteria1").RefersToFormula);
                AreaReference aref = new AreaReference(WithContentsSheetRng.FormatAsString(), SpreadsheetVersion.EXCEL2007);
                CellReference[] crefs = aref.GetAllReferencedCells();
                int row = crefs[0].Row;
                int col = crefs[0].Col;
                ContentsSheet.GetRow(row).GetCell(col).SetCellValue((string)v.GetValue(0));
                col++;
                ContentsSheet.GetRow(row).GetCell(col).SetCellValue((string)v.GetValue(1));
                //if (r == null) continue;
                //XSSFCell c = r.GetCell(colLocal) as XSSFCell;
                //int i, j;
                //InsertValues(WithContentsSheetRng, ContentsSheet, v);
                //OutputUtil.PutValue(WithContentsSheetRng.Cells, ref v);
                //Range WithContentsSheetRngER = WithContentsSheetRng.EntireRow;
                //h = WithContentsSheetRngER.RowHeight;
                //WithContentsSheetRngER.AutoFit();
                //if (WithContentsSheetRngER.RowHeight < h) { WithContentsSheetRngER.RowHeight = h; }
                //else if (WithContentsSheetRngER.RowHeight > MAXROW_HEIGHT_INDEX_TITLE) { WithContentsSheetRngER.RowHeight = MAXROW_HEIGHT_INDEX_TITLE; }
            }
            if (CurrentOutput.WBOn)
            {
                string wb = LocalResource.WEIGHT_BACK;
                string msg = wb.Insert(wb.Length - 1, "[" + tmpTable.Question.WBValue + "]") + '\u2009'
                                + (tmpTable.Question.TabulateFullQuantity ? LocalResource.WB_TOTAL_NUMBER_BASE : string.Empty);
                CellRangeAddress WithContentsSheetRng = CellRangeAddress.ValueOf(GetNamedRange((XSSFSheet)ContentsSheet, "WBPrompt").RefersToFormula);
                AreaReference aref = new AreaReference(WithContentsSheetRng.FormatAsString(), SpreadsheetVersion.EXCEL2007);
                CellReference[] crefs = aref.GetAllReferencedCells();
                int row = crefs[0].Row;
                int col = crefs[0].Col;
                ContentsSheet.GetRow(row).GetCell(col).SetCellValue(msg);
                //OutputUtil.PutValue(ContentsSheet.Range["WBPrompt"], ref msg);
            }
            else if (tmpTable.Question.TabulateFullQuantity)
            {
                string msg = LocalResource.WB_TOTAL_NUMBER_BASE;
                CellRangeAddress WithContentsSheetRng = CellRangeAddress.ValueOf(GetNamedRange((XSSFSheet)ContentsSheet, "WBPrompt").RefersToFormula);
                AreaReference aref = new AreaReference(WithContentsSheetRng.FormatAsString(), SpreadsheetVersion.EXCEL2007);
                CellReference[] crefs = aref.GetAllReferencedCells();
                int row = crefs[0].Row;
                int col = crefs[0].Col;
                ContentsSheet.GetRow(row).GetCell(col).SetCellValue(msg);
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

                    CellRangeAddress WithContentsSheetRng = CellRangeAddress.ValueOf(GetNamedRange((XSSFSheet)ContentsSheet, "MinBase").RefersToFormula);
                    AreaReference aref = new AreaReference(WithContentsSheetRng.FormatAsString(), SpreadsheetVersion.EXCEL2007);
                    CellReference[] crefs = aref.GetAllReferencedCells();
                    int row = crefs[0].Row;
                    int col = crefs[0].Col;
                    ContentsSheet.GetRow(row).GetCell(col).SetCellValue(msg);
                    //OutputUtil.PutValue(ContentsSheet.Range["MinBase"], ref msg);
                }
            }
            if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
            {
                string msg = "";
                bool isSig = false;
                switch (TableType)
                {
                    case TableType.NPer:
                        msg = LocalResource.REPORT_CROSS_NP_SHEET_NAME;
                        break;
                    case TableType.N:
                        msg = LocalResource.REPORT_CROSS_N_SHEET_NAME;
                        break;
                    case TableType.Per:
                        msg = LocalResource.REPORT_CROSS_P_SHEET_NAME;
                        break;
                    case TableType.SignificanceTest:
                        isSig = true;
                        msg = LocalResource.REPORT_CROSS_SIGNIFICANCE_TEST_SHEET_NAME;
                        break;
                }

                CellRangeAddress WithContentsSheetRng = CellRangeAddress.ValueOf(GetNamedRange((XSSFSheet)ContentsSheet, "tableType").RefersToFormula);
                AreaReference aref = new AreaReference(WithContentsSheetRng.FormatAsString(), SpreadsheetVersion.EXCEL2007);
                CellReference[] crefs = aref.GetAllReferencedCells();
                int row = crefs[0].Row;
                int col = crefs[0].Col;
                ContentsSheet.GetRow(row).GetCell(col).SetCellValue(msg);
                if (isSig)
                    ContentsSheet.GetRow(row).GetCell(col).CellStyle.WrapText = true;
            }

            bool sigLgnd = false;
            bool isRankingMarkingLegendRequired = (IsSigTest && CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single) ? false : true;
            if (IsSigTest || CurrentOutput.MarkingSignificance)
            {
                sigLgnd = true;
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
                    XSSFRichTextString richString = new XSSFRichTextString(System.Text.RegularExpressions.Regex.Unescape(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_CAPTION) + "\n" + String.Join("\n", (string[])v));
                    richString.ApplyFont(xfLegnd);
                    sigMainS.SetText(richString);
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
                    XSSFRichTextString richString = new XSSFRichTextString(System.Text.RegularExpressions.Regex.Unescape(LocalResource.REPORT_MARKING_LEGEND_SIGNIFICANCE_TEST_TO_WHOLE_CAPTION) + "\n" + String.Join("\n", (string[])v));
                    richString.ApplyFont(xfLegnd);
                    sigMainS.SetText(richString);
                }
            }
            else
            {
                drawing.GetCTDrawing().CellAnchors.Remove(sigMainSAncr);
            }

            tmpS = RateDifferenceLegend;
            bool rateDiffgnd = false;
            if (CurrentOutput.MarkingColoring && isRankingMarkingLegendRequired)
            {
                rateDiffgnd = true;
                XSSFRichTextString richString = new XSSFRichTextString(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_CAPTION);
                richString.ApplyFont(xfLegnd);
                tmpS.SetText(richString);
                if (!sigLgnd)
                {
                    XSSFClientAnchor ca = RateDifferenceLegend.GetAnchor() as XSSFClientAnchor;
                    LegendPostion l = Books == null ? new LegendPostion() : new LegendPostionIndividual();
                    ca.Col1 = l.RateDifferenceLegend_Col1;
                    ca.Dx1 = l.RateDifferenceLegend_Dx1;
                    ca.Col2 = l.RateDifferenceLegend_Col2;
                    ca.Dx2 = l.RateDifferenceLegend_Dx2;

                    ca = Level2HighLabel.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Level2HighLabel_Col1;
                    ca.Dx1 = l.Level2HighLabel_Dx1;
                    ca.Col2 = l.Level2HighLabel_Col2;
                    ca.Dx2 = l.Level2HighLabel_Dx2;

                    ca = Level2HighPalette.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Level2HighPalette_Col1;
                    ca.Dx1 = l.Level2HighPalette_Dx1;
                    ca.Col2 = l.Level2HighPalette_Col2;
                    ca.Dx2 = l.Level2HighPalette_Dx2;

                    ca = Level2LowLabel.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Level2HighLabel_Col1;
                    ca.Dx1 = l.Level2HighLabel_Dx1;
                    ca.Col2 = l.Level2HighLabel_Col2;
                    ca.Dx2 = l.Level2HighLabel_Dx2;

                    ca = Level2LowPalette.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Level2HighPalette_Col1;
                    ca.Dx1 = l.Level2HighPalette_Dx1;
                    ca.Col2 = l.Level2HighPalette_Col2;
                    ca.Dx2 = l.Level2HighPalette_Dx2;

                    ca = Level1HighLabel.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Level2HighLabel_Col1;
                    ca.Dx1 = l.Level2HighLabel_Dx1;
                    ca.Col2 = l.Level2HighLabel_Col2;
                    ca.Dx2 = l.Level2HighLabel_Dx2;

                    ca = Level1HighPalette.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Level2HighPalette_Col1;
                    ca.Dx1 = l.Level2HighPalette_Dx1;
                    ca.Col2 = l.Level2HighPalette_Col2;
                    ca.Dx2 = l.Level2HighPalette_Dx2;

                    ca = Level1LowLabel.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Level2HighLabel_Col1;
                    ca.Dx1 = l.Level2HighLabel_Dx1;
                    ca.Col2 = l.Level2HighLabel_Col2;
                    ca.Dx2 = l.Level2HighLabel_Dx2;

                    ca = Level1LowPalette.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Level2HighPalette_Col1;
                    ca.Dx1 = l.Level2HighPalette_Dx1;
                    ca.Col2 = l.Level2HighPalette_Col2;
                    ca.Dx2 = l.Level2HighPalette_Dx2;
                }
            }
            else
            {
                drawing.GetCTDrawing().CellAnchors.Remove(RateDifferenceLegendAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Level2HighLabelAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Level2HighPaletteAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Level2LowLabelAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Level2LowPaletteAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Level1HighLabelAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Level1HighPaletteAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Level1LowLabelAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Level1LowPaletteAncr);
            }

            tmpS = RankingMarkingLegend;
            if (CurrentOutput.MarkingRanking && isRankingMarkingLegendRequired)
            {
                XSSFRichTextString richString = new XSSFRichTextString(LocalResource.REPORT_MARKING_LEGEND_RANKING_CAPTION);
                richString.ApplyFont(xfLegnd);
                tmpS.SetText(richString);
                richString = new XSSFRichTextString(LocalResource.REPORT_MARKING_LEGEND_RANKING_1ST_CAPTION);
                richString.ApplyFont(xfLegnd);
                Rank1Label.SetText(richString);
                richString = new XSSFRichTextString(LocalResource.REPORT_MARKING_LEGEND_RANKING_2ND_CAPTION);
                richString.ApplyFont(xfLegnd);
                Rank2Label.SetText(richString);
                richString = new XSSFRichTextString(LocalResource.REPORT_MARKING_LEGEND_RANKING_3RD_CAPTION);
                richString.ApplyFont(xfLegnd);
                Rank3Label.SetText(richString);
                LegendPostion l = null;
                if (!sigLgnd && !rateDiffgnd)
                {
                    l = Books == null ? new LegendPostion(false) : new LegendPostionIndividual(false);
                }
                else if (!sigLgnd)
                {
                    l = Books == null ? new LegendPostion() : new LegendPostionIndividual();
                }
                else if (!rateDiffgnd)
                {
                    l = Books == null ? new LegendPostion(true) : new LegendPostionIndividual(true);
                }
                if (l != null)
                {
                    XSSFClientAnchor ca = RankingMarkingLegend.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.RankingMarkingLegendCol1;
                    ca.Dx1 = l.RankingMarkingLegendDx1;
                    ca.Col2 = l.RankingMarkingLegendCol2;
                    ca.Dx2 = l.RankingMarkingLegendDx2;

                    ca = Rank1Oval.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Rank1OvalCol1;
                    ca.Dx1 = l.Rank1OvalDx1;
                    ca.Col2 = l.Rank1OvalCol2;
                    ca.Dx2 = l.Rank1OvalDx2;

                    ca = Rank1Label.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Rank1LabelCol1;
                    ca.Dx1 = l.Rank1LabelDx1;
                    ca.Col2 = l.Rank1LabelCol2;
                    ca.Dx2 = l.Rank1LabelDx2;

                    ca = Rank2Oval.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Rank1OvalCol1;
                    ca.Dx1 = l.Rank1OvalDx1;
                    ca.Col2 = l.Rank1OvalCol2;
                    ca.Dx2 = l.Rank1OvalDx2;

                    ca = Rank2Label.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Rank1LabelCol1;
                    ca.Dx1 = l.Rank1LabelDx1;
                    ca.Col2 = l.Rank1LabelCol2;
                    ca.Dx2 = l.Rank1LabelDx2;

                    ca = Rank3Oval.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Rank1OvalCol1;
                    ca.Dx1 = l.Rank1OvalDx1;
                    ca.Col2 = l.Rank1OvalCol2;
                    ca.Dx2 = l.Rank1OvalDx2;

                    ca = Rank3Label.GetAnchor() as XSSFClientAnchor;
                    ca.Col1 = l.Rank1LabelCol1;
                    ca.Dx1 = l.Rank1LabelDx1;
                    ca.Col2 = l.Rank1LabelCol2;
                    ca.Dx2 = l.Rank1LabelDx2;
                }
            }
            else
            {
                drawing.GetCTDrawing().CellAnchors.Remove(tRankingMarkingLegendAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Rank1OvalAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Rank1LabelAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Rank2OvalAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Rank2LabelAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Rank3OvalAncr);
                drawing.GetCTDrawing().CellAnchors.Remove(Rank3LabelAncr);
            }
            tmpS = null;

            //bool showLevel1High = true;
            //bool showLevel1Low = true;
            bool showLevel1High = !CurrentOutput.MarkingColoringLevel2High && CurrentOutput.MarkingColoringLevel1High ? false : true; ;
            bool showLevel2High = !CurrentOutput.MarkingColoringLevel2High && CurrentOutput.MarkingColoringLevel1High ? true : false;
            bool showLevel1Low = !CurrentOutput.MarkingColoringLevel2Low ? false : true;
            bool showlevel2Low = !CurrentOutput.MarkingColoringLevel2Low && CurrentOutput.MarkingColoringLevel1Low ? true : false;
            int clrPos = 0;
            if (CurrentOutput.Level1Percent == CurrentOutput.Level2Percent)
            {
                if (CurrentOutput.MarkingColoringLevel2High)
                {
                    showLevel1High = false;
                }
                if (CurrentOutput.MarkingColoringLevel2Low)
                {
                    showLevel1Low = false;
                }
            }
            if (CurrentOutput.MarkingColoring && isRankingMarkingLegendRequired)
            {
                t1 = Level2HighLabel.GetAnchor().Dy1;
                t2 = Level2HighPalette.GetAnchor().Dy1;
                d = Level1HighLabel.GetAnchor().Dy1 - t1;
                tmpS = Level2HighLabel;
                tmpS2 = Level2HighPalette;
                if (CurrentOutput.MarkingColoringLevel2High)
                {
                    XSSFRichTextString richString = null;
                    if (!globalMode)
                    {
                        richString = new XSSFRichTextString(string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
                           , (" " + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1)));
                    }
                    else
                    {
                        richString = new XSSFRichTextString(string.Format("Total {0} Points"
                          , ((CurrentOutput.Level2Percent.ToString().Length > 1 ? " +" : "  +") + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1)));
                    }
                    clrIdx = CurrentOutput.Level2HighColorIndex;
                    richString.ApplyFont(xfLegnd);
                    tmpS.SetText(richString);

                    Color s = Helper.GetColorByColorIndex(clrIdx);
                    Level2HighPalette.SetFillColor(s.R, s.G, s.B);
                    Level2HighLabel.GetAnchor().Dy1 = Convert.ToInt32(t1);
                    Level2HighPalette.GetAnchor().Dy1 = Convert.ToInt32(t2);

                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else if (showLevel2High)
                {
                    XSSFRichTextString richString = null;
                    if (!globalMode)
                    {
                        richString = new XSSFRichTextString(string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
                             , (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1)));
                    }
                    else
                    {
                        richString = new XSSFRichTextString(string.Format("Total {0} Points"
                            , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? " +" : "  +") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1)));
                    }
                    clrIdx = CurrentOutput.Level2HighColorIndex;
                    richString.ApplyFont(xfLegnd);
                    tmpS.SetText(richString);

                    Color s = Helper.GetColorByColorIndex(clrIdx);
                    Level2HighPalette.SetFillColor(s.R, s.G, s.B);
                    Level2HighLabel.GetAnchor().Dy1 = Convert.ToInt32(t1);
                    Level2HighPalette.GetAnchor().Dy1 = Convert.ToInt32(t2);

                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.SetText("");
                    drawing.GetCTDrawing().CellAnchors.Remove(Level2HighPaletteAncr);
                }
                tmpS = Level1HighLabel;
                tmpS2 = Level1HighPalette;
                if (CurrentOutput.MarkingColoringLevel1High && showLevel1High)
                {
                    XSSFRichTextString richString = null;
                    if (!globalMode)
                    {
                        richString = new XSSFRichTextString(string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
                             , (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1)));
                    }
                    else
                    {
                        richString = new XSSFRichTextString(string.Format("Total {0} Points"
                            , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? " +" : "  +") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1)));
                    }
                    richString.ApplyFont(xfLegnd);
                    tmpS.SetText(richString);

                    clrIdx = CurrentOutput.Level1HighColorIndex;
                    Color s = Helper.GetColorByColorIndex(clrIdx);
                    Level1HighPalette.SetFillColor(s.R, s.G, s.B);
                    int diff = Level1HighLabel.GetAnchor().Dy2 - Level1HighLabel.GetAnchor().Dy1;
                    int diffP = Level1HighPalette.GetAnchor().Dy2 - Level1HighPalette.GetAnchor().Dy1;
                    Level1HighLabel.GetAnchor().Dy1 = Convert.ToInt32(t1);
                    Level1HighLabel.GetAnchor().Dy2 = Convert.ToInt32(t1 + diff);
                    Level1HighPalette.GetAnchor().Dy1 = Convert.ToInt32(t2);
                    Level1HighPalette.GetAnchor().Dy2 = Convert.ToInt32(t2 + diffP);

                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.SetText("");
                    drawing.GetCTDrawing().CellAnchors.Remove(Level1HighPaletteAncr);
                }
                tmpS = Level1LowLabel;
                tmpS2 = Level1LowPalette;
                if (CurrentOutput.MarkingColoringLevel1Low && showLevel1Low)
                {
                    XSSFRichTextString richString = null;
                    if (!globalMode)
                    {
                        richString = new XSSFRichTextString(string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                               , (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1)));
                    }
                    else
                    {
                        richString = new XSSFRichTextString(string.Format("Total {0} Points"
                             , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? "  -" : "   -") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1)));
                    }
                    richString.ApplyFont(xfLegnd);
                    tmpS.SetText(richString);

                    clrIdx = CurrentOutput.Level1LowColorIndex;
                    Color s = Helper.GetColorByColorIndex(clrIdx);
                    Level1LowPalette.SetFillColor(s.R, s.G, s.B);
                    int diff = Level1LowLabel.GetAnchor().Dy2 - Level1LowLabel.GetAnchor().Dy1;
                    int diffP = Level1LowPalette.GetAnchor().Dy2 - Level1LowPalette.GetAnchor().Dy1;
                    Level1LowLabel.GetAnchor().Dy1 = Convert.ToInt32(t1);
                    Level1LowLabel.GetAnchor().Dy2 = Convert.ToInt32(t1 + diff);
                    Level1LowPalette.GetAnchor().Dy1 = Convert.ToInt32(t2);
                    Level1LowPalette.GetAnchor().Dy2 = Convert.ToInt32(t2 + diffP);
                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.SetText("");
                    drawing.GetCTDrawing().CellAnchors.Remove(Level1LowPaletteAncr);
                }
                tmpS = Level2LowLabel;
                tmpS2 = Level2LowPalette;
                if (CurrentOutput.MarkingColoringLevel2Low)
                {
                    XSSFRichTextString richString = null;
                    if (!globalMode)
                    {
                        richString = new XSSFRichTextString(string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                           , (" " + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1)));
                    }
                    else
                    {
                        richString = new XSSFRichTextString(string.Format("Total {0} Points"
                          , ((CurrentOutput.Level2Percent.ToString().Length > 1 ? "  -" : "   -") + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1)));
                    }
                    richString.ApplyFont(xfLegnd);
                    tmpS.SetText(richString);

                    clrIdx = CurrentOutput.Level2LowColorIndex;
                    Color s = Helper.GetColorByColorIndex(clrIdx);
                    Level2LowPalette.SetFillColor(s.R, s.G, s.B);
                    int diff = Level2LowLabel.GetAnchor().Dy2 - Level2LowLabel.GetAnchor().Dy1;
                    int diffP = Level2LowPalette.GetAnchor().Dy2 - Level2LowPalette.GetAnchor().Dy1;
                    Level2LowLabel.GetAnchor().Dy1 = Convert.ToInt32(t1);
                    Level2LowLabel.GetAnchor().Dy2 = Convert.ToInt32(t1 + diff);
                    Level2LowPalette.GetAnchor().Dy1 = Convert.ToInt32(t2);
                    Level2LowPalette.GetAnchor().Dy2 = Convert.ToInt32(t2 + diffP);
                }
                else if (showlevel2Low)
                {
                    XSSFRichTextString richString = null;
                    if (!globalMode)
                    {
                        richString = new XSSFRichTextString(string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                               , (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1)));
                    }
                    else
                    {
                        richString = new XSSFRichTextString(string.Format("Total {0} Points"
                             , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? "  -" : "   -") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1)));
                    }
                    richString.ApplyFont(xfLegnd);
                    tmpS.SetText(richString);

                    clrIdx = CurrentOutput.Level2LowColorIndex;
                    Color s = Helper.GetColorByColorIndex(clrIdx);
                    Level2LowPalette.SetFillColor(s.R, s.G, s.B);
                    int diff = Level2LowLabel.GetAnchor().Dy2 - Level2LowLabel.GetAnchor().Dy1;
                    int diffP = Level2LowPalette.GetAnchor().Dy2 - Level2LowPalette.GetAnchor().Dy1;
                    Level2LowLabel.GetAnchor().Dy1 = Convert.ToInt32(t1);
                    Level2LowLabel.GetAnchor().Dy2 = Convert.ToInt32(t1 + diff);
                    Level2LowPalette.GetAnchor().Dy1 = Convert.ToInt32(t2);
                    Level2LowPalette.GetAnchor().Dy2 = Convert.ToInt32(t2 + diffP);
                }
                else
                {
                    tmpS.SetText("");
                    drawing.GetCTDrawing().CellAnchors.Remove(Level2LowPaletteAncr);
                }
            }

            if (globalMode)
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single && isRankingMarkingLegendRequired)
                {
                    if (CurrentOutput.MarkingRanking)
                    {
                        var rankMarking = RankingMarkingLegend.GetAnchor();
                        if (sigLgnd && rateDiffgnd)
                        {
                            //rankMarking.Dx1 -= 100000;
                            rankMarking.Dx2 -= 85000;
                            //AdjustRankingLegendOverallPosition_GlobalMode(RankingMarkingLegend, Rank1Oval, Rank2Oval, Rank3Oval, Rank1Label, Rank2Label, Rank3Label, 550000,60000,0);
                        }
                        else if (!sigLgnd && !rateDiffgnd)
                        {
                            rankMarking.Dx1 += 750000;
                            //rankMarking.Dx2 -= 80000;
                            //AdjustRankingLegendOverallPosition_GlobalMode(RankingMarkingLegend, Rank1Oval, Rank2Oval, Rank3Oval, Rank1Label, Rank2Label, Rank3Label, 550000, -60000, -150000);
                        }
                        else if (sigLgnd)
                        {
                            rankMarking.Dx1 += 810000;
                            rankMarking.Dx2 += 845000;
                            var rank1Oval = Rank1Oval.GetAnchor();
                            var rank1Label = Rank1Label.GetAnchor();
                            var rank2Oval = Rank2Oval.GetAnchor();
                            var rank2Label = Rank2Label.GetAnchor();
                            var rank3Oval = Rank3Oval.GetAnchor();
                            var rank3Label = Rank3Label.GetAnchor();

                            rank1Oval.Dx1 += 800000;
                            rank1Oval.Dx2 += 800000;
                            rank1Label.Dx1 += 800000;
                            rank1Label.Dx2 += 800000;


                            rank2Oval.Dx1 += 800000;
                            rank2Oval.Dx2 += 800000;
                            rank2Label.Dx1 += 800000;
                            rank2Label.Dx2 += 800000;


                            rank3Oval.Dx1 += 800000;
                            rank3Oval.Dx2 += 800000;
                            rank3Label.Dx1 += 800000;
                            rank3Label.Dx2 += 800000;
                            //AdjustRankingLegendOverallPosition_GlobalMode(RankingMarkingLegend, Rank1Oval, Rank2Oval, Rank3Oval, Rank1Label, Rank2Label, Rank3Label, 550000, -60000, -150000);
                        }
                        else
                        {
                            rankMarking.Dx1 += 610000;
                            rankMarking.Dx2 += 645000;
                            var rank1Oval = Rank1Oval.GetAnchor();
                            var rank1Label = Rank1Label.GetAnchor();
                            var rank2Oval = Rank2Oval.GetAnchor();
                            var rank2Label = Rank2Label.GetAnchor();
                            var rank3Oval = Rank3Oval.GetAnchor();
                            var rank3Label = Rank3Label.GetAnchor();

                            rank1Oval.Dx1 += 700000;
                            rank1Oval.Dx2 += 700000;
                            rank1Label.Dx1 += 700000;
                            rank1Label.Dx2 += 700000;


                            rank2Oval.Dx1 += 700000;
                            rank2Oval.Dx2 += 700000;
                            rank2Label.Dx1 += 700000;
                            rank2Label.Dx2 += 700000;


                            rank3Oval.Dx1 += 700000;
                            rank3Oval.Dx2 += 700000;
                            rank3Label.Dx1 += 700000;
                            rank3Label.Dx2 += 700000;

                            //AdjustRankingLegendOverallPosition_GlobalMode(RankingMarkingLegend, Rank1Oval, Rank2Oval, Rank3Oval, Rank1Label, Rank2Label, Rank3Label, 550000, -10000, -100000);
                        }
                    }

                    if (CurrentOutput.MarkingColoring)
                    {
                        var rateDifflegend = RateDifferenceLegend.GetAnchor();
                        if (sigLgnd)
                        {
                            rateDifflegend.Dx1 += -130000;
                            rateDifflegend.Dx2 += 50000;

                            var level2HighPalette = Level2HighPalette.GetAnchor();
                            level2HighPalette.Dx1 += -110000;
                            level2HighPalette.Dx2 += -125000;
                            var level2LowPalette = Level2LowPalette.GetAnchor();
                            level2LowPalette.Dx1 += -110000;
                            level2LowPalette.Dx2 += -125000;
                            var level1HighPalette = Level1HighPalette.GetAnchor();
                            level1HighPalette.Dx1 += -110000;
                            level1HighPalette.Dx2 += -125000;
                            var level1LowPalette = Level1LowPalette.GetAnchor();
                            level1LowPalette.Dx1 += -110000;
                            level1LowPalette.Dx2 += -125000;
                        }
                        else
                        {
                            rateDifflegend.Dx1 += 610000;
                            rateDifflegend.Dx2 += 50000;

                            var level2HighPalette = Level2HighPalette.GetAnchor();
                            level2HighPalette.Dx1 += 610000;
                            level2HighPalette.Dx2 += 625000;
                            var level2LowPalette = Level2LowPalette.GetAnchor();
                            level2LowPalette.Dx1 += 610000;
                            level2LowPalette.Dx2 += 625000;
                            var level1HighPalette = Level1HighPalette.GetAnchor();
                            level1HighPalette.Dx1 += 610000;
                            level1HighPalette.Dx2 += 625000;
                            var level1LowPalette = Level1LowPalette.GetAnchor();
                            level1LowPalette.Dx1 += 610000;
                            level1LowPalette.Dx2 += 625000;
                        }

                        var level2lowlabel = Level2LowLabel.GetAnchor();
                        level2lowlabel.Dx1 -= 140000;
                        level2lowlabel.Dx2 += 340000;
                        var level1lowlabel = Level1LowLabel.GetAnchor();
                        level1lowlabel.Dx1 -= 140000;
                        level1lowlabel.Dx2 += 340000;
                        var level2Highlabel = Level2HighLabel.GetAnchor();
                        level2Highlabel.Dx1 -= 140000;
                        level2Highlabel.Dx2 += 340000;
                        var level1Highlabel = Level1HighLabel.GetAnchor();
                        level1Highlabel.Dx1 -= 140000;
                        level1Highlabel.Dx2 += 340000;
                    }
                }
                else
                {
                    if (CurrentOutput.MarkingRanking)
                    {
                        if (sigLgnd && rateDiffgnd)
                        {
                            var rankMarking = RankingMarkingLegend.GetAnchor();
                            rankMarking.Dx1 -= 100000;
                            rankMarking.Dx2 -= 100000;
                            AdjustRankingLegendOverallPosition_GlobalMode(RankingMarkingLegend, Rank1Oval, Rank2Oval, Rank3Oval, Rank1Label, Rank2Label, Rank3Label, 550000);
                        }
                        else if (!sigLgnd && !rateDiffgnd)
                        {
                            //RankingMarkingLegend.GetAnchor().Dx2 -= 100000;
                            AdjustRankingLegendOverallPosition_GlobalMode(RankingMarkingLegend, Rank1Oval, Rank2Oval, Rank3Oval, Rank1Label, Rank2Label, Rank3Label, 550000, -50000, -125000);
                        }
                        else if (sigLgnd)
                        {
                            //RankingMarkingLegend.GetAnchor().Dx2 -= 100000;
                            AdjustRankingLegendOverallPosition_GlobalMode(RankingMarkingLegend, Rank1Oval, Rank2Oval, Rank3Oval, Rank1Label, Rank2Label, Rank3Label, 550000, -50000, -125000);
                        }
                        else
                        {
                            RankingMarkingLegend.GetAnchor().Dx2 -= 100000;
                            AdjustRankingLegendOverallPosition_GlobalMode(RankingMarkingLegend, Rank1Oval, Rank2Oval, Rank3Oval, Rank1Label, Rank2Label, Rank3Label, 550000, -5000, -70000);
                        }
                    }

                    if (CurrentOutput.MarkingColoring)
                    {
                        var rateDifflegend = RateDifferenceLegend.GetAnchor();
                        rateDifflegend.Dx1 += -130000;
                        rateDifflegend.Dx2 += 50000;

                        var level2HighPalette = Level2HighPalette.GetAnchor();
                        level2HighPalette.Dx1 += -110000;
                        level2HighPalette.Dx2 += -125000;
                        var level2LowPalette = Level2LowPalette.GetAnchor();
                        level2LowPalette.Dx1 += -110000;
                        level2LowPalette.Dx2 += -125000;
                        var level1HighPalette = Level1HighPalette.GetAnchor();
                        level1HighPalette.Dx1 += -110000;
                        level1HighPalette.Dx2 += -125000;
                        var level1LowPalette = Level1LowPalette.GetAnchor();
                        level1LowPalette.Dx1 += -110000;
                        level1LowPalette.Dx2 += -125000;

                        var level2Highlabel = Level2HighLabel.GetAnchor();
                        level2Highlabel.Dx1 -= 120000;
                        level2Highlabel.Dx2 += 80000;
                        var level1Highlabel = Level1HighLabel.GetAnchor();
                        level1Highlabel.Dx1 = level2Highlabel.Dx1;
                        level1Highlabel.Dx2 = level2Highlabel.Dx2;
                        var level1lowlabel = Level1LowLabel.GetAnchor();
                        level1lowlabel.Dx1 = level2Highlabel.Dx1;
                        level1lowlabel.Dx2 = level2Highlabel.Dx2;
                        var level2lowlabel = Level2LowLabel.GetAnchor();
                        level2lowlabel.Dx1 = level2Highlabel.Dx1;
                        level2lowlabel.Dx2 = level2Highlabel.Dx2;
                    }
                }
                ContentsSheet.GetRow(8).HeightInPoints = (float)74;
            }
            else
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                {
                    if (CurrentOutput.MarkingRanking)
                    {
                        var rankMarking = RankingMarkingLegend.GetAnchor();
                        if (!sigLgnd && !rateDiffgnd)
                        {
                            rankMarking.Dx1 += 750000;
                        }
                        else if (!sigLgnd && rateDiffgnd)
                        {
                            rankMarking.Dx1 += 610000;
                            rankMarking.Dx2 += 645000;
                            var rank1Oval = Rank1Oval.GetAnchor();
                            var rank1Label = Rank1Label.GetAnchor();
                            var rank2Oval = Rank2Oval.GetAnchor();
                            var rank2Label = Rank2Label.GetAnchor();
                            var rank3Oval = Rank3Oval.GetAnchor();
                            var rank3Label = Rank3Label.GetAnchor();
                            rank1Oval.Dx1 += 700000;
                            rank1Oval.Dx2 += 700000;
                            rank1Label.Dx1 += 700000;
                            rank1Label.Dx2 += 700000;
                            rank2Oval.Dx1 += 700000;
                            rank2Oval.Dx2 += 700000;
                            rank2Label.Dx1 += 700000;
                            rank2Label.Dx2 += 700000;
                            rank3Oval.Dx1 += 700000;
                            rank3Oval.Dx2 += 700000;
                            rank3Label.Dx1 += 700000;
                            rank3Label.Dx2 += 700000;
                        }
                    }

                    if (CurrentOutput.MarkingColoring)
                    {
                        var rateDifflegend = RateDifferenceLegend.GetAnchor();
                        if (!sigLgnd)
                        {
                            rateDifflegend.Dx1 += 610000;
                            rateDifflegend.Dx2 += 50000;
                            var level2HighPalette = Level2HighPalette.GetAnchor();
                            level2HighPalette.Dx1 += 610000;
                            level2HighPalette.Dx2 += 625000;
                            var level2LowPalette = Level2LowPalette.GetAnchor();
                            level2LowPalette.Dx1 += 610000;
                            level2LowPalette.Dx2 += 625000;
                            var level1HighPalette = Level1HighPalette.GetAnchor();
                            level1HighPalette.Dx1 += 610000;
                            level1HighPalette.Dx2 += 625000;
                            var level1LowPalette = Level1LowPalette.GetAnchor();
                            level1LowPalette.Dx1 += 610000;
                            level1LowPalette.Dx2 += 625000;
                        }
                    }
                }
            }

            c = CurrentOutput.Tables.Count;
            if (Books == null)
            {
                ContentsValue = Array.CreateInstance(typeof(string),
                    new int[] { c, 7 },
                    new int[] { 1, 1 });
                n = c;
            }
            else
            {
                ContentsValue = Array.CreateInstance(typeof(string),
                    new int[] { MaxIndex - MinIndex + 1, 4 },
                    new int[] { MinIndex, 1 });
                n = MaxIndex - MinIndex + 1;
            }
            int st = 4;
            HyperlinkTargetCells = Array.CreateInstance(typeof(CellRangeAddress),
            new int[] { ContentsValue.GetUpperBound(0) - ContentsValue.GetLowerBound(0) + 1, ContentsValue.GetUpperBound(1) - st + 1 },
            new int[] { ContentsValue.GetLowerBound(0), st });

            HyperlinkTargetSheets = Array.CreateInstance(typeof(string),
                new int[] { ContentsValue.GetUpperBound(0) - ContentsValue.GetLowerBound(0) + 1, ContentsValue.GetUpperBound(1) - st + 1 },
                new int[] { ContentsValue.GetLowerBound(0), st });

            CellRangeAddress rowWith = CellRangeAddress.ValueOf(GetNamedRange((XSSFSheet)ContentsSheet, "Contents").RefersToFormula);
            if (n > 3)
            {
                int copyStartRow = rowWith.FirstRow + 1;
                ContentsSheet.ShiftRows(ContentsSheet.LastRowNum, ContentsSheet.LastRowNum, (n - 3));
                int lstRow = ContentsSheet.LastRowNum;
                while (copyStartRow < lstRow - 1)
                {
                    ContentsSheet.CopyRow(rowWith.FirstRow + 1, copyStartRow + 1);
                    ContentsSheet.GetRow(copyStartRow + 1).Height = 450;
                    copyStartRow++;
                }
                ContentsSheet.GetRow(copyStartRow + 1).Height = 450;
            }
            else if (n < 3)
            {
                if (n < 2)
                {
                    ContentsSheet.ShiftRows(ContentsSheet.LastRowNum - 1, ContentsSheet.LastRowNum - 1, -1);
                    ContentsSheet.ShiftRows(ContentsSheet.LastRowNum, ContentsSheet.LastRowNum, -2);
                    ContentsSheet.GetRow(ContentsSheet.LastRowNum).Height = 450;
                }
                else
                {
                    ContentsSheet.ShiftRows(15, 15, -1);
                    ContentsSheet.GetRow(13).Height = 450;
                    ContentsSheet.GetRow(14).Height = 450;
                }
            }
        }

        private void AdjustRankingLegendOverallPosition_GlobalMode(XSSFSimpleShape RankingMarkingLegend, XSSFSimpleShape Rank1Oval, XSSFSimpleShape Rank2Oval, XSSFSimpleShape Rank3Oval, XSSFSimpleShape Rank1Label, XSSFSimpleShape Rank2Label, XSSFSimpleShape Rank3Label, int Adjust_XaxisPosition, int Adjust_OvalaxisPosition = 100000, int Adjust_LabelAxisPosition = 50000, int labelAdjust = 150000)
        {
            var rank1Oval = Rank1Oval.GetAnchor();
            var rank1Label = Rank1Label.GetAnchor();
            var rank2Oval = Rank2Oval.GetAnchor();
            var rank2Label = Rank2Label.GetAnchor();
            var rank3Oval = Rank3Oval.GetAnchor();
            var rank3Label = Rank3Label.GetAnchor();
            var rankMarkingLegend = RankingMarkingLegend.GetAnchor();
            rankMarkingLegend.Dx1 -= (Adjust_XaxisPosition);
            //rankMarkingLegend.Dx2 += Adjust_X2axisPosition;
            int rankOvalSymbolWidth = rank2Oval.Dx2 - rank2Oval.Dx1 + 3000;

            rank1Oval.Dx1 -= (Adjust_XaxisPosition + Adjust_OvalaxisPosition);
            rank1Oval.Dx2 = rank1Oval.Dx1 + rankOvalSymbolWidth;
            rank1Oval.Dy1 = rank2Oval.Dy1;
            rank1Oval.Dy2 = rank2Oval.Dy2;

            rank1Label.Dx1 -= (Adjust_XaxisPosition + Adjust_LabelAxisPosition + labelAdjust);
            rank1Label.Dx2 = rank1Label.Dx1 + 150000;
            rank1Label.Dy1 = rank2Label.Dy1;
            rank1Label.Dy2 = rank2Label.Dy2;

            rank2Oval.Dx1 = rank1Oval.Dx1 + 300000;
            rank2Oval.Dx2 = rank2Oval.Dx1 + rankOvalSymbolWidth;
            rank2Label.Dx1 = rank1Label.Dx2 + labelAdjust;
            rank2Label.Dx2 = rank2Label.Dx1 + 200000;

            rank3Oval.Dx1 = rank2Oval.Dx1 + 350000;
            rank3Oval.Dx2 = rank3Oval.Dx1 + rankOvalSymbolWidth;
            rank3Oval.Dy1 = rank2Oval.Dy1;
            rank3Oval.Dy2 = rank2Oval.Dy2;

            rank3Label.Dx1 = rank2Label.Dx2 + labelAdjust;
            rank3Label.Dx2 = rank3Label.Dx1 + 250000;
            rank3Label.Dy1 = rank2Label.Dy1;
            rank3Label.Dy2 = rank2Label.Dy2;

            rank1Label.Dx2 += 50000;
        }

        private int findColumnForEmu(long v, ISheet s)
        {
            int i = 0;
            double vF = Convert.ToDouble(v);
            for (i = 0; i < 10; i++)
            {
                s.GetColumnWidthInPixels(i);
                vF = vF - s.GetColumnWidthInPixels(i) * Units.EMU_PER_PIXEL;
                if (vF < 0) return i;
            }
            return i;
        }

        public void RemoveNamedRange(XSSFSheet sheet, string rangeName, bool isNegativeCol = false)
        {
            int i = 0, j = 0, u = 0;

            IName name = GetNamedRange(sheet, rangeName);
            AreaReference area = new AreaReference(name.RefersToFormula);
            CellReference[] cells = area.GetAllReferencedCells();

            for (j = area.FirstCell.Row; j <= area.LastCell.Row; j++)
            {
                IRow r = sheet.GetRow(cells[i].Row);
                for (u = area.FirstCell.Col; u <= area.LastCell.Col; u++)
                {
                    ICell cell = r.GetCell(cells[i].Col);
                    r.RemoveCell(cell);
                    i++;
                }
            }
        }


        public static int ToInt(bool test)
        {
            return test ? -1 : 0;
        }

        private void CreateIndividualCross(string FormatPath, string TempPath, XlFileFormat FileFormat, string Suffix)
        {
            XSSFWorkbook FormatBook = null;
            XSSFWorkbook FormatBookN = null;
            XSSFWorkbook FormatBookP = null;
            XSSFWorkbook FormatBookT = null;
            XSSFSheet NPerFormatSheet = null;
            XSSFSheet NPerDoubleFormatSheet = null;
            XSSFSheet NFormatSheet = null;
            XSSFSheet NDoubleFormatSheet = null;
            XSSFSheet PerFormatSheet = null;
            XSSFSheet PerDoubleFormatSheet = null;
            XSSFSheet SigTestPerFormatSheet = null;
            XSSFSheet SigTestPerDoubleFormatSheet = null;
            //Worksheet FormatSheet = null;
            bool HasWeightColumn = false;
            int m = 0;
            bool HasWeightBack;
            bool HasWeight;
            Hashtable CutRowsCol = null;
            Hashtable CutColumnsCol = null;
            string FormatRangeNamePrefix;

            CrossTable tmpTable;
            string ReportTitle;
            List<XSSFWorkbook> NPerBooks = null;
            List<XSSFWorkbook> NBooks = null;
            List<XSSFWorkbook> PerBooks = null;
            List<XSSFWorkbook> SigTestBooks = null;
            Array NPerContentsValue = null;  //string
            Array NContentsValue = null; // string
            Array PerContentsValue = null;  //string
            Array SigTestContentsValue = null; // string
            Array NPerHyperLinkTargetCells = null; // Range
            Array NHyperLinkTargetCells = null;  //Range
            Array PerHyperLinkTargetCells = null; // Range
            Array SigTestHyperLinkTargetCells = null;  //Range
            Array NPerHyperlinkTargetSheets = null; //string
            Array NHyperlinkTargetSheets = null; //string
            Array PerHyperlinkTargetSheets = null; //string
            Array SigTestHyperlinkTargetSheets = null; //string
            XSSFSheet NPerContentsSheet = null;
            XSSFSheet NContentsSheet = null;
            XSSFSheet PerContentsSheet = null;
            XSSFSheet SigTestContentsSheet = null;
            List<XSSFSheet> NPerOrgSheets = null;
            List<XSSFSheet> NOrgSheets = null;
            List<XSSFSheet> PerOrgSheets = null;
            List<XSSFSheet> SigTestOrgSheets = null;
            //Workbook NewBook = null;
            XSSFSheet sht = null;
            XSSFSheet FormatSheet = null;
            XSSFName hyperLinkTargetName = null;
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
                            //Err().Raise vbObjectError + 100 &, RunningProcName, ThisWorkbook.LocalResource_ReportUnjustQuestionTypeMessageIndex)
                    }

                    if (HasWeight) { FormatRangeNamePrefix = FormatRangeNamePrefix + "_WT"; }
                    if (SigTestOn) { WholeRowCol = new Hashtable(); }
                    int medIdx = -1;
                    GetCutRowsAndColumns(tmpTable, HasWeightBack, HasWeight,
                        MaxAxesCountArray[i], ref CutRowsCol, ref CutColumnsCol, ref medIdx, false, CutMedian, WholeRowCol);
                    if (HasOutputNPerTable)
                    {
                        sht = CreateNewSheet(ref FormatBook, tmpTable, ref NPerContentsSheet, ref NPerContentsValue, ref NPerHyperLinkTargetCells,
                            ref NPerHyperlinkTargetSheets, ref NPerOrgSheets, FormatPath, ref NPerBooks, ref NPerFormatSheet, ref NPerDoubleFormatSheet,
                            HasWeightColumn, m, false, TableType.NPer);
                        CheckOverRow = true;
                        CheckOverColumn = false;
                        OverColumnsCount = 0;
                        Hashtable WholeRowColRef = null;// only for ref 
                        int OverRowssCountTmpRef = 0; // only for ref 
                        if (IsOrientationLandscape)
                        {
                            CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                   , 2, //wt
                                   1 + MaxAxesCountArray[i], HasWeight, isN, TableType.NPer
                                    , XLSX_MAX_ROW - 1, XLSX_MAX_COUMN - 2, ref CheckOverRow, WholeRowColRef, ref OverRowssCountTmpRef, ref OverColumnsCount);
                            if (OverColumnsCount > 0)
                            {
                                // ResumeError = true;
                                tableTypeBuf = LocalResource.REPORT_NP_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                                //    Err().Raise vbObjectError + 400&, RunningProcName _
                                //              , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailMessageIndex, tmpTable.Question.Name, tableTypeBuf)
                                //
                            }
                        }
                        else
                        {
                            CheckOverRow = true;
                            CheckOverColumn = true;
                            CreatePortraitCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                                  , 1 + (ToInt(HasWeight) & 1), 1 + MaxAxesCountArray[i], HasWeightColumn, HasWeight, isN
                                                  , TableType.NPer, XLSX_MAX_ROW - 1, XLSX_MAX_COUMN - 2, ref CheckOverRow, ref CheckOverColumn, ref OverRowssCountTmpRef, ref OverColumnsCount);
                        }

                        if (checkSimpleAggr(tmpTable)
                            && !string.IsNullOrEmpty(tmpTable.Question.QNumber)
                            //&& !string.IsNullOrEmpty(tmpTable.Question.TableHeading)    Redmine ID : #211718 
                            && tmpTable.AxesGroups.Count > 1)
                        {
                            NPerContentsValue.SetValue(tmpTable.Question.QNumber, i, 1);
                            NPerContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                            NPerContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                        }
                        else
                        {
                            NPerContentsValue.SetValue(tmpTable.Question.Name, i, 1);
                            NPerContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                            NPerContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                        }
                        if (CheckOverRow || CheckOverColumn)
                        {
                            xlApp.DisplayAlerts = false;
                            //      sht.Delete();
                            // NPerContentsValue.SetValue("Error", i, 4);
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

                                FormatLandscapeTableIndividual(FormatBook, FormatSheet, sht, ref hyperLinkTargetName, StdNumericCutCol, "1", tmpTable,
                                    CellRangeAddress.ValueOf("A1"), CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.NPer
                                       , HasWeight, CellRangeAddress.ValueOf("A1"), isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: v,
                                    CutMedian: CutMedian, MedIdx: medIdx);

                            }
                            else
                            {
                                FormatPortraitTable(FormatBook, FormatSheet, sht, ref hyperLinkTargetName, StdNumericCutCol, "1", tmpTable,
                                   CellRangeAddress.ValueOf("A1"), CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.NPer
                                      , HasWeight, CellRangeAddress.ValueOf("A1"), isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: v,
                                   CutMedian: CutMedian, MedIdx: medIdx);
                            }

                            if (bgWorker.CancellationPending) return;
                            CellRangeAddress insertValuerangeAddress2 = null;
                            if (IsOrientationLandscape)
                            {
                                insertValuerangeAddress = new CellRangeAddress(1, 1 + v.GetUpperBound(0) - 1, 2, 2 + v.GetUpperBound(1) - 1);
                                NpoiHelper.PutValue(sht as XSSFSheet, insertValuerangeAddress.FormatAsString(), v);
                                insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 1, insertValuerangeAddress.LastRow,
                                   insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                                NpoiHelper.PutValue(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue);
                            }
                            else
                            {
                                insertValuerangeAddress = new CellRangeAddress(1, v.GetUpperBound(0), 2, v.GetUpperBound(1) + 1);
                                NpoiHelper.PutValuePortraitTable(sht as XSSFSheet, insertValuerangeAddress.FormatAsString(), v);
                                int ac = DataValue.GetLowerBound(0);
                                int bd = DataValue.GetLowerBound(1);
                                insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0), insertValuerangeAddress.LastRow,
                                insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                                NpoiHelper.PutDataValuePortrait(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue);
                                insertValuerangeAddress.FirstRow = insertValuerangeAddress.FirstRow + 1;
                            }

                            _log.Info("Auto fit started");
                            //AutoFit(dataRange, colWidthMap);
                            if (/*Ranking != null*/false) // Autofit column set in Template
                            {
                                NpoiHelper.AutoFit(sht, insertValuerangeAddress.FirstColumn + Ranking.GetLowerBound(1),
        insertValuerangeAddress.LastColumn, insertValuerangeAddress.FirstRow + Ranking.GetLowerBound(0),
        insertValuerangeAddress.LastRow);
                            }
                            _log.Info("Auto fit started complted");

                            //_log.Info("AutoFitEx start");
                            //Range labelRange = WithshtResize.Worksheet.Range[WithshtResize.Item[DataValue.GetLowerBound(0) + (isN ? 1 : 2),
                            //    1], WithshtResize.Item[WithshtResize.Rows.Count, DataValue.GetLowerBound(1) - 1]];
                            //    OutputUtil.AutoFitEx(WithshtResize.Rows.Item[1], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitEx(WithshtResize.Rows.Item[2], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitEx(WithshtResize.Rows.Item[3], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            //OutputUtil.AutoFitExCrossLabel(labelRange, xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                            _log.Info("AutoFitEx completed");

                            if (HasWeight)
                            {
                                if (IsOrientationLandscape)
                                {
                                    insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 2, insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 2,
                                       insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1),
                                       insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1) + 1 + tmpTable.SectorsCount);
                                    NpoiHelper.PutValueNumeric(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString());
                                }
                                else if (IsOrientationPortrait)
                                {
                                    insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress2.FirstRow, insertValuerangeAddress2.LastRow,
                                      insertValuerangeAddress2.FirstColumn - 1, insertValuerangeAddress2.FirstColumn - 1);
                                    NpoiHelper.PutValueNumeric(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString());
                                }
                            }

                            if (!isN)
                            {
                                if (IsMarkingRanking) { RankMarking(insertValuerangeAddress, ref Ranking, sht); }
                                if (IsMarkingColoring) { Hatching(insertValuerangeAddress, ref HatchingColorIndex, sht, FormatBook, TableType.NPer); }
                                //if (IsMarkingAscending) { AscendingMarking(WithshtResize.Cells, ref ArrowEnd); }
                                if (IsMarkingSignificance) { SignificanceTestMarking(insertValuerangeAddress, ref SigTestMarking, sht, TableType.NPer); }
                            }
                            NPerContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                            NPerHyperLinkTargetCells.SetValue(CellRangeAddress.ValueOf("A1"), i, 4);
                            NPerHyperlinkTargetSheets.SetValue(sht.SheetName, i, 4);
                        }
                    }
                    if (HasOutputNTable)
                    {
                        sht = CreateNewSheet(ref FormatBookN, tmpTable, ref NContentsSheet, ref NContentsValue, ref NHyperLinkTargetCells,
                            ref NHyperlinkTargetSheets, ref NOrgSheets, FormatPath, ref NBooks, ref NFormatSheet, ref NDoubleFormatSheet,
                            HasWeightColumn, m, false, TableType.N);


                        CheckOverRow = true;
                        CheckOverColumn = false;
                        OverColumnsCount = 0;
                        Hashtable WholeRowColRef = null;// only for ref 
                        int OverRowssCountTmpRef = 0; // only for ref 
                        if (IsOrientationLandscape)
                        {
                            CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                                    , 2 //wt
                                                    , 1 + MaxAxesCountArray[i], HasWeight, isN, TableType.N
                                                    , XLSX_MAX_ROW - 1, XLSX_MAX_COUMN - 2, ref CheckOverRow, WholeRowColRef, ref OverRowssCountTmpRef, ref OverColumnsCount);
                            if (OverColumnsCount > 0)
                            {
                                //ResumeError = true
                                tableTypeBuf = LocalResource.REPORT_N_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                                //Err().Raise vbObjectError + 400&, RunningProcName _
                                //          , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailMessageIndex, tmpTable.Question.Name, tableTypeBuf)
                            }
                        }
                        else
                        {
                            CreatePortraitCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                              , 1 + (ToInt(HasWeight) & 1), 1 + MaxAxesCountArray[i], HasWeightColumn, HasWeight, isN
                                              , TableType.N, XLSX_MAX_ROW - 1, XLSX_MAX_COUMN - 2, ref CheckOverRow, ref CheckOverColumn, ref OverRowssCountTmpRef, ref OverColumnsCount);

                        }

                        if (checkSimpleAggr(tmpTable)
                            && !string.IsNullOrEmpty(tmpTable.Question.QNumber)
                            //&& !string.IsNullOrEmpty(tmpTable.Question.TableHeading)
                            && tmpTable.AxesGroups.Count > 1)
                        {
                            NContentsValue.SetValue(tmpTable.Question.QNumber, i, 1);
                            NContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                            NContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                        }
                        else
                        {
                            NContentsValue.SetValue(tmpTable.Question.Name, i, 1);
                            NContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                            NContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                        }
                        if (CheckOverRow || CheckOverColumn)
                        {
                            xlApp.DisplayAlerts = false;
                            //sht.Delete();
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
                                FormatLandscapeTableIndividual(FormatBookN, FormatSheet, sht, ref hyperLinkTargetName, StdNumericCutCol, "1", tmpTable,
                                    CellRangeAddress.ValueOf("A1"), CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.N
                                       , HasWeight, CellRangeAddress.ValueOf("A1"), isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: v,
                                    CutMedian: CutMedian, MedIdx: medIdx);
                            }
                            else
                            {
                                FormatPortraitTable(FormatBookN, FormatSheet, sht, ref hyperLinkTargetName, StdNumericCutCol, "1", tmpTable,
                                   CellRangeAddress.ValueOf("A1"), CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.N
                                      , HasWeight, CellRangeAddress.ValueOf("A1"), isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: v,
                                   CutMedian: CutMedian, MedIdx: medIdx);
                            }

                            if (bgWorker.CancellationPending) return;
                            CellRangeAddress insertValuerangeAddress2 = null;
                            if (IsOrientationLandscape)
                            {
                                insertValuerangeAddress = new CellRangeAddress(1, 1 + v.GetUpperBound(0) - 1, 2, 2 + v.GetUpperBound(1) - 1);
                                NpoiHelper.PutValue(sht as XSSFSheet, insertValuerangeAddress.FormatAsString(), v);
                                insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 1, insertValuerangeAddress.LastRow,
                                  insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                                NpoiHelper.PutValue(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue);
                            }
                            else
                            {
                                insertValuerangeAddress = new CellRangeAddress(1, v.GetUpperBound(0), 2, v.GetUpperBound(1) + 1);
                                NpoiHelper.PutValuePortraitTable(sht as XSSFSheet, insertValuerangeAddress.FormatAsString(), v);
                                insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0), insertValuerangeAddress.LastRow,
                                insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                                NpoiHelper.PutDataValuePortrait(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue);
                                insertValuerangeAddress.FirstRow = insertValuerangeAddress.FirstRow + 1;
                            }

                            if (/*Ranking != null*/false)//Autofit column set in Template
                            {
                                NpoiHelper.AutoFit(sht, insertValuerangeAddress.FirstColumn + Ranking.GetLowerBound(1),
        insertValuerangeAddress.LastColumn, insertValuerangeAddress.FirstRow + Ranking.GetLowerBound(0),
        insertValuerangeAddress.LastRow);
                            }

                            if (HasWeight)
                            {
                                if (IsOrientationLandscape)
                                {
                                    insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 2, insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 2,
                                       insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1),
                                       insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1) + 1 + tmpTable.SectorsCount);
                                    NpoiHelper.PutValueNumeric(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString());
                                }
                                else if (IsOrientationPortrait)
                                {
                                    insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress2.FirstRow, insertValuerangeAddress2.LastRow,
                                      insertValuerangeAddress2.FirstColumn - 1, insertValuerangeAddress2.FirstColumn - 1);
                                    NpoiHelper.PutValueNumeric(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString());
                                }
                            }
                            if (!isN)
                            {
                                if (IsMarkingRanking) { RankMarking(insertValuerangeAddress, ref Ranking, sht); }
                                if (IsMarkingColoring) { Hatching(insertValuerangeAddress, ref HatchingColorIndex, sht, FormatBookN, TableType.N); }
                                //if (IsMarkingAscending) { AscendingMarking(WithShtRangeResize.Cells, ref ArrowEnd); }
                                //if (IsMarkingSignificance) { SignificanceTestMarking(WithShtRangeResize.Cells, ref SigTestMarking); }
                            }

                            NContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                            NHyperLinkTargetCells.SetValue(CellRangeAddress.ValueOf("A1"), i, 4);
                            NHyperlinkTargetSheets.SetValue(sht.SheetName, i, 4);
                        }
                    }
                    if (HasOutputPerTable)
                    {
                        sht = CreateNewSheet(ref FormatBookP, tmpTable, ref PerContentsSheet, ref PerContentsValue, ref PerHyperLinkTargetCells,
                            ref PerHyperlinkTargetSheets, ref PerOrgSheets, FormatPath, ref PerBooks, ref PerFormatSheet, ref PerDoubleFormatSheet,
                            HasWeightColumn, m, false, TableType.Per);

                        CheckOverRow = true;
                        CheckOverColumn = false;
                        OverColumnsCount = 0;
                        Hashtable WholeRowColRef = null;// only for ref 
                        int OverRowssCountTmpRef = 0; // only for ref 
                        if (IsOrientationLandscape)
                        {
                            CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                                    , 2 //wt
                                                    , 1 + MaxAxesCountArray[i], HasWeight, isN, TableType.Per
                                                    , XLSX_MAX_ROW - 1, XLSX_MAX_COUMN - 2, ref CheckOverRow, WholeRowColRef, ref OverRowssCountTmpRef, ref OverColumnsCount);
                            if (OverColumnsCount > 0)
                            {
                                // ResumeError = true
                                tableTypeBuf = LocalResource.REPORT_P_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                                //tableTypeBuf = ThisWorkbook.LocalResource_ReportPKeywordIndex)
                                //        Err().Raise vbObjectError + 400&, RunningProcName _
                                //                  , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailMessageIndex, tmpTable.Question.Name, tableTypeBuf)
                            }
                        }
                        else
                        {
                            CreatePortraitCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                                 , 1 + (ToInt(HasWeight) & 1), 1 + MaxAxesCountArray[i], HasWeightColumn, HasWeight, isN
                                                 , TableType.Per, XLSX_MAX_ROW - 1, XLSX_MAX_COUMN - 2, ref CheckOverRow, ref CheckOverColumn, ref OverRowssCountTmpRef, ref OverColumnsCount);
                        }

                        if (checkSimpleAggr(tmpTable)
                            && !string.IsNullOrEmpty(tmpTable.Question.QNumber)
                            //&& !string.IsNullOrEmpty(tmpTable.Question.TableHeading)
                            && tmpTable.AxesGroups.Count > 1)
                        {
                            PerContentsValue.SetValue(tmpTable.Question.QNumber, i, 1);
                            PerContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                            PerContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                        }
                        else
                        {
                            PerContentsValue.SetValue(tmpTable.Question.Name, i, 1);
                            PerContentsValue.SetValue(tmpTable.Question.TableHeading, i, 2);
                            PerContentsValue.SetValue(tmpTable.Question.Description, i, 3);
                        }
                        if (CheckOverRow || CheckOverColumn)
                        {
                            //xlApp.DisplayAlerts = false;
                            //sht.Delete();
                            //PerContentsValue.SetValue("Error", i, 4);
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
                                FormatLandscapeTableIndividual(FormatBookP, FormatSheet, sht, ref hyperLinkTargetName, StdNumericCutCol, "1", tmpTable,
                                    CellRangeAddress.ValueOf("A1"), CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.Per
                                       , HasWeight, CellRangeAddress.ValueOf("A1"), isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: v,
                                    CutMedian: CutMedian, MedIdx: medIdx);
                            }
                            else
                            {
                                FormatPortraitTable(FormatBookP, FormatSheet, sht, ref hyperLinkTargetName, StdNumericCutCol, "1", tmpTable,
                                   CellRangeAddress.ValueOf("A1"), CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.Per
                                      , HasWeight, CellRangeAddress.ValueOf("A1"), isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: v,
                                   CutMedian: CutMedian, MedIdx: medIdx);
                            }
                            CellRangeAddress insertValuerangeAddress2 = null;
                            if (bgWorker.CancellationPending) return;
                            if (IsOrientationLandscape)
                            {
                                insertValuerangeAddress = new CellRangeAddress(1, 1 + v.GetUpperBound(0) - 1, 2, 2 + v.GetUpperBound(1) - 1);
                                NpoiHelper.PutValue(sht as XSSFSheet, insertValuerangeAddress.FormatAsString(), v);
                                insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 1, insertValuerangeAddress.LastRow,
                                  insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                                NpoiHelper.PutValue(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue);
                            }
                            else
                            {
                                insertValuerangeAddress = new CellRangeAddress(1, v.GetUpperBound(0), 2, v.GetUpperBound(1) + 1);
                                NpoiHelper.PutValuePortraitTable(sht as XSSFSheet, insertValuerangeAddress.FormatAsString(), v);
                                insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0), insertValuerangeAddress.LastRow,
                                insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                                NpoiHelper.PutDataValuePortrait(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue);
                                insertValuerangeAddress.FirstRow = insertValuerangeAddress.FirstRow + 1;
                            }

                            if (/*Ranking != null*/false) //Autofit column set in Template
                            {
                                NpoiHelper.AutoFit(sht, insertValuerangeAddress.FirstColumn + Ranking.GetLowerBound(1),
        insertValuerangeAddress.LastColumn, insertValuerangeAddress.FirstRow + Ranking.GetLowerBound(0),
        insertValuerangeAddress.LastRow);
                            }
                            if (HasWeight)
                            {
                                if (IsOrientationLandscape)
                                {
                                    insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 2, insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 2,
                                       insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1),
                                       insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1) + 1 + tmpTable.SectorsCount);
                                    NpoiHelper.PutValueNumeric(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString());
                                }
                                else if (IsOrientationPortrait)
                                {
                                    insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress2.FirstRow, insertValuerangeAddress2.LastRow,
                                      insertValuerangeAddress2.FirstColumn - 1, insertValuerangeAddress2.FirstColumn - 1);
                                    NpoiHelper.PutValueNumeric(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString());
                                }
                            }
                            if (!isN)
                            {
                                if (IsMarkingRanking) { RankMarking(insertValuerangeAddress, ref Ranking, sht); }
                                if (IsMarkingColoring) { Hatching(insertValuerangeAddress, ref HatchingColorIndex, sht, FormatBookP, TableType.Per); }
                                //if (IsMarkingAscending) { AscendingMarking(WithshtResize.Cells, ref ArrowEnd); }
                                if (IsMarkingSignificance) { SignificanceTestMarking(insertValuerangeAddress, ref SigTestMarking, sht, TableType.Per); }
                            }
                            PerContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                            PerHyperLinkTargetCells.SetValue(CellRangeAddress.ValueOf("A1"), i, 4);
                            PerHyperlinkTargetSheets.SetValue(sht.SheetName, i, 4);
                        }
                    }
                    ////        DoEvents
                    if (SigTestOn)
                    {
                        if (checkSimpleAggr(tmpTable) && !isN && !IsOrientationPortrait)
                        {
                            FormatRangeNamePrefix = FormatRangeNamePrefix + "_NP";
                        }
                        //sht = CreateNewSheet(FormatBookT, tmpTable, ref SigTestOrgSheets, true);
                        sht = CreateNewSheet(ref FormatBookT, tmpTable, ref SigTestContentsSheet, ref SigTestContentsValue, ref SigTestHyperLinkTargetCells,
                            ref SigTestHyperlinkTargetSheets, ref SigTestOrgSheets, FormatPath, ref SigTestBooks, ref SigTestPerFormatSheet,
                            ref SigTestPerDoubleFormatSheet, HasWeightColumn, m, true, TableType.SignificanceTest);
                        if (IsOrientationLandscape)
                        {
                            CheckOverRow = true;
                            CheckOverColumn = false;
                            OverColumnsCount = 0;
                            int OverRowssCountTmpRef = 0; // only for ref 
                            CreateLandscapeCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd,
                                ref SigTestMarking, 2, //wt
                                1 + MaxAxesCountArray[i], HasWeight, isN, TableType.SignificanceTest
                                 , XLSX_MAX_ROW - 1, XLSX_MAX_COUMN - 2, ref CheckOverRow, WholeRowCol, ref OverRowssCountTmpRef, ref OverColumnsCount);
                            if (OverColumnsCount > 0)
                            {
                                tableTypeBuf = LocalResource.REPORT_SIGNIFICANCE_TEST_KEYWORD;
                                throw new Exception(string.Format(LocalResource.REPORT_COLUMNS_COUNT_OVER_DETAIL_MESSAGE, tmpTable.Question.Name, tableTypeBuf));
                                //esumeError = true
                                //   tableTypeBuf = ThisWorkbook.LocalResource_ReportSignificanceTestKeywordIndex)
                                //  Err().Raise vbObjectError + 400&, RunningProcName _
                                //          , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailMessageIndex, tmpTable.Question.Name, tableTypeBuf)
                            }
                        }
                        else
                        {
                            CheckOverRow = true;
                            CheckOverColumn = false;
                            OverColumnsCount = 0;
                            int OverRowssCountTmpRef = 0;
                            CreatePortraitCrossArray(tmpTable, CutRowsCol, CutColumnsCol, ref v, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                                , 1 + (ToInt(HasWeight) & 1), 1 + MaxAxesCountArray[i], HasWeightColumn, HasWeight, isN
                                                , TableType.SignificanceTest, XLSX_MAX_ROW - 1, XLSX_MAX_COUMN - 2, ref CheckOverRow, ref CheckOverColumn, ref OverRowssCountTmpRef, ref OverColumnsCount);

                        }
                        if (checkSimpleAggr(tmpTable)
                            && !string.IsNullOrEmpty(tmpTable.Question.QNumber)
                            //&& !string.IsNullOrEmpty(tmpTable.Question.TableHeading)
                            && tmpTable.AxesGroups.Count > 1)
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
                                FormatLandscapeTableIndividual(FormatBookT, FormatSheet, sht, ref hyperLinkTargetName, StdNumericCutCol, "1", tmpTable,
                                                                    CellRangeAddress.ValueOf("A1"), CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.SignificanceTest
                                                                       , HasWeight, CellRangeAddress.ValueOf("A1"), isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: v,
                                                                    CutMedian: CutMedian, MedIdx: medIdx);
                            }
                            else
                            {
                                FormatPortraitTable(FormatBookT, FormatSheet, sht, ref hyperLinkTargetName, StdNumericCutCol, "1", tmpTable,
                                   CellRangeAddress.ValueOf("A1"), CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType.SignificanceTest
                                      , HasWeight, CellRangeAddress.ValueOf("A1"), isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: v,
                                   CutMedian: CutMedian, MedIdx: medIdx);
                            }
                            if (bgWorker.CancellationPending) return;
                            CellRangeAddress insertValuerangeAddress2 = null;
                            if (IsOrientationLandscape)
                            {
                                insertValuerangeAddress = new CellRangeAddress(1, 1 + v.GetUpperBound(0) - 1, 2, 2 + v.GetUpperBound(1) - 1);
                                NpoiHelper.PutValue(sht as XSSFSheet, insertValuerangeAddress.FormatAsString(), v);
                                insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 1, insertValuerangeAddress.LastRow,
                                insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                                NpoiHelper.PutValue(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue);
                            }
                            else
                            {
                                if (!checkSimpleAggr(tmpTable))
                                {
                                    insertValuerangeAddress = new CellRangeAddress(1, v.GetUpperBound(0), 2, v.GetUpperBound(1) + 1);
                                    NpoiHelper.PutValuePortraitTable(sht as XSSFSheet, insertValuerangeAddress.FormatAsString(), v);
                                    insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0), insertValuerangeAddress.LastRow,
                                    insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                                    NpoiHelper.PutDataValuePortrait(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue);
                                }
                                else
                                {
                                    insertValuerangeAddress = new CellRangeAddress(1, v.GetUpperBound(0), 2, v.GetUpperBound(1) + 1);
                                    NpoiHelper.PutValuePortraitTable(sht as XSSFSheet, insertValuerangeAddress.FormatAsString(), v, false, null, 4, 7, simpleaggr: true);
                                    insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0), insertValuerangeAddress.LastRow,
                                    insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                                    NpoiHelper.PutDataValuePortrait(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue);
                                }
                            }

                            if (HasWeight)
                            {
                                if (IsOrientationLandscape)
                                {
                                    insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 2, insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0) - 2,
                                       insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1),
                                       insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) + (ToInt(HasWeightBack) & 1) + 1 + tmpTable.SectorsCount);
                                    NpoiHelper.PutValueNumeric(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString());
                                }
                                else if (IsOrientationPortrait)
                                {
                                    insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress2.FirstRow, insertValuerangeAddress2.LastRow,
                                      insertValuerangeAddress2.FirstColumn - 1, insertValuerangeAddress2.FirstColumn - 1);
                                    NpoiHelper.PutValueNumeric(sht as XSSFSheet, insertValuerangeAddress2.FormatAsString());
                                }
                            }
                            SigTestContentsValue.SetValue("TABLE[" + tmpTable.Question.Name + "]", i, 4);
                            SigTestHyperLinkTargetCells.SetValue(CellRangeAddress.ValueOf("A1"), i, 4);
                            SigTestHyperlinkTargetSheets.SetValue(sht.SheetName, i, 4);
                        }
                    }
                    ////        DoEvents
                }
                if (HasOutputNPerTable)
                {
                    n1 = NPerOverRowsQs.GetUpperBound(0);
                    if (IsOrientationPortrait)
                    {
                        n2 = NPerOverColumnsQs.GetUpperBound(0) + 1;
                    }
                    n = n1 + n2;
                    errBuf = (n1 > 0) ? Qc4Launcher.LocalResource.REPORT_ROWS_COUNT_OVER_KEYWORD : ""
                    + ((n1 > 0 && n2 > 0) ? Qc4Launcher.LocalResource.REPORT_AND_CONJUNCTION : "")
                    + ((n2 > 0) ? Qc4Launcher.LocalResource.REPORT_COLUMNS_COUNT_OVER_KEYWORD : "");
                    if (n == CurrentOutput.Tables.Count)
                    {
                        if (NPerBooks != null)
                        {
                            for (i = NPerBooks.Count; i >= 1; i--)
                            {
                                //   NPerBooks[i - 1].Close(false);
                            }
                            NPerBooks = null;
                        }
                        //if (NBooks != null || PerBooks != null || SigTestBooks != null)
                        //{
                        //    //ResumeError = true;
                        //    //Err().Raise vbObjectError + 1100&, RunningProcName _
                        //    //          , ThisWorkbook.LocalResource_ReportOutputIndividualCrossNPMessageIndex, errBuf)

                        //    throw new Exception(LocalResource.ReportOutputIndividualCrossNPMessageIndex, errBuf));
                        //}
                        //else
                        //{
                        //    // Err().Raise vbObjectError + 1000 &, RunningProcName _
                        //    //            , ThisWorkbook.LocalResource_ReportOutputIndividualCrossErrorMessageIndex, errBuf)
                        //    throw new Exception(LocalResource.ReportOutputIndividualCrossErrorMessageIndex, errBuf));
                        //}
                    }
                    else
                    {
                        if (n > 0)
                        {
                            if (n1 > 0)
                            {
                                //  ResumeError = true
                                // Err().Raise vbObjectError + 1110&, RunningProcName _
                                //         , ThisWorkbook.LocalResource_ReportRowsCountOverIndividualCrossesNPMessageIndex, Join(NPerOverRowsQs, " , "))
                                throw new Exception(string.Format(Qc4Launcher.LocalResource.REPORT_ROWS_COUNT_OVER_INDIVIDUAL_CROSSES_NP_MESSAGE, String.Join(" , ", NPerOverRowsQs)));
                            }
                            if (n2 > 0)
                            {
                                //  ResumeError = true
                                //  Err().Raise vbObjectError + 1120&, RunningProcName _
                                //           , ThisWorkbook.LocalResource_ReportColumnsCountOverIndividualCrossesNPMessageIndex, Join(NPerOverColumnsQs, " , "))
                                throw new Exception(string.Format(Qc4Launcher.LocalResource.REPORT_COLUMNS_COUNT_OVER_INDIVIDUAL_CROSSES_NP_MESSAGE, String.Join(" , ", NPerOverColumnsQs)));
                            }
                        }
                        PutContents(NPerContentsSheet, ref NPerContentsValue, ref NPerHyperLinkTargetCells, ref NPerHyperlinkTargetSheets, NPerOrgSheets);
                    }
                }
                if (HasOutputNTable)
                {
                    PutContents(NContentsSheet, ref NContentsValue, ref NHyperLinkTargetCells, ref NHyperlinkTargetSheets, NPerOrgSheets);
                }
                //    n1 = NOverRowsQs.GetUpperBound(0);
                //    if (IsOrientationPortrait)
                //    {
                //        n2 = NOverColumnsQs.GetUpperBound(0) + 1;
                //    }
                //    n = n1 + n2;
                //    errBuf = (n1 > 0 ? LocalResource.ReportRowsCountOverKeywordIndex) : "")
                //           + (n1 > 0 && n2 > 0 ? LocalResource.ReportAndConjunctionIndex) : "")
                //           + (n2 > 0 ? LocalResource.ReportColumnsCountOverKeywordIndex) : "");
                //    if (n == CurrentOutput.Tables.Count)
                //    {
                //        if (NBooks != null)
                //        {
                //            for (i = NBooks.Count; i >= 1; i--)
                //            {
                //                NBooks[i - 1].Close(false);
                //            }
                //            NBooks = null;
                //        }
                //        if (NPerBooks != null || PerBooks != null || SigTestBooks != null)
                //        {
                //            //   ResumeError = true
                //            // Err().Raise vbObjectError + 1200&, RunningProcName _
                //            //         , ThisWorkbook.LocalResource_ReportOutputIndividualCrossNMessageIndex, errBuf)
                //            throw new Exception(LocalResource.ReportOutputIndividualCrossNMessageIndex, errBuf));
                //        }
                //        else
                //        {
                //            //Err().Raise vbObjectError + 1000&, RunningProcName _
                //            //        , ThisWorkbook.LocalResource_ReportOutputIndividualCrossErrorMessageIndex, errBuf)
                //            throw new Exception(LocalResource.ReportOutputIndividualCrossErrorMessageIndex, errBuf));
                //        }
                //    }
                //    else
                //    {
                //        if (n > 0)
                //        {
                //            if (n1 > 0)
                //            {
                //                //ResumeError = true
                //                //Err().Raise vbObjectError + 1210&, RunningProcName _
                //                //        , ThisWorkbook.LocalResource_ReportRowsCountOverIndividualCrossesNMessageIndex, Join(NOverRowsQs, " , "))
                //                throw new Exception(LocalResource.ReportRowsCountOverIndividualCrossesNMessageIndex, String.Join(" , ", NOverRowsQs)));
                //            }
                //            if (n2 > 0)
                //            {
                //                //     ResumeError = true
                //                //      Err().Raise vbObjectError + 1220&, RunningProcName _
                //                //             , ThisWorkbook.LocalResource_ReportColumnsCountOverIndividualCrossesNMessageIndex, Join(NOverColumnsQs, " , "))
                //                throw new Exception(LocalResource.ReportColumnsCountOverIndividualCrossesNMessageIndex, String.Join(" , ", NOverColumnsQs)));
                //            }
                //        }
                //        PutContents(NContentsSheet, ref NContentsValue, ref NHyperLinkTargetCells, xlApp, NOrgSheets);
                //    }
                //}
                if (HasOutputPerTable)
                {
                    PutContents(PerContentsSheet, ref PerContentsValue, ref PerHyperLinkTargetCells, ref PerHyperlinkTargetSheets, PerOrgSheets);
                }
                //    n1 = PerOverRowsQs.GetUpperBound(0);
                //    if (IsOrientationPortrait)
                //    {
                //        n2 = PerOverColumnsQs.GetUpperBound(0) + 1;
                //    }
                //    n = n1 + n2;
                //    errBuf = (n1 > 0 ? LocalResource.ReportRowsCountOverKeywordIndex) : "")
                //            + (n1 > 0 && n2 > 0 ? LocalResource.ReportAndConjunctionIndex) : "")
                //              + (n2 > 0 ? LocalResource.ReportColumnsCountOverKeywordIndex) : "");
                //    if (n == CurrentOutput.Tables.Count)
                //    {
                //        if (PerBooks != null)
                //        {
                //            for (i = PerBooks.Count; i >= 1; i--)
                //            {
                //                PerBooks[i - 1].Close(false);
                //            }
                //            PerBooks = null;
                //        }
                //        if (NPerBooks != null || NBooks != null || SigTestBooks != null)
                //        {
                //            //ResumeError = true
                //            //Err().Raise vbObjectError + 1300 &, RunningProcName _
                //            //         , ThisWorkbook.LocalResource_ReportOutputIndividualCrossPMessageIndex, errBuf)
                //            throw new Exception(LocalResource.ReportOutputIndividualCrossPMessageIndex));
                //        }
                //        else
                //        {
                //            //  Err().Raise vbObjectError + 1000 &, RunningProcName _
                //            //              , ThisWorkbook.LocalResource_ReportOutputIndividualCrossErrorMessageIndex, errBuf)
                //            throw new Exception(LocalResource.ReportOutputIndividualCrossErrorMessageIndex));
                //        }
                //    }
                //    else
                //    {
                //        if (n > 0)
                //        {
                //            if (n1 > 0)
                //            {
                //                //ResumeError = true
                //                //      Err().Raise vbObjectError + 1310 &, RunningProcName _
                //                //               , ThisWorkbook.LocalResource_ReportRowsCountOverIndividualCrossesPMessageIndex, Join(PerOverRowsQs, " , "))
                //                throw new Exception(LocalResource.ReportRowsCountOverIndividualCrossesPMessageIndex));
                //            }
                //            if (n2 > 0)
                //            {
                //                //    ResumeError = true
                //                //          Err().Raise vbObjectError + 1320 &, RunningProcName _
                //                //                   , ThisWorkbook.LocalResource_ReportColumnsCountOverIndividualCrossesPMessageIndex, Join(PerOverColumnsQs, " , "))
                //                throw new Exception(LocalResource.ReportColumnsCountOverIndividualCrossesPMessageIndex));
                //            }
                //        }
                //        PutContents(PerContentsSheet, ref PerContentsValue, ref PerHyperLinkTargetCells, xlApp, PerOrgSheets);
                //    }
                //}
                if (SigTestOn)
                {
                    PutContents(SigTestContentsSheet, ref SigTestContentsValue, ref SigTestHyperLinkTargetCells, ref SigTestHyperlinkTargetSheets, SigTestOrgSheets);
                }
                //    n1 = SigTestOverRowsQs.GetUpperBound(0);
                //    if (IsOrientationPortrait)
                //    {
                //        n2 = SigTestOverColumnsQs.GetUpperBound(0) + 1;
                //    }
                //    n = n1 + n2;
                //    errBuf = (n1 > 0 ? LocalResource.ReportRowsCountOverKeywordIndex) : "")
                //           + (n1 > 0 && n2 > 0 ? LocalResource.ReportAndConjunctionIndex) : "")
                //           + (n2 > 0 ? LocalResource.ReportColumnsCountOverKeywordIndex) : "");
                //    if (n == CurrentOutput.Tables.Count)
                //    {
                //        if (SigTestBooks != null)
                //        {
                //            for (i = SigTestBooks.Count; i >= 1; i--)
                //            {
                //                SigTestBooks[i - 1].Close(false);
                //            }
                //            SigTestBooks = null;
                //        }
                //        if (NPerBooks != null || NBooks != null || PerBooks != null)
                //        {
                //            //    ResumeError = true
                //            //  Err().Raise vbObjectError + 1400&, RunningProcName _
                //            //          , ThisWorkbook.LocalResource_ReportOutputIndividualCrossSignificanceTestMessageIndex, errBuf)
                //            throw new Exception(LocalResource.ReportOutputIndividualCrossSignificanceTestMessageIndex));
                //        }
                //        else
                //        {
                //            //Err().Raise vbObjectError + 1000&, RunningProcName _
                //            //        , ThisWorkbook.LocalResource_ReportOutputIndividualCrossErrorMessageIndex, errBuf)
                //            throw new Exception(LocalResource.ReportOutputIndividualCrossErrorMessageIndex));
                //        }
                //    }
                //    else
                //    {
                //        if (n > 0)
                //        {
                //            if (n1 > 0)
                //            {
                //                //ResumeError = true
                //                // Err().Raise vbObjectError + 1410&, RunningProcName _
                //                //        , ThisWorkbook.LocalResource_ReportRowsCountOverIndividualCrossesSignificanceTestMessageIndex, Join(SigTestOverRowsQs, " , "))
                //                throw new Exception(LocalResource.ReportRowsCountOverIndividualCrossesSignificanceTestMessageIndex));
                //            }
                //            if (n2 > 0)
                //            {
                //                //    ResumeError = true
                //                //  Err().Raise vbObjectError + 1420&, RunningProcName _
                //                //          , ThisWorkbook.LocalResource_ReportColumnsCountOverIndividualCrossesSignificanceTestMessageIndex, Join(SigTestOverColumnsQs, " , "))
                //                throw new Exception(LocalResource.ReportColumnsCountOverIndividualCrossesSignificanceTestMessageIndex));
                //            }
                //        }
                //        PutContents(SigTestContentsSheet, ref SigTestContentsValue, ref SigTestHyperLinkTargetCells, xlApp, SigTestOrgSheets);
                //    }
                //}
                //if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0)
                //{
                tmpPrefix = CurrentOutput.ParentReportset.DivName + CurrentOutput.ExcelBookNamePrefix;

                i = 0;
                if (null != NPerBooks)
                {
                    foreach (XSSFWorkbook NewBook in NPerBooks)
                    {
                        i = i + 1;
                        if (OutputDirectoryPath != null)
                        {
                            SaveBook(NewBook, tmpPrefix + "_np", Suffix + (NPerBooks.Count > 1 ? "_" + i : ""), FileFormat, NPerOrgSheets, NPerContentsSheet);
                        }
                        else
                        {
                            SaveWorkBook(NewBook, NPerOrgSheets, tmpPrefix + "_np", NPerContentsSheet);
                        }
                    }
                }
                i = 0;
                if (null != NBooks)
                {
                    foreach (XSSFWorkbook NewBook in NBooks)
                    {
                        i = i + 1;
                        if (OutputDirectoryPath != null)
                        {
                            SaveBook(NewBook, tmpPrefix + "_n", Suffix + (NBooks.Count > 1 ? "_" + i : ""), FileFormat, NOrgSheets, NContentsSheet);
                        }
                        else
                        {
                            SaveWorkBook(NewBook, NOrgSheets, tmpPrefix + "_n", NContentsSheet);
                        }
                    }
                }
                i = 0;
                if (null != PerBooks)
                {
                    foreach (XSSFWorkbook NewBook in PerBooks)
                    {
                        i = i + 1;
                        if (OutputDirectoryPath != null)
                        {
                            SaveBook(NewBook, tmpPrefix + "_p", Suffix + (PerBooks.Count > 1 ? "_" + i : ""), FileFormat, PerOrgSheets, PerContentsSheet);
                        }
                        else
                        {
                            SaveWorkBook(NewBook, PerOrgSheets, tmpPrefix + "_p", PerContentsSheet);
                        }
                    }
                }
                i = 0;
                if (null != SigTestBooks)
                {
                    foreach (XSSFWorkbook NewBook in SigTestBooks)
                    {
                        i = i + 1;
                        if (OutputDirectoryPath != null)
                        {
                            SaveBook(NewBook, tmpPrefix + "_ps", Suffix + (SigTestBooks.Count > 1 ? "_" + i : ""), FileFormat, SigTestOrgSheets, SigTestContentsSheet);
                        }
                        else
                        {
                            SaveWorkBook(NewBook, SigTestOrgSheets, tmpPrefix + "_ps", SigTestContentsSheet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message);
                _log.Info(ex.StackTrace);
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

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

        public bool GetHasWeight(CrossTable Table)
        {
            return Table.Question.HasWeight || Table.Question.HasCount;
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

        public void GetCutRowsAndColumns(CrossTable Table
             , bool HasWeightBack, bool HasWeight, int MaxAxesCount
             , ref Hashtable CutRowsCol, ref Hashtable CutColumnsCol, ref int MedIdx // ref int MedIdx   = -1
             , bool IsReport = false
             , bool CutMedian = false
             , Hashtable WholeRowCol = null
             , bool isCheckCross = false)
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
                    CutNA = false;
                    if (!isCheckCross && Table.AxesGroups.Count > 1 && checkSimpleAggr(Table))
                    {
                        int row = 2;
                        while (row <= Table.GetTableValueRowIndexMaximum)
                        {
                            CutNA = Convert.ToDouble(Table.TableValue(row, tmpIdx)) == 0;
                            if (!CutNA)
                                break;
                            row++;
                        }
                        if (CutNA) { CutColumnsCol.Add(tmpIdx, tmpIdx); }
                    }
                    else
                    {
                        CutNA = Convert.ToDouble(Table.TableValue(2, tmpIdx)) == 0;
                        if (CutNA) { CutColumnsCol.Add(tmpIdx, tmpIdx); }
                    }
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
            if (checkSimpleAggr(Table))
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

        public static bool checkSimpleAggr(CrossTable table)
        {
            int i;
            AxesGroupInformation WithTableAxesGroups = table.AxesGroups;
            for (i = 1; i <= WithTableAxesGroups.Count; i++)
            {
                AxesInformation Ax = WithTableAxesGroups[i - 1];
                if (Ax[0].SectorsCount == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public string TemplatePath(ref XlFileFormat FileFormat, TableOrientation Orientation = TableOrientation.Landscape, bool checkCross = false)
        {
            if (FileFormat == null) { FileFormat = XlFileFormat.xlOpenXMLWorkbook; }
            string d;
            string n = null;

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

            if (FileFormat == XlFileFormat.xlOpenXMLWorkbook) { n = n + "x"; }
            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, "\\");
            return OutputUtil.BuildPath(d, n, "\\");
        }


        private void OutputTable(XSSFWorkbook FormatBook, XSSFSheet namedSheet, CrossTable Table, int NumericCutCol, ref CellRangeAddress StartCell
              , TableType TableType, bool isN, string FormatRangeNamePrefix
              , bool HasWeight, TableOrientation Orientation, bool PageSetupOn
              , Hashtable CutRowsCol, Hashtable CutColumnsCol, int AxesCount, int MaxAxesCount
              , ref CellRangeAddress PageSetupStartCell, bool IsFirstTable, bool NextIsLast
            , CrossTable NextTable, ISheet FormatSheet
            , ref ISheet TableSheet, ref ISheet PageSetupSheet, ref Array ContentsValue, ref Array HyperlinkTargetCells, ref Array HyperlinkTargetSheets
            , ref Array PageSetupContentsValue, ref Array PageSetupHyperlinkTargetCells, string TableIndex
            , ISheet TemplateSheet, ISheet ContentsSheet, bool HasWeightColumn
            , int PageSetupColumnsCountPerPage, int MaxRowsCountPerPage, ref int RemainedRowsCount, double DefLineHeight
            //, ByRef Errors() As ErrorStruct, ByRef ErrorsCount As Long, ByRef res As MethodResult _
            , Hashtable WholeRowCol = null, bool CutMedian = false, int medIdx = 0)
        {
            string tmp = null;
            Array TableValue = null; // objec
            string[,] TableStringValue = null;// string
            Array DataValue = null;// object
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
            XSSFName hyperLinkTargetName = null;
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
                if (TableType == TableType.SignificanceTest && checkSimpleAggr(Table))
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        tmp = "_NP";
                    }
                    else
                    {
                        tmp = "_P";
                    }
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
                    idx = 7; break;
            }
            TableIndex = "[" + tmp + "TABLE" + TableIndex + "]";

            if (Orientation == TableOrientation.Landscape)
            {
                CreateTurnedLandscapeCrossArray(Table, CutRowsCol, CutColumnsCol, ref TableValue, ref Ranking, ref HatchingColorIndex,
                  ref ArrowEnd, ref SigTestMarking
                                                , 2 // wt
                                                , 1 + AxesCount, HasWeight, isN
                                                 , TableType, 1048574
                                                , 16382, MaxAxesCount - AxesCount
                                                , ref PagesCount, ref PageRowsCount, WholeRowCol);
                _log.Info("CreateTurnedLandscapeCrossArray completed");

                CheckOverRow = false;
            }
            else
            {
                CheckOverRow = true;
                CheckOverColumn = true;
                int OverRowssCountTmpRef = 0;
                int OverColumnsCount = 0;

                CreatePortraitCrossArray(Table, CutRowsCol, CutColumnsCol, ref TableValue, ref DataValue, ref Ranking, ref HatchingColorIndex, ref ArrowEnd, ref SigTestMarking
                                                   , 1 + (ToInt(HasWeight) & 1), 1 + AxesCount, HasWeightColumn, HasWeight, isN
                                                   , TableType, XLSX_MAX_ROW - 1, XLSX_MAX_COUMN - 2, ref CheckOverRow, ref CheckOverColumn, ref OverRowssCountTmpRef, ref OverColumnsCount);
            }

            if (CheckOverRow || CheckOverColumn)
            {
                StartCell = null;
                PageSetupStartCell = null;
                if (IsFirstTable)
                {
                    //Me.Application.DisplayAlerts = false;
                    //Sheet.Delete();
                    //Sheet = null;
                    if (PageSetupSheet != null)
                    {
                        //PageSetupSheet.Delete();
                        PageSetupSheet = null;
                    }
                    if (f)
                    {
                        if (CheckOverRow)
                        {
                            //Err().Raise vbObjectError + 100 &, RunningProcName _
                            //           , ThisWorkbook.LocalResource_ReportRowsCountOverAtOneTableMessageIndex)
                        }
                        else
                        {
                            //Err().Raise vbObjectError + 101 &, RunningProcName _
                            //           , ThisWorkbook.LocalResource_ReportColumnsCountOverAtOneTableFailedMessageIndex)
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
                                //               , ThisWorkbook.LocalResource_ReportRowsCountOverDetailNPMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 151 &, RunningProcName _
                                //             , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailNPWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //   Err().Raise vbObjectError + 150 &, RunningProcName _
                                //              , ThisWorkbook.LocalResource_ReportRowsCountOverDetailNPOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 151 &, RunningProcName _
                                //              , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailNPOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                    case TableType.N:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                                //  Err().Raise vbObjectError + 160 &, RunningProcName _
                                //             , ThisWorkbook.LocalResource_ReportRowsCountOverDetailNMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //  Err().Raise vbObjectError + 161 &, RunningProcName _
                                //             , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailNWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //   Err().Raise vbObjectError + 160 &, RunningProcName _
                                //              , ThisWorkbook.LocalResource_ReportRowsCountOverDetailNOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 161 &, RunningProcName _
                                //              , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailNOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                    case TableType.Per:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                                // Err().Raise vbObjectError + 170 &, RunningProcName _
                                //             , ThisWorkbook.LocalResource_ReportRowsCountOverDetailPMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                // Err().Raise vbObjectError + 171 &, RunningProcName _
                                //            , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailPWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //  Err().Raise vbObjectError + 170 &, RunningProcName _
                                //            , ThisWorkbook.LocalResource_ReportRowsCountOverDetailPOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //  Err().Raise vbObjectError + 171 &, RunningProcName _
                                //             , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailPOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                    case TableType.SignificanceTest:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                                //   Err().Raise vbObjectError + 180 &, RunningProcName _
                                //              , ThisWorkbook.LocalResource_ReportRowsCountOverDetailSignificanceTestMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 181 &, RunningProcName _
                                //             , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailSignificanceTestWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //  Err().Raise vbObjectError + 180 &, RunningProcName _
                                //           , ThisWorkbook.LocalResource_ReportRowsCountOverDetailSignificanceTestOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 181 &, RunningProcName _
                                //              , ThisWorkbook.LocalResource_ReportColumnsCountOverDetailSignificanceTestOnAfterWarningMessageIndex, Table.Question.Name)
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
                    FormatLandscapeTable(FormatBook, namedSheet, TableSheet, ref hyperLinkTargetName, NumericCutCol, TableIndex, Table, StartCell, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType
                                        , HasWeight, StartCell, isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: TableValue, CutMedian: CutMedian, MedIdx: medIdx);
                    _log.Info("FormatTurnedLandscapeTable completed");
                    if (bgWorker.CancellationPending) return;
                    NpoiHelper.PutValue(TableSheet as XSSFSheet, insertValuerangeAddress.FormatAsString(), TableValue, true, quotedStyles);

                    FormatBook.RemoveSheetAt((FormatBook.NumberOfSheets - 1));
                    _log.Info("Auto fit started");
                    if (!IsSigTest)//Autofit done in Template
                    {
                        //NpoiHelper.AutoFit(TableSheet, insertValuerangeAddress.FirstColumn + Ranking.GetLowerBound(1),
                        //    insertValuerangeAddress.LastColumn, insertValuerangeAddress.FirstRow + Ranking.GetLowerBound(0),
                        //    insertValuerangeAddress.LastRow);
                    }
                    _log.Info("Auto fit started complted");
                    if (!isN)
                    {
                        if (CurrentOutput.MarkingRanking && TableType != TableType.SignificanceTest)
                        {
                            RankMarking(insertValuerangeAddress, ref Ranking, TableSheet);
                            _log.Info("Rank Marking completed");
                        }
                        if (CurrentOutput.MarkingColoring && TableType != TableType.SignificanceTest)
                        {
                            Hatching(insertValuerangeAddress, ref HatchingColorIndex, TableSheet, FormatBook);
                            _log.Info("Color Marking completed");
                        }
                        if (CurrentOutput.MarkingAscending && TableType != TableType.SignificanceTest)
                        {
                            //AscendingMarking(resizRnge.Cells, ref ArrowEnd);
                        }
                        if (CurrentOutput.MarkingSignificance && TableType != TableType.N)
                        {
                            SignificanceTestMarking(insertValuerangeAddress, ref SigTestMarking, TableSheet);
                            _log.Info("SignificanceTest Marking completed");
                        }
                    }
                    _log.Info("Marking completed");
                    //       ' オートフィット

                    _log.Info("AutoFitEx start");
                    NpoiHelper.autoFitSingleRow(TableSheet.GetRow(insertValuerangeAddress.FirstRow), false);
                    NpoiHelper.autoFitSingleRow(TableSheet.GetRow(insertValuerangeAddress.FirstRow + 2));


                    int stRow = insertValuerangeAddress.FirstRow + Ranking.GetLowerBound(0) - 1;
                    int edRow = insertValuerangeAddress.LastRow;
                    int edCol = insertValuerangeAddress.FirstColumn + Ranking.GetLowerBound(1) - (TableType == TableType.SignificanceTest ? 2 : 1) - 1;

                    NpoiHelper.AutoFitExCorssSub(new CellRangeAddress(stRow, edRow, edCol, edCol),
                        TableType, TableSheet as XSSFSheet);
                    //OutputUtil.AutoFitExCrossLabel(labelRange, xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                    _log.Info("AutoFitEx completed");

                    if (idx > 0)
                    {
                        ContentsValue.SetValue(TableIndex, Table.Index + 1, idx);
                        if (numberOfHyperLink < 64530)
                        {
                            HyperlinkTargetCells.SetValue(insertValuerangeAddress, Table.Index + 1, idx);
                            HyperlinkTargetSheets.SetValue(TableSheet.SheetName, Table.Index + 1, idx);
                            numberOfHyperLink++;
                        }
                    }
                    else
                    {
                        //StartCell = null;
                        //if (NextTable != null)
                        //{
                        //    switch (TableType)
                        //    {
                        //        case TableType.NPer:
                        //            if (NextIsLast)
                        //            {

                        //            }
                        //            else
                        //            {

                        //            }
                        //            break;
                        //        case TableType.N:
                        //            if (NextIsLast)
                        //            {

                        //            }
                        //            else
                        //            {

                        //            }
                        //            break;
                        //        case TableType.Per:
                        //            if (NextIsLast)
                        //            {

                        //            }
                        //            else
                        //            {

                        //            }
                        //            break;
                        //        case TableType.SignificanceTest:
                        //            if (NextIsLast)
                        //            {

                        //            }
                        //            else
                        //            {

                        //            }
                        //            break;
                        //    }
                        //}
                    }
                    if (PageSetupStartCell != null)
                    {

                    }
                }
                else
                {

                    FormatPortraitTable(FormatBook, namedSheet, TableSheet, ref hyperLinkTargetName, NumericCutCol, TableIndex, Table, StartCell, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType
                                    , HasWeight, StartCell, isN, WholeRowCol: WholeRowCol, Ranking: Ranking, TableValue: TableValue, CutMedian: CutMedian, MedIdx: medIdx);


                    //insertValuerangeAddress = new CellRangeAddress(2 , TableValue.GetUpperBound(0), 1, TableValue.GetUpperBound(1));
                    if (!checkSimpleAggr(Table))
                        NpoiHelper.PutValuePortraitTable(TableSheet as XSSFSheet, insertValuerangeAddress.FormatAsString(), TableValue, false, null, 6 + flag, -1);
                    else
                        NpoiHelper.PutValuePortraitTable(TableSheet as XSSFSheet, insertValuerangeAddress.FormatAsString(), TableValue, true, null, -1, -1, simpleaggr: true);
                    if (!IsSigTest)
                    {
                        CellRangeAddress insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0), insertValuerangeAddress.LastRow,
                        insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                        NpoiHelper.PutDataValuePortrait(TableSheet as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue, false, null, 2);
                        if (HasWeight)
                        {
                            insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress2.FirstRow, insertValuerangeAddress2.LastRow,
                                              insertValuerangeAddress2.FirstColumn - 1, insertValuerangeAddress2.FirstColumn - 1);
                            NpoiHelper.PutValueNumeric(TableSheet as XSSFSheet, insertValuerangeAddress2.FormatAsString());
                            if (Ranking != null)
                            {
                                // NpoiHelper.AutoFit(TableSheet, insertValuerangeAddress.FirstColumn + Ranking.GetLowerBound(1),insertValuerangeAddress.LastColumn, insertValuerangeAddress.FirstRow + Ranking.GetLowerBound(0),insertValuerangeAddress.LastRow);
                            }
                        }
                    }
                    else
                    {
                        CellRangeAddress insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress.FirstRow + DataValue.GetLowerBound(0), insertValuerangeAddress.LastRow,
                        insertValuerangeAddress.FirstColumn + DataValue.GetLowerBound(1) - 1, insertValuerangeAddress.LastColumn);
                        NpoiHelper.PutDataValuePortrait(TableSheet as XSSFSheet, insertValuerangeAddress2.FormatAsString(), DataValue, false, null, 2);
                        if (HasWeight)
                        {
                            insertValuerangeAddress2 = new CellRangeAddress(insertValuerangeAddress2.FirstRow, insertValuerangeAddress2.LastRow,
                                              insertValuerangeAddress2.FirstColumn - 1, insertValuerangeAddress2.FirstColumn - 1);
                            NpoiHelper.PutValueNumeric(TableSheet as XSSFSheet, insertValuerangeAddress2.FormatAsString());
                        }
                    }
                    FormatBook.RemoveSheetAt((FormatBook.NumberOfSheets - 1));

                    //NpoiHelper.saveTmp(FormatBook, true);
                    //OutputUtil.PutValue(StartCell.Range["B2"], ref TableIndex);
                    //Range resizRnge = StartCell.Range["B3"].Resize[TableStringValue.GetUpperBound(0), TableStringValue.GetUpperBound(1)];
                    //OutputUtil.PutValue(resizRnge.Cells, ref TableStringValue);
                    //OutputUtil.PutValue(resizRnge.Worksheet.Range[resizRnge.Item[DataValue.GetLowerBound(0), DataValue.GetLowerBound(1)], resizRnge.Item[resizRnge.Rows.Count, resizRnge.Columns.Count]], ref DataValue);
                    //if (HasWeight)
                    //{
                    //    Range itemWith = resizRnge.Item[DataValue.GetLowerBound(0) + (ToInt(CurrentOutput.ShowPreWBTotal) & 1) + 1, DataValue.GetLowerBound(1) - 1].Resize[Table.SectorsCount * (1 + (ToInt(!isN) & 1))];
                    //    itemWith.Value = itemWith.Value;
                    //}
                    if (!IsSigTest)
                        insertValuerangeAddress.FirstRow = insertValuerangeAddress.FirstRow + 1;
                    else
                        insertValuerangeAddress.FirstRow = insertValuerangeAddress.FirstRow + 1;
                    if (!isN)
                    {
                        if (CurrentOutput.MarkingRanking && TableType != TableType.SignificanceTest)
                        {
                            RankMarking(insertValuerangeAddress, ref Ranking, TableSheet);
                            _log.Info("Rank Marking completed");
                        }
                        if (CurrentOutput.MarkingColoring && TableType != TableType.SignificanceTest)
                        {
                            Hatching(insertValuerangeAddress, ref HatchingColorIndex, TableSheet, FormatBook);
                            _log.Info("Color Marking completed");
                        }
                        if (CurrentOutput.MarkingAscending && TableType != TableType.SignificanceTest)
                        {
                            //AscendingMarking(resizRnge.Cells, ref ArrowEnd);
                        }
                        if (CurrentOutput.MarkingSignificance && TableType != TableType.N)
                        {
                            SignificanceTestMarking(insertValuerangeAddress, ref SigTestMarking, TableSheet);
                            _log.Info("SignificanceTest Marking completed");
                        }
                    }

                    _log.Info("AutoFitEx start");
                    // NpoiHelper.autoFitSingleRow(TableSheet.GetRow(checkSimpleAggr(Table) ? insertValuerangeAddress.FirstRow : insertValuerangeAddress.FirstRow - 1), false);
                    // NpoiHelper.autoFitSingleRow(TableSheet.GetRow(checkSimpleAggr(Table) ? insertValuerangeAddress.FirstRow + 2 : insertValuerangeAddress.FirstRow - 1 + 2));


                    //int stRow = insertValuerangeAddress.FirstRow + Ranking.GetLowerBound(0) - 1;
                    //int edRow = insertValuerangeAddress.LastRow;
                    //int edCol = insertValuerangeAddress.FirstColumn + Ranking.GetLowerBound(1) - (TableType == TableType.SignificanceTest ? 2 : 1) - 1;
                    //NpoiHelper.AutoFitExCorssSub(new CellRangeAddress(stRow, edRow, edCol, edCol),
                    //    TableType, TableSheet as XSSFSheet);
                    _log.Info("AutoFitEx completed");

                    if (AxesCount == 1)
                    {
                        // OutputUtil.AutoFitEx(resizRnge.Rows.Item[4], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                    }

                    else
                    {
                        //resizRnge.Rows.Item[4].RowHeight = resizRnge.Rows.Item[4].RowHeight * 2;
                        //OutputUtil.AutoFitEx(resizRnge.Rows.Item[5], xlApp, WorkingSheet, ROW_MAX_HEIGHT);
                    }
                    if (idx > 0)
                    {
                        insertValuerangeAddress.FirstRow += checkSimpleAggr(Table) ? 1 : -1;
                        ContentsValue.SetValue(TableIndex, Table.Index + 1, idx);
                        if (numberOfHyperLink < 64530)
                        {
                            HyperlinkTargetCells.SetValue(insertValuerangeAddress, Table.Index + 1, idx);
                            HyperlinkTargetSheets.SetValue(TableSheet.SheetName, Table.Index + 1, idx);
                            numberOfHyperLink++;
                        }
                    }
                    //if (resizRnge.Row + resizRnge.Rows.Count < resizRnge.Worksheet.Rows.Count)
                    //{
                    //    StartCell = resizRnge.Item[resizRnge.Rows.Count + 1, 1].EntireRow.Range("A1");
                    //}
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
                    if (checkSimpleAggr(Table))
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
                if (isN && !NPOICrossCreator.checkSimpleAggr(Table))
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
                                                    //clr = tmpLevel1HighColorIndex;
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
                                                    //clr = tmpLevel1LowColorIndex;
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
        }

        public void CreatePortraitCrossArray(CrossTable Table
          , Hashtable CutRowsCol, Hashtable CutColumnsCol
          , ref Array TableStringValue, ref Array DataValue
          , ref Array Ranking, ref Array HatchingColorIndex, ref Array ArrowEnd, ref Array SigTestMarking
          , int DataOffsetRow, int DataOffsetColumn
          , bool HasWeightColumn, bool HasWeight, bool isN
          , TableType TableType, int MaxRowsCount, int MaxColumnsCount
          , ref bool CheckOverRow, ref bool CheckOverColumn
          , ref int OverRowsCount
          , ref int OverColumnsCount)
        {
            int RowsCount = 0;
            int ColumnsCount = 0;
            int d = 0;
            int AddColumnsCount;
            int CaptionRowsCount = 0;
            int PreWBRowIndex = 0;
            int x;
            int y;
            int r;
            int c;
            Array tmp = null;
            string tmpBuf = String.Empty;
            object buf;
            int? clr = 0;
            bool f;
            bool IsSigTest;
            TableType tType;
            int[] tmpArrowEnd = new int[2];
            DataMarking reverseSide;
            int tmpY;
            int tmpC;
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


            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType =/* checkSimpleAggr(Table) && !isN ? TableType.NPer :*/ TableType.Per; }
            if (!HasWeightColumn)
                AddColumnsCount = (ToInt(HasWeightColumn & !HasWeight) & 1); //Need to confirm
            else
                AddColumnsCount = 0;
            GetRequiredRowsColsCountPortrait(Table, CutRowsCol, CutColumnsCol, DataOffsetRow, DataOffsetColumn, TableType, AddColumnsCount
                                           , isN, ref d, ref PreWBRowIndex, ref CaptionRowsCount, ref RowsCount, ref ColumnsCount); //To Complete

            if (RowsCount > MaxRowsCount)
            {
                if (CheckOverRow) { return; }
                OverRowsCount = RowsCount - MaxRowsCount;
                RowsCount = MaxRowsCount;
            }
            CheckOverRow = false;

            if (ColumnsCount > MaxColumnsCount)
            {
                if (CheckOverColumn) { return; }
                OverColumnsCount = ColumnsCount - MaxColumnsCount;
                ColumnsCount = MaxColumnsCount;
            }
            CheckOverColumn = false;

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
                            TableStringValue.SetValue(Table.Question.Description, 3, 1);
                        }
                    }
                    else
                    {
                        TableStringValue.SetValue(Table.Question.TableHeading, 2, 1);
                        TableStringValue.SetValue(Table.Question.Description, 3, 1);
                    }
                    //TableStringValue.SetValue(Table.Question.NarrowingCondition, CaptionRowsCount + 1, 1);
                }
                else
                {
                    if (checkSimpleAggr(Table)
                && !string.IsNullOrEmpty(Table.Question.QNumber)
                && !string.IsNullOrEmpty(Table.Question.TableHeading)
                && Table.AxesGroups.Count > 1)
                    {
                        tmpBuf = Table.Question.QNumber + " " + Table.Question.TableHeading;
                    }
                    else if (String.IsNullOrEmpty(Table.Question.TableHeading))
                    {
                        tmpBuf = Table.Question.Name + " " + Table.Question.Description;
                    }
                    else
                    {
                        tmpBuf = Table.Question.Name + " " + Table.Question.TableHeading + "\n[" + Table.Question.Description + "]";
                    }

                    TableStringValue.SetValue(tmpBuf, 1, 1);
                    //PreWBRowIndex = Table.GetTableValueRowIndexMinimum + DataOffsetRow + CaptionRowsCount;
                    //TableStringValue.SetValue(Table.Question.NarrowingCondition, checkSimpleAggr(Table) ? CaptionRowsCount + 1 /*4*/ : /*(CaptionRowsCount + 1) + 2*/ CaptionRowsCount + 2 + 1, 1);
                    //TableStringValue.SetValue(Table.Question.Name + " " + Table.Question.TableHeading + "\n[" + Table.Question.Description + "]", 1, 1);
                }
            }

            DataValue = Array.CreateInstance(typeof(object), new int[] { RowsCount - (CaptionRowsCount + DataOffsetColumn + (ToInt(IsSigTest) & 1) + 1) + 1, ColumnsCount - (DataOffsetRow + AddColumnsCount + 1) + 1 },
                new int[] { CaptionRowsCount + DataOffsetColumn + (ToInt(IsSigTest) & 1) + 1, DataOffsetRow + AddColumnsCount + 1 });
            if (IsMarkingRanking)
            {
                Ranking = Array.CreateInstance(typeof(int), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 }, new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });
            }
            if (IsMarkingColoring)
            {
                HatchingColorIndex = Array.CreateInstance(typeof(int?), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 },
                    new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });
            }
            if (IsMarkingAscending)
            {
                ArrowEnd = Array.CreateInstance(typeof(object), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 },
                    new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });
            }
            if (IsMarkingSignificance)
            {
                SigTestMarking = Array.CreateInstance(typeof(string), new int[] { DataValue.GetUpperBound(0) - DataValue.GetLowerBound(0) + 1, DataValue.GetUpperBound(1) - DataValue.GetLowerBound(1) + 1 },
                    new int[] { DataValue.GetLowerBound(0), DataValue.GetLowerBound(1) });
            }

            tmp = Array.CreateInstance(typeof(string), new int[] { PreWBRowIndex - 1 - Table.GetTableValueColumnIndexMinimum + 1 },
                new int[] { Table.GetTableValueColumnIndexMinimum });
            //batch2 finished

            //batch3 started
            c = DataOffsetRow + AddColumnsCount;
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
                    c = c + 1;
                    if (c > ColumnsCount) { break; }
                    r = CaptionRowsCount;
                    if ((tmp.GetValue(tmp.GetLowerBound(0))) == null)
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            r = r + 1;
                            TableStringValue.SetValue(Table.TableValue(y, x, true), r, c);
                        }
                    }
                    else
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            r = r + 1;
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
                            r = r + 1;
                            TableStringValue.SetValue(tmpBuf, r, c);
                        }
                    }
                    tmp = Array.CreateInstance(typeof(string), new int[] { tmp.GetUpperBound(0) - tmp.GetLowerBound(0) + 1 },
                                    new int[] { tmp.GetLowerBound(0) });
                }
            }
            //batch3 ended

            //batch4 started
            if (IsSigTest)
            {
                if (isN && !NPOICrossCreator.checkSimpleAggr(Table))
                {
                    TableStringValue.SetValue(LocalResource.REPORT_SIGNIFICANCE_TEST_ROW_COLUMN_CAPTION, TableStringValue.GetUpperBound(0), 1);//Need to confirm : TableStringValue.GetUpperBound(0)
                }
            }
            r = CaptionRowsCount + DataOffsetColumn + (ToInt(IsSigTest) & 1);
            var a = (ToInt(IsSigTest));
            var b = (ToInt(IsSigTest) & 1);
            for (x = PreWBRowIndex; x <= Table.GetTableValueColumnIndexMaximum; x++)
            {
                if (!(CutColumnsCol.Contains(x)))
                {
                    r = r + 1;
                    //If r +(x - PreWBRowIndex > (IsShowPreWBTotal And 1 &) And(d - 1 &)) > RowsCount Then Exit For
                    if ((r + (ToInt((x - PreWBRowIndex) > (ToInt(IsShowPreWBTotal) & 1)) & (d - 1))) > RowsCount) { break; }//Need to confirm  : Most probably wrong  If r + (x - PreWBRowIndex > (IsShowPreWBTotal And 1&) And (d - 1&)) > RowsCount Then Exit For
                    c = 0;
                    for (y = Table.GetTableValueRowIndexMinimum;
                         y <= Table.GetTableValueRowIndexMinimum + DataOffsetRow - 1 - (ToInt(HasWeight) & 1); y++) //Need to confirm
                    {
                        c = c + 1;
                        if (y == (Table.GetTableValueRowIndexMinimum + 1) && Table.Question.HasCount) { continue; }
                        TableStringValue.SetValue(Table.TableValue(y, x, true), r, c);
                    }
                    if (Table.Question.HasWeight)
                    {
                        c = DataOffsetRow + AddColumnsCount;
                        TableStringValue.SetValue(Table.TableValue(y, x), r, c);
                    }
                    //r = r + (x - PreWBRowIndex > (IsShowPreWBTotal And 1 &) And(d - 1 &))
                    r = r + (ToInt((x - PreWBRowIndex) > (ToInt(IsShowPreWBTotal) & 1)) & (d - 1));

                    //r = r + (x - ToInt(PreWBRowIndex > (ToInt(IsShowPreWBTotal) & 1)) & (d - 1));//Need to confirm     
                    // r = r + ToInt(x - PreWBRowIndex > (ToInt(IsShowPreWBTotal) & 1)) & (d - 1); // Previous

                }
            }
            //batch4 ended

            //-------------------------Xtra noticed in Landscape : Start
            //if (IsReport)
            //{
            //    int c1 = DataOffsetColumn + (ToInt(IsSigTest) & 1) + 1;
            //    int r1 = CaptionRowsCount + 1;
            //    TableStringValue.SetValue(Table.TableValue(0, PreWBColumnIndex, true), r1 + 1, c1);
            //    TableStringValue.SetValue(null, r1, c1);
            //    if (IsShowPreWBTotal)
            //    {
            //        TableStringValue.SetValue(Table.TableValue(0, PreWBColumnIndex + 1, true), r1 + 1, c1 + 1);
            //        TableStringValue.SetValue(null, r1, c1 + 1);
            //    }
            //}
            //-------------------------Xtra noticed in Landscape : End

            //batch5 started
            tmpRowIndexFrom = Table.GetTableValueRowIndexMinimum + DataOffsetRow;
            tmpRowIndexTo = Table.GetTableValueRowIndexMaximum;
            tmpColumnIndexFrom = PreWBRowIndex;
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
            //batch5 ended

            //batch6 started
            c = DataOffsetRow + AddColumnsCount;
            for (y = Table.GetTableValueRowIndexMinimum + DataOffsetRow; y <= Table.GetTableValueRowIndexMaximum; y++)
            {
                if (!CutRowsCol.ContainsKey(y))
                {
                    c = c + 1;
                    if (c > ColumnsCount) { break; }
                    r = CaptionRowsCount + DataOffsetColumn + (ToInt(IsSigTest) & 1);
                    for (x = PreWBRowIndex; x <= Table.GetTableValueColumnIndexMaximum; x++)
                    {
                        if (!CutColumnsCol.ContainsKey(x))
                        {
                            r = r + 1;
                            //If r +(x - PreWBRowIndex > (IsShowPreWBTotal And 1 &) And(d - 1 &)) > RowsCount Then Exit For
                            if ((r + (ToInt((x - PreWBRowIndex) > (ToInt(IsShowPreWBTotal) & 1)) & (d - 1))) > RowsCount) { break; }  //Need to Confirm
                            f = false;
                            if (HasWeight)
                            {
                                switch (Table.GetTableValueColumnIndexMaximum - x)
                                {
                                    case 0:
                                        buf = tmpTableValue[y, x];
                                        if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                        DataValue.SetValue(buf, r + (ToInt(tType == TableType.NPer) & 1), c); //Need to Confirm : DataValue(r + (tType = TableType_NPer And 1&), c) = buf
                                        if (IsSigTest)
                                        {
                                            buf = tmpSignificanceTestCharacters[y, x];
                                            if (null != buf && ((string)buf).Length > 0)
                                            {
                                                DataValue.SetValue(buf, r + d - 1, c);
                                            }
                                        }
                                        else if (IsMarkingSignificance)
                                        {
                                            //SigTestMarking.SetValue(tmpSignificanceMark[y, x], r + (Convert.ToInt32(tType = TableType.NPer) & 1), c); //Commented in Landscape code
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
                            if (f)
                            {
                                if (x - PreWBRowIndex > (ToInt(IsShowPreWBTotal) & 1))
                                {
                                    r = r + d - 1;
                                }
                            }
                            else
                            {
                                if (isN || x - PreWBRowIndex <= (ToInt(IsShowPreWBTotal) & 1))
                                {  // ' WB前全体/全体
                                    buf = tmpTableValue[y, x];
                                    if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                    DataValue.SetValue(buf, r, c);
                                    if (isN)
                                    {
                                        if (IsSigTest)
                                        {
                                            buf = tmpSignificanceTestCharacters[y, x];
                                            if (null != buf && ((string)buf).Length > 0)
                                            {
                                                try
                                                {
                                                    DataValue.SetValue(buf, DataValue.GetUpperBound(0), c);//Need to confirm : DataValue.GetUpperBound(0 or 1(because in landscape can see the difference))
                                                }
                                                catch { }
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
                                        buf = tmpSignificanceTestCharacters[y, x];
                                        if (null != buf && ((string)buf).Length > 0)
                                        { DataValue.SetValue(buf, r + d - 1, c); }
                                    }
                                    if (IsMarkingRanking) { Ranking.SetValue(tmpRank[y, x], r, c); }
                                    // If IsMarkingSignificance Then SigTestMarking(r +(tType = TableType_NPer And 1 &), c) = tmpSignificanceMark(y, x)
                                    if (IsMarkingSignificance)
                                    {
                                        SigTestMarking.SetValue(tmpSignificanceMark[y, x], r + (ToInt(tType == TableType.NPer) & 1), c); //Need to Confirm
                                    }
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
                                                    //clr = tmpLevel1HighColorIndex;
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
                                                    //clr = tmpLevel1LowColorIndex;
                                                    clr = IsMarkingColoringLevel2Low ? tmpLevel1LowColorIndex : tmpLevel2LowColorIndex;
                                                }
                                            }
                                        }
                                        
                                        if (clr < 0)
                                            clr = null;

                                        HatchingColorIndex.SetValue(clr, r, c);
                                        if (x - PreWBRowIndex > (ToInt(IsShowPreWBTotal) & 1))
                                        {
                                            if (tType == TableType.NPer) { HatchingColorIndex.SetValue(clr, r + 1, c); }
                                            if (IsSigTest) { HatchingColorIndex.SetValue(clr, r + d - 1, c); }
                                        }
                                    }
                                    if (IsMarkingAscending)
                                    {
                                        if (ArrowEnd.GetValue(r, c).GetType().IsArray)
                                        {
                                            if (Table.IsArrowEnd(y, x, out reverseSide))
                                            {
                                                tmpC = c;
                                                for (tmpY = y + 1; tmpY <= Table.GetTableValueRowIndexMaximum; tmpY++)
                                                {
                                                    if (!CutRowsCol.ContainsKey(tmpY))
                                                    {
                                                        tmpC = tmpC + 1;
                                                        if (!Table.IsArrowShaft(tmpY, x))
                                                        {
                                                            if (Table.AscendingMarking(tmpY, x) == reverseSide)
                                                            {
                                                                tmpArrowEnd.SetValue(r, 1);
                                                                if (reverseSide == DataMarking.AscendingStart)
                                                                {
                                                                    tmpArrowEnd.SetValue(c, 0);
                                                                    ArrowEnd.SetValue(tmpArrowEnd, r, tmpC);
                                                                }
                                                                else
                                                                {
                                                                    tmpArrowEnd.SetValue(tmpC, 0);
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
                                    if ((x - PreWBRowIndex) > (ToInt(IsShowPreWBTotal) & 1))
                                    {
                                        r = r + d - 1;
                                    }
                                    //Some codes below are not added as its not added in Landscape mode.
                                }
                            }
                        }
                    }
                }
            }
            //batch6 ended
        }

        public void CreateTurnedLandscapeCrossArray(CrossTable Table
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
            if (checkSimpleAggr(Table)
                && !string.IsNullOrEmpty(Table.Question.QNumber)
                && !string.IsNullOrEmpty(Table.Question.TableHeading)
                && Table.AxesGroups.Count > 1)
            {
                tmpBuf = Table.Question.QNumber + " " + Table.Question.TableHeading;
            }
            else if (String.IsNullOrEmpty(Table.Question.TableHeading))
            {
                tmpBuf = Table.Question.Name + " " + Table.Question.Description;
            }
            else
            {
                tmpBuf = Table.Question.Name + " " + Table.Question.TableHeading + "\n[" + Table.Question.Description + "]";
            }
            //OutputUtil.AddPrefix(ref tmpBuf);
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
                            //OutputUtil.AddPrefix(ref tmpArrStr);
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
                            //OutputUtil.AddPrefix(ref tmpBuf);
                            TableValue.SetValue(tmpBuf, r, c);
                        }
                    }
                    else
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            c = c + 1;
                            tmpBuf = Table.TableValue(y, x, true);
                            //OutputUtil.AddPrefix(ref tmpBuf);
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
                        //OutputUtil.AddPrefix(ref tmpBuf);
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
                if (isN && !NPOICrossCreator.checkSimpleAggr(Table))
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
                            //OutputUtil.AddPrefix(ref tmpBuf);
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
                                                if ((bool)tmpColoringLevel1High[y, x]) { /*clr = tmpLevel1HighColorIndex;*/ clr = IsMarkingColoringLevel2High ? tmpLevel1HighColorIndex : tmpLevel2HighColorIndex; }
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
                                                if ((bool)tmpColoringLevel1Low[y, x]) { /*clr = tmpLevel1LowColorIndex*/ clr = IsMarkingColoringLevel2Low ? tmpLevel1LowColorIndex : tmpLevel2LowColorIndex; }
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
            if (IsSigTest && checkSimpleAggr(Table) && !isN)
            {
                d = d + 1;
            }

            RowsCount = Table.GetTableValueRowIndexMaximum - Table.GetTableValueRowIndexMinimum + 1 - CutRowsCol.Count;
            ColumnsCount = Table.GetTableValueColumnIndexMaximum - Table.GetTableValueColumnIndexMinimum + 1 - CutColumnsCol.Count + AddColumnsCount;
            if (IsSigTest && !checkSimpleAggr(Table))
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

        private void GetRequiredRowsColsCountPortrait(CrossTable Table
           , Hashtable CutRowsCol, Hashtable CutColumnsCol
           , int DataOffsetRow, int DataOffsetColumn
           , TableType TableType, int AddColumnsCount, bool isN
           , ref int d, ref int PreWBRowIndex
           , ref int CaptionRowsCount
           , ref int RequiredRowsCount, ref int RequiredColumnsCount)
        {
            int RowsCount;
            int ColumnsCount;
            int DataRowsCount;
            int tmpRowsCount;
            bool IsSigTest;
            TableType tType;
            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;//tType = TableType & !TableType.SignificanceTest; Need to Confirm
            if (IsSigTest)
                tType = TableType.Per;

            d = 1 + (ToInt(!isN) & ((ToInt(tType == TableType.NPer) & 1) + (ToInt(IsSigTest) & 1)));  //Need to confirm : d = 1 & +(Not isN And((tType = TableType_NPer And 1 &) +(IsSigTest And 1 &)))
            RowsCount = Table.GetTableValueColumnIndexMaximum - Table.GetTableValueColumnIndexMinimum + 1 - CutColumnsCol.Count;
            ColumnsCount = Table.GetTableValueRowIndexMaximum - Table.GetTableValueRowIndexMinimum + 1 - CutRowsCol.Count;
            PreWBRowIndex = Table.GetTableValueColumnIndexMinimum + DataOffsetColumn;
            tmpRowsCount = 1 + (ToInt(CutColumnsCol.ContainsKey(PreWBRowIndex)) & 1); //Need to Confirm : tmpRowsCount = 1& + (ExistKey(CutColumnsCol, CStr(PreWBRowIndex)) And 1&)
            DataRowsCount = (RowsCount - DataOffsetColumn - tmpRowsCount) * d + tmpRowsCount;
            if ((Table.ParentReportset.FileType & FileType.Report) == 0 || onlySigPage)
            {
                CaptionRowsCount = 2 + (ToInt(CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single) & 2);
                RequiredRowsCount = CaptionRowsCount + DataOffsetColumn + DataRowsCount;
            }
            else
            {
                RequiredRowsCount = DataOffsetColumn + DataRowsCount;
            }
            RequiredRowsCount = RequiredRowsCount + (ToInt(IsSigTest) & (1 + (ToInt(isN) & d)));
            RequiredColumnsCount = ColumnsCount + AddColumnsCount;
        }

        public void FormatLandscapeTable(XSSFWorkbook FormatBook, XSSFSheet namedSheet, ISheet TableSheet, ref XSSFName hyperLinkTargetName, int NumericCutCol, string TableIndex, CrossTable Table
             , CellRangeAddress TemplateSheet
             , Hashtable CutRowsCol, Hashtable CutColumnsCol
             , ISheet FormatSheet, string FormatRangeNamePrefix
             , TableType TableType, bool HasWeight
             , CellRangeAddress StartCell, bool isN
             , ISheet ContentsSheet = null
             , bool IsReport = false
             , bool CutMedian = false, int MedIdx = -1
             , Hashtable WholeRowCol = null,
                       Array Ranking = null,
                                   Array TableValue = null)
        {
            bool HasNAColumn = false;
            int d = 0;
            int d2 = 0;
            bool CutNAColumn = false;
            int ItemSectorsCount = 0;
            int[] SectorsCount = new int[2]; //int
            int c = 0;
            int r = 0;
            int i = 0;
            int n = 0;
            int firstRow = 0;
            int lastRow = 0;
            IName WithFormatSheetHeader = null;
            ISheet standardFormatSheet;
            ISheet baseSheet = null;
            CellRangeAddress cellRange;
            int idx = 0;
            int y = 0;
            bool IsSigTest = false;
            TableType tType;

            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            HasNAColumn = CurrentOutput.ShowNAAtItem;
            d = 1 + (ToInt(!isN & TableType == TableType.NPer) & 1);
            d2 = d + (ToInt(!isN & IsSigTest) & 1);
            if (IsSigTest && checkSimpleAggr(Table) && !isN)
            {
                d = d + 1;
            }

            if (isN)
            {
                if (HasNAColumn)
                {
                    CutNAColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum);
                }
                //else if (IsSigTest && !HasNAColumn && NPOICrossCreator.checkSimpleAggr(Table))
                //{
                //    CutNAColumn = true;
                //}
                if (CutMedian)
                {
                    CutMedian = (MedIdx >= Table.GetTableValueColumnIndexMinimum);
                }
            }
            else
            {
                CutMedian = false;
                ItemSectorsCount = Table.SectorsCount;

                if (HasNAColumn && !CutNAColumn)
                {
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2) - (Table.Question.SubTotalCnt)))
                    {
                        CutNAColumn = true;
                    }
                }
            }

            //' ヘッダ
            _log.Debug("UsedRange clear start");

            WithFormatSheetHeader = GetNamedRange(namedSheet, FormatRangeNamePrefix + "_Header");
            FormatBook.CreateSheet();
            FormatBook.SetSheetName((FormatBook.NumberOfSheets - 1), "Base");
            baseSheet = FormatBook.GetSheet("Base");
            _log.Debug("UsedRange clear completed");

            AreaReference area = new AreaReference(WithFormatSheetHeader.RefersToFormula, SpreadsheetVersion.EXCEL2007);
            standardFormatSheet = (WithFormatSheetHeader.SheetName == "Standard" ? FormatBook.GetSheet("Standard") : FormatBook.GetSheet("SignificanceTest"));
            cellRange = CellRangeAddress.ValueOf(WithFormatSheetHeader.RefersToFormula);
            lastRow = firstRow + (cellRange.LastRow - cellRange.FirstRow);
            NpoiHelper.copyRows(baseSheet as XSSFSheet, standardFormatSheet as XSSFSheet, FormatRangeNamePrefix + "_Header", 1);
            _log.Debug("Header copied");

            r = (cellRange.LastRow - cellRange.FirstRow) + 1;

            y = 2;// wt ' ヘッダの下端インデックス
            if ((CutRowsCol.ContainsKey(checkSimpleAggr(Table) ? y + 1 : y)))
            {
                r = r - d;
            }
            int starttempRow = r;
            bool firstDouble = true;
            int firstDoublerow = 0;
            int firstDoublerowCnt = 0;
            bool firstTripple = true;
            int firstTripplerow = 0;
            int firstTripplerowCnt = 0;
            ISheet tmpSheet = null;
            for (idx = 0; idx <= Table.AxesGroups.Count - 1; idx++)
            {
                if (Table.AxesGroups[idx].Count == 1 && firstDouble || Table.AxesGroups[idx].Count == 2 && firstTripple)
                {

                    var tmpRangeName = Table.AxesGroups[idx].Count == 1 ? GetNamedRange(namedSheet, FormatRangeNamePrefix + "_Double") : GetNamedRange(namedSheet, FormatRangeNamePrefix + "_Triple");
                    cellRange = CellRangeAddress.ValueOf(tmpRangeName.RefersToFormula);
                    firstRow = r;
                    lastRow = firstRow + (cellRange.LastRow - cellRange.FirstRow) + 1;
                    NpoiHelper.copyRows(baseSheet as XSSFSheet, standardFormatSheet as XSSFSheet, Table.AxesGroups[idx].Count == 1 ? FormatRangeNamePrefix + "_Double" : FormatRangeNamePrefix + "_Triple", firstRow + 1);
                    if (Table.AxesGroups[idx].Count == 1)
                    {
                        firstDoublerow = r;
                        firstDoublerowCnt = (cellRange.LastRow - cellRange.FirstRow);
                        firstDouble = false;
                        r = r + firstDoublerowCnt;
                    }
                    else
                    {
                        firstTripplerow = r;
                        firstTripplerowCnt = (cellRange.LastRow - cellRange.FirstRow);
                        firstTripple = false;
                        r = r + firstTripplerowCnt;
                        Tripple = true;
                    }
                    r++;
                }
            }

            if (CutNAColumn)
            {
                cellRange = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_NoAnswerColumn").RefersToFormula);
                cellRange = new CellRangeAddress(4, lastRow - 1, cellRange.FirstColumn - CutColoums - 1, cellRange.LastColumn - CutColoums - 1);
                NpoiHelper.deleteSingleColumnName(baseSheet as XSSFSheet, cellRange);
            }


            if (isN)
            {
                if (CutMedian)
                {
                    CellRangeAddress cellRangeAddressNumeric = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_MedianColumn").RefersToFormula);
                    cellRange = new CellRangeAddress(1, baseSheet.LastRowNum, cellRangeAddressNumeric.FirstColumn - CutColoums - 1, cellRangeAddressNumeric.LastColumn - CutColoums - 1);
                    NpoiHelper.deleteSingleColumnName(baseSheet as XSSFSheet, cellRange);
                }

                if (checkSimpleAggr(Table) && IsSigTest)
                {
                    int cutValue = !HasNAColumn && CurrentOutput.WBOn ? 2 : 1;
                    CellRangeAddress cellRangeAddressNumeric = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_SigTestColumn").RefersToFormula);
                    cellRange = new CellRangeAddress(1, baseSheet.LastRowNum, cellRangeAddressNumeric.FirstColumn - CutColoums - cutValue, cellRangeAddressNumeric.LastColumn - CutColoums - cutValue);
                    NpoiHelper.deleteSingleColumnName(baseSheet as XSSFSheet, cellRange);
                }
            }
            else
            {

                CellRangeAddress WithWorkingSheet = null;
                c = (CellRangeAddress.ValueOf(GetNamedRange(namedSheet, FormatRangeNamePrefix + "_SectorColumns").RefersToFormula)).FirstColumn + 1;
                c = c - CutColoums;
                if (ItemSectorsCount != 2)
                {
                    WithWorkingSheet = CellRangeAddress.ValueOf(ColumnIndexToColumnLetter(c) + 5 + ":" + ColumnIndexToColumnLetter(c) + lastRow);
                    if (ItemSectorsCount > 2)
                    {
                        NpoiHelper.copyColumn(baseSheet as XSSFSheet, WithWorkingSheet, ItemSectorsCount - 2, bgWorker);
                    }
                    else
                    {
                        NpoiHelper.deleteSingleColumnName(baseSheet as XSSFSheet, WithWorkingSheet);
                    }
                }
                if (Table.Question.SubTotalCnt > 0)
                {
                    int subTtlCl = c + ItemSectorsCount - (HasNAColumn && !CutNAColumn ? 0 : 1) - Table.Question.SubTotalCnt;
                    int endCl = c + ItemSectorsCount - (HasNAColumn && !CutNAColumn ? 0 : 1);
                    CellRangeAddress subTtlClC = CellRangeAddress.ValueOf(ColumnIndexToColumnLetter(subTtlCl) + 5 + ":" + ColumnIndexToColumnLetter(subTtlCl) + lastRow);
                    CellRangeAddress endClC = CellRangeAddress.ValueOf(ColumnIndexToColumnLetter(endCl) + 5 + ":" + ColumnIndexToColumnLetter(endCl) + lastRow);
                    NpoiHelper.applyStyles(baseSheet as XSSFSheet, endClC, subTtlClC);
                }
            }
            removeAxisMergeRegion(baseSheet, firstDoublerow == 0 ? 0 : 1, firstDoublerow == 0 ? 0 : (firstDoublerow + d),
                firstTripplerow == 0 ? 0 : 2, firstTripplerow == 0 ? 0 : (firstTripplerow + d), d, d2, 2, 0);
            for (idx = 0; idx <= Table.AxesGroups.Count - 1; idx++)
            {
                int cpyDestinationRow = r;
                tmpSheet = FormatBook.GetSheetAt((FormatBook.NumberOfSheets - 1));
                int row = Table.AxesGroups[idx].Count == 1 ? firstDoublerow : firstTripplerow;
                int rowCount = Table.AxesGroups[idx].Count == 1 ? firstDoublerowCnt : firstTripplerowCnt;
                for (int cpyRow = row; cpyRow <= (row + rowCount); cpyRow++)
                {
                    tmpSheet.CopyRow(cpyRow, cpyDestinationRow);
                    cpyDestinationRow++;
                }
                _log.Debug("copy tmp rows complted");
                y = y + 1;  //' 小計行インデックス
                if (CutRowsCol.ContainsKey(y) && Table.AxesGroups[idx][0].SectorsCount > 0)
                {
                    DeleteShiftRowUp(tmpSheet, d, r);
                }
                else
                {
                    r = r + d;
                }
                //    ' 行
                if (Table.AxesGroups[idx].Count == 1)
                { //' 二重クロス
                    r = r + d2; //' 2つ目の選択肢行
                    AxesInformation tmpAxes = Table.AxesGroups[idx];
                    SectorsCount[0] = tmpAxes[0].SectorsCount;
                    y = y + SectorsCount[0];
                    if (SectorsCount[0] > 3)
                    {
                        //CopyShiftRowDown(tmpSheet, ((SectorsCount[0] - 3) * d2), r, d2);
                        CopyShiftRowDown2Way(tmpSheet, ((SectorsCount[0] - 3) * d2), r, d2);
                    }
                    else if (SectorsCount[0] < 3)
                    {
                        //DeleteShiftRowUp(tmpSheet, ((3 - SectorsCount[0]) * d2), r - d2);
                        DeleteShiftRowUp2Way(tmpSheet, SectorsCount[0], r, d2);
                    }
                    if (SectorsCount[0] > 0)
                    {
                        CellRangeAddress addMergedRegion = null;
                        addMergedRegion = CellRangeAddress.ValueOf("B" + (r - d2 + 1) + ":" + "B" + ((r - d2) + SectorsCount[0] * d2));
                        try
                        {
                            baseSheet.AddMergedRegion(addMergedRegion);
                        }
                        catch (Exception) { }
                    }
                    _log.Debug("adjust _SectorColumns rows complted");
                }
                else
                {   // ' 三重クロス
                    AxesInformation tmpAxes2 = Table.AxesGroups[idx];
                    SectorsCount[0] = tmpAxes2[0].SectorsCount;
                    n = tmpAxes2[1].SectorsCount + 1;
                    y = y + SectorsCount[0] * n;
                    if (SectorsCount[0] < 3)
                    {
                        // DeleteShiftRowUp(tmpSheet, (d + 3 * d2), (r + d + 3 * d2)); ;
                        DeleteShiftRowUp(tmpSheet, (3 - SectorsCount[0]) * (d + 3 * d2), r);
                    }
                    //        ' 内側(表側)の選択肢数(便宜)
                    SectorsCount[1] = n - 1;
                    if (SectorsCount[1] > 3)
                    {
                        for (i = r + 3 * d + 7 * d2; i >= r + d + d2; i = i - (d + 3 * d2))
                        {
                            if (bgWorker.CancellationPending) return;
                            if (tmpSheet.LastRowNum >= i)
                            {
                                CopyShiftRowDown(tmpSheet, ((SectorsCount[1] - 3) * d2), i, d2);
                                CellRangeAddress cAddr = CellRangeAddress.ValueOf("c" + (i - (d + d2) + 1) + ":"
        + "c" + (i + (SectorsCount[1] - 1) * d2));
                                baseSheet.AddMergedRegion(cAddr);
                            }
                        }
                    }
                    else if (SectorsCount[1] < 3)
                    {
                        for (i = r + 3 * d + 7 * d2; i >= r + d + d2; i = i - (d + 3 * d2))
                        {
                            if (tmpSheet.LastRowNum >= i)
                            {
                                DeleteShiftRowUp(tmpSheet, ((3 - SectorsCount[1]) * d2), i - d2);
                                CellRangeAddress cAddr = CellRangeAddress.ValueOf("c" + (i - (d + d2) + 1) + ":"
        + "c" + (i + (SectorsCount[1] - 1) * d2));
                                baseSheet.AddMergedRegion(cAddr);
                            }
                        }
                    }
                    else if (SectorsCount[1] == 3)
                    {
                        for (i = r + 3 * d + 7 * d2; i >= r + d + d2; i = i - (d + 3 * d2))
                        {
                            if (tmpSheet.LastRowNum >= i)
                            {
                                CellRangeAddress cAddr = CellRangeAddress.ValueOf("c" + (i - (d + d2) + 1) + ":"
        + "c" + (i + (SectorsCount[1] - 1) * d2));
                                baseSheet.AddMergedRegion(cAddr);
                            }
                        }
                    }
                    if (SectorsCount[0] > 3)
                    {
                        int rowDiff = ((d + 3 * d2) + ((SectorsCount[1] - 3) * d2));
                        int lRow = tmpSheet.LastRowNum;
                        int fRow = lRow - rowDiff;
                        tmpSheet.ShiftRows(fRow, lRow, ((d + 3 * d2) + ((SectorsCount[1] - 3) * d2)) * (SectorsCount[0] - 3));
                        int t = 1, e;
                        while (t <= SectorsCount[0] - 3)
                        {
                            if (bgWorker.CancellationPending) return;
                            for (e = fRow - rowDiff; e <= fRow - 1; e++)
                            {
                                tmpSheet.GetRow(e).CopyRowTo(e + rowDiff);
                            }
                            t++; fRow = e + rowDiff;
                        }
                    }
                    int axisCount = SectorsCount[0] * (SectorsCount[1] * d2 + d);
                    if (SectorsCount[0] > 0)
                    {
                        CellRangeAddress addMergedRegion = null;
                        addMergedRegion = CellRangeAddress.ValueOf("b" + (r + 1) + ":" + "b" + (r + axisCount));
                        try
                        {
                            baseSheet.AddMergedRegion(addMergedRegion);
                        }
                        catch (Exception) { }
                    }
                    r = r + SectorsCount[0] * (SectorsCount[1] * d2 + d);
                    _log.Debug("adjust _SectorColumns rows complted");
                }
                r = (tmpSheet.LastRowNum + 1); //' 次行
                if (bgWorker.CancellationPending) return;
            }

            _log.Debug("deleting extra rows");
            int len = (firstDoublerowCnt + firstTripplerowCnt + (firstDoublerowCnt > 0 && firstTripplerowCnt > 0 ? 2 : 1));
            for (int j = starttempRow; j <= len + starttempRow - 1; j++)
            {
                IRow row = tmpSheet.GetRow(j);
                if (row == null) continue;
                tmpSheet.RemoveRow(row);

            }
            CellRangeAddress WithWorkingSheet1 = CellRangeAddress.ValueOf("B3:R3");
            baseSheet.AddMergedRegion(WithWorkingSheet1);
            _log.Debug("copying to main sheet");
            firstRow = TableSheet.PhysicalNumberOfRows == 0 ? baseSheet.FirstRowNum : TableSheet.LastRowNum + 1;
            NpoiHelper.copyRows(len + starttempRow, len, TableSheet as XSSFSheet, baseSheet as XSSFSheet, baseSheet.FirstRowNum + 1,
                baseSheet.LastRowNum + 1, firstRow + 1);
            _log.Debug("copying to main sheet complted");

            //CopySheet(firstRow, lastRow, baseSheet, TableSheet);
            insertValuerangeAddress = new CellRangeAddress(2 + firstRow,
                (2 + (TableValue.GetUpperBound(0) - 1)) + firstRow, 1, TableValue.GetUpperBound(1));

            TableSheet.CreateRow(firstRow + 1).CreateCell(1).SetCellValue(TableIndex);
        }

        public void FormatLandscapeTableIndividual(XSSFWorkbook FormatBook, XSSFSheet namedSheet, ISheet TableSheet, ref XSSFName hyperLinkTargetName, int NumericCutCol, string TableIndex, CrossTable Table
             , CellRangeAddress TemplateSheet
             , Hashtable CutRowsCol, Hashtable CutColumnsCol
             , ISheet FormatSheet, string FormatRangeNamePrefix
             , TableType TableType, bool HasWeight
             , CellRangeAddress StartCell, bool isN
             , ISheet ContentsSheet = null
             , bool IsReport = false
             , bool CutMedian = false, int MedIdx = -1
             , Hashtable WholeRowCol = null,
                       Array Ranking = null,
                                   Array TableValue = null)
        {
            bool HasNAColumn = false;
            int d = 0;
            int d2 = 0;
            bool CutNAColumn = false;
            int ItemSectorsCount = 0;
            int[] SectorsCount = new int[2]; //int
            int c = 0;
            int r = 0;
            int i = 0;
            int n = 0;
            int firstRow = 0;
            int lastRow = 0;
            IName WithFormatSheetHeader = null;
            ISheet baseSheet = null;
            CellRangeAddress cellRange;
            bool CutNA = false;
            bool CutIV = false;
            int idx = 0;
            int y = 0;
            int cutBaseCol = 0;
            bool IsSigTest = false;
            TableType tType;

            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            HasNAColumn = CurrentOutput.ShowNAAtItem;
            d = 1 + (ToInt(!isN & TableType == TableType.NPer) & 1);
            d2 = d + (ToInt(!isN & IsSigTest) & 1);

            if (IsSigTest && checkSimpleAggr(Table) && !isN)
            {
                d = d + 1;
            }

            if (isN)
            {
                if (HasNAColumn)
                {
                    CutNAColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum);
                }
                //else if (IsSigTest && !HasNAColumn && NPOICrossCreator.checkSimpleAggr(Table))
                //{
                //    CutNAColumn = true;
                //}
                if (CutMedian)
                {
                    CutMedian = (MedIdx >= Table.GetTableValueColumnIndexMinimum);
                }
            }
            else
            {
                CutMedian = false;
                ItemSectorsCount = Table.SectorsCount;

                if (HasNAColumn && !CutNAColumn)
                {
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2) - (Table.Question.SubTotalCnt)))
                    {
                        CutNAColumn = true;
                    }
                }
                else if (IsSigTest && !HasNAColumn && NPOICrossCreator.checkSimpleAggr(Table))
                {
                    CutNAColumn = true;
                }
            }

            WithFormatSheetHeader = GetNamedRange(namedSheet, FormatRangeNamePrefix + "_Header");
            baseSheet = NpoiHelper.createBaseSheet(FormatBook);

            AreaReference area = new AreaReference(WithFormatSheetHeader.RefersToFormula, SpreadsheetVersion.EXCEL2007);
            cellRange = CellRangeAddress.ValueOf(WithFormatSheetHeader.RefersToFormula);
            lastRow = cellRange.LastRow + 1;
            NpoiHelper.copyRows(baseSheet as XSSFSheet, namedSheet as XSSFSheet, FormatRangeNamePrefix + "_Header", 1);
            _log.Debug("Header copied");

            r = (cellRange.LastRow - cellRange.FirstRow) + 1;
            //end
            y = 2;// wt ' ヘッダの下端インデックス
            if ((CutRowsCol.ContainsKey(checkSimpleAggr(Table) ? y + 1 : y)))
            {
                r = r - d;
            }
            int starttempRow = r;
            bool firstDouble = true;
            int firstDoublerow = 0;
            int firstDoublerowCnt = 0;
            bool firstTripple = true;
            int firstTripplerow = 0;
            int firstTripplerowCnt = 0;
            ISheet tmpSheet = null;
            bool isDoubleOnly = true;
            for (idx = 0; idx <= Table.AxesGroups.Count - 1; idx++)
            {
                if (Table.AxesGroups[idx].Count == 1 && firstDouble || Table.AxesGroups[idx].Count == 2 && firstTripple)
                {

                    var tmpRangeName = Table.AxesGroups[idx].Count == 1 ? GetNamedRange(namedSheet, FormatRangeNamePrefix + "_Double") : GetNamedRange(namedSheet, FormatRangeNamePrefix + "_Triple");
                    cellRange = CellRangeAddress.ValueOf(tmpRangeName.RefersToFormula);
                    firstRow = r;
                    lastRow = firstRow + (cellRange.LastRow - cellRange.FirstRow) + 1;
                    NpoiHelper.copyRows(baseSheet as XSSFSheet, namedSheet as XSSFSheet,
                        Table.AxesGroups[idx].Count == 1 ? FormatRangeNamePrefix + "_Double" : FormatRangeNamePrefix + "_Triple", firstRow + 1);
                    if (Table.AxesGroups[idx].Count == 1)
                    {
                        firstDoublerow = r;
                        firstDoublerowCnt = (cellRange.LastRow - cellRange.FirstRow) + 1;
                        firstDouble = false;
                        r = r + firstDoublerowCnt;
                    }
                    else
                    {
                        firstTripplerow = r;
                        firstTripplerowCnt = (cellRange.LastRow - cellRange.FirstRow) + 1;
                        firstTripple = false;
                        r = r + firstTripplerowCnt;
                        isDoubleOnly = false;
                    }
                    r++;
                }
            }
            if (CutNAColumn)
            {
                cellRange = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_NoAnswerColumn").RefersToFormula);
                cellRange = new CellRangeAddress(5, lastRow - 1, cellRange.FirstColumn - CutColoums - 1, cellRange.LastColumn - CutColoums - 1);
                NpoiHelper.deleteSingleColumnName(baseSheet as XSSFSheet, cellRange);
            }


            if (isN)
            {
                if (CutMedian)
                {
                    CellRangeAddress cellRangeAddressNumeric = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_MedianColumn").RefersToFormula);
                    cellRange = new CellRangeAddress(1, baseSheet.LastRowNum, cellRangeAddressNumeric.FirstColumn - CutColoums - 1, cellRangeAddressNumeric.LastColumn - CutColoums - 1);
                    NpoiHelper.deleteSingleColumnName(baseSheet as XSSFSheet, cellRange);
                }

                if (checkSimpleAggr(Table) && IsSigTest)
                {
                    int cutValue = !HasNAColumn && CurrentOutput.WBOn ? 2 : 1;
                    CellRangeAddress cellRangeAddressNumeric = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_SigTestColumn").RefersToFormula);
                    cellRange = new CellRangeAddress(1, baseSheet.LastRowNum, cellRangeAddressNumeric.FirstColumn - CutColoums - cutValue, cellRangeAddressNumeric.LastColumn - CutColoums - cutValue);
                    NpoiHelper.deleteSingleColumnName(baseSheet as XSSFSheet, cellRange);
                }
            }
            else
            {

                CellRangeAddress WithWorkingSheet = null;
                c = (CellRangeAddress.ValueOf(GetNamedRange(namedSheet, FormatRangeNamePrefix + "_SectorColumns").RefersToFormula)).FirstColumn + 1;
                c = c - CutColoums;
                if (ItemSectorsCount != 2)
                {
                    WithWorkingSheet = CellRangeAddress.ValueOf(ColumnIndexToColumnLetter(c) + 6 + ":" + ColumnIndexToColumnLetter(c) + lastRow);
                    if (ItemSectorsCount > 2)
                    {
                        NpoiHelper.copyColumn(baseSheet as XSSFSheet, WithWorkingSheet, ItemSectorsCount - 2, bgWorker);
                    }
                    else
                    {
                        NpoiHelper.deleteSingleColumnName(baseSheet as XSSFSheet, WithWorkingSheet);
                    }
                }
                if (Table.Question.SubTotalCnt > 0)
                {
                    int subTtlCl = c + ItemSectorsCount - (HasNAColumn && !CutNAColumn ? 0 : 1) - Table.Question.SubTotalCnt;
                    int endCl = c + ItemSectorsCount - (HasNAColumn && !CutNAColumn ? 0 : 1);
                    CellRangeAddress subTtlClC = CellRangeAddress.ValueOf(ColumnIndexToColumnLetter(subTtlCl) + 6 + ":" + ColumnIndexToColumnLetter(subTtlCl) + lastRow);
                    CellRangeAddress endClC = CellRangeAddress.ValueOf(ColumnIndexToColumnLetter(endCl) + 6 + ":" + ColumnIndexToColumnLetter(endCl) + lastRow);
                    NpoiHelper.applyStyles(baseSheet as XSSFSheet, endClC, subTtlClC);
                }
            }

            //NpoiHelper.saveTmp(FormatBook, true);
            removeAxisMergeRegion(baseSheet, 2, firstDoublerow + d, 3, firstTripplerow + d, d, d2, 2, 3, (isDoubleOnly & IsSigTest));
            for (idx = 0; idx <= Table.AxesGroups.Count - 1; idx++)
            {
                int cpyDestinationRow = r;
                tmpSheet = baseSheet;
                int row = Table.AxesGroups[idx].Count == 1 ? firstDoublerow : firstTripplerow;
                int rowCount = Table.AxesGroups[idx].Count == 1 ? firstDoublerowCnt : firstTripplerowCnt;

                y = y + 1;  //' 小計行インデックス
                if (CutRowsCol.ContainsKey(y) && Table.AxesGroups[idx][0].SectorsCount > 0)
                {
                    row += d;
                    rowCount -= d;
                }
                else
                {
                    r = r + d;
                }

                for (int cpyRow = row; cpyRow < (row + rowCount); cpyRow++)
                {
                    tmpSheet.CopyRow(cpyRow, cpyDestinationRow);
                    cpyDestinationRow++;
                }
                _log.Debug("copy tmp rows complted");
                //    ' 行
                if (Table.AxesGroups[idx].Count == 1)
                { //' 二重クロス
                    r = r + d2; //' 2つ目の選択肢行
                    AxesInformation tmpAxes = Table.AxesGroups[idx];
                    SectorsCount[0] = tmpAxes[0].SectorsCount;
                    y = y + SectorsCount[0];
                    if (SectorsCount[0] > 3)
                    {
                        CopyShiftRowDown2Way(tmpSheet, ((SectorsCount[0] - 3) * d2), r, d2);
                    }
                    else if (SectorsCount[0] < 3)
                    {
                        DeleteShiftRowUp2Way(tmpSheet, SectorsCount[0], r, d2);
                    }
                    if (SectorsCount[0] > 0)
                    {
                        CellRangeAddress addMergedRegion = null;
                        addMergedRegion = CellRangeAddress.ValueOf("c" + (r - d2 + 1) + ":" + "c" + ((r - d2) + SectorsCount[0] * d2));
                        try
                        {
                            baseSheet.AddMergedRegion(addMergedRegion);
                        }
                        catch (Exception) { }
                    }
                    _log.Debug("adjust _SectorColumns rows complted");
                }
                else
                {   // ' 三重クロス
                    //rowCount = (r + d + 3 * d2) + (d + 3 * d2);
                    AxesInformation tmpAxes2 = Table.AxesGroups[idx];
                    SectorsCount[0] = tmpAxes2[0].SectorsCount;
                    n = tmpAxes2[1].SectorsCount + 1;
                    y = y + SectorsCount[0] * n;
                    if (SectorsCount[0] < 3)
                    {
                        DeleteShiftRowUp(tmpSheet, (3 - SectorsCount[0]) * (d + 3 * d2), r);
                    }
                    //        ' 内側(表側)の選択肢数(便宜)
                    SectorsCount[1] = n - 1;
                    if (SectorsCount[1] > 3)
                    {
                        for (i = r + 3 * d + 7 * d2; i >= r + d + d2; i = i - (d + 3 * d2))
                        {
                            if (bgWorker.CancellationPending) return;
                            if (tmpSheet.LastRowNum >= i)
                            {
                                CopyShiftRowDown(tmpSheet, ((SectorsCount[1] - 3) * d2), i, d2);
                                if (tmpSheet.LastRowNum >= i)
                                {
                                    CellRangeAddress cAddr = CellRangeAddress.ValueOf("d" + (i - (d + d2) + 1) + ":"
            + "d" + (i + (SectorsCount[1] - 1) * d2));
                                    baseSheet.AddMergedRegion(cAddr);
                                }
                            }
                        }
                    }
                    else if (SectorsCount[1] < 3)
                    {
                        for (i = r + 3 * d + 7 * d2; i >= r + d + d2; i = i - (d + 3 * d2))
                        {
                            if (tmpSheet.LastRowNum >= i)
                            {
                                DeleteShiftRowUp(tmpSheet, ((3 - SectorsCount[1]) * d2), i - d2);
                                if (tmpSheet.LastRowNum >= i)
                                {
                                    CellRangeAddress cAddr = CellRangeAddress.ValueOf("d" + (i - (d + d2) + 1) + ":"
            + "d" + (i + (SectorsCount[1] - 1) * d2));
                                    baseSheet.AddMergedRegion(cAddr);
                                }
                            }
                        }
                    }
                    if (SectorsCount[1] == 3)
                    {
                        for (i = r + 3 * d + 7 * d2; i >= r + d + d2; i = i - (d + 3 * d2))
                        {
                            if (tmpSheet.LastRowNum >= i)
                            {
                                CellRangeAddress cAddr = CellRangeAddress.ValueOf("d" + (i - (d + d2) + 1) + ":"
        + "d" + (i + (SectorsCount[1] - 1) * d2));
                                baseSheet.AddMergedRegion(cAddr);
                            }
                        }
                    }
                    if (SectorsCount[0] > 3)
                    {
                        int rowDiff = ((d + 3 * d2) + ((SectorsCount[1] - 3) * d2));
                        int lRow = tmpSheet.LastRowNum;
                        int fRow = lRow - rowDiff;
                        tmpSheet.ShiftRows(fRow, lRow, ((d + 3 * d2) + ((SectorsCount[1] - 3) * d2)) * (SectorsCount[0] - 3));
                        int t = 1, e;
                        while (t <= SectorsCount[0] - 3)
                        {
                            if (bgWorker.CancellationPending) return;
                            for (e = fRow - rowDiff; e <= fRow - 1; e++)
                            {
                                tmpSheet.GetRow(e).CopyRowTo(e + rowDiff);
                            }
                            t++; fRow = e + rowDiff;
                        }
                    }

                    int axisCount = SectorsCount[0] * (SectorsCount[1] * d2 + d);
                    if (SectorsCount[0] > 0)
                    {
                        CellRangeAddress addMergedRegion = null;
                        addMergedRegion = CellRangeAddress.ValueOf("c" + (r + 1) + ":" + "c" + (r + axisCount));
                        try
                        {
                            baseSheet.AddMergedRegion(addMergedRegion);
                        }
                        catch (Exception) { }
                    }
                    r = r + SectorsCount[0] * (SectorsCount[1] * d2 + d);
                    _log.Debug("adjust _SectorColumns rows complted");
                }
                r = (tmpSheet.LastRowNum + 1); //' 次行
                if (bgWorker.CancellationPending) return;
            }
            int len = (firstDoublerowCnt + firstTripplerowCnt + (firstDoublerowCnt > 0 && firstTripplerowCnt > 0 ? 2 : 1));
            for (int j = starttempRow; j <= len + starttempRow - 1; j++)
            {
                IRow row = tmpSheet.GetRow(j);
                if (row == null) continue;
                tmpSheet.RemoveRow(row);

            }
            firstRow = TableSheet.PhysicalNumberOfRows == 0 ? baseSheet.FirstRowNum : TableSheet.LastRowNum + 1;
            lastRow = firstRow + baseSheet.LastRowNum;
            NpoiHelper.copyRows(len + starttempRow, len, TableSheet as XSSFSheet, baseSheet as XSSFSheet, firstRow + 1, lastRow + 1, individualCross: true);
            //CopySheet(firstRow, lastRow, baseSheet, TableSheet);

            ContentsSheet = FormatBook.GetSheet("INDEX") as XSSFSheet;
            string tmpAddress = "'" + ContentsSheet.SheetName + "'!$A$1";
            int cnt = lastRow - firstRow - len + 1;
            Array tmpBuf = Array.CreateInstance(typeof(string), new int[] { cnt, 1 },
                new int[] { 1, 0 });

            XSSFCellStyle shrinkSt = null;
            XSSFCellStyle shrinkSt2 = null;
            for (i = 1; i <= cnt; i++)
            {
                tmpBuf.SetValue("", i, 0);
                XSSFHyperlink href = new XSSFHyperlink(HyperlinkType.Document);
                href.Address = tmpAddress;
                IRow row = TableSheet.GetRow(i - 1);
                row.GetCell(0).Hyperlink = href;
                if (i != cnt)
                {
                    if (i == 1)
                    {
                        if (null == shrinkSt)
                        {
                            shrinkSt = (XSSFCellStyle)FormatBook.CreateCellStyle();
                            shrinkSt.CloneStyleFrom(row.GetCell(0).CellStyle);
                            shrinkSt.ShrinkToFit = true;
                        }
                        row.GetCell(0).CellStyle = shrinkSt;
                    }
                    else
                    {
                        if (null == shrinkSt2)
                        {
                            shrinkSt2 = (XSSFCellStyle)FormatBook.CreateCellStyle();
                            shrinkSt2.CloneStyleFrom(row.GetCell(0).CellStyle);
                            shrinkSt2.ShrinkToFit = true;
                        }
                        row.GetCell(0).CellStyle = shrinkSt2;
                    }
                }
            }
            tmpBuf.SetValue(LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME, cnt, 0);
            NpoiHelper.PutValue(TableSheet as XSSFSheet, "A1:A" + cnt, tmpBuf);

            CellRangeAddress WithWorkingSheet1 = CellRangeAddress.ValueOf("C3:R3");
            TableSheet.AddMergedRegion(WithWorkingSheet1);
            WithWorkingSheet1 = CellRangeAddress.ValueOf("D4:R4");
            TableSheet.AddMergedRegion(WithWorkingSheet1);
        }

        public void FormatPortraitTable(XSSFWorkbook FormatBook, XSSFSheet namedSheet, ISheet TableSheet, ref XSSFName hyperLinkTargetName, int NumericCutCol, string TableIndex, CrossTable Table
              , CellRangeAddress TemplateSheet
              , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , ISheet FormatSheet, string FormatRangeNamePrefix
              , TableType TableType, bool HasWeight
              , CellRangeAddress StartCell, bool isN
              , ISheet ContentsSheet = null
              , bool IsReport = false
              , bool CutMedian = false, int MedIdx = -1
              , Hashtable WholeRowCol = null,
                        Array Ranking = null,
                                    Array TableValue = null)
        {
            bool RedrawBorder = false;
            bool HasNARow = false;
            bool HasIVRow = false;
            bool HasNAColumn = false;
            bool HasIVColumn = false;
            int d = 0;
            bool CutNARow = false;
            bool CutIVRow = false;
            bool CutWTRows = false;
            int ItemSectorsCount = 0;
            int[] SectorsCount = new int[2];
            int r = 0;
            int c = 0;
            int i = 0;
            int n = 0;
            CellRangeAddress tmpRange;
            CellRangeAddress tmpRange2;
            bool CutNA = false;
            bool CutIV = false;
            int rowNum_Border = 0;
            CellRangeAddress topRow;
            bool f;
            int idx = 0;
            int y = 0;
            int tmpC = 0;
            int tmpY = 0;
            int hc = 0;
            Array tmpBuf;
            string tmpAddress;
            int afterTopRow = 0;
            bool IsSigTest = false;
            TableType tType;
            IName WithFormatSheetHeader = null;
            ISheet baseSheet = null;


            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            HasNARow = CurrentOutput.ShowNAAtItem;
            HasIVRow = CurrentOutput.ShowIVAtItem;
            HasNAColumn = CurrentOutput.ShowNAAtAxis;
            HasIVColumn = CurrentOutput.ShowIVAtAxis;

            d = 1 + (ToInt(!isN) & ((ToInt(tType == TableType.NPer) & 1) + (ToInt(IsSigTest) & 1)));
            if (ContentsSheet != null)
            {
                topRow = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_Header").RefersToFormula);
                afterTopRow = baseSheet.PhysicalNumberOfRows;
            }
            else
            {
                topRow = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_TopHeader").RefersToFormula);
                afterTopRow = 1;
            }

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
                    if (CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2) - (Table.Question.SubTotalCnt)))
                    {
                        CutNARow = true;
                    }
                }
            }

            //Base sheet Created and Top header Copied
            baseSheet = NpoiHelper.createBaseSheet(FormatBook);
            if (afterTopRow == 1)
            {
                NpoiHelper.copyRows(baseSheet as XSSFSheet, namedSheet as XSSFSheet, FormatRangeNamePrefix + "_TopHeader", afterTopRow);
                afterTopRow = baseSheet.PhysicalNumberOfRows + 1;
            }

            //Total Header and columns copied and initial value calculated for header position
            tmpRange = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_Header").RefersToFormula);
            i = (tmpRange.FirstColumn + 1);
            hc = (tmpRange.LastColumn - tmpRange.FirstColumn) + 1;
            c = i + hc;
            //if (tmpRange.FirstRow - topRow.LastRow > 1)
            //{
            //    baseSheet.CopyRow(0, afterTopRow - 1);
            //    baseSheet.GetRow(afterTopRow - 1).HeightInPoints = (float)22.5;
            //    afterTopRow += 1;
            //}
            NpoiHelper.copyRows(baseSheet as XSSFSheet, namedSheet as XSSFSheet, FormatRangeNamePrefix + "_Header", afterTopRow);

            //Removing _Triple column or _Double column on the basis of AxesGroup[-].count and do the merging and unmerging area
            String value = Table.AxesGroups[0].Count == 1 ? "_Triple" : "_Double";
            WithFormatSheetHeader = GetNamedRange(namedSheet, FormatRangeNamePrefix + value);
            tmpRange = CellRangeAddress.ValueOf(WithFormatSheetHeader.RefersToFormula);
            NpoiHelper.deleteColumn(baseSheet as XSSFSheet, tmpRange);
            r = afterTopRow;
            if (value.Equals("_Triple"))
            {
                CellRangeAddress cell = new CellRangeAddress(r - 1, r - 1, tmpRange.FirstColumn, tmpRange.LastColumn);
                NpoiHelper.UnmergeSelectedRegion(baseSheet as XSSFSheet, cell.FormatAsString());
                cell = new CellRangeAddress(r - 1, r - 1, tmpRange.LastColumn + 2, (tmpRange.LastColumn + 2) + 2);
                NpoiHelper.UnmergeSelectedRegion(baseSheet as XSSFSheet, cell.FormatAsString());
                cell = new CellRangeAddress(r, r, tmpRange.FirstColumn, (tmpRange.FirstColumn) + 3);
                NpoiHelper.UnmergeSelectedRegion(baseSheet as XSSFSheet, cell.FormatAsString());
                cell = new CellRangeAddress(r, r, tmpRange.FirstColumn + 4, (tmpRange.FirstColumn + 4) + 3);
                NpoiHelper.UnmergeSelectedRegion(baseSheet as XSSFSheet, cell.FormatAsString());
                cell = new CellRangeAddress(r, r, tmpRange.FirstColumn + 8, (tmpRange.FirstColumn + 8) + 3);
                NpoiHelper.UnmergeSelectedRegion(baseSheet as XSSFSheet, cell.FormatAsString());
                //List<int> removeAllMergedArea = new List<int>();
                //for (int k = 0; k < baseSheet.NumMergedRegions; k++)
                //{
                //    removeAllMergedArea.Add(k);
                //}
                //baseSheet.RemoveMergedRegions(removeAllMergedArea);
                int replacedColumnStart = tmpRange.FirstColumn + 1;
                CellRangeAddress mergeRegion = new CellRangeAddress(r - 1, r - 1, replacedColumnStart, replacedColumnStart + 2);
                NpoiHelper.AddMergedRegion(baseSheet as XSSFSheet, mergeRegion);
            }
            else
            {
                CellRangeAddress cell = new CellRangeAddress(tmpRange.FirstRow, tmpRange.FirstRow, tmpRange.FirstColumn + 1, tmpRange.LastColumn);
                NpoiHelper.UnmergeSelectedRegion(baseSheet as XSSFSheet, cell.FormatAsString());
            }

            //Setting template row height as per requirement
            if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
            {
                baseSheet.GetRow(afterTopRow - 4).HeightInPoints = (float)22.50;
                baseSheet.GetRow(afterTopRow - 3).HeightInPoints = (float)24.90;
                baseSheet.GetRow(afterTopRow - 2).HeightInPoints = (float)3.00;
                baseSheet.GetRow(afterTopRow - 1).HeightInPoints = (float)33.75;
                baseSheet.GetRow(afterTopRow).HeightInPoints = (float)22.50;
                baseSheet.GetRow(afterTopRow + 1).HeightInPoints = (float)56.3;
            }
            else if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
            {
                baseSheet.GetRow(afterTopRow - 3).HeightInPoints = (float)24.90;
                baseSheet.GetRow(afterTopRow - 2).HeightInPoints = (float)3.00;
                baseSheet.GetRow(afterTopRow - 1).HeightInPoints = (float)33.75;
                baseSheet.GetRow(afterTopRow).HeightInPoints = (float)22.50;
                baseSheet.GetRow(afterTopRow + 1).HeightInPoints = (float)33.8;

            }
            tmpRange = null;

            y = 1;
            for (idx = 0; idx <= Table.AxesGroups.Count - 1; idx++)
            {
                tmpC = c;
                if (idx > 0)
                {
                    hc = 0;
                }
                tmpRange = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, Table.AxesGroups[idx].Count == 1 ? FormatRangeNamePrefix + "_Double" : FormatRangeNamePrefix + "_Triple").RefersToFormula);
                n = hc + ((tmpRange.LastColumn - tmpRange.FirstColumn) + 1);

                if (idx > 0)
                {
                    //Copy new tmpRange Columns
                    CellRangeAddress mergedRegion = null;
                    string format = "";
                    int mergedcellRegion = NpoiHelper.GetIndexIfCellIsInMergedCells(namedSheet, tmpRange.FirstRow, tmpRange.LastColumn);
                    if (mergedcellRegion != -1)
                    {
                        format = namedSheet.GetMergedRegion(mergedcellRegion).FormatAsString();
                        mergedRegion = CellRangeAddress.ValueOf(format);
                    }
                    NpoiHelper.CopyRange(tmpRange, namedSheet, baseSheet, afterTopRow - 1, c - 1);
                    tmpRange = new CellRangeAddress(afterTopRow - 1, (afterTopRow - 1) + (tmpRange.LastRow - tmpRange.FirstRow), c - 1, (c - 1) + tmpRange.LastColumn - tmpRange.FirstColumn);
                    if (mergedRegion != null)
                    {
                        int mergedCells = mergedRegion.LastColumn - mergedRegion.FirstColumn;
                        var newRegion = new CellRangeAddress(tmpRange.FirstRow, tmpRange.FirstRow, tmpRange.FirstColumn + 1, tmpRange.FirstColumn + 1 + mergedCells);
                        NpoiHelper.AddMergedRegion(baseSheet as XSSFSheet, newRegion);
                    }
                }
                else
                {
                    //Set the tmpRange as range of content in current sheet
                    tmpRange = new CellRangeAddress(r - 1, (r - 1) + (tmpRange.LastRow - tmpRange.FirstRow), i - 1, (n + (i - 1)) - 1);
                }

                if (Table.AxesGroups[idx].Count == 1)
                {
                    y = y + 1;
                    if (Table.AxesGroups[idx][0].SectorsCount > 0)
                    {
                        if (CutRowsCol.ContainsKey(y))
                        {
                            //CutColoums the column next to Total
                            CellRangeAddress cell = new CellRangeAddress(tmpRange.FirstRow, tmpRange.FirstRow, tmpRange.FirstColumn + hc + 1, tmpRange.LastColumn);
                            NpoiHelper.UnmergeSelectedRegion(baseSheet as XSSFSheet, cell.FormatAsString());
                            NpoiHelper.deleteColumn(baseSheet as XSSFSheet, idx == 0 ? c : tmpRange.FirstColumn);
                            tmpRange = new CellRangeAddress(tmpRange.FirstRow, tmpRange.LastRow, tmpRange.FirstColumn, tmpRange.LastColumn - 1);
                            cell = new CellRangeAddress(tmpRange.FirstRow, tmpRange.FirstRow, tmpRange.FirstColumn + hc, tmpRange.LastColumn);
                            NpoiHelper.AddMergedRegion(baseSheet as XSSFSheet, cell);
                        }
                        else
                        {
                            c = c + 1;
                        }
                    }
                    c = c + 1;
                    AxesInformation tmpAxes = Table.AxesGroups[idx];
                    SectorsCount[0] = tmpAxes[0].SectorsCount;
                    y = idx == 0 ? y + SectorsCount[0] + 1 : y + SectorsCount[0];

                    if (HasNAColumn)
                    {
                        y = y + 1;
                        if (!(CutRowsCol.ContainsKey(y)))
                        {
                            SectorsCount[0] = SectorsCount[0] + 1;
                        }
                    }

                    if (SectorsCount[0] > 3)
                    {
                        //If sector count greter than 3 then copying the required number of columns
                        String mergedRegion = "";
                        CellRangeAddress mergeRegion = null;
                        int mergedIndex = NpoiHelper.GetIndexIfCellIsInMergedCells(baseSheet, tmpRange.FirstRow, tmpRange.LastColumn);
                        if (mergedIndex != -1)
                        {
                            mergedRegion = baseSheet.GetMergedRegion(mergedIndex).FormatAsString();
                            NpoiHelper.UnmergeSelectedRegion(baseSheet as XSSFSheet, mergedRegion);
                            mergeRegion = CellRangeAddress.ValueOf(mergedRegion);
                        }
                        NpoiHelper.copyColumn(baseSheet as XSSFSheet, c - 1, SectorsCount[0] - 3);
                        tmpRange = new CellRangeAddress(tmpRange.FirstRow, tmpRange.LastRow, tmpRange.FirstColumn, tmpRange.LastColumn + SectorsCount[0] - 3);
                        if (mergeRegion != null)
                        {
                            mergeRegion = new CellRangeAddress(mergeRegion.FirstRow, mergeRegion.FirstRow, mergeRegion.FirstColumn, tmpRange.LastColumn);
                            NpoiHelper.AddMergedRegion(baseSheet as XSSFSheet, mergeRegion);
                        }
                    }
                    else if (SectorsCount[0] < 3)
                    {
                        //If sector count less than 3 then deleting the required number of columns
                        String mergedRegion = "";
                        CellRangeAddress mergeRegion = null;
                        int mergedIndex = NpoiHelper.GetIndexIfCellIsInMergedCells(baseSheet, tmpRange.FirstRow, tmpRange.LastColumn);
                        if (mergedIndex != -1)
                        {
                            mergedRegion = baseSheet.GetMergedRegion(mergedIndex).FormatAsString();
                            NpoiHelper.UnmergeSelectedRegion(baseSheet as XSSFSheet, mergedRegion);
                            mergeRegion = CellRangeAddress.ValueOf(mergedRegion);
                        }
                        NpoiHelper.deleteColumn(baseSheet as XSSFSheet, (idx == 0 && CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi) ? c - 1 : c - 1, 3 - SectorsCount[0]);
                        tmpRange = new CellRangeAddress(tmpRange.FirstRow, tmpRange.LastRow, tmpRange.FirstColumn, tmpRange.LastColumn - (3 - SectorsCount[0]));
                        if (mergeRegion != null)
                        {
                            if (mergeRegion.FirstColumn < tmpRange.LastColumn)
                            {
                                mergeRegion = new CellRangeAddress(mergeRegion.FirstRow, mergeRegion.FirstRow, mergeRegion.FirstColumn, tmpRange.LastColumn);
                                NpoiHelper.AddMergedRegion(baseSheet as XSSFSheet, mergeRegion);
                            }
                        }

                        if (SectorsCount[0] < 2 && checkSimpleAggr(Table))
                        {
                            //GT tabulation
                            if (tmpRange.FirstColumn != tmpRange.LastColumn)
                            {
                                NpoiHelper.deleteColumn(baseSheet as XSSFSheet, tmpRange.LastColumn + 1, 1);
                                tmpRange = new CellRangeAddress(tmpRange.FirstRow, tmpRange.LastRow, tmpRange.FirstColumn, tmpRange.LastColumn - 1);
                            }
                        }
                    }

                    //Merging heading regions
                    if (!checkSimpleAggr(Table))
                    {
                        if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                        {
                            for (int itr = idx == 0 ? 4 : tmpRange.FirstColumn; itr <= tmpRange.LastColumn; itr++)
                            {
                                CellRangeAddress mergeRegion = new CellRangeAddress(6, 7, itr, itr);
                                if (!baseSheet.MergedRegions.Contains(mergeRegion))
                                    NpoiHelper.AddMergedRegion(baseSheet as XSSFSheet, mergeRegion);
                            }
                        }
                        else if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                        {
                            for (int itr = idx == 0 ? 3 : tmpRange.FirstColumn; itr <= tmpRange.LastColumn; itr++)
                            {
                                CellRangeAddress mergeRegion = new CellRangeAddress(5, 6, itr, itr);
                                if (!baseSheet.MergedRegions.Contains(mergeRegion))
                                    NpoiHelper.AddMergedRegion(baseSheet as XSSFSheet, mergeRegion);
                            }
                        }
                    }
                    else
                    {
                        if (IsSigTest)
                        {
                            if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                            {
                                for (int itr = idx == 0 ? 3 : tmpRange.FirstColumn; itr <= tmpRange.LastColumn; itr++)
                                {
                                    CellRangeAddress mergeRegion = new CellRangeAddress(4, 5, itr, itr);
                                    if (!baseSheet.MergedRegions.Contains(mergeRegion))
                                        NpoiHelper.AddMergedRegion(baseSheet as XSSFSheet, mergeRegion);
                                }
                            }
                            else
                            {
                                for (int itr = idx == 0 ? 4 : tmpRange.FirstColumn; itr <= tmpRange.LastColumn; itr++)
                                {
                                    CellRangeAddress mergeRegion = new CellRangeAddress(5, 6, itr, itr);
                                    if (!baseSheet.MergedRegions.Contains(mergeRegion))
                                        NpoiHelper.AddMergedRegion(baseSheet as XSSFSheet, mergeRegion);
                                }
                            }
                        }
                    }
                }
                c = tmpC + (tmpRange.LastColumn - tmpRange.FirstColumn + 1) - hc;
            }

            if (isN)
            {
                if (CutMedian && CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                {
                    r = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_MedianRow").RefersToFormula).LastRow - topRow.FirstRow + 1; //CutRowsCount      
                    int cell = (IsSigTest) ? tmpRange.LastRow - 1 : tmpRange.LastRow;
                    baseSheet.ShiftRows(cell, cell, -d);
                    tmpRange.LastRow += -1;
                }

                if (IsSigTest && checkSimpleAggr(Table))
                {
                    r = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_SigTestRow").RefersToFormula).LastRow - topRow.FirstRow + 1; //CutSigTestResult      
                    int cell = CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single ? tmpRange.LastRow + 1 : tmpRange.LastRow;
                    cell = CurrentOutput.ShowPreWBTotal && CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single ? cell + 1 : cell;
                    baseSheet.ShiftRows(cell, cell, -d);
                    tmpRange.LastRow += -1;
                    if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                    {
                        if (CutNARow || !HasNARow)
                            baseSheet.CopyRow(tmpRange.LastRow - 1, tmpRange.LastRow);
                        IRow row2 = baseSheet.GetRow(tmpRange.LastRow);
                        NpoiHelper.SetStyleToEntireRow(row2, CellUtil.BORDER_TOP, BorderStyle.Hair);
                    }
                }

                if (CutMedian && CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                {
                    r = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_MedianRow").RefersToFormula).LastRow - topRow.FirstRow + 1; //CutRowsCount      
                    int cell = (IsSigTest) ? tmpRange.LastRow - 1 : tmpRange.LastRow;
                    baseSheet.ShiftRows(cell, cell, -d);
                    tmpRange.LastRow += -1;
                }
            }
            else
            {
                if (ItemSectorsCount != 2)
                {
                    r = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_SectorRows").RefersToFormula).FirstRow - topRow.FirstRow + 1 + d;//CutRowsCount
                    CellRangeAddress tmpRange3 = null;
                    bool shift = true;
                    bool setBorder = false;
                    bool setStyleBottom = false;
                    int rowNumToSetBorder = 0;
                    tmpRange2 = new CellRangeAddress(r - 1, ((r - 1) + d) - 1, tmpRange.FirstColumn, tmpRange.LastColumn);
                    if (HasWeight || HasNARow)
                    {
                        tmpRange3 = new CellRangeAddress(tmpRange2.LastRow + 1, tmpRange.LastRow, tmpRange.FirstColumn, tmpRange.LastColumn);
                    }
                    else
                    {
                        tmpRange3 = tmpRange2;
                        IRow row = baseSheet.GetRow(tmpRange2.LastRow);
                        NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_BOTTOM, BorderStyle.Hair);
                        shift = false;
                    }

                    if (shift)
                    {
                        //Shifting the required cells to the correct position
                        baseSheet.ShiftRows(tmpRange3.FirstRow, tmpRange3.LastRow, (ItemSectorsCount - 2) * d);
                        tmpRange.LastRow += (ItemSectorsCount - 2) * d;

                        if (ItemSectorsCount - Table.Question.SubTotalCnt != 1)
                        {
                            IRow row = baseSheet.GetRow(tmpRange2.LastRow);
                            NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_BOTTOM, BorderStyle.Hair);
                        }
                    }

                    if (ItemSectorsCount - Table.Question.SubTotalCnt == 1)
                    {
                        setBorder = true;
                        rowNumToSetBorder = HasNARow ? tmpRange2.LastRow : tmpRange2.FirstRow;
                        setStyleBottom = HasNARow ? true : false;
                        if (HasNARow && CutNARow && Table.Question.SubTotalCnt > 0)
                        {
                            rowNum_Border = tmpRange2.FirstRow;
                            setBorder = false;
                            RedrawBorder = true;
                        }
                    }
                    else if (ItemSectorsCount - Table.Question.SubTotalCnt == 2 && !HasNARow)
                    {
                        setBorder = true;
                        rowNumToSetBorder = tmpRange2.LastRow;
                        setStyleBottom = true;
                    }

                    if (ItemSectorsCount > 2)
                    {
                        int noAnswerRow = HasNARow ? 1 : 0;
                        for (int itr = 1; itr <= (ItemSectorsCount - 2); itr++)
                        {
                            if (shift)
                            {
                                //If the shifting is done copying the sector row in the required region
                                if (Table.Question.SubTotalCnt > 0 && (ItemSectorsCount - 2 - Table.Question.SubTotalCnt) + noAnswerRow == itr)
                                {
                                    IRow row = baseSheet.GetRow(tmpRange2.LastRow);
                                    NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_BOTTOM, BorderStyle.Thin);
                                    NpoiHelper.CopyRangeofRow(tmpRange2, baseSheet, tmpRange3.FirstRow);
                                    tmpRange3.FirstRow += d;
                                    tmpRange3.LastRow += d;
                                    NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_BOTTOM, BorderStyle.Hair);
                                }
                                else
                                {
                                    NpoiHelper.CopyRangeofRow(tmpRange2, baseSheet, tmpRange3.FirstRow);
                                    tmpRange3.FirstRow += d;
                                    tmpRange3.LastRow += d;
                                }


                            }
                            else
                            {
                                //Simply copying the required region 
                                if (Table.Question.SubTotalCnt > 0 && (ItemSectorsCount - 2 - Table.Question.SubTotalCnt) + noAnswerRow == itr)
                                {
                                    IRow row = baseSheet.GetRow(tmpRange2.LastRow);
                                    NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_BOTTOM, BorderStyle.Thin);
                                    NpoiHelper.CopyRangeofRow(tmpRange2, baseSheet, tmpRange.LastRow + 1);
                                    tmpRange.LastRow += d;
                                    NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_BOTTOM, BorderStyle.Hair);
                                }
                                else
                                {
                                    NpoiHelper.CopyRangeofRow(tmpRange2, baseSheet, tmpRange.LastRow + 1);
                                    tmpRange.LastRow += d;
                                }

                            }
                        }
                        if (setBorder)
                        {
                            IRow row = baseSheet.GetRow(rowNumToSetBorder);
                            NpoiHelper.SetStyleToEntireRow(row, setStyleBottom ? CellUtil.BORDER_BOTTOM : CellUtil.BORDER_TOP, BorderStyle.Thin);
                        }
                    }
                    else
                    {
                        //if column variable sector is less than 2 delete the required range of column and shift the range up
                        if (!HasNARow && !HasWeight)
                        {
                            NpoiHelper.ShiftRowUp(baseSheet as XSSFSheet, tmpRange2.FirstRow, (2 - ItemSectorsCount) * d);
                            tmpRange = new CellRangeAddress(tmpRange.FirstRow, (tmpRange.LastRow + (2 - ItemSectorsCount) * d), tmpRange.FirstColumn, tmpRange.LastColumn);
                        }
                        else if (HasNARow && CutNARow && !HasWeight)
                        {
                            NpoiHelper.ShiftRowUp(baseSheet as XSSFSheet, tmpRange2.FirstRow, (2 - ItemSectorsCount) * d);
                            tmpRange = new CellRangeAddress(tmpRange.FirstRow, (tmpRange.LastRow - (2 - ItemSectorsCount) * d), tmpRange.FirstColumn, tmpRange.LastColumn);
                            CutNARow = false;
                        }
                        IRow row = baseSheet.GetRow(baseSheet.LastRowNum);
                        NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_BOTTOM, BorderStyle.Thin);
                    }
                }
                else if ((ItemSectorsCount - Table.Question.SubTotalCnt) == 1)
                {
                    if (!HasWeight)
                    {
                        if (CutNARow)
                        {
                            if (d < 2)
                            {
                                CellRangeAddress cellRegion = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_NoAnswerRow").RefersToFormula);
                                r = cellRegion.LastRow - topRow.FirstRow + 1;//CutRowsCount
                                NpoiHelper.ShiftRowUp(baseSheet as XSSFSheet, r - d, d);
                                tmpRange = new CellRangeAddress(tmpRange.FirstRow, tmpRange.LastRow - d, tmpRange.FirstColumn, tmpRange.LastColumn);
                                CutNARow = false;
                                IRow row = baseSheet.GetRow(tmpRange.LastRow - (d - 1));
                                baseSheet.CopyRow(row.RowNum, row.RowNum - 1);
                            }
                            else
                            {
                                IRow row = baseSheet.GetRow(tmpRange.LastRow - (d - 1));
                                NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_TOP, BorderStyle.Thin);
                            }
                        }
                        else
                        {
                            if (d < 2)
                            {
                                IRow row = baseSheet.GetRow(baseSheet.LastRowNum);
                                NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_BOTTOM, BorderStyle.Thin);
                                row = baseSheet.GetRow(tmpRange.LastRow - (d - 1));
                                baseSheet.CopyRow(row.RowNum, row.RowNum - 1);
                            }
                            else
                            {
                                IRow row = baseSheet.GetRow(tmpRange.LastRow - (d - 1));
                                NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_TOP, BorderStyle.Thin);
                            }

                        }
                    }
                    else
                    {
                        if (TablesOnOneSheet.Single == CurrentOutput.TablesOnOnesheet)
                        {
                            if (HasNARow && CutNARow)
                            {
                                if (d < 2)
                                {
                                    CellRangeAddress cellRegion = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_NoAnswerRow").RefersToFormula);
                                    r = cellRegion.LastRow - topRow.FirstRow + 1;//CutRowsCount
                                    NpoiHelper.ShiftRowUp(baseSheet as XSSFSheet, r - d, d);
                                    tmpRange = new CellRangeAddress(tmpRange.FirstRow, tmpRange.LastRow - d, tmpRange.FirstColumn, tmpRange.LastColumn);
                                    CutNARow = false;
                                    IRow row = baseSheet.GetRow((tmpRange.LastRow - (d * 2)) - (d - 1));
                                    baseSheet.CopyRow(row.RowNum, row.RowNum - 1);
                                }
                                else
                                {
                                    IRow row = baseSheet.GetRow((tmpRange.LastRow - (d * 2)) - (d - 1));
                                    NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_TOP, BorderStyle.Thin);
                                }
                            }
                            else if (HasNARow && !CutNARow)
                            {
                                IRow row = baseSheet.GetRow((tmpRange.LastRow - (d * 2)) - (d - 1) - 1);
                                NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_BOTTOM, BorderStyle.Thin);
                            }
                            else if (!HasNARow)
                            {
                                IRow row = baseSheet.GetRow((tmpRange.LastRow - (d * 2)) - (d - 1));
                                NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_TOP, BorderStyle.Thin);
                            }
                        }
                        else
                        {

                            IRow row = baseSheet.GetRow((tmpRange.LastRow - (d * 2)) - (d - 1));
                            NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_TOP, BorderStyle.Thin);
                        }
                    }
                }
            }

            if (CutNARow)
            {
                //Cut NA answer row and unmerge the deleted rows
                CellRangeAddress cellRegion = CellRangeAddress.ValueOf(GetNamedRange(namedSheet as XSSFSheet, FormatRangeNamePrefix + "_NoAnswerRow").RefersToFormula);
                r = cellRegion.LastRow - topRow.FirstRow + 1;//CutRowsCount
                NpoiHelper.ShiftRowUp(baseSheet as XSSFSheet, r - d, d);
                tmpRange = new CellRangeAddress(tmpRange.FirstRow, tmpRange.LastRow - d, tmpRange.FirstColumn, tmpRange.LastColumn);
            }

            if (RedrawBorder)
            {
                IRow row = baseSheet.GetRow(rowNum_Border);
                NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_TOP, BorderStyle.Thin);
            }

            //In case HasNARow is deleted from the AdjustFormat function setting the last row style
            if (!HasNARow)
            {
                IRow row = baseSheet.GetRow(baseSheet.LastRowNum);
                NpoiHelper.SetStyleToEntireRow(row, CellUtil.BORDER_BOTTOM, BorderStyle.Thin);
            }
            //List<int> removeAllMergedArea1 = new List<int>();
            //for (int k = 0; k < baseSheet.NumMergedRegions; k++)
            //{
            //    removeAllMergedArea1.Add(k);
            //}
            //baseSheet.RemoveMergedRegions(removeAllMergedArea1);


            //For Simple aggregation, setting the position of table heading 
            if (NPOICrossCreator.checkSimpleAggr(Table))
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                {
                    int startingPosition = 3;
                    int endingPosition = TableValue.GetLength(TableValue.GetLowerBound(0));
                    baseSheet.ShiftRows(8, baseSheet.LastRowNum, -2, true, true);
                    baseSheet.ShiftRows(5, baseSheet.LastRowNum, 2, true, true);
                    baseSheet.CreateRow(5);
                    baseSheet.CreateRow(6);
                    IRow row = baseSheet.GetRow(7);
                    if (row != null)
                    {
                        row.HeightInPoints = (float)56.3;
                        while (startingPosition <= endingPosition)
                        {
                            baseSheet.GetRow(7).GetCell(startingPosition + 1).SetCellValue(Convert.ToString(TableValue.GetValue(5, startingPosition)));
                            startingPosition++;
                        }

                        //Setting narrowing condition
                        ICell cell = row.CreateCell(2);
                        cell.CellStyle.VerticalAlignment = VerticalAlignment.Bottom;
                        cell.SetCellValue(Table.Question.NarrowingCondition);
                    }
                }
                else if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                {
                    IRow row = checkSimpleAggr(Table) ? baseSheet.GetRow(4) : baseSheet.GetRow(6);
                    if (row != null)
                    {
                        row.HeightInPoints = (float)33.8;
                        NpoiHelper.autoFitSingleRow(row, false);

                        ICell cell = row.CreateCell(1);
                        cell.CellStyle.VerticalAlignment = VerticalAlignment.Bottom;
                        cell.SetCellValue(Table.Question.NarrowingCondition);
                    }
                    baseSheet.ShiftRows(7, baseSheet.LastRowNum, -2, true, true);
                }
            }
            else
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                {
                    IRow row = baseSheet.GetRow(6);
                    if (row != null)
                    {
                        ICell cell = row.CreateCell(1);
                        cell.CellStyle.VerticalAlignment = VerticalAlignment.Bottom;
                        cell.SetCellValue(Table.Question.NarrowingCondition);
                    }
                }
                else if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                {
                    IRow row = baseSheet.GetRow(7);
                    if (row != null)
                    {
                        ICell cell = row.CreateCell(2);
                        cell.CellStyle.VerticalAlignment = VerticalAlignment.Bottom;
                        cell.SetCellValue(Table.Question.NarrowingCondition);
                    }
                }
            }

            if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
            {
                NpoiHelper.copyRows(0, 0, TableSheet as XSSFSheet, baseSheet as XSSFSheet, baseSheet.FirstRowNum + 1, baseSheet.LastRowNum + 1, individualCross: true);
                if (TableSheet.GetRow(1) != null)
                {
                    if (TableSheet.GetRow(1).GetCell(2) == null)
                        TableSheet.GetRow(1).CreateCell(2);
                }

                ContentsSheet = FormatBook.GetSheet("INDEX") as XSSFSheet;
                tmpAddress = "'" + ContentsSheet.SheetName + "'!$A$1";

                tmpBuf = Array.CreateInstance(typeof(string), new int[] { tmpRange.LastRow + 1, 1 },
                   new int[] { 1, 0 });

                try
                {
                    XSSFCellStyle shrinkSt = null;
                    XSSFCellStyle shrinkSt2 = null;
                    for (i = 1; i <= tmpRange.LastRow + 1; i++)
                    {
                        tmpBuf.SetValue("", i, 0);
                        XSSFHyperlink href = new XSSFHyperlink(HyperlinkType.Document);
                        href.Address = tmpAddress;
                        IRow row = TableSheet.GetRow(i - 1);
                        if (row == null)
                            continue;
                        if (row.GetCell(0) != null)
                            row.GetCell(0).Hyperlink = href;
                        else
                            row.CreateCell(0).Hyperlink = href;


                        if (i == 1)
                        {
                            if (null == shrinkSt)
                            {
                                shrinkSt = (XSSFCellStyle)FormatBook.CreateCellStyle();
                                shrinkSt.CloneStyleFrom(row.GetCell(0).CellStyle);
                                shrinkSt.ShrinkToFit = true;
                            }
                            row.GetCell(0).CellStyle = shrinkSt;
                        }
                        else
                        {
                            if (null == shrinkSt2)
                            {
                                shrinkSt2 = (XSSFCellStyle)FormatBook.CreateCellStyle();
                                shrinkSt2.CloneStyleFrom(row.GetCell(0).CellStyle);
                                shrinkSt2.ShrinkToFit = true;
                            }
                            row.GetCell(0).CellStyle = shrinkSt2;
                        }

                    }
                    tmpBuf.SetValue(LocalResource.REPORT_CROSS_CONTENTS_SHEET_NAME, tmpRange.LastRow + 1, 0);
                    NpoiHelper.PutValue(TableSheet as XSSFSheet, "A1:A" + tmpRange.LastRow + 1, tmpBuf);
                    FormatBook.SetActiveSheet(FormatBook.GetSheetIndex("INDEX"));
                }
                catch
                { }
            }
            else
            {
                int firstRow = TableSheet.PhysicalNumberOfRows == 0 ? baseSheet.FirstRowNum : TableSheet.LastRowNum + 1;
                int lastRow = firstRow + baseSheet.LastRowNum;
                //NpoiHelper.copyRows(TableSheet as XSSFSheet, baseSheet as XSSFSheet, firstRow + 1, 1, lastRow - firstRow + 1);
                NpoiHelper.copyRows(0, 0, TableSheet as XSSFSheet, baseSheet as XSSFSheet, baseSheet.FirstRowNum + 1, baseSheet.LastRowNum + 1, firstRow + 1, individualCross: false);
                if (TableSheet.GetRow(1).GetCell(2) == null)
                    TableSheet.GetRow(1).CreateCell(2);
                TableSheet.CreateRow(firstRow + 1).CreateCell(1).SetCellValue(TableIndex);
                if (!checkSimpleAggr(Table))
                    insertValuerangeAddress = new CellRangeAddress(2 + firstRow, TableSheet.LastRowNum/*TableValue.GetUpperBound(0) + firstRow + 2*/, 1, TableValue.GetUpperBound(1));
                else
                    insertValuerangeAddress = new CellRangeAddress(firstRow, TableSheet.LastRowNum/*TableValue.GetUpperBound(0) + firstRow*/, 1, TableValue.GetUpperBound(1));
                flag = firstRow;
            }
        }

        private void removeAxisMergeRegion(ISheet baseSheet, int firstDoubleCol, int firstDoublerow, int firstTrippleCol,
            int firstTripplerow, int d, int d2, int titleRow, int sentanceRow, bool addRow = false)
        {
            List<int> idx = new List<int>();
            for (int i = 0; i < baseSheet.NumMergedRegions; i++)
            {
                CellRangeAddress mReg = baseSheet.GetMergedRegion(i);
                if (firstDoubleCol > 0)
                {
                    if (mReg.FirstRow == firstDoublerow && mReg.FirstColumn == firstDoubleCol)
                    {
                        idx.Add(i);
                    }
                }
                if (firstTrippleCol > 0)
                {
                    if (mReg.FirstRow == firstTripplerow && mReg.FirstColumn == firstTrippleCol - 1)
                    {
                        idx.Add(i);
                    }
                    if (mReg.FirstRow == firstTripplerow && mReg.FirstColumn == firstTrippleCol)
                    {
                        idx.Add(i);
                    }
                    if (mReg.FirstRow == (firstTripplerow + 3 * d2 + d + (addRow ? 1 : 0)) && mReg.FirstColumn == firstTrippleCol)
                    {
                        idx.Add(i);
                    }
                    if (mReg.FirstRow == (firstTripplerow + 6 * d2 + 2 * d) && mReg.FirstColumn == firstTrippleCol)
                    {
                        idx.Add(i);
                    }
                }
                if (mReg.FirstRow == titleRow || mReg.FirstRow == sentanceRow)
                {
                    idx.Add(i);
                }


            }
            baseSheet.RemoveMergedRegions(idx);
        }

        //to do  maxsheet split
        private XSSFSheet CreateNewSheet(ref XSSFWorkbook formatBook
              , CrossTable Table
              , ref XSSFSheet ContentsSheet
              , ref Array ContentsValue  //string 
              , ref Array HyperlinkTargetCells  //Range 
              , ref Array HyperlinkTargetSheets
              , ref List<XSSFSheet> OrgSheets
              , string FormatPath
            , ref List<XSSFWorkbook> Books
            , ref XSSFSheet FormatSheet
            , ref XSSFSheet DoubleFormatSheet
            , bool HasWeightColumn
            , int m
              , bool IsSigTest = false
            , TableType TableType = 0
              )
        {
            int MAX_SHEETS_COUNT = int.MaxValue; //redmine - 183710 ,No need divsion
            int MAX_SHEET_NAME_LENGTH = 31;
            int MaxAxesCount;
            XSSFSheet TemplateSheet;
            string tmp;
            string TempSheetName = "";
            string ReportTitle;
            string header;
            string n;
            XSSFSheet sh;
            int i;
            int MinIndex;
            int MaxIndex = -1;
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
                createNewBook(ref Books, FormatPath, ref formatBook, ref FormatSheet, ref DoubleFormatSheet, HasWeightColumn,
                        m, TableType, ref ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, ref HyperlinkTargetSheets,
                        ref OrgSheets, MinIndex, MaxIndex);
            }

            n = formatBook.NumberOfSheets - OrgSheets.Count + 1 + "(" + Table.Question.Name + ")";
            TemplateSheet = formatBook.GetSheet(TempSheetName) as XSSFSheet;

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
                        sh = formatBook.GetSheet(n) as XSSFSheet;
                    }
                    catch (Exception ex)
                    { }
                } while (sh != null);
            }
            XSSFSheet NewSheet = TemplateSheet.CopySheet(n) as XSSFSheet;
            ReportTitle = CurrentOutput.ParentRequest.Title;
            return NewSheet;
        }

        private void createNewBook(ref List<XSSFWorkbook> Books, string FormatPath, ref XSSFWorkbook FormatBook, ref XSSFSheet FormatSheet
            , ref XSSFSheet DoubleFormatSheet, bool HasWeightColumn, int m, TableType TableType, ref XSSFSheet ContentsSheet, ref Array ContentsValue,
           ref Array HyperLinkTargetCells, ref Array HyperlinkTargetSheets, ref List<XSSFSheet> OrgSheets, int MinIndex, int MaxIndex)
        {

            if (Books == null) { Books = new List<XSSFWorkbook>(); }
            if (Books.Count > 0)
            {
                PutContents(ContentsSheet, ref ContentsValue, ref HyperLinkTargetCells, ref HyperlinkTargetSheets, OrgSheets);
            }
            string pre = "";
            if (TableType == TableType.NPer)
            {
                pre = "NP_Std";
            }
            else if (TableType == TableType.N)
            {
                pre = "N_Std";

            }
            else if (TableType == TableType.Per)
            {
                pre = "P_Std";

            }
            else if (TableType == TableType.SignificanceTest)
            {
                pre = "P_Sig";

            }
            using (FileStream file = new FileStream(FormatPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                FormatBook = new XSSFWorkbook(file);
            }
            if ((m & 2) == 2)
            {// ' 三重あり
                FormatSheet = FormatBook.GetSheet(pre + "_Triple") as XSSFSheet;
            }
            if ((m & 1) == 1)
            { //' 二重あり
                if ((m & 2) == 0)
                {
                    DoubleFormatSheet = FormatBook.GetSheet(pre) as XSSFSheet;
                }
                else
                {
                    FormatSheet = FormatBook.GetSheet(pre + "_Triple") as XSSFSheet;
                    DoubleFormatSheet = FormatBook.GetSheet(pre) as XSSFSheet;
                }
            }
            if (FormatSheet != null)
                AdjustFormat(FormatBook, FormatSheet, null, 2, HasWeightColumn);
            if (DoubleFormatSheet != null)
                AdjustFormat(FormatBook, DoubleFormatSheet, null, 1, HasWeightColumn);

            NpoiHelper.createBaseSheet(FormatBook);


            OrgSheets = new List<XSSFSheet>();
            for (int k = 0; k < FormatBook.NumberOfSheets; k++)
            {
                OrgSheets.Add(FormatBook.GetSheetAt(k) as XSSFSheet);
            }
            ContentsSheet = FormatBook.GetSheet("INDEX") as XSSFSheet;
            AdjustContentsSheet(FormatBook, Books, ContentsSheet, ref ContentsValue, ref HyperLinkTargetCells, ref HyperlinkTargetSheets,
                TableType, MinIndex, MaxIndex);
            Books.Add(FormatBook);
        }

        private void PutContents(
             XSSFSheet ContentsSheet, ref Array ContentsValue
           , ref Array HyperlinkTargetCells, ref Array HyperlinkTargetSheets //Excel.Range 
           , List<XSSFSheet> OrgSheets = null)
        {
            string OrgProcName;
            int i, j;
            XSSFSheet shts;
            //  Object sh  ;
            string n;
            XSSFSheet tmpSht;
            string tmp;
            XSSFName name = GetNamedRange(ContentsSheet, "Contents");
            CellRangeAddress WithContentsSheet = CellRangeAddress.ValueOf(name.RefersToFormula);
            AreaReference aref = new AreaReference(WithContentsSheet.FormatAsString(), SpreadsheetVersion.EXCEL2007);
            CellReference[] crefs = aref.GetAllReferencedCells();
            int row = crefs[0].Row;
            NpoiHelper.PutValue(ContentsSheet, WithContentsSheet.FormatAsString(), ContentsValue);

            for (i = HyperlinkTargetCells.GetLowerBound(0); i <= HyperlinkTargetCells.GetUpperBound(0); i++, row++)
            {
                XSSFRow r = ContentsSheet.GetRow(row) as XSSFRow;
                for (j = HyperlinkTargetCells.GetLowerBound(1); j <= HyperlinkTargetCells.GetUpperBound(1); j++)
                {
                    if (HyperlinkTargetCells.GetValue(i, j) != null)
                    {
                        CellRangeAddress tmpRng = (CellRangeAddress)HyperlinkTargetCells.GetValue(i, j);
                        XSSFHyperlink href = new XSSFHyperlink(HyperlinkType.Document);
                        tmp = "'" + HyperlinkTargetSheets.GetValue(i, j) + "'!$" + (ColumnIndexToColumnLetter(tmpRng.FirstColumn + 1)
                            + "$" + (tmpRng.FirstRow + (OrgSheets == null ? 3 : 1)) + ":$" + ColumnIndexToColumnLetter(tmpRng.LastColumn + 1) + "$" + (tmpRng.LastRow + 1));
                        href.Address = tmp;
                        r.GetCell(j).Hyperlink = href;
                        r.GetCell(j).CellStyle.ShrinkToFit = true;
                    }
                }
            }

            //saving  HyperlinkTargetCells values to static variable for combine banner post processing
            hyperlinks = HyperlinkTargetCells;

            //if (OrgSheets != null)
            //{  //  ' 1シート1クロス
            //    Application.DisplayAlerts = false;
            //    foreach (Worksheet sht in OrgSheets)
            //    {
            //        if (sht.Name != ContentsSheet.Name) { sht.Delete(); }
            //    }
            //    shts = ContentsSheet.Parent.Sheets;
            //    foreach (Worksheet sh in shts)
            //    {
            //        tmpSht = null;
            //        n = sh.Name + "~2";
            //        try
            //        {
            //            tmpSht = shts.Item[n];
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //        if (tmpSht != null)
            //        {
            //            sh.Name = sh.Name + "~1";
            //        }
            //        tmpSht = null;
            //    }
            //}
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

        public static void ArrayPreserve(ref Array v, Type type, int u)
        {
            Array t = (Array)v.Clone();
            v = Array.CreateInstance(type, u + 1);
            t.CopyTo(v, 0);
        }

        public void DeleteShiftRowUp(ISheet sheet, int resizeCount, int r)
        {
            int rowCount;
            for (rowCount = r; rowCount < (r + resizeCount); rowCount++)
            {
                IRow sht = sheet.GetRow(rowCount);
                if (sht != null)
                    sheet.RemoveRow(sht);
            }
            int last = sheet.LastRowNum;
            if (last < r + resizeCount + resizeCount - 1)
                last = r + resizeCount + resizeCount - 1;
            sheet.ShiftRows(r + resizeCount, last, -resizeCount, true, true);
        }

        public void DeleteShiftRowUp2Way(ISheet sheet, int chCnt, int r, int d2)
        {
            int start = r - d2;
            IRow row;
            IRow rowDest;
            row = sheet.GetRow(start + d2 * 3 - 1);
            rowDest = sheet.GetRow(start + chCnt * d2 - 1);
            NpoiHelper.CopyRowStyle(row, rowDest);

            r = start + chCnt * d2;
            int resizeCount = (3 - chCnt) * d2;
            List<int> delIdx = new List<int>();
            for (int rowCount = r; rowCount < (r + resizeCount); rowCount++)
            {
                IRow sht = sheet.GetRow(rowCount);

                if (sht != null)
                {
                    sheet.RemoveRow(sht);
                    delIdx.Add(rowCount);
                }
            }

            List<int> idx = new List<int>();
            for (int i = 0; i < sheet.NumMergedRegions; i++)
            {
                CellRangeAddress mReg = sheet.GetMergedRegion(i);
                foreach (int mrgIdx in delIdx)
                {
                    if (mReg.FirstRow == mrgIdx)
                    {
                        idx.Add(i);
                        break;
                    }
                }
            }
            sheet.RemoveMergedRegions(idx);
            //NpoiHelper.saveTmp(sheet.Workbook as XSSFWorkbook, true);
        }

        public void ShiftRowUp(ISheet sheet, int resizeCount, int r)
        {
            int last = sheet.LastRowNum;
            if (last < r + resizeCount + resizeCount - 1)
                last = r + resizeCount + resizeCount - 1;
            sheet.ShiftRows(r + resizeCount, last, -resizeCount, true, true);
        }
        public void RankMarking(CellRangeAddress range, ref Array Ranking, ISheet sheet)
        {
            //if  shouldCombineBanners==true ,store ranking array to RangeRankingSheetData class
            if (ShouldCombineBanners)
            {
                // Append the data to the static class for later use
                RangeRankingSheetData data = new RangeRankingSheetData
                {
                    Range = range,
                    Ranking = Ranking,
                    Sheet = sheet
                };

                if (sheet.SheetName.Equals(LocalResource.REPORT_CROSS_NP_SHEET_NAME))
                {

                    RangeRankingSheetStore.NModeDataList.Add(data);
                }
                else if (sheet.SheetName.Equals(LocalResource.REPORT_CROSS_N_SHEET_NAME))
                {
                    RangeRankingSheetStore.NDataList.Add(data);
                }
                else if (sheet.SheetName.Equals(LocalResource.REPORT_CROSS_P_SHEET_NAME))
                {
                    RangeRankingSheetStore.ModDataList.Add(data);
                }
                return;
            }

            int rowNum, col, rangeRow = range.FirstRow - 1, rangeCol = range.FirstColumn - 1;
            int red, grn, blu;
            for (rowNum = Ranking.GetLowerBound(0); rowNum <= Ranking.GetUpperBound(0); rowNum++)
            {
                for (col = Ranking.GetLowerBound(1); col <= Ranking.GetUpperBound(1); col++)
                {
                    switch (Ranking.GetValue(rowNum, col))
                    {
                        case 1:
                            red = 0;//red
                            grn = 0;
                            blu = 255;
                            break;
                        case 2:
                            red = 255;//red
                            grn = 0;
                            blu = 0;//0XFF0000;//blue
                            break;
                        case 3:
                            red = 51;//red
                            grn = 153;
                            blu = 102;//0X339966;
                            break;
                        default:
                            continue;
                    }
                    IDrawing drawing = sheet.CreateDrawingPatriarch();
                    XSSFDrawing patriarch = (XSSFDrawing)sheet.CreateDrawingPatriarch();
                    XSSFClientAnchor a = (XSSFClientAnchor)drawing.CreateAnchor(Units.ToEMU(3), Units.ToEMU(3), Units.ToEMU(9), Units.ToEMU(9), rangeCol + col, rangeRow + rowNum, rangeCol + col, rangeRow + rowNum);
                    XSSFSimpleShape shape = patriarch.CreateSimpleShape(a);
                    shape.ShapeType = (int)ShapeTypes.Ellipse;
                    shape.SetLineStyleColor(255, 255, 255);
                    shape.SetFillColor(blu, grn, red);
                }
            }
        }

        public void SignificanceTestMarking(CellRangeAddress range, ref Array SigTestMarking, ISheet sheet, TableType tableType = TableType.NPer)
        {
            int y, x, rangeRow = range.FirstRow - 1, rangeCol = range.FirstColumn - 1;
            ICell c = null;
            string buf;
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
                            buf = loadGlobalModeSymbol(buf);
                        }
                        c = sheet.GetRow(rangeRow + y).GetCell(rangeCol + x);
                        XSSFCellStyle cellStyle = c.CellStyle as XSSFCellStyle;
                        XSSFCellStyle st;
                        IDictionary<XSSFCellStyle, XSSFCellStyle> clrdStls;
                        string key = buf + "_" + tableType + "_" + sheet.Workbook.GetHashCode();
                        if (!sigTestStyles.ContainsKey(key))
                        {
                            clrdStls = new Dictionary<XSSFCellStyle, XSSFCellStyle>();
                            st = createSigTestStyles(sheet.Workbook as XSSFWorkbook, c, buf);
                            clrdStls[cellStyle] = st;
                            sigTestStyles[key] = clrdStls;
                        }
                        else
                        {
                            clrdStls = sigTestStyles[key];
                            if (!clrdStls.ContainsKey(cellStyle))
                            {
                                st = createSigTestStyles(sheet.Workbook as XSSFWorkbook, c, buf);
                                clrdStls[cellStyle] = st;
                            }
                            else
                            {
                                st = clrdStls[cellStyle];
                            }
                        }

                        c.CellStyle = st;
                    }
                }
            }
        }

        public static string loadGlobalModeSymbol(string buf)
        {
            if (buf.Equals("▲"))
                buf = "+++";
            else if (buf.Equals("▼"))
                buf = "---";
            else if (buf.Equals("△"))
                buf = "++";
            else if (buf.Equals("▽"))
                buf = "--";
            else if (buf.Equals("∴"))
                buf = "+";
            else if (buf.Equals("∵"))
                buf = "-";
            return buf;
        }

        public XSSFCellStyle createSigTestStyles(XSSFWorkbook workbook, ICell cell, string buf)
        {
            XSSFCellStyle st = workbook.CreateCellStyle() as XSSFCellStyle;
            st.CloneStyleFrom(cell.CellStyle);
            string dtf = st.GetDataFormatString();
            IDataFormat dt = workbook.CreateDataFormat();
            dtf = @"""" + buf + @"""" + dtf;
            st.DataFormat = dt.GetFormat(dtf);
            return st;
        }

        public void Hatching(CellRangeAddress range, ref Array HatchingColorIndex, ISheet sheet, XSSFWorkbook workbook, TableType tableType = TableType.NPer)
        {
            int r, c, rangeRow = range.FirstRow - 1, rangeCol = range.FirstColumn - 1;
            for (r = HatchingColorIndex.GetLowerBound(0); r <= HatchingColorIndex.GetUpperBound(0); r++)
            {
                for (c = HatchingColorIndex.GetLowerBound(1); c <= HatchingColorIndex.GetUpperBound(1); c++)
                {
                    switch (HatchingColorIndex.GetValue(r, c))
                    {
                        case null:
                            break;
                        default:
                            ICell cell = sheet.GetRow(rangeRow + r).GetCell(rangeCol + c);
                            XSSFCellStyle style;
                            XSSFCellStyle celltyle = cell.CellStyle as XSSFCellStyle;
                            int intClr = Convert.ToInt32(HatchingColorIndex.GetValue(r, c));
                            string key = intClr + "_" + tableType + "_" + workbook.GetHashCode();
                            IDictionary<XSSFCellStyle, XSSFCellStyle> clrdStls;
                            if (!hatchedStyles.ContainsKey(key))
                            {
                                clrdStls = new Dictionary<XSSFCellStyle, XSSFCellStyle>();
                                style = createHatchedStyle(workbook, cell, intClr);
                                clrdStls[celltyle] = style;
                                hatchedStyles[key] = clrdStls;
                            }
                            else
                            {
                                clrdStls = hatchedStyles[key];
                                if (!clrdStls.ContainsKey(celltyle))
                                {
                                    style = createHatchedStyle(workbook, cell, intClr);
                                    clrdStls[celltyle] = style;
                                }
                                else
                                {
                                    style = clrdStls[celltyle];
                                }

                            }
                            cell.CellStyle = style;
                            break;
                    }
                }
            }
        }
        public XSSFCellStyle createHatchedStyle(XSSFWorkbook workbook, ICell cell, int intClr)
        {
            XSSFCellStyle style = workbook.CreateCellStyle() as XSSFCellStyle;
            style.CloneStyleFrom(cell.CellStyle);
            Color s = Color.FromArgb(intClr);
            XSSFColor clr = new XSSFColor(Color.FromArgb(s.B, s.G, s.R));
            style.FillForegroundColorColor = clr;
            style.FillPattern = (FillPatternType.SolidForeground);
            return style;
        }

        public void CopyShiftRowDown(ISheet sheet, int resizeCount, int r, int d2)
        {
            int rowCount = r;
            sheet.ShiftRows((r + d2), sheet.LastRowNum, resizeCount);

            switch (d2)
            {
                case 1:
                    while (rowCount <= (r + resizeCount) - 1)
                    {
                        if (bgWorker.CancellationPending) return;
                        sheet.GetRow(r).CopyRowTo((rowCount + d2));
                        rowCount++;
                    }
                    break;
                case 2:
                    while (rowCount <= (r + resizeCount) - 2)
                    {
                        if (bgWorker.CancellationPending) return;
                        sheet.GetRow(r).CopyRowTo((rowCount + d2));
                        rowCount++;
                        sheet.GetRow(r + 1).CopyRowTo((rowCount + d2));
                        rowCount++;
                    }
                    break;
                default:
                    break;
            }
        }

        public void CopyShiftRowDown2Way(ISheet sheet, int resizeCount, int r, int d2)
        {
            //made this function for avoiding the shift for performance optimization
            int rowCount = r;
            //sheet.ShiftRows((r + d2), sheet.LastRowNum, resizeCount);
            _log.Debug("shift1 rows complted");
            IRow row;
            IRow rowDest;
            switch (d2)
            {
                case 1:
                    sheet.GetRow(r + d2).CopyRowTo((r + d2 + resizeCount));
                    row = sheet.GetRow(r);
                    rowDest = sheet.GetRow(r + d2);
                    NpoiHelper.CopyRowStyle(row, rowDest);
                    while (rowCount <= (r + resizeCount) - 2)
                    {
                        if (bgWorker.CancellationPending) return;
                        sheet.GetRow(r).CopyRowTo((rowCount + d2 + d2));
                        rowCount++;
                    }
                    break;
                case 2:
                    sheet.GetRow(r + d2).CopyRowTo((r + d2 + resizeCount));
                    sheet.GetRow(r + d2 + 1).CopyRowTo((r + d2 + resizeCount + 1));
                    //sheet.RemoveRow(sheet.GetRow(r + d2));
                    //sheet.RemoveRow(sheet.GetRow(r + d2 + 1));
                    //sheet.GetRow(r).CopyRowTo((r + d2 ));
                    //sheet.GetRow(r + 1).CopyRowTo((r + d2 + 1));
                    //copyRows(XSSFSheet targetSheet, XSSFSheet sourSheetNpoi, int targetStartRow, int sourceStartRow, int cnt)
                    //NpoiHelper.copyRowsWithoutMerge(sheet as XSSFSheet, sheet as XSSFSheet, r + d2 + 1, r + 1, d2);

                    row = sheet.GetRow(r + 1);
                    rowDest = sheet.GetRow(r + d2 + 1);
                    NpoiHelper.CopyRowStyle(row, rowDest);

                    while (rowCount <= (r + resizeCount) - 4)
                    {
                        if (bgWorker.CancellationPending) return;
                        sheet.GetRow(r).CopyRowTo((rowCount + d2 + d2));
                        rowCount++;
                        sheet.GetRow(r + 1).CopyRowTo((rowCount + d2 + d2));
                        rowCount++;
                    }
                    break;
                default:
                    break;
            }
        }

        public void CopySheet(int firstRow, int lastRow, ISheet sourceSheet, ISheet destinationSheet, IName WithFormatSheetHeader = null)
        {
            IName name = WithFormatSheetHeader;
            var d = ColumnIndexToColumnLetter(sourceSheet.GetRow(sourceSheet.LastRowNum).LastCellNum) + lastRow;
            var range = (name == null ? CellRangeAddress.ValueOf("A1:" + d) : CellRangeAddress.ValueOf(name.RefersToFormula));

            for (var rowNum = firstRow; rowNum <= lastRow; rowNum++)
            {
                IRow sourceRow = sourceSheet.GetRow(range.FirstRow++);
                if (destinationSheet.GetRow(rowNum) == null)
                    destinationSheet.CreateRow(rowNum);
                if (sourceRow != null)
                {
                    IRow destinationRow = destinationSheet.GetRow(rowNum);
                    for (var col = range.FirstColumn; col < sourceRow.LastCellNum && col <= range.LastColumn; col++)
                    {
                        destinationRow.CreateCell(col);
                        CopyCell(sourceRow.GetCell(col), destinationRow.GetCell(col));
                    }
                    if (sourceRow.HeightInPoints != 15)
                        destinationRow.HeightInPoints = sourceRow.HeightInPoints;
                }
            }
        }

        public XSSFName GetNamedRange(XSSFSheet formatSheetNpoi, string cname)
        {
            XSSFWorkbook wb = formatSheetNpoi.GetWorkbook();
            IList<IName> names = wb.GetNames(cname);
            foreach (IName name in names)
            {
                if (name.SheetName == formatSheetNpoi.SheetName)
                {
                    XSSFName aNamedCell = name as XSSFName;
                    return aNamedCell;
                }
            }
            return null;
        }

        private void InsertValues(CellRangeAddress range, ISheet sheet, Array tableValue)
        {
            int i = 1, j = 1;

            for (var rowNum = range.FirstRow; rowNum <= range.LastRow; rowNum++)
            {
                IRow Row = sheet.GetRow(rowNum);
                for (var col = Row.FirstCellNum + 1; col < Row.LastCellNum; col++)
                {
                    var obj = tableValue.GetValue(i, j);
                    if (obj == null)
                    {
                        Row.GetCell(col).SetCellValue("");
                    }
                    else if (obj.GetType() == typeof(string))
                    {
                        Row.GetCell(col).SetCellValue((string)tableValue.GetValue(i, j));
                    }
                    else if (obj.GetType() == typeof(double))
                    {
                        Row.GetCell(col).SetCellValue((double)tableValue.GetValue(i, j));
                    }
                    else if (obj.GetType() == typeof(int))
                    {
                        Row.GetCell(col).SetCellValue((int)tableValue.GetValue(i, j));
                    }
                    else
                    {
                        Row.GetCell(col).SetCellValue((string)tableValue.GetValue(i, j));
                    }
                    j++;
                }
                i++; j = 1;
            }
        }

        private void CopyCell(ICell source, ICell destination)
        {
            if (destination != null && source != null)
            {
                //you can comment these out if you don't want to copy the style ...
                destination.CellComment = source.CellComment;
                destination.CellStyle = source.CellStyle;
                destination.Hyperlink = source.Hyperlink;

                switch (source.CellType)
                {
                    case NPOI.SS.UserModel.CellType.Formula:
                        destination.CellFormula = source.CellFormula; break;
                    case NPOI.SS.UserModel.CellType.Numeric:
                        destination.SetCellValue(source.NumericCellValue); break;
                    case NPOI.SS.UserModel.CellType.String:
                        destination.SetCellValue(source.StringCellValue); break;
                }
            }
        }

        private void SaveBook(XSSFWorkbook Book
             , string Prefix,
           string Suffix = ""
             , XlFileFormat FileFormat = XlFileFormat.xlOpenXMLWorkbook
            , List<XSSFSheet> OrgSheets = null, ISheet ContentsSheet = null, bool isMultiCross = false)
        {

            string ext = "";
            string n;
            string p;
            int i;

            for (i = Book.NumberOfSheets; i >= 1; i--)
            {

            }
            Prefix = prfix + Prefix;

            if (OrgSheets != null)
            {  //  ' 1シート1クロス
                foreach (XSSFSheet sht in OrgSheets)
                {
                    if (sht.SheetName != ContentsSheet.SheetName)
                    {
                        Book.RemoveAt(Book.GetSheetIndex(sht.SheetName));
                    }

                }
            }

            ext = FileFormat == XlFileFormat.xlExcel8 ? "xls" : "xlsx";
            do
            {
                n = Prefix + (i > 0 ? "_" + i : "") + Suffix + "." + ext;
                i = i + 1;
                p = OutputUtil.BuildPath(OutputDirectoryPath, n, xlApp.PathSeparator);
            } while (File.Exists(p));

            using (var fs = new FileStream(OutputDirectoryPath + @"\" + n, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                Book.Write(fs);
                fs.Close();
                outputFiles.Add(OutputDirectoryPath + @"\" + n);
            }
            Book.Close();
            // Check if both 'isMultiCross' and 'shouldCombineBanners' are true
            if (isMultiCross && ShouldCombineBanners)
            {
                // If both conditions are met, call the Combine method with the following arguments:
                // - 'hyperlinks' is an array or list of hyperlinks
                // - 'OutputDirectoryPath' is the directory where the output will be saved
                // - 'CurrentOutput' is the current output object or value  
                updateProgress(currentProgress, LocalResource.PB_EXCEL_GEN);
                CombineBanners.Combine(hyperlinks, Path.Combine(OutputDirectoryPath, n), CurrentOutput);
                CombineBanners.ClearDatas();
            }
            Microsoft.Office.Interop.Excel.Workbooks wbs = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook wb = wbs.Add(OutputDirectoryPath + @"\" + n);
            Microsoft.Office.Interop.Excel.Workbook targetWorkbook = wb.Application.Workbooks.Open(OutputDirectoryPath + @"\" + n);//ExcelUtil.OpenWorkbok(TemplatePath, sourceWorkbook.Application);
            targetWorkbook.Application.EnableEvents = false;
            Microsoft.Office.Interop.Excel.XlCalculation xlCalculation = targetWorkbook.Application.Calculation;
            targetWorkbook.Application.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual;
            targetWorkbook.Application.ScreenUpdating = false;
            foreach (Microsoft.Office.Interop.Excel.Worksheet shet in targetWorkbook.Worksheets)
            {
                shet.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                if (shet.Name == "INDEX")
                {
                    shet.Range["B11"].Value = LocalResource.REPORT_LAYOUT_QUESTION_NUMBER_COLUMN_CAPTION;
                    shet.Range["C11"].Value = LocalResource.REPORT_LAYOUT_TABLE_HEADING_COLUMN_CAPTION;
                    shet.Range["D11"].Value = LocalResource.REPORT_LAYOUT_QC3_DESCRIPTION_2COLUMN_CAPTION;
                    if (isMultiCross)
                    {
                        shet.Range["E11"].Value = LocalResource.REPORT_NP_TABLE_KEYWORD;
                        shet.Range["F11"].Value = LocalResource.REPORT_N_TABLE_KEYWORD;
                        shet.Range["G11"].Value = LocalResource.REPORT_P_TABLE_KEYWORD;
                        shet.Range["H11"].Value = LocalResource.REPORT_CROSS_SIGNIFICANCE_TEST_SHEET_NAME;
                        shet.Range["H11"].WrapText = true;
                    }
                }
            }
            targetWorkbook.Save();
            targetWorkbook.Application.EnableEvents = true;
            targetWorkbook.Application.Calculation = xlCalculation;
            targetWorkbook.Application.ScreenUpdating = true;
            //wb.Application.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            targetWorkbook.Close();
            wb.Close();
            wbs.Close();
        }

        private void SaveWorkBook(XSSFWorkbook workbook, List<XSSFSheet> OrgSheets = null, string Prefix = "", ISheet ContentsSheet = null, bool isMultiCross = false)
        {
            string fileName = "";
            string ext = "";
            string path = "";
            ext = "xlsx";
            if (OrgSheets != null)
            {  //  ' 1シート1クロス
                foreach (XSSFSheet sht in OrgSheets)
                {
                    if (sht.SheetName != ContentsSheet.SheetName)
                    {
                        workbook.RemoveAt(workbook.GetSheetIndex(sht.SheetName));
                    }
                }
            }
            if (!string.IsNullOrEmpty(prfix))
            {
                Prefix = prfix + Prefix;
            }
            string outPath = Path.Combine(Path.GetTempPath(), "QC4", "output");
            outPath = Path.Combine(outPath, Path.GetFileNameWithoutExtension(Util.Constants.Qc4FileName));
            GlobalMethodClass.GuaranteeDirectoryExist(outPath);
            DirectoryInfo dir = new DirectoryInfo(outPath);
            if (!dir.Attributes.HasFlag(FileAttributes.ReadOnly))
                dir.Attributes = FileAttributes.ReadOnly;
            fileName = Prefix + "." + ext;
            path = OutputUtil.BuildPath(outPath, fileName, xlApp.PathSeparator);

            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                workbook.Write(fs);
                fs.Close();
            }
            // Check if both 'isMultiCross' and 'shouldCombineBanners' are true
            if (isMultiCross && ShouldCombineBanners)
            {
                // If both conditions are met, call the Combine method with the following arguments:
                // - 'hyperlinks' is an array or list of hyperlinks
                // - 'OutputDirectoryPath' is the directory where the output will be saved
                // - 'CurrentOutput' is the current output object or value
                updateProgress(currentProgress, LocalResource.PB_EXCEL_GEN);
                CombineBanners.Combine(hyperlinks, path, CurrentOutput);
                CombineBanners.ClearDatas();

            }

            Microsoft.Office.Interop.Excel.Workbooks wbs = xlApp.Workbooks;
            //Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(path, CorruptLoad: Microsoft.Office.Interop.Excel.XlCorruptLoad.xlRepairFile);
            Microsoft.Office.Interop.Excel.Workbook wb = wbs.Add(path);
            wb.Unprotect(BookPSWD);
            wb.BuiltinDocumentProperties["Author"] = "MACROMILL, INC.";
            foreach (Microsoft.Office.Interop.Excel.Workbook wb1 in wbs)
                foreach (Microsoft.Office.Interop.Excel.Worksheet shet in wb1.Worksheets)
                {
                    shet.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                    if (shet.Name == "INDEX")
                    {
                        shet.Range["B11"].Value = LocalResource.REPORT_LAYOUT_QUESTION_NUMBER_COLUMN_CAPTION;
                        shet.Range["C11"].Value = LocalResource.REPORT_LAYOUT_TABLE_HEADING_COLUMN_CAPTION;
                        shet.Range["D11"].Value = LocalResource.REPORT_LAYOUT_QC3_DESCRIPTION_2COLUMN_CAPTION;
                        if (isMultiCross)
                        {
                            shet.Range["E11"].Value = LocalResource.REPORT_NP_TABLE_KEYWORD;
                            shet.Range["F11"].Value = LocalResource.REPORT_N_TABLE_KEYWORD;
                            shet.Range["G11"].Value = LocalResource.REPORT_P_TABLE_KEYWORD;
                            shet.Range["H11"].Value = LocalResource.REPORT_CROSS_SIGNIFICANCE_TEST_SHEET_NAME;
                            shet.Range["H11"].WrapText = true;
                        }
                    }
                }
            //wb.Application.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            workbook.Close();
        }

        public static void RemoveOutPutFiles()
        {

            string filePath = Path.Combine(Path.GetTempPath(), "QC4", "output");
            GlobalMethodClass.GuaranteeDirectoryExist(filePath);
            System.IO.DirectoryInfo di = new DirectoryInfo(filePath);

            if (di.GetFiles() != null)
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        file.Delete();

                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.Message);
                    }
                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    try
                    {
                        if (!dir.Attributes.HasFlag(FileAttributes.ReadOnly))
                        {
                            dir.Delete(true);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.Message);
                    }
                }
            }
        }

        private void drawColoum(XSSFWorkbook FormatBook, CellRangeAddress cellRange, ISheet sheet, int Coloum)
        {
            int i = 0, j = 0;
            for (i = cellRange.FirstRow; i <= cellRange.LastRow; i++)
            {
                IRow row = sheet.GetRow(i);
                for (j = Coloum; j <= Coloum; j++)
                {
                    DrawBoarderStyle(FormatBook, (XSSFRow)row, Coloum);
                }
            }
        }

        private void CopyColoum(XSSFWorkbook FormatBook, CellRangeAddress cellRange, ISheet sheet, int startColoum, int endColoum)
        {
            int i = 0, j = 0, coloumShift = 0;
            for (i = cellRange.FirstRow; i <= cellRange.LastRow; i++)
            {
                IRow row = sheet.GetRow(i);
                for (j = startColoum; j <= startColoum; j++)
                {
                    coloumShift = startColoum;
                    while (coloumShift++ < endColoum)
                        row.CopyCell(startColoum, coloumShift).CellStyle.BorderRight = BorderStyle.Hair;

                    DrawBoarderStyle(FormatBook, (XSSFRow)row, coloumShift - 1);
                }
            }
        }

        private void DrawBoarderStyle(XSSFWorkbook FormatBook, XSSFRow row, int coloumShift)
        {
            XSSFCellStyle destinationStyle = (XSSFCellStyle)FormatBook.CreateCellStyle();
            XSSFCellStyle sourceStyle = (XSSFCellStyle)row.GetCell(coloumShift - 1).CellStyle;
            XSSFColor clr = sourceStyle.GetBorderColor(NPOI.XSSF.UserModel.Extensions.BorderSide.RIGHT);
            destinationStyle.BorderRight = BorderStyle.Thin;
            destinationStyle.BorderTop = sourceStyle.BorderTop;
            destinationStyle.SetRightBorderColor(clr);
            destinationStyle.SetLeftBorderColor(clr);
            destinationStyle.SetTopBorderColor(clr);
            destinationStyle.SetBottomBorderColor(clr);
            destinationStyle.BorderBottom = sourceStyle.BorderBottom;
            destinationStyle.SetFillForegroundColor((XSSFColor)sourceStyle.FillForegroundColorColor);
            destinationStyle.FillPattern = sourceStyle.FillPattern;
            destinationStyle.DataFormat = sourceStyle.DataFormat;
            destinationStyle.WrapText = sourceStyle.WrapText;
            destinationStyle.VerticalAlignment = sourceStyle.VerticalAlignment;
            row.GetCell(coloumShift).CellStyle = destinationStyle;
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

        public void ChangingNamedRange(XSSFSheet formatSheetNpoi, string cname, string newFormulaToAppend, string existingFormula)
        {
            XSSFWorkbook wb = formatSheetNpoi.GetWorkbook();
            IList<IName> names = wb.GetNames(cname);
            foreach (IName name in names)
            {
                if (name.SheetName == formatSheetNpoi.SheetName)
                {
                    XSSFName aNamedCell = name as XSSFName;
                    String[] existingValues = existingFormula.Split('$');
                    String[] newValues = newFormulaToAppend.Split(':');
                    String newFormula = existingValues[0] + "$" + newValues[0].Substring(0, 1) + "$" + newValues[0].Substring(1) + ":" + "$" + newValues[1].Substring(0, 1) + "$" + newValues[1].Substring(1);
                    aNamedCell.RefersToFormula = newFormula; /*"NP_Std!$F$5:$Q$13"*/
                }
            }
        }

    }

    internal class LegendPostion
    {
        public int RateDifferenceLegend_Col1 = 6;
        public int RateDifferenceLegend_Dx1 = 264101;
        public int RateDifferenceLegend_Col2 = 7;
        public int RateDifferenceLegend_Dx2 = 626862;

        public int Level2HighLabel_Col1 = 7;
        public int Level2HighLabel_Dx1 = 17407;
        public int Level2HighLabel_Col2 = 7;
        public int Level2HighLabel_Dx2 = 593002;

        public int Level2HighPalette_Col1 = 6;
        public int Level2HighPalette_Dx1 = 306596;
        public int Level2HighPalette_Col2 = 6;
        public int Level2HighPalette_Dx2 = 620201;

        public int RankingMarkingLegendCol1 = 5;
        public int RankingMarkingLegendDx1 = 124631;
        public int RankingMarkingLegendCol2 = 6;
        public int RankingMarkingLegendDx2 = 191711;

        public int Rank1OvalCol1 = 5;
        public int Rank1OvalDx1 = 267506;
        public int Rank1OvalCol2 = 5;
        public int Rank1OvalDx2 = 324656;

        public int Rank1LabelCol1 = 5;
        public int Rank1LabelDx1 = 429431;
        public int Rank1LabelCol2 = 5;
        public int Rank1LabelDx2 = 602555;

        public LegendPostion()
        {

        }
        public LegendPostion(bool sigPresent = false)
        {
            if (!sigPresent)
            {
                RankingMarkingLegendCol1 = 6;
                RankingMarkingLegendDx1 = 562371;
                RankingMarkingLegendCol2 = 8;
                RankingMarkingLegendDx2 = 1207;

                Rank1OvalCol1 = 7;
                Rank1OvalDx1 = 77002;
                Rank1OvalCol2 = 7;
                Rank1OvalDx2 = 134152;

                Rank1LabelCol1 = 7;
                Rank1LabelDx1 = 238927;
                Rank1LabelCol2 = 7;
                Rank1LabelDx2 = 412051;
            }
            else
            {
                RankingMarkingLegendDx1 = 56049;
                RankingMarkingLegendDx2 = 122724;

                Rank1OvalDx1 = 198924;
                Rank1OvalDx2 = 256074;

                Rank1LabelDx1 = 360849;
                Rank1LabelDx2 = 533973;
            }
        }

    }

    internal class LegendPostionIndividual : LegendPostion
    {
        public LegendPostionIndividual()
        {
            RateDifferenceLegend_Col1 = 3;
            RateDifferenceLegend_Dx1 = 1969770;
            RateDifferenceLegend_Col2 = 4;
            RateDifferenceLegend_Dx2 = 617220;

            Level2HighLabel_Col1 = 4;
            Level2HighLabel_Dx1 = 7765;
            Level2HighLabel_Col2 = 4;
            Level2HighLabel_Dx2 = 583765;

            Level2HighPalette_Col1 = 3;
            Level2HighPalette_Dx1 = 2012265;
            RankingMarkingLegendDx2 = 1897380;
            Level2HighPalette_Col2 = 3;
            Level2HighPalette_Dx2 = 2325465;

            RankingMarkingLegendCol1 = 3;
            RankingMarkingLegendDx1 = 1202055;
            RankingMarkingLegendCol2 = 3;

            Rank1OvalCol1 = 3;
            Rank1OvalDx1 = 1344930;
            Rank1OvalCol2 = 3;
            Rank1OvalDx2 = 1402080;

            Rank1LabelCol1 = 3;
            Rank1LabelDx1 = 1506855;
            Rank1LabelCol2 = 3;
            Rank1LabelDx2 = 1679979;
        }
        public LegendPostionIndividual(bool sigPresent = false)
        {
            if (!sigPresent)
            {
                RankingMarkingLegendCol1 = 3;
                RankingMarkingLegendDx1 = 2268855;
                RankingMarkingLegendCol2 = 4;
                RankingMarkingLegendDx2 = 621030;

                Rank1OvalCol1 = 4;
                Rank1OvalDx1 = 68580;
                Rank1OvalCol2 = 4;
                Rank1OvalDx2 = 125730;

                Rank1LabelCol1 = 4;
                Rank1LabelDx1 = 230505;
                Rank1LabelCol2 = 4;
                Rank1LabelDx2 = 403629;
            }
            else
            {
                RankingMarkingLegendCol1 = 3;
                RankingMarkingLegendDx1 = 1144905;
                RankingMarkingLegendCol2 = 3;
                RankingMarkingLegendDx2 = 1840230;

                Rank1OvalCol1 = 3;
                Rank1OvalDx1 = 1287780;
                Rank1OvalCol2 = 3;
                Rank1OvalDx2 = 1344930;

                Rank1LabelCol1 = 3;
                Rank1LabelDx1 = 1449705;
                Rank1LabelCol2 = 3;
                Rank1LabelDx2 = 1622829;
            }
        }
    }
}
