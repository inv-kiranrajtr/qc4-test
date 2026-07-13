using VB = Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Util
{
	class QCWorkbookHelper
	{
		internal static List<Excel.Worksheet> GetDataSheets(Excel.Workbook workbook)
		{
			List<Excel.Worksheet> worksheets = new List<Excel.Worksheet>();
			foreach (Excel.Worksheet sheet in workbook.Worksheets)
			{
				if (Regex.IsMatch(sheet.Name, "Data[0-9]+$"))
				{
					worksheets.Add(sheet);
				}
			}
			return worksheets.OrderBy(w => w.Name).ToList();
		}

		internal static List<Excel.Worksheet> GetDataSheetsAfterAndBefore(Excel.Workbook workbook)
		{
			List<Excel.Worksheet> worksheets = new List<Excel.Worksheet>();
			foreach (Excel.Worksheet sheet in workbook.Worksheets)
			{
				if (Regex.IsMatch(sheet.Name, @"Data[0-9]+.*[)）]$"))
				{
					worksheets.Add(sheet);
				}
			}
			return worksheets.OrderBy(w => w.Name).ToList();
		}

		internal static List<Excel.Worksheet> GetDataAfterProcessSheets(Excel.Workbook workbook)
		{
			return GetSheetByRegex(@"Data[0-9]+[（(].*[)）]$", workbook);
		}
		
		internal static List<Excel.Worksheet> GetMultivariateSheet(Excel.Workbook workbook)
		{
			return GetSheetByRegex(@"(多変量|Multivariate)[0-9]+$", workbook);
		}

		internal static List<Excel.Worksheet> GetSheetByRegex(string regex, Excel.Workbook workbook)
		{
			List<Excel.Worksheet> worksheets = new List<Excel.Worksheet>();
			foreach (Excel.Worksheet sheet in workbook.Worksheets)
			{
				string name = sheet.Name;
				try
				{
					name = VB.Strings.StrConv(name, VB.VbStrConv.Narrow);
				}
				catch { }
				if (Regex.IsMatch(name, regex))
				{
					worksheets.Add(sheet);
				}
			}
			return worksheets.OrderBy(w => w.Name).ToList();
		}
	}
}
