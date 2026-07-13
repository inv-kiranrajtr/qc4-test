using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Application = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace QC4Common.Common
{
    public static class ExcelUtilForAddIn
    {
        public static bool sylk = false;
        public static Worksheet setLoading(Excel.Workbook wb)
        {
            Excel.Workbook exBook = wb.Application.ActiveWorkbook;
            Excel.Worksheet exSheet = (Excel.Worksheet)exBook.ActiveSheet;
            if (exSheet.CodeName != Constants.SheetType.sh_QuesSetting)
            {
                string range = string.Empty;
                switch (exSheet.CodeName)
                {
                    case Constants.SheetType.GTCounting:
                        range = "I2";
                        exSheet.Range[range].HorizontalAlignment = XlHAlign.xlHAlignRight;
                        break;
                    case Constants.SheetType.sh_CrossCounting:
                        range = "J2";
                        exSheet.Range[range].HorizontalAlignment = XlHAlign.xlHAlignRight;
                        break;
                    case Constants.SheetType.sh_SummaryList:
                        range = "J2";
                        break;
                    case Constants.SheetType.DataProcess:
                        range = "L2";
                        break;
                }
                if (!string.IsNullOrEmpty(range))
                {
                    exSheet.Application.EnableEvents = false;
                    exSheet.Range[range].NumberFormat = "General";
                    exSheet.Range[range].Font.Size = "24";
                    exSheet.Range[range].VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    exSheet.Range[range].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    exSheet.Range[range].Value2 = "...";
                    exSheet.Application.EnableEvents = true;
                }
            }
            return exSheet;
        }

        public static void unsetLoading(Excel.Workbook wb)
        {
            Excel.Workbook exBook = wb.Application.ActiveWorkbook;
            Excel.Worksheet Sheet = (Excel.Worksheet)exBook.ActiveSheet;
            if (Sheet!=null&& Sheet.CodeName != Constants.SheetType.sh_QuesSetting)
            {
                string range = "L2";
                switch (Sheet.CodeName)
                {
                    case Constants.SheetType.GTCounting:
                        range = "I2";
                        break;
                    case Constants.SheetType.sh_CrossCounting:
                        range = "J2";
                        break;
                    case Constants.SheetType.sh_SummaryList:
                        range = "J2";
                        break;
                    case Constants.SheetType.DataProcess:
                        range = "L2";
                        break;
                }
                Sheet.Application.EnableEvents = false;
                Sheet.Range[range].Value2 = "";
                Sheet.Application.EnableEvents = true;
            }
        }

        public static void CopySelected(Excel.Workbook wb)
        {
            Workbook exBook = wb.Application.ActiveWorkbook;
            Worksheet exSheet = (Worksheet)exBook.ActiveSheet;
            exSheet.Application.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlWait;
            string locat = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Range Selection = exSheet.Application.Selection as Range;
            object[,] copy = new object[0, 0];
            int start = 0;

            if (Selection.Count > 1)
            {
                copy = Selection.Value2;
            }
            else
            {
                copy = new object[,] { { Selection.Value2 } };
                start = 1;
            }
            int rowCount = copy.GetUpperBound(0) + start;
            int columnCount = copy.GetUpperBound(1) + start;
            string content = "";
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= columnCount; j++)
                {
                    content += copy[i - start, j - start] + "\t";
                }
                content += "\n";
            }
            int str = locat.LastIndexOf(@"\");
            string path = locat.Remove(locat.LastIndexOf(@"\"), (locat.Length - locat.LastIndexOf(@"\"))) + @"\TempQC1234567890Text.txt";
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path, false))
            {
                file.Write(content);
            }
            exSheet.Application.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault;
        }
        public static void PasteSelected(Excel.Workbook wb)
        {
            Workbook exBook = wb.Application.ActiveWorkbook;
            Worksheet exSheet = (Worksheet)exBook.ActiveSheet;
            exSheet.Application.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlWait;
            string locat = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int str = locat.LastIndexOf(@"\");
            string content = "";
            string[] spltRw;
            Range pasteRange = exSheet.Application.Selection as Range;
            string path = locat.Remove(locat.LastIndexOf(@"\"), (locat.Length - locat.LastIndexOf(@"\"))) + @"\TempQC1234567890Text.txt";
            if (System.IO.File.Exists(path))
            {
                using (System.IO.StreamReader sr =
                new System.IO.StreamReader(path))
                {
                    string line = "";
                    int j = 1;
                    while ((line = sr.ReadLine()) != null)
                    {
                        spltRw = line.Split('\t');
                        for (int i = 1; i < spltRw.Count(); i++)
                        {
                            pasteRange[j, i] = spltRw[i - 1];
                        }
                        j++;
                    }
                }
            }
            exSheet.Application.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault;

            //Range range = exSheet.Range[exSheet.Cells[pasteRange.Row, pasteRange.Column], exSheet.Cells[rowCount, columnCount]];
            //range.Value2 = Selection.Value2;
            //rowCount = copy.GetUpperBound(0)+ start;
            //columnCount = copy.GetUpperBound(1) + start;
            //for (int i = 0; i < rowCount; i++)
            //{
            //    for (int j = 0; j < columnCount; j++)
            //    {
            //        Range tRange = range.Cells[i + 1, j + 1];
            //        Range sRange = Selection.Cells[i + 1, j + 1];

            //        tRange.Cells.Interior.ColorIndex = sRange.Cells.Interior.ColorIndex;
            //        tRange.Cells.Interior.Color = sRange.Cells.Interior.Color;
            //        tRange.Cells.Borders.Weight = sRange.Cells.Borders.Weight;
            //        tRange.Cells.Borders.Color = sRange.Cells.Borders.Color;
            //        tRange.Cells.Borders.ColorIndex = sRange.Cells.Borders.ColorIndex;
            //        tRange.Cells.Borders.TintAndShade = sRange.Cells.Borders.TintAndShade;
            //        tRange.Cells.Interior.Pattern = sRange.Cells.Interior.Pattern;
            //        tRange.Cells.HorizontalAlignment = sRange.Cells.HorizontalAlignment;
            //        tRange.Cells.IndentLevel = sRange.Cells.IndentLevel;
            //        tRange.Cells.NumberFormat = sRange.Cells.NumberFormat;
            //        tRange.Cells.Orientation = sRange.Cells.Orientation;
            //        tRange.Cells.RowHeight = sRange.Cells.RowHeight;
            //        tRange.Cells.UseStandardHeight = sRange.Cells.UseStandardHeight;
            //        tRange.Cells.UseStandardWidth = sRange.Cells.UseStandardWidth;
            //        tRange.Cells.VerticalAlignment = sRange.Cells.VerticalAlignment;
            //        tRange.Cells.WrapText = sRange.Cells.WrapText;
            //        tRange.Cells.Font.Background = sRange.Cells.Font.Background;
            //        tRange.Cells.Font.Bold = sRange.Cells.Font.Bold;
            //        tRange.Cells.Font.Color = sRange.Cells.Font.Color;
            //        tRange.Cells.Font.ColorIndex = sRange.Cells.Font.ColorIndex;
            //        tRange.Cells.Font.FontStyle = sRange.Cells.Font.FontStyle;
            //        tRange.Cells.Font.Italic = sRange.Cells.Font.Italic;
            //        tRange.Cells.Font.Shadow = sRange.Cells.Font.Shadow;
            //        tRange.Cells.Font.Size = sRange.Cells.Font.Size;
            //        tRange.Cells.Font.Strikethrough = sRange.Cells.Font.Strikethrough;
            //        tRange.Cells.Font.Subscript = sRange.Cells.Font.Subscript;
            //        tRange.Cells.Font.Superscript = sRange.Cells.Font.Superscript;
            //        tRange.Cells.Font.Underline = sRange.Cells.Font.Underline;
            //        tRange.Cells.ColumnWidth = sRange.Cells.ColumnWidth;
            //        tRange.Cells.Font.ThemeFont = sRange.Cells.Font.ThemeFont;
            //        tRange.Cells.Borders.LineStyle = sRange.Cells.Borders.LineStyle;
            //    }
            //}
        }
        public static string GetActiveWorkBookName(Excel.Workbook wb)
        {
            string WorkbookName = "";
            try
            {
                WorkbookName = wb.Application.ActiveWorkbook.FullName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return WorkbookName;
        }
        public static bool AddValidation(Excel.Range excelRange, Excel.XlDVType validationType, object AlertStyle, object Operator, object Formula1, object Formula2, object message = null)
        {
			excelRange.Validation.Delete();
            try
            {
                excelRange.Validation.Add(validationType, AlertStyle, Operator, Formula1, Formula2);
            }
            catch(Exception ex)
            {

            }
            return true;
        }

        public static void SetCellInteriorColor(Excel.Range ExcelCell, dynamic Color)
        {
            ExcelCell.Interior.Color = Color;
        }

        public static void SetCellBorderColor(Excel.Range ExcelCell, dynamic Color)
        {
            ExcelCell.Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color;
            ExcelCell.Borders[Excel.XlBordersIndex.xlEdgeRight].Color = Color;
            ExcelCell.Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = Color;
            ExcelCell.Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color;
        }
        public static void SetFontColor(Excel.Range ExcelCell, dynamic Color)
        {
            ExcelCell.Font.Color = Color;
        }
        public static void ResetCell(Excel.Range excelCell, bool resetcolor = true, bool resetvalidation = true)
        {
			bool flag = excelCell.Application.EnableEvents;
			excelCell.Application.EnableEvents = false;
			//excelCell.ClearContents();
			QC4Common.Util.ExcelUtil.ClearContents(excelCell);
			if (resetcolor)
            {
                ExcelUtilForAddIn.SetCellInteriorColor(excelCell, Excel.XlColorIndex.xlColorIndexNone);
            }
            if (resetvalidation)
            {
                excelCell.Validation.Delete();
            }
            excelCell.Application.EnableEvents = flag;
        }
        public static void SetSelectedCell(Excel.Range targetcell)
        {
            targetcell.Select();
        }
        public static Excel.Range GetNamedRange(Excel.Workbook wb, string CodeName, string RangeName)
        {
            try
            {
                Excel.Range ReturnRange = GetWorksheetByName(wb,CodeName).get_Range(RangeName);

                return ReturnRange;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


		public static Excel.Worksheet GetWorksheetByCodeName(Excel.Workbook wb,string SheetName)
        {
            try
            {
                return wb.Application.ActiveWorkbook.Worksheets[SheetName];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
		}

		

		public static Excel.Worksheet GetWorkSheetByCodeName(Excel.Workbook book, string codeName)
        {
            foreach (Excel.Worksheet sheet in book.Worksheets)
            {
                if (codeName == sheet.CodeName)
                {
                    return sheet;
                }
            }
            return null;
        }

        public static Excel.Worksheet GetWorksheetByName(Excel.Workbook wb,string SheetName,Excel.Application application = null)
        {
            if (application == null)
            {
                try
                {
                    return wb.Application.ActiveWorkbook.Worksheets[SheetName];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else
            {

                try
                {

                    return application.ActiveWorkbook.Worksheets[SheetName];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }



            }

            //try
            //{

            //    return Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets[SheetName];
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return null;
            //}
        }

        //public static Excel.Range EndxlLeft(Excel.Range targetCell)
        //{
        //    Excel.Range targetColumn = targetCell.Cells[1].EntireRow;
        //    return targetColumn.Cells[targetColumn.Cells.Count].End(Excel.XlDirection.xlToLeft);
        //} 

        public static Excel.Worksheet GetWorksheetByIndex(Excel.Workbook wb,int SheetIndex)

        {
            try
            {
                return wb.Application.ActiveWorkbook.Worksheets[SheetIndex];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

       
        public static int? EndColumn(Excel.Worksheet sheet)
        {
            if (sheet == null)
            {
                return null;
            }
            return sheet.Columns.Count;
        }

        public static int? EndRow(Excel.Worksheet sheet)
        {
            if (sheet == null)
            {
                return null;
            }
            return sheet.Rows.Count;
        }

        public static Excel.Range EndxlUp(Excel.Range targetCell, bool unhide = false)
        {
			if (unhide) //TO handle last hidden row
			{
				targetCell.Worksheet.Rows.Hidden = false;
			}
            Excel.Range targetColumn = targetCell.Cells[1].EntireColumn;
            return targetColumn.Cells[targetColumn.Cells.Count].End(Excel.XlDirection.xlUp);
        }



        public static Excel.Range EndxlRight(Excel.Range targetCell)
        {
            Excel.Range targetColumn = targetCell.Cells[1].EntireRow;
            return targetColumn.Cells[targetColumn.Cells.Count].End(Excel.XlDirection.xlToLeft);
        }
       

        public static Excel.Range EndxlLeft(Excel.Range targetCell, int start = 1)
        {
            Excel.Range targetColumn = targetCell.Cells[1].EntireRow;
            return targetColumn.Cells[start].End(Excel.XlDirection.xlToRight);
        }


        public static Office.CommandBar GetCellContextMenu(Excel.Workbook wb,string index = "Cell")
        {
            Application targetApp = wb.Application;
            return targetApp.CommandBars[index];
        }

		public static Office.CommandBars GetContextMenus(Excel.Workbook wb)
		{
			Application targetApp = wb.Application;
			return targetApp.CommandBars;
		}

		public static Office.CommandBarButton GetNewMenuItem(Excel.Workbook wb, string Caption, int index = 1,string menuIndex = "Cell")
        {
            Office.CommandBar contextMenu = GetCellContextMenu(wb,menuIndex);
            Office.CommandBarButton menuItem = (Office.CommandBarButton)contextMenu.Controls.Add(Office.MsoControlType.msoControlButton, Type.Missing, Type.Missing, index, true);
            menuItem.Style = Office.MsoButtonStyle.msoButtonCaption;
            menuItem.Caption = Caption;
            return menuItem;
        }

		public static void ResetCellMenu(Excel.Workbook wb,string index = "Cell")
		{
			GetCellContextMenu(wb,index).Reset(); // reset the cell context menu back to the default
		}

		public static Excel.Range Find(Excel.Range range, string text)
		{
			return range.Find(text, Type.Missing,
								Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
								Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
								Type.Missing, Type.Missing);
		}
        public static void pastespecialcheck(Excel.Range ChangedRange)
        {
            if (!QC4Common.Common.Constants.IsCtrlV)
                return;
            QC4Common.Common.Constants.IsCtrlV = false;
            string fulltext = string.Empty;
            string clipboardtext = string.Empty;
            bool eventstate = ChangedRange.Application.EnableEvents;
            bool screenUpdateState = ChangedRange.Application.ScreenUpdating;
            try
            {

                if (Clipboard.GetDataObject()!=null)
                {
                    if (Clipboard.GetDataObject().GetData(DataFormats.Text, true) != null)
                    {
                        foreach (Excel.Range ChangedCell in ChangedRange.Cells)
                        {
                            fulltext = fulltext + ChangedCell.Value;
                        }
                        fulltext = fulltext.Replace(" ", string.Empty);
                        fulltext = fulltext.Replace("\t", string.Empty);
                        fulltext = fulltext.Replace("\n", string.Empty);
                        fulltext = fulltext.Replace("\r", string.Empty);
                        clipboardtext = (string)Clipboard.GetDataObject().GetData(DataFormats.Text, true);// Clipboard.GetText(TextDataFormat.Text);
                        clipboardtext = clipboardtext.Replace(" ", string.Empty);
                        clipboardtext = clipboardtext.Replace("\t", string.Empty);
                        clipboardtext = clipboardtext.Replace("\n", string.Empty);
                        clipboardtext = clipboardtext.Replace("\r", string.Empty);
                        if (fulltext == clipboardtext && (!sylk))
                        {
                            ChangedRange.Application.EnableEvents = false;

                            try
                            {

                                var copiedText = (string)Clipboard.GetDataObject().GetData(DataFormats.Text, true);

                                if (copiedText.EndsWith("\r\n"))
                                {
                                    copiedText = copiedText.Remove(copiedText.Length - 2);
                                }
                                var dataToPaste = copiedText.Split(new[] { "\r\n" }, StringSplitOptions.None).Select(i => Regex.Split(i, "\t")).ToArray();
                                Object[,] copyArray = new Object[dataToPaste.Length, dataToPaste[0].Length];
                                for (int i = 0; i != dataToPaste.Length; i++)
                                    for (int j = 0; j != dataToPaste[0].Length; j++)
                                    {
                                        copyArray[i, j] = dataToPaste[i][j];
                                        string str = Convert.ToString(copyArray[i, j]);
                                        if (str.Contains("\n"))
                                        {
                                            copyArray[i, j] = str.Remove(str.Length - 1, 1).Remove(0, 1);
                                            // copyArray[i, j] = str.Replace("(\"\"){2}", string.Empty);
                                        }

                                    }

                                ChangedRange = ChangedRange.Resize[copyArray.GetLength(0), copyArray.GetLength(1)];
                                ChangedRange.Value = copyArray;
                                foreach (Excel.Range setIgnoreErrorCell in ChangedRange.Cells)
                                {
                                    if (setIgnoreErrorCell.Errors[Excel.XlErrorChecks.xlNumberAsText].Value)
                                    {
                                        try
                                        {
                                            setIgnoreErrorCell.Errors.Item[Excel.XlErrorChecks.xlNumberAsText].Ignore = true;
                                        }
                                        catch { }
                                    }
                                }
                            }

                            catch (Exception e) { }
                            finally
                            {
                             ChangedRange.Application.EnableEvents = eventstate;
                            }
                        }
                       
                    }
                }
                sylk = false;
            }
            catch (Exception e) { }
        }
	}


}

