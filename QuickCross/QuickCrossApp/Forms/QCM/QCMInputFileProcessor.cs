using Microsoft.VisualBasic.FileIO;
using Qc4Launcher.Classes;
using Qc4Launcher.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Forms.QCM
{
    class QCMInputFileProcessor
    {
        #region Qlayout
        public static bool QlayoutParser(string qlayoutFilePath, Encoding encode, string deLimiter, out List<String[]> qlayoutData, out string message)
        {
            try
            {
                //TextFieldParser parsedByLine = TextParser.ParseFile(qlayoutFilePath, encode, deLimiter);
                //qlayoutData = TextParser.SplitBy(parsedByLine, ',');
                qlayoutData = TextParser.ReadFile(qlayoutFilePath, encode, deLimiter);
                if (!QCMValidation.Validate_QlayoutSampleID(qlayoutData, out string msg))
                {
                    message = msg;
                    qlayoutData = null;
                    return false;
                }
                message = "";
                return true;
            }
            catch (Exception ex)
            {
                qlayoutData = null;
                message = QCMHelper.ReturnErrorLabel() + ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException;
                return false;
            }
        }

        public static bool QlayoutDataProcessor(List<string[]> parsedCsv, out List<Util.Qc3Parse.QDataDetail> qDataDetails, out Object[,] qsAry, out string message)
        {
            try
            {
                int numberofCount = 0;
                int index = 0;
                int maxCol = 1038;
                message = "";
                qsAry = new Object[parsedCsv.Count(), maxCol];
                qDataDetails = new List<Util.Qc3Parse.QDataDetail>();
                bool isNew = true;
                int maxChoices = 0;
                int categoryCount = 0;
                int prevQstnOrder = 0;
                List<String> duplicateVariableChecker = new List<String>();
                bool matrixQuestion = false;
                string primary_QuestionNumber = "";
                string primary_QuestionType = "";
                int primary_NumberOfQuestion = 1;
                string primary_VariableName = "";
                string primary_AnswerType = "";
                string primary_NumberofCategory = "";
                string primary_WT = "";
                string primary_Sort = "";
                int matrixQuestionCount = 0;
                //int oneLineChoiceLimit = 200;
                foreach (string[] csvValues in parsedCsv)
                {
                    if (numberofCount > 1)
                    {
                        QCMHelper.SetQstnColumn_WT(0);
                        if (!csvValues[QCMHelper.QlayoutVariables.ColItem].Equals(""))
                        {
                            if (!isNew)
                            {
                                isNew = true;
                                index++;
                            }
                        }
                        else
                        {
                            if (maxChoices == categoryCount)
                                isNew = true;
                            else
                                isNew = false;
                        }
                        if (!isNew)
                        {
                            for (int i = QCMHelper.QlayoutVariables.ColChoices, j = 0; i < csvValues.Count(); i++, j++)
                            {
                                if ((maxChoices + j + 1) <= categoryCount)
                                {
                                    string choice200above = String.IsNullOrEmpty(csvValues[i]) ? "" : csvValues[i];
                                    choice200above = QCMConversionRules.ReConvertProcess(choice200above);
                                    choice200above = QCMConversionRules.ConvertCommaArrow(choice200above);
                                    choice200above = QCMConversionRules.Add_HalfWidthSpace_SingleQutoation(choice200above);
                                    if (choice200above.Length > QCMHelper.TitleQuestionChoice_TextLimit)
                                    {
                                        choice200above = choice200above.Substring(0, QCMHelper.TitleQuestionChoice_TextLimit);
                                    }
                                    qsAry[index, Util.Constants.QS.QsColChoiceBegin - 1 + maxChoices + j] = choice200above;
                                }
                            }
                            maxChoices += csvValues.Count() - QCMHelper.QlayoutVariables.ColChoices;
                            continue;
                        }

                        //------------------------------------------------------------------------------------------------------------------------------------------
                        maxChoices = 0;

                        //set org
                        qsAry[index, Util.Constants.QS.QsColNew - 1] = "Org";

                        //set questionNumber
                        string questionNumber = csvValues[QCMHelper.QlayoutVariables.ColQuestionNumber];
                        //questionNumber = questionNumber.Replace("\"\"", "\"");
                        questionNumber = QCMConversionRules.ReConvertProcess(questionNumber);
                        questionNumber = QCMConversionRules.ConvertCommaArrow(questionNumber);
                        questionNumber = QCMConversionRules.Add_HalfWidthSpace_SingleQutoation(questionNumber);
                        if (questionNumber.Contains("\r\n") || questionNumber.Contains("\t"))
                        {
                            message = LocalResource.QCM_INVALID_QUESTION_NUMBER;
                            return false;
                        }
                        if (questionNumber.Length > QCMHelper.TitleQuestionChoice_TextLimit)
                        {
                            questionNumber = questionNumber.Substring(0, QCMHelper.TitleQuestionChoice_TextLimit);
                        }
                        qsAry[index, Util.Constants.QS.QsColQuestionNumber - 1] = String.IsNullOrEmpty(questionNumber) ? "" : questionNumber;                     

                        //set questionType
                        string questionType = String.IsNullOrEmpty(csvValues[QCMHelper.QlayoutVariables.ColQuestionType]) ? "" : csvValues[QCMHelper.QlayoutVariables.ColQuestionType];     
                        if (!questionType.Equals(""))
                        {
                            questionType = questionType.ToUpper();
                            if (!QCMValidation.CheckQuestionType(questionType))
                            {
                                message = LocalResource.QCM_INVALID_QSTNTYPE;
                                return false;
                            }
                        }
                        qsAry[index, Util.Constants.QS.QsColQuestionType - 1] = questionType;

                        //set numberofQuestion
                        string numberOfQstn = String.IsNullOrEmpty(csvValues[QCMHelper.QlayoutVariables.ColNumberOfQuestion]) ? "" : csvValues[QCMHelper.QlayoutVariables.ColNumberOfQuestion];
                        if (!numberOfQstn.Equals(""))
                        {
                            numberOfQstn = QCMConversionRules.ConvertCommaArrow(numberOfQstn);
                            if (!QCMValidation.ValidateInRange(1,200,numberOfQstn))
                            {
                                message = LocalResource.QCM_INVALID_NUMBROFQSTN;
                                return false;
                            }
                        }
                        qsAry[index, Util.Constants.QS.QsColNumberOfQuestion - 1] = numberOfQstn;
                       
                        //set variable
                        Util.Qc3Parse.QDataDetail qDataDetail;
                        string variableName = String.IsNullOrEmpty(csvValues[QCMHelper.QlayoutVariables.ColItem]) ? "" : csvValues[QCMHelper.QlayoutVariables.ColItem];
                        if (!variableName.Equals(""))
                        {
                            //variableName = variableName.Replace("\"\"", "\"");
                            variableName = QCMConversionRules.ReConvertProcess(variableName);
                            variableName = QCMConversionRules.ConvertCommaArrow(variableName);
                            qDataDetail = new Util.Qc3Parse.QDataDetail(variableName);
                            duplicateVariableChecker.Add(variableName.ToUpper());
                            if (QC4Common.Util.QSUtil.ValidateVariable(qDataDetail.variableName, out string msg)/*QCMConversionRules.ConversionRuleValidate(qDataDetail.variableName)*/)
                            {
                                qsAry[index, Util.Constants.QS.QsColItem - 1] = qDataDetail.variableName;
                            }
                            else
                            {
                                message = msg;
                                return false;
                            }                      
                        }
                        else
                        {
                            message = LocalResource.QC3PARSE_ALERT_CONTAIN_INVALID_VARIABLE1;
                            return false;
                        }

                        //set answerType
                        string answerType = String.IsNullOrEmpty(csvValues[QCMHelper.QlayoutVariables.ColAnswerType]) ? "" : csvValues[QCMHelper.QlayoutVariables.ColAnswerType];
                        if (!answerType.Equals("") && QCMValidation.CheckAnswerType(answerType.ToUpper()))
                        {
                            answerType = answerType.ToUpper();
                            qDataDetail.answerType = answerType;
                            qsAry[index, Util.Constants.QS.QsColAnswerType - 1] = answerType;
                        }
                        else
                        {
                            message = LocalResource.QCM_INVALID_ANSWERTYPE;
                            return false;
                        }

                        if (!QCMValidation.ValidateQuestionTypeAndAnswerType(answerType,questionType))
                        {
                            message = LocalResource.QCM_QSTNTYPE_ANSTYPE_NOTMATCH;
                            return false;
                        }                     

                        //set numberofCategories 
                        categoryCount = 0;
                        string categoryCnt = String.IsNullOrEmpty(csvValues[QCMHelper.QlayoutVariables.ColCategories]) ? "" : csvValues[QCMHelper.QlayoutVariables.ColCategories];
                        if (!categoryCnt.Equals(""))
                        {
                            categoryCnt = QCMConversionRules.ConvertCommaArrow(categoryCnt);
                            if (QCMValidation.ValidateInRange(1,1000,categoryCnt))
                            {
                                qDataDetail.categoryCount = Convert.ToInt32(csvValues[QCMHelper.QlayoutVariables.ColCategories]);
                                categoryCount = qDataDetail.categoryCount;
                            }
                            else
                            {
                                message = LocalResource.QCM_INVALID_NMBROFCATGRY;
                                return false;
                            }
                        }      
                        qsAry[index, Util.Constants.QS.QsColCategories - 1] = categoryCnt;

                        //set score or weight
                        string WT = String.IsNullOrEmpty(csvValues[QCMHelper.QlayoutVariables.ColWT]) ? "" : csvValues[QCMHelper.QlayoutVariables.ColWT];
                        if (!WT.Equals(""))
                        {
                            WT = QCMConversionRules.ConvertCommaArrow(WT);
                            if (answerType.Equals(Util.Constants.AnswerType.SA) || answerType.Equals(Util.Constants.AnswerType.MA))
                            {
                                if (WT.Contains("\""))
                                {
                                    int i = 0;
                                    WT = "";
                                    bool addComma = false;
                                    while (csvValues[QCMHelper.QlayoutVariables.ColWT + i].Contains("\""))
                                    {
                                        if (!WT.Equals("") || addComma)
                                        {
                                            WT += ",";
                                            addComma = false;
                                        }
                                        string value = csvValues[QCMHelper.QlayoutVariables.ColWT + i].Replace("\"", "");
                                        if (!QCMValidation.IsNumeric(value))
                                        {
                                            message = LocalResource.QCM_INVALID_WT;
                                            return false;
                                        }
                                        if (value.Equals(""))
                                        {
                                            addComma = true;
                                        }
                                        WT += value;
                                        i++;
                                    }
                                    QCMHelper.SetQstnColumn_WT(i - 1);

                                    if (i != categoryCount)
                                    {
                                        if (i > categoryCount)
                                        {
                                            message = LocalResource.QCM_INVALID_WT;
                                            return false;
                                        }
                                        while (i < categoryCount)
                                        {
                                            WT += ",";
                                            i++;
                                        }
                                    }
                                }
                                else
                                {
                                    WT = WT.Replace("，", ",");
                                    string[] commaSperated = WT.Split(',');
                                    foreach (string val in commaSperated)
                                    {
                                        if (!QCMValidation.IsNumeric(val))
                                        {
                                            message = LocalResource.QCM_INVALID_WT;
                                            return false;
                                        }
                                    }
                                    if (commaSperated.Count() != categoryCount)
                                    {
                                        int j = commaSperated.Count();
                                        if (j > categoryCount)
                                        {
                                            message = LocalResource.QCM_INVALID_WT;
                                            return false;
                                        }

                                        while (j < categoryCount)
                                        {
                                            WT += ",";
                                            j++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                message = LocalResource.QCM_SET_WT_SAMA;
                                return false;
                            }
                        }
                        qsAry[index, Util.Constants.QS.QsColWT - 1] = WT;

                        //set sort
                        string sort = String.IsNullOrEmpty(csvValues[QCMHelper.QlayoutVariables.ColSortDisplay]) ? "" : csvValues[QCMHelper.QlayoutVariables.ColSortDisplay]; 
                        if (!sort.Equals(""))
                        {
                            sort = QCMConversionRules.ConvertCommaArrow(sort);
                            if (!QCMValidation.ValidateSortValue(answerType, sort,categoryCount))
                            {
                                message = LocalResource.QCM_INVALID_SORT;
                                return false;
                            }
                        }
                        qsAry[index, Util.Constants.QS.QsColSortDisplay - 1] = sort;

                        //set column
                        string questionOrdr = String.IsNullOrEmpty(csvValues[QCMHelper.QlayoutVariables.Colmn]) ? "" : csvValues[QCMHelper.QlayoutVariables.Colmn];
                        questionOrdr = QCMConversionRules.ConvertCommaArrow(questionOrdr);
                        if (questionOrdr.Equals("") || !QCMConversionRules.IsDigitsOnly(questionOrdr))
                        {
                            message = LocalResource.QCM_INVALID_COLUMN;
                            return false;
                        }
               
                        int questionOrder = Convert.ToInt32(csvValues[QCMHelper.QlayoutVariables.Colmn]);            
                        if (questionOrder == 0 || questionOrder != prevQstnOrder + 1)
                        {
                            message =LocalResource.QCM_INVALID_COLUMN;
                            return false;
                        }
                        qDataDetail.questionOrder = questionOrder-1;
                        qDataDetail.isFound = true;
                        prevQstnOrder = questionOrder;

                        //set tableHeading
                        string title = QCMConversionRules.ReConvertProcess(csvValues[QCMHelper.QlayoutVariables.ColTableHeading]);
                        title = QCMConversionRules.ConvertCommaArrow(title);
                        title = QCMConversionRules.Add_HalfWidthSpace_SingleQutoation(title);
                        if (title.Length > QCMHelper.TitleQuestionChoice_TextLimit)
                        {
                            title = title.Substring(0, QCMHelper.TitleQuestionChoice_TextLimit);
                        }
                        qsAry[index, Util.Constants.QS.QsColTableHeading - 1] = String.IsNullOrEmpty(title) ? "" : title;

                        //set Question
                        string questionText = QCMConversionRules.ReConvertProcess(csvValues[QCMHelper.QlayoutVariables.ColQuestion]);
                        questionText = QCMConversionRules.ConvertCommaArrow(questionText);
                        questionText = QCMConversionRules.Add_HalfWidthSpace_SingleQutoation(questionText);
                        if (questionText.Length > QCMHelper.TitleQuestionChoice_TextLimit)
                        {
                            questionText = questionText.Substring(0, QCMHelper.TitleQuestionChoice_TextLimit);
                        }
                        qsAry[index, Util.Constants.QS.QsColQuestion - 1] = String.IsNullOrEmpty(questionText) ? "" : questionText;

                        //set Choices                     
                        if (categoryCount > 0)
                        {
                            if (!QCMValidation.ValidateChoiceAndAnswerType(answerType))
                            {
                                message = LocalResource.QCM_CHOICE_NDFA_NOTACCPTD;
                                return false;
                            }
                        }
                        else
                        {
                            if (!QCMValidation.ValidateZeroChoiceAndAnswerType(answerType))
                            {
                                message = LocalResource.QCM_CHOICE_MNDTRY_SAMA;
                                return false;
                            }
                        }
                        int choiceCountN = csvValues.Count();
                        if ((csvValues.Count() - QCMHelper.QlayoutVariables.ColChoices) > categoryCount)
                        {
                            choiceCountN = categoryCount + QCMHelper.QlayoutVariables.ColChoices;
                            //message = LocalResource.QCM_CHOICE_NMBROFCATGRY_NOTMATCH;
                            //return false;
                        }
                        for (int i = QCMHelper.QlayoutVariables.ColChoices, j = 0; i < choiceCountN; i++, j++)
                        {
                           // string choice = csvValues[i].Replace("<LF>", "\r\n");
                            string choice = QCMConversionRules.ReConvertProcess(csvValues[i]);
                            choice = QCMConversionRules.ConvertCommaArrow(choice);
                            choice = QCMConversionRules.Add_HalfWidthSpace_SingleQutoation(choice);
                            if (choice.Length > QCMHelper.TitleQuestionChoice_TextLimit)
                            {
                                choice = choice.Substring(0, QCMHelper.TitleQuestionChoice_TextLimit);
                            }
                            qsAry[index, Util.Constants.QS.QsColChoiceBegin - 1 + j] = choice;                       
                        }

                        if ((!numberOfQstn.Equals("") || questionType.Equals(Util.Constants.QuestionType.FAS)) && !matrixQuestion)
                        {
                            matrixQuestion = true;
                            primary_QuestionNumber = questionNumber;
                            if (!questionType.Equals(Util.Constants.QuestionType.SAP) && !questionType.Equals(Util.Constants.QuestionType.SAR) && !questionType.Equals(Util.Constants.QuestionType.SAS) && !questionType.Equals(Util.Constants.QuestionType.MAC))
                            {
                                primary_QuestionType = questionType;
                            }
                            else
                            {
                                message = LocalResource.QCM_INVALID_SUBQSTNTYPE_SAPSARMACSAS + questionType;
                                return false;
                            }
                            if (!numberOfQstn.Equals(""))
                            {
                                primary_NumberOfQuestion = Convert.ToInt32(numberOfQstn);
                            }
                            else if(questionType.Equals(Util.Constants.QuestionType.FAS))
                            {
                                primary_NumberOfQuestion = 1;
                            }
                            primary_VariableName = variableName;
                            primary_AnswerType = answerType;
                            primary_NumberofCategory = categoryCnt;
                            primary_WT = WT;
                            primary_Sort = sort;
                            matrixQuestionCount = 1;
                            if (primary_NumberOfQuestion == 1)
                            {
                                matrixQuestion = false;
                            }
                        }
                        else if(matrixQuestion)
                        {
                            if (matrixQuestionCount < primary_NumberOfQuestion)
                            {
                                matrixQuestionCount++;
                                if (matrixQuestionCount == primary_NumberOfQuestion)
                                {
                                    matrixQuestion = false;
                                }
                                if (!questionNumber.Equals("") || !questionType.Equals("") || !numberOfQstn.Equals(""))
                                {
                                    message = LocalResource.QCM_INVALID_SUBQSTN_QNMBR_QTYPE_NMBRQSTN;
                                    return false;
                                }
                                if (!primary_AnswerType.Equals(answerType))
                                {
                                    if (primary_QuestionType.Equals(Util.Constants.QuestionType.FAS))
                                    {
                                        if (answerType.Equals(Util.Constants.AnswerType.N) || answerType.Equals(Util.Constants.AnswerType.FA))
                                        { }
                                        else
                                        {
                                            message = LocalResource.QCM_INVALID_SUBQSTN_ANSTYPE;
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        message = LocalResource.QCM_INVALID_SUBQSTN_ANSTYPE;
                                        return false;
                                    }
                                }
                                if (!primary_NumberofCategory.Equals(categoryCnt))
                                {
                                    message = LocalResource.QCM_INVALID_SUBQSTN_QNMBR_QTYPE_NMBRQSTN;
                                    return false;
                                }
                                if (!primary_WT.Equals(WT))
                                {
                                    message = LocalResource.QCM_INVALID_SUBQSTN_WT;
                                    return false;
                                }
                                if (!primary_Sort.Equals(sort))
                                {
                                    message = LocalResource.QCM_INVALID_SUBQSTN_SORT;
                                    return false;
                                }

                                //string primaryVariable = primary_VariableName.ToUpper().Replace("S1", "");
                                //primaryVariable = primaryVariable.ToUpper().Replace("S" + matrixQuestionCount, "");
                                //string secondaryVariable = variableName.ToUpper().Replace("S" + matrixQuestionCount, "");
                                //secondaryVariable = secondaryVariable.ToUpper().Replace("S1", "");
                                //if (!secondaryVariable.Equals(primaryVariable))
                                //{
                                //    message = "Error in Sub Question";
                                //    return false;
                                //}

                            }
                            else
                            {
                                matrixQuestion = false;
                            }
                        }

                        qDataDetails.Add(qDataDetail);

                        if (!isNew && csvValues[3] != "")
                        {
                            message = LocalResource.QCM_FILE_CORREPTED;
                            return false;
                        }

                        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------

                        if (categoryCount > 200)
                        {
                            if ((maxChoices += csvValues.Count() - QCMHelper.QlayoutVariables.ColChoices) == categoryCount)
                            {
                                isNew = true;
                                ++index;
                            }
                            else
                                isNew = false;
                        }
                        else
                        {
                            isNew = true;
                            ++index;
                        }
                    }
                    else if (numberofCount == 0)
                    {
                        if (csvValues.Count()>=2)
                        {
                            if (!csvValues[0].Equals(""))
                            {
                                if (!QCMConversionRules.IsDigitsOnly(csvValues[0]))
                                {
                                    message = LocalResource.QCM_INVALID_SURVEY_ID;
                                    return false;
                                }
                                QCMHelper.QlayoutVariables.surveyID = csvValues[0];
                            }
                            else
                            {
                                message = LocalResource.QCM_SURVEY_NUMB_MISSING;
                                return false;
                            }
                            if (!csvValues[1].Equals(""))
                            {
                                QCMHelper.QlayoutVariables.surveyName = csvValues[1];
                                if (QCMHelper.QlayoutVariables.surveyName.Length > QCMHelper.TitleQuestionChoice_TextLimit)
                                {
                                    QCMHelper.QlayoutVariables.surveyName = QCMHelper.QlayoutVariables.surveyName.Substring(0, QCMHelper.TitleQuestionChoice_TextLimit);
                                }
                            }
                            else
                            {
                                message = LocalResource.QCM_SURVEY_TITLE_MISSING;
                                return false;
                            }
                        }
                        else
                        {
                            message = LocalResource.QCM_SURVEY_TITLE_MISSING;
                            return false;
                        }                    
                    }
                    ++numberofCount;
                }
                if (!(duplicateVariableChecker.Distinct().Count().Equals(qDataDetails.Count())))
                {
                    message = LocalResource.QCM_DUPLICATE_ITEM_QLAYOUTDATA;
                    return false;
                }

                if (matrixQuestion)
                {
                    message = LocalResource.QCM_INVALID_SUBQSTN_QNMBR_QTYPE_NMBRQSTN;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                qDataDetails = null;
                qsAry = null;
                message = QCMHelper.ReturnErrorLabel() + ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException;
                return false;
            }
        }
        #endregion

        #region Qrawdata

        public static bool QrawdataParser(string tsvFilePath, Encoding encode, string deLimiter, out List<string[]> qrawData, out string message)
        {
            try
            {
                message = "";
                //TextFieldParser parsedByLine = TextParser.ParseFile(tsvFilePath, encode, deLimiter);
                //qrawData = TextParser.SplitBy(parsedByLine, '\t');
                qrawData = TextParser.ReadFile(tsvFilePath, encode, deLimiter);
                if (!QCMValidation.Validate_QrawDataSampleID(qrawData, out message))
                {
                    qrawData = null;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                qrawData = null;
                message = QCMHelper.ReturnErrorLabel() + ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException;
                return false;
            }
        }

        public static bool QrawdataProcessor(List<string[]> qrawData, List<Util.Qc3Parse.QDataDetail> qData, String tempPath, ProgressUpdate progressBarUpdate, double percentageCount, out string message)
        {
            try
            {
                string sql = "insert into answers ( sample_id";
                string values = "  values( @sample_id";
                for (int j = 1; j < qData.Count(); j++)
                {
                    sql += " , q_" + qData[j].questionOrder;
                    values += " , @q_" + qData[j].questionOrder;
                }
                sql += " ) ";
                values += " ) ";

                message = "";
                int i = 0;
                bool skipVariableName = true;
                List<string[]> processedRawdata = new List<string[]>();
                bool dbExecuteForFirstTime = true;
                foreach (string[] value in qrawData)
                {
                    if (!skipVariableName)
                    {
                        for (int j = 0; j < value.Count(); j++)
                        {
                            if (value[j].Contains("<LF>"))
                                value[j] = value[j].Replace("<LF>", "\r\n");
                            if (value[j].Contains("<TAB>"))
                                value[j] = value[j].Replace("<TAB>", "\t");
                            if (qData[j].answerType == Util.Constants.AnswerType.MA)
                                value[j] = String.IsNullOrEmpty(value[j]) ? "" : GenerateMaAnswer(value[j], qData[j].categoryCount);
                            else if (qData[j].answerType == Util.Constants.AnswerType.FA)
                            {
                                value[j] = String.IsNullOrEmpty(value[j]) ? "" : QCMConversionRules.ConvertCommaArrow(value[j]);
                                value[j] = String.IsNullOrEmpty(value[j]) ? "" : QCMConversionRules.Add_HalfWidthSpace_SingleQutoation(value[j])/*ConvertDoubleQuoteFAData(value[j])*/;  
                            }
                            else
                                value[j] = value[j];
                        }
                        processedRawdata.Add(value);
                        if (processedRawdata.Count == 50000)
                        {
                            QCMDatabaseOperations.Insert_AnswerToDb(qData, tempPath, processedRawdata, progressBarUpdate, percentageCount, sql + values, dbExecuteForFirstTime);
                            dbExecuteForFirstTime = false;
                            processedRawdata.Clear();
                        }
                    }
                    skipVariableName = false;
                }
                if (processedRawdata.Count != 0)
                    QCMDatabaseOperations.Insert_AnswerToDb(qData, tempPath, processedRawdata, progressBarUpdate, percentageCount, sql + values, dbExecuteForFirstTime);
                return true;
            }
            catch (Exception ex)
            {
                message = QCMHelper.ReturnErrorLabel() + ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException;
                return false;
            }
        }

        private static string GenerateMaAnswer(string ans, int count)
        {
            if ("*".Equals(ans) || string.Empty.Equals(ans))
            {
                return ans;
            }
            var str = ans.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            char[] cArray = Enumerable.Repeat('0', count).ToArray();
            foreach (string s in str)
            {
                try
                {
                    cArray[count - Convert.ToInt32(s)] = '1';
                }
                catch { }
            }
            return new string(cArray);
        }

        private static string ConvertDoubleQuoteFAData(String value)
        {
            if (value == null)
            {
                return "";
            }
            string data = value;//.Replace("\"\"", "\"");
            if (data.Length > 0 && data[0] == '\'')
                data = " " + data;
            return data;
        }
        #endregion
    }
}
