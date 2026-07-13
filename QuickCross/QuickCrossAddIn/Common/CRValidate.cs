using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using System;

namespace ExcelAddIn.Common
{
    public class CRValidate
    {
        internal static ReturnClass validate()
        {
            Worksheet sht = getCRSheet();
            if (null == sht) return null;

            ReturnClass returnClass = validateNarrow(sht);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateAxis3WayCrossVAraible(sht, Constants.CR.CR3WayCrossStartAddress, true);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateAxis3WayCrossVAraible(sht, Constants.CR.CRAxesVariableStartAddress);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateChartSetting(sht);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateGTsettings(sht);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateRowDivSection(sht);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateVariableSection(sht);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            return new ReturnClass(true);
        }

        private static ReturnClass validateChartSetting(Worksheet sht)
        {
            Range cRSettingRange = ExcelUtil.EndxlRight(sht.Range[Constants.CR.CRChartStartAddress]);
            if (cRSettingRange.Column <= Constants.CR.CRColInputStart)
            {
                return new ReturnClass(true);
            }
            cRSettingRange = sht.Range[sht.Range[Constants.CR.CRChartStartAddress], cRSettingRange];

            foreach (Range targetCell in cRSettingRange.Cells)
            {
                Range chart = targetCell;
                Range variable = targetCell.Offset[-3, 0];

                if (null == chart.Value2)
                {
                    continue;
                }
                string chartStr = chart.Value2.ToString();
                if (string.IsNullOrEmpty(chartStr))
                {
                    continue;
                }

                Range variable3Way = variable.Offset[-3, 0];
                if (null != variable3Way.Value2)
                {
                    return new ReturnClass(false, chart, "Can't set 3-way cross and line chart simultaneously. col{0}", new string[] { chart.Column.ToString() });
                }

                if (!validateVariable(variable, false))
                {
                    return new ReturnClass(false, variable, AddinResource.CR_AXES_VARIABLE_INVALID, new string[] { variable.Column.ToString() });
                }

                if (null == variable.Value2)
                {
                    return new ReturnClass(false, chart, AddinResource.CR_AXES_VARIABLE_INVALID, new string[] { chart.Column.ToString() });
                }
                string variableName = variable.Value2.ToString();
                if (string.IsNullOrEmpty(variableName))
                {
                    return new ReturnClass(false, chart);
                }

                QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];

                if (!ValidateCell(chart, 1, questionDetails.CategoryCount))
                {
                    return new ReturnClass(false, chart);
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateGTsettings(Worksheet sht)
        {
            Range cRSettingRange = ExcelUtil.EndxlRight(sht.Range[Constants.CR.CRGTStartAddress]);
            if (cRSettingRange.Column <= Constants.CR.CRColInputStart)
            {
                return new ReturnClass(true);
            }
            cRSettingRange = sht.Range[sht.Range[Constants.CR.CRGTStartAddress], cRSettingRange];

            foreach (Range targetCell in cRSettingRange.Cells)
            {
                Range gTSetting = targetCell;
                if (!validateGTSetting(gTSetting))
                {
                    return new ReturnClass(false, gTSetting, AddinResource.CR_COL_GT_INVALID, new string[] { gTSetting.Column.ToString() });
                }
            }
            return new ReturnClass(true);
        }

        private static bool validateGTSetting(Range gTSetting)
        {
            if (null == gTSetting.Value2)
            {
                return true;
            }
            string gTSettingStr = gTSetting.Value2.ToString();
            if (string.IsNullOrEmpty(gTSettingStr) || AddinResource.MarkBlackCircle == gTSettingStr)
            {
                return true;
            }
            return false;
        }

        private static ReturnClass validateRowDivSection(Worksheet sht)
        {
            Range cRSettingRange = ExcelUtil.EndxlRight(sht.Range[Constants.CR.CRRowDivStartAddress]);
            if (cRSettingRange.Column <= Constants.CR.CRColInputStart)
            {
                return new ReturnClass(true);
            }
            cRSettingRange = sht.Range[sht.Range[Constants.CR.CRRowDivStartAddress], cRSettingRange];

            foreach (Range targetCell in cRSettingRange.Cells)
            {
                Range divSetting = targetCell;
                if (!validateDivSetting(divSetting))
                {
                    return new ReturnClass(false, divSetting, AddinResource.CR_COL_DIV_INVALID, new string[] { divSetting.Column.ToString() });
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateNarrow(Worksheet sht)
        {
            Range cRSettingRange = findMaxAllocatedRange(sht, true, sht.Range[Constants.CR.CRNarrowStartAddress], sht.Range[Constants.CR.CRNarrowStartAddress].Offset[1, 0]);
            if (cRSettingRange.Column <= Constants.CR.CRColInputStart)
            {
                return new ReturnClass(true);
            }
            cRSettingRange = sht.Range[sht.Range[Constants.CR.CRNarrowStartAddress], cRSettingRange];

            foreach (Range targetCell in cRSettingRange.Cells)
            {
                Range variable = targetCell;
                Range narrowStr = targetCell.Offset[1, 0];
                string chartStr = null;
                string variableName = null;
                if (null != narrowStr.Value2)
                {
                    chartStr = narrowStr.Value2.ToString();
                }
                if (null != variable.Value2)                {
                    variableName = variable.Value2.ToString(); 
                }

                if (string.IsNullOrEmpty(chartStr) && string.IsNullOrEmpty(variableName))
                {
                    continue;
                }

                if (!validateVariable(variable, false))
                {
                    return new ReturnClass(false, variable, AddinResource.CR_NARROW_VARIABLE_INVALID, new string[] { variable.Column.ToString() });
                }

                if (string.IsNullOrEmpty(variableName))
                {
                    return new ReturnClass(false, narrowStr, AddinResource.CR_NARROW_CRITERIA_INVALID, new string[] { variable.Column.ToString() });
                }


                if (string.IsNullOrEmpty(chartStr))
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

        private static ReturnClass validateAxis3WayCrossVAraible(Worksheet sht, string address, bool trippleItem = false)
        {
            Range cRSettingRange = findMaxAllocatedRange(sht, true, sht.Range[address], sht.Range[address].Offset[1, 0], sht.Range[address].Offset[2, 0]);

            if (cRSettingRange.Column <= Constants.CR.CRColInputStart)
            {
                return new ReturnClass(true);
            }
            cRSettingRange = sht.Range[sht.Range[address], cRSettingRange];

            foreach (Range targetCell in cRSettingRange.Cells)
            {
                Range variable = targetCell;
                Range variableType = targetCell.Offset[1, 0];
                Range variableChoiceCnt = targetCell.Offset[2, 0];
                if (!validateVariable(variable, false))
                {
                    return new ReturnClass(false, variable, trippleItem ? AddinResource.CR_3WAY_VARIABLE_INVALID : AddinResource.CR_AXES_VARIABLE_INVALID, new string[] { variable.Column.ToString() });
                }
                if (!validateVariableType(variable, variableType))
                {
                    return new ReturnClass(false, variableType, trippleItem ? AddinResource.CR_3WAY_VARIABLE_TYPE_INVALID : AddinResource.CR_AXES_VARIABLE_TYPE_INVALID, new string[] { variableType.Column.ToString() });
                }
                if (!validateVariableChoiceCnt(variable, variableChoiceCnt))
                {
                    return new ReturnClass(false, variableChoiceCnt, trippleItem ? AddinResource.CR_3WAY_VARIABLE_CATEGORY_CNT_INVALID : AddinResource.CR_AXES_VARIABLE_CATEGORY_CNT_INVALID, new string[] { variableChoiceCnt.Column.ToString() });
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateVariableSection(Worksheet sht)
        {
            Range cRSettingRange = findMaxAllocatedRange(sht, false, sht.Range[Constants.CR.CRVariableStartAddress],
                sht.Range[Constants.CR.CRVariableStartAddress].Offset[0, 1], 
                sht.Range[Constants.CR.CRVariableStartAddress].Offset[0, 2],
                sht.Range[Constants.CR.CRVariableStartAddress].Offset[0, 3],
                sht.Range[Constants.CR.CRVariableStartAddress].Offset[0, 4]);
            if (cRSettingRange.Row <= Constants.CR.CRRowInputStart)
            {
                return new ReturnClass(true);
            }
            cRSettingRange = sht.Range[sht.Range[Constants.CR.CRVariableStartAddress], cRSettingRange];

            foreach (Range targetCell in cRSettingRange.Cells)
            {
                //if (!validateNumCol(targetCell))
                //{
                //    return new ReturnClass(false, targetCell);
                //}
                Range variable = targetCell.Offset[0, 1];
                Range variableType = targetCell.Offset[0, 2];
                Range variableChoiceCnt = targetCell.Offset[0, 3];
                Range divSetting = targetCell.Offset[0, 4];
                if (!validateVariable(variable))
                {
                    return new ReturnClass(false, variable, AddinResource.CR_VARIABLE_INVALID, new string[] { variable.Row.ToString() });
                }
                if (!validateVariableType(variable, variableType))
                {
                    return new ReturnClass(false, variableType, AddinResource.CR_VARIABLE_TYPE_INVALID, new string[] { variableType.Row.ToString() });
                }
                if (!validateVariableChoiceCnt(variable, variableChoiceCnt))
                {
                    return new ReturnClass(false, variableChoiceCnt, AddinResource.CR_VARIABLE_CATEGORY_CNT_INVALID, new string[] { variableChoiceCnt.Row.ToString() });
                }
                if (!validateDivSetting(divSetting))
                {
                    return new ReturnClass(false, divSetting, AddinResource.CR_ROW_DIV_INVALID, new string[] { divSetting.Row.ToString() });
                }
            }
            return new ReturnClass(true);
        }

        public static bool validateDivSetting(Range divSetting, string markDiv = Constants.Mark.MarkDiv)
        {
            if (null == divSetting.Value2)
            {
                return true;
            }
            string divSettingStr = divSetting.Value2.ToString();
            if (string.IsNullOrEmpty(divSettingStr) || markDiv == divSettingStr)
            {
                return true;
            }
            return false;
        }

        public static bool validateVariableChoiceCnt(Range variable, Range variableChoiceCnt)
        {
            if (null == variableChoiceCnt.Value2)
            {
                return true;
            }
            string variableChoiceCntStr = variableChoiceCnt.Value2.ToString();
            if (string.IsNullOrEmpty(variableChoiceCntStr))
            {
                return true;
            }
            if (null == variable.Value2)
            {
                return false;
            }
            string variableName = variable.Value2.ToString();
            if (string.IsNullOrEmpty(variableName))
            {
                return false;
            }
            if (!Definitions.VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }

            QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];
            if (questionDetails.CategoryCount.ToString() == variableChoiceCntStr)
            {
                return true;
            }
            return false;
        }

        public static bool validateVariableType(Range variable, Range variableType)
        {
            if (null == variableType.Value2)
            {
                return true;
            }
            string variableTypeName = variableType.Value2.ToString();
            if (string.IsNullOrEmpty(variableTypeName))
            {
                return true;
            }
            if (null == variable.Value2)
            {
                return false;
            }
            string variableName = variable.Value2.ToString();
            if (string.IsNullOrEmpty(variableName))
            {
                return false;
            }
            if (!Definitions.VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }

            QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];
            if (questionDetails.AnswerType == variableTypeName)
            {
                return true;
            }
            return false;
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

        public static Worksheet getCRSheet()
        {
            foreach (Worksheet sht in Globals.ThisAddIn.Application.Worksheets)
            {
                switch (sht.CodeName)
                {
                    case Common.Constants.SheetType.sh_CrossCounting:
                        return sht;
                    default:
                        break;
                }

            }
            return null;
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
                    Range cRSettingRange = ExcelUtil.EndxlRight(range);
                    if (cRSettingRange.Column > large)
                    {
                        large = cRSettingRange.Column;
                    }
                    if (start == 0)
                    {
                        start = cRSettingRange.Row;
                    }

                }
                else
                {
                    Range cRSettingRange = ExcelUtil.EndxlUp(range);
                    if (cRSettingRange.Row > large)
                    {
                        large = cRSettingRange.Row;
                    }
                    if (start == 0)
                    {
                        start = cRSettingRange.Column;
                    }
                }
            }
            if (column) { return sht.Cells[start, large]; } else { return sht.Cells[large, start]; }
        }

    }
}