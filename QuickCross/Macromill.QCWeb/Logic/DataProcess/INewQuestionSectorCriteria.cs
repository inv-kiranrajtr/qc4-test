using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Macromill.QCWeb.DataProcess
{
    /// <summary>
    /// 選択肢または仮想選択肢の条件を扱うインターフェイス
    /// </summary>
    public interface INewQuestionSectorCriteria : Tabulation.ICriteria
    {
        /// <summary>
        /// ローデータのデータタイプを取得するメソッド
        /// 条件アイテムが一つの場合のみに有効。RECODEで利用している
        /// </summary>
        /// <param name="index">データ(レコード)のインデックス</param>
        /// <returns>データ種別列挙型</returns>
        Tabulation.DataType GetDataType(int index);
        /// <summary>
        /// データが条件を満たすかどうかを返すメソッド
        /// </summary>
        /// <param name="index">データ(レコード)のインデックス</param>
        /// <returns><paramref name="index"/>番目のデータが条件を満たす場合true、満たさない場合false</returns>
        bool IsTrue(int index);
        /// <summary>
        /// 親のIDataProcessインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        IDataProcess ParentDataProcess { get; }
        /// <summary>
        /// 親のINewQuestionインターフェイスまたはINewVirtualQuestionインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        _INewQuestion ParentQuestion { get; }
        /// <summary>
        /// 親のINewQuestionSectorインターフェイスまたはINewVirtualQuestionSectorインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        _INewQuestionSector ParentSector { get; }

        /// <summary>
        /// 回答個数を取得する。
        /// </summary>
        /// <param name="index">データ(レコード)のインデックス</param>
        /// <returns>回答個数(数字)または“*”(非該当)</returns>
        /// <remarks>
        ///  データが「DK：無回答」の場合、0としてカウントされる。但し、条件に「DK：無回答」が含まれている場合はカウントする						
        ///  データが「*：非該当」はカウント対象外とし、結果は必ず「*：非該当」とする。但し、条件値に「*：非該当」が入っている場合はカウント対象(=1)とする						
        ///  上記以外のデータは、「=」条件のみの基本ルールに従う						
        ///  [SA]どの新カテゴリ範囲にも合致しないデータは「DK：無回答」にする						
        ///  新回答タイプSA時の範囲指定「～n」でも「DK：無回答」は0とみなし合致させる						
        /// </remarks>
        string GetCountResult(int index);

        /// <summary>
        /// MtoS用BinValue文字列を作成する。
        /// </summary>
        /// <param name="index">データ(レコード)のインデックス</param>
        /// <returns>
        ///「DK：無回答」のデータは、必ず「DK：無回答」が設定される
        ///「*：非該当」のデータは必ず「*：非該当」が設定される
        ///　新カテゴリ範囲外のデータしか存在しない場合は「DK：無回答」になる 
        /// </returns>
        string GetMtoSResultBinValue(int index);

    }
}
