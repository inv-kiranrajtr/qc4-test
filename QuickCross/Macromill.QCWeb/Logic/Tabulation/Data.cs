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

#define AFTER_2ND_PHASE
#define IS_2ND_PHASE
#undef AFTER_2ND_PHASE
#undef IS_2ND_PHASE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Tabulation
{
    #region Dataクラスとそのサブクラス
    /// <summary>
    /// 各レコードのデータを扱うクラスの基底クラス
    /// </summary>
    [ComVisible(false), Guid("C7DE6A1F-FBAD-44b6-A12F-A6EA3BA9FBB3")]
    public class Data
    {
        #region メンバ変数
        private DataType dataType = DataType.NormalData;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataType">データ種別を表すDataType列挙型の値</param>
        /// <param name="isDeleted">
        /// 削除データかどうかを示すフラグ (省略可、既定値false)
        /// <note><paramref name="dataType"/>にDataType.DeletedDataを含めて指定してもかまわない</note>
        /// </param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        public Data(DataType dataType, bool isDeleted = false)
        {
            if (isDeleted) dataType |= Tabulation.DataType.DeletedData;
            this.dataType = dataType;
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// データ種別は既定値のままインスタンスを生成する
        /// </summary>
        public Data()
        {
        }
        #endregion

        #region インスタンスメンバ
        /// <summary>
        /// データ種別を、DataType列挙型の値で返す読み取り専用プロパティ
        /// </summary>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        public DataType DataType
        {
            get
            {
                return dataType;
            }
            protected set
            {
                dataType = value;
            }
        }

        /// <summary>
        /// 削除データかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                return (dataType & Tabulation.DataType.DeletedData) == Tabulation.DataType.DeletedData;
            }
        }

        /// <summary>
        /// 通常データかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool IsNormal
        {
            get
            {
                return (dataType & ~Tabulation.DataType.DeletedData) == Tabulation.DataType.NormalData;
            }
        }

        /// <summary>
        /// 無回答データかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool IsNA
        {
            get
            {
                return (dataType & ~Tabulation.DataType.DeletedData) == Tabulation.DataType.NAData;
            }
        }

        /// <summary>
        /// 非該当データかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool IsIV
        {
            get
            {
                return (dataType & ~Tabulation.DataType.DeletedData) == Tabulation.DataType.IVData;
            }
        }

        /// <summary>
        /// データの種類が条件と等しいかどうかを返すメソッド
        /// </summary>
        /// <param name="CriteriaDataType">条件のデータ種類を表すDataType列挙型の値</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public bool Equals(DataType CriteriaDataType)
        {
            CriteriaDataType &= ~DataType.DeletedData;
            DataType dType = this.dataType & ~DataType.DeletedData;
            return CriteriaDataType == dType;
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// データが条件値リストと等しいかどうかを返すメソッド
        /// <note>MADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValueList">
        /// 条件値リスト
        /// <note>
        /// 選択肢番号からなるリストではなく、MADataのValueプロパティの配列に即した形のリスト<br />
        /// MAData.GetCriteriaValueListメソッドを使って、選択肢リストから取得することができる
        /// </note>
        /// </param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public virtual bool Equals(int[] CriteriaValueList)
        {
            return false;
        }
#endif

        /// <summary>
        /// データが条件値と等しいかどうかを返すメソッド
        /// <note>SADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaSector">条件値 (選択肢番号)</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public virtual bool Equals(int CriteriaSector)
        {
            return false;
        }

        /// <summary>
        /// データが条件値と等しいかどうかを返すメソッド
        /// <note>NDataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public virtual bool Equals(double CriteriaValue)
        {
            return false;
        }

        /// <summary>
        /// データが条件値と等しいかどうかを返すメソッド
        /// <note>FADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public virtual bool Equals(string CriteriaValue)
        {
            return false;
        }

        /// <summary>
        /// データが条件データと等しいかどうかを返すメソッド
        /// </summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public virtual bool Equals(Data CriteriaData)
        {
            return false;
        }

        public virtual bool Equals(NData CriteriaValue)
        {
            return false;
        }
        /// <summary>
        /// データの種類が条件に含まれるかどうかを返すメソッド
        /// <note>条件には、NAData、IVDataのいずれかまたはそのビット和を指定</note>
        /// </summary>
        /// <param name="CriteriaDataType">条件のデータ種類を表すDataType列挙型の値</param>
        /// <returns>条件のいずれかの場合true、いずれでもない場合false</returns>
        public bool IsAnyOne(DataType CriteriaDataType)
        {
            CriteriaDataType &= ~DataType.DeletedData;
            DataType dType = this.dataType & ~DataType.DeletedData;
            return (CriteriaDataType & dType) != Tabulation.DataType.NormalData;
        }

        /// <summary>
        /// データが条件選択肢リストに含まれるかどうかを返すメソッド
        /// <note>SADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaSectorsList">条件選択肢リスト</param>
        /// <returns>条件選択肢リストのいずれかの場合true、いずれでもない場合false</returns>
        public virtual bool IsAnyOne(int[] CriteriaSectorsList)
        {
            return false;
        }

        /// <summary>
        /// データが条件値リストに含まれるかどうかを返すメソッド
        /// <note>NDataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValueList">条件値リスト</param>
        /// <returns>条件値リストのいずれかの場合true、いずれでもない場合false</returns>
        public virtual bool IsAnyOne(double[] CriteriaValueList)
        {
            return false;
        }

        /// <summary>
        /// データが条件範囲リストのいずれかに含まれるかどうかを返すメソッド
        /// <note>NDataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaRangeList">条件範囲リスト</param>
        /// <returns>条件範囲リストのいずれかの範囲内の場合true、いずれの範囲内でもない場合false</returns>
        public virtual bool IsAnyOne(NData.ValueRange[] CriteriaRangeList)
        {
            return false;
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// データが条件値リストに含まれるかどうかを返すメソッド
        /// <note>FADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValueList">条件値リスト</param>
        /// <returns>条件値リストのいずれかの場合true、いずれでもない場合false</returns>
        public virtual bool IsAnyOne(string[] CriteriaValueList)
        {
            return false;
        }
#endif

#if AFTER_2ND_PHASE
        /// <summary>
        /// データが条件値リストのすべてを含むかどうかを返すメソッド
        /// <note>MADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValueList">
        /// 条件値リスト
        /// <note>
        /// 選択肢番号からなるリストではなく、MADataのValueプロパティの配列に即した形のリスト<br />
        /// MAData.GetCriteriaValueListメソッドを使って、選択肢リストから取得することができる
        /// </note>
        /// </param>
        /// <returns>条件値リストのすべてを含む場合true、一部でも含まない場合false</returns>
        public virtual bool IncludeAll(int[] CriteriaValueList)
        {
            return false;
        }
#endif

        /// <summary>
        /// データが条件値リストのいずれかを含むかどうかを返すメソッド
        /// <note>MADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValueList">
        /// 条件値リスト
        /// <note>
        /// 選択肢番号からなるリストではなく、MADataのValueプロパティの配列に即した形のリスト<br />
        /// MAData.GetCriteriaValueListメソッドを使って、選択肢リストから取得することができる
        /// </note>
        /// </param>
        /// <returns>条件値リストのいずれかを含む場合true、いずれも含まない場合false</returns>
        public virtual bool IncludeAnyone(int[] CriteriaValueList)
        {
            return false;
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// データが条件値リスト以外を含まないかどうかを返すメソッド
        /// <note>MADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValueList">
        /// 条件値リスト
        /// <note>
        /// 選択肢番号からなるリストではなく、MADataのValueプロパティの配列に即した形のリスト<br />
        /// MAData.GetCriteriaValueListメソッドを使って、選択肢リストから取得することができる
        /// </note>
        /// </param>
        /// <returns>条件値リスト以外をまったく含まない場合true、一部でも含む場合false</returns>
        public virtual bool NotIncludeUnList(int[] CriteriaValueList)
        {
            return false;
        }
#endif

        /// <summary>
        /// データが条件値より大きいかどうかを返すメソッド
        /// <note>SADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaSector">条件値 (選択肢番号)</param>
        /// <returns>大きい場合true、大きくない場合false</returns>
        public virtual bool IsGreater(int CriteriaSector)
        {
            return false;
        }

        /// <summary>
        /// データが条件値より大きいかどうかを返すメソッド
        /// <note>NDataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>大きい場合true、大きくない場合false</returns>
        public virtual bool IsGreater(double CriteriaValue)
        {
            return false;
        }

        /// <summary>
        /// データが条件データより大きいかどうかを返すメソッド
        /// <note>
        /// SADataまたはNDataの場合にのみ有効、他のデータではfalseを返す
        /// </note>
        /// </summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>大きい場合true、大きくない場合false</returns>
        public virtual bool IsGreater(Data CriteriaData)
        {
            return false;
        }

        public virtual bool IsGreater(NData CriteriaData)//QC4
        {
            return false;
        }

        /// <summary>
        /// データが条件値以上かどうかを返すメソッド
        /// <note>SADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaSector">条件値 (選択肢番号)</param>
        /// <returns>条件値以上の場合true、小さい場合false</returns>
        public virtual bool IsGreaterEqual(int CriteriaSector)
        {
            return false;
        }

        /// <summary>
        /// データが条件値以上かどうかを返すメソッド
        /// <note>NDataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>条件値以上の場合true、小さい場合false</returns>
        public virtual bool IsGreaterEqual(double CriteriaValue)
        {
            return false;
        }

        /// <summary>
        /// データが条件データ以上かどうかを返すメソッド
        /// <note>
        /// SADataまたはNDataの場合にのみ有効、他のデータではfalseを返す<br />
        /// また、比較対象とデータの種類は同じである必要がある<br />
        /// 異なる場合はfalseを返す
        /// </note>
        /// </summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>条件データ以上の場合true、小さい場合false</returns>
        public virtual bool IsGreaterEqual(Data CriteriaData)
        {
            return false;
        }

        public virtual bool IsGreaterEqual(NData CriteriaData)//QC4
        {
            return false;
        }
        /// <summary>
        /// データが条件値より小さいかどうかを返すメソッド
        /// <note>SADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaSector">条件値 (選択肢番号)</param>
        /// <returns>小さい場合true、小さくない場合false</returns>
        public virtual bool IsLess(int CriteriaSector)
        {
            return false;
        }

        /// <summary>
        /// データが条件値より小さいかどうかを返すメソッド
        /// <note>NDataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>小さい場合true、小さくない場合false</returns>
        public virtual bool IsLess(double CriteriaValue)
        {
            return false;
        }

        /// <summary>
        /// データが条件データより小さいかどうかを返すメソッド
        /// <note>
        /// SADataまたはNDataの場合にのみ有効、他のデータではfalseを返す<br />
        /// また、比較対象とデータの種類は同じである必要がある<br />
        /// 異なる場合はfalseを返す
        /// </note>
        /// </summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>小さい場合true、小さくない場合false</returns>
        public virtual bool IsLess(Data CriteriaData)
        {
            return false;
        }

        public virtual bool IsLess(NData CriteriaData)//QC4
        {
            return false;
        }
        /// <summary>
        /// データが条件値以下かどうかを返すメソッド
        /// <note>SADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaSector">条件値 (選択肢番号)</param>
        /// <returns>条件値以下の場合true、大きい場合false</returns>
        public virtual bool IsLessEqual(int CriteriaSector)
        {
            return false;
        }

        /// <summary>
        /// データが条件値以下かどうかを返すメソッド
        /// <note>NDataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>条件値以下の場合true、大きい場合false</returns>
        public virtual bool IsLessEqual(double CriteriaValue)
        {
            return false;
        }

        /// <summary>
        /// データが条件データ以下かどうかを返すメソッド
        /// <note>
        /// SADataまたはNDataの場合にのみ有効、他のデータではfalseを返す<br />
        /// また、比較対象とデータの種類は同じである必要がある<br />
        /// 異なる場合はfalseを返す
        /// </note>
        /// </summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>条件データ以下の場合true、大きい場合false</returns>
        public virtual bool IsLessEqual(Data CriteriaData)
        {
            return false;
        }

        public virtual bool IsLessEqual(NData CriteriaData)//QC4
        {
            return false;
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// データが条件値から始まるかどうかを返すメソッド
        /// <note>FADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>条件値から始まる場合true、始まらない場合false</returns>
        public virtual bool IsBeginAt(string CriteriaValue)
        {
            return false;
        }
#endif

#if AFTER_2ND_PHASE
        /// <summary>
        /// データが条件値で終わるかどうかを返すメソッド
        /// <note>FADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>条件値で終わる場合true、終わらない場合false</returns>
        public virtual bool IsEndAt(string CriteriaValue)
        {
            return false;
        }
#endif

#if AFTER_2ND_PHASE
        /// <summary>
        /// データが条件値を含むかどうかを返すメソッド
        /// <note>FADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>条件値を含む場合true、含まない場合false</returns>
        public virtual bool Include(string CriteriaValue)
        {
            return false;
        }
#endif

        /// <summary>
        /// データが条件パターンとマッチするかどうかを返すメソッド
        /// <note>FADataの場合にのみ有効、他のデータではfalseを返す</note>
        /// </summary>
        /// <param name="CriteriaPattern">
        /// 条件パターン (正規表現)
        /// <note>FAData.ConvertToRegExpPatternメソッドを使って、ワイルドカードを使ったパターン文字列から取得することができる</note>
        /// </param>
        /// <returns>マッチする場合true、マッチしない場合false</returns>
        public virtual bool IsMatch(string CriteriaPattern)
        {
            return false;
        }

        /// <summary>
        /// 条件データとの比較処理を行うメソッド
        /// </summary>
        /// <param name="CriteriaData">条件データのDataクラスのインスタンスへの参照</param>
        /// <param name="criteriaOperator">条件演算子を表すCriteriaOperator列挙型の値</param>
        /// <returns>条件を満たす場合はtrue/満たさない場合はfalse</returns>
        protected bool CompareData(Data CriteriaData, CriteriaOperator criteriaOperator)
        {
            double value = 0.0;
            if (this.GetType() == typeof(SAData))
            {
                value = (double)(this as SAData).Value;
            }
            else if (this.GetType() == typeof(NData))
            {
                value = (this as NData).Value;
            }
            else
            {
                return false;
            }
            DataType dataType = this.DataType & ~DataType.DeletedData;
            DataType criteriaDataType = CriteriaData.DataType & ~DataType.DeletedData;
            Type criteriaType = CriteriaData.GetType();
            if (criteriaType != typeof(SAData) && criteriaType != typeof(NData) && criteriaType != typeof(FAData)) return false;
            if (dataType != Tabulation.DataType.NormalData)
            {
                return criteriaOperator == CriteriaOperator.Equal && dataType == criteriaDataType;
            }
            if (criteriaDataType != Tabulation.DataType.NormalData) return false;
            double criteriaNum = 0.0;
            if (criteriaType == typeof(SAData))
            {
                criteriaNum = (double)(CriteriaData as SAData).Value;
            }
            else if (criteriaType == typeof(NData))
            {
                criteriaNum = (CriteriaData as NData).Value;
            }
            else    // FA
            {
                if (!double.TryParse((CriteriaData as FAData).Value, out criteriaNum)) return false;
            }
            switch (criteriaOperator)
            {
                case CriteriaOperator.Equal:
                    return value == criteriaNum;
                case CriteriaOperator.Greater:
                    return value > criteriaNum;
                case CriteriaOperator.GreaterEqual:
                    return value >= criteriaNum;
                case CriteriaOperator.Less:
                    return value < criteriaNum;
                case CriteriaOperator.LessEqual:
                    return value <= criteriaNum;
                default:    // ここには来ない
                    return false;
            }
        }
        #endregion
    }

    /// <summary>
    /// MAデータを扱うクラス
    /// </summary>
    [ComVisible(false), Guid("AF553227-4060-45d9-8D42-7CC13657DF20")]
    public class MAData : Data  // , IDisposable
    {
        #region メンバ変数
        private int[] value = null;
        // private int[] sectors = null;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataType">データ種別を表すDataType列挙型の値</param>
        /// <param name="valueSize">データを格納する配列のサイズ</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        public MAData(DataType dataType, int valueSize, bool isDeleted = false)
            : base(dataType, isDeleted)
        {
            this.value = new int[valueSize];
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// データ種別は既定値のままインスタンスを生成する
        /// </summary>
        /// <param name="valueSize">データを格納する配列のサイズ</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        public MAData(int valueSize, bool isDeleted = false)
        {
            if (isDeleted) this.DataType |= Tabulation.DataType.DeletedData;
            this.value = new int[valueSize];
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// データを格納する配列が不要な時に使用する
        /// </summary>
        /// <param name="dataType">データ種別を表すDataType列挙型の値</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        public MAData(DataType dataType, bool isDeleted = false)
            : base(dataType, isDeleted)
        {
        }
        #endregion

        #region インスタンスメンバ
        /// <summary>
        /// データを格納した配列の要素を取得するメソッド
        /// </summary>
        /// <param name="index">配列内インデックス</param>
        /// <returns>インデックスが示す配列要素</returns>
        public int Value(int index)
        {
            if (value == null) return 0;
            if (index >= value.GetLowerBound(0) && index <= value.GetUpperBound(0))
            {
                return this.value[index];
            }
            return 0;
        }

        /*
        private void setSectors()
        {
            sectors = null;
            if (value == null) return;
            int cnt = 0;
            for (int i = 0; i < value.Length; ++i)
            {
                for (int j = 0; j < GlobalTabulation.SECTORS_COUNT_PER_4BITE; ++j)
                {
                    if ((value[i] & (int)Math.Pow(2.0, (double)j)) != 0)
                    {
                        int n = GlobalTabulation.SECTORS_COUNT_PER_4BITE * i + j + 1;
                        Array.Resize<int>(ref sectors, ++cnt);
                        sectors[cnt - 1] = n;
                    }
                }
            }
        }
        */

        /// <summary>
        /// コード形式での文字列表現を返す読み取り専用プロパティ
        /// </summary>
        public string CodeValue
        {
            get
            {
                if (IsIV) return "*";
                if (IsNA) return "";
                if (value == null || value.Length == 0) return null;
                List<int> sectors = new List<int>();
                for (int i = 0; i < value.Length; ++i)
                {
                    for (int j = 0; j < GlobalTabulation.SECTORS_COUNT_PER_4BITE; ++j)
                    {
                        if ((value[i] & (int)Math.Pow(2.0, (double)j)) != 0)
                        {
                            int n = GlobalTabulation.SECTORS_COUNT_PER_4BITE * i + j + 1;
                            sectors.Add(n);
                        }
                    }
                }
                return string.Join<int>(",", sectors);
            }
        }

        /// <summary>
        /// 01形式での文字列表現を返すメソッド
        /// </summary>
        /// <param name="sectorsCount">選択肢数</param>
        public string BinValue(int sectorsCount)
        {
            if (IsIV) return "*";
            if (IsNA) return "";
            if (value == null || value.Length == 0) return null;
            string[] buf = new string[value.Length];
            int idx = value.Length;
            for (int i = 0; i < value.Length; ++i)
            {
                buf[--idx] = Convert.ToString(value[i], 2);
                if (buf[idx].Length < GlobalTabulation.SECTORS_COUNT_PER_4BITE)
                {
                    string preZero = new string('0', GlobalTabulation.SECTORS_COUNT_PER_4BITE - buf[idx].Length);
                    buf[idx] = preZero + buf[idx];
                }
            }
            string res = string.Join("", buf);
            if (sectorsCount >= 0 && res.Length > sectorsCount)
            {
                res = res.Substring(res.Length - sectorsCount);
            }
            else if (sectorsCount >= 0 && res.Length < sectorsCount)//Redmine id: 188588
            {
                string preZero = new string('0', sectorsCount - res.Length);
                res = preZero + res;
            }
            return res;
        }

        /// <summary>
        /// フィルタ選択肢番号配列でMaDataをフィルタを掛けた値で01文字列を作成する。
        /// </summary>
        /// <param name="CriteriaValueListFilter">フィルタ選択肢番号配列</param>
        /// <param name="operation">Equal/NotEquql</param>
        /// <returns>01のCsv文字列</returns>
        public string FilterBinValue(int[] CriteriaValueListFilter, string operation)
        {
            if (IsIV) return "*";
            if (IsNA) return "";
            if (value == null || value.Length == 0) return null;

            //条件配列を取得する。
            int[] filter = GetCriteriaValueList(CriteriaValueListFilter);
            //フィルタなしの場合、BinValueをそのまま返す
            if (filter == null || filter.Length == 0)
            {
                return BinValue(-1);
            }

            string[] buf = new string[value.Length];
            int idx = value.Length;
            int filVal = 0;
            for (int i = 0; i < value.Length; ++i)
            {
                filVal = value[i];

                if (i < filter.Length)
                {
                    if (operation == "=")
                    {
                        filVal &= filter[i];
                    }
                    else if (operation == "!=" || operation == "<>")
                    {
                        filVal &= (~filter[i]);
                    }
                }
                else
                {
                    filVal = 0;
                }
                //test

                buf[--idx] = Convert.ToString(filVal, 2);
                if (buf[idx].Length < GlobalTabulation.SECTORS_COUNT_PER_4BITE)
                {
                    string preZero = new string('0', GlobalTabulation.SECTORS_COUNT_PER_4BITE - buf[idx].Length);
                    buf[idx] = preZero + buf[idx];
                }
            }
            string res = string.Join("", buf);

            return res;
        }

        /// <summary>
        /// データを格納する配列の要素を設定するメソッド
        /// </summary>
        /// <param name="index">配列内インデックス</param>
        /// <param name="newValue">設定する値</param>
        public void setValue(int index, int newValue)
        {
            if (index >= value.GetLowerBound(0) && index <= value.GetUpperBound(0))
            {
                this.value[index] = newValue;
                // setSectors();
            }
        }

        /// <summary>
        /// データを追加するメソッド
        /// </summary>
        /// <param name="index">配列内インデックス</param>
        /// <param name="newValue">追加する値</param>
        public void apendValue(int index, int newValue)
        {
            if (index >= value.GetLowerBound(0) && index <= value.GetUpperBound(0))
            {
                this.value[index] |= newValue;
            }
        }

        /// <summary>
        /// データが格納された配列のサイズを返す読み取り専用プロパティ
        /// </summary>
        public int ValueSize
        {
            get
            {
                if (value == null) return 0;
                return value.GetLength(0);
            }
        }

        /// <summary>
        /// データを選択肢番号リストとなる配列で返す読み取り専用プロパティ
        /// </summary>
        public int[] SectorsArray
        {
            get
            {
                int[] sectors = null;
                if (value == null) return sectors;
                int cnt = 0;
                for (int i = 0; i < value.Length; ++i)
                {
                    for (int j = 0; j < GlobalTabulation.SECTORS_COUNT_PER_4BITE; ++j)
                    {
                        if ((value[i] & (int)Math.Pow(2.0, (double)j)) != 0)
                        {
                            int n = GlobalTabulation.SECTORS_COUNT_PER_4BITE * i + j + 1;
                            Array.Resize<int>(ref sectors, ++cnt);
                            sectors[cnt - 1] = n;
                        }
                    }
                }
                return sectors;
            }
        }

        /// <summary>
        /// 反応数を返すメソッド
        /// </summary>
        /// <param name="SectorNumbers">反応数のカウント対象とする選択肢番号(1ベース)からなる配列</param>
        /// <returns>カウント対象の選択肢の反応数</returns>
        public int ResponsesCount(int[] SectorNumbers)
        {
            if (SectorNumbers == null || SectorNumbers.Length == 0) return 0;
            int s = ValueSize;
            if (s == 0) return 0;
            Array.Sort<int>(SectorNumbers);
            int maxSectorNumber = SectorNumbers[SectorNumbers.GetUpperBound(0)];
            int size = (maxSectorNumber - 1) / GlobalTabulation.SECTORS_COUNT_PER_4BITE + 1;
            if (s < size) size = s;
            int[] tmpValue = new int[size];
            for (int i = 0; i < SectorNumbers.Length; ++i)
            {
                int tmp = SectorNumbers[i] - 1;
                if (tmp < 0) continue;
                int idx = tmp / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                if (idx > size - 1) continue;
                int n = tmp % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                tmpValue[idx] |= (int)Math.Pow(2.0, (double)n);
                tmpValue[idx] &= value[idx];
            }
            MAData tmpData = new MAData(size);
            for (int i = 0; i < tmpValue.Length; ++i)
            {
                tmpData.setValue(i, tmpValue[i]);
            }
            string bin = tmpData.BinValue(size * GlobalTabulation.SECTORS_COUNT_PER_4BITE);
            return bin.Length - bin.Replace("1", string.Empty).Length;
        }

        /// <summary>
        /// 反応数を返すメソッド
        /// </summary>
        /// <param name="minSectorNumber">
        /// カウント対象とする先頭の選択肢番号<br />
        /// 0以下の値を指定した場合は1を指定したものとする
        /// (省略可、既定値0)
        /// </param>
        /// <param name="maxSectorNumber">
        /// カウント対象とする末尾の選択肢番号
        /// 0以下の値を指定した場合、あるいは選択肢数よりも大きな値を指定した場合には、末尾の選択肢番号を指定したものとする
        /// (省略可、既定値0)
        /// </param>
        /// <returns>指定した範囲の選択肢の反応数</returns>
        public int ResponsesCount(int minSectorNumber = 0, int maxSectorNumber = 0)
        {
            int s = ValueSize;
            if (s == 0) return 0;
            if (minSectorNumber < 1) minSectorNumber = 1;
            if (maxSectorNumber < 1 || maxSectorNumber > GlobalTabulation.SECTORS_COUNT_PER_4BITE * s)
            {
                maxSectorNumber = GlobalTabulation.SECTORS_COUNT_PER_4BITE * s;
            }
            if (minSectorNumber > maxSectorNumber) return 0;
            int[] SectorNumbers = new int[maxSectorNumber - minSectorNumber + 1];
            for (int n = minSectorNumber, i = -1; n <= maxSectorNumber; ++n)
            {
                SectorNumbers[++i] = n;
            }
            return ResponsesCount(SectorNumbers);
        }

#if AFTER_2ND_PHASE
        /// <summary>データが条件値リストと等しいかどうかを返すメソッド</summary>
        /// <param name="CriteriaValueList">
        /// 条件値リスト
        /// <note>
        /// 選択肢番号からなるリストではなく、MADataのValueプロパティの配列に即した形のリスト<br />
        /// MAData.GetCriteriaValueListメソッドを使って、選択肢リストから取得することができる
        /// </note>
        /// </param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public override bool Equals(int[] CriteriaValueList)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            if (CriteriaValueList.Length > value.Length) return false;
            if (CriteriaValueList.Length < value.Length) Array.Resize<int>(ref CriteriaValueList, value.Length);
            for (int i = 0; i < value.Length; ++i)
            {
                if (value[i] != CriteriaValueList[i]) return false;
            }
            return true;
        }
#endif

        /// <summary>データが条件データと等しいかどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public override bool Equals(Data CriteriaData)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            DataType criteriaDataType = CriteriaData.DataType & ~DataType.DeletedData;
            Type criteriaType = CriteriaData.GetType();
            if (criteriaType != typeof(MAData)) return false;
            if (dataType != criteriaDataType) return false;
            if (dataType == Tabulation.DataType.NormalData)
            {
                MAData criteriaData = CriteriaData as MAData;
                MAData smallData = null;
                MAData bigData = null;
                if (value.Length <= criteriaData.ValueSize)
                {
                    smallData = this;
                    bigData = criteriaData;
                }
                else
                {
                    smallData = criteriaData;
                    bigData = this;
                }
                for (int i = 0; i < smallData.ValueSize; ++i)
                {
                    if (smallData.Value(i) != bigData.Value(i)) return false;
                }
                for (int i = smallData.ValueSize; i < bigData.ValueSize; ++i)
                {
                    if (bigData.Value(i) != 0) return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }

#if AFTER_2ND_PHASE
        /// <summary>データが条件値リストのすべてを含むかどうかを返すメソッド</summary>
        /// <param name="CriteriaValueList">
        /// 条件値リスト
        /// <note>
        /// 選択肢番号からなるリストではなく、MADataのValueプロパティの配列に即した形のリスト<br />
        /// MAData.GetCriteriaValueListメソッドを使って、選択肢リストから取得することができる
        /// </note>
        /// </param>
        /// <returns>条件値リストのすべてを含む場合true、一部でも含まない場合false</returns>
        public override bool IncludeAll(int[] CriteriaValueList)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            if (CriteriaValueList.Length < value.Length) Array.Resize<int>(ref CriteriaValueList, value.Length);
            for (int i = 0; i < value.Length; ++i)
            {
                if ((value[i] & CriteriaValueList[i]) != CriteriaValueList[i]) return false;
            }
            return true;
        }
#endif

        /// <summary>データが条件値リストのいずれかを含むかどうかを返すメソッド</summary>
        /// <param name="CriteriaValueList">
        /// 条件値リスト
        /// <note>
        /// 選択肢番号からなるリストではなく、MADataのValueプロパティの配列に即した形のリスト<br />
        /// MAData.GetCriteriaValueListメソッドを使って、選択肢リストから取得することができる
        /// </note>
        /// </param>
        /// <returns>条件値リストのいずれかを含む場合true、いずれも含まない場合false</returns>
        public override bool IncludeAnyone(int[] CriteriaValueList)
        {
            if (CriteriaValueList == null)
                return false;
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            if (CriteriaValueList.Length < value.Length) Array.Resize<int>(ref CriteriaValueList, value.Length);
            for (int i = 0; i < value.Length; ++i)
            {
                if ((value[i] & CriteriaValueList[i]) != 0) return true;
            }
            return false;
        }

#if AFTER_2ND_PHASE
        /// <summary>データが条件値リスト以外を含まないかどうかを返すメソッド</summary>
        /// <param name="CriteriaValueList">
        /// 条件値リスト
        /// <note>
        /// 選択肢番号からなるリストではなく、MADataのValueプロパティの配列に即した形のリスト<br />
        /// MAData.GetCriteriaValueListメソッドを使って、選択肢リストから取得することができる
        /// </note>
        /// </param>
        /// <returns>条件値リスト以外をまったく含まない場合true、一部でも含む場合false</returns>
        public override bool NotIncludeUnList(int[] CriteriaValueList)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            if (CriteriaValueList.Length < value.Length) Array.Resize<int>(ref CriteriaValueList, value.Length);
            for (int i = 0; i < value.Length; ++i)
            {
                if ((value[i] & ~CriteriaValueList[i]) != 0) return false;
            }
            return true;
        }
#endif
        #endregion

        #region 静的メンバ
        /// <summary>
        /// 選択肢番号からなる配列から、MADataのValueプロパティの配列に即した形の配列を返す静的メソッド
        /// </summary>
        /// <param name="CriteriaSectorsList">条件の選択肢番号からなる配列</param>
        /// <returns>MADataの条件値リストとなる配列</returns>
        public static int[] GetCriteriaValueList(int[] CriteriaSectorsList)
        {
            int[] res = null;
            if (CriteriaSectorsList != null)
            {
                for (int i = 0; i < CriteriaSectorsList.Length; ++i)
                {
                    if (CriteriaSectorsList[i] > 0)
                    {
                        int idx = (CriteriaSectorsList[i] - 1) / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                        int e = (CriteriaSectorsList[i] - 1) % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                        if (res == null || idx > res.GetUpperBound(0)) Array.Resize<int>(ref res, idx + 1);
                        res[idx] |= (int)Math.Pow(2.0, (double)e);
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// コード形式表現を01形式表現に変換して返す静的メソッド
        /// </summary>
        /// <param name="codeBuffer">コード形式表現</param>
        /// <param name="sectorsCount">選択肢数</param>
        /// <param name="ignoreDuplicate">選択肢の重複を許可する場合true (省略可、既定値true)</param>
        /// <param name="ignoreOutOfSectorsRange">範囲外の選択肢番号を無視する場合true (省略可、既定値true)</param>
        /// <param name="ignoreNaN">整数値でないものを無視する場合true (省略可、既定値true)</param>
        /// <returns>
        /// 01形式表現
        /// <note>
        /// 無回答の場合は空文字列が返る<br />
        /// 不正な値の場合、または<paramref name="sectorsCount"/>に非自然数が指定された場合にはnullが返る
        /// </note>
        /// </returns>
        public static string GetBinBuffer(string codeBuffer, int sectorsCount
                , bool ignoreDuplicate = true, bool ignoreOutOfSectorsRange = true, bool ignoreNaN = true)
        {
            if (sectorsCount <= 0) return null;
            if (string.IsNullOrWhiteSpace(codeBuffer)) return string.Empty; // 無回答
            codeBuffer = codeBuffer.Trim();
            if (codeBuffer.Equals("*")) return "*";  // 非該当
            string[] splitBuffer = codeBuffer.Split(',');
            char[] res = new string('0', sectorsCount).ToCharArray();
            bool isNA = true;
            for (int i = 0; i < splitBuffer.Length; ++i)
            {
                int secNo = 0;
                if (!Macromill.QCWeb.Common.GlobalMethodClass.IsIntegerExpression(
                        splitBuffer[i], out secNo, true, false, false))
                {
                    // 整数ではない
                    if (ignoreNaN) continue;
                    return null;
                }
                if (secNo >= 1 && secNo <= sectorsCount)
                {
                    int idx = sectorsCount - secNo;
                    if (res[idx] == '1')
                    {
                        // 重複
                        if (ignoreOutOfSectorsRange) continue;
                        return null;
                    }
                    res[idx] = '1';
                    isNA = false;
                }
                else
                {
                    // 選択肢範囲内ではない                
                    if (ignoreOutOfSectorsRange) continue;
                    return null;
                }
            }
            return isNA ? string.Empty : new string(res);
        }

        /// <summary>
        /// QC3で「=」が示す処理を行う、==演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaValueList"></param>
        /// <returns></returns>
        public static bool operator ==(MAData data, int[] CriteriaValueList)
        {
            return data.IncludeAnyone(CriteriaValueList);
        }

        /// <summary>
        /// QC3で「=」が示す処理を行う、==演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator ==(MAData data, Data CriteriaData)
        {
            return data.Equals(CriteriaData);
        }

        /// <summary>
        /// QC3で「!=」または「&lt;&gt;」が示す処理を行う、!=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaValueList"></param>
        /// <returns></returns>
        public static bool operator !=(MAData data, int[] CriteriaValueList)
        {
            return !data.IncludeAnyone(CriteriaValueList);
        }

        /// <summary>
        /// QC3で「!=」または「&lt;&gt;」が示す処理を行う、!=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator !=(MAData data, Data CriteriaData)
        {
            return !data.Equals(CriteriaData);
        }
        #endregion

        /*
        #region インターフェイスの実装
        /// <summary>
        /// System.IDisposable.Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            value = null;
        }
        #endregion
        */
    }

    /// <summary>
    /// SAデータを扱うクラス
    /// </summary>
    [ComVisible(false), Guid("7BCBA5AE-8AF3-4d5f-8864-D8F65548566E")]
    public class SAData : Data
    {
        #region メンバ変数
        private int value = 0;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataType">データ種別を表すDataType列挙型の値</param>
        /// <param name="value">データ</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        public SAData(DataType dataType, int value, bool isDeleted = false)
            : base(dataType, isDeleted)
        {
            this.value = value;
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// データ種別は既定値のままインスタンスを生成する
        /// </summary>
        /// <param name="value">データ</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        public SAData(int value, bool isDeleted = false)
        {
            if (isDeleted) this.DataType |= Tabulation.DataType.DeletedData;
            this.value = value;
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// データが不要な時に使用する
        /// </summary>
        /// <param name="dataType">データ種別を表すDataType列挙型の値</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        public SAData(DataType dataType, bool isDeleted = false)
            : base(dataType, isDeleted)
        {
        }
        #endregion

        #region インスタンスメンバ
        /// <summary>
        /// データを返す読み取り専用プロパティ
        /// </summary>
        public int Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>データが条件値と等しいかどうかを返すメソッド</summary>
        /// <param name="CriteriaSector">条件値 (選択肢番号)</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public override bool Equals(int CriteriaSector)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value == CriteriaSector;
        }

        /// <summary>データが条件データと等しいかどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public override bool Equals(Data CriteriaData)
        {
            return CompareData(CriteriaData, CriteriaOperator.Equal);
        }
        public override bool Equals(NData CriteriaSector)//QC4
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value == CriteriaSector.Value;
        }
        /// <summary>データが条件選択肢リストに含まれるかどうかを返すメソッド</summary>
        /// <param name="CriteriaSectorsList">条件選択肢リスト</param>
        /// <returns>条件選択肢リストのいずれかの場合true、いずれでもない場合false</returns>
        public override bool IsAnyOne(int[] CriteriaSectorsList)
        {
            if (CriteriaSectorsList == null || CriteriaSectorsList.Length == 0) return false; // 念のため
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            // バイナリサーチ (二分探索)
            return Array.BinarySearch<int>(CriteriaSectorsList, value) >= 0;
        }

        /// <summary>データが条件値より大きいかどうかを返すメソッド</summary>
        /// <param name="CriteriaSector">条件値 (選択肢番号)</param>
        /// <returns>大きい場合true、大きくない場合false</returns>
        public override bool IsGreater(int CriteriaSector)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value > CriteriaSector;
        }

        /// <summary>データが条件データより大きいかどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>大きい場合true、大きくない場合false</returns>
        public override bool IsGreater(Data CriteriaData)
        {
            return CompareData(CriteriaData, CriteriaOperator.Greater);
        }

        public override bool IsGreater(NData CriteriaSector)//QC4
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value > CriteriaSector.Value;
        }


        /// <summary>データが条件値以上かどうかを返すメソッド</summary>
        /// <param name="CriteriaSector">条件値 (選択肢番号)</param>
        /// <returns>条件値以上の場合true、小さい場合false</returns>
        public override bool IsGreaterEqual(int CriteriaSector)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value >= CriteriaSector;
        }
        
        /// <summary>データが条件データ以上かどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>条件データ以上の場合true、小さい場合false</returns>
        public override bool IsGreaterEqual(Data CriteriaData)
        {
            return CompareData(CriteriaData, CriteriaOperator.GreaterEqual);
        }

        public override bool IsGreaterEqual(NData CriteriaSector)//QC4
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value >= CriteriaSector.Value;
        }
        /// <summary>データが条件値より小さいかどうかを返すメソッド</summary>
        /// <param name="CriteriaSector">条件値 (選択肢番号)</param>
        /// <returns>小さい場合true、小さくない場合false</returns>
        public override bool IsLess(int CriteriaSector)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value < CriteriaSector;
        }

        /// <summary>データが条件データより小さいかどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>小さい場合true、小さくない場合false</returns>
        public override bool IsLess(Data CriteriaData)
        {
            return CompareData(CriteriaData, CriteriaOperator.Less);
        }

        public override bool IsLess(NData CriteriaSector)//QC4
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value < CriteriaSector.Value;
        }
        /// <summary>データが条件値以下かどうかを返すメソッド</summary>
        /// <param name="CriteriaSector">条件値 (選択肢番号)</param>
        /// <returns>条件値以下の場合true、大きい場合false</returns>
        public override bool IsLessEqual(int CriteriaSector)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value <= CriteriaSector;
        }

        /// <summary>データが条件データ以下かどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>条件データ以下の場合true、大きい場合false</returns>
        public override bool IsLessEqual(Data CriteriaData)
        {
            return CompareData(CriteriaData, CriteriaOperator.LessEqual);
        }

        public override bool IsLessEqual(NData CriteriaSector)//QC4
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value <= CriteriaSector.Value;
        }
        #endregion

        #region 静的メンバ
        /// <summary>
        /// QC3で「=」が示す処理を行う、==演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaSector"></param>
        /// <returns></returns>
        public static bool operator ==(SAData data, int CriteriaSector)
        {
            return data.Equals(CriteriaSector);
        }

        /// <summary>
        /// QC3で「=」が示す処理を行う、==演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaSectorsList"></param>
        /// <returns></returns>
        public static bool operator ==(SAData data, int[] CriteriaSectorsList)
        {
            return data.IsAnyOne(CriteriaSectorsList);
        }

        /// <summary>
        /// QC3で「=」が示す処理を行う、==演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator ==(SAData data, Data CriteriaData)
        {
            return data.Equals(CriteriaData);
        }

        /// <summary>
        /// QC3で「!=」または「&lt;&gt;」が示す処理を行う、!=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaSector"></param>
        /// <returns></returns>
        public static bool operator !=(SAData data, int CriteriaSector)
        {
            return !data.Equals(CriteriaSector);
        }

        /// <summary>
        /// QC3で「!=」または「&lt;&gt;」が示す処理を行う、!=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaSectorsList"></param>
        /// <returns></returns>
        public static bool operator !=(SAData data, int[] CriteriaSectorsList)
        {
            return !data.IsAnyOne(CriteriaSectorsList);
        }

        /// <summary>
        /// QC3で「!=」または「&lt;&gt;」が示す処理を行う、!=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator !=(SAData data, Data CriteriaData)
        {
            return !data.Equals(CriteriaData);
        }

        /// <summary>
        /// QC3で「&gt;」が示す処理を行う、&gt;演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaSector"></param>
        /// <returns></returns>
        public static bool operator >(SAData data, int CriteriaSector)
        {
            return data.IsGreater(CriteriaSector);
        }

        /// <summary>
        /// QC3で「&gt;」が示す処理を行う、&gt;演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator >(SAData data, Data CriteriaData)
        {
            return data.IsGreater(CriteriaData);
        }

        /// <summary>
        /// QC3で「&gt;=」が示す処理を行う、&gt;=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaSector"></param>
        /// <returns></returns>
        public static bool operator >=(SAData data, int CriteriaSector)
        {
            return data.IsGreaterEqual(CriteriaSector);
        }

        /// <summary>
        /// QC3で「&gt;=」が示す処理を行う、&gt;=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator >=(SAData data, Data CriteriaData)
        {
            return data.IsGreaterEqual(CriteriaData);
        }

        /// <summary>
        /// QC3で「&lt;」が示す処理を行う、&lt;演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaSector"></param>
        /// <returns></returns>
        public static bool operator <(SAData data, int CriteriaSector)
        {
            return data.IsLess(CriteriaSector);
        }

        /// <summary>
        /// QC3で「&lt;」が示す処理を行う、&lt;演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator <(SAData data, Data CriteriaData)
        {
            return data.IsLess(CriteriaData);
        }

        /// <summary>
        /// QC3で「&lt;=」が示す処理を行う、&lt;=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaSector"></param>
        /// <returns></returns>
        public static bool operator <=(SAData data, int CriteriaSector)
        {
            return data.IsLessEqual(CriteriaSector);
        }

        /// <summary>
        /// QC3で「&lt;=」が示す処理を行う、&lt;=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator <=(SAData data, Data CriteriaData)
        {
            return data.IsLessEqual(CriteriaData);
        }

        /// <summary>
        /// コード形式表現をDB投入値に変換して返す静的メソッド
        /// </summary>
        /// <param name="codeBuffer">コード形式表現</param>
        /// <param name="sectorsCount">選択肢数</param>
        /// <param name="ignoreOutOfSectorsRange">範囲外の選択肢番号を無視する場合true (省略可、既定値はtrue)</param>
        /// <param name="ignoreNaN">整数値でないものを無視する場合true (省略可、既定値true)</param>
        /// <returns>
        /// DB投入値
        /// <note>
        /// 無回答の場合は空文字列が返る<br />
        /// 不正な値の場合、または<paramref name="sectorsCount"/>に非自然数が指定された場合にはnullが返る
        /// </note>
        /// </returns>
        public static string GetBuffer(string codeBuffer, int sectorsCount
                , bool ignoreOutOfSectorsRange = true, bool ignoreNaN = true)
        {
            if (sectorsCount <= 0) return null;
            if (string.IsNullOrWhiteSpace(codeBuffer)) return string.Empty; // 無回答
            if (codeBuffer.Trim().Equals("*")) return "*";  // 非該当
            int secNo = 0;
            if (!Macromill.QCWeb.Common.GlobalMethodClass.IsIntegerExpression(
                    codeBuffer, out secNo, true, false, false))
            {
                // 整数ではない
                if (ignoreNaN) return string.Empty;
                return null;
            }
            if (secNo >= 1 && secNo <= sectorsCount) return secNo.ToString();
            // 選択肢範囲内ではない
            if (ignoreOutOfSectorsRange) return string.Empty;
            return null;
        }
        #endregion
    }

    /// <summary>
    /// Nデータを扱うクラス
    /// </summary>
    [ComVisible(false), Guid("AC6805B4-9167-44c6-B104-9E4C39A788FC")]
    public class NData : Data
    {
        /// <summary>
        /// データの範囲を表す構造体
        /// </summary>
        [ComVisible(false)]
        public struct ValueRange : IComparable<ValueRange>
        {
            private double minValue;
            private double maxValue;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="minValue">最小値</param>
            /// <param name="maxValue">最大値</param>
            public ValueRange(double minValue, double maxValue)
            {
                this.minValue = minValue;
                this.maxValue = maxValue;
            }
            /// <summary>
            /// 最小値
            /// </summary>
            public double MinValue
            {
                get
                {
                    return minValue;
                }
            }
            /// <summary>
            /// 最大値
            /// </summary>
            public double MaxValue
            {
                get
                {
                    return maxValue;
                }
            }

            /// <summary>
            /// CompareToメソッドの実装
            /// </summary>
            /// <param name="other">比較対象のValueRange構造体</param>
            /// <returns>最小値同士の比較結果</returns>
            public int CompareTo(ValueRange other)
            {
                return minValue.CompareTo(other.minValue);
            }
        }

        #region メンバ変数
        private double value = 0.0;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataType">データ種別を表すDataType列挙型の値</param>
        /// <param name="value">データ</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        public NData(DataType dataType, double value, bool isDeleted = false)
            : base(dataType, isDeleted)
        {
            this.value = value;
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// データ種別は既定値のままインスタンスを生成する
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        /// </summary>
        /// <param name="value">データ</param>
        public NData(double value, bool isDeleted = false)
        {
            if (isDeleted) this.DataType |= Tabulation.DataType.DeletedData;
            this.value = value;
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// データが不要な時に使用する
        /// </summary>
        /// <param name="dataType">データ種別を表すDataType列挙型の値</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        public NData(DataType dataType, bool isDeleted = false)
            : base(dataType, isDeleted)
        {
        }
        #endregion

        #region インスタンスメンバ
        /// <summary>
        /// データを返す読み取り専用プロパティ
        /// </summary>
        public double Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>データが条件値と等しいかどうかを返すメソッド</summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public override bool Equals(double CriteriaValue)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value == CriteriaValue;
        }

        /// <summary>データが条件データと等しいかどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public override bool Equals(Data CriteriaData)
        {
            return CompareData(CriteriaData, CriteriaOperator.Equal);
        }

        /// <summary>データが条件値リストに含まれるかどうかを返すメソッド</summary>
        /// <param name="CriteriaValueList">条件値リスト</param>
        /// <returns>条件値リストのいずれかの場合true、いずれでもない場合false</returns>
        public override bool IsAnyOne(double[] CriteriaValueList)
        {
            if (CriteriaValueList == null || CriteriaValueList.Length == 0) return false; // 念のため
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            // 逐次探索
            return CriteriaValueList.Contains<double>(value);
        }

        public override bool Equals(NData CriteriaValue)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value == CriteriaValue.Value;
        }
        /// <summary>データが条件値より大きいかどうかを返すメソッド</summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>大きい場合true、大きくない場合false</returns>
        public override bool IsGreater(double CriteriaValue)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value > CriteriaValue;
        }

        /// <summary>データが条件データより大きいかどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>大きい場合true、大きくない場合false</returns>
        public override bool IsGreater(Data CriteriaData)
        {
            return CompareData(CriteriaData, CriteriaOperator.Greater);
        }

        /// <summary>データが条件値以上かどうかを返すメソッド</summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>条件値以上の場合true、小さい場合false</returns>
        public override bool IsGreaterEqual(double CriteriaValue)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value >= CriteriaValue;
        }

        /// <summary>データが条件データ以上かどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>条件データ以上の場合true、小さい場合false</returns>
        public override bool IsGreaterEqual(Data CriteriaData)
        {
            return CompareData(CriteriaData, CriteriaOperator.GreaterEqual);
        }

        /// <summary>データが条件値より小さいかどうかを返すメソッド</summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>小さい場合true、小さくない場合false</returns>
        public override bool IsLess(double CriteriaValue)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value < CriteriaValue;
        }

        /// <summary>データが条件データより小さいかどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>小さい場合true、小さくない場合false</returns>
        public override bool IsLess(Data CriteriaData)
        {
            return CompareData(CriteriaData, CriteriaOperator.Less);
        }

        /// <summary>データが条件値以下かどうかを返すメソッド</summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>条件値以下の場合true、大きい場合false</returns>
        public override bool IsLessEqual(double CriteriaValue)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value <= CriteriaValue;
        }

        /// <summary>データが条件データ以下かどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>条件データ以下の場合true、大きい場合false</returns>
        public override bool IsLessEqual(Data CriteriaData)
        {
            return CompareData(CriteriaData, CriteriaOperator.LessEqual);
        }

        /// <summary>データが条件範囲リストのいずれかに含まれるかどうかを返すメソッド</summary>
        /// <param name="CriteriaRangeList">条件範囲リスト</param>
        /// <returns>条件範囲リストのいずれかの範囲内の場合true、いずれの範囲内でもない場合false</returns>
        public override bool IsAnyOne(ValueRange[] CriteriaRangeList)
        {
            if (CriteriaRangeList == null || CriteriaRangeList.Length == 0) return false; // 念のため
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            // MinValue <= MaxValueが担保されている必要はない
            for (int i = 0; i < CriteriaRangeList.Length; ++i)
            {
                if (value >= CriteriaRangeList[i].MinValue && value <= CriteriaRangeList[i].MaxValue) return true;
            }
            return false;
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// データが2つの条件値の間(両端を含む)にあるかどうかを返すメソッド
        /// <note><paramref name="CriteriaValue1"/>と<paramref name="CriteriaValue2"/>の大小は問わない</note>
        /// </summary>
        /// <param name="CriteriaValue1">条件値</param>
        /// <param name="CriteriaValue2">条件値</param>
        /// <returns></returns>
        public bool IsBetween(double CriteriaValue1, double CriteriaValue2)
        {
            return (CriteriaValue1 - value) * (CriteriaValue2 - value) <= 0;
        }
#endif
        #endregion

        #region 静的メンバ
        /// <summary>
        /// QC3で「=」が示す処理を行う、==演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaValue"></param>
        /// <returns></returns>
        public static bool operator ==(NData data, double CriteriaValue)
        {
            return data.Equals(CriteriaValue);
        }

        /// <summary>
        /// QC3で「=」が示す処理を行う、==演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaValueList"></param>
        /// <returns></returns>
        public static bool operator ==(NData data, double[] CriteriaValueList)
        {
            return data.IsAnyOne(CriteriaValueList);
        }

        /// <summary>
        /// QC3で「=」が示す処理を行う、==演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator ==(NData data, Data CriteriaData)
        {
            return data.Equals(CriteriaData);
        }

        /// <summary>
        /// QC3で「!=」または「&lt;&gt;」が示す処理を行う、!=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaValue"></param>
        /// <returns></returns>
        public static bool operator !=(NData data, double CriteriaValue)
        {
            return !data.Equals(CriteriaValue);
        }

        /// <summary>
        /// QC3で「!=」または「&lt;&gt;」が示す処理を行う、!=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaValueList"></param>
        /// <returns></returns>
        public static bool operator !=(NData data, double[] CriteriaValueList)
        {
            return !data.IsAnyOne(CriteriaValueList);
        }

        /// <summary>
        /// QC3で「!=」または「&lt;&gt;」が示す処理を行う、!=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator !=(NData data, Data CriteriaData)
        {
            return !data.Equals(CriteriaData);
        }

        /// <summary>
        /// QC3で「&gt;」が示す処理を行う、&gt;演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaValue"></param>
        /// <returns></returns>
        public static bool operator >(NData data, double CriteriaValue)
        {
            return data.IsGreater(CriteriaValue);
        }

        /// <summary>
        /// QC3で「&gt;」が示す処理を行う、&gt;演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator >(NData data, Data CriteriaData)
        {
            return data.IsGreater(CriteriaData);
        }

        /// <summary>
        /// QC3で「&gt;=」が示す処理を行う、&gt;=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaValue"></param>
        /// <returns></returns>
        public static bool operator >=(NData data, double CriteriaValue)
        {
            return data.IsGreaterEqual(CriteriaValue);
        }

        /// <summary>
        /// QC3で「&gt;=」が示す処理を行う、&gt;=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator >=(NData data, Data CriteriaData)
        {
            return data.IsGreaterEqual(CriteriaData);
        }

        /// <summary>
        /// QC3で「&lt;」が示す処理を行う、&lt;演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaValue"></param>
        /// <returns></returns>
        public static bool operator <(NData data, double CriteriaValue)
        {
            return data.IsLess(CriteriaValue);
        }

        /// <summary>
        /// QC3で「&lt;」が示す処理を行う、&lt;演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator <(NData data, Data CriteriaData)
        {
            return data.IsLess(CriteriaData);
        }

        /// <summary>
        /// QC3で「&lt;=」が示す処理を行う、&lt;=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaValue"></param>
        /// <returns></returns>
        public static bool operator <=(NData data, double CriteriaValue)
        {
            return data.IsLessEqual(CriteriaValue);
        }

        /// <summary>
        /// QC3で「&lt;=」が示す処理を行う、&lt;=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator <=(NData data, Data CriteriaData)
        {
            return data.IsLessEqual(CriteriaData);
        }

        /// <summary>
        /// コード形式表現をDB投入値に変換して返す静的メソッド
        /// </summary>
        /// <param name="codeBuffer">コード形式表現</param>
        /// <param name="ignoreNaN">数値でないものを無視する場合true (省略可、既定値true)</param>
        /// <returns>
        /// DB投入値
        /// <note>
        /// 無回答の場合は空文字列が返る<br />
        /// 不正な値の場合にはnullが返る
        /// </note>
        /// </returns>
        public static string GetBuffer(string codeBuffer, bool ignoreNaN = true)
        {
            if (string.IsNullOrWhiteSpace(codeBuffer)) return string.Empty; // 無回答
            if (codeBuffer.Trim().Equals("*")) return "*";
            double num = 0.0;
            if (!Macromill.QCWeb.Common.GlobalMethodClass.IsDoubleExpression(
                    codeBuffer, out num, true, true, true, false, false, true, true))
            {
                // 数値ではない
                if (ignoreNaN) return string.Empty;
                return null;
            }
            return num.ToString();
        }
        #endregion
    }

    /// <summary>
    /// FAデータを扱うクラス
    /// </summary>
    [ComVisible(false), Guid("762A7233-D8D3-4519-A3AA-3330145BBC94")]
    public class FAData : Data
    {
        #region メンバ変数
        private string value;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataType">データ種別を表すDataType列挙型の値</param>
        /// <param name="value">データ</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        public FAData(DataType dataType, string value, bool isDeleted = false)
            : base(dataType, isDeleted)
        {
            this.value = value;
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// データ種別は既定値のままインスタンスを生成する
        /// </summary>
        /// <param name="value">データ</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        public FAData(string value, bool isDeleted = false)
        {
            if (isDeleted) this.DataType |= Tabulation.DataType.DeletedData;
            this.value = value;
        }

        /// <summary>
        /// コンストラクタ
        /// <para>コンストラクタ</para>
        /// データが不要な時に使用する
        /// <note>現状では削除データの時にのみ使用する</note>
        /// </summary>
        /// <param name="dataType">データ種別を表すDataType列挙型の値</param>
        /// <param name="isDeleted">削除データかどうかを示すフラグ (省略可、既定値false)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        public FAData(DataType dataType, bool isDeleted = false)
            : base(dataType, isDeleted)
        {
        }
        #endregion

        #region インスタンスメンバ
        /// <summary>
        /// データを返す読み取り専用プロパティ
        /// </summary>
        public string Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>データが条件値と等しいかどうかを返すメソッド</summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public override bool Equals(string CriteriaValue)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return value.Equals(CriteriaValue);
        }

        /// <summary>データが条件データと等しいかどうかを返すメソッド</summary>
        /// <param name="CriteriaData">条件データ</param>
        /// <returns>等しい場合true、等しくない場合false</returns>
        public override bool Equals(Data CriteriaData)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            DataType criteriaDataType = CriteriaData.DataType & ~DataType.DeletedData;
            Type criteriaType = CriteriaData.GetType();
            if (criteriaType != typeof(SAData) && criteriaType != typeof(NData) && criteriaType != typeof(FAData)) return false;
            if (dataType != criteriaDataType) return false;
            if (dataType == Tabulation.DataType.NormalData)
            {
                if (criteriaType == typeof(SAData))
                {
                    return value.Equals((CriteriaData as SAData).Value.ToString());
                }
                else if (criteriaType == typeof(NData))
                {
                    return value.Equals((CriteriaData as NData).Value.ToString());
                }
                else    // FA
                {
                    return value.Equals((CriteriaData as FAData).Value);
                }
            }
            else
            {
                return true;
            }
        }

#if AFTER_2ND_PHASE
        /// <summary>データが条件値リストに含まれるかどうかを返すメソッド</summary>
        /// <param name="CriteriaValueList">条件値リスト</param>
        /// <returns>条件値リストのいずれかの場合true、いずれでもない場合false</returns>
        public override bool IsAnyOne(string[] CriteriaValueList)
        {
            if (CriteriaValueList == null || CriteriaValueList.Length == 0) return false; // 念のため
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            // 逐次探索
            return CriteriaValueList.Contains<string>(value);
        }
#endif

        /// <summary>データが条件パターンとマッチするかどうかを返すメソッド</summary>
        /// <param name="CriteriaPattern">
        /// 条件パターン (正規表現)
        /// <note>FAData.ConvertToRegExpPatternメソッドを使って、ワイルドカードを使ったパターン文字列から取得することができる</note>
        /// </param>
        /// <returns>マッチする場合true、マッチしない場合false</returns>
        public override bool IsMatch(string CriteriaPattern)
        {
            DataType dataType = this.DataType & ~DataType.DeletedData;
            if (dataType != Tabulation.DataType.NormalData) return false;
            return System.Text.RegularExpressions.Regex.IsMatch(value, CriteriaPattern);
        }

#if AFTER_2ND_PHASE
        /// <summary>データが条件値から始まるかどうかを返すメソッド</summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>条件値から始まる場合true、始まらない場合false</returns>
        public override bool IsBeginAt(string CriteriaValue)
        {
            string ptn = "^" + System.Text.RegularExpressions.Regex.Escape(CriteriaValue);
            return IsMatch(ptn);
        }
#endif

#if AFTER_2ND_PHASE
        /// <summary>データが条件値で終わるかどうかを返すメソッド</summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>条件値で終わる場合true、終わらない場合false</returns>
        public override bool IsEndAt(string CriteriaValue)
        {
            string ptn = System.Text.RegularExpressions.Regex.Escape(CriteriaValue) + "$";
            return IsMatch(ptn);
        }
#endif

#if AFTER_2ND_PHASE
        /// <summary>データが条件値を含むかどうかを返すメソッド</summary>
        /// <param name="CriteriaValue">条件値</param>
        /// <returns>条件値を含む場合true、含まない場合false</returns>
        public override bool Include(string CriteriaValue)
        {
            string ptn = System.Text.RegularExpressions.Regex.Escape(CriteriaValue);
            return IsMatch(ptn);
        }
#endif
        #endregion

        #region 静的メンバ
        /// <summary>ワイルドカードを使ったパターン文字列を、正規表現のパターン文字列に変換して返す</summary>
        /// <param name="CriteriaPattern">ワイルドカードを使ったパターン文字列</param>
        /// <returns>正規表現のパターン文字列</returns>
        public static string ConvertToRegExpPattern(string CriteriaPattern)
        {
            System.Text.RegularExpressions.Regex escapedregex
                        = new System.Text.RegularExpressions.Regex(@"\[(\!|)([^\]]+|\])\]");
            string[] splitBuf = null;
            System.Text.RegularExpressions.MatchCollection escapedwcmatches = null;
            int mCnt = 0;
            if (escapedregex.IsMatch(CriteriaPattern))
            {
                escapedwcmatches = escapedregex.Matches(CriteriaPattern);
                splitBuf = escapedregex.Split(CriteriaPattern);
                mCnt = escapedwcmatches.Count;
            }
            else
            {
                splitBuf = new string[1];
                splitBuf[0] = CriteriaPattern;
            }
            System.Text.RegularExpressions.Regex wcregex
                        = new System.Text.RegularExpressions.Regex(@"[\*\?\#]");
            System.Text.StringBuilder newBuf = new System.Text.StringBuilder("");
            for (int i = 0; i <= mCnt; ++i)
            {
                if (i > 0)
                {
                    string tmp = escapedwcmatches[i - 1].Groups[1].Value;
                    if (tmp.Equals("!")) tmp = "^";
                    newBuf.Append("[" + tmp);
                    tmp = System.Text.RegularExpressions.Regex.Escape(escapedwcmatches[i - 1].Groups[2].Value);
                    newBuf.Append(tmp + "]");
                }
                if (wcregex.IsMatch(splitBuf[i * 3]))
                {
                    System.Text.RegularExpressions.MatchCollection wcmatches = wcregex.Matches(splitBuf[i * 3]);
                    string[] splitbuf = wcregex.Split(splitBuf[i * 3]);
                    for (int j = 0; j < splitbuf.Length; ++j)
                    {
                        if (j > 0)
                        {
                            string tmp = wcmatches[j - 1].Value;
                            if (tmp.Equals("*"))
                            {
                                tmp = @"[\d\D]*";
                            }
                            else if (tmp.Equals("?"))
                            {
                                tmp = @"[\d\D]";
                            }
                            else if (tmp.Equals("#"))
                            {
                                tmp = @"\d";
                            }
                            newBuf.Append(tmp);
                        }
                        newBuf.Append(System.Text.RegularExpressions.Regex.Escape(splitbuf[j]));
                    }
                }
                else
                {
                    newBuf.Append(System.Text.RegularExpressions.Regex.Escape(splitBuf[i * 3]));
                }
            }
            return newBuf.ToString();
        }

        /// <summary>
        /// QC3で「=」が示す処理を行う、==演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaPattern"></param>
        /// <returns></returns>
        public static bool operator ==(FAData data, string CriteriaPattern)
        {
            // return data.IsMatch(CriteriaPattern);// [Redmine id : 175610]
            return data.Equals(CriteriaPattern);// [Redmine id : 175610]
        }

        /// <summary>
        /// QC3で「=」が示す処理を行う、==演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator ==(FAData data, Data CriteriaData)
        {
            return data.Equals(CriteriaData);
        }

        /// <summary>
        /// QC3で「!=」または「&lt;&gt;」が示す処理を行う、!=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaPattern"></param>
        /// <returns></returns>
        public static bool operator !=(FAData data, string CriteriaPattern)
        {
            return !data.Equals(CriteriaPattern);// [Redmine id : 178707]// !data.IsMatch(CriteriaPattern);
        }

        /// <summary>
        /// QC3で「!=」または「&lt;&gt;」が示す処理を行う、!=演算子のオーバーロード
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CriteriaData"></param>
        /// <returns></returns>
        public static bool operator !=(FAData data, Data CriteriaData)
        {
            return !data.Equals(CriteriaData);
        }
        #endregion
    }
    #endregion
}
