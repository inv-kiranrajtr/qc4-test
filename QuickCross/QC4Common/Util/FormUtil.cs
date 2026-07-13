using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using excel = Microsoft.Office.Interop.Excel;
using System.Data;
using Macromill.QCWeb.Common;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using QC4Common.Model;
using System.Data.SQLite;

namespace QC4Common.Util
{
    public class FormUtil
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        public static string commaseperatedvalues = string.Empty;
        public static List<string> listrangevalues = new List<string>();
        //get combo choice count from 1-1000 with (Auto)
        private static Regex itemNameRegex = new Regex(Common.Constants.ITEMNAME_PATTERN_MCONVERT);//ITEMNAME_PATTERN   //ITEMNAME_PATTERN_MCONVERT
        public string[] LoadComboWithChoices(int choicecount = 1000)
        {
            int[] choices = new int[choicecount + 1];
            string[] choicesList = new string[choicecount + 1];
            choices = Enumerable.Range(0, choicecount + 1).ToArray();
            choicesList = Array.ConvertAll(choices, ele => ele.ToString());
            choicesList[0] = CommonResource.LBL_AUTO;//Common.Constants.comboautovalue;//"(Auto)";
            return choicesList;
        }
        public int IsDataProcessedMulti(excel.Workbook workbook)
        {
           // string tablename = QC4Common.Common.Constants.tablenameanswers;
            DataTable dt = new DataTable();
            int error = 0;
            string tablename = string.Empty;
            if (DB.DBHelper.checkAfterProcess(workbook))
            {
                tablename = QC4Common.Common.Constants.tablenamedataafterprocess;

                
                try
                {

                    string query = "select * from " + tablename + "";//select  min(CAST(q_3 as decimal))as min ,max(CAST(q_3 as decimal))as max,avg(sample_id) as avg from answers where q_3 IS NOT NULL and  q_3<>'*'
                    string connectionstring = DB.DBHelper.GetConnectionString(workbook);
                    if (connectionstring != null)
                    {
                        using (SQLiteConnection connection = new SQLiteConnection(connectionstring))
                        {
                            connection.Open();
                            using (SQLiteCommand cmd = new SQLiteCommand(connection))
                            {
                                cmd.CommandText = query.ToString();
                                cmd.CommandType = CommandType.Text;
                                var dr = cmd.ExecuteReader();
                                error = dr.FieldCount;
                                return error;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return error;
                }
                
            }
            return error;
           

        }
        public bool IsDataProcessed(string variablename, excel.Workbook workbook, bool isnewvariable = false)
        {
            string tablename = QC4Common.Common.Constants.tablenameanswers;

            if (isnewvariable)
            {
                tablename = QC4Common.Common.Constants.tablenamedataafterprocess;
            }
            bool error = true;
            try
            {
                Dictionary<string, QuestionSettings> dictionary = Util.DictionaryUtil.PopulateQSDictionary(workbook);
                int variableid = Convert.ToInt32(Regex.Match(variablename, @"\d+").Value);
                if (dictionary.Values.Any(x => x.ItemId == variableid && x.QuestionFlag == "An"))
                {
                    tablename = "multivariate";
                }
                string query = "select CAST(" + variablename + " as double) from " + tablename + " where " + variablename + " IS NOT NULL and  " + variablename + "<>'*'";//select  min(CAST(q_3 as decimal))as min ,max(CAST(q_3 as decimal))as max,avg(sample_id) as avg from answers where q_3 IS NOT NULL and  q_3<>'*'

                

                string connectionstring = DB.DBHelper.GetConnectionString(workbook);
                if (connectionstring != null)
                {
                    DataTable dt = DB.DBHelper.GetDataTable(query, connectionstring);
                    if (dt != null)
                    {
                        try
                        {
                            error = true;
                        }
                        catch (Exception ex)
                        {
                        }

                    }
                    else
                    {
                        error = false;
                    }
                }
            }
            catch (Exception ex)
            {
                error = false;
            }
            return error;
        }
        //get minmaxavg for ntype variables in integrate
        public void StandardDeviationSA(string Score, string variablename, excel.Workbook workbook, out double avg, out double meadian, out double deviation, out double mode, out double meanplusdev, out double Min, out double max, bool isnewvariable = false)
        {
            Dictionary<string, QuestionSettings> dictionary = Util.DictionaryUtil.PopulateQSDictionary(workbook);
            List<double?> NoOfValue = new List<double?>();
            double sum = 0;
            double count = 0;

            meadian = 0; deviation = 0; mode = 0; meanplusdev = 0; Min = 0; max = 0; avg = 0;
            string tablename = QC4Common.Common.Constants.tablenameanswers;

            bool isdataProcess = DB.DBHelper.checkAfterProcess(workbook);
            if (isdataProcess)
            {
                tablename = QC4Common.Common.Constants.tablenamedataafterprocess;
            }
            int variableid = Convert.ToInt32(Regex.Match(variablename, @"\d+").Value);
            if (dictionary.Values.Any(x => x.ItemId == variableid && x.QuestionFlag == "An"))
            {
                tablename = "multivariate";
            }

            string query = "select CAST(" + variablename + " as double) from " + tablename + " where " + variablename + " IS NOT NULL and  " + variablename + "<>'*'";//select  min(CAST(q_3 as decimal))as min ,max(CAST(q_3 as decimal))as max,avg(sample_id) as avg from answers where q_3 IS NOT NULL and  q_3<>'*'
                                                                                                                                                                      //   string query = "CASE EXISTS (SELECT CAST(" + variablename + " as double) from " + tablename1 + ") THEN select CAST(" + variablename + " as double) from " + tablename1 + " where " + variablename + " IS NOT NULL and  " + variablename + "<>'*' else  select CAST(" + variablename + " as double) from " + tablename + " where " + variablename + " IS NOT NULL and  " + variablename + "<>'*'";


            string connectionstring = DB.DBHelper.GetConnectionString(workbook);
            if (connectionstring != null)
            {
                DataTable dt = DB.DBHelper.GetDataTable(query, connectionstring);
                if (dt != null)
                {
                    try
                    {


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            NoOfValue.Add(double.Parse(Convert.ToString(dt.Rows[i][0])));
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                }
            }

            List<double?> values = new List<double?>();
            if (!string.IsNullOrWhiteSpace(Score))
            {
                foreach (var s in Score.Split(','))
                {
                    double num;
                    if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                    {
                        values.Add(null);
                    }
                    if (double.TryParse(s, out num))
                    {
                        values.Add(num);
                    }

                }
            }

            List<double?> replaceList = new List<double?>();
            if (values.Count > 0 && NoOfValue.Count > 0)
            {

                try
                {
                    for (int i = 0; i < NoOfValue.Count; i++)
                    {
                        if (((Convert.ToInt16(NoOfValue[i]) - 1) >= 0))
                        {
                            replaceList.Add(values[(Convert.ToInt16(NoOfValue[i]) - 1)]);
                        }
                        else
                        {
                            replaceList.Add(null);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            try
            {
                if (replaceList.Count > 0)
                {
                    
                    replaceList.ToArray();

                    stddev(replaceList, workbook, out meadian, out deviation, ref mode, out Min, out max, out avg);
                    deviation = workbook.Application.Evaluate("=ROUND(" + deviation + ",1)");
                    avg = workbook.Application.Evaluate("=ROUND(" + avg + ",1)");
                    double meanplus = (2 * deviation) + avg;
                    meanplusdev = (double)Math.Truncate(meanplus);
                }

            }
            catch (Exception ex)
            {

            }

        }
        public void SAtypeGetMinMaxAvg(string Score, string variablename, double choicecount, excel.Workbook workbook, out double min, out double max, out double avg, bool isnewvariable = false)
        {

            min = 0; max = 0; avg = 0;
            List<double?> values = new List<double?>();
            if (!string.IsNullOrWhiteSpace(Score))
            {
                foreach (var s in Score.Split(','))
                {
                    double num;
                    if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                    {
                        // values.Add(0);
                        values.Add(null);
                    }
                    if (double.TryParse(s, out num))
                    {
                        values.Add(num);
                    }

                }
            }


            string tablename = QC4Common.Common.Constants.tablenameanswers;
            if (isnewvariable)
            {
                tablename = QC4Common.Common.Constants.tablenamedataafterprocess;
            }
            Dictionary<string, QuestionSettings> dictionary = Util.DictionaryUtil.PopulateQSDictionary(workbook);
            if (variablename != QC4Common.Common.Constants.VariableSampleId)
            {
                int variableid = Convert.ToInt32(Regex.Match(variablename, @"\d+").Value);
                if (dictionary.Values.Any(x => x.ItemId == variableid && x.QuestionFlag == "An"))
                {
                    tablename = "multivariate";
                }
            }

            List<double> VariableVal = new List<double>();
            string query = "select  CAST(" + variablename + " as double) from " + tablename + " where " + variablename + " IS NOT NULL and  " + variablename + "<>'*'";//select  min(CAST(q_3 as decimal))as min ,max(CAST(q_3 as decimal))as max,avg(sample_id) as avg from answers where q_3 IS NOT NULL and  q_3<>'*'
            string connectionstring = DB.DBHelper.GetConnectionString(workbook);
            if (connectionstring != null)
            {
                DataTable dt = DB.DBHelper.GetDataTable(query, connectionstring);
                if (dt != null && dt.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (DataColumn col in dt.Columns)
                            {
                                VariableVal.Add(Convert.ToDouble(row[col]));
                            }
                        }
                    }
                    catch { }
                    List<double?> replaceList = new List<double?>();
                    if (values.Count > 0 && VariableVal.Count > 0)
                    {
                        for (int i = 0; i < values.Count; i++)
                        {
                            for (int j = 0; j < VariableVal.Count; j++)
                            {
                                if (VariableVal[j] == i + 1)
                                {
                                    replaceList.Add(values[i]);
                                }
                            }
                        }
                        if (values.Count > 0)
                        {
                            min = replaceList.Where(m => m != null).Min(m => m.Value);
                            max = replaceList.Where(m => m != null).Max(m => m.Value);
                            //avg = values.Sum()/choicecount;
                        }
                        avg = Math.Round((replaceList.Where(x => x != null).Sum(x => x.Value) / replaceList.Count(x => x != null)), 1);
                    }
                }

            }

        }

        public void NtypeGetMinMaxAvg(string variablename, excel.Workbook workbook, out double min, out double max, out double avg, bool isnewvariable = false)
        {
            min = 0; max = 0; avg = 0;
            string tablename = QC4Common.Common.Constants.tablenameanswers;

            if (isnewvariable)
            {
                tablename = QC4Common.Common.Constants.tablenamedataafterprocess;
            }
            Dictionary<string, QuestionSettings> dictionary = Util.DictionaryUtil.PopulateQSDictionary(workbook);
            if (variablename != QC4Common.Common.Constants.VariableSampleId)
            {
                int variableid = Convert.ToInt32(Regex.Match(variablename, @"\d+").Value);
                if (dictionary.Values.Any(x => x.ItemId == variableid && x.QuestionFlag == "An"))
                {
                    tablename = "multivariate";
                }
            }
            string query = "select  min(CAST(" + variablename + " as decimal))as min ,max(CAST(" + variablename + " as decimal))as max,avg(CAST(" + variablename + " as decimal)) as avg from " + tablename + " where " + variablename + " IS NOT NULL and  " + variablename + "<>'*'";//select  min(CAST(q_3 as decimal))as min ,max(CAST(q_3 as decimal))as max,avg(sample_id) as avg from answers where q_3 IS NOT NULL and  q_3<>'*'
            string connectionstring = DB.DBHelper.GetConnectionString(workbook);
            if (connectionstring != null)
            {
                DataTable dt = DB.DBHelper.GetDataTable(query, connectionstring);
                if (dt != null && dt.Rows.Count == 1)
                {
                    try
                    {
                        min = double.Parse(Convert.ToString(dt.Rows[0][0]));
                        max = double.Parse(Convert.ToString(dt.Rows[0][1]));
                        avg = Math.Round(double.Parse(Convert.ToString(dt.Rows[0][2])), 1);// (min + max) / 2;
                    }
                    catch { }
                }
            }
        }

        public void StandardDeviation(string variablename, excel.Workbook workbook, out double avg, out double meadian, out double deviation, out double mode, out double meanplusdev, out double min, out double max, bool isnewvariable = false)
        {


            Dictionary<string, QuestionSettings> dictionary = Util.DictionaryUtil.PopulateQSDictionary(workbook);
            List<double?> NoOfValue = new List<double?>();
            double sum = 0;
            double count = 0;
            meadian = 0; deviation = 0; mode = 0; meanplusdev = 0; avg = 0; min = 0; max = 0;
            string tablename = QC4Common.Common.Constants.tablenameanswers;


            bool isdataProcess = DB.DBHelper.checkAfterProcess(workbook);
            if (isdataProcess)
            {
                tablename = QC4Common.Common.Constants.tablenamedataafterprocess;
            }
            if (variablename != QC4Common.Common.Constants.VariableSampleId)
            {
                int variableid = Convert.ToInt32(Regex.Match(variablename, @"\d+").Value);
                if (dictionary.Values.Any(x => x.ItemId == variableid && x.QuestionFlag == "An"))
                {
                    tablename = "multivariate";
                }
            }
            string query = "select CAST(" + variablename + " as decimal) from " + tablename + " where " + variablename + " IS NOT NULL and  " + variablename + "<>'*'";//select  min(CAST(q_3 as decimal))as min ,max(CAST(q_3 as decimal))as max,avg(sample_id) as avg from answers where q_3 IS NOT NULL and  q_3<>'*'
            string connectionstring = DB.DBHelper.GetConnectionString(workbook);
            if (connectionstring != null)
            {
                DataTable dt = DB.DBHelper.GetDataTable(query, connectionstring);
                if (dt != null)
                {
                    try
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            NoOfValue.Add(double.Parse(Convert.ToString(dt.Rows[i][0])));
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            try
            {
                if (NoOfValue.Count > 0)
                {
                   
                    stddev(NoOfValue, workbook, out meadian, out deviation, ref mode, out min, out max, out avg);

                    deviation = workbook.Application.Evaluate("=ROUND(" + deviation + ",1)");
                    avg = workbook.Application.Evaluate("=ROUND(" + avg + ",1)");
                    double meanplus = (2 * deviation) + avg;
                    meanplusdev = (double)Math.Truncate(meanplus);
                }
            }
            catch (Exception ex)
            {

            }
        }

        
        public void stddev(List<double?> values, excel.Workbook wbbook, out double Meadian, out double stdValue, ref double Mode, out double Min, out double Max, out double Avg)
        {

            int endrow = values.Count;
          
            var xlWorkSheet = Util.ExcelUtil.GetWorkSheetByCodeName(wbbook, "Sheet2");
            excel.Range start = xlWorkSheet.Cells[50, 1];
            excel.Range end = xlWorkSheet.Cells[56 + endrow, 1];
            excel.Range r = xlWorkSheet.Range[start, end];
            r.Value = null;
            var data = r.Value;
            for (int i = 0; i < endrow; i++)
            {
                data[i + 1, 1] = values[i];
            }
            data[endrow + 2, 1] = "=STDEV(A50:A" + (50 + endrow) + ")";
            data[endrow + 3, 1] = "=MEDIAN(A50:A" + (50 + endrow) + ")";
            data[endrow + 4, 1] = "=MODE(A50:A" + (50 + endrow) + ")";
            data[endrow + 5, 1] = "=MIN(A50:A" + (50 + endrow) + ")";
            data[endrow + 6, 1] = "=MAX(A50:A" + (50 + endrow) + ")";
            data[endrow + 7, 1] = "=AVERAGE(A50:A" + (50 + endrow) + ")";
            r.Value = data;
            stdValue = 0;
            Meadian = 0;
            Mode = 0;
            Min = 0;
            Max = 0;
            Avg = 0;
            if (!values.All(x => x == null))
            {
                excel.Range r1 = xlWorkSheet.Range[start, end];
                object[,] stds = r1.Value;
                if (stds != null)
                {
                    stdValue = Convert.ToDouble(stds[endrow + 2, 1]);
                    Meadian = Convert.ToDouble(stds[endrow + 3, 1]);
                    Mode = Convert.ToDouble(stds[endrow + 4, 1]);
                    Min = Convert.ToDouble(stds[endrow + 5, 1]);
                    Max = Convert.ToDouble(stds[endrow + 6, 1]);
                    Avg = Convert.ToDouble(stds[endrow + 7, 1]);
                }
            }
            if (values.Count <= 1)
            {
                stdValue = 0;
                double? mode = null;
                Mode =0;
            }
           
        }
        private double CalculateStandardDeviation(IEnumerable<double?> values)
        {
            double standardDeviation = 0;

            if (values.Any())
            {
                 
                double? avg = values.Average();
                double? avg1 = values.Sum() / values.Count();

                     
                double sum = Convert.ToDouble(values.Sum(d => Math.Pow(Convert.ToDouble(d) - Convert.ToDouble(avg), 2)));

                    
                standardDeviation = Convert.ToDouble(Math.Sqrt((sum) / (values.Count() - 1)));
            }

            return standardDeviation;
        }
        public static string GetWindowTitle(IntPtr hWnd)
        {
            var length = GetWindowTextLength(hWnd);
            var title = new StringBuilder(length + 1);
            GetWindowText(hWnd, title, length + 1);
            return title.ToString();
        }
        public string GetDuplicateVariableName(string oldvariableName, int count, Dictionary<String, QC4Common.Model.QuestionSettings> Dictionary)
        {
            string newVariable = "N" + oldvariableName;
            int j = 1;
            for (int i = 0; i < count; i++)
            {
                if ((Dictionary.Values.ToList()).ElementAt(i).Variable == newVariable)
                {
                    newVariable = "N" + j + oldvariableName;
                    i = 0;
                    j++;
                }
            }

            return newVariable;
        }
        public int GetLastRow(DataTable dt, int column)//to get last inserted row of a gridcolumn
        {
            int row = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][column])))
                {
                    row = i + 1;
                }
            }
            return row;
        }
        public string GetCountMean(int[] a, int choiceindex)//get selected choices from list as choice numbers by limit or / seperated
        {
            try
            {
                Array.Sort(a);
                string startValue = a[0].ToString();
                string val1 = "";//startValue;
                string val2 = startValue;
                for (int k = 0; k < choiceindex; k++)
                {
                    List<int> startVal = new List<int>();
                    int s = k;
                    for (int i = s; i < choiceindex - 1 && a[i] + 1 == a[k + 1]; i++, k++)
                    {
                        startVal.Add(a[k]);
                    }
                    int endVal = a[k];
                    if (startVal.Count > 0)
                    {
                        if (val1 == "")
                            val1 = startVal[0] + "-" + endVal;
                        else
                            val1 += "/" + startVal[0] + "-" + endVal;
                    }
                    else
                    {
                        if (val1 == "")
                            val1 += endVal;
                        else
                            val1 += "/" + endVal;
                    }


                }
                return val1;
            }
            catch (Exception ex) { return string.Empty; }
        }
        public bool IsNumeric(string Contents, bool onlycheck = true)//checking if the string is numeric or not mainly for "N"types
        {
            commaseperatedvalues = string.Empty;
            string[] SplitContent;
            if (Contents.StartsWith("="))
            {
                // Contents = Contents.TrimStart('=');//Contents.Replace("=", string.Empty);
                try
                {
                    Contents = Contents.Remove(0, 1);
                }
                catch { }
            }
            else if (Contents.StartsWith("<>"))
            {
                Contents = Contents.TrimStart('<');//Contents.Replace("<>", string.Empty);
                Contents = Contents.TrimStart('>');
            }
            else if (Contents.StartsWith("!"))
            {
                Contents = Contents.TrimStart('!');
            }
            if (Contents.Contains("/") || Contents.Contains(","))
            {
                char[] splitchar = { '/', ',' };
                SplitContent = Contents.Split(splitchar);
            }
            else
            {
                SplitContent = new string[] { Contents };
            }
            foreach (string item in SplitContent)
            {
                string value = item;
                string Div_Char = "@";
                string minval = "minVal";

                value = value.Replace("(-", Div_Char);
                value = value.Replace(int.MinValue.ToString(), minval);
                value = value.Replace("(", "");
                value = value.Replace(")", "");
                value = value.Replace("-", ",");
                value = value.Replace(Div_Char, "-");
                value = value.Replace(minval, int.MinValue.ToString());
                string[] split2 = value.Split(',');
                //listrangevalues.Add(value);
                foreach (string s2 in split2)
                {
                    if (string.IsNullOrEmpty(s2))
                        continue;
                    double output = 0;
                    bool err = false;
                    if (!s2.IsDoubleExpression(out output, false, true, true, false))
                    {
                        err = true;
                    }
                    else if (output > int.MaxValue || output < int.MinValue)
                    {
                        err = true;
                    }
                    if (err && onlycheck)
                    {
                        //if (displayMessage)
                        //{
                        //    MessageDialog.ErrorOk(AddinResource.ERR_MSG_SET_NUMERIC_VALUE);

                        //}
                        commaseperatedvalues = string.Empty;
                        return false;
                    }
                    if (string.IsNullOrEmpty(commaseperatedvalues))
                    {
                        commaseperatedvalues += s2;
                    }
                    else
                    {
                        commaseperatedvalues += "," + s2;
                    }
                }

            }
            return true;
        }

        public bool IsMultipleLimit(string Contents)//checking if multiple Limits"-" is there
        {
            Contents = Contents.Replace("(-", string.Empty);
            string[] splitvalues = Contents.Split('-');
            if (splitvalues.Length > 2)
            {
                return false;
            }
            return true;
        }
        public bool IsLimitPresent(string Contents)//checking if Limits"-" is there
        {
            Contents = Contents.Replace("(-", string.Empty);
            string[] splitvalues = Contents.Split('-');
            if (splitvalues.Length >= 2)
            {
                return false;
            }
            return true;
        }
        public string ReplaceBrackets(string value)//replace brackets if present
        {
            if (value != null)
            {
                if (value.Contains("(") || value.Contains(")"))
                {
                    value = value.Replace("(", string.Empty);
                    value = value.Replace(")", string.Empty);
                }
            }

            return value;
        }
        public bool IsNotOtherThanStart(string Contents)//checking if ! is present other than at start
        {
            int count = 0;
            if (Contents.StartsWith("<>"))
            {

                Contents = Contents.TrimStart('<');
                Contents = Contents.TrimStart('>');
            }
            else if (Contents.StartsWith("!"))
            {
                count = Contents.Count(f => f == '!');
                Contents = Contents.TrimStart('!');
            }
            if (Contents.Contains("!") || Contents.Contains("<>") || count > 1)
            {
                return false;
            }
            return true;
        }

        public bool IsOverlap(List<string> countparamsValues, int catcount)
        {
            bool isoverlap = false;
            //if    countparamsValues is null then return true

            if (countparamsValues.Count > 1)
            {
                //if "!"  contains return true
                for (int i = 0; i < countparamsValues.Count; i++)
                {
                    string[] val1 = GetCommaSeperated(countparamsValues[i], catcount).Split(',');
                    for (int j = i + 1; j < countparamsValues.Count; j++)
                    {
                        string[] val2 = GetCommaSeperated(countparamsValues[j], catcount).Split(',');
                        foreach (string s in val1)
                        {
                            if (Array.Exists(val2, element => element == s))//(val2.Contains(s))// Array.Exists(array, element => element == "perls")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            //else if "!"  contains return true

            return isoverlap;

        }
        public String GetCommaSeperated(string value, int categorycount, bool exclude = true)//exclude if<> is mentioned so taking other values;if false nly take mentioned values 
        {
            if (value.StartsWith("="))
            {
                value = value.Replace("=", string.Empty);
            }
            string commaseperatedvalues = string.Empty;
            bool isnot = false;
            if (value.StartsWith("!") || value.StartsWith("<>"))
            {
                isnot = true;
            }
            if (value.StartsWith("!")) value = value.TrimStart('!');
            if (value.StartsWith("<>"))
            {
                value = value.Replace("<>", string.Empty);
            }
            List<string> commasep = new List<string>();
            List<string> barsep = new List<string>();
            List<string> minsep = new List<string>();
            List<int> exclidelist = new List<int>();
            //split with ','
            string[] criteriacommavalues = value.Split(',');
            foreach (string str in criteriacommavalues)
            {
                commasep.Add(str);//add whole to  list
            }
            // for each nd split with '/'
            foreach (string str in commasep)
            {
                if (str.Contains('/'))
                {
                    string[] criteriabarvalues = str.Split('/');
                    foreach (string s in criteriabarvalues)
                    {
                        barsep.Add(s);//add whole to list
                    }
                }
                else
                    barsep.Add(str);
            }

            foreach (string str in barsep)
            {
                if (isnot)//str.StartsWith("!") || str.StartsWith("<>")
                {
                    string notvalue = str;
                    //need to remove the items from list and add other category numbers
                    // criteriaValueDescription = criteriaValueDescription.TrimStart('!');
                    if (str.StartsWith("!")) notvalue = str.TrimStart('!');
                    else if (str.StartsWith("<>")) notvalue = str.Replace("<>", "");
                    //criteriaValueDescription = criteriaValueDescription.Replace("<>", "");//TrimStart('<>');

                    int criteriaend = categorycount;// Definitions.VariableDictionary[quesvar].CategoryCount;
                    if (str.Contains('-'))
                    {
                        int strt = 0, end = 0;
                        string[] criterisplitvals = notvalue.Split('-');

                        if (criterisplitvals.Length == 1)
                        {
                            try
                            {
                                strt = Convert.ToInt32(criterisplitvals[0]);
                            }
                            catch (Exception e) { strt = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                            end = strt;

                        }
                        else
                        {
                            try
                            {
                                strt = Convert.ToInt32(criterisplitvals[0]);
                            }
                            catch (Exception e) { strt = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                            try
                            {
                                end = Convert.ToInt32(criterisplitvals[1]);
                            }
                            catch (Exception e)
                            {
                                end = categorycount;// Definitions.VariableDictionary[quesvar].CategoryCount;
                                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                            }
                        }

                        for (int ci = strt; ci <= end; ci++)
                        {
                            exclidelist.Add(ci);
                        }
                        //for (int ci = 1; ci <= Definitions.VariableDictionary[quesvar].CategoryCount; ci++)
                        //{
                        //    if (!exclidelist.Contains(ci))
                        //        minsep.Add(ci.ToString());
                        //}
                    }
                    else
                    {
                        try
                        {
                            exclidelist.Add(Convert.ToInt32(str));
                        }
                        catch { }
                    }


                }
                else
                {
                    //else
                    if (str.Contains('-'))
                    {

                        int start = 0, limit = 0;
                        string[] criteriaminvalues = str.Split('-');
                        // foreach (string s in criteriaminvalues)
                        {

                            try
                            {

                                if (criteriaminvalues.Length == 1)
                                {
                                    try
                                    {
                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                                    limit = start;
                                }
                                else
                                {
                                    try
                                    {
                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }//actually get min value of answer
                                    try
                                    {
                                        limit = Convert.ToInt32(criteriaminvalues[1]);
                                    }
                                    catch (Exception e)
                                    {//actually get max value of answer;need to get max of choice no from item id and set limit
                                        limit = categorycount;// Definitions.VariableDictionary[quesvar].CategoryCount;
                                        System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                                    }
                                }
                                if (limit < start)//need to reverse if 9-7 comes
                                {
                                    int temp = limit;
                                    limit = start;
                                    start = temp;
                                }
                            }
                            catch { }

                            for (int ci = start; ci <= limit; ci++)
                            {
                                minsep.Add(ci.ToString());//add whole to list
                            }
                        }
                    }
                    else
                        minsep.Add(str);
                }
            }
            if (isnot && exclude)
            {
                for (int ci = 1; ci <= categorycount; ci++)// Definitions.VariableDictionary[quesvar].CategoryCount
                {
                    if (!exclidelist.Contains(ci))
                    {
                        if (!string.IsNullOrEmpty(commaseperatedvalues))
                        {
                            commaseperatedvalues += ",";
                        }

                        commaseperatedvalues += ci;
                    }
                }
            }
            else if (isnot && !exclude)
            {
                for (int ci = 1; ci <= categorycount; ci++)
                {
                    if (exclidelist.Contains(ci))
                    {
                        if (!string.IsNullOrEmpty(commaseperatedvalues))
                        {
                            commaseperatedvalues += ",";
                        }

                        commaseperatedvalues += ci;
                    }
                }
            }
            else
            {
                foreach (string str in minsep)
                {
                    if (!string.IsNullOrEmpty(commaseperatedvalues))
                    {
                        commaseperatedvalues += ",";
                    }

                    commaseperatedvalues += str;
                }
            }
            return commaseperatedvalues;
        }
        public bool CheckRangeExceeds(string choices, int choicecount)
        {
            string[] arraychoices = choices.Split(',');
            int outval = 0;
            for (int i = 0; i < arraychoices.Length; i++)
            {
                //https://app.gluemodel.com/#/project/task/4295061874
                if (!string.IsNullOrEmpty(Convert.ToString(arraychoices[i])) && !int.TryParse(Convert.ToString(arraychoices[i]), out outval) || !string.IsNullOrEmpty(Convert.ToString(arraychoices[i])) && (int.Parse(Convert.ToString(arraychoices[i])) > choicecount || int.Parse(Convert.ToString(arraychoices[i])) < 1))
                {
                    return false;
                }
            }
            return true;
        }
        public bool GetAllranges(string choice)
        {
            listrangevalues.Clear();
            choice = choice.TrimStart('=');
            choice = choice.TrimStart('<');
            choice = choice.TrimStart('>');
            choice = choice.TrimStart('!');
            string[] splitwithbar = choice.Split('/');
            foreach (string s in splitwithbar)
            {
                string[] splitwitcomma = s.Split(',');
                foreach (string ss in splitwitcomma)
                {
                    if (ss.Contains("-"))
                    {
                        listrangevalues.Add(ss);
                        try
                        {
                            string[] values = ss.Split('-');
                            if (!string.IsNullOrEmpty(Convert.ToString(values[0])) && !string.IsNullOrEmpty(Convert.ToString(values[1])))
                            {
                                float val1 = float.Parse(Convert.ToString(values[0]));
                                float val2 = float.Parse(Convert.ToString(values[1]));
                                if (val1 > val2)
                                {
                                    return false;
                                }
                            }

                        }
                        catch { }
                    }
                }
            }
            return true;
        }
        public string GetNtypeValueSeperatedByComma(string gridcelltext)
        {
            if (gridcelltext.StartsWith("="))
            {
                gridcelltext = gridcelltext.Replace("=", string.Empty);
            }
            if (gridcelltext.StartsWith("<>") || gridcelltext.StartsWith("!"))
            {
                gridcelltext = gridcelltext.Replace("<>", string.Empty);
                gridcelltext = gridcelltext.Replace("!", string.Empty);
            }
            string Div_Char = "@";
            string minval = "minVal";

            gridcelltext = gridcelltext.Replace("(-", Div_Char);
            gridcelltext = gridcelltext.Replace(int.MinValue.ToString(), minval);
            gridcelltext = gridcelltext.Replace("(", "");
            gridcelltext = gridcelltext.Replace(")", "");
            gridcelltext = gridcelltext.Replace("-", ",");
            gridcelltext = gridcelltext.Replace(Div_Char, "-");
            return gridcelltext;
        }
        public bool IsLowerLimitLesser(string val1, string val2)
        {
            if (!string.IsNullOrEmpty(val1) && !string.IsNullOrEmpty(val2))
            {
                try
                {
                    if (val1.Contains(Common.Constants.STD_DP.NotEqual) || val1.Contains(Common.Constants.STD_DP.NotEqualAngle))
                    {
                        val1 = TrimStartEqualNotequal(val1);
                    }

                    try { val1 = ReplaceBrackets(val1); } catch { }
                    try { val2 = ReplaceBrackets(val2); } catch { }

                    float floatval1 = float.Parse(val1);
                    float floatval2 = float.Parse(val2);
                    if (floatval1 > floatval2)
                    {
                        return false;
                    }
                }
                catch { return false; }
            }
            return true;
        }
        public bool IsonlySplChar(string cellvalue)
        {
            if (cellvalue.Equals(Common.Constants.EqEqual) || cellvalue.Equals(Common.Constants.STD_DP.NotEqual) || cellvalue.Equals(Common.Constants.STD_DP.NotEqualAngle) || cellvalue.Equals(Common.Constants.Minus))
            {
                return false;
            }
            return true;
        }
        public string TrimStartEqualNotequal(string cellvalue)
        {
            string value = cellvalue;
            if (value.StartsWith("="))
            {
                value = value.TrimStart('=');
            }
            if (value.StartsWith("!") || value.StartsWith("<>"))
            {
                value = value.TrimStart('!');
                value = value.TrimStart('<');
                value = value.TrimStart('>');
            }
            return value;
        }
        public string GetOperator(string value)
        {
            if (value.StartsWith("<>"))
            {
                return "<>";
            }
            else if (value.StartsWith("!"))
            {
                return "!";
            }
            else if (value.StartsWith("="))
            {
                return "=";
            }
            return "";
        }
        public bool IsVariableNameExists(String variable, List<QC4Common.Model.QuestionSettings> variableList, int org_imp_an = 0)
        {
            if (org_imp_an == 0)
            {
                if (variableList.Any(q => q.Variable.Equals(variable, StringComparison.OrdinalIgnoreCase) && q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Org))
                {
                    return false;
                }
            }
            else if (org_imp_an == 1)
            {
                if (
                    (variableList.Any(q => q.Variable.Equals(variable, StringComparison.OrdinalIgnoreCase) && (q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Org || q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Imp || q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.An))) ||
                    (variableList.Any(q => q.Variable.Equals(variable, StringComparison.OrdinalIgnoreCase) && (q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.New)) &&
                    (!variableList.Any(q => q.Variable.Equals(variable) && (q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.New)))
                    )
                    )
                {
                    return false;
                }
            }
            else if (org_imp_an == 2)
            {
                if (variableList.Any(q => q.Variable.Equals(variable, StringComparison.OrdinalIgnoreCase)) &&
                    (!variableList.Any(q => q.Variable.Equals(variable) && (q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.New))))
                {
                    return false;
                }
            }
            else if (org_imp_an == 3)
            {
                if (variableList.Any(q => q.Variable.Equals(variable, StringComparison.OrdinalIgnoreCase) && (q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Org || q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Imp || q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.An)))
                {
                    return false;
                }
            }
            else
            {
                if (variableList.Any(q => q.Variable.Equals(variable, StringComparison.OrdinalIgnoreCase)))
                {
                    return false;
                }
            }
            return true;
        }
        public bool NotExistsInString(string Contents)//checking if ! is present other than at start
        {

            if (Contents.Contains("!") || Contents.Contains("<>"))
            {
                return false;
            }

            return true;
        }
        public string GetOperator_in_string(string cellvalue)
        {
            string opr = string.Empty;
            if (cellvalue.Contains("="))
            {
                return "=";
            }
            if (cellvalue.Contains("!"))
            {
                return "!";
            }
            if (cellvalue.Contains("<>"))
            {
                return "<>";
            }
            return opr;
        }
        public DataTable ClearDataTableColumn(DataTable dt, int column)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][column] = string.Empty;
                }
            }
            catch { return dt; }
            return dt;
        }
        public bool IsNewQuestion(string questionvariableflagtype)
        {
            return (questionvariableflagtype == QC4Common.Common.Constants.QuestionFlag.New) ? true : false;
        }
        public bool IsVariableLengthExceeds(string variablename)
        {
            bool retvalue = false;
            if (variablename.Length > QC4Common.Common.Constants.QsVariableMaxLength)
            {
                retvalue = true;
            }
            return retvalue;
        }
        public string[] GetGridList()
        {
            string[] dt_Choices_columns = new string[2];
            dt_Choices_columns[0] = "SL";
            dt_Choices_columns[1] = "Choice";

            return dt_Choices_columns;
        }
        public string EscapeCRLF(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("\r\n", QC4Common.Common.Constants.CRLFchar);
                text = text.Replace("\n", QC4Common.Common.Constants.CRLFchar);
                
            }
            return text;
        }
        public string UnEscapeCRLF(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace(QC4Common.Common.Constants.CRLFchar, "\n");
            }
            return text;
        }
        public DataTable UnEscapeCRLFFromAllRows(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][j])))
                    {
                        dt.Rows[i][j] = UnEscapeCRLF(Convert.ToString(dt.Rows[i][j]));
                    }
                }
            }
            return dt;
        }
        public List<string> GetTextFromSquareBrakets(string choices)
        {
            if (!string.IsNullOrEmpty(choices))
            {
                choices = EscapeCRLF(choices);
            }
            List<string> choiceslist = new List<string>();
            foreach (Match match in itemNameRegex.Matches(choices))
            {
                string tmpName = match.Groups[1].Value;
                if (choiceslist.Contains(tmpName)) continue;
                choiceslist.Add(tmpName);
            }
            return choiceslist;
        }
        public string Addsinglequete(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.StartsWith("'"))
            {
                value = "'" + value;
            }
            return value;
        }
        public string RemovesingleForcheck(string value)
        {
            if (value.StartsWith("'"))
            {
                value = value.Remove(0, 1);
            }
            return value;
        }
        public T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        public void SetErrorForGrid(System.Windows.Controls.DataGrid gridnewvariable, int rowpos, int columnpos, string BrushesArea, bool Reset = false)
        {
            if (rowpos > -1)
            {
                DataGridRow row = (DataGridRow)gridnewvariable.ItemContainerGenerator.ContainerFromIndex(rowpos);
                if (row != null)
                {
                    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                    System.Windows.Controls.DataGridCell cell = (System.Windows.Controls.DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnpos);
                    SolidColorBrush color = Brushes.Red;
                    if (Reset)
                    {

                        switch (BrushesArea)
                        {
                            case QC4Common.Common.Constants.STD_DP.Background:
                                color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE1E1E1"));// Brushes.Transparent;//recheck color and set
                                break;
                            case QC4Common.Common.Constants.STD_DP.Foreground:
                                color = Brushes.Black;//recheck color and set
                                break;
                        }
                    }
                    switch (BrushesArea)
                    {
                        case QC4Common.Common.Constants.STD_DP.Background:
                            if (Reset)
                            {
                                cell.ClearValue(Border.BorderBrushProperty);//  cell.BorderThickness = new Thickness(0);
                            }
                            else { cell.BorderBrush = color; }//cell.BorderThickness = new Thickness(1); 
                            break;
                        case QC4Common.Common.Constants.STD_DP.Foreground:
                            cell.Foreground = color;
                            break;
                    }
                }
            }
        }
        public void SetCellValueInSettings(int row, int column, excel.Workbook Workbook, string value)
        {
            excel.Worksheet settingssheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, QC4Common.Common.Constants.SheetCodeName.Setting);
            excel.Range dpcell = settingssheet.Cells[row, column];
            dpcell.Value = value;
        }
        public string GetCellValueFromSettings(int row, int column, excel.Workbook Workbook)
        {
            string retval = string.Empty;
            excel.Worksheet settingssheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, QC4Common.Common.Constants.SheetCodeName.Setting);
            excel.Range dpcell = settingssheet.Cells[row, column];
            retval = dpcell.Value == null ? string.Empty : Convert.ToString(dpcell.Value);
            return retval;
        }
        public bool GetBoolValueFromString(string value)
        {
            bool retval = false;

            if (string.IsNullOrEmpty(value))
            {
                retval = false;
            }
            else
            {
                bool.TryParse(value, out retval);
            }
            return retval;
        }
        public List<int> GetListfromCommaseperatedValues(string value)
        {
            List<int> retlist = new List<int>();

            if (!string.IsNullOrEmpty(value))
            {
                string[] values = value.Split(',');
                if (values.Length > 0)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        int res = 0;
                        if (int.TryParse(Convert.ToString(values[i]), out res))
                        {
                            retlist.Add(res);
                        }
                    }
                }
            }

            return retlist;
        }
        private bool CheckLimitExceed(int noofchoices, List<string> paramvalues)
        {

            List<string> commasep;
            List<string> barsep;
            List<string> minsep = new List<string>();
            foreach (string paramstr in paramvalues)
            {
                commasep = new List<string>();
                barsep = new List<string>();

                //split with ','
                string[] criteriacommavalues = paramstr.Split(',');
                foreach (string str in criteriacommavalues)
                {
                    commasep.Add(str);//add whole to  list
                }
                // for each nd split with '/'
                foreach (string str in commasep)
                {
                    if (str.Contains('/'))
                    {
                        string[] criteriabarvalues = str.Split('/');
                        foreach (string s in criteriabarvalues)
                        {
                            barsep.Add(s);//add whole to list
                        }
                    }
                    else
                        barsep.Add(str);
                }

                //chek for '-'
                foreach (string str in barsep)
                {
                    if (str.Contains('-'))
                    {
                        int start = 0, limit = 0;
                        string[] criteriaminvalues = str.Split('-');
                        {

                            try
                            {

                                if (criteriaminvalues.Length == 1)
                                {
                                    try
                                    {
                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                                    limit = start;
                                }
                                else
                                {
                                    try
                                    {
                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }//actually get min value of answer
                                    try
                                    {
                                        limit = Convert.ToInt32(criteriaminvalues[1]);
                                    }
                                    catch (Exception e) { System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }//actually get max value of answer;need to get max of choice no from item id and set limit
                                }
                                if (limit < start)//need to reverse if 9-7 comes
                                {
                                    int temp = limit;
                                    limit = start;
                                    start = temp;
                                }
                            }
                            catch { }

                            for (int ci = start; ci <= limit; ci++)
                            {
                                minsep.Add(ci.ToString());//add whole to list
                            }
                        }
                    }
                    else
                        minsep.Add(str);
                }
            }

            if (minsep.Count > noofchoices)
                return false;
            else return true;
        }
        public bool IsSystemKey(System.Windows.Input.KeyEventArgs key)
        {
            System.Diagnostics.Debug.WriteLine(key.Key);
            System.Diagnostics.Debug.WriteLine(key.SystemKey);
            if (key.Key >= System.Windows.Input.Key.F1 && key.Key <= System.Windows.Input.Key.F1)
            {
                return true;
            }
            if (key.Key == System.Windows.Input.Key.LeftShift || key.Key == System.Windows.Input.Key.RightShift || key.Key == System.Windows.Input.Key.LeftCtrl || key.Key == System.Windows.Input.Key.RightCtrl ||
               key.SystemKey == System.Windows.Input.Key.LeftAlt || key.SystemKey == System.Windows.Input.Key.RightAlt || key.Key == System.Windows.Input.Key.CapsLock ||
              key.Key == System.Windows.Input.Key.Tab || key.Key == System.Windows.Input.Key.Enter ||
              key.Key == System.Windows.Input.Key.NumLock || key.Key == System.Windows.Input.Key.Insert)
            {
                return true;
            }

            return false;
        }
        public System.Windows.Controls.DataGridCell GetCell(System.Windows.Controls.DataGrid gridnewvariable, int row, int column)
        {
            if (row >= 0)
            {
                DataGridRow gridrow = (DataGridRow)gridnewvariable.ItemContainerGenerator.ContainerFromIndex(row);
                if (gridrow != null)
                {
                    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(gridrow);
                    System.Windows.Controls.DataGridCell cell = (System.Windows.Controls.DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                    return cell;
                }
            }
            return null;
        }
        public System.Windows.Controls.DataGridRow GetRow(System.Windows.Controls.DataGrid gridnewvariable, int row, int column)
        {
            DataGridRow gridrow = (DataGridRow)gridnewvariable.ItemContainerGenerator.ContainerFromIndex(row);
            if (gridrow != null)
            {
                return gridrow;
            }
            return null;
        }
        public bool ValidateRange(string Contents, int minvalue, int maxvalue)
        {
            string[] SplitContents;
            Contents = TrimStartEqualNotequal(Contents);
            if (Contents.Contains("/") || Contents.Contains(","))
            {
                char[] splitchar = { '/', ',' };
                SplitContents = Contents.Split(splitchar);
            }
            else
            {
                SplitContents = new string[] { Contents };
            }
            foreach (string item in SplitContents)
            {
                Contents = item;
                if (Contents.StartsWith("-"))
                {
                    Contents = minvalue + Contents;
                }
                if (Contents.EndsWith("-"))
                {
                    Contents = Contents + maxvalue;
                }
                string[] SplitContent = Contents.Split('-');
                string[] splitExclusion = SplitContent[0].Split('!');
                try
                {
                    if (splitExclusion[1] != "")
                    {
                        SplitContent[0] = splitExclusion[1];
                    }
                }
                catch { }

                foreach (string itemm in SplitContent)
                {
                    try
                    {


                        float value = 0;
                        if (float.TryParse(itemm, out value))
                        {
                            if (value < minvalue || value > maxvalue)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception ex)
                    {

                        return false;
                    }
                }
            }
            return true;
        }
        public bool ValidateRegex(string Contents)//checking if Limits"-" is there
        {
            Contents = TrimStartEqualNotequal(Contents);
            Regex paranthesisRegEx = new Regex(@"^((-?\d+\.)?-?\d+)|([(-](\d+\.)?-?\d+[)])$");
            if (!paranthesisRegEx.Match(Contents).Success)
            {
                return false;
            }
            return true;
        }

        //^((\d+\.)?\d+)[-]$
        public bool ValidateRegexForNType(string Contents)//checking if Limits"-" is there
        {
            Contents = TrimStartEqualNotequal(Contents);
            Regex paranthesisRegEx = new Regex(@"^((\d+\.)?\d+)[-]$");
            if (paranthesisRegEx.Match(Contents).Success)
            {
                return false;
            }
            return true;
        }
        public bool CheckMultiplkeLimit_SAMA(string text)
        {
            int countofseperator = text.Count(f => (f == '-'));
            if (countofseperator > 1)
            {
                return true;
            }
            return false;
        }
    }
}
