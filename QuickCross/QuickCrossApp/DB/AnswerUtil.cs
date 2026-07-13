using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Qc4Launcher.Util;

namespace Qc4Launcher.DB
{
	class AnswerUtil
	{
		private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static DataTable GetData(int columnName,string conString)
		{
			DataTable dataTable = null;
			try
			{
				using (SQLiteConnection dbSource = DB.DBHelper.GetConnection(conString))
				{
					string sql = "SELECT " + columnName + " FROM answers";
					dbSource.Open();
					dataTable = DB.DBHelper.GetDataTable(sql, dbSource);
				}
				
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
			}
			return dataTable;
		}
	}
}
