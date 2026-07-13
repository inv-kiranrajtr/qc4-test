using Microsoft.Office.Interop.Excel;
using QC4Common;
using QC4Common.Common;
using System.Linq;
using QC4Common.Model;
using QC4Common.Util;
using System;
using System.Collections.Generic;
using Constants = QC4Common.Common.Constants;

namespace QC4Common.Common
{
    public class SLValidate
    {
        public static bool ValidateSLTab(Worksheet sht, Dictionary<string, QuestionSettings> VariableDictionary, ref string msg, bool showError = true)
        {
            ReturnClass result = validate(sht, VariableDictionary);
            if (null != result && !result.Result)
            {
                Range value = (Range)result.Value;
                if (null != result.Msg)
                {
                    //MessageDialog.ErrorOk(result.Msg);
                    msg = result.Msg;
                }
                else
                {
                    //MessageDialog.ErrorOk(string.Format(CommonResource.VALIDATION_FAIL_SHEET, value.Row, value.Column));
                    msg = string.Format(CommonResource.VALIDATION_FAIL_SHEET, value.Row, value.Column);
                }
                if (showError)
                {
                    MessageDialog.ErrorOk(msg);
                }
                value.Select();
                return false;
            }
            else
            {
                result = SLValidateName.validateName(sht);
                if (null != result && !result.Result)
                {
                    Range value = (Range)result.Value;
                    value.Select();
                    msg = "DND";
                    return false;
                }
                result = validateOutputNameLength(sht);
                if (null != result && result.Result == false)
                {
                    Range value = (Range)result.Value;
                    if (null != result.Msg)
                    {
                        msg = result.Msg;
                    }
                    else
                    {
                        msg = string.Format(CommonResource.VALIDATION_FAIL_SHEET, value.Row, value.Column);
                    }
                    if (showError)
                    {
                        MessageDialog.ErrorOk(msg);
                    }
                    value.Select();
                    return false;
                }
                return true;
            }
        }

        public static ReturnClass validate(Worksheet sht, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            if (null == sht) return null;

            ReturnClass returnClass = validateOutputEnablesettings(sht);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateAxisSLVAraible(sht, Constants.SL.SLAxesVariableStartAddress, VariableDictionary);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateRowDivSection(sht);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateVariableSection(sht, VariableDictionary);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateOutputName(sht);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            return new ReturnClass(true);
        }

        private static ReturnClass validateOutputName(Worksheet sht)
        {
            Range sLSettingRange = CRValidate.findMaxAllocatedRange(sht, false, sht.Range[Constants.SL.SLVariableStartAddress]);
            if (sLSettingRange.Row <= Constants.SL.SLRowInputStart)
            {
                return new ReturnClass(true);
            }
            sLSettingRange = sht.Range[sht.Range[Constants.SL.SLVariableStartAddress], sLSettingRange];

            foreach (Range targetCell in sLSettingRange.Cells)
            {
                Range outputNameSet = targetCell.Offset[0, -1];
                if (!String.IsNullOrEmpty(outputNameSet.Value2))
                {
                    string var = outputNameSet.Value2.ToString();
                    if (var.Length > 25)
                    {
                        return new ReturnClass(false, outputNameSet, CommonResource.SL_OUTPUT_NAME_CHAR_LENGTH);
                    }
                    QC4Common.Util.QSUtil.ValidateVariable(var, out string msg);
                    if ("" != msg)
                    {
                        return new ReturnClass(false, outputNameSet, msg);
                    }
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateOutputNameLength(Worksheet sht)
        {
            Range sLSettingRange = CRValidate.findMaxAllocatedRange(sht, false, sht.Range[Constants.SL.SLVariableStartAddress]);
            if (sLSettingRange.Row <= Constants.SL.SLRowInputStart)
            {
                return new ReturnClass(true);
            }
            sLSettingRange = sht.Range[sht.Range[Constants.SL.SLVariableStartAddress], sLSettingRange];

            foreach (Range targetCell in sLSettingRange.Cells)
            {
                Range outputNameSet = targetCell.Offset[0, -1];
                if (outputNameSet.Value2 != null)
                {
                    string var = outputNameSet.Value2.ToString();
                    if (var.Length > 25)
                    {
                        return new ReturnClass(false, outputNameSet, CommonResource.SL_OUTPUT_NAME_CHAR_LENGTH);
                    }
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateOutputEnablesettings(Worksheet sht)
        {
            Range sLSettingRange = ExcelUtil.EndxlRight(sht.Range[Constants.SL.SLOutputStartAddress]);
            if (sLSettingRange.Column <= Constants.SL.SLColInputStart)
            {
                return new ReturnClass(true);
            }
            sLSettingRange = sht.Range[sht.Range[Constants.SL.SLOutputStartAddress], sLSettingRange];

            Range prevSetting = null;
            foreach (Range targetCell in sLSettingRange.Cells)
            {
                Range sLSetting = targetCell;
                if (!validateOutputEnable(sLSetting, prevSetting))
                {
                    return new ReturnClass(false, sLSetting, CommonResource.SL_OUTPUT_ENABLE_INVALID, new string[] { GetExcelColumnName(sLSetting.Column) });
                }
                if (checkDivPresent(sLSetting))
                {
                    prevSetting = null;
                }
                else
                {
                    prevSetting = targetCell;
                }
            }
            return new ReturnClass(true);
        }

        private static bool checkDivPresent(Range varSetting, bool takeCol = false)
        {
            if (takeCol)
            {
                varSetting = varSetting.Worksheet.Cells[Constants.SL.SLRowInputStart, varSetting.Column];
            }
            else
            {
                varSetting = varSetting.Offset[4, 0];
            }
            if (null == varSetting.Value2)
            {
                return false;
            }
            string gTSettingStr = varSetting.Value2.ToString();
            if (gTSettingStr == Constants.Mark.MarkDiv)
            {
                return true;
            }
            return false;
        }

        public static bool checkSepPresent(Range varSetting, bool takeCol = false)
        {
            if (takeCol)
            {
                varSetting = varSetting.Worksheet.Cells[varSetting.Row, Constants.SL.SLColInputStart];
            }
            else
            {
                varSetting = varSetting.Offset[0, 3];
            }

            if (null == varSetting.Value2)
            {
                return false;
            }
            string gTSettingStr = varSetting.Value2.ToString();
            if (gTSettingStr == Constants.Mark.MarkSep)
            {
                return true;
            }
            return false;
        }

        private static bool validateOutputEnable(Range sLSetting, Range prevSetting)
        {
            if (!String.IsNullOrEmpty(sLSetting.Offset[1, 0].Value2))
            {
                if (null == sLSetting.Value2 || string.IsNullOrEmpty(sLSetting.Value2.ToString()))
                {
                    return false;
                }
            }
            if (null == sLSetting.Value2)
            {
                return true;
            }
            string sLSettingStr = sLSetting.Value2.ToString();
            if (string.IsNullOrEmpty(sLSettingStr)) return true;

            if (!(CommonResource.MarkWhiteCircle == sLSettingStr
                || CommonResource.MarkOFF == sLSettingStr
                || sLSettingStr.Equals("On", StringComparison.InvariantCultureIgnoreCase)
                || sLSettingStr.Equals("Off", StringComparison.InvariantCultureIgnoreCase)))
            {
                return false;
            }
            if (null == prevSetting || null == prevSetting.Value2)
            {
                return true;
            }
            string prevSettingStr = prevSetting.Value2.ToString();

            if (string.IsNullOrEmpty(prevSettingStr)) return true;

            if (sLSettingStr == prevSettingStr)
            {
                return true;
            }

            return false;
        }

        private static ReturnClass validateRowDivSection(Worksheet sht)
        {
            Range sLSettingRange = ExcelUtil.EndxlRight(sht.Range[Constants.SL.SLRowDivStartAddress]);
            if (sLSettingRange.Column <= Constants.SL.SLColInputStart)
            {
                return new ReturnClass(true);
            }
            sLSettingRange = sht.Range[sht.Range[Constants.SL.SLRowDivStartAddress], sLSettingRange];

            foreach (Range targetCell in sLSettingRange.Cells)
            {
                Range divSetting = targetCell;
                if (!CRValidate.validateDivSetting(divSetting))
                {
                    return new ReturnClass(false, divSetting, CommonResource.CR_COL_DIV_INVALID, new string[] { GetExcelColumnName(divSetting.Column) });
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateAxisSLVAraible(Worksheet sht, string address, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            Range sLSettingRange = CRValidate.findMaxAllocatedRange(sht, true, sht.Range[address], sht.Range[address].Offset[1, 0], sht.Range[address].Offset[2, 0]);

            if (sLSettingRange.Column <= Constants.SL.SLColInputStart)
            {
                return new ReturnClass(true);
            }
            sLSettingRange = sht.Range[sht.Range[address], sLSettingRange];

            foreach (Range targetCell in sLSettingRange.Cells)
            {
                Range variable = targetCell;
                Range variableType = targetCell.Offset[1, 0];
                Range variableChoiceCnt = targetCell.Offset[2, 0];
                if (!CRValidate.validateVariable(variable, VariableDictionary, false))
                {
                    return new ReturnClass(false, variable, CommonResource.CR_AXES_VARIABLE_INVALID, new string[] { GetExcelColumnName(variable.Column) });
                }
                if (!CRValidate.validateVariableType(variable, variableType, VariableDictionary))
                {
                    return new ReturnClass(false, variableType, CommonResource.CR_AXES_VARIABLE_TYPE_INVALID, new string[] { GetExcelColumnName(variableType.Column) });
                }
                if (!CRValidate.validateVariableChoiceCnt(variable, variableChoiceCnt, VariableDictionary))
                {
                    return new ReturnClass(false, variableChoiceCnt, CommonResource.CR_AXES_VARIABLE_CATEGORY_CNT_INVALID, new string[] { GetExcelColumnName(variableChoiceCnt.Column) });
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateVariableSection(Worksheet sht, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            Range sLSettingRange = CRValidate.findMaxAllocatedRange(sht, false, sht.Range[Constants.SL.SLVariableStartAddress],
                sht.Range[Constants.SL.SLVariableStartAddress].Offset[0, 1],
                sht.Range[Constants.SL.SLVariableStartAddress].Offset[0, 2],
                sht.Range[Constants.SL.SLVariableStartAddress].Offset[0, 3],
                sht.Range[Constants.SL.SLVariableStartAddress].Offset[0, 4]);
            if (sLSettingRange.Row <= Constants.SL.SLRowInputStart)
            {
                return new ReturnClass(true);
            }

            //Check weight back is ON
            bool hasSetWeightback = checkWeightbackOn(sht);

            sLSettingRange = sht.Range[sht.Range[Constants.SL.SLVariableStartAddress], sLSettingRange];
            Range prevSetting = null;
            Range fistSep = null;
            int maxCol = 0;
            foreach (Range targetCell in sLSettingRange.Cells)
            {
                //if (!validateNumCol(targetCell))
                //{
                //    return new ReturnClass(false, targetCell);
                //}
                Range variable = targetCell;
                Range variableType = targetCell.Offset[0, 1];
                Range variableChoiceCnt = targetCell.Offset[0, 2];
                Range divSetting = targetCell.Offset[0, 3];
                if (!CRValidate.validateVariable(variable, VariableDictionary))
                {
                    return new ReturnClass(false, variable, CommonResource.CR_VARIABLE_INVALID, new string[] { variable.Row.ToString() });
                }
                if (!CRValidate.validateVariableType(variable, variableType, VariableDictionary))
                {
                    return new ReturnClass(false, variableType, CommonResource.CR_VARIABLE_TYPE_INVALID, new string[] { variableType.Row.ToString() });
                }
                if (!CRValidate.validateVariableChoiceCnt(variable, variableChoiceCnt, VariableDictionary))
                {
                    return new ReturnClass(false, variableChoiceCnt, CommonResource.CR_VARIABLE_CATEGORY_CNT_INVALID, new string[] { variableChoiceCnt.Row.ToString() });
                }
                if (!CRValidate.validateDivSetting(divSetting, Constants.Mark.MarkSep))
                {
                    return new ReturnClass(false, divSetting, CommonResource.CR_ROW_DIV_INVALID, new string[] { divSetting.Row.ToString() });
                }
                if (!validateTableVaraibleTypeSetting(variable, prevSetting, VariableDictionary))
                {
                    return new ReturnClass(false, variable, CommonResource.SL_VARIABLE_SEP_NO_MATCH, new string[] { divSetting.Row.ToString() });
                }
                if (prevSetting == null)
                {
                    if (!String.IsNullOrEmpty(targetCell.Value2))
                    {
                        fistSep = targetCell;
                    }
                    else
                    {
                        fistSep = null;
                    }

                }
                bool hasMedian = false;
                Range targetCellMed = null;
                ReturnClass validateRes = validateTableInputSetting(variable, ref maxCol, fistSep, VariableDictionary, ref hasMedian, ref targetCellMed);
                if (!validateRes.Result)
                {
                    Range val = (Range)validateRes.Value;
                    return new ReturnClass(false, val, CommonResource.SL_INPUT_INVALID, new string[] { val.Row.ToString(), GetExcelColumnName(val.Column) });
                }

                if (hasMedian)
                {
                    if (hasSetWeightback)
                    {
                        return new ReturnClass(false, targetCellMed, CommonResource.SL_MED_IN_WEIGTBACK);
                    }
                }

                if (checkSepPresent(variable))
                {
                    prevSetting = null;
                    maxCol = 0;
                }
                else if (!String.IsNullOrEmpty(targetCell.Value2))
                {
                    prevSetting = targetCell;
                }
            }
            return new ReturnClass(true);
        }
        /// <summary>
        /// Validate the Summary table input settings
        /// </summary>
        /// <param name="variable">Variable list</param>
        /// <param name="colNo">Column number</param>
        /// <param name="fistSep">First separation</param>
        /// <param name="VariableDictionary">Variable details</param>
        /// <param name="hasMedian">bool reference value that will set to true if there is Median</param>
        /// <param name="targetCellMed">Target cell of the Median</param>
        /// <returns>return the the result of the validation as ReturnClass object</returns>
        private static ReturnClass validateTableInputSetting(Range variable, ref int colNo, Range fistSep, Dictionary<string, QuestionSettings> VariableDictionary, ref bool hasMedian, ref Range targetCellMed)
        {
            Worksheet sht = variable.Worksheet;
            Range sLSettingRange;
            if (colNo == 0)
            {
                sLSettingRange = ExcelUtil.EndxlRight(sht.Range[Constants.SL.SLAxesVariableStartAddress]);
                colNo = sLSettingRange.Column;
            }
            sLSettingRange = ExcelUtil.EndxlRight(variable);
            if (sLSettingRange.Column < colNo)
            {
                sLSettingRange = sht.Cells[sLSettingRange.Row, colNo];
            }
            else
            {
                colNo = sLSettingRange.Column;
            }
            if (sLSettingRange.Column <= Constants.SL.SLColInputStart)
            {
                return new ReturnClass(true);
            }

            if (String.IsNullOrEmpty(variable.Value2) || null == fistSep)
            {
                return new ReturnClass(true);
            }

            QuestionSettings questionDetails = null;
            sLSettingRange = sht.Range[variable.Offset[0, 4], sLSettingRange];

            if (VariableDictionary.ContainsKey(variable.Value2.ToString()))
            {
                questionDetails = VariableDictionary[variable.Value2.ToString()];
            }

            if (questionDetails == null)
            {
                return new ReturnClass(false, variable);
            }

            string valueListStr = getValueList(questionDetails);
            string[] valueList = valueListStr.Split(new char[1] { ',' });
            Range firstDiv = null;
            foreach (Range targetCell in sLSettingRange.Cells)
            {
                Range axesVariable = sht.Cells[Constants.SL.SLRow2CRVariable, targetCell.Column];
                string valSet = targetCell.Value2 == null ? null : targetCell.Value2.ToString();//Fix for Redmine Id:228789

                if (firstDiv == null && !String.IsNullOrEmpty(axesVariable.Value2))
                {
                    firstDiv = targetCell;
                }

                if (String.IsNullOrEmpty(valSet) && String.IsNullOrEmpty(axesVariable.Value2))
                {
                    if (checkDivPresent(axesVariable, true))
                    {
                        firstDiv = null;
                    }
                    continue;
                }
                if (!String.IsNullOrEmpty(valSet) && String.IsNullOrEmpty(axesVariable.Value2))
                {
                    return new ReturnClass(false, targetCell);
                }

                if (null != valSet && "MED" == valSet)
                {
                    hasMedian = true;
                    targetCellMed = targetCell;
                }
                if (!string.IsNullOrEmpty(valSet) && !valueList.Contains(valSet))
                {
                    // Added to update list in case if St is disabled.
                    Range endCell = sht.Cells[targetCell.Row, ExcelUtil.EndColumn(targetCell.Worksheet)];
                    Range ItemArea = sht.Range[sht.Cells[targetCell.Row, Constants.SL.SLColInputStart + 1], endCell];
                    ItemArea.Validation.Delete();
                    String catList = SLValidate.getValueList(questionDetails);
                    if (catList.Length > 255)//[Redmine id: 189027]
                    {
                        int cateCnt = questionDetails.CategoryCount;
                        Microsoft.Office.Interop.Excel.Worksheet settingssheet = ExcelUtil.GetWorkSheetByCodeName(targetCell.Application.ActiveWorkbook, Constants.SheetCodeName.Setting);
                        catList = "= INDIRECT(" + "\"" + settingssheet.Name + "!$A$1:$A$" + cateCnt + "\")";
                    }
                    CommonFunctions.CellFormatSetting(ItemArea, catList, "");

                    return new ReturnClass(false, targetCell);
                }

                String prevValue = findSepDivValue(targetCell, fistSep, firstDiv);

                if (valSet != prevValue)
                {
                    return new ReturnClass(false, targetCell);
                }

                if (checkDivPresent(axesVariable, true))
                {
                    firstDiv = null;
                }
            }
            return new ReturnClass(true);
        }

        public static string getValueList(QuestionSettings questionDetails)
        {
            String catList = "";
            if (questionDetails.AnswerType == Constants.AnswerType.N)
            {
                catList = Constants.SL.SLNumericItemList;
            }
            else if (questionDetails.AnswerType == Constants.AnswerType.MA || questionDetails.AnswerType == Constants.AnswerType.SA)
            {
                int cateCnt = questionDetails.CategoryCount;
                catList = CateList(cateCnt, true);
                if (!String.IsNullOrEmpty(questionDetails.Score))
                {
                    catList += ",WT";
                }
                if (!String.IsNullOrEmpty(questionDetails.AddSubTotal) && questionDetails.SubTotalCount > 0)
                {
                    for (int i = 1; i <= questionDetails.SubTotalCount; i++)
                    {
                        catList += ",ST" + i;
                    }
                }
                if (questionDetails.AnswerType == Constants.AnswerType.MA &&
                   !String.IsNullOrEmpty(questionDetails.Count))
                {
                    catList += ",CT";
                }
            }
            return catList;
        }

        private static string findSepDivValue(Range targetCell, Range fistSep, Range fistDiv)
        {
            if (null == fistDiv)
            {
                return null;
            }
            Range valCell = targetCell.Worksheet.Cells[fistSep.Row, fistDiv.Column];
            if (null == valCell.Value2)
            {
                return null;
            }
            return valCell.Value2.ToString();
        }

        private static bool validateTableVaraibleTypeSetting(Range variable, Range prevSetting, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            if (null == prevSetting || null == prevSetting.Value2)
            {
                return true;
            }
            string prevSettingStr = prevSetting.Value2.ToString();

            if (string.IsNullOrEmpty(prevSettingStr)) return true;

            if (null == variable.Value2)
            {
                return true;
            }
            string variableName = variable.Value2.ToString();
            if (string.IsNullOrEmpty(variableName)) return true;

            if (!VariableDictionary.ContainsKey(prevSettingStr))
            {
                return false;
            }
            QuestionSettings prevQuestionDetails = VariableDictionary[prevSettingStr];

            if (!VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }
            QuestionSettings questionDetails = VariableDictionary[variableName];

            if (prevQuestionDetails.AnswerType == Constants.AnswerType.N
                && questionDetails.AnswerType == Constants.AnswerType.N)
            {
                return true;
            }
            else if ((prevQuestionDetails.AnswerType == Constants.AnswerType.MA || prevQuestionDetails.AnswerType == Constants.AnswerType.SA) &&
                (questionDetails.AnswerType == Constants.AnswerType.MA || questionDetails.AnswerType == Constants.AnswerType.SA))
            {
                return true;
            }
            return false;
        }

        internal static string CateList(int cateCnt, bool noDK)
        {
            if (cateCnt == 0)
            {
                return "";
            }
            string catList = String.Join(",", Enumerable.Range(1, cateCnt).Select((x => x)));
            if (noDK == false)
            {
                catList += ",DK,*";
            }
            return catList;
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

        private static bool checkWeightbackOn(Worksheet sht)
        {
            Worksheet shtSettings = getASSheet(sht.Application.ActiveWorkbook);
            bool hasWeightback = false;
            Range start = shtSettings.Cells[1, 1];
            Range end = ExcelUtil.EndxlUp(start);
            Range settingRange = shtSettings.Range[start, end.Offset[0, 1]];
            object[,] settings = settingRange.Value2;
            int j = settings.GetLowerBound(1);
            for (int i = settings.GetLowerBound(0); i <= settings.GetUpperBound(0); i++)
            {
                if (settings[i, j] != null && settings[i, j + 1] != null)
                {
                    string key = Convert.ToString(settings[i, j]);
                    if (Convert.ToString(settings[i, j]) == "F_Cr_Cross_AddUp_Check_Summary_WeightBack_P")
                    {
                        string value = Convert.ToString(settings[i, j + 1]);
                        if (value.ToLower() == "true")
                        {
                            hasWeightback = true;
                        }
                        break;
                    }
                }
            }

            return hasWeightback;
        }

        private static Worksheet getASSheet(Workbook workbook)
        {
            foreach (Worksheet sht in workbook.Worksheets)
            {
                switch (sht.CodeName)
                {
                    case "Sheet93_Cm": //"Sheet93_Cm"
                        return sht;
                    default:
                        break;
                }

            }
            return null;
        }
    }
}
