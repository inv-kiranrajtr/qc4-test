using log4net;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;
using QC4Common.Model;
using Qc4Launcher.DB;
using Qc4Launcher.Model;
using Qc4Launcher.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Common.Constants;
using Constants = Microsoft.VisualBasic.Constants;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Logic
{
    class OutputUtil
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static int MAX_HPAGEBREAKS_COUNT = 1023;
        public static readonly Double HEADER_MAX_ROW_HEIGHT = 81;   // ' 108px
        public static Double DATA_MAX_ROW_HEIGHT = 36;     // ' 48px
        public static int LINE_COLOR = 0XBFBFBF;
        public static float LINE_WEIGHT = 0.75F;
        public static string BASE_TEMPLATE_NAME = "Base.xltx";
        public static double OVER_2007_CHART_AREA_LEFT = 3.75;
        public static double OVER_2007_CHART_AREA_TOP = 4;
        public static double MAX_LEGEND_WIDTH = 160;
        public static double CHART_LEFT = 100;
        public static double heightDef = 11.25;
        public static double widthDef = 9.33203125;
        public static double checkcrosswidthDef = 2;
        public static double widthDefWithoutMrgn = 8.5;
        public static double widthDefColDesc = 154;
        public static double charwidthDefAsci = 1.02;
        public static double charwidthDef = 2.0413;
        public static Dictionary<string, double> widthDefMap = new Dictionary<string, double>();
        public static Dictionary<string, double> widthDefAsciMap = new Dictionary<string, double>();


        //private static void AutoFitExMain(Range Rows, Worksheet WorkingSheet, bool AddOneRow, double MaxHeight)
        //{
        //    int tmp;
        //    Range upperRows;
        //    Range underRows;
        //    Range Row;
        //    Array rowHeights;
        //    int i;
        //    Array v = null;
        //    Array tmpBuf;
        //    int x, y;
        //    double h;
        //    if (Rows.MergeCells == null)
        //    {
        //        if (WorkingSheet == null)
        //        {
        //            //  WorkingSheet = GetWorkingBook.Worksheets.Item(1)
        //            WorkingSheet.UsedRange.Clear();
        //        }
        //        if (Rows.Count == 1)
        //        {
        //            //   AutoFitExSub .Rows, WorkingSheet, AddOneRow, MaxHeight
        //        }
        //        else
        //        {
        //            tmp = Rows.Count / 2;
        //            upperRows = Rows.Resize[tmp];
        //            underRows = Rows.Item[tmp + 1].Resize[Rows.Count - tmp];
        //            AutoFitExMain(upperRows, WorkingSheet, AddOneRow, MaxHeight);
        //            AutoFitExMain(underRows, WorkingSheet, AddOneRow, MaxHeight);
        //        }
        //    }
        //    else if (!Rows.MergeCells)
        //    {
        //        if (AddOneRow)
        //        {
        //            Range WithRowsCells = Rows.Cells;
        //            if (WithRowsCells.Count == 1)
        //            {
        //                v = Array.CreateInstance(typeof(object), new int[] { 1, 1 }, new int[] { 1, 1 });
        //                v.SetValue(WithRowsCells.Value, 1, 1);
        //            }
        //            else
        //            {
        //                v = WithRowsCells.Value;
        //            }
        //            tmpBuf = Array.CreateInstance(typeof(string), new int[] { v.GetUpperBound(0), v.GetUpperBound(1) }, new int[] { 1, 1 });
        //            for (y = 1; y <= v.GetUpperBound(0); y++)
        //            {
        //                for (x = 1; x <= v.GetUpperBound(1); x++)
        //                {
        //                    tmpBuf.SetValue(v.GetValue(y, x) + "\n", y, x);
        //                }
        //            }
        //            PutValue(WithRowsCells.Cells, ref tmpBuf);
        //        }
        //        rowHeights = Array.CreateInstance(typeof(double), new int[] { Rows.Count }, new int[] { 1 });
        //        for (i = 1; i <= Rows.Count; i++)
        //        {
        //            rowHeights.SetValue(Rows.Item[i].RowHeight, i);
        //        }
        //        Rows.AutoFit();
        //        for (i = 1; i <= Rows.Count; i++)
        //        {
        //            Row = Rows.Item[i];
        //            h = Row.RowHeight;
        //            if ((double)rowHeights.GetValue(i) > h) { h = (double)rowHeights.GetValue(i); }
        //            if (h > MaxHeight) { h = MaxHeight; }
        //            if (Row.RowHeight != h) { Row.RowHeight = h; }
        //        }
        //        if (AddOneRow) { PutValue(Rows.Cells, ref v); }
        //    }
        //    else
        //    {
        //        if (WorkingSheet == null)
        //        {
        //            // WorkingSheet = GetWorkingBook.Worksheets.Item(1)
        //            WorkingSheet.UsedRange.Clear();
        //        }
        //        foreach (Range RowI in Rows.Rows)
        //        {
        //            //   AutoFitExSub(RowI, WorkingSheet, AddOneRow, MaxHeight);
        //        }
        //    }
        //}

        // Basil Changes

        private static void AutoFitExMain(Excel.Range Rows, Excel.Worksheet WorkingSheet, bool AddOneRow, double MaxHeight, Application xlApp)
        {
            long tmp;
            Excel.Range upperRows;
            Excel.Range underRows;
            Excel.Range Row;
            Array rowHeights;
            long i;
            Array v = null;
            Array tmpBuf;
            long x;
            long y;
            double h;
            string OrgProcName;
            {
                var withBlock = Rows;
                if (withBlock.MergeCells == null || DBNull.Value.Equals(withBlock.MergeCells))
                {
                    // 性能対策 start
                    if (WorkingSheet == null)
                    {
                        WorkingSheet = xlApp.ActiveWorkbook.Worksheets.Item[1];
                    }
                    WorkingSheet.UsedRange.Delete();
                    // 性能対策 end
                    if (withBlock.Count == 1)
                        AutoFitExSub(withBlock.Rows, WorkingSheet, AddOneRow, MaxHeight);
                    else
                    {
                        tmp = withBlock.Count / 2;
                        upperRows = withBlock.Resize[tmp];
                        underRows = withBlock.Item[tmp + 1].Resize[withBlock.Count - tmp];
                        AutoFitExMain(upperRows, WorkingSheet, AddOneRow, MaxHeight, xlApp);
                        AutoFitExMain(underRows, WorkingSheet, AddOneRow, MaxHeight, xlApp);
                    }
                }
                else if (!withBlock.MergeCells)
                {
                    if (AddOneRow)
                    {
                        {
                            var withBlock1 = withBlock.Cells;
                            if (withBlock1.Count == 1)
                            {
                                v = Array.CreateInstance(typeof(object), // ReDim v(1 To 1, 1 To 1)
                                    new int[] { 1, 1 },
                                    new int[] { 1, 1 });
                                v.SetValue(withBlock1.Value, 1, 1);
                            }
                            else
                                v = withBlock1.Value;

                            tmpBuf = Array.CreateInstance(typeof(object),  //ReDim tmpBuf(1 To UBound(v, 1), 1 To UBound(v, 2))
                                  new int[] { Information.UBound(v, 1), Information.UBound(v, 2) },
                                  new int[] { 1, 1 });

                            for (y = 1; y <= Information.UBound(v, 1); y++)
                            {
                                for (x = 1; x <= Information.UBound(v, 2); x++)
                                    tmpBuf.SetValue(v.GetValue(y, x) + Constants.vbLf, y, x);
                            }
                            // .Value = tmpBuf
                            PutValue(withBlock1.Cells, ref tmpBuf);
                        }
                    }

                    rowHeights = Array.CreateInstance(typeof(object), //ReDim rowHeights(1 & To.Count)
                                  new int[] { withBlock.Count },
                                  new int[] { 1 });

                    for (i = 1; i <= withBlock.Count; i++)
                        rowHeights.SetValue(withBlock.Item[i].RowHeight, i);
                    withBlock.AutoFit();
                    for (i = 1; i <= withBlock.Count; i++)
                    {
                        Row = withBlock.Item[i];
                        h = Row.RowHeight;
                        if ((double)rowHeights.GetValue(i) > h)
                            h = (double)rowHeights.GetValue(i);
                        if (h > MaxHeight)
                            h = MaxHeight;
                        if (Row.RowHeight != h)
                            Row.RowHeight = h;
                    }
                    // If AddOneRow Then .Value = v
                    if (AddOneRow)
                        PutValue(withBlock.Cells, ref v);
                }
                else
                {
                    // 性能対策 start
                    if (WorkingSheet == null)
                    {
                        WorkingSheet = xlApp.ActiveWorkbook.Worksheets.Item[1];
                    }
                    WorkingSheet.UsedRange.Delete();
                    // 性能対策 end
                    foreach (Range Row1 in withBlock.Rows)
                        AutoFitExSub(Row1, WorkingSheet, AddOneRow, MaxHeight);
                }
            }
        }

        private static void AutoFitExSub(Excel.Range Row, Excel.Worksheet WorkingSheet, bool AddOneRow, double MaxHeight)
        {
            const double MAX_COLUMNWIDTH = 255;
            Excel.Range WorkCell;
            Array v;
            long i;
            long j;
            Excel.Range rng;
            double w;
            double h = 0;
            double tmpH;
            string OrgProcName;
            WorkCell = WorkingSheet.Range["A1"];
            {
                var withBlock = Row.Cells;
                if (withBlock.Count == 1)
                {
                    v = Array.CreateInstance(typeof(object), // ReDim v(1 To 1, 1 To 1)
                      new int[] { 1, 1 },
                      new int[] { 1, 1 });
                    v.SetValue(withBlock.Value, 1, 1);
                }
                else
                    v = withBlock.Value;
                for (i = 1; i <= Information.UBound(v, 2); i++)
                {
                    if (v.GetValue(1, i) != null)
                    {
                        rng = withBlock.Item[i].MergeArea;
                        {
                            var withBlock1 = rng;
                            if (withBlock1.Rows.Count == 1)
                            {
                                w = 0;
                                for (j = 1; j <= withBlock1.Columns.Count; j++)
                                    w = w + withBlock1.Columns.Item[j].ColumnWidth;
                                if (w <= MAX_COLUMNWIDTH)
                                {
                                    rng.Copy(WorkCell);
                                    {
                                        var withBlock2 = WorkCell;
                                        // If AddOneRow Then .Cells.Item(1).Value = .Cells.Item(1).Value & vbLf
                                        if (AddOneRow)
                                            PutValue(withBlock2.Cells.Item[1], withBlock2.Cells.Item[1].Value + Constants.vbLf);
                                        withBlock2.MergeArea.UnMerge();
                                        withBlock2.ColumnWidth = w;
                                        withBlock2.EntireRow.AutoFit();
                                        tmpH = withBlock2.EntireRow.RowHeight;
                                    }
                                    if (tmpH > h)
                                        h = tmpH;
                                }
                            }
                            else if (withBlock1.Rows.Count > 1) // #OutputFormatting - New implementation for row merge case
                            {
                                var newWithBlock = withBlock1.Rows.Item[1];
                                w = 0;
                                for (j = 1; j <= newWithBlock.Columns.Count; j++)
                                    w = w + newWithBlock.Columns.Item[j].ColumnWidth;
                                if (w <= MAX_COLUMNWIDTH)
                                {
                                    rng.Copy(WorkCell);
                                    {
                                        var withBlock2 = WorkCell;
                                        if (AddOneRow) PutValue(withBlock2.Cells.Item[1], withBlock2.Cells.Item[1].Value + Constants.vbLf);
                                        withBlock2.MergeArea.UnMerge();
                                        withBlock2.ColumnWidth = w;
                                        withBlock2.EntireRow.AutoFit();
                                        tmpH = withBlock2.EntireRow.RowHeight;
                                    }
                                    if (tmpH > h)
                                        h = tmpH;
                                }
                            }

                            i = i + withBlock1.Columns.Count - 1;
                        }
                    }
                }
            }
            if (h > MaxHeight)
                h = MaxHeight;
            if (h > Row.RowHeight)
                Row.RowHeight = h;
        }

        private static void AutoFitExCorssSub(Excel.Range column, Excel.Range columnP, Excel.Worksheet WorkingSheet, double MaxHeight)
        {
            Excel.Range WorkCell;
            double h = 0;
            double w = 0;
            double tmpH;
            Array v;
            Array u;
            Range first = column.Range["A1"];
            bool bold = first.Font.Bold;
            double fontSize = first.Font.Size;
            WorkCell = WorkingSheet.Range["A1"];
            {
                int cnt = column.Rows.Count;
                if (cnt == 1)
                {
                    v = Array.CreateInstance(typeof(object), // ReDim v(1 To 1, 1 To 1)
                      new int[] { 1, 1 },
                      new int[] { 1, 1 });
                    v.SetValue(column.Rows.Value, 1, 1);
                    u = Array.CreateInstance(typeof(object), // ReDim v(1 To 1, 1 To 1)
                      new int[] { 1, 1 },
                      new int[] { 1, 1 });
                    u.SetValue(columnP.Rows.Value, 1, 1);
                }
                else
                {
                    v = column.Rows.Value;
                    u = columnP.Rows.Value;
                }
                for (int r = 1; r <= cnt;)
                {
                    Range withBlockRow = column.Rows.Item[r];
                    Range withBlock = withBlockRow.MergeArea;
                    h = withBlockRow.RowHeight * withBlock.Rows.Count;
                    w = 0;
                    for (int j = 1; j <= withBlock.Columns.Count; j++)
                        w = w + withBlock.Columns.Item[j].ColumnWidth;
                    if (withBlock.Rows.Count > 1 || withBlock.Columns.Count > 1)
                    {
                        string val = null;
                        if (withBlock.Columns.Count == 1 && v.GetValue(r, 1) != null)
                        {
                            val = Convert.ToString(v.GetValue(r, 1));
                        }
                        else if (withBlock.Columns.Count == 2 && u.GetValue(r, 1) != null)
                        {
                            val = Convert.ToString(u.GetValue(r, 1));
                        }
                        if (val != null)
                        {
                            double lines = findTotalWidth(val, WorkingSheet, w, bold, fontSize);
                            tmpH = (lines) * heightDef;
                            if (tmpH > h)
                                h = tmpH;
                        }
                        //withBlock.Copy(WorkCell);
                        //var withBlock2 = WorkCell;
                        //withBlock2.MergeArea.UnMerge();
                        //withBlock2.ColumnWidth = w;
                        //withBlock2.EntireRow.AutoFit();
                        //tmpH = withBlock2.EntireRow.RowHeight;
                        //if (tmpH > h)
                        //   h = tmpH;
                    }
                    else
                    {
                        withBlock.EntireRow.AutoFit();
                        tmpH = withBlock.EntireRow.RowHeight;
                        if (tmpH > h)
                            h = tmpH;
                    }
                    //withBlockRow.RowHeight = h;
                    r += withBlock.Rows.Count;
                    if (h > MaxHeight)
                        h = MaxHeight;
                    double hI = h / withBlock.Rows.Count;
                    withBlock.RowHeight = hI;
                }

            }
        }

        internal static ColumnImportSettings NewCopy(ColumnImportSettings importSettings)
        {
            ColumnImportSettings settings = new ColumnImportSettings()
            {
                AfterProcessingData = importSettings.AfterProcessingData,
                BeforeProcessingData = importSettings.BeforeProcessingData,
                DataCount = importSettings.DataCount,
                DestinationFileKey1 = importSettings.DestinationFileKey1,
                DestinationFileKey2 = importSettings.DestinationFileKey2,
                ImportInformations = importSettings.ImportInformations,
                IsDataProcessed = importSettings.IsDataProcessed,
                MAformat = importSettings.MAformat,
                NotApplicable = importSettings.NotApplicable,
                NotApplicableCharacter = importSettings.NotApplicableCharacter,
                SelectedColumn = importSettings.SelectedColumn,
                SelectedIndex = importSettings.SelectedIndex,
                SourceFileKey1 = importSettings.SourceFileKey1,
                SourceFileKey2 = importSettings.SourceFileKey2
            };
            return settings;
        }

        private static double findTotalWidth(string val, Worksheet WorkingSheet, double colWidth, bool bold, double fontSize)
        {
            string key = Convert.ToString(bold) + ":" + Convert.ToString(fontSize);
            double widthDef;
            double widthDefAsci;
            if (!widthDefMap.ContainsKey(key))
            {
                WorkingSheet.UsedRange.Delete();
                Range WorkCell = WorkingSheet.Range["A1:B1"];
                Array test = Array.CreateInstance(typeof(string), // ReDim v(1 To 1, 1 To 1)
                       new int[] { 1, 2 },
                       new int[] { 1, 1 });
                test.SetValue("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", 1, 1);
                test.SetValue("ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー", 1, 2);
                Font WorkCellFont = WorkCell.Font;
                try
                {
                    WorkCellFont.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                    WorkCellFont.Bold = bold;
                    WorkCellFont.Size = fontSize;
                }
                catch (Exception ex) { };// to do                 
                WorkCell.Value2 = test;
                WorkCell.Columns.Item[1].Autofit();
                WorkCell.Columns.Item[2].Autofit();
                widthDefAsci = WorkCell.Columns.Item[1].ColumnWidth / 100;
                widthDef = WorkCell.Columns.Item[2].ColumnWidth / 100;
                widthDefMap[key] = widthDef;
                widthDefAsciMap[key] = widthDefAsci;
            }
            else
            {
                widthDef = widthDefMap[key];
                widthDefAsci = widthDefAsciMap[key];
            }
            const int MaxAnsiCode = 255;
            double width = 0;
            val = Regex.Replace(val, @"\n|\r|\r\n", "\r");
            val = Regex.Replace(val, @"\t", "");
            double lines = 0;
            double extrLineForSpace = 0;
            char prevChar = ' ';
            double line;
            foreach (char c in val)
            {
                if (c == '\r')
                {
                    line = Math.Ceiling(width / colWidth);
                    if (line > 1 && extrLineForSpace > 0)
                    {
                        line++;
                    }
                    lines += line;
                    width = 0;
                    extrLineForSpace = 0;
                }
                else if (c == ' ' && prevChar == '\r')
                {
                    extrLineForSpace++;
                }
                else
                {
                    if (c > MaxAnsiCode) { width += widthDef; } else { width += widthDefAsci; }
                }
                prevChar = c;
            }
            line = Math.Ceiling(width / colWidth);
            if (line > 1 && extrLineForSpace > 0)
            {
                line++;
            }
            lines += line;
            return lines;
        }

        public static void AutoFitEx(Excel.Range Range, Application xlapp, Excel.Worksheet WorkingSheet = null, double maxHeight = 0, bool iscross = false)
        {
            bool AddOneRow = false;
            Excel.Range Rows;
            if (Range == null)
                goto ExitProc;
            if (Range.Worksheet.ProtectContents)
                Information.Err().Raise(1004, Description: "保護されているシートの行高は変更できません。");
            Rows = xlapp.Intersect(Range, Range).Rows;
            try
            {
                if (maxHeight == 0)
                {
                    maxHeight = HEADER_MAX_ROW_HEIGHT;
                }
                AutoFitExMain(Rows, WorkingSheet, AddOneRow, maxHeight, xlapp);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
            }
        ExitProc:
            ;
        }


        internal static void AutoFitExCrossLabel(Range labelRange, Application xlApp, Worksheet workingSheet, double maxHeight)
        {
            int cnt = labelRange.Columns.Count;
            for (int i = cnt; i > 0; i--)
            {
                Range column = labelRange.Columns.Item[i];
                Range columnP = null;
                if (i - 1 > 0)
                {
                    columnP = labelRange.Columns.Item[i - 1];
                }
                AutoFitExCorssSub(column, columnP, workingSheet, maxHeight);
                break;
            }
        }


        //public static void AutoFitEx(Range Range, Application xlapp)
        //{
        //    AutoFitEx(Range, false, HEADER_MAX_ROW_HEIGHT, xlapp);
        //}

        //public static void AutoFitEx(Range Range, Boolean AddOneRow, Application xlapp)
        //{
        //    AutoFitEx(Range, AddOneRow, HEADER_MAX_ROW_HEIGHT, xlapp);
        //}

        //public static void AutoFitEx(Range Range, Boolean AddOneRow, double MaxHeight, Application xlapp)
        //{
        //    Range Rows;
        //    Worksheet WorkingSheet = null;
        //    if (Range == null) return;
        //    if (Range.Worksheet.ProtectContents) return;// "保護されているシートの行高は変更できません。"
        //    Rows = xlapp.Intersect(Range, Range).Rows;
        //    AutoFitExMain(Rows, WorkingSheet, AddOneRow, MaxHeight);

        //}

        public static void ConvertAndPutValue(Range Range, Array arrValue, Range RangeData, Array arrData)
        {
            int u = 0;
            int i, j;
            int d = 0;
            string tmp;
            Type VarType = arrValue.GetType().GetElementType();
            if (Range == null) { return; }
            //VarType = VarType.GetElementType();
            Array tmpArray = (Array)arrValue;


            Array tmpArrayStr = Array.CreateInstance(typeof(object),
    new int[] { arrValue.GetUpperBound(0) - arrValue.GetLowerBound(0) + 1, arrValue.GetUpperBound(1) - arrValue.GetLowerBound(1) + 1 },
    new int[] { arrValue.GetLowerBound(0), arrValue.GetLowerBound(1) });
            Array tmpArrayObj = Array.CreateInstance(typeof(object),
    new int[] { arrData.GetUpperBound(0) - arrData.GetLowerBound(0) + 1, arrData.GetUpperBound(1) - arrData.GetLowerBound(1) + 1 },
    new int[] { arrData.GetLowerBound(0), arrData.GetLowerBound(1) });

            switch (tmpArray.Rank)
            {
                case 1:
                case 2:
                    u = tmpArray.GetUpperBound(tmpArray.Rank - 1);
                    d = tmpArray.Rank;
                    break;
                default:
                    return;
            }
            if (u < tmpArray.GetLowerBound(tmpArray.Rank - 1)) { return; }
            if (VarType == typeof(object))
            {
                for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++)
                {
                    for (j = tmpArray.GetLowerBound(1); j <= tmpArray.GetUpperBound(1); j++)
                    {
                        if (j < arrData.GetLowerBound(1) || i < arrData.GetLowerBound(0))
                        {
                            if (tmpArray.GetValue(i, j) == null)
                            {
                                tmpArrayStr.SetValue(string.Empty, i, j);
                            }
                            else if (tmpArray.GetValue(i, j).GetType() == typeof(string))
                            {
                                tmp = Convert.ToString(tmpArray.GetValue(i, j));
                                if (tmp == null || tmp.Length == 0)
                                {
                                    tmpArrayStr.SetValue(string.Empty, i, j);
                                }
                                else
                                {
                                    tmpArrayStr.SetValue(tmp, i, j);
                                }
                            }
                            else if (tmpArray.GetValue(i, j).GetType() == typeof(double))
                            {
                                double tmp2 = (double)tmpArray.GetValue(i, j);
                                if (double.IsInfinity(tmp2))
                                {
                                    tmpArrayStr.SetValue(QC4Common.Common.Constants.ExcelDiv, i, j);
                                }
                                else
                                {
                                    tmpArrayStr.SetValue(tmpArray.GetValue(i, j), i, j);
                                }
                            }
                        }
                        else
                        {
                            if (tmpArray.GetValue(i, j) != null && tmpArray.GetValue(i, j).GetType() == typeof(double))
                            {
                                double tmp2 = (double)tmpArray.GetValue(i, j);
                                if (double.IsInfinity(tmp2))
                                {
                                    tmpArrayObj.SetValue(QC4Common.Common.Constants.ExcelDiv, i, j);
                                }
                                else
                                {
                                    tmpArrayObj.SetValue(tmpArray.GetValue(i, j), i, j);
                                }
                            }
                            else
                            {
                                tmpArrayObj.SetValue(tmpArray.GetValue(i, j), i, j);
                            }
                        }
                    }
                }
            }
            Range.Value = tmpArrayStr;
            RangeData.Value = tmpArrayObj;
        }

        public static void PutValue(Range Range, ref Array arrValue, bool NotRevise = false)
        {
            int u = 0;
            int i, j;
            int d = 0;
            string tmp;
            Type VarType = arrValue.GetType().GetElementType();
            if (Range == null) { return; }
            //VarType = VarType.GetElementType();
            Array tmpArray = (Array)arrValue;
            switch (tmpArray.Rank)
            {
                case 1:
                case 2:
                    u = tmpArray.GetUpperBound(tmpArray.Rank - 1);
                    d = tmpArray.Rank;
                    break;
                default:
                    return;
            }
            if (u < tmpArray.GetLowerBound(tmpArray.Rank - 1)) { return; }
            if (VarType == typeof(object))
            {
                if (NotRevise)
                {
                    switch (d)
                    {
                        case 1:
                            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++)
                            {
                                if (tmpArray.GetValue(i) == null)
                                {
                                    tmpArray.SetValue(string.Empty, i);
                                }
                                else if (tmpArray.GetValue(i).GetType() == typeof(string))
                                {
                                    tmp = (string)tmpArray.GetValue(i);
                                    if (tmp == null || tmp.Length == 0)
                                    {
                                        tmpArray.SetValue(string.Empty, i);
                                    }
                                    else
                                    {
                                        AddPrefix(ref tmp);
                                        tmpArray.SetValue(tmp, i);
                                    }
                                }
                                else if (tmpArray.GetValue(i).GetType() == typeof(double))
                                {
                                    double tmp2 = (double)tmpArray.GetValue(i);
                                    if (double.IsInfinity(tmp2))
                                    {
                                        tmpArray.SetValue(QC4Common.Common.Constants.ExcelDiv, i);
                                    }
                                }
                            }
                            break;
                        case 2:
                            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++)
                            {
                                for (j = tmpArray.GetLowerBound(1); j <= tmpArray.GetUpperBound(1); j++)
                                {
                                    if (tmpArray.GetValue(i, j) == null)
                                    {
                                        tmpArray.SetValue(string.Empty, i, j);
                                    }
                                    else if (tmpArray.GetValue(i, j).GetType() == typeof(string))
                                    {
                                        tmp = (string)tmpArray.GetValue(i, j);
                                        if (tmp == null || tmp.Length == 0)
                                        {
                                            tmpArray.SetValue(string.Empty, i, j);
                                        }
                                        else
                                        {
                                            AddPrefix(ref tmp);
                                            tmpArray.SetValue(tmp, i, j);
                                        }
                                    }
                                    else if (tmpArray.GetValue(i, j).GetType() == typeof(double))
                                    {
                                        double tmp2 = (double)tmpArray.GetValue(i, j);
                                        if (double.IsInfinity(tmp2))
                                        {
                                            tmpArray.SetValue(QC4Common.Common.Constants.ExcelDiv, i, j);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                {
                    switch (d)
                    {
                        case 1:
                            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++)
                            {
                                if (tmpArray.GetValue(i) != null && tmpArray.GetValue(i).GetType() == typeof(double))
                                {
                                    double tmp2 = (double)tmpArray.GetValue(i);
                                    if (double.IsInfinity(tmp2))
                                    {
                                        tmpArray.SetValue(QC4Common.Common.Constants.ExcelDiv, i);
                                    }
                                }
                            }
                            break;
                        case 2:
                            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++)
                            {
                                for (j = tmpArray.GetLowerBound(1); j <= tmpArray.GetUpperBound(1); j++)
                                {
                                    if (tmpArray.GetValue(i, j) != null && tmpArray.GetValue(i, j).GetType() == typeof(double))
                                    {
                                        double tmp2 = (double)tmpArray.GetValue(i, j);
                                        if (double.IsInfinity(tmp2))
                                        {
                                            tmpArray.SetValue(QC4Common.Common.Constants.ExcelDiv, i, j);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            else if (VarType == typeof(string))
            {
                if (!NotRevise)
                {
                    switch (d)
                    {
                        case 1:
                            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++)
                            {
                                tmp = (string)tmpArray.GetValue(i);
                                if (tmp == null || tmp.Length == 0)
                                {
                                    tmpArray.SetValue(null, i);
                                }
                                else
                                {
                                    if (tmp.StartsWith("'")) { tmp = "'" + tmp; tmpArray.SetValue(tmp, i); }
                                }
                            }
                            break;
                        case 2:
                            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++)
                            {
                                for (j = tmpArray.GetLowerBound(1); j <= tmpArray.GetUpperBound(1); j++)
                                {
                                    tmp = (string)tmpArray.GetValue(i, j);
                                    if (tmp == null || tmp.Length == 0)
                                    {
                                        tmpArray.SetValue(null, i, j);
                                    }
                                    else
                                    {
                                        if (tmp.StartsWith("'")) { tmp = "'" + tmp; tmpArray.SetValue(tmp, i, j); }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            Range.Value = arrValue;
        }


        public static void PutValue(Range Range, ref string Value, bool NotRevise = false)
        {
            int u = 0;
            int i, j;
            int d = 0;
            string tmp;
            Type VarType = Value.GetType();
            if (Range == null) { return; }
            if (VarType == typeof(string))
            {
                if (!NotRevise)
                {
                    tmp = (string)Value;
                    if (tmp == null || tmp.Length == 0)
                    {
                        Range.Value = string.Empty;
                    }
                    else
                    {
                        if (tmp.StartsWith("'")) { tmp = "'" + tmp; }
                        Range.Value = tmp.Split(new char[] { }, 1);
                    }
                    return;
                }
            }
            //else if (VarType == typeof(bool) || VarType == typeof(double) || VarType == typeof(long)
            //    || VarType == typeof(int) || VarType == typeof(decimal) || VarType == typeof(bool)
            //    //|| VarType == typeof(bool)|| VarType == typeof(bool)|| VarType == typeof(bool)
            //    //|| VarType == typeof(bool)|| VarType == typeof()|| VarType == typeof(null)
            //    )
            //{
            //    //     Case vbBoolean, vbCurrency, vbDate, vbDouble, vbSingle, vbLong, vbInteger _
            //    //, vbDecimal, vbByte, vbEmpty, vbError, vbNull
            //}
            //else
            //{
            //    return;
            //}
            Range.Value = Value;
        }

        public static void PutValueLong(Range Range, ref string Value, bool NotRevise = false)
        {
            //only work if next cell is balnk

            string tmp;
            Type VarType = Value.GetType();
            if (Range == null) { return; }
            if (VarType == typeof(string))
            {
                if (!NotRevise)
                {
                    tmp = (string)Value;
                    if (tmp == null || tmp.Length == 0)
                    {
                        Range.Value = string.Empty;
                    }
                    else
                    {
                        if (tmp.StartsWith("'")) { tmp = "'" + tmp; }
                        Array v = Array.CreateInstance(typeof(string),
                          new int[] { 1, 2 },
                          new int[] { 1, 1 });
                        v.SetValue(tmp, 1, 1);
                        //v.SetValue(Range.Next.Value2, 1, 2);
                        Range.Resize[1, 2].Value = v;
                    }
                    return;
                }
            }
        }

        public static void PutValue(Range Range, ref object Value, bool NotRevise = false)
        {
            int u = 0;
            int i, j;
            int d = 0;
            string tmp;
            Type VarType = Value.GetType();
            if (Range == null) { return; }
            if (VarType == typeof(string))
            {
                if (!NotRevise)
                {
                    tmp = (string)Value;
                    if (tmp == null || tmp.Length == 0)
                    {
                        Range.Value = string.Empty;
                    }
                    else
                    {
                        if (tmp.StartsWith("'")) { tmp = "'" + tmp; }
                        Range.Value = tmp.Split(new char[] { }, 1);
                    }
                    return;
                }
            }
            //else if (VarType == typeof(bool) || VarType == typeof(double) || VarType == typeof(long)
            //    || VarType == typeof(int) || VarType == typeof(decimal) || VarType == typeof(bool)
            //    //|| VarType == typeof(bool)|| VarType == typeof(bool)|| VarType == typeof(bool)
            //    //|| VarType == typeof(bool)|| VarType == typeof()|| VarType == typeof(null)
            //    )
            //{
            //    //     Case vbBoolean, vbCurrency, vbDate, vbDouble, vbSingle, vbLong, vbInteger _
            //    //, vbDecimal, vbByte, vbEmpty, vbError, vbNull
            //}
            //else
            //{
            //    return;
            //}
            Range.Value = Value;
        }

        public static void AddPrefix(ref string buffer, bool ForceToString = false)
        {
            int l;
            if (null == buffer) { buffer = null; return; }

            l = buffer.Length;
            if (l == 0)
            {
                buffer = null;
            }
            else
            {
                if (ForceToString)
                {
                    if (IsNumeric(buffer) || IsDate(buffer))
                    {
                        buffer = "'" + buffer;
                        return;
                    }
                }
                if (l == 1)
                {
                    if (buffer == "'") { buffer = "''"; }
                }
                else
                {
                    if (Regex.IsMatch(buffer, @"^['=+@-]")) { buffer = "'" + buffer; }
                }
            }
        }

        public static bool IsNumeric(object text)
        {
            double test;
            return double.TryParse(Convert.ToString(text), out test);
        }


        public static bool IsDate(object text)
        {
            DateTime test;
            return DateTime.TryParse(Convert.ToString(text), out test);
        }

        public static string GetAdjustedHeader(string orgHeader, int FontSize = 9)
        {
            int MAX_HEADER_LENGTH = 253;
            string LEADER = "...";
            string Prefix = String.Empty;
            string header;
            orgHeader = orgHeader.Replace("&", "&&");
            if (FontSize > 0)
            {
                Prefix = "&" + FontSize;
                if (Regex.IsMatch(orgHeader, @"^[0-9]")) { Prefix = Prefix + ' '; }
            }
            header = Prefix + orgHeader;
            if (header.Length > MAX_HEADER_LENGTH)
            {
                header = header.Substring(0, MAX_HEADER_LENGTH - LEADER.Length) + LEADER;
            }
            return header;
        }


        public static void CopyRow(Range SourceRow, Range DestinationRow)
        {
            int c;
            int i;
            if (SourceRow == null || DestinationRow == null || DestinationRow.Worksheet.ProtectContents) return;
            //'256列まで情報を持つシートで2003形式への行コピー時に行高さを維持するためのおまじない
            c = SourceRow.Worksheet.UsedRange.Columns[SourceRow.Worksheet.UsedRange.Columns.Count].Column;
            SourceRow = SourceRow.Areas.Item[1].EntireRow;
            DestinationRow = DestinationRow.Areas.Item[1].EntireRow.Item[1];
            if (c > DestinationRow.Columns.Count)
            {
                SourceRow.Resize[Type.Missing, DestinationRow.Columns.Count].Copy(DestinationRow.Item[1]);
                CopyRowHeight(SourceRow.Rows, DestinationRow.Rows, 0);
            }
            else
            {
                SourceRow.Copy(DestinationRow);
            }
        }


        public static void CopyRowHeight(Range SourceRow, Range DestinationRow, double OrgRowHeight)
        {
            int c;
            int x;
            Range SourceRowHighPart;
            Range SourceRowLowPart;
            Range DestinationRowHighPart;
            Range DestinationRowLowPart;
            object tmpRowHeight;
            if (OrgRowHeight == 0) { OrgRowHeight = DestinationRow.Item[1].RowHeight; }
            tmpRowHeight = SourceRow.RowHeight;
            if (tmpRowHeight == null)
            {
                c = SourceRow.Count;
                x = c / 2;
                SourceRowHighPart = SourceRow.Resize[x];
                SourceRowLowPart = SourceRow.Item[x + 1].Resize[c - x];
                DestinationRowHighPart = DestinationRow.Resize[x];
                DestinationRowLowPart = DestinationRow.Item[x + 1].Resize[c - x];
                CopyRowHeight(SourceRowHighPart, DestinationRowHighPart, OrgRowHeight);
                CopyRowHeight(SourceRowLowPart, DestinationRowLowPart, OrgRowHeight);
            }
            else if ((double)tmpRowHeight != OrgRowHeight)
            {
                DestinationRow.RowHeight = tmpRowHeight;
            }
        }

        public static string BuildPath(string Path1, string Path2, string sep)
        {
            if (Path1.IndexOf(sep) == -1)
            {
                if (Path1.IndexOf("/") > 0) { sep = "/"; }
            }
            while (Path1.Substring(Path1.Length - sep.Length) == sep)
            {
                Path1 = Path1.Substring(0, Path1.Length - sep.Length);
            }

            while (Path2.Substring(0, sep.Length) == sep)
            {
                Path2 = Path2.Substring(sep.Length);
            }
            return Path1 + sep + Path2;
        }

        public static string GetTemplateDirectoryPath(string basePath, string sep)
        {
            string TEMPLATE_DIRECTORY_NAME = "Templates";
            return BuildPath(basePath, TEMPLATE_DIRECTORY_NAME, sep);
        }

        public static string BaseTemplatePath(string basePath, string sep)
        {
            return BuildPath(GetTemplateDirectoryPath(basePath, sep), BASE_TEMPLATE_NAME, sep);
        }



        //public static void PutValue(Range Range, ref Array Value, bool NotRevise = false)
        //{
        //    int u = 0;
        //    int i, j;
        //    int d = 0;
        //    string tmp;
        //    Type VarType = Value.GetType();
        //    Array tmpArray = null;
        //    if (Range == null) { return; }
        //    if (Value.GetType().IsArray)
        //    {
        //        VarType = VarType.GetElementType();
        //        tmpArray = (Array)Value;
        //        switch (tmpArray.Rank)
        //        {
        //            case 1:
        //            case 2:
        //                u = tmpArray.GetUpperBound(tmpArray.Rank);
        //                break;
        //            default:
        //                return;
        //        }
        //        if (u < tmpArray.GetLowerBound(tmpArray.Rank)) { return; }
        //    }

        //    //#if( AFTER_1HALF_PHASE ){
        //    if (VarType == typeof(object))
        //    {
        //        if (NotRevise)
        //        {
        //            switch (d)
        //            {
        //                case 1:
        //                    for (i = tmpArray.GetLowerBound(1); i <= tmpArray.GetUpperBound(1); i++)
        //                    {
        //                        if (tmpArray.GetValue(i).GetType() == typeof(string))
        //                        {
        //                            tmp = (string)tmpArray.GetValue(i);
        //                            if (tmp.Length == 0)
        //                            {
        //                                tmpArray.SetValue(string.Empty, i);
        //                            }
        //                            else
        //                            {
        //                                // AddPrefix tmp
        //                                tmpArray.SetValue(tmp, i);
        //                            }
        //                        }
        //                    }
        //                    break;
        //                case 2:
        //                    for (i = tmpArray.GetLowerBound(1); i <= tmpArray.GetUpperBound(1); i++)
        //                    {
        //                        for (j = tmpArray.GetLowerBound(2); j <= tmpArray.GetUpperBound(j); j++)
        //                        {
        //                            if (tmpArray.GetValue(i, j).GetType() == typeof(string))
        //                            {
        //                                tmp = (string)tmpArray.GetValue(i, j);
        //                                if (tmp.Length == 0)
        //                                {
        //                                    tmpArray.SetValue(string.Empty, i, j);
        //                                }
        //                                else
        //                                {
        //                                    //AddPrefix tmp
        //                                    tmpArray.SetValue(tmp, i, j);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    break;
        //            }
        //        }
        //    }
        //    else if (VarType == typeof(string))
        //    {
        //        if (!NotRevise)
        //        {
        //            switch (d)
        //            {
        //                case 0:
        //                    tmp = (string)Value;
        //                    if (tmp.Length == 0)
        //                    {
        //                        Range.Value = string.Empty;
        //                    }
        //                    else
        //                    {
        //                        if (tmp.StartsWith("'")) { tmp = "'" + tmp; }
        //                        Range.Value = tmp.Split(new char[] { }, 1);
        //                    }
        //                    return;
        //                case 1:
        //                    for (i = tmpArray.GetLowerBound(1); i <= tmpArray.GetUpperBound(1); i++)
        //                    {
        //                        tmp = (string)tmpArray.GetValue(i);
        //                        if (tmp.Length == 0)
        //                        {
        //                            tmpArray.SetValue(null, i);
        //                        }
        //                        else
        //                        {
        //                            if (tmp.StartsWith("'")) { tmp = "'" + tmp; tmpArray.SetValue(tmp, i); }
        //                        }
        //                    }
        //                    break;
        //                case 2:
        //                    for (i = tmpArray.GetLowerBound(1); i <= tmpArray.GetUpperBound(1); i++)
        //                    {
        //                        for (j = tmpArray.GetLowerBound(2); j <= tmpArray.GetUpperBound(j); j++)
        //                        {
        //                            tmp = (string)tmpArray.GetValue(i, j);
        //                            if (tmp.Length == 0)
        //                            {
        //                                tmpArray.SetValue(null, i, j);
        //                            }
        //                            else
        //                            {
        //                                if (tmp.StartsWith("'")) { tmp = "'" + tmp; tmpArray.SetValue(tmp, i, j); }
        //                            }
        //                        }
        //                    }
        //                    break;
        //            }
        //        }
        //    }
        //    else if (VarType == typeof(bool) || VarType == typeof(double) || VarType == typeof(long)
        //        || VarType == typeof(int) || VarType == typeof(decimal) || VarType == typeof(bool)
        //        //|| VarType == typeof(bool)|| VarType == typeof(bool)|| VarType == typeof(bool)
        //        //|| VarType == typeof(bool)|| VarType == typeof()|| VarType == typeof(null)
        //        )
        //    {
        //        //     Case vbBoolean, vbCurrency, vbDate, vbDouble, vbSingle, vbLong, vbInteger _
        //        //, vbDecimal, vbByte, vbEmpty, vbError, vbNull
        //    }
        //    else
        //    {
        //        return;
        //        //#}else{
        //        //        Case vbVariant, vbString
        //        //            Select Case d
        //        //                Case 0&
        //        //                    tmp = CStr(Value)
        //        //                    if( tmp Like "['=+'@-]*" ){ Value = "'" & tmp}
        //        //                Case 1&
        //        //                    for( i = LBound(Value) To UBound(Value)){
        //        //                        tmp = CStr(Value(i))
        //        //                        if( tmp Like "['=+'@-]*" ){ Value(i) = "'" & tmp}
        //        //                    }
        //        //                Case 2&
        //        //                    for( i = LBound(Value, 1) To UBound(Value, 1)){
        //        //                        for( j = LBound(Value, 2) To UBound(Value, 2)){
        //        //                            tmp = CStr(Value(i, j))
        //        //                            if( tmp Like "['=+'@-]*" ){ Value(i, j) = "'" & tmp}
        //        //                        }
        //        //                    }
        //        //            End Select
        //        //#}
        //    }
        //    Range.Value = Value;
        //}

        public static object CreateObject(string objectName)
        {
            //return Interaction.CreateObject(objectName);
            return Activator.CreateInstance(Type.GetTypeFromProgID(objectName));
        }

        public static void AddSheetCustomProperty(Worksheet Sheet, string pName, string pValue)
        {
            if (null == pName) { return; }
            foreach (CustomProperty p in Sheet.CustomProperties)
            {
                if (p.Name == pName)
                {
                    SetSheetCustomProperty(p, pValue);
                    return;
                }
            }
            if (null == pValue) { pValue = string.Empty; }
            Sheet.CustomProperties.Add(pName, pValue);
        }


        public static void SetSheetCustomProperty(CustomProperty p, string NewValue)
        {
            if (null == NewValue) { NewValue = string.Empty; }
            p.Value = NewValue;
        }


        public static void SetHideZeroNumberFormat(Series Series)
        {
            DataLabels dls;
            string fmt;

            DataLabel dl;
            if (Series == null) { return; }
            dls = Series.DataLabels();
            fmt = dls.NumberFormat;
            if (fmt == ";;;")
            {
                foreach (Point p in Series.Points())
                {
                    dl = p.DataLabel;
                    fmt = dl.NumberFormat;
                    AdjustFormat(ref fmt);
                    dl.NumberFormat = fmt;
                }
            }
            else
            {
                AdjustFormat(ref fmt);
                dls.NumberFormat = fmt;
            }
            return;
        }

        private static void AdjustFormat(ref string fmt)
        {
            string[] buf; // string
            buf = fmt.Split(new char[] { ';' }, 2);
            Array.Resize(ref buf, 3);
            buf.SetValue(string.Empty, 1);
            if (buf.GetValue(2) == null)
            {
                buf.SetValue(string.Empty, 2);
            }
            fmt = string.Join(";", buf);
        }

        public static void FitChartHeightAndPositionToRangeRight(ChartObject chtObj, Range rng, Application xlApp)
        {
            FitChartHeightToRangeHeight(chtObj, rng, xlApp);
            PositionToRangeRight(chtObj, rng, xlApp);
        }

        private static void PositionToRangeRight(ChartObject chtObj, Range rng, Application xlApp)
        {
            double t;
            double l;
            t = rng.Top;
            l = rng.Left + rng.Width;
            chtObj.Parent.Activate();
            Chart WithchtObjChart = chtObj.Chart;
            xlApp.ScreenUpdating = true;
            xlApp.ScreenUpdating = false;
            t = t - OVER_2007_CHART_AREA_TOP - WithchtObjChart.PlotArea.InsideTop;
            l = l - OVER_2007_CHART_AREA_LEFT - WithchtObjChart.PlotArea.InsideLeft;
            if (t < 0)
            {
                WithchtObjChart.Parent.Top = 0;
                WithchtObjChart.PlotArea.Top = WithchtObjChart.PlotArea.Top + t;
            }
            else
            {
                WithchtObjChart.Parent.Top = t;
            }
            if (l < 0)
            {
                WithchtObjChart.Parent.Left = 0;
                WithchtObjChart.PlotArea.Left = WithchtObjChart.PlotArea.Left + l;
            }
            else
            {
                WithchtObjChart.Parent.Left = l;
            }
        }




        public static void FitChartHeightToRangeHeight(ChartObject chtObj, Range rng, Application xlApp)
        {
            FitChartHeight(chtObj, rng, xlApp);
        }


        public static void FitChartHeight(ChartObject chtObj, Range rng, Application xlApp)
        {
            double t;
            double o;  //' Offset
            double d;  // ' Distance
            double h = rng.Height;
            chtObj.Parent.Activate();
            Chart WithchtObjChart = chtObj.Chart;
            PlotArea WithPlotArea = WithchtObjChart.PlotArea;
            xlApp.ScreenUpdating = true;
            //hack for getting correct legend postion
            rng.Select();
            xlApp.ScreenUpdating = false;
            t = WithPlotArea.Top;
            o = WithPlotArea.Height - WithPlotArea.InsideHeight;
            d = h + o - WithPlotArea.InsideHeight;
            WithchtObjChart.Parent.Height = WithchtObjChart.Parent.Height + d;
            WithPlotArea = WithchtObjChart.PlotArea;
            xlApp.ScreenUpdating = true;
            xlApp.ScreenUpdating = false;
            WithPlotArea.Top = t;
            o = WithPlotArea.Height - WithPlotArea.InsideHeight;
            WithPlotArea.Height = h + o;
            AdjustOverlap(chtObj, xlApp);
        }


        public static void AdjustOverlap(ChartObject chtObj, Application xlApp)
        {
            double MAX_CHART_OBJECT_HEIGHT = 450;
            double VERTICAL_MARGIN = 3.75; //  ' 5px
            double HORIZONTAL_MARGIN = 3.75; //  ' 5px
            double TitleBottom = 0;
            double o = 0;
            double tmpHeight = 0;
            double h = 0;
            SeriesCollection ss = null;
            Points ps = null;
            List<DataLabel> dLbls;
            DataLabel lbl1 = null;
            DataLabel lbl2 = null;
            double t = 0;
            double l = 0;
            double d = 0;
            int i = 0;
            int j = 0;
            bool existIntersect = false;
            double t1 = 0;
            double t2 = 0;
            double b1 = 0;
            double b2 = 0;
            double l1 = 0;
            double l2 = 0;
            double r1 = 0;
            double r2 = 0;
            chtObj.Parent.Activate();
            Chart WithchtObjChart = chtObj.Chart;
            if (WithchtObjChart.HasTitle) { TitleBottom = WithchtObjChart.ChartTitle.Top + WithchtObjChart.ChartTitle.Height; }
            if (WithchtObjChart.HasLegend)
            {
                if (WithchtObjChart.Legend.Position == XlLegendPosition.xlLegendPositionTop)
                {
                    t = WithchtObjChart.Legend.Top + WithchtObjChart.Legend.Height;
                    if (t > TitleBottom) { TitleBottom = t; }
                }
            }
            if (WithchtObjChart.ChartType == XlChartType.xlPie)
            {
                ss = WithchtObjChart.SeriesCollection();
                ps = ss.Item(1).Points();
                dLbls = new List<DataLabel>();
                for (i = 1; i <= ps.Count; i++)
                {
                    if (ps.Item(i).HasDataLabel) { dLbls.Add(ps.Item(i).DataLabel); }
                }
                do
                {
                    xlApp.ScreenUpdating = true;
                    xlApp.ScreenUpdating = false;
                    t = WithchtObjChart.PlotArea.Top;
                    existIntersect = false;
                    for (i = 1; i <= dLbls.Count(); i++)
                    {
                        lbl1 = dLbls[i];
                        if (lbl1.Top < t) { t = lbl1.Top; }
                        if (!existIntersect)
                        {
                            t1 = lbl1.Top + VERTICAL_MARGIN;
                            b1 = lbl1.Top + lbl1.Height - VERTICAL_MARGIN;
                            l1 = lbl1.Left + HORIZONTAL_MARGIN;
                            r1 = lbl1.Left + lbl1.Width - HORIZONTAL_MARGIN;
                            for (j = i + 1; j <= dLbls.Count(); j++)
                            {
                                lbl2 = dLbls[j];
                                t2 = lbl2.Top + VERTICAL_MARGIN;
                                b2 = lbl2.Top + lbl2.Height - VERTICAL_MARGIN;
                                l2 = lbl2.Left + HORIZONTAL_MARGIN;
                                r2 = lbl2.Left + lbl2.Width - HORIZONTAL_MARGIN;
                                if ((t1 - t2) * (b1 - t2) < 0 || (t1 - b2) * (b1 - b2) < 0 ||
                                    (t2 - t1) * (b2 - t1) < 0 || (t2 - b1) * (b2 - b1) < 0)
                                {
                                    if ((l1 - l2) * (r1 - l2) < 0 || (l1 - r2) * (r1 - r2) < 0 ||
                                    (l2 - l1) * (r2 - l1) < 0 || (l2 - r1) * (r2 - r1) < 0)
                                    {
                                        existIntersect = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (existIntersect)
                    {
                        if (t >= TitleBottom) { t = TitleBottom - 10; }  //' データラベル間の被りがあったら、強制的に調整する
                    }
                    if (t >= TitleBottom) { break; }
                    PlotArea WithPlotArea = WithchtObjChart.PlotArea;
                    d = TitleBottom - t;
                    h = WithPlotArea.Height - d;
                    if (h >= chtObj.Height * 0.4)
                    {
                        t = WithPlotArea.Top + d / 2;
                        l = WithPlotArea.Left + d / 2;
                        WithPlotArea.Height = h;
                        WithPlotArea.Width = h;
                        WithPlotArea.Left = l;
                        WithPlotArea.Top = t;
                    }
                    else
                    {
                        h = WithPlotArea.Height;
                        tmpHeight = chtObj.Height + d;
                        if (tmpHeight > MAX_CHART_OBJECT_HEIGHT) { tmpHeight = MAX_CHART_OBJECT_HEIGHT; }
                        chtObj.Height = tmpHeight;
                        WithPlotArea.Height = h;
                        WithPlotArea.Top = tmpHeight - WithPlotArea.Height;
                        break;
                    }

                } while (true);
            }
            else
            {
                PlotArea WithPlotArea = WithchtObjChart.PlotArea;
                if (WithPlotArea.Top >= TitleBottom) { return; }
                xlApp.ScreenUpdating = true;
                xlApp.ScreenUpdating = false;
                o = TitleBottom - WithPlotArea.Top;
                h = WithPlotArea.Height;
                tmpHeight = chtObj.Height + o;
                switch (chtObj.Chart.ChartType)
                {
                    case XlChartType.xlBarClustered:
                    case XlChartType.xlBarStacked:
                    case XlChartType.xlBarStacked100:
                        break;
                    default:
                        if (tmpHeight > MAX_CHART_OBJECT_HEIGHT) { tmpHeight = MAX_CHART_OBJECT_HEIGHT; }
                        break;
                }
                chtObj.Height = tmpHeight;
                WithPlotArea.Height = h;
                WithPlotArea.Top = tmpHeight - WithPlotArea.Height;
            }

        }

        public static void FitChartWidth(ChartObject chtObj, double w, Application xlApp, int sectorCnt)
        {
            double l;
            double o;  //' Offset
            double d; //' Distance
            chtObj.Parent.Activate();
            Chart WithchtObjChart = chtObj.Chart;
            PlotArea WithPlotArea = WithchtObjChart.PlotArea;
            xlApp.ScreenUpdating = true;
            xlApp.ScreenUpdating = false;
            l = WithPlotArea.Left;
            if (sectorCnt > 1)
            {
                o = WithPlotArea.Width - WithPlotArea.InsideWidth;
                d = w + o - WithPlotArea.InsideWidth;
                WithchtObjChart.Parent.Width = WithchtObjChart.Parent.Width + d;
                WithPlotArea = WithchtObjChart.PlotArea;
            }
            xlApp.ScreenUpdating = true;
            xlApp.ScreenUpdating = false;
            WithPlotArea.Left = l;
            o = WithPlotArea.Width - WithPlotArea.InsideWidth;
            WithPlotArea.Width = w + o;
        }



        public static void FitChartWidthAndPositionToRangeTop(ChartObject chtObj, Range rng, Application xlApp, int sectorCnt)
        {
            double l;
            double w;
            double t;
            w = rng.Width;
            FitChartWidth(chtObj, w, xlApp, sectorCnt);
            l = rng.Left;
            t = rng.Top;
            Chart WithchtObjChart = chtObj.Chart;
            xlApp.ScreenUpdating = true;
            xlApp.ScreenUpdating = false;
            l = l - OVER_2007_CHART_AREA_LEFT - WithchtObjChart.PlotArea.InsideLeft;
            t = t - OVER_2007_CHART_AREA_TOP - WithchtObjChart.PlotArea.InsideTop - WithchtObjChart.PlotArea.InsideHeight;
            if (l < 0)
            {
                WithchtObjChart.Parent.Left = 0;
                WithchtObjChart.PlotArea.Left = WithchtObjChart.PlotArea.Left + l;
            }
            else
            {
                WithchtObjChart.Parent.Left = l;
            }
            if (t < 0)
            {
                WithchtObjChart.Parent.Top = 0;
                WithchtObjChart.PlotArea.Top = WithchtObjChart.PlotArea.Top + t;
            }
            else
            {
                WithchtObjChart.Parent.Top = t;
            }
        }


        public static bool IsNumeric(string value)
        {
            if (value == null)
            {
                return false;
            }
            return Information.IsNumeric(value);
            // return value.All(char.IsNumber);
        }

        public static long StrPtr(string MyString)// get address of variable
        {
            GCHandle gh = GCHandle.Alloc(MyString, GCHandleType.Pinned);
            IntPtr AddrOfMyString = gh.AddrOfPinnedObject();
            return AddrOfMyString.ToInt64();
        }

        public bool DBUpdateSampleWeightBack(Workbook workBook)
        {
            bool isSuccess = false;
            try
            {
                //Reading from Excel settings
                string wText = "";
                int settingIndex = 0;
                Worksheet settingSheet = ExcelUtil.GetWorkSheetByCodeName(workBook, Util.Constants.SheetCodeName.Setting);
                Worksheet workSheet = workBook.Worksheets[Util.Constants.SheetType.sh_Data01];
                Range start = workSheet.Cells[3, 2];
                long lastRow = start.EntireRow.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                long lastCol = Definiotion.VariableDictionary.Count();
                if (lastCol == -1) lastCol = start.EntireColumn.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Column;
                Dictionary<string, string> weightBack = new Dictionary<string, string>();
                if (settingSheet != null)
                {
                    wText = settingSheet.Cells[2, 10].Text;
                    if (Definiotion.VariableDictionary.ContainsKey(wText))
                    {
                        QuestionSettings qstnDet = Definiotion.VariableDictionary[wText];
                        wText = Util.Constants.DBSettings.ColumnNamePreText + qstnDet.Id;

                        Range s = settingSheet.Cells[3, 9];
                        Range e = ExcelUtil.EndxlUp(s);
                        Range range = settingSheet.get_Range(s, e);
                        foreach (Range r in range)
                        {
                            if (!weightBack.ContainsKey(r.Text))
                                weightBack.Add(r.Text, (r.Offset[0, 3].Value2).ToString());
                        }
                        Worksheet qsSheet = ExcelUtil.GetWorkSheetByCodeName(workBook, Util.Constants.SheetCodeName.QuestionSetting);
                        Range dHead = workSheet.get_Range(start, start.Offset[0, lastCol]);

                        dHead = dHead.Find(wText);

                        if (null != dHead)
                        {
                            settingIndex = dHead.Column;
                        }
                    }
                }

                using (SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                {
                    con.Open();
                    string oldWeightBackTable = null;
                    string sSql = "Select weight_back_table from weight_back where name='" + Util.Constants.WeightBack + "' ";
                    DataTable dtResult = DBHelper.GetDataTable(sSql, con);

                    string tableName = "answers";
                    string dataTablename = "answers";
                    bool isMultivariate = false;
                    if (DBHelper.checkAfterProcess(workBook))
                    {
                        tableName = "data_after_process";
                        dataTablename = "data_after_process";
                    }
                    if (DBHelper.checkIsMultivariateVariable(workBook, wText))
                    {
                        tableName = "multivariate";
                        isMultivariate = true;
                    }
                    using (SQLiteTransaction tr = con.BeginTransaction())
                    {
                        using (SQLiteCommand sQLiteCommand = con.CreateCommand())
                        {
                            sQLiteCommand.Transaction = tr;
                            try
                            {
                                if (dtResult.Rows.Count > 0)
                                {
                                    oldWeightBackTable = Convert.ToString(dtResult.Rows[0][0]).Trim();
                                }
                                else
                                {
                                    sSql = "Insert into weight_back(name,weight_back_table) values('" + Util.Constants.WeightBack + "', null) ";
                                    sQLiteCommand.CommandText = sSql;
                                    sQLiteCommand.ExecuteNonQuery();
                                    oldWeightBackTable = null;
                                }

                                if (oldWeightBackTable != null && oldWeightBackTable.Length > 0)
                                {
                                    sSql = "DROP TABLE IF EXISTS " + oldWeightBackTable;
                                    sQLiteCommand.CommandText = sSql;
                                    sQLiteCommand.ExecuteNonQuery();
                                }


                                if (wText != "")
                                {
                                    string newWTable = "weight_back_" + wText;

                                    sSql = " UPDATE weight_back set weight_back_table='" + newWTable + "' where name='" + Util.Constants.WeightBack + "'  ";
                                    sQLiteCommand.CommandText = sSql;
                                    sQLiteCommand.ExecuteNonQuery();

                                    sSql = "DROP TABLE IF EXISTS " + newWTable;
                                    sQLiteCommand.CommandText = sSql;
                                    sQLiteCommand.ExecuteNonQuery();

                                    sSql = "CREATE TABLE IF NOT EXISTS " + newWTable;
                                    sSql += "(sample_id  VARCHAR(255) ,value DECIMAL(19,9))";
                                    sQLiteCommand.CommandText = sSql;
                                    sQLiteCommand.ExecuteNonQuery();

                                    if (weightBack.Count > 0)
                                    {
                                        if (!isMultivariate)
                                        {
                                            sSql = " insert into " + newWTable + "(sample_id,value) select sample_id, ";
                                            sSql += " case " + wText + " ";
                                            for (int i = 0; i <= weightBack.Count - 1; i++)
                                            {
                                                sSql += " When '" + weightBack.ElementAt(i).Key + "' Then '" + weightBack.ElementAt(i).Value + "' ";
                                            }
                                            sSql += " Else '0' End Value from " + tableName + " ";
                                        }
                                        else
                                        {
                                            sSql = " insert into " + newWTable + "(sample_id,value) select m.sample_id, ";
                                            sSql += " case " + wText + " ";
                                            for (int i = 0; i <= weightBack.Count - 1; i++)
                                            {
                                                sSql += " When '" + weightBack.ElementAt(i).Key + "' Then '" + weightBack.ElementAt(i).Value + "' ";
                                            }
                                            sSql += " Else '0' End Value from " + tableName + " m join " + dataTablename + " a on a.sort_no = m.sort_no ";
                                        }
                                        sQLiteCommand.CommandText = sSql;
                                        sQLiteCommand.ExecuteNonQuery();
                                    }
                                }
                                tr.Commit();
                                isSuccess = true;
                            }
                            catch (Exception ex)
                            {
                                _log.Error(ex.StackTrace);
                                tr.Rollback();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.StackTrace);
                //MessageDialog.Error(ex.Message);
                MessageDialog.ShowMessageOnWorkBook(ex.Message, Enums.MessageType.Error, workBook);
            }
            return isSuccess;
        }

        public List<Data> GetWeightList(Workbook workbook, string variableName, ref bool IsWeightListValid)
        {
            IsWeightListValid = true;
            List<Data> WBDataList = null;
            QCWebException ex = null;

            string tableName = "answers";
            if (DBHelper.checkAfterProcess(workbook))
            {
                tableName = "data_after_process";
            }
            if (DBHelper.checkIsAnVariable(workbook, variableName))
            {
                tableName = "multivariate";
            }

            using (SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workbook)))
            {
                con.Open();
                if (variableName == Util.Constants.WeightBack)
                {
                    if (DBUpdateSampleWeightBack(workbook))
                    {
                        DataTable dataTable = DBHelper.GetDataTable("Select weight_back_table from weight_back ", con);
                        if (dataTable.Rows.Count > 0)
                        {
                            string weightBackTable = Convert.ToString(dataTable.Rows[0][0]).Trim();
                            if (weightBackTable.Length > 0)
                            {
                                try
                                {
                                    DataTable dataTbleWB = DBHelper.GetDataTable("Select value from " + weightBackTable, con);
                                    WBDataList = ReadTextFile.ReadDataTable(dataTbleWB, QuestionType.N, null, out ex);
                                }
                                catch (Exception e)
                                {
                                    if (e.Message.Contains("no such column")) // If no such column, stop execution
                                    {
                                        IsWeightListValid = false;
                                        MessageDialog.ShowMessageOnWorkBook(string.Format(LocalResource.GT_SAMPLE_WEIGHT_NO_DATA, variableName), Enums.MessageType.Warning, workbook);
                                        //MessageDialog.Warning(string.Format(GTMessages.NoDataSampleWng, variableName));
                                    }
                                    else
                                        throw;
                                }
                            }
                        }
                    }
                }
                else
                {
                    QuestionSettings qstnDet = Definiotion.VariableDictionary[variableName];

                    string columnName = Util.Constants.DBSettings.ColumnNamePreText + qstnDet.Id;
                    if (variableName.ToUpper() == Util.Constants.QuestionVariableValue.QuestionVariableItem.ToUpper())
                        columnName = Util.Constants.DBSettings.SampleIdColumnName;

                    DataTable dataTble = new DataTable();
                    try
                    {
                        dataTble = DBHelper.GetDataTable("Select " + columnName + " from " + tableName + " order by sort_no ", con);
                        WBDataList = ReadTextFile.ReadDataTable(dataTble, QuestionType.N, null, out ex);
                    }
                    catch (Exception e)
                    {
                        if (e.Message.Contains("no such column")) // If no such column, stop execution
                        {
                            IsWeightListValid = false;
                            MessageDialog.ShowMessageOnWorkBook(string.Format(LocalResource.GT_SAMPLE_WEIGHT_NO_DATA, variableName), Enums.MessageType.Warning, workbook);
                            //MessageDialog.Warning(string.Format(GTMessages.NoDataSampleWng, variableName));
                        }
                        else
                            throw;
                    }
                }
            }
            if (ex != null)
            {
                _log.Error(ex.StackTrace);
                WBDataList = null;
            }
            return WBDataList;
        }

        public static int GetColor(int itemIndex)
        {
            return Util.Constants.DefaultColorIndex[itemIndex];
        }

        public static string RemoveLeadingSpclChar(string buffer, bool ForceToString = false)
        {
            int l;
            if (null == buffer) { return null; }

            l = buffer.Length;
            if (l == 0)
            {
                buffer = null;
            }
            else
            {
                //buffer = @"=""" + buffer + @"""" ;
                if (Regex.IsMatch(buffer, @"^['=+@-]")) { buffer = @"=""" + buffer + @""""; }
                // buffer = Regex.Replace(buffer, @"^['=+@-]", "");
            }
            return buffer;
        }
        //public static T Clone<T>(T source)
        //{
        //    if (!typeof(T).IsSerializable)
        //    {
        //        throw new ArgumentException("The type must be serializable.", "source");
        //    }

        //    if (Object.ReferenceEquals(source, null))
        //    {
        //        return default(T);
        //    }

        //    System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //    Stream stream = new MemoryStream();
        //    using (stream)
        //    {
        //        formatter.Serialize(stream, source);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return (T)formatter.Deserialize(stream);
        //    }
        //}

        public static T DeepClone<T>(T obj)
        {
            T objResult;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = (T)bf.Deserialize(ms);
            }
            return objResult;
        }



        public static string GetSigLogPath(string sep)
        {
            string sigLogPath = null;
            try
            {
                string appPath = AppContext.BaseDirectory;
                string StatPath = BuildPath(appPath, "STAT.txt", sep);
                _log.Info("appPath:" + appPath);
                if (File.Exists(StatPath))
                {
                    sigLogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Macromill", "QuickCross", "QC4", "Logs", "Significance");
                    if (!Directory.Exists(sigLogPath))
                        Directory.CreateDirectory(sigLogPath);
                    sigLogPath = BuildPath(sigLogPath, DateTime.Now.ToString("yyMMdd_HHmmss") + ".log", sep);
                }
            }
            catch (Exception ex)
            {
                sigLogPath = null;
                _log.Error(ex.Message + "\n" + ex.StackTrace);
            }
            return sigLogPath;
        }

        public static void AdjustQuestionSentanceRowHeight(Range tableRange, Worksheet workSheet, Application xlApp)
        {
            try
            {
                Range questionRow = tableRange.Rows[1].Offset[-2].Rows[1];
                AutoFitEx(questionRow, xlApp, workSheet, Util.Constants.ExcelRowMaxHeight);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static void AutofitColumn(Range tableRange)
        {
            try
            {
                for (int i = 1; i <= tableRange.Columns.Count; i++)
                {
                    double prevWidth = tableRange.Columns.Item[i].ColumnWidth;
                    tableRange.Columns.Item[i].AutoFit();
                    double newWidth = tableRange.Columns.Item[i].ColumnWidth;
                    if (newWidth < prevWidth)
                    {
                        tableRange.Columns.Item[i].ColumnWidth = prevWidth;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }



        internal static object getTemplatePath(string basePath, string tEMPLATE_NAME, string sep)
        {
            return BuildPath(GetTemplateDirectoryPath(basePath, sep), tEMPLATE_NAME, sep);
        }
    }
}
