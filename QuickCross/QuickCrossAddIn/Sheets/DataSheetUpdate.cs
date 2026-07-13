using log4net;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.Tabulation;
using QC4Common.Common;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SQLite;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using ProgressBar = Qc4Launcher.Forms.ProgressBar;

namespace ExcelAddIn.Sheets
{
	class DataSheetUpdate
	{
		private Excel.Worksheet Sheet;
		private Excel.Workbook Workbook;
		private Excel.Application excelApp;
		private String connectionString;
		private double percentage = 0;
		private double incrementPercentage = 0;
		ProgressBar progress;
		public double setprogersscount = 0;
		int maxRowCount =5000;
		private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		public DataSheetUpdate(Excel.Application app,Excel.Workbook workbook, Excel.Worksheet sheet)
		{
			Sheet = sheet;
			Workbook = workbook;
			excelApp = app;
		}

		public void LoadDataWithPB(string tableName, List<QC4Common.Model.QuestionSettings> questions,bool DataBrowse,bool isMultiVariate=false)
		{
			if(isMultiVariate)
			{
				progress = new ProgressBar(Globals.ThisAddIn.Application.ActiveSheet);
				new Thread(() => LoadMultiData(questions)).Start();
				progress.ShowDialog();
			}
			else if(DataBrowse)
			{
				QC4Common.Util.DictionaryUtil.PopulateQSDictionary(Globals.ThisAddIn.Application.ActiveWorkbook);
				progress = new ProgressBar(Globals.ThisAddIn.Application.ActiveSheet);
			    new Thread(() => LoadData(tableName, questions)).Start();
				progress.ShowDialog();
			}
			else
			{
				QC4Common.Util.DictionaryUtil.PopulateQSDictionary(Workbook);
				progress = new ProgressBar(excelApp);
				new Thread(() => LoadData(tableName, questions)).Start();
				progress.ShowDialog();
				
			}
            
		}

		public void LoadMultiData(List<QC4Common.Model.QuestionSettings> questions)
		{
			try
			{
				string[] variableName;
				string sql = "";
				DataTable dt;
				Excel.Range startcell;
				Excel.Range datarange;
				int sortNo = 0;
				int rowNumber = 4;
				object[,] tablearray = null;
				Excel.Range ItemCol = null;
				int Current_Col_Count = questions.Count();
				maxRowCount = (int)(10000 * ((float)300 / questions.Count()));
				progress.OnWorkerMethodComplete(GetProgress(++percentage), AddinResource.PROGRESS_LOADING);
				var ary = questions.Where(q => q.Id != 0 &&( q.QuestionFlag.Equals("An"))).Select(q => "q_" + q.Id).ToArray();
                string connectionString = QC4Common.DB.DBHelper.GetConnectionString(Workbook);
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				DataTable dtCount = QC4Common.DB.DBHelper.GetDataTable("SELECT COUNT(*) FROM multivariate", connectionString);
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				int Current_Row_count = Convert.ToInt32(dtCount.Rows[0][0].ToString());
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				int nPageCount = Current_Row_count % maxRowCount > 0 ? (Current_Row_count / maxRowCount) + 1 : (Current_Row_count / maxRowCount);
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				variableName = questions.Where(a =>( a.QuestionFlag.Equals("An")|| a.Variable.Equals("SAMPLEID"))).Select(a => a.Variable ).ToArray();
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				sql = String.Join(",", ary);
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				percentage = 45;
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
                if (!string.IsNullOrEmpty(sql))
                {
                    sql = "select sample_id, " + sql + " from multivariate";
                }
                else
                {
                    sql = "select sample_id from multivariate";
                }
                int max = variableName.Length;
				string[,] varAry = new string[1, max];
				Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, Common.Constants.SheetType.sh_Data_AN2);
				QC4Common.Util.ExcelUtil.ClearContents(Sheet.Cells);
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				Sheet.Cells.NumberFormat = "General";
				Sheet.Columns.Hidden = false;

                for (int i = 0; i < max; i++)
				{
					varAry[0, i] = variableName[i];
				}
				Excel.Range r = Sheet.Range["A3"].Resize[1, max];
				ItemCol = r;

				r.Value = varAry;
				try
				{
					r.Columns.Hidden = false;
				}
				catch { }
				FormatCell(Current_Col_Count, questions, Sheet);

				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				incrementPercentage = (Current_Row_count / Current_Col_Count) / 1000;
				for (int m = 0; m < nPageCount; m++)
				{
					dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
					tablearray = LoadDataToArray(dt, questions, Current_Col_Count, 0);
					startcell = Sheet.Cells[rowNumber, 1];
					datarange = startcell.Resize[tablearray.GetLength(0), tablearray.GetLength(1)];
					datarange.Value = tablearray;
					datarange.WrapText = false;
					sortNo = Common.Util.GetLastsortnumberForMultivariat(sortNo, maxRowCount, Workbook);   
					rowNumber += tablearray.GetLength(0);
					datarange.WrapText = true;

				}

				Excel.Range dhE = Sheet.Range["A3"].Offset[0, Current_Col_Count + 1];
				if (dhE.Column < 8)
				{
					dhE = Sheet.Range["I3"];
				}
				Excel.Range dhEndMost = dhE.End[Excel.XlDirection.xlToRight];
				Sheet.get_Range(dhE, dhEndMost).EntireColumn.Hidden = true;

				ItemCol.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
				progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_COMPLETE);
			}
			catch(Exception ex)
			{
                progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_COMPLETE);
            }
		}

		private void LoadData(string tableName, List<QC4Common.Model.QuestionSettings> questions)
		{
			try
			{
				
				setprogersscount = 0;
				Common.Definitions.isSheetUpdating = true;
				Excel.Range ItemCol = null;
				Excel.Worksheet Sheet;
				DataTable Sheetdt;
				DataTable dt;
				object[,] tablearray = null;
				int sortNo = 0;
				string[] variableName;
				Excel.Range startcell;
				Excel.Range datarange;
				int rowNumber = 4;
				maxRowCount = (int)(10000 * ((float)300 / questions.Count()));
				progress.OnWorkerMethodComplete(GetProgress(++percentage), AddinResource.PROGRESS_LOADING);
				int Max_RowCount = Constants.ExcelRowColumnMax.ExcelMaxRow;
				int Max_ColumnCount = Constants.ExcelRowColumnMax.ExcelMaxCol;
				string connectionString = QC4Common.DB.DBHelper.GetConnectionString(Workbook);
				questions = questions.OrderBy(a => a.RowNumber).ToList();
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				DataTable dtCount = QC4Common.DB.DBHelper.GetDataTable("SELECT COUNT(*) FROM " + tableName, connectionString);
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				int Current_Row_count = Convert.ToInt32(dtCount.Rows[0][0].ToString());
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				int Current_Col_Count = questions.Count();
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				var ary = questions.Where(q => q.Id != 0).Select(q => "q_" + q.Id).ToArray();
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				string sql = "";
				int No_Datasheet_Rowwise = 1;
				int No_Datasheet_Colwise = 0;
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				if (Current_Row_count > Max_RowCount)
				{
					No_Datasheet_Rowwise += Current_Row_count / Max_RowCount;
					
				}
				if (Current_Col_Count > Max_ColumnCount)
				{
					No_Datasheet_Colwise = 1;
				}
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				percentage = 45;
				progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
				int nPageCount = Current_Row_count % maxRowCount > 0 ? (Current_Row_count / maxRowCount) + 1 : (Current_Row_count / maxRowCount);
				if (tableName == "answers")
				{
					variableName = questions.Select(a => a.Variable).ToArray();
					sql = String.Join(",", ary);
					if (sql != "")
					{
						sql = "select sample_id, " + sql + " from " + tableName;
					}
					else
						sql = "select sample_id from " + tableName;
					int max = variableName.Length;
					string[,] varAry = new string[1, max];
				    dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
					progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
					
					if (No_Datasheet_Rowwise > 1 || No_Datasheet_Colwise > 0)
					{
						for (int i=0;i<=No_Datasheet_Colwise;i++)
						{
							progress.OnWorkerMethodComplete(GetProgress(++percentage), "");

							for (int j=0; j < No_Datasheet_Rowwise;j++)
							{
								progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
								if (i == 0)
								{
									if (j == 0)
									{
										Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Common.Constants.SheetCodeName.Data01);
									}
									else
									{
										Sheet= QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, "Data01"+"("+j+")");
									}
								}
								else
								{
									if (j == 0)
									{
										Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, "Data02");
									}
									else
									{
										Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, "Data02" + "(" + j + ")");
									}
								}
								QC4Common.Util.ExcelUtil.ClearContents(Sheet.Cells);
								Sheet.Cells.NumberFormat = "General";
								Sheet.Columns.Hidden = false;
								if(No_Datasheet_Colwise>0)
								{
									if (i == 0)
									{
										sortNo = 0;
										rowNumber = 4;
										progress.OnWorkerMethodComplete(GetProgress(++percentage), "");

										for (int k = 0; k < Constants.ExcelRowColumnMax.ExcelMaxCol; k++)
										{
											varAry[0, k] = variableName[k];
										}
										Excel.Range r = Sheet.Range["A3"].Resize[1, Constants.ExcelRowColumnMax.ExcelMaxCol];
										ItemCol = r;
										r.Value = varAry;
										try
										{
											r.Columns.Hidden = false;
										}
										catch { }

										FormatCell(Constants.ExcelRowColumnMax.ExcelMaxCol, questions, Sheet);
										incrementPercentage =( dt.Rows.Count / Constants.ExcelRowColumnMax.ExcelMaxCol)/1000;
										for (int n = 0; n < nPageCount; n++)
										{
											dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
											tablearray = LoadDataToArray(dt, questions, Constants.ExcelRowColumnMax.ExcelMaxCol, j);
											startcell = Sheet.Cells[rowNumber, 1];
											datarange = startcell.Resize[tablearray.GetLength(0), Constants.ExcelRowColumnMax.ExcelMaxCol];
											datarange.Value = tablearray;
											datarange.WrapText = false;
											sortNo = Common.Util.GetLastsortnumberForDataProcess(sortNo, maxRowCount,Workbook);
											rowNumber += tablearray.GetLength(0);
											datarange.WrapText = true;
										}
									}
									else
									{
										sortNo = 0;
										rowNumber = 4;
										int lastcolumn = max - Constants.ExcelRowColumnMax.ExcelMaxCol;
										
										for (int k = 0, l = Constants.ExcelRowColumnMax.ExcelMaxCol; k <= lastcolumn; k++ )
										{
											if (k == 0)
											{
												varAry[0, k] = variableName[0];
											}
											else
											{
												varAry[0, k] = variableName[l];
												l++;
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
										incrementPercentage = (dt.Rows.Count / Constants.ExcelRowColumnMax.ExcelMaxCol)/1000;
										int data02posi = Constants.ExcelRowColumnMax.ExcelMaxCol;
										FormatCell((Current_Col_Count - Constants.ExcelRowColumnMax.ExcelMaxCol)+1, questions, Sheet, data02posi);
										
										for (int m = 0; m < nPageCount; m++)
										{
											 dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
											tablearray = LoadDataToArray(dt, questions, (Current_Col_Count - Constants.ExcelRowColumnMax.ExcelMaxCol)+1, j, 1);
											startcell = Sheet.Cells[rowNumber, 1];
											datarange = startcell.Resize[tablearray.GetLength(0), (Current_Col_Count - Constants.ExcelRowColumnMax.ExcelMaxCol)+1];
											datarange.Value = tablearray;
											datarange.WrapText = false;
											sortNo = Common.Util.GetLastsortnumberForDataProcess(sortNo, maxRowCount,Workbook);
											rowNumber += tablearray.GetLength(0);
											datarange.WrapText = true;
										}
									
										Excel.Range dhE = Sheet.Range["A3"].Offset[0, (Current_Col_Count - Constants.ExcelRowColumnMax.ExcelMaxCol) + 2];
										if (dhE.Column < 8)
										{
											dhE = Sheet.Range["I3"];
										}
										Excel.Range dhEndMost = dhE.End[Excel.XlDirection.xlToRight];
										Sheet.get_Range(dhE, dhEndMost).EntireColumn.Hidden = true;
										
									}
								}
								else
								{
									for (int k = 0; k < max; k++)
									{
										varAry[0, k] = variableName[k];
									}
									Excel.Range r = Sheet.Range["A3"].Resize[1, max];
									ItemCol = r;
									r.Value = varAry;
									try
									{
										r.Columns.Hidden = false;
									}
									catch { }
									FormatCell(Current_Col_Count, questions, Sheet);
									
									incrementPercentage = (dt.Rows.Count / Current_Col_Count)/100;
									for (int m = 0; m < nPageCount; m++)
									{
										dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
										tablearray = LoadDataToArray(dt, questions, Current_Col_Count, j);
										startcell = Sheet.Cells[rowNumber, 1];
										datarange = startcell.Resize[tablearray.GetLength(0), tablearray.GetLength(1)];
										datarange.Value = tablearray;
										datarange.WrapText = false;
										sortNo = Common.Util.GetLastsortnumberForDataProcess(sortNo, maxRowCount,Workbook);
										rowNumber += tablearray.GetLength(0);
										datarange.WrapText = true;

									}
									Excel.Range dhE = Sheet.Range["A3"].Offset[0, Current_Col_Count + 1];
									if (dhE.Column < 8)
									{
										dhE = Sheet.Range["I3"];
									}
									Excel.Range dhEndMost = dhE.End[Excel.XlDirection.xlToRight];
									Sheet.get_Range(dhE, dhEndMost).EntireColumn.Hidden = true;
									
								}
							ItemCol.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
							}
						}

					}
					else
					{
						Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Common.Constants.SheetCodeName.Data01);
						QC4Common.Util.ExcelUtil.ClearContents(Sheet.Cells);
						Sheet.Cells.NumberFormat = "General";
						Sheet.Columns.Hidden = false;
						for (int i = 0; i < max; i++)
						{
							varAry[0, i] = variableName[i];
						}
						Excel.Range r = Sheet.Range["A3"].Resize[1, max];
						ItemCol = r;

						r.Value = varAry;
						try
						{
							r.Columns.Hidden = false;
						}
						catch { }
						FormatCell(Current_Col_Count, questions, Sheet);

						incrementPercentage =( dt.Rows.Count / Current_Col_Count)/1000;
						for (int m = 0; m < nPageCount; m++)
						{
							dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
							tablearray = LoadDataToArray(dt, questions, Current_Col_Count, 0);
							startcell = Sheet.Cells[rowNumber, 1];
							datarange = startcell.Resize[tablearray.GetLength(0), tablearray.GetLength(1)];
							datarange.Value = tablearray;
							datarange.WrapText = false;
							sortNo = Common.Util.GetLastsortnumberForDataProcess(sortNo, maxRowCount, Workbook);
							rowNumber += tablearray.GetLength(0);
							datarange.WrapText = true;

						}

						Excel.Range dhE = Sheet.Range["A3"].Offset[0, Current_Col_Count + 1];
						if (dhE.Column < 8)
						{
							dhE = Sheet.Range["I3"];
						}
						Excel.Range dhEndMost = dhE.End[Excel.XlDirection.xlToRight];
						Sheet.get_Range(dhE, dhEndMost).EntireColumn.Hidden = true;
						
						ItemCol.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
					}

				}
				else
				{
					progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
					List<string> ansColumns = new List<string>();
					using (var conn = new SQLiteConnection(connectionString))
					{
						conn.Open();
						using (SQLiteCommand cmd = conn.CreateCommand())
						{
							cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

							SQLiteDataReader reader = cmd.ExecuteReader();
							int nameIndex = reader.GetOrdinal("Name");
							DataTable dtt = new DataTable();
							dtt.Load(reader);
							int maxRow = dtt.Rows.Count;

							for (int i = 0; i < maxRow; i++)
							{
								string column = dtt.Rows[i][nameIndex].ToString();
								if ("sort_no" != column)
								{

									ansColumns.Add(column);
								}
							}
						}
						conn.Close();
					}
					questions = questions.OrderBy(a => a.RowNumber).ToList();
					Dictionary<string, QuestionSettings> idQDict = new Dictionary<string, QuestionSettings>();
					foreach (QuestionSettings qs in questions)
					{
						string key = qs.Id == 0 ? "sample_id" : "q_" + qs.Id.ToString();


						idQDict.Add(key, qs);
					}
					progress.OnWorkerMethodComplete(GetProgress(++percentage), "");

					Current_Col_Count = ansColumns.Count();
					string[,] varAry = new string[1, Current_Col_Count];
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
					questions = qList;
					variableName= questions.Select(a => a.Variable).ToArray();
					Current_Col_Count = questions.Count();
					int max = variableName.Length;
					sql = "select " + sql.Remove(sql.Length - 1) + " from " + tableName;
					dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
					progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
					if (No_Datasheet_Rowwise > 1 || No_Datasheet_Colwise > 0)
					{
						Sheetdt = QC4Common.Util.ExcelUtil.GetDataSheetNamesAndPosition(Workbook);
						for (int i = 0; i <= No_Datasheet_Colwise; i++)
						{
							progress.OnWorkerMethodComplete(GetProgress(++percentage), "");

							for (int j = 0; j < No_Datasheet_Rowwise; j++)
							{
								progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
								if (i == 0)
								{
									if (j == 0)
									{
										Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, "Data01(Processed)");
									}
									else
									{
										Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, "Data01" + "(" + j + ")"+ "(Processed)");
									}
								}
								else
								{
									if (j == 0)
									{
										Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, "Data02(Processed)");
									}
									else
									{
										Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, "Data02" + "(" + j + ")"+ "(Processed)");
									}
								}
								QC4Common.Util.ExcelUtil.ClearContents(Sheet.Cells);
								Sheet.Cells.NumberFormat = "General";
								Sheet.Columns.Hidden = false;
								if (No_Datasheet_Colwise > 0)
								{
									if (i == 0)
									{
										sortNo = 0;
										rowNumber = 4;
										for (int k = 0; k < Constants.ExcelRowColumnMax.ExcelMaxCol; k++)
										{
											varAry[0, k] = variableName[k];
										}
										Excel.Range r = Sheet.Range["A3"].Resize[1, Constants.ExcelRowColumnMax.ExcelMaxCol];
										ItemCol = r;
										r.Value = varAry;
										try
										{
											r.Columns.Hidden = false;
										}
										catch { }

										FormatCell(Constants.ExcelRowColumnMax.ExcelMaxCol, questions, Sheet);
									
										incrementPercentage = (dt.Rows.Count / Constants.ExcelRowColumnMax.ExcelMaxCol)/1000;

										for (int m = 0; m < nPageCount; m++)
										{
											dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
											tablearray = LoadDataToArray(dt, questions, Constants.ExcelRowColumnMax.ExcelMaxCol, j);
											startcell = Sheet.Cells[rowNumber, 1];
											datarange = startcell.Resize[tablearray.GetLength(0), Constants.ExcelRowColumnMax.ExcelMaxCol];
											datarange.Value = tablearray;
											datarange.WrapText = false;
											sortNo = Common.Util.GetLastsortnumberForDataAfterProcess(sortNo, maxRowCount, Workbook);
											rowNumber += tablearray.GetLength(0);
											datarange.WrapText = true;

										}
									

									}
									else
									{
										int lastcolumn = Current_Col_Count - Constants.ExcelRowColumnMax.ExcelMaxCol;
										for (int k = 0, l = Constants.ExcelRowColumnMax.ExcelMaxCol; k <= lastcolumn; k++)
										{
											if (k == 0)
											{
												varAry[0, k] = variableName[0];
											}
											else
											{
												varAry[0, k] = variableName[l];
												l++;
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
										int data02posi = Constants.ExcelRowColumnMax.ExcelMaxCol;
										FormatCell((Current_Col_Count - Constants.ExcelRowColumnMax.ExcelMaxCol)+1, questions, Sheet, data02posi);
									
										incrementPercentage = (dt.Rows.Count / Current_Col_Count - Constants.ExcelRowColumnMax.ExcelMaxCol)/1000;
										sortNo = 0;
										rowNumber = 4;
										for (int m = 0; m < nPageCount; m++)
										{
											dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
											tablearray = LoadDataToArray(dt, questions, (Current_Col_Count - Constants.ExcelRowColumnMax.ExcelMaxCol)+1, j, 1);
											startcell = Sheet.Cells[rowNumber, 1];
											datarange = startcell.Resize[tablearray.GetLength(0), (Current_Col_Count - Constants.ExcelRowColumnMax.ExcelMaxCol)+1];
											datarange.Value = tablearray;
											datarange.WrapText = false;
											sortNo = Common.Util.GetLastsortnumberForDataAfterProcess(sortNo, maxRowCount, Workbook);
											rowNumber += tablearray.GetLength(0);
											datarange.WrapText = true;
										}
										try
										{
											Excel.Range dhE = Sheet.Range["A3"].Offset[0, (Current_Col_Count - Constants.ExcelRowColumnMax.ExcelMaxCol) + 2];
											if (dhE.Column < 8)
											{
												dhE = Sheet.Range["I3"];
											}
											Excel.Range dhEndMost = dhE.End[Excel.XlDirection.xlToRight];
											Sheet.get_Range(dhE, dhEndMost).EntireColumn.Hidden = true;
										}
										catch { }
										
										
									}
								}
								else
								{
									for (int k = 0; k < max; k++)
									{
										varAry[0, k] = variableName[k];
									}
									Excel.Range r = Sheet.Range["A3"].Resize[1, max];
									ItemCol = r;
									r.Value = varAry;
									try
									{
										r.Columns.Hidden = false;
									}
									catch { }
									FormatCell(Constants.ExcelRowColumnMax.ExcelMaxCol, questions, Sheet);
								
									incrementPercentage =( dt.Rows.Count / Current_Col_Count)/1000;
									sortNo = 0;
									rowNumber = 4;
									for (int m = 0; m < nPageCount; m++)
									{
										dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
										tablearray = LoadDataToArray(dt, questions, Current_Col_Count, j);
										startcell = Sheet.Cells[rowNumber, 1];
										datarange = startcell.Resize[tablearray.GetLength(0), tablearray.GetLength(1)];
										datarange.Value = tablearray;
										datarange.WrapText = false;
										sortNo = Common.Util.GetLastsortnumberForDataAfterProcess(sortNo, maxRowCount, Workbook);
										rowNumber += tablearray.GetLength(0);
										datarange.WrapText = true;
									}
									try
									{
										Excel.Range dhE = Sheet.Range["A3"].Offset[0, Current_Col_Count + 1];
										if (dhE.Column < 8)
										{
											dhE = Sheet.Range["I3"];
										}
										Excel.Range dhEndMost = dhE.End[Excel.XlDirection.xlToRight];
										Sheet.get_Range(dhE, dhEndMost).EntireColumn.Hidden = true;
									}
									catch { }
								
								}
								ItemCol.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
							}
						}

					}
					else
					{
						progress.OnWorkerMethodComplete(GetProgress(++percentage), "");
						Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, "Data01(Processed)");
						QC4Common.Util.ExcelUtil.ClearContents(Sheet.Cells);
						Sheet.Cells.NumberFormat = "General";
						Sheet.Columns.Hidden = false;
						for (int i = 0; i < max; i++)
						{
							varAry[0, i] = variableName[i];
						}
						Excel.Range r = Sheet.Range["A3"].Resize[1, max];
						ItemCol = r;

						r.Value = varAry;
						try
						{
							r.Columns.Hidden = false;
						}
						catch { }
						FormatCell(Current_Col_Count, questions, Sheet);
						
						incrementPercentage = (dt.Rows.Count / Current_Col_Count)/1000;
						sortNo = 0;
						rowNumber = 4;
						for (int m = 0; m < nPageCount; m++)
						{
							dt = QC4Common.DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " order by sort_no limit " + maxRowCount, connectionString);
							tablearray = LoadDataToArray(dt, questions, Current_Col_Count, 0);
							startcell = Sheet.Cells[rowNumber, 1];
							datarange = startcell.Resize[tablearray.GetLength(0), tablearray.GetLength(1)];
							datarange.Value = tablearray;
							datarange.WrapText = false;
							sortNo = Common.Util.GetLastsortnumberForDataAfterProcess(sortNo, maxRowCount, Workbook);
							rowNumber += tablearray.GetLength(0);
							datarange.WrapText = true;
						}
						
						Excel.Range dhE = Sheet.Range["A3"].Offset[0, Current_Col_Count + 1];
						if (dhE.Column < 8)
						{
							dhE = Sheet.Range["I3"];
						}
						Excel.Range dhEndMost = dhE.End[Excel.XlDirection.xlToRight];
						Sheet.get_Range(dhE, dhEndMost).EntireColumn.Hidden = true;
						
						ItemCol.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
					}


				}

				progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_COMPLETE);
				
			}
			catch(Exception ex)
			{
				_log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
				progress.OnWorkerMethodComplete(100, AddinResource.PROGRESS_FAIL);
			}
		}

		private void FormatCell(int Current_Col_Count,List<QuestionSettings> questions,Excel.Worksheet sheet,int strtng =0)
		{
			List<string> lst = new List<string>();
			int col = 0;
			bool is2Sheet = false;
			if (strtng > 0)
			{
				strtng--;
				is2Sheet = true;
			}
			for (int m = strtng; col < Current_Col_Count; m++,col++)
			{
				int strts = m+1;
				if (is2Sheet|| questions[m].AnswerType == Constants.AnswerType.N)
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
				else if(questions[m].AnswerType == Constants.AnswerType.FA)
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

		public Object[,] LoadDataToArray(DataTable dt, List<QuestionSettings> questions, int maxCol, int Rowturn, int sheetno = 0)
		{
		     int maxRow = dt.Rows.Count;
			if(maxRow>Constants.ExcelRowColumnMax.ExcelMaxRow)
			{
			   if(Rowturn==0)
				{
					maxRow = Constants.ExcelRowColumnMax.ExcelMaxRow;
				}
				else
				{
					int n = Rowturn * Constants.ExcelRowColumnMax.ExcelMaxRow;
					maxRow = maxRow - n;
				}

			}
			int dtcolno = 0;
			if (sheetno != 0)
			{
				dtcolno = Constants.ExcelRowColumnMax.ExcelMaxCol;
			}
			Object[,] array = new Object[maxRow, maxCol];
			int rowcount;
			for (int c = 0; c < maxCol; c++,dtcolno++)
			{
				rowcount= Rowturn * Constants.ExcelRowColumnMax.ExcelMaxRow;
				if(sheetno!=0&&c==0)
				{
					dtcolno = 0;
				}
				switch (questions[dtcolno].AnswerType)
				{
					case Constants.AnswerType.MA:
						for (int r = 0; r < maxRow; r++,rowcount++)
						{
							array[r, c] = dt.Rows[rowcount][dtcolno] == null ? "" : ConverToMaAnser(dt.Rows[rowcount][dtcolno].ToString());
						}
						break;
					case Constants.AnswerType.FA:
						for (int r = 0; r < maxRow; r++,rowcount++)
						{
							array[r, c] = dt.Rows[rowcount][dtcolno] == null ? "" : dt.Rows[rowcount][dtcolno].ToString().StartsWith("=") ? "'" + dt.Rows[rowcount][dtcolno] : dt.Rows[rowcount][dtcolno];
						}
						break;
					default:
						for (int r = 0; r < maxRow; r++,rowcount++)
						{
							array[r, c] = dt.Rows[rowcount][dtcolno] == null ? "" : dt.Rows[rowcount][dtcolno];
						}
						break;
				}
				if(sheetno != 0 && c == 0)
				{
					dtcolno = Constants.ExcelRowColumnMax.ExcelMaxCol-1;
				}
				percentage += incrementPercentage;
				progress.OnWorkerMethodComplete(GetProgress(percentage), "");
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
		private double GetProgress(double value)
		{
			if(value<=0)
			{
				return setprogersscount;
			}
			if (value > setprogersscount)
			{
				if (value > 85)
				{
					return 85;
				}
				else
				{
					setprogersscount = value;
					return value;
				}
			}
			else
			{
				return setprogersscount;
			}
		}
	}
}
