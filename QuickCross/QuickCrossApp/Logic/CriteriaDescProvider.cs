using System;
using System.Collections.Generic;
using System.Text;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.ExBhv;
using System.Text.RegularExpressions;
using Qc4Launcher.Logic;
using Qc4Launcher.Util;
using Macromill.QCWeb.Question;
using QC4Common.Model;
using Macromill.QCWeb.Tabulation;
using FilterSettingsView;

namespace Macromill.QCWeb.Logic.TabulationEx.Criteria
{

    /// <summary>
    /// 絞込み拡張クラス実装
    /// </summary>
    public class CriteriaDescProvider
    {

        #region クラス変数

        /// <summary>
        /// {0}=[T_ITEM_INFO.ITEM_INFO_ID]
        /// {1}=[=, &lt;&gt;, &lt; , &gt;, &lt;=, &gt;=] 
        /// {2}=[条件値]
        /// </summary>
        private static readonly string DESC_FORMAT1 = "{0}{1}{2}";

        /// <summary>
        /// {0}=[AND, OR]
        /// {1}=[T_ITEM_INFO.ITEM_INFO_ID]
        /// {2}=[=, &lt;&gt;, &lt; , &gt;, &lt;=, &gt;=]
        /// {3}=[条件値]
        /// </summary>
        private static readonly string DESC_FORMAT2 = " {0} {4}{1}{2}{3}";

        #endregion


        public static string CreateCriteriaDescriptions(List<FilterSettingsCr> filters, Question.Questions questions)
        {


            var descriptions = new StringBuilder();
            bool closeGroup = false;
            bool openGroup = false;
            bool openGroupAdded = false;
            for (int i = 0; i < filters.Count; i++)
            {
                FilterSettingsCr criterion = filters[i];
                if (i < filters.Count - 1)
                {
                    FilterSettingsCr criterionNext = filters[i + 1];
                    string conditionTypeNext = criterionNext.conditionType;
                    if (conditionTypeNext == CrossSettingsReader.AND)
                    {
                        openGroup = true;
                        closeGroup = false;
                    }
                    else
                    {
                        closeGroup = true;
                        openGroup = false;
                    }
                }
                else if (openGroupAdded)
                {
                    closeGroup = true;
                }
                QuestionSettings qstnDet = Definiotion.VariableDictionary[criterion.variable];
                Question.Questions.Question itemInfo = (Question.Questions.Question)questions[qstnDet.Id];
                var itemInfoId = itemInfo.ID;
                var operationCode = criterion.operatorType;
                var conditionString = criterion.values;
                bool addDK = false;
                bool dkClose = false;
                if (itemInfo.QCAnswerType == QCAnswerType.FA) {
                    conditionString = Regex.Escape(conditionString); 
                }else{ 
                    conditionString = conditionString.Replace(" ", "");
                }
                if ((itemInfo.QuestionType & QuestionType.FA) != QuestionType.FA)
                {
                    if (conditionString.StartsWith("!") && (operationCode == "=" || operationCode == "!=" || operationCode == "<>"))
                    {
                        if (operationCode == "=")
                        {
                            conditionString = conditionString.TrimStart('!');
                            operationCode = "!=";
                        }
                        else if ((operationCode == "!=" || operationCode == "<>"))
                        {
                            conditionString = conditionString.TrimStart('!');
                            operationCode = "=";
                        }
                        //if ((itemInfo.QuestionType & QuestionType.N) == QuestionType.N){ 
                        //    Regex grpstartregexN = new Regex(@"(?<!\\)\(");
                        //    Regex grpendregexN = new Regex(@"(?<!\\)\)");
                        //    string conditionStringN = grpstartregexN.Replace(conditionString, "");
                        //    conditionStringN = grpendregexN.Replace(conditionStringN, "");
                        //    string[] split =  conditionStringN.Split(new char[] { ',', '/', '-' });
                        //    if(split.Length > 2 || split.Length == 2 && !string.IsNullOrEmpty(split[0]) && !string.IsNullOrEmpty(split[1])){ 
                        //        addDK = true;
                        //    }
                        //}                        
                    }
                    // invalid specification as per req redmineId: 168020
                    //else if (conditionString.StartsWith("!") && operationCode == ">")
                    //{
                    //    conditionString = conditionString.TrimStart('!');
                    //    operationCode = "<=";
                    //    addDK = true;
                    //}
                    //else if (conditionString.StartsWith("!") && operationCode == ">=")
                    //{
                    //    conditionString = conditionString.TrimStart('!');
                    //    operationCode = "<";
                    //    addDK = true;
                    //}
                    //else if (conditionString.StartsWith("!") && operationCode == "<=")
                    //{
                    //    conditionString = conditionString.TrimStart('!');
                    //    operationCode = ">";
                    //    addDK = true;
                    //}
                    //else if (conditionString.StartsWith("!") && operationCode == "<")
                    //{
                    //    conditionString = conditionString.TrimStart('!');
                    //    operationCode = ">=";
                    //    addDK = true;
                    //}
                }
                if ((itemInfo.QuestionType & QuestionType.N) == QuestionType.N)
                {
                    if (conditionString.StartsWith("-"))
                    {
                        conditionString = conditionString.TrimStart('-');
                        if (operationCode == "=")
                        {
                            operationCode = "<=";
                        }
                        else if (operationCode == "<>" || operationCode == "!=")
                        {
                            operationCode = ">";
                            addDK = true;
                        }
                    }
                    else if (conditionString.EndsWith("-"))
                    {
                        conditionString = conditionString.TrimEnd('-');
                        if (operationCode == "=")
                        {
                            operationCode = ">=";
                        }
                        else if (operationCode == "<>" || operationCode == "!=")
                        {
                            operationCode = "<";
                            addDK = true;
                        }
                    }
                }

                Regex grpstartregex = new Regex(@"(?<!\\)\(");
                Regex grpendregex = new Regex(@"(?<!\\)\)");
                conditionString = grpstartregex.Replace(conditionString, "");
                conditionString = grpendregex.Replace(conditionString, "");
                //FAアイテムの場合は条件値をエスケープする。半角スペースの判定の為
                var criteriaDescription = String.Empty;

                if (openGroup && !openGroupAdded)
                {
                    if (i == 0)
                    {
                        criteriaDescription = (addDK ? "((" : "(") + String.Format(DESC_FORMAT1, itemInfoId, operationCode, conditionString);
                    }
                    else
                    {
                        criteriaDescription = String.Format(DESC_FORMAT2, criterion.conditionType, itemInfoId, operationCode, conditionString, addDK ? "((" : "(");
                    }
                    openGroupAdded = true;
                }
                else if (closeGroup && openGroupAdded)
                {
                    if (i == 0)
                    {
                        criteriaDescription = (addDK ? "(" : "") + String.Format(DESC_FORMAT1, itemInfoId, operationCode, conditionString);
                    }
                    else
                    {
                        criteriaDescription = String.Format(DESC_FORMAT2, criterion.conditionType, itemInfoId, operationCode, conditionString, addDK ? "(" : "");
                    }
                    if (!addDK)
                    {
                        criteriaDescription += ")";
                    }
                    else
                    {
                        dkClose = true;
                    }
                    openGroupAdded = false;
                }
                else
                {
                    if (i == 0)
                    {
                        criteriaDescription = (addDK ? "(" : "") + String.Format(DESC_FORMAT1, itemInfoId, operationCode, conditionString);
                    }
                    else
                    {
                        criteriaDescription = String.Format(DESC_FORMAT2, criterion.conditionType, itemInfoId, operationCode, conditionString, addDK ? "(" : "");
                    }
                }

                //if (itemInfo.QCAnswerType == QCAnswerType.N)
                //{
                //    // Nアイテムの場合は条件値から「(」「)」を外す。マイナス値の入力チェックでのエラー回避の為。
                //    Regex grpstartregex = new Regex(@"(?<!\\)\(");
                //    Regex grpendregex = new Regex(@"(?<!\\)\)");
                //    criteriaDescription = grpstartregex.Replace(criteriaDescription, "");
                //    criteriaDescription = grpendregex.Replace(criteriaDescription, "");
                //}

                descriptions.Append(criteriaDescription);
                if (addDK)
                {
                    criteriaDescription = String.Format(DESC_FORMAT2, CrossSettingsReader.OR, itemInfoId, "=", "DK", "");
                    descriptions.Append(criteriaDescription);
                    criteriaDescription = String.Format(DESC_FORMAT2, CrossSettingsReader.OR, itemInfoId, "=", "*", "");
                    criteriaDescription += dkClose ? "))" : ")";
                    descriptions.Append(criteriaDescription);

                }
            }

            return descriptions.ToString();

        }

        public static string CreateCriteriaDescriptionsForLocalExp(List<FilterSettingsCr> filters, Question.Questions questions)
        {
            var descriptions = new StringBuilder();

            for (int i = 0; i < filters.Count; i++)
            {
                FilterSettingsCr criterion = filters[i];
                QuestionSettings qstnDet = Definiotion.VariableDictionary[criterion.variable];
                Question.Questions.Question itemInfo = (Question.Questions.Question)questions[qstnDet.Id];
                var itemInfoId = itemInfo.ID;
                var operationCode = criterion.operatorType;
                var conditionString = criterion.values;
                //if (conditionString.StartsWith("!"))
                //{
                //    conditionString = conditionString.TrimStart('!');
                //    operationCode = "!=";
                //}

                //FAアイテムの場合は条件値をエスケープする。半角スペースの判定の為
                if (itemInfo.QCAnswerType == QCAnswerType.FA) {
                    conditionString = Regex.Escape(conditionString);
                }
                else{ 
                    conditionString = conditionString.Replace(" ", "");
                }
                var criteriaDescription = String.Empty;
                if (i == 0)
                {
                    criteriaDescription = String.Format(DESC_FORMAT1, itemInfoId, operationCode, conditionString);
                }
                else
                {
                    criteriaDescription = String.Format(DESC_FORMAT2, criterion.conditionType, itemInfoId, operationCode, conditionString, "");
                }

                //if (itemInfo.QCAnswerType == QCAnswerType.N)
                //{
                    // Nアイテムの場合は条件値から「(」「)」を外す。マイナス値の入力チェックでのエラー回避の為。
                   // Regex grpstartregex = new Regex(@"(?<!\\)\(");
                    //Regex grpendregex = new Regex(@"(?<!\\)\)");
                   // criteriaDescription = grpstartregex.Replace(criteriaDescription, "");
                   // criteriaDescription = grpendregex.Replace(criteriaDescription, "");
               // }

                descriptions.Append(criteriaDescription);
            }

            return descriptions.ToString();

        }


        public static string CreateCriteriaDescriptions(FilterSettingsCr criterion, Question.Questions questions)
        {


            var descriptions = new StringBuilder();

            QuestionSettings qstnDet = Definiotion.VariableDictionary[criterion.variable];
            Question.Questions.Question itemInfo = (Question.Questions.Question)questions[qstnDet.Id];
            var itemInfoId = itemInfo.ID;
            var operationCode = criterion.operatorType;
            var conditionString = criterion.values;
            if (itemInfo.QCAnswerType == QCAnswerType.FA) {
                conditionString = Regex.Escape(conditionString); 
            }else{ 
                conditionString = conditionString.Replace(" ", "");
            }
            if (conditionString.StartsWith("=") || conditionString.StartsWith("!") || conditionString.StartsWith("<>"))
            {
                if (conditionString.StartsWith("<>"))
                {
                    conditionString = conditionString.Substring("<>".Length);
                    operationCode = "!=";
                }
                else if (conditionString.StartsWith("!"))
                {
                    conditionString = conditionString.TrimStart('!');
                    operationCode = "!=";
                }
                else 
                {
                    conditionString = conditionString.Substring(1);
                }

                if (conditionString.StartsWith("<>") || conditionString.StartsWith("!"))
                {
                    if (conditionString.StartsWith("<>"))
                    {
                        conditionString = conditionString.Substring("<>".Length);
                        conditionString = "!" + conditionString;
                    }
                    int[] criteriaSectorList;
                    GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
                                                QuestionType.SA, conditionString, out criteriaSectorList, qstnDet.CategoryCount);
                    if (criteriaSectorList == null || criteriaSectorList.Length < 1) { 
                        conditionString =  Convert.ToString(qstnDet.CategoryCount + 1); // hack for no category 
                    }else{
                        conditionString = string.Join(",", criteriaSectorList); 
                    }
                }
            }

            //if (conditionString.StartsWith("=") || conditionString.StartsWith("!") || conditionString.StartsWith("<>"))
            //{
            //    int opCntr = 0;

            //    if (conditionString.StartsWith("<>"))
            //    {
            //        conditionString = conditionString.Substring("<>".Length);
            //        opCntr++;
            //    }
            //    if (conditionString.StartsWith("!"))
            //    {
            //        conditionString = conditionString.TrimStart('!');
            //        opCntr++;
            //    }
            //    operationCode = opCntr % 2 == 0 ? "=" : "!=";
            //}


            if (itemInfo.QCAnswerType == QCAnswerType.FA) {
                conditionString = Regex.Escape(conditionString);
             }else{ 
                conditionString = conditionString.Trim();
             }
            var criteriaDescription = String.Empty;
            criteriaDescription = String.Format(DESC_FORMAT1, itemInfoId, operationCode, conditionString);
            if (itemInfo.QCAnswerType == QCAnswerType.N)
            {
                Regex grpstartregex = new Regex(@"(?<!\\)\(");
                Regex grpendregex = new Regex(@"(?<!\\)\)");
                criteriaDescription = grpstartregex.Replace(criteriaDescription, "");
                criteriaDescription = grpendregex.Replace(criteriaDescription, "");
            }

            descriptions.Append(criteriaDescription);
            return descriptions.ToString();

        }



        public static string LocalizeFilteringExpression(string filter, Macromill.QCWeb.ReportRequest.Request ParentRequest, Questions questions, bool isNarrow = false)
        {
            // リソースを使って、絞込み条件式を文章化する処理
            if (string.IsNullOrWhiteSpace(filter)) return null;
            string lccd = ParentRequest.LocationCode;
            if (string.IsNullOrWhiteSpace(lccd)) lccd = "ja";
            Tabulation.Criteria criteria = new Tabulation.Criteria(filter, "", questions, LocalizeFilteringExpression:true);
            List<Tabulation.ICriteria> criterias = criteria.SubCriterias;
            if (criterias == null || criterias.Count == 0)
            {
                criterias = new List<Tabulation.ICriteria>();
                criterias.Add(criteria);
            }
            string[] resultBuf = new string[criterias.Count];
            //QueryItemName query = new QueryItemName();
            // {0}の値
            string itemValueFormat = Qc4Launcher.LocalResource.REPORT_CRITERION_ITEM_VALUE_FORMAT;
            // {0}以上{1}以下
            string betweenNumberFormat = Qc4Launcher.LocalResource.REPORT_CRITERION_BETWEEN_NUMBER_FORMAT;
            // {0}が{1}{2}
            string criterionFormat = Qc4Launcher.LocalResource.REPORT_CRITERION_FORMAT;
            for (int i = 0; i < criterias.Count; ++i)
            {
                Tabulation.Criteria tmpCriteria = criterias[i] as Tabulation.Criteria;
                Tabulation.QuestionType qType = tmpCriteria.QuestionType;
                qType &= Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.N | Tabulation.QuestionType.FA;
                if (!Enum.IsDefined(typeof(Tabulation.QuestionType), qType))
                {
                    // 不正な指定
                    return null;
                }
                string ope = tmpCriteria.CriteriaOperatorDescription + string.Empty;
                switch (ope)
                {
                    case "=":
                    case "<>":
                    case ">":
                    case "<":
                    case ">=":
                    case "<=":
                        break;
                    case "!=":
                        ope = "<>";
                        break;
                    default:
                        // 不正な指定
                        return null;
                }
                string addChoice = "";
                QuestionSettings qstnDet = Definiotion.VariableDictionary[tmpCriteria.Question.Name];
                int numberOfOperator = criterias.Count;
                string qName = isNarrow ? tmpCriteria.Question.Name3 : tmpCriteria.Question.Name2;
                int catCnt = tmpCriteria.Question.Sectors == null ? 0 : tmpCriteria.Question.Sectors.Count;
                qName = string.Format(itemValueFormat, qName);
                string value = tmpCriteria.CriteriaValueDescription + string.Empty;
                Tabulation.QuestionType allowQType = (Tabulation.QuestionType)0;
                switch (qType)
                {
                    case Tabulation.QuestionType.SA:
                    case Tabulation.QuestionType.N:
                    case Tabulation.QuestionType.FA:
                        allowQType = Tabulation.QuestionType.SA | Tabulation.QuestionType.N | Tabulation.QuestionType.FA;
                        break;
                    case Tabulation.QuestionType.MA:
                        allowQType = Tabulation.QuestionType.MA;
                        break;
                }
                //decimal CriteriaQID = query.QuestionNameToID(ParentRequest.QCWebID, allowQType, value, ScenarioId);
                decimal CriteriaQID = 0; // TO DO
                if (CriteriaQID == (decimal)0)
                {
                    List<string> values = new List<string>();
                    Tabulation.DataType criteriaDataType = Tabulation.DataType.NormalData;
                    bool isSingle = true;
                    bool isRange = false;
                    switch (qType)
                    {
                        case Tabulation.QuestionType.SA:
                        case Tabulation.QuestionType.MA:
                            {
                                //int[] criteriaValueList = null;
                                //criteriaDataType = Tabulation.GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
                                //                                            qType, value, out criteriaValueList, catCnt);
                                //if (criteriaDataType == Tabulation.DataType.NormalData)
                                //{
                                //    int s = 0, e = 0;
                                //    isSingle = criteriaValueList != null && criteriaValueList.Length == 1;
                                //    for (int j = 0; j < (criteriaValueList == null ? 0 : criteriaValueList.Length); ++j)
                                //    {
                                //        if (j == 0 || criteriaValueList[j] != criteriaValueList[j - 1] + 1)
                                //        {
                                //            s = criteriaValueList[j];
                                //        }
                                //        if (j == criteriaValueList.Length - 1 || criteriaValueList[j + 1] != criteriaValueList[j] + 1)
                                //        {
                                //            e = criteriaValueList[j];
                                //            if (s == e)
                                //            {
                                //                values.Add(s.ToString());
                                //            }
                                //            else
                                //            {
                                //                values.Add(s.ToString() + "-" + e.ToString());
                                //            }
                                //        }
                                //    }
                                //}
                                values.Add(value);
                                break;
                            }
                        case Tabulation.QuestionType.N:
                            {
                                //Tabulation.NData.ValueRange[] criteriaValueList = null;
                                //criteriaDataType = Tabulation.GlobalTabulation.CriteriaValueDescriptionToValueList<Tabulation.NData.ValueRange>(
                                //                                             qType, value, out criteriaValueList, catCnt);
                                //if (criteriaDataType == Tabulation.DataType.NormalData)
                                //{
                                //    isSingle = criteriaValueList != null && criteriaValueList.Length == 1;
                                //    for (int j = 0; j < (criteriaValueList == null ? 0 : criteriaValueList.Length); ++j)
                                //    {
                                //        double min = criteriaValueList[j].MinValue;
                                //        double max = criteriaValueList[j].MaxValue;
                                //        if (min == max)
                                //        {
                                //            values.Add(min.ToString());
                                //        }
                                //        else
                                //        {
                                //            values.Add(string.Format(betweenNumberFormat, min.ToString(), max.ToString()));
                                //            isRange = true;
                                //        }
                                //    }
                                //}
                                values.Add(value);
                                break;
                            }
                        case Tabulation.QuestionType.FA:
                            {
                                if (value.Length == 0 || value.Equals("DK"))
                                {
                                    criteriaDataType = Tabulation.DataType.NAData;
                                }
                                else
                                {
                                    values.Add("\"" + Regex.Unescape(value) + "\"");
                                }
                                break;
                            }
                    }
                    if (criteriaDataType != Tabulation.DataType.NormalData)
                    {
                        if ((criteriaDataType & Tabulation.DataType.NAData) == Tabulation.DataType.NAData)
                        {
                            // 無回答
                            values.Add(Qc4Launcher.LocalResource.REPORT_NA_DESCRIPTION_KEYWORD);
                        }
                        if ((criteriaDataType & Tabulation.DataType.IVData) == Tabulation.DataType.IVData)
                        {
                            // 非該当
                            values.Add(Qc4Launcher.LocalResource.REPORT_IV_DESCRIPTION_KEYWORD);
                        }
                        isSingle = values.Count == 1;
                    }
                    if (values.Count == 0)
                    {
                        // 不正な指定
                        return null;
                    }
                    value = string.Join("/", values.ToArray());
                    switch (ope)
                    {
                        case "=":
                            if (isSingle)
                            {
                                if (criteriaDataType == Tabulation.DataType.NormalData)
                                {
                                    switch (qType)
                                    {
                                        case Tabulation.QuestionType.SA:
                                            ope = IsNegationNotationChangeRequired(value) ? Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD : Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_KEYWORD;
                                            addChoice = IsAddChoiceValueRequired(value, "=", qstnDet, out value, numberOfOperator);
                                            break;
                                        case Tabulation.QuestionType.N:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_KEYWORD;
                                            break;
                                        case Tabulation.QuestionType.MA:
                                            if (value.Equals("DK") || value.Equals("*"))
                                                ope = Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_KEYWORD;
                                            else if (value.Equals("!DK") || value.Equals("!*") || value.Equals("<>DK") || value.Equals("<>*"))
                                                ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD;
                                            else
                                                ope = IsNegationNotationChangeRequired(value) ? Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_INCLUDE_KEYWORD : Qc4Launcher.LocalResource.REPORT_CRITERION_INCLUDE_KEYWORD;
                                            addChoice = IsAddChoiceValueRequired(value, "=", qstnDet, out value, numberOfOperator);
                                            break;
                                        case Tabulation.QuestionType.FA:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_MATCH_KEYWORD;
                                            break;
                                    }
                                }
                                else
                                {
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_KEYWORD;
                                }
                            }
                            else
                            {
                                if (criteriaDataType == Tabulation.DataType.NormalData)
                                {
                                    switch (qType)
                                    {
                                        case Tabulation.QuestionType.SA:
                                        case Tabulation.QuestionType.N:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_ANYONE_KEYWORD;
                                            break;
                                        case Tabulation.QuestionType.MA:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_INCLUDE_ANYONE_KEYWORD;
                                            break;
                                    }
                                }
                                else
                                {
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_ANYONE_KEYWORD;
                                }
                            }
                            break;
                        case "<>":
                            if (isSingle)
                            {
                                if (criteriaDataType == Tabulation.DataType.NormalData)
                                {
                                    switch (qType)
                                    {
                                        case Tabulation.QuestionType.SA:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD;
                                            addChoice = IsAddChoiceValueRequired(value, "<>", qstnDet,out value, numberOfOperator);
                                            break;
                                        case Tabulation.QuestionType.N:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD;
                                            break;
                                        case Tabulation.QuestionType.MA:
                                            ope = (value.Equals("*") || value.Equals("DK")) ? Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD : Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_INCLUDE_KEYWORD;
                                            addChoice = IsAddChoiceValueRequired(value, "<>", qstnDet,out value, numberOfOperator);
                                            break;
                                        case Tabulation.QuestionType.FA:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_UNMATCH_KEYWORD;
                                            break;
                                    }
                                }
                                else
                                {
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD;
                                }
                            }
                            else
                            {
                                if (criteriaDataType == Tabulation.DataType.NormalData)
                                {
                                    switch (qType)
                                    {
                                        case Tabulation.QuestionType.SA:
                                        case Tabulation.QuestionType.N:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_ANYONE_KEYWORD;
                                            break;
                                        case Tabulation.QuestionType.MA:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_INCLUDE_ANYONE_KEYWORD;
                                            break;
                                    }
                                }
                                else
                                {
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_ANYONE_KEYWORD;
                                }
                            }
                            break;
                        default:
                            switch (qType)
                            {
                                case Tabulation.QuestionType.MA:
                                case Tabulation.QuestionType.FA:
                                    // 不正な指定
                                    return null;
                            }
                            if (criteriaDataType != Tabulation.DataType.NormalData)
                            {
                                // 不正な指定
                                return null;
                            }
                            if (!isSingle || isRange)
                            {
                                // 不正な指定
                                return null;
                            }
                            switch (ope)
                            {
                                case ">":
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_GREATER_KEYWORD;
                                    break;
                                case "<":
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_LESS_KEYWORD;
                                    break;
                                case ">=":
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_GREATER_EQUAL_KEYWORD;
                                    break;
                                case "<=":
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_LESS_EQUAL_KEYWORD;
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    Question.Questions tmpQs = new Question.Questions(0, CriteriaQID);
                    Question.Questions.Question criteriaQ = tmpQs[CriteriaQID] as Question.Questions.Question;
                    string qName2 = isNarrow ? criteriaQ.Name : criteriaQ.Name2;
                    value = string.Format(itemValueFormat, qName2);
                    switch (ope)
                    {
                        case "=":
                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_MATCH_KEYWORD;
                            break;
                        case "<>":
                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_UNMATCH_KEYWORD;
                            break;
                        default:
                            switch (qType)
                            {
                                case Tabulation.QuestionType.SA:
                                case Tabulation.QuestionType.N:
                                    switch (ope)
                                    {
                                        case ">":
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_GREATER_KEYWORD;
                                            break;
                                        case "<":
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_LESS_KEYWORD;
                                            break;
                                        case ">=":
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_GREATER_EQUAL_KEYWORD;
                                            break;
                                        case "<=":
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_LESS_EQUAL_KEYWORD;
                                            break;
                                    }
                                    break;
                                case Tabulation.QuestionType.MA:
                                case Tabulation.QuestionType.FA:
                                    // 不正な指定
                                    return null;
                            }
                            break;
                    }
                }
                if (i > 0)
                {
                    if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                    {
                        resultBuf[i - 1] += tmpCriteria.Operator == Tabulation.Operator.opAnd
                                                ? " " + Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_AND_KEYWORD + " "
                                                : " " + Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_OR_KEYWORD + " ";
                    }
                    else
                    {
                        resultBuf[i - 1] += tmpCriteria.Operator == Tabulation.Operator.opAnd
                                                ? Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_AND_KEYWORD
                                                : Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_OR_KEYWORD;
                    }
                }
                if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                    resultBuf[i] = string.Format(criterionFormat.Replace("is",ope), qName, value,addChoice);
                else
                    resultBuf[i] = string.Format(criterionFormat, qName, value + addChoice, ope);
            }
            return string.Join("\n", resultBuf);
        }

        public static string IsAddChoiceValueRequired(string value, string operatr, QuestionSettings qstnDet,out string newValue, int numberOfOperator = 0)
        {
            string choiceVal = "";
            newValue = value;
            if (numberOfOperator == 1)
            {
                if (operatr.Equals("=") || operatr.Equals("<>"))
                {
                    if (int.TryParse(value, out int result))
                        choiceVal = (result > 0 && (result - 1) < qstnDet.Choices.Count) ? "[" + qstnDet.Choices[result - 1] + "]" : "";
                    else if (value.Contains("!"))
                    {
                        string numbr = value.Replace("!", "");
                        if (int.TryParse(numbr, out result))
                        {
                            choiceVal = (result > 0 && (result - 1) < qstnDet.Choices.Count) ? "[" + qstnDet.Choices[result - 1] + "]" : "";
                            newValue = numbr;
                        }
                        else if (numbr.Equals("DK") || numbr.Equals("*"))
                        {
                            newValue = numbr;
                        }
                    }
                    else if (value.Contains("<>"))
                    {
                        string numbr = value.Replace("<>", "");
                        if (int.TryParse(numbr, out result))
                        {
                            choiceVal = (result > 0 && (result - 1) < qstnDet.Choices.Count) ? "[" + qstnDet.Choices[result - 1] + "]" : "";
                            newValue = numbr;
                        }
                        else if (numbr.Equals("DK") || numbr.Equals("*"))
                        {
                            newValue = numbr;
                        }
                    }
                }
            }
            else
            {
                if (IsNegationNotationChangeRequired(value))
                {
                    if (value.Contains("!"))
                    {
                        string numbr = value.Replace("!", "");
                        if (int.TryParse(numbr, out int result))
                        {
                            newValue = numbr;
                        }
                    }
                }
            }
            return choiceVal;
        }

        public static bool IsNegationNotationChangeRequired(string value)
        {
            if (!String.IsNullOrEmpty(value) && value.Contains("!"))
            {
                string numbr = value.Replace("!", "");
                if (int.TryParse(numbr, out int result))
                {
                    return true;
                }
                else if(numbr.Equals("DK") || numbr.Equals("*"))
                {
                    return true;
                }
            }
            else if (!String.IsNullOrEmpty(value) && value.Contains("<>"))
            {
                string numbr = value.Replace("<>", "");
                if (int.TryParse(numbr, out int result))
                {
                    return true;
                }
                else if (numbr.Equals("DK") || numbr.Equals("*"))
                {
                    return true;
                }
            }
            return false;
        }

        public static string Edit_SiborikomiAll_General(System.Collections.Generic.List<FilterSettingsCr> filters, Questions questions)
        {
            string EditString = "";
            foreach (FilterSettingsCr filter in filters)
            {
                EditString = Edit_SiborikomiAll_General(filter.variable, filter.operatorType, filter.values, filter.conditionType, questions, EditString, filters.Count);
            }
            return EditString;
        }

        public static string Edit_SiborikomiAll_General(string variable, string operatorType, string val, string conditionType, Questions questions, string EditString, int numberOfOperator = 0)
        {
            if (numberOfOperator == 0 && EditString.Equals("") && !conditionType.Equals("AND") && !conditionType.Equals("OR"))
                numberOfOperator = 1;
            QuestionSettings qstnDet = Qc4Launcher.Util.Definiotion.VariableDictionary[variable];
            string filter = qstnDet.Id + operatorType + val;
            Tabulation.Criteria criteria = new Tabulation.Criteria(filter, "", questions, LocalizeFilteringExpression: true);
            List<ICriteria> criterias = criteria.SubCriterias;
            if (criterias == null || criterias.Count == 0)
            {
                criterias = new List<ICriteria>();
                criterias.Add(criteria);
            }
            string resultBuf = "";
            //QueryItemName query = new QueryItemName();
            // {0}の値
            string itemValueFormat = Qc4Launcher.LocalResource.REPORT_CRITERION_ITEM_VALUE_FORMAT;
            // {0}以上{1}以下
            string betweenNumberFormat = Qc4Launcher.LocalResource.REPORT_CRITERION_BETWEEN_NUMBER_FORMAT;
            // {0}が{1}{2}
            string criterionFormat = Qc4Launcher.LocalResource.REPORT_CRITERION_FORMAT;
            for (int i = 0; i < criterias.Count; ++i)
            {
                Tabulation.Criteria tmpCriteria = criterias[i] as Tabulation.Criteria;
                QuestionType qType = tmpCriteria.QuestionType;
                qType &= QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA;
                if (!Enum.IsDefined(typeof(QuestionType), qType))
                {
                    // 不正な指定
                    return null;
                }
                string ope = tmpCriteria.CriteriaOperatorDescription + string.Empty;
                switch (ope)
                {
                    case "=":
                    case "<>":
                    case ">":
                    case "<":
                    case ">=":
                    case "<=":
                        break;
                    case "!=":
                        ope = "<>";
                        break;
                    default:
                        // 不正な指定
                        return null;
                }
                string addChoice = "";
                string qName = true ? tmpCriteria.Question.Name3 : tmpCriteria.Question.Name2;
                int catCnt = tmpCriteria.Question.Sectors == null ? 0 : tmpCriteria.Question.Sectors.Count;
                qName = string.Format(itemValueFormat, qName);
                string value = tmpCriteria.CriteriaValueDescription + string.Empty;
                QuestionType allowQType = (QuestionType)0;
                switch (qType)
                {
                    case QuestionType.SA:
                    case QuestionType.N:
                    case QuestionType.FA:
                        allowQType = QuestionType.SA | QuestionType.N | QuestionType.FA;
                        break;
                    case QuestionType.MA:
                        allowQType = QuestionType.MA;
                        break;
                }
                //decimal CriteriaQID = query.QuestionNameToID(ParentRequest.QCWebID, allowQType, value, ScenarioId);
                decimal CriteriaQID = 0; // TO DO
                if (CriteriaQID == (decimal)0)
                {
                    List<string> values = new List<string>();
                    DataType criteriaDataType = DataType.NormalData;
                    bool isSingle = true;
                    bool isRange = false;
                    switch (qType)
                    {
                        case QuestionType.SA:
                        case QuestionType.MA:
                            {
                                values.Add(value);
                                break;
                            }
                        case QuestionType.N:
                            {
                                values.Add(value);
                                break;
                            }
                        case QuestionType.FA:
                            {
                                if (value.Length == 0 || value.Equals("DK"))
                                {
                                    criteriaDataType = DataType.NAData;
                                }
                                else
                                {
                                    values.Add("\"" + Regex.Unescape(value) + "\"");
                                }
                                break;
                            }
                    }
                    if (criteriaDataType != DataType.NormalData)
                    {
                        if ((criteriaDataType & DataType.NAData) == DataType.NAData)
                        {
                            // 無回答
                            values.Add(Qc4Launcher.LocalResource.REPORT_NA_DESCRIPTION_KEYWORD);
                        }
                        if ((criteriaDataType & DataType.IVData) == DataType.IVData)
                        {
                            // 非該当
                            values.Add(Qc4Launcher.LocalResource.REPORT_IV_DESCRIPTION_KEYWORD);
                        }
                        isSingle = values.Count == 1;
                    }
                    if (values.Count == 0)
                    {
                        // 不正な指定
                        return null;
                    }
                    value = string.Join("/", values.ToArray());
                    switch (ope)
                    {
                        case "=":
                            if (isSingle)
                            {
                                if (criteriaDataType == DataType.NormalData)
                                {
                                    switch (qType)
                                    {
                                        case QuestionType.SA:
                                            ope = IsNegationNotationChangeRequired(value) ? Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD : Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_KEYWORD;
                                            addChoice = IsAddChoiceValueRequired(value, "=", qstnDet, out value, numberOfOperator);
                                            break;
                                        case QuestionType.N:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_KEYWORD;
                                            break;
                                        case QuestionType.MA:
                                            if (value.Equals("DK") || value.Equals("*"))
                                                ope = Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_KEYWORD;
                                            else
                                                ope = IsNegationNotationChangeRequired(value) ? Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_INCLUDE_KEYWORD : Qc4Launcher.LocalResource.REPORT_CRITERION_INCLUDE_KEYWORD;
                                            addChoice = IsAddChoiceValueRequired(value, "=", qstnDet, out value, numberOfOperator);
                                            break;
                                        case QuestionType.FA:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_MATCH_KEYWORD;
                                            break;
                                    }
                                }
                                else
                                {
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_KEYWORD;
                                }
                            }
                            else
                            {
                                if (criteriaDataType == DataType.NormalData)
                                {
                                    switch (qType)
                                    {
                                        case QuestionType.SA:
                                        case QuestionType.N:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_ANYONE_KEYWORD;
                                            break;
                                        case QuestionType.MA:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_INCLUDE_ANYONE_KEYWORD;
                                            break;
                                    }
                                }
                                else
                                {
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_EQUAL_ANYONE_KEYWORD;
                                }
                            }
                            break;
                        case "<>":
                            if (isSingle)
                            {
                                if (criteriaDataType == DataType.NormalData)
                                {
                                    switch (qType)
                                    {
                                        case QuestionType.SA:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD;
                                            addChoice = IsAddChoiceValueRequired(value, "<>", qstnDet, out value, numberOfOperator);
                                            break;
                                        case QuestionType.N:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD;
                                            break;
                                        case QuestionType.MA:
                                            ope = (value.Equals("*") || value.Equals("DK")) ? Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD : Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_INCLUDE_KEYWORD;
                                            addChoice = IsAddChoiceValueRequired(value, "<>", qstnDet, out value, numberOfOperator);
                                            break;
                                        case QuestionType.FA:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_UNMATCH_KEYWORD;
                                            break;
                                    }
                                }
                                else
                                {
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_KEYWORD;
                                }
                            }
                            else
                            {
                                if (criteriaDataType == DataType.NormalData)
                                {
                                    switch (qType)
                                    {
                                        case QuestionType.SA:
                                        case QuestionType.N:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_ANYONE_KEYWORD;
                                            break;
                                        case QuestionType.MA:
                                            ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_INCLUDE_ANYONE_KEYWORD;
                                            break;
                                    }
                                }
                                else
                                {
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_NOT_EQUAL_ANYONE_KEYWORD;
                                }
                            }
                            break;
                        default:
                            switch (qType)
                            {
                                case QuestionType.MA:
                                case QuestionType.FA:
                                    // 不正な指定
                                    return null;
                            }
                            if (criteriaDataType != DataType.NormalData)
                            {
                                // 不正な指定
                                return null;
                            }
                            if (!isSingle || isRange)
                            {
                                // 不正な指定
                                return null;
                            }
                            switch (ope)
                            {
                                case ">":
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_GREATER_KEYWORD;
                                    break;
                                case "<":
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_LESS_KEYWORD;
                                    break;
                                case ">=":
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_GREATER_EQUAL_KEYWORD;
                                    break;
                                case "<=":
                                    ope = Qc4Launcher.LocalResource.REPORT_CRITERION_LESS_EQUAL_KEYWORD;
                                    break;
                            }
                            break;
                    }
                }

                if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                    resultBuf = string.Format(criterionFormat.Replace("is", ope), qName, value, addChoice);
                else
                    resultBuf = string.Format(criterionFormat, qName, value + addChoice, ope);
                if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                {
                    if (conditionType == "AND")
                        resultBuf += " " + Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_AND_KEYWORD + "\n";
                    else if (conditionType == "&")
                        resultBuf = " " + Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_AND_KEYWORD + "\n" + resultBuf;
                    else if (conditionType == "OR")
                        resultBuf += " " + Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_OR_KEYWORD + "\n";
                    else if (conditionType == "|")
                        resultBuf = " " + Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_OR_KEYWORD + "\n" + resultBuf;
                }
                else
                {
                    if (conditionType == "AND")
                        resultBuf += Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_AND_KEYWORD + "\n";
                    else if (conditionType == "&")
                        resultBuf = Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_AND_KEYWORD + "\n" + resultBuf;
                    else if (conditionType == "OR")
                        resultBuf += Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_OR_KEYWORD + "\n";
                    else if (conditionType == "|")
                        resultBuf = Qc4Launcher.LocalResource.REPORT_CRITERION_OPERATOR_OR_KEYWORD + "\n" + resultBuf;
                }

                EditString += resultBuf;
            }
            return EditString;
        }
    }
}
