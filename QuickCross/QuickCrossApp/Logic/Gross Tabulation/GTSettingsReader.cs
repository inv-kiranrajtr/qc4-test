using System;
using System.Collections.Generic;
using ExcelAddIn;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using Qc4Launcher.Util;
using static Qc4Launcher.Util.Constants;
using XlPageOrientation = Microsoft.Office.Interop.Excel.XlPageOrientation;
using XlPaperSize = Microsoft.Office.Interop.Excel.XlPaperSize;
using System.Data;
using DataTable = System.Data.DataTable;
using log4net;
using System.Reflection;
using QuestionType = Macromill.QCWeb.Tabulation.QuestionType;
using ReportMessageIndex = Macromill.QCWeb.Common.Constants.ReportMessageIndex;
using Macromill.QCWeb.Question;
using static Macromill.QCWeb.Question.Questions;
using System.Linq;
using QC4Common.Model;

namespace Qc4Launcher.Logic.Gross_Tabulation
{
    internal class GTSettingsReader
    {
        private static readonly string AND = "&";
        private static readonly string OR = "|";

        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal GTSettings ReadGrossSettings(Workbook workBook, Worksheet gtSheet, Questions questions, SigSettings sigSettings, ref bool stopExecution, ref string errorMsg,string version = null)
        {
            try
            {
                GTSettings gTSettings = new GTSettings();

                if(version == "S")
                    gTSettings.GtOptions = ReadOptionsStandard(workBook, ref stopExecution, ref errorMsg);
                else
                    gTSettings.GtOptions = ReadOptions(workBook, ref stopExecution, ref errorMsg);

                bool isSignificanceExist = false;
                MarkingType markingType = 0;
                SignificanceTestLevel sigTestLevel = 0;
                List<double> testLevels = new List<double>();
                UpdateSignificantMainSettings(ref gtSheet, ref markingType, ref sigTestLevel, ref testLevels, ref isSignificanceExist, sigSettings);
                gTSettings.Markingtype = markingType;
                gTSettings.Significancetestlevel = sigTestLevel;
                gTSettings.TestLevels = testLevels;

                List<GrossTableInput> grossTableInputs = new List<GrossTableInput>();

                int sheetStart = GT.GtRowDataStart;
                sheetStart = sheetStart - 2; // 1st row will be taken as header in dt;
                bool atleastOneSigTestAvailable = false;
                FileUtil fileUtil = new FileUtil();
                DataTable dtData = fileUtil.ReadDataFromSheet(gtSheet, sheetStart);
                for (int i = 0; i <= dtData.Rows.Count - 1; i++)
                {
                    DataRow drow = dtData.Rows[i];
                    if (IsRowActive(drow[GT.GtColExec - 1]))
                    {
                        GrossTableInput grossTableInput = new GrossTableInput();
                        grossTableInput.VariableName = (string)drow[GT.GtColItem - 1];
                        grossTableInput.ChartType = null;

                        bool IsMatrixType = false;
                        string gtTableType = null;
                        UpdateMatrixSettings((string)drow[GT.GtColChartType - 1], ref IsMatrixType, ref gtTableType);
                        grossTableInput.IsMatrixPatent = IsMatrixType;
                        grossTableInput.GTTableType = gtTableType;

                        if (string.IsNullOrEmpty(grossTableInput.VariableName) || string.IsNullOrEmpty(grossTableInput.GTTableType))
                        {
                            gTSettings.GtOptions.IsDataValid = false;
                            MessageDialog.ShowMessageOnWorkBook(String.Format(LocalResource.GT_INVALID_ITEM_SETTINGS_IN_ROW, (GT.GtRowDataStart + i)), Enums.MessageType.ErrorOk, workBook);
                            return gTSettings;
                        }

                        //Significance Setting

                        SignificanceTestCode significanceTestCode = GetSignificanceTestCode(Convert.ToString(drow[GT.GtColTestCode - 1]).Trim(), IsMatrixType);
                        if (!isSignificanceExist && significanceTestCode != SignificanceTestCode.Off)
                        {
                            gTSettings.GtOptions.IsDataValid = false;
                            MessageDialog.ShowMessageOnWorkBook(LocalResource.GT_SEPECIFY_SIGNTEST_LEVEL, Enums.MessageType.Warning, workBook);
                            return gTSettings;
                        }

                        if (isSignificanceExist)
                        {
                            grossTableInput.SigTestCode = significanceTestCode;
                            if (grossTableInput.SigTestCode != SignificanceTestCode.Off)
                                atleastOneSigTestAvailable = true;
                        }
                        else
                            grossTableInput.SigTestCode = SignificanceTestCode.Off;

                        if (gTSettings.GtOptions.OutputGraph)
                        {
                            grossTableInput.GraphType = GetGraphType(Convert.ToString(drow[GT.GtColGraphType - 1]).Trim());
                            if (grossTableInput.GraphType != null)
                                grossTableInput.HasGraph = true;
                        }

                        //Matrix columns
                        List<string> matrixColumns = new List<string>();

                        if (grossTableInput.IsMatrixPatent)
                        {
                            for (int j = GT.GtColItem - 1; j <= dtData.Columns.Count - 1; j++) //GtColItem means excluding the 1st Item and taking rest
                            {
                                if (j <= (GT.GtColumnLimit - 1))
                                {
                                    string matrixColumn = Convert.ToString(drow[j]).Trim();
                                    if (matrixColumn.Length > 0)
                                        matrixColumns.Add(matrixColumn);
                                }
                            }
                        }
                        grossTableInput.MatrixColumns = matrixColumns;

                        string tableHeading = Convert.ToString(drow[GT.GtColTableHeading - 1]).Trim();

                        if (tableHeading != null && tableHeading.Trim().Length > 0)
                        {
                            grossTableInput.TableHeading = tableHeading;
                        }

                        // Reorganize matrix columns
                        if (grossTableInput.MatrixColumns != null)
                        {
                            if (grossTableInput.MatrixColumns.Count <= 1)
                                grossTableInputs.Add(grossTableInput);
                            else
                            {
                                Dictionary<string, List<string>> parentChildValues = new Dictionary<string, List<string>>(); //new new Dictionary<parent, children>
                                foreach (string variableName in grossTableInput.MatrixColumns)
                                {
                                    //Question qstn = getQuestion(questions, variableName);
                                    QuestionSettings qstnDet = Definiotion.VariableDictionary[variableName];
                                    string parentName = GetParentOfGTQuestion(qstnDet);
                                    if (!parentChildValues.ContainsKey(parentName))
                                        parentChildValues.Add(parentName, new List<string>() { variableName });
                                    else
                                    {
                                        if (!parentChildValues[parentName].Contains(variableName))
                                            parentChildValues[parentName].Add(variableName);
                                    }
                                }

                                foreach (string parentName in parentChildValues.Keys)
                                {
                                    GrossTableInput gtInputNew = new GrossTableInput();
                                    gtInputNew.ChartType = grossTableInput.ChartType;
                                    gtInputNew.GraphType = grossTableInput.GraphType;
                                    gtInputNew.GTTableType = grossTableInput.GTTableType;
                                    gtInputNew.HasGraph = grossTableInput.HasGraph;
                                    gtInputNew.IsMatrixPatent = grossTableInput.IsMatrixPatent;
                                    gtInputNew.QuestionType2 = grossTableInput.QuestionType2;
                                    gtInputNew.SigTestCode = grossTableInput.SigTestCode;
                                    gtInputNew.TableHeading = grossTableInput.TableHeading;
                                    gtInputNew.VariableName = parentChildValues[parentName][0];
                                    gtInputNew.MatrixColumns = parentChildValues[parentName];
                                    grossTableInputs.Add(gtInputNew);
                                }
                            }
                        }
                        else
                            grossTableInputs.Add(grossTableInput);
                    }
                }

                //gTSettings.GTInputs = ReorganizeInputs(grossTableInputs, questions);
                gTSettings.GTInputs = grossTableInputs;


                if (!atleastOneSigTestAvailable)
                { // If there is no test code is selected, Removing significant setting from master
                    gTSettings.Markingtype = 0;
                    gTSettings.Significancetestlevel = 0;
                    gTSettings.TestLevels = new List<double>();
                }

                return gTSettings;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "/n" + ex.StackTrace);
                throw new Exception("Invalid GT settings");
            }

        }


         public static Question getQuestion(Questions questions, string variableName)
        {
            Question qstn;
            if (variableName == QuestionVariableValue.QuestionVariableItem)
                qstn = (Question)questions[0]; // need to change after question settings updations
            else
            {
                QuestionSettings qstnDet = Definiotion.VariableDictionary[variableName];
                qstn = (Question)questions[qstnDet.Id];
            }
            return qstn;
        }

       private string GetParentOfGTQuestion(QuestionSettings qstn)
       {
           //qstn.Sectors.Count;
           //string parentName;
           //if (qstn.ParentQuestion != null)
           //    parentName = qstn.ParentQuestion.ColumnName;
           //else
           //    parentName = qstn.ColumnName;
           string join = qstn.CategoryCount + "_" + qstn.SubTotalCount + "_" + (qstn.Count.Length > 0 ? "c" :(qstn.Score.Length > 0 ? "s" : ""));
           return Convert.ToString(join);
       }

        private bool ItemExistInList(List<string> itemList, string itemName)
        {
            bool isExist = false;
            var selectedList = itemList.AsEnumerable().Where(field => field == itemName).ToList();
            if (selectedList != null && selectedList.Count > 0)
                isExist = true;
            return isExist;

        }

        private void UpdateSignificantMainSettings(ref Worksheet gtSheet, ref MarkingType markingType, ref SignificanceTestLevel significanceTestLevel, ref List<double> testLevels, ref bool isSignificanceExist, SigSettings sigSettings)
        {
            //bool IsSigPer1Checked = ExcelUtil.GetCheckboxValue(gtSheet, GT.GTCheckBoxSig1Per);
            //bool IsSigPer5Checked = ExcelUtil.GetCheckboxValue(gtSheet, GT.GTCheckBoxSig5Per);
            //bool IsSigPer10Checked = ExcelUtil.GetCheckboxValue(gtSheet, GT.GTCheckBoxSig10Per);

            bool IsSigPer1Checked = sigSettings.IsSig1Checked;
            bool IsSigPer5Checked = sigSettings.IsSig5Checked;
            bool IsSigPer10Checked = sigSettings.IsSig10Checked;

            if (IsSigPer1Checked || IsSigPer5Checked || IsSigPer10Checked)
            {
                if (IsSigPer1Checked && IsSigPer5Checked && IsSigPer10Checked)
                    throw new Exception("Maximum 2 selections are allowed for significant test");

                isSignificanceExist = true;
                markingType = MarkingType.Significance;
                if (IsSigPer1Checked && IsSigPer5Checked)
                {
                    significanceTestLevel = SignificanceTestLevel.One | SignificanceTestLevel.Five;
                    testLevels.Add(1); testLevels.Add(5);
                }
                else if (IsSigPer1Checked && IsSigPer10Checked)
                {
                    significanceTestLevel = SignificanceTestLevel.One | SignificanceTestLevel.Ten;
                    testLevels.Add(1); testLevels.Add(10);
                }
                else if (IsSigPer5Checked && IsSigPer10Checked)
                {
                    significanceTestLevel = SignificanceTestLevel.Five | SignificanceTestLevel.Ten;
                    testLevels.Add(5); testLevels.Add(10);
                }
                else if (IsSigPer1Checked)
                {
                    significanceTestLevel = SignificanceTestLevel.One;
                    testLevels.Add(1);
                }
                else if (IsSigPer5Checked)
                {
                    significanceTestLevel = SignificanceTestLevel.Five;
                    testLevels.Add(5);
                }
                else if (IsSigPer10Checked)
                {
                    significanceTestLevel = SignificanceTestLevel.Ten;
                    testLevels.Add(10);
                }
            }

        }

        private SignificanceTestCode GetSignificanceTestCode(string selectedCode, bool IsMatrixType)
        {
            SignificanceTestCode sigTestCode = SignificanceTestCode.Off;
            if (selectedCode.Length > 0)
            {
                if (selectedCode == "1")
                {
                    return SignificanceTestCode.BetweenSectors;
                }
                else if (IsMatrixType && selectedCode == "2")
                {
                    return SignificanceTestCode.BetweenChildQuestions;
                }
            }
            return sigTestCode;
        }

        private void UpdateMatrixSettings(string gtTableType, ref bool IsMatrixType, ref string tableType)
        {
            switch (gtTableType)
            {
                case GT.GTSA:
                    IsMatrixType = false;
                    tableType = GT.GTSA;
                    break;

                case GT.GTMA:
                    IsMatrixType = false;
                    tableType = GT.GTMA;
                    break;

                case GT.GTN:
                    IsMatrixType = false;
                    tableType = GT.GTN;
                    break;

                case GT.GTRAT:
                    IsMatrixType = true;
                    tableType = GT.GTRAT;
                    break;

                case GT.GTRNK:
                    IsMatrixType = true;
                    tableType = GT.GTRNK;
                    break;

                case GT.GTMTS:
                    IsMatrixType = true;
                    tableType = GT.GTMTS;
                    break;

                case GT.GTMTM:
                    IsMatrixType = true;
                    tableType = GT.GTMTM;
                    break;

                case GT.GTMTN:
                    IsMatrixType = true;
                    tableType = GT.GTMTN;
                    break;
            }
        }

        private bool IsRowActive(object columnValue)
        {
            bool isRowActive = false;
            try
            {
                if ((string)columnValue == "○" || ((string)columnValue).ToLower() == "on")
                {
                    isRowActive = true;
                }
            }
            catch
            {

            }
            return isRowActive;
        }


        private GTOptions ReadOptions(Workbook workbook, ref bool stopExecution, ref string errorMsg)
        {
            Worksheet sht = CrossSettingsReader.getASSheet(workbook);
            Dictionary<string, string> Adsettings = CrossSettingsReader.getAdvacedSettings(sht);
            GTOptions gTOptions = new GTOptions();

            string True = "TRUE";

            //Filter
            string F_Gt_GT_AddUp_Check_Refine_Condition_P = "F_Gt_GT_AddUp_Check_Refine_Condition_P";
            string F_Gt_GT_AddUp_Combo_Conditional_Item_P = "F_Gt_GT_AddUp_Combo_Conditional_Item_{0}_P";
            string F_Gt_GT_AddUp_Combo_Conditional_Operator_P = "F_Gt_GT_AddUp_Combo_Conditional_Operator_{0}_P";
            string F_Gt_GT_AddUp_Combo_Conditional_Value_P = "F_Gt_GT_AddUp_Combo_Conditional_Value_{0}_P";
            string F_Gt_GT_AddUp_Option_Conditional_And_P = "F_Gt_GT_AddUp_Option_Conditional_And_{0}_P";
            string F_Gt_GT_AddUp_Option_Conditional_Or_P = "F_Gt_GT_AddUp_Option_Conditional_Or_{0}_P";

            //Sample Weighting
            string F_Gt_GT_AddUp_Check_Summary_WeightBack_P = "F_Cr_Cross_AddUp_Check_Summary_WeightBack_P";
            string F_Gt_GT_AddUp_Combo_Summary_WeightBack_P = "F_Cr_Cross_AddUp_Combo_Summary_WeightBack_P";
            string F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P = "F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P";

            //div
            string F_Gt_GT_AddUp_Combo_Classify_Item_P = "F_Gt_GT_AddUp_Combo_Classify_Item_P";
            string F_Gt_GT_AddUp_Combo_Classify_FolderPath_P = "F_Gt_GT_AddUp_Combo_Classify_FolderPath_P";

            //heading
            string F_Gt_GT_AddUp_Text_Summary_Change_Hyoutou_P = "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P";
            string F_Gt_GT_AddUp_Text_Summary_Change_Non_P = "F_Cr_Cross_AddUp_Text_Summary_Change_Non_P";

            //Respondance
            string F_Gt_GT_AddUp_Check_All_Base_P = "F_Gt_GT_AddUp_Check_All_Base_P";

            //Graph
            string F_Gt_GT_AddUp_Check_Output_Graphs_P = "F_Gt_GT_AddUp_Check_Output_Graphs_P";
            string F_Gt_GT_AddUp_Check_Output_Hide_ChoiceFor_PieChart_P = "F_Gt_GT_AddUp_Check_Output_Hide_ChoiceFor_PieChart_P";
            string F_Gt_GT_AddUp_Text_Outputs_Ratios_EqualToLessThan_P = "F_Gt_GT_AddUp_Text_Outputs_Ratios_EqualToLessThan_P";

            string wbComboItem = null;
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Combo_Summary_WeightBack_P))
            {
                if (Adsettings[F_Gt_GT_AddUp_Combo_Summary_WeightBack_P] != null && Adsettings[F_Gt_GT_AddUp_Combo_Summary_WeightBack_P] != string.Empty)
                    wbComboItem = Adsettings[F_Gt_GT_AddUp_Combo_Summary_WeightBack_P];
            }

            bool showUnweightedCase = false;
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P))
            {
                if (Adsettings[F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P] != null && Adsettings[F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P] != string.Empty)
                    showUnweightedCase = Adsettings[F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P].ToUpper() == True ? true : false;
            }

            // Sample Weighting
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Summary_WeightBack_P))
            {
                if (Adsettings[F_Gt_GT_AddUp_Check_Summary_WeightBack_P] != null && Adsettings[F_Gt_GT_AddUp_Check_Summary_WeightBack_P] != string.Empty)
                {
                    string enableWeightBack = Adsettings[F_Gt_GT_AddUp_Check_Summary_WeightBack_P];
                    if (enableWeightBack.ToUpper() == True)
                    {
                        if (wbComboItem != null)
                        {
                            bool isWeightValid = true;
                            gTOptions.WBDataList = new OutputUtil().GetWeightList(workbook, wbComboItem, ref isWeightValid);
                            gTOptions.IsDataValid = isWeightValid;

                            if (wbComboItem == "WeightBack")
                            {
                                Worksheet sh = CrossSettingsReader.GetSettingSheet(workbook);
                                Range start = sh.Cells[2, 10];
                                gTOptions.WBVariable = start.Value;
                            }
                            else
                            {
                                gTOptions.WBVariable = wbComboItem;
                            }

                            if (gTOptions.WBDataList != null && gTOptions.WBDataList.Count > 0)
                            {
                                if (showUnweightedCase)
                                {
                                    gTOptions.WBOn1 = WBSettingCode.WBOn | WBSettingCode.ShowPreWB;
                                    gTOptions.PreWbBase = true;
                                }
                                else
                                    gTOptions.WBOn1 = WBSettingCode.WBOn | WBSettingCode.HidePreWB;
                            }
                        }
                    }
                }
            }

            //Filter
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Refine_Condition_P) && Adsettings[F_Gt_GT_AddUp_Check_Refine_Condition_P].ToLower() == "true")
            {

                for (int i = 1; i <= 5; i++)
                {
                    if (CrossSettingsReader.checkSettingExist(Adsettings, string.Format(F_Gt_GT_AddUp_Combo_Conditional_Item_P, i))
                        && CrossSettingsReader.checkSettingExist(Adsettings, string.Format(F_Gt_GT_AddUp_Combo_Conditional_Operator_P, i))
                        && CrossSettingsReader.checkSettingExist(Adsettings, string.Format(F_Gt_GT_AddUp_Combo_Conditional_Value_P, i)))
                    {
                        if (!gTOptions.HasFilter)
                        {
                            gTOptions.HasFilter = true;
                            gTOptions.Filters = new List<FilterSettingsCr>();
                        }
                        FilterSettingsCr fs = new FilterSettingsCr();
                        gTOptions.Filters.Add(fs);
                        fs.variable = Adsettings[string.Format(F_Gt_GT_AddUp_Combo_Conditional_Item_P, i)];

                        if (!Definiotion.VariableDictionary.ContainsKey(fs.variable)) //integrity check
                        {
                            stopExecution = true;
                            errorMsg = LocalResource.GT_INVALID_FILTER_SETTINGS;
                            gTOptions.HasFilter = false;
                            gTOptions.Filters = new List<FilterSettingsCr>();
                            break;
                        }

                        fs.operatorType = Adsettings[string.Format(F_Gt_GT_AddUp_Combo_Conditional_Operator_P, i)];
                        fs.values = Adsettings[string.Format(F_Gt_GT_AddUp_Combo_Conditional_Value_P, i)];

                        if (Adsettings.ContainsKey(string.Format(F_Gt_GT_AddUp_Option_Conditional_And_P, i - 1))
                            && Adsettings[string.Format(F_Gt_GT_AddUp_Option_Conditional_And_P, i - 1)].ToLower() == "true")
                            fs.conditionType = AND;

                        else if (Adsettings.ContainsKey(string.Format(F_Gt_GT_AddUp_Option_Conditional_Or_P, i - 1))
                            && Adsettings[string.Format(F_Gt_GT_AddUp_Option_Conditional_Or_P, i - 1)].ToLower() == "true")
                            fs.conditionType = OR;
                    }
                }
            }

            //division
            if (CrossSettingsReader.checkSettingExist(Adsettings, F_Gt_GT_AddUp_Combo_Classify_Item_P) && CrossSettingsReader.checkSettingExist(Adsettings, F_Gt_GT_AddUp_Combo_Classify_FolderPath_P))
            {
                gTOptions.GroupVariable = Adsettings[F_Gt_GT_AddUp_Combo_Classify_Item_P];
                gTOptions.GroupFolderPath = Adsettings[F_Gt_GT_AddUp_Combo_Classify_FolderPath_P];
            }

            //heading
            TabulationDescriptions tabulationDescriptions = new TabulationDescriptions(Qc4Launcher.Util.CommonFunction.SetDescriptionString());

            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Text_Summary_Change_Hyoutou_P))
            {
                if (Adsettings[F_Gt_GT_AddUp_Text_Summary_Change_Hyoutou_P] != null && Adsettings[F_Gt_GT_AddUp_Text_Summary_Change_Hyoutou_P] != string.Empty && Adsettings[F_Gt_GT_AddUp_Text_Summary_Change_Hyoutou_P] != QC4Common.Common.Constants.CRLFchar)
                    tabulationDescriptions.TotalDescription = Adsettings[F_Gt_GT_AddUp_Text_Summary_Change_Hyoutou_P];
            }

            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Text_Summary_Change_Non_P))
            {
                if (Adsettings[F_Gt_GT_AddUp_Text_Summary_Change_Non_P] != null && Adsettings[F_Gt_GT_AddUp_Text_Summary_Change_Non_P] != string.Empty && Adsettings[F_Gt_GT_AddUp_Text_Summary_Change_Non_P] != QC4Common.Common.Constants.CRLFchar)
                    tabulationDescriptions.NADescription = Adsettings[F_Gt_GT_AddUp_Text_Summary_Change_Non_P];
            }

            gTOptions.TabulationDescriptions = tabulationDescriptions;


            //tabulatewhole
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_All_Base_P))
            {
                if (Adsettings[F_Gt_GT_AddUp_Check_All_Base_P].ToUpper() == True)
                    gTOptions.TabulateFullQuantity1 = true;
            }

            //Graph
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Output_Graphs_P))
            {
                if (Adsettings[F_Gt_GT_AddUp_Check_Output_Graphs_P].ToUpper() == True)
                    gTOptions.OutputGraph = true;
            }

            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Output_Hide_ChoiceFor_PieChart_P))
            {
                if (Adsettings[F_Gt_GT_AddUp_Check_Output_Hide_ChoiceFor_PieChart_P].ToUpper() == True)
                    gTOptions.PieChartHideChoice = true;
            }

            if (gTOptions.PieChartHideChoice)
            {
                if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Text_Outputs_Ratios_EqualToLessThan_P))
                {
                    if (Adsettings[F_Gt_GT_AddUp_Text_Outputs_Ratios_EqualToLessThan_P] != null && Adsettings[F_Gt_GT_AddUp_Text_Outputs_Ratios_EqualToLessThan_P] != string.Empty)
                        gTOptions.HideChartDescriptionMaxPercent = Convert.ToInt32(Adsettings[F_Gt_GT_AddUp_Text_Outputs_Ratios_EqualToLessThan_P]);
                }
            }

            return gTOptions;
        }        

        private GTOptions ReadOptionsStandard(Workbook workbook, ref bool stopExecution, ref string errorMsg)
        {
            Worksheet sht = CrossSettingsReader.getASSheet(workbook);
            Dictionary<string, string> Adsettings = CrossSettingsReader.getAdvacedSettings(sht);
            GTOptions gTOptions = new GTOptions();

            string True = "TRUE";           

            //Filter
            string F_Gt_GT_AddUp_Check_Refine_Condition_S = "F_Gt_GT_AddUp_Check_Refine_Condition_S";
            string F_Gt_GT_AddUp_Combo_Conditional_Item_S = "F_Gt_GT_AddUp_Combo_Conditional_Item_{0}_S";
            string F_Gt_GT_AddUp_Combo_Conditional_Operator_S = "F_Gt_GT_AddUp_Combo_Conditional_Operator_{0}_S";
            string F_Gt_GT_AddUp_Combo_Conditional_Value_S = "F_Gt_GT_AddUp_Combo_Conditional_Value_{0}_S";
            string F_Gt_GT_AddUp_Option_Conditional_And_S = "F_Gt_GT_AddUp_Option_Conditional_And_{0}_S";
            string F_Gt_GT_AddUp_Option_Conditional_Or_S = "F_Gt_GT_AddUp_Option_Conditional_Or_{0}_S";

            //Sample Weighting
            string F_Gt_GT_AddUp_Check_Summary_WeightBack_S = "F_Cr_Cross_AddUp_Check_Summary_WeightBack_S";
            string F_Gt_GT_AddUp_Combo_Summary_WeightBack_S = "F_Cr_Cross_AddUp_Combo_Summary_WeightBack_S";
            string F_Gt_GT_AddUp_Check_Output_Sort_S = "F_Gt_GT_AddUp_Check_Output_Sort_S";

            //div
            string F_Gt_GT_AddUp_Combo_Classify_Item_S = "F_Gt_GT_AddUp_Combo_Classify_Item_S";
            string F_Gt_GT_AddUp_Combo_Classify_FolderPath_S = "F_Gt_GT_AddUp_Combo_Classify_FolderPath_S";

            //heading
            string F_Gt_GT_AddUp_Text_Option_Total_S = "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_S";
            string F_Gt_GT_AddUp_Text_No_Answer_S = "F_Cr_Cross_AddUp_Text_Summary_Change_Non_S";

            //Respondance
            string F_Gt_GT_AddUp_Check_All_Base_S = "F_Gt_GT_AddUp_Check_All_Base_S";

            //Graph
            string F_Gt_GT_AddUp_Check_Graph_Output_S = "F_Gt_GT_AddUp_Check_Graph_Output_S";
            string F_Gt_GT_AddUp_Check_Rate_S = "F_Gt_GT_AddUp_Check_Rate_S";
            string F_Gt_GT_AddUp_Text_Input_Rate_S = "F_Gt_GT_AddUp_Text_Input_Rate_S";

            string wbComboItem = null;
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Combo_Summary_WeightBack_S))
            {
                if (Adsettings[F_Gt_GT_AddUp_Combo_Summary_WeightBack_S] != null && Adsettings[F_Gt_GT_AddUp_Combo_Summary_WeightBack_S] != string.Empty)
                    wbComboItem = Adsettings[F_Gt_GT_AddUp_Combo_Summary_WeightBack_S];
            }

            bool showUnweightedCase = false;
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Output_Sort_S))
            {
                if (Adsettings[F_Gt_GT_AddUp_Check_Output_Sort_S] != null && Adsettings[F_Gt_GT_AddUp_Check_Output_Sort_S] != string.Empty)
                    showUnweightedCase = Adsettings[F_Gt_GT_AddUp_Check_Output_Sort_S].ToUpper() == True ? true : false;
            }

            // Sample Weighting
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Summary_WeightBack_S))
            {
                if (Adsettings[F_Gt_GT_AddUp_Check_Summary_WeightBack_S] != null && Adsettings[F_Gt_GT_AddUp_Check_Summary_WeightBack_S] != string.Empty)
                {
                    string enableWeightBack = Adsettings[F_Gt_GT_AddUp_Check_Summary_WeightBack_S];
                    if (enableWeightBack.ToUpper() == True)
                    {
                        if (wbComboItem != null)
                        {
                            bool isWeightValid = true;
                            gTOptions.WBDataList = new OutputUtil().GetWeightList(workbook, wbComboItem, ref isWeightValid);
                            gTOptions.IsDataValid = isWeightValid;

                            if (wbComboItem == "WeightBack")
                            {
                                Worksheet sh = CrossSettingsReader.GetSettingSheet(workbook);
                                Range start = sh.Cells[2, 10];
                                gTOptions.WBVariable = start.Value;
                            }
                            else
                            {
                                gTOptions.WBVariable = wbComboItem;
                            }

                            if (gTOptions.WBDataList != null && gTOptions.WBDataList.Count > 0)
                            {
                                if (showUnweightedCase)
                                {
                                    gTOptions.WBOn1 = WBSettingCode.WBOn | WBSettingCode.ShowPreWB;
                                    gTOptions.PreWbBase = true;
                                }
                                else
                                    gTOptions.WBOn1 = WBSettingCode.WBOn | WBSettingCode.HidePreWB;
                            }
                        }
                    }
                }
            }

            //Filter
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Refine_Condition_S) && Adsettings[F_Gt_GT_AddUp_Check_Refine_Condition_S].ToLower() == "true")
            {

                for (int i = 1; i <= 5; i++)
                {
                    if (CrossSettingsReader.checkSettingExist(Adsettings, string.Format(F_Gt_GT_AddUp_Combo_Conditional_Item_S, i))
                        && CrossSettingsReader.checkSettingExist(Adsettings, string.Format(F_Gt_GT_AddUp_Combo_Conditional_Operator_S, i))
                        && CrossSettingsReader.checkSettingExist(Adsettings, string.Format(F_Gt_GT_AddUp_Combo_Conditional_Value_S, i)))
                    {
                        if (!gTOptions.HasFilter)
                        {
                            gTOptions.HasFilter = true;
                            gTOptions.Filters = new List<FilterSettingsCr>();
                        }
                        FilterSettingsCr fs = new FilterSettingsCr();
                        gTOptions.Filters.Add(fs);
                        fs.variable = Adsettings[string.Format(F_Gt_GT_AddUp_Combo_Conditional_Item_S, i)];

                        if (!Definiotion.VariableDictionary.ContainsKey(fs.variable)) //integrity check
                        {
                            stopExecution = true;
                            errorMsg = LocalResource.GT_INVALID_FILTER_SETTINGS;
                            gTOptions.HasFilter = false;
                            gTOptions.Filters = new List<FilterSettingsCr>();
                            break;
                        }

                        fs.operatorType = Adsettings[string.Format(F_Gt_GT_AddUp_Combo_Conditional_Operator_S, i)];
                        fs.values = Adsettings[string.Format(F_Gt_GT_AddUp_Combo_Conditional_Value_S, i)];

                        if (Adsettings.ContainsKey(string.Format(F_Gt_GT_AddUp_Option_Conditional_And_S, i - 1))
                            && Adsettings[string.Format(F_Gt_GT_AddUp_Option_Conditional_And_S, i - 1)].ToLower() == "true")
                            fs.conditionType = AND;

                        else if (Adsettings.ContainsKey(string.Format(F_Gt_GT_AddUp_Option_Conditional_Or_S, i - 1))
                            && Adsettings[string.Format(F_Gt_GT_AddUp_Option_Conditional_Or_S, i - 1)].ToLower() == "true")
                            fs.conditionType = OR;
                    }
                }
            }

            //division
            if (CrossSettingsReader.checkSettingExist(Adsettings, F_Gt_GT_AddUp_Combo_Classify_Item_S) && CrossSettingsReader.checkSettingExist(Adsettings, F_Gt_GT_AddUp_Combo_Classify_FolderPath_S))
            {
                gTOptions.GroupVariable = Adsettings[F_Gt_GT_AddUp_Combo_Classify_Item_S];
                gTOptions.GroupFolderPath = Adsettings[F_Gt_GT_AddUp_Combo_Classify_FolderPath_S];
            }

            //heading
            TabulationDescriptions tabulationDescriptions = new TabulationDescriptions(Qc4Launcher.Util.CommonFunction.SetDescriptionString());

            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Text_Option_Total_S))
            {
                if (Adsettings[F_Gt_GT_AddUp_Text_Option_Total_S] != null && Adsettings[F_Gt_GT_AddUp_Text_Option_Total_S] != string.Empty && Adsettings[F_Gt_GT_AddUp_Text_Option_Total_S] != QC4Common.Common.Constants.CRLFchar)
                    tabulationDescriptions.TotalDescription = Adsettings[F_Gt_GT_AddUp_Text_Option_Total_S];
            }

            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Text_No_Answer_S))
            {
                if (Adsettings[F_Gt_GT_AddUp_Text_No_Answer_S] != null && Adsettings[F_Gt_GT_AddUp_Text_No_Answer_S] != string.Empty && Adsettings[F_Gt_GT_AddUp_Text_No_Answer_S] != QC4Common.Common.Constants.CRLFchar)
                    tabulationDescriptions.NADescription = Adsettings[F_Gt_GT_AddUp_Text_No_Answer_S];
            }

            gTOptions.TabulationDescriptions = tabulationDescriptions;

            //tabulatewhole
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_All_Base_S))
            {
                if (Adsettings[F_Gt_GT_AddUp_Check_All_Base_S].ToUpper() == True)
                    gTOptions.TabulateFullQuantity1 = true;
            }

            //Graph
            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Graph_Output_S))
            {
                if (Adsettings[F_Gt_GT_AddUp_Check_Graph_Output_S].ToUpper() == True)
                    gTOptions.OutputGraph = true;
            }

            if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Rate_S))
            {
                if (Adsettings[F_Gt_GT_AddUp_Check_Rate_S].ToUpper() == True)
                    gTOptions.PieChartHideChoice = true;
            }

            if (gTOptions.PieChartHideChoice)
            {
                if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Text_Input_Rate_S))
                {
                    if (Adsettings[F_Gt_GT_AddUp_Text_Input_Rate_S] != null && Adsettings[F_Gt_GT_AddUp_Text_Input_Rate_S] != string.Empty)
                        gTOptions.HideChartDescriptionMaxPercent = Convert.ToInt32(Adsettings[F_Gt_GT_AddUp_Text_Input_Rate_S]);
                }
            }

            return gTOptions;
        }

        private string GetGraphType(string graphTypeVal) // Mapping Qc4 graph type to QcWeb graphtype
        {
            string graphType = null;
            if (AddinResource.GTGraphQCPieChart.Equals(graphTypeVal)) //SA
                graphType = GraphType.GRAPH_TYPE_QCCIRCLE;
            else if (AddinResource.GTGraphQC100StackedBarChart.Equals(graphTypeVal)) //SA
                graphType = GraphType.GRAPH_TYPE_QCWIDTHBELT;
            else if (AddinResource.GTGraphQC100StackedColumnChart.Equals(graphTypeVal)) //SA
                graphType = GraphType.GRAPH_TYPE_QCHEIGHTBELT;
            else if (AddinResource.GTGraphQCBarChart.Equals(graphTypeVal)) //SA
                graphType = GraphType.GRAPH_TYPE_QCWIDTHSTICK;
            else if (AddinResource.GTGraphQCColumnChart.Equals(graphTypeVal)) //SA
                graphType = GraphType.GRAPH_TYPE_QCHEIGHTSTICK;
            else if (AddinResource.GTGraphQCPieRATChart.Equals(graphTypeVal)) //RAT
                graphType = GraphType.GRAPH_TYPE_QCMCIRCLERAT;
            else if (AddinResource.GTGraphQCRATBarChart.Equals(graphTypeVal)) //RAT
                graphType = GraphType.GRAPH_TYPE_QCWIDTHSTICKRAT;
            else if (AddinResource.GTGraphQCRATColumnChart.Equals(graphTypeVal)) //RAT
                graphType = GraphType.GRAPH_TYPE_QCHEIGHTSTICKRAT;
            else if (AddinResource.GTGraphQCStackedBarChart.Equals(graphTypeVal)) //MA
                graphType = GraphType.GRAPH_TYPE_QCWIDTHONSTICK;
            else if (AddinResource.GTGraphQCStackedColumnChart.Equals(graphTypeVal)) //MA
                graphType = GraphType.GRAPH_TYPE_QCHEIGHTONSTICK;
            else if (AddinResource.GTGraphQCMPieChart.Equals(graphTypeVal)) //MTS
                graphType = GraphType.GRAPH_TYPE_QCMCIRCLE;
            else if (AddinResource.GTGraphQCMBarChart.Equals(graphTypeVal)) //MTS
                graphType = GraphType.GRAPH_TYPE_QCMWIDTHSTICK;
            else if (AddinResource.GTGraphQCMColumnChart.Equals(graphTypeVal)) //MTS
                graphType = GraphType.GRAPH_TYPE_QCMHEIGHTSTICK;

            return graphType;
        }


    }//- End Class

    internal class GTSettings
    {
        internal List<GrossTableInput> GTInputs = new List<GrossTableInput>();
        internal MarkingType Markingtype { get; set; } = 0;
        internal SignificanceTestLevel Significancetestlevel { get; set; } = 0;
        internal List<double> TestLevels { get; set; } = new List<double>();
        internal GTOptions GtOptions { get; set; } = new GTOptions();
    }

    internal class GTOptions
    {
        internal string Reportprefix { get; set; } = "Report";
        internal string Xlbooknameprefix { get; set; } = "GT";
        internal TableType Tabletype { get; set; } = TableType.NPer | TableType.N | TableType.Per;
        internal TableOrientation Tableorientation { get; set; } = TableOrientation.Landscape;
        //internal TableType Pagesetuptabletype { get; set; } = TableType.NPer | TableType.N | TableType.Per;
        internal TableType Pagesetuptabletype { get; set; } = (TableType)0;
        internal int Minsamplescountonmarking { get; set; } = 0;
        internal XlPaperSize Papersize { get; set; } = XlPaperSize.xlPaperA4;
        internal XlPageOrientation Paperorientation { get; set; } = XlPageOrientation.xlLandscape;
        internal TablesOnOneSheet Tablesononesheet { get; set; } = TablesOnOneSheet.Multi;
        internal int Level2highcolorindex { get; set; } = int.Parse(LocalResource.REPORT_HATCHING_LEVEL2_HIGH_COLOR_INDEX_DEFAULT);
        internal int Level1highcolorindex { get; set; } = int.Parse(LocalResource.REPORT_HATCHING_LEVEL1_HIGH_COLOR_INDEX_DEFAULT);
        internal int Level1lowcolorindex { get; set; } = int.Parse(LocalResource.REPORT_HATCHING_LEVEL1_LOW_COLOR_INDEX_DEFAULT);
        internal int Level2lowcolorindex { get; set; } = int.Parse(LocalResource.REPORT_HATCHING_LEVEL2_LOW_COLOR_INDEX_DEFAULT);
        internal int Level1percent { get; set; } = int.Parse(LocalResource.REPORT_HATCHING_LEVEL1_PERCENT_DEFAULT);
        internal int Level2percent { get; set; } = int.Parse(LocalResource.REPORT_HATCHING_LEVEL2_PERCENT_DEFAULT);
        internal ShowCode ShowNACode1 { get; set; } = (ShowCode)3;
        internal ShowCode ShowIVCode1 { get; set; } = 0;
        internal string FilteringExpression1 { get; set; } = "[性別]の値が無回答ではない";
        internal TabulationDescriptions TabulationDescriptions { get; set; } = new TabulationDescriptions(Qc4Launcher.Util.CommonFunction.SetDescriptionString());
        internal WBSettingCode WBOn1 { get; set; } = WBSettingCode.WBOff;
        internal List<Data> WBDataList { get; set; } = null;
        internal bool HasFilter { get; set; } = false;
        internal List<FilterSettingsCr> Filters { get; set; } = null;
        internal string LocalizedFilteringExpression1 { get; set; } = "";
        internal string GroupFolderPath { get; set; } = null;
        internal string GroupVariable { get; set; } = null;
        internal bool TabulateFullQuantity1 { get; set; } = false;
        internal bool OutputGraph { get; set; } = false;
        internal bool PieChartHideChoice { get; set; } = false;
        internal int HideChartDescriptionMaxPercent { get; set; } = -1;
        internal bool VisibleUnfitFlagAsFlag { get; set; } = true;
        internal bool NoAnswerDenominatorFlag { get; set; } = false;
        internal bool TargetNoAnswerOnOff { get; set; } = true;
        public bool PreWbBase { get; set; } = true;
        public bool IsDataValid { get; set; } = true;
        public string WBVariable { get; set; } = null;
    }

    internal class GrossTableInput
    {
        internal string VariableName { get; set; }
        internal string GTTableType { get; set; }
        internal Microsoft.Office.Core.XlChartType? ChartType { get; set; }
        internal bool IsMatrixPatent { get; set; } = false;
        internal List<string> MatrixColumns { get; set; } = null;
        internal SignificanceTestCode SigTestCode { get; set; } = SignificanceTestCode.Off;
        internal bool HasGraph { get; set; } = false; // changed for graph
        internal string GraphType { get; set; }
        internal QuestionType QuestionType2 { get; set; } // This will be assigned when application creates tsv file settings
        internal string TableHeading { get; set; } = null;
        internal bool HasWeight { get; set; } = false;
    }
}
