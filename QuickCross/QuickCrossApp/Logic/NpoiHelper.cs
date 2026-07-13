using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using log4net;
using Macromill.QCWeb.ReportRequest;
using NPOI.SS;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using NPOI.XSSF.UserModel;
using static NPOI.SS.Util.SheetUtil;

namespace Qc4Launcher.Logic
{
    internal class NpoiHelper
    {
        public static int MAX_CELL_CHAR_LENGTH = 32767;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static XSSFName getNamedRangeNpoi(XSSFSheet formatSheetNpoi, string cname)
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

        internal static CellReference[] getNamedRangeAreaNpoi(XSSFSheet formatSheetNpoi, string cname)
        {
            XSSFName aNamedCell = getNamedRangeNpoi(formatSheetNpoi, cname);
            return getNamedRangeAreaNpoi(aNamedCell);
        }

        internal static CellReference[] getNamedRangeAreaNpoi(XSSFName aNamedCell)
        {
            AreaReference aref = new AreaReference(aNamedCell.RefersToFormula, SpreadsheetVersion.EXCEL2007);
            CellReference[] crefs = aref.GetAllReferencedCells();
            return crefs;
        }


        internal static List<XSSFRow> getNamedRangeRowsNpoi(XSSFSheet formatSheetNpoi, string cname)
        {
            XSSFWorkbook wb = formatSheetNpoi.GetWorkbook();
            XSSFName aNamedCell = getNamedRangeNpoi(formatSheetNpoi, cname);
            AreaReference aref = new AreaReference(aNamedCell.RefersToFormula, SpreadsheetVersion.EXCEL2007);
            List<XSSFRow> rows = new List<XSSFRow>();
            CellReference[] crefs = aref.GetAllReferencedCells();
            if (crefs.Rank > 1) return rows;
            HashSet<int> rowsAdded = new HashSet<int>();
            for (int i = 0; i < crefs.Length; i++)
            {
                if (rowsAdded.Contains(crefs[i].Row)) continue;
                rowsAdded.Add(crefs[i].Row);
                XSSFSheet s = wb.GetSheet(crefs[i].SheetName) as XSSFSheet;
                XSSFRow r = s.GetRow(crefs[i].Row) as XSSFRow;
                if (null == r)
                {
                    r = s.CreateRow(crefs[i].Row) as XSSFRow;
                }
                rows.Add(r);
            }
            return rows;
        }

        internal static List<XSSFRow> getRowsNpoi(XSSFSheet formatSheetNpoi, int start, int end)
        {
            List<XSSFRow> rows = new List<XSSFRow>();
            for (int i = start - 1; i < end; i++)
            {
                XSSFRow r = formatSheetNpoi.GetRow(i) as XSSFRow;
                if (null == r)
                {
                    continue;
                }
                rows.Add(r);
            }
            return rows;
        }

        internal static List<XSSFRow> getNamedRangeRowsNpoi(XSSFSheet formatSheetNpoi, XSSFName aNamedCell)
        {
            XSSFWorkbook wb = formatSheetNpoi.GetWorkbook();
            AreaReference aref = new AreaReference(aNamedCell.RefersToFormula, SpreadsheetVersion.EXCEL2007);
            CellReference[] crefs = aref.GetAllReferencedCells();
            List<XSSFRow> rows = new List<XSSFRow>();
            for (int i = 0; i < crefs.Length; i++)
            {
                XSSFSheet s = wb.GetSheet(crefs[i].SheetName) as XSSFSheet;
                XSSFRow r = s.GetRow(crefs[i].Row) as XSSFRow;
                rows.Add(r);
            }
            return rows;
        }


        internal static void copyRows(XSSFSheet targetSheet, XSSFSheet sourSheetNpoi, string cname, int targetStartRow)
        {
            List<XSSFRow> rows = getNamedRangeRowsNpoi(sourSheetNpoi, cname);
            //for making zero index based
            targetSheet.CopyRows(rows, targetStartRow - 1, new CellCopyPolicy());
        }

        //internal static void copyRows(XSSFSheet targetSheet, XSSFSheet sourSheetNpoi, int startRow, int endRow, int startRowT)
        //{
        //    List<XSSFRow> rows = getRowsNpoi(sourSheetNpoi, startRow, endRow);
        //    //for making zero index based
        //    targetSheet.CopyRows(rows, startRowT - 1, new CellCopyPolicy());
        //}

        internal static void copyRows(XSSFSheet targetSheet, XSSFSheet sourSheetNpoi, int startRow, int endRow)
        {
            List<XSSFRow> rows = getRowsNpoi(sourSheetNpoi, startRow, endRow);
            //for making zero index based
            targetSheet.CopyRows(rows, startRow - 1, new CellCopyPolicy());
        }

        internal static void copyRows(XSSFSheet targetSheet, XSSFSheet sourSheetNpoi, int targetStartRow, int sourceStartRow, int cnt)
        {
            List<XSSFRow> rows = getRows(sourSheetNpoi, sourceStartRow, cnt);
            //for making zero index based
            targetSheet.CopyRows(rows, targetStartRow - 1, new CellCopyPolicy());
        }

        internal static void copyRowsWithoutMerge(XSSFSheet targetSheet, XSSFSheet sourSheetNpoi, int targetStartRow, int sourceStartRow, int cnt)
        {
            List<XSSFRow> rows = getRows(sourSheetNpoi, sourceStartRow, cnt);
            //for making zero index based
            CellCopyPolicy cp = new CellCopyPolicy();
            cp.IsCopyMergedRegions = false;
            targetSheet.CopyRows(rows, targetStartRow - 1, cp);
        }

        private static List<XSSFRow> getRows(XSSFSheet sourSheetNpoi, int sourceStartRow, int cnt)
        {
            List<XSSFRow> rows = new List<XSSFRow>();
            //for making zero index based
            sourceStartRow--;
            for (int i = 0; i < cnt; i++)
            {
                XSSFRow r = sourSheetNpoi.GetRow(sourceStartRow + i) as XSSFRow;
                rows.Add(r);
            }
            return rows;
        }

        internal static void copyRows(int mergeStart, int mergeOffset, XSSFSheet targetSheet,
            XSSFSheet sourSheetNpoi, int startRow, int endRow, int startTarget = 0, bool individualCross = false)
        {
            List<XSSFRow> rows = getRowsNpoi(sourSheetNpoi, startRow, endRow);

            CellCopyPolicy p = new CellCopyPolicy();
            //for making zero index based
            //targetSheet.CopyRows(rows, startRow - 1, p);

            p.IsCopyMergedRegions = (false);
            p.IsCopyRowHeight = false;
            startTarget = startTarget == 0 ? startRow : startTarget;
            int r = startTarget - 1;
            int destRowNum;
            foreach (IRow srcRow in rows)
            {
                destRowNum = r++;
                //removeRow(destRowNum); //this probably clears all external formula references to destRow, causing unwanted #REF! errors
                XSSFRow destRow = targetSheet.CreateRow(destRowNum) as XSSFRow;
                if (srcRow.RowNum < mergeStart)
                {
                    p.IsCopyMergedRegions = (true);
                }
                else
                {
                    p.IsCopyMergedRegions = (false);
                }
                destRow.CopyRowFrom(srcRow, p);
                if (srcRow.HeightInPoints != 15)
                    destRow.HeightInPoints = srcRow.HeightInPoints;
                else if (individualCross)
                    destRow.HeightInPoints = destRow.HeightInPoints;

            }
            // FIXME: is this something that rowShifter could be doing?
            foreach (CellRangeAddress srcRegion in sourSheetNpoi.MergedRegions)
            {
                if (mergeStart <= srcRegion.FirstRow && srcRegion.LastRow <= endRow)
                {
                    // srcRegion is fully inside the copied rows
                    CellRangeAddress destRegion = srcRegion.Copy();
                    destRegion.FirstRow = (destRegion.FirstRow) - mergeOffset + startTarget - 1;
                    destRegion.LastRow = (destRegion.LastRow) - mergeOffset + startTarget - 1;
                    //try
                    //{
                    targetSheet.AddMergedRegion(destRegion);
                    //}
                    //catch (Exception ex)
                    //{
                    //    _log.Info("MERGE ERROR:" + ex.Message);
                    //}
                }
            }
        }

        internal static void copyRows(XSSFSheet targetSheet, List<XSSFRow> rows, int targetStartRow)
        {
            targetSheet.CopyRows(rows, targetStartRow - 1, new CellCopyPolicy());
        }

        internal static void saveTmp(XSSFWorkbook formatBookNpoi, bool open = false)
        {
            string path = "test_" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".xlsx";
            formatBookNpoi.Write(new FileStream(path, FileMode.Create, FileAccess.ReadWrite));
            if (open)
                Process.Start(path);
        }

        private static void saveTmp(XSSFSheet sheet)
        {
            saveTmp(sheet.GetWorkbook());
        }


        public static void deleteSingleColumnName(XSSFSheet sheet, string cname)
        {

            XSSFWorkbook wb = sheet.GetWorkbook();
            XSSFName aNamedCell = getNamedRangeNpoi(sheet, cname);
            CellRangeAddress cellRange = CellRangeAddress.ValueOf(aNamedCell.RefersToFormula);
            deleteSingleColumnName(sheet, cellRange);
        }

        public static void deleteSingleColumnName(XSSFSheet sheet, CellRangeAddress cellRange)
        {

            int maxColumn = 0;
            int columnToDelete = cellRange.FirstColumn;
            for (int r = cellRange.FirstRow; r < cellRange.LastRow + 1; r++)
            {
                XSSFRow row = sheet.GetRow(r) as XSSFRow;

                // if no row exists here; then nothing to do; next!
                if (row == null)
                    continue;

                // if the row doesn't have this many columns then we are good; next!
                int lastColumn = row.LastCellNum;
                if (lastColumn > maxColumn)
                    maxColumn = lastColumn;

                if (lastColumn < columnToDelete)
                    continue;

                for (int x = columnToDelete + 1; x < lastColumn + 1; x++)
                {
                    XSSFCell oldCell = row.GetCell(x - 1) as XSSFCell;
                    if (oldCell != null)
                        row.RemoveCell(oldCell);

                    XSSFCell nextCell = row.GetCell(x) as XSSFCell;
                    if (nextCell != null)
                    {
                        XSSFCell newCell = row.CreateCell(x - 1, nextCell.CellType) as XSSFCell;
                        cloneCell(newCell, nextCell);
                    }
                }
            }
            //    // Adjust the column widths
            //    //for (int c = 0; c < maxColumn; c++)
            //    //{
            //    //    sheet.SetColumnWidth(c, sheet.GetColumnWidth(c + 1));
            //    //}
        }

        public static void deleteBaseSheetSingleColumnName(XSSFSheet sheet, CellRangeAddress cellRange)
        {

            int maxColumn = 0;
            XSSFWorkbook wb = sheet.GetWorkbook();
            int columnToDelete = cellRange.FirstColumn;
            for (int r = cellRange.FirstRow; r < cellRange.LastRow + 1; r++)
            {
                XSSFRow row = sheet.GetRow(r) as XSSFRow;

                // if no row exists here; then nothing to do; next!
                if (row == null)
                    continue;

                // if the row doesn't have this many columns then we are good; next!
                int lastColumn = row.LastCellNum;
                if (lastColumn > maxColumn)
                    maxColumn = lastColumn;

                if (lastColumn < columnToDelete)
                    continue;

                for (int x = columnToDelete + 1; x < lastColumn + 1; x++)
                {
                    XSSFCell oldCell = row.GetCell(x - 1) as XSSFCell;
                    if (oldCell != null)
                        row.RemoveCell(oldCell);

                    XSSFCell nextCell = row.GetCell(x) as XSSFCell;
                    if (nextCell != null)
                    {
                        XSSFCell newCell = row.CreateCell(x - 1, nextCell.CellType) as XSSFCell;
                        cloneCell(newCell, nextCell);
                    }
                }
            }
        }

        public static void deleteColumn(XSSFSheet sheet, int columnToDelete)
        {

            int maxColumn = 0;
            for (int r = 0; r < sheet.LastRowNum + 1; r++)
            {
                XSSFRow row = sheet.GetRow(r) as XSSFRow;

                // if no row exists here; then nothing to do; next!
                if (row == null)
                    continue;

                // if the row doesn't have this many columns then we are good; next!
                int lastColumn = row.LastCellNum;
                if (lastColumn > maxColumn)
                    maxColumn = lastColumn;

                if (lastColumn < columnToDelete)
                    continue;

                for (int x = columnToDelete + 1; x < lastColumn + 1; x++)
                {
                    XSSFCell oldCell = row.GetCell(x - 1) as XSSFCell;
                    if (oldCell != null)
                        row.RemoveCell(oldCell);

                    XSSFCell nextCell = row.GetCell(x) as XSSFCell;
                    if (nextCell != null)
                    {
                        XSSFCell newCell = row.CreateCell(x - 1, nextCell.CellType) as XSSFCell;
                        cloneCell(newCell, nextCell);
                    }
                }
            }
            //    // Adjust the column widths
            //    //for (int c = 0; c < maxColumn; c++)
            //    //{
            //    //    sheet.SetColumnWidth(c, sheet.GetColumnWidth(c + 1));
            //    //}
        }

        public static void copyColumn(XSSFSheet sheet, int columnToCopy, int numberofCols)
        {
            for (int r = 0; r < numberofCols; r++)
            {
                copyColumn(sheet, columnToCopy, 0, sheet.LastRowNum);
            }
        }

        public static void copyColumn(XSSFSheet sheet, CellRangeAddress cellRange, int numberofCols, BackgroundWorker bgWorker)
        {
            for (int r = 0; r < numberofCols; r++)
            {
                if (bgWorker.CancellationPending) return;
                copyColumn(sheet, cellRange.FirstColumn, cellRange.FirstRow, cellRange.LastRow);
            }
        }


        public static void copyColumn(XSSFSheet sheet, int columnToCopy, int startRow, int endRow)
        {

            for (int r = startRow; r < endRow + 1; r++)
            {
                XSSFRow row = sheet.GetRow(r) as XSSFRow;

                if (row == null)
                    continue;

                int lastColumn = row.LastCellNum;

                if (lastColumn < columnToCopy)
                    continue;

                for (int x = lastColumn + 1; x > columnToCopy; x--)
                {
                    XSSFCell oldCell = row.GetCell(x - 1) as XSSFCell;
                    if (oldCell != null)
                    {
                        //row.RemoveCell(oldCell);
                        XSSFCell nextCell = row.CreateCell(x, oldCell.CellType) as XSSFCell;
                        cloneCell(nextCell, oldCell);
                    }
                }
            }
            //    // Adjust the column widths
            //    //for (int c = 0; c < maxColumn; c++)
            //    //{
            //    //    sheet.SetColumnWidth(c, sheet.GetColumnWidth(c + 1));
            //    //}
        }
        /*
         * Takes an existing Cell and merges all the styles and forumla
         * into the new one
         */
        private static void cloneCell(XSSFCell cNew, XSSFCell cOld)
        {
            cNew.CellComment = cOld.CellComment;
            cNew.CellStyle = cOld.CellStyle;

            switch (cNew.CellType)
            {
                case CellType.Boolean:
                    {
                        cNew.SetCellValue(cOld.BooleanCellValue);
                        break;
                    }
                case CellType.Numeric:
                    {
                        cNew.SetCellValue(cOld.NumericCellValue);
                        break;
                    }
                case CellType.String:
                    {
                        cNew.SetCellValue(cOld.StringCellValue);
                        break;
                    }
                case CellType.Error:
                    {
                        cNew.SetCellValue(cOld.ErrorCellValue);
                        break;
                    }
                case CellType.Formula:
                    {
                        cNew.SetCellFormula(cOld.CellFormula);
                        break;
                    }
            }

        }

        internal static void applyStyles(XSSFSheet formatSheetNpoi, CellRangeAddress arefSrcc, CellRangeAddress arefc)
        {
            AreaReference arefSrc = new AreaReference(arefSrcc.FormatAsString(), SpreadsheetVersion.EXCEL2007);
            AreaReference aref = new AreaReference(arefc.FormatAsString(), SpreadsheetVersion.EXCEL2007);
            CellReference[] crefs = aref.GetAllReferencedCells();
            CellReference[] crefsSrc = arefSrc.GetAllReferencedCells();
            for (int i = 0; i < crefs.Length; i++)
            {
                XSSFRow r = formatSheetNpoi.GetRow(crefs[i].Row) as XSSFRow;
                if (r == null) continue;
                XSSFCell c = r.GetCell(crefs[i].Col) as XSSFCell;

                XSSFRow rSrc = formatSheetNpoi.GetRow(crefsSrc[i].Row) as XSSFRow;
                if (rSrc == null) continue;
                XSSFCell cSrc = rSrc.GetCell(crefsSrc[i].Col) as XSSFCell;

                XSSFCellStyle styleSrc = cSrc.CellStyle as XSSFCellStyle;
                c.CellStyle = styleSrc;
            }
        }

        internal static void removeRows(XSSFSheet workingSheetNpoi, int r, int d)
        {
            for (int i = 0; i < d; i++)
            {
                IRow row = workingSheetNpoi.GetRow(r - 1);
                //if(null != row){ 
                //     workingSheetNpoi.RemoveRow(row); 
                //}
                workingSheetNpoi.ShiftRows(r, workingSheetNpoi.LastRowNum, -1);
            }
        }

        internal static void copyRowsByshiftDown(XSSFSheet targetSheet, XSSFSheet sourSheetNpoi, int targetStartRow, int sourceStartRow, int cnt, int times)
        {
            for (int i = 0; i < times; i++)
            {
                targetSheet.ShiftRows(targetStartRow + i, targetSheet.LastRowNum, cnt);
                copyRows(targetSheet, sourSheetNpoi, targetStartRow + cnt + i, sourceStartRow, cnt);
            }
        }

        internal static void PutValue(XSSFSheet workingSheetNpoi, string address, Array tmpArray, bool fromMulti = false, IDictionary<XSSFCellStyle, XSSFCellStyle> quotedStyles = null)
        {
            AreaReference aref = new AreaReference(address, SpreadsheetVersion.EXCEL2007);
            CellReference[] crefs = aref.GetAllReferencedCells();
            int row = crefs[0].Row;
            int col = crefs[0].Col;
            int i, j;
            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++, row++)
            {
                int colLocal = col;
                for (j = tmpArray.GetLowerBound(1); j <= tmpArray.GetUpperBound(1); j++, colLocal++)
                {
                    if (tmpArray.GetValue(i, j) != null)
                    {
                        object val = tmpArray.GetValue(i, j);
                        XSSFRow r = workingSheetNpoi.GetRow(row) as XSSFRow;
                        if (r == null) continue;
                        XSSFCell c = r.GetCell(colLocal) as XSSFCell;
                        if (c == null) continue;
                        if (val.GetType() == typeof(string))
                        {
                            string s = (string)tmpArray.GetValue(i, j);
                            if (s.Length > MAX_CELL_CHAR_LENGTH)
                            {
                                s = s.Substring(0, MAX_CELL_CHAR_LENGTH);
                            }
                            c.SetCellValue(s);
                            if (fromMulti && Regex.IsMatch(s, @"^['=+@-]"))
                            {
                                XSSFCellStyle cs = c.CellStyle as XSSFCellStyle;
                                XSSFCellStyle qs;
                                if (!quotedStyles.ContainsKey(cs))
                                {
                                    qs = workingSheetNpoi.Workbook.CreateCellStyle() as XSSFCellStyle;
                                    qs.CloneStyleFrom(cs);
                                    qs.QuotePrefix(true);
                                    quotedStyles.Add(cs, qs);
                                }
                                else
                                {
                                    qs = quotedStyles[cs];
                                }
                                c.CellStyle = qs;
                            }
                        }
                        else if (val.GetType() == typeof(double))
                        {
                            c.SetCellValue((double)tmpArray.GetValue(i, j));

                        }
                        else if (val.GetType() == typeof(int))
                        {
                            c.SetCellValue((int)tmpArray.GetValue(i, j));

                        }
                        else
                        {
                            string s = (string)tmpArray.GetValue(i, j);
                            if (s.Length > MAX_CELL_CHAR_LENGTH)
                            {
                                s = s.Substring(0, MAX_CELL_CHAR_LENGTH);
                            }
                            c.SetCellValue(s);
                        }
                    }
                }
            }

        }

        internal static ISheet createBaseSheet(XSSFWorkbook FormatBook)
        {
            int idx = FormatBook.GetSheetIndex("Base");
            if (idx > -1)
                FormatBook.RemoveAt(idx);
            FormatBook.CreateSheet();
            FormatBook.SetSheetName((FormatBook.NumberOfSheets - 1), "Base");
            return FormatBook.GetSheet("Base");
        }
        internal static void PutValueNumeric(XSSFSheet workingSheetNpoi, string address)
        {
            AreaReference aref = new AreaReference(address, SpreadsheetVersion.EXCEL2007);
            CellReference[] crefs = aref.GetAllReferencedCells();
            CellRangeAddress CR = CellRangeAddress.ValueOf(address);
            int row = crefs[0].Row;
            int col = crefs[0].Col;
            int i, j;
            for (i = CR.FirstRow; i <= CR.LastRow; i++, row++)
            {
                int colLocal = col;
                for (j = CR.FirstColumn; j <= CR.LastColumn; j++, colLocal++)
                {
                    XSSFRow r = workingSheetNpoi.GetRow(row) as XSSFRow;
                    if (r == null) continue;
                    XSSFCell c = r.GetCell(colLocal) as XSSFCell;
                    if (c == null) continue;
                    string val = c.StringCellValue;
                    if (string.IsNullOrEmpty(val)) continue;
                    c.SetCellValue(Convert.ToDouble(val));
                }
            }

        }

        public static void AutoFitExCorssSub(CellRangeAddress column, TableType TableType, XSSFSheet sht)
        {
            double h = 0;
            double w = 0;
            double tmpH;
            Array v;
            Array u;
            List<CellRangeAddress> listMrg = new List<CellRangeAddress>();
            Dictionary<int, int> mapMrg = new Dictionary<int, int>();
            Dictionary<int, int> mapColMrg = new Dictionary<int, int>();

            foreach (CellRangeAddress srcRegion in sht.MergedRegions)
            {
                if (srcRegion.FirstRow >= column.FirstRow && srcRegion.LastRow <= column.LastRow &&
                    column.FirstColumn == srcRegion.FirstColumn && column.LastColumn == srcRegion.LastColumn)
                {
                    mapMrg[srcRegion.FirstRow] = srcRegion.LastRow - srcRegion.FirstRow + 1;
                }
                if (srcRegion.FirstRow >= column.FirstRow && srcRegion.LastRow <= column.LastRow &&
                    (column.LastColumn == srcRegion.LastColumn || column.LastColumn + 1 == srcRegion.LastColumn))
                {
                    if (column.LastColumn + 1 == srcRegion.LastColumn && srcRegion.LastColumn - srcRegion.FirstColumn == 0) { continue; }//skippping test column in label part
                    mapColMrg[srcRegion.FirstRow] = srcRegion.LastColumn - srcRegion.FirstColumn + 1;
                    mapMrg[srcRegion.FirstRow] = srcRegion.LastRow - srcRegion.FirstRow + 1;
                }
            }
            for (int r = column.FirstRow; r <= column.LastRow;)
            {
                if (mapMrg.ContainsKey(r) || mapColMrg.ContainsKey(r))
                {
                    IRow row = sht.GetRow(r);
                    int rowCnt = 1;
                    if (mapMrg.ContainsKey(r))
                    {
                        rowCnt = mapMrg[r];
                    }
                    int colCnt = 1;
                    if (mapColMrg.ContainsKey(r))
                    {
                        colCnt = mapColMrg[r];
                    }
                    ICell cell = row.GetCell(column.FirstColumn);
                    if (cell == null)
                    {
                        r += rowCnt;
                        continue;
                    }

                    h = row.HeightInPoints * rowCnt;
                    w = 0;
                    int colIdx = colCnt > 2 && TableType == TableType.SignificanceTest ? column.FirstColumn + 1 : column.FirstColumn;
                    for (int j = 0; j < colCnt; j++)
                    {
                        int colw = sht.GetColumnWidth(colIdx - j);
                        double wdth = colw == 0 ? OutputUtil.widthDef : (colw / 256.0);
                        w = w + wdth;
                    }
                    w = w - (OutputUtil.widthDef - OutputUtil.widthDefWithoutMrgn);
                    string val = cell.StringCellValue;
                    if (colCnt > 1)
                    {
                        ICell cellc = row.GetCell(colIdx - colCnt + 1);
                        if (cellc != null)
                        {
                            val = cellc.StringCellValue;
                        }
                    }

                    if (val != null)
                    {
                        w = Math.Floor(w);
                        double lines = findWidth(val, w);
                        tmpH = (lines) * row.Sheet.DefaultRowHeightInPoints;
                        if (tmpH > h)
                            h = tmpH;
                    }

                    if (h > CrossCreator.ROW_MAX_HEIGHT)
                        h = CrossCreator.ROW_MAX_HEIGHT;

                    int px = Units.PointsToPixel(h);
                    double pxH = Math.Ceiling((float)px/rowCnt);
                    double hI = Units.PixelToPoints((int)pxH);
                    row.HeightInPoints = (float)hI;
                    if (rowCnt > 1)
                    {
                        row = sht.GetRow(r + 1);
                        row.HeightInPoints = (float)hI;
                    }
                    r += rowCnt;
                }
                else
                {
                    r++;
                }

            }

        }

        internal static void autoFitSingleRow(IRow row, bool hasNoMergeCol = true)
        {
            double ht = row.HeightInPoints;
            double ht2 = getRowHeight(row);
            if (ht > ht2)
            {
                ht2 = ht;
            }
            else
            {
                if (hasNoMergeCol)
                {
                    ht2 = -1;
                }
            }
            row.HeightInPoints = (float)ht2;
        }

        private static double getRowHeight(IRow row)
        {
            double maxLines = 0;
            List<CellRangeAddress> listMrg = new List<CellRangeAddress>();
            XSSFSheet sht = row.Sheet as XSSFSheet;
            foreach (CellRangeAddress srcRegion in sht.MergedRegions)
            {
                if (row.RowNum == srcRegion.FirstRow && srcRegion.LastRow == row.RowNum)
                {
                    listMrg.Add(srcRegion);
                }
            }
            for (int i = row.FirstCellNum; i <= row.LastCellNum; i++)
            {
                ICell cell = row.GetCell(i - 1);
                if (cell == null)
                {
                    continue;
                }
                string val = cell.StringCellValue;
                int colw = row.Sheet.GetColumnWidth(i - 1);
                double wdth = colw == 0 ? OutputUtil.widthDefWithoutMrgn : (colw / 256.0) - (OutputUtil.widthDef - OutputUtil.widthDefWithoutMrgn);
                foreach (CellRangeAddress srcRegion in listMrg)
                {
                    if (i - 1 == srcRegion.FirstColumn)
                    {
                        wdth = 0;
                        for (int k = srcRegion.FirstColumn; k <= srcRegion.LastColumn; k++)
                        {
                            colw = row.Sheet.GetColumnWidth(i - 1);
                            wdth += colw == 0 ? OutputUtil.widthDef : (colw / 256.0);
                            i++;
                        }
                        wdth = wdth - (OutputUtil.widthDef - OutputUtil.widthDefWithoutMrgn);
                    }
                }

                wdth = Math.Floor(wdth);
                double lines = findWidth(val, wdth);
                if (maxLines < lines)
                {
                    maxLines = lines;
                }
            }
            double maxLimitHeightInPoints = CrossCreator.ROW_MAX_HEIGHT;
            if (maxLimitHeightInPoints < maxLines * row.Sheet.DefaultRowHeightInPoints)
            {
                return maxLimitHeightInPoints;
            }
            return maxLines * row.Sheet.DefaultRowHeightInPoints;
        }

        private static double findWidth(string val, double colWidth)
        {
            const int MaxAnsiCode = 255;
            double width = 0;
            val = Regex.Replace(val, @"\n|\r|\r\n", "\r");
            val = Regex.Replace(val, @"\t", "");
            double lines = 0;
            double extrLineForSpace = 0;
            char prevChar = ' ';
            double line;
            double widthDef = OutputUtil.charwidthDef;
            double widthDefAsci = OutputUtil.charwidthDefAsci;
            foreach (char c in val)
            {
                if (c == '\r')
                {
                    line = Math.Ceiling(width / colWidth);
                    if (line == 0) line = 1;
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
        internal static void AutoFit(ISheet tableSheet, int firtColumn, int lastColumn, int firtRow, int lastRow)
        {
            for (int i = firtColumn - 1; i <= lastColumn; i++)
            {
                //double widthCalc = SheetUtil.GetColumnWidth(tableSheet, i, false, firtRow - 1, lastRow);
                double widthCalc = GetColumnWidth(tableSheet, i, firtRow - 1, lastRow);
                if (widthCalc > 255)
                {
                    widthCalc = 0;
                }
                widthCalc *= 256;              
                double width = tableSheet.GetColumnWidth(i);
                if (widthCalc < OutputUtil.widthDef * 256)
                {
                    widthCalc = OutputUtil.widthDef * 256;
                }
                if (width < widthCalc)
                {
                    tableSheet.SetColumnWidth(i, (int)(widthCalc));
                }
            }
        }

        private static double GetColumnWidth(ISheet sheet, int column, int firstRow, int lastRow)
        {
            int defaultCharWidth = SheetUtil.GetDefaultCharWidth(sheet.Workbook);
            IFormulaEvaluator dummyEvaluator = new DummyEvaluator();
            DataFormatter formatter = new DataFormatter();
            double width = -1;
            for (int rowIdx = firstRow; rowIdx <= lastRow; ++rowIdx)
            {
                IRow row = sheet.GetRow(rowIdx);
                if (row != null)
                {
                    ICell cell = row.GetCell(column);
                    if (null == cell) { continue; }
                    string sval = formatter.FormatCellValue(cell, dummyEvaluator);
                    width = Math.Max(width, sval.Length + 2);
                }
            }
            return width * OutputUtil.charwidthDefAsci;
        }

        internal static void CopyRowStyle(IRow row, IRow rowDest)
        {
            for (int col = row.FirstCellNum; col <= row.LastCellNum; col++)
            {
                ICell cell = row.GetCell(col);
                ICell cellDest = rowDest.GetCell(col);
                if (cell == null || cellDest == null)
                {
                    continue;
                }
                cellDest.CellStyle = cell.CellStyle;
            }
        }

        //Added Portrait

        internal static void ShiftRowUp(XSSFSheet workingSheetNpoi, int rowToDelete)
        {
            workingSheetNpoi.ShiftRows(rowToDelete, workingSheetNpoi.LastRowNum, -1);
        }

        public static void UnmergeSelectedRegion(XSSFSheet baseSheet, String regionFormula)
        {
            List<CellRangeAddress> mergedRegions = baseSheet.MergedRegions;
            for (int index = 0; index < baseSheet.NumMergedRegions; index++)
            {
                if (regionFormula.Equals(mergedRegions[index].FormatAsString()))
                {
                    baseSheet.RemoveMergedRegion(index);
                    break;
                }
            }
        }

        internal static void RemoveNumberOfRows(XSSFSheet workingSheetNpoi, int rowToDelete, int numberOfRows)
        {
            IRow row = workingSheetNpoi.GetRow(rowToDelete - 1);
            rowToDelete = rowToDelete + numberOfRows - 1;
            for (int i = 0; i < numberOfRows; i++)
            {
                if (rowToDelete != workingSheetNpoi.LastRowNum + 1)
                {
                    workingSheetNpoi.ShiftRows(rowToDelete, workingSheetNpoi.LastRowNum, -1);
                    rowToDelete = rowToDelete - 1;
                }
                else
                {
                    workingSheetNpoi.ShiftRows(rowToDelete, workingSheetNpoi.LastRowNum + 1, -1);
                }
            }
        }

        public static void AddMergedRegion(XSSFSheet baseSheet, CellRangeAddress cellrange)
        {
            if (cellrange.NumberOfCells != 1)
                baseSheet.AddMergedRegion(cellrange);
        }

        public static void deleteColumn(XSSFSheet sheet, CellRangeAddress cellRange)
        {
            for (int i = cellRange.LastColumn + 1; i >= cellRange.FirstColumn + 1; i--)
            {
                NpoiHelper.deleteColumn(sheet as XSSFSheet, i);
            }
        }

        public static int GetIndexIfCellIsInMergedCells(ISheet sheet, int row, int column)
        {
            int numberOfMergedRegions = sheet.NumMergedRegions;
            for (int i = 0; i < numberOfMergedRegions; i++)
            {
                CellRangeAddress mergedCell = sheet.GetMergedRegion(i);

                if (mergedCell.IsInRange(row, column))
                {
                    return i;
                }
            }
            return -1;
        }

        public static void CopyRange(CellRangeAddress range, ISheet sourceSheet, ISheet destinationSheet, int destinationStartRow, int destinationColumnNumber)
        {
            for (var rowNum = range.FirstRow; rowNum <= range.LastRow; rowNum++)
            {
                IRow sourceRow = sourceSheet.GetRow(rowNum);

                if (destinationSheet.GetRow(destinationStartRow) == null)
                    destinationSheet.CreateRow(destinationStartRow);

                if (sourceRow != null)
                {
                    IRow destinationRow = destinationSheet.GetRow(destinationStartRow);
                    int lastCell = destinationColumnNumber;
                    for (var col = range.FirstColumn; col < sourceRow.LastCellNum && col <= range.LastColumn; col++)
                    {
                        destinationRow.CreateCell(lastCell);
                        CopyCell(sourceRow.GetCell(col), destinationRow.GetCell(lastCell));
                        lastCell++;
                    }
                }
                destinationStartRow++;
            }
        }

        public static void CopyCell(ICell source, ICell destination)
        {
            if (destination != null && source != null)
            {
                //you can comment these out if you don't want to copy the style ...
                destination.CellComment = source.CellComment;
                destination.CellStyle = source.CellStyle;
                destination.Hyperlink = source.Hyperlink;

                switch (source.CellType)
                {
                    case CellType.Formula:
                        destination.CellFormula = source.CellFormula; break;
                    case CellType.Numeric:
                        destination.SetCellValue(source.NumericCellValue); break;
                    case CellType.String:
                        destination.SetCellValue(source.StringCellValue); break;
                }
            }
        }

        internal static void ShiftCellsUp(XSSFSheet sheet, CellRangeAddress cellRange, int numberOfShift)
        {
            if (numberOfShift < 1)
            {
                cellRange.LastRow -= 1;
                cellRange.FirstRow -= 1;
                ShiftCellsUp(sheet, cellRange);
            }
            else
            {
                for (int r = cellRange.FirstColumn; r < cellRange.LastColumn + 1; r++)
                {
                    for (int x = cellRange.LastRow; x >= cellRange.FirstRow; x--)
                    {
                        XSSFRow row = sheet.GetRow(x) as XSSFRow;
                        if (row == null)
                            continue;
                        int rownumb = 1;
                        while (rownumb < numberOfShift)
                        {
                            XSSFRow row2 = sheet.GetRow(x + rownumb) as XSSFRow;

                            XSSFCell cellToRemove = row.GetCell(r) as XSSFCell;
                            if (cellToRemove != null)
                                row.RemoveCell(cellToRemove);

                            XSSFCell nextCell = row2.GetCell(r) as XSSFCell;
                            if (nextCell != null)
                            {
                                XSSFCell newCell = row.CreateCell(r, nextCell.CellType) as XSSFCell;
                                cloneCell(newCell, nextCell);
                                row2.RemoveCell(nextCell);
                            }
                            rownumb++;
                            row = row2;
                        }
                    }
                }
            }
        }

        internal static void ShiftCellsUp(XSSFSheet sheet, CellRangeAddress cellRange)
        {
            for (int r = cellRange.FirstColumn; r < cellRange.LastColumn + 1; r++)
            {
                for (int x = cellRange.LastRow; x >= cellRange.FirstRow; x--)
                {
                    XSSFRow row = sheet.GetRow(x) as XSSFRow;
                    XSSFRow row2 = sheet.GetRow(x + 1) as XSSFRow;
                    if (row == null)
                        continue;

                    XSSFCell cellToRemove = row.GetCell(r) as XSSFCell;
                    if (cellToRemove != null)
                        row.RemoveCell(cellToRemove);

                    XSSFCell nextCell = row2.GetCell(r) as XSSFCell;
                    if (nextCell != null)
                    {
                        XSSFCell newCell = row.CreateCell(r, nextCell.CellType) as XSSFCell;
                        cloneCell(newCell, nextCell);
                        row2.RemoveCell(nextCell);
                    }
                }
            }
        }

        internal static void DeleteAndRemoveRow(XSSFSheet workingSheetNpoi, int rowToDelete, int numberOfRows)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                workingSheetNpoi.ShiftRows(rowToDelete, workingSheetNpoi.LastRowNum, -1);
                rowToDelete = rowToDelete - 1;
            }
        }

        internal static void ShiftRowUp(XSSFSheet workingSheetNpoi, int startRow, int numberOfShift)
        {
            if (startRow <= workingSheetNpoi.LastRowNum)
                workingSheetNpoi.ShiftRows(startRow, workingSheetNpoi.LastRowNum, -numberOfShift);
            else
            {
                workingSheetNpoi.ShiftRows(startRow, startRow, -numberOfShift);
            }
        }

        public static void CopyRangeofCells(CellRangeAddress range, ISheet sourceSheet, int destinationStartRow, int destinationColumn,BorderStyle bottomBorderStyle)
        {
            for (var rowNum = range.FirstRow; rowNum <= range.LastRow; rowNum++)
            {
                IRow sourceRow = sourceSheet.GetRow(rowNum);

                if (sourceSheet.GetRow(destinationStartRow) == null)
                    sourceSheet.CreateRow(destinationStartRow);

                if (sourceRow != null)
                {
                    IRow row = sourceSheet.GetRow(destinationStartRow);
                    int lastCell = destinationColumn;
                    for (var col = range.FirstColumn; col < sourceRow.LastCellNum && col <= range.LastColumn; col++)
                    {
                        row.CreateCell(lastCell);

                        CopyCell(sourceRow.GetCell(col), row.GetCell(lastCell));
                        if (sourceRow.GetCell(col).CellStyle.BorderBottom != BorderStyle.None)
                            row.GetCell(lastCell).CellStyle.BorderBottom = BorderStyle.Hair;
                        lastCell++;
                    }
                }
                destinationStartRow++;
            }
        }

        public static void deleteColumn(XSSFSheet sheet, int columnToDelete, int numberOfColumns)
        {
            for (int n = 1; n <= numberOfColumns; n++)
            {
                NpoiHelper.deleteColumn(sheet, columnToDelete - 1);
            }
        }

        public static void ShiftRow(ISheet sheet, int startRow, int endRow, int shift)
        {
            sheet.ShiftRows(startRow, endRow, shift);
        }

        internal static void PutValuePortraitTable(XSSFSheet workingSheetNpoi, string address, Array tmpArray, bool fromMulti = false, IDictionary<XSSFCellStyle, XSSFCellStyle> quotedStyles = null,int skipRow = 4,int skipRow2 = 7,bool simpleaggr = false)
        {
            AreaReference aref = new AreaReference(address, SpreadsheetVersion.EXCEL2007);
            CellReference[] crefs = aref.GetAllReferencedCells();

            int row = (simpleaggr && fromMulti) ? crefs[0].Row + 2 : crefs[0].Row;
            int col = crefs[0].Col;
            int i, j;
            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++, row++)
            {
                if (i == 5 && fromMulti && simpleaggr)
                    row += - 1;
                int colLocal = col;
                for (j = tmpArray.GetLowerBound(1); j <= tmpArray.GetUpperBound(1); j++, colLocal++)
                {
                    if (tmpArray.GetValue(i, j) != null)
                    {
                        object val = tmpArray.GetValue(i, j);
                        if (row == skipRow || row == skipRow2)
                            row++;
                        if (simpleaggr && row == 8 && !fromMulti)
                            row++;
                        XSSFRow r = workingSheetNpoi.GetRow(row) as XSSFRow;
                        if (r == null) continue;
                        XSSFCell c = r.GetCell(colLocal) as XSSFCell;
                        if (c == null) continue;
                        if (val.GetType() == typeof(string))
                        {
                            string s = (string)tmpArray.GetValue(i, j);
                            if (s.Length > MAX_CELL_CHAR_LENGTH)
                            {
                                s = s.Substring(0, MAX_CELL_CHAR_LENGTH);
                            }
                            c.SetCellValue(s);
                            if (fromMulti && !simpleaggr && Regex.IsMatch(s, @"^['=+@-]"))
                            {
                                XSSFCellStyle cs = c.CellStyle as XSSFCellStyle;
                                XSSFCellStyle qs;
                                if (!quotedStyles.ContainsKey(cs))
                                {
                                    qs = workingSheetNpoi.Workbook.CreateCellStyle() as XSSFCellStyle;
                                    qs.CloneStyleFrom(cs);
                                    qs.QuotePrefix(true);
                                    quotedStyles.Add(cs, qs);
                                }
                                else
                                {
                                    qs = quotedStyles[cs];
                                }
                                c.CellStyle = qs;
                            }
                        }
                        else if (val.GetType() == typeof(double))
                        {
                            c.SetCellValue((double)tmpArray.GetValue(i, j));

                        }
                        else if (val.GetType() == typeof(int))
                        {
                            c.SetCellValue((int)tmpArray.GetValue(i, j));

                        }
                        else
                        {
                            string s = (string)tmpArray.GetValue(i, j);
                            if (s.Length > MAX_CELL_CHAR_LENGTH)
                            {
                                s = s.Substring(0, MAX_CELL_CHAR_LENGTH);
                            }
                            c.SetCellValue(s);
                        }
                    }
                }
            }

        }

        internal static void PutDataValuePortrait(XSSFSheet workingSheetNpoi, string address, Array tmpArray, bool fromMulti = false, IDictionary<XSSFCellStyle, XSSFCellStyle> quotedStyles = null,int skipColumn = 2)
        {
            AreaReference aref = new AreaReference(address, SpreadsheetVersion.EXCEL2007);
            CellReference[] crefs = aref.GetAllReferencedCells();
            int row = crefs[0].Row;
            int col = crefs[0].Col;
            int i, j;
            for (i = tmpArray.GetLowerBound(0); i <= tmpArray.GetUpperBound(0); i++, row++)
            {
                int colLocal = col;
                for (j = tmpArray.GetLowerBound(1); j <= tmpArray.GetUpperBound(1); j++, colLocal++)
                {
                    if (tmpArray.GetValue(i, j) != null && j != skipColumn)
                    {
                        object val = tmpArray.GetValue(i, j);
                        XSSFRow r = workingSheetNpoi.GetRow(row) as XSSFRow;
                        if (r == null) continue;
                        XSSFCell c = r.GetCell(colLocal) as XSSFCell;
                        if (c == null) continue;
                        if (val.GetType() == typeof(string))
                        {
                            string s = (string)tmpArray.GetValue(i, j);
                            if (s.Length > MAX_CELL_CHAR_LENGTH)
                            {
                                s = s.Substring(0, MAX_CELL_CHAR_LENGTH);
                            }
                            c.SetCellValue(s);
                            if (fromMulti && Regex.IsMatch(s, @"^['=+@-]"))
                            {
                                XSSFCellStyle cs = c.CellStyle as XSSFCellStyle;
                                XSSFCellStyle qs;
                                if (!quotedStyles.ContainsKey(cs))
                                {
                                    qs = workingSheetNpoi.Workbook.CreateCellStyle() as XSSFCellStyle;
                                    qs.CloneStyleFrom(cs);
                                    qs.QuotePrefix(true);
                                    quotedStyles.Add(cs, qs);
                                }
                                else
                                {
                                    qs = quotedStyles[cs];
                                }
                                c.CellStyle = qs;
                            }
                        }
                        else if (val.GetType() == typeof(double))
                        {
                            c.SetCellValue((double)tmpArray.GetValue(i, j));

                        }
                        else if (val.GetType() == typeof(int))
                        {
                            c.SetCellValue((int)tmpArray.GetValue(i, j));

                        }
                        else
                        {
                            string s = (string)tmpArray.GetValue(i, j);
                            if (s.Length > MAX_CELL_CHAR_LENGTH)
                            {
                                s = s.Substring(0, MAX_CELL_CHAR_LENGTH);
                            }
                            c.SetCellValue(s);
                        }
                    }
                }
            }

        }

        internal static void CopyRangeofRow(CellRangeAddress cellrange,ISheet sheet,int destinationStartRow)
        {
            int numberOfRows = cellrange.LastRow - cellrange.FirstRow + 1;
            int firstRow = cellrange.FirstRow;
            for (int i = 0; i < numberOfRows; i++)
            {
                sheet.CopyRow(firstRow + i, destinationStartRow + i);
            }
        }

        internal static void SetStyleToEntireRow(IRow row,String property,Object propertyValue)
        {
            if (row != null)
            {
                for (int numb = 0; numb < row.LastCellNum; numb++)
                {
                    ICell currentCell = row.GetCell(numb);
                    if (currentCell != null)
                    {
                        CellUtil.SetCellStyleProperty(currentCell, property, propertyValue);
                    }
                }
            }

        }
    }
}