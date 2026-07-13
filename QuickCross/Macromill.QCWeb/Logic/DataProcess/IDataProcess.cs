using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.DataProcess
{
    /// <summary>
    /// データ加工の種類を表すコード
    /// </summary>
    [ComVisible(false)]
    public enum DataProcessCode
    {
        /// <summary>
        /// INTEGRATE (= 0)
        /// </summary>
        Integrate,
        /// <summary>
        /// RECODE (= 1)
        /// </summary>
        Recode,
        /// <summary>
        /// MCONVERT (= 2)
        /// </summary>
        MConvert,
        /// <summary>
        /// CLASS (= 3)
        /// </summary>
        Class,
        /// <summary>
        /// COUNT (SA) (= 4)
        /// </summary>
        CategorizeResponseCount,
        /// <summary>
        /// MTOS (= 5)
        /// </summary>
        MtoS,
        /// <summary>
        /// データ削除 (= 6)
        /// </summary>
        DeleteData,
        /// <summary>
        /// データ修正 (= 7)
        /// </summary>
        ModifyData,
        /// <summary>
        /// COUNT (N) (= 8)
        /// </summary>
        ResponseCount,
        /// <summary>
        /// COMPUTE (= 9)
        /// </summary>
        Compute,
        /// <summary>
        /// GROUP (= 10)
        /// </summary>
        Group,
        /// <summary>
        /// ウエイトバック設定 (= 11)
        /// </summary>
        SetWeightBack
    }

    /// <summary>
    /// データ加工情報を扱うインターフェイス
    /// </summary>
    [ComVisible(false)]
    public interface IDataProcess : IDisposable
    {
        /// <summary>
        /// データ加工の種類を表すDataProcessCode列挙型の値
        /// </summary>
        DataProcessCode DataProcessCode { get; }
        /// <summary>
        /// データ加工の実行のオン/オフ
        /// </summary>
        bool RunFlag { get; set; }
        /// <summary>
        /// 新アイテムまたは新仮想アイテム群を表すINewQuestionsインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        INewQuestions Questions { get; }
        /// <summary>
        /// 説明
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 親のDataProcessesクラスのインスタンスへの参照
        /// </summary>
        DataProcesses ParentCollection { get; }
        /// <summary>
        /// DB登録を行うメソッド
        /// </summary>
        void Regist();
        /// <summary>
        /// データ加工を実行するメソッド
        /// </summary>
        void Execute();
        /// <summary>
        /// DB登録の直後にデータ加工を実行するメソッド
        /// </summary>
        void RegistAndExecute();

        /// <summary>
        /// 
        /// </summary>
        bool ReverseIsTrue{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsTreatasZero { get; set; }

    }
}
