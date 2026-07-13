using log4net;
using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExcelAddIn.Common
{
    internal class SLValidate
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static ReturnClass validate()
        {
            _log.Info("Starting Summary List validation");
            Worksheet sht = getSLSheet();
            if (null == sht) return null;

            ReturnClass returnClass = validateOutputEnablesettings(sht);
            if (null != returnClass && returnClass.Result == false)
            {
                return returnClass;
            }

            returnClass = validateAxisSLVAraible(sht, Constants.SL.SLAxesVariableStartAddress);
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
                    return new ReturnClass(false, sLSetting, AddinResource.SL_OUTPUT_ENABLE_INVALID, new string[] { sLSetting.Column.ToString() });
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
            if (null == sLSetting.Value2)
            {
                return true;
            }
            string sLSettingStr = sLSetting.Value2.ToString();
            if (string.IsNullOrEmpty(sLSettingStr)) return true;

            if (!(AddinResource.MarkWhiteCircle == sLSettingStr
                || AddinResource.MarkOFF == sLSettingStr
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
                    return new ReturnClass(false, divSetting, AddinResource.CR_COL_DIV_INVALID, new string[] { divSetting.Column.ToString() });
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateAxisSLVAraible(Worksheet sht, string address)
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
                if (!CRValidate.validateVariable(variable, false))
                {
                    return new ReturnClass(false, variable, AddinResource.CR_AXES_VARIABLE_INVALID, new string[] { variable.Column.ToString() });
                }
                if (!CRValidate.validateVariableType(variable, variableType))
                {
                    return new ReturnClass(false, variableType, AddinResource.CR_AXES_VARIABLE_TYPE_INVALID, new string[] { variableType.Column.ToString() });
                }
                if (!CRValidate.validateVariableChoiceCnt(variable, variableChoiceCnt))
                {
                    return new ReturnClass(false, variableChoiceCnt, AddinResource.CR_AXES_VARIABLE_CATEGORY_CNT_INVALID, new string[] { variableChoiceCnt.Column.ToString() });
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateVariableSection(Worksheet sht)
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
                if (!CRValidate.validateVariable(variable))
                {
                    return new ReturnClass(false, variable, AddinResource.CR_VARIABLE_INVALID, new string[] { variable.Row.ToString() });
                }
                if (!CRValidate.validateVariableType(variable, variableType))
                {
                    return new ReturnClass(false, variableType, AddinResource.CR_VARIABLE_TYPE_INVALID, new string[] { variableType.Row.ToString() });
                }
                if (!CRValidate.validateVariableChoiceCnt(variable, variableChoiceCnt))
                {
                    return new ReturnClass(false, variableChoiceCnt, AddinResource.CR_VARIABLE_CATEGORY_CNT_INVALID, new string[] { variableChoiceCnt.Row.ToString() });
                }
                if (!CRValidate.validateDivSetting(divSetting, Constants.Mark.MarkSep))
                {
                    return new ReturnClass(false, divSetting, AddinResource.CR_ROW_DIV_INVALID, new string[] { divSetting.Row.ToString() });
                }
                if (!validateTableVaraibleTypeSetting(variable, prevSetting))
                {
                    return new ReturnClass(false, variable, AddinResource.SL_VARIABLE_SEP_NO_MATCH, new string[] { divSetting.Row.ToString() });
                }
                if (prevSetting == null)
                {
                    if (targetCell.Value2 != null)
                    {
                        fistSep = targetCell;
                    }
                    else
                    {
                        fistSep = null;
                    }

                }
                ReturnClass validateRes = validateTableInputSetting(variable, ref maxCol, fistSep);
                if (!validateRes.Result)
                {
                    Range val = (Range)validateRes.Value;
                    return new ReturnClass(false, val, AddinResource.SL_INPUT_INVALID, new string[] { val.Row.ToString(), val.Column.ToString() });
                }

                if (checkSepPresent(variable))
                {
                    prevSetting = null;
                    maxCol = 0;
                }
                else if (targetCell.Value2 != null)
                {
                    prevSetting = targetCell;
                }
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateTableInputSetting(Range variable, ref int colNo, Range fistSep)
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

            if (null == variable.Value2 || null == fistSep)
            {
                return new ReturnClass(true);
            }

            QuestionSettings questionDetails = null;
            sLSettingRange = sht.Range[variable.Offset[0, 4], sLSettingRange];

            if (Definitions.VariableDictionary.ContainsKey(variable.Value2.ToString()))
            {
                questionDetails = Definitions.VariableDictionary[variable.Value2.ToString()];
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

                if (firstDiv == null && null != axesVariable.Value2)
                {
                    firstDiv = targetCell;
                }

                if (null == targetCell.Value2 && null == axesVariable.Value2)
                {
                    if (checkDivPresent(axesVariable, true))
                    {
                        firstDiv = null;
                    }
                    continue;
                }
                if (null != targetCell.Value2 && null == axesVariable.Value2)
                {
                    return new ReturnClass(false, targetCell);
                }

                string valSet = targetCell.Value2 == null ? null : targetCell.Value2.ToString();
                if (!string.IsNullOrEmpty(valSet) && !valueList.Contains(valSet))
                {
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
            String sep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            String catList = "";
            if (questionDetails.AnswerType == Constants.AnswerType.N)
            {
                catList = Constants.SL.SLNumericItemList.Replace(",", sep);
                            }
            else if (questionDetails.AnswerType == Constants.AnswerType.MA || questionDetails.AnswerType == Constants.AnswerType.SA)
            {
                int cateCnt = questionDetails.CategoryCount;
                catList = CommonFunctions.CateList(cateCnt, true);
                if (!String.IsNullOrEmpty(questionDetails.Score))
                {
                    catList += sep+"WT";
                }
                if (!String.IsNullOrEmpty(questionDetails.AddSubTotal) && questionDetails.SubTotalCount > 0)
                {
                    for (int i = 1; i <= questionDetails.SubTotalCount; i++)
                    {
                        catList += sep+"ST" + i;
                    }
                }
                if (questionDetails.AnswerType == Constants.AnswerType.MA &&
                   !String.IsNullOrEmpty(questionDetails.Count))
                {
                    catList += sep+"CT";
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

        private static bool validateTableVaraibleTypeSetting(Range variable, Range prevSetting)
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

            if (!Definitions.VariableDictionary.ContainsKey(prevSettingStr))
            {
                return false;
            }
            QuestionSettings prevQuestionDetails = Definitions.VariableDictionary[prevSettingStr];

            if (!Definitions.VariableDictionary.ContainsKey(variableName))
            {
                return false;
            }
            QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];

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

        public static Worksheet getSLSheet()
        {
            foreach (Worksheet sht in Globals.ThisAddIn.Application.Worksheets)
            {
                switch (sht.CodeName)
                {
                    case Common.Constants.SheetType.sh_SummaryList:
                        return sht;
                    default:
                        break;
                }

            }
            return null;
        }

    }
}