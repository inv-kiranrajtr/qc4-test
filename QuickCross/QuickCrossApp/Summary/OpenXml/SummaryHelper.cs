using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using log4net;
using Macromill.QCWeb.Common;
using Qc4Launcher.Logic;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Qc4Launcher.Summary.OpenXml
{
    internal class SummaryHelper
    {
        public static Microsoft.Office.Interop.Excel.Application xlApp;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
       
        internal static void PutValue(WorksheetPart worksheetPart,int rowIdx, int col, ref Array arrValue, bool NotRevise = false)
        {
            int u = 0;
            int i, j;
            int d = 0;
            string tmp;
            Type VarType = arrValue.GetType().GetElementType();
            if (worksheetPart == null) { return; }
            Array tmpArray = (Array)arrValue;
            
            for (i = arrValue.GetLowerBound(0); i <= arrValue.GetUpperBound(0); i++, rowIdx++)
            {
                int colLocal = col;
                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowIdx);
                for (j = arrValue.GetLowerBound(1); j <= arrValue.GetUpperBound(1); j++, colLocal++)
                {
                    if (arrValue.GetValue(i, j) != null)
                    {                     
                        object val = arrValue.GetValue(i, j);
                        Cell cell = OpenXmlHelper.GetCell(row, rowIdx, colLocal);
                        if (val.GetType() == typeof(string))
                        {
                            CellValue cellval = new CellValue((string)val)
                            {
                                Space=SpaceProcessingModeValues.Preserve
                            };
                            cell.CellValue = cellval;
                            cell.DataType = CellValues.String;
                        }
                        else if (VarType == typeof(object))
                        {
                            cell.CellValue = new CellValue(val.ToString());
                        }
                    }
                }
            }
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
        internal static Cell GetCell(WorksheetPart worksheetPart, int row, int col)
        {
            return worksheetPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == (OpenXmlHelper.ColumnIndexToColumnLetter(col) + row)).FirstOrDefault();
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

       

        internal static void SetNumberFormat(WorkbookStylesPart workbookStylesPart, int styleIdx,string fmt)
        {
            NumberingFormats numberingFormats = workbookStylesPart.Stylesheet.NumberingFormats;
            uint formatId = numberingFormats.Descendants<NumberingFormat>().LastOrDefault().NumberFormatId.Value +1;
            CellFormat cellFormat = OpenXmlHelper.GetCellFormat(workbookStylesPart, styleIdx);
            NumberingFormat numberingFormat = new NumberingFormat() { NumberFormatId = formatId, FormatCode = fmt };
            numberingFormats.Append(numberingFormat);
            cellFormat.NumberFormatId = (uint)formatId;
            numberingFormats.Count = numberingFormats.Count + 1;
        }

        public static Fill FillColur(string color)
        {
            Fill fill = new Fill();

            PatternFill patternFill = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor = new ForegroundColor() { Rgb = color };
            BackgroundColor backgroundColor = new BackgroundColor() { Indexed = (UInt32Value)64U };

            patternFill.Append(foregroundColor);
            patternFill.Append(backgroundColor);

            fill.Append(patternFill);
            return fill;
        }

        public static void FillCellForgroundColor(SpreadsheetDocument document,Cell cell,string color)
        {
            WorkbookStylesPart styles = document.WorkbookPart.WorkbookStylesPart;
            Fills fills = styles.Stylesheet.Fills;
            fills.Append(FillColur(color));
            fills.Count = fills.Count + 1;                      
            CellFormats cellFormats = styles.Stylesheet.CellFormats;
            CellFormat oldCellFormat = (CellFormat)cellFormats.ChildElements[(int)cell.StyleIndex.Value];
            Alignment alignment = new Alignment() { Horizontal = oldCellFormat.Alignment.Horizontal, Vertical = oldCellFormat.Alignment.Vertical, WrapText = oldCellFormat.Alignment.WrapText };
            CellFormat newCellFormat = new CellFormat()
            {
                NumberFormatId = oldCellFormat.NumberFormatId,
                FontId = oldCellFormat.FontId,
                FillId = fills.Count - 1,
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
            cell.StyleIndex = cellFormats.Count -1;
        }
        
        internal static string GetSelectedPath(Microsoft.Office.Interop.Excel.Application xlAppG,string OutputDirectoryPath,
                                                string Prefix,string Suffix = "", XlFileFormat FileFormat = XlFileFormat.xlOpenXMLWorkbook)
        {
            string ext;
            string n;
            string path = null;
            int i =0;
            xlApp = xlAppG;
            if (OutputDirectoryPath != null)
            {
                ext = FileFormat == XlFileFormat.xlExcel8 ? "xls" : "xlsx";
                do
                {
                    n = Prefix + (i > 0 ? "_" + i : "") + Suffix + "." + ext;
                    i = i + 1;
                    path = OutputUtil.BuildPath(OutputDirectoryPath, n, xlApp.PathSeparator);
                } while (File.Exists(path));
            }
            return path;
        }
    }
}
