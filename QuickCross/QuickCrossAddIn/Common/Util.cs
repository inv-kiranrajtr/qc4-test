using ExcelAddIn.DB;
using ExcelAddIn.Sheets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Diagnostics;
using QC4Common.Model;

namespace ExcelAddIn.Common
{

    class Util
    {
        public static int IsVariableFoundInQSSheet(string TargetCellContent)
        {

            int FoundAtRow = -1;
            Excel.Worksheet wsQstnSettings = ExcelUtil.GetWorkSheetByCodeName(Constants.SheetCodeName.QuestionSetting);//ExcelUtil.GetWorksheetByIndex(1);
            Excel.Range range = ExcelUtil.GetNamedRange("List", "List_Item_ALLD");

            if (range == null) return FoundAtRow;

            int i = 4;
            foreach (Excel.Range qstnCells in range.Cells)
            {

                Excel.Range qstnSettingsVariableCln = wsQstnSettings.Cells[i, 6];
                if (TargetCellContent == qstnSettingsVariableCln.Text)
                {
                    FoundAtRow = i;
                    break;

                }
                i++;
            }

            return FoundAtRow;
        }

		public static int IsVariableFoundInList(string TargetCellContent,string ListType = Constants.VariableList.ListItemALLD)
		{

			int FoundAtRow = -1;
			Excel.Worksheet wsQstnSettings = ExcelUtil.GetWorksheetByCodeName(Constants.SheetType.sh_ListView);
			Excel.Range range = ExcelUtil.GetNamedRange("List", ListType);

			if (range == null) return FoundAtRow;

			int i = 4;
			foreach (Excel.Range qstnCells in range.Cells)
			{

				Excel.Range qstnSettingsVariableCln = wsQstnSettings.Cells[i, 6];
				if (TargetCellContent == qstnSettingsVariableCln.Text)
				{
					FoundAtRow = i;
					break;

				}
				i++;
			}

			return FoundAtRow;
		}

		public static int GetQuestionTypeValue(string key)
		{
			switch (key)
			{
				case Constants.QuestionType.FAL: return (int)Constants.QuestionTypeEnum.FAL;
				case Constants.QuestionType.FAS: return (int)Constants.QuestionTypeEnum.FAL;
				case Constants.QuestionType.MAC: return (int)Constants.QuestionTypeEnum.FAL;
				case Constants.QuestionType.MTM: return (int)Constants.QuestionTypeEnum.FAL;
				case Constants.QuestionType.MTS: return (int)Constants.QuestionTypeEnum.FAL;
				case Constants.QuestionType.MTT: return (int)Constants.QuestionTypeEnum.FAL;
				case Constants.QuestionType.RAT: return (int)Constants.QuestionTypeEnum.FAL;
				case Constants.QuestionType.RNK: return (int)Constants.QuestionTypeEnum.FAL;
				case Constants.QuestionType.SAP: return (int)Constants.QuestionTypeEnum.FAL;
				case Constants.QuestionType.SAR: return (int)Constants.QuestionTypeEnum.FAL;
				case Constants.QuestionType.SAS: return (int)Constants.QuestionTypeEnum.FAL;
				default: return 0;
			}
		}

		public static int GetAnswerTypeValue(string key)
		{
			switch (key)
			{
				case Constants.AnswerType.SA: return (int)Constants.AnswerTypeEnum.SA;
				case Constants.AnswerType.MA: return (int)Constants.AnswerTypeEnum.MA;
				case Constants.AnswerType.FA: return (int)Constants.AnswerTypeEnum.FA;
				case Constants.AnswerType.N: return (int)Constants.AnswerTypeEnum.N;
				case Constants.AnswerType.D: return (int)Constants.AnswerTypeEnum.D;
				default: return 0;
			}
		}

        public static object[,] LoadDTtoArray(DataTable dt)
        {
            object[,] dtarray = new object[dt.Rows.Count, dt.Columns.Count - 1];

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int ii = 0; ii < dt.Columns.Count; ii++)
                {
                    if (ii == 0)
                    {
                        dtarray[j, ii] = dt.Rows[j][ii];
                    }
                    if (ii > 1)
                    {
                        dtarray[j, ii - 1] = dt.Rows[j][ii];
                    }
                }
            }

            return dtarray;
        }
        public static DataTable LoadDataFromDB(string sql, Excel.Workbook activeworkbook= null)
        {
            if (activeworkbook == null) activeworkbook = Globals.ThisAddIn.Application.ActiveWorkbook;
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(activeworkbook)))//DataProcess.Sheet.Application.ActiveWorkbook
                {
                   
                    dbSource.Open();
                   
                    dt = DB.DBHelper.GetDataTable(sql, dbSource);
                    if (dt.Rows.Count == 0)
                    {
                        return null;
                    }
                    dbSource.Close();
                }
            }
            catch (Exception ex)
            {
                MessageDialog.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                return null;
            }
            return dt;
        }

        public static int GetTotalRowcount(string connectionstring = "")
        {
            int count = 0;
            try
            {
                using (SQLiteConnection dbSource = DBHelper.GetConnection(!string.IsNullOrEmpty(connectionstring) ? connectionstring : DBHelper.GetConnectionString(QC4Common.Sheets.DataProcess.currworkbook)))//DataProcess.Sheet.Application.ActiveWorkbook
                {
                    string sql = "SELECT COUNT(*) FROM data_after_process";
                    count = DBHelper.ExecuteScalar(dbSource, sql);

                }
            }
            catch (Exception ex)
            {

            }
            return count;
        }
        public static int GetTotalRowCountForDeleteLDEL(int sortnumbr = 0, int deletedrows = 0)
        {
            int count = 0;
            try
            {

                using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(QC4Common.Sheets.DataProcess.currworkbook)))//DataProcess.Sheet.Application.ActiveWorkbook
                {

                    string sql = "SELECT count(sort_no) FROM (select sort_no from data_after_process where sort_no > " + sortnumbr.ToString() + " limit " + (Constants.MAX_ROW_COUNT-deletedrows).ToString() + ")";// "select count(sort_no) from data_after_process where sort_no > " + sortnumbr.ToString() + " limit " + Constants.MAX_ROW_COUNT.ToString();
                    count = DBHelper.ExecuteScalar(dbSource, sql);

                }
            }
            catch (Exception ex)
            {

            }
            return count;
            // throw new NotImplementedException();
        }
        public static int GetLastprocessedsortnumber(int sortnumbr = 0, int deletedrows = 0)
        {
            int count = 0;
            try
            {

                using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(QC4Common.Sheets.DataProcess.currworkbook)))//DataProcess.Sheet.Application.ActiveWorkbook
                {

                    string sql = "SELECT MAX(sort_no) FROM (select sort_no from data_after_process where sort_no > " + sortnumbr.ToString() + " limit " + (Constants.MAX_ROW_COUNT - deletedrows).ToString() + ")";
                    count = DBHelper.ExecuteScalar(dbSource, sql);

                }
            }
            catch (Exception ex)
            {

            }
            return count;
            // throw new NotImplementedException();
        }
        public static int GetLastsortnumberForDataAfterProcess(int sortnumbr = 0, int limit = 0, Excel.Workbook Workbook = null)
        {
            int count = 0;
            try
            {

                using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(Workbook)))//DataProcess.Sheet.Application.ActiveWorkbook
                {

                    string sql = "SELECT MAX(sort_no) FROM (select sort_no from data_after_process where sort_no > " + sortnumbr.ToString() + " limit " + (limit).ToString() + ")";
                    count = DBHelper.ExecuteScalar(dbSource, sql);

                }
            }
            catch (Exception ex)
            {

            }
            return count;
            // throw new NotImplementedException();
        }
        public static int GetLastsortnumberForDataProcess(int sortnumbr = 0, int limit = 0, Excel.Workbook Workbook=null)
        {
            int count = 0;
            try
            {

                using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(Workbook)))//DataProcess.Sheet.Application.ActiveWorkbook
                {

                    string sql = "SELECT MAX(sort_no) FROM (select sort_no from answers where sort_no > " + sortnumbr.ToString() + " limit " + (limit).ToString() + ")";
                    count = DBHelper.ExecuteScalar(dbSource, sql);

                }
            }
            catch (Exception ex)
            {

            }
            return count;
            // throw new NotImplementedException();
        }
        public static int GetWIndowHandleFromSettings()
        {
            IntPtr hWnd_Processhandle;
            int ProcesId = Convert.ToInt32(QC4Common.Util.QCUtil.GetProcessId(Globals.ThisAddIn.Application.ActiveWorkbook));
            Process hostProcess = Process.GetProcessById(ProcesId);
             
            hWnd_Processhandle = hostProcess.MainWindowHandle;
            return hWnd_Processhandle.ToInt32();

        }
        public static IntPtr GetIntPtrWIndowHandleFromSettings()
        {
            IntPtr hWnd_Processhandle;
            int ProcesId = Convert.ToInt32(QC4Common.Util.QCUtil.GetProcessId(Globals.ThisAddIn.Application.ActiveWorkbook));
            Process hostProcess = Process.GetProcessById(ProcesId);

            hWnd_Processhandle = hostProcess.MainWindowHandle;
            return hWnd_Processhandle;

        }
        public static bool checkUnprocessedNewQuestionDialog(Excel.Workbook workBook, IntPtr active)
        {
            if (checkUnprocessedNewQuestion(workBook))
            {
                System.Windows.Forms.DialogResult dialogResult = MessageDialog.InfoYesNo(AddinResource.ALERT_UN_PROCESSED_VARIABLE_EXIST, active);
                if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    MessageDialog.Info(AddinResource.ALERT_EXECUTE_DP_OR_DELET_VAR, active);
                    return true;
                }
            }
            return false;
        }
        public static bool checkUnprocessedNewQuestion(Excel.Workbook workBook)
        {
            List<QuestionSettings> qlist = Definitions.VariableDictionary.Values.ToList();
            QC4Common.DB.DBHelper.CheckIfColumnExists(workBook, qlist, out List<string> variables, out List<string> columns, out List<decimal> idss);
            if (variables.Count > 0) { return true; }
            return false;
        }
        public static int GetLastsortnumberForMultivariat(int sortnumbr = 0, int limit = 0, Excel.Workbook Workbook = null)
        {
            int count = 0;
            try
            {

                using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(Workbook)))//DataProcess.Sheet.Application.ActiveWorkbook
                {

                    string sql = "SELECT MAX(sort_no) FROM (select sort_no from multivariate where sort_no > " + sortnumbr.ToString() + " limit " + (limit).ToString() + ")";
                    count = DBHelper.ExecuteScalar(dbSource, sql);

                }
            }
            catch (Exception ex)
            {

            }
            return count;
            // throw new NotImplementedException();
        }
    }
}
