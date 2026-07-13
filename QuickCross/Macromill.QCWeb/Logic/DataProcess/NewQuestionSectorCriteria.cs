using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Tabulation;

namespace Macromill.QCWeb.DataProcess
{
    /// <summary>
    /// 選択肢または仮想選択肢の条件を表すクラス
    /// </summary>
    public class NewQuestionSectorCriteria : Tabulation.Criteria, INewQuestionSectorCriteria
    {
        private NewQuestionSectors._NewQuestionSector parentSector = null;

        internal NewQuestionSectorCriteria(NewQuestionSectors._NewQuestionSector parentsector, string criteriadescription)
            : base(criteriadescription, parentsector.ParentDataProcess.ParentCollection.DataDirectoryPath)
        {
            parentSector = parentsector;
        }

        internal NewQuestionSectorCriteria(string criteriadescription, string dataDirectoryPath, Operator ope, NewQuestionSectors._NewQuestionSector parentsector)
            : base(criteriadescription, dataDirectoryPath, ope, null)
        {
            parentSector = parentsector;
        }


        /// <summary>
        /// ローデータのデータタイプを取得するメソッド
        /// RECODE、CLASSの場合に利用する
        /// </summary>
        /// <param name="index">データ(レコード)のインデックス</param>
        /// <returns>データ種別列挙型</returns>
        public DataType GetDataType(int index)
        {
            decimal itemid = QuestionIDforDP;
            if (itemid == (decimal)0)
            {
                itemid = GetQuestionIDforDP(parentSector.Criteria.SubCriterias);
            }
            Data data = GetData(parentSector, itemid, index);
            return data.DataType;
        }

        /// <summary>
        /// RECODE、CLASSの場合に利用する
        /// </summary>
        /// <param name="subCriterias"></param>
        /// <returns></returns>
        private decimal GetQuestionIDforDP(List<ICriteria> subCriterias)
        {
            var itemid = (decimal)0;
            if (subCriterias != null && subCriterias.Count > 0)
            {
                var sub = subCriterias[0] as Criteria;
                if (sub != null)
                {
                    itemid = sub.QuestionIDforDP;
                    if (itemid == (decimal)0)
                    {
                        itemid = GetQuestionIDforDP(sub.SubCriterias);
                    }
                }
            }
            return itemid;
        }

        /// <summary>
        /// データが条件を満たすかどうかを返すメソッド
        /// </summary>
        /// <param name="index">データ(レコード)のインデックス</param>
        /// <returns><paramref name="index"/>番目のデータが条件を満たす場合true、満たさない場合false</returns>
        public bool IsTrue(int index)
        {
            bool isTrue = false;
            IsTrue(index, parentSector, ref isTrue);

            return isTrue;
        }

        /// <summary>
        /// 基本となるINTEGRATEのIsTrueをまずは実装する
        /// </summary>
        /// <param name="index">データ(レコード)のインデックス</param>
        /// <param name="parentsector"></param>
        /// <param name="isTrue"></param>
        internal void IsTrue(int index, NewQuestionSectors._NewQuestionSector parentsector, ref bool isTrue)
        {
            var dataprocess = parentsector.ParentDataProcess;
            if (SubCriterias != null)
            {
                bool isTrueSub = false;
                foreach (NewQuestionSectorCriteria subcriteria in SubCriterias)
                {

                    subcriteria.IsTrue(index, parentsector, ref isTrueSub);
                }
                isTrue = performOP(isTrue, isTrueSub, Operator);
                if (dataprocess.ReverseIsTrue)
                {
                    isTrue = !isTrue;
                }
            }
            else
            {
                CriteriaOperator criteriaOperator = ConvertToCriteriaOperator(CriteriaOperatorDescription);

                //mantis:2150 絞り込みの仕様に合わせる対応。
                //条件値に("xxxx=アイテム名")がセットされた場合、有効とする。
                //Index=0初回のみ、「CriteriaOperatorDescription」がアイテム名であるかを確認。
                //Index>0の場合、「QuestionIDforDP+CriteriaOperatorDescription+CriteriaValueDescription」キー
                //で取得済み情報を確認、あればそのまま使用する。
                //条件値にアイテム名が許容するのは、サンプル削除とデータ修正のみ。その他の加工への影響がない。
                //sectorQuestionid + CriteriaOperatorDescription + CriteriaValueDescription

                string key = QuestionIDforDP + CriteriaOperatorDescription + CriteriaValueDescription;
                bool[] filteringFlag = null;
                if (dataprocess.DataProcessCode == DataProcessCode.ModifyData || dataprocess.DataProcessCode == DataProcessCode.DeleteData)
                {
                    if (index == 0 && !dataprocess.ParentCollection.IsFilterResultData(key))
                    {
                        /*
                        var question = parentsector.ParentQuestion;
                        decimal qcwebid = dataprocess.ParentCollection.QcWebId;
                        //QuestionIDforDPの質問タイプ
                        Question.Questions.Question questionForDp = dataprocess.ParentCollection.GetQuestion(QuestionIDforDP);
                        QuestionType questionType = questionForDp != null ? questionForDp.QuestionType : 0;
                        var dirPath = dataprocess.ParentCollection.DataDirectoryPath;
                        QueryItemName query = new QueryItemName();
                        */
                        //decimal criteriaValueQuestionid = (decimal)0;
                        //既に取得ずみのQuestionsから検索する
                        Question.Questions questions = dataprocess.ParentCollection.Questions;
                        if (questions != null)
                        {
                            /*
                            foreach (object criteriaValueQuestion in questions.Values)
                            {
                                var criteriaQuestion = criteriaValueQuestion as Question.Questions.Question;
                                string questionName = criteriaQuestion.Name;
                                //query.QuestionNameToID()のQuestionName比較方法に合わせる。
                                if (questionName == CriteriaValueDescription.ToUpper())
                                {
                                    criteriaValueQuestionid = criteriaQuestion.ID;
                                    break;
                                }
                            }
                            */
                            // 条件値がアイテム名の場合は、Filteringを利用する
                            // 条件値がアイテム名ではない場合はcriteriaQはnullとなるので、以降の処理に任せる

                            Question.Questions.Question criteriaQ = null;//by 191 
                            try//try catch by 191 for  getting narrow error when zero index and questions comminf for add3 option checking criteria
                            {
                                criteriaQ = questions[CriteriaValueDescription];
                            }
                            catch { }
                            if (criteriaQ != null)
                            {
                                var criteriaData = dataprocess.ParentCollection.GetRawData(criteriaQ.ID);
                                var data = dataprocess.ParentCollection.GetRawData(QuestionIDforDP);
                                GlobalTabulation.Filtering(data, criteriaData, criteriaOperator, ref filteringFlag);
                                dataprocess.ParentCollection.SetFilterResultData(key, filteringFlag);
                            }
                        }
                        //else
                        //{
                        //    //アイテム名→アイテムIDを検索する場合、許容するQuestionタイプ
                        //    QuestionType allowQType = QuestionType.SA | QuestionType.N | QuestionType.FA;
                        //    if (questionType == QuestionType.MA) allowQType = QuestionType.MA;
                        //    CriteriaValueQuestionid = query.QuestionNameToID(qcwebid, allowQType, CriteriaValueDescription);
                        //}
                        /*
                        if (criteriaValueQuestionid > 0)
                        {
                            List<Data> criteriaData = dataprocess.ParentCollection.GetRawData(criteriaValueQuestionid);
                            var data = dataprocess.ParentCollection.GetRawData(QuestionIDforDP);
                            // Filtering201に仲介、Filter結果を取得する。(前結果がないので、演算子がOrとする。)
                            GlobalTabulation.Filtering(data, criteriaData, criteriaOperator, ref filteringFlag, Tabulation.Operator.opOr);
                            dataprocess.ParentCollection.SetFilterResultData(key, filteringFlag);
                        }
                        */
                    }
                    else
                    {
                        if (dataprocess.ParentCollection.IsFilterResultData(key))
                        {
                            filteringFlag = dataprocess.ParentCollection.GetFilterResultData(key);
                        }
                    }
                }

                //前条件との演算子がAndで、かつ、前条件がfalseの場合は、今回の評価がtrueだろうがfalseだろうが結果はfalseになるので評価をスキップする
                if (Operator == Tabulation.Operator.opAnd && !isTrue) return;

                bool isTrueSub = false;
                var targetData = GetData(parentsector, QuestionIDforDP, index);

                if (filteringFlag != null)
                {
                    isTrueSub = filteringFlag[index];
                }
                else
                {
                    if (targetData is SAData)
                    {
                        Data criteriadata = null;
                        string criteriavalue = CriteriaValueDescription;
                        if (CriteriaValueDescription.StartsWith("[") && CriteriaValueDescription.EndsWith("]"))
                        {

                            criteriavalue = criteriavalue.Remove(0, 1);
                            criteriavalue = criteriavalue.Remove(criteriavalue.Length - 1, 1);
                            string[] criteriavaluearray = criteriavalue.Split('@');
                            if (criteriavaluearray.Length == 3)
                            {
                                decimal result;
                                if (decimal.TryParse(criteriavaluearray[1], out result))
                                {
                                    decimal qid = decimal.Parse(criteriavaluearray[1]);
                                    criteriadata = GetData(parentsector, qid, index);
                                    if (criteriadata.IsNA == true)
                                    {
                                        criteriavalue = string.Empty;
                                    }
                                    else if (criteriadata.IsIV == true)
                                    {
                                        criteriavalue = "*";
                                    }
                                    else if (criteriadata is SAData)
                                    {
                                        criteriavalue = Convert.ToString(((SAData)criteriadata).Value);
                                    }
                                    else if (criteriadata is NData)
                                    {
                                        criteriavalue = Convert.ToString(((NData)criteriadata).Value);
                                    }
                                    else if (criteriadata is FAData)
                                    {
                                        criteriavalue = ((FAData)criteriadata).Value;
                                    }
                                }
                            }
                        }
                        isTrueSub = IsTrue((SAData)targetData, criteriaOperator, criteriavalue, index);
                        if (isTrueSub == false && targetData.IsNA && dataprocess.ReverseIsTrue)
                        {
                            isTrueSub = true;
                        }
                    }
                    else if (targetData is MAData)
                    {

                        Data criteriadata = null;
                        string criteriavalue = CriteriaValueDescription;
                        if (CriteriaValueDescription.StartsWith("[") && CriteriaValueDescription.EndsWith("]"))
                        {

                            criteriavalue = criteriavalue.Remove(0, 1);
                            criteriavalue = criteriavalue.Remove(criteriavalue.Length - 1, 1);
                            string[] criteriavaluearray = criteriavalue.Split('@');
                            if (criteriavaluearray.Length == 3)
                            {
                                decimal result;
                                if (decimal.TryParse(criteriavaluearray[1], out result))
                                {
                                    decimal qid = decimal.Parse(criteriavaluearray[1]);
                                    criteriadata = GetData(parentsector, qid, index);
                                    if (criteriadata.IsNA == true)
                                    {
                                        criteriavalue = string.Empty;
                                    }
                                    else if (criteriadata.IsIV == true)
                                    {
                                        criteriavalue = "*";
                                    }
                                    else if (criteriadata is MAData)
                                    {
                                        criteriavalue = Convert.ToString(((MAData)criteriadata).CodeValue);//(criteriadata as MAData).BinValue(parentSector.ParentQuestion.Sectors.Count);// 
                                    }

                                }
                            }
                        }


                        isTrueSub = IsTrue((MAData)targetData, criteriaOperator, criteriavalue);// CriteriaValueDescription
                    }
                    else if (targetData is NData)
                    {

                        Data criteriadata = null;
                        string criteriavalue = CriteriaValueDescription;
                        if (CriteriaValueDescription.StartsWith("[") && CriteriaValueDescription.EndsWith("]"))
                        {

                            criteriavalue = criteriavalue.Remove(0, 1);
                            criteriavalue = criteriavalue.Remove(criteriavalue.Length - 1, 1);
                            string[] criteriavaluearray = criteriavalue.Split('@');
                            if (criteriavaluearray.Length == 3)
                            {
                                decimal result;
                                if (decimal.TryParse(criteriavaluearray[1], out result))
                                {
                                    decimal qid = decimal.Parse(criteriavaluearray[1]);
                                    criteriadata = GetData(parentsector, qid, index);
                                    if (criteriadata.IsNA == true)
                                    {
                                        criteriavalue = string.Empty;
                                    }
                                    else if (criteriadata.IsIV == true)
                                    {
                                        criteriavalue = "*";
                                    }
                                    else if (criteriadata is SAData)
                                    {
                                        criteriavalue = Convert.ToString(((SAData)criteriadata).Value);
                                    }
                                    else if (criteriadata is NData)
                                    {
                                        criteriavalue = Convert.ToString(((NData)criteriadata).Value);
                                    }
                                    else if (criteriadata is FAData)
                                    {
                                        criteriavalue = ((FAData)criteriadata).Value;
                                    }
                                }
                            }
                        }


                        isTrueSub = IsTrue((NData)targetData, criteriaOperator, criteriavalue);// CriteriaValueDescription
                    }
                    else if (targetData is FAData)
                    {
                        Data criteriadata = null;
                       
                        string criteriavalue = Regex.Unescape(CriteriaValueDescription);//[UAT1] [Redmine ID:206108]//https://app.gluemodel.com/#/project/task/4295063196 //CriteriaValueDescription;
                        if (criteriavalue.StartsWith("[") && criteriavalue.EndsWith("]"))
                        {

                            criteriavalue = criteriavalue.Remove(0, 1);
                            criteriavalue = criteriavalue.Remove(criteriavalue.Length - 1, 1);
                            string[] criteriavaluearray = criteriavalue.Split('@');
                            if (criteriavaluearray.Length == 3)
                            {
                                decimal result;
                                if (decimal.TryParse(criteriavaluearray[1], out result))
                                {
                                    decimal qid = decimal.Parse(criteriavaluearray[1]);
                                    criteriadata = GetData(parentsector, qid, index);
                                    if (criteriadata.IsNA == true)
                                    {
                                        criteriavalue = string.Empty;
                                    }
                                    else if (criteriadata.IsIV == true)
                                    {
                                        criteriavalue = "*";
                                    }
                                    else if (criteriadata is SAData)
                                    {
                                        criteriavalue = Convert.ToString(((SAData)criteriadata).Value);
                                    }
                                    else if (criteriadata is NData)
                                    {
                                        criteriavalue = Convert.ToString(((NData)criteriadata).Value);
                                    }
                                    else if (criteriadata is FAData)
                                    {
                                        criteriavalue = ((FAData)criteriadata).Value;
                                    }
                                }
                            }
                        }

                        isTrueSub = IsTrue((FAData)targetData, criteriaOperator, criteriavalue);
                    }
                }
                switch (Operator)
                {
                    case Tabulation.Operator.opAnd:
                        isTrue = isTrue && isTrueSub;
                        break;
                    case Tabulation.Operator.opOr:
                        isTrue = isTrue || isTrueSub;
                        break;
                }

            }

        }
        private bool performOP(bool filteringFlag, bool filteringFlagSub, Operator ope)//191 from ramees
        {
            if (null == filteringFlag) return filteringFlagSub;
            if (null == filteringFlagSub) return filteringFlag;
            if (ope == Operator.opAnd)
            {
                filteringFlag = filteringFlag & filteringFlagSub;
            }
            else
            {
                filteringFlag = filteringFlag | filteringFlagSub;
            }
            return filteringFlag;

        }
        private Data GetData(NewQuestionSectors._NewQuestionSector parentsector, decimal questionid, int index)
        {
            if (questionid == 0)
            {
                // questionidが0です
                // throw new QCWebException("QCCMN05020001", Common.GlobalsCommonConstant.LogLevel.FATAL, null);//191  commented for sample id's Item id becomes zero
            }
            var dataProcesses = parentsector.ParentDataProcess.ParentCollection;
            return dataProcesses.GetRawData(index, questionid);
        }

        private bool IsTrue(SAData data, CriteriaOperator criteriaOperator, string criteriaValueDescription, int indexpos = -1)//trial
        {
            bool isTrueSub = false;
            QuestionType qType = Tabulation.QuestionType.SA;

            int[] criteriaSectorList = null;
            DataType criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
                                qType, criteriaValueDescription, out criteriaSectorList, parentSector == null ? 0 : ParentQuestion.Sectors.Count, indexpos);//trial
            /* DataType criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
                                qType, criteriaValueDescription, out criteriaSectorList,parentSector.ParentQuestion.Sectors.Count);*/
            if ((criteriaDataType & (DataType.NAData | DataType.IVData)) != 0)
            {
                switch (criteriaOperator)
                {
                    case CriteriaOperator.Equal:
                        isTrueSub = data.IsAnyOne(criteriaDataType);
                        break;
                    case CriteriaOperator.NotEqual:
                        isTrueSub = !data.IsAnyOne(criteriaDataType);
                        break;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(criteriaValueDescription) || criteriaValueDescription.Equals("*"))//|| data[i].IsNA|| data[i].IsIV
                {
                    string datavalue = string.Empty;
                    
                    if (data.IsIV == true)
                    {
                        datavalue = "*";
                    }
                    else if (data.IsNA == true)
                    {
                        datavalue =string.Empty;
                    }
                    else if (data is SAData)
                    {
                        datavalue = Convert.ToString(((SAData)data).Value);
                    }
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            return isTrueSub = datavalue == criteriaValueDescription;
                            break;
                        case CriteriaOperator.NotEqual:
                            return isTrueSub = datavalue != criteriaValueDescription;
                            break;
                    }                  
                }
                if (data.DataType == DataType.IVData || data.DataType == DataType.NAData)//For = <>
                {
                    return criteriaOperator == CriteriaOperator.NotEqual; ;
                }
                else if (criteriaValueDescription.StartsWith("-"))//FOR NEG
                {
                    double retrnval;
                    if (!double.TryParse(criteriaValueDescription, out retrnval))//178707#note-9 -for
                    {
                        return criteriaOperator == CriteriaOperator.NotEqual; 
                    }
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            return isTrueSub = double.Parse(Convert.ToString(data.Value)) == double.Parse(criteriaValueDescription);
                            break;
                        case CriteriaOperator.NotEqual:
                            return isTrueSub = double.Parse(Convert.ToString(data.Value)) != double.Parse(criteriaValueDescription);//  isTrueSub = data != criteriaSectorList[0] && !data.IsNA && !data.IsIV;
                            Console.WriteLine(data);
                            break;
                        case CriteriaOperator.GreaterEqual:
                            return isTrueSub = double.Parse(Convert.ToString(data.Value)) >= double.Parse(criteriaValueDescription);
                            break;
                        case CriteriaOperator.Greater:
                            return isTrueSub = double.Parse(Convert.ToString(data.Value)) > double.Parse(criteriaValueDescription);
                            break;
                        case CriteriaOperator.LessEqual:
                            return isTrueSub = double.Parse(Convert.ToString(data.Value)) <= double.Parse(criteriaValueDescription);
                            break;
                        case CriteriaOperator.Less:
                            return isTrueSub = double.Parse(Convert.ToString(data.Value)) < double.Parse(criteriaValueDescription);
                            break;
                    }
                }
                if (criteriaSectorList == null || criteriaSectorList.Length == 0)
                {
                    if (criteriaValueDescription.Equals("*") || string.IsNullOrEmpty(criteriaValueDescription))
                    {
                        string datavalue = string.Empty;
                        if (data.IsNA == true)
                        {
                            datavalue = string.Empty;
                        }
                        else if (data.IsIV == true)
                        {
                            datavalue = "*";
                        }
                        else if (data is SAData)
                        {
                            datavalue = Convert.ToString(((SAData)data).Value);
                        }

                        switch (criteriaOperator)
                        {
                            case CriteriaOperator.Equal:
                                return isTrueSub = datavalue == criteriaValueDescription;
                                break;
                            case CriteriaOperator.NotEqual:
                                return isTrueSub = datavalue != criteriaValueDescription;
                                break;
                        }
                    }
                    else//[Redmine id : 178707] -
                    {
                        //if (data.DataType == DataType.IVData || data.DataType == DataType.NAData)
                        //{
                        //    return criteriaOperator == CriteriaOperator.NotEqual; ;
                        //}
                        double retrnval;
                        if(!double.TryParse(criteriaValueDescription,out retrnval))//178707#note-9
                        {
                            return criteriaOperator == CriteriaOperator.NotEqual;
                        }

                        switch (criteriaOperator)
                        {
                            case CriteriaOperator.Equal:
                                return isTrueSub = double.Parse(Convert.ToString(data.Value)) == double.Parse(criteriaValueDescription);
                                break;
                            case CriteriaOperator.NotEqual:
                                return isTrueSub = double.Parse(Convert.ToString(data.Value)) != double.Parse(criteriaValueDescription);//  isTrueSub = data != criteriaSectorList[0] && !data.IsNA && !data.IsIV;
                                Console.WriteLine(data);
                                break;
                            case CriteriaOperator.GreaterEqual:
                                return isTrueSub = double.Parse(Convert.ToString(data.Value)) >= double.Parse(criteriaValueDescription);
                                break;
                            case CriteriaOperator.Greater:
                                return isTrueSub = double.Parse(Convert.ToString(data.Value)) > double.Parse(criteriaValueDescription);
                                break;
                            case CriteriaOperator.LessEqual:
                                return isTrueSub = double.Parse(Convert.ToString(data.Value)) <= double.Parse(criteriaValueDescription);
                                break;
                            case CriteriaOperator.Less:
                                return isTrueSub = double.Parse(Convert.ToString(data.Value)) < double.Parse(criteriaValueDescription);
                                break;
                        }
                        //commented for QC4 CRITERIA VARIABLE IN VALUE PLACE //[Redmine id : 178707] -
                        /* //この例外は本来、発生しない（画面側で制御しているため）
                         // 条件値が不正

                         throw new QCWebException("QCCMN05020000", Common.GlobalsCommonConstant.LogLevel.FATAL, null);*/
                    }
                }
                else if (criteriaSectorList.Length > 1)//added else  https://app.gluemodel.com/#/project/task/4295057184
                {
                    //カテゴリが複数
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            isTrueSub = data == criteriaSectorList;
                            break;
                        case CriteriaOperator.NotEqual:
                            isTrueSub = data != criteriaSectorList;//   isTrueSub = data != criteriaSectorList && !data.IsNA && !data.IsIV;
                            break;
                    }
                }
                else
                {
                    //カテゴリがひとつのみ
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            isTrueSub = data == criteriaSectorList[0];
                            break;
                        case CriteriaOperator.NotEqual:
                            isTrueSub = data != criteriaSectorList[0];//  isTrueSub = data != criteriaSectorList[0] && !data.IsNA && !data.IsIV;
                            Console.WriteLine(data);
                            break;
                        case CriteriaOperator.GreaterEqual:
                            isTrueSub = data >= criteriaSectorList[0];
                            break;
                        case CriteriaOperator.Greater:
                            isTrueSub = data > criteriaSectorList[0];
                            break;
                        case CriteriaOperator.LessEqual:
                            isTrueSub = data <= criteriaSectorList[0];
                            break;
                        case CriteriaOperator.Less:
                            isTrueSub = data < criteriaSectorList[0];
                            break;
                    }
                }
            }
            return isTrueSub;
        }

        private bool IsTrue(MAData data, CriteriaOperator criteriaOperator, string criteriaValueDescription)
        {
            bool isTrueSub = false;
            QuestionType qType = Tabulation.QuestionType.MA;

            int[] criteriaSectorList = null;
            //DataType criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
            //                    qType, criteriaValueDescription, out criteriaSectorList);
            DataType criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
                                qType, criteriaValueDescription, out criteriaSectorList, parentSector == null ? 0 : ParentQuestion.Sectors.Count);
            if ((criteriaDataType & (DataType.NAData | DataType.IVData)) != 0)
            {
                switch (criteriaOperator)
                {
                    case CriteriaOperator.Equal:
                        isTrueSub = data.IsAnyOne(criteriaDataType);
                        break;
                    case CriteriaOperator.NotEqual:
                        isTrueSub = !data.IsAnyOne(criteriaDataType);
                        break;
                }
            }
            else
            {
                if (criteriaSectorList == null || criteriaSectorList.Length == 0)
                {



                    if (criteriaValueDescription.Equals("*") || string.IsNullOrEmpty(criteriaValueDescription))//test trial
                    {
                        string datavalue = string.Empty;
                        criteriaDataType = DataType.NAData;
                        if (criteriaValueDescription.Equals("*"))
                        {
                            criteriaDataType = DataType.IVData;
                        }
                        if (data.IsNA == true)
                        {
                            datavalue = string.Empty;
                        }
                        else if (data.IsIV == true)
                        {
                            datavalue = "*";
                        }
                        else if (data is NData)
                        {
                            // datavalue = Convert.ToString(((MAData)data).Value);
                        }

                        switch (criteriaOperator)
                        {
                            case CriteriaOperator.Equal:
                                return isTrueSub = data.IsAnyOne(criteriaDataType);// return isTrueSub = datavalue == criteriaValueDescription;
                                break;
                            case CriteriaOperator.NotEqual:
                                return isTrueSub = !data.IsAnyOne(criteriaDataType);// return isTrueSub = datavalue != criteriaValueDescription;
                                break;

                        }
                    }
                    else
                    {
                        // [Redmine id : 178707] -
                        //この例外は本来、発生しない（画面側で制御しているため）
                        // 条件値が不正
                        //  throw new QCWebException("QCCMN05020000", Common.GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                }
                int[] CriteriaValueList = MAData.GetCriteriaValueList(criteriaSectorList);
                switch (criteriaOperator)
                {
                    case CriteriaOperator.Equal:
                        isTrueSub = data == CriteriaValueList;
                        break;
                    case CriteriaOperator.NotEqual:
                        isTrueSub = data != CriteriaValueList;//  isTrueSub = data != CriteriaValueList && !data.IsNA && !data.IsIV;
                        break;
                }
            }
            return isTrueSub;
        }

        private bool IsTrue(NData data, CriteriaOperator criteriaOperator, string criteriaValueDescription)
        {
            bool isTrueSub = false;
            QuestionType qType = Tabulation.QuestionType.N;

            double[] criteriaNumberList = null;
            DataType criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<double>(
                                qType, criteriaValueDescription, out criteriaNumberList);

            if ((criteriaDataType & (DataType.NAData | DataType.IVData)) != 0)
            {
                switch (criteriaOperator)
                {
                    case CriteriaOperator.Equal:
                        isTrueSub = data.IsAnyOne(criteriaDataType);
                        break;
                    case CriteriaOperator.NotEqual:
                        isTrueSub = !data.IsAnyOne(criteriaDataType);
                        break;
                }
            }
            else
            {
                if (criteriaNumberList == null || criteriaNumberList.Length == 0)
                {
                    NData.ValueRange[] criteriaValueRangeList = null;
                    criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<NData.ValueRange>(
                                        qType, criteriaValueDescription, out criteriaValueRangeList);
                    if (criteriaValueRangeList == null || criteriaValueRangeList.Length == 0)
                    {
                        if (criteriaValueDescription.Equals("*") || string.IsNullOrEmpty(criteriaValueDescription))
                        {
                            string datavalue = string.Empty;
                            if (data.IsNA == true)
                            {
                                datavalue = string.Empty;
                            }
                            else if (data.IsIV == true)
                            {
                                datavalue = "*";
                            }
                            else if (data is NData)
                            {
                                datavalue = Convert.ToString(((NData)data).Value);
                            }

                            switch (criteriaOperator)
                            {
                                case CriteriaOperator.Equal:
                                    return isTrueSub = datavalue == criteriaValueDescription;
                                    break;
                                case CriteriaOperator.NotEqual:
                                    return isTrueSub = datavalue != criteriaValueDescription;
                                    break;
                            }
                        }
                        else
                        {//[Redmine id : 178707] -
                         //この例外は本来、発生しない（画面側で制御しているため）
                         // 条件値が不正
                         //  throw new QCWebException("QCCMN05020000", Common.GlobalsCommonConstant.LogLevel.FATAL, null);
                            double retrnval;
                            if (!double.TryParse(criteriaValueDescription, out retrnval))//178707#note-9
                            {
                                return criteriaOperator == CriteriaOperator.NotEqual;
                            }

                            switch (criteriaOperator)
                            {
                                case CriteriaOperator.Equal:
                                    return isTrueSub = double.Parse(Convert.ToString(data.Value)) == double.Parse(criteriaValueDescription);
                                    break;
                                case CriteriaOperator.NotEqual:
                                    return isTrueSub = double.Parse(Convert.ToString(data.Value)) != double.Parse(criteriaValueDescription);//  isTrueSub = data != criteriaSectorList[0] && !data.IsNA && !data.IsIV;
                                    Console.WriteLine(data);
                                    break;
                                case CriteriaOperator.GreaterEqual:
                                    return isTrueSub = double.Parse(Convert.ToString(data.Value)) >= double.Parse(criteriaValueDescription);
                                    break;
                                case CriteriaOperator.Greater:
                                    return isTrueSub = double.Parse(Convert.ToString(data.Value)) > double.Parse(criteriaValueDescription);
                                    break;
                                case CriteriaOperator.LessEqual:
                                    return isTrueSub = double.Parse(Convert.ToString(data.Value)) <= double.Parse(criteriaValueDescription);
                                    break;
                                case CriteriaOperator.Less:
                                    return isTrueSub = double.Parse(Convert.ToString(data.Value)) < double.Parse(criteriaValueDescription);
                                    break;
                            }
                        }
                    }

                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            isTrueSub = data.IsAnyOne(criteriaValueRangeList);
                            break;
                        case CriteriaOperator.NotEqual:
                            isTrueSub = !data.IsAnyOne(criteriaValueRangeList);
                            break;
                    }

                }
                else if (criteriaNumberList.Length > 1)
                {
                    //複数指定の場合
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            isTrueSub = data == (criteriaNumberList);
                            break;
                        case CriteriaOperator.NotEqual:
                            isTrueSub = data != (criteriaNumberList);
                            break;
                    }
                }
                else
                {
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            isTrueSub = data == criteriaNumberList[0];
                            break;
                        case CriteriaOperator.NotEqual:
                            isTrueSub = data != criteriaNumberList[0];
                            break;
                        case CriteriaOperator.GreaterEqual:
                            isTrueSub = data >= criteriaNumberList[0];
                            break;
                        case CriteriaOperator.Greater:
                            isTrueSub = data > criteriaNumberList[0];
                            break;
                        case CriteriaOperator.LessEqual:
                            isTrueSub = data <= criteriaNumberList[0];
                            break;
                        case CriteriaOperator.Less:
                            isTrueSub = data < criteriaNumberList[0];
                            break;
                    }
                }
            }
            return isTrueSub;
        }

        private bool IsTrue(FAData data, CriteriaOperator criteriaOperator, string criteriaValueDescription)
        {
            bool isTrueSub = false;

            //Mantis#0002150対応 FAの条件値"DK"が「無回答」とみなす。 Tabulation_Shared.EasyFiltering line:2960 参照
            if (criteriaValueDescription.Equals(string.Empty) || criteriaValueDescription.Equals("DK"))
            {
                DataType criteriaDataType = DataType.NAData;
                switch (criteriaOperator)
                {
                    case CriteriaOperator.Equal:
                        isTrueSub = data.Equals(criteriaDataType);
                        break;
                    case CriteriaOperator.NotEqual:
                        isTrueSub = !data.Equals(criteriaDataType);
                        break;
                }
                return isTrueSub;
            }

            //var CriteriaPattern = FAData.ConvertToRegExpPattern(System.Text.RegularExpressions.Regex.Unescape(criteriaValueDescription));
            //if (!criteriaValueDescription.Contains("*")) CriteriaPattern = criteriaValueDescription;////[Redmine id : 175610]
            var CriteriaPattern = criteriaValueDescription;//[Redmine id : 175610]
           // CriteriaPattern = Regex.Unescape(CriteriaPattern);//https://app.gluemodel.com/#/project/task/4295063196
            switch (criteriaOperator)
            {
                case CriteriaOperator.Equal:
                    isTrueSub = data == CriteriaPattern;  //[Redmine id : 175610]
                    break;
                case CriteriaOperator.NotEqual:
                    isTrueSub = data != CriteriaPattern;  //[Redmine id : 175610]
                    break;
            }
            return isTrueSub;
        }

        private static CriteriaOperator ConvertToCriteriaOperator(string criteriaOperatorDescription)
        {
            CriteriaOperator criteriaOperator = CriteriaOperator.Equal;
            if (criteriaOperatorDescription.Equals("="))
            {
                criteriaOperator = CriteriaOperator.Equal;
            }
            else if (criteriaOperatorDescription.Equals("<>") || criteriaOperatorDescription.Equals("!="))
            {
                criteriaOperator = CriteriaOperator.NotEqual;
            }
            else if (criteriaOperatorDescription.Equals(">="))
            {
                criteriaOperator = CriteriaOperator.GreaterEqual;
            }
            else if (criteriaOperatorDescription.Equals(">"))
            {
                criteriaOperator = CriteriaOperator.Greater;
            }
            else if (criteriaOperatorDescription.Equals("<="))
            {
                criteriaOperator = CriteriaOperator.LessEqual;
            }
            else if (criteriaOperatorDescription.Equals("<"))
            {
                criteriaOperator = CriteriaOperator.Less;
            }
            return criteriaOperator;
        }

        /// <summary>
        /// 回答個数を取得する。
        /// </summary>
        /// <param name="index">データ(レコード)のインデックス</param>
        /// <returns>回答個数(数字)または“*”(非該当)</returns>
        /// <remarks>
        ///  データが「DK：無回答」の場合、0としてカウントされる。但し、条件に「DK：無回答」が含まれている場合はカウントする						
        ///  データが「*：非該当」はカウント対象外とし、結果は必ず「*：非該当」とする。但し、条件値に「*：非該当」が入っている場合はカウント対象(=1)とする						
        ///  上記以外のデータは、「=」条件のみの基本ルールに従う						
        ///  [SA]どの新カテゴリ範囲にも合致しないデータは「DK：無回答」にする						
        ///  新回答タイプSA時の範囲指定「～n」でも「DK：無回答」は0とみなし合致させる						
        /// </remarks>
        public string GetCountResult(int index)
        {

            int[] criteriaSectorList = null;

            QuestionType qType = QuestionType.MA;

            //条件値の質問タイプおよび条件値カテゴリ配列を作成
            DataType criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<int>(qType, CriteriaValueDescription, out criteriaSectorList, parentSector.ParentQuestion.CategoryCount);

            string retVal = "0";
            var targetData = GetData(parentSector, QuestionIDforDP, index);
            MAData maData = targetData as MAData;
            if ((criteriaDataType & (DataType.IVData | DataType.NAData)) != 0)
            {
                //非該当
                //但し、条件値に「*：非該当」が入っている場合はカウント対象(=1)とする
                if ((criteriaDataType & DataType.IVData) != 0)
                {
                    if (maData.IsIV)
                    {
                        retVal = "1";
                    }
                }
                //無回答
                //但し、条件に「DK：無回答」が含まれている場合はカウントする
                if ((criteriaDataType & DataType.NAData) != 0)
                {
                    if (maData.IsNA)
                    {
                        retVal = "1";
                    }
                }
            }
            else
            {
                if (criteriaSectorList == null || criteriaSectorList.Length == 0)
                {
                    //この例外は本来、発生しない（画面側で制御しているため）
                    // 条件値が不正
                    throw new QCWebException("QCCMN05020000", Common.GlobalsCommonConstant.LogLevel.FATAL, null);
                }

                //01の文字列　非該当：*　無回答：string.empty
                retVal = maData.FilterBinValue(criteriaSectorList, this.CriteriaOperatorDescription);

                //データが「DK：無回答」の場合、0としてカウントされる。
                if (string.IsNullOrEmpty(retVal))
                {
                    retVal = "0";
                }
                else if (retVal == "*")
                {
                    //データが「*：非該当」はカウント対象外とし、結果は必ず「*：非該当」とする
                    retVal = "*";
                }
                else
                {
                    if (criteriaSectorList.Length == 1 && criteriaSectorList[0] == 0)
                    {
                        retVal = "0";
                    }
                    else
                    {
                        char[] charVal = retVal.ToCharArray();

                        var cnt = 0;
                        foreach (char c in charVal)
                        {
                            cnt += int.Parse(c.ToString());
                        }
                        retVal = cnt.ToString();
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// MtoS用BinValue文字列を作成する。
        /// </summary>
        /// <param name="index">データ(レコード)のインデックス</param>
        /// <returns>
        ///「DK：無回答」のデータは、必ず「DK：無回答」が設定される
        ///「*：非該当」のデータは必ず「*：非該当」が設定される
        ///　新カテゴリ範囲外のデータしか存在しない場合は「DK：無回答」になる 
        /// </returns>
        public string GetMtoSResultBinValue(int index)
        {
            int[] criteriaSectorList = null;

            QuestionType qType = QuestionType.MA;

            //条件値の質問タイプおよび条件値カテゴリ配列を作成
            DataType criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<int>(qType, CriteriaValueDescription, out criteriaSectorList);

            string retVal = "0";
            var targetData = GetData(parentSector, QuestionIDforDP, index);
            MAData maData = targetData as MAData;

            //「DK：無回答」のデータは、必ず「DK：無回答」が設定される						
            //「*：非該当」のデータは必ず「*：非該当」が設定される						
            if (maData.IsIV)
            {
                return "*";
            }
            else if (maData.IsNA)
            {
                return string.Empty;
            }

            if ((criteriaDataType & (DataType.IVData | DataType.NAData)) != 0)
            {
                //ここに来ないはず
                return string.Empty;
            }
            else
            {
                if (criteriaSectorList == null || criteriaSectorList.Length == 0)
                {
                    //この例外は本来、発生しない（画面側で制御しているため）
                    // 条件値が不正
                    throw new QCWebException("QCCMN05020000", Common.GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                //01の文字列　非該当：*　無回答：string.empty
                retVal = maData.FilterBinValue(criteriaSectorList, this.CriteriaOperatorDescription);

            }

            return retVal;
        }

        /// <summary>
        /// 親であるDataProcessクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IDataProcess ParentDataProcess
        {
            get
            {
                return parentSector == null ? null : parentSector.ParentDataProcess;
            }
        }

        /// <summary>
        /// 親であるNewQuestionクラスまたはNewVirtualQuestionクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public _INewQuestion ParentQuestion
        {
            get
            {
                return parentSector == null ? null : parentSector.ParentQuestion;
            }
        }

        /// <summary>
        /// 親であるNewQuestionSectorクラスまたはNewVirtualQuestionSectorクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public _INewQuestionSector ParentSector
        {
            get
            {
                return parentSector;
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            parentSector = null;
        }
    }
}
