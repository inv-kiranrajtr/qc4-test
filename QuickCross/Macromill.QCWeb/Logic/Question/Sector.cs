#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Sector.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/4/2
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/5
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Question
{
    /// <summary>
    /// 選択肢情報コレクションクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("5B04789A-1E44-46B5-9CCC-0E98053C7243")]
    public class Sectors : Hashtable, ISectors
    {
        /// <summary>
        /// 選択肢情報クラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("E62D41CB-315C-4E1C-B979-7D7F7ABCAA50")]
        public class Sector : ISector
        {
            private Sectors Collection = null;
            private int number = 0;
            private string description = null;
            private string originaldescription = null;
            private string weight = null;
            private bool isunsort = false;
            private Questions.Question addedquestion = null;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="sectors">追加する選択肢情報クラスのインスタンス</param>
            /// <param name="number">選択肢番号</param>
            protected internal Sector(Sectors sectors, int number)
            {
                Collection = sectors;
                this.number = number;
            }

            /// <summary>
            /// 選択肢番号を返す読み取り専用プロパティ
            /// </summary>
            public int Number
            {
                get 
                {
                    return number;
                }
            }

            /// <summary>
            /// 選択肢文を取得/設定するプロパティ<br />
            /// 設定は1度のみ可能
            /// </summary>
            public string Description
            {
                get 
                {
                    return description;
                }
                set
                {
                    if (description == null) description = value;
                }
            }

            /// <summary>
            /// 既定の選択肢文を取得/設定するプロパティ<br />
            /// 設定は1度のみ可能
            /// </summary>
            public string OriginalDescription
            {
                get 
                {
                    return originaldescription;
                }
                set
                {
                    if (originaldescription == null) originaldescription = value;
                }
            }

            /// <summary>
            /// ウエイト値の文字列表現を取得/設定するプロパティ
            /// </summary>
            public string Weight
            {
                get 
                {
                    return weight;
                }
                set
                {
                    double wt = 0.0;
                    if (double.TryParse(value, out wt))
                    {
                        weight = wt.ToString();
                    }
                    else
                    {
                        weight = null;
                    }
                }
            }

            /// <summary>
            /// 並べ替えの対象外であることを示すフラグを取得/設定するプロパティ
            /// </summary>
            public bool IsUnsort
            {
                get 
                {
                    return isunsort;
                }
                set
                {
                    isunsort = value;
                }
            }

            /// <summary>
            /// 付加質問を持つかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool HasAddedQuestion
            {
                get 
                {
                    return addedquestion != null;
                }
            }

            /// <summary>
            /// 付加質問を表すQuestionクラスのインスタンスへの参照を取得/設定するプロパティ<br />
            /// 設定は1度のみ可能
            /// </summary>
            public IQuestion AddedQuestion
            {
                get 
                {
                    return addedquestion;
                }
                set
                {
                    if (addedquestion == null)
                    {
                        if (value == null) return;
                        addedquestion = value as Questions.Question;
                        (value as Questions.Question).ParentSector = this;
                    }
                }
            }

            /// <summary>
            /// 自身が格納されているSectorsコレクションクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public ISectors ParentCollection
            {
                get
                {
                    return Collection;
                }
            }

            /// <summary>
            /// 自身の親であるQuestionクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IQuestion ParentQuestion
            {
                get 
                {
                    if (Collection == null) return null;
                    return Collection.ParentQuestion;
                }
            }

            /// <summary>
            /// 選択肢単位の集計結果(全体)を取得/設定するプロパティ
            /// </summary>
            public double Value { get; set; }

            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                Collection = null;
                addedquestion = null;
            }

            /// <summary>
            /// CompareToメソッドの実装
            /// <note>このインスタンスおよび比較対象のインスタンスのValueプロパティの値をあらかじめ設定しておく必要がある</note>
            /// </summary>
            /// <param name="obj">比較対象のISectorクラスのインスタンスへの参照</param>
            /// <returns>自身のインスタンスの持つデータが、比較対象のデータに比べて小さい場合には-1、同じ場合には0、大きい場合には0</returns>
            public int CompareTo(object obj)
            {
                Sector other = obj as Sector;
                return this.Value.CompareTo(other.Value);
            }
        }

        Questions.Question question = null;
		private string v;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="question">親のQuestionクラスのインスタンスへの参照</param>
		public Sectors(Questions.Question question)
        {
            this.question = question;
        }

		//public Sectors(string v)
		//{
		//	this.v = v;
		//}

		/// <summary>
		/// Sectorクラスのインスタンスを生成して、それへの参照を自身のコレクションに追加するメソッド
		/// </summary>
		/// <param name="number">選択肢番号</param>
		/// <param name="description">選択肢文</param>
		/// <param name="originaldescription">既定の選択肢文</param>
		/// <param name="weight">ウエイト値の文字列表現</param>
		/// <param name="isunsort">並べ替え対象からはずすことを示すフラグ</param>
		/// <returns></returns>
		public Sector Add(int number, string description = null, string originaldescription = null
                    , string weight = null, bool isunsort = false)
        {
            if (this.Contains(number.ToString()))
            {
                return this[number] as Sector;
            }
            Sector newSector = new Sector(this, number);
            newSector.Description = description;
            newSector.OriginalDescription = originaldescription;
            newSector.Weight = weight;
            newSector.IsUnsort = isunsort;
            this.Add(number.ToString(), newSector);
            return newSector;
        }

        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="number">選択肢番号</param>
        /// <returns>選択肢番号が示す選択肢情報を保持したSectorクラスのインスタンスへの参照</returns>
        public ISector this[int number]
        {
            get 
            {
                string key = number.ToString();
                if (this.Contains(key))
                {
                    return this[number.ToString()] as Sector;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 親質問を表すQuestionクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IQuestion ParentQuestion
        {
            get 
            {
                return question;
            }
        }

        /// <summary>
        /// 集計結果を元にSectorクラスのインスタンスへの参照を並べ替えた結果のListオブジェクトへの参照を返すメソッド
        /// </summary>
        /// <param name="sectorNValues">選択肢番号順にN値を格納した配列</param>
        /// <returns>並べ替えた結果のListクラスのインスタンスへの参照</returns>
        [ComVisible(false)]
        public List<ISector> SortedSectors(double[] sectorNValues)
        {
            if (sectorNValues == null || sectorNValues.Length != this.Count || question == null) return null;
            if (question.DoSort)
            {
                for (int i = 0; i < this.Count; ++i)
                {
                    (this[i + 1] as Sector).Value = sectorNValues[i];
                }
                List<ISector> SortList = new List<ISector>();
                List<ISector> UnsortList = new List<ISector>();
                for (int i = 0; i < this.Count; ++i)
                {
                    if (this[i + 1].IsUnsort)
                    {
                        UnsortList.Add(this[i + 1]);
                    }
                    else
                    {
                        SortList.Add(this[i + 1]);
                    }
                }
                if (SortList.Count > 1)
                {
                    Macromill.QCWeb.Common.GlobalMethodClass.StableSort<ISector>(ref SortList);
                }
                for (int i = 0; i < UnsortList.Count; ++i)
                {
                    SortList.Add(UnsortList[i]);
                }
                return SortList;
            }
            else
            {
                List<ISector> res = new List<ISector>(this.Count);
                for (int i = 0; i < this.Count; ++i)
                {
                    res.Add(this[(i + 1).ToString()] as Sector);
                    // res[i] = this[(i + 1).ToString()] as Sector;
                }
                return res;
            }
        }

        /// <summary>
        /// 集計結果を元にISectorクラスのインスタンスへの参照を並べ替えた結果のListオブジェクトへの参照を返すメソッド
        /// </summary>
        /// <param name="sectorNValues">選択肢番号順にN値を格納した配列</param>
        /// <returns>並べ替えた結果のListクラスのインスタンスへの参照</returns>
        [ComVisible(false)]
        public List<ISector> SortedSectors(int[] sectorNValues)
        {
            if (sectorNValues == null) return null;
            Converter<int, double> converter = x => (double)x;
            double[] sectornvalues = Array.ConvertAll<int, double>(sectorNValues, converter);
            return SortedSectors(sectornvalues);
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (DictionaryEntry de in this)
            {
                Sector sector = de.Value as Sector;
                sector.Dispose();
            }
            question = null;
        }
    }
}
