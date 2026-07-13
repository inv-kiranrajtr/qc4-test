#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：IQuestion.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/4/2
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/5
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Question
{
    /// <summary>
    /// QC3での質問タイプを表すコード
    /// </summary>
    [ComVisible(true), Flags]
    public enum QCQuestionType : int
    {
        /// <summary>
        /// なし (= 0)
        /// </summary>
        None,
        /// <summary>
        /// SAR (= 1)
        /// </summary>
        SAR,
        /// <summary>
        /// SAS (= 2)
        /// </summary>
        SAS,
        /// <summary>
        /// SAP (= 3)
        /// </summary>
        SAP,
        /// <summary>
        /// MAC (= 4)
        /// </summary>
        MAC,
        /// <summary>
        /// MTS (= 5)
        /// </summary>
        MTS,
        /// <summary>
        /// MTM (= 6)
        /// </summary>
        MTM,
        /// <summary>
        /// MTT (= 7)
        /// </summary>
        MTT,
        /// <summary>
        /// RNK (= 8)
        /// </summary>
        RNK,
        /// <summary>
        /// RAT (= 9)
        /// </summary>
        RAT,
        /// <summary>
        /// FAS (= 10)
        /// </summary>
        FAS,
        /// <summary>
        /// FAL (= 11)
        /// </summary>
        FAL,
        /// <summary>
        /// QC3で言う質問タイプ(SARなど)で使用するすべてのビット
        /// </summary>
        QuestionTypeAllBit = 15,
        /// <summary>
        /// 通常質問 (= 0)
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 新質問 (= 64)
        /// </summary>
        NewItem = 64,
        /// <summary>
        /// 多変量解析 (= 128)
        /// </summary>
        Analysis = 128,
        /// <summary>
        /// 一時作成(ちょっと加工) (= 192)
        /// </summary>
        Temporary = 192,
        /// <summary>
        /// ロード時QC3で新アイテムだったもの (= 256)
        /// </summary>
        NewAtQC3 = 256,
        /// <summary>
        /// ロード時QC3で多変量解析アイテムだったもの (= 320)
        /// </summary>
        AnaAtQC3 = 320,
        /// <summary>
        /// 質問の種類で使用するすべてのビット
        /// </summary>
        QuestionTypeExAllBit = 448
    }

    /// <summary>
    /// QC3での回答タイプを表すコード
    /// </summary>
    [ComVisible(true), Flags]
    public enum QCAnswerType : int
    {
        /// <summary>
        /// SA
        /// </summary>
        SA = 1,
        /// <summary>
        /// MA
        /// </summary>
        MA,
        /// <summary>
        /// N
        /// </summary>
        N,
        /// <summary>
        /// FA
        /// </summary>
        FA,
        /// <summary>
        /// D
        /// </summary>
        D
    }

    /// <summary>
    /// DB上マトリクスコード
    /// </summary>
    [ComVisible(true)]
    public enum QCMatrixCode : int
    {
        /// <summary>
        /// 通常質問
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 親マトリクス
        /// </summary>
        MatrixParent = 1,
        /// <summary>
        /// 子マトリックス（通常子アイテム）
        /// </summary>
        MatrixChild = 2,
        /// <summary>
        /// 子アイテム（付加FA）
        /// </summary>
        SubFA = 3,
        /// <summary>
        /// 子マトリックス（親作成元アイテム）
        /// </summary>
        FirstChild = 4
    }

    /// <summary>
    /// 質問コレクション情報を扱うインターフェイス
    /// </summary>
    [ComVisible(true), Guid("84EC8901-B2F3-46BA-A5AF-1B9B3F2D3318")]
    public interface IQuestions : IDisposable, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
    {
        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="id">質問ID</param>
        /// <returns>質問IDが示す質問情報を保持したIQuestionインターフェイスの実装クラスのインスタンスへの参照</returns>
        [ComVisible(false)]
        IQuestion this[decimal id] { get; }

        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="index">質問の1ベースインデックス</param>
        /// <returns>インデックスが示すIQuestionインターフェイスの実装クラスのインスタンスへの参照</returns>
        IQuestion this[int index] { get; }

        /// <summary>
        /// コレクション内にnameに指定したアイテム名を持つインスタンスがあるかどうかを、サブコレクション内まで考慮して返すメソッド
        /// </summary>
        /// <param name="name">アイテム名</param>
        /// <param name="id">
        /// シナリオID（省略可、既定値null）
        /// GT集計設定追加、カテゴリ出力編集で作成されたアイテムを検索するのに利用する
        /// </param>
        /// <param name="ignoreCase">大文字小文字を区別しない場合true (省略可、既定値true)</param>
        /// <param name="ignoreByte">全角半角を区別しない場合true (省略可、既定値true)</param>
        /// <returns>ある場合true、ない場合false</returns>
        bool Contains(string name, decimal? id = null, bool ignoreCase = true, bool ignoreByte = true);

        /// <summary>
        /// 親質問を表すIQuestionインターフェイスの実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// <note>マトリクス子質問コレクションの場合にのみ有効</note>
        /// </summary>
        IQuestion ParentQuestion { get; }
    }

    /// <summary>
    /// 質問情報を扱うインターフェイス
    /// </summary>
    [ComVisible(true), Guid("1B03DE28-5F04-411B-9D73-F64EE16DA766")]
    public interface IQuestion : IDisposable
    {
        /// <summary>
        /// 質問IDを返す読み取り専用プロパティ
        /// </summary>
        [ComVisible(false)]
        decimal ID { get; }

        /// <summary>
        /// 質問のインデックスを返す読み取り専用プロパティ
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 質問番号を返す読み取り専用プロパティ
        /// </summary>
        string Number { get; }

        /// <summary>
        /// アイテム名を返す読み取り専用プロパティ
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 質問文を返す読み取り専用プロパティ
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 既定の質問文を返す読み取り専用プロパティ
        /// </summary>
        string OriginalDescription { get; }

        /// <summary>
        /// 質問に紐付く選択肢コレクションを表すISectorsインターフェイスの実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// <note>SAまたはMA質問の場合に有効</note>
        /// </summary>
        ISectors Sectors { get; }

        /// <summary>
        /// 集計結果を並べ替えるかどうかを示すフラグを返す読み取り専用プロパティ
        /// </summary>
        bool DoSort { get; }

        /// <summary>
        /// QC3での質問タイプを表すQCQuestionType列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        QCQuestionType QCQuestionType { get; }

        /// <summary>
        /// データ加工または多変量解析による新アイテムかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool IsNewItem { get; }

        /// <summary>
        /// ちょっと加工の一時アイテムかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool IsTemporatyItem { get; }

        /// <summary>
        /// 既存の通常アイテムかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool IsNormalItem { get; }

        /// <summary>
        /// QC3での回答タイプを表すQCAnswerType列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        QCAnswerType QCAnswerType { get; }

        /// <summary>
        /// QCWebでの集計に適した質問タイプを表すQuestionType列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        Tabulation.QuestionType QuestionType { get; }

        /// <summary>
        /// 子質問コレクションを表すIQuestionsインターフェイスの実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// <note>マトリクスの親質問の場合にのみ有効</note>
        /// </summary>
        IQuestions ChildQuestions { get; }

        /// <summary>
        /// ローデータ情報が入っているテーブル名を返す読み取り専用プロパティ
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// ローデータ情報が入っているカラム名を返す読み取り専用プロパティ
        /// </summary>
        string ColumnName { get; }

        /// <summary>
        /// 同一調査の先頭のローデータテーブル名を返す読み取り専用プロパティ
        /// </summary>
        string TopTableName { get; }

        /// <summary>
        /// 実装クラスのインスタンスの親であるIQuestionインターフェイスの実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// <note>マトリクスの子質問の場合および付加質問の場合にのみ有効</note>
        /// </summary>
        IQuestion ParentQuestion { get; }

        /// <summary>
        /// 実装クラスのインスタンスの親であるISectorインターフェイスの実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// <note>付加質問の場合にのみ有効</note>
        /// </summary>
        ISector ParentSector { get; }

        /// <summary>
        /// 実装クラスのインスタンスが格納されているIQuestionsインターフェイスの実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IQuestions ParentCollection { get; }

        /// <summary>
        /// 最終更新日を返す読み取り専用プロパティ
        /// </summary>
        DateTime LastUpdateDateTime { get; }
    }
}
