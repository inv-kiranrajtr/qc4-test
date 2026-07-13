using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Qc4Launcher.Util;

namespace Qc4Launcher.DB
{
    class DataSheetDao
    {
        private SQLiteConnection connection;
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataSheetDao(string connectionString)
        {
            connection = new SQLiteConnection(connectionString);
        }

        internal void CreateAnswer(List<Qc3Parse.QDataDetail> qData)
        {
            StringBuilder str = new StringBuilder();
            str.Append("CREATE TABLE IF NOT EXISTS `answers` (sample_id VARCHAR(255) ,sort_no INTEGER PRIMARY KEY AUTOINCREMENT");//changed tablename for phase3,now reverted
            for (int i = 1; i < qData.Count(); i++)
            {
                if (!qData[i].isFound)
                {
                    continue;
                }

                int count = 5000;
                switch (qData[i].answerType)
                {
                    case Constants.AnswerType.D:
                        count = 20;
                        break;
                    //case Constants.AnswerType.FA:
                    //break;
                    case Constants.AnswerType.MA:
                        count = 1000;
                        break;
                    //case Constants.AnswerType.N:
                    //break;
                    case Constants.AnswerType.SA:
                        count = 4;
                        break;
                }
                str.Append(" , q_" + qData[i].questionOrder + " TEXT");
            }
            str.Append(")");

            try
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = str.ToString();
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        internal void InsertData(Object[,] objAry, List<Qc3Parse.QDataDetail> qData, Qc3Parse qc3Parse, double pbVal, bool pbUpdate)
        {
            string sql = "insert into answers ( sample_id";//changed tablename for phase3,now reverted
            string values = "  values( @sample_id";
            for (int j = 1; j < qData.Count(); j++)
            {
                sql += " , q_" + qData[j].questionOrder;
                values += " , @q_" + qData[j].questionOrder;
            }
            sql += " ) ";
            values += " ) ";

            try
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    Console.WriteLine(" START    >>>>>>>>>>>>>>>>" + DateTime.Now);
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;

                        for (int i = 0; i < objAry.GetLength(0); i++)
                        {
                            command.CommandText = sql + values;
                            SQLiteParameter sQLiteParameter = new SQLiteParameter("@sample_id", System.Data.DbType.String);
                            sQLiteParameter.Value = objAry[i, 0];
                            command.Parameters.Add(sQLiteParameter);
                            for (int j = 1; j < objAry.GetLength(1); j++)
                            {
                                sQLiteParameter = new SQLiteParameter("@q_" + j, System.Data.DbType.String);
                                sQLiteParameter.Value = objAry[i, j];
                                command.Parameters.Add(sQLiteParameter);
                            }
                            command.ExecuteNonQuery();
                            if (pbUpdate)
                            {
                                qc3Parse.ProgressUpdate(qc3Parse.prec += pbVal, "");
                            }
                        }
                    }
                    transaction.Commit();
                    Console.WriteLine(" END    >>>>>>>>>>>>>>>>" + DateTime.Now);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        internal void InsertAnswerData(Object[,] objAry, List<Qc3Parse.QDataDetail> qData, Forms.QCM.ProgressUpdate progressUpdate, double pbVal, bool pbUpdate)
        {
            string sql = "insert into answers ( sample_id";
            string values = "  values( @sample_id";
            for (int j = 1; j < qData.Count(); j++)
            {
                sql += " , q_" + qData[j].questionOrder;
                values += " , @q_" + qData[j].questionOrder;
            }
            sql += " ) ";
            values += " ) ";

            try
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    double value = 0;
                    Console.WriteLine(" START    >>>>>>>>>>>>>>>>" + DateTime.Now);
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;

                        for (int i = 0; i < objAry.GetLength(0); i++)
                        {
                            command.CommandText = sql + values;
                            SQLiteParameter sQLiteParameter = new SQLiteParameter("@sample_id", System.Data.DbType.String);
                            sQLiteParameter.Value = objAry[i, 0];
                            command.Parameters.Add(sQLiteParameter);
                            for (int j = 1; j < objAry.GetLength(1); j++)
                            {
                                sQLiteParameter = new SQLiteParameter("@q_" + j, System.Data.DbType.String);
                                sQLiteParameter.Value = objAry[i, j];
                                command.Parameters.Add(sQLiteParameter);
                            }
                            command.ExecuteNonQuery();
                            if (pbUpdate)
                            {
                                value += pbVal;
                                if (value > 1)
                                {
                                    progressUpdate.UpdateProgress(progressUpdate.prec += value, "");
                                    value = 0;
                                }
                            }
                        }
                    }
                    transaction.Commit();
                    Console.WriteLine(" END    >>>>>>>>>>>>>>>>" + DateTime.Now);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        internal void InsertAnswerData2(List<String[]> valueList, List<Qc3Parse.QDataDetail> qData, Forms.QCM.ProgressUpdate progressUpdate, double pbVal, bool pbUpdate, string sql)
        {
            try
            {
                connection.Open();
                double value = 0;
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        command.CommandText = sql;
                        command.Parameters.AddWithValue("@sample_id", "");
                        for (int j = 1; j < qData.Count; j++)
                        {
                            command.Parameters.AddWithValue("@q_" + j, "");
                        }
                        for (int i = 0; i < valueList.Count(); i++)
                        {
                            command.Parameters["@sample_id"].Value = valueList[i][0];
                            for (int j = 1; j < valueList[0].Length; j++)
                            {
                                command.Parameters["@q_" + j].Value = valueList[i][j];
                            }
                            command.ExecuteNonQuery();

                            if (pbUpdate)
                            {
                                value += pbVal;
                                if (value > 1)
                                {
                                    progressUpdate.UpdateProgress(progressUpdate.prec += value, "");
                                    value = 0;
                                }
                            }
                        }
                        transaction.Commit();
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
    }
}

