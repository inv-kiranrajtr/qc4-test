using QC4Common.Common;
using QC4Common.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using log4net;
using System.Reflection;

namespace QC4Common.DB
{
    public class DBHelper
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static string GetConnectionString(string filePath)
        {
            bool isPath = UNC.IsNetworkPath(filePath);
            if (isPath == true)
            {
                filePath = filePath.Replace(@"\\", @"\\\\");
            }
            if (filePath.Contains(";"))
            {
                filePath = "\"" + filePath + "\"";
            }
            SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder
            {
                DataSource = filePath,
                Version = 3,
                Password = Constants.Password
            };
            string constring = csb.ToString();
            return constring;
            
        }

        public static SQLiteConnection GetConnectionStringByWrokbook(Excel.Workbook workbook)
        {
            Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting);
            return new SQLiteConnection(sheet.Cells[23, 4].Text);
        }

        public static bool checkNegetiveNumberInData(Excel.Workbook workbook, string column)
        {
            if (column == "" || column == "WeightBack")
                return false;
            int cnt = 0;
            string col = "";
            try
            {
                using (SQLiteConnection con = GetConnection(GetConnectionString(workbook)))
                {
                    con.Open();
                    string sql = "SELECT count(name) FROM sqlite_master WHERE type='table' AND name='data_after_process';";
                    cnt = ExecuteScalar(sql, con);
                    sql = "select id from question where variable = '" + column + "';";
                    col = "q_" + ExecuteScalar(sql, con).ToString();
                    if (cnt > 0)
                    {
                        sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('data_after_process') WHERE name='" + col + "'";
                        cnt = ExecuteScalar(sql, con);
                        if (cnt > 0)
                        {
                            sql = "select count(" + col + ") from data_after_process where CAST(" + col + " as INTEGER)<0;";
                            cnt = ExecuteScalar(sql, con);
                        }
                    }
                    else
                    {
                        sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('answers') WHERE name='" + col + "'";
                        cnt = ExecuteScalar(sql, con);
                        if (cnt > 0)
                        {
                            sql = "select count(" + col + ") from answers where CAST(" + col + " as INTEGER)<0;";
                            cnt = ExecuteScalar(sql, con);
                        }
                    }
                }
                return cnt > 0;
            }
            catch (Exception ex)
            {
                return true;
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

        public static SQLiteConnection GetConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
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
                    //SQLiteDataReader reader = mycommand.ExecuteReader();
                    //dt.Load(reader);
                    //reader.Close();
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
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand mycommand = connection.CreateCommand())
                    {
                        mycommand.CommandText = sql;
                        SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(mycommand);
                        dataAdapter.Fill(dt);
                        //SQLiteDataReader reader = mycommand.ExecuteReader();
                        //dt.Load(reader);
                        //reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
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

        public static void SetConnectionString(Excel.Workbook workbook, string dbPath, string fileName = "", string targetPath = null, string ProcessId = "")
        {
            Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting);
            if (sheet != null)
            {
                sheet.Cells[23, 4].Value = GetConnectionString(dbPath + @"\" + Constants.TemplateFile.DB_FIlE);
                sheet.Cells[24, 4].Value = (fileName.StartsWith("=") ? "'" : "") + fileName;
                sheet.Cells[26, 4].Value = dbPath;
                if (!string.IsNullOrEmpty(ProcessId))
                {
                    sheet.Cells[30, 4].Value = ProcessId;
                }

                if (targetPath == null)
                {
                    return;
                }

                if (!targetPath.EndsWith(".qc4"))
                {
                    targetPath += ".qc4";
                }
                sheet.Cells[27, 4].Value = targetPath;
            }
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

        public static void CheckIfColumnExists(Excel.Workbook workbook, List<Model.QuestionSettings> questions, out List<string> variables, out List<string> columns, out List<decimal> ids)
        {
            string connectionString = GetConnectionString(workbook);
            string tableName = "answers";
            bool isContainMultivariate = false;
            if (checkAfterProcess(workbook))
            {
                tableName = "data_after_process";
            }
            if (questions.Any(x => x.QuestionFlag == "An"))
                isContainMultivariate = true;

            columns = questions.Where(q => q.Id != 0).Select(a => "q_" + a.Id).ToList();
            ids = questions.Where(q => q.Id != 0).Select(a => a.Id).ToList();
            variables = questions.Where(q => q.Id != 0).Select(a => a.Variable).ToList();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    int nameIndex = reader.GetOrdinal("Name");
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    int maxRow = dt.Rows.Count;
                    for (int i = 0; i < maxRow; i++)
                    {
                        int index = columns.IndexOf(dt.Rows[i][nameIndex].ToString());
                        if (index != -1)
                        {
                            variables.RemoveAt(index);
                            columns.RemoveAt(index);
                            ids.RemoveAt(index);
                        }
                    }
                }
                conn.Close();
            }
            if (isContainMultivariate)
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("PRAGMA table_info({0})", "multivariate");

                        SQLiteDataReader reader = cmd.ExecuteReader();
                        int nameIndex = reader.GetOrdinal("Name");
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        int maxRow = dt.Rows.Count;
                        for (int i = 0; i < maxRow; i++)
                        {
                            int index = columns.IndexOf(dt.Rows[i][nameIndex].ToString());
                            if (index != -1)
                            {
                                variables.RemoveAt(index);
                                columns.RemoveAt(index);
                                ids.RemoveAt(index);
                            }
                        }
                    }
                    conn.Close();
                }
            }
        }

        public static string getTableName(Excel.Workbook workbook, string columnName, out bool isMv)
        {
            string connectionString = GetConnectionString(workbook);
            string tableName = "answers";
            bool isDAp = false;
            if (checkAfterProcess(workbook))
            {
                tableName = "data_after_process";
                isDAp = true;
            }
            isMv = false;
            bool colExist = false;
            string tableNameAnswer = tableName;

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    int nameIndex = reader.GetOrdinal("Name");
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    int maxRow = dt.Rows.Count;
                    for (int i = 0; i < maxRow; i++)
                    {
                        if (columnName == dt.Rows[i][nameIndex].ToString())
                        {
                            colExist = true;
                            break;
                        }
                    }
                    if (!colExist)
                    {
                        tableName = "multivariate";
                        cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

                        reader = cmd.ExecuteReader();
                        nameIndex = reader.GetOrdinal("Name");
                        dt = new DataTable();
                        dt.Load(reader);
                        maxRow = dt.Rows.Count;
                        for (int i = 0; i < maxRow; i++)
                        {
                            if (columnName == dt.Rows[i][nameIndex].ToString())
                            {
                                colExist = true;
                                isMv = isDAp ? true : false;
                                break;
                            }
                        }
                    }

                }
                conn.Close();
            }
            if (colExist)
            {
                return tableName;
            }
            return tableNameAnswer; 
        }

        public static string getTableName(SQLiteConnection con, string columnName, out bool isMv)
        {
            //string connectionString = GetConnectionString(workbook);
            string tableName = "answers";
            bool isDAp = false;
            if (checkAfterProcess(con))
            {
                tableName = "data_after_process";
                isDAp = true;
            }
            isMv = false;
            bool colExist = false;
            string tableNameAnswer = tableName;

            //using (var conn = new SQLiteConnection(connectionString))
            {
                //conn.Open();
                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    int nameIndex = reader.GetOrdinal("Name");
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    int maxRow = dt.Rows.Count;
                    for (int i = 0; i < maxRow; i++)
                    {
                        if (columnName == dt.Rows[i][nameIndex].ToString())
                        {
                            colExist = true;
                            break;
                        }
                    }
                    if (!colExist)
                    {
                        tableName = "multivariate";
                        cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

                        reader = cmd.ExecuteReader();
                        nameIndex = reader.GetOrdinal("Name");
                        dt = new DataTable();
                        dt.Load(reader);
                        maxRow = dt.Rows.Count;
                        for (int i = 0; i < maxRow; i++)
                        {
                            if (columnName == dt.Rows[i][nameIndex].ToString())
                            {
                                colExist = true;
                                isMv = isDAp ? true : false;
                                break;
                            }
                        }
                    }

                }
                //conn.Close();
            }
            if (colExist)
            {
                return tableName;
            }
            return tableNameAnswer;
        }

        public static int CheckIfNewExists(Excel.Workbook workbook, List<Model.QuestionSettings> questions)
        {
            string connectionString = GetConnectionString(workbook);
            string tableName = "answers";
            bool isContainMultivariate = false;
            if (checkAfterProcess(workbook))
            {
                tableName = "data_after_process";
            }
            if (questions.Any(x => x.QuestionFlag == "An"))
                isContainMultivariate = true;

            var columns = questions.Where(q => q.Id != 0).Select(a => "q_" + a.Id).ToList();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    int nameIndex = reader.GetOrdinal("Name");
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    int maxRow = dt.Rows.Count;
                    for (int i = 0; i < maxRow; i++)
                    {
                        int index = columns.IndexOf(dt.Rows[i][nameIndex].ToString());
                        if (index != -1)
                        {
                            columns.RemoveAt(index);
                        }
                    }
                }
                conn.Close();
            }
            if (isContainMultivariate)
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("PRAGMA table_info({0})", "multivariate");

                        SQLiteDataReader reader = cmd.ExecuteReader();
                        int nameIndex = reader.GetOrdinal("Name");
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        int maxRow = dt.Rows.Count;
                        for (int i = 0; i < maxRow; i++)
                        {
                            int index = columns.IndexOf(dt.Rows[i][nameIndex].ToString());
                            if (index != -1)
                            {
                                columns.RemoveAt(index);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            return columns.Count;
        }

        public static void GetAnswerTableColumns(Excel.Workbook workbook, out List<string> columns)
        {
            string connectionString = GetConnectionString(workbook);
            string tableName = "answers";
            if (checkAfterProcess(workbook))
            {
                tableName = "data_after_process";
            }
            columns = new List<string>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    int nameIndex = reader.GetOrdinal("Name");
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    int maxRow = dt.Rows.Count;
                    for (int i = 0; i < maxRow; i++)
                    {
                        columns.Add(dt.Rows[i][nameIndex].ToString());
                    }
                }
                conn.Close();
            }
        }

        public static int ExecuteScalarr(string sql, SQLiteConnection con)
        {
            using (SQLiteCommand command = con.CreateCommand())
            {
                command.CommandText = sql;
                int RowCount = 0;
                RowCount = Convert.ToInt32(command.ExecuteScalar());
                return RowCount;
            }
        }
        public static bool checkMultiTableExists(Excel.Workbook workbook)
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
        
        public static bool checkAfterProcess(Excel.Workbook workbook)
        {
            using (System.Data.SQLite.SQLiteConnection con = GetConnection(DBHelper.GetConnectionString(workbook)))
            {
                using (SQLiteCommand command = con.CreateCommand())
                {
                    con.Open();
                    string sql = "SELECT count(name) FROM sqlite_master WHERE type='table' AND name='data_after_process'";
                    int cnt = ExecuteScalarr(sql, con);
                    return cnt > 0;
                }
            }
        }

        public static bool checkAfterProcess(SQLiteConnection con)
        {
            //using (System.Data.SQLite.SQLiteConnection con = GetConnection(DBHelper.GetConnectionString(workbook)))
            {
                using (SQLiteCommand command = con.CreateCommand())
                {
                    //con.Open();
                    string sql = "SELECT count(name) FROM sqlite_master WHERE type='table' AND name='data_after_process'";
                    int cnt = ExecuteScalarr(sql, con);
                    return cnt > 0;
                }
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
                            for (int c = 0; c < columnList.Count; c++)
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
                                for (int j = 0, pref = 0, pref2 = 0; j < columnList.Count; j++)
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
                                        value = value.Replace(@"'", @"''");
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
                                if (isAn)
                                {
                                    cmd.CommandText = query2;
                                    cmd.ExecuteNonQuery();
                                }

                            }


                        }
                        tr.Commit();
                    }
                    catch (Exception ex)
                    {
                        _log.LogErrorForExcel(ex.Message);
                        try
                        {
                            tr.Rollback();
                        }
                        catch (Exception ex2)
                        { _log.LogErrorForExcel(ex2.Message); }
                    }
                }

            }
            catch (Exception Ex)
            {
                System.Windows.MessageBox.Show(Ex.Message, "QuickCross");
                _log.LogErrorForExcel(Ex.Message);
            }
            _log.Info("------------ENGING DB UPDATE--------");
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
    }
}
