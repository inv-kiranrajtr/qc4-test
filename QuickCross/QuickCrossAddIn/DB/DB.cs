//using ExcelAddIn.Common;
//using System;
//using System.Collections.Generic;
//using System.Data.SQLite;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ExcelAddIn.DB
//{
//	class DB
//	{
//		private SQLiteConnection _DBConnection = null;
//		private static System.Threading.Mutex _mutex_DB = new System.Threading.Mutex();
//		private String DBPath = System.IO.Path.Combine(@"D:\Projects\Quick cross\DB\database.sqlite3");

//		public SQLiteConnection Connection
//		{
//			get { return _DBConnection; }
//		}


//		public Boolean Connect()
//		{
//			try
//			{
//				if (_DBConnection == null)
//				{
//					_DBConnection = new SQLiteConnection("Data Source=" + DBPath + ";DateTimeFormat=ISO8601;JournalMode=Persist");
//					_DBConnection.Open();
//				};
//				return true;
//			}
//			catch (Exception ex)
//			{
//				Console.WriteLine(ex.Message + "\r\n\r\n" + ex.StackTrace);
//				return false;
//			};
//		}

//		public Boolean Disconnect()
//		{
//			try
//			{
//				if (_DBConnection == null)
//				{
//					return false;
//				}
//				else
//				{
//					_DBConnection.Close();
//					_DBConnection.Dispose();
//					_DBConnection = null;
//					return true;
//				};
//			}
//			catch (Exception ex)
//			{
//				Console.WriteLine(ex.Message + "\r\n\r\n" + ex.StackTrace);
//				return false;
//			};
//		}

//		public Boolean CreateAllTables()
//		{
//			try
//			{
//				//SET @OLD_UNIQUE_CHECKS =@@UNIQUE_CHECKS, UNIQUE_CHECKS = 0;
//				//SET @OLD_FOREIGN_KEY_CHECKS =@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS = 0;
//				//SET @OLD_SQL_MODE =@@SQL_MODE, SQL_MODE = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';
//				if (_DBConnection == null) return false;

//				using (SQLiteCommand command = _DBConnection.CreateCommand())
//				{
//					//StringBuilder sql = new StringBuilder();
//					//sql.Append("CREATE SCHEMA IF NOT EXISTS `QC4` DEFAULT CHARACTER SET utf8;");
//					//command.CommandText = sql.ToString();
//					//command.ExecuteNonQuery();

//					StringBuilder sql = new StringBuilder();
//					sql.Append("CREATE TABLE IF NOT EXISTS `answer_fa`(");
//					sql.Append("`sample_id` INT NOT NULL,");
//					sql.Append(" `sort_no` INT NULL,");
//					sql.Append("`fa_1` TEXT NULL,");
//					sql.Append("`fa_2` TEXT NULL,");
//					sql.Append(" PRIMARY KEY(`sample_id`))");
//					command.CommandText = sql.ToString();
//					command.ExecuteNonQuery();
					
//					sql = new StringBuilder();
//					sql.Append("CREATE TABLE IF NOT EXISTS `answer` (");
//					sql.Append("`sample_id` INT NOT NULL,");
//					sql.Append("`sort_no` INT NULL,");
//					sql.Append("`answer_date` TIMESTAMP NULL,");
//					sql.Append("`q_1` INT NULL,");
//					sql.Append("`q_2` VARCHAR(2000) NULL,");
//					sql.Append("PRIMARY KEY(`sample_id`))");
//					command.CommandText = sql.ToString();
//					command.ExecuteNonQuery();

//					sql = new StringBuilder();
//					sql.Append("CREATE TABLE IF NOT EXISTS `weight_back` (");
//					sql.Append("`id` INT NOT NULL,");
//					sql.Append("`name` INT NULL,");
//					sql.Append("`weight_back_table` VARCHAR(255) NULL,");
//					sql.Append("`q_1` INT NULL,");
//					sql.Append("`q_2` VARCHAR(2000) NULL,");
//					sql.Append("PRIMARY KEY (`id`))");
//					command.CommandText = sql.ToString();
//					command.ExecuteNonQuery();

//					//sql = new StringBuilder();
//					//sql.Append("CREATE TABLE IF NOT EXISTS `weight_back_value_ < id >` (");
//					//sql.Append("`sample_id` INT NOT NULL,");
//					//sql.Append("`value` INT NULL,");
//					//sql.Append("PRIMARY KEY(`sample_id`))");
//					//command.CommandText = sql.ToString();
//					//command.ExecuteNonQuery();

//					sql = new StringBuilder();
//					sql.Append("CREATE TABLE IF NOT EXISTS `question` (");
//					sql.Append("`variable_name` VARCHAR(255) NOT NULL,");
//					sql.Append("`new` TINYINT NULL,");
//					sql.Append("`question_number` VARCHAR(255) NULL,");
//					sql.Append("`question_type` INT NULL,");
//					sql.Append("`number_of_question` INT NULL,");
//					sql.Append("`answer_type` INT NULL,");
//					sql.Append("`no_of_categories` INT NULL,");
//					sql.Append("`score` VARCHAR(45) NULL,");
//					sql.Append("`sort` INT NULL,");
//					sql.Append("`column` VARCHAR(255) NULL,");
//					sql.Append("`table_heading` VARCHAR(2000) NULL,");
//					sql.Append("`question_sentance` VARCHAR(2000) NULL,");
//					sql.Append("`add_sub_totals` INT NULL,");
//					sql.Append("`count` VARCHAR(255) NULL,");
//					sql.Append("`count_base` INT NULL,");
//					sql.Append("PRIMARY KEY (`variable_name`))");
//					command.CommandText = sql.ToString();
//					command.ExecuteNonQuery();

//					sql = new StringBuilder();
//					sql.Append("CREATE TABLE IF NOT EXISTS `question_sub_total` (");
//					sql.Append("`variable_name` VARCHAR(255) NOT NULL,");
//					sql.Append("`name` VARCHAR(2000) NOT NULL,");
//					sql.Append("`criteria` VARCHAR(255) NULL,");
//					sql.Append("PRIMARY KEY(`variable_name`, `name`),");
//					sql.Append("CONSTRAINT `fk_question_sub_total_question` ");
//					sql.Append("FOREIGN KEY(`variable_name`) ");
//					sql.Append("REFERENCES `question` (`variable_name`) ");
//					sql.Append("ON DELETE NO ACTION ");
//					sql.Append("ON UPDATE NO ACTION); ");
//					sql.Append("CREATE INDEX `fk_question_sub_total_question` ON `question_sub_total` (`variable_name` ASC);");
//					command.CommandText = sql.ToString();
//					command.ExecuteNonQuery();

//					sql = new StringBuilder();
//					sql.Append("CREATE TABLE IF NOT EXISTS `settings` (");
//					sql.Append("`key` INT NOT NULL,");
//					sql.Append("`value` INT NULL,");
//					sql.Append("`created_at` DATETIME NULL,");
//					sql.Append("`updated_at` DATETIME NULL,");
//					sql.Append("PRIMARY KEY (`key`))");
//					command.CommandText = sql.ToString();
//					command.ExecuteNonQuery();

//					sql = new StringBuilder();
//					sql.Append("CREATE TABLE IF NOT EXISTS `question_choice` (");
//					sql.Append("`variable_name` VARCHAR(255) NOT NULL,");
//					sql.Append("`ch_text` VARCHAR(2000) NOT NULL,");
//					sql.Append("`ch_id` INT NULL,");
//					sql.Append("PRIMARY KEY(`variable_name`, `ch_text`),");
//					sql.Append("CONSTRAINT `fk_question_sub_total_question0` ");
//					sql.Append("FOREIGN KEY(`variable_name`) ");
//					sql.Append("REFERENCES `question` (`variable_name`) ");
//					sql.Append("ON DELETE NO ACTION ");
//					sql.Append("ON UPDATE NO ACTION); ");
//					sql.Append("CREATE INDEX `fk_question_sub_total_question0` ON `question_choice` (`variable_name` ASC);");
//					command.CommandText = sql.ToString();
//					command.ExecuteNonQuery();
//				};

//				return true;
//			}
//			catch (Exception ex)
//			{
//				Console.WriteLine(ex.Message + "\r\n\r\n" + ex.StackTrace);
//				return false;
//			}
//		}

//		//public Boolean InsertLocation(Location Location)
//		//{
//		//	try
//		//	{
//		//		if (_DBConnection == null) return false;
//		//		_mutex_DB.WaitOne();
//		//		using (SQLiteCommand command = _DBConnection.CreateCommand())
//		//		{
//		//			command.CommandText = "INSERT INTO Location (CustomerID, LocationID, Name, OpenTime, CloseTime) VALUES (@CustomerID, @LocationID, @Name, @OpenTime, @CloseTime);";
//		//			command.Parameters.AddWithValue("@CustomerID", Location.CustomerID);
//		//			command.Parameters.AddWithValue("@LocationID", Location.LocationID);
//		//			command.Parameters.AddWithValue("@Name", Location.Name);
//		//			command.Parameters.AddWithValue("@OpenTime", Location.OpenTime);
//		//			command.Parameters.AddWithValue("@CloseTime", Location.CloseTime);
//		//			command.ExecuteNonQuery();
//		//		};
//		//		_mutex_DB.ReleaseMutex();
//		//		return true;
//		//	}
//		//	catch (Exception ex)
//		//	{
//		//		String msg = ex.Message + "\r\n\r\n" + ex.StackTrace;
//		//		Console.WriteLine(msg);
//		//		_mutex_DB.ReleaseMutex();
//		//		return false;
//		//	}
//		//}
//	}
//}
