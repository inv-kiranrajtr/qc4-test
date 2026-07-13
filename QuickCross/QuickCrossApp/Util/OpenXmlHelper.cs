using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using log4net;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using Microsoft.VisualBasic;
using Qc4Launcher.Logic;
using Qc4Launcher.Summary.OpenXml;
using Column = DocumentFormat.OpenXml.Spreadsheet.Column;
using Columns = DocumentFormat.OpenXml.Spreadsheet.Columns;
using NumberingFormat = DocumentFormat.OpenXml.Spreadsheet.NumberingFormat;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;

namespace Qc4Launcher.Util
{
    public class OpenXmlHelper
    {
        public static Microsoft.Office.Interop.Excel.Application xlApp;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static Row GetRow(Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().
                  Elements<Row>().Where(r => r.RowIndex == rowIndex).FirstOrDefault();
        }

        internal static Cell GetCell(Row row, int rowIdx, int col)
        {
            return row.Elements<Cell>().Where(r => r.CellReference == ColumnIndexToColumnLetter(col) + rowIdx).FirstOrDefault();
        }
        internal static Column GetColumn(WorksheetPart worksheetPart, int col)
        {
            return worksheetPart.Worksheet.GetFirstChild<Columns>().
                 Elements<Column>().Where(c => c.Min == col).FirstOrDefault();
        }
        public static void PutValueToSingleCell(WorksheetPart worksheetPart, int row, int col, string value)
        {
            Row r = GetRow(worksheetPart.Worksheet, (uint)row);
            Cell cell = GetCell(r, row, col);
            cell.CellValue = new CellValue(value);
            double val = 0;
            if (double.TryParse(value, out val))
            {
                cell.DataType = CellValues.Number;
            }
            else
            {
                cell.DataType = CellValues.String;
            }
        }

        public static int ToInt(bool test)
        {
            return test ? -1 : 0;
        }
        internal static string ColumnIndexToColumnLetter(int colIndex)
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
        internal static WorksheetPart GetWorksheetPartByName(SpreadsheetDocument document, string sheetName)
        {
            IEnumerable<Sheet> sheets =
               document.WorkbookPart.Workbook.GetFirstChild<Sheets>().
               Elements<Sheet>().Where(s => s.Name == sheetName);

            if (sheets?.Count() == 0)
            {
                return null;
            }

            string relationshipId = sheets?.First().Id.Value;

            WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(relationshipId);

            return worksheetPart;
        }
        public static void RemoveOutPutFiles(string folderName, bool removeInsideDirectories = false)
        {
            string filePath = Path.Combine(Path.GetTempPath(), "QC4", folderName);
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

                if (removeInsideDirectories)
                {
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
        }
        internal static CellFormat GetCellFormat(WorkbookStylesPart workbookStylesPart, int styleIdx)
        {
            CellFormats cellFormats = workbookStylesPart.Stylesheet.CellFormats;
            return (CellFormat)cellFormats.ElementAt(styleIdx);
        }
        public static void AutofitRow(WorksheetPart worksheetPart, Row row, int fCol, int lCol, bool isRowDesc = false, double NumericRowHt = 0)
        {
            double ht = row.Height;
            double ht2 = NumericRowHt == 0 ? GetRowHeight(worksheetPart, row, fCol, lCol, isRowDesc) : NumericRowHt;
            if (ht > ht2)
            {
                ht2 = ht;
            }
            row.Height = (float)ht2 + 2;
        }
        private static double GetRowHeight(WorksheetPart worksheetPart, Row row, int fCol, int lCol, bool isRowDesc = false)
        {
            double maxLines = 0;
            for (int i = fCol; i <= lCol; i++)
            {
                Cell cell = GetCell(row, (int)row.RowIndex.Value, i);
                if (cell == null)
                {
                    continue;
                }
                string val = cell.CellValue == null ? "" : cell.CellValue.InnerText;
                double lines = FindLine(val, isRowDesc ? OutputUtil.widthDefColDesc : GetColumn(worksheetPart, i) == null ? OutputUtil.widthDefWithoutMrgn : GetColumn(worksheetPart, i).Width.Value);
                if (maxLines < lines)
                {
                    maxLines = lines;
                }
            }
            double maxLimitHeightInPoints = CrossCreator.ROW_MAX_HEIGHT * 20;
            if (maxLimitHeightInPoints < maxLines * 11.3)
            {
                return maxLimitHeightInPoints;
            }
            return maxLines * 11.3;
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
        public static Xdr.TwoCellAnchor GetLastCellAnchor(DrawingsPart drawingsPart)
        {
            return drawingsPart.WorksheetDrawing.Descendants<Xdr.TwoCellAnchor>().LastOrDefault();
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
        public static double FindLine(string val, double colWidth)
        {
            string[] valAry = val.Replace("\r", "").Split('\n');
            double lnes = valAry.Length;
            double lines = 0;
            for (int i = 0; i < valAry.Length; i++)
            {
                string str = valAry[i];
                System.Drawing.Bitmap objBitmap = default(System.Drawing.Bitmap);
                System.Drawing.Graphics objGraphics = default(System.Drawing.Graphics);

                objBitmap = new System.Drawing.Bitmap(1, 1);
                objGraphics = System.Drawing.Graphics.FromImage(objBitmap);

                System.Drawing.SizeF stringSize = objGraphics.MeasureString(str, new System.Drawing.Font(QC4Common.Common.Constants.GlobalMode.Split(',')[1], 9));

                objBitmap.Dispose();
                objGraphics.Dispose();
                var tempWidth = stringSize.Width * 0.14099;
                var correction = (tempWidth / 100) * -1.30;
                lnes += Math.Ceiling((tempWidth - correction) / colWidth);
                lnes--;
            }
            lines = lnes;
            if (lines > 2 && colWidth > 150)
                lines += QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? 1 : 2;
            else if (lines > 1 && QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                lines++;

            //const int MaxAnsiCode = 255;
            //double width = 0;
            //val = Regex.Replace(val, @"\n|\r|\r\n", "\r");
            //val = Regex.Replace(val, @"\t", "");
            //double extrLineForSpace = 0;
            //char prevChar = ' ';
            //double line;
            //double widthDef = OutputUtil.charwidthDef;
            //double widthDefAsci = OutputUtil.charwidthDefAsci;
            //foreach (char c in val)
            //{
            //    if (c == '\r')
            //    {
            //        line = Math.Ceiling(width / colWidth);
            //        if (line > 1 && extrLineForSpace > 0)
            //        {
            //            line++;
            //        }
            //        lines += line;
            //        width = 0;
            //        extrLineForSpace = 0;
            //    }
            //    else if (c == ' ' && prevChar == '\r')
            //    {
            //        extrLineForSpace++;
            //    }
            //    else
            //    {
            //        if (c > MaxAnsiCode) { width += widthDef; } else { width += widthDefAsci; }
            //    }
            //    prevChar = c;
            //}
            //line = Math.Ceiling(width / colWidth);
            //if (line > 1 /*&& extrLineForSpace > 0*/)
            //{
            //    line += QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? 1 : 2;
            //}
            //lines += line;
            return lines;
        }
        internal static void AutoFitColumn(WorkbookPart workbookPart, WorksheetPart worksheetPart, int firtColumn, int lastColumn, int firtRow, int lastRow, bool isCheckCross = false)
        {
            for (int i = firtColumn; i <= lastColumn; i++)
            {
                double defColWidth = isCheckCross ? OutputUtil.checkcrosswidthDef : OutputUtil.widthDef;
                double widthCalc = GetColumnWidth(workbookPart, worksheetPart, i, firtRow, lastRow);
                if (widthCalc > 255)
                {
                    widthCalc = 0;
                }
                Column column = GetColumn(worksheetPart, i);
                double width = column == null ? defColWidth : column.Width.Value;
                if (widthCalc < defColWidth)
                {
                    widthCalc = defColWidth;
                }
                if (width < widthCalc)
                {
                    if (column == null)
                    {
                        Columns columns = worksheetPart.Worksheet.GetFirstChild<Columns>();
                        Column column1 = new Column() { Min = (uint)i, Max = (uint)i, Width = widthCalc, Style = (UInt32Value)2U, CustomWidth = true };
                        columns.InsertAfter(column1, columns.LastChild);
                    }
                    else
                        column.Width = widthCalc;
                }
            }
        }
        private static double GetColumnWidth(WorkbookPart workbookPart, WorksheetPart worksheetPart, int column, int firstRow, int lastRow)
        {
            double width = -1;
            for (int rowIdx = firstRow; rowIdx <= lastRow; ++rowIdx)
            {
                Row row = GetRow(worksheetPart.Worksheet, (uint)rowIdx);//worksheetPart.Worksheet.Descendants<Row>().Where(p => p.RowIndex == rowIdx).FirstOrDefault();
                if (row != null)
                {
                    Cell cell = GetCell(row, rowIdx, column);
                    if (null == cell) { continue; }
                    string sval = GetFormatCellValue(workbookPart, cell);
                    width = Math.Max(width, sval.Length + 2);
                }
            }
            return width * OutputUtil.charwidthDefAsci;
        }
        public static string GetFormatCellValue(WorkbookPart workbookPart, Cell cell)
        {
            string val; double n;
            CellFormat cellFormat = (CellFormat)workbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ElementAt((int)cell.StyleIndex.Value);
            if (cellFormat.NumberFormatId > 163 && cell.CellValue != null)
            {
                string format = workbookPart.WorkbookStylesPart.Stylesheet.NumberingFormats.Elements<NumberingFormat>()
                .Where(i => i.NumberFormatId.ToString() == cellFormat.NumberFormatId.ToString())
                .First().FormatCode;
                bool isNumeric = double.TryParse(cell.CellValue.InnerText, out n);
                if (isNumeric == true)
                {
                    double d = double.Parse(cell.CellValue.InnerText);
                    val = d.ToString(format);
                    return val;
                }
                return cell.CellValue.InnerText;
            }
            else if (cell.CellValue != null)
            {
                bool isNumeric = double.TryParse(cell.CellValue.InnerText, out n);
                if (isNumeric == true)
                {
                    double d = double.Parse(cell.CellValue.InnerText);
                    val = d.ToString("0");
                    return val;
                }
                return cell.CellValue.InnerText;
            }
            return "";
        }
        public static void SaveWorkBookCross(string path, string BookPSWD, ref Microsoft.Office.Interop.Excel.Application xlAppG, bool isMaximizeWindow = true)
        {
            xlApp = xlAppG;
            Microsoft.Office.Interop.Excel.Workbooks wbs = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook wb = wbs.Add(path);
            try
            {
                //foreach (Microsoft.Office.Interop.Excel.Worksheet sht in wb.Worksheets)
                //{
                //    foreach (Microsoft.Office.Interop.Excel.Shape shp in sht.Shapes)
                //    {
                //        try
                //        {
                //            shp.TextFrame2.TextRange.Font.NameFarEast = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                //            //shp.TextFrame2.TextRange.Characters.Font.NameComplexScript = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                //            shp.TextFrame2.TextRange.Characters.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                //            //shp.TextFrame2.TextRange.Characters.Font.NameAscii = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                //            //shp.TextFrame2.TextRange.Characters.Font.NameOther = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                //        }
                //        catch { }
                //    }
                //}
                if (isMaximizeWindow)
                {
                    //wb.Application.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
                    wb.Unprotect(BookPSWD);
                }
                else
                {
                    wb.Unprotect(BookPSWD);
                    wb.Close();
                    wbs.Close();
                }
            }
            finally
            {
                if (wb != null)
                {
                    try
                    {
                        COMWholeOperate.releaseComObject(ref wb);
                    }
                    catch { }
                }
                if (wbs != null)
                {
                    try
                    {
                        COMWholeOperate.releaseComObject(ref wbs);
                    }
                    catch { }
                }
            }
        }
        public static void SaveWorkBook(string path, string BookPSWD, Microsoft.Office.Interop.Excel.Application xlAppG, bool isMaximizeWindow = true)
        {
            xlApp = xlAppG;
            Microsoft.Office.Interop.Excel.Workbooks wbs = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook wb = wbs.Add(path);
            //foreach (Microsoft.Office.Interop.Excel.Worksheet sht in wb.Worksheets)
            //{
            //    foreach (Microsoft.Office.Interop.Excel.Shape shp in sht.Shapes)
            //    {
            //        try
            //        {
            //            shp.TextFrame2.TextRange.Font.NameFarEast = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            //            //shp.TextFrame2.TextRange.Characters.Font.NameComplexScript = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            //            shp.TextFrame2.TextRange.Characters.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            //            //shp.TextFrame2.TextRange.Characters.Font.NameAscii = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            //            //shp.TextFrame2.TextRange.Characters.Font.NameOther = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            //        }
            //        catch { }
            //    }
            //}
            try
            {
                if (isMaximizeWindow)
                {
                    wb.Application.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
                    wb.Unprotect(BookPSWD);
                }
                else
                {
                    wb.Unprotect(BookPSWD);
                    wb.Close();
                    wbs.Close();
                }
            }
            catch
            {

            }
            finally//Redmine id :193454
            {

                if (wb != null)
                {
                    try
                    {
                        COMWholeOperate.releaseComObject(ref wb);
                    }
                    catch { }
                }
                if (wbs != null)
                {
                    try
                    {
                        COMWholeOperate.releaseComObject(ref wbs);
                    }
                    catch { }
                }
                if (xlAppG != null)
                {
                    try
                    {
                        COMWholeOperate.releaseComObject(ref xlAppG);
                    }
                    catch { }
                }

            }
        }
        internal static string GetDefaultPath(Microsoft.Office.Interop.Excel.Application xlAppG, string fileName, string folderName
                                               , string outputFolder = null)
        {
            string path = "";
            if (!Util.CommonFunction.ActivationKeyChecking())
            {
                string tempFolder = Path.Combine(Path.GetTempPath(), "QC4") + "\\" + outputFolder;//TempPath.Substring(0, TempPath.LastIndexOf('\\')) + "\\GTOutputForSTD";
                string fileDirectory = "";
                if (Directory.Exists(tempFolder))
                {
                    try
                    {
                        Directory.Delete(tempFolder, true);
                        Directory.CreateDirectory(tempFolder);
                    }
                    catch { }
                }
                else
                    Directory.CreateDirectory(tempFolder);
                int fi = 0;
                while (fi != -1)
                {
                    fi++;
                    try
                    {
                        fileDirectory = tempFolder + "\\" + fileName + fi;
                        if (!Directory.Exists(tempFolder + "\\" + fileName + fi))
                        {
                            Directory.CreateDirectory(tempFolder + "\\" + fileName + fi);
                            fi = -1;
                        }
                    }
                    catch { }
                }
                string filename = Qc4Launcher.Util.Definiotion.SelectedFile;
                filename = filename.Split('_')[0];
                path = fileDirectory + "\\" + filename + "_" + (DateTime.Now.ToString("yyyyMMdd_HHmm")) + "_" + fileName + ".xlsx";
            }
            else
            {
                xlApp = xlAppG;
                string ext = ".xlsx";

                string outPath = Path.Combine(Path.GetTempPath(), "QC4", folderName);
                outPath = Path.Combine(outPath, Path.GetFileNameWithoutExtension(Util.Constants.Qc4FileName));
                GlobalMethodClass.GuaranteeDirectoryExist(outPath);
                DirectoryInfo dir = new DirectoryInfo(outPath);
                dir.Attributes = FileAttributes.ReadOnly;
                do
                {
                    fileName += ext;
                    path = OutputUtil.BuildPath(outPath, fileName, xlApp.PathSeparator);
                } while (File.Exists(path));
            }
            return path;
        }
        public static void FillCellForgroundColor(SpreadsheetDocument document, Cell cell, string color)
        {
            WorkbookStylesPart styles = document.WorkbookPart.WorkbookStylesPart;
            if (!SummaryCreatorXml.FillColors.ContainsKey(color))
            {
                Fills fills = styles.Stylesheet.Fills;
                Fill fill = FillColur(color);
                fills.Append(fill);
                fills.Count = fills.Count + 1;
                SummaryCreatorXml.FillColors.Add(color, fills.Count - 1);
            }
            CellFormats cellFormats = styles.Stylesheet.CellFormats;
            CellFormat oldCellFormat = (CellFormat)cellFormats.ChildElements[(int)cell.StyleIndex.Value];
            Alignment alignment = new Alignment() { Horizontal = oldCellFormat.Alignment.Horizontal, Vertical = oldCellFormat.Alignment.Vertical, WrapText = oldCellFormat.Alignment.WrapText };//oldCellFormat.Alignment;
            CellFormat newCellFormat = new CellFormat()
            {
                NumberFormatId = oldCellFormat.NumberFormatId,
                FontId = oldCellFormat.FontId,
                FillId = SummaryCreatorXml.FillColors[color],
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
        public static void SetSignificanceTestMarking(SpreadsheetDocument document, Cell cell, string buf)
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
            Alignment alignment = new Alignment() { Horizontal = oldCellFormat.Alignment.Horizontal, Vertical = oldCellFormat.Alignment.Vertical, WrapText = oldCellFormat.Alignment.WrapText };//oldCellFormat.Alignment;
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
    }
}
