using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb = Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using QC4Common.Model;

namespace QC4Common.Validation
{
    public class NumberCheck
    {

        public static string CheckNotOperator(string value, string operatorr, string type = "")
        {
            if (operatorr != "" && (Vb.Strings.Left(value, 1) == "*" || Vb.Strings.Left(value, 2) == "DK"))
            {
                if ((operatorr != "=" && operatorr != "<>"))
                    return "1";
            }
            if (value.Contains('!') && type == "MA")
            {
                value = Regex.Replace(value, @"\s+", "");
                if ((value.IndexOf('!') != 0 || value.Count(x => x == '!') > 1))
                {
                    return "<>";
                }
            }
            if (value.Contains('!'))
            {
                value = Regex.Replace(value, @"\s+", "");
                if ((value.IndexOf('!') != 0 || value.Count(x => x == '!') > 1))
                {
                    return "2";
                }
            }
            if (value.Length == 1 && value.IndexOf('!') == 0 && type != "FA")
            {
                return "!1";
            }
            return "";
        }

        public static bool Check(string value, int count)
        {
            if (Vb.Strings.Left(value, 1) == "=")
            {
                return false;
                //value = Vb.Strings.Mid(value, 2);
            }
            if (Vb.Strings.Left(value, 2) == "<>")
            {
                return false;
                //value = Vb.Strings.Mid(value, 3);
            }
            return NumberCheckFn(value, count, 1);
        }
        public static bool CheckFromOption(string value, int count, string type, string operatr)
        {
            if (Vb.Strings.Left(value, 1) == "=")
            {
                return false;
                //value = Vb.Strings.Mid(value, 2);
            }
            if (Vb.Strings.Left(value, 2) == "<>")
            {
                return false;
                //value = Vb.Strings.Mid(value, 3);
            }
            return NumberCheckFn2(value, count, 1, type: type, operatr: operatr);
        }

        public static bool NUmberCheckSubtotal(string value, int count)
        {
            if (Vb.Strings.Left(value, 1) == "=")
            {
                value = value.Remove(0, 1);
            }
            if (Vb.Strings.Left(value, 2) == "<>")
            {
                value = value.Remove(0, 2);
            }
            return NumberCheckFn(value, count, 1);
        }

        public static bool NUmberCheckNarrow(string value, int count)
        {
            if (Vb.Strings.Left(value, 1) == "=")
            {
                value = value.Remove(0, 1);
            }
            if (Vb.Strings.Left(value, 2) == "<>")
            {
                value = value.Remove(0, 2);
            }
            return NumberCheckFn3(value, count, 1);
        }

        public static string Error_Mesage = "";
        private static bool NumberCheckFn2(string value, int maxVal = 0, int minVal = 1, bool blankCheck = false, bool numberObly = false, bool noSplit = false, string type = "", string operatr = "")
        {
            string val = Regex.Replace(value, @"\s+", "").Replace('/', ',');
            if (Vb.Information.IsNumeric(val) && (type == "SA" || type == "MA"))
            {
                double data = 0;
                if (double.TryParse(val, out data))
                {
                    if ((data % 1 != 0)) { return false; }
                }
                else if (val.Contains("."))
                {
                    return false;
                }
            }
            if (val.Contains("-)") && type != "FA")
                return false;
            if (operatr == "=" || operatr == "<>")
            {
                string[] commaSplit = val.Replace("!", "").Split(',');
                decimal parentFirstVal = 0;
                decimal parentSecondVal = 0;
                if (commaSplit.Length > 1)
                {
                    for (int i = 0; i < commaSplit.Length; i++)
                    {
                        decimal? childFirstVal = null;
                        decimal? childSecondVal = null;
                        string[] iphanSplit = commaSplit[i].Split('-');
                        int nullCount = 0;
                        for (int j = 0; j < iphanSplit.Length; j++)
                        {
                            if (iphanSplit[j] == "")
                                nullCount++;
                            if (nullCount > 1)
                                return false;
                            if (iphanSplit[j] == "")
                                return false;
                            if (iphanSplit[j].Contains('(') && iphanSplit[j].Contains(')'))
                                return false;
                            if ((iphanSplit[j].Contains('(') && iphanSplit[j].LastIndexOf('(') != 0) ||
                            (iphanSplit[j].Contains(')') && iphanSplit[j].IndexOf(')') != iphanSplit[j].Length - 1))
                                return false;
                            if ((iphanSplit[j].Contains('(') && (iphanSplit.Length < j + 2 || !iphanSplit[j + 1].Contains(')')))
                                || (iphanSplit[j].Contains(')') && (j == 0 || !iphanSplit[j - 1].Contains('('))))
                                return false;
                        }
                        string rplcAt = commaSplit[i].Replace('(', '@').Replace(')', '@');
                        string[] atSplit = rplcAt.Split('@');
                        if (!rplcAt.Contains("@"))
                        {
                            string[] split = commaSplit[i].Split('-');
                            if (split.Length >= 2 && split[0] != "" && split[1] != "")
                                atSplit = split;
                        }
                        bool isRangeValue = false;
                        for (int n = 0; n < atSplit.Length; n++)
                        {
                            if (atSplit[n] != "" && atSplit[n] != "-")
                            {
                                if (atSplit[n].LastIndexOf('-') == atSplit[n].Length - 1)
                                {
                                    atSplit[n] = atSplit[n].Remove(atSplit[n].Length - 1);
                                    isRangeValue = true;
                                }
                                if (!Vb.Information.IsNumeric(atSplit[n]))
                                {
                                    return false;
                                }
                                else
                                {
                                    decimal valu = Convert.ToDecimal(atSplit[n]);
                                    if ((atSplit[n].Contains('-') && childFirstVal != null && atSplit[n - 1] != "-" && !isRangeValue && valu < 0) ||
                                        (atSplit.Length == 1 && valu < 0))
                                        valu = valu * -1;
                                    if (type != "N" && valu < 0)
                                        return false;
                                    if (0 < maxVal)
                                    {
                                        if (valu < minVal)
                                        {
                                            return false;
                                        }

                                        if (maxVal < valu)
                                        {
                                            return false;
                                        }

                                    }
                                    if (childFirstVal != null && childSecondVal != null)
                                        return false;
                                    if (childFirstVal == null)
                                        childFirstVal = valu;
                                    else if (childSecondVal == null)
                                        childSecondVal = valu;
                                    if (parentFirstVal == 0)
                                        parentFirstVal = valu;
                                    else if (parentSecondVal == 0)
                                        parentSecondVal = valu;
                                    else
                                    {
                                        parentFirstVal = parentSecondVal;
                                        parentSecondVal = valu;
                                    }
                                    if (parentSecondVal != 0 && parentFirstVal >= parentSecondVal)
                                        return false;
                                    if (childSecondVal != 0 && childFirstVal >= childSecondVal)
                                        return false;
                                }
                            }
                        }
                    }
                }
                else if (commaSplit.Length == 1)
                {
                    decimal? childFirstVal = null;
                    decimal? childSecondVal = null;
                    string[] iphanSplit = commaSplit[0].Split('-');
                    int nullCount = 0;
                    for (int j = 0; j < iphanSplit.Length; j++)
                    {
                        if (iphanSplit[j] == "")
                            nullCount++;
                        if (nullCount > 1)
                            return false;
                        if (iphanSplit[j] == "" && (j != 0 && j != iphanSplit.Length - 1))
                            return false;
                        if (iphanSplit[j].Contains('(') && iphanSplit[j].Contains(')'))
                            return false;
                        if ((iphanSplit[j].Contains('(') && iphanSplit[j].LastIndexOf('(') != 0) ||
                        (iphanSplit[j].Contains(')') && iphanSplit[j].IndexOf(')') != iphanSplit[j].Length - 1))
                            return false;
                        if ((iphanSplit[j].Contains('(') && (iphanSplit.Length < j + 2 || !iphanSplit[j + 1].Contains(')')))
                            || (iphanSplit[j].Contains(')') && (j == 0 || !iphanSplit[j - 1].Contains('('))))
                            return false;
                    }
                    string rplcAt = commaSplit[0].Replace('(', '@').Replace(')', '@');
                    string[] atSplit = rplcAt.Split('@');
                    if (!rplcAt.Contains("@"))
                    {
                        string[] split = commaSplit[0].Split('-');
                        if (split.Length >= 2 && split[0] != "" && split[1] != "")
                            atSplit = split;
                    }
                    bool isRangeValue = false;
                    for (int n = 0; n < atSplit.Length; n++)
                    {
                        if (atSplit[n] != "" && atSplit[n] != "-")
                        {
                            if (atSplit[n].LastIndexOf('-') == atSplit[n].Length - 1)
                            {
                                atSplit[n] = atSplit[n].Remove(atSplit[n].Length - 1);
                                isRangeValue = true;
                            }
                            if (!Vb.Information.IsNumeric(atSplit[n]))
                            {
                                return false;
                            }
                            else
                            {
                                decimal valu = Convert.ToDecimal(atSplit[n]);
                                if ((atSplit[n].Contains('-') && childFirstVal != null && atSplit[n - 1] != "-" && !isRangeValue && valu < 0) ||
                                    (atSplit.Length == 1 && valu < 0))
                                    valu = valu * -1;
                                if (type != "N" && valu < 0)
                                    return false;
                                if (0 < maxVal)
                                {
                                    if (valu < minVal)
                                    {
                                        return false;
                                    }

                                    if (maxVal < valu)
                                    {
                                        return false;
                                    }

                                }
                                if (childFirstVal != null && childSecondVal != null)
                                    return false;
                                if (childFirstVal == null)
                                    childFirstVal = valu;
                                else if (childSecondVal == null)
                                    childSecondVal = valu;
                                if (childSecondVal != 0 && childFirstVal > childSecondVal)
                                    return false;

                                if (parentFirstVal == 0)
                                    parentFirstVal = valu;
                                else
                                    parentSecondVal = valu;
                                if (parentSecondVal != 0 && parentFirstVal >= parentSecondVal)
                                    return false;
                            }
                        }
                    }
                }
            }
            else
            {
                if (type == "N")
                {
                    if ((val.Contains('(') || val.Contains(')'))
                        && ((val.IndexOf('(') != 0 || val.IndexOf(')') != val.Length - 1) || (!val.Contains('-'))))
                        return false;
                    if (val.Contains('-') && (!val.Contains('(') || !val.Contains(')')))
                    {
                        Error_Mesage = "ERR_MSG_DATAEXPORT_SET_NUMERIC";
                        return false;
                    }
                    string valu = val.Replace("(", "").Replace(")", "");
                    if (valu.Contains('-') && valu.IndexOf('-') != 0)
                        return false;
                    if (!Vb.Information.IsNumeric(valu))
                    {
                        return false;
                    }
                }
                else if (type == "SA" || type == "MA")
                {
                    if (!Vb.Information.IsNumeric(val))
                        return false;

                    if (Convert.ToDecimal(val) <= 0)
                        return false;

                    if (Convert.ToDecimal(val) > maxVal)
                        return false;
                }
                else
                {
                    if (!Vb.Information.IsNumeric(val))
                    {
                        return false;
                    }
                    else
                    {
                        if (Convert.ToDecimal(val) < 0)
                            return false;
                    }
                }
            }
            return true;
        }

        //private static bool NumberCheckFn2(string value, int maxVal = 0, int minVal = 1, bool blankCheck = false, bool numberObly = false, bool noSplit = false, string type = "")
        //{
        //    string[] dataArray1;
        //    string[] dataArray2;
        //    int nullCount;
        //    bool isNegetive = false;
        //    value = Regex.Replace(value, @"\s+", "");
        //    if (type == "N")
        //    {
        //        string val = value.Replace("!", "").Replace("/", ",");
        //        if (val.Contains("-)"))
        //            return false;
        //        string[] valAry = val.Split(',');
        //        for (int i = 0; i < valAry.Length; i++)
        //        {
        //            if (valAry[i].Count(x => x == '-') > 1 && !valAry[i].Contains("("))
        //                return false;
        //            if (Vb.Information.IsNumeric(valAry[i]))
        //            {

        //            }
        //            else
        //            {
        //                int firstNum = 0;
        //                int secondNum = 0;
        //                string otherVal = valAry[i].Replace("(", "@").Replace(")", "@");
        //                if (!otherVal.Contains("-"))
        //                    return false;
        //                string[] spl = otherVal.Split('-');
        //                string num = "";
        //                for (int j = 0; j < spl.Length; j++)
        //                {
        //                    if (spl[j] == "")
        //                        return false;
        //                    if (spl[j].Contains("@") && (spl[j].IndexOf("@") != 0 && spl[j].LastIndexOf("@") != spl[j].Length - 1))
        //                        return false;
        //                    num += spl[j] == "@" ? "-" : spl[j].Replace("@", "");
        //                    if (spl.Length > 2)
        //                    {
        //                        if (Vb.Information.IsNumeric(num))
        //                        {
        //                            if (firstNum == 0)
        //                            {
        //                                firstNum = Convert.ToInt32(num);
        //                                num = "";
        //                            }
        //                            else if (secondNum == 0)
        //                            {
        //                                secondNum = Convert.ToInt32(num);
        //                                if (firstNum > secondNum)
        //                                    return false;
        //                                num = "";
        //                                if (firstNum < 0 || secondNum < 0)
        //                                    isNegetive = true;
        //                            }
        //                            else
        //                                num = "wrong";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (num != "-" && !Vb.Information.IsNumeric(num))
        //                            return false;
        //                        else if (Vb.Information.IsNumeric(num))
        //                            num = "";
        //                    }
        //                    if (num != "" && num != "-")
        //                        return false;
        //                }
        //            }
        //        }
        //    }
        //    value = Vb.Strings.Replace(value, ",", "/");
        //    if (value == "!")
        //    {
        //        return false;
        //    }

        //    if (Vb.Strings.Left(value, 1) == "!")
        //    {
        //        value = Vb.Strings.Mid(value, 2);
        //    }

        //    if (Vb.Strings.InStr(1, value, "!") > 0)
        //    {
        //        return false;
        //    }

        //    if (blankCheck && value == "")
        //    {
        //        return false;
        //    }

        //    if (numberObly && value != "" && Vb.Information.IsNumeric(value))
        //    {
        //        return false;
        //    }

        //    if (noSplit && value != "" && Vb.Strings.InStr(1, value, "/") > 0)
        //    {
        //        return false;
        //    }

        //    dataArray1 = value.Split('/');
        //    string findData1;
        //    string findData2;
        //    foreach (string str in dataArray1)
        //    {
        //        findData1 = str;
        //        if (blankCheck && findData1 == "")
        //        {
        //            return false;
        //        }

        //        nullCount = 0;
        //        if (type != "N")
        //            findData1 = Vb.Strings.Replace(findData1, "(-", "@");
        //        findData1 = Vb.Strings.Replace(findData1, "(", "");
        //        findData1 = Vb.Strings.Replace(findData1, ")", "");
        //        findData1 = Vb.Strings.Replace(findData1, "-", ",");
        //        findData1 = Vb.Strings.Replace(findData1, "(@", "-");

        //        if (type != "N" && String.IsNullOrEmpty(findData1))
        //        {
        //            return false;
        //        }
        //        dataArray2 = Vb.Strings.Split(findData1, ",");
        //        if (type != "N" && Vb.Information.UBound(dataArray2) > 1)
        //        {
        //            return false;
        //        }
        //        double firstVal = 0;
        //        double secondVal = 0;
        //        foreach (string str2 in dataArray2)
        //        {
        //            findData2 = str2;
        //            if (findData2.Trim() != "")
        //            {
        //                if (!Vb.Information.IsNumeric(findData2))
        //                {
        //                    return false;
        //                }
        //                else
        //                {
        //                    double d = Vb.Conversion.Val(findData2);
        //                    if (0 < maxVal)
        //                    {
        //                        if (d < minVal)
        //                        {
        //                            return false;
        //                        }

        //                        if (maxVal < d)
        //                        {
        //                            return false;
        //                        }

        //                        if (d != Vb.Conversion.Int(d))
        //                        {
        //                            return false;
        //                        }
        //                        findData2 = d.ToString();
        //                    }
        //                    if (type == "N")
        //                    {
        //                        if (firstVal == 0)
        //                            firstVal = d;
        //                        else
        //                            secondVal += d;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (numberObly)
        //                {
        //                    return false;
        //                }
        //                nullCount += 1;
        //                if (nullCount > 1)
        //                {
        //                    if (type == "N" && (value.Count(x => x == '-') == 2 && !value.Contains("-(-") && !value.Contains("-)-")
        //                        && !value.Contains("-(") && !value.Contains("(-") && !value.Contains(")-") && !value.Contains("-)") ||
        //                        (value.Contains("-(-)") || value.Contains("(-)-") || value.Contains("(-)") || value.Contains("()-") || value.Contains("-()")
        //                        )))
        //                        return false;
        //                    if (type != "N")
        //                        return false;
        //                }
        //            }
        //        }

        //        if (!isNegetive && secondVal != 0 && firstVal > secondVal)
        //            return false;

        //    }
        //    return true;
        //}
        /// <summary>
        /// Check whether the string values contains are valid numbers or not
        /// </summary>
        /// <param name="value">string values</param>
        /// <param name="maxVal">Max value</param>
        /// <param name="minVal">Minimum value</param>
        /// <param name="blankCheck">bool value that represents whether check empty or not</param>
        /// <param name="numberObly">bool value that represents wthether check only numbers or not</param>
        /// <param name="noSplit">bool value that represents wthether values contains split or not</param>
        /// <returns>Return bool value that represents the values are valid or not</returns>
        private static bool NumberCheckFn(string value, int maxVal = 0, int minVal = 1, bool blankCheck = false, bool numberObly = false, bool noSplit = false)
        {
            string[] dataArray1;
            string[] dataArray2;
            int nullCount;

            value = Vb.Strings.Replace(value, ",", "/");
            if (value == "!")
            {
                return false;
            }

            if (Vb.Strings.Left(value, 1) == "!")
            {
                value = Vb.Strings.Mid(value, 2);
            }

            if (Vb.Strings.InStr(1, value, "!") > 0)
            {
                return false;
            }

            if (blankCheck && value == "")
            {
                return false;
            }

            if (numberObly && value != "" && Vb.Information.IsNumeric(value))
            {
                return false;
            }

            if (noSplit && value != "" && Vb.Strings.InStr(1, value, "/") > 0)
            {
                return false;
            }

            dataArray1 = value.Split('/');
            string findData1;
            string findData2;
            foreach (string str in dataArray1)
            {
                findData1 = str;
                if (blankCheck && findData1 == "")
                {
                    return false;
                }

                nullCount = 0;
                findData1 = Vb.Strings.Replace(findData1, "(", "");
                findData1 = Vb.Strings.Replace(findData1, ")", "");
                findData1 = Vb.Strings.Replace(findData1, "-", ",");
                findData1 = Vb.Strings.Replace(findData1, "(@", "-");

                dataArray2 = Vb.Strings.Split(findData1, ",");
                foreach (string str2 in dataArray2)
                {
                    findData2 = str2;
                    if (findData2.Trim() != "")
                    {
                        if (!Vb.Information.IsNumeric(findData2) || CheckSubtotalContainsFullWidth(findData2))
                        {
                            return false;
                        }
                        else
                        {
                            double d = Vb.Conversion.Val(findData2);
                            if (0 < maxVal)
                            {
                                if (d < minVal)
                                {
                                    return false;
                                }

                                if (maxVal < d)
                                {
                                    return false;
                                }

                                if (d != Vb.Conversion.Int(d))
                                {
                                    return false;
                                }
                                findData2 = d.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (numberObly)
                        {
                            return false;
                        }
                        nullCount += 1;
                        if (nullCount > 1)
                        {
                            return false;
                        }
                    }
                }

            }
            return true;
        }
        /// <summary>
        /// Check whether the value is full width or not
        /// </summary>
        /// <param name="value">a string value</param>
        /// <returns>return a bool value that represents whether the value is full width or not</returns>
        private static bool CheckSubtotalContainsFullWidth(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i].GetHashCode() < 0 || value[i] == '　')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool NumberCheckFn3(string value, int maxVal = 0, int minVal = 1, bool blankCheck = false, bool numberObly = false, bool noSplit = false)
        {
            string[] dataArray1;
            string[] dataArray2;
            int nullCount;

            value = Vb.Strings.Replace(value, ",", "/");
            if (value == "!")
            {
                return false;
            }

            if (Vb.Strings.Left(value, 1) == "!")
            {
                value = Vb.Strings.Mid(value, 2);
            }

            if (Vb.Strings.InStr(1, value, "!") > 0)
            {
                return false;
            }

            if (blankCheck && value == "")
            {
                return false;
            }

            if (numberObly && value != "" && Vb.Information.IsNumeric(value))
            {
                return false;
            }

            if (noSplit && value != "" && Vb.Strings.InStr(1, value, "/") > 0)
            {
                return false;
            }

            dataArray1 = value.Split('/');
            string findData1;
            string findData2;
            foreach (string str in dataArray1)
            {
                findData1 = str;
                if (blankCheck && findData1 == "")
                {
                    return false;
                }

                nullCount = 0;
                findData1 = Vb.Strings.Replace(findData1, "(", "");
                findData1 = Vb.Strings.Replace(findData1, ")", "");
                findData1 = Vb.Strings.Replace(findData1, "-", ",");
                findData1 = Vb.Strings.Replace(findData1, "(@", "-");

                dataArray2 = Vb.Strings.Split(findData1, ",");
                foreach (string str2 in dataArray2)
                {
                    findData2 = str2;
                    if (findData2.Trim() != "")
                    {
                        if (!Vb.Information.IsNumeric(findData2))
                        {
                            if (value.Trim() != "*" && value.Trim() != "DK")
                            {
                                return false;
                            }
                        }
                        else
                        {
                            double d = Vb.Conversion.Val(findData2);
                            if (0 < maxVal)
                            {
                                if (d < minVal)
                                {
                                    return false;
                                }

                                if (maxVal < d)
                                {
                                    return false;
                                }

                                if (d != Vb.Conversion.Int(d))
                                {
                                    return false;
                                }
                                findData2 = d.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (numberObly)
                        {
                            return false;
                        }
                        nullCount += 1;
                        if (nullCount > 1)
                        {
                            return false;
                        }
                    }
                }

            }
            return true;
        }

        public static string CheckValueAgainstOP(string value, string operatorr, string variabletype)
        {

            if (operatorr != "=" && operatorr != "<>")
            {
                if (value.Contains(','))
                    return ",";
                if (value.Contains('/'))
                    return ",";
                if (value.Contains('-') && variabletype != Common.Constants.AnswerType.N)
                    return ",";
                if (value.Contains('!'))
                    return "<>";
            }
            if (operatorr != "=" && variabletype != Common.Constants.AnswerType.FA)
            {
                if (value.Contains('!'))
                    return "!";
            }
            return "";
        }

        public static bool CheckRange(string textvalue, int categorycount, string type, string operatr)
        {
            int minVal = 1;
            string val = Regex.Replace(textvalue, @"\s+", "").Replace('/', ',');
            if (operatr == "=" || operatr == "<>")
            {
                string[] commaSplit = val.Replace("!", "").Split(',');
                if (commaSplit.Length > 1)
                {
                    for (int i = 0; i < commaSplit.Length; i++)
                    {
                        string rplcAt = commaSplit[i].Replace('(', '@').Replace(')', '@');
                        string[] atSplit = rplcAt.Split('@');
                        if (!rplcAt.Contains("@"))
                        {
                            string[] split = commaSplit[i].Split('-');
                            if (split.Length >= 2 && split[0] != "" && split[1] != "")
                                atSplit = split;
                            for (int n = 0; n < atSplit.Length; n++)
                            {
                                if (atSplit[n] != "" && atSplit[n] != "-")
                                {
                                    if (atSplit[n].LastIndexOf('-') == atSplit[n].Length - 1)
                                    {
                                        atSplit[n] = atSplit[n].Remove(atSplit[n].Length - 1);
                                    }
                                    if (Vb.Information.IsNumeric(atSplit[n]))
                                    {
                                        decimal valu = Convert.ToDecimal(atSplit[n]);
                                        if ((atSplit.Length == 1 && valu < 0))
                                            valu = valu * -1;
                                        if (0 < categorycount)
                                        {
                                            if (valu < minVal)
                                            {
                                                return false;
                                            }

                                            if (categorycount < valu)
                                            {
                                                return false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (commaSplit.Length == 1)
                {
                    string rplcAt = commaSplit[0].Replace('(', '@').Replace(')', '@');
                    string[] atSplit = rplcAt.Split('@');
                    if (!rplcAt.Contains("@"))
                    {
                        string[] split = commaSplit[0].Split('-');
                        if (split.Length >= 2 && split[0] != "" && split[1] != "")
                            atSplit = split;
                    }
                    for (int n = 0; n < atSplit.Length; n++)
                    {
                        if (atSplit[n] != "" && atSplit[n] != "-")
                        {
                            if (atSplit[n].LastIndexOf('-') == atSplit[n].Length - 1)
                            {
                                atSplit[n] = atSplit[n].Remove(atSplit[n].Length - 1);
                            }
                            if (Vb.Information.IsNumeric(atSplit[n]))
                            {
                                decimal valu = Convert.ToDecimal(atSplit[n]);
                                if ((atSplit.Length == 1 && valu < 0))
                                    valu = valu * -1;
                                if (0 < categorycount)
                                {
                                    if (valu < minVal)
                                    {
                                        return false;
                                    }

                                    if (categorycount < valu)
                                    {
                                        return false;
                                    }
                                }

                            }
                        }
                    }
                }
            }
            else if (operatr != "=" && operatr != "<>")
            {
                if (Vb.Information.IsNumeric(val))
                {
                    decimal valu = Convert.ToDecimal(val);
                    if (0 < categorycount)
                    {
                        if (valu < minVal)
                        {
                            return false;
                        }

                        if (categorycount < valu)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

    }
}
