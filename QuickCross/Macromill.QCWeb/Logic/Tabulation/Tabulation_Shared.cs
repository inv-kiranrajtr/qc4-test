#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：EnumeratedType.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/7/24
 * 作　成　者：井川はるき
 * 更　新　日：2012/8/7
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion

#define AFTER_2ND_PHASE
#define IS_2ND_PHASE
#undef AFTER_2ND_PHASE
// #undef IS_2ND_PHASE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Macromill.QCWeb.Question;
using Seasar.Quill;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Exceptions;
using System.Text.RegularExpressions;
using System.IO;
using ExcelAddIn.DB;
using Strings = Microsoft.VisualBasic.Strings;

namespace Macromill.QCWeb.Tabulation
{
    #region DataWithMarking構造体
    /// <summary>
    /// DataWithMarking構造体のタイプを表す列挙型
    /// </summary>
    [ComVisible(false), Flags]
    public enum CellType : int
    {
        /// <summary>見出しなど、データが不要なセルを表す</summary>
        CaptionCell = 0,
        /// <summary>WB前全体列のセル (カウントを出すだけのセル)を表す</summary>
        PreWBTotalCell = 1,
        /// <summary>全体列のセルを表す</summary>
        TotalCell = 2,
        /// <summary>データセル(詳細情報を持つセル)を表す</summary>
        DataCell = 4,
        /// <summary>無回答や非該当などのデータセル(サマリを出すだけのセル)を表す</summary>
        NAIVCell = 8,
        /// <summary>GTマトリクスの選択肢間検定時にデータセルを表す</summary>
        SimpleDataCell = 16,
        /// <summary>統計量母数列のセルを表す</summary>
        PopulationCell = TotalCell | DataCell,
        /// <summary>
        /// 小計行または全体行のセルを表す(全体との差の検定で使用)
        /// </summary>
        SubTotalCell = TotalCell | 32
    }
    /// <summary>
    /// 詳細データを管理する構造体
    /// </summary>
    [ComVisible(false)]
    public struct DetailData
    {
        /// <summary>データ値×WB値のサマリ</summary>
        public double summary;
        /// <summary>WB値のサマリ</summary>
        public double count;
        /// <summary>データ値の平方×WB値のサマリ</summary>
        public double squareSummary;
        /// <summary>WB値の平方のサマリ</summary>
        public double wbSquareSummary;
        /// <summary>オーバーラップ対象のデータ値×WB値のサマリ (オーバーラップ分で使用)</summary>
        public double overlaptargetValueSummary;
        /// <summary>2つのデータ値の積×WB値のサマリ (オーバーラップ分で使用)</summary>
        public double multipliedSummary;
        /// <summary>オーバーラップ対象のデータ値の平方×WB値のサマリ (オーバーラップ分で使用)</summary>
        public double overlaptargetSquareValueSummary;
        /// <summary>検定対象とのオーバーラップ分の詳細データを保持するハッシュテーブル</summary>
        public Hashtable overlapDatas;

        /// <summary>
        /// カウントの加算を行うメソッド
        /// </summary>
        /// <param name="weightback">ウエイトバック値</param>
        /// <param name="AddWbSquareSummary">ウエイトバック値の平方のサマリも求める場合はtrue</param>
        private void AddCount(double weightback, bool AddWbSquareSummary)
        {
            count += weightback;    // オーバーラップ分の場合N0、そうでなければN1やN2
            if (AddWbSquareSummary)
            {
                wbSquareSummary += Math.Pow(weightback, 2.0);   // オーバーラップ分の場合q0、そうでなければq1やq2
            }
        }

        /// <summary>
        /// データ値の加算を行うメソッド
        /// </summary>
        /// <param name="value">データ値 (SA/MAの集計では1.0/0.0)</param>
        /// <param name="weightback">ウエイトバック値</param>
        /// <param name="AddSquareSummary">データ値の平方のサマリも求める場合はtrue</param>
        private void AddData(double value, double weightback, bool AddSquareSummary)
        {
            if (double.IsNaN(summary)) return;
            summary += value * weightback;  // オーバーラップの場合はX1inOverlap、そうでなければX1やX2
            if (AddSquareSummary)
            {
                squareSummary += Math.Pow(value, 2.0) * weightback; // オーバーラップの場合はY1inOverlap、そうでなければY1やY2
            }
        }

        /// <summary>
        /// オーバーラップ情報時の詳細データを加算更新するメソッド
        /// </summary>
        /// <param name="value">データ値 (SA/MAの集計では1.0/0.0)</param>
        /// <param name="overlapValue">データ値の積を算出する際(オーバーラップ分で使用)に指定 (省略可、既定値NaN)</param>
        /// <param name="weightback">ウエイトバック値</param>
        /// <param name="isSimple">セルの種類がSimpleDataCellのときtrue</param>
        private void AddDetailAtOverlap(double value, double overlapValue, double weightback, bool isSimple)
        {
            multipliedSummary += value * overlapValue * weightback; // X0inOverlap
            if (isSimple) return;
            overlaptargetValueSummary += overlapValue * weightback; // X2inOverlap
            overlaptargetSquareValueSummary += Math.Pow(overlapValue, 2.0) * weightback;    // Y2inOverlap            
        }

        /// <summary>
        /// 詳細データを加算更新するメソッド
        /// </summary>
        /// <param name="value">データ値 (SA/MAの集計では1.0/0.0)</param>
        /// <param name="weightback">ウエイトバック値</param>
        /// <param name="overlapValue">データ値の積を算出する際(オーバーラップ分で使用)に指定 (省略可、既定値NaN)</param>
        /// <param name="celltype">DataWithMarking構造体のタイプを表すCellType列挙型の値 (省略可、既定値CellType.DataCell)</param>
        public void AddDetail(double value, double weightback, double overlapValue = double.NaN, CellType celltype = CellType.DataCell)
        {
            if (celltype == CellType.CaptionCell) return;
            if ((celltype & CellType.PreWBTotalCell) == CellType.PreWBTotalCell)
            {
                ++count;
                return;
            }
            if ((celltype & CellType.TotalCell) == CellType.TotalCell)
            {
                AddCount(weightback, (celltype & CellType.NAIVCell) != CellType.NAIVCell);
            }
            if ((celltype & CellType.DataCell) == CellType.DataCell)
            {
                AddData(value, weightback, (int)(celltype & CellType.NAIVCell) == 0);
                if (!double.IsNaN(overlapValue))
                {
                    AddDetailAtOverlap(value, overlapValue, weightback, false);
                }
                return;
            }
            else if ((celltype & CellType.SimpleDataCell) == CellType.SimpleDataCell)
            {
                AddData(1.0, weightback, false);
                if (!double.IsNaN(overlapValue))
                {
                    AddDetailAtOverlap(1.0, 1.0, weightback, true);
                }
                return;
            }
            else
            {
                // 無回答や非該当など
                AddData(value, weightback, false);
            }
        }
    }

    /// <summary>
    /// 項目間検定結果の情報を管理する構造体
    /// </summary>
    [ComVisible(false)]
    public struct SignificanceTestTarget
    {
        private int sectornumber;
        private int level;
        private int order;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sectornumber">対象選択肢の選択肢番号</param>
        /// <param name="level">行う検定の棄却域が狭いものから0ベース (1％と5％の場合、0が1％、1が5％)</param>
        public SignificanceTestTarget(int sectornumber, int level)
        {
            this.sectornumber = sectornumber;
            this.level = GlobalMethodClass.CInt(level != 0) & 1;
            this.order = sectornumber;
        }

        /// <summary>
        /// 対象選択肢の選択肢番号を返す読み取り専用プロパティ
        /// </summary>
        public int SectorNubmer
        {
            get
            {
                return sectornumber;
            }
        }

        /// <summary>
        /// 並び順を表すプロパティ
        /// </summary>
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

        /// <summary>
        /// 有意水準のレベルを返す読み取り専用プロパティ
        /// </summary>
        public int Level
        {
            get
            {
                return level;
            }
        }
    }

    /// <summary>
    /// SignificanceTestTargetの比較を行うためのIEqualityComparerインターフェイス実装クラス
    /// </summary>
    [ComVisible(false), Guid("5B9C5D7F-E3F8-4CA0-8F24-952ED6B6BC39")]
    public class SignificanceTestTargetComparer : IEqualityComparer<SignificanceTestTarget>, IComparer<SignificanceTestTarget>
    {
        /// <summary>
        /// Equalsメソッドの実装
        /// </summary>
        /// <param name="x">比較する1つ目のSignificanceTestTarget構造体の値</param>
        /// <param name="y">比較する2つ目のSignificanceTestTarget構造体の値</param>
        /// <returns>選択肢番号が等しいかどうか</returns>
        public bool Equals(SignificanceTestTarget x, SignificanceTestTarget y)
        {
            return x.SectorNubmer == y.SectorNubmer;
        }

        /// <summary>
        /// GetHashCodeメソッドの実装
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(SignificanceTestTarget obj)
        {
            return obj.SectorNubmer;
        }

        /// <summary>
        /// Compareメソッドの実装
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(SignificanceTestTarget x, SignificanceTestTarget y)
        {
            return x.SectorNubmer.CompareTo(y.SectorNubmer);
        }
    }

    /// <summary>
    /// 項目間検定レターとする文字または文字列からなる配列を管理する静的クラス
    /// </summary>
    [ComVisible(false), Guid("5AC667B4-A0A8-4CBA-BC7A-1CC3F63BF8BF")]
    public static class SignificanceTestLetters
    {
        private static string[] significanceTestLetters = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static SignificanceTestLetters()
        {
            if (significanceTestLetters != null) return;
            significanceTestLetters = new string[52];
            int i = 0;
            for (char c = 'A'; c <= 'Z'; ++c, ++i)
            {
                significanceTestLetters[i] = c.ToString();
                significanceTestLetters[i + 26] = c.ToString() + "'";
            }
        }

        /// <summary>
        /// 項目間検定レターからなる配列への参照を返す読み取り専用静的プロパティ
        /// </summary>
        public static string[] Array
        {
            get
            {
                return significanceTestLetters;
            }
        }

        /// <summary>
        /// 選択肢番号または子質問番号が示す項目間検定レターを返す静的メソッド
        /// </summary>
        /// <param name="index">1ベースのインデックス</param>
        /// <returns>項目間検定レター</returns>
        public static string Character(int index)
        {
            if (index >= 1 && index <= significanceTestLetters.Length)
            {
                return significanceTestLetters[index - 1];
            }
            return null;
        }
    }

    /// <summary>
    /// 集計結果データにマーキング情報を付けた形で管理する構造体
    /// </summary>
    [ComVisible(false), Serializable]
    public struct DataWithMarking : IComparable
    {
        private CellType celltype;
        private string value;   // N値の文字列表現またはキャプション
        private double percent; // ％値
        private DataMarking marking;    // データのマーキング情報
        // private char[] significancetestcharacters;  // 項目間検定レター
        private string significancetestcharacters;
        private DetailData detail;  // 詳細データ
        private int sectornumber;   // 選択肢(子質問)番号
        private int sectorscount;   // 選択肢(子質問)数
        private List<SignificanceTestTarget> significancetestTargets;    // 項目間検定結果対象選択肢(子質問)番号と検定水準
        // private string[] significanceTestLetters;
        private bool countOverlap;

        /*
        private void initSignificanceTestLetters()
        {
            significanceTestLetters = new string[52];
            int i = 0;
            for (char c = 'A'; c <= 'Z'; ++c, ++i)
            {
                significanceTestLetters[i] = c.ToString();
                significanceTestLetters[i + 26] = c.ToString() + "'";
            }
        }
        */

        /// <summary>
        /// オーバーラップ分の詳細データを保持するハッシュテーブルの初期化
        /// </summary>
        private void InitializeOverlapDatas()
        {
            detail.overlapDatas = new Hashtable();
            for (int i = sectornumber + 1; i <= sectorscount; ++i)
            {
                detail.overlapDatas.Add(i.ToString(), new DetailData());
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">データ値の文字列表現</param>
        /// <param name="judgeData">
        /// <paramref name="value"/>が数値の文字列表現として評価できる場合に、DataWithMarking構造体のタイプをDataCellとする判定を行う場合はtrue (省略可、既定値true)<br />
        /// falseを指定した場合、タイプをCaptionCellとする
        /// </param>
        public DataWithMarking(string value, bool judgeData = true)
        {
            this.value = value;
            percent = 0.0;
            marking = (DataMarking)0;
            significancetestcharacters = null;
            detail = new DetailData();
            Tabulation.CellType celltype = Tabulation.CellType.CaptionCell;
            if (judgeData)
            {
                double n = 0.0;
                if (double.TryParse(value, out n))
                {
                    detail.summary = n;
                    celltype = Tabulation.CellType.DataCell;
                }
            }
            if (celltype == Tabulation.CellType.CaptionCell)
            {
                detail.summary = double.NaN;
            }
            this.celltype = celltype;
            sectornumber = 0;
            sectorscount = 0;
            significancetestTargets = null;
            // significanceTestLetters = null;
            countOverlap = false;
        }

        /// <summary>
        /// 項目間検定時に、検定処理対象のセルデータを保持する場合のコンストラクタ
        /// </summary>
        /// <param name="sectornumber">選択肢番号</param>
        /// <param name="sectorscount">選択肢数</param>
        /// <param name="celltype">DataWithMarking構造体のタイプを表すCellType列挙型の値 (省略可、既定値CellType.DataCell)</param>
        /// <param name="CountOverlap">オーバーラップ分を集計する場合はtrue (省略可、既定値true)</param>
        /// <note>マトリクスの子質問間検定の場合は、「選択肢」を「子質問」と読み替える</note>
        public DataWithMarking(int sectornumber, int sectorscount, CellType celltype = CellType.DataCell, bool CountOverlap = true)
        {
            value = "0";
            percent = 0.0;
            marking = (DataMarking)0;
            significancetestcharacters = null;
            detail = new DetailData();
            this.sectornumber = sectornumber;
            this.sectorscount = sectorscount;
            this.celltype = celltype;
            significancetestTargets = null;
            // significanceTestLetters = null;
            countOverlap = CountOverlap;
            if (SettedSectorInformation)
            {
                significancetestTargets = new List<SignificanceTestTarget>();
                // initSignificanceTestLetters();
                if (CountOverlap) InitializeOverlapDatas();
            }
            else
            {
                this.sectornumber = 0;
                this.sectorscount = 0;
            }
        }

        /// <summary>
        /// 項目間検定でマトリクスの選択肢間検定時などに、全体列などのセルデータを保持する場合のコンストラクタ
        /// </summary>
        /// <param name="celltype">DataWithMarking構造体のタイプを表すCellType列挙型の値 (省略可、既定値CellType.TotalCell)</param>
        public DataWithMarking(CellType celltype = CellType.TotalCell)
        {
            value = "0";
            percent = 0.0;
            marking = (DataMarking)0;
            significancetestcharacters = null;
            detail = new DetailData();
            sectornumber = 0;
            sectorscount = 0;
            this.celltype = celltype;
            significancetestTargets = null;
            // significanceTestLetters = null;
            countOverlap = false;
        }

        /// <summary>
        /// DataWithMarking構造体のタイプを表すCellType列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public CellType CellType
        {
            get
            {
                return celltype;
            }
        }

        /// <summary>
        /// N値の文字列表現またはキャプションを返す読み取り専用プロパティ
        /// </summary>
        public string Value
        {
            get
            {
                if (value == null) return value;
                if ((int)(celltype & (Tabulation.CellType.PreWBTotalCell | Tabulation.CellType.TotalCell)) != 0)
                {
                    return detail.count.ToString();
                }
                if (double.IsNaN(detail.summary)) return value;
                return detail.summary.ToString();
            }
        }

        /// <summary>
        /// N値を倍精度浮動小数点数型で返す読み取り専用プロパティ
        /// </summary>
        public double NumValue
        {
            get
            {
                //double n = 0.0;
                //if (double.TryParse(value, out n))
                //{
                //    return n;
                //}
                //else
                //{
                //    // return 0.0;
                //    return double.NaN;
                //}
                if (value == null) return double.NaN;
                if ((int)(celltype & (Tabulation.CellType.PreWBTotalCell | Tabulation.CellType.TotalCell)) != 0)
                {
                    if ((int)(celltype & (Tabulation.CellType.DataCell | Tabulation.CellType.SimpleDataCell)) == 0)
                    {
                        return detail.count;
                    }
                }
                return detail.summary;
            }
        }

        /// <summary>
        /// ％値を取得/設定するプロパティ
        /// </summary>
        public double Percent
        {
            get
            {
                return percent;
            }
            set
            {
                percent = value;
            }
        }

        /// <summary>
        /// 選択肢(または子質問)情報が設定されているかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool SettedSectorInformation
        {
            get
            {
                return sectorscount > 0 && sectornumber > 0;
            }
        }

        /// <summary>
        /// 選択肢(または子質問)番号を返す読み取り専用プロパティ
        /// </summary>
        public int SectorNumber
        {
            get
            {
                return sectornumber;
            }
        }

        /// <summary>
        /// 選択肢(または子質問)数を返す読み取り専用プロパティ
        /// </summary>
        public int SectorsCount
        {
            get
            {
                return sectorscount;
            }
        }

        /// <summary>
        /// オーバーラップ情報があるかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool HasOverlap
        {
            get
            {
                return countOverlap;
            }
        }

        /// <summary>
        /// 検定対象の選択肢とのオーバーラップ分の詳細データを返すメソッド
        /// </summary>
        /// <param name="targetSectorNumber">検定対象の選択肢番号(自身の選択肢番号より大きい必要がある)</param>
        /// <returns>検定対象の選択肢とのオーバーラップ分の詳細データ</returns>
        /// <note>マトリクスの子質問間検定の場合は、「選択肢」を「子質問」と読み替える</note>
        public DetailData OverlapData(int targetSectorNumber)
        {
            if (detail.overlapDatas == null
                || targetSectorNumber <= sectornumber || targetSectorNumber > sectorscount) return new DetailData();
            return (DetailData)detail.overlapDatas[targetSectorNumber.ToString()];
        }

        /// <summary>
        /// 詳細データを加算更新するメソッド
        /// </summary>
        /// <param name="value">データ値 (SA/MAの集計では1.0/0.0)</param>
        /// <param name="weightback">ウエイトバック値</param>
        public void AddDetail(double value, double weightback)
        {
            detail.AddDetail(value, weightback, double.NaN, celltype);
        }

        /// <summary>
        /// 検定対象の選択肢とのオーバーラップ分の詳細データを加算更新するメソッド
        /// </summary>
        /// <param name="targetSectorNumber">検定対象の選択肢番号(自身の選択肢番号より大きい必要がある)</param>
        /// <param name="value1">データ値1 (SA/MAの集計では1.0/0.0)</param>
        /// <param name="value2">データ値2 (SA/MAの集計では1.0/0.0)</param>
        /// <param name="weightback">ウエイトバック値</param>
        /// <note>マトリクスの子質問間検定の場合は、「選択肢」を「子質問」と読み替える</note>
        public void AddOverlapDetail(int targetSectorNumber, double value1, double value2, double weightback)
        {
            if (detail.overlapDatas == null || !detail.overlapDatas.ContainsKey(targetSectorNumber.ToString())) return;
            if (targetSectorNumber <= sectornumber || targetSectorNumber > sectorscount) return;
            DetailData tmp = OverlapData(targetSectorNumber);
            tmp.AddDetail(value1, weightback, value2, celltype);
            detail.overlapDatas[targetSectorNumber.ToString()] = tmp;
        }

        /// <summary>
        /// データ値の平方×WB値の合計を返す読み取り専用プロパティ
        /// </summary>
        public double SquareSummary
        {
            get
            {
                return detail.squareSummary;
            }
        }

        /// <summary>
        /// WB値の平方の合計を返す読み取り専用プロパティ
        /// </summary>
        public double WBSquareSummary
        {
            get
            {
                return detail.wbSquareSummary;
            }
        }

        /// <summary>
        /// オーバーラップ分の2つのデータ値の積のサマリを返す読み取り専用プロパティ
        /// </summary>
        public double MultipliedSummary
        {
            get
            {
                return detail.multipliedSummary;
            }
        }

        /// <summary>
        /// オーバーラップ分のデータ数を返す読み取り専用プロパティ
        /// </summary>
        public double Count
        {
            get
            {
                return detail.count;
            }
        }

        /// <summary>
        /// マーキング情報を追加またはリフレッシュするメソッド
        /// </summary>
        /// <param name="marking">追加するマーキング情報または新たなマーキング情報を表すDataMarking列挙型の値</param>
        /// <param name="replace">リフレッシュ登録する場合はtrue (省略可, 既定値false)</param>
        public void AppendMarking(DataMarking marking, bool replace = false)
        {
            marking &= DataMarking.MarkingAllBit;
            if (replace)
            {
                this.marking = marking;
            }
            else
            {
                this.marking |= marking;
            }
        }

        /// <summary>
        /// マーキング情報を削除するメソッド
        /// </summary>
        /// <param name="marking">削除するマーキング情報</param>
        public void RemoveMarking(DataMarking marking)
        {
            this.marking &= ~marking;
        }

        /// <summary>
        /// すべてのマーキング情報を削除するメソッド
        /// </summary>
        /// <param name="clearSignificanceTargets">項目間検定結果対象選択肢コレクションおよび項目間検定レターもクリアする場合true (省略可、既定値true)</param>
        public void ClearAllMarking(bool clearSignificanceTargets = true)
        {
            RemoveMarking(DataMarking.MarkingAllBit);
            if (clearSignificanceTargets) ClearSignificanceTargets();
        }

        /// <summary>
        /// マーキング情報を表すDataMarking列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public DataMarking Marking
        {
            get
            {
                return marking;
            }
        }

        /// <summary>
        /// 全体との比率の差の色付けマーキングの情報を表すDataMarking列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public DataMarking ColoringMarking
        {
            get
            {
                return marking & DataMarking.ColoringAllBit;
            }
        }

        /// <summary>
        /// 全体との差の色付けマーキングで水準1で高いかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ColoringLevel1High
        {
            get
            {
                switch (ColoringMarking)
                {
                    case DataMarking.ColoringLevel1High:
                    case DataMarking.ColoringLevel2High:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 全体との差の色付けマーキングで水準2で高いかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ColoringLevel2High
        {
            get
            {
                return ColoringMarking == DataMarking.ColoringLevel2High;
            }
        }

        /// <summary>
        /// 全体との差の色付けマーキングで水準1で低いかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ColoringLevel1Low
        {
            get
            {
                switch (ColoringMarking)
                {
                    case DataMarking.ColoringLevel1Low:
                    case DataMarking.ColoringLevel2Low:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 全体との差の色付けマーキングで水準2で低いかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ColoringLevel2Low
        {
            get
            {
                return ColoringMarking == DataMarking.ColoringLevel2Low;
            }
        }

        /// <summary>
        /// ランキングマーキングの情報を表すDataMarking列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public DataMarking RankingMarking
        {
            get
            {
                return marking & DataMarking.RankingAllBit;
            }
        }

        /// <summary>
        /// ランク情報を返す読み取り専用プロパティ
        /// </summary>
        public int Rank
        {
            get
            {
                DataMarking rankMark = RankingMarking;
                return (int)rankMark / (int)DataMarking.Ranking1;
            }
        }

        /// <summary>
        /// 昇降分析マーキングの情報を表すDataMarking列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public DataMarking AscendingMarking
        {
            get
            {
                return marking & DataMarking.AscendingAllBit;
            }
        }

        /// <summary>
        /// 昇降分析マーキングの矢筈または矢尻にあたるかどうかを返すメソッド
        /// </summary>
        /// <param name="reverseSide">逆側の昇降分析マーキングの値(DataMarking列挙型) (戻り値)</param>
        public bool IsArrowEnd(out DataMarking reverseSide)
        {
            reverseSide = (DataMarking)0;
            switch (AscendingMarking)
            {
                case DataMarking.AscendingStart:
                    reverseSide = DataMarking.AscendingEnd;
                    return true;
                case DataMarking.AscendingEnd:
                    reverseSide = DataMarking.AscendingStart;
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 昇降分析マーキングの矢柄にあたるかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool IsArrowShaft
        {
            get
            {
                return AscendingMarking == DataMarking.AscendingBody;
            }
        }

        /// <summary>
        /// 有意差検定マーキングの情報を表すDataMarking列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public DataMarking SignificanceMarking
        {
            get
            {
                return marking & DataMarking.SignificanceAllBit;
            }
        }

        /// <summary>
        /// 有意差検定で1％水準で有意に高いかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool Significance1PercentHigh
        {
            get
            {
                return SignificanceMarking == DataMarking.SignificanceOneHigh;
            }
        }

        /// <summary>
        /// 有意差検定で5％水準で有意に高いかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool Significance5PercentHigh
        {
            get
            {
                switch (SignificanceMarking)
                {
                    case DataMarking.SignificanceOneHigh:
                    case DataMarking.SignificanceFiveHigh:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 有意差検定で10％水準で有意に高いかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool Significance10PercentHigh
        {
            get
            {
                switch (SignificanceMarking)
                {
                    case DataMarking.SignificanceOneHigh:
                    case DataMarking.SignificanceFiveHigh:
                    case DataMarking.SignificanceTenHigh:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 有意差検定で1％水準で有意に低いかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool Significance1PercentLow
        {
            get
            {
                return SignificanceMarking == DataMarking.SignificanceOneLow;
            }
        }

        /// <summary>
        /// 有意差検定で5％水準で有意に低いかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool Significance5PercentLow
        {
            get
            {
                switch (SignificanceMarking)
                {
                    case DataMarking.SignificanceOneLow:
                    case DataMarking.SignificanceFiveLow:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 有意差検定で10％水準で有意に低いかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool Significance10PercentLow
        {
            get
            {
                switch (SignificanceMarking)
                {
                    case DataMarking.SignificanceOneLow:
                    case DataMarking.SignificanceFiveLow:
                    case DataMarking.SignificanceTenLow:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 全体との差の検定マークを返すメソッド
        /// </summary>
        public string SignificanceMark(string locale = "ja")
        {
            if (Significance1PercentHigh) return GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeHighAt1MarkIndex, locale);
            if (Significance1PercentLow) return GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeLowAt1MarkIndex, locale);
            if (Significance5PercentHigh) return GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeHighAt5MarkIndex, locale);
            if (Significance5PercentLow) return GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeLowAt5MarkIndex, locale);
            if (Significance10PercentHigh) return GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeHighAt10MarkIndex, locale);
            if (Significance10PercentLow) return GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeLowAt10MarkIndex, locale);
            return null;
        }

        /*
        /// <summary>
        /// 項目間検定レターを追加するメソッド
        /// </summary>
        /// <param name="character">追加する項目間検定レター</param>
        public void AppendSignificanceCharacter(char character)
        {
            if ((character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z'))
            {
                if (significancetestcharacters == null)
                {
                    significancetestcharacters = new char[1];
                    significancetestcharacters[0] = character;
                }
                else
                {
                    if (!significancetestcharacters.Contains<char>(character))
                    {
                        Array.Resize<char>(ref significancetestcharacters, significancetestcharacters.Length + 1);
                        significancetestcharacters[significancetestcharacters.GetUpperBound(0)] = character;
                    }
                }
            }
        }

        /// <summary>
        /// 項目間検定レターを一度に設定するメソッド
        /// </summary>
        /// <param name="characters">項目間検定レターからなる文字列</param>
        public void SetSignificanceCharacters(string characters)
        {
            significancetestcharacters = characters.ToCharArray();
        }

        /// <summary>
        /// 項目間検定レターをテキストモードで昇順に並べてから連結した文字列を返すメソッド
        /// </summary>
        public string SignificanceCharacters()
        {
            if (significancetestcharacters == null) return null;
            Comparison<char> comparison = (x, y) => string.Compare(x.ToString(), y.ToString(), true);
            Array.Sort(significancetestcharacters, comparison);
            Converter<char, string> converter = chr => chr.ToString();
            string[] sigcharacters = Array.ConvertAll<char, string>(significancetestcharacters, converter);
            return string.Join("", sigcharacters);
        }
        */

        /// <summary>
        /// 項目間検定結果対象選択肢を追加するメソッド
        /// </summary>
        /// <param name="targetSectornumber">追加する検定結果対象選択肢</param>
        /// <param name="level">有意水準レベル</param>
        /// <note>マトリクスの子質問間検定の場合は、「選択肢」を「子質問」と読み替える</note>
        public void AppendSignificanceSectorNumber(int targetSectornumber, int level)
        {
            if (targetSectornumber < 1) return;
            if (targetSectornumber > sectorscount) return;
            if (targetSectornumber == sectornumber) return;
            if (significancetestTargets == null) return;
            significancetestcharacters = null;
            SignificanceTestTarget newItem = new SignificanceTestTarget(targetSectornumber, level);
            if (!significancetestTargets.Contains<SignificanceTestTarget>(newItem, new SignificanceTestTargetComparer()))
            {
                significancetestTargets.Add(newItem);
            }
        }

        /// <summary>
        /// 項目間検定結果対象選択肢コレクションから特定の選択肢を削除するメソッド
        /// </summary>
        /// <param name="targetSectornumber">削除する検定結果対象選択肢</param>
        public void RemoveSignificanceSectorNumber(int targetSectornumber)
        {
            if (significancetestTargets == null) return;
            significancetestcharacters = null;
            for (int i = 0; i < significancetestTargets.Count; ++i)
            {
                if (significancetestTargets[i].SectorNubmer == targetSectornumber)
                {
                    significancetestTargets.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// 項目間検定レターを一度に設定するメソッド
        /// </summary>
        /// <param name="characters">項目間検定レターからなる文字列</param>
        public void SetSignificanceCharacters(string characters)
        {
            if (string.IsNullOrWhiteSpace(characters)) characters = null;
            significancetestcharacters = characters;
            if (characters == null && significancetestTargets != null) significancetestTargets.Clear();
        }

        /// <summary>
        /// 項目間検定結果対象選択肢コレクションおよび項目間検定レターをクリアするメソッド
        /// </summary>
        public void ClearSignificanceTargets()
        {
            if (significancetestTargets != null) significancetestTargets.Clear();
            significancetestcharacters = null;
        }

        /// <summary>
        /// 項目間検定結果対象選択肢コレクションの複製を、他のDataWithMarking構造体のメンバに設定するメソッド
        /// <note>選択肢情報も複製される</note>
        /// </summary>
        /// <param name="other">複製先のDataWithMarking構造体</param>
        public void CloneSignificanceSectorNumbers(ref DataWithMarking other)
        {
            other.ClearSignificanceTargets();
            if (!SettedSectorInformation) return;
            if (!other.SettedSectorInformation)
            {
                other.significancetestTargets = new List<SignificanceTestTarget>();
            }
            other.sectornumber = sectornumber;
            other.sectorscount = sectorscount;
            for (int i = 0; i < significancetestTargets.Count; ++i)
            {
                other.significancetestTargets.Add(significancetestTargets[i]);
            }
        }

        /// <summary>
        /// 項目間検定結果を表す文字列を返すメソッド
        /// </summary>
        public string SignificanceCharacters()
        {
            if (significancetestcharacters != null) return significancetestcharacters;
            if (significancetestTargets == null) return null;
            // Comparison<SignificanceTestTarget> comparison = (x, y) => x.SectorNubmer.CompareTo(y.SectorNubmer);
            Comparison<SignificanceTestTarget> comparison = (x, y) => x.Order.CompareTo(y.Order);
            significancetestTargets.Sort(comparison);
            System.Text.StringBuilder resultBuffer = new System.Text.StringBuilder();
            for (int i = 0; i < significancetestTargets.Count; ++i)
            {
                // int idx = significancetestTargets[i].SectorNubmer;
                int idx = significancetestTargets[i].Order;
                string buf = SignificanceTestLetters.Character(idx);
                if (buf != null)
                {
                    if (significancetestTargets[i].Level == 0)
                    {
                        resultBuffer.Append(buf);
                    }
                    else
                    {
                        resultBuffer.Append(buf.ToLower());
                    }
                }
            }
            significancetestcharacters = resultBuffer.ToString();
            return significancetestcharacters;
        }

        /// <summary>
        /// 項目間検定対象に新たな並び順を設定するメソッド
        /// </summary>
        /// <param name="newSortedSectorNumbers">新たな並び順で並んだ、選択肢番号からなるリストへの参照</param>
        public void SetNewOrder(List<int> newSortedSectorNumbers)
        {
            if (significancetestTargets == null || newSortedSectorNumbers == null || newSortedSectorNumbers.Count == 0) return;
            Comparison<SignificanceTestTarget> comparison = (x, y) => x.SectorNubmer.CompareTo(y.SectorNubmer);
            significancetestTargets.Sort(comparison);
            SignificanceTestTargetComparer comparer = new SignificanceTestTargetComparer();
            for (int i = 0; i < newSortedSectorNumbers.Count; ++i)
            {
                int order = i + 1;
                int secNo = newSortedSectorNumbers[i];
                int idx = significancetestTargets.BinarySearch(new SignificanceTestTarget(secNo, 0), comparer);
                if (idx >= 0)
                {
                    SignificanceTestTarget tmp = significancetestTargets[idx];
                    tmp.Order = order;
                    significancetestTargets[idx] = tmp;
                }

            }
        }

        /// <summary>
        /// IComparable.CompareToメソッドの実装
        /// </summary>
        /// <param name="obj">比較相手のDataWithMarking構造体</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            DataWithMarking other = (DataWithMarking)obj;
            // return this.NumValue.CompareTo(other.NumValue);
            return other.NumValue.CompareTo(this.NumValue);
        }
    }
    #endregion

    #region NumericData構造体
    /// <summary>
    /// 数値データ情報を保持する構造体
    /// </summary>
    [ComVisible(false)]
    public struct NumericData : IComparable
    {
        private double value;
        private double wb;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">データ値</param>
        /// <param name="weightback">ウエイトバック値</param>
        public NumericData(double value, double weightback)
        {
            this.value = value;
            wb = weightback;
        }

        /// <summary>
        /// データ値を返す読み取り専用プロパティ
        /// </summary>
        public double Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>
        /// ウエイトバック値を返す読み取り専用プロパティ
        /// </summary>
        public double WeightBack
        {
            get
            {
                return wb;
            }
        }

        /// <summary>
        /// CompareToメソッドの実装
        /// </summary>
        /// <param name="obj">比較相手のNumericData構造体</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            NumericData target = (NumericData)obj;
            return value.CompareTo(target.Value);
        }
    }
    #endregion

    #region GlobalTabulationクラス

    /// <summary>
    /// 集計表の作成に必要な共有メソッドをまとめた静的クラス
    /// </summary>
    [ComVisible(false), Guid("A6DFCFEC-B3FD-4d08-8885-1755B4295B77")]
    public static class GlobalTabulation
    {
        #region メンバ定数
        /// <summary>MAデータの配列要素1つで扱う最大選択肢数</summary>
        public const int SECTORS_COUNT_PER_4BITE = 30;
        /// <summary>
        /// 全体との差の色付けの水準のパーセンテージの最大値
        /// </summary>
        public static readonly int COLORINGPERCENT_MAX = int.Parse(GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportHatchingPercentUpperLimitIndex));
        /// <summary>
        /// 全体との差の色付けの水準のパーセンテージの最小値
        /// </summary>
        public static readonly int COLORINGPERCENT_MIN = int.Parse(GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportHatchingPercentLowerLimitIndex));
        /// <summary>
        /// 全体との差の色付けの水準1のパーセンテージの既定値
        /// </summary>
        public const int COLORING_LEVEL1_DEFAULT = 5;
        /// <summary>
        /// 全体との差の色付けの水準2のパーセンテージの既定値
        /// </summary>
        public const int COLORING_LEVEL2_DEFAULT = 10;

        /// <summary>
        /// 加重平均の表記をリソースから取得するキー
        /// </summary>
        public const string WEIGHT_AVERAGE_KEY = "WeightAverage";
        /// <summary>
        /// 母数の表記をリソースから取得するキー
        /// </summary>
        public const string PARAMETER_KEY = "Parameter";

        #endregion

        #region フィルタリング関連
        /// <alias>Filtering001</alias>
        /// <summary>
        /// <para>エイリアス:Filtering001</para>
        /// 複数(1つ以上)の選択肢番号を条件値とするフィルタリングを行う<br />
        /// 以下の質問タイプ、演算子で使用する
        /// <list type="table">
        /// <listheader>
        /// <term>質問タイプ</term>
        /// <description>演算子</description>
        /// </listheader>
        /// <item>
        /// <term>SA</term>
        /// <description>～のいずれかと等しい, ～のいずれとも等しくない</description>
        /// </item>
        /// <item>
        /// <term>MA</term>
        /// <description>～と等しい, ～と等しくない, ～のすべてを含む, ～のいずれかを含む, ～のいずれかを含み～にないものを含まない, ～のいずれも含まない</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaSectorsList">条件値(選択肢番号)を昇順に格納した配列</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Operator">Operator列挙型</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, ref int[] CriteriaSectorsList, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag, Operator ope, System.Data.DataTable dt = null)
        {
            // dataがnullの場合処理しない
            if (data == null) return;
            // SAでもMAでもなければ処理しない
            QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA);
            if ((int)qType == 0) return;
            // FilteringFlagがnullの場合、新たに配列生成

            List<Data> criteriavaluedata = null;//trial
            if (Criteria.isvariable)//trial
            {
                if (dt != null)
                {
                    QCWebException exception = null;
                    //  QuestionType questiontype = DBHelper.GetAnswertype(CriteriaSector, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(CriteriaSector, con) == "SA" ? QuestionType.SA : DBHelper.GetAnswertype(CriteriaSector, con) == "N" ? QuestionType.N : QuestionType.FA);
                    string colName = CriteriaSectorsList[0] == 0 ? "sample_id" : "q_" + CriteriaSectorsList[0].ToString();
                    criteriavaluedata = ReadTextFile.ReadDataTable(dt.DefaultView.ToTable(false, colName), Criteria.variabletype, null, out exception);
                }

            }

            if (FilteringFlag == null) FilteringFlag = new bool[data.Count];
            switch (qType)
            {
                case QuestionType.SA:   // SAの場合
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Anyone:
                        case CriteriaOperator.NotEqualAnyone:
                            break;
                        default:    // このメソッドで扱わない絞り込み演算子の場合は処理しない
                            return;
                    }
                    for (int i = 0; i < data.Count; ++i)
                    {
                        SAData sadata = data[i] as SAData;
                        // 削除データの場合
                        if (data[i].IsDeleted)
                        {
                            FilteringFlag[i] = false;
                            continue;
                        }
                        // 不要な評価はしない
                        if (FilteringFlag[i] ^ ope != Operator.opOr) continue;
                        // 非削除通常データの場合
                        if (data[i].DataType == DataType.NormalData)
                        {
                            /*
                            // バイナリサーチ (二分探索)
                            if (criteriaOperator == CriteriaOperator.Anyone)
                            {
                                FilteringFlag[i] = Array.BinarySearch(CriteriaValueList, sadata.Value) >= 0;
                            }
                            else
                            {
                                FilteringFlag[i] = Array.BinarySearch(CriteriaValueList, sadata.Value) < 0;
                            }
                            */
                            FilteringFlag[i] = sadata.IsAnyOne(CriteriaSectorsList);
                            if (criteriaOperator == CriteriaOperator.NotEqualAnyone)
                            {
                                FilteringFlag[i] = !FilteringFlag[i];
                            }
                            continue;
                        }
                        // 非削除の無回答または非該当の場合 (必要時のみ評価)
                        FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqualAnyone;
                    }
                    break;
                case QuestionType.MA:   // MAの場合
                    switch (criteriaOperator)
                    {
#if AFTER_2ND_PHASE
                        case CriteriaOperator.Equal:
                        case CriteriaOperator.NotEqual:
                        case CriteriaOperator.IncludeAll:
#endif
                        case CriteriaOperator.IncludeAnyone:
#if AFTER_2ND_PHASE
                        case CriteriaOperator.IncludeAnyoneAndNotIncludeUnList:
#endif
                        case CriteriaOperator.NotIncludeAnyone:
                            break;
                        default:    // このメソッドで扱わない絞り込み演算子の場合は処理しない
                            return;
                    }
                    int[] CriteriaValueList = MAData.GetCriteriaValueList(CriteriaSectorsList);
                    for (int i = 0; i < data.Count; ++i)
                    {
                        // 削除データの場合
                        if (data[i].IsDeleted)
                        {
                            FilteringFlag[i] = false;
                            continue;
                        }
                        // 不要な評価はしない
                        if (FilteringFlag[i] ^ ope != Operator.opOr) continue;
                        MAData madata = data[i] as MAData;

                        //need to check if it is tryparsable,else  continue;
                        if (Criteria.isvariable)//trial
                        {
                            int returnvalue;
                            if (criteriavaluedata[i].IsIV == true || criteriavaluedata[i].IsNA == true)//|| data[i].IsNA|| data[i].IsIV
                            {
                                DataType criteriaDataType = DataType.NAData;
                                string datavalue = string.Empty;
                                string criteriavalue = string.Empty;
                                if (criteriavaluedata[i].IsIV == true)
                                {
                                    criteriavalue = "*";
                                    criteriaDataType = DataType.IVData;
                                }
                                if (data[i].IsNA == true)
                                {
                                    datavalue = string.Empty;
                                }
                                else if (data[i].IsIV == true)
                                {
                                    datavalue = "*";
                                }
                                else if (data[i] is MAData)
                                {
                                    // datavalue = Convert.ToString(((madata)criteriavaluedata[i]).Value);
                                }

                                switch (criteriaOperator)
                                {
                                    case CriteriaOperator.IncludeAnyone:
                                        FilteringFlag[i] = madata.IsAnyOne(criteriaDataType);
                                        break;
                                    case CriteriaOperator.NotIncludeAnyone:
                                        FilteringFlag[i] = !madata.IsAnyOne(criteriaDataType);
                                        break;
                                        //case CriteriaOperator.IncludeAnyone:
                                        //case CriteriaOperator.NotIncludeAnyone:

                                        //    FilteringFlag[i] = madata.IncludeAnyone(CriteriaValueList);
                                        //    if (criteriaOperator == CriteriaOperator.NotIncludeAnyone)
                                        //    {
                                        //        FilteringFlag[i] = !FilteringFlag[i];
                                        //    }
                                        //    break;
                                }
                                continue;
                            }

                        }//end trial






                        // 非削除通常データの場合
                        if (data[i].DataType == DataType.NormalData)
                        {
                            if (Criteria.isvariable)//trial
                            {
                                int returnvalue;
                                if (criteriavaluedata[i].GetType() == typeof(MAData))
                                {
                                    MAData criteriavaluemadata = (criteriavaluedata[i]) as MAData;
                                    CriteriaValueList[0] = Convert.ToInt32(criteriavaluemadata.Value(0));//Convert.ToInt32(Convert.ToString(criteriavaluedata[i]), 2);
                                }

                            }//end trial
                            switch (criteriaOperator)
                            {
#if AFTER_2ND_PHASE
                                // Equal(完全一致)は、少しでも違えばフラグを降ろす
                                // NotEqual(完全一致ではない)は、その逆
                                case CriteriaOperator.Equal:
                                case CriteriaOperator.NotEqual:
                                    /*
                                    FilteringFlag[i] = criteriaOperator == CriteriaOperator.Equal;
                                    for (int j = 0; j < madata.ValueSize; ++j)
                                    {
                                        if (madata.Value(j) != CriteriaValueList[j])
                                        {
                                            FilteringFlag[i] = !FilteringFlag[i];
                                            break;
                                        }
                                    }
                                    */
                                    FilteringFlag[i] = madata.Equals(CriteriaValueList);
                                    if (criteriaOperator == CriteriaOperator.NotEqual)
                                    {
                                        FilteringFlag[i] = !FilteringFlag[i];
                                    }
                                    break;
                                // IncludeAll(すべてを含む)は、少しでも漏れがあればフラグを降ろす
                                case CriteriaOperator.IncludeAll:
                                    /*
                                    FilteringFlag[i] = true;
                                    for (int j = 0; j < madata.ValueSize; ++j)
                                    {
                                        if ((madata.Value(j) & CriteriaValueList[j]) != CriteriaValueList[j])
                                        {
                                            FilteringFlag[i] = false;
                                            break;
                                        }
                                    }
                                    */
                                    FilteringFlag[i] = madata.IncludeAll(CriteriaValueList);
                                    break;
                                // IncludeAnyone(いずれかを含む)は、一つでも見つかればフラグを立てる
                                // NotIncludeAnyone(いずれも含まない)は、その逆
#endif
                                case CriteriaOperator.IncludeAnyone:
                                case CriteriaOperator.NotIncludeAnyone:
                                    /*
                                    FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotIncludeAnyone;
                                    for (int j = 0; j < madata.ValueSize; ++j)
                                    {
                                        if ((madata.Value(j) & CriteriaValueList[j]) != 0)
                                        {
                                            FilteringFlag[i] = !FilteringFlag[i];
                                            break;
                                        }
                                    }
                                    */
                                    FilteringFlag[i] = madata.IncludeAnyone(CriteriaValueList);
                                    if (criteriaOperator == CriteriaOperator.NotIncludeAnyone)
                                    {
                                        FilteringFlag[i] = !FilteringFlag[i];
                                    }
                                    break;
#if AFTER_2ND_PHASE
                                // IncludeAnyoneAndNotIncludeUnList(いずれかを含み、リスト以外のものは含まない)は、
                                // 一つでも見つかったらいったんフラグを立て、一つでもリスト以外のものが含まれていたらフラグを降ろす
                                case CriteriaOperator.IncludeAnyoneAndNotIncludeUnList:
                                    /*
                                    FilteringFlag[i] = false;
                                    for (int j = 0; j < madata.ValueSize; ++j)
                                    {
                                        if ((madata.Value(j) & CriteriaValueList[j]) != 0)
                                        {
                                            FilteringFlag[i] = true;
                                            for (int k = 0; k < madata.ValueSize; ++k)
                                            {
                                                if ((madata.Value(j) & ~CriteriaValueList[j]) != 0)
                                                {
                                                    FilteringFlag[i] = false;
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                    */
                                    FilteringFlag[i] = madata.IncludeAnyone(CriteriaValueList) && madata.NotIncludeUnList(CriteriaValueList);
                                    break;
#endif
                                default:
                                    return;
                            }
                            continue;
                        }
                        // 非削除の無回答または非該当の場合
                        FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual || criteriaOperator == CriteriaOperator.NotIncludeAnyone;
                    }
                    break;
                default:
                    return;
            }
        }

        /// <alias>Filtering002</alias>
        /// <summary>
        /// <para>エイリアス:Filtering002</para>
        /// 複数(1つ以上)の選択肢番号を条件値とするフィルタリングを行う<br />
        /// 引数opeにOperator.opOrを指定してFiltering001に仲介する
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaSectorsList">条件値(選択肢番号)を昇順に格納した配列</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GlobalTabulation.Filtering(System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},Macromill.QCWeb.Tabulation.QuestionType,System.Int32[]@,Macromill.QCWeb.Tabulation.CriteriaOperator,System.Boolean[]@,Macromill.QCWeb.Tabulation.Operator)">Filtering001</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, ref int[] CriteriaSectorsList, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag)
        {
            Filtering(data, questionType, ref CriteriaSectorsList, criteriaOperator, ref FilteringFlag, Operator.opOr);
        }

        /// <alias>Filtering003</alias>
        /// <summary>
        /// <para>エイリアス:Filtering003</para>
        /// 1つの選択肢番号を条件値とするフィルタリングを行う<br />
        /// 以下の質問タイプ、演算子で使用する
        /// <list type="table">
        /// <listheader>
        /// <term>質問タイプ</term>
        /// <description>演算子</description>
        /// </listheader>
        /// <item>
        /// <term>SA</term>
        /// <description>～と等しい, ～と等しくない, ～より大きい, ～以上, ～より小さい, ～以下</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaSector">条件値(選択肢番号)</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Operator">Operator列挙型</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, int CriteriaSector, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag, Operator ope, System.Data.DataTable dt = null)
        {
            // dataがnullの場合処理しない
            if (data == null) return;
            // SAでなければ処理しない
            QuestionType qType = questionType & QuestionType.SA;
            if (qType != QuestionType.SA) return;
            switch (criteriaOperator)
            {
                case CriteriaOperator.Equal:
                case CriteriaOperator.NotEqual:
                case CriteriaOperator.Greater:
                case CriteriaOperator.GreaterEqual:
                case CriteriaOperator.Less:
                case CriteriaOperator.LessEqual:
                    break;
                default:    // このメソッドで扱わない絞り込み演算子の場合は処理しない
                    return;
            }
            // FilteringFlagがnullの場合、新たに配列生成
            if (FilteringFlag == null) FilteringFlag = new bool[data.Count];
            List<Data> criteriavaluedata = null;//trial
            if (Criteria.isvariable)//trial
            {
                if (dt != null)
                {
                    QCWebException exception = null;
                    //  QuestionType questiontype = DBHelper.GetAnswertype(CriteriaSector, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(CriteriaSector, con) == "SA" ? QuestionType.SA : DBHelper.GetAnswertype(CriteriaSector, con) == "N" ? QuestionType.N : QuestionType.FA);
                    string colName = CriteriaSector == 0 ? "sample_id" : "q_" + CriteriaSector.ToString();
                    criteriavaluedata = ReadTextFile.ReadDataTable(dt.DefaultView.ToTable(false, colName), Criteria.variabletype, null, out exception);
                }

            }
            for (int i = 0; i < data.Count; ++i)
            {
                // 削除データの場合
                if (data[i].IsDeleted)
                {
                    FilteringFlag[i] = false;
                    continue;
                }
                // 不要な評価はしない
                if (FilteringFlag[i] ^ ope != Operator.opOr) continue;
                SAData sadata = data[i] as SAData;
                // 非削除通常データの場合

                //need to check if it is tryparsable,else  continue;
                if (Criteria.isvariable)//trial
                {
                    int returnvalue;
                    if (criteriavaluedata[i].IsIV == true || criteriavaluedata[i].IsNA == true)//|| data[i].IsNA|| data[i].IsIV
                    {
                        string datavalue = string.Empty;
                        string criteriavalue = string.Empty;
                        if (criteriavaluedata[i].IsIV == true)
                        {
                            criteriavalue = "*";
                        }
                        if (data[i].IsNA == true)
                        {
                            datavalue = string.Empty;
                        }
                        else if (data[i].IsIV == true)
                        {
                            datavalue = "*";
                        }
                        else if (data[i] is SAData)
                        {
                            datavalue = Convert.ToString(((SAData)data[i]).Value);
                        }

                        switch (criteriaOperator)
                        {
                            case CriteriaOperator.Equal:
                                FilteringFlag[i] = datavalue == criteriavalue;
                                break;
                            case CriteriaOperator.NotEqual:
                                FilteringFlag[i] = datavalue != criteriavalue;
                                break;

                            //aditional for multiple criteria issue -179884
                            case CriteriaOperator.Greater:

                                FilteringFlag[i] = false;
                                break;
                            case CriteriaOperator.GreaterEqual:

                                FilteringFlag[i] = false;
                                break;
                            case CriteriaOperator.Less:

                                FilteringFlag[i] = false;
                                break;
                            case CriteriaOperator.LessEqual:

                                FilteringFlag[i] = false;
                                break;
                        }
                        continue;
                    }
                    //else if (criteriavaluedata[i].GetType() == typeof(NData))
                    //{
                    //    if (!int.TryParse(Convert.ToString((criteriavaluedata[i] as NData).Value), out returnvalue))
                    //    {
                    //        //FilteringFlag[i] = false;
                    //        FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual;
                    //        continue;
                    //    }
                    //}
                    else if (criteriavaluedata[i].GetType() == typeof(FAData))
                    {
                        // CriteriaSector = int.Parse((criteriavaluedata[i] as FAData).Value);
                        if (!int.TryParse((criteriavaluedata[i] as FAData).Value, out returnvalue))
                        {
                            //FilteringFlag[i] = false;
                            FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual;
                            continue;
                        }
                    }
                }//end trial

                if (data[i].DataType == DataType.NormalData)
                {
                    if (Criteria.isvariable)//trial
                    {
                        int returnvalue;
                        if (criteriavaluedata[i].GetType() == typeof(SAData))
                        {
                            CriteriaSector = (criteriavaluedata[i] as SAData).Value;
                        }
                        else if (criteriavaluedata[i].GetType() == typeof(NData))//issue decimal neg takes as range
                        {
                            //  CriteriaSector = int.Parse(Convert.ToString((criteriavaluedata[i] as NData).Value));
                        }
                        else if (criteriavaluedata[i].GetType() == typeof(FAData))
                        {
                            CriteriaSector = int.Parse((criteriavaluedata[i] as FAData).Value);
                        }
                    }//end trial
                    if (criteriavaluedata == null || criteriavaluedata[i].GetType() != typeof(NData))
                    {
                        switch (criteriaOperator)
                        {
                            case CriteriaOperator.Equal:
                            case CriteriaOperator.NotEqual:
                                // Equal(一致)かNotEqual(不一致)かは、単純な等価比較
                                /*
                                FilteringFlag[i] = sadata.Value == CriteriaSector ^ criteriaOperator == CriteriaOperator.NotEqual;
                                */
                                FilteringFlag[i] = sadata.Equals(CriteriaSector);
                                if (criteriaOperator == CriteriaOperator.NotEqual)
                                {
                                    FilteringFlag[i] = !FilteringFlag[i];
                                }
                                continue;
                            case CriteriaOperator.Greater:
                                /*
                                FilteringFlag[i] = sadata.Value > CriteriaSector;
                                */
                                FilteringFlag[i] = sadata.IsGreater(CriteriaSector);
                                break;
                            case CriteriaOperator.GreaterEqual:
                                /*
                                FilteringFlag[i] = sadata.Value >= CriteriaSector;
                                */
                                FilteringFlag[i] = sadata.IsGreaterEqual(CriteriaSector);
                                break;
                            case CriteriaOperator.Less:
                                /*
                                FilteringFlag[i] = sadata.Value < CriteriaSector;
                                */
                                FilteringFlag[i] = sadata.IsLess(CriteriaSector);
                                break;
                            case CriteriaOperator.LessEqual:
                                /*
                                FilteringFlag[i] = sadata.Value <= CriteriaSector;
                                */
                                FilteringFlag[i] = sadata.IsLessEqual(CriteriaSector);
                                break;
                        }
                    }
                    else
                    {
                        switch (criteriaOperator)
                        {
                            case CriteriaOperator.Equal:
                            case CriteriaOperator.NotEqual:
                                // Equal(一致)かNotEqual(不一致)かは、単純な等価比較
                                /*
                                FilteringFlag[i] = sadata.Value == CriteriaSector ^ criteriaOperator == CriteriaOperator.NotEqual;
                                */
                                FilteringFlag[i] = sadata.Equals((criteriavaluedata[i] as NData));
                                if (criteriaOperator == CriteriaOperator.NotEqual)
                                {
                                    FilteringFlag[i] = !FilteringFlag[i];
                                }
                                continue;
                            case CriteriaOperator.Greater:
                                /*
                                FilteringFlag[i] = sadata.Value > CriteriaSector;
                                */
                                FilteringFlag[i] = sadata.IsGreater((criteriavaluedata[i] as NData));
                                break;
                            case CriteriaOperator.GreaterEqual:
                                /*
                                FilteringFlag[i] = sadata.Value >= CriteriaSector;
                                */
                                FilteringFlag[i] = sadata.IsGreaterEqual((criteriavaluedata[i] as NData));// sadata.IsGreaterEqual(CriteriaSector);
                                break;
                            case CriteriaOperator.Less:
                                /*
                                FilteringFlag[i] = sadata.Value < CriteriaSector;
                                */
                                FilteringFlag[i] = sadata.IsLess((criteriavaluedata[i] as NData));// sadata.IsLess(CriteriaSector);
                                break;
                            case CriteriaOperator.LessEqual:
                                /*
                                FilteringFlag[i] = sadata.Value <= CriteriaSector;
                                */
                                FilteringFlag[i] = sadata.IsLessEqual((criteriavaluedata[i] as NData));// sadata.IsLessEqual(CriteriaSector);
                                break;
                        }
                    }
                    continue;
                }//logic here
                else if (data[i].DataType == DataType.IVData || data[i].DataType == DataType.NAData)
                {
                    string datavalue = string.Empty;
                    if (data[i].IsIV == true)
                    {
                        datavalue = "*";
                    }
                    else if (data is NData)
                    {
                        datavalue = Convert.ToString(((NData)criteriavaluedata[i]).Value);
                    }
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            /*
                            FilteringFlag[i] = ndata.Value == CriteriaValue;
                            */
                            FilteringFlag[i] = data[i].Equals(CriteriaSector);
                            break;
                        case CriteriaOperator.NotEqual:
                            /*
                            FilteringFlag[i] = ndata.Value != CriteriaValue;
                            */
                            FilteringFlag[i] = !data[i].Equals(CriteriaSector);
                            break;

                        //aditional for multiple criteria issue -179884
                        case CriteriaOperator.Greater:

                            FilteringFlag[i] = false;
                            break;
                        case CriteriaOperator.GreaterEqual:

                            FilteringFlag[i] = false;
                            break;
                        case CriteriaOperator.Less:

                            FilteringFlag[i] = false;
                            break;
                        case CriteriaOperator.LessEqual:

                            FilteringFlag[i] = false;
                            break;
                    }
                    continue;

                }


                // 非削除の無回答または非該当の場合
                FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual;
            }
        }

        /// <alias>Filtering004</alias>
        /// <summary>
        /// <para>エイリアス:Filtering004</para>
        /// 1つの選択肢番号を条件値とするフィルタリングを行う<br />
        /// 引数opeにOperator.opOrを指定してFiltering003に仲介する
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaSector">条件値(選択肢番号)</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GlobalTabulation.Filtering(System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},Macromill.QCWeb.Tabulation.QuestionType,System.Int32,Macromill.QCWeb.Tabulation.CriteriaOperator,System.Boolean[]@,Macromill.QCWeb.Tabulation.Operator)">Filtering003</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, int CriteriaSector, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag)
        {
            Filtering(data, questionType, CriteriaSector, criteriaOperator, ref FilteringFlag, Operator.opOr);
        }

#if AFTER_2ND_PHASE
        /// <alias>Filtering005</alias>
        /// <summary>
        /// <para>エイリアス:Filtering005</para>
        /// 複数(1つ以上)の文字列を条件値とするフィルタリングを行う<br />
        /// 以下の質問タイプ、演算子で使用する
        /// <list type="table">
        /// <listheader>
        /// <term>質問タイプ</term>
        /// <description>演算子</description>
        /// </listheader>
        /// <item>
        /// <term>FA</term>
        /// <description>～のいずれかと等しい, ～のいずれとも等しくない</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaValueList">条件値(文字列)を格納した配列</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Operator">Operator列挙型</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, ref string[] CriteriaValueList, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag, Operator ope)
        {
            // dataがnullの場合処理しない
            if (data == null) return;
            // FAでなければ処理しない
            QuestionType qType = questionType & QuestionType.FA;
            if (qType != QuestionType.FA) return;
            switch (criteriaOperator)
            {
                case CriteriaOperator.Anyone:
                case CriteriaOperator.NotEqualAnyone:
                    break;
                default:    // このメソッドで扱わない絞り込み演算子の場合は処理しない
                    return;
            }
            // FilteringFlagがnullの場合、新たに配列生成
            if (FilteringFlag == null) FilteringFlag = new bool[data.Count];
            for (int i = 0; i < data.Count; ++i)
            {
                FAData fadata = data[i] as FAData;
                // 削除データの場合
                if (data[i].IsDeleted)
                {
                    FilteringFlag[i] = false;
                    continue;
                }
                // 不要な評価はしない
                if (!(FilteringFlag[i] ^ ope == Operator.opOr)) continue;
                // 非削除通常データの場合 (現状FAのデータはすべて通常データ)
                if (data[i].DataType == DataType.NormalData)
                {
                    // 逐次探索
                    /*
                    FilteringFlag[i] = CriteriaValueList.Contains(fadata.Value);
                    */
                    FilteringFlag[i] = fadata.IsAnyOne(CriteriaValueList);
                    if (criteriaOperator == CriteriaOperator.NotEqualAnyone)
                    {
                        FilteringFlag[i] = !FilteringFlag[i];
                    }
                    continue;
                }
                // 非削除の無回答または非該当の場合
                // (現状では絶対に走らないが将来的な拡張のための保険と、他同類メソッドとの見通しを揃えるため)
                FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqualAnyone;
            }
        }
#endif

#if AFTER_2ND_PHASE
        /// <alias>Filtering006</alias>
        /// <summary>
        /// <para>エイリアス:Filtering006</para>
        /// 複数(1つ以上)の文字列を条件値とするフィルタリングを行う<br />
        /// 引数opeにOperator.opOrを指定してFiltering005に仲介する
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaValueList">条件値(文字列)を格納した配列</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GlobalTabulation.Filtering(System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},Macromill.QCWeb.Tabulation.QuestionType,System.String[]@,Macromill.QCWeb.Tabulation.CriteriaOperator,System.Boolean[]@,Macromill.QCWeb.Tabulation.Operator)">Filtering005</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, ref string[] CriteriaValueList, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag)
        {
            Filtering(data, questionType, ref CriteriaValueList, criteriaOperator, ref FilteringFlag, Operator.opOr);
        }
#endif

        /// <alias>Filtering007</alias>
        /// <summary>
        /// <para>エイリアス:Filtering007</para>
        /// 1つの文字列を条件値とするフィルタリングを行う<br />
        /// 以下の質問タイプ、演算子で使用する
        /// <list type="table">
        /// <listheader>
        /// <term>質問タイプ</term>
        /// <description>演算子</description>
        /// </listheader>
        /// <item>
        /// <term>FA</term>
        /// <description>～と等しい, ～と等しくない, ～から始まる, ～から始まらない, ～で終わる, ～で終わらない, ～を含む, ～を含まない, ～とパターンが一致する, ～とパターンが一致しない</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaValue">条件値(文字列)</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Operator">Operator列挙型</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, string CriteriaValue, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag, Operator ope, System.Data.DataTable dt = null)
        {
            // dataがnullの場合処理しない
            if (data == null) return;
            // FAでなければ処理しない
            QuestionType qType = questionType & QuestionType.FA;
            if (qType != QuestionType.FA) return;

            //trial criteria value as variable [Redmine id : 178707] -
            CriteriaValue = Regex.Unescape(CriteriaValue);//#206108
            string criteriavalue = CriteriaValue;
            Criteria.isvariable = false;
            //  List<Data> criteriavaluedata = null;//trial           
            if (criteriavalue.StartsWith("[") && criteriavalue.EndsWith("]"))//needed regex for characters with digits enclosed in [ ] with spl symbols like _ -
            {
                if (criteriavalue.Length > 1)
                {
                    criteriavalue = criteriavalue.Remove(0, 1);
                    criteriavalue = criteriavalue.Remove(criteriavalue.Length - 1, 1);
                    string[] criteriavaluearray = criteriavalue.Split('@');
                    if (criteriavaluearray.Length == 3)
                    {
                        int result;
                        if (int.TryParse(criteriavaluearray[1], out result))
                        {
                            CriteriaValue = criteriavaluearray[1];// criteriaValueDescription = criteriavaluearray[1];
                            Criteria.isvariable = true;
                            Criteria.variabletype = (criteriavaluearray[2] == "N") ? QuestionType.N : (criteriavaluearray[2] == "SA") ? QuestionType.SA : QuestionType.FA;
                        }
                    }
                }
            }
            //end trial

            string CriteriaPattern = null;
            List<Data> criteriavaluedata = null;//trial
            if (Criteria.isvariable)//trial
            {
                if (dt != null)
                {
                    QCWebException exception = null;
                    //  QuestionType questiontype = DBHelper.GetAnswertype(CriteriaSector, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(CriteriaSector, con) == "SA" ? QuestionType.SA : DBHelper.GetAnswertype(CriteriaSector, con) == "N" ? QuestionType.N : QuestionType.FA);
                    string colName = CriteriaValue == "0" ? "sample_id" : "q_" + CriteriaValue.ToString();
                    criteriavaluedata = ReadTextFile.ReadDataTable(dt.DefaultView.ToTable(false, colName), Criteria.variabletype, null, out exception);
                }

            }
            switch (criteriaOperator)
            {
#if AFTER_2ND_PHASE
                case CriteriaOperator.Equal:
                case CriteriaOperator.NotEqual:
                    break;
#endif
                case CriteriaOperator.Equal:
                case CriteriaOperator.NotEqual:
                    CriteriaValue = CriteriaValue;//Unescape removed. This fix as per Redmine Id:232757, 233829
                    break;
                case CriteriaOperator.PatternMatching:
                case CriteriaOperator.PatternUnmatching:
                    // 正規表現パターン文字列への変換
                    /*
                    System.Text.RegularExpressions.Regex escapedregex
                                = new System.Text.RegularExpressions.Regex(@"\[(\!|)([^\]]+)\]");
                    string[] splitBuf = null;
                    System.Text.RegularExpressions.MatchCollection escapedwcmatches = null;
                    int mCnt = 0;
                    if (escapedregex.IsMatch(CriteriaValue))
                    {
                        escapedwcmatches = escapedregex.Matches(CriteriaValue);
                        splitBuf = escapedregex.Split(CriteriaValue);
                        mCnt = escapedwcmatches.Count;
                    }
                    else
                    {
                        splitBuf = new string[1];
                        splitBuf[0] = CriteriaValue;
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
                    CriteriaPattern = newBuf.ToString();
                    */
                    CriteriaPattern = FAData.ConvertToRegExpPattern(CriteriaValue);//Unescape removed. This fix as per Redmine Id:232757, 233829
                    break;
                /*
                default:
                    // 絞り込み演算子に応じたパターン文字列の生成
                    string tmp2 = System.Text.RegularExpressions.Regex.Escape(CriteriaValue);
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.BeginAt:
                        case CriteriaOperator.NotBeginAt:
                            CriteriaPattern = "^" + tmp2;
                            break;
                        case CriteriaOperator.EndAt:
                        case CriteriaOperator.NotEndAt:
                            CriteriaPattern = tmp2 + "$";
                            break;
                        case CriteriaOperator.Include:
                        case CriteriaOperator.NotInclude:
                            CriteriaPattern = tmp2;
                            break;
                        default:    // このメソッドで扱わない絞り込み演算子の場合は処理しない
                            return;
                    }
                    break;
                */
#if AFTER_2ND_PHASE
                case CriteriaOperator.BeginAt:
                case CriteriaOperator.NotBeginAt:
                case CriteriaOperator.EndAt:
                case CriteriaOperator.NotEndAt:
                case CriteriaOperator.Include:
                case CriteriaOperator.NotInclude:
                    break;
#endif
                default:    // このメソッドで扱わない絞り込み演算子の場合は処理しない
                    return;
            }
            // FilteringFlagがnullの場合、新たに配列生成
            if (FilteringFlag == null) FilteringFlag = new bool[data.Count];
            for (int i = 0; i < data.Count; ++i)
            {
                FAData fadata = data[i] as FAData;
                // 削除データの場合
                if (data[i].IsDeleted)
                {
                    FilteringFlag[i] = false;
                    continue;
                }
                // 不要な評価はしない
                if (!(FilteringFlag[i] ^ ope == Operator.opOr)) continue;
                // 非削除通常データの場合
                //need to check if it is tryparsable,else  continue;
                if (Criteria.isvariable)//trial
                {
                    int returnvalue;
                    if (criteriavaluedata[i].IsIV == true || criteriavaluedata[i].IsNA == true)//|| data[i].IsNA|| data[i].IsIV
                    {
                        CriteriaValue = string.Empty; //Cleared the previously stored value. This fix as per Remine Id:233830
                        string datavalue = string.Empty;
                        string criteriavaluetemp = string.Empty;
                        if (criteriavaluedata[i].IsIV == true)
                        {
                            criteriavaluetemp = "*";
                        }
                        //if (data[i].IsNA == true)
                        //{
                        //    datavalue = string.Empty;
                        //}
                        //else if (data[i].IsIV == true)
                        //{
                        //    datavalue = "*";
                        //}
                        //else if (data is FAData)
                        //{
                        //    datavalue = Convert.ToString(((FAData)criteriavaluedata[i]).Value);
                        //}
                        if (string.IsNullOrEmpty(fadata.Value))
                        {
                            datavalue = string.Empty;
                        }
                        else
                        {
                            datavalue = fadata.Value;
                        }
                        switch (criteriaOperator)
                        {
                            //case CriteriaOperator.Equal:
                            //    FilteringFlag[i] = datavalue == criteriavaluetemp;
                            //    break;
                            //case CriteriaOperator.NotEqual:
                            //    FilteringFlag[i] = datavalue != criteriavaluetemp;
                            //    break;
                            case CriteriaOperator.Equal: // redmine 168071
                                FilteringFlag[i] = datavalue.Equals(criteriavaluetemp);
                                break;
                            case CriteriaOperator.NotEqual:
                                // Equal(一致)かNotEqual(不一致)かは、単純な等価比較                              
                                FilteringFlag[i] = !datavalue.Equals(CriteriaValue);//[Redmine id : 178707] -
                                break;
                        }
                        continue;
                    }
                    //else if (criteriavaluedata[i].GetType() == typeof(NData))
                    //{
                    //    if (!int.TryParse(Convert.ToString((criteriavaluedata[i] as NData).Value), out returnvalue))
                    //    {
                    //        FilteringFlag[i] = false;
                    //        continue;
                    //    }
                    //}
                    //else if (criteriavaluedata[i].GetType() == typeof(SAData))
                    //{
                    //    // CriteriaSector = int.Parse((criteriavaluedata[i] as FAData).Value);
                    //    if (!int.TryParse((criteriavaluedata[i] as SAData).Value, out returnvalue))
                    //    {
                    //        FilteringFlag[i] = false;
                    //        continue;
                    //    }
                    //}
                }//end trial
                if (data[i].DataType == DataType.NormalData)
                {
                    if (Criteria.isvariable)//trial
                    {
                        int returnvalue;
                        if (criteriavaluedata[i].GetType() == typeof(SAData))
                        {
                            CriteriaValue = Convert.ToString((criteriavaluedata[i] as SAData).Value);
                        }
                        else if (criteriavaluedata[i].GetType() == typeof(NData))
                        {
                            CriteriaValue = Convert.ToString((criteriavaluedata[i] as NData).Value);
                        }
                        else if (criteriavaluedata[i].GetType() == typeof(FAData))
                        {
                            CriteriaValue = (criteriavaluedata[i] as FAData).Value;
                        }
                    }//end trial
                    switch (criteriaOperator)
                    {
#if AFTER_2ND_PHASE
                        /*
                        case CriteriaOperator.Equal:
                        case CriteriaOperator.NotEqual:
                            // Equal(一致)かNotEqual(不一致)かは、単純な等価比較
                            FilteringFlag[i] = fadata.Value.Equals(CriteriaValue);
                            break;
                        default:
                            // それ以外では、正規表現によるパターンマッチング
                            FilteringFlag[i] = System.Text.RegularExpressions.Regex.IsMatch(fadata.Value, CriteriaPattern);
                            break;
                        */
                        case CriteriaOperator.Equal:
                        case CriteriaOperator.NotEqual:
                            FilteringFlag[i] = fadata.Equals(CriteriaValue);
                            break;
                        case CriteriaOperator.BeginAt:
                        case CriteriaOperator.NotBeginAt:
                            FilteringFlag[i] = fadata.IsBeginAt(CriteriaValue);
                            break;
                        case CriteriaOperator.EndAt:
                        case CriteriaOperator.NotEndAt:
                            FilteringFlag[i] = fadata.IsEndAt(CriteriaValue);
                            break;
                        case CriteriaOperator.Include:
                        case CriteriaOperator.NotInclude:
                            FilteringFlag[i] = fadata.Include(CriteriaValue);
                            break;
#endif
                        case CriteriaOperator.PatternMatching:
                        case CriteriaOperator.PatternUnmatching:
                            FilteringFlag[i] = fadata.IsMatch(CriteriaPattern);
                            break;
                        case CriteriaOperator.Equal: // redmine 168071
                        case CriteriaOperator.NotEqual:
                            // Equal(一致)かNotEqual(不一致)かは、単純な等価比較
                            FilteringFlag[i] = fadata.Value.Equals(CriteriaValue);
                            break;
                    }
                    switch (criteriaOperator)
                    {
#if AFTER_2ND_PHASE
                        // 結果を反転させる絞り込み演算子の場合
                        case CriteriaOperator.NotEqual:
                        case CriteriaOperator.NotBeginAt:
                        case CriteriaOperator.NotEndAt:
                        case CriteriaOperator.NotInclude:
#endif
                        case CriteriaOperator.PatternUnmatching:
                        case CriteriaOperator.NotEqual:
                            FilteringFlag[i] = !FilteringFlag[i];
                            break;
                    }
                    continue;
                }
                else if (data[i].DataType == DataType.IVData || data[i].DataType == DataType.NAData)//[Redmine id : 178707] -
                {
                    string datavalue = string.Empty;
                    if (data[i].IsIV == true)
                    {
                        datavalue = "*";
                    }
                    if (Criteria.isvariable)//trial
                    {
                        int returnvalue;
                        if (criteriavaluedata[i].GetType() == typeof(SAData))
                        {
                            CriteriaValue = Convert.ToString((criteriavaluedata[i] as SAData).Value);
                        }
                        else if (criteriavaluedata[i].GetType() == typeof(NData))
                        {
                            CriteriaValue = Convert.ToString((criteriavaluedata[i] as NData).Value);
                        }
                        else if (criteriavaluedata[i].GetType() == typeof(FAData))
                        {
                            CriteriaValue = (criteriavaluedata[i] as FAData).Value;
                        }
                    }//end trial
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            /*
                            FilteringFlag[i] = ndata.Value == CriteriaValue;
                            */
                            FilteringFlag[i] = datavalue.Equals(CriteriaValue);
                            break;
                        case CriteriaOperator.NotEqual:
                            /*
                            FilteringFlag[i] = ndata.Value != CriteriaValue;
                            */
                            FilteringFlag[i] = !datavalue.Equals(CriteriaValue);
                            break;
                    }
                    continue;

                }
                // 非削除の無回答または非該当の場合(現状では、FAのデータに非該当はない)
                FilteringFlag[i] = false;
                switch (criteriaOperator)
                {
#if AFTER_2ND_PHASE
                    case CriteriaOperator.NotEqual:
                    case CriteriaOperator.NotBeginAt:
                    case CriteriaOperator.NotEndAt:
                    case CriteriaOperator.NotInclude:
#endif
                    case CriteriaOperator.PatternUnmatching:
                    case CriteriaOperator.NotEqual:
                        FilteringFlag[i] = !FilteringFlag[i];
                        break;
                }
            }
        }

        /// <alias>Filtering008</alias>
        /// <summary>
        /// <para>エイリアス:Filtering008</para>
        /// 1つの文字列を条件値とするフィルタリングを行う<br />
        /// 引数opeにOperator.opOrを指定してFiltering007に仲介する
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaValue">条件値(文字列)</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GlobalTabulation.Filtering(System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},Macromill.QCWeb.Tabulation.QuestionType,System.String,Macromill.QCWeb.Tabulation.CriteriaOperator,System.Boolean[]@,Macromill.QCWeb.Tabulation.Operator)">Filtering007</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, string CriteriaValue, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag)
        {
            Filtering(data, questionType, CriteriaValue, criteriaOperator, ref FilteringFlag, Operator.opOr);
        }

        /// <alias>Filtering009</alias>
        /// <summary>
        /// <para>エイリアス:Filtering009</para>
        /// 複数(1つ以上)の数値を条件値を条件とするフィルタリングを行う<br />
        /// 以下の質問タイプ、演算子で使用する
        /// <list type="table">
        /// <listheader>
        /// <term>質問タイプ</term>
        /// <description>演算子</description>
        /// </listheader>
        /// <item>
        /// <term>N</term>
        /// <description>～のいずれかと等しい, ～のいずれとも等しくない</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaValueList">条件値(数値)を格納した配列</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Operator">Operator列挙型</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, ref double[] CriteriaValueList, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag, Operator ope)
        {
            // dataがnullの場合処理しない
            if (data == null) return;
            // Nでなければ処理しない
            QuestionType qType = questionType & QuestionType.N;
            if (qType != QuestionType.N) return;
            switch (criteriaOperator)
            {
                case CriteriaOperator.Anyone:
                case CriteriaOperator.NotEqualAnyone:
                    break;
                default:    // このメソッドで扱わない絞り込み演算子の場合は処理しない
                    return;
            }
            // FilteringFlagがnullの場合、新たに配列生成
            if (FilteringFlag == null) FilteringFlag = new bool[data.Count];
            for (int i = 0; i < data.Count; ++i)
            {
                NData ndata = data[i] as NData;
                // 削除データの場合
                if (data[i].IsDeleted)
                {
                    FilteringFlag[i] = false;
                    continue;
                }
                // 不要な評価はしない
                if (!(FilteringFlag[i] ^ ope == Operator.opOr)) continue;
                // 非削除通常データの場合
                if (data[i].DataType == DataType.NormalData)
                {
                    // 逐次探索
                    /*
                    FilteringFlag[i] = CriteriaValueList.Contains(ndata.Value);
                    */
                    FilteringFlag[i] = ndata.IsAnyOne(CriteriaValueList);
                    if (criteriaOperator == CriteriaOperator.NotEqualAnyone)
                    {
                        FilteringFlag[i] = !FilteringFlag[i];
                    }
                    continue;
                }
                // 非削除の無回答または非該当の場合
                FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqualAnyone;
            }
        }

        /// <alias>Filtering010</alias>
        /// <summary>
        /// <para>エイリアス:Filtering010</para>
        /// 複数(1つ以上)の数値を条件値を条件とするフィルタリングを行う<br />
        /// 引数opeにOperator.opOrを指定してFiltering009に仲介する
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaValueList">条件値(数値)を格納した配列</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GlobalTabulation.Filtering(System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},Macromill.QCWeb.Tabulation.QuestionType,System.Double[]@,Macromill.QCWeb.Tabulation.CriteriaOperator,System.Boolean[]@,Macromill.QCWeb.Tabulation.Operator)">Filtering009</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, ref double[] CriteriaValueList, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag)
        {
            Filtering(data, questionType, ref CriteriaValueList, criteriaOperator, ref FilteringFlag, Operator.opOr);
        }

        /// <alias>Filtering011</alias>
        /// <summary>
        /// <para>エイリアス:Filtering011</para>
        /// 1つの数値を条件値とするフィルタリングを行う<br />
        /// 以下の質問タイプ、演算子で使用する
        /// <list type="table">
        /// <listheader>
        /// <term>質問タイプ</term>
        /// <description>演算子</description>
        /// </listheader>
        /// <item>
        /// <term>N</term>
        /// <description>～と等しい, ～と等しくない, ～より大きい, ～以上, ～より小さい, ～以下</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaValue">条件値(数値)</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Operator">Operator列挙型</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, double CriteriaValue, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag, Operator ope, System.Data.DataTable dt = null)
        {
            // dataがnullの場合処理しない
            if (data == null) return;
            // Nでなければ処理しない
            QuestionType qType = questionType & QuestionType.N;
            if (qType != QuestionType.N) return;
            switch (criteriaOperator)
            {
                case CriteriaOperator.Equal:
                case CriteriaOperator.NotEqual:
                case CriteriaOperator.Greater:
                case CriteriaOperator.GreaterEqual:
                case CriteriaOperator.Less:
                case CriteriaOperator.LessEqual:
                    break;
                default:    // このメソッドで扱わない絞り込み演算子の場合は処理しない
                    return;
            }
            // FilteringFlagがnullの場合、新たに配列生成
            if (FilteringFlag == null) FilteringFlag = new bool[data.Count];
            List<Data> criteriavaluedata = null;//trial
            if (Criteria.isvariable)//trial
            {
                if (dt != null)
                {
                    QCWebException exception = null;
                    //  QuestionType questiontype = DBHelper.GetAnswertype(CriteriaSector, con) == "MA" ? QuestionType.MA : (DBHelper.GetAnswertype(CriteriaSector, con) == "SA" ? QuestionType.SA : DBHelper.GetAnswertype(CriteriaSector, con) == "N" ? QuestionType.N : QuestionType.FA);
                    string colName = CriteriaValue == 0 ? "sample_id" : "q_" + CriteriaValue.ToString();
                    criteriavaluedata = ReadTextFile.ReadDataTable(dt.DefaultView.ToTable(false, colName), Criteria.variabletype, null, out exception);
                }

            }
            for (int i = 0; i < data.Count; ++i)
            {
                NData ndata = data[i] as NData;
                // 削除データの場合
                if (data[i].IsDeleted)
                {
                    FilteringFlag[i] = false;
                    continue;
                }
                // 不要な評価はしない
                if (!(FilteringFlag[i] ^ ope == Operator.opOr)) continue;
                //need to check if it is tryparsable,else  continue;
                if (Criteria.isvariable)//trial
                {
                    float returnvalue;
                    if (criteriavaluedata[i].IsIV == true || criteriavaluedata[i].IsNA == true)//|| data[i].IsNA|| data[i].IsIV
                    {
                        string datavalue = string.Empty;
                        string criteriavalue = string.Empty;
                        if (criteriavaluedata[i].IsIV == true)
                        {
                            criteriavalue = "*";
                        }
                        if (data[i].IsNA == true)
                        {
                            datavalue = string.Empty;
                        }
                        else if (data[i].IsIV == true)
                        {
                            datavalue = "*";
                        }
                        else if (data[i] is NData)
                        {
                            datavalue = Convert.ToString(((NData)data[i]).Value);
                        }

                        switch (criteriaOperator)
                        {
                            case CriteriaOperator.Equal:
                                FilteringFlag[i] = datavalue == criteriavalue;
                                break;
                            case CriteriaOperator.NotEqual:
                                FilteringFlag[i] = datavalue != criteriavalue;
                                break;
                            //
                            //aditional for multiple criteria issue -179884
                            case CriteriaOperator.Greater:

                                FilteringFlag[i] = false;
                                break;
                            case CriteriaOperator.GreaterEqual:

                                FilteringFlag[i] = false;
                                break;
                            case CriteriaOperator.Less:

                                FilteringFlag[i] = false;
                                break;
                            case CriteriaOperator.LessEqual:

                                FilteringFlag[i] = false;
                                break;
                                //
                        }
                        continue;
                    }
                    else if (criteriavaluedata[i].GetType() == typeof(SAData))
                    {
                        if (!float.TryParse(Convert.ToString((criteriavaluedata[i] as SAData).Value), out returnvalue))
                        {
                            // FilteringFlag[i] = false;
                            FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual;
                            continue;
                        }
                    }
                    else if (criteriavaluedata[i].GetType() == typeof(FAData))
                    {
                        // CriteriaSector = int.Parse((criteriavaluedata[i] as FAData).Value);
                        if (!float.TryParse((criteriavaluedata[i] as FAData).Value, out returnvalue))
                        {
                            //FilteringFlag[i] = false;
                            FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual;
                            continue;
                        }
                    }
                }//end trial
                // 非削除通常データの場合
                if (data[i].DataType == DataType.NormalData)
                {
                    if (Criteria.isvariable)//trial
                    {
                        int returnvalue;
                        if (criteriavaluedata[i].GetType() == typeof(SAData))
                        {
                            CriteriaValue = double.Parse(Convert.ToString((criteriavaluedata[i] as SAData).Value));
                        }
                        else if (criteriavaluedata[i].GetType() == typeof(NData))
                        {
                            CriteriaValue = double.Parse(Convert.ToString((criteriavaluedata[i] as NData).Value));
                        }
                        else if (criteriavaluedata[i].GetType() == typeof(FAData))
                        {
                            CriteriaValue = double
                                .Parse((criteriavaluedata[i] as FAData).Value);
                        }
                    }//end trial
                    // 絞り込み演算子に応じた比較演算
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            /*
                            FilteringFlag[i] = ndata.Value == CriteriaValue;
                            */
                            FilteringFlag[i] = ndata.Equals(CriteriaValue);
                            break;
                        case CriteriaOperator.NotEqual:
                            /*
                            FilteringFlag[i] = ndata.Value != CriteriaValue;
                            */
                            FilteringFlag[i] = !ndata.Equals(CriteriaValue);
                            break;
                        case CriteriaOperator.Greater:
                            /*
                            FilteringFlag[i] = ndata.Value > CriteriaValue;
                            */
                            FilteringFlag[i] = ndata.IsGreater(CriteriaValue);
                            break;
                        case CriteriaOperator.GreaterEqual:
                            /*
                            FilteringFlag[i] = ndata.Value >= CriteriaValue;
                            */
                            FilteringFlag[i] = ndata.IsGreaterEqual(CriteriaValue);
                            break;
                        case CriteriaOperator.Less:
                            /*
                            FilteringFlag[i] = ndata.Value < CriteriaValue;
                            */
                            FilteringFlag[i] = ndata.IsLess(CriteriaValue);
                            break;
                        case CriteriaOperator.LessEqual:
                            /*
                            FilteringFlag[i] = ndata.Value <= CriteriaValue;
                            */
                            FilteringFlag[i] = ndata.IsLessEqual(CriteriaValue);
                            break;
                    }
                    continue;
                }
                else if (data[i].DataType == DataType.IVData || data[i].DataType == DataType.NAData)
                {
                    string datavalue = string.Empty;
                    if (data[i].IsIV == true)
                    {
                        datavalue = "*";
                    }
                    else if (data is NData)
                    {
                        datavalue = Convert.ToString(((NData)criteriavaluedata[i]).Value);
                    }
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                            /*
                            FilteringFlag[i] = ndata.Value == CriteriaValue;
                            */
                            FilteringFlag[i] = datavalue.Equals(CriteriaValue);
                            break;
                        case CriteriaOperator.NotEqual:
                            /*
                            FilteringFlag[i] = ndata.Value != CriteriaValue;
                            */
                            FilteringFlag[i] = !datavalue.Equals(CriteriaValue);
                            break;
                        //aditional for multiple criteria issue -179884
                        case CriteriaOperator.Greater:

                            FilteringFlag[i] = false;
                            break;
                        case CriteriaOperator.GreaterEqual:

                            FilteringFlag[i] = false;
                            break;
                        case CriteriaOperator.Less:

                            FilteringFlag[i] = false;
                            break;
                        case CriteriaOperator.LessEqual:

                            FilteringFlag[i] = false;
                            break;
                    }
                    continue;

                }
                // 非削除の無回答または非該当の場合
                FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual;
            }
        }

        /// <alias>Filtering012</alias>
        /// <summary>
        /// <para>エイリアス:Filtering012</para>
        /// 1つの数値を条件値とするフィルタリングを行う<br />
        /// 引数opeにOperator.opOrを指定してFiltering011に仲介する
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaValue">条件値(数値)</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GlobalTabulation.Filtering(System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},Macromill.QCWeb.Tabulation.QuestionType,System.Double,Macromill.QCWeb.Tabulation.CriteriaOperator,System.Boolean[]@,Macromill.QCWeb.Tabulation.Operator)">Filtering011</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType, double CriteriaValue, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag)
        {
            Filtering(data, questionType, CriteriaValue, criteriaOperator, ref FilteringFlag, Operator.opOr);
        }

#if AFTER_2ND_PHASE
        /// <alias>Filtering013</alias>
        /// <summary>
        /// <para>エイリアス:Filtering013</para>
        /// 2つの条件を一度に処理するフィルタリングを行う<br />
        /// 以下の質問タイプ、演算子で使用する
        /// <list type="table">
        /// <listheader>
        /// <term>質問タイプ</term>
        /// <description>演算子</description>
        /// </listheader>
        /// <item>
        /// <term>N</term>
        /// <description>～以上…以下, ～以上…より小さい, ～より大きく…以下, ～より大きく…より小さい, ～以下…以上, ～以下…より大きい, ～より小さく…以上, ～より小さく…より大きい</description>
        /// </item>
        /// </list>
        /// <note>その他、引数の内容に応じて一部他オーバーロードメソッドへの仲介を行う</note>
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaValue1">1つ目の条件の条件値(数値)</param>
        /// <param name="criteriaOperator1">1つ目の条件の絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="CriteriaValue2">2つ目の条件の条件値(数値)</param>
        /// <param name="criteriaOperator2">2つ目の条件の絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Operator">Operator列挙型</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType
                            , double CriteriaValue1, CriteriaOperator criteriaOperator1
                            , double CriteriaValue2, CriteriaOperator criteriaOperator2
                            , ref bool[] FilteringFlag, Operator ope)
        {
            // dataがnullの場合処理しない
            if (data == null) return;
            // Nでなければ処理しない
            QuestionType qType = questionType & QuestionType.N;
            if (qType != QuestionType.N) return;
            switch (criteriaOperator1)
            {
                case CriteriaOperator.Equal:
                case CriteriaOperator.NotEqual:
                    // EqualまたはNotEqualでは、2つ目の条件は無視
                    // 1つ目の条件を引数としてFiltering011に仲介
                    Filtering(data, questionType, CriteriaValue1, criteriaOperator1, ref FilteringFlag, ope);
                    return;
                case CriteriaOperator.Greater:
                case CriteriaOperator.GreaterEqual:
                    switch (criteriaOperator2)
                    {
                        case CriteriaOperator.Less:
                        case CriteriaOperator.LessEqual:
                            break;
                        default:
                            // 1つ目の条件の絞り込み演算子と2つ目の条件の絞り込み演算子の組み合わせが不正な場合は、2つ目の条件を無視
                            // 1つ目の条件を引数としてFiltering011に仲介
                            Filtering(data, questionType, CriteriaValue1, criteriaOperator1, ref FilteringFlag, ope);
                            return;
                    }
                    break;
                case CriteriaOperator.Less:
                case CriteriaOperator.LessEqual:
                    switch (criteriaOperator2)
                    {
                        case CriteriaOperator.Greater:
                        case CriteriaOperator.GreaterEqual:
                            break;
                        default:
                            // 1つ目の条件の絞り込み演算子と2つ目の条件の絞り込み演算子の組み合わせが不正な場合は、2つ目の条件を無視
                            // 1つ目の条件を引数としてFiltering011に仲介
                            Filtering(data, questionType, CriteriaValue1, criteriaOperator1, ref FilteringFlag, ope);
                            return;
                    }
                    break;
                default:    // このメソッドで扱わない絞り込み演算子の場合は処理しない
                    return;
            }
            // FilteringFlagがnullの場合、新たに配列生成
            if (FilteringFlag == null) FilteringFlag = new bool[data.Count];
            for (int i = 0; i < data.Count; ++i)
            {
                NData ndata = data[i] as NData;
                // 削除データの場合
                if (data[i].IsDeleted)
                {
                    FilteringFlag[i] = false;
                    continue;
                }
                // 不要な評価はしない
                if (!(FilteringFlag[i] ^ ope == Operator.opOr)) continue;
                // 非削除通常データの場合
                if (data[i].DataType == DataType.NormalData)
                {
                    // 絞り込み演算子に応じた比較演算
                    switch (criteriaOperator1)
                    {
                        case CriteriaOperator.Greater:
                            /*
                            FilteringFlag[i] = ndata.Value > CriteriaValue1;
                            */
                            FilteringFlag[i] = ndata.IsGreater(CriteriaValue1);
                            break;
                        case CriteriaOperator.GreaterEqual:
                            /*
                            FilteringFlag[i] = ndata.Value >= CriteriaValue1;
                            */
                            FilteringFlag[i] = ndata.IsGreaterEqual(CriteriaValue1);
                            break;
                        case CriteriaOperator.Less:
                            /*
                            FilteringFlag[i] = ndata.Value < CriteriaValue1;
                            */
                            FilteringFlag[i] = ndata.IsLess(CriteriaValue1);
                            break;
                        case CriteriaOperator.LessEqual:
                            /*
                            FilteringFlag[i] = ndata.Value <= CriteriaValue1;
                            */
                            FilteringFlag[i] = ndata.IsLessEqual(CriteriaValue1);
                            break;
                    }
                    // 1つ目の条件と2つ目の条件とはAND結合
                    if (FilteringFlag[i])
                    {
                        // 絞り込み演算子に応じた比較演算
                        switch (criteriaOperator2)
                        {
                            case CriteriaOperator.Greater:
                                /*
                                FilteringFlag[i] = ndata.Value > CriteriaValue2;
                                */
                                FilteringFlag[i] = ndata.IsGreater(CriteriaValue2);
                                break;
                            case CriteriaOperator.GreaterEqual:
                                /*
                                FilteringFlag[i] = ndata.Value >= CriteriaValue2;
                                */
                                FilteringFlag[i] = ndata.IsGreaterEqual(CriteriaValue2);
                                break;
                            case CriteriaOperator.Less:
                                /*
                                FilteringFlag[i] = ndata.Value < CriteriaValue2;
                                */
                                FilteringFlag[i] = ndata.IsLess(CriteriaValue2);
                                break;
                            case CriteriaOperator.LessEqual:
                                /*
                                FilteringFlag[i] = ndata.Value <= CriteriaValue2;
                                */
                                FilteringFlag[i] = ndata.IsLessEqual(CriteriaValue2);
                                break;
                        }
                    }
                    continue;
                }
                // 非削除の無回答または非該当の場合 (必要時のみ評価)
                FilteringFlag[i] = false;
            }
        }
#endif

#if AFTER_2ND_PHASE
        /// <alias>Filtering014</alias>
        /// <summary>
        /// <para>エイリアス:Filtering014</para>
        /// 2つの条件を一度に処理するフィルタリングを行う<br />
        /// 引数opeにOperator.opOrを指定してFiltering013に仲介する
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="CriteriaValue1">1つ目の条件の条件値(数値)</param>
        /// <param name="criteriaOperator1">1つ目の条件の絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="CriteriaValue2">2つ目の条件の条件値(数値)</param>
        /// <param name="criteriaOperator2">2つ目の条件の絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GlobalTabulation.Filtering(System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},Macromill.QCWeb.Tabulation.QuestionType,System.Double,Macromill.QCWeb.Tabulation.CriteriaOperator,System.Double,Macromill.QCWeb.Tabulation.CriteriaOperator,System.Boolean[]@,Macromill.QCWeb.Tabulation.Operator)">Filtering013</seealso>
        public static void Filtering(List<Data> data, QuestionType questionType
                            , double CriteriaValue1, CriteriaOperator criteriaOperator1
                            , double CriteriaValue2, CriteriaOperator criteriaOperator2
                            , ref bool[] FilteringFlag)
        {
            Filtering(data, questionType, CriteriaValue1, criteriaOperator1, CriteriaValue2, criteriaOperator2, ref FilteringFlag, Operator.opOr);
        }
#endif

        /// <alias>Filtering101</alias>
        /// <summary>
        /// <para>エイリアス:Filtering101</para>
        /// データ種別を条件値としてフィルタリングを行う<br />
        /// 無回答あるいは非該当のいずれか、またはその組み合わせを表す値を条件値に指定し、演算子には、<br />
        /// ～と等しい, ～と等しくない, ～のいずれか, ～のいずれでもない<br />
        /// のいずれかを表す値を指定して使用する
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="CriteriaType">条件値(データ種別を表すDataType列挙型の値)</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Operator">Operator列挙型</seealso>
        public static void Filtering(List<Data> data, DataType CriteriaType, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag, Operator ope)
        {
            // dataがnullの場合処理しない
            if (data == null) return;
            // 無回答または非該当、あるいはその両方での絞り込みでなければ処理しない
            if ((int)(CriteriaType & (DataType.NAData | DataType.IVData)) == 0) return;
            switch (criteriaOperator)
            {
                case CriteriaOperator.Equal:
                case CriteriaOperator.NotEqual:
                case CriteriaOperator.Anyone:
                case CriteriaOperator.NotEqualAnyone:
                    break;
                default:    // このメソッドで扱わない絞り込み演算子の場合は処理しない
                    return;
            }
            // FilteringFlagがnullの場合、新たに配列生成
            if (FilteringFlag == null) FilteringFlag = new bool[data.Count];
            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i].IsDeleted)
                {
                    FilteringFlag[i] = false;
                    continue;
                }
                // 不要な評価はしない
                if (!(FilteringFlag[i] ^ ope == Operator.opOr)) continue;
                /*
                DataType tmp = data[i].DataType;
                */
                switch (criteriaOperator)
                {
                    // Equal(一致)、NotEqual(不一致)では、等価演算
                    case CriteriaOperator.Equal:
                        /*
                        FilteringFlag[i] = tmp == CriteriaType;
                        */
                        FilteringFlag[i] = data[i].Equals(CriteriaType);
                        break;
                    case CriteriaOperator.NotEqual:
                        /*
                        FilteringFlag[i] = tmp != CriteriaType;
                        */
                        FilteringFlag[i] = !data[i].Equals(CriteriaType);
                        break;
                    // Anyone(いずれか)、NotEqualAnyone(いずれでもない)では、ビット積演算
                    case CriteriaOperator.Anyone:
                        /*
                        FilteringFlag[i] = (tmp & CriteriaType) == tmp;
                        */
                        FilteringFlag[i] = data[i].IsAnyOne(CriteriaType);
                        break;
                    case CriteriaOperator.NotEqualAnyone:
                        /*
                        FilteringFlag[i] = (int)(tmp & CriteriaType) == 0;
                        */
                        FilteringFlag[i] = !data[i].IsAnyOne(CriteriaType);
                        break;
                }
            }
        }

        /// <alias>Filtering102</alias>
        /// <summary>
        /// <para>エイリアス:Filtering101</para>
        /// データ種別を条件値としてフィルタリングを行う<br />
        /// 引数opeにOperator.opOrを指定してFiltering101に仲介する
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="CriteriaType">条件値(データ種別を表すDataType列挙型の値)</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.DataType">DataType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.CriteriaOperator">CriteriaOperator列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GlobalTabulation.Filtering(System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},Macromill.QCWeb.Tabulation.DataType,Macromill.QCWeb.Tabulation.CriteriaOperator,System.Boolean[]@,Macromill.QCWeb.Tabulation.Operator)">Filtering101</seealso>
        public static void Filtering(List<Data> data, DataType CriteriaType, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag)
        {
            Filtering(data, CriteriaType, criteriaOperator, ref FilteringFlag, Operator.opOr);
        }

        /// <alias>Filtering201</alias>
        /// <summary>
        /// <para>エイリアス:Filtering201</para>
        /// アイテムデータを条件値としてフィルタリングを行う
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスへの参照を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="CriteriaData">条件データとするDataクラスのインスタンスへの参照を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値</param>
        public static void Filtering(List<Data> data, List<Data> CriteriaData, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag, Operator ope)
        {
            // dataがnullの場合、CriteriaDataがnullの場合、両者の要素数が異なる場合は処理しない
            if (data == null || CriteriaData == null || data.Count != CriteriaData.Count || data.Count == 0) return;
            Type dataType = data[0].GetType();
            /*
            認識間違い
            // データの種類が異なる場合には処理しない
            if (CriteriaData[0].GetType() != dataType) return;
            */
            Type criteriaDataType = CriteriaData[0].GetType();
            // 使用できる演算子以外では処理しない
            if (dataType == typeof(SAData) || dataType == typeof(NData)) // SAまたはNの場合
            {
                switch (criteriaOperator)
                {
                    case CriteriaOperator.Equal:
                    case CriteriaOperator.NotEqual:
                    case CriteriaOperator.Greater:
                    case CriteriaOperator.GreaterEqual:
                    case CriteriaOperator.Less:
                    case CriteriaOperator.LessEqual:
                        break;
                    default:
                        return;
                }
            }
            else if (dataType == typeof(MAData) || dataType == typeof(FAData)) // MAまたはFAの場合
            {
                switch (criteriaOperator)
                {
                    case CriteriaOperator.Equal:
                    case CriteriaOperator.NotEqual:
                        break;
                    default:
                        return;
                }
            }
            else
            {
                return;
            }
            if (dataType == typeof(MAData))
            {
                // 条件データはMA
                if (criteriaDataType != typeof(MAData)) return;
            }
            else
            {
                // 条件データはSA/N/FAのいずれか
                if (criteriaDataType != typeof(SAData) && criteriaDataType != typeof(NData) && criteriaDataType != typeof(FAData)) return;
            }
            // FilteringFlagがnullの場合、新たに配列生成
            if (FilteringFlag == null) FilteringFlag = new bool[data.Count];
            for (int i = 0; i < data.Count; ++i)
            {
                // 削除データの場合
                if (data[i].IsDeleted)
                {
                    FilteringFlag[i] = false;
                    continue;
                }
                // 不要な評価はしない
                if (FilteringFlag[i] ^ ope != Operator.opOr) continue;
                /*
                // 無回答や非該当の場合
                if (data[i].DataType != DataType.NormalData)
                {
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                        case CriteriaOperator.NotEqual:
                            FilteringFlag[i] = data[i].DataType == CriteriaData[i].DataType ^ criteriaOperator == CriteriaOperator.NotEqual;
                            break;
                        default:
                            FilteringFlag[i] = false;
                            break;
                    }
                    continue;
                }
                // 通常データの場合
                if (dataType == typeof(SAData)) // SAの場合
                {
                    SAData tmpData = data[i] as SAData;
                    SAData criteriaData = CriteriaData[i] as SAData;
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                        case CriteriaOperator.NotEqual:
                            if (criteriaData.DataType == DataType.NormalData)
                            {
                                FilteringFlag[i] = tmpData.Value == criteriaData.Value ^ criteriaOperator == CriteriaOperator.NotEqual;
                            }
                            else
                            {
                                FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual;
                            }
                            break;
                        case CriteriaOperator.Greater:
                            FilteringFlag[i] = criteriaData.DataType == DataType.NormalData && tmpData.Value > criteriaData.Value;
                            break;
                        case CriteriaOperator.GreaterEqual:
                            FilteringFlag[i] = criteriaData.DataType == DataType.NormalData && tmpData.Value >= criteriaData.Value;
                            break;
                        case CriteriaOperator.Less:
                            FilteringFlag[i] = criteriaData.DataType == DataType.NormalData && tmpData.Value < criteriaData.Value;
                            break;
                        case CriteriaOperator.LessEqual:
                            FilteringFlag[i] = criteriaData.DataType == DataType.NormalData && tmpData.Value <= criteriaData.Value;
                            break;
                    }
                }
                else if (dataType == typeof(MAData))    // MAの場合
                {
                    MAData tmpData = data[i] as MAData;
                    MAData criteriaData = CriteriaData[i] as MAData;
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                        case CriteriaOperator.NotEqual:
                            if (criteriaData.DataType == DataType.NormalData)
                            {
                                FilteringFlag[i] = criteriaOperator == CriteriaOperator.Equal;
                                MAData data1 = tmpData.ValueSize <= criteriaData.ValueSize ? tmpData : criteriaData;
                                MAData data2 = tmpData.ValueSize <= criteriaData.ValueSize ? criteriaData : tmpData;
                                for (int j = 0; j < data1.ValueSize; ++j)
                                {
                                    if (data1.Value(j) != data2.Value(j))
                                    {
                                        FilteringFlag[i] = !FilteringFlag[i];
                                        break;
                                    }
                                }
                                if (data1.ValueSize == data2.ValueSize) break;
                                if (FilteringFlag[i] == (criteriaOperator != CriteriaOperator.Equal)) break;
                                for (int j = data1.ValueSize + 1; j < data2.ValueSize; ++j)
                                {
                                    if (data2.Value(j) != 0)
                                    {
                                        FilteringFlag[i] = !FilteringFlag[i];
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual;
                            }
                            break;
                    }
                }
                else if (dataType == typeof(FAData))    // FAの場合
                {
                    FAData tmpData = data[i] as FAData;
                    FAData criteriaData = CriteriaData[i] as FAData;
                    if (criteriaData.DataType == DataType.NormalData)
                    {
                        FilteringFlag[i] = tmpData.Value.Equals(criteriaData.Value) ^ criteriaOperator == CriteriaOperator.NotEqual;
                    }
                    else
                    {
                        FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual;
                    }
                }
                else    // Nの場合
                {
                    NData tmpData = data[i] as NData;
                    NData criteriaData = CriteriaData[i] as NData;
                    switch (criteriaOperator)
                    {
                        case CriteriaOperator.Equal:
                        case CriteriaOperator.NotEqual:
                            if (criteriaData.DataType == DataType.NormalData)
                            {
                                FilteringFlag[i] = tmpData.Value == criteriaData.Value ^ criteriaOperator == CriteriaOperator.NotEqual;
                            }
                            else
                            {
                                FilteringFlag[i] = criteriaOperator == CriteriaOperator.NotEqual;
                            }
                            break;
                        case CriteriaOperator.Greater:
                            FilteringFlag[i] = criteriaData.DataType == DataType.NormalData && tmpData.Value > criteriaData.Value;
                            break;
                        case CriteriaOperator.GreaterEqual:
                            FilteringFlag[i] = criteriaData.DataType == DataType.NormalData && tmpData.Value >= criteriaData.Value;
                            break;
                        case CriteriaOperator.Less:
                            FilteringFlag[i] = criteriaData.DataType == DataType.NormalData && tmpData.Value < criteriaData.Value;
                            break;
                        case CriteriaOperator.LessEqual:
                            FilteringFlag[i] = criteriaData.DataType == DataType.NormalData && tmpData.Value <= criteriaData.Value;
                            break;
                    }
                }
                */
                switch (criteriaOperator)
                {
                    case CriteriaOperator.Equal:
                        FilteringFlag[i] = data[i].Equals(CriteriaData[i]);
                        break;
                    case CriteriaOperator.NotEqual:
                        FilteringFlag[i] = !data[i].Equals(CriteriaData[i]);
                        break;
                    case CriteriaOperator.Greater:
                        FilteringFlag[i] = data[i].IsGreater(CriteriaData[i]);
                        break;
                    case CriteriaOperator.GreaterEqual:
                        FilteringFlag[i] = data[i].IsGreaterEqual(CriteriaData[i]);
                        break;
                    case CriteriaOperator.Less:
                        FilteringFlag[i] = data[i].IsLess(CriteriaData[i]);
                        break;
                    case CriteriaOperator.LessEqual:
                        FilteringFlag[i] = data[i].IsLessEqual(CriteriaData[i]);
                        break;
                }
            }
        }

        /// <alias>Filtering202</alias>
        /// <summary>
        /// <para>エイリアス:Filtering202</para>
        /// アイテムデータを条件値としてフィルタリングを行う<br />
        /// 引数opeにOperator.opOrを指定してFiltering201に仲介する
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスへの参照を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="CriteriaData">条件データとするDataクラスのインスタンスへの参照を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="criteriaOperator">絞り込み演算子を表すCriteriaOperator列挙型の値</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        public static void Filtering(List<Data> data, List<Data> CriteriaData, CriteriaOperator criteriaOperator, ref bool[] FilteringFlag)
        {
            Filtering(data, CriteriaData, criteriaOperator, ref FilteringFlag, Operator.opOr);
        }

        /// <summary>
        /// 条件値の文字列表現をFilteringメソッドで使用する配列に変換して返す<br />
        /// また、無回答または非該当を条件していると判断した場合には、戻り値としてその条件データ種別を返す
        /// </summary>
        /// <typeparam name="T">SA/MA質問の場合はint、Nの場合はdoubleまたはNData.ValueRangeのみ可能。他の質問タイプでは不可</typeparam>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="criteriaValueDescription">条件値の文字列表現。無回答は「DK」、非該当は「*」、複数の値を指定する場合は半角カンマまたは半角スラッシュ区切り</param>
        /// <param name="criteriaValueList">T型の一次元配列 (戻り値)</param>
        /// <returns>条件のデータ種別を表すDataType列挙型の値</returns>
        public static DataType CriteriaValueDescriptionToValueList<T>(QuestionType questionType, string criteriaValueDescription, out T[] criteriaValueList, int catCnt = 0, int indexpos = -1) where T : struct //trial
        {
            //trial criteria value as variable [Redmine id : 178707] -
            string criteriavalue = criteriaValueDescription;
            Criteria.isvariable = false;
            List<Data> criteriavaluedata = null;//trial
            if (criteriavalue.StartsWith("[") && criteriavalue.EndsWith("]"))//needed regex for characters with digits enclosed in [ ] with spl symbols like _ -
            {
                if (criteriavalue.Length > 1)
                {
                    criteriavalue = criteriavalue.Remove(0, 1);
                    criteriavalue = criteriavalue.Remove(criteriavalue.Length - 1, 1);
                    string[] criteriavaluearray = criteriavalue.Split('@');
                    if (criteriavaluearray.Length == 3)
                    {
                        int result;
                        if (int.TryParse(criteriavaluearray[1], out result))
                        {
                            criteriaValueDescription = criteriavaluearray[1];// criteriaValueDescription = criteriavaluearray[1];
                            Criteria.isvariable = true;
                            Criteria.variabletype = (criteriavaluearray[2] == "N") ? QuestionType.N : (criteriavaluearray[2] == "SA") ? QuestionType.SA : (criteriavaluearray[2] == "FA") ? QuestionType.FA : QuestionType.MA;
                        }
                    }
                }
            }
            //end trial
            criteriaValueList = null;
            DataType criteriaDataType = DataType.NormalData;
            if (string.IsNullOrWhiteSpace(criteriaValueDescription)) return criteriaDataType;
            criteriaValueDescription = criteriaValueDescription.Replace(" ", "");
            bool isDouble = false;
            bool isRange = false;
            Type t = typeof(T);
            if (t == typeof(int))
            {
                if ((int)(questionType & (QuestionType.SA | QuestionType.MA)) == 0) return criteriaDataType;
            }
            else if (t == typeof(double) || t == typeof(NData.ValueRange))
            {
                if ((questionType & QuestionType.N) != QuestionType.N) return criteriaDataType;
                isDouble = true;
                isRange = t == typeof(NData.ValueRange);
            }
            else
            {
                return criteriaDataType;
            }
            Hashtable criteriaValues = new Hashtable();
            bool isExclude = false;
            if (criteriaValueDescription.StartsWith("!"))
            {
                criteriaValueDescription = criteriaValueDescription.TrimStart('!');
                isExclude = true;
            }
            //if (criteriaValueDescription.StartsWith("<>"))//191  added for <> 'not'
            //{
            //    criteriaValueDescription = criteriaValueDescription.Replace("<>","");//TrimStart('<>');
            //    isExclude = true;
            //}
            bool catAdded = false;
            foreach (string criteriaValue in criteriaValueDescription.Split(new char[] { ',', '/' }))
            {
                int sectorNumber = 0;
                double criteriaNumber = 0.0;
                string modCriteriaValue = criteriaValue;
                if (!isDouble)
                {
                    if (criteriaValue.StartsWith("-"))
                    {
                        modCriteriaValue = 1 + criteriaValue;
                    }
                    else if (criteriaValue.EndsWith("-"))
                    {
                        modCriteriaValue = criteriaValue + catCnt;
                    }
                }
                if (criteriaValue.Equals("DK"))
                {
                    if (criteriaValues.Count == 0 || (int)(criteriaDataType & (DataType.NAData | DataType.IVData)) != 0)
                    {
                        criteriaDataType |= DataType.NAData;
                    }
                    else
                    {
                        // 不正な条件値指定
                        return criteriaDataType;
                    }
                }
                else if (criteriaValue.Equals("*"))
                {
                    if (criteriaValues.Count == 0 || (int)(criteriaDataType & (DataType.NAData | DataType.IVData)) != 0)
                    {
                        criteriaDataType |= DataType.IVData;
                    }
                    else
                    {
                        // 不正な条件値指定
                        return criteriaDataType;
                    }
                }
                else if (isDouble ? double.TryParse(modCriteriaValue, out criteriaNumber) : int.TryParse(modCriteriaValue, out sectorNumber))
                {
                    if (criteriaDataType == DataType.NormalData)
                    {
                        string key = isDouble ? (isRange ? string.Format("{0}to{0}", criteriaNumber.ToString()) : criteriaNumber.ToString()) : sectorNumber.ToString();

                        if (isDouble)
                        {
                            if (!criteriaValues.ContainsKey(key))
                            {
                                if (isRange)
                                {
                                    criteriaValues.Add(key, new NData.ValueRange(criteriaNumber, criteriaNumber));
                                }
                                else
                                {
                                    criteriaValues.Add(key, criteriaNumber);
                                }
                            }
                        }
                        else
                        {
                            if (isExclude)
                            {
                                for (int i = 1; i <= catCnt; ++i)
                                {
                                    if (!catAdded && !criteriaValues.ContainsKey(i.ToString()) && i != sectorNumber)
                                    {
                                        criteriaValues.Add(i.ToString(), i);
                                    }
                                    if (catAdded && criteriaValues.ContainsKey(i.ToString()) && i == sectorNumber)
                                    {
                                        criteriaValues.Remove(i.ToString());
                                    }
                                }
                                catAdded = true;
                            }
                            else
                            {
                                if (!criteriaValues.ContainsKey(key))
                                {
                                    criteriaValues.Add(key, sectorNumber);
                                }
                            }
                        }
                    }
                    else
                    {
                        // 不正な条件値指定
                        criteriaDataType = DataType.NormalData;
                        return criteriaDataType;
                    }
                }
                else if (isDouble)
                {
                    bool isError = !isRange;
                    while (!isError)
                    {
                        isError = true;
                        if (criteriaDataType != DataType.NormalData) break;
                        string ptn = @"(\(*)(\-?[\d\.]+)(\)*)";
                        Regex regex = new Regex("^" + ptn);
                        if (!regex.IsMatch(modCriteriaValue)) break;
                        Match m = regex.Match(modCriteriaValue);
                        double min = 0.0;
                        if (m.Groups[1].Length != m.Groups[3].Length || !double.TryParse(m.Groups[2].Value, out min)) break;
                        int idx = m.Length + 1;
                        regex = new Regex(ptn + "$");
                        if (!regex.IsMatch(modCriteriaValue, idx)) break;
                        m = regex.Match(modCriteriaValue, idx);
                        Console.WriteLine(string.Format("{0}\t{1}\t{2}", m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value));
                        double max = 0.0;
                        if (m.Groups[1].Length != m.Groups[3].Length || !double.TryParse(m.Groups[2].Value, out max)) break;
                        if (min > max) break;
                        string key = string.Format("{0}to{1}", min.ToString(), max.ToString());
                        if (!criteriaValues.ContainsKey(key))
                        {
                            criteriaValues.Add(key, new NData.ValueRange(min, max));
                        }
                        isError = false;
                        break;
                    }
                    if (isError)
                    {
                        // 不正な条件値指定
                        criteriaDataType = DataType.NormalData;
                        return criteriaDataType;
                    }
                }
                else
                {
                    if (criteriaDataType != DataType.NormalData)
                    {
                        // 不正な条件値指定
                        criteriaDataType = DataType.NormalData;
                        return criteriaDataType;
                    }
                    string[] criteriaValueSplit = modCriteriaValue.Split(new char[] { '-' }, 2);
                    int sectorNumberMin = 0, sectorNumberMax = 0;
                    if (criteriaValueSplit.Length == 2
                            && int.TryParse(criteriaValueSplit[0], out sectorNumberMin)
                            && int.TryParse(criteriaValueSplit[1], out sectorNumberMax)
                            && sectorNumberMin <= sectorNumberMax)
                    {
                        if (isExclude)
                        {
                            for (int i = 1; i <= catCnt; ++i)
                            {
                                if (!catAdded && !criteriaValues.ContainsKey(i.ToString()) && (i < sectorNumberMin || i > sectorNumberMax))
                                {
                                    criteriaValues.Add(i.ToString(), i);
                                }
                                if (catAdded && criteriaValues.ContainsKey(i.ToString()) && i >= sectorNumberMin && i <= sectorNumberMax)
                                {
                                    criteriaValues.Remove(i.ToString());
                                }
                            }
                            catAdded = true;
                        }
                        else
                        {
                            for (int i = sectorNumberMin; i <= sectorNumberMax; ++i)
                            {
                                if (!criteriaValues.ContainsKey(i.ToString()))
                                {
                                    criteriaValues.Add(i.ToString(), i);
                                }
                            }
                        }
                    }
                    else
                    {
                        // 不正な条件値指定
                        return criteriaDataType;
                    }
                }
            }
            if (criteriaDataType != DataType.NormalData) return criteriaDataType;
            if (criteriaValues.Count == 0)
            {
                // 不正な条件値指定
                return criteriaDataType;
            }
            criteriaValueList = new T[criteriaValues.Count];
            int j = -1;
            foreach (T value in criteriaValues.Values)
            {
                criteriaValueList[++j] = value;
            }
            Array.Sort<T>(criteriaValueList);
            return criteriaDataType;
        }

        /// <summary>
        /// 各種フィルタリングを簡易呼び出しで、内部で分岐処理させる
        /// <note>
        /// 現行QC3仕様でのフィルタリングのみ対応した簡易版であり、より高度なフィルタリングを行う場合は、Filteringメソッドを使用すること
        /// 演算子の文字列表現、MAでの「=」「&lt;&gt;」が表す演算内容などの特殊仕様は、すべて現行QC3に合わせている
        /// </note>
        /// </summary>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="questionType">質問タイプ</param>
        /// <param name="criteriaValueDescription">条件値の文字列表現。無回答は「DK」、非該当は「*」、複数の値を指定する場合は半角カンマまたは半角スラッシュ区切り</param>
        /// <param name="criteriaOperatorDescription">条件値との比較演算に用いる演算子の文字列表現 (※QC3の特殊仕様に従う)</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値 (省略可、既定値Operator.opOr)</param>
        internal static void EasyFiltering(List<Data> data, QuestionType questionType, string criteriaValueDescription, string criteriaOperatorDescription, ref bool[] FilteringFlag, Operator ope = Operator.opOr, int catCnt = 0, System.Data.DataTable dt = null)
        {
            if (criteriaOperatorDescription == null) return;
            if (criteriaValueDescription == null) criteriaValueDescription = string.Empty;
            QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA);
            DataType criteriaDataType = DataType.NormalData;
            int[] criteriaSectorList = null;
            double[] criteriaNumberList = null;
            string criteriaBuffer = null;
            CriteriaOperator criteriaOperator = CriteriaOperator.Equal;
            // ざっくり演算子決定
            if (criteriaOperatorDescription.Equals("="))
            {
                criteriaOperator = CriteriaOperator.Equal;
            }
            else if (criteriaOperatorDescription.Equals("<>") || criteriaOperatorDescription.Equals("!="))
            {
                criteriaOperator = CriteriaOperator.NotEqual;
            }
            else if (criteriaOperatorDescription.Equals(">="))
            {
                criteriaOperator = CriteriaOperator.GreaterEqual;
            }
            else if (criteriaOperatorDescription.Equals(">"))
            {
                criteriaOperator = CriteriaOperator.Greater;
            }
            else if (criteriaOperatorDescription.Equals("<="))
            {
                criteriaOperator = CriteriaOperator.LessEqual;
            }
            else if (criteriaOperatorDescription.Equals("<"))
            {
                criteriaOperator = CriteriaOperator.Less;
            }
            else
            {
                return;
            }
            switch (qType)
            {
                case QuestionType.SA:
                case QuestionType.MA:
                    criteriaDataType = CriteriaValueDescriptionToValueList<int>(
                                        qType, criteriaValueDescription, out criteriaSectorList, catCnt);
                    if (criteriaDataType != DataType.NormalData) break;
                    if (criteriaSectorList == null || criteriaSectorList.Length == 0) return;
                    if (qType == QuestionType.MA || criteriaSectorList.Length > 1)
                    {
                        // 演算子の調整
                        switch (criteriaOperator)
                        {
                            case CriteriaOperator.Equal:
                                criteriaOperator = qType == QuestionType.MA ? CriteriaOperator.IncludeAnyone : CriteriaOperator.Anyone;
                                break;
                            case CriteriaOperator.NotEqual:
                                criteriaOperator = qType == QuestionType.MA ? CriteriaOperator.NotIncludeAnyone : CriteriaOperator.NotEqualAnyone;
                                break;
                            default:
                                return;
                        }
                        // Filtering001をコール
                        Filtering(data, questionType, ref criteriaSectorList, criteriaOperator, ref FilteringFlag, ope, dt);
                    }
                    else    // SAで条件値1つ
                    {
                        // Filtering003をコール
                        Filtering(data, questionType, criteriaSectorList[0], criteriaOperator, ref FilteringFlag, ope, dt);
                    }
                    return;
                case QuestionType.N:
                    criteriaDataType = CriteriaValueDescriptionToValueList<double>(
                                        qType, criteriaValueDescription, out criteriaNumberList, catCnt);
                    if (criteriaDataType != DataType.NormalData) break;
                    if (criteriaNumberList == null || criteriaNumberList.Length == 0)
                    {
                        NData.ValueRange[] criteriaValueRangeList;
                        criteriaDataType = CriteriaValueDescriptionToValueList<NData.ValueRange>(
                                            qType, criteriaValueDescription, out criteriaValueRangeList, catCnt);
                        if (criteriaValueRangeList == null || criteriaValueRangeList.Length == 0) return;
                        if (FilteringFlag == null) FilteringFlag = new bool[data.Count];
                        for (int i = 0; i < data.Count; ++i)
                        {
                            if (!(FilteringFlag[i] ^ ope == Operator.opOr)) continue;
                            switch (criteriaOperator)
                            {
                                case CriteriaOperator.Equal:
                                    FilteringFlag[i] = data[i].IsAnyOne(criteriaValueRangeList);
                                    break;
                                case CriteriaOperator.NotEqual:
                                    FilteringFlag[i] = !data[i].IsAnyOne(criteriaValueRangeList);
                                    break;
                                default:
                                    return;
                            }
                        }
                    }
                    else
                    {
                        if (criteriaNumberList.Length > 1)
                        {
                            // 演算子の調整
                            switch (criteriaOperator)
                            {
                                case CriteriaOperator.Equal:
                                    criteriaOperator = CriteriaOperator.Anyone;
                                    break;
                                case CriteriaOperator.NotEqual:
                                    criteriaOperator = CriteriaOperator.NotEqualAnyone;
                                    break;
                                default:
                                    return;
                            }
                            // Filtering009をコール
                            Filtering(data, qType, ref criteriaNumberList, criteriaOperator, ref FilteringFlag, ope);
                        }
                        else　   // 条件値1つ
                        {
                            // Filtering011をコール
                            Filtering(data, qType, criteriaNumberList[0], criteriaOperator, ref FilteringFlag, ope, dt);
                        }
                    }
                    return;
                case QuestionType.FA:
                    if (criteriaValueDescription.Equals(string.Empty) || criteriaValueDescription.Equals("DK"))
                    {
                        criteriaDataType = DataType.NAData;
                        break;
                    }
                    criteriaBuffer = criteriaValueDescription;
                    switch (criteriaOperator)
                    {
                        // 演算子の調整
                        case CriteriaOperator.Equal:
                            //criteriaOperator = CriteriaOperator.PatternMatching;
                            criteriaOperator = CriteriaOperator.Equal; // redmine 168071
                            break;
                        case CriteriaOperator.NotEqual:
                            //criteriaOperator = CriteriaOperator.PatternUnmatching;
                            criteriaOperator = CriteriaOperator.NotEqual; // redmine 168071
                            break;
                        default:
                            return;
                    }
                    // Filtering007をコール
                    Filtering(data, questionType, criteriaBuffer, criteriaOperator, ref FilteringFlag, ope, dt);
                    return;
                default:
                    return;
            }
            // 標準データでは、ここまで来ない
            switch (criteriaOperator)
            {
                case CriteriaOperator.Equal:
                case CriteriaOperator.NotEqual:
                    break;
                default:
                    return;
            }
            if (criteriaDataType == (DataType.NAData | DataType.IVData))
            {
                if (criteriaOperator == CriteriaOperator.Equal)
                {
                    criteriaOperator = CriteriaOperator.Anyone;
                }
                else
                {
                    criteriaOperator = CriteriaOperator.NotEqualAnyone;
                }
            }
            // Filtering101をコール
            Filtering(data, criteriaDataType, criteriaOperator, ref FilteringFlag, ope);
        }

        /// <summary>
        /// 各種フィルタリングを簡易呼び出しで、内部で分岐処理させる<br />
        /// 質問データテキストファイルの存在が担保されていて、その内容の読み込みができていない場合に使用する
        /// <note>
        /// 現行QC3仕様でのフィルタリングのみ対応した簡易版であり、より高度なフィルタリングを行う場合は、Filteringメソッドを使用すること
        /// 演算子の文字列表現、MAでの「=」「&lt;&gt;」が表す演算内容などの特殊仕様は、すべて現行QC3に合わせている
        /// </note>
        /// </summary>
        /// <param name="questionFilePath">質問データのテキストファイルのパス</param>
        /// <param name="questionType">質問タイプ</param>
        /// <param name="criteriaValueDescription">条件値の文字列表現。無回答は「DK」、非該当は「*」、複数の値を指定する場合は半角カンマまたは半角スラッシュ区切り</param>
        /// <param name="criteriaOperatorDescription">条件値との比較演算に用いる演算子の文字列表現 (※QC3の特殊仕様に従う)</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス (省略可、既定値null(nullのときには削除フラグ情報を更新しない))</param>
        /// <param name="reRead">既に削除フラグ情報を保持しているときに、再度削除ファイルの読み込みを行うかどうかを示すフラグ (省略可、既定値false)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値 (省略可、既定値Operator.opOr)</param>
        internal static void EasyFiltering(string questionFilePath, QuestionType questionType
                                       , string criteriaValueDescription, string criteriaOperatorDescription
                                       , ref bool[] FilteringFlag, string deleteFlagFilePath = null
                                       , bool reRead = false, Operator ope = Operator.opOr)
        {
            QCWebException exception = null;
            List<Data> data = ReadTextFile.ReadData(questionFilePath, questionType, deleteFlagFilePath, out exception, reRead);
            if (exception != null) throw exception;
            EasyFiltering(data, questionType, criteriaValueDescription, criteriaOperatorDescription, ref FilteringFlag, ope);
        }

        /// <summary>
        /// 各種フィルタリングを簡易呼び出しで、内部で分岐処理させる<br />
        /// 質問データテキストファイルの存在が担保されていない場合にも使用できる
        /// <note>
        /// 現行QC3仕様でのフィルタリングのみ対応した簡易版であり、より高度なフィルタリングを行う場合は、Filteringメソッドを使用すること
        /// 演算子の文字列表現、MAでの「=」「&lt;&gt;」が表す演算内容などの特殊仕様は、すべて現行QC3に合わせている
        /// </note>
        /// </summary>
        /// <param name="questionid">アイテムID</param>
        /// <param name="dirpath">データテキストファイルを配置するディレクトリパス</param>
        /// <param name="questionFilePath">データテキストファイルパス (戻り値)</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値 (戻り値)</param>
        /// <param name="criteriaValueDescription">条件値の文字列表現。無回答は「DK」、非該当は「*」、複数の値を指定する場合は半角カンマまたは半角スラッシュ区切り</param>
        /// <param name="criteriaOperatorDescription">条件値との比較演算に用いる演算子の文字列表現 (※QC3の特殊仕様に従う)</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="reReadDeleteFlag">既に削除フラグ情報を保持しているときに、再度削除ファイルの読み込みを行うかどうかを示すフラグ (省略可、既定値false)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値 (省略可、既定値Operator.opOr)</param>
        internal static void EasyFiltering(decimal questionid, string dirpath
                                       , out string questionFilePath, out QuestionType questionType
                                       , string criteriaValueDescription, string criteriaOperatorDescription
                                       , ref bool[] FilteringFlag, bool reReadDeleteFlag = false, Operator ope = Operator.opOr)
        {
            QCWebException exception = null;
            List<Data> data = ReadTextFile.ReadData2(questionid, dirpath, out questionFilePath, out questionType, out exception, reReadDeleteFlag);
            if (exception != null) throw exception;
            EasyFiltering(data, questionType, criteriaValueDescription, criteriaOperatorDescription, ref FilteringFlag, ope);
        }

        /// <summary>
        /// 各種フィルタリングを簡易呼び出しで、内部で分岐処理させる<br />
        /// 質問データテキストファイルの存在が担保されていない場合にも使用できる
        /// <note>
        /// 現行QC3仕様でのフィルタリングのみ対応した簡易版であり、より高度なフィルタリングを行う場合は、Filteringメソッドを使用すること
        /// 演算子の文字列表現、MAでの「=」「&lt;&gt;」が表す演算内容などの特殊仕様は、すべて現行QC3に合わせている
        /// </note>
        /// </summary>
        /// <param name="qcwebid">QCWeb管理ID</param>
        /// <param name="questionid">アイテムID</param>
        /// <param name="dirpath">データテキストファイルを配置するディレクトリパス</param>
        /// <param name="questionFilePath">データテキストファイルパス (戻り値)</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値 (戻り値)</param>
        /// <param name="criteriaValueDescription">条件値の文字列表現。無回答は「DK」、非該当は「*」、複数の値を指定する場合は半角カンマまたは半角スラッシュ区切り</param>
        /// <param name="criteriaOperatorDescription">条件値との比較演算に用いる演算子の文字列表現 (※QC3の特殊仕様に従う)</param>
        /// <param name="FilteringFlag">絞り込みフラグ (戻り値)</param>
        /// <param name="reReadDeleteFlag">既に削除フラグ情報を保持しているときに、再度削除ファイルの読み込みを行うかどうかを示すフラグ (省略可、既定値false)</param>
        /// <param name="ope">前条件との演算子を表すOperator列挙型の値 (省略可、既定値Operator.opOr)</param>
        /// <param name="sId">シナリオID</param>
        internal static void EasyFiltering(decimal qcwebid, decimal questionid
                    , string dirpath, out string questionFilePath, out QuestionType questionType
                    , string criteriaValueDescription, string criteriaOperatorDescription
                    , ref bool[] FilteringFlag, bool reReadDeleteFlag = false, Operator ope = Operator.opOr, decimal? sId = null)
        {
            QCWebException exception = null;
            List<Data> data = ReadTextFile.ReadData2(questionid, dirpath, out questionFilePath, out questionType, out exception, reReadDeleteFlag);
            if (exception != null) throw exception;
            QueryItemName query = new QueryItemName();
            List<Data> criteriaData = query.CriteriaValueDescriptionToData(
                        qcwebid, questionType, dirpath, criteriaValueDescription, sId);
            if (criteriaOperatorDescription == null) return;
            if (criteriaData == null)
            {
                EasyFiltering(data, questionType, criteriaValueDescription, criteriaOperatorDescription, ref FilteringFlag, ope);
                return;
            }
            QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA | QuestionType.FA | QuestionType.N);
            CriteriaOperator criteriaOperator = CriteriaOperator.Equal;
            if (criteriaOperatorDescription.Equals("="))
            {
                criteriaOperator = CriteriaOperator.Equal;
            }
            else if (criteriaOperatorDescription.Equals("<>") || criteriaOperatorDescription.Equals("!="))
            {
                criteriaOperator = CriteriaOperator.NotEqual;
            }
            else
            {
                switch (qType)
                {
                    case QuestionType.SA:
                    case QuestionType.N:
                        if (criteriaOperatorDescription.Equals(">"))
                        {
                            criteriaOperator = CriteriaOperator.Greater;
                        }
                        else if (criteriaOperatorDescription.Equals(">="))
                        {
                            criteriaOperator = CriteriaOperator.GreaterEqual;
                        }
                        else if (criteriaOperatorDescription.Equals("<"))
                        {
                            criteriaOperator = CriteriaOperator.Less;
                        }
                        else if (criteriaOperatorDescription.Equals("<="))
                        {
                            criteriaOperator = CriteriaOperator.LessEqual;
                        }
                        else
                        {
                            return;
                        }
                        break;
                    default:
                        return;
                }
            }
            // Filtering201に仲介
            Filtering(data, criteriaData, criteriaOperator, ref FilteringFlag, ope);
        }

        internal static void EasyFiltering(decimal questionid, QuestionType questionType
                    , string criteriaValueDescription, string criteriaOperatorDescription
                    , ref bool[] FilteringFlag, Operator ope = Operator.opOr, string connectionString = null, int catCnt = 0, string tableName = "answers", System.Data.DataTable dt = null)
        {
            QCWebException exception = null;
            List<Data> data = null;
            string path = Path.Combine(Path.GetTempPath(), @"QC4\" + questionid.ToString() + ".txt");
            var dpPath = Path.Combine(Path.GetTempPath(), @"QC4\" + questionid.ToString() + ".dp");
            List<Data> dataList = new List<Data>();
            //if ((File.Exists(path) || File.Exists(dpPath)))
            //{
            //    string filePath = File.Exists(path) ? path : dpPath;
            //    FileInfo info = new FileInfo(filePath);
            //    if (info.Length > 0)
            //    {
            //        try
            //        {
            //            QuestionType QType = QuestionType.SA;

            //            {
            //                //con.Open();

            //                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(connectionString))
            //                {
            //                    con.Open();
            //                    string ansType = DBHelper.GetAnswertype(questionid, con);
            //                    con.Close();
            //                    switch (ansType)
            //                    {
            //                        case "MA":
            //                            QType = QuestionType.MA;
            //                            break;
            //                        case "SA":
            //                            QType = QuestionType.SA;
            //                            break;
            //                        case "N":
            //                            QType = QuestionType.N;
            //                            break;
            //                        case "FA":
            //                            QType = QuestionType.FA;
            //                            break;

            //                    }
            //                }

            //            }
            //            data = ReadTextFile.ReadData(filePath, QType, null, out exception, false);

            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }
            //    }
            //}

            if (dt != null)
            {
                string colName = questionid == 0 ? "sample_id" : "q_" + questionid.ToString();
                data = ReadTextFile.ReadDataTable(dt.DefaultView.ToTable(false, colName), questionType, null, out exception);
            }
            else
            {
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(connectionString))
                {
                    con.Open();
                    System.Data.DataTable dataTble;
                    try
                    {
                        if (questionid == 0)
                        {
                            dataTble = DBHelper.GetDataTable("Select sample_id from " + tableName + " order by sort_no ", con);
                        }
                        else
                        {
                            bool isMv = false;
                            string tableNameMv = getTableName(connectionString, tableName, "q_" + questionid, out isMv);
                            if (!isMv)
                            {
                                dataTble = DBHelper.GetDataTable("Select q_" + questionid + " from " + tableNameMv + " order by sort_no ", con);
                            }
                            else
                            {
                                dataTble = DBHelper.GetDataTable("Select q_" + questionid + " from  " + tableName + " a join "
                                    + tableNameMv + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                            }
                        }


                    }
                    catch (Exception exc)
                    {
                        if (exc.Message.Contains("no such column")) // If no such column, load null data
                        {
                            dataTble = DBHelper.GetDataTable("Select NULL As q_" + questionid + " from " + tableName + " LIMIT 0 ", con);
                        }
                        else
                            throw;
                    }

                    data = ReadTextFile.ReadDataTable(dataTble, questionType, null, out exception);
                }
            }

            // List<Data> data = ReadTextFile.ReadData2(questionid, dirpath, out questionFilePath, out questionType, out exception, reReadDeleteFlag);
            if (exception != null) throw exception;
            QueryItemName query = new QueryItemName();
            //List<Data> criteriaData = query.CriteriaValueDescriptionToData(
            //            qcwebid, questionType, dirpath, criteriaValueDescription, sId);
            if (criteriaOperatorDescription == null) return;
            // if (criteriaData == null)
            // {
            EasyFiltering(data, questionType, criteriaValueDescription, criteriaOperatorDescription, ref FilteringFlag, ope, catCnt, dt);
            return;
            //}
            //QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA | QuestionType.FA | QuestionType.N);
            //CriteriaOperator criteriaOperator = CriteriaOperator.Equal;
            //if (criteriaOperatorDescription.Equals("="))
            //{
            //    criteriaOperator = CriteriaOperator.Equal;
            //}
            //else if (criteriaOperatorDescription.Equals("<>") || criteriaOperatorDescription.Equals("!="))
            //{
            //    criteriaOperator = CriteriaOperator.NotEqual;
            //}
            //else
            //{
            //    switch (qType)
            //    {
            //        case QuestionType.SA:
            //        case QuestionType.N:
            //            if (criteriaOperatorDescription.Equals(">"))
            //            {
            //                criteriaOperator = CriteriaOperator.Greater;
            //            }
            //            else if (criteriaOperatorDescription.Equals(">="))
            //            {
            //                criteriaOperator = CriteriaOperator.GreaterEqual;
            //            }
            //            else if (criteriaOperatorDescription.Equals("<"))
            //            {
            //                criteriaOperator = CriteriaOperator.Less;
            //            }
            //            else if (criteriaOperatorDescription.Equals("<="))
            //            {
            //                criteriaOperator = CriteriaOperator.LessEqual;
            //            }
            //            else
            //            {
            //                return;
            //            }
            //            break;
            //        default:
            //            return;
            //    }
            //}
            //// Filtering201に仲介
            //Filtering(data, criteriaData, criteriaOperator, ref FilteringFlag, ope);
        }

        private static bool checkAfterProcess(string connectionString)
        {
            using (var con = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                using (System.Data.SQLite.SQLiteCommand command = con.CreateCommand())
                {
                    con.Open();
                    string sql = "SELECT count(name) FROM sqlite_master WHERE type='table' AND name='data_after_process'";
                    int cnt = ExecuteScalarr(sql, con);
                    return cnt > 0;
                }
            }
        }

        public static int ExecuteScalarr(string sql, System.Data.SQLite.SQLiteConnection con)
        {
            using (System.Data.SQLite.SQLiteCommand command = con.CreateCommand())
            {
                command.CommandText = sql;
                int RowCount = 0;
                RowCount = Convert.ToInt32(command.ExecuteScalar());
                return RowCount;
            }
        }
        private static string getTableName(string connectionString, string tableName, string columnName, out bool isMv)
        {
            tableName = "answers";
            bool isDAp = false;
            if (checkAfterProcess(connectionString))
            {
                tableName = "data_after_process";
                isDAp = true;
            }
            isMv = false;
            bool colExist = false;
            string tableNameAnswer = tableName;

            using (var conn = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                conn.Open();
                using (System.Data.SQLite.SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

                    System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader();
                    int nameIndex = reader.GetOrdinal("Name");
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Load(reader);

                    int maxRow = dt.Rows.Count;
                    for (int i = 0; i < maxRow; i++)
                    {
                        if (columnName == dt.Rows[i][nameIndex].ToString())
                        {
                            colExist = true;
                            break;
                        }
                    }
                    if (!colExist)
                    {
                        tableName = "multivariate";
                        cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

                        reader = cmd.ExecuteReader();
                        nameIndex = reader.GetOrdinal("Name");
                        dt = new System.Data.DataTable();
                        dt.Load(reader);
                        maxRow = dt.Rows.Count;
                        for (int i = 0; i < maxRow; i++)
                        {
                            if (columnName == dt.Rows[i][nameIndex].ToString())
                            {
                                colExist = true;
                                isMv = isDAp ? true : false;
                                break;
                            }
                        }
                    }

                }
                conn.Close();
            }
            if (colExist)
            {
                return tableName;
            }
            return tableNameAnswer;
        }

        #endregion

        /*
        #region 表記文字列自動設定関連
        /// <alias>nullToDefault</alias>
        /// <summary>
        /// 表記文字列の指定がない場合(null)に、既定値を設定する
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        public static void nullToDefault(QuestionType questionType
                  , ref TabulationDescriptions descs)
        {
            if (descs.PreWBtotalDescription == null)
            {
                descs.PreWBtotalDescription = "WB前全体";   // リソースから読み込むこと
            }
            if (descs.TotalDescription == null)
            {
                descs.TotalDescription = "全体";    // リソースから読み込むこと
            }
            if (descs.TotalAxisDescription == null)
            {
                descs.TotalAxisDescription = "全体";  // リソースから読み込むこと
            }
            if (descs.NADescription == null)
            {
                descs.NADescription = "無回答";  // リソースから読み込むこと
            }
            if (descs.IVDescription == null)
            {
                descs.IVDescription = "非該当";  //リソースから読み込むこと
            }
            if ((questionType & QuestionType.N) == QuestionType.N)
            {
                if (descs.ParameterDescription == null)
                {
                    descs.ParameterDescription = "統計量母数"; //リソースから読み込むこと
                }
                if (descs.SummaryDescription == null)
                {
                    descs.SummaryDescription = "合計";  //リソースから読み込むこと
                }
                if (descs.AverageDescription == null)
                {
                    descs.AverageDescription = "平均";  //リソースから読み込むこと
                }
                if (descs.StdevDescription == null)
                {
                    descs.StdevDescription = "標準偏差";  //リソースから読み込むこと
                }
                if (descs.MinDescription == null)
                {
                    descs.MinDescription = "最小値"; //リソースから読み込むこと
                }
                if (descs.MaxDescription == null)
                {
                    descs.MaxDescription = "最大値"; //リソースから読み込むこと
                }
                if (descs.MedianDescription == null)
                {
                    descs.MedianDescription = "中央値";  //リソースから読み込むこと
                }
            }
        }
        #endregion
        */

        #region 引数チェック
        /// <summary>
        /// 集計時の共通引数チェックルーチン
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="dataCount">データ数</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="sectorsCount">選択肢数 (戻り値)</param>
        /// <param name="keyQsectorDescription">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="keyQsectorsCount">分類アイテムの選択肢数 (戻り値)</param>
        /// <param name="FilteringFlag">絞り込みフラグ配列 (戻り値)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (戻り値)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (戻り値)</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="exception">問題の情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <param name="allowComplex">SA/MAの混在を許可する場合はtrue (省略可、既定値false)</param>
        /// <returns>問題がなければtrue/あればfalse</returns>
        internal static bool checkGTorCrossArguments2(QuestionType questionType
                  , QuestionType keyQuestionType, List<Data> keyData, int dataCount
                  , ref string[] sectorDescription, ref int sectorsCount
                  , ref string[] keyQsectorDescription, ref int keyQsectorsCount
                  , ref bool[] FilteringFlag
                  , ref List<Data> weightback, ref string[] wt, ref TabulationDescriptions descs
                  , out QCWebException exception, bool allowComplex = false)
        {
            exception = null;
            QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA | QuestionType.N);
            if (allowComplex && qType == (QuestionType.SA | QuestionType.MA)) qType = QuestionType.SA;  // チェックだけなので便宜的に
            switch (qType)
            {
                case QuestionType.SA:
                case QuestionType.MA:
                    // SAまたはMAの場合
                    // 選択肢情報がなければNG
                    if (sectorDescription == null || (sectorsCount = sectorDescription.Length) == 0)
                    {
                        exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyTabulationSubjectItemSectorInformationMessageIndex));
                        return false;
                    }
                    // ウエイト値を保持した配列のサイズを調整
                    Array.Resize<string>(ref wt, sectorsCount);
                    break;
                case QuestionType.N:
                    break;
                default:
                    // SAでもMAでもNでもなければNG
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustTabulationSubjectItemQuestionTypeMessageIndex)
                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                 , questionType.ToString());
                    return false;
            }
            // 分類アイテムがある場合
            if ((int)(keyQuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
            {
                // マトリクスだとNG
                if ((keyQuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustCategorizeItemQuestionTypeMessageIndex)
                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                 , keyQuestionType.ToString());
                    return false;
                }
                // 分類アイテムの選択肢情報がなければNG
                if (keyQsectorDescription == null || (keyQsectorsCount = keyQsectorDescription.Length) == 0)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyCategorizeItemSectorInformationMessageIndex));
                    return false;
                }
                // 分類アイテムデータを保持したListオブジェクトがなければNG
                if (keyData == null)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullCategorizeItemDataMessageIndex));
                    return false;
                }
                // 分類アイテムデータを保持したListオブジェクトの要素数がデータ数と一致していなければNG
                if (keyData.Count != dataCount)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustCategorizeItemDataMessageIndex));
                    return false;
                }
            }
            if (dataCount > 0)
            {
                int l = FilteringFlag == null ? 0 : FilteringFlag.Length;
                // 絞り込みフラグ配列のサイズが足りなければ既定値で補充
                Array.Resize(ref FilteringFlag, dataCount);
                for (int i = l; i < dataCount; ++i)
                {
                    FilteringFlag[i] = true;
                }
                // WB値データを保持したListオブジェクトがなければ生成
                if (weightback == null)
                {
                    weightback = new List<Data>();
                }
                // 要素数が多い場合は末尾の超過分をカット
                for (int i = weightback.Count; i > dataCount; --i)
                {
                    weightback.RemoveAt(i - 1);
                }
                // WB値データを保持したListオブジェクトの要素数が足りなければ既定値で補充
                for (int i = weightback.Count; i < dataCount; ++i)
                {
                    weightback.Add(new NData(1.0));
                }
            }
            else
            {
                FilteringFlag = null;
                weightback = null;
            }
            // 表記文字列のチェック
            // GlobalTabulation.nullToDefault(questionType, ref descs);
            if (descs == null) descs = new TabulationDescriptions();
            return true;
        }

        /// <summary>
        /// 集計時の共通引数チェックルーチン
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="sectorsCount">選択肢数 (戻り値)</param>
        /// <param name="keyQsectorDescription">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="keyQsectorsCount">分類アイテムの選択肢数 (戻り値)</param>
        /// <param name="FilteringFlag">絞り込みフラグ配列 (戻り値)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (戻り値)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (戻り値)</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="exception">問題の情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>問題がなければtrue/あればfalse</returns>
        internal static bool checkGTorCrossArguments(
                    QuestionType questionType, QuestionType keyQuestionType
                  , List<Data> data, List<Data> keyData
                  , ref string[] sectorDescription, ref int sectorsCount
                  , ref string[] keyQsectorDescription, ref int keyQsectorsCount
                  , ref bool[] FilteringFlag
                  , ref List<Data> weightback, ref string[] wt, ref TabulationDescriptions descs
                  , out QCWebException exception)
        {
            // データ情報を保持したListオブジェクトがなければNG
            if (data == null)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullParameterMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , "data");
                return false;
            }
            return checkGTorCrossArguments2(questionType, keyQuestionType, keyData, data.Count
                          , ref sectorDescription, ref sectorsCount
                          , ref keyQsectorDescription, ref keyQsectorsCount
                          , ref FilteringFlag, ref weightback, ref wt, ref descs, out exception);
        }
        #endregion

        #region マーキング
        /// <summary>
        /// マーキングの対象となる全体を表すコード
        /// </summary>
        [ComVisible(false)]
        public enum MarkingTotal : int
        {
            /// <summary>
            /// 全体行を対象としてマーキングすることを表す (= 0)
            /// </summary>
            Total,
            /// <summary>
            /// 小計行を対象としてマーキングすることを表す (= 1)
            /// </summary>
            Subtotal
        }

        /// <summary>
        /// ランキングマーキング
        /// </summary>
        /// <remarks>
        /// resultArrayの内容は、WB前全体/無回答/非該当/加重平均などすべての内容を含んだ横N％表イメージ<br />
        /// マーキング情報はN行にのみ付与する
        /// </remarks>
        /// <param name="resultArray">マーキング結果を付与するDataWithMarking型の二次元配列 (戻り値)</param>
        /// <param name="startRow">マーキング対象開始行インデックス</param>
        /// <param name="endRow">マーキング対象終了行インデックス</param>
        /// <param name="startColumn">マーキング対象開始列インデックス</param>
        /// <param name="endColumn">マーキング対象終了列インデックス</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        public static void MarkingRanking(ref DataWithMarking[,] resultArray
                  , int startRow, int endRow, int startColumn, int endColumn
                  , QuestionType questionType)
        {
            // ランキングマーキングのクリア
            for (int r = startRow; r <= endRow; ++r)
            {
                for (int c = startColumn; c <= endColumn; ++c)
                {
                    resultArray[r, c].RemoveMarking(DataMarking.RankingAllBit);
                }
            }
            if ((questionType & QuestionType.N) == QuestionType.N) return;
            int clmsCnt = endColumn - startColumn + 1;
            bool execMarking = clmsCnt >= 1;
            if (!execMarking) return;
            Common.DescComparer desccompare = new Common.DescComparer();
            for (int r = startRow; r <= endRow; ++r)
            {
                double[] data = new double[clmsCnt];
                int[] idx = new int[clmsCnt];
                for (int c = startColumn; c <= endColumn; ++c)
                {
                    data[c - startColumn] = resultArray[r, c].NumValue;
                    idx[c - startColumn] = c;
                }
                Array.Sort(data, idx, desccompare);
                int index = 0;
                int rank = 1;
                while (true)
                {
                    int baseIdx = index;
                    //if (data[index] == 0.0) break;
                    if (Function.Compare(data[index], Function.CompareOperator.Equal, 0.0)) break;
                    DataMarking mark = (DataMarking)((int)DataMarking.Ranking1 * rank);
                    resultArray[r, idx[index]].AppendMarking(mark);
                    //while (++index <= idx.GetUpperBound(0) && data[index] == data[index - 1])
                    while (++index <= idx.GetUpperBound(0) && Function.Compare(data[index], Function.CompareOperator.Equal, data[index - 1]))
                    {
                        resultArray[r, idx[index]].AppendMarking(mark);
                    }
                    rank++;
                    if (rank > 4 || index > idx.GetUpperBound(0)) break;
                }
            }
        }

#if IS_2ND_PHASE
        /// <summary>
        /// 昇降分析マーキング
        /// </summary>
        /// <remarks>
        /// resultArrayの内容は、WB前全体/無回答/非該当/加重平均などすべての内容を含んだ横N％表イメージ<br />
        /// マーキング情報はN行にのみ付与する
        /// </remarks>
        /// <param name="resultArray">マーキング結果を付与するDataWithMarking型の二次元配列 (戻り値)</param>
        /// <param name="startRow">マーキング対象開始行インデックス</param>
        /// <param name="endRow">マーキング対象終了行インデックス</param>
        /// <param name="startColumn">マーキング対象開始列インデックス</param>
        /// <param name="endColumn">マーキング対象終了列インデックス</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        public static void MarkingAscending(ref DataWithMarking[,] resultArray
                  , int startRow, int endRow, int startColumn, int endColumn
                  , QuestionType questionType)
        {
            // 昇降分析マーキングのクリア
            for (int r = startRow; r <= endRow; ++r)
            {
                for (int c = startColumn; c <= endColumn; ++c)
                {
                    resultArray[r, c].RemoveMarking(DataMarking.AscendingAllBit);
                }
            }
            if ((int)(questionType & (QuestionType.N | QuestionType.MatrixParent)) != 0) return;
            int rowsCnt = endRow - startRow + 1;
            bool execMarking = rowsCnt >= 3;
            if (!execMarking) return;
            for (int c = startColumn; c <= endColumn; ++c)
            {
                int comp = resultArray[startRow, c].Percent.CompareTo(resultArray[startRow + 1, c].Percent);
                if (comp == 0) continue;
                bool f = false;
                for (int r = startRow + 1; r <= endRow - 1; ++r)
                {
                    if (resultArray[r, c].Percent.CompareTo(resultArray[r + 2, c].Percent) != comp)
                    {
                        f = true;
                        break;
                    }
                }
                if (f) continue;
                resultArray[(comp == 1 ? startRow : endRow), c].AppendMarking(DataMarking.AscendingStart);
                resultArray[(comp == 1 ? endRow : startRow), c].AppendMarking(DataMarking.AscendingEnd);
                for (int r = startRow + 1; r < endRow; ++r)
                {
                    resultArray[r, c].AppendMarking(DataMarking.AscendingBody);
                }
            }
        }
#endif

        /// <summary>
        /// 全体との差の色付けマーキング
        /// </summary>
        /// <remarks>
        /// resultArrayの内容は、WB前全体/無回答/非該当/加重平均などすべての内容を含んだ横N％表イメージ<br />
        /// マーキング情報はN行にのみ付与する
        /// </remarks>
        /// <param name="resultArray">マーキング結果を付与するDataWithMarking型の二次元配列 (戻り値)</param>
        /// <param name="totalRow">全体行インデックス</param>
        /// <param name="startRow">マーキング対象開始行インデックス</param>
        /// <param name="endRow">マーキング対象終了行インデックス</param>
        /// <param name="startColumn">マーキング対象開始列インデックス</param>
        /// <param name="endColumn">マーキング対象終了列インデックス</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="coloringlevel1Percent">水準1のパーセンテージ (省略可)</param>
        /// <param name="coloringlevel2Percent">水準2のパーセンテージ (省略可)</param>
        public static void MarkingColoring(ref DataWithMarking[,] resultArray
                  , int totalRow, int startRow, int endRow, int startColumn, int endColumn
                  , QuestionType questionType, int coloringlevel1Percent = COLORING_LEVEL1_DEFAULT, int coloringlevel2Percent = COLORING_LEVEL2_DEFAULT)
        {
            // 全体との差の色付けマーキングのクリア
            for (int r = startRow; r <= endRow; ++r)
            {
                for (int c = startColumn; c <= endColumn; ++c)
                {
                    resultArray[r, c].RemoveMarking(DataMarking.ColoringAllBit);
                }
            }
            if ((int)(questionType & (QuestionType.N | QuestionType.MatrixParent)) != 0) return;
            if (coloringlevel1Percent < COLORINGPERCENT_MIN || coloringlevel1Percent > COLORINGPERCENT_MAX)
            {
                coloringlevel1Percent = COLORING_LEVEL1_DEFAULT;
            }
            if (coloringlevel2Percent < COLORINGPERCENT_MIN || coloringlevel2Percent > COLORINGPERCENT_MAX)
            {
                coloringlevel2Percent = COLORING_LEVEL2_DEFAULT;
            }
            if (coloringlevel1Percent > coloringlevel2Percent)
            {
                // 水準1 < 水準2がルール→ここでは水準1 <= 水準2を確実に担保するが、ルール的にこれが走ることはありえない
                coloringlevel2Percent = coloringlevel1Percent;
            }
            for (int c = startColumn; c <= endColumn; ++c)
            {
                double total = resultArray[totalRow, c].Percent;
                for (int r = startRow; r <= endRow; ++r)
                {
                    double d = resultArray[r, c].Percent - total;
                    //if (Math.Abs(d) >= (double)coloringlevel2Percent)
                    if (Function.Compare(Math.Abs(d), Function.CompareOperator.GreaterEqual, (double)coloringlevel2Percent))
                    {
                        resultArray[r, c].AppendMarking(Math.Sign(d) == 1 ? DataMarking.ColoringLevel2High : DataMarking.ColoringLevel2Low);
                    }
                    //else if (Math.Abs(d) >= (double)coloringlevel1Percent)
                    else if (Function.Compare(Math.Abs(d), Function.CompareOperator.GreaterEqual, (double)coloringlevel1Percent))
                    {
                        resultArray[r, c].AppendMarking(Math.Sign(d) == 1 ? DataMarking.ColoringLevel1High : DataMarking.ColoringLevel1Low);
                    }
                }
            }
        }

#if IS_2ND_PHASE
        /// <summary>
        /// 全体との差の有意差検定マーキング
        /// </summary>
        /// <remarks>
        /// resultArrayの内容は、WB前全体/無回答/非該当/加重平均などすべての内容を含んだ横N％表イメージ<br />
        /// マーキング情報はN行にのみ付与する
        /// </remarks>
        /// <param name="resultArray">マーキング結果を付与するDataWithMarking型の二次元配列 (戻り値)</param>
        /// <param name="totalRow">全体行インデックス</param>
        /// <param name="prewbtotalColumn">WB全体列インデックス</param>
        /// <param name="startRow">マーキング対象開始行インデックス</param>
        /// <param name="endRow">マーキング対象終了行インデックス</param>
        /// <param name="startColumn">マーキング対象開始列インデックス</param>
        /// <param name="endColumn">マーキング対象終了列インデックス</param>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        public static void MarkingSignificance(ref DataWithMarking[,] resultArray
                  , int totalRow, int prewbtotalColumn
                  , int startRow, int endRow, int startColumn, int endColumn
                  , QuestionType questionType)
        {
            // 全体との差の有意差検定マーキングのクリア
            for (int r = startRow; r <= endRow; ++r)
            {
                for (int c = startColumn; c <= endColumn; ++c)
                {
                    resultArray[r, c].RemoveMarking(DataMarking.SignificanceAllBit);
                }
            }
            if ((int)(questionType & (QuestionType.N | QuestionType.MatrixParent)) != 0) return;
            for (int c = startColumn; c <= endColumn; ++c)
            {
                double P = resultArray[totalRow, c].Percent / 100.0;
                if (P > 0.0 && P < 1.0)
                //if (Function.Compare(P, Function.CompareOperator.Greater, 0.0) && Function.Compare(P, Function.CompareOperator.Less, 1.0))
                {
                    for (int r = startRow; r <= endRow; r += 2)
                    {
                        int n = (int)resultArray[r, prewbtotalColumn].NumValue;
                        if (n != 0)
                        {
                            double p = resultArray[r, c].Percent / 100.0;
                            if (p > 0.0 && p < 1.0)
                            //if (Function.Compare(p, Function.CompareOperator.Greater, 0.0) && Function.Compare(p, Function.CompareOperator.Less, 1.0))
                            {
                                double z = (p - P) / Math.Sqrt(P * (1.0 - P) / (double)n);
                                double Z = Math.Abs(z);
                                //if (Z > 2.576)  // 1％棄却域に入る
                                if (Function.Compare(Z, Function.CompareOperator.Greater, 2.576))  // 1％棄却域に入る
                                {
                                    resultArray[r, c].AppendMarking(z > 0.0 ? DataMarking.SignificanceOneHigh : DataMarking.SignificanceOneLow);
                                }
                                //else if (Z > 1.96)  // 5％棄却域に入る
                                else if (Function.Compare(Z, Function.CompareOperator.Greater, 1.96))  // 5％棄却域に入る
                                {
                                    resultArray[r, c].AppendMarking(z > 0.0 ? DataMarking.SignificanceFiveHigh : DataMarking.SignificanceFiveLow);
                                }
                                //else if (Z > 1.645) // 10％棄却域に入る
                                else if (Function.Compare(Z, Function.CompareOperator.Greater, 1.645)) // 10％棄却域に入る
                                {
                                    resultArray[r, c].AppendMarking(z > 0.0 ? DataMarking.SignificanceTenHigh : DataMarking.SignificanceTenLow);
                                }
                            }
                        }
                    }
                }
            }
        }
#endif
        #endregion

        #region 中央値算出
        /// <summary>
        /// 中央値を返す
        /// </summary>
        /// <param name="numDatas">データ情報を保持したNumericData型の昇順ソート済み一次元配列</param>
        /// <param name="lastIndex">中央値を求める対象となるインデックスの最大値</param>
        /// <returns>WB値を考慮した中央値</returns>
        public static double GetMedian(NumericData[] numDatas, int lastIndex)
        {
            if (numDatas == null || numDatas.Length == 0) return double.NaN;
            if (lastIndex < 0) return double.NaN;
            if (lastIndex > numDatas.GetUpperBound(0)) lastIndex = numDatas.GetUpperBound(0);
            if (lastIndex == 0) return numDatas[0].Value;
            double wbSum = 0.0;
            for (int i = 0; i <= lastIndex; ++i)
            {
                wbSum += numDatas[i].WeightBack;
            }
            if (wbSum < 0.0) return double.NaN;
            if (wbSum == 0.0) return numDatas[0].Value;
            double wSum = wbSum * 2.0;
            double medPos = double.NaN;
            for (int i = 0; i < 16; ++i)
            {
                double d = Math.Pow(0.1, (double)i);
                if (wbSum + d <= wSum)
                {
                    double tmp = wbSum - d;
                    medPos = tmp / 2.0;
                    break;
                }
            }
            if (double.IsNaN(medPos)) return double.NaN;
            if (medPos == 0.0) return numDatas[0].Value;
            double pos = 0.0;
            for (int i = 0; i < lastIndex; ++i)
            {
                double prePos = pos;
                double wb = numDatas[i].WeightBack;
                pos += wb;
                double overMed = pos - medPos;
                if (overMed < 0.0) continue;
                double d = numDatas[i + 1].Value - numDatas[i].Value;
                return numDatas[i + 1].Value - d * overMed / wb;
            }
            return numDatas[lastIndex].Value;
        }

        /// <summary>
        /// 中央値を返す
        /// </summary>
        /// <param name="normalDatas">データ値を昇順にソートした一次元配列</param>
        /// <param name="lastIndex">中央値を求める対象となるインデックスの最大値</param>
        /// <returns>中央値</returns>
        public static double GetMedian(double[] normalDatas, int lastIndex)
        {
            NumericData[] numDatas = Array.ConvertAll<double, NumericData>(normalDatas, x => new NumericData(x, 1.0));
            return GetMedian(numDatas, lastIndex);
        }
        #endregion

        #region 検定ログ出力
        /// <summary>
        /// 行のヘッダ情報をテキストライターに書き込む
        /// </summary>
        /// <param name="writer">テキストライターへの参照</param>
        /// <param name="arg1">1カラム目の値</param>
        /// <param name="arg2">2カラム目の値</param>
        /// <param name="arg3">3カラム目の値</param>
        /// <param name="arg4">4カラム目の値</param>
        public static void LogWriteLineHeader(TextWriter writer, object arg1, object arg2, object arg3, object arg4)
        {
            if (writer == null) return;
            writer.Write("{0}\t{1}\t{2}\t{3}\t", arg1, arg2, arg3, arg4);
        }

        private static void LogWriteLineData(TextWriter writer, double N0, double N1, double N2, double X1, double X2
                , double q0, double q1, double q2, double X0inOverlap, double X1inOverlap, double X2inOverlap
                , double e0, double e1, double e2, double Z, double t, double d, double p
                , double p1orU1, double p2orU2, double p12orUe
                , string c, string Y1, string Y2
                , string Y1inOverlap, string Y2inOverlap, string meanX1, string meanX2)
        {
            writer.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}\t{23}\t{24}\t{25}\t{26}\t{27}"
                    , N0.ToString("R"), N1.ToString("R"), N2.ToString("R"), X1.ToString("R"), X2.ToString("R"), Y1, Y2, q0.ToString("R"), q1.ToString("R"), q2.ToString("R")
                    , X0inOverlap.ToString("R"), X1inOverlap.ToString("R"), X2inOverlap.ToString("R"), Y1inOverlap, Y2inOverlap
                    , p1orU1.ToString("R"), p2orU2.ToString("R"), p12orUe.ToString("R"), meanX1, meanX2, e0.ToString("R"), e1.ToString("R"), e2.ToString("R"), Z.ToString("R"), t.ToString("R"), d.ToString("R"), p.ToString("R"), c);
        }

        /// <summary>
        /// 行データをテキストライターに書き込む(末尾に改行出力)
        /// </summary>
        /// <param name="writer">テキストライターへの参照</param>
        /// <param name="N0">N0</param>
        /// <param name="N1">N1</param>
        /// <param name="N2">N2</param>
        /// <param name="X1">X1</param>
        /// <param name="X2">X2</param>
        /// <param name="q0">q0</param>
        /// <param name="q1">q1</param>
        /// <param name="q2">q2</param>
        /// <param name="X0inOverlap">Z算出時のX0</param>
        /// <param name="X1inOverlap">Z算出時のX1</param>
        /// <param name="X2inOverlap">Z算出時のX2</param>
        /// <param name="e0">e0</param>
        /// <param name="e1">e1</param>
        /// <param name="e2">e2</param>
        /// <param name="Z">Z値</param>
        /// <param name="t">t値</param>
        /// <param name="d">自由度</param>
        /// <param name="p">p値</param>
        /// <param name="p1orU1">p1またはU1</param>
        /// <param name="p2orU2">p2またはU2</param>
        /// <param name="p12orUe">p12またはUe</param>
        /// <param name="c">c</param>
        /// <param name="Y1">Y1</param>
        /// <param name="Y2">Y2</param>
        /// <param name="Y1inOverlap">Z算出時のY1</param>
        /// <param name="Y2inOverlap">Z算出時のY2</param>
        /// <param name="meanX1">X1の平均</param>
        /// <param name="meanX2">X2の平均</param>
        public static void LogWriteLineData(TextWriter writer, double N0, double N1, double N2, double X1, double X2
                , double q0, double q1, double q2, double X0inOverlap, double X1inOverlap, double X2inOverlap
                , double e0, double e1, double e2, double Z, double t, double d, double p
                , double p1orU1, double p2orU2, double p12orUe
                , object c = null, object Y1 = null, object Y2 = null
                , object Y1inOverlap = null, object Y2inOverlap = null, object meanX1 = null, object meanX2 = null)
        {
            if (writer == null) return;
            /*
            if (c == null) c = "-";
            if (Y1 == null) Y1 = "-";
            if (Y2 == null) Y2 = "-";
            if (Y1inOverlap == null) Y1inOverlap = "-";
            if (Y2inOverlap == null) Y2inOverlap = "-";
            if (meanX1 == null) meanX1 = "-";
            if (meanX2 == null) meanX2 = "-";
            LogWriteLineData(writer, N0, N1, N2, X1, X2, q0, q1, q2
                    , X0inOverlap, X1inOverlap, X2inOverlap, e0, e1, e2, Z, t, d, p, p1orU1, p2orU2, p12orUe
                    , c.ToString(), Y1.ToString(), Y2.ToString()
                    , Y1inOverlap.ToString(), Y2inOverlap.ToString(), meanX1.ToString(), meanX2.ToString());
            */
            string sC = c == null ? "-" : ((double)c).ToString("R");
            string sY1 = Y1 == null ? "-" : ((double)Y1).ToString("R");
            string sY2 = Y2 == null ? "-" : ((double)Y2).ToString("R");
            string sY1inOverlap = Y1inOverlap == null ? "-" : ((double)Y1inOverlap).ToString("R");
            string sY2inOverlap = Y2inOverlap == null ? "-" : ((double)Y2inOverlap).ToString("R");
            string sMeanX1 = meanX1 == null ? "-" : ((double)meanX1).ToString("R");
            string sMeanX2 = meanX2 == null ? "-" : ((double)meanX2).ToString("R");
            LogWriteLineData(writer, N0, N1, N2, X1, X2, q0, q1, q2
                    , X0inOverlap, X1inOverlap, X2inOverlap, e0, e1, e2, Z, t, d, p, p1orU1, p2orU2, p12orUe
                    , sC, sY1, sY2, sY1inOverlap, sY2inOverlap, sMeanX1, sMeanX2);
        }

        /// <summary>
        /// Proportion testのヘッダ行をテキストライターに書き込む(末尾に改行あり)
        /// </summary>
        /// <param name="writer">テキストライターへの参照</param>
        /// <param name="arg1">1カラム目の見出し</param>
        /// <param name="arg2">2カラム目の見出し</param>
        /// <param name="arg3">3カラム目の見出し</param>
        /// <param name="arg4">4カラム目の見出し</param>
        public static void LogWritePTHeaderLine(TextWriter writer, object arg1, object arg2, object arg3, object arg4)
        {
            if (writer == null) return;
            writer.WriteLine("proportion test");
            LogWriteLineHeader(writer, arg1, arg2, arg3, arg4);
            LogWriteLineHeader(writer, "N0", "N1", "N2", "X1");
            LogWriteLineHeader(writer, "X2", "-", "-", "q0");
            LogWriteLineHeader(writer, "q1", "q2", "X0(Z)", "X1(Z)");
            LogWriteLineHeader(writer, "X2(Z)", "-", "-", "p1");
            LogWriteLineHeader(writer, "p2", "p12", "-", "-");
            LogWriteLineHeader(writer, "e0", "e1", "e2", "Z");
            LogWriteLineHeader(writer, "t", "d", "p", "c");
            writer.WriteLine();
        }

        /// <summary>
        /// Proportion testのヘッダ行をテキストライターに書き込む(末尾に改行あり)
        /// </summary>
        /// <param name="writer">ストリームライターへの参照</param>
        /// <param name="arg1">1カラム目の見出し</param>
        /// <param name="arg2">2カラム目の見出し</param>
        /// <param name="arg3">3カラム目の見出し</param>
        /// <param name="arg4">4カラム目の見出し</param>
        public static void LogWriteMTHeaderLine(TextWriter writer, object arg1, object arg2, object arg3, object arg4)
        {
            if (writer == null) return;
            writer.WriteLine("mean test");
            LogWriteLineHeader(writer, arg1, arg2, arg3, arg4);
            LogWriteLineHeader(writer, "N0", "N1", "N2", "X1");
            LogWriteLineHeader(writer, "X2", "Y1", "Y2", "q0");
            LogWriteLineHeader(writer, "q1", "q2", "X0(Z)", "X1(Z)");
            LogWriteLineHeader(writer, "X2(Z)", "Y1(Z)", "Y2(Z)", "U1");
            LogWriteLineHeader(writer, "U2", "Ue", "mX1", "mX2");
            LogWriteLineHeader(writer, "e0", "e1", "e2", "Z");
            LogWriteLineHeader(writer, "t", "d", "p", "-");
            writer.WriteLine();
        }

        /// <summary>
        /// 検定ログ出力に使うストリームライターをインスタンシングして、それへの参照を返す静的メソッド
        /// </summary>
        /// <param name="SignificanceTestLogFilePath">検定ログファイルのパス</param>
        /// <returns>ストリームライターへの参照</returns>
        public static StreamWriter CreateSignificanceTestLogWriter(ref string SignificanceTestLogFilePath)
        {
            if (SignificanceTestLogFilePath == null) return null;
            StreamWriter writer = null;
            try
            {
                if (File.Exists(SignificanceTestLogFilePath))
                {
                    writer = new StreamWriter(SignificanceTestLogFilePath, true, System.Text.Encoding.UTF8);
                    writer.WriteLine(); // 追加のときには1行空行を入れる
                }
                else
                {
                    writer = new StreamWriter(SignificanceTestLogFilePath, false, System.Text.Encoding.UTF8);
                }
            }
            catch
            {
                if (writer != null)
                {
                    writer.Dispose();
                    writer = null;
                }
                SignificanceTestLogFilePath = null;
            }
            return writer;
        }
        #endregion
    }
    #endregion

    #region QueryItemNameクラス
    /// <summary>
    /// アイテム名の問い合わせを行うクラス
    /// </summary>
    [ComVisible(false), Guid("A2DEBA58-E33B-43BA-9907-C896CAAD6D9F")]
    public class QueryItemName
    {
        /// <summary>アイテム情報TBLアクセス</summary>
        protected TItemInfoBhv tItemInfoBhv;

        /// <summary>
        /// アイテム名を基にアイテムIDを取得して返すメソッド
        /// </summary>
        /// <param name="qcwebid">QCWeb管理ID</param>
        /// <param name="allowQType">許容する質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="criteriaValueDescription">条件値(アイテム名)の文字列表現</param>
        /// <param name="sId">シナリオID
        /// <note>
        /// シナリオ編集内から呼び出される場合に指定する必要がある
        /// GT集計設定追加、カテゴリ出力編集で作られたアイテムを検索する場合に必要になる
        /// </note>
        /// </param>
        /// <returns>見つかった場合アイテムID、見つからない場合0</returns>
        public decimal QuestionNameToID(decimal qcwebid, QuestionType allowQType, string criteriaValueDescription, decimal? sId = null)
        {
            decimal defReturn = (decimal)0;
            if (string.IsNullOrWhiteSpace(criteriaValueDescription)) return defReturn;
            QuestionType qType = allowQType & (QuestionType.SA | QuestionType.MA | QuestionType.FA | QuestionType.N);
            if ((int)qType == 0) return defReturn;
            //criteriaValueDescription = criteriaValueDescription.ToUpper();

            QuillInjector.GetInstance().Inject(this);
            // アイテム情報TBLに問い合わせ
            TItemInfoCB tItemInfoCB = new TItemInfoCB();
            // QCWebID = %qcwebid% AND Item_Name = '%criteriaValueDescription%'はこれでよい？
            tItemInfoCB.Query().SetQcwebid_Equal(qcwebid);
            tItemInfoCB.Query().SetItemName_Equal(criteriaValueDescription);
            if (sId != null)
            {
                tItemInfoCB.OrScopeQuery(delegate (TItemInfoCB subcb)
                {
                    subcb.Query().SetCategoryEditId_Equal(sId);
                    subcb.Query().SetCategoryEditId_IsNull();
                });
            }
            else
            {
                tItemInfoCB.Query().SetCategoryEditId_IsNull();
            }
            TItemInfo tItemInfo = tItemInfoBhv.SelectEntity(tItemInfoCB);

            // データが見つからない場合はこれでよい？
            if (tItemInfo == null) return defReturn;

            int aType = 0;
            if (!int.TryParse(tItemInfo.AnswerType, out aType)) return defReturn;
            if (tItemInfo.MatrixDiv == null) return defReturn;
            switch ((int)tItemInfo.MatrixDiv)   // 列挙型はないのか？
            {
                case 0:
                case 2:
                case 3:
                case 4:
                    break;
                default:
                    return defReturn;
            }
            QCAnswerType answertype = (QCAnswerType)aType;
            if (!Enum.IsDefined(typeof(QCAnswerType), answertype) || answertype == QCAnswerType.D) return defReturn;
            QuestionType tmpQType = (QuestionType)Enum.Parse(typeof(QuestionType), answertype.ToString());
            if ((int)(tmpQType & qType) == 0) return defReturn;
            if (tItemInfo.ItemInfoId == null) return defReturn;
            return (decimal)tItemInfo.ItemInfoId;
        }

        /// <summary>
        /// 条件値をアイテム名と見立てて、適切なアイテムが見つかれば、そのデータのListへの参照を返す
        /// </summary>
        /// <param name="qcwebid">QCWeb管理ID</param>
        /// <param name="questionType">元アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="dirpath">データファイルの出力先ディレクトリパス</param>
        /// <param name="criteriaValueDescription">条件値を表す文字列</param>
        /// <param name="sId">シナリオID</param>
        /// <returns></returns>
        public List<Data> CriteriaValueDescriptionToData(decimal qcwebid, QuestionType questionType, string dirpath, string criteriaValueDescription, decimal? sId)
        {
            QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA);
            if (!Enum.IsDefined(typeof(QuestionType), qType)) return null;

            QuestionType allowQType = QuestionType.SA | QuestionType.N | QuestionType.FA;
            if (qType == QuestionType.MA) allowQType = QuestionType.MA;

            decimal questionid = QuestionNameToID(qcwebid, allowQType, criteriaValueDescription, sId);
            if (questionid == (decimal)0) return null;

            string path = null;
            QuestionType questiontype = (QuestionType)0;
            // ローデータTXTパスが未設定の場合
            if (string.IsNullOrEmpty(dirpath))
            {
                ReadDBInfo readDBInfo = new ReadDBInfo();
                QuillInjector.GetInstance().Inject(readDBInfo);
                dirpath = System.IO.Path.Combine(readDBInfo.GetRawdataPath(), qcwebid.ToString());
            }
            QCWebException exception = null;
            return ReadTextFile.ReadData2(questionid, dirpath, out path, out questiontype, out exception);
        }
    }

    #endregion

    #region TabulationDescriptionsクラス

    /// <summary>
    /// 表記文字列を保持するクラス
    /// </summary>
    [ComVisible(false), Guid("05906770-2A3F-4CA4-A20B-DCFD6D3CAA3B")]
    public class TabulationDescriptions
    {
        private TabulationDescriptions tabulationDescriptions;

        /// <summary>
        /// WB前全体の見出しの表記文字列
        /// </summary>
        public string PreWBtotalDescription { get; set; }

        /// <summary>
        /// 全体の見出しの表記文字列
        /// </summary>
        public string TotalDescription { get; set; }

        /// <summary>
        /// 全体の見出し（軸）の表記文字列
        /// </summary>
        public string TotalAxisDescription { get; set; }

        /// <summary>
        /// 無回答の見出しの表記文字列
        /// </summary>
        public string NADescription { get; set; }

        /// <summary>
        /// 非該当の見出しの表記文字列
        /// </summary>
        public string IVDescription { get; set; }

        /// <summary>
        /// 統計量母数の見出しの表記文字列
        /// </summary>
        public string ParameterDescription { get; set; }

        /// <summary>
        /// 合計の見出しの表記文字列
        /// </summary>
        public string SummaryDescription { get; set; }

        /// <summary>
        /// 平均の見出しの表記文字列
        /// </summary>
        public string AverageDescription { get; set; }

        /// <summary>
        /// 標準偏差の見出しの表記文字列
        /// </summary>
        public string StdevDescription { get; set; }

        /// <summary>
        /// 最小値の見出しの表記文字列
        /// </summary>
        public string MinDescription { get; set; }

        /// <summary>
        /// 最大値の見出しの表記文字列
        /// </summary>
        public string MaxDescription { get; set; }

        /// <summary>
        /// 中央値の見出しの表記文字列
        /// </summary>
        public string MedianDescription { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="locale">ロケールID</param>
        public TabulationDescriptions(string locale = "ja")
        {
            PreWBtotalDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportPreWBWholeDescriptionDefaultIndex, locale);
            TotalDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportTargetWholeDescriptionDefaultIndex, locale);
            TotalAxisDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportAxisWholeDescriptionDefaultIndex, locale);
            NADescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportNaDescriptionDefaultIndex, locale);
            IVDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportIvDescriptionDefaultIndex, locale);
            ParameterDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportParameterDescriptionDefaultIndex, locale);
            SummaryDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportSummaryDescriptionDefaultIndex, locale);
            AverageDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportAverageDescriptionDefaultIndex, locale);
            StdevDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportDeviationDescriptionDefaultIndex, locale);
            MinDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMinimumDescriptionDefaultIndex, locale);
            MaxDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMaximumDescriptionDefaultIndex, locale);
            MedianDescription = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMedianDescriptionDefaultIndex, locale);
        }

        public TabulationDescriptions(TabulationDescriptions tabulationDescriptions)
        {
            PreWBtotalDescription = tabulationDescriptions.PreWBtotalDescription;
            TotalDescription = tabulationDescriptions.TotalDescription;
            TotalAxisDescription = tabulationDescriptions.TotalAxisDescription;
            NADescription = tabulationDescriptions.NADescription;
            IVDescription = tabulationDescriptions.IVDescription;
            ParameterDescription = tabulationDescriptions.ParameterDescription;
            SummaryDescription = tabulationDescriptions.SummaryDescription;
            AverageDescription = tabulationDescriptions.AverageDescription;
            StdevDescription = tabulationDescriptions.StdevDescription;
            MinDescription = tabulationDescriptions.MinDescription;
            MaxDescription = tabulationDescriptions.MaxDescription;
            MedianDescription = tabulationDescriptions.MedianDescription;
        }
    }

    #endregion
}
