using log4net;
using QC4Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using VB = Microsoft.VisualBasic;

namespace QC4Common.Util
{
    public class QSUtil
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="range"></param>
        /// <returns>-1 if no id found</returns>
        /// <returns>0 - n if id found</returns>
        public static int GetQSRowId(Excel.Range range)
        {
            try
            {
                Excel.Name name = range.Name;
                if (name != null)
                {
                    string str = name.Name;
                    //if (Regex.IsMatch(str, ".*QSRN_[0-9]{4}$"))
                    if (IsRowName(str))
                    {
                        return Convert.ToInt32(str.Substring(str.Length - 5));
                    }
                }
            }
            catch { }
            return -1;
        }

        public static bool IsVariableColumnFound(Excel.Range range)
        {
            int start = range.Column;
            int last = range.Columns[range.Columns.Count].Column;

            if (start <= Constants.QS.QsColItem && last >= Constants.QS.QsColItem)
            {
                return true;
            }
            return false;
        }

        public static void SetRowName(Excel.Worksheet worksheet, Excel.Range row, int id)
        {
            worksheet.Names.Add(Constants.QS.RowNamePrefix + id.ToString("00000"), row, false);
        }

        public static bool IsRowName(string name)
        {
            return Regex.IsMatch(name, Constants.QS.RowNamePrefix + "[0-9]{5}$");
        }

        public static void AnImpRowDelete(Excel.Range range)
        {
            DialogResult result = MessageBox.Show(CommonResource.QS_ALERT_AN_IMP_ROW_DELETE, "QuickCross", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                DeleteRow(range);
            }
        }

        public static void OrgDelete()
        {
            MessageDialog.ErrorOk(CommonResource.QS_ALERT_ORG_DELETE);
        }

        public static void DeleteRow(Excel.Range range)
        {
			Excel.Application app = range.Application;
			app.EnableEvents = false;
            range.EntireRow.Delete();
			app.EnableEvents = true;
        }

        public static bool ValidateVariable(string variable, out string message)
        {
            message = "";
            try
            {
                if (variable.ToLower().Equals("weightback"))
                {
                    message = CommonResource.QS_ALERT_WEIGHTBACK;
                    return false;
                }

                string varS = Encoding.Default.GetString(Encoding.Default.GetBytes(variable));
               if(Regex.IsMatch(varS, @"^[0-9０-９]*$"))
                { 
                    message = CommonResource.QS_VARIABLE_NUMBER_ONLY;
                    return false;
                }
                else if (variable.Length > 25)
                {
                    message = CommonResource.QS_VARIABLE_MAX_LENGTH;
                    return false;
                }

                else if (!ValidateVariable(variable, out message, true))
                {
                    message = CommonResource.QS_VARIABLE_SPECIAL_CHARACTER;
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }
        public static bool ValidatedQuestionNumber(string QuestionNumber,out string message)
        {
            message = "";
            try
            {
                if (!ValidateVariable_qustionnumber(QuestionNumber, out message, true))
                {
                    message = CommonResource.QS_INVALID_QUSTIONNUMBER;
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        private static bool ValidateVariable(string Check_String, out string msg, bool dummy)
        {
            string Now_Str;
            int i;
            for (i = 1; i <= VB.Strings.Len(Check_String); i++)
            {
                Now_Str = VB.Strings.Mid(Check_String, i, 1);
                if (VB.Strings.Asc("\n") == VB.Strings.Asc(Now_Str) || VB.Strings.Asc("\r") == VB.Strings.Asc(Now_Str))
                {
                    msg = CommonResource.QS_VARIABLE_LINEFEED_CHARACTER;
                    return false;
                }
                else if (VB.Strings.Asc("\t") == VB.Strings.Asc(Now_Str))
                {
                    msg = CommonResource.QS_VARIABLE_TAB_CHARACTER;
                    return false;
                }
                else if (VB.Strings.Asc("A") <= VB.Strings.Asc(Now_Str) && VB.Strings.Asc(Now_Str) <= VB.Strings.Asc("Z"))
                {
                    continue;
                }
                else if (VB.Strings.Asc("a") <= VB.Strings.Asc(Now_Str) && VB.Strings.Asc(Now_Str) <= VB.Strings.Asc("z"))
                {
                    continue;
                }
                else if (VB.Strings.Asc("0") <= VB.Strings.Asc(Now_Str) && VB.Strings.Asc(Now_Str) <= VB.Strings.Asc("9"))
                {
                    continue;
                }
                else if (VB.Strings.Asc("¦") <= VB.Strings.Asc(Now_Str) && VB.Strings.Asc(Now_Str) <= VB.Strings.Asc("ß"))
                {
                    continue;
                }
                else if (VB.Strings.Asc("`") == VB.Strings.Asc(Now_Str))
                {
                    msg = CommonResource.QS_VARIABLE_SPECIAL_CHARACTER;
                    return false;
                }
                else
                {
                    switch (Now_Str)
                    {
                        case "@":
                        case "=":
                        case "!":
                        case "?":
                        case "`":
                        case "*":
                        case ":":
                        case @"\":
                        case "/":
                        case "<":
                        case ">":
                        case "[":
                        case "]":
                        case "&":
                        case "'":
                        case "|":
                        case "~":
                        case "¥":
						case "＝":
						case "’":
						case "＠":
						case "！":
						case "＆":
						case "～":
						case "｜":
						case "￥":
						case "［":
						case "］":
						case "‘":
						case "＊":
						case "：":
						case "＜":
						case "＞":
						case "／":
						case "？":
							{
                                msg = CommonResource.QS_VARIABLE_SPECIAL_CHARACTER;
                                return false;
                            }
                    }
                }
            }
            msg = "";
            return true;
        }
        private static bool ValidateVariable_qustionnumber(string Check_String, out string msg,bool dummy)
        {
            string Now_Str;
            int i;
            for (i = 1; i <= VB.Strings.Len(Check_String); i++)
            {
                Now_Str = VB.Strings.Mid(Check_String, i, 1);
                if (VB.Strings.Asc("\n") == VB.Strings.Asc(Now_Str) || VB.Strings.Asc("\r") == VB.Strings.Asc(Now_Str))
                {
                    msg = CommonResource.QS_INVALID_QUSTIONNUMBER;
                    return false;
                }
                else if (VB.Strings.Asc("\t") == VB.Strings.Asc(Now_Str))
                {
                    msg = CommonResource.QS_INVALID_QUSTIONNUMBER; ;
                    return false;
                }
                
            }
            msg = "";
            return true;
        }
        /// <summary>
        /// Generate a new variable name by checking in the current variable list
        /// </summary>
        /// <param name="variable">new variable name</param>
        /// <param name="variableList">Current variable list</param>
        /// <returns>Return new variable name</returns>
        public String GetVariableName(String variable, List<QC4Common.Model.QuestionSettings> variableList)
        {
            Regex regex;
            Match match;
            if (!variableList.Any(q => q.Variable.Equals(variable, StringComparison.OrdinalIgnoreCase)))
            {
                regex = new Regex(@"^N(\d+)(.*)");
                match = regex.Match(variable);
                if (match.Success)
                {
                    variable = GenerateVariableName(match.Groups[2].Value, variableList);
                }
                return variable;
            }

            regex = new Regex(@"^N(\d+)(.*)");
            match = regex.Match(variable);
            if (match.Success)
            {
                variable = GenerateVariableName(match.Groups[2].Value, variableList, Convert.ToInt32(match.Groups[1].Value) + 1);
            }
            else
            {
                regex = new Regex(@"^N(.*)");
                match = regex.Match(variable);
                if (match.Success)
                {
                    variable = GenerateVariableName(match.Groups[1].Value, variableList, 1);
                }
                else
                {
                    variable = GenerateVariableName(variable, variableList);
                }
            }
            return variable;
        }

        private static string GenerateVariableName(string varible, List<QC4Common.Model.QuestionSettings> variableList, int times = 0)
        {
            String str = "N" + (times == 0 ? "" : times.ToString()) + varible;
            if (variableList.Any(q => q.Variable.Equals(str, StringComparison.OrdinalIgnoreCase)))
            {
                str = GenerateVariableName(varible, variableList, times + 1);
            }
            return str;
        }
        public String GetANVariableName(String variable, List<QC4Common.Model.QuestionSettings> variableList,bool isfactor=false,int choicecount=0,string Suffix="")
        {
            if (!variableList.Any(q => q.Variable.StartsWith(variable, StringComparison.OrdinalIgnoreCase)))
            {
                if (isfactor)
                {
                    variable = GenerateANVariableName(variable, variableList, isfactor, choicecount,Suffix);
                }
                return variable;
            }

            Regex regex = new Regex(@"^AN(\d+)");
            Match match = regex.Match(variable);
            if (match.Success)
            {
                variable = GenerateANVariableName("AN", variableList, Convert.ToInt32(match.Groups[1].Value) + 1);
            }     
            if(isfactor)
            {
                variable = GenerateANVariableName(variable, variableList,isfactor, choicecount,Suffix);
            }
            return variable;
        }

        private static string GenerateANVariableName(string varible, List<QC4Common.Model.QuestionSettings> variableList, int times = 0)
        {
            String str =  varible+ (times == 0 ? "" : times.ToString());
            if (variableList.Any(q => q.Variable.StartsWith(str, StringComparison.OrdinalIgnoreCase)))
            {
                str = GenerateANVariableName(varible, variableList, times + 1);
            }
            return str;
        }
        private static string GenerateANVariableName(string varible, List<QC4Common.Model.QuestionSettings> variableList,bool isfactor=true ,int choicecount = 1, string Suffix = "")
        {
            
            for (int i = 1; i <= choicecount; i++)
            {
                String str = varible + Suffix + i;
                if (variableList.Any(q => q.Variable.Equals(str, StringComparison.OrdinalIgnoreCase)))
                {
                    Regex regex = new Regex(@"^AN(\d+)");
                    Match match = regex.Match(varible);
                    if (match.Success)
                    {
                        varible = GenerateANVariableName("AN", variableList, Convert.ToInt32(match.Groups[1].Value) + 1);
                        varible = GenerateANVariableName(varible, variableList, isfactor, choicecount,  Suffix);
                    }
                }
            }
            return varible;
        }
    }
}

