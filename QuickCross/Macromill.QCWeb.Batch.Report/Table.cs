#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Table.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/2/20
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/8
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Macromill.QCWeb.ReportRequest;
using System.Diagnostics;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Exceptions;

namespace Macromill.QCWeb.Batch.Report
{
    /// <summary>
    /// 集計表のコレクションクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("04ED7562-C223-4f01-98B6-1ED64C733D53")]
    public class Tables : Hashtable, ITables
    {
        /// <summary>
        /// 集計表を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("77CFDEEA-9EEC-41ed-B17D-8C66A1256316")]
        public class Table : ITable
        {
            #region 定数
            /// <summary>
            /// GT
            /// </summary>
            protected const string GT_PROMPT = "GT";
            /// <summary>
            /// クロス
            /// </summary>
            protected const string CROSS_PROMPT = "クロス";
            /// <summary>
            /// FAリスト
            /// </summary>
            protected const string FALIST_PROMPT = "FAリスト";
            /// <summary>
            /// チェックリスト
            /// </summary>
            protected const string CHECKLIST_PROMPT = "チェックリスト";
            /// <summary>
            /// 調査票
            /// </summary>
            protected const string QUESTIONNAIRE_PROMPT = "調査票";
            /// <summary>
            /// ローデータ
            /// </summary>
            protected const string RAWDATA_PROMPT = "ローデータ";
            /// <summary>
            /// アイテム情報
            /// </summary>
            protected const string ITEM_INFORMATION_PROMPT = "アイテム情報";
            /// <summary>
            /// 選択肢情報
            /// </summary>
            protected const string SECTORS_INFORMATION_PROMPT = "選択肢情報";
            /// <summary>
            /// 選択肢数
            /// </summary>
            protected const string SECTORS_COUNT_PROMPT = "選択肢数";
            /// <summary>
            /// 分類アイテム情報
            /// </summary>
            protected const string KEY_ITEM_INFORMATION_PROMPT = "分類" + ITEM_INFORMATION_PROMPT;
            /// <summary>
            /// 集計対象アイテム情報
            /// </summary>
            protected const string MAIN_ITEM_INFORMATION_PROMPT = "集計対象" + ITEM_INFORMATION_PROMPT;
            /// <summary>
            /// 項目間検定コード
            /// </summary>
            protected const string SIGNIFICANCE_TEST_CODE_PROMPT = "項目間検定コード";
            /// <summary>
            /// 子質問数
            /// </summary>
            protected const string CHILD_QUESTIONS_COUNT_PROMPT = "子質問数";
            /// <summary>
            /// グラフ情報
            /// </summary>
            protected const string CHART_INFORMATION_PROMPT = "グラフ情報";
            /// <summary>
            /// グラフ情報 - グラデーション情報
            /// </summary>
            protected const string CHART_GRADIENT_INFORMATION_PROMPT = CHART_INFORMATION_PROMPT + " - グラデーション情報";
            /// <summary>
            /// アイテム情報 - 選択肢情報
            /// </summary>
            protected const string ITEM_SECTORS_INFORMATION_PROMPT = ITEM_INFORMATION_PROMPT + " - " + SECTORS_INFORMATION_PROMPT;
            /// <summary>
            /// アイテム情報 - 選択肢数
            /// </summary>
            protected const string ITEM_SECTORS_COUNT_PROMPT = ITEM_INFORMATION_PROMPT + " - " + SECTORS_COUNT_PROMPT;
            /// <summary>
            /// 集計対象アイテム情報 - 選択肢情報
            /// </summary>
            protected const string MAIN_ITEM_SECTORS_INFORMATION_PROMPT = "集計対象" + ITEM_SECTORS_INFORMATION_PROMPT;
            /// <summary>
            /// 集計対象アイテム情報
            /// </summary>
            protected const string MAIN_ITEM_SECTORS_COUNT_PROMPT = "集計対象" + ITEM_INFORMATION_PROMPT;
            /// <summary>
            /// 集計軸アイテム情報
            /// </summary>
            protected const string AXES_ITEM_INFORMATION_PROMPT = "集計軸" + ITEM_INFORMATION_PROMPT;
            /// <summary>
            /// 集計軸グループ数
            /// </summary>
            protected const string AXES_GROUPS_COUNT_PROMPT = "集計軸グループ数";
            /// <summary>
            /// 集計軸数
            /// </summary>
            protected const string AXES_COUNT_PROMPT = "集計軸数";
            /// <summary>
            /// 集計軸アイテム情報 - 選択肢数
            /// </summary>
            protected const string AXIS_ITEM_SECTORS_COUNT_PROMPT = "集計軸" + ITEM_SECTORS_COUNT_PROMPT;
            /// <summary>
            /// 折れ線グラフ行情報
            /// </summary>
            protected const string LINE_CHART_ROWS_INFORMATION_PROMPT = "折れ線グラフ行情報";
            /// <summary>
            /// アイテム数情報
            /// </summary>
            protected const string ITEMS_COUNT_INFORMATION_PROMPT = "アイテム数情報";
            /// <summary>
            /// FAアイテム数
            /// </summary>
            protected const string FA_ITEMS_COUNT_PROMPT = "FAアイテム数";
            /// <summary>
            /// 付加アイテム数
            /// </summary>
            protected const string ADDED_ITEMS_COUNT_PROMPT = "付加アイテム数";
            /// <summary>
            /// 先頭アイテム名
            /// </summary>
            protected const string FIRST_ITEM_NAME_PROMPT = "先頭アイテム名";
            /// <summary>
            /// フラグ情報
            /// </summary>
            protected const string FLAG_INFORMATION_PROMPT = "フラグ情報";
            /// <summary>
            /// 子質問情報
            /// </summary>
            protected const string CHILD_QUESTIONS_INFORMATION_PROMPT = "子質問情報";
            /// <summary>
            /// データタイプコード
            /// </summary>
            protected const string DATA_TYPE_CODE_PROMPT = "データタイプコード";
            /// <summary>
            /// QInfo調査情報
            /// </summary>
            protected const string RESEARCH_INFORMATION_PROMPT = "QInfo調査情報";
            /// <summary>
            /// QInfo調査情報 - 調査ID
            /// </summary>
            protected const string RESEARCH_ID_INFORMATION_PROMPT = RESEARCH_INFORMATION_PROMPT + " - 調査ID";
            /// <summary>
            /// QInfo調査情報 - 調査方法
            /// </summary>
            protected const string RESEARCH_METHOD_INFORMATION_PROMPT = RESEARCH_INFORMATION_PROMPT + " - 調査方法";
            /// <summary>
            /// QInfo調査情報 - 商品種別
            /// </summary>
            protected const string RESEARCH_SERVICE_INFORMATION_PROMPT = RESEARCH_INFORMATION_PROMPT + " - 商品種別";
            /// <summary>
            /// QInfo調査情報 - 実施期間
            /// </summary>
            protected const string RESEARCH_PERIODS_INFORMATION_PROMPT = RESEARCH_INFORMATION_PROMPT + " - 実施期間";
            /// <summary>
            /// QInfo割付セル情報
            /// </summary>
            protected const string CELLS_INFORMATION_PROMPT = "QInfo割付セル情報";
            /// <summary>
            /// QInfoセレクト条件情報
            /// </summary>
            protected const string RULES_INFORMATION_PROMPT = "QInfoセレクト条件情報";
            /// <summary>
            /// レイアウト表向きコード
            /// </summary>
            protected const string LAYOUT_ORIENTATION_CODE_PROMPT = "レイアウト表向きコード";
            #endregion

            private int index = 0;
            private string information = null;
            private string[][] datavalue = null;
            /// <summary>
            /// DataWithMarking型の二段階配列
            /// </summary>
            protected Tabulation.DataWithMarking[][] datawithmarking = null;
            private bool withmarking = false;
            private string comment = null;

            private Tables Collection = null;

            /// <summary>
            /// [性能対策]行と列を引数に持ち、文字列を戻り値に持つデリゲート型の定義
            /// </summary>
            /// <param name="r">行インデックス</param>
            /// <param name="c">列インデックス</param>
            /// <returns>文字列</returns>
            protected delegate string StringByMatrix(int r, int c);

            /// <summary>
            /// [性能対策]文字列の２次元配列を返す
            /// </summary>
            /// <param name="func">行と列を引数に持ち、文字列を戻り値に持つ関数の参照</param>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            protected string[,] ByMatrixForString(StringByMatrix func, int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                string[,] res = new string[RowIndexTo + 1, ColumnIndexTo + 1];
                for (int r = RowIndexFrom; r <= RowIndexTo; ++r)
                {
                    for (int c = ColumnIndexFrom; c <= ColumnIndexTo; ++c)
                    {
                        res[r, c] = func(r, c);
                    }
                }
                return res;
            }

            #region コンストラクタ
#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            Table(Tables tables, string tsvpath, bool withmarking = false)
            {
                Collection = tables;
                index = Collection.Count;
                this.withmarking = withmarking;
                // TSVのget
                if (string.IsNullOrWhiteSpace(tsvpath) || !System.IO.File.Exists(tsvpath))
                {
                    throw new QCWebException("QCB0301012011", new string[] { tsvpath }, GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                // テンポラリファイルパス
                string tempPath = System.IO.Path.GetTempFileName();
                System.IO.StreamReader reader = null;
                try
                {
                    // tsvpath→tempPath (上書きフラグは念のため指定)
                    System.IO.File.Copy(tsvpath, tempPath, true);
                    reader = new System.IO.StreamReader(tempPath);
                }
                catch (Exception e)
                {
                    try
                    {
                        System.IO.File.Delete(tempPath);
                    }
                    catch
                    {
                    }
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    throw;
                }
                using (reader)
                {
                    try
                    {
                        information = reader.ReadLine();
                        int columnsCount = 0;
                        for (int i = 0; !reader.EndOfStream; ++i)
                        {
                            string rowBuffer = reader.ReadLine();
                            string[] splitBuffer = rowBuffer.Split('\t');
                            if (i == 0)
                            {
                                columnsCount = splitBuffer.Length;
                            }
                            else if (splitBuffer.Length != columnsCount)
                            {
                                Array.Resize<string>(ref splitBuffer, columnsCount);
                            }
                            if (withmarking)
                            {
                                Array.Resize<Tabulation.DataWithMarking[]>(ref datawithmarking, i + 1);
                                Array.Resize<Tabulation.DataWithMarking>(ref datawithmarking[i], columnsCount);
                                for (int j = 0; j < columnsCount; ++j)
                                {
                                    string[] tmpSplit = splitBuffer[j].Split('\v');
                                    if (tmpSplit.Length != 4) Array.Resize<string>(ref tmpSplit, 4);
                                    datawithmarking[i][j] = new Tabulation.DataWithMarking(tmpSplit[0], false);
                                    double n = 0.0;
                                    if (double.TryParse(tmpSplit[0], out n))
                                    {
                                        double p = 0.0;
                                        if (double.TryParse(tmpSplit[1], out p))
                                        {
                                            datawithmarking[i][j].Percent = p;
                                        }
                                        int m = 0;
                                        if (int.TryParse(tmpSplit[2], out m))
                                        {
                                            Tabulation.DataMarking mark = (Tabulation.DataMarking)m;
                                            mark &= Tabulation.DataMarking.ColoringAllBit | Tabulation.DataMarking.RankingAllBit | Tabulation.DataMarking.AscendingAllBit | Tabulation.DataMarking.SignificanceAllBit;
                                            datawithmarking[i][j].AppendMarking(mark, true);
                                        }
                                    }
                                    datawithmarking[i][j].SetSignificanceCharacters(tmpSplit[3]);
                                }
                            }
                            else
                            {
                                Array.Resize<string[]>(ref datavalue, i + 1);
                                datavalue[i] = splitBuffer;
                                // datavalue[i] = Array.ConvertAll<string, string>(
                                //             splitBuffer, x => System.Text.RegularExpressions.Regex.Unescape(x));
                            }
                        }
                        reader.Close();
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
                        try
                        {
                            System.IO.File.Delete(tempPath);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                            Debug.Indent();
                            Debug.WriteLine("Type:{0}", e.GetType().ToString());
                            Debug.WriteLine("Description:{0}", e.Message);
                            Debug.Unindent();
                        }
                    }
                }
            }
            #endregion

            #region インスタンスメンバ
            /// <summary>
            /// インデックス番号(0ベース)を返す読み取り専用プロパティ
            /// </summary>
            public int Index
            {
                get
                {
                    return index;
                }
            }

            /// <summary>
            /// 集計表のセルのN値の文字列表現またはキャプションを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <param name="Unescape">アンエスケープ処理を行うかどうかを示すフラグ</param>
            /// <param name="WhitespaceIsNull">スペースのみの場合にnullを返す場合はtrue</param>
            /// <returns>
            /// 集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるセルの文字列データまたはN値の文字列表現<br />
            /// <paramref name="Unescape"/>がtrueのときには、正規表現のアンエスケープ処理を行ってから返す
            /// </returns>
            // 戻り値に配列を指定すると配列のコピーが返される模様で、配列への参照を返すことはできなさそう
            // (COMからの呼び出しの場合)
            // 配列のコピーは大量データの場合、最悪死ぬので、逐次アクセス
            // (→あるいはExcelマクロ側でファイル読み込みを行うか→余裕があればパフォーマンスの差を計測して決定)
            public string TableValue(int RowIndex, int ColumnIndex, bool Unescape, bool WhitespaceIsNull)
            {
                string res = null;
                if (withmarking)
                {
                    if (datawithmarking == null || datawithmarking[datawithmarking.GetLowerBound(0)] == null) return null;
                    if (RowIndex < datawithmarking.GetLowerBound(0) || RowIndex > datawithmarking.GetUpperBound(0)
                        || ColumnIndex < datawithmarking[RowIndex].GetLowerBound(0) || ColumnIndex > datawithmarking[RowIndex].GetUpperBound(0))
                    {
                        return null;
                    }
                    res = datawithmarking[RowIndex][ColumnIndex].Value;
                }
                else
                {
                    if (datavalue == null || datavalue[datavalue.GetLowerBound(0)] == null) return null;
                    if (RowIndex < datavalue.GetLowerBound(0) || RowIndex > datavalue.GetUpperBound(0)
                        || ColumnIndex < datavalue[RowIndex].GetLowerBound(0) || ColumnIndex > datavalue[RowIndex].GetUpperBound(0))
                    {
                        return null;
                    }
                    res = datavalue[RowIndex][ColumnIndex];
                }
                if (Unescape)
                {
                    res = System.Text.RegularExpressions.Regex.Unescape(res + string.Empty);
                }
                if (WhitespaceIsNull)
                {
                    if (string.IsNullOrWhiteSpace(res)) res = null;
                }
                else if (string.IsNullOrEmpty(res))
                {
                    res = null;
                }
                return res;
            }

            /// <summary>
            /// 集計表のセルのN値の文字列表現またはキャプションを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <param name="Unescape">アンエスケープ処理を行うかどうかを示すフラグ (省略可、既定値false)</param>
            /// <returns>
            /// 集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるセルの文字列データまたはN値の文字列表現<br />
            /// <paramref name="Unescape"/>がtrueのときには、正規表現のアンエスケープ処理を行ってから返す
            /// </returns>
            public string TableValue(int RowIndex, int ColumnIndex, bool Unescape = false)
            {
                return TableValue(RowIndex, ColumnIndex, Unescape, true);
            }

            /// <summary>
            /// [性能対策]集計表のセルのN値の文字列表現またはキャプションを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <param name="Unescape">アンエスケープ処理を行うかどうかを示すフラグ (省略可、既定値false)</param>
            /// <returns>セルの文字列データまたはN値の文字列表現を２次元配列で返す</returns>
            public string[,] TableValueByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo, bool Unescape = false)
            {
                return ByMatrixForString((int r, int c) => { return TableValue(r, c, Unescape); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルの％値を返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内でRowIndexとColumnIndexとで示されるセルの％値</returns>
            public double PercentValue(int RowIndex, int ColumnIndex)
            {
                if (!withmarking || datawithmarking == null || datawithmarking[datawithmarking.GetLowerBound(0)] == null) return 0.0;
                if (RowIndex < datawithmarking.GetLowerBound(0) || RowIndex > datawithmarking.GetUpperBound(0)
                    || ColumnIndex < datawithmarking[RowIndex].GetLowerBound(0) || ColumnIndex > datawithmarking[RowIndex].GetUpperBound(0))
                {
                    return 0.0;
                }
                return datawithmarking[RowIndex][ColumnIndex].Percent;
            }

            /// <summary>
            /// [性能対策]集計表のセルの％値の２次元配列を返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>集計表データ内のセルの％値の２次元配列</returns>
            public double[,] PercentValueByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                double[,] res = new double[RowIndexTo + 1, ColumnIndexTo + 1];
                for (int r = RowIndexFrom; r <= RowIndexTo; ++r)
                {
                    for (int c = ColumnIndexFrom; c <= ColumnIndexTo; ++c)
                    {
                        res[r, c] = PercentValue(r, c);
                    }
                }
                return res;
            }

            /// <summary>
            /// 集計表の行データを文字列型配列で返すメソッド<br />
            /// アンエスケープ処理はしない
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <returns><paramref name="RowIndex"/>が示す行の行データが入った一次元配列</returns>
#if FOR_UNIT_TEST
            public
#else
            protected
#endif
            string[] GetSliceData(int RowIndex)
            {
                if (datavalue == null) return null;
                if (RowIndex < datavalue.GetLowerBound(0) || RowIndex > datavalue.GetUpperBound(0)) return null;
                return datavalue[RowIndex];
            }

            /// <summary>
            /// 集計表のセルのマーキング情報を表すDataMarking列挙型の値を返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内でRowIndexとColumnIndexとで示されるデータのマーキング情報を表すDataMarking列挙型の値</returns>
            public Tabulation.DataMarking DataMarking(int RowIndex, int ColumnIndex)
            {
                const Tabulation.DataMarking res0 = (Tabulation.DataMarking)0;
                if (!withmarking || datawithmarking == null || datawithmarking[datawithmarking.GetLowerBound(0)] == null) return res0;
                if (RowIndex < datawithmarking.GetLowerBound(0) || RowIndex > datawithmarking.GetUpperBound(0)
                    || ColumnIndex < datawithmarking[RowIndex].GetLowerBound(0) || ColumnIndex > datawithmarking[RowIndex].GetUpperBound(0))
                {
                    return res0;
                }
                return datawithmarking[RowIndex][ColumnIndex].Marking;
            }

            /// <summary>
            /// 集計表のセルの項目間検定レターを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内でRowIndexとColumnIndexとで示されるデータの項目間検定レター</returns>
            public string SignificanceTestCharacters(int RowIndex, int ColumnIndex)
            {
                if (!withmarking || datawithmarking == null || datawithmarking[datawithmarking.GetLowerBound(0)] == null) return null;
                if (RowIndex < datawithmarking.GetLowerBound(0) || RowIndex > datawithmarking.GetUpperBound(0)
                    || ColumnIndex < datawithmarking[RowIndex].GetLowerBound(0) || ColumnIndex > datawithmarking[RowIndex].GetUpperBound(0))
                {
                    return null;
                }
                return datawithmarking[RowIndex][ColumnIndex].SignificanceCharacters();
            }

            /// <summary>
            /// [性能対策]集計表のセルの項目間検定レターを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public string[,] SignificanceTestCharactersByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForString((int r, int c) => { return SignificanceTestCharacters(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表データの行インデックスの最小値を返す読み取り専用プロパティ
            /// </summary>
            public int GetTableValueRowIndexMinimum
            {
                get
                {
                    if (withmarking)
                    {
                        if (datawithmarking == null) return -1;
                        return datawithmarking.GetLowerBound(0);
                    }
                    else
                    {
                        if (datavalue == null) return -1;
                        return datavalue.GetLowerBound(0);
                    }
                }
            }

            /// <summary>
            /// 集計表データの行インデックスの最大値を返す読み取り専用プロパティ
            /// </summary>
            public int GetTableValueRowIndexMaximum
            {
                get
                {
                    if (withmarking)
                    {
                        if (datawithmarking == null) return -1;
                        return datawithmarking.GetUpperBound(0);
                    }
                    else
                    {
                        if (datavalue == null) return -1;
                        return datavalue.GetUpperBound(0);
                    }
                }
            }

            /// <summary>
            /// 集計表データの列インデックスの最小値を返す読み取り専用プロパティ
            /// </summary>
            public int GetTableValueColumnIndexMinimum
            {
                get
                {
                    if (withmarking)
                    {
                        if (datawithmarking == null || datawithmarking[datawithmarking.GetLowerBound(0)] == null) return -1;
                        return datawithmarking[datawithmarking.GetLowerBound(0)].GetLowerBound(0);
                    }
                    else
                    {
                        if (datavalue == null || datavalue[datavalue.GetLowerBound(0)] == null) return -1;
                        return datavalue[datavalue.GetLowerBound(0)].GetLowerBound(0);
                    }
                }
            }

            /// <summary>
            /// 集計表データの列インデックスの最大値を返す読み取り専用プロパティ
            /// </summary>
            public int GetTableValueColumnIndexMaximum
            {
                get
                {
                    if (withmarking)
                    {
                        if (datawithmarking == null || datawithmarking[datawithmarking.GetLowerBound(0)] == null) return -1;
                        return datawithmarking[datawithmarking.GetLowerBound(0)].GetUpperBound(0);
                    }
                    else
                    {
                        if (datavalue == null || datavalue[datavalue.GetLowerBound(0)] == null) return -1;
                        return datavalue[datavalue.GetLowerBound(0)].GetUpperBound(0);
                    }
                }
            }

            /// <summary>
            /// TSVファイルの内容から、集計表設定情報を切り出して返す読み取り専用プロパティ
            /// </summary>
#if FOR_UNIT_TEST
            public
#else
            protected
#endif
            string Information
            {
                get
                {
                    return information;
                }
            }

            /// <summary>
            /// コメントを取得/設定するプロパティ<br />
            /// 設定が有効なのは1度だけ
            /// <note>設定するコメントのアンエスケープ処理は内部で行うので、設定時には行わないこと</note>
            /// </summary>
            public string Comment
            {
                get
                {
                    return comment;
                }
                protected set
                {
                    if (comment == null) comment = System.Text.RegularExpressions.Regex.Unescape(value);
                }
            }

            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                Collection = null;
            }

            /// <summary>
            /// 自身のインスタンスが格納されているTablesコレクションクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public ITables ParentCollection
            {
                get
                {
                    return null;
                    //                    return Collection;
                }
            }

            /// <summary>
            /// 自身のインスタンスの親であるOutputクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IOutput ParentOutput
            {
                get
                {
                    if (Collection == null) return null;
                    return Collection.ParentOutput;
                }
            }

            /// <summary>
            /// 自身のインスタンスの親であるReportsetクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IReportset ParentReportset
            {
                get
                {
                    if (Collection == null) return null;
                    return Collection.ParentReportset;
                }
            }

            /// <summary>
            /// 自身のインスタンスの親であるRequestクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IRequest ParentRequest
            {
                get
                {
                    if (Collection == null) return null;
                    return Collection.ParentRequest;
                }
            }
            #endregion
        }

        /// <summary>
        /// GT表の集計表を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("8B49CA65-71CB-4f43-B96B-D9985E048EBF")]
        public class GTTable : Table, IGTTable
        {
            private KeyItemInformation keyiteminformation = null;
            private QuestionInformation questioninformation = null;
            private SignificanceTestCode significancetestcode = SignificanceTestCode.Off;
            private int childquestionscount = 0;
            private ChartInformation chartinformation = null;
            private int hidechartdescriptionmaxpercent = -1;
            private SectorInformation[] sectorinformations = null;
            /// <summary>
            /// 親質問情報
            /// </summary>
            protected string parentQInfo = null;
            /// <summary>
            /// クロス時TSVファイル内の集計軸情報部分の文字列をそのまま保持するプロパティ
            /// </summary>
            protected string axesInformation { get; private set; }
            /// <summary>
            /// クロス時TSVファイル内の折れ線グラフ行情報部分の文字列をそのまま保持するプロパティ
            /// </summary>
            protected string linechartrowInformation { get; private set; }

            /// <summary>
            /// コンストラクタのサブルーチン<br />
            /// GTまたはクロスの集計表情報を精査、整理する
            /// </summary>
            /// <param name="isCross">クロス集計時はtrueを指定</param>
#if FOR_UNIT_TEST
            public
#else
            protected
#endif
            void GetGTTableInformation(bool isCross)
            {
                string prompt = isCross ? CROSS_PROMPT : GT_PROMPT;
                string[] informations = this.Information.Split('\f');   // FF区切りでザックリ分割
                if (informations.Length < 4)
                {
                    throw new QCWebException(Constants.INSUFFICIENT_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { prompt, "" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }

                // informations[0] = 分類アイテム情報
                string[] keyiteminformations = informations[0].Split('\t');
                if (keyiteminformations.Length != 4)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { prompt, "(" + KEY_ITEM_INFORMATION_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                if (!string.IsNullOrWhiteSpace(keyiteminformations[0]))
                {
                    keyiteminformation = new KeyItemInformation(keyiteminformations[0], keyiteminformations[1], keyiteminformations[2], keyiteminformations[3], true);
                }

                // informations[1] = 集計対象アイテム情報
                string[] questioninformations = informations[1].Split('\t');
                if (questioninformations.Length < 4 || questioninformations.Length > 13 + (GlobalMethodClass.CInt(isCross) & 1))
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { prompt, "(" + MAIN_ITEM_INFORMATION_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                if (isCross)
                    questioninformation = new QuestionInformation(questioninformations[0], questioninformations[1], questioninformations[2], false, true,
                       Convert.ToBoolean(questioninformations[5]), questioninformations[6], Convert.ToInt32(questioninformations[7]), Convert.ToBoolean(questioninformations[8]),
                       System.Text.RegularExpressions.Regex.Unescape(questioninformations[9]),
                       System.Text.RegularExpressions.Regex.Unescape(questioninformations[10]),
                       System.Text.RegularExpressions.Regex.Unescape(questioninformations[11]), questioninformations[12],
                       Convert.ToBoolean(questioninformations[13]));
                else if (this.GetType() == typeof(GTTable))
                    questioninformation = new QuestionInformation(questioninformations[0], questioninformations[1],
                        questioninformations[2], false, true,
                        Convert.ToBoolean(questioninformations[4]), null, Convert.ToInt32(questioninformations[5]), Convert.ToBoolean(questioninformations[6]),null,System.Text.RegularExpressions.Regex.Unescape(questioninformations[9]),null,questioninformations[7],
                                          Convert.ToBoolean(questioninformations[8]));
                else
                    questioninformation = new QuestionInformation(questioninformations[0], questioninformations[1], questioninformations[2], false, true);

                int sigCode = 0;
                if (!int.TryParse(questioninformations[3], out sigCode))
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { prompt, "(" + SIGNIFICANCE_TEST_CODE_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                significancetestcode = SignificanceTestCode.Off;
                switch ((SignificanceTestCode)sigCode)
                {
                    case SignificanceTestCode.BetweenSectors:
                        if ((questioninformation.QuestionType & Tabulation.QuestionType.N) != Tabulation.QuestionType.N)
                        {
                            significancetestcode = SignificanceTestCode.BetweenSectors;
                        }
                        break;
                    case SignificanceTestCode.BetweenChildQuestions:
                        if (!isCross && (questioninformation.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
                        {
                            significancetestcode = SignificanceTestCode.BetweenChildQuestions;
                        }
                        break;
                }
                if (isCross)
                {
                    if (questioninformations.Length == 5) parentQInfo = questioninformations[4];
                }

                bool isN = false;
                bool isRatio = false;

                if ((questioninformation.QuestionType & Tabulation.QuestionType.N) == Tabulation.QuestionType.N)
                {
                    if ((questioninformation.QuestionType & Tabulation.QuestionType.Ratio) == Tabulation.QuestionType.Ratio)
                    {
                        if (informations.Length != 5)
                        {
                            throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                        , new string[] { prompt, "" }
                                        , GlobalsCommonConstant.LogLevel.FATAL, null);
                        }
                        isRatio = true;
                    }
                    else
                    {
                        if (informations.Length != (isCross ? 5 : 4))
                        {
                            throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                        , new string[] { prompt, "" }
                                        , GlobalsCommonConstant.LogLevel.FATAL, null);
                        }
                        isN = true;
                    }
                }
                else
                {
                    if (informations.Length != (isCross ? 7 : 6))
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { prompt, "" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                }

                // informations[2] = GT:子質問数情報, Cross:集計軸アイテム情報
                if (isCross)
                {
                    axesInformation = informations[2];
                }
                else if ((questioninformation.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
                {
                    int cCnt = 0;
                    if (!int.TryParse(informations[2], out cCnt) || cCnt <= 0)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { prompt, "(" + CHILD_QUESTIONS_COUNT_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    childquestionscount = cCnt;
                }

                // informations[3] = コメント情報
                this.Comment = informations[3];

                if (isN && !isCross) return;

                // informations[4] = グラフ情報
                //if ((ParentCollection.ParentOutput.ParentCollection.ParentReportset.Filetype & Reportset.FileType.Report) == Reportset.FileType.Report)
                //{
                string[] chartinformations = informations[4].Split('\t');
                if (chartinformations.Length != (isCross ? 6 : 4))
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { prompt, "(" + CHART_INFORMATION_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                if (chartinformations[0].Equals("0"))
                {
                    chartinformation = new ChartInformation();
                }
                else
                {
                    string[] gradsetting = chartinformations[2].Split(' ');
                    if (gradsetting.Length != 2)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { prompt, "(" + CHART_GRADIENT_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    chartinformation = new ChartInformation(
                            chartinformations[0], chartinformations[1], gradsetting[0], gradsetting[1]
                          , (questioninformation.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent);

                    // ↓仕様確認の上変更するかも
                    if (isCross)
                    {
                        int idx = 0;
                        if (int.TryParse(chartinformations[3], out idx))
                        {
                            chartinformation.InteriorColorIndex = idx;
                        }
                        gradsetting = chartinformations[4].Split(' ');
                        if (gradsetting.Length == 2 && int.TryParse(gradsetting[0], out idx))
                        {
                            chartinformation.InteriorGradientStyle = (MsoGradientStyle)idx;
                        }
                        if ((this.ParentOutput as Outputs.OutputGT).Orientation == TableOrientation.Landscape)
                        {
                            switch (questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA))
                            {
                                case Tabulation.QuestionType.SA:
                                    switch (chartinformation.ChartType)
                                    {
                                        case XlChartType.xlPie:   // 円
                                        case XlChartType.xlColumnClustered:   // 縦棒
                                        case XlChartType.xlBarStacked100: // 横帯
                                            break;
                                        default:
                                            chartinformation.ChartType = (XlChartType)0;
                                            break;
                                    }
                                    break;
                                case Tabulation.QuestionType.MA:
                                    if (chartinformation.ChartType == XlChartType.xlColumnClustered)  // 縦棒
                                    {
                                        break;
                                    }
                                    chartinformation.ChartType = (XlChartType)0;
                                    break;
                                default:
                                    chartinformation.ChartType = (XlChartType)0;
                                    break;
                            }
                        }
                        else    // 縦％表
                        {
                            switch (questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA))
                            {
                                case Tabulation.QuestionType.SA:
                                    switch (chartinformation.ChartType)
                                    {
                                        case XlChartType.xlPie:   // 円
                                        case XlChartType.xlBarClustered:  // 横棒
                                        case XlChartType.xlColumnStacked100:  // 縦帯
                                            break;
                                        default:
                                            chartinformation.ChartType = (XlChartType)0;
                                            break;
                                    }
                                    break;
                                case Tabulation.QuestionType.MA:
                                    if (chartinformation.ChartType == XlChartType.xlBarClustered)  // 横棒
                                    {
                                        break;
                                    }
                                    chartinformation.ChartType = (XlChartType)0;
                                    break;
                                default:
                                    chartinformation.ChartType = (XlChartType)0;
                                    break;
                            }
                        }
                    }
                    else    // GT
                    {
                        if ((questioninformation.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
                        {
                            // マトリクス
                            switch (questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.N))
                            {
                                case Tabulation.QuestionType.SA:
                                    if ((questioninformation.QuestionType & Tabulation.QuestionType.Rank) == Tabulation.QuestionType.Rank)
                                    {
                                        // 順位回答
                                        switch (chartinformation.ChartType)
                                        {
                                            case XlChartType.xlColumnStacked:  // 縦棒積上
                                            case XlChartType.xlBarStacked: // 横棒積上
                                                break;
                                            default:
                                                chartinformation.ChartType = (XlChartType)0;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        // SAマト
                                        switch (chartinformation.ChartType)
                                        {
                                            case XlChartType.xlLine:    // 折れ線
                                            case XlChartType.xlColumnStacked100:  // 縦帯
                                            case XlChartType.xlBarStacked100: // 横帯
                                            case XlChartType.xlColumnClustered: // QCM縦棒 //Qc4 change
                                            case XlChartType.xlBarClustered:    // QCM横棒 //Qc4 change
                                                break;
                                            default:
                                                if ((chartinformation.ChartType & XlChartType.QCM) == XlChartType.QCM)
                                                {
                                                    switch (chartinformation.ChartType & ~XlChartType.QCM)
                                                    {
                                                        case XlChartType.xlPie: // QCM円
                                                        case XlChartType.xlColumnStacked100:    // QCM縦帯
                                                        case XlChartType.xlBarStacked100:   // QCM横帯
                                                        case XlChartType.xlColumnClustered: // QCM縦棒
                                                        case XlChartType.xlBarClustered:    // QCM横棒
                                                            break;
                                                        default:
                                                            chartinformation.ChartType = (XlChartType)0;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    chartinformation.ChartType = (XlChartType)0;
                                                }
                                                break;
                                        }
                                    }
                                    break;
                                case Tabulation.QuestionType.MA:
                                    // MAマト
                                    switch (chartinformation.ChartType)
                                    {
                                        case XlChartType.xlLine:    // 折れ線
                                        case XlChartType.xlColumnClustered: // 縦棒
                                        case XlChartType.xlBarClustered:    // 横棒
                                            break;
                                        default:
                                            if ((chartinformation.ChartType & XlChartType.QCM) == XlChartType.QCM)
                                            {
                                                switch (chartinformation.ChartType & ~XlChartType.QCM)
                                                {
                                                    case XlChartType.xlColumnClustered: // QCM縦棒
                                                    case XlChartType.xlBarClustered:    // QCM横棒
                                                        break;
                                                    default:
                                                        chartinformation.ChartType = (XlChartType)0;
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                chartinformation.ChartType = (XlChartType)0;
                                            }
                                            break;
                                    }
                                    break;
                                case Tabulation.QuestionType.N:
                                    if ((questioninformation.QuestionType & Tabulation.QuestionType.Ratio) == Tabulation.QuestionType.Ratio)
                                    {
                                        // 割合回答
                                        if ((chartinformation.ChartType & XlChartType.RAT) == XlChartType.RAT)
                                        {
                                            switch (chartinformation.ChartType & ~XlChartType.RAT)
                                            {
                                                case XlChartType.xlPie:   // 円
                                                case XlChartType.xlColumnClustered:   // 縦棒
                                                case XlChartType.xlBarClustered:  // 横棒
                                                    break;
                                                default:
                                                    chartinformation.ChartType = (XlChartType)0;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            chartinformation.ChartType = (XlChartType)0;
                                        }
                                    }
                                    else
                                    {
                                        chartinformation.ChartType = (XlChartType)0;
                                    }
                                    break;
                                default:
                                    chartinformation.ChartType = (XlChartType)0;
                                    break;
                            }
                        }
                        else
                        {
                            // 通常質問
                            switch (questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA))
                            {
                                case Tabulation.QuestionType.SA:
                                    switch (chartinformation.ChartType)
                                    {
                                        case XlChartType.xlPie: // 円
                                        case XlChartType.xlColumnStacked100:    // 縦帯
                                        case XlChartType.xlBarStacked100:   // 横帯
                                        case XlChartType.xlColumnClustered: // 縦棒
                                        case XlChartType.xlBarClustered:    // 横棒
                                            break;
                                        default:
                                            chartinformation.ChartType = (XlChartType)0;
                                            break;
                                    }
                                    break;
                                case Tabulation.QuestionType.MA:
                                    switch (chartinformation.ChartType)
                                    {
                                        case XlChartType.xlColumnClustered: // 縦棒
                                        case XlChartType.xlBarClustered:    // 横棒

                                        case XlChartType.xlPie: // 円 #OutputFormatting - Because of question type change for subtotal
                                        case XlChartType.xlColumnStacked100:    // 縦帯 #OutputFormatting - Because of question type change for subtotal
                                        case XlChartType.xlBarStacked100:   // 横帯 #OutputFormatting - Because of question type change for subtotal
                                            break;
                                        default:
                                            chartinformation.ChartType = (XlChartType)0;
                                            break;
                                    }
                                    break;
                                default:
                                    chartinformation.ChartType = (XlChartType)0;
                                    break;
                            }
                        }

                        /*
                        if ((questioninformation.QuestionType & Tabulation.QuestionType.SA) == Tabulation.QuestionType.SA)
                        {
                            if ((int)(questioninformation.QuestionType & Tabulation.QuestionType.Rank) == 0)
                            {
                                switch (chartinformation.ChartType)
                                {
                                    case XlChartType.xlPie:   // 円
                                    case XlChartType.xlColumnClustered:   // 縦棒
                                    case XlChartType.xlBarClustered:  // 横棒
                                    case XlChartType.xlColumnStacked100:  // 縦帯
                                    case XlChartType.xlBarStacked100: // 横帯
                                        break;
                                    default:
                                        chartinformation.ChartType = (XlChartType)0;
                                        break;
                                }
                            }
                            else    // 順位回答
                            {
                                switch (chartinformation.ChartType)
                                {
                                    case XlChartType.xlColumnStacked:  // 縦棒積上
                                    case XlChartType.xlBarStacked: // 横棒積上
                                    case XlChartType.xlColumnStacked100:  // 縦帯
                                    case XlChartType.xlBarStacked100: // 横帯
                                        break;
                                    default:
                                        chartinformation.ChartType = (XlChartType)0;
                                        break;
                                }
                            }
                        }
                        else if ((questioninformation.QuestionType & Tabulation.QuestionType.MA) == Tabulation.QuestionType.MA)
                        {
                            switch (chartinformation.ChartType)
                            {
                                case XlChartType.xlColumnClustered:   // 縦棒
                                case XlChartType.xlBarClustered:  // 横棒
                                    break;
                                default:
                                    chartinformation.ChartType = (XlChartType)0;
                                    break;
                            }
                        }
                        else    // 割合回答
                        {
                            switch (chartinformation.ChartType)
                            {
                                case XlChartType.xlPie:   // 円
                                case XlChartType.xlColumnClustered:   // 縦棒
                                case XlChartType.xlBarClustered:  // 横棒
                                case XlChartType.xlColumnStacked100:  // 縦帯
                                case XlChartType.xlBarStacked100: // 横帯
                                    break;
                                default:
                                    chartinformation.ChartType = (XlChartType)0;
                                    break;
                            }
                        }
                        */
                    }
                    //}

                    int upper = int.Parse(GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportHidePieChartLabelDescriptionPercentUpperLimitIndex));
                    if (!int.TryParse(chartinformations[chartinformations.GetUpperBound(0)], out hidechartdescriptionmaxpercent)
                        || hidechartdescriptionmaxpercent < 0 || hidechartdescriptionmaxpercent > upper)
                    {
                        hidechartdescriptionmaxpercent = -1;
                    }
                }

                if (isRatio || isN) return;

                // informations[5] = 集計対象選択肢情報
                string[] secinformations = informations[5].Split('\t');
                if (secinformations.Length < 1)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { prompt, "(" + MAIN_ITEM_SECTORS_INFORMATION_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                int sectorscount = 0;
                if (!int.TryParse(secinformations[0], out sectorscount) || sectorscount < 1 || secinformations.GetUpperBound(0) != sectorscount)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { prompt, "(" + MAIN_ITEM_SECTORS_COUNT_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                sectorinformations = new SectorInformation[sectorscount];
                for (int i = 0; i < sectorscount; ++i)
                {
                    string[] tmp = secinformations[i + 1].Split('\v');  // ウエイト値と並べ替えフラグからなる一時配列
                    // 正常なときだけ処理 (異常値は救済)
                    if (tmp.Length == 2)
                    {
                        double wt = 0.0;
                        if (double.TryParse(tmp[0], out wt))
                        {
                            tmp[0] = wt.ToString();
                        }
                        else
                        {
                            tmp[0] = null;
                        }
                        bool unsort = tmp[0] == "1";
                        sectorinformations[i] = new SectorInformation(tmp[0], unsort);
                    }
                    else
                    {
                        sectorinformations[i] = new SectorInformation(null, false);
                    }
                }

                if (!isCross) return;
                // informations[6] = 折れ線グラフ行情報
                linechartrowInformation = informations[6];
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            GTTable(Tables tables, string tsvpath, bool isCross)
                : base(tables, tsvpath, true)
            {
                GetGTTableInformation(isCross);
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            GTTable(Tables tables, string tsvpath)
                : base(tables, tsvpath, true)
            {
                GetGTTableInformation(false);
            }

            /// <summary>
            /// 分類アイテムの簡易情報を保持したKeyItemInformationクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public KeyItemInformation KeyItem
            {
                get
                {
                    return keyiteminformation;
                }
            }

            /// <summary>
            /// 集計アイテムの簡易情報を保持したQuestionInformationクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public QuestionInformation Question
            {
                get
                {
                    return questioninformation;
                }
            }

            /// <summary>
            /// 項目間検定の種類を表すSignificanceTestCode列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public SignificanceTestCode SignificancetestCode
            {
                get
                {
                    return significancetestcode;
                }
            }

            /// <summary>
            /// 集計対象アイテムの子質問数を返す読み取り専用プロパティ<br />
            /// マトリクス以外では0
            /// </summary>
            public int ChildQuestionsCount
            {
                get
                {
                    return childquestionscount;
                }
            }

            /// <summary>
            /// グラフの簡易情報を保持したChartInformationクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public ChartInformation Chart
            {
                get
                {
                    return chartinformation;
                }
            }

            /// <summary>
            /// 円グラフの出力時に、選択肢名を非表示にする最大パーセンテージを、-1～50の整数で返す読み取り専用プロパティ<br />
            /// 既定値-1
            /// </summary>
            public int HideChartDescriptionMaxPercent
            {
                get
                {
                    return hidechartdescriptionmaxpercent;
                }
            }

            /// <summary>
            /// 集計対象質問の選択肢の簡易情報を保持したSectorInformationクラスのインスタンスへの参照を返すメソッド
            /// </summary>
            /// <param name="index">選択肢のインデックス</param>
            /// <returns>インデックスが示す選択肢の簡易情報を保持したSectorInformationクラスのインスタンスへの参照</returns>
            public SectorInformation Sector(int index)
            {
                if (index >= sectorinformations.GetLowerBound(0) && index <= sectorinformations.GetUpperBound(0))
                {
                    return sectorinformations[index];
                }
                else
                {
                    return null;
                }
            }

            /// <summary>
            /// 集計対象質問の選択肢数を返す読み取り専用プロパティ
            /// </summary>
            public int SectorsCount
            {
                get
                {
                    if (sectorinformations == null) return 0;
                    return sectorinformations.Length;
                }
            }

            /// <summary>
            /// 集計表のセルのランク情報を返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータのランク情報</returns>
            public int Rank(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return 0;
                return datawithmarking[RowIndex][ColumnIndex].Rank;
            }

            /// <summary>
            /// [性能対策]集計表のセルのランク情報を２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public int[,] RankByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                int[,] res = new int[RowIndexTo + 1, ColumnIndexTo + 1];
                for (int r = RowIndexFrom; r <= RowIndexTo; ++r)
                {
                    for (int c = ColumnIndexFrom; c <= ColumnIndexTo; ++c)
                    {
                        res[r, c] = Rank(r, c);
                    }
                }
                return res;
            }
        }

        /// <summary>
        /// クロス表の集計表を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("8D22689E-D742-4A63-B4B6-8BF52E24B644")]
        public class CrossTable : GTTable, ICrossTable
        {
            private AxesGroupInformation axesgroups = null;
            private ArrayList linechartrows = null;
            private delegate bool BooleanByMatrix(int r, int c);

            private bool[,] ByMatrixForBoolean(BooleanByMatrix func, int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                bool[,] res = new bool[RowIndexTo + 1, ColumnIndexTo + 1];
                for (int r = RowIndexFrom; r <= RowIndexTo; ++r)
                {
                    for (int c = ColumnIndexFrom; c <= ColumnIndexTo; ++c)
                    {
                        res[r, c] = func(r, c);
                    }
                }
                return res;
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            CrossTable(Tables tables, string tsvpath)
                : base(tables, tsvpath, true)
            {
                if (string.IsNullOrWhiteSpace(axesInformation))
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { CROSS_PROMPT, "(" + AXES_ITEM_INFORMATION_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                string[] axesgroupinformations = axesInformation.Split('\t');
                if (axesgroupinformations.Length < 2)
                {
                    throw new QCWebException(Constants.INSUFFICIENT_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { CROSS_PROMPT, "(" + AXES_ITEM_INFORMATION_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                int gCnt = 0;   // 集計軸グループ数
                if (!int.TryParse(axesgroupinformations[0], out gCnt))
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { CROSS_PROMPT, "(" + AXES_GROUPS_COUNT_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                if (gCnt != axesgroupinformations.GetUpperBound(0))
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { CROSS_PROMPT, "(" + AXES_GROUPS_COUNT_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                axesgroups = new AxesGroupInformation();
                for (int i = 1; i < axesgroupinformations.Length; ++i)
                {
                    string[] axesinformation = axesgroupinformations[i].Split('\v');
                    if (axesinformation.Length < 3 || axesinformation.Length > 4)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { CROSS_PROMPT, "(" + AXES_ITEM_INFORMATION_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    int aCnt = 0;   // グループ内の集計軸数
                    if (!int.TryParse(axesinformation[0], out aCnt))
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { CROSS_PROMPT, "(" + AXES_COUNT_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    if (aCnt != axesinformation.GetUpperBound(0) - 1)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { CROSS_PROMPT, "(" + AXES_COUNT_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    AxesInformation axes = axesgroups.Add();
                    axes.ShowTotal = Convert.ToBoolean(axesinformation[1]);
                    for (int j = 2; j < axesinformation.Length; ++j)
                    {
                        int sCnt = 0;   // 集計軸の選択肢数
                        if (!int.TryParse(axesinformation[j], out sCnt))
                        {
                            throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { CROSS_PROMPT, "(" + AXIS_ITEM_SECTORS_COUNT_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                        }
                        if (axes.Add(sCnt) == null)
                        {
                            throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { CROSS_PROMPT, "(" + AXIS_ITEM_SECTORS_COUNT_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                        }
                    }
                }
                linechartrows = new ArrayList();
                if (string.IsNullOrWhiteSpace(linechartrowInformation)) return;
                string[] strRows = linechartrowInformation.Split(',');
                for (int i = 0; i < strRows.Length; ++i)
                {
                    int r = 0;
                    if (!int.TryParse(strRows[i], out r) || r < 1)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { CROSS_PROMPT, "(" + LINE_CHART_ROWS_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    if (!linechartrows.Contains(r)) linechartrows.Add(r);
                }
                linechartrows.Sort();
            }

            /// <summary>
            /// 集計軸グループコレクションへの参照を返す読み取り専用プロパティ
            /// </summary>
            public AxesGroupInformation AxesGroups
            {
                get
                {
                    return axesgroups;
                }
            }

            /// <summary>
            /// 折れ線グラフとする行のインデックス (折れ線設定可能な行の1から始まるインデックス)をまとめたArrayListクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            [ComVisible(false)]
            public ArrayList LineChartRows
            {
                get
                {
                    return linechartrows;
                }
            }

            /// <summary>
            /// 折れ線グラフとする行のインデックス (折れ線設定可能な行の1から始まるインデックス)からなる一次元配列を返す読み取り専用プロパティ
            /// </summary>
            public int[] LineChartRowsArray
            {
                get
                {
                    if (linechartrows == null) return null;
                    int[] res = new int[linechartrows.Count];
                    for (int i = 0; i < linechartrows.Count; ++i)
                    {
                        res[i] = (int)linechartrows[i];
                    }
                    return res;
                }
            }

            /// <summary>
            /// <paramref name="Index"/>に指定したインデックスが、折れ線グラフとする行のインデックス (折れ線設定可能な行の1から始まるインデックス)に含まれるかどうかを返すメソッド
            /// </summary>
            /// <param name="Index">調べる行インデックス</param>
            /// <returns>存在する場合true、存在しない場合false</returns>
            public bool ExistLineChartIndex(int Index)
            {
                if (linechartrows == null) return false;
                return linechartrows.BinarySearch(Index) >= 0;
            }

            /// <summary>
            /// 集計表のセルの％値が全体との差の色付けマーキングで水準1で高いかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータが全体との差の色付けマーキングで水準1で高い場合true</returns>
            public bool ColoringLevel1High(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return false;
                return datawithmarking[RowIndex][ColumnIndex].ColoringLevel1High;
            }

            /// <summary>
            /// [性能対策]集計表のセルの％値が全体との差の色付けマーキングで水準1で高いかどうかを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public bool[,] ColoringLevel1HighByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForBoolean((int r, int c) => { return ColoringLevel1High(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルの％値が全体との差の色付けマーキングで水準2で高いかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータが全体との差の色付けマーキングで水準2で高い場合true</returns>
            public bool ColoringLevel2High(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return false;
                return datawithmarking[RowIndex][ColumnIndex].ColoringLevel2High;
            }

            /// <summary>
            /// [性能対策]集計表のセルの％値が全体との差の色付けマーキングで水準2で高いかどうかを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public bool[,] ColoringLevel2HighByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForBoolean((int r, int c) => { return ColoringLevel2High(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルの％値が全体との差の色付けマーキングで水準1で低いかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータが全体との差の色付けマーキングで水準1で低い場合true</returns>
            public bool ColoringLevel1Low(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return false;
                return datawithmarking[RowIndex][ColumnIndex].ColoringLevel1Low;
            }

            /// <summary>
            /// [性能対策]集計表のセルの％値が全体との差の色付けマーキングで水準1で低いかどうかを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public bool[,] ColoringLevel1LowByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForBoolean((int r, int c) => { return ColoringLevel1Low(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルの％値が全体との差の色付けマーキングで水準2で低いかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータが全体との差の色付けマーキングで水準2で低い場合true</returns>
            public bool ColoringLevel2Low(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return false;
                return datawithmarking[RowIndex][ColumnIndex].ColoringLevel2Low;
            }

            /// <summary>
            /// [性能対策]集計表のセルの％値が全体との差の色付けマーキングで水準2で低いかどうかを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public bool[,] ColoringLevel2LowByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForBoolean((int r, int c) => { return ColoringLevel2Low(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルの昇降分析マーキング情報を返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータの昇降分析マーキング情報</returns>
            public Tabulation.DataMarking AscendingMarking(int RowIndex, int ColumnIndex)
            {
                Tabulation.DataMarking res = DataMarking(RowIndex, ColumnIndex);
                if (res == (Tabulation.DataMarking)0) return res;
                return datawithmarking[RowIndex][ColumnIndex].AscendingMarking;
            }

            /// <summary>
            /// 集計表のセルが昇降分析マーキングの矢筈または矢尻にあたるかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <param name="reverseSide">逆側の昇降分析マーキングの値(DataMarking列挙型) (戻り値)</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータの昇降分析マーキング情報が、矢筈または矢尻を表すかどうか</returns>
            public bool IsArrowEnd(int RowIndex, int ColumnIndex, out Tabulation.DataMarking reverseSide)
            {
                Tabulation.DataMarking defDataMarking = (Tabulation.DataMarking)0;
                reverseSide = defDataMarking;
                Tabulation.DataMarking tmp = DataMarking(RowIndex, ColumnIndex);
                if (tmp == defDataMarking) return false;
                return datawithmarking[RowIndex][ColumnIndex].IsArrowEnd(out reverseSide);
            }

            /// <summary>
            /// 集計表のセルが昇降分析マーキングの矢柄にあたるかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータの昇降分析マーキング情報が、矢柄を表すかどうか</returns>
            public bool IsArrowShaft(int RowIndex, int ColumnIndex)
            {
                Tabulation.DataMarking defDataMarking = (Tabulation.DataMarking)0;
                Tabulation.DataMarking tmp = DataMarking(RowIndex, ColumnIndex);
                if (tmp == defDataMarking) return false;
                return datawithmarking[RowIndex][ColumnIndex].IsArrowShaft;
            }

            /// <summary>
            /// 集計表のセルのデータが1％水準で有意に高いかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータが1％水準で有意に高い場合true</returns>
            public bool Significance1PercentHigh(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return false;
                return datawithmarking[RowIndex][ColumnIndex].Significance1PercentHigh;
            }

            /// <summary>
            /// [性能対策]集計表のセルのデータが1％水準で有意に高いかどうかを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public bool[,] Significance1PercentHighByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForBoolean((int r, int c) => { return Significance1PercentHigh(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルのデータが5％水準で有意に高いかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータが5％水準で有意に高い場合true</returns>
            public bool Significance5PercentHigh(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return false;
                return datawithmarking[RowIndex][ColumnIndex].Significance5PercentHigh;
            }

            /// <summary>
            /// [性能対策]集計表のセルのデータが5％水準で有意に高いかどうかを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public bool[,] Significance5PercentHighByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForBoolean((int r, int c) => { return Significance5PercentHigh(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルのデータが10％水準で有意に高いかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータが10％水準で有意に高い場合true</returns>
            public bool Significance10PercentHigh(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return false;
                return datawithmarking[RowIndex][ColumnIndex].Significance10PercentHigh;
            }

            /// <summary>
            /// [性能対策]集計表のセルのデータが10％水準で有意に高いかどうかを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public bool[,] Significance10PercentHighByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForBoolean((int r, int c) => { return Significance10PercentHigh(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルのデータが1％水準で有意に低いかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータが1％水準で有意に低い場合true</returns>
            public bool Significance1PercentLow(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return false;
                return datawithmarking[RowIndex][ColumnIndex].Significance1PercentLow;
            }

            /// <summary>
            /// [性能対策]集計表のセルのデータが1％水準で有意に低いかどうかを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public bool[,] Significance1PercentLowByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForBoolean((int r, int c) => { return Significance1PercentLow(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルのデータが5％水準で有意に低いかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータが5％水準で有意に低い場合true</returns>
            public bool Significance5PercentLow(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return false;
                return datawithmarking[RowIndex][ColumnIndex].Significance5PercentLow;
            }

            /// <summary>
            /// [性能対策]集計表のセルのデータが5％水準で有意に低いかどうかを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public bool[,] Significance5PercentLowByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForBoolean((int r, int c) => { return Significance5PercentLow(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルのデータが10％水準で有意に低いかどうかを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータが10％水準で有意に低い場合true</returns>
            public bool Significance10PercentLow(int RowIndex, int ColumnIndex)
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return false;
                return datawithmarking[RowIndex][ColumnIndex].Significance10PercentLow;
            }

            /// <summary>
            /// [性能対策]集計表のセルのデータが10％水準で有意に低いかどうかを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <returns>２次元配列</returns>
            public bool[,] Significance10PercentLowByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo)
            {
                return ByMatrixForBoolean((int r, int c) => { return Significance10PercentLow(r, c); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// 集計表のセルの全体との差の検定マークを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <param name="locale">ロケール</param>
            /// <returns>集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるデータの検定マーク</returns>
            public string SignificanceMark(int RowIndex, int ColumnIndex, string locale = "ja")
            {
                if (DataMarking(RowIndex, ColumnIndex) == (Tabulation.DataMarking)0) return null;
                return datawithmarking[RowIndex][ColumnIndex].SignificanceMark(locale);
            }

            /// <summary>
            /// [性能対策]集計表のセルの全体との差の検定マークを２次元配列で返すメソッド
            /// </summary>
            /// <param name="RowIndexFrom">行インデックス開始値</param>
            /// <param name="RowIndexTo">行インデックス終了値</param>
            /// <param name="ColumnIndexFrom">列インデックス開始値</param>
            /// <param name="ColumnIndexTo">列インデックス終了値</param>
            /// <param name="locale">ロケール</param>
            /// <returns>２次元配列</returns>
            public string[,] SignificanceMarkByMatrix(int RowIndexFrom, int RowIndexTo, int ColumnIndexFrom, int ColumnIndexTo, string locale = "ja")
            {
                return ByMatrixForString((int r, int c) => { return SignificanceMark(r, c, locale); }, RowIndexFrom, RowIndexTo, ColumnIndexFrom, ColumnIndexTo);
            }

            /// <summary>
            /// セット番号を取得する読み取り専用プロパティ (1シート1クロスのシナリオ出力時に有効)
            /// </summary>
            public int SetNo
            {
                get
                {
                    int setNo = 0;
                    if (parentQInfo != null)
                    {
                        string[] tmp = parentQInfo.Split(new char[] { '_' }, 2);
                        if (tmp.Length == 2)
                        {
                            if (!GlobalMethodClass.IsIntegerExpression(tmp[0], out setNo, false, false, false, false, false, false, false)
                                || setNo < 0)
                            {
                                setNo = 0;
                            }
                        }
                    }
                    return setNo;
                }
            }

            /// <summary>
            /// マトリクスの親質問番号を取得する読み取り専用プロパティ (1シート1クロスのシナリオ出力時に有効)
            /// </summary>
            public string ParentQNo
            {
                get
                {
                    string parentQNo = string.Empty;
                    if (parentQInfo != null)
                    {
                        string[] tmp = parentQInfo.Split(new char[] { '_' }, 2);
                        if (tmp.Length == 2)
                        {
                            parentQNo = tmp[1] + string.Empty;
                        }
                    }
                    return parentQNo;
                }
            }
        }

        /// <summary>
        /// FAリストの集計表を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("FE46ED9E-2C50-48A9-94E4-938C5CD3078A")]
        public class FAListTable : Table, IFAListTable
        {
            private static readonly int FAITEMSCOUNT_MAX = int.Parse(GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportFAListFAItemsCountLimitIndex));
            private static readonly int ADDEDITEMSCOUNT_MAX = int.Parse(GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportFAListAddedItemsCountLimitIndex));

            private KeyItemInformation keyiteminformation = null;
            private int faitemscount = 0;   // FAアイテム数
            private int addeditemscount = 0;  // 付加アイテム数
            private Macromill.QCWeb.Tabulation.QuestionType[] addeditemsQType = null;   // 付加アイテムの質問タイプ
            private string topitemname = null;

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            FAListTable(Tables tables, string tsvpath)
                : base(tables, tsvpath)
            {
                string[] informations = this.Information.Split('\f');   // FF区切りでザックリ分割
                if (informations.Length != 3)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { FALIST_PROMPT, "" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }

                // informations[0] = 分類アイテム情報
                string[] keyiteminformations = informations[0].Split('\t');
                if (keyiteminformations.Length != 4)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { FALIST_PROMPT, "(" + KEY_ITEM_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                if (!string.IsNullOrWhiteSpace(keyiteminformations[0]))
                {
                    keyiteminformation = new KeyItemInformation(keyiteminformations[0], keyiteminformations[1], keyiteminformations[2], keyiteminformations[3], true);
                }

                // informations[1] = アイテム数情報
                string[] itemscount = informations[1].Split('\t');
                if (itemscount.Length != 4)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { FALIST_PROMPT, "(" + ITEMS_COUNT_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                int fCnt = 0;   // FAアイテム数
                if (!int.TryParse(itemscount[0], out fCnt) || fCnt <= 0 || fCnt > FAITEMSCOUNT_MAX)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { FALIST_PROMPT, "(" + FA_ITEMS_COUNT_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                int aCnt = 0;   // 付加アイテム数
                if (!int.TryParse(itemscount[1], out aCnt) || aCnt < 0 || aCnt > ADDEDITEMSCOUNT_MAX)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { FALIST_PROMPT, "(" + ADDED_ITEMS_COUNT_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                if (aCnt > 0)
                {
                    if (itemscount[2] == null) itemscount[2] = string.Empty;
                    int len = itemscount[2].Length;
                    this.addeditemsQType = new Tabulation.QuestionType[aCnt];
                    for (int i = 0; i < aCnt; ++i)
                    {
                        int tmp = (int)Macromill.QCWeb.Tabulation.QuestionType.SA;
                        if (i < len)
                        {
                            if (int.TryParse(itemscount[2].Substring(i, 1), out tmp))
                            {
                                switch ((Macromill.QCWeb.Tabulation.QuestionType)tmp)
                                {
                                    case Macromill.QCWeb.Tabulation.QuestionType.SA:
                                    case Macromill.QCWeb.Tabulation.QuestionType.MA:
                                    case Macromill.QCWeb.Tabulation.QuestionType.FA:
                                    case Macromill.QCWeb.Tabulation.QuestionType.N:
                                        break;
                                    default:
                                        tmp = (int)Macromill.QCWeb.Tabulation.QuestionType.SA;
                                        break;
                                }
                            }
                        }
                        this.addeditemsQType[i] = (Tabulation.QuestionType)tmp;
                    }
                }
                if (string.IsNullOrWhiteSpace(itemscount[3]))
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { FALIST_PROMPT, "(" + FIRST_ITEM_NAME_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                this.faitemscount = fCnt;
                this.addeditemscount = aCnt;
                this.topitemname = itemscount[3];

                // informations[2] = コメント情報
                this.Comment = informations[2];
            }

            /// <summary>
            /// 分類アイテムの簡易情報を保持したKeyItemInformationクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public KeyItemInformation KeyItem
            {
                get
                {
                    return keyiteminformation;
                }
            }

            /// <summary>
            /// FAアイテム数を返す読み取り専用プロパティ
            /// </summary>
            public int FAItemsCount
            {
                get
                {
                    return faitemscount;
                }
            }

            /// <summary>
            /// 付加アイテム数を返す読み取り専用プロパティ
            /// </summary>
            public int AddedItemsCount
            {
                get
                {
                    return addeditemscount;
                }
            }

            /// <summary>
            /// 付加アイテムの質問タイプを表すQuestionType列挙型の値を返すメソッド
            /// </summary>
            /// <param name="index">付加アイテムの0ベースインデックス</param>
            /// <returns>インデックスが示す付加アイテムの簡易な質問タイプ</returns>
            public Tabulation.QuestionType AddedItemQType(int index)
            {
                Tabulation.QuestionType errRet = (Tabulation.QuestionType)0;
                if (addeditemsQType == null) return errRet;
                if (index >= 0 && index < addeditemsQType.Length)
                {
                    return addeditemsQType[index];
                }
                return errRet;
            }

            /// <summary>
            /// 先頭のFAアイテム名を返す読み取り専用プロパティ
            /// </summary>
            public string TopItemName
            {
                get
                {
                    return topitemname;
                }
            }

            /// <summary>
            /// FAアイテム数の上限値を返す読み取り専用プロパティ
            /// <note>静的な値だが、COMからの参照のためにインスタンスメンバ</note>
            /// </summary>
            public int FAItemsCountMax
            {
                get
                {
                    return FAITEMSCOUNT_MAX;
                }
            }

            /// <summary>
            /// 付加アイテム数の上限値を返す読み取り専用プロパティ
            /// <note>静的な値だが、COMからの参照のためにインスタンスメンバ</note>
            /// </summary>
            public int AddedItemsCountMax
            {
                get
                {
                    return ADDEDITEMSCOUNT_MAX;
                }
            }
        }

        /// <summary>
        /// チェックリストの集計表を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("AAED5936-90FB-498B-BB4D-4E9D84F355E1")]
        public class CheckListTable : Table, ICheckListTable
        {
            private QuestionInformation questioninformation = null;
            private int sectorscount = 0;
            private bool ischanged = false;
            private bool isnewitem = false;

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            CheckListTable(Tables tables, string tsvpath)
                : base(tables, tsvpath)
            {
                string[] informations = this.Information.Split('\f');   // FF区切りでザックリ分割
                if (informations.Length != 2)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                     , new string[] { CHECKLIST_PROMPT, "" }
                                     , GlobalsCommonConstant.LogLevel.FATAL, null);
                }

                // informations[0] = 集計対象アイテム情報
                string[] questioninformations = informations[0].Split('\t');
                if (questioninformations.Length != 4)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { CHECKLIST_PROMPT, "(" + MAIN_ITEM_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                // informations[1] = フラグ情報
                string[] flags = informations[1].Split('\t');
                if (flags.Length != 2)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { CHECKLIST_PROMPT, "(" + FLAG_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                questioninformation = new QuestionInformation(questioninformations[0], questioninformations[1], questioninformations[2], false, true);
                int sCnt = 0;
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0)
                {
                    if (!int.TryParse(questioninformations[3], out sCnt) || sCnt <= 0)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { CHECKLIST_PROMPT, "(" + MAIN_ITEM_SECTORS_COUNT_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                }
                sectorscount = sCnt;
                ischanged = flags[0].Equals("1");
                isnewitem = flags[1].Equals("1");
            }

            /// <summary>
            /// 集計アイテムの簡易情報を保持したQuestionInformationクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public QuestionInformation Question
            {
                get
                {
                    return questioninformation;
                }
            }

            /// <summary>
            /// 集計アイテムの選択肢数を返す読み取り専用プロパティ
            /// </summary>
            public int SectorsCount
            {
                get
                {
                    return sectorscount;
                }
            }

            /// <summary>
            /// 変更の有無を返す読み取り専用プロパティ
            /// </summary>
            public bool IsChanged
            {
                get
                {
                    return ischanged;
                }
            }

            /// <summary>
            /// 新アイテムかどうかを返す読み取り専用プロパティ
            /// </summary>
            public bool IsNewItem
            {
                get
                {
                    return isnewitem;
                }
            }
        }

        /// <summary>
        /// 調査票の質問情報を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("1937363A-5637-4609-BAC6-E3A22FA649B8")]
        public class QuestionnaireTable : Table, IQuestionnaireTable
        {
            private QuestionInformation2 questioninformation = null;
            private SectorInformation2[] sectorinformations = null;
            private QuestionInformation2[] childquestioninformation = null;

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            QuestionnaireTable(Tables tables, string tsvpath)
                : base(tables, tsvpath)
            {
                string[] informations = this.Information.Split('\f');   // FF区切りでザックリ分割
                if (informations.Length != 3)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { QUESTIONNAIRE_PROMPT, "" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }

                // informations[0] = 質問情報
                string[] questioninformations = informations[0].Split('\t');
                if (questioninformations.Length != 4)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { QUESTIONNAIRE_PROMPT, "(" + ITEM_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                string[] tmpBuf = questioninformations[0].Split(new char[] { '\v' }, 2);
                string itemName = tmpBuf[0];
                string qNo = tmpBuf.Length == 2 ? tmpBuf[1] : null;
                questioninformation = new QuestionInformation2(itemName, qNo, questioninformations[1], questioninformations[2], questioninformations[3], true);
                Tabulation.QuestionType qType = questioninformation.QuestionType;

                // informations[1] = 選択肢情報
                if ((int)(qType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0)
                {
                    string[] secinformations = informations[1].Split('\t');
                    if (secinformations.Length < 1)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { QUESTIONNAIRE_PROMPT, "(" + SECTORS_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    int sCnt = 0;
                    if (!int.TryParse(secinformations[0], out sCnt) || sCnt <= 0 || secinformations.GetUpperBound(0) != sCnt)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { QUESTIONNAIRE_PROMPT, "(" + SECTORS_COUNT_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    sectorinformations = new SectorInformation2[sCnt];
                    for (int i = 0; i < sCnt; ++i)
                    {
                        string[] secinfo = secinformations[i + 1].Split('\v');
                        int sNum = 0;
                        if (secinfo.Length != 3 || !int.TryParse(secinfo[0], out sNum) || sNum <= 0)
                        {
                            throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { QUESTIONNAIRE_PROMPT, "(" + SECTORS_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                        }
                        sectorinformations[i] = new SectorInformation2(sNum.ToString(), secinfo[1], secinfo[2], true);
                    }
                }

                // informations[2] = 子質問情報
                if ((qType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
                {
                    //qType = qType & ~Tabulation.QuestionType.MatrixParent | Tabulation.QuestionType.MatrixChild;
                    //string qTypeString = ((int)qType).ToString();
                    string[] childinformations = informations[2].Split('\t');
                    if (childinformations.Length < 1)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { QUESTIONNAIRE_PROMPT, "(" + CHILD_QUESTIONS_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    int cCnt = 0;
                    if (!int.TryParse(childinformations[0], out cCnt) || cCnt <= 0 || childinformations.GetUpperBound(0) != cCnt)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { QUESTIONNAIRE_PROMPT, "(" + CHILD_QUESTIONS_COUNT_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    childquestioninformation = new QuestionInformation2[cCnt];
                    for (int i = 0; i < cCnt; ++i)
                    {
                        string[] childinfo = childinformations[i + 1].Split('\v');
                        // if (childinfo.Length != 2 || string.IsNullOrWhiteSpace(childinfo[0]))
                        int intQType = 0;
                        if (childinfo.Length != 3 || string.IsNullOrWhiteSpace(childinfo[0]) || !int.TryParse(childinfo[1], out intQType))
                        {
                            throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { QUESTIONNAIRE_PROMPT, "(" + CHILD_QUESTIONS_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                        }
                        // childquestioninformation[i] = new QuestionInformation2(childinfo[0], null, childinfo[1], qTypeString, null, true);
                        childquestioninformation[i] = new QuestionInformation2(
                                        childinfo[0], null, childinfo[2], (Tabulation.QuestionType)intQType, null, true);
                    }
                }
            }

            /// <summary>
            /// 質問の簡易情報を保持したQuestionInformation2クラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public QuestionInformation2 Question
            {
                get
                {
                    return questioninformation;
                }
            }

            /// <summary>
            /// 選択肢の簡易情報を保持したSectorInformation2クラスのインスタンスへの参照を返すメソッド
            /// </summary>
            /// <param name="index">選択肢のインデックス</param>
            /// <returns>インデックスが示す選択肢の簡易情報を保持したSectorInformation2クラスのインスタンスへの参照</returns>
            public SectorInformation2 Sector(int index)
            {
                if (sectorinformations != null && index >= sectorinformations.GetLowerBound(0) && index <= sectorinformations.GetUpperBound(0))
                {
                    return sectorinformations[index];
                }
                else
                {
                    return null;
                }
            }

            /// <summary>
            /// 選択肢数を返す読み取り専用プロパティ
            /// </summary>
            public int SectorsCount
            {
                get
                {
                    if (sectorinformations == null) return 0;
                    return sectorinformations.Length;
                }
            }

            /// <summary>
            /// 子質問の簡易情報を保持したQuestionInformation2クラスのインスタンスへの参照を返すメソッド
            /// </summary>
            /// <param name="index">子質問のインデックス</param>
            /// <returns>インデックスが示す子質問の簡易情報を保持したQuestionInformation2クラスのインスタンスへの参照</returns>
            public QuestionInformation2 ChildQuestion(int index)
            {
                if (childquestioninformation != null && index >= childquestioninformation.GetLowerBound(0) && index <= childquestioninformation.GetUpperBound(0))
                {
                    return childquestioninformation[index];
                }
                else
                {
                    return null;
                }
            }

            /// <summary>
            /// 子質問数を返す読み取り専用プロパティ
            /// </summary>
            public int ChildQuestionSCount
            {
                get
                {
                    if (childquestioninformation == null) return 0;
                    return childquestioninformation.Length;
                }
            }
        }

        /// <summary>
        /// ローデータの質問情報を扱うクラス
        /// </summary>
        [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("ECC78D1A-601A-4D04-B29C-FECD93A6BE1E")]
        public class RawDataTable : Table, IRawDataTable
        {
            // private List<Question.Questions.Question> questions = null;
            private OutputDataType datatype = OutputDataType.Code;
            private LayoutOrientation orientation = LayoutOrientation.Landscape;
            private List<ResearchInformation> researchinformations = null;
            private List<CellInformation> cellinformations = null;
            private List<RuleInformation> ruleinformations = null;
            private bool[] divisiblePoint = null;
            private Macromill.QCWeb.Question.QCAnswerType[] columnQCAnsType = null;
            private string[,] layoutValue = null;
            private string[,] researchInformationCaptionArray = null;
            private string[] cellInformationCaptionArray = null;
            private string cellInformationRowCaption = null;
            private string[] ruleInformationCaptionArray = null;
            private string ruleInformationRowCaption = null;

            private struct DividedIndexes
            {
                private int startIndex;
                private int endIndex;

                internal DividedIndexes(int start, int end)
                {
                    startIndex = start;
                    endIndex = end;
                }

                /// <summary>
                /// 開始列インデックスを返す読み取り専用プロパティ
                /// </summary>
                public int StartIndex
                {
                    get
                    {
                        return startIndex;
                    }
                }

                /// <summary>
                /// 終了列インデックスを返す読み取り専用プロパティ
                /// </summary>
                public int EndIndex
                {
                    get
                    {
                        return endIndex;
                    }
                }
            }

            private List<DividedIndexes> dividedColumnsList = null;

            private void putColumnIndexes()
            {
                if (layoutValue == null) return;
                if (orientation != ReportRequest.LayoutOrientation.Landscape || datatype == OutputDataType.QC3) return;
                int shtIdx = 0;
                int idx = 0;
                int s = 0, e = 0;
                bool outputtedSheetRangeBuf = false;
                for (int r = 1; r < layoutValue.GetLength(0); ++r)
                {
                    int secCnt = 0;
                    int n = 1;
                    const int MAX_SECTORS_COUNT_PER_ROW = 200;
                    if (layoutValue[r, 3].Equals("SA") || layoutValue[r, 3].Equals("MA"))
                    {
                        const int MAX_SECTORS_COUNT = 600;
                        if (!int.TryParse(layoutValue[r, 4], out secCnt) || secCnt <= 0 || secCnt > MAX_SECTORS_COUNT)
                        {
                            throw new QCWebException(new Message(Macromill.QCWeb.Common.Constants.CommonMessageIndex.UnjustDataMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , layoutValue[r, 4]);
                        }
                        n = (secCnt - 1) / MAX_SECTORS_COUNT_PER_ROW + 1;
                    }
                    bool multiColumn = datatype != OutputDataType.Code && layoutValue[r, 3].Equals("MA");
                    if (multiColumn)
                    {
                        const string DELIMITER = " - ";
                        int endShtIdx = -1;
                        if (r == 1 || dividedColumnsList[shtIdx].EndIndex < idx)
                        {
                            if (r > 1) ++shtIdx;
                            s = 1 + (GlobalMethodClass.CInt(r > 1) & 1);
                            // シート番号の出力が必要
                            endShtIdx = shtIdx;
                        }
                        else if (outputtedSheetRangeBuf)
                        {
                            // シート番号の出力が必要
                            endShtIdx = shtIdx;
                        }
                        int startShtIdx = shtIdx;
                        List<string> rangeBuf = new List<string>();
                        while (secCnt > 0)
                        {
                            int d = dividedColumnsList[shtIdx].EndIndex - idx + 1;  // shtIdxのシートで消費できる列数
                            if (d > secCnt) d = secCnt; // shtIdxで消費する選択肢列数
                            if (d > 0)
                            {
                                e = s + d - 1;
                                rangeBuf.Add(s.ToString() + (d > 1 ? DELIMITER + e.ToString() : string.Empty));
                                secCnt -= d;
                                idx += d;
                                s = e + 1;
                            }
                            else
                            {
                                ++shtIdx;
                                s = 2;
                                // シート番号の出力が必要
                                endShtIdx = shtIdx;
                            }
                        }
                        if (endShtIdx >= 0)
                        {
                            outputtedSheetRangeBuf = startShtIdx < endShtIdx;
                            layoutValue[r, 0] = (startShtIdx + 1).ToString("000")
                                              + (outputtedSheetRangeBuf ? DELIMITER + (endShtIdx + 1).ToString("000") : string.Empty);
                        }
                        layoutValue[r, 1] = string.Join<string>(" / ", rangeBuf);
                    }
                    else
                    {
                        string shtIdxBuf = string.Empty;
                        //if (r == 1 || dividedColumnsList[shtIdx].EndIndex < idx)
                        //{
                        //    shtIdxBuf = ((r == 1 ? shtIdx : ++shtIdx) + 1).ToString("000");
                        //    s = 1 + (GlobalMethodClass.CInt(r > 1) & 1);
                        //}
                        if (r == 1 || dividedColumnsList[shtIdx].EndIndex < idx)
                        {
                            if (r > 1) ++shtIdx;
                            s = 1 + (GlobalMethodClass.CInt(r > 1) & 1);
                            shtIdxBuf = (shtIdx + 1).ToString("000");
                        }
                        else if (outputtedSheetRangeBuf)
                        {
                            shtIdxBuf = (shtIdx + 1).ToString("000");
                            outputtedSheetRangeBuf = false;
                        }
                        e = s;
                        layoutValue[r, 0] = shtIdxBuf;
                        layoutValue[r, 1] = s.ToString();
                        ++idx;
                        s = e + 1;
                    }
                    r += n - 1;

                    //for (int i = 1; i <= n; ++i)
                    //{
                    //    if (multiColumn)
                    //    {
                    //        if (i < n)
                    //        {
                    //            d = MAX_SECTORS_COUNT_PER_ROW - 1;
                    //        }
                    //        else
                    //        {
                    //            d = (secCnt - 1) % MAX_SECTORS_COUNT_PER_ROW;
                    //        }
                    //    }
                    //    if (i > 1)
                    //    {
                    //        if (++r > layoutValue.GetUpperBound(0))
                    //        {
                    //            throw new QCWebException(new Message(Macromill.QCWeb.Common.Constants.CommonMessageIndex.UnjustDataMessageIndex)
                    //                                   , GlobalsCommonConstant.LogLevel.FATAL
                    //                                   , layoutValue[r, 4]);                               
                    //        }
                    //    }
                    //    if (i == 1 || multiColumn)
                    //    {
                    //        string shtIdxBuf = string.Empty;
                    //        if (r == 1 || dividedColumnsList[shtIdx].EndIndex < idx + d)
                    //        {
                    //            shtIdxBuf = ((shtIdx += GlobalMethodClass.CInt(r > 1) & 1) + 1).ToString("000");
                    //            s = 1 + (GlobalMethodClass.CInt(r > 1) & 1);
                    //        }
                    //        e = s + d;
                    //        layoutValue[r, 0] = shtIdxBuf;
                    //        layoutValue[r, 1] = s.ToString() + (d == 0 ? string.Empty : " - " + e.ToString());
                    //        idx += d + 1;
                    //        s = e + 1;
                    //    }
                    //}

                    //int d = 0;
                    //if (datatype != OutputDataType.Code && layoutValue[r, 3].Equals("MA"))
                    //{
                    //    if (!int.TryParse(layoutValue[r, 4], out d) || d <= 0)
                    //    {
                    //        throw new QCWebException(new Message(Macromill.QCWeb.Common.Constants.CommonMessageIndex.UnjustDataMessageIndex)
                    //                               , GlobalsCommonConstant.LogLevel.FATAL
                    //                               , layoutValue[r, 4]);
                    //    }
                    //    --d;
                    //}
                    //string shtIdxBuf = string.Empty;
                    //if (r == 1 || dividedColumnsList[shtIdx].EndIndex < idx + d)
                    //{
                    //    shtIdxBuf = ((r == 1 ? shtIdx : ++shtIdx) + 1).ToString("000");
                    //    s = 1;
                    //}
                    //e = s + d;
                    //layoutValue[r, 0] = shtIdxBuf;
                    //layoutValue[r, 1] = s.ToString() + (d == 0 ? "" : " - " + e.ToString());
                    //idx += d + 1;
                    //s = e + 1;
                }
            }

            /// <summary>
            /// 列数による分割処理を行うメソッド
            /// </summary>
            /// <param name="ColumnsCountPerSheet">分割する1シートあたりの列数</param>
            public void DivideColumns(int ColumnsCountPerSheet)
            {
                if (GetTableValueColumnIndexMaximum == -1) return;
                dividedColumnsList = new List<DividedIndexes>();
                int s = 0;
                if (divisiblePoint == null || ColumnsCountPerSheet < 2)
                {
                    int e = GetTableValueColumnIndexMaximum;
                    dividedColumnsList.Add(new DividedIndexes(s, e));
                }
                else
                {
                    while (s <= GetTableValueColumnIndexMaximum)
                    {
                        int nextS = s + ColumnsCountPerSheet + (dividedColumnsList.Count == 0 ? 0 : -1);
                        if (nextS <= GetTableValueColumnIndexMaximum)
                        {
                            // 最後まで分割可能ポイントがなければ、そのままの位置で分割させる
                            for (int i = nextS; i > s; --i)
                            {
                                if (divisiblePoint[i])
                                {
                                    nextS = i;
                                    break;
                                }
                            }
                            int e = nextS - 1;
                            dividedColumnsList.Add(new DividedIndexes(s, e));
                            s = nextS;
                        }
                        else
                        {
                            int e = GetTableValueColumnIndexMaximum;
                            dividedColumnsList.Add(new DividedIndexes(s, e));
                            break;
                        }
                    }
                }
                putColumnIndexes();
            }

            private List<DividedIndexes> dividedRowsList = null;
            /// <summary>
            /// 行数によるレイアウトの分割処理を行うメソッド
            /// </summary>
            /// <param name="RowsCountPerSheet">RowsCountPerSheet</param>
            public void DivideRows(int RowsCountPerSheet)
            {
                // if (datatype == OutputDataType.QC3) return;
                if (layoutValue == null || layoutValue.GetUpperBound(1) < 2 || RowsCountPerSheet < 2) return;
                dividedRowsList = new List<DividedIndexes>();
                int s = 0;
                if (datatype == OutputDataType.QC3)
                {
                    int e = layoutValue.GetUpperBound(0);
                    dividedRowsList.Add(new DividedIndexes(s, e));
                    return;
                }
                while (s < layoutValue.GetLength(0))
                {
                    int nextS = s + RowsCountPerSheet + (dividedRowsList.Count == 0 ? 0 : -1);
                    if (nextS < layoutValue.GetLength(0))
                    {
                        // 最後まで分割可能でなければ、そのままの位置で分割させる
                        for (int i = nextS; i > s; --i)
                        {
                            if (layoutValue[i, 2].Length > 0)
                            {
                                nextS = i;
                                break;
                            }
                        }
                        int e = nextS - 1;
                        dividedRowsList.Add(new DividedIndexes(s, e));
                        s = nextS;
                    }
                    else
                    {
                        int e = layoutValue.GetUpperBound(0);
                        dividedRowsList.Add(new DividedIndexes(s, e));
                        break;
                    }
                }
            }

#if FOR_UNIT_TEST
            public
#else
            internal
#endif
            RawDataTable(Tables tables, string tsvpath)
                : base(tables, tsvpath)
            {
                string[] informations = this.Information.Split('\f');   // FF区切りでザックリ分割
                if (informations.Length < 4)
                {
                    throw new QCWebException(Constants.INSUFFICIENT_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                int dType = 0;
                if (!int.TryParse(informations[0], out dType))
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "(" + DATA_TYPE_CODE_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                datatype = (OutputDataType)dType;
                switch (datatype)
                {
                    case OutputDataType.Flag:
                    case OutputDataType.Decode:
                    case OutputDataType.QC3:
                        break;
                    default:
                        datatype = OutputDataType.Code; // 既定値とする
                        break;
                }
                if (informations.Length != (datatype == OutputDataType.QC3 ? 6 : 4))
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                // 列情報
                string columnInfoBuf = informations[1];
                int l = columnInfoBuf.Length;
                int clmsCount = GetTableValueColumnIndexMaximum + 1;
                if (l < clmsCount)
                {
                    columnInfoBuf += new string('1', clmsCount - l);
                }
                else if (l > clmsCount)
                {
                    columnInfoBuf = columnInfoBuf.Substring(0, clmsCount);
                }
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\D");
                columnInfoBuf = regex.Replace(columnInfoBuf, "1");
                int[] columnInfo = Array.ConvertAll<char, int>(columnInfoBuf.ToCharArray(), x => int.Parse(x.ToString(), System.Globalization.NumberStyles.HexNumber));
                divisiblePoint = Array.ConvertAll<int, bool>(columnInfo, x => (x & 1) == 1);
                columnQCAnsType = Array.ConvertAll<int, Macromill.QCWeb.Question.QCAnswerType>(columnInfo, x => (Macromill.QCWeb.Question.QCAnswerType)(x >> 1));
                if (datatype == OutputDataType.QC3)
                {
                    // informations[3] = 調査情報
                    string tmpBuf = System.Text.RegularExpressions.Regex.Unescape(informations[3]);
                    string[] tmpRowBuf = tmpBuf.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    if (tmpRowBuf.Length != 5)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "(" + RESEARCH_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    // 調査名(names[0]は「RNAME」)
                    string[] names = tmpRowBuf[0].Split('\t');
                    int researchCount = names.GetUpperBound(0);
                    if (researchCount < 1)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "(" + RESEARCH_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    // 調査ID(ids[0]は「RID」)
                    string[] ids = tmpRowBuf[1].Split('\t');
                    if (ids.GetUpperBound(0) != researchCount)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "(" + RESEARCH_ID_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    // 調査方法(methods[0]は「RMETHOD」)
                    string[] methods = tmpRowBuf[2].Split('\t');
                    if (methods.GetUpperBound(0) != researchCount)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "(" + RESEARCH_METHOD_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    // 商品種別(services[0]は「RSERVICE」)
                    string[] services = tmpRowBuf[3].Split('\t');
                    if (services.GetUpperBound(0) != researchCount)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "(" + RESEARCH_SERVICE_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    // 実施期間(periods[0]は「RPERIOD」)
                    string[] periods = tmpRowBuf[4].Split('\t');
                    if (periods.GetUpperBound(0) != researchCount)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "(" + RESEARCH_PERIODS_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    researchInformationCaptionArray = new string[tmpRowBuf.Length, 1];
                    researchInformationCaptionArray[0, 0] = names[0];
                    researchInformationCaptionArray[1, 0] = ids[0];
                    researchInformationCaptionArray[2, 0] = methods[0];
                    researchInformationCaptionArray[3, 0] = services[0];
                    researchInformationCaptionArray[4, 0] = periods[0];
                    researchinformations = new List<ResearchInformation>(researchCount);
                    // 各配列の左端は、上記キャプション
                    for (int i = 0; i < researchCount; ++i)
                    {
                        int id = 0;
                        if (!int.TryParse(ids[i + 1], out id))
                        {
                            throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                        , new string[] { RAWDATA_PROMPT, "(" + RESEARCH_METHOD_INFORMATION_PROMPT + ")" }
                                        , GlobalsCommonConstant.LogLevel.FATAL, null);
                        }
                        // ResearchInformation researchinformation = new ResearchInformation(id);
                        // researchinformation.Name = names[i + 1];
                        // researchinformation.Method = methods[i + 1];
                        // researchinformation.Service = services[i + 1];
                        // researchinformation.Period = periods[i + 1];
                        // researchinformations[i] = researchinformation;
                        // researchinformations.Add(researchinformation);
                        researchinformations.Add(
                                    new ResearchInformation(id)
                                    {
                                        Name = names[i + 1],
                                        Method = methods[i + 1],
                                        Service = services[i + 1],
                                        Period = periods[i + 1]
                                    }
                                );

                    }

                    // informations[4] = 割付セル情報
                    tmpBuf = System.Text.RegularExpressions.Regex.Unescape(informations[4]);
                    tmpRowBuf = tmpBuf.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    int cellCount = tmpRowBuf.GetUpperBound(0);
                    if (cellCount < 0)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "(" + CELLS_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    cellInformationCaptionArray = tmpRowBuf[0].Split('\t');
                    if (cellCount > 0)
                    {
                        cellinformations = new List<CellInformation>(cellCount);
                        for (int i = 0; i < cellCount; ++i)
                        {
                            string[] tmpSplit = tmpRowBuf[i + 1].Split('\t');
                            if (tmpSplit.Length != 5)
                            {
                                throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                            , new string[] { RAWDATA_PROMPT, "(" + CELLS_INFORMATION_PROMPT + ")" }
                                            , GlobalsCommonConstant.LogLevel.FATAL, null);
                            }
                            if (i == 0) cellInformationRowCaption = tmpSplit[0];
                            string number = tmpSplit[1];
                            string description = tmpSplit[2];
                            CellInformation cellinformation = new CellInformation(number);
                            cellinformation.Description = description;
                            int reqdatacount = 0;
                            if (int.TryParse(tmpSplit[3], out reqdatacount))
                            {
                                //                                cellinformation.RequestDataCount = reqdatacount;
                                cellinformation.RequestDataCount = reqdatacount.ToString();
                            }
                            else
                            {
                                cellinformation.RequestDataCount = null;
                            }
                            int validdatacount = 0;
                            if (int.TryParse(tmpSplit[4], out validdatacount))
                            {
                                //                                cellinformation.ValidDataCount = validdatacount;
                                cellinformation.ValidDataCount = validdatacount.ToString();
                            }
                            else
                            {
                                cellinformation.ValidDataCount = null;
                            }
                            // cellinformations[i] = cellinformation;
                            cellinformations.Add(cellinformation);
                        }
                    }
                    else
                    {
                        cellinformations = new List<CellInformation>();
                    }

                    // informations[5] = セレクト条件情報
                    tmpBuf = System.Text.RegularExpressions.Regex.Unescape(informations[5]);
                    tmpRowBuf = tmpBuf.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    int ruleCount = tmpRowBuf.GetUpperBound(0);
                    if (ruleCount < 0)
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "(" + RULES_INFORMATION_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    ruleInformationCaptionArray = tmpRowBuf[0].Split('\t');
                    if (ruleCount > 0)
                    {
                        ruleinformations = new List<RuleInformation>(ruleCount);
                        for (int i = 0; i < ruleCount; ++i)
                        {
                            string[] tmpSplit = tmpRowBuf[i + 1].Split('\t');
                            if (tmpSplit.Length != 4)
                            {
                                throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                            , new string[] { RAWDATA_PROMPT, "(" + RULES_INFORMATION_PROMPT + ")" }
                                            , GlobalsCommonConstant.LogLevel.FATAL, null);
                            }
                            if (i == 0) ruleInformationRowCaption = tmpSplit[0];
                            string qno = tmpSplit[1];
                            string childqno = tmpSplit[2];
                            string expression = tmpSplit[3];
                            RuleInformation ruleinformation = new RuleInformation(qno);
                            ruleinformation.ChildQuestionNo = childqno;
                            ruleinformation.Expression = expression;
                            // ruleinformations[i] = ruleinformation;
                            ruleinformations.Add(ruleinformation);
                        }
                    }
                    else
                    {
                        ruleinformations = new List<RuleInformation>();
                    }
                }
                else
                {
                    // informations[3] = レイアウト表向き情報
                    int orientation = 0;
                    if (!int.TryParse(informations[3], out orientation))
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                    , new string[] { RAWDATA_PROMPT, "(" + LAYOUT_ORIENTATION_CODE_PROMPT + ")" }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                    this.orientation = (LayoutOrientation)orientation;
                    if (this.orientation != ReportRequest.LayoutOrientation.Portrait)
                    {
                        this.orientation = ReportRequest.LayoutOrientation.Landscape;   // 既定値とする
                    }
                }
                // レイアウト表配列
                if (!string.IsNullOrWhiteSpace(informations[2]))
                {
                    int offset = GlobalMethodClass.CInt(orientation == ReportRequest.LayoutOrientation.Landscape && datatype != OutputDataType.QC3) & 2;
                    int s = 1, e = s;
                    string[] tmpRowBuf = informations[2].Split('\v');
                    for (int i = 0; i < tmpRowBuf.Length; ++i)
                    {
                        string[] tmpBuf = tmpRowBuf[i].Split('\t');
                        if (i == 0)
                        {
                            layoutValue = new string[tmpRowBuf.Length, tmpBuf.Length + offset];
                            if (offset == 2)
                            {
                                string lccd = ParentRequest.LocationCode;
                                layoutValue[i, 0] = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportLayoutSheetNumberColumnCaptionIndex, lccd);
                                layoutValue[i, 1] = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportLayoutColumnNumberColumnCaptionIndex, lccd);
                            }
                        }
                        else if (tmpBuf.Length != layoutValue.GetLength(1) - offset)
                        {
                            Array.Resize<string>(ref tmpBuf, layoutValue.GetLength(1) - offset);
                        }
                        for (int j = 0; j < tmpBuf.Length; ++j)
                        {
                            layoutValue[i, j + offset] = tmpBuf[j] == null ? string.Empty : System.Text.RegularExpressions.Regex.Unescape(tmpBuf[j]);
                        }
                    }
                    DivideRows(layoutValue.GetLength(0));
                }
                DivideColumns(clmsCount);
                /*
                // string[] questionids = this.GetSliceData(0);
                if (questionids == null || questionids.Length == 0)
                {
                    throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                , new string[] { RAWDATA_PROMPT, "(" + ITEM_INFORMATION_PROMPT + ")" }
                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                }
                this.questions = new List<Question.Questions.Question>();
                for (int i = 0; i < questionids.Length; ++i)
                {
                    decimal questionid = (decimal)0;
                    if (decimal.TryParse(questionids[i], out questionid))
                    {
                        Question.Questions.Question question = questions[questionid] as Question.Questions.Question;
                        if (question == null)
                        {
                            throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                        , new string[] { RAWDATA_PROMPT, "(" + ITEM_INFORMATION_PROMPT + ")" }
                                        , GlobalsCommonConstant.LogLevel.FATAL, null);
                        }
                        if ((question.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
                        {
                            for (int cIdx = 1; cIdx <= question.ChildQuestions.Count; ++cIdx)
                            {
                                Question.Questions.Question c = question.ChildQuestions[cIdx] as Question.Questions.Question;
                                if (c == null)
                                {
                                    throw new QCWebException(Constants.INSUFFICIENT_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                                , new string[] { RAWDATA_PROMPT, "(" + ITEM_INFORMATION_PROMPT + ")" }
                                                , GlobalsCommonConstant.LogLevel.FATAL, null);
                                }
                                if (!this.questions.Contains(c)) this.questions.Add(c);
                            }
                        }
                        else
                        {
                            if (!this.questions.Contains(question)) this.questions.Add(question);
                        }
                    }
                    else
                    {
                        throw new QCWebException(Constants.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
                                        , new string[] { RAWDATA_PROMPT, "(" + ITEM_INFORMATION_PROMPT + ")" }
                                        , GlobalsCommonConstant.LogLevel.FATAL, null);
                    }
                }
                */
            }

            /*
            /// <summary>
            /// 出力対象のトップレベルの質問数を返す読み取り専用プロパティ
            /// </summary>
            public int QuestionsCount
            {
                get
                {
                    if (questions == null) return -1;
                    return questions.Count;
                }
            }

            /// <summary>
            /// <paramref name="Index"/>の位置にあるQuestionクラスのインスタンスへの参照を返すメソッド
            /// </summary>
            /// <param name="Index">0ベースのインデックス</param>
            /// <returns>質問情報を保持したQuestionクラスのインスタンスへの参照</returns>
            Question.IQuestion Question(int Index)
            {
                if (questions != null && Index >= 0 && Index < questions.Count)
                {
                    return questions[Index];
                }
                else
                {
                    return null;
                }
            }
            */

            /// <summary>
            /// データ出力時のデータ形式を表すOutputDataType列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public OutputDataType DataType
            {
                get
                {
                    return datatype;
                }
            }

            /// <summary>
            /// レイアウト表の向きを表すLayoutOrientation列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public LayoutOrientation LayoutOrientation
            {
                get
                {
                    return orientation;
                }
            }

            /// <summary>
            /// 調査概要の調査情報を保持したResearchInformation構造体の値からなる配列を返す読み取り専用プロパティ
            /// </summary>
            [ComVisible(false)]
            public ResearchInformation[] ResearchInformationsArray
            {
                get
                {
                    if (researchinformations == null) return null;
                    return researchinformations.ToArray();
                }
            }

            /// <summary>
            /// 調査概要の調査情報の見出しを保持した二次元配列を返す読み取り専用プロパティ
            /// </summary>
            public string[,] ResearchInformationCaptionArray
            {
                get
                {
                    return researchInformationCaptionArray;
                }
            }

            /// <summary>
            /// 調査情報の調査IDの値からなる配列を返す読み取り専用プロパティ
            /// </summary>
            public double[] ResearchInformationIDsArray
            {
                get
                {
                    if (researchinformations == null) return null;
                    return Array.ConvertAll<ResearchInformation, double>(ResearchInformationsArray, x => x.ID2);
                }
            }

            /// <summary>
            /// 調査情報の調査名の値からなる配列を返す読み取り専用プロパティ
            /// </summary>
            public string[] ResearchInformationNamesArray
            {
                get
                {
                    if (researchinformations == null) return null;
                    return Array.ConvertAll<ResearchInformation, string>(ResearchInformationsArray, x => x.Name);
                }
            }

            /// <summary>
            /// 調査情報の調査方法の値からなる配列を返す読み取り専用プロパティ
            /// </summary>
            public string[] ResearchInformationMethodsArray
            {
                get
                {
                    if (researchinformations == null) return null;
                    return Array.ConvertAll<ResearchInformation, string>(ResearchInformationsArray, x => x.Method);
                }
            }

            /// <summary>
            /// 調査情報の商品種別の値からなる配列を返す読み取り専用プロパティ
            /// </summary>
            public string[] ResearchInformationServicesArray
            {
                get
                {
                    if (researchinformations == null) return null;
                    return Array.ConvertAll<ResearchInformation, string>(ResearchInformationsArray, x => x.Service);
                }
            }

            /// <summary>
            /// 調査情報の実施期間の値からなる配列を返す読み取り専用プロパティ
            /// </summary>
            public string[] ResearchInformationPeriodsArray
            {
                get
                {
                    if (researchinformations == null) return null;
                    return Array.ConvertAll<ResearchInformation, string>(ResearchInformationsArray, x => x.Period);
                }
            }

            /// <summary>
            /// 調査概要の割付セル情報を保持したCellInformation構造体の値からなる配列を返す読み取り専用プロパティ
            /// </summary>
            [ComVisible(false)]
            public CellInformation[] CellInformationsArray
            {
                get
                {
                    if (cellinformations == null) return null;
                    return cellinformations.ToArray();
                }
            }

            /// <summary>
            /// 調査概要の割付セル情報の見出しを保持した配列を返す読み取り専用プロパティ
            /// </summary>
            public string[] CellInformationCaptionArray
            {
                get
                {
                    return cellInformationCaptionArray;
                }
            }

            /// <summary>
            /// 調査概要の割付セル情報の行見出しを返す読み取り専用プロパティ
            /// </summary>
            public string CellInformationRowCaption
            {
                get
                {
                    return cellInformationRowCaption;
                }
            }

            /// <summary>
            /// 割付セル情報のセルNo.の値からなる1列の二次元配列を返す読み取り専用プロパティ
            /// </summary>
            public string[,] CellInformationNumbersArray
            {
                get
                {
                    if (cellinformations == null) return null;
                    string[,] res = new string[cellinformations.Count, 1];
                    for (int i = 0; i < cellinformations.Count; ++i)
                    {
                        res[i, 0] = cellinformations[i].Number;
                    }
                    return res;
                }
            }

            /// <summary>
            /// 割付セル情報のセル名称の値からなる1列の二次元配列を返す読み取り専用プロパティ
            /// </summary>
            public string[,] CellInformationDescriptionsArray
            {
                get
                {
                    if (cellinformations == null) return null;
                    string[,] res = new string[cellinformations.Count, 1];
                    for (int i = 0; i < cellinformations.Count; ++i)
                    {
                        res[i, 0] = cellinformations[i].Description;
                    }
                    return res;
                }
            }

            /// <summary>
            /// 割付セル情報の希望サンプル数の値からなる1列の二次元配列を返す読み取り専用プロパティ
            /// </summary>
//            public int?[,] CellInformationRequestDataCountsArray
            public string[,] CellInformationRequestDataCountsArray
            {
                get
                {
                    if (cellinformations == null) return null;
                    //int?[,] res = new int?[cellinformations.Count, 1];
                    string[,] res = new string[cellinformations.Count, 1];
                    for (int i = 0; i < cellinformations.Count; ++i)
                    {
                        res[i, 0] = cellinformations[i].RequestDataCount;
                    }
                    return res;
                }
            }

            /// <summary>
            /// 割付セル情報の有効サンプル数の値からなる1列の二次元配列を返す読み取り専用プロパティ
            /// </summary>
//            public int?[,] CellInformationValidDataCountsArray
            public string[,] CellInformationValidDataCountsArray
            {
                get
                {
                    if (cellinformations == null) return null;
                    //                    int?[,] res = new int?[cellinformations.Count, 1];
                    string[,] res = new string[cellinformations.Count, 1];
                    for (int i = 0; i < cellinformations.Count; ++i)
                    {
                        res[i, 0] = cellinformations[i].ValidDataCount;
                    }
                    return res;
                }
            }

            /// <summary>
            /// 調査概要のセレクト条件情報を保持したRuleInformation構造体の値からなる配列を返す読み取り専用プロパティ
            /// </summary>
            [ComVisible(false)]
            public RuleInformation[] RuleInformationsArray
            {
                get
                {
                    if (ruleinformations == null) return null;
                    return ruleinformations.ToArray();
                }
            }

            /// <summary>
            /// 調査概要のセレクト条件情報の見出しを保持した配列を返す読み取り専用プロパティ
            /// </summary>
            public string[] RuleInformationCaptionArray
            {
                get
                {
                    return ruleInformationCaptionArray;
                }
            }

            /// <summary>
            /// 調査概要のセレクト条件情報の行見出しを返す読み取り専用プロパティ
            /// </summary>
            public string RuleInformationRowCaption
            {
                get
                {
                    return ruleInformationRowCaption;
                }
            }

            /// <summary>
            /// セレクト条件情報の質問番号の値からなる1列の二次元配列を返す読み取り専用プロパティ
            /// </summary>
            public string[,] RuleInformationQuestionNosArray
            {
                get
                {
                    if (ruleinformations == null) return null;
                    string[,] res = new string[ruleinformations.Count, 1];
                    for (int i = 0; i < ruleinformations.Count; ++i)
                    {
                        res[i, 0] = ruleinformations[i].QuestionNo;
                    }
                    return res;
                }
            }

            /// <summary>
            /// セレクト条件情報の子質問番号の値からなる1列の二次元配列を返す読み取り専用プロパティ
            /// </summary>
            public string[,] RuleInformationChildQuestionNosArray
            {
                get
                {
                    if (ruleinformations == null) return null;
                    string[,] res = new string[ruleinformations.Count, 1];
                    for (int i = 0; i < ruleinformations.Count; ++i)
                    {
                        res[i, 0] = ruleinformations[i].ChildQuestionNo;
                    }
                    return res;
                }
            }

            /// <summary>
            /// セレクト条件情報のセレクト条件の値からなる1列の二次元配列を返す読み取り専用プロパティ
            /// </summary>
            public string[,] RuleInformationExpressionsArray
            {
                get
                {
                    if (ruleinformations == null) return null;
                    string[,] res = new string[ruleinformations.Count, 1];
                    for (int i = 0; i < ruleinformations.Count; ++i)
                    {
                        res[i, 0] = ruleinformations[i].Expression;
                    }
                    return res;
                }
            }

            /// <summary>
            /// データシート分割後のシート数を返す
            /// <note>シート分割は、DivideColumnsメソッドを使って行う</note>
            /// </summary>
            public int DividedSheetsCount
            {
                get
                {
                    if (dividedColumnsList == null) return 1;
                    return dividedColumnsList.Count;
                }
            }

            /// <summary>
            /// レイアウトシート分割後のシート数を返す
            /// <note>シート分割は、DivideRowsメソッドを使って行う</note>
            /// </summary>
            public int DividedLayoutSheetsCount
            {
                get
                {
                    if (dividedRowsList == null) return 1;
                    return dividedRowsList.Count;
                }

            }

            /// <summary>
            /// 集計表データの列数を返す読み取り専用プロパティ
            /// </summary>
            /// <param name="SheetIndex">シートインデックス (0ベース)</param>
            /// <returns>集計表データの列数</returns>
            public int SheetColumnsCount(int SheetIndex)
            {
                int res = base.GetTableValueColumnIndexMaximum + 1;
                if (dividedColumnsList == null || SheetIndex < 0 || SheetIndex >= dividedColumnsList.Count) return res;
                res = dividedColumnsList[SheetIndex].EndIndex - dividedColumnsList[SheetIndex].StartIndex + 1 + (GlobalMethodClass.CInt(SheetIndex > 0) & 1);
                return res;
            }

            /// <summary>
            /// レイアウト表データの行数を返す読み取り専用プロパティ
            /// </summary>
            /// <param name="SheetIndex">シートインデックス (0ベース)</param>
            /// <returns>レイアウト表データの行数</returns>
            public int SheetRowsCount(int SheetIndex)
            {
                if (layoutValue == null) return 0;
                int res = layoutValue.GetLength(0);
                if (dividedRowsList == null || SheetIndex < 0 || SheetIndex >= dividedRowsList.Count) return res;
                res = dividedRowsList[SheetIndex].EndIndex - dividedRowsList[SheetIndex].StartIndex + 1 + (GlobalMethodClass.CInt(SheetIndex > 0) & 1);
                return res;
            }

            /// <summary>
            /// レイアウト表データの列数を返す読み取り専用プロパティ
            /// </summary>
            public int LayoutColumnsCount
            {
                get
                {
                    if (layoutValue == null) return 0;
                    return layoutValue.GetLength(1);
                }
            }

            /// <summary>
            /// ローデータ表の各セルの値の文字列表現またはキャプションを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <param name="SheetIndex">シートインデックス (0ベース) (省略可、既定値0)</param>
            /// <param name="Unescape">アンエスケープ処理を行うかどうかを示すフラグ (省略可、既定値false)</param>
            /// <param name="WhitespaceIsNull">スペースのみの場合にnullを返す場合はtrue (省略可、既定値true)</param>
            /// <returns>
            /// <paramref name="SheetIndex"/>番目のローデータ表で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるセルの文字列表現<br />
            /// <paramref name="Unescape"/>がtrueのときには、正規表現のアンエスケープ処理を行ってから返す
            /// </returns>
            public string TableValue(int RowIndex, int ColumnIndex, int SheetIndex = 0, bool Unescape = false, bool WhitespaceIsNull = true)
            {
                if (dividedColumnsList == null || SheetIndex < 0 || SheetIndex >= dividedColumnsList.Count) return null;
                if (ColumnIndex != 0)   // 0の場合はSAMPLEID
                {
                    ColumnIndex += dividedColumnsList[SheetIndex].StartIndex - (GlobalMethodClass.CInt(SheetIndex > 0) & 1);
                    if (ColumnIndex < dividedColumnsList[SheetIndex].StartIndex || ColumnIndex > dividedColumnsList[SheetIndex].EndIndex) return null;
                }
                return TableValue(RowIndex, ColumnIndex, Unescape, WhitespaceIsNull);
            }

            /// <summary>
            /// ローデータ表の列が示すアイテムのQC3での回答タイプを表すQCAnswerType列挙型の値を返すメソッド
            /// </summary>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <param name="SheetIndex">シートインデックス (0ベース) (省略可、既定値0)</param>
            /// <returns>QC3での回答タイプを表すQCAnswerType列挙型の値</returns>
            public Macromill.QCWeb.Question.QCAnswerType ColumnQCAnswerType(int ColumnIndex, int SheetIndex = 0)
            {
                if (dividedColumnsList == null || SheetIndex < 0 || SheetIndex >= dividedColumnsList.Count) return (Macromill.QCWeb.Question.QCAnswerType)0;
                if (ColumnIndex != 0)   // 0の場合はSAMPLEID
                {
                    ColumnIndex += dividedColumnsList[SheetIndex].StartIndex - (GlobalMethodClass.CInt(SheetIndex > 0) & 1);
                    if (ColumnIndex < dividedColumnsList[SheetIndex].StartIndex || ColumnIndex > dividedColumnsList[SheetIndex].EndIndex) return (Macromill.QCWeb.Question.QCAnswerType)0;
                }
                return columnQCAnsType[ColumnIndex];
            }

            /// <summary>
            /// コード形式の場合、ローデータ表の列がMA列かどうかを返すメソッド
            /// </summary>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <param name="SheetIndex">シートインデックス (0ベース) (省略可、既定値0)</param>
            /// <returns>MA列の場合true、そうでない場合false</returns>
            public bool IsMAColumn(int ColumnIndex, int SheetIndex = 0)
            {
                switch (datatype)
                {
                    case OutputDataType.Code:
                    case OutputDataType.QC3:
                        return ColumnQCAnswerType(ColumnIndex, SheetIndex) == Question.QCAnswerType.MA;
                }
                return false;
            }

            /// <summary>
            /// ローデータ表の列がFA列かどうかを返すメソッド
            /// </summary>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <param name="SheetIndex">シートインデックス (0ベース) (省略可、既定値0)</param>
            /// <returns>FA列の場合true、そうでない場合false</returns>
            public bool IsFAColumn(int ColumnIndex, int SheetIndex = 0)
            {
                return ColumnQCAnswerType(ColumnIndex, SheetIndex) == Question.QCAnswerType.FA;
            }

            /// <summary>
            /// レイアウト表のセルの値をアンエスケープして返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <param name="SheetIndex">シートインデックス (0ベース) (省略可、既定値0)</param>
            /// <returns>
            /// レイアウト表で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるセルのアンエスケープ済み文字列表現
            /// </returns>
            public string LayoutValue(int RowIndex, int ColumnIndex, int SheetIndex = 0)
            {
                if (layoutValue == null || dividedRowsList == null || SheetIndex < 0 || SheetIndex >= dividedRowsList.Count) return null;
                if (RowIndex != 0)  // 0の場合は見出し行
                {
                    RowIndex += dividedRowsList[SheetIndex].StartIndex - (GlobalMethodClass.CInt(SheetIndex > 0) & 1);
                    if (RowIndex < dividedRowsList[SheetIndex].StartIndex || RowIndex > dividedRowsList[SheetIndex].EndIndex) return null;
                }
                return layoutValue[RowIndex, ColumnIndex];
            }
        }

        private Outputs.Output output = null;

#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        Tables(Outputs.Output output)
        {
            this.output = output;
        }

        /*
#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        Table Add(string tsvpath)
        {
            string key = tsvpath;
            if (!this.Contains(key))
            {
                Table newTable = new Table(this, tsvpath);
                this.Add(key, newTable);
                return newTable;
            }
            else
            {
                return this[key] as Table;
            }
        }
        */

        private Hashtable keyItemMaxSectorNumberTable = new Hashtable();

        /// <summary>
        /// 分類アイテムの最大選択肢番号を返すメソッド
        /// </summary>
        /// <param name="keyItemName">分類アイテム名</param>
        /// <returns>最大選択肢番号</returns>
        public int KeyItemMaxSectorNumber(string keyItemName)
        {
            if (keyItemName == null) return 0;
            if (keyItemMaxSectorNumberTable.ContainsKey(keyItemName)) return (int)keyItemMaxSectorNumberTable[keyItemName];
            return 0;
        }

        /*
#if FOR_UNIT_TEST
        public
#else
        internal
#endif
        Table Add(string tsvpath, Type tableType = null)
        {
            string key = tsvpath;
            if (!this.Contains(key))
            {
                Table newTable = null;
                if (tableType == null) 
                {
                    newTable = new Table(this, tsvpath);
                }
                else if (tableType == typeof(GTTable)) 
                {
                    newTable = new GTTable(this, tsvpath);
                } 
                else if (tableType == typeof(CrossTable)) 
                {
                    newTable = new CrossTable(this, tsvpath);
                }
                else if (tableType == typeof(FAListTable)) 
                {
                    newTable = new FAListTable(this, tsvpath);
                }
                else if (tableType == typeof(CheckListTable)) 
                {
                    newTable = new CheckListTable(this, tsvpath);
                }
                else if (tableType == typeof(QuestionnaireTable)) 
                {
                    newTable = new QuestionnaireTable(this, tsvpath);
                }
                else if (tableType == typeof(RawDataTable)) 
                {
                    newTable = new RawDataTable(this, tsvpath);
                }
                if (newTable != null) this.Add(key, newTable);
                return newTable;
            }
            else
            {
                return this[key] as RawDataTable;
            }
        }
        */
        internal Table Add(string tsvpath, OutputType outputType)
        {
            string key = tsvpath;
            if (this.Contains(key))
            {
                switch (outputType)
                {
                    case OutputType.GT:
                        return this[key] as GTTable;
                    case OutputType.Cross:
                        return this[key] as CrossTable;
                    case OutputType.FAList:
                        return this[key] as FAListTable;
                    case OutputType.CheckList:
                        return this[key] as CheckListTable;
                    case OutputType.Questionnaire:
                        return this[key] as QuestionnaireTable;
                    case OutputType.RawData:
                    case OutputType.QC3:
                        return this[key] as RawDataTable;
                    default:
                        return this[key] as Table;
                }
            }
            Table newTable = null;
            KeyItemInformation keyItem = null;
            switch (outputType)
            {
                case OutputType.GT:
                    newTable = new GTTable(this, tsvpath);
                    keyItem = (newTable as GTTable).KeyItem;
                    break;
                case OutputType.Cross:
                    newTable = new CrossTable(this, tsvpath);
                    keyItem = (newTable as CrossTable).KeyItem;
                    break;
                case OutputType.FAList:
                    newTable = new FAListTable(this, tsvpath);
                    keyItem = (newTable as FAListTable).KeyItem;
                    break;
                case OutputType.CheckList:
                    newTable = new CheckListTable(this, tsvpath);
                    break;
                case OutputType.Questionnaire:
                    newTable = new QuestionnaireTable(this, tsvpath);
                    break;
                case OutputType.RawData:
                case OutputType.QC3:
                    newTable = new RawDataTable(this, tsvpath);
                    break;
                default:
                    newTable = new Table(this, tsvpath);
                    break;
            }
            if (newTable != null)
            {
                this.Add(key, newTable);
                if (keyItem != null)
                {
                    string keyItemName = keyItem.Name;
                    int keyItemSecNo = keyItem.SectorNumber;
                    if (!string.IsNullOrWhiteSpace(keyItemName) && keyItemSecNo > 0)
                    {
                        int orgMaxSecNo = KeyItemMaxSectorNumber(keyItemName);
                        if (orgMaxSecNo == 0)
                        {
                            keyItemMaxSectorNumberTable.Add(keyItemName, keyItemSecNo);
                        }
                        else if (keyItemSecNo > orgMaxSecNo)
                        {
                            keyItemMaxSectorNumberTable[keyItemName] = keyItemSecNo;
                        }
                    }
                }
            }
            return newTable;
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>インデックスが示すコレクションの要素であるTableクラスのインスタンスへの参照</returns>
        public ITable this[int index]
        {
            get
            {
                foreach (ITable table in this.Values)
                {
                    if (table.Index == index) return table;
                }
                return null;
            }
        }

        /// <summary>
        /// コレクションの要素を返すインデクサ
        /// </summary>
        /// <param name="key">キーとなる文字列</param>
        /// <returns>キーが示すコレクションの要素であるTableクラスのインスタンスへの参照</returns>
        public ITable this[string key]
        {
            get
            {
                return base[key] as ITable;
            }
        }

        /// <summary>
        /// 要素数を返す読み取り専用プロパティ
        /// </summary>
        public new int Count
        {
            get
            {
                return (this as Hashtable).Count;
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (Table tbl in this.Values)
            {
                tbl.Dispose();
            }
            output = null;
        }

        /// <summary>
        /// 自身のインスタンスの親であるOutputクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IOutput ParentOutput
        {
            get
            {
                return output;
            }
        }

        /// <summary>
        /// 自身のインスタンスの親であるReportsetクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IReportset ParentReportset
        {
            get
            {
                if (output == null) return null;
                return output.ParentReportset;
            }
        }

        /// <summary>
        /// 自身のインスタンスの親であるRequestクラスのインスタンスへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IRequest ParentRequest
        {
            get
            {
                if (output == null) return null;
                return output.ParentRequest;
            }
        }
    }
}

