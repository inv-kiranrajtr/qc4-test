using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QC4Common.Classes
{
    public class Help
    {
        /// <summary>Gets the help link.</summary>
        /// <param name="section">The section.</param>
        /// <returns></returns>
        public static string GetHelpLink(string section)
        {
            string helpLink = "";
            switch(section)
            {
                case QC4Common.Common.Constants.HelpButtonType.MULTIVARIATE:
                    helpLink = "https://www.macromill.com/tabulation/faq/08-multivariate-analysis/";
                    break;
                case QC4Common.Common.Constants.HelpButtonType.MAINWINDOW:
                    helpLink = "https://www.macromill.com/tabulation/faq/";
                    break;
                case QC4Common.Common.Constants.HelpButtonType.DATAPROCESSING:
                    helpLink = "https://www.macromill.com/tabulation/faq/03-data-processing/";
                    break;
                case QC4Common.Common.Constants.HelpButtonType.RECODE:
                    helpLink = "https://www.macromill.com/tabulation/faq/03-data-processing/recode";
                    break;
                case QC4Common.Common.Constants.HelpButtonType.INTEGRATE:
                    helpLink = "https://www.macromill.com/tabulation/faq/03-data-processing/integrate";
                    break;
                case QC4Common.Common.Constants.HelpButtonType.MCONVERT:
                    helpLink = "https://www.macromill.com/tabulation/faq/03-data-processing/mconvert";
                    break;
                case QC4Common.Common.Constants.HelpButtonType.CLASS:
                    helpLink = "https://www.macromill.com/tabulation/faq/03-data-processing/class";
                    break;
                case QC4Common.Common.Constants.HelpButtonType.DATAMERGE:
                    helpLink = "https://www.macromill.com/tabulation/faq/07-data-capture/";
                    break;
                default:
                    helpLink = "https://www.macromill.com/tabulation/faq/";
                    break;
            }
            return helpLink;
        }

        /// <summary>Removes the CRLF characters.</summary>
        /// <param name="text">The text contains CRLF characters.</param>
        /// <returns></returns>
        public static string RemoveCRLFCharacters(string text)
        {
            text = text.Replace("\n", "");
            text = text.Replace("\r", "");

            return text;
        }
        public static string GetCriteriaValue(int[] a, int criteriaCount)
        {
            int j = 0;
            Array.Sort(a);
            string startValue = a[0].ToString();
            string val1 = "";//startValue;
            string val2 = startValue;
            for (int k = 0; k < a.Length; k++)
            {
                List<int> startVal = new List<int>();
                int s = k;
                for (int i = s; i < criteriaCount - 1 && a[i] + 1 == a[k + 1]; i++, k++)
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
    }
}
