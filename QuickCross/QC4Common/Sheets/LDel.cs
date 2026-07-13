using QC4Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace QC4Common.Sheets
{
    class LDel
    {
        public static Excel.Worksheet LdelSheet;

        public LDel(Excel.Worksheet worksheet)
        {
            LdelSheet = worksheet;
            LdelSheet.Change += OnLDELCellChanged;

        }
        public void OnLDELCellChanged(Excel.Range ChangedCell)
        {

            Excel.Range start = LdelSheet.Cells[2, 2];
            Excel.Range end = ExcelUtilForAddIn.EndxlRight(start);
            Excel.Range range = LdelSheet.get_Range(start, end);
            Definitions.optionList.Clear();

            foreach (Excel.Range r in range)
            {
                if (!string.IsNullOrEmpty(r.Text))
                    Definitions.optionList.Add(r.Text);
            }

        }
    }
}
