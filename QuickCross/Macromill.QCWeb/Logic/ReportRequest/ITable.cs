#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：ITable.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/3/19
 * 作　成　者：井川はるき
 * 更　新　日：2012/8/2
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Common;
/*
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
*/

namespace Macromill.QCWeb.ReportRequest
{
    #region 構造体
    #region ResearchInformation構造体
    /// <summary>
    /// 調査概要の調査情報を管理する構造体
    /// </summary>
    [ComVisible(false)]
    public struct ResearchInformation
    {
        /// <summary>
        /// 調査IDを取得/設定するプロパティ
        /// </summary>
        [ComVisible(false)]
        public decimal ID;
        /// <summary>
        /// 調査名を取得/設定するプロパティ
        /// </summary>
        public string Name;
        /// <summary>
        /// 調査方法を取得/設定するプロパティ
        /// </summary>
        public string Method;
        /// <summary>
        /// 商品種別を取得/設定するプロパティ
        /// </summary>
        public string Service;
        /// <summary>
        /// 実施期間を取得/設定するプロパティ
        /// </summary>
        public string Period;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">調査ID</param>
        public ResearchInformation(decimal id)
        {
            ID = id;
            Name = null;
            Method = null;
            Service = null;
            Period = null;
        }
        /// <summary>
        /// 調査IDを返す読み取り専用プロパティ
        /// <note>VBAから呼べるようにdouble</note>
        /// </summary>
        public double ID2
        {
            get
            {
                return (double)ID;
            }
        }
    }
    #endregion

    #region CellInformation構造体
    /// <summary>
    /// 調査概要の割付セル情報を管理する構造体   
    /// </summary>
    [ComVisible(false)]
    public struct CellInformation
    {
        /// <summary>
        /// セルNo.を取得/設定するプロパティ
        /// </summary>
        public string Number;
        /// <summary>
        /// セル名称を取得/設定するプロパティ
        /// </summary>
        public string Description;
        /// <summary>
        /// 希望サンプル数を取得/設定するプロパティ
        /// </summary>
//        public int? RequestDataCount;
        public string RequestDataCount;
        /// <summary>
        /// 有効サンプル数を取得/設定するプロパティ
        /// </summary>
//        public int? ValidDataCount;
        public string ValidDataCount;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="number">No.</param>
        public CellInformation(string number)
        {
            Number = number;
            Description = null;
            RequestDataCount = null;
            ValidDataCount = null;
        }
    }
    #endregion

    #region RuleInformation構造体
    /// <summary>
    /// セレクト条件情報を管理する構造体
    /// </summary>
    [ComVisible(false)]
    public struct RuleInformation
    {
        /// <summary>
        /// 質問番号を取得/設定するプロパティ
        /// </summary>
        public string QuestionNo;
        /// <summary>
        /// 子質問番号を取得/設定するプロパティ
        /// </summary>
        public string ChildQuestionNo;
        /// <summary>
        /// セレクト条件を取得/設定するプロパティ
        /// </summary>
        public string Expression;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="qno">質問番号</param>
        public RuleInformation(string qno)
        {
            QuestionNo = qno;
            ChildQuestionNo = null;
            Expression = null;
        }
    }
    #endregion
    #endregion

    /// <summary>
    /// 集計対象質問の簡易情報を扱うクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("8FA57587-1809-4adf-9C7F-9EF0D9BB3DB0")]
    public class QuestionInformation
    {
        private string narrowingCondition = null;
        private string tableHeading = null;
        private string qNumber = null;
        private bool hasCount = false;
        private bool hasWeight = false;
        private int subTotalCnt = 0;
        private string summaryTableName = null;
        private string name = null;
        private string description = null;
        private string wBValue = null;
        private bool tabulateFullQuantity = false;
        private Tabulation.QuestionType questiontype = (Tabulation.QuestionType)0;

        private void init(string name, string description, Tabulation.QuestionType questiontype, bool allowFA, bool unescape, bool hasCount = false, string summaryTableName = null, int subTotalCnt = 0, bool hasWeight = false, string narrowingCondition = null, string tableHeading = null, string qNumber = null,string wBValue = null,bool tabulateFullQuantity = false)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "name");
            }
            this.name = name;
            if (unescape)
            {
                try
                {
                    description = System.Text.RegularExpressions.Regex.Unescape(description);
                }
                catch (Exception)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "description", "\"" + description + "\"");
                }
            }
            this.description = description;
            Tabulation.QuestionType allowType = Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.N;
            if (allowFA) allowType |= Tabulation.QuestionType.FA | Tabulation.QuestionType.FA_Sub;
            Tabulation.QuestionType qType = questiontype & allowType;
            // FAは許可しても付加FAは許可しない
            if (!Enum.IsDefined(typeof(Tabulation.QuestionType), qType))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "questiontype", questiontype.ToString());
            }
            this.questiontype = questiontype;
            this.hasCount = hasCount;
            this.narrowingCondition = narrowingCondition;
            this.tableHeading = tableHeading;
            this.hasWeight = hasWeight;
            this.summaryTableName = summaryTableName;
            this.subTotalCnt = subTotalCnt;
            this.qNumber = qNumber;
            this.wBValue = wBValue;
            this.tabulateFullQuantity = tabulateFullQuantity;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アイテム名</param>
        /// <param name="description">質問文</param>
        /// <param name="questiontype">質問タイプを表す整数値の文字列表現</param>
        /// <param name="allowFA">FAを許可する場合はtrue (省略可:既定値false)</param>
        /// <param name="unescape">質問文のアンエスケープが必要な場合true (省略可、既定値false)</param>
        public QuestionInformation(string name, string description, string questiontype, bool allowFA = false, bool unescape = false, bool hasCount = false, string summaryTableName = null, int subTotalCnt = 0, bool hasWeight = false, string narrowingCondition = null, string tableHeading = null, string qNumber = null, string wBValue = null,bool tabulateFullQuantity = false)
        {                                   
            int qType = 0;
            if (!int.TryParse(questiontype, out qType))
            {
                string v = questiontype == null ? "null" : "\"" + questiontype + "\"";
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "questiontype", v);
            }
            Tabulation.QuestionType questionType = (Tabulation.QuestionType)qType;
            init(name, description, questionType, allowFA, unescape, hasCount, summaryTableName, subTotalCnt, hasWeight, narrowingCondition, tableHeading, qNumber, wBValue, tabulateFullQuantity);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アイテム名</param>
        /// <param name="description">質問文</param>
        /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="allowFA">FAを許可する場合はtrue (省略可:既定値false)</param>
        /// <param name="unescape">質問文のアンエスケープが必要な場合true (省略可、既定値false)</param>
        public QuestionInformation(string name, string description, Tabulation.QuestionType questiontype, bool allowFA = false, bool unescape = false, bool hasCount = false, string summaryTableName = null, int subTotalCnt = 0, bool hasWeight = false, string narrowingCondition = null, string tableHeading = null, string qNumber = null, string wBValue = null,bool tabulateFullQuantity = false)
        {
            init(name, description, questiontype, allowFA, unescape, hasCount, summaryTableName, subTotalCnt, hasWeight, narrowingCondition, tableHeading, qNumber, wBValue, tabulateFullQuantity);
        }

        /// <summary>
        /// アイテム名を返す読み取り専用プロパティ
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        /// <summary>
        /// 質問文を返す読み取り専用プロパティ
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
        }
        /// <summary>
        /// 質問タイプを表すQuestionType列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public Tabulation.QuestionType QuestionType
        {
            get
            {
                return questiontype;
            }
        }

        public bool HasCount { get => hasCount; set => hasCount = value; }
        public int SubTotalCnt { get => subTotalCnt; set => subTotalCnt = value; }
        public string SummaryTableName { get => summaryTableName; set => summaryTableName = value; }
        public bool HasWeight { get => hasWeight; set => hasWeight = value; }
        public string NarrowingCondition { get => narrowingCondition; set => narrowingCondition = value; }
        public string TableHeading { get => tableHeading; set => tableHeading = value; }
        public string QNumber { get => qNumber; set => qNumber = value; }
        public string WBValue { get => wBValue; set => wBValue = value; }
        public bool TabulateFullQuantity { get => tabulateFullQuantity; set => tabulateFullQuantity = value; }
        
    }

    /// <summary>
    /// 集計軸質問の簡易情報を扱うクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("841907B6-47ED-4FEE-AE94-256ACF0ADCBA")]
    public class AxisInformation
    {
        private int sectorscount = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sectorscount">選択肢数</param>
        public AxisInformation(int sectorscount)
        {
            if (sectorscount > 0) this.sectorscount = sectorscount;
        }
        /// <summary>
        /// 集計軸質問の選択肢数を返す読み取り専用プロパティ
        /// </summary>
        public int SectorsCount
        {
            get
            {
                return sectorscount;
            }
        }
    }

    /// <summary>
    /// 集計軸グループ内の集計軸情報を扱うクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("78D21F2C-2862-4165-8F3C-5A8DD52F5D1C")]
    public class AxesInformation : ArrayList
    {
        /// <summary>
        /// AxisInformationクラスのインスタンスを生成して自身のコレクションに追加する
        /// </summary>
        /// <param name="sectorscount">追加する集計軸質問の選択肢数</param>
        /// <returns>成功時は追加したAxisInformationクラスのインスタンスへの参照<br />失敗時はnull</returns>
        public AxisInformation Add(int sectorscount)
        {
            //if (this.Count < 2 && sectorscount > 0)
            {
                AxisInformation axis = new AxisInformation(sectorscount);
                this.Add(axis);
                return axis;
            }
            return null;
        }
        /// <summary>
        /// コレクション内のindexが示す要素を返す読み取り専用インデクサ
        /// </summary>
        /// <param name="index">インデックス番号</param>
        /// <returns>AxisInformationクラスのインスタンスへの参照</returns>
        public new AxisInformation this[int index]
        {
            get
            {
                if (this.Count == 0 || index < 0 || index >= this.Count) return null;
                return base[index] as AxisInformation;
            }
        }
        private bool showTotal = true;

        public bool ShowTotal { get => showTotal; set => showTotal = value; }
    }

    /// <summary>
    /// 集計軸グループのコレクションを扱うクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("A1483506-D9D8-4679-A54E-130AD8BAAB3B")]
    public class AxesGroupInformation : ArrayList
    {
        /// <summary>
        /// 新たなAxesInformationクラスのインスタンスを生成して自身のコレクションに追加する
        /// </summary>
        /// <returns>追加したAxesInformationクラスのインスタンスへの参照</returns>
        public AxesInformation Add()
        {
            AxesInformation axes = new AxesInformation();
            this.Add(axes);
            return axes;
        }

        /// <summary>
        /// コレクション内のindexが示す要素を返す読み取り専用インデクサ
        /// </summary>
        /// <param name="index">インデックス番号</param>
        /// <returns>AxesInformationクラスのインスタンスへの参照</returns>
        public new AxesInformation this[int index]
        {
            get
            {
                if (this.Count == 0 || index < 0 || index >= this.Count) return null;
                return base[index] as AxesInformation;
            }
        }
    }

    /// <summary>
    /// 分類アイテムの簡易情報を扱うクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("C0C6FF59-44F0-4327-B0D6-F0C8D0702EDF")]
    public class KeyItemInformation
    {
        string name = null;
        string description = null;
        int sectornumber = 0;
        string sectordescription = null;

        private void init(string name, string description, int sectornumber, string sectordescription, bool unescape)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "name");
            }
            this.name = name;
            if (unescape)
            {
                try
                {
                    description = System.Text.RegularExpressions.Regex.Unescape(description + string.Empty);
                }
                catch (Exception)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                            , GlobalsCommonConstant.LogLevel.FATAL
                                            , "description", "\"" + description + "\"");
                }
            }
            this.description = description;
            if (sectornumber <= 0)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "sectornumber", sectornumber.ToString());
            }
            this.sectornumber = sectornumber;
            if (unescape)
                try
                {
                    sectordescription = System.Text.RegularExpressions.Regex.Unescape(sectordescription + string.Empty);
                }
                catch (Exception)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                            , GlobalsCommonConstant.LogLevel.FATAL
                                            , "sectordescription", "\"" + sectordescription + "\"");
                }
            this.sectordescription = sectordescription;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アイテム名</param>
        /// <param name="description">質問文</param>
        /// <param name="sectornumber">選択肢番号の文字列表現</param>
        /// <param name="sectordescription">選択肢文</param>
        /// <param name="unescape">質問文/選択肢文のアンエスケープが必要な場合true (省略可、既定値false)</param>
        public KeyItemInformation(string name, string description, string sectornumber, string sectordescription, bool unescape = false)
        {
            int sectorNumber = 0;
            if (!int.TryParse(sectornumber, out sectorNumber))
            {
                string v = sectornumber == null ? "null" : "\"" + sectornumber + "\"";
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "sectornumber", v);
            }
            init(name, description, sectorNumber, sectordescription, unescape);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アイテム名</param>
        /// <param name="description">質問文</param>
        /// <param name="sectornumber">選択肢番号</param>
        /// <param name="sectordescription">選択肢文</param>
        /// <param name="unescape">質問文/選択肢文のアンエスケープが必要な場合true (省略可、既定値false)</param>
        public KeyItemInformation(string name, string description, int sectornumber, string sectordescription, bool unescape = false)
        {
            init(name, description, sectornumber, sectordescription, unescape);
        }

        /// <summary>
        /// アイテム名を返す読み取り専用プロパティ
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        /// <summary>
        /// 質問文を返す読み取り専用プロパティ
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
        }
        /// <summary>
        /// 選択肢番号を返す読み取り専用プロパティ
        /// </summary>
        public int SectorNumber
        {
            get
            {
                return sectornumber;
            }
        }
        /// <summary>
        /// 選択肢文を返す読み取り専用プロパティ
        /// </summary>
        public string SectorDescription
        {
            get
            {
                return sectordescription;
            }
        }
    }

    /// <summary>
    /// 集計対象質問の選択肢の簡易情報を扱うクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("1DEC9E26-F0F4-419f-AA25-7BAF029B3939")]
    public class SectorInformation
    {
        private string weight = null;
        private bool unsortflag = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="weight">ウエイト値</param>
        /// <param name="unsortflag">並べ替え対象からはずすことを示すフラグ</param>
        public SectorInformation(string weight, bool unsortflag)
        {
            this.weight = weight;
            this.unsortflag = unsortflag;
        }
        /// <summary>
        /// ウエイト値を文字列で返す読み取り専用プロパティ
        /// </summary>
        public string Weight
        {
            get
            {
                return weight;
            }
        }
        /// <summary>
        /// 並べ替え対象からはずすフラグのオン/オフを返す読み取り専用プロパティ
        /// </summary>
        public bool IsUnsort
        {
            get
            {
                return unsortflag;
            }
        }
    }

    /// <summary>
    /// グラフの簡易情報を扱うクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("9BD1DC86-DAA2-4df4-8ED1-7B4B327A3848")]
    public class ChartInformation
    {
        // private Excel.XlChartType charttype = (Excel.XlChartType)0;
        private Common.XlChartType charttype = (Common.XlChartType)0;
        private int[] seriescolorindex = null;
        // private Office.MsoGradientStyle gradientstyle = (Office.MsoGradientStyle)0;
        private Common.MsoGradientStyle gradientstyle = (Common.MsoGradientStyle)0;
        private int gradientvariant = 1;
        private int interiorcolorindex = 36;
        private Common.MsoGradientStyle interiorgradientstyle = (Common.MsoGradientStyle)0;
        private int interiorgradientvariant = 1;
        private bool allowLine = false;

        private void init(Common.XlChartType charttype, string joinedseriescolorindex, Common.MsoGradientStyle gradientstyle, int gradientvariant, bool isMatrix)
        {
            // this.charttype = (Excel.XlChartType)charttype;
            this.charttype = charttype;
            if ((int)charttype == 0) return;
            if (string.IsNullOrWhiteSpace(joinedseriescolorindex))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceParameterMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , "joinedseriescolorindex");
            }
            string[] seriescolorindex = joinedseriescolorindex.Split(' ');
            int orgLength = seriescolorindex.Length;
            switch (this.charttype & ~XlChartType.QCM & ~XlChartType.RAT)
            {
                // case Excel.XlChartType.xlColumnClustered:
                // case Excel.XlChartType.xlBarClustered:
                case Common.XlChartType.xlColumnClustered:
                case Common.XlChartType.xlBarClustered:
                    allowLine = !isMatrix;
                    int c = allowLine ? 21 : 20;
                    if ((this.charttype & XlChartType.RAT) == XlChartType.RAT) c = 1;
                    Array.Resize<string>(ref seriescolorindex, c);
                    // Array.Resize<string>(ref seriescolorindex, isMatrix && (int)(this.charttype & XlChartType.RAT) == 0 ? 20 : 1);
                    break;
                default:
                    allowLine = false;
                    Array.Resize<string>(ref seriescolorindex, 20);
                    break;
            }
            this.seriescolorindex = new int[seriescolorindex.Length];
            for (int i = 0; i < (orgLength < seriescolorindex.Length ? orgLength : seriescolorindex.Length); ++i)
            {
                // 0(色なし)は不可
                if (!int.TryParse(seriescolorindex[i], out this.seriescolorindex[i]) || this.seriescolorindex[i] < 1 || this.seriescolorindex[i] > 56)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                            , GlobalsCommonConstant.LogLevel.FATAL
                                            , "joinedseriescolorindex", "\"" + joinedseriescolorindex + "\"");
                }
            }
            for (int i = orgLength, n = i; i < this.seriescolorindex.Length; n = i)
            {
                // n = i + n <= this.seriescolorindex.Length ? n : this.seriescolorindex.Length - i;
                if (i + n > this.seriescolorindex.Length) n = this.seriescolorindex.Length - i;
                Array.Copy(this.seriescolorindex, 0, this.seriescolorindex, i, n);
                i += n;
            }
            // this.gradientstyle = Enum.IsDefined(typeof(Office.MsoGradientStyle), gradientstyle) ? gradientstyle : (Office.MsoGradientStyle)0;
            this.gradientstyle = Enum.IsDefined(typeof(MsoGradientStyle), gradientstyle) ? gradientstyle : (MsoGradientStyle)0;
            if ((int)this.gradientstyle != 0)
            {
                switch (gradientvariant)
                {
                    case 1:
                    case 2:
                        break;
                    case 3:
                    case 4:
                        // if (this.gradientstyle == Office.MsoGradientStyle.msoGradientFromCenter) return;
                        if (this.gradientstyle == Common.MsoGradientStyle.msoGradientFromCenter)
                        {
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                                    , GlobalsCommonConstant.LogLevel.FATAL
                                                    , "gradientvariant", gradientvariant.ToString());
                        }
                        break;
                    default:
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                                , GlobalsCommonConstant.LogLevel.FATAL
                                                , "gradientvariant", gradientvariant.ToString());
                }
                this.gradientvariant = gradientvariant;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChartInformation() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="charttype">グラフの種類を表す整数値の文字列表現</param>
        /// <param name="joinedseriescolorindex">各系列色を表すインデックス番号を半角スペース区切りで連結した文字列</param>
        /// <param name="gradientstyle">グラデーションのスタイルを表す整数値の文字列表現</param>
        /// <param name="gradientvariant">グラデーションのバリエーションを表すインデックス番号の文字列表現</param>
        /// <param name="isMatrix">マトリクスの場合trueを指定 (省略可、既定値false)</param>
        public ChartInformation(string charttype, string joinedseriescolorindex, string gradientstyle, string gradientvariant, bool isMatrix = false)
        {
            int chtType = 0;
            if (!int.TryParse(charttype, out chtType))
            {
                string v = charttype == null ? "null" : "\"" + charttype + "\"";
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "charttype", v);
            }
            if (chtType == 0) return;
            // Excel.XlChartType chartType = (Common.XlChartType)chtType;
            Common.XlChartType chartType = (Common.XlChartType)chtType;
            int gradStyle = 0;
            if (!int.TryParse(gradientstyle, out gradStyle))
            {
                string v = gradientstyle == null ? "null" : "\"" + gradientstyle + "\"";
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "gradientstyle", v);
            }
            // Office.MsoGradientStyle gradientStyle = (Office.MsoGradientStyle)gradStyle;
            Common.MsoGradientStyle gradientStyle = (Common.MsoGradientStyle)gradStyle;
            int gradientVariant = 0;
            if ((int)gradientStyle != 0 && !int.TryParse(gradientvariant, out gradientVariant))
            {
                string v = gradientvariant == null ? "null" : "\"" + gradientvariant + "\"";
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "gradientvariant", v);
            }
            init(chartType, joinedseriescolorindex, gradientStyle, gradientVariant, isMatrix);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="charttype">グラフの種類を表すXlChartType列挙型の値</param>
        /// <param name="joinedseriescolorindex">各系列色を表すインデックス番号を半角スペース区切りで連結した文字列</param>
        /// <param name="gradientstyle">グラデーションのスタイルを表すMsoGradientStyle列挙型の値</param>
        /// <param name="gradientvariant">グラデーションのバリエーションを表す1～4の整数値</param>
        /// <param name="isMatrix">マトリクスの場合trueを指定 (省略可、既定値false)</param>
        public ChartInformation(Common.XlChartType charttype, string joinedseriescolorindex, Common.MsoGradientStyle gradientstyle, int gradientvariant, bool isMatrix = false)
        {
            init(charttype, joinedseriescolorindex, gradientstyle, gradientvariant, isMatrix);
        }

        /// <summary>
        /// グラフの種類を表すXlChartType列挙型の値を取得/設定するプロパティ
        /// </summary>
        public Common.XlChartType ChartType
        {
            get
            {
                // return (Common.XlChartType)charttype;
                return charttype;
            }
            set
            {
                // charttype = (Excel.XlChartType)value;
                charttype = value;
            }
        }

        /// <summary>
        /// 系列色を表す数値を返すメソッド
        /// </summary>
        /// <param name="index">系列または要素のインデックス (0ベース)</param>
        /// <returns>indexが示す系列または要素色を表す数値</returns>
        public int SeriesColorIndex(int index)
        {
            if (seriescolorindex == null || seriescolorindex.Length == 0 || index < seriescolorindex.GetLowerBound(0)) return 0;
            if (allowLine)
            {
                if (index > 0)
                {
                    if (seriescolorindex.Length == 1)   // 念のため
                    {
                        index = 0;
                    }
                    else
                    {
                        index = (index - 1) % (seriescolorindex.Length - 1) + 1;
                    }
                }
            }
            else
            {
                index = index % seriescolorindex.Length;
            }
            return seriescolorindex[index];
        }

        /// <summary>
        /// 系列の数を返す読み取り専用プロパティ
        /// </summary>
        public int SeriesCount
        {
            get
            {
                if (seriescolorindex == null) return -1;
                return seriescolorindex.Length;
            }
        }

        /// <summary>
        /// グラデーションのスタイルを表すMsoGradientStyle列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public Common.MsoGradientStyle GradientStyle
        {
            get
            {
                // return (Common.MsoGradientStyle)gradientstyle;
                return gradientstyle;
            }
        }

        /// <summary>
        /// グラデーションのバリエーションを表す1～4の整数値を返す読み取り専用プロパティ
        /// </summary>
        public int GradientVariant
        {
            get
            {
                return gradientvariant;
            }
        }

        /// <summary>
        /// プロットエリアの背景色を表す数値を取得/設定するプロパティ
        /// </summary>
        public int InteriorColorIndex
        {
            get
            {
                return interiorcolorindex;
            }
            set
            {
                if (value >= 0 && value <= 56)
                {
                    interiorcolorindex = value;
                }
            }
        }

        /// <summary>
        /// プロットエリアの背景のグラデーションのスタイルを表すMsoGradientStyle列挙型の値を取得/設定するプロパティ
        /// </summary>
        public Common.MsoGradientStyle InteriorGradientStyle
        {
            get
            {
                return interiorgradientstyle;
            }
            set
            {
                switch (value)
                {
                    case Common.MsoGradientStyle.msoGradientHorizontal:
                    case Common.MsoGradientStyle.msoGradientVertical:
                    case Common.MsoGradientStyle.msoGradientDiagonalUp:
                    case Common.MsoGradientStyle.msoGradientDiagonalDown:
                    case Common.MsoGradientStyle.msoGradientFromCorner:
                        interiorgradientstyle = value;
                        interiorgradientvariant = 3;
                        break;
                    case Common.MsoGradientStyle.msoGradientFromCenter:
                    case (Common.MsoGradientStyle)0:
                        interiorgradientstyle = value;
                        interiorgradientvariant = 1;
                        break;
                }
            }
        }

        /// <summary>
        /// プロットエリアの背景のグラデーションのバリエーションを表す1～4の整数値を返す読み取り専用プロパティ
        /// </summary>
        public int InteriorGradientVariant
        {
            get
            {
                return interiorgradientvariant;
            }
        }
    }

    /// <summary>
    /// 調査票の質問簡易情報を扱うクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("E6F2B777-262F-4ef6-A117-EE8AE4C385B3")]
    public class QuestionInformation2 : QuestionInformation
    {
        private string ruleDescription = null;
        private string qno = null;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アイテム名</param>
        /// <param name="qno">質問番号</param>
        /// <param name="description">質問文</param>
        /// <param name="questiontype">質問タイプを表す整数値の文字列表現</param>
        /// <param name="ruledescription">セレクト条件 (省略可、省略時null)</param>
        /// <param name="unescape">質問文/セレクト条件のアンエスケープが必要な場合true (省略可、既定値false)</param>
        public QuestionInformation2(string name, string qno, string description, string questiontype, string ruledescription = null, bool unescape = false)
            : base(name, description, questiontype, true, unescape)
        {
            this.qno = qno;
            if (unescape) ruledescription = System.Text.RegularExpressions.Regex.Escape(ruledescription + string.Empty);
            ruleDescription = ruledescription;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アイテム名</param>
        /// <param name="qno">質問番号</param>
        /// <param name="description">質問文</param>
        /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="ruledescription">セレクト条件 (省略可、省略時null)</param>
        /// <param name="unescape">質問文/セレクト条件のアンエスケープが必要な場合true (省略可、既定値false)</param>
        public QuestionInformation2(string name, string qno, string description, Tabulation.QuestionType questiontype, string ruledescription = null, bool unescape = false)
            : base(name, description, questiontype, true, unescape)
        {
            this.qno = qno;
            if (unescape) ruledescription = System.Text.RegularExpressions.Regex.Escape(ruledescription + string.Empty);
            ruleDescription = ruledescription;
        }

        /// <summary>
        /// 質問番号を返す読み取り専用プロパティ<br />
        /// 質問番号情報がない場合はアイテム名を返す
        /// </summary>
        public string QuestionNo
        {
            get
            {
                if (string.IsNullOrWhiteSpace(qno)) return this.Name;
                return qno;
            }
        }

        /// <summary>
        /// セレクト条件を取得/設定するプロパティ
        /// </summary>
        public string RuleDescription
        {
            get
            {
                return ruleDescription;
            }
            set
            {
                ruleDescription = value;
            }
        }
    }

    /// <summary>
    /// 調査票の選択肢簡易情報を扱うクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("EC502CC9-5AFD-4B1E-AB6D-CA515CEF2829")]
    public class SectorInformation2
    {
        private int number = 0;
        private string addedquestionname = null;
        private string description = null;

        private void init(int number, string addedquestionname, string description, bool unescape)
        {
            if (number < 0)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                        , GlobalsCommonConstant.LogLevel.FATAL
                                        , "number", number.ToString());
            }
            this.number = number;
            if (!string.IsNullOrWhiteSpace(addedquestionname))
            {
                this.addedquestionname = addedquestionname;
            }
            if (unescape)
            {
                try
                {
                    description = System.Text.RegularExpressions.Regex.Unescape(description + string.Empty);
                }
                catch (Exception)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                            , GlobalsCommonConstant.LogLevel.FATAL
                                            , "description", "\"" + description + "\"");
                }
            }
            this.description = description;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="number">選択肢番号の文字列表現</param>
        /// <param name="addedquestionname">付加アイテム名</param>
        /// <param name="description">選択肢文</param>
        /// <param name="unescape">選択肢文のアンエスケープが必要な場合true (省略可、既定値false)</param>
        public SectorInformation2(string number, string addedquestionname, string description, bool unescape = false)
        {
            int num = 0;
            if (!int.TryParse(number, out num))
            {
                string v = number == null ? "null" : "\"" + number + "\"";
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "number", v);
            }
            init(num, addedquestionname, description, unescape);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="number">選択肢番号</param>
        /// <param name="addedquestionname">付加アイテム名</param>
        /// <param name="description">選択肢文</param>
        /// <param name="unescape">選択肢文のアンエスケープが必要な場合true (省略可、既定値false)</param>
        public SectorInformation2(int number, string addedquestionname, string description, bool unescape = false)
        {
            init(number, addedquestionname, description, unescape);
        }

        /// <summary>
        /// 選択肢番号を返す読み取り専用プロパティ
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }
        }

        /// <summary>
        /// 付加アイテム名を返す読み取り専用プロパティ
        /// </summary>
        public string AddedQuestionName
        {
            get
            {
                return addedquestionname;
            }
        }

        /// <summary>
        /// 選択肢文を返す読み取り専用プロパティ
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
        }
    }

    /// <summary>
    /// 項目間検定の種類を表すコード
    /// </summary>
    [ComVisible(true), Flags]
    public enum SignificanceTestCode
    {
        /// <summary>
        /// 項目間検定を行わないことを表す (= 0)
        /// </summary>
        Off = 0,
        /// <summary>
        /// 選択肢間での検定を行うことを表す (= 1)
        /// </summary>
        BetweenSectors = 1,
        /// <summary>
        /// 子質問間での検定を行うことを表す (= 2)
        /// </summary>
        BetweenChildQuestions = 2
    }

    /// <summary>
    /// データ出力時のデータ形式を表すコード
    /// </summary>
    [ComVisible(true)]
    public enum OutputDataType : int
    {
        /// <summary>
        /// コード形式を表す (= 0)
        /// </summary>
        Code,
        /// <summary>
        /// フラグ形式を表す (= 1)
        /// </summary>
        Flag,
        /// <summary>
        /// デコード形式を表す (= 2)
        /// </summary>
        Decode,
        /// <summary>
        /// QC3形式を表す (= 3)
        /// </summary>
        QC3
    }

    /// <summary>
    /// 作成するレイアウト表の向きを表すコード
    /// </summary>
    [ComVisible(true)]
    public enum LayoutOrientation : int
    {
        /// <summary>
        /// 横型を表す (= 1)
        /// </summary>
        Landscape = 1,
        /// <summary>
        /// 縦型を表す (= 2)
        /// </summary>
        Portrait
    }

    /// <summary>
    /// 集計表コレクションインターフェイス
    /// </summary>
    [ComVisible(true), Guid("C68E6A76-AB66-4554-8B4D-A6407AA61201")]
    public interface ITables : IDisposable, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
    {
        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>インデックスが示すコレクションの要素であるTableクラス(ITableインターフェイスの実装クラス)のインスタンスへの参照</returns>
        ITable this[int index] { get; }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="key">キーとなる文字列</param>
        /// <returns>キーが示すコレクションの要素であるTableクラス(ITableインターフェイスの実装クラス)のインスタンスへの参照</returns>
        ITable this[string key] { get; }

        /// <summary>
        /// コレクションの要素数を返す読み取り専用プロパティ
        /// <note>ICollectionでCountが定義されているが、COM連携のために、ここで明示的に定義</note>
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// 実装クラスのインスタンスの親であるOutputクラス(IOutputインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IOutput ParentOutput { get; }

        /// <summary>
        /// 実装クラスのインスタンスの親であるReportsetクラス(IReportsetインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IReportset ParentReportset { get; }

        /// <summary>
        /// 実装クラスのインスタンスの親であるRequestクラス(IRequestインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IRequest ParentRequest { get; }
    }

    /// <summary>
    /// 集計表インターフェイス
    /// </summary>
    [ComVisible(true), Guid("D6B10C57-F24D-467C-8F63-D9D40E122F9B")]
    public interface ITable : IDisposable
    {
        /// <summary>
        /// インデックス番号を返す読み取り専用プロパティ
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 集計表のセルのN値の文字列表現またはキャプションを返すメソッド
        /// </summary>
        /// <param name="RowIndex">行インデックス</param>
        /// <param name="ColumnIndex">列インデックス</param>
        /// <param name="Unescape">アンエスケープ処理を行うかどうかを示すフラグ</param>
        /// <returns>
        /// 集計表データ内でRowIndexとColumnIndexとで示されるセルの文字列データまたはN値の文字列表現
        /// Unescapeがtrueのときには、正規表現のアンエスケープ処理を行ってから返す
        /// </returns>
        // 戻り値に配列を指定すると配列のコピーが返される模様で、配列への参照を返すことはできなさそう
        // (COMからの呼び出しの場合)
        // 配列のコピーは大量データの場合、最悪死ぬので、逐次アクセス
        // (→あるいはExcelマクロ側でファイル読み込みを行うか→余裕があればパフォーマンスの差を計測して決定)
        string TableValue(int RowIndex, int ColumnIndex, bool Unescape = false);

        /// <summary>
        /// 集計表のセルの％値を返すメソッド
        /// </summary>
        /// <param name="RowIndex">行インデックス</param>
        /// <param name="ColumnIndex">列インデックス</param>
        /// <returns>集計表データ内でRowIndexとColumnIndexとで示されるセルの％値</returns>
        double PercentValue(int RowIndex, int ColumnIndex);

        /// <summary>
        /// 集計表のセルのマーキング情報を表すDataMarking列挙型の値を返すメソッド
        /// </summary>
        /// <param name="RowIndex">行インデックス</param>
        /// <param name="ColumnIndex">列インデックス</param>
        /// <returns>集計表データ内でRowIndexとColumnIndexとで示されるデータのマーキング情報を表すDataMarking列挙型の値</returns>
        Tabulation.DataMarking DataMarking(int RowIndex, int ColumnIndex);

        /// <summary>
        /// 集計表のセルの項目間検定レターを返すメソッド
        /// </summary>
        /// <param name="RowIndex">行インデックス</param>
        /// <param name="ColumnIndex">列インデックス</param>
        /// <returns>集計表データ内でRowIndexとColumnIndexとで示されるデータの項目間検定レター</returns>
        string SignificanceTestCharacters(int RowIndex, int ColumnIndex);

        /// <summary>
        /// 集計表データの行インデックスの最小値を返す読み取り専用プロパティ
        /// </summary>
        int GetTableValueRowIndexMinimum { get; }

        /// <summary>
        /// 集計表データの行インデックスの最大値を返す読み取り専用プロパティ
        /// </summary>
        int GetTableValueRowIndexMaximum { get; }

        /// <summary>
        /// 集計表データの列インデックスの最小値を返す読み取り専用プロパティ
        /// </summary>
        int GetTableValueColumnIndexMinimum { get; }

        /// <summary>
        /// 集計表データの列インデックスの最大値を返す読み取り専用プロパティ
        /// </summary>
        int GetTableValueColumnIndexMaximum { get; }

        /// <summary>
        /// コメントを返す読み取り専用プロパティ
        /// </summary>
        string Comment { get; }

        /// <summary>
        /// 実装クラスのインスタンスが格納されているTablesコレクションクラス(ITablesインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        ITables ParentCollection { get; }

        /// <summary>
        /// 実装クラスのインスタンスの親であるOutputクラス(IOutputインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IOutput ParentOutput { get; }

        /// <summary>
        /// 実装クラスのインスタンスの親であるReportsetクラス(IReportsetインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IReportset ParentReportset { get; }

        /// <summary>
        /// 実装クラスインスタンスの親であるRequestクラス(IRequestインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IRequest ParentRequest { get; }
    }

    /// <summary>
    /// GT表の集計表インターフェイス
    /// </summary>
    [ComVisible(true), Guid("61D64986-E4E6-4993-842D-1DC6BA87BCB3")]
    public interface IGTTable : ITable
    {
        /// <summary>
        /// 分類アイテムの簡易情報を保持したKeyItemInformationクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        KeyItemInformation KeyItem { get; }

        /// <summary>
        /// 集計アイテムの簡易情報を保持したQuestionInformationクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        QuestionInformation Question { get; }

        /// <summary>
        /// 項目間検定の種類を表すSignificanceTestCode列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        SignificanceTestCode SignificancetestCode { get; }

        /// <summary>
        /// 集計対象アイテムの子質問数を返す読み取り専用プロパティ<br />
        /// マトリクス以外では0
        /// </summary>
        int ChildQuestionsCount { get; }

        /// <summary>
        /// グラフの簡易情報を保持したChartInformationクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        ChartInformation Chart { get; }

        /// <summary>
        /// 円グラフの出力時に、選択肢名を非表示にする最大パーセンテージを、-1～50の整数で返す読み取り専用プロパティ<br />
        /// 既定値-1
        /// </summary>
        int HideChartDescriptionMaxPercent { get; }

        /// <summary>
        /// 集計対象質問の選択肢の簡易情報を保持したSectorInformationクラスのインスタンスへの参照を返すメソッド
        /// </summary>
        /// <param name="index">選択肢のインデックス</param>
        /// <returns>インデックスが示す選択肢の簡易情報を保持したSectorInformationクラスのインスタンスへの参照</returns>
        SectorInformation Sector(int index);

        /// <summary>
        /// 集計対象質問の選択肢数を返す読み取り専用プロパティ
        /// </summary>
        int SectorsCount { get; }
    }

    /// <summary>
    /// クロス表の集計表インターフェイス
    /// </summary>
    [ComVisible(true), Guid("BCFEFB10-4606-42FF-A8F0-F02612DF5407")]
    public interface ICrossTable : IGTTable
    {
        /// <summary>
        /// 集計軸グループコレクションへの参照を返す読み取り専用プロパティ
        /// </summary>
        AxesGroupInformation AxesGroups { get; }

        /// <summary>
        /// セット番号を取得するプロパティ (1シート1クロスのシナリオ出力時に有効)
        /// </summary>
        int SetNo { get; }

        /// <summary>
        /// マトリクスの親質問番号を取得するプロパティ (1シート1クロスのシナリオ出力時に有効)
        /// </summary>
        string ParentQNo { get; }
    }

    /// <summary>
    /// FAリストの集計表インターフェイス
    /// </summary>
    [ComVisible(true), Guid("3E32D6AE-44E8-411D-B1FE-EA1D46B0ED37")]
    public interface IFAListTable : ITable
    {
        /// <summary>
        /// 分類アイテムの簡易情報を保持したKeyItemInformationクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        KeyItemInformation KeyItem { get; }

        /// <summary>
        /// FAアイテム数を返す読み取り専用プロパティ
        /// </summary>
        int FAItemsCount { get; }

        /// <summary>
        /// 付加アイテム数を返す読み取り専用プロパティ
        /// </summary>
        int AddedItemsCount { get; }

        /// <summary>
        /// 先頭のFAアイテム名を返す読み取り専用プロパティ
        /// </summary>
        string TopItemName { get; }
    }

    /// <summary>
    /// チェックリストの集計表インターフェイス
    /// </summary>
    [ComVisible(true), Guid("EDDA3CD8-A360-4C2D-ACAC-436524CA5ADE")]
    public interface ICheckListTable : ITable
    {
        /// <summary>
        /// 集計アイテムの簡易情報を保持したQuestionInformationクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        QuestionInformation Question { get; }

        /// <summary>
        /// 集計アイテムの選択肢数を返す読み取り専用プロパティ
        /// </summary>
        int SectorsCount { get; }

        /// <summary>
        /// 変更の有無を返す読み取り専用プロパティ
        /// </summary>
        bool IsChanged { get; }

        /// <summary>
        /// 新アイテムかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool IsNewItem { get; }
    }

    /// <summary>
    /// 調査票の集計表インターフェイス
    /// </summary>
    [ComVisible(true), Guid("FDA89147-588F-4ADE-B6E2-E3ECF8E683C8")]
    public interface IQuestionnaireTable : ITable
    {
        /// <summary>
        /// 質問の簡易情報を保持したQuestionInformation2クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        QuestionInformation2 Question { get; }

        /// <summary>
        /// 選択肢の簡易情報を保持したSectorInformation2クラスのインスタンスへの参照を返すメソッド
        /// </summary>
        /// <param name="index">選択肢のインデックス</param>
        /// <returns>インデックスが示す選択肢の簡易情報を保持したSectorInformation2クラスのインスタンスへの参照</returns>
        SectorInformation2 Sector(int index);

        /// <summary>
        /// 選択肢数を返す読み取り専用プロパティ
        /// </summary>
        int SectorsCount { get; }

        /// <summary>
        /// 子質問の簡易情報を保持したQuestionInformation2クラスのインスタンスへの参照を返すメソッド
        /// </summary>
        /// <param name="index">子質問のインデックス</param>
        /// <returns>インデックスが示す子質問の簡易情報を保持したQuestionInformation2クラスのインスタンスへの参照</returns>
        QuestionInformation2 ChildQuestion(int index);

        /// <summary>
        /// 子質問数を返す読み取り専用プロパティ
        /// </summary>
        int ChildQuestionSCount { get; }
    }

    /// <summary>
    /// ローデータの集計表インターフェイス
    /// </summary>
    public interface IRawDataTable : ITable
    {
        /// <summary>
        /// データ出力時のデータ形式を表すOutputDataType列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        OutputDataType DataType { get; }

        /// <summary>
        /// レイアウト表の向きを表すLayoutOrientation列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        LayoutOrientation LayoutOrientation { get; }

        /// <summary>
        /// 調査概要の調査情報を保持したResearchInformationsArray構造体の値からなる配列を返す読み取り専用プロパティ
        /// </summary>
        ResearchInformation[] ResearchInformationsArray { get; }

        /// <summary>
        /// 調査概要の割付セル情報を保持したCellInformation構造体の値からなる配列を返す読み取り専用プロパティ
        /// </summary>
        CellInformation[] CellInformationsArray { get; }

        /// <summary>
        /// 調査概要のセレクト条件情報を保持したRuleInformation構造体の値からなる配列を返す読み取り専用プロパティ
        /// </summary>
        RuleInformation[] RuleInformationsArray { get; }
    }
}
