using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExcelAddIn.Common;
using log4net;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using QC4Common.Classes.HatchColor;
using QC4Common.Model;
using Qc4Launcher.Logic.CombineBanner;
using static Macromill.QCWeb.Question.Questions;
using static Qc4Launcher.Util.Constants;
using Constants = ExcelAddIn.Common.Constants;
using XlPageOrientation = Microsoft.Office.Interop.Excel.XlPageOrientation;
using XlPaperSize = Microsoft.Office.Interop.Excel.XlPaperSize;

namespace Qc4Launcher.Logic
{
    internal class CrossSettingsReader
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static string AND = "&";
        public static string OR = "|";
        public static string F_Cr_Cross_AddUp_Check_Summary_Mark_Ranking = "F_Cr_Cross_AddUp_Check_Summary_Mark_Ranking";
        public static string F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1 = "F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1";
        public static string F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 = "F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1";
        public static string F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 = "F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2";
        public static string F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1 = "F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1";
        public static string F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2 = "F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2";
        public static string F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1 = "F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1";
        public static string F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2 = "F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2";
        public static string F_Cr_Cross_AddUp_Check_Summary_SignificantDifferece_Test = "F_Cr_Cross_AddUp_Check_Summary_SignificantDifferece_Test";
        public static string F_Cr_Cross_AddUp_Combo_SignificantDifference_Test = "F_Cr_Cross_AddUp_Combo_SignificantDifference_Test";
        public static string F_Cr_Cross_AddUp_Check_SignificantDifference_Test_Total = "F_Cr_Cross_AddUp_rd_btn_chk";
        public static string F_Cr_Cross_AddUp_Check_SignificantDifference_Test_Choice = "F_Cr_Cross_AddUp_rd_btn_chk2";
        public static string F_Cr_Cross_AddUp_Check_Par_99 = "F_Cr_Cross_AddUp_Check_Par_99";
        public static string F_Cr_Cross_AddUp_Check_Par_95 = "F_Cr_Cross_AddUp_Check_Par_95";
        public static string F_Cr_Cross_AddUp_Check_Par_90 = "F_Cr_Cross_AddUp_Check_Par_90";
        public static string F_Cr_Cross_AddUp_Text_Summary_Mark_N_Equal = "F_Cr_Cross_AddUp_Text_Summary_Mark_N_Equal";

        //np,n,p
        public static string F_Cr_Cross_AddUp_Check_Output_Cross_N_Par_One = "F_Cr_Cross_AddUp_Check_Output_Cross_N_Par";
        public static string F_Cr_Cross_AddUp_Check_Output_Cross_N_One = "F_Cr_Cross_AddUp_Check_Output_Cross_N";
        public static string F_Cr_Cross_AddUp_Check_Output_Cross_Par_One = "F_Cr_Cross_AddUp_Check_Output_Cross_Par";
        //multi / individual
        public static string F_Cr_Cross_AddUp_Option_Output_SheetType_One = "F_Cr_Cross_AddUp_Option_Output_SheetType_One";
        public static string F_Cr_Cross_AddUp_Option_Output_SheetType_Plural = "F_Cr_Cross_AddUp_Option_Output_SheetType_Plural";
        //show no answer
        public static string F_Cr_Cross_AddUp_Check_Summary_Non1 = "F_Cr_Cross_AddUp_Check_Summary_Non1";
        public static string F_Cr_Cross_AddUp_Check_Summary_Non2 = "F_Cr_Cross_AddUp_Check_Summary_Non2";
        //heading
        public static string F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku = "F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku";
        public static string F_Cr_Cross_AddUp_Text_Summary_Change_Non = "F_Cr_Cross_AddUp_Text_Summary_Change_Non";
        public static string F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou = "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou";

        public static string F_Cr_Cross_AddUp_Check_All_Base = "F_Cr_Cross_AddUp_Check_All_Base";

        //div
        public static string F_Cr_Cross_AddUp_Combo_Classify_Item = "F_Cr_Cross_AddUp_Combo_Classify_Item";
        public static string F_Cr_Cross_AddUp_Combo_Classify_FolderPath = "F_Cr_Cross_AddUp_Combo_Classify_FolderPath";

        //filter
        public static string F_Cr_Cross_AddUp_Check_Refine_Condition = "F_Cr_Cross_AddUp_Check_Refine_Condition";

        public static string F_Cr_Cross_AddUp_Combo_Conditional_Item_1 = "F_Cr_Cross_AddUp_Combo_Conditional_Item_{0}";
        public static string F_Cr_Cross_AddUp_Combo_Conditional_Operator_1 = "F_Cr_Cross_AddUp_Combo_Conditional_Operator_{0}";
        public static string F_Cr_Cross_AddUp_Combo_Conditional_Value_1 = "F_Cr_Cross_AddUp_Combo_Conditional_Value_{0}";
        public static string F_Cr_Cross_AddUp_Option_Conditional_And_1 = "F_Cr_Cross_AddUp_Option_Conditional_And_{0}";
        public static string F_Cr_Cross_AddUp_Option_Conditional_Or_1 = "F_Cr_Cross_AddUp_Option_Conditional_Or_{0}";

        public static string F_Cr_Cross_AddUp_Combo_Conditional_Item_2 = "F_Cr_Cross_AddUp_Combo_Conditional_Item_2";
        public static string F_Cr_Cross_AddUp_Combo_Conditional_Operator_2 = "F_Cr_Cross_AddUp_Combo_Conditional_Operator_2";
        public static string F_Cr_Cross_AddUp_Combo_Conditional_Value_2 = "F_Cr_Cross_AddUp_Combo_Conditional_Value_2";
        public static string F_Cr_Cross_AddUp_Option_Conditional_And_2 = "F_Cr_Cross_AddUp_Option_Conditional_And_2";
        public static string F_Cr_Cross_AddUp_Option_Conditional_Or_2 = "F_Cr_Cross_AddUp_Option_Conditional_Or_2";

        public static string F_Cr_Cross_AddUp_Combo_Conditional_Item_3 = "F_Cr_Cross_AddUp_Combo_Conditional_Item_3";
        public static string F_Cr_Cross_AddUp_Combo_Conditional_Operator_3 = "F_Cr_Cross_AddUp_Combo_Conditional_Operator_3";
        public static string F_Cr_Cross_AddUp_Combo_Conditional_Value_3 = "F_Cr_Cross_AddUp_Combo_Conditional_Value_3";
        public static string F_Cr_Cross_AddUp_Option_Conditional_And_3 = "F_Cr_Cross_AddUp_Option_Conditional_And_3";
        public static string F_Cr_Cross_AddUp_Option_Conditional_Or_3 = "F_Cr_Cross_AddUp_Option_Conditional_Or_3";

        public static string F_Cr_Cross_AddUp_Combo_Conditional_Item_4 = "F_Cr_Cross_AddUp_Combo_Conditional_Item_4";
        public static string F_Cr_Cross_AddUp_Combo_Conditional_Operator_4 = "F_Cr_Cross_AddUp_Combo_Conditional_Operator_4";
        public static string F_Cr_Cross_AddUp_Combo_Conditional_Value_4 = "F_Cr_Cross_AddUp_Combo_Conditional_Value_4";
        public static string F_Cr_Cross_AddUp_Option_Conditional_And_4 = "F_Cr_Cross_AddUp_Option_Conditional_And_4";
        public static string F_Cr_Cross_AddUp_Option_Conditional_Or_4 = "F_Cr_Cross_AddUp_Option_Conditional_Or_4";

        public static string F_Cr_Cross_AddUp_Combo_Conditional_Item_5 = "F_Cr_Cross_AddUp_Combo_Conditional_Item_5";
        public static string F_Cr_Cross_AddUp_Combo_Conditional_Operator_5 = "F_Cr_Cross_AddUp_Combo_Conditional_Operator_5";
        public static string F_Cr_Cross_AddUp_Combo_Conditional_Value_5 = "F_Cr_Cross_AddUp_Combo_Conditional_Value_5";
        public static string F_Cr_Cross_AddUp_Option_Conditional_And_5 = "F_Cr_Cross_AddUp_Option_Conditional_And_5";

        //sample weighting
        public static string F_Cr_Cross_AddUp_Check_Summary_WeightBack = "F_Cr_Cross_AddUp_Check_Summary_WeightBack";
        public static string F_Cr_Cross_AddUp_Combo_Summary_WeightBack = "F_Cr_Cross_AddUp_Combo_Summary_WeightBack";
        public static string F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck = "F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck";
        public static string F_Cr_Cross_AddUp_UnweightbackedBaseCheck = "F_Cr_Cross_AddUp_UnweightbackedBaseCheck";
        public static string F_Cr_Cross_AddUp_Option_Setting_Output_Lateral = "F_Cr_Cross_AddUp_Option_Setting_Output_Lateral";
        //combine banner key
        public static readonly string F_Cr_Cross_AddUp_Check_Summary_Combine_Banners = "F_Cr_Cross_AddUp_Check_Summary_Combine_Banners";

        // Hatch color settings
        public const string Combo_Color_Settings = "Combo_Color_Settings";

        internal static List<List<CrossTableDiv>> ReadCrossSettings(Worksheet sht, ref bool hasDiv, Workbook workBook, Dictionary<string,
            QuestionSettings> variableDictionary, Questions questions, ref bool threeway, bool std = false)
        {
            List<List<CrossTableDiv>> crossTableDivList = new List<List<CrossTableDiv>>();
            //_log.Debug("finding max allocated");
            //Range cRSettingTargetRange = CRValidate.findMaxAllocatedRange(sht, false, sht.Range[Constants.CR.CRVariableStartAddress],
            //sht.Range[Constants.CR.CRVariableStartAddress].Offset[0, 1],
            //sht.Range[Constants.CR.CRVariableStartAddress].Offset[0, 2],
            //sht.Range[Constants.CR.CRVariableStartAddress].Offset[0, 3],
            //sht.Range[Constants.CR.CRVariableStartAddress].Offset[0, 4]);
            //_log.Debug("finding max allocated completed:" + cRSettingTargetRange.Address);
            //_log.Debug("finding row allocated");
            Range LastCellRow = sht.Cells.Find("*", SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlPrevious);
            Range LastCellCol = sht.Cells.Find("*", SearchOrder: XlSearchOrder.xlByColumns, SearchDirection: XlSearchDirection.xlPrevious);
            Range valuesRange = sht.Range["A1", sht.Cells[LastCellRow.Row, LastCellCol.Column]];
            object[,] values = (object[,])valuesRange.Value2;
            _log.Debug("finding row allocated completed:" + LastCellRow.Address);

            if (LastCellRow.Row <= (!std ? Constants.CR.CRRowInputStart : Constants.CR.CRRowInputStart + 1))
            {
                return crossTableDivList;
            }
            Range cRSettingTargetRange = sht.Range[sht.Range[!std ? "C14" : "C15"], sht.Cells[LastCellRow.Row, 3]];

            List<List<CossTableInput>> cossTableInputsFirst = new List<List<CossTableInput>>();
            List<CrossTableDiv> crossTableDivRowListFirst = new List<CrossTableDiv>();
            CrossTableDiv crossTableDivFirst = new CrossTableDiv(0, 0, cossTableInputsFirst);
            crossTableDivRowListFirst.Add(crossTableDivFirst);
            crossTableDivList.Add(crossTableDivRowListFirst);

            Range divStartColumn = sht.Range[Constants.CR.CRRowDivStartAddress];
            Range divEndColumn = ExcelUtil.EndxlRight(divStartColumn);
            Dictionary<int, int> simpleAggrLastPos = new Dictionary<int, int>();

            if (divStartColumn.Column <= divEndColumn.Column)
            {
                hasDiv = true;
            }

            int divRow = 0;
            int settingsCnt = 0;
            Range axis1RngStart = sht.Cells[Constants.CR.CRRow2CRVariable, 1];
            Range lineSetRngStart = sht.Cells[Constants.CR.CRRowLineSettings, 1];
            Range narrowValueStart = sht.Cells[Constants.CR.CRRowNarrowVariable + 1, 1];

            foreach (Range variable in cRSettingTargetRange.Cells)
            {
                int row = variable.Row;
                int col = variable.Column;
                List<CrossTableDiv> crossTableDivRowList = crossTableDivList[divRow];
                string cRSettingDivAxisStr = Convert.ToString(values[row, col + 3]);
                if (!string.IsNullOrEmpty(cRSettingDivAxisStr))
                {
                    if ("div" == cRSettingDivAxisStr) // to do 
                    {
                        divRow++;
                        hasDiv = true;
                        List<List<CossTableInput>> cossTableInputsNext = new List<List<CossTableInput>>();
                        List<CrossTableDiv> crossTableDivRowListNext = new List<CrossTableDiv>();
                        CrossTableDiv crossTableDivNext = new CrossTableDiv(divRow, 0, cossTableInputsNext);
                        crossTableDivRowListNext.Add(crossTableDivNext);
                        crossTableDivList.Add(crossTableDivRowListNext);
                    }
                }
                string variableStr = Convert.ToString(values[row, col]);
                if (string.IsNullOrEmpty(variableStr))
                {
                    continue;
                }

                //Range cRSettingEnd = variable.EntireRow.Find("*", SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlPrevious);
                //if (Constants.CR.CRColInputStart + 1 > cRSettingEnd.Column)
                //{
                //    continue;
                //}
                col = 0;
                for (int y = values.GetLength(1); y >= 1; y -= 1)
                {
                    string s = Convert.ToString(values[row, y]);
                    if (!string.IsNullOrEmpty(s))
                    {
                        col = y;
                        break;
                    }
                }
                if (Constants.CR.CRColInputStart + 1 > col)
                {
                    continue;
                }

                int divColumn = 0;
                bool nextColDiv = true;
                List<CossTableInput> crossTableInputItemList = null;
                bool simpleAggStart = false;
                for (int j = Constants.CR.CRColInputStart + 1; j <= col; j++)
                {
                    string targetCellSetStr = Convert.ToString(values[row, j]);
                    CrossTableDiv crossTableDiv = crossTableDivRowList[divColumn];
                    List<List<CossTableInput>> crossTableInputList = crossTableDiv.CossTableInputs;
                    if (nextColDiv)
                    {
                        crossTableInputItemList = new List<CossTableInput>();
                        nextColDiv = false;
                        crossTableInputList.Add(crossTableInputItemList);
                    }
                    //Range cRSettingDivTarget = targetCell.Worksheet.Cells[Constants.CR.CRRowInputStart, targetCellSet.Column];
                    string cRSettingDivTargetStr = Convert.ToString(values[Constants.CR.CRRowInputStart, j]);
                    if (!string.IsNullOrEmpty(cRSettingDivTargetStr))
                    {
                        if ("div" == cRSettingDivTargetStr) // to do 
                        {
                            divColumn++;
                            nextColDiv = true;
                            hasDiv = true;
                            if (divColumn >= crossTableDivRowList.Count)
                            {
                                List<List<CossTableInput>> cossTableInputsNext = new List<List<CossTableInput>>();
                                CrossTableDiv crossTableDivNext = new CrossTableDiv(crossTableDiv.DivRow, divColumn, cossTableInputsNext);
                                crossTableDivRowList.Add(crossTableDivNext);
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(targetCellSetStr))
                    {
                        continue;
                    }
                    string cRSettingStr = targetCellSetStr;
                    if (string.IsNullOrEmpty(cRSettingStr) || "●" != cRSettingStr && "On" != cRSettingStr) // to do 
                    {
                        continue;
                    }
                    //Range axis1Rng = sht.Cells[Constants.CR.CRRow2CRVariable, j];
                    //Range axis1Rng = axis1RngStart.Offset[0,j];
                    string axis1RngStr = Convert.ToString(values[Constants.CR.CRRow2CRVariable, j]);

                    if (null == axis1RngStr || string.IsNullOrEmpty(axis1RngStr))
                    {
                        //  continue;
                    }
                    string axis1RngTypeStr = Convert.ToString(values[Constants.CR.CRRow2CRVariable + 1, j]);
                    string axis1RngChCntStr = Convert.ToString(values[Constants.CR.CRRow2CRVariable + 2, j]);
                    AxisSetting axis1Rng = new AxisSetting(axis1RngStr, axis1RngTypeStr, axis1RngChCntStr, j);

                    string lineSet = null;
                    //Range lineSetRng = lineSetRngStart.Offset[0,j];
                    string lineSetRngStr = Convert.ToString(values[Constants.CR.CRRowLineSettings, j]);
                    AxisSetting lineSetRngStrAxSt = null;
                    if (null != lineSetRngStr && !string.IsNullOrEmpty(lineSetRngStr))
                    {
                        lineSet = lineSetRngStr;
                        lineSetRngStrAxSt = new AxisSetting(axis1RngStr, null, lineSetRngStr, j);
                    }

                    QC4Common.Model.FilterSettingsCr filter = null;

                    //Range narrowVariable = sht.Cells[Constants.CR.CRRowNarrowVariable, targetCellSet.Column];
                    string narrowVariableStr = Convert.ToString(values[Constants.CR.CRRowNarrowVariable, j]);
                    //Range narrowValue = sht.Cells[Constants.CR.CRRowNarrowVariable + 1, j];
                    //Range narrowValue = narrowValueStart.Offset[0,j];
                    string narrowValueStr = Convert.ToString(values[Constants.CR.CRRowNarrowVariable + 1, j]);
                    AxisSetting narrowValueRng = null;
                    if (null != narrowVariableStr && !string.IsNullOrEmpty(narrowVariableStr)
                        && null != narrowValueStr && !string.IsNullOrEmpty(narrowValueStr))
                    {
                        filter = new QC4Common.Model.FilterSettingsCr();
                        filter.variable = narrowVariableStr;
                        filter.values = narrowValueStr;
                        filter.operatorType = "=";
                        // filter.conditionType = AND;
                        narrowValueRng = new AxisSetting(narrowVariableStr, j);
                    }

                    //Range axis2Rng = sht.Cells[Constants.CR.CRRow3CRVariable, j];
                    string axis2RngStr = Convert.ToString(values[Constants.CR.CRRow3CRVariable, j]);

                    if (null == axis2RngStr || string.IsNullOrEmpty(axis2RngStr))
                    {
                        if (string.IsNullOrEmpty(axis1RngStr))
                        {
                            crossTableInputItemList = new List<CossTableInput>();
                            crossTableInputList.Add(crossTableInputItemList);
                            readSimpleAggrSettings(crossTableInputItemList, values, row, j, LastCellRow.Row, sht, simpleAggrLastPos, questions, variableDictionary);
                            simpleAggStart = true;
                        }
                        else
                        {
                            if (simpleAggStart)
                            {
                                crossTableInputItemList = new List<CossTableInput>();
                                crossTableInputList.Add(crossTableInputItemList);
                            }
                            crossTableInputItemList.Add(new CossTableInput(variableStr, axis1RngStr,
                                lineSet, filter, variable, axis1Rng, lineSetRngStrAxSt, narrowValueRng));
                            simpleAggStart = false;
                        }
                        settingsCnt++;
                    }
                    else
                    {
                        if (simpleAggStart)
                        {
                            crossTableInputItemList = new List<CossTableInput>();
                            crossTableInputList.Add(crossTableInputItemList);
                        }
                        string axis2RngTypeStr = Convert.ToString(values[Constants.CR.CRRow3CRVariable + 1, j]);
                        string axis2RngChCntStr = Convert.ToString(values[Constants.CR.CRRow3CRVariable + 2, j]);
                        threeway = true;
                        AxisSetting axis2Rng = new AxisSetting(axis2RngStr, axis2RngTypeStr, axis2RngChCntStr, j, axis1RngStr);
                        if (lineSetRngStrAxSt != null)
                        {
                            lineSetRngStrAxSt.variableTripple = axis2RngStr;
                        }
                        crossTableInputItemList.Add(new CossTableInput(variableStr
                           , string.IsNullOrEmpty(axis2RngStr) ? null : axis2RngStr, string.IsNullOrEmpty(axis1RngStr) ? null : axis1RngStr,
                           lineSet, filter, variable, axis1Rng, axis2Rng, lineSetRngStrAxSt, narrowValueRng));
                        settingsCnt++;
                        simpleAggStart = false;
                    }
                    //if (crossTableInputItemList.Count > 0)
                    //{
                    //    crossTableInputList.Add(crossTableInputItemList);
                    //}
                }
            }
            if (settingsCnt == 0)
            {
                return new List<List<CrossTableDiv>>();
            }
            return crossTableDivList;
        }

        private static void readSimpleAggrSettings(List<CossTableInput> crossTableInputItemList, object[,] values, int start, int col, int end
            , Worksheet sht, Dictionary<int, int> simpleAggrLastPos, Questions questions, Dictionary<string, QuestionSettings> variableDictionary)
        {
            int last;

            if (simpleAggrLastPos.TryGetValue(col, out last) && last >= start) return;
            string prev = "";
            for (int j = start; j <= end; j++)
            {
                string targetCellSetStr = Convert.ToString(values[j, col]);
                if (string.IsNullOrEmpty(targetCellSetStr))
                {
                    continue;
                }
                string cRSettingStr = targetCellSetStr;
                if (string.IsNullOrEmpty(cRSettingStr) || "●" != cRSettingStr && "On" != cRSettingStr) // to do 
                {
                    continue;
                }
                string variableStr = Convert.ToString(values[j, 3]);
                if (string.IsNullOrEmpty(variableStr))
                {
                    continue;
                }
                if (!variableDictionary.ContainsKey(variableStr))
                {
                    break;
                }
                QuestionSettings qstn = variableDictionary[variableStr];
                Question qs = (Question)questions[qstn.Id];
                string subtotalSettings = "";
                if (!String.IsNullOrEmpty(qstn.AddSubTotal) && qstn.SubTotalCount > 0)
                {
                    subtotalSettings = qstn.AddSubTotal + "_" + qstn.SubTotalCount + "_" + string.Join("_", qstn.SubTotals.Select(r => r.Criteria + "_" + r.Subtotal));
                }
                string join = qstn.AnswerType + "_" + qstn.TableHeading + "_" + String.Join("_", qstn.Choices.ToArray()) + "_" + qstn.CategoryCount + "_"
                    + "_" + subtotalSettings + "_" + qstn.Sort + "_"
                    + (qstn.Count.Length > 0 ? "c" + "_" + qstn.Count + "_" + qstn.CountBase :
                        (qstn.Score.Length > 0 ? "s" + "_" + qstn.Score : ""));
                if (prev != "" && prev != join)
                {
                    break;
                }
                prev = join;
                AxisSetting narrowValueRng = null;
                QC4Common.Model.FilterSettingsCr filter = null;
                string narrowVariableStr = Convert.ToString(values[Constants.CR.CRRowNarrowVariable, col]);
                string narrowValueStr = Convert.ToString(values[Constants.CR.CRRowNarrowVariable + 1, col]);
                if (null != narrowVariableStr && !string.IsNullOrEmpty(narrowVariableStr)
                    && null != narrowValueStr && !string.IsNullOrEmpty(narrowValueStr))
                {
                    filter = new QC4Common.Model.FilterSettingsCr();
                    filter.variable = narrowVariableStr;
                    filter.values = narrowValueStr;
                    filter.operatorType = "=";
                    // filter.conditionType = AND;
                    narrowValueRng = new AxisSetting(narrowVariableStr, col);
                }
                AxisSetting axis1Rng = new AxisSetting("", "", "", col);
                Range variable = sht.Cells[j, 3];
                bool combine = false;
                string GtStr = Convert.ToString(values[12, col]);
                if ("●" == GtStr || "On" == GtStr)
                {
                    combine = true;
                }
                crossTableInputItemList.Add(new CossTableInput(variableStr, "",
                                            null, filter, variable, axis1Rng, null, narrowValueRng, combine: combine));
                simpleAggrLastPos[col] = j;
                if (!combine)
                {
                    break;
                }
                string cRSettingDivAxisStr = Convert.ToString(values[j, 6]);
                if (!string.IsNullOrEmpty(cRSettingDivAxisStr))
                {
                    if ("div" == cRSettingDivAxisStr) // to do 
                    {
                        break;
                    }
                }
            }
        }

        internal static CrossOptions ReadOptions(Workbook workbook, bool isChart, bool std = false)
        {
            Worksheet sht = getASSheet(workbook);
            Dictionary<String, String> Adsettings = getAdvacedSettings(sht);

            MarkingType markingType = 0;
            SignificanceTestLevel significanceTestLevel = 0;

            CrossOptions crossOptions = new CrossOptions();

            string sfx = std ? "_S" : "_P";
            string TableOrientation = string.Empty;
            QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
            //if (!isChart)
            {
                if (std)
                {
                    if ((Adsettings.ContainsKey(F_Cr_Cross_AddUp_Option_Setting_Output_Lateral + sfx)
                   && Adsettings[F_Cr_Cross_AddUp_Option_Setting_Output_Lateral + sfx].ToLower() == "true") || QC4Common.Common.Constants.IsRow == true)
                    {
                        crossOptions.Tableorientation = Macromill.QCWeb.ReportRequest.TableOrientation.Landscape;
                    }
                    else
                    {
                        crossOptions.Tableorientation = Macromill.QCWeb.ReportRequest.TableOrientation.Portrait;
                    }
                }
                else
                {
                    TableOrientation = frmutil.GetCellValueFromSettings(QC4Common.Common.Constants.Cross_TableOrientation.PRO_Cross_TableOrientation_Row, QC4Common.Common.Constants.Cross_TableOrientation.PRO_Cross_TableOrientation_Column, workbook);
                    if (!string.IsNullOrEmpty(TableOrientation))
                        crossOptions.Tableorientation = TableOrientation == "1" ? Macromill.QCWeb.ReportRequest.TableOrientation.Portrait : Macromill.QCWeb.ReportRequest.TableOrientation.Landscape;
                    else
                    {
                        if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
                            crossOptions.Tableorientation = Macromill.QCWeb.ReportRequest.TableOrientation.Landscape;
                        else
                            crossOptions.Tableorientation = Macromill.QCWeb.ReportRequest.TableOrientation.Portrait;
                    }
                }

                // Sets the table presentation mode - multi / individual
                if (TryAndCheck(Adsettings, F_Cr_Cross_AddUp_Option_Output_SheetType_One + sfx, "true"))
                    crossOptions.Tablesononesheet = TablesOnOneSheet.Single;

                if (TryAndCheck(Adsettings, F_Cr_Cross_AddUp_Option_Output_SheetType_Plural + sfx, "true"))
                    crossOptions.Tablesononesheet = TablesOnOneSheet.Multi;

                // Checks if the CombineBanner option is valid.
                if (crossOptions.Tableorientation == Macromill.QCWeb.ReportRequest.TableOrientation.Portrait
                    && crossOptions.Tablesononesheet == TablesOnOneSheet.Multi)
                {
                    // Set CombineBanner value to cross options
                    crossOptions.CombineBanner = IsCombineBannersChecked(Adsettings, sfx);
                }
            }
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_WeightBack + sfx)
                && Adsettings[F_Cr_Cross_AddUp_Check_Summary_WeightBack + sfx].ToLower() == "true")
            {

                if (checkSettingExist(Adsettings, F_Cr_Cross_AddUp_Combo_Summary_WeightBack + sfx))
                {
                    string wbComboItem = Adsettings[F_Cr_Cross_AddUp_Combo_Summary_WeightBack + sfx];
                    if (wbComboItem != null)
                    {
                        bool isWeightValid = true;
                        crossOptions.WBDataList = new OutputUtil().GetWeightList(workbook, wbComboItem, ref isWeightValid);
                        crossOptions.IsWeightListValid = isWeightValid;

                        if (wbComboItem == "WeightBack")
                        {
                            Worksheet sh = GetSettingSheet(workbook);
                            Range start = sh.Cells[2, 10];
                            crossOptions.WBVariable = start.Value;
                        }
                        else
                        {
                            crossOptions.WBVariable = wbComboItem;
                        }

                        if (crossOptions.WBDataList != null && crossOptions.WBDataList.Count > 0)
                        {
                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck + sfx)
                                && Adsettings[F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck + sfx].ToLower() == "true")
                            {
                                crossOptions.WBOn1 = WBSettingCode.WBOn | WBSettingCode.ShowPreWB;
                                if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_UnweightbackedBaseCheck + sfx)
                                    && Adsettings[F_Cr_Cross_AddUp_UnweightbackedBaseCheck + sfx].ToLower() == "true")
                                {
                                    crossOptions.PreWbBase = true;
                                }
                                else
                                {
                                    crossOptions.PreWbBase = false;
                                }
                            }
                            else
                            {
                                crossOptions.WBOn1 = WBSettingCode.WBOn | WBSettingCode.HidePreWB;
                            }
                        }
                    }
                }
            }

            if (checkSettingExist(Adsettings, F_Cr_Cross_AddUp_Combo_Classify_Item + sfx)
                       && checkSettingExist(Adsettings, F_Cr_Cross_AddUp_Combo_Classify_FolderPath + sfx))
            {
                crossOptions.GroupVariable = Adsettings[F_Cr_Cross_AddUp_Combo_Classify_Item + sfx];
                crossOptions.GroupFolderPath = Adsettings[F_Cr_Cross_AddUp_Combo_Classify_FolderPath + sfx];
            }


            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Refine_Condition + sfx)
                && Adsettings[F_Cr_Cross_AddUp_Check_Refine_Condition + sfx].ToLower() == "true")
            {
                crossOptions.IsCheckRefineCondition = true;
                for (int i = 1; i <= 5; i++)
                {
                    if (checkSettingExist(Adsettings, string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Item_1 + sfx, i))
                        && checkSettingExist(Adsettings, string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Operator_1 + sfx, i))
                        && checkSettingExist(Adsettings, string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Value_1 + sfx, i)))
                    {
                        if (!crossOptions.HasFilter)
                        {
                            crossOptions.HasFilter = true;
                            crossOptions.Filters = new List<FilterSettingsCr>();
                        }
                        FilterSettingsCr fs = new FilterSettingsCr();
                        crossOptions.Filters.Add(fs);
                        fs.variable = Adsettings[string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Item_1 + sfx, i)];
                        fs.operatorType = Adsettings[string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Operator_1 + sfx, i)];
                        fs.values = Adsettings[string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Value_1 + sfx, i)];

                        if (Adsettings.ContainsKey(string.Format(F_Cr_Cross_AddUp_Option_Conditional_And_1 + sfx, i - 1))
                            && Adsettings[string.Format(F_Cr_Cross_AddUp_Option_Conditional_And_1 + sfx, i - 1)].ToLower() == "true")
                        {
                            fs.conditionType = AND;
                        }

                        else if (Adsettings.ContainsKey(string.Format(F_Cr_Cross_AddUp_Option_Conditional_Or_1 + sfx, i - 1))
                            && Adsettings[string.Format(F_Cr_Cross_AddUp_Option_Conditional_Or_1 + sfx, i - 1)].ToLower() == "true")
                        {
                            fs.conditionType = OR;
                        }
                    }
                }
            }


            //tabulatewhole
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_All_Base + sfx))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Check_All_Base + sfx].ToLower() == "true")
                {
                    crossOptions.TabulateFullQuantity1 = true;
                }
            }

            //heading
            TabulationDescriptions tabulationDescriptions = new TabulationDescriptions(Qc4Launcher.Util.CommonFunction.SetDescriptionString());
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku + sfx))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku + sfx] != null && Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku + sfx] != QC4Common.Common.Constants.CRLFchar)
                {
                    tabulationDescriptions.TotalAxisDescription = Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku + sfx];
                }
            }
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Change_Non + sfx))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Non + sfx] != null && Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Non + sfx] != QC4Common.Common.Constants.CRLFchar)
                {
                    tabulationDescriptions.NADescription = Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Non + sfx];
                }
            }
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou + sfx))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou + sfx] != null && Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou + sfx] != QC4Common.Common.Constants.CRLFchar)
                {
                    tabulationDescriptions.TotalDescription = Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou + sfx];
                }
            }
            crossOptions.TabulationDescriptions = tabulationDescriptions;


            //show no answer
            ShowCode ShowNACode = (ShowCode)0;
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Non1 + sfx))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Check_Summary_Non1 + sfx].ToLower() == "true")
                {
                    ShowNACode = ShowNACode | ShowCode.Item;
                    crossOptions.ShowNoAnswerItem = true;
                }
            }
            //redmine - 171193
            //if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Non2_P))
            //if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Non2_P))
            //{
            //    if (Adsettings[F_Cr_Cross_AddUp_Check_Summary_Non2_P].ToLower() == "true")
            //    {
            //        ShowNACode = ShowNACode | ShowCode.Axis;
            //        crossOptions.ShowNoAnswerAxis = true;
            //    }
            //}
            crossOptions.ShowNACode1 = ShowNACode;

            //np,n,p
            TableType tabletype = 0;
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Output_Cross_N_Par_One + sfx))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Check_Output_Cross_N_Par_One + sfx].ToLower() == "true")
                {
                    tabletype = tabletype | TableType.NPer;
                }
            }
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Output_Cross_N_One + sfx))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Check_Output_Cross_N_One + sfx].ToLower() == "true")
                {
                    tabletype = tabletype | TableType.N;
                }
            }
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Output_Cross_Par_One + sfx))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Check_Output_Cross_Par_One + sfx].ToLower() == "true")
                {
                    tabletype = tabletype | TableType.Per;
                }
            }
            crossOptions.Tabletype = tabletype;

            //marking
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Mark_Ranking + sfx))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Check_Summary_Mark_Ranking + sfx].ToLower() == "true")
                {
                    markingType = markingType | MarkingType.Ranking;
                }
            }
            int? mark1High = null;
            int? mark2High = null;
            int Level1percent = 0;
            int Level2percent = 0;
            bool useLevel1 = false;

            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1 + sfx) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1 + sfx].ToLower() == "true")
            {
                if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1 + sfx) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1 + sfx].ToLower() == "true")
                {
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 + sfx))
                    {
                        if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 + sfx].ToLower() == "2")
                        {
                            mark1High = -1;
                        }
                        else
                        {
                            mark1High = 1;
                        }
                    }
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1 + sfx))
                    {
                        Level1percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1 + sfx]);
                    }
                }

                if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2 + sfx) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2 + sfx].ToLower() == "true")
                {
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 + sfx))
                    {
                        if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 + sfx].ToLower() == "2")
                        {
                            mark2High = -1;
                        }
                        else
                        {
                            mark2High = 1;
                        }
                    }
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2 + sfx))
                    {
                        Level2percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2 + sfx]);
                    }
                }

                if (mark2High == null && mark1High == null)
                {
                    useLevel1 = true;
                }
                else if (mark2High == null && mark1High != null)
                {
                    useLevel1 = true;
                }
                else if (mark2High != null && mark1High == null)
                {
                    useLevel1 = false;
                }
                else if (Level1percent > Level2percent)
                {
                    useLevel1 = true;
                }
            }


            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1 + sfx) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1 + sfx].ToLower() == "true")
            {
                if (!useLevel1)
                {
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1 + sfx) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1 + sfx].ToLower() == "true")
                    {
                        if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 + sfx))
                        {
                            if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 + sfx].ToLower() == "0")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 + sfx].ToLower() == "1")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1High;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 + sfx].ToLower() == "2")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1Low;
                            }

                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1 + sfx))
                            {
                                crossOptions.Level1percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1 + sfx]);
                            }
                        }
                    }

                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2 + sfx) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2 + sfx].ToLower() == "true")
                    {
                        if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 + sfx))
                        {
                            if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 + sfx].ToLower() == "0")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 + sfx].ToLower() == "1")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2High;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 + sfx].ToLower() == "2")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2Low;
                            }

                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2 + sfx))
                            {
                                crossOptions.Level2percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2 + sfx]);
                            }
                        }
                    }
                }
                else
                {
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1 + sfx) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1 + sfx].ToLower() == "true")
                    {
                        if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 + sfx))
                        {
                            if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 + sfx].ToLower() == "0")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 + sfx].ToLower() == "1")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2High;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1 + sfx].ToLower() == "2")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2Low;
                            }

                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1 + sfx))
                            {
                                crossOptions.Level2percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1 + sfx]);
                            }
                        }
                    }

                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2 + sfx) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2 + sfx].ToLower() == "true")
                    {
                        if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 + sfx))
                        {
                            if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 + sfx].ToLower() == "0")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 + sfx].ToLower() == "1")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1High;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2 + sfx].ToLower() == "2")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1Low;
                            }

                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2 + sfx))
                            {
                                crossOptions.Level1percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2 + sfx]);
                            }
                        }
                    }
                }
            }


            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_SignificantDifferece_Test + sfx))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Check_Summary_SignificantDifferece_Test + sfx].ToLower() == "true")
                {
                    crossOptions.TestFlag1 = true;
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_SignificantDifference_Test + sfx) ||
                         Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_SignificantDifference_Test_Total + sfx) ||
                         Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_SignificantDifference_Test_Choice + sfx)
                        )
                    {
                        if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_SignificantDifference_Test + sfx) &&
                            Adsettings[F_Cr_Cross_AddUp_Combo_SignificantDifference_Test + sfx].ToLower() == "0" ||
                            Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_SignificantDifference_Test_Total + sfx)
                            && Adsettings[F_Cr_Cross_AddUp_Check_SignificantDifference_Test_Total + sfx].ToLower() == "true") // test overall
                        {
                            markingType = markingType | MarkingType.Significance;
                            crossOptions.TestCode = SignificanceTestCode.Off;
                            crossOptions.TestLevels.Add(1.0);
                            crossOptions.TestLevels.Add(5.0);
                            crossOptions.TestLevels.Add(10.0);
                        }
                        else if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_SignificantDifference_Test + sfx) &&
                            Adsettings[F_Cr_Cross_AddUp_Combo_SignificantDifference_Test + sfx].ToLower() == "1" ||
                            Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_SignificantDifference_Test_Choice + sfx)
                            && Adsettings[F_Cr_Cross_AddUp_Check_SignificantDifference_Test_Choice + sfx].ToLower() == "true")// test between sectors
                        {
                            crossOptions.TestCode = SignificanceTestCode.BetweenSectors;
                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Par_99 + sfx))
                            {
                                if (Adsettings[F_Cr_Cross_AddUp_Check_Par_99 + sfx].ToLower() == "true")
                                {
                                    significanceTestLevel = significanceTestLevel | SignificanceTestLevel.One;
                                    crossOptions.TestLevels.Add(1.0);
                                }
                            }
                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Par_95 + sfx))
                            {
                                if (Adsettings[F_Cr_Cross_AddUp_Check_Par_95 + sfx].ToLower() == "true")
                                {
                                    significanceTestLevel = significanceTestLevel | SignificanceTestLevel.Five;
                                    crossOptions.TestLevels.Add(5.0);
                                }
                            }
                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Par_90 + sfx))
                            {
                                if (Adsettings[F_Cr_Cross_AddUp_Check_Par_90 + sfx].ToLower() == "true")
                                {
                                    significanceTestLevel = significanceTestLevel | SignificanceTestLevel.Ten;
                                    crossOptions.TestLevels.Add(10.0);
                                }
                            }

                        }

                    }
                }
            }
            crossOptions.Markingtype = markingType;
            crossOptions.Significancetestlevel = significanceTestLevel;

            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Mark_N_Equal + sfx))
            {
                crossOptions.Minsamplescountonmarking = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Mark_N_Equal + sfx]);
            }

            // Update Hatch Color preference
            Adsettings.TryGetValue(Combo_Color_Settings + sfx, out string presetName);
            Classes.HatchColor.HatchColorCommon.SetHatchColorPreference(crossOptions, presetName);

            return crossOptions;
        }


        /// <summary>
        /// Checks if the "Combine Banners" setting is enabled in the Adsettings dictionary.
        /// The "Combine Banners" functionality is applicable only when the table orientation is set to Portrait
        /// and the presentation mode is set to Multi cross table per sheet.
        /// </summary>
        /// <param name="Adsettings">A dictionary containing advanced settings from advanced setting sheet.</param>
        /// <param name="sfx">A suffix to create the key for "Combine Banners" setting.</param>
        /// <returns>True if "Combine Banners" is enabled, false otherwise.</returns>
        public static bool IsCombineBannersChecked(Dictionary<string, string> Adsettings, string sfx)
        {
            if (TryAndCheck(Adsettings, F_Cr_Cross_AddUp_Check_Summary_Combine_Banners + sfx,"true"))
            {
                //clear ranking array store if the combine banner is checked.
                RangeRankingSheetStore.ClearRangeRankingSheetData();

                CombineBanners.SetDataMap();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to retrieve a value from the dictionary and checks if it matches the expected value (case-insensitive).
        /// </summary>
        /// <param name="settings">The dictionary to search for the key-value pair.</param>
        /// <param name="key">The key to look up in the dictionary.</param>
        /// <param name="expectedValue">The value to compare against the retrieved value (case-insensitive).</param>
        /// <returns>True if the key exists and the retrieved value matches the expected value; otherwise, false.</returns>
        public static bool TryAndCheck(Dictionary<string, string> settings, string key, string expectedValue)
        {
            return settings.TryGetValue(key, out var result) && result.ToLower() == expectedValue;
        }

        public static bool checkSettingExist(Dictionary<string, string> settings, string key)
        {
            return (settings.ContainsKey(key) && settings[key] != null && settings[key] != string.Empty);
        }

        public static Dictionary<String, String> getAdvacedSettings(Worksheet sht)
        {
            Dictionary<String, String> Adsettings = new Dictionary<string, string>();
            Range strart = sht.Cells[1, 1];
            Range end = ExcelUtil.EndxlUp(strart);
            Range settingRange = sht.Range[strart, end.Offset[0, 1]];
            object[,] settings = settingRange.Value2;
            int j = settings.GetLowerBound(1);
            for (int i = settings.GetLowerBound(0); i <= settings.GetUpperBound(0); i++)
            {
                if (settings[i, j] != null && settings[i, j + 1] != null)
                {
                    string key = Convert.ToString(settings[i, j]);
                    string value = Convert.ToString(settings[i, j + 1]);
                    Adsettings[key] = value;
                }

                //Convert.ToString(settings[i, j]);
            }
            return Adsettings;
        }

        public static Worksheet getASSheet(Workbook workbook)
        {
            foreach (Worksheet sht in workbook.Worksheets)
            {
                switch (sht.CodeName)
                {
                    case SheetCodeName.DetailsSetting: //"Sheet93_Cm"
                        return sht;
                    default:
                        break;
                }
            }
            return null;
        }

        public static Worksheet GetSettingSheet(Workbook workbook)
        {
            foreach (Worksheet sht in workbook.Worksheets)
            {
                switch (sht.CodeName)
                {
                    case SheetCodeName.Setting: //"Sheet92_Cm"
                        return sht;
                    default:
                        break;
                }
            }
            return null;
        }

        public class CrossTableDiv
        {
            private List<List<CossTableInput>> cossTableInputs;
            int divRow;
            int divColumn;

            public CrossTableDiv(int divRow, int divColumn, List<List<CossTableInput>> cossTableInputs)
            {
                DivColumn = divColumn;
                DivRow = divRow;
                CossTableInputs = cossTableInputs;
            }

            public int DivColumn { get => divColumn; set => divColumn = value; }
            public int DivRow { get => divRow; set => divRow = value; }
            internal List<List<CossTableInput>> CossTableInputs { get => cossTableInputs; set => cossTableInputs = value; }
        }

        public class CrossOptions
        {
            string xlbooknameprefix = "Cross";
            string reportprefix = "Chart";

            private TableType tabletype = TableType.NPer | TableType.N | TableType.Per;
            private TableOrientation tableorientation = TableOrientation.Landscape;
            private TableType pagesetuptabletype = (TableType)0;
            private int minsamplescountonmarking = 0;
            private MarkingType markingtype = (MarkingType)0;
            private SignificanceTestLevel significancetestlevel = (SignificanceTestLevel)0;
            private XlPaperSize papersize = XlPaperSize.xlPaperA4;
            private XlPageOrientation paperorientation = XlPageOrientation.xlPortrait;
            private TablesOnOneSheet tablesononesheet = TablesOnOneSheet.Multi;

            private int level2highcolorindex = ColorPallet.colorIndex[45];
            private int level1highcolorindex = ColorPallet.colorIndex[40];
            private int level1lowcolorindex = ColorPallet.colorIndex[38];
            private int level2lowcolorindex = ColorPallet.colorIndex[3];
            
            private int level1percent = int.Parse(LocalResource.REPORT_HATCHING_LEVEL1_PERCENT_DEFAULT);
            private int level2percent = int.Parse(LocalResource.REPORT_HATCHING_LEVEL2_PERCENT_DEFAULT);

            ShowCode ShowNACode = (ShowCode)3;
            ShowCode ShowIVCode = (ShowCode)0;
            WBSettingCode WBOn = WBSettingCode.WBOff;
            string FilteringExpression = "";
            string LocalizedFilteringExpression = "";
            TabulationDescriptions tabulationDescriptions = new TabulationDescriptions(Qc4Launcher.Util.CommonFunction.SetDescriptionString());
            bool TabulateFullQuantity = false;
            SignificanceTestCode testCode = SignificanceTestCode.Off;
            List<double> testlevels = new List<double>();
            bool TestFlag = false;
            bool showNoAnswerItem = false;
            bool showNoAnswerAxis = false;
            bool hasFilter = false;
            List<FilterSettingsCr> filters = new List<FilterSettingsCr>();
            string groupVariable;
            string groupFolderPath;
            bool preWbBase = false;
            bool hasDiv = false;

            public string Xlbooknameprefix { get => xlbooknameprefix; set => xlbooknameprefix = value; }
            public TableType Tabletype { get => tabletype; set => tabletype = value; }
            public TableOrientation Tableorientation { get => tableorientation; set => tableorientation = value; }
            public TablesOnOneSheet Tablesononesheet { get => tablesononesheet; set => tablesononesheet = value; }
            public TableType Pagesetuptabletype { get => pagesetuptabletype; set => pagesetuptabletype = value; }
            public XlPaperSize Papersize { get => papersize; set => papersize = value; }
            public XlPageOrientation Paperorientation { get => paperorientation; set => paperorientation = value; }
            public SignificanceTestLevel Significancetestlevel { get => significancetestlevel; set => significancetestlevel = value; }
            public MarkingType Markingtype { get => markingtype; set => markingtype = value; }
            public int Minsamplescountonmarking { get => minsamplescountonmarking; set => minsamplescountonmarking = value; }
            public int Level2highcolorindex { get => level2highcolorindex; set => level2highcolorindex = value; }
            public int Level1highcolorindex { get => level1highcolorindex; set => level1highcolorindex = value; }
            public int Level1lowcolorindex { get => level1lowcolorindex; set => level1lowcolorindex = value; }
            public int Level2lowcolorindex { get => level2lowcolorindex; set => level2lowcolorindex = value; }
            public int Level1percent { get => level1percent; set => level1percent = value; }
            public int Level2percent { get => level2percent; set => level2percent = value; }
            public ShowCode ShowNACode1 { get => ShowNACode; set => ShowNACode = value; }
            public ShowCode ShowIVCode1 { get => ShowIVCode; set => ShowIVCode = value; }
            public WBSettingCode WBOn1 { get => WBOn; set => WBOn = value; }
            public string FilteringExpression1 { get => FilteringExpression; set => FilteringExpression = value; }
            public string Reportprefix { get => reportprefix; set => reportprefix = value; }
            public TabulationDescriptions TabulationDescriptions { get => tabulationDescriptions; set => tabulationDescriptions = value; }
            public bool TabulateFullQuantity1 { get => TabulateFullQuantity; set => TabulateFullQuantity = value; }
            public SignificanceTestCode TestCode { get => testCode; set => testCode = value; }
            public List<double> TestLevels { get => testlevels; set => testlevels = value; }
            public bool TestFlag1 { get => TestFlag; set => TestFlag = value; }
            public bool ShowNoAnswerItem { get => showNoAnswerItem; set => showNoAnswerItem = value; }
            public bool ShowNoAnswerAxis { get => showNoAnswerAxis; set => showNoAnswerAxis = value; }
            public bool HasFilter { get => hasFilter; set => hasFilter = value; }
            public List<FilterSettingsCr> Filters { get => filters; set => filters = value; }
            public string GroupFolderPath { get => groupFolderPath; set => groupFolderPath = value; }
            public string GroupVariable { get => groupVariable; set => groupVariable = value; }
            public List<Data> WBDataList { get; set; } = null;
            public bool PreWbBase { get => preWbBase; set => preWbBase = value; }
            public string LocalizedFilteringExpression1 { get => LocalizedFilteringExpression; set => LocalizedFilteringExpression = value; }
            public bool HasDiv { get => hasDiv; set => hasDiv = value; }
            public bool IsWeightListValid { get; set; } = true;
            public bool IsCheckRefineCondition { get; set; } = false;
            public string WBVariable { get; set; } = null;
            public bool CombineBanner { get; set; } = false;
        }
    }
}