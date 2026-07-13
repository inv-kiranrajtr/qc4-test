using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace QC4Common.Common
{
	public class CommonFlag
	{
        
        public static bool IsDataUpdated(Excel.Workbook workbook)
		{
			return IsDataUpdated(QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting), 28, 4);
		}

		public static bool IsDataAfterUpdated(Excel.Workbook workbook)
		{
			return IsDataUpdated(QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting), 29, 4);
		}
        public static bool IsMultivariateUpdated(Excel.Workbook workbook)
        {
            return IsDataUpdated(QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting), QC4Common.Common.Constants.STD_DP.Multivariate_Row, QC4Common.Common.Constants.STD_DP.Multivariate_Column);
        }



        private static bool IsDataUpdated(Excel.Worksheet worksheet, int row, int col)
		{
			if (worksheet == null)
			{
				return false;
			}

			Excel.Range range = worksheet.Cells[row, col];
			string str = range.Text;
			return str.ToLower() == "true" ? true : false;
		}

		public static void SetIsDataUpdated(Excel.Workbook workbook, bool isUpdated)
		{
			SetIsDataUpdated(workbook, 28, 4, isUpdated);
		}

		public static void SetIsDataAfterUpdated(Excel.Workbook workbook, bool isUpdated)
		{
			SetIsDataUpdated(workbook, 29, 4, isUpdated);
		}
        public static void SetIsMultivariateUpdated(Excel.Workbook workbook, bool isUpdated)
        {
            SetIsDataUpdated(workbook, QC4Common.Common.Constants.STD_DP.Multivariate_Row, QC4Common.Common.Constants.STD_DP.Multivariate_Column, isUpdated);
        }
        //public static void SetQsUpdated(Excel.Workbook workbook, bool isUpdated)
        //{
        //	SetIsDataUpdated(workbook, 30, 4, isUpdated);
        //}

        private static void SetIsDataUpdated(Excel.Workbook workbook, int row, int col, bool isUpdated)
		{
			if (workbook == null)
			{
				return;
			}
			Excel.Worksheet worksheet  = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting);
			if (worksheet == null)
			{
				return;
			}
            try
            {
                Excel.Range range = worksheet.Cells[row, col];
                range.Value = isUpdated ? "True" : "False";
            }
            catch (Exception e) { /*MessageDialog.Error(e.Message + " ****** " + e.StackTrace + " ****** " + e.InnerException); */}
        }

	}
}
