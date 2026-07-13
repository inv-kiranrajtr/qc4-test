using Macromill.QCWeb.ReportRequest;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Qc4Launcher.Logic
{
    class OutputUtil
    {

        public static int MAX_HPAGEBREAKS_COUNT = 1023;
        public static readonly Double HEADER_MAX_ROW_HEIGHT = 81;   // ' 108px
        public static Double DATA_MAX_ROW_HEIGHT = 36;     // ' 48px
        public static int LINE_COLOR = 0X808080;
        public static string BASE_TEMPLATE_NAME = "Base.xltx";

        private static void AutoFitExMain(Range Rows, Worksheet WorkingSheet, bool AddOneRow, double MaxHeight)
        {
            int tmp;
            Range upperRows;
            Range underRows;
            Range Row;
            Array rowHeights;
            int i;
            Array v = null;
            Array tmpBuf;
            int x, y;
            double h;
            if (Rows.MergeCells == null)
            {
                if (WorkingSheet == null)
                {
                    //  WorkingSheet = GetWorkingBook.Worksheets.Item(1)
                    WorkingSheet.UsedRange.Clear();
                }
                if (Rows.Count == 1)
                {
                    //   AutoFitExSub .Rows, WorkingSheet, AddOneRow, MaxHeight
                }
                else
                {
                    tmp = Rows.Count / 2;
                    upperRows = Rows.Resize[tmp];
                    underRows = Rows.Item[tmp + 1].Resize[Rows.Count - tmp];
                    AutoFitExMain(upperRows, WorkingSheet, AddOneRow, MaxHeight);
                    AutoFitExMain(underRows, WorkingSheet, AddOneRow, MaxHeight);
                }
            }
            else if (!Rows.MergeCells)
            {
                if (AddOneRow)
                {
                    Range WithRowsCells = Rows.Cells;
                    if (WithRowsCells.Count == 1)
                    {
                        v = Array.CreateInstance(typeof(object), new int[] { 1, 1 }, new int[] { 1, 1 });
                        v.SetValue(WithRowsCells.Value, 1, 1);
                    }
                    else
                    {
                        v = WithRowsCells.Value;
                    }
                    tmpBuf = Array.CreateInstance(typeof(string), new int[] { v.GetUpperBound(0), v.GetUpperBound(1) }, new int[] { 1, 1 });
                    for (y = 1; y <= v.GetUpperBound(0); y++)
                    {
                        for (x = 1; x <= v.GetUpperBound(1); x++)
                        {
                            tmpBuf.SetValue(v.GetValue(y, x) + "\n", y, x);
                        }
                    }
                    PutValue(WithRowsCells.Cells, ref tmpBuf);
                }
                rowHeights = Array.CreateInstance(typeof(double), new int[] { Rows.Count }, new int[] { 1 });
                for (i = 1; i <= Rows.Count; i++)
                {
                    rowHeights.SetValue(Rows.Item[i].RowHeight, i);
                }
                Rows.AutoFit();
                for (i = 1; i <= Rows.Count; i++)
                {
                    Row = Rows.Item[i];
                    h = Row.RowHeight;
                    if ((double)rowHeights.GetValue(i) > h) { h = (double)rowHeights.GetValue(i); }
                    if (h > MaxHeight) { h = MaxHeight; }
                    if (Row.RowHeight != h) { Row.RowHeight = h; }
                }
                if (AddOneRow) { PutValue(Rows.Cells, ref v); }
            }
            else
            {
                if (WorkingSheet == null)
                {
                    // WorkingSheet = GetWorkingBook.Worksheets.Item(1)
                    WorkingSheet.UsedRange.Clear();
                }
                foreach (Range RowI in Rows.Rows)
                {
                    //   AutoFitExSub(RowI, WorkingSheet, AddOneRow, MaxHeight);
                }
            }
        }

        public static void AutoFitEx(Range Range)
        {
            AutoFitEx(Range, false, HEADER_MAX_ROW_HEIGHT);
        }

        public static void AutoFitEx(Range Range, Boolean AddOneRow)
        {
            AutoFitEx(Range, AddOneRow, HEADER_MAX_ROW_HEIGHT);
        }
        public static void AutoFitEx(Range Range, Boolean AddOneRow, double MaxHeight)
        {
            Range Rows;
            Worksheet WorkingSheet;
            //        String OrgProcName As String
            //OrgProcName = RunningProcName
            //RunningProcName = "GlobalMember.AutoFitEx"
            if (Range == null) return;
            if (Range.Worksheet.ProtectContents) return;// "保護されているシートの行高は変更できません。"
            Rows = Range.Application.Intersect(Range, Range).Rows;
            // AutoFitExMain Rows, WorkingSheet, AddOneRow, MaxHeight

        }

        public static void PutValue(Range Range, ref Array Value, bool NotRevise = false)
        {
            int u = 0;
            int i, j;
            int d = 0;
            string tmp;
            Type VarType = Value.GetType().GetElementType();
            if (Range == null) { return; }
            //VarType = VarType.GetElementType();
            Array tmpArray = (Array)Value;
            switch (tmpArray.Rank)
            {
                case 1:
                case 2:
                    u = tmpArray.GetUpperBound(tmpArray.Rank-1);
                    d = tmpArray.Rank;
                    break;
                default:
                    return;
            }
            if (u < tmpArray.GetLowerBound(tmpArray.Rank-1)) { return; }
            if (VarType == typeof(object))
            {
                if (NotRevise)
                {
                    switch (d)
                    {
                        case 1:
                            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++)
                            {
                                if (tmpArray.GetValue(i).GetType() == typeof(string))
                                {
                                    tmp = (string)tmpArray.GetValue(i);
                                    if (tmp == null || tmp.Length == 0)
                                    {
                                        tmpArray.SetValue(string.Empty, i);
                                    }
                                    else
                                    {
                                        // AddPrefix tmp
                                        tmpArray.SetValue(tmp, i);
                                    }
                                }
                            }
                            break;
                        case 2:
                            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++)
                            {
                                for (j = tmpArray.GetLowerBound(1); j <= tmpArray.GetUpperBound(1); j++)
                                {
                                    if (tmpArray.GetValue(i, j).GetType() == typeof(string))
                                    {
                                        tmp = (string)tmpArray.GetValue(i, j);
                                        if (tmp == null || tmp.Length == 0)
                                        {
                                            tmpArray.SetValue(string.Empty, i, j);
                                        }
                                        else
                                        {
                                            //AddPrefix tmp
                                            tmpArray.SetValue(tmp, i, j);
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
            Range.Value = Value;
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
                    if (IsNumeric(buffer) || IsDate(buffer.GetType()))
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





    }
}
