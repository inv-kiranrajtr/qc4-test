using log4net;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.DB
{
    internal class DBHelper
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static string ConnectionString { get; set; }

        public static void CreateDatabase(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                SQLiteConnection.CreateFile(filePath);
                try
                {
                    if (filePath.Contains(";"))
                    {
                        filePath = "\"" + filePath + "\"";
                    }
                    using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + filePath + ";Version=3;"))
                    {
                        conn.SetPassword(Constants.Password);
                        conn.Open();
                    }
                }
                catch { }
            }
        }

        public static string GetConnectionString(string filePath)
        {
            if (filePath.Contains(";"))
            {
                filePath = "\"" + filePath + "\"";
            }
            return @"Data Source=" + filePath + "; Version=3;Password=" + Constants.Password + ";";
        }

        public static SQLiteConnection GetConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
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

        //pass opened connection
        public static DataTable GetDataTable(string sql, SQLiteConnection cnn)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
                {
                    mycommand.CommandText = sql;
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

        public static DataTable GetDataTable(string sql, string connectionString)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                return GetDataTable(sql, con);
            }
        }

        public static void ExecuteQuery(string sql, SQLiteConnection con, SQLiteTransaction tr = null)
        {
            using (SQLiteCommand command = con.CreateCommand())
            {
                if (tr != null)
                {
                    command.Transaction = tr;
                }
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public static void ExecuteQuery(string sql, string connectionString)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                using (SQLiteCommand command = con.CreateCommand())
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
        }
        internal static bool checkMultiTableExists(Excel.Workbook workbook)
        {
            int cnt = 0;
            using (SQLiteConnection con = GetConnection(GetConnectionString(workbook)))
            {
                con.Open();
                string sql = "SELECT count(name) FROM sqlite_master WHERE type='table' AND name='multivariate'";
                cnt = ExecuteScalar(sql, con);
            }
            return cnt > 0;
        }
        internal static bool checkAfterProcess(Excel.Workbook workbook)
        {
            int cnt = 0;
            using (SQLiteConnection con = GetConnection(GetConnectionString(workbook)))
            {
                con.Open();
                string sql = "SELECT count(name) FROM sqlite_master WHERE type='table' AND name='data_after_process'";
                cnt = ExecuteScalar(sql, con);
            }
            return cnt > 0;
        }

        public static void CreateMultivariateTempTable(Excel.Workbook workbook)
        {
            using (SQLiteConnection cnn = GetConnection(GetConnectionString(workbook)))
            {
                cnn.Open();
                using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
                {
                    if (Convert.ToInt16(GetDataTable("select Count(question_flag) from question where question_flag = 'An'", cnn).Rows[0][0].ToString()) > 0)
                    {
                        string dp_sql = "DROP TABLE IF EXISTS multivariate_temp";
                        mycommand.CommandText = dp_sql.ToString();
                        mycommand.ExecuteNonQuery();
                        mycommand.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'multivariate'";
                        SQLiteDataReader reader = mycommand.ExecuteReader();
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

       

        internal static string GetConnectionString(Excel.Workbook workbook)
        {
            Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting);
            if (sheet == null)
            {
                return null;
            }
            return sheet.Cells[23, 4].Text;
        }

        internal static string GetDataType(string answerType)
        {
            string dataType = "TEXT";
            switch (answerType)
            {
                case Constants.AnswerType.SA:
                    dataType = "TEXT";
                    break;
                case Constants.AnswerType.N:
                    dataType = "TEXT";
                    break;
                case Constants.AnswerType.MA:
                    dataType = "TEXT";
                    break;
            }
            return dataType;
        }

        internal static int TableExist(string sql, SQLiteConnection con)
        {
            return ExecuteScalar(sql, con);
        }
        public static void SaveDataTable(SQLiteConnection con, DataTable DT, object[,] valueArray, List<string> columnList,int rowcount, string tablename = "multivariate")
        {
            string query = string.Empty;
            try
            {
                //  _log.Info("------------STARTING DB UPDATE--------");

                using (SQLiteTransaction tr = con.BeginTransaction())
                {
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(con))
                        {
                            _log.Info("-------FOR LOOP START-------");
                            //int rowcount = valueArray.GetLength(0);
                            for (int i = 2; i < rowcount+2; i++)//1st loc is header
                            {
                                 //_log.Info("="+i+"=");
                               // if (!string.IsNullOrEmpty(Convert.ToString(valueArray[i, 1])))
                               // {
                                    query = "Update " + tablename + " set ";
                                    for (int x = 1; x <= columnList.Count; x++)
                                    {
                                        if (x > 1)
                                        {
                                            query += ",";
                                        }
                                        query += columnList[x - 1] + " = '" + valueArray[i, x + 1] + "'";
                                    }
                                    query += " where sort_no = " + valueArray[i, 1];
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                   // _log.Info("=" + i + "= /done");
                             //   }
                              //  else { break; }

                            }

                            _log.Info("-------FOR LOOP FINISHED-------");
                        }
                        tr.Commit();
                    }
                    catch (Exception ex)
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

        public static void CreateMultivariateTable(SQLiteConnection cnn)
        {
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
                {

                    mycommand.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'answers'";
                    SQLiteDataReader reader = mycommand.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        mycommand.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'multivariate'";
                        SQLiteDataReader multivariatereader = mycommand.ExecuteReader();
                        if (!multivariatereader.Read())
                        {

                            multivariatereader.Close();
                            string daptable_query = "CREATE TABLE multivariate (sample_id VARCHAR(255) ,sort_no INTEGER PRIMARY KEY AUTOINCREMENT )";
                            mycommand.CommandText = daptable_query;
                            mycommand.ExecuteNonQuery();
                            try
                            {
                                mycommand.CommandText = "SELECT sample_id,sort_no FROM answers";
                                SQLiteDataReader rdr = mycommand.ExecuteReader();
                                string columns = string.Empty;
                                for (var i = 0; i < rdr.FieldCount; i++)
                                {
                                    columns += (i > 0 ? "," : string.Empty) + rdr.GetName(i);
                                }
                                rdr.Close();
                                mycommand.CommandText = "INSERT INTO multivariate(" + columns + ") SELECT " + columns + " FROM answers";
                                mycommand.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message);
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static int AlterTable(Excel.Workbook workbook, string query)//by 191  for altering dataafterprocess
        {
            int success = 0;
            using (SQLiteConnection cnn = DBHelper.GetConnection(DBHelper.GetConnectionString(workbook)))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = query.ToString();
                    cmd.CommandType = CommandType.Text;
                    success = Convert.ToInt32(cmd.ExecuteScalar());
                }
                cnn.Close();
            }
            return success;
        }

        public static bool CreateMultivariateTable(Excel.Workbook workbook)
        {
            bool returnvalue = true;
            try
            {
                using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(workbook)))
                {
                    dbSource.Open();
                    DBHelper.CreateMultivariateTable(dbSource);
                    dbSource.Close();
                }
            }
            catch (Exception ex)
            {
                returnvalue = false;
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            return returnvalue;
        }

        public static void SaveDataTable(Excel.Workbook workbook, DataTable DT, object[,] valueArray, List<string> columnList, int rowcount, string tablename = "multivariate")
        {
            try
            {
                SQLiteConnection dbcon = DBHelper.GetConnection(DBHelper.GetConnectionString(workbook));
                dbcon.Open();
                SaveDataTable(dbcon, DT, valueArray, columnList, rowcount, tablename);
                dbcon.Close();

            }
            catch (Exception ex)
            {

                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        public static void DeleteVariableFromTable(Excel.Workbook workbook)
        {
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(DBHelper.GetConnection(DBHelper.GetConnectionString(workbook))))
                {
                    mycommand.Connection.Open();
                    string dp_sql = "Select * from ";
                    mycommand.CommandText = dp_sql.ToString();
                    mycommand.ExecuteNonQuery();
                    mycommand.Connection.Close();
                }
            }
            catch (Exception ex) { }
        }
        public static void DeleteMultivariateTable(Excel.Workbook workbook)
        {
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(DBHelper.GetConnection(DBHelper.GetConnectionString(workbook))))
                {
                    mycommand.Connection.Open();
                    string dp_sql = "DROP TABLE IF EXISTS multivariate";
                    mycommand.CommandText = dp_sql.ToString();
                    mycommand.ExecuteNonQuery();
                    mycommand.Connection.Close();
                }
            }
            catch (Exception ex) { }
        }

        internal static bool checkIsMultivariateVariable(Excel.Workbook workbook, string variableName)
        {
            int cnt = 0;
            using (SQLiteConnection con = GetConnection(GetConnectionString(workbook)))
            {
                con.Open();
                string sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('multivariate') WHERE name='" + variableName + "'";
                cnt = Convert.ToInt32(GetDataTable(sql, con).Rows[0][0].ToString());
            }
            return cnt > 0;
        }
        internal static bool checkIsAnVariable(Excel.Workbook workbook, string variableName)
        {
            int cnt = 0;
            using (SQLiteConnection con = GetConnection(GetConnectionString(workbook)))
            {
                con.Open();
                string sql = "SELECT count(id) from question where variable = '" + variableName + "' and question_flag = 'An'";
                cnt = Convert.ToInt32(GetDataTable(sql, con).Rows[0][0].ToString());
            }
            return cnt > 0;
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
        public static void CreateDataprocessTable(Excel.Workbook workbook)
        {
            try
            {
                SQLiteConnection dbcon = DBHelper.GetConnection(DBHelper.GetConnectionString(workbook));
                dbcon.Open();
                DBHelper.CreateDataprocessTable(dbcon);
                dbcon.Close();

            }
            catch (Exception ex)
            {

                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        public static bool AlterMultivariateTable(Excel.Workbook workbook, string newfields)
        {
            try
            {
                SQLiteConnection dbcon = DBHelper.GetConnection(DBHelper.GetConnectionString(workbook));
                dbcon.Open();
                DBHelper.AlterMultivariateTable(dbcon, newfields);
                dbcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                return false;
            }
        }
        public static void AlterMultivariateTable(SQLiteConnection cnn, string newfields)
        {
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
                {
                    mycommand.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'multivariate'";
                    SQLiteDataReader reader = mycommand.ExecuteReader();
                    if (reader.Read())
                    {
                        string daptable_query = reader.GetString(0);
                        daptable_query = daptable_query.Replace("multivariate", "multivariate_T");
                        if (!string.IsNullOrEmpty(newfields))
                        {
                            daptable_query = daptable_query.Remove(daptable_query.Length - 1, 1);
                            daptable_query += newfields;
                            daptable_query = daptable_query + ")";
                        }

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
                        mycommand.CommandText = "INSERT INTO multivariate_T(" + columns + ") SELECT " + columns + " FROM multivariate";
                        mycommand.ExecuteNonQuery();

                        string dp_sql = "DROP TABLE IF EXISTS multivariate";
                        mycommand.CommandText = dp_sql.ToString();
                        mycommand.ExecuteNonQuery();

                        mycommand.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'table' AND name = 'multivariate_T'";
                        reader = mycommand.ExecuteReader();
                        if (reader.Read())
                        {
                            daptable_query = reader.GetString(0);
                            daptable_query = daptable_query.Replace("multivariate_T", "multivariate");
                            reader.Close();
                            mycommand.CommandText = daptable_query;
                            mycommand.ExecuteNonQuery();

                            mycommand.CommandText = "SELECT * FROM multivariate_T";
                            rdr = mycommand.ExecuteReader();
                            columns = string.Empty;
                            for (var i = 0; i < rdr.FieldCount; i++)
                            {
                                columns += (i > 0 ? "," : string.Empty) + rdr.GetName(i);
                            }
                            rdr.Close();
                            mycommand.CommandText = "INSERT INTO multivariate(" + columns + ") SELECT " + columns + " FROM multivariate_T";
                            mycommand.ExecuteNonQuery();

                            dp_sql = "DROP TABLE IF EXISTS multivariate_T";
                            mycommand.CommandText = dp_sql.ToString();
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
    }
}
