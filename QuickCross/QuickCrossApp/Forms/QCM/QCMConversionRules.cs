using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Qc4Launcher.Forms.QCM
{
    class QCMConversionRules
    {
        public static List<String> excludedChar = new List<string> {"\t","=", "＝","'","’", "@", "＠","!","！","&","＆","~","～","|","｜","\\", "￥","[","［","]", "］","`","｀", "*","＊",":","：","<","＜",">","＞","/","／","?","？" };

        public static bool ConversionRuleValidate(string itemName)
        {
            string cr_lf_crlf = "/\n\r ?/ g";
            Regex regex = new Regex(cr_lf_crlf);
            if (regex.IsMatch(itemName))
                return false;

            if (!IsDigitsOnly(itemName))
                return false;

            int count = itemName.Count();
            foreach (string excludChar in excludedChar)
            {
                if (StartWith(itemName, excludChar, count))
                {
                    return false;
                }
            }
            return true;
        }

        public static string ReConvertProcess(string itemName)
        {
            //if (itemName.Contains("\"\""))
            //    itemName = itemName.Replace("\"\"", "\"");

            itemName = itemName.Replace("<LF>", "\r\n")
                               .Replace("<TAB>", "\t")
                               .Replace("，",",");
            return itemName;
        }

        public static string ConvertCommaArrow(string itemName)
        {
            itemName = itemName.Replace("<COMMA>", ",")
                               .Replace("←→", "\r\n");
            return itemName;
        }

        public static string Add_HalfWidthSpace_SingleQutoation(String itemName)
        {
            if (itemName.Length > 0)
            {
                char FirstCharctr = itemName[0];
                if ((FirstCharctr == '’') || (FirstCharctr == '\''))
                    itemName = " " + itemName;
            }
            return itemName;
        }

        public static bool StartWith(String value, String findString, int count)
        {
            bool val = value.Substring(0, count).Contains(findString);
            return val;
        }

        public static bool IsDigitsOnly(string str)
        {
            try
            {
                int count = str.Count();
                int countChar = 0;
                foreach (char c in str)
                {
                    if (!(c < '0' || c > '9'))
                        countChar++;
                }
                if (countChar == count)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
