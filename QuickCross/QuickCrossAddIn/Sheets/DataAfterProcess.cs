using ExcelAddIn.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelAddIn.Sheets
{
    class DataAfterProcess
    {
        public Excel.Worksheet Sheet;
        public DataAfterProcess(Excel.Worksheet sheet)
        {
            Sheet = sheet;
        }
        public string GetSqlQueryforDT(int sortnumber = 0)
        {
            string sql = "SELECT * FROM data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + Constants.MAX_ROW_COUNT.ToString();            
            return sql;
        }
        public void LoadData(Excel.Worksheet sheet)
        {
            int sort_number = 0;
            int samplescount = Common.Util.GetTotalRowcount();

            int nPageCount = samplescount % Constants.MAX_ROW_COUNT > 0 ? (samplescount / Constants.MAX_ROW_COUNT) + 1 : (samplescount / Constants.MAX_ROW_COUNT);
            for (int i = 0; i < nPageCount; i++)
            {
                string sql = GetSqlQueryforDT(sort_number);
                DataTable dt = Common.Util.LoadDataFromDB(sql);
                object[,] tablearray = Common.Util.LoadDTtoArray(dt);
                //datarange = DataAfterProcess.Range[DataAfterProcess.Cells[4, 1], DataAfterProcess.Cells[3 + dt.Rows.Count + sort_number, dt.Columns.Count - 1]];
                Excel.Range startcell = sheet.Cells[4 + sort_number, 1];
                Excel.Range endcell = sheet.Cells[3 + sort_number+ dt.Rows.Count, dt.Columns.Count - 1];
                Excel.Range datarange = sheet.Range[startcell, endcell];
                datarange.Value = tablearray;
                sort_number = Common.Util.GetLastprocessedsortnumber(sort_number);
            }

            

        }
    }
}
