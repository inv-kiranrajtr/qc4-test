#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：IRequest.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/3/19
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/8
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.ReportRequest
{
    /// <summary>
    /// 0件の無回答または非該当を表示することを示すコード
    /// <note>現行ではまとめてオン/オフのみ、将来の拡張に備えて無回答と非該当を個別に管理できる形にしている</note>
    /// </summary>
    [Flags, ComVisible(true)]
    public enum ZeroNAIVShowCode : int
    {
        /// <summary>
        /// 無回答を0件でも表示することを示す (= 1)
        /// </summary>
        NA = 1,
        /// <summary>
        /// 非該当を0件でも表示することを示す (= 2)
        /// </summary>
        IV = 2
    }

    /// <summary>
    /// 数値回答質問の表示集計項目を表すコード
    /// </summary>
    [Flags, ComVisible(true)]
    public enum NumericContentsCode : int
    {
        /// <summary>
        /// 統計量母数の表示を示す (= 1)
        /// </summary>
        Parameter = 1,
        /// <summary>
        /// 合計の表示を示す (= 2)
        /// </summary>
        Summary = 2,
        /// <summary>
        /// 平均の表示を示す (= 4)
        /// </summary>
        Average = 4,
        /// <summary>
        /// 標準偏差の表示を示す (= 8)
        /// </summary>
        Stdev = 8,
        /// <summary>
        /// 最小値の表示を示す (= 16)
        /// </summary>
        Minimum = 16,
        /// <summary>
        /// 最大値の表示を示す (= 32)
        /// </summary>
        Maximum = 32,
        /// <summary>
        /// 中央値の表示を示す (= 64)
        /// </summary>
        Median = 64,
        /// <summary>
        /// すべての数値回答集計項目の表示を示す (= 127)
        /// </summary>
        All = Parameter | Summary | Average | Stdev | Minimum | Maximum | Median
    }

    /// <summary>
    /// 処理重度を表すコード
    /// </summary>
    [ComVisible(true)]
    public enum ProcessingWeightCode : int
    {
        /// <summary>
        /// チェックリスト作成を示す (= 0)
        /// </summary>
        CheckList = 0,  // チェックリスト
        /// <summary>
        /// 極小を示す (= 1)
        /// </summary>
        Minimum = 1,
        /// <summary>
        /// 小を示す (= 2)
        /// </summary>
        Smallness = 2,
        /// <summary>
        /// 中を示す (= 3)
        /// </summary>
        Middle = 3,
        /// <summary>
        /// 大を示す (= 4)
        /// </summary>
        Large = 4,
        /// <summary>
        /// 極大を示す (= 5)
        /// </summary>
        Maximum = 5,
        /// <summary>
        /// PPテンプレートチェックを示す (= 10)
        /// </summary>
        CheckTemplate = 10
    }

    /// <summary>
    /// 出力物ZIPファイルタイプ
    /// </summary>
    [ComVisible(false)]
    public enum OutputZIPType : int {
        /// <summary>GT表の出力を表す</summary>
        GT,
        /// <summary>クロス表の出力を表す</summary>
        Cross,
        /// <summary>FAリストの出力を表す</summary>
        FA,
        /// <summary>Reportの出力を表す</summary>
        Report,
        /// <summary>チェックリストの出力を表す</summary>
        CheckList,
        /// <summary>調査票の出力を表す</summary>
        Questionnaire,
        /// <summary>データ出力を表す</summary>
        Data,
        /// <summary>検定ログを表す</summary>
        TestLog
    }

    /// <summary>
    /// 出力リクエストインターフェイス
    /// </summary>
    [ComVisible(true), Guid("E153B49F-DADA-454C-A3F3-081C3345370B")]
    public interface IRequest : IDisposable
    {
        /// <summary>
        /// リクエストに紐づくReportsetsクラス(IReportsetsインターフェイスの実装クラス)への参照を返す読み取り専用プロパティ
        /// </summary>
        IReportsets Reportsets { get; }

        /// <summary>
        /// リクエストIDを返す読み取り専用プロパティ
        /// </summary>
        [ComVisible(false)]
        decimal ID { get; }

        /// <summary>
        /// 調査IDを返す読み取り専用プロパティ
        /// </summary>
        [ComVisible(false)]
        decimal QCWebID { get; }

        /// <summary>
        /// リクエストを発行したサーバのサーバコードを返す読み取り専用プロパティ
        /// </summary>
        string RequestServerCode { get; }

        /// <summary>
        /// 出力物のダウンロードパスを返す読み取り専用プロパティ
        /// </summary>
        string DownloadPath { get; }

        /// <summary>
        /// 調査タイトルを返す読み取り専用プロパティ
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 出力するExcelブックのファイル形式を表すXlFileFormat列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        Common.XlFileFormat ExcelFileFormat { get; }

        /// <summary>
        /// 0件の無回答/非該当を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowZeroNAIV { get; }

        /// <summary>
        /// 数値回答質問の集計時に、統計量母数を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowParameter { get; }

        /// <summary>
        /// 数値回答質問の集計時に、合計を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowSummary { get; }

        /// <summary>
        /// 数値回答質問の集計時に、平均を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowAverage { get; }

        /// <summary>
        /// 数値回答質問の集計時に、標準偏差を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowStdev { get; }

        /// <summary>
        /// 数値回答質問の集計時に、最小値を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowMinimum { get; }

        /// <summary>
        /// 数値回答質問の集計時に、最大値を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowMaximum { get; }

        /// <summary>
        /// 数値回答質問の集計時に、中央値を表示するかどうかを返す読み取り専用プロパティ
        /// WB集計時には無視される
        /// </summary>
        bool ShowMedian { get; }

        /// <summary>
        /// 集計軸のセルを結合するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MergeAxis { get; }

        /// <summary>
        /// 表示する小数点以下の桁数を返すメソッド
        /// </summary>
        /// <param name="ncontentscode">数値回答質問の集計項目を表すNumericContentsCode列挙型の値</param>
        /// <returns>ncontentscodeが表す数値回答質問の集計項目で、表示する小数点以下の桁数</returns>
        int NumDigitsAfterDecimal(NumericContentsCode ncontentscode);

        /// <summary>
        /// ウエイト値設定時に、表示する小数点以下の桁数を返す読み取り専用プロパティ
        /// </summary>
        int WeightNumDigitsAfterDecimal { get; }

        /// <summary>
        /// 加重平均算出時に、表示する小数点以下の桁数を返す読み取り専用プロパティ
        /// </summary>
        int WeightAverageNumDigitsAfterDecimal { get; }

        ///// <summary>
        ///// 言語を返す読み取り専用プロパティ
        ///// </summary>
        //string Language { get; }

        /// <summary>
        /// ロケーションを表すコードを返す読み取り専用プロパティ
        /// </summary>
        string LocationCode{ get; }

        ///// <summary>
        ///// リクエストユーザIDを返す読み取り専用プロパティ
        ///// </summary>
        //string RequestUserId { get; }

        ///// <summary>
        ///// 出力するPowerPointのファイル形式を表すPpSaveAsFileType列挙型の値を返す読み取り専用プロパティ
        ///// </summary>
        //Common.PpSaveAsFileType PptFileFormat { get; }
    }
}
