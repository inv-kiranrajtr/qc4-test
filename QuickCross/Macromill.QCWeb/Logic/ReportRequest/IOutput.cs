#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：IOutput.cs
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
using System.Collections;
using System.Runtime.Serialization;

namespace Macromill.QCWeb.ReportRequest
{
    #region 列挙型
    /// <summary>
    /// 無回答/非該当の表示/非表示情報を表すコード
    /// </summary>
    [Flags, ComVisible(true)]
    public enum ShowCode : int
    {
        /// <summary>
        /// 集計対象での表示を示す (= 1)
        /// </summary>
        Item = 1,   // 集計対象
        /// <summary>
        /// 集計軸での表示を示す (= 2)
        /// </summary>
        Axis = 2
    }

    /// <summary>
    /// WB集計のオン/オフ、WB前全体の表示/非表示を表すコード
    /// </summary>
    [Flags, ComVisible(true)]
    public enum WBSettingCode : int
    {
        /// <summary>
        /// WB集計していないことを表す (= 0)
        /// </summary>
        WBOff = 0,
        /// <summary>
        /// WB集計していることを表す (= 1)
        /// </summary>
        WBOn = 1,
        /// <summary>
        /// WB前全体を表示しないことを表す (= 0)
        /// </summary>
        HidePreWB = 0,
        /// <summary>
        /// WB前全体を表示することを表す (= 2)
        /// </summary>
        ShowPreWB = 2
    }

    /// <summary>
    /// 出力物の種類を表すコード
    /// </summary>
    [ComVisible(true)]
    public enum OutputType : int
    {
        /// <summary>
        /// PowerPointテンプレートのチェックを表す (= 0)
        /// </summary>
        CheckTemplate,
        /// <summary>
        /// GT表の出力を表す (= 1)
        /// </summary>
        GT,
        /// <summary>
        /// クロス表の出力を表す (= 2)
        /// </summary>
        Cross,
        /// <summary>
        /// FAリストの出力を表す (= 3)
        /// </summary>
        FAList,
        /// <summary>
        /// チェックリストの出力を表す (= 4)
        /// </summary>
        CheckList,
        /// <summary>
        /// 調査票の出力を表す (= 5)
        /// </summary>
        Questionnaire,
        /// <summary>
        /// Excel形式のローデータ並びにレイアウトの出力を表す (= 6)
        /// </summary>
        RawData,
        /// <summary>
        /// QC3形式のローデータ並びにレイアウトの出力を表す (= 7)
        /// </summary>
        QC3
    }

    /// <summary>
    /// 作成する集計表の種類を表すコード
    /// </summary>
    [Flags, ComVisible(true)]
    public enum TableType : int
    {
        /// <summary>
        /// N％表を表す (= 1)
        /// </summary>
        NPer = 1,
        /// <summary>
        /// N表を表す (= 2)
        /// </summary>
        N = 2,
        /// <summary>
        /// ％表を表す (= 4)
        /// </summary>
        Per = 4,
        /// <summary>
        /// 検定表を表す (= 8)
        /// </summary>
        SignificanceTest = 8
    }

    /// <summary>
    /// 項目間検定の有意水準を表すコード
    /// </summary>
    [Flags, ComVisible(true)]
    public enum SignificanceTestLevel : int
    {
        /// <summary>
        /// 有意水準1％での検定を表す (= 1)
        /// </summary>
        One = 1,
        /// <summary>
        /// 有意水準5％での検定を表す (= 2)
        /// </summary>
        Five = 2,
        /// <summary>
        /// 有意水準10％での検定を表す (= 4)
        /// </summary>
        Ten = 4
    }

    /// <summary>
    /// 作成する集計表の向きを表すコード
    /// </summary>
    [ComVisible(true)]
    public enum TableOrientation : int
    {
        /// <summary>
        /// 横％表を表す (= 0)
        /// </summary>
        Landscape,
        /// <summary>
        /// 縦％表を表す (= 1)
        /// </summary>
        Portrait
    }

    /// <summary>
    /// ステータスコード
    /// </summary>
    [Flags, ComVisible(true)]
    public enum StatusCode : int
    {
        /// <summary>
        /// 出力リクエスト待ち状態を表す (= 0)
        /// </summary>
        WaitRequest = 0,
        /// <summary>
        /// 処理中を表す (= 2048)
        /// </summary>
        Running = 2048,
        /// <summary>
        /// 処理が終了していることを表す (= 4096)
        /// </summary>
        Over = 4096
    }

    /// <summary>
    /// 行うマーキングの種類を表すコード
    /// </summary>
    [Flags, ComVisible(true)]
    public enum MarkingType : int
    {
        /// <summary>
        /// 全体との比率の差の水準1高での色付けのマーキングを行うことを表す (= 1)
        /// </summary>
        ColoringLevel1High = 1,
        /// <summary>
        /// 全体との比率の差の水準1低での色付けのマーキングを行うことを表す (= 2)
        /// </summary>
        ColoringLevel1Low = 2,
        /// <summary>
        /// 全体との比率の差の水準2高での色付けのマーキングを行うことを表す (= 4)
        /// </summary>
        ColoringLevel2High = 4,
        /// <summary>
        /// 全体との比率の差の水準2低での色付けのマーキングを行うことを表す (= 8)
        /// </summary>
        ColoringLevel2Low = 8,
        /// <summary>
        /// ランキングのマーキングを行うことを表す (= 16)
        /// </summary>
        Ranking = 16,
        /// <summary>
        /// 昇降分析のマーキングを行うことを表す (= 32)
        /// </summary>
        Ascending = 32,
        /// <summary>
        /// 全体との比率の差の1％検定のマーキングを行うことを表す (= 64)
        /// </summary>
        SignificanceOne = 64,
        /// <summary>
        /// 全体との比率の差の5％検定のマーキングを行うことを表す (= 128)
        /// </summary>
        SignificanceFive = 128,
        /// <summary>
        /// 全体との比率の差の10％検定のマーキングを行うことを表す (= 256)
        /// </summary>
        SignificanceTen = 256,
        /// <summary>
        /// 全体との比率の差の水準1での色付けのマーキングを行うことを表す (= 3)
        /// </summary>
        ColoringLevel1 = ColoringLevel1High | ColoringLevel1Low,
        /// <summary>
        /// 全体との比率の差の水準2での色付けのマーキングを行うことを表す (= 12)
        /// </summary>
        ColoringLevel2 = ColoringLevel2High | ColoringLevel2Low,
        /// <summary>
        /// 全体との比率の差の色付けのマーキングを行うことを表す (= 15)
        /// </summary>
        Coloring = ColoringLevel1 | ColoringLevel2,
        /// <summary>
        /// 全体との比率の差の検定のマーキングを行うことを表す (= 448)
        /// </summary>
        Significance = SignificanceOne | SignificanceFive | SignificanceTen
    }

    /// <summary>
    /// 1つのワークシートに出力する集計表の単/複を表すコード
    /// </summary>
    [ComVisible(true)]
    public enum TablesOnOneSheet : int
    {
        /// <summary>
        /// 1シートに複数(1つ以上)のクロス表を出力することを表す (= 0)
        /// </summary>
        Multi,
        /// <summary>
        /// 1シートに1つのクロス表を出力することを表す (= 1)
        /// </summary>
        Single
    }
    #endregion

    /// <summary>
    /// 出力物コレクションインターフェイス
    /// </summary>
    [ComVisible(true), Guid("9F007806-E398-4C3C-8A60-25F4EA3D3712")]
    public interface IOutputs : IDisposable, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
    {
        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="key">キーとなる文字列</param>
        /// <returns>キーが示すコレクションの要素であるOutputクラス(IOutputインターフェイスの実装クラス)のインスタンスへの参照</returns>
        IOutput this[string key] { get; }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="id">出力物ID</param>
        /// <returns>出力物IDが示すコレクションの要素であるOutputクラス(IOutputインターフェイスの実装クラス)のインスタンスへの参照</returns>
        [ComVisible(false)]
        IOutput this[decimal id] { get; }

        /// <summary>
        /// コレクションの要素数を返す読み取り専用プロパティ
        /// <note>ICollectionでCountが定義されているが、COM連携のために、ここで明示的に定義</note>
        /// </summary>
        new int Count { get; }

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
    /// 出力物インターフェイス
    /// </summary>
    [ComVisible(true), Guid("408957E9-1E11-44DE-A224-C3E20DBC96C4")]
    public interface IOutput : IDisposable
    {
        /// <summary>
        /// 出力物に紐づくTablesコレクションクラス(ITablesインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        ITables Tables { get; }

        /// <summary>
        /// 出力物IDを返す読み取り専用プロパティ
        /// </summary>
        [ComVisible(false)]
        decimal ID { get; }

        /// <summary>
        /// 出力物のExcelブック名のプリフィックスを返す読み取り専用プロパティ
        /// </summary>
        string ExcelBookNamePrefix { get; }

        /// <summary>
        /// 出力物の種類を表すOutputType列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        OutputType OutputType { get; }

        /// <summary>
        /// 実装クラスのインスタンスが格納されているOutputsコレクションクラス(IOutputsインターフェイスの実装クラス)のインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IOutputs ParentCollection { get; }

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
    /// FAリストの出力物インターフェイス
    /// </summary>
    [ComVisible(true), Guid("7554A9E2-9A32-4035-B27C-6B62DEA6AEC9")]
    public interface IOutputFA : IOutput
    {
        /// <summary>
        /// ページ設定時の用紙サイズを表すXlPaperSize列挙型の値を取得/設定するプロパティ
        /// </summary>
        Macromill.QCWeb.Common.XlPaperSize PaperSize { get; set; }

        /// <summary>
        /// ページ設定時の用紙の向きを表すXlPageOrientation列挙型の値を取得/設定するプロパティ
        /// </summary>
        Macromill.QCWeb.Common.XlPageOrientation PaperOrientation { get; set; }

        /// <summary>
        /// ページ設定のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool PageSetup { get; }

        /// <summary>
        /// 絞込み条件式を返す読み取り専用プロパティ
        /// </summary>
        string FilteringExpression { get; }
    }

    /// <summary>
    /// GT表の出力物インターフェイス
    /// </summary>
    [ComVisible(true), Guid("23746A58-69BE-425F-965B-2B5011345AC0")]
    public interface IOutputGT : IOutputFA
    {
        /// <summary>
        /// N％表作成のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool OutputNPerTable { get; }

        /// <summary>
        /// N表作成のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool OutputNTable { get; }

        /// <summary>
        /// ％表作成のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool OutputPerTable { get; }

        /// <summary>
        /// 作成する集計表の向きを表すTableOrientation列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        TableOrientation Orientation { get; }

        /// <summary>
        /// N％表のページ設定のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool PageSetupNPerTable { get; }

        /// <summary>
        /// N表のページ設定のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool PageSetupNTable { get; }

        /// <summary>
        /// ％表のページ設定のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool PageSetupPerTable { get; }

        /// <summary>
        /// 項目間検定表のページ設定のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool PageSetupSignificanceTestTable { get; }

        /// <summary>
        /// 色付けのマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingColoring { get; }

        /// <summary>
        /// 水準1での色付けのマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingColoringLevel1 { get; }

        /// <summary>
        /// 水準2での色付けのマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingColoringLevel2 { get; }

        /// <summary>
        /// 水準1高での色付けのマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingColoringLevel1High { get; }

        /// <summary>
        /// 水準1低での色付けのマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingColoringLevel1Low { get; }

        /// <summary>
        /// 水準2高での色付けのマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingColoringLevel2High { get; }

        /// <summary>
        /// 水準2低での色付けのマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingColoringLevel2Low { get; }

        /// <summary>
        /// ランキングのマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingRanking { get; }

        /// <summary>
        /// 昇降分析のマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingAscending { get; }

        /// <summary>
        /// 有意差検定のマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingSignificance { get; }

        /// <summary>
        /// 1％有意差検定のマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingSignificanceOne { get; }

        /// <summary>
        /// 5％有意差検定のマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingSignificanceFive { get; }

        /// <summary>
        /// 10％有意差検定のマーキングを行うかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool MarkingSignificanceTen { get; }

        /// <summary>
        /// マーキングを行う条件とする母数の最小値を返す読み取り専用プロパティ
        /// </summary>
        int MinSamplesCountOnMarking { get; }

        /// <summary>
        /// 項目間検定のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool SignificanceTest { get; }

        /// <summary>
        /// 有意水準1％での項目間検定のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool SignificanceTestOne { get; }

        /// <summary>
        /// 有意水準5％での項目間検定のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool SignificanceTestFive { get; }

        /// <summary>
        /// 有意水準10％での項目間検定のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool SignificanceTestTen { get; }

        /// <summary>
        /// 集計対象の無回答を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowNAAtItem { get; }

        /// <summary>
        /// 集計軸の無回答を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowNAAtAxis { get; }

        /// <summary>
        /// 集計対象の非該当を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowIVAtItem { get; }

        /// <summary>
        /// 集計軸の非該当を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowIVAtAxis { get; }

        /// <summary>
        /// WB集計のオン/オフを返す読み取り専用プロパティ
        /// </summary>
        bool WBOn { get; }

        /// <summary>
        /// WB前全体を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool ShowPreWBTotal { get; }
    }

    /// <summary>
    /// クロス集計表の出力物インターフェイス
    /// </summary>
    [ComVisible(true), Guid("69219579-9663-4031-8C1E-42DD031C6AF6")]
    public interface IOutputCross : IOutputGT
    {
        /// <summary>
        /// 1つのワークシートに出力する集計表の数を表すTablesOnOneSheet列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        TablesOnOneSheet TablesOnOnesheet { get; }

        /// <summary>
        /// 水準2高(+10％)の色付けで使用する色インデックスを表す数値を返す読み取り専用プロパティ
        /// </summary>
        int Level2HighColorIndex { get; }

        /// <summary>
        /// 水準1高(+5％)の色付けで使用する色インデックスを表す数値を返す読み取り専用プロパティ
        /// </summary>
        int Level1HighColorIndex { get; }

        /// <summary>
        /// 水準1低(-5％)の色付けで使用する色インデックスを表す数値を返す読み取り専用プロパティ
        /// </summary>
        int Level1LowColorIndex { get; }

        /// <summary>
        /// 水準2低(-10％)の色付けで使用する色インデックスを表す数値を返す読み取り専用プロパティ
        /// </summary>
        int Level2LowColorIndex { get; }

        /// <summary>
        /// 水準1のパーセンテージを返す読み取り専用プロパティ
        /// </summary>
        int Level1Percent { get; }

        /// <summary>
        /// 水準2のパーセンテージを返す読み取り専用プロパティ
        /// </summary>
        int Level2Percent { get; }
    }

    /// <summary>
    /// チェックリストの出力物インターフェイス
    /// </summary>
    [ComVisible(true), Guid("3F1B6413-58AA-4EEB-B0C4-8AC62C3B16C1")]
    public interface IOutputCheckList : IOutput
    {
        /// <summary>
        /// 全体数(サンプル数)を返す読み取り専用プロパティ
        /// </summary>
        int TotalCount { get; }
    }
}
