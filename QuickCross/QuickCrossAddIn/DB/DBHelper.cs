using ExcelAddIn.Common;
using ExcelAddIn.Model;
using log4net;
using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
//using System.Data;
//using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static ExcelAddIn.Common.Constants;
using Constants = ExcelAddIn.Common.Constants;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelAddIn.DB
{

    public class DBHelper
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static string GetConnectionString(string filePath)
        {
            if (filePath.Contains(";"))
            {
                filePath = "\"" + filePath + "\"";
            }
            return @"Data Source='" + filePath + "'; Version=3;Password=" + Constants.Password + ";";
        }

        public static SQLiteConnection GetConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }

        public static string GetConnectionString(Excel.Workbook workbook)
        {
            Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting);
            if (sheet == null)
            {
                return null;
            }
            return sheet.Cells[23, 4].Text;
        }

        public static DataTable GetDataTable(string sql, SQLiteConnection cnn)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
                {
                    mycommand.CommandText = sql;
                    //SQLiteDataReader reader = mycommand.ExecuteReader();
                    //dt.Load(reader);
                    //reader.Close();
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(mycommand);
                    dataAdapter.Fill(dt);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }
        public static int ExecuteScalar(string sql, SQLiteConnection con)
        {
            using (SQLiteCommand command = con.CreateCommand())
            {
                command.CommandText = sql;
                int RowCount = 0;
                RowCount = Convert.ToInt32(command.ExecuteScalar());
                return RowCount;
            }
        }
        public static int ExecuteScalar(SQLiteConnection conn, string sql)
        {
            int value = 0;
            try
            {
                using (SQLiteCommand myCommand = conn.CreateCommand())
                {
                    conn.Open();
                    myCommand.CommandText = sql;
                    value = Convert.ToInt32(myCommand.ExecuteScalar());
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return value;

        }
        public static void DeleteDataAfterProcessTable(SQLiteConnection cnn)
        {
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
                {
                    string dp_sql = "DROP TABLE IF EXISTS data_after_process";
                    mycommand.CommandText = dp_sql.ToString();
                    mycommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void CreateDataprocessTable(SQLiteConnection cnn)
        {
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
                {
                    string dp_sql = "DROP TABLE IF EXISTS data_after_process";
                    mycommand.CommandText = dp_sql.ToString();
                    mycommand.ExecuteNonQuery();
                    /*SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'answers'*/
                    mycommand.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'answers'";
                    SQLiteDataReader reader = mycommand.ExecuteReader();
                    if (reader.Read())
                    {
                        string daptable_query = reader.GetString(0);
                        daptable_query = daptable_query.Replace("answers", "data_after_process");
                        DataTable iddt = GetDataTable("select id from question where LOWER(question_flag) <> 'org' AND LOWER(question_flag) <> 'imp' AND LOWER(question_flag) <> 'an' COLLATE NOCASE", cnn);// DataTable iddt = GetDataTable("select id from question where question_flag <> 'org' COLLATE NOCASE", cnn);//select id from question where question_flag <> 'orG' COLLATE NOCASE
                        if (iddt.Rows.Count > 0)
                        {
                            daptable_query = daptable_query.Remove(daptable_query.Length - 1, 1);
                            for (int i = 0; i < iddt.Rows.Count; i++)
                            {
                                daptable_query += ",q_" + iddt.Rows[i][0].ToString() + " TEXT ";
                            }
                            daptable_query = daptable_query + ")";
                        }
                        reader.Close();
                        mycommand.CommandText = daptable_query;
                        mycommand.ExecuteNonQuery();

                        mycommand.CommandText = "SELECT * FROM answers";
                        SQLiteDataReader rdr = mycommand.ExecuteReader();
                        string columns = string.Empty;
                        for (var i = 0; i < rdr.FieldCount; i++)
                        {
                            columns += (i > 0 ? "," : string.Empty) + rdr.GetName(i);
                        }
                        rdr.Close();
                        mycommand.CommandText = "INSERT INTO data_after_process(" + columns + ") SELECT " + columns + " FROM answers";
                        mycommand.ExecuteNonQuery();

                        ////need to alter data_after_process table ,if unanswered questions are there

                        //DataTable iddt = GetDataTable("select id from question where LOWER(question_flag) <> 'org' AND LOWER(question_flag) <> 'imp' COLLATE NOCASE", cnn);// DataTable iddt = GetDataTable("select id from question where question_flag <> 'org' COLLATE NOCASE", cnn);//select id from question where question_flag <> 'orG' COLLATE NOCASE
                        //if (iddt.Rows.Count > 0)
                        //{
                        //    string query = "ALTER TABLE data_after_process ADD q_" + iddt.Rows[0][0].ToString() + " VARCHAR ";  /*ALTER TABLE data_after_process ADD newfield VARCHAR;*/
                        //    for (int i = 0; i < iddt.Rows.Count; i++)
                        //    {
                        //        query = "ALTER TABLE data_after_process ADD q_" + iddt.Rows[i][0].ToString() + " VARCHAR ";
                        //        AlterDataAfterProcess(cnn, query);
                        //    }
                        //    // int result = AlterDataAfterProcess(cnn, query);
                        //}
                    }
                    if (Convert.ToInt16(GetDataTable("select Count(question_flag) from question where question_flag = 'An'", cnn).Rows[0][0].ToString()) > 0)
                    {
                        dp_sql = "DROP TABLE IF EXISTS multivariate_temp";
                        mycommand.CommandText = dp_sql.ToString();
                        mycommand.ExecuteNonQuery();
                        mycommand.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'multivariate'";
                        reader = mycommand.ExecuteReader();
                        if (reader.Read())
                        {
                            string daptable_query = reader.GetString(0);
                            daptable_query = daptable_query.Replace("multivariate", "multivariate_temp");
                            reader.Close();
                            mycommand.CommandText = daptable_query;
                            mycommand.ExecuteNonQuery();

                            mycommand.CommandText = "SELECT * FROM multivariate";
                            SQLiteDataReader rdr = mycommand.ExecuteReader();
                            string columns = string.Empty;
                            for (var i = 0; i < rdr.FieldCount; i++)
                            {
                                columns += (i > 0 ? "," : string.Empty) + rdr.GetName(i);
                            }
                            rdr.Close();
                            mycommand.CommandText = "INSERT INTO multivariate_temp(" + columns + ") SELECT " + columns + " FROM multivariate";
                            mycommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static int GetRowCount(SQLiteConnection cnn)//by 191  for question table question numbers
        {
            int rowcount = 0;
            using (SQLiteCommand cmd = new SQLiteCommand(cnn))
            {
                string sqlquery = "select count(*) from question";
                cmd.CommandText = sqlquery.ToString();
                cmd.CommandType = CommandType.Text;
                rowcount = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return rowcount;
        }
        private static int GetColumnCount(SQLiteConnection cnn)//by 191  for question table question numbers
        {
            int columncount = 0;
            using (SQLiteCommand cmd = new SQLiteCommand(cnn))
            {
                string sqlquery = "select * from answers";
                cmd.CommandText = sqlquery.ToString();
                cmd.CommandType = CommandType.Text;
                var dr = cmd.ExecuteReader();
                columncount = dr.FieldCount;
            }
            return columncount;
        }
        private static int AlterDataAfterProcess(SQLiteConnection cnn, string query)//by 191  for altering dataafterprocess
        {
            int success = 0;
            using (SQLiteCommand cmd = new SQLiteCommand(cnn))
            {
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;
                success = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return success;
        }
        //private bool DropTempTable(SQLiteCommand sQLiteCommand)
        //{
        //    string sSql = "DROP TABLE IF EXISTS " + Constants.DP.TEMP_DATA_AFTER_PROC;
        //    sQLiteCommand.CommandText = sSql;
        //    sQLiteCommand.ExecuteNonQuery();
        //    return true;
        //}

        //private bool CreateTempDataProcessTable(SQLiteCommand sQLiteCommand, List<string> columnNames)
        //{
        //    try
        //    {
        //        string sSql = "CREATE TABLE IF NOT EXISTS `" + Constants.DP.TEMP_DATA_AFTER_PROC + "`";
        //        sSql += "(";

        //        int ColumnLimit = columnNames.Count;
        //     //   if (ColumnLimit > DBSettings.MaxNoOfColumnInsertInBulk) ColumnLimit = DBSettings.MaxNoOfColumnInsertInBulk;

        //        for (int i = 0; i <= ColumnLimit - 1; i++)
        //        {
        //            sSql += "`" + columnNames[i] + "` " + Constants.DP.TempTableVarcharDataType + " ";
        //            if (ColumnLimit > (i + 1))
        //            {
        //                sSql += ",";
        //            }
        //        }
        //        sSql += ")";

        //        sQLiteCommand.CommandText = sSql;
        //        sQLiteCommand.ExecuteNonQuery();

        //        //for (int i = ColumnLimit; i <= columnNames.Count - 1; i++)
        //        //{
        //        //    TempTableAddNewColumn(sQLiteCommand, columnNames[i]); // adding new column if not exist;
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        //_log.Error(ex.StackTrace);
        //    }
        //    return true;
        //}
        /// <summary>
        /// Method to save data to database after data process
        /// </summary>
        /// <param name="con">SQL connection string</param>
        /// <param name="DT"></param>
        /// <param name="valueArray">Processed data</param>
        /// <param name="columnList">Column list</param>
        /// <param name="faDict">FA Question details</param>
        public static void SaveDataTable(SQLiteConnection con, DataTable DT, object[,] valueArray, List<string> columnList, Dictionary<string, List<string>> faDict = null)
        {
            string query = string.Empty;
            string query2 = string.Empty;
            bool isAn = false;
            bool isOther = false;
            try
            {
                //  _log.Info("------------STARTING DB UPDATE--------");
                columnList.Count.ToString();
                List<string> faColumnList = new List<string>();
                if (faDict != null)
                {
                    faColumnList = faDict.Keys.ToList<string>();
                }
                using (SQLiteTransaction tr = con.BeginTransaction())
                {
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(con))
                        {
                            List<bool> isAnCols = new List<bool>();
                            for (int c=0;c< columnList.Count; c++)
                            {
                                if (Convert.ToInt16(GetDataTable("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('data_after_process') WHERE name='" + columnList[c] + "'", con).Rows[0][0].ToString()) == 0)
                                {
                                    isAnCols.Add(true);
                                    isAn = true;
                                }
                                else
                                {
                                    isAnCols.Add(false);
                                    isOther = true;
                                }
                            }
                            int rowcount = valueArray.GetLength(0);

                            for (int i = 0; i < rowcount; i++)
                            {
                                // _log.Info("-------PREPARING PARAMS-------");
                                query = "Update data_after_process set ";
                                query2 = "Update multivariate_temp set ";
                                for (int j = 0,pref = 0, pref2 = 0; j < columnList.Count; j++)
                                {
                                    if (isAnCols[j])
                                    {
                                        if (pref2 > 0)
                                        {
                                            query2 += ",";
                                        }
                                        pref2++;
                                        query2 += columnList[j];
                                        query2 += "='" + valueArray[i, j + 1] + "'";
                                        continue;
                                    }
                                    if (pref > 0)
                                    {
                                        query += ",";
                                    }
                                    pref++;
                                    query += columnList[j];
                                    //query += "=@param"+(j+1).ToString();
                                    //sql ' exception fix  RedmineId:232757 ... fix is done by replacing single quotes with double single quotes in value
                                    string value = valueArray[i, j + 1].ToString();
                                    if (value.Contains("'"))
                                    {
                                        value=value.Replace(@"'",@"''");
                                        query += "='" + value + "'";
                                    }
                                    else
                                    query += "='" + valueArray[i, j + 1] + "'";

                                }
                                for (int k = 0; k < faColumnList.Count; k++)
                                {
                                    isOther = true;
                                    if (columnList.Count > 0 || k > 0)
                                    {
                                        query += ",";
                                    }
                                    query += faColumnList[k];
                                    query += "=@param" + k.ToString();

                                }

                                //valueArray[i, j+1]
                                query += " where sort_no = " + valueArray[i, 0];
                                query2 += " where sort_no = " + valueArray[i, 0];
                                if (isOther)
                                {
                                    cmd.CommandText = query;
                                    // _log.Info("-------REPLACING PARAMS-------");
                                    for (int k = 0, x = columnList.Count; k < faColumnList.Count; k++, x++)
                                    {
                                        //   cmd.Parameters.AddWithValue("@param" + k.ToString(), faDict[faColumnList[k]][i]);
                                        cmd.Parameters.AddWithValue("@param" + k.ToString(), valueArray[i, x + 1]/*faDict[faColumnList[k]][i]*/);
                                    }
                                    cmd.ExecuteNonQuery();
                                }
                                if(isAn)
                                {
                                    cmd.CommandText = query2;
                                    cmd.ExecuteNonQuery();
                                }

                            }


                        }
                        tr.Commit();
                    }catch(Exception ex)
                    {
                        _log.LogError(ex.Message);
                        try
                        {
                            tr.Rollback();
                        }
                        catch (Exception ex2)
                        { _log.LogError(ex2.Message); }
                    }
                }

            }
            catch (Exception Ex)
            {
                System.Windows.MessageBox.Show(Ex.Message, "QuickCross");
                _log.LogError(Ex.Message);
            }
            _log.Info("------------ENGING DB UPDATE--------");
        }
        public static void SaveDataTable(SQLiteConnection con, DataTable DT, object[,] valueArray, string columnName, string sql = "")
        {
            try
            {
                string query = "Update data_after_process set " + columnName + "='{0}' where sort_no = {1}";

                using (SQLiteTransaction tr = con.BeginTransaction())
                {

                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        //from jijesh

                        ////////cmd.CommandText = string.IsNullOrEmpty(sql) == true ? string.Format("SELECT * FROM {0}", DT.TableName) : sql;
                        ////////SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        ////////SQLiteCommandBuilder builder = new SQLiteCommandBuilder(adapter);
                        ////////adapter.Update(DT);
                        ///
                        int rowcount = valueArray.GetLength(0);
                        for (int i = 0; i < rowcount; i++)

                        {
                            cmd.CommandText = string.Format(query, valueArray[i, 1], valueArray[i, 0]);
                            cmd.ExecuteNonQuery();

                        }

                        // ------******** important ***** Update adapter is not working----** so after delete datatbase is not upated,
                        //--------******* hence wrong data after first instruction

                        //modified by 191 
                        // con.Open();
                        // cmd = con.CreateCommand();
                        //cmd.CommandText = string.Format("DELETE FROM {0}", DT.TableName);
                        //cmd.ExecuteNonQuery();
                        //DT.AcceptChanges();
                        //cmd.CommandText = string.Format("SELECT * FROM {0}", DT.TableName);
                        //SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        //adapter.SelectCommand = cmd;
                        //var ds = new DataSet();
                        //adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        //adapter.AcceptChangesDuringFill = false;
                        //adapter.Fill(ds, DT.TableName);
                        //SQLiteCommandBuilder builder = new SQLiteCommandBuilder(adapter);
                        //adapter.DeleteCommand = builder.GetDeleteCommand(true);                       
                        //adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        //adapter.InsertCommand = builder.GetInsertCommand(true);

                        // ------******** important ***** Update adapter is not working----** so after delete datatbase is not upated,
                        //--------******* hence wrong data after first instruction
                        //adapter.Update(ds, DT.TableName);
                        //adapter.FillSchema(dtnew, SchemaType.Mapped);
                        //adapter.Update(dtnew);
                        // DataSet ds = new DataSet();                                            
                        // adapter.Fill(ds);                       
                        //// builder.GetUpdateCommand();
                        // adapter.Update(ds.Tables[0]);

                        // // con.Close();
                    }
                    tr.Commit();
                }
            }
            catch (Exception Ex)
            {
                System.Windows.MessageBox.Show(Ex.Message, "QuickCross");
                _log.LogError(Ex.Message);
            }
        }

        //public static SQLiteConnection GetConnection()
        //{
        //	return new SQLiteConnection(@"Data Source=D:\Projects\Quick cross\db\qc5.db; Version=3;");
        //}

        //public static void CreateQuestionTable()
        //{
        //	using (dbConnection = GetConnection())
        //	{
        //		dbConnection.Open();
        //		using (SQLiteCommand command = dbConnection.CreateCommand())
        //		{
        //			StringBuilder sql = new StringBuilder();
        //			sql.Append("CREATE TABLE IF NOT EXISTS `question` (");
        //			sql.Append("`id` INT AUTO INCREMENT NOT NULL , ");
        //			sql.Append("`variable_name` VARCHAR(255) UNIQUE NOT NULL , ");
        //			sql.Append("`new` TINYINT NULL , ");
        //			sql.Append("`question_number` VARCHAR(255) NULL , ");
        //			sql.Append("`question_type` INT NULL , ");
        //			sql.Append("`number_of_question` INT NULL , ");
        //			sql.Append("`answer_type` INT NULL , ");
        //			sql.Append("`no_of_categories` INT NULL , ");
        //			sql.Append("`score` VARCHAR(45) NULL , ");
        //			sql.Append("`sort` INT NULL , ");
        //			sql.Append("`column` VARCHAR(255) NULL , ");
        //			sql.Append("`table_heading` VARCHAR(2000) NULL , ");
        //			sql.Append("`question_sentance` VARCHAR(2000) NULL , ");
        //			sql.Append("`add_sub_totals` INT NULL , ");
        //			sql.Append("`count` VARCHAR(255) NULL , ");
        //			sql.Append("`count_base` INT NULL , ");
        //			sql.Append("PRIMARY KEY (`id`))");
        //			command.CommandText = sql.ToString();
        //			command.ExecuteNonQuery();
        //		}
        //	}
        //}

        //public static void PopulateInitialDB()
        //{
        //	Worksheet QuestionSettingSheet = ExcelUtil.GetWorksheetByIndex(1);
        //	Range range = ExcelUtil.GetNamedRange("List", "List_Item_ALLD");
        //	List<Question> list = new List<Question>();
        //	int i = 4;

        //	try
        //	{
        //		int ansCol = 1;
        //		foreach (Range cell in range)
        //		{
        //			//int enumValue = 0;
        //			//Console.WriteLine("Name: " + Enum.GetName(typeof(QuestionTypeEnum), enumValue) + " , Value: " + enumValue);
        //			Question qs = new Question();
        //			qs.VariableName = QuestionSettingSheet.Cells[i, 6].Text;
        //			qs.IsNew = QuestionSettingSheet.Cells[i, 2].Text == "" ? 0 : 1;
        //			qs.QuestionNumber = QuestionSettingSheet.Cells[i, 3].Text;
        //			qs.QuestionType = Util.GetQuestionTypeValue(QuestionSettingSheet.Cells[i, 4].Text); //(int)QuestionTypeEnum.FAL; //;
        //			qs.QuestionCount = QuestionSettingSheet.Cells[i, 5].Text == "" ? 0 : Convert.ToInt32(QuestionSettingSheet.Cells[i, 5].Text);
        //			qs.AnswerType = Util.GetAnswerTypeValue(QuestionSettingSheet.Cells[i, 7].Text);
        //			qs.CategoriesCount = QuestionSettingSheet.Cells[i, 8].Text == "" ? 0 : Convert.ToInt32(QuestionSettingSheet.Cells[i, 8].Text);
        //			qs.Score = QuestionSettingSheet.Cells[i, 9].Text;
        //			qs.Sort = QuestionSettingSheet.Cells[i, 10].Text == "" ? 0 : Convert.ToInt32(QuestionSettingSheet.Cells[i, 8].Text);
        //			qs.Column = "q_" + ansCol++;
        //			qs.TableHeading = QuestionSettingSheet.Cells[i, 12].Text;
        //			qs.QuestionSentence = QuestionSettingSheet.Cells[i, 13].Text;
        //			for (int j = 0; j < qs.CategoriesCount; j++)
        //			{
        //				//string choice = QuestionSettingSheet.Cells[i, 14 + j].Text;
        //				//qs.Choices.Add(choice);
        //			}
        //			list.Add(qs);
        //			i++;
        //		}
        //		InsertQuestions(null, list);
        //	}
        //	catch (Exception ex)
        //	{
        //	}
        //}

        //public static void InsertQuestions(Question question = null, List<Question> questions = null)
        //{
        //	if (questions == null && question == null)
        //	{
        //		return;
        //	}
        //	if (questions == null)
        //	{
        //		questions = new List<Question>();
        //		questions.Add(question);
        //	}
        //	try
        //	{
        //		using (dbConnection = GetConnection())
        //		{
        //			dbConnection.Open();

        //			using (SQLiteTransaction tr = dbConnection.BeginTransaction())
        //			{

        //				using (SQLiteCommand insertSQL = dbConnection.CreateCommand())
        //				{
        //					insertSQL.Transaction = tr;
        //					foreach (Question qs in questions)
        //					{
        //						string sql = "INSERT INTO question (variable_name, new, question_number,question_type," +
        //							"number_of_question,answer_type,no_of_categories,score,sort,column,table_heading,question_sentance)" +
        //							"VALUES (@variable,@new,@questionNumber,@questionType,@questionCount,@anserType," +
        //							"@categoryCount,@score,@sort,@column,@heading,@sentence)";
        //						insertSQL.CommandText = sql;
        //						insertSQL.Parameters.Add(new SQLiteParameter("@variable", qs.VariableName));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@new", qs.IsNew));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@questionNumber", qs.QuestionNumber));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@questionType", qs.QuestionType));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@questionCount", qs.QuestionCount));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@anserType", qs.AnswerType));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@categoryCount", qs.CategoriesCount));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@score", qs.Score));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@sort", qs.Sort));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@column", qs.Column));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@heading", qs.TableHeading));
        //						insertSQL.Parameters.Add(new SQLiteParameter("@sentence", qs.QuestionSentence));
        //						insertSQL.ExecuteNonQuery();
        //					}
        //					tr.Commit();
        //				}
        //			}
        //		}
        //	}
        //	catch (Exception ex)
        //	{
        //		Console.WriteLine(ex.Message);
        //		dbConnection.Close();
        //	}
        //}


        //public static void UpdateVariable(Dictionary<int, string> updateList)
        //{
        //    try
        //    {
        //        //using (dbConnection = GetConnection())
        //        //{
        //        //	dbConnection.Open();

        //        //	using (SQLiteTransaction tr = dbConnection.BeginTransaction())
        //        //	{
        //        //		using (SQLiteCommand cmd = dbConnection.CreateCommand())
        //        //		{
        //        //			cmd.Transaction = tr;
        //        foreach (int index in updateList.Keys)
        //        {
        //            string var = Definitions.RowVariableList[index];
        //            string key = Definitions.VariableDictionary[var].VariableBefore;
        //            Definitions.VariableDictionary[var].VariableBefore = var;
        //            Definitions.isQsChanged = true;
        //            //cmd.CommandText = "UPDATE question SET variable_name = '" + var + "'  WHERE variable_name = '" + key + "' ;";
        //            //cmd.ExecuteNonQuery();
        //        }
        //        //		}
        //        //		tr.Commit();
        //        //	}
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        //dbConnection.Close();
        //    }
        //}

        //public static void UpdateCategories(List<QuestionSettings> updateList)
        //{
        //    try
        //    {
        //        //using (dbConnection = GetConnection())
        //        //{
        //        //	dbConnection.Open();

        //        //	using (SQLiteTransaction tr = dbConnection.BeginTransaction())
        //        //	{
        //        //		using (SQLiteCommand cmd = dbConnection.CreateCommand())
        //        //		{
        //        //			cmd.Transaction = tr;
        //        foreach (QuestionSettings qs in updateList)
        //        {
        //            Definitions.VariableDictionary[qs.Variable].CategoryCountBefore = qs.CategoryCount;
        //            Definitions.isQsChanged = true;
        //            //			cmd.CommandText = "UPDATE question SET no_of_categories = " + qs.CategoryCount + "  WHERE variable_name = '" + qs.Variable + "' ;";
        //            //			cmd.ExecuteNonQuery();
        //            //		}
        //            //	}
        //            //	tr.Commit();
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        //dbConnection.Close();
        //    }
        //}

        //public static void UpdateAnswerTypes(List<QuestionSettings> updateList)
        //{
        //    try
        //    {
        //        //using (dbConnection = GetConnection())
        //        //{
        //        //	dbConnection.Open();

        //        //	using (SQLiteTransaction tr = dbConnection.BeginTransaction())
        //        //	{
        //        //		using (SQLiteCommand cmd = dbConnection.CreateCommand())
        //        //		{
        //        //			cmd.Transaction = tr;
        //        foreach (QuestionSettings qs in updateList)
        //        {
        //            Definitions.VariableDictionary[qs.Variable].AnswerTypeBefore = qs.AnswerType;
        //            Definitions.isQsChanged = true;
        //            //cmd.CommandText = "UPDATE question SET answer_type = " + qs.AnswerType + "  WHERE variable_name = '" + qs.Variable + "' ;";
        //            //cmd.ExecuteNonQuery();
        //        }
        //        //		}
        //        //		tr.Commit();
        //        //	}
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        //	dbConnection.Close();
        //    }
        //}

        //public static void CreateAnswerTable()
        //{
        //	try
        //	{
        //		StringBuilder answer = new StringBuilder();
        //		StringBuilder faAnswer = new StringBuilder();
        //		int count = ExcelUtil.GetNamedRange("List", "List_Item_ALLD").Count;
        //		Worksheet QuestionSettingSheet = ExcelUtil.GetWorksheetByIndex(1);
        //		count += 4;
        //		answer.Append("CREATE TABLE IF NOT EXISTS `answer`(");
        //		answer.Append("`sample_id` INT NOT NULL PRIMARY KEY,");
        //		answer.Append(" `sort_no` INT NULL,");

        //		faAnswer.Append("CREATE TABLE IF NOT EXISTS `answer_fa`(");
        //		faAnswer.Append("`sample_id` INT NOT NULL PRIMARY KEY,");
        //		faAnswer.Append(" `sort_no` INT NULL,");

        //		for (int i = 4; i < count; i++)
        //		{
        //			string ansType = QuestionSettingSheet.Cells[i, 7].Text;
        //			string variable = QuestionSettingSheet.Cells[i, 6].Text;
        //			if (AnswerType.FA.Equals(ansType))
        //			{
        //				faAnswer.Append(" `"+ variable + "` TEXT NULL,");
        //			}
        //			//else if ()
        //			//{
        //			//}
        //		}

        //		using (dbConnection = GetConnection())
        //		{
        //			using (SQLiteCommand command = dbConnection.CreateCommand())
        //			{


        //				//sql.Append("CREATE TABLE IF NOT EXISTS `answer`(");
        //				//sql.Append("`sample_id` INT NOT NULL PRIMARY KEY,");
        //				//sql.Append(" `sort_no` INT NULL,");
        //				//sql.Append("`answer_date` TIMESTAMP NULL,");

        //				//command.CommandText = sql.ToString();
        //				//command.ExecuteNonQuery();
        //			}
        //		}
        //	}
        //	catch { }
        //}


        //public static void InsertSettings()
        //{
        //	try
        //	{
        //		dbConnection.Open();
        //		string sql = "INSERT INTO settings (key, value, created_at, updated_at) VALUES (@key,@val,@create,@update)";
        //		SQLiteCommand insertSQL = new SQLiteCommand(sql, dbConnection);
        //		insertSQL.Parameters.Add(new SQLiteParameter("@key", 15));
        //		insertSQL.Parameters.Add(new SQLiteParameter("@val", 1111));
        //		insertSQL.Parameters.Add(new SQLiteParameter("@create", DateTime.Now));
        //		insertSQL.Parameters.Add(new SQLiteParameter("@update", DateTime.Now));
        //		insertSQL.ExecuteNonQuery();
        //	}
        //	catch (Exception ex)
        //	{
        //		Console.WriteLine(ex.Message);
        //	}
        //	finally
        //	{
        //		dbConnection.Close();
        //	}
        //}

        public static void ExecuteQuery(string sql, SQLiteConnection con)
        {
            using (SQLiteCommand command = con.CreateCommand())
            {

                using (SQLiteTransaction tr = con.BeginTransaction())
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    tr.Commit();
                }


            }
        }

        internal static string GetConnectionString()
        {
            Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(Globals.ThisAddIn.Application.ActiveWorkbook, Common.Constants.SheetCodeName.Setting);
            if (sheet == null)
            {
                return null;
            }
            string str = sheet.Cells[23, 4].Text;
            return str;
        }

        public static void DeleteMultivariateTable(SQLiteConnection cnn)
        {
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
                {
                    string dp_sql = "DROP TABLE IF EXISTS data_after_process";
                    mycommand.CommandText = dp_sql.ToString();
                    mycommand.ExecuteNonQuery();
                }
            }
            catch { }
        }
        //public static DataTable GetDataTable(string sql, SQLiteConnection cnn)
        //{
        //	DataTable dt = new DataTable();
        //	try
        //	{
        //		using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
        //		{
        //			mycommand.CommandText = sql;
        //			SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(mycommand);
        //			dataAdapter.Fill(dt);
        //			//SQLiteDataReader reader = mycommand.ExecuteReader();
        //			//dt.Load(reader);
        //			//reader.Close();
        //		}
        //	}
        //	catch (Exception e)
        //	{
        //		throw new Exception(e.Message);
        //	}
        //	return dt;
        //}
    }
}