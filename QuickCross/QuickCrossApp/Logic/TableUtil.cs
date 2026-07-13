using Macromill.QCWeb.Common;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using QC4Common.Model;
using Qc4Launcher.DB;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using static Macromill.QCWeb.Common.GlobalsCommonConstant;
using static Macromill.QCWeb.Question.Questions;
using static Macromill.QCWeb.ReportRequest.Outputs;
using static Macromill.QCWeb.ReportRequest.Reportsets;
using Constants = Qc4Launcher.Util.Constants;
using DBHelperCommon = QC4Common.DB.DBHelper;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Logic
{
    internal class TableUtil
    {
        internal static void CreateTable(Macromill.QCWeb.Tabulation.DataWithMarking[][,] results)
        {
            throw new NotImplementedException();
        }

        // not using
        public static Tables.CrossTable SetOutputRequestTableCross(OutputCross cross, DataWithMarking[,] tabulation,
                                         Questions.Question question, Questions.Question axis1, Questions.Question axis2,
                                         bool hasCount, string narrowingCondition = null, string wBValue = null)
        {

            QuestionSettings qstnDet = Definiotion.VariableDictionary[question.Name];
            int sortNo = 0;
            QuestionType qType = question.QuestionType;
            if (qstnDet.Sort.Length > 0)
            {
                sortNo = Convert.ToInt32(qstnDet.Sort);
                qType = qType | QuestionType.Sort;
            }
            Tables.CrossTable table =
                (Tables.CrossTable)(cross.Tables as Tables).Add(qType, tabulation, OutputType.Cross);

            //if (colorIndexArray != null)
            //{
            //    if (polylineFlag)
            //    {
            //        string[] polylineColorIndexArray = null;
            //        GetColorCross(question.QCWebID, (decimal)crossItem.CrossScenarioItemId
            //                      , GlobalsConstant.GRAPH_TYPE_QCLINE, out polylineColorIndexArray);
            //        Array.Resize<string>(ref colorIndexArray, 21);
            //        for (int i = 1; i < 21; ++i)
            //        {
            //            colorIndexArray[i] = polylineColorIndexArray[i - 1];
            //        }
            //    }
            //    MsoGradientStyle gradStyle = CovertToMsoGradientStyle(gradationType);
            //    int gradVariant = gradStyle == MsoGradientStyle.msoGradientFromCenter ? 1 : 3;
            //    table.SetChartInformation(ConvertToXlChartType(graphType)
            //        , string.Join(" ", colorIndexArray), gradStyle, gradVariant);

            //    // プロットエリア背景の設定
            //    if (interiorGradationType != null && interiorColorIndex != null)
            //    {
            //        ChartInformation chart = table.Chart;
            //        chart.InteriorGradientStyle = CovertToMsoGradientStyle(interiorGradationType);
            //        chart.InteriorColorIndex = int.Parse(interiorColorIndex);
            //    }
            //}
            table.SetQuestionInformation(question.Name, question.Description, question.QuestionType, hasCount, null, !String.IsNullOrEmpty(qstnDet.AddSubTotal) ? qstnDet.SubTotalCount : 0, qNumber: question.Number2);
            if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                table.ChildQuestionsCount = question.ChildQuestions.Count;
            }

            // 選択肢情報を設定する
            if (question.Sectors != null)
            {
                for (int i = 1; i <= question.Sectors.Count; ++i)
                {
                    Sectors.Sector sector = (Sectors.Sector)question.Sectors[i];
                    table.AddSectorInformation(sector.Weight, i <= sortNo ? false : true);
                }

                if (!String.IsNullOrEmpty(qstnDet.AddSubTotal) && qstnDet.SubTotalCount > 0)
                {
                    foreach (QuestionSettings.SubTotal subTotal in qstnDet.SubTotals)
                    {
                        table.AddSectorInformation("0", true);
                    }
                }
            }

            // クロス軸の選択肢数をセット
            AxesInformation axesgroup = table.AxesGroups.Add();
            if (axis1 != null)
            {
                axesgroup.Add(axis1.Sectors.Count);
            }
            if (axis2 != null)
            {
                axesgroup.Add(axis2.Sectors.Count);
            }

            // 折れ線の設定を行う
            //if (polylineFlag)
            //{
            //    TPolylineCategoryListCB cb = new TPolylineCategoryListCB();
            //    cb.SetupSelect_TCrossScenarioItem().WithTCrossScenarioTarget();
            //    cb.Query().SetCrossScenarioItemId_Equal(crossItem.CrossScenarioItemId);
            //    cb.Query().AddOrderBy_PolylineCategoryListId_Asc();
            //    ListResultBean<TPolylineCategoryList> list = tPolylineCategoryListBhv.SelectList(cb);
            //    foreach (TPolylineCategoryList bean in list)
            //    {
            //        // 出力形式により利用するカラムが異なる
            //        table.AddLineChartRow(
            //            tablesOnOneSheet == TablesOnOneSheet.Single ? (int)bean.ArrayNoSingular : (int)bean.ArrayNoPlural
            //        );
            //    }
            //}

            //#region 検定対応
            //if (!reportFlag)
            //{
            //    // 項目間検定の種類
            //    table.SignificancetestCode = significancetestCode;
            //}
            // #endregion
            return table;
        }

        internal static Tables.CrossTable SetOutputRequestTableCross(Excel.Workbook workbook, OutputCross cross, Questions.Question question, DataWithMarking[][,] tabulationArray,
            List<CossTableInput> crTableSetItems, Questions questions, SQLiteConnection con,
            Dictionary<string, int> excludeCntMap, bool tabulateFullQuantity, string tableName, bool isChart = false, bool showNa = false, bool hasCount = false,
            bool hasWeight = false, bool checkCross = false, string narrowingCondition = null, bool enableSort = true, string wBValue = null)
        {

            QuestionSettings qstnDetT = Definiotion.VariableDictionary[question.Name];
            int sortNo = 0;
            QuestionType qType = question.QuestionType;
            if (qstnDetT.Sort.Length > 0)
            {
                sortNo = Convert.ToInt32(qstnDetT.Sort);
                qType = qType | QuestionType.Sort;
            }
            Tables.CrossTable table =
                  (Tables.CrossTable)(cross.Tables as Tables).Add(qType, tabulationArray);
            table.enableSort = enableSort;
            bool isAdded = false;
            string[] colorIndexArray = null;
            string gradationType = null;
            string graphType = null;
            string interiorGradationType = null;
            string interiorColorIndex = null;

            if (isChart)
            {
                // colorIndexArray = new string[]{ "3","45","6","43","50","42","41","11","24","38","3","45","6","43","50","42","41","11","24","38"};
                colorIndexArray = new string[] { "3", "45", "43", "50", "42", "41", "13", "7", "44", "6", "4", "8", "33", "54", "38", "40", "36", "35", "34", "37" };
                //colorIndexArray = new string[] { "41", "41", "41", "41", "41", "41", "41", "41", "41", "41", "41", "41", "41", "41", "41", "41", "41", "41", "41", "41" };
                // gradationType = "002";
                gradationType = "001";
                graphType = "002";
                //graphType = "005";
                interiorGradationType = null;
                interiorColorIndex = null;
            }
            int prevLine = 0;
            string preAxis1Varaible = null;
            string preAxis2Varaible = null;
            foreach (CossTableInput crTableSet in crTableSetItems)
            {

                if (colorIndexArray != null)
                {
                    //if (polylineFlag)
                    //{
                    //    string[] polylineColorIndexArray = null;
                    //    GetColorCross(question.QCWebID, (decimal)crossItem.CrossScenarioItemId
                    //                  , GlobalsConstant.GRAPH_TYPE_QCLINE, out polylineColorIndexArray);
                    //    Array.Resize<string>(ref colorIndexArray, 21);
                    //    for (int i = 1; i < 21; ++i)
                    //    {
                    //        colorIndexArray[i] = polylineColorIndexArray[i - 1];
                    //    }
                    //}
                    MsoGradientStyle gradStyle = CovertToMsoGradientStyle(gradationType);
                    int gradVariant = gradStyle == MsoGradientStyle.msoGradientFromCenter ? 1 : 3;
                    table.SetChartInformation(ConvertToXlChartType(graphType)
                        , string.Join(" ", colorIndexArray), gradStyle, gradVariant);

                    // プロットエリア背景の設定
                    if (interiorGradationType != null && interiorColorIndex != null)
                    {
                        ChartInformation chart = table.Chart;
                        chart.InteriorGradientStyle = CovertToMsoGradientStyle(interiorGradationType);
                        chart.InteriorColorIndex = int.Parse(interiorColorIndex);
                    }
                }

                table.SetQuestionInformation(question.Name, question.Description, qType, hasCount, null, String.IsNullOrEmpty(qstnDetT.AddSubTotal) || checkCross ? 0 : qstnDetT.SubTotalCount,
                    hasWeight, narrowingCondition, qstnDetT.TableHeading, question.Number2, wBValue, tabulateFullQuantity);
                if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    table.ChildQuestionsCount = question.ChildQuestions.Count;
                }

                // 選択肢情報を設定する
                if (!isAdded && question.Sectors != null)
                {
                    for (int i = 1; i <= question.Sectors.Count; ++i)
                    {
                        Sectors.Sector sector = (Sectors.Sector)question.Sectors[i];
                        table.AddSectorInformation(sector.Weight, i <= sortNo ? false : true);
                    }
                    if (!String.IsNullOrEmpty(qstnDetT.AddSubTotal) && qstnDetT.SubTotalCount > 0 && !checkCross)
                    {
                        foreach (QuestionSettings.SubTotal subTotal in qstnDetT.SubTotals)
                        {
                            table.AddSectorInformation("0", true);
                        }
                    }
                    isAdded = true;
                }

                // クロス軸の選択肢数をセット
                string axis1Varaible = crTableSet.axis1;
                string axis2Varaible = crTableSet.axis2;
                Question axis1;
                QuestionSettings qstnDet;
                if (String.IsNullOrEmpty(axis1Varaible))
                {
                    axis1 = new Question();
                    axis1.QuestionType = QuestionType.SA;
                }
                else
                {
                    qstnDet = Definiotion.VariableDictionary[axis1Varaible];
                    axis1 = (Question)questions[qstnDet.Id];
                }
                Question axis2 = null;
                if (null != axis2Varaible)
                {
                    qstnDet = Definiotion.VariableDictionary[axis2Varaible];
                    axis2 = (Question)questions[qstnDet.Id];
                }

                AxesInformation axesgroup = table.AxesGroups.Add();
                if (table.AxesGroups.Count == 1 || axis1.Sectors.Count == 0)
                {
                    axesgroup.ShowTotal = true;
                }
                else
                {
                    axesgroup.ShowTotal = tabulateFullQuantity ? false : !checkAdjuscentCount(workbook, axis1.ColumnName, null != axis2 ? axis2.ColumnName : null, preAxis1Varaible, preAxis2Varaible, con, excludeCntMap, tableName);
                }
                preAxis1Varaible = axis1.ColumnName;
                preAxis2Varaible = null != axis2 ? axis2.ColumnName : null;

                if (axis1 != null)
                {
                    axesgroup.Add(axis1.Sectors.Count);
                }
                if (axis2 != null)
                {
                    axesgroup.Add(axis2.Sectors.Count);
                }


                if (axis2 != null)
                {
                    if (crTableSet.lineSettings != null) // to do
                    {
                        int prevLine2 = prevLine;
                        int[] criteriaSectorList;
                        GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
                                                                QuestionType.SA, crTableSet.lineSettings, out criteriaSectorList, axis1.Sectors.Count);
                        for (int l = 0; l < (axis1.Sectors.Count + (showNa ? 1 : 0)); l++)
                        {
                            foreach (int sector in criteriaSectorList)
                            {
                                table.AddLineChartRow(prevLine2 + sector);

                            }
                            prevLine2 += axis2.Sectors.Count + 2;
                        }
                    }
                    prevLine = prevLine + (axis1.Sectors.Count + 2) * (axis2.Sectors.Count + 2);
                }
                else
                {
                    if (crTableSet.lineSettings != null) // to do
                    {
                        int[] criteriaSectorList;
                        GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
                                                                QuestionType.SA, crTableSet.lineSettings, out criteriaSectorList, axis1.Sectors.Count);
                        if (criteriaSectorList != null)
                        {
                            foreach (int sector in criteriaSectorList)
                            {
                                table.AddLineChartRow(prevLine + sector);

                            }
                        }
                    }
                    prevLine = prevLine + axis1.Sectors.Count + 2;
                }

            }

            //#region 検定対応
            //if (!reportFlag) {
            //    // 項目間検定の種類
            //    table.SignificancetestCode = significancetestCode;
            //}
            //#endregion
            return table;
        }


        public static bool checkAdjuscentCount(Excel.Workbook workbook, string axis1Varaible, string axis2Varaible, string preAxis1Varaible, string preAxis2Varaible, SQLiteConnection con, Dictionary<string, int> excludeCntMap, string tableName)
        {
            int cntAxis1 = findExcludeCnt(workbook, con, excludeCntMap, axis1Varaible, tableName: tableName);

            int preCntAxis1 = findExcludeCnt(workbook, con, excludeCntMap, preAxis1Varaible, tableName: tableName);

            if (cntAxis1 != preCntAxis1) return false;

            int cntPreAxis1Axis1 = findExcludeCnt(workbook, con, excludeCntMap, preAxis1Varaible, axis1Varaible, tableName: tableName);

            if (cntAxis1 != cntPreAxis1Axis1 || preCntAxis1 != cntPreAxis1Axis1) return false;

            if (axis2Varaible != null && preAxis2Varaible != null)
            {
                int cntAxis2 = findExcludeCnt(workbook, con, excludeCntMap, axis2Varaible, tableName: tableName);
                int preCntAxis2 = findExcludeCnt(workbook, con, excludeCntMap, preAxis2Varaible, tableName: tableName);

                if (cntAxis2 != preCntAxis1 || cntAxis2 != preCntAxis2) return false;


                int cntPreAxis1PreAxis2Axis1Axis2 = findExcludeCnt(workbook, con, excludeCntMap, preAxis1Varaible, preAxis2Varaible, axis1Varaible, axis2Varaible, tableName: tableName);
                if (cntPreAxis1PreAxis2Axis1Axis2 != cntAxis2 || cntPreAxis1PreAxis2Axis1Axis2 != preCntAxis2) return false;

            }
            else if (axis2Varaible != null && preAxis2Varaible == null)
            {
                int cntAxis2 = findExcludeCnt(workbook, con, excludeCntMap, axis2Varaible, tableName: tableName);
                if (cntAxis2 != preCntAxis1) return false;
                int cntPreAxis1Axis1Axis2 = findExcludeCnt(workbook, con, excludeCntMap, preAxis1Varaible, axis1Varaible, axis2Varaible, tableName: tableName);
                if (cntPreAxis1Axis1Axis2 != cntAxis2) return false;

            }
            else if (axis2Varaible == null && preAxis2Varaible != null)
            {
                int preCntAxis2 = findExcludeCnt(workbook, con, excludeCntMap, preAxis2Varaible, tableName: tableName);
                if (preCntAxis2 != cntAxis1) return false;
                int cntPreAxis1PreAxis2Axis1 = findExcludeCnt(workbook, con, excludeCntMap, preAxis1Varaible, preAxis2Varaible, axis1Varaible, tableName: tableName);
                if (cntPreAxis1PreAxis2Axis1 != preCntAxis2) return false;
            }

            return true;
        }

        public static int findExcludeCnt(Excel.Workbook workbook, SQLiteConnection con, Dictionary<string, int> excludeCntMap, string varaible1, string varaible2 = null, string varaible3 = null, string varaible4 = null, string tableName = "answers")
        {

            int cntAxis = 0;
            string axisVar = varaible1;
            string whereCnd = varaible1 + " in ('*' , '**') ";
            bool isMv = false;
            string tableNameAnswer = tableName;

            if (varaible1 == null)
            {
                return 0;
            }
            tableName = DBHelperCommon.getTableName(con, varaible1, out isMv);


            if (varaible2 != null)
            {
                axisVar += ":" + varaible2;
                whereCnd += " AND " + varaible2 + " in ('*' , '**') ";
                if (!isMv) tableName = DBHelperCommon.getTableName(con, varaible2, out isMv);
            }

            if (varaible3 != null)
            {
                axisVar += ":" + varaible3;
                whereCnd += " AND " + varaible3 + " in ('*' , '**') ";
                if (!isMv) tableName = DBHelperCommon.getTableName(con, varaible3, out isMv);
            }

            if (varaible4 != null)
            {
                axisVar += ":" + varaible4;
                whereCnd += " AND " + varaible4 + " in ('*' , '**') ";
                if (!isMv) tableName = DBHelperCommon.getTableName(con, varaible4, out isMv);
            }

            if (excludeCntMap.ContainsKey(axisVar))
            {
                cntAxis = excludeCntMap[axisVar];
            }
            else
            {
                try
                {
                    if (isMv) { 
                        cntAxis = DBHelper.ExecuteScalar("Select count(" + varaible1 + ") from " + tableNameAnswer + " a join "
                                        + tableName + " m on a.sort_no = m.sort_no where " + whereCnd, con);
                    }
                    else
                    {
                        cntAxis = DBHelper.ExecuteScalar("Select count(" + varaible1 + ") from " + tableName + " where " + whereCnd, con);
                    }
                }
                catch (Exception exc)
                {
                    if (exc.Message.Contains("no such column")) // If no such column, load null data
                    {
                        cntAxis = 0;
                    }
                    else
                        throw exc;
                }
                excludeCntMap[axisVar] = cntAxis;
            }

            return cntAxis;

        }


        private static MsoGradientStyle CovertToMsoGradientStyle(string gradationType)
        {
            switch (gradationType)
            {
                case CODE_VALUE.GRADATION_TYPE_NONE:
                    return (MsoGradientStyle)0;
                case CODE_VALUE.GRADATION_TYPE_MSO_GRADIENT_HORIZONTAL:
                    return MsoGradientStyle.msoGradientHorizontal;
                case CODE_VALUE.GRADATION_TYPE_MSO_GRADIENT_VERTICAL:
                    return MsoGradientStyle.msoGradientVertical;
                case CODE_VALUE.GRADATION_TYPE_MSO_GRADIENT_DIAGONALUP:
                    return MsoGradientStyle.msoGradientDiagonalUp;
                case CODE_VALUE.GRADATION_TYPE_MSO_GRADIENT_DIAGONALDOWN:
                    return MsoGradientStyle.msoGradientDiagonalDown;
                case CODE_VALUE.GRADATION_TYPE_MSO_GRADIENT_FROMCORNER:
                    return MsoGradientStyle.msoGradientFromCorner;
                case CODE_VALUE.GRADATION_TYPE_MSO_GRADIENT_FROMCENTER:
                    return MsoGradientStyle.msoGradientFromCenter;
                default:
                    // グラデーションタイプの変換に失敗しました。:{0}
                    // throw new QCWebException("QCS0502000020", new string[] { gradationType }
                    //                      , GlobalsCommonConstant.LogLevel.FATAL, null);
                    throw new Exception();
            }
        }

        private static XlChartType ConvertToXlChartType(string graphType)
        {
            XlChartType chartType = (XlChartType)0;
            if (string.IsNullOrEmpty(graphType)) return chartType;

            switch (graphType)
            {
                case GRAPH_TYPE_QCWIDTHBELT:                            // 横帯
                    chartType = XlChartType.xlBarStacked100;
                    break;
                case GRAPH_TYPE_SA_MATRIX_QCM_WIDTH_BELT_CODE_VALUE:    // MT横帯
                    chartType = XlChartType.QCM | XlChartType.xlBarStacked100;
                    break;
                case GRAPH_TYPE_QCWIDTHONSTICK:                         // 積上横棒
                    chartType = XlChartType.xlBarStacked;
                    break;
                case GRAPH_TYPE_QCHEIGHTBELT:                           // 縦帯
                    chartType = XlChartType.xlColumnStacked100;
                    break;
                case GRAPH_TYPE_SA_MATRIX_QCM_HEIGHT_BELT_CODE_VALUE:   // MT縦帯
                    chartType = XlChartType.QCM | XlChartType.xlColumnStacked100;
                    break;
                case GRAPH_TYPE_QCHEIGHTONSTICK:                        // 積上縦棒
                    chartType = XlChartType.xlColumnStacked;
                    break;
                case GRAPH_TYPE_QCCIRCLE:                               // 円
                    chartType = XlChartType.xlPie;
                    break;
                case GRAPH_TYPE_QCMCIRCLE:                              // MT円
                    chartType = XlChartType.QCM | XlChartType.xlPie;
                    break;
                case GRAPH_TYPE_QCMCIRCLERAT:                           // 円RAT
                    chartType = XlChartType.RAT | XlChartType.xlPie;
                    break;
                case GRAPH_TYPE_QCWIDTHSTICK:                           // 横棒
                    chartType = XlChartType.xlBarClustered;
                    break;
                case GRAPH_TYPE_QCMWIDTHSTICK:                          // MT横棒
                    chartType = XlChartType.QCM | XlChartType.xlBarClustered;
                    break;
                case GRAPH_TYPE_QCWIDTHSTICKRAT:                        // 横棒RAT
                    chartType = XlChartType.RAT | XlChartType.xlBarClustered;
                    break;
                case GRAPH_TYPE_QCHEIGHTSTICK:                          // 縦棒
                    chartType = XlChartType.xlColumnClustered;
                    break;
                case GRAPH_TYPE_QCMHEIGHTSTICK:                         // MT縦棒
                    chartType = XlChartType.QCM | XlChartType.xlColumnClustered;
                    break;
                case GRAPH_TYPE_QCHEIGHTSTICKRAT:                       // 縦棒RAT
                    chartType = XlChartType.RAT | XlChartType.xlColumnClustered;
                    break;
                case GRAPH_TYPE_QCLINE:                                 // 折れ線
                    chartType = XlChartType.xlLine;
                    break;
                default:
                    // グラフ種別の変換に失敗しました。:{0}
                    //   throw new QCWebException("QCS0502000021", new string[] { graphType }
                    //                            , GlobalsCommonConstant.LogLevel.FATAL, null);
                    throw new Exception();
            }
            return chartType;
        }

        //private Tables.GTTable SetOutputRequestTableGT(DataWithMarking[,] tabulation, Outputs.OutputGT gt
        //                                , Questions.Question question, string gradationType, string graphType
        //                                , string[] colorIndexArray, string scenarioDescription, string interiorGradationType
        //                                , string interiorColorIndex, SignificanceTestCode significancetestCode = SignificanceTestCode.Off
        //                                , string graphTypeOrg = null, bool reportFlag = false)
        //{
        /// <summary>
        /// Method to set output the request table for GT
        /// </summary>
        /// <param name="tabulation">Summary Tabulation Array</param>
        /// <param name="gt">OutputGT object</param>
        /// <param name="question">Question details</param>
        /// <param name="questionType">Question type</param>
        /// <param name="gradationType">Gradation type</param>
        /// <param name="interiorGradationType">Interior Gradation type</param>
        /// <param name="interiorColorIndex">Interior Color Index</param>
        /// <param name="colorIndexArray">Color Index Array</param>
        /// <param name="graphTypeOrg">Graph Type for Org</param>
        /// <param name="significancetestCode">Significance Test Code flag</param>
        /// <param name="tableHeading">Table Heading</param>
        /// <param name="hasCount">bool value to check Count</param>
        /// <param name="isMatrix">bool value to check whether it is Matrix or not</param>
        /// <param name="hasWeight">bool value represent whether there is weight or not</param>
        /// <param name="wbVariable">Weightback variable</param>
        /// <param name="tabulateFullQuantity">bool value to represent tabulate Quantity</param>
        /// <returns></returns>
        public static Tables.GTTable SetOutputRequestTableGT(DataWithMarking[,] tabulation, OutputGT gt
                            , Question question, QuestionType questionType, string gradationType, string interiorGradationType, string interiorColorIndex, string[] colorIndexArray, string graphTypeOrg = null, SignificanceTestCode significancetestCode = SignificanceTestCode.Off
                            , string tableHeading = null, bool hasCount = false, bool isMatrix = false, bool hasWeight = false, string wbVariable = null, bool tabulateFullQuantity = false)
        {
            QuestionSettings qstnDetT = Definiotion.VariableDictionary[question.Name];
            int sortNo = 0;
            //QuestionType qType = question.QuestionType;
            if (qstnDetT.Sort.Length > 0)
            {
                sortNo = Convert.ToInt32(qstnDetT.Sort);
                questionType = questionType | QuestionType.Sort;
            }

            Tables.GTTable table =
                (Tables.GTTable)(gt.Tables as Tables).Add(questionType, tabulation, OutputType.GT);

            if (gradationType != null)
            {
                // グラフ情報を設定
                MsoGradientStyle gradStyle = CovertToMsoGradientStyle(gradationType);
                int gradVariant = gradStyle == MsoGradientStyle.msoGradientFromCenter ? 1 : 3;
                table.SetChartInformation(
                    ConvertToXlChartType(graphTypeOrg), string.Join(" ", colorIndexArray)
                    , gradStyle, gradVariant
                    , (questionType & QuestionType.MatrixParent) == QuestionType.MatrixParent);

                // プロットエリア背景の設定
                if (interiorGradationType != null && interiorColorIndex != null)
                {
                    ChartInformation chart = table.Chart;
                    chart.InteriorGradientStyle = CovertToMsoGradientStyle(interiorGradationType);
                    chart.InteriorColorIndex = int.Parse(interiorColorIndex);
                }
            }

            string qDescription = qstnDetT.Question;
            //string tableHeading = qstnDetT.TableHeading;

            //if (isMatrixParent)
            //{
            //    table.SetQuestionInformation(question.Description, question.Number, questionType);
            //}
            //else 【】
            //{
            //table.SetQuestionInformation(question.Name, tableHeading != null ? tableHeading + "\n" + (question.Description != null ? question.Description : "") : question.Description, questionType, 

            string tableDescription = null;
            string graphDescription = null;

            if (isMatrix)
            {
                tableDescription = qDescription != null ? qDescription : string.Empty;
                graphDescription = tableHeading != null ? tableHeading : string.Empty;
            }
            else
            {
                tableDescription = qDescription != null ? qDescription : string.Empty;
                graphDescription = tableHeading != null ? tableHeading : string.Empty;
                //if (string.IsNullOrEmpty(tableDescription))
                //{
                //    tableDescription = qDescription != null ? qDescription : string.Empty;
                //    graphDescription = tableDescription;
                //}
                //else
                //{
                //    string addDesc = qDescription != null ? string.Empty : string.Empty;
                //    tableDescription += addDesc;
                //    if ((graphDescription + addDesc).Length <= 250)
                //    {
                //        graphDescription += addDesc;
                //    }
                //}
            }

            table.SetQuestionInformation((question.Number != null && question.Number.Trim().Length > 0) ? question.Number : question.Name, tableDescription, questionType,
    hasCount, null, !String.IsNullOrEmpty(qstnDetT.AddSubTotal) ? qstnDetT.SubTotalCount : 0, tableHeading: graphDescription, hasWeight: hasWeight, wBValue: wbVariable, tabulateFullQuantity: tabulateFullQuantity);
            //}

            if ((questionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                if (question.ChildQuestions != null)
                    table.ChildQuestionsCount = question.ChildQuestions.Count;
                else
                    table.ChildQuestionsCount = 0;
            }

            // 選択肢情報を設定する
            if (question.Sectors != null)
            {
                for (int i = 1; i <= question.Sectors.Count; ++i)
                {
                    Sectors.Sector sector = (Sectors.Sector)question.Sectors[i];
                    table.AddSectorInformation(sector.Weight, i <= sortNo ? false : true);
                }


                if (!String.IsNullOrEmpty(qstnDetT.AddSubTotal) && qstnDetT.SubTotalCount > 0)
                {
                    foreach (QuestionSettings.SubTotal subTotal in qstnDetT.SubTotals)
                    {
                        table.AddSectorInformation("0", true);
                    }
                }

            }

            //bool reportFlag = false;
            #region 検定対応
            //if (!reportFlag) // This is commented because Qc4 work always as report mode
            //{
            // 項目間検定の種類
            table.SignificancetestCode = significancetestCode;
            //}
            #endregion

            // Customization as per QcWeb
            return table;
        }
    }
}