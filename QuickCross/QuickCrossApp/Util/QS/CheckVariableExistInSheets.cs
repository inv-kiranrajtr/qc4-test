using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Util.QS
{
    class CheckVariableExistInSheets
    {
        public static bool DataProcess_sheet = false;
        public static bool Cross_sheet = false;
        public static bool Summery_T_sheet = false;
        public static bool Gt_sheet = false;
        public static bool FA_sheet = false;
        public static bool Data_After_process = false;
        
        public void Changestate (string sheet, bool value)
        {
            if(sheet==Util.Constants.SheetCodeName.DataProcess)
            {
                DataProcess_sheet = value;
            }
            if (sheet == Util.Constants.SheetCodeName.CrossTabulation)
            {
                Cross_sheet = value;
            }
            if (sheet == Util.Constants.SheetCodeName.SummaryTable)
            {
                Summery_T_sheet = value;
            }
            if (sheet == Util.Constants.SheetCodeName.GTTabulation)
            {
                Gt_sheet = value;
            }
            if(sheet == Util.Constants.SheetCodeName.FACreation)
            {
                FA_sheet = value;
            }
            if (sheet == Util.Constants.SheetCodeName.Data01After)
            {
                Data_After_process = value;
            }

        }
        public void Resetsheetstatus()
        {
          DataProcess_sheet = false;
          Cross_sheet = false;
          Summery_T_sheet = false;
          Gt_sheet = false;
          FA_sheet = false;
        }
           
       
    }
}
