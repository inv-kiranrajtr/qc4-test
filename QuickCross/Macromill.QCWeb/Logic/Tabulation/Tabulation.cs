#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Tabulation.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/2/15
 * 作　成　者：井川はるき
 * 更　新　日：2012/8/7
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion

#define AFTER_2ND_PHASE
#define IS_2ND_PHASE
#undef AFTER_2ND_PHASE
// #undef IS_2ND_PHASE
// 2014/4/26 井川
#define N0_INCLUDE_NA_IV

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.ReportRequest;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Exceptions;
using System.IO;
using Strings = Microsoft.VisualBasic.Strings;
using Macromill.QCWeb.Model;

namespace Macromill.QCWeb.Tabulation
{
    #region GTTabulationクラス
    /// <summary>
    /// GT表の作成に必要なメソッドをまとめた静的クラス
    /// </summary>
    [ComVisible(false), Guid("417E9B9B-F6AC-43bb-AE5C-652E6DA47CDD")]
    public static class GTTabulation
    {
        #region SAの集計
        /*  →不要 (スコープを広げて直接コールさせるようにするかも？なので置いておく)
        // エイリアス : getSAGTArray01
        // SAのGT表イメージ二次元配列を生成する
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // data                 :   各データを表すSADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照
        // weightback           :   各データのWB値を表すNDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照(dataと同じサイズ)
        // wt                   :   各選択肢のウエイト値を格納した配列(sectorDescriptionと同じサイズ)
        // preWBtotalDescription:   WB前全体の表記文字列
        // totalDescription     :   全体の表記文字列
        // naDescription        :   無回答の表記文字列
        // ivDescription        :   非該当の表記文字列
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , List<Data> data, List<Data> weightback
                , string[] wt, string preWBtotalDescription, string totalDescription
                , string naDescription, string ivDescription, out string[,] resultArray)
        {
            // 引数のチェック
            resultArray = null;
            if (sectorDescription == null) return;
            int sectorsCount = sectorDescription.Count();
            if (weightback == null)
            {
                weightback = new List<Data>();
                for (int i = 0; i < data.Count; ++i)
                {
                    weightback.Add(new NData(1.0));
                }
            }
            Array.Resize(ref wt, sectorsCount);
            if (preWBtotalDescription == null)
            {
                preWBtotalDescription = "WB前全体";   // リソースから読み込むこと
            }
            if (totalDescription == null)
            {
                totalDescription = "全体";    // リソースから読み込むこと
            }
            if (naDescription == null)
            {
                naDescription = "無回答";  // リソースから読み込むこと
            }
            if (ivDescription == null)
            {
                ivDescription = "非該当";  //リソースから読み込むこと
            }

            // 結果の配列のサイズ決定
            resultArray = new string[3 + sectorsCount + 3, 5];

            // 見出し部分の投入
            resultArray[0, 1] = "単一回答"; // リソースから読み込むこと
            resultArray[0, 3] = "n";        // リソースから読み込むこと
            resultArray[0, 4] = "％";       // リソースから読み込むこと
            resultArray[1, 1] = preWBtotalDescription;
            resultArray[2, 1] = totalDescription;
            for (int i = 1; i <= sectorsCount; ++i)
            {
                resultArray[2 + i, 0] = i.ToString();
                resultArray[2 + i, 1] = sectorDescription[i - 1];
            }
            resultArray[2 + sectorsCount + 1, 1] = naDescription;
            resultArray[2 + sectorsCount + 2, 1] = ivDescription;
            resultArray[2 + sectorsCount + 3, 1] = "加重平均";   // リソースから読み込むこと
            
            // 集計(N)
            double[] nArray = new double[2 + sectorsCount + 2];
            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i].DataType != DataType.DeletedData)
                {
                    double wb = ((NData)weightback[i]).Value;
                    nArray[0] += 1.0; // WB前全体
                    nArray[1] += wb; // 全体
                    switch (data[i].DataType)
                    {
                        case DataType.NormalData:
                            int n = ((SAData)data[i]).Value;
                            if (n >= 1 && n <= sectorsCount)
                            {
                                nArray[1 + n] += wb;  // 該当する選択肢 
                            }
                            else
                            {
                                nArray[1 + sectorsCount + 1] += wb; // 無回答扱い
                            }
                            break;
                        case DataType.NAData:
                            nArray[1 + sectorsCount + 1] += wb; // 無回答
                            break;
                        case DataType.IVData:
                            nArray[1 + sectorsCount + 2] += wb; // 非該当
                            break;
                        default:
                            break;
                    }
                }
            }
            // N値の投入
            for (int i = 0; i < nArray.Length; ++i)
            {
                resultArray[1 + i, 3] = nArray[i].ToString();
            }
            // ％値の投入
            if (nArray[1] == 0.0)
            {
                for (int i = 2; i < nArray.Length; ++i)
                {
                    resultArray[1 + i, 4] = "0.0";
                }
            }
            else
            {
                for (int i = 2; i < nArray.Length; ++i)
                {
                    resultArray[1 + i, 4] = (nArray[1 + i] / nArray[1]).ToString();
                }
            }
            // 加重平均の投入
            double s = 0.0;
            int c = 0;
            for (int i = 0; i < sectorsCount; ++i)
            {
                if (wt[i] != null)
                {
                    double w = 0.0;
                    if (double.TryParse(wt[i], out w))
                    {
                        c += 1;
                        s += nArray[1 + i] * w;
                        resultArray[3 + i, 2] = wt[i];
                    }
                }
            }
            if (c > 0)
            {
                resultArray[2 + sectorsCount + 3, 4] = (s / c).ToString();
            }
        }

        // エイリアス : getSAGTArray02
        // SAのGT表イメージ二次元配列を生成する (getSAGTArray01の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // data                 :   各データを表すSADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照
        // weightback           :   ※
        // wt                   :   各選択肢のウエイト値を格納した配列(sectorDescriptionと同じサイズ)
        // preWBtotalDescription:   WB前全体の表記文字列
        // totalDescription     :   全体の表記文字列
        // naDescription        :   無回答の表記文字列
        // ivDescription        :   非該当の表記文字列
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , List<Data> data
                , string[] wt, string preWBtotalDescription, string totalDescription
                , string naDescription, string ivDescription, out string[,] resultArray)
        {
            // getSAGTArray01に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, data, null, wt, preWBtotalDescription, totalDescription, naDescription, ivDescription, out resultArray);
        }

        // エイリアス : getSAGTArray03
        // SAのGT表イメージ二次元配列を生成する (getSAGTArray01の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // data                 :   各データを表すSADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照
        // weightback           :   各データのWB値を表すNDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照(dataと同じサイズ)
        // wt                   :   ※
        // preWBtotalDescription:   WB前全体の表記文字列
        // totalDescription     :   全体の表記文字列
        // naDescription        :   無回答の表記文字列
        // ivDescription        :   非該当の表記文字列
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , List<Data> data, List<Data> weightback
                , string preWBtotalDescription, string totalDescription
                , string naDescription, string ivDescription, out string[,] resultArray)
        {
            // getSAGTArray01に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, data, weightback, null, preWBtotalDescription, totalDescription, naDescription, ivDescription, out resultArray);
        }

        // エイリアス : getSAGTArray04
        // SAのGT表イメージ二次元配列を生成する (getSAGTArray01の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // data                 :   各データを表すSADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照
        // weightback           :   ※
        // wt                   :   ※
        // preWBtotalDescription:   WB前全体の表記文字列
        // totalDescription     :   全体の表記文字列
        // naDescription        :   無回答の表記文字列
        // ivDescription        :   非該当の表記文字列
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , List<Data> data
                , string preWBtotalDescription, string totalDescription
                , string naDescription, string ivDescription, out string[,] resultArray)
        {
            // getSAGTArray01に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, data, null, null, preWBtotalDescription, totalDescription, naDescription, ivDescription, out resultArray);
        }

        // エイリアス : getSAGTArray05
        // SAのGT表イメージ二次元配列を生成する (getSAGTArray01の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // data                 :   各データを表すSADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照
        // weightback           :   各データのWB値を表すNDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照(dataと同じサイズ)
        // wt                   :   各選択肢のウエイト値を格納した配列(sectorDescriptionと同じサイズ)
        // preWBtotalDescription:   ※
        // totalDescription     :   ※
        // naDescription        :   ※
        // ivDescription        :   ※
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , List<Data> data, List<Data> weightback
                , string[] wt, out string[,] resultArray)
        {
            // getSAGTArray01に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, data, weightback, wt, null, null, null, null, out resultArray);
        }

        // エイリアス : getSAGTArray06
        // SAのGT表イメージ二次元配列を生成する (getSAGTArray01の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // data                 :   各データを表すSADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照
        // weightback           :   ※
        // wt                   :   各選択肢のウエイト値を格納した配列(sectorDescriptionと同じサイズ)
        // preWBtotalDescription:   ※
        // totalDescription     :   ※
        // naDescription        :   ※
        // ivDescription        :   ※
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , List<Data> data, string[] wt, out string[,] resultArray)
        {
            // getSAGTArray01に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, data, null, wt, null, null, null, null, out resultArray);
        }

        // エイリアス : getSAGTArray07
        // SAのGT表イメージ二次元配列を生成する (getSAGTArray01の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // data                 :   各データを表すSADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照
        // weightback           :   各データのWB値を表すNDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照(dataと同じサイズ)
        // wt                   :   ※
        // preWBtotalDescription:   ※
        // totalDescription     :   ※
        // naDescription        :   ※
        // ivDescription        :   ※
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , List<Data> data, List<Data> weightback, out string[,] resultArray)
        {
            // getSAGTArray01に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, data, weightback, null, null, null, null, null, out resultArray);
        }

        // エイリアス : getSAGTArray08
        // SAのGT表イメージ二次元配列を生成する (getSAGTArray01の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // data                 :   各データを表すSADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照
        // weightback           :   ※
        // wt                   :   ※
        // preWBtotalDescription:   ※
        // totalDescription     :   ※
        // naDescription        :   ※
        // ivDescription        :   ※
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , List<Data> data, out string[,] resultArray)
        {
            // getSAGTArray01に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, data, null, null, null, null, null, null, out resultArray);
        }

        // エイリアス : getSAGTArray21
        // SAのGT表イメージ二次元配列を生成する
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // QFilePath            :   集計項目データのテキストファイルのパス
        // deleteFlagFilePath   :   削除フラグデータのテキストファイルのパス
        // weightbackFilePath   :   WBデータのテキストファイルのパス
        // wt                   :   各選択肢のウエイト値を格納した配列(sectorDescriptionと同じサイズ)
        // preWBtotalDescription:   WB前全体の表記文字列
        // totalDescription     :   全体の表記文字列
        // naDescription        :   無回答の表記文字列
        // ivDescription        :   非該当の表記文字列
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , string QFilePath, string deleteFlagFilePath, string weightbackFilePath
                , string[] wt, string preWBtotalDescription, string totalDescription
                , string naDescription, string ivDescription, out string[,] resultArray)
        {
            // 各データを表すSADataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を取得
            List<Data> data = ReadTextFile.ReadData(QFilePath, QuestionType.SA, deleteFlagFilePath);
            // 各データのWB値を表すNDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照(dataと同じサイズ)の取得
            List<Data> weightback = null;
            if (weightbackFilePath != null)
            {
                weightback = ReadTextFile.ReadData(weightbackFilePath, QuestionType.N);
            }
            // getSAGTArray01をコール
            getSAGTArray(sectorDescription, data, weightback, wt, preWBtotalDescription, totalDescription, naDescription, ivDescription, out resultArray);
        }

        // エイリアス : getSAGTArray22
        // SAのGT表イメージ二次元配列を生成する  (getSAGTArray21の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // QFilePath            :   集計項目データのテキストファイルのパス
        // deleteFlagFilePath   :   削除フラグデータのテキストファイルのパス
        // weightbackFilePath   :   ※
        // wt                   :   各選択肢のウエイト値を格納した配列(sectorDescriptionと同じサイズ)
        // preWBtotalDescription:   WB前全体の表記文字列
        // totalDescription     :   全体の表記文字列
        // naDescription        :   無回答の表記文字列
        // ivDescription        :   非該当の表記文字列
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , string QFilePath, string deleteFlagFilePath
                , string[] wt, string preWBtotalDescription, string totalDescription
                , string naDescription, string ivDescription, out string[,] resultArray)
        {
            // getSAGTArray21に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, QFilePath, deleteFlagFilePath, null, wt
                        , preWBtotalDescription, totalDescription, naDescription, ivDescription, out resultArray);
        }

        // エイリアス : getSAGTArray23
        // SAのGT表イメージ二次元配列を生成する  (getSAGTArray21の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // QFilePath            :   集計項目データのテキストファイルのパス
        // deleteFlagFilePath   :   削除フラグデータのテキストファイルのパス
        // weightbackFilePath   :   WBデータのテキストファイルのパス
        // wt                   :   ※
        // preWBtotalDescription:   WB前全体の表記文字列
        // totalDescription     :   全体の表記文字列
        // naDescription        :   無回答の表記文字列
        // ivDescription        :   非該当の表記文字列
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , string QFilePath, string deleteFlagFilePath, string weightbackFilePath
                , string preWBtotalDescription, string totalDescription
                , string naDescription, string ivDescription, out string[,] resultArray)
        {
            // getSAGTArray21に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, QFilePath, deleteFlagFilePath, weightbackFilePath, null
                        , preWBtotalDescription, totalDescription, naDescription, ivDescription, out resultArray);
        }

        // エイリアス : getSAGTArray24
        // SAのGT表イメージ二次元配列を生成する  (getSAGTArray21の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // QFilePath            :   集計項目データのテキストファイルのパス
        // deleteFlagFilePath   :   削除フラグデータのテキストファイルのパス
        // weightbackFilePath   :   ※
        // wt                   :   ※
        // preWBtotalDescription:   WB前全体の表記文字列
        // totalDescription     :   全体の表記文字列
        // naDescription        :   無回答の表記文字列
        // ivDescription        :   非該当の表記文字列
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , string QFilePath, string deleteFlagFilePath
                , string preWBtotalDescription, string totalDescription
                , string naDescription, string ivDescription, out string[,] resultArray)
        {
            // getSAGTArray21に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, QFilePath, deleteFlagFilePath, null, null
                        , preWBtotalDescription, totalDescription, naDescription, ivDescription, out resultArray);
        }

        // エイリアス : getSAGTArray25
        // SAのGT表イメージ二次元配列を生成する  (getSAGTArray21の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // QFilePath            :   集計項目データのテキストファイルのパス
        // deleteFlagFilePath   :   削除フラグデータのテキストファイルのパス
        // weightbackFilePath   :   WBデータのテキストファイルのパス
        // wt                   :   各選択肢のウエイト値を格納した配列(sectorDescriptionと同じサイズ)
        // preWBtotalDescription:   ※
        // totalDescription     :   ※
        // naDescription        :   ※
        // ivDescription        :   ※
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , string QFilePath, string deleteFlagFilePath
                , string weightbackFilePath, string[] wt, out string[,] resultArray)
        {
            // getSAGTArray21に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, QFilePath, deleteFlagFilePath, weightbackFilePath, wt
                        , null, null, null, null, out resultArray);
        }

        // エイリアス : getSAGTArray26
        // SAのGT表イメージ二次元配列を生成する  (getSAGTArray21の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // QFilePath            :   集計項目データのテキストファイルのパス
        // deleteFlagFilePath   :   削除フラグデータのテキストファイルのパス
        // weightbackFilePath   :   ※
        // wt                   :   各選択肢のウエイト値を格納した配列(sectorDescriptionと同じサイズ)
        // preWBtotalDescription:   ※
        // totalDescription     :   ※
        // naDescription        :   ※
        // ivDescription        :   ※
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , string QFilePath, string deleteFlagFilePath
                , string[] wt, out string[,] resultArray)
        {
            // getSAGTArray21に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, QFilePath, deleteFlagFilePath, null, wt
                        , null, null, null, null, out resultArray);
        }

        // エイリアス : getSAGTArray27
        // SAのGT表イメージ二次元配列を生成する  (getSAGTArray21の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // QFilePath            :   集計項目データのテキストファイルのパス
        // deleteFlagFilePath   :   削除フラグデータのテキストファイルのパス
        // weightbackFilePath   :   WBデータのテキストファイルのパス
        // wt                   :   ※
        // preWBtotalDescription:   ※
        // totalDescription     :   ※
        // naDescription        :   ※
        // ivDescription        :   ※
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , string QFilePath, string deleteFlagFilePath
                , string weightbackFilePath, out string[,] resultArray)
        {
            // getSAGTArray21に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, QFilePath, deleteFlagFilePath, weightbackFilePath, null
                        , null, null, null, null, out resultArray);
        }

        // エイリアス : getSAGTArray28
        // SAのGT表イメージ二次元配列を生成する  (getSAGTArray21の以下※引数省略型)
        // 引数
        // sectorDescription    :   選択肢文を要素とする配列
        // QFilePath            :   集計項目データのテキストファイルのパス
        // deleteFlagFilePath   :   削除フラグデータのテキストファイルのパス
        // weightbackFilePath   :   ※
        // wt                   :   ※
        // preWBtotalDescription:   ※
        // totalDescription     :   ※
        // naDescription        :   ※
        // ivDescription        :   ※
        // resultArray          :   集計結果の二次元配列 (戻り値)
        private static void getSAGTArray(string[] sectorDescription
                , string QFilePath, string deleteFlagFilePath, out string[,] resultArray)
        {
            // getSAGTArray21に仲介
            // 省略された引数にはnullを与える
            getSAGTArray(sectorDescription, QFilePath, deleteFlagFilePath, null, null
                        , null, null, null, null, out resultArray);
        }
        */
        #endregion

        #region GT表生成関連
        #region 引数チェック
        #region 標準質問用
        /// <summary>
        /// getGTArrayメソッドの引数のチェックを行うサブルーチン
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
        private static bool checkGetGTArrayArguments(
                    QuestionType questionType, QuestionType keyQuestionType
                  , List<Data> data, List<Data> keyData
                  , ref string[] sectorDescription, ref int sectorsCount
                  , ref string[] keyQsectorDescription, ref int keyQsectorsCount
                  , ref bool[] FilteringFlag
                  , ref List<Data> weightback, ref string[] wt, ref TabulationDescriptions descs
                  , out QCWebException exception)
        {
            // マトリクスはNG
            if ((questionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustTabulationSubjectItemQuestionTypeMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , questionType.ToString());
                return false;
            }
            return GlobalTabulation.checkGTorCrossArguments(questionType, keyQuestionType, data, keyData
                        , ref sectorDescription, ref sectorsCount, ref keyQsectorDescription, ref keyQsectorsCount
                        , ref FilteringFlag, ref weightback, ref wt, ref descs, out exception);
        }
        #endregion

        #region マトリクス用
        /// <summary>
        /// getGTArrayメソッドの引数のチェックを行うサブルーチン (マトリクス用)
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="childQuestionType">子質問の質問タイプを表すQuestionType列挙型の値を要素とする配列</param>
        /// <param name="childQuestionDescription">子質問文を要素とする配列</param>
        /// <param name="childQuestionsCount">子質問数 (戻り値)</param>
        /// <param name="dataCount">データ数 (戻り値)</param>
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
        private static bool checkGetGTArrayArguments(
                    QuestionType questionType, QuestionType keyQuestionType
                  , List<List<Data>> data, List<Data> keyData
                  , ref QuestionType[] childQuestionType, string[] childQuestionDescription
                  , ref int childQuestionsCount, ref int dataCount
                  , ref string[] sectorDescription, ref int sectorsCount
                  , ref string[] keyQsectorDescription, ref int keyQsectorsCount
                  , ref bool[] FilteringFlag
                  , ref List<Data> weightback, ref string[] wt, ref TabulationDescriptions descs
                  , out QCWebException exception)
        {
            // マトリクスでなければNG
            if ((questionType & QuestionType.MatrixParent) != QuestionType.MatrixParent)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustTabulationSubjectItemQuestionTypeMessageIndex)
                                             , GlobalsCommonConstant.LogLevel.FATAL
                                             , questionType.ToString());
                return false;
            }
            // データ情報を保持したListオブジェクトがなければNG
            if (data == null || data.Count == 0 || data[0] == null)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyTabulationSubjectItemDataMessageIndex));
                return false;
            }
            // 子質問文情報を保持した配列の要素数がdataの要素数と異なればNG
            childQuestionsCount = data.Count;
            if (childQuestionType == null || childQuestionDescription == null
                || childQuestionType.Length != childQuestionsCount || childQuestionDescription.Length != childQuestionsCount)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustChildQuestionsInformationMessageIndex));
                return false;
            }
            // 子質問の質問タイプが親の質問タイプと整合性が取れない場合はNG
            for (int c = 0; c < childQuestionsCount; ++c)
            {
                childQuestionType[c] &= QuestionType.SA | QuestionType.MA | QuestionType.N;
                //178122
                //if ((int)(childQuestionType[c] & questionType) == 0 || !Enum.IsDefined(typeof(QuestionType), childQuestionType[c]))
                //{
                //   exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustChildQuestionsInformationMessageIndex));
                //    return false;
                //}
            }
            // データ数にブレがあるとNG
            dataCount = data[0].Count;
            for (int i = 1; i < data.Count; ++i)
            {
                if (data[i] == null || data[i].Count != dataCount)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.ExistMismatchBetweenDatasMessageIndex));
                    return false;
                }
            }
            return GlobalTabulation.checkGTorCrossArguments2(
                        questionType, keyQuestionType, keyData, dataCount
                      , ref sectorDescription, ref sectorsCount
                      , ref keyQsectorDescription, ref keyQsectorsCount
                      , ref FilteringFlag, ref weightback, ref wt, ref descs, out exception);
        }
        #endregion
        #endregion

        #region マーキング固有
        private static void MarkingRanking(ref DataWithMarking[,] resultArray
                  , int startRow, int endRow, int startColumn, int endColumn
                  , QuestionType questionType)
        {
            bool isMatrix = (questionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
            if (isMatrix)
            {
                GlobalTabulation.MarkingRanking(ref resultArray, startRow, endRow, startColumn, endColumn, questionType);
            }
            else
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
                int rowsCnt = endRow - startRow + 1;
                bool execMarking = rowsCnt >= 3;
                if (!execMarking) return;
                Common.DescComparer desccompare = new Common.DescComparer();
                for (int c = startColumn; c <= endColumn; ++c)
                {
                    double[] data = new double[rowsCnt];
                    int[] idx = new int[rowsCnt];
                    for (int r = startRow; r <= endRow; ++r)
                    {
                        data[r - startRow] = resultArray[r, c].NumValue;
                        idx[r - startRow] = r;
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
                        resultArray[idx[index], c].AppendMarking(mark);
                        //while (++index <= idx.GetUpperBound(0) && data[index] == data[index - 1])
                        while (++index <= idx.GetUpperBound(0) && Function.Compare(data[index], Function.CompareOperator.Equal, data[index - 1]))
                        {
                            resultArray[idx[index], c].AppendMarking(mark);
                        }
                        if ((rank += index - baseIdx) > 5 || index > 4 || index > idx.GetUpperBound(0)) break;
                    }
                    /*
                    execMarking = data[2] > 0.0;
                    if (!execMarking) continue;
                    int index = 0;
                    DataMarking mark = DataMarking.Ranking1;
                    resultArray[idx[index], c].AppendMarking(mark);
                    while (++index <= idx.GetUpperBound(0) && data[index] == data[index - 1])
                    {
                        resultArray[idx[index], c].AppendMarking(mark);
                    }
                    if (index > 2) continue;
                    mark = index == 1 ? DataMarking.Ranking2 : DataMarking.Ranking3;
                    resultArray[idx[index], c].AppendMarking(mark);
                    while (++index <= idx.GetUpperBound(0) && data[index] == data[index - 1])
                    {
                        resultArray[idx[index], c].AppendMarking(mark);
                    }
                    if (mark == DataMarking.Ranking3 || index > 2) continue;
                    mark = DataMarking.Ranking3;
                    resultArray[idx[index], c].AppendMarking(mark);
                    while (++index <= idx.GetUpperBound(0) && data[index] == data[index - 1])
                    {
                        resultArray[idx[index], c].AppendMarking(mark);
                    }
                    */
                }
            }
        }
        #endregion

        #region 集計表配列生成
        #region 標準質問用
        /// <summary>
        /// 項目間検定
        /// </summary>
        /// <param name="dataArray"></param>
        /// <param name="significanceTestLevel">
        /// 項目間検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は最大2つ
        /// </param>
        /// <param name="writer">検定ログ出力に使用するストリームライターへの参照 (省略可、既定値null)</param>
        /// <param name="keyQName">
        /// 分類アイテム名 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="subHeaderBuffer">
        /// サブヘッダの共通文字列 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        private static void SignificanceTest(ref DataWithMarking[][] dataArray, double[] significanceTestLevel
                , StreamWriter writer = null, string keyQName = null, string subHeaderBuffer = null, int subTotalCount = 0)
        {
            System.Text.StringBuilder ptBuilder = null;
            StringWriter ptWriter = null;
            try
            {
                if (writer != null)
                {
                    ptBuilder = new System.Text.StringBuilder();
                    ptWriter = new StringWriter(ptBuilder);
                }
                for (int k = 0; k < dataArray.Length; ++k)
                {
                    if (ptWriter != null)
                    {
                        ptBuilder.Clear();
                        ptWriter.Write("KeyItem:");
                        if (string.IsNullOrWhiteSpace(keyQName))
                        {
                            ptWriter.WriteLine();
                        }
                        else
                        {
                            ptWriter.WriteLine("{0} = {1}", keyQName, k + 1);
                        }
                        ptWriter.Write(subHeaderBuffer);
                        GlobalTabulation.LogWritePTHeaderLine(ptWriter, "-", "-", "cate1", "cate2");
                    }
                    double N0 = 0.0;
                    double N1 = 0.0;
                    double N2 = 0.0;
                    double q0 = 0.0;
                    double q1 = 0.0;
                    double q2 = 0.0;
                    for (int y = 0; y < dataArray[k].Length; ++y)
                    {
                        DataWithMarking d1 = dataArray[k][y];
                        if (!d1.SettedSectorInformation)
                        {
                            if ((d1.CellType & CellType.TotalCell) == CellType.TotalCell)
                            {
                                N1 = d1.Count;
                                q1 = d1.WBSquareSummary;
                                N2 = N1;
                                N0 = N1;
                                q2 = q1;
                                q0 = q1;
                            }
                            continue;
                        }
                        // if ((d1.CellType & CellType.DataCell) != CellType.DataCell) continue;
                        if ((int)(d1.CellType & (CellType.DataCell | CellType.SimpleDataCell)) == 0) continue;
                        int sNo = d1.SectorNumber;
                        int sCnt = d1.SectorsCount - subTotalCount;
                        if (sNo >= sCnt) continue;
                        for (int s = sNo + 1; s <= sCnt; ++s)
                        {
                            DataWithMarking d2 = dataArray[k][y + s - sNo];
                            double X1 = d1.NumValue;
                            double X2 = d2.NumValue;
                            double X0inOverlap = 0.0;
                            double X1inOverlap = X1;
                            double X2inOverlap = X2;
                            if (d1.HasOverlap)
                            {
                                DetailData od = d1.OverlapData(s);
                                X0inOverlap = od.multipliedSummary;
                                // X1inOverlap = od.summary;
                                // X2inOverlap = od.overlaptargetValueSummary;
                            }
                            double p = 0.0;
                            double e0, e1, e2, Z, t, d;
                            double p1, p2, p12, c;
                            // ログ出力
                            GlobalTabulation.LogWriteLineHeader(ptWriter, "-", "-", sNo, s);
                            Function.RoundOffTDistribution(ref N0, ref N1, ref N2, ref X1, ref X2, ref q0, ref q1, ref q2, ref X0inOverlap, ref X1inOverlap, ref X2inOverlap);
                            p = Function.TDistribution(N0, N1, N2, X1, X2, q0, q1, q2, X0inOverlap, X1inOverlap, X2inOverlap
                                                        , out p1, out p2, out p12, out c, out e0, out e1, out e2
                                                        , out Z, out t, out d);
                            // ログ出力
                            GlobalTabulation.LogWriteLineData(ptWriter, N0, N1, N2, X1, X2, q0, q1, q2
                                    , X0inOverlap, X1inOverlap, X2inOverlap, e0, e1, e2, Z, t, d, p
                                    , p1, p2, p12, c);
                            if (p < 0.0) continue;
                            //if (Function.Compare(p, Function.CompareOperator.LessEqual, 0.0)) continue;
                            p *= 100.0;
                            for (int i = 0; i < significanceTestLevel.Length; ++i)
                            {
                                //if (p <= significanceTestLevel[i])
                                if (Function.Compare(p, Function.CompareOperator.LessEqual, significanceTestLevel[i]))
                                {
                                    if (t >= 0)
                                    //if (Function.Compare(t, Function.CompareOperator.GreaterEqual, 0.0))
                                    {
                                        d1.AppendSignificanceSectorNumber(s, i);
                                    }
                                    else
                                    {
                                        d2.AppendSignificanceSectorNumber(sNo, i);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    if (writer != null) writer.Write(ptBuilder);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (ptWriter != null) ptWriter.Dispose();
            }
        }

        #region →サブルーチン
        /// <alias>getGTArray0000000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0000000</para>
        /// GT表イメージ二次元配列を生成するgetGTArrayオーバーロードメソッド群の根幹ロジック
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <param name="significanceTestCode">項目間検定の種類を表すSignificanceTestCode列挙型のコード値 (省略可、既定値Off)</param>
        /// <param name="significanceTestLevel">
        /// 項目間検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は最大2つ
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="SignificanceTestLogFilePath">
        /// 検定のログファイルの出力先パス (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="qName">
        /// 集計対象アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <param name="keyQName">
        /// 分類アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , List<Data> data, List<Data> keyData, bool[] filteringFlags
                , List<Data> weightback, string[] wt
                , TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl
                , SignificanceTestCode significanceTestCode = SignificanceTestCode.Off
                , double[] significanceTestLevel = null, string SignificanceTestLogFilePath = null
                , string qName = null, string keyQName = null, bool hasCount = false, int subTotalCount = 0, QuestionType qTypeOr = 0, bool isLower = true
                )
        {
            // 戻り値の初期化
            resultArray = null;
            // 引数のチェック
            int sectorsCount = 0;
            int keyQsectorsCount = 0;
            QCWebException exception = null;
            if (!checkGetGTArrayArguments(questionType, keyQuestionType, data, keyData
                    , ref sectorDescriptions, ref sectorsCount, ref keyQsectorDescriptions, ref keyQsectorsCount
                    , ref filteringFlags, ref weightback, ref wt, ref descs, out exception))
            {
                return exception;
            }
            // ウエイト値保持ハッシュテーブル
            Hashtable wtList = new Hashtable();

            // 結果の配列のサイズ決定と見出し部分の投入
            int k = keyQsectorsCount == 0 ? 1 : keyQsectorsCount;
            resultArray = new DataWithMarking[k][,];
            // リテラルが多い…
            QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA | QuestionType.N);
            qTypeOr = qTypeOr & (QuestionType.SA | QuestionType.MA | QuestionType.N);
            switch (qType)
            {
                case QuestionType.SA:   // SA
                case QuestionType.MA:   // MA
                    for (int i = 0; i < k; ++i)
                    {
                        // resultArray[i] = new DataWithMarking[3 + sectorsCount + 3, 4];
                        resultArray[i] = new DataWithMarking[3 + sectorsCount + 4, 4];
                        //resultArray[i][0, 1] = new DataWithMarking(qType == QuestionType.SA ? "単一回答" : "複数回答");  // リソースから読み込むこと
                        resultArray[i][0, 1] = new DataWithMarking(
                            qType == QuestionType.SA
                                                            ? transl.REPORT_SA_DESCRIPTION_KEYWORD
                                                            : transl.REPORT_MA_DESCRIPTION_KEYWORD
                          , false
                        );
                        resultArray[i][0, 3] = new DataWithMarking(transl.REPORT_GT_N_COLUMN_CAPTION, false);
                        resultArray[i][1, 1] = new DataWithMarking(descs.PreWBtotalDescription, false);  // WB前全体
                        resultArray[i][2, 1] = new DataWithMarking(descs.TotalDescription, false);   // 全体
                        for (int j = 1; j <= sectorsCount; ++j)
                        {
                            resultArray[i][2 + j, 0] = new DataWithMarking(j.ToString());
                            resultArray[i][2 + j, 1] = new DataWithMarking(sectorDescriptions[j - 1], false);   // 各選択肢文
                            resultArray[i][2 + j, 1].SetSignificanceCharacters(Strings.LCase(SignificanceTestLetters.Character(j)));
                        }
                        resultArray[i][2 + sectorsCount + 1, 1] = new DataWithMarking(descs.NADescription, false);   // 無回答
                        resultArray[i][2 + sectorsCount + 2, 1] = new DataWithMarking(descs.IVDescription, false);   // 非該当

                        // resultArray[i][2 + sectorsCount + 3, 1] = new DataWithMarking("加重平均");   // リソースから読み込むこと
                        //resultArray[i][2 + sectorsCount + 3, 1] = new DataWithMarking("加重平均母数");    // リソースから読み込むこと
                        if (hasCount)
                        {
                            resultArray[i][2 + sectorsCount + 3, 1] = new DataWithMarking(transl.REPORT_COUNT_AVERAGE_DENOMINATOR_KEYWORD, false);
                            resultArray[i][2 + sectorsCount + 4, 1] = new DataWithMarking(transl.REPORT_COUNT_AVERAGE_KEYWORD, false);
                        }
                        else
                        {
                            resultArray[i][2 + sectorsCount + 3, 1] = new DataWithMarking(transl.REPORT_WEIGHT_AVERAGE_DENOMINATOR_KEYWORD, false);
                            resultArray[i][2 + sectorsCount + 4, 1] = new DataWithMarking(transl.REPORT_WEIGHT_AVERAGE_KEYWORD, false);
                            //resultArray[i][2 + sectorsCount + 3, 1] = new DataWithMarking("ウエイト有効ケース数", false);
                            //resultArray[i][2 + sectorsCount + 4, 1] = new DataWithMarking("ウエイト平均", false);

                        }

                    }
                    for (int i = 0; i < sectorsCount; ++i)
                    {
                        if (!string.IsNullOrWhiteSpace(wt[i]))
                        {
                            double w = 0.0;
                            if (double.TryParse(wt[i], out w))
                            {
                                // キーは選択肢インデックス(1ベース)
                                wtList.Add((i + 1).ToString(), w);
                            }
                        }
                    }


                    break;
                case QuestionType.N:    // N
                    for (int i = 0; i < k; ++i)
                    {
                        resultArray[i] = new DataWithMarking[2, 14];
                        //resultArray[i][0, 1] = new DataWithMarking("数値回答"); // リソースから読み込むこと
                        resultArray[i][0, 1] = new DataWithMarking(transl.REPORT_N_DESCRIPTION_KEYWORD, false);
                        resultArray[i][1, 0] = new DataWithMarking("1");
                        resultArray[i][1, 1] = new DataWithMarking(nQuestionDescription, false);   // 質問文
                        resultArray[i][0, 3] = new DataWithMarking(descs.PreWBtotalDescription, false);  // WB前全体
                        resultArray[i][0, 4] = new DataWithMarking(descs.TotalDescription, false);       // 全体
                        resultArray[i][0, 5] = new DataWithMarking(descs.ParameterDescription, false);   // 統計量母数
                        resultArray[i][0, 6] = new DataWithMarking(descs.SummaryDescription, false);     // 合計
                        resultArray[i][0, 7] = new DataWithMarking(descs.AverageDescription, false);     // 平均
                        resultArray[i][0, 8] = new DataWithMarking(descs.StdevDescription, false);       // 標準偏差
                        resultArray[i][0, 9] = new DataWithMarking(descs.MinDescription, false);         // 最小値
                        resultArray[i][0, 10] = new DataWithMarking(descs.MaxDescription, false);        // 最大値
                        resultArray[i][0, 11] = new DataWithMarking(descs.MedianDescription, false);     // 中央値
                        resultArray[i][0, 12] = new DataWithMarking(descs.NADescription, false);         // 無回答
                        resultArray[i][0, 13] = new DataWithMarking(descs.IVDescription, false);         // 非該当
                    }
                    break;
            }

            // 項目間検定設定の補正
            bool SignificanceTestOn = qType != QuestionType.N && significanceTestCode == SignificanceTestCode.BetweenSectors;
            if (SignificanceTestOn)
            {
                if (significanceTestLevel == null || significanceTestLevel.Length == 0)
                {
                    SignificanceTestOn = false;
                }
                else
                {
                    if (significanceTestLevel.Length > 2) Array.Resize<double>(ref significanceTestLevel, 2);
                    if (significanceTestLevel.Length == 2 && significanceTestLevel[0] == significanceTestLevel[1])
                    {
                        Array.Resize<double>(ref significanceTestLevel, 1);
                    }
                    if (significanceTestLevel.Length == 1)
                    {
                        SignificanceTestOn = significanceTestLevel[0] > 0.0 && significanceTestLevel[0] <= 10.0;
                    }
                    else
                    {
                        Array.Sort<double>(significanceTestLevel);
                        bool[] OnFlag = new bool[significanceTestLevel.Length];
                        for (int i = 0; i < significanceTestLevel.Length; ++i)
                        {
                            OnFlag[i] = significanceTestLevel[i] > 0.0 && significanceTestLevel[i] <= 10.0;
                        }
                        if (OnFlag[0] ^ OnFlag[1])
                        {
                            if (OnFlag[1]) significanceTestLevel[0] = significanceTestLevel[1];
                            Array.Resize<double>(ref significanceTestLevel, 1);
                        }
                        else
                        {
                            SignificanceTestOn = OnFlag[0];
                        }
                    }
                }
            }
            if (SignificanceTestOn)
            {
                if (SignificanceTestLogFilePath != null)
                {
                    if (string.IsNullOrWhiteSpace(SignificanceTestLogFilePath) || string.IsNullOrWhiteSpace(qName)
                        || SignificanceTestLogFilePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                    {
                        SignificanceTestLogFilePath = null;
                    }
                }
            }
            else
            {
                significanceTestCode = SignificanceTestCode.Off;
                SignificanceTestLogFilePath = null;
            }

            // 集計
            double[][] nArray = null; // 集計値を格納する配列
            DataWithMarking[][] dataArray = null;   // 詳細な集計値を格納する配列
            if (SignificanceTestOn)
            {
                dataArray = new DataWithMarking[k][];
            }
            else
            {
                nArray = new double[k][];
            }

            // N質問集計時に、最大値, 最小値, 中央値を出すための配列
            // double[][] normalDatas = new double[k][];
            NumericData[][] normalDatas = new NumericData[k][];
            int[] lastIndex = null; // normalDatasの使用最大インデックス
            // double[] effectiveSamplesCount = null;  // 加重平均用統計量母数
            // 集計値を格納する配列のサイズ定義
            switch (qType)
            {
                case QuestionType.SA:   // SA
                case QuestionType.MA:   // MA
                    // if (wtList.Count > 0) effectiveSamplesCount = new double[k];
                    for (int i = 0; i < k; ++i)
                    {
                        if (SignificanceTestOn)
                        {
                            dataArray[i] = new DataWithMarking[2 + sectorsCount + 3];
                            dataArray[i][0] = new DataWithMarking(CellType.PreWBTotalCell);
                            dataArray[i][1] = new DataWithMarking(CellType.TotalCell);
                            for (int s = 0; s < sectorsCount; ++s)
                            {
                                dataArray[i][2 + s] = new DataWithMarking(s + 1, sectorsCount, CellType.SimpleDataCell, qType == QuestionType.MA);
                            }
                            // 加重平均母数列の詳細集計は不要なので、無回答列などと同等に扱う
                            for (int c = 0; c < 3; ++c)
                            {
                                dataArray[i][2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                            }
                        }
                        else
                        {
                            // nArray[i] = new double[2 + sectorsCount + 2];
                            // if (wtList.Count > 0) effectiveSamplesCount[i] = 0.0;
                            nArray[i] = new double[2 + sectorsCount + 3];
                        }
                    }
                    break;
                case QuestionType.N:    // N
                    lastIndex = new int[k];
                    for (int i = 0; i < k; ++i)
                    {
                        nArray[i] = new double[7];
                        // normalDatas[i] = new double[data.Count];
                        normalDatas[i] = new NumericData[data.Count];
                        lastIndex[i] = -1;
                    }
                    break;
            }

            // オーバーラップ分集計用リスト
            List<DataWithMarking> tmpList = null;
            if (SignificanceTestOn && qType == QuestionType.MA)
            {
                tmpList = new List<DataWithMarking>();
            }
            if (TabulateFullQuantity) CutNA = false;
            // データ走査
            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i].IsDeleted || !filteringFlags[i]) continue;
                // WB
                double wb = (weightback[i] as NData).Value;
                // 結果配列一段階インデックス
                List<int> keyIdx = new List<int>();
                if (keyQsectorsCount == 0)  // 分類アイテムなし
                {
                    keyIdx.Add(0);
                }
                else  // 分類アイテムあり
                {
                    if (keyData[i].DataType == DataType.NormalData)
                    {
                        switch (keyQuestionType & (QuestionType.SA | QuestionType.MA))
                        {
                            case QuestionType.SA:
                                {
                                    int n = (keyData[i] as SAData).Value;
                                    if (n >= 1 && n <= keyQsectorsCount)
                                    {
                                        keyIdx.Add(n - 1);
                                    }
                                    break;
                                }
                            case QuestionType.MA:
                                {
                                    /*
                                    for (int j = 0; j < keyQsectorsCount; ++j)
                                    {
                                        int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                        int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                        if (((keyData[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                        {
                                            int n = j + 1;
                                            keyIdx.Add(n - 1);
                                        }
                                    }
                                    */
                                    int[] sectors = (keyData[i] as MAData).SectorsArray;
                                    if (sectors != null)
                                    {
                                        for (int j = 0; j < sectors.Length; ++j)
                                        {
                                            int n = sectors[j];
                                            if (n <= keyQsectorsCount)
                                            {
                                                keyIdx.Add(n - 1);
                                            }
                                        }
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
                // 分類アイテムごとに集計
                for (int j = 0; j < keyIdx.Count; ++j)
                {
                    int x = keyIdx[j];
                    if (tmpList != null) tmpList.Clear();
                    // if (data[i].DataType != DataType.IVData || TabulateFullQuantity)
                    bool IncrementTotal = true;
                    switch (data[i].DataType)
                    {
                        case DataType.NAData:
                            IncrementTotal = !CutNA;
                            break;
                        case DataType.IVData:
                            IncrementTotal = TabulateFullQuantity;
                            break;
                    }
                    /*
                    if (IncrementTotal)
                    {
                        // WB前全体
                        nArray[x][0] += 1.0;
                        // 全体
                        nArray[x][1] += wb;
                    }
                    */
                    // int y = nArray[x].GetUpperBound(0) - 1;
                    int y = 0;
                    int validCases = 0;
                    if (SignificanceTestOn)
                    {
                        y = dataArray[x].GetUpperBound(0) - 2;
                        validCases = dataArray[x].GetUpperBound(0);
                    }
                    else
                    {
                        y = nArray[x].GetUpperBound(0) - (qType == QuestionType.N ? 1 : 2);
                        validCases = nArray[x].GetUpperBound(0);
                    }
                    switch (data[i].DataType)
                    {
                        case DataType.NAData:   // 無回答
                            if (SignificanceTestOn)
                            {
                                dataArray[x][y].AddDetail(1.0, wb);
                                if (!isLower)
                                {
                                    dataArray[x][validCases].AddDetail(1.0, wb);
                                }
                            }
                            else
                            {
                                nArray[x][y] += wb;
                                if (!isLower) nArray[x][validCases] += wb;
                            }
                            break;
                        case DataType.IVData:   // 非該当
                            if (SignificanceTestOn)
                            {
                                dataArray[x][y + (TabulateFullQuantity && IVtoNA ? 0 : 1)].AddDetail(1.0, wb);
                            }
                            else
                            {
                                nArray[x][y + (TabulateFullQuantity && IVtoNA ? 0 : 1)] += wb;
                            }
                            break;
                        case DataType.NormalData:   // 通常データ
                            switch (qType)
                            {
                                case QuestionType.SA:
                                    {
                                        int n = (data[i] as SAData).Value;
                                        if (n >= 1 && n <= sectorsCount)
                                        {
                                            bool weighted = wtList.ContainsKey(n.ToString());
                                            if (SignificanceTestOn)
                                            {
                                                dataArray[x][1 + n].AddDetail(1.0, wb);
                                                if (weighted) dataArray[x][y + 2].AddDetail(1.0, wb);
                                            }
                                            else
                                            {
                                                nArray[x][1 + n] += wb; // 該当する選択肢
                                                if (weighted)
                                                {
                                                    // effectiveSamplesCount[x] += wb;
                                                    nArray[x][y + 2] += wb; // 統計量母数
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // 無回答扱い
                                            IncrementTotal = !CutNA;
                                            if (SignificanceTestOn)
                                            {
                                                dataArray[x][y].AddDetail(1.0, wb);
                                            }
                                            else
                                            {
                                                nArray[x][y] += wb;
                                            }
                                        }
                                        break;
                                    }
                                case QuestionType.MA:
                                    {
                                        int[] sectors = (data[i] as MAData).SectorsArray;
                                        bool isNA = true;
                                        bool isEffectiveSample = false;
                                        /*
                                        for (int m = 0; m < sectorsCount; ++m)
                                        {
                                            int idx = m / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            int e = m % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                            {
                                                int n = m + 1;
                                                nArray[x][1 + n] += wb; // 該当する選択肢
                                                if (!isEffectiveSample) isEffectiveSample = wtList.ContainsKey(n.ToString());
                                            }
                                        }
                                        */
                                        if (sectors != null)
                                        {
                                            for (int m = 0; m < sectors.Length; ++m)
                                            {
                                                int n = sectors[m];
                                                if (n <= sectorsCount)
                                                {
                                                    if (SignificanceTestOn)
                                                    {
                                                        dataArray[x][1 + n].AddDetail(1.0, wb);
                                                        for (int t = 0; t < tmpList.Count; ++t)
                                                        {
                                                            tmpList[t].AddOverlapDetail(n, 1.0, 1.0, wb);
                                                        }
                                                        tmpList.Add(dataArray[x][1 + n]);
                                                    }
                                                    else
                                                    {
                                                        nArray[x][1 + n] += wb; // 該当する選択肢
                                                    }
                                                    isNA = false;
                                                    if (!isEffectiveSample) isEffectiveSample = wtList.ContainsKey(n.ToString());
                                                }
                                            }
                                        }
                                        if (isNA)
                                        {
                                            IncrementTotal = !CutNA;
                                            if (SignificanceTestOn)
                                            {
                                                dataArray[x][y].AddDetail(1.0, wb);
                                            }
                                            else
                                            {
                                                nArray[x][y] += wb; // 無回答扱い
                                            }
                                        }
                                        // if (isEffectiveSample) effectiveSamplesCount[x] += wb;
                                        if (isEffectiveSample)
                                        {
                                            if (SignificanceTestOn)
                                            {
                                                dataArray[x][y + 2].AddDetail(1.0, wb);
                                            }
                                            else
                                            {
                                                nArray[x][y + 2] += wb;  // 統計量母数
                                            }
                                        }
                                        break;
                                    }
                                case QuestionType.N:
                                    {
                                        double v = (data[i] as NData).Value;
                                        // 値を個別に確保
                                        // normalDatas[x][++lastIndex[x]] = v;
                                        // if (wb > 0.0) normalDatas[x][++lastIndex[x]] = v;
                                        if (wb > 0.0) normalDatas[x][++lastIndex[x]] = new NumericData(v, wb);
                                        // 統計量母数
                                        nArray[x][2] += wb;
                                        // 合計
                                        nArray[x][3] += v * wb;
                                        // 平方の合計
                                        nArray[x][4] += Math.Pow(v, 2.0) * wb;
                                        break;
                                    }
                            }
                            break;
                        default:
                            break;
                    }
                    if (IncrementTotal)
                    {
                        if (SignificanceTestOn)
                        {
                            dataArray[x][0].AddDetail(1.0, 1.0);
                            dataArray[x][1].AddDetail(1.0, wb);
                        }
                        else
                        {
                            // WB前全体
                            nArray[x][0] += 1.0;
                            // 全体
                            nArray[x][1] += wb;
                        }
                    }
                }
            }

            // normalDatasのソート (クイックソート)
            if (qType == QuestionType.N)
            {
                for (int i = 0; i < k; ++i)
                {
                    // if (lastIndex[i] > 0) Array.Sort<double>(normalDatas[i], 0, lastIndex[i] + 1);
                    if (lastIndex[i] > 0) Array.Sort<NumericData>(normalDatas[i], 0, lastIndex[i] + 1);
                }
            }

            if (SignificanceTestOn)
            {
                StreamWriter writer = GlobalTabulation.CreateSignificanceTestLogWriter(ref SignificanceTestLogFilePath);
                try
                {
                    string subHeaderBuffer = null;
                    if (writer != null)
                    {
                        writer.WriteLine("GT SignificanceTest between categories (" + qTypeOr.ToString() + ")");
                        System.Text.StringBuilder builder = new System.Text.StringBuilder("");
                        builder.AppendLine(string.Format("Item:{0}", qName));
                        builder.AppendLine("\tCategory");
                        for (int i = 0; i < sectorsCount; ++i)
                        {
                            builder.AppendLine(string.Format("\t\t{0}:{1}", i + 1, sectorDescriptions[i]));
                        }
                        subHeaderBuffer = builder.ToString();
                    }
                    SignificanceTest(ref dataArray, significanceTestLevel, writer, keyQName, subHeaderBuffer, subTotalCount);
                    if (writer != null) writer.Close();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (writer != null) writer.Dispose();
                }
            }

            // 出力配列の仕上げ
            for (int i = 0; i < k; ++i)
            {
                switch (qType)
                {
                    case QuestionType.SA:
                    case QuestionType.MA:
                        // N値またはキャプションの投入
                        if (SignificanceTestOn)
                        {
                            for (int j = 0; j < dataArray[i].Length; ++j)
                            {
                                resultArray[i][1 + j, 3] = dataArray[i][j];
                            }
                        }
                        else
                        {
                            for (int j = 0; j < nArray[i].Length; ++j)
                            {
                                resultArray[i][1 + j, 3] = new DataWithMarking(nArray[i][j].ToString());
                            }
                        }
                        // ％値の投入
                        if (SignificanceTestOn)
                        {
                            if (dataArray[i][1].NumValue != 0.0)
                            {
                                for (int j = 2; j < dataArray[i].Length - 1; ++j)
                                {
                                    resultArray[i][1 + j, 3].Percent = dataArray[i][j].NumValue * 100.0 / dataArray[i][1].NumValue;
                                }
                            }
                        }
                        else
                        {
                            /*
                            if (nArray[i][1] == 0.0)
                            {
                                // for (int j = 2; j < nArray[i].Length; ++j)
                                for (int j = 2; j < nArray[i].Length - 1; ++j)
                                {
                                    resultArray[i][1 + j, 3].Percent = 0.0;
                                }
                            }
                            else
                            {
                                // for (int j = 2; j < nArray[i].Length; ++j)
                                for (int j = 2; j < nArray[i].Length - 1; ++j)
                                {
                                    resultArray[i][1 + j, 3].Percent = nArray[i][j] * 100.0 / nArray[i][1];
                                }
                            }
                            */
                            if (nArray[i][1] != 0.0)
                            {
                                for (int j = 2; j < nArray[i].Length - 1; ++j)
                                {
                                    resultArray[i][1 + j, 3].Percent = nArray[i][j] * 100.0 / nArray[i][1];
                                }
                            }
                        }
                        // 加重平均の投入
                        /*
                        double s = 0.0;
                        int c = 0;
                        for (int j = 0; j < sectorsCount; ++j)
                        {
                            if (!string.IsNullOrWhiteSpace(wt[j]))
                            {
                                double w = 0.0;
                                if (double.TryParse(wt[j], out w))
                                {
                                    ++c;
                                    s += nArray[i][2 + j] * w;
                                    resultArray[i][3 + j, 2] = new DataWithMarking(wt[j]);
                                }
                            }
                        }
                        if (c > 0)
                        {
                            resultArray[i][2 + sectorsCount + 3, 3] = new DataWithMarking((s / c).ToString());
                        }
                        */
                        if (wtList.Count > 0)
                        {
                            double s = 0.0;
                            for (int j = 1; j <= sectorsCount; ++j)
                            {
                                if (wtList.ContainsKey(j.ToString()))
                                {
                                    if (SignificanceTestOn)
                                    {
                                        s += dataArray[i][1 + j].NumValue * (double)wtList[j.ToString()];
                                    }
                                    else
                                    {
                                        s += nArray[i][1 + j] * (double)wtList[j.ToString()];
                                    }
                                    resultArray[i][2 + j, 2] = new DataWithMarking(wt[j - 1]);
                                }
                            }
                            /*
                            if (effectiveSamplesCount[i] > 0.0)
                            {
                                resultArray[i][2 + sectorsCount + 3, 3] = new DataWithMarking((s / effectiveSamplesCount[i]).ToString());
                            }
                            */
                            if (SignificanceTestOn)
                            {
                                // if (dataArray[i][1 + sectorsCount + 3].Count > 0.0)
                                if (dataArray[i][1 + sectorsCount + 3].NumValue > 0.0)
                                {
                                    // resultArray[i][2 + sectorsCount + 4, 3] = new DataWithMarking((s / dataArray[i][1 + sectorsCount + 3].Count).ToString());
                                    resultArray[i][2 + sectorsCount + 4, 3] = new DataWithMarking((s / dataArray[i][1 + sectorsCount + 3].NumValue).ToString());
                                }
                                else
                                {
                                    resultArray[i][2 + sectorsCount + 4, 3] = new DataWithMarking("-", false);
                                }
                            }
                            else
                            {
                                if (nArray[i][1 + sectorsCount + 3] > 0.0)
                                {
                                    resultArray[i][2 + sectorsCount + 4, 3] = new DataWithMarking((s / nArray[i][1 + sectorsCount + 3]).ToString());
                                }
                                else
                                {
                                    // resultArray[i][2 + sectorsCount + 4, 3] = new DataWithMarking("0.0");
                                    resultArray[i][2 + sectorsCount + 4, 3] = new DataWithMarking("-", false);
                                }
                            }
                        }
                        // ランキングマーキング
                        MarkingRanking(ref resultArray[i], 3, 2 + sectorsCount, 3, 3, questionType);
                        break;
                    case QuestionType.N:
                        resultArray[i][1, 3] = new DataWithMarking(nArray[i][0].ToString());   // WB前全体
                        resultArray[i][1, 4] = new DataWithMarking(nArray[i][1].ToString());   // 全体
                        resultArray[i][1, 5] = new DataWithMarking(nArray[i][2].ToString());   // 統計量母数
                        // resultArray[i][1, 6] = new DataWithMarking(nArray[i][3].ToString());   // 合計
                        resultArray[i][1, 6] = new DataWithMarking(nArray[i][2] == 0.0 ? "-" : nArray[i][3].ToString());   // 合計
                        // 平均
                        // double average = 0.0;
                        double average = double.NaN;
                        if (nArray[i][2] != 0.0)
                        {
                            average = nArray[i][3] / nArray[i][2];  // 合計÷統計量母数
                        }
                        // resultArray[i][1, 7] = new DataWithMarking(average.ToString());
                        resultArray[i][1, 7] = new DataWithMarking(double.IsNaN(average) ? "-" : average.ToString());
                        // 標準偏差
                        // double stdev = 0.0;
                        double stdev = double.NaN;
                        if (nArray[i][2] > 1.0)
                        {
                            // 統計量母数×平方の合計－合計の平方

                            //double tmp = nArray[i][2] * nArray[i][4] - Math.Pow(nArray[i][3], 2.0);
                            //tmp /= nArray[i][2] * (nArray[i][2] - 1);
                            //stdev = Math.Sqrt(tmp);
                            //var da=normalDatas;

                            double weightSum = 0;
                            double standardDeviation = 0;
                            double normalValue = 0;
                            double totalCount = nArray[i][2];
                            double weightedMean = nArray[i][3] / totalCount;
                            for (int l = 0; l < normalDatas[i].Length; l++)
                            {
                                normalValue = normalDatas[i][l].Value;
                                standardDeviation += normalDatas[i][l].WeightBack *
                                    Math.Pow((normalValue - weightedMean), 2.0);
                            }
                            stdev = Math.Sqrt(standardDeviation / (((totalCount - 1) / totalCount) * totalCount));

                        }
                        // resultArray[i][1, 8] = new DataWithMarking(stdev.ToString());
                        resultArray[i][1, 8] = new DataWithMarking(double.IsNaN(stdev) ? "-" : stdev.ToString());
                        // 最小値
                        // double min = 0.0;
                        double min = double.NaN;
                        // 最大値
                        // double max = 0.0;
                        double max = double.NaN;
                        // 中央値
                        // double median = 0.0;
                        double median = double.NaN;
                        if (lastIndex[i] > -1)
                        {
                            // min = normalDatas[i][0];
                            // max = normalDatas[i][lastIndex[i]];
                            min = normalDatas[i][0].Value;
                            max = normalDatas[i][lastIndex[i]].Value;
                            /*
                            int medIdx = lastIndex[i] / 2;
                            //if (lastIndex[i] % 2 == 0) // 要素数が奇数
                            //{
                            //    median = normalDatas[i][medIdx];
                            //}
                            //else    // 要素数が偶数
                            //{
                            //    median = (normalDatas[i][medIdx] + normalDatas[i][medIdx + 1]) / 2.0;
                            //}
                            median = normalDatas[i][medIdx];
                            // 中央値のインデックス相関位置
                            // double medPos = ((double)lastIndex[i] + 1.0 + 1.0) / 2.0 - 1.0;
                            double medPos = (double)lastIndex[i] / 2.0;
                            // 位置によって按分した、normalDatas[i][medIdx]との差分
                            if (medPos > (double)medIdx)    // 按分が必要な場合だけ処理し、またlastIndex[i] == 0のときにコケないように分岐
                            {
                                double d = (normalDatas[i][medIdx + 1] - normalDatas[i][medIdx]) * (medPos - (double)medIdx);
                                median += d;
                            }
                            */
                            median = GlobalTabulation.GetMedian(normalDatas[i], lastIndex[i]);
                        }
                        // resultArray[i][1, 9] = new DataWithMarking(min.ToString());
                        // resultArray[i][1, 10] = new DataWithMarking(max.ToString());
                        // resultArray[i][1, 11] = new DataWithMarking(median.ToString());
                        resultArray[i][1, 9] = new DataWithMarking(double.IsNaN(min) ? "-" : min.ToString());
                        resultArray[i][1, 10] = new DataWithMarking(double.IsNaN(max) ? "-" : max.ToString());
                        resultArray[i][1, 11] = new DataWithMarking(double.IsNaN(median) ? "-" : median.ToString());
                        resultArray[i][1, 12] = new DataWithMarking(nArray[i][5].ToString());  // 無回答
                        resultArray[i][1, 13] = new DataWithMarking(nArray[i][6].ToString());  // 非該当
                        break;
                    default:
                        break;
                }
            }
            return null;
        }

        /// <alias>getGTArray0100000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0100000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数で受け取ったファイルパスを使ってデータを取得してgetGTArray0000000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Boolean[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0000000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath, string deleteFlagFilePath, bool[] filteringFlags
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity, bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            resultArray = null;
            QCWebException exception = null;
            // 各データを表すDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を取得
            List<Data> data = ReadTextFile.ReadData(qFilePath, questionType, deleteFlagFilePath, out exception);
            if (data == null) return exception;
            // 分類アイテムデータを表すDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を取得
            List<Data> keyData = null;
            if (!string.IsNullOrWhiteSpace(keyQFilePath))
            {
                keyData = ReadTextFile.ReadData(keyQFilePath, keyQuestionType, out exception);
                if (keyData == null) return exception;
            }
            // 各データのWB値を表すNDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照(dataと同じサイズ)の取得
            List<Data> weightback = null;
            if (!string.IsNullOrWhiteSpace(weightbackFilePath))
            {
                weightback = ReadTextFile.ReadData(weightbackFilePath, QuestionType.N, out exception);
                if (weightback == null) return exception;
            }
            // getGTArray0000000をコール
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData, filteringFlags, weightback, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0200000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0200000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数deleteFlagFilePathにnullを指定してgetGTArray0100000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0100000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath, bool[] filteringFlags
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity, bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            string deleteFlagFilePath = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, deleteFlagFilePath, filteringFlags, weightbackFilePath, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1000000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1000000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 分類アイテム情報関連の引数にnullを指定してgetGTArray0000000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Boolean[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0000000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , List<Data> data, bool[] filteringFlags
                , List<Data> weightback, string[] wt
                , TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            QuestionType keyQuestionType = (QuestionType)0;
            string[] keyQsectorDescriptions = null;
            List<Data> keyData = null;
            DataWithMarking[][,] res = null;
            QCWebException exception = GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData, filteringFlags, weightback, wt
                                                , descs, nQuestionDescription
                                                , out res, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
            resultArray = null;
            if (exception != null) return exception;
            if (res != null && res.Length == 1) resultArray = res[0];
            return null;
        }

        /// <alias>getGTArray1100000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1100000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 分類アイテム情報関連の引数にnullを指定してgetGTArray0100000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0100000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath, string deleteFlagFilePath, bool[] filteringFlags
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity, bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            QuestionType keyQuestionType = (QuestionType)0;
            string[] keyQsectorDescriptions = null;
            string keyQFilePath = null;
            DataWithMarking[][,] res = null;
            QCWebException exception = GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, qFilePath, keyQFilePath, deleteFlagFilePath, filteringFlags
                                                , weightbackFilePath, wt, descs, nQuestionDescription
                                                , out res, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
            resultArray = null;
            if (exception != null) return exception;
            if (res != null && res.Length == 1) resultArray = res[0];
            return null;
        }

        /// <alias>getGTArray1200000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1200000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 分類アイテム情報関連の引数にnullを指定してgetGTArray0200000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0200000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath, bool[] filteringFlags
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity, bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            QuestionType keyQuestionType = (QuestionType)0;
            string[] keyQsectorDescriptions = null;
            string keyQFilePath = null;
            DataWithMarking[][,] res = null;
            QCWebException exception = GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, qFilePath, keyQFilePath, filteringFlags
                                                , weightbackFilePath, wt, descs, nQuestionDescription
                                                , out res, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
            resultArray = null;
            if (exception != null) return exception;
            if (res != null && res.Length == 1) resultArray = res[0];
            return null;
        }

        /// <alias>getGTArray0010000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0010000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数FilteringFlagにnullを指定してgetGTArray0000000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ 無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Boolean[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0000000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , List<Data> data, List<Data> keyData
                , List<Data> weightback, string[] wt
                , TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            bool[] filteringFlags = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData, filteringFlags, weightback, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0110000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0110000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数FilteringFlagにnullを指定してgetGTArray0100000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0100000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath, string deleteFlagFilePath
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            bool[] filteringFlags = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, deleteFlagFilePath, filteringFlags, weightbackFilePath, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0210000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0210000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数FilteringFlagにnullを指定してgetGTArray0200000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0200000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            bool[] filteringFlags = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, filteringFlags, weightbackFilePath, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1010000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1010000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数FilteringFlagにnullを指定してgetGTArray1000000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Boolean[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1000000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , List<Data> data
                , List<Data> weightback, string[] wt
                , TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            bool[] filteringFlags = null;
            return GetGTArray(questionType, sectorDescriptions, data, filteringFlags, weightback, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1110000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1110000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数FilteringFlagにnullを指定してgetGTArray1100000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1100000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath, string deleteFlagFilePath
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity, bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            bool[] filteringFlags = null;
            return GetGTArray(questionType, sectorDescriptions, qFilePath, deleteFlagFilePath, filteringFlags
                            , weightbackFilePath, wt, descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1210000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1210000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数FilteringFlagにnullを指定してgetGTArray1200000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1200000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity, bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            bool[] filteringFlags = null;
            return GetGTArray(questionType, sectorDescriptions, qFilePath, filteringFlags
                            , weightbackFilePath, wt, descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0001000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0001000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackにnullを指定してgetGTArray0000000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Boolean[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0000000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , List<Data> data, List<Data> keyData, bool[] filteringFlags
                , string[] wt
                , TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            List<Data> weightback = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData, filteringFlags, weightback, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0101000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0101000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackFilePathにnullを指定してgetGTArray0100000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0100000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath, string deleteFlagFilePath, bool[] filteringFlags
                , string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            string weightbackFilePath = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, deleteFlagFilePath, filteringFlags, weightbackFilePath, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0201000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0201000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackFilePathにnullを指定してgetGTArray0200000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0200000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath, bool[] filteringFlags
                , string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            string weightbackFilePath = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, filteringFlags, weightbackFilePath, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1001000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1001000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackにnullを指定してgetGTArray1000000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Boolean[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1000000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , List<Data> data, bool[] filteringFlags
                , string[] wt
                , TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            List<Data> weightback = null;
            return GetGTArray(questionType, sectorDescriptions, data, filteringFlags, weightback, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1101000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1101000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackFilePathにnullを指定してgetGTArray1100000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1100000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath, string deleteFlagFilePath, bool[] filteringFlags
                , string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            string weightbackFilePath = null;
            return GetGTArray(questionType, sectorDescriptions, qFilePath, deleteFlagFilePath, filteringFlags
                            , weightbackFilePath, wt, descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1201000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1201000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackFilePathにnullを指定してgetGTArray1200000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1200000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath, bool[] filteringFlags
                , string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            string weightbackFilePath = null;
            return GetGTArray(questionType, sectorDescriptions, qFilePath, filteringFlags
                            , weightbackFilePath, wt, descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0011000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0011000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackにnullを指定してgetGTArray0010000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ 無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0010000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , List<Data> data, List<Data> keyData
                , string[] wt
                , TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            List<Data> weightback = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData, weightback, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0111000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0111000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackFilePathにnullを指定してgetGTArray0110000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String,System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0110000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, string qFilePath
                , QuestionType keyQuestionType, string keyQFilePath
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string deleteFlagFilePath
                , string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            string weightbackFilePath = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, deleteFlagFilePath, weightbackFilePath, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0211000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0211000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackFilePathにnullを指定してgetGTArray0210000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0210000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath
                , string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            string weightbackFilePath = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, weightbackFilePath, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1011000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1011000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackにnullを指定してgetGTArray1010000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1010000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , List<Data> data
                , string[] wt
                , TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            List<Data> weightback = null;
            return GetGTArray(questionType, sectorDescriptions, data, weightback, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1111000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1111000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackFilePathにnullを指定してgetGTArray1110000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.String,System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1110000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, string qFilePath
                , string[] sectorDescriptions
                , string deleteFlagFilePath
                , string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            string weightbackFilePath = null;
            return GetGTArray(questionType, sectorDescriptions, qFilePath, deleteFlagFilePath
                            , weightbackFilePath, wt, descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1211000</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1211000</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 引数weightbackFilePathにnullを指定してgetGTArray1210000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    有
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="nQuestionDescription">数値回答質問の質問文</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1210000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath
                , string[] wt, TabulationDescriptions descs, string nQuestionDescription
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            string weightbackFilePath = null;
            return GetGTArray(questionType, sectorDescriptions, qFilePath
                            , weightbackFilePath, wt
                            , descs, nQuestionDescription
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0000001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0000001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0000000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <param name="significanceTestCode">項目間検定の種類を表すSignificanceTestCode列挙型のコード値 (省略可、既定値Off)</param>
        /// <param name="significanceTestLevel">
        /// 項目間検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は最大2つ
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="SignificanceTestLogFilePath">
        /// 検定のログファイルの出力先パス (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="qName">
        /// 集計対象アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <param name="keyQName">
        /// 分類アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Boolean[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0000000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , List<Data> data, List<Data> keyData, bool[] filteringFlags
                , List<Data> weightback, string[] wt
                , TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl
                , SignificanceTestCode significanceTestCode = SignificanceTestCode.Off
                , double[] significanceTestLevel = null, string SignificanceTestLogFilePath = null
                , string qName = null, string keyQName = null, bool hasCount = false, int subTotalCount = 0, QuestionType qTypeOr = 0, bool isLower = true
                )
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData, filteringFlags, weightback, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl
                            , significanceTestCode, significanceTestLevel, SignificanceTestLogFilePath, qName, keyQName, hasCount, subTotalCount, qTypeOr: qTypeOr, isLower: isLower);
        }

        /// <alias>getGTArray0100001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0100001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0100000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0100000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath, string deleteFlagFilePath, bool[] filteringFlags
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, deleteFlagFilePath, filteringFlags, weightbackFilePath, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0200001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0200001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0200000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0200000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath, bool[] filteringFlags
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, filteringFlags, weightbackFilePath, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1000001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1000001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1000000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Boolean[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1000000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , List<Data> data, bool[] filteringFlags
                , List<Data> weightback, string[] wt
                , TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescriptions, data, filteringFlags, weightback, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1100001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1100001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1100000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1100000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath, string deleteFlagFilePath, bool[] filteringFlags
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescriptions, qFilePath, deleteFlagFilePath, filteringFlags
                            , weightbackFilePath, wt, descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1200001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1200001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1200000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  有
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.Boolean[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1200000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath, bool[] filteringFlags
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescriptions, qFilePath, filteringFlags
                            , weightbackFilePath, wt, descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0010001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0010001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0010000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ 無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0010000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , List<Data> data, List<Data> keyData
                , List<Data> weightback, string[] wt
                , TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData, weightback, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0110001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0110001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0110000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String,System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0110000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath, string deleteFlagFilePath
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, deleteFlagFilePath, weightbackFilePath, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0210001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0210001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0210000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0210000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, weightbackFilePath, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1010001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1010001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1010000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1010000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , List<Data> data
                , List<Data> weightback, string[] wt
                , TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescriptions, data, weightback, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1110001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1110001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1110000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.String,System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1110000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath, string deleteFlagFilePath
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescriptions, qFilePath, deleteFlagFilePath
                            , weightbackFilePath, wt, descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1210001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1210001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1210000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  無
        /// WB 有
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="weightbackFilePath">WB値データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1210000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath
                , string weightbackFilePath, string[] wt, TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescriptions, qFilePath
                            , weightbackFilePath, wt, descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0001001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0001001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0001000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Boolean[],System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0001000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , List<Data> data, List<Data> keyData, bool[] filteringFlags
                , string[] wt
                , TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData, filteringFlags, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0101001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0101001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0101000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String,System.Boolean[],System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0101000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath, string deleteFlagFilePath, bool[] filteringFlags
                , string[] wt, TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, deleteFlagFilePath, filteringFlags, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0201001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0201001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0201000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.Boolean[],System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0201000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath, bool[] filteringFlags
                , string[] wt, TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, filteringFlags, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1001001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1001001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1001000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Boolean[],System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1001000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , List<Data> data, bool[] filteringFlags
                , string[] wt
                , TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescriptions, data, filteringFlags, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1101001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1101001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1101000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.String,System.Boolean[],System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1101000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath, string deleteFlagFilePath, bool[] filteringFlags
                , string[] wt, TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescriptions, qFilePath, deleteFlagFilePath, filteringFlags
                            , wt, descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1201001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1201001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1201000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  有
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="filteringFlags">絞り込みフラグ</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.Boolean[],System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1201000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescriptions
                , string qFilePath, bool[] filteringFlags
                , string[] wt, TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescriptions, qFilePath, filteringFlags
                            , wt, descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0011001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0011001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0011000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ 無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0011000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , List<Data> data, List<Data> keyData
                , string[] wt
                , TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0111001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0111001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0111000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescription">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String,Macromill.QCWeb.Tabulation.QuestionType,System.String,System.String[],System.String[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0111000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, string qFilePath
                , QuestionType keyQuestionType, string keyQFilePath
                , string[] sectorDescription, string[] keyQsectorDescription
                , string deleteFlagFilePath
                , string[] wt, TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, qFilePath, keyQuestionType, keyQFilePath
                            , sectorDescription, keyQsectorDescription
                            , deleteFlagFilePath, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray0211001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray0211001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray0211000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  有
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="keyQFilePath">分類アイテム質問データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String[],System.String,System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[][0:,0:]@)">getGTArray0211000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescriptions
                , string qFilePath, string keyQFilePath
                , string[] wt, TabulationDescriptions descs
                , out DataWithMarking[][,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions
                            , qFilePath, keyQFilePath, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1011001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1011001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1011000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ Listクラスのインスタンスへの参照
        /// 絞り込みフラグデータ  無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1011000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescription
                , List<Data> data
                , string[] wt
                , TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescription, data, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1111001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1111001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1111000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス
        /// 絞り込みフラグデータ  無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="deleteFlagFilePath">削除フラグ列データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String,System.String[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1111000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType, string qFilePath
                , string[] sectorDescription
                , string deleteFlagFilePath
                , string[] wt, TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, qFilePath, sectorDescription, deleteFlagFilePath
                            , wt, descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }

        /// <alias>getGTArray1211001</alias>
        /// <summary>
        /// <para>エイリアス:getGTArray1211001</para>
        /// GT表イメージ二次元配列を生成する<br />
        /// 数値回答表記情報関連の引数にnullを指定してgetGTArray1211000に仲介する
        /// </summary>
        /// <breakdown>
        /// 分類アイテム  無
        /// 受け取るデータ ファイルパス (削除データファイルパスなし)
        /// 絞り込みフラグデータ  無
        /// WB 無
        /// ウエイト    有
        /// 表記情報    有
        /// 数値回答表記情報    無
        /// </breakdown>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="qFilePath">質問データのテキストファイルのパス</param>
        /// <param name="wt">ウエイト値情報を保持した配列</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue</param>
        /// <param name="locale">多言語情報取得用情報</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.GTTabulation.getGTArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.String,System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String[0:,0:]@)">getGTArray1211000</seealso>
        private static QCWebException GetGTArray(QuestionType questionType
                , string[] sectorDescription
                , string qFilePath
                , string[] wt, TabulationDescriptions descs
                , out DataWithMarking[,] resultArray, bool TabulateFullQuantity
                , bool IVtoNA, string locale, bool CutNA, Translation transl)
        {
            return GetGTArray(questionType, sectorDescription, qFilePath, wt
                            , descs, null
                            , out resultArray, TabulateFullQuantity, IVtoNA, locale, CutNA, transl);
        }
        #endregion

        #region SA/MA用
        /// <summary>
        /// 分類アイテムありの時のSAまたはMAのGT集計<br />getGTArray0000001に仲介する
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="descs">表示文字列を保持するクラス (省略可)</param>
        /// <param name="filteringFlags">絞り込みフラグ (省略可)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (省略可)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (省略可)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue (省略可、既定値true)</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue (省略可、既定値false)</param>
        /// <param name="locale">多言語情報取得用情報 (省略可、規定値ja)</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue (省略可、既定値false)</param>
        /// <param name="significanceTestCode">項目間検定の種類を表すSignificanceTestCode列挙型のコード値 (省略可、既定値Off)</param>
        /// <param name="significanceTestLevel">
        /// 項目間検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は最大2つ
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="SignificanceTestLogFilePath">
        /// 検定のログファイルの出力先パス (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="qName">
        /// 集計対象アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <param name="keyQName">
        /// 分類アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetGTArraySAMA(QuestionType questionType, string[] sectorDescriptions, List<Data> data
                , QuestionType keyQuestionType, string[] keyQsectorDescriptions, List<Data> keyData
                , out DataWithMarking[][,] resultArray, Translation transl
                , TabulationDescriptions descs = null
                , bool[] filteringFlags = null, List<Data> weightback = null, string[] wt = null
                , bool TabulateFullQuantity = true, bool IVtoNA = false, string locale = "ja"
                , bool CutNA = false
                , SignificanceTestCode significanceTestCode = SignificanceTestCode.Off
                , double[] significanceTestLevel = null, string SignificanceTestLogFilePath = null
                , string qName = null, string keyQName = null, bool hasCount = false, int subTotalCount = 0, QuestionType qTypeOr = 0, bool isLower = true
                )
        {
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData
                            , filteringFlags, weightback, wt, descs, out resultArray, TabulateFullQuantity
                            , IVtoNA, locale, CutNA, transl, significanceTestCode, significanceTestLevel, SignificanceTestLogFilePath
                            , qName, keyQName, hasCount, subTotalCount, qTypeOr: qTypeOr, isLower: isLower);
        }

        /// <summary>
        /// 分類アイテムなしの時のSAまたはMAのGT集計
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="descs">表示文字列を保持するクラス (省略可)</param>
        /// <param name="filteringFlags">絞り込みフラグ (省略可)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (省略可)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (省略可)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue(省略可、既定値true)</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue (省略可、既定値false)</param>
        /// <param name="locale">多言語情報取得用情報(省略可、規定値ja)</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue (省略可、既定値false)</param>
        /// <param name="significanceTestCode">項目間検定の種類を表すSignificanceTestCode列挙型のコード値 (省略可、既定値Off)</param>
        /// <param name="significanceTestLevel">
        /// 項目間検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は最大2つ
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="SignificanceTestLogFilePath">
        /// 検定のログファイルの出力先パス (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="qName">
        /// 集計対象アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetGTArraySAMA(QuestionType questionType, string[] sectorDescriptions, List<Data> data
                , out DataWithMarking[,] resultArray, Translation transl
                , TabulationDescriptions descs
                , bool[] filteringFlags = null, List<Data> weightback = null, string[] wt = null
                , bool TabulateFullQuantity = true, bool IVtoNA = false, string locale = "ja"
                , bool CutNA = false
                , SignificanceTestCode significanceTestCode = SignificanceTestCode.Off
                , double[] significanceTestLevel = null, string SignificanceTestLogFilePath = null, string qName = null, bool hasCount = false, int subTotalCount = 0, QuestionType qTypeOr = 0, bool isLower = true
                )
        {
            QuestionType keyQuestionType = (QuestionType)0;
            string[] keyQsectorDescription = null;
            List<Data> keyData = null;
            DataWithMarking[][,] res = null;
            QCWebException exception = GetGTArraySAMA(questionType, sectorDescriptions, data, keyQuestionType, keyQsectorDescription, keyData
                                                    , out res, transl, descs, filteringFlags, weightback, wt, TabulateFullQuantity, IVtoNA, locale, CutNA
                                                    , significanceTestCode, significanceTestLevel, SignificanceTestLogFilePath, qName, null, hasCount, subTotalCount, qTypeOr: qTypeOr, isLower: isLower);
            resultArray = null;
            if (exception != null) return exception;
            if (res != null && res.Length == 1) resultArray = res[0];
            return null;
        }
        #endregion

        #region N用
        /// <summary>
        /// 分類アイテムありの時のNのGT集計<br />getGTArray0000000に仲介する
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="questionDescription">集計するN質問の質問文</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQsectorDescriptions">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="descs">表示文字列を保持するクラス (省略可)</param>
        /// <param name="filteringFlags">絞り込みフラグ (省略可)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (省略可)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (省略可)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue(省略可、既定値true)</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue (省略可、既定値false)</param>
        /// <param name="locale">多言語情報取得用情報(省略可、規定値ja)</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue (省略可、既定値false)</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetGTArrayN(QuestionType questionType, string questionDescription, List<Data> data
                , QuestionType keyQuestionType, string[] keyQsectorDescriptions, List<Data> keyData
                , out DataWithMarking[][,] resultArray, Translation transl
                , TabulationDescriptions descs = null
                , bool[] filteringFlags = null, List<Data> weightback = null, string[] wt = null
                , bool TabulateFullQuantity = true, bool IVtoNA = false, string locale = "ja"
                , bool CutNA = false, bool isLower = true)
        {
            string[] sectorDescriptions = null;
            return GetGTArray(questionType, keyQuestionType, sectorDescriptions, keyQsectorDescriptions, data, keyData
                            , filteringFlags, weightback, wt, descs, questionDescription, out resultArray, TabulateFullQuantity
                            , IVtoNA, locale, CutNA, transl, isLower: isLower);
        }

        /// <summary>
        /// 分類アイテムなしの時のNのGT集計<br />getGTArray0000000に仲介する
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="questionDescription">集計するN質問の質問文</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="descs">表示文字列を保持するクラス (省略可)</param>
        /// <param name="filteringFlags">絞り込みフラグ (省略可)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (省略可)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (省略可)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue(省略可、既定値true)</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue (省略可、既定値false)</param>
        /// <param name="locale">多言語情報取得用情報(省略可、規定値ja)</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue (省略可、既定値false)</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetGTArrayN(QuestionType questionType, string questionDescription, List<Data> data
                , out DataWithMarking[,] resultArray, Translation transl
                , TabulationDescriptions descs = null
                , bool[] filteringFlags = null, List<Data> weightback = null, string[] wt = null
                , bool TabulateFullQuantity = true, bool IVtoNA = false, string locale = "ja"
                , bool CutNA = false, bool isLower = true)
        {
            QuestionType keyQuestionType = (QuestionType)0;
            string[] keyQsectorDescription = null;
            List<Data> keyData = null;
            DataWithMarking[][,] res = null;
            QCWebException exception = GetGTArrayN(questionType, questionDescription, data, keyQuestionType, keyQsectorDescription, keyData
                                                 , out res, transl, descs, filteringFlags, weightback, wt, TabulateFullQuantity, IVtoNA, locale, CutNA, isLower: isLower);
            resultArray = null;
            if (exception != null) return exception;
            if (res != null && res.Length == 1) resultArray = res[0];
            return null;
        }
        #endregion
        #endregion

        #region マトリクス用
        /// <summary>
        /// 項目間検定
        /// </summary>
        /// <param name="dataArray"></param>
        /// <param name="significanceTestLevel">
        /// 項目間検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は最大2つ
        /// </param>
        /// <param name="significanceTestCode">項目間検定の種類を表すSignificanceTestCode列挙型のコード値</param>
        /// <param name="writer">検定ログ出力に使用するストリームライターへの参照 (省略可、既定値null)</param>
        /// <param name="keyQName">
        /// 分類アイテム名 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="qType">
        /// 集計対象質問のSA/MA/Nのいずれかを表すQuestionType列挙型の値 (省略可、既定値SA)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="subHeaderBuffer">
        /// サブヘッダの共通文字列 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        private static void SignificanceTest(ref DataWithMarking[][][] dataArray
                      , double[] significanceTestLevel, SignificanceTestCode significanceTestCode
                      , StreamWriter writer = null, string keyQName = null, QuestionType qType = QuestionType.SA, string subHeaderBuffer = null, int subTotalCount = 0)
        {
            System.Text.StringBuilder headerBuilder = null;
            System.Text.StringBuilder ptBuilder = null;
            System.Text.StringBuilder mtBuilder = null;
            StringWriter headerWriter = null;
            StringWriter ptWriter = null;
            StringWriter mtWriter = null;
            try
            {
                if (writer != null)
                {
                    headerBuilder = new System.Text.StringBuilder();
                    headerWriter = new StringWriter(headerBuilder);
                    if (significanceTestCode == SignificanceTestCode.BetweenSectors || qType != QuestionType.N)
                    {
                        ptBuilder = new System.Text.StringBuilder();
                        ptWriter = new StringWriter(ptBuilder);
                    }
                    if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                    {
                        mtBuilder = new System.Text.StringBuilder();
                        mtWriter = new StringWriter(mtBuilder);
                    }
                }
                for (int k = 0; k < dataArray.Length; ++k)
                {
                    if (writer != null)
                    {
                        headerBuilder.Clear();
                        headerWriter.Write("KeyItem:");
                        if (string.IsNullOrWhiteSpace(keyQName))
                        {
                            headerWriter.WriteLine();
                        }
                        else
                        {
                            headerWriter.WriteLine("{0} = {1}", keyQName, k + 1);
                        }
                        headerWriter.Write(subHeaderBuffer);
                        if (ptWriter != null)
                        {
                            ptBuilder.Clear();
                            if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                            {
                                GlobalTabulation.LogWritePTHeaderLine(ptWriter, "item", "-", "cate1", "cate2");
                            }
                            else
                            {
                                GlobalTabulation.LogWritePTHeaderLine(ptWriter, "cate", "-", "item1", "item2");
                            }
                        }
                        if (mtWriter != null)
                        {
                            mtBuilder.Clear();
                            GlobalTabulation.LogWriteMTHeaderLine(mtWriter, qType == QuestionType.N ? "-" : "cate", "-", "item1", "item2");
                        }
                        writer.Write(headerBuilder);
                    }
                    for (int y = 0; y < dataArray[k].Length; ++y)
                    {
                        double N0 = 0.0;
                        double N1 = 0.0;
                        double N2 = 0.0;
                        double q0 = 0.0;
                        double q1 = 0.0;
                        double q2 = 0.0;
                        int totalColumnIndex = 1;
                        DataWithMarking totalD1 = dataArray[k][0][totalColumnIndex];
                        for (int x = 0; x < dataArray[k][y].Length; ++x)
                        {
                            DataWithMarking d1 = dataArray[k][y][x];
                            if (!d1.SettedSectorInformation)
                            {
                                if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                                {
                                    if ((d1.CellType & CellType.TotalCell) == CellType.TotalCell)
                                    {
                                        N1 = d1.Count;
                                        q1 = d1.WBSquareSummary;
                                        N2 = N1;
                                        N0 = N1;
                                        q2 = q1;
                                        q0 = q1;
                                    }
                                }
                                continue;
                            }
                            int sNo = d1.SectorNumber;
                            int sCnt = d1.SectorsCount - subTotalCount; //#OutputFormatting
                            if (sNo >= sCnt) continue;
                            if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                            {
                                if ((d1.CellType & CellType.TotalCell) == CellType.TotalCell)
                                {
                                    totalColumnIndex = x;
                                    totalD1 = d1;
                                }
                            }
                            if ((int)(d1.CellType & (CellType.DataCell | CellType.SimpleDataCell)) == 0) continue;
                            for (int s = sNo + 1; s <= sCnt; ++s)
                            {
                                DataWithMarking d2;
                                if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                                {
                                    d2 = dataArray[k][y][x + s - sNo];
                                }
                                else
                                {
                                    d2 = dataArray[k][y + s - sNo][x];
                                    DataWithMarking totalD2 = dataArray[k][y + s - sNo][totalColumnIndex];
                                    N1 = totalD1.Count;
                                    N2 = totalD2.Count;
                                    q1 = totalD1.WBSquareSummary;
                                    q2 = totalD2.WBSquareSummary;
                                    if (totalD1.HasOverlap)
                                    {
                                        DetailData totalOd = totalD1.OverlapData(s);
                                        N0 = totalOd.count;
                                        q0 = totalOd.wbSquareSummary;
                                    }
                                }
                                DetailData od = default(DetailData);
                                if (d1.HasOverlap) od = d1.OverlapData(s);
                                /*
                                if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                                {
                                    if ((d1.CellType & CellType.TotalCell) == CellType.TotalCell)
                                    {
                                        N1 = d1.Count;
                                        N2 = d2.Count;
                                        q1 = d1.WBSquareSummary;
                                        q2 = d2.WBSquareSummary;
                                        if (d1.HasOverlap)
                                        {
                                            N0 = od.count;
                                            q0 = od.wbSquareSummary;
                                        }
                                    }
                                }
                                */
                                // if ((d1.CellType & CellType.DataCell) != CellType.DataCell) continue;
                                // if ((int)(d1.CellType & (CellType.DataCell | CellType.SimpleDataCell)) == 0) continue;
                                double X1 = d1.NumValue;
                                double X2 = d2.NumValue;
                                double X0inOverlap = 0.0;
                                double X1inOverlap = significanceTestCode == SignificanceTestCode.BetweenSectors ? X1 : 0.0;
                                double X2inOverlap = significanceTestCode == SignificanceTestCode.BetweenSectors ? X2 : 0.0;
                                if (d1.HasOverlap)
                                {
                                    X0inOverlap = od.multipliedSummary;
                                    if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                                    {
                                        X1inOverlap = od.summary;
                                        X2inOverlap = od.overlaptargetValueSummary;
                                    }
                                }
                                double p = 0.0;
                                double e0, e1, e2, Z, t, d;
                                if ((d1.CellType & CellType.PopulationCell) == CellType.PopulationCell)
                                {
                                    double Y1 = d1.SquareSummary;
                                    double Y2 = d2.SquareSummary;
                                    double Y1inOverlap = significanceTestCode == SignificanceTestCode.BetweenSectors ? Y1 : 0.0;
                                    double Y2inOverlap = significanceTestCode == SignificanceTestCode.BetweenSectors ? Y2 : 0.0;
                                    if (d1.HasOverlap && significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                                    {
                                        Y1inOverlap = od.squareSummary;
                                        Y2inOverlap = od.overlaptargetSquareValueSummary;
                                    }
                                    double U1, U2, Ue, meanX1, meanX2;
                                    // ログ出力
                                    GlobalTabulation.LogWriteLineHeader(mtWriter, qType == QuestionType.N ? "-" : "WTAVG", "-", sNo, s);
                                    Function.RoundOffTDistribution(ref N0, ref N1, ref N2, ref X1, ref X2, ref Y1, ref Y2, ref q0, ref q1, ref q2, ref X0inOverlap, ref X1inOverlap, ref X2inOverlap, ref Y1inOverlap, ref Y2inOverlap);
                                    p = Function.TDistribution(N0, N1, N2, X1, X2, Y1, Y2, q0, q1, q2
                                                                , X0inOverlap, X1inOverlap, X2inOverlap, Y1inOverlap, Y2inOverlap
                                                                , out U1, out U2, out Ue, out meanX1, out meanX2
                                                                , out e0, out e1, out e2, out Z, out t, out d);
                                    // ログ出力
                                    GlobalTabulation.LogWriteLineData(mtWriter
                                            , N0, N1, N2, X1, X2, q0, q1, q2, X0inOverlap, X1inOverlap, X2inOverlap
                                            , e0, e1, e2, Z, t, d, p, U1, U2, Ue, null, Y1, Y2, Y1inOverlap, Y2inOverlap
                                            , meanX1, meanX2);
                                }
                                else
                                {
                                    double p1, p2, p12, c;
                                    // ログ出力
                                    GlobalTabulation.LogWriteLineHeader(ptWriter
                                            , significanceTestCode == SignificanceTestCode.BetweenSectors ? y + 1 : x - 1
                                            , "-", sNo, s);
                                    Function.RoundOffTDistribution(ref N0, ref N1, ref N2, ref X1, ref X2, ref q0, ref q1, ref q2, ref X0inOverlap, ref X1inOverlap, ref X2inOverlap);
                                    p = Function.TDistribution(N0, N1, N2, X1, X2, q0, q1, q2, X0inOverlap, X1inOverlap, X2inOverlap
                                                            , out p1, out p2, out p12, out c, out e0, out e1, out e2
                                                            , out Z, out t, out d);
                                    // ログ出力
                                    GlobalTabulation.LogWriteLineData(ptWriter, N0, N1, N2, X1, X2, q0, q1, q2
                                            , X0inOverlap, X1inOverlap, X2inOverlap, e0, e1, e2, Z, t, d, p
                                            , p1, p2, p12, c);
                                }
                                if (p < 0.0) continue;
                                if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions
                                   && (d1.CellType & CellType.PopulationCell) == CellType.PopulationCell
                                   && X2 <= 0.0
                                   && p <= 0.0)
                                {
                                    continue;
                                }
                                //if (Function.Compare(p, Function.CompareOperator.LessEqual, 0.0)) continue;
                                p *= 100.0;
                                for (int i = 0; i < significanceTestLevel.Length; ++i)
                                {
                                    //if (p <= significanceTestLevel[i])
                                    if (Function.Compare(p, Function.CompareOperator.LessEqual, significanceTestLevel[i]))
                                    {
                                        if (t >= 0)
                                        //if (Function.Compare(t, Function.CompareOperator.GreaterEqual, 0.0))
                                        {
                                            d1.AppendSignificanceSectorNumber(s, i);
                                        }
                                        else
                                        {
                                            d2.AppendSignificanceSectorNumber(sNo, i);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        if (writer != null)
                        {
                            if (ptWriter != null)
                            {
                                writer.Write(ptBuilder);
                                ptBuilder.Remove(0, ptBuilder.Length);
                            }
                            if (mtWriter != null)
                            {
                                writer.Write(mtBuilder);
                                mtBuilder.Remove(0, mtBuilder.Length);
                            }
                        }
                    }
                    //if (writer != null)
                    //{
                    //    writer.Write(headerBuilder);
                    //    if (ptWriter != null) writer.Write(ptBuilder);
                    //    if (mtWriter != null) writer.Write(mtBuilder);
                    //}
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (headerWriter != null) headerWriter.Dispose();
                if (ptWriter != null) ptWriter.Dispose();
                if (mtWriter != null) mtWriter.Dispose();
            }
        }

        /// <alias>GetGTArrayMatrix01</alias>
        /// <summary>
        /// <para>エイリアス:GetGTArrayMatrix01</para>
        /// マトリクス質問のGT表イメージ二次元配列を生成する
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="keyQsectorDescription">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="childQuestionType">子質問の質問タイプを表すQuestionType列挙型の値を要素とする配列</param>
        /// <param name="childQuestionDescription">子質問文を要素とする配列</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="FilteringFlag">絞り込みフラグ (省略可能)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (省略可能)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (省略可能)</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue(省略可、既定値true)</param>
        /// <param name="locale">多言語情報取得用情報(省略可、規定値ja)</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue (省略可、既定値false)</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue (省略可、既定値false)</param>
        /// <param name="significanceTestCode">項目間検定の種類を表すSignificanceTestCode列挙型のコード値 (省略可、既定値Off)</param>
        /// <param name="significanceTestLevel">
        /// 項目間検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は最大2つ
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="SignificanceTestLogFilePath">
        /// 検定のログファイルの出力先パス (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="qName">
        /// 集計対象アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <param name="childQuestionName">
        /// 子アイテム名からなる配列
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <param name="keyQName">
        /// 分類アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetGTArrayMatrix(QuestionType questionType, QuestionType keyQuestionType
                , string[] sectorDescriptions, string[] keyQsectorDescription
                , List<List<Data>> data, List<Data> keyData
                , QuestionType[] childQuestionType, string[] childQuestionDescription
                , out DataWithMarking[][,] resultArray, Translation translation
                , TabulationDescriptions descs
                , bool[] FilteringFlag = null
                , List<Data> weightback = null, string[] wt = null
                , bool TabulateFullQuantity = true
                , string locale = "ja", bool IVtoNA = false, bool CutNA = false
                , SignificanceTestCode significanceTestCode = SignificanceTestCode.Off
                , double[] significanceTestLevel = null, string SignificanceTestLogFilePath = null
                , string qName = null, string[] childQuestionName = null, string keyQName = null, bool hasCount = false, int subTotalCount = 0, bool isMTS = false, QuestionType qTypeOr = 0, bool isLower = true
                )
        {
            // 戻り値の初期化
            resultArray = null;
            // 引数のチェック
            int childQuestionsCount = 0;
            int dataCount = 0;
            int sectorsCount = 0;
            int keyQsectorsCount = 0;
            QCWebException exception = null;
            if (!checkGetGTArrayArguments(questionType, keyQuestionType, data, keyData
                        , ref childQuestionType, childQuestionDescription
                        , ref childQuestionsCount, ref dataCount, ref sectorDescriptions, ref sectorsCount
                        , ref keyQsectorDescription, ref keyQsectorsCount, ref FilteringFlag
                        , ref weightback, ref wt, ref descs, out exception))
            {
                return exception;
            }
            // ウエイト値保持ハッシュテーブル
            Hashtable wtList = new Hashtable();

            // 質問タイプのシンプル化
            // 集計対象質問がマトリクスであることは、この時点で担保されているので、カット
            QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA | QuestionType.N);
            qTypeOr = qTypeOr & (QuestionType.SA | QuestionType.MA | QuestionType.N);
            QuestionType keyQType = keyQuestionType & (QuestionType.SA | QuestionType.MA);
            // 結果の配列のサイズ決定と見出し部分の投入
            int k = keyQsectorsCount == 0 ? 1 : keyQsectorsCount;
            resultArray = new DataWithMarking[k][,];
            switch (qType)
            {
                case QuestionType.SA:   // SA
                case QuestionType.MA:   // MA
                case QuestionType.SA | QuestionType.MA: // 混在
                    for (int i = 0; i < k; ++i)
                    {
                        // resultArray[i] = new DataWithMarking[3 + childQuestionsCount, 3 + 2 + sectorsCount + 3];
                        resultArray[i] = new DataWithMarking[3 + childQuestionsCount, 3 + 2 + sectorsCount + 4];
                        // TODO:Const化[QCR0000019]単一回答
                        // TODO:Const化[QCR0000020]複数回答
                        // TODO:Const化[QCR0000021]マトリクス
                        resultArray[i][0, 1] = new DataWithMarking(
                            //(qType == QuestionType.SA ?
                            //    GetResource.GetCommonResourceData("QCR0000019", locale) :
                            //    GetResource.GetCommonResourceData("QCR0000020", locale)
                            //) + GetResource.GetCommonResourceData("QCR0000021", locale)
                            qType == QuestionType.SA
                                                                ? translation.REPORT_SA_DESCRIPTION_KEYWORD
                                                                : qType == QuestionType.MA
                                                                        ? translation.REPORT_MA_DESCRIPTION_KEYWORD
                                                                        : translation.REPORT_COMPLEX_DESCRIPTION_KEYWORD
                                + translation.REPORT_MATRIX_DESCRIPTION_KEYWORD
                          , false
                        );
                        resultArray[i][1, 3] = new DataWithMarking(descs.PreWBtotalDescription, false);  // WB前全体
                        resultArray[i][1, 4] = new DataWithMarking(descs.TotalDescription, false);   // 全体
                        for (int j = 1; j <= sectorsCount; ++j)
                        {
                            resultArray[i][0, 4 + j] = new DataWithMarking(j.ToString());  // 選択肢インデックス
                            resultArray[i][1, 4 + j] = new DataWithMarking(sectorDescriptions[j - 1], false);   // 選択肢文
                            resultArray[i][1, 4 + j].SetSignificanceCharacters(Strings.LCase(SignificanceTestLetters.Character(j)));
                        }
                        resultArray[i][1, 4 + sectorsCount + 1] = new DataWithMarking(descs.NADescription, false);   // 無回答
                        resultArray[i][1, 4 + sectorsCount + 2] = new DataWithMarking(descs.IVDescription, false);   // 非該当
                                                                                                                     // resultArray[i][1, 4 + sectorsCount + 3] = new DataWithMarking("加重平均");  // リソース読み込み
                        if (hasCount)
                        {
                            resultArray[i][1, 4 + sectorsCount + 3] = new DataWithMarking(translation.REPORT_COUNT_AVERAGE_DENOMINATOR_KEYWORD, false);
                            resultArray[i][1, 4 + sectorsCount + 4] = new DataWithMarking(translation.REPORT_COUNT_AVERAGE_KEYWORD, false);
                        }
                        else
                        {
                            //resultArray[i][1, 4 + sectorsCount + 3] = new DataWithMarking(GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportWeightAverageDenominatorKeywordIndex, locale), false);
                            //resultArray[i][1, 4 + sectorsCount + 4] = new DataWithMarking(GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportWeightAverageKeywordIndex, locale), false);
                            resultArray[i][1, 4 + sectorsCount + 3] = new DataWithMarking(translation.REPORT_WEIGHT_AVERAGE_DENOMINATOR_KEYWORD, false);
                            resultArray[i][1, 4 + sectorsCount + 4] = new DataWithMarking(translation.REPORT_WEIGHT_AVERAGE_KEYWORD, false);
                        }

                        for (int j = 1; j <= childQuestionsCount; ++j)
                        {
                            // resultArray[i][1 + j, 0] = new DataWithMarking(j.ToString());   // 子質問インデックス
                            // resultArray[i][1 + j, 1] = new DataWithMarking(childQuestionDescription[j - 1]);    // 子質問文
                            resultArray[i][2 + j, 0] = new DataWithMarking(j.ToString());   // 子質問インデックス
                            resultArray[i][2 + j, 1] = new DataWithMarking(childQuestionDescription[j - 1], false);    // 子質問文
                            resultArray[i][2 + j, 1].SetSignificanceCharacters(Strings.LCase(SignificanceTestLetters.Character(j)));
                        }

                    }

                    for (int i = 0; i < sectorsCount; ++i)
                    {
                        if (!string.IsNullOrWhiteSpace(wt[i]))
                        {
                            double w = 0.0;
                            if (double.TryParse(wt[i], out w))
                            {
                                // キーは選択肢インデックス(1ベース)
                                wtList.Add((i + 1).ToString(), w);
                            }
                        }
                    }
                    break;
                case QuestionType.N:    // N
                    for (int i = 0; i < k; ++i)
                    {
                        resultArray[i] = new DataWithMarking[2 + childQuestionsCount, 3 + 2 + 7 + 2];
                        // TODO:Const化[QCR0000024]数値回答マトリクス
                        // resultArray[i][0, 1] = new DataWithMarking(GetResource.GetCommonResourceData("QCR0000024", locale));
                        resultArray[i][0, 1] = new DataWithMarking(translation.REPORT_N_MATRIX_DESCRIPTION_KEYWORD, false);
                        resultArray[i][1, 2 + 1] = new DataWithMarking(descs.PreWBtotalDescription, false);  // WB前全体
                        resultArray[i][1, 2 + 2] = new DataWithMarking(descs.TotalDescription, false);   // 全体
                        resultArray[i][1, 2 + 2 + 1] = new DataWithMarking(descs.ParameterDescription, false);   // 統計量母数
                        resultArray[i][1, 2 + 2 + 2] = new DataWithMarking(descs.SummaryDescription, false); // 合計
                        resultArray[i][1, 2 + 2 + 3] = new DataWithMarking(descs.AverageDescription, false); // 平均
                        resultArray[i][1, 2 + 2 + 4] = new DataWithMarking(descs.StdevDescription, false);   // 標準偏差
                        resultArray[i][1, 2 + 2 + 5] = new DataWithMarking(descs.MinDescription, false); // 最小値
                        resultArray[i][1, 2 + 2 + 6] = new DataWithMarking(descs.MaxDescription, false); // 最大値
                        resultArray[i][1, 2 + 2 + 7] = new DataWithMarking(descs.MedianDescription, false);  // 中央値
                        resultArray[i][1, 2 + 2 + 7 + 1] = new DataWithMarking(descs.NADescription, false);  // 無回答
                        resultArray[i][1, 2 + 2 + 7 + 2] = new DataWithMarking(descs.IVDescription, false);  // 非該当
                        for (int j = 1; j <= childQuestionsCount; ++j)
                        {
                            resultArray[i][1 + j, 0] = new DataWithMarking(j.ToString());   // 子質問インデックス
                            resultArray[i][1 + j, 1] = new DataWithMarking(childQuestionDescription[j - 1], false);    // 子質問文
                            resultArray[i][1 + j, 1].SetSignificanceCharacters(Strings.LCase(SignificanceTestLetters.Character(j)));
                        }
                    }
                    break;
            }

            // 項目間検定設定の補正
            bool SignificanceTestOn = Enum.IsDefined(typeof(SignificanceTestCode), significanceTestCode);
            if (SignificanceTestOn)
            {
                if (SignificanceTestOn = significanceTestCode != SignificanceTestCode.Off)
                {
                    if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                    {
                        SignificanceTestOn = (qType != QuestionType.N);
                    }
                }
            }
            if (SignificanceTestOn)
            {
                if (significanceTestLevel == null || significanceTestLevel.Length == 0)
                {
                    SignificanceTestOn = false;
                }
                else
                {
                    if (significanceTestLevel.Length > 2) Array.Resize<double>(ref significanceTestLevel, 2);
                    if (significanceTestLevel.Length == 2 && significanceTestLevel[0] == significanceTestLevel[1])
                    {
                        Array.Resize<double>(ref significanceTestLevel, 1);
                    }
                    if (significanceTestLevel.Length == 1)
                    {
                        SignificanceTestOn = significanceTestLevel[0] > 0.0 && significanceTestLevel[0] <= 10.0;
                    }
                    else
                    {
                        Array.Sort<double>(significanceTestLevel);
                        bool[] OnFlag = new bool[significanceTestLevel.Length];
                        for (int i = 0; i < significanceTestLevel.Length; ++i)
                        {
                            OnFlag[i] = significanceTestLevel[i] > 0.0 && significanceTestLevel[i] <= 10.0;
                        }
                        if (OnFlag[0] ^ OnFlag[1])
                        {
                            if (OnFlag[1]) significanceTestLevel[0] = significanceTestLevel[1];
                            Array.Resize<double>(ref significanceTestLevel, 1);
                        }
                        else
                        {
                            SignificanceTestOn = OnFlag[0];
                        }
                    }
                }
            }
            if (SignificanceTestOn)
            {
                if (SignificanceTestLogFilePath != null)
                {
                    if (string.IsNullOrWhiteSpace(SignificanceTestLogFilePath) || string.IsNullOrWhiteSpace(qName)
                        || childQuestionName == null || childQuestionName.Length != childQuestionsCount
                        || SignificanceTestLogFilePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                    {
                        SignificanceTestLogFilePath = null;
                    }
                }
            }
            else
            {
                significanceTestCode = SignificanceTestCode.Off;
                SignificanceTestLogFilePath = null;
            }
            // 集計
            double[][][] nArray = null; // 集計値を格納する配列
            DataWithMarking[][][] dataArray = null; // 詳細な集計値を格納する配列
            if (SignificanceTestOn)
            {
                dataArray = new DataWithMarking[k][][];
            }
            else
            {
                nArray = new double[k][][];
            }
            // N質問集計時に、最大値, 最小値, 中央値を出すための配列
            // double[][][] normalDatas = new double[k][][];
            NumericData[][][] normalDatas = new NumericData[k][][];
            int[][] lastIndex = new int[k][];   // normalDatasの使用最大インデックス
            // double[][] effectiveSamplesCount = null; // 加重平均用統計量母数
            // 集計値を格納する配列のサイズ定義
            switch (qType)
            {
                case QuestionType.SA:   // SA
                case QuestionType.MA:   // MA
                case QuestionType.SA | QuestionType.MA: // 混在
                    // if (wtList.Count > 0) effectiveSamplesCount = new double[k][];
                    for (int i = 0; i < k; ++i)
                    {
                        if (SignificanceTestOn)
                        {
                            dataArray[i] = new DataWithMarking[childQuestionsCount][];
                            for (int j = 0; j < childQuestionsCount; ++j)
                            {
                                dataArray[i][j] = new DataWithMarking[2 + sectorsCount + 3];
                                dataArray[i][j][0] = new DataWithMarking(CellType.PreWBTotalCell);
                                if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                                {
                                    dataArray[i][j][1] = new DataWithMarking(CellType.TotalCell);
                                    for (int s = 0; s < sectorsCount; ++s)
                                    {
                                        dataArray[i][j][2 + s] = new DataWithMarking(s + 1, sectorsCount, CellType.SimpleDataCell, qType == QuestionType.MA);
                                    }
                                    // 加重平均母数列の詳細集計は不要なので、無回答列などと同等に扱う
                                    for (int c = 0; c < 3; ++c)
                                    {
                                        dataArray[i][j][2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                    }
                                }
                                else    // significanceTestCode == SignificanceTestCode.BetweenChildQuestions
                                {
                                    dataArray[i][j][1] = new DataWithMarking(j + 1, childQuestionsCount, CellType.TotalCell);
                                    for (int s = 0; s < sectorsCount; ++s)
                                    {
                                        dataArray[i][j][2 + s] = new DataWithMarking(j + 1, childQuestionsCount);
                                    }
                                    for (int c = 0; c < 2; ++c)
                                    {
                                        dataArray[i][j][2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                    }
                                    dataArray[i][j][2 + sectorsCount + 3 - 1] = new DataWithMarking(j + 1, childQuestionsCount, CellType.PopulationCell);
                                }
                            }
                        }
                        else
                        {
                            nArray[i] = new double[childQuestionsCount][];
                            // if (wtList.Count > 0) effectiveSamplesCount[i] = new double[childQuestionsCount];
                            for (int j = 0; j < childQuestionsCount; ++j)
                            {
                                // nArray[i][j] = new double[2 + sectorsCount + 2];
                                // if (wtList.Count > 0) effectiveSamplesCount[i][j] = 0.0;
                                nArray[i][j] = new double[2 + sectorsCount + 3];
                            }
                        }
                    }
                    break;
                case QuestionType.N:    // N
                    for (int i = 0; i < k; ++i)
                    {
                        // normalDatas[i] = new double[childQuestionsCount][];
                        normalDatas[i] = new NumericData[childQuestionsCount][];
                        lastIndex[i] = new int[childQuestionsCount];
                        for (int j = 0; j < childQuestionsCount; ++j)
                        {
                            // normalDatas[i][j] = new double[dataCount];
                            normalDatas[i][j] = new NumericData[dataCount];
                            lastIndex[i][j] = -1;
                        }
                        if (SignificanceTestOn)
                        {
                            dataArray[i] = new DataWithMarking[childQuestionsCount][];
                            for (int j = 0; j < childQuestionsCount; ++j)
                            {
                                dataArray[i][j] = new DataWithMarking[5];
                                dataArray[i][j][0] = new DataWithMarking(CellType.PreWBTotalCell);  // WB前全体
                                // 全体:数値回答では、ただ集計するだけなので、無回答などと同じ扱いにする
                                dataArray[i][j][1] = new DataWithMarking(j + 1, childQuestionsCount, CellType.NAIVCell);
                                dataArray[i][j][2] = new DataWithMarking(j + 1, childQuestionsCount, CellType.PopulationCell);   // 統計量母数
                                for (int c = 0; c < 2; ++c)
                                {
                                    dataArray[i][j][3 + c] = new DataWithMarking(CellType.NAIVCell);
                                }
                            }
                        }
                        else
                        {
                            nArray[i] = new double[childQuestionsCount][];
                            for (int j = 0; j < childQuestionsCount; ++j)
                            {
                                nArray[i][j] = new double[7];
                            }
                        }
                    }
                    break;
            }

            // オーバーラップ分集計用リスト
            List<DataWithMarking> tmpList = null;
            List<List<DataWithMarking>> tmpListList = null;
            List<double> tmpNumList = null;
            double[,] tmpNumArray = null;
            switch (significanceTestCode)
            {
                case SignificanceTestCode.BetweenSectors:
                    if ((qType & QuestionType.MA) == QuestionType.MA)
                    {
                        tmpList = new List<DataWithMarking>();
                    }
                    break;
                case SignificanceTestCode.BetweenChildQuestions:
                    if (qType != QuestionType.N)
                    {
                        tmpListList = new List<List<DataWithMarking>>();
                        tmpNumArray = new double[childQuestionsCount, sectorsCount];
                        tmpListList.Add(new List<DataWithMarking>());   // 全体用
                        for (int i = 0; i < sectorsCount; ++i)
                        {
                            tmpListList.Add(new List<DataWithMarking>());
                        }
                        if (wtList.Count > 0)
                        {
                            tmpList = new List<DataWithMarking>();   // 加重平均母数用
                            tmpNumList = new List<double>();
                        }
                    }
                    else
                    {
                        tmpList = new List<DataWithMarking>();  // 統計量母数用
                        tmpNumList = new List<double>();
                    }
                    break;
            }
            if (TabulateFullQuantity) CutNA = false;
            // データ走査
            for (int i = 0; i < dataCount; ++i)
            {
                if (data[0][i].IsDeleted || !FilteringFlag[i]) continue;
                // WB
                double wb = (weightback[i] as NData).Value;
                // 結果配列一段階インデックス
                List<int> keyIdx = new List<int>();
                if (keyQsectorsCount == 0)  // 分類アイテムなし
                {
                    keyIdx.Add(0);
                }
                else    // 分類アイテムあり
                {
                    if (keyData[i].DataType != DataType.NormalData) continue;
                    switch (keyQType)
                    {
                        case QuestionType.SA:   // SA
                            {
                                int n = (keyData[i] as SAData).Value;
                                if (n >= 1 && n <= keyQsectorsCount)
                                {
                                    keyIdx.Add(n - 1);
                                }
                                break;
                            }
                        case QuestionType.MA:   // MA
                            /*
                            for (int j = 0; j < keyQsectorsCount; ++j)
                            {
                                int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                if (((keyData[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                {
                                    n = j + 1;
                                    keyIdx.Add(n - 1);
                                }
                            }
                            */
                            {
                                int[] sectors = (keyData[i] as MAData).SectorsArray;
                                if (sectors != null)
                                {
                                    for (int j = 0; j < sectors.Length; ++j)
                                    {
                                        int n = sectors[j];
                                        if (n <= keyQsectorsCount)
                                        {
                                            keyIdx.Add(n - 1);
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
                // 分類アイテムごとに集計
                for (int j = 0; j < keyIdx.Count; ++j)
                {
                    int x = keyIdx[j];
                    // 子質問ごとに集計
                    if (tmpListList != null)
                    {
                        for (int s = 0; s <= sectorsCount; ++s)
                        {
                            tmpListList[s].Clear();
                        }
                        OperateArray.Initialize<double>(ref tmpNumArray);
                    }
                    if (tmpNumList != null) tmpNumList.Clear();
                    for (int c = 0; c < childQuestionsCount; ++c)
                    {
                        if (tmpList != null && (significanceTestCode == SignificanceTestCode.BetweenSectors || c == 0))
                        {
                            tmpList.Clear();
                        }
                        // if (data[c][i].DataType != DataType.IVData || TabulateFullQuantity)
                        bool IncrementTotal = true;
                        switch (data[c][i].DataType)
                        {
                            case DataType.NAData:
                                IncrementTotal = !CutNA;
                                break;
                            case DataType.IVData:
                                IncrementTotal = TabulateFullQuantity;
                                break;
                        }
                        /*
                        if (IncrementTotal)
                        {
                            if (SignificanceTestOn)
                            {
                                dataArray[x][c][0].AddDetail(1.0, 1.0);
                                dataArray[x][c][1].AddDetail(1.0, wb);
                                if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                                {
                                    for (int t = 0; t < tmpListList[0].Count; ++t)
                                    {
                                        tmpListList[0][t].AddOverlapDetail(c + 1, 1.0, 1.0, wb);
                                    }
                                    tmpListList[0].Add(dataArray[x][c][1]);
                                }
                            }
                            else
                            {
                                // WB前全体
                                nArray[x][c][0] += 1.0;
                                // 全体
                                nArray[x][c][1] += wb;
                            }
                        }
                        */
                        // int y = nArray[x][c].GetUpperBound(0) - 1;
                        int y = 0;
                        int validcasesIdx = 0;
                        if (SignificanceTestOn)
                        {
                            y = dataArray[x][c].GetUpperBound(0) - (qType == QuestionType.N ? 1 : 2);
                            validcasesIdx = dataArray[x][c].GetUpperBound(0);
                        }
                        else
                        {
                            y = nArray[x][c].GetUpperBound(0) - (qType == QuestionType.N ? 1 : 2);
                            validcasesIdx = nArray[x][c].GetUpperBound(0);
                        }
                        switch (data[c][i].DataType)
                        {
                            case DataType.NAData:   // 無回答
                                if (SignificanceTestOn)
                                {
                                    double w = 0.0;
                                    dataArray[x][c][y].AddDetail(1.0, wb);
                                    if (!isLower)
                                    {
                                        if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                                        {
                                            dataArray[x][c][y + 2].AddDetail(w, wb);
                                            for (int t = 0; t < tmpList.Count; ++t)
                                            {
                                                tmpList[t].AddOverlapDetail(c + 1, tmpNumList[t], w, wb);
                                            }
                                            tmpList.Add(dataArray[x][c][y + 2]);
                                            tmpNumList.Add(w);
                                        }
                                        else
                                        {
                                            dataArray[x][c][y + 2].AddDetail(1.0, wb);
                                        }
                                    }
                                }

                                else
                                {
                                    nArray[x][c][y] += wb;
                                    if (!isLower)
                                    {
                                        nArray[x][c][validcasesIdx] += wb;
                                    }
                                }
                                break;
                            case DataType.IVData:   // 非該当
                                if (SignificanceTestOn)
                                {
                                    dataArray[x][c][y + (TabulateFullQuantity && IVtoNA ? 0 : 1)].AddDetail(1.0, wb);

                                }
                                else
                                {
                                    nArray[x][c][y + (TabulateFullQuantity && IVtoNA ? 0 : 1)] += wb;
                                }
                                break;
                            case DataType.NormalData:   // 通常データ
                                switch (childQuestionType[c])
                                {
                                    case QuestionType.SA:   // SA
                                        {
                                            int n = (data[c][i] as SAData).Value;
                                            if (n >= 1 && n <= sectorsCount)
                                            {
                                                bool weighted = wtList.ContainsKey(n.ToString());
                                                if (SignificanceTestOn)
                                                {
                                                    dataArray[x][c][1 + n].AddDetail(1.0, wb);
                                                    if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                                                    {
                                                        tmpNumArray[c, n - 1] = 1.0;
                                                        //if (tmpNumList != null)
                                                        //{
                                                        //    double w = 0.0;
                                                        //    if (weighted) w = (double)wtList[n.ToString()];
                                                        //    dataArray[x][c][y + 2].AddDetail(w, wb);    // 加重平均母数
                                                        //    for (int t = 0; t < tmpList.Count; ++t)
                                                        //    {
                                                        //        tmpList[t].AddOverlapDetail(c + 1, tmpNumList[t], w, wb);
                                                        //    }
                                                        //    tmpList.Add(dataArray[x][c][y + 2]);
                                                        //    tmpNumList.Add(w);
                                                        //}
                                                        if (tmpNumList != null && weighted)
                                                        {
                                                            double w = (double)wtList[n.ToString()];
                                                            dataArray[x][c][y + 2].AddDetail(w, wb);
                                                            for (int t = 0; t < tmpList.Count; ++t)
                                                            {
                                                                tmpList[t].AddOverlapDetail(c + 1, tmpNumList[t], w, wb);
                                                            }
                                                            tmpList.Add(dataArray[x][c][y + 2]);
                                                            tmpNumList.Add(w);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (weighted) dataArray[x][c][y + 2].AddDetail(1.0, wb);
                                                    }
                                                }
                                                else
                                                {
                                                    nArray[x][c][1 + n] += wb;  // 該当する選択肢
                                                    if (weighted)
                                                    {
                                                        // effectiveSamplesCount[x][c] += wb;
                                                        nArray[x][c][y + 2] += wb;  // 加重平均母数
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // 無回答扱い
                                                IncrementTotal = !CutNA;
                                                if (SignificanceTestOn)
                                                {
                                                    dataArray[x][c][y].AddDetail(1.0, wb);
                                                }
                                                else
                                                {
                                                    nArray[x][c][y] += wb;
                                                }
                                            }
                                            break;
                                        }
                                    case QuestionType.MA:   // MA
                                        {
                                            int[] sectors = (data[c][i] as MAData).SectorsArray;
                                            bool isNA = true;
                                            bool isEffectiveSample = false;
                                            double w = 0.0;
                                            /*
                                            for (int m = 0; m < sectorsCount; ++m)
                                            {
                                                int idx = m / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                                int e = m % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                                if (((data[c][i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                                {
                                                    int n = m + 1;
                                                    nArray[x][c][1 + n] += wb;  // 該当する選択肢
                                                    if (!isEffectiveSample) isEffectiveSample = wtList.ContainsKey(n.ToString());
                                                }
                                            }
                                            */
                                            if (sectors != null)
                                            {
                                                for (int m = 0; m < sectors.Length; ++m)
                                                {
                                                    int n = sectors[m];
                                                    if (n <= sectorsCount)
                                                    {
                                                        bool weighted = wtList.ContainsKey(n.ToString());
                                                        if (SignificanceTestOn)
                                                        {
                                                            dataArray[x][c][1 + n].AddDetail(1.0, wb);
                                                            if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                                                            {
                                                                for (int t = 0; t < tmpList.Count; ++t)
                                                                {
                                                                    tmpList[t].AddOverlapDetail(n, 1.0, 1.0, wb);
                                                                }
                                                                tmpList.Add(dataArray[x][c][1 + n]);
                                                            }
                                                            else    // significanceTestCode == SignificanceTestCode.BetweenChildQuestions
                                                            {
                                                                tmpNumArray[c, n - 1] = 1.0;
                                                                if (weighted)
                                                                {
                                                                    w += (double)wtList[n.ToString()];
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            nArray[x][c][1 + n] += wb;  // 該当する選択肢
                                                        }
                                                        isNA = false;
                                                        if (!isEffectiveSample) isEffectiveSample = weighted;
                                                    }
                                                }
                                            }
                                            if (isNA)
                                            {
                                                IncrementTotal = !CutNA;
                                                if (SignificanceTestOn)
                                                {
                                                    dataArray[x][c][y].AddDetail(1.0, wb);
                                                    if (!isLower)
                                                    {
                                                        dataArray[x][c][validcasesIdx].AddDetail(1.0, wb);
                                                    }
                                                }
                                                else
                                                {
                                                    nArray[x][c][y] += wb;  // 無回答扱い
                                                    if (!isLower)
                                                    {
                                                        nArray[x][c][validcasesIdx] += wb;  // 無回答扱い
                                                    }
                                                }
                                            }
                                            // if (isEffectiveSample) effectiveSamplesCount[x][c] += wb;
                                            if (isEffectiveSample)
                                            {
                                                if (SignificanceTestOn)
                                                {
                                                    if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                                                    {
                                                        dataArray[x][c][y + 2].AddDetail(w, wb);
                                                        for (int t = 0; t < tmpList.Count; ++t)
                                                        {
                                                            tmpList[t].AddOverlapDetail(c + 1, tmpNumList[t], w, wb);
                                                        }
                                                        tmpList.Add(dataArray[x][c][y + 2]);
                                                        tmpNumList.Add(w);
                                                    }
                                                    else
                                                    {
                                                        dataArray[x][c][y + 2].AddDetail(1.0, wb);
                                                    }
                                                }
                                                else
                                                {
                                                    nArray[x][c][y + 2] += wb;   // 加重平均母数
                                                }
                                            }
                                            break;
                                        }
                                    case QuestionType.N:    // 数値回答
                                        {
                                            double v = (data[c][i] as NData).Value;
                                            // 値を個別に確保
                                            // normalDatas[x][c][++lastIndex[x][c]] = v;
                                            // if (wb > 0.0) normalDatas[x][c][++lastIndex[x][c]] = v;
                                            if (wb > 0.0) normalDatas[x][c][++lastIndex[x][c]] = new NumericData(v, wb);
                                            if (SignificanceTestOn)
                                            {
                                                dataArray[x][c][2].AddDetail(v, wb);  // 統計量母数
                                                for (int t = 0; t < tmpList.Count; ++t)
                                                {
                                                    tmpList[t].AddOverlapDetail(c + 1, tmpNumList[t], v, wb);
                                                }
                                                tmpList.Add(dataArray[x][c][2]);
                                                tmpNumList.Add(v);
                                            }
                                            else
                                            {
                                                // 統計量母数
                                                nArray[x][c][2] += wb;
                                                // 合計
                                                nArray[x][c][3] += v * wb;
                                                // 平方の合計
                                                nArray[x][c][4] += Math.Pow(v, 2.0) * wb;
                                            }
                                            break;
                                        }
                                }
                                break;
                        }
                        if (IncrementTotal)
                        {
                            if (SignificanceTestOn)
                            {
                                dataArray[x][c][0].AddDetail(1.0, 1.0);
                                dataArray[x][c][1].AddDetail(1.0, wb);
                                if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                                {
                                    if (qType != QuestionType.N)
                                    {
                                        for (int t = 0; t < tmpListList[0].Count; ++t)
                                        {
                                            tmpListList[0][t].AddOverlapDetail(c + 1, 1.0, 1.0, wb);
                                        }
                                        tmpListList[0].Add(dataArray[x][c][1]);
                                        for (int s = 1; s < tmpListList.Count; ++s)
                                        {
                                            double value2 = tmpNumArray[c, s - 1];
                                            for (int t = 0; t < tmpListList[s].Count; ++t)
                                            {
                                                double value1 = tmpNumArray[tmpListList[s][t].SectorNumber - 1, s - 1];
                                                tmpListList[s][t].AddOverlapDetail(c + 1, value1, value2, wb);
                                            }
                                            tmpListList[s].Add(dataArray[x][c][1 + s]);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // WB前全体
                                nArray[x][c][0] += 1.0;
                                // 全体
                                nArray[x][c][1] += wb;
                            }
                        }
                    }
                }
            }

            // normalDatasのソート (クイックソート)
            if (qType == QuestionType.N)
            {
                for (int i = 0; i < k; ++i)
                {
                    for (int c = 0; c < childQuestionsCount; ++c)
                    {
                        if (lastIndex[i][c] > 0)
                        {
                            // Array.Sort<double>(normalDatas[i][c], 0, lastIndex[i][c] + 1);
                            Array.Sort<NumericData>(normalDatas[i][c], 0, lastIndex[i][c] + 1);
                        }
                    }
                }
            }

            if (SignificanceTestOn)
            {
                StreamWriter writer = GlobalTabulation.CreateSignificanceTestLogWriter(ref SignificanceTestLogFilePath);
                try
                {
                    string subHeaderBuffer = null;
                    if (writer != null)
                    {
                        writer.WriteLine("GT SignificanceTest between "
                            + (significanceTestCode == SignificanceTestCode.BetweenSectors ? "categories" : "subitems")
                            + " (" + qTypeOr.ToString() + " Matrix)");
                        System.Text.StringBuilder builder = new System.Text.StringBuilder("");
                        builder.AppendLine(string.Format("Item:{0}", qName));
                        if (qType != QuestionType.N)
                        {
                            builder.AppendLine("\tCategory");
                            for (int i = 0; i < sectorsCount; ++i)
                            {
                                builder.AppendLine(string.Format("\t\t{0}:{1}", i + 1, sectorDescriptions[i]));
                            }
                        }
                        builder.AppendLine("\tSubItem");
                        for (int i = 0; i < childQuestionsCount; ++i)
                        {
                            builder.AppendLine(string.Format("\t\t{0}:{1}", i + 1, childQuestionName[i]));
                        }
                        subHeaderBuffer = builder.ToString();
                    }

                    //if (isMTS)//#OutputFormatting //Need to calculate significance for MTS as per customer clarification :- https://redmine.macromill.com/issues/166518#note-1
                    //SignificanceTest(ref dataArray, significanceTestLevel, significanceTestCode, writer, keyQName, qType, subHeaderBuffer, 0);
                    //else
                    if (significanceTestCode == SignificanceTestCode.BetweenChildQuestions)
                        SignificanceTest(ref dataArray, significanceTestLevel, significanceTestCode, writer, keyQName, qType, subHeaderBuffer, 0);
                    else
                        SignificanceTest(ref dataArray, significanceTestLevel, significanceTestCode, writer, keyQName, qType, subHeaderBuffer, subTotalCount);

                    if (writer != null) writer.Close();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (writer != null) writer.Dispose();
                }
            }

            // 出力配列の仕上げ
            for (int i = 0; i < k; ++i)
            {
                for (int c = 0; c < childQuestionsCount; ++c)
                {
                    switch (qType)
                    {
                        case QuestionType.SA:
                        case QuestionType.MA:
                            // N値の投入
                            if (SignificanceTestOn)
                            {
                                for (int j = 0; j < dataArray[i][c].Length; ++j)
                                {
                                    if ((dataArray[i][c][j].CellType & CellType.PopulationCell) == CellType.PopulationCell)
                                    {
                                        resultArray[i][3 + c, 3 + j] = new DataWithMarking(dataArray[i][c][j].Count.ToString());
                                    }
                                    else
                                    {
                                        resultArray[i][3 + c, 3 + j] = dataArray[i][c][j];
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < nArray[i][c].Length; ++j)
                                {
                                    resultArray[i][3 + c, 3 + j] = new DataWithMarking(nArray[i][c][j].ToString());
                                }
                            }
                            // ％値の投入
                            if (SignificanceTestOn)
                            {
                                if (dataArray[i][c][1].NumValue != 0.0)
                                {
                                    for (int j = 2; j < dataArray[i][c].Length - 1; ++j)
                                    {
                                        resultArray[i][3 + c, 3 + j].Percent = dataArray[i][c][j].NumValue * 100.0 / dataArray[i][c][1].NumValue;
                                    }

                                }
                            }
                            else
                            {
                                /*
                                if (nArray[i][c][1] == 0.0)
                                {
                                    // for (int j = 2; j < nArray[i][c].Length; ++j)
                                    for (int j = 2; j < nArray[i][c].Length - 1; ++j)
                                    {
                                        resultArray[i][3 + c, 3 + j].Percent = 0.0;
                                    }
                                }
                                else
                                {
                                    // for (int j = 2; j < nArray[i][c].Length; ++j)
                                    for (int j = 2; j < nArray[i][c].Length - 1; ++j)
                                    {
                                        resultArray[i][3 + c, 3 + j].Percent = nArray[i][c][j] * 100.0 / nArray[i][c][1];
                                    }
                                }
                                */
                                if (nArray[i][c][1] != 0.0)
                                {
                                    // for (int j = 2; j < nArray[i][c].Length; ++j)
                                    for (int j = 2; j < nArray[i][c].Length - 1; ++j)
                                    {
                                        resultArray[i][3 + c, 3 + j].Percent = nArray[i][c][j] * 100.0 / nArray[i][c][1];
                                    }
                                }
                            }
                            // 加重平均の投入
                            /*
                            double s = 0.0;
                            int cnt = 0;
                            for (int j = 0; j < sectorsCount; ++j)
                            {
                                if (!string.IsNullOrWhiteSpace(wt[j]))
                                {
                                    double w = 0.0;
                                    if (double.TryParse(wt[j], out w))
                                    {
                                        ++cnt;
                                        s += nArray[i][c][2 + j] * w;
                                        resultArray[i][2, 5 + j] = new DataWithMarking(wt[j]);
                                    }
                                }
                            }
                            if (cnt > 0)
                            {
                                resultArray[i][3 + c, 2 + 2 + sectorsCount + 3] = new DataWithMarking((s / cnt).ToString());
                            }
                            */
                            if (wtList.Count > 0)
                            {
                                double s = significanceTestCode == SignificanceTestCode.BetweenChildQuestions ? dataArray[i][c][1 + sectorsCount + 3].NumValue : 0.0;

                                //double s = significanceTestCode == SignificanceTestCode.BetweenChildQuestions ? (isLower ? (dataArray[i][c][1 + sectorsCount + 3].NumValue)
                                //    : ((dataArray[i][c][1 + sectorsCount + 3].NumValue) - ((dataArray[i][c][1 + sectorsCount + 1].NumValue))))
                                //    : (isLower ? 0.0 : 0.0 /*nArray[i][c][1 + sectorsCount + 1]*/);
                                for (int j = 1; j <= sectorsCount; ++j)
                                {
                                    if (wtList.ContainsKey(j.ToString()))
                                    {
                                        // s += nArray[i][c][2 + j] * (double)wtList[j.ToString()];
                                        // resultArray[i][2, 5 + j] = new DataWithMarking(wt[j - 1]);
                                        switch (significanceTestCode)
                                        {
                                            case SignificanceTestCode.BetweenSectors:
                                                s += dataArray[i][c][1 + j].NumValue * (double)wtList[j.ToString()];
                                                break;
                                            case SignificanceTestCode.BetweenChildQuestions:
                                                break;
                                            default:
                                                s += nArray[i][c][1 + j] * (double)wtList[j.ToString()];
                                                break;

                                        }
                                        resultArray[i][2, 4 + j] = new DataWithMarking(wt[j - 1]);
                                    }
                                }
                                /*
                                if (effectiveSamplesCount[i][c] > 0.0)
                                {
                                    resultArray[i][3 + c, 2 + 2 + sectorsCount + 3] = new DataWithMarking((s / effectiveSamplesCount[i][c]).ToString());
                                }
                                */
                                if (SignificanceTestOn)
                                {
                                    double d = significanceTestCode == SignificanceTestCode.BetweenSectors ? dataArray[i][c][1 + sectorsCount + 3].NumValue : dataArray[i][c][1 + sectorsCount + 3].Count;
                                    if (d > 0.0)
                                    {
                                        resultArray[i][3 + c, 2 + 2 + sectorsCount + 4] = new DataWithMarking((s / d).ToString());
                                        // resultArray[i][3 + c, 2 + 2 + sectorsCount + 4].SetSignificanceCharacters(dataArray[i][c][1 + sectorsCount + 3].SignificanceCharacters());
                                        dataArray[i][c][1 + sectorsCount + 3].CloneSignificanceSectorNumbers(ref resultArray[i][3 + c, 2 + 2 + sectorsCount + 4]);
                                    }
                                    else
                                    {
                                        resultArray[i][3 + c, 2 + 2 + sectorsCount + 4] = new DataWithMarking("-", false);
                                    }
                                }
                                else
                                {
                                    if (nArray[i][c][1 + sectorsCount + 3] > 0.0)
                                    {
                                        resultArray[i][3 + c, 2 + 2 + sectorsCount + 4] = new DataWithMarking((s / nArray[i][c][1 + sectorsCount + 3]).ToString());
                                    }
                                    else
                                    {
                                        // resultArray[i][3 + c, 2 + 2 + sectorsCount + 4] = new DataWithMarking("0.0");
                                        resultArray[i][3 + c, 2 + 2 + sectorsCount + 4] = new DataWithMarking("-", false);
                                    }
                                }
                            }
                            break;
                        case QuestionType.N:
                            double n = 0.0; // 統計量母数
                            if (SignificanceTestOn)
                            {
                                resultArray[i][2 + c, 3] = dataArray[i][c][0];  // WB前全体
                                resultArray[i][2 + c, 4] = dataArray[i][c][1];  // 全体
                                n = dataArray[i][c][2].Count;
                                resultArray[i][2 + c, 5] = new DataWithMarking(n.ToString());  // 統計量母数
                                resultArray[i][2 + c, 6] = new DataWithMarking(n == 0.0 ? "-" : dataArray[i][c][2].NumValue.ToString());  // 合計
                            }
                            else
                            {
                                resultArray[i][2 + c, 3] = new DataWithMarking(nArray[i][c][0].ToString()); // WB前全体
                                resultArray[i][2 + c, 4] = new DataWithMarking(nArray[i][c][1].ToString()); // 全体
                                n = nArray[i][c][2];
                                resultArray[i][2 + c, 5] = new DataWithMarking(n.ToString()); // 統計量母数
                                // resultArray[i][2 + c, 6] = new DataWithMarking(nArray[i][c][3].ToString()); // 合計
                                resultArray[i][2 + c, 6] = new DataWithMarking(n == 0.0 ? "-" : nArray[i][c][3].ToString());   // 合計
                            }
                            // 平均
                            // double average = 0.0;
                            double average = double.NaN;
                            if (n != 0.0)
                            {
                                average = (SignificanceTestOn ? dataArray[i][c][2].NumValue : nArray[i][c][3]) / n;    // 合計÷統計量母数
                            }
                            // resultArray[i][2 + c, 7] = new DataWithMarking(average.ToString());
                            resultArray[i][2 + c, 7] = new DataWithMarking(double.IsNaN(average) ? "-" : average.ToString());
                            if (SignificanceTestOn && !double.IsNaN(average))
                            {
                                // resultArray[i][2 + c, 7].SetSignificanceCharacters(dataArray[i][c][2].SignificanceCharacters());
                                dataArray[i][c][2].CloneSignificanceSectorNumbers(ref resultArray[i][2 + c, 7]);
                            }
                            // 標準偏差
                            // double stdev = 0.0;
                            double stdev = double.NaN;
                            if (n > 1.0)
                            {
                                // 統計量母数×平方の合計－合計の平方
                                double weightSum = 0;
                                double standardDeviation = 0;
                                double normalValue = 0;
                                if (SignificanceTestOn)
                                {
                                    double WeightedMean = dataArray[i][c][2].NumValue / n;
                                    for (int j = 0; j < normalDatas[i][c].Length; j++)
                                    {
                                        normalValue = normalDatas[i][c][j].Value;
                                        standardDeviation += normalDatas[i][c][j].WeightBack * Math.Pow((normalValue - WeightedMean), 2.0);
                                    }
                                }
                                else
                                {
                                    double WeightedMean = nArray[i][c][3] / n;
                                    for (int j = 0; j < normalDatas[i][c].Length; j++)
                                    {
                                        normalValue = normalDatas[i][c][j].Value;
                                        standardDeviation += normalDatas[i][c][j].WeightBack * Math.Pow((normalValue - WeightedMean), 2.0);
                                    }
                                }
                                stdev = Math.Sqrt(standardDeviation / (((n - 1) / n) * n));
                            }
                            // resultArray[i][2 + c, 8] = new DataWithMarking(stdev.ToString());
                            resultArray[i][2 + c, 8] = new DataWithMarking(double.IsNaN(stdev) ? "-" : stdev.ToString());
                            // 最小値
                            // double min = 0.0;
                            double min = double.NaN;
                            // 最大値
                            // double max = 0.0;
                            double max = double.NaN;
                            // 中央値
                            // double median = 0.0;
                            double median = double.NaN;
                            if (lastIndex[i][c] > -1)
                            {
                                // min = normalDatas[i][c][0];
                                // max = normalDatas[i][c][lastIndex[i][c]];
                                min = normalDatas[i][c][0].Value;
                                max = normalDatas[i][c][lastIndex[i][c]].Value;
                                /*
                                int medIdx = lastIndex[i][c] / 2;
                                //if (lastIndex[i][c] % 2 == 0) // 要素数が奇数
                                //{
                                //    median = normalDatas[i][c][medIdx];
                                //}
                                //else    // 要素数が偶数
                                //{
                                //    median = (normalDatas[i][c][medIdx] + normalDatas[i][c][medIdx + 1]) / 2.0;
                                //}
                                median = normalDatas[i][c][medIdx];
                                double medPos = (double)lastIndex[i][c] / 2.0;
                                if (medPos > (double)medIdx)
                                {
                                    double d = (normalDatas[i][c][medIdx + 1] - normalDatas[i][c][medIdx]) * (medPos - (double)medIdx);
                                    median += d;
                                }
                                */
                                median = GlobalTabulation.GetMedian(normalDatas[i][c], lastIndex[i][c]);
                            }
                            // resultArray[i][2 + c, 9] = new DataWithMarking(min.ToString());
                            // resultArray[i][2 + c, 10] = new DataWithMarking(max.ToString());
                            // resultArray[i][2 + c, 11] = new DataWithMarking(median.ToString());
                            resultArray[i][2 + c, 9] = new DataWithMarking(double.IsNaN(min) ? "-" : min.ToString());
                            resultArray[i][2 + c, 10] = new DataWithMarking(double.IsNaN(max) ? "-" : max.ToString());
                            resultArray[i][2 + c, 11] = new DataWithMarking(double.IsNaN(median) ? "-" : median.ToString());
                            int x = SignificanceTestOn ? 2 : 4;
                            if (SignificanceTestOn)
                            {
                                resultArray[i][2 + c, 12] = dataArray[i][c][++x];    // 無回答
                                resultArray[i][2 + c, 13] = dataArray[i][c][++x];    // 非該当
                            }
                            else
                            {
                                resultArray[i][2 + c, 12] = new DataWithMarking(nArray[i][c][++x].ToString());    // 無回答
                                resultArray[i][2 + c, 13] = new DataWithMarking(nArray[i][c][++x].ToString());    // 非該当
                            }
                            break;
                    }
                }
                // ランキングマーキング
                if (qType != QuestionType.N)
                {
                    MarkingRanking(ref resultArray[i], 3, 2 + childQuestionsCount, 5, 4 + sectorsCount, questionType);
                }
            }
            return null;
        }

        /// <alias>GetGTArrayMatrix02</alias>
        /// <summary>
        /// <para>エイリアス:GetGTArrayMatrix02</para>
        /// 分類アイテムがない場合にマトリクス質問のGT表イメージ二次元配列を生成する
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="childQuestionType">子質問の質問タイプを表すQuestionType列挙型の値を要素とする配列</param>
        /// <param name="childQuestionDescription">子質問文を要素とする配列</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="FilteringFlag">絞り込みフラグ (省略可能)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (省略可能)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (省略可能)</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue(省略可、既定値true)</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue (省略可、既定値false)</param>
        /// <param name="locale">多言語情報取得用情報(省略可、規定値ja)</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue (省略可、既定値false)</param>
        /// <param name="significanceTestCode">項目間検定の種類を表すSignificanceTestCode列挙型のコード値 (省略可、既定値Off)</param>
        /// <param name="significanceTestLevel">
        /// 項目間検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は最大2つ
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="SignificanceTestLogFilePath">
        /// 検定のログファイルの出力先パス (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// </param>
        /// <param name="qName">
        /// 集計対象アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <param name="childQuestionName">
        /// 子アイテム名からなる配列
        /// <note>この値は<paramref name="significanceTestCode"/>がOff以外の有効な値の場合以外では無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetGTArrayMatrix(QuestionType questionType
                , string[] sectorDescription, List<List<Data>> data
                , QuestionType[] childQuestionType, string[] childQuestionDescription
                , out DataWithMarking[,] resultArray, Translation translation
                , TabulationDescriptions descs
                , bool[] FilteringFlag = null
                , List<Data> weightback = null, string[] wt = null
                , bool TabulateFullQuantity = true, bool IVtoNA = false, string locale = "ja"
                , bool CutNA = false
                , SignificanceTestCode significanceTestCode = SignificanceTestCode.Off
                , double[] significanceTestLevel = null, string SignificanceTestLogFilePath = null
                , string qName = null, string[] childQuestionName = null, bool hasCount = false, int subTotalCount = 0, bool isMTS = false, QuestionType qTypeOr = 0, bool isLower = true
                )
        {
            DataWithMarking[][,] res = null;
            QCWebException exception = GetGTArrayMatrix(questionType, (QuestionType)0, sectorDescription, null, data, null
                                                      , childQuestionType, childQuestionDescription
                                                      , out res, translation, descs, FilteringFlag, weightback, wt, TabulateFullQuantity, locale, IVtoNA, CutNA
                                                      , significanceTestCode, significanceTestLevel
                                                      , SignificanceTestLogFilePath, qName, childQuestionName, null, hasCount, subTotalCount, isMTS, qTypeOr: qTypeOr, isLower: isLower);
            resultArray = null;
            if (exception != null) return exception;
            if (res != null && res.Length == 1) resultArray = res[0];
            return null;
        }
        #endregion
        #endregion
        #endregion

    }
    #endregion

    #region CrossTabulationクラス
    /// <summary>
    /// クロス表の作成に必要なメソッドをまとめた静的クラス
    /// </summary>
    [ComVisible(false), Guid("2AA157DB-4DAA-4e63-9A77-16FF5E07254E")]
    public static class CrossTabulation
    {
        #region クロス表作成関連
        private const char MULTIPLY_LETTER = 'x';   // ×の代替文字

        #region 引数チェック
        /// <summary>
        /// GetCrossArrayメソッドの引数のチェックを行うサブルーチン
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="axisQuestionType">集計軸アイテムの質問タイプを表すQuestionType列挙型の値からなるListクラスのインスタンスへの参照</param>
        /// <param name="axisQDescription">集計軸アイテムの質問文からなるListクラスのインスタンスへの参照</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="axisData">集計軸アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="sectorsCount">選択肢数 (戻り値)</param>
        /// <param name="keyQsectorDescription">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="keyQsectorsCount">分類アイテムの選択肢数 (戻り値)</param>
        /// <param name="axisQsectorDescription">集計軸アイテムの選択肢文を要素とする配列からなるListクラスのインスタンスへの参照</param>
        /// <param name="axisQsectorsCount">集計軸アイテムの選択肢数からなるListクラスのインスタンスへの参照 (戻り値)</param>
        /// <param name="axesCount">集計軸アイテムの数 (戻り値)</param>
        /// <param name="FilteringFlag">絞り込みフラグ配列 (戻り値)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (戻り値)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (戻り値)</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="exception">問題の情報を保持したQCWebExceptionクラスのインスタンスへの参照 (戻り値)</param>
        /// <returns>問題ない場合true、問題ある場合false</returns>
        private static bool checkGetCrossArrayArguments(
                  QuestionType questionType, QuestionType keyQuestionType, List<QuestionType> axisQuestionType, List<string> axisQDescription
                , List<Data> data, List<Data> keyData, List<List<Data>> axisData
                , ref string[] sectorDescription, ref int sectorsCount
                , ref string[] keyQsectorDescription, ref int keyQsectorsCount
                , List<string[]> axisQsectorDescription, ref List<int> axisQsectorsCount, ref int axesCount
                , ref bool[] FilteringFlag
                , ref List<Data> weightback, ref string[] wt, ref TabulationDescriptions descs
                , out QCWebException exception)
        {
            exception = null;
            // マトリクスはNG
            if ((questionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustTabulationSubjectItemQuestionTypeMessageIndex));
                return false;
            }
            bool res = GlobalTabulation.checkGTorCrossArguments(questionType, keyQuestionType, data, keyData
                        , ref sectorDescription, ref sectorsCount, ref keyQsectorDescription, ref keyQsectorsCount
                        , ref FilteringFlag, ref weightback, ref wt, ref descs, out exception);
            if (!res) return false;
            if (axisQuestionType == null || axisData == null || axisQsectorDescription == null || axisQDescription == null)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.InsufficientAxisItemInformationsMessageIndex));
                return false;
            }
            axesCount = axisQuestionType.Count;
            switch (axesCount)
            {
                case 1:
                case 2:
                    if (axisData.Count != axesCount || axisQsectorDescription.Count != axesCount || axisQDescription.Count != axesCount)
                    {
                        exception = new QCWebException(new Message(Constants.CommonMessageIndex.ExistMismatchBetweenAxisItemInformationsMessageIndex));
                        return false;
                    }
                    break;
                default:
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustAxisItemInformationsMessageIndex));
                    return false;
            }
            // それぞれの軸アイテム情報
            axisQsectorsCount = new List<int>(axesCount);
            for (int i = 0; i < axesCount; ++i)
            {
                bool isError = false;
                // マトリクスはNG
                if (!(isError = (axisQuestionType[i] & QuestionType.MatrixParent) == QuestionType.MatrixParent))
                {
                    // SAまたはMAでなければNG
                    isError = (int)(axisQuestionType[i] & (QuestionType.SA | QuestionType.MA)) == 0;
                }
                if (isError)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustAxisItemQuestionTypeMessageIndex)
                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                 , (i + 1).ToString(), axisQuestionType[i].ToString());
                    return false;
                }
                // 軸アイテムのデータがなければNG
                if (axisData[i] == null)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.AxisItemDataIsNullMessageIndex)
                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                 , (i + 1).ToString());
                    return false;
                }
                // 軸アイテムのデータ数が集計項目のデータ数と異なればNG
                if (axisData[i].Count != data.Count)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.ExistMismatchBetweenAxisItemDataMessageIndex));
                    return false;
                }
                // 軸アイテムの選択肢文情報がなければNG
                if (!(isError = axisQsectorDescription[i] == null))
                {
                    axisQsectorsCount.Add(axisQsectorDescription[i].Count());
                    isError = axisQsectorsCount[i] == 0;
                }
                //if (isError)
                //{
                //    exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyAxisItemSectorInformationMessageIndex)
                //                                 , GlobalsCommonConstant.LogLevel.FATAL
                //                                 , (i + 1).ToString());
                //    return false;
                //}
            }
            return true;
        }
        #endregion

        #region 集計表配列生成

        private static System.Text.RegularExpressions.Regex breaklineRegex = new System.Text.RegularExpressions.Regex("\r\n|\r|\n");

        /// <summary>
        /// 全体との差の検定
        /// </summary>
        /// <param name="dataArray"></param>
        /// <param name="significanceTestLevel">
        /// 検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は、最大3つ(1％、5％、10％の固定)
        /// </param>
        /// <param name="markingTotal">マーキングの対象となる全体を表すMarkingTotal列挙型の値</param>
        /// <param name="writer">検定ログ出力に使用するストリームライターへの参照 (省略可、既定値null)</param>
        /// <param name="keyQName">
        /// 分類アイテム名 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="qType">
        /// 集計対象質問のSA/MA/Nのいずれかを表すQuestionType列挙型の値 (省略可、既定値SA)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="subHeaderBuffer">
        /// サブヘッダの共通文字列 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="axesCount">
        /// 集計軸数 (省略可、既定値2)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="rowSectorNumbers">
        /// 各行の表肩アイテムの選択肢番号からなる配列 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        private static void MarkingSignificance(ref DataWithMarking[][,] dataArray
                , double[] significanceTestLevel
                , GlobalTabulation.MarkingTotal markingTotal
                , StreamWriter writer = null, string keyQName = null, QuestionType qType = QuestionType.SA
                , string subHeaderBuffer = null, int axesCount = 2, int[][] rowSectorNumbers = null)
        {
            System.Text.StringBuilder headerBuilder = null;
            System.Text.StringBuilder ptBuilder = null;
            System.Text.StringBuilder mtBuilder = null;
            StringWriter headerWriter = null;
            StringWriter ptWriter = null;
            StringWriter mtWriter = null;
            try
            {
                if (writer != null)
                {
                    headerBuilder = new System.Text.StringBuilder();
                    headerWriter = new StringWriter(headerBuilder);
                    if (qType != QuestionType.N)
                    {
                        ptBuilder = new System.Text.StringBuilder();
                        ptWriter = new StringWriter(ptBuilder);
                    }
                    mtBuilder = new System.Text.StringBuilder();
                    mtWriter = new StringWriter(mtBuilder);
                }
                for (int k = 0; k < dataArray.Length; ++k)
                {
                    if (writer != null)
                    {
                        headerBuilder.Clear();
                        headerWriter.Write("KeyItem:");
                        if (string.IsNullOrWhiteSpace(keyQName))
                        {
                            headerWriter.WriteLine();
                        }
                        else
                        {
                            headerWriter.WriteLine("{0} = {1}", keyQName, k + 1);
                        }
                        headerWriter.Write(subHeaderBuffer);
                        if (ptWriter != null)
                        {
                            ptBuilder.Clear();
                            if (axesCount == 1)
                            {
                                GlobalTabulation.LogWritePTHeaderLine(ptWriter, "cate", "-", "axiscate1", "axiscate2");
                            }
                            else
                            {
                                GlobalTabulation.LogWritePTHeaderLine(ptWriter, "cate", "axis1cate", "axis2cate1", "axis2cate2");
                            }
                        }
                        mtBuilder.Clear();
                        string tmp = qType == QuestionType.N ? "-" : "cate";
                        if (axesCount == 1)
                        {
                            GlobalTabulation.LogWriteMTHeaderLine(mtWriter, tmp, "-", "axiscate1", "axiscate2");
                        }
                        else
                        {
                            GlobalTabulation.LogWriteMTHeaderLine(mtWriter, tmp, "axis1cate", "axis2cate1", "axis2cate2");
                        }
                    }
                    // リテラル指定(将来の仕様変更によってループでの検索が必要になる可能性は低いと判断)
                    int totalRowIndex = markingTotal == GlobalTabulation.MarkingTotal.Total ? 0 : -1;
                    int totalColumnIndex = 1;
                    double N2 = 0.0;
                    double q2 = 0.0;
                    if (totalRowIndex >= 0)
                    {
                        N2 = dataArray[k][totalRowIndex, totalColumnIndex].Count;
                        q2 = dataArray[k][totalRowIndex, totalColumnIndex].WBSquareSummary;
                    }
                    for (int x = totalColumnIndex + 1; x < dataArray[k].GetLength(1); ++x)
                    {
                        double X2 = 0.0;
                        double Y2 = 0.0;
                        if (totalRowIndex >= 0)
                        {
                            X2 = dataArray[k][totalRowIndex, x].NumValue;
                            Y2 = dataArray[k][totalRowIndex, x].SquareSummary;
                        }
                        for (int y = totalRowIndex + 1; y < dataArray[k].GetLength(0); ++y)
                        {
                            DataWithMarking d1 = dataArray[k][y, x];
                            if ((d1.CellType & CellType.NAIVCell) == CellType.NAIVCell) continue;
                            if (totalRowIndex == -1)
                            {
                                if ((d1.CellType & CellType.SubTotalCell) == CellType.SubTotalCell)
                                {
                                    N2 = dataArray[k][y, totalColumnIndex].Count;
                                    q2 = dataArray[k][y, totalColumnIndex].WBSquareSummary;
                                    X2 = dataArray[k][y, x].NumValue;
                                    Y2 = dataArray[k][y, x].SquareSummary;
                                    continue;
                                }
                            }
                            if ((d1.CellType & CellType.DataCell) != CellType.DataCell) continue;
                            double N1 = dataArray[k][y, totalColumnIndex].Count;
                            double q1 = dataArray[k][y, totalColumnIndex].WBSquareSummary;
                            double X1 = d1.NumValue;
                            double N0 = N1;
                            double q0 = q1;
                            double X1inOverlap = X1;
                            double X2inOverlap = X1;
                            double X0inOverlap = X1;
                            double p = 0.0;
                            double e0, e1, e2, Z, t, d;
                            if ((d1.CellType & CellType.PopulationCell) == CellType.PopulationCell)
                            {
                                double Y1 = d1.SquareSummary;
                                double Y1inOverlap = Y1;
                                double Y2inOverlap = Y1;
                                double U1, U2, Ue, meanX1, meanX2;
                                // ログ出力
                                if (rowSectorNumbers != null)
                                {
                                    GlobalTabulation.LogWriteLineHeader(mtWriter
                                        , qType == QuestionType.N ? "-" : "WTAVG"
                                        , axesCount == 1 ? "-" : rowSectorNumbers[y][0].ToString()
                                        , rowSectorNumbers[y][1], 0);
                                }
                                Function.RoundOffTDistribution(ref N0, ref N1, ref N2, ref X1, ref X2, ref Y1, ref Y2, ref q0, ref q1, ref q2, ref X0inOverlap, ref X1inOverlap, ref X2inOverlap, ref Y1inOverlap, ref Y2inOverlap);
                                p = Function.TDistribution(N0, N1, N2, X1, X2, Y1, Y2, q0, q1, q2
                                                         , X0inOverlap, X1inOverlap, X2inOverlap, Y1inOverlap, Y2inOverlap
                                                         , out U1, out U2, out Ue, out meanX1, out meanX2
                                                         , out e0, out e1, out e2, out Z, out t, out d);
                                // ログ出力
                                GlobalTabulation.LogWriteLineData(mtWriter
                                        , N0, N1, N2, X1, X2, q0, q1, q2, X0inOverlap, X1inOverlap, X2inOverlap
                                        , e0, e1, e2, Z, t, d, p, U1, U2, Ue, null, Y1, Y2, Y1inOverlap, Y2inOverlap
                                        , meanX1, meanX2);
                            }
                            else
                            {
                                double p1, p2, p12, c;
                                // ログ出力
                                if (rowSectorNumbers != null)
                                {
                                    GlobalTabulation.LogWriteLineHeader(ptWriter, x - 1
                                            , axesCount == 1 ? "-" : rowSectorNumbers[y][0].ToString()
                                            , rowSectorNumbers[y][1], 0);
                                }
                                Function.RoundOffTDistribution(ref N0, ref N1, ref N2, ref X1, ref X2, ref q0, ref q1, ref q2, ref X0inOverlap, ref X1inOverlap, ref X2inOverlap);
                                p = Function.TDistribution(N0, N1, N2, X1, X2, q0, q1, q2, X0inOverlap, X1inOverlap, X2inOverlap
                                                         , out p1, out p2, out p12, out c, out e0, out e1, out e2
                                                         , out Z, out t, out d);
                                // ログ出力
                                GlobalTabulation.LogWriteLineData(ptWriter, N0, N1, N2, X1, X2, q0, q1, q2
                                        , X0inOverlap, X1inOverlap, X2inOverlap, e0, e1, e2, Z, t, d, p
                                        , p1, p2, p12, c);
                            }
                            if (p < 0.0) continue;
                            //if (Function.Compare(p, Function.CompareOperator.LessEqual, 0.0)) continue;
                            p *= 100.0;
                            for (int i = 0; i < significanceTestLevel.Length; ++i)
                            {
                                //if (p <= significanceTestLevel[i])
                                if (Function.Compare(p, Function.CompareOperator.LessEqual, significanceTestLevel[i]))
                                {
                                    DataMarking mark = (DataMarking)0;
                                    if (t >= 0)
                                    //if (Function.Compare(t, Function.CompareOperator.GreaterEqual, 0.0))
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                mark = DataMarking.SignificanceOneHigh;
                                                break;
                                            case 1:
                                                mark = DataMarking.SignificanceFiveHigh;
                                                break;
                                            case 2:
                                                mark = DataMarking.SignificanceTenHigh;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                mark = DataMarking.SignificanceOneLow;
                                                break;
                                            case 1:
                                                mark = DataMarking.SignificanceFiveLow;
                                                break;
                                            case 2:
                                                mark = DataMarking.SignificanceTenLow;
                                                break;
                                        }
                                    }
                                    dataArray[k][y, x].AppendMarking(mark);
                                    break;
                                }
                            }
                        }
                    }
                    if (writer != null)
                    {
                        writer.Write(headerBuilder);
                        if (ptWriter != null) writer.Write(ptBuilder);
                        writer.Write(mtBuilder);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (headerWriter != null) headerWriter.Dispose();
                if (ptWriter != null) ptWriter.Dispose();
                if (mtWriter != null) mtWriter.Dispose();
            }
        }

        /// <summary>
        /// 項目間検定
        /// </summary>
        /// <param name="dataArray"></param>
        /// <param name="significanceTestLevel">
        /// 検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は、最大2つ
        /// </param>
        /// <param name="writer">検定ログ出力に使用するストリームライターへの参照 (省略可、既定値null)</param>
        /// <param name="keyQName">
        /// 分類アイテム名 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="qType">
        /// 集計対象質問のSA/MA/Nのいずれかを表すQuestionType列挙型の値 (省略可、既定値SA)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="subHeaderBuffer">
        /// サブヘッダの共通文字列 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="axesCount">
        /// 集計軸数 (省略可、既定値2)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="rowSectorNumbers">
        /// 各行の表肩アイテムの選択肢番号からなる配列 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// <note>この引数は、<paramref name="axesCount"/>が1のときには無視される</note>
        /// </param>
        private static void SignificanceTest(ref DataWithMarking[][,] dataArray
                , double[] significanceTestLevel
                , StreamWriter writer = null, string keyQName = null, QuestionType qType = QuestionType.SA
                , string subHeaderBuffer = null, int axesCount = 2, int[][] rowSectorNumbers = null)
        {
            System.Text.StringBuilder headerBuilder = null;
            System.Text.StringBuilder ptBuilder = null;
            System.Text.StringBuilder mtBuilder = null;
            StringWriter headerWriter = null;
            StringWriter ptWriter = null;
            StringWriter mtWriter = null;
            try
            {
                if (writer != null)
                {
                    headerBuilder = new System.Text.StringBuilder();
                    headerWriter = new StringWriter(headerBuilder);
                    if (qType != QuestionType.N)
                    {
                        ptBuilder = new System.Text.StringBuilder();
                        ptWriter = new StringWriter(ptBuilder);
                    }
                    mtBuilder = new System.Text.StringBuilder();
                    mtWriter = new StringWriter(mtBuilder);
                }
                for (int k = 0; k < dataArray.Length; ++k)
                {
                    if (writer != null)
                    {
                        headerBuilder.Clear();
                        headerWriter.Write("KeyItem:");
                        if (string.IsNullOrWhiteSpace(keyQName))
                        {
                            headerWriter.WriteLine();
                        }
                        else
                        {
                            headerWriter.WriteLine("{0} = {1}", keyQName, k + 1);
                        }
                        headerWriter.Write(subHeaderBuffer);
                        if (ptWriter != null)
                        {
                            ptBuilder.Clear();
                            if (axesCount == 1)
                            {
                                GlobalTabulation.LogWritePTHeaderLine(ptWriter, "cate", "-", "axiscate1", "axiscate2");
                            }
                            else
                            {
                                GlobalTabulation.LogWritePTHeaderLine(ptWriter, "cate", "axis1cate", "axis2cate1", "axis2cate2");
                            }
                        }
                        mtBuilder.Clear();
                        string tmp = qType == QuestionType.N ? "-" : "cate";
                        if (axesCount == 1)
                        {
                            GlobalTabulation.LogWriteMTHeaderLine(mtWriter, tmp, "-", "axiscate1", "axiscate2");
                        }
                        else
                        {
                            GlobalTabulation.LogWriteMTHeaderLine(mtWriter, tmp, "axis1cate", "axis2cate1", "axis2cate2");
                        }
                    }
                    for (int y = 0; y < dataArray[k].GetLength(0); ++y)
                    {
                        double N0 = 0.0;
                        double N1 = 0.0;
                        double N2 = 0.0;
                        double q0 = 0.0;
                        double q1 = 0.0;
                        double q2 = 0.0;
                        int totalColumnIndex = 1;
                        DataWithMarking totalD1 = dataArray[k][0, totalColumnIndex];
                        for (int x = 0; x < dataArray[k].GetLength(1); ++x)
                        {
                            DataWithMarking d1 = dataArray[k][y, x];
                            if (!d1.SettedSectorInformation) continue;
                            int sNo = d1.SectorNumber;
                            int sCnt = d1.SectorsCount;
                            if (sNo >= sCnt) continue;
                            if ((d1.CellType & CellType.TotalCell) == CellType.TotalCell)
                            {
                                totalColumnIndex = x;
                                totalD1 = d1;
                            }
                            if ((d1.CellType & CellType.DataCell) != CellType.DataCell) continue;
                            for (int s = sNo + 1; s <= sCnt; ++s)
                            {
                                DataWithMarking d2 = dataArray[k][y + s - sNo, x];
                                DetailData od = d1.HasOverlap ? d1.OverlapData(s) : default(DetailData);
                                /*
                                if ((d1.CellType & CellType.TotalCell) == CellType.TotalCell)
                                {
                                    N1 = d1.Count;
                                    N2 = d2.Count;
                                    q1 = d1.WBSquareSummary;
                                    q2 = d2.WBSquareSummary;
                                    if (d1.HasOverlap)
                                    {
                                        N0 = od.count;
                                        q0 = od.wbSquareSummary;
                                    }
                                }
                                if ((d1.CellType & CellType.DataCell) != CellType.DataCell) continue;
                                */
                                DataWithMarking totalD2 = dataArray[k][y + s - sNo, totalColumnIndex];
                                N1 = totalD1.Count;
                                N2 = totalD2.Count;
                                q1 = totalD1.WBSquareSummary;
                                q2 = totalD2.WBSquareSummary;
                                if (totalD1.HasOverlap)
                                {
                                    DetailData totalOd = totalD1.OverlapData(s);
                                    N0 = totalOd.count;
                                    q0 = totalOd.wbSquareSummary;
                                }
                                double X1 = d1.NumValue;
                                double X2 = d2.NumValue;
                                double X0inOverlap = 0.0;
                                double X1inOverlap = 0.0;
                                double X2inOverlap = 0.0;
                                if (d1.HasOverlap)
                                {
                                    X0inOverlap = od.multipliedSummary;
                                    X1inOverlap = od.summary;
                                    X2inOverlap = od.overlaptargetValueSummary;
                                }
                                double p = 0.0;
                                double e0, e1, e2, Z, t, d;
                                if ((d1.CellType & CellType.PopulationCell) == CellType.PopulationCell)
                                {
                                    double Y1 = d1.SquareSummary;
                                    double Y2 = d2.SquareSummary;
                                    double Y1inOverlap = 0.0;
                                    double Y2inOverlap = 0.0;
                                    if (d1.HasOverlap)
                                    {
                                        Y1inOverlap = od.squareSummary;
                                        Y2inOverlap = od.overlaptargetSquareValueSummary;
                                    }
                                    double U1, U2, Ue, meanX1, meanX2;
                                    // ログ出力
                                    if (rowSectorNumbers != null)
                                    {
                                        GlobalTabulation.LogWriteLineHeader(mtWriter
                                                , qType == QuestionType.N ? "-" : "WTAVG"
                                                , axesCount == 1 ? "-" : rowSectorNumbers[y][0].ToString()
                                                , sNo, s);
                                    }
                                    Function.RoundOffTDistribution(ref N0, ref N1, ref N2, ref X1, ref X2, ref Y1, ref Y2, ref q0, ref q1, ref q2, ref X0inOverlap, ref X1inOverlap, ref X2inOverlap, ref Y1inOverlap, ref Y2inOverlap);
                                    p = Function.TDistribution(N0, N1, N2, X1, X2, Y1, Y2, q0, q1, q2
                                                             , X0inOverlap, X1inOverlap, X2inOverlap, Y1inOverlap, Y2inOverlap
                                                             , out U1, out U2, out Ue, out meanX1, out meanX2
                                                             , out e0, out e1, out e2, out Z, out t, out d);
                                    // ログ出力
                                    GlobalTabulation.LogWriteLineData(mtWriter
                                            , N0, N1, N2, X1, X2, q0, q1, q2, X0inOverlap, X1inOverlap, X2inOverlap
                                            , e0, e1, e2, Z, t, d, p, U1, U2, Ue, null, Y1, Y2, Y1inOverlap, Y2inOverlap
                                            , meanX1, meanX2);
                                }
                                else
                                {
                                    double p1, p2, p12, c;
                                    // ログ出力
                                    if (rowSectorNumbers != null)
                                    {
                                        GlobalTabulation.LogWriteLineHeader(ptWriter, x - 1
                                                , axesCount == 1 ? "-" : rowSectorNumbers[y][0].ToString()
                                                , sNo, s);
                                    }
                                    Function.RoundOffTDistribution(ref N0, ref N1, ref N2, ref X1, ref X2, ref q0, ref q1, ref q2, ref X0inOverlap, ref X1inOverlap, ref X2inOverlap);
                                    p = Function.TDistribution(N0, N1, N2, X1, X2, q0, q1, q2, X0inOverlap, X1inOverlap, X2inOverlap
                                                            , out p1, out p2, out p12, out c, out e0, out e1, out e2
                                                            , out Z, out t, out d);
                                    // ログ出力
                                    GlobalTabulation.LogWriteLineData(ptWriter, N0, N1, N2, X1, X2, q0, q1, q2
                                            , X0inOverlap, X1inOverlap, X2inOverlap, e0, e1, e2, Z, t, d, p
                                            , p1, p2, p12, c);
                                }
                                if (p < 0.0) continue;
                                //if (Function.Compare(p, Function.CompareOperator.LessEqual, 0.0)) continue;
                                p *= 100.0;
                                for (int i = 0; i < significanceTestLevel.Length; ++i)
                                {
                                    //if (p <= significanceTestLevel[i])
                                    if (Function.Compare(p, Function.CompareOperator.LessEqual, significanceTestLevel[i]))
                                    {
                                        if (t >= 0)
                                        //if (Function.Compare(t, Function.CompareOperator.GreaterEqual, 0.0))
                                        {
                                            d1.AppendSignificanceSectorNumber(s, i);
                                        }
                                        else
                                        {
                                            d2.AppendSignificanceSectorNumber(sNo, i);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (writer != null)
                    {
                        writer.Write(headerBuilder);
                        if (ptWriter != null) writer.Write(ptBuilder);
                        writer.Write(mtBuilder);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (headerWriter != null) headerWriter.Dispose();
                if (ptWriter != null) ptWriter.Dispose();
                if (mtWriter != null) mtWriter.Dispose();
            }
        }

        /// <summary>
        /// 検定
        /// </summary>
        /// <param name="dataArray"></param>
        /// <param name="significanceTestCode">
        /// 検定の種類を表すSignificanceTestCode列挙型のコード値<br />
        /// BetweenSectorsは項目間検定、Offは全体との差の検定であることを表す
        /// </param>
        /// <param name="significanceTestLevel">
        /// 検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は、全体との差の検定では最大3つ、項目間検定では最大2つ
        /// </param>
        /// <param name="markingTotal">
        /// マーキングの対象となる全体を表すMarkingTotal列挙型の値
        /// <note>この値は、<paramref name="significanceTestCode"/>がOffのときには無視される</note>
        /// </param>
        /// <param name="writer">検定ログ出力に使用するストリームライターへの参照 (省略可、既定値null)</param>
        /// <param name="keyQName">
        /// 分類アイテム名 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="qType">
        /// 集計対象質問のSA/MA/Nのいずれかを表すQuestionType列挙型の値 (省略可、既定値SA)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="subHeaderBuffer">
        /// サブヘッダの共通文字列 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="axesCount">
        /// 集計軸数 (省略可、既定値2)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        /// <param name="rowSectorNumbers">
        /// 各行の表肩アイテムの選択肢番号からなる配列 (省略可、既定値null)
        /// <note>この引数は、<paramref name="writer"/>がnullのときには無視される</note>
        /// </param>
        private static void SignificanceTest(ref DataWithMarking[][,] dataArray
                  , SignificanceTestCode significanceTestCode, double[] significanceTestLevel
                  , GlobalTabulation.MarkingTotal markingTotal
                  , StreamWriter writer = null, string keyQName = null, QuestionType qType = QuestionType.SA
                  , string subHeaderBuffer = null, int axesCount = 2, int[][] rowSectorNumbers = null)
        {
            if (significanceTestCode == SignificanceTestCode.BetweenSectors)
            {
                SignificanceTest(ref dataArray, significanceTestLevel, writer, keyQName, qType, subHeaderBuffer, axesCount, rowSectorNumbers);
            }
            else
            {
                MarkingSignificance(ref dataArray, significanceTestLevel, markingTotal, writer, keyQName, qType, subHeaderBuffer, axesCount, rowSectorNumbers);
            }
        }

        /// <alias>getCrossArray00</alias>
        /// <summary>
        /// <para>エイリアス:getCrossArray00</para>
        /// 分類アイテムの選択肢ごとにクロス表イメージ二次元配列を生成して、一次元×二次元のジャグ配列を返す
        /// <note>
        /// 表の単位は、集計項目×集計軸で1つ<br />
        /// 1シート1クロスの場合の集計項目の結合表は、本メソッドを軸ごとに実行して、受け取り側でマージする
        /// </note>
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescriptions">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="axisQuestionType">集計軸アイテムの質問タイプを表すQuestionType列挙型の値からなるListクラスのインスタンスへの参照</param>
        /// <param name="axisQDescription">集計軸アイテムの質問文からなるListクラスのインスタンスへの参照</param>
        /// <param name="axisQsectorDescription">集計軸アイテムの選択肢文を要素とする配列からなるListクラスのインスタンスへの参照</param>
        /// <param name="axisData">集計軸アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyQuestionType">分類アイテムの質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="keyQsectorDescription">分類アイテムの選択肢文を要素とする配列</param>
        /// <param name="keyData">分類アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="FilteringFlag">絞り込みフラグ配列 (省略可能)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (省略可能)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (省略可能)</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="coloringLevel1Percent">全体との差の色付けの水準1のパーセンテージ (省略可能, 既定値5)</param>
        /// <param name="coloringLevel2Percent">全体との差の色付けの水準2のパーセンテージ (省略可能, 既定値10)</param>
        /// <param name="SubtotalIncludeAxisNA">軸の小計に無回答を含めるかどうかを示すフラグ (省略可能, 既定値true)</param>
        /// <param name="SubtotalIncludeAxisIV">軸の小計に非該当を含めるかどうかを示すフラグ (省略可能, 既定値true)</param>
        /// <param name="markingTotal">マーキングの対象となる全体を表すMarkingTotal列挙型の値 (省略可能, 既定値MarkingTotal.Total)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue(省略可、既定値true)</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue (省略可、既定値false)</param>
        /// <param name="locale">多言語情報取得用情報(省略可、規定値ja)</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue (省略可、既定値false)</param>
        /// <param name="SignificanceTestOn">全体との差の検定または項目間検定を行うときはtrue (省略可、既定値false)</param>
        /// <param name="significanceTestCode">
        /// 項目間検定の種類を表すSignificanceTestCode列挙型のコード値 (省略可、既定値Off)<br />
        /// この値がBetweenSectors以外の値であることは、有意差検定の種類が全体との差の検定であることを表す
        /// <note>この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される</note>
        /// </param>
        /// <param name="significanceTestLevel">
        /// 項目間検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は、全体との差の検定では最大3つ、項目間検定では最大2つ (省略可、既定値null)
        /// <note>
        /// この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される<br />
        /// また、全体との差の検定時で、この値がnullのときには、既定として1％、5％、10％を指定したものとする
        /// </note>
        /// </param>
        /// <param name="SignificanceTestLogFilePath">
        /// 検定のログファイルの出力先パス (省略可、既定値null)
        /// <note>この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される</note>
        /// </param>
        /// <param name="qName">
        /// 集計対象アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <param name="keyQName">
        /// 分類アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <param name="axisQName">
        /// 集計軸アイテムのアイテム名からなるListクラスのインスタンスへの参照
        /// <note>この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        public static QCWebException GetCrossArray(
                    QuestionType questionType, string[] sectorDescriptions, List<Data> data
                  , List<QuestionType> axisQuestionType, List<string> axisQDescription, List<string[]> axisQsectorDescription, List<List<Data>> axisData
                  , QuestionType keyQuestionType, string[] keyQsectorDescription, List<Data> keyData
                  , out DataWithMarking[][,] resultArray, Translation translation
                  , TabulationDescriptions descs
                  , bool[] FilteringFlag = null, List<Data> weightback = null, string[] wt = null
                  , int coloringLevel1Percent = GlobalTabulation.COLORING_LEVEL1_DEFAULT
                  , int coloringLevel2Percent = GlobalTabulation.COLORING_LEVEL2_DEFAULT
                  , bool SubtotalIncludeAxisNA = true, bool SubtotalIncludeAxisIV = true
                  , GlobalTabulation.MarkingTotal markingTotal = GlobalTabulation.MarkingTotal.Total
                  , bool TabulateFullQuantity = true, bool IVtoNA = false
                  , string locale = "ja", bool CutNA = false
                  , bool SignificanceTestOn = false, SignificanceTestCode significanceTestCode = SignificanceTestCode.Off
                  , double[] significanceTestLevel = null, string SignificanceTestLogFilePath = null
                  , string qName = null, string keyQName = null, List<string> axisQName = null, bool hasCount = false
                  , int subTotalCnt = 0, QuestionType qTypeOr = 0, bool hasLower = true
                  )
        {
            // 戻り値の初期化
            resultArray = null;
            // 引数のチェック
            int sectorsCount = 0;
            int keyQsectorsCount = 0;
            int axesCount = 0;
            List<int> axisQsectorsCount = null;
            QCWebException exception = null;
            if (!checkGetCrossArrayArguments(questionType, keyQuestionType, axisQuestionType, axisQDescription, data, keyData, axisData
                    , ref sectorDescriptions, ref sectorsCount, ref keyQsectorDescription, ref keyQsectorsCount, axisQsectorDescription, ref axisQsectorsCount, ref axesCount
                    , ref FilteringFlag, ref weightback, ref wt, ref descs, out exception))
            {
                return exception;
            }

            // 質問タイプのシンプル化
            QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA | QuestionType.N);
            qTypeOr = qTypeOr & (QuestionType.SA | QuestionType.MA | QuestionType.N);
            QuestionType keyQType = keyQuestionType & (QuestionType.SA | QuestionType.MA);
            List<QuestionType> axisQType = new List<QuestionType>(axisQuestionType.Count);
            for (int i = 0; i < axisQuestionType.Count; ++i)
            {
                //axisQType[i] = axisQuestionType[i] & (QuestionType.SA | QuestionType.MA);
                axisQType.Add(axisQuestionType[i] & (QuestionType.SA | QuestionType.MA));
            }

            // ウエイト値保持ハッシュテーブル
            Hashtable wtList = new Hashtable();

            // 結果の配列のサイズ決定と見出し部分の投入
            int k = keyQsectorsCount == 0 ? 1 : keyQsectorsCount;
            resultArray = new DataWithMarking[k][,];
            // 集計軸行数の算出 (+2は無回答/非該当、+1は小計行)
            int axisRowsCount = 1;  // 全体行
            if (axesCount == 1) // 二重クロス
            {
                axisRowsCount += axisQsectorsCount[0] + 2 + 1;
            }
            else    // 三重クロス (axesCount = 2)
            {
                axisRowsCount += (1 + axisQsectorsCount[1] + 2 + 1) * (axisQsectorsCount[0] + 2) + 1;
            }
            switch (qType)
            {
                case QuestionType.SA:   // SA
                case QuestionType.MA:   // MA
                    for (int i = 0; i < k; ++i)
                    {
                        resultArray[i] = new DataWithMarking[2 + axisRowsCount, 1 + axesCount + 2 + sectorsCount + 4];
                        // 列見出し
                        int c = axesCount;  // 配列内列方向インデックス
                        resultArray[i][0, ++c] = new DataWithMarking(descs.PreWBtotalDescription, false);   // WB前全体
                        resultArray[i][0, ++c] = new DataWithMarking(descs.TotalDescription, false);    // 全体
                        for (int j = 0; j < sectorsCount; ++j)
                        {
                            resultArray[i][0, ++c] = new DataWithMarking(sectorDescriptions[j], false);
                        }
                        resultArray[i][0, ++c] = new DataWithMarking(descs.NADescription, false); // 無回答
                        resultArray[i][0, ++c] = new DataWithMarking(descs.IVDescription, false); // 非該当
                        if (hasCount)
                        {
                            resultArray[i][0, ++c] = new DataWithMarking(translation.REPORT_COUNT_AVERAGE_DENOMINATOR_KEYWORD, false);
                            resultArray[i][0, ++c] = new DataWithMarking(translation.REPORT_COUNT_AVERAGE_KEYWORD, false);
                        }
                        else
                        {
                            // TODO:Const化[QCR0000022]加重平均母数
                            // resultArray[i][0, ++c] = new DataWithMarking(GetResource.GetCommonResourceData("QCR0000022", locale));
                            resultArray[i][0, ++c] = new DataWithMarking(translation.REPORT_WEIGHT_AVERAGE_DENOMINATOR_KEYWORD, false);
                            // TODO:Const化[QCR0000023]加重平均
                            // resultArray[i][0, ++c] = new DataWithMarking(GetResource.GetCommonResourceData("QCR0000023", locale));
                            resultArray[i][0, ++c] = new DataWithMarking(translation.REPORT_WEIGHT_AVERAGE_KEYWORD, false);
                        }
                    }
                    for (int i = 0; i < sectorsCount; ++i)
                    {
                        if (!string.IsNullOrWhiteSpace(wt[i]))
                        {
                            double w = 0.0;
                            if (double.TryParse(wt[i], out w))
                            {
                                // キーは選択肢インデックス(1ベース)
                                wtList.Add((i + 1).ToString(), w);
                            }
                        }
                    }
                    break;
                case QuestionType.N:    // N
                    for (int i = 0; i < k; ++i)
                    {
                        resultArray[i] = new DataWithMarking[1 + axisRowsCount, 1 + axesCount + 11];
                        // 列見出し
                        int c = axesCount;  // 配列内列方向インデックス
                        resultArray[i][0, ++c] = new DataWithMarking(descs.PreWBtotalDescription, false); // WB前全体
                        resultArray[i][0, ++c] = new DataWithMarking(descs.TotalDescription, false);      // 全体
                        resultArray[i][0, ++c] = new DataWithMarking(descs.ParameterDescription, false);  // 統計量母数
                        resultArray[i][0, ++c] = new DataWithMarking(descs.SummaryDescription, false);    // 合計
                        resultArray[i][0, ++c] = new DataWithMarking(descs.AverageDescription, false);    // 平均
                        resultArray[i][0, ++c] = new DataWithMarking(descs.StdevDescription, false);      // 標準偏差
                        resultArray[i][0, ++c] = new DataWithMarking(descs.MinDescription, false);        // 最小値
                        resultArray[i][0, ++c] = new DataWithMarking(descs.MaxDescription, false);        // 最大値
                        resultArray[i][0, ++c] = new DataWithMarking(descs.MedianDescription, false);     // 中央値
                        resultArray[i][0, ++c] = new DataWithMarking(descs.NADescription, false);         // 無回答
                        resultArray[i][0, ++c] = new DataWithMarking(descs.IVDescription, false);         // 非該当
                    }
                    break;
            }
            for (int i = 0; i < k; ++i)
            {
                // 行見出し
                int r = qType == QuestionType.N ? 0 : 1;  // 配列内行方向インデックス
                //resultArray[i][++r, 0] = new DataWithMarking(descs.TotalAxisDescription + "(GT)", false);    // 全体(GT) // commented for header correction when no header
                resultArray[i][++r, 0] = new DataWithMarking(descs.TotalAxisDescription, false);    // 全体(GT)
                // TODO:Const化[QCR0000025]小計
                // resultArray[i][++r, 0] = new DataWithMarking(GetResource.GetCommonResourceData("QCR0000025", locale));
                // ++r;
                resultArray[i][++r, 0] = new DataWithMarking(descs.TotalAxisDescription, false);   // 全体
                // 左端見出し
                if (axesCount == 1) // 二重クロス
                {
                    // resultArray[i][r + 1, 0] = new DataWithMarking(axisQDescription[0]);
                    // 改行をカットする
                    // →TSVを介する際のエスケープ処理およびその読み込み時のアンエスケープ処理は
                    // 基本的に不要になるが、都合により残しておく
                    //resultArray[i][r + 1, 0] = new DataWithMarking(breaklineRegex.Replace(axisQDescription[0], ""), false);
                    resultArray[i][r + 1, 0] = new DataWithMarking(axisQDescription[0], false);
                }
                else    // 三重クロス (axesCount = 2)
                {
                    // resultArray[i][r + 1, 0] = new DataWithMarking(axisQDescription[0] + "\n" + MULTIPLY_LETTER.ToString() + "\n" + axisQDescription[1]);
                    // 改行をカットする
                    // →TSVを介する際のエスケープ処理およびその読み込み時のアンエスケープ処理は
                    // 基本的に不要になるが、都合により残しておく
                    //resultArray[i][r + 1, 0] = new DataWithMarking(
                    //            breaklineRegex.Replace(axisQDescription[0], "")
                    //          + " " + MULTIPLY_LETTER.ToString() + " "
                    //          + breaklineRegex.Replace(axisQDescription[1], "")
                    //          , false);
                    resultArray[i][r + 1, 0] = new DataWithMarking(
                                axisQDescription[0]
                              + " " + MULTIPLY_LETTER.ToString() + " "
                              + axisQDescription[1]
                              , false);
                }
                // 軸選択肢文
                for (int j = 0; j < axisQsectorsCount[0] + 2; ++j)
                {
                    switch (j - axisQsectorsCount[0])
                    {
                        case 0:
                            resultArray[i][++r, 1] = new DataWithMarking(descs.NADescription, false);  // 無回答
                            break;
                        case 1:
                            resultArray[i][++r, 1] = new DataWithMarking(descs.IVDescription, false);  // 非該当
                            break;
                        default:
                            // resultArray[i][++r, 1] = new DataWithMarking(axisQsectorDescription[0][j] + (axesCount == 2 ? descs.TotalAxisDescription : ""));
                            resultArray[i][++r, 1] = new DataWithMarking(axisQsectorDescription[0][j], false);
                            if (axesCount == 1)
                            {
                                resultArray[i][r, 1].SetSignificanceCharacters(Strings.LCase(SignificanceTestLetters.Character(j + 1)));
                            }
                            break;
                    }
                    if (axesCount == 2) // 三重クロス
                    {
                        // resultArray[i][r, 2] = new DataWithMarking(descs.TotalAxisDescription);
                        // TODO:Const化[QCR0000025]小計
                        // resultArray[i][++r, 2] = new DataWithMarking(GetResource.GetCommonResourceData("QCR0000025", locale));
                        ++r;
                        for (int x = 0; x < axisQsectorsCount[1]; ++x)
                        {
                            resultArray[i][++r, 2] = new DataWithMarking(axisQsectorDescription[1][x], false);
                            if (j < axisQsectorsCount[0])
                            {
                                resultArray[i][r, 2].SetSignificanceCharacters(Strings.LCase(SignificanceTestLetters.Character(x + 1)));
                            }
                        }
                        resultArray[i][++r, 2] = new DataWithMarking(descs.NADescription, false);  // 無回答
                        resultArray[i][++r, 2] = new DataWithMarking(descs.IVDescription, false);  // 非該当
                    }
                }
            }

            // 検定設定の補正
            //if (SignificanceTestOn)
            //{
            //    SignificanceTestOn = significanceTestCode == SignificanceTestCode.BetweenSectors || qType != QuestionType.N;
            //}
            if (SignificanceTestOn)
            {
                if (significanceTestCode != SignificanceTestCode.BetweenSectors)
                {
                    significanceTestCode = SignificanceTestCode.Off;
                }
                if (significanceTestLevel != null && significanceTestLevel.Length > 0)
                {
                    int maxSize = significanceTestCode == SignificanceTestCode.BetweenSectors ? 2 : 3;
                    if (significanceTestLevel.Length > maxSize) Array.Resize<double>(ref significanceTestLevel, maxSize);
                    List<double> validLevel = new List<double>();
                    for (int i = 0; i < significanceTestLevel.Length; ++i)
                    {
                        double sigLv = significanceTestLevel[i];
                        if (sigLv > 0.0 && sigLv <= 10.0)
                        {
                            if (!validLevel.Contains(sigLv)) validLevel.Add(sigLv);
                        }
                    }
                    validLevel.Sort();
                    significanceTestLevel = validLevel.ToArray();
                }
                if (significanceTestLevel == null || significanceTestLevel.Length == 0)
                {
                    SignificanceTestOn = significanceTestCode == SignificanceTestCode.Off;
                    if (SignificanceTestOn)
                    {
                        // 全体との差の検定
                        significanceTestLevel = new[] { 1.0, 5.0, 10.0 };
                    }
                }
            }
            if (SignificanceTestOn)
            {
                if (SignificanceTestLogFilePath != null)
                {
                    if (string.IsNullOrWhiteSpace(SignificanceTestLogFilePath) || string.IsNullOrWhiteSpace(qName)
                        || axisQName == null || axisQName.Count != axesCount
                        || SignificanceTestLogFilePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                    {
                        SignificanceTestLogFilePath = null;
                    }
                    else
                    {
                        for (int i = 0; i < axesCount; ++i)
                        {
                            if (string.IsNullOrWhiteSpace(axisQName[i]))
                            {
                                SignificanceTestLogFilePath = null;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                significanceTestCode = SignificanceTestCode.Off;
                SignificanceTestLogFilePath = null;
            }
            bool CountOverlap = false;
            if (significanceTestCode == SignificanceTestCode.BetweenSectors)
            {
                CountOverlap = axisQType[axesCount - 1] == QuestionType.MA;
            }

            // 集計
            double[][,] nArray = null;  // 集計値を格納する配列
            DataWithMarking[][,] dataArray = null;  // 詳細な集計値を格納する配列
            if (SignificanceTestOn)
            {
                dataArray = new DataWithMarking[k][,];
            }
            else
            {
                nArray = new double[k][,];
            }
            // N質問集計時に、最大値, 最小値, 中央値を出すための配列
            // double[][][] normalDatas = new double[k][][];
            NumericData[][][] normalDatas = new NumericData[k][][];
            int[][] lastIndex = null; // normalDatasの使用最大インデックス
            // 各行の表肩アイテムの選択肢番号からなるジャグ配列 (検定ログ出力時に使用)
            int[][] rowSectorNumbers = null;
            if (SignificanceTestLogFilePath != null)
            {
                rowSectorNumbers = new int[axisRowsCount][];
                for (int r = 0; r < 2; ++r) // 全体(GT)行と全体(小計)行
                {
                    rowSectorNumbers[r] = new[] { 0, 0 };
                }
                if (axesCount == 1) // 二重クロス
                {
                    for (int as1 = 0, r = 2; as1 < axisQsectorsCount[0]; ++as1, ++r)    // 選択肢行
                    {
                        rowSectorNumbers[r] = new[] { 0, as1 + 1 };
                    }
                    for (int r = 2 + axisQsectorsCount[0]; r < axisRowsCount; ++r)  // 無回答行と非該当行
                    {
                        rowSectorNumbers[r] = new[] { 0, 0 };
                    }
                }
                else    // 三重クロス
                {
                    for (int as1 = 0, r = 2; as1 < axisQsectorsCount[0]; ++as1) // 表肩選択肢行
                    {
                        for (int j = 0; j < 2; ++j, ++r)    // 表側全体行と表側小計行
                        {
                            rowSectorNumbers[r] = new int[] { as1 + 1, 0 };
                        }
                        for (int as2 = 0; as2 < axisQsectorsCount[1]; ++as2, ++r) // 表側選択肢行
                        {
                            rowSectorNumbers[r] = new int[] { as1 + 1, as2 + 1 };
                        }
                        for (int y = 0; y < 2; ++y, ++r)    // 表側無回答行と表側非該当行
                        {
                            rowSectorNumbers[r] = new int[] { as1 + 1, 0 };
                        }
                    }
                    for (int r = 2 + (2 + axisQsectorsCount[1] + 2) * axisQsectorsCount[0]; r < axisRowsCount; ++r) // 表肩無回答行と表肩非該当行
                    {
                        rowSectorNumbers[r] = new[] { 0, 0 };
                    }
                }
            }
            // 集計値を格納する配列のサイズ定義
            if (qType != QuestionType.N)    // SA/MA
            {
                if (SignificanceTestOn)
                {
                    for (int i = 0; i < k; ++i)
                    {
                        dataArray[i] = new DataWithMarking[axisRowsCount, 2 + sectorsCount + 3];
                        if (significanceTestCode == SignificanceTestCode.BetweenSectors)    // 項目間検定
                        {
                            for (int r = 0; r < 2; ++r) // 全体(GT)行と全体(小計)行
                            {
                                dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);  // WB前全体列
                                dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell);   // 全体列
                                for (int s = 0; s < sectorsCount; ++s)
                                {
                                    dataArray[i][r, 2 + s] = new DataWithMarking(CellType.NAIVCell);   // 選択肢列
                                }
                                for (int c = 0; c < 2; ++c)
                                {
                                    // 無回答列と非該当列
                                    dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell | CellType.TotalCell);
                                }
                                // 加重平均母数列
                                dataArray[i][r, 2 + sectorsCount + 3 - 1] = new DataWithMarking(CellType.PopulationCell);
                            }
                            if (axesCount == 1) // 二重クロス
                            {
                                for (int as1 = 0, r = 2; as1 < axisQsectorsCount[0]; ++as1, ++r)
                                {
                                    // WB前全体列
                                    dataArray[i][r, 0] = new DataWithMarking(
                                            as1 + 1, axisQsectorsCount[0], CellType.PreWBTotalCell, CountOverlap);
                                    // 全体列
                                    dataArray[i][r, 1] = new DataWithMarking(
                                            as1 + 1, axisQsectorsCount[0], CellType.TotalCell, CountOverlap);
                                    // 選択肢列
                                    for (int s = 0; s < sectorsCount; ++s)
                                    {
                                        dataArray[i][r, 2 + s] = new DataWithMarking(
                                                as1 + 1, axisQsectorsCount[0], CellType.DataCell, CountOverlap);
                                    }
                                    // 無回答列と非該当列
                                    for (int c = 0; c < 2; ++c)
                                    {
                                        dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                    }
                                    // 加重平均母数列
                                    dataArray[i][r, 2 + sectorsCount + 3 - 1] = new DataWithMarking(
                                                as1 + 1, axisQsectorsCount[0], CellType.PopulationCell, CountOverlap);
                                }
                                // 無回答行と非該当行
                                for (int r = 2 + axisQsectorsCount[0]; r < axisRowsCount; ++r)
                                {
                                    //for (int c = 0; c < dataArray[i].GetLength(1); ++c)
                                    //{
                                    //    // 加重平均母数列ではTotalCellを立てる
                                    //    CellType tmp = c == dataArray[i].GetUpperBound(1)
                                    //                    ? CellType.PopulationCell : (CellType)0;
                                    //    dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell | tmp);
                                    //}
                                    // WB前全体列
                                    dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);
                                    // 全体列
                                    dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell | CellType.NAIVCell);
                                    for (int c = 2; c < dataArray[i].GetUpperBound(1); ++c)
                                    {
                                        dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell);
                                    }
                                    // 加重平均母数列
                                    dataArray[i][r, dataArray[i].GetUpperBound(1)] = new DataWithMarking(CellType.PopulationCell | CellType.NAIVCell);
                                }
                            }
                            else    // 三重クロス
                            {
                                for (int as1 = 0, r = 2; as1 < axisQsectorsCount[0]; ++as1)
                                {
                                    // 表側全体行と表側小計行
                                    for (int j = 0; j < 2; ++j, ++r)
                                    {
                                        dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);  // WB前全体列
                                        dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell);   // 全体列
                                        for (int s = 0; s < sectorsCount; ++s)
                                        {
                                            dataArray[i][r, 2 + s] = new DataWithMarking(CellType.NAIVCell);   // 選択肢列
                                        }
                                        for (int c = 0; c < 2; ++c)
                                        {
                                            // 無回答列と非該当列
                                            dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell | CellType.TotalCell);
                                        }
                                        // 加重平均母数列
                                        dataArray[i][r, 2 + sectorsCount + 3 - 1] = new DataWithMarking(CellType.PopulationCell);
                                    }
                                    // 表側選択肢行
                                    for (int as2 = 0; as2 < axisQsectorsCount[1]; ++as2, ++r)
                                    {
                                        // WB前全体列
                                        dataArray[i][r, 0] = new DataWithMarking(
                                                as2 + 1, axisQsectorsCount[1], CellType.PreWBTotalCell, CountOverlap);
                                        // 全体列
                                        dataArray[i][r, 1] = new DataWithMarking(
                                                as2 + 1, axisQsectorsCount[1], CellType.TotalCell, CountOverlap);
                                        // 選択肢列
                                        for (int s = 0; s < sectorsCount; ++s)
                                        {
                                            dataArray[i][r, 2 + s] = new DataWithMarking(
                                                    as2 + 1, axisQsectorsCount[1], CellType.DataCell, CountOverlap);
                                        }
                                        // 無回答列と非該当列
                                        for (int c = 0; c < 2; ++c)
                                        {
                                            dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                        }
                                        // 加重平均母数列
                                        dataArray[i][r, 2 + sectorsCount + 3 - 1] = new DataWithMarking(
                                                    as2 + 1, axisQsectorsCount[1], CellType.PopulationCell, CountOverlap);
                                    }
                                    // 表側無回答行と表側非該当行
                                    for (int y = 0; y < 2; ++y, ++r)
                                    {
                                        //for (int c = 0; c < dataArray[i].GetLength(1); ++c)
                                        //{
                                        //    // 加重平均母数列ではTotalCellを立てる
                                        //    CellType tmp = c == dataArray[i].GetUpperBound(1)
                                        //                    ? CellType.PopulationCell : (CellType)0;
                                        //    dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell | tmp);
                                        //}
                                        // WB前全体列
                                        dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);
                                        // 全体列
                                        dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell | CellType.NAIVCell);
                                        for (int c = 2; c < dataArray[i].GetUpperBound(1); ++c)
                                        {
                                            dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell);
                                        }
                                        // 加重平均母数列
                                        dataArray[i][r, dataArray[i].GetUpperBound(1)] = new DataWithMarking(CellType.PopulationCell | CellType.NAIVCell);
                                    }
                                }
                                // 表肩無回答行と表肩非該当行
                                for (int r = 2 + (2 + axisQsectorsCount[1] + 2) * axisQsectorsCount[0]; r < axisRowsCount; ++r)
                                {
                                    //for (int c = 0; c < dataArray[i].GetLength(1); ++c)
                                    //{
                                    //    // 加重平均母数列ではTotalCellを立てる
                                    //    CellType tmp = c == dataArray[i].GetUpperBound(1)
                                    //                    ? CellType.PopulationCell : (CellType)0;
                                    //    dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell | tmp);
                                    //}
                                    // WB前全体列
                                    dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);
                                    // 全体列
                                    dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell | CellType.NAIVCell);
                                    for (int c = 2; c < dataArray[i].GetUpperBound(1); ++c)
                                    {
                                        dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell);
                                    }
                                    // 加重平均母数列
                                    dataArray[i][r, dataArray[i].GetUpperBound(1)] = new DataWithMarking(CellType.PopulationCell | CellType.NAIVCell);
                                }
                            }
                        }
                        else    // 全体との差の検定
                        {
                            for (int r = 0; r < 2; ++r) // 全体(GT)行と全体(小計)行
                            {
                                dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);  // WB前全体列
                                dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell);   // 全体列
                                //CellType ct = CellType.TotalCell;
                                //if (r == 1 && markingTotal == GlobalTabulation.MarkingTotal.Total)
                                //{
                                //    ct |= CellType.DataCell;
                                //}
                                CellType ct = CellType.SubTotalCell;
                                if (r == 1 && markingTotal == GlobalTabulation.MarkingTotal.Total)
                                {
                                    ct = CellType.DataCell;
                                }
                                for (int s = 0; s < sectorsCount; ++s)
                                {
                                    dataArray[i][r, 2 + s] = new DataWithMarking(ct);   // 選択肢列
                                }
                                //for (int c = 0; c < 3; ++c)
                                //{
                                //    // 無回答列と非該当列と加重平均母数列
                                //    dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                //}
                                for (int c = 0; c < 2; ++c)
                                {
                                    // 無回答列と非該当列
                                    dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                }
                                // 加重平均母数列
                                // if (ct == CellType.DataCell) ct = CellType.PopulationCell;
                                dataArray[i][r, 2 + sectorsCount + 2] = new DataWithMarking(ct | CellType.PopulationCell);
                            }
                            if (axesCount == 1) // 二重クロス
                            {
                                for (int as1 = 0, r = 2; as1 < axisQsectorsCount[0]; ++as1, ++r)
                                {
                                    // WB前全体列
                                    dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);
                                    // 全体列
                                    dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell);
                                    // 選択肢列
                                    for (int s = 0; s < sectorsCount; ++s)
                                    {
                                        dataArray[i][r, 2 + s] = new DataWithMarking(CellType.DataCell);
                                    }
                                    //// 無回答列と非該当列と加重平均母数列
                                    //for (int c = 0; c < 3; ++c)
                                    //{
                                    //    dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                    //}
                                    // 無回答列と非該当列
                                    for (int c = 0; c < 2; ++c)
                                    {
                                        dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                    }
                                    // 加重平均母数列
                                    dataArray[i][r, 2 + sectorsCount + 2] = new DataWithMarking(CellType.PopulationCell);
                                }
                                // 無回答行と非該当行
                                for (int r = 2 + axisQsectorsCount[0]; r < axisRowsCount; ++r)
                                {
                                    //for (int c = 0; c < dataArray[i].GetLength(1); ++c)
                                    //{
                                    //    // 加重平均母数列ではTotalCellを立てる
                                    //    CellType tmp = c == dataArray[i].GetUpperBound(1)
                                    //                    ? CellType.PopulationCell : (CellType)0;
                                    //    dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell | tmp);
                                    //}
                                    // WB前全体列
                                    dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);
                                    // 全体列
                                    dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell | CellType.NAIVCell);
                                    for (int c = 2; c < dataArray[i].GetUpperBound(1); ++c)
                                    {
                                        dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell);
                                    }
                                    // 加重平均母数列
                                    dataArray[i][r, dataArray[i].GetUpperBound(1)] = new DataWithMarking(CellType.PopulationCell | CellType.NAIVCell);
                                }
                            }
                            else    // 三重クロス
                            {
                                for (int as1 = 0, r = 2; as1 < axisQsectorsCount[0]; ++as1)
                                {
                                    // 表側全体行と表側小計行
                                    for (int j = 0; j < 2; ++j, ++r)
                                    {
                                        dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);  // WB前全体列
                                        dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell);   // 全体列
                                        //CellType ct = CellType.TotalCell;
                                        //if (j == 1 && markingTotal == GlobalTabulation.MarkingTotal.Total)
                                        //{
                                        //    ct |= CellType.DataCell;
                                        //}
                                        CellType ct = markingTotal == GlobalTabulation.MarkingTotal.Total ? CellType.DataCell : CellType.SubTotalCell;
                                        for (int s = 0; s < sectorsCount; ++s)
                                        {
                                            dataArray[i][r, 2 + s] = new DataWithMarking(ct);   // 選択肢列
                                        }
                                        //for (int c = 0; c < 3; ++c)
                                        //{
                                        //    // 無回答列と非該当列と加重平均母数列
                                        //    dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell | CellType.TotalCell);
                                        //}
                                        for (int c = 0; c < 2; ++c)
                                        {
                                            // 無回答列と非該当列
                                            dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                        }
                                        // 加重平均母数列
                                        // if (ct == CellType.DataCell) ct = CellType.PopulationCell;
                                        dataArray[i][r, 2 + sectorsCount + 2] = new DataWithMarking(ct | CellType.PopulationCell);
                                    }
                                    // 表側選択肢行
                                    for (int as2 = 0; as2 < axisQsectorsCount[1]; ++as2, ++r)
                                    {
                                        // WB前全体列
                                        dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);
                                        // 全体列
                                        dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell);
                                        // 選択肢列
                                        for (int s = 0; s < sectorsCount; ++s)
                                        {
                                            dataArray[i][r, 2 + s] = new DataWithMarking(CellType.DataCell);
                                        }
                                        //// 無回答列と非該当列と加重平均母数列
                                        //for (int c = 0; c < 3; ++c)
                                        //{
                                        //    dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                        //}
                                        // 無回答列と非該当列
                                        for (int c = 0; c < 2; ++c)
                                        {
                                            dataArray[i][r, 2 + sectorsCount + c] = new DataWithMarking(CellType.NAIVCell);
                                        }
                                        // 加重平均母数列
                                        dataArray[i][r, 2 + sectorsCount + 2] = new DataWithMarking(CellType.PopulationCell);
                                    }
                                    // 表側無回答行と表側非該当行
                                    for (int y = 0; y < 2; ++y, ++r)
                                    {
                                        //for (int c = 0; c < dataArray[i].GetLength(1); ++c)
                                        //{
                                        //    // 加重平均母数列ではTotalCellを立てる
                                        //    CellType tmp = c == dataArray[i].GetUpperBound(1)
                                        //                    ? CellType.PopulationCell : (CellType)0;
                                        //    dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell | tmp);
                                        //}
                                        // WB前全体列
                                        dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);
                                        // 全体列
                                        dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell | CellType.NAIVCell);
                                        for (int c = 2; c < dataArray[i].GetUpperBound(1); ++c)
                                        {
                                            dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell);
                                        }
                                        // 加重平均母数列
                                        dataArray[i][r, dataArray[i].GetUpperBound(1)] = new DataWithMarking(CellType.PopulationCell | CellType.NAIVCell);
                                    }
                                }
                                // 表肩無回答行と表肩非該当行
                                for (int r = 2 + (2 + axisQsectorsCount[1] + 2) * axisQsectorsCount[0]; r < axisRowsCount; ++r)
                                {
                                    //for (int c = 0; c < dataArray[i].GetLength(1); ++c)
                                    //{
                                    //    // 加重平均母数列ではTotalCellを立てる
                                    //    CellType tmp = c == dataArray[i].GetUpperBound(1)
                                    //                    ? CellType.PopulationCell : (CellType)0;
                                    //    dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell | tmp);
                                    //}
                                    // WB前全体列
                                    dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);
                                    // 全体列
                                    dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell | CellType.NAIVCell);
                                    for (int c = 2; c < dataArray[i].GetUpperBound(1); ++c)
                                    {
                                        dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell);
                                    }
                                    // 加重平均母数列
                                    dataArray[i][r, dataArray[i].GetUpperBound(1)] = new DataWithMarking(CellType.PopulationCell | CellType.NAIVCell);
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < k; ++i)
                    {
                        nArray[i] = new double[axisRowsCount, 2 + sectorsCount + 3];
                    }
                }
            }
            else    // N
            {
                lastIndex = new int[k][];
                for (int i = 0; i < k; ++i)
                {
                    // normalDatas[i] = new double[axisRowsCount][];
                    normalDatas[i] = new NumericData[axisRowsCount][];
                    lastIndex[i] = new int[axisRowsCount];
                    for (int j = 0; j < axisRowsCount; ++j)
                    {
                        // normalDatas[i][j] = new double[data.Count];
                        normalDatas[i][j] = new NumericData[data.Count];
                        lastIndex[i][j] = -1;
                    }
                    if (SignificanceTestOn)
                    {
                        dataArray[i] = new DataWithMarking[axisRowsCount, 5];
                        // 全体(GT)行と全体(小計)行
                        for (int r = 0; r < 2; ++r)
                        {
                            dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);  // WB前全体
                            dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell);   // 全体
                            // dataArray[i][r, 2] = new DataWithMarking(CellType.PopulationCell);   // 統計量母数
                            CellType ct = CellType.PopulationCell;
                            if (significanceTestCode == SignificanceTestCode.Off)   // 全体との差の検定
                            {
                                if (r == 0 || markingTotal == GlobalTabulation.MarkingTotal.Subtotal)
                                {
                                    ct |= CellType.SubTotalCell;
                                }
                            }
                            dataArray[i][r, 2] = new DataWithMarking(ct);   // 統計量母数
                            // 無回答と非該当
                            for (int c = 3; c < 5; ++c)
                            {
                                dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell);
                            }
                        }
                        if (axesCount == 1) // 二重クロス
                        {
                            // 表側選択肢行
                            // for (int as1 = 0, r = 2; as1 < axisQsectorsCount[0]; ++as1, ++r)
                            for (int as1 = 0, r = 2; r < axisRowsCount; ++as1, ++r)
                            {
                                bool isNAIVRow = r >= 2 + axisQsectorsCount[0];
                                if (significanceTestCode == SignificanceTestCode.BetweenSectors && !isNAIVRow)
                                {
                                    // WB前全体
                                    dataArray[i][r, 0] = new DataWithMarking(
                                            as1 + 1, axisQsectorsCount[0], CellType.PreWBTotalCell, CountOverlap);
                                    // 全体
                                    dataArray[i][r, 1] = new DataWithMarking(
                                            as1 + 1, axisQsectorsCount[0], CellType.TotalCell, CountOverlap);
                                    // 統計量母数
                                    dataArray[i][r, 2] = new DataWithMarking(
                                            as1 + 1, axisQsectorsCount[0], CellType.PopulationCell, CountOverlap);
                                }
                                else
                                {
                                    CellType tmp = isNAIVRow ? CellType.NAIVCell : (CellType)0;
                                    dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell | tmp);
                                    dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell | tmp);
                                    dataArray[i][r, 2] = new DataWithMarking(CellType.PopulationCell);
                                }
                                // 無回答と非該当
                                for (int c = 3; c < 5; ++c)
                                {
                                    dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell);
                                }
                            }
                            //// 無回答行と非該当行
                            //for (int r = 2 + axisQsectorsCount[0]; r < axisRowsCount; ++r)
                            //{
                            //    for (int c = 0; c < dataArray[i].GetLength(1); ++c)
                            //    {
                            //        dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell);
                            //    }
                            //}
                        }
                        else    // 三重クロス
                        {
                            // 表肩選択肢行
                            for (int as1 = 0, r = 2; as1 < axisQsectorsCount[0]; ++as1)
                            {
                                // 表側全体行と表側小計行
                                for (int j = 0; j < 2; ++j, ++r)
                                {
                                    // WB前全体
                                    dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);
                                    // 全体
                                    dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell);
                                    // 統計量母数
                                    // dataArray[i][r, 2] = new DataWithMarking(CellType.PopulationCell);
                                    CellType ct = CellType.PopulationCell;
                                    if (significanceTestCode == SignificanceTestCode.Off)   // 全体との差の検定
                                    {
                                        if (markingTotal == GlobalTabulation.MarkingTotal.Subtotal)
                                        {
                                            ct |= CellType.SubTotalCell;
                                        }
                                    }
                                    dataArray[i][r, 2] = new DataWithMarking(ct);
                                    // 無回答と非該当
                                    for (int c = 3; c < 5; ++c)
                                    {
                                        dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell | CellType.TotalCell);
                                    }
                                }
                                // 表側選択肢行
                                for (int as2 = 0; as2 < axisQsectorsCount[1]; ++as2, ++r)
                                {
                                    if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                                    {
                                        // WB前全体
                                        dataArray[i][r, 0] = new DataWithMarking(
                                                as2 + 1, axisQsectorsCount[1], CellType.PreWBTotalCell, CountOverlap);
                                        // 全体
                                        dataArray[i][r, 1] = new DataWithMarking(
                                                as2 + 1, axisQsectorsCount[1], CellType.TotalCell, CountOverlap);
                                        // 統計量母数
                                        dataArray[i][r, 2] = new DataWithMarking(
                                                as2 + 1, axisQsectorsCount[1], CellType.PopulationCell, CountOverlap);
                                    }
                                    else
                                    {
                                        dataArray[i][r, 0] = new DataWithMarking(CellType.PreWBTotalCell);
                                        dataArray[i][r, 1] = new DataWithMarking(CellType.TotalCell);
                                        dataArray[i][r, 2] = new DataWithMarking(CellType.PopulationCell);
                                    }
                                    // 無回答と非該当
                                    for (int c = 3; c < 5; ++c)
                                    {
                                        dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell);
                                    }
                                }
                                // 表側無回答行と表側非該当行
                                for (int y = 0; y < 2; ++y, ++r)
                                {
                                    for (int c = 0; c < dataArray[i].GetLength(1); ++c)
                                    {
                                        //CellType tmp = c == 0 ? CellType.PreWBTotalCell
                                        //                      : c == 1 ? CellType.TotalCell
                                        //                               : c == 2 ? CellType.PopulationCell
                                        //                                        : (CellType)0;
                                        //dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell | tmp);
                                        CellType tmp = c == 2 ? CellType.PopulationCell
                                                              : (c == 0 ? CellType.PreWBTotalCell
                                                                        : c == 1 ? CellType.TotalCell
                                                                        : (CellType)0)
                                                                | CellType.NAIVCell;
                                        dataArray[i][r, c] = new DataWithMarking(tmp);
                                    }
                                }
                            }
                            // 表肩無回答行と表肩非該当行
                            for (int r = 2 + (2 + axisQsectorsCount[1] + 2) * axisQsectorsCount[0]; r < axisRowsCount; ++r)
                            {
                                for (int c = 0; c < dataArray[i].GetLength(1); ++c)
                                {
                                    //CellType tmp = c == 0 ? CellType.PreWBTotalCell
                                    //                      : c == 1 ? CellType.TotalCell
                                    //                               : c == 2 ? CellType.PopulationCell
                                    //                                        : (CellType)0;
                                    //dataArray[i][r, c] = new DataWithMarking(CellType.NAIVCell | tmp);
                                    CellType tmp = c == 2 ? CellType.PopulationCell
                                                          : (c == 0 ? CellType.PreWBTotalCell
                                                                    : c == 1 ? CellType.TotalCell
                                                                    : (CellType)0)
                                                            | CellType.NAIVCell;
                                    dataArray[i][r, c] = new DataWithMarking(tmp);
                                }
                            }
                        }
                    }
                    else
                    {
                        nArray[i] = new double[axisRowsCount, 7];
                    }
                }
            }

            // オーバーラップ分集計用リスト
            List<DataWithMarking>[] tmpListArray = null;
            List<List<DataWithMarking>>[] tmpListListArray = null;
            List<double>[] tmpNumListArray = null;
            // double[][,] tmpNumArrayArray = null;
            if (CountOverlap)
            {
                int secCnt = axisQsectorsCount[axesCount - 1];
                int cnt = axesCount == 1 ? 1 : axisQsectorsCount[0];
                if (qType != QuestionType.N)
                {
                    tmpListListArray = new List<List<DataWithMarking>>[cnt];
                    // tmpNumArrayArray = new double[cnt][,];
                    if (wtList.Count > 0)
                    {
                        tmpListArray = new List<DataWithMarking>[cnt];  // 加重平均母数用
                        tmpNumListArray = new List<double>[cnt];
                    }
                    for (int i = 0; i < cnt; ++i)
                    {
                        tmpListListArray[i] = new List<List<DataWithMarking>>();
                        // tmpNumArrayArray[i] = new double[secCnt, sectorsCount];
                        tmpListListArray[i].Add(new List<DataWithMarking>());   // 全体用
                        for (int j = 0; j < sectorsCount; ++j)
                        {
                            tmpListListArray[i].Add(new List<DataWithMarking>());
                        }
                    }
                }
                else
                {
                    tmpListArray = new List<DataWithMarking>[cnt];  // 統計量母数用
                    tmpNumListArray = new List<double>[cnt];
                }
                if (tmpListArray != null)
                {
                    for (int i = 0; i < cnt; ++i)
                    {
                        tmpListArray[i] = new List<DataWithMarking>();  //  統計量母数用
                        tmpNumListArray[i] = new List<double>();
                    }
                }
            }

            CutNA &= !TabulateFullQuantity;
            // データ走査
            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i].IsDeleted || !FilteringFlag[i]) continue;
                // WB
                double wb = (weightback[i] as NData).Value;
                // 結果配列一段階インデックス
                List<int> keyIdx = new List<int>();
                if (keyQsectorsCount == 0)  // 分類アイテムなし
                {
                    keyIdx.Add(0);
                }
                else    // 分類アイテムあり
                {
                    if (keyData[i].DataType == DataType.NormalData)
                    {
                        if (keyQType == QuestionType.SA)    // SA
                        {
                            int n = (keyData[i] as SAData).Value;
                            if (n >= 1 && n <= keyQsectorsCount)
                            {
                                keyIdx.Add(n - 1);
                            }
                        }
                        else    // MA
                        {
                            /*
                            for (int j = 0; j < keyQsectorsCount; ++j)
                            {
                                int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                if (((keyData[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                {
                                    int n = j + 1;
                                    keyIdx.Add(n - 1);
                                }
                            }
                            */
                            int[] sectors = (keyData[i] as MAData).SectorsArray;
                            if (sectors != null)
                            {
                                for (int j = 0; j < sectors.Length; ++j)
                                {
                                    int n = sectors[j];
                                    if (n <= keyQsectorsCount)
                                    {
                                        keyIdx.Add(n - 1);
                                    }
                                }
                            }
                        }
                    }
                }

                if (tmpListArray != null)
                {
                    for (int j = 0; j < tmpListArray.Length; ++j)
                    {
                        tmpListArray[j].Clear();
                        tmpNumListArray[j].Clear();
                    }
                }
                if (tmpListListArray != null)
                {
                    for (int j = 0; j < tmpListListArray.Length; ++j)
                    {
                        for (int s = 0; s <= sectorsCount; ++s)
                        {
                            tmpListListArray[j][s].Clear();
                        }
                        // OperateArray.Initialize<double>(ref tmpNumArrayArray[j]);
                    }
                }

                // 行インデックス
                List<int> rowIdx = new List<int>();
                // rowIdxと対で、その行が示す軸選択肢情報を保持するリスト (オーバーラップ算出時のみ使用)
                List<int[]> rowIdxSectorInfo = null;
                if (CountOverlap) rowIdxSectorInfo = new List<int[]>();

                rowIdx.Add(0);  // 全体
                if (CountOverlap) rowIdxSectorInfo.Add(new[] { 0 });

                // 軸アイテムの回答のインデックスを保持する配列
                List<int>[] selectedIndex = new List<int>[axesCount];
                for (int x = 0; x < axesCount; ++x)
                {
                    selectedIndex[x] = new List<int>();
                    switch (axisData[x][i].DataType)
                    {
                        case DataType.NormalData:   // 通常データ
                            switch (axisQType[x])
                            {
                                case QuestionType.SA:
                                    {
                                        int n = (axisData[x][i] as SAData).Value;
                                        if (n >= 1 && n <= axisQsectorsCount[x])
                                        {
                                            selectedIndex[x].Add(n);    // 該当する選択肢
                                            selectedIndex[x].Add(0);    // 小計
                                        }
                                        else
                                        {
                                            selectedIndex[x].Add(axisQsectorsCount[x] + 1); // 無回答扱い
                                            if (SubtotalIncludeAxisNA) selectedIndex[x].Add(0); // 小計
                                        }
                                        break;
                                    }
                                case QuestionType.MA:
                                    {
                                        /*
                                        for (int j = 0; j < axisQsectorsCount[x]; ++j)
                                        {
                                            int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            if (((axisData[x][i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                            {
                                                int n = j + 1;
                                                selectedIndex[x].Add(n);    // 該当する選択肢
                                            }
                                        }
                                        */
                                        int[] sectors = (axisData[x][i] as MAData).SectorsArray;
                                        bool isNA = true;
                                        if (sectors != null)
                                        {
                                            for (int j = 0; j < sectors.Length; ++j)
                                            {
                                                int n = sectors[j];
                                                if (n <= axisQsectorsCount[x])
                                                {
                                                    selectedIndex[x].Add(n);
                                                    isNA = false;
                                                }
                                            }
                                        }
                                        if (isNA) selectedIndex[x].Add(axisQsectorsCount[x] + 1);   // 無回答扱い
                                        if (!isNA || SubtotalIncludeAxisNA) selectedIndex[x].Add(0);    // 小計
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        case DataType.NAData:   // 無回答
                            selectedIndex[x].Add(axisQsectorsCount[x] + 1);
                            if (SubtotalIncludeAxisNA) selectedIndex[x].Add(0); // 小計
                            break;
                        case DataType.IVData:   // 非該当
                            selectedIndex[x].Add(axisQsectorsCount[x] + (IVtoNA ? 1 : 2));
                            bool f = false;
                            if (IVtoNA)
                            {
                                f = SubtotalIncludeAxisNA;
                            }
                            else
                            {
                                f = SubtotalIncludeAxisIV;
                            }
                            if (f) selectedIndex[x].Add(0); // 小計
                            break;
                        default:
                            break;
                    }
                    selectedIndex[x].Sort();
                }

                if (axesCount == 1) //二重クロス
                {
                    for (int x = 0; x < selectedIndex[0].Count; ++x)
                    {
                        rowIdx.Add(selectedIndex[0][x] + 1);
                        if (CountOverlap) rowIdxSectorInfo.Add(new[] { selectedIndex[0][x] });
                    }
                }
                else    // 三重クロス (axesCount == 2)
                {
                    // 二重クロスの選択肢全体行間
                    int n = 1 + 1 + axisQsectorsCount[1] + 2;
                    for (int x = 0; x < selectedIndex[0].Count; ++x)
                    {
                        if (selectedIndex[0][x] == 0)
                        {
                            if (selectedIndex[1].Contains(0))
                            {
                                rowIdx.Add(1);
                                if (CountOverlap) rowIdxSectorInfo.Add(new[] { 0 });
                            }
                            continue;
                        }
                        // 二重クロスの選択肢の先頭行(三重クロスの全体行)インデックス
                        int topIdx = n * (selectedIndex[0][x] - 1) + 2;
                        rowIdx.Add(topIdx);
                        if (CountOverlap) rowIdxSectorInfo.Add(new[] { 0 });
                        for (int y = 0; y < selectedIndex[1].Count; ++y)
                        {
                            rowIdx.Add(topIdx + selectedIndex[1][y] + 1);
                            if (CountOverlap) rowIdxSectorInfo.Add(new[] { selectedIndex[0][x], selectedIndex[1][y] });
                        }
                    }
                }

                // 列インデックス
                List<int> columnIdx = new List<int>();

                // if (data[i].DataType != DataType.IVData || TabulateFullQuantity)
                bool IncrementTotal = true;
                switch (data[i].DataType)
                {
                    case DataType.NAData:
                        IncrementTotal = !CutNA;
                        break;
                    case DataType.IVData:
                        IncrementTotal = TabulateFullQuantity;
                        break;
                }
                int naIdx = (SignificanceTestOn ? dataArray[0].GetUpperBound(1) : nArray[0].GetUpperBound(1))
                                - (qType == QuestionType.N ? 1 : 2); // 無回答のインデックス
                int validcasesIdx = (SignificanceTestOn ? dataArray[0].GetUpperBound(1) : nArray[0].GetUpperBound(1));
                double v = 1.0;
                double v1 = 0.0;

#if N0_INCLUDE_NA_IV
                bool AddedTotalOverlap = qType == QuestionType.N || !CountOverlap;
#endif
                switch (data[i].DataType)
                {
                    case DataType.NAData:   // 無回答
                        columnIdx.Add(naIdx);
                        if (!hasLower)
                        {
                            
                            if (SignificanceTestOn)
                            {
                                for (int j = 0; j < keyIdx.Count; ++j)
                                {
                                    for (int x = 0; x < rowIdx.Count; ++x)
                                    {
                                        dataArray[keyIdx[j]][rowIdx[x], naIdx + 2].AddDetail(v1, wb);
                                        if (CountOverlap)
                                        {
                                            int S = 0, s = 0;
                                            if (axesCount == 1)
                                            {
                                                S = 1;
                                                s = rowIdxSectorInfo[x][0];
                                                if (s > axisQsectorsCount[0]) s = 0;
                                            }
                                            else
                                            {
                                                S = rowIdxSectorInfo[x][0];
                                                if (S > 0 && S <= axisQsectorsCount[0])
                                                {
                                                    s = rowIdxSectorInfo[x][1];
                                                    if (s > axisQsectorsCount[1]) s = 0;
                                                }
                                            }
                                            if (s > 0)
                                            {
                                                for (int t = 0; t < tmpListArray[S - 1].Count; ++t)
                                                {
                                                    tmpListArray[S - 1][t].AddOverlapDetail(s, tmpNumListArray[S - 1][t], v1, wb);
                                                }
                                                tmpListArray[S - 1].Add(dataArray[keyIdx[j]][rowIdx[x], naIdx + 2]);
                                                tmpNumListArray[S - 1].Add(v1);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                columnIdx.Add(validcasesIdx);
                            }
                        }
                        break;
                    case DataType.IVData:   // 非該当
                        columnIdx.Add(naIdx + (TabulateFullQuantity && IVtoNA ? 0 : 1));
                        break;
                    case DataType.NormalData:   // 通常データ
                        switch (qType)
                        {
                            case QuestionType.SA:
                                {
                                    int n = (data[i] as SAData).Value;
                                    if (n >= 1 && n <= sectorsCount)
                                    {
                                        columnIdx.Add(1 + n);   // 該当する選択肢
                                        bool weighted = wtList.ContainsKey(n.ToString());
                                        // if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                                        if (SignificanceTestOn)
                                        {
                                            double w = 0.0;
                                            if (weighted) w = (double)wtList[n.ToString()];
                                            for (int j = 0; j < keyIdx.Count; ++j)
                                            {
                                                for (int x = 0; x < rowIdx.Count; ++x)
                                                {
                                                    if (weighted) dataArray[keyIdx[j]][rowIdx[x], naIdx + 2].AddDetail(w, wb);
                                                    if (CountOverlap)
                                                    {
                                                        int S = 0, s = 0;
                                                        if (axesCount == 1)
                                                        {
                                                            S = 1;
                                                            s = rowIdxSectorInfo[x][0];
                                                            if (s > axisQsectorsCount[0]) s = 0;
                                                        }
                                                        else
                                                        {
                                                            S = rowIdxSectorInfo[x][0];
                                                            if (S > 0 && S <= axisQsectorsCount[0])
                                                            {
                                                                s = rowIdxSectorInfo[x][1];
                                                                if (s > axisQsectorsCount[1]) s = 0;
                                                            }
                                                        }
                                                        if (s > 0)
                                                        {
                                                            // tmpNumArrayArray[S - 1][s - 1, n - 1] = 1.0;
                                                            for (int t = 0; t < tmpListListArray[S - 1][0].Count; ++t)
                                                            {
                                                                tmpListListArray[S - 1][0][t].AddOverlapDetail(s, 1.0, 1.0, wb);
                                                            }
                                                            tmpListListArray[S - 1][0].Add(dataArray[keyIdx[j]][rowIdx[x], 1]);
                                                            for (int t = 0; t < tmpListListArray[S - 1][n].Count; ++t)
                                                            {
                                                                tmpListListArray[S - 1][n][t].AddOverlapDetail(s, 1.0, 1.0, wb);
                                                            }
                                                            tmpListListArray[S - 1][n].Add(dataArray[keyIdx[j]][rowIdx[x], 1 + n]);
                                                            if (weighted)
                                                            {
                                                                for (int t = 0; t < tmpListArray[S - 1].Count; ++t)
                                                                {
                                                                    tmpListArray[S - 1][t].AddOverlapDetail(s, tmpNumListArray[S - 1][t], w, wb);
                                                                }
                                                                tmpListArray[S - 1].Add(dataArray[keyIdx[j]][rowIdx[x], naIdx + 2]);
                                                                tmpNumListArray[S - 1].Add(w);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (weighted) columnIdx.Add(naIdx + 2);   // 統計量母数
                                        }
#if N0_INCLUDE_NA_IV
                                        AddedTotalOverlap = true;
#endif
                                    }
                                    else
                                    {
                                        columnIdx.Add(naIdx);   // 無回答扱い
                                        IncrementTotal = !CutNA;
                                    }
                                    break;
                                }
                            case QuestionType.MA:
                                {
                                    /*
                                    for (int j = 0; j < sectorsCount; ++j)
                                    {
                                        int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                        int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                        if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                        {
                                            n = j + 1;
                                            columnIdx.Add(1 + n);
                                        }
                                    }
                                    */
                                    int[] sectors = (data[i] as MAData).SectorsArray;
                                    bool isNA = true;
                                    bool isEffectiveSample = false;
                                    double w = 0.0;
                                    if (sectors != null)
                                    {
                                        for (int m = 0; m < sectors.Length; ++m)
                                        {
                                            int n = sectors[m];
                                            if (n <= sectorsCount)
                                            {
                                                bool weighted = wtList.ContainsKey(n.ToString());
                                                columnIdx.Add(1 + n);   // 該当する選択肢
                                                // if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                                                if (SignificanceTestOn)
                                                {
                                                    if (CountOverlap)
                                                    {
                                                        for (int j = 0; j < keyIdx.Count; ++j)
                                                        {
                                                            for (int x = 0; x < rowIdx.Count; ++x)
                                                            {
                                                                int S = 0, s = 0;
                                                                if (axesCount == 1)
                                                                {
                                                                    S = 1;
                                                                    s = rowIdxSectorInfo[x][0];
                                                                    if (s > axisQsectorsCount[0]) s = 0;
                                                                }
                                                                else
                                                                {
                                                                    S = rowIdxSectorInfo[x][0];
                                                                    if (S > 0 && S <= axisQsectorsCount[0])
                                                                    {
                                                                        s = rowIdxSectorInfo[x][1];
                                                                        if (s > axisQsectorsCount[1]) s = 0;
                                                                    }
                                                                }
                                                                if (s > 0)
                                                                {
                                                                    // tmpNumArrayArray[S - 1][s - 1, n - 1] = 1.0;
                                                                    /*
                                                                    for (int t = 0; t < tmpListListArray[S - 1][0].Count; ++t)
                                                                    {
                                                                        tmpListListArray[S - 1][0][t].AddOverlapDetail(s, 1.0, 1.0, wb);
                                                                    }
                                                                    tmpListListArray[S - 1][0].Add(dataArray[j][rowIdx[x], 1]);
                                                                    */
                                                                    for (int t = 0; t < tmpListListArray[S - 1][n].Count; ++t)
                                                                    {
                                                                        tmpListListArray[S - 1][n][t].AddOverlapDetail(s, 1.0, 1.0, wb);
                                                                    }
                                                                    tmpListListArray[S - 1][n].Add(dataArray[keyIdx[j]][rowIdx[x], 1 + n]);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (weighted) w += (double)wtList[n.ToString()];
                                                }
                                                isNA = false;
                                                if (!isEffectiveSample) isEffectiveSample = weighted;
                                            }
                                        }
                                    }
                                    if (isNA)
                                    {
                                        // 無回答扱い
                                        columnIdx.Add(naIdx);
                                        IncrementTotal = !CutNA;
                                    }
#if N0_INCLUDE_NA_IV
                                    AddedTotalOverlap = !IncrementTotal || significanceTestCode != SignificanceTestCode.BetweenSectors || !CountOverlap;
#else
                                    if (IncrementTotal && significanceTestCode == SignificanceTestCode.BetweenSectors && CountOverlap)
                                    {
                                        for (int j = 0; j < keyIdx.Count; ++j)
                                        {
                                            for (int x = 0; x < rowIdx.Count; ++x)
                                            {
                                                int S = 0, s = 0;
                                                if (axesCount == 1)
                                                {
                                                    S = 1;
                                                    s = rowIdxSectorInfo[x][0];
                                                    if (s > axisQsectorsCount[0]) s = 0;
                                                }
                                                else
                                                {
                                                    S = rowIdxSectorInfo[x][0];
                                                    if (S > 0 && S <= axisQsectorsCount[0])
                                                    {
                                                        s = rowIdxSectorInfo[x][1];
                                                        if (s > axisQsectorsCount[1]) s = 0;
                                                    }
                                                }
                                                if (s > 0)
                                                {
                                                    // tmpNumArrayArray[S - 1][s - 1, n - 1] = 1.0;
                                                    for (int t = 0; t < tmpListListArray[S - 1][0].Count; ++t)
                                                    {
                                                        tmpListListArray[S - 1][0][t].AddOverlapDetail(s, 1.0, 1.0, wb);
                                                    }
                                                    tmpListListArray[S - 1][0].Add(dataArray[keyIdx[j]][rowIdx[x], 1]);
                                                }
                                            }
                                        }
                                    }
#endif
                                    if (isEffectiveSample)
                                    {
                                        // if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                                        if (SignificanceTestOn)
                                        {
                                            for (int j = 0; j < keyIdx.Count; ++j)
                                            {
                                                for (int x = 0; x < rowIdx.Count; ++x)
                                                {
                                                    dataArray[keyIdx[j]][rowIdx[x], naIdx + 2].AddDetail(w, wb);
                                                    if (CountOverlap)
                                                    {
                                                        int S = 0, s = 0;
                                                        if (axesCount == 1)
                                                        {
                                                            S = 1;
                                                            s = rowIdxSectorInfo[x][0];
                                                            if (s > axisQsectorsCount[0]) s = 0;
                                                        }
                                                        else
                                                        {
                                                            S = rowIdxSectorInfo[x][0];
                                                            if (S > 0 && S <= axisQsectorsCount[0])
                                                            {
                                                                s = rowIdxSectorInfo[x][1];
                                                                if (s > axisQsectorsCount[1]) s = 0;
                                                            }
                                                        }
                                                        if (s > 0)
                                                        {
                                                            for (int t = 0; t < tmpListArray[S - 1].Count; ++t)
                                                            {
                                                                tmpListArray[S - 1][t].AddOverlapDetail(s, tmpNumListArray[S - 1][t], w, wb);
                                                            }
                                                            tmpListArray[S - 1].Add(dataArray[keyIdx[j]][rowIdx[x], naIdx + 2]);
                                                            tmpNumListArray[S - 1].Add(w);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            columnIdx.Add(naIdx + 2);    // 統計量母数
                                        }
                                    }
                                    break;
                                }
                            case QuestionType.N:
                                {
                                    columnIdx.Add(2);   // 統計量母数
                                    v = (data[i] as NData).Value;
                                    for (int j = 0; j < keyIdx.Count; ++j)
                                    {
                                        for (int x = 0; x < rowIdx.Count; ++x)
                                        {
                                            // 値を個別に確保
                                            // normalDatas[keyIdx[j]][rowIdx[x]][++lastIndex[keyIdx[j]][rowIdx[x]]] = v;
                                            // if (wb > 0.0) normalDatas[keyIdx[j]][rowIdx[x]][++lastIndex[keyIdx[j]][rowIdx[x]]] = v;
                                            if (wb > 0.0) normalDatas[keyIdx[j]][rowIdx[x]][++lastIndex[keyIdx[j]][rowIdx[x]]] = new NumericData(v, wb);
                                            if (SignificanceTestOn)
                                            {
                                                if (CountOverlap)
                                                {
                                                    int S = 0, s = 0;
                                                    if (axesCount == 1)
                                                    {
                                                        S = 1;
                                                        s = rowIdxSectorInfo[x][0];
                                                        if (s > axisQsectorsCount[0]) s = 0;
                                                    }
                                                    else
                                                    {
                                                        S = rowIdxSectorInfo[x][0];
                                                        if (S > 0 && S <= axisQsectorsCount[0])
                                                        {
                                                            s = rowIdxSectorInfo[x][1];
                                                            if (s > axisQsectorsCount[1]) s = 0;
                                                        }
                                                    }
                                                    if (s > 0)
                                                    {
                                                        for (int t = 0; t < tmpListArray[S - 1].Count; ++t)
                                                        {
                                                            tmpListArray[S - 1][t].AddOverlapDetail(s, tmpNumListArray[S - 1][t], v, wb);
                                                        }
                                                        tmpListArray[S - 1].Add(dataArray[keyIdx[j]][rowIdx[x], 2]);
                                                        tmpNumListArray[S - 1].Add(v);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // 合計
                                                nArray[keyIdx[j]][rowIdx[x], 3] += v * wb;
                                                // 平方の合計
                                                nArray[keyIdx[j]][rowIdx[x], 4] += Math.Pow(v, 2.0) * wb;
                                            }
                                        }
                                    }
                                    break;
                                }
                        }
                        break;
                    default:
                        break;
                }
                if (IncrementTotal)
                {
                    if (SignificanceTestOn)
                    {
                        columnIdx.Add(0);   // WB前全体
#if N0_INCLUDE_NA_IV
                        if (!AddedTotalOverlap)
                        {
                            for (int j = 0; j < keyIdx.Count; ++j)
                            {
                                for (int x = 0; x < rowIdx.Count; ++x)
                                {
                                    int S = 0, s = 0;
                                    if (axesCount == 1)
                                    {
                                        S = 1;
                                        s = rowIdxSectorInfo[x][0];
                                        if (s > axisQsectorsCount[0]) s = 0;
                                    }
                                    else
                                    {
                                        S = rowIdxSectorInfo[x][0];
                                        if (S > 0 && S <= axisQsectorsCount[0])
                                        {
                                            s = rowIdxSectorInfo[x][1];
                                            if (s > axisQsectorsCount[1]) s = 0;
                                        }
                                    }
                                    if (s > 0)
                                    {
                                        // tmpNumArrayArray[S - 1][s - 1, n - 1] = 1.0;
                                        for (int t = 0; t < tmpListListArray[S - 1][0].Count; ++t)
                                        {
                                            tmpListListArray[S - 1][0][t].AddOverlapDetail(s, 1.0, 1.0, wb);
                                        }
                                        tmpListListArray[S - 1][0].Add(dataArray[keyIdx[j]][rowIdx[x], 1]);
                                    }
                                }
                            }
                        }
#endif
                    }
                    else
                    {
                        // WB前全体のインクリメント
                        for (int j = 0; j < keyIdx.Count; ++j)
                        {
                            for (int x = 0; x < rowIdx.Count; ++x)
                            {
                                nArray[keyIdx[j]][rowIdx[x], 0] += 1.0;
                            }
                        }
                    }
                    columnIdx.Add(1);   // 全体
                }
                // 更新該当箇所にWBを加算
                for (int j = 0; j < keyIdx.Count; ++j)
                {
                    for (int r = 0; r < rowIdx.Count; ++r)
                    {
                        for (int c = 0; c < columnIdx.Count; ++c)
                        {
                            if (SignificanceTestOn)
                            {
                                dataArray[keyIdx[j]][rowIdx[r], columnIdx[c]].AddDetail(v, wb);
                            }
                            else
                            {
                                nArray[keyIdx[j]][rowIdx[r], columnIdx[c]] += wb;
                            }
                        }
                    }
                }
            }

            // normalDatasのソート (クイックソート)
            if (qType == QuestionType.N)
            {
                for (int i = 0; i < k; ++i)
                {
                    for (int j = 0; j < axisRowsCount; ++j)
                    {
                        // if (lastIndex[i][j] > 0) Array.Sort<double>(normalDatas[i][j], 0, lastIndex[i][j] + 1);
                        if (lastIndex[i][j] > 0) Array.Sort<NumericData>(normalDatas[i][j], 0, lastIndex[i][j] + 1);
                    }
                }
            }

            if (SignificanceTestOn)
            {
                StreamWriter writer = GlobalTabulation.CreateSignificanceTestLogWriter(ref SignificanceTestLogFilePath);
                try
                {
                    string subHeaderBuffer = null;
                    if (writer != null)
                    {
                        writer.WriteLine("Cross SignificanceTest between "
                            + (significanceTestCode == SignificanceTestCode.BetweenSectors ? "categories" : "category and whole")
                            + " (" + qTypeOr.ToString() + ")");
                        System.Text.StringBuilder builder = new System.Text.StringBuilder("");
                        builder.AppendLine(string.Format("Item:{0}", qName));
                        if (qType != QuestionType.N)
                        {
                            builder.AppendLine("\tCategory");
                            for (int i = 0; i < sectorsCount; ++i)
                            {
                                builder.AppendLine(string.Format("\t\t{0}:{1}", i + 1, sectorDescriptions[i]));
                            }
                        }
                        for (int i = 0; i < axesCount; ++i)
                        {
                            builder.AppendLine(string.Format("AxisItem{0}:{1}"
                                                , axesCount == 1 ? string.Empty : (i + 1).ToString(), axisQName[i]));
                            builder.AppendLine("\tCategory");
                            for (int j = 0; j < axisQsectorsCount[i]; ++j)
                            {
                                builder.AppendLine(string.Format("\t\t{0}:{1}", j + 1, axisQsectorDescription[i][j]));
                            }
                        }
                        subHeaderBuffer = builder.ToString();
                    }
                    SignificanceTest(ref dataArray, significanceTestCode, significanceTestLevel, markingTotal
                                   , writer, keyQName, qType, subHeaderBuffer, axesCount, rowSectorNumbers);
                    if (writer != null) writer.Close();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (writer != null) writer.Dispose();
                }
            }

            // 出力配列の仕上げ
            if (qType != QuestionType.N)    // SA/MA
            {
                /*
                // 加重平均情報の精査
                double wtCnt = 0;   // ウエイト値の設定されている選択肢数
                double[] wtNum = new double[wt.Length]; // 数値型配列
                for (int i = 0; i < sectorsCount; ++i)
                {
                    wtNum[i] = double.MinValue;
                    if (wt[i] != null)
                    {
                        if (double.TryParse(wt[i], out wtNum[i]))
                        {
                            ++wtCnt;
                            for (int j = 0; j < k; ++j)
                            {
                                resultArray[j][1, 1 + axesCount + 2 + i] = new DataWithMarking(wt[i]);
                            }
                        }
                    }
                }
                */
                for (int i = 0; i < k; ++i)
                {
                    for (int r = 0; r < (SignificanceTestOn ? dataArray[i].GetLength(0) : nArray[i].GetLength(0)); ++r)
                    {
                        // N値の投入
                        if (SignificanceTestOn)
                        {
                            for (int j = 0; j < dataArray[i].GetLength(1) - 1; ++j)
                            {
                                resultArray[i][2 + r, 1 + axesCount + j] = dataArray[i][r, j];
                            }
                            resultArray[i][2 + r, axesCount + 2 + sectorsCount + 3] = new DataWithMarking(dataArray[i][r, 1 + sectorsCount + 3].Count.ToString());
                        }
                        else
                        {
                            for (int c = 0; c < nArray[i].GetLength(1); ++c)
                            // 加重平均、加重平均母数の順→ボツで元に戻す
                            // for (int c = 0; c < nArray[i].GetLength(1) - 1; ++c)
                            {
                                resultArray[i][2 + r, 1 + axesCount + c] = new DataWithMarking(nArray[i][r, c].ToString());
                            }
                            // int x = nArray[i].GetUpperBound(1);
                            // resultArray[i][2 + r, 1 + axesCount + x + 1] = new DataWithMarking(nArray[i][r, x].ToString());
                        }
                        // ％値の投入
                        if (SignificanceTestOn)
                        {
                            if (dataArray[i][r, 1].NumValue != 0.0)
                            {
                                for (int j = 2; j < dataArray[i].GetLength(1); ++j)
                                {
                                    resultArray[i][2 + r, 1 + axesCount + j].Percent = dataArray[i][r, j].NumValue * 100.0 / dataArray[i][r, 1].NumValue;
                                }
                            }
                        }
                        else
                        {
                            /*
                            if (nArray[i][r, 1] == 0.0)
                            {
                                for (int c = 2; c < nArray[i].GetLength(1); ++c)
                                {
                                    resultArray[i][2 + r, 1 + axesCount + c].Percent = 0.0;
                                }
                            }
                            else
                            {
                                for (int c = 2; c < nArray[i].GetLength(1); ++c)
                                {
                                    resultArray[i][2 + r, 1 + axesCount + c].Percent = nArray[i][r, c] * 100.0 / nArray[i][r, 1];
                                }
                            }
                            */
                            if (nArray[i][r, 1] != 0.0)
                            {
                                for (int c = 2; c < nArray[i].GetLength(1); ++c)
                                {
                                    resultArray[i][2 + r, 1 + axesCount + c].Percent = nArray[i][r, c] * 100.0 / nArray[i][r, 1];
                                }
                            }
                        }
                        // 加重平均の投入
                        /*
                        if (wtCnt > 0)
                        {
                            double wtSum = 0.0;
                            for (int j = 0; j < sectorsCount; ++j)
                            {
                                if (wtNum[j] != double.MinValue)
                                {
                                    wtSum += nArray[i][r, 2 + j] * wtNum[j];
                                }
                            }
                            resultArray[i][2 + r, axesCount + 2 + sectorsCount + 3] = new DataWithMarking((wtSum / wtCnt).ToString());
                        }
                        */
                        if (wtList.Count > 0)
                        {
                            // double s = significanceTestCode == SignificanceTestCode.BetweenSectors ? dataArray[i][r, 1 + sectorsCount + 3].NumValue : 0.0;
                            //double s = significanceTestCode == SignificanceTestCode.BetweenSectors  ? dataArray[i][r, 1 + sectorsCount + 3].NumValue : 0.0;

                            //double s = SignificanceTestOn ? (hasLower ? (dataArray[i][r, 1 + sectorsCount + 3].NumValue)
                            //       : ((dataArray[i][r, 1 + sectorsCount + 3].NumValue) - ((dataArray[i][r, 1 + sectorsCount + 1].NumValue))))
                            //       : (hasLower ? 0.0 : 0.0 /*nArray[i][c][1 + sectorsCount + 1]*/);

                            double s = SignificanceTestOn ? (dataArray[i][r, 1 + sectorsCount + 3].NumValue) : 0.0;


                            for (int j = 1; j <= sectorsCount; ++j)
                            {
                                if (wtList.ContainsKey(j.ToString()))
                                {
                                    //if (SignificanceTestOn && !hasLower)
                                    //{
                                    //    s += dataArray[i][r, 1 + j].NumValue * (double)wtList[j.ToString()];
                                    //}

                                    if (!SignificanceTestOn)
                                    {
                                        s += nArray[i][r, 1 + j] * (double)wtList[j.ToString()];
                                    }
                                    resultArray[i][1, axesCount + 2 + j] = new DataWithMarking(wt[j - 1]);
                                }
                            }
                            if (SignificanceTestOn)
                            {
                                if (dataArray[i][r, 1 + sectorsCount + 3].Count > 0.0)
                                {
                                    resultArray[i][2 + r, axesCount + 2 + sectorsCount + 4] = new DataWithMarking((s / dataArray[i][r, 1 + sectorsCount + 3].Count).ToString());
                                    if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                                    {
                                        // resultArray[i][2 + r, axesCount + 2 + sectorsCount + 4].SetSignificanceCharacters(dataArray[i][r, 1 + sectorsCount + 3].SignificanceCharacters());
                                        dataArray[i][r, 1 + sectorsCount + 3].CloneSignificanceSectorNumbers(ref resultArray[i][2 + r, axesCount + 2 + sectorsCount + 4]);
                                    }
                                    else
                                    {
                                        resultArray[i][2 + r, axesCount + 2 + sectorsCount + 4].AppendMarking(dataArray[i][r, 1 + sectorsCount + 3].Marking, true);
                                    }
                                }
                                else
                                {
                                    resultArray[i][2 + r, axesCount + 2 + sectorsCount + 4] = new DataWithMarking("-", false);
                                }
                            }
                            else
                            {
                                if (nArray[i][r, 1 + sectorsCount + 3] > 0.0)
                                {
                                    resultArray[i][2 + r, axesCount + 2 + sectorsCount + 4] = new DataWithMarking((s / nArray[i][r, 1 + sectorsCount + 3]).ToString());
                                }
                                else
                                {
                                    // resultArray[i][2 + r, axesCount + 2 + sectorsCount + 4] = new DataWithMarking("0.0");
                                    resultArray[i][2 + r, axesCount + 2 + sectorsCount + 4] = new DataWithMarking("-", false);
                                }
                            }
                        }
                    }
                    // マーキング
                    int prewbtotalColumn = axesCount + 1;
                    int startColumn = prewbtotalColumn + 2;
                    int endColumn = startColumn + (sectorsCount - 1 - subTotalCnt);
                    int endColumnColor = startColumn + (sectorsCount - 1);
                    if (axesCount == 2) // 三重クロス
                    {
                        int n = 1 + 1 + axisQsectorsCount[1] + 2;
                        // ランキング
                        // 全体行
                        GlobalTabulation.MarkingRanking(ref resultArray[i]
                                    , 2, 2
                                    , startColumn, endColumn, questionType);
                        // 小計行
                        GlobalTabulation.MarkingRanking(ref resultArray[i]
                                    , 3, 3
                                    , startColumn, endColumn, questionType);
                        // 各選択肢行
                        for (int j = 0; j < axisQsectorsCount[0]; ++j)
                        {
                            GlobalTabulation.MarkingRanking(ref resultArray[i]
                                        , 4 + j * n, 4 + (j + 1) * n - 3
                                        , startColumn, endColumn, questionType);
                        }
#if IS_2ND_PHASE
                        // 昇降分析
                        for (int j = 0; j < axisQsectorsCount[0]; ++j)
                        {
                            GlobalTabulation.MarkingAscending(ref resultArray[i]
                                        , 6 + j * n + 1, 6 + (j + 1) * n - 5
                                        , startColumn, endColumn, questionType);
                        }
#endif
                        // 全体との差のマーキング
                        for (int j = 0; j < axisQsectorsCount[0]; ++j)
                        {
                            int totalRow = markingTotal == GlobalTabulation.MarkingTotal.Total ? 2 : 3;
                            // 色付け
                            GlobalTabulation.MarkingColoring(ref resultArray[i], totalRow
                                        , 4 + j * n, 4 + j * n
                                        , startColumn, endColumnColor, questionType
                                        , coloringLevel1Percent, coloringLevel2Percent);
                            /*
                            #if IS_2ND_PHASE
                                                        // 有意差検定
                                                        GlobalTabulation.MarkingSignificance(ref resultArray[i]
                                                                    , totalRow, prewbtotalColumn
                                                                    , 4 + j * n, 4 + j * n
                                                                    , startColumn, endColumn, questionType);
                            #endif
                            */
                            totalRow = markingTotal == GlobalTabulation.MarkingTotal.Total ? 2 : 5 + j * n;
                            // 色付け
                            GlobalTabulation.MarkingColoring(ref resultArray[i], totalRow
                                        , 6 + j * n, 6 + (j + 1) * n - 5
                                        , startColumn, endColumnColor, questionType
                                        , coloringLevel1Percent, coloringLevel2Percent);
                            /*
                            #if IS_2ND_PHASE
                                                        // 有意差検定
                                                        GlobalTabulation.MarkingSignificance(ref resultArray[i]
                                                                    , totalRow, prewbtotalColumn
                                                                    , 6 + j * n, 6 + (j + 1) * n - 5
                                                                    , startColumn, endColumn, questionType);
                            #endif
                            */
                        }
                    }
                    else    // axesCount == 1   // 二重クロス
                    {
                        // ランキング
                        GlobalTabulation.MarkingRanking(ref resultArray[i], 2, 3 + axisQsectorsCount[0]
                                    , startColumn, endColumn, questionType);
#if IS_2ND_PHASE
                        // 昇降分析
                        GlobalTabulation.MarkingAscending(ref resultArray[i], 4, 3 + axisQsectorsCount[0]
                                    , startColumn, endColumn, questionType);
#endif
                        // 全体との差の色付け
                        int totalRow = markingTotal == GlobalTabulation.MarkingTotal.Total ? 2 : 3;
                        GlobalTabulation.MarkingColoring(ref resultArray[i], totalRow, 4, 3 + axisQsectorsCount[0]
                                    , startColumn, endColumnColor, questionType, coloringLevel1Percent, coloringLevel2Percent);
                        /*
                        #if IS_2ND_PHASE
                                                // 全体との差の有意差検定
                                                GlobalTabulation.MarkingSignificance(ref resultArray[i]
                                                            , totalRow, prewbtotalColumn, 4, 3 + axisQsectorsCount[0]
                                                            , startColumn, endColumn, questionType);
                        #endif
                        */
                    }
                }
            }
            else    // N
            {
                for (int i = 0; i < k; ++i)
                {
                    for (int r = 0; r < (SignificanceTestOn ? dataArray[i].GetLength(0) : nArray[i].GetLength(0)); ++r)
                    {
                        double n = 0.0; // 統計量母数
                        // 列方向インデックス
                        int c = axesCount;
                        if (SignificanceTestOn)
                        {
                            resultArray[i][1 + r, ++c] = dataArray[i][r, 0];    // WB前全体
                            resultArray[i][1 + r, ++c] = dataArray[i][r, 1];    // 全体
                            n = dataArray[i][r, 2].Count;
                            resultArray[i][1 + r, ++c] = new DataWithMarking(n.ToString()); // 統計量母数
                            resultArray[i][1 + r, ++c] = new DataWithMarking(n == 0.0 ? "-" : dataArray[i][r, 2].NumValue.ToString());  // 合計
                        }
                        else
                        {
                            resultArray[i][1 + r, ++c] = new DataWithMarking(nArray[i][r, 0].ToString());    // WB前全体
                            resultArray[i][1 + r, ++c] = new DataWithMarking(nArray[i][r, 1].ToString());    // 全体
                            n = nArray[i][r, 2];
                            resultArray[i][1 + r, ++c] = new DataWithMarking(n.ToString());    // 統計量母数
                            // resultArray[i][1 + r, ++c] = new DataWithMarking(nArray[i][r, 3].ToString());    // 合計
                            resultArray[i][1 + r, ++c] = new DataWithMarking(n == 0.0 ? "-" : nArray[i][r, 3].ToString());    // 合計
                        }
                        // 平均
                        // double average = 0.0;
                        double average = double.NaN;
                        if (n != 0.0)
                        {
                            average = (SignificanceTestOn ? dataArray[i][r, 2].NumValue : nArray[i][r, 3]) / n;
                        }
                        // resultArray[i][1 + r, ++c] = new DataWithMarking(average.ToString());
                        resultArray[i][1 + r, ++c] = new DataWithMarking(double.IsNaN(average) ? "-" : average.ToString());
                        if (SignificanceTestOn)
                        {
                            if (significanceTestCode == SignificanceTestCode.BetweenSectors)
                            {
                                // resultArray[i][1 + r, c].SetSignificanceCharacters(dataArray[i][r, 2].SignificanceCharacters());
                                dataArray[i][r, 2].CloneSignificanceSectorNumbers(ref resultArray[i][1 + r, c]);
                            }
                            else
                            {
                                resultArray[i][1 + r, c].AppendMarking(dataArray[i][r, 2].Marking, true);
                            }
                        }
                        // 標準偏差
                        // double stdev = 0.0;
                        double stdev = double.NaN;
                        if (n > 1.0)
                        {
                            // 統計量母数×平方の合計－合計の平方
                            double weightSum = 0;
                            double standardDeviation = 0;
                            double normalValue = 0;
                            if (SignificanceTestOn)
                            {
                                double WeightedMean = dataArray[i][r, 2].NumValue / n;
                                for (int j = 0; j < normalDatas[i][r].Length; j++)
                                {
                                    normalValue = normalDatas[i][r][j].Value;
                                    standardDeviation += normalDatas[i][r][j].WeightBack * Math.Pow((normalValue - WeightedMean), 2.0);
                                }
                            }
                            else
                            {
                                double WeightedMean = nArray[i][r, 3] / n;
                                for (int j = 0; j < normalDatas[i][r].Length; j++)
                                {
                                    normalValue = normalDatas[i][r][j].Value;
                                    standardDeviation += normalDatas[i][r][j].WeightBack * Math.Pow((normalValue - WeightedMean), 2.0);
                                }
                            }
                            stdev = Math.Sqrt(standardDeviation / (((n - 1) / n) * n));
                        }
                        // resultArray[i][1 + r, ++c] = new DataWithMarking(stdev.ToString());
                        resultArray[i][1 + r, ++c] = new DataWithMarking(double.IsNaN(stdev) ? "-" : stdev.ToString());
                        // 最小値
                        // double min = 0.0;
                        double min = double.NaN;
                        // 最大値
                        // double max = 0.0;
                        double max = double.NaN;
                        // 中央値
                        // double median = 0.0;
                        double median = double.NaN;
                        if (lastIndex[i][r] > -1)
                        {
                            // min = normalDatas[i][r][0];
                            // max = normalDatas[i][r][lastIndex[i][r]];
                            min = normalDatas[i][r][0].Value;
                            max = normalDatas[i][r][lastIndex[i][r]].Value;
                            /*
                            int medIdx = lastIndex[i][r] / 2;
                            //if (lastIndex[i][r] % 2 == 0)   // 要素数が奇数
                            //{
                            //    median = normalDatas[i][r][medIdx];
                            //}
                            //else    // 要素数が偶数
                            //{
                            //    median = (double)(normalDatas[i][r][medIdx] + normalDatas[i][r][medIdx + 1]) / 2.0;
                            //}
                            median = normalDatas[i][r][medIdx];
                            double medPos = (double)lastIndex[i][r] / 2.0;
                            if (medPos > (double)medIdx)
                            {
                                double d = (normalDatas[i][r][medIdx + 1] - normalDatas[i][r][medIdx]) * (medPos - (double)medIdx);
                                median += d;
                            }
                            */
                            median = GlobalTabulation.GetMedian(normalDatas[i][r], lastIndex[i][r]);
                        }
                        // resultArray[i][1 + r, ++c] = new DataWithMarking(min.ToString());
                        // resultArray[i][1 + r, ++c] = new DataWithMarking(max.ToString());
                        // resultArray[i][1 + r, ++c] = new DataWithMarking(median.ToString());
                        resultArray[i][1 + r, ++c] = new DataWithMarking(double.IsNaN(min) ? "-" : min.ToString());
                        resultArray[i][1 + r, ++c] = new DataWithMarking(double.IsNaN(max) ? "-" : max.ToString());
                        resultArray[i][1 + r, ++c] = new DataWithMarking(double.IsNaN(median) ? "-" : median.ToString());
                        int x = SignificanceTestOn ? 2 : 4;
                        if (SignificanceTestOn)
                        {
                            resultArray[i][1 + r, ++c] = dataArray[i][r, ++x];  // 無回答
                            resultArray[i][1 + r, ++c] = dataArray[i][r, ++x];  // 非該当
                        }
                        else
                        {
                            resultArray[i][1 + r, ++c] = new DataWithMarking(nArray[i][r, ++x].ToString());    // 無回答
                            resultArray[i][1 + r, ++c] = new DataWithMarking(nArray[i][r, ++x].ToString());    // 非該当
                        }
                    }
                }
            }
            return null;
        }

        /// <alias>getCrossArray01</alias>
        /// <summary>
        /// <para>エイリアス:getCrossArray01</para>
        /// クロス表イメージ二次元配列を生成して返す<br />
        /// 分類アイテムの指定がない場合に、getCrossArray00の戻り値resultArrayの1つ目の要素を返す
        /// <note>
        /// 表の単位は、集計項目×集計軸で1つ
        /// 1シート1クロスの場合の集計項目の結合表は、本メソッドを軸ごとに実行して、受け取り側でマージする
        /// </note>
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="data">Dataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="axisQuestionType">集計軸アイテムの質問タイプを表すQuestionType列挙型の値からなるListクラスのインスタンスへの参照</param>
        /// <param name="axisQDescription">集計軸アイテムの質問文からなるListクラスのインスタンスへの参照</param>
        /// <param name="axisQsectorDescription">集計軸アイテムの選択肢文を要素とする配列からなるListクラスのインスタンスへの参照</param>
        /// <param name="axisData">集計軸アイテムのデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="descs">表示文字列を保持するクラス</param>
        /// <param name="FilteringFlag">絞り込みフラグ配列 (省略可能)</param>
        /// <param name="weightback">WB情報を保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照 (省略可能)</param>
        /// <param name="wt">ウエイト値情報を保持した配列 (省略可能)</param>
        /// <param name="coloringLevel1Percent">全体との差の色付けの水準1のパーセンテージ (省略可能, 既定値5)</param>
        /// <param name="coloringLevel2Percent">全体との差の色付けの水準2のパーセンテージ (省略可能, 既定値10)</param>
        /// <param name="SubtotalIncludeAxisNA">軸の小計に無回答を含めるかどうかを示すフラグ (省略可能, 既定値true)</param>
        /// <param name="SubtotalIncludeAxisIV">軸の小計に非該当を含めるかどうかを示すフラグ (省略可能, 既定値true)</param>
        /// <param name="markingTotal">マーキングの対象となる全体を表すMarkingTotal列挙型の値 (省略可能, 既定値MarkingTotal.Total)</param>
        /// <param name="TabulateFullQuantity">全数ベース集計のときtrue(省略可、既定値true)</param>
        /// <param name="IVtoNA">非該当を無回答に含めるときtrue (省略可、既定値false)</param>
        /// <param name="locale">多言語情報取得用情報(省略可、規定値ja)</param>
        /// <param name="CutNA">集計対象の無回答を全体に含めないときtrue (省略可、既定値false)</param>
        /// <param name="SignificanceTestOn">全体との差の検定または項目間検定を行うときはtrue (省略可、既定値false)</param>
        /// <param name="significanceTestCode">
        /// 項目間検定の種類を表すSignificanceTestCode列挙型のコード値 (省略可、既定値Off)<br />
        /// この値がBetweenSectors以外の値であることは、有意差検定の種類が全体との差の検定であることを表す
        /// <note>この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される</note>
        /// </param>
        /// <param name="significanceTestLevel">
        /// 項目間検定を行う場合の、有意水準(百分率)からなる配列<br />
        /// 現行仕様での要素数は、全体との差の検定では最大3つ、項目間検定では最大2つ (省略可、既定値null)
        /// <note>
        /// この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される<br />
        /// また、全体との差の検定時で、この値がnullのときには、既定として1％、5％、10％を指定したものとする
        /// </note>
        /// </param>
        /// <param name="SignificanceTestLogFilePath">
        /// 検定のログファイルの出力先パス (省略可、既定値null)
        /// <note>この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される</note>
        /// </param>
        /// <param name="qName">
        /// 集計対象アイテム名 (省略可、既定値null)
        /// <note>この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <param name="axisQName">
        /// 集計軸アイテムのアイテム名からなるListクラスのインスタンスへの参照
        /// <note>この値は<paramref name="SignificanceTestOn"/>がfalseのときには無視される</note>
        /// <note>この値は<paramref name="SignificanceTestLogFilePath"/>がnullの場合は無視される</note>
        /// </param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.QuestionType">QuestionType列挙型</seealso>
        /// <seealso cref="T:Macromill.QCWeb.Tabulation.Data">Dataクラス</seealso>
        /// <seealso cref="M:Macromill.QCWeb.Tabulation.CrossTabulation.GetCrossArray(Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.Collections.Generic.List{Macromill.QCWeb.Tabulation.QuestionType},System.Collections.Generic.List{System.String[]},System.Collections.Generic.List{System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data}},Macromill.QCWeb.Tabulation.QuestionType,System.String[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[][0:,0:]@,System.Boolean[],System.Collections.Generic.List{Macromill.QCWeb.Tabulation.Data},System.String[],System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String)">getCrossArray00</seealso>
        public static QCWebException GetCrossArray(
                    QuestionType questionType, string[] sectorDescription, List<Data> data
                  , List<QuestionType> axisQuestionType, List<string> axisQDescription, List<string[]> axisQsectorDescription, List<List<Data>> axisData
                  , out DataWithMarking[,] resultArray, Translation translation
                  , TabulationDescriptions descs
                  , bool[] FilteringFlag = null, List<Data> weightback = null, string[] wt = null
                  , int coloringLevel1Percent = GlobalTabulation.COLORING_LEVEL1_DEFAULT
                  , int coloringLevel2Percent = GlobalTabulation.COLORING_LEVEL2_DEFAULT
                  , bool SubtotalIncludeAxisNA = true, bool SubtotalIncludeAxisIV = true
                  , GlobalTabulation.MarkingTotal markingTotal = GlobalTabulation.MarkingTotal.Total
                  , bool TabulateFullQuantity = true, bool IVtoNA = false, string locale = "ja", bool CutNA = false
                  , bool SignificanceTestOn = false, SignificanceTestCode significanceTestCode = SignificanceTestCode.Off
                  , double[] significanceTestLevel = null, string SignificanceTestLogFilePath = null
                  , string qName = null, List<string> axisQName = null, bool hasCount = false, int subTotalCnt = 0, QuestionType qTypeOr = 0, bool hasLower = true
                  )
        {
            DataWithMarking[][,] res = null;
            QCWebException exception = GetCrossArray(questionType, sectorDescription, data
                                                   , axisQuestionType, axisQDescription, axisQsectorDescription, axisData
                                                   , (QuestionType)0, null, null, out res, translation
                                                   , descs
                                                   , FilteringFlag, weightback, wt
                                                   , coloringLevel1Percent, coloringLevel2Percent
                                                   , SubtotalIncludeAxisNA, SubtotalIncludeAxisIV
                                                   , markingTotal, TabulateFullQuantity, IVtoNA, locale, CutNA
                                                   , SignificanceTestOn, significanceTestCode, significanceTestLevel
                                                   , SignificanceTestLogFilePath, qName, null, axisQName, hasCount, subTotalCnt, qTypeOr: qTypeOr, hasLower: hasLower);
            resultArray = null;
            if (exception != null) return exception;
            if (res != null && res.Length == 1) resultArray = res[0];
            return null;
        }
        #endregion
        #endregion
    }
    #endregion

    #region FAListTabulationクラス
    /// <summary>
    /// FAリストの作成に必要なメソッドをまとめた静的クラス
    /// </summary>
    [ComVisible(false), Guid("34ED3DD0-E169-40A0-9284-F1C16C0C0B4F")]
    public static class FAListTabulation
    {
        #region 構造体
        #region SectorInformation構造体
        /// <summary>
        /// 選択肢の簡易情報を保持する構造体
        /// </summary>
        [ComVisible(false)]
        public struct SectorInformation
        {
            private int number;
            private string description;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="number">選択肢番号</param>
            /// <param name="description">選択肢文</param>
            public SectorInformation(int number, string description)
            {
                this.number = number;
                this.description = description;
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
            /// 選択肢文を返す読み取り専用プロパティ
            /// </summary>
            public string Description
            {
                get
                {
                    return description;
                }
            }
        }
        #endregion

        #region QuestionInformation構造体
        /// <summary>
        /// 質問の簡易情報を保持する構造体
        /// </summary>
        [ComVisible(false)]
        public struct QuestionInformation
        {
            private QuestionType questiontype;
            private string name;
            private string description;
            private string tableHeading; //added for FAList.cs
            private List<Data> data;
            private List<SectorInformation> sectorinformations;

            /// <summary>
            /// コンストラクタ<br />
            /// 付加アイテムの質問情報を保持する場合に使用
            /// </summary>
            /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
            /// <param name="description">質問文</param>
            /// <param name="data">データを保持したDataクラスのインスタンスへの参照をまとめたListクラスのインスタンスへの参照</param>
            public QuestionInformation(QuestionType questiontype, string description, List<Data> data)
            {
                this.questiontype = questiontype;
                this.name = null;
                this.description = description;
                this.tableHeading = null;
                this.data = data;
                sectorinformations = new List<SectorInformation>();
            }

            /// <summary>
            /// コンストラクタ<br />
            /// リスト化するFAアイテムの質問情報を保持する場合に使用
            /// </summary>
            /// <param name="description">質問文</param>
            /// <param name="data">データを保持したDataクラスのインスタンスへの参照をまとめたListクラスのインスタンスへの参照</param>
            public QuestionInformation(string description, List<Data> data)
            {
                this.questiontype = Tabulation.QuestionType.FA;
                this.name = null;
                this.description = description;
                this.tableHeading = null;
                this.data = data;
                sectorinformations = null;
            }

            /// <summary>
            /// to get FAlist with question, table heading and data
            /// added for FAList.cs
            /// </summary>
            /// <param name="description"></param>
            /// <param name="data"></param>
            public QuestionInformation(string description, string tableHeading, List<Data> data)
            {
                this.questiontype = Tabulation.QuestionType.FA;
                this.name = null;
                this.description = description;
                this.tableHeading = tableHeading;
                this.data = data;
                sectorinformations = null;
            }

            /// <summary>
            /// コンストラクタ<br />
            /// 分類アイテムの質問情報を保持する場合に使用
            /// </summary>
            /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
            /// <param name="name">アイテム名</param>
            /// <param name="description">質問文</param>
            /// <param name="data">データを保持したDataクラスのインスタンスへの参照をまとめたListクラスのインスタンスへの参照</param>
            public QuestionInformation(QuestionType questiontype, string name, string description, List<Data> data)
            {
                this.questiontype = questiontype;
                this.name = name;
                this.description = description;
                this.tableHeading = name; //edited for FAList.cs
                this.data = data;
                sectorinformations = new List<SectorInformation>();
            }

            /// <summary>
            /// 質問タイプを表すQuestionType列挙型の値を返す読み取り専用プロパティ
            /// </summary>
            public QuestionType QuestionType
            {
                get
                {
                    return questiontype;
                }
            }

            /// <summary>
            /// 質問文を返す読み取り専用プロパティ
            /// </summary>
            public string Description
            {
                get
                {
                    return description;
                }
            }

            //added for FAList.cs
            ///<summary>
            /// get table heading of the question information
            ///</summary>
            public string TableHeading
            {
                get
                {
                    return tableHeading;
                }
            }


            /// <summary>
            /// データを保持したDataクラスのインスタンスへの参照をまとめたListクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public List<Data> Datas
            {
                get
                {
                    return data;
                }
            }

            /// <summary>
            /// 選択肢の簡易情報を保持するSectorInformationクラスのインスタンスへの参照をまとめたListクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public List<SectorInformation> SectorInformations
            {
                get
                {
                    return sectorinformations;
                }
            }

            /// <summary>
            /// 選択肢の簡易情報を追加する
            /// </summary>
            /// <param name="sector"></param>
            public void AddSectorInformation(SectorInformation sector)
            {
                if (sectorinformations == null)
                {
                    sectorinformations = new List<SectorInformation>();
                }
                sectorinformations.Add(sector);
            }
        }
        #endregion
        #endregion

        #region FAリスト作成関連
        private static bool checkGetFAListArrayArguments(
                  List<QuestionInformation> FAItems, ref int FAItemsCount
                , List<QuestionInformation> addedItems, ref int addedItemsCount
                , QuestionInformation? keyItem, ref int keyQsectorsCount
                , ref int dataCount, ref bool[] FilteringFlag
                , out QCWebException exception)
        {
            exception = null;
            //ReadDBInfo db = new ReadDBInfo();  naresh
            //QuillInjector.GetInstance().Inject(db); naresh

            // FAアイテム情報のチェック
            //int faItemMax = int.Parse(db.AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_FALIST_FA_ITEM_MAX));
            int faItemMax = 30;
            if (FAItems == null || (FAItemsCount = FAItems.Count) == 0 || FAItemsCount > faItemMax)
            {
                exception = new QCWebException(new Message(
                        FAItems == null ? Constants.CommonMessageIndex.OutputItemInformationIsNullMessageIndex
                                        : Constants.CommonMessageIndex.OutputItemsCountIsOutOfRangeMessageIndex
                        ));
                return false;
            }
            for (int i = 0; i < FAItemsCount; ++i)
            {
                if (FAItems[i].Datas == null)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.OutputItemDataIsNullMessageIndex)
                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                 , (i + 1).ToString());
                    return false;
                }
                bool isError = (FAItems[i].QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
                if (!isError)
                {
                    isError = (FAItems[i].QuestionType & QuestionType.FA) != QuestionType.FA;
                }
                if (isError)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustOutputItemQuestionTypeMessageIndex)
                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                 , (i + 1).ToString(), FAItems[i].QuestionType.ToString());
                    return false;
                }
                if (i == 0)
                {
                    dataCount = FAItems[i].Datas.Count;
                    if (dataCount == 0)
                    {
                        // 出力アイテムデータが存在しません。
                        exception = new QCWebException(new Message(Constants.CommonMessageIndex.NotExistOutputItemDataMessageIndex));
                        return false;
                    }
                }
                else if (FAItems[i].Datas.Count != dataCount)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.ExistMismatchBetweenDatasMessageIndex));
                    return false;
                }
            }
            // 付加アイテム情報のチェック
            if (addedItems != null)
            {
                //int faAddItemMax = int.Parse(db.AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_FALIST_FA_ADD_ITEM_MAX));
                int faAddItemMax = 31;
                if ((addedItemsCount = addedItems.Count) > faAddItemMax)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.AddedItemsCountIsOutOfRangeMessageIndex));
                    return false;
                }
                for (int i = 0; i < addedItemsCount; ++i)
                {
                    if (addedItems[i].Datas == null)
                    {
                        exception = new QCWebException(new Message(Constants.CommonMessageIndex.AddedItemDataIsNullMessageIndex)
                                                     , GlobalsCommonConstant.LogLevel.FATAL
                                                     , (i + 1).ToString());
                        return false;
                    }
                    bool isError = (addedItems[i].QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
                    if (!isError)
                    {
                        isError = (int)(addedItems[i].QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.FA | QuestionType.N)) == 0;
                    }
                    if (isError)
                    {
                        exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustAddedItemQuestionTypeMessageIndex)
                                                     , GlobalsCommonConstant.LogLevel.FATAL
                                                     , (i + 1).ToString(), addedItems[i].QuestionType.ToString());
                        return false;
                    }
                    if ((int)(addedItems[i].QuestionType & (QuestionType.SA | QuestionType.MA)) != 0
                            && (addedItems[i].SectorInformations == null || addedItems[i].SectorInformations.Count == 0))
                    {
                        exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyAddedItemSectorInformationMessageIndex)
                                                     , GlobalsCommonConstant.LogLevel.FATAL
                                                     , (i + 1).ToString());
                        return false;
                    }
                    if (addedItems[i].Datas.Count != dataCount)
                    {
                        exception = new QCWebException(new Message(Constants.CommonMessageIndex.ExistMismatchBetweenDatasMessageIndex));
                        return false;
                    }
                }
            }
            // 分類アイテム情報のチェック
            if (keyItem != null)
            {
                QuestionInformation keyitem = (QuestionInformation)keyItem;
                //if (keyitem.Datas == null || keyitem.SectorInformations == null || keyitem.SectorInformations.Count == 0)
                if (keyitem.Datas == null || keyitem.SectorInformations == null || keyitem.SectorInformations.Count == 0)
                {
                    exception = new QCWebException(new Message(
                            keyitem.Datas == null ? Constants.CommonMessageIndex.ReferNullCategorizeItemDataMessageIndex
                                                  : Constants.CommonMessageIndex.NullOrEmptyCategorizeItemSectorInformationMessageIndex
                            ));
                    return false;
                }
                //bool isError = (keyitem.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
                bool isError = (keyitem.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
                if (!isError)
                {
                    isError = (int)(keyitem.QuestionType & (QuestionType.SA | QuestionType.MA)) == 0;
                }
                if (isError)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustCategorizeItemQuestionTypeMessageIndex)
                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                 , keyitem.QuestionType.ToString());
                    return false;
                }
                if (keyitem.Datas.Count != dataCount)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.ExistMismatchBetweenDatasMessageIndex));
                    return false;
                }
                keyQsectorsCount = keyitem.SectorInformations.Count;
            }

            int l = FilteringFlag == null ? 0 : FilteringFlag.Length;
            // 絞り込みフラグ配列のサイズが足りなければ既定値で補充
            Array.Resize<bool>(ref FilteringFlag, dataCount);
            for (int i = l; i < dataCount; ++i)
            {
                FilteringFlag[i] = true;
            }

            return true;
        }

        /// <alias>getFAListArray00</alias>
        /// <summary>
        /// <para>エイリアス:getFAListArray00</para>
        /// 分類アイテムの選択肢ごとにFAリストイメージ二次元配列を生成して、一次元×二次元のジャグ配列を返す
        /// </summary>
        /// <param name="FAItems">リスト化するFAアイテムの質問情報を保持するQuestionInformation構造体を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="addedItems">付加アイテムの質問情報を保持するQuestionInformation構造体を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyItem">分類アイテムの質問情報を保持するQuestionInformation構造体</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="AddedItemsQTypeBuffer">付加アイテムの質問タイプを表す数字の羅列 (戻り値)</param>
        /// <param name="FilteringFlag">絞り込みフラグ配列 (省略可能)</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetFAListArray(List<QuestionInformation> FAItems, List<QuestionInformation> addedItems, QuestionInformation? keyItem
                  , out string[][,] resultArray, out string AddedItemsQTypeBuffer, bool[] FilteringFlag = null)
        {
            // 戻り値の初期化
            resultArray = null;
            AddedItemsQTypeBuffer = null;
            // 引数のチェック
            int FAItemsCount = 0;
            int addedItemsCount = 0;
            int keyQsectorsCount = 0;
            int dataCount = 0;
            QCWebException exception = null;
            if (!checkGetFAListArrayArguments(FAItems, ref FAItemsCount
                                            , addedItems, ref addedItemsCount
                                            , keyItem, ref keyQsectorsCount
                                            , ref dataCount, ref FilteringFlag, out exception))
            {
                return exception;
            }
            QuestionInformation keyitem;

            // 質問タイプのシンプル化
            List<QuestionType> addedItemsQType = null;
            if (addedItemsCount > 0)
            {
                addedItemsQType = new List<QuestionType>(addedItemsCount);
                for (int i = 0; i < addedItemsCount; ++i)
                {
                    //addedItemsQType[i] = addedItems[i].QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA);
                    addedItemsQType.Add(addedItems[i].QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA));
                }
                AddedItemsQTypeBuffer = string.Join(string.Empty, Array.ConvertAll<QuestionType, string>(addedItemsQType.ToArray(), x => ((int)x).ToString()));
            }
            QuestionType keyQType = (QuestionType)0;
            if (keyQsectorsCount > 0)
            {
                keyitem = (QuestionInformation)keyItem;
                keyQType = keyitem.QuestionType & (QuestionType.SA | QuestionType.MA);
            }

            // 結果の配列のサイズ決定と見出し部分の投入
            int k = keyQsectorsCount == 0 ? 1 : keyQsectorsCount;
            resultArray = new string[k][,];
            // 生成過程で使用する一時ジャグ配列
            // リサイズを極力行わないため最大サイズで定義 (メモリ不足などが発生する場合は要修正)
            string[][][] tmpArray = new string[k][][];
            // 最終成果物の配列におけるデータ投入数を確保する配列
            int[] inputedCount = new int[k];
            for (int i = 0; i < k; ++i)
            {
                tmpArray[i] = new string[dataCount + 1][];
                inputedCount[i] = 0;
                for (int j = 0; j <= dataCount; ++j)
                {
                    tmpArray[i][j] = new string[FAItemsCount + addedItemsCount];
                }
                for (int j = 0; j < FAItemsCount; ++j)
                {
                    //tmpArray[i][0][j] = FAItems[j].Description; original code commented
                    //edited for FAList.cs
                    if (FAItems[j].TableHeading == string.Empty || FAItems[j].TableHeading == null)
                        tmpArray[i][0][j] = FAItems[j].Description;
                    else
                        tmpArray[i][0][j] = FAItems[j].TableHeading + "\n" + "【" + FAItems[j].Description + "】";
                }
                for (int j = 0; j < addedItemsCount; ++j)
                {
                    //tmpArray[i][0][FAItemsCount + j] = addedItems[j].Description; // original code commented
                    //edited for FAList.cs
                    if (addedItems[j].TableHeading == string.Empty || addedItems[j].TableHeading == null)
                        tmpArray[i][0][FAItemsCount + j] = addedItems[j].Description;
                    else
                        tmpArray[i][0][FAItemsCount + j] = addedItems[j].TableHeading + "\n" + "【" + addedItems[j].Description + "】";
                }
            }

            // データ走査
            for (int i = 0; i < dataCount; ++i)
            {
                if (FAItems[0].Datas[i] == null || FAItems[0].Datas[i].IsDeleted || !FilteringFlag[i]) continue;
                bool isAllBlank = true;
                string[] tmpDataArray = new string[FAItemsCount];
                for (int j = 0; j < FAItemsCount; ++j)
                {
                    //if (!FAItems[j].Datas[i].IsNA) original
                    if ((FAItems[j].Datas[i] != null) && !FAItems[j].Datas[i].IsNA) //edited for FAList.cs
                    {
                        tmpDataArray[j] = (FAItems[j].Datas[i] as FAData).Value.Trim();
                        //tmpDataArray[j] = (FAItems[j].Datas[i] as FAData).Value;
                        if (isAllBlank) isAllBlank = false;
                    }
                }
                if (isAllBlank) continue;
                List<int> keyIdx = new List<int>();
                if (keyQsectorsCount == 0)   // 分類アイテムなし
                {
                    keyIdx.Add(0);
                }
                else    // 分類アイテムあり
                {
                    keyitem = (QuestionInformation)keyItem;
                    if (keyitem.Datas[i].DataType == DataType.NormalData)
                    {
                        if (keyQType == QuestionType.SA)    // SA
                        {
                            int n = (keyitem.Datas[i] as SAData).Value;
                            if (n >= 1 && n <= keyQsectorsCount)
                            {
                                keyIdx.Add(n - 1);
                            }
                        }
                        else    // MA
                        {
                            for (int j = 0; j < keyQsectorsCount; ++j)
                            {
                                int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                if (((keyitem.Datas[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                {
                                    int n = j + 1;
                                    keyIdx.Add(n - 1);
                                }
                            }
                        }
                    }
                }
                if (keyIdx.Count == 0) continue;
                for (int x = 0; x < keyIdx.Count; ++x)
                {
                    ++inputedCount[keyIdx[x]];
                    for (int j = 0; j < FAItemsCount; ++j)
                    {
                        tmpArray[keyIdx[x]][inputedCount[keyIdx[x]]][j] = tmpDataArray[j];
                    }
                    for (int j = 0; j < addedItemsCount; ++j)
                    {
                        List<string> addedItemDescription = new List<string>();
                        if (addedItems[j].Datas[i] != null)//if stmt added for FAList.cs
                        {
                            switch (addedItems[j].Datas[i].DataType)
                            {
                                case DataType.NAData:
                                    addedItemDescription.Add("");
                                    break;
                                case DataType.IVData:
                                    addedItemDescription.Add("*");
                                    break;
                                case DataType.NormalData:
                                    switch (addedItemsQType[j])
                                    {
                                        case QuestionType.SA:
                                            {
                                                int num = (addedItems[j].Datas[i] as SAData).Value;
                                                if (num >= 1 && num <= addedItems[j].SectorInformations.Count)
                                                {
                                                    int n = (int)Math.Floor(Math.Log10(addedItems[j].SectorInformations.Count)) + 1;
                                                    if (n < 2) n = 2;
                                                    string fmt = new string('0', n);
                                                    addedItemDescription.Add(num.ToString(fmt) + "." + addedItems[j].SectorInformations[num - 1].Description);
                                                }
                                                break;
                                            }
                                        case QuestionType.MA:
                                            {
                                                int n = (int)Math.Floor(Math.Log10(addedItems[j].SectorInformations.Count)) + 1;
                                                if (n < 2) n = 2;
                                                string fmt = new string('0', n);
                                                for (int m = 0; m < addedItems[j].SectorInformations.Count; ++m)
                                                {
                                                    int idx = m / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                                    int e = m % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                                    if (((addedItems[j].Datas[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                                    {
                                                        int num = m + 1;
                                                        addedItemDescription.Add(num.ToString(fmt) + "." + addedItems[j].SectorInformations[num - 1].Description);
                                                    }
                                                }
                                                break;
                                            }
                                        case QuestionType.FA:
                                            {
                                                addedItemDescription.Add((addedItems[j].Datas[i] as FAData).Value);
                                                break;
                                            }
                                        case QuestionType.N:
                                            {
                                                addedItemDescription.Add((addedItems[j].Datas[i] as NData).Value.ToString());
                                                break;
                                            }
                                    }
                                    break;
                            }
                        }
                        tmpArray[keyIdx[x]][inputedCount[keyIdx[x]]][FAItemsCount + j] = string.Join(" / ", addedItemDescription.ToArray<string>());
                    }
                }
            }

            // 戻り値の配列に投入
            for (int x = 0; x < k; ++x)
            {
                resultArray[x] = new string[inputedCount[x] + 1, FAItemsCount + addedItemsCount];
                for (int i = 0; i <= inputedCount[x]; ++i)
                {
                    for (int j = 0; j < FAItemsCount + addedItemsCount; ++j)
                    {
                        resultArray[x][i, j] = tmpArray[x][i][j];
                    }
                }
            }
            return null;
        }

        /// <alias>getFAListArray01</alias>
        /// <summary>
        /// <para>エイリアス:getFAListArray01</para>
        /// 分類アイテムの選択肢ごとにFAリストイメージ二次元配列を生成して、一次元×二次元のジャグ配列を返す<br />
        /// 付加アイテムがない場合に使用
        /// </summary>
        /// <param name="FAItems">リスト化するFAアイテムの質問情報を保持するQuestionInformation構造体を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="keyItem">分類アイテムの質問情報を保持するQuestionInformation構造体</param>
        /// <param name="resultArray">集計結果の一次元×二次元のジャグ配列 (戻り値)</param>
        /// <param name="AddedItemsQTypeBuffer">付加アイテムの質問タイプを表す数字の羅列 (戻り値)</param>
        /// <param name="FilteringFlag">絞り込みフラグ配列 (省略可能)</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetFAListArray(List<QuestionInformation> FAItems, QuestionInformation keyItem
                  , out string[][,] resultArray, out string AddedItemsQTypeBuffer, bool[] FilteringFlag = null)
        {
            return GetFAListArray(FAItems, null, keyItem, out resultArray, out AddedItemsQTypeBuffer, FilteringFlag);
        }

        /// <alias>getFAListArray10</alias>
        /// <summary>
        /// <para>エイリアス:getFAListArray10</para>
        /// FAリストイメージ二次元配列を生成して返す<br />
        /// 分類アイテムの指定がない場合に、getFAListArray00の戻り値resultArrayの1つ目の要素を返す
        /// </summary>
        /// <param name="FAItems">リスト化するFAアイテムの質問情報を保持するQuestionInformation構造体を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="addedItems">付加アイテムの質問情報を保持するQuestionInformation構造体を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="AddedItemsQTypeBuffer">付加アイテムの質問タイプを表す数字の羅列 (戻り値)</param>
        /// <param name="FilteringFlag">絞り込みフラグ配列 (省略可能)</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetFAListArray(List<QuestionInformation> FAItems, List<QuestionInformation> addedItems
                  , out string[,] resultArray, out string AddedItemsQTypeBuffer, bool[] FilteringFlag = null)
        {
            string[][,] res = null;
            QCWebException exception = GetFAListArray(FAItems, addedItems, null, out res, out AddedItemsQTypeBuffer, FilteringFlag);
            resultArray = null;
            if (exception != null) return exception;
            if (res != null && res.Length == 1) resultArray = res[0];
            return null;
        }

        /// <alias>getFAListArray11</alias>
        /// <summary>
        /// <para>エイリアス:getFAListArray11</para>
        /// FAリストイメージ二次元配列を生成して返す<br />
        /// 分類アイテムおよび付加アイテムがない場合に使用
        /// </summary>
        /// <param name="FAItems">リスト化するFAアイテムの質問情報を保持するQuestionInformation構造体を要素とするListクラスのインスタンスへの参照</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="AddedItemsQTypeBuffer">付加アイテムの質問タイプを表す数字の羅列 (戻り値)</param>
        /// <param name="FilteringFlag">絞り込みフラグ配列 (省略可能)</param>
        public static QCWebException GetFAListArray(List<QuestionInformation> FAItems
                  , out string[,] resultArray, out string AddedItemsQTypeBuffer, bool[] FilteringFlag = null)
        {
            return GetFAListArray(FAItems, null, out resultArray, out AddedItemsQTypeBuffer, FilteringFlag);
        }
        #endregion
    }
    #endregion

    #region CheckListTabulationクラス
    /// <summary>
    /// チェックリストの作成に必要なメソッドをまとめた静的クラス
    /// </summary>
    [ComVisible(false), Guid("CC5B8BA9-C35C-434E-8632-5E1A251D72B1")]
    public static class CheckListTabulation
    {
        #region チェックリストGT表生成関連
        #region 引数チェック
        private static bool checkGetCheckListGTArrayArguments(
                    QuestionType questionType, List<Data> originalData, List<Data> newData
                  , ref string[] sectorDescription, ref int dataCount, ref int sectorsCount
                  , out QCWebException exception)
        {
            exception = null;
            // マトリクスはNG
            if ((questionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustTabulationSubjectItemQuestionTypeMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , questionType.ToString());
                return false;
            }
            // データ情報を保持したListオブジェクトがなければNG
            if (newData == null || newData.Count == 0)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyTabulationSubjectItemDataMessageIndex));
                return false;
            }
            dataCount = newData.Count;
            if (originalData != null && originalData.Count != dataCount)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.ExistMismatchBetweenDatasMessageIndex));
                return false;
            }
            QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA | QuestionType.N);
            switch (qType)
            {
                case QuestionType.SA:
                case QuestionType.MA:
                    // 選択肢情報がなければNG
                    if (sectorDescription == null || (sectorsCount = sectorDescription.Length) == 0)
                    {
                        exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptySectorInformationMessageIndex));
                        return false;
                    }
                    break;
                case QuestionType.N:
                    break;
                default:
                    // SAでもMAでもNでもなければNG
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyTabulationSubjectItemDataMessageIndex));
                    return false;
            }
            return true;
        }
        #endregion

        #region 集計表配列生成
        /// <summary>
        /// チェックリストGT表イメージ二次元配列を生成する
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="originalData">データ加工前のデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="newData">最新のデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="withPercent">
        /// ％値を算出して結果に含めるかどうかを示すフラグ (省略可、既定値false)
        /// <note>
        /// このフラグはチェックリストGT表イメージの作成では指定不要<br />
        /// 参考データ表示などでこのメソッドを使用する場合にtrueを指定する<br />
        /// また、このフラグがtrueのときには、数値回答項目の集計項目すべてを算出し、originalDataを無視する
        /// </note>
        /// </param>
        /// <param name="locale">多言語情報取得用情報(省略可、規定値ja)</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetCheckListGTArray(QuestionType questionType
                , List<Data> originalData, List<Data> newData, string[] sectorDescription
                , out string[,] resultArray, TabulationDescriptions tabulationDescriptions, bool withPercent = false, string locale = "ja")
        {
            // 戻り値の初期化
            resultArray = null;
            // 引数のチェック
            int dataCount = 0;
            int sectorsCount = 0;
            if (withPercent) originalData = null;
            QCWebException exception = null;
            if (!checkGetCheckListGTArrayArguments(questionType, originalData, newData
                    , ref sectorDescription, ref dataCount, ref sectorsCount, out exception))
            {
                return exception;
            }

            // 質問タイプのシンプル化
            QuestionType qType = questionType & (QuestionType.SA | QuestionType.MA | QuestionType.N);
            // 新質問かどうかを示すフラグ
            bool isnewitem = originalData == null;
            // 結果の配列のサイズ決定と見出し部分の投入
            /*
            switch (qType)
            {
                case QuestionType.SA:   // SA
                case QuestionType.MA:   // MA
                    resultArray = new string[1 + (withPercent ? 2 : 1) * (isnewitem ? 1 : 2), 1 + sectorsCount + 2];
                    resultArray[0, 0] = "全体";    // リソース読み込み
                    for (int i = 0; i < sectorsCount; ++i)
                    {
                        resultArray[0, 1 + i] = (i + 1).ToString() + "." + sectorDescription[i];
                    }
                    resultArray[0, sectorsCount + 1] = "無回答";   // リソース読み込み
                    resultArray[0, sectorsCount + 2] = "非該当";   // リソース読み込み
                    break;
                case QuestionType.N:    // N
                    resultArray = new string[isnewitem ? 2 : 3, withPercent ? 10 : 8];
                    resultArray[0, 0] = "全体";   // リソース読み込み
                    int idx = withPercent ? 1 : 0;
                    resultArray[0, ++idx] = withPercent ? "総和" : "合計";    // リソース読み込み
                    resultArray[0, ++idx] = "平均";   // リソース読み込み
                    resultArray[0, ++idx] = "標準偏差"; // リソース読み込み
                    resultArray[0, ++idx] = "最小値";  // リソース読み込み
                    resultArray[0, ++idx] = "最大値";  // リソース読み込み
                    if (withPercent)
                    {
                        resultArray[0, 1] = "統計量母数";    // リソース読み込み
                        resultArray[0, ++idx] = "中央値";  // リソース読み込み
                    }
                    resultArray[0, ++idx] = "無回答";  // リソース読み込み
                    resultArray[0, ++idx] = "非該当";  // リソース読み込み
                    break;
            }
            */
            int NAIdx = 0;
            int IVIdx = 0;
            int StartIdx = 0;
            switch (qType)
            {
                case QuestionType.SA:   // SA
                case QuestionType.MA:   // MA
                    resultArray = new string[1 + (withPercent ? 2 : 1) * (isnewitem ? 1 : 2), 1 + sectorsCount + 2];
                    break;
                case QuestionType.N:    // N
                    resultArray = new string[isnewitem ? 2 : 3, withPercent ? 10 : 9];
                    break;
            }

            // TODO:Const化[QCR0000026]全体
            // resultArray[0, 0] = GetResource.GetCommonResourceData("QCR0000026", locale);
            resultArray[0, 0] = tabulationDescriptions.TotalAxisDescription;//GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportWholeKeywordIndex, locale);
            if (withPercent)    // 参考データ表示
            {
                NAIdx = resultArray.GetUpperBound(1) - 1;
                IVIdx = resultArray.GetUpperBound(1);
                StartIdx = 1;
            }
            else    // チェックリスト
            {
                NAIdx = 1;
                IVIdx = 2;
                StartIdx = 3;
            }

            // TODO:Const化[QCR0000027]無回答
            // resultArray[0, NAIdx] = GetResource.GetCommonResourceData("QCR0000027", locale);
            resultArray[0, NAIdx] = tabulationDescriptions.NADescription;//GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportNADescriptionKeywordIndex, locale);
            // TODO:Const化[QCR0000028]非該当
            // resultArray[0, IVIdx] = GetResource.GetCommonResourceData("QCR0000028", locale);
            resultArray[0, IVIdx] = tabulationDescriptions.IVDescription;//GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportIVDescriptionKeywordIndex, locale);
            switch (qType)
            {
                case QuestionType.SA:   // SA
                case QuestionType.MA:   // MA
                    for (int i = 0; i < sectorsCount; ++i)
                    {
                        resultArray[0, StartIdx + i] = (i + 1).ToString() + "." + sectorDescription[i];
                    }
                    break;
                case QuestionType.N:    // N
                    int idx = StartIdx - 1;
                    if (withPercent)
                    {
                        // TODO:Const化[QCR0000029]統計量母数
                        // resultArray[0, ++idx] = GetResource.GetCommonResourceData("QCR0000029", locale);
                        resultArray[0, ++idx] = tabulationDescriptions.ParameterDescription; //GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportParameterDescriptionKeywordIndex, locale);
                    }
                    // TODO:Const化[QCR0000030]合計
                    // resultArray[0, ++idx] = GetResource.GetCommonResourceData("QCR0000030", locale);
                    resultArray[0, ++idx] = tabulationDescriptions.SummaryDescription;//GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportSummaryDescriptionKeywordIndex, locale);
                    // TODO:Const化[QCR0000031]平均
                    // resultArray[0, ++idx] = GetResource.GetCommonResourceData("QCR0000031", locale);
                    resultArray[0, ++idx] = tabulationDescriptions.AverageDescription;//GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportAverageDescriptionKeywordIndex, locale);
                    // TODO:Const化[QCR0000032]標準偏差
                    // resultArray[0, ++idx] = GetResource.GetCommonResourceData("QCR0000032", locale);
                    resultArray[0, ++idx] = tabulationDescriptions.StdevDescription;//GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportDeviationDescriptionKeywordIndex, locale);
                    // TODO:Const化[QCR0000033]最小値
                    // resultArray[0, ++idx] = GetResource.GetCommonResourceData("QCR0000033", locale);
                    resultArray[0, ++idx] = tabulationDescriptions.MinDescription;//GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMinimumDescriptionKeywordIndex, locale);
                    // TODO:Const化[QCR0000034]最大値
                    // resultArray[0, ++idx] = GetResource.GetCommonResourceData("QCR0000034", locale);
                    resultArray[0, ++idx] = tabulationDescriptions.MaxDescription;//GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMaximumDescriptionKeywordIndex, locale);
                    // TODO:Const化[QCR0000035]中央値
                    // resultArray[0, ++idx] = GetResource.GetCommonResourceData("QCR0000035", locale);
                    resultArray[0, ++idx] = tabulationDescriptions.MedianDescription;//GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportMedianDescriptionKeywordIndex, locale);
                    break;
            }

            // 集計
            for (int x = GlobalMethodClass.CInt(isnewitem) & 1; x < 2; ++x)
            {
                // 三項演算だと値(コピー)で返しそうなのでやめる
                // List<Data> data = x == 0 ? originalData : newData;  // 集計するデータ
                List<Data> data = null;
                if (x == 0) data = originalData; else data = newData;
                double[] nArray = null; // 集計値を格納する配列
                // N質問集計時に、最大値, 最小値, 中央値を出すための配列
                double[] normalDatas = null;
                int lastIndex = -1; // normalDatasの使用最大インデックス
                // 集計値を格納する配列のサイズ定義
                switch (qType)
                {
                    case QuestionType.SA:   // SA
                    case QuestionType.MA:   // MA
                        nArray = new double[1 + sectorsCount + 2];
                        break;
                    case QuestionType.N:    // N
                        nArray = new double[6];
                        normalDatas = new double[dataCount];
                        break;
                }
                // データ走査
                for (int i = 0; i < dataCount; ++i)
                {
                    if (data[i].IsDeleted) continue;
                    // // 全体 (WB無視)
                    // ++nArray[0];
                    switch (data[i].DataType)
                    {
                        case DataType.NAData:   // 無回答
                            // 全体
                            ++nArray[0];
                            ++nArray[nArray.GetUpperBound(0) - 1];
                            break;
                        case DataType.IVData:   // 非該当
                            if (withPercent) ++nArray[0];   // 全数ベース集計
                            ++nArray[nArray.GetUpperBound(0)];
                            break;
                        case DataType.NormalData:   // 通常データ
                            // 全体
                            ++nArray[0];
                            switch (qType)
                            {
                                case QuestionType.SA:   // SA
                                    int n = (data[i] as SAData).Value;
                                    if (n >= 1 && n <= sectorsCount)
                                    {
                                        ++nArray[n];    // 該当する選択肢
                                    }
                                    else
                                    {
                                        ++nArray[sectorsCount + 1]; // 無回答扱い
                                    }
                                    break;
                                case QuestionType.MA:   // MA
                                    for (int m = 0; m < sectorsCount; ++m)
                                    {
                                        int idx = m / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                        int e = m % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                        if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                        {
                                            n = m + 1;
                                            ++nArray[n];    // 該当する選択肢
                                        }
                                    }
                                    break;
                                case QuestionType.N:    // N
                                    double v = (data[i] as NData).Value;
                                    // 値を個別に確保
                                    normalDatas[++lastIndex] = v;
                                    // 統計量母数
                                    ++nArray[1];
                                    // 合計
                                    nArray[2] += v;
                                    // 平方の合計
                                    nArray[3] += Math.Pow(v, 2.0);
                                    break;
                            }
                            break;
                    }
                }
                // normalDatasのソート (クイックソート)
                if (qType == QuestionType.N && lastIndex > 0)
                {
                    Array.Sort<double>(normalDatas, 0, lastIndex + 1);
                }
                // 出力配列の仕上げ
                switch (qType)
                {
                    case QuestionType.SA:   // SA
                    case QuestionType.MA:   // MA
                        // N値の投入
                        int rowIdx = 1 + (isnewitem ? x - 1 : x) * (withPercent ? 2 : 1);
                        /*
                        for (int j = 0; j < nArray.Length; ++j)
                        {
                            resultArray[rowIdx, j] = nArray[j].ToString();
                        }
                        */
                        // 全体
                        resultArray[rowIdx, 0] = nArray[0].ToString();
                        // 各選択肢
                        for (int j = 1; j < nArray.Length - 2; ++j)
                        {
                            resultArray[rowIdx, StartIdx - 1 + j] = nArray[j].ToString();
                        }
                        // 無回答
                        resultArray[rowIdx, NAIdx] = nArray[nArray.GetUpperBound(0) - 1].ToString();
                        // 非該当
                        resultArray[rowIdx, IVIdx] = nArray[nArray.GetUpperBound(0)].ToString();
                        if (withPercent)
                        {
                            // ％値の投入
                            rowIdx = 2 + (isnewitem ? x - 1 : x) * (withPercent ? 2 : 1);
                            if (nArray[0] == 0.0)
                            {
                                for (int j = 1; j < nArray.Length; ++j)
                                {
                                    resultArray[rowIdx, j] = "0.0";
                                }
                            }
                            else
                            {
                                /*
                                for (int j = 1; j < nArray.Length; ++j)
                                {
                                    resultArray[rowIdx, j] = (nArray[j] / nArray[0]).ToString();
                                }
                                */
                                // 各選択肢
                                for (int j = 1; j < nArray.Length - 2; ++j)
                                {
                                    resultArray[rowIdx, StartIdx - 1 + j] = (nArray[j] * 100 / nArray[0]).ToString();
                                }
                                // 無回答
                                resultArray[rowIdx, NAIdx] = (nArray[nArray.GetUpperBound(0) - 1] * 100.0 / nArray[0]).ToString();
                                // 非該当
                                resultArray[rowIdx, IVIdx] = (nArray[nArray.GetUpperBound(0)] * 100.0 / nArray[0]).ToString();
                            }
                        }
                        break;
                    case QuestionType.N:    // N
                        rowIdx = 1 + (isnewitem ? x - 1 : x);
                        resultArray[rowIdx, 0] = nArray[0].ToString();  // 全体
                        int clmIdx = StartIdx;
                        // resultArray[rowIdx, withPercent ? ++clmIdx : clmIdx] = nArray[2].ToString();   // 合計
                        resultArray[rowIdx, withPercent ? ++clmIdx : clmIdx] = nArray[1] == 0.0 ? "-" : nArray[2].ToString();   // 合計
                        // 平均
                        // double average = 0.0;
                        double average = double.NaN;
                        if (nArray[1] != 0.0)
                        {
                            average = nArray[2] / nArray[1];    // 合計÷統計量母数
                        }
                        // resultArray[rowIdx, ++clmIdx] = average.ToString();
                        resultArray[rowIdx, ++clmIdx] = double.IsNaN(average) ? "-" : average.ToString();
                        // 標準偏差
                        // double stdev = 0.0;
                        double stdev = double.NaN;
                        if (nArray[1] > 1.0)
                        {
                            // 統計量母数×平方の合計－合計の平方
                            //double tmp = nArray[1] * nArray[3] - Math.Pow(nArray[2], 2.0);
                            //tmp /= nArray[1] * (nArray[1] - 1);
                            //stdev = Math.Sqrt(tmp);

                            double standardDeviation = 0;
                            double WeightedMean = nArray[2] / nArray[1];
                            double weightBack = 1;
                            for (int j = 0; j < data.Count; j++)
                            {
                                if (!data[j].IsNormal) continue;
                                standardDeviation += weightBack * Math.Pow(((data[j] as NData).Value - WeightedMean), 2.0);
                            }
                            standardDeviation = Math.Sqrt(standardDeviation / (((nArray[1] - 1) / nArray[1]) * nArray[1]));
                            stdev = standardDeviation;

                        }
                        // resultArray[rowIdx, ++clmIdx] = stdev.ToString();
                        resultArray[rowIdx, ++clmIdx] = double.IsNaN(stdev) ? "-" : stdev.ToString();
                        // 最小値
                        // double min = 0.0;
                        double min = double.NaN;
                        // 最大値
                        // double max = 0.0;
                        double max = double.NaN;
                        if (lastIndex > -1)
                        {
                            min = normalDatas[0];
                            max = normalDatas[lastIndex];
                        }
                        // resultArray[rowIdx, ++clmIdx] = min.ToString();
                        // resultArray[rowIdx, ++clmIdx] = max.ToString();
                        resultArray[rowIdx, ++clmIdx] = double.IsNaN(min) ? "-" : min.ToString();
                        resultArray[rowIdx, ++clmIdx] = double.IsNaN(max) ? "-" : max.ToString();
                        // 中央値
                        // double median = 0.0;
                        double median = double.NaN;
                        if (lastIndex > -1)
                        {
                            /*
                            int medIdx = lastIndex / 2;
                            //if (lastIndex % 2 == 0) // 要素数が奇数
                            //{
                            //    median = normalDatas[medIdx];
                            //}
                            //else    // 要素数が偶数
                            //{
                            //    median = (normalDatas[medIdx] + normalDatas[medIdx + 1]) / 2.0;
                            //}
                            median = normalDatas[medIdx];
                            double medPos = (double)lastIndex / 2.0;
                            if (medPos > (double)medIdx)
                            {
                                double d = (normalDatas[medIdx + 1] - normalDatas[medIdx]) * (medPos - (double)medIdx);
                                median += d;
                            }
                            */
                            median = GlobalTabulation.GetMedian(normalDatas, lastIndex);
                            // resultArray[rowIdx, ++clmIdx] = median.ToString();
                        }
                        resultArray[rowIdx, ++clmIdx] = double.IsNaN(median) ? "-" : median.ToString();

                        if (withPercent)
                        {
                            // resultArray[rowIdx, 1] = nArray[1].ToString();  // 統計量母数
                            resultArray[rowIdx, StartIdx] = nArray[1].ToString();   // 統計量母数
                            /*
                            // 中央値
                            double median = 0.0;
                            if (lastIndex > -1)
                            {
                                int medIdx = lastIndex / 2;
                                if (lastIndex % 2 == 0) // 要素数が奇数
                                {
                                    median = normalDatas[medIdx];
                                }
                                else    // 要素数が偶数
                                {
                                    median = (normalDatas[medIdx] + normalDatas[medIdx + 1]) / 2.0;
                                }
                                resultArray[rowIdx, ++clmIdx] = median.ToString();
                            }
                            */
                        }
                        /*
                        resultArray[rowIdx, ++clmIdx] = nArray[4].ToString();   // 無回答
                        resultArray[rowIdx, ++clmIdx] = nArray[5].ToString();   // 非該当
                        */
                        resultArray[rowIdx, NAIdx] = nArray[4].ToString();  // 無回答
                        resultArray[rowIdx, IVIdx] = nArray[5].ToString();  // 非該当
                        break;
                }
            }
            return null;
        }

        /// <summary>
        /// チェックリストGT表イメージ二次元配列を生成する<br />
        /// 最新の集計結果のみ必要な場合に使用する
        /// </summary>
        /// <param name="questionType">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="newData">最新のデータを保持したDataクラスのインスタンスを要素とするListクラスのインスタンスへの参照</param>
        /// <param name="sectorDescription">選択肢文を要素とする配列</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="withPercent">
        /// ％値を算出して結果に含めるかどうかを示すフラグ (省略可、既定値false)
        /// <note>
        /// このフラグはチェックリストGT表イメージの作成では指定不要<br />
        /// 参考データ表示などでこのメソッドを使用する場合にtrueを指定する<br />
        /// また、このフラグがtrueのときには、数値回答項目の集計項目すべてを算出する
        /// </note>
        /// </param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetCheckListGTArray(QuestionType questionType
                , List<Data> newData, string[] sectorDescription
                , out string[,] resultArray, bool withPercent = false)
        {
            return GetCheckListGTArray(questionType, null, newData, sectorDescription
                    , out resultArray, null, withPercent);
        }

        /// <summary>
        /// チェックリストGT表イメージ二次元配列を生成する<br />
        /// 最新の集計結果のみ必要な場合に使用する
        /// </summary>
        /// <param name="question">集計する質問のQuestionクラスのインスタンスへの参照</param>
        /// <param name="dirpath">データファイルや削除フラグファイルを出力するディレクトリパス</param>
        /// <param name="resultArray">集計結果の二次元配列 (戻り値)</param>
        /// <param name="withPercent">
        /// このフラグはチェックリストGT表イメージの作成では指定不要<br />
        /// 参考データ表示などでこのメソッドを使用する場合にtrueを指定する<br />
        /// また、このフラグがtrueのときには、数値回答項目の集計項目すべてを算出する
        /// </param>
        /// <param name="isUseDataProcess">データ加工で利用する場合はtrue、集計で利用する場合はfalse（省略可、既定値false）</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetCheckListGTArray(Question.Questions.Question question
                , string dirpath
                , out string[,] resultArray, bool withPercent = false, bool isUseDataProcess = false)
        {
            resultArray = null;
            if (question == null)
            {
                return new QCWebException(new Message(Constants.CommonMessageIndex.OutputItemInformationIsNullMessageIndex));
            }

            // ローデータTXTパスが未設定の場合
            if (string.IsNullOrEmpty(dirpath))
            {
                ReadDBInfo readDBInfo = new ReadDBInfo();
                QuillInjector.GetInstance().Inject(readDBInfo);
                dirpath = System.IO.Path.Combine(readDBInfo.GetRawdataPath(), question.QCWebID.ToString());
            }

            QuestionType questionType = question.QuestionType;
            if ((questionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                return new QCWebException(new Message(Constants.CommonMessageIndex.UnjustTabulationSubjectItemQuestionTypeMessageIndex)
                                        , GlobalsCommonConstant.LogLevel.FATAL
                                        , questionType.ToString());
            }
            //string tablename = question.TableName;
            //string columnname = question.ColumnName;
            //string delflgtablename = question.TopTableName;
            //string delfilepath = null;
            QCWebException exception = null;
            //if (delflgtablename != null)
            //{
            //    if (!CreateTextFile.CreateDeleteFlag(delflgtablename, dirpath, out delfilepath, out exception))
            //    {
            //        return exception;
            //    }
            //    ReadTextFile.ReadDeleteFlag(delfilepath, out exception);
            //    if (exception != null) return exception;
            //}
            //string datafilepath = null;
            //if (!CreateTextFile.CreateData(question.ID, dirpath, out datafilepath, out questionType, out exception))
            //{
            //    return exception;
            //}
            //List<Data> data = ReadTextFile.ReadData(datafilepath, questionType, delfilepath, out exception);
            //if (data == null) return exception;
            string path = string.Empty;
            List<Data> data = null;
            if (isUseDataProcess)
            {
                //デフォルトの削除フラグを読み込むことで削除フラグ情報をデータ加工実行前に戻す
                string delfilepath = System.IO.Path.Combine(dirpath, DataIoConstant.DELETE_FLAG_TEXT_FILE_NAME_DEFAULT);
                //オリジナルが存在する場合はオリジナルを読み込む
                path = System.IO.Path.Combine(dirpath, question.ID + ".txt");
                if (!System.IO.File.Exists(path)) path = System.IO.Path.Combine(dirpath, question.ID + ".dp");
                data = ReadTextFile.ReadData(path, question.QuestionType, delfilepath, out exception, true);
            }
            else
            {
                data = ReadTextFile.ReadData2(question, dirpath, out path, out questionType, out exception, true, isUseDataProcess);
            }
            if (data == null) return exception;
            string[] sectorDescription = null;
            if ((int)(questionType & (QuestionType.SA | QuestionType.MA)) != 0)
            {
                sectorDescription = new string[question.Sectors.Count];
                for (int i = 0; i < question.Sectors.Count; ++i)
                {
                    sectorDescription[i] = question.Sectors[i + 1].Description;
                }
            }
            return GetCheckListGTArray(questionType, data, sectorDescription, out resultArray, withPercent);
        }

        #endregion
        #endregion
    }
    #endregion

    #region RawDataTabulationクラス
    /// <summary>
    /// ローデータ出力時の表作成に必要なメソッドをまとめた静的クラス
    /// </summary>
    [ComVisible(false), Guid("D222493B-A464-495E-B004-B4DE069EDF0B")]
    public static class RawDataTabulation
    {
        #region 引数チェック
        private static bool checkGetRawDataArrayArguments(
                    Questions questions, decimal[] questionids
                  , ref OutputDataType datatype
                  , ref string toptablename, ref int columnsCount
                  , out bool[] divisiblePoint, out QCAnswerType[] columnQCAnsType, out QCWebException exception)
        {
            exception = null;
            divisiblePoint = null;
            columnQCAnsType = null;
            if (questions == null || questions.Count == 0)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyQuestionInformationMessageIndex));
                return false;
            }
            if (questionids == null || questionids.Length == 0)
            {
                // TODO:暫定
                // throw new QCWebException("questions,questionidsのいづれかが不正");
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyOutputItemInformationMessageIndex));
                return false;
            }
            // 不正なデータタイプ指定時はコード形式
            OutputDataType dType = OutputDataType.Code;
            switch (datatype)
            {
                case OutputDataType.Flag:
                case OutputDataType.Decode:
                    dType = datatype;
                    break;
                case OutputDataType.QC3:
                    break;
                default:
                    datatype = OutputDataType.Code;
                    break;
            }
            columnsCount = 0;
            toptablename = null;
            Questions.Question preParentQ = null;
            for (int i = 0; i < questionids.Length; ++i)
            {
                // Questions.Question question = questions[questionids[i], true] as Questions.Question;
                Questions.Question question = questions[questionids[i]] as Questions.Question;
                if (question == null)
                {
                    divisiblePoint = null;
                    columnQCAnsType = null;
                    // TODO:暫定
                    // throw new QCWebException("questionが不正:" + questionids[i]);
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.NotExistOutputItemInformationInQuestionInformationsMessageIndex)
                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                 , questionids[i].ToString());
                    return false;
                }
                if (toptablename == null)
                {
                    toptablename = question.TopTableName;
                    if (string.IsNullOrWhiteSpace(toptablename))
                    {
                        // TODO:暫定
                        // throw new QCWebException("toptablenameが不正");
                        exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrWhiteSpaceHeadTableNameMessageIndex));
                        return false;
                    }
                }
                Questions.Question parentQ = question;
                int cnt = 1;
                if (dType != OutputDataType.Code && (question.QuestionType & QuestionType.MA) == QuestionType.MA)
                {
                    cnt = question.Sectors.Count;
                }
                if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    // マトリクス
                    cnt *= question.ChildQuestions.Count;
                }
                else if ((question.QuestionType & QuestionType.MatrixChild) == QuestionType.MatrixChild)
                {
                    parentQ = question.ParentQuestion as Questions.Question;
                }
                else if ((question.QuestionType & QuestionType.FA_Sub) == QuestionType.FA_Sub)
                {
                    parentQ = question.ParentSector.ParentQuestion as Questions.Question;
                }
                Array.Resize<bool>(ref divisiblePoint, columnsCount += cnt);
                Array.Resize<QCAnswerType>(ref columnQCAnsType, columnsCount);
                divisiblePoint[columnsCount - cnt] = !parentQ.Equals(preParentQ);
                if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    for (int c = 1, j = columnsCount - cnt; c <= question.ChildQuestions.Count; ++c, ++j)
                    {
                        Questions.Question childQ = question.ChildQuestions[c] as Questions.Question;
                        columnQCAnsType[j] = childQ.QCAnswerType;
                    }
                }
                else
                {
                    OperateArray.InitializeWith<QCAnswerType>(ref columnQCAnsType, question.QCAnswerType
                                                    , columnsCount - cnt, columnsCount - 1);
                }
                preParentQ = parentQ;
            }
            return true;
        }

        private const int MAX_SECTORS_COUNT_PER_ROW = 200;
        private const int MAX_SECTORS_COUNT = 600;

        private static bool checkGetLayoutArrayArguments(
                    Questions questions, decimal[] questionids, int secCountPerRow
                  , ref int rowsCount, out QCWebException exception
                  , bool isVertical = false)
        {
            exception = null;
            if (questions == null || questions.Count == 0)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyQuestionInformationMessageIndex));
                return false;
            }
            if (questionids == null || questionids.Length == 0)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyOutputItemInformationMessageIndex));
                return false;
            }
            rowsCount = 1;
            Questions.Question preParentQ = null;
            for (int i = 0; i < questionids.Length; ++i)
            {
                Questions.Question question = questions[questionids[i]] as Questions.Question;
                if (question == null)
                {
                    exception = new QCWebException(new Message(Constants.CommonMessageIndex.NotExistOutputItemInformationInQuestionInformationsMessageIndex)
                                                 , GlobalsCommonConstant.LogLevel.FATAL
                                                 , questionids[i].ToString());
                    return false;
                }
                int cnt = 1;
                if ((int)(question.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                {
                    if (isVertical)
                    {
                        cnt += question.Sectors.Count;
                    }
                    else
                    {
                        // cnt = (question.Sectors.Count - 1) / MAX_SECTORS_COUNT_PER_ROW + 1;
                        cnt = (question.Sectors.Count - 1) / secCountPerRow + 1;
                    }
                }
                if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    // マトリクス
                    cnt *= question.ChildQuestions.Count;
                    if (isVertical) ++cnt;
                }
                else if (isVertical && (question.QuestionType & QuestionType.MatrixChild) == QuestionType.MatrixChild)
                {
                    Questions.Question parentQ = question.ParentQuestion as Questions.Question;
                    if (parentQ != null)
                    {
                        if (parentQ != preParentQ)
                        {
                            ++cnt;
                            preParentQ = parentQ;
                        }
                    }
                }
                rowsCount += cnt;
            }
            return true;
        }

        #endregion

        #region ローデータ表生成
        private static bool putData(Questions.Question question, OutputDataType datatype
                           , int columnsCount, string dirpath, string NALetter, string IVLetter
                           , ref int dataCount, ref int allDataCount, ref string[,] puttoarray, ref int clmIdx
                           , out QCWebException exception, ref bool[] filteringFlag)
        {
            exception = null;
            if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                foreach (DictionaryEntry de in question.ChildQuestions)
                {
                    Questions.Question childQ = de.Value as Questions.Question;
                    if (!putData(childQ, datatype, columnsCount, dirpath, NALetter, IVLetter
                            , ref dataCount, ref allDataCount, ref puttoarray, ref clmIdx, out exception, ref filteringFlag))
                    {
                        return false;
                    }
                }
                return true;
            }
            QuestionType qType = question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA);
            if ((int)qType == 0)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustQuestionTypeMessageIndex));
                return false;
            }
            string datapath = null;
            //if (!CreateTextFile.CreateData(question.ID, dirpath, out datapath))
            //if (!CreateTextFile.CreateData(question, dirpath, out datapath, out exception))
            //{
            //    // エラースロー
            //    return false;
            //}
            //List<Data> data = ReadTextFile.ReadData(datapath, question.QuestionType, out exception);
            QuestionType questionType;
            List<Data> data = ReadTextFile.ReadData2(question, dirpath, out datapath, out questionType, out exception);
            if (data == null) return false;
            if (clmIdx == -1)
            {
                allDataCount = data.Count;
                int l = filteringFlag == null ? 0 : filteringFlag.Length;
                if (filteringFlag == null)
                {
                    filteringFlag = new bool[allDataCount];
                }
                if (l != allDataCount)
                {
                    Array.Resize<bool>(ref filteringFlag, allDataCount);
                    if (l < allDataCount) OperateArray.InitializeWith<bool>(ref filteringFlag, true, l, allDataCount - 1);
                }
                dataCount = 1;
                for (int i = 0; i < allDataCount; ++i)
                {
                    if (!data[i].IsDeleted && filteringFlag[i]) ++dataCount;
                }
                puttoarray = new string[dataCount, columnsCount];
            }
            else if (data.Count != allDataCount)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.ExistMismatchBetweenDatasMessageIndex));
                return false;
            }
            string[] sectorDecArray = null;
            if (qType == QuestionType.SA || qType == QuestionType.MA)
            {
                string fmt = new string('0', (int)Math.Floor(Math.Log10(question.Sectors.Count)) + 1);
                if (datatype == OutputDataType.Decode)
                {
                    sectorDecArray = new string[question.Sectors.Count];
                    for (int i = 0; i < question.Sectors.Count; ++i)
                    {
                        sectorDecArray[i] = (i + 1).ToString(fmt) + ":" + question.Sectors[i + 1].Description;
                    }
                }
            }
            if (qType == QuestionType.MA && datatype != OutputDataType.Code && datatype != OutputDataType.QC3)
            {
                int startClmIdx = clmIdx;
                for (int i = 0; i < question.Sectors.Count; ++i)
                {
                    puttoarray[0, ++clmIdx] = question.Name + "_" + (i + 1).ToString();
                }
                int rowIdx = 0;
                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].IsDeleted || !filteringFlag[i]) continue;
                    ++rowIdx;
                    //clmIdx = startClmIdx;
                    //for (int j = 0; j < question.Sectors.Count; ++j)
                    //{
                    //    switch (data[i].DataType)
                    //    {
                    //        case DataType.NormalData:
                    //            int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                    //            int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                    //            if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                    //            {
                    //                puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "1" : sectorDecArray[j];
                    //            }
                    //            else
                    //            {
                    //                puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "0" : null;
                    //            }
                    //            break;
                    //        case DataType.NAData:
                    //            puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "0" : NALetter;
                    //            break;
                    //        case DataType.IVData:
                    //            puttoarray[rowIdx, ++clmIdx] = IVLetter;
                    //            break;
                    //    }
                    //}
                    clmIdx = startClmIdx;
                    switch (data[i].DataType)
                    {
                        case DataType.NormalData:
                        case DataType.NAData:
                            bool isNA = true;
                            if (data[i].DataType == DataType.NormalData)
                            {
                                for (int j = 0; j < question.Sectors.Count; ++j)
                                {
                                    int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                    int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                    if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                    {
                                        puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "1" : sectorDecArray[j];
                                        isNA = false;
                                    }
                                    else
                                    {
                                        puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "0" : null;
                                    }
                                }
                            }
                            if (isNA)
                            {
                                clmIdx = startClmIdx;
                                for (int j = 0; j < question.Sectors.Count; ++j)
                                {
                                    puttoarray[rowIdx, ++clmIdx] = NALetter;
                                }
                            }
                            break;
                        case DataType.IVData:
                            for (int j = 0; j < question.Sectors.Count; ++j)
                            {
                                puttoarray[rowIdx, ++clmIdx] = IVLetter;
                            }
                            break;
                    }
                }
            }
            else
            {
                puttoarray[0, ++clmIdx] = question.Name;
                int rowIdx = 0;
                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].IsDeleted || !filteringFlag[i]) continue;
                    ++rowIdx;
                    switch (data[i].DataType)
                    {
                        case DataType.NormalData:
                            switch (qType)
                            {
                                case QuestionType.SA:
                                    if (datatype == OutputDataType.Decode)
                                    {
                                        puttoarray[rowIdx, clmIdx] = sectorDecArray[(data[i] as SAData).Value - 1];
                                    }
                                    else
                                    {
                                        puttoarray[rowIdx, clmIdx] = (data[i] as SAData).Value.ToString();
                                    }
                                    break;
                                case QuestionType.MA:
                                    // コード形式orQC3形式のみ
                                    /*
                                    System.Text.StringBuilder secNoBuf = new System.Text.StringBuilder("");
                                    for (int j = 0; j < question.Sectors.Count; ++j)
                                    {
                                        int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                        int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                        if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                        {
                                            secNoBuf.Append("," + (j + 1).ToString());
                                        }
                                    }
                                    if (secNoBuf.Length == 0)
                                    {
                                        // 無回答扱い
                                        puttoarray[rowIdx, clmIdx] = NALetter;
                                    }
                                    else
                                    {
                                        if (datatype == OutputDataType.Code)
                                        {
                                            puttoarray[rowIdx, clmIdx] = secNoBuf.ToString().Substring(1);
                                        }
                                        else    // datatype == OutputDataType.QC3
                                        {
                                            puttoarray[rowIdx, clmIdx] = secNoBuf.ToString() + ",";
                                        }
                                    }
                                    */
                                    if (datatype == OutputDataType.Code)
                                    {
                                        puttoarray[rowIdx, clmIdx] = (data[i] as MAData).CodeValue;
                                    }
                                    else    // datatype == OutputDataType.QC3
                                    {
                                        puttoarray[rowIdx, clmIdx] = "," + (data[i] as MAData).CodeValue + ",";
                                    }
                                    break;
                                case QuestionType.FA:
                                    puttoarray[rowIdx, clmIdx] = (data[i] as FAData).Value;
                                    break;
                                case QuestionType.N:
                                    puttoarray[rowIdx, clmIdx] = (data[i] as NData).Value.ToString();
                                    break;
                            }
                            break;
                        case DataType.NAData:
                            puttoarray[rowIdx, clmIdx] = NALetter;
                            break;
                        case DataType.IVData:
                            puttoarray[rowIdx, clmIdx] = IVLetter;
                            break;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// ローデータイメージ二次元配列を生成する
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids">
        /// <paramref name="questions"/>内での出力対象の質問IDからなる配列
        /// <note>
        /// マトリクスの場合親質問を指定すると、その子質問をすべて出力対象とする<br />
        /// 子質問を個別に指定することは可能だが、親質問とその子質問の両方を指定することはNG
        /// </note>
        /// </param>
        /// <param name="datatype">出力データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="dirpath">データファイルの出力先ディレクトリパス</param>
        /// <param name="NALetter">無回答置き換え文字</param>
        /// <param name="IVLetter">非該当置き換え文字</param>
        /// <param name="resultArray">結果の二次元配列 (戻り値)</param>
        /// <param name="divisiblePoint">分割可能ポイント(列)ではtrueが入った一次元配列 (戻り値)</param>
        /// <param name="columnQCAnsType">列に該当するアイテムのQC3での回答タイプを表すQCAnswerType列挙型の値が入った一次元配列 (戻り値)</param>
        /// <param name="filteringFlag">絞り込みフラグ配列 (省略可、既定値null)</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetRawDataArray(Questions questions
                , decimal[] questionids, OutputDataType datatype, string dirpath
                , string NALetter, string IVLetter
                , out string[,] resultArray, out bool[] divisiblePoint, out QCAnswerType[] columnQCAnsType
                , bool[] filteringFlag = null)
        {
            // ローデータTXTパスが未設定の場合
            if (string.IsNullOrEmpty(dirpath))
            {
                ReadDBInfo readDBInfo = new ReadDBInfo();
                QuillInjector.GetInstance().Inject(readDBInfo);
                dirpath = System.IO.Path.Combine(readDBInfo.GetRawdataPath(), ((Questions.Question)questions[1]).QCWebID.ToString());
            }

            // 戻り値の初期化
            resultArray = null;
            // 引数のチェック
            int columnsCount = 0;
            string toptablename = null;
            QCWebException exception = null;
            if (!checkGetRawDataArrayArguments(questions, questionids
                    , ref datatype, ref toptablename, ref columnsCount, out divisiblePoint, out columnQCAnsType, out exception))
            {
                // throw new QCWebException("checkGetRawDataArrayArgumentsでエラーが発生しました");
                return exception;
            }
            string delflagpath = null;
            if (!CreateTextFile.CreateDeleteFlag(toptablename, dirpath, out delflagpath, out exception))
            {
                // throw new QCWebException("CreateDeleteFlagでエラーが発生しました");
                return exception;
            }
            ReadTextFile.ReadDeleteFlag(delflagpath, out exception);
            if (exception != null) return exception;
            int clmIdx = -1;
            int dataCount = 0;
            int allDataCount = 0;
            for (int i = 0; i < questionids.Length; ++i)
            {
                Questions.Question question = questions[questionids[i]] as Questions.Question;
                if (!putData(question, datatype, columnsCount, dirpath, NALetter, IVLetter
                           , ref dataCount, ref allDataCount, ref resultArray, ref clmIdx, out exception, ref filteringFlag))
                {
                    // throw new QCWebException("putDataでエラーが発生しました アイテムID:" + questionids[i]);
                    return exception;
                }
            }
            return null;
        }

        /// <summary>
        /// ローデータイメージ二次元配列を生成する
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids">
        /// <paramref name="questions"/>内での出力対象の質問IDからなる配列
        /// <note>
        /// マトリクスの場合親質問を指定すると、その子質問をすべて出力対象とする<br />
        /// 子質問を個別に指定することは可能だが、親質問とその子質問の両方を指定することはNG
        /// </note>
        /// </param>
        /// <param name="datatype">出力データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="dirpath">データファイルの出力先ディレクトリパス</param>
        /// <param name="NALetter">無回答置き換え文字</param>
        /// <param name="IVLetter">非該当置き換え文字</param>
        /// <param name="resultArray">結果の二次元配列 (戻り値)</param>
        /// <param name="divisiblePoint">分割可能ポイント(列)ではtrueが入った一次元配列 (戻り値)</param>
        /// <param name="filteringFlag">絞り込みフラグ配列 (省略可、既定値null)</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetRawDataArray(Questions questions
               , decimal[] questionids, OutputDataType datatype, string dirpath
               , string NALetter, string IVLetter
               , out string[,] resultArray, out bool[] divisiblePoint
               , bool[] filteringFlag = null)
        {
            QCAnswerType[] columnQCAnsType = null;
            return GetRawDataArray(questions, questionids, datatype, dirpath, NALetter, IVLetter
                                 , out resultArray, out divisiblePoint, out columnQCAnsType, filteringFlag);
        }

        private delegate QCWebException getRawDataArrayDelegate(Questions questions
                , decimal[] questionids, OutputDataType datatype, string dirpath
                , string NALetter, string IVLetter
                , out string[,] resultArray, out bool[] divisiblePoint, out QCAnswerType[] columnQCAnsType
                , bool[] filteringFlag);
        #endregion

        #region レイアウト表生成
        /// <summary>
        /// QC3形式設問設定表の列インデックス
        /// </summary>
        public enum QC3ColumnIndex
        {
            /// <summary>
            /// New列
            /// </summary>
            NewFlagColumn,
            /// <summary>
            /// 質問番号列
            /// </summary>
            QuestionNoColumn,
            /// <summary>
            /// 質問タイプ列
            /// </summary>
            QuestionTypeColumn,
            /// <summary>
            /// 質問数列
            /// </summary>
            ChildQuestionsCountColumn,
            /// <summary>
            /// アイテム名列
            /// </summary>
            ItemNameColumn,
            /// <summary>
            /// 回答タイプ列
            /// </summary>
            AnswerTypeColumn,
            /// <summary>
            /// カテゴリ数列
            /// </summary>
            SectorsCountColumn,
            /// <summary>
            /// WT列
            /// </summary>
            WeightColumn,
            /// <summary>
            /// 並替表示列
            /// </summary>
            SortSectorsColumn,
            /// <summary>
            /// カラム列
            /// </summary>
            ColumnIndexColumn,
            /// <summary>
            /// 表題列
            /// </summary>
            Description1Column,
            /// <summary>
            /// 質問文列
            /// </summary>
            Description2Column,
            /// <summary>
            /// 選択肢文先頭列
            /// </summary>
            SectorStartColumn
        }

        /// <summary>
        /// 横型レイアウト表の列インデックス
        /// </summary>
        public enum LandscapeLayoutColumnIndex
        {
            /// <summary>
            /// アイテム名列
            /// </summary>
            ItemNameColumn,
            /// <summary>
            /// 回答タイプ列
            /// </summary>
            AnswerTypeColumn,
            /// <summary>
            /// カテゴリ数列
            /// </summary>
            SectorsCountColumn,
            /// <summary>
            /// ＷＴ列
            /// </summary>
            WeightColumn,
            /// <summary>
            /// 並べ替え列
            /// </summary>
            SortSectorsColumn,
            /// <summary>
            /// 質問文A列
            /// </summary>
            Description1Column,
            /// <summary>
            /// 質問文B列
            /// </summary>
            Description2Column,
            /// <summary>
            /// 選択肢文の開始列
            /// </summary>
            SectorStartColumn
        }

        /// <summary>
        /// 縦型レイアウト表の列インデックス
        /// </summary>
        public enum PortraitLayoutColumnIndex
        {
            /// <summary>
            /// 質問番号列
            /// </summary>
            QuestionNoColumn,
            /// <summary>
            /// 質問タイプ列
            /// </summary>
            QuestionTypeColumn,
            /// <summary>
            /// アイテム名列
            /// </summary>
            ItemNameColumn,
            /// <summary>
            /// ラベル列
            /// </summary>
            LabelColumn,
            /// <summary>
            /// 回答タイプ列
            /// </summary>
            AnswerTypeColumn,
            /// <summary>
            /// カテゴリ数列
            /// </summary>
            SectorsCountColumn,
            /// <summary>
            /// カラム列
            /// </summary>
            ColumnIndexColumn,
            /// <summary>
            /// 選択肢番号列
            /// </summary>
            SectorNumberColumn,
            /// <summary>
            /// 質問文／選択肢文列
            /// </summary>
            DescriptionColumn
        }

        /// <summary>
        /// 横型レイアウトイメージ二次元配列を生成する
        /// <note>QC3形式でない場合は、シート番号列、列番号列を除いた二次元配列を生成する</note>
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids"><paramref name="questions"/>内での出力対象の質問IDからなる配列</param>
        /// <param name="resultArray">結果の二次元配列 (戻り値)</param>
        /// <param name="isQC3">QC3形式の場合true (省略可能、既定値false)</param>
        /// <param name="isExcel">Excel形式での出力時はtrue (省略可、既定値true)</param>
        /// <param name="locale">多言語情報取得用情報(省略可、規定値ja)</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        private static QCWebException getHorizontalLayoutArray(Questions questions
                , decimal[] questionids, out string[,] resultArray
                , bool isQC3 = false, bool isExcel = true, string locale = "ja")
        {
            // 戻り値の初期化
            resultArray = null;
            // 引数チェック
            int rowsCount = 0;
            QCWebException exception = null;
            if (isQC3) isExcel = true;
            int secCountPerRow = isExcel ? MAX_SECTORS_COUNT_PER_ROW : MAX_SECTORS_COUNT;
            if (!checkGetLayoutArrayArguments(questions, questionids, secCountPerRow, ref rowsCount, out exception))
            {
                // エラースロー
                return exception;
            }
            int tmpColumn = (int)LandscapeLayoutColumnIndex.SectorStartColumn;
            if (isQC3)
            {
                tmpColumn = (int)QC3ColumnIndex.SectorStartColumn;
            }
            resultArray = new string[rowsCount, tmpColumn + secCountPerRow];
            // 見出しの投入
            if (isQC3)
            {
                // TODO:Const化[QCR0000036]New
                // resultArray[0, (int)QC3ColumnIndex.NewFlagColumn] = GetResource.GetCommonResourceData("QCR0000036", locale);
                resultArray[0, (int)QC3ColumnIndex.NewFlagColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutNewFlagColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000037]質問番号
                // resultArray[0, (int)QC3ColumnIndex.QuestionNoColumn] = GetResource.GetCommonResourceData("QCR0000037", locale);
                // resultArray[0, (int)QC3ColumnIndex.QuestionNoColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQuestionNumberColumnCaptionIndex, locale);
                resultArray[0, (int)QC3ColumnIndex.QuestionNoColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQC3QuestionNumberColumnCaptionIndex, locale, true);
                // TODO:Const化[QCR0000038]質問タイプ
                // resultArray[0, (int)QC3ColumnIndex.QuestionTypeColumn] = GetResource.GetCommonResourceData("QCR0000038", locale);
                // resultArray[0, (int)QC3ColumnIndex.QuestionTypeColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQuestionTypeColumnCaptionIndex, locale);
                resultArray[0, (int)QC3ColumnIndex.QuestionTypeColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQC3QuestionTypeColumnCaptionIndex, locale, true);
                // TODO:Const化[QCR0000039]質問数
                // resultArray[0, (int)QC3ColumnIndex.ChildQuestionsCountColumn] = GetResource.GetCommonResourceData("QCR0000039", locale);
                // resultArray[0, (int)QC3ColumnIndex.ChildQuestionsCountColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutChildQuestionsCountColumnCaptionIndex, locale);
                resultArray[0, (int)QC3ColumnIndex.ChildQuestionsCountColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQC3ChildQuestionsCountColumnCaptionIndex, locale, true);
                // TODO:Const化[QCR0000040]アイテム名
                // resultArray[0, (int)QC3ColumnIndex.ItemNameColumn] = GetResource.GetCommonResourceData("QCR0000040", locale);
                resultArray[0, (int)QC3ColumnIndex.ItemNameColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutItemNameColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000041]回答タイプ
                // resultArray[0, (int)QC3ColumnIndex.AnswerTypeColumn] = GetResource.GetCommonResourceData("QCR0000041", locale);
                // resultArray[0, (int)QC3ColumnIndex.AnswerTypeColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutAnswerTypeColumnCaptionIndex, locale);
                resultArray[0, (int)QC3ColumnIndex.AnswerTypeColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQC3AnswerTypeColumnCaptionIndex, locale, true);
                // TODO:Const化[QCR0000042]カテゴリ数
                // resultArray[0, (int)QC3ColumnIndex.SectorsCountColumn] = GetResource.GetCommonResourceData("QCR0000042", locale);
                // resultArray[0, (int)QC3ColumnIndex.SectorsCountColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutSectorsCountColumnCaptionIndex, locale);
                resultArray[0, (int)QC3ColumnIndex.SectorsCountColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQC3SectorsCountColumnCaptionIndex, locale, true);
                // TODO:Const化[QCR0000043]WT
                // resultArray[0, (int)QC3ColumnIndex.WeightColumn] = GetResource.GetCommonResourceData("QCR0000043", locale);
                resultArray[0, (int)QC3ColumnIndex.WeightColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQC3WeightColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000044]並替表示
                // resultArray[0, (int)QC3ColumnIndex.SortSectorsColumn] = GetResource.GetCommonResourceData("QCR0000044", locale);
                resultArray[0, (int)QC3ColumnIndex.SortSectorsColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQC3SortSectorsColumnCaptionIndex, locale, true);
                // TODO:Const化[QCR0000045]カラム
                // resultArray[0, (int)QC3ColumnIndex.ColumnIndexColumn] = GetResource.GetCommonResourceData("QCR0000045", locale);
                resultArray[0, (int)QC3ColumnIndex.ColumnIndexColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutColumnIndexColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000046]表題
                // resultArray[0, (int)QC3ColumnIndex.Description1Column] = GetResource.GetCommonResourceData("QCR0000046", locale);
                resultArray[0, (int)QC3ColumnIndex.Description1Column] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQC3Description1ColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000047]質問文
                // resultArray[0, (int)QC3ColumnIndex.Description2Column] = GetResource.GetCommonResourceData("QCR0000047", locale);
                resultArray[0, (int)QC3ColumnIndex.Description2Column] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQC3Description2ColumnCaptionIndex, locale);
                for (int i = 0; i < secCountPerRow; ++i)
                {
                    resultArray[0, (int)QC3ColumnIndex.SectorStartColumn + i] = (i + 1).ToString();
                }
            }
            else
            {
                // TODO:Const化[QCR0000040]アイテム名
                // resultArray[0, (int)ColumnIndex.ItemNameColumn] = GetResource.GetCommonResourceData("QCR0000040", locale);
                resultArray[0, (int)LandscapeLayoutColumnIndex.ItemNameColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutItemNameColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000041]回答タイプ
                // resultArray[0, (int)ColumnIndex.AnswerTypeColumn] = GetResource.GetCommonResourceData("QCR0000041", locale);
                resultArray[0, (int)LandscapeLayoutColumnIndex.AnswerTypeColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutAnswerTypeColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000042]カテゴリ数
                resultArray[0, (int)LandscapeLayoutColumnIndex.SectorsCountColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutSectorsCountColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000048]ＷＴ
                // resultArray[0, (int)ColumnIndex.WeightColumn] = GetResource.GetCommonResourceData("QCR0000048", locale);
                resultArray[0, (int)LandscapeLayoutColumnIndex.WeightColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutLandscapeWeightColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000049]並べ替え
                // resultArray[0, (int)ColumnIndex.SortSectorsColumn] = GetResource.GetCommonResourceData("QCR0000049", locale);
                resultArray[0, (int)LandscapeLayoutColumnIndex.SortSectorsColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutLandscapeSortSectorsColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000050]質問文A
                // resultArray[0, (int)ColumnIndex.Description1Column] = GetResource.GetCommonResourceData("QCR0000050", locale);
                resultArray[0, (int)LandscapeLayoutColumnIndex.Description1Column] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutLandscapeDescription1ColumnCaptionIndex, locale);
                // TODO:Const化[QCR0000051]質問文B
                // resultArray[0, (int)ColumnIndex.Description2Column] = GetResource.GetCommonResourceData("QCR0000051", locale);
                resultArray[0, (int)LandscapeLayoutColumnIndex.Description2Column] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutLandscapeDescription2ColumnCaptionIndex, locale);
                for (int i = 0; i < secCountPerRow; ++i)
                {
                    resultArray[0, (int)LandscapeLayoutColumnIndex.SectorStartColumn + i] = (i + 1).ToString();
                }
            }

            int rowIdx = 0;
            int qCnt = 0;
            Questions.Question preParentQ = null;
            int dupStartRow = 0;
            int dupEndRow = 0;
            int childCount = 0;
            for (int i = 0; i < questionids.Length; ++i)
            {
                Questions.Question question = questions[questionids[i]] as Questions.Question;
                tmpColumn = isQC3 ? (int)QC3ColumnIndex.ItemNameColumn : (int)LandscapeLayoutColumnIndex.ItemNameColumn;
                if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    // 先頭の子アイテム名
                    resultArray[++rowIdx, tmpColumn] = question.ChildQuestions[1].Name;
                    dupStartRow = rowIdx;
                    dupEndRow = rowIdx;
                }
                else
                {
                    resultArray[++rowIdx, tmpColumn] = question.Name; // アイテム名
                }
                if (isQC3) resultArray[rowIdx, (int)QC3ColumnIndex.ColumnIndexColumn] = (++qCnt).ToString();  // カラム
                bool duplicate = false;
                if ((question.QuestionType & QuestionType.MatrixChild) == QuestionType.MatrixChild)
                {
                    duplicate = question.ParentQuestion.Equals(preParentQ);
                    if (!duplicate)
                    {
                        dupStartRow = rowIdx;
                        dupEndRow = rowIdx;
                        preParentQ = question.ParentQuestion as Questions.Question;
                        childCount = 0;
                    }
                }
                else
                {
                    preParentQ = question;
                }
                if (!duplicate)
                {
                    if (isQC3)
                    {
                        /*
                        string newCode = null;
                        if (question.IsNewItem)
                        {
                            switch (question.QCQuestionType & ~QCQuestionType.QuestionTypeAllBit)
                            {
                                case QCQuestionType.NewItem:    // 新アイテム
                                    // TODO:Const化[QCR0000052]NEW
                                    // newCode = GetResource.GetCommonResourceData("QCR0000052", locale);
                                    newCode = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportNewKeywordIndex, locale);
                                    break;
                                case QCQuestionType.Analysis:   // 多変量解析
                                    // TODO:Const化[QCR0000053]ANA
                                    // newCode = GetResource.GetCommonResourceData("QCR0000053", locale);
                                    newCode = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportAbbreviationForAnalyzeKeywordIndex, locale);
                                    break;
                            }
                        }
                        resultArray[rowIdx, (int)QC3ColumnIndex.NewFlagColumn] = newCode;    // New
                        */
                        if (!question.IsQC3BlankNumber)
                        {
                            resultArray[rowIdx, (int)QC3ColumnIndex.QuestionNoColumn] = question.Number;   // 質問番号
                            resultArray[rowIdx, (int)QC3ColumnIndex.QuestionTypeColumn]
                                        = Questions.Question.GetDescriptionFromQCQuestionType(
                                        ((question.QuestionType & QuestionType.MatrixChild) == QuestionType.MatrixChild ? preParentQ : question).QCQuestionType); // 質問タイプ
                        }
                    }
                    tmpColumn = isQC3 ? (int)QC3ColumnIndex.Description1Column : (int)LandscapeLayoutColumnIndex.Description1Column;
                    if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                    {
                        if (isQC3 && !question.IsQC3BlankNumber)
                        {
                            resultArray[rowIdx, (int)QC3ColumnIndex.ChildQuestionsCountColumn] = question.ChildQuestions.Count.ToString(); // 質問数
                        }
                        resultArray[rowIdx, tmpColumn] = question.Description;    // 表題
                    }
                    else
                    {
                        if ((question.QuestionType & QuestionType.MatrixChild) == QuestionType.MatrixChild)
                        {
                            if (isQC3 && !question.IsQC3BlankNumber)
                            {
                                // resultArray[rowIdx, (int)QC3ColumnIndex.ChildQuestionsCountColumn] = preParentQ.ChildQuestions.Count.ToString(); // 質問数
                                resultArray[rowIdx, (int)QC3ColumnIndex.ChildQuestionsCountColumn] = (++childCount).ToString(); // 質問数
                            }
                            resultArray[rowIdx, tmpColumn] = preParentQ.Description;    // 表題
                        }
                        else
                        {
                            resultArray[rowIdx, tmpColumn] = question.SuperfluityDescription;
                        }
                        tmpColumn = isQC3 ? (int)QC3ColumnIndex.Description2Column : (int)LandscapeLayoutColumnIndex.Description2Column;
                        resultArray[rowIdx, tmpColumn] = question.Description;
                    }
                    tmpColumn = isQC3 ? (int)QC3ColumnIndex.AnswerTypeColumn : (int)LandscapeLayoutColumnIndex.AnswerTypeColumn;
                    resultArray[rowIdx, tmpColumn]
                                = Questions.Question.GetDescriptionFromQCAnswerType(question.QCAnswerType); // 回答タイプ
                    if ((int)(question.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                    {
                        tmpColumn = isQC3 ? (int)QC3ColumnIndex.SectorsCountColumn : (int)LandscapeLayoutColumnIndex.SectorsCountColumn;
                        resultArray[rowIdx, tmpColumn] = question.Sectors.Count.ToString();   // カテゴリ数
                        System.Text.StringBuilder wtBuf = new System.Text.StringBuilder("");
                        System.Text.StringBuilder secNoBuf = new System.Text.StringBuilder("");
                        bool hasWeight = false;
                        int rOffset = 0;
                        for (int j = 0; j < question.Sectors.Count; ++j)
                        {
                            Sectors.Sector sector = question.Sectors[j + 1] as Sectors.Sector;
                            string wt = sector.Weight;
                            if (string.IsNullOrWhiteSpace(wt))
                            {
                                wt = "";
                            }
                            else
                            {
                                double wtnum = 0.0;
                                if (double.TryParse(wt, out wtnum))
                                {
                                    wt = wtnum.ToString();
                                    hasWeight = true;
                                }
                                else
                                {
                                    wt = "";
                                }
                            }
                            wtBuf.Append("," + wt);
                            if (question.DoSort)
                            {
                                if (!sector.IsUnsort)
                                {
                                    // secNoBuf.Append("," + (j + 1).ToString());
                                    // 現行仕様に合わせるが、将来の機能拡張を見越した作りにしておく
                                    secNoBuf = new System.Text.StringBuilder((j + 1).ToString());
                                }
                            }
                            rOffset = j / secCountPerRow;
                            int cOffset = j % secCountPerRow;
                            tmpColumn = isQC3 ? (int)QC3ColumnIndex.SectorStartColumn : (int)LandscapeLayoutColumnIndex.SectorStartColumn;
                            resultArray[rowIdx + rOffset, tmpColumn + cOffset] = sector.Description;  // 選択肢文
                        }
                        tmpColumn = isQC3 ? (int)QC3ColumnIndex.WeightColumn : (int)LandscapeLayoutColumnIndex.WeightColumn;
                        if (hasWeight) resultArray[rowIdx, tmpColumn] = wtBuf.ToString().Substring(1);  // WT
                        tmpColumn = isQC3 ? (int)QC3ColumnIndex.SortSectorsColumn : (int)LandscapeLayoutColumnIndex.SortSectorsColumn;
                        //if (secNoBuf.Length > 0)
                        //{
                        //    resultArray[rowIdx, tmpColumn] = secNoBuf.ToString().Substring(1); // 並替表示
                        //}
                        // 現行仕様に合わせるが、将来の機能拡張を見越した作りにしておく
                        if (secNoBuf.Length > 0)
                        {
                            resultArray[rowIdx, tmpColumn] = secNoBuf.ToString(); // 並替表示
                        }
                        rowIdx += rOffset;
                        dupEndRow = rowIdx;
                    }
                    if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                    {
                        // bool isfirst = true;
                        // foreach (DictionaryEntry de in question.ChildQuestions)
                        for (int n = 1; n <= question.ChildQuestions.Count; ++n)
                        {
                            // Questions.Question childQ = de.Value as Questions.Question;
                            Questions.Question childQ = question.ChildQuestions[n] as Questions.Question;
                            tmpColumn = isQC3 ? (int)QC3ColumnIndex.Description2Column : (int)LandscapeLayoutColumnIndex.Description2Column;
                            // if (isfirst)
                            if (n == 1)
                            {
                                resultArray[rowIdx, tmpColumn] = childQ.Description;
                                // isfirst = false;
                                continue;
                            }
                            resultArray[++rowIdx, tmpColumn] = childQ.Description;
                            tmpColumn = isQC3 ? (int)QC3ColumnIndex.ItemNameColumn : (int)LandscapeLayoutColumnIndex.ItemNameColumn;
                            resultArray[++rowIdx, tmpColumn] = childQ.Name; // アイテム名
                            if (isQC3)
                            {
                                resultArray[rowIdx, (int)QC3ColumnIndex.ColumnIndexColumn] = (++qCnt).ToString();  // カラム
                            }
                            tmpColumn = isQC3 ? (int)QC3ColumnIndex.AnswerTypeColumn : (int)LandscapeLayoutColumnIndex.AnswerTypeColumn;
                            int tmpColumn2 = isQC3 ? (int)QC3ColumnIndex.SectorStartColumn : (int)LandscapeLayoutColumnIndex.SectorStartColumn;
                            for (int c = tmpColumn; c < tmpColumn2; ++c)
                            {
                                if (!isQC3 || c != (int)QC3ColumnIndex.ColumnIndexColumn)
                                {
                                    resultArray[rowIdx, c] = resultArray[dupStartRow, c];
                                }
                            }
                            if ((int)(question.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                            {
                                --rowIdx;
                                for (int r = dupStartRow; r <= dupEndRow; ++r)
                                {
                                    ++rowIdx;
                                    tmpColumn = isQC3 ? (int)QC3ColumnIndex.SectorStartColumn : (int)LandscapeLayoutColumnIndex.SectorStartColumn;
                                    int c = tmpColumn - 1;
                                    for (int j = 0; j < secCountPerRow; ++j)
                                    {
                                        resultArray[rowIdx, ++c] = resultArray[r, c];
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    tmpColumn = isQC3 ? (int)QC3ColumnIndex.AnswerTypeColumn : (int)LandscapeLayoutColumnIndex.AnswerTypeColumn;
                    int tmpColumn2 = isQC3 ? (int)QC3ColumnIndex.SectorStartColumn : (int)LandscapeLayoutColumnIndex.SectorStartColumn;
                    for (int c = tmpColumn; c < tmpColumn2; ++c)
                    {
                        // if (!isQC3 || c != (int)QC3ColumnIndex.ColumnIndexColumn)
                        tmpColumn = isQC3 ? (int)QC3ColumnIndex.Description2Column : (int)LandscapeLayoutColumnIndex.Description2Column;
                        bool dup = c != tmpColumn;
                        if (!dup)
                        {
                            resultArray[rowIdx, c] = question.Description;
                        }
                        else if (isQC3)
                        {
                            tmpColumn = (int)QC3ColumnIndex.ColumnIndexColumn;
                            dup = c != tmpColumn;
                        }
                        if (dup)
                        {
                            resultArray[rowIdx, c] = resultArray[dupStartRow, c];
                        }
                    }
                    if ((int)(question.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                    {
                        --rowIdx;
                        for (int r = dupStartRow; r <= dupEndRow; ++r)
                        {
                            ++rowIdx;
                            tmpColumn = isQC3 ? (int)QC3ColumnIndex.SectorStartColumn : (int)LandscapeLayoutColumnIndex.SectorStartColumn;
                            int c = tmpColumn - 1;
                            for (int j = 0; j < secCountPerRow; ++j)
                            {
                                resultArray[rowIdx, ++c] = resultArray[r, c];
                            }
                        }
                    }
                    if (isQC3 && !question.IsQC3BlankNumber)
                    {
                        resultArray[dupStartRow, (int)QC3ColumnIndex.ChildQuestionsCountColumn] = (++childCount).ToString();
                    }
                }
            }
            return null;
        }

        private static void putVerticalLayoutData(Questions.Question question
                , ref int rowIdx, ref int cIdx, ref string[,] resultArray
                , bool putParentInfo = false, bool putFAL = false)
        {
            if (putParentInfo & (question.QuestionType & QuestionType.MatrixChild) == QuestionType.MatrixChild)
            {
                Questions.Question parentQ = question.ParentQuestion as Questions.Question;
                resultArray[++rowIdx, (int)PortraitLayoutColumnIndex.QuestionNoColumn] = parentQ.Number;
                resultArray[rowIdx, (int)PortraitLayoutColumnIndex.QuestionTypeColumn]
                            = Questions.Question.GetDescriptionFromQCQuestionType(parentQ.QCQuestionType);
                resultArray[rowIdx, (int)PortraitLayoutColumnIndex.DescriptionColumn] = parentQ.Description;
            }
            resultArray[++rowIdx, (int)PortraitLayoutColumnIndex.ItemNameColumn] = question.Name;
            resultArray[rowIdx, (int)PortraitLayoutColumnIndex.AnswerTypeColumn] = Questions.Question.GetDescriptionFromQCAnswerType(question.QCAnswerType);
            // resultArray[rowIdx, (int)PortraitColumnIndex.DescriptionColumn] = question.Description;
            resultArray[rowIdx, (int)PortraitLayoutColumnIndex.DescriptionColumn] = question.Description2(null, false);
            if (!question.IsPropertyItem && (int)(question.QuestionType & QuestionType.MatrixChild) == 0)
            {
                if ((question.QuestionType & QuestionType.FA_Sub) == QuestionType.FA_Sub)
                {
                    if (putFAL)
                    {
                        resultArray[rowIdx, (int)PortraitLayoutColumnIndex.QuestionNoColumn] = question.Name;
                        resultArray[rowIdx, (int)PortraitLayoutColumnIndex.QuestionTypeColumn] = "FAL";
                    }
                }
                else if (!question.IsQC3BlankNumber)
                {
                    resultArray[rowIdx, (int)PortraitLayoutColumnIndex.QuestionNoColumn] = question.Number;
                    resultArray[rowIdx, (int)PortraitLayoutColumnIndex.QuestionTypeColumn]
                                = Questions.Question.GetDescriptionFromQCQuestionType(question.QCQuestionType);
                }
            }
            QuestionType qType = question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA);
            if (qType != QuestionType.MA)
            {
                resultArray[rowIdx, (int)PortraitLayoutColumnIndex.LabelColumn] = question.Name;
                resultArray[rowIdx, (int)PortraitLayoutColumnIndex.ColumnIndexColumn] = (++cIdx).ToString();
            }
            switch (qType)
            {
                case QuestionType.SA:
                case QuestionType.MA:
                    resultArray[rowIdx, (int)PortraitLayoutColumnIndex.SectorsCountColumn] = question.Sectors.Count.ToString();
                    for (int i = 1; i <= question.Sectors.Count; ++i)
                    {
                        Sectors.Sector sec = question.Sectors[i] as Sectors.Sector;
                        resultArray[++rowIdx, (int)PortraitLayoutColumnIndex.SectorNumberColumn] = sec.Number.ToString();
                        resultArray[rowIdx, (int)PortraitLayoutColumnIndex.DescriptionColumn] = sec.Description;
                        if (qType == QuestionType.MA)
                        {
                            resultArray[rowIdx, (int)PortraitLayoutColumnIndex.LabelColumn] = question.Name + "_" + sec.Number.ToString();
                            resultArray[rowIdx, (int)PortraitLayoutColumnIndex.ColumnIndexColumn] = (++cIdx).ToString();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 縦型レイアウトイメージ二次元配列を生成する
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids"><paramref name="questions"/>内での出力対象の質問IDからなる配列</param>
        /// <param name="resultArray">結果の二次元配列 (戻り値)</param>
        /// <param name="locale">多言語情報取得用情報(省略可、規定値ja)</param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        private static QCWebException getVerticalLayoutArray(Questions questions
                , decimal[] questionids, out string[,] resultArray, string locale = "ja")
        {
            // 戻り値の初期化
            resultArray = null;
            // 引数チェック
            int rowsCount = 0;
            QCWebException exception = null;
            if (!checkGetLayoutArrayArguments(questions, questionids, 0, ref rowsCount, out exception, true))
            {
                return exception;
            }
            resultArray = new string[rowsCount, (int)PortraitLayoutColumnIndex.DescriptionColumn + 1];
            // 見出しの投入
            // TODO:Const化[QCR0000037]質問番号
            // resultArray[0, (int)PortraitColumnIndex.QuestionNoColumn] = GetResource.GetCommonResourceData("QCR0000037", locale);
            resultArray[0, (int)PortraitLayoutColumnIndex.QuestionNoColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQuestionNumberColumnCaptionIndex, locale);
            // TODO:Const化[QCR0000038]質問タイプ
            // resultArray[0, (int)PortraitColumnIndex.QuestionTypeColumn] = GetResource.GetCommonResourceData("QCR0000038", locale);
            resultArray[0, (int)PortraitLayoutColumnIndex.QuestionTypeColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutQuestionTypeColumnCaptionIndex, locale);
            // TODO:Const化[QCR0000040]アイテム名
            // resultArray[0, (int)PortraitColumnIndex.ItemNameColumn] = GetResource.GetCommonResourceData("QCR0000040", locale);
            resultArray[0, (int)PortraitLayoutColumnIndex.ItemNameColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutItemNameColumnCaptionIndex, locale);
            // TODO:Const化[QCR0000054]ラベル
            // resultArray[0, (int)PortraitColumnIndex.LabelColumn] = GetResource.GetCommonResourceData("QCR0000054", locale);
            resultArray[0, (int)PortraitLayoutColumnIndex.LabelColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutPortraitLabelColumnCaptionIndex, locale);
            // TODO:Const化[QCR0000041]回答タイプ
            // resultArray[0, (int)PortraitColumnIndex.AnswerTypeColumn] = GetResource.GetCommonResourceData("QCR0000041", locale);
            resultArray[0, (int)PortraitLayoutColumnIndex.AnswerTypeColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutAnswerTypeColumnCaptionIndex, locale);
            // TODO:Const化[QCR0000042]カテゴリ数
            // resultArray[0, (int)PortraitColumnIndex.SectorsCountColumn] = GetResource.GetCommonResourceData("QCR0000042", locale);
            resultArray[0, (int)PortraitLayoutColumnIndex.SectorsCountColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutSectorsCountColumnCaptionIndex, locale);
            // TODO:Const化[QCR0000045]カラム
            // resultArray[0, (int)PortraitColumnIndex.ColumnIndexColumn] = GetResource.GetCommonResourceData("QCR0000045", locale);
            resultArray[0, (int)PortraitLayoutColumnIndex.ColumnIndexColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutColumnIndexColumnCaptionIndex, locale);
            // TODO:Const化[QCR0000055]選択肢番号
            // resultArray[0, (int)PortraitColumnIndex.SectorNumberColumn] = GetResource.GetCommonResourceData("QCR0000055", locale);
            resultArray[0, (int)PortraitLayoutColumnIndex.SectorNumberColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutPortraitSectorNumberColumnCaptionIndex, locale);
            // TODO:Const化[QCR0000056]質問文／選択肢文
            // resultArray[0, (int)PortraitColumnIndex.DescriptionColumn] = GetResource.GetCommonResourceData("QCR0000056", locale);
            resultArray[0, (int)PortraitLayoutColumnIndex.DescriptionColumn] = GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportLayoutPortraitDescriptionColumnCaptionIndex, locale);
            int rowIdx = 0;
            int cIdx = 0;
            Questions.Question preParentQ = null;
            //int dupStartRow = 0;
            //int dupEndRow = 0;
            for (int i = 0; i < questionids.Length; ++i)
            {
                Questions.Question question = questions[questionids[i]] as Questions.Question;
                /*
                bool putQNo = false;
                Questions.Question firstChildQ = null;
                if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    // 先頭の子アイテム名
                    firstChildQ = question.ChildQuestions[1] as Questions.Question;
                    resultArray[++rowIdx, (int)PortraitColumnIndex.ItemNameColumn] = firstChildQ.Name;
                    putQNo = true;
                    dupStartRow = rowIdx;
                    dupEndRow = rowIdx;
                }
                else
                {
                    resultArray[++rowIdx, (int)PortraitColumnIndex.ItemNameColumn] = question.Name; // アイテム名
                }
                bool duplicate = false;
                if ((question.QuestionType & QuestionType.MatrixChild) == QuestionType.MatrixChild)
                {
                    duplicate = question.ParentQuestion.Equals(preParentQ);
                    if (!duplicate)
                    {
                        dupStartRow = rowIdx;
                        dupEndRow = rowIdx;
                        preParentQ = question.ParentQuestion as Questions.Question;
                    }
                    // マトリクスの先頭子質問かどうか (この判定いるのか？)
                    putQNo = question.Index == 1;
                }
                if (!duplicate)
                {
                    if (putQNo)
                    {
                        resultArray[rowIdx, (int)PortraitColumnIndex.QuestionNoColumn] = question.Number;   // 質問番号
                        resultArray[rowIdx, (int)PortraitColumnIndex.QuestionTypeColumn]
                                    = Questions.Question.GetDescriptionFromQCQuestionType(
                                    ((question.QuestionType & QuestionType.MatrixChild) == QuestionType.MatrixChild ? preParentQ : question).QCQuestionType); // 質問タイプ
                    }
                    resultArray[rowIdx, (int)PortraitColumnIndex.AnswerTypeColumn]
                                = Questions.Question.GetDescriptionFromQCAnswerType(question.QCAnswerType);   // 回答タイプ
                    resultArray[rowIdx, (int)PortraitColumnIndex.DescriptionColumn] = (firstChildQ == null ? question : firstChildQ).Description; // 質問文／選択肢文
                    if ((int)(question.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                    {
                        resultArray[rowIdx, (int)PortraitColumnIndex.SectorsCountColumn] = question.Sectors.Count.ToString();   // カテゴリ数
                        for (int j = 0; j < question.Sectors.Count; ++j)
                        {
                            Sectors.Sector sector = question.Sectors[j + 1] as Sectors.Sector;
                            resultArray[++rowIdx, (int)PortraitColumnIndex.LabelColumn] = (firstChildQ == null ? question : firstChildQ).Name + "_" + sector.Number.ToString();  // ラベル
                            resultArray[rowIdx, (int)PortraitColumnIndex.ColumnIndexColumn] = (++cIdx).ToString();  // カラム
                            resultArray[rowIdx, (int)PortraitColumnIndex.SectorNumberColumn] = sector.Number.ToString();    // 選択肢番号
                            resultArray[rowIdx, (int)PortraitColumnIndex.DescriptionColumn] = sector.Description;   // 質問文／選択肢文
                        }
                        dupEndRow = rowIdx;
                    }
                    else    // FA/N
                    {
                        resultArray[rowIdx, (int)PortraitColumnIndex.LabelColumn] = (firstChildQ == null ? question : firstChildQ).Name; // ラベル
                        resultArray[rowIdx, (int)PortraitColumnIndex.ColumnIndexColumn] = (++cIdx).ToString();  // カラム
                    }
                    if (firstChildQ != null)    // questionがマト親
                    {
                        foreach (DictionaryEntry de in question.ChildQuestions)
                        {
                            Questions.Question childQ = de.Value as Questions.Question;
                            if (childQ.Equals(firstChildQ)) continue;
                            int tmpRow = rowIdx;
                            resultArray[++tmpRow, (int)PortraitColumnIndex.ItemNameColumn] = childQ.Name;   // アイテム名
                            resultArray[tmpRow, (int)PortraitColumnIndex.DescriptionColumn] = childQ.Description; // 質問文／選択肢文
                            if ((int)(childQ.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                            {
                                for (int j = 0; j < childQ.Sectors.Count; ++j)
                                {
                                    resultArray[++tmpRow, (int)PortraitColumnIndex.ColumnIndexColumn] = (++cIdx).ToString();    // カラム
                                }
                            }
                            else
                            {
                                resultArray[tmpRow, (int)PortraitColumnIndex.ColumnIndexColumn] = (++cIdx).ToString();  // カラム
                            }
                            for (int r = dupStartRow; r <= dupEndRow; ++r)
                            {
                                if (r == dupStartRow)
                                {
                                    int c = (int)PortraitColumnIndex.AnswerTypeColumn;
                                    resultArray[++rowIdx, c] = resultArray[r, c];
                                    c = (int)PortraitColumnIndex.SectorsCountColumn;
                                    resultArray[rowIdx, c] = resultArray[r, c];
                                }
                                else
                                {
                                    int c = (int)PortraitColumnIndex.SectorNumberColumn;
                                    resultArray[++rowIdx, c] = resultArray[r, c];
                                    c = (int)PortraitColumnIndex.DescriptionColumn;
                                    resultArray[rowIdx, c] = resultArray[r, c];
                                }
                            }
                        }
                    }
                }
                */
                if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    for (int j = 1; j <= question.ChildQuestions.Count; ++j)
                    {
                        Questions.Question childQ = question.ChildQuestions[j] as Questions.Question;
                        putVerticalLayoutData(childQ, ref rowIdx, ref cIdx, ref resultArray, j == 1);
                    }
                }
                else
                {
                    bool putParentInfo = false;
                    bool putFAL = false;
                    if ((question.QuestionType & QuestionType.MatrixChild) == QuestionType.MatrixChild)
                    {
                        Questions.Question parentQ = question.ParentQuestion as Questions.Question;
                        if (parentQ != null)
                        {
                            if (!parentQ.Equals(preParentQ))
                            {
                                putParentInfo = true;
                                preParentQ = parentQ;
                            }
                        }
                    }
                    else if ((question.QuestionType & QuestionType.FA_Sub) == QuestionType.FA_Sub)
                    {
                        if (i > 0)
                        {
                            Questions.Question preQ = questions[questionids[i - 1]] as Questions.Question;
                            if (preQ.ParentQuestion != null)
                            {
                                preQ = preQ.ParentQuestion as Questions.Question;
                            }
                            putFAL = !question.ParentQuestion.Equals(preQ);
                        }
                    }
                    putVerticalLayoutData(question, ref rowIdx, ref cIdx, ref resultArray, putParentInfo, putFAL);
                }
            }
            return null;
        }

        /// <summary>
        /// レイアウトイメージ二次元配列を生成する
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids"><paramref name="questions"/>内での出力対象の質問IDからなる配列</param>
        /// <param name="datatype">出力データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="orientation">レイアウト表の向きを表すLayoutOrientation列挙型の値</param>
        /// <param name="resultArray">レイアウトイメージの二次元配列 (戻り値)</param>
        /// <param name="isExcel">
        /// Excel形式での出力時はtrue (省略可、既定値true)
        /// <note>この引数は<paramref name="orientation"/>がLayoutOrientation.Landscapeのときのみ有効</note>
        /// </param>
        /// <returns>失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</returns>
        public static QCWebException GetLayoutArray(Questions questions, decimal[] questionids
                    , OutputDataType datatype, LayoutOrientation orientation, out string[,] resultArray
                    , bool isExcel = true)
        {
            // 戻り値の初期化
            resultArray = null;
            // 引数の補正
            if (orientation != LayoutOrientation.Portrait) orientation = LayoutOrientation.Landscape;
            // 複雑な分岐は現時点では必要ないが、どうなるかわからないので念のため枠組みを用意しておく
            switch (datatype)
            {
                case OutputDataType.Flag:
                case OutputDataType.Decode:
                    if (orientation == LayoutOrientation.Landscape)
                    {
                        return getHorizontalLayoutArray(questions, questionids, out resultArray, false, isExcel);
                    }
                    else
                    {
                        return getVerticalLayoutArray(questions, questionids, out resultArray);
                    }
                case OutputDataType.QC3:
                    return getHorizontalLayoutArray(questions, questionids, out resultArray, true, true);
                default:    // フラグ/デコード/QC3のいずれでもなければ、デフォルトとしてコードとする
                    if (orientation == LayoutOrientation.Landscape)
                    {
                        return getHorizontalLayoutArray(questions, questionids, out resultArray, false, isExcel);
                    }
                    else
                    {
                        return getVerticalLayoutArray(questions, questionids, out resultArray);
                    }
            }
        }
        #endregion

        #region ローデータ表+レイアウト表生成
        /// <summary>
        /// ローデータイメージ二次元配列とレイアウトイメージ二次元配列を生成する
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids"><paramref name="questions"/>内での出力対象の質問IDからなる配列</param>
        /// <param name="datatype">出力データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="dirpath">データファイルの出力先ディレクトリパス</param>
        /// <param name="NALetter">無回答置き換え文字</param>
        /// <param name="IVLetter">非該当置き換え文字</param>
        /// <param name="orientation">レイアウト表の向きを表すLayoutOrientation列挙型の値</param>
        /// <param name="rawDataArray">ローデータイメージの二次元配列 (戻り値)</param>
        /// <param name="divisiblePoint">ローデータの分割可能ポイント(列)ではtrueが入った一次元配列 (戻り値)</param>
        /// <param name="columnQCAnsType">列に該当するアイテムのQC3での回答タイプを表すQCAnswerType列挙型の値が入った一次元配列 (戻り値)</param>
        /// <param name="layoutArray">レイアウトイメージの二次元配列 (戻り値)</param>
        /// <param name="isExcel">
        /// Excel形式での出力時はtrue
        /// <note>この引数は<paramref name="orientation"/>がLayoutOrientation.Landscapeのときのみ有効</note>
        /// </param>
        /// <param name="filteringFlag">絞り込みフラグ配列</param>
        private static QCWebException getRawDataAndLayoutArray(
                  Questions questions, decimal[] questionids
                , OutputDataType datatype, string dirpath
                , string NALetter, string IVLetter
                , LayoutOrientation orientation
                , out string[,] rawDataArray, out bool[] divisiblePoint, out QCAnswerType[] columnQCAnsType, out string[,] layoutArray
                , bool isExcel, bool[] filteringFlag)
        {
            // 戻り値の初期化
            rawDataArray = null;
            layoutArray = null;

            // ローデータTXTパスが未設定の場合
            if (string.IsNullOrEmpty(dirpath))
            {
                ReadDBInfo readDBInfo = new ReadDBInfo();
                QuillInjector.GetInstance().Inject(readDBInfo);
                dirpath = System.IO.Path.Combine(readDBInfo.GetRawdataPath(), (questions[0] as Questions.Question).QCWebID.ToString());
            }

            // ローデータ配列の取得
            getRawDataArrayDelegate getRawDataArrayDelegate
                    = new getRawDataArrayDelegate(GetRawDataArray);
            IAsyncResult ar = getRawDataArrayDelegate.BeginInvoke(
                                    questions, questionids, datatype, dirpath
                                  , NALetter, IVLetter, out rawDataArray, out divisiblePoint, out columnQCAnsType, filteringFlag, null, null);
            // レイアウト配列の取得
            QCWebException layoutException = GetLayoutArray(questions, questionids, datatype, orientation, out layoutArray, isExcel);
            QCWebException rawdataException = getRawDataArrayDelegate.EndInvoke(out rawDataArray, out divisiblePoint, out columnQCAnsType, ar);
            if (rawdataException != null) return rawdataException;
            return layoutException;
        }

        /// <summary>
        /// ローデータイメージ二次元配列とレイアウトイメージ二次元配列を生成する
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids"><paramref name="questions"/>内での出力対象の質問IDからなる配列</param>
        /// <param name="datatype">出力データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="dirpath">データファイルの出力先ディレクトリパス</param>
        /// <param name="NALetter">無回答置き換え文字</param>
        /// <param name="IVLetter">非該当置き換え文字</param>
        /// <param name="orientation">レイアウト表の向きを表すLayoutOrientation列挙型の値</param>
        /// <param name="rawDataArray">ローデータイメージの二次元配列 (戻り値)</param>
        /// <param name="divisiblePoint">ローデータの分割可能ポイント(列)ではtrueが入った一次元配列 (戻り値)</param>
        /// <param name="columnQCAnsType">列に該当するアイテムのQC3での回答タイプを表すQCAnswerType列挙型の値が入った一次元配列 (戻り値)</param>
        /// <param name="layoutArray">レイアウトイメージの二次元配列 (戻り値)</param>
        /// <param name="filteringFlag">絞り込みフラグ配列 (省略可、既定値null)</param>
        public static QCWebException GetRawDataAndLayoutArray(
                  Questions questions, decimal[] questionids
                , OutputDataType datatype, string dirpath
                , string NALetter, string IVLetter
                , LayoutOrientation orientation
                , out string[,] rawDataArray, out bool[] divisiblePoint, out QCAnswerType[] columnQCAnsType, out string[,] layoutArray
                , bool[] filteringFlag = null)
        {
            return getRawDataAndLayoutArray(questions, questionids, datatype, dirpath, NALetter, IVLetter, orientation
                                          , out rawDataArray, out divisiblePoint, out columnQCAnsType, out layoutArray
                                          , true, filteringFlag);
        }

        /// <summary>
        /// ローデータイメージ二次元配列とレイアウトイメージ二次元配列を生成する
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids"><paramref name="questions"/>内での出力対象の質問IDからなる配列</param>
        /// <param name="datatype">出力データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="dirpath">データファイルの出力先ディレクトリパス</param>
        /// <param name="NALetter">無回答置き換え文字</param>
        /// <param name="IVLetter">非該当置き換え文字</param>
        /// <param name="orientation">レイアウト表の向きを表すLayoutOrientation列挙型の値</param>
        /// <param name="rawDataArray">ローデータイメージの二次元配列 (戻り値)</param>
        /// <param name="divisiblePoint">ローデータの分割可能ポイント(列)ではtrueが入った一次元配列 (戻り値)</param>
        /// <param name="layoutArray">レイアウトイメージの二次元配列 (戻り値)</param>
        /// <param name="filteringFlag">絞り込みフラグ配列 (省略可、既定値null)</param>
        public static QCWebException GetRawDataAndLayoutArray(
                  Questions questions, decimal[] questionids
                , OutputDataType datatype, string dirpath
                , string NALetter, string IVLetter
                , LayoutOrientation orientation
                , out string[,] rawDataArray, out bool[] divisiblePoint, out string[,] layoutArray
                , bool[] filteringFlag = null)
        {
            QCAnswerType[] columnQCAnsType = null;
            return getRawDataAndLayoutArray(questions, questionids, datatype, dirpath, NALetter, IVLetter, orientation
                                          , out rawDataArray, out divisiblePoint, out columnQCAnsType, out layoutArray
                                          , false, filteringFlag);
        }
        #endregion

        #region ローデータ表イメージ文字列生成
        // TSV/CSV出力に使えるものを保険として用意しておく
        // いずれのメソッドも、パラメータを適切に渡してコールすれば、そのまま出力すればよい文字列あるいは文字列からなる配列を返す静的メソッド

        private struct DividedIndexes
        {
            public int StartIndex;
            public int EndIndex;

            internal DividedIndexes(int start, int end)
            {
                StartIndex = start;
                EndIndex = end;
            }
        }
        /// <summary>
        /// ローデータ表イメージ文字列からなる配列を返す
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids">
        /// <paramref name="questions"/>内での出力対象の質問IDからなる配列
        /// <note>
        /// マトリクスの場合親質問を指定すると、その子質問をすべて出力対象とする<br />
        /// 子質問を個別に指定することは可能だが、親質問とその子質問の両方を指定することはNG
        /// </note>
        /// </param>
        /// <param name="datatype">出力データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="dirpath">データファイルの出力先ディレクトリパス</param>
        /// <param name="NALetter">無回答置き換え文字</param>
        /// <param name="IVLetter">非該当置き換え文字</param>
        /// <param name="ColumnsCountPerPage">1ページの最大カラム数</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</param>
        /// <param name="RowDelimiter">行区切り文字 (省略可、既定値CR+LF)</param>
        /// <param name="ColumnDelimiter">列区切文字 (省略可、既定値半角カンマ)</param>
        /// <param name="EncloseWithDoubleQuot">半角ダブルクォートでセルの値を囲む場合true (省略可、既定値true)</param>
        /// <param name="EscapeDoubleQuotLetter">セル内の半角ダブルクォートのエスケープ文字 (省略可、既定値半角ダブルクォート)</param>
        /// <param name="filteringFlag">絞り込みフラグ配列 (省略可、既定値null)</param>
        /// <returns>ローデータ表イメージ文字列からなる配列</returns>
        public static string[] GetRawDataBuffer(Questions questions
                , decimal[] questionids, OutputDataType datatype, string dirpath
                , string NALetter, string IVLetter
                , int ColumnsCountPerPage, out QCWebException exception
                , string RowDelimiter = "\r\n", string ColumnDelimiter = ","
                , bool EncloseWithDoubleQuot = true, char EscapeDoubleQuotLetter = '"'
                , bool[] filteringFlag = null)
        {
            string[,] res = null;
            bool[] divisiblePoint = null;
            exception = GetRawDataArray(questions, questionids, datatype, dirpath, NALetter, IVLetter, out res, out divisiblePoint, filteringFlag);
            if (exception != null) return null;
            List<DividedIndexes> dividedColumnsList = new List<DividedIndexes>();
            int s = 0;
            int last = res.GetUpperBound(1);
            if (ColumnsCountPerPage < 2 || divisiblePoint == null)
            {
                dividedColumnsList.Add(new DividedIndexes(s, last));
            }
            else
            {
                while (s <= last)
                {
                    int nextS = s + ColumnsCountPerPage + (dividedColumnsList.Count == 0 ? 0 : -1);
                    if (nextS <= last)
                    {
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
                        dividedColumnsList.Add(new DividedIndexes(s, last));
                        break;
                    }
                }
            }
            string[] result = new string[dividedColumnsList.Count];
            for (int i = 0; i < dividedColumnsList.Count; ++i)
            {
                System.Text.StringBuilder tmpBuilder = new System.Text.StringBuilder();
                bool addRowDemim = false;
                for (int r = 0; r < res.GetLength(0); ++r)
                {
                    if (addRowDemim) tmpBuilder.Append(RowDelimiter);
                    addRowDemim = true;
                    bool addClmDelim = false;
                    if (i > 0)
                    {
                        string tmp = (EncloseWithDoubleQuot ? "\"" : "")
                                    + res[r, 0].Replace("\"", EscapeDoubleQuotLetter + "\"")
                                    + (EncloseWithDoubleQuot ? "\"" : "");
                        tmpBuilder.Append(tmp);
                        addClmDelim = true;
                    }
                    for (int c = dividedColumnsList[i].StartIndex; c <= dividedColumnsList[i].EndIndex; ++c)
                    {
                        if (addClmDelim) tmpBuilder.Append(ColumnDelimiter);
                        addClmDelim = true;
                        string tmp = (EncloseWithDoubleQuot ? "\"" : "")
                                    + res[r, c].Replace("\"", EscapeDoubleQuotLetter + "\"")
                                    + (EncloseWithDoubleQuot ? "\"" : "");
                        tmpBuilder.Append(tmp);
                    }
                }
                result[i] = tmpBuilder.ToString();
            }
            return result;
        }

        /// <summary>
        /// 列分割なしのローデータ表イメージ文字列を返す
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids">
        /// <paramref name="questions"/>内での出力対象の質問IDからなる配列
        /// <note>
        /// マトリクスの場合親質問を指定すると、その子質問をすべて出力対象とする<br />
        /// 子質問を個別に指定することは可能だが、親質問とその子質問の両方を指定することはNG
        /// </note>
        /// </param>
        /// <param name="datatype">出力データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="dirpath">データファイルの出力先ディレクトリパス</param>
        /// <param name="NALetter">無回答置き換え文字</param>
        /// <param name="IVLetter">非該当置き換え文字</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</param>
        /// <param name="RowDelimiter">行区切り文字 (省略可、既定値CR+LF)</param>
        /// <param name="ColumnDelimiter">列区切文字 (省略可、既定値半角カンマ)</param>
        /// <param name="EncloseWithDoubleQuot">半角ダブルクォートでセルの値を囲む場合true (省略可、既定値true)</param>
        /// <param name="EscapeDoubleQuotLetter">セル内の半角ダブルクォートのエスケープ文字 (省略可、既定値半角ダブルクォート)</param>
        /// <param name="filteringFlag">絞り込みフラグ配列 (省略可、既定値null)</param>
        /// <returns>ローデータ表イメージ文字列</returns>
        public static string GetRawDataBuffer(Questions questions
                , decimal[] questionids, OutputDataType datatype, string dirpath
                , string NALetter, string IVLetter
                , out QCWebException exception
                , string RowDelimiter = "\r\n", string ColumnDelimiter = ","
                , bool EncloseWithDoubleQuot = true, char EscapeDoubleQuotLetter = '"'
                , bool[] filteringFlag = null)
        {
            string[] res = GetRawDataBuffer(questions, questionids, datatype, dirpath, NALetter, IVLetter, 0, out exception, RowDelimiter, ColumnDelimiter, EncloseWithDoubleQuot, EscapeDoubleQuotLetter, filteringFlag);
            if (res == null) return null;
            return res[0];
        }

        /// <summary>
        /// レイアウト表イメージ文字列を返す
        /// </summary>
        /// <param name="questions">質問情報を保持するQuestionクラスのインスタンスへの参照をまとめたコレクションへの参照</param>
        /// <param name="questionids"><paramref name="questions"/>内での出力対象の質問IDからなる配列</param>
        /// <param name="datatype">出力データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="orientation">レイアウト表の向きを表すLayoutOrientation列挙型の値</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</param>
        /// <param name="RowDelimiter">行区切り文字 (省略可、既定値CR+LF)</param>
        /// <param name="ColumnDelimiter">列区切文字 (省略可、既定値半角カンマ)</param>
        /// <param name="EncloseWithDoubleQuot">半角ダブルクォートでセルの値を囲む場合true (省略可、既定値true)</param>
        /// <param name="EscapeDoubleQuotLetter">セル内の半角ダブルクォートのエスケープ文字 (省略可、既定値半角ダブルクォート)</param>
        /// <returns>レイアウト表イメージ文字列</returns>
        public static string GetLayoutBuffer(Questions questions, decimal[] questionids
                    , OutputDataType datatype, LayoutOrientation orientation
                , out QCWebException exception
                , string RowDelimiter = "\r\n", string ColumnDelimiter = ","
                , bool EncloseWithDoubleQuot = true, char EscapeDoubleQuotLetter = '"')
        {
            string[,] res = null;
            exception = GetLayoutArray(questions, questionids, datatype, orientation, out res);
            if (exception != null) return null;
            System.Text.StringBuilder tmpBuilder = new System.Text.StringBuilder();
            bool addRowDemim = false;
            for (int r = 0; r < res.GetLength(0); ++r)
            {
                if (addRowDemim) tmpBuilder.Append(RowDelimiter);
                addRowDemim = true;
                bool addClmDelim = false;
                for (int c = 0; c <= res.GetLength(1); ++c)
                {
                    if (addClmDelim) tmpBuilder.Append(ColumnDelimiter);
                    addClmDelim = true;
                    string tmp = (EncloseWithDoubleQuot ? "\"" : "")
                                + res[r, c].Replace("\"", EscapeDoubleQuotLetter + "\"")
                                + (EncloseWithDoubleQuot ? "\"" : "");
                    tmpBuilder.Append(tmp);
                }
            }
            return tmpBuilder.ToString();
        }
        #endregion
    }
    #endregion

    #region SetWeightBackクラス
    /// <summary>
    /// WB設定処理を行う静的メソッドを定義する静的クラス
    /// </summary>
    [ComVisible(false), Guid("1C6731D2-1803-4C92-A689-B42FAFEB0F58")]
    [Implementation]
    public class SetWeightBack
    {
        /// <summary>
        /// WB設定処理を行う
        /// </summary>
        /// <param name="qcwebid">条件のQCWEB管理ID</param>
        /// <param name="orgqid">条件のSA質問の質問ID</param>
        /// <param name="wbs">条件質問の各選択肢に設定されているWB値からなる配列</param>
        /// <param name="nawb">条件質問の無回答に設定されているWB値</param>
        /// <param name="ivwb">条件質問の非該当に設定されているWB値</param>
        /// <param name="exception">失敗時のエラー情報を保持したQCWebExceptionクラスのインスタンスへの参照</param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <returns>
        /// 各データのWB値情報を保持したDataクラスのインスタンスへの参照からなるListクラスのインスタンスへの参照
        /// </returns>
        public static List<Data> Execute(decimal qcwebid, decimal orgqid, double[] wbs, double nawb, double ivwb
                                       , out QCWebException exception, string dirpath = null)
        {
            exception = null;
            if (wbs == null || wbs.Length == 0)
            {
                // パラメータが不正です。wbs
                exception = new QCWebException("QCCMN13030302", GlobalsCommonConstant.LogLevel.FATAL, null);
                return null;
            }

            // ローデータTXTパスが未設定の場合
            if (string.IsNullOrEmpty(dirpath))
            {
                ReadDBInfo readDBInfo = new ReadDBInfo();
                QuillInjector.GetInstance().Inject(readDBInfo);
                dirpath = System.IO.Path.Combine(readDBInfo.GetRawdataPath(), qcwebid.ToString());
            }

            string orgpath = null;
            QuestionType qType;
            List<Data> orgdata = ReadTextFile.ReadData2(orgqid, dirpath, out orgpath, out qType, out exception, true);

            if ((qType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustQuestionTypeMessageIndex));
                return null;
            }
            if ((qType & QuestionType.SA) != QuestionType.SA)
            {
                exception = new QCWebException(new Message(Constants.CommonMessageIndex.UnjustQuestionTypeMessageIndex));
                return null;
            }

            List<Data> wbdata = new List<Data>();
            string wbpath = System.IO.Path.Combine(dirpath, DataIoConstant.WEIGHTBACK_TEXT_FILE_NAME);
            System.IO.StreamWriter writer = null;
            try
            {
                if (System.IO.File.Exists(wbpath))
                {
                    System.IO.File.Delete(wbpath);
                }
                Common.GlobalMethodClass.GuaranteeDirectoryExist(dirpath);
                writer = new System.IO.StreamWriter(wbpath, true, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                writer = null;
                // ファイルの操作に失敗しました:{0}
                exception = new QCWebException("QCCMN13030303", new string[] { e.Message }
                                               , GlobalsCommonConstant.LogLevel.FATAL, e);
                return null;
            }

            for (int i = 0; i < orgdata.Count; ++i)
            {
                bool isDeleted = orgdata[i].IsDeleted;
                double wb = nawb;   // 無回答扱いを初期値とする
                if (orgdata[i].IsNormal)
                {
                    int n = (orgdata[i] as SAData).Value;
                    if (n > 0 && n <= wbs.Length)
                    {
                        wb = wbs[n - 1];
                    }
                }
                else if (orgdata[i].IsIV)
                {
                    wb = ivwb;
                }
                wbdata.Add(new NData(wb, isDeleted));

                // データファイル書き出し
                if (writer != null)
                {
                    try
                    {
                        writer.WriteLine(wb);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();
                        writer.Dispose();
                        writer = null;
                        try
                        {
                            System.IO.File.Delete(wbpath);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                            Debug.Indent();
                            Debug.WriteLine("Type:{0}", ex.GetType().ToString());
                            Debug.WriteLine("Description:{0}", ex.Message);
                            Debug.Unindent();
                        }
                        // ファイルの操作に失敗しました:{0}
                        exception = new QCWebException("QCCMN13030303", new string[] { e.Message }
                                                       , GlobalsCommonConstant.LogLevel.FATAL, e);
                        return null;
                    }
                }
            }
            if (writer != null)
            {
                writer.Close();
                writer.Dispose();
            }

            return wbdata;
        }

        /// <summary>
        /// WB設定処理を行う
        /// ※使用禁止(N質問のWBファイルは作成しない)
        /// </summary>
        /// <param name="qcwebid">条件のQCWEB管理ID</param>
        /// <param name="orgqid">新アイテムのN質問の質問ID</param>
        /// <param name="exception"></param>
        /// <param name="dirpath">出力先ディレクトリのパス</param>
        /// <returns>
        /// 各データのWB値情報を保持したDataクラスのインスタンスへの参照からなるListクラスのインスタンスへの参照
        /// </returns>
        [System.ObsoleteAttribute]
        public static List<Data> Execute(decimal qcwebid, decimal orgqid, out QCWebException exception, string dirpath = null)
        {
            exception = null;
            ReadDBInfo readDBInfo = new ReadDBInfo();
            QuillInjector.GetInstance().Inject(readDBInfo);

            // アイテム情報TBLの検索
            Question.Questions questions = new Question.Questions(qcwebid);
            IQuestion question = questions[orgqid] as Question.Questions.Question;

            // ローデータTXTパスが未設定の場合
            if (string.IsNullOrEmpty(dirpath))
            {
                dirpath = System.IO.Path.Combine(
                    readDBInfo.GetRawdataPath(), (question as Questions.Question).QCWebID.ToString());
            }

            string orgpath = null;
            //QuestionType qType = (QuestionType)int.Parse(entity.AnswerType);  //QuestionType.SA;
            QuestionType qType = question.QuestionType;  //QuestionType.SA;
            if (!CreateTextFile.CreateData(orgqid, dirpath, out orgpath, out qType, out exception)) throw exception;
            if ((qType & QuestionType.MatrixParent) == QuestionType.MatrixParent) return null;    // エラースロー
            if ((qType & QuestionType.N) != QuestionType.N) return null;    // エラースロー
            List<Data> orgdata = ReadTextFile.ReadData(orgpath, qType, out exception);
            if (orgdata == null) return null;

            List<Data> wbdata = orgdata.ToList<Data>();
            string wbpath = System.IO.Path.Combine(dirpath
                                                  , string.Format(DataIoConstant.WEIGHTBACK_TEXT_FILE_NAME2, orgqid.ToString()));
            System.IO.StreamWriter writer = null;
            try
            {
                if (System.IO.File.Exists(wbpath))
                {
                    System.IO.File.Delete(wbpath);
                }
                Common.GlobalMethodClass.GuaranteeDirectoryExist(dirpath);
                writer = new System.IO.StreamWriter(wbpath, true, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                writer = null;
                // ファイルの操作に失敗しました:{0}
                exception = new QCWebException("QCCMN13030303", new string[] { e.Message }
                                               , GlobalsCommonConstant.LogLevel.FATAL, e);
                return null;
            }

            for (int i = 0; i < wbdata.Count; ++i)
            {
                string wbbuf = null;    // 無回答扱いを初期値とする
                if (wbdata[i].IsNormal)
                {
                    wbbuf = (wbdata[i] as NData).Value.ToString();
                }
                else if (wbdata[i].IsIV)
                {
                    wbbuf = "*";
                }

                // データファイル書き出し
                if (writer != null)
                {
                    try
                    {
                        writer.WriteLine(wbbuf);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();
                        writer.Dispose();
                        writer = null;
                        try
                        {
                            System.IO.File.Delete(wbpath);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                            Debug.Indent();
                            Debug.WriteLine("Type:{0}", ex.GetType().ToString());
                            Debug.WriteLine("Description:{0}", ex.Message);
                            Debug.Unindent();
                        }
                        // ファイルの操作に失敗しました:{0}
                        exception = new QCWebException("QCCMN13030303", new string[] { e.Message }
                                                       , GlobalsCommonConstant.LogLevel.FATAL, e);
                        return null;
                    }
                }
            }
            if (writer != null)
            {
                writer.Close();
                writer.Dispose();
            }

            return wbdata;
        }

    }
    #endregion
}
