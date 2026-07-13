#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：EnumeratedType.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/7/24
 * 作　成　者：井川はるき
 * 更　新　日：2012/7/24
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion

using System;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Tabulation
{
    #region 列挙型
    /// <summary>
    /// 質問タイプ列挙型
    /// </summary>
    [Flags, ComVisible(true)]
    public enum QuestionType : int
    {
        /// <summary>
        /// 単一回答質問を表す (2 ^ 0 = 1)
        /// </summary>
        SA = 1, // Math.Pow(2, 0),
        /// <summary>
        /// 複数回答質問を表す (2 ^ 1 = 2)
        /// </summary>
        MA = 2, // Math.Pow(2, 1),
        /// <summary>
        /// 自由回答質問を表す (2 ^ 2 = 4)
        /// </summary>
        FA = 4, // Math.Pow(2, 2),
        /// <summary>
        /// 数値回答質問を表す (2 ^ 3 = 8) <br />
        /// NはFAの一形態とはしない(付加FAにNはない)
        /// </summary>
        N = 8, // Math.Pow(2, 3),
        /// <summary>
        /// 付加質問を表す (2 ^ 5 = 32) <br />
        /// (付加FAはFAのビットとFA_Subのビットの両方が立つ)
        /// </summary>
        FA_Sub = 32, // Math.Pow(2, 5),
        /// <summary>
        /// 割合回答質問を表す (2 ^ 6 = 64) <br />
        /// (NのビットとMatrixParentのビットも立つ)
        /// </summary>
        Ratio = 64, // Math.Pow(2, 6),
        /// <summary>
        /// 順位回答質問を表す (2 ^ 7 = 128) <br />
        /// (SAのビットとMatrixParentのビットも立つ)
        /// </summary>
        Rank = 128, // Math.Pow(2, 7),
        /// <summary>
        /// マトリクスの親質問を表す (2 ^ 9 = 512)
        /// </summary>
        MatrixParent = 512, // Math.Pow(2, 9),
        /// <summary>
        /// マトリクスの子質問を表す (2 ^ 10 = 1024)
        /// </summary>
        MatrixChild = 1024, // Math.Pow(2, 10),
        /// <summary>
        /// 属性項目を表す (2 ^ 12 = 4096)
        /// </summary>
        Property = 4096, // Math.Pow(2, 12), 
        /// <summary>
        /// 集計結果での並べ替えを行うことを表すフラグ (2 ^ 14 = 16384)
        /// </summary>
        Sort = 16384 // Math.Pow(2, 14)
    }

    /// <summary>
    /// データ種別列挙型
    /// </summary>
    [Flags, ComVisible(true)]
    public enum DataType : int
    {
        /// <summary>
        /// 通常データを表す (= 0)
        /// </summary>
        NormalData = 0,
        /// <summary>
        /// 無回答データを表す (= 1)
        /// </summary>
        NAData = 1,
        /// <summary>
        /// 非該当データを表す (= 2)
        /// </summary>
        IVData = 2,
        /// <summary>
        /// 削除済みデータフラグ (2 ^ 2 = 4)
        /// </summary>
        DeletedData = 4
    }

    /// <summary>
    /// 演算子列挙型
    /// </summary>
    [ComVisible(true)]
    public enum Operator : int
    {
        /// <summary>
        /// AND演算(論理積)を表す (= 0)
        /// </summary>
        opAnd,
        /// <summary>
        /// OR演算(論理和)を表す (= 1)
        /// </summary>
        opOr
    }

    /// <summary>
    /// 絞り込み演算子列挙型
    /// </summary>
    [ComVisible(true)]
    public enum CriteriaOperator : int
    {
        /// <summary>
        /// 「○と等しい」を表す (○は単一の値) (= 0)<br />
        /// (SA/MA/N/FA/無回答/非該当で使用可)
        /// </summary>
        Equal,
        /// <summary>
        /// 「○と等しくない」を表す (○は単一の値) (= 1)<br />
        /// (SA/MA/N/FA/無回答/非該当で使用可)
        /// </summary>
        NotEqual,
        /// <summary>
        /// 「○○のいずれかと等しい」を表す (○○は1つ以上の値からなるリスト) (= 2)<br />
        /// (SA/N/FA/無回答/非該当で使用可)
        /// </summary>
        Anyone,
        /// <summary>
        /// 「○○のいずれとも等しくない」を表す (○○は1つ以上の値からなるリスト) (= 3)<br />
        /// (SA/N/FA/無回答/非該当で使用可)
        /// </summary>
        NotEqualAnyone,
        /// <summary>
        /// 「○○のすべてを含む」を表す (○○は1つ以上の値からなるリスト) (= 4)<br />
        /// (MAで使用可)
        /// </summary>
        IncludeAll,
        /// <summary>
        /// 「○○のいずれかを含む」を表す (○○は1つ以上の値からなるリスト) (= 5)<br />
        /// (MAで使用可)
        /// </summary>
        IncludeAnyone,
        /// <summary>
        /// 「○○のいずれかを含み、○○以外を含まない」を表す (○○は1つ以上の値からなるリスト) (= 6)<br />
        /// (MAで使用可)
        /// </summary>
        IncludeAnyoneAndNotIncludeUnList,
        /// <summary>
        /// 「○○のいずれも含まない」を表す (○○は1つ以上の値からなるリスト) (= 7)<br />
        /// (MAで使用可)
        /// </summary>
        NotIncludeAnyone,
        /// <summary>
        /// 「○より大きい」を表す (○は単一の値) (= 8)<br />
        /// (Nで使用可)
        /// </summary>
        Greater,
        /// <summary>
        /// 「○以上」を表す (○は単一の値) (= 9)<br />
        /// (Nで使用可)
        /// </summary>
        GreaterEqual,
        /// <summary>
        /// 「○より小さい」を表す (○は単一の値) (= 10)<br />
        /// (Nで使用可)
        /// </summary>
        Less,
        /// <summary>
        /// 「○以下」を表す (○は単一の値) (= 11)<br />
        /// (Nで使用可)
        /// </summary>
        LessEqual,
        /// <summary>
        /// 「～で始まる」を表す (～は文字列) (= 12)<br />
        /// (FAで使用可)
        /// </summary>
        BeginAt,
        /// <summary>
        /// 「～で始まらない」を表す (～は文字列) (= 13)<br />
        /// (FAで使用可)
        /// </summary>
        NotBeginAt,
        /// <summary>
        /// 「～で終わる」を表す (～は文字列) (= 14)<br />
        /// (FAで使用可)
        /// </summary>
        EndAt,
        /// <summary>
        /// 「～で終わらない」を表す (～は文字列) (= 15)<br />
        /// (FAで使用可)
        /// </summary>
        NotEndAt,
        /// <summary>
        /// 「～を含む」を表す (～は文字列) (= 16)<br />
        /// (FAで使用可)
        /// </summary>
        Include,
        /// <summary>
        /// 「～で含まない」を表す (～は文字列) (= 17)<br />
        /// (FAで使用可)
        /// </summary>
        NotInclude,
        /// <summary>
        /// 「～とパターンが一致する」を表す (～はパターン文字列) (= 18)<br />
        /// (FAで使用可)
        /// </summary>
        PatternMatching,
        /// <summary>
        /// 「～とパターンが一致しない」を表す (～はパターン文字列) (= 19)<br />
        /// (FAで使用可)
        /// </summary>
        PatternUnmatching
    }

    /// <summary>
    /// 集計結果の各データが持つマーキング情報列挙型
    /// </summary>
    [Flags, ComVisible(true)]
    public enum DataMarking : int
    {
        /// <summary>
        /// 全体との比率の差の水準1高での色付けを表す (= 1)
        /// <note>
        /// 全体との比率の差の色付けグループ<br />
        /// 使用ビットイメージ　0000000000111
        /// </note>
        /// </summary>
        ColoringLevel1High = 1,
        /// <summary>
        /// 全体との比率の差の水準2高での色付けを表す (= 2)
        /// <note>
        /// 全体との比率の差の色付けグループ<br />
        /// 使用ビットイメージ　0000000000111
        /// </note>
        /// </summary>
        ColoringLevel2High = 2,
        /// <summary>
        /// 全体との比率の差の水準1低での色付けを表す (= 3)
        /// <note>
        /// 全体との比率の差の色付けグループ<br />
        /// 使用ビットイメージ　0000000000111
        /// </note>
        /// </summary>
        ColoringLevel1Low = 3,
        /// <summary>
        /// 全体との比率の差の水準2低での色付けを表す (= 4)
        /// <note>
        /// 全体との比率の差の色付けグループ<br />
        /// 使用ビットイメージ　0000000000111
        /// </note>
        /// </summary>
        ColoringLevel2Low = 4,
        /// <summary>
        /// 全体との比率の差の色付けグループ全体 (= 7)
        /// </summary>
        ColoringAllBit = 7,
        /// <summary>
        /// ランキング1位を表す (= 16)
        /// <note>
        /// ランキンググループ<br />
        /// 使用ビットイメージ　0000001110000
        /// </note>
        /// </summary>
        Ranking1 = 16,
        /// <summary>
        /// ランキング2位を表す (= 32)
        /// <note>
        /// ランキンググループ<br />
        /// 使用ビットイメージ　0000001110000
        /// </note>
        /// </summary>
        Ranking2 = 32,
        /// <summary>
        /// ランキング3位を表す (= 48)
        /// <note>
        /// ランキンググループ<br />
        /// 使用ビットイメージ　0000001110000
        /// </note>
        /// </summary>
        Ranking3 = 48,
        /// <summary>
        /// ランキング4位を表す (コメント用)
        /// <note>
        /// ランキンググループ<br />
        /// 使用ビットイメージ　0000001110000
        /// </note>
        /// </summary>
        Ranking4 = 64,
        /// <summary>
        /// ランキング5位を表す (コメント用)
        /// <note>
        /// ランキンググループ<br />
        /// 使用ビットイメージ　0000001110000
        /// </note>
        /// </summary>
        Ranking5 = 80,
        /// <summary>
        /// ランキンググループ全体 (= 112)
        /// </summary>
        RankingAllBit = 112,
        /// <summary>
        /// 昇降分析での筈(矢筈)にあたることを表す (= 128)
        /// <note>
        /// 昇降分析グループ<br />
        /// 使用ビットイメージ　0000110000000
        /// </note>
        /// </summary>
        AscendingStart = 128,
        /// <summary>
        /// 昇降分析での鏃(矢尻)にあたることを表す (= 256)
        /// <note>
        /// 昇降分析グループ<br />
        /// 使用ビットイメージ　0000110000000
        /// </note>
        /// </summary>
        AscendingEnd = 256,
        /// <summary>
        /// 昇降分析での箆(矢柄)にあたることを表す (= 384)
        /// </summary>
        AscendingBody = 384,
        /// <summary>
        /// 昇降分析グループ全体 (= 384)
        /// </summary>
        AscendingAllBit = 384,
        /// <summary>
        /// 有意差検定で1％水準で有意に高いことを表す (= 1024)
        /// <note>
        /// 有意差検定グループ<br />
        /// 使用ビットイメージ　1110000000000
        /// </note>
        /// </summary>
        SignificanceOneHigh = 1024,
        /// <summary>
        /// 有意差検定で5％水準で有意に高いことを表す (= 2048)
        /// <note>
        /// 有意差検定グループ<br />
        /// 使用ビットイメージ　1110000000000
        /// </note>
        /// </summary>
        SignificanceFiveHigh = 2048,
        /// <summary>
        /// 有意差検定で10％水準で有意に高いことを表す (= 3072)
        /// <note>
        /// 有意差検定グループ<br />
        /// 使用ビットイメージ　1110000000000
        /// </note>
        /// </summary>
        SignificanceTenHigh = 3072,
        /// <summary>
        /// 有意差検定で1％水準で有意に低いことを表す (= 4096)
        /// <note>
        /// 有意差検定グループ<br />
        /// 使用ビットイメージ　1110000000000
        /// </note>
        /// </summary>
        SignificanceOneLow = 4096,
        /// <summary>
        /// 有意差検定で5％水準で有意に低いことを表す (= 5120)
        /// <note>
        /// 有意差検定グループ<br />
        /// 使用ビットイメージ　1110000000000
        /// </note>
        /// </summary>
        SignificanceFiveLow = 5120,
        /// <summary>
        /// 有意差検定で10％水準で有意に低いことを表す (= 6144)
        /// <note>
        /// 有意差検定グループ<br />
        /// 使用ビットイメージ　1110000000000
        /// </note>
        /// </summary>
        SignificanceTenLow = 6144,
        /// <summary>
        /// 有意差検定グループ全体 (= 7168)
        /// </summary>
        SignificanceAllBit = 7168,
        /// <summary>
        /// ColoringAllBit | RankingAllBit | AscendingAllBit | SignificanceAllBit
        /// </summary>
        MarkingAllBit = 7671
    }
    #endregion
}
