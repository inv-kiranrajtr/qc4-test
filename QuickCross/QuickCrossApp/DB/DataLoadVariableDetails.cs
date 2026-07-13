using ExcelAddIn.Sheets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.DB
{
    class DataLoadVariableDetails
    {
        private string ConnectionString { get; set; }

        internal DataLoadVariableDetails(Microsoft.Office.Interop.Excel.Workbook source)
        {
            ConnectionString = DBHelper.GetConnectionString(source);
        }
        public DataTable GetVariableValues(string Questionvariable)
        {
            string q = "SELECT id,variable FROM question WHERE variable = @id";
            DataTable dt = GetVariableId(Questionvariable,q);
            if (dt.Rows.Count > 0)
            {
                string qid = dt.Rows[0][0].ToString();
                Questionvariable = "q_" + qid;
                if (IsDataExistInTable("SELECT COUNT(*) AS ColCount FROM pragma_table_info('data_after_process') WHERE name='" + Questionvariable + "'"))
                {
                    q = "select " + Questionvariable + " ,count(*) from data_after_process group by 1 order by 1";
                    dt = GetVariableNPer(q);
                }
                else if(IsAnVar(qid))
                {
                    q = "select " + Questionvariable + " ,count(*) from multivariate group by 1 order by 1";
                    dt = GetVariableNPer(q);
                }
                else
                {
                    if (IsDataExistInTable("SELECT COUNT(*) AS ColCount FROM pragma_table_info('answers') WHERE name='" + Questionvariable + "'"))
                    {
                        q = "select " + Questionvariable + " ,count(*) from answers group by 1 order by 1";
                        dt = GetVariableNPer(q);
                    }
                    else
                    {
                        dt = null;
                    }
                }
            }
            return dt;
        }

        private bool IsAnVar(string qid)
        {
            using (SQLiteConnection connection = DBHelper.GetConnection(ConnectionString))
            {
                connection.Open();
                return DBHelper.GetDataTable("select question_flag from question where id = " + qid, connection).Rows[0][0].ToString() == "An";
            }
        }

        private bool IsDataExistInTable(string sql)
        {
            int colCount = 0;
            using (SQLiteConnection connection = DBHelper.GetConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    colCount = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return colCount > 0;
        }

        private DataTable GetVariableId(string Questionvariable, String query)
        {
            try
            {                
                DataTable dt = new DataTable();
                using (SQLiteConnection connection = DBHelper.GetConnection(ConnectionString))
                {
                    connection.Open();
                    
                    if (Questionvariable != null)
                    {
                        string sql =query;
                                             
                          /*select id from question where variable='PREFECTURE';
                            select q_6,count(*) from answers group by 1 order by 1*/
                        using (SQLiteCommand command = connection.CreateCommand())
                        {
                            command.CommandText = sql;

                            command.Parameters.Add(new SQLiteParameter("@id", Questionvariable));

                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                dt.Load(reader);
                            }
                        }
                    }                   
                    return dt;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private DataTable GetVariableNPer(String query)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection connection = DBHelper.GetConnection(ConnectionString))
                {
                    connection.Open();

                    if (query != null)
                    {
                        string sql =query;

                        /*select id from question where variable='PREFECTURE';
                          select q_6,count(*) from answers group by 1 order by 1*/
                        using (SQLiteCommand command = connection.CreateCommand())
                        {
                            command.CommandText = sql;

                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    return dt;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
