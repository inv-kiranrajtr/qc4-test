using ExcelAddIn.Common;
using ExcelAddIn.Model;
using QcWebCommon.Sheets;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
//using System.Data;
//using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QcWebCommon.Common.Constants;
using Constants = QcWebCommon.Common.Constants;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelAddIn.DB
{
	public class DBHelper
	{
        
        public static string GetConnectionString(string filePath)
        {
			if (filePath.Contains(";"))
			{
				filePath = "\"" + filePath + "\"";
			}
			char a = (char)14;
			string Password = "MacroMill" + a + "!3";
            return @"Data Source=" + filePath + "; Version=3;Password=" + Password + ";";
        }

        public static SQLiteConnection GetConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }
        
        public static DataTable GetDataTable(string sql, SQLiteConnection cnn)
        {
            DataTable dt = new DataTable();
            
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
                {
                    mycommand.CommandText = sql;
                    SQLiteDataReader reader = mycommand.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }
        public static string GetAnswertype(decimal itemId, SQLiteConnection cnn)
        {
            string answertype = string.Empty;
            try
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(cnn))
                {
                    mycommand.CommandText = "SELECT answer_type from question where id ="+ itemId.ToString();
                    SQLiteDataReader reader = mycommand.ExecuteReader();
                    if (reader.Read())
                    {
                        answertype = reader.GetString(0);
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return answertype;
        }
        public static bool checkAfterProcess(string constring)
        {
            int cnt = 0;
            using (SQLiteConnection con = GetConnection(constring))
            {
                con.Open();
                string sql = "SELECT count(name) FROM sqlite_master WHERE type='table' AND name='data_after_process'";
                cnt = ExecuteScalar(sql, con);
            }
            return cnt > 0;
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


        //      public static void UpdateVariable(Dictionary<int, string> updateList)
        //{
        //	try
        //	{
        //		//using (dbConnection = GetConnection())
        //		//{
        //		//	dbConnection.Open();

        //		//	using (SQLiteTransaction tr = dbConnection.BeginTransaction())
        //		//	{
        //		//		using (SQLiteCommand cmd = dbConnection.CreateCommand())
        //		//		{
        //		//			cmd.Transaction = tr;
        //					foreach (int index in updateList.Keys)
        //					{
        //						string var = Definitions.RowVariableList[index];
        //						string key = Definitions.VariableDictionary[var].VariableBefore;
        //						Definitions.VariableDictionary[var].VariableBefore = var;
        //						Definitions.FlagFromQs = true;
        //						//cmd.CommandText = "UPDATE question SET variable_name = '" + var + "'  WHERE variable_name = '" + key + "' ;";
        //						//cmd.ExecuteNonQuery();
        //		}
        //		//		}
        //		//		tr.Commit();
        //		//	}
        //		//}
        //	}
        //	catch (Exception ex)
        //	{
        //		Console.WriteLine(ex.Message);
        //		//dbConnection.Close();
        //	}
        //}



        //public static void UpdateAnswerTypes(List<QcWebCommon.Sheets.QuestionSettings> updateList)
        //{
        //	try
        //	{
        //		//using (dbConnection = GetConnection())
        //		//{
        //		//	dbConnection.Open();

        //		//	using (SQLiteTransaction tr = dbConnection.BeginTransaction())
        //		//	{
        //		//		using (SQLiteCommand cmd = dbConnection.CreateCommand())
        //		//		{
        //		//			cmd.Transaction = tr;
        //					foreach (QcWebCommon.Sheets.QuestionSettings qs in updateList)
        //					{
        //						Definitions.VariableDictionary[qs.Variable].AnswerTypeBefore = qs.AnswerType;
        //						Definitions.FlagFromQs = true;
        //						//cmd.CommandText = "UPDATE question SET answer_type = " + qs.AnswerType + "  WHERE variable_name = '" + qs.Variable + "' ;";
        //						//cmd.ExecuteNonQuery();
        //		}
        //		//		}
        //		//		tr.Commit();
        //		//	}
        //		//}
        //	}
        //	catch (Exception ex)
        //	{
        //		Console.WriteLine(ex.Message);
        //	//	dbConnection.Close();
        //	}
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

        
    }
}