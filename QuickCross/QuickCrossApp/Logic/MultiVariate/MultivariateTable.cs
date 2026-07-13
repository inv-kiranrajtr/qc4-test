using log4net;
using Qc4Launcher.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Logic.MultiVariate
{
    class MultivariateTable
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static bool CreateMultivariateTable(Microsoft.Office.Interop.Excel.Workbook workbook)
        {
            bool returnvalue = true;
            try
            {
                DB.DBHelper.CreateMultivariateTable(workbook);
            }
            catch (Exception ex)
            {
                returnvalue = false;
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            return returnvalue;
        }

        public static int Execute(Microsoft.Office.Interop.Excel.Workbook workbook, string query)//by 191  for altering dataafterprocess
        {
          return   DB.DBHelper.AlterTable(workbook, query );
        }
        public static bool AlterMultivariateTable(Microsoft.Office.Interop.Excel.Workbook workbook, string fields)//
        {            
            return DB.DBHelper.AlterMultivariateTable(workbook, fields);
        }
        public static void SaveDataTable(Microsoft.Office.Interop.Excel.Workbook workbook, DataTable DT, object[,] valueArray, List<string> columnList, int rowcount, string tablename = "multivariate")
        {
            try
            {
               
                    DB.DBHelper.SaveDataTable(workbook, DT, valueArray, columnList,  rowcount, tablename);
               
            }
            catch (Exception ex)
            {

                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        public static DataTable GetMultivariateData(System.Data.SQLite.SQLiteConnection con,string tablename)
        {
            DataTable dt=new DataTable();
            dt= DBHelper.GetDataTable("Select * from " + tablename + " order by sort_no ", con);
            return dt;
        }
        public static DataTable GetMultivariateData(System.Data.SQLite.SQLiteConnection con, string tablename,String queryvariablename)
        {
            DataTable dt = new DataTable();
            dt = DBHelper.GetDataTable("Select "+ queryvariablename + " from " + tablename + " order by sort_no ", con);
            return dt;
        }

        public bool[] GetFilterForInvalidUnknow(DataTable dttable, bool[] filterflag)
        {
            for(int i=0;i<dttable.Rows.Count;i++)
            {
                if(filterflag[i]==true)
                {
                    for(int j=0;j<dttable.Columns.Count;j++)
                    {
                        if(Convert.ToString( dttable.Rows[i][j]).Equals(string.Empty)|| Convert.ToString(dttable.Rows[i][j]).Equals("*") || Convert.ToString(dttable.Rows[i][j]).Equals("**") || Convert.ToString(dttable.Rows[i][j]).Equals("DK"))
                        {
                            filterflag[i] = false;
                            break;
                        }
                    }
                }
            }

            return filterflag;
        }
        public bool[] CopyOrgFilterflag(bool[] filterflagorg, bool[] filterflag)
        {
            if(filterflagorg.Length!= filterflag.Length)
            {
                return filterflag;
            }
            for(int i=0;i< filterflagorg.Length;i++)
            {
                filterflag[i] = filterflagorg[i];
            }
            return filterflag;
        }
        public bool[] SetFilterflag( bool[] filterflag,bool value)
        {           
            for (int i = 0; i < filterflag.Length; i++)
            {
                filterflag[i] = value;
            }
            return filterflag;
        }
    }
}
