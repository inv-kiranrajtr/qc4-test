using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelAddIn.Common;
using Macromill.QCWeb.Tabulation;
using Qc4Launcher.Util;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Logic.MultiVariate
{
    class CS_Portfolio_Settings
    {
        public static string MinMaxAppendWithMinus(string value, int row, int column, Excel.Worksheet ProcessSheet = null)
        {

            Excel.Range crange;

            crange = ProcessSheet.Cells[row, column];

            if (Definiotion.VariableDictionary.ContainsKey(crange.Text))
            {
                bool isnot = false;
                if (value.StartsWith("!"))
                {
                    isnot = true;
                    value = value.TrimStart('!');
                }
                string maxDoubleValue = "179769313486231570000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                string doubleMinValue = "-179769313486231570000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";

                if (value.StartsWith("-"))
                { value = (Definitions.VariableDictionary[crange.Text].CategoryCount == 0 ? doubleMinValue : "1") + value; }//Redmine id:177538 //Redmine id: 176455
                if (value.EndsWith("-"))
                { value = value + (Definitions.VariableDictionary[crange.Text].CategoryCount == 0 ? maxDoubleValue : Definitions.VariableDictionary[crange.Text].CategoryCount); }//Redmine id : 197237//(maxDoubleValue)
                if (isnot) value = "!" + value;
            }
            return value;
        }      
        public String GetCommaSeperated(string value, string quesvar)
        {
            string commaseperatedvalues = string.Empty;
            bool isnot = false;
            if (value.StartsWith("!") || value.StartsWith("<>"))
            {
                isnot = true;
            }
            if (value.StartsWith("!")) value = value.TrimStart('!');
            //else if (value.StartsWith("<>")) value = value.Replace("<>", "");//currently <>  omitted
            List<string> commasep = new List<string>();
            List<string> barsep = new List<string>();
            List<string> minsep = new List<string>();
            List<double> exclidelist = new List<double>();
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

                    int criteriaend = Definitions.VariableDictionary[quesvar].CategoryCount;
                    if (str.Contains('-'))
                    {
                        double strt = 0, end = 0;
                        string[] criterisplitvals = notvalue.Split('-');

                        if (criterisplitvals.Length == 1)
                        {
                            try
                            {
                                strt = Convert.ToDouble(criterisplitvals[0]);
                            }
                            catch (Exception e) { strt = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                            end = strt;

                        }
                        else
                        {
                            try
                            {
                                strt = Convert.ToDouble(criterisplitvals[0]);
                            }
                            catch (Exception e) { strt = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                            try
                            {
                                end = Convert.ToDouble(criterisplitvals[1]);
                            }
                            catch (Exception e)
                            {
                                end = Definitions.VariableDictionary[quesvar].CategoryCount;
                                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                            }
                        }

                        for (double ci = strt; ci <= end; ci++)
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
                            exclidelist.Add(Convert.ToDouble(str));
                        }
                        catch { }
                    }


                }
                else
                {
                    //else
                    if (str.Contains('-'))
                    {

                        double start = 0, limit = 0;
                        string[] criteriaminvalues = str.Split('-');
                        // foreach (string s in criteriaminvalues)
                        {

                            try
                            {

                                if (criteriaminvalues.Length == 1)
                                {
                                    try
                                    {
                                        start = Convert.ToDouble(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                                    limit = start;
                                }
                                else
                                {
                                    try
                                    {
                                        start = Convert.ToDouble(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }//actually get min value of answer
                                    try
                                    {
                                        limit = Convert.ToDouble(criteriaminvalues[1]);
                                    }
                                    catch (Exception e)
                                    {//actually get max value of answer;need to get max of choice no from item id and set limit
                                        limit = Definitions.VariableDictionary[quesvar].CategoryCount;
                                        System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                                    }
                                }
                                if (limit < start)//need to reverse if 9-7 comes
                                {
                                    double temp = limit;
                                    limit = start;
                                    start = temp;
                                }
                            }
                            catch { }

                            for (double ci = start; ci <= limit; ci++)
                            {
                                minsep.Add(ci.ToString());//add whole to list
                            }
                        }
                    }
                    else
                        minsep.Add(str);
                }
            }
            if (isnot)
            {
                for (int ci = 1; ci <= Definitions.VariableDictionary[quesvar].CategoryCount; ci++)
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
        /*
        public static string MinMaxAppendWithMinus(string value, int row, int column, Excel.Worksheet ProcessSheet = null)
        {

            Excel.Range crange;

            crange = ProcessSheet.Cells[row, column];

            if (Definiotion.VariableDictionary.ContainsKey(crange.Text))
            {
                bool isnot = false;
                if (value.StartsWith("!"))
                {
                    isnot = true;
                    value = value.TrimStart('!');
                }
                int catcount = Definiotion.VariableDictionary[crange.Text].CategoryCount == 0 ? int.MaxValue : Definiotion.VariableDictionary[crange.Text].CategoryCount;
                if (value.StartsWith("-"))
                { value = (Definiotion.VariableDictionary[crange.Text].CategoryCount == 0 ? (int.MinValue).ToString() : "1") + value; }
                if (value.EndsWith("-"))
                { value = value + catcount.ToString(); }
                if (isnot) value = "!" + value;
            }
            return value;
        }
        public String GetCommaSeperated(string value, string quesvar)
        {
            string commaseperatedvalues = string.Empty;
            bool isnot = false;
            if (value.StartsWith("!") || value.StartsWith("<>"))
            {
                isnot = true;
            }
            if (value.StartsWith("!")) value = value.TrimStart('!');
            //else if (value.StartsWith("<>")) value = value.Replace("<>", "");//currently <>  omitted
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

                    int criteriaend = Definiotion.VariableDictionary[quesvar].CategoryCount;
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
                                end = Definiotion.VariableDictionary[quesvar].CategoryCount;
                                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                            }
                        }

                        for (int ci = strt; ci <= end; ci++)
                        {
                            exclidelist.Add(ci);
                        }
                     
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
                                        limit = Definiotion.VariableDictionary[quesvar].CategoryCount;
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
            if (isnot)
            {
                for (int ci = 1; ci <= Definiotion.VariableDictionary[quesvar].CategoryCount; ci++)
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
        */
    }
}
