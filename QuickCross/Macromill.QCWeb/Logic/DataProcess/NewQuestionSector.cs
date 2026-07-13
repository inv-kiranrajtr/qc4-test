using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Macromill.QCWeb.DataProcess
{
    /// <summary>
    /// 選択肢または仮想選択肢群を扱うコレクションクラス
    /// </summary>
    public class NewQuestionSectors : List<_INewQuestionSector>, INewQuestionSectors
    {
        /// <summary>
        /// NewQuestionSectorクラス、NewVirtualQuestionSectorクラスのスーパークラス
        /// </summary>
        public class _NewQuestionSector : _INewQuestionSector
        {
            private NewQuestionSectors collection = null;
            private NewQuestionSectorCriteria criteria = null;

            /// <summary>
            /// コンストラクタ
            /// <note>
            /// <paramref name="criteriadescription"/>にはAND→&amp;、OR→|に置換したものを渡し、
            /// 条件値が文字列の場合に、それに含まれる次の文字はエスケープするものとする<br />
            /// また、かっこの中には、2つ以上の条件式と、条件式の数-1個の演算子が含まれるものとする<br />
            /// この形式にしたがわない文字列が渡された場合には、正常動作は保証されない
            /// <list type="table">
            /// <listheader>
            /// <term>文字</term>
            /// <description>エスケープ後</description>
            /// </listheader>
            /// <item>
            /// <term>&amp;</term>
            /// <description>\&amp;</description>
            /// </item>
            /// <item>
            /// <term>|</term>
            /// <description>\|</description>
            /// </item>
            /// <item>
            /// <term>(</term>
            /// <description>\(</description>
            /// </item>
            /// <item>
            /// <term>)</term>
            /// <description>\)</description>
            /// </item>
            /// <item>
            /// <term>\</term>
            /// <description>\\</description>
            /// </item>
            /// </list>
            /// また、アイテム名ではなくアイテムIDを指定し、演算子(&amp;または|)の前後には半角スペースを付与し、絞り込み条件の演算子(=など)の前後にはスペースがないものとする
            /// </note>
            /// </summary>
            /// <param name="collection">親コレクションへの参照</param>
            /// <param name="criteriadescription">条件文字列</param>
            protected _NewQuestionSector(NewQuestionSectors collection, string criteriadescription)
            {
                this.collection = collection;
                if (!string.IsNullOrEmpty(criteriadescription))
                {
                    criteria = new NewQuestionSectorCriteria(this, criteriadescription);
                }
            }

            /// <summary>
            /// 親コレクションのNewQuestionSectorsクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public INewQuestionSectors ParentCollection
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

            /// <summary>
            /// 親であるNewQuestionクラスまたはNewVirtualQuestionクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public _INewQuestion ParentQuestion
            {
                get
                {
                    return collection == null ? null : collection.ParentQuestion;
                }
            }

            private Tabulation.DataType dataType = Tabulation.DataType.NormalData;
            /// <summary>
            /// 選択肢(選択肢見立て)のデータの種類を表すDataType列挙型の値を取得/設定するプロパティ
            /// <note>仮想選択肢では、DataTypeプロパティの値にはNormalDataを設定(既定値)し、値はAliasプロパティで管理すること</note>
            /// </summary>
            public Tabulation.DataType DataType
            {
                get
                {
                    return dataType;
                }
                set
                {
                    if (this.GetType() == typeof(NewQuestionSectors.NewVirtualQuestionSector)) return;
                    Tabulation.DataType dType = value & ~Tabulation.DataType.DeletedData;
                    if (Enum.IsDefined(typeof(Tabulation.DataType), dType)) dataType = dType;
                }
            }

            /// <summary>
            /// 選択肢の条件を表すNewQuestionSectorCriteriaクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public INewQuestionSectorCriteria Criteria
            {
                get
                {
                    return criteria;
                }
            }

            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                if (criteria != null) criteria.Dispose();
                collection = null;
            }
        }

        /// <summary>
        /// 新アイテムの選択肢を表すクラス
        /// </summary>
        public class NewQuestionSector : _NewQuestionSector, INewQuestionSector
        {
            internal NewQuestionSector(NewQuestionSectors collection, string criteriadescription) : base(collection, criteriadescription) { }

            /// <summary>
            /// 1ベースのインデックス(選択肢番号)を返す読み取り専用プロパティ
            /// </summary>
            public int Index
            {
                get
                {
                    int n = 0;
                    for (int i = 0; i < ParentCollection.Count; ++i)
                    {
                        if (ParentCollection[i].DataType == Tabulation.DataType.NormalData) ++n;
                    }
                    return n;
                }
            }

            private string description = null;
            /// <summary>
            /// 選択肢文を取得/設定するプロパティ
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
        }

        /// <summary>
        /// 新仮想アイテムの仮想選択肢を表すクラス
        /// </summary>
        public class NewVirtualQuestionSector : _NewQuestionSector, INewVirtualQuestionSector
        {
            internal NewVirtualQuestionSector(NewQuestionSectors collection, string criteriadescription) : base(collection, criteriadescription) { }

            private string alias = null;
            private EditMethod editMethod;
            private ModifyDataEdit modifyDataEdit;
            private int jointcategorycount = 0;
            private string add1paramvalue = string.Empty;
            private int add3excludesetting = 0;
            /// <summary>
            /// 仮想選択肢に割り当てる値を取得/設定するプロパティ
            /// </summary>
            public string Alias
            {
                get
                {
                    return alias;
                }
                set
                {
                    alias = value;
                }
            }

            /// <summary>
            /// データ加工(データ修正)の修正方法を取得/設定するプロパティ
            /// </summary>
            public EditMethod EditMethod
            {
                get
                {
                    return editMethod;
                }
                set
                {
                    editMethod = value;
                }
            }

            /// <summary>
            /// データ加工(データ修正)の修正値タイプを取得/設定するプロパティ
            /// </summary>
            public ModifyDataEdit ModifyDataEdit
            {
                get
                {
                    return modifyDataEdit;
                }
                set
                {
                    modifyDataEdit = value;
                }
            }
            public int jointCategoryCount
            {
                get
                {
                    return jointcategorycount;
                }
                set
                {
                    jointcategorycount = value;
                }
            }

            // string  Add1paramvalue { get; set; }
            public string Add1paramvalue
            {
                get
                {
                    return add1paramvalue;
                }
                set
                {
                    add1paramvalue = value;
                }
            }
            // int Add3Exludesettings { get; set; }//by 191  for Add1
            public int Add3Exludesettings
            {
                get
                {
                    return add3excludesetting;
                }
                set
                {
                    add3excludesetting = value;
                }
            }
        }

        private NewQuestions._NewQuestion parentQuestion = null;

        internal NewQuestionSectors(NewQuestions._NewQuestion parentQuestion)
        {
            this.parentQuestion = parentQuestion;
        }

        /// <summary>
        /// 親であるDataProcessクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IDataProcess ParentDataProcess
        {
            get
            {
                return parentQuestion == null ? null : parentQuestion.ParentDataProcess;
            }
        }

        /// <summary>
        /// 親であるNewQuestionクラスまたはNewVirtualQuestionクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public _INewQuestion ParentQuestion
        {
            get
            {
                return parentQuestion;
            }
        }

        /// <summary>
        /// NewQuestionSectorクラスまたはNewVirtualQuestionSectorクラスのインスタンスを生成して、要素に追加するメソッド
        /// <note>
        /// <paramref name="criteriadescription"/>にはAND→&amp;、OR→|に置換したものを渡し、
        /// 条件値が文字列の場合に、それに含まれる次の文字はエスケープするものとする<br />
        /// また、かっこの中には、2つ以上の条件式と、条件式の数-1個の演算子が含まれるものとする<br />
        /// この形式にしたがわない文字列が渡された場合には、正常動作は保証されない
        /// <list type="table">
        /// <listheader>
        /// <term>文字</term>
        /// <description>エスケープ後</description>
        /// </listheader>
        /// <item>
        /// <term>&amp;</term>
        /// <description>\&amp;</description>
        /// </item>
        /// <item>
        /// <term>|</term>
        /// <description>\|</description>
        /// </item>
        /// <item>
        /// <term>(</term>
        /// <description>\(</description>
        /// </item>
        /// <item>
        /// <term>)</term>
        /// <description>\)</description>
        /// </item>
        /// <item>
        /// <term>\</term>
        /// <description>\\</description>
        /// </item>
        /// </list>
        /// また、アイテム名ではなくアイテムIDを指定し、演算子(&amp;または|)の前後には半角スペースを付与し、条件の演算子(=など)の前後にはスペースがないものとする
        /// </note>
        /// </summary>
        /// <param name="criteriadescription">条件文字列</param>
        /// <param name="isVirtual">仮想選択肢を追加する場合はtrue (省略可、既定値false)</param>
        /// <returns>生成したインスタンスへの参照</returns>
        public _INewQuestionSector Add(string criteriadescription, bool isVirtual = false)
        {
            if (parentQuestion == null) return null;
            _INewQuestionSector newItem = null;
            if (isVirtual)
            {
                newItem = new NewVirtualQuestionSector(this, criteriadescription);
            }
            else
            {
                newItem = new NewQuestionSector(this, criteriadescription);
            }
            this.Add(newItem);
            return newItem;
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (_INewQuestionSector sector in this)
            {
                sector.Dispose();
            }
            parentQuestion = null;
        }
    }
}
