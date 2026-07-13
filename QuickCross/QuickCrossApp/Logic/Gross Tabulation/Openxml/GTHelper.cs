using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using log4net;
using Macromill.QCWeb.Common;
using Microsoft.VisualBasic;
using Qc4Launcher.Util;
using Column = DocumentFormat.OpenXml.Spreadsheet.Column;
using Columns = DocumentFormat.OpenXml.Spreadsheet.Columns;
using NumberingFormat = DocumentFormat.OpenXml.Spreadsheet.NumberingFormat;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;

namespace Qc4Launcher.Logic.Gross_Tabulation.Openxml
{
    internal class GTHelper
    {
        public static Microsoft.Office.Interop.Excel.Application xlApp;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
       internal static void InsertValues(ref Array arrValue, SpreadsheetDocument package, string StartCell, int rowIdx, int col, bool NotRevise = false)
        {
            int u = 0;
            int i, j;
            int d = 0;
            double n;
            bool isNumeric;
            string tmp;
            WorksheetPart worksheetPart = OpenXmlHelper.GetWorksheetPartByName(package, StartCell);
            Type VarType = arrValue.GetType().GetElementType();
            Array tmpArray = (Array)arrValue;

            CellFormats cellFormats = package.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats;
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

                        string value = val.ToString();
                        if (val.GetType() == typeof(double))
                        {
                            double tmp2 = (double)val;
                            if (double.IsInfinity(tmp2))
                            {
                                value = QC4Common.Common.Constants.ExcelDiv;
                            }
                        }

                        if (value.Length > 32767)
                        {
                            value = value.Substring(0,32767);
                        }

                        if (val.GetType() == typeof(string))
                        {
                            CellValue data = new CellValue(value)
                            {
                                Space = SpaceProcessingModeValues.Preserve
                            };
                            cell.CellValue = data;
                            isNumeric = double.TryParse(value, out n);
                            var c = (CellFormat)cellFormats.ElementAt((int)cell.StyleIndex.Value);
                            if ((c.NumberFormatId != 1 && c.NumberFormatId < 163) || !isNumeric)
                                cell.DataType = CellValues.String;
                        }
                        else if (VarType == typeof(object))
                        {
                            if(value == QC4Common.Common.Constants.ExcelDiv)
                            {
                                cell.DataType = CellValues.Error;
                            }
                            cell.CellValue = new CellValue(value);
                        }
                    }
                }
            }
        }
        internal static void AddPrefix(ref string buffer, bool ForceToString = false)
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

        internal static bool IsDate(object text)
        {
            DateTime test;
            return DateTime.TryParse(Convert.ToString(text), out test);
        }
        public static bool IsNumeric(string value)
        {
            if (value == null)
            {
                return false;
            }
            return Information.IsNumeric(value);
        }
    
        public static Xdr.TwoCellAnchor GetLastCellAnchor(DrawingsPart drawingsPart)
        {
            return drawingsPart.WorksheetDrawing.Descendants<Xdr.TwoCellAnchor>().LastOrDefault();
        }
        
        public static void DrawEdgeLeft(WorksheetPart worksheetPart,int fRow,int lRow,int lCol,int styleIdx)
        {
            lCol += 1;
            for (int rowIdx = fRow; rowIdx <= lRow; rowIdx++)
            {
                Row row = worksheetPart.Worksheet.Descendants<Row>().Where(p => p.RowIndex == rowIdx).FirstOrDefault();
                Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(lCol) + rowIdx, StyleIndex = (uint)styleIdx };
                row.Append(cell);
            }          
        }
    }
}
