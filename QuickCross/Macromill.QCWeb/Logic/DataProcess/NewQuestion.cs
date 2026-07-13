using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Tabulation;

namespace Macromill.QCWeb.DataProcess
{
    /// <summary>
    /// 新アイテムまたは新仮想アイテム群を扱うコレクションクラス
    /// </summary>
    public class NewQuestions : List<_INewQuestion>, INewQuestions
    {
        /// <summary>
        /// NewQuestionクラス、NewVirtualQuestionクラスのスーパークラス
        /// </summary>
        public class _NewQuestion : _INewQuestion
        {
            private NewQuestions collection = null;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="collection">親コレクションへの参照</param>
            protected _NewQuestion(NewQuestions collection)
            {
                this.collection = collection;
            }

            /// <summary>
            /// 親コレクションのNewQuestionsクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public INewQuestions ParentCollection
            {
                get
                {
                    return collection;
                }
            }

            /// <summary>
            /// 親であるDataProcessクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IDataProcess ParentDataProcess
            {
                get 
                {
                    return collection == null ? null : collection.ParentDataProcess;
                }
            }

            private string itemid = null;
            /// <summary>
            /// 新アイテムIDを取得/設定するプロパティ
            /// <note>設定は一回のみ可能<br />数値以外については制御する<br />ファイル名を生成するために利用</note>
            /// </summary>
            public string ItemId
            {
                get
                {
                    return itemid;
                }
                set
                {
                    if (itemid != null) return;
                    decimal id;
                    if (decimal.TryParse(value, out id)) itemid = value;
                }
            }

            private string name = null;
            /// <summary>
            /// 新アイテム名または仮想名を取得/設定するプロパティ
            /// <note>設定は1回のみ可能<br />禁則文字については制御する<br />予約語については制御しない</note>
            /// </summary>
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    /*
                     * ・25文字以内 (byteではなく文字数)
                     * ・数字で始まらない
                     * ・記号 !&'=~|\@[]`*:<>/? は使用不可。"^-#$%(){};_,. は使用可。
                     * "※~は、Excelで検索時のエスケープ文字。加工アイテム名に~を入れると、
                     * 1回目の登録はできるが、2回目の登録で実行時エラーとなる。
                     * 半角空白は？ →
                    */
                    if (name != null) return;
                    string prohibitletters = @"\!&'=~\|\\@\[\]`\*:<>/\?\s";
                    // ^[^\!&'=~\|\\@\[\]`\*:<>/\?\s\d][^\!&'=~\|\\@\[\]`\*:<>/\?\s]{0,24}$
                    string ptn = "^[^" + prohibitletters + @"\d][^" + prohibitletters + "]{0,24}$";
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ptn);
                    if (regex.IsMatch(value)) name = value;
                }
            }

            private Tabulation.QuestionType questionType = (Tabulation.QuestionType)0;
            private NewQuestionSectors sectors = null;
            /// <summary>
            /// 質問タイプを表すQuestionType列挙型の値を取得/設定するプロパティ
            /// </summary>
            public Tabulation.QuestionType QuestionType
            {
                get
                {
                    return questionType;
                }
                set
                {
                    Tabulation.QuestionType qType = value & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.N | Tabulation.QuestionType.FA);
                    if (Enum.IsDefined(typeof(Tabulation.QuestionType), qType)) questionType = qType;
                    switch (qType)
                    {
                        case Tabulation.QuestionType.SA:
                        case Tabulation.QuestionType.MA:
                        case Tabulation.QuestionType.N:         //新アイテムのタイプがNで元アイテムの条件値を格納するため、追加
                            sectors = new NewQuestionSectors(this);
                            break;
                        default:
                            if (sectors != null) sectors.Dispose();
                            sectors = null;
                            break;
                    }
                }
            }

            /// <summary>
            /// 選択肢または仮想選択肢群を表すNewQuestionSectorsクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// <note>QuestionTypeプロパティの値がSAまたはMAでなければnullが返される</note>
            /// </summary>
            public INewQuestionSectors Sectors
            {
                get 
                {
                    return sectors;
                }
            }

            /// <summary>
            /// 加工元アイテムIDまたは仮想加工元アイテムIDの値を取得/設定するプロパティ
            /// RECODEおよびCLASSでのみ利用
            /// </summary>
            public string SourceItemId { get; set; }
            public QuestionType SourceQuestionType { get; set; }
            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                if (sectors != null) sectors.Dispose();
                collection = null;
            }
          public int  CategoryCount { get; set; }
    }

        /// <summary>
        /// 新アイテムを表すクラス
        /// </summary>
        public class NewQuestion : _NewQuestion, INewQuestion
        {
            internal NewQuestion(NewQuestions collection) : base(collection) { }

            private string description = null;
            /// <summary>
            /// 質問文を取得/設定するプロパティ
            /// </summary>
            public string Description
            {
                get
                {
                    return description;
                }
                set
                {
                    description = value;
                }
            }

            private string formulastring = null;
            /// <summary>
            /// 式を取得/設定するプロパティ
            /// </summary>
            public string FormulaString
            {
                get
                {
                    return formulastring;
                }
                set
                {
                    formulastring = value;
                }
            }

            private bool unfitFlag = false;
            /// <summary>
            /// 「全てが非該当だった場合結果は非該当」チェックボックス
            /// INTEGRATEおよびMCONVERT用プロパティ
            /// </summary>
            public bool UnfitFlag
            {
                get
                {
                    return unfitFlag;
                }
                set
                {
                    unfitFlag = value;
                }
            }
            /// <summary>
            /// MtoS用処理方法
            /// </summary>
            public Common.GlobalsCommonConstant.MtoS_SelectMethod SelectedMethod { get; set; }

            /// <summary>
            /// COUNT用新カテゴリの個数範囲を表すリスト
            /// </summary>
            public List<List<Tabulation.NData.ValueRange>> CountSectorRange { get; set; } //QC4: Changed List<Tabulation.NData.ValueRange> to List<List> to support non range values

            private GlobalsCommonConstant.fileExtension changeExtension = GlobalsCommonConstant.fileExtension.dp;
            /// <summary>
            /// 新アイテムファイルの拡張子を.txtにする場合はGlobalsCommonConstant.fileExtension.txt、
            /// .dpの場合はGlobalsCommonConstant.fileExtension.dp
            /// .tmpの場合はGlobalsCommonConstant.fileExtension.tmp
            /// 既定値GlobalsCommonConstant.fileExtension.dp
            /// </summary>
            public GlobalsCommonConstant.fileExtension ChangeExtension
            {
                get
                {
                    return changeExtension;
                }
                set
                {
                    changeExtension = value;
                }
            }
        }

        /// <summary>
        /// 新仮想アイテムを表すクラス
        /// </summary>
        public class NewVirtualQuestion : _NewQuestion, INewVirtualQuestion
        {
            internal NewVirtualQuestion(NewQuestions collection) : base(collection) { }
        }

        private DataProcesses.DataProcess parentDataProcess = null;
        internal NewQuestions(DataProcesses.DataProcess parentDataProcess)
        {
            this.parentDataProcess = parentDataProcess;
        }

        /// <summary>
        /// 親であるDataProcessクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IDataProcess ParentDataProcess
        {
            get 
            {
                return parentDataProcess as IDataProcess;
            }
        }

        /// <summary>
        /// NewQuestionクラスまたはNewVirtualQuestionクラスのインスタンスを生成して、要素に追加するメソッド
        /// </summary>
        /// <param name="isVirtual">仮想アイテムを追加する場合はtrue (省略可、既定値false)</param>
        /// <returns>生成したインスタンスへの参照</returns>
        public _INewQuestion Add(bool isVirtual = false)
        {
            if (parentDataProcess == null) return null;
            if (this.Count == 1 &&
              (parentDataProcess.DataProcessCode != DataProcessCode.Recode &&
               parentDataProcess.DataProcessCode != DataProcessCode.ModifyData &&
               parentDataProcess.DataProcessCode != DataProcessCode.DeleteData)) {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.RegistMultiNewItemsFailedMessageIndex));
            }
            _NewQuestion newItem = null;
            if (isVirtual)
            {
                newItem = new NewVirtualQuestion(this);
            }
            else
            {
                newItem = new NewQuestion(this);
            }
            this.Add(newItem);
            return newItem;
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (_INewQuestion question in this)
            {
                question.Dispose();
            }
            parentDataProcess = null;
        }
    }
}
