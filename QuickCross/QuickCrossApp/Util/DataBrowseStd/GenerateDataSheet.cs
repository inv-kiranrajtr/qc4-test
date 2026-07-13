using log4net;
using Macromill.QCWeb.COMOperate;
using NPOI.SS.Formula.Functions;
using QC4Common.DB;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Util.DataBrowseStd
{
    class GenerateDataSheet
    {
        private Excel.Application excelApp;
        private Excel.Workbook excelWorkbook;
		private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public delegate void OnWorkerMethodCompleteDelegate(double value, string status);
		public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
		public static bool IsSuccess = true;
		public double setprogersscount = 0;
		public GenerateDataSheet(Excel.Workbook workbook)
		{
			excelWorkbook = workbook;
		}
        public void LoadData()
        {
			
			ExcelOperate excelOperate = null;
			Excel.Application xlApp = null;
			Excel.Workbook targetBook=null;
			Excel.Workbooks targetBooks = null;
			Excel.Worksheet activesheet=null;
			double pogresscount = 0;
			try
            {

				string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + "DataBrowse_Std.xlsx";
				OnWorkerComplete(pogresscount, LocalResource.STD_DB_LOAD);
				excelOperate = new ExcelOperate();
                xlApp = excelOperate.Excel;
				targetBooks = xlApp.Workbooks;
                xlApp.Visible = false;
				pogresscount += 2;
				OnWorkerComplete(pogresscount, LocalResource.STD_DB_CREATE_EXCEL);
				targetBook = targetBooks.Add(tempPath);
				xlApp.ScreenUpdating = false;
				xlApp.EnableEvents = false;
				OnWorkerComplete(++pogresscount, LocalResource.STD_DB_CREATE_EXCEL_WB);
				activesheet = ExcelUtil.GetWorkSheetBySheetName(targetBook, "Data01");
                List<QC4Common.Model.QuestionSettings> questions = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == "Org") || (a.Value.QuestionFlag == "Imp")).Select(q => q.Value).ToList();
                string tableName = string.Empty;
				int datacount = 1;
				int dataaftercount = 1;
				bool isAn = Util.Definiotion.VariableDictionary.Any(x => x.Value.QuestionFlag == "An");
				if (questions.Count<= QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol)
				{
					activesheet = ExcelUtil.GetWorkSheetBySheetName(targetBook, "Data02");
					if(activesheet!=null)
					{
						xlApp.DisplayAlerts = false;
						activesheet.Delete();
					}
				}
				else
				{
					datacount++;
				}
				if (ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, Constants.SheetType.sh_Data01 + "(Processed)") == null)
				{
					activesheet = ExcelUtil.GetWorkSheetBySheetName(targetBook, "Data01(Processed)");
					if (activesheet != null)
					{
						xlApp.DisplayAlerts = false;
						activesheet.Delete();
					}
					activesheet = ExcelUtil.GetWorkSheetBySheetName(targetBook, "Data02(Processed)");
					if (activesheet != null)
					{
						xlApp.DisplayAlerts = false;
						activesheet.Delete();
					}
					dataaftercount = 0;
				}
				else
				{
					if (Util.Definiotion.VariableDictionary.Count <= QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol)
					{
						activesheet = ExcelUtil.GetWorkSheetBySheetName(targetBook, "Data02(Processed)");
						if (activesheet != null)
						{
							xlApp.DisplayAlerts = false;
							activesheet.Delete();
						}
					}
					else
					{
						dataaftercount++;
					}
				}
				pogresscount += 1;
				tableName = "answers";
				int noDP = 0;
				if (dataaftercount != 0)
				{
					OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_CREATE_DATASHEET);
				}
				else
				{
					noDP = 30;
					pogresscount = noDP;
					OnWorkerComplete(GetProgress(pogresscount),LocalResource.STD_DB_CREATE_DATASHEET);
				}
				if (QC4Common.Common.Constants.GlobalMode != "ja-JP," + Util.Constants.QCFont.MS_Gothic)
				{
					foreach (Excel.Worksheet sht in targetBook.Worksheets)
					{
						sht.Cells.EntireColumn.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];

						foreach (Excel.Shape item in sht.Shapes)
						{
							if (QC4Common.Common.Constants.Data.DataTopShapeName1 == item.Name)
							{
								string str = item.TextFrame2.TextRange.Text;
								item.TextFrame2.TextRange.Text = "";
								item.TextFrame2.TextRange.Font.NameFarEast = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
								item.TextFrame2.TextRange.Characters.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
								item.TextFrame2.TextRange.Text = str;
							}
							else if (QC4Common.Common.Constants.Data.DataTopShapeName2 == item.Name)
							{
								string str = item.TextFrame2.TextRange.Text;
								item.TextFrame2.TextRange.Text = "";
								item.TextFrame2.TextRange.Font.NameFarEast = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
								item.TextFrame2.TextRange.Characters.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
								item.TextFrame2.TextRange.Characters.Font.Size = 11;
								item.TextFrame2.TextRange.Text = str;
							}
						}
					}
				}
				SaveData(targetBook, tableName, questions, datacount,pogresscount);
				if (!isAn)
					pogresscount += 20;
				if (dataaftercount != 0)
				{
					pogresscount = 40+ noDP;
					OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_CREATE_DATASHEET1);
					tableName = "data_after_process";
					questions = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == "Org") || (a.Value.QuestionFlag == "Imp") || a.Value.QuestionFlag == "New").Select(q => q.Value).ToList();
					SaveData(targetBook, tableName, questions, dataaftercount,pogresscount);
				}
				if(isAn)
				{
					pogresscount = 72;
					OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_CREATE_MULTIVARIATESHEET);
					tableName = "multivariate";
					questions = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == "An")||a.Value.ItemId==0).Select(q => q.Value).ToList();
					SaveData(targetBook, tableName, questions, 1, pogresscount);
				}
				if (dataaftercount != 0)
				{
					activesheet = ExcelUtil.GetWorkSheetBySheetName(targetBook, "Data01(Processed)");
				}
				else
				{
					activesheet = ExcelUtil.GetWorkSheetBySheetName(targetBook, "Data01");
				}
				foreach(Excel.Worksheet sht in targetBook.Worksheets)
				{
					foreach (Excel.Shape shp in sht.Shapes)
					{
						if (QC4Common.Common.Constants.Data.DataTopShapeName1 == shp.Name)
						{
							shp.TextFrame2.TextRange.Text = LocalResource.TITLE_DATA_SHEET;
						}
						else if (QC4Common.Common.Constants.Data.DataTopShapeName2 == shp.Name)
						{
							shp.TextFrame2.TextRange.Text = LocalResource.DATA_TOP_BOX_TEXT;
						}
					}
				}
				OnWorkerComplete(100, LocalResource.STD_DB_CREATE_DATASHEET_FINISH);
				targetBook.Windows[1].Caption = "Data";
				xlApp.Caption = "Macromill - Quick-CROSS";
				xlApp.Visible = true;
				xlApp.EnableEvents = true;
				xlApp.DisplayAlerts = true;
				xlApp.ScreenUpdating = true;
				activesheet.Activate();
				xlApp.WindowState = Excel.XlWindowState.xlMaximized;
				
			}
            catch(Exception ex)
            {
				_log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
				try
				{
					excelOperate.Excel.EnableEvents = false;
					excelOperate.Excel.DisplayAlerts = false;
				}
				catch { }
				try
				{
					targetBook.Close();		
					excelOperate.Excel.Quit();
				}
				catch { }
				finally
				{
					excelOperate.Dispose();
				}
				OnWorkerComplete(100,"");
			}
			finally
			{
				COMWholeOperate.releaseComObject(ref activesheet);
				COMWholeOperate.releaseComObject(ref targetBook);
				COMWholeOperate.releaseComObject(ref targetBooks);
				COMWholeOperate.releaseComObject(ref xlApp);
				GC.Collect();
			}
        }
        private void SaveData(Excel.Workbook workbook, string tableName, List<QC4Common.Model.QuestionSettings> questions, int sheetNo,double pogresscount)
        {
			try
			{
				Excel.Worksheet Sheet;
				Excel.Range ItemCol;
				int maxCol = 0;
				string[] variableName;
				int maxRowCount =(int)(10000 * ((float)300 / questions.Count()));
				string connectionString = QC4Common.DB.DBHelper.GetConnectionString(excelWorkbook);
				questions = questions.OrderBy(a => a.RowNumber).ToList();
				DataTable dtCount = QC4Common.DB.DBHelper.GetDataTable("SELECT COUNT(*) FROM " + tableName, connectionString);
				int samplescount = Convert.ToInt32(dtCount.Rows[0][0].ToString());
				pogresscount += 1; 
				if (tableName == "multivariate")
					OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_CREATE_MULTIVARIATESHEET);
				else
					OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_CREATE_DATASHEET);
				var ary = questions.Where(q => q.Id != 0).Select(q => "q_" + q.Id).ToArray();
				string sql = "";
				if (tableName == "answers")
				{
					for (int i = 0; i < sheetNo; i++)
					{
						Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, "Data0" + (i + 1));
						QC4Common.Util.ExcelUtil.ClearContents(Sheet.Cells);
						Sheet.Cells.NumberFormat = "General";
						Sheet.Columns.Hidden = false;
					}
					variableName = questions.Select(a => a.Variable).ToArray();
					sql = String.Join(",", ary);
					sql = "select sample_id, " + sql + " from " + tableName;
					maxCol = questions.Count();
					int max = variableName.Length;
					string[,] varAry = new string[1, max];
					OnWorkerComplete(GetProgress(++pogresscount),LocalResource.STD_DB_CREATE_DATASHEET);

					if (max > QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol)
					{
						for (int i = 0; i < sheetNo; i++)
						{
							Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, "Data0" + (i + 1));
							if (i == 0)
							{
								for (int j = 0; j < QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol; j++)
								{
									varAry[0, j] = variableName[j];
								}
								Excel.Range r = Sheet.Range["A3"].Resize[1, QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol];
								ItemCol = r;
								r.Value = varAry;
								try
								{
									r.Columns.Hidden = false;
								}
								catch { }
								OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_CREATE_DATASHEET);
							}
							else
							{
								int lastcolumn = max - QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol;
								for (int j = 0,k= QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol; j <= lastcolumn; j++)
								{
									if (j == 0)
									{
										varAry[0, j] = variableName[0];
									}
									else
									{
										varAry[0, j] = variableName[k];
										k++;

									}
								}
								Excel.Range r = Sheet.Range["A3"].Resize[1, lastcolumn+1];
								ItemCol = r;
								r.Value = varAry;
								try
								{
									r.Columns.Hidden = false;
								}
								catch { }
								OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_CREATE_DATASHEET);

							}
				    	ItemCol.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
						}
						OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_CREATE_DATASHEET);
					}
					else
					{
						for (int i = 0; i < max; i++)
						{
							varAry[0, i] = variableName[i];
						}
						Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, "Data01");
						Excel.Range r = Sheet.Range["A3"].Resize[1, max];
						ItemCol = r;
						r.Value = varAry;
						try
						{
							r.Columns.Hidden = false;
						}
						catch { }
						ItemCol.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
						OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_CREATE_DATASHEET);
					}
				}
				else if(tableName == "multivariate")
				{
					OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_CREATE_MULTIVARIATESHEET);

					Excel.Worksheet xlSht = workbook.Sheets[1];
					xlSht.Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]); // copy
					workbook.Sheets[workbook.Sheets.Count].Name = Constants.SheetType.sh_Data_AN2;

                    Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, Constants.SheetType.sh_Data_AN2);
					QC4Common.Util.ExcelUtil.ClearContents(Sheet.Cells);
						Sheet.Cells.NumberFormat = "General";
						Sheet.Columns.Hidden = false;

					List<string> ansColumns = new List<string>();
					using (var conn = new SQLiteConnection(connectionString))
					{
						conn.Open();
						using (SQLiteCommand cmd = conn.CreateCommand())
						{
							cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);
							SQLiteDataReader reader = cmd.ExecuteReader();
							int nameIndex = reader.GetOrdinal("Name");
							DataTable data = new DataTable();
							data.Load(reader);
							int maxRow = data.Rows.Count;
							for (int i = 0; i < maxRow; i++)
							{
								string column = data.Rows[i][nameIndex].ToString();
								if ("sort_no" != column)
								{
									ansColumns.Add(column);
								}
							}
						}
						conn.Close();
						OnWorkerComplete(++pogresscount, LocalResource.STD_DB_CREATE_MULTIVARIATESHEET);
					}
					questions = questions.OrderBy(a => a.RowNumber).ToList();
					Dictionary<string, QuestionSettings> idQDict = new Dictionary<string, QuestionSettings>();
					foreach (QuestionSettings qs in questions)
					{
						string key = qs.Id == 0 ? "sample_id" : "q_" + qs.Id.ToString();
						idQDict.Add(key, qs);
					}
					maxCol = ansColumns.Count();
					string[,] varAry = new string[1, maxCol];
					int index = 0;
					sql = "";
					List<QuestionSettings> qList = new List<QuestionSettings>();
					foreach (KeyValuePair<string, QuestionSettings> kvp in idQDict)
					{
						if (ansColumns.Contains(kvp.Key))
						{
							{
								varAry[0, index++] = kvp.Value.Variable;
								sql += kvp.Key + ",";
								qList.Add(kvp.Value);
							}
						}
					}
					OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_CREATE_MULTIVARIATESHEET);
					questions = qList;
					variableName = questions.Select(a => a.Variable).ToArray();
					maxCol = questions.Count();
					sql = "select " + sql.Remove(sql.Length - 1) + " from " + tableName;
						OnWorkerComplete(++pogresscount, "Creating Multivariate sheet");
						for (int i = 0; i < maxCol; i++)
						{
							varAry[0, i] = variableName[i];
						}
						Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, Constants.SheetType.sh_Data_AN2);
						Excel.Range r = Sheet.Range["A3"].Resize[1, maxCol];
						ItemCol = r;
						r.Value = varAry;
						try
						{
							r.Columns.Hidden = false;
						}
						catch { }
						ItemCol.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
				}
				else
				{
					OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_CREATE_DATASHEET1);
					for (int i = 0; i < sheetNo; i++)
					{
						Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, "Data0" + (i + 1)+ "(Processed)");
						QC4Common.Util.ExcelUtil.ClearContents(Sheet.Cells);
						Sheet.Cells.NumberFormat = "General";
						Sheet.Columns.Hidden = false;
					}
					List<string> ansColumns = new List<string>();
					using (var conn = new SQLiteConnection(connectionString))
					{
						conn.Open();
						using (SQLiteCommand cmd = conn.CreateCommand())
						{
							cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);
							SQLiteDataReader reader = cmd.ExecuteReader();
							int nameIndex = reader.GetOrdinal("Name");
							DataTable data = new DataTable();
							data.Load(reader);
							int maxRow = data.Rows.Count;
							for (int i = 0; i < maxRow; i++)
							{
								string column = data.Rows[i][nameIndex].ToString();
								if ("sort_no" != column)
								{
									ansColumns.Add(column);
								}
							}
						}
						conn.Close();
						OnWorkerComplete(++pogresscount, LocalResource.STD_DB_CREATE_DATASHEET1);
					}
					questions = questions.OrderBy(a => a.RowNumber).ToList();
					Dictionary<string, QuestionSettings> idQDict = new Dictionary<string, QuestionSettings>();
					foreach (QuestionSettings qs in questions)
					{
						string key = qs.Id == 0 ? "sample_id" : "q_" + qs.Id.ToString();
						idQDict.Add(key, qs);
					}
					maxCol = ansColumns.Count();
					string[,] varAry = new string[1, maxCol];
					int index = 0;
					sql = "";
					List<QuestionSettings> qList = new List<QuestionSettings>();
					foreach (KeyValuePair<string, QuestionSettings> kvp in idQDict)
					{
						if (ansColumns.Contains(kvp.Key))
						{
							{
								varAry[0, index++] = kvp.Value.Variable;
								sql += kvp.Key + ",";
								qList.Add(kvp.Value);
							}
						}
					}
					OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_CREATE_DATASHEET1);
					questions = qList;
					variableName = questions.Select(a => a.Variable).ToArray();
					maxCol = questions.Count();
					sql = "select " + sql.Remove(sql.Length - 1) + " from " + tableName;
					if (maxCol > QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol)
					{
						for (int i = 0; i < sheetNo; i++)
						{
							OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_CREATE_DATASHEET1);
							Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, "Data0" + (i + 1)+ "(Processed)");
							if (i == 0)
							{
								for (int j = 0; j < QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol; j++)
								{
									varAry[0, j] = variableName[j];
								}
								Excel.Range r = Sheet.Range["A3"].Resize[1, QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol];
								ItemCol = r;
								r.Value = varAry;
								try
								{
									r.Columns.Hidden = false;
								}
								catch { }
							}
							else
							{
								int lastcolumn = maxCol - QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol;
								for (int j = 0, k = QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol; j <= lastcolumn; j++)
								{
									if (j == 0)
									{
										varAry[0, j] = variableName[0];
									}
									else
									{
										varAry[0, j] = variableName[k];
										k++;
									}
								}
								Excel.Range r = Sheet.Range["A3"].Resize[1, lastcolumn+1];
								ItemCol = r;
								r.Value = varAry;
								try
								{
									r.Columns.Hidden = false;
								}
								catch { }

							}
							ItemCol.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
						}
						OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_CREATE_DATASHEET1);
					}
					else
					{
						OnWorkerComplete(++pogresscount, LocalResource.STD_DB_CREATE_DATASHEET1);
						for (int i = 0; i < maxCol; i++)
						{
							varAry[0, i] = variableName[i];
						}
						Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, "Data01(Processed)");
						Excel.Range r = Sheet.Range["A3"].Resize[1, maxCol];
						ItemCol = r;
						r.Value = varAry;
						try
						{
							r.Columns.Hidden = false;
						}
						catch { }
						ItemCol.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
					}
					
				}
				
				DataTable dt=null;
				object[,] tablearray = null;
				int percentage = 20;
				if(tableName == "multivariate")
					percentage = 10;
				int nPageCount = samplescount % maxRowCount > 0 ? (samplescount / maxRowCount) + 1 : (samplescount / maxRowCount);
				double increment = (percentage / nPageCount) / sheetNo;
				for (int i = 0; i < sheetNo; i++)
				{
					int limit=0;
					if (tableName == "answers")
					{
						Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, "Data0" + (i + 1));
					}
					else if(tableName == "multivariate")
					{
						Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, Constants.SheetType.sh_Data_AN2);
					}
					else
					{
						Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, "Data0" + (i + 1) + "(Processed)");
					}
					if (maxCol >= QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol && tableName != "multivariate")
					{
						if (i != 0)
						{
							int lastcolumn = maxCol - QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol;
							Excel.Range dhE = Sheet.Range["A3"].Offset[0, lastcolumn + 2];
							if (dhE.Column < 8)
							{
								dhE = Sheet.Range["I3"];
								limit = 8;
							}
							else
							{
								limit = lastcolumn;
							}
							Excel.Range dhEndMost = dhE.End[Excel.XlDirection.xlToRight];
							Sheet.get_Range(dhE, dhEndMost).EntireColumn.Hidden = true;
							OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_LOAD_DATA);

						}
						else
						{
							limit = QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol;
							OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_LOAD_DATA);
						}
					}
					else
					{
						try
						{
							Excel.Range dhE = Sheet.Range["A3"].Offset[0, maxCol + 1];
							if (dhE.Column < 8)
							{
								dhE = Sheet.Range["I3"];
								limit = 8;
							}
							else
							{
								limit = maxCol;
							}
							Excel.Range dhEndMost = dhE.End[Excel.XlDirection.xlToRight];
							Sheet.get_Range(dhE, dhEndMost).EntireColumn.Hidden = true;
							OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_LOAD_DATA);
						}
						catch
						{
							limit = maxCol;
							OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_LOAD_DATA);
						}
					}
					Sheet.Cells.NumberFormat = "General";
					OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_LOAD_DATA);
					try
					{
						if (i == 0)
						{
                            int updateper = limit / 4;
                            int inc = 1;
                            FormatCell(limit, questions, Sheet);
                            /*
							for (int j = 0; j < limit; j++)
							{

								if (questions[j].AnswerType == Constants.AnswerType.FA)
								{
									Excel.Range c = Sheet.Cells[1, j + 1];
									c.EntireColumn.NumberFormat = "@";
								}
								else if (questions[j].AnswerType == Constants.AnswerType.D)
								{
									Excel.Range c = Sheet.Cells[1, j + 1];
									c.EntireColumn.NumberFormat = "yyyy/mm/dd hh:mm:ss";
								}
                                if(j==updateper)
                                {
                                    OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_LOAD_DATA);
                                    inc++;
                                    updateper = updateper * inc;
                                }
								
							}
                            */
                            OnWorkerComplete(GetProgress(++pogresscount), LocalResource.STD_DB_LOAD_DATA);
						}
						else
						{
							int data02posi = QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol;
                            FormatCell((maxCol - QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol) + 1, questions, Sheet, data02posi);
                            /*
							for (int m = 0; m <= maxCol - QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol; m++)
							{
								if (m == 0)
								{
									if (questions[0].AnswerType == Constants.AnswerType.FA)
									{
										Excel.Range c = Sheet.Cells[1, m + 1];
										c.EntireColumn.NumberFormat = "@";
									}
									else if (questions[0].AnswerType == Constants.AnswerType.D)
									{
										Excel.Range c = Sheet.Cells[1, m + 1];
										c.EntireColumn.NumberFormat = "yyyy/mm/dd hh:mm:ss";
									}
								}
								else
								{
									if (questions[data02posi].AnswerType == Constants.AnswerType.FA)
									{
										Excel.Range c = Sheet.Cells[1, m + 1];
										c.EntireColumn.NumberFormat = "@";
									}
									else if (questions[data02posi].AnswerType == Constants.AnswerType.D)
									{
										Excel.Range c = Sheet.Cells[1, m + 1];
										c.EntireColumn.NumberFormat = "yyyy/mm/dd hh:mm:ss";
									}
									data02posi++;
								}

							}
                            */
						}
					}
					catch { }
					pogresscount += 1;
					OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_LOAD_DATA);

					int sortNo = 0;
					int rowNumber = 4;

					Excel.Range startcell = Sheet.Cells[rowNumber, 1];
					if(maxCol>= QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol && tableName != "multivariate")
					{
						Excel.Range datarange;
						if (i==0)
						{
							rowNumber = 4;
							sortNo = 0;
							for (int m = 0; m < nPageCount; m++)
							{
								dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
								tablearray = LoadDataToArray(dt, questions, maxCol);
								datarange = startcell.Resize[tablearray.GetLength(0), QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol];
								datarange.Value = tablearray;
								datarange.WrapText = false;
								
								if (tableName == "answers")
								{
									sortNo = GetLastsortnumberForDataProcess(sortNo, maxRowCount);  

								}
								else
								{
									sortNo = GetLastsortnumberForDataAfterProcess(sortNo, maxRowCount);
								}
								rowNumber += tablearray.GetLength(0);
								startcell = Sheet.Cells[rowNumber, 1];
								tablearray = null;
								pogresscount = pogresscount+ increment;
								OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_LOAD_DATA);
							}
							pogresscount += 2;
							OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_LOAD_DATA);

						}
						else
						{
							rowNumber = 4;
							sortNo = 0;
							for (int m = 0; m < nPageCount; m++)
							{
								dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
								tablearray = LoadDataToArray(dt, questions, maxCol - QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol+1, i);
								datarange = startcell.Resize[tablearray.GetLength(0), tablearray.GetLength(1)];
								datarange.Value = tablearray;
								datarange.WrapText = false;
								if (tableName == "answers")
								{
									sortNo = GetLastsortnumberForDataProcess(sortNo, maxRowCount); 

								}
								else
								{
									sortNo = GetLastsortnumberForDataAfterProcess(sortNo, maxRowCount);
								}
								rowNumber += tablearray.GetLength(0);
								
								startcell = Sheet.Cells[rowNumber, 1];
								pogresscount = pogresscount + increment;
								tablearray = null;
								OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_LOAD_DATA);
							}
							
							pogresscount += 2;
							OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_LOAD_DATA);

						}

					}
					else
					{
						rowNumber = 4;
						sortNo = 0;
						for (int m = 0; m < nPageCount; m++)
						{
							dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
							tablearray = LoadDataToArray(dt, questions, maxCol, 0);
							Excel.Range datarange = startcell.Resize[tablearray.GetLength(0), tablearray.GetLength(1)];
							datarange.Value = tablearray;
							datarange.WrapText = false;

							int column = dt.Rows.Count - 1;
							if (tableName == "answers")
							{
								sortNo = GetLastsortnumberForDataProcess(sortNo, maxRowCount);
							}
							else if (tableName == "multivariate")
							{
								sortNo = GetLastsortnumberForMultivariate(sortNo, maxRowCount);

							}
							else
							{
								sortNo = GetLastsortnumberForDataAfterProcess(sortNo, maxRowCount);
							}
							rowNumber += tablearray.GetLength(0);
							startcell = Sheet.Cells[rowNumber, 1];
							tablearray = null;
							pogresscount = pogresscount + increment;
							OnWorkerComplete(GetProgress(pogresscount), LocalResource.STD_DB_LOAD_DATA);
						}
						

					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		private int GetLastsortnumberForMultivariate(int sortnumbr, int limit)
		{
			int count = 0;
			try
			{

				using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(excelWorkbook)))
				{

					string sql = "SELECT MAX(sort_no) FROM (select sort_no from multivariate where sort_no > " + sortnumbr.ToString() + " limit " + (limit).ToString() + ")";
					count = DBHelper.ExecuteScalar(dbSource, sql);

				}
			}
			catch (Exception ex)
			{

			}
			return count;
		}

		public Object[,] LoadDataToArray(DataTable dt, List<QuestionSettings> questions, int maxCol,int sheetno=0)
		{
			int maxRow = dt.Rows.Count;
			Object[,] array = new Object[maxRow, maxCol];
			int dtcolno=0;
			if(sheetno!=0)
			{
				dtcolno = QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol;
			}
			for (int c = 0; c < maxCol; c++, dtcolno++)
			{
				if(sheetno!=0&&c==0)
				{
					dtcolno = 0;
				}
				switch (questions[dtcolno].AnswerType)
				{
					case Constants.AnswerType.MA:
						for (int r = 0; r < maxRow; r++)
						{
							array[r, c] = dt.Rows[r][dtcolno] == null ? "" : ConverToMaAnser(dt.Rows[r][dtcolno].ToString());
						}
						break;
					case Constants.AnswerType.FA:
						for (int r = 0; r < maxRow; r++)
						{
							array[r, c] = dt.Rows[r][dtcolno] == null ? "" : dt.Rows[r][dtcolno].ToString().StartsWith("=") ? "'" + dt.Rows[r][dtcolno] : dt.Rows[r][dtcolno];
						}
						break;
					default:
						for (int r = 0; r < maxRow; r++)
						{
							array[r, c] = dt.Rows[r][dtcolno] == null ? "" : dt.Rows[r][dtcolno];
						}
						break;
				}
				if (sheetno != 0 && c == 0)
				{
					dtcolno = QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol-1;
				}
			}
			return array;
		}
		private string ConverToMaAnser(string str)
		{
			string res = "";
			if (String.IsNullOrEmpty(str))
			{
				return res;
			}

			if ("*".Equals(str))
			{
				return str;
			}

			char[] array = str.ToCharArray();
			int len = array.Length;
			for (int i = len - 1; i >= 0; i--)
			{
				if (array[i] == '1')
				{
					res += "," + (len - i);
				}
			}

			if (!String.IsNullOrEmpty(res))
			{
				res += ",";
			}
			return res;
		}
		private int GetLastsortnumberForDataAfterProcess(int sortnumbr = 0, int limit = 0)
		{
			int count = 0;
			try
			{

				using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(excelWorkbook)))
				{

					string sql = "SELECT MAX(sort_no) FROM (select sort_no from data_after_process where sort_no > " + sortnumbr.ToString() + " limit " + (limit).ToString() + ")";
					count = DBHelper.ExecuteScalar(dbSource, sql);

				}
			}
			catch (Exception ex)
			{

			}
			return count;
		}
		private int GetLastsortnumberForDataProcess(int sortnumbr = 0, int limit = 0)
		{
			int count = 0;
			try
			{

				using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(excelWorkbook)))
				{

					string sql = "SELECT MAX(sort_no) FROM (select sort_no from answers where sort_no > " + sortnumbr.ToString() + " limit " + (limit).ToString() + ")";
					count = DBHelper.ExecuteScalar(dbSource, sql);

				}
			}
			catch (Exception ex)
			{

			}
			return count;
		}
		private double GetProgress(double value)
		{
			if (value > setprogersscount)
			{
				if (value > 85)
				{
					return 85;
				}
				else
				{
					return value;
				}
			}
			else
			{
				return setprogersscount;
			}
		}

        private void FormatCell(int Current_Col_Count, List<QuestionSettings> questions, Excel.Worksheet sheet, int strtng = 0)
        {
            List<string> lst = new List<string>();
            int col = 0;
            bool is2Sheet = false;
            if (strtng > 0)
            {
                strtng--;
                is2Sheet = true;
            }
            for (int m = strtng; col < Current_Col_Count; m++, col++)
            {
                int strts = m + 1;
                if (is2Sheet || questions[m].AnswerType == Constants.AnswerType.N)
                {
                    if (m == strtng && questions[0].AnswerType == Constants.AnswerType.FA)
                    {
                        lst.Add("FA," + 1 + "," + 1);
                    }
                    is2Sheet = false;
                    int ct = 1;
                    col++;
                    int srt = col;
                    for (int t = strts; col < Current_Col_Count; t++, col++)
                    {
                        if (questions[t].AnswerType == Constants.AnswerType.N)
                        {
                            ct++;
                        }
                        else break;
                    }
                    m += (ct - 1);
                    lst.Add("N," + srt.ToString() + "," + ct.ToString());
                    col--;
                }
                else if (questions[m].AnswerType == Constants.AnswerType.FA)
                {
                    int ct = 1;
                    col++;
                    int srt = col;
                    for (int t = strts; col < Current_Col_Count; t++, col++)
                    {
                        if (questions[t].AnswerType == Constants.AnswerType.FA)
                        {
                            ct++;
                        }
                        else break;
                    }
                    m += (ct - 1);
                    lst.Add("FA," + srt.ToString() + "," + ct.ToString());
                    col--;
                }
                else if (questions[m].AnswerType == Constants.AnswerType.D)
                {
                    int ct = 1;
                    col++;
                    int srt = col;
                    for (int t = strts; col < Current_Col_Count; t++, col++)
                    {
                        if (questions[t].AnswerType == Constants.AnswerType.D)
                        {
                            ct++;
                        }
                        else break;
                    }
                    m += (ct - 1);
                    lst.Add("D," + srt.ToString() + "," + ct.ToString());
                    col--;
                }
            }
            for (int y = 0; y < lst.Count; y++)
            {
                string[] spl = lst[y].Split(',');
                int st = Convert.ToInt32(spl[1]);
                int ed = (st + Convert.ToInt32(spl[2])) - 1;
                if (spl[0] == Constants.AnswerType.FA)
                {
                    Excel.Range range = sheet.Cells[3, st];
                    Excel.Range range1 = sheet.Cells[3, ed];
                    Excel.Range rndg = sheet.get_Range(range, range1);
                    rndg.EntireColumn.NumberFormat = "@";
                }
                else if (spl[0] == Constants.AnswerType.N)
                {
                    Excel.Range range = sheet.Cells[3, st];
                    Excel.Range range1 = sheet.Cells[3, ed];
                    Excel.Range rndg = sheet.get_Range(range, range1);
                    //rndg.EntireColumn.NumberFormat = "@";
                    range = sheet.Cells[4, st];
                    range1 = sheet.Cells[4, ed];
                    rndg = sheet.get_Range(range, range1);
                    //rndg.EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    range = sheet.Cells[3, st];
                    range1 = sheet.Cells[3, ed];
                    rndg = sheet.get_Range(range, range1);
                    //rndg.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                }
                else
                {
                    Excel.Range range = sheet.Cells[3, st];
                    Excel.Range range1 = sheet.Cells[3, ed];
                    Excel.Range rndg = sheet.get_Range(range, range1);
                    rndg.EntireColumn.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                }
            }
        }

    }
}
