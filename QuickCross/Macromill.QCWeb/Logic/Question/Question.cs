#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Question.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/4/2
 * 作　成　者：井川はるき
 * 更　新　日：2012/7/25
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Seasar.Quill;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Dao.ExDao.PmBean;
using Macromill.QCWeb.Dao.ExEntity.Customize;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.DataProcess;
using System.Text;

namespace Macromill.QCWeb.Question
{
    /// <summary>
    /// 質問情報コレクションクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("05CBFB87-F2B5-4675-89FF-2B9C271F35B6")]
    public class Questions : Hashtable, IQuestions
    {
        public string SurveyTitle { get; set; }
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 質問情報クラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("4A44E319-DAC3-4337-9559-FBFCB0CC8F5F")]
        public class Question : IQuestion
        {
            private Questions Collection = null;
            private decimal id = (decimal)0;
            private decimal qcwebid = (decimal)0;
            private int index = 0;
            private string number = null;
            private string number2 = null;
            private string name = null;
            private string description = null;
            private string originaldescription = null;
            /// <summary>
            /// 質問に紐付く選択肢コレクションを表すSectorsクラスのインスタンスへの参照
            /// </summary>
            protected Sectors sectors = null;
            private QCQuestionType qcquestiontype = QCQuestionType.None;
            private QCAnswerType qcanswertype = (QCAnswerType)0;
            /// <summary>
            /// 質問タイプを表すQuestionType列挙型の値
            /// </summary>
            protected Tabulation.QuestionType questiontype = (Tabulation.QuestionType)0;
            /// <summary>
            /// 子質問コレクションへの参照
            /// </summary>
            public Questions childquestions = null;
            private Sectors.Sector parentsector = null;
            private string tablename = null;
            private string columnname = null;
            private string toptablename = null;
            private DateTime lastUpdateDateTime = new DateTime(0);
            private DateTime dataEditDateTime = new DateTime(0);
            private bool dataEdit = false;
            private GlobalsCommonConstant.PROCESS_TYPE dataProcessType;
            private GlobalsCommonConstant.TemporaryDataProcess temporaryDataProcess;
            private decimal gtMatrixBaseItemId = (decimal)0;
            private Question gtMatrixBaseQuestion = null;
            private decimal? categoryEditID = null;
            private bool qc3BlankNumber = false;
			public bool isOrg = false;

			public int questionOrder; //QC4Change - This should not be changed as it is using for proper ordering in DP-Checklist. And its value should not be duplicated for any other variables

            /// <summary>
            /// QC3での質問タイプ文字列から質問タイプコードを表すQCQuestionType列挙型の値を返す静的メソッド
            /// </summary>
            /// <param name="qcqtypedescription">QC3での質問タイプ文字列</param>
            /// <returns>QC3での質問タイプを表すQCQuestionType列挙型の値</returns>
            public static QCQuestionType GetQCQuestionTypeFromDescription(string qcqtypedescription)
            {
                QCQuestionType rc = QCQuestionType.None;
                Enum.TryParse<QCQuestionType>(qcqtypedescription, out rc);
                return rc & QCQuestionType.QuestionTypeAllBit;
            }

            /// <summary>
            /// QC3での回答タイプ文字列から回答タイプコードを表すQCAnswerType列挙型の値を返す静的メソッド
            /// </summary>
            /// <param name="qcanstypedescription">QC3での回答タイプ文字列</param>
            /// <returns>QC3での回答タイプを表すQCAnswerType列挙型の値</returns>
            public static QCAnswerType GetQCAnswerTypeFromDescription(string qcanstypedescription)
            {
                QCAnswerType rc = (QCAnswerType)0;
                Enum.TryParse<QCAnswerType>(qcanstypedescription, out rc);
                return rc;
            }

            /// <summary>
            /// QC3での質問タイプコードを表すQCQuestionType列挙型の値から質問タイプ文字列を返す静的メソッド
            /// </summary>
            /// <param name="qcqtype">QC3での質問タイプを表すQCQuestionType列挙型の値</param>
            /// <returns>QC3での質問タイプ文字列</returns>
            public static string GetDescriptionFromQCQuestionType(QCQuestionType qcqtype)
            {
                QCQuestionType qcqType = qcqtype & QCQuestionType.QuestionTypeAllBit;
                if (Enum.IsDefined(typeof(QCQuestionType), qcqType) && qcqType != QCQuestionType.None)
                {
                    return qcqType.ToString().Replace("None, ", "").Replace("Normal, ", "");
                }
                return null;
            }

            /// <summary>
            /// QC3での回答タイプコードを表すQCAnswerType列挙型の値から回答タイプ文字列を返す静的メソッド
            /// </summary>
            /// <param name="qcanstype">QC3での回答タイプを表すQCAnswerType列挙型の値</param>
            /// <returns>QC3での回答タイプ文字列</returns>
            public static string GetDescriptionFromQCAnswerType(QCAnswerType qcanstype)
            {
                if (Enum.IsDefined(typeof(QCAnswerType), qcanstype))
                {
                    return qcanstype.ToString();
                }
                return null;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="questions">親コレクションへの参照</param>
            /// <param name="id">アイテムID</param>
            protected internal Question(Questions questions, decimal id)
            {
                Collection = questions;
                this.id = id;
                index = questions.Count + 1;
            }

			public Question()
			{
				this.sectors = new Sectors(this);
			}

			internal void SetQuestionType(QCMatrixCode matrixCode)
            {
                Message mainErrorMessage = new Message(Common.Constants.CommonMessageIndex.GetQuestionTypeWithCauseFatalMessageIndex);
                if (Collection == null)
                {
                    throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(Common.Constants.REFER_NULL_COLLECTION_MESSAGE_ID, name));
                }
                sectors = null;
                childquestions = null;
                /*
                if ((matrixCode == QCMatrixCode.MatrixChild || matrixCode == QCMatrixCode.FirstChild) && Collection != null)
                {
                    // マトリクス子質問の場合
                    if (Collection.ParentQuestion == null)
                    {
                        throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(Common.Constants.REFER_NULL_MATRIX_PARENT_QUESTION_MESSAGE_ID, name));
                    }
                    if ((int)(Collection.ParentQuestion.QuestionType & Tabulation.QuestionType.MatrixParent) == 0)
                    {
                        throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(Common.Constants.UNJUST_PARENT_QUESTIONTYPE_MESSAGE_ID, name));
                    }
                    questiontype = (Collection.ParentQuestion.QuestionType & ~Tabulation.QuestionType.MatrixParent)
                                 | Tabulation.QuestionType.MatrixChild;
                    return;
                }
                */
                bool isMatrixParent = matrixCode == QCMatrixCode.MatrixParent;
                bool isMatrixChild = (matrixCode == QCMatrixCode.MatrixChild || matrixCode == QCMatrixCode.FirstChild) && Collection != null;
                Question parentQ = Collection.ParentQuestion as Question;
                if (isMatrixChild)
                {
                    // マトリクス子質問の場合
                    if (Collection.ParentQuestion == null)
                    {
                        throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(Common.Constants.REFER_NULL_MATRIX_PARENT_QUESTION_MESSAGE_ID, name));
                    }
                    if ((int)(Collection.ParentQuestion.QuestionType & Tabulation.QuestionType.MatrixParent) == 0)
                    {
                        throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(Common.Constants.UNJUST_PARENT_QUESTIONTYPE_MESSAGE_ID, name));
                    }
                }
                // 基本的な質問タイプ
                switch (qcanswertype)
                {
                    case QCWeb.Question.QCAnswerType.SA:
                        questiontype = Tabulation.QuestionType.SA;
                        break;
                    case QCWeb.Question.QCAnswerType.MA:
                        questiontype = Tabulation.QuestionType.MA;
                        break;
                    case QCWeb.Question.QCAnswerType.N:
                        questiontype = Tabulation.QuestionType.N;
                        break;
                    case QCWeb.Question.QCAnswerType.FA:
                    case QCWeb.Question.QCAnswerType.D:
                        questiontype = Tabulation.QuestionType.FA;
                        break;
                    case (QCWeb.Question.QCAnswerType)0:
                        if (isMatrixParent) break;
                        throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(Common.Constants.UNJUST_QC_ANSWERTYPE_MESSAGE_ID, name));
                    default:
                        throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(Common.Constants.UNJUST_QC_ANSWERTYPE_MESSAGE_ID, name));
                }
                if (isMatrixChild)
                {
                    parentQ.questiontype |= questiontype;
                    questiontype |= Tabulation.QuestionType.MatrixChild;
                }
                else
                {
                    switch (qcanswertype)
                    {
                        case QCWeb.Question.QCAnswerType.SA:
                        case QCWeb.Question.QCAnswerType.MA:
                            sectors = new Sectors(this);
                            break;
                    }
                }
                if (qcquestiontype == QCWeb.Question.QCQuestionType.None)
                {
                    if (isMatrixChild)
                    {
                        questiontype |= parentQ.QuestionType & Tabulation.QuestionType.Property;
                    }
                    else
                    {
                        // 属性アイテムorマトリクス子質問or付加FA
                        if (!Collection.includeQuestion)
                        {
                            // 属性アイテム
                            questiontype |= Tabulation.QuestionType.Property;
                        }
                        else if (Collection.ParentQuestion != null)
                        {
                            // マトリクス子質問→ここには来ないはず
                            throw new QCWebException(mainErrorMessage
                                    , GlobalsCommonConstant.LogLevel.FATAL
                                    , GetResource.GetLogMessage(Common.Constants.INCONSISTENCY_MATRIX_CODE_MESSAGE_ID, name, matrixCode.ToString()));
                        }
                        else if (questiontype == Tabulation.QuestionType.FA && parentsector != null)
                        {
                            // 付加FA
                            questiontype |= Tabulation.QuestionType.FA_Sub;
                            //} else {
                            //    throw new QCWebException(mainErrorMessage
                            //            , GlobalsCommonConstant.LogLevel.FATAL
                            //            , GetResource.GetLogMessage(Common.Constants.NOT_EXIST_QC_QUESTIONTYPE_MESSAGE_ID, name));
                        }
                    }
                }
                else
                {
                    // マトリクス親質問or通常質問
                    // if (matrixCode == QCMatrixCode.MatrixParent)
                    if (isMatrixParent)
                    {
                        // マトリクス親質問
                        questiontype |= Tabulation.QuestionType.MatrixParent;
                        childquestions = new Questions(this);
                        if (qcquestiontype == QCWeb.Question.QCQuestionType.RAT)
                        {
                            questiontype |= Tabulation.QuestionType.Ratio;
                        }
                        else if (qcquestiontype == QCWeb.Question.QCQuestionType.RNK)
                        {
                            questiontype |= Tabulation.QuestionType.Rank;
                        }
                    }
                }
            }

            /// <summary>
            /// 質問IDを返す読み取り専用プロパティ
            /// </summary>
            [ComVisible(false)]
            public decimal ID
            {
                get
                {
                    return id;
                }
				set
				{
					id = value;
				}
            }

            /// <summary>
            /// 質問IDを返す読み取り専用プロパティ
            /// <note>VBAから呼べるようにdouble</note>
            /// </summary>
            public double ID2
            {
                get
                {
                    return (double)id;
                }
            }

            /// <summary>
            /// 質問の1ベースインデックスを返す読み取り専用プロパティ
            /// </summary>
            public int Index
            {
                get
                {
                    return index;
                }
				set
				{
					index = value;
				}
            }

            /// <summary>
            /// QCWeb管理IDを取得/設定するプロパティ<br />
            /// 設定は1度のみ可能
            /// </summary>
            [ComVisible(false)]
            public decimal QCWebID
            {
                get
                {
                    return qcwebid;
                }
                set
                {
                    if (qcwebid == (decimal)0)
                    {
                        qcwebid = value;
                    }
                }
            }

            /// <summary>
            /// QCWeb管理IDを返す読み取り専用プロパティ
            /// <note>VBAから呼べるようにdouble</note>
            /// </summary>
            public double QCWebID2
            {
                get
                {
                    return (double)qcwebid;
                }
            }

            /// <summary>
            /// 質問番号を取得/設定するプロパティ
            /// </summary>
            public string Number
            {
                get
                {
                    if (IsPropertyItem)
                        return null;
                    if ((questiontype & Tabulation.QuestionType.FA_Sub) == Tabulation.QuestionType.FA_Sub)
                        return null;
                    Question tmpQ = ParentQuestion as Question;
                    if (tmpQ == null)
                        return number;
                    return tmpQ.Number;
                }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        Question tmpQ = ParentQuestion as Question;
                        if (tmpQ == null){
                            number = value; 
                            number2 = value;
                        }
                    }
                }
            }

            public string Number2
            {
                get
                {
                    if (IsPropertyItem)
                        return null;
                    //if ((questiontype & Tabulation.QuestionType.FA_Sub) == Tabulation.QuestionType.FA_Sub)
                    //    return null;
                    Question tmpQ = ParentQuestion as Question;
                    if (tmpQ == null)
                        return number2;
                    return tmpQ.Number2;
                }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        Question tmpQ = ParentQuestion as Question;
                        if (tmpQ == null)
                            number2 = value;
                    }
                }
            }

            public string Number3
            {
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        Question tmpQ = ParentQuestion as Question;
                        if (tmpQ == null)
                            number = value;
                    }
                }
            }

            /// <summary>
            /// アイテム名を取得/設定するプロパティ<br />
            /// 設定は1度のみ可能
            /// </summary>
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    if (name == null && !string.IsNullOrWhiteSpace(value))
                    {
                        name = value;
                        if (number == null)
                            Number3 = value;
                    }
                }
            }

            /// <summary>
            /// アイテムの表現方法の一つを返す読み取り専用プロパティ
            /// </summary>
            public string Name2
            {
                get
                {
                    return "[" + (this.IsPropertyItem ? description : name) + "]";
                }
            }

            public string Name3
            {
                get
                {
                    return "[" + name + "]";
                }
            }

            private string superfluity = null;
            private string originalSuperfluity = null;

            /// <summary>
            /// QC3でいうところの表題を取得する読み取り専用プロパティ
            /// </summary>
            public string SuperfluityDescription
            {
                get
                {
                    return superfluity;
                }
            }

            /// <summary>
            /// QC3でいうところの既定の表題を取得する読み取り専用プロパティ
            /// </summary>
            public string OriginalSuperfluityDescription
            {
                get
                {
                    return originalSuperfluity;
                }
            }

            private int FIXED_DESCRIPTION_LENGTH = 1000;

            private void setDescription(string buffer, ref string superfluity, ref string description)
            {
                char[] delim = new char[]{(char)1};
                superfluity = buffer.Substring(0, FIXED_DESCRIPTION_LENGTH).Split(delim, 2)[0];
                if (string.IsNullOrEmpty(superfluity)) superfluity = null;
                description = buffer.Substring(FIXED_DESCRIPTION_LENGTH).Split(delim, 2)[0];
                if (string.IsNullOrEmpty(description)) description = null;
            }

            /// <summary>
            /// 質問文を取得/設定するプロパティ<br />
            /// 設定は1度のみ可能
            /// </summary>
            public string Description
            {
                get
                {
                    return description;
                }
                set
                {
                    if (description == null && !string.IsNullOrWhiteSpace(value))
                    {
                         description = value;
                        //setDescription(value, ref superfluity, ref description);
                    }
                }
            }

            /// <summary>
            /// 質問文を返すメソッド<br />
            /// 引数の指定に基づいて、親質問文や親選択肢文を付けて返す
            /// </summary>
            /// <param name="scenarioDescription">シナリオで編集された質問文 (省略可、既定値null)</param>
            /// <param name="AddMatrixParentQuestionDescription">マトリクスの子質問に、親質問文を付ける場合true (省略可、既定値true)</param>
            /// <param name="AddSubFAParentQuestionAndSectorDescription">付加FA質問に親質問文および親選択肢文を付ける場合true (省略可、既定値false)</param>
            /// <param name="Delimiter">親と子との区切り文字 (省略可、既定値\n)</param>
            /// <param name="bracketStart">子の囲み開始文字 (省略可、既定値[)</param>
            /// <param name="bracketEnd">子の囲み終了文字 (省略可、既定値])</param>
            /// <returns></returns>
            public string Description2(string scenarioDescription = null, bool AddMatrixParentQuestionDescription = true
                  , bool AddSubFAParentQuestionAndSectorDescription = false
                  , string Delimiter = "\n", string bracketStart = "[", string bracketEnd = "]")
            {
                string d = scenarioDescription == null ? description : scenarioDescription;
                //if (AddMatrixParentQuestionDescription
                //    && (questiontype & Tabulation.QuestionType.MatrixChild) == Tabulation.QuestionType.MatrixChild)
                //{
                //    Question parentQ = ParentQuestion as Question;
                //    if (parentQ != null)
                //    {
                //        return parentQ.Description + Delimiter + bracketStart + d + bracketEnd;
                //    }
                //}
                if ((questiontype & Tabulation.QuestionType.MatrixChild) == Tabulation.QuestionType.MatrixChild)
                {
                    if (AddMatrixParentQuestionDescription)
                    {
                        Question parentQ = ParentQuestion as Question;
                        if (parentQ != null)
                        {
                            //return parentQ.Description + Delimiter + bracketStart + d + bracketEnd;
                            return d;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(superfluity))
                    {
                        d = superfluity + Delimiter + bracketStart + d + bracketEnd;
                    }
                }
                if (AddSubFAParentQuestionAndSectorDescription
                    && (questiontype & Tabulation.QuestionType.FA_Sub) == Tabulation.QuestionType.FA_Sub)
                {
                    Question parentQ = ParentQuestion as Question;
                    if (parentQ != null)
                    {
                        return parentQ.Description2(scenarioDescription, AddMatrixParentQuestionDescription, false, Delimiter, bracketStart, bracketEnd)
                             + Delimiter + bracketStart + parentsector.Description + bracketEnd;
                    }
                }
                return d;
            }

            /// <summary>
            /// 既定の質問文を取得/設定するプロパティ<br />
            /// 設定は1度のみ可能
            /// </summary>
            public string OriginalDescription
            {
                get
                {
                    return originaldescription;
                }
                set
                {
                    if (originaldescription == null && !string.IsNullOrWhiteSpace(value))
                    {
                        // originaldescription = value;
                        setDescription(value, ref originalSuperfluity, ref originaldescription);
                    }
                }
            }

            /// <summary>
            /// 質問に紐付く選択肢コレクションを表すSectorsクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// <note>SAまたはMA質問の場合に有効</note>
            /// </summary>
            public ISectors Sectors
            {
                get
                {
                    if ((int)(questiontype & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) == 0)
                    {
                        return null;
                    }
                    if ((questiontype & Tabulation.QuestionType.MatrixChild) == Tabulation.QuestionType.MatrixChild)
                    {
						return sectors;
						//if (Collection != null && Collection.ParentQuestion != null)
      //                  {
						//	//return Collection.ParentQuestion.Sectors;
						//	return sectors;
						//}
						//else
      //                  {
      //                      return null;
      //                  }
                    }
                    else
                    {
                        return sectors;
                    }
                }
				set
				{
					sectors = value as Macromill.QCWeb.Question.Sectors;
				}
				
            }

            /// <summary>
            /// 集計結果を並べ替えるかどうかを示すフラグを取得/設定するプロパティ
            /// </summary>
            public bool DoSort
            {
                get
                {
                    if ((int)(questiontype & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) == 0)
                    {
                        return false;
                    }
                    if ((questiontype & Tabulation.QuestionType.MatrixChild) == Tabulation.QuestionType.MatrixChild)
                    {
                        if (Collection != null && Collection.ParentQuestion != null)
                        {
                            return Collection.ParentQuestion.DoSort;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return (questiontype & Tabulation.QuestionType.Sort) == Tabulation.QuestionType.Sort;
                    }
                }
                set
                {
                    if ((int)(questiontype & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) == 0
                            || (questiontype & Tabulation.QuestionType.MatrixChild) == Tabulation.QuestionType.MatrixChild)
                    {
                        return;
                    }
                    if (value)
                    {
                        questiontype |= Tabulation.QuestionType.Sort;
                    }
                    else
                    {
                        questiontype &= ~Tabulation.QuestionType.Sort;
                    }
                }
            }

            /// <summary>
            /// QC3での質問タイプを表すQCQuestionType列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public QCQuestionType QCQuestionType
            {
                get
                {
                    return qcquestiontype;
                }
                set
                {
                    qcquestiontype = value;
                }
            }

            /// <summary>
            /// 属性質問かどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool IsPropertyItem
            {
                get
                {
                    return (questiontype & Tabulation.QuestionType.Property) == Tabulation.QuestionType.Property;
                }
            }

            /// <summary>
            /// データ加工による新アイテムかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool IsNewItem
            {
                get
                {
                    return (int)(qcquestiontype & (QCQuestionType.NewItem | QCQuestionType.Analysis)) != 0;
                }
            }

            /// <summary>
            /// ちょっと加工の一時アイテムかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool IsTemporatyItem
            {
                get
                {
                    return (qcquestiontype & QCQuestionType.Temporary) == QCQuestionType.Temporary;
                }
            }

            /// <summary>
            /// 既存の通常アイテムかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool IsNormalItem
            {
                get
                {
                    return (int)(qcquestiontype & ~QCQuestionType.QuestionTypeAllBit) == 0;
                }
            }

            /// <summary>
            /// データ修正が行われているかどうかを示すフラグを取得/設定するプロパティ
            /// </summary>
            public bool IsDataEdit
            {
                get
                {
                    return dataEdit;
                }
                set
                {
                    this.dataEdit = value;
                }
            }

            /// <summary>
            /// データ加工の種類を表すコードを取得/設定するプロパティ
            /// </summary>
            public GlobalsCommonConstant.PROCESS_TYPE DataProcessType
            {
                get
                {
                    return this.dataProcessType;
                }
                set
                {
                    this.dataProcessType = value;
                }
            }

            /// <summary>
            /// 一時作成データ加工の種類を表すコードを取得/設定するプロパティ
            /// </summary>
            public GlobalsCommonConstant.TemporaryDataProcess TemporaryDataProcess
            {
                get
                {
                    return temporaryDataProcess;
                }
                set
                {
                    this.temporaryDataProcess = value;
                }
            }

            /// <summary>
            /// QC3での回答タイプを表すQCAnswerType列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public QCAnswerType QCAnswerType
            {
                get
                {
                    return qcanswertype;
                }
                set
                {
                    qcanswertype = value;
                }
            }

            /// <summary>
            /// QCWebでの集計に適した質問タイプを表すQuestionType列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public Tabulation.QuestionType QuestionType
            {
                get
                {
                    return questiontype;
                }
				set
				{
					questiontype = value;
				}
            }

            /// <summary>
            /// 子質問コレクションを表すQuestionsクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// <note>マトリクスの親質問の場合にのみ有効</note>
            /// </summary>
            public IQuestions ChildQuestions
            {
                get
                {
                    return childquestions;
                }
				set
				{
					childquestions = value as Macromill.QCWeb.Question.Questions;
				}
            }

            /// <summary>
            /// ローデータが入っているテーブル名を返す読み取り専用プロパティ
            /// </summary>
            public string TableName
            {
                get
                {
                    return tablename;
                }
                internal set
                {
                    tablename = value;
                }
            }

            /// <summary>
            /// ローデータが入っているカラム名を返す読み取り専用プロパティ
            /// </summary>
            public string ColumnName
            {
                get
                {
                    return columnname;
                }
                set
                {
                    columnname = value;
                }
            }

            /// <summary>
            /// 同一調査の先頭のローデータテーブル名を返す読み取り専用プロパティ
            /// </summary>
            public string TopTableName
            {
                get
                {
                    return toptablename;
                }
                set
                {
                    toptablename = value;
                }
            }

            /// <summary>
            /// 自身の親であるQuestionクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// <note>マトリクスの子質問の場合および付加質問の場合にのみ有効</note>
            /// </summary>
            public IQuestion ParentQuestion
            {
                get
                {
                    if ((questiontype & Tabulation.QuestionType.MatrixChild) == Tabulation.QuestionType.MatrixChild)
                    {
                        if (Collection == null)
                            return null;
                        return Collection.ParentQuestion;
                    }
                    if ((questiontype & Tabulation.QuestionType.FA_Sub) == Tabulation.QuestionType.FA_Sub)
                    {
                        if (parentsector == null)
                            return null;
                        return parentsector.ParentQuestion;
                    }
                    return null;
                }
            }

            /// <summary>
            /// 自身の親であるSectorクラスのインスタンスへの参照を取得/設定するプロパティ<br />
            /// 設定は1度のみ有効
            /// <note>付加質問の場合にのみ有効</note>
            /// </summary>
            public ISector ParentSector
            {
                get
                {
                    return parentsector;
                }
                set
                {
                    if (parentsector == null)
                    {
                        parentsector = value as Sectors.Sector;
                    }
                }
            }

            /// <summary>
            /// 自身が格納されているQuestionsクラスのインスタンスへの参照を取得/設定するプロパティ<br />
            /// 設定は1度のみ有効
            /// </summary>
            public IQuestions ParentCollection
            {
                get
                {
                    return Collection;
                }
				set
				{
					Collection = value as Questions;
				}
            }

            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                if (sectors != null)
                    sectors.Dispose();
                if (childquestions != null)
                    childquestions.Dispose();
                parentsector = null;
                Collection = null;
            }

            /// <summary>
            /// 最終更新日を返す読み取り専用プロパティ
            /// </summary>
            public DateTime LastUpdateDateTime
            {
                get
                {
                    if (IsDataEdit)
                    {
                        return dataEditDateTime;
                    }
                    else
                    {
                        return lastUpdateDateTime;
                    }
                }
                internal set
                {
                    lastUpdateDateTime = value;
                }
            }

            /// <summary>
            /// データ修正最終更新日をを設定するプロパティ
            /// 設定は一度のみ有効
            /// </summary>
            public DateTime DataEditDateTime
            {
                set
                {
                    if (dataEditDateTime == new DateTime(0))
                    {
                        dataEditDateTime = value;
                    }
                }
            }

            /// <summary>
            /// ローデータテキストファイルを取得するプロパティ
            /// </summary>
            public string RawdataTextFileName
            {
                get
                {
                    if (IsNewItem && IsTemporatyItem && TemporaryDataProcess == GlobalsCommonConstant.TemporaryDataProcess.GTSetting)
                    {
                        return GtMatrixBaseQuestion.RawdataTextFileName;
                    }
                    else
                    {
                        return id.ToString() + RawdataTextFileExtension;
                    }
                }
            }

            /// <summary>
            /// ローデータテキストファイル
            /// </summary>
            public string RawdataTextFileExtension
            {
                get
                {
                    if (IsNewItem && !IsTemporatyItem)
                    {
                        // データ加工アイテム
                        return ".dp";
                    }
                    else if (IsNewItem && IsTemporatyItem)
                    {
                        string extension = null;
                        switch (TemporaryDataProcess)
                        {
                            case GlobalsCommonConstant.TemporaryDataProcess.GTSetting:  // GT集計設定追加ファイル
                                extension = GtMatrixBaseQuestion.RawdataTextFileExtension;
                                break;
                            case GlobalsCommonConstant.TemporaryDataProcess.CategoryEdit: // カテゴリ出力編集ファイル
                                extension = ".dp2";
                                break;
                            default:
                                // 一時アイテムの判定に失敗しました。コード:{0}
                                throw new QCWebException("QCCMN03000007"
                                                         , new string[] { TemporaryDataProcess.GetHashCode().ToString() }
                                                         , GlobalsCommonConstant.LogLevel.FATAL, null);
                        }
                        return extension;
                    }
                    //else if (IsNormalItem && IsDataEdit)
                    else if (IsDataEdit)
                    {
                        // 通常アイテムでデータ修正あり
                        return ".dp";
                    }
                    else
                    {
                        // 通常アイテムでデータ修正なし
                        return ".txt";
                    }
                }
            }

            /// <summary>
            /// GT集計設定追加基準アイテムIDを取得/設定するプロパティ
            /// </summary>
            public decimal GtMatrixBaseItemId
            {
                get
                {
                    return gtMatrixBaseItemId;
                }
                set
                {
                    if (gtMatrixBaseItemId == (decimal)0)
                    {
                        gtMatrixBaseItemId = value;
                    }
                }
            }

            /// <summary>
            /// GT集計設定追加基準アイテムのQuestionクラスを取得/設定するプロパティ
            /// </summary>
            public Question GtMatrixBaseQuestion
            {
                get
                {
                    return gtMatrixBaseQuestion;
                }
                set
                {
                    gtMatrixBaseQuestion = value;
                }
            }

            /// <summary>
            /// 加工元シナリオIDを取得/設定するプロパティ
            /// (GT集計設定追加、カテゴリ出力編集で作られたアイテムのみ設定される)
            /// </summary>
            public decimal? CategoryEditID
            {
                get
                {
                    return categoryEditID;
                }
                set
                {
                    categoryEditID = value;
                }
            }

            private void setQC3BlankForce(ref bool arg)
            {
                if (!arg)
                {
                    switch (qcquestiontype & QCWeb.Question.QCQuestionType.QuestionTypeExAllBit)
                    {
                        case QCWeb.Question.QCQuestionType.NewItem:
                        case QCWeb.Question.QCQuestionType.Analysis:
                        case QCWeb.Question.QCQuestionType.Temporary:
                            arg = true;
                            break;
                    }
                }
            }

            /// <summary>
            /// QC3ファイルの質問番号が空であったかを取得/設定するプロパティ
            /// </summary>
            public bool IsQC3BlankNumber
            {
                get
                {
                    setQC3BlankForce(ref qc3BlankNumber);
                    return qc3BlankNumber;
                }
                set
                {
                    setQC3BlankForce(ref value);
                    qc3BlankNumber = value;
                }
            }

        }

        private Question parentquestion = null;
        private bool includeQuestion = false;

        /// <summary>アイテム情報TBLBhv</summary>
        protected TItemInfoBhv tItemInfoBhv = null;
        /// <summary>カテゴリ情報TBLBhv</summary>
        protected TCategoryInfoBhv tCategoryInfoBhv = null;
        /// <summary>マトリックス情報Bhv</summary>
        protected TMatrixInfoBhv tMatrixInfoBhv = null;
        /// <summary>データ修正対象アイテムBhv</summary>
        protected TEditTargetItemBhv tEditTargetItemBhv = null;
        /// <summary>データ加工リストBhv</summary>
        protected TDataEditListBhv tDataEditListBhv = null;
        /// <summary>GT集計設定追加Bhv</summary>
        protected TGtMatrixInfoBhv tGtMatrixInfoBhv = null;
        /// <summary>GT集計設定子アイテムTBLビヘイビア</summary>
        protected TGtMatrixChildBhv tGtMatrixChildBhv = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="qcwebid">検索するQCWebID</param>
        /// <param name="sourceDivArray">検索対象としたいOriginalデータ区分リスト</param>
        public Questions(decimal qcwebid, string[] sourceDivArray = null)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Message mainErrorMessage = new Message(Common.Constants.CommonMessageIndex.GetItemInformationsWithCauseFatalMessageIndex);
            QuillInjector.GetInstance().Inject(this);
            _log.Debug("Inject " + sw.ElapsedMilliseconds + "ms");

            // DB問い合わせ
            #region サンプルSQL
            /*
            SELECT Itm1.Item_ID, Itm1.Item_Name, Itm1.ItemNo
                 , Itm1.Item_Type, Item1.Source_Div
                 , Itm1.Answer_Type, Itm1.Matrix_Div
                 , Itm1.Lv1Title, Itm1.Lv2Title
                 , Itm1.Original_Lv1Title, Itm1.Original_Lv2Title
                 , Itm1.Table_Name, Itm1.Column_Name, Itm1.Top_Table_Name
                 , Itm1.Sort_Flag, Itm1.Sort_Range
                 , Cate1.Category_No
                 , Cate1.Category_Name, Cate1.Original_Category_Name
                 , Cate1.Weight_Value
                 , Itm2.Item_ID, Cate2.Category_No
            FROM   (
                   (
                   T_Item_Info Itm1 LEFT JOIN T_Category_Info Cate1
                       ON Cate1.Item_ID = Itm1.Item_ID
                   ) LEFT JOIN T_Category_Info Cate2
                       ON Itm1.Parent_Category_ID = Cate2.Category_ID
                   ) LEFT JOIN T_Item_Info Itm2
                       ON Cate2.Item_ID = Itm2.Item_ID
            WHERE  Itm1.QCWebID = %qcwebid%
                     AND Itm1.Status = 1
                     AND Itm1.Matrix_Div != 2
                     AND Itm1.Source_Div != 2
            ORDER BY Itm1.Sort_Number, Cate1.Category_No;
            */
            // EOF→エラースロー
            // while (!EOF)
            #endregion
            //TItemInfoQuestionsPmb pmb = new TItemInfoQuestionsPmb();
            //pmb.QCWebId = qcwebid;
            //ListResultBean<TItemInfoQuestions> list =
            //    tItemInfoBhv.OutsideSql().SelectList<TItemInfoQuestions>(TItemInfoBhv.PATH_SelectQuestionsInfo, pmb);

            sw.Restart();
            TItemInfoCB itemInfoCB = new TItemInfoCB();
            itemInfoCB.SetupSelect_TMatrixInfo().WithTCategoryInfo();
            itemInfoCB.SetupSelect_TTableControl();
            itemInfoCB.SetupSelect_TScenarioTotalization();
            itemInfoCB.SetupSelect_TDataEditList();
            itemInfoCB.Query().QueryTTableControl().InnerJoin();
            itemInfoCB.Query().SetQcwebid_Equal(qcwebid);
            List<int?> matrixDivList = new List<int?>();
            matrixDivList.Add(int.Parse(CDef.MatrixType.NormalItem.Code));
            matrixDivList.Add(int.Parse(CDef.MatrixType.MatrixParent.Code));
            matrixDivList.Add(int.Parse(CDef.MatrixType.SubFA.Code));
            itemInfoCB.Query().SetMatrixDiv_InScope(matrixDivList);
            if (sourceDivArray != null)
            {
                List<string> sourceDivList = new List<string>(sourceDivArray);
                itemInfoCB.Query().SetSourceDiv_InScope(sourceDivList);
            }
            itemInfoCB.Query().SetStatus_Equal_Effective();
            itemInfoCB.Query().AddOrderBy_SortNumber_Asc();
            itemInfoCB.Query().AddOrderBy_ItemInfoId_Asc();
            ListResultBean<TItemInfo> list = tItemInfoBhv.SelectList(itemInfoCB);
            if (list.Count <= 0)
            {
                throw new QCWebException(mainErrorMessage
                            , GlobalsCommonConstant.LogLevel.FATAL
                            , GetResource.GetLogMessage(Common.Constants.NOT_EXIST_ITEM_INFORMATION_AT_QCWEB_ID_MESSAGE_ID, qcwebid.ToString()));
            }

            tItemInfoBhv.LoadTCategoryInfoList(list, categoryInfoCB => categoryInfoCB.Query().AddOrderBy_CategoryNo_Asc());

            // データ修正情報の検索
            Dictionary<decimal, DateTime> dataEditMap = new Dictionary<decimal, DateTime>();
            ListResultBean<TEditTargetItem> listBean = GetDataEditList(qcwebid);
            foreach (TEditTargetItem item in listBean)
            {
                decimal itemid = (decimal)item.TargetItemId;
                if (dataEditMap.ContainsKey(itemid))
                {
                    dataEditMap.Remove(itemid);
                }
                dataEditMap.Add(itemid, (DateTime)item.TEditData.TDataEditList.LastUpdateDatetime);
            }

            // GT集計設定追加情報の検索(加工後のアイテムID/基準アイテムID)
            Dictionary<decimal, decimal> gtMatrixMap = new Dictionary<decimal, decimal>();
            ListResultBean<TGtMatrixInfo> gtMatrixList = GetGtMatrixInfoList(qcwebid);
            foreach (TGtMatrixInfo gtMatrix in gtMatrixList)
            {
                gtMatrixMap.Add((decimal)gtMatrix.NewItemId, (decimal)gtMatrix.BaseItemId);
            }

            _log.Debug("Select1 " + sw.ElapsedMilliseconds + "ms");

            sw.Restart();
            foreach (TItemInfo entity in list)
            {
                decimal id = (decimal)entity.ItemInfoId;
                Question question = this[id] as Question;
                if (question == null)
                {
                    QCQuestionType qcqType = QCQuestionType.None;
                    GlobalsCommonConstant.TemporaryDataProcess temporaryDataProcess = GlobalsCommonConstant.TemporaryDataProcess.None;
                    decimal baseItemId = (decimal)0;
                    Question gtMatrixBaseQuestion = null;

                    if (!string.IsNullOrEmpty(entity.ItemType))
                    {
                        qcqType = (QCQuestionType)System.Enum.Parse(typeof(QCQuestionType), entity.ItemType);
                    }
                    char sourcediv = entity.SourceDiv.ToCharArray()[0];
                    if (sourcediv == '1')
                        qcqType |= QCQuestionType.NewItem;
                    if (sourcediv == '2')
                    {
                        qcqType |= QCQuestionType.Temporary;

                        if (entity.TScenarioTotalization.ScenarioTypeAsScenarioType == CDef.ScenarioType.GT)
                        {
                            temporaryDataProcess = GlobalsCommonConstant.TemporaryDataProcess.GTSetting;
                            baseItemId = gtMatrixMap[id];
                            gtMatrixBaseQuestion = (Question)this[baseItemId];
                            if (gtMatrixBaseQuestion == null)
                            {
                                Questions q = new Questions(0, baseItemId);
                                gtMatrixBaseQuestion = (Question)q[baseItemId];
                            }
                        }
                        else if (entity.TScenarioTotalization.ScenarioTypeAsScenarioType == CDef.ScenarioType.CROSS)
                        {
                            temporaryDataProcess = GlobalsCommonConstant.TemporaryDataProcess.CategoryEdit;
                        }
                    }

                    if (entity.IsMultivariateFlagTrue)
                    {
                        // 多変量解析
                        qcqType |= QCQuestionType.AnaAtQC3;
                    }
                    if (entity.IsNewAtQc3FlagTrue)
                    {
                        // QC3新アイテム
                        qcqType |= QCQuestionType.NewAtQC3;
                    }

                    question = new Question(this, id)
                    {
                        QCWebID = qcwebid,
                        Name = entity.ItemName,
                        Number = entity.Itemno,
                        QCQuestionType = qcqType,
                        QCAnswerType = (QCAnswerType)int.Parse(entity.AnswerType),
                        TableName = entity.TableName,
                        ColumnName = entity.ColumnName,
                        TopTableName = entity.TTableControl.BaseTableName,
                        TemporaryDataProcess = temporaryDataProcess,
                        GtMatrixBaseItemId = baseItemId,
                        GtMatrixBaseQuestion = gtMatrixBaseQuestion,
                        CategoryEditID = entity.CategoryEditId,
                        LastUpdateDateTime = entity.LastUpdateDatetime == null ? new DateTime(0) : (DateTime)entity.LastUpdateDatetime
                    };
                    if (question.QCQuestionType != QCQuestionType.None)
                    {
                        includeQuestion = true;
                    }
                    QCMatrixCode matricsCode = (QCMatrixCode)entity.MatrixDiv;
                    //if (matricsCode == QCMatrixCode.MatrixParent)
                    //{
                    //    question.Description = entity.Lv1title;
                    //    question.OriginalDescription = entity.OriginalLv1title;
                    //}
                    //else
                    //{
                    //    question.Description = entity.Lv2title;
                    //    question.OriginalDescription = entity.OriginalLv2title;
                    //}
                    if (matricsCode == QCMatrixCode.MatrixParent) {
                        question.Description = MakeDescription("", entity.Lv1title);
                        question.OriginalDescription = MakeDescription("", entity.OriginalLv1title);
                    } else {
                        question.Description = MakeDescription(entity.Lv1title, entity.Lv2title);
                        question.OriginalDescription = MakeDescription(entity.OriginalLv1title, entity.OriginalLv2title);
                    }

                    if (matricsCode == QCMatrixCode.SubFA)
                    {
                        decimal? parentsectorQID = entity.TMatrixInfo.ItemInfoId;
                        if (parentsectorQID != null)
                        {
                            // 付加FA
                            decimal qid = (decimal)parentsectorQID;
                            decimal secNo = (decimal)entity.TMatrixInfo.AddFaCategoryInfoId;
                            Question parentQ = this[qid] as Question;
                            if (parentQ == null)
                            {
                                // マト子の可能性があるのため、マト親のアイテムIDを検索する
                                TMatrixInfoCB matrixInfoCB = new TMatrixInfoCB();
                                matrixInfoCB.Query().SetChildItemInfoId_Equal(qid);
                                TMatrixInfo matrixInfo = tMatrixInfoBhv.SelectEntity(matrixInfoCB);
                                if (matrixInfo == null)
                                {
                                    throw new QCWebException(mainErrorMessage
                                                , GlobalsCommonConstant.LogLevel.FATAL
                                                , GetResource.GetLogMessage(Common.Constants.NOT_EXIST_PARENT_ITEM_INFORMATION_OF_ADDED_FA_MESSAGE_ID, id.ToString(), qid.ToString()));
                                }
                                parentQ = this[(decimal)matrixInfo.ItemInfoId] as Question;
                                if (parentQ == null)
                                {
                                    throw new QCWebException(mainErrorMessage
                                                , GlobalsCommonConstant.LogLevel.FATAL
                                                , GetResource.GetLogMessage(Common.Constants.NOT_EXIST_PARENT_ITEM_INFORMATION_OF_ADDED_FA_MESSAGE_ID, id.ToString(), qid.ToString()));
                                }
                            }
                            Sectors.Sector parentsector =
                                ((Sectors)parentQ.Sectors)[(int)entity.TMatrixInfo.TCategoryInfo.CategoryNo] as Sectors.Sector;
                            if (parentsector == null)
                            {
                                throw new QCWebException(mainErrorMessage
                                            , GlobalsCommonConstant.LogLevel.FATAL
                                            , GetResource.GetLogMessage(Common.Constants.NOT_EXIST_PARENT_SECTOR_INFORMATION_OF_ADDED_FA_MESSAGE_ID, id.ToString(), secNo.ToString()));
                            }
                            parentsector.AddedQuestion = question;
                        }
                        else
                        {
                            throw new QCWebException(mainErrorMessage
                                        , GlobalsCommonConstant.LogLevel.FATAL
                                        , GetResource.GetLogMessage(Common.Constants.NOT_EXIST_PARENT_ITEM_INFORMATION_OF_ADDED_FA_MESSAGE_ID, id.ToString(), string.Empty));
                        }
                    }
                    question.SetQuestionType(matricsCode);
                    question.DoSort = entity.SortFlag == 0 ? false : true;
                    // QC3での質問番号のブランク判定
                    question.IsQC3BlankNumber = string.IsNullOrEmpty(entity.Itemno);

                    // データ修正の有無
                    question.IsDataEdit = dataEditMap.ContainsKey(id);
                    if (question.IsDataEdit)
                    {
                        question.DataEditDateTime = dataEditMap[id];
                    }

                    // データ加工の場合、データ加工の種別を取得する
                    if (question.IsNewItem)
                    {
                        if (entity.DataEditId != null)
                        {
                            question.DataProcessType = (GlobalsCommonConstant.PROCESS_TYPE)entity.TDataEditList.EditMenuMasterId;
                        }
                    }

                    if ((entity.IsAnswerTypeSA || entity.IsAnswerTypeMA) && (matricsCode == QCMatrixCode.Normal || matricsCode == QCMatrixCode.MatrixParent))
                    {
                        foreach (TCategoryInfo cateEntity in entity.TCategoryInfoList)
                        {
                            int? categoryNo = cateEntity.CategoryNo;
                            if (categoryNo == null ^ (int)(question.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) == 0)
                            {
                                Tabulation.QuestionType qType = question.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.N | Tabulation.QuestionType.FA);
                                throw new QCWebException(mainErrorMessage
                                            , GlobalsCommonConstant.LogLevel.FATAL
                                            , GetResource.GetLogMessage(
                                                categoryNo == null ? Common.Constants.NOT_EXIST_CATEGORY_INFORMATION_MESSAGE_ID : Common.Constants.EXIST_UNJUST_CATEGORY_INFORMATIONS_MESSAGE_ID
                                                , id.ToString(), qType.ToString())
                                            );
                            }
                            if (categoryNo != null)
                            {
                                int cateNo = (int)categoryNo;
                                string cateDesc = cateEntity.CategoryName;
                                string cateOrgDesc = cateEntity.OriginalCategoryName;
                                string wt = cateEntity.WeightValue;
                                int maxSortIdx = entity.SortRange == null ? 0 : (int)entity.SortRange;
                                bool isunsort = cateNo > maxSortIdx;
                                (question.Sectors as Sectors).Add(cateNo, cateDesc, cateOrgDesc, wt, isunsort);
                            }
                        }
                    }
                    this.Add(id.ToString(), question);
                }
            }
            _log.Debug("Item1 " + sw.ElapsedMilliseconds + "ms");

            #region サンプルSQL
            /*
            SELECT Item_ID, Item_Name
                 , Lv2Title, Original_Lv2Title
                 , Table_Name, Column_Name, Top_Table_Name
                 , Parent_Item_ID
            FROM   T_Item_Info
            WHERE  QCWebID = %qcwebid% AND Status = 1 AND Matrix_Div = 2 AND Source_Div != 2
            ORDER BY Sort_Number;
            */
            // while (!EOF)
            #endregion

            sw.Restart();
            TItemInfoCB tItemInfoCB = new TItemInfoCB();
            tItemInfoCB.SetupSelect_TMatrixInfo();
            tItemInfoCB.SetupSelect_TTableControl();
            tItemInfoCB.SetupSelect_TDataEditList();
            tItemInfoCB.Query().QueryTTableControl().InnerJoin();
            tItemInfoCB.Query().SetQcwebid_Equal(qcwebid);
            tItemInfoCB.Query().SetStatus_Equal_Effective();
            List<int?> matrixDivChildList = new List<int?>();
            matrixDivChildList.Add(int.Parse(CDef.MatrixType.MatrixChild.Code));
            matrixDivChildList.Add(int.Parse(CDef.MatrixType.FirstChild.Code));
            tItemInfoCB.Query().SetMatrixDiv_InScope(matrixDivChildList);
            if (sourceDivArray != null)
            {
                List<string> sourceDivList = new List<string>(sourceDivArray);
                tItemInfoCB.Query().SetSourceDiv_InScope(sourceDivList);
            }
            tItemInfoCB.Query().AddOrderBy_SortNumber_Asc();
            tItemInfoCB.Query().QueryTMatrixInfo().AddOrderBy_ChildItemInfoId_Asc();
            ListResultBean<TItemInfo> itemList = tItemInfoBhv.SelectList(tItemInfoCB);
            _log.Debug("Select2 " + sw.ElapsedMilliseconds + "ms");
            _log.Debug("Select2 " + itemList.Count + "Recode");

            sw.Restart();
            foreach (TItemInfo entity in itemList)
            {
                decimal id = (decimal)entity.ItemInfoId;
                decimal parentId = (decimal)entity.TMatrixInfo.ItemInfoId;
                Question parentQ = this[parentId] as Question;
                if (parentQ == null || (parentQ.QuestionType & Tabulation.QuestionType.MatrixParent) != Tabulation.QuestionType.MatrixParent
                                    || parentQ.ChildQuestions == null)
                {
                    throw new QCWebException(mainErrorMessage
                            , GlobalsCommonConstant.LogLevel.FATAL
                            , GetResource.GetLogMessage(Common.Constants.UNJUST_MATRIX_PARENT_INFORMATION_MESSAGE_ID, id.ToString(), parentId.ToString()));
                }

                Question question = new Question(parentQ.ChildQuestions as Questions, id)
                {
                    QCWebID = qcwebid,
                    Name = entity.ItemName,
                    QCAnswerType = (QCAnswerType)int.Parse(entity.AnswerType),
                    Description = MakeDescription("", entity.Lv2title),
                    OriginalDescription = MakeDescription("", entity.OriginalLv2title),
                    TableName = entity.TableName,
                    ColumnName = entity.ColumnName,
                    TopTableName = entity.TTableControl.BaseTableName,
                    CategoryEditID = entity.CategoryEditId,
                    LastUpdateDateTime = entity.LastUpdateDatetime == null ? new DateTime(0) : (DateTime)entity.LastUpdateDatetime
                };
                question.SetQuestionType((QCMatrixCode)entity.MatrixDiv);

                // データ修正の有無
                question.IsDataEdit = dataEditMap.ContainsKey(id);
                if (question.IsDataEdit)
                {
                    question.DataEditDateTime = dataEditMap[id];
                }

                // データ加工の場合、データ加工の種別を取得する
                if (question.IsNewItem)
                {
                    if (entity.DataEditId != null)
                    {
                        question.DataProcessType = (GlobalsCommonConstant.PROCESS_TYPE)entity.TDataEditList.EditMenuMasterId;
                    }
                }
                parentQ.ChildQuestions.Add(id.ToString(), question);
            }

            // GT集計設定追加の子供を抽出
            bool procFlag = false;
            if (sourceDivArray == null)
            {
                procFlag = true;
            }
            else
            {
                foreach (string v in sourceDivArray)
                {
                    if (v == CDef.SourceDiv.ScenarioDataEdit.Code)
                    {
                        procFlag = true;
                        break;
                    }
                }
            }

            if (procFlag)
            {
                TGtMatrixChildCB tGtMatrixChildCB = new TGtMatrixChildCB();
                tGtMatrixChildCB.SetupSelect_TGtMatrixInfo().WithTScenarioTotalization();
                tGtMatrixChildCB.SetupSelect_TItemInfo().WithTTableControl();
                tGtMatrixChildCB.SetupSelect_TItemInfo().WithTDataEditList();
                tGtMatrixChildCB.Query().QueryTGtMatrixInfo().QueryTScenarioTotalization().SetQcwebid_Equal(qcwebid);
                //tGtMatrixChildCB.Query().AddOrderBy_GtMatrixChildid_Asc();
                tGtMatrixChildCB.Query().QueryTItemInfo().AddOrderBy_SortNumber_Asc();
                ListResultBean<TGtMatrixChild> gtMatrixChildList = tGtMatrixChildBhv.SelectList(tGtMatrixChildCB);

                foreach (TGtMatrixChild gtMatrixChild in gtMatrixChildList)
                {
                    decimal childID = (decimal)gtMatrixChild.ChildItemId;
                    decimal parentId = (decimal)gtMatrixChild.TGtMatrixInfo.NewItemId;
                    Question parentQ = this[parentId] as Question;
                    if (parentQ == null
                        || (parentQ.QuestionType & Tabulation.QuestionType.MatrixParent) != Tabulation.QuestionType.MatrixParent
                        || parentQ.ChildQuestions == null)
                    {
                        throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(Common.Constants.UNJUST_MATRIX_PARENT_INFORMATION_MESSAGE_ID, qcwebid.ToString(), parentId.ToString()));
                    }
                    Question childQ = new Question(parentQ.ChildQuestions as Questions, childID)
                    {
                        QCWebID = qcwebid,
                        Name = gtMatrixChild.TItemInfo.ItemName,
                        QCAnswerType = (QCAnswerType)int.Parse(gtMatrixChild.TItemInfo.AnswerType),
                        Description = MakeDescription("", gtMatrixChild.TItemInfo.Lv2title),
                        OriginalDescription = MakeDescription("", gtMatrixChild.TItemInfo.OriginalLv2title),
                        TableName = gtMatrixChild.TItemInfo.TableName,
                        ColumnName = gtMatrixChild.TItemInfo.ColumnName,
                        TopTableName = gtMatrixChild.TItemInfo.TTableControl.BaseTableName,
                        CategoryEditID = gtMatrixChild.TItemInfo.CategoryEditId,
                        LastUpdateDateTime = gtMatrixChild.TItemInfo.LastUpdateDatetime == null ? new DateTime(0) : (DateTime)gtMatrixChild.TItemInfo.LastUpdateDatetime
                    };
                    childQ.SetQuestionType(QCMatrixCode.MatrixChild);

                    // データ修正の有無
                    ListResultBean<TEditTargetItem> dataEditList = GetDataEditList(qcwebid, childID);
                    childQ.IsDataEdit = dataEditList.Count > 0;
                    if (childQ.IsDataEdit)
                    {
                        childQ.DataEditDateTime = (DateTime)dataEditList[0].TEditData.TDataEditList.LastUpdateDatetime;
                    }

                    // データ加工の場合、データ加工の種別を取得する
                    if (childQ.IsNewItem)
                    {
                        if (gtMatrixChild.TItemInfo.DataEditId != null)
                        {
                            childQ.DataProcessType = (GlobalsCommonConstant.PROCESS_TYPE)gtMatrixChild.TItemInfo.TDataEditList.EditMenuMasterId;
                        }
                    }

                    parentQ.ChildQuestions.Add(childID.ToString(), childQ);
                }
            }
            _log.Debug("Item2 " + sw.ElapsedMilliseconds + "ms");
        }

        /// <summary>
        /// 要素にQuestionクラスのインスタンスへの参照を1つ追加するメソッド
        /// </summary>
        /// <param name="id">追加する質問のアイテムID</param>
        public void AddOne(decimal id)
        {
            if (Contains(id))
                return;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Message mainErrorMessage = new Message(Common.Constants.CommonMessageIndex.GetItemInformationsWithCauseFatalMessageIndex);
            QuillInjector.GetInstance().Inject(this);
            _log.Debug("Inject " + sw.ElapsedMilliseconds + "ms");

            // DB問い合わせ
            #region サンプルSQL
            /*
            SELECT T_Item_Info.Item_Name, T_Item_Info.ItemNo
                 , T_Item_Info.Item_Type, T_Item_Info.Source_Div
                 , T_Item_Info.Answer_Type, T_Item_Info.Matrix_Div
                 , T_Item_Info.Lv1Title, T_Item_Info.Lv2Title
                 , T_Item_Info.Original_Lv1Title, T_Item_Info.Original_Lv2Title
                 , T_Item_Info.Table_Name, T_Item_Info.Column_Name, T_Item_Info.Top_Table_Name
                 , T_Item_Info.Sort_Flag, T_Item_Info.Sort_Range
                 , T_Item_Info.Parent_Item_ID
                 , T_Category_Info.Category_No
                 , T_Category_Info.Category_Name, T_Category_Info.Original_Category_Name
                 , T_Category_Info.Weight_Value
            FROM   T_Item_Info LEFT JOIN T_Category_Info
                       ON T_Category_Info.Item_ID = T_Item_Info.Item_ID
            WHERE  T_Item_Info.Status = 1
                       AND T_Item_Info.Item_ID = %id%
                       AND T_Item_Info.Source_Div != 2
            ORDER BY T_Category_Info.Category_No;
            */
            // EOF→エラースロー
            #endregion

            #region コメント
            //TCategoryInfoCB countCB = new TCategoryInfoCB();
            //countCB.Query().SetItemInfoId_Equal(id);
            //int count = tCategoryInfoBhv.SelectCount(countCB);

            //ListResultBean<TCategoryInfo> categoryList = null;
            //// 先頭レコード→カレント
            //TItemInfo entity = null;
            //if (count <= 0) {
            //    TItemInfoCB cb = new TItemInfoCB();
            //    cb.SetupSelect_TMatrixInfo();
            //    cb.SetupSelect_TTableControl();
            //    cb.Query().QueryTTableControl().InnerJoin();
            //    cb.Query().SetItemInfoId_Equal(id);
            //    cb.Query().SetStatus_Equal_Effective();
            //    // TODO:GT集計でアイテムを作るとデータ参照がエラーになるため暫定的にコメントとする
            //    //cb.Query().SetSourceDiv_NotEqual_ScenarioDataEdit();
            //    ListResultBean<TItemInfo> list = tItemInfoBhv.SelectList(cb);
            //    if (list.Count <= 0)
            //        throw new QCWebException("アイテム情報、カテゴリ情報が存在しません。");
            //    entity = list[0];

            //} else {
            //    TCategoryInfoCB tCategoryInfoCB = new TCategoryInfoCB();
            //    tCategoryInfoCB.SetupSelect_TItemInfo().WithTMatrixInfo();
            //    tCategoryInfoCB.SetupSelect_TItemInfo().WithTTableControl();
            //    tCategoryInfoCB.Query().QueryTItemInfo().QueryTTableControl().InnerJoin();
            //    tCategoryInfoCB.Query().QueryTItemInfo().SetStatus_Equal_Effective();
            //    tCategoryInfoCB.Query().SetItemInfoId_Equal(id);
            //    // TODO:GT集計でアイテムを作るとデータ参照がエラーになるため暫定的にコメントとする
            //    //tCategoryInfoCB.Query().QueryTItemInfo().SetSourceDiv_NotEqual_ScenarioDataEdit();
            //    tCategoryInfoCB.Query().AddOrderBy_CategoryInfoId_Asc();
            //    categoryList = tCategoryInfoBhv.SelectList(tCategoryInfoCB);
            //    if (categoryList.Count <= 0)
            //        throw new QCWebException("アイテム情報、カテゴリ情報がありません。");
            //    entity = categoryList[0].TItemInfo;
            //}
            #endregion
            //TItemInfoQuestionsCategoryPmb pmb = new TItemInfoQuestionsCategoryPmb();
            //pmb.ItemInfoId = id;
            //ListResultBean<TItemInfoQuestionsCategory> list =
            //    tItemInfoBhv.OutsideSql().SelectList<TItemInfoQuestionsCategory>(TItemInfoBhv.PATH_SelectQuestionsCategoryInfo, pmb);

            sw.Restart();
            TItemInfoCB itemInfoCB = new TItemInfoCB();
            itemInfoCB.SetupSelect_TMatrixInfo();
            itemInfoCB.SetupSelect_TTableControl();
            itemInfoCB.SetupSelect_TScenarioTotalization();
            itemInfoCB.SetupSelect_TDataEditList();
            itemInfoCB.Query().QueryTTableControl().InnerJoin();
            itemInfoCB.Query().SetItemInfoId_Equal(id);
            itemInfoCB.Query().SetStatus_Equal_Effective();
            ListResultBean<TItemInfo> itemList = tItemInfoBhv.SelectList(itemInfoCB);
            if (itemList.Count == 0)
            {
                throw new QCWebException(mainErrorMessage
                            , GlobalsCommonConstant.LogLevel.FATAL
                            , GetResource.GetLogMessage(Common.Constants.NOT_EXIST_ITEM_INFORMATION_AT_ITEM_ID_MESSAGE_ID, id.ToString()));
            }
            TItemInfo entity = itemList[0];

            QCMatrixCode matricsCode = (QCMatrixCode)entity.MatrixDiv;
            if (matricsCode == QCMatrixCode.MatrixChild || matricsCode == QCMatrixCode.FirstChild)
            {
                decimal parentID = (decimal)entity.TMatrixInfo.ItemInfoId;
                AddOne(parentID);
                return;
            }

            // カテゴリ情報の検索
            if (matricsCode == QCMatrixCode.MatrixParent)
            {
                tItemInfoBhv.LoadTCategoryInfoList(entity, categoryInfoCB => categoryInfoCB.Query().AddOrderBy_CategoryNo_Asc());
            }
            else
            {
                TCategoryInfoCB subcb = new TCategoryInfoCB();
                subcb.Query().SetItemInfoId_Equal(entity.ItemInfoId);
                subcb.Query().AddOrderBy_CategoryNo_Asc();
                entity.TCategoryInfoList = tCategoryInfoBhv.SelectList(subcb);
            }

            // データ修正情報の検索
            ListResultBean<TEditTargetItem> listBean = GetDataEditList((decimal)entity.Qcwebid, (decimal)entity.ItemInfoId);
            bool dataEditFlg = listBean.Count > 0;

            // GT集計設定追加情報の検索(加工後のアイテムID/基準アイテムID)
            ListResultBean<TGtMatrixInfo> gtMatrixList = GetGtMatrixInfoList((decimal)entity.Qcwebid, (decimal)entity.ItemInfoId);

            _log.Debug("Select1 " + sw.ElapsedMilliseconds + "ms");

            sw.Restart();
            QCQuestionType qcqType = QCQuestionType.None;
            GlobalsCommonConstant.TemporaryDataProcess temporaryDataProcess = GlobalsCommonConstant.TemporaryDataProcess.None;
            decimal baseItemId = (decimal)0;
            Question gtMatrixBaseQuestion = null;

            if (!string.IsNullOrEmpty(entity.ItemType))
            {
                qcqType = (QCQuestionType)System.Enum.Parse(typeof(QCQuestionType), entity.ItemType);
            }

            char sourcediv = entity.SourceDiv.ToCharArray()[0];
            if (sourcediv == '1')
                qcqType |= QCQuestionType.NewItem;
            if (sourcediv == '2')
            {
                qcqType |= QCQuestionType.Temporary;

                if (entity.TScenarioTotalization.ScenarioTypeAsScenarioType == CDef.ScenarioType.GT)
                {
                    temporaryDataProcess = GlobalsCommonConstant.TemporaryDataProcess.GTSetting;
                    baseItemId = (decimal)gtMatrixList[0].BaseItemId;
                    gtMatrixBaseQuestion = (Question)this[baseItemId];
                    if (gtMatrixBaseQuestion == null)
                    {
                        Questions q = new Questions(0, baseItemId);
                        gtMatrixBaseQuestion = (Question)q[baseItemId];
                    }
                }
                else if (entity.TScenarioTotalization.ScenarioTypeAsScenarioType == CDef.ScenarioType.CROSS)
                {
                    temporaryDataProcess = GlobalsCommonConstant.TemporaryDataProcess.CategoryEdit;
                }
            }

            if (entity.IsMultivariateFlagTrue)
            {
                // 多変量解析
                qcqType |= QCQuestionType.AnaAtQC3;
            }
            if (entity.IsNewAtQc3FlagTrue)
            {
                // QC3新アイテム
                qcqType |= QCQuestionType.NewAtQC3;
            }

            decimal qcwebid = (decimal)entity.Qcwebid;
            Question question = new Question(this, id)
            {
                QCWebID = qcwebid,
                Name = entity.ItemName,
                Number = entity.Itemno,
                QCQuestionType = qcqType,
                QCAnswerType = (QCAnswerType)int.Parse(entity.AnswerType),
                DoSort = Convert.ToBoolean(entity.SortFlag),
                TableName = entity.TableName,
                ColumnName = entity.ColumnName,
                TopTableName = entity.TTableControl.BaseTableName,
                TemporaryDataProcess = temporaryDataProcess,
                GtMatrixBaseItemId = baseItemId,
                GtMatrixBaseQuestion = gtMatrixBaseQuestion,
                CategoryEditID = entity.CategoryEditId,
                LastUpdateDateTime = entity.LastUpdateDatetime == null ? new DateTime(0) : (DateTime)entity.LastUpdateDatetime
            };

            if (matricsCode == QCMatrixCode.MatrixParent) {
                question.Description = MakeDescription("", entity.Lv1title);
                question.OriginalDescription = MakeDescription("", entity.OriginalLv1title);
            } else {
                question.Description = MakeDescription(entity.Lv1title, entity.Lv2title);
                question.OriginalDescription = MakeDescription(entity.OriginalLv1title, entity.OriginalLv2title);
            }

            // QC3での質問番号のブランク判定
            question.IsQC3BlankNumber = string.IsNullOrEmpty(entity.Itemno);

            // データ修正の有無
            question.IsDataEdit = dataEditFlg;
            if (question.IsDataEdit)
            {
                question.DataEditDateTime = (DateTime)listBean[0].TEditData.TDataEditList.LastUpdateDatetime;
            }

            // データ加工の場合、データ加工の種別を取得する
            if (question.IsNewItem)
            {
                if (entity.DataEditId != null)
                {
                    question.DataProcessType = (GlobalsCommonConstant.PROCESS_TYPE)entity.TDataEditList.EditMenuMasterId;
                }
            }

            if (matricsCode == QCMatrixCode.SubFA)
            {
                matricsCode = QCMatrixCode.Normal;
                question.QCQuestionType = QCQuestionType.FAL;
                decimal? parentsectorQID = entity.TMatrixInfo.ItemInfoId;
                if (parentsectorQID != null)
                {
                    do
                    {
                        // 付加FA
                        decimal qid = (decimal)parentsectorQID;
                        decimal secNo = (decimal)entity.TMatrixInfo.AddFaCategoryInfoId;
                        Question parentQ = this[qid] as Question;
                        if (parentQ == null) break;
                        Sectors.Sector parentsector =
                            (parentQ.Sectors as Sectors)[(int)entity.TMatrixInfo.TCategoryInfo.CategoryNo] as Sectors.Sector;
                        if (parentsector == null) break;
                        matricsCode = QCMatrixCode.SubFA;
                        question.QCQuestionType = QCQuestionType.None;
                        parentsector.AddedQuestion = question;
                    } while (false);
                }
            }
            question.SetQuestionType(matricsCode);
            this.Add(id.ToString(), question);
            foreach (TCategoryInfo cateEntity in entity.TCategoryInfoList)
            {
                int? categoryNo = cateEntity.CategoryNo;
                if (categoryNo == null ^ (int)(question.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) == 0)
                {
                    Tabulation.QuestionType qType = question.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.N | Tabulation.QuestionType.FA);
                    throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(
                                    categoryNo == null ? Common.Constants.NOT_EXIST_CATEGORY_INFORMATION_MESSAGE_ID : Common.Constants.EXIST_UNJUST_CATEGORY_INFORMATIONS_MESSAGE_ID
                                    , id.ToString(), qType.ToString())
                                );
                }

                if (categoryNo != null)
                {
                    int cateNo = (int)categoryNo;
                    string cateDesc = cateEntity.CategoryName;
                    string cateOrgDesc = cateEntity.OriginalCategoryName;
                    string wt = cateEntity.WeightValue;
                    int maxSortIdx = entity.SortRange == null ? 0 : (int)entity.SortRange;
                    bool isunsort = cateNo > maxSortIdx;
                    (question.Sectors as Sectors).Add(cateNo, cateDesc, cateOrgDesc, wt, isunsort);
                }
            } // while (!EOF)
            _log.Debug("Item1 " + sw.ElapsedMilliseconds + "ms");

            if (matricsCode == QCMatrixCode.Normal)
                return;

            #region サンプルSQL
            /*
            SELECT Item_ID, Item_Name
                    , Lv2Title, Original_Lv2Title
                    , Table_Name, Column_Name, Top_Table_Name
                    , T_Item_Info.Parent_Item_ID
            FROM   T_Item_Info
            WHERE  Parent_Item_ID = %id% AND Status = 1
                        AND Matrix_Div = 2 AND QCWebID = %qcwebid% // たぶんいらん
            ORDER BY Sort_Number;
            */
            // while (!EOF)
            #endregion

            sw.Restart();
            if (temporaryDataProcess == GlobalsCommonConstant.TemporaryDataProcess.GTSetting)
            {
                TGtMatrixChildCB tGtMatrixChildCB = new TGtMatrixChildCB();
                tGtMatrixChildCB.SetupSelect_TGtMatrixInfo();
                tGtMatrixChildCB.SetupSelect_TItemInfo().WithTTableControl();
                tGtMatrixChildCB.SetupSelect_TItemInfo().WithTDataEditList();
                tGtMatrixChildCB.Query().QueryTGtMatrixInfo().SetNewItemId_Equal(id);
                //tGtMatrixChildCB.Query().AddOrderBy_GtMatrixChildid_Asc();
                tGtMatrixChildCB.Query().QueryTItemInfo().AddOrderBy_SortNumber_Asc();
                ListResultBean<TGtMatrixChild> gtMatrixChildList = tGtMatrixChildBhv.SelectList(tGtMatrixChildCB);
                if (gtMatrixChildList.Count == 0)
                {
                    // GT集計設定追加情報がありません。アイテムID:{0}
                    throw new QCWebException("QCS0501006001", new string[] { id.ToString() }
                                            , GlobalsCommonConstant.LogLevel.FATAL, null);
                }

                foreach (TGtMatrixChild gtMatrixChild in gtMatrixChildList)
                {
                    decimal childID = (decimal)gtMatrixChild.ChildItemId;
                    decimal parentId = (decimal)gtMatrixChild.TGtMatrixInfo.NewItemId;
                    Question parentQ = this[parentId] as Question;
                    if (parentQ == null
                        || (parentQ.QuestionType & Tabulation.QuestionType.MatrixParent) != Tabulation.QuestionType.MatrixParent
                        || parentQ.ChildQuestions == null)
                    {
                        throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(Common.Constants.UNJUST_MATRIX_PARENT_INFORMATION_MESSAGE_ID, id.ToString(), parentId.ToString()));
                    }
                    Question childQ = new Question(parentQ.ChildQuestions as Questions, childID)
                    {
                        QCWebID = qcwebid,
                        Name = gtMatrixChild.TItemInfo.ItemName,
                        QCAnswerType = (QCAnswerType)int.Parse(gtMatrixChild.TItemInfo.AnswerType),
                        Description = MakeDescription("", gtMatrixChild.TItemInfo.Lv2title),
                        OriginalDescription = MakeDescription("", gtMatrixChild.TItemInfo.OriginalLv2title),
                        TableName = gtMatrixChild.TItemInfo.TableName,
                        ColumnName = gtMatrixChild.TItemInfo.ColumnName,
                        TopTableName = gtMatrixChild.TItemInfo.TTableControl.BaseTableName,
                        CategoryEditID = gtMatrixChild.TItemInfo.CategoryEditId,
                        LastUpdateDateTime = gtMatrixChild.TItemInfo.LastUpdateDatetime == null ? new DateTime(0) : (DateTime)gtMatrixChild.TItemInfo.LastUpdateDatetime
                    };
                    childQ.SetQuestionType(QCMatrixCode.MatrixChild);

                    // データ修正の有無
                    ListResultBean<TEditTargetItem> dataEditList = GetDataEditList((decimal)entity.Qcwebid, childID);
                    childQ.IsDataEdit = dataEditList.Count > 0;
                    if (childQ.IsDataEdit)
                    {
                        childQ.DataEditDateTime = (DateTime)dataEditList[0].TEditData.TDataEditList.LastUpdateDatetime;
                    }

                    // データ加工の場合、データ加工の種別を取得する
                    if (childQ.IsNewItem)
                    {
                        if (gtMatrixChild.TItemInfo.DataEditId != null)
                        {
                            childQ.DataProcessType = (GlobalsCommonConstant.PROCESS_TYPE)gtMatrixChild.TItemInfo.TDataEditList.EditMenuMasterId;
                        }
                    }

                    parentQ.ChildQuestions.Add(childID.ToString(), childQ);
                }
            }
            else
            {
                TMatrixInfoCB tMatrixInfoCB = new TMatrixInfoCB();
                tMatrixInfoCB.SetupSelect_TItemInfoByChildItemInfoId().WithTTableControl();
                tMatrixInfoCB.SetupSelect_TItemInfoByChildItemInfoId().WithTDataEditList();
                tMatrixInfoCB.Query().QueryTItemInfoByItemInfoId().SetStatus_Equal_Effective();
                tMatrixInfoCB.Query().SetItemInfoId_Equal(id);
                tMatrixInfoCB.Query().QueryTItemInfoByItemInfoId().AddOrderBy_SortNumber_Asc();
                tMatrixInfoCB.Query().AddOrderBy_ChildItemInfoId_Asc();
                ListResultBean<TMatrixInfo> matrixList = tMatrixInfoBhv.SelectList(tMatrixInfoCB);
                _log.Debug("Select2 " + sw.ElapsedMilliseconds + "ms");

                sw.Restart();
                foreach (TMatrixInfo matrixEntity in matrixList)
                {
                    decimal childID = (decimal)matrixEntity.ChildItemInfoId;
                    decimal parentId = (decimal)matrixEntity.ItemInfoId;
                    Question parentQ = this[parentId] as Question;
                    if (parentQ == null
                        || (parentQ.QuestionType & Tabulation.QuestionType.MatrixParent) != Tabulation.QuestionType.MatrixParent
                        || parentQ.ChildQuestions == null)
                    {
                        throw new QCWebException(mainErrorMessage
                                , GlobalsCommonConstant.LogLevel.FATAL
                                , GetResource.GetLogMessage(Common.Constants.UNJUST_MATRIX_PARENT_INFORMATION_MESSAGE_ID, id.ToString(), parentId.ToString()));
                    }
                    Question childQ = new Question(parentQ.ChildQuestions as Questions, childID)
                    {
                        QCWebID = qcwebid,
                        Name = matrixEntity.TItemInfoByChildItemInfoId.ItemName,
                        QCAnswerType = (QCAnswerType)int.Parse(matrixEntity.TItemInfoByChildItemInfoId.AnswerType),
                        Description = MakeDescription("", matrixEntity.TItemInfoByChildItemInfoId.Lv2title),
                        OriginalDescription = MakeDescription("", matrixEntity.TItemInfoByChildItemInfoId.OriginalLv2title),
                        TableName = matrixEntity.TItemInfoByChildItemInfoId.TableName,
                        ColumnName = matrixEntity.TItemInfoByChildItemInfoId.ColumnName,
                        TopTableName = matrixEntity.TItemInfoByChildItemInfoId.TTableControl.BaseTableName,
                        CategoryEditID = matrixEntity.TItemInfoByChildItemInfoId.CategoryEditId,
                        LastUpdateDateTime = matrixEntity.TItemInfoByChildItemInfoId.LastUpdateDatetime == null ? new DateTime(0) : (DateTime)matrixEntity.TItemInfoByChildItemInfoId.LastUpdateDatetime
                    };
                    childQ.SetQuestionType((QCMatrixCode)matrixEntity.TItemInfoByChildItemInfoId.MatrixDiv);

                    // データ修正の有無
                    ListResultBean<TEditTargetItem> dataEditList = GetDataEditList((decimal)entity.Qcwebid, childID);
                    childQ.IsDataEdit = dataEditList.Count > 0;
                    if (childQ.IsDataEdit)
                    {
                        childQ.DataEditDateTime = (DateTime)dataEditList[0].TEditData.TDataEditList.LastUpdateDatetime;
                    }

                    // データ加工の場合、データ加工の種別を取得する
                    if (childQ.IsNewItem)
                    {
                        if (matrixEntity.TItemInfoByChildItemInfoId.DataEditId != null)
                        {
                            childQ.DataProcessType = (GlobalsCommonConstant.PROCESS_TYPE)matrixEntity.TItemInfoByChildItemInfoId.TDataEditList.EditMenuMasterId;
                        }
                    }

                    parentQ.ChildQuestions.Add(childID.ToString(), childQ);
                }
            }
            _log.Debug("Item2 " + sw.ElapsedMilliseconds + "ms");
        }

        /// <summary>
        /// コンストラクタ<br />
        /// コレクションに格納せず、単一のインスタンスを要素とする場合に使用
        /// (<paramref name="id"/>にマトリクス子質問の質問IDを指定した場合は、その親質問を表すQuestionクラスのインスタンスへの参照を直下に持つ)
        /// <note>このコンストラクタを使って作成したインスタンスでは、属性アイテムかどうかの判定は不可</note>
        /// </summary>
        /// <param name="qcwebid">
        /// QCWebデータ管理ID
        /// <note>
        /// この引数は、当初使用することを想定していたが使用しない<br />
        /// オーバーロードメソッド同士の引数定義の衝突を避ける目的で残している<br />
        /// 0を指定すればよい
        /// </note>
        /// </param>
        /// <param name="id">質問ID</param>
        public Questions(decimal qcwebid, decimal id)
        {
            AddOne(id);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parentquestion">親質問を表すQuestionクラスのインスタンスへの参照</param>
        public Questions(Question parentquestion = null)
        {
            this.parentquestion = parentquestion;
        }

        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="id">質問ID</param>
        /// <param name="onlytop">サブコレクション内を検索しない場合true</param>
        /// <returns>コレクション内をサブコレクション内まで検索して、質問IDが示す質問情報を保持したQuestionクラスのインスタンスへの参照</returns>
        public IQuestion this[decimal id, bool onlytop]
        {
            get
            {
                string key = id.ToString();
                if (base.Contains(key))
                    return base[key] as Question;
                if (onlytop)
                    return null;
                foreach (DictionaryEntry de in this)
                {
                    Question question = de.Value as Question;
                    if (question.ChildQuestions != null)
                    {
                        //if (question.ChildQuestions.Contains(id))
                        if ((question.ChildQuestions as Questions).Contains(id))
                        {
                            return (question.ChildQuestions as Questions)[id, true];
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="id">質問ID</param>
        /// <returns>コレクション内をサブコレクション内まで検索して、質問IDが示す質問情報を保持したQuestionクラスのインスタンスへの参照</returns>
        public IQuestion this[decimal id]
        {
            get
            {
                return this[id, false];
            }
        }

        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="index">質問の1ベースインデックス</param>
        /// <returns>インデックスが示すQuestionクラスのインスタンスへの参照</returns>
        public IQuestion this[int index]
        {
            get
            {
                foreach (DictionaryEntry de in this)
                {
                    Question question = de.Value as Question;
                    if (question.Index == index)
                        return question;
                }
                return null;
            }
        }

        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="name">アイテム名</param>
        /// <param name="id">
        /// シナリオID（省略可、既定値null）
        /// GT集計設定追加、カテゴリ出力編集で作成されたアイテムを検索するのに利用する
        /// </param>
        /// <param name="ignoreCase">大文字小文字を区別しない場合true (省略可、既定値true)</param>
        /// <param name="ignoreByte">全角半角を区別しない場合true (省略可、既定値true)</param>
        /// <param name="onlytop">サブコレクション内を検索しない場合true (省略可、既定値false)</param>
        /// <returns>コレクション内をサブコレクション内まで検索して、アイテム名が示す質問情報を保持したQuestionクラスのインスタンスへの参照</returns>
        public Question this[string name, decimal? id = null, bool ignoreCase = true, bool ignoreByte = true, bool onlytop = false]
        {
            get
            {
                VbStrConv conv = VbStrConv.None;
                if (ignoreCase)
                    conv |= VbStrConv.Lowercase;
                if (ignoreByte)
                    conv |= VbStrConv.Narrow;
                name = Strings.StrConv(name, conv);
                foreach (DictionaryEntry de in this)
                {
                    Question question = de.Value as Question;
                    if (Strings.StrConv(question.Name, conv).Equals(name) && question.CategoryEditID == id)
                    {
                        return question;
                    }
                    if (question.ChildQuestions != null && !onlytop)
                    {
                        if (question.ChildQuestions.Contains(name, id, ignoreCase, ignoreByte))
                        {
                            return (question.ChildQuestions as Questions)[name, id, ignoreCase, ignoreByte];
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// コレクション内に<paramref name="name"/>に指定したアイテム名を持つインスタンスがあるかどうかを、サブコレクション内まで考慮して返すメソッド
        /// </summary>
        /// <param name="name">アイテム名</param>
        /// <param name="id">
        /// シナリオID（省略可、既定値null）
        /// GT集計設定追加、カテゴリ出力編集で作成されたアイテムを検索するのに利用する
        /// </param>
        /// <param name="ignoreCase">大文字小文字を区別しない場合true (省略可、既定値true)</param>
        /// <param name="ignoreByte">全角半角を区別しない場合true (省略可、既定値true)</param>
        /// <returns>ある場合true、ない場合false</returns>
        public bool Contains(string name, decimal? id = null, bool ignoreCase = true, bool ignoreByte = true)
        {
            return this[name, id, ignoreCase, ignoreByte] != null;
        }

        /// <summary>
        /// コレクション内に<paramref name="id"/>に指定した質問IDを持つインスタンスがあるかどうかを、サブコレクション内まで考慮して返すメソッド
        /// </summary>
        /// <param name="id">質問ID</param>
        /// <returns>ある場合true、ない場合false</returns>
        public bool Contains(decimal id)
        {
            return this[id] != null;
        }

        /// <summary>
        /// 親質問を表すQuestionクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// <note>マトリクス子質問コレクションの場合にのみ有効</note>
        /// </summary>
        public IQuestion ParentQuestion
        {
            get
            {
                return parentquestion;
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (DictionaryEntry de in this)
            {
                Question question = de.Value as Question;
                question.Dispose();
            }
            parentquestion = null;
        }

        internal bool IncludeQuestion
        {
            get
            {
                return includeQuestion;
            }
        }

        private ListResultBean<TEditTargetItem> GetDataEditList(decimal qcwebid, decimal? itemid = null)
        {
            TEditTargetItemCB cb = new TEditTargetItemCB();
            cb.SetupSelect_TEditData().WithTDataEditList();
            cb.Query().QueryTEditData().QueryTDataEditList().SetQcwebid_Equal(qcwebid);
            if (itemid != null)
                cb.Query().SetTargetItemId_Equal(itemid);
            cb.Query().QueryTEditData().QueryTDataEditList().SetStatus_Equal(GlobalsCommonConstant.ProcessStatus.BeenExecuted);
            cb.Query().AddOrderBy_TargetItemId_Asc();

            return tEditTargetItemBhv.SelectList(cb);
        }

        private ListResultBean<TGtMatrixInfo> GetGtMatrixInfoList(decimal qcwebid, decimal? itemid = null)
        {
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB();
            cb.SetupSelect_TScenarioTotalization();
            cb.Query().QueryTScenarioTotalization().InnerJoin();
            cb.Query().QueryTScenarioTotalization().SetQcwebid_Equal(qcwebid);
            if (itemid != null)
                cb.Query().SetNewItemId_Equal(itemid);
            cb.Query().AddOrderBy_GtMatrixInfoId_Asc();

            return tGtMatrixInfoBhv.SelectList(cb);
        }

        private string MakeDescription(string lv1title, string lv2title) {
            string lv1 = string.IsNullOrEmpty(lv1title) ? "" : lv1title;
            string lv2 = string.IsNullOrEmpty(lv2title) ? "" : lv2title;

            StringBuilder description = new StringBuilder(lv1);
            for (int i = 0; i < 1000 - lv1.Length; ++i) {
                description.Append((char)1);
            }
            description.Append(lv2);
            for (int i = 0; i < 1000 - lv2.Length; ++i) {
                description.Append((char)1);
            }
            return description.ToString();
        }
    }
}
