using QcWebCommon.Sheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QcWebCommon.Sheets;

namespace QcWebCommon.Common
{

    enum SheetIndex
    {
        QUESTION_SETTINGS = 1,
        DATA_PROCESS = 2
    }
    enum DP_COLUMN_INDEX
    {

    }
    class Definitions
    {
       // public static Dictionary<string, QuestionSettings> VariableDictionary;
        public static List<string> RowVariableList;
        public static bool VariableEditMode = false;
        public static bool FlagFromQs = false;
        public static List<string> optionList = new List<string>();

        public static string[][,] resultjagArray1;

        public static List<string> fAOfList = new List<string>();
        public static List<string> fACriteria = new List<string>();
        public static List<string> fAVariable = new List<string>();
        public static List<string> fAAddtionalVariable = new List<string>();


//#if DEBUG
//        public static List<string> ExtentionList = new List<string>() { ".xlsx", ".qc3", ".qcg", ".qc3x", ".qcgx", ".qc4" };
//#else
        public static List<string> ExtentionList = new List<string>() { ".qc4", ".xlsx"};        
//#endif
    }
}
