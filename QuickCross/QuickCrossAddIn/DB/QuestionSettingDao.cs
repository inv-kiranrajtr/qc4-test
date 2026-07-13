using ExcelAddIn.Common;
using VB = Microsoft.VisualBasic;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelAddIn.DB
{
	class QuestionSettingDao
	{
		private SQLiteConnection connection;

		public QuestionSettingDao(string connectionString)
		{
			connection = new SQLiteConnection(connectionString);
		}

		public void InsertQuestionSetting(List<QuestionSettings> updateList, Excel.Worksheet worksheet)
		{
			if (updateList == null || updateList.Count <= 0)
			{
				return;
			}

			Definitions.isQsChanged = true;
			connection.Open();
			List<QC4Common.Common.GTAutoSetting.VariableDT> vars = new List<QC4Common.Common.GTAutoSetting.VariableDT>();
			using (SQLiteTransaction transaction = connection.BeginTransaction())
			{
				int maxId = 0;
				using (SQLiteCommand maxCmd = connection.CreateCommand())
				{
					DataTable dt = QC4Common.DB.DBHelper.GetDataTable("select max(id) from question", connection);
					if (dt.Rows.Count > 0)
					{
						maxId = Convert.ToInt32(dt.Rows[0][0]);
					}

				}

				using (SQLiteCommand command = connection.CreateCommand())
				{
					command.Transaction = transaction;
					foreach (QuestionSettings qs in updateList)
					{
						command.CommandText = "insert into question(id, variable, answer_type,category_count,question_flag) values(@id, @variable, @answer,@count, @flag)";
						command.Parameters.Add(new SQLiteParameter("@id", ++maxId));
						command.Parameters.Add(new SQLiteParameter("@variable", qs.Variable));
						command.Parameters.Add(new SQLiteParameter("@answer", qs.AnswerType));
						command.Parameters.Add(new SQLiteParameter("@count", qs.CategoryCount));
						command.Parameters.Add(new SQLiteParameter("@flag", "New"));
						command.ExecuteNonQuery();
						//UpdateRowName(worksheet, maxId, qs.RowNumber);
						QC4Common.Util.QSUtil.SetRowName(worksheet, worksheet.Rows[qs.RowNumber], maxId);
                        QC4Common.Common.GTAutoSetting.VariableDT qsInf = new QC4Common.Common.GTAutoSetting.VariableDT { Variable = qs.Variable, Type = qs.AnswerType };
                        vars.Add(qsInf);
					}
				}
				transaction.Commit();
			}

			QC4Common.Common.GTAutoSetting.LoadNewDataToGTHiddenSheet(worksheet.Application.ActiveWorkbook, vars);
			connection.Close();
		}

		public void UpdateQuestionSetting(List<QuestionSettings> variables, List<QuestionSettings> answers, List<QuestionSettings> counts)
		{
			if (variables != null && variables.Count > 0)
			{
				UpdateQuestionSetting("variable", variables);
				Definitions.isQsChanged = true;
			}

			if (answers != null && answers.Count > 0)
			{
				UpdateQuestionSetting("answer_type", answers);
				Definitions.isQsChanged = true;
			}

			if (counts != null && counts.Count > 0)
			{
				UpdateQuestionSetting("category_count", counts);
				Definitions.isQsChanged = true;
			}
		}

		public void UpdateQuestionSetting(string columnName, List<QuestionSettings> values)
		{
			connection.Open();
			string sql = "UPDATE question set " + columnName + " = @value where id = @id";
			using (SQLiteTransaction transaction = connection.BeginTransaction())
			{
				using (SQLiteCommand command = connection.CreateCommand())
				{
					command.Transaction = transaction;
					foreach (QuestionSettings val in values)
					{
						command.CommandText = sql;
						command.Parameters.Add(new SQLiteParameter("@id", val.Id));
						switch (columnName)
						{
							case "variable":
								command.Parameters.Add(new SQLiteParameter("@value", val.Variable));
								break;
							case "answer_type":
								command.Parameters.Add(new SQLiteParameter("@value", val.AnswerType));
								break;
							case "category_count":
								command.Parameters.Add(new SQLiteParameter("@value", val.CategoryCount));
								break;
						}
						command.ExecuteNonQuery();
					}
				}
				transaction.Commit();
			}
			connection.Close();
		}

		public void DeleteQuestions(List<QuestionSettings> deleteList)
		{
			if (deleteList.Count() < 1)
			{
				return;
			}
			string sql = "DELETE FROM question WHERE";
			string prefix = "";
			for (int i = 0; i < deleteList.Count(); i++)
			{
				sql += prefix + " id = " + deleteList[i].Id;
				prefix = " OR ";
			}

			connection.Open();
			using (SQLiteCommand command = connection.CreateCommand())
			{
				command.CommandText = sql;
				command.ExecuteNonQuery();
			}
			connection.Close();
		}

        public void DeleteFromMultiVariateTable(string variable)
        {
            int colcount = GetMultivariateColumnCount();
            int i = 0;
            string columnNames = string.Empty;
            string columnToDelete = "q_" + GetvariableId(variable);
            DataTable dt = GetmultivariatetableFieldList();
            bool firstColumn = true;
            connection.Open();
            using (SQLiteCommand mycommand = new SQLiteCommand(connection))
            {
                mycommand.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'multivariate'";
                SQLiteDataReader reader = mycommand.ExecuteReader();
                if (reader.Read())
                {
                    string daptable_query = reader.GetString(0);
                    daptable_query = daptable_query.Replace("multivariate", "multivariate_T");
                    string[] q = daptable_query.Split(',');

                    reader.Close();

                    while (i < colcount)
                    {
                        var tableColumnName = dt.Rows[i].ItemArray[1].ToString();
                        if (tableColumnName != columnToDelete)
                        {
                            if (firstColumn)
                            {
                                columnNames += tableColumnName;
                                firstColumn = false;
                            }
                            else
                            {
                                columnNames += "," + tableColumnName;
                            }
                        }
                        else
                        {
                            q[i] = (i == q.Length - 1) ? ")" : string.Empty;
                        }
                        i++;
                    }
                    string newq = string.Empty;
                    for (int k = 0; k < q.Length; k++)
                    {
                        newq += (string.IsNullOrEmpty(q[k]) || string.IsNullOrEmpty(newq) || string.Equals(q[k], ")")) ? q[k] : ("," + q[k]);
                    }
                    string org_query = newq.Replace("multivariate_T", "multivariate");
                    string sql = "BEGIN TRANSACTION;" +
                    newq + ";" +
                    "INSERT INTO multivariate_T SELECT " + columnNames + " FROM multivariate;" +
                    "DROP TABLE multivariate;" +
                    org_query + ";" +
                    "INSERT INTO multivariate SELECT " + columnNames + " FROM multivariate_T;" +
                    "DROP TABLE multivariate_T;" +
                    "COMMIT;";


                    // using (SQLiteCommand command = connection.CreateCommand())
                    {
                        mycommand.CommandText = sql;
                        mycommand.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
        }

        public void DeleteFromMultiVariateTable(List<QuestionSettings> deleteList)
        {
            int j = 0;
            List<string> multivariateTabledeleteList = new List<string>();
            while (j < deleteList.Count)
            {
                if (deleteList[j].QuestionFlag == "An")
                {
                    multivariateTabledeleteList.Add("q_" + GetvariableId(deleteList[j].VariableBefore));
                }
                j++;
            }

            if (multivariateTabledeleteList.Count > 1)
            {
                int colcount = GetMultivariateColumnCount();
                int i = 0;
                string columnNames = string.Empty;
                DataTable dt = GetmultivariatetableFieldList();
                bool firstColumn = true;

                while (i < colcount)
                {
                    var tableColumnName = dt.Rows[i].ItemArray[1].ToString();
                    if (!multivariateTabledeleteList.Contains(tableColumnName))
                    {
                        if (firstColumn)
                        {
                            columnNames += tableColumnName;
                            firstColumn = false;
                        }
                        else
                        {
                            columnNames += "," + tableColumnName;
                        }
                    }
                    i++;
                }

                string sql = "BEGIN TRANSACTION;" +
                "CREATE TEMPORARY TABLE multivariate_backup(" + columnNames + ");" +
                "INSERT INTO multivariate_backup SELECT " + columnNames + " FROM multivariate;" +
                "DROP TABLE multivariate;" +
                "CREATE TABLE multivariate(" + columnNames + ");" +
                "INSERT INTO multivariate SELECT " + columnNames + " FROM multivariate_backup;" +
                "DROP TABLE multivariate_backup;" +
                "COMMIT;";

                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private int GetMultivariateColumnCount()
        {
            int columncount = 0;
            connection.Open();
            using (SQLiteCommand cmd = connection.CreateCommand())
            {
                string sqlquery = "select * from multivariate";
                cmd.CommandText = sqlquery.ToString();
                cmd.CommandType = CommandType.Text;
                var dr = cmd.ExecuteReader();
                columncount = dr.FieldCount;
                dr.Close();
            }
            connection.Close();
            return columncount;
        }

        public int GetvariableId(string variable)
        {
            connection.Open();
            int rowcount = 0;
            string sql = "SELECT id from question WHERE variable =" + "'" + variable + "'";
            using (SQLiteCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql.ToString();
                cmd.CommandType = CommandType.Text;
                rowcount = Convert.ToInt32(cmd.ExecuteScalar());
            }
            connection.Close();
            return rowcount;
        }

        private DataTable GetmultivariatetableFieldList()
        {
            //using (var con = new SQLiteConnection(preparedConnectionString))
            {
                using (var cmd = new SQLiteCommand("PRAGMA table_info(multivariate);"))
                {
                    var table = new DataTable();

                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    SQLiteDataAdapter adp = null;
                    try
                    {
                        adp = new SQLiteDataAdapter(cmd);
                        adp.Fill(table);

                        connection.Close();
                        return table;
                    }
                    catch (Exception ex)
                    {
                        return null;

                    }
                }
            }
        }

        private DataTable getAnswertableFieldList(string tableName= "answers")
		{
			//using (var con = new SQLiteConnection(preparedConnectionString))
			{
				using (var cmd = new SQLiteCommand("PRAGMA table_info("+ tableName + ");"))
				{
					var table = new DataTable();

					cmd.Connection = connection;
					cmd.Connection.Open();

					SQLiteDataAdapter adp = null;
					try
					{
						adp = new SQLiteDataAdapter(cmd);
						adp.Fill(table);

						connection.Close();
						return table;
					}
					catch (Exception ex)
					{
						return null;

					}
				}
			}
		}
		private int GetRowCount()
		{
			int rowcount = 0;
			connection.Open();
			using (SQLiteCommand cmd = connection.CreateCommand())
			{
				string sqlquery = "select count(*) from question";
				cmd.CommandText = sqlquery.ToString();
				cmd.CommandType = CommandType.Text;
				rowcount = Convert.ToInt32(cmd.ExecuteScalar());


			}

			connection.Close();
			return rowcount;
		}
		public void DeleteAnswer(string variable)
        {
            StringBuilder columnname = new StringBuilder();
            int selectedItemIndex = GetIndex(variable);
            DataTable dt = getAnswertableFieldList();

            for(int r=0;r< dt.Rows.Count;r++)
            {
                if (dt.Rows[r].ItemArray[1].ToString() == "q_" + selectedItemIndex)
                    continue;
                columnname.Append(dt.Rows[r].ItemArray[1].ToString());
                columnname.Append(",");
            }
            columnname.Length--;
            string sql = "BEGIN TRANSACTION;" +
                "CREATE TEMPORARY TABLE answers_backup(" + columnname + ");" +
                "INSERT INTO answers_backup SELECT " + columnname + " FROM answers;" +
                "DROP TABLE answers;" +
                "CREATE TABLE answers(" + columnname + ");" +
                "INSERT INTO answers SELECT " + columnname + " FROM answers_backup;" +
                "DROP TABLE answers_backup;" +
                "COMMIT; ";
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private int GetIndex(string variable)
        {
            int index = 0;
            connection.Open();
            using (SQLiteCommand cmd = connection.CreateCommand())
            {
                string sqlquery = "select id from question where variable = '" + variable + "'";
                cmd.CommandText = sqlquery.ToString();
                cmd.CommandType = CommandType.Text;
                index = Convert.ToInt32(cmd.ExecuteScalar());
            }

            connection.Close();
            return index;
        }
        /// <summary>
        /// Delete columns from database "data_after_process" table
        /// </summary>
        /// <param name="selectedItemIndex">Column index to be delete</param>
        public void DeleteData_After(decimal selectedItemIndex)//bug fix as per the Redmine Id:237392
        {
            string columnname = string.Empty;
            DataTable dt = getAnswertableFieldList("data_after_process");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string variableId = dt.Rows[i].ItemArray[1].ToString();
                if (!variableId.Equals("q_" + selectedItemIndex))
                {
                    columnname += variableId + ",";
                }
            }
            columnname = columnname.Remove(columnname.Length - 1);

            string sql = "BEGIN TRANSACTION;" +
                "CREATE TEMPORARY TABLE data_after_process_backup(" + columnname + ");" +
                "INSERT INTO data_after_process_backup SELECT " + columnname + " FROM data_after_process;" +
                "DROP TABLE data_after_process;" +
                "CREATE TABLE data_after_process(" + columnname + ");" +
                "INSERT INTO data_after_process SELECT " + columnname + " FROM data_after_process_backup;" +
                "DROP TABLE data_after_process_backup;" +
                "COMMIT; ";

            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private int GetColumnCount(string tablename= "answers")
		{
			int columncount = 0;
			connection.Open();
			using (SQLiteCommand cmd = connection.CreateCommand())
			{
				string sqlquery = "select * from "+tablename;
				cmd.CommandText = sqlquery.ToString();
				cmd.CommandType = CommandType.Text;
				var dr = cmd.ExecuteReader();
				columncount = dr.FieldCount;
				dr.Close();
			}
			connection.Close();
			return columncount;
		}

		public void UpdateVariable(Dictionary<int, string> dict)
		{
			connection.Open();
			string sql = "UPDATE question set variable = @variable where id = @id";
			using (SQLiteTransaction transaction = connection.BeginTransaction())
			{
				using (SQLiteCommand command = connection.CreateCommand())
				{
					command.Transaction = transaction;
					foreach (int key in dict.Keys)
					{
						command.CommandText = sql;
						command.Parameters.Add(new SQLiteParameter("@id", key));
						command.Parameters.Add(new SQLiteParameter("@variable", dict[key]));
						command.ExecuteNonQuery();
					}
				}
				transaction.Commit();
			}
			connection.Close();
		}

		

	}
}


