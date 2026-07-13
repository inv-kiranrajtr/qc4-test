using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelAddIn.Logic
{
    public class DPCallMethod
    {
        public string MethodName { get; set; }
        public int paramcount { get; set; }
        public int Rowstart { get; set; }
        public int RowEnd { get; set; }
        public List<string> ParamList { get; set; }

        public DPCallMethod()
        {
            MethodName = string.Empty;
            Rowstart = 0;
            RowEnd = 0;
            paramcount = 0;
            ParamList = new List<string>();
        }

    }
}
