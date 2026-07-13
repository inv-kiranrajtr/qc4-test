using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Macromill.QCWeb.DataProcess
{
    /// <summary>
    /// 選択肢または仮想選択肢群を扱うインターフェイス
    /// </summary>
    public interface INewQuestionSectors : IList<_INewQuestionSector>, ICollection<_INewQuestionSector>, IEnumerable<_INewQuestionSector>, IDisposable
    {
        /// <summary>
        /// 親のIDataProcessインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        IDataProcess ParentDataProcess { get; }
        /// <summary>
        /// 親のINewQuestionインターフェイスまたはINewVirtualQuestionインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        _INewQuestion ParentQuestion { get; }
        /// <summary>
        /// INewQuestionSectorインターフェイスまたはINewVirtualQuestionSectorインターフェイスの実装クラスのインスタンスを生成して、要素に追加するメソッド
        /// </summary>
        /// <param name="criteriadescription">条件文字列</param>
        /// <param name="isVirtual">仮想選択肢を追加する場合はtrue (省略可、既定値false)</param>
        /// <returns>生成したインスタンスへの参照</returns>
        _INewQuestionSector Add(string criteriadescription, bool isVirtual = false);
    }
    /// <summary>
    /// 選択肢または仮想選択肢を扱うインターフェイス
    /// </summary>
    public interface _INewQuestionSector : IDisposable
    {
        /// <summary>
        /// 親コレクションのINewQuestionSectorsインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        INewQuestionSectors ParentCollection { get; }
        /// <summary>
        /// 親のIDataProcessインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        IDataProcess ParentDataProcess { get; }
        /// <summary>
        /// 親のINewQuestionインターフェイスまたはINewVirtualQuestionインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        _INewQuestion ParentQuestion { get; }
        /// <summary>
        /// 選択肢または仮想選択肢の見なしデータタイプを表すDataType列挙型の値
        /// </summary>
        Tabulation.DataType DataType { get; set; }
        /// <summary>
        /// 選択肢または仮想選択肢の条件を表すINewQuestionSectorCriteriaインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        INewQuestionSectorCriteria Criteria { get; }
    }
    /// <summary>
    /// 選択肢を扱うインターフェイス
    /// </summary>
    public interface INewQuestionSector : _INewQuestionSector
    {
        /// <summary>
        /// インデックス
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 選択肢文
        /// </summary>
        string Description { get; set; }
    }
    /// <summary>
    /// 仮想選択肢を扱うインターフェイス
    /// </summary>
    public interface INewVirtualQuestionSector : _INewQuestionSector
    {
        /// <summary>
        /// エイリアス
        /// </summary>
        string Alias { get; set; }

        /// <summary>
        /// データ修正の修正方法
        /// </summary>
        EditMethod EditMethod { get; set; }

        /// <summary>
        /// データ修正の修正値タイプ
        /// </summary>
        ModifyDataEdit ModifyDataEdit { get; set; }

        int jointCategoryCount { get; set; }//by 191  for joint
        string Add1paramvalue { get; set; }//by 191  for Add1
        int Add3Exludesettings { get; set; }//by 191  for Add1
    }

    /// <summary>
    /// データ修正の修正方法
    /// </summary>
    public enum EditMethod
    {
        /// <summary>修正値を代入する</summary>
        SUBSTITUTION = 1,
        /// <summary>修正値を追加する</summary>
        APPEND = 2,
        /// <summary>修正値を除外する</summary>
        REMOVE = 3,
        // by 191  for join 
        JOIN
    }

    /// <summary>
    /// データ加工データ修正の修正値タイプ
    /// </summary>
    public enum ModifyDataEdit
    {
        /// <summary>非該当</summary>
        UNMATCH = 1,
        /// <summary>無回答</summary>
        DK = 2,
        /// <summary>カテゴリ</summary>
        CATEGORY = 3,
        /// <summary>アイテム</summary>
        ITEM = 4,
        /// <summary>フリー入力</summary>
        FREE = 5,
        // by 191  for join 
        JOIN
    }
}
