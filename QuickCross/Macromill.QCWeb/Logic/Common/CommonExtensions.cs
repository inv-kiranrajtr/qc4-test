#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：CommonExtensions.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2013/1/24
 * 作　成　者：井川はるき
 * 更　新　日：2013/1/24
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion

#define AFTER_2ND_PHASE
#undef AFTER_2ND_PHASE

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common
{
    /// <summary>
    /// Common.csで定義した静的メソッドの拡張メソッド群を定義した静的クラス
    /// </summary>
    [ComVisible(false), Guid("23BED139-5BC1-462F-A099-6C876579A373")]
    public static class CommonExtensions
    {
        /// <summary>
        /// 一次元配列の安定ソートを行う
        /// </summary>
        /// <remarks>
        /// 安定ソートとは、等価のものについては元の並び順を保持して、ソートすることです。
        /// </remarks>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする一次元配列</param>
        /// <param name="comparison">比較メソッドを参照するdelegateへの参照</param>
        /// <param name="index">
        /// ソート対象範囲の開始インデックス (省略可、既定値0)
        /// <note>0未満の値を指定した場合は0とする</note>
        /// </param>
        /// <param name="length">
        /// 並べ替え対象要素数 (省略可、既定値0)
        /// <note>0以下の値を指定した場合は<paramref name="array"/>の要素数とする</note>
        /// </param>
        /// <example>
        /// 次のサンプルは、文字列型配列の要素を、文字数で昇順に安定ソートして、結果を出力します。
        /// <code lang="C#">
        /// string[] strArray = { "000", "0", "00", "11", "111", "1" };
        /// Comparison&lt;string&gt; comparison = delegate(string x, string y) { return x.Length - y.Length; };
        /// strArray.StableSort&lt;string&gt;(comparison);
        /// for (int i = 0; i &lt; strArray.Length; )
        /// {
        ///     Console.WriteLine(strArray[i++]);
        /// }
        /// </code>
        /// </example>
        public static void StableSort<T>(this T[] array, Comparison<T> comparison, int index = 0, int length = 0) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, comparison, index, length);
        }

        /// <summary>
        /// 一次元配列の安定ソートを行う<br />
        /// 降順ソートの指定を容易にするオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする一次元配列</param>
        /// <param name="isDesc">降順にソートするときtrue/昇順ではfalse</param>
        /// <param name="index">
        /// ソート対象範囲の開始インデックス (省略可、既定値0)
        /// <note>0未満の値を指定した場合は0とする</note>
        /// </param>
        /// <param name="length">
        /// 並べ替え対象要素数 (省略可、既定値0)
        /// <note>0以下の値を指定した場合は<paramref name="array"/>の要素数とする</note>
        /// </param>
        /// <example>
        /// 次のサンプルは、文字列型配列の要素を降順に安定ソートし、結果を出力します。
        /// <code lang="C#">
        /// string[] strArray = { "Yamaji", "Kawahito", "Shimazaki", "Yamada", "Yoshino" };
        /// strArray.StableSort&lt;string&gt;(true);
        /// for (int i = 0; i &lt; strArray.Length; )
        /// {
        ///     Console.WriteLine(strArray[i++]);
        /// }
        /// </code>
        /// </example>
        public static void StableSort<T>(this T[] array, bool isDesc, int index = 0, int length = 0) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, isDesc, index, length);
        }

        /// <summary>
        /// 一次元配列の安定ソートを行う<br />
        /// ソートの定義(比較メソッド)の指定を省略したオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする一次元配列</param>
        /// <param name="index">
        /// ソート対象範囲の開始インデックス (省略可、既定値0)
        /// <note>0未満の値を指定した場合は0とする</note>
        /// </param>
        /// <param name="length">
        /// 並べ替え対象要素数 (省略可、既定値0)
        /// <note>0以下の値を指定した場合は<paramref name="array"/>の要素数とする</note>
        /// </param>
        /// <example>
        /// 次のサンプルは、文字列型配列の要素を昇順(既定のソート順)で安定ソートし、結果を出力します。
        /// <code lang="C#">
        /// string[] strArray = { "Yamaji", "Kawahito", "Shimazaki", "Yamada", "Yoshino" };
        /// strArray.StableSort&lt;string&gt;();
        /// for (int i = 0; i &lt; strArray.Length; )
        /// {
        ///     Console.WriteLine(strArray[i++]);
        /// }
        /// </code>
        /// </example>
        public static void StableSort<T>(this T[] array, int index = 0, int length = 0) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, index, length);
        }

        /// <summary>
        /// 二次元配列の安定ソートを行う
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="keyColumnIndex">キーとするカラムのインデックス</param>
        /// <param name="comparison">比較メソッドを参照するdelegateへの参照</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        public static void StableSort<T>(this T[,] array, int keyColumnIndex, Comparison<T> comparison, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, keyColumnIndex, comparison, IgnoreTopRowsCount);
        }

        /// <summary>
        /// 二次元配列の安定ソートを行う<br />
        /// 降順ソートの指定を容易にするオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="keyColumnIndex">キーとするカラムのインデックス</param>
        /// <param name="isDesc">降順にソートするときtrue/昇順ではfalse</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        public static void StableSort<T>(this T[,] array, int keyColumnIndex, bool isDesc, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, keyColumnIndex, isDesc, IgnoreTopRowsCount);
        }

        /// <summary>
        /// 二次元配列の安定ソートを行う<br />
        /// ソートの定義(比較メソッド)の指定を省略したオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="keyColumnIndex">キーとするカラムのインデックス</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        public static void StableSort<T>(this T[,] array, int keyColumnIndex, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, keyColumnIndex, IgnoreTopRowsCount);
        }

        /// <summary>
        /// 二次元配列の安定ソートを行う<br />
        /// キーカラムの指定を省略したオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="comparison">比較メソッドを参照するdelegateへの参照</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        public static void StableSort<T>(this T[,] array, Comparison<T> comparison, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, comparison, IgnoreTopRowsCount);
        }

        /// <summary>
        /// 二次元配列の安定ソートを行う<br />
        /// キーカラムの指定を省略し、降順ソートの指定を容易にするオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="isDesc">降順にソートするときtrue/昇順ではfalse</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        public static void StableSort<T>(this T[,] array, bool isDesc, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, isDesc, IgnoreTopRowsCount);
        }

        /// <summary>
        /// 二次元配列の安定ソートを行う<br />
        /// キーカラムの指定を省略し、ソートの定義(比較メソッド)の指定を省略したオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        public static void StableSort<T>(this T[,] array, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, IgnoreTopRowsCount);
        }

        /// <summary>
        /// 複数のキーを使って二次元配列の安定ソートを行う<br />
        /// ソートの定義が一定で、複数のキーを使ってソートを行う場合に適切なオーバーロード<br />
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="comparison">すべてのキーにおけるソートで適用する比較メソッドを参照するdelegateへの参照</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <param name="keyColumnIndexes">キーとするカラムのインデックス (可変長)</param>
        public static void StableSort<T>(this T[,] array, Comparison<T> comparison, int IgnoreTopRowsCount = 0, params int[] keyColumnIndexes) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, comparison, IgnoreTopRowsCount, keyColumnIndexes);
        }

        /// <summary>
        /// 複数のキーを使って二次元配列の安定ソートを行う<br />
        /// ソートの定義が昇順または降順で一定で、複数のキーを使ってソートを行う場合に適切なオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="isDesc">すべてのキーにおいて降順にソートするときtrue/すべてのキーにおいて昇順にソートするときfalse</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <param name="keyColumnIndexes">キーとするカラムのインデックス (可変長)</param>
        public static void StableSort<T>(this T[,] array, bool isDesc, int IgnoreTopRowsCount = 0, params int[] keyColumnIndexes) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, isDesc, IgnoreTopRowsCount, keyColumnIndexes);
        }

        /// <summary>
        /// 複数のキーを使って二次元配列の安定ソートを行う<br />
        /// ソートの定義が昇順で一定で、複数のキーを使ってソートを行う場合に適切なオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <param name="keyColumnIndexes">キーとするカラムのインデックス (可変長)</param>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32)">StableSort102</seealso>
        public static void StableSort<T>(this T[,] array, int IgnoreTopRowsCount = 0, params int[] keyColumnIndexes) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, IgnoreTopRowsCount, keyColumnIndexes);
        }

        /// <summary>
        /// 複数のキーを使って二次元配列の安定ソートを行う<br />
        /// 汎用性の高いオーバーロードで、各キーにおけるソートの定義が同一でない場合に適切なオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <param name="keys">
        /// 以下に示すいずれかのタイプのペアの羅列によって、複数のキーとソート方法を指定する (可変長)<br />
        /// <list type="bullet">
        /// <item>
        /// <description>キーとするカラムのインデックス(int型), 降順にソースすることを示すフラグ(bool型)</description>
        /// </item>
        /// <item>
        /// <description>キーとするカラムのインデックス(int型), 比較メソッドを参照するdelegateへの参照(Comparison&lt;T&gt;型)</description>
        /// </item>
        /// </list>
        /// なお、ペアのタイプは統一されている必要はない<br />
        /// 2つの引数ごとにペアとなり、その一方または両方が、どちらのタイプとも評価できない場合には、それ以降の引数をすべて無視する<br />
        /// また、ペアとならない引数(引数が奇数の場合)は無視する
        /// </param>
        public static void StableSort<T>(this T[,] array, int IgnoreTopRowsCount = 0, params object[] keys) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref array, IgnoreTopRowsCount, keys);
        }

        /// <summary>
        /// Listコレクションの安定ソートを行う
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="list">ソートするListクラスのインスタンスへの参照</param>
        /// <param name="comparison">比較メソッドを参照するdelegateへの参照</param>
        public static void StableSort<T>(this System.Collections.Generic.List<T> list, Comparison<T> comparison) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref list, comparison);
        }

        /// <summary>
        /// Listコレクションの安定ソートを行う<br />
        /// 降順ソートの指定を容易にするオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="list">ソートするListクラスのインスタンスへの参照</param>
        /// <param name="isDesc">降順にソートするときtrue/昇順ではfalse</param>
        public static void StableSort<T>(this System.Collections.Generic.List<T> list, bool isDesc) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref list, isDesc);
        }

        /// <summary>
        /// Listコレクションの安定ソートを行う<br />
        /// ソートの定義(比較メソッド)の指定を省略したオーバーロード
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="list">ソートするListクラスのインスタンスへの参照</param>
        public static void StableSort<T>(this System.Collections.Generic.List<T> list) where T : IComparable
        {
            GlobalMethodClass.StableSort<T>(ref list);
        }

        /// <summary>
        /// 集計結果を並べ替える
        /// </summary>
        /// <param name="dataArray">集計結果配列</param>
        /// <param name="sectorStartIndex">選択肢の開始列または開始行のインデックス</param>
        /// <param name="sectorEndIndex">選択肢の終了列または終了行のインデックス</param>
        /// <param name="totalIndex">全体行または全体列のインデックス</param>
        /// <param name="sortSectorIndexes">並べ替え対象の選択肢のインデックス(0ベース)からなる配列</param>
        /// <param name="sectorIncreaseDirection">選択肢の並び方向を表すSectorIncreaseDirection列挙型の値 (省略可、既定値LeftToRight)</param>
        /// <param name="setNewOrder">
        /// 並び替え結果によって、検定結果の並び替えを行う場合はtrue (省略可、既定値false)
        /// <note>この引数は<paramref name="sectorIncreaseDirection"/>がTopToBottomのときのみ有効</note>
        /// </param>
        /// <param name="orderColumnIndex"><paramref name="setNewOrder"/>がtrueのとき、オーダー列のインデックス (省略可、既定値0)</param>
        /// <param name="descColumnIndex"><paramref name="setNewOrder"/>がtrueのとき、選択肢文列のインデックス (省略可、既定値1)</param>
        /// <param name="dataColumnIndex"><paramref name="setNewOrder"/>がtrueのとき、データ列のインデックス (省略可、既定値3)</param>
        public static void StableSort(this Macromill.QCWeb.Tabulation.DataWithMarking[,] dataArray, int sectorStartIndex, int sectorEndIndex, int totalIndex
                , int[] sortSectorIndexes, GlobalMethodClass.SectorIncreaseDirection sectorIncreaseDirection = GlobalMethodClass.SectorIncreaseDirection.LeftToRight
                , bool setNewOrder = false, int orderColumnIndex = 0, int descColumnIndex = 1, int dataColumnIndex = 3)
        {
            GlobalMethodClass.StableSort(ref dataArray, sectorStartIndex, sectorEndIndex, totalIndex, sortSectorIndexes, sectorIncreaseDirection
                                       , setNewOrder, orderColumnIndex, descColumnIndex, dataColumnIndex);
        }

        /// <summary>
        /// 文字列データのハッシュ値を返す<br />
        /// 文字列データの左端にsaltの前半部分を、右端にsaltの後半部分を付けた文字列データのハッシュ値を返す
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="salt">salt文字列</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalMethodClass.HashType">HashType列挙型</seealso>
        public static string GetHash(this string data, string salt, GlobalMethodClass.HashType hashType)
        {
            return GlobalMethodClass.getHash(data, salt, hashType);
        }

        /// <summary>
        /// 文字列データのMD5ハッシュ値を返す<br />
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="salt">salt文字列</param>
        /// <returns>ハッシュ値</returns>
        public static string GetHash(this string data, string salt)
        {
            return GlobalMethodClass.getHash(data, salt);
        }

        /// <summary>
        /// 文字列にランダムなsaltを付けたデータのハッシュ値を返す
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="randomSaltLength">生成するsalt文字列の長さ</param>
        /// <param name="randomSaltSeed">salt文字列の生成に使用する乱数発生アルゴリズムのSeed値</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ハッシュ値</returns>
        public static string GetHash(this string data, int randomSaltLength, int randomSaltSeed, GlobalMethodClass.HashType hashType)
        {
            return GlobalMethodClass.getHash(data, randomSaltLength, randomSaltSeed, hashType);
        }

        /// <summary>
        /// 文字列にランダムなsaltを付けたデータのMD5ハッシュ値を返す
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="randomSaltLength">生成するsalt文字列の長さ</param>
        /// <param name="randomSaltSeed">salt文字列の生成に使用する乱数発生アルゴリズムのSeed値</param>
        /// <returns>ハッシュ値</returns>
        public static string GetHash(this string data, int randomSaltLength, int randomSaltSeed)
        {
            return GlobalMethodClass.getHash(data, randomSaltLength, randomSaltSeed);
        }

        /// <summary>
        /// 文字列にランダムなsaltを付けたデータのハッシュ値を返す
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="randomSaltLength">生成するsalt文字列の長さ</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ハッシュ値</returns>
        public static string GetHash(this string data, int randomSaltLength, GlobalMethodClass.HashType hashType)
        {
            return GlobalMethodClass.getHash(data, randomSaltLength, hashType);
        }

        /// <summary>
        /// 文字列にランダムなsaltを付けたデータのMD5ハッシュ値を返す
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="randomSaltLength">生成するsalt文字列の長さ</param>
        /// <returns>ハッシュ値</returns>
        public static string GetHash(this string data, int randomSaltLength)
        {
            return GlobalMethodClass.getHash(data, randomSaltLength);
        }

        /// <summary>
        /// 文字列データのハッシュ値を返す
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ハッシュ値</returns>
        public static string GetHash(this string data, GlobalMethodClass.HashType hashType)
        {
            return GlobalMethodClass.getHash(data, hashType);
        }

        /// <summary>
        /// 文字列データのMD5ハッシュ値を返す
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <returns>ハッシュ値</returns>
        public static string GetHash(this string data)
        {
            return GlobalMethodClass.getHash(data);
        }
    
        /// <summary>
        /// 文字列データのハッシュ値を返す
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="addRandomSalt">ランダムに生成したsalt文字列を付ける場合true/付けない場合false</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ハッシュ値</returns>
        public static string GetHash(this string data, bool addRandomSalt, GlobalMethodClass.HashType hashType)
        {
            return GlobalMethodClass.getHash(data, addRandomSalt, hashType);
        }

        /// <summary>
        /// 文字列データのMD5ハッシュ値を返す
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="addRandomSalt">ランダムに生成したsalt文字列を付ける場合true/付けない場合false</param>
        /// <returns>ハッシュ値</returns>
        public static string GetHash(this string data, bool addRandomSalt)
        {
            return GlobalMethodClass.getHash(data, addRandomSalt);
        }

        /// <summary>
        /// ピクセル値からポイント値に換算する
        /// </summary>
        /// <param name="pixel">ピクセル値</param>
        /// <returns>ポイント値</returns>
        public static float PixelToPoint(this int pixel)
        {
            return GlobalMethodClass.PixelToPoint(pixel);
        }

        /// <summary>
        /// ポイント値からピクセル値に換算する
        /// <note>換算の結果が小数を含む場合、整数に切り上げる</note>
        /// </summary>
        /// <param name="point">ポイント値</param>
        /// <returns>ピクセル値</returns>
        public static int PointToPixel(this float point)
        {
            return GlobalMethodClass.PointToPixel(point);
        }

        /// <summary>
        /// ポイント値からピクセル値に換算する
        /// <note>換算の結果が小数を含む場合、整数に切り上げる</note>
        /// </summary>
        /// <param name="point">ポイント値</param>
        /// <returns>ピクセル値</returns>
        public static int PointToPixel(this double point)
        {
            return GlobalMethodClass.PointToPixel(point);
        }

        /// <summary>
        /// 文字列のバイト長を返す
        /// </summary>
        /// <param name="buffer">バイト長を調べる文字列</param>
        /// <param name="encodeName">エンコーディングのコードページ名 (省略可、既定値「shift-jis」)</param>
        /// <returns>文字列のバイト長</returns>
        public static int GetByteLength(this string buffer, string encodeName = "shift-jis")
        {
            return GlobalMethodClass.GetByteLength(buffer, encodeName);
        }

        /// <summary>
        /// 文字列の左端から指定した長さの文字列を切り出して返すメソッド
        /// </summary>
        /// <param name="buffer">元の文字列</param>
        /// <param name="Length">切り出す長さ</param>
        /// <returns>切り出した文字列</returns>
        public static string Left(this string buffer, int Length)
        {
            return GlobalMethodClass.Left(buffer, Length);
        }

        /// <summary>
        /// 文字列の左端から指定したバイト分の文字列を切り出して返す
        /// <note>1文字途中で切り取られる場合は、その前の文字までを返す</note>
        /// </summary>
        /// <param name="buffer">元の文字列</param>
        /// <param name="Length">切り出すバイト長</param>
        /// <param name="encodeName">エンコーディングのコードページ名 (省略可、既定値「shift-jis」)</param>
        /// <returns>切り出した文字列</returns>
        public static string LeftB(this string buffer, int Length, string encodeName = "shift-jis")
        {
            return GlobalMethodClass.LeftB(buffer, Length, encodeName);
        }

        /// <summary>
        /// 文字列の幅を返す
        /// <note>描画領域として、プライマリモニタのデバイスコンテキストを用いるため、その環境に依存する</note>
        /// </summary>
        /// <param name="buffer">幅を調べる文字列</param>
        /// <param name="font">フォントを表すFontクラスのインスタンスへの参照</param>
        /// <param name="withPadding">グリフ突出対応のためのパディングを含めるかどうか (省略可、既定値true)</param>
        /// <returns>文字列の幅 (ピクセル値)</returns>
        public static int GetTextWidth(this string buffer, Font font, bool withPadding = true)
        {
            return GlobalMethodClass.GetTextWidth(buffer, font, withPadding);
        }

        /// <summary>
        /// 文字列の幅を返す
        /// <note>描画領域として、プライマリモニタのデバイスコンテキストを用いるため、その環境に依存する</note>
        /// </summary>
        /// <param name="buffer">幅を調べる文字列</param>
        /// <param name="fontName">フォント名</param>
        /// <param name="fontPointSize">フォントサイズ (ポイント)</param>
        /// <param name="fontStyle">フォントスタイルを表すFontStyle列挙型の値 (省略可、既定値FontStyle.Regular)</param>
        /// <param name="withPadding">グリフ突出対応のためのパディングを含めるかどうか (省略可、既定値true)</param>
        /// <returns>文字列の幅 (ピクセル値)</returns>
        public static int GetTextWidth(this string buffer
                    , string fontName, float fontPointSize
                    , FontStyle fontStyle = FontStyle.Regular
                    , bool withPadding = true)
        {
            return GlobalMethodClass.GetTextWidth(buffer, fontName, fontPointSize, fontStyle, withPadding);
        }

        /// <summary>
        /// 文字列の幅を返すメソッド
        /// <note>
        /// 描画領域として、プライマリモニタのデバイスコンテキストを用いるため、その環境に依存する<br />
        /// ピクセル→ポイント換算においても同様のため、実行する環境に依存する
        /// </note>
        /// </summary>
        /// <param name="buffer">幅を調べる文字列</param>
        /// <param name="fontName">フォント名</param>
        /// <param name="fontSize">フォントサイズ (ピクセル)</param>
        /// <param name="fontStyle">フォントスタイルを表すFontStyle列挙型の値 (省略可、既定値FontStyle.Regular)</param>
        /// <param name="withPadding">グリフ突出対応のためのパディングを含めるかどうか (省略可、既定値true)</param>
        /// <returns>文字列の幅 (ピクセル値)</returns>
        public static int GetTextWidth(this string buffer
                    , string fontName, int fontSize
                    , FontStyle fontStyle = FontStyle.Regular
                    , bool withPadding = true)
        {
            return GlobalMethodClass.GetTextWidth(buffer, fontName, fontSize, fontStyle, withPadding);
        }

        /// <summary>
        /// 文字列の幅を厳密に求めて返す
        /// <note>
        /// 描画領域として、プライマリモニタのデバイスコンテキストを用いるため、その環境に依存する<br />
        /// ピクセル→ポイント換算においても同様のため、実行する環境に依存する
        /// </note>
        /// </summary>
        /// <param name="buffer">幅を調べる文字列</param>
        /// <param name="font">フォントを表すFontクラスのインスタンスへの参照</param>
        /// <returns>文字列の幅 (ピクセル値)</returns>
        public static int GetStrictTextWidth(this string buffer, Font font)
        {
            return GlobalMethodClass.GetStrictTextWidth(buffer, font);
        }

        /// <summary>
        /// 一定のフォントでの文字列の行数を算出する
        /// </summary>
        /// <param name="buffer">行数を調べる文字列</param>
        /// <param name="font">フォントを表すFontクラスのインスタンスへの参照</param>
        /// <param name="areaWidth">表示領域の幅 (ピクセル値)</param>
        /// <param name="leftMargin">左余白 (ピクセル値)</param>
        /// <param name="rightMargin">右余白 (ピクセル値)</param>
        /// <param name="delimiterPattern">改行コードとする文字列の正規表現パターン (省略可:既定値「\r\n|\r|\n」)</param>
        /// <returns>成功時:行数、引数不正時:0、表示領域幅が小さすぎる時:-1</returns>
        public static int GetRowsCount(this string buffer, Font font, int areaWidth, int leftMargin, int rightMargin
                                     , string delimiterPattern = @"\r\n|\r|\n")
        {
            return GlobalMethodClass.GetRowsCount(buffer, font, areaWidth, leftMargin, rightMargin, delimiterPattern);
        }

        /// <summary>
        /// 文字列表現が、数値を示す指数表記ではない表現かどうかを返す静的メソッド
        /// </summary>
        /// <param name="buffer">文字列表現</param>
        /// <param name="resultNumber">結果の値 (戻り値)</param>
        /// <param name="ignoreByte">全角/半角の区別をしない場合はtrue (省略可、既定値false)</param>
        /// <param name="allowLeadingSign">先頭に符号を付けることを許可する場合true (省略可、既定値true)</param>
        /// <param name="allowLeadingWhite">先頭の空白を無視する場合true (省略可、既定値true)</param>
        /// <param name="allowParentheses">数値を囲むペアのかっこの使用を許可する場合true (省略可、既定値false)</param>
        /// <param name="allowThousands">桁区切り文字の使用を許可する場合true (省略可、既定値false)</param>
        /// <param name="allowTrailingWhite">末尾の空白を無視する場合true (省略可、既定値true)</param>
        /// <param name="allowExponent">指数表記を許可する場合true (省略可、既定値false)</param>
        /// <returns>文字列表現が、数値を示す場合true、そうでない場合false</returns>
        public static bool IsDoubleExpression(this string buffer, out double resultNumber, bool ignoreByte = false
                                            , bool allowLeadingSign = true
                                            , bool allowLeadingWhite = true
                                            , bool allowParentheses = false
                                            , bool allowThousands = false
                                            , bool allowTrailingWhite = true
                                            , bool allowExponent = false)
        {
            return GlobalMethodClass.IsDoubleExpression(buffer, out resultNumber, ignoreByte
                    , allowLeadingSign, allowLeadingWhite, allowParentheses, allowThousands, allowTrailingWhite, allowExponent);
        }

        /// <summary>
        /// 文字列表現が、符号付き4バイト整数型で扱える整数の表現かどうかを返すメソッド
        /// </summary>
        /// <param name="buffer">文字列表現</param>
        /// <param name="resultNumber">結果の値 (戻り値)</param>
        /// <param name="ignoreByte">全角/半角の区別をしない場合はtrue (省略可、既定値false)</param>
        /// <param name="allowDecimalPoint">小数点の使用を許可する場合true (省略可、既定値true)</param>
        /// <param name="allowLeadingSign">先頭に符号を付けることを許可する場合true (省略可、既定値true)</param>
        /// <param name="allowLeadingWhite">先頭の空白を無視する場合true (省略可、既定値true)</param>
        /// <param name="allowParentheses">数値を囲むペアのかっこの使用を許可する場合true (省略可、既定値false)</param>
        /// <param name="allowThousands">桁区切り文字の使用を許可する場合true (省略可、既定値false)</param>
        /// <param name="allowTrailingWhite">末尾の空白を無視する場合true (省略可、既定値true)</param>
        /// <returns>文字列表現が、-2147483648 ～ 2147483647の整数を示す場合true、そうでない場合および指数表記の場合false</returns>
        public static bool IsIntegerExpression(this string buffer, out int resultNumber, bool ignoreByte = false
                                             , bool allowDecimalPoint = true
                                             , bool allowLeadingSign = true
                                             , bool allowLeadingWhite = true
                                             , bool allowParentheses = false
                                             , bool allowThousands = false
                                             , bool allowTrailingWhite = true)
        {
            return GlobalMethodClass.IsIntegerExpression(buffer, out resultNumber, ignoreByte
                    , allowDecimalPoint, allowLeadingSign, allowLeadingWhite, allowParentheses, allowThousands, allowTrailingWhite);
        }

        /// <summary>
        /// 半角文字列を取得する
        /// </summary>
        /// <param name="str">変換対象文字列</param>
        /// <returns></returns>
        public static string ToStrNarrow(this string str)
        {
            return GlobalMethodClass.ToStrNarrow(str);
        }

        /// <summary>
        /// S-JIS1バイトの特殊文字を削除する
        /// </summary>
        /// <param name="buffer">文字列</param>
        /// <param name="cutLineFeed">ラインフィードもカットする場合true (省略可、既定値false)</param>
        /// <returns>特殊文字(<paramref name="cutLineFeed"/>がfalseの場合は、ラインフィードは除く)を削除した文字列</returns>
        public static string CleanSpecialCharacters(this string buffer, bool cutLineFeed = false)
        {
            return GlobalMethodClass.CleanSpecialCharacters(buffer, cutLineFeed);
        }

        /// <summary>
        /// 文字列表現を式として評価し、その結果を返す
        /// </summary>
        /// <param name="expression">文字列表現</param>
        /// <param name="qcwebid">QCWeb管理ID</param>
        /// <param name="AllowStandardFunction">MAX/MIN/AVERAGE/SUMを許容する場合はtrue (省略可、既定値false)</param>
        /// <param name="itemNamesList">アイテム名のリストを格納するstring型Listクラスのインスタンスへの参照 (省略可、既定値null)</param>
        /// <param name="itemNameValueList">アイテム名と数値のKey-Valueデータを格納するDictionaryのインスタンスへの参照 (省略可、既定値null)</param>
        /// <returns>
        /// 式として成り立っている場合、式の評価結果、成り立っていない場合NaN
        /// <note>式内にアイテム参照が含まれている場合、正しい値は返されない</note>
        /// </returns>
        public static double ParseExpression(this string expression, decimal qcwebid, bool AllowStandardFunction = false, List<string> itemNamesList = null, Dictionary<string, double> itemNameValueList = null)
        {
            return GlobalMethodClass.ParseExpression(qcwebid, expression, AllowStandardFunction, itemNamesList, itemNameValueList);
        }

        /// <summary>
        /// 四捨五入を行った結果を返す
        /// </summary>
        /// <param name="number">元の値</param>
        /// <param name="numdigitsafterdecimal">
        /// 小数点以下何桁で四捨五入するかを指定する整数値<br />
        /// 負数を指定することもできる
        /// </param>
        /// <returns>四捨五入の結果を返す</returns>
        public static decimal RoundOff(this decimal number, int numdigitsafterdecimal)
        {
            return Function.RoundOff(number, numdigitsafterdecimal);
        }

        /// <summary>
        /// 四捨五入を行った結果を返す
        /// </summary>
        /// <param name="number">元の値</param>
        /// <param name="numdigitsafterdecimal">
        /// 小数点以下何桁で四捨五入するかを指定する整数値<br />
        /// 負数を指定することもできる
        /// </param>
        /// <returns>四捨五入の結果を返す</returns>
        public static double RoundOff(this double number, int numdigitsafterdecimal)
        {
            return Function.RoundOff(number, numdigitsafterdecimal);
        }

        /// <summary>
        /// 四捨五入を行った結果を返す
        /// </summary>
        /// <param name="number">元の値</param>
        /// <returns>整数に四捨五入した結果を返す</returns>
        public static decimal RoundOff(this decimal number)
        {
            return Function.RoundOff(number);
        }

        /// <summary>
        /// 四捨五入を行った結果を返す
        /// </summary>
        /// <param name="number">元の値</param>
        /// <returns>整数に四捨五入した結果を返す</returns>
        public static double RoundOff(this double number)
        {
            return Function.RoundOff(number);
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// 高精度で平方根を返す
        /// </summary>
        /// <param name="num"></param>
        /// <returns>十進型の平方根</returns>
        public static decimal Sqrt(this decimal num)
        {
            return Function.Sqrt(num);
        }
#endif

        /// <summary>
        /// 平方根を返す
        /// </summary>
        /// <param name="num"></param>
        /// <returns>平方根</returns>
        public static double Sqrt(this double num)
        {
            if (num < 0.0) return double.NaN;
            return Math.Sqrt(num);
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// 高精度の対数関数
        /// </summary>
        /// <param name="num"></param>
        /// <returns>自然対数</returns>
        public static decimal Log(this decimal num)
        {
            return Function.Log(num);
        }
#endif

        /// <summary>
        /// 対数関数
        /// </summary>
        /// <param name="num"></param>
        /// <returns>自然対数</returns>
        public static double Log(this double num)
        {
            return Math.Log(num);
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// 高精度べき乗関数
        /// </summary>
        /// <param name="num">基数</param>
        /// <param name="n">指数</param>
        /// <returns>べき乗</returns>
        public static decimal Pow(this decimal num, decimal n)
        {
            return Function.Pow(num, n);
        }
#endif

        /// <summary>
        /// べき乗関数
        /// </summary>
        /// <param name="num">基数</param>
        /// <param name="n">指数</param>
        /// <returns>べき乗</returns>
        public static double Pow(this double num, double n)
        {
            return Math.Pow(num, n);
        }

        /// <summary>
        /// 配列の要素を任意の値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">一次元配列</param>
        /// <param name="val">初期値</param>
        /// <param name="startIndex">初期値を投入する開始インデックス</param>
        /// <param name="endIndex">初期値を投入する終了インデックス</param>
        public static void InitializeWith<T>(this T[] array, T val, int startIndex, int endIndex)
        {
            OperateArray.InitializeWith<T>(ref array, val, startIndex, endIndex);
        }

        /// <summary>
        /// 配列の要素を任意の値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">一次元配列</param>
        /// <param name="val">初期値</param>
        public static void InitializeWith<T>(this T[] array, T val)
        {
            OperateArray.InitializeWith<T>(ref array, val);
        }

        /// <summary>
        /// 配列の要素を任意の値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">二次元配列</param>
        /// <param name="val">初期値</param>
        /// <param name="startRowIndex">初期値を投入する行方向開始インデックス</param>
        /// <param name="endRowIndex">初期値を投入する行方向終了インデックス</param>
        /// <param name="startColumnIndex">初期値を投入する列方向開始インデックス</param>
        /// <param name="endColumnIndex">初期値を投入する列方向終了インデックス</param>
        public static void InitializeWith<T>(this T[,] array, T val, int startRowIndex, int endRowIndex, int startColumnIndex, int endColumnIndex)
        {
            OperateArray.InitializeWith<T>(ref array, val, startRowIndex, endRowIndex, startColumnIndex, endColumnIndex);
        }

        /// <summary>
        /// 配列の要素を任意の値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">二次元配列</param>
        /// <param name="val">初期値</param>
        /// <param name="startRowIndex">初期値を投入する行方向開始インデックス</param>
        /// <param name="endRowIndex">初期値を投入する行方向終了インデックス</param>
        public static void InitializeWith<T>(this T[,] array, T val, int startRowIndex, int endRowIndex)
        {
            OperateArray.InitializeWith<T>(ref array, val, startRowIndex, endRowIndex);
        }

        /// <summary>
        /// 配列の要素を任意の値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">二次元配列</param>
        /// <param name="val">初期値</param>
        public static void InitializeWith<T>(this T[,] array, T val)
        {
            OperateArray.InitializeWith<T>(ref array, val);
        }

        /// <summary>
        /// 式の結果を返す静的メソッド
        /// </summary>
        /// <param name="expression">式</param>
        /// <returns>成功時は式の結果、失敗時はNaN</returns>
        public static double Eval(this string expression)
        {
            return GlobalMethodClass.Eval(expression);
        }
    }
}
