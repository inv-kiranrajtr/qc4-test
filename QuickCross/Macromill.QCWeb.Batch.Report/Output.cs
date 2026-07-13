#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Output.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/2/16
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/8
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Runtime.InteropServices;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using Macromill.QCWeb.ReportRequest;
using System.Diagnostics;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Tabulation;
using System.Collections.Generic;

namespace Macromill.QCWeb.Batch.Report
{
    /// <summary>
    /// 出力物のコレクションクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("F3B779DD-6723-4de3-A061-E15E89C3A8A0")]
    public class Outputs : Hashtable, IOutputs
    {
        /// <summary>
        /// 出力物を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("374F521A-88B5-4308-B13D-923200C54423")]
        public class Output : IOutput
        {
            private int index = 0;
            private decimal id = 0;
            //private string[] tsvpaths = null;
            private string xlbooknameprefix = null;
            private DateTime starttime = DateTime.MaxValue;
            private DateTime endpredictiontime = DateTime.MaxValue;
            private DateTime endtime = DateTime.MaxValue;
            private StatusCode statuscode = StatusCode.WaitRequest;
            private string statusdescription = null;
            /// <summary>
            /// 出力物の種類を表すコード
            /// </summary>
            protected OutputType outputtype = (OutputType)(-1);

            private Outputs Collection = null;

            private Tables tables = null;

            /// <summary>
            /// 初期化
            /// </summary>
            /// <param name="outputs">親コレクションへの参照</param>
            /// <param name="id">出力物ID</param>
            /// <param name="joinedtsvpaths">TSVファイルパスを、半角セミコロンで連結した文字列</param>
            /// <param name="xlbooknameprefix">Excelブック名のプリフィックス</param>
            /// <param name="outputtype">出力物の種類を表すOutputType列挙型の値</param>
            protected void Init(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix, OutputType outputtype)
            {
                Collection = outputs;
                index = Collection.Count;
                tables = new Tables(this);
                this.id = id;
                this.outputtype = outputtype;
                if (outputtype == ReportRequest.OutputType.CheckTemplate) return;
                this.xlbooknameprefix = xlbooknameprefix;
                //this.tsvpaths = joinedtsvpaths.Split(';');
                //if (outputtype != ReportRequest.OutputType.RawData)
                //{
                foreach (string tsvpath in joinedtsvpaths.Split(';'))
                {
                    tables.Add(tsvpath, outputtype);
                }
                //}
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            Output(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix, OutputType outputtype)
            {
                Init(outputs, id, joinedtsvpaths, xlbooknameprefix, outputtype);
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            protected Output() { }

            /// <summary>
            /// インデックス番号(0ベース)を返す読み取り専用プロパティ
            /// </summary>
            public int Index
            {
                get
                {
                    return index;
                }
            }

            /// <summary>
            /// 出力物に紐づく集計表コレクションへの参照を返す読み取り専用プロパティ
            /// </summary>
            public ITables Tables
            {
                get
                {
                    return tables;
                }
            }

            /// <summary>
            /// 出力物IDを返す読み取り専用プロパティ
            /// </summary>
            [ComVisible(false)]
            public decimal ID
            {
                get
                {
                    return id;
                }
            }

            /// <summary>
            /// 出力物IDを返す読み取り専用プロパティ
            /// <note>VBAから呼べるようにdouble</note>
            /// </summary>
            public double ID2
            {
                get
                {
                    return (double)id;
                }
            }

            //public string[] TSVPaths
            //{
            //    get
            //    {
            //        return tsvpaths;
            //    }
            //}

            /// <summary>
            /// 出力物のExcelブック名のプリフィックスを返す読み取り専用プロパティ
            /// </summary>
            public string ExcelBookNamePrefix
            {
                get
                {
                    return xlbooknameprefix;
                }
            }

            /// <summary>
            /// 出力物の作成開始時間を取得/設定するプロパティ
            /// </summary>
            public DateTime StartTime
            {
                get
                {
                    return starttime;
                }
                set
                {
                    if (starttime == DateTime.MaxValue)
                    {
                        // 設定処理
                        starttime = value;
                    }
                }
            }

            /// <summary>
            /// 出力物の作成終了予測時間を取得/設定するプロパティ
            /// </summary>
            public DateTime EndPredictionTime
            {
                get
                {
                    return endpredictiontime;
                }
                set
                {
                    // 設定処理
                    endpredictiontime = value;
                }
            }

            /// <summary>
            /// 出力物の作成終了時間を取得/設定するプロパティ
            /// </summary>
            public DateTime EndTime
            {
                get
                {
                    return endtime;
                }
                set
                {
                    if (endtime == DateTime.MaxValue)
                    {
                        endtime = value;
                    }
                }
            }

            /// <summary>
            /// ステータスコードを取得/設定するプロパティ
            /// </summary>
            public StatusCode Status
            {
                get
                {
                    return statuscode;
                }
                set
                {
                    statuscode = value;
                }
            }

            /// <summary>
            /// ステータスコードの追加処理を行うメソッド
            /// </summary>
            /// <param name="statuscode">追加するステータスコード</param>
            public void AddStatus(StatusCode statuscode)
            {
                Status = Status | statuscode;
            }

            /// <summary>
            /// ステータスコードの解除処理を行うメソッド
            /// </summary>
            /// <param name="statuscode">解除するステータスコード</param>
            public void RemoveStatus(StatusCode statuscode)
            {
                Status = Status & ~statuscode;
            }

            /// <summary>
            /// ステータスの説明文字列を取得/設定するプロパティ
            /// </summary>
            public string StatusDescription
            {
                get
                {
                    return statusdescription;
                }
                set
                {
                    // 設定処理
                    statusdescription = value;
                }
            }

            /// <summary>
            /// 出力物の種類を表すOutputType列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public OutputType OutputType
            {
                get
                {
                    return outputtype;
                }
            }

            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                if (tables != null) tables.Dispose();
                Collection = null;
            }

            /// <summary>
            /// 自身のインスタンスが格納されているOutputsコレクションクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IOutputs ParentCollection
            {
                get
                {
                    return Collection;
                }
            }

            /// <summary>
            /// 自身のインスタンスの親であるReportsetクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IReportset ParentReportset
            {
                get
                {
                    if (Collection == null) return null;
                    return Collection.ParentReportset;
                }
            }

            /// <summary>
            /// 自身のインスタンスの親であるRequestクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IRequest ParentRequest
            {
                get
                {
                    if (Collection == null) return null;
                    return Collection.ParentRequest;
                }
            }
        }

        /// <summary>
        /// FAリストの出力物を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("3058F594-48E5-430e-BC07-7EE70A9F5381")]
        public class OutputFA : Output, IOutputFA
        {
            /// <summary>
            /// ページ設定を行うかどうかを表すフラグ
            /// </summary>
            protected bool pagesetup = false;
            private Excel.XlPaperSize papersize = Excel.XlPaperSize.xlPaperA4;
            private Excel.XlPageOrientation paperorientation = Excel.XlPageOrientation.xlPortrait;

            /// <summary>
            /// 初期化
            /// </summary>
            /// <param name="outputs">親コレクションへの参照</param>
            /// <param name="id">出力物ID</param>
            /// <param name="joinedtsvpaths">TSVファイルパスを、半角セミコロンで連結した文字列</param>
            /// <param name="xlbooknameprefix">Excelブック名のプリフィックス</param>
            /// <param name="papersize">用紙サイズ列挙値</param>
            /// <param name="paperorientation">ページの向き列挙値</param>
            /// <param name="outputtype">出力物の種類を表す列挙値</param>
            public void Init(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation, OutputType outputtype)
            {
                base.Init(outputs, id, joinedtsvpaths, xlbooknameprefix, outputtype);
                switch (papersize)
                {
                    case Excel.XlPaperSize.xlPaperA3:
                    case Excel.XlPaperSize.xlPaperA4:
                    case Excel.XlPaperSize.xlPaperB4:
                        if (Enum.IsDefined(typeof(Excel.XlPageOrientation), paperorientation))
                        {
                            pagesetup = true;
                            this.papersize = papersize;
                            this.paperorientation = paperorientation;
                        }
                        break;
                    default:
                        break;
                }
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputFA(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation, OutputType outputtype = OutputType.FAList)
            {
                Init(outputs, id, joinedtsvpaths, xlbooknameprefix, papersize, paperorientation, outputtype);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputFA(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix, OutputType outputtype = OutputType.FAList)
            {
                base.Init(outputs, id, joinedtsvpaths, xlbooknameprefix, outputtype);
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            protected OutputFA() { }

            /// <summary>
            /// ページ設定時の用紙サイズを表すXlPaperSize列挙型の値を取得/設定するプロパティ
            /// </summary>
            public XlPaperSize PaperSize
            {
                get
                {
                    return (XlPaperSize)papersize;
                }
                set
                {
                    if (Enum.IsDefined(typeof(XlPaperSize), value))
                    {
                        pagesetup = true;
                        papersize = (Excel.XlPaperSize)value;
                    }
                }
            }

            /// <summary>
            /// ページ設定時の用紙の向きを表すXlPageOrientation列挙型の値を取得/設定するプロパティ
            /// </summary>
            public XlPageOrientation PaperOrientation
            {
                get
                {
                    return (XlPageOrientation)paperorientation;
                }
                set
                {
                    if (Enum.IsDefined(typeof(Excel.XlPageOrientation), value))
                    {
                        pagesetup = true;
                        paperorientation = (Excel.XlPageOrientation)value;
                    }
                }
            }

            /// <summary>
            /// ページ設定のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool PageSetup
            {
                get
                {
                    return pagesetup;
                }
            }

            string filteringExpression = null;
            string localizedFilteringExpression = null;

            #region ReportRequestのOutputクラスへ移動
            //private void LocalizeFilteringExpression()
            //{
            //    // リソースを使って、絞込み条件式を文章化する処理
            //    localizedFilteringExpression = null;
            //    if (string.IsNullOrWhiteSpace(filteringExpression)) return;
            //    string lccd = ParentRequest.LocationCode;
            //    if (string.IsNullOrWhiteSpace(lccd)) lccd = "ja";
            //    Criteria criteria = new Criteria(filteringExpression, null);
            //    List<ICriteria> criterias = criteria.SubCriterias;
            //    if (criterias == null) return;
            //    if (criterias.Count == 0)
            //    {
            //        criterias = new List<ICriteria>();
            //        criterias.Add(criteria);
            //    }
            //    string[] resultBuf = new string[criterias.Count];
            //    QueryItemName query = new QueryItemName();
            //    // {0}の値
            //    string itemValueFormat = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionItemValueFormatIndex, lccd);
            //    // {0}以上{1}以下
            //    string betweenNumberFormat = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionBetweenNumberFormatIndex, lccd);
            //    // {0}が{1}{2}
            //    string criterionFormat = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionFormatIndex, lccd);
            //    for (int i = 0; i < criterias.Count; ++i)
            //    {
            //        Criteria tmpCriteria = criterias[i] as Criteria;
            //        QuestionType qType = tmpCriteria.QuestionType;
            //        qType &= QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA;
            //        if (!Enum.IsDefined(typeof(QuestionType), qType))
            //        {
            //            // 不正な指定
            //            return;
            //        }
            //        string ope = tmpCriteria.CriteriaOperatorDescription + string.Empty;
            //        switch (ope)
            //        {
            //            case "=":
            //            case "<>":
            //            case ">":
            //            case "<":
            //            case ">=":
            //            case "<=":
            //                break;
            //            case "!=":
            //                ope = "<>";
            //                break;
            //            default:
            //                // 不正な指定
            //                return;
            //        }
            //        string qName = tmpCriteria.Question.Name2;
            //        qName = string.Format(itemValueFormat, qName);
            //        string value = tmpCriteria.CriteriaValueDescription + string.Empty;
            //        QuestionType allowQType = (QuestionType)0;
            //        switch (qType)
            //        {
            //            case QuestionType.SA:
            //            case QuestionType.N:
            //            case QuestionType.FA:
            //                allowQType = QuestionType.SA | QuestionType.N | QuestionType.FA;
            //                break;
            //            case QuestionType.MA:
            //                allowQType = QuestionType.MA;
            //                break;
            //        }
            //        decimal CriteriaQID = query.QuestionNameToID(ParentRequest.QCWebID, allowQType, value);
            //        if (CriteriaQID == (decimal)0)
            //        {
            //            List<string> values = new List<string>();
            //            DataType criteriaDataType = DataType.NormalData;
            //            bool isSingle = true;
            //            bool isRange = false;
            //            switch (qType)
            //            {
            //                case QuestionType.SA:
            //                case QuestionType.MA:
            //                    {
            //                        int[] criteriaValueList = null;
            //                        criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
            //                                                                    qType, value, out criteriaValueList);
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            int s = 0, e = 0;
            //                            isSingle = criteriaValueList != null && criteriaValueList.Length == 1;
            //                            for (int j = 0; j < (criteriaValueList == null ? 0 : criteriaValueList.Length); ++j)
            //                            {
            //                                if (j == 0 || criteriaValueList[j] != criteriaValueList[j - 1] + 1)
            //                                {
            //                                    s = criteriaValueList[j];
            //                                }
            //                                if (j == criteriaValueList.Length - 1 || criteriaValueList[j + 1] != criteriaValueList[j] + 1)
            //                                {
            //                                    e = criteriaValueList[j];
            //                                    if (s == e)
            //                                    {
            //                                        values.Add(s.ToString());
            //                                    }
            //                                    else
            //                                    {
            //                                        values.Add(s.ToString() + "-" + e.ToString());
            //                                    }
            //                                }
            //                            }
            //                        }
            //                        break;
            //                    }
            //                case QuestionType.N:
            //                    {
            //                        NData.ValueRange[] criteriaValueList = null;
            //                        criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<NData.ValueRange>(
            //                                                                    qType, value, out criteriaValueList);
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            isSingle = criteriaValueList != null && criteriaValueList.Length == 1;
            //                            for (int j = 0; j < (criteriaValueList == null ? 0 : criteriaValueList.Length); ++j)
            //                            {
            //                                double min = criteriaValueList[j].MinValue;
            //                                double max = criteriaValueList[j].MaxValue;
            //                                if (min == max)
            //                                {
            //                                    values.Add(min.ToString());
            //                                }
            //                                else
            //                                {
            //                                    values.Add(string.Format(betweenNumberFormat, min.ToString(), max.ToString()));
            //                                    isRange = true;
            //                                }
            //                            }
            //                        }
            //                        break;
            //                    }
            //                case QuestionType.FA:
            //                    {
            //                        if (value.Length == 0 || value.Equals("DK"))
            //                        {
            //                            criteriaDataType = DataType.NAData;
            //                        }
            //                        else
            //                        {
            //                            values.Add("\"" + value + "\"");
            //                        }
            //                        break;
            //                    }
            //            }
            //            if (criteriaDataType != DataType.NormalData)
            //            {
            //                if ((criteriaDataType & DataType.NAData) == DataType.NAData)
            //                {
            //                    // 無回答
            //                    values.Add(GetResource.GetReportKeyword(
            //                            QCWeb.Common.Constants.ReportMessageIndex.ReportNADescriptionKeywordIndex, lccd));
            //                }
            //                if ((criteriaDataType & DataType.IVData) == DataType.IVData)
            //                {
            //                    // 非該当
            //                    values.Add(GetResource.GetReportKeyword(
            //                            QCWeb.Common.Constants.ReportMessageIndex.ReportIVDescriptionKeywordIndex, lccd));
            //                }
            //                isSingle = values.Count == 1;
            //            }
            //            if (values.Count == 0)
            //            {
            //                // 不正な指定
            //                return;
            //            }
            //            value = string.Join("/", values.ToArray());
            //            switch (ope)
            //            {
            //                case "=":
            //                    if (isSingle)
            //                    {
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            switch (qType)
            //                            {
            //                                case QuestionType.SA:
            //                                case QuestionType.N:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionEqualKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.MA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionIncludeKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.FA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionMatchKeywordIndex, lccd);
            //                                    break;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionEqualKeywordIndex, lccd);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            switch (qType)
            //                            {
            //                                case QuestionType.SA:
            //                                case QuestionType.N:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionEqualAnyoneKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.MA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionIncludeAnyoneKeywordIndex, lccd);
            //                                    break;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionEqualAnyoneKeywordIndex, lccd);
            //                        }
            //                    }
            //                    break;
            //                case "<>":
            //                    if (isSingle)
            //                    {
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            switch (qType)
            //                            {
            //                                case QuestionType.SA:
            //                                case QuestionType.N:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotEqualKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.MA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotIncludeKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.FA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionUnmatchKeywordIndex, lccd);
            //                                    break;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotEqualKeywordIndex, lccd);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            switch (qType)
            //                            {
            //                                case QuestionType.SA:
            //                                case QuestionType.N:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotEqualAnyoneKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.MA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotIncludeAnyoneKeywordIndex, lccd);
            //                                    break;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotEqualAnyoneKeywordIndex, lccd);
            //                        }
            //                    }
            //                    break;
            //                default:
            //                    switch (qType)
            //                    {
            //                        case QuestionType.MA:
            //                        case QuestionType.FA:
            //                            // 不正な指定
            //                            return;
            //                    }
            //                    if (criteriaDataType != DataType.NormalData)
            //                    {
            //                        // 不正な指定
            //                        return;
            //                    }
            //                    if (!isSingle || isRange)
            //                    {
            //                        // 不正な指定
            //                        return;
            //                    }
            //                    switch (ope)
            //                    {
            //                        case ">":
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionGreaterKeywordIndex, lccd);
            //                            break;
            //                        case "<":
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionLessKeywordIndex, lccd);
            //                            break;
            //                        case ">=":
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionGreaterEqualKeywordIndex, lccd);
            //                            break;
            //                        case "<=":
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionLessEqualKeywordIndex, lccd);
            //                            break;
            //                    }
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            Question.Questions tmpQs = new Question.Questions(0, CriteriaQID);
            //            Question.Questions.Question criteriaQ = tmpQs[CriteriaQID] as Question.Questions.Question;
            //            value = criteriaQ.Name2;
            //            value = string.Format(itemValueFormat, qName);
            //            switch (ope)
            //            {
            //                case "=":
            //                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionMatchKeywordIndex, lccd);
            //                    break;
            //                case "<>":
            //                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionUnmatchKeywordIndex, lccd);
            //                    break;
            //                default:
            //                    switch (qType)
            //                    {
            //                        case QuestionType.SA:
            //                        case QuestionType.N:
            //                            switch (ope)
            //                            {
            //                                case ">":
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionGreaterKeywordIndex, lccd);
            //                                    break;
            //                                case "<":
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionLessKeywordIndex, lccd);
            //                                    break;
            //                                case ">=":
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionGreaterEqualKeywordIndex, lccd);
            //                                    break;
            //                                case "<=":
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionLessEqualKeywordIndex, lccd);
            //                                    break;
            //                            }
            //                            break;
            //                        case QuestionType.MA:
            //                        case QuestionType.FA:
            //                            // 不正な指定
            //                            return;
            //                    }
            //                    break;
            //            }
            //        }
            //        if (i > 0)
            //        {
            //            resultBuf[i - 1] += GetResource.GetReportKeyword(
            //                                    tmpCriteria.Operator == Operator.opAnd
            //                                        ? QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionOperatorAndKeywordIndex
            //                                        : QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionOperatorOrKeywordIndex
            //                                  , lccd);
            //        }
            //        resultBuf[i] = string.Format(criterionFormat, qName, value, ope);
            //    }
            //    localizedFilteringExpression = string.Join("\n", resultBuf);
            //}
            #endregion

            /// <summary>
            /// 絞込み条件式を返す読み取り専用プロパティ
            /// </summary>
            [ComVisible(false)]
            public string FilteringExpression
            {
                get
                {
                    return filteringExpression;
                }
                set
                {
                    filteringExpression = value;
                    localizedFilteringExpression = value;
                    //LocalizeFilteringExpression();
                }
            }

            /// <summary>
            /// ロケールに応じて文章化された絞込み条件式を返す読み取り専用プロパティ
            /// </summary>
            public string LocalizedFilteringExpression
            {
                get
                {
                    return localizedFilteringExpression;
                }
            }

            ///// <summary>
            ///// 絞込み条件式を、ロケールに応じて文章化したものを返すメソッド
            ///// </summary>
            ///// <param name="lccd">ロケールコード (省略可、省略時ja)</param>
            ///// <returns>ローカライズ済みの絞込み条件文</returns>
            //public string LocalizedFilteringExpression(string lccd = "ja")
            //{
            //    // リソースを使って、絞込み条件式を文章化する処理
            //    if (string.IsNullOrWhiteSpace(filteringExpression)) return filteringExpression;
            //    Criteria criteria = new Criteria(filteringExpression, null);
            //    List<ICriteria> criterias = criteria.SubCriterias;
            //    if (criterias.Count == 0)
            //    {
            //        criterias = new List<ICriteria>();
            //        criterias.Add(criteria);
            //    }
            //    string[] returnBuf = new string[criterias.Count];
            //    QueryItemName query = new QueryItemName();
            //    // {0}の値
            //    string itemValueFormat = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionItemValueFormatIndex, lccd);
            //    // {0}以上{1}以下
            //    string betweenNumberFormat = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionBetweenNumberFormatIndex, lccd);
            //    // {0}が{1}{2}
            //    string criterionFormat = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionFormatIndex, lccd);
            //    for (int i = 0; i < criterias.Count; ++i)
            //    {
            //        Criteria tmpCriteria = criterias[i] as Criteria;
            //        QuestionType qType = tmpCriteria.QuestionType;
            //        qType &= QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA;
            //        if (!Enum.IsDefined(typeof(QuestionType), qType))
            //        {
            //            // 不正な指定
            //            // return filteringExpression;
            //            return null;
            //        }
            //        string ope = tmpCriteria.CriteriaOperatorDescription + string.Empty;
            //        switch (ope)
            //        {
            //            case "=":
            //            case "<>":
            //            case ">":
            //            case "<":
            //            case ">=":
            //            case "<=":
            //                break;
            //            case "!=":
            //                ope = "<>";
            //                break;
            //            default:
            //                // 不正な指定
            //                // return filteringExpression;
            //                return null;
            //        }
            //        string qName = tmpCriteria.Question.Name2;
            //        qName = string.Format(itemValueFormat, qName);
            //        string value = tmpCriteria.CriteriaValueDescription + string.Empty;
            //        QuestionType allowQType = (QuestionType)0;
            //        switch (qType)
            //        {
            //            case QuestionType.SA:
            //            case QuestionType.N:
            //            case QuestionType.FA:
            //                allowQType = QuestionType.SA | QuestionType.N | QuestionType.FA;
            //                break;
            //            case QuestionType.MA:
            //                allowQType = QuestionType.MA;
            //                break;
            //        }
            //        decimal CriteriaQID = query.QuestionNameToID(ParentRequest.QCWebID, allowQType, value);
            //        if (CriteriaQID == (decimal)0)
            //        {
            //            List<string> values = new List<string>();
            //            DataType criteriaDataType = DataType.NormalData;
            //            bool isSingle = true;
            //            bool isRange = false;
            //            switch (qType)
            //            {
            //                case QuestionType.SA:
            //                case QuestionType.MA:
            //                    {
            //                        int[] criteriaValueList = null;
            //                        criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
            //                                                                    qType, value, out criteriaValueList);
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            int s = 0, e = 0;
            //                            isSingle = criteriaValueList != null && criteriaValueList.Length == 1;
            //                            for (int j = 0; j < (criteriaValueList == null ? 0 : criteriaValueList.Length); ++j)
            //                            {
            //                                if (j == 0 || criteriaValueList[j] != criteriaValueList[j - 1] + 1)
            //                                {
            //                                    s = criteriaValueList[j];
            //                                }
            //                                if (j == criteriaValueList.Length - 1 || criteriaValueList[j + 1] != criteriaValueList[j] + 1)
            //                                {
            //                                    e = criteriaValueList[j];
            //                                    if (s == e)
            //                                    {
            //                                        values.Add(s.ToString());
            //                                    }
            //                                    else
            //                                    {
            //                                        values.Add(s.ToString() + "-" + e.ToString());
            //                                    }
            //                                }
            //                            }
            //                        }
            //                        break;
            //                    }
            //                case QuestionType.N:
            //                    {
            //                        NData.ValueRange[] criteriaValueList = null;
            //                        criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<NData.ValueRange>(
            //                                                                    qType, value, out criteriaValueList);
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            isSingle = criteriaValueList != null && criteriaValueList.Length == 1;
            //                            for (int j = 0; j < (criteriaValueList == null ? 0 : criteriaValueList.Length); ++j)
            //                            {
            //                                double min = criteriaValueList[j].MinValue;
            //                                double max = criteriaValueList[j].MaxValue;
            //                                if (min == max)
            //                                {
            //                                    values.Add(min.ToString());
            //                                }
            //                                else
            //                                {
            //                                    values.Add(string.Format(betweenNumberFormat, min.ToString(), max.ToString()));
            //                                    isRange = true;
            //                                }
            //                            }
            //                        }
            //                        break;
            //                    }
            //                case QuestionType.FA:
            //                    {
            //                        if (value.Length == 0 || value.Equals("DK"))
            //                        {
            //                            criteriaDataType = DataType.NAData;
            //                        }
            //                        else
            //                        {
            //                            values.Add("\"" + value + "\"");
            //                        }
            //                        break;
            //                    }
            //            }
            //            if (criteriaDataType != DataType.NormalData)
            //            {
            //                if ((criteriaDataType & DataType.NAData) == DataType.NAData)
            //                {
            //                    // 無回答
            //                    values.Add(GetResource.GetReportKeyword(
            //                            QCWeb.Common.Constants.ReportMessageIndex.ReportNADescriptionKeywordIndex, lccd));
            //                }
            //                if ((criteriaDataType & DataType.IVData) == DataType.IVData)
            //                {
            //                    // 非該当
            //                    values.Add(GetResource.GetReportKeyword(
            //                            QCWeb.Common.Constants.ReportMessageIndex.ReportIVDescriptionKeywordIndex, lccd));
            //                }
            //                isSingle = values.Count == 1;
            //            }
            //            if (values.Count == 0)
            //            {
            //                // 不正な指定
            //                // return filteringExpression;
            //                return null;
            //            }
            //            value = string.Join("/", values.ToArray());
            //            switch (ope)
            //            {
            //                case "=":
            //                    if (isSingle)
            //                    {
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            switch (qType)
            //                            {
            //                                case QuestionType.SA:
            //                                case QuestionType.N:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionEqualKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.MA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionIncludeKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.FA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionMatchKeywordIndex, lccd);
            //                                    break;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionEqualKeywordIndex, lccd);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            switch (qType)
            //                            {
            //                                case QuestionType.SA:
            //                                case QuestionType.N:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionEqualAnyoneKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.MA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionIncludeAnyoneKeywordIndex, lccd);
            //                                    break;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionEqualAnyoneKeywordIndex, lccd);
            //                        }
            //                    }
            //                    break;
            //                case "<>":
            //                    if (isSingle)
            //                    {
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            switch (qType)
            //                            {
            //                                case QuestionType.SA:
            //                                case QuestionType.N:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotEqualKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.MA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotIncludeKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.FA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionUnmatchKeywordIndex, lccd);
            //                                    break;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotEqualKeywordIndex, lccd);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (criteriaDataType == DataType.NormalData)
            //                        {
            //                            switch (qType)
            //                            {
            //                                case QuestionType.SA:
            //                                case QuestionType.N:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotEqualAnyoneKeywordIndex, lccd);
            //                                    break;
            //                                case QuestionType.MA:
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotIncludeAnyoneKeywordIndex, lccd);
            //                                    break;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionNotEqualAnyoneKeywordIndex, lccd);
            //                        }
            //                    }
            //                    break;
            //                default:
            //                    switch (qType)
            //                    {
            //                        case QuestionType.MA:
            //                        case QuestionType.FA:
            //                            // 不正な指定
            //                            // return filteringExpression;
            //                            return null;
            //                    }
            //                    if (criteriaDataType != DataType.NormalData)
            //                    {
            //                        // 不正な指定
            //                        // return filteringExpression;
            //                        return null;
            //                    }
            //                    if (!isSingle || isRange)
            //                    {
            //                        // 不正な指定
            //                        // return filteringExpression;
            //                        return null;
            //                    }
            //                    switch (ope)
            //                    {
            //                        case ">":
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionGreaterKeywordIndex, lccd);
            //                            break;
            //                        case "<":
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionLessKeywordIndex, lccd);
            //                            break;
            //                        case ">=":
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionGreaterEqualKeywordIndex, lccd);
            //                            break;
            //                        case "<=":
            //                            ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionLessEqualKeywordIndex, lccd);
            //                            break;                                    
            //                    }
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            Question.Questions tmpQs = new Question.Questions(0, CriteriaQID);
            //            Question.Questions.Question criteriaQ = tmpQs[CriteriaQID] as Question.Questions.Question;
            //            value = criteriaQ.Name2;
            //            value = string.Format(itemValueFormat, qName);
            //            switch (ope)
            //            {
            //                case "=":
            //                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionMatchKeywordIndex, lccd);
            //                    break;
            //                case "<>":
            //                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionUnmatchKeywordIndex, lccd);
            //                    break;
            //                default:
            //                    switch (qType)
            //                    {
            //                        case QuestionType.SA:
            //                        case QuestionType.N:
            //                            switch (ope)
            //                            {
            //                                case ">":
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionGreaterKeywordIndex, lccd);
            //                                    break;
            //                                case "<":
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionLessKeywordIndex, lccd);
            //                                    break;
            //                                case ">=":
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionGreaterEqualKeywordIndex, lccd);
            //                                    break;
            //                                case "<=":
            //                                    ope = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionLessEqualKeywordIndex, lccd);
            //                                    break;
            //                            }
            //                            break;
            //                        case QuestionType.MA:
            //                        case QuestionType.FA:
            //                            // 不正な指定
            //                            // return filteringExpression;
            //                            return null;
            //                    }
            //                    break;
            //            }
            //        }
            //        if (i > 0)
            //        {
            //            returnBuf[i - 1] += GetResource.GetReportKeyword(
            //                                    tmpCriteria.Operator == Operator.opAnd
            //                                        ? QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionOperatorAndKeywordIndex 
            //                                        : QCWeb.Common.Constants.ReportMessageIndex.ReportCriterionOperatorOrKeywordIndex
            //                                  , lccd);
            //        }
            //        returnBuf[i] = string.Format(criterionFormat, qName, value, ope);
            //    }
            //    return string.Join("\n", returnBuf);
            //}
        }

        /// <summary>
        /// GT表の出力物を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("A47004AD-3C0A-4ab1-8340-A891CF839372")]
        public class OutputGT : OutputFA, IOutputGT
        {
            private TableType tabletype = TableType.NPer | TableType.N | TableType.Per;
            private TableOrientation tableorientation = TableOrientation.Landscape;
            private TableType pagesetuptabletype = (TableType)0;
            private int minsamplescountonmarking = 0;
            private MarkingType markingtype = (MarkingType)0;
            private SignificanceTestLevel significancetestlevel = (SignificanceTestLevel)0;
            private bool preWbBase = true;

            #region コンストラクタ
#if FOR_UNIT_TEST
            public
#else
            private
#endif
            void init(TableType tabletype, TableOrientation tableorientation)
            {
                if ((int)this.outputtype == -1) this.outputtype = OutputType.GT;
                tabletype &= TableType.NPer | TableType.N | TableType.Per;
                if ((int)tabletype != 0)
                {
                    this.tabletype = tabletype;
                }
                if (tableorientation == TableOrientation.Portrait)
                {
                    this.tableorientation = tableorientation;
                }
            }

#if FOR_UNIT_TEST
            public
#else
            private
#endif
            void initpagesetup(TableType pagesetuptabletype)
            {
                pagesetuptabletype &= this.tabletype;
                this.pagesetuptabletype = pagesetuptabletype;
                this.pagesetup = (int)pagesetuptabletype != 0;
            }

#if FOR_UNIT_TEST
            public
#else
            private
#endif
            void initmarking(MarkingType markingtype, int minsamplescountonmarking)
            {
                this.markingtype = markingtype & (MarkingType.Significance | MarkingType.Coloring | MarkingType.Ranking | MarkingType.Ascending);
                if (minsamplescountonmarking < 0)
                {
                    minsamplescountonmarking = 0;
                }
                this.minsamplescountonmarking = minsamplescountonmarking;
            }

#if FOR_UNIT_TEST
            public
#else
            private
#endif
            void initsignificancetestlevel(SignificanceTestLevel significancetestlevel)
            {
                significancetestlevel &= SignificanceTestLevel.One | SignificanceTestLevel.Five | SignificanceTestLevel.Ten;
                if (significancetestlevel != (SignificanceTestLevel.One | SignificanceTestLevel.Five | SignificanceTestLevel.Ten))
                {
                    this.significancetestlevel = significancetestlevel;
                }
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputGT(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking
                    , SignificanceTestLevel significancetestlevel, OutputType outputtype = OutputType.GT)
            {
                init(tabletype, tableorientation);
                base.Init(outputs, id, joinedtsvpaths, xlbooknameprefix, papersize, paperorientation, outputtype);
                initpagesetup(pagesetuptabletype);
                initmarking(markingtype, minsamplescountonmarking);
                initsignificancetestlevel(significancetestlevel);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputGT(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , MarkingType markingtype, int minsamplescountonmarking
                    , SignificanceTestLevel significancetestlevel, OutputType outputtype = OutputType.GT)
            {
                init(tabletype, tableorientation);
                base.Init(outputs, id, joinedtsvpaths, xlbooknameprefix, outputtype);
                initmarking(markingtype, minsamplescountonmarking);
                initsignificancetestlevel(significancetestlevel);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputGT(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking, OutputType outputtype = OutputType.GT)
            {
                init(tabletype, tableorientation);
                base.Init(outputs, id, joinedtsvpaths, xlbooknameprefix, papersize, paperorientation, outputtype);
                initpagesetup(pagesetuptabletype);
                initmarking(markingtype, minsamplescountonmarking);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputGT(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel, OutputType outputtype = OutputType.GT)
            {
                init(tabletype, tableorientation);
                base.Init(outputs, id, joinedtsvpaths, xlbooknameprefix, papersize, paperorientation, outputtype);
                initpagesetup(pagesetuptabletype);
                initsignificancetestlevel(significancetestlevel);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputGT(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , MarkingType markingtype, int minsamplescountonmarking, OutputType outputtype = OutputType.GT)
            {
                init(tabletype, tableorientation);
                base.Init(outputs, id, joinedtsvpaths, xlbooknameprefix, outputtype);
                initmarking(markingtype, minsamplescountonmarking);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputGT(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , SignificanceTestLevel significancetestlevel, OutputType outputtype = OutputType.GT)
            {
                init(tabletype, tableorientation);
                base.Init(outputs, id, joinedtsvpaths, xlbooknameprefix, outputtype);
                initsignificancetestlevel(significancetestlevel);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputGT(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , OutputType outputtype = OutputType.GT)
            {
                init(tabletype, tableorientation);
                base.Init(outputs, id, joinedtsvpaths, xlbooknameprefix, papersize, paperorientation, outputtype);
                initpagesetup(pagesetuptabletype);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputGT(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation, OutputType outputtype = OutputType.GT)
            {
                init(tabletype, tableorientation);
                base.Init(outputs, id, joinedtsvpaths, xlbooknameprefix, outputtype);
            }
            #endregion

            /// <summary>
            /// N％表作成のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool OutputNPerTable
            {
                get
                {
                    return (tabletype & TableType.NPer) == TableType.NPer;
                }
            }

            /// <summary>
            /// N表作成のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool OutputNTable
            {
                get
                {
                    return (tabletype & TableType.N) == TableType.N;
                }
            }

            /// <summary>
            /// ％表作成のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool OutputPerTable
            {
                get
                {
                    return (tabletype & TableType.Per) == TableType.Per;
                }
            }

            /// <summary>
            /// 作成する集計表の向きを表すTableOrientation列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public TableOrientation Orientation
            {
                get
                {
                    return tableorientation;
                }
            }

            /// <summary>
            /// N％表のページ設定のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool PageSetupNPerTable
            {
                get
                {
                    return (pagesetuptabletype & TableType.NPer) == TableType.NPer;
                }
            }

            /// <summary>
            /// N表のページ設定のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool PageSetupNTable
            {
                get
                {
                    return (pagesetuptabletype & TableType.N) == TableType.N;
                }
            }

            /// <summary>
            /// ％表のページ設定のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool PageSetupPerTable
            {
                get
                {
                    return (pagesetuptabletype & TableType.Per) == TableType.Per;
                }
            }

            /// <summary>
            /// 項目間検定表のページ設定のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool PageSetupSignificanceTestTable
            {
                get
                {
                    return (pagesetuptabletype & TableType.SignificanceTest) == TableType.SignificanceTest;
                }
            }

            /// <summary>
            /// 色付けのマーキングを行うかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool MarkingColoring
            {
                get
                {
                    return (int)(markingtype & MarkingType.Coloring) != 0;
                }
            }

            /// <summary>
            /// 水準1での色付けのマーキングを行うかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool MarkingColoringLevel1
            {
                get
                {
                    return (int)(markingtype & MarkingType.ColoringLevel1) != 0;
                }
            }

            /// <summary>
            /// 水準2での色付けのマーキングを行うかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool MarkingColoringLevel2
            {
                get
                {
                    return (int)(markingtype & MarkingType.ColoringLevel2) != 0;
                }
            }

            /// <summary>
            /// ランキングのマーキングを行うかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool MarkingRanking
            {
                get
                {
                    return (markingtype & MarkingType.Ranking) == MarkingType.Ranking;
                }
            }

            /// <summary>
            /// 昇降分析のマーキングを行うかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool MarkingAscending
            {
                get
                {
                    return (markingtype & MarkingType.Ascending) == MarkingType.Ascending;
                }
            }

            /// <summary>
            /// 有意差検定のマーキングを行うかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool MarkingSignificance
            {
                get
                {
                    return (int)(markingtype & MarkingType.Significance) != 0;
                }
            }

            /// <summary>
            /// 1％有意差検定のマーキングを行うかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool MarkingSignificanceOne
            {
                get
                {
                    return (markingtype & MarkingType.SignificanceOne) == MarkingType.SignificanceOne;
                }
            }

            /// <summary>
            /// 5％有意差検定のマーキングを行うかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool MarkingSignificanceFive
            {
                get
                {
                    return (markingtype & MarkingType.SignificanceFive) == MarkingType.SignificanceFive;
                }
            }

            /// <summary>
            /// 10％有意差検定のマーキングを行うかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool MarkingSignificanceTen
            {
                get
                {
                    return (markingtype & MarkingType.SignificanceTen) == MarkingType.SignificanceTen;
                }
            }

            /// <summary>
            /// マーキングを行う条件とする母数の最小値を返す読み取り専用プロパティ
            /// </summary>
            public int MinSamplesCountOnMarking
            {
                get
                {
                    return minsamplescountonmarking;
                }
            }

            /// <summary>
            /// 項目間検定のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool SignificanceTest
            {
                get
                {
                    int sigTestLv = (int)significancetestlevel;
                    return sigTestLv >= (int)SignificanceTestLevel.One
                            && sigTestLv < (int)(SignificanceTestLevel.One | SignificanceTestLevel.Five | SignificanceTestLevel.Ten);
                }
            }

            /// <summary>
            /// 有意水準1％での項目間検定のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool SignificanceTestOne
            {
                get
                {
                    return (significancetestlevel & SignificanceTestLevel.One) == SignificanceTestLevel.One;
                }
            }

            /// <summary>
            /// 有意水準5％での項目間検定のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool SignificanceTestFive
            {
                get
                {
                    return (significancetestlevel & SignificanceTestLevel.Five) == SignificanceTestLevel.Five;
                }
            }

            /// <summary>
            /// 有意水準10％での項目間検定のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool SignificanceTestTen
            {
                get
                {
                    return (significancetestlevel & SignificanceTestLevel.Ten) == SignificanceTestLevel.Ten;
                }
            }

            /// <summary>
            /// 水準1高での色付けのマーキングを行うかどうかを取得/設定するプロパティ
            /// </summary>
            public bool MarkingColoringLevel1High
            {
                get
                {
                    return (markingtype & MarkingType.ColoringLevel1High) == MarkingType.ColoringLevel1High;
                }
            }

            /// <summary>
            /// 水準1低での色付けのマーキングを行うかどうかを取得/設定するプロパティ
            /// </summary>
            public bool MarkingColoringLevel1Low
            {
                get
                {
                    return (markingtype & MarkingType.ColoringLevel1Low) == MarkingType.ColoringLevel1Low;
                }
            }

            /// <summary>
            /// 水準2高での色付けのマーキングを行うかどうかを取得/設定するプロパティ
            /// </summary>
            public bool MarkingColoringLevel2High
            {
                get
                {
                    return (markingtype & MarkingType.ColoringLevel2High) == MarkingType.ColoringLevel2High;
                }
            }

            /// <summary>
            /// 水準2低での色付けのマーキングを行うかどうかを取得/設定するプロパティ
            /// </summary>
            public bool MarkingColoringLevel2Low
            {
                get
                {
                    return (markingtype & MarkingType.ColoringLevel2Low) == MarkingType.ColoringLevel2Low;
                }
            }

            private ShowCode shownacd = (ShowCode)0;  // 無回答表示コード
            internal ShowCode ShowNACode
            {
                set
                {
                    shownacd = value & (ShowCode.Item | ShowCode.Axis);
                }
            }

            /// <summary>
            /// 集計対象の無回答を表示するかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool ShowNAAtItem
            {
                get
                {
                    return (shownacd & ShowCode.Item) == ShowCode.Item;
                }
                internal set
                {
                    if (value)
                    {
                        shownacd |= ShowCode.Item;
                    }
                    else
                    {
                        shownacd &= ~ShowCode.Item;
                    }
                }
            }

            /// <summary>
            /// 集計軸の無回答を表示するかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool ShowNAAtAxis
            {
                get
                {
                    return (shownacd & ShowCode.Axis) == ShowCode.Axis;
                }
                internal set
                {
                    if (value)
                    {
                        shownacd |= ShowCode.Axis;
                    }
                    else
                    {
                        shownacd &= ~ShowCode.Axis;
                    }

                }
            }

            private ShowCode showivcd = (ShowCode)0;  // 非該当表示コード
            internal ShowCode ShowIVCode
            {
                set
                {
                    showivcd = value & (ShowCode.Item | ShowCode.Axis);
                }
            }
            /// <summary>
            /// 集計対象の非該当を表示するかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool ShowIVAtItem
            {
                get
                {
                    return (showivcd & ShowCode.Item) == ShowCode.Item;
                }
                internal set
                {
                    if (value)
                    {
                        showivcd |= ShowCode.Item;
                    }
                    else
                    {
                        showivcd &= ~ShowCode.Item;
                    }
                }
            }

            /// <summary>
            /// 集計軸の非該当を表示するかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool ShowIVAtAxis
            {
                get
                {
                    return (showivcd & ShowCode.Axis) == ShowCode.Axis;
                }
                internal set
                {
                    if (value)
                    {
                        showivcd |= ShowCode.Axis;
                    }
                    else
                    {
                        showivcd &= ~ShowCode.Axis;
                    }
                }
            }

            private WBSettingCode wbcd = WBSettingCode.WBOff; // WB集計表示コード
            internal WBSettingCode WBSettingCode
            {
                set
                {
                    if ((value & ReportRequest.WBSettingCode.WBOn) == ReportRequest.WBSettingCode.WBOn)
                    {
                        wbcd = value & (ReportRequest.WBSettingCode.WBOn | ReportRequest.WBSettingCode.ShowPreWB);
                    }
                    else
                    {
                        wbcd = ReportRequest.WBSettingCode.WBOff;
                    }
                }
            }
            /// <summary>
            /// WB前全体を表示するかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool ShowPreWBTotal
            {
                get
                {
                    return (wbcd & WBSettingCode.ShowPreWB) == WBSettingCode.ShowPreWB;
                }
            }

            /// <summary>
            /// WB集計のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool WBOn
            {
                get
                {
                    return (wbcd & WBSettingCode.WBOn) == WBSettingCode.WBOn;
                }
            }

            public bool PreWbBase { get => preWbBase; set => preWbBase = value; }

        }

        /// <summary>
        /// クロス集計表の出力物を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("C744256D-BE45-42da-827B-661A15AB4CFB")]
        public class OutputCross : OutputGT, IOutputCross
        {
            private TablesOnOneSheet tablesononesheet = TablesOnOneSheet.Multi;
            private int level2highcolorindex = int.Parse(GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportHatchingLevel2HighColorIndexDefaultIndex));
            private int level1highcolorindex = int.Parse(GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportHatchingLevel1HighColorIndexDefaultIndex));
            private int level1lowcolorindex = int.Parse(GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportHatchingLevel1LowColorIndexDefaultIndex));
            private int level2lowcolorindex = int.Parse(GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportHatchingLevel2LowColorIndexDefaultIndex));
            private int level1percent = int.Parse(GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportHatchingLevel1PercentDefaultIndex));
            private int level2percent = int.Parse(GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportHatchingLevel2PercentDefaultIndex));

            #region コンストラクタ
#if FOR_UNIT_TEST
            public
#else
            private
#endif
            void init(TablesOnOneSheet tablesononesheet)
            {
                if ((int)this.outputtype == -1) this.outputtype = OutputType.Cross;
                if (tablesononesheet == TablesOnOneSheet.Single)
                {
                    this.tablesononesheet = tablesononesheet;
                }
            }

#if FOR_UNIT_TEST
            public
#else
            private
#endif
            void initmarkingcolorindex(int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex)
            {
                if (level2highcolorindex >= 0)//&& level2highcolorindex <= 56)
                {
                    this.level2highcolorindex = level2highcolorindex;
                }
                if (level1highcolorindex >= 0)//&& level1highcolorindex <= 56)
                {
                    this.level1highcolorindex = level1highcolorindex;
                }
                if (level1lowcolorindex >= 0)//&& level1lowcolorindex <= 56)
                {
                    this.level1lowcolorindex = level1lowcolorindex;
                }
                if (level2lowcolorindex >= 0)//&& level2lowcolorindex <= 56)
                {
                    this.level2lowcolorindex = level2lowcolorindex;
                }
            }

#if FOR_UNIT_TEST
            public
#else
            private
#endif
            void initcoloringpercent(int level1percent, int level2percent)
            {
                // 上限下限値はスタティック
                const int MAX_PERCENT = 30;
                const int MIN_PERCENT = 1;
                if (level1percent >= MIN_PERCENT && level1percent <= MAX_PERCENT
                    && level2percent >= MIN_PERCENT && level2percent <= MAX_PERCENT)
                {
                    this.level1percent = level1percent;
                    this.level2percent = level2percent;
                }
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, markingtype, minsamplescountonmarking, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
                initmarkingcolorindex(level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, markingtype, minsamplescountonmarking, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
                initmarkingcolorindex(level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, markingtype, minsamplescountonmarking, OutputType.Cross)
            {
                init(tablesononesheet);
                initmarkingcolorindex(level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, markingtype, minsamplescountonmarking, OutputType.Cross)
            {
                init(tablesononesheet);
                initmarkingcolorindex(level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, markingtype, minsamplescountonmarking, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, markingtype, minsamplescountonmarking, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, markingtype, minsamplescountonmarking, OutputType.Cross)
            {
                init(tablesononesheet);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, markingtype, minsamplescountonmarking, OutputType.Cross)
            {
                init(tablesononesheet);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, OutputType.Cross)
            {
                init(tablesononesheet);
                initcoloringpercent(level1percent, level2percent);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , int level1percent, int level2percent)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, OutputType.Cross)
            {
                init(tablesononesheet);
                initcoloringpercent(level1percent, level2percent);
            }


#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, markingtype, minsamplescountonmarking, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
                initmarkingcolorindex(level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, markingtype, minsamplescountonmarking, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
                initmarkingcolorindex(level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, markingtype, minsamplescountonmarking, OutputType.Cross)
            {
                init(tablesononesheet);
                initmarkingcolorindex(level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, markingtype, minsamplescountonmarking, OutputType.Cross)
            {
                init(tablesononesheet);
                initmarkingcolorindex(level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, markingtype, minsamplescountonmarking, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, markingtype, minsamplescountonmarking, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, markingtype, minsamplescountonmarking, OutputType.Cross)
            {
                init(tablesononesheet);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , MarkingType markingtype, int minsamplescountonmarking)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, markingtype, minsamplescountonmarking, OutputType.Cross)
            {
                init(tablesononesheet);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, significancetestlevel, OutputType.Cross)
            {
                init(tablesononesheet);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation, OutputType.Cross)
            {
                init(tablesononesheet);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCross(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation, OutputType.Cross)
            {
                init(tablesononesheet);
            }
            #endregion

            /// <summary>
            /// 1つのワークシートに出力する集計表の数を表すTablesOnOneSheet列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public TablesOnOneSheet TablesOnOnesheet
            {
                get
                {
                    return tablesononesheet;
                }
            }

            /// <summary>
            /// 水準2高(+10％)の色付けで使用する色インデックスを表す数値を返す読み取り専用プロパティ
            /// </summary>
            public int Level2HighColorIndex
            {
                get
                {
                    return level2highcolorindex;
                }
            }

            /// <summary>
            /// 水準1高(+5％)の色付けで使用する色インデックスを表す数値を返す読み取り専用プロパティ
            /// </summary>
            public int Level1HighColorIndex
            {
                get
                {
                    return level1highcolorindex;
                }
            }

            /// <summary>
            /// 水準1低(-5％)の色付けで使用する色インデックスを表す数値を返す読み取り専用プロパティ
            /// </summary>
            public int Level1LowColorIndex
            {
                get
                {
                    return level1lowcolorindex;
                }
            }

            /// <summary>
            /// 水準2低(-10％)の色付けで使用する色インデックスを表す数値を返す読み取り専用プロパティ
            /// </summary>
            public int Level2LowColorIndex
            {
                get
                {
                    return level2lowcolorindex;
                }
            }

            /// <summary>
            /// 水準1のパーセンテージを返す読み取り専用プロパティ
            /// </summary>
            public int Level1Percent
            {
                get
                {
                    return level1percent;
                }
            }

            /// <summary>
            /// 水準2のパーセンテージを返す読み取り専用プロパティ
            /// </summary>
            public int Level2Percent
            {
                get
                {
                    return level2percent;
                }
            }


        }

        /// <summary>
        /// チェックリストの出力物を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("BF88EFEC-E915-4688-9BA4-D0DFEB091F9D")]
        public class OutputCheckList : Output, IOutputCheckList
        {
            private int totalcount = 0;

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputCheckList(Outputs outputs, decimal id, string joinedtsvpaths, string xlbooknameprefix, int totalcount)
                : base(outputs, id, joinedtsvpaths, xlbooknameprefix, OutputType.CheckList)
            {
                if (totalcount > 0)
                {
                    this.totalcount = totalcount;
                }
            }

            /// <summary>
            /// 全体数(サンプル数)を返す読み取り専用プロパティ
            /// </summary>
            public int TotalCount
            {
                get
                {
                    return totalcount;
                }
            }
        }

        /// <summary>
        /// ローデータの出力物を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("8F0E84C6-2346-42F7-A92F-2B3AEB1E21DE")]
        public class OutputRawData : Output
        {
#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            OutputRawData(Outputs outputs, decimal id, string tsvpath, string xlbooknameprefix, bool isQC3 = false)
                : base(outputs, id, tsvpath, xlbooknameprefix, isQC3 ? OutputType.QC3 : OutputType.RawData)
            {
            }

            private string qc3bookpassword = null;
            /// <summary>
            /// QC3ファイルのブックのパスワードを取得/設定するプロパティ
            /// </summary>
            public string QC3BookPassword
            {
                get
                {
                    return qc3bookpassword;
                }
                internal set
                {
                    qc3bookpassword = value;
                }
            }

            private string qc3sheetpassword = null;
            /// <summary>
            /// QC3ファイルのシートのパスワードを取得/設定するプロパティ
            /// </summary>
            public string QC3SheetPassword
            {
                get
                {
                    return qc3sheetpassword;
                }
                internal set
                {
                    qc3sheetpassword = value;
                }
            }

            private string qc3readpassword = null;
            /// <summary>
            /// QC3ファイルの読み取りパスワードを取得/設定するプロパティ
            /// </summary>
            public string QC3ReadPassword
            {
                get
                {
                    return qc3readpassword;
                }
                internal set
                {
                    qc3readpassword = value;
                }
            }

            private bool utf8flag = false;
            /// <summary>
            /// UTF-8フラグを返す読み取り専用プロパティ
            /// </summary>
            public bool IsUTF8
            {
                get
                {
                    return utf8flag;
                }
                internal set
                {
                    utf8flag = value;
                }
            }

        }

        private Reportsets.Reportset reportset = null;

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        Outputs(Reportsets.Reportset reportset)
        {
            this.reportset = reportset;
        }

        #region FAリストページ設定なし出力時および調査票出力時使用
#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        Output Add(decimal id, string joinedtsvpaths, string xlbooknameprefix, OutputType outputtype)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                Output newOutput = null;
                switch (outputtype)
                {
                    case OutputType.FAList:
                        newOutput = new OutputFA(this, id, joinedtsvpaths, xlbooknameprefix);
                        break;
                    case OutputType.GT:
                    case OutputType.Cross:
                    case OutputType.CheckList:
                    case OutputType.RawData:
                    case OutputType.QC3:
                        return null;
                    default:
                        newOutput = new Output(this, id, joinedtsvpaths, xlbooknameprefix, outputtype);
                        break;
                }
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as Output;
            }
        }
        #endregion

        #region FAリスト出力時使用
#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputFA Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputFA newOutput = new OutputFA(this, id, joinedtsvpaths, xlbooknameprefix, papersize, paperorientation);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputFA;
            }
        }
        #endregion

        #region GT出力時使用
#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputGT Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking
                    , SignificanceTestLevel significancetestlevel)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputGT newOutput = new OutputGT(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                      , pagesetuptabletype, papersize, paperorientation, markingtype
                                                      , minsamplescountonmarking, significancetestlevel);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputGT;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputGT Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , MarkingType markingtype, int minsamplescountonmarking
                    , SignificanceTestLevel significancetestlevel)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputGT newOutput = new OutputGT(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                      , markingtype, minsamplescountonmarking, significancetestlevel);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputGT;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputGT Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputGT newOutput = new OutputGT(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                      , pagesetuptabletype, papersize, paperorientation, markingtype
                                                      , minsamplescountonmarking);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputGT;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputGT Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputGT newOutput = new OutputGT(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                      , pagesetuptabletype, papersize, paperorientation
                                                      , significancetestlevel);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputGT;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputGT Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , MarkingType markingtype, int minsamplescountonmarking)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputGT newOutput = new OutputGT(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                      , markingtype, minsamplescountonmarking);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputGT;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputGT Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , SignificanceTestLevel significancetestlevel)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputGT newOutput = new OutputGT(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                      , significancetestlevel);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputGT;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputGT Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputGT newOutput = new OutputGT(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                      , pagesetuptabletype, papersize, paperorientation);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputGT;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputGT Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputGT newOutput = new OutputGT(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputGT;
            }
        }
        #endregion

        #region クロス出力時使用
#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , significancetestlevel, markingtype, minsamplescountonmarking
                                                            , level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet
                                                            , significancetestlevel, markingtype, minsamplescountonmarking
                                                            , level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , markingtype, minsamplescountonmarking
                                                            , level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, markingtype, minsamplescountonmarking
                                                            , level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , significancetestlevel, markingtype, minsamplescountonmarking
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet
                                                            , significancetestlevel, markingtype, minsamplescountonmarking
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , markingtype, minsamplescountonmarking
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, markingtype, minsamplescountonmarking
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , significancetestlevel
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, significancetestlevel
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , int level1percent, int level2percent)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet
                                                            , level1percent, level2percent);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , significancetestlevel, markingtype, minsamplescountonmarking
                                                            , level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet
                                                            , significancetestlevel, markingtype, minsamplescountonmarking
                                                            , level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , markingtype, minsamplescountonmarking
                                                            , level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , MarkingType markingtype, int minsamplescountonmarking
                    , int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, markingtype, minsamplescountonmarking
                                                            , level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , significancetestlevel, markingtype, minsamplescountonmarking);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet
                                                            , significancetestlevel, markingtype, minsamplescountonmarking);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , MarkingType markingtype, int minsamplescountonmarking)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , markingtype, minsamplescountonmarking);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , MarkingType markingtype, int minsamplescountonmarking)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, markingtype, minsamplescountonmarking);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation
                                                            , significancetestlevel);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , SignificanceTestLevel significancetestlevel)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, significancetestlevel);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet, pagesetuptabletype, papersize, paperorientation);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCross Add(decimal id, string joinedtsvpaths, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCross newOutput = new OutputCross(this, id, joinedtsvpaths, xlbooknameprefix, tabletype, tableorientation
                                                            , tablesononesheet);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCross;
            }
        }
        #endregion

        #region チェックリスト出力時使用
#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputCheckList Add(decimal id, string joinedtsvpaths, string xlbooknameprefix, int totalcount)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                OutputCheckList newOutput = new OutputCheckList(this, id, joinedtsvpaths, xlbooknameprefix, totalcount);
                this.Add(key, newOutput);
                return newOutput;
            }
            else
            {
                return this[key] as OutputCheckList;
            }
        }
        #endregion

        #region ローデータ出力時(QC3形式含む)使用
#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        OutputRawData Add(decimal id, string tsvpath, string xlbooknameprefix, bool isQC3 = false)
        {
            string key = id.ToString();
            if (!this.Contains(key))
            {
                try
                {
                    OutputRawData newOutput = new OutputRawData(this, id, tsvpath, xlbooknameprefix, isQC3);
                    this.Add(key, newOutput);
                    return newOutput;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    throw;
                }
            }
            else
            {
                return this[key] as OutputRawData;
            }
        }
        #endregion

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="key">キーとなる文字列</param>
        /// <returns>キーが示すコレクションの要素であるOutputクラスのインスタンスへの参照</returns>
        public IOutput this[string key]
        {
            get
            {
                if (this.Contains(key))
                {
                    return base[key] as Output;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="id">出力物ID</param>
        /// <returns>出力物IDが示すコレクションの要素であるOutputクラスのインスタンスへの参照</returns>
        [ComVisible(false)]
        public IOutput this[decimal id]
        {
            get
            {
                string key = id.ToString();
                return this[key];
            }
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// <note>VBAから呼べるようにdouble</note>
        /// </summary>
        /// <param name="id">出力物ID</param>
        /// <returns>出力物IDが示すコレクションの要素であるOutputクラスのインスタンスへの参照</returns>
        public IOutput this[double id]
        {
            get
            {
                return this[(decimal)id];
            }
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="index">0ベースのインデックス</param>
        /// <returns>インデックスが示す要素</returns>
        public Output this[int index]
        {
            get
            {
                /*
                if (index < 0 || index >= this.Count) return null;
                IOutput[] tmp = new IOutput[this.Count];
                this.Values.CopyTo(tmp, 0);
                return tmp[index];
                */
                foreach (Output output in this.Values)
                {
                    if (output.Index == index) return output;
                }
                return null;
            }

        }

        /// <summary>
        /// 要素数を返す読み取り専用プロパティ
        /// </summary>
        public new int Count
        {
            get
            {
                return (this as Hashtable).Count;
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (Output op in this.Values)
            {
                op.Dispose();
            }
            reportset = null;
        }

        /// <summary>
        /// 自身のインスタンスの親であるReportsetクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IReportset ParentReportset
        {
            get
            {
                return reportset;
            }
        }

        /// <summary>
        /// 自身のインスタンスの親であるRequestクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IRequest ParentRequest
        {
            get
            {
                if (reportset == null) return null;
                return reportset.ParentRequest;
            }
        }
    }
}
