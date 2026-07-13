using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Qc4Launcher.Util;
using System.Windows;
using log4net;
using Questions = QC4Common.Model.Model.Question;
using System.Data;
using System.Data.SQLite;
using System.ComponentModel;

namespace Qc4Launcher.Classes
{
	class SwapData : ProgressBar
	{
		private Excel.Workbook TargetWorkbook { get; set; }
		private Excel.Workbook SourceWorkbook { get; set; }
		private string TargetPath { get; set; }
		private string SourcePath { get; set; }
		private string Extension { get; set; }
		private List<String> ErrorList = new List<string>();
		private List<int> DeleteRowList = new List<int>();
		private Excel.Range TargetVaribaleRows;
		private List<String> VariableList = new List<string>();
		private readonly ILog _log;

        internal SwapData(System.Windows.Window parent)
		{
			ParentWindow = parent;
			_log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		internal void SwapDataMain(Excel.Workbook targetBook, string targetPath, string selectedFile)
		{
            TargetWorkbook = targetBook;
			TargetPath = targetPath;
            InitProgressBar(false); 
			string path = ShowFileDialog();
			if (null == path)
			{
				return;
			}
			if (selectedFile.Equals(path))
			{
				MessageDialog.ErrorOk(LocalResource.SD_ALERT_SAME_FILENAME);
				return;
			}
           
            new Thread(() => SwapDataInit(path)).Start();
            progress.ShowDialog();
		}

		private void SwapDataInit(string path) //changed for phase 3
		{
            try
			{
                UpdateProgressBar(++percentage, LocalResource.PB_SD_START);
                UpdateProgressBar(++percentage, LocalResource.SD_PB_PROCESS_SHEETCHECK);
                if (!OpenWorkBook(path))
                {
                    UpdateProgressBar(100, LocalResource.PB_SD_FAILED);
                    MessageDialog.ErrorOk(LocalResource.SD_ALERT_CHOOSE_OTHER_FILE);
                    return;
                }
                UpdateProgressBar(percentage += 10, LocalResource.SD_PB_DONE_SHEETCHECK);

                UpdateProgressBar(percentage += 5, LocalResource.SD_PB_PROCESS_INTEGRITY_CHECK);
                if (!SheetCheck(out List<int> errorIds))
                {
                    SourceWorkbook.Close();
                    UpdateProgressBar(100, LocalResource.SD_PB_FAILD);
                    MessageDialog.ErrorOk(LocalResource.SD_ERROR_FAILED);

                    return;
                }
                UpdateProgressBar(percentage += 5, LocalResource.SD_PB_PROCESS_QS_UPDATE);
                QuestionCheck(SourceWorkbook, TargetWorkbook);
                UpdateProgressBar(percentage += 10, LocalResource.SD_PB_DONE_QS_UPDATE);
                UpdateProgressBar(percentage += 5, LocalResource.SD_PB_DELETE_QUESTIONS);
                DeleteUnwantedQuestions();
                UpdateProgressBar(percentage += 5, LocalResource.SD_PB_DELETE_SHEET);
                TargetWorkbook.Unprotect(Constants.Password);
                bool displayAlert = TargetWorkbook.Application.DisplayAlerts;
                TargetWorkbook.Application.DisplayAlerts = false;
                foreach (Excel.Worksheet sheet in QCWorkbookHelper.GetDataSheetsAfterAndBefore(TargetWorkbook))
                {
                    sheet.Delete();
                }
                TargetWorkbook.Application.DisplayAlerts = displayAlert;
                TargetWorkbook.Password = Constants.Password;
                TargetWorkbook.Protect(Constants.Password, true);
                UpdateProgressBar(percentage += 8, LocalResource.SD_PB_PROCESS_DB_UPDATE);
                ExecuteSwapData();
                UpdateProgressBar(percentage += 14, LocalResource.SD_PB_PROCESS_INTEGRITY_CHECK);
                DataIntegrityCheck();

                UpdateProgressBar(percentage += 2, LocalResource.SD_PB_FINALIZING);
				TargetWorkbook.Application.EnableEvents = false;
				SourceWorkbook.Close();
                TargetWorkbook.Application.EnableEvents = true;
                UpdateProgressBar(percentage += 2, LocalResource.SP_PB_ALMOST_FINISH);

				if (Extension == Constants.EXT_QC4)
				{
					System.IO.Directory.Delete(SourcePath, true);
				}

				UpdateProgressBar(100, LocalResource.SP_PB_COMPLETED);
				
				if (errorIds.Count > 0)
				{
                    string sql = "SELECT variable from question where id = " + String.Join(" or id = ", errorIds);
                    DataTable dt = DB.DBHelper.GetDataTable(sql, DB.DBHelper.GetConnectionString(TargetPath + "\\" + Constants.TemplateFile.DB_FIlE));
					int max = dt.Rows.Count;
					for (int i = 0; i < max; i++)
					{
						if (!ErrorList.Contains(dt.Rows[i][0]))
						{
							ErrorList.Add(Convert.ToString(dt.Rows[i][0]));
						}
					}
				}

				if (ErrorList.Count > 0)
				{
					string msg = String.Join(",", ErrorList);
					MessageDialog.Warning(string.Format(LocalResource.SD_ALERT_MISMATCH_VARIABLES, msg));
				}
				else
				{
					MessageDialog.Info(LocalResource.SD_ALERT_SUCESS);
				}
				QC4Common.Common.CommonFlag.SetIsDataUpdated(TargetWorkbook, false);
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
				UpdateProgressBar(100, LocalResource.SD_PB_FAILD);
				MessageDialog.ErrorOk(LocalResource.PB_SD_FAILED);
			}
		}

		private string ShowFileDialog()
		{
			Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
			ofd.Multiselect = false;
			ofd.Filter = "All(*.qcg; *.qc3x; *.qcgx; *.qc3; *.qc4)| *.qcg; *.qc3x; *.qcgx; *.qc3; *.qc4; | QC3 files(*.qcg; *.qc3x; *.qcgx; *.qc3)| *.qcg; *.qc3x; *.qcgx; *.qc3; | QC4 files(*.QC4) | *.qc4";
			if (ofd.ShowDialog() != true)
			{
				return null;
			}
			return ofd.FileName;
		}

		private bool OpenWorkBook(string selectedPath)
		{
			Extension = System.IO.Path.GetExtension(selectedPath);
            Excel.Application app = new Excel.Application();
			if (Constants.EXT_QC4 == Extension)
			{
				SourcePath = QcFileHelper.ExtractFile(selectedPath, Constants.PathName.FileSwapData);
				string temp = SourcePath + "\\" + Constants.TemplateFile.QC4_Template;
				//to avoid excel error with same file name
				selectedPath = FileUtil.GenerateFileName(1, "", System.IO.Path.GetFileNameWithoutExtension(selectedPath), SourcePath);
				System.IO.File.Move(temp, selectedPath);  
				SourceWorkbook = ExcelUtil.OpenWorkbok(selectedPath, app);
			}
			else
			{
				SourceWorkbook = ExcelUtil.OpenWorkbok(selectedPath, app);
			}

			if (null == SourceWorkbook)
			{
				return false;
			}
			return true;
		}

		private bool SheetCheck(out List<int> errorIds)
		{
			Excel.Worksheet targetQB = ExcelUtil.GetWorkSheetByCodeName(TargetWorkbook, Constants.SheetCodeName.QuestionSettingB);
			Excel.Worksheet sourceQB = ExcelUtil.GetWorkSheetByCodeName(SourceWorkbook, Constants.SheetCodeName.QuestionSettingB);
			Excel.Range targetStart = targetQB.Cells[Constants.QS.START_ROW, Constants.QS.COL_VARIABLE];
			Excel.Range sourceStart = sourceQB.Cells[Constants.QS.START_ROW, Constants.QS.COL_VARIABLE];
			Excel.Range targetEnd = ExcelUtil.EndxlUp(targetStart);
			Excel.Range sourceEnd = ExcelUtil.EndxlUp(sourceStart);
			errorIds = new List<int>();

			if (targetEnd.Row != sourceEnd.Row)
			{
				return false;
			}
			//ErrorList = new List<string>();
			Object[,] tArray = targetStart.Resize[targetEnd.Row - Constants.QS.ROW_HEADER, 3].Value2;
			Object[,] sArray = sourceStart.Resize[sourceEnd.Row - Constants.QS.ROW_HEADER, 3].Value2;

			string str = "";
			int max = targetEnd.Row - Constants.QS.ROW_HEADER;
			for (int i = 1; i <= max; i++)
			{
				str = String.Empty;
				try
				{
					if (tArray[i, 1].ToString() != sArray[i, 1].ToString())
					{
						return false;
					}

					if (tArray[i, 2].ToString() != sArray[i, 2].ToString())
					{
						errorIds.Add(i - 1);
					}
					else if (Convert.ToString(tArray[i, 3]) != Convert.ToString(sArray[i, 3]))
					{
						errorIds.Add(i - 1);
					}

				}
				catch (Exception ex)
				{
					_log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
					return false;
				}
			}
			return true;
		}

		private bool IsNullOrEmpty(Object obj, out string str)
		{
			str = String.Empty;
			if (null != obj)
			{
				str = obj.ToString();
				return string.IsNullOrEmpty(str);

			}
			return true;
		}

		private void QuestionCheck(Excel.Workbook source, Excel.Workbook target)
		{
			DeleteRowList = new List<int>();
			VariableList = new List<string>();
			Excel.Worksheet qsTarget = ExcelUtil.GetWorkSheetByCodeName(target, Constants.SheetCodeName.QuestionSetting);
			Excel.Range targetStart = qsTarget.Cells[Constants.QS.START_ROW, Constants.QS.COL_VARIABLE];
			Excel.Range targetEnd = ExcelUtil.EndxlUp(targetStart);
			TargetVaribaleRows = targetStart;
			targetStart = targetStart.Offset[0, -5];
			Excel.Range targetArray = targetStart.Resize[targetEnd.Row - Constants.QS.ROW_HEADER, 12];
			Object[,] targetAry = targetArray.Value2;
			int minRow = targetAry.GetLength(0); //(targetEnd.Row > sourceEnd.Row ? sourceEnd.Row : targetEnd.Row) - Constants.QS.START_ROW;
			for (int i = 1; i <= minRow; i++)
			{
				string flag = targetAry[i, Constants.QS.COL_NEW] == null ? "" : targetAry[i, Constants.QS.COL_NEW].ToString();
				if (flag == "Imp" || flag == "An")
				{
					DeleteRowList.Add(i + Constants.QS.ROW_HEADER);
					continue;
				}
				if (flag == "New")
				{
					continue;
				}
				VariableList.Add(targetAry[i, Constants.QS.COL_VARIABLE].ToString());
			}
		}
		/// <summary>
		/// Method to execute Swap data
		/// </summary>
		private void ExecuteSwapData()
		{
			DB.SwapData swap = new DB.SwapData();
			if (Constants.EXT_QC4 == Extension)
			{
				swap.DataFromDB(SourcePath + "\\" + Constants.TemplateFile.DB_FIlE, TargetPath + "\\" + Constants.TemplateFile.DB_FIlE , SourceWorkbook); // changed for phase3
			}
			else
			{
				swap.DropTable("answers", TargetPath + "\\" + Constants.TemplateFile.DB_FIlE); 
				Qc3Parse swapLogic = new Qc3Parse(TargetPath, SourceWorkbook);
				List<Qc3Parse.QDataDetail> qDataDetails = swapLogic.GetQDataDetail(out List<Excel.Worksheet> dataSheets, out List<int> dataSheetMaxCol, false);
				swapLogic.DataUpdation(dataSheets, qDataDetails, dataSheetMaxCol, false);
			}
            swap.UpdateWeightBack(TargetWorkbook, TargetPath + "\\" + Constants.TemplateFile.DB_FIlE);
			swap.DeleteDataAfterProcessTableAndMultivariate(TargetPath + "\\" + Constants.TemplateFile.DB_FIlE, TargetWorkbook);
			Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(TargetWorkbook);
		}

		private void DeleteUnwantedQuestions()
		{
			int max = DeleteRowList.Count();
			DeleteRowList.Reverse();
			Excel.Worksheet qsTarget = ExcelUtil.GetWorkSheetByCodeName(TargetWorkbook, Constants.SheetCodeName.QuestionSetting);
			string sql = String.Empty;
			string prefix = "DELETE FROM question where ";

			for (int i = 0; i < max; i++)
			{
				Excel.Range row = qsTarget.Rows[DeleteRowList[i]];
				try
				{
					Excel.Name name = row.Name;
					string str = name.Name;
					if (QC4Common.Util.QSUtil.IsRowName(str))
					{
						sql += prefix + " id = " + Convert.ToInt32(str.Substring(str.Length - 5));
						prefix = " or ";
					}
				}
				catch { }
				row.Delete();
			}

			if (!string.Empty.Equals(sql))
			{
				DB.DBHelper.ExecuteQuery(sql, DB.DBHelper.GetConnectionString(TargetPath + "\\" + Constants.TemplateFile.DB_FIlE));
			}
		}

		private void DataIntegrityCheck()
		{
			DB.SwapData swap = new DB.SwapData();
			List<Questions> questions = swap.GetOrgQuestions(DB.DBHelper.GetConnectionString(TargetPath + "\\" + Constants.TemplateFile.DB_FIlE));
			int max = questions.Count();

			using (SQLiteConnection connection = new SQLiteConnection(DB.DBHelper.GetConnectionString(TargetPath + "\\" + Constants.TemplateFile.DB_FIlE)))
			{
				connection.Open();
				bool result;
				for (int i = 0; i < max; i++)
				{
                    result = true;
					Questions question = questions[i];
					switch (question.AnswerType)
					{
						case Constants.AnswerType.SA:
							result = SAIntegrityCheck(question, connection);
							break;
						case Constants.AnswerType.MA:
							result = MAIntegrityCheck(question, connection);
							break;
						case Constants.AnswerType.N:
							result = NIntegrityCheck(question, connection);
							break;
					}
                    if (!result)
					{
						ErrorList.Add(question.Variable);
					}
				}
                connection.Close();
			}
        }

		private bool NIntegrityCheck(Questions question, SQLiteConnection connection)
		{
			long count = 0;
			string columnName = question.Id == 0 ? "sample_id" : "q_" + question.Id;
			string sql = "SELECT count(" + columnName + ") FROM answers where " + columnName + " != 0 AND " + columnName + " != '*' AND " + columnName + " is not null AND " + columnName + " != ''";
			DataTable dt = DB.DBHelper.GetDataTable(sql, connection);
			count = Convert.ToInt32(dt.Rows[0][0]);

			sql = "SELECT " + columnName + " FROM answers WHERE abs(" + columnName + ") > 0 AND " + columnName + " != 0 AND " + columnName + " != '*' AND " + columnName + " is not null AND " + columnName + " != ''";
			dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
			if ((count != 0 && dt == null) || dt.Rows.Count != count)
			{
				//unwanted value
				return false;
			}

			int rows = dt.Rows.Count;
			for (int i = 0; i < rows; i++)
			{
				if (!Double.TryParse(dt.Rows[i][0].ToString(), out double val))
				{
					//unwanted value
					return false;
				}
			}
			return true;
		}

		private bool SAIntegrityCheck(Questions question, SQLiteConnection connection)
		{
			long count = 0;
			string columnName = question.Id == 0 ? "sample_id" : "q_" + question.Id;
			string sql = "select count(" + columnName + ") from answers where (" + columnName + " glob '*[^0-9]*' and " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null) or " + columnName + " = 0";
			DataTable dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
			count = Convert.ToInt32(dt.Rows[0][0]);
			if (count > 0)
			{
				//unwanted value
				return false;
			}

			sql = "select count(value) from (select CAST(" + columnName + " as integer) as value from answers where " + columnName + " glob '[0-9]' or " + columnName + " glob '[0-9][0-9]' or " + columnName + " glob '[0-9][0-9][0-9]') where value < 1 or value > " + question.CategoryCount + "";
			dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
			count = Convert.ToInt32(dt.Rows[0][0]);
			if (count > 0)
			{
				//category count not match
				return false;
			}
			return true;
		}

		private bool MAIntegrityCheck(Questions question, SQLiteConnection connection)
		{
			long count = 0;
			string columnName = question.Id == 0 ? "sample_id" : "q_" + question.Id;
			string sql = "select count(" + columnName + ") from answers where " + columnName + " glob '*[^0-1]*' and " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null";
			DataTable dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
			count = Convert.ToInt32(dt.Rows[0][0]);
			if (count > 0)
			{
				//unwanted value
				return false;
			}

			sql = "select count(" + columnName + ") from answers where " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null and " + columnName + " glob '*1*" + String.Concat(Enumerable.Repeat("?", question.CategoryCount)) + "'";
			dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
			count = Convert.ToInt32(dt.Rows[0][0]);
			if (count > 0)
			{
				//category count not match
				return false;
			}
			return true;
		}
	}
}
