using QC4Common.Model;
using System.Collections.Generic;
using Constant = QC4Common.Common.Constants;

namespace ExcelAddIn.Common
{
    public class Definitions
    {
        public static Dictionary<string, QuestionSettings> VariableDictionary;
		public static Dictionary<int, string> IdFlagDictionary;
		public static List<string> RowVariableList;
        public static bool VariableEditMode = false;
        public static bool isQsChanged = false;
        public static List<string> optionList = new List<string>();
		public static bool isQsUpdated = true;
		public static bool isSheetUpdating = false;

        public static string[][,] resultjagArray1;

        public static List<string> fAOfList = new List<string>();
        public static List<string> fACriteria = new List<string>();
        public static List<string> fAVariable = new List<string>();
        public static List<string> fAAddtionalVariable = new List<string>();
		public static long QsRowCount = 0;

		public static string connectionString;
		private static Microsoft.Office.Interop.Excel.Application _outputApplication = null;
		public static Microsoft.Office.Interop.Excel.Application outputApplication
		{
			get
			{
				if (_outputApplication == null)
				{
					_outputApplication = new Microsoft.Office.Interop.Excel.Application();
				}
				else
				{
					try
					{
						_outputApplication.Caption = "";
					}
					catch
					{
						_outputApplication = new Microsoft.Office.Interop.Excel.Application();
					}
				}
				return _outputApplication;
			}
		}

		public static List<string> ExtentionList = new List<string>() { ".Quick-CROSS", ".quick-cross" };        
		public static List<string> FileList = new List<string>() { Constant.TemplateFile.DATA_OUTPUT, Constant.TemplateFile.QC3_Template, Constant.TemplateFile.QC4_Template_Do, Constant.TemplateFile.List_UP };
	}
}
