using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelAddIn.Common;
using Microsoft.Office.Interop.Excel;
using Constants = ExcelAddIn.Common.Constants;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Macromill.QCWeb.Common;
using XlPageOrientation = Microsoft.Office.Interop.Excel.XlPageOrientation;
using XlPaperSize = Microsoft.Office.Interop.Excel.XlPaperSize;
using Qc4Launcher.Logic;
using QC4Common.Model;
using QC4Common.Classes.HatchColor;

namespace Qc4Launcher.Summary
{
    class SummaryReader
    {
        public static string AND = "&";
        public static string OR = "|";

        internal static Dictionary<String, List<List<SummaryTableInput>>> ReadSummarySettings(Worksheet sht, ref bool divPresent)
        {
            Dictionary<String, List<List<SummaryTableInput>>> summaryItemDict = new Dictionary<string, List<List<SummaryTableInput>>>();
            Dictionary<String, List<List<SummaryTableInput>>> summaryItemDictOrd = new Dictionary<string, List<List<SummaryTableInput>>>();
            List<List<SummaryTableInput>> summaryTableInputList = new List<List<SummaryTableInput>>();
            Range sTSettingTargetRange = CRValidate.findMaxAllocatedRange(sht, false, sht.Range[Constants.SL.SLVariableStartAddress],
            sht.Range[Constants.SL.SLVariableStartAddress].Offset[0, 1],
            sht.Range[Constants.SL.SLVariableStartAddress].Offset[0, 2],
            sht.Range[Constants.SL.SLVariableStartAddress].Offset[0, 3],
            sht.Range[Constants.SL.SLVariableStartAddress].Offset[0, 4]);
            if (sTSettingTargetRange.Row <= Constants.SL.SLRowInputStart)
            {
                return summaryItemDict;
            }
            sTSettingTargetRange = sht.Range[sht.Range[Constants.SL.SLVariableStartAddress], sTSettingTargetRange];

            int firstCntr = 1;
            int secondCntr = 0;
            int divCount = 0;
            bool hasDiv = false;
            bool hasSep = false;
            string summaryName = "";

            foreach (Range targetCell in sTSettingTargetRange.Cells)
            {
                if (hasSep)
                {
                    firstCntr++;
                    summaryName = "";
                    hasSep = false;
                }

                if ("" == summaryName)
                {
                    Range titleVariable = sht.Cells[targetCell.Row, 3];
                    if (null != titleVariable.Value2 && !string.IsNullOrEmpty(titleVariable.Value2.ToString()))
                    {
                        summaryName = titleVariable.Value2.ToString();
                    }
                }

                Range sepVariable = targetCell.Offset[0, 3];
                if (null != sepVariable.Value2 && !string.IsNullOrEmpty(sepVariable.Value2.ToString()))
                {
                    if ("sep" == sepVariable.Value2.ToString())
                    {
                        hasSep = true;
                    }
                }
                Range variable = targetCell.Offset[0, 0];
                if (null == variable.Value2 || string.IsNullOrEmpty(variable.Value2.ToString()))
                {
                    continue;
                }
                if ("" == summaryName)
                {
                    summaryName = "N" + variable.Value2 + "_MT";
                }
                //summaryName = summaryName.Substring(summaryName.IndexOf("N"));

                Range cRSettingFirst = targetCell.Offset[0, 4];
                Range cRSettingEnd = ExcelUtil.EndxlRight(cRSettingFirst);
                Range cRSettingRange = sht.Range[cRSettingFirst, cRSettingEnd];
                List<SummaryTableInput> summaryTableInputItemList = new List<SummaryTableInput>();
                secondCntr = 0;
                hasDiv = false;
                foreach (Range targetCellSet in cRSettingRange.Cells)
                {
                    Range toggle = sht.Cells[Constants.SL.SLRow2CRVariable - 1, targetCellSet.Column];
                    if (null != toggle.Value2 && !string.IsNullOrEmpty(toggle.Value2.ToString()))
                    {
                        if (LocalResource.CELL_OFF == toggle.Value2.ToString())
                        {
                            continue;
                        }
                    }

                    if (hasDiv)
                    {
                        secondCntr++;
                        if (secondCntr > divCount) { divCount = secondCntr; }
                        if (summaryTableInputItemList.Count >= 0)
                        {
                            String key = firstCntr.ToString() + secondCntr.ToString();
                            if (summaryItemDict.ContainsKey(key))
                            {
                                List<List<SummaryTableInput>> sTemp = summaryItemDict[key];
                                sTemp.Add(summaryTableInputItemList);
                                summaryItemDict[key] = sTemp;
                            }
                            else
                            {
                                summaryTableInputList.Add(summaryTableInputItemList);
                                summaryItemDict[key] = summaryTableInputList;
                            }

                            summaryTableInputList = new List<List<SummaryTableInput>>();
                            summaryTableInputItemList = new List<SummaryTableInput>();
                        }
                        hasDiv = false;
                    }
                    Range divVariable = sht.Cells[Constants.SL.SLRow2CRVariable + 3, targetCellSet.Column];
                    if (null != divVariable.Value2 && !string.IsNullOrEmpty(divVariable.Value2.ToString()))
                    {
                        if ("div" == divVariable.Value2.ToString())
                        {
                            divPresent = true;
                            hasDiv = true;
                        }
                    }

                    if (null == targetCellSet.Value2)
                    {
                        continue;
                    }
                    string cRSettingStr = targetCellSet.Value2.ToString();
                    if (string.IsNullOrEmpty(cRSettingStr)) // to do 
                    {
                        continue;
                    }
                    Range axis1Rng = sht.Cells[Constants.SL.SLRow2CRVariable, targetCellSet.Column];
                    if (null == axis1Rng.Value2 || string.IsNullOrEmpty(axis1Rng.Value2.ToString()))
                    {
                        continue;
                    }

                    summaryTableInputItemList.Add(new SummaryTableInput(
                        variable.Value2.ToString(), axis1Rng.Value2.ToString(), cRSettingStr, summaryName));

                }
                if (summaryTableInputItemList.Count > 0)
                {
                    secondCntr++;
                    if (secondCntr > divCount) { divCount = secondCntr; }
                    String key = firstCntr.ToString() + secondCntr.ToString();
                    if (summaryItemDict.ContainsKey(key))
                    {
                        List<List<SummaryTableInput>> sTemp = summaryItemDict[key];
                        sTemp.Add(summaryTableInputItemList);
                        summaryItemDict[key] = sTemp;
                    }
                    else
                    {
                        summaryTableInputList.Add(summaryTableInputItemList);
                        summaryItemDict[key] = summaryTableInputList;
                    }

                    summaryTableInputList = new List<List<SummaryTableInput>>();
                    summaryTableInputItemList = new List<SummaryTableInput>();
                }
            }

            bool flag = false;
            for (int i = 1; i <= divCount; i++)
            {
                flag = false;
                foreach (KeyValuePair<String, List<List<SummaryTableInput>>> entry in summaryItemDict)
                {
                    string key = entry.Key;
                    if (key.EndsWith(i.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        summaryItemDictOrd[key] = entry.Value;
                        flag = true;
                    }
                }
                if (flag)
                {
                    summaryItemDictOrd["div" + i.ToString()] = null;
                }
            }

            return summaryItemDictOrd;
        }

        internal static SummaryOptions ReadOptions(Workbook workbook)
        {
            Worksheet sht = CrossSettingsReader.getASSheet(workbook);
            Dictionary<String, String> Adsettings = CrossSettingsReader.getAdvacedSettings(sht);

            string F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1_P = "F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1_P";
            string F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P = "F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P";
            string F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P = "F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P";
            string F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1_P = "F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1_P";
            string F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2_P = "F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2_P";
            string F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1_P = "F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1_P";
            string F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2_P = "F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2_P";
            string F_Cr_Cross_AddUp_Check_Summary_SignificantDifferece_Test_P = "F_Cr_Cross_AddUp_Check_Summary_SignificantDifferece_Test_P";
            string F_Cr_Cross_AddUp_Combo_SignificantDifference_Test_P = "F_Cr_Cross_AddUp_Combo_SignificantDifference_Test_P";
            string F_Cr_Cross_AddUp_Check_Par_99_P = "F_Cr_Cross_AddUp_Check_Par_99_P";
            string F_Cr_Cross_AddUp_Check_Par_95_P = "F_Cr_Cross_AddUp_Check_Par_95_P";
            string F_Cr_Cross_AddUp_Check_Par_90_P = "F_Cr_Cross_AddUp_Check_Par_90_P";
            string F_Cr_Cross_AddUp_Text_Summary_Mark_N_Equal_P = "F_Cr_Cross_AddUp_Text_Summary_Mark_N_Equal_P";

            //np,n,p
            string F_Cr_Cross_AddUp_Check_Output_Cross_N_One_P = "F_Cr_Cross_AddUp_Check_Output_Cross_N_P";
            string F_Cr_Cross_AddUp_Check_Output_Cross_Par_One_P = "F_Cr_Cross_AddUp_Check_Output_Cross_Par_P";
            //heading
            string F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_P = "F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_P";
            string F_Cr_Cross_AddUp_Text_Summary_Change_Non_P = "F_Cr_Cross_AddUp_Text_Summary_Change_Non_P";
            string F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P = "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P";

            //div
            string F_Cr_Cross_AddUp_Combo_Classify_Item_P = "F_Cr_Cross_AddUp_Combo_Classify_Item_P";
            string F_Cr_Cross_AddUp_Combo_Classify_FolderPath_P = "F_Cr_Cross_AddUp_Combo_Classify_FolderPath_P";

            //filter
            string F_Cr_Cross_AddUp_Check_Refine_Condition_P = "F_Cr_Cross_AddUp_Check_Refine_Condition_P";

            string F_Cr_Cross_AddUp_Combo_Conditional_Item_1_P = "F_Cr_Cross_AddUp_Combo_Conditional_Item_{0}_P";
            string F_Cr_Cross_AddUp_Combo_Conditional_Operator_1_P = "F_Cr_Cross_AddUp_Combo_Conditional_Operator_{0}_P";
            string F_Cr_Cross_AddUp_Combo_Conditional_Value_1_P = "F_Cr_Cross_AddUp_Combo_Conditional_Value_{0}_P";
            string F_Cr_Cross_AddUp_Option_Conditional_And_1_P = "F_Cr_Cross_AddUp_Option_Conditional_And_{0}_P";
            string F_Cr_Cross_AddUp_Option_Conditional_Or_1_P = "F_Cr_Cross_AddUp_Option_Conditional_Or_{0}_P";

            //sample weighting
            string F_Cr_Cross_AddUp_Check_Summary_WeightBack_P = "F_Cr_Cross_AddUp_Check_Summary_WeightBack_P";
            string F_Cr_Cross_AddUp_Combo_Summary_WeightBack_P = "F_Cr_Cross_AddUp_Combo_Summary_WeightBack_P";

            // Hatch Color
            string Combo_Color_Settings_P = "Combo_Color_Settings_P";

            MarkingType markingType = 0;
            SignificanceTestLevel significanceTestLevel = 0;

            SummaryOptions crossOptions = new SummaryOptions();

            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_WeightBack_P)
                && Adsettings[F_Cr_Cross_AddUp_Check_Summary_WeightBack_P].ToLower() == "true")
            {

                if (checkSettingExist(Adsettings, F_Cr_Cross_AddUp_Combo_Summary_WeightBack_P))
                {
                    string wbComboItem = Adsettings[F_Cr_Cross_AddUp_Combo_Summary_WeightBack_P];
                    if (wbComboItem != null)
                    {
                        bool isWeightValid = true;
                        crossOptions.WBDataList = new OutputUtil().GetWeightList(workbook, wbComboItem, ref isWeightValid);
                        crossOptions.IsWeightListValid = isWeightValid;
                        crossOptions.WBOn1 = WBSettingCode.WBOn | WBSettingCode.ShowPreWB;
                        crossOptions.PreWbBase = true;

                        if (wbComboItem == "WeightBack")
                        {
                            Worksheet sh = CrossSettingsReader.GetSettingSheet(workbook);
                            Range start = sh.Cells[2, 10];
                            crossOptions.WBVariable = start.Value;
                        }
                        else
                        {
                            crossOptions.WBVariable = wbComboItem;
                        }
                    }
                }
            }

            if (checkSettingExist(Adsettings, F_Cr_Cross_AddUp_Combo_Classify_Item_P)
                       && checkSettingExist(Adsettings, F_Cr_Cross_AddUp_Combo_Classify_FolderPath_P))
            {
                crossOptions.GroupVariable = Adsettings[F_Cr_Cross_AddUp_Combo_Classify_Item_P];
                crossOptions.GroupFolderPath = Adsettings[F_Cr_Cross_AddUp_Combo_Classify_FolderPath_P];
            }


            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Refine_Condition_P)
                && Adsettings[F_Cr_Cross_AddUp_Check_Refine_Condition_P].ToLower() == "true")
            {

                for (int i = 1; i <= 5; i++)
                {
                    if (checkSettingExist(Adsettings, string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Item_1_P, i))
                        && checkSettingExist(Adsettings, string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Operator_1_P, i))
                        && checkSettingExist(Adsettings, string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Value_1_P, i)))
                    {
                        if (!crossOptions.HasFilter)
                        {
                            crossOptions.HasFilter = true;
                            crossOptions.Filters = new List<FilterSettingsCr>();
                        }
                        FilterSettingsCr fs = new FilterSettingsCr();
                        crossOptions.Filters.Add(fs);
                        fs.variable = Adsettings[string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Item_1_P, i)];
                        fs.operatorType = Adsettings[string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Operator_1_P, i)];
                        fs.values = Adsettings[string.Format(F_Cr_Cross_AddUp_Combo_Conditional_Value_1_P, i)];

                        if (Adsettings.ContainsKey(string.Format(F_Cr_Cross_AddUp_Option_Conditional_And_1_P, i - 1))
                            && Adsettings[string.Format(F_Cr_Cross_AddUp_Option_Conditional_And_1_P, i - 1)].ToLower() == "true")
                        {
                            fs.conditionType = AND;
                        }

                        else if (Adsettings.ContainsKey(string.Format(F_Cr_Cross_AddUp_Option_Conditional_Or_1_P, i - 1))
                            && Adsettings[string.Format(F_Cr_Cross_AddUp_Option_Conditional_Or_1_P, i - 1)].ToLower() == "true")
                        {
                            fs.conditionType = OR;
                        }
                    }
                }
            }

            //heading
            TabulationDescriptions tabulationDescriptions = new TabulationDescriptions(Qc4Launcher.Util.CommonFunction.SetDescriptionString());
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_P))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_P] != null && Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_P]!= QC4Common.Common.Constants.CRLFchar)
                {
                    tabulationDescriptions.TotalAxisDescription = Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_P];
                }
            }
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Change_Non_P))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Non_P] != null && Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Non_P] != QC4Common.Common.Constants.CRLFchar)
                {
                    tabulationDescriptions.NADescription = Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Non_P];
                }
            }
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P] != null && Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P] != QC4Common.Common.Constants.CRLFchar)
                {
                    tabulationDescriptions.TotalDescription = Adsettings[F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P];
                }
            }
            crossOptions.TabulationDescriptions = tabulationDescriptions;


            //np,n,p
            TableType tabletype = 0;
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Output_Cross_N_One_P))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Check_Output_Cross_N_One_P].ToLower() == "true")
                {
                    tabletype = tabletype | TableType.N;
                }
            }
            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Output_Cross_Par_One_P))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Check_Output_Cross_Par_One_P].ToLower() == "true")
                {
                    tabletype = tabletype | TableType.Per;
                }
            }
            if (0 != tabletype)
            {
                crossOptions.Tabletype = tabletype;
            }

            int? mark1High = null;
            int? mark2High = null;
            int Level1percent = 0;
            int Level2percent = 0;
            bool useLevel1 = false;

            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1_P) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1_P].ToLower() == "true")
            {
                if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1_P) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1_P].ToLower() == "true")
                {
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P))
                    {
                        if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P].ToLower() == "2")
                        {
                            mark1High = -1;
                        }
                        else
                        {
                            mark1High = 1;
                        }
                    }
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1_P))
                    {
                        Level1percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1_P]);
                    }
                }

                if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2_P) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2_P].ToLower() == "true")
                {
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P))
                    {
                        if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P].ToLower() == "2")
                        {
                            mark2High = -1;
                        }
                        else
                        {
                            mark2High = 1;
                        }
                    }
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2_P))
                    {
                        Level2percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2_P]);
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


            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1_P) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1_P].ToLower() == "true")
            {
                if (!useLevel1)
                {
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1_P) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1_P].ToLower() == "true")
                    {
                        if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P))
                        {
                            if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P].ToLower() == "0")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P].ToLower() == "1")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1High;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P].ToLower() == "2")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1Low;
                            }

                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1_P))
                            {
                                crossOptions.Level1percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1_P]);
                            }
                        }
                    }

                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2_P) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2_P].ToLower() == "true")
                    {
                        if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P))
                        {
                            if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P].ToLower() == "0")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P].ToLower() == "1")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2High;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P].ToLower() == "2")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2Low;
                            }

                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2_P))
                            {
                                crossOptions.Level2percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2_P]);
                            }
                        }
                    }
                }
                else
                {
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1_P) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1_P].ToLower() == "true")
                    {
                        if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P))
                        {
                            if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P].ToLower() == "0")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P].ToLower() == "1")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2High;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference1_P].ToLower() == "2")
                            {
                                markingType = markingType | MarkingType.ColoringLevel2Low;
                            }

                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1_P))
                            {
                                crossOptions.Level2percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference1_P]);
                            }
                        }
                    }

                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2_P) && Adsettings[F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2_P].ToLower() == "true")
                    {
                        if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P))
                        {
                            if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P].ToLower() == "0")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P].ToLower() == "1")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1High;
                            }
                            else if (Adsettings[F_Cr_Cross_AddUp_Combo_Summary_Rate_Difference2_P].ToLower() == "2")
                            {
                                markingType = markingType | MarkingType.ColoringLevel1Low;
                            }

                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2_P))
                            {
                                crossOptions.Level1percent = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Rate_Difference2_P]);
                            }
                        }
                    }
                }
            }


            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Summary_SignificantDifferece_Test_P))
            {
                if (Adsettings[F_Cr_Cross_AddUp_Check_Summary_SignificantDifferece_Test_P].ToLower() == "true")
                {
                    crossOptions.TestFlag1 = true;
                    if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Combo_SignificantDifference_Test_P))
                    {
                        if (Adsettings[F_Cr_Cross_AddUp_Combo_SignificantDifference_Test_P].ToLower() == "0")
                        {
                            markingType = markingType | MarkingType.Significance;
                            crossOptions.TestCode = SignificanceTestCode.Off;
                            crossOptions.TestLevels.Add(1.0);
                            crossOptions.TestLevels.Add(5.0);
                            crossOptions.TestLevels.Add(10.0);
                        }
                        else if (Adsettings[F_Cr_Cross_AddUp_Combo_SignificantDifference_Test_P].ToLower() == "1")
                        {
                            crossOptions.TestCode = SignificanceTestCode.BetweenSectors;
                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Par_99_P))
                            {
                                if (Adsettings[F_Cr_Cross_AddUp_Check_Par_99_P].ToLower() == "true")
                                {
                                    significanceTestLevel = significanceTestLevel | SignificanceTestLevel.One;
                                    crossOptions.TestLevels.Add(1.0);
                                }
                            }
                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Par_95_P))
                            {
                                if (Adsettings[F_Cr_Cross_AddUp_Check_Par_95_P].ToLower() == "true")
                                {
                                    significanceTestLevel = significanceTestLevel | SignificanceTestLevel.Five;
                                    crossOptions.TestLevels.Add(5.0);
                                }
                            }
                            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Check_Par_90_P))
                            {
                                if (Adsettings[F_Cr_Cross_AddUp_Check_Par_90_P].ToLower() == "true")
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

            if (Adsettings.ContainsKey(F_Cr_Cross_AddUp_Text_Summary_Mark_N_Equal_P))
            {
                crossOptions.Minsamplescountonmarking = Convert.ToInt32(Adsettings[F_Cr_Cross_AddUp_Text_Summary_Mark_N_Equal_P]);
            }

            // Update Hatch Color preference
            Adsettings.TryGetValue(Combo_Color_Settings_P , out string presetName);
            Classes.HatchColor.HatchColorCommon.SetHatchColorPreference(crossOptions, presetName);

            return crossOptions;
        }

        private static bool checkSettingExist(Dictionary<string, string> settings, string key)
        {
            return (settings.ContainsKey(key) && settings[key] != null && settings[key] != string.Empty);
        }

        public class SummaryTableInput
        {

            public SummaryTableInput(string target, string axis1)
            {
                this.target = target;
                this.axis1 = axis1;
            }
            public SummaryTableInput(string target, string axis1, string position, string summaryName)
            {
                this.target = target;
                this.axis1 = axis1;
                this.position = position;
                this.summaryName = summaryName;
            }
            public string target { get; set; }
            public string axis1 { get; set; }
            public string position { get; set; }
            public string summaryName { get; set; }

        }

        public class SummaryOptions
        {
            string xlbooknameprefix = "Summary";
            string reportprefix = "Report";

            private TableType tabletype = TableType.N | TableType.Per;
            private TableOrientation tableorientation = TableOrientation.Landscape;
            private TableType pagesetuptabletype = (TableType)0;
            private int minsamplescountonmarking = 0;
            private MarkingType markingtype = (MarkingType)0;
            private SignificanceTestLevel significancetestlevel = (SignificanceTestLevel)0;
            private XlPaperSize papersize = XlPaperSize.xlPaperA4;
            private XlPageOrientation paperorientation = XlPageOrientation.xlPortrait;
            private TablesOnOneSheet tablesononesheet = TablesOnOneSheet.Single;
            private int level2highcolorindex = ColorPallet.colorIndex[45]; // to do take from config
            private int level1highcolorindex = ColorPallet.colorIndex[40];
            private int level1lowcolorindex = ColorPallet.colorIndex[38];
            private int level2lowcolorindex = ColorPallet.colorIndex[3];
            private int level1percent = int.Parse(LocalResource.REPORT_HATCHING_LEVEL1_PERCENT_DEFAULT);
            private int level2percent = int.Parse(LocalResource.REPORT_HATCHING_LEVEL2_PERCENT_DEFAULT);
            ShowCode ShowNACode = (ShowCode)0;
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
            bool preWbBase = true;
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
            public string WBVariable { get; set; } = null;
        }
    }
}

