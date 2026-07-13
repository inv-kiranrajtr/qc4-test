#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Output.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/3/23
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/8
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
/*
using Excel = Microsoft.Office.Interop.Excel;
*/
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Common;

using Seasar.Quill;
using Macromill.QCWeb.Tabulation;
using System.Collections.Generic;

namespace Macromill.QCWeb.ReportRequest
{
    /// <summary>
    /// 出力命令側から扱う出力物のコレクションクラス
    /// </summary>
    [ComVisible(false), Guid("97C62F48-5698-4ca4-B0ED-B664E0F02690")]
    public class Outputs : Hashtable, IOutputs
    {
        /// <summary>
        /// 出力命令側から扱う出力物を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("58D67FA8-68C9-4caa-B191-4120EA1F60E8")]
        public class Output : IOutput
        {
            private decimal id = 0;
            private string xlbooknameprefix = null;
            private OutputType outputtype = (OutputType)0;
            private int index = 0;

            private Outputs Collection = null;

            private Tables tables = null;

            internal Output(Outputs outputs, OutputType outputtype)
            {
                Collection = outputs;
                OutputType = outputtype;
                index = outputs.Count;
                tables = new Tables(this);
            }

            //internal void InsertToDB(decimal reportsetid)
            internal void InsertToDB()
            {
                if (xlbooknameprefix == null || (int)outputtype == 0)
                {
                    // throw new QCWebException(string.Format("出力物の構成が不正です。xlbooknameprefix={0} outputtype={1}", xlbooknameprefix, outputtype));    // 出力物の構成が不正
                    string[] param = new string[0];
                    if (xlbooknameprefix == null)
                    {
                        Array.Resize<string>(ref param, param.Length + 1);
                        param[param.GetUpperBound(0)] = GetResource.GetLogMessage(Constants.EXCEL_BOOK_NAME_PREFIX_IS_NULL_MESSAGE_ID);
                    }
                    if ((int)outputtype == 0)
                    {
                        Array.Resize<string>(ref param, param.Length + 1);
                        param[param.GetUpperBound(0)] = GetResource.GetLogMessage(Constants.UNJUST_OUTPUT_TYPE_MESSAGE_ID, "0");
                    }
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.InsufficientOutputInformationWithDetailMessageIndex)
                                                       , GlobalsCommonConstant.LogLevel.FATAL
                                                       , param);
                }

                // TODO:暫定対応→抹殺
                // this.id = index + 1;

                // 集計結果TSVファイルサイズの算出
                string tsvpaths = tables.OutputToTSV();
                Request req = ParentRequest as Request;
                foreach (string tsvpath in tsvpaths.Split(';'))
                {
                    if (string.IsNullOrEmpty(tsvpath)) continue;
                    long fileSize = new System.IO.FileInfo(tsvpath).Length;
                    req.PushFileSize(fileSize, outputtype);
                }
                tsvpaths = tsvpaths.Replace(";;", ";");

                // 出力物共通
                TOutputCommon tOutputCommon = new TOutputCommon();
                tOutputCommon.OrderCount = this.Order;                          // オーダー
                tOutputCommon.TsvFilePath = tsvpaths;                           // TSVファイルパス
                tOutputCommon.ExcelbookNamePrefix = this.ExcelBookNamePrefix;   // Excelブック名プリフィックス
                tOutputCommon.ProcessStartDatetime = null;                      // 処理開始時刻
                tOutputCommon.ProcessForecastEndDatetime = null;                // 処理終了予想時刻
                tOutputCommon.ProcessEndDatetime = null;                        // 処理終了時刻
                tOutputCommon.StatusCode = (int)StatusCode.WaitRequest;         // ステータスコード
                tOutputCommon.Description = null;                               // 説明
                tOutputCommon.OutputType = (int)outputtype;                     // 出力物種類
                tOutputCommon.OutputRequestId = (ParentRequest as Request).ID;  // リクエストID

                // 出力物情報の書き込み
                switch (outputtype)
                {
                    case OutputType.GT:
                        {
                            Outputs.OutputGT gt = this as Outputs.OutputGT;

                            // WB集計表示コード
                            WBSettingCode wbcd = WBSettingCode.WBOff;
                            if (gt.ShowPreWBTotal) wbcd = WBSettingCode.ShowPreWB;
                            if (gt.WBOn) wbcd = wbcd | WBSettingCode.WBOn;
                            tOutputCommon.WbSettingCode = (int)wbcd;
                            // 無回答表示コード
                            ShowCode shownacd = (ShowCode)0;
                            if (gt.ShowNAAtItem) shownacd = ShowCode.Item;
                            if (gt.ShowNAAtAxis) shownacd = shownacd | ShowCode.Axis;
                            tOutputCommon.NoanswerVisibleCode = (int)shownacd;
                            // 非該当表示コード
                            ShowCode showivcd = (ShowCode)0;
                            if (gt.ShowIVAtItem) showivcd = ShowCode.Item;
                            if (gt.ShowIVAtAxis) showivcd = showivcd | ShowCode.Axis;
                            tOutputCommon.UnmatchVisibleCode = (int)showivcd;

                            (ParentRequest as Request).TOutputCommonBhv.Insert(tOutputCommon);
                            this.id = (decimal)tOutputCommon.OutputCommonId;

                            // 出力物GT
                            TOutputSubGt tOutputSubGt = new TOutputSubGt();
                            tOutputSubGt.OutputCommonId = tOutputCommon.OutputCommonId;     // 出力物ID
                            // 2012/09/21 M.Inaba Edit -------->
                            // tOutputSubGt.OutputTableType = (int)gt.OutputType;           // 出力表種類
                            tOutputSubGt.OutputTableType = (int)gt.OutputTableType;         // 出力表種類
                            // <-------- 2012/09/21 M.Inaba Edit
                            tOutputSubGt.OutputTableOrientation = (int)gt.Orientation;      // 出力表向き
                            // 2012/09/21 M.Inaba Edit -------->
                            //tOutputSubGt.PageSettingTableType = (int)gt.OutputTableType;  // ページ設定表種類
                            tOutputSubGt.PageSettingTableType = (int)gt.PageSetupTableType; // ページ設定表種類
                            // <-------- 2012/09/21 M.Inaba Edit
                            tOutputSubGt.PageSettingPaperSize = (int)gt.PaperSize;          // ページ設定用紙サイズ
                            tOutputSubGt.PageSettingPaperOrientation = (int)gt.PaperOrientation;    // ページ設定用紙の向き
                            tOutputSubGt.MarkingLevel = (int)gt.SignificanceTestLevel;      // 項目間検定マーキングレベル
                            tOutputSubGt.MarkingMinParameter = gt.MinSamplesCountOnMarking; // マーキング最小母数
                            tOutputSubGt.MarkingCode = (int)gt.Markingtype;                 // マーキングコード
                            tOutputSubGt.FilteringExpression = gt.FilteringExpression;      // 絞込み条件
                            (ParentRequest as Request).TOutputSubGtBhv.Insert(tOutputSubGt);

                            break;
                        }
                    case OutputType.Cross:
                        {
                            Outputs.OutputCross cross = this as Outputs.OutputCross;

                            // WB集計表示コード
                            WBSettingCode wbcd = WBSettingCode.WBOff;
                            if (cross.ShowPreWBTotal) wbcd = WBSettingCode.ShowPreWB;
                            if (cross.WBOn) wbcd = wbcd | WBSettingCode.WBOn;
                            tOutputCommon.WbSettingCode = (int)wbcd;
                            // 無回答表示コード
                            ShowCode shownacd = (ShowCode)0;
                            if (cross.ShowNAAtItem) shownacd = ShowCode.Item;
                            if (cross.ShowNAAtAxis) shownacd = shownacd | ShowCode.Axis;
                            tOutputCommon.NoanswerVisibleCode = (int)shownacd;
                            // 非該当表示コード
                            ShowCode showivcd = (ShowCode)0;
                            if (cross.ShowIVAtItem) showivcd = ShowCode.Item;
                            if (cross.ShowIVAtAxis) showivcd = showivcd | ShowCode.Axis;
                            tOutputCommon.UnmatchVisibleCode = (int)showivcd;

                            (ParentRequest as Request).TOutputCommonBhv.Insert(tOutputCommon);
                            this.id = (decimal)tOutputCommon.OutputCommonId;

                            // 出力物クロス
                            TOutputSubCross tOutputSubCross = new TOutputSubCross();
                            tOutputSubCross.OutputCommonId = tOutputCommon.OutputCommonId;          // 出力物ID
                            tOutputSubCross.OutputType = (int)cross.TablesOnOnesheet;               // 出力形式
                            /* 2012/11/12 M.Inaba Edit -------> */
                            //tOutputSubCross.OutputTableType = (int)cross.OutputType;                // 出力表種類
                            tOutputSubCross.OutputTableType = (int)cross.OutputTableType;                // 出力表種類
                            /* <--------2012/11/12 M.Inaba Edit */
                            tOutputSubCross.OutputTableOrientation = (int)cross.Orientation;        // 出力表向き
                            tOutputSubCross.PageSettingTableType = (int)cross.PageSetupTableType;   // ページ設定表種類
                            tOutputSubCross.PageSettingPaperSize = (int)cross.PaperSize;            // ページ設定用紙サイズ
                            tOutputSubCross.PageSettingPaperOrientation = (int)cross.PaperOrientation;  // ページ設定用紙の向き
                            tOutputSubCross.MarkingMinParameter = cross.MinSamplesCountOnMarking;   // マーキング最小母数
                            tOutputSubCross.MarkingCode = (int)cross.Markingtype;                   // マーキングコード
                            tOutputSubCross.MarkingLevel = (int)cross.SignificanceTestLevel;        // 項目間検定マーキングレベル
                            tOutputSubCross.Level2pluscolor = cross.Level2HighColorIndex;           // +10％色
                            tOutputSubCross.Level1pluscolor = cross.Level1HighColorIndex;           // +5％色
                            tOutputSubCross.Level1minuscolor = cross.Level1LowColorIndex;           // -5％色
                            tOutputSubCross.Level2minuscolor = cross.Level2LowColorIndex;           // -10％色
                            tOutputSubCross.Level1percent = cross.Level1Percent;                    // 5%カスタム率
                            tOutputSubCross.Level2percent = cross.Level2Percent;                    // 10%カスタム率
                            tOutputSubCross.FilteringExpression = cross.FilteringExpression;        // 絞込み条件
                            (ParentRequest as Request).TOutputSubCrossBhv.Insert(tOutputSubCross);

                            break;
                        }
                    case OutputType.FAList:
                        {
                            Outputs.OutputFA fa = this as Outputs.OutputFA;

                            (ParentRequest as Request).TOutputCommonBhv.Insert(tOutputCommon);
                            this.id = (decimal)tOutputCommon.OutputCommonId;

                            // 出力物FA
                            TOutputSubFa tOutputSubFa = new TOutputSubFa();
                            tOutputSubFa.OutputCommonId = tOutputCommon.OutputCommonId;             // 出力物ID
                            tOutputSubFa.PageSettingPaperSize = (int)fa.PaperSize;                  // ページ設定用紙サイズ
                            tOutputSubFa.PageSettingPaperOrientation = (int)fa.PaperOrientation;    // ページ設定用紙の向き
                            tOutputSubFa.FilteringExpression = fa.FilteringExpression;              // 絞込み条件
                            (ParentRequest as Request).TOutputSubFaBhv.Insert(tOutputSubFa);

                            break;
                        }
                    case OutputType.CheckList:
                        {
                            Outputs.OutputCheckList chkList = this as Outputs.OutputCheckList;

                            (ParentRequest as Request).TOutputCommonBhv.Insert(tOutputCommon);
                            this.id = (decimal)tOutputCommon.OutputCommonId;

                            // 出力物チェックリスト
                            TOutputSubCklist tOutputSubCklist = new TOutputSubCklist();
                            tOutputSubCklist.OutputCommonId = tOutputCommon.OutputCommonId;     // 出力物ID
                            tOutputSubCklist.TotalCount = chkList.TotalCount;                   // 全体数
                            (ParentRequest as Request).TOutputSubCklistBhv.Insert(tOutputSubCklist);

                            break;
                        }
                    case OutputType.Questionnaire:
                    case OutputType.RawData:
                    case OutputType.QC3:
                        {
                            (ParentRequest as Request).TOutputCommonBhv.Insert(tOutputCommon);
                            this.id = (decimal)tOutputCommon.OutputCommonId;
                            break;
                        }
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
            public decimal ID
            {
                get
                {
                    return id;
                }
            }

            /// <summary>
            /// オーダー順を返す読み取り専用プロパティ
            /// </summary>
            public long Order
            {
                get
                {
                    return (long)(index + 1);
                }
            }

            /// <summary>
            /// 出力物のExcelブック名のプリフィックスを取得/設定するプロパティ<br />
            /// 設定は1度のみ可能
            /// </summary>
            public string ExcelBookNamePrefix
            {
                get
                {
                    return xlbooknameprefix;
                }
                set
                {
                    if (xlbooknameprefix == null)
                        xlbooknameprefix = value;
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
                private set
                {
                    switch (value)
                    {
                        case ReportRequest.OutputType.CheckTemplate:
                        case ReportRequest.OutputType.GT:
                        case ReportRequest.OutputType.Cross:
                        case ReportRequest.OutputType.FAList:
                        case ReportRequest.OutputType.CheckList:
                        case ReportRequest.OutputType.Questionnaire:
                        case ReportRequest.OutputType.RawData:
                        case ReportRequest.OutputType.QC3:
                            outputtype = value;
                            break;
                    }
                }
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
                    if (Collection == null)
                        return null;
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
                    if (Collection == null)
                        return null;
                    return Collection.ParentRequest;
                }
            }

            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                tables.Dispose();
                Collection = null;
            }
        }

        /// <summary>
        /// 出力命令側から扱うFAリストの出力物を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("C69F116C-DEFA-4f99-82C7-4B0F0C13326D")]
        public class OutputFA : Output, IOutputFA
        {
            private bool pagesetup = false;
            // private Excel.XlPaperSize papersize = Excel.XlPaperSize.xlPaperA4;
            // private Excel.XlPageOrientation paperorientation = Excel.XlPageOrientation.xlPortrait;
            private Common.XlPaperSize papersize = Common.XlPaperSize.xlPaperA4;
            private Common.XlPageOrientation paperorientation = Common.XlPageOrientation.xlPortrait;

            internal OutputFA(Outputs outputs, OutputType outputtype = ReportRequest.OutputType.FAList) : base(outputs, outputtype) { }

            /// <summary>
            /// ページ設定時の用紙サイズを表すXlPaperSize列挙型の値を取得/設定するプロパティ
            /// </summary>
            public Common.XlPaperSize PaperSize
            {
                get
                {
                    // return (Common.XlPaperSize)papersize;
                    return papersize;
                }
                set
                {
                    if (Enum.IsDefined(typeof(Common.XlPaperSize), value))
                    {
                        pagesetup = true;
                        // papersize = (Excel.XlPaperSize)value;
                        papersize = value;
                    }
                }
            }

            /// <summary>
            /// ページ設定時の用紙の向きを表すXlPageOrientation列挙型の値を取得/設定するプロパティ
            /// </summary>
            public Common.XlPageOrientation PaperOrientation
            {
                get
                {
                    // return (Common.XlPageOrientation)paperorientation;
                    return paperorientation;
                }
                set
                {
                    switch (value)
                    {
                        case Common.XlPageOrientation.xlPortrait:
                        case Common.XlPageOrientation.xlLandscape:
                            pagesetup = true;
                            // paperorientation = (Excel.XlPageOrientation)value;
                            paperorientation = value;
                            break;
                        default:
                            break;
                    }
                }
            }

            /// <summary>
            /// ページ設定のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool PageSetup
            {
                get
                {
                    return pagesetup;
                }
                protected set
                {
                    pagesetup = value;
                }
            }

            private string filteringExpression = null;

            private string LocalizeFilteringExpression(string filter)
            {
                // リソースを使って、絞込み条件式を文章化する処理
                if (string.IsNullOrWhiteSpace(filter)) return null;
                string lccd = ParentRequest.LocationCode;
                if (string.IsNullOrWhiteSpace(lccd)) lccd = "ja";
                Criteria criteria = new Criteria(filter, null);
                List<ICriteria> criterias = criteria.SubCriterias;
                if (criterias == null || criterias.Count == 0)
                {
                    criterias = new List<ICriteria>();
                    criterias.Add(criteria);
                }
                string[] resultBuf = new string[criterias.Count];
                QueryItemName query = new QueryItemName();
                // {0}の値
                string itemValueFormat = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionItemValueFormatIndex, lccd);
                // {0}以上{1}以下
                string betweenNumberFormat = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionBetweenNumberFormatIndex, lccd);
                // {0}が{1}{2}
                string criterionFormat = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionFormatIndex, lccd);
                for (int i = 0; i < criterias.Count; ++i)
                {
                    Criteria tmpCriteria = criterias[i] as Criteria;
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
                    string qName = tmpCriteria.Question.Name2;
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
                    decimal CriteriaQID = query.QuestionNameToID(ParentRequest.QCWebID, allowQType, value, ScenarioId);
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
                                    int[] criteriaValueList = null;
                                    criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
                                                                                qType, value, out criteriaValueList);
                                    if (criteriaDataType == DataType.NormalData)
                                    {
                                        int s = 0, e = 0;
                                        isSingle = criteriaValueList != null && criteriaValueList.Length == 1;
                                        for (int j = 0; j < (criteriaValueList == null ? 0 : criteriaValueList.Length); ++j)
                                        {
                                            if (j == 0 || criteriaValueList[j] != criteriaValueList[j - 1] + 1)
                                            {
                                                s = criteriaValueList[j];
                                            }
                                            if (j == criteriaValueList.Length - 1 || criteriaValueList[j + 1] != criteriaValueList[j] + 1)
                                            {
                                                e = criteriaValueList[j];
                                                if (s == e)
                                                {
                                                    values.Add(s.ToString());
                                                }
                                                else
                                                {
                                                    values.Add(s.ToString() + "-" + e.ToString());
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            case QuestionType.N:
                                {
                                    NData.ValueRange[] criteriaValueList = null;
                                    criteriaDataType = GlobalTabulation.CriteriaValueDescriptionToValueList<NData.ValueRange>(
                                                                                qType, value, out criteriaValueList);
                                    if (criteriaDataType == DataType.NormalData)
                                    {
                                        isSingle = criteriaValueList != null && criteriaValueList.Length == 1;
                                        for (int j = 0; j < (criteriaValueList == null ? 0 : criteriaValueList.Length); ++j)
                                        {
                                            double min = criteriaValueList[j].MinValue;
                                            double max = criteriaValueList[j].MaxValue;
                                            if (min == max)
                                            {
                                                values.Add(min.ToString());
                                            }
                                            else
                                            {
                                                values.Add(string.Format(betweenNumberFormat, min.ToString(), max.ToString()));
                                                isRange = true;
                                            }
                                        }
                                    }
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
                                        values.Add("\"" + value + "\"");
                                    }
                                    break;
                                }
                        }
                        if (criteriaDataType != DataType.NormalData)
                        {
                            if ((criteriaDataType & DataType.NAData) == DataType.NAData)
                            {
                                // 無回答
                                values.Add(GetResource.GetReportKeyword(
                                        Constants.ReportMessageIndex.ReportNADescriptionKeywordIndex, lccd));
                            }
                            if ((criteriaDataType & DataType.IVData) == DataType.IVData)
                            {
                                // 非該当
                                values.Add(GetResource.GetReportKeyword(
                                        Constants.ReportMessageIndex.ReportIVDescriptionKeywordIndex, lccd));
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
                                            case QuestionType.N:
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionEqualKeywordIndex, lccd);
                                                break;
                                            case QuestionType.MA:
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionIncludeKeywordIndex, lccd);
                                                break;
                                            case QuestionType.FA:
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionMatchKeywordIndex, lccd);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionEqualKeywordIndex, lccd);
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
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionEqualAnyoneKeywordIndex, lccd);
                                                break;
                                            case QuestionType.MA:
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionIncludeAnyoneKeywordIndex, lccd);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionEqualAnyoneKeywordIndex, lccd);
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
                                            case QuestionType.N:
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionNotEqualKeywordIndex, lccd);
                                                break;
                                            case QuestionType.MA:
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionNotIncludeKeywordIndex, lccd);
                                                break;
                                            case QuestionType.FA:
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionUnmatchKeywordIndex, lccd);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionNotEqualKeywordIndex, lccd);
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
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionNotEqualAnyoneKeywordIndex, lccd);
                                                break;
                                            case QuestionType.MA:
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionNotIncludeAnyoneKeywordIndex, lccd);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionNotEqualAnyoneKeywordIndex, lccd);
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
                                        ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionGreaterKeywordIndex, lccd);
                                        break;
                                    case "<":
                                        ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionLessKeywordIndex, lccd);
                                        break;
                                    case ">=":
                                        ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionGreaterEqualKeywordIndex, lccd);
                                        break;
                                    case "<=":
                                        ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionLessEqualKeywordIndex, lccd);
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        Question.Questions tmpQs = new Question.Questions(0, CriteriaQID);
                        Question.Questions.Question criteriaQ = tmpQs[CriteriaQID] as Question.Questions.Question;
                        string qName2 = criteriaQ.Name2;
                        value = string.Format(itemValueFormat, qName2);
                        switch (ope)
                        {
                            case "=":
                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionMatchKeywordIndex, lccd);
                                break;
                            case "<>":
                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionUnmatchKeywordIndex, lccd);
                                break;
                            default:
                                switch (qType)
                                {
                                    case QuestionType.SA:
                                    case QuestionType.N:
                                        switch (ope)
                                        {
                                            case ">":
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionGreaterKeywordIndex, lccd);
                                                break;
                                            case "<":
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionLessKeywordIndex, lccd);
                                                break;
                                            case ">=":
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionGreaterEqualKeywordIndex, lccd);
                                                break;
                                            case "<=":
                                                ope = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportCriterionLessEqualKeywordIndex, lccd);
                                                break;
                                        }
                                        break;
                                    case QuestionType.MA:
                                    case QuestionType.FA:
                                        // 不正な指定
                                        return null;
                                }
                                break;
                        }
                    }
                    if (i > 0)
                    {
                        resultBuf[i - 1] += GetResource.GetReportKeyword(
                                                tmpCriteria.Operator == Operator.opAnd
                                                    ? Constants.ReportMessageIndex.ReportCriterionOperatorAndKeywordIndex
                                                    : Constants.ReportMessageIndex.ReportCriterionOperatorOrKeywordIndex
                                              , lccd);
                    }
                    resultBuf[i] = string.Format(criterionFormat, qName, value, ope);
                }
                return string.Join("\n", resultBuf);
            }

            /// <summary>
            /// 絞込み条件式を取得/設定するプロパティ
            /// </summary>
            public string FilteringExpression
            {
                get
                {
                    return LocalizeFilteringExpression(filteringExpression);
                }
                set
                {
                    filteringExpression = value;
                }
            }

            /// <summary>
            /// シナリオIDを取得/設定するプロパティ
            /// </summary>
            public decimal ScenarioId { get; set; }
        }

        /// <summary>
        /// 出力命令側から扱うGT表の出力物を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("F7C9F00E-7B5B-4f33-A633-B303DAEB57DD")]
        public class OutputGT : OutputFA, IOutputGT
        {
            private TableType tabletype = TableType.NPer | TableType.N | TableType.Per;
            private TableOrientation tableorientation = TableOrientation.Landscape;
            private TableType pagesetuptabletype = (TableType)0;
            private int minsamplescountonmarking = 0;
            private MarkingType markingtype = (MarkingType)0;
            private SignificanceTestLevel significancetestlevel = (SignificanceTestLevel)0;
            private bool preWbBase = true;

            internal OutputGT(Outputs outputs, OutputType outputtype = ReportRequest.OutputType.GT) : base(outputs, outputtype) { }

            internal OutputGT(Outputs outputs, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, XlPaperSize papersize, XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking
                    //, int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex
                    //, int level1percent, int level2percent
                    , OutputType outputtype = ReportRequest.OutputType.GT)
    : base(outputs, outputtype)
            {
                this.ExcelBookNamePrefix = xlbooknameprefix;
                this.OutputTableType = tabletype;
                //this.TablesOnOnesheet = tablesononesheet;
                this.PageSetupTableType = pagesetuptabletype;
                this.PaperSize = papersize;
                this.PaperOrientation = paperorientation;
                this.SignificanceTestLevel = SignificanceTestLevel;
                //this.Markingtype = (MarkingType)markingtype;
                this.MinSamplesCountOnMarking = minsamplescountonmarking;

                //this.level2highcolorindex = level2highcolorindex;
                //this.level1highcolorindex = level1highcolorindex;
                //this.level1lowcolorindex = level1lowcolorindex;
                //this.level2lowcolorindex = level2lowcolorindex;
                //this.level1percent = level1percent;
                //this.level2percent = level2percent;

                //Default values as per web logic
                this.PageSetupNPerTable = true;
                this.PageSetupNTable = true;
                this.PageSetupPerTable = true;
                this.PageSetupSignificanceTestTable = false;
                this.PageSetupTableType = TableType.NPer | TableType.N | TableType.Per;
                this.ShowNAAtItem = true;
            }

            /// <summary>
            /// 作成する表の種類を表すTableType列挙型の値を直接取得/設定するプロパティ
            /// </summary>
            public TableType OutputTableType
            {
                get
                {
                    return tabletype;
                }
                set
                {
                    value &= TableType.NPer | TableType.N | TableType.Per;
                    if ((int)value == 0)
                        return;
                    tabletype = value;
                    pagesetuptabletype &= value;
                }
            }

            /// <summary>
            /// N％表作成のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool OutputNPerTable
            {
                get
                {
                    return (tabletype & TableType.NPer) == TableType.NPer;
                }
                set
                {
                    if (value)
                    {
                        tabletype |= TableType.NPer;
                    }
                    else
                    {
                        if (tabletype != TableType.NPer)
                        {
                            tabletype &= ~TableType.NPer;
                            pagesetuptabletype &= tabletype;
                        }
                    }
                }
            }

            /// <summary>
            /// N表作成のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool OutputNTable
            {
                get
                {
                    return (tabletype & TableType.N) == TableType.N;
                }
                set
                {
                    if (value)
                    {
                        tabletype |= TableType.N;
                    }
                    else
                    {
                        if (tabletype != TableType.N)
                        {
                            tabletype &= ~TableType.N;
                            pagesetuptabletype &= tabletype;
                        }
                    }
                }
            }

            /// <summary>
            /// ％表作成のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool OutputPerTable
            {
                get
                {
                    return (tabletype & TableType.Per) == TableType.Per;
                }
                set
                {
                    if (value)
                    {
                        tabletype |= TableType.Per;
                    }
                    else
                    {
                        if (tabletype != TableType.Per)
                        {
                            tabletype &= ~TableType.Per;
                            pagesetuptabletype &= tabletype;
                        }
                    }
                }
            }

            /// <summary>
            /// 作成する集計表の向きを表すTableOrientation列挙型の値を取得/設定するプロパティ<br />
            /// 設定できる値は、TableOrientation.Landscape、TableOrientation.Portraitのいずれか
            /// </summary>
            public TableOrientation Orientation
            {
                get
                {
                    return tableorientation;
                }
                set
                {
                    switch (value)
                    {
                        case TableOrientation.Landscape:
                        case TableOrientation.Portrait:
                            tableorientation = value;
                            break;
                    }
                }
            }

            /// <summary>
            /// ページ設定する表の種類を表すTableType列挙型の値を直接取得/設定するプロパティ
            /// </summary>
            public TableType PageSetupTableType
            {
                get
                {
                    return pagesetuptabletype;
                }
                set
                {
                    pagesetuptabletype = value & tabletype;
                }
            }

            /// <summary>
            /// N％表ページ設定のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool PageSetupNPerTable
            {
                get
                {
                    return (pagesetuptabletype & TableType.NPer) == TableType.NPer;
                }
                set
                {
                    if (value)
                    {
                        pagesetuptabletype |= TableType.NPer & tabletype;
                    }
                    else
                    {
                        pagesetuptabletype &= ~TableType.NPer;
                    }
                }
            }

            /// <summary>
            /// N表ページ設定のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool PageSetupNTable
            {
                get
                {
                    return (pagesetuptabletype & TableType.N) == TableType.N;
                }
                set
                {
                    if (value)
                    {
                        pagesetuptabletype |= TableType.N & tabletype;
                    }
                    else
                    {
                        pagesetuptabletype &= ~TableType.N;
                    }
                }
            }

            /// <summary>
            /// ％表ページ設定のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool PageSetupPerTable
            {
                get
                {
                    return (pagesetuptabletype & TableType.Per) == TableType.Per;
                }
                set
                {
                    if (value)
                    {
                        pagesetuptabletype |= TableType.Per & tabletype;
                    }
                    else
                    {
                        pagesetuptabletype &= ~TableType.Per;
                    }
                }
            }

            /// <summary>
            /// 項目間検定表ページ設定のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool PageSetupSignificanceTestTable
            {
                get
                {
                    return (pagesetuptabletype & TableType.SignificanceTest) == TableType.SignificanceTest;
                }
                set
                {
                    if (value)
                    {
                        if (SignificanceTest)
                        {
                            pagesetuptabletype |= TableType.SignificanceTest;
                        }
                    }
                    else
                    {
                        pagesetuptabletype &= ~TableType.SignificanceTest;
                    }
                }
            }

            /// <summary>
            /// マーキングの種別を取得するプロパティ
            /// </summary>
            public MarkingType Markingtype
            {
                get { return markingtype; }
            }

            private void setMarkingType(MarkingType type, bool value)
            {
                if (value)
                {
                    markingtype |= type;
                }
                else
                {
                    markingtype &= ~type;
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
            /// ランキングのマーキングを行うかどうかを取得/設定するプロパティ
            /// </summary>
            public bool MarkingRanking
            {
                get
                {
                    return (markingtype & MarkingType.Ranking) == MarkingType.Ranking;
                }
                set
                {
                    setMarkingType(MarkingType.Ranking, value);
                }
            }

            /// <summary>
            /// 昇降分析のマーキングを行うかどうかを取得/設定するプロパティ
            /// </summary>
            public bool MarkingAscending
            {
                get
                {
                    return (markingtype & MarkingType.Ascending) == MarkingType.Ascending;
                }
                set
                {
                    setMarkingType(MarkingType.Ascending, value);
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
            /// 1％有意差検定のマーキングを行うかどうかを取得/設定するプロパティ
            /// </summary>
            public bool MarkingSignificanceOne
            {
                get
                {
                    return (markingtype & MarkingType.SignificanceOne) == MarkingType.SignificanceOne;
                }
                set
                {
                    // if (!value || ((markingtype | MarkingType.SignificanceOne) & MarkingType.Significance) != MarkingType.Significance)
                    {
                        setMarkingType(MarkingType.SignificanceOne, value);
                    }
                }
            }

            /// <summary>
            /// 5％有意差検定のマーキングを行うかどうかを取得/設定するプロパティ
            /// </summary>
            public bool MarkingSignificanceFive
            {
                get
                {
                    return (markingtype & MarkingType.SignificanceFive) == MarkingType.SignificanceFive;
                }
                set
                {
                    // if (!value || ((markingtype | MarkingType.SignificanceFive) & MarkingType.Significance) != MarkingType.Significance)
                    {
                        setMarkingType(MarkingType.SignificanceFive, value);
                    }
                }
            }

            /// <summary>
            /// 10％有意差検定のマーキングを行うかどうかを取得/設定するプロパティ
            /// </summary>
            public bool MarkingSignificanceTen
            {
                get
                {
                    return (markingtype & MarkingType.SignificanceTen) == MarkingType.SignificanceTen;
                }
                set
                {
                    // if (!value || ((markingtype | MarkingType.SignificanceTen) & MarkingType.Significance) != MarkingType.Significance)
                    {
                        setMarkingType(MarkingType.SignificanceTen, value);
                    }
                }
            }

            /// <summary>
            /// マーキングを行う条件とする母数の最小値を取得/設定するプロパティ
            /// </summary>
            public int MinSamplesCountOnMarking
            {
                get
                {
                    return minsamplescountonmarking;
                }
                set
                {
                    if (value >= 0)
                        minsamplescountonmarking = value;
                }
            }

            /// <summary>
            /// 項目間検定の有意水準を表すSignificanceTestLevel列挙型の値を直接取得/設定するプロパティ
            /// </summary>
            public SignificanceTestLevel SignificanceTestLevel
            {
                get
                {
                    return significancetestlevel;
                }
                set
                {
                    value &= ReportRequest.SignificanceTestLevel.One | ReportRequest.SignificanceTestLevel.Five | ReportRequest.SignificanceTestLevel.Ten;
                    if (value != (ReportRequest.SignificanceTestLevel.One | ReportRequest.SignificanceTestLevel.Five | ReportRequest.SignificanceTestLevel.Ten))
                    {
                        significancetestlevel = value;
                    }
                }
            }

            private void setSignificanceTestLevel(SignificanceTestLevel level, bool value)
            {
                if (value)
                {
                    const SignificanceTestLevel SIGNIFICANCE_TEST_LEVEL_ALL
                            = ReportRequest.SignificanceTestLevel.One | ReportRequest.SignificanceTestLevel.Five | ReportRequest.SignificanceTestLevel.Ten;
                    if (((significancetestlevel | level) & SIGNIFICANCE_TEST_LEVEL_ALL) != SIGNIFICANCE_TEST_LEVEL_ALL)
                    {
                        significancetestlevel |= level;
                    }
                }
                else
                {
                    significancetestlevel &= ~level;
                }
            }

            /// <summary>
            /// 項目間検定のオン/オフを返す読み取り専用プロパティ
            /// </summary>
            public bool SignificanceTest
            {
                get
                {
                    return (int)significancetestlevel != 0;
                }
            }

            /// <summary>
            /// 有意水準1％での項目間検定のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool SignificanceTestOne
            {
                get
                {
                    return (significancetestlevel & SignificanceTestLevel.One) == SignificanceTestLevel.One;
                }
                set
                {
                    setSignificanceTestLevel(ReportRequest.SignificanceTestLevel.One, value);
                }
            }

            /// <summary>
            /// 有意水準5％での項目間検定のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool SignificanceTestFive
            {
                get
                {
                    return (significancetestlevel & SignificanceTestLevel.Five) == SignificanceTestLevel.Five;
                }
                set
                {
                    setSignificanceTestLevel(ReportRequest.SignificanceTestLevel.Five, value);
                }
            }

            /// <summary>
            /// 有意水準10％での項目間検定のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool SignificanceTestTen
            {
                get
                {
                    return (significancetestlevel & SignificanceTestLevel.Ten) == SignificanceTestLevel.Ten;
                }
                set
                {
                    setSignificanceTestLevel(ReportRequest.SignificanceTestLevel.Ten, value);
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
                set
                {
                    setMarkingType(MarkingType.ColoringLevel1High, value);
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
                set
                {
                    setMarkingType(MarkingType.ColoringLevel1Low, value);
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
                set
                {
                    setMarkingType(MarkingType.ColoringLevel2High, value);
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
                set
                {
                    setMarkingType(MarkingType.ColoringLevel2Low, value);
                }
            }

            private ShowCode shownacd = (ShowCode)0;  // 無回答表示コード
            /// <summary>
            /// 無回答表示コードを表すShowCode列挙型の値を直接設定する書き込み専用プロパティ
            /// </summary>
            public ShowCode ShowNACode
            {
                set
                {
                    shownacd = value & (ShowCode.Item | ShowCode.Axis);
                }
            }

            /// <summary>
            /// 集計対象の無回答を表示するかどうかを取得/設定するプロパティ
            /// </summary>
            public bool ShowNAAtItem
            {
                get
                {
                    return (shownacd & ShowCode.Item) == ShowCode.Item;
                }
                set
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
            /// 集計軸の無回答を表示するかどうかを取得/設定するプロパティ
            /// </summary>
            public bool ShowNAAtAxis
            {
                get
                {
                    return (shownacd & ShowCode.Axis) == ShowCode.Axis;
                }
                set
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
            /// <summary>
            /// 非該当表示コードを表すShowCode列挙型の値を直接設定する書き込み専用プロパティ
            /// </summary>
            public ShowCode ShowIVCode
            {
                set
                {
                    showivcd = value & (ShowCode.Item | ShowCode.Axis);
                }
            }

            /// <summary>
            /// 集計対象の非該当を表示するかどうかを取得/設定するプロパティ
            /// </summary>
            public bool ShowIVAtItem
            {
                get
                {
                    return (showivcd & ShowCode.Item) == ShowCode.Item;
                }
                set
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
            /// 集計軸の非該当を表示するかどうかを取得/設定するプロパティ
            /// </summary>
            public bool ShowIVAtAxis
            {
                get
                {
                    return (showivcd & ShowCode.Axis) == ShowCode.Axis;
                }
                set
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
            /// <summary>
            /// WB前全体を表示するかどうかを取得/設定するプロパティ
            /// </summary>
            public bool ShowPreWBTotal
            {
                get
                {
                    return wbcd == (WBSettingCode.WBOn | WBSettingCode.ShowPreWB);
                }
                set
                {
                    if (value)
                    {
                        wbcd |= WBSettingCode.ShowPreWB;
                    }
                    else
                    {
                        wbcd &= ~WBSettingCode.ShowPreWB;
                    }
                }
            }

            /// <summary>
            /// WB集計のオン/オフを取得/設定するプロパティ
            /// </summary>
            public bool WBOn
            {
                get
                {
                    return (wbcd & WBSettingCode.WBOn) == WBSettingCode.WBOn;
                }
                set
                {
                    if (value)
                    {
                        wbcd |= WBSettingCode.WBOn;
                    }
                    else
                    {
                        wbcd &= ~WBSettingCode.WBOn;
                    }
                }
            }

            public bool PreWbBase { get => preWbBase; set => preWbBase = value; }

        }

        /// <summary>
        /// 出力命令側から扱うクロス集計表の出力物を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("0A650858-A67A-46c1-B0EA-D60629F4EB8B")]
        public class OutputCross : OutputGT, IOutputCross
        {
            private TablesOnOneSheet tablesononesheet = TablesOnOneSheet.Multi;
            private int level2highcolorindex = 6;
            private int level1highcolorindex = 36;
            private int level1lowcolorindex = 34;
            private int level2lowcolorindex = 37;
            private int level1percent = 5;
            private int level2percent = 10;
            /// <summary>環境設定情報</summary>
            protected TDefaultEnvBhv tDefaultEnvBhv = null;
            /// <summary>アプリ環境設定</summary>
            protected ApplicationConfig config = null;

            internal OutputCross(Outputs outputs, OutputType outputtype = ReportRequest.OutputType.Cross)
                : base(outputs, outputtype)
            {
                QuillInjector.GetInstance().Inject(this);

                // 規定値の読み込み
                TDefaultEnv entity =
                    tDefaultEnvBhv.SelectByPKValueWithDeletedCheck(outputs.ParentRequest.QCWebID);
                level2highcolorindex = (int)entity.RateDiffColorPlus10;
                level1highcolorindex = (int)entity.RateDiffColorPlus5;
                level1lowcolorindex = (int)entity.RateDiffColorMinus10;
                level2lowcolorindex = (int)entity.RateDiffColorMinus5;

                level1percent = int.Parse(config.GetValue(GlobalsCommonConstant.APP_CONFIG_SCENARIO_LEVEL1_PERCENT));
                level2percent = int.Parse(config.GetValue(GlobalsCommonConstant.APP_CONFIG_SCENARIO_LEVEL2_PERCENT));
            }

            internal OutputCross(Outputs outputs, string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, XlPaperSize papersize, XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking,
    int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex,
    int level1percent, int level2percent, OutputType outputtype = ReportRequest.OutputType.Cross)
    : base(outputs, outputtype)
            {
                this.ExcelBookNamePrefix = xlbooknameprefix;
                this.OutputTableType = tabletype;
                this.TablesOnOnesheet = tablesononesheet;
                this.PageSetupTableType = pagesetuptabletype;
                this.PaperSize = papersize;
                this.PaperOrientation = paperorientation;
                this.SignificanceTestLevel = SignificanceTestLevel;
                //  this.Markingtype = (MarkingType)markingtype;
                this.MinSamplesCountOnMarking = minsamplescountonmarking;
                this.level2highcolorindex = level2highcolorindex;
                this.level1highcolorindex = level1highcolorindex;
                this.level1lowcolorindex = level1lowcolorindex;
                this.level2lowcolorindex = level2lowcolorindex;
                this.level1percent = level1percent;
                this.level2percent = level2percent;

            }

            /// <summary>
            /// 1つのワークシートに出力する集計表の数を表すTablesOnOneSheet列挙型の値を取得/設定するプロパティ<br />
            /// 設定できる値は、TablesOnOneSheet.Multi、TablesOnOneSheet.Singleのいずれか
            /// </summary>
            public TablesOnOneSheet TablesOnOnesheet
            {
                get
                {
                    return tablesononesheet;
                }
                set
                {
                    switch (value)
                    {
                        case TablesOnOneSheet.Multi:
                        case TablesOnOneSheet.Single:
                            tablesononesheet = value;
                            break;
                    }
                }
            }

            /// <summary>
            /// 水準2高(+10％)の色付けで使用する色インデックスを表す数値を取得/設定するプロパティ
            /// </summary>
            public int Level2HighColorIndex
            {
                get
                {
                    return level2highcolorindex;
                }
                set
                {
                    if (value >= 0 && value <= 56)
                    {
                        level2highcolorindex = value;
                    }
                }
            }

            /// <summary>
            /// 水準1高(+5％)の色付けで使用する色インデックスを表す数値を取得/設定するプロパティ
            /// </summary>
            public int Level1HighColorIndex
            {
                get
                {
                    return level1highcolorindex;
                }
                set
                {
                    if (value >= 0 && value <= 56)
                    {
                        level1highcolorindex = value;
                    }
                }
            }

            /// <summary>
            /// 水準1低(-5％)の色付けで使用する色インデックスを表す数値を取得/設定するプロパティ
            /// </summary>
            public int Level1LowColorIndex
            {
                get
                {
                    return level1lowcolorindex;
                }
                set
                {
                    if (value >= 0 && value <= 56)
                    {
                        level1lowcolorindex = value;
                    }
                }
            }

            /// <summary>
            /// 水準2低(-10％)の色付けで使用する色インデックスを表す数値を取得/設定するプロパティ
            /// </summary>
            public int Level2LowColorIndex
            {
                get
                {
                    return level2lowcolorindex;
                }
                set
                {
                    if (value >= 0 && value <= 56)
                    {
                        level2lowcolorindex = value;
                    }
                }
            }

            // 上限下限値はスタティック
            private const int MAX_PERCENT = 30;
            private const int MIN_PERCENT = 1;

            /// <summary>
            /// 水準1のパーセンテージを取得/設定するプロパティ<br />
            /// 設定できる値は1～30の整数で水準2のパーセンテージ以下の値
            /// </summary>
            public int Level1Percent
            {
                get
                {
                    return level1percent;
                }
                set
                {
                    if (value >= MIN_PERCENT && value <= MAX_PERCENT)
                    {
                        level1percent = value;
                        if (level2percent < value)
                            level2percent = value;
                    }
                }
            }

            /// <summary>
            /// 水準2のパーセンテージを取得/設定するプロパティ<br />
            /// 設定できる値は1～30の整数で水準1のパーセンテージ以上の値
            /// </summary>
            public int Level2Percent
            {
                get
                {
                    return level2percent;
                }
                set
                {
                    if (value >= MIN_PERCENT && value <= MAX_PERCENT)
                    {
                        level2percent = value;
                        if (level1percent > value)
                            level1percent = value;
                    }
                }
            }

        }

        /// <summary>
        /// 出力命令側から扱うチェックリストの出力物を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("14CC5463-2320-42c4-9AAD-191FCF89A6A9")]
        public class OutputCheckList : Output, IOutputCheckList
        {
            private int totalcount = 0;

            internal OutputCheckList(Outputs outputs, OutputType outputtype = ReportRequest.OutputType.CheckList) : base(outputs, outputtype) { }

            internal OutputCheckList(Outputs outputs, string xlbooknameprefix, int totalCount
                   , OutputType outputtype = ReportRequest.OutputType.CheckList)
   : base(outputs, outputtype)
            {
                this.ExcelBookNamePrefix = xlbooknameprefix;
                this.TotalCount = totalCount;
            }

            /// <summary>
            /// 全体数(サンプル数)を取得/設定するプロパティ<br />
            /// 設定は1度のみ可能
            /// </summary>
            public int TotalCount
            {
                get
                {
                    return totalcount;
                }
                set
                {
                    if (totalcount == 0 && value > 0)
                        totalcount = value;
                }
            }
        }

        private Reportsets.Reportset reportset = null;

        internal Outputs(Reportsets.Reportset reportset)
        {
            this.reportset = reportset;
        }

        /// <summary>
        /// Outputクラスのインスタンスを生成して、それへの参照を返す
        /// </summary>
        /// <param name="outputtype">
        /// 出力物の種類を表す、以下に示すOutputType列挙型の値
        /// <list type="bullet">
        /// <item>
        /// <description>OutputType.GT</description>
        /// </item>
        /// <item>
        /// <description>OutputType.Cross</description>
        /// </item>
        /// <item>
        /// <description>OutputType.FAList</description>
        /// </item>
        /// <item>
        /// <description>OutputType.CheckList</description>
        /// </item>
        /// <item>
        /// <description>OutputType.Questionnaire</description>
        /// </item>
        /// <item>
        /// <description>OutputType.RawData</description>
        /// </item>
        /// <item>
        /// <description>OutputType.QC3</description>
        /// </item>
        /// </list>
        /// </param>
        /// <returns></returns>
        public Output Add(OutputType outputtype)
        {
            string key = this.Count.ToString();
            Outputs.Output newOutput = null;
            switch (outputtype)
            {
                case OutputType.FAList:
                    newOutput = new OutputFA(this);
                    break;
                case OutputType.GT:
                    newOutput = new OutputGT(this);
                    break;
                case OutputType.Cross:
                    newOutput = new OutputCross(this);
                    break;
                case OutputType.CheckList:
                    newOutput = new OutputCheckList(this);
                    break;
                default:
                    newOutput = new Output(this, outputtype);
                    break;
            }
            this.Add(key, newOutput);
            return newOutput;
        }


        public Output Add(string xlbooknameprefix
                    , TableType tabletype, TableOrientation tableorientation
                    , TablesOnOneSheet tablesononesheet
                    , TableType pagesetuptabletype, XlPaperSize papersize, XlPageOrientation paperorientation
                    , SignificanceTestLevel significancetestlevel
                    , MarkingType markingtype, int minsamplescountonmarking, int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex,
            int level1percent, int level2percent)
        {
            string key = this.Count.ToString();
            Outputs.Output newOutput = null;

            newOutput = new OutputCross(this, xlbooknameprefix
                    , tabletype, tableorientation
                    , tablesononesheet
                    , pagesetuptabletype, papersize, paperorientation
                    , significancetestlevel
                    , markingtype, minsamplescountonmarking,
 level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex,
 level1percent, level2percent);
            this.Add(key, newOutput);
            return newOutput;
        }

        public Output AddGT(string xlbooknameprefix
            , TableType tabletype, TableOrientation tableorientation
            , TablesOnOneSheet tablesononesheet
            , TableType pagesetuptabletype, XlPaperSize papersize, XlPageOrientation paperorientation
            , SignificanceTestLevel significancetestlevel
            , MarkingType markingtype, int minsamplescountonmarking
            //, int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex
            //, int level1percent, int level2percent
            )
        {
            string key = this.Count.ToString();
            Outputs.Output newOutput = null;

            newOutput = new OutputGT(this, xlbooknameprefix
                    , tabletype, tableorientation
                    , tablesononesheet
                    , pagesetuptabletype, papersize, paperorientation
                    , significancetestlevel
                    , markingtype, minsamplescountonmarking
 //                   , level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex,
 //level1percent, level2percent
 );
            this.Add(key, newOutput);
            return newOutput;
        }

        public Output AddCheckList(string xlbooknameprefix, int totalCount, OutputType outputtype = ReportRequest.OutputType.CheckList)
        {
            string key = this.Count.ToString();
            Outputs.Output newOutput = null;
            newOutput = new OutputCheckList(this, xlbooknameprefix, totalCount, outputtype);
            this.Add(key, newOutput);
            return newOutput;
        }

        internal void InsertToDB()
        {
            //decimal reportsetid = reportset.ID;
            foreach (DictionaryEntry de in this)
            {
                Output output = de.Value as Output;
                //output.InsertToDB(reportsetid);
                output.InsertToDB();
            }
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="key">
        /// キーとなる文字列<br />
        /// この値は要素の順序を表す0ベースのインデックス番号を文字列化したもの
        /// </param>
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
        /// <param name="id">0ベースのインデックス番号</param>
        /// <returns>インデックス番号が示すコレクションの要素であるOutputクラスのインスタンスへの参照</returns>
        public IOutput this[decimal id]
        {
            get
            {
                string key = id.ToString();
                return this[key];
            }
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
                if (reportset == null)
                    return null;
                return reportset.ParentRequest;
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (DictionaryEntry de in this)
            {
                Output op = de.Value as Output;
                op.Dispose();
            }
            reportset = null;
        }
    }
}
