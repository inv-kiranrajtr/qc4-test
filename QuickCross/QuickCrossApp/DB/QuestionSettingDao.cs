using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.DB
{
    internal class QuestionSettingDao
    {
        private SQLiteConnection connection;

        public QuestionSettingDao()
        {
        }

        public QuestionSettingDao(string connectionString)
        {
            connection = new SQLiteConnection(connectionString);
        }

        internal void CreateQuestion()
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS `question` (id INTEGER, variable TEXT ,answer_type TEXT,category_count int,question_flag VARCHAR(5))";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        internal void InsertQuestions(List<Util.Qc3Parse.QDataDetail> qDataDetails)
        {
            connection.Open();
            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    int i = 0;
                    foreach (var qd in qDataDetails)
                    {
                        //if (!qd.isFound)
                        //{
                        //	continue;
                        //}

                        command.CommandText = "insert into question(id, variable, answer_type,category_count, question_flag) values(@id, @variable, @answer, @count, @flag)";
                        command.Parameters.Add(new SQLiteParameter("@id", qd.questionOrder));
                        command.Parameters.Add(new SQLiteParameter("@variable", qd.variableName));
                        command.Parameters.Add(new SQLiteParameter("@answer", qd.answerType));
                        command.Parameters.Add(new SQLiteParameter("@count", qd.categoryCount));
                        command.Parameters.Add(new SQLiteParameter("@flag", "Org"));
                        command.ExecuteNonQuery();
                        i++;
                    }

                }
                transaction.Commit();
            }
            connection.Close();
        }
        public void insertQuestioninForm(string variable, string answer_type, string question_flag, int category_count = 0, Microsoft.Office.Interop.Excel.Worksheet sheet = null, int Rownumber = 0)
        {
            connection.Open();

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

                    command.CommandText = "insert into question(id, variable, answer_type,category_count, question_flag) values(@id, @variable, @answer, @count, @flag)";
                    command.Parameters.Add(new SQLiteParameter("@id", ++maxId));
                    command.Parameters.Add(new SQLiteParameter("@variable", variable));
                    command.Parameters.Add(new SQLiteParameter("@answer", answer_type));
                    command.Parameters.Add(new SQLiteParameter("@count", category_count));
                    command.Parameters.Add(new SQLiteParameter("@flag", question_flag));
                    command.ExecuteNonQuery();
                    QC4Common.Util.QSUtil.SetRowName(sheet, sheet.Rows[Rownumber], maxId);
                }
                transaction.Commit();

            }
            connection.Close();

        }
        public void DeleteDataAfterProcess()
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS data_after_process";
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
        public void DeleteQuestions(string variable)
        {

            string sql = "DELETE FROM question WHERE variable =" + "'" + variable + "'";
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }

            connection.Close();
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
                            q[i] = (i == q.Length-1)? ")": string.Empty;
                        }
                        i++;
                    }
                    string newq = string.Empty;
                    for(int k=0;k<q.Length;k++)
                    {
                        newq += (string.IsNullOrEmpty(q[k]) || string.IsNullOrEmpty( newq)|| string.Equals(q[k], ")")) ? q[k]:(","+ q[k]);
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
        public void UpdateQuestioninForm(string variable, int category_count = 0)
        {
            connection.Open();
            string sql = "UPDATE question set category_count =" + "'" + category_count.ToString() + "'" + " Where variable =" + "'" + variable + "'";
            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            connection.Close();
        }



        private int GetColumnCount()//by 191  for question table question numbers
        {
            int columncount = 0;
            connection.Open();
            using (SQLiteCommand cmd = connection.CreateCommand())
            {
                string sqlquery = "select * from answers";
                cmd.CommandText = sqlquery.ToString();
                cmd.CommandType = CommandType.Text;
                var dr = cmd.ExecuteReader();
                columncount = dr.FieldCount;
                dr.Close();
            }
            connection.Close();
            return columncount;
        }

        private int GetMultivariateColumnCount()//by 191  for question table question numbers
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

        private DataTable getAnswertableFieldList()
        {
            //using (var con = new SQLiteConnection(preparedConnectionString))
            {
                using (var cmd = new SQLiteCommand("PRAGMA table_info(answers);"))
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
    }
}
