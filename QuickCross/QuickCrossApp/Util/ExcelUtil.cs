using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Util
{
	internal class ExcelUtil
	{
		internal Excel.Range GetLastCell(Excel.Range range)
		{
			return range.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
		}

		public static Excel.Range EndxlUp(Excel.Range targetCell)
		{
			//targetCell.Worksheet.Rows.Hidden = false; //TO handle last hidden row
			Excel.Range targetColumn = targetCell.Cells[1].EntireColumn;
			return targetColumn.Cells[targetColumn.Cells.Count].End(Excel.XlDirection.xlUp);
		}

		public static Excel.Range EndxlRight(Excel.Range targetCell)
		{
			Excel.Range targetColumn = targetCell.Cells[1].EntireRow;
			return targetColumn.Cells[targetColumn.Cells.Count].End(Excel.XlDirection.xlToRight);
		}

		public static dynamic Convert(Excel.Range range, Constants.DataType type)
		{
			dynamic value = range.Value2;
			switch (type)
			{
				case Constants.DataType.INTEGER:
					break;
				case Constants.DataType.STRING:
					break;
				case Constants.DataType.LONG:
					break;
				case Constants.DataType.DATETIME:
					break;
			}
			return value;
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

        public static Excel.Worksheet GetWorkSheetBySheetName(Excel.Workbook book, string sheetName)
        {
            foreach (Excel.Worksheet sheet in book.Worksheets)
            {
                if (sheetName == sheet.Name)
                {
                    return sheet;
                }
            }
            return null;
        }


        public static Excel.Workbook AddWorkbook(string selectedFile, Excel.Application excelApp = null)
		{
			Excel.Workbook excelWorkbook = null;
			try
			{
				if (excelApp == null)
				{
					excelApp = new Excel.Application();
				}

				excelApp.DisplayAlerts = false;
				excelWorkbook = excelApp.Workbooks.Add(selectedFile);
				excelWorkbook.Unprotect(Constants.Password);
				excelWorkbook.Saved = true;
			}
			catch (Exception ex)
			{
                Console.Write(ex.Message);
			}
			return excelWorkbook;
		}

		public static Excel.Workbook OpenWorkbok(string selectedFile, Excel.Application excelApp = null)
		{
			Excel.Workbook excelWorkbook = null;
			try
			{
				if (excelApp == null)
				{
					excelApp = new Excel.Application();
				}
				try
				{
					excelApp.DisplayAlerts = false;
				}
				catch { }
				excelWorkbook = excelApp.Workbooks.Open(selectedFile, 0, false, 5, Constants.Password, "", true,
				   Excel.XlPlatform.xlWindows, "\t", false, false, 0, false, 1, 0);
				//excelWorkbook.Unprotect(Constants.Password);
				excelWorkbook.Saved = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return excelWorkbook;
		}

		internal static string GetTemplatePath(string templateName)
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + templateName;
		}

		internal static Excel.Range GetNamedRange(string CodeName, string RangeName,Excel.Workbook workbook)
		{
			try
			{
				Excel.Range ReturnRange = GetWorkSheetByCodeName(workbook,CodeName).get_Range(RangeName);
				return ReturnRange;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

        public static bool GetCheckboxValue(Excel.Worksheet worksheet, string checkBoxName)
        {

            var response = false;
            // A checkbox is considered a shape by Excel and accessed using that object model
            Excel.Shapes shapes = worksheet.Shapes;
            foreach (Excel.Shape shape in shapes)
                if (shape.Name == checkBoxName) // Only give me the value for the checkbox I’m looking for
                    response = DetermineIfCheckboxIsChecked(shape.OLEFormat.Object.Value); // Value returned is a double (not a bool)
            return response;

        }

        private static bool DetermineIfCheckboxIsChecked(double value)
        {

            var response = false;
            if (value > 0) // Checkbox is checked == 1.0
                response = true;
            else // Checkbox is not checked == -4713.0
                response = false;
            return response;
        }      
    }
}
