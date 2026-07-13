using ExcelAddIn.Sheets;
using QC4Common.Model;
using Qc4Launcher.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Util
{
	class Definiotion
	{
		public static Dictionary<string, QuestionSettings> VariableDictionary = new Dictionary<string, QuestionSettings>();
		public static string SelectedDir = null;
		public static string SelectedFile = null;
		internal static string commandLineArg;
		public static bool SaveAs = false;
	}
}
