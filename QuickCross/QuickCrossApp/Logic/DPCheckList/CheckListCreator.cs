using System;
using Microsoft.VisualBasic;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.Tabulation;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Common.Constants;
using XlFileFormat = Macromill.QCWeb.Common.XlFileFormat;

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using VBA = Microsoft.VisualBasic;
using Tables = Macromill.QCWeb.Batch.Report.Tables;
using log4net;
using System.Reflection;
using static Qc4Launcher.Logic.DPCheckList.DPCheckListTabulationQc;
using System.IO;
using Macromill.QCWeb.Common;
using System.ComponentModel;

namespace Qc4Launcher.Logic.DPCheckList
{
    public class CheckListCreator
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //string VB_Name = "CheckListCreator";
        //bool VB_GlobalNameSpace = false;
        //bool VB_Creatable = false;
        //bool VB_PredeclaredId = true;
        //bool VB_Exposed = true;

        private const string TEMPLATE_NAME = "CheckList.xlt";
        public const string TEMPLATE_NAME_GT = "Comparison_GT.xlt";
        private OutputCheckList CurrentOutput;
        // Private BookIndex As Long

        //New Implemetation
        private VBA.Collection NewBooks;
        private static string TemplateDirectoryPath;
        private Application xlApp;
        private static Workbook WorkingBook;
        private static string BookPSWD;
        private static string SheetPSWD;
        private static ExecuteStaticMethod ExecuteStaticMethod = new ExecuteStaticMethod();
        //private static string OutputDirectoryPath;
        //VBA.Collection OutputBooksPathCol = new Collection();
        private static string lccd;

        CrossCreator CrossCreator = new CrossCreator();

        // チェックリストを生成するサブルーチン
        internal void CreateCheckList(OutputCheckList Output, string bookPSWD, string sheetPSWD, string templateDirectoryPath, string Lccd, Application excelApp, string[] tableKeys,
            OnWorkerMethodCompleteDelegate OnWorkerComplete, ref double currentProgress, double allocatedProgress,object worker
            )
        {
            BackgroundWorker bgWorker = worker as BackgroundWorker;
            BookPSWD = bookPSWD;
            SheetPSWD = sheetPSWD;
            TemplateDirectoryPath = templateDirectoryPath;
            xlApp = excelApp;

            //OutputDirectoryPath = outputDirectoryPath;
            lccd = Lccd;

            string TempPath;
            //Workbook TempBook;
            Worksheet tmpOriginalItemsSheet;
            Worksheet tmpNewItemsSheet;
            long preProcessCount = 0;
            long afterProcessCount = 0;
            Worksheet OriginalItemsSheet = null;
            Worksheet NewItemsSheet = null;
            Worksheet FormatSheet;
            long originalItemsSheetIndex;
            long newItemsSheetIndex = 0;
            XlFileFormat fmt;
            string ReportTitle;
            CheckListTable tmpCheckListTable;
            Range originalitemsOutputCell = null;
            Range newitemsOutputCell = null;
            Range HeaderFormatRange;
            Range FormatRange;
            long idx;
            long changedIdx = 0;
            long newItemsCnt = 0;
            long changedFlag;
            //Excel.Workbook NewBook;
            //string Prefix;
            string ext;
            //string n;
            //string p;
            //long i;
            //long j;
            string header;
            Excel.Worksheet sh;
            //MethodResult res;
            string OrgProcName;
            //ErrorStruct[] Errors;
            long ErrorsCount;
            //OrgProcName = RunningProcName;
            //RunningProcName = "CheckListCreator.CreateCheckList";
            Init();
            CurrentOutput = Output;
            //On Error GoTo ErrHdl
            if (Output.Tables.Count == 0)
            {
            }
            fmt = Output.ParentRequest.ExcelFileFormat;
            if (fmt != XlFileFormat.xlExcel8)
                fmt = XlFileFormat.xlOpenXMLWorkbook;
            TempPath = TemplatePath(fmt);
            if (Strings.Len(FileSystem.Dir(TempPath)) == 0)
            {
            }
            //FileSystem.Dir();
            //On Error GoTo ErrHdl
            ext = fmt == XlFileFormat.xlExcel8 ? "xls" : "xlsx";
            // 調査タイトル
            ReportTitle = Output.ParentRequest.Title;

            Workbooks wbs = xlApp.Workbooks;
            WorkingBook = wbs.Add(TempPath);
            foreach (Excel.Worksheet sht in WorkingBook.Worksheets)
                sht.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            //WorkingBook.Application.ScreenUpdating = false;
            //WorkingBook.Application.PrintCommunication = false;
            // TempBook.Unprotect ThisWorkbook.TemplateBookPassword
            {
                var withBlock = WorkingBook.Worksheets;
                tmpOriginalItemsSheet = withBlock["OriginalItems"];
                tmpNewItemsSheet = withBlock["NewItems"];
                FormatSheet = withBlock["Format"];
            }
            HeaderFormatRange = FormatSheet.Range["GlobalHeader"];
            // ヘッダ設定
            tmpOriginalItemsSheet.Unprotect(SheetPSWD);
            tmpNewItemsSheet.Unprotect(SheetPSWD);
            // header = "&9" & IIf(ReportTitle Like "[0-9]*", " ", "") & ReportTitle
            header = OutputUtil.GetAdjustedHeader(ReportTitle);
            tmpOriginalItemsSheet.PageSetup.CenterHeader = header;
            tmpNewItemsSheet.PageSetup.CenterHeader = header;
            originalItemsSheetIndex = 1;
            CreateNewBook(tmpOriginalItemsSheet, ref OriginalItemsSheet, ref originalitemsOutputCell);
            idx = 0;
            //foreach (string key in Output.Tables.Keys)

            #region Progress Bar Implementation
            double childProgress = 0; double UpdProgress = 0;
            #endregion


            foreach (string key in tableKeys)
            {
                #region Progress Bar Implementation
                double progressChildPerc = (double)(idx + 1) / tableKeys.Length * 100;
                childProgress = allocatedProgress * progressChildPerc / 100;
                UpdProgress = currentProgress + childProgress;
                if (bgWorker.CancellationPending)
                {
                    currentProgress = Convert.ToInt32(UpdProgress);
                    return;
                }
                OnWorkerComplete(Convert.ToInt32(UpdProgress), String.Format(LocalResource.PB_CHECK_LIST_TABLE, (idx + 1), tableKeys.Length));
                #endregion

                tmpCheckListTable = (CheckListTable)Output.Tables[key];
                if (tmpCheckListTable.IsNewItem)
                {
                    if (afterProcessCount == 0)
                    {
                        {
                            var withBlock = tmpCheckListTable;
                            afterProcessCount = Convert.ToInt64(withBlock.TableValue(withBlock.GetTableValueRowIndexMinimum + 1, withBlock.GetTableValueColumnIndexMinimum, true))
                                             + Convert.ToInt64(withBlock.TableValue(withBlock.GetTableValueRowIndexMinimum + 1, withBlock.GetTableValueColumnIndexMinimum + 2, true));
                        }
                        // tmpNewItemsSheet.Range("AfterProcessSamplesCount").Value = afterProcessCount
                        object afterProcessCountStr = Convert.ToString(afterProcessCount);
                        OutputUtil.PutValue(tmpNewItemsSheet.Range["AfterProcessSamplesCount"], ref afterProcessCountStr);
                        if (!(NewItemsSheet == null))
                            // NewItemsSheet.Range("AfterProcessSamplesCount").Value = afterProcessCount
                            OutputUtil.PutValue(NewItemsSheet.Range["AfterProcessSamplesCount"], ref afterProcessCountStr);
                    }
                    newItemsCnt = newItemsCnt + 1;
                    changedFlag = newItemsCnt;
                    if ((tmpCheckListTable.Question.QuestionType & QuestionType.N) == QuestionType.N)
                        FormatRange = FormatSheet.Range["New_N"];
                    else
                        FormatRange = FormatSheet.Range["New_SA_MA"];
                    OutputTable(tmpCheckListTable, HeaderFormatRange, FormatRange, tmpNewItemsSheet, ref NewItemsSheet, ref newItemsSheetIndex, ref newitemsOutputCell, changedFlag, true);
                }
                else
                {
                    if (preProcessCount == 0)
                    {
                        {
                            var withBlock = tmpCheckListTable;
                            preProcessCount = Convert.ToInt64(withBlock.TableValue(withBlock.GetTableValueRowIndexMinimum + 1, withBlock.GetTableValueColumnIndexMinimum, true))
                                           + System.Convert.ToInt64(withBlock.TableValue(withBlock.GetTableValueRowIndexMinimum + 1, withBlock.GetTableValueColumnIndexMinimum + 2, true));
                            afterProcessCount = System.Convert.ToInt64(withBlock.TableValue(withBlock.GetTableValueRowIndexMinimum + 2, withBlock.GetTableValueColumnIndexMinimum, true))
                                              + System.Convert.ToInt64(withBlock.TableValue(withBlock.GetTableValueRowIndexMinimum + 2, withBlock.GetTableValueColumnIndexMinimum + 2, true));
                        }
                        // tmpOriginalItemsSheet.Range("BeforeProcessSamplesCount").Value = preProcessCount
                        object preProcessCountStr = preProcessCount;
                        OutputUtil.PutValue(tmpOriginalItemsSheet.Range["BeforeProcessSamplesCount"], ref preProcessCountStr);
                        // tmpOriginalItemsSheet.Range("AfterProcessSamplesCount").Value = afterProcessCount
                        object afterProcessCountStr = afterProcessCount;
                        OutputUtil.PutValue(tmpOriginalItemsSheet.Range["AfterProcessSamplesCount"], ref afterProcessCountStr);
                        // tmpNewItemsSheet.Range("AfterProcessSamplesCount").Value = afterProcessCount
                        OutputUtil.PutValue(tmpNewItemsSheet.Range["AfterProcessSamplesCount"], ref afterProcessCountStr);
                        if (!(OriginalItemsSheet == null))
                        {
                            // OriginalItemsSheet.Range("BeforeProcessSamplesCount").Value = preProcessCount
                            // OriginalItemsSheet.Range("AfterProcessSamplesCount").Value = afterProcessCount
                            OutputUtil.PutValue(OriginalItemsSheet.Range["BeforeProcessSamplesCount"], ref preProcessCountStr);
                            OutputUtil.PutValue(OriginalItemsSheet.Range["AfterProcessSamplesCount"], ref afterProcessCountStr);
                        }
                        if (!(NewItemsSheet == null))
                            // NewItemsSheet.Range("AfterProcessSamplesCount").Value = afterProcessCount
                            OutputUtil.PutValue(NewItemsSheet.Range["AfterProcessSamplesCount"], ref afterProcessCountStr);
                    }
                    if (tmpCheckListTable.IsChanged)
                    {
                        changedIdx = changedIdx + 1;
                        changedFlag = changedIdx;
                    }
                    else
                        changedFlag = 0;
                    if ((tmpCheckListTable.Question.QuestionType & QuestionType.N) == QuestionType.N)
                        FormatRange = FormatSheet.Range["Original_N"];
                    else
                        FormatRange = FormatSheet.Range["Original_SA_MA"];
                    OutputTable(tmpCheckListTable, HeaderFormatRange, FormatRange, tmpOriginalItemsSheet, ref OriginalItemsSheet, ref originalItemsSheetIndex, ref originalitemsOutputCell, changedFlag);
                }
                //DoEvents();
                idx++;
            }

            try
            {
                WorkingBook.Close(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
            }

            // 保存
            //WorkingBook.Application.PrintCommunication = true;

            try
            {
                Range filterRng;
                Range newFilterRng;
                for (int i = 1; i <= NewBooks.Count; i++)
                {
                    Workbook NewBook = (Workbook)NewBooks[i];
                    // オートフィルタの再設定
                    foreach (Worksheet sheet in NewBook.Worksheets)
                    {
                        if (sheet.AutoFilterMode)
                        {
                            filterRng = sheet.AutoFilter.Range;
                            {
                                var withBlock = filterRng;
                                {
                                    var withBlock1 = xlApp.Intersect(withBlock.EntireColumn, withBlock.Worksheet.UsedRange);
                                    newFilterRng = withBlock1.Worksheet.Range[filterRng, withBlock1.Item[withBlock1.Count, 1]];
                                }
                            }
                            if (filterRng.Address != newFilterRng.Address)
                            {
                                sheet.AutoFilterMode = false;
                                newFilterRng.AutoFilter(1, Missing.Value, XlAutoFilterOperator.xlOr, Missing.Value, true);
                                //newFilterRng.AutoFilter();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message + "\n" + e.StackTrace);
            }

        ExitProc:;

            NewBooks = null;

        ErrHdl:
            ;

        }

        private void Init()
        {
            CurrentOutput = null;
            NewBooks = new VBA.Collection();
        }

        // テンプレートのパスを返すプロパティ

        private string TemplatePath(XlFileFormat FileFormat = XlFileFormat.xlOpenXMLWorkbook, bool gt = false)
        {
            string TemplatePath = null;
            string d;
            string n;
            if (FileFormat != XlFileFormat.xlExcel8) FileFormat = XlFileFormat.xlOpenXMLWorkbook;
            if (gt)
            {
                n = TEMPLATE_NAME_GT;
            }
            else
            {
                n = TEMPLATE_NAME;
            }
            if (FileFormat == XlFileFormat.xlOpenXMLWorkbook) n = n + "x";
            d = OutputUtil.GetTemplateDirectoryPath(TemplateDirectoryPath, xlApp.PathSeparator);
            TemplatePath = OutputUtil.BuildPath(d, n, xlApp.PathSeparator);

            if (gt)
            {
                string outPath = Path.Combine(Path.GetTempPath(), "QC4", "CheckListOutput");
                GlobalMethodClass.GuaranteeDirectoryExist(outPath);
                string p = "";
                n = LocalResource.REPORT_CHECK_LIST_BOOK_NAME_PREFIX + ".xltx";
                p = OutputUtil.BuildPath(outPath, n, xlApp.PathSeparator);
                try
                {
                    if (!File.Exists(p))
                        File.Copy(TemplatePath, p);
                }
                catch { }
                TemplatePath = p;
            }

            return TemplatePath;
        }

        private void CreateNewBook(Excel.Worksheet TempSheet
      , ref Excel.Worksheet OutputSheet, ref Excel.Range OutputCell
      , bool isNew = false)
        {
            XlFileFormat fmt = XlFileFormat.xlOpenXMLWorkbook;
            string TempPath = TemplatePath(fmt, true);
            Excel.Workbook NewBook;
            Workbooks wbs = xlApp.Workbooks;
            NewBook = wbs.Add(TempPath);
            NewBooks.Add(NewBook);
            OutputSheet = NewBook.Worksheets[1];
            OutputSheet.Name = isNew ? LocalResource.REPORT_CHECK_LIST_NEW_ITEMS_SHEET_NAME : LocalResource.REPORT_CHECK_LIST_ORIGINAL_ITEMS_SHEET_NAME;
            OutputCell = OutputSheet.Range["OutputStart"];
        }

        private void OutputTable(CheckListTable Table
       , Excel.Range HeaderFormatRange, Excel.Range FormatRange
       , Excel.Worksheet TempSheet
       , ref Excel.Worksheet OutputSheet, ref long SheetIndex, ref Excel.Range OutputCell
       , long changedFlag, bool isNew = false)
        {
            long MaxColumnsCount;
            string qType;
            long SectorsCount = 0;
            long n;
            long RowsCount;
            long clmsCount;
            Excel.Range tableStartRow;
            Array v;
            long r;
            long c;
            long i;
            long x = 0;
            long y;
            long xStart = 0;
            long xEnd;
            Tables tmpTables;
            string prompt;
            string tmpBuf;
            string OrgProcName;

            string[,] tmpTableValue;

            if ((Table.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                goto ExitProc;
            MaxColumnsCount = System.Convert.ToInt64(LocalResource.REPORT_MAX_COLUMNS_COUNT_PER_SHEET_KEYWORD) + 3;
            switch (Table.Question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N))
            {

                case QuestionType.SA:
                    {
                        qType = "SA";
                        break;
                    }

                case QuestionType.MA:
                    {
                        qType = "MA";
                        break;
                    }

                case QuestionType.N:
                    {
                        qType = "N";
                        break;
                    }

                default:
                    {
                        goto ExitProc;
                        break;
                    }
            }
            {
                var withBlock = HeaderFormatRange;
                r = withBlock.Rows.Count; // ヘッダ部の行数
                {
                    var withBlock1 = Table;
                    clmsCount = withBlock1.GetTableValueColumnIndexMaximum - withBlock1.GetTableValueColumnIndexMinimum + 1;
                }
                // 全体/無回答/非該当を除いた列数(SA/MAでは選択肢数)
                SectorsCount = clmsCount - 3;  // 3列以下になるのはおかしい
                n = (SectorsCount - 1) / (MaxColumnsCount - 3) + 1;    // 折り返し表数
                                                                       // 表フォーマットの行数とTableValue配列の行数は一致しなければおかしい
                RowsCount = r + FormatRange.Rows.Count;
                if (n > 1)
                    RowsCount = RowsCount + (n - 1) * (2 + FormatRange.Rows.Count);

                if (OutputSheet == null || OutputCell.Row + RowsCount - 1 > OutputSheet.Rows.Count)
                {
                    CreateNewSheet(TempSheet, ref OutputSheet, ref OutputCell, ref SheetIndex, isNew);

                }

                //ReDim v(1 & To RowsCount, 1 & To IIf(n > 1 &, MaxColumnsCount, clmsCount))
                // HeaderFormatRangeの左端列は配列外

                v = Array.CreateInstance(typeof(object),
                new int[] { (int)RowsCount, n > 1 ? (int)MaxColumnsCount : (int)clmsCount },
                new int[] { 1, 1 });

                // ヘッダ部
                // .EntireRow.Copy OutputCell.EntireRow
                OutputUtil.CopyRow(withBlock.Rows, OutputCell);
                {
                    var withBlock1 = withBlock.Worksheet;
                    {
                        var withBlock2 = withBlock1.Range["QuestionID"];
                        // v(.Row - HeaderFormatRange.Row + 1&, .Column - HeaderFormatRange.Column) = Table.Question.Name
                        tmpBuf = Table.Question.Name;
                        OutputUtil.AddPrefix(ref tmpBuf, true);
                        v.SetValue(tmpBuf, withBlock2.Row - HeaderFormatRange.Row + 1, withBlock2.Column - HeaderFormatRange.Column);
                    }
                    {
                        var withBlock2 = withBlock1.Range["QuestionType"];
                        v.SetValue(qType, withBlock2.Row - HeaderFormatRange.Row + 1, withBlock2.Column - HeaderFormatRange.Column);
                    }
                    if ((Table.Question.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                    {
                        {
                            var withBlock2 = withBlock1.Range["SectorsCount"];
                            v.SetValue(Convert.ToString(SectorsCount), withBlock2.Row - HeaderFormatRange.Row + 1, withBlock2.Column - HeaderFormatRange.Column);
                        }
                    }
                    {
                        var withBlock2 = withBlock1.Range["QuestionDescription"];
                        // v(.Row - HeaderFormatRange.Row + 1&, .Column - HeaderFormatRange.Column) = Table.Question.Description
                        tmpBuf = Table.Question.Description;
                        OutputUtil.AddPrefix(ref tmpBuf, true);
                        v.SetValue(tmpBuf, withBlock2.Row - HeaderFormatRange.Row + 1, withBlock2.Column - HeaderFormatRange.Column);
                    }
                }
                // 表部
                for (i = 1; i <= n; i++)
                {
                    tableStartRow = OutputCell.EntireRow.Item[r + (i - 1) * (FormatRange.Rows.Count + 2) + 1];
                    tableStartRow = OutputCell.EntireRow.Item[r + 1];

                    // n番目の分割表の全体/無回答/非該当を除いた列数
                    clmsCount = i == n ? (SectorsCount - 1) % (MaxColumnsCount - 3) + 1 : MaxColumnsCount - 3;
                    // If clmsCount <= 0& Then Err().Raise vbObjectError + 100&, , "データが不正です"
                    if (clmsCount <= 0)
                        throw new Exception(LocalResource.REPORT_UNJUST_DATA_MESSAGE);
                    if (i == 1)
                    {
                        // FormatRange.EntireRow.Copy tableStartRow
                        OutputUtil.CopyRow(FormatRange, tableStartRow);
                        if (qType != "N")
                        {
                            // フォーマット調整
                            switch (clmsCount)
                            {
                                case 1:
                                    {
                                        tableStartRow.Range["E1"].Resize[FormatRange.Rows.Count].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                                        break;
                                    }

                                case 2:
                                    {
                                        break;
                                    }

                                default:
                                    {
                                        {
                                            var withBlock1 = tableStartRow.Range["E1"].Resize[FormatRange.Rows.Count];
                                            withBlock1.Copy();
                                            withBlock1.Resize[withBlock.Rows.Count, clmsCount - 2].Insert(XlInsertShiftDirection.xlShiftToRight);
                                            xlApp.CutCopyMode = (XlCutCopyMode)1;
                                        }

                                        break;
                                    }
                            }
                        }
                        xStart = Table.GetTableValueColumnIndexMinimum;
                        // 性能対策 start
                        tmpTableValue = Table.TableValueByMatrix(Table.GetTableValueRowIndexMinimum, Table.GetTableValueRowIndexMaximum, (int)xStart, (int)xStart + 2, true);
                        // 性能対策 end
                        for (y = Table.GetTableValueRowIndexMinimum; y <= Table.GetTableValueRowIndexMaximum; y++)
                        {
                            r = r + 1;
                            c = 0;
                            // ここでは、全体/無回答/非該当のみ投入
                            for (x = xStart; x <= xStart + 2; x++)
                            {
                                c = c + 1;
                                // v(r, c) = Table.TableValue(y, x, True)
                                // tmpBuf = Table.TableValue(y, x, True)
                                tmpBuf = Convert.ToString(tmpTableValue.GetValue(y, x));
                                if (tmpBuf == "∞" || tmpBuf == "-∞")
                                {
                                    tmpBuf = QC4Common.Common.Constants.ExcelDiv;
                                }
                                if (y == Table.GetTableValueRowIndexMinimum)
                                    OutputUtil.AddPrefix(ref tmpBuf, true);
                                v.SetValue(tmpBuf, r, c);
                            }
                        }
                        xStart = x;
                        r = withBlock.Rows.Count; // 後続処理のために元に戻す
                    }
                    else
                    {
                        // SA/MAの場合にしかここには来ない
                        if (clmsCount > 1)
                            FormatRange.EntireRow.Range["E1"].Resize[FormatRange.Rows.Count]
        .Copy(tableStartRow.Range["E1"].Resize[FormatRange.Rows.Count, clmsCount - 1]);
                        FormatRange.EntireRow.Range["F1"].Resize[FormatRange.Rows.Count]
                            .Copy(tableStartRow.Range["E1"].Item[1, clmsCount].Resize(FormatRange.Rows.Count));
                        tableStartRow.Range["E1"].Resize[FormatRange.Rows.Count].Borders.Item[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
                        tableStartRow.Range["E1"].Resize[FormatRange.Rows.Count].Borders.Item[XlBordersIndex.xlEdgeLeft].Color = Util.Constants.GT.GTColBorder.ToArgb();
                    }
                    // 全体/無回答/非該当以外を投入
                    if (i == n)
                        xEnd = Table.GetTableValueColumnIndexMaximum;
                    else
                        xEnd = xStart + MaxColumnsCount - 3 + (-1);
                    // 性能対策 start
                    tmpTableValue = Table.TableValueByMatrix(Table.GetTableValueRowIndexMinimum, Table.GetTableValueRowIndexMaximum, (int)xStart, (int)xEnd, true);
                    // 性能対策 end
                    for (y = Table.GetTableValueRowIndexMinimum; y <= Table.GetTableValueRowIndexMaximum; y++)
                    {
                        r = r + 1;
                        c = 3;
                        for (x = xStart; x <= xEnd; x++)
                        {
                            c = c + 1;
                            // v(r, c) = Table.TableValue(y, x, True)
                            // tmpBuf = Table.TableValue(y, x, True)
                            //tmpBuf = tmpTableValue(y, x);
                            tmpBuf = Convert.ToString(tmpTableValue.GetValue(y, x));
                            if (tmpBuf == "∞" || tmpBuf == "-∞")
                            {
                                tmpBuf = QC4Common.Common.Constants.ExcelDiv;
                            }
                            if (y == Table.GetTableValueRowIndexMinimum)
                                OutputUtil.AddPrefix(ref tmpBuf, true);
                            v.SetValue(tmpBuf, r, c);
                        }
                    }
                    xStart = x;
                    if (i < n)
                    {
                        r = r + 1;
                        v.SetValue(LocalResource.REPORT_TO_AFTER_TABLE_MARK_AT_TURN_KEYWORD, r, MaxColumnsCount);
                        r = r + 1;
                        v.SetValue(LocalResource.REPORT_FROM_BEFORE_TABLE_MARK_AT_TURN_KEYWORD, r, 1);
                    }
                }
                // OutputCell.Range("A2").Resize(UBound(v, 1) - 1&).Value = changedFlag
                string changedFlagStr = Convert.ToString(changedFlag).Trim() == "0" ? string.Empty : LocalResource.DP_PROCESSED;
                OutputUtil.PutValue(OutputCell.Range["A2"].Resize[Information.UBound(v, 1) - 1], ref changedFlagStr);
                // OutputCell.Range("B1").Resize(UBound(v, 1), UBound(v, 2)).Value = v
                // PutValue OutputCell.Range("B1").Resize(UBound(v, 1), UBound(v, 2)), v
                Array chArr = getChoiceArray(ref v, withBlock.Rows.Count + 1);
                OutputCell.Range["B1"].Resize[Information.UBound(v, 1), Information.UBound(v, 2)].Value = v;
                OutputCell.Range["B" + (withBlock.Rows.Count + 1)].Resize[1, Information.UBound(v, 2)].Value = chArr;
                tmpTables = Table.ParentCollection as Tables;
                if (OutputCell.Row + RowsCount + withBlock.Rows.Count <= OutputCell.Worksheet.Rows.Count)
                    OutputCell = OutputCell.Item[RowsCount + 1];
                else if (Table.Index < tmpTables.Count - 1)
                    CreateNewSheet(TempSheet, ref OutputSheet, ref OutputCell, ref SheetIndex, isNew);
                else
                    OutputCell = null;
            }

        ExitProc:;

        }

        private Array getChoiceArray(ref Array v, int chIdx)
        {
            Array c = Array.CreateInstance(typeof(string),
                new int[] { 1, v.GetUpperBound(1) - v.GetLowerBound(1) + 1 },
                new int[] { 1, 1 });

            for (int x = v.GetLowerBound(1); x <= v.GetUpperBound(1); x++)
            {
                string tmp = Convert.ToString(v.GetValue(chIdx, x));
                c.SetValue(tmp, 1, x);
                v.SetValue(null, chIdx, x);
            }

            return c;
        }

        private void CreateNewSheet(Excel.Worksheet TempSheet
      , ref Excel.Worksheet OutputSheet, ref Excel.Range OutputCell
      , ref long SheetIndex, bool isNew)
        {
            const long MAX_SHEETS_COUNT = 250;
            Excel.Workbook wb;
            double dblLog;
            double lngLog;
            string preFmt;
            string fmt;
            long i;
            ReportMessageIndex idx;
            string tmpPreName;
            string tmpNewName;
            string OrgProcName;
            if (OutputSheet == null)
                wb = (Workbook)(NewBooks[NewBooks.Count]);
            else
                wb = OutputSheet.Parent;

            if (wb.Sheets.Count >= MAX_SHEETS_COUNT)
            {
                SheetIndex = 1;
                CreateNewBook(TempSheet, ref OutputSheet, ref OutputCell, isNew);
                return;
            }
            SheetIndex = SheetIndex + 1;
            dblLog = Math.Log(Convert.ToDouble(SheetIndex)) / (double)Math.Log(10);
            lngLog = -Conversion.Int(-dblLog);  // 切り上げ
                                                //fmt = String(lngLog + 1, "0");
            fmt = new string('0', Convert.ToInt32(lngLog + 1));

            string word = isNew ? LocalResource.REPORT_CHECK_LIST_NEW_ITEMS_SHEET_NAME_WITH_INDEX_FORMAT : LocalResource.REPORT_CHECK_LIST_ORIGINAL_ITEMS_SHEET_NAME_WITH_INDEX_FORMAT;
            if (SheetIndex > 1 & dblLog == Convert.ToDouble(lngLog))
            {
                preFmt = new string('0', Convert.ToInt32(lngLog));
                {
                    var withBlock = wb.Worksheets;
                    for (i = 2; i <= SheetIndex - 1; i++)
                    {
                        tmpPreName = string.Format(word, Strings.Format(i, preFmt));
                        tmpNewName = string.Format(word, Strings.Format(i, fmt));
                        // .Item(TempSheet.Name & "_" & Format$(i, preFmt)).Name = TempSheet.Name & "_" & Format$(i, fmt)
                        withBlock.Item[tmpPreName].Name = tmpNewName;
                    }
                }
            }
            if (!(OutputSheet == null))
            {
                TempSheet.Copy(Type.Missing, OutputSheet);
                OutputSheet = OutputSheet.Next;
            }
            else
            {
                var withBlock = wb.Sheets;
                TempSheet.Copy(Type.Missing, withBlock.Item[withBlock.Count]);
                OutputSheet = withBlock.Item[withBlock.Count];
            }
            OutputSheet.Unprotect(SheetPSWD);
            if (SheetIndex == 1)
            {
                string tmpName = isNew ? LocalResource.REPORT_CHECK_LIST_NEW_ITEMS_SHEET_NAME
        : LocalResource.REPORT_CHECK_LIST_ORIGINAL_ITEMS_SHEET_NAME;
                tmpNewName = tmpName;
            }
            else
                tmpNewName = Convert.ToString(string.Format(word, Strings.Format(SheetIndex, fmt), lccd));
            // If SheetIndex > 1& Then
            OutputSheet.Name = tmpNewName;
            OutputCell = OutputSheet.Range["OutputStart"];
        }

    } // End Class

}
