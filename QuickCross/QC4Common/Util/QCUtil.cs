using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace QC4Common.Util
{
	public class QCUtil
	{
		public static void SetApplicationPaths(Excel.Workbook workbook, ref string tempPath, ref string targetPath)
		{
			Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Common.Constants.SheetCodeName.Setting);
			tempPath = sheet.Cells[26, 4].Text;
			targetPath = sheet.Cells[27, 4].Text;
		}

		public static string GetFileName(Excel.Workbook workbook)
		{
			Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Common.Constants.SheetCodeName.Setting);
			if (sheet == null)
			{
				return null;
			}
			return sheet.Cells[24, 4].Text;
		}


		public static string GetTempPath(Excel.Workbook workbook)
		{
			Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Common.Constants.SheetCodeName.Setting);
			if (sheet == null)
			{
				return null;
			}
			return sheet.Cells[26, 4].Text;
		}

		public static string GetTargetPath(Excel.Workbook workbook)
		{
			Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Common.Constants.SheetCodeName.Setting);
			if (sheet == null)
			{
				return null;
			}
			return sheet.Cells[27, 4].Text;
		}

        public static void SetReadOnly(Excel.Workbook workbook, bool Read_Only)
        {
            try
            {
                Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Common.Constants.SheetCodeName.Setting);
                if (sheet != null)
                {
                    Excel.Range ReadMode = sheet.Cells[QC4Common.Common.Constants.FileOpendMode.File_Read_Only_Row, QC4Common.Common.Constants.FileOpendMode.File_Read_Only_Column];
                    string data = string.Empty;
                    if (Read_Only)
                    {
                        ReadMode.Value = "1";
                    }
                    else
                    {
                        ReadMode.Value = "0";
                    }
                }
            }
            catch { }
        }

        public static string GetProcessId(Excel.Workbook workbook)
        {
            Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Common.Constants.SheetCodeName.Setting);
            if (sheet == null)
            {
                return null;
            }
            return sheet.Cells[30, 4].Text;
        }
    }
}
