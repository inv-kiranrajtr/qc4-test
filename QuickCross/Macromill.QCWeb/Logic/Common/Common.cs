#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Common.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/2/20
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/2
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion

#define AFTER_2ND_PHASE
#undef AFTER_2ND_PHASE
#define ROUNDOFF_TDISTRIBUTION
#undef ROUNDOFF_TDISTRIBUTION

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using Macromill.QCWeb.Tabulation;
using System.Reflection;
using System.Resources;
using Macromill.QCWeb.Exceptions;
using System.Xml;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

/*
using Excel = Microsoft.Office.Interop.Excel;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
*/

namespace Macromill.QCWeb.Common
{
    #region Office関連列挙型
    #region Excel関連列挙型
    /// <summary>
    /// QCWebで使用するグラフの種類を表すExcel.XlChartType列挙型のラッパー
    /// </summary>
    [ComVisible(true)]
    public enum XlChartType
    {
        /// <summary>
        /// 折れ線グラフを表す
        /// </summary>
        xlLine = 4,
        /// <summary>
        /// 円グラフを表す
        /// </summary>
        xlPie = 5,  // Excel.XlChartType.xlPie
        /// <summary>
        /// 縦棒グラフを表す
        /// </summary>
        xlColumnClustered = 51, // Excel.XlChartType.xlColumnClustered
        /// <summary>
        /// 横棒グラフを表す
        /// </summary>
        xlBarClustered = 57,    // Excel.XlChartType.xlBarClustered
        /// <summary>
        /// 縦帯グラフを表す
        /// </summary>
        xlColumnStacked100 = 53,    // Excel.XlChartType.xlColumnStacked100
        /// <summary>
        /// 横帯グラフを表す
        /// </summary>
        xlBarStacked100 = 59,   // Excel.XlChartType.xlBarStacked100
        /// <summary>
        /// 縦棒積上グラフを表す
        /// </summary>
        xlColumnStacked = 52,   // Excel.XlChartType.xlColumnStacked
        /// <summary>
        /// 横棒積上グラフを表す
        /// </summary>
        xlBarStacked = 58,   // Excel.XlChartType.xlBarStacked
        /// <summary>
        /// QCMを表す
        /// </summary>
        QCM = 256,
        /// <summary>
        /// RATを表す
        /// </summary>
        RAT = 512
    }

    /// <summary>
    /// QCWebで使用する用紙サイズを表すExcel.XlPaperSize列挙型のラッパー
    /// </summary>
    [ComVisible(true)]
    public enum XlPaperSize
    {
        /// <summary>
        /// A3を表す
        /// </summary>
        xlPaperA3 = 8,  // Excel.XlPaperSize.xlPaperA3
        /// <summary>
        /// A4を表す
        /// </summary>
        xlPaperA4 = 9,  // Excel.XlPaperSize.xlPaperA4
        /// <summary>
        /// B4を表す
        /// </summary>
        xlPaperB4 = 12  // Excel.XlPaperSize.xlPaperB4
    }

    /// <summary>
    /// QCWebで使用する用紙の向きを表すExcel.XlPageOrientation列挙型のラッパー
    /// </summary>
    [ComVisible(true)]
    public enum XlPageOrientation
    {
        /// <summary>
        /// 縦向きを表す
        /// </summary>
        xlPortrait = 1, // Excel.XlPageOrientation.xlPortrait
        /// <summary>
        /// 横向きを表す
        /// </summary>
        xlLandscape = 2 // Excel.XlPageOrientation.xlLandscape
    }

    /// <summary>
    /// QCWebで使用するExcelのファイルの保存形式を表すExcel.XlFileFormat列挙型のラッパー
    /// </summary>
    [ComVisible(true)]
    public enum XlFileFormat
    {
        /// <summary>
        /// Excel 2007形式を表す
        /// </summary>
        xlOpenXMLWorkbook = 51, // Excel.XlFileFormat.xlOpenXMLWorkbook
        /// <summary>
        /// Excel 2003形式を表す
        /// </summary>
        xlExcel8 = 56   // Excel.XlFileFormat.xlExcel8
    }
    #endregion

    #region PowerPoint関連列挙型
    /// <summary>
    /// QCWebで使用するPowerPointのファイルの保存形式を表すPowerPoint.PpSaveAsFileType列挙型のラッパー
    /// </summary>
    [ComVisible(true)]
    public enum PpSaveAsFileType
    {
        /// <summary>
        /// PowerPoint 2007形式を表す
        /// </summary>
        ppSaveAsOpenXMLPresentation = 24,   // PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation
        /// <summary>
        /// PowerPoint 2003形式を表す
        /// </summary>
        ppSaveAsPresentation = 1    // PowerPoint.PpSaveAsFileType.ppSaveAsPresentation
    }
    #endregion

    #region Office関連列挙型
    /// <summary>
    /// QCWebで使用するグラデーションの種類を表すOffice.MsoGradientStyle列挙型のラッパー
    /// </summary>
    [ComVisible(true)]
    public enum MsoGradientStyle
    {
        /// <summary>
        /// 横 (= 1)
        /// </summary>
        msoGradientHorizontal = 1,  // Office.MsoGradientStyle.msoGradientHorizontal
        /// <summary>
        /// 縦 (= 2)
        /// </summary>
        msoGradientVertical = 2,    // Office.MsoGradientStyle.msoGradientVertical
        /// <summary>
        /// 右上対角線 (= 3)
        /// </summary>
        msoGradientDiagonalUp = 3,  // Office.MsoGradientStyle.msoGradientDiagonalUp
        /// <summary>
        /// 右下対角線 (= 4)
        /// </summary>
        msoGradientDiagonalDown = 4,    // Office.MsoGradientStyle.msoGradientDiagonalDown
        /// <summary>
        /// 角から (= 5)
        /// </summary>
        msoGradientFromCorner = 5,  // Office.MsoGradientStyle.msoGradientFromCorner
        /// <summary>
        /// 中央から (= 7)
        /// </summary>
        msoGradientFromCenter = 7   // Office.MsoGradientStyle.msoGradientFromCenter
    }
    #endregion
    #endregion

    #region AddedIndex構造体
    /// <summary>
    /// データにインデックスを付けた形で管理する構造体
    /// </summary>
    /// <remarks>
    /// 配列の要素の型として使用する構造体で、データとインデックスを保持する<br />
    /// 安定ソートの実装などで使用する
    /// </remarks>
    /// <example>
    /// 次のサンプルは、文字列型配列の内容を降順にソートした結果を出力します
    /// <code lang="C#">
    /// // 文字列型配列
    /// string[] strArray = { "Yamaji", "Kawahito", "Shimazaki" };
    /// // 降順に並べるための比較メソッドのdelegate
    /// System.Comparison&lt;string&gt; comparison = delegate(string x, string y) { return x.CompareTo(y) * -1; };
    /// // 比較メソッドの設定は共通 (staticメンバ)
    /// Macromill.QCWeb.Common.AddedIndex&lt;string&gt;.changeComparison(comparison);
    /// // AddedIndex&lt;string&gt;型配列を定義して、元の配列の各要素とインデックスをペアとして格納
    /// Macromill.QCWeb.Common.AddedIndex&lt;string&gt;[] AddedIndexArray 
    ///     = new Macromill.QCWeb.Common.AddedIndex&lt;string&gt;[strArray.Length];
    /// for (int i = 0; i &lt; strArray.Length; ++i)
    /// {
    ///     AddedIndexArray[i] 
    ///         = new Macromill.QCWeb.Common.AddedIndex&lt;string&gt;(strArray[i], i);
    /// }
    /// // ソートの実行
    /// Array.Sort&lt;Macromill.QCWeb.Common.AddedIndex&lt;string&gt;&gt;(AddedIndexArray);
    /// // 結果の出力
    /// for (int i = 0; i &lt; AddedIndexArray.Length; )
    /// {
    ///     Console.Write(AddedIndexArray[i].index.ToString());     // 元の配列内でのインデックス
    ///     Console.Write("\t");
    ///     Console.WriteLine(AddedIndexArray[i++].data);   // データ
    /// }
    /// </code>
    /// </example>
    /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
    [ComVisible(false)]
    public struct AddedIndex<T> : IComparable where T : IComparable
    {
        /// <summary>
        /// データ値
        /// </summary>
        public T data;
        /// <summary>
        /// インデックス (安定ソート用)
        /// </summary>
        public int index;
        private static Comparison<T> comparison = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">データ</param>
        /// <param name="index">インデックス</param>
        public AddedIndex(T value, int index)
        {
            this.data = value;
            this.index = index;
        }

        /// <summary>
        /// IComparable.CompareToメソッドの実装
        /// </summary>
        /// <param name="other">比較相手のAddedIndex構造体</param>
        /// <returns>
        /// comparisonがnullでないときには、comparisonに指定されたdelegateで参照できるメソッドの戻り値を返す<br />
        /// comparisonがnullのときには、既定の比較結果を返す<br />
        /// また、比較相手と等価の場合(比較結果が0の場合)には、インデックスの既定比較結果(昇順)を返す
        /// </returns>
        public int CompareTo(object other)
        {
            int res = 0;
            AddedIndex<T> Other = (AddedIndex<T>)other;
            if (comparison != null)
            {
                res = comparison(this.data, Other.data);
            }
            else
            {
                res = this.data.CompareTo(Other.data);
            }
            if (res == 0) res = this.index.CompareTo(Other.index);
            return res;
        }

        /// <summary>
        /// 比較メソッドを設定する
        /// </summary>
        /// <param name="newComparison">比較メソッドを参照するdelegateへの参照</param>
        public static void changeComparison(Comparison<T> newComparison)
        {
            comparison = newComparison;
        }
    }
    #endregion

    #region GlobalMethodClassクラス
    /// <summary>
    /// 共有関数的メソッドをまとめた静的クラス
    /// </summary>
    [ComVisible(false), Guid("DD5D4853-D64C-4f35-9DAD-7C2D00E6FA22")]
    public static class GlobalMethodClass
    {
        #region Win32API
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        #endregion

        #region 安定ソート関連
        #region サブルーチン
        /// <summary>
        /// 一次元配列の安定ソート結果のAddedIndex構造体配列を返す
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">一次元配列</param>
        /// <param name="comparison">比較メソッドを参照するdelegateへの参照 (省略可、既定値null)</param>
        /// <param name="index">
        /// ソート対象範囲の開始インデックス (省略可、既定値0)
        /// <note>0未満の値を指定した場合は0とする</note>
        /// </param>
        /// <param name="length">
        /// 並べ替え対象要素数 (省略可、既定値0)
        /// <note>0以下の値を指定した場合は<paramref name="array"/>の要素数とする</note>
        /// </param>
        /// <returns>安定ソート結果のAddedIndex構造体配列</returns>
        private static AddedIndex<T>[] GetStableSortedAddedIndexArray<T>(
                ref T[] array, Comparison<T> comparison = null, int index = 0, int length = 0) where T : IComparable
        {
            if (array == null) return null;
            AddedIndex<T>.changeComparison(comparison);
            AddedIndex<T>[] AddedIndexArray = new AddedIndex<T>[array.Length];
            if (index < 0) index = 0;
            if (length <= 0) length = array.Length;
            for (int i = 0; i < array.Length; ++i)
            {
                AddedIndexArray[i] = new AddedIndex<T>(array[i], i);
            }
            Array.Sort<AddedIndex<T>>(AddedIndexArray, index, length);
            return AddedIndexArray;
        }

        /// <summary>
        /// 一次元配列の安定ソート結果のインデックス配列を返す
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">一次元配列</param>
        /// <param name="comparison">比較メソッドを参照するdelegateへの参照 (省略可、既定値null)</param>
        /// <param name="index">
        /// ソート対象範囲の開始インデックス (省略可、既定値0)
        /// <note>0未満の値を指定した場合は0とする</note>
        /// </param>
        /// <param name="length">
        /// 並べ替え対象要素数 (省略可、既定値0)
        /// <note>0以下の値を指定した場合は<paramref name="array"/>の要素数とする</note>
        /// </param>
        /// <returns>安定ソート結果のインデックス配列</returns>
        private static int[] GetStableSortedIndexArray<T>(
                ref T[] array, Comparison<T> comparison = null, int index = 0, int length = 0) where T : IComparable
        {
            AddedIndex<T>[] AddedIndexArray = GetStableSortedAddedIndexArray<T>(ref array, comparison, index, length);
            if (AddedIndexArray == null) return null;
            int[] res = new int[AddedIndexArray.Length];
            for (int i = 0; i < AddedIndexArray.Length; ++i)
            {
                res[i] = AddedIndexArray[i].index;
            }
            return res;
        }
        #endregion

        #region 一次元配列用

        /// <alias>StableSort000</alias>
        /// <summary>
        /// <para>エイリアス:StableSort000</para>
        /// 一次元配列の安定ソートを行うStableSortオーバーロードメソッド群の根幹メソッド
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
        /// Macromill.QCWeb.Common.GlobalMethodClass.StableSort&lt;string&gt;(ref strArray, comparison);
        /// for (int i = 0; i &lt; strArray.Length; )
        /// {
        ///     Console.WriteLine(strArray[i++]);
        /// }
        /// </code>
        /// </example>
        public static void StableSort<T>(ref T[] array, Comparison<T> comparison, int index = 0, int length = 0) where T : IComparable
        {
            AddedIndex<T>[] AddedIndexArray = GetStableSortedAddedIndexArray<T>(ref array, comparison, index, length);
            if (AddedIndexArray == null) return;
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = AddedIndexArray[i].data;
            }
        }

        /// <alias>StableSort001</alias>
        /// <summary>
        /// <para>エイリアス:StableSort001</para>
        /// 一次元配列の安定ソートを行う<br />
        /// 降順ソートの指定を容易にするオーバーロードで、
        /// <paramref name="isDesc"/>がtrueのときには、降順ソートのための比較メソッドを内部で定義して、
        /// そのdelegateへの参照を引数comparisonに指定して、StableSort000に仲介する
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
        /// Macromill.QCWeb.Common.GlobalMethodClass.StableSort&lt;string&gt;(ref strArray, true);
        /// for (int i = 0; i &lt; strArray.Length; )
        /// {
        ///     Console.WriteLine(strArray[i++]);
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[]@,System.Comparison{``0})">StableSort0001</seealso>
        public static void StableSort<T>(ref T[] array, bool isDesc, int index = 0, int length = 0) where T : IComparable
        {
            Comparison<T> comparison = null;
            if (isDesc) comparison = (x, y) => x.CompareTo(y) * -1;
            StableSort<T>(ref array, comparison, index, length);
        }

        /// <aleas>StableSort002</aleas>
        /// <summary>
        /// <para>エイリアス:StableSort002</para>
        /// 一次元配列の安定ソートを行う<br />
        /// ソートの定義(比較メソッド)の指定を省略したオーバーロードで、引数comparisonにnullを指定して、StableSort000に仲介する
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
        /// Macromill.QCWeb.Common.GlobalMethodClass.StableSort&lt;string&gt;(ref strArray);
        /// for (int i = 0; i &lt; strArray.Length; )
        /// {
        ///     Console.WriteLine(strArray[i++]);
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[]@,System.Comparison{``0})">StableSort0001</seealso>
        public static void StableSort<T>(ref T[] array, int index = 0, int length = 0) where T : IComparable
        {
            StableSort<T>(ref array, null, index, length);
        }
        #endregion

        #region 二次元配列用
        /// <alias>StableSort100</alias>
        /// <summary>
        /// <para>エイリアス:StableSort100</para>
        /// 二次元配列の安定ソートを行うStableSortオーバーロードメソッド群の根幹メソッド
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="keyColumnIndex">キーとするカラムのインデックス</param>
        /// <param name="comparison">比較メソッドを参照するdelegateへの参照</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        public static void StableSort<T>(ref T[,] array, int keyColumnIndex, Comparison<T> comparison, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            if (array == null) return;
            if (IgnoreTopRowsCount < 0) IgnoreTopRowsCount = 0;
            if (IgnoreTopRowsCount >= array.GetLength(0) - 1) return;
            if (keyColumnIndex < array.GetLowerBound(1) || keyColumnIndex > array.GetUpperBound(1)) return;
            AddedIndex<T>.changeComparison(comparison);
            AddedIndex<T>[] AddedIndexArray = new AddedIndex<T>[array.GetLength(0) - IgnoreTopRowsCount];
            for (int i = IgnoreTopRowsCount; i < array.GetLength(0); ++i)
            {
                AddedIndexArray[i - IgnoreTopRowsCount] = new AddedIndex<T>(array[i, keyColumnIndex], i);
            }
            Array.Sort<AddedIndex<T>>(AddedIndexArray);
            T[,] clone = new T[array.GetLength(0), array.GetLength(1)];
            Array.Copy(array, clone, array.Length);
            for (int i = IgnoreTopRowsCount; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    array[i, j] = clone[AddedIndexArray[i - IgnoreTopRowsCount].index, j];
                }
            }
        }

        /// <alias>StableSort101</alias>
        /// <summary>
        /// <para>エイリアス:StableSort101</para>
        /// 二次元配列の安定ソートを行う<br />
        /// 降順ソートの指定を容易にするオーバーロードで、
        /// <paramref name="isDesc"/>がtrueのときには、降順ソートのための比較メソッドを内部で定義して、
        /// そのdelegateへの参照を引数comparisonに指定して、StableSort100に仲介する
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="keyColumnIndex">キーとするカラムのインデックス</param>
        /// <param name="isDesc">降順にソートするときtrue/昇順ではfalse</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32,System.Comparison{``0})">StableSort100</seealso>
        public static void StableSort<T>(ref T[,] array, int keyColumnIndex, bool isDesc, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            Comparison<T> comparison = null;
            if (isDesc) comparison = (x, y) => x.CompareTo(y) * -1;
            StableSort<T>(ref array, keyColumnIndex, comparison, IgnoreTopRowsCount);
        }

        /// <alias>StableSort102</alias>
        /// <summary>
        /// <para>エイリアス:StableSort102</para>
        /// 二次元配列の安定ソートを行う<br />
        /// ソートの定義(比較メソッド)の指定を省略したオーバーロードで、引数comparisonにnullを指定して、StableSort100に仲介し、昇順(既定のソート順)でソートする
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="keyColumnIndex">キーとするカラムのインデックス</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32,System.Comparison{``0})">StableSort100</seealso>
        public static void StableSort<T>(ref T[,] array, int keyColumnIndex, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            StableSort<T>(ref array, keyColumnIndex, null, IgnoreTopRowsCount);
        }

        /// <alias>StableSort103</alias>
        /// <summary>
        /// <para>エイリアス:StableSort103</para>
        /// 二次元配列の安定ソートを行う<br />
        /// キーカラムの指定を省略したオーバーロードで、引数keyColumnIndexに列方向のベースインデックスを指定(左端のカラムをキーとして)して、StableSort100に仲介する
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="comparison">比較メソッドを参照するdelegateへの参照</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32,System.Comparison{``0})">StableSort100</seealso>
        public static void StableSort<T>(ref T[,] array, Comparison<T> comparison, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            if (array == null) return;
            StableSort<T>(ref array, array.GetLowerBound(1), comparison, IgnoreTopRowsCount);
        }

        /// <alias>StableSort104</alias>
        /// <summary>
        /// <para>エイリアス:StableSort104</para>
        /// 二次元配列の安定ソートを行う<br />
        /// キーカラムの指定を省略し、降順ソートの指定を容易にするオーバーロードで、
        /// 引数keyColumnIndexに列方向のベースインデックスを指定(左端のカラムをキーとして)して、StableSort101に仲介する
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="isDesc">降順にソートするときtrue/昇順ではfalse</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32,System.Boolean)">StableSort101</seealso>
        public static void StableSort<T>(ref T[,] array, bool isDesc, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            if (array == null) return;
            StableSort<T>(ref array, array.GetLowerBound(1), isDesc, IgnoreTopRowsCount);
        }

        /// <alias>StableSort105</alias>
        /// <summary>
        /// <para>エイリアス:StableSort105</para>
        /// 二次元配列の安定ソートを行う<br />
        /// キーカラムの指定を省略し、ソートの定義(比較メソッド)の指定を省略したオーバーロードで、
        /// 引数keyColumnIndexに列方向のベースインデックスを指定(左端のカラムをキーとして)して、StableSort102に仲介する
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32)">StableSort102</seealso>
        public static void StableSort<T>(ref T[,] array, int IgnoreTopRowsCount = 0) where T : IComparable
        {
            StableSort<T>(ref array, array.GetLowerBound(1), IgnoreTopRowsCount);
        }

        /// <alias>StableSort111</alias>
        /// <summary>
        /// <para>エイリアス:StableSort111</para>
        /// 複数のキーを使って二次元配列の安定ソートを行う<br />
        /// ソートの定義が一定で、複数のキーを使ってソートを行う場合に適切なオーバーロード<br />
        /// 内部的にはStableSort100を順次(逆順)実行する
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="comparison">すべてのキーにおけるソートで適用する比較メソッドを参照するdelegateへの参照</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <param name="keyColumnIndexes">キーとするカラムのインデックス (可変長)</param>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32,System.Comparison{``0})">StableSort100</seealso>
        public static void StableSort<T>(ref T[,] array, Comparison<T> comparison, int IgnoreTopRowsCount = 0, params int[] keyColumnIndexes) where T : IComparable
        {
            for (int i = keyColumnIndexes.GetUpperBound(0); i >= 0; --i)
            {
                StableSort<T>(ref array, keyColumnIndexes[i], comparison, IgnoreTopRowsCount);
            }
        }

        /// <alias>StableSort112</alias>
        /// <summary>
        /// <para>エイリアス:StableSort112</para>
        /// 複数のキーを使って二次元配列の安定ソートを行う<br />
        /// ソートの定義が昇順または降順で一定で、複数のキーを使ってソートを行う場合に適切なオーバーロード<br />
        /// 内部的にはStableSort101を順次(逆順)実行する
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="isDesc">すべてのキーにおいて降順にソートするときtrue/すべてのキーにおいて昇順にソートするときfalse</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <param name="keyColumnIndexes">キーとするカラムのインデックス (可変長)</param>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32,System.Boolean)">StableSort101</seealso>
        public static void StableSort<T>(ref T[,] array, bool isDesc, int IgnoreTopRowsCount = 0, params int[] keyColumnIndexes) where T : IComparable
        {
            for (int i = keyColumnIndexes.GetUpperBound(0); i >= 0; --i)
            {
                StableSort<T>(ref array, keyColumnIndexes[i], isDesc, IgnoreTopRowsCount);
            }
        }

        /// <alias>StableSort113</alias>
        /// <summary>
        /// <para>エイリアス:StableSort113</para>
        /// 複数のキーを使って二次元配列の安定ソートを行う<br />
        /// ソートの定義が昇順で一定で、複数のキーを使ってソートを行う場合に適切なオーバーロード<br />
        /// 内部的にはStableSort102を順次(逆順)実行する
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="array">ソートする二次元配列</param>
        /// <param name="IgnoreTopRowsCount">並べ替えの対象外とする冒頭部の行数 (省略可:既定値0)</param>
        /// <param name="keyColumnIndexes">キーとするカラムのインデックス (可変長)</param>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32)">StableSort102</seealso>
        public static void StableSort<T>(ref T[,] array, int IgnoreTopRowsCount = 0, params int[] keyColumnIndexes) where T : IComparable
        {
            for (int i = keyColumnIndexes.GetUpperBound(0); i >= 0; --i)
            {
                StableSort<T>(ref array, keyColumnIndexes[i], IgnoreTopRowsCount);
            }
        }

        /// <alias>StableSort114</alias>
        /// <summary>
        /// <para>エイリアス:StableSort114</para>
        /// 複数のキーを使って二次元配列の安定ソートを行う<br />
        /// 汎用性の高いオーバーロードで、各キーにおけるソートの定義が同一でない場合に適切なオーバーロード<br />
        /// 内部的には、引数の指定内容に適切なオーバーロードメソッドを順次(逆順)実行する
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
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32,System.Comparison{``0})">StableSort100</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.StableSort``1(``0[0:,0:]@,System.Int32,System.Boolean)">StableSort101</seealso>
        public static void StableSort<T>(ref T[,] array, int IgnoreTopRowsCount = 0, params object[] keys) where T : IComparable
        {
            // 引数のチェック
            int e = (keys.Length / 2 - 1) * 2;
            int keyColumnIndex;
            bool isDesc;
            Comparison<T> comparison;
            for (int i = 0; i <= e; i += 2)
            {
                try
                {
                    keyColumnIndex = (int)keys[i];
                }
                catch
                {
                    e = i - 2;
                    break;
                }
                try
                {
                    isDesc = (bool)keys[i + 1];
                }
                catch
                {
                    try
                    {
                        comparison = (Comparison<T>)keys[i + 1];
                    }
                    catch
                    {
                        e = i - 2;
                        break;
                    }
                }
            }
            // ソート
            if (e < 0)
            {
                StableSort<T>(ref array);
            }
            else
            {
                for (int i = e; i >= 0; i -= 2)
                {
                    keyColumnIndex = (int)keys[i];
                    try
                    {
                        isDesc = (bool)keys[i + 1];
                        StableSort<T>(ref array, keyColumnIndex, isDesc, IgnoreTopRowsCount);
                    }
                    catch
                    {
                        comparison = (Comparison<T>)keys[i + 1];
                        StableSort<T>(ref array, keyColumnIndex, comparison, IgnoreTopRowsCount);
                    }
                }
            }
        }

        /// <summary>
        /// QCでのデータの並び替えルールに従う、文字列間の比較メソッド
        /// </summary>
        /// <param name="ConvertToNumeric">数値化できるものは数値化して比較する場合true (省略可:既定値false)</param>
        /// <param name="isDesc">降順に並び替えるときtrue (省略可:既定値false)</param>
        /// <param name="isFA">FAのデータの比較の場合true (省略可:既定値false)</param>
        /// <param name="NADescription">無回答データを表す文字列 (省略可:既定値"")</param>
        /// <param name="IVDescription">非該当データを表す文字列 (省略可:既定値*</param>
        /// <returns></returns>
        public static Comparison<string> GetQCComparison(bool ConvertToNumeric = false, bool isDesc = false, bool isFA = false, string NADescription = "", string IVDescription = "*")
        {
            if (NADescription == null) NADescription = "";
            if (IVDescription == null) IVDescription = "";
            return (x, y) =>
            {
                if (x == null) x = "";
                if (y == null) y = "";
                int res = 0;
                int xType = 0;
                if (x.Equals(NADescription))
                {
                    xType = 1;
                }
                else if (x.Equals(IVDescription) && !isFA)
                {
                    xType = 2;
                }
                int yType = 0;
                if (y.Equals(NADescription))
                {
                    yType = 1;
                }
                else if (y.Equals(IVDescription) && !isFA)
                {
                    yType = 2;
                }
                if (xType != 0 || yType != 0)
                {
                    // return xType.CompareTo(yType) * (isDesc ? -1 : 1);
                    res = xType.CompareTo(yType);
                }
                else if (ConvertToNumeric)
                {
                    double dblX = 0.0;
                    double dblY = 0.0;
                    if (double.TryParse(x, out dblX))
                    {
                        if (double.TryParse(y, out dblY))
                        {
                            // return dblX.CompareTo(dblY) * (isDesc ? -1 : 1);
                            res = dblX.CompareTo(dblY);
                        }
                        else
                        {
                            // return isDesc ? 1 : -1;
                            res = -1;
                        }
                    }
                    else
                    {
                        if (double.TryParse(y, out dblY))
                        {
                            // return isDesc ? -1 : 1;
                            res = 1;
                        }
                        else
                        {
                            // return x.CompareTo(y) * (isDesc ? -1 : 1);
                            // res = Strings.StrComp(x, y, CompareMethod.Text);
                            res = string.Compare(x, y, CultureInfo.CurrentCulture
                                    , CompareOptions.IgnoreCase | CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType);
                        }
                    }
                }
                else
                {
                    // return x.CompareTo(y) * (isDesc ? -1 : 1);
                    // res = Strings.StrComp(x, y, CompareMethod.Text);
                    res = string.Compare(x, y, CultureInfo.CurrentCulture
                            , CompareOptions.IgnoreCase | CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType);
                }
                return res * (isDesc ? -1 : 1);
            };
        }
        #endregion

        #region List用
        /// <alias>StableSort200</alias>
        /// <summary>
        /// <para>エイリアス:StableSort200</para>
        /// Listコレクションの安定ソートを行うStableSortオーバーロードメソッド群の根幹メソッド
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="list">ソートするListクラスのインスタンスへの参照</param>
        /// <param name="comparison">比較メソッドを参照するdelegateへの参照</param>
        public static void StableSort<T>(ref System.Collections.Generic.List<T> list, Comparison<T> comparison) where T : IComparable
        {
            if (list == null) return;
            AddedIndex<T>.changeComparison(comparison);
            AddedIndex<T>[] AddedIndexArray = new AddedIndex<T>[list.Count];
            for (int i = 0; i < list.Count; ++i)
            {
                AddedIndexArray[i] = new AddedIndex<T>(list[i], i);
            }
            Array.Sort<AddedIndex<T>>(AddedIndexArray);
            for (int i = 0; i < list.Count; ++i)
            {
                list[i] = AddedIndexArray[i].data;
            }
        }

        /// <alias>StableSort201</alias>
        /// <summary>
        /// <para>エイリアス:StableSort201</para>
        /// Listコレクションの安定ソートを行う<br />
        /// 降順ソートの指定を容易にするオーバーロードで、
        /// <paramref name="isDesc"/>がtrueのときには、降順ソートのための比較メソッドを内部で定義して、
        /// そのdelegateへの参照を引数comparisonに指定して、StableSort200に仲介する
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="list">ソートするListクラスのインスタンスへの参照</param>
        /// <param name="isDesc">降順にソートするときtrue/昇順ではfalse</param>
        public static void StableSort<T>(ref System.Collections.Generic.List<T> list, bool isDesc) where T : IComparable
        {
            Comparison<T> comparison = null;
            if (isDesc) comparison = (x, y) => x.CompareTo(y) * -1;
            StableSort<T>(ref list, comparison);
        }

        /// <alias>StableSort202</alias>
        /// <summary>
        /// <para>エイリアス:StableSort202</para>
        /// Listコレクションの安定ソートを行う<br />
        /// ソートの定義(比較メソッド)の指定を省略したオーバーロードで、引数comparisonにnullを指定して、StableSort200に仲介し、昇順(既定のソート順)でソートする
        /// </summary>
        /// <typeparam name="T">IComparableインターフェイスを実装した型</typeparam>
        /// <param name="list">ソートするListクラスのインスタンスへの参照</param>
        public static void StableSort<T>(ref System.Collections.Generic.List<T> list) where T : IComparable
        {
            StableSort<T>(ref list, null);
        }
        #endregion

        #region 集計結果並べ替え用
        /// <summary>
        /// 選択肢の並び方向を表す列挙型
        /// </summary>
        [Flags]
        public enum SectorIncreaseDirection
        {
            /// <summary>
            /// 左から右方向を表す (=0)
            /// </summary>
            LeftToRight,
            /// <summary>
            /// 上から下方向を表す (=1)
            /// </summary>
            TopToBottom
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
        public static void StableSort(ref DataWithMarking[,] dataArray, int sectorStartIndex, int sectorEndIndex, int totalIndex
                , int[] sortSectorIndexes, SectorIncreaseDirection sectorIncreaseDirection = SectorIncreaseDirection.LeftToRight
                , bool setNewOrder = false, int orderColumnIndex = 0, int descColumnIndex = 1, int dataColumnIndex = 3)
        {
            if (dataArray == null || sortSectorIndexes == null) return;
            if (sectorStartIndex >= sectorEndIndex) return;
            if (sectorIncreaseDirection == SectorIncreaseDirection.TopToBottom)
            {
                if (sectorStartIndex < dataArray.GetLowerBound(0) || sectorEndIndex > dataArray.GetUpperBound(0)) return;
                if (totalIndex < dataArray.GetLowerBound(1) || totalIndex > dataArray.GetUpperBound(1)) return;
            }
            else
            {
                if (sectorStartIndex < dataArray.GetLowerBound(1) || sectorEndIndex > dataArray.GetUpperBound(1)) return;
                if (totalIndex < dataArray.GetLowerBound(0) || totalIndex > dataArray.GetUpperBound(0)) return;
            }
            int secCnt = sectorEndIndex - sectorStartIndex + 1;
            List<int> sortSectors = new List<int>();    // 配列内インデックス
            for (int i = 0; i < sortSectorIndexes.Length; ++i)
            {
                int s = sortSectorIndexes[i];
                if (s >= 0 && s < secCnt)
                {
                    s += sectorStartIndex;
                    if (!sortSectors.Contains(s)) sortSectors.Add(s);
                }
            }
            if (sortSectors.Count <= 1) return;
            sortSectors.Sort();
            List<int> unsortSectors = new List<int>();  // 0ベース
            for (int s = sectorStartIndex; s <= sectorEndIndex; ++s)
            {
                if (!sortSectors.Contains(s)) unsortSectors.Add(s - sectorStartIndex);
            }
            DataWithMarking[] tmpArray = new DataWithMarking[sortSectors.Count];
            if (sectorIncreaseDirection == SectorIncreaseDirection.TopToBottom)
            {
                for (int i = 0; i < tmpArray.Length; ++i)
                {
                    tmpArray[i] = dataArray[sortSectors[i], totalIndex];
                }
            }
            else
            {
                for (int i = 0; i < tmpArray.Length; ++i)
                {
                    tmpArray[i] = dataArray[totalIndex, sortSectors[i]];
                }
            }
            int[] sortedIndexes = GetStableSortedIndexArray<DataWithMarking>(ref tmpArray);
            if (sectorIncreaseDirection == SectorIncreaseDirection.TopToBottom)
            {
                DataWithMarking[,] tmpOrgArray = new DataWithMarking[secCnt, dataArray.GetLength(1)];
                for (int i = 0, s = sectorStartIndex; i < secCnt; ++i, ++s)
                {
                    for (int j = 0; j < dataArray.GetLength(1); ++j)
                    {
                        tmpOrgArray[i, j] = dataArray[s, j];
                    }
                }
                for (int i = 0, s = sectorStartIndex; i < sortedIndexes.Length; ++i, ++s)
                {
                    for (int j = 0; j < dataArray.GetLength(1); ++j)
                    {
                        dataArray[s, j] = tmpOrgArray[sortedIndexes[i], j];
                    }
                }
                for (int i = 0, s = sectorStartIndex + sortedIndexes.Length; i < unsortSectors.Count; ++i, ++s)
                {
                    for (int j = 0; j < dataArray.GetLength(1); ++j)
                    {
                        dataArray[s, j] = tmpOrgArray[unsortSectors[i], j];
                    }
                }
                if (!setNewOrder) return;
                List<int> newSectorNumbers = new List<int>();
                for (int i = 0; i < sortedIndexes.Length; ++i)
                {
                    newSectorNumbers.Add(sortedIndexes[i] + 1);
                }
                for (int i = 0; i < unsortSectors.Count; ++i)
                {
                    newSectorNumbers.Add(unsortSectors[i] + 1);
                }
                for (int i = 0, order = 1, s = sectorStartIndex; s <= sectorEndIndex; ++i, ++order, ++s)
                {
                    dataArray[s, orderColumnIndex] = new DataWithMarking(newSectorNumbers[i].ToString());
                    dataArray[s, descColumnIndex].SetSignificanceCharacters(Strings.LCase(SignificanceTestLetters.Character(order)));
                    dataArray[s, dataColumnIndex].SetNewOrder(newSectorNumbers);
                }
            }
            else
            {
                DataWithMarking[,] tmpOrgArray = new DataWithMarking[dataArray.GetLength(0), secCnt];
                for (int i = 0; i < dataArray.GetLength(0); ++i)
                {
                    for (int j = 0, s = sectorStartIndex; j < secCnt; ++j, ++s)
                    {
                        tmpOrgArray[i, j] = dataArray[i, s];
                    }
                }
                for (int i = 0; i < dataArray.GetLength(0); ++i)
                {
                    for (int j = 0, s = sectorStartIndex; j < sortedIndexes.Length; ++j, ++s)
                    {
                        dataArray[i, s] = tmpOrgArray[i, sortedIndexes[j]];
                    }
                    for (int j = 0, s = sectorStartIndex + sortedIndexes.Length; j < unsortSectors.Count; ++j, ++s)
                    {
                        dataArray[i, s] = tmpOrgArray[i, unsortSectors[j]];
                    }
                }
            }
        }
        #endregion
        #endregion

        #region ハッシュ関連
        #region ランダムなsalt文字列を生成するメソッド群
        /// <alias>PrivateGetRandomSalt</alias>
        /// <summary>
        /// <para>エイリアス:PrivateGetRandomSalt</para>
        /// ランダムなsalt文字列を生成して返す<br />
        /// 外部には非公開 (内部の根幹ロジック)
        /// </summary>
        /// <param name="length">saltの長さ</param>
        /// <param name="rnd">乱数生成を行うRandomクラスのインスタンスへの参照</param>
        /// <returns>生成したsalt文字列</returns>
        private static string getRandomSalt(int length, Random rnd)
        {
            const string USE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            if (rnd == null || length <= 0) return null;
            char[] res = new char[length];
            for (int i = 0; i < length; ++i)
            {
                int r = rnd.Next(i == 0 ? 52 : length);  // 1文字目はアルファベット
                res[i] = Char.Parse(USE_CHARACTERS.Substring(r, 1));
            }
            return new string(res);
        }

        /// <alias>getRandomSalt01</alias>
        /// <summary>
        /// <para>エイリアス:getRandomSalt01</para>
        /// ランダムなsalt文字列を生成して返す<br />
        /// 与えられたSeed値を使ってRandomクラスのインスタンスを生成して内部ロジックに仲介
        /// </summary>
        /// <param name="length">saltの長さ</param>
        /// <param name="seed">乱数生成アルゴリズム(線形合同法)のSeed値</param>
        /// <returns>生成したsalt文字列</returns>
        public static string getRandomSalt(int length, int seed)
        {
            return getRandomSalt(length, new Random(seed));
        }

        /// <alias>getRandomSalt02</alias>
        /// <summary>
        /// <para>エイリアス:getRandomSalt02</para>
        /// ランダムなsalt文字列を生成して返す<br />
        /// システム日時から求められるSeed値を使ってRandomクラスのインスタンスを生成して内部ロジックに仲介
        /// </summary>
        /// <param name="length">saltの長さ</param>
        /// <returns>生成したsalt文字列</returns>
        public static string getRandomSalt(int length)
        {
            return getRandomSalt(length, new Random());
        }

        /// <alias>getRandomSalt03</alias>
        /// <summary>
        /// <para>エイリアス:getRandomSalt03</para>
        /// ランダムなsalt文字列を生成して返す<br />
        /// 引数lengthに12を指定してgetRandomSalt02に仲介
        /// </summary>
        /// <returns>生成したsalt文字列</returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getRandomSalt(System.Int32)">getRandomSalt02</seealso>
        public static string getRandomSalt()
        {
            return getRandomSalt(12);
        }
        #endregion

        /// <summary>
        /// サポートするハッシュタイプを定義する列挙型
        /// </summary>
        [ComVisible(false)]
        public enum HashType : int
        {
            /// <summary>
            /// MD5ハッシュアルゴリズムの使用を表す (= 0)
            /// </summary>
            MD5,
            /// <summary>
            /// SHA1ハッシュアルゴリズムの使用を表す (= 1)
            /// </summary>
            SHA1
        }

        #region 文字列データのハッシュ値を取得するメソッド群
        private static string getHash<T1, T2>(string data, string salt, int stretchingcount, System.Text.Encoding encode)
            where T1 : System.Security.Cryptography.HashAlgorithm, new() where T2 : System.Security.Cryptography.HashAlgorithm, new()
        {
            if (data == null) data = string.Empty;
            if (salt == null) salt = string.Empty;
            if (stretchingcount < 1) stretchingcount = 1;
            if (encode == null) encode = System.Text.Encoding.UTF8;
            int m = salt.Length / 2;
            string tmp = salt.Substring(0, m) + data + salt.Substring(m);
            byte[] buf = encode.GetBytes(tmp);
            T1 alg1 = new T1();
            T2 alg2 = null;
            if (stretchingcount > 1) alg2 = new T2();
            for (int i = 0; i < stretchingcount; ++i)
            {
                buf = alg1.ComputeHash(buf);
                if (++i < stretchingcount) buf = alg2.ComputeHash(buf);
            }
            return BitConverter.ToString(buf).Replace("-", "");
        }

        private static string getHash<T>(string data, string salt, int stretchingcount = 1, System.Text.Encoding encode = null) where T : System.Security.Cryptography.HashAlgorithm, new()
        {
            return getHash<T, T>(data, salt, stretchingcount, encode);
        }

        /// <alias>getHash00</alias>
        /// <summary>
        /// <para>エイリアス:getHash00</para>
        /// 文字列データのハッシュ値を返すgetHashオーバーロードメソッド群の根幹ロジック<br />
        /// 与えられた文字列データの左端にsaltの前半部分を、右端にsaltの後半部分を付けた文字列データのハッシュ値を返す
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="salt">salt文字列</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalMethodClass.HashType">HashType列挙型</seealso>
        public static string getHash(string data, string salt, HashType hashType)
        {
            if (hashType == HashType.SHA1) return getHash<System.Security.Cryptography.SHA1CryptoServiceProvider>(data, salt);
            return getHash<System.Security.Cryptography.MD5CryptoServiceProvider>(data, salt);
        }

        /// <alias>getHash01</alias>
        /// <summary>
        /// <para>エイリアス:getHash01</para>
        /// 文字列データのハッシュ値を返す<br />
        /// 引数hashTypeにHashType.MD5を指定してgetHash00に仲介
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="salt">salt文字列</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="F:Macromill.QCWeb.Common.GlobalMethodClass.HashType.MD5">HashType.MD5</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String,System.String,Macromill.QCWeb.Common.GlobalMethodClass.HashType)">getHash00</seealso>
        public static string getHash(string data, string salt)
        {
            return getHash(data, salt, HashType.MD5);
        }

        /// <alias>getHash02</alias>
        /// <summary>
        /// <para>エイリアス:getHash02</para>
        /// 文字列データのハッシュ値を返す<br />
        /// 引数saltにランダムに生成した文字列を指定してgetHash00に仲介
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="randomSaltLength">生成するsalt文字列の長さ</param>
        /// <param name="randomSaltSeed">salt文字列の生成に使用する乱数発生アルゴリズムのSeed値</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String,System.String,Macromill.QCWeb.Common.GlobalMethodClass.HashType)">getHash00</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalMethodClass.HashType">HashType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getRandomSalt(System.Int32,System.Int32)">getRandomSalt01</seealso>
        public static string getHash(string data, int randomSaltLength, int randomSaltSeed, HashType hashType)
        {
            return getHash(data, getRandomSalt(randomSaltLength, randomSaltSeed), hashType);
        }

        /// <alias>getHash03</alias>
        /// <summary>
        /// <para>エイリアス:getHash03</para>
        /// 文字列データのハッシュ値を返す<br />
        /// 引数saltにランダムに生成した文字列を指定してgetHash01に仲介
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="randomSaltLength">生成するsalt文字列の長さ</param>
        /// <param name="randomSaltSeed">salt文字列の生成に使用する乱数発生アルゴリズムのSeed値</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String,System.String)">getHash01</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getRandomSalt(System.Int32,System.Int32)">getRandomSalt01</seealso>
        public static string getHash(string data, int randomSaltLength, int randomSaltSeed)
        {
            return getHash(data, getRandomSalt(randomSaltLength, randomSaltSeed));
        }

        /// <alias>getHash04</alias>
        /// <summary>
        /// <para>エイリアス:getHash04</para>
        /// 文字列データのハッシュ値を返す<br />
        /// 引数saltにランダムに生成した文字列を指定してgetHash00に仲介
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="randomSaltLength">生成するsalt文字列の長さ</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String,System.String,Macromill.QCWeb.Common.GlobalMethodClass.HashType)">getHash00</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalMethodClass.HashType">HashType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getRandomSalt(System.Int32)">getRandomSalt02</seealso>
        public static string getHash(string data, int randomSaltLength, HashType hashType)
        {
            return getHash(data, getRandomSalt(randomSaltLength), hashType);
        }

        /// <alias>getHash05</alias>
        /// <summary>
        /// <para>エイリアス:getHash05</para>
        /// 文字列データのハッシュ値を返す<br />
        /// 引数saltにランダムに生成した文字列を指定してgetHash01に仲介
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="randomSaltLength">生成するsalt文字列の長さ</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String,System.String)">getHash01</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getRandomSalt(System.Int32)">getRandomSalt02</seealso>
        public static string getHash(string data, int randomSaltLength)
        {
            return getHash(data, getRandomSalt(randomSaltLength));
        }

        /// <alias>getHash06</alias>
        /// <summary>
        /// <para>エイリアス:getHash06</para>
        /// 文字列データのハッシュ値を返す<br />
        /// 引数saltに空文字列を指定してgetHash00に仲介
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String,System.String,Macromill.QCWeb.Common.GlobalMethodClass.HashType)">getHash00</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalMethodClass.HashType">HashType列挙型</seealso>
        public static string getHash(string data, HashType hashType)
        {
            return getHash(data, "", hashType);
        }

        /// <alias>getHash07</alias>
        /// <summary>
        /// <para>エイリアス:getHash07</para>
        /// 文字列データのハッシュ値を返す<br />
        /// 引数saltに空文字列を指定してgetHash01に仲介
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String,System.String)">getHash01</seealso>
        public static string getHash(string data)
        {
            return getHash(data, "");
        }

        /// <alias>getHash08</alias>
        /// <summary>
        /// <para>エイリアス:getHash08</para>
        /// 文字列データのハッシュ値を返す<br />
        /// <paramref name="addRandomSalt"/>がtrueのときは、引数saltにランダムに生成した文字列を指定してgetHash00に仲介
        /// <paramref name="addRandomSalt"/>がfalseのときは、getHash06に仲介
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="addRandomSalt">ランダムに生成したsalt文字列を付ける場合true/付けない場合false</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String,System.String,Macromill.QCWeb.Common.GlobalMethodClass.HashType)">getHash00</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String,Macromill.QCWeb.Common.GlobalMethodClass.HashType)">getHash06</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalMethodClass.HashType">HashType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getRandomSalt">getRandomSalt03</seealso>
        public static string getHash(string data, bool addRandomSalt, HashType hashType)
        {
            if (addRandomSalt)
            {
                return getHash(data, getRandomSalt(), hashType);
            }
            {
                return getHash(data, hashType);
            }
        }

        /// <alias>getHash09</alias>
        /// <summary>
        /// <para>エイリアス:getHash09</para>
        /// 文字列データのハッシュ値を返す<br />
        /// <paramref name="addRandomSalt"/>がtrueのときは、引数saltにランダムに生成した文字列を指定してgetHash01に仲介
        /// <paramref name="addRandomSalt"/>がfalseのときは、getHash07に仲介
        /// </summary>
        /// <param name="data">文字列データ</param>
        /// <param name="addRandomSalt">ランダムに生成したsalt文字列を付ける場合true/付けない場合false</param>
        /// <returns>ハッシュ値</returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String,System.String)">getHash01</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getHash(System.String)">getHash07</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getRandomSalt">getRandomSalt03</seealso>
        public static string getHash(string data, bool addRandomSalt)
        {
            if (addRandomSalt)
            {
                return getHash(data, getRandomSalt());
            }
            {
                return getHash(data);
            }
        }
        #endregion

        #region ファイルハッシュ(ファイルのバイナリデータからなるバイト配列のハッシュ値)を取得するメソッド群
        /// <alias>getFileHash00</alias>
        /// <summary>
        /// <para>エイリアス:getFileHash00</para>
        /// ファイルハッシュを返すgetFileHashオーバーロードメソッド群の根幹ロジック
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <param name="hashType">ハッシュタイプを表すHashType列挙型の値</param>
        /// <returns>ファイルハッシュ</returns>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalMethodClass.HashType">HashType列挙型</seealso>
        public static string getFileHash(string path, HashType hashType)
        {
            if (String.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path)) return null;
            if (hashType != HashType.SHA1) hashType = HashType.MD5;
            System.IO.FileStream fs = null;
            string res = null;
            try
            {
                fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                using (fs)
                {
                    byte[] buf = null;
                    if (hashType == HashType.MD5)
                    {
                        System.Security.Cryptography.MD5CryptoServiceProvider
                            MD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                        buf = MD5.ComputeHash(fs);
                    }
                    else
                    {
                        System.Security.Cryptography.SHA1CryptoServiceProvider
                            SHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                        buf = SHA1.ComputeHash(fs);
                    }
                    fs.Close();
                    res = BitConverter.ToString(buf).Replace("-", "");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
            }
            return res;
        }

        /// <alias>getFileHash01</alias>
        /// <summary>
        /// ファイルハッシュを返す<br />
        /// 引数hashTypeにHashType.MD5を指定してgetFileHash00に仲介
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <returns>ファイルハッシュ</returns>
        /// <seealso cref="F:Macromill.QCWeb.Common.GlobalMethodClass.HashType.MD5">HashType.MD5</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Common.GlobalMethodClass.getFileHash(System.String,Macromill.QCWeb.Common.GlobalMethodClass.HashType)">getFileHash00</seealso>
        public static string getFileHash(string path)
        {
            return getFileHash(path, HashType.MD5);
        }
        #endregion
        #endregion

        #region I/O関連
        /// <summary>
        /// ディレクトリが存在しない場合には生成して、その存在を保証する
        /// <note>指定したファイルが存在する場合、パスに使用できない文字が含まれる場合などにはエラーをスローする</note>
        /// </summary>
        /// <param name="dirpath"></param>
        public static void GuaranteeDirectoryExist(string dirpath)
        {
            Message mainErrorMessage = new Message(Common.Constants.CommonMessageIndex.CreateDirectoryWithCauseFatalMessageIndex);
            if (System.IO.Directory.Exists(dirpath)) return;
            if (System.IO.File.Exists(dirpath))
            {
                throw new QCWebException(mainErrorMessage
                        , GlobalsCommonConstant.LogLevel.FATAL
                        , dirpath
                        , GetResource.GetLogMessage(Common.Constants.EXIST_SAME_NAME_FILE_MESSAGE_ID, dirpath));
            }
            try
            {
                System.IO.Directory.CreateDirectory(dirpath);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                throw;
            }
        }
        #endregion

        #region ポイント⇔ピクセル換算
        static float dpiX = 0F;
        private static void getDpiX()
        {
            if (dpiX != 0F) return;
            IntPtr hDc = IntPtr.Zero;
            hDc = GetDC(IntPtr.Zero);
            if (hDc == IntPtr.Zero) return; // デバイスコンテキストハンドル取得失敗時
            Graphics g = Graphics.FromHdc(hDc);
            using (g)
            {
                dpiX = g.DpiX;
            }
            ReleaseDC(IntPtr.Zero, hDc);
            if (dpiX < 0F) dpiX = 0F;
        }

        /// <summary>
        /// ピクセル値からポイント値を求める静的メソッド
        /// </summary>
        /// <param name="pixel">ピクセル値</param>
        /// <returns>ポイント値</returns>
        public static float PixelToPoint(int pixel)
        {
            getDpiX();
            if (dpiX == 0F) return (float)pixel * 3F / 4F;
            return (float)pixel * 72F / dpiX;
        }

        /// <summary>
        /// ポイント値からピクセル値を求める静的メソッド
        /// <note>換算の結果が小数を含む場合、整数に切り上げる</note>
        /// </summary>
        /// <param name="point">ポイント値</param>
        /// <returns>ピクセル値</returns>
        public static int PointToPixel(float point)
        {
            getDpiX();
            float res = 0F;
            if (dpiX == 0F) res = point * 4F / 3F;
            else res = point * dpiX / 72F;
            return (int)Math.Ceiling((double)res);
        }

        /// <summary>
        /// ポイント値からピクセル値を求める静的メソッド
        /// <note>換算の結果が小数を含む場合、整数に切り上げる</note>
        /// </summary>
        /// <param name="point">ポイント値</param>
        /// <returns>ピクセル値</returns>
        public static int PointToPixel(double point)
        {
            getDpiX();
            double res = 0.0;
            if (dpiX == 0F) res = point * 4.0 / 3.0;
            else res = point * (double)dpiX / 72.0;
            return (int)Math.Ceiling(res);
        }
        #endregion

        #region 文字列操作関連
        /// <summary>
        /// 文字列のバイト長を返すメソッド
        /// </summary>
        /// <param name="buffer">バイト長を調べる文字列</param>
        /// <param name="encodeName">エンコーディングのコードページ名 (省略可、既定値「shift-jis」)</param>
        /// <returns>文字列のバイト長</returns>
        public static int GetByteLength(string buffer, string encodeName = "shift-jis")
        {
            int res = 0;
            try
            {
                Encoding enc = Encoding.GetEncoding(encodeName);
                res = enc.GetBytes(buffer).Length;
            }
            catch (Exception)
            {
            }
            return res;
        }

        /// <summary>
        /// 文字列の左端から指定した長さの文字列を切り出して返すメソッド
        /// </summary>
        /// <param name="buffer">元の文字列</param>
        /// <param name="Length">切り出す長さ</param>
        /// <returns>切り出した文字列</returns>
        public static string Left(string buffer, int Length)
        {
            if (buffer == null || Length < 0) return null;
            if (Length > buffer.Length) return buffer;
            return buffer.Substring(0, Length);
        }

        /// <summary>
        /// 文字列の左端から指定したバイト分の文字列を切り出して返すメソッド
        /// <note>1文字途中で切り取られる場合は、その前の文字までを返す</note>
        /// </summary>
        /// <param name="buffer">元の文字列</param>
        /// <param name="Length">切り出すバイト長</param>
        /// <param name="encodeName">エンコーディングのコードページ名 (省略可、既定値「shift-jis」)</param>
        /// <returns>切り出した文字列</returns>
        public static string LeftB(string buffer, int Length, string encodeName = "shift-jis")
        {
            if (buffer == null || Length < 0) return null;
            if (Length == 0) return "";
            Encoding enc = Encoding.GetEncoding(encodeName);
            string buf = buffer.Length > Length ? buffer.Substring(0, Length) : buffer;
            byte[] tmp = enc.GetBytes(buf);
            if (tmp.Length > Length)
            {
                byte[] org = new byte[Length];
                Array.Copy(tmp, org, Length);
                buf = enc.GetString(tmp, 0, Length);
                tmp = enc.GetBytes(buf);
                for (int i = Length - 1; i >= 0; --i)
                {
                    if (tmp[i] == org[i])
                    {
                        // buf = enc.GetString(tmp, 0, i + 1);
                        break;
                    }
                    else
                    {
                        --Length;
                    }
                }
                buf = enc.GetString(tmp, 0, Length);
            }
            return buf;
        }

        /// <summary>
        /// 文字列の幅を返すメソッド
        /// <note>描画領域として、プライマリモニタのデバイスコンテキストを用いるため、その環境に依存する</note>
        /// </summary>
        /// <param name="buffer">幅を調べる文字列</param>
        /// <param name="font">フォントを表すFontクラスのインスタンスへの参照</param>
        /// <param name="withPadding">グリフ突出対応のためのパディングを含めるかどうか (省略可、既定値true)</param>
        /// <returns>文字列の幅 (ピクセル値)</returns>
        public static int GetTextWidth(string buffer, Font font, bool withPadding = true)
        {
            if (buffer == null) return 0;
            IntPtr hDc = IntPtr.Zero;
            int w = 0;
            try
            {
                hDc = GetDC(IntPtr.Zero);
                if (hDc == IntPtr.Zero) throw new Exception();  // デバイスコンテキストハンドル取得失敗時→catch句に飛ばす
                Graphics g = Graphics.FromHdc(hDc);
                using (g)
                {
                    SizeF size = g.MeasureString(buffer, font);
                    if (withPadding)
                    {
                        w = size.ToSize().Width;
                    }
                    else
                    {
                        StringFormat fmt = new StringFormat();
                        CharacterRange[] characterRanges = { new CharacterRange(0, buffer.Length) };
                        fmt.SetMeasurableCharacterRanges(characterRanges);
                        PointF p = new PointF(0F, 0F);
                        RectangleF layoutRect = new RectangleF(p, size);
                        Region[] regions = g.MeasureCharacterRanges(buffer, font, layoutRect, fmt);
                        RectangleF rect = regions[0].GetBounds(g);
                        w = (int)rect.Width;
                    }
                }
            }
            catch (Exception)
            {
                w = 0;
            }
            if (hDc != IntPtr.Zero) ReleaseDC(IntPtr.Zero, hDc);
            return w;
        }

        /// <summary>
        /// 文字列の幅を返すメソッド
        /// <note>描画領域として、プライマリモニタのデバイスコンテキストを用いるため、その環境に依存する</note>
        /// </summary>
        /// <param name="buffer">幅を調べる文字列</param>
        /// <param name="fontName">フォント名</param>
        /// <param name="fontPointSize">フォントサイズ (ポイント)</param>
        /// <param name="fontStyle">フォントスタイルを表すFontStyle列挙型の値 (省略可、既定値FontStyle.Regular)</param>
        /// <param name="withPadding">グリフ突出対応のためのパディングを含めるかどうか (省略可、既定値true)</param>
        /// <returns>文字列の幅 (ピクセル値)</returns>
        public static int GetTextWidth(string buffer
                    , string fontName, float fontPointSize
                    , FontStyle fontStyle = FontStyle.Regular
                    , bool withPadding = true)
        {
            int w = 0;
            Font font = null;
            try
            {
                font = new Font(fontName, fontPointSize, fontStyle);
            }
            catch (Exception)
            {
                return w;
            }
            using (font)
            {
                w = GetTextWidth(buffer, font, withPadding);
            }
            return w;
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
        public static int GetTextWidth(string buffer
                    , string fontName, int fontSize
                    , FontStyle fontStyle = FontStyle.Regular
                    , bool withPadding = true)
        {
            float fontPointSize = PixelToPoint(fontSize);
            return GetTextWidth(buffer, fontName, fontPointSize, fontStyle, withPadding);
        }

        /// <summary>
        /// 文字列の幅を厳密に求めて返すメソッド
        /// <note>
        /// 描画領域として、プライマリモニタのデバイスコンテキストを用いるため、その環境に依存する<br />
        /// ピクセル→ポイント換算においても同様のため、実行する環境に依存する
        /// </note>
        /// </summary>
        /// <param name="buffer">幅を調べる文字列</param>
        /// <param name="font">フォントを表すFontクラスのインスタンスへの参照</param>
        /// <returns>文字列の幅 (ピクセル値)</returns>
        public static int GetStrictTextWidth(string buffer, Font font)
        {
            if (buffer == null) return 0;
            Hashtable charlist = new Hashtable();
            for (int i = 0; i < buffer.Length; ++i)
            {
                string c = buffer.Substring(i, 1);
                if (charlist.ContainsKey(c))
                {
                    charlist[c] = (int)charlist[c] + 1;
                }
                else
                {
                    charlist.Add(c, 1);
                }
            }
            int w = 0;
            foreach (DictionaryEntry de in charlist)
            {
                char c = ((string)de.Key)[0];
                int cw = GetTextWidth(new string(c, 4), font, false) - GetTextWidth(new string(c, 3), font, false);
                w += cw * (int)de.Value;
            }
            return w;
        }

        private static int getLineRowsCount(string buffer, Font font, int limit)
        {
            // 二分探索で改行位置を探す
            if (buffer.Length == 0) return 1;
            int cnt = 0;
            for (cnt = 0; buffer.Length > 0; ++cnt)
            {
                int ps = 0, pe = buffer.Length;
                int pm = 0;
                while (ps <= pe)
                {
                    pm = (ps + pe) / 2;
                    int w = GetStrictTextWidth(buffer.Substring(0, pm), font);
                    if (w > limit)
                    {
                        pe = pm - 1;
                    }
                    else if (w == limit)
                    {
                        pe = pm;
                        break;
                    }
                    else
                    {
                        ps = pm + 1;
                    }
                }
                if (pe <= 0) return -1;  // 表示領域の幅が小さすぎて1文字も入らない
                buffer = buffer.Substring(pe);
            }
            return cnt;
        }

        /// <summary>
        /// 一定のフォントでの文字列の行数を算出するメソッド
        /// </summary>
        /// <param name="buffer">行数を調べる文字列</param>
        /// <param name="font">フォントを表すFontクラスのインスタンスへの参照</param>
        /// <param name="areaWidth">表示領域の幅 (ピクセル値)</param>
        /// <param name="leftMargin">左余白 (ピクセル値)</param>
        /// <param name="rightMargin">右余白 (ピクセル値)</param>
        /// <param name="delimiterPattern">改行コードとする文字列の正規表現パターン (省略可:既定値「\r\n|\r|\n」)</param>
        /// <returns>成功時:行数、引数不正時:0、表示領域幅が小さすぎる時:-1</returns>
        public static int GetRowsCount(string buffer, Font font, int areaWidth, int leftMargin, int rightMargin
                                     , string delimiterPattern = @"\r\n|\r|\n")
        {
            int limit = areaWidth - leftMargin - rightMargin;
            if (buffer == null || font == null || limit <= 0) return 0;
            Regex regex = null;
            try
            {
                if (string.IsNullOrEmpty(delimiterPattern)) throw new Exception();
                regex = new Regex(delimiterPattern);
            }
            catch (Exception)
            {
                return getLineRowsCount(buffer, font, limit);
            }
            int cnt = 0;
            foreach (string lineBuf in regex.Split(buffer))
            {
                int tmpCnt = getLineRowsCount(lineBuf, font, limit);
                if (tmpCnt == -1) return -1;
                cnt += tmpCnt;
            }
            return cnt;
        }

        private static bool IsNumericExpression<T>(string buffer, bool ignoreByte
                                              , bool allowDecimalPoint
                                              , bool allowLeadingSign
                                              , bool allowLeadingWhite
                                              , bool allowParentheses
                                              , bool allowThousands
                                              , bool allowTrailingWhite
                                              , bool allowExponent
                                              , out int resultInt, out double resultDouble) where T : struct
        {
            resultInt = 0;
            resultDouble = 0.0;
            if (string.IsNullOrWhiteSpace(buffer)) return false;
            if (ignoreByte) buffer = Strings.StrConv(buffer, VbStrConv.Narrow);
            NumberStyles style = allowDecimalPoint ? NumberStyles.AllowDecimalPoint : (NumberStyles)0;
            if (allowLeadingSign) style |= NumberStyles.AllowLeadingSign;
            if (allowLeadingWhite) style |= NumberStyles.AllowLeadingWhite;
            if (allowParentheses) style |= NumberStyles.AllowParentheses;
            if (allowThousands) style |= NumberStyles.AllowThousands;
            if (allowTrailingWhite) style |= NumberStyles.AllowTrailingWhite;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("ja-JP");
            if (typeof(T) == typeof(int))
            {
                return int.TryParse(buffer, style, culture, out resultInt);
            }
            if (typeof(T) == typeof(double))
            {
                if (allowExponent) style |= NumberStyles.AllowExponent;
                return double.TryParse(buffer, style, culture, out resultDouble);
            }
            return false;
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
        public static bool IsDoubleExpression(string buffer, out double resultNumber, bool ignoreByte = false
                                            , bool allowLeadingSign = true
                                            , bool allowLeadingWhite = true
                                            , bool allowParentheses = false
                                            , bool allowThousands = false
                                            , bool allowTrailingWhite = true
                                            , bool allowExponent = false)
        {
            int dummy = 0;
            return IsNumericExpression<double>(buffer, ignoreByte
                                             , true, allowLeadingSign, allowLeadingWhite
                                             , allowParentheses, allowThousands, allowTrailingWhite
                                             , allowExponent, out dummy, out resultNumber);
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
        public static bool IsIntegerExpression(string buffer, out int resultNumber, bool ignoreByte = false
                                             , bool allowDecimalPoint = true
                                             , bool allowLeadingSign = true
                                             , bool allowLeadingWhite = true
                                             , bool allowParentheses = false
                                             , bool allowThousands = false
                                             , bool allowTrailingWhite = true)
        {
            double dummy = 0.0;
            return IsNumericExpression<int>(buffer, ignoreByte
                                          , allowDecimalPoint, allowLeadingSign, allowLeadingWhite
                                          , allowParentheses, allowThousands, allowTrailingWhite
                                          , false, out resultNumber, out dummy);
        }

        /// <summary>
        /// 半角文字列を取得する
        /// </summary>
        /// <param name="str">変換対象文字列</param>
        /// <returns></returns>
        public static string ToStrNarrow(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return Strings.StrConv(str, VbStrConv.Narrow);
        }

        static Regex cleanRegex = new Regex(@"[\x00-\x09\x0B-\x1F\x7F]");
        static Regex cleanRegex2 = new Regex(@"[\x00-\x1F\x7F]");

        /// <summary>
        /// S-JIS1バイトの特殊文字を削除する
        /// </summary>
        /// <param name="buffer">文字列</param>
        /// <param name="cutLineFeed">ラインフィードもカットする場合true (省略可、既定値false)</param>
        /// <returns>特殊文字(<paramref name="cutLineFeed"/>がfalseの場合は、ラインフィードは除く)を削除した文字列</returns>
        public static string CleanSpecialCharacters(string buffer, bool cutLineFeed = false)
        {
            if (string.IsNullOrWhiteSpace(buffer)) return buffer;
            Regex regex = cutLineFeed ? cleanRegex2 : cleanRegex;
            return regex.Replace(buffer, string.Empty);
        }
        #endregion

        #region 式構文解析関連
        /// <summary>
        /// 外部EXE(ComputeExpression.exe)を呼び出す
        /// </summary>
        /// <param name="readFilePath">データ加工COMPUTEで利用する中間ファイル</param>
        /// <param name="writeFilePath">データ加工COMPUTEで作成する最終ファイル</param>
        /// <returns>終了コード。0は正常終了、0以外は異常終了</returns>
        public static int ComputeProcess(string readFilePath, string writeFilePath)
        {
            ReadDBInfo readDBInfo = new ReadDBInfo();
            // Seasar.Quill.QuillInjector.GetInstance().Inject(readDBInfo);
            //string exe_path = readDBInfo.AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_COMPUTE_EXPRESSION_PATH);
            string program = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"QC4\Templates\ComputeExpression\ComputeExpression.exe");//@"C:\QCWeb\exec\ComputeExpression\ComputeExpression.exe";//System.IO.Path.Combine(exe_path, GlobalsCommonConstant.computeExpressionExe);
            string arguments = string.Format("{0} {1}", "\"" + readFilePath + "\"", "\"" + writeFilePath + "\"");

            System.Diagnostics.Process extProcess = new Process();
            extProcess.StartInfo.CreateNoWindow = true;
            extProcess.StartInfo.ErrorDialog = false;
            extProcess.StartInfo.FileName = program;	//起動するファイル名
            extProcess.StartInfo.Arguments = arguments;	//起動時の引数
            extProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; // ProcessWindowStyle.Minimized;
            extProcess.Start(); //プロセス開始

            extProcess.WaitForExit();
            int result = extProcess.ExitCode;
            extProcess.Close(); //プロセスクローズ
            return result;
        }

        /// <summary>
        /// ComputeExpressionDllで文字列表現を式として評価し、その結果を返す静的メソッド
        /// </summary>
        /// <param name="qcwebid">QCWEBID</param>
        /// <param name="expression">文字列表現</param>
        /// <returns>
        /// 式として成り立っている場合true、成り立っていない場合false
        /// </returns>
        private static bool CheckExpression(decimal qcwebid, string expression)
        {
            bool retVal = true;

            //評価結果を貰うファイル名作成する(GUIDを利用)(「GUID.tmp.tmp」に評価式を書き込み)。
            ReadDBInfo readDBInfo = new ReadDBInfo();
            Seasar.Quill.QuillInjector.GetInstance().Inject(readDBInfo);
            string workDir = System.IO.Path.Combine(readDBInfo.AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_RAWDATA_PATH_AP), qcwebid.ToString());
            string tmpfileExt = "." + GlobalsCommonConstant.fileExtension.tmp;
            string tmpFileName = System.Guid.NewGuid().ToString() + tmpfileExt;
            string tmpFilePath = System.IO.Path.Combine(workDir, tmpFileName);
            string tmpTmpFilePath = System.IO.Path.Combine(workDir, tmpFileName + tmpfileExt);

            //評価する式を一時ファイル(「GUID.tmp.tmp」へ)へ格納する。
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(tmpTmpFilePath, false, Encoding.UTF8))
            {
                //チェック済み、nullではないはず。
                if (!string.IsNullOrEmpty(expression))
                {
                    writer.WriteLine(expression);
                }
                writer.Close();
            }

            // パス ファイル名
            /*
            StringBuilder argsBuilder = new StringBuilder(workDir);
            argsBuilder.Append(" " + tmpFileName);

            string arguments = argsBuilder.ToString();

            System.Diagnostics.Process extProcess = new Process();
            extProcess.StartInfo.CreateNoWindow = true;
            extProcess.StartInfo.ErrorDialog = false;
            extProcess.StartInfo.FileName = program;	//起動するファイル名
            extProcess.StartInfo.Arguments = arguments;	//起動時の引数
            extProcess.Start(); //プロセス開始
            extProcess.WaitForExit();
            extProcess.Close(); //プロセスクローズ
             */

            //computeExpressionで評価式を評価する。
            int result = ComputeProcess(tmpTmpFilePath, tmpFilePath);
            if (result != 0)
            {
                string errorMessage = string.Format("COMPUTEのProcess処理に失敗しました。ExitCode={0}", result);
                throw new QCWebException(errorMessage, GlobalsCommonConstant.LogLevel.FATAL, null);
            }

            //評価式の評価結果を読み込む(GUID.tmpから読み込む)。
            string formulaStr = null;
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(tmpFilePath, false))
                {
                    //一行しかない。
                    formulaStr = reader.ReadLine();
                    reader.Close();
                }
            }
            catch // ※※※式解析エラーを拾って式評価するためのcatchなので消してはいけない※※※
            {
                formulaStr = null;
            }

            //評価で使用した一時ファイルを削除する。
            System.IO.File.Delete(tmpFilePath);

            //評価結果を返す　0除算(無限大/無限小の場合、無回答)
            if (formulaStr == double.NaN.ToString())
            {
                retVal = false;
            }

            return retVal;
        }

        /// <summary>
        /// 式評価時アイテム名のパターン
        /// </summary>
        private const string ITEMNAME_PATTERN = @"\[([^:\[\]@\|='&\\\!\?<>\*/\r\n]+)\]";
        private static Regex itemNameRegex = new Regex(ITEMNAME_PATTERN);

        /// <summary>
        /// 評価式のアイテム名を評価する。
        /// </summary>
        /// <param name="qcwebid">QCWEBID</param>
        /// <param name="expression">評価対象の式</param>
        /// <param name="itemNamesList">アイテム名のリストを格納するstring型Listクラスのインスタンスへの参照</param>
        /// <param name="sId">シナリオID</param>
        /// <returns>0：正常、</returns>
        public static bool ParseExpressionItemName(decimal qcwebid, string expression, List<string> itemNamesList, decimal? sId = null)
        {
            if (itemNamesList != null) itemNamesList.Clear();
            if (string.IsNullOrWhiteSpace(expression)) return false;

            //一致するものがなかったら、正常で返す。
            if (!itemNameRegex.IsMatch(expression)) return true;

            if (qcwebid == (decimal)0) return false;

            QueryItemName query = new QueryItemName();
            List<string> tmpNamesList = new List<string>();
            // アイテム名箇所を精査
            foreach (Match match in itemNameRegex.Matches(expression))
            {
                // SAかNでマトリクス親でないアイテム名があるかどうか
                string tmpName = match.Groups[1].Value;
                if (tmpNamesList.Contains(tmpName)) continue;
                decimal qID = query.QuestionNameToID(
                        qcwebid, QuestionType.SA | QuestionType.N, tmpName, sId);
                if (qID == (decimal)0)
                {
                    if (itemNamesList != null) itemNamesList.Clear();
                    return false;
                }
                tmpNamesList.Add(tmpName);
                if (itemNamesList != null) itemNamesList.Add(tmpName);
            }

            return true;
        }

        //private static System.CodeDom.Compiler.CodeDomProvider provider = null;
        //private static System.CodeDom.Compiler.CompilerParameters parameters = null;
        private static Regex mathMethodRegex = null;
        private static object evalObj = null;
        // private static dynamic evalObj = null;
        private static CallSite<Func<CallSite, object, string>> castSite = null;
        private static CallSite<Func<CallSite, object, string, object>> bodySite = null;

        /// <summary>
        /// 式の結果を返す静的メソッド
        /// </summary>
        /// <param name="expression">式</param>
        /// <returns>成功時は式の結果、失敗時はNaN</returns>
        public static double Eval(string expression)
        {
            #region C#
            /*
            if (provider == null)
            {
                provider = new Microsoft.CSharp.CSharpCodeProvider();
                parameters = new System.CodeDom.Compiler.CompilerParameters();
                parameters.GenerateInMemory = true;
                mathMethodRegex = new Regex(@"(max\(|min\()");
            }
            expression = mathMethodRegex.Replace(expression, "Math.$1");
            string code
                = @"public class TempClass
                    {
                        private static double sumsub(params double[] args)
                        {
                            if (args.Length == 0) return double.NaN;
                            try
                            {
                                double s = 0.0;
                                for (int i = 0; i < args.Length; ++i)
                                {
                                    if (double.IsNaN(args[i])) return double.NaN;
                                    s += args[i];
                                }
                                return s;
                            }
                            catch
                            {
                                return double.NaN;
                            }
                        }
                        private static double sum(params double[] args)
                        {
                            return sumsub(args);
                        }
                        private static double average(params double[] args)
                        {
                            double s = sumsub(args);
                            if (double.IsNaN(s)) return s;
                            return s / (double)args.Length;
                        }
                        public static double Eval()
                        {
                            return " + expression + @";
                        }                        
                    }";
            System.CodeDom.Compiler.CompilerResults res = provider.CompileAssemblyFromSource(parameters, code);
            Assembly asm = res.CompiledAssembly;
            Type t = asm.GetType("TempClass");
            return (double)t.InvokeMember("Eval", BindingFlags.InvokeMethod, null, null, null);
            */
            #endregion
            if (evalObj == null)
            {
                #region JScript.netソース
                #region 引数の数に制限なし (ネストが複雑)
                string code
                    = @"package TempPackage
                        {
                            class TempClass
                            {
                                private function ParseParameters(paramBuffer : String) : String
                                {
                                    while (true)
                                    {
                                        var funcName : String = 'average';
                                        var x : int = paramBuffer.indexOf(funcName + '(');
                                        if (x < 0)
                                        {
                                            funcName = 'sum';
                                            x = paramBuffer.indexOf(funcName + '(');
                                        }
                                        if (x < 0) break;
                                        var l : int = funcName.length;
                                        var c : int = 1;
                                        var s : int = x + l + 1;
                                        var e : int = s;
                                        while (c > 0)
                                        {
                                            e = paramBuffer.indexOf(')', s);
                                            if (e < 0) throw '';
                                            var y : int = paramBuffer.indexOf('(', s);
                                            if (y >= 0 && e > y)
                                            {
                                                ++c;
                                                s = y + 1;
                                            }
                                            else
                                            {
                                                --c;
                                                s = e + 1;
                                            }
                                        }
                                        var leftPart : String = paramBuffer.substring(0, x);
                                        var rightPart : String = paramBuffer.substring(e + 1, paramBuffer.length);
                                        var paramsPart = ParseParameters(paramBuffer.substring(x + funcName.length + 1, e));
                                        var funcPart = funcName + '([' + paramsPart + '])';
                                        paramBuffer = leftPart + eval(funcPart).toString() + rightPart;
                                    }
                                    return paramBuffer;
                                }
                                private function EvalBody(expression : String) : double
                                {
                                    var x : int = expression.indexOf('average(');
                                    var y : int = expression.indexOf('sum(');
                                    var i : int = -1;
                                    var l = 0;
                                    if (x >= 0 && (y < 0 || x < y))
                                    {
                                        i = x;
                                        l = 'average('.length;
                                    }
                                    else if (y >= 0)
                                    {
                                        i = y;
                                        l = 'sum('.length;
                                    }
                                    while (i >= 0)
                                    {
                                        var c : int = 1;
                                        var s : int = i + l;
                                        while (c > 0)
                                        {
                                            x = expression.indexOf(')', s);
                                            if (x < 0) throw '';
                                            y = expression.indexOf('(', s);
                                            if (y >= 0 && x > y)
                                            {
                                                ++c;
                                                s = y + 1;
                                            }
                                            else
                                            {
                                                --c;
                                                s = x + 1;
                                            }
                                        }
                                        var leftPart : String = expression.substring(0, i + l);
                                        var paramsPart : String = ParseParameters(expression.substring(i + l, x));
                                        var rightPart : String = expression.substring(x, expression.length);
                                        expression = leftPart + '[' + paramsPart + ']' + rightPart;
                                        x = expression.indexOf('average(', s);
                                        y = expression.indexOf('sum(', s);
                                        i = -1;
                                        if (x >= 0 && (y < 0 || x < y))
                                        {
                                            i = x;
                                            l = 'average('.length;
                                        }
                                        else if (y >= 0)
                                        {
                                            i = y;
                                            l = 'sum('.length;
                                        }                                        
                                    }
                                    return double(eval(expression));
                                }
                                // private function sumsub(... args : double[]) : double
                                private function sumsub(args : double[]) : double
                                {
                                    try
                                    {
                                        if (args == null || args.length == 0) throw '';
                                        var s : double = 0.0;
                                        for (var i = 0; i < args.length; ++i)
                                        {
                                            if (args[i] == null) throw '';
                                            s += EvalBody(args[i]);
                                        }
                                        return s;
                                    }
                                    catch (e)
                                    {
                                        throw;
                                    }
                                }
                                // public function sum(... args : double[]) : double
                                public function sum(args : double[]) : double
                                {
                                    try
                                    {
                                        return sumsub(args);
                                    }
                                    catch (e)
                                    {
                                        throw;
                                    }
                                }
                                // public function average(... args : double[]) : double
                                public function average(args : double[]) : double
                                {
                                    try
                                    {
                                        var s : double = sumsub(args);
                                        return s / args.length;
                                    }
                                    catch (e)
                                    {
                                        throw;
                                    }
                                }
                                public function Eval(expression : String) : String
                                {
                                    try
                                    {
                                        return EvalBody(expression).toString();
                                    }
                                    catch (e)
                                    {
                                        return '';
                                    }
                                }
                            }
                        }";
                #endregion
                #region 引数の数を1～30個に制限(Excelと同じ仕様)して、オーバーロードで実装
                /*
                string code
                    = @"package TempPackage
                        {
                            class TempClass
                            {";
                List<string> argList = new List<string>();
                for (int i = 1; i <= 30; ++i)
                {
                    argList.Add("arg" + i.ToString());
                    string[] argsArray = argList.ToArray();
                    string argsBuf = string.Join(" : double, ", argsArray) + " : double";
                    string sumBuf = string.Join(" + ", argsArray);
                    string args = string.Join(", ", argsArray);
                    code += @"
                                public static function sum(" + argsBuf + @") : double
                                {
                                    try
                                    {
                                        return " + sumBuf + @";
                                    }
                                    catch (e)
                                    {
                                        throw;
                                    }
                                }
                                public static function average(" + argsBuf + @") : double
                                {
                                    try
                                    {
                                        var s : double = sum(" + args + @");
                                        if (isNaN(s)) throw '';
                                        return s / " + i.ToString() + @".0;
                                    }
                                    catch (e)
                                    {
                                        throw;
                                    }
                                }";
                }
                code += @"
                                public function Eval(expression : String) : String
                                {
                                    try
                                    {
                                        return eval(expression).toString();
                                    }
                                    catch (e)
                                    {
                                        return '';
                                    }
                                }
                            }
                        }";
                */
                #endregion
                #endregion
                System.CodeDom.Compiler.CodeDomProvider provider = new Microsoft.JScript.JScriptCodeProvider();
                System.CodeDom.Compiler.CompilerParameters parameters = new System.CodeDom.Compiler.CompilerParameters();
                parameters.GenerateInMemory = true;
                System.CodeDom.Compiler.CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
                Assembly asm = results.CompiledAssembly;
                Type t = asm.GetType("TempPackage.TempClass");
                evalObj = Activator.CreateInstance(t);
                castSite = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(GlobalMethodClass)));
                bodySite = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Eval", null, typeof(GlobalMethodClass)
                                            , new CSharpArgumentInfo[]
                                                {
                                                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                                                  , CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
                                                }));
                mathMethodRegex = new Regex(@"(max\(|min\()");
            }
            expression = mathMethodRegex.Replace(expression, "Math.$1");
            // string resBuf = LateBind.RunMethodLateBind(evalObj, "Eval", expression).ToString();
            // string resBuf = evalObj.Eval(expression);
            string resBuf = castSite.Target(castSite, bodySite.Target(bodySite, evalObj, expression));
            double res = 0.0;
            if (double.TryParse(resBuf, out res)) return res;
            return double.NaN;
        }

        /// <summary>
        /// 全角の四則演算記号と括弧は半角で評価する。Mantis.0000453
        /// 引数の文字列がnullまたは空文字の場合は変換せずに返す。
        /// </summary>
        /// <param name="s">文字列</param>
        /// <returns>変換後の文字列。</returns>
        private static string ToStrNarrowFormula(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            s = s.Replace("＋", "+");
            s = s.Replace("－", "-");
            s = s.Replace("＊", "*");
            s = s.Replace("／", "/");
            s = s.Replace("（", "(");
            s = s.Replace("）", ")");
            return s;
        }

        /// <summary>
        /// 文字列表現を式として評価し、その結果を返す静的メソッド
        /// </summary>
        /// <param name="qcwebid">QCWeb管理ID</param>
        /// <param name="expression">文字列表現</param>
        /// <param name="AllowStandardFunction">MAX/MIN/AVERAGE/SUMを許容する場合はtrue (省略可、既定値false)</param>
        /// <param name="itemNamesList">アイテム名のリストを格納するstring型Listクラスのインスタンスへの参照 (省略可、既定値null)</param>
        /// <param name="itemNameValueList">アイテム名と数値のKey-Valueデータを格納するDictionaryのインスタンスへの参照 (省略可、既定値null)</param>
        /// <returns>
        /// 式として成り立っている場合、式の評価結果、成り立っていない場合NaN
        /// <note>式内にアイテム参照が含まれている場合、正しい値は返されない</note>
        /// </returns>
        /// 64bit環境で使えないJScriptを使用しない
        public static double ParseExpression(decimal qcwebid,
                                             string expression,
                                             bool AllowStandardFunction = false,
                                             List<string> itemNamesList = null,
                                             Dictionary<string, double> itemNameValueList = null)
        {
            if (itemNamesList != null) itemNamesList.Clear();
            if (string.IsNullOrWhiteSpace(expression)) return double.NaN;

            //Regex regex = new Regex(@"\[([^:\[\]@\|='&\\\!\?<>\*/\r\n]+)\]");
            if (itemNameRegex.IsMatch(expression))
            {
                if (qcwebid == (decimal)0) return double.NaN;
                if (itemNameValueList == null) // 式の評価
                {
                    //QueryItemName query = new QueryItemName();
                    //List<string> tmpNamesList = new List<string>();
                    //// アイテム名箇所を精査
                    //foreach (Match match in regex.Matches(expression))
                    //{
                    //    // SAかNでマトリクス親でないアイテム名があるかどうか
                    //    string tmpName = match.Groups[1].Value;
                    //    if (tmpNamesList.Contains(tmpName)) continue;
                    //    decimal qID = query.QuestionNameToID(
                    //            qcwebid, QuestionType.SA | QuestionType.N, tmpName);
                    //    if (qID == (decimal)0)
                    //    {
                    //        if (itemNamesList != null) itemNamesList.Clear();
                    //        return double.NaN;
                    //    }
                    //    tmpNamesList.Add(tmpName);
                    //    if (itemNamesList != null) itemNamesList.Add(tmpName);
                    //}
                    if (!ParseExpressionItemName(qcwebid, expression, itemNamesList)) return double.NaN;

                    // 引数の数値が省略された場合は、式評価が目的なので1で計算する
                    expression = itemNameRegex.Replace(expression, "(1)");
                }
                else if (itemNameValueList != null) // 式に数値をあてはめて計算
                {
                    // OKの場合、アイテム名箇所を、適当な数値に置換
                    // かっこを付けることで、次のようなNGケースにおいて、正しくNGと判断させる
                    /*
                                1           1.5         (1)
                    [Q1][Q2]    11→OK×  1.51.5→NG○  (1)(1)→NG○
                    [Q1]1       11→OK×  1.51→OK×    (1)1→NG○
                    1[Q1]       11→OK×  11.5→OK×    1(1)→NG○
                    */
                    foreach (string itemName in itemNameValueList.Keys)
                    {
                        // 正規表現のインスタンシングは無駄にオーバーヘッドを発生させるだけ
                        // Regex itemNameSearch = new Regex(@"\[" + itemName + @"\]");
                        double value = itemNameValueList[itemName];
                        // expression = itemNameSearch.Replace(expression, "(" + value.ToString() + ")");
                        expression = expression.Replace("[" + itemName + "]", "(" + value.ToString() + ")");
                    }
                }
                // 値が無回答と非該当の場合、Key-Valueに無いアイテム名が残るので消す
                // 何がしたいのかがよくわからんから、そのままにしておくけど、消したらアカンやろ
                // 1*[Q1]*[Q2]*5→1***5にしてよいのか？
                // つか、そもそもitemNameValueList == nullのとき、この処理は無意味→else if句内とelse句に入れなアカン
                expression = itemNameRegex.Replace(expression, string.Empty);
            }

            string tmpBuf = expression;
            Regex regex = null;
            if (AllowStandardFunction)
            {
                // こんなことしたら成り立たない式まで成り立つことにしてまうからアカン
                /*
                // カンマの連続は1つに置換する
                regex = new Regex(@"\,{2,}");
                expression = regex.Replace(expression, ",");
                // 先頭にカンマがあったら除去
                regex = new Regex(@"\(\,");
                expression = regex.Replace(expression, "(");
                // 最後にカンマがあったら除去
                regex = new Regex(@"\,\)");
                expression = regex.Replace(expression, ")");
                */
                // 許可する関数を退避させる
                expression = expression.ToLower();
                regex = new Regex(@"max\(|min\(|average\(|sum\(|\,");
                tmpBuf = regex.Replace(expression, string.Empty);
            }
            // 全角の四則演算記号と括弧は半角で評価する。Mantis.0000453
            tmpBuf = ToStrNarrowFormula(tmpBuf);
            expression = ToStrNarrowFormula(expression);

            // tmpBufは、数値と四則演算記号、かっこ、半角スペースのみになっていなければならない
            regex = new Regex(@"[^\d\.\+\-\*\/\(\) ]");
            if (regex.IsMatch(tmpBuf)) return double.NaN;

            return expression.Eval();
            /*
            // 構文チェック
            if (!CheckExpression(qcwebid, expression)) return double.NaN;

            return 0;
            */
        }

        /// <summary>
        /// COMPUTEの式からアイテム名を抽出する
        /// </summary>
        /// <param name="qcwebid">QCWeb管理ID</param>
        /// <param name="expression">式の文字列表現</param>
        /// <returns>アイテム名のリスト</returns>
        public static List<string> ExtractItemName(decimal qcwebid, string expression)
        {
            List<string> names = new List<string>();
            if (!ParseExpressionItemName(qcwebid, expression, names)) return null;
            return names;
        }

        /// <summary>
        /// 式のアイテム名を値に置き換えて、その結果を返す静的メソッド
        /// </summary>
        /// <param name="expression">文字列表現</param>
        /// <param name="itemNamesList">アイテム名のリストを格納するstring型Listクラスのインスタンスへの参照 (省略可、既定値null)</param>
        /// <param name="itemNameValueList">アイテム名と数値のKey-Valueデータを格納するDictionaryのインスタンスへの参照 (省略可、既定値null)</param>
        /// <returns>
        /// 式として成り立っている場合、式の評価結果、成り立っていない場合NaN
        /// <note>式内にアイテム参照が含まれている場合、正しい値は返されない</note>
        /// </returns>
        /// 64bit環境で使えないJScriptを使用しない
        public static string BuildExpression(string expression, List<string> itemNamesList = null, Dictionary<string, double> itemNameValueList = null)
        {
            if (string.IsNullOrWhiteSpace(expression)) return null;

            Regex regex = new Regex(@"\[([^:\[\]@\|='&\\\!\?<>\*/\r\n]+)\]");
            string expressionValue = null;
            if (regex.IsMatch(expression))
            {
                // OKの場合、アイテム名箇所を、適当な数値に置換
                // かっこを付けることで、次のようなNGケースにおいて、正しくNGと判断させる
                /*
                            1           1.5         (1)
                [Q1][Q2]    11→OK×  1.51.5→NG○  (1)(1)→NG○
                [Q1]1       11→OK×  1.51→OK×    (1)1→NG○
                1[Q1]       11→OK×  11.5→OK×    1(1)→NG○
                */
                for (int i = 0; i < itemNamesList.Count; i++)
                {
                    Regex itemNameSearch = new Regex(@"\[" + itemNamesList[i] + @"\]");
                    if (itemNameValueList.ContainsKey(itemNamesList[i]))
                    {
                        double value = itemNameValueList[itemNamesList[i]];
                        expression = itemNameSearch.Replace(expression, "(" + value.ToString() + ")");
                    }
                }
                // 値が無回答と非該当の場合、Key-Valueに無いアイテム名が残るので消す
                expressionValue = regex.Replace(expression, string.Empty);
            }

            //変換後文字列がNullの場合、元の文字列でセットする。
            if (expressionValue == null) expressionValue = expression;

            // カンマの連続は1つに置換する
            regex = new Regex(@"\,{2,}");
            expressionValue = regex.Replace(expressionValue, ",");
            // 先頭にカンマがあったら除去
            regex = new Regex(@"\(\,");
            expressionValue = regex.Replace(expressionValue, "(");
            // 最後にカンマがあったら除去
            regex = new Regex(@"\,\)");
            expressionValue = regex.Replace(expressionValue, ")");

            // 全角の四則演算記号と括弧は半角で評価する。Mantis.0000453
            expressionValue = ToStrNarrowFormula(expressionValue);

            return expressionValue;
        }

        public static string BuildExpressionFA_(string expression, List<string> itemNamesList = null, Dictionary<string, string> itemNameValueList = null)
        {
            if (string.IsNullOrWhiteSpace(expression)) return null;

            Regex regex = new Regex(@"\[([^:\[\]@\|='&\\\!\?<>\*/\r\n]+)\]");
            string expressionValue = null;
            if (regex.IsMatch(expression))
            {
                // OKの場合、アイテム名箇所を、適当な数値に置換
                // かっこを付けることで、次のようなNGケースにおいて、正しくNGと判断させる
                /*
                            1           1.5         (1)
                [Q1][Q2]    11→OK×  1.51.5→NG○  (1)(1)→NG○
                [Q1]1       11→OK×  1.51→OK×    (1)1→NG○
                1[Q1]       11→OK×  11.5→OK×    1(1)→NG○
                */
                for (int i = 0; i < itemNamesList.Count; i++)
                {
                    Regex itemNameSearch = new Regex(@"\[" + itemNamesList[i] + @"\]");
                    if (itemNameValueList.ContainsKey(itemNamesList[i]))
                    {
                        string value = itemNameValueList[itemNamesList[i]];
                        expression = itemNameSearch.Replace(expression, value.ToString());// expression = itemNameSearch.Replace(expression, "(" + value.ToString() + ")");
                    }
                }
                // 値が無回答と非該当の場合、Key-Valueに無いアイテム名が残るので消す
                // expressionValue = regex.Replace(expression, string.Empty);//commented for FA values like "[1]" replace empty
            }

            //変換後文字列がNullの場合、元の文字列でセットする。
            if (expressionValue == null) expressionValue = expression;

            //// カンマの連続は1つに置換する
            //regex = new Regex(@"\,{2,}");
            //expressionValue = regex.Replace(expressionValue, ",");
            //// 先頭にカンマがあったら除去
            //regex = new Regex(@"\(\,");
            //expressionValue = regex.Replace(expressionValue, "（");
            //// 最後にカンマがあったら除去
            //regex = new Regex(@"\,\)");
            //expressionValue = regex.Replace(expressionValue, ")");

            // 全角の四則演算記号と括弧は半角で評価する。Mantis.0000453
            // expressionValue = ToStrNarrowFormula(expressionValue);

            return expressionValue;
        }
        public static string BuildExpressionFA__(string expression, List<string> itemNamesList = null, Dictionary<string, string> itemNameValueList = null)
        {
            if (string.IsNullOrWhiteSpace(expression)) return null;

            Regex regex = new Regex(@"\[([^:\[\]@\|='&\\\!\?<>\*/\r\n]+)\]");
            string expressionValue = null;
            if (regex.IsMatch(expression))
            {
                // OKの場合、アイテム名箇所を、適当な数値に置換
                // かっこを付けることで、次のようなNGケースにおいて、正しくNGと判断させる
                /*
                            1           1.5         (1)
                [Q1][Q2]    11→OK×  1.51.5→NG○  (1)(1)→NG○
                [Q1]1       11→OK×  1.51→OK×    (1)1→NG○
                1[Q1]       11→OK×  11.5→OK×    1(1)→NG○
                */
                for (int i = 0; i < itemNamesList.Count; i++)
                {
                    Regex itemNameSearch = new Regex(@"\[" + itemNamesList[i] + @"\]");
                    if (itemNameValueList.ContainsKey(itemNamesList[i]))
                    {
                        string value = itemNameValueList[itemNamesList[i]];
                        expression = itemNameSearch.Replace(expression, "\"" + value.ToString() + "\"");// expression = itemNameSearch.Replace(expression, "(" + value.ToString() + ")");
                    }
                }
                // 値が無回答と非該当の場合、Key-Valueに無いアイテム名が残るので消す
                // expressionValue = regex.Replace(expression, string.Empty);//commented for FA values like "[1]" replace empty
            }

            //変換後文字列がNullの場合、元の文字列でセットする。
            if (expressionValue == null) expressionValue = expression;

            //// カンマの連続は1つに置換する
            //regex = new Regex(@"\,{2,}");
            //expressionValue = regex.Replace(expressionValue, ",");
            //// 先頭にカンマがあったら除去
            //regex = new Regex(@"\(\,");
            //expressionValue = regex.Replace(expressionValue, "(");
            //// 最後にカンマがあったら除去
            //regex = new Regex(@"\,\)");
            //expressionValue = regex.Replace(expressionValue, ")");

            // 全角の四則演算記号と括弧は半角で評価する。Mantis.0000453
            //expressionValue = ToStrNarrowFormula(expressionValue);//replacing  japanese "( " with "("

            return expressionValue;
        }

        public static string BuildExpressionFA(string expression, List<string> itemNamesList = null, Dictionary<string, string> itemNameValueList = null)
        {
            if (string.IsNullOrWhiteSpace(expression)) return null;

            Regex regex = new Regex(@"\[([^:\[\]@\|='&\\\!\?<>\*/\r\n]+)\]");
            string expressionValue = null;
            if (regex.IsMatch(expression))
            {
                // OKの場合、アイテム名箇所を、適当な数値に置換
                // かっこを付けることで、次のようなNGケースにおいて、正しくNGと判断させる
                /*
                            1           1.5         (1)
                [Q1][Q2]    11→OK×  1.51.5→NG○  (1)(1)→NG○
                [Q1]1       11→OK×  1.51→OK×    (1)1→NG○
                1[Q1]       11→OK×  11.5→OK×    1(1)→NG○
                */
                for (int i = 0; i < itemNamesList.Count; i++)
                {
                    Regex itemNameSearch = new Regex(@"\[" + itemNamesList[i] + @"\]");
                    if (itemNameValueList.ContainsKey(itemNamesList[i]))
                    {
                        string value = itemNameValueList[itemNamesList[i]];
                        expression = itemNameSearch.Replace(expression, "(" + value.ToString() + ")");
                    }
                }
                // 値が無回答と非該当の場合、Key-Valueに無いアイテム名が残るので消す
                expressionValue = regex.Replace(expression, string.Empty);
            }

            //変換後文字列がNullの場合、元の文字列でセットする。
            if (expressionValue == null) expressionValue = expression;

            // カンマの連続は1つに置換する
            regex = new Regex(@"\,{2,}");
            expressionValue = regex.Replace(expressionValue, ",");
            // 先頭にカンマがあったら除去
            regex = new Regex(@"\(\,");
            expressionValue = regex.Replace(expressionValue, "(");
            // 最後にカンマがあったら除去
            regex = new Regex(@"\,\)");
            expressionValue = regex.Replace(expressionValue, ")");

            // 全角の四則演算記号と括弧は半角で評価する。Mantis.0000453
            expressionValue = ToStrNarrowFormula(expressionValue);

            return expressionValue;
        }
        public static string BuildExpressionFA_Temp(string expression, List<string> itemNamesList = null, Dictionary<string, string> itemNameValueList = null)
        {
            if (string.IsNullOrWhiteSpace(expression)) return null;

            Regex regex = new Regex(@"\[([^:\[\]@\|='&\\\!\?<>\*/\r\n]+)\]");
            string expressionValue = null;
            if (regex.IsMatch(expression))
            {
                // OKの場合、アイテム名箇所を、適当な数値に置換
                // かっこを付けることで、次のようなNGケースにおいて、正しくNGと判断させる
                /*
                            1           1.5         (1)
                [Q1][Q2]    11→OK×  1.51.5→NG○  (1)(1)→NG○
                [Q1]1       11→OK×  1.51→OK×    (1)1→NG○
                1[Q1]       11→OK×  11.5→OK×    1(1)→NG○
                */
                for (int i = 0; i < itemNamesList.Count; i++)
                {
                    Regex itemNameSearch = new Regex(@"\[" + itemNamesList[i] + @"\]");
                    if (itemNameValueList.ContainsKey(itemNamesList[i]))
                    {
                        string value = itemNameValueList[itemNamesList[i]];
                        expression = itemNameSearch.Replace(expression, "\"" + value.ToString() + "\"");// expression = itemNameSearch.Replace(expression, "(" + value.ToString() + ")");
                    }
                }
                // 値が無回答と非該当の場合、Key-Valueに無いアイテム名が残るので消す
                // expressionValue = regex.Replace(expression, string.Empty);//commented for altering string to add  double quotes for each variable name
            }

            //変換後文字列がNullの場合、元の文字列でセットする。
            if (expressionValue == null) expressionValue = expression;

            // カンマの連続は1つに置換する
            regex = new Regex(@"\,{2,}");
            expressionValue = regex.Replace(expressionValue, ",");
            // 先頭にカンマがあったら除去
            regex = new Regex(@"\(\,");
            expressionValue = regex.Replace(expressionValue, "(");
            // 最後にカンマがあったら除去
            regex = new Regex(@"\,\)");
            expressionValue = regex.Replace(expressionValue, ")");

            // 全角の四則演算記号と括弧は半角で評価する。Mantis.0000453
            // expressionValue = ToStrNarrowFormula(expressionValue);

            return expressionValue;
        }
        #endregion

        #region キャスト
        /// <summary>
        /// bool型の値をint型にキャストする静的メソッド
        /// </summary>
        /// <param name="Expression">bool型の値</param>
        /// <returns><paramref name="Expression"/>がtrueのとき-1(0xffffffff)、falseのとき0(0x00000000)</returns>
        public static int CInt(bool Expression)
        {
            return Expression ? -1 : 0;
        }

        /// <summary>
        /// int型の値をbool型にキャストする静的メソッド
        /// </summary>
        /// <param name="Expression">int型の値</param>
        /// <returns><paramref name="Expression"/>が0以外のときtrue、0のときfalse</returns>
        public static bool CBool(int Expression)
        {
            return Expression != 0;
        }

        /// <summary>
        /// double型の値をbool型にキャストする静的メソッド
        /// </summary>
        /// <param name="Expression">double型の値</param>
        /// <returns><paramref name="Expression"/>が0以外のときtrue、0のときfalse</returns>
        public static bool CBool(double Expression)
        {
            return Expression != 0.0;
        }

        /// <summary>
        /// short型の値をbool型にキャストする静的メソッド
        /// </summary>
        /// <param name="Expression">short型の値</param>
        /// <returns><paramref name="Expression"/>が0以外のときtrue、0のときfalse</returns>
        public static bool CBool(short Expression)
        {
            return Expression != (short)0;
        }

        /// <summary>
        /// long型の値をbool型にキャストする静的メソッド
        /// </summary>
        /// <param name="Expression">long型の値</param>
        /// <returns><paramref name="Expression"/>が0以外のときtrue、0のときfalse</returns>
        public static bool CBool(long Expression)
        {
            return Expression != 0L;
        }

        /// <summary>
        /// float型の値をbool型にキャストする静的メソッド
        /// </summary>
        /// <param name="Expression">float型の値</param>
        /// <returns><paramref name="Expression"/>が0以外のときtrue、0のときfalse</returns>
        public static bool CBool(float Expression)
        {
            return Expression != 0.0f;
        }

        /// <summary>
        /// decimal型の値をbool型にキャストする静的メソッド
        /// </summary>
        /// <param name="Expression">decimal型の値</param>
        /// <returns><paramref name="Expression"/>が0以外のときtrue、0のときfalse</returns>
        public static bool CBool(decimal Expression)
        {
            return Expression != 0.0M;
        }
        #endregion
    }
    #endregion

    #region DescComparerクラス
    /// <summary>
    /// 降順ソート用IComparerインターフェイス実装クラス
    /// </summary>
    [ComVisible(false), Guid("60E92524-5759-4FC2-94C2-960803557393")]
    public class DescComparer : System.Collections.IComparer
    {
        /// <summary>
        /// 降順ソート用比較メソッド
        /// </summary>
        /// <param name="x">比較元の値</param>
        /// <param name="y">比較対象の値</param>
        /// <returns>
        /// x &lt; y のとき 1
        /// x = y のとき 0
        /// x &gt; yのとき -1
        /// </returns>
        public int Compare(object x, object y)
        {
            try
            {
                if (x == null || y == null) throw new System.ArgumentNullException();   // nullはNG
                if (x.GetType() != y.GetType()) throw new System.ArgumentException();   // 同じ型のデータ比較でなければダメ
                if (!(x is IComparable)) throw new System.TypeAccessException();        // IComparableの実装クラスである必要あり
                return ((IComparable)x).CompareTo(y) * -1;
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                throw;  // 再スロー→弊害が生じるようであれば、return 0に変更
            }
        }
    }
    #endregion

    #region PasswordIOクラス
    /// <summary>
    /// パスワードファイルへのI/O処理を一元的に管理するクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("06185F91-8FC6-4375-8F1D-C7C73A11926C")]
    public class PasswordIO
    {
        #region メンバ変数宣言
        // パスワードファイルのパス
        private string passwordFilePath = null;
        // 暗号化キー
        private int key = 0x80;
        // マスクされた文字列群
        private string[] maskCharacters = new[]
                        {"?S`EaVMNx", "Fn0.C<Jm%", "cYaWbyGg=", "rK._jVdBT"
                       , "Wc[7?QgO&", "joCM1#DES", "<SOEAjCu'", "H]iU|K$fl"
                       , "GJrY5DbB{", "duE_IJU5v"};
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタのサブルーチン
        /// メンバー変数passwordFilePathを設定する
        /// </summary>
        /// <param name="PasswordFilePath">パスワードファイルのパス</param>
        private void init(string PasswordFilePath)
        {
            passwordFilePath = PasswordFilePath;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="PasswordFilePath">パスワードファイルのパス</param>
        public PasswordIO(string PasswordFilePath)
        {
            init(PasswordFilePath);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="PasswordFilePath">パスワードファイルのパス</param>
        /// <param name="Key">暗号化キー</param>
        public PasswordIO(string PasswordFilePath, int Key)
        {
            init(PasswordFilePath);
            key = Key;
        }
        #endregion

        #region 暗号化(または復号化)関連
        /// <alias>getEncodedString00</alias>
        /// <summary>
        /// 文字列をShift-JISで評価し、暗号化キー(変更なし)を使ってXor暗号化し、結果をバイト配列で返す
        /// 暗号化済バイト配列を返すgetEncodedStringオーバーロードメソッド群の根幹ロジック
        /// </summary>
        /// <param name="buffer">暗号化(または復号化)する文字列</param>
        /// <returns>暗号化(復号化)済バイト配列</returns>
        private byte[] getEncodedString(string buffer)
        {
            // Shift-JISでのバイト配列を取得
            byte[] byteBuffer = System.Text.Encoding.GetEncoding(932).GetBytes(buffer);
            // 暗号化
            for (int i = 0; i < byteBuffer.Length; i++)
            {
                byteBuffer[i] = (byte)((int)byteBuffer[i] ^ key);
            }
            return byteBuffer;
        }

        /// <alias>getEncodedString10</alias>
        /// <summary>
        /// バイト配列を暗号化キー(変更なし)を使ってXor暗号化し、Shift-JISで文字列に変換して返す
        /// 暗号化済文字列を返すgetEncodedStringオーバーロードメソッド群の根幹ロジック
        /// </summary>
        /// <param name="byteBuffer">暗号化(復号化)するバイト配列</param>
        /// <returns>暗号化(復号化)済文字列</returns>
        private string getEncodedString(byte[] byteBuffer)
        {
            // 暗号化
            for (int i = 0; i < byteBuffer.Length; ++i)
            {
                byteBuffer[i] = (byte)((int)byteBuffer[i] ^ key);
            }
            // Shift-JISでバイト配列を評価して文字列を返す
            return System.Text.Encoding.GetEncoding(932).GetString(byteBuffer);
        }

        /// <alias>getEncodedString01</alias>
        /// <summary>
        /// 文字列をShift-JISで評価し、暗号化キーを使ってXor暗号化し、結果をバイト配列で返す
        /// 暗号化キーの変更を行ってから、getEncodedString00に仲介
        /// </summary>
        /// <param name="buffer">暗号化(または復号化)する文字列</param>
        /// <param name="Key">暗号化キー</param>
        /// <returns>暗号化(復号化)済バイト配列</returns>
        private byte[] getEncodedString(string buffer, int Key)
        {
            key = Key;
            return getEncodedString(buffer);
        }

        /// <alias>getEncodedString11</alias>
        /// <summary>
        /// バイト配列を暗号化キーを使ってXor暗号化し、Shift-JISで文字列に変換して返す
        /// 暗号化キーの変更を行ってから、getEncodedString10に仲介
        /// </summary>
        /// <param name="byteBuffer">暗号化(復号化)するバイト配列</param>
        /// <param name="Key">暗号化キー</param>
        /// <returns>暗号化(復号化)済文字列</returns>
        private string getEncodedString(byte[] byteBuffer, int Key)
        {
            key = Key;
            return getEncodedString(byteBuffer);
        }
        #endregion

        #region マスク関連
        /// <alias>getCharactersTable</alias>
        /// <summary>
        /// マスクされた文字群内の文字からその位置を返すHashtableオブジェクトを生成して参照を返す
        /// </summary>
        /// <param name="index">マスクされた文字群配列内で使用する文字群のインデックス</param>
        /// <returns>
        /// 成功時:マスクされた文字群内の文字からその位置を返すHashtableオブジェクトへの参照
        /// 失敗時:null
        /// </returns>
        private System.Collections.Hashtable getCharactersTable(int index)
        {
            // 引数不正時
            if (index < maskCharacters.GetLowerBound(0) || index > maskCharacters.GetUpperBound(0))
            {
                return null;
            }
            string maskCharacter = maskCharacters[index];
            // Hashtableオブジェクトの生成
            System.Collections.Hashtable tmp = new System.Collections.Hashtable();
            for (int i = 0; i < maskCharacter.Length;)
            {
                tmp.Add(maskCharacter.Substring(i, 1), ++i);
            }
            return tmp;
        }

        /// <alias>getConsealedPassword</alias>
        /// <summary>
        /// パスワードを一見しただけではわからない文字列にして返す
        /// 1文字目は、使用するマスクされた文字群の配列内のインデックス
        /// パスワード一文字ごとにブロックを持ち、ブロックは「XabcdYefgh」の形で構成される
        /// このときXは、Yの位置を示す文字で、マスクされた文字群内のXの位置がYの位置を示す
        /// 各ブロックの文字数は7文字～10文字の間のランダムな値とし、abcdefghは、いずれもランダムな文字とする
        /// 各ブロックは文字コード0x01の文字で区切られ、2文字目以降で構成する
        /// </summary>
        /// <param name="password">隠蔽するパスワード</param>
        /// <returns>
        /// 成功時:隠蔽済みパスワード
        /// 失敗時:null
        /// </returns>
        private string getConsealedPassword(string password)
        {
            // ランダムな文字に使用する文字群
            const string CONSEAL_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!\"#$%&'()=~|`{+*}<>?_-^\\@[;:],./";
            // 引数がnullまたは空文字列、あるいは空白文字だけで構成されている場合
            if (String.IsNullOrWhiteSpace(password)) return null;
            // 各ブロックの文字列を要素とする配列
            string[] newBuffer = new string[password.Length];
            // 乱数オブジェクトの生成
            System.Random rnd = new System.Random();
            // 使用するマスクされた文字群の配列内インデックスをランダムに取得
            int idx = rnd.Next(10);
            // 各ブロックの文字列生成
            for (int i = 0; i < newBuffer.Length; i++)
            {
                // ブロックの文字列長は、1 + 6～9の範囲でランダムに取得(lengthは、1文字目を除いた長さ)
                int length = rnd.Next(6, 10);
                // ブロック内への文字出力位置をランダムに取得
                int pos = rnd.Next(length) + 1;
                // ブロック内の各文字を要素とする配列
                string[] tmp = new string[length + 1];
                // 1文字目は、文字出力位置を示すマスクされた文字
                tmp[0] = maskCharacters[idx].Substring(pos - 1, 1);
                // 1 + pos文字目は、passwordのi + 1文字目
                tmp[pos] = password.Substring(i, 1);
                // 残りの文字をランダムに生成
                for (int j = 1; j < tmp.Length; j++)
                {
                    if (j != pos)
                    {
                        string c = CONSEAL_CHARACTERS.Substring(rnd.Next(CONSEAL_CHARACTERS.Length), 1);
                        tmp[j] = c;
                    }
                }
                newBuffer[i] = string.Join("", tmp);
            }
            // 使用するマスクされた文字群の配列内インデックスを1文字目とし、
            // 各ブロックは文字コード0x01の文字を区切り文字として連結して返す
            return idx.ToString() + string.Join(((char)0x01).ToString(), newBuffer);
        }

        /// <alias>getUnconsealedPassword</alias>
        /// <summary>
        /// getConsealedPasswordで隠蔽したパスワードを復元して返す
        /// </summary>
        /// <param name="consealedPassword">隠蔽済みパスワード</param>
        /// <returns>
        /// 成功時:復元したパスワード
        /// 失敗時:null
        /// </returns>
        private string getUnconsealedPassword(string consealedPassword)
        {
            // 引数がnullまたは8文字未満の場合
            if (consealedPassword == null || consealedPassword.Length < 8) return null;
            try
            {
                // 使用するマスクされた文字群の配列内インデックス
                int idx = int.Parse(consealedPassword.Substring(0, 1));
                // マスクされた文字群内の文字からその位置を返すHashtableオブジェクトへの参照
                System.Collections.Hashtable charactersTable = getCharactersTable(idx);
                // 1文字目をカットして、各ブロックに分割
                string[] newBuffer = consealedPassword.Substring(1).Split((char)0x01);
                // 各ブロック内の必要文字を抽出
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    int pos = int.Parse(charactersTable[newBuffer[i].Substring(0, 1)].ToString());
                    newBuffer[i] = newBuffer[i].Substring(pos, 1);
                }
                return string.Join("", newBuffer);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region パスワードファイルとのI/O関連
        /// <alias>outputPassword00</alias>
        /// <summary>
        /// <para>エイリアス:outputPassword00</para>
        /// パスワードを隠蔽して暗号化し、パスワードファイルに出力するoutputPasswordオーバーロードメソッド群の根幹ロジック
        /// </summary>
        /// <param name="password">パスワード</param>
        /// <param name="lineIndex">隠蔽済みパスワード内での0ベース行インデックス(行区切りLF)</param>
        /// <returns>
        /// 成功時にはtrue、失敗時にはfalseを返す
        /// </returns>
        public bool outputPassword(string password, int lineIndex)
        {
            try
            {
                // 引数不正時
                if (String.IsNullOrWhiteSpace(password) || lineIndex < 0) return false;
                // 出力するバイト配列
                byte[] byteBuffer;
                // パスワードファイルが存在する場合
                if (System.IO.File.Exists(passwordFilePath))
                {
                    // 全体読み込み
                    using (System.IO.FileStream stream = new System.IO.FileStream(passwordFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        try
                        {
                            byteBuffer = new byte[stream.Length];
                            stream.Read(byteBuffer, 0, (int)stream.Length);
                            stream.Close();
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    // 復号化
                    string buffer = getEncodedString(byteBuffer);
                    // 各行に分割(最大lineIndex + 2個)
                    string[] tmp = buffer.Split(new Char[] { (char)0x0A }, lineIndex + 2);
                    // 行数不足時は追加
                    if (lineIndex > tmp.GetUpperBound(0))
                    {
                        Array.Resize(ref tmp, lineIndex + 1);
                    }
                    // 隠蔽済みパスワードに置き換え
                    tmp[lineIndex] = getConsealedPassword(password);
                    // 行区切りで連結して暗号化済みバイト配列を取得
                    byteBuffer = getEncodedString(string.Join(((char)0x0A).ToString(), tmp));
                }
                else // パスワードファイルが存在しない場合
                {
                    // lineIndex + 1行分の配列
                    string[] tmp = new string[lineIndex + 1];
                    // 隠蔽済みパスワードを取得
                    tmp[lineIndex] = getConsealedPassword(password);
                    // 行区切りで連結して暗号化済みバイト配列を取得
                    byteBuffer = getEncodedString(string.Join(((char)0x0A).ToString(), tmp));
                }
                // バイト配列の出力
                string dirpath = System.IO.Path.GetDirectoryName(passwordFilePath);
                Common.GlobalMethodClass.GuaranteeDirectoryExist(dirpath);
                using (System.IO.FileStream stream = new System.IO.FileStream(passwordFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    try
                    {
                        stream.Write(byteBuffer, 0, byteBuffer.Length);
                        stream.Close();
                    }
                    catch
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <alias>outputPassword01</alias>
        /// <summary>
        /// <para>エイリアス:outputPassword01</para>
        /// パスワードを隠蔽して暗号化し、パスワードファイルに出力する<br />
        /// 引数lineIndexに0を指定して、outputPassword00に仲介
        /// </summary>
        /// <param name="password">パスワード</param>
        /// <returns>
        /// 成功時にはtrue、失敗時にはfalseを返す
        /// </returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.PasswordIO.outputPassword(System.String,System.Int32)">outputPassword00</seealso>
        public bool outputPassword(string password)
        {
            return outputPassword(password, 0);
        }

        /// <alias>getPassword00</alias>
        /// <summary>
        /// <para>エイリアス:getPassword00</para>
        /// パスワードファイルからパスワードを取得して返すgetPasswordオーバーロードメソッド群の根幹ロジック
        /// </summary>
        /// <param name="lineIndex">隠蔽済みパスワード内での0ベース行インデックス(行区切りLF)</param>
        /// <returns>
        /// 成功時には指定行に登録されているパスワードを返す<br />
        /// 失敗時にはnullを返す
        /// </returns>
        public string getPassword(int lineIndex)
        {
            // パスワードファイルが存在しない場合
            if (!System.IO.File.Exists(passwordFilePath)) return null;
            // 全体読み込み
            using (System.IO.FileStream stream = new System.IO.FileStream(passwordFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                try
                {
                    byte[] byteBuffer = new byte[stream.Length];
                    stream.Read(byteBuffer, 0, (int)stream.Length);
                    // 復号化
                    string buffer = getEncodedString(byteBuffer);
                    // 各行に分割(最大LineIndex + 2個)
                    string[] tmp = buffer.Split(new Char[] { (char)0x0A }, lineIndex + 2);
                    // LineIndex + 1行目を復元して返す
                    return getUnconsealedPassword(tmp[lineIndex]);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <alias>getPassword01</alias>
        /// <summary>
        /// <para>エイリアス:getPassword01</para>
        /// パスワードファイルからパスワードを取得して返す<br />
        /// 引数lineIndexに0を指定して、getPassword00に仲介
        /// </summary>
        /// <returns>
        /// 成功時には1行目に登録されているパスワードを返す<br />
        /// 失敗時にはnullを返す
        /// </returns>
        /// <seealso cref="M:Macromill.QCWeb.Common.PasswordIO.getPassword(System.Int32)">getPassword00</seealso>
        public string getPassword()
        {
            return getPassword(0);
        }
        #endregion
    }
    #endregion

    #region Functionクラス
    /// <summary>
    /// 数学的関数をまとめた静的クラス
    /// </summary>
    [ComVisible(false), Guid("749BF366-CBDC-42ff-BD84-358C4FC84B19")]
    public static class Function
    {
        /// <summary>
        /// 比較演算子列挙型
        /// </summary>
        public enum CompareOperator
        {
            /// <summary>
            /// 「○と等しい」を表す (= 0)<br />
            /// </summary>
            Equal,
            /// <summary>
            /// 「○と等しくない」を表す (= 1)<br />
            /// </summary>
            NotEqual,
            /// <summary>
            /// 「○より大きい」を表す (= 2)<br />
            /// </summary>
            Greater,
            /// <summary>
            /// 「○以上」を表す (= 3)<br />
            /// </summary>
            GreaterEqual,
            /// <summary>
            /// 「○より小さい」(= 4)<br />
            /// </summary>
            Less,
            /// <summary>
            /// 「○以下」(= 5)<br />
            /// </summary>
            LessEqual,
        }

        //private static double DOUBLE_EPSILON = Math.Pow(2.0, (1 - 53));
        //private const double DOUBLE_EPSILON = 2.2204460492503131E-16;
        //private static double DOUBLE_EPSILON = Math.Pow(2.0, (1 - 35));
        private const double DOUBLE_EPSILON = 5.8207660913467407E-11;


        ///// <summary>
        ///// 計算機イプシロン（machine epsilon）、倍精度における「1より大きい最小の数」と1との差
        ///// </summary>
        //public static double Epsilon
        //{
        //    get { return DOUBLE_EPSILON; }
        //}

        /// <summary>
        /// 微小値を算出する
        /// </summary>
        /// <param name="v">対象となる数値</param>
        /// <returns>微小値</returns>
        public static double CreateEpsilon(double v)
        {
            double eps;
            if (v == 0.0 || double.IsNaN(v) || double.IsInfinity(v))
            {
                eps = DOUBLE_EPSILON;
            }
            else
            {
                eps = Math.Abs(v) * DOUBLE_EPSILON;
            }
            return eps;
        }

        /// <summary>
        /// double型の比較演算において、比較対象の数字に非数値、無限大が含まれていた場合に利用する
        /// </summary>
        /// <param name="a">比較対象元の数値</param>
        /// <param name="ope">比較演算子の列挙型</param>
        /// <param name="b">比較対象先の数値</param>
        /// <returns>比較結果</returns>
        private static bool CompareEx(double a, CompareOperator ope, double b)
        {
            bool result = false;
            switch (ope)
            {
                case CompareOperator.GreaterEqual:
                    result = a >= b;
                    break;
                case CompareOperator.Greater:
                    result = a > b;
                    break;
                case CompareOperator.LessEqual:
                    result = a <= b;
                    break;
                case CompareOperator.Less:
                    result = a < b;
                    break;
                case CompareOperator.Equal:
                    result = a == b;
                    break;
                case CompareOperator.NotEqual:
                    result = a != b;
                    break;
                default:
                    return false;
            }

            return result;
        }

        /// <summary>
        /// double型の比較演算において、数値誤差を考慮した結果を返す
        /// 使い方は、aはbより大きい、aはb以上、aはbより小さい、aはb以下、aはbと等しい、aはbと等しくない、の6種類
        /// </summary>
        /// <param name="a">比較対象元の数値(数値誤差を含んでいる)</param>
        /// <param name="ope">比較演算子の列挙型</param>
        /// <param name="b">比較対象先の数値</param>
        /// <returns>数値誤差を考慮した比較結果</returns>
        public static bool Compare(double a, CompareOperator ope, double b)
        {
            if (double.IsNaN(a) || double.IsInfinity(a) || double.IsNaN(b) || double.IsInfinity(b))
            {
                return CompareEx(a, ope, b);
            }

            bool result = false;
            double epsA = CreateEpsilon(a);
            double epsB = CreateEpsilon(b);
            double eps = Math.Max(epsA, epsB);

            switch (ope)
            {
                case CompareOperator.GreaterEqual:
                    //result = (a + eps) >= b;
                    result = (a + eps) > b;
                    break;
                case CompareOperator.Greater:
                    //result = (a - eps) > b;
                    result = (a - eps) >= b;
                    break;
                case CompareOperator.LessEqual:
                    //result = (a - eps) <= b;
                    result = (a - eps) < b;
                    break;
                case CompareOperator.Less:
                    //result = (a + eps) < b;
                    result = (a + eps) <= b;
                    break;
                case CompareOperator.Equal:
                    result = Math.Abs(a - b) < eps;
                    break;
                case CompareOperator.NotEqual:
                    result = Math.Abs(a - b) >= eps;
                    break;
                default:
                    return false;
            }

            return result;
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
        public static decimal RoundOff(decimal number, int numdigitsafterdecimal)
        {
            // decimal n = (decimal)(Math.Pow(10.0, (double)(numdigitsafterdecimal)));
            // return Math.Truncate((number * n) + (decimal)0.5 * Math.Sign(number)) / n;
            // 簡単にできるようになっていたらしい
            return Math.Round(number, numdigitsafterdecimal, MidpointRounding.AwayFromZero);
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
        public static double RoundOff(double number, int numdigitsafterdecimal)
        {
            // return (double)(RoundOff((decimal)number, numdigitsafterdecimal));
            return Math.Round(number, numdigitsafterdecimal, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 四捨五入を行った結果を返す
        /// </summary>
        /// <param name="number">元の値</param>
        /// <returns>整数に四捨五入した結果を返す</returns>
        public static decimal RoundOff(decimal number)
        {
            // return RoundOff(number, 0);
            return Math.Round(number, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 四捨五入を行った結果を返す
        /// </summary>
        /// <param name="number">元の値</param>
        /// <returns>整数に四捨五入した結果を返す</returns>
        public static double RoundOff(double number)
        {
            // return RoundOff(number, 0);
            return Math.Round(number, MidpointRounding.AwayFromZero);
        }

#if AFTER_2ND_PHASE
        private static Dictionary<decimal, decimal> sqrtDic = new Dictionary<decimal,decimal>();
        private static Dictionary<decimal, decimal> expDic = new Dictionary<decimal,decimal>();
        private static Dictionary<decimal, decimal> sinDic = new Dictionary<decimal,decimal>();
        private static Dictionary<decimal, decimal> logDic = new Dictionary<decimal, decimal>();
        private static Dictionary<string, decimal> powDic = new Dictionary<string,decimal>();
        private static Dictionary<decimal, decimal> loggammaDic = new Dictionary<decimal, decimal>();
        private static Dictionary<string, decimal> betaDic = new Dictionary<string,decimal>();
        private static Dictionary<string, decimal> betaIncompleteDic = new Dictionary<string,decimal>();
        private static Dictionary<string, decimal> tdistDic = new Dictionary<string, decimal>();

        private static void AddDictionaryItem<TKey>(Dictionary<TKey, decimal> dic, TKey key, decimal value)
        {
            const int MAX_ITEMS_COUNT = 1000000;
            if (dic.Count >= MAX_ITEMS_COUNT) return;
            dic.Add(key, value);
        }

        /// <summary>
        /// 高精度で平方根を返す
        /// </summary>
        /// <param name="num"></param>
        /// <returns>十進型の平方根</returns>
        public static decimal Sqrt(decimal num)
        {
            if (num <= 0.0m) return 0.0m;
            decimal x = 0.0m;
            if (sqrtDic.TryGetValue(num, out x)) return x;
            x = (decimal)Math.Sqrt((double)num);
            for (int i = 1; i <= 250; ++i)
            {
                decimal old_x = x;
                x = (x + num / x) / 2.0m;
                if (x == old_x) break;
            }
            AddDictionaryItem<decimal>(sqrtDic, num, x);
            return x;
        }

        private const decimal e = 2.7182818284590452353602874714m;

        /// <summary>
        /// 高精度の指数関数
        /// </summary>
        /// <param name="num"></param>
        /// <returns>十進型のネイピア数(e)の<paramref name="num"/>乗</returns>
        public static decimal Exp(decimal num)
        {
            if (num == 0.0m) return 1.0m;
            if (num == 1.0m) return e;
            if (num < 0.0m) return 1.0m / Exp(-num);
            decimal x = 0.0m;
            if (expDic.TryGetValue(num, out x)) return x;
            if (num > 1.0m)
            {
                decimal n = Math.Floor(num);
                decimal tmp = e;
                for (decimal i = 2.0m; i <= n; ++i)
                {
                    tmp *= e;
                }
                tmp *= Exp(num - n);
                AddDictionaryItem<decimal>(expDic, num, tmp);
                return tmp; 
            }
            // 0 < n < 1
            x = 1.0m;
            decimal nn = 1.0m;
            decimal y = 1.0m;
            for (int i = 1; i <= 27; ++i)
            {
                decimal old_x = x;
                nn *= num;
                y *= (decimal)i;
                x += nn / y;
                if (x == old_x) break;
            }
            AddDictionaryItem<decimal>(expDic, num, x);
            return x;
        }

        private const decimal PI = 3.1415926535897932384626433833m;
        private const decimal LOG10 = 2.3025850929940456840179914547m;
        private static readonly decimal halfPI = PI / 2.0m;
        private static readonly decimal wPI = PI * 2.0m;

        /// <summary>
        /// 高精度の正弦関数
        /// </summary>
        /// <param name="num"></param>
        /// <returns>十進型の正弦</returns>
        public static decimal Sin(decimal num)
        {
            if (num == 0.0m) return 0.0m;
            if (num == halfPI) return 1.0m;
            if (num < 0.0m) return -Sin(-num);
            if (num > halfPI)
            {
                if (num <= PI) return Sin(PI - num);
                if (num <= wPI) return -Sin(wPI - num);
                num -= wPI;
                while (true)
                {
                    decimal tmp = num - wPI;
                    if (tmp < 0.0m) break;
                    num = tmp;
                }
                return Sin(num);
            }
            decimal x = 0.0m;
            if (sinDic.TryGetValue(num, out x)) return x;
            // 0 < num < π/2
            x = num;
            decimal nn = num;
            decimal y = 1.0m;
            for (int i = 2; i <= 14; ++i)
            {
                nn *= num * num;
                y *= 2.0m * ((decimal)i - 1.0m) * (2.0m * (decimal)i - 1.0m);
                decimal old_x = x;
                x += (i % 2 == 0 ? -1.0m : 1.0m) * nn / y;
                if (x == old_x) break;
            }
            AddDictionaryItem<decimal>(sinDic, num, x);
            return x;
        }

        /// <summary>
        /// 高精度の対数関数
        /// </summary>
        /// <param name="num"></param>
        /// <returns>十進型の自然対数</returns>
        public static decimal Log(decimal num)
        {
            if (num < 0.0m) return 0.0m;
            if (num == 10.0m) return LOG10;
            if (num < 0.1m)
            {
                decimal tmpNum = 0m;
                while (num < 0.1m)
                {
                    num *= 10.0m;
                    ++tmpNum;
                }
                return Log(num) - tmpNum * LOG10;
            }
            if (num > 10m)
            {
                decimal tmpNum = 0m;
                while (num > 10m)
                {
                    num /= 10.0m;
                    ++tmpNum;
                }
                return Log(num) + tmpNum * LOG10;
            }
            decimal x = 0.0m;
            if (logDic.TryGetValue(num, out x)) return x;
            decimal n = (num - 1.0m) / (num + 1.0m);
            x = n;
            decimal nn = n;
            decimal y = 1.0m;
            for (int i = 2; i <= 1000000; ++i)
            {
                y += 2.0m;
                nn *= n * n;
                decimal old_x = x;
                x += nn / y;
                if (x == old_x) break;
            }
            x *= 2.0m;
            AddDictionaryItem<decimal>(logDic, num, x);
            return x;
        }

        private static decimal powM(decimal num, decimal m)
        {
            if (m == 0.0m) return 1.0m;
            if (m == 1.0m) return num;
            if (m == 2.0m) return num * num;
            string key = num.ToString() + "_" + m.ToString();
            decimal res = 0.0m;
            if (powDic.TryGetValue(key, out res)) return res;
            if (m % 2.0m == 0.0m)
            {
                decimal tmp = powM(num, m / 2.0m);
                res = tmp * tmp;
            }
            else
            {
                decimal tmp = powM(num, (m - 1.0m) / 2.0m);
                res = tmp * tmp * num;
            }
            AddDictionaryItem<string>(powDic, key, res);
            return res;
        }

        private static decimal powX(decimal num, decimal x)
        {
            if (x == 0.0m) return 1.0m;
            string key = num.ToString() + "_" + x.ToString();
            decimal res = 0.0m;
            if (powDic.TryGetValue(key, out res)) return res;
            decimal n = 1.0m;
            res = 1.0m;
            while (true)
            {
                n /= 2.0m;
                num = Sqrt(num);
                if (x >= n)
                {
                    x -= n;
                    decimal old_res = res;
                    res *= num;
                    if (res == old_res) break;
                }
            }
            AddDictionaryItem<string>(powDic, key, res);
            return res;
        }

        /// <summary>
        /// 高精度べき乗関数
        /// </summary>
        /// <param name="num">基数</param>
        /// <param name="n">指数</param>
        /// <returns>十進数のべき乗</returns>
        public static decimal Pow(decimal num, decimal n)
        {
            if (n < 0.0m) return 1.0m / Pow(num, -n);
            decimal m = Math.Floor(n);
            decimal x = n - m;
            return powM(num, m) * powX(num, x);
        }
#endif

        /// <summary>
        /// 重複部分の相関係数を算出して返す静的メソッド
        /// </summary>
        /// <param name="N0">第1群と第2群の両方に該当するサンプルのWB値のサマリ</param>
        /// <param name="X0">第1群と第2群の両方に該当するサンプルで、第1群の回答データ(SA/MAでは1/0)×第2群の回答データ(SA/MAでは1/0)×WB値のサマリ</param>
        /// <param name="X1">第1群と第2群の両方に該当するサンプルで、第1群の回答データ(SA/MAでは1/0)×WB値のサマリ</param>
        /// <param name="X2">第1群と第2群の両方に該当するサンプルで、第2群の回答データ(SA/MAでは1/0)×WB値のサマリ</param>
        /// <param name="Y1">第1群と第2群の両方に該当するサンプルで、第1群の回答データ(SA/MAでは1/0)の平方×WB値のサマリ</param>
        /// <param name="Y2">第1群と第2群の両方に該当するサンプルで、第2群の回答データ(SA/MAでは1/0)の平方×WB値のサマリ</param>
        /// <returns>重複部分の相関係数Z</returns>
        public static double Z(double N0, double X0, double X1, double X2, double Y1, double Y2)
        {
            string ErrorParameterName = null;
            double ErrorParameterValue = 0.0;
            //if (N0 < 0.0)
            if (Compare(N0, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "N0";
                ErrorParameterValue = N0;
            }
            /*
            else if (X0 < 0.0)
            {
                ErrorParameterName = "X0";
                ErrorParameterValue = X0;
            }
            else if (X1 < 0.0)
            {
                ErrorParameterName = "X1";
                ErrorParameterValue = X1;
            }
            else if (X2 < 0.0)
            {
                ErrorParameterName = "X2";
                ErrorParameterValue = X2;
            }
            */
            //else if (Y1 < 0.0)
            else if (Compare(Y1, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "Y1";
                ErrorParameterValue = Y1;
            }
            //else if (Y2 < 0.0)
            else if (Compare(Y2, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "Y2";
                ErrorParameterValue = Y2;
            }
            if (ErrorParameterName != null)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, ErrorParameterName, ErrorParameterValue.ToString());
            }
            //if (N0 == 0.0) return 0.0;
            if (Compare(N0, CompareOperator.Equal, 0.0)) return 0.0;
            double tmp1 = N0 * Y1 - Math.Pow(X1, 2.0);
#if ROUNDOFF_TDISTRIBUTION
            double error = 1E-13;
            if (Math.Abs(tmp1) < error) tmp1 = 0.0;
#endif
            //if (tmp1 <= 0.0) return 0.0;
            if (Compare(tmp1, CompareOperator.LessEqual, 0.0)) return 0.0;
            double tmp2 = N0 * Y2 - Math.Pow(X2, 2.0);
#if ROUNDOFF_TDISTRIBUTION
            if (Math.Abs(tmp2) < error) tmp2 = 0.0;
#endif
            //if (tmp2 <= 0.0) return 0.0;
            if (Compare(tmp2, CompareOperator.LessEqual, 0.0)) return 0.0;
            // 誤差の拡張を抑えるように、必要に応じて分母の有理化
            if (tmp1 > 1.0 ^ tmp2 > 1.0)
            //if (Compare(tmp1, CompareOperator.Greater, 1.0) ^ Compare(tmp2, CompareOperator.Greater, 1.0))
            {
                if (tmp1 > 1.0)
                //if (Compare(tmp1, CompareOperator.Greater, 1.0))
                {
                    return (N0 * X0 - X1 * X2) * Math.Sqrt(tmp1) / (tmp1 * Math.Sqrt(tmp2));
                }
                return (N0 * X0 - X1 * X2) * Math.Sqrt(tmp2) / (Math.Sqrt(tmp1) * tmp2);
            }
            double tmp = tmp1 * tmp2;
            if (tmp1 > 1.0)
            //if (Compare(tmp1, CompareOperator.Greater, 1.0))
            {
                return (N0 * X0 - X1 * X2) * Math.Sqrt(tmp) / tmp;
            }
            return (N0 * X0 - X1 * X2) / Math.Sqrt(tmp);
        }

        /// <summary>
        /// 検定統計量を算出して返す静的メソッド
        /// <note>SA/MAで使用</note>
        /// </summary>
        /// <param name="N0">第1群と第2群の両方に該当するサンプルのWB値のサマリ</param>
        /// <param name="N1">第1群に該当するサンプルのWB値のサマリ</param>
        /// <param name="N2">第2群に該当するサンプルのWB値のサマリ</param>
        /// <param name="X1">第1群に反応したサンプルのWB値のサマリ</param>
        /// <param name="X2">第2群に反応したサンプルのWB値のサマリ</param>
        /// <param name="q0">第1群と第2群の両方に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q1">第1群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q2">第2群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="Z">重複部分の相関係数</param>
        /// <param name="p1">第1群に反応したサンプルの比率 (戻り値)</param>
        /// <param name="p2">第2群に反応したサンプルの比率 (戻り値)</param>
        /// <param name="p12">第1群に反応したサンプルと第2群に反応したサンプルの平均比率 (戻り値)</param>
        /// <param name="c">連続性の補正量 (戻り値)</param>
        /// <param name="e0">第1群と第2群の両方に該当するサンプルのEffective Base (戻り値)</param>
        /// <returns>検定統計量t</returns>
        public static double t(double N0, double N1, double N2, double X1, double X2, double q0, double q1, double q2, double Z
                             , out double p1, out double p2, out double p12, out double c, out double e0)
        {
            string ErrorParameterName = null;
            double ErrorParameterValue = 0.0;
            //if (N0 < 0.0)
            if (Compare(N0, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "N0";
                ErrorParameterValue = N0;
            }
            //else if (N1 < X1 || N1 < N0)
            else if (Compare(N1, CompareOperator.Less, X1) || Compare(N1, CompareOperator.Less, N0))
            {
                ErrorParameterName = "N1";
                ErrorParameterValue = N1;
            }
            //else if (N2 < X2 || N2 < N0)
            else if (Compare(N2, CompareOperator.Less, X2) || Compare(N2, CompareOperator.Less, N0))
            {
                ErrorParameterName = "N2";
                ErrorParameterValue = N2;
            }
            //else if (X1 < 0.0)
            else if (Compare(X1, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "X1";
                ErrorParameterValue = X1;
            }
            //else if (X2 < 0.0)
            else if (Compare(X2, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "X2";
                ErrorParameterValue = X2;
            }
            //else if (q0 < 0.0)
            else if (Compare(q0, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "q0";
                ErrorParameterValue = q0;
            }
            //else if (q1 < 0.0)
            else if (Compare(q1, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "q1";
                ErrorParameterValue = q1;
            }
            //else if (q2 < 0.0)
            else if (Compare(q2, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "q2";
                ErrorParameterValue = q2;
            }
            if (ErrorParameterName != null)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, ErrorParameterName, ErrorParameterValue.ToString());
            }
            // p1 = 0.0;
            // p2 = 0.0;
            // p12 = 0.0;
            // c = 0.0;
            // e0 = 0.0;
            p1 = double.NaN;
            p2 = double.NaN;
            p12 = double.NaN;
            c = double.NaN;
            e0 = double.NaN;

            // TODO p1,p2計算位置移動
            p1 = X1 / N1;
            p2 = X2 / N2;
            //if (N1 == 0.0 || N2 == 0.0) return 0.0;
            //if (X1 == 0.0 && X2 == 0.0) return 0.0;
            //if (X1 == N1 && X2 == N2) return 0.0;
            //if (q1 == 0.0 || q2 == 0.0) return 0.0;
            if (Compare(N1, CompareOperator.Equal, 0.0) || Compare(N2, CompareOperator.Equal, 0.0)) return 0.0;
            if (Compare(X1, CompareOperator.Equal, 0.0) && Compare(X2, CompareOperator.Equal, 0.0)) return 0.0;
            if (Compare(X1, CompareOperator.Equal, N1) && Compare(X2, CompareOperator.Equal, N2)) return 0.0;
            if (Compare(q1, CompareOperator.Equal, 0.0) || Compare(q2, CompareOperator.Equal, 0.0)) return 0.0;
            e0 = 0.0;
            /*
            double tmp = 0.0;
            if (q0 == 0)
            {
                tmp = (X1 + X2) * (N1 + N2 - (X1 + X2)) * (Math.Pow(N2, 2.0) * q1 + Math.Pow(N1, 2.0) * q2);
                q0 = 1.0;
            }
            else
            {
                double tmp1 = q1 * q0 * Math.Pow(N2, 2.0) + q2 * q0 * Math.Pow(N1, 2.0) - 2 * Math.Pow(N0, 2.0) * q1 * q2 * Z;
                if (tmp1 <= 0.0) return 0.0;
                tmp = (X1 + X2) * (N1 + N2 - (X1 + X2)) * tmp1;
            }
            return 0.5 * Math.Sign(N2 * X1 - N1 * X2) * (N1 + N2) * (2 * N1 * N2 * Math.Abs(N2 * X1 - N1 * X2) - (Math.Pow(N2, 2.0) * q1 + Math.Pow(N1, 2.0) * q2)) * Math.Sqrt(q0 * tmp) / (N1 * N2 * tmp);
            */
            // TODO: #256298 p12計算位置移動
            p12 = (X1 + X2) / (N1 + N2);

            double recE1 = q1 / Math.Pow(N1, 2.0);
            double recE2 = q2 / Math.Pow(N2, 2.0);
            c = 0.5 * Math.Sign(p1 - p2) * (recE1 + recE2);
            // TODO: #256298 e0計算位置移動
            double tmp = recE1 + recE2;
            //if (q0 > 0.0 && N0 > 0.0)
            if (Compare(q0, CompareOperator.Greater, 0.0) && Compare(N0, CompareOperator.Greater, 0.0))
            {
                e0 = Math.Pow(N0, 2.0) / q0;
                tmp -= 2.0 * e0 * recE1 * recE2 * Z;
            }

            //if (Math.Abs(p1 - p2) - Math.Abs(c) <= 0) return 0.0;
            if (Compare(Math.Abs(p1 - p2) - Math.Abs(c), CompareOperator.LessEqual, 0)) return 0.0;
            if (Compare(tmp, CompareOperator.LessEqual, 0.0)) return 0.0;
            // 誤差の拡張を抑えるように、必要に応じて分母の有理化
            if (tmp > 1.0)
            //if (Compare(tmp, CompareOperator.Greater, 1.0))
            {
                return (p1 - p2 - c) * Math.Sqrt(tmp) / (Math.Sqrt(p12 * (1.0 - p12)) * tmp);
            }
            return (p1 - p2 - c) / Math.Sqrt(p12 * (1.0 - p12) * tmp);
        }

        /// <summary>
        /// 検定統計量を算出して返す静的メソッド
        /// <note>N/加重平均で使用</note>
        /// </summary>
        /// <param name="N0">第1群と第2群の両方に該当するサンプルのWB値のサマリ</param>
        /// <param name="N1">第1群に該当するサンプルのWB値のサマリ</param>
        /// <param name="N2">第2群に該当するサンプルのWB値のサマリ</param>
        /// <param name="X1">第1群に該当するサンプルの回答データ×WB値のサマリ</param>
        /// <param name="X2">第2群に該当するサンプルの回答データ×WB値のサマリ</param>
        /// <param name="Y1">第1群に該当するサンプルの回答データの平方×WB値のサマリ</param>
        /// <param name="Y2">第2群に該当するサンプルの回答データの平方×WB値のサマリ</param>
        /// <param name="q0">第1群と第2群の両方に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q1">第1群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q2">第2群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="Z">重複部分の相関係数</param>
        /// <param name="U1">第1群の分散 (戻り値)</param>
        /// <param name="U2">第2群の分散 (戻り値)</param>
        /// <param name="Ue">2群の分散の推定値 (戻り値)</param>
        /// <param name="meanX1">第1群に該当するサンプルの回答データ×WB値の平均 (戻り値)</param>
        /// <param name="meanX2">第2群に該当するサンプルの回答データ×WB値の平均 (戻り値)</param>
        /// <param name="e0">第1群と第2群の両方に該当するサンプルのEffective Base (戻り値)</param>
        /// <returns>検定統計量t</returns>
        public static double t(double N0, double N1, double N2, double X1, double X2, double Y1, double Y2, double q0, double q1, double q2, double Z
                             , out double U1, out double U2, out double Ue, out double meanX1, out double meanX2, out double e0)
        {
            string ErrorParameterName = null;
            double ErrorParameterValue = 0.0;
            //if (N0 < 0.0)
            if (Compare(N0, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "N0";
                ErrorParameterValue = N0;
            }
            /*
            else if (N1 < X1)
            {
                ErrorParameterName = "N1";
                ErrorParameterValue = N1;
            }
            else if (N2 < X2)
            {
                ErrorParameterName = "N2";
                ErrorParameterValue = N2;
            }
            else if (X1 < 0.0)
            {
                ErrorParameterName = "X1";
                ErrorParameterValue = X1;
            }
            else if (X2 < 0.0)
            {
                ErrorParameterName = "X2";
                ErrorParameterValue = X2;
            }
            */
            //else if (N1 < N0)
            else if (Compare(N1, CompareOperator.Less, N0))
            {
                ErrorParameterName = "N1";
                ErrorParameterValue = N1;
            }
            //else if (N2 < N0)
            else if (Compare(N2, CompareOperator.Less, N0))
            {
                ErrorParameterName = "N2";
                ErrorParameterValue = N2;
            }
            //else if (Y1 < 0.0)
            else if (Compare(Y1, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "Y1";
                ErrorParameterValue = Y1;
            }
            //else if (Y2 < 0.0)
            else if (Compare(Y2, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "Y2";
                ErrorParameterValue = Y2;
            }
            //else if (q0 < 0.0)
            else if (Compare(q0, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "q0";
                ErrorParameterValue = q0;
            }
            //else if (q1 < 0.0)
            else if (Compare(q1, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "q1";
                ErrorParameterValue = q1;
            }
            //else if (q2 < 0.0)
            else if (Compare(q2, CompareOperator.Less, 0.0))
            {
                ErrorParameterName = "q2";
                ErrorParameterValue = q2;
            }
            if (ErrorParameterName != null)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, ErrorParameterName, ErrorParameterValue.ToString());
            }
            // U1 = 0.0;
            // U2 = 0.0;
            // Ue = 0.0;
            // meanX1 = 0.0;
            // meanX2 = 0.0;
            // e0 = 0.0;
            U1 = double.NaN;
            U2 = double.NaN;
            Ue = double.NaN;
            meanX1 = double.NaN;
            meanX2 = double.NaN;
            e0 = double.NaN;
            //if (N1 <= 1.0 || N2 <= 1.0) return 0.0;
            if (Compare(N1, CompareOperator.LessEqual, 1.0) || Compare(N2, CompareOperator.LessEqual, 1.0)) return 0.0;
            double tmp1 = N1 * Y1 - Math.Pow(X1, 2.0);
            //if (tmp1 < 0.0) return 0.0;
            if (Compare(tmp1, CompareOperator.Less, 0.0)) return 0.0;
            double tmp2 = N2 * Y2 - Math.Pow(X2, 2.0);
            //if (tmp2 < 0.0) return 0.0;
            //if (tmp1 == 0.0 && tmp2 == 0.0) return 0.0;
            if (Compare(tmp2, CompareOperator.Less, 0.0)) return 0.0;
            if (Compare(tmp1, CompareOperator.Equal, 0.0) && Compare(tmp2, CompareOperator.Equal, 0.0)) return 0.0;
            /*
            double tmp3 = q0 * q1 * Math.Pow(N2, 2.0) + q0 * q2 * Math.Pow(N1, 2.0) - 2 * q1 * q2 * Math.Pow(N0, 2.0) * Z;
            if (tmp3 <= 0.0) return 0.0;
            double tmp4 = Math.Pow(N1, 2.0) - q1;
            if (tmp4 < 0.0) return 0.0;
            double tmp5 = Math.Pow(N2, 2.0) - q2;
            if (tmp5 < 0.0) return 0.0;
            double tmp = (N2 * tmp1 + N1 * tmp2) * tmp3;
            return (N2 * X1 - N1 * X2) * Math.Sqrt(q0 * tmp4 * tmp5 * tmp) / tmp;
            */
            double tmp3 = Math.Pow(N1, 2.0) - q1;
            //if (tmp3 <= 0.0) return 0.0;
            if (Compare(tmp3, CompareOperator.LessEqual, 0.0)) return 0.0;
            double tmp4 = Math.Pow(N2, 2.0) - q2;
            //if (tmp4 <= 0.0) return 0.0;
            //if (q1 == 0.0 || q2 == 0.0) return 0.0;
            if (Compare(tmp4, CompareOperator.LessEqual, 0.0)) return 0.0;
            if (Compare(q1, CompareOperator.Equal, 0.0) || Compare(q2, CompareOperator.Equal, 0.0)) return 0.0;
            e0 = 0.0;
            U1 = tmp1 / (N1 * (N1 - 1.0));
            U2 = tmp2 / (N2 * (N2 - 1.0));
            Ue = (N2 * tmp1 + N1 * tmp2) / (N2 * tmp3 + N1 * tmp4);
            meanX1 = X1 / N1;
            meanX2 = X2 / N2;
            double recE1 = q1 / Math.Pow(N1, 2.0);
            double recE2 = q2 / Math.Pow(N2, 2.0);
            double tmp = recE1 + recE2;
            //if (q0 > 0 && N0 > 0.0)
            if (Compare(q0, CompareOperator.Greater, 0.0) && Compare(N0, CompareOperator.Greater, 0.0))
            {
                e0 = Math.Pow(N0, 2.0) / q0;
                tmp -= 2.0 * e0 * recE1 * recE2 * Z;
            }
            //if (tmp <= 0.0) return 0.0;
            if (Compare(tmp, CompareOperator.LessEqual, 0.0)) return 0.0;
            // 誤差の拡張を抑えるように、必要に応じて分母の有理化
            if (Ue > 1.0 ^ tmp > 1.0)
            //if (Compare(Ue, CompareOperator.Greater, 1.0) ^ Compare(tmp, CompareOperator.Greater, 1.0))
            {
                if (Ue > 1.0)
                //if (Compare(Ue, CompareOperator.Greater, 1.0))
                {
                    return (meanX1 - meanX2) * Math.Sqrt(Ue) / (Ue * Math.Sqrt(tmp));
                }
                return (meanX1 - meanX2) * Math.Sqrt(tmp) / (Math.Sqrt(Ue) * tmp);
            }
            tmp *= Ue;
            if (Ue > 1.0)
            //if (Compare(Ue, CompareOperator.Greater, 1.0))
            {
                return (meanX1 - meanX2) * Math.Sqrt(tmp) / tmp;
            }
            return (meanX1 - meanX2) / Math.Sqrt(tmp);
        }

#if AFTER_2ND_PHASE
        /*
        private static double[] Bernoulli = new double[]
            {
                1.0, 1.0 / 6.0, -1.0 / 30.0, 1.0 / 42.0, -1.0 / 30.0, 5.0 / 66.0, -691.0 / 2730.0, 7.0 / 6.0, -3617.0 / 510.0
              , 43867.0 / 798.0, -174611.0 / 330.0, 854513.0 / 138.0, -236364091.0 / 2730.0, 8553103.0 / 6.0, -1869628555.0 / 58.0
            };
        */
        private static decimal[] BernoulliDecimal = new decimal[]
            {
                1.0m, 1.0m / 6.0m, -1.0m / 30.0m, 1.0m / 42.0m, -1.0m / 30.0m, 5.0m / 66.0m, -691.0m / 2730.0m
              , 7.0m / 6.0m, -3617.0m / 510.0m, 43867.0m / 798.0m, -174611.0m / 330.0m, 854513.0m / 138.0m
              , -236364091.0m / 2730.0m, 8553103.0m / 6.0m, -1869628555.0m / 58.0m
            };
#endif

        private static double[] LanczosD = new double[]
            {
                 2.48574089138753565546e-5,
                 1.05142378581721974210,
                -3.45687097222016235469,
                 4.51227709466894823700,
                -2.98285225323576655721,
                 1.05639711577126713077,
                -1.95428773191645869583e-1,
                 1.70970543404441224307e-2,
                -5.71926117404305781283e-4,
                 4.63399473359905636708e-6,
                -2.71994908488607703910e-9
            };
        private const double LanczosR = 10.900511;

#if AFTER_2ND_PHASE
        private static decimal[] LanczosDDecimal = new decimal[] 
            {
                 2.48574089138753565546e-5m,
                 1.05142378581721974210m,
                -3.45687097222016235469m,
                 4.51227709466894823700m,
                -2.98285225323576655721m,
                 1.05639711577126713077m,
                -1.95428773191645869583e-1m,
                 1.70970543404441224307e-2m,
                -5.71926117404305781283e-4m,
                 4.63399473359905636708e-6m,
                -2.71994908488607703910e-9m
            };
        private const decimal LanczosRDecimal = 10.900511m;

        /*
        private static double LogGamma_Stirling(double x)
        {
            double f = (x - 0.5) * Math.Log(x) - x + Math.Log(2.0 * Math.PI) / 2.0;
            double xx = Math.Pow(x, 2.0);
            double xp = x;
            for (int i = 1; i < Bernoulli.Length; ++i)
            {
                double tmp = Bernoulli[i] / (2 * i * (2 * i - 1) * xp);
                double old_f = f;
                f += tmp;
                if (f == old_f) return f;
                xp *= xx;
            }
            return 0.0;
        }

        private static double LanczosLogGamma(double x)
        {
            double s = LanczosD[0];
            for (int i = 1; i < LanczosD.Length; ++i)
            {
                s += LanczosD[i] / (x + (double)i);
            }
            s *= 2.0 / (Math.Sqrt(Math.PI) * x);
            double xx = x + 0.5;
            double t = xx * Math.Log(xx + LanczosR) - x;
            return t + Math.Log(s);
        }
        */
        /*
        private static decimal LogGamma_Stirling(decimal x)
        {
            decimal f = (x - 0.5m) * Log(x) - x + Log(2.0m * PI) / 2.0m;
            decimal xx = x * x;
            decimal xp = x;
            for (int i = 1; i < BernoulliDecimal.Length; ++i)
            {
                decimal tmp = BernoulliDecimal[i] / (2.0m * (decimal)i * (2.0m * (decimal)i - 1.0m) * xp);
                decimal old_f = f;
                f += tmp;
                if (f == old_f) break;
                xp *= xx;
            }
            return f;
        }

        private static decimal LanczosLogGamma(decimal x)
        {
            decimal s = LanczosDDecimal[0];
            for (int i = 1; i < LanczosDDecimal.Length; ++i)
            {
                s += LanczosDDecimal[i] / (x + (decimal)i);
            }
            s *= 2.0m / (Sqrt(PI) * x);
            decimal xx = x + 0.5m;
            decimal t = xx * Log(xx + LanczosRDecimal) - x;
            return t + Log(s);
        }
        */
#endif
        private static readonly double TWO_SQRT_E_PER_PI = 2.0 * Math.Sqrt(Math.E / Math.PI);
        private static readonly double LN_PI = Math.Log(Math.PI);
        private static readonly double LN_TWO_SQRT_E_PER_PI = Math.Log(TWO_SQRT_E_PER_PI);

#if AFTER_2ND_PHASE
        private static readonly decimal TWO_SQRT_E_PER_PI_DECIMAL = 2.0m * Sqrt(e / PI);
        private static readonly decimal LN_PI_DECIMAL = Log(PI);
        private static readonly decimal LN_TWO_SQRT_E_PER_PI_DECIMAL = Log(TWO_SQRT_E_PER_PI_DECIMAL);
#endif
        /// <summary>
        /// 対数γ関数
        /// </summary>
        /// <param name="x"></param>
        /// <returns>γ関数の自然対数</returns>
        public static double LogGamma(double x)
        {
            if (x <= 0.0)
            //if (Compare(x, CompareOperator.LessEqual, 0.0))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "x", x.ToString());
            }
            //if (x > (double)Bernoulli.GetUpperBound(0) * 2.0) return LogGamma_Stirling(x);
            //return LanczosLogGamma(x);
            double s = LanczosD[0];
            if (x < 0.5)
            //if (Compare(x, CompareOperator.Less, 0.5))
            {
                for (int i = 1; i < LanczosD.Length; ++i)
                {
                    s += LanczosD[i] / ((double)i - x);
                }
                return LN_PI - Math.Log(Math.Sin(Math.PI * x)) - Math.Log(s) - LN_TWO_SQRT_E_PER_PI
                     - ((0.5 - x) * Math.Log((0.5 - x + LanczosR) / Math.E));
            }
            for (int i = 1; i < LanczosD.Length; ++i)
            {
                s += LanczosD[i] / (x + (double)i - 1.0);
            }
            return Math.Log(s) + LN_TWO_SQRT_E_PER_PI + ((x - 0.5) * Math.Log((x - 0.5 + LanczosR) / Math.E));
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// 対数γ関数
        /// </summary>
        /// <param name="x"></param>
        /// <returns>γ関数の自然対数</returns>
        public static decimal LogGamma(decimal x)
        {
            if (x <= 0.0m)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "x", x.ToString());
            }
            decimal res = 0.0m;
            if (loggammaDic.TryGetValue(x, out res)) return res;
            //if (x > (decimal)BernoulliDecimal.GetUpperBound(0) * 2.0m) return LogGamma_Stirling(x);
            //return LanczosLogGamma(x);
            decimal s = LanczosDDecimal[0];
            if (x < 0.5m)
            {
                for (int i = 1; i < LanczosDDecimal.Length; ++i)
                {
                    s += LanczosDDecimal[i] / ((decimal)i - x);
                }
                res = LN_PI_DECIMAL - Log(Sin(PI * x))
                     - Log(s) - LN_TWO_SQRT_E_PER_PI_DECIMAL
                     - ((0.5m - x) * Log((0.5m - x + LanczosRDecimal) / e));
            }
            else
            {
                for (int i = 1; i < LanczosDDecimal.Length; ++i)
                {
                    s += LanczosDDecimal[i] / (x + (decimal)i - 1.0m);
                }
                res = Log(s) + LN_TWO_SQRT_E_PER_PI_DECIMAL
                     + ((x - 0.5m) * Log((x - 0.5m + LanczosRDecimal) / e));
            }
            AddDictionaryItem<decimal>(loggammaDic, x, res);
            return res;
        }

        /// <summary>
        /// γ関数
        /// </summary>
        /// <param name="x"></param>
        /// <returns>γ関数の値</returns>
        public static double Gamma(double x)
        {
            if (x <= 0.0)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "x", x.ToString());
            }
            double s = LanczosD[0];
            if (x < 0.5)
            {
                for (int i = 1; i < LanczosD.Length; ++i)
                {
                    s += LanczosD[i] / ((double)i - x);
                }
                return Math.PI / (Math.Sin(Math.PI * x) * s * TWO_SQRT_E_PER_PI * Math.Pow((0.5 - x + LanczosR) / Math.E, 0.5 - x));
            }
            for (int i = 1; i < LanczosD.Length; ++i)
            {
                s += LanczosD[i] / (x + (double)i - 1.0);
            }
            return s * TWO_SQRT_E_PER_PI * Math.Pow((x - 0.5 + LanczosR) / Math.E, x - 0.5);
        }
#endif

        // private static System.Web.UI.DataVisualization.Charting.Chart chart = null;

        /// <summary>
        /// β関数
        /// </summary>
        /// <param name="a">最初の値</param>
        /// <param name="b">2番目の値</param>
        /// <returns>β関数の値</returns>
        public static double Beta(double a, double b)
        {
            if (a < 0.0)
            //if (Compare(a, CompareOperator.Less, 0.0))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "a", a.ToString());
            }
            if (b < 0.0)
            //if (Compare(b, CompareOperator.Less, 0.0))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "b", b.ToString());
            }
            //if (chart == null) chart = new System.Web.UI.DataVisualization.Charting.Chart();
            //System.Web.UI.DataVisualization.Charting.StatisticFormula statisticFormula = chart.DataManipulator.Statistics;
            //return statisticFormula.BetaFunction(a, b);
            //double gab = Gamma(a + b);
            //if (gab == 0.0) return 0.0;
            //return Gamma(a) * Gamma(b) / gab;
            return Math.Exp(LogGamma(a) + LogGamma(b) - LogGamma(a + b));
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// β関数
        /// </summary>
        /// <param name="a">最初の値</param>
        /// <param name="b">2番目の値</param>
        /// <returns>β関数の値</returns>
        public static decimal Beta(decimal a, decimal b)
        {
            if (a < 0.0m)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "a", a.ToString());
            }
            if (b < 0.0m)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "b", b.ToString());
            }
            string key = a.ToString() + "_" + b.ToString();
            decimal res = 0.0m;
            if (betaDic.TryGetValue(key, out res)) return res;
            res = Exp(LogGamma(a) + LogGamma(b) - LogGamma(a + b));
            AddDictionaryItem<string>(betaDic, key, res);
            return res;
        }

        /*
        private const double BINARY_BASE_NUMBER = 2.0;
        private const double DOUBLE_PRECISION = 53.0;
        private static readonly double DoubleMachinePrecision = Math.Pow(BINARY_BASE_NUMBER, DOUBLE_PRECISION);
        private static double BetaRegularized(double a, double b, double x)
        {
            if (a < 0.0)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "a", a.ToString());
            }
            if (b < 0.0)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "b", b.ToString());
            }
            if (x < 0.0 || x > 1.0)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "x", x.ToString());
            }
            double bt = 0.0;
            if (x != 0.0 && x != 1.0)
            {
                bt = Math.Exp(LogGamma(a + b) - LogGamma(a) - LogGamma(b) + a * Math.Log(x) + (b * Math.Log(1.0 - x)));
            }
            bool symmetryTransformation = x >= (a + 1.0) / (a + b + 2.0);
            double eps = DoubleMachinePrecision;
            double fpmin = 1.0 / eps;
            if (symmetryTransformation)
            {
                x = 1.0 - x;
                double tmp = a;
                a = b;
                b = tmp;
            }
            double gab = a + b;
            double gap = a + 1.0;
            double gam = a - 1.0;
            double c = 1.0;
            double d = 1.0 - gab * x / gap;
            if (Math.Abs(d) < fpmin) d = fpmin;
            d = 1.0 / d;
            double h = d;
            for (int i = 1, j = 2; i <= 250; ++i, j += 2)
            {
                double m = (double)i;
                double m2 = (double)j;
                double aa = m * (b - m) * x / ((gam + m2) * (a + m2));
                d = 1.0 + aa * d;
                if (Math.Abs(d) < fpmin) d = fpmin;
                c = 1.0 + aa / c;
                if (Math.Abs(c) < fpmin) c = fpmin;
                d = 1.0 / d;
                h *= d * c;
                aa = -(a + m) * (gab + m) * x / ((a + m2) * (gap + m2));
                d = 1.0 + aa * d;
                if (Math.Abs(d) < fpmin) d = fpmin;
                c = 1.0 + aa / c;
                if (Math.Abs(c) < fpmin) c = fpmin;
                d = 1.0 / d;
                double del = d * c;
                h *= del;
                if (Math.Abs(del - 1.0) <= eps)
                {
                    double res = bt * h / a;
                    return symmetryTransformation ? 1.0 - res : res;
                }
            }
            return 0.0;            
        }
        */
#endif

        /// <summary>
        /// 不完全β関数
        /// </summary>
        /// <param name="a">最初の値</param>
        /// <param name="b">2番目の値</param>
        /// <param name="x">積分最終値 (0.0～1.0)</param>
        /// <returns>不完全β関数の値</returns>
        public static double BetaIncomplete(double a, double b, double x)
        {
            if (a < 0.0)
            //if (Compare(a, CompareOperator.Less, 0.0))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "a", a.ToString());
            }
            if (b < 0.0)
            //if (Compare(b, CompareOperator.Less, 0.0))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "b", b.ToString());
            }
            if (x < 0.0 || x > 1.0)
            //if (Compare(x, CompareOperator.Less, 0.0) || Compare(x, CompareOperator.Greater, 1.0))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "x", x.ToString());
            }
            if (x == 0.0) return 0.0;
            //if (Compare(x, CompareOperator.Equal, 0.0)) return 0.0;
            double xtp = (a + 1.0) / (a + b + 2.0);
            if (x > xtp) return Beta(a, b) - BetaIncomplete(b, a, 1.0 - x);
            //if (Compare(x, CompareOperator.Greater, xtp)) return Beta(a, b) - BetaIncomplete(b, a, 1.0 - x);
            double aa = 1.0;
            double bb = 1.0;
            double D = 1.0;
            double Df = aa / bb;
            double f = Df;
            for (int i = 1; i < 250; ++i)
            {
                double k = (double)i;
                double m = (double)(i / 2);
                if (i % 2 == 0)
                {
                    aa = x * m * (b - m) / ((a + k - 1.0) * (a + k));
                }
                else
                {
                    aa = -x * (a + m) * (a + b + m) / ((a + k - 1.0) * (a + k));
                }
                D = 1.0 / (bb + aa * D);
                Df *= bb * D - 1.0;
                double old_f = f;
                f += Df;
                if (f == old_f) break;
                //if (Compare(f, CompareOperator.Equal, old_f)) break;
            }
            return Math.Pow(x, a) * Math.Pow(1.0 - x, b) / a * f;
            //return BetaRegularized(a, b, x) * Beta(a, b);
        }

#if AFTER_2ND_PHASE
        /*
        private const decimal MAX_GAMMA = 1755.5483429m;
        private static readonly decimal MAX_LOG = Log(decimal.MaxValue - 1.0m);
        private const decimal BETA_BIG = 9.223372036854775808e18m;  // 2 ^ 63
        private const decimal BETA_BIGINV = 1.0m / BETA_BIG;

        private static decimal betaDistPowerSeries(decimal a, decimal b, decimal x)
        {
            decimal ai = 1.0m / a;
            decimal u = (1.0m - b) * x;
            decimal v = u / (a + 1.0m);
            decimal t1 = v;
            decimal t = u;
            decimal n = 2.0m;
            decimal s = 0.0m;
            decimal z = 1.0e-10m * ai;
            while (Math.Abs(v) > z)
            {
                u = (n - b) * x / n;
                t *= u;
                v = t / (a + n);
                s += v;
                ++n;
            }
            s += t1;
            s += ai;
            u = a * Log(x);
            if (a + b < MAX_GAMMA && Math.Abs(u) < MAX_LOG)
            {
                t = 1.0m / Beta(a, b);
                s *= t * Pow(x, a);
            }
            else
            {
                t = LogGamma(a + b) - LogGamma(a) - LogGamma(b) + u + Log(s);
                s = Exp(t);
            }
            return s;
        }

        private const decimal THRESH = 3.0m * 1.0e-10m;

        private static decimal betaDistExpansion1(decimal a, decimal b, decimal x)
        {
            decimal k1 = a;
            decimal k2 = a + b;
            decimal k3 = a;
            decimal k4 = a + 1.0m;
            decimal k5 = 1.0m;
            decimal k6 = b - 1.0m;
            decimal k7 = k4;
            decimal k8 = a + 2.0m;
            decimal pkm1 = 1.0m;
            decimal pkm2 = 0.0m;
            decimal qkm1 = 1.0m;
            decimal qkm2 = 1.0m;
            decimal res = 1.0m;
            decimal r = 1.0m;
            decimal n = 0;
            do
            {
                decimal xk = -x * k1 * k2 / (k3 * k4);
                decimal pk = pkm1 + pkm2 * xk;
                decimal qk = qkm1 + qkm2 * xk;
                pkm2 = pkm1;
                pkm1 = pk;
                qkm2 = qkm1;
                qkm1 = qk;
                xk = x * k5 * k6 / (k7 * k8);
                pk = pkm1 + pkm2 * xk;
                qk = qkm1 + qkm2 * xk;
                pkm2 = pkm1;
                pkm1 = pk;
                qkm2 = qkm1;
                qkm1 = qk;
                if (qk != 0.0m) r = pk / qk;
                decimal t = 1.0m;
                if (r != 0.0m)
                {
                    t = Math.Abs((res - r) / r);
                    res = r;
                }
                if (t < THRESH) return res;
                ++k1;
                ++k2;
                k3 += 2.0m;
                k4 += 2.0m;
                ++k5;
                --k6;
                k7 += 2.0m;
                k8 += 2.0m;
                if (Math.Abs(qk) + Math.Abs(pk) > BETA_BIG)
                {
                    pkm1 *= BETA_BIGINV;
                    pkm2 *= BETA_BIGINV;
                    qkm1 *= BETA_BIGINV;
                    qkm2 *= BETA_BIGINV;                    
                }
                if (Math.Abs(qk) < BETA_BIGINV || Math.Abs(pk) < BETA_BIGINV)
                {
                    pkm1 *= BETA_BIG;
                    pkm2 *= BETA_BIG;
                    qkm1 *= BETA_BIG;
                    qkm2 *= BETA_BIG;
                }

            } while (++n < 400);
            return res;
        }

        private static decimal betaDistExpansion2(decimal a, decimal b, decimal x)
        {
            decimal k1 = a;
            decimal k2 = b - 1.0m;
            decimal k3 = a;
            decimal k4 = a + 1.0m;
            decimal k5 = 1.0m;
            decimal k6 = a + b;
            decimal k7 = a + 1.0m;
            decimal k8 = a + 2.0m;
            decimal pkm1 = 1.0m;
            decimal pkm2 = 0.0m;
            decimal qkm1 = 1.0m;
            decimal qkm2 = 1.0m;
            decimal z = x / (1.0m - x);
            decimal res = 1.0m;
            decimal r = 1.0m;
            decimal n = 0;
            do
            {
                decimal xk = -z * k1 * k2 / (k3 * k4);
                decimal pk = pkm1 + pkm2 * xk;
                decimal qk = qkm1 + qkm2 * xk;
                pkm2 = pkm1;
                pkm1 = pk;
                qkm2 = qkm1;
                qkm1 = qk;
                xk = z * k5 * k6 / (k7 * k8);
                pk = pkm1 + pkm2 * xk;
                qk = qkm1 + qkm2 * xk;
                pkm2 = pkm1;
                pkm1 = pk;
                qkm2 = qkm1;
                qkm1 = qk;
                if (qk != 0.0m) r = pk / qk;
                decimal t = 1.0m;
                if (r != 0.0m)
                {
                    t = Math.Abs((res - r) / r);
                    res = r;
                }
                if (t < THRESH) return res;
                ++k1;
                --k2;
                k3 += 2.0m;
                k4 += 2.0m;
                ++k5;
                ++k6;
                k7 += 2.0m;
                k8 += 2.0m;
                if (Math.Abs(qk) + Math.Abs(pk) > BETA_BIG)
                {
                    pkm1 *= BETA_BIGINV;
                    pkm2 *= BETA_BIGINV;
                    qkm1 *= BETA_BIGINV;
                    qkm2 *= BETA_BIGINV;
                }
                if (Math.Abs(qk) < BETA_BIGINV || Math.Abs(pk) < BETA_BIGINV)
                {
                    pkm1 *= BETA_BIG;
                    pkm2 *= BETA_BIG;
                    qkm1 *= BETA_BIG;
                    qkm2 *= BETA_BIG;
                }

            } while (++n < 400);
            return res;
        }
        */

        /// <summary>
        /// 不完全β関数
        /// </summary>
        /// <param name="a">最初の値</param>
        /// <param name="b">2番目の値</param>
        /// <param name="x">積分最終値 (0.0～1.0)</param>
        /// <returns>不完全β関数の値</returns>
        public static decimal BetaIncomplete(decimal a, decimal b, decimal x)
        {
            if (a < 0.0m)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "a", a.ToString());
            }
            if (b < 0.0m)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "b", b.ToString());
            }
            if (x < 0.0m || x > 1.0m)
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL, "x", x.ToString());
            }
            if (x == 0.0m) return 0.0m;
            decimal xtp = (a + 1.0m) / (a + b + 2.0m);
            if (x > xtp) return Beta(a, b) - BetaIncomplete(b, a, 1.0m - x);
            string key = a.ToString() + "_" + b.ToString() + "_" + x.ToString();
            decimal res = 0.0m;
            if (betaIncompleteDic.TryGetValue(key, out res)) return res;
            decimal aa = 1.0m;
            decimal bb = 1.0m;
            decimal D = 1.0m / bb;
            decimal Df = aa / bb;
            decimal f = Df;
            for (int i = 1; i < 250; ++i)
            {
                decimal k = (decimal)i;
                decimal m = (decimal)(i / 2);
                if (i % 2 == 0)
                {
                    aa = x * m * (b - m) / ((a + k - 1.0m) * (a + k));
                }
                else
                {
                    aa = -x * (a + m) * (a + b + m) / ((a + k - 1.0m) * (a + k));
                }
                D = 1.0m / (bb + aa * D);
                Df *= bb * D - 1.0m;
                decimal old_f = f;
                f += Df;
                if (f == old_f) break;
            }
            res = Pow(x, a) * Pow(1.0m - x, b) / a * f;
            AddDictionaryItem<string>(betaIncompleteDic, key, res);
            return res;
            /*
            if (b * x <= 1.0m && x <= 0.95m) return betaDistPowerSeries(a, b, x);
            bool f = x > a / (a + b);
            decimal xc = 1.0m - x;
            if (f)
            {
                decimal tmp = a;
                a = b;
                b = tmp;
                tmp = x;
                x = xc;
                xc = tmp;
                if (b * x <= 1.0m && x <= 0.95m)
                {
                    return 1.0m - betaDistPowerSeries(a, b, x);
                }
            }
            decimal y = x * (a + b - 2.0m) - (a - 1.0m);
            decimal w = 0.0m;
            if (y < 0.0m)
            {
                w = betaDistExpansion1(a, b, x);
            }
            else
            {
                w = betaDistExpansion2(a, b, x) / xc;
            }
            y = a * Log(x);
            decimal t = b * Log(xc);
            if (a + b < MAX_GAMMA && Math.Abs(y) < MAX_LOG && Math.Abs(t) < MAX_LOG)
            {
                t = Pow(xc, b) * Pow(x, a) * w / (Beta(a, b) * a);
            }
            else
            {
                y += t + LogGamma(a + b) - LogGamma(a) - LogGamma(b) + Log(w / a);
                t = Exp(y);
            }
            return f ? 1.0m - t : t;
            */
        }
#endif

        /*
        /// <summary>
        /// スチューデントT分布の確率を計算する(自由度整数)
        /// </summary>
        /// <param name="value">t値</param>
        /// <param name="degreeOfFreedom">自由度</param>
        /// <returns>p値</returns>
        public static double TDistribution(double value, int degreeOfFreedom)
        {
            value = Math.Abs(value);
            if (degreeOfFreedom < 1) return 0.0;
            if (chart == null) chart = new System.Web.UI.DataVisualization.Charting.Chart();
            System.Web.UI.DataVisualization.Charting.StatisticFormula statisticFormula = chart.DataManipulator.Statistics;
            return statisticFormula.TDistribution(value, degreeOfFreedom, false);
        }
        */

        /// <summary>
        /// スチューデントT分布の確率を計算する
        /// </summary>
        /// <param name="value">t値</param>
        /// <param name="degreeOfFreedom">自由度</param>
        /// <returns>p値</returns>
        public static double TDistribution(double value, double degreeOfFreedom)
        {
            degreeOfFreedom = Math.Floor(degreeOfFreedom);  // SPSSに合わせて小数部分を落とす
            //if (degreeOfFreedom < 1.0) return 0.0;
            // TODO: #256298 返り値(p)を0.0⇒NaNに変更　（※GT検定結果のエクセルだけならここだけで直る）
            if (Compare(degreeOfFreedom, CompareOperator.Less, 1.0)) return double.NaN;
            double x = degreeOfFreedom / (degreeOfFreedom + Math.Pow(value, 2.0));
            double a = degreeOfFreedom / 2.0;
            double b = 0.5;
            return BetaIncomplete(a, b, x) / Beta(a, b);
        }

#if AFTER_2ND_PHASE
        /// <summary>
        /// スチューデントT分布の確率を計算する
        /// </summary>
        /// <param name="value">t値</param>
        /// <param name="degreeOfFreedom">自由度</param>
        /// <returns>p値</returns>
        public static decimal TDistribution(decimal value, decimal degreeOfFreedom)
        {
            if (degreeOfFreedom < 1.0m) return 0.0m;
            string key = value.ToString() + "_" + degreeOfFreedom.ToString();
            decimal res = 0.0m;
            if (tdistDic.TryGetValue(key, out res)) return res;
            decimal x = degreeOfFreedom / (degreeOfFreedom + Pow(value, 2.0m));
            decimal a = degreeOfFreedom / 2.0m;
            decimal b = 0.5m;
            res = BetaIncomplete(a, b, x) / Beta(a, b);
            AddDictionaryItem<string>(tdistDic, key, res);
            return res;
        }
#endif

        /// <summary>
        /// 有効桁数を15桁に丸める
        /// </summary>
        /// <param name="N0">第1群と第2群の両方に該当するサンプルのWB値のサマリ</param>
        /// <param name="N1">第1群に該当するサンプルのWB値のサマリ</param>
        /// <param name="N2">第2群に該当するサンプルのWB値のサマリ</param>
        /// <param name="X1">第1群に該当するサンプルの回答データ×WB値のサマリ</param>
        /// <param name="X2">第2群に該当するサンプルの回答データ×WB値のサマリ</param>
        /// <param name="Y1">第1群に該当するサンプルの回答データの平方×WB値のサマリ</param>
        /// <param name="Y2">第2群に該当するサンプルの回答データの平方×WB値のサマリ</param>
        /// <param name="q0">第1群と第2群の両方に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q1">第1群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q2">第2群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="X0inOverlap">第1群と第2群の両方に該当するサンプルで、第1群の回答データ×第2群の回答データ×WB値のサマリ</param>
        /// <param name="X1inOverlap">第1群と第2群の両方に該当するサンプルで、第1群の回答データ×WBのサマリ</param>
        /// <param name="X2inOverlap">第1群と第2群の両方に該当するサンプルで、第2群の回答データ×WB値のサマリ</param>
        /// <param name="Y1inOverlap">第1群と第2群の両方に該当するサンプルで、第1群の回答データの平方×WB値のサマリ</param>
        /// <param name="Y2inOverlap">第1群と第2群の両方に該当するサンプルで、第2群の回答データの平方×WB値のサマリ</param>
        public static void RoundOffTDistribution(
                    ref double N0, ref double N1, ref double N2, ref double X1, ref double X2, ref double Y1, ref double Y2, ref double q0, ref double q1, ref double q2
                  , ref double X0inOverlap, ref double X1inOverlap, ref double X2inOverlap, ref double Y1inOverlap, ref double Y2inOverlap)
        {
#if ROUNDOFF_TDISTRIBUTION 
            N0 = double.Parse(N0.ToString());
            N1 = double.Parse(N1.ToString());
            N2 = double.Parse(N2.ToString());
            X1 = double.Parse(X1.ToString());
            X2 = double.Parse(X2.ToString());
            Y1 = double.Parse(Y1.ToString());
            Y2 = double.Parse(Y2.ToString());
            q0 = double.Parse(q0.ToString());
            q1 = double.Parse(q1.ToString());
            q2 = double.Parse(q2.ToString());
            X0inOverlap = double.Parse(X0inOverlap.ToString());
            X1inOverlap = double.Parse(X1inOverlap.ToString());
            X2inOverlap = double.Parse(X2inOverlap.ToString());
            Y1inOverlap = double.Parse(Y1inOverlap.ToString());
            Y2inOverlap = double.Parse(Y2inOverlap.ToString());
#endif
        }

        /// <summary>
        /// 有効桁数を15桁に丸める
        /// </summary>
        /// <param name="N0">第1群と第2群の両方に該当するサンプルのWB値のサマリ</param>
        /// <param name="N1">第1群に該当するサンプルのWB値のサマリ</param>
        /// <param name="N2">第2群に該当するサンプルのWB値のサマリ</param>
        /// <param name="X1">第1群に反応したサンプルのWB値のサマリ</param>
        /// <param name="X2">第2群に反応したサンプルのWB値のサマリ</param>
        /// <param name="q0">第1群と第2群の両方に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q1">第1群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q2">第2群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="X0inOverlap">第1群と第2群の両方に該当するサンプルで、両方に反応したサンプルのWB値のサマリ</param>
        /// <param name="X1inOverlap">第1群と第2群の両方に該当するサンプルで、第1群に反応したサンプルのWB値のサマリ</param>
        /// <param name="X2inOverlap">第1群と第2群の両方に該当するサンプルで、第2群に反応したサンプルのWB値のサマリ</param>
        public static void RoundOffTDistribution(
                    ref double N0, ref double N1, ref double N2, ref double X1, ref double X2, ref double q0, ref double q1, ref double q2
                  , ref double X0inOverlap, ref double X1inOverlap, ref double X2inOverlap)
        {
            double dummy = 0.0;
            RoundOffTDistribution(ref N0, ref N1, ref N2, ref X1, ref X2, ref dummy, ref dummy
                , ref q0, ref q1, ref q2, ref X0inOverlap, ref X1inOverlap, ref X2inOverlap, ref dummy, ref dummy);
        }

        /// <summary>
        /// スチューデントT分布の確率を計算する
        /// <note>SA/MAで使用</note>
        /// </summary>
        /// <param name="N0">第1群と第2群の両方に該当するサンプルのWB値のサマリ</param>
        /// <param name="N1">第1群に該当するサンプルのWB値のサマリ</param>
        /// <param name="N2">第2群に該当するサンプルのWB値のサマリ</param>
        /// <param name="X1">第1群に反応したサンプルのWB値のサマリ</param>
        /// <param name="X2">第2群に反応したサンプルのWB値のサマリ</param>
        /// <param name="q0">第1群と第2群の両方に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q1">第1群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q2">第2群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="X0inOverlap">第1群と第2群の両方に該当するサンプルで、両方に反応したサンプルのWB値のサマリ</param>
        /// <param name="X1inOverlap">第1群と第2群の両方に該当するサンプルで、第1群に反応したサンプルのWB値のサマリ</param>
        /// <param name="X2inOverlap">第1群と第2群の両方に該当するサンプルで、第2群に反応したサンプルのWB値のサマリ</param>
        /// <param name="p1">第1群に反応したサンプルの比率 (戻り値)</param>
        /// <param name="p2">第2群に反応したサンプルの比率 (戻り値)</param>
        /// <param name="p12">第1群に反応したサンプルと第2群に反応したサンプルの平均比率 (戻り値)</param>
        /// <param name="c">連続性の補正量 (戻り値)</param>
        /// <param name="e0">第1群と第2群の両方に該当するサンプルのEffective Base (戻り値)</param>
        /// <param name="e1">第1群に該当するサンプルのEffective Base (戻り値)</param>
        /// <param name="e2">第2群に該当するサンプルのEffective Base (戻り値)</param>
        /// <param name="Z">重複部分の相関係数 (戻り値)</param>
        /// <param name="t">t値 (戻り値)</param>
        /// <param name="d">自由度 (戻り値)</param>
        /// <returns>p値</returns>
        public static double TDistribution(
                    double N0, double N1, double N2, double X1, double X2, double q0, double q1, double q2
                  , double X0inOverlap, double X1inOverlap, double X2inOverlap
                  , out double p1, out double p2, out double p12, out double c, out double e0, out double e1, out double e2
                  , out double Z, out double t, out double d)
        {
            Z = Function.Z(N0, X0inOverlap, X1inOverlap, X2inOverlap, X1inOverlap, X2inOverlap);
            t = Function.t(N0, N1, N2, X1, X2, q0, q1, q2, Z, out p1, out p2, out p12, out c, out e0);
            // e1 = 0.0;
            // e2 = 0.0;
            // d = 0.0;
            e1 = double.NaN;
            e2 = double.NaN;
            d = double.NaN;
            //if (q1 == 0.0 || q2 == 0.0) return 0.0;
            if (Compare(q1, CompareOperator.Equal, 0.0) || Compare(q2, CompareOperator.Equal, 0.0)) return 0.0;
            e1 = Math.Pow(N1, 2.0) / q1;
            e2 = Math.Pow(N2, 2.0) / q2;
            d = e1 + e2 - e0 - 2.0;
#if ROUNDOFF_TDISTRIBUTION
            d = double.Parse(d.ToString());
#endif
            d = d + CreateEpsilon(d);
            return TDistribution(t, d);
        }

        /// <summary>
        /// スチューデントT分布の確率を計算する
        /// <note>N/加重平均で使用</note>
        /// </summary>
        /// <param name="N0">第1群と第2群の両方に該当するサンプルのWB値のサマリ</param>
        /// <param name="N1">第1群に該当するサンプルのWB値のサマリ</param>
        /// <param name="N2">第2群に該当するサンプルのWB値のサマリ</param>
        /// <param name="X1">第1群に該当するサンプルの回答データ×WB値のサマリ</param>
        /// <param name="X2">第2群に該当するサンプルの回答データ×WB値のサマリ</param>
        /// <param name="Y1">第1群に該当するサンプルの回答データの平方×WB値のサマリ</param>
        /// <param name="Y2">第2群に該当するサンプルの回答データの平方×WB値のサマリ</param>
        /// <param name="q0">第1群と第2群の両方に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q1">第1群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="q2">第2群に該当するサンプルのWB値の平方のサマリ</param>
        /// <param name="X0inOverlap">第1群と第2群の両方に該当するサンプルで、第1群の回答データ×第2群の回答データ×WB値のサマリ</param>
        /// <param name="X1inOverlap">第1群と第2群の両方に該当するサンプルで、第1群の回答データ×WBのサマリ</param>
        /// <param name="X2inOverlap">第1群と第2群の両方に該当するサンプルで、第2群の回答データ×WB値のサマリ</param>
        /// <param name="Y1inOverlap">第1群と第2群の両方に該当するサンプルで、第1群の回答データの平方×WB値のサマリ</param>
        /// <param name="Y2inOverlap">第1群と第2群の両方に該当するサンプルで、第2群の回答データの平方×WB値のサマリ</param>
        /// <param name="U1">第1群の分散 (戻り値)</param>
        /// <param name="U2">第2群の分散 (戻り値)</param>
        /// <param name="Ue">2群の分散の推定値 (戻り値)</param>
        /// <param name="meanX1">第1群に該当するサンプルの回答データ×WB値の平均 (戻り値)</param>
        /// <param name="meanX2">第2群に該当するサンプルの回答データ×WB値の平均 (戻り値)</param>
        /// <param name="e0">第1群と第2群の両方に該当するサンプルのEffective Base (戻り値)</param>
        /// <param name="e1">第1群に該当するサンプルのEffective Base (戻り値)</param>
        /// <param name="e2">第2群に該当するサンプルのEffective Base (戻り値)</param>
        /// <param name="Z">重複部分の相関係数 (戻り値)</param>
        /// <param name="t">t値 (戻り値)</param>
        /// <param name="d">自由度 (戻り値)</param>
        /// <returns>p値</returns>
        public static double TDistribution(
                    double N0, double N1, double N2, double X1, double X2, double Y1, double Y2, double q0, double q1, double q2
                  , double X0inOverlap, double X1inOverlap, double X2inOverlap, double Y1inOverlap, double Y2inOverlap
                  , out double U1, out double U2, out double Ue, out double meanX1, out double meanX2, out double e0, out double e1, out double e2
                  , out double Z, out double t, out double d)
        {
            Z = Function.Z(N0, X0inOverlap, X1inOverlap, X2inOverlap, Y1inOverlap, Y2inOverlap);
            t = Function.t(N0, N1, N2, X1, X2, Y1, Y2, q0, q1, q2, Z, out U1, out U2, out Ue, out meanX1, out meanX2, out e0);
            // e1 = 0.0;
            // e2 = 0.0;
            // d = 0.0;
            e1 = double.NaN;
            e2 = double.NaN;
            d = double.NaN;
            //if (q1 == 0.0 || q2 == 0.0) return 0.0;
            if (Compare(q1, CompareOperator.Equal, 0.0) || Compare(q2, CompareOperator.Equal, 0.0)) return 0.0;
            e1 = Math.Pow(N1, 2.0) / q1;
            e2 = Math.Pow(N2, 2.0) / q2;
            d = e1 + e2 - e0 - 2.0;
#if ROUNDOFF_TDISTRIBUTION
            d = double.Parse(d.ToString());
#endif
            d = d + CreateEpsilon(d);
            return TDistribution(t, d);
        }
    }
    #endregion

    #region Zipクラス
    /// <summary>
    /// Zip解凍/圧縮処理を行うメソッドをまとめた静的クラス
    /// </summary>
    [ComVisible(false), Guid("E3A981B7-9E84-4768-A40E-9BDCC09B86B1")]
    public static class Zip
    {
        /// <summary>
        /// Zip圧縮処理を行う
        /// </summary>
        /// <param name="ZipFilePath">作成するZipファイルのパス</param>
        /// <param name="FilePaths">Zipファイルに含めるファイルのパス (可変長)</param>
        public static void Create(string ZipFilePath, params string[] FilePaths)
        {
            if (string.IsNullOrWhiteSpace(ZipFilePath)) return;
            if (FilePaths == null) return;
            using (Ionic.Zip.ZipFile zipfile = new Ionic.Zip.ZipFile(ZipFilePath, Encoding.GetEncoding("shift_jis")))
            {
                zipfile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                foreach (string filepath in FilePaths)
                {
                    if (!string.IsNullOrWhiteSpace(filepath) && System.IO.File.Exists(filepath))
                    {
                        zipfile.AddFile(filepath);
                    }
                }
                if (zipfile.Count > 0) zipfile.Save();
            }
        }

        /// <summary>
        /// Zip圧縮処理を行う再帰処理用ルーチン
        /// </summary>
        /// <memo>
        /// 自前で再起を使わずとも、System.IO.Directory.GetFiles(DirectoryPath,"*.*", System.IO.SearchOption.AllDirectories)
        /// に対してなめればよいかも知れない
        /// その場合、上記メソッドの戻り値の配列内の序列において、ディレクトリツリーの上から順となることが担保されるかどうかが
        /// はっきりせず、また、Zipアーカイブ内ディレクトリツリーが上から順に作成されなくても問題ないのかがはっきりしない
        /// そのため、確実な処理の担保のために、自前で再起させる処理とした
        /// </memo>
        /// <param name="zipfile">ZipFileクラスのインスタンスへの参照</param>
        /// <param name="DirectoryPath">圧縮するファイルがあるディレクトリのパス</param>
        /// <param name="ArchiveDirectory">Zipアーカイブ内ディレクトリ</param>
        private static void Create(Ionic.Zip.ZipFile zipfile, string DirectoryPath, string ArchiveDirectory)
        {
            foreach (string filepath in System.IO.Directory.GetFiles(DirectoryPath, "*.*", System.IO.SearchOption.TopDirectoryOnly))
            {
                zipfile.AddFile(filepath, ArchiveDirectory);
            }
            foreach (string dirpath in System.IO.Directory.GetDirectories(DirectoryPath, "*", System.IO.SearchOption.TopDirectoryOnly))
            {
                string dirname = System.IO.Path.GetFileName(dirpath);
                string archivedirectory = System.IO.Path.Combine(ArchiveDirectory, dirname);
                string directorypath = System.IO.Path.Combine(DirectoryPath, dirname);
                Create(zipfile, directorypath, archivedirectory);
            }
        }

        /// <summary>
        /// 元のディレクトリ構造を保持したまま、Zip圧縮処理を行う
        /// </summary>
        /// <param name="ZipFilePath">作成するZipファイルのパス</param>
        /// <param name="DirectoryPath">圧縮するファイル/ディレクトリを配置したディレクトリのパス</param>
        public static void Create(string ZipFilePath, string DirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(ZipFilePath)) return;
            if (string.IsNullOrWhiteSpace(DirectoryPath)) return;
            if (System.IO.Directory.Exists(DirectoryPath))
            {
                using (Ionic.Zip.ZipFile zipfile = new Ionic.Zip.ZipFile(ZipFilePath, Encoding.GetEncoding("shift_jis")))
                {
                    zipfile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    Create(zipfile, DirectoryPath, "");
                    if (zipfile.Count > 0) zipfile.Save();
                }
            }
            else if (System.IO.File.Exists(DirectoryPath))
            {
                Create(ZipFilePath, FilePaths: DirectoryPath);
            }
        }

        /// <summary>
        /// Zip解凍処理を行う
        /// </summary>
        /// <param name="ZipFilePath">Zipファイルのパス</param>
        /// <param name="ExtractToDirectory">解凍先ディレクトリのパス</param>
        /// <param name="overwrite">既存のファイルを上書きすることを示すフラグ (省略可、既定値true)</param>
        /// <param name="throwerror">失敗時にエラーをスローすることを示すフラグ (省略可、既定値false)</param>
        /// <param name="flatfolder">フォルダ構造を無視して解凍することを示すフラグ (省略可、既定値false)</param>
        /// <param name="SelectionCriteria">特定ファイルのみ解凍する場合のファイル名 (Ex:"ABC.txt","*.txt") (省略可、既定値null)</param>
        public static void Extract(string ZipFilePath,
                                   string ExtractToDirectory,
                                   bool overwrite = true,
                                   bool throwerror = false,
                                   bool flatfolder = false,
                                   string SelectionCriteria = null)
        {
            if (string.IsNullOrWhiteSpace(ZipFilePath) || !System.IO.File.Exists(ZipFilePath)) return;
            Ionic.Zip.ReadOptions option = new Ionic.Zip.ReadOptions();
            option.Encoding = Encoding.GetEncoding("shift_jis");
            Ionic.Zip.ExtractExistingFileAction ExistingAction =
                overwrite ? Ionic.Zip.ExtractExistingFileAction.OverwriteSilently :
                            Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite;

            if (throwerror)
            {
                Ionic.Zip.ZipFile zipfile = Ionic.Zip.ZipFile.Read(ZipFilePath, option);
                zipfile.FlattenFoldersOnExtract = flatfolder;
                try
                {
                    if (SelectionCriteria == null)
                    {
                        zipfile.ExtractAll(ExtractToDirectory, ExistingAction);
                    }
                    else
                    {
                        // 指定されたファイルだけを解凍する
                        zipfile.ExtractSelectedEntries(SelectionCriteria, null, ExtractToDirectory, ExistingAction);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    throw;
                }
                finally
                {
                    zipfile.Dispose();
                }
            }
            else
            {
                using (Ionic.Zip.ZipFile zipfile = Ionic.Zip.ZipFile.Read(ZipFilePath, option))
                {
                    zipfile.FlattenFoldersOnExtract = flatfolder;
                    if (SelectionCriteria == null)
                    {
                        zipfile.ExtractAll(ExtractToDirectory, ExistingAction);
                        //foreach (Ionic.Zip.ZipEntry zipentry in zipfile)
                        //{
                        //    zipentry.Extract(ExtractToDirectory
                        //        , overwrite ? Ionic.Zip.ExtractExistingFileAction.OverwriteSilently: Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite);
                        //}
                    }
                    else
                    {
                        // 指定されたファイルだけを解凍する
                        zipfile.ExtractSelectedEntries(SelectionCriteria, null, ExtractToDirectory, ExistingAction);
                    }
                }
            }
        }
    }
    #endregion

    #region SFTPクラス
    /// <summary>
    /// SFTP通信処理を行うメソッドをまとめた静的クラス
    /// </summary>
    [ComVisible(false), GuidAttribute("893309A3-865B-4E92-A50F-AC0549F18790")]
    public class SFTP
    {
        private const string PUT_SCRIPT = "put.script";
        private const string GET_SCRIPT = "get.script";
        private const string COMMAND_SCRIPT1 = "command1.script";
        private const string COMMAND_SCRIPT2 = "command2.script";
        private readonly string[] DOS_COMMAND_ESCAPE = new string[] { "&" };
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region プロパティ
        /// <summary>ホスト名</summary>
        private string hostName = null;
        /// <summary>ユーザ名</summary>
        private string userId = null;
        /// <summary>パスワード</summary>
        private string password = null;
        /// <summary>公開鍵の指紋</summary>
        private string sshHostKey = null;
        /// <summary>WinSCPアプリ</summary>
        private string winscpCommand = null;
        /// <summary>コマンドスクリプトパス</summary>
        private string scriptPath = null;

        private ProcessStartInfo psi = null;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="hostName">SFTPホスト名</param>
        /// <param name="userId">SFTPユーザ名</param>
        /// <param name="password">SFTPパスワード</param>
        /// <param name="sshHostKey">公開鍵の指紋</param>
        /// <param name="winscpCommand">WinSCPコマンド</param>
        /// <param name="scriptPath">コマンドスクリプトパス</param>
        public SFTP(string hostName, string userId, string password, string sshHostKey, string winscpCommand, string scriptPath)
        {
            this.hostName = hostName;
            this.userId = userId;
            this.password = password;
            this.sshHostKey = sshHostKey;
            this.winscpCommand = winscpCommand;
            this.scriptPath = scriptPath;
            psi = new ProcessStartInfo();
            psi.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = false;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
        }

        private void execCommand(string command, params string[] commandParams)
        {
            if (commandParams != null && commandParams.Length != 0)
            {
                for (int i = 0; i < commandParams.Length; ++i)
                {
                    command += " " + EncodingCommandParamter(commandParams[i]);
                }
            }
            psi.Arguments = command + @"""";
            _log.Debug("SFTPコマンド:" + psi.Arguments);
            using (System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi))
            {
                string results = p.StandardError.ReadToEnd();
                results += p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                if (p.ExitCode != 0)
                {
                    throw new QCWebException(new Message(results)
                                            , GlobalsCommonConstant.LogLevel.FATAL);
                }
            }
        }

        /// <summary>
        /// 半角スペース、全角スペースを含む場合はダブルクォーテーションで囲む
        /// </summary>
        /// <param name="paramter"></param>
        /// <returns></returns>
        private string EncodingCommandParamter(string paramter)
        {
            if (paramter.IndexOf(" ") > 0 || paramter.IndexOf("　") > 0)
            {
                return @"""" + paramter + @"""";
            }
            return paramter;
        }

        private string GetBaseCommand(string cmd)
        {
            return @"/c """"" + this.winscpCommand + @""" /console /script=" + this.scriptPath + cmd + " /parameter ";
        }

        private string Escape(string cmd)
        {
            string escape = cmd;
            foreach (string ch in DOS_COMMAND_ESCAPE)
            {
                escape = escape.Replace(ch, "^" + ch);
            }

            return escape;
        }

        /// <summary>
        /// ファイルをアップロードするメソッド
        /// </summary>
        /// <param name="localpath">アップロードするファイルのローカルパス</param>
        /// <param name="serverpath">アップロード先のリモートパス</param>
        /// <returns></returns>
        public void PutFile(string localpath, string serverpath)
        {
            if (string.IsNullOrEmpty(localpath))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "localpath");
            }
            if (string.IsNullOrEmpty(serverpath))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "serverpath");
            }

            if (!File.Exists(localpath))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NotExistFileMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , localpath);
            }

            // ディレクトリの存在チェック
            string dirPath = Path.GetDirectoryName(serverpath).Replace(@"\", "/");
            try
            {
                // ディレクトリ作成
                ExecuteCommand("mkdir", dirPath, null);
            }
            catch (Exception)
            {
                // 例外が発生した場合は既にディレクトリがある
            }

            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            string command = GetBaseCommand(PUT_SCRIPT);

            execCommand(command + GetConnectionString(), Escape(localpath), Escape(serverpath));
        }

        /// <summary>
        /// ファイルをダウンロードするメソッド
        /// </summary>
        /// <param name="serverpath">ダウンロードするファイルのリモートパス</param>
        /// <param name="localpath">ダウンロード先のローカルパス</param>
        public void GetFile(string serverpath, string localpath)
        {
            // 必須チェック
            if (string.IsNullOrEmpty(localpath))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "localpath");
            }
            if (string.IsNullOrEmpty(serverpath))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "serverpath");
            }

            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            string command = GetBaseCommand(GET_SCRIPT);

            execCommand(command + GetConnectionString(), Escape(serverpath), Escape(localpath));
        }

        /// <summary>
        /// リモートサーバ上でコマンドを実行する
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        public void ExecuteCommand(string cmd, string parameter1 = null, string parameter2 = null)
        {
            if (string.IsNullOrEmpty(cmd))
            {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyParameterMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , "cmd");
            }

            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            string command = "";
            if (string.IsNullOrEmpty(parameter2))
            {
                command = GetBaseCommand(COMMAND_SCRIPT1);
            }
            else
            {
                command = GetBaseCommand(COMMAND_SCRIPT2);
            }
            command = command + GetConnectionString() + " " + cmd;
            if (!string.IsNullOrEmpty(parameter1))
            {
                command += " " + EncodingCommandParamter(parameter1);
                if (!string.IsNullOrEmpty(parameter2))
                {
                    command += " " + EncodingCommandParamter(parameter2);
                }
            }
            else
            {
                command += "";
            }

            execCommand(command);
        }

        /// <summary>
        /// SFTPコネクション情報の取得
        /// </summary>
        /// <returns>コネクション文字列</returns>
        private string GetConnectionString()
        {
            return string.Format(@"{0}:{1}@{2} {3}", this.userId, this.password, this.hostName, this.sshHostKey);
        }
    }
    #endregion

    #region リソース読み込み関連
    #region GetResourceクラス
    /// <summary>
    /// リソース読み込みメソッドを定義した静的クラス
    /// </summary>
    [ComVisible(false), Guid("DB971AF7-802A-4052-88EB-C6372DD504EE")]
    public static class GetResource
    {
        private static ResourceManager manager = null;
        private static string resourcesName = null;

        private static string GetResourcesData(string rn, string msgId, params string[] replaceWords)
        {
            const int MAX_PARAMS_COUNT = 30;    // パラメータリストの補完の際の最大数

            if (resourcesName != null && !resourcesName.Equals(rn))
            {
                manager = null;
            }
            resourcesName = rn;

            if (manager == null)
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                manager = new ResourceManager(asm.GetName().Name + resourcesName, asm);
            }
            string message = null;
            try
            {
                message = manager.GetString(msgId);
            }
            catch
            {
            }

            //if (message == null) return null;   // 取得失敗時には、MSGID_ERROR = "メッセージID が不正です。"を返すのがよいのか？
            if (message == null) return msgId;    // TODO:取得失敗時には、メッセージIDをメッセージとする
            // Regex regex = new Regex(@"`(QC([SB]\d{2}|CMN)\d{8})");
            // Regex regex = new Regex(@"`(QC(([SB]\d{2}|CMN)\d{8}|R\d{7}-[A-Za-z]{2}))");
            Regex regex = new Regex(@"`(QC(([SB]\d{3}|CMN\d|R)\d{7}))");
            if (regex.IsMatch(message))
            {
                MatchCollection ms = regex.Matches(message);
                for (int i = ms.Count - 1; i >= 0; --i)
                {
                    Match m = ms[i];
                    string submsgid = m.Groups[1].Value;
                    // string submsg = GetLogMessage(submsgid);
                    string submsg = GetResourcesData(rn, submsgid);
                    // 同じリソースファイルにない場合は、Messages.resourcesを見る
                    if (submsg.Equals(submsgid)) submsg = GetLogMessage(submsgid);
                    if (!submsg.Equals(submsgid)) // メッセージIDが正しくないものは置換しない
                    {
                        message = regex.Replace(message, submsg, 1, m.Index);
                    }
                }
            }

            if (replaceWords == null) replaceWords = new string[0];
            for (int i = 0; i < replaceWords.Length; ++i)
            {
                if (replaceWords[i] == null) replaceWords[i] = string.Empty;
            }

            string res = null;
            if (replaceWords.Length > 0)
            {
                try
                {
                    res = string.Format(message, replaceWords);
                }
                catch (FormatException)
                {
                    int c = replaceWords.Length;
                    Array.Resize<string>(ref replaceWords, MAX_PARAMS_COUNT);
                    for (int i = c; i < MAX_PARAMS_COUNT; ++i)
                    {
                        // 置換しない
                        replaceWords[i] = "{" + i.ToString() + "}";
                    }
                    try
                    {
                        res = string.Format(message, replaceWords);
                    }
                    catch
                    {
                        res = message;
                    }
                }
            }
            else
            {
                res = message;
            }
            return res;
        }


        /// <summary>
        /// メッセージIDからMessages.resourcesを読み込んで返す静的メソッド
        /// </summary>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">パラメータリスト (可変長)</param>
        /// <returns>リソースから取得した文字列</returns>
        public static string GetLogMessage(string msgId, params string[] replaceWords)
        {
            const string RESOURCE_NAME = ".Resources.Messages";
            return GetResourcesData(RESOURCE_NAME, msgId, replaceWords);
        }

        private static Hashtable resources = null;

        /// <summary>
        /// resxファイルのdataエレメントの情報を読み込む静的メソッド
        /// </summary>
        /// <param name="resxPath">resxファイルのパス</param>
        /// <returns>dataエレメントの名前をキーとし値を要素とするHashtableクラスのインスタンスへの参照</returns>
        public static Hashtable GetResourceDatas(string resxPath)
        {
            if (string.IsNullOrWhiteSpace(resxPath) || !File.Exists(resxPath)) return null;
            if (resources == null)
            {
                resources = new Hashtable();
            }
            else if (resources.ContainsKey(resxPath))
            {
                return resources[resxPath] as Hashtable;
            }
            XmlNameTable nameTable = new NameTable();
            nameTable.Add("data");
            try
            {
                XmlTextReader reader = new XmlTextReader(resxPath);
                Hashtable res = new Hashtable();
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element) continue;
                    if (!reader.Name.ToLower().Equals("data")) continue;
                    string name = reader.GetAttribute("name").ToLower();
                    if (name == null) continue;
                    bool eof = false;
                    do
                    {
                        eof = !reader.Read();
                    } while (!eof && reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement);
                    if (eof) break;
                    if (reader.NodeType != XmlNodeType.Element) continue;
                    if (!reader.Name.ToLower().Equals("value")) continue;
                    string value = string.Empty;
                    if (!reader.IsEmptyElement)
                    {
                        do
                        {
                            eof = !reader.Read();
                        } while (!eof && reader.NodeType != XmlNodeType.Text && reader.NodeType != XmlNodeType.EndElement);
                        if (eof) break;
                        if (reader.NodeType != XmlNodeType.Text) continue;
                        value = reader.Value;
                    }
                    res.Add(name, value);
                }
                resources.Add(resxPath, res);
                return res;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// resxファイルの<paramref name="key"/>が示す値を返す静的メソッド
        /// </summary>
        /// <param name="resxPath">resxファイルのパス</param>
        /// <param name="key">キー</param>
        /// <returns>resxファイル内でname属性の値が<paramref name="key"/>のdataエレメントの子エレメントvalueの値</returns>
        public static string GetResourceData(string resxPath, string key)
        {
            Hashtable datas = GetResourceDatas(resxPath);
            if (datas == null) return null;
            key = key.ToLower();
            if (!datas.ContainsKey(key)) return null;
            return datas[key].ToString();
        }

        private const string DEFALUT_LOCALE = "ja";
        /// <summary>
        /// 共通リソースファイルパスの取得
        /// </summary>
        /// <param name="msgId">メッセージID</param>
        /// <param name="locale">言語情報</param>
        /// <param name="replaceWords">パラメータリスト (可変長)</param>
        /// <returns>リソースから取得した文字列</returns>
        public static string GetCommonResourceData(string msgId, string locale = DEFALUT_LOCALE, params string[] replaceWords)
        {
            const string RESOURCE_NAME = ".Resources.Common-";
            string lccd = DEFALUT_LOCALE;
            if (locale != null && locale.Length >= 2) lccd = locale.Substring(0, 2).ToLower();
            return GetResourcesData(RESOURCE_NAME + lccd, msgId, replaceWords);
        }

        /// <summary>
        /// バッチリソースデータを返すメソッド
        /// </summary>
        /// <param name="index">レポートメッセージを表すConstants.ReportMessageIndex列挙型の値</param>
        /// <param name="lccd">ロケーション情報を表す2文字の文字列 (省略可、既定値ja)</param>
        /// <param name="replaceWords">パラメータリスト (可変長)</param>
        /// <returns></returns>
        public static string GetReportKeyword(Constants.ReportMessageIndex index, string lccd = "ja", params string[] replaceWords)
        {
            string msgId = Constants.GlobalConstants.REPORT_ID + ((int)index).ToString(new string('0', 7));
            return GetResource.GetCommonResourceData(msgId, lccd, replaceWords);
        }

        /// <summary>
        /// バッチリソースデータを返すメソッド
        /// </summary>
        /// <param name="index">レポートメッセージを表すConstants.ReportMessageIndex列挙型の値</param>
        /// <param name="lccd">ロケーション情報を表す2文字の文字列 (省略可、既定値ja)</param>
        /// <param name="unescape">アンエスケープが必要な場合はtrue (省略可、既定値false)</param>
        /// <param name="replaceWords">パラメータリスト (可変長)</param>
        /// <returns></returns>
        public static string GetReportKeyword(Constants.ReportMessageIndex index, string lccd = "ja", bool unescape = false, params string[] replaceWords)
        {
            string res = GetReportKeyword(index, lccd, replaceWords);
            if (unescape && res != null) res = System.Text.RegularExpressions.Regex.Unescape(res);
            return res;
        }

        /// <summary>
        /// バッチリソースデータを返すメソッド
        /// </summary>
        /// <param name="index">レポートメッセージを表すConstants.ReportMessageIndex列挙型の値</param>
        /// <returns></returns>
        public static string GetReportKeyword(Constants.ReportMessageIndex index)
        {
            return GetReportKeyword(index, "ja");
        }
    }
    #endregion

    #region 列挙型
    /// <summary>
    /// メッセージ区分を表す列挙型
    /// </summary>
    [ComVisible(false)]
    public enum Classification : int
    {
        /// <summary>
        /// 共通
        /// </summary>
        Common,
        /// <summary>
        /// 画面
        /// </summary>
        UI,
        /// <summary>
        /// バッチ
        /// </summary>
        Batch,
        /// <summary>
        /// 帳票
        /// </summary>
        Report
    }

    /// <summary>
    /// カテゴリコードを表す列挙型
    /// </summary>
    [ComVisible(false)]
    public enum CategoryCode : int
    {
        /// <summary>
        /// UI-調査リスト
        /// </summary>
        ResearchList = 1,
        /// <summary>
        /// UI-集計系
        /// </summary>
        Tablulation,
        /// <summary>
        /// UI-加工系
        /// </summary>
        DataProcess,
        /// <summary>
        /// UI-データ参照
        /// </summary>
        DataReference,
        /// <summary>
        /// UI-その他
        /// </summary>
        NotCategorized,
        /// <summary>
        /// バッチ-QC3取込
        /// </summary>
        ImportQC3 = 1,
        /// <summary>
        /// バッチ-データ削除
        /// </summary>
        DeleteData,
        /// <summary>
        /// バッチ-レポート
        /// </summary>
        Report
    }

    /// <summary>
    /// 機能コードを表す列挙型
    /// </summary>
    [ComVisible(false)]
    public enum FunctionCode : int
    {
        /// <summary>
        /// UI-調査リスト-調査リスト
        /// </summary>
        ResearchList = 1,
        /// <summary>
        /// UI-集計系-GT集計
        /// </summary>
        GTTablulation = 1,
        /// <summary>
        /// UI-集計系-クロス集計
        /// </summary>
        CrossTabulation,
        /// <summary>
        /// UI-集計系-FAリスト
        /// </summary>
        FAList,
        /// <summary>
        /// UI-集計系-レポート
        /// </summary>
        Report,
        /// <summary>
        /// UI-集計系-GT・クロス共通
        /// </summary>
        GTorCross,
        /// <summary>
        /// UI-集計系-出力共通
        /// </summary>
        Output,
        /// <summary>
        /// UI-集計系-シナリオ管理
        /// </summary>
        ManageScenario,
        /// <summary>
        /// UI-加工系-新規アイテム作成
        /// </summary>
        CreateNewItem = 1,
        /// <summary>
        /// UI-加工系-データクリーニング
        /// </summary>
        DataCleaning,
        /// <summary>
        /// UI-加工系-設問設定
        /// </summary>
        QuestionSetting,
        /// <summary>
        /// UI-加工系-チェックリスト
        /// </summary>
        CheckList,
        /// <summary>
        /// UI-加工系-設問加工共通
        /// </summary>
        DataProcess,
        /// <summary>
        /// UI-データ参照-データ参照
        /// </summary>
        DataReference = 1,
        /// <summary>
        /// UI-その他-その他
        /// </summary>
        NotCategorized = 1,
        /// <summary>
        /// バッチ-QC3取込-QC3取込
        /// </summary>
        ImportQC3 = 1,
        /// <summary>
        /// バッチ-データ削除-データ削除
        /// </summary>
        DeleteData = 1,
        /// <summary>
        /// バッチ-レポート-レポート
        /// </summary>
        ReportBatch = 1
    }

    /// <summary>
    /// Reportメッセージの分類を表す列挙型
    /// </summary>
    [ComVisible(true)]
    public enum ReportClassificationCode : int
    {
        /// <summary>
        /// キーワード
        /// </summary>
        Keyword,
        /// <summary>
        /// ブック名
        /// </summary>
        BookName,
        /// <summary>
        /// シート名
        /// </summary>
        SheetName,
        /// <summary>
        /// メッセージ
        /// </summary>
        Message = 10,
    }
    #endregion

    #region Messageクラス
    /// <summary>
    /// ログメッセージを管理するクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("C3A1AA48-2466-4494-B83E-DC580A40799D")]
    public class Message
    {
        private Classification classification = Classification.Common;
        private CategoryCode categorycode = (CategoryCode)0;
        private FunctionCode functioncode = (FunctionCode)0;
        private ReportClassificationCode reportclassificationcode = (ReportClassificationCode)0;
        private string msgid = null;
        private string description = null;

        #region コンストラクタ  
        private void init(Classification classification, CategoryCode categorycode, FunctionCode functioncode, int index)
        {
            if (!Enum.IsDefined(typeof(Classification), classification)) return;
            if (index < 0 || index > (classification == Classification.Common ? 99999999 : 999999)) return;
            StringBuilder msgId = new StringBuilder("");
            switch (classification)
            {
                case Classification.Common:
                    msgId.Append(Constants.GlobalConstants.COMMON_ID);
                    break;
                case Classification.UI:
                    msgId.Append(Constants.GlobalConstants.UI_ID);
                    switch (categorycode)
                    {
                        case CategoryCode.ResearchList:
                            switch (functioncode)
                            {
                                case FunctionCode.ResearchList:
                                    break;
                                default:
                                    return;
                            }
                            break;
                        case CategoryCode.Tablulation:
                            switch (functioncode)
                            {
                                case FunctionCode.GTTablulation:
                                case FunctionCode.CrossTabulation:
                                case FunctionCode.FAList:
                                case FunctionCode.Report:
                                case FunctionCode.GTorCross:
                                case FunctionCode.Output:
                                case FunctionCode.ManageScenario:
                                    break;
                                default:
                                    return;
                            }
                            break;
                        case CategoryCode.DataProcess:
                            switch (functioncode)
                            {
                                case FunctionCode.CreateNewItem:
                                case FunctionCode.DataCleaning:
                                case FunctionCode.QuestionSetting:
                                case FunctionCode.CheckList:
                                case FunctionCode.DataProcess:
                                    break;
                                default:
                                    return;
                            }
                            break;
                        case CategoryCode.DataReference:
                            switch (functioncode)
                            {
                                case FunctionCode.DataReference:
                                    break;
                                default:
                                    return;
                            }
                            break;
                        case CategoryCode.NotCategorized:
                            switch (functioncode)
                            {
                                case FunctionCode.NotCategorized:
                                    break;
                                default:
                                    return;
                            }
                            break;
                        default:
                            return;
                    }
                    break;
                case Classification.Batch:
                    msgId.Append(Constants.GlobalConstants.BATCH_ID);
                    switch (categorycode)
                    {
                        case CategoryCode.ImportQC3:
                            switch (functioncode)
                            {
                                case FunctionCode.ImportQC3:
                                    break;
                                default:
                                    return;
                            }
                            break;
                        case CategoryCode.DeleteData:
                            switch (functioncode)
                            {
                                case FunctionCode.DeleteData:
                                    break;
                                default:
                                    return;
                            }
                            break;
                        case CategoryCode.Report:
                            switch (functioncode)
                            {
                                case FunctionCode.ReportBatch:
                                    break;
                                default:
                                    return;
                            }
                            break;
                        default:
                            return;
                    }
                    break;
                case Classification.Report:
                    return; // 使わない
                // msgId.Append(Constants.GlobalConstants.REPORT_ID);
                default:
                    // ここに到達するのはおかしい
                    // 列挙型に値を追加した場合などの修正漏れ対応
                    return;
            }
            if (classification == Classification.Common)
            {
                msgId.Append(index.ToString(new string('0', 8)));
            }
            else
            {
                msgId.Append(((int)categorycode).ToString("00"));
                msgId.Append(((int)functioncode).ToString("00"));
                msgId.Append(index.ToString(new String('0', 6)));
            }
            // メンバ変数の初期化
            this.classification = classification;
            this.categorycode = categorycode;
            this.functioncode = functioncode;
            msgid = msgId.ToString();
            description = GetResource.GetLogMessage(msgid);
        }

        /// <summary>
        /// メッセージIDを生成して、それを基にリソース読み込みを行うコンストラクタ
        /// </summary>
        /// <param name="classification">メッセージ区分を表すClassification列挙型の値</param>
        /// <param name="categorycode">カテゴリコードを表すCategoryCode列挙型の値</param>
        /// <param name="functioncode">機能コードを表すFunctionCode列挙型の値</param>
        /// <param name="index"><paramref name="classification"/>がClassification.Commonの場合は最大8桁、それ以外では最大6桁のインデックス(連番)</param>
        public Message(Classification classification, CategoryCode categorycode, FunctionCode functioncode, int index)
        {
            init(classification, categorycode, functioncode, index);
        }

        /// <summary>
        /// 共通区分に特化して、リソース読み込みを行うコンストラクタ
        /// </summary>
        /// <param name="index">最大8桁のインデックス(連番)を表すConstants.CommonMessageIndex列挙型の値</param>
        public Message(Constants.CommonMessageIndex index)
        {
            if (!Enum.IsDefined(typeof(Constants.CommonMessageIndex), index)) return;
            init(Classification.Common, (CategoryCode)0, (FunctionCode)0, (int)index);
        }

        /// <summary>
        /// 共通区分に特化して、リソース読み込みを行うコンストラクタ
        /// </summary>
        /// <param name="index">最大8桁のインデックス(連番)</param>
        public Message(int index)
        {
            if (!Enum.IsDefined(typeof(Constants.CommonMessageIndex), index)) return;
            init(Classification.Common, (CategoryCode)0, (FunctionCode)0, index);
        }

        private void init(Constants.ReportMessageIndex index, string lccd, params string[] replaceWords)
        {
            if (!Enum.IsDefined(typeof(Constants.ReportMessageIndex), index)) return;
            if (lccd == null || (lccd = lccd.Replace(" ", "").ToUpper()).Length != 2) return;
            StringBuilder msgId = new StringBuilder(Constants.GlobalConstants.REPORT_ID);
            msgId.Append(((int)index).ToString(new string('0', 7)));
            //msgId.Append("_" + lccd);
            this.classification = Common.Classification.Report;
            reportclassificationcode = (ReportClassificationCode)((int)index / 100000);
            msgid = msgId.ToString();
            description = GetResource.GetCommonResourceData(msgid, lccd, replaceWords);
            //description = GetResource.GetLogMessage(msgid, replaceWords);
        }

        /// <summary>
        /// 帳票区分に特化して、リソース読み込みを行うコンストラクタ
        /// </summary>
        /// <param name="index">分類コード込のインデックスを表すConstants.ReportMessageIndex列挙型の値</param>
        /// <param name="lccd">ロケーション情報を表す2文字の文字列</param>
        /// <param name="replaceWords">パラメータリスト (可変長)</param>
        public Message(Constants.ReportMessageIndex index, string lccd, params string[] replaceWords)
        {
            init(index, lccd, replaceWords);
        }

        /// <summary>
        /// 帳票区分に特化して、リソース読み込みを行うコンストラクタ
        /// </summary>
        /// <param name="reportclassificationcode">Reportメッセージの分類を表すReportClassificationCode列挙型の値</param>
        /// <param name="index">最大5桁のインデックス(連番)</param>
        /// <param name="lccd">ロケーション情報を表す2文字の文字列</param>
        /// <param name="replaceWords">パラメータリスト (可変長)</param>
        public Message(ReportClassificationCode reportclassificationcode, int index, string lccd, params string[] replaceWords)
        {
            if (!Enum.IsDefined(typeof(ReportClassificationCode), reportclassificationcode)) return;
            if (index < 0 || index > 99999) return;
            init((Constants.ReportMessageIndex)((int)reportclassificationcode * 100000 + index), lccd, replaceWords);
        }

        /// <summary>
        /// メッセージ文を直接設定するコンストラクタ
        /// <note>メインメッセージのパラメータリストに指定する文字列のMessageクラスのインスタンス生成時に必要に応じて使用すること</note>
        /// </summary>
        /// <param name="description">メッセージ文</param>
        public Message(string description)
        {
            this.msgid = Constants.CUSTOM_MESSAGE_ID;
            this.description = description;
        }
        #endregion

        #region インスタンスメンバ
        /// <summary>
        /// メッセージ区分を表すClassification列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public Classification Classification
        {
            get
            {
                return classification;
            }
        }

        /// <summary>
        /// カテゴリコードを表すCategoryCode列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public CategoryCode CategoryCode
        {
            get
            {
                return categorycode;
            }
        }

        /// <summary>
        /// 機能コードを表すFunctionCode列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public FunctionCode FunctionCode
        {
            get
            {
                return functioncode;
            }
        }

        /// <summary>
        /// メッセージIDを返す読み取り専用プロパティ
        /// </summary>
        public string MessageID
        {
            get
            {
                return msgid;
            }
        }

        /// <summary>
        /// メッセージを返す読み取り専用プロパティ
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
        }
        #endregion
    }
    #endregion
    #endregion

    #region 配列操作関連
    /// <summary>
    /// 配列操作を行うメソッドをまとめた静的クラス
    /// </summary>
    [ComVisible(false), Guid("938DE2DE-024A-4E26-95D8-79C2661FAC46")]
    public static class OperateArray
    {
        /// <summary>
        /// 配列の要素を任意の値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">一次元配列</param>
        /// <param name="val">初期値</param>
        /// <param name="startIndex">初期値を投入する開始インデックス</param>
        /// <param name="endIndex">初期値を投入する終了インデックス</param>
        public static void InitializeWith<T>(ref T[] array, T val, int startIndex, int endIndex)
        {
            if (array == null || array.Length == 0) return;
            if (startIndex < 0) startIndex = 0;
            if (endIndex > array.GetUpperBound(0)) endIndex = array.GetUpperBound(0);
            if (startIndex > endIndex) return;
            array[startIndex] = val;
            int len = endIndex - startIndex + 1;
            for (int i = 1, n = i; i < len; i += n, n = i)
            {
                if (i + n > len) n = len - i;
                Array.Copy(array, startIndex, array, startIndex + i, n);
            }
        }

        /// <summary>
        /// 配列の要素を任意の値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">一次元配列</param>
        /// <param name="val">初期値</param>
        public static void InitializeWith<T>(ref T[] array, T val)
        {
            if (array == null) return;
            int startIndex = 0;
            int endIndex = array.GetUpperBound(0);
            InitializeWith<T>(ref array, val, startIndex, endIndex);
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
        public static void InitializeWith<T>(ref T[,] array, T val, int startRowIndex, int endRowIndex, int startColumnIndex, int endColumnIndex)
        {
            if (array == null || array.GetLength(0) == 0 || array.GetLength(1) == 0) return;
            if (startRowIndex < 0) startRowIndex = 0;
            if (endRowIndex > array.GetUpperBound(0)) endRowIndex = array.GetUpperBound(0);
            if (startRowIndex > endRowIndex) return;
            if (startColumnIndex < 0) startColumnIndex = 0;
            if (endColumnIndex > array.GetUpperBound(1)) endColumnIndex = array.GetUpperBound(1);
            if (startColumnIndex > endColumnIndex) return;
            int columnsCount = endColumnIndex - startColumnIndex + 1;
            if (columnsCount == array.GetLength(1))
            {
                int startIndex = startRowIndex * columnsCount + startColumnIndex;
                int endIndex = endRowIndex * columnsCount + endColumnIndex;
                array[startRowIndex, startColumnIndex] = val;
                int len = endIndex - startIndex + 1;
                for (int i = 1, n = i; i < len; i += n, n = i)
                {
                    if (i + n > len) n = len - i;
                    Array.Copy(array, startIndex, array, startIndex + i, n);
                }
            }
            else
            {
                for (int r = startRowIndex; r <= endRowIndex; ++r)
                {
                    array[r, startColumnIndex] = val;
                    int startIndex = r * array.GetLength(1) + startColumnIndex;
                    for (int i = 1, n = i; i < columnsCount; i += n, n = i)
                    {
                        if (i + n > columnsCount) n = columnsCount - i;
                        Array.Copy(array, startIndex, array, startIndex + i, n);
                    }
                }
            }
        }

        /// <summary>
        /// 配列の要素を任意の値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">二次元配列</param>
        /// <param name="val">初期値</param>
        /// <param name="startRowIndex">初期値を投入する行方向開始インデックス</param>
        /// <param name="endRowIndex">初期値を投入する行方向終了インデックス</param>
        public static void InitializeWith<T>(ref T[,] array, T val, int startRowIndex, int endRowIndex)
        {
            if (array == null) return;
            int startColumnIndex = 0;
            int endColumnIndex = array.GetUpperBound(1);
            InitializeWith<T>(ref array, val, startRowIndex, endRowIndex, startColumnIndex, endColumnIndex);
        }

        /// <summary>
        /// 配列の要素を任意の値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">二次元配列</param>
        /// <param name="val">初期値</param>
        public static void InitializeWith<T>(ref T[,] array, T val)
        {
            if (array == null) return;
            int startRowIndex = 0;
            int endRowIndex = array.GetUpperBound(0);
            int startColumnIndex = 0;
            int endColumnIndex = array.GetUpperBound(1);
            InitializeWith<T>(ref array, val, startRowIndex, endRowIndex, startColumnIndex, endColumnIndex);
        }

        /// <summary>
        /// 配列の要素を初期値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">一次元配列</param>
        public static void Initialize<T>(ref T[] array)
        {
            if (array == null || array.Length == 0) return;
            array = new T[array.Length];
        }

        /// <summary>
        /// 配列の要素を初期値で初期化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">二次元配列</param>
        public static void Initialize<T>(ref T[,] array)
        {
            if (array == null || array.GetLength(0) == 0 || array.GetLength(1) == 0) return;
            array = new T[array.GetLength(0), array.GetLength(1)];
        }
    }
    #endregion
}