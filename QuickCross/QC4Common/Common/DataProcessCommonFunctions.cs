using log4net;
using QC4Common.DB;
using QC4Common.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace QC4Common.Common
{
    public class DataProcessCommonFunctions
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Method to drop data_after_process table and Data01 Processed sheet if exist
        /// </summary>
        /// <param name="Workbook">Workbook</param>
        /// <returns>bool value that indicate whether process successfullu completed or not</returns>
        public static bool RestoreDataAfterProcess(Excel.Workbook Workbook)
        {
            MessageBoxResult result;
            result = MessageBox.Show(String.Format(CommonResource.ALERT_UNDO_DATA_PROCESSING), CommonResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (SQLiteConnection dbSource = DBHelper.GetConnectionStringByWrokbook(Workbook))
                    {
                        dbSource.Open();
                        SQLiteCommand mycommand = new SQLiteCommand(dbSource);
                        string dp_sql = "DROP TABLE IF EXISTS data_after_process";
                        mycommand.CommandText = dp_sql.ToString();
                        mycommand.ExecuteNonQuery();
                        dbSource.Close();
                        Excel.Worksheet ws = ExcelUtil.GetWorkSheetBySheetName(Workbook, Constants.SheetType.sh_Data01 + "(Processed)");
                        Workbook.Unprotect(Constants.Password);
                        if (ws != null)
                        {
                            ws.Application.DisplayAlerts = false;
                            ws.Delete();
                            Workbook.Protect(Constants.Password, true);
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);

                }
            }
            return false;
        }
    }
}
