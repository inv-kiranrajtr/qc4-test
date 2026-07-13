using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using QC4Common.Model;

namespace ExcelAddIn.Common
{
    internal class FaValidates
    {
        internal static ReturnClass validate()
        {

            Worksheet sht = getFASheet();
            if (null == sht) return null;


            Range gtSettingRange = ExcelUtil.EndxlUp(sht.Range[Constants.FA.FAStartAddress]);
            if (gtSettingRange.Row <= Constants.FA.FARowDataStart)
            {
                return null;
            }


            ReturnClass returnClass = validateNarrow(sht);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }
            return new ReturnClass(true);
        }

        private static Worksheet getFASheet()
        {
            foreach (Worksheet sht in Globals.ThisAddIn.Application.Worksheets)
            {
                switch (sht.CodeName)
                {
                    case Common.Constants.SheetType.SH_FAList:
                        return sht;
                    default:
                        break;
                }

            }
            return null;
        }
        private static ReturnClass validateNarrow(Worksheet sht)
        {
            Range fASettingRange = findMaxAllocatedRange(sht, true, sht.Range[Constants.FA.FANarrowStartAddress], sht.Range[Constants.FA.FANarrowStartAddress].Offset[1, 0]);
            if (fASettingRange.Column <= Constants.FA.FAColInputStart)
            {
                return new ReturnClass(true);
            }
            fASettingRange = sht.Range[sht.Range[Constants.FA.FANarrowStartAddress], fASettingRange];

            foreach (Range targetCell in fASettingRange
                )
            {
                Range variable = targetCell;
                Range narrowStr = targetCell.Offset[1, 0];

                if (null == narrowStr.Value2)
                {
                    continue;
                }
                string chartStr = narrowStr.Value2.ToString();
                if (string.IsNullOrEmpty(chartStr))
                {
                    continue;
                }

                if (!validateVariable(variable, false))
                {
                    return new ReturnClass(false, variable, AddinResource.CR_NARROW_VARIABLE_INVALID, new string[] { variable.Column.ToString() });
                }

                if (null == variable.Value2)
                {
                    return new ReturnClass(false, narrowStr, AddinResource.CR_NARROW_CRITERIA_INVALID, new string[] { narrowStr.Column.ToString() });
                }
                string variableName = variable.Value2.ToString();
                if (string.IsNullOrEmpty(variableName))
                {
                    return new ReturnClass(false, narrowStr, AddinResource.CR_NARROW_CRITERIA_INVALID, new string[] { narrowStr.Column.ToString() });
                }

                QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];

                if (!ValidateCell(narrowStr, 1, questionDetails.CategoryCount))
                {
                    return new ReturnClass(false, narrowStr, AddinResource.CR_NARROW_CRITERIA_INVALID, new string[] { narrowStr.Column.ToString() });
                }
            }
            return new ReturnClass(true);
        }
        public static bool validateVariable(Range variable, bool nTypeInclude = true)
        {
            if (null == variable.Value2)
            {
                return true;
            }
            string variableName = variable.Value2.ToString();
            if (string.IsNullOrEmpty(variableName))
            {
                return true;
            }
            if (!Definitions.VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }
            QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];
            if (questionDetails.AnswerType == Constants.AnswerType.SA
                || questionDetails.AnswerType == Constants.AnswerType.MA
                || (nTypeInclude && questionDetails.AnswerType == Constants.AnswerType.N)
                )
            {
                return true;
            }
            return false;
        }
        private static bool ValidateCell(Range range, int minvalue, int maxvalue)
        {
            if (range.Text == "")
            {
                return true;
            }

            string[] SplitContent;
            string Contents = range.Text;
            if (Contents.Contains("/") || Contents.Contains(","))
            {
                char[] splitchar = { '/', ',' };
                SplitContent = Contents.Split(splitchar);
                foreach (string item in SplitContent)
                {
                    try
                    {

                        if (item.Contains("-"))
                        {
                            if (!ValidateRange(item, minvalue, maxvalue)) { return false; }
                        }
                        else
                        {
                            int value = Convert.ToInt32(item);
                            if (value < minvalue || value > maxvalue)
                            {
                                return false;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        return false;
                    }
                }
            }
            else
            {
                if (!ValidateRange(range.Text, minvalue, maxvalue)) { return false; }
            }
            return true;
        }
        private static bool ValidateRange(string Contents, int minvalue, int maxvalue)
        {
            string[] SplitContent;
            SplitContent = Contents.Split('-');
            foreach (string item in SplitContent)
            {
                try
                {
                    int value = Convert.ToInt32(item);
                    if (value < minvalue || value > maxvalue)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
        public static Range findMaxAllocatedRange(Worksheet sht, bool column, params Range[] args)
        {
            int large = 0;
            int start = 0;
            foreach (Range range in args)
            {
                if (column)
                {
                    Range fASettingRange = ExcelUtil.EndxlRight(range);
                    if (fASettingRange.Column > large)
                    {
                        large = fASettingRange.Column;
                    }
                    if (start == 0)
                    {
                        start = fASettingRange.Row;
                    }

                }
                else
                {

                    Range faSettingRange = ExcelUtil.EndxlUp(range);
                    if (faSettingRange.Row > large)
                    {
                        large = faSettingRange.Row;
                    }
                    if (start == 0)
                    {
                        start = faSettingRange.Column;
                    }

                }

            }
            if (column) { return sht.Cells[start, large]; } else { return sht.Cells[large, start]; }
        }
    }
}
