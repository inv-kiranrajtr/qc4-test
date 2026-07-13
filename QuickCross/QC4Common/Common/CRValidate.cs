using log4net;
using Microsoft.Office.Interop.Excel;
using QC4Common;
using QC4Common.Common;
using QC4Common.Model;
using QC4Common.Util;
using QC4Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Constants = QC4Common.Common.Constants;

namespace QC4Common.Common
{
    public class CRValidate
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static bool ValidateCRTab(Worksheet sht, Dictionary<string, QuestionSettings> VariableDictionary, ref string msg, 
            bool showError = true, bool pro = true, bool report = false)
        {
            ReturnClass result = validate(sht, VariableDictionary, true, !pro, report);
            if (null != result && !result.Result)
            {
                Range value = null;
                if (result.Value is Range)
                {
                    value = (Range)result.Value;
                }
                else if (result.Value is string)
                {
                    string valStr = (string)result.Value;
                    string[] valArr = valStr.Split(':');
                    if (valArr.Length == 2)
                    {
                        value = sht.Cells[Convert.ToInt32(valArr[0]), Convert.ToInt32(valArr[1])];
                    }
                }

                if (null != result.Msg)
                {
                    msg = result.Msg;
                }
                else
                {
                    if (value != null)
                    {
                        msg = string.Format(CommonResource.VALIDATION_FAIL_SHEET, value.Row, value.Column);
                    }
                    else
                    {
                        msg = CommonResource.VALIDATION_FAIL_SHEET;
                    }
                }
                if (showError)
                {
                    MessageDialog.ErrorOk(msg);
                }

                if (value != null && pro)
                {
                    value.Select();
                }
                return false;
            }
            return true;
        }


        public static ReturnClass validate(Worksheet sht, Dictionary<string, QuestionSettings> VariableDictionary, bool fromApp = false, 
            bool std = false , bool report = false)
        {
            _log.Debug("cr validate start");
            if (null == sht) return null;

            ReturnClass returnClass = validateNarrow(sht, VariableDictionary);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateAxis3WayCrossVAraible(sht, Constants.CR.CR3WayCrossStartAddress, VariableDictionary, true);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateAxis3WayCrossVAraible(sht, Constants.CR.CRAxesVariableStartAddress, VariableDictionary);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateChartSetting(sht, VariableDictionary);
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
            if (fromApp)
            {
                returnClass = validateVariableSectionUsingArray(sht, VariableDictionary, std, report);
            }
            else
            {
                returnClass = validateVariableSection(sht, VariableDictionary, std);
            }
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }
            _log.Debug("cr validate complete");
            return new ReturnClass(true);
        }


        public static ReturnClass ValidateDiv(List<List<CossTableInput>> crTableSets, Dictionary<string, QuestionSettings> variableDictionary, bool report = false)
        {

            ReturnClass returnClass;
            foreach (List<CossTableInput> crTableSetItems in crTableSets)
            {
                foreach (CossTableInput crTableSet in crTableSetItems)
                {
                    if (crTableSet.filter != null)
                    {
                        returnClass = validateNarrow(crTableSet.filter, crTableSet.filterRange, variableDictionary);
                        if (null != returnClass && returnClass.Result == false)
                        {
                            return returnClass;
                        }
                    }

                    if (crTableSet.Axis2Range != null)
                    {
                        returnClass = validateAxis3WayCrossVAraible(crTableSet.Axis2Range, Constants.CR.CR3WayCrossStartAddress, variableDictionary, true);
                        if (null != returnClass && returnClass.Result == false)
                        {
                            return returnClass;
                        }
                    }

                    returnClass = validateAxis3WayCrossVAraible(crTableSet.Axis1Range, Constants.CR.CRAxesVariableStartAddress, variableDictionary);
                    if (null != returnClass && returnClass.Result == false)
                    {
                        return returnClass;
                    }

                    if (crTableSet.lineSettings != null)
                    {
                        returnClass = validateChartSetting(crTableSet.lineSettingsRange, variableDictionary);
                        if (null != returnClass && returnClass.Result == false)
                        {
                            return returnClass;
                        }
                    }

                    returnClass = validateVariableSection(crTableSet.targetRange.Offset[0, -1], variableDictionary, report);
                    if (null != returnClass && returnClass.Result == false)
                    {
                        return returnClass;
                    }
                    //returnClass = validateGTsettings(sht);
                    //if (null != returnClass && returnClass.Result == false)
                    //{
                    //    return returnClass;
                    //}

                    //returnClass = validateRowDivSection(sht);
                    //if (null != returnClass && returnClass.Result == false)
                    //{
                    //    return returnClass;
                    //}

                    //returnClass = validateVariableSection(sht, VariableDictionary);
                    //if (null != returnClass && returnClass.Result == false)
                    //{
                    //    return returnClass;
                    //}                    


                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateChartSetting(Worksheet sht, Dictionary<string, QuestionSettings> VariableDictionary)
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
                if (null != variable3Way.Value2 && !string.IsNullOrEmpty(variable3Way.Value2.ToString()))
                {
                    return new ReturnClass(false, chart, CommonResource.CR_COL_CHART_3WAY_INVALID, new string[] { GetExcelColumnName(chart.Column) });
                }

                if (!validateVariable(variable, VariableDictionary, false))
                {
                    return new ReturnClass(false, variable, CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { GetExcelColumnName(variable.Column) });
                }

                if (null == variable.Value2)
                {
                    return new ReturnClass(false, chart, CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { GetExcelColumnName(chart.Column) });
                }
                string variableName = variable.Value2.ToString();
                if (string.IsNullOrEmpty(variableName))
                {
                    return new ReturnClass(false, chart, CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { GetExcelColumnName(chart.Column) });
                }

                QuestionSettings questionDetails = VariableDictionary[variableName];

                if (!ValidateCell(chart, 1, questionDetails.CategoryCount))
                {
                    return new ReturnClass(false, chart, CommonResource.CR_COL_CHART_INVALID, new string[] { GetExcelColumnName(chart.Column) });
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateChartSetting(Range targetCell, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            Range chart = targetCell;
            Range variable = targetCell.Offset[-3, 0];

            if (null == chart.Value2)
            {
                return new ReturnClass(true);
            }
            string chartStr = chart.Value2.ToString();
            if (string.IsNullOrEmpty(chartStr))
            {
                return new ReturnClass(true);
            }

            Range variable3Way = variable.Offset[-3, 0];
            if (null != variable3Way.Value2)
            {
                return new ReturnClass(false, chart, CommonResource.CR_COL_CHART_3WAY_INVALID, new string[] { chart.Column.ToString() });
            }

            if (!validateVariable(variable, VariableDictionary, false))
            {
                return new ReturnClass(false, variable, CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { variable.Column.ToString() });
            }

            if (null == variable.Value2)
            {
                return new ReturnClass(false, chart, CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { chart.Column.ToString() });
            }
            string variableName = variable.Value2.ToString();
            if (string.IsNullOrEmpty(variableName))
            {
                return new ReturnClass(false, chart, CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { chart.Column.ToString() });
            }

            QuestionSettings questionDetails = VariableDictionary[variableName];

            if (!ValidateCell(chart, 1, questionDetails.CategoryCount))
            {
                return new ReturnClass(false, chart, CommonResource.CR_COL_CHART_INVALID, new string[] { chart.Column.ToString() });
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateChartSetting(AxisSetting targetCell, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            // Range chart = targetCell;
            // Range variable = targetCell.Offset[-3, 0];

            //if (null == chart.Value2)
            //{
            //    return new ReturnClass(true);
            // }
            string chartStr = targetCell.choiceCnt;
            if (string.IsNullOrEmpty(chartStr))
            {
                return new ReturnClass(true);
            }

            // Range variable3Way = variable.Offset[-3, 0];
            if (!string.IsNullOrEmpty(targetCell.variableTripple))
            {
                return new ReturnClass(false, targetCell, CommonResource.CR_COL_CHART_3WAY_INVALID, new string[] { targetCell.Column.ToString() });
            }

            if (!validateVariable(targetCell.variable, VariableDictionary, false))
            {
                return new ReturnClass(false, targetCell, CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { targetCell.Column.ToString() });
            }

            string variableName = targetCell.variable;
            if (string.IsNullOrEmpty(variableName))
            {
                return new ReturnClass(false, targetCell, CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { targetCell.Column.ToString() });
            }

            QuestionSettings questionDetails = VariableDictionary[variableName];

            if (!ValidateCell(targetCell.choiceCnt, 1, questionDetails.CategoryCount))
            {
                return new ReturnClass(false, targetCell, CommonResource.CR_COL_CHART_INVALID, new string[] { targetCell.Column.ToString() });
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
                    return new ReturnClass(false, gTSetting, CommonResource.CR_COL_GT_INVALID, new string[] { GetExcelColumnName(gTSetting.Column) });
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
            if (string.IsNullOrEmpty(gTSettingStr) || CommonResource.MarkBlackCircle == gTSettingStr)
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
                    return new ReturnClass(false, divSetting, CommonResource.CR_COL_DIV_INVALID, new string[] { GetExcelColumnName(divSetting.Column) });
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateNarrow(Worksheet sht, Dictionary<string, QuestionSettings> VariableDictionary)
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
                if (null != variable.Value2)
                {
                    variableName = variable.Value2.ToString();
                }

                if (string.IsNullOrEmpty(chartStr) && string.IsNullOrEmpty(variableName))
                {
                    continue;
                }

                if (!validateVariable(variable, VariableDictionary, false))
                {
                    return new ReturnClass(false, variable, CommonResource.CR_NARROW_VARIABLE_INVALID, new string[] { GetExcelColumnName(variable.Column) });
                }

                if (string.IsNullOrEmpty(variableName))
                {
                    return new ReturnClass(false, narrowStr, CommonResource.CR_NARROW_CRITERIA_INVALID, new string[] { GetExcelColumnName(variable.Column) });
                }


                if (string.IsNullOrEmpty(chartStr))
                {
                    return new ReturnClass(false, narrowStr, CommonResource.CR_NARROW_CRITERIA_INVALID, new string[] { GetExcelColumnName(narrowStr.Column) });
                }

                QuestionSettings questionDetails = VariableDictionary[variableName];

                if (!NumberCheck.NUmberCheckNarrow(narrowStr.Value2.ToString(), questionDetails.CategoryCount))
                {
                    return new ReturnClass(false, narrowStr, CommonResource.CR_NARROW_CRITERIA_INVALID, new string[] { GetExcelColumnName(narrowStr.Column) });
                }
            }
            return new ReturnClass(true);
        }

        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return " " + columnName;
        }


        private static ReturnClass validateNarrow(FilterSettingsCr narrow, AxisSetting narrowRng, Dictionary<string, QuestionSettings> VariableDictionary)
        {

            string variable = narrow.variable;
            string narrowStr = narrow.values;
            string chartStr = narrowStr;
            string variableName = variable;

            if (string.IsNullOrEmpty(chartStr) && string.IsNullOrEmpty(variableName))
            {
                return new ReturnClass(true);
            }

            if (!validateVariable(variable, VariableDictionary, false))
            {
                return new ReturnClass(false, variable, CommonResource.CR_NARROW_VARIABLE_INVALID, new string[] { narrowRng.Column.ToString() });
            }

            if (string.IsNullOrEmpty(variableName))
            {
                return new ReturnClass(false, narrowStr, CommonResource.CR_NARROW_CRITERIA_INVALID, new string[] { narrowRng.Column.ToString() });
            }


            if (string.IsNullOrEmpty(chartStr))
            {
                return new ReturnClass(false, narrowStr, CommonResource.CR_NARROW_CRITERIA_INVALID, new string[] { narrowRng.Column.ToString() });
            }

            QuestionSettings questionDetails = VariableDictionary[variableName];

            if (!NumberCheck.NUmberCheckNarrow(narrowStr, questionDetails.CategoryCount))
            {
                return new ReturnClass(false, narrowStr, CommonResource.CR_NARROW_CRITERIA_INVALID, new string[] { narrowRng.Column.ToString() });
            }
            return new ReturnClass(true);
        }



        private static ReturnClass validateAxis3WayCrossVAraible(Worksheet sht, string address, Dictionary<string, QuestionSettings> VariableDictionary, bool trippleItem = false)
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
                if (!validateVariable(variable, VariableDictionary, false))
                {
                    return new ReturnClass(false, variable, trippleItem ? CommonResource.CR_3WAY_VARIABLE_INVALID : CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { GetExcelColumnName(variable.Column) });
                }
                if (!validateVariableType(variable, variableType, VariableDictionary))
                {
                    return new ReturnClass(false, variableType, trippleItem ? CommonResource.CR_3WAY_VARIABLE_TYPE_INVALID : CommonResource.CR_AXES_VARIABLE_TYPE_INVALID, new string[] { GetExcelColumnName(variableType.Column) });
                }
                if (!validateVariableChoiceCnt(variable, variableChoiceCnt, VariableDictionary))
                {
                    return new ReturnClass(false, variableChoiceCnt, trippleItem ? CommonResource.CR_3WAY_VARIABLE_CATEGORY_CNT_INVALID : CommonResource.CR_AXES_VARIABLE_CATEGORY_CNT_INVALID, new string[] { GetExcelColumnName(variableChoiceCnt.Column) });
                }
                if (trippleItem && checkVariablePresent(variable))
                {
                    Range variableTripple = targetCell.Offset[3, 0];
                    if (!checkVariablePresent(variableTripple))
                    {
                        return new ReturnClass(false, variableChoiceCnt, CommonResource.CR_AXES_VARIABLE_NOT_PRESENT, new string[] { GetExcelColumnName(variable.Column) });
                    }
                }

            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateAxis3WayCrossVAraible(AxisSetting targetCell, string address, Dictionary<string, QuestionSettings> VariableDictionary, bool trippleItem = false)
        {
            string variable = targetCell.variable;
            string variableType = targetCell.variableType;
            string variableChoiceCnt = targetCell.choiceCnt;
            if (!validateVariable(variable, VariableDictionary, false))
            {
                return new ReturnClass(false, variable, trippleItem ? CommonResource.CR_3WAY_VARIABLE_INVALID : CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { targetCell.Column.ToString() });
            }
            if (!validateVariableType(variable, variableType, VariableDictionary))
            {
                return new ReturnClass(false, variableType, trippleItem ? CommonResource.CR_3WAY_VARIABLE_TYPE_INVALID : CommonResource.CR_AXES_VARIABLE_TYPE_INVALID, new string[] { targetCell.Column.ToString() });
            }
            if (!validateVariableChoiceCnt(variable, variableChoiceCnt, VariableDictionary))
            {
                return new ReturnClass(false, variableChoiceCnt, trippleItem ? CommonResource.CR_3WAY_VARIABLE_CATEGORY_CNT_INVALID : CommonResource.CR_AXES_VARIABLE_CATEGORY_CNT_INVALID, new string[] { targetCell.Column.ToString() });
            }
            if (trippleItem && checkVariablePresent(variable))
            {
                string variableTripple = targetCell.variableTripple;
                if (!checkVariablePresent(variableTripple))
                {
                    return new ReturnClass(false, variableChoiceCnt, CommonResource.CR_AXES_VARIABLE_NOT_PRESENT, new string[] { targetCell.Column.ToString() });
                }
            }

            return new ReturnClass(true);
        }


        private static ReturnClass validateVariableSection(Range targetCell, Dictionary<string, QuestionSettings> VariableDictionary, bool report = false)
        {
            Range variable = targetCell.Offset[0, 1];
            Range variableType = targetCell.Offset[0, 2];
            Range variableChoiceCnt = targetCell.Offset[0, 3];
            Range divSetting = targetCell.Offset[0, 4];
            if (!validateVariable(variable, VariableDictionary))
            {
                return new ReturnClass(false, variable, CommonResource.CR_VARIABLE_INVALID, new string[] { variable.Row.ToString() });
            }
            if (!validateVariableType(variable, variableType, VariableDictionary))
            {
                return new ReturnClass(false, variableType, CommonResource.CR_VARIABLE_TYPE_INVALID, new string[] { variableType.Row.ToString() });
            }
            if (!validateVariableChoiceCnt(variable, variableChoiceCnt, VariableDictionary))
            {
                return new ReturnClass(false, variableChoiceCnt, CommonResource.CR_VARIABLE_CATEGORY_CNT_INVALID, new string[] { variableChoiceCnt.Row.ToString() });
            }
            if (report && !validateVariableChoiceCntRpt(variable, variableChoiceCnt, VariableDictionary))
            {
                return new ReturnClass(false, variableChoiceCnt, CommonResource.CR_VARIABLE_CATEGORY_CNT_INVALID_RPT, new string[] { variable.Value2.ToString() });
            }
            if (!validateDivSetting(divSetting))
            {
                return new ReturnClass(false, divSetting, CommonResource.CR_ROW_DIV_INVALID, new string[] { divSetting.Row.ToString() });
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateVariableSection(Worksheet sht, Dictionary<string, QuestionSettings> VariableDictionary, bool std)
        {
            Range cRSettingRange = findMaxAllocatedRange(sht, false, sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress],
                sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress].Offset[0, 1],
                sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress].Offset[0, 2],
                sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress].Offset[0, 3],
                sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress].Offset[0, 4]);
            if (cRSettingRange.Row <= (!std ? Constants.CR.CRRowInputStart : Constants.CR.CRRowInputStart + 1))
            {
                return new ReturnClass(true);
            }
            cRSettingRange = sht.Range[sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress], cRSettingRange];

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
                if (!validateVariable(variable, VariableDictionary))
                {
                    return new ReturnClass(false, variable, CommonResource.CR_VARIABLE_INVALID, new string[] { variable.Row.ToString() });
                }
                if (!validateVariableType(variable, variableType, VariableDictionary))
                {
                    return new ReturnClass(false, variableType, CommonResource.CR_VARIABLE_TYPE_INVALID, new string[] { variableType.Row.ToString() });
                }
                if (!validateVariableChoiceCnt(variable, variableChoiceCnt, VariableDictionary))
                {
                    return new ReturnClass(false, variableChoiceCnt, CommonResource.CR_VARIABLE_CATEGORY_CNT_INVALID, new string[] { variableChoiceCnt.Row.ToString() });
                }
                if (!validateDivSetting(divSetting))
                {
                    return new ReturnClass(false, divSetting, CommonResource.CR_ROW_DIV_INVALID, new string[] { divSetting.Row.ToString() });
                }
            }
            return new ReturnClass(true);
        }


        private static ReturnClass validateVariableSectionUsingArray(Worksheet sht, Dictionary<string, QuestionSettings> VariableDictionary, bool std, bool report)
        {
            Range cRSettingRange = findMaxAllocatedRange(sht, false, sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress],
                sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress].Offset[0, 1],
                sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress].Offset[0, 2],
                sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress].Offset[0, 3],
                sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress].Offset[0, 4]);
            if (cRSettingRange.Row <= (!std ? Constants.CR.CRRowInputStart : Constants.CR.CRRowInputStart + 1))
            {
                return new ReturnClass(true);
            }
            Range valuesRange = sht.Range["A1", sht.Cells[cRSettingRange.Row, cRSettingRange.Column + 4]];
            object[,] values = (object[,])valuesRange.Value2;
            cRSettingRange = sht.Range[sht.Range[std ? Constants.CR.CRVariableStartAddress_S : Constants.CR.CRVariableStartAddress], cRSettingRange];

            Range LastCellRow = sht.Cells.Find("*", SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlPrevious);
            Range LastCellCol = sht.Cells.Find("*", SearchOrder: XlSearchOrder.xlByColumns, SearchDirection: XlSearchDirection.xlPrevious);
            Range valuesRangeAll = sht.Range["G1", sht.Cells[LastCellRow.Row, LastCellCol.Column]];
            object[,] valuesAll = (object[,])valuesRangeAll.Value2;


            int row = Constants.CR.CRRowInputStart;
            row = std ? row + 1 : row;
            int col = 1;
            foreach (Range targetCell in cRSettingRange.Cells)
            {
                row++;

                string variableStr = Convert.ToString(values[row, col + 2]);
                string variableTypeStr = Convert.ToString(values[row, col + 3]);
                string variableChoiceCntStr = Convert.ToString(values[row, col + 4]);
                string divSettingStr = Convert.ToString(values[row, col + 5]);



                if (!validateVariable(variableStr, VariableDictionary))
                {
                    return new ReturnClass(false, row + ":" + (col + 2), CommonResource.CR_VARIABLE_INVALID, new string[] { row.ToString() });
                }
                if (!validateVariableType(variableStr, variableTypeStr, VariableDictionary))
                {
                    return new ReturnClass(false, row + ":" + (col + 3), CommonResource.CR_VARIABLE_TYPE_INVALID, new string[] { row.ToString() });
                }
                if (!validateVariableChoiceCnt(variableStr, variableChoiceCntStr, VariableDictionary))
                {
                    return new ReturnClass(false, row + ":" + (col + 4), CommonResource.CR_VARIABLE_CATEGORY_CNT_INVALID, new string[] { row.ToString() });
                }
                if (report && variableTypeStr == Constants.AnswerType.SA &&
                    !validateVariableChoiceCntRpt(variableStr, variableChoiceCntStr, VariableDictionary, valuesAll, row))
                {
                    return new ReturnClass(false, row + ":" + (col + 4), CommonResource.CR_VARIABLE_CATEGORY_CNT_INVALID_RPT, new string[] {  variableStr });
                }
                if (!validateDivSetting(divSettingStr))
                {
                    return new ReturnClass(false, row + ":" + (col + 5), CommonResource.CR_ROW_DIV_INVALID, new string[] { row.ToString() });
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

        public static bool validateDivSetting(string divSetting, string markDiv = Constants.Mark.MarkDiv)
        {
            if (null == divSetting)
            {
                return true;
            }
            string divSettingStr = divSetting;
            if (string.IsNullOrEmpty(divSettingStr) || markDiv == divSettingStr)
            {
                return true;
            }
            return false;
        }

        public static bool validateVariableChoiceCnt(Range variable, Range variableChoiceCnt, Dictionary<string, QuestionSettings> VariableDictionary)
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
            if (!VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }

            QuestionSettings questionDetails = VariableDictionary[variableName];
            if (questionDetails.CategoryCount.ToString() == variableChoiceCntStr)
            {
                return true;
            }
            return false;
        }

        public static bool validateVariableChoiceCntRpt(Range variable, Range variableChoiceCnt, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            string variableName = variable.Value2.ToString();
            if (string.IsNullOrEmpty(variableName))
            {
                return false;
            }
            if (!VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }

            QuestionSettings questionDetails = VariableDictionary[variableName];
            if (questionDetails.CategoryCount > Constants.max_series_cnt_rpt && questionDetails.AnswerType == Constants.AnswerType.SA)
            {
                return false;
            }
            return true;
        }
        public static bool validateVariableChoiceCntRpt(string variable, string variableChoiceCnt, 
            Dictionary<string, QuestionSettings> VariableDictionary, object[,] values, int row)
        {
            int i;
            for (i = 1; i <= values.GetLength(1); i++)
            {
                string targetCellSetStr = Convert.ToString(values[row, i]);
                if ("●" == targetCellSetStr || "On" == targetCellSetStr) // to do 
                {
                    break;
                }
            }
            if (i > values.GetLength(1))
            {
                return true;
            }
            string variableName = variable;
            if (string.IsNullOrEmpty(variableName))
            {
                return false;
            }
            if (!VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }

            QuestionSettings questionDetails = VariableDictionary[variableName];

            if (questionDetails.CategoryCount > Constants.max_series_cnt_rpt)
            {
                return false;
            }


            return true;
        }

        public static bool validateVariableChoiceCnt(string variable, string variableChoiceCnt, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            string variableChoiceCntStr = variableChoiceCnt;
            if (string.IsNullOrEmpty(variableChoiceCntStr))
            {
                return true;
            }
            string variableName = variable;
            if (string.IsNullOrEmpty(variableName))
            {
                return false;
            }
            if (!VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }

            QuestionSettings questionDetails = VariableDictionary[variableName];
            if (questionDetails.CategoryCount.ToString() == variableChoiceCntStr)
            {
                return true;
            }
            return false;
        }

        public static bool validateVariableType(Range variable, Range variableType, Dictionary<string, QuestionSettings> VariableDictionary)
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
            if (!VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }

            QuestionSettings questionDetails = VariableDictionary[variableName];
            if (questionDetails.AnswerType == variableTypeName)
            {
                return true;
            }
            return false;
        }

        public static bool validateVariableType(string variable, string variableType, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            string variableTypeName = variableType;
            if (string.IsNullOrEmpty(variableTypeName))
            {
                return true;
            }
            string variableName = variable;
            if (string.IsNullOrEmpty(variableName))
            {
                return false;
            }
            if (!VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }

            QuestionSettings questionDetails = VariableDictionary[variableName];
            if (questionDetails.AnswerType == variableTypeName)
            {
                return true;
            }
            return false;
        }

        public static bool checkVariablePresent(Range variable)
        {
            if (null == variable.Value2)
            {
                return false;
            }
            string variableName = variable.Value2.ToString();
            if (string.IsNullOrEmpty(variableName))
            {
                return false;
            }
            return true;
        }

        public static bool checkVariablePresent(string variable)
        {
            string variableName = variable;
            if (string.IsNullOrEmpty(variableName))
            {
                return false;
            }
            return true;
        }

        public static bool validateVariable(Range variable, Dictionary<string, QuestionSettings> VariableDictionary, bool nTypeInclude = true)
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
            if (!VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }
            QuestionSettings questionDetails = VariableDictionary[variableName];
            if (questionDetails.AnswerType == Constants.AnswerType.SA
                || questionDetails.AnswerType == Constants.AnswerType.MA
                || (nTypeInclude && questionDetails.AnswerType == Constants.AnswerType.N)
                )
            {
                return true;
            }
            return false;
        }


        public static bool validateVariable(string variable, Dictionary<string, QuestionSettings> VariableDictionary, bool nTypeInclude = true)
        {
            if (null == variable)
            {
                return true;
            }
            string variableName = variable;
            if (string.IsNullOrEmpty(variableName))
            {
                return true;
            }
            if (!VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }
            QuestionSettings questionDetails = VariableDictionary[variableName];
            if (questionDetails.AnswerType == Constants.AnswerType.SA
                || questionDetails.AnswerType == Constants.AnswerType.MA
                || (nTypeInclude && questionDetails.AnswerType == Constants.AnswerType.N)
                )
            {
                return true;
            }
            return false;
        }
        //private static Worksheet getCRSheet()
        //{
        //    foreach (Worksheet sht in Globals.ThisAddIn.Application.Worksheets)
        //    {
        //        switch (sht.CodeName)
        //        {
        //            case Common.Constants.SheetType.sh_CrossCounting:
        //                return sht;
        //            default:
        //                break;
        //        }

        //    }
        //    return null;
        //}

        private static bool ValidateCell(Range range, int minvalue, int maxvalue)
        {
            if (range.Text == "")
            {
                return true;
            }
            return ValidateCell(range.Text, minvalue, maxvalue);
        }

        private static bool ValidateCell(string range, int minvalue, int maxvalue)
        {
            if (range == "")
            {
                return true;
            }

            string[] SplitContent;
            string Contents = range;
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
                if (!ValidateRange(range, minvalue, maxvalue)) { return false; }
            }
            return true;
        }

        private static bool ValidateRange(string Contents, int minvalue, int maxvalue)
        {
            string[] SplitContent;
            if (Contents.StartsWith("<>"))
            {
                Contents = Contents.Substring("<>".Length);
            }
            if (Contents.StartsWith("!"))
            {
                Contents = Contents.TrimStart('!');
            }
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