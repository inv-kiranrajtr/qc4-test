using log4net;
using Qc4Launcher.Model;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Qc4Launcher.Util.Constants;
using static Qc4Launcher.Util.Enums;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.DB
{
    public class DataImportTrans
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        class RowNdValue
        {
            public string RowId { get; set; }
            public string Value { get; set; }
        }
        /// <summary>
        /// Method to update a new column in database
        /// </summary>
        /// <param name="DbPath">Database connection string</param>
        /// <param name="workBook">QC4 Excel workbook</param>
        /// <param name="columnImportSettings">New column informations</param>
        /// <returns>bool value that represents whether the database updation was successfull</returns>
        public bool UpdateNewColumns(string DbPath, Excel.Workbook workBook, ColumnImportSettings columnImportSettings)
        {
            string notApplicableChar = columnImportSettings.NotApplicableCharacter;
            try
            {
                if (notApplicableChar == null || notApplicableChar.Trim().Length == 0)
                {
                    columnImportSettings.NotApplicableCharacter = DatatableSettings.DummyColumnSpecifier;
                }
                using (SQLiteConnection dbConn = DBHelper.GetConnection(DBHelper.GetConnectionString(DbPath)))
                {
                    dbConn.Open();

                    string sSql = "Select IfNull(Max(id),0)  As MAXID  from question ";
                    DataTable dt = DBHelper.GetDataTable(sSql, dbConn);
                    int newId = Convert.ToInt16(dt.Rows[0]["MAXID"].ToString()) + 1;

                    using (SQLiteTransaction tr = dbConn.BeginTransaction())
                    {
                        using (SQLiteCommand mapSql = dbConn.CreateCommand())
                        {
                            mapSql.Transaction = tr;
                            try
                            {
                                for (int i = 0; i <= columnImportSettings.ImportInformations.Count - 1; i++)
                                {
                                    if ((columnImportSettings.ImportInformations[i].AnswerType == AnswerType.FA) || (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.N))
                                    {
                                        columnImportSettings.ImportInformations[i].NoOfChoices = 0;
                                        columnImportSettings.ImportInformations[i].ChoiceWordings = new List<ChoiceWording>();
                                    }

                                    sSql = " Insert Into question(id, variable,answer_type,category_count,question_flag) ";
                                    sSql = sSql + " Values(@id, @variable, @answertype, @categorycount,'Imp') ";
                                    mapSql.CommandText = sSql;
                                    mapSql.Parameters.Clear();
                                    mapSql.Parameters.Add(new SQLiteParameter("@id", newId));
                                    mapSql.Parameters.Add(new SQLiteParameter("@variable", columnImportSettings.ImportInformations[i].VariableName));
                                    mapSql.Parameters.Add(new SQLiteParameter("@answertype", columnImportSettings.ImportInformations[i].AnswerType.ToUpper()));
                                    mapSql.Parameters.Add(new SQLiteParameter("@categorycount", columnImportSettings.ImportInformations[i].NoOfChoices));
                                    mapSql.ExecuteNonQuery();

                                    string dataType = DBHelper.GetDataType(columnImportSettings.ImportInformations[i].AnswerType);
                                    string newColumnName = DBSettings.ColumnNamePreText + newId.ToString();
                                    columnImportSettings.ImportInformations[i].DBColumn = newColumnName;
                                    columnImportSettings.ImportInformations[i].DBQuestionId = newId;

                                    sSql = " ALTER TABLE answers ADD COLUMN `" + newColumnName + "` " + dataType + " NULL ";
                                    mapSql.CommandText = sSql;
                                    mapSql.Parameters.Clear();
                                    mapSql.ExecuteNonQuery();

                                    if (columnImportSettings.IsDataProcessed)
                                    {
                                        sSql = " ALTER TABLE data_after_process ADD COLUMN `" + newColumnName + "` " + dataType + " NULL ";
                                        mapSql.CommandText = sSql;
                                        mapSql.Parameters.Clear();
                                        mapSql.ExecuteNonQuery();
                                    }

                                    newId++;
                                }

                                bool IsFile2Exist = false;

                                if (columnImportSettings.DestinationFileKey1 != ComboBoxSettings.NoneText && columnImportSettings.SourceFileKey1 != ComboBoxSettings.NoneText)
                                {
                                    if (columnImportSettings.DestinationFileKey2 != ComboBoxSettings.NoneText && columnImportSettings.SourceFileKey2 != ComboBoxSettings.NoneText)
                                        IsFile2Exist = true;

                                    sSql = " DROP TABLE IF EXISTS `" + DataImportSettings.DataImportTempAnswers + "` ";
                                    mapSql.CommandText = sSql;
                                    mapSql.ExecuteNonQuery();

                                    sSql = " Create Temp Table `" + DataImportSettings.DataImportTempAnswers + "` As  ";
                                    sSql += " Select A.* from `answers` A ";
                                    sSql += " Left Join `" + DataImportSettings.DataImportSourceTempTable + "` D On ";

                                    sSql += GetQueryWithKey(columnImportSettings, IsFile2Exist, "A", "D");

                                    mapSql.CommandText = sSql;
                                    mapSql.ExecuteNonQuery();
                                    if (columnImportSettings.IsDataProcessed)
                                    {
                                        sSql = " DROP TABLE IF EXISTS `" + DataImportSettings.DataImportTempDataAfterProcess + "` ";
                                        mapSql.CommandText = sSql;
                                        mapSql.ExecuteNonQuery();

                                        sSql = " Create Temp Table `" + DataImportSettings.DataImportTempDataAfterProcess + "` As  ";
                                        sSql += " Select A.* from `data_after_process` A ";
                                        sSql += " left Join `answers` D On   A.`sort_no` = D.`sort_no`;";
                                        mapSql.CommandText = sSql;
                                        mapSql.ExecuteNonQuery();
                                    }

                                    for (int i = 0; i <= columnImportSettings.ImportInformations.Count - 1; i++)
                                    {
                                        sSql = " Update " + DataImportSettings.DataImportTempAnswers + " As a Set ";
                                        sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";

                                        string sourceColumnName = "TRIM(`" + columnImportSettings.ImportInformations[i].ColumnName + "`)";
                                        if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.MA && columnImportSettings.MAformat == MAFormat.FlagFormat)
                                        {
                                            int givenMa = columnImportSettings.ImportInformations[i].MAColumns.Count;
                                            int colMa = 0;
                                            int ColumnLimit = columnImportSettings.ImportInformations[i].MAColumns.Count;
                                            int maDiv = 0;
                                            while (givenMa != 0)
                                            {
                                                maDiv++;
                                                if (maDiv == 1)//Redmine Id:224239
                                                    sSql += "CASE WHEN(`" + columnImportSettings.ImportInformations[i].DBColumn + "` is NULL) THEN '' ELSE `" + columnImportSettings.ImportInformations[i].DBColumn + "` END || ";
                                                if (givenMa > DBSettings.MaxNoOfColumnInsertInBulk)
                                                {
                                                    ColumnLimit = DBSettings.MaxNoOfColumnInsertInBulk - 1;
                                                    givenMa = givenMa - (DBSettings.MaxNoOfColumnInsertInBulk - 1);
                                                }
                                                else
                                                {
                                                    ColumnLimit = givenMa;
                                                    givenMa = 0;
                                                }
                                                sourceColumnName = "";
                                                if (colMa == 0 && columnImportSettings.IsDataProcessed)
                                                {
                                                    string sSql1 = " DROP TABLE IF EXISTS `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` ";
                                                    mapSql.CommandText = sSql1;
                                                    mapSql.ExecuteNonQuery();

                                                    sSql1 = " Create Temp Table `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` As  ";
                                                    sSql1 += " Select A.* from `answers` A ";
                                                    sSql1 += " Join `data_after_process` D On   A.`sort_no` = D.`sort_no`;";
                                                    mapSql.CommandText = sSql1;
                                                    mapSql.ExecuteNonQuery();
                                                }
                                                for (int c = 0; c < ColumnLimit; c++)
                                                {
                                                    string MAColumn = columnImportSettings.ImportInformations[i].MAColumns[colMa];
                                                    colMa++;
                                                    if (sourceColumnName == "")
                                                    {
                                                        sourceColumnName = " case when(TRIM(`" + MAColumn + "`)='' or `" + MAColumn + "`=null) then  '0' else TRIM(`" + MAColumn + "`) end ";
                                                        continue;
                                                    }
                                                    sourceColumnName = sourceColumnName + "|| case when(TRIM(`" + MAColumn + "`)='' or `" + MAColumn + "`=null) then  '0' else TRIM(`" + MAColumn + "`) end ";
                                                }

                                                string sSl = "DROP TABLE IF EXISTS tempdata;";
                                                mapSql.CommandText = sSl;
                                                mapSql.ExecuteNonQuery();
                                                sSl = "";
                                                sSl += " CREATE TEMP TABLE tempdata as Select Replace(" + sourceColumnName + ",'" + columnImportSettings.NotApplicableCharacter.Replace("'", "''") + "'  ";
                                                sSl += " ,'" + DBSettings.NotApplicableCharacter + "') as data ";
                                                sSl += " From `" + DataImportSettings.DataImportTempAnswers + "` a ";
                                                sSl += " LEFT JOIN `" + DataImportSettings.DataImportSourceTempTable + "` t ON ";
                                                sSl += GetQueryWithKeyS(columnImportSettings, IsFile2Exist, "a", "t");
                                                mapSql.CommandText = sSl;
                                                mapSql.ExecuteNonQuery();

                                                if (maDiv == 1)
                                                    sSql += " (SELECT data from tempdata WHERE a.ROWID = tempdata.ROWID);";
                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();

                                                if (columnImportSettings.IsDataProcessed)
                                                {
                                                    sSql = " Update `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` As a Set ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";

                                                    sSl = "DROP TABLE IF EXISTS tempdata;";
                                                    sSl += " CREATE TEMP TABLE tempdata as Select Replace(" + sourceColumnName + ",'" + columnImportSettings.NotApplicableCharacter.Replace("'", "''") + "'  ";
                                                    sSl += " ,'" + DBSettings.NotApplicableCharacter + "') as data ";
                                                    sSl += " From `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` a ";
                                                    sSl += " LEFT JOIN `" + DataImportSettings.DataImportSourceTempTable + "` t ";
                                                    sSl += " ON ";
                                                    sSl += GetQueryWithKeyS(columnImportSettings, IsFile2Exist, "a", "t");
                                                    mapSql.CommandText = sSl;
                                                    mapSql.ExecuteNonQuery();

                                                    sSql += " (SELECT data from tempdata WHERE a.ROWID = tempdata.ROWID);";
                                                    mapSql.CommandText = sSql;
                                                    mapSql.ExecuteNonQuery();
                                                }
                                            }

                                            sSql = " Update " + DataImportSettings.DataImportTempAnswers + " set ";
                                            sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                            sSql += " '" + DBSettings.NotApplicableCharacter + "' Where ";
                                            sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` LIKE '%" + DBSettings.NotApplicableCharacter + "%'  ";
                                            mapSql.CommandText = sSql;
                                            mapSql.ExecuteNonQuery();

                                            sSql = " Update " + DataImportSettings.DataImportTempAnswers + " set ";
                                            sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                            sSql += " (  ";
                                            sSql += " WITH reverse(i, c) AS (values(-1, '') UNION ALL SELECT i-1,   ";
                                            sSql += " substr(`" + columnImportSettings.ImportInformations[i].DBColumn + "`, i, 1)    ";
                                            sSql += " AS r FROM reverse WHERE r!='') ";
                                            sSql += " SELECT group_concat(c, '') AS reversed FROM reverse   ";
                                            sSql += " )  ";
                                            mapSql.CommandText = sSql;
                                            mapSql.ExecuteNonQuery();

                                            if (columnImportSettings.IsDataProcessed)
                                            {
                                                sSql = " Update `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                sSql += " '" + DBSettings.NotApplicableCharacter + "' Where ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` LIKE '%" + DBSettings.NotApplicableCharacter + "%'  ";
                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();

                                                sSql = " Update " + DataImportSettings.DataImportTempDataAfterProcess1 + " set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                sSql += " (  ";
                                                sSql += " WITH reverse(i, c) AS (values(-1, '') UNION ALL SELECT i-1,   ";
                                                sSql += " substr(`" + columnImportSettings.ImportInformations[i].DBColumn + "`, i, 1)    ";
                                                sSql += " AS r FROM reverse WHERE r!='') ";
                                                sSql += " SELECT group_concat(c, '') AS reversed FROM reverse   ";
                                                sSql += " )  ";
                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();


                                                sSql = " Update `" + DataImportSettings.DataImportTempDataAfterProcess + "` as A set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";

                                                string sSl1 = "DROP TABLE IF EXISTS tempdata;";
                                                sSl1 += " CREATE TEMP TABLE tempdata as Select D." + columnImportSettings.ImportInformations[i].DBColumn + " as data ";
                                                sSl1 += " From `" + DataImportSettings.DataImportTempDataAfterProcess + "` A ";
                                                sSl1 += " LEFT JOIN `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` D ON A.`sort_no` = D.`sort_no` ";
                                                mapSql.CommandText = sSl1;
                                                mapSql.ExecuteNonQuery();

                                                sSql += " (SELECT data from tempdata WHERE a.ROWID = tempdata.ROWID);";
                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();
                                            }
                                        }
                                        else
                                        {
                                            if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.MA)
                                            {
                                                string srcSql = " (Select t.*,t.ROWID from ";
                                                srcSql += " answers a, " + DataImportSettings.DataImportSourceTempTable + " t ";



                                                bool IsKey1Exist = true;
                                                srcSql += " where ";
                                                srcSql += GetQueryWithKey(columnImportSettings, IsFile2Exist, "a", "t");

                                                srcSql += " ) ";
                                                string tsql = "SELECT CASE WHEN(`" + columnImportSettings.ImportInformations[i].ColumnName + "` = '" + columnImportSettings.NotApplicableCharacter.Replace("'", "''") + "') THEN '" + DBSettings.NotApplicableCharacter + "' ELSE `" + columnImportSettings.ImportInformations[i].ColumnName + "` END as maRows,ROWID FROM " + srcSql;
                                                DataTable maTble = DBHelper.GetDataTable(tsql, dbConn);
                                                string mSql = DataImportHelper.GetMASplittingQueryMax(columnImportSettings.ImportInformations[i].ColumnName, DataImportSettings.DataImportSourceTempTable, columnImportSettings.NotApplicable, columnImportSettings.DataCount, IsFile2Exist, columnImportSettings, dbConn);
                                                DataTable dataTble = DBHelper.GetDataTable(mSql, dbConn);
                                                int maxCount = Convert.ToInt32(dataTble.Rows[0][0]);
                                                List<RowNdValue> maData = new List<RowNdValue>();
                                                for (int m = 0; m < maTble.Rows.Count; m++)
                                                {
                                                    string data = maTble.Rows[m][0].ToString();
                                                    string key = maTble.Rows[m][1].ToString();
                                                    string ma01 = "";
                                                    if (data != DBSettings.NotApplicableCharacter)
                                                    {
                                                        if (data == "")
                                                        {
                                                            maData.Add(new RowNdValue() { RowId = key, Value = ma01 });
                                                            continue;
                                                        }
                                                        string[] maSplit = data.Split(',');
                                                        int l = 0;
                                                        for (int s = 1; s <= maxCount; s++)
                                                        {
                                                            if (l == maSplit.Length)
                                                                break;
                                                            if (maSplit[l] == "")
                                                                l++;
                                                            if (l == maSplit.Length)
                                                                break;
                                                            if (maSplit[l] == s.ToString())
                                                            {
                                                                ma01 += "1";
                                                                l++;
                                                            }
                                                            else
                                                                ma01 += "0";
                                                        }
                                                    }
                                                    else
                                                        ma01 = DBSettings.NotApplicableCharacter;
                                                    maData.Add(new RowNdValue() { RowId = key, Value = ma01 });
                                                }

                                                StringBuilder dSql = new StringBuilder();
                                                StringBuilder ids = new StringBuilder();
                                                dSql.Append(" Update `" + DataImportSettings.DataImportSourceTempTable + "` set `" + columnImportSettings.ImportInformations[i].ColumnName + "`= CASE ROWID ");
                                                for (int r = 0; r < maData.Count; r++)
                                                {
                                                    if (maData[r].Value.Length < maxCount)
                                                    {
                                                        if (maData[r].Value != DBSettings.NotApplicableCharacter)
                                                        {
                                                            int diff = maxCount - maData[r].Value.Length;
                                                            for (int d = 0; d < diff; d++)
                                                                maData[r].Value = maData[r].Value + "0";
                                                        }
                                                    }
                                                    dSql.Append(" WHEN " + maData[r].RowId + " THEN '" + maData[r].Value + "' ");
                                                    if ((r + 1) == maData.Count)
                                                        ids.Append(maData[r].RowId);
                                                    else
                                                        ids.Append(maData[r].RowId + ",");
                                                }
                                                dSql.Append(" END WHERE ROWID IN (");
                                                ids.Append(")");
                                                dSql.Append(ids);
                                                mapSql.CommandText = dSql.ToString();
                                                mapSql.ExecuteNonQuery();
                                            }

                                            if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.FA)
                                            {
                                                string sSl = "DROP TABLE IF EXISTS tempdata;";
                                                mapSql.CommandText = sSl;
                                                mapSql.ExecuteNonQuery();
                                                sSl = "";
                                                sSl += " CREATE TEMP TABLE tempdata as Select CASE when( substr(" + sourceColumnName + ",1,1)=='''') THEN ";
                                                sSl += " ' ' || " + sourceColumnName;
                                                sSl += " when( substr(" + sourceColumnName + ",1,1)=='’') THEN ";
                                                sSl += " ' ' || " + sourceColumnName + " ELSE " + sourceColumnName;
                                                sSl += " END as data ";
                                                sSl += " From `" + DataImportSettings.DataImportTempAnswers + "` a ";
                                                sSl += " LEFT JOIN `" + DataImportSettings.DataImportSourceTempTable + "` t ON ";
                                                sSl += GetQueryWithKey(columnImportSettings, IsFile2Exist, "a", "t");
                                                mapSql.CommandText = sSl;
                                                mapSql.ExecuteNonQuery();

                                                sSql += " (SELECT data from tempdata WHERE a.ROWID = tempdata.ROWID);";
                                            }
                                            else
                                            {
                                                string sSl = "DROP TABLE IF EXISTS tempdata;";
                                                mapSql.CommandText = sSl;
                                                mapSql.ExecuteNonQuery();
                                                sSl = "";
                                                sSl += " CREATE TEMP TABLE tempdata as Select CASE WHEN(" + sourceColumnName + "=='" + columnImportSettings.NotApplicableCharacter.Replace("'", "''") + "') THEN ";
                                                sSl += " '" + DBSettings.NotApplicableCharacter + "' ELSE " + sourceColumnName + " END as data ";
                                                sSl += " From `" + DataImportSettings.DataImportTempAnswers + "` a ";
                                                sSl += " LEFT JOIN `" + DataImportSettings.DataImportSourceTempTable + "` t ON ";
                                                sSl += GetQueryWithKey(columnImportSettings, IsFile2Exist, "a", "t");
                                                mapSql.CommandText = sSl;
                                                mapSql.ExecuteNonQuery();

                                                sSql += " (SELECT data from tempdata WHERE a.ROWID = tempdata.ROWID);";
                                            }
                                            mapSql.CommandText = sSql;
                                            mapSql.ExecuteNonQuery();

                                            if (columnImportSettings.ImportInformations[i].AnswerType != AnswerType.FA)
                                            {
                                                sSql = " Update " + DataImportSettings.DataImportTempAnswers + " set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                sSql += " '" + DBSettings.NotApplicableCharacter + "' Where ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` LIKE '%" + DBSettings.NotApplicableCharacter + "%'  ";
                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();
                                            }

                                            if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.MA)
                                            {
                                                sSql = " Update " + DataImportSettings.DataImportTempAnswers + " set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                sSql += " (  ";
                                                sSql += " WITH reverse(i, c) AS (values(-1, '') UNION ALL SELECT i-1,   ";
                                                sSql += " substr(`" + columnImportSettings.ImportInformations[i].DBColumn + "`, i, 1)    ";
                                                sSql += " AS r FROM reverse WHERE r!='') ";
                                                sSql += " SELECT group_concat(c, '') AS reversed FROM reverse   ";
                                                sSql += " )  ";

                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();
                                            }

                                            if (columnImportSettings.IsDataProcessed)
                                            {
                                                sSql = " DROP TABLE IF EXISTS `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` ";
                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();

                                                sSql = " Create Temp Table `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` As  ";
                                                sSql += " Select A.* from `answers` A ";
                                                sSql += " Join `data_after_process` D On   A.`sort_no` = D.`sort_no`;";

                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();

                                                sSql = " Update `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` As a Set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";

                                                if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.FA)
                                                {
                                                    string sSl = "DROP TABLE IF EXISTS tempdata;";
                                                    sSl += " CREATE TEMP TABLE tempdata as Select " + sourceColumnName + " as data ";
                                                    sSl += " From `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` a ";
                                                    sSl += " LEFT JOIN `" + DataImportSettings.DataImportSourceTempTable + "` t ";
                                                    sSl += " ON ";
                                                    sSl += GetQueryWithKey(columnImportSettings, IsFile2Exist, "a", "t");
                                                    mapSql.CommandText = sSl;
                                                    mapSql.ExecuteNonQuery();

                                                    sSql += " (SELECT data from tempdata WHERE a.ROWID = tempdata.ROWID);";
                                                }
                                                else
                                                {
                                                    string sSl = "DROP TABLE IF EXISTS tempdata;";
                                                    sSl += " CREATE TEMP TABLE tempdata as Select CASE WHEN(" + sourceColumnName + "=='" + columnImportSettings.NotApplicableCharacter.Replace("'", "''") + "') THEN ";
                                                    sSl += " '" + DBSettings.NotApplicableCharacter + "' ELSE " + sourceColumnName + " END as data ";
                                                    sSl += " From `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` a ";
                                                    sSl += " LEFT JOIN `" + DataImportSettings.DataImportSourceTempTable + "` t ";
                                                    sSl += " ON ";
                                                    sSl += GetQueryWithKey(columnImportSettings, IsFile2Exist, "a", "t");
                                                    mapSql.CommandText = sSl;
                                                    mapSql.ExecuteNonQuery();

                                                    sSql += " (SELECT data from tempdata WHERE a.ROWID = tempdata.ROWID);";
                                                }
                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();

                                                if (columnImportSettings.ImportInformations[i].AnswerType != AnswerType.FA)
                                                {
                                                    sSql = " Update `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` set ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                    sSql += " '" + DBSettings.NotApplicableCharacter + "' Where ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` LIKE '%" + DBSettings.NotApplicableCharacter + "%'  ";
                                                    mapSql.CommandText = sSql;
                                                    mapSql.ExecuteNonQuery();
                                                }

                                                if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.MA)
                                                {
                                                    sSql = " Update " + DataImportSettings.DataImportTempDataAfterProcess1 + " set ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                    sSql += " (  ";
                                                    sSql += " WITH reverse(i, c) AS (values(-1, '') UNION ALL SELECT i-1,   ";
                                                    sSql += " substr(`" + columnImportSettings.ImportInformations[i].DBColumn + "`, i, 1)    ";
                                                    sSql += " AS r FROM reverse WHERE r!='') ";
                                                    sSql += " SELECT group_concat(c, '') AS reversed FROM reverse   ";
                                                    sSql += " )  ";

                                                    mapSql.CommandText = sSql;
                                                    mapSql.ExecuteNonQuery();
                                                }

                                                sSql = " Update `" + DataImportSettings.DataImportTempDataAfterProcess + "` as A set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";

                                                string sSl1 = "DROP TABLE IF EXISTS tempdata;";
                                                sSl1 += " CREATE TEMP TABLE tempdata as Select CASE when( substr(D." + columnImportSettings.ImportInformations[i].DBColumn + ",1,1)=='''' ) THEN ";
                                                sSl1 += " ' ' || D." + columnImportSettings.ImportInformations[i].DBColumn;
                                                sSl1 += " when( substr(D." + columnImportSettings.ImportInformations[i].DBColumn + ",1,1)=='’') THEN ";
                                                sSl1 += " ' ' || D." + columnImportSettings.ImportInformations[i].DBColumn + " ELSE D." + columnImportSettings.ImportInformations[i].DBColumn;
                                                sSl1 += " END as data ";
                                                sSl1 += " From `" + DataImportSettings.DataImportTempDataAfterProcess + "` A ";
                                                sSl1 += " LEFT JOIN `" + DataImportSettings.DataImportTempDataAfterProcess1 + "` D ON A.`sort_no` = D.`sort_no` ";
                                                mapSql.CommandText = sSl1;
                                                mapSql.ExecuteNonQuery();


                                                sSql += " (SELECT data from tempdata WHERE a.ROWID = tempdata.ROWID);";
                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();
                                            }
                                        }
                                    }

                                    sSql = " Delete from `Answers` ";
                                    mapSql.CommandText = sSql;
                                    mapSql.ExecuteNonQuery();

                                    sSql = " Insert Into `Answers` Select * From `" + DataImportSettings.DataImportTempAnswers + "`  ";
                                    mapSql.CommandText = sSql;
                                    mapSql.ExecuteNonQuery();

                                    sSql = " DROP TABLE IF EXISTS `" + DataImportSettings.DataImportTempAnswers + "` ";
                                    mapSql.CommandText = sSql;
                                    mapSql.ExecuteNonQuery();

                                    if (columnImportSettings.IsDataProcessed)
                                    {
                                        sSql = " Delete from `data_after_process` ";
                                        mapSql.CommandText = sSql;
                                        mapSql.ExecuteNonQuery();

                                        sSql = " Insert Into `data_after_process` Select * From `" + DataImportSettings.DataImportTempDataAfterProcess + "`  ";
                                        mapSql.CommandText = sSql;
                                        mapSql.ExecuteNonQuery();

                                        sSql = " DROP TABLE IF EXISTS `" + DataImportSettings.DataImportTempDataAfterProcess + "` ";
                                        mapSql.CommandText = sSql;
                                        mapSql.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i <= columnImportSettings.ImportInformations.Count - 1; i++)
                                    {
                                        sSql = " Update Answers As a Set ";
                                        sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` = ";

                                        string sourceColumnName = "TRIM(`" + columnImportSettings.ImportInformations[i].ColumnName + "`)";
                                        if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.MA && columnImportSettings.MAformat == MAFormat.FlagFormat)
                                        {
                                            int givenMa = columnImportSettings.ImportInformations[i].MAColumns.Count;
                                            int colMa = 0;
                                            int ColumnLimit = columnImportSettings.ImportInformations[i].MAColumns.Count;
                                            while (givenMa != 0)
                                            {
                                                sSql = " Update Answers As a Set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` = ";
                                                sSql += "CASE WHEN(`" + columnImportSettings.ImportInformations[i].DBColumn + "` is NULL) THEN '' ELSE `" + columnImportSettings.ImportInformations[i].DBColumn + "` END || ";
                                                if (givenMa > DBSettings.MaxNoOfColumnInsertInBulk)
                                                {
                                                    ColumnLimit = DBSettings.MaxNoOfColumnInsertInBulk - 1;
                                                    givenMa = givenMa - (DBSettings.MaxNoOfColumnInsertInBulk - 1);
                                                }
                                                else
                                                {
                                                    ColumnLimit = givenMa;
                                                    givenMa = 0;
                                                }
                                                sourceColumnName = "";
                                                for (int c = 0; c < ColumnLimit; c++)
                                                {
                                                    string MAColumn = columnImportSettings.ImportInformations[i].MAColumns[colMa];
                                                    colMa++;
                                                    if (sourceColumnName == "")
                                                    {
                                                        sourceColumnName = " case when(TRIM(`" + MAColumn + "`)='' or `" + MAColumn + "`=null) then  '0' else TRIM(`" + MAColumn + "`) end ";
                                                        continue;
                                                    }
                                                    sourceColumnName = sourceColumnName + "|| case when(TRIM(`" + MAColumn + "`)='' or `" + MAColumn + "`=null) then  '0' else TRIM(`" + MAColumn + "`) end ";
                                                }

                                                sSql += " (Select Replace(" + sourceColumnName + ",'" + columnImportSettings.NotApplicableCharacter.Replace("'", "''") + "'  ";
                                                sSql += " ,'" + DBSettings.NotApplicableCharacter + "') ";
                                                sSql += " From `" + DataImportSettings.DataImportSourceTempTable + "` t Where ";
                                                sSql += " t.RowId = a.RowId Limit 1)  ";
                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();
                                                if (columnImportSettings.IsDataProcessed)
                                                {
                                                    sSql = " Update data_after_process As a Set ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` = ";
                                                    sSql += "CASE WHEN(`" + columnImportSettings.ImportInformations[i].DBColumn + "` is NULL) THEN '' ELSE `" + columnImportSettings.ImportInformations[i].DBColumn + "` END || ";

                                                    sSql += " (Select Replace(" + sourceColumnName + ",'" + columnImportSettings.NotApplicableCharacter.Replace("'", "''") + "'  ";
                                                    sSql += " ,'" + DBSettings.NotApplicableCharacter + "') ";
                                                    sSql += " From `" + DataImportSettings.DataImportSourceTempTable + "` t Where ";
                                                    sSql += " t.RowId = a.RowId Limit 1)  ";
                                                    mapSql.CommandText = sSql;
                                                    mapSql.ExecuteNonQuery();
                                                }
                                                if (givenMa == 0)
                                                {
                                                    sSql = " Update Answers set ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                    sSql += " '" + DBSettings.NotApplicableCharacter + "' Where ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` LIKE '%" + DBSettings.NotApplicableCharacter + "%'  ";
                                                    mapSql.CommandText = sSql;
                                                    mapSql.ExecuteNonQuery();

                                                    sSql = " Update Answers set ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                    sSql += " (  ";
                                                    sSql += " WITH reverse(i, c) AS (values(-1, '') UNION ALL SELECT i-1,   ";
                                                    sSql += " substr(`" + columnImportSettings.ImportInformations[i].DBColumn + "`, i, 1)    ";
                                                    sSql += " AS r FROM reverse WHERE r!='') ";
                                                    sSql += " SELECT group_concat(c, '') AS reversed FROM reverse   ";
                                                    sSql += " )  ";

                                                    mapSql.CommandText = sSql;
                                                    mapSql.ExecuteNonQuery();

                                                    if (columnImportSettings.IsDataProcessed)
                                                    {

                                                        sSql = " Update data_after_process set ";
                                                        sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                        sSql += " '" + DBSettings.NotApplicableCharacter + "' Where ";
                                                        sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` LIKE '%" + DBSettings.NotApplicableCharacter + "%'  ";
                                                        mapSql.CommandText = sSql;
                                                        mapSql.ExecuteNonQuery();


                                                        sSql = " Update data_after_process set ";
                                                        sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                        sSql += " (  ";
                                                        sSql += " WITH reverse(i, c) AS (values(-1, '') UNION ALL SELECT i-1,   ";
                                                        sSql += " substr(`" + columnImportSettings.ImportInformations[i].DBColumn + "`, i, 1)    ";
                                                        sSql += " AS r FROM reverse WHERE r!='') ";
                                                        sSql += " SELECT group_concat(c, '') AS reversed FROM reverse   ";
                                                        sSql += " )  ";

                                                        mapSql.CommandText = sSql;
                                                        mapSql.ExecuteNonQuery();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.MA)
                                            {
                                                string tsql = "SELECT CASE WHEN(`" + columnImportSettings.ImportInformations[i].ColumnName + "` = '" + columnImportSettings.NotApplicableCharacter.Replace("'", "''") + "') THEN '" + DBSettings.NotApplicableCharacter + "' ELSE `" + columnImportSettings.ImportInformations[i].ColumnName + "` END as maRows FROM `" + DataImportSettings.DataImportSourceTempTable + "`";
                                                DataTable maTble = DBHelper.GetDataTable(tsql, dbConn);
                                                string mSql = DataImportHelper.GetMASplittingQueryMax(columnImportSettings.ImportInformations[i].ColumnName, DataImportSettings.DataImportSourceTempTable, columnImportSettings.NotApplicable, columnImportSettings.DataCount);
                                                DataTable dataTble = DBHelper.GetDataTable(mSql, dbConn);
                                                int maxCount = Convert.ToInt32(dataTble.Rows[0][0]);
                                                List<string> maData = new List<string>();

                                                for (int m = 0; m < maTble.Rows.Count; m++)
                                                {
                                                    string data = maTble.Rows[m][0].ToString();
                                                    string ma01 = "";
                                                    if (data != DBSettings.NotApplicableCharacter)
                                                    {
                                                        if (data == "")
                                                        {
                                                            maData.Add(ma01);
                                                            continue;
                                                        }
                                                        string[] maSplit = data.Split(',');
                                                        int l = 0;
                                                        for (int s = 1; s <= maxCount; s++)
                                                        {
                                                            if (l == maSplit.Length)
                                                                break;
                                                            if (maSplit[l] == "")
                                                                l++;
                                                            if (l == maSplit.Length)
                                                                break;
                                                            if (maSplit[l] == s.ToString())
                                                            {
                                                                ma01 += "1";
                                                                l++;
                                                            }
                                                            else
                                                                ma01 += "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ma01 = DBSettings.NotApplicableCharacter;
                                                    }

                                                    maData.Add(ma01);
                                                }

                                                string dSql = "";
                                                for (int r = 0; r < maData.Count; r++)
                                                {
                                                    if (maData[r].Length < maxCount)
                                                    {
                                                        if (maData[r] != DBSettings.NotApplicableCharacter)
                                                        {
                                                            int diff = maxCount - maData[r].Length;
                                                            for (int d = 0; d < diff; d++)
                                                                maData[r] = maData[r] + "0";
                                                        }
                                                    }
                                                    dSql += " Update `" + DataImportSettings.DataImportSourceTempTable + "` set `" + columnImportSettings.ImportInformations[i].ColumnName + "`= '" + maData[r] + "'  Where ROWID = " + (r + 1) + " ;";
                                                }
                                                mapSql.CommandText = dSql;
                                                mapSql.ExecuteNonQuery();
                                            }

                                            if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.FA)
                                            {
                                                sSql += " (Select CASE when( substr(" + sourceColumnName + ",1,1)=='''' ) THEN ";
                                                sSql += " ' ' || " + sourceColumnName;
                                                sSql += " when( substr(" + sourceColumnName + ",1,1)=='’') THEN ";
                                                sSql += " ' ' || " + sourceColumnName + " ELSE " + sourceColumnName;
                                                sSql += " END ";
                                                sSql += " From `" + DataImportSettings.DataImportSourceTempTable + "` t Where ";
                                                sSql += " t.RowId = a.RowId Limit 1)  ";
                                            }
                                            else
                                            {
                                                sSql += " (Select CASE WHEN(" + sourceColumnName + "=='" + columnImportSettings.NotApplicableCharacter.Replace("'", "''") + "') THEN  ";
                                                sSql += " '" + DBSettings.NotApplicableCharacter + "' ELSE " + sourceColumnName + " END ";
                                                sSql += " From `" + DataImportSettings.DataImportSourceTempTable + "` t Where ";
                                                sSql += " t.RowId = a.RowId Limit 1)  ";
                                            }
                                            mapSql.CommandText = sSql;
                                            mapSql.ExecuteNonQuery();

                                            if (columnImportSettings.ImportInformations[i].AnswerType != AnswerType.FA)
                                            {
                                                sSql = " Update Answers set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                sSql += " '" + DBSettings.NotApplicableCharacter + "' Where ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` = '" + DBSettings.NotApplicableCharacter + "'  ";
                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();
                                            }

                                            if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.MA)
                                            {
                                                sSql = " Update Answers set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                sSql += " (  ";
                                                sSql += " WITH reverse(i, c) AS (values(-1, '') UNION ALL SELECT i-1,   ";
                                                sSql += " substr(`" + columnImportSettings.ImportInformations[i].DBColumn + "`, i, 1)    ";
                                                sSql += " AS r FROM reverse WHERE r!='') ";
                                                sSql += " SELECT group_concat(c, '') AS reversed FROM reverse   ";
                                                sSql += " )  ";

                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();


                                            }

                                            if (columnImportSettings.IsDataProcessed)
                                            {
                                                sSql = " Update data_after_process As a Set ";
                                                sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` = ";

                                                if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.FA)
                                                {
                                                    sSql += " (Select CASE when( substr(" + sourceColumnName + ",1,1)=='''') THEN ";
                                                    sSql += " ' ' || " + sourceColumnName;
                                                    sSql += " when( substr(" + sourceColumnName + ",1,1)=='’') THEN ";
                                                    sSql += " ' ' || " + sourceColumnName + " ELSE " + sourceColumnName;
                                                    sSql += " END ";
                                                    sSql += " From `" + DataImportSettings.DataImportSourceTempTable + "` t Where ";
                                                    sSql += " t.RowId = a.RowId Limit 1)  ";
                                                }
                                                else
                                                {
                                                    sSql += " (Select CASE WHEN(" + sourceColumnName + "=='" + columnImportSettings.NotApplicableCharacter.Replace("'", "''") + "') THEN  ";
                                                    sSql += " '" + DBSettings.NotApplicableCharacter + "' ELSE " + sourceColumnName + " END ";
                                                    sSql += " From `" + DataImportSettings.DataImportSourceTempTable + "` t Where ";
                                                    sSql += " t.RowId = a.RowId Limit 1)  ";
                                                }

                                                mapSql.CommandText = sSql;
                                                mapSql.ExecuteNonQuery();

                                                if (columnImportSettings.ImportInformations[i].AnswerType != AnswerType.FA)
                                                {
                                                    sSql = " Update data_after_process set ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                    sSql += " '" + DBSettings.NotApplicableCharacter + "' Where ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "` = '" + DBSettings.NotApplicableCharacter + "'  ";
                                                    mapSql.CommandText = sSql;
                                                    mapSql.ExecuteNonQuery();
                                                }

                                                if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.MA)
                                                {
                                                    sSql = " Update data_after_process set ";
                                                    sSql += " `" + columnImportSettings.ImportInformations[i].DBColumn + "`=  ";
                                                    sSql += " (  ";
                                                    sSql += " WITH reverse(i, c) AS (values(-1, '') UNION ALL SELECT i-1,   ";
                                                    sSql += " substr(`" + columnImportSettings.ImportInformations[i].DBColumn + "`, i, 1)    ";
                                                    sSql += " AS r FROM reverse WHERE r!='') ";
                                                    sSql += " SELECT group_concat(c, '') AS reversed FROM reverse   ";
                                                    sSql += " )  ";

                                                    mapSql.CommandText = sSql;
                                                    mapSql.ExecuteNonQuery();

                                                }
                                            }
                                        }
                                    }
                                }
                                for (int i = 0; i <= columnImportSettings.ImportInformations.Count - 1; i++)
                                {
                                    if (columnImportSettings.ImportInformations[i].AnswerType == AnswerType.MA)
                                    {
                                        sSql = "UPDATE answers SET `" + columnImportSettings.ImportInformations[i].DBColumn + "`= CASE WHEN(`" + columnImportSettings.ImportInformations[i].DBColumn + "` != '*' ";
                                        sSql += "AND length(replace(`" + columnImportSettings.ImportInformations[i].DBColumn + "`,'1','')) = length(`" + columnImportSettings.ImportInformations[i].DBColumn + "`)) ";
                                        sSql += "THEN '' ELSE `" + columnImportSettings.ImportInformations[i].DBColumn + "` END;";
                                        mapSql.CommandText = sSql;
                                        mapSql.ExecuteNonQuery();
                                        if (columnImportSettings.IsDataProcessed)
                                        {
                                            sSql = "UPDATE data_after_process SET `" + columnImportSettings.ImportInformations[i].DBColumn + "`= CASE WHEN(`" + columnImportSettings.ImportInformations[i].DBColumn + "` != '*' ";
                                            sSql += "AND length(replace(`" + columnImportSettings.ImportInformations[i].DBColumn + "`,'1','')) = length(`" + columnImportSettings.ImportInformations[i].DBColumn + "`)) ";
                                            sSql += "THEN '' ELSE `" + columnImportSettings.ImportInformations[i].DBColumn + "` END;";
                                            mapSql.CommandText = sSql;
                                            mapSql.ExecuteNonQuery();
                                        }
                                    }
                                }
                                if (UpdateQuestionsInExcelSheet(workBook, columnImportSettings))
                                {
                                    tr.Commit();
                                    workBook.Save();
                                    Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(workBook);
                                    new QC4Common.Sheets.ListUpdate(workBook).UpdateListSheet(Util.Definiotion.VariableDictionary.Select(q => q.Value).ToList());
                                    QC4Common.Common.CommonFlag.SetIsDataUpdated(workBook, false);
                                    if (columnImportSettings.IsDataProcessed)
                                    {
                                        QC4Common.Common.CommonFlag.SetIsDataAfterUpdated(workBook, false);
                                    }
                                    var array = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Org) || (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Imp)).Select(q => q.Value).ToList();
                                    QC4Common.Util.ExcelUtil.GenerateNewDataSheet(workBook, array);
                                    List<QC4Common.Common.GTAutoSetting.VariableDT> colums = new List<QC4Common.Common.GTAutoSetting.VariableDT>();
                                    for (int cl = 0; cl < columnImportSettings.ImportInformations.Count; cl++)
                                    {
                                        QC4Common.Common.GTAutoSetting.VariableDT dat = new QC4Common.Common.GTAutoSetting.VariableDT();
                                        dat.Variable = columnImportSettings.ImportInformations[cl].VariableName;
                                        dat.Type = columnImportSettings.ImportInformations[cl].AnswerType;
                                        colums.Add(dat);
                                    }
                                    QC4Common.Common.GTAutoSetting.LoadNewDataToGTHiddenSheet(workBook, colums);
                                }
                                else
                                {
                                    tr.Rollback();
                                    throw new Exception("Failed to update to question settings sheet");
                                }

                            }
                            catch (Exception e)
                            {
                                _log.LogError(e.Message + "\n" + e.StackTrace);
                                tr.Rollback();
                                throw e;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
            finally
            {
                columnImportSettings.NotApplicableCharacter = notApplicableChar;
            }
        }

        private string GetQueryWithKey(ColumnImportSettings columnImportSettings, bool isFile2Exist, string dPrefix, string sPrefix)
        {
            string sSql = "";
            sSql += " " + dPrefix + ".`temprysfiltered` = " + sPrefix + ".`temprysfiltered` ";
            sSql += " and " + dPrefix + ".`temprysfiltered` != '' and " + dPrefix + ".`temprysfiltered` is not null ";

            if (isFile2Exist)
            {
                sSql += " And ";
                sSql += " " + dPrefix + ".`temprysfiltered1` = " + sPrefix + ".`temprysfiltered1` ";
                sSql += " and " + dPrefix + ".`temprysfiltered1` != '' and " + dPrefix + ".`temprysfiltered1` is not null ";
            }
            return sSql;
        }
        private string GetQueryWithKeyS(ColumnImportSettings columnImportSettings, bool isFile2Exist, string dPrefix, string sPrefix)
        {
            string sSql = "";
            sSql += " " + dPrefix + ".`temprysfiltered` = " + sPrefix + ".`temprysfiltered` ";
            sSql += " and " + sPrefix + ".`temprysfiltered` != '' and " + sPrefix + ".`temprysfiltered` is not null ";

            if (isFile2Exist)
            {
                sSql += " And ";
                sSql += " " + dPrefix + ".`temprysfiltered1` = " + sPrefix + ".`temprysfiltered1` ";
                sSql += " and " + sPrefix + ".`temprysfiltered1` != '' and " + sPrefix + ".`temprysfiltered1` is not null ";
            }
            return sSql;
        }

        private bool UpdateQuestionsInExcelSheet(Excel.Workbook workBook, ColumnImportSettings columnImportSettings)
        {
            try
            {

                Excel.Worksheet workSheet = ExcelUtil.GetWorkSheetByCodeName(workBook, SheetCodeName.QuestionSetting);
                object[,] excelDataArray = ConvertImportDataToQuestionArray(columnImportSettings);
                Excel.Range range = workSheet.Cells[QS.StartRow, QS.ColVariable];
                range = ExcelUtil.EndxlUp(range);
                range = range.Offset[1, -4];
                workSheet.Application.EnableEvents = false;
                range.Resize[excelDataArray.GetLength(0), excelDataArray.GetLength(1)].Value = excelDataArray;

                for (int i = 0; i <= excelDataArray.GetUpperBound(0); i++)
                {
                    Excel.Range nRange = range.Offset[i, 0];
                    QC4Common.Util.QSUtil.SetRowName(workSheet, nRange.EntireRow, columnImportSettings.ImportInformations[i].DBQuestionId);
                    QC4Common.Util.ExcelUtil.SetCellColor(workSheet, columnImportSettings.ImportInformations[i].AnswerType, nRange.Row);
                    if (columnImportSettings.ImportInformations[i].AnswerType == QC4Common.Common.Constants.AnswerType.MA || columnImportSettings.ImportInformations[i].AnswerType == QC4Common.Common.Constants.AnswerType.SA)
                    {
                        QC4Common.Util.ExcelUtil.SetChoiceCellsColor(workSheet, columnImportSettings.ImportInformations[i].ChoiceWordings.Count, nRange.Row);
                        QC4Common.Util.ExcelUtil.ResetChoiceCellsColor(workSheet, columnImportSettings.ImportInformations[i].ChoiceWordings.Count, nRange.Row);
                    }


                }

            
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        public bool UpdateAnswersInExcelSheet(Excel.Workbook workBook, SQLiteConnection dbConn, ColumnImportSettings columnImportSettings)
        {
            try
            {
                Excel.Worksheet workSheet = ExcelUtil.GetWorkSheetByCodeName(workBook, SheetCodeName.Data01);

                string[,] headerData = new string[1, columnImportSettings.ImportInformations.Count];
                for (int j = 0; j <= columnImportSettings.ImportInformations.Count - 1; j++)
                {
                    headerData[0, j] = columnImportSettings.ImportInformations[j].VariableName;
                }

                Excel.Range range = workSheet.Cells[DataSheet.ROW_HEADER, (columnImportSettings.ImportInformations[0].DBQuestionId + 1)];
                int newColumnStartIndex = range.Column;
                workSheet.Application.EnableEvents = false;
                range.Resize[headerData.GetLength(0), headerData.GetLength(1)].Value = headerData;

                string[,] excelDataArray = null;
                string fields = "";
                for (int i = 0; i <= columnImportSettings.ImportInformations.Count - 1; i++)
                {
                    fields += columnImportSettings.ImportInformations[i].DBColumn;
                    if ((i + 1) <= columnImportSettings.ImportInformations.Count - 1)
                    {
                        fields += ",";
                    }
                }
                int queryOffsetValue = 0;

                DrawQuery:
                string sSql = "Select " + fields + " From Answers Limit " + DBSettings.BulkDataInsertMaxRecordPerTrans + " Offset " + queryOffsetValue + " ";
                DataTable dt = DBHelper.GetDataTable(sSql, dbConn);
                if (dt.Rows.Count > 0)
                {
                    excelDataArray = new string[DBSettings.BulkDataInsertMaxRecordPerTrans, columnImportSettings.ImportInformations.Count];
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= columnImportSettings.ImportInformations.Count - 1; j++)
                        {
                            string columnValue = Convert.ToString(dt.Rows[i][j]);
                            if (columnImportSettings.ImportInformations[j].AnswerType == AnswerType.MA)
                            {
                                columnValue = ConvertToSheetMAAnswer(columnValue, DBSettings.NotApplicableCharacter);
                            }
                            excelDataArray[i, j] = columnValue;
                        }
                    }
                    if (dt.Rows.Count <= DBSettings.BulkDataInsertMaxRecordPerTrans)
                    {
                        range = workSheet.Cells[(DataSheet.ROW_HEADER + 1 + queryOffsetValue), newColumnStartIndex];
                        range.Resize[excelDataArray.GetLength(0), excelDataArray.GetLength(1)].Value = excelDataArray;

                        if (dt.Rows.Count == DBSettings.BulkDataInsertMaxRecordPerTrans)
                        {
                            excelDataArray = new string[DBSettings.BulkDataInsertMaxRecordPerTrans, columnImportSettings.ImportInformations.Count];
                            queryOffsetValue = queryOffsetValue + DBSettings.BulkDataInsertMaxRecordPerTrans;
                            goto DrawQuery;
                        }
                    }
                }

                workSheet.Application.EnableEvents = true;
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        public bool UpdateProcessedDataInExcelSheet(Excel.Workbook workBook, SQLiteConnection dbConn, ColumnImportSettings columnImportSettings)
        {
            try
            {
                Excel.Worksheet workSheet = ExcelUtil.GetWorkSheetByCodeName(workBook, SheetType.sh_DataAfterProcess);

                string[,] headerData = new string[1, columnImportSettings.ImportInformations.Count];
                for (int j = 0; j <= columnImportSettings.ImportInformations.Count - 1; j++)
                {
                    headerData[0, j] = columnImportSettings.ImportInformations[j].VariableName;
                }

                Excel.Range range = workSheet.Cells[DataSheet.ROW_HEADER, (columnImportSettings.ImportInformations[0].DBQuestionId + 1)];
                int newColumnStartIndex = range.Column;
                workSheet.Application.EnableEvents = false;
                range.Resize[headerData.GetLength(0), headerData.GetLength(1)].Value = headerData;

                string[,] excelDataArray = null;
                string fields = "";
                for (int i = 0; i <= columnImportSettings.ImportInformations.Count - 1; i++)
                {
                    fields += columnImportSettings.ImportInformations[i].DBColumn;
                    if ((i + 1) <= columnImportSettings.ImportInformations.Count - 1)
                    {
                        fields += ",";
                    }
                }
                int queryOffsetValue = 0;

                DrawQuery:
                string sSql = "Select " + fields + " From data_after_process Limit " + DBSettings.BulkDataInsertMaxRecordPerTrans + " Offset " + queryOffsetValue + " ";
                DataTable dt = DBHelper.GetDataTable(sSql, dbConn);
                if (dt.Rows.Count > 0)
                {
                    excelDataArray = new string[DBSettings.BulkDataInsertMaxRecordPerTrans, columnImportSettings.ImportInformations.Count];
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= columnImportSettings.ImportInformations.Count - 1; j++)
                        {
                            string columnValue = Convert.ToString(dt.Rows[i][j]);
                            if (columnImportSettings.ImportInformations[j].AnswerType == AnswerType.MA)
                            {
                                columnValue = ConvertToSheetMAAnswer(columnValue, DBSettings.NotApplicableCharacter);
                            }
                            excelDataArray[i, j] = columnValue;
                        }
                    }
                    if (dt.Rows.Count <= DBSettings.BulkDataInsertMaxRecordPerTrans)
                    {
                        range = workSheet.Cells[(DataSheet.ROW_HEADER + 1 + queryOffsetValue), newColumnStartIndex];
                        range.Resize[excelDataArray.GetLength(0), excelDataArray.GetLength(1)].Value = excelDataArray;

                        if (dt.Rows.Count == DBSettings.BulkDataInsertMaxRecordPerTrans)
                        {
                            excelDataArray = new string[DBSettings.BulkDataInsertMaxRecordPerTrans, columnImportSettings.ImportInformations.Count];
                            queryOffsetValue = queryOffsetValue + DBSettings.BulkDataInsertMaxRecordPerTrans;
                            goto DrawQuery;
                        }
                    }
                }

                workSheet.Application.EnableEvents = true;
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                return false;
            }

        }

        private string ConvertToSheetMAAnswer(string DbValue, string notApplicableChar)
        {
            string sheetMAAnswer = "";
            if (DbValue == notApplicableChar)
            {
                sheetMAAnswer = "*";
            }
            else
            {
                char[] answerArray = DbValue.ToCharArray();
                if (answerArray.Length > 0) sheetMAAnswer = ",";
                for (int i = 0; i <= answerArray.Length - 1; i++)
                {
                    if (answerArray[i].ToString() == "1")
                    {
                        if (sheetMAAnswer.Length > 1) sheetMAAnswer += ",";
                        sheetMAAnswer += (i + 1).ToString();
                    }
                }
                if (sheetMAAnswer.Length > 1) sheetMAAnswer += ",";
            }
            return sheetMAAnswer;
        }

        private string[,] ConvertImportDataToQuestionArray(ColumnImportSettings columnImportSettings)
        {
            string[,] questionData = new string[columnImportSettings.ImportInformations.Count, 1038];
            int rowIndex = 0;
            foreach (ImportInformation importInformation in columnImportSettings.ImportInformations)
            {
                string noOfChoices = null;
                if (importInformation.AnswerType != AnswerType.FA && importInformation.AnswerType != AnswerType.N)
                {
                    noOfChoices = importInformation.NoOfChoices.ToString();
                }

                questionData[rowIndex, 0] = "Imp"; 
                questionData[rowIndex, 4] = importInformation.VariableName;
                questionData[rowIndex, 5] = importInformation.AnswerType;
                questionData[rowIndex, 6] = noOfChoices;
                questionData[rowIndex, 9] = importInformation.QuestionTitle;
                questionData[rowIndex, 10] = importInformation.QuestionSentence;

                int columnIndex = 11;
                for (int i = 0; i <= importInformation.ChoiceWordings.Count - 1; i++)
                {
                    questionData[rowIndex, columnIndex] = importInformation.ChoiceWordings[i].WordingText;
                    columnIndex++;
                }
                rowIndex++;
            }
            return questionData;
        }

    }
}
