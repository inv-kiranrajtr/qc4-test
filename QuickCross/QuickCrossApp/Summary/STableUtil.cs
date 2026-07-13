using Macromill.QCWeb.Question;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Qc4Launcher.DB;
using static Macromill.QCWeb.ReportRequest.Outputs;
using System.Collections.Generic;
using System.Data.SQLite;
using static Macromill.QCWeb.Question.Questions;
using Qc4Launcher.Util;
using QC4Common.Model;
using System;

namespace Qc4Launcher.Summary
{
    class STableUtil
    {
        internal static Tables.CrossTable SetOutputRequestTableCross(OutputCross cross, Questions.Question question, DataWithMarking[][,] tabulationArray, DataWithMarking[][,] tabulationArrayTotal,
            List<SummaryReader.SummaryTableInput> stTableSetItems, Questions questions, SQLiteConnection con, Dictionary<string, int> excludeCntMap, 
            bool tabulateFullQuantity, int count, string summaryName, string tableName, string tableTitle, bool hasCount = false,string wbVariable = null, DataWithMarking[][,] tabulationArrayUnweightedTotal=null)
        {
            QuestionSettings qstnDetT = Definiotion.VariableDictionary[question.Name];
            Tables.CrossTable table =
                  (Tables.CrossTable)(cross.Tables as Tables).Add(QuestionType.SA, tabulationArray, tabulationArrayTotal, tabulationArrayUnweightedTotal);
            bool isAdded = false;

            string preAxis1Varaible = null;
            string preAxis2Varaible = null;
            foreach (SummaryReader.SummaryTableInput stTableSet in stTableSetItems)
            {
                table.SetQuestionInformation(question.Name, tableTitle, QuestionType.SA, false, 
                    summaryName, 0, false, null, qstnDetT.TableHeading,null, wbVariable, tabulateFullQuantity);
                if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    table.ChildQuestionsCount = question.ChildQuestions.Count;
                }

                if (!isAdded)
                {
                    for (int i = 1; i <= count; ++i)
                    {
                        table.AddSectorInformation(null, false);
                    }
                    isAdded = true;
                }

                // クロス軸の選択肢数をセット
                string axis1Varaible = stTableSet.axis1;
                QuestionSettings qstnDet = Definiotion.VariableDictionary[axis1Varaible];
                Question axis1 = (Question)questions[qstnDet.Id];
                AxesInformation axesgroup = table.AxesGroups.Add();
                if (table.AxesGroups.Count == 1)
                {
                    axesgroup.ShowTotal = true;
                }
                else
                {
                    axesgroup.ShowTotal = tabulateFullQuantity ? false : !checkAdjuscentCount(axis1.ColumnName, null, preAxis1Varaible, preAxis2Varaible, con, excludeCntMap, tableName);
                }
                preAxis1Varaible = axis1.ColumnName;
                preAxis2Varaible = null;

                if (axis1 != null)
                {
                    axesgroup.Add(axis1.Sectors.Count);
                }
            }

            return table;
        }
        private static bool checkAdjuscentCount(string axis1Varaible, string axis2Varaible, string preAxis1Varaible, string preAxis2Varaible, SQLiteConnection con, Dictionary<string, int> excludeCntMap, string tableName)
        {
            int cntAxis1 = findExcludeCnt(con, excludeCntMap, axis1Varaible, tableName);

            int preCntAxis1 = findExcludeCnt(con, excludeCntMap, preAxis1Varaible, tableName);

            if (cntAxis1 != preCntAxis1) return false;

            int cntPreAxis1Axis1 = findExcludeCnt(con, excludeCntMap, preAxis1Varaible, tableName, axis1Varaible);

            if (cntAxis1 != cntPreAxis1Axis1 || preCntAxis1 != cntPreAxis1Axis1) return false;

            if (axis2Varaible != null && preAxis2Varaible != null)
            {
                int cntAxis2 = findExcludeCnt(con, excludeCntMap, axis2Varaible, tableName);
                int preCntAxis2 = findExcludeCnt(con, excludeCntMap, preAxis2Varaible, tableName);

                if (cntAxis2 != preCntAxis1 || cntAxis2 != preCntAxis2) return false;


                int cntPreAxis1PreAxis2Axis1Axis2 = findExcludeCnt(con, excludeCntMap, preAxis1Varaible, tableName, preAxis2Varaible, axis1Varaible, axis2Varaible);
                if (cntPreAxis1PreAxis2Axis1Axis2 != cntAxis2 || cntPreAxis1PreAxis2Axis1Axis2 != preCntAxis2) return false;

            }
            else if (axis2Varaible != null && preAxis2Varaible == null)
            {
                int cntAxis2 = findExcludeCnt(con, excludeCntMap, axis2Varaible, tableName);
                if (cntAxis2 != preCntAxis1) return false;
                int cntPreAxis1Axis1Axis2 = findExcludeCnt(con, excludeCntMap, preAxis1Varaible, tableName, axis1Varaible, axis2Varaible);
                if (cntPreAxis1Axis1Axis2 != cntAxis2) return false;

            }
            else if (axis2Varaible == null && preAxis2Varaible != null)
            {
                int preCntAxis2 = findExcludeCnt(con, excludeCntMap, preAxis2Varaible, tableName);
                if (preCntAxis2 != cntAxis1) return false;
                int cntPreAxis1PreAxis2Axis1 = findExcludeCnt(con, excludeCntMap, preAxis1Varaible, tableName, preAxis2Varaible, axis1Varaible);
                if (cntPreAxis1PreAxis2Axis1 != preCntAxis2) return false;
            }

            return true;
        }

        private static int findExcludeCnt(SQLiteConnection con, Dictionary<string, int> excludeCntMap, string varaible1, string tableName, string varaible2 = null, string varaible3 = null, string varaible4 = null)
        {

            int cntAxis = 0;
            string axisVar = varaible1;
            string whereCnd = varaible1 + " = '*'";

            if (varaible1 == null)
            {
                return 0;
            }

            if (varaible2 != null)
            {
                axisVar += ":" + varaible2;
                whereCnd += " AND " + varaible2 + " = '*' ";

            }

            if (varaible3 != null)
            {
                axisVar += ":" + varaible3;
                whereCnd += " AND " + varaible3 + " = '*' ";

            }

            if (varaible4 != null)
            {
                axisVar += ":" + varaible4;
                whereCnd += " AND " + varaible4 + " = '*' ";

            }

            if (excludeCntMap.ContainsKey(axisVar))
            {
                cntAxis = excludeCntMap[axisVar];
            }
            else
            {
                try
                {
                    cntAxis = DBHelper.ExecuteScalar("Select count(" + varaible1 + ") from " + tableName + " where " + whereCnd, con);
                }
                catch (Exception exc)
                {
                    if (exc.Message.Contains("no such column") || exc.Message.Contains("no such table")) // If no such column, load null data
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
    }
}
