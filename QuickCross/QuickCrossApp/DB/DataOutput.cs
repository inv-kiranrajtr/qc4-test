using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Qc4Launcher.Model;
using ExcelAddIn.Sheets;
using Qc4Launcher.Classes;
using QC4Common.Model;
using Macromill.QCWeb.Question;
using log4net;
using System.Reflection;
using Qc4Launcher.Util;
using Macromill.QCWeb.Tabulation;

namespace Qc4Launcher.DB
{
	class DataOutput
	{
		private string ConnectionString {get;set; }
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal DataOutput(Excel.Workbook source)
		{
			ConnectionString = DBHelper.GetConnectionString(source);
		}

		internal List<Question> GetVariableMappingList(List<QuestionSettings> variableList = null)
		{
			DataTable dt = GetVariableMapping(variableList);
			List<Question> list = new List<Question>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				list.Add(new Question(Convert.ToInt32(dt.Rows[i][0]), dt.Rows[i][1].ToString(), "", 0));//dt.Rows[i][2].ToString(), Convert.ToInt32(dt.Rows[i][3])));
			}
			return list;
		}

		internal Dictionary<string, Question> GetVariableMappingDictionary(List<QuestionSettings> variableList = null)
		{
			DataTable dt = GetVariableMapping(variableList);
			Dictionary<string,Question> dict = new Dictionary<string, Question>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				dict.Add(dt.Rows[i][1].ToString(), new Question(Convert.ToInt32(dt.Rows[i][0]), dt.Rows[i][1].ToString(), "", 0));//dt.Rows[i][2].ToString(), Convert.ToInt32(dt.Rows[i][3])));
			}
			return dict;
		}

		private DataTable GetVariableMapping(List<QuestionSettings> variableList)
		{
			try
			{
				int i = 1;
				DataTable dt = new DataTable();
				using (SQLiteConnection connection = DBHelper.GetConnection(ConnectionString))
				{
					connection.Open();
					string sql = "SELECT id,variable FROM question ";//answer_type,category_count FROM question ";
					if (null != variableList && variableList.Count > 0)
					{
						sql += "WHERE ";
						string prefix = "";
						for (i = 0; i < variableList.Count; i++)
						{
							sql += prefix + "variable = @id" + (i + 1);
							prefix = " OR ";
						}

						i = 1;
						using (SQLiteCommand command = connection.CreateCommand())
						{
							command.CommandText = sql;
							foreach (QuestionSettings qs in variableList)
							{
								command.Parameters.Add(new SQLiteParameter("@id" + (i++), qs.Variable));
							}
							using (SQLiteDataReader reader = command.ExecuteReader())
							{
								dt.Load(reader);
							}
						}
					}
					else
					{
						dt = DBHelper.GetDataTable(sql, connection);
					}
					return dt;
				}

			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
				return null;
			}
		}

		internal DataTable GetData(List<Question> questions,List<Condition> conditions = null)
		{
			StringBuilder sql = new StringBuilder();
			DataTable dt;
			sql.Append("SELECT sample_id");

			foreach (Question qs in questions)
			{
				sql.Append(", q_" + qs.Id);
			}
			sql.Append(" FROM answers");
			if (null != conditions && conditions.Count > 0)
			{
				sql.Append(" WHERE ");
				foreach (Condition cd in conditions)
				{
					sql.Append(cd.AndOr);
					string prefix = " ( ";
					string s = " q_" + cd.Id + " " + cd.Operator + " '" ;
					foreach (var value in cd.Values)
					{
						sql.Append(prefix  + s + value + "'");
						prefix = " OR ";
					}
					sql.Append(" ) ");
				}
			}
				
			using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();
				dt = DBHelper.GetDataTable(sql.ToString(), connection);
			}
			return dt;
		}

        internal static string[] SpssNCount(decimal[] questionIds, Questions questions,Excel.Workbook workbook, QCAnswerType type,bool unicode, bool[] filteringFlag, string table)
        {
            int qsCount = questionIds.Count();
            string[] maxVal = new string[qsCount];
            string antbl = "";
            string mainTable = table;
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(DBHelper.GetConnectionString(workbook)))
                {
                    con.Open();
                    for (int i = 0; i < qsCount; i++)
                    {
                        antbl = "";
                        Questions.Question question = questions[questionIds[i]] as Questions.Question;
                        if (Definiotion.VariableDictionary[question.Name].QuestionFlag == "An")
                            antbl = "multivariate";
                        if (question.QCAnswerType == type)
                        {
                            if (type == QCAnswerType.N)
                            {
                                if (questionIds[i] == 0)
                                {
                                    maxVal[i] = SetMaxVal("sample_id", con, type, unicode, filteringFlag, mainTable, antbl);
                                }
                                else
                                {
                                    maxVal[i] = SetMaxVal(questionIds[i].ToString(), con, type, unicode, filteringFlag, mainTable, antbl);
                                }
                            }
                            else if (type == QCAnswerType.FA)
                            {

                                if (questionIds[i] == 0)
                                {
                                    maxVal[i] = SetMaxVal("sample_id", con, type, unicode, filteringFlag, mainTable, antbl);
                                }
                                else
                                {
                                    maxVal[i] = SetMaxVal(questionIds[i].ToString(), con, type, unicode, filteringFlag, mainTable, antbl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
				_log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
			}
			return maxVal;
        }
        internal static int Rowcount(Excel.Workbook workbook)
        {
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Data01);
            int rcell = SettingSheet.UsedRange.Rows.Count;
            return rcell;
        }
        internal static int RowCount(Excel.Workbook workbook)
        {
            int maxVal = 0;
            using (SQLiteConnection con = new SQLiteConnection(DBHelper.GetConnectionString(workbook)))
            {
                con.Open();
                string sql = "SELECT COUNT(sample_id) FROM answers";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                maxVal = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return maxVal;
        }

        private static string SetMaxVal(string questionIds, SQLiteConnection con, QCAnswerType type,bool unicode,bool[] filteringFlag, string mainTable, string anTable)
        {
            int maxVal1 = 0;
            int maxVal2 = 0;
            string maxVal = "";
            int pointNumlen = 0;
            string sql = "";
            string ouput = "";
            string columnName = questionIds == "sample_id" ? "sample_id" : ("q_" + questionIds);
            switch (type)
            {
                case QCAnswerType.N:
                    if (anTable != "")
                        sql = "SELECT " + columnName + " FROM multivariate m join " + mainTable + " a on m.sort_no = a.sort_no";
                    else
                        sql = "SELECT " + columnName + " FROM " + mainTable;
                    DataTable dt = new DataTable();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                        int len = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (filteringFlag != null && !filteringFlag[i])
                                    continue;
                                if (dt.Rows[i][0] != null && dt.Rows[i][0].ToString().Length > 0)
                                {
                                    try
                                    {
                                        string number = dt.Rows[i][0].ToString().ToLower();
                                        ouput = "";
                                        if (number.Contains('e'))
                                        {
                                            if (number.Contains("e-"))
                                            {
                                                bool isHaveMinuz = number[0] == '-';
                                                if (number.Contains("-"))
                                                    number = number.Replace("-", "");
                                                string[] str = number.Split('e');
                                                int count = Convert.ToInt32(str[1]);
                                                ouput = str[0];
                                                for (int j = 0; j < count; j++)
                                                {
                                                    if (ouput.Contains('.'))
                                                    {
                                                        string[] splt = ouput.Split('.');
                                                        if (splt[0] == "0")
                                                        {
                                                            ouput = ouput.Replace("0.", "0.0");
                                                        }
                                                        else
                                                        {
                                                            if (splt[0].Length > 1)
                                                                ouput = splt[0].Substring(0, splt[0].Length - 1) + "." + splt[0].Substring(splt[0].Length - 1, 1) + splt[1];
                                                            else
                                                                ouput = "0." + splt[0] + splt[1];
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (ouput.Length > 1)
                                                            ouput = ouput[0] + "." + ouput.Substring(1, ouput.Length - 1);
                                                        else
                                                            ouput = "0." + ouput;
                                                    }
                                                }
                                                ouput = isHaveMinuz ? "-" + ouput : ouput;
                                            }
                                            else
                                            {

                                                bool isHaveMinuz = number[0] == '-';
                                                if (number.Contains("-"))
                                                    number = number.Replace("-", "");

                                                string[] str = number.Split('e');
                                                if (str[1].Contains('+'))
                                                    str[1] = str[1].Replace("+", "");
                                                int count = Convert.ToInt32(str[1]);
                                                ouput = str[0];
                                                for (int j = 0; j < count; j++)
                                                {
                                                    if (ouput.Contains('.'))
                                                    {
                                                        string[] splt = ouput.Split('.');
                                                        if (splt[0] == "")
                                                            ouput = splt[1];
                                                        else if (splt[0] == "0")
                                                            ouput = splt[1];
                                                        else if (splt[1].Length > 1)
                                                            ouput = splt[0] + splt[1][0] + "." + splt[1].Substring(1, splt[1].Length - 1);
                                                        else
                                                            ouput = splt[0] + splt[1];
                                                    }
                                                    else
                                                    {
                                                        ouput += "0";
                                                    }
                                                }
                                                ouput = isHaveMinuz ? "-" + ouput : ouput;
                                            }
                                        }
                                        else
                                        {
                                            ouput = dt.Rows[i][0].ToString();
                                        }
                                        if (ouput.Contains('.'))
                                        {
                                            int plen = ouput.Split('.')[1].Length;
                                            int numlen = ouput.Split('.')[0].Length;
                                            if (len < numlen)
                                                len = numlen;
                                            if (pointNumlen < plen)
                                                pointNumlen = plen;
                                        }
                                        else
                                        {
                                            int numlen = ouput.Length;
                                            if (len < numlen)
                                                len = numlen;
                                        }
                                    }
                                    catch { return "F40.40"; }
                                }
                            }
                        }
                        maxVal1 = len;
                        maxVal2 = pointNumlen;
                        maxVal2 = maxVal2 > 16 ? 16 : maxVal2;
                        maxVal1 = (maxVal1 + maxVal2) > 40 ? 40 : (maxVal1 + maxVal2);
                        maxVal1 = maxVal1 == 0 ? 1 : maxVal1;
                        maxVal = "F" + maxVal1.ToString() + "." + maxVal2.ToString();
                    }
                    break;
                case QCAnswerType.FA:
                    if (anTable != "")
                        sql = "SELECT " + columnName + " FROM multivariate m join " + mainTable + " a on m.sort_no = a.sort_no";
                    else
                        sql = "SELECT " + columnName + " FROM "+ mainTable;
                    dt = new DataTable();
                    int codepage = LocalResource.Culture.TextInfo.ANSICodePage;//System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage;
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                        int len = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string fa = dt.Rows[i][0].ToString();
                                if (fa == "" || (filteringFlag != null && !filteringFlag[i]))
                                    continue;

                                int leng = 0;
                                if (unicode)
                                {
                                    var bytes = Encoding.UTF8.GetBytes(fa);
                                    leng = bytes.Count();
                                }
                                else
                                {
                                    fa = fa.Replace("\n　", " ").Replace("\r", "").Replace("\t", "");
                                    byte[] convertedBytes = Encoding.GetEncoding(codepage).GetBytes(fa);
                                    string convertedAsciiString = System.Text.Encoding.ASCII.GetString(convertedBytes);
                                    leng = convertedAsciiString.Length;
                                }
                                if (len < leng)
                                    len = leng;
                            }
                            if (len > 32767)
                                len = 32767;
                            maxVal = "A" + len.ToString();
                        }
                    }
                    break;
            }
            return maxVal;
        }
    }
}
