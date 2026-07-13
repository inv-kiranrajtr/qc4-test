using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
//using QC4Common.Util;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Constants = Qc4Launcher.Util.Constants;

namespace Qc4Launcher.Logic.MultiVariate
{
    internal class MultiVariateSettingsReader
    {
        public static int PSMRowStart = 17;
        public static int PSMRowEnd = 22;
        public static int PSMOnCol = 2;
        public static int PSMFilterVarCol = 3;
        public static int PSMFilterOperatorCol = 4;
        public static int PSMFilterValueCol = 5;
        public static int PSMFilterCondtionCol = 6;
        public static int PSMFilterOnCol = 7;
        public static int PSMQtypeCol = 8;
        public static int PSMHighCol = 9;
        public static int PSMCheapCol = 10;
        public static int PSMTooHighCol = 11;
        public static int PSMTooCheapCol = 12;
        public static int PSMGraphRangeMInCol = 13;
        public static int PSMGraphRangeMaxCol = 14;
        public static int PSMcaleIntervalCol = 15;
        public static int PSMInvertCol = 16;
        public static int PSMValidValOnCol = 17;
        public static int PSMHighValidMinCol = 18;
        public static int PSMHighValidMaxCol = 22;
        public static int PSMCheapValidMinCol = 19;
        public static int PSMCheapValidMaxCol = 23;
        public static int PSMTooHighValidMinCol = 20;
        public static int PSMTooHighValidMaxCol = 24;
        public static int PSMTooCheapValidMinCol = 21;
        public static int PSMTooCheapValidMaxCol = 25;
        public static string PSMAnalysitring = "PSMAnalysis";

        public static int CARowStart = 23;
        public static int CARowEnd = 28;
        public static int CAOnCol = 2;
        public static int CAFilterVarCol = 3;
        public static int CAFilterOperatorCol = 4;
        public static int CAFilterValueCol = 5;
        public static int CAFilterCondtionCol = 6;
        public static int CAFilterOnCol = 7;
        public static int CATabulationTypeCol = 8;
        public static int CADimensionCol = 9;
        public static int CAHorizontalNoCol = 10;
        public static int CAVerticalNoCol = 11;
        public static int CAHorizontalRevCol = 12;
        public static int CAVerticalRevCol = 13;
        public static int CACalcTypeCol = 14;
        public static int CACrRowVarCol = 15;
        public static int CACRRowChoiceCol = 16;
        public static int CACrColVarCol = 17;
        public static int CACRColChoiceCol = 18;
        public static int CAGTChoiceCol = 19;
        public static int CAGtVarStartCol = 20;
        public static string CAalysitring = "CorrespondenceAnalysis";


        public static string ANDStr = "AND";
        public static string ORStr = "OR";
        public static string filterCondtionPrev;
        internal static PSMSettings ReadPSMSettings(Workbook workBook, Dictionary<string, QuestionSettings> variableDictionary)
        {
            PSMSettings pSMSettings = new PSMSettings();
            filterCondtionPrev = "";
            Worksheet sht = ExcelUtil.GetWorkSheetBySheetName(workBook, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.MultiVariate);
            if (sht == null)
            {
                return null;
            }
            Range valuesRange = sht.Range["A" + PSMRowStart, sht.Cells[PSMRowEnd, PSMTooCheapValidMaxCol]];
            object[,] values = (object[,])valuesRange.Value2;
            for (int x = 1; x <= values.GetLength(0); x += 1)
            {
                string on = Convert.ToString(values[x, PSMOnCol]);
                if (string.IsNullOrEmpty(on) || "○" != on && "On" != on) // todo 
                {
                    continue;
                }

                string filterVar = Convert.ToString(values[x, PSMFilterVarCol]);
                string filterOperator = Convert.ToString(values[x, PSMFilterOperatorCol]);
                string filterValue = Convert.ToString(values[x, PSMFilterValueCol]);
                string filterCondtion = Convert.ToString(values[x, PSMFilterCondtionCol]);

                if (!string.IsNullOrEmpty(filterVar) && !string.IsNullOrEmpty(filterOperator)
                    && !string.IsNullOrEmpty(filterValue) && !string.IsNullOrEmpty(filterCondtion))
                {

                    if (pSMSettings.Filters == null)
                    {
                        pSMSettings.Filters = new List<FilterSettingsCr>();
                    }

                    FilterSettingsCr fs = new FilterSettingsCr();
                    pSMSettings.Filters.Add(fs);
                    fs.variable = filterVar;
                    fs.operatorType = filterOperator;
                    fs.values = filterValue;
                    fs.conditionType = filterCondtionPrev;
                    if (filterCondtion == ANDStr)
                    {
                        filterCondtionPrev = CrossSettingsReader.AND;

                    }
                    else
                    {
                        filterCondtionPrev = CrossSettingsReader.OR;

                    }

                    if (PSMAnalysitring != filterCondtion)
                    {
                        continue;
                    }
                }
                string hasFilter = Convert.ToString(values[x, PSMFilterOnCol]);
                pSMSettings.HasFilter = hasFilter == "1" ? true : false;
                if (pSMSettings.Filters == null)
                {
                    pSMSettings.HasFilter = false;
                }
                string qtype = Convert.ToString(values[x, PSMQtypeCol]);
                string high = Convert.ToString(values[x, PSMHighCol]);
                string cheap = Convert.ToString(values[x, PSMCheapCol]);
                string tooHigh = Convert.ToString(values[x, PSMTooHighCol]);
                string tooCheap = Convert.ToString(values[x, PSMTooCheapCol]);
                string graphRangeMIn = Convert.ToString(values[x, PSMGraphRangeMInCol]);
                string graphRangeMax = Convert.ToString(values[x, PSMGraphRangeMaxCol]);
                string scaleInterval = Convert.ToString(values[x, PSMcaleIntervalCol]);
                if (!validateVariable(high, variableDictionary) || !validateVariable(cheap, variableDictionary) || !validateVariable(tooHigh, variableDictionary)
                    || !validateVariable(tooCheap, variableDictionary)
                     || string.IsNullOrEmpty(graphRangeMIn) || string.IsNullOrEmpty(graphRangeMax) || string.IsNullOrEmpty(scaleInterval))
                {
                    return null;
                }
                string invert = Convert.ToString(values[x, PSMInvertCol]);
                string validValOn = Convert.ToString(values[x, PSMValidValOnCol]);
                string highValidMin = Convert.ToString(values[x, PSMHighValidMinCol]);
                string highValidMax = Convert.ToString(values[x, PSMHighValidMaxCol]);
                string cheapValidMin = Convert.ToString(values[x, PSMCheapValidMinCol]);
                string cheapValidMax = Convert.ToString(values[x, PSMCheapValidMaxCol]);
                string tooHighValidMin = Convert.ToString(values[x, PSMTooHighValidMinCol]);
                string tooHighValidMax = Convert.ToString(values[x, PSMTooHighValidMaxCol]);
                string tooCheapValidMin = Convert.ToString(values[x, PSMTooCheapValidMinCol]);
                string tooCheapValidMax = Convert.ToString(values[x, PSMTooCheapValidMaxCol]);
                pSMSettings.questionType = "SA" == qtype ? Macromill.QCWeb.Tabulation.QuestionType.SA : Macromill.QCWeb.Tabulation.QuestionType.N;
                pSMSettings.high = high;
                pSMSettings.cheap = cheap;
                pSMSettings.tooHigh = tooHigh;
                pSMSettings.tooCheap = tooCheap;
                pSMSettings.minPrice = Convert.ToDouble(graphRangeMIn);
                pSMSettings.maxPrice = Convert.ToDouble(graphRangeMax);
                pSMSettings.scaleInterval = Convert.ToDouble(scaleInterval);
                pSMSettings.invertHighAndCheap = invert == "1" ? true : false;
                pSMSettings.HasvalidValue = validValOn == "1" ? true : false;
                if (pSMSettings.HasvalidValue)
                {
                    if (!string.IsNullOrEmpty(highValidMin))
                        pSMSettings.effValHighStart = Convert.ToDouble(highValidMin);
                    if (!string.IsNullOrEmpty(highValidMax))
                        pSMSettings.effValHighEnd = Convert.ToDouble(highValidMax);
                    if (!string.IsNullOrEmpty(cheapValidMin))
                        pSMSettings.effValCheapStart = Convert.ToDouble(cheapValidMin);
                    if (!string.IsNullOrEmpty(cheapValidMax))
                        pSMSettings.effValCheapEnd = Convert.ToDouble(cheapValidMax);
                    if (!string.IsNullOrEmpty(tooHighValidMin))
                        pSMSettings.effValTooHighStart = Convert.ToDouble(tooHighValidMin);
                    if (!string.IsNullOrEmpty(tooHighValidMax))
                        pSMSettings.effValTooHighEnd = Convert.ToDouble(tooHighValidMax);
                    if (!string.IsNullOrEmpty(tooCheapValidMin))
                        pSMSettings.effValTooCheapStart = Convert.ToDouble(tooCheapValidMin);
                    if (!string.IsNullOrEmpty(tooCheapValidMax))
                        pSMSettings.effValTooCheapEnd = Convert.ToDouble(tooCheapValidMax);
                }
                break;
            }


            return pSMSettings;
        }

        internal static CorrespondenceSettings ReadCorrespondenceSettings(Workbook workBook, Dictionary<string, QuestionSettings> variableDictionary)
        {
            CorrespondenceSettings pSMSettings = new CorrespondenceSettings();
            filterCondtionPrev = "";
            Worksheet sht = ExcelUtil.GetWorkSheetBySheetName(workBook, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.MultiVariate);
            if (sht == null)
            {
                return null;
            }
            Range valuesRange = sht.Range["A" + CARowStart, "A" + CARowEnd];
            valuesRange = valuesRange.EntireRow.Cells.Find("*", SearchOrder: XlSearchOrder.xlByColumns, SearchDirection: XlSearchDirection.xlPrevious);
            int lastCol = valuesRange.Column;
            valuesRange = sht.Range["A" + CARowStart, sht.Cells[CARowEnd, lastCol]];

            object[,] values = (object[,])valuesRange.Value2;
            for (int x = 1; x <= values.GetLength(0); x += 1)
            {
                string on = Convert.ToString(values[x, PSMOnCol]);
                if (string.IsNullOrEmpty(on) || "○" != on && "On" != on) // todo 
                {
                    continue;
                }

                string filterVar = Convert.ToString(values[x, PSMFilterVarCol]);
                string filterOperator = Convert.ToString(values[x, PSMFilterOperatorCol]);
                string filterValue = Convert.ToString(values[x, PSMFilterValueCol]);
                string filterCondtion = Convert.ToString(values[x, PSMFilterCondtionCol]);

                if (!string.IsNullOrEmpty(filterVar) && !string.IsNullOrEmpty(filterOperator)
                    && !string.IsNullOrEmpty(filterValue) && !string.IsNullOrEmpty(filterCondtion))
                {

                    if (pSMSettings.Filters == null)
                    {
                        pSMSettings.Filters = new List<FilterSettingsCr>();
                    }

                    FilterSettingsCr fs = new FilterSettingsCr();
                    pSMSettings.Filters.Add(fs);
                    fs.variable = filterVar;
                    fs.operatorType = filterOperator;
                    fs.values = filterValue;
                    fs.conditionType = filterCondtionPrev;
                    if (filterCondtion == ANDStr)
                    {
                        filterCondtionPrev = CrossSettingsReader.AND;

                    }
                    else
                    {
                        filterCondtionPrev = CrossSettingsReader.OR;

                    }

                    if (CAalysitring != filterCondtion)
                    {
                        continue;
                    }
                }
                string hasFilter = Convert.ToString(values[x, PSMFilterOnCol]);
                pSMSettings.HasFilter = hasFilter == "1" ? true : false;
                if (pSMSettings.Filters == null)
                {
                    pSMSettings.HasFilter = false;
                }

                string tabulationType = Convert.ToString(values[x, CATabulationTypeCol]);
                int tabType = 1;
                try
                {
                    tabType = Convert.ToInt32(tabulationType);
                }
                catch (Exception) { }
                string noOfDimension = Convert.ToString(values[x, CADimensionCol]);
                string horizontalNo = Convert.ToString(values[x, CAHorizontalNoCol]);
                string verticalNo = Convert.ToString(values[x, CAVerticalNoCol]);
                string horizontalRevData = Convert.ToString(values[x, CAHorizontalRevCol]);
                string verticalRevData = Convert.ToString(values[x, CAVerticalRevCol]);
                string calcType = Convert.ToString(values[x, CACalcTypeCol]);
                string crRowVar = Convert.ToString(values[x, CACrRowVarCol]);
                string crColVar = Convert.ToString(values[x, CACrColVarCol]);
                string crRowChoiceCnt = Convert.ToString(values[x, CACRRowChoiceCol]);
                string crColChoiceCnt = Convert.ToString(values[x, CACRColChoiceCol]);
                string gtChoiceCnt = "0";
                List<string> gtvars = new List<string>();
                if (tabType == 2)
                {
                    gtChoiceCnt = Convert.ToString(values[x, CAGTChoiceCol]);
                    for (int y = CAGtVarStartCol; y <= lastCol; y++)
                    {
                        string gtvar = Convert.ToString(values[x, y]);
                        gtvars.Add(gtvar);
                    }
                    foreach (string gtvar in gtvars)
                    {
                        if (!validateVariable(gtvar, variableDictionary, false))
                        {
                            return null;
                        }
                    }
                    if (string.IsNullOrEmpty(gtChoiceCnt))
                    {
                        return null;
                    }
                }
                else
                {
                    if (!validateVariable(crRowVar, variableDictionary, false) || !validateVariable(crColVar, variableDictionary, false)
                        || string.IsNullOrEmpty(crRowChoiceCnt) || string.IsNullOrEmpty(crColChoiceCnt))
                    {
                        return null;
                    }
                }
                //string.IsNullOrEmpty(tabulationType) ||
                if ( string.IsNullOrEmpty(noOfDimension) || string.IsNullOrEmpty(horizontalNo)
                    || string.IsNullOrEmpty(verticalNo) || string.IsNullOrEmpty(horizontalRevData) || string.IsNullOrEmpty(verticalRevData)
                    || string.IsNullOrEmpty(calcType))
                {
                    return null;
                }
                pSMSettings.tabulationType = tabType;
                pSMSettings.noOfDimension = Convert.ToInt32(noOfDimension);
                pSMSettings.horizontalNo = Convert.ToInt32(horizontalNo);
                pSMSettings.verticalNo = Convert.ToInt32(verticalNo);
                pSMSettings.horizontalRevData = horizontalRevData == "1" ? true : false; // Convert.ToBoolean(horizontalRevData);
                pSMSettings.verticalRevData = verticalRevData == "1" ? true : false; // Convert.ToBoolean(verticalRevData);
                pSMSettings.calcType = Convert.ToInt32(calcType);
                pSMSettings.crRowVar = crRowVar;
                pSMSettings.crColVar = crColVar;
                if (tabType == 1)
                {
                    pSMSettings.crRowChoiceCnt = Convert.ToInt32(crRowChoiceCnt);
                    pSMSettings.crColChoiceCnt = Convert.ToInt32(crColChoiceCnt);
                }
                else
                {
                    pSMSettings.gtChoiceCnt = Convert.ToInt32(gtChoiceCnt);
                    pSMSettings.gtVars = gtvars.ToList();

                }
                break;
            }


            return pSMSettings;
        }

        public static bool validateVariable(string variable, Dictionary<string, QuestionSettings> VariableDictionary, bool isPSM = true)
        {
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
            if ((isPSM && (questionDetails.AnswerType == Constants.AnswerType.SA || questionDetails.AnswerType == Constants.AnswerType.N))
                || (!isPSM && (questionDetails.AnswerType == Constants.AnswerType.SA || questionDetails.AnswerType == Constants.AnswerType.MA)))
            {
                return true;
            }
            return false;
        }
    }
}