#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Criteria.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/8/3
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/4
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Seasar.Quill;
using System.Text.RegularExpressions;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Exceptions;

namespace Macromill.QCWeb.Tabulation
{
    /// <summary>
    /// 条件を扱うインターフェイス
    /// </summary>
    [ComVisible(false)]
    public interface ICriteria : IDisposable
    {
        /// <summary>
        /// 大元のICriteriaインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        ICriteria TopCriteria { get; }
        /// <summary>
        /// 末端のICriteriaインターフェイスの実装クラスのインスタンスにおいて、条件アイテムを表すQuestions.Questionクラスのインスタンスへの参照
        /// </summary>
        Questions.Question Question { get; }
        /// データファイルのディレクトリのパス
        /// </summary>
        string DataDirectoryPath { get; }
        /// <summary>
        /// 末端のICriteriaインターフェイスの実装クラスのインスタンスにおいて、条件値の文字列表現
        /// </summary>
        string CriteriaValueDescription { get; }
        /// <summary>
        /// サブインスタンスである、ICriteriaインターフェイスの実装クラスのインスタンスからなるListクラスのインスタンスへの参照
        /// </summary>
        List<ICriteria> SubCriterias { get; }
        /// <summary>
        /// 前条件との演算子を表すOperator列挙型の値
        /// </summary>
        Operator Operator { get; }
        /// <summary>
        /// 末端のICriteriaインターフェイスの実装クラスのインスタンスにおいて、演算子の文字列表現
        /// </summary>
        string CriteriaOperatorDescription { get; }

        /// <summary>
        /// #297101で条件のアイテムIDを参照するため追加。データ加工用IDを取得する。
        /// </summary>
        decimal QuestionIDforDP { get; }

    }

    /// <summary>
    /// 絞り込み条件を扱うクラス
    /// </summary>
    [ComVisible(false), Guid("7D366163-FF8E-4A3A-82AD-91488A89EF3F")]
    public class Criteria : ICriteria
    {
        private Question.Questions.Question question = null;
        // private decimal qcwebid = (decimal)0;
        private string dirPath = null;
        // private string topTableName = null;
        // private decimal qId = 0;
        // private string qName = null;
        // private QuestionType qType = (QuestionType)0;
        private string criteriaOperatorDescription = null;
        private string criteriaValueDescription = null;
        private List<ICriteria> subCriterias = null;
        private Operator ope = Operator.opOr;
        private Criteria parentCriteria = null;
        public static bool isvariable = false;
        public static QuestionType variabletype = QuestionType.N;//non nullable
        #region コンストラクタ
        private void init(decimal questionid, string criteriaoperatordescription, string criteriavaluebuffer, string dirpath, Operator ope)
        {
            // qId = questionid;
            // DB接続してアイテム名と質問タイプ、先頭ローデータテーブル名を取得
            // TODO:QCWeb管理IDは手元にないのでダミー
            Question.Questions questions = new Question.Questions(0, questionid);
            question = questions[questionid] as Question.Questions.Question;

            //TItemInfo entity = GetItemInfo(questionid);
            //qName = entity.ItemName;  //"hogehoge";
            //qType = (Tabulation.QuestionType)int.Parse(entity.AnswerType);  //Tabulation.QuestionType.SA;
            //topTableName = entity.TQcwebSurveyInfo.TTableControl.BaseTableName;  //null;
            //qcwebid = question.QCWebID;
            //qName = question.Name;
            //qType = question.QuestionType;
            //topTableName = question.TopTableName;
            // マトリクスは不可
            if ((question.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustConstructorMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL);
            }
            //criteriaOperatorDescription = criteriaoperatordescription;
            //criteriaValueDescription = criteriavaluebuffer;
            //this.ope = ope;
            //dirPath = dirpath;
            init(criteriaoperatordescription, criteriavaluebuffer, dirpath, ope, questionid);
        }

        private void init(Question.Questions questions, decimal questionid, string criteriaoperatordescription, string criteriavaluebuffer, string dirpath, Operator ope)
        {
            // qId = questionid;
            // DB接続してアイテム名と質問タイプ、先頭ローデータテーブル名を取得
            // TODO:QCWeb管理IDは手元にないのでダミー
            question = questions[questionid] as Question.Questions.Question;

            //TItemInfo entity = GetItemInfo(questionid);
            //qName = entity.ItemName;  //"hogehoge";
            //qType = (Tabulation.QuestionType)int.Parse(entity.AnswerType);  //Tabulation.QuestionType.SA;
            //topTableName = entity.TQcwebSurveyInfo.TTableControl.BaseTableName;  //null;
            //qcwebid = question.QCWebID;
            //qName = question.Name;
            //qType = question.QuestionType;
            //topTableName = question.TopTableName;
            // マトリクスは不可
            if ((question.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustConstructorMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL);
            }
            //criteriaOperatorDescription = criteriaoperatordescription;
            //criteriaValueDescription = criteriavaluebuffer;
            //this.ope = ope;
            //dirPath = dirpath;
            init(criteriaoperatordescription, criteriavaluebuffer, dirpath, ope, question.ID);
        }


        private void init(string criteriaoperatordescription, string criteriavaluebuffer, string dirpath, Operator ope, decimal questionid)
        {
            criteriaOperatorDescription = criteriaoperatordescription;
            criteriaValueDescription = criteriavaluebuffer;
            this.ope = ope;
            dirPath = dirpath;
            QuestionIDforDP = questionid;
        }

        /*
        private void init(decimal questionid, CriteriaOperator criteriaoperator, DataType criteriadatatype, string dirpath, Operator ope)
        {
            criteriadatatype &= DataType.NAData | DataType.IVData;
            if ((int)criteriadatatype == 0) return; // エラースロー
            qId = questionid;
            // DB接続してアイテム名と質問タイプ、先頭ローデータテーブル名を取得
            // TODO:QCWeb管理IDは手元にないのでダミー
            Question.Questions questions = new Question.Questions(0, qId);
            Questions.Question question = questions[qId] as Question.Questions.Question;

            //TItemInfo entity = GetItemInfo(questionid);
            //qName = entity.ItemName;  //"hogehoge";
            //qType = (Tabulation.QuestionType)int.Parse(entity.AnswerType);  //Tabulation.QuestionType.SA;
            //topTableName = entity.TQcwebSurveyInfo.TTableControl.BaseTableName;  //null;
            qcwebid = question.QCWebID;
            qName = question.Name;
            qType = question.QuestionType;
            topTableName = question.TopTableName;

            // マトリクスは不可
            if ((qType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
            {
                // エラースロー
                return;
            }
            criteriaOperator = criteriaoperator;
            criteriaDataType = criteriadatatype;
            dirPath = dirpath;
            this.ope = ope;
        }
        */

        private void init(string dirpath, Operator ope = Operator.opOr)
        {
            dirPath = dirpath;
            this.ope = ope;
            subCriterias = new List<ICriteria>();
        }

        /*
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="questionid">アイテムID</param>
        /// <param name="criteriaoperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="criteriavaluebuffer">絞り込み条件値の文字列表現</param>
        /// <param name="dirpath">データテキストファイルを出力するディレクトリのパス</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        public Criteria(decimal questionid, CriteriaOperator criteriaoperator, string criteriavaluebuffer, string dirpath, Operator ope = Operator.opOr)
        {
            init(questionid, criteriaoperator, criteriavaluebuffer, dirpath, ope);
        }

        /// <summary>
        /// コンストラクタ<br />
        /// 本インスタンスが複数のCriteriaを包含する場合に使用する
        /// </summary>
        /// <param name="dirpath">データテキストファイルを出力するディレクトリのパス</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        public Criteria(string dirpath, Operator ope = Operator.opOr)
        {
            init(dirpath, ope);
        }
        */

        /// <summary>
        /// コンストラクタ<br />
        /// QC3仕様の絞り込み条件文字列から必要情報を取り出して整形して保持する
        /// <note>
        /// <paramref name="criteriadescription"/>にはAND→&amp;、OR→|に置換したものを渡し、
        /// 条件値が文字列の場合に、それに含まれる次の文字はエスケープするものとする<br />
        /// また、かっこの中には、2つ以上の条件式と、条件式の数-1個の演算子が含まれるものとする<br />
        /// この形式にしたがわない文字列が渡された場合には、正常動作は保証されない
        /// <list type="table">
        /// <listheader>
        /// <term>文字</term>
        /// <description>エスケープ後</description>
        /// </listheader>
        /// <item>
        /// <term>&amp;</term>
        /// <description>\&amp;</description>
        /// </item>
        /// <item>
        /// <term>|</term>
        /// <description>\|</description>
        /// </item>
        /// <item>
        /// <term>(</term>
        /// <description>\(</description>
        /// </item>
        /// <item>
        /// <term>)</term>
        /// <description>\)</description>
        /// </item>
        /// <item>
        /// <term>\</term>
        /// <description>\\</description>
        /// </item>
        /// </list>
        /// また、アイテム名ではなくアイテムIDを指定し、演算子(&amp;または|)の前後には半角スペースを付与し、絞り込み条件の演算子(=など)の前後にはスペースがないものとする
        /// </note>
        /// </summary>
        /// <param name="criteriadescription">条件文字列</param>
        /// <param name="dirpath">データテキストファイルを出力するディレクトリのパス</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値 (省略可、既定値Operator.opOr)</param>
        /// <param name="parentCriteria">親Criteriaクラスのインスタンスへの参照 (省略可、既定値null)</param>
        /// <example>
        /// 次のサンプルは、以下に示す抽出条件でCriteriaクラスのインスタンスを生成して、それへの参照を得る
        /// <list type="table">
        /// <listheader>
        /// <term>アイテムID</term>
        /// <description>値</description>
        /// </listheader>
        /// <item>
        /// <term>1 (SA)</term>
        /// <description>1または3～5のいずれか</description>
        /// </item>
        /// <item>
        /// <term>AND</term>
        /// <description></description>
        /// </item>
        /// <item>
        /// <term>(</term>
        /// <description>(</description>
        /// </item>
        /// <item>
        /// <term>2 (SA)</term>
        /// <description>1と等しい</description>
        /// </item>
        /// <item>
        /// <term>OR</term>
        /// <description></description>
        /// </item>
        /// <item>
        /// <term>3 (SA)</term>
        /// <description>無回答または非該当</description>
        /// </item>
        /// <item>
        /// <term>)</term>
        /// <description></description>
        /// </item>
        /// </list>
        /// <code lang="C#">
        /// Criteria criteria = new Criteria("1=1,3-5 &amp; (2=1 | 3=DK,*)", Operator.opOr);
        /// </code>
        /// </example>
        public Criteria(string criteriadescription, string dirpath, Operator ope = Tabulation.Operator.opOr, Criteria parentCriteria = null)
        {
            if (string.IsNullOrWhiteSpace(criteriadescription))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "criteriadescription");
            }
            Regex cleanregex = new Regex(@"(^|[^\\]) {2,}");
            criteriadescription = cleanregex.Replace(criteriadescription, "$1 ");
            Regex grpstartregex = new Regex(@"(?<!\\)\(");
            Regex grpendregex = new Regex(@"(?<!\\)\)");
            Regex operegex = new Regex(@"(?<!\\)[&|]");
            Regex crioperegex = new Regex("<>|>=|<=|!=|=|>|<");
            Regex escapeRegex = new Regex(@"\\([&|()])");//Slash escape removed. This fix as per Redmine Id:232757, 233829
            this.parentCriteria = parentCriteria;
            this.ope = ope;
            Regex splitregex = new Regex(@"(?<!\\) ");
            // string[] splitBuf = criteriadescription.Split(' ');
            string[] splitBuf = splitregex.Split(criteriadescription);
            if (grpstartregex.IsMatch(CutVariableName(criteriadescription)))
            {
                // かっこで括られた箇所を抽出
                int cnt = 0;
                int startIdx = 0;

                int modifyIdx = 0;
                for (int i = 0; i < splitBuf.Length; ++i)
                {
                    if (cnt == 0) startIdx = i;
                    if (splitBuf[i][0].Equals('(')) // 先頭が「(」＝先頭何文字かが「(」
                    {
                        MatchCollection ms = grpstartregex.Matches(CutVariableName(splitBuf[i]));
                        cnt += ms.Count;    // 「(」の数を足す
                    }
                    else if (splitBuf[i].Substring(splitBuf[i].Length - 1).Equals(")")) // 末尾が「)」＝末尾何文字かが「)」
                    {
                        MatchCollection ms = grpendregex.Matches(CutVariableName(splitBuf[i]));
                        cnt -= ms.Count;    // 「)」の数を引く
                    }
                    if (cnt < 0)    // 「(」と「)」の組み合わせが正しくない
                    {
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                               , GlobalsCommonConstant.LogLevel.FATAL
                                               , "criteriadescription", "\"" + criteriadescription + "\"");
                    }
                    if (cnt == 0)
                    {
                        // 「(」に対応する「)」までをまとめる
                        splitBuf[modifyIdx] = splitBuf[startIdx];
                        for (int j = startIdx + 1; j <= i; ++j)
                        {
                            splitBuf[modifyIdx] += " " + splitBuf[j];
                        }
                        ++modifyIdx;
                    }
                }
                // 条件式と、条件式の数-1個の演算子からなるので、奇数とならなければならない
                if (modifyIdx % 2 != 1)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "criteriadescription", "\"" + criteriadescription + "\"");
                }
                Array.Resize<string>(ref splitBuf, modifyIdx);
                Regex regex = new Regex(@"^\((.+)\)$");
                init(dirpath, ope);
                Operator op = Tabulation.Operator.opOr;
                for (int i = 0; i < splitBuf.Length; ++i)
                {
                    if (string.IsNullOrWhiteSpace(splitBuf[i]))
                    {
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                               , GlobalsCommonConstant.LogLevel.FATAL
                                               , "criteriadescription", "\"" + criteriadescription + "\"");
                    }
                    string s = splitBuf[i].Trim();
                    if (i % 2 == 0) // 条件式
                    {
                        if (regex.IsMatch(s))
                        {
                            // 両端の「(」「)」を落とす
                            s = regex.Match(s).Groups[1].Value.Trim();
                            // s = s.Substring(1, s.Length - 2).Trim();
                        }
                        if (this is DataProcess.NewQuestionSectorCriteria)
                        {
                            subCriterias.Add(new DataProcess.NewQuestionSectorCriteria(s, dirpath, op, null));
                        }
                        else
                        {
                            subCriterias.Add(new Criteria(s, dirpath, op, this));
                        }
                    }
                    else    // 条件間演算子
                    {
                        if (!s.Equals("&") && !s.Equals("|"))
                        {
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , "criteriadescription", "\"" + criteriadescription + "\"");
                        }
                        op = s.Equals("&") ? Tabulation.Operator.opAnd : Tabulation.Operator.opOr;
                    }
                }
            }
            else
            {
                // 条件式と、条件式の数-1個の演算子からなるので、奇数とならなければならない
                if (splitBuf.Length % 2 != 1)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "criteriadescription", "\"" + criteriadescription + "\"");
                }
                // &, |で分割
                if (operegex.IsMatch(criteriadescription))
                {
                    init(dirpath, ope);
                    MatchCollection ms = operegex.Matches(criteriadescription);
                    string[] descArray = operegex.Split(criteriadescription);
                    for (int i = 0; i < descArray.Length; ++i)
                    {
                        Operator op = Tabulation.Operator.opOr;
                        if (i > 0 && ms[i - 1].Value == "&") op = Tabulation.Operator.opAnd;
                        if (this is DataProcess.NewQuestionSectorCriteria)
                        {
                            subCriterias.Add(new DataProcess.NewQuestionSectorCriteria(descArray[i].Trim(), dirpath, op, null));
                        }
                        else
                        {
                            subCriterias.Add(new Criteria(descArray[i].Trim(), dirpath, op, this));
                        }
                    }
                }
                else    // 条件式
                {
                    if (crioperegex.IsMatch(criteriadescription))
                    {
                        string[] tmpBuf = crioperegex.Split(criteriadescription, 2);
                        decimal questionid = (decimal)0;
                        if (tmpBuf.Length != 2 || !decimal.TryParse(tmpBuf[0].Trim(), out questionid))
                        {
                            // 演算子で分割できない(事前チェックしているのでありえない)か、
                            // 演算子で分割した1つ目の要素(アイテムID)が数値化できない
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , "criteriadescription", "\"" + criteriadescription + "\"");
                        }
                        string criteriaoperatordescription = crioperegex.Match(criteriadescription).Value;
                        string criteriavaluebuffer = escapeRegex.Replace(tmpBuf[1].Trim(), "$1");
                        if (this is DataProcess.NewQuestionSectorCriteria)
                        {
                            init(criteriaoperatordescription, criteriavaluebuffer, dirpath, ope, questionid);
                        }
                        else
                        {
                            init(questionid, criteriaoperatordescription, criteriavaluebuffer, dirpath, ope);
                        }
                    }
                    else
                    {
                        // 演算子がない
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                               , GlobalsCommonConstant.LogLevel.FATAL
                                               , "criteriadescription", "\"" + criteriadescription + "\"");
                    }
                }
            }
        }

        public Criteria(string criteriadescription, string dirpath, Question.Questions questions, Operator ope = Tabulation.Operator.opOr,
            Criteria parentCriteria = null, bool LocalizeFilteringExpression = false)
        {
            if (string.IsNullOrWhiteSpace(criteriadescription))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "criteriadescription");
            }
            Regex cleanregex = new Regex(@"(^|[^\\]) {2,}");
            criteriadescription = cleanregex.Replace(criteriadescription, "$1 ");
            Regex grpstartregex = new Regex(@"(?<!\\)\(");
            Regex grpendregex = new Regex(@"(?<!\\)\)");
            Regex operegex = new Regex(@"(?<!\\)[&|]");
            Regex crioperegex = new Regex("<>|>=|<=|!=|=|>|<");
            Regex escapeRegex = new Regex(@"\\([&|()])");//Slash escape removed. This fix as per Redmine Id:232757, 233829
            this.parentCriteria = parentCriteria;
            this.ope = ope;
            Regex splitregex = new Regex(@"(?<!\\) ");
            // string[] splitBuf = criteriadescription.Split(' ');
            string[] splitBuf = splitregex.Split(criteriadescription);
            if (!LocalizeFilteringExpression && grpstartregex.IsMatch(CutVariableName(criteriadescription)))
            {
                // かっこで括られた箇所を抽出
                int cnt = 0;
                int startIdx = 0;
                int modifyIdx = 0;
                for (int i = 0; i < splitBuf.Length; ++i)
                {
                    if (cnt == 0) startIdx = i;
                    if (splitBuf[i][0].Equals('(')) // 先頭が「(」＝先頭何文字かが「(」
                    {
                        MatchCollection ms = grpstartregex.Matches(CutVariableName(splitBuf[i]));
                        cnt += ms.Count;    // 「(」の数を足す
                    }
                    else if (splitBuf[i].Substring(splitBuf[i].Length - 1).Equals(")")) // 末尾が「)」＝末尾何文字かが「)」
                    {
                        MatchCollection ms = grpendregex.Matches(CutVariableName(splitBuf[i]));
                        cnt -= ms.Count;    // 「)」の数を引く
                    }
                    if (cnt < 0)    // 「(」と「)」の組み合わせが正しくない
                    {
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                               , GlobalsCommonConstant.LogLevel.FATAL
                                               , "criteriadescription", "\"" + criteriadescription + "\"");
                    }
                    if (cnt == 0)
                    {
                        // 「(」に対応する「)」までをまとめる
                        splitBuf[modifyIdx] = splitBuf[startIdx];
                        for (int j = startIdx + 1; j <= i; ++j)
                        {
                            splitBuf[modifyIdx] += " " + splitBuf[j];
                        }
                        ++modifyIdx;
                    }
                }
                // 条件式と、条件式の数-1個の演算子からなるので、奇数とならなければならない
                if (modifyIdx % 2 != 1)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "criteriadescription", "\"" + criteriadescription + "\"");
                }
                Array.Resize<string>(ref splitBuf, modifyIdx);
                Regex regex = new Regex(@"^\((.+)\)$");
                init(dirpath, ope);
                Operator op = Tabulation.Operator.opOr;
                for (int i = 0; i < splitBuf.Length; ++i)
                {
                    if (string.IsNullOrWhiteSpace(splitBuf[i]))
                    {
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                               , GlobalsCommonConstant.LogLevel.FATAL
                                               , "criteriadescription", "\"" + criteriadescription + "\"");
                    }
                    string s = splitBuf[i].Trim();
                    if (i % 2 == 0) // 条件式
                    {
                        if (regex.IsMatch(s))
                        {
                            // 両端の「(」「)」を落とす
                            s = regex.Match(s).Groups[1].Value.Trim();
                            // s = s.Substring(1, s.Length - 2).Trim();
                        }
                        if (this is DataProcess.NewQuestionSectorCriteria)
                        {
                            subCriterias.Add(new DataProcess.NewQuestionSectorCriteria(s, dirpath, op, null));
                        }
                        else
                        {
                            subCriterias.Add(new Criteria(s, dirpath, questions, op, this, LocalizeFilteringExpression));
                        }
                    }
                    else    // 条件間演算子
                    {
                        if (!s.Equals("&") && !s.Equals("|"))
                        {
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , "criteriadescription", "\"" + criteriadescription + "\"");
                        }
                        op = s.Equals("&") ? Tabulation.Operator.opAnd : Tabulation.Operator.opOr;
                    }
                }
            }
            else
            {
                // 条件式と、条件式の数-1個の演算子からなるので、奇数とならなければならない
                if (splitBuf.Length % 2 != 1)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "criteriadescription", "\"" + criteriadescription + "\"");
                }
                // &, |で分割
                if (operegex.IsMatch(criteriadescription))
                {
                    init(dirpath, ope);
                    MatchCollection ms = operegex.Matches(criteriadescription);
                    string[] descArray = operegex.Split(criteriadescription);
                    for (int i = 0; i < descArray.Length; ++i)
                    {
                        Operator op = Tabulation.Operator.opOr;
                        if (i > 0 && ms[i - 1].Value == "&") op = Tabulation.Operator.opAnd;
                        if (this is DataProcess.NewQuestionSectorCriteria)
                        {
                            subCriterias.Add(new DataProcess.NewQuestionSectorCriteria(descArray[i].Trim(), dirpath, op, null));
                        }
                        else
                        {
                            subCriterias.Add(new Criteria(descArray[i].Trim(), dirpath, questions, op, this, LocalizeFilteringExpression));
                        }
                    }
                }
                else    // 条件式
                {
                    if (crioperegex.IsMatch(criteriadescription))
                    {
                        string[] tmpBuf = crioperegex.Split(criteriadescription, 2);
                        decimal questionid = (decimal)0;
                        if (tmpBuf.Length != 2 || !decimal.TryParse(tmpBuf[0].Trim(), out questionid))
                        {
                            // 演算子で分割できない(事前チェックしているのでありえない)か、
                            // 演算子で分割した1つ目の要素(アイテムID)が数値化できない
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , "criteriadescription", "\"" + criteriadescription + "\"");
                        }
                        string criteriaoperatordescription = crioperegex.Match(criteriadescription).Value;
                        string criteriavaluebuffer = escapeRegex.Replace(tmpBuf[1].Trim(), "$1");
                        if (this is DataProcess.NewQuestionSectorCriteria)
                        {
                            init(criteriaoperatordescription, criteriavaluebuffer, dirpath, ope, questionid);
                        }
                        else
                        {
                            init(questions, questionid, criteriaoperatordescription, criteriavaluebuffer, dirpath, ope);
                        }
                    }
                    else
                    {
                        // 演算子がない
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                               , GlobalsCommonConstant.LogLevel.FATAL
                                               , "criteriadescription", "\"" + criteriadescription + "\"");
                    }
                }
            }
        }


        #endregion

        /// <summary>
        /// 文字列からアイテム名を除外する( "["から"]"で括られた部分をアイテム名と判断して除外する）
        /// </summary>
        /// <param name="orgStr">処理対象文字列</param>
        /// <returns></returns>
        private string CutVariableName(string orgStr)
        {
            int startIndex = orgStr.IndexOf('[');
            int endIndex = orgStr.IndexOf(']') + 1;
            if (startIndex >= 0 && endIndex > startIndex)
            {
                return orgStr.Remove(startIndex, endIndex - startIndex);
            }
            return orgStr;
        }

        /// <summary>
        /// 大元のCriteriaクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public ICriteria TopCriteria
        {
            get
            {
                if (parentCriteria == null)
                {
                    return this;
                }
                else
                {
                    return parentCriteria.TopCriteria;
                }
            }
        }

        /// <summary>
        /// 条件アイテムを表すQuestions.Questionクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public Questions.Question Question
        {
            get
            {
                return question;
            }
        }

        /// <summary>
        /// アイテムIDを返す読み取り専用プロパティ
        /// </summary>
        public decimal QuestionID
        {
            get
            {
                return question == null ? (decimal)0 : question.ID;
            }
        }

        /// <summary>
        /// データ加工専用のアイテムIDを返すプロパティ
        /// </summary>
        public decimal QuestionIDforDP { get; set; }

        /// <summary>
        /// QCWeb管理IDを返す読み取り専用プロパティ
        /// </summary>
        public decimal QCWebID
        {
            get
            {
                return question == null ? (decimal)0 : question.QCWebID;
            }
        }

        /// <summary>
        /// アイテム名を返す読み取り専用プロパティ
        /// </summary>
        public string QuestionName
        {
            get
            {
                return question == null ? null : question.Name;
            }
        }

        /// <summary>
        /// 質問タイプを表すQuestionType列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public QuestionType QuestionType
        {
            get
            {
                return question == null ? (QuestionType)0 : question.QuestionType;
            }
        }

        /// <summary>
        /// データファイルのディレクトリのパスを返す読み取り専用プロパティ
        /// </summary>
        public string DataDirectoryPath
        {
            get
            {
                if (parentCriteria == null)
                {
                    return dirPath;
                }
                else
                {
                    return parentCriteria.DataDirectoryPath;
                }
            }
        }

        /*
        /// <summary>
        /// 絞り込み演算子を表すCriteriaOperator列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public CriteriaOperator CriteriaOperator
        {
            get
            {
                return criteriaOperator;
            }
        }

        /// <summary>
        /// データ種別による絞り込みの条件とするデータ種別を表すDataType列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public DataType CriteriaDataType
        {
            get
            {
                return criteriaDataType;
            }
        }
        */

        /// <summary>
        /// 絞り込み条件の文字列表現を返す読み取り専用プロパティ
        /// </summary>
        public string CriteriaValueDescription
        {
            get
            {
                return criteriaValueDescription;
            }
        }

        /// <summary>
        /// 絞り込み条件演算子の文字列表現を返す読み取り専用プロパティ
        /// </summary>
        public string CriteriaOperatorDescription
        {
            get
            {
                return criteriaOperatorDescription;
            }
        }

        /// <summary>
        /// 本インスタンスが複数のCriteriaを包含する場合に、そのサブインスタンスからなるListクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public List<ICriteria> SubCriterias
        {
            get
            {
                return subCriterias;
            }
        }

        /// <summary>
        /// 前条件との演算子を表すOperator列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public Operator Operator
        {
            get
            {
                return ope;
            }
        }

        internal void Filtering(ref bool[] FilteringFlag, decimal? sId)
        {
            if (subCriterias != null)
            {
                foreach (Criteria subcriteria in subCriterias)
                {
                    subcriteria.Filtering(ref FilteringFlag, sId);
                }
            }
            else
            {
                string qFilePath = null;
                QCWebException e = null;
                QuestionType qType = (QuestionType)0;
                if (!Tabulation.CreateTextFile.CreateData(QuestionID, dirPath, out qFilePath, out qType, out e)) throw e;
                // QuestionType qtype = qType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.FA | Tabulation.QuestionType.N);
                // QCAnswerType atype = (QCAnswerType)Enum.Parse(typeof(QCAnswerType), qtype.ToString());
                if (criteriaOperatorDescription != null)
                {
                    //Tabulation.GlobalTabulation.EasyFiltering(
                    //            qFilePath, qType, criteriaValueDescription, criteriaOperatorDescription
                    //            , ref FilteringFlag, ope: ope);
                    Tabulation.GlobalTabulation.EasyFiltering(
                                  QCWebID, QuestionID, DataDirectoryPath
                                , out qFilePath, out qType
                                , criteriaValueDescription
                                , criteriaOperatorDescription
                                , ref FilteringFlag, false, ope, sId);
                }
                else
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullDataMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "criteriaOperatorDescription");
                    /*
                    List<Data> data = ReadTextFile.ReadData(qFilePath, qType);
                    if (criteriaDataType == DataType.NormalData)
                    {
                        switch (qtype)
                        {
                            case Tabulation.QuestionType.SA:
                            case Tabulation.QuestionType.MA:
                                int[] criteriaSectorList = null;
                                criteriaDataType = Tabulation.GlobalTabulation.CriteriaValueDescriptionToValueList<int>
                                                            (qType, criteriaValueDescription, out criteriaSectorList);
                                if (criteriaDataType == DataType.NormalData)
                                {
                                    if (criteriaSectorList == null || criteriaSectorList.Length == 0)
                                    {
                                        // エラースロー
                                        return;
                                    }
                                    if (criteriaSectorList.Length == 1 && qtype == Tabulation.QuestionType.SA)
                                    {
                                        // Filtering003をコール
                                        Tabulation.GlobalTabulation.Filtering(data, qType, criteriaSectorList[0], criteriaOperator, ref FilteringFlag, ope);
                                    }
                                    else
                                    {
                                        // Filtering001をコール
                                        Tabulation.GlobalTabulation.Filtering(data, qType, ref criteriaSectorList, criteriaOperator, ref FilteringFlag, ope);
                                    }
                                }
                                else
                                {
                                    // データ種別によるフィルタリング
                                    // Filtering101を実行
                                    Tabulation.GlobalTabulation.Filtering(data, criteriaDataType, criteriaOperator, ref FilteringFlag, ope);
                                }
                                break;
                            case Tabulation.QuestionType.FA:
                                // Filtering007をコール
                                Tabulation.GlobalTabulation.Filtering(data, qType, criteriaValueDescription, criteriaOperator, ref FilteringFlag, ope);
                                break;
                            case Tabulation.QuestionType.N:
                                double[] criteriaNumberList = null;
                                criteriaDataType = Tabulation.GlobalTabulation.CriteriaValueDescriptionToValueList<double>
                                                            (qType, criteriaValueDescription, out criteriaNumberList);
                                if (criteriaDataType == DataType.NormalData)
                                {
                                    if (criteriaNumberList == null || criteriaNumberList.Length == 0)
                                    {
                                        // エラースロー
                                        return;
                                    }
                                    if (criteriaNumberList.Length == 1)
                                    {
                                        // Filtering011をコール
                                        Tabulation.GlobalTabulation.Filtering(data, qType, criteriaNumberList[0], criteriaOperator, ref FilteringFlag, ope);
                                    }
                                    else
                                    {
                                        // Filtering009をコール
                                        Tabulation.GlobalTabulation.Filtering(data, qType, ref criteriaNumberList, criteriaOperator, ref FilteringFlag, ope);
                                    }
                                }
                                else
                                {
                                    // データ種別によるフィルタリング
                                    // Filtering101を実行
                                    Tabulation.GlobalTabulation.Filtering(data, criteriaDataType, criteriaOperator, ref FilteringFlag, ope);
                                }
                                break;
                        }
                    }
                    else
                    {
                        // データ種別によるフィルタリング
                        // Filtering101を実行
                        Tabulation.GlobalTabulation.Filtering(data, criteriaDataType, criteriaOperator, ref FilteringFlag, ope);
                    }
                    */
                }
            }
        }

        /// <summary>
        /// ツリーに対して再帰的にフィルタリングを行うメソッド<br />
        /// トップレベルにあるCriteriaクラスのインスタンスに対して使用すること
        /// </summary>
        /// <param name="sId">シナリオID</param>
        /// <returns>抽出されたデータはtrue、除かれたデータはfalseとなるbool型の一次元配列 (要素数はレコード数)</returns>
        public bool[] Filtering(decimal? sId = null)
        {
            string delFilePath = null;
            QCWebException e = null;
            if (!Tabulation.CreateTextFile.CreateDeleteFlag(this.TopTableName, dirPath, out delFilePath, out e)) throw e;
            Tabulation.ReadTextFile.ReadDeleteFlag(delFilePath, out e, true);
            if (e != null) throw e;
            bool[] FilteringFlag = null;
            Filtering(ref FilteringFlag, sId);
            return FilteringFlag;
        }

        public bool[] Filtering(string connectionString, string tableName = "answers", System.Data.DataTable dt = null)
        {
            string delFilePath = null;
            QCWebException e = null;
            //if (!Tabulation.CreateTextFile.CreateDeleteFlag(this.TopTableName, dirPath, out delFilePath, out e)) throw e;
            //Tabulation.ReadTextFile.ReadDeleteFlag(delFilePath, out e, true);
            if (e != null) throw e;
            bool[] FilteringFlag = null;
            Filtering(ref FilteringFlag, connectionString, tableName, dt);
            return FilteringFlag;
        }

        public void Filtering(ref bool[] FilteringFlag, string connectionString, string tableName = "answers", System.Data.DataTable dt = null)
        {
            if (subCriterias != null)
            {
                bool[] FilteringFlagSub = null;
                foreach (Criteria subcriteria in subCriterias)
                {
                    subcriteria.Filtering(ref FilteringFlagSub, connectionString, tableName, dt);
                }
                FilteringFlag = performOP(FilteringFlag, FilteringFlagSub, ope);
            }
            else
            {
                string qFilePath = null;
                QCWebException e = null;
                QuestionType qType = Question.QuestionType;
                // if (!Tabulation.CreateTextFile.CreateData(QuestionID, dirPath, out qFilePath, out qType, out e)) throw e;
                // QuestionType qtype = qType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.FA | Tabulation.QuestionType.N);
                // QCAnswerType atype = (QCAnswerType)Enum.Parse(typeof(QCAnswerType), qtype.ToString());
                if (criteriaOperatorDescription != null)
                {
                    //Tabulation.GlobalTabulation.EasyFiltering(
                    //            qFilePath, qType, criteriaValueDescription, criteriaOperatorDescription
                    //            , ref FilteringFlag, ope: ope);
                    int catCnt = question.Sectors == null ? 0 : question.Sectors.Count;
                    Tabulation.GlobalTabulation.EasyFiltering(
                                  QuestionID, qType
                                , criteriaValueDescription
                                , criteriaOperatorDescription
                                , ref FilteringFlag, ope, connectionString, catCnt, tableName, dt);
                }
                else
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullDataMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "criteriaOperatorDescription");
                }
            }
        }

        private bool[] performOP(bool[] filteringFlag, bool[] filteringFlagSub, Operator ope)
        {
            if (null == filteringFlag) return filteringFlagSub;
            if (null == filteringFlagSub) return filteringFlag;
            if (ope == Operator.opAnd)
            {
                for (int i = 0; i < filteringFlag.Length; i++)
                {
                    filteringFlag[i] = filteringFlag[i] & filteringFlagSub[i];

                }
            }
            else
            {
                for (int i = 0; i < filteringFlag.Length; i++)
                {
                    filteringFlag[i] = filteringFlag[i] | filteringFlagSub[i];

                }
            }
            return filteringFlag;

        }

        /// <summary>
        /// ローデータの先頭テーブル名を返す読み取り専用プロパティ
        /// </summary>
        public string TopTableName
        {
            get
            {
                if (question != null) return question.TopTableName;
                if (subCriterias != null && subCriterias.Count > 0)
                {
                    return (subCriterias[0] as Criteria).TopTableName;
                }
                return null;
                /*
                if (topTableName == null)
                {
                    if (subCriterias != null && subCriterias.Count > 0)
                    {
                        topTableName = subCriterias[0].TopTableName;
                    }
                }
                return topTableName;
                */
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public virtual void Dispose()
        {
            if (subCriterias != null)
            {
                for (int i = 0; i < subCriterias.Count; ++i)
                {
                    if (subCriterias[i] != null)
                    {
                        subCriterias[i].Dispose();
                    }
                }
            }
            parentCriteria = null;
        }
    }
}
