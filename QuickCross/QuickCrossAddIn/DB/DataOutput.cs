using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelAddIn.Sheets;
using ExcelAddIn.Model;
using QC4Common.Model;

namespace ExcelAddIn.DB
{
	class DataOutput
	{
		private string ConnectionString {get;set;}

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

		internal Dictionary<string, QuestionSettings> GetVariableMappingDict(List<QuestionSettings> variableList = null)
		{
			DataTable dt = GetVariableMapping(variableList);
			Dictionary<string, QuestionSettings> dict = new Dictionary<string, QuestionSettings>();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				dict.Add(dt.Rows[i][1].ToString(), new QuestionSettings(Convert.ToInt32(dt.Rows[i][0]), dt.Rows[i][1].ToString()));//dt.Rows[i][2].ToString(), Convert.ToInt32(dt.Rows[i][3])));
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
				Console.Write(ex.Message);
				return null;
			}
		}

		//internal DataTable GetData(List<Question> questions,List<Condition> conditions = null)
		//{
		//	StringBuilder sql = new StringBuilder();
		//	DataTable dt;
		//	sql.Append("SELECT sample_id");

		//	foreach (Question qs in questions)
		//	{
		//		sql.Append(", q_" + qs.Id);
		//	}
		//	sql.Append(" FROM answers");
		//	if (null != conditions && conditions.Count > 0)
		//	{
		//		sql.Append(" WHERE ");
		//		foreach (Condition cd in conditions)
		//		{
		//			sql.Append(cd.AndOr);
		//			string prefix = " ( ";
		//			string s = " q_" + cd.Id + " " + cd.Operator + " '" ;
		//			foreach (var value in cd.Values)
		//			{
		//				sql.Append(prefix  + s + value + "'");
		//				prefix = " OR ";
		//			}
		//			sql.Append(" ) ");
		//		}
		//	}
				
		//	using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
		//	{
		//		connection.Open();
		//		dt = DBHelper.GetDataTable(sql.ToString(), connection);
		//	}
		//	return dt;
		//}
	}
}
