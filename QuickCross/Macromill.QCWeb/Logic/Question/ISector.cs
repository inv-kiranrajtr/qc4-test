#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：ISector.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/4/2
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/2
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Question
{
    /// <summary>
    /// 選択肢コレクション情報を扱うインターフェイス
    /// </summary>
    [ComVisible(true), Guid("0A8396FF-8DA6-4901-A735-D689F7309B6B")]
    public interface ISectors : IDisposable, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
    {
        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="number">選択肢番号</param>
        /// <returns>選択肢番号が示す選択肢情報を保持したISectorインターフェイスの実装クラスのインスタンスへの参照</returns>
        ISector this[int number] { get; }

        /// <summary>
        /// 親質問を表すIQuestionインターフェイスの実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IQuestion ParentQuestion { get; }

        /// <summary>
        /// 集計結果を元にISectorクラスのインスタンスへの参照を並べ替えた結果のListオブジェクトへの参照を返すメソッド
        /// </summary>
        /// <param name="sectorNValues">選択肢番号順にN値を格納した配列</param>
        /// <returns>並べ替えた結果のListクラスのインスタンスへの参照</returns>
        [ComVisible(false)]
        List<ISector> SortedSectors(double[] sectorNValues);

        /// <summary>
        /// 集計結果を元にISectorクラスのインスタンスへの参照を並べ替えた結果のListオブジェクトへの参照を返すメソッド
        /// </summary>
        /// <param name="sectorNValues">選択肢番号順にN値を格納した配列</param>
        /// <returns>並べ替えた結果のListクラスのインスタンスへの参照</returns>
        [ComVisible(false)]
        List<ISector> SortedSectors(int[] sectorNValues);
    }

    /// <summary>
    /// 選択肢情報を扱うインターフェイス
    /// </summary>
    [ComVisible(true), Guid("3BEC3FE1-E83E-485C-BC55-2AC6E3DFE3E3")]
    public interface ISector : IDisposable, IComparable
    {
        /// <summary>
        /// 選択肢番号を返す読み取り専用プロパティ
        /// </summary>
        int Number { get; }

        /// <summary>
        /// 選択肢文を返す読み取り専用プロパティ
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 既定の選択肢文を返す読み取り専用プロパティ
        /// </summary>
        string OriginalDescription { get; }

        /// <summary>
        /// ウエイト値の文字列表現を返す読み取り専用プロパティ
        /// </summary>
        string Weight { get; }

        /// <summary>
        /// 並べ替えの対象外であることを示すフラグを返す読み取り専用プロパティ
        /// </summary>
        bool IsUnsort { get; }

        /// <summary>
        /// 付加質問を持つかどうかを返す読み取り専用プロパティ
        /// </summary>
        bool HasAddedQuestion { get; }

        /// <summary>
        /// 付加質問を表すIQuestionインターフェイスの実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IQuestion AddedQuestion { get; }

        /// <summary>
        /// 実装クラスのインスタンスが格納されているISectorsインターフェイスの実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        ISectors ParentCollection { get; }

        /// <summary>
        /// 実装クラスのインスタンスの親であるIQuestionインターフェイスの実装クラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        IQuestion ParentQuestion { get; }
    }
}
