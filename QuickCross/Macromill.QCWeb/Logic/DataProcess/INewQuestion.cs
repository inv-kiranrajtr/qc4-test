using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Tabulation;

namespace Macromill.QCWeb.DataProcess
{
    /// <summary>
    /// 新アイテムまたは新仮想アイテム群を扱うインターフェイス
    /// </summary>
    public interface INewQuestions : IList<_INewQuestion>, ICollection<_INewQuestion>, IEnumerable<_INewQuestion>, IDisposable
    {
        /// <summary>
        /// 親のIDataProcessインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        IDataProcess ParentDataProcess { get; }
        /// <summary>
        /// INewQuestionインターフェイスまたはINewVirtualQuestionインターフェイスの実装クラスのインスタンスを生成して、要素に追加するメソッド
        /// </summary>
        /// <param name="isVirtual">仮想アイテムを追加する場合はtrue (省略可、既定値false)</param>
        /// <returns>生成したインスタンスへの参照</returns>
        _INewQuestion Add(bool isVirtual = false);
    }
    /// <summary>
    /// 新アイテムまたは新仮想アイテムを扱うインターフェイス
    /// </summary>
    public interface _INewQuestion : IDisposable
    {
        /// <summary>
        /// 親コレクションのINewQuestionsインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        INewQuestions ParentCollection { get; }
        /// <summary>
        /// 親のIDataProcessインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        IDataProcess ParentDataProcess { get; }
        /// <summary>
        /// アイテムIDまたは仮想アイテムID
        /// </summary>
        string ItemId { get; set; }
        /// <summary>
        /// アイテム名または仮想アイテム名
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 質問タイプを表すQuestionType列挙型の値
        /// </summary>
        Tabulation.QuestionType QuestionType { get; set; }
        /// <summary>
        /// 選択肢または仮想選択肢群を表すINewQuestionSectorsインターフェイスの実装クラスのインスタンスへの参照
        /// </summary>
        INewQuestionSectors Sectors { get; }
        /// <summary>
        /// 加工元アイテムIDまたは仮想加工元アイテムID
        /// RECODEおよびCLASSでのみ利用
        /// </summary>
        string SourceItemId { get; set; }

        QuestionType SourceQuestionType { get; set; }

        int CategoryCount { get; set; }//by 191  for MTOS 
    }
    /// <summary>
    /// 新アイテムを扱うインターフェイス
    /// </summary>
    public interface INewQuestion : _INewQuestion
    {
        /// <summary>
        /// 質問文
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 式
        /// </summary>
        string FormulaString { get; set; }
        /// <summary>
        /// 「全てが非該当だった場合結果は非該当」チェックボックス
        /// INTEGRATEおよびMCONVERT用プロパティ
        /// </summary>
        bool UnfitFlag { get; set; }
        /// <summary>
        /// MtoS用処理方法
        /// </summary>
        Common.GlobalsCommonConstant.MtoS_SelectMethod SelectedMethod { get; set; }

        /// <summary>
        /// COUNT用新カテゴリの個数範囲を表すリスト
        /// </summary>
        List<List<Tabulation.NData.ValueRange>> CountSectorRange { get; set; } //QC4: Changed List<Tabulation.NData.ValueRange> to List<List> to support non range values
        /// <summary>
        /// 新アイテムファイルの拡張子を.txtにする場合はGlobalsCommonConstant.fileExtension.txt、
        /// .dpの場合はGlobalsCommonConstant.fileExtension.dp
        /// .tmpの場合はGlobalsCommonConstant.fileExtension.tmp
        /// 既定値GlobalsCommonConstant.fileExtension.dp
        /// </summary>
        GlobalsCommonConstant.fileExtension ChangeExtension { get; set; }
    }
    /// <summary>
    /// 新仮想アイテムを扱うインターフェイス
    /// </summary>
    public interface INewVirtualQuestion : _INewQuestion
    {
    }
}
