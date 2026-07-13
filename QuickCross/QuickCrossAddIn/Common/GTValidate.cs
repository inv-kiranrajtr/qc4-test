using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelAddIn.Common
{
    class GTValidate
    {
        public static ReturnClass validate(Microsoft.Office.Interop.Excel.Sheets sHeets = null,bool isFromSTD=false)
        {
            Worksheet sht = getGTSheet(sHeets, isFromSTD);
            if (null == sht) return null;

            Range gtSettingRange = ExcelUtil.EndxlUp(sht.Range[Constants.GT.GTStartAddress]);
            if (gtSettingRange.Row < Constants.GT.GtRowDataStart)
            {
                return null;
            }
            gtSettingRange = sht.Range[sht.Range[Constants.GT.GTStartAddress], gtSettingRange];

            foreach (Range targetCell in gtSettingRange.Cells)
            {
                if (!validateExecCol(targetCell))
                {
                    int rowNum = isFromSTD ? Convert.ToInt32(targetCell.Row.ToString()) - 4 : Convert.ToInt32(targetCell.Row.ToString());
                    return new ReturnClass(false, targetCell, AddinResource.GT_EXEC_INVALID, new string[] { " " + rowNum.ToString() });
                }

                if (CheckExecColOff(targetCell))
                {
                    continue;
                }

                Range charType = targetCell.Offset[0, 1];
                if (!validateChartType(charType))
                {
                    int rowNum = isFromSTD ? Convert.ToInt32(charType.Row.ToString()) - 4 : Convert.ToInt32(charType.Row.ToString());
                    return new ReturnClass(false, charType, AddinResource.GT_CHART_TYPE_INVALID, new string[] { " " + rowNum.ToString() });
                }

                Range testType = targetCell.Offset[0, 2];
                if (!validateTestType(testType, charType))
                {
                    int rowNum = isFromSTD ? Convert.ToInt32(testType.Row.ToString()) - 4 : Convert.ToInt32(testType.Row.ToString());
                    return new ReturnClass(false, testType, AddinResource.GT_TEST_INVALID, new string[] { " " + rowNum.ToString() });
                }

                Range graphType = targetCell.Offset[0, 3];
                if (!validateGraphType(graphType, charType))
                {
                    int rowNum = isFromSTD ? Convert.ToInt32(graphType.Row.ToString()) - 4 : Convert.ToInt32(graphType.Row.ToString());
                    return new ReturnClass(false, graphType, AddinResource.GT_GRAPH_INVALID, new string[] { " " + rowNum.ToString() });
                }

                Range tableHeading = targetCell.Offset[0, 4];
                if (!validateTableHeading(tableHeading, charType))
                {
                    int rowNum = isFromSTD ? Convert.ToInt32(tableHeading.Row.ToString()) - 4 : Convert.ToInt32(tableHeading.Row.ToString());
                    return new ReturnClass(false, tableHeading, AddinResource.GT_TABLE_HEADING_INVALID, new string[] { " " + rowNum.ToString() });
                }

                Range itemArea = targetCell.Offset[0, 5];
                Range invalid = validateItemArea(itemArea, charType, sht);
                if (invalid != null)
                {
                    int rowNum = isFromSTD ? Convert.ToInt32(invalid.Row.ToString()) - 4 : Convert.ToInt32(invalid.Row.ToString());
                    return new ReturnClass(false, invalid, AddinResource.GT_TABULATION_ITEM_INVALID, new string[] { " " + rowNum.ToString() });
                }
            }
            return new ReturnClass(true);
        }

        private static Range validateItemArea(Range itemAreaStart, Range charType, Worksheet sht)
        {
            if (null == charType.Value2)
            {
                return null;
            }
            string charTypeVal = charType.Value2.ToString();
            if (string.IsNullOrEmpty(charTypeVal))
            {
                return null;
            }
            Range itemArea = sht.Range[itemAreaStart, itemAreaStart.Offset[0, Constants.GT.GtMaxItemNo]];
            foreach (Range itemCell in itemArea.Cells)
            {
                if (null == itemCell.Value2)
                {
                    break;
                }
                string variableName = itemCell.Value2.ToString();
                if (string.IsNullOrEmpty(variableName))
                {
                    break;
                }
                if (!Definitions.VariableDictionary.ContainsKey(variableName))
                {
                    return itemCell;
                }
                QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];
                switch (charTypeVal)
                {
                    case Constants.GT.GTN:
                        if (questionDetails.AnswerType.Equals("N"))
                        {
                            return null;
                        }
                        return itemCell;
                    case Constants.GT.GTSA:
                        if (questionDetails.AnswerType.Equals("SA"))
                        {
                            return null;
                        }
                        return itemCell;
                    case Constants.GT.GTMA:
                        if (questionDetails.AnswerType.Equals("MA"))
                        {
                            return null;
                        }
                        return itemCell;
                    case Constants.GT.GTMTS:
                    case Constants.GT.GTRNK:
                        if (questionDetails.AnswerType.Equals("SA"))
                        {
                            break;
                        }
                        return itemCell;
                    case Constants.GT.GTMTM:
                        if (questionDetails.AnswerType.Equals("SA") || questionDetails.AnswerType.Equals("MA"))
                        {
                            break;
                        }
                        return itemCell;
                    case Constants.GT.GTMTN:
                    case Constants.GT.GTRAT:
                        if (questionDetails.AnswerType.Equals("N"))
                        {
                            break;
                        }
                        return itemCell;
                    default:
                        break;
                }

            }
            return null;
        }

        private static bool validateTableHeading(Range tableHeading, Range charType)
        {
            if (null == tableHeading.Value2 || null == charType.Value2)
            {
                return true;
            }
            string tableHeadingVal = tableHeading.Value2.ToString();
            string charTypeVal = charType.Value2.ToString();

            switch (charTypeVal)
            {
                case Constants.GT.GTN:
                case Constants.GT.GTSA:
                case Constants.GT.GTMA:
                    if (!string.IsNullOrEmpty(tableHeadingVal))
                    {
                        return false;
                    }
                    break;
                default:
                    return true;
            }
            return true;
        }

        private static bool validateGraphType(Range graphType, Range charType)
        {
            if (null == graphType.Value2 || null == charType.Value2)
            {
                return true;
            }

            string graphTypeVal = graphType.Value2.ToString();
            string charTypeVal = charType.Value2.ToString();

            if (string.IsNullOrEmpty(graphTypeVal) || string.IsNullOrEmpty(charTypeVal))
            {
                return true;
            }

            switch (charTypeVal)
            {
                case Common.Constants.GT.GTSA:
                    if (QC4Common.Common.Constants.GTGraphQCPieChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQC100StackedBarChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQC100StackedColumnChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCBarChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCColumnChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCPieChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQC100StackedBarChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQC100StackedColumnChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCBarChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCColumnChartJP.Equals(graphTypeVal)
                        )
                    {
                        return true;
                    }
                    break;
                case Common.Constants.GT.GTMA:
                    if (QC4Common.Common.Constants.GTGraphQCBarChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCColumnChart.Equals(graphTypeVal)

                        || QC4Common.Common.Constants.GTGraphQCBarChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCColumnChartJP.Equals(graphTypeVal)
                        )
                    {
                        return true;
                    }
                    break;
                case Common.Constants.GT.GTRAT:
                    if (QC4Common.Common.Constants.GTGraphQCPieRATChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCRATBarChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCRATColumnChart.Equals(graphTypeVal)

                        || QC4Common.Common.Constants.GTGraphQCPieRATChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCRATBarChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCRATColumnChartJP.Equals(graphTypeVal)
                        )
                    {
                        return true;
                    }
                    break;
                case Common.Constants.GT.GTRNK:
                    if (QC4Common.Common.Constants.GTGraphQCStackedBarChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCStackedColumnChart.Equals(graphTypeVal)

                        || QC4Common.Common.Constants.GTGraphQCStackedBarChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCStackedColumnChartJP.Equals(graphTypeVal)
                        )
                    {
                        return true;
                    }
                    break;
                case Common.Constants.GT.GTMTS:
                    if (QC4Common.Common.Constants.GTGraphQC100StackedBarChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQC100StackedColumnChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCMPieChart.Equals(graphTypeVal)

                        || QC4Common.Common.Constants.GTGraphQC100StackedBarChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQC100StackedColumnChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCMPieChartJP.Equals(graphTypeVal)
                        )
                    {
                        return true;
                    }
                    break;
                case Common.Constants.GT.GTMTM:
                    if (QC4Common.Common.Constants.GTGraphQCMBarChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCMColumnChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCBarChart.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCColumnChart.Equals(graphTypeVal)

                        || QC4Common.Common.Constants.GTGraphQCMBarChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCMColumnChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCBarChartJP.Equals(graphTypeVal)
                        || QC4Common.Common.Constants.GTGraphQCColumnChartJP.Equals(graphTypeVal)
                        )
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }
            return false;
        }

        private static bool validateTestType(Range testType, Range charType)
        {
            if (null == testType.Value2)
            {
                return true;
            }
            if (null == charType.Value2)
            {
                return true;
            }

            string testTypeVal = testType.Value2.ToString();
            string charTypeVal = charType.Value2.ToString();

            if (string.IsNullOrEmpty(testTypeVal) || string.IsNullOrEmpty(charTypeVal))
            {
                return true;
            }

            switch (charTypeVal)
            {
                case Constants.GT.GTSA:
                case Constants.GT.GTMA:
                    if ("1".Equals(testTypeVal))
                    {
                        return true;
                    }
                    break;
                case Constants.GT.GTRAT:
                case Constants.GT.GTMTN:
                    if ("2".Equals(testTypeVal))
                    {
                        return true;
                    }
                    break;
                case Constants.GT.GTRNK:
                case Constants.GT.GTMTS:
                case Constants.GT.GTMTM:
                    if ("1".Equals(testTypeVal) || "2".Equals(testTypeVal))
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }
            return false;
        }

        private static bool validateChartType(Range targetCell)
        {
            if (checkChartTypeExist(targetCell.Value2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool checkChartTypeExist(dynamic value2)
        {
            if (null == value2)
            {
                return true;
            }
            if (string.IsNullOrEmpty(value2.ToString()))
            {
                return true;
            }
            switch (value2.ToString())
            {
                case Common.Constants.GT.GTSA:
                case Common.Constants.GT.GTMA:
                case Common.Constants.GT.GTN:
                case Common.Constants.GT.GTRAT:
                case Common.Constants.GT.GTRNK:
                case Common.Constants.GT.GTMTS:
                case Common.Constants.GT.GTMTN:
                case Common.Constants.GT.GTMTM:
                    return true;
                default:
                    return false;
            }
        }

        private static bool validateExecCol(Range targetCell)
        {
            if (null == targetCell.Value2)
            {
                return true;
            }
            if (string.IsNullOrEmpty(targetCell.ToString()))
            {
                return true;
            }
            if (QC4Common.Common.Constants.MarkOFF.Equals(targetCell.Value2.ToString(), StringComparison.InvariantCultureIgnoreCase)
                || QC4Common.Common.Constants.MarkON.Equals(targetCell.Value2.ToString(), StringComparison.InvariantCultureIgnoreCase)
                || QC4Common.Common.Constants.MarkWhiteCircle.Equals(targetCell.Value2.ToString(), StringComparison.InvariantCultureIgnoreCase)
                || QC4Common.Common.Constants.MarkStar.Equals(targetCell.Value2.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check the GT item is On or Off
        /// </summary>
        /// <param name="targetCell">Target GT item</param>
        /// <returns>Return voll value whether the GT item is On or Off</returns>
        private static bool CheckExecColOff(Range targetCell)
        {
            if (null == targetCell.Value2)
            {
                return false;
            }
            if (string.IsNullOrEmpty(targetCell.ToString()))
            {
                return false;
            }
            if (AddinResource.MarkOFFForValidateGT.Equals(targetCell.Value2.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        private static Worksheet getGTSheet(Microsoft.Office.Interop.Excel.Sheets sHeets = null, bool isFromSTD = false)
        {
            //foreach (Worksheet sht in Globals.ThisAddIn.Application.Worksheets)
            if (sHeets == null)
            {
                foreach (Worksheet sht in Globals.ThisAddIn.Application.Worksheets)
                {
                    if (isFromSTD)
                    {
                        switch (sht.CodeName)
                        {
                            case Common.Constants.SheetType.GTHidden:
                                return sht;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (sht.CodeName)
                        {
                            case Common.Constants.SheetType.GTCounting:
                                return sht;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                foreach (Worksheet sht in sHeets)
                {
                    if (isFromSTD)
                    {
                        switch (sht.CodeName)
                        {
                            case Common.Constants.SheetType.GTHidden:
                                return sht;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (sht.CodeName)
                        {
                            case Common.Constants.SheetType.GTCounting:
                                return sht;
                            default:
                                break;
                        }
                    }
                }
            }
            return null;
        }
    }
}
