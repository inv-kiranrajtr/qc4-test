using log4net;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Questions = QC4Common.Model.Model.Question;


namespace Qc4Launcher.DB
{
	internal class SwapData
	{
		private readonly ILog _log;

		internal SwapData()
		{
			_log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		/// <summary>
		/// Update target answer table value in database from source database
		/// </summary>
		/// <param name="sourcePath"></param>
		/// <param name="targetPath"></param>
		internal void DataFromDB(string sourcePath,string targetPath, Excel.Workbook sourceWorkbook)
		{
			using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(sourcePath)))
			{
				using (SQLiteConnection dbTarget = DBHelper.GetConnection(DBHelper.GetConnectionString(targetPath)))
				{
					try
					{
						dbSource.Open();
						dbTarget.Open();
						//string sql = "ATTACH '" + targetPath + "' AS target";
						//DBHelper.ExecuteQuery(sql, dbSource);

						string sql = "SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'answers'";
						DataTable dt = DBHelper.GetDataTable(sql, dbSource);
						if (dt.Rows.Count == 0)
						{
							return;
						}
                        sql = "DROP TABLE IF EXISTS answers";
                        DBHelper.ExecuteQuery(sql, dbTarget);
                       
                        sql = dt.Rows[0][0].ToString();
                        DBHelper.ExecuteQuery(sql, dbTarget);
                       
                        sql = "ATTACH '" + sourcePath + "' AS source";
						DBHelper.ExecuteQuery(sql, dbTarget);

						sql = "INSERT INTO answers SELECT * FROM source.answers";
						DBHelper.ExecuteQuery(sql, dbTarget);
                    }
                    catch (Exception ex)
					{
						//TODO Close connections
						_log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
						//return false;
					}
					finally
					{
						dbSource.Close();
						dbTarget.Close();
					}
				}
			}
		}

		/// <summary>
		/// Update target answer table value in database from source Data Sheets
		/// </summary>
		/// <param name="sourceWorkbook"></param>
		/// <param name="targetPath"></param>
		internal void DataFromSheet(Excel.Workbook sourceWorkbook, string targetPath,bool convert = true)
		{
			Excel.Worksheet qb;

			string sheetCode = convert ? Constants.SheetCodeName.QuestionSettingB : Constants.SheetCodeName.QuestionSetting;
			qb = ExcelUtil.GetWorkSheetByCodeName(sourceWorkbook, sheetCode);
			
			Excel.Range qbStart = qb.Cells[Constants.QS.START_ROW, Constants.QS.COL_VARIABLE];
			Excel.Range qbEnd = ExcelUtil.EndxlUp(qbStart);
			int totalCol = qbEnd.Row - Constants.QS.ROW_HEADER;

			using (SQLiteConnection connection = DBHelper.GetConnection(DBHelper.GetConnectionString(targetPath)))
			{
				connection.Open();
				string sql = "DROP TABLE IF EXISTS answers";
				DBHelper.ExecuteQuery(sql, connection);

				sql = "CREATE TABLE IF NOT EXISTS `answers` (sample_id VARCHAR(255) ,sort_no INTEGER PRIMARY KEY AUTOINCREMENT";
				
				int index = 1;
				Excel.Range tmpqbEnd = qbEnd.Offset[-1, 0];//to avoid creating extra column
				Excel.Range range = qb.get_Range(qbStart, tmpqbEnd);
				Object[,] rangeAry = range.Value2;
				int max = rangeAry.GetLength(0);
				for (int i = 1; i <= max; i++)
				{
					string ansType = rangeAry[i, 1] == null ? "" : rangeAry[i, 1].ToString();
					string dataType = "TEXT";
					switch (ansType)
					{
						case Constants.AnswerType.SA:
							dataType = "TEXT";
							break;
						case Constants.AnswerType.D:
							dataType = "TEXT";
							break;
						case Constants.AnswerType.N:
							dataType = "TEXT";
							break;
					}
					sql += ",`q_" + index + "` " + dataType + " NULL ";
					index++;
				}
				sql += ")";
				
				DBHelper.ExecuteQuery(sql, connection);

				List<string> listSql = new List<string>();
				List<Excel.Worksheet> dataSheets = QCWorkbookHelper.GetDataSheets(sourceWorkbook);

				long colStart = Constants.DataSheet.COL_START;
				bool updateFlag = false;

				using (SQLiteTransaction tr = connection.BeginTransaction())
				{
					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.Transaction = tr;
						long lastColPage = 1;
						foreach (Excel.Worksheet sourceSheet in dataSheets)
						{
							sourceSheet.Unprotect(Constants.Password);
							Excel.Range start = sourceSheet.Cells[Constants.DataSheet.ROW_HEADER, 1];
							long lastRow = ExcelUtil.EndxlUp(start).Row;//start.EntireRow.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;
							Excel.Range dStart = sourceSheet.Cells[Constants.DataSheet.RowHeader, colStart];
							Excel.Range dEnd = dStart.End[Excel.XlDirection.xlToRight];
							long tempMax = lastColPage + dEnd.Column - 1;

							string insertSql = "";
							string insertValues = "";
							string updateSql = "";
							if (updateFlag)
							{
								updateSql = "UPDATE answers SET ";
								for (long v = lastColPage; v < tempMax; v++)
								{
									updateSql += "q_" +v +" = @q_"+ v+ " , ";
								}
								updateSql = updateSql.Substring(0, updateSql.Length - 2);
								updateSql += " WHERE sample_id = @sample_id"; ;
							}
							else
							{
								insertSql = "insert into answers ( sample_id";
								insertValues = "  values( @sample_id";
								for (long v = lastColPage; v < tempMax; v++)
								{
									insertSql += ",q_" + v;
									insertValues += ",@q_" + v;
								}
								insertSql += ")";
								insertValues += ")";
							}
							
							for (long k = 4; k < lastRow;)
							{
								long sRow = k;
								k += Constants.MAX_ROW_COUNT;
								if (k > lastRow)
								{
									k = lastRow;
								}
								//TODO HERE
								Excel.Range tmpS = sourceSheet.Cells[sRow, 1];
								Excel.Range tmpE = sourceSheet.Cells[k, dEnd.Column];
								Excel.Range totalRng = sourceSheet.get_Range(tmpS,tmpE);

								long maxCunt = totalRng.Columns.Count;
								

								Object[,] obj = totalRng.Value2;
								for (long j = sRow, ii = 1; j <= k; j++,ii++)
								{
									if (updateFlag)
									{
										command.CommandText = updateSql;
										command.Parameters.Add(new SQLiteParameter("@sample_id", obj[ii, 1].ToString()));
										for (long i = lastColPage, jj = 2; jj <= maxCunt; i++, jj++)
										{
											string str = "";
											if (null != obj[ii, jj])
											{
												str = obj[ii, jj].ToString();
											}
											command.Parameters.Add(new SQLiteParameter("@q_" + i, str));
										}
									}
									else
									{
										command.CommandText = insertSql + insertValues;
										command.Parameters.Add(new SQLiteParameter("@sample_id", obj[ii, 1].ToString()));
										for (long i = lastColPage, jj = 2; jj <= maxCunt; i++, jj++)
										{
											string str = "";
											if (null != obj[ii, jj])
											{
												str = obj[ii, jj].ToString();
											}
											command.Parameters.Add(new SQLiteParameter("@q_" + i, str));
										}
									}
									command.ExecuteNonQuery();
								}
							}
							
							if (updateFlag)
							{
								lastColPage--;
							}
							lastColPage += dEnd.Column - 1;
							updateFlag = true;
						}
						tr.Commit();
					}
				}
			}
		}

		internal void DropTable(String tableName, string target)
		{
			using (SQLiteConnection connection = DBHelper.GetConnection(DBHelper.GetConnectionString(target)))
			{
				connection.Open();
				DBHelper.ExecuteScalar("DROP TABLE " + tableName, connection);
			}
		}

		//internal void RenameTable(String tableName, string target) //added for phase3
		//{
		//    using (SQLiteConnection connection = DBHelper.GetConnection(DBHelper.GetConnectionString(target)))
		//    {
		//        try
		//        {
		//            connection.Open();
		//            DBHelper.ExecuteScalar("DROP TABLE " + tableName, connection);
		//            DBHelper.ExecuteScalar("ALTER TABLE answersTemp RENAME TO " + tableName, connection);

		//        }
		//        catch (Exception ex)
		//        {
		//            _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
		//        }
		//        finally
		//        {
		//            connection.Close();
		//        }
		//    }
		//}

		//internal void RenameWBTable(String tableName, string target)//added for phase3
		//{
		//    int indx = tableName.IndexOf("Temp");
		//    string originalWB = tableName.Remove(indx);
		//    using (SQLiteConnection connection = DBHelper.GetConnection(DBHelper.GetConnectionString(target)))
		//    {
		//        try
		//        {
		//            connection.Open();
		//            DBHelper.ExecuteScalar("DROP TABLE IF EXISTS " + originalWB, connection);
		//            DBHelper.ExecuteScalar("ALTER TABLE " + tableName + " RENAME TO " + originalWB, connection);
		//            connection.Close();
		//        }
		//        catch (Exception ex)
		//        {
		//            _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
		//        }
		//        finally
		//        {
		//            connection.Close();
		//        }
		//    }
		//}

		/// <summary>
		/// Method to update Weight Back Database table for Target QC4 file
		/// </summary>
		/// <param name="workBook">Target Workbook</param>
		/// <param name="dbPath">Target QC4 DB path</param>
		internal void UpdateWeightBack(Excel.Workbook workBook, string dbPath) 
		{
			string wText = "";

			Excel.Worksheet settingSheet = ExcelUtil.GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.Setting);
			Dictionary<string, string> weightBack = new Dictionary<string, string>();
			if (settingSheet == null)
			{
                return;
			}

			wText = settingSheet.Cells[2, 10].Text;
			if (wText == "")
			{
                return;
            }

			Excel.Range s = settingSheet.Cells[3, 9];
			Excel.Range e = ExcelUtil.EndxlUp(s);
			Excel.Range range = settingSheet.get_Range(s, e);
			foreach (Excel.Range r in range)
			{
				weightBack.Add(r.Text, r.Offset[0, 3].Text);
			}

			using (SQLiteConnection dbConn = DBHelper.GetConnection(DBHelper.GetConnectionString(dbPath)))
			{
				dbConn.Open();
				using (SQLiteTransaction tr = dbConn.BeginTransaction())
				{
					string sql = "select id from question where variable = '" + wText + "'";
					DataTable dt = DBHelper.GetDataTable(sql, dbConn);
					if (dt.Rows.Count == 0)
					{
						tr.Commit(); dbConn.Close();
						return;
                    }
					string index = dt.Rows[0][0].ToString();
					string variableName = "q_" + index;
					sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('answers') WHERE name='" + variableName + "'";
					int cnt = Convert.ToInt32(DBHelper.GetDataTable(sql, dbConn).Rows[0][0].ToString());
					if (cnt <= 0)
					{
						tr.Commit(); dbConn.Close();
						return;
					}


					wText = "weight_back_q_" + index;
					sql = "create table IF NOT EXISTS " + wText + " (sample_id  VARCHAR(255) ,value DECIMAL(19,9))";
					DBHelper.ExecuteQuery(sql, dbConn,tr); 

					sql = "DELETE FROM " + wText;
					DBHelper.ExecuteQuery(sql, dbConn,tr);

					sql = "CREATE temp table IF NOT EXISTS temp (id int not null,value long not null default 0)";
					DBHelper.ExecuteQuery(sql, dbConn, tr);

					sql = "DELETE FROM temp";
					DBHelper.ExecuteQuery(sql, dbConn, tr);

					using (SQLiteCommand tempInsert = dbConn.CreateCommand())
					{
						tempInsert.Transaction = tr;
						foreach (string key in weightBack.Keys)
						{
							tempInsert.CommandText = "insert into temp (id,value) VALUES(@id,@value)";
							tempInsert.Parameters.Add(new SQLiteParameter("@id", key));
							tempInsert.Parameters.Add(new SQLiteParameter("@value", weightBack[key].ToString()));
							tempInsert.ExecuteNonQuery();
						}
					}

					sql = "insert into "+ wText +" (sample_id,value) SELECT a.sample_id, t.value " +
						"FROM answers a INNER JOIN temp t ON a.q_"+ index +" = t.id; ";
					DBHelper.ExecuteQuery(sql, dbConn, tr);

                    tr.Commit();
				}
			}
		}
		/// <summary>
		/// Delete Data_After_Process and multivariate Table from database if exist
		/// </summary>
		/// <param name="targetPath">Database Path</param>
		internal void DeleteDataAfterProcessTableAndMultivariate(string targetPath, Excel.Workbook Workbook)
		{
			using (SQLiteConnection dbTarget = DBHelper.GetConnection(DBHelper.GetConnectionString(targetPath)))
			{
				try
				{
					dbTarget.Open();

					string sql = "DROP TABLE IF EXISTS data_after_process";
					DBHelper.ExecuteQuery(sql, dbTarget);

					sql = "DROP TABLE IF EXISTS multivariate";
					DBHelper.ExecuteQuery(sql, dbTarget);

					Excel.Worksheet ws = ExcelUtil.GetWorkSheetBySheetName(Workbook, "Multivariate");
					Workbook.Unprotect(Constants.Password);
					if (ws != null)
					{
						ws.Application.DisplayAlerts = false;
						ws.Delete();
						Workbook.Protect(Constants.Password, true);
					}
				}
				catch (Exception ex)
				{
					_log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
				}
				finally
				{
					dbTarget.Close();
				}
			}
		}

		internal List<Questions> GetOrgQuestions(string connectionString)
		{
			List<Questions> questions = new List<Questions>();
			string sql = "SELECT id, variable, answer_type, category_count FROM question WHERE question_flag = 'Org'";
			DataTable dt = DBHelper.GetDataTable(sql, connectionString);
			int max = dt.Rows.Count;
			for (int i = 0; i < max; i++)
			{
				questions.Add(new Questions(Convert.ToInt32(dt.Rows[i][0]), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), Convert.ToInt32(dt.Rows[i][3])));
			}
			return questions;
		}
	}
}
