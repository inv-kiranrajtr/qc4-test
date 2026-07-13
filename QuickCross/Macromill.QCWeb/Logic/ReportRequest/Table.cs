#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Table.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/3/23
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/8
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Tabulation;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Exceptions;
using Strings = Microsoft.VisualBasic.Strings;

namespace Macromill.QCWeb.ReportRequest
{
    /// <summary>
    /// 出力命令側から扱う集計表のコレクションクラス
    /// </summary>
    [ComVisible(false), Guid("5E90B254-1E2B-437e-AC74-198B2E4F03BD")]
    public class Tables : Hashtable, ITables
    {
        /// <summary>
        /// 出力命令側から扱う集計表を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("1A1C0E49-734C-45d4-95C7-48823F542986")]
        public class Table : ITable
        {
            private int index = 0;
            private Tabulation.QuestionType questiontype = (Tabulation.QuestionType)0;
            private Tabulation.DataWithMarking[,] datavalue = null;
            private Tabulation.DataWithMarking[][,] datajagvalue = null;
            private Tabulation.DataWithMarking[][,] datajagvalueTotal = null;
            private Tabulation.DataWithMarking[][,] datajagvalueUnweightedTotal = null;
            private string[,] strvalue = null;
            private string comment = null;
            private int sectorStartIndex = 0;
            private int sectorEndIndex = 0;
            private int totalIndex = 0;

            private Tables Collection = null;

            // GT/Cross(1シート複数クロス)用
            internal Table(Tables tables, Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[,] wholeArray)
            {
                Collection = tables;
                index = Collection.Count;
                this.questiontype = questiontype;
                switch (ParentOutput.OutputType)
                {
                    case OutputType.GT:
                        break;
                    case OutputType.Cross:
                        if ((ParentOutput as Outputs.OutputCross).TablesOnOnesheet == TablesOnOneSheet.Multi)
                        {
                            datajagvalue = new Tabulation.DataWithMarking[1][,];
                            datajagvalue[0] = wholeArray;
                            return;    // クロスは出力時にカット行列を算出 (軸情報が設定されてから)
                        }
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustConstructorMessageIndex)
                                               , GlobalsCommonConstant.LogLevel.FATAL);
                    default:
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustConstructorMessageIndex)
                                               , GlobalsCommonConstant.LogLevel.FATAL);
                }
                int minbase = (ParentOutput as Outputs.OutputGT).MinSamplesCountOnMarking;
                List<int> cutColumnIndexes = new List<int>();
                List<int> cutRowIndexes = new List<int>();
                // wholeArrayを走査して、設定に応じたdatavalueを生成
                if ((questiontype & Tabulation.QuestionType.N) == QuestionType.N)
                {
                    cutColumnIndexes.Add(2);    // 空列
                    if (!(ParentOutput as Outputs.OutputGT).ShowPreWBTotal) cutColumnIndexes.Add(3); // WB前全体列
                    if (!ParentRequest.ShowParameter) cutColumnIndexes.Add(5);  // 統計量母数列
                    if (!ParentRequest.ShowSummary) cutColumnIndexes.Add(6);    // 合計列
                    if (!ParentRequest.ShowAverage) cutColumnIndexes.Add(7);    // 平均列
                    if (!ParentRequest.ShowStdev) cutColumnIndexes.Add(8);      // 標準偏差列
                    if (!ParentRequest.ShowMinimum) cutColumnIndexes.Add(9);    // 最小値列
                    if (!ParentRequest.ShowMaximum) cutColumnIndexes.Add(10);   // 最大値列
                    if (!ParentRequest.ShowMedian) cutColumnIndexes.Add(11);    // 中央値列
                    /*
                    bool cutNA = !ParentRequest.ShowNAAtItem;
                    bool cutIV = !ParentRequest.ShowIVAtItem;
                    if (!ParentRequest.ShowZeroNAIV && (!cutNA | !cutIV))
                    {
                        int startRow = 1;
                        int endRow = 1;
                        if ((questiontype & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
                        {
                            startRow = 2;
                            endRow = wholeArray.GetUpperBound(0);
                            if (!cutNA)
                            {
                                cutNA = true;
                                for (int i = startRow; i <= endRow; ++i)
                                {
                                    if (wholeArray[i, 12].NumValue > 0.0)
                                    {
                                        cutNA = false;
                                        break;
                                    }
                                }
                            }
                            if (!cutIV)
                            {
                                cutIV = true;
                                for (int i = startRow; i <= endRow; ++i)
                                {
                                    if (wholeArray[i, 13].NumValue > 0.0)
                                    {
                                        cutIV = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (cutNA) cutColumnIndexes.Add(12);    // 無回答列
                    if (cutIV) cutColumnIndexes.Add(13);    // 非該当列
                    */
                    if (!(ParentOutput as Outputs.OutputGT).ShowNAAtItem) cutColumnIndexes.Add(12);  // 無回答列
                    if (!(ParentOutput as Outputs.OutputGT).ShowIVAtItem) cutColumnIndexes.Add(13);  // 非該当列
                    // 最小ベースに応じたマーキング情報のカット
                    if ((questiontype & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                    {
                        for (int i = 2; i < wholeArray.GetLength(0); ++i)
                        {
                            if (wholeArray[i, 3].NumValue < (double)minbase || wholeArray[i, 3].NumValue == 0)
                            {
                                // インデックス7は平均
                                wholeArray[i, 7].ClearAllMarking();
                                if (wholeArray[i, 7].SettedSectorInformation)
                                {
                                    int sNo = wholeArray[i, 7].SectorNumber;
                                    int sCnt = wholeArray[i, 7].SectorsCount;
                                    for (int s = 1, j = i - sNo + 1; s <= sCnt; ++s, ++j)
                                    {
                                        if (s != sNo)
                                        {
                                            wholeArray[j, 7].RemoveSignificanceSectorNumber(sNo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if ((questiontype & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    cutColumnIndexes.Add(2);    // 空列
                    if (!(ParentOutput as Outputs.OutputGT).ShowPreWBTotal) cutColumnIndexes.Add(3);    // WB前全体列
                    int NAColumn = wholeArray.GetUpperBound(1) - 3;
                    if (!(ParentOutput as Outputs.OutputGT).ShowNAAtItem) cutColumnIndexes.Add(NAColumn);   // 無回答列
                    if (!(ParentOutput as Outputs.OutputGT).ShowIVAtItem) cutColumnIndexes.Add(NAColumn + 1);   // 非該当列
                    bool cutWT = true;
                    for (int i = 5; i < NAColumn; ++i)
                    {
                        double wt = 0.0;
                        if (double.TryParse(wholeArray[2, i].Value, out wt))
                        {
                            cutWT = false;
                            break;
                        }
                    }
                    if (cutWT)
                    {
                        cutColumnIndexes.Add(NAColumn + 2); // 統計量母数列
                        cutColumnIndexes.Add(NAColumn + 3); // 加重平均列
                        //cutRowIndexes.Add(2);   // WT行
                    }
                    // 最小ベースに応じたマーキング情報のカット
                    for (int i = 3; i < wholeArray.GetLength(0); ++i)
                    {
                        if (wholeArray[i, 3].NumValue < (double)minbase || wholeArray[i, 3].NumValue == 0)
                        {
                            int wtaveIdx = NAColumn + 3;
                            for (int j = 5; j <= wtaveIdx; ++j)
                            {
                                wholeArray[i, j].ClearAllMarking();
                                if (wholeArray[i, j].SettedSectorInformation)
                                {
                                    int sNo = wholeArray[i, j].SectorNumber;
                                    int sCnt = wholeArray[i, j].SectorsCount;
                                    if (sCnt > 1)
                                    {
                                        int sIdx = i - sNo + 1, eIdx = sIdx + sCnt - 1;
                                        if (sIdx >= wholeArray.GetLowerBound(0) && sIdx <= wholeArray.GetUpperBound(0)
                                            && eIdx >= wholeArray.GetLowerBound(0) && eIdx <= wholeArray.GetUpperBound(0))
                                        {
                                            DataWithMarking d = wholeArray[sNo == 1 ? eIdx : sIdx, j];
                                            // 子質問間検定のとき
                                            if (d.SettedSectorInformation && d.SectorNumber != sNo && d.SectorsCount == sCnt)
                                            {
                                                for (int s = 1, idx = sIdx; s <= sCnt; ++s, ++idx)
                                                {
                                                    if (s != sNo)
                                                    {
                                                        wholeArray[idx, j].RemoveSignificanceSectorNumber(sNo);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (j == NAColumn - 1) j = wtaveIdx - 1;
                            }
                        }
                    }

                    if (this.GetType() == typeof(GTTable))// #OutputFormatting #For GT Sub total implementation
                    {
                        if (!(ParentOutput as Outputs.OutputGT).ShowPreWBTotal)
                            sectorEndIndex = NAColumn - 2;
                        else
                            sectorEndIndex = NAColumn - 1;
                    }
                    else
                        sectorEndIndex = NAColumn - 2;

                }
                else // if ((int)(questiontype & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0)
                {
                    /*
                    bool cutNA = !ParentRequest.ShowNAAtItem;
                    bool cutIV = !ParentRequest.ShowIVAtItem;
                    */
                    totalIndex = 3;
                    sectorStartIndex = 3;
                    int NARow = wholeArray.GetUpperBound(0) - 3;
                    sectorEndIndex = NARow - 1;
                    if (!(ParentOutput as Outputs.OutputGT).ShowPreWBTotal)
                    {
                        cutRowIndexes.Add(1);    // WB前全体行
                        --sectorStartIndex;
                        --sectorEndIndex;
                    }
                    int IVRow = wholeArray.GetUpperBound(0) - 2;
                    /*
                    if (!ParentRequest.ShowZeroNAIV && (!cutNA | !cutIV))
                    {
                        if (!cutNA) cutNA = wholeArray[NARow, 3].NumValue == 0.0;
                        if (!cutIV) cutIV = wholeArray[IVRow, 3].NumValue == 0.0;
                    }
                    if (cutNA) cutRowIndexes.Add(NARow);    // 無回答行
                    if (cutIV) cutRowIndexes.Add(IVRow);    // 非該当行
                    */
                    if (!(ParentOutput as Outputs.OutputGT).ShowNAAtItem) cutRowIndexes.Add(NARow);    // 無回答行
                    if (!(ParentOutput as Outputs.OutputGT).ShowIVAtItem) cutRowIndexes.Add(IVRow);    // 非該当行
                    // 加重平均
                    bool cutWT = true;
                    for (int i = 3; i < NARow; ++i)
                    {
                        double wt = 0.0;
                        if (double.TryParse(wholeArray[i, 2].Value, out wt))
                        {
                            cutWT = false;
                            break;
                        }
                    }
                    if (cutWT)
                    {
                        cutRowIndexes.Add(wholeArray.GetUpperBound(0) - 1); // 統計量母数行
                        cutRowIndexes.Add(wholeArray.GetUpperBound(0)); // 加重平均行
                        cutColumnIndexes.Add(2);    // WT列
                        --totalIndex;
                    }
                    // 最小ベースに応じたマーキング情報のカット
                    if (wholeArray[1, 3].NumValue < (double)minbase || wholeArray[1, 3].NumValue == 0)
                    {
                        for (int i = 3; i < NARow; ++i)
                        {
                            wholeArray[i, 3].ClearAllMarking();
                        }
                    }
                }
                datavalue = new Tabulation.DataWithMarking
                        [wholeArray.GetLength(0) - cutRowIndexes.Count, wholeArray.GetLength(1) - cutColumnIndexes.Count];
                int r = -1;
                for (int i = 0; i < wholeArray.GetLength(0); ++i)
                {
                    if (cutRowIndexes.BinarySearch(i) >= 0) continue;
                    ++r;
                    int c = -1;
                    for (int j = 0; j < wholeArray.GetLength(1); ++j)
                    {
                        if (cutColumnIndexes.BinarySearch(j) >= 0) continue;
                        datavalue[r, ++c] = wholeArray[i, j];
                    }
                }
            }

            // Cross(1シート1クロス)用
            internal Table(Tables tables, Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[][,] wholeJagArray)
            {
                Collection = tables;
                index = Collection.Count;
                if (ParentOutput.OutputType != OutputType.Cross || (ParentOutput as Outputs.OutputCross).TablesOnOnesheet != TablesOnOneSheet.Single)
                {
                    // commented for qc4
                    //throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustConstructorMessageIndex)
                    //                      , GlobalsCommonConstant.LogLevel.FATAL);
                }
                this.questiontype = questiontype;
                datajagvalue = wholeJagArray;
            }

            // Cross(1シート1クロス)用 For summary list
            internal Table(Tables tables, Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[][,] wholeJagArray, Tabulation.DataWithMarking[][,] wholeJagArrayTotal, Tabulation.DataWithMarking[][,] wholeJagArrayUnweightedTotal=null)
            {
                Collection = tables;
                index = Collection.Count;
                if (ParentOutput.OutputType != OutputType.Cross || (ParentOutput as Outputs.OutputCross).TablesOnOnesheet != TablesOnOneSheet.Single)
                {
                    // commented for qc4
                    //throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustConstructorMessageIndex)
                    //                      , GlobalsCommonConstant.LogLevel.FATAL);
                }
                this.questiontype = questiontype;
                datajagvalue = wholeJagArray;
                datajagvalueTotal = wholeJagArrayTotal;
                datajagvalueUnweightedTotal = wholeJagArrayUnweightedTotal;
            }

            // FAリスト/チェックリスト
            internal Table(Tables tables, Tabulation.QuestionType questiontype, string[,] strArray)
            {
                Collection = tables;
                index = Collection.Count;
                this.questiontype = questiontype;
                strvalue = strArray;
            }

            // 調査票用
            internal Table(Tables tables, Tabulation.QuestionType questiontype)
            {
                Collection = tables;
                index = Collection.Count;
                this.questiontype = questiontype;
            }

            // RawData用 (QC3を含む)
            internal Table(Tables tables, string[,] strArray)
            {
                Collection = tables;
                index = Collection.Count;
                strvalue = strArray;
            }

            private void JagToNormalDataValue(AxesGroupInformation axesgroups)
            {
                int maxAxesCount = 1;
                for (int i = 0; i < axesgroups.Count; ++i)
                {
                    if (axesgroups[i].Count == 2)
                    {
                        maxAxesCount = 2;
                        break;
                    }
                }
                List<int> cutColumnIndexes = new List<int>();
                List<int> cutRowIndexes = new List<int>();

                #region コメント
                /*
                int cutRowCount = (questiontype & Tabulation.QuestionType.N) == Tabulation.QuestionType.N ? 1 : 2;
                // 最小ベースに応じたマーキング情報のカット
                if (cutRowCount == 2)   // SA/MA
                {
                    int minbase = (ParentOutput as Outputs.OutputCross).MinSamplesCountOnMarking;
                    for (int x = 0; x < datajagvalue.Length; ++x)
                    {
                        for (int r = 2; r < datajagvalue[x].GetLength(0); ++r)
                        {
                            if (datajagvalue[x][r, axesgroups[x].Count + 1].NumValue < (double)minbase)
                            {
                                for (int c = axesgroups[x].Count + 3; c < datajagvalue[x].GetUpperBound(1) - 3; ++c)
                                {
                                    datajagvalue[x][r, c].ClearAllMarking();
                                }
                            }
                        }
                    }
                }
                for (int x = 0; x < 1; ++x)
                {
                    // int AddRowCount = (questiontype & Tabulation.QuestionType.N) == Tabulation.QuestionType.N ? 0 : 1;
                    if (!(ParentOutput as Outputs.OutputCross).ShowPreWBTotal) cutColumnIndexes.Add(1 + maxAxesCount);  // WB前全体列
                    bool cutNA = !(ParentOutput as Outputs.OutputCross).ShowNAAtItem;
                    bool cutIV = !(ParentOutput as Outputs.OutputCross).ShowIVAtItem;
                    //bool cutZeroNAIV = !ParentRequest.ShowZeroNAIV;
                    //if (cutZeroNAIV)
                    //{
                    //    if (AddRowCount == 0)   // N
                    //    {
                    //        if (!cutNA) cutNA = datajagvalue[0][1, datajagvalue[0].GetUpperBound(1) - 1].NumValue == 0.0;
                    //        if (!cutIV) cutIV = datajagvalue[0][1, datajagvalue[0].GetUpperBound(1)].NumValue == 0.0;
                    //    }
                    //    else   // SA/MA
                    //    {
                    //        if (!cutNA) cutNA = datajagvalue[0][2, datajagvalue[0].GetUpperBound(1) - 2].NumValue == 0.0;
                    //        if (!cutIV) cutIV = datajagvalue[0][2, datajagvalue[0].GetUpperBound(1) - 1].NumValue == 0.0;
                    //    }
                    //}
                    //if (AddRowCount == 0)   // N
                    if (cutRowCount == 1)   // N
                    {
                        if (!ParentRequest.ShowSummary) cutColumnIndexes.Add(1 + maxAxesCount + 3); // 合計列
                        if (!ParentRequest.ShowAverage) cutColumnIndexes.Add(1 + maxAxesCount + 4); // 平均列
                        if (!ParentRequest.ShowStdev) cutColumnIndexes.Add(1 + maxAxesCount + 5);   // 標準偏差列
                        if (!ParentRequest.ShowMinimum) cutColumnIndexes.Add(1 + maxAxesCount + 6); // 最小値列
                        if (!ParentRequest.ShowMaximum) cutColumnIndexes.Add(1 + maxAxesCount + 7); // 最大値列
                        if (!ParentRequest.ShowMedian) cutColumnIndexes.Add(1 + maxAxesCount + 8);  // 中央値列
                        if (cutNA) cutColumnIndexes.Add(1 + maxAxesCount + 9);  // 無回答列
                        if (cutIV) cutColumnIndexes.Add(1 + maxAxesCount + 10); // 非該当列
                    }
                    else    // SA/MA
                    {
                        int AddColumnsCount = axesgroups[0].Count < maxAxesCount ? 1 : 0;
                        if (cutNA) cutColumnIndexes.Add(datajagvalue[0].GetUpperBound(1) - 2 + AddColumnsCount);    // 無回答列
                        if (cutIV) cutColumnIndexes.Add(datajagvalue[0].GetUpperBound(1) - 1 + AddColumnsCount);    // 非該当列
                        // ウエイト
                        bool cutWT = true;
                        for (int i = 1 + axesgroups[0].Count + 2; i < datajagvalue[0].GetLength(1) - 3; ++i)
                        {
                            string strWT = datajagvalue[0][1, i].Value;
                            double wt = 0.0;
                            if (double.TryParse(strWT, out wt))
                            {
                                cutWT = false;
                                break;
                            }
                        }
                        if (cutWT)
                        {
                            cutRowIndexes.Add(1);   // ウエイト行
                            cutColumnIndexes.Add(datajagvalue[0].GetUpperBound(1) - 1 + AddColumnsCount);   // 加重平均母数列
                            cutColumnIndexes.Add(datajagvalue[0].GetUpperBound(1) + AddColumnsCount);   // 加重平均列
                        }
                    }
                    cutNA = !(ParentOutput as Outputs.OutputCross).ShowNAAtAxis;
                    cutIV = !(ParentOutput as Outputs.OutputCross).ShowIVAtAxis;
                    //if (!cutNA && !cutIV && !cutZeroNAIV) break;
                    //int r = 0;
                    //for (int i = 0; i < axesgroups.Count; ++i)
                    //{
                    //    int c = 1 + axesgroups[i].Count;    // WB前全体列のインデックス
                    //    if (c == 2) // 二重クロス
                    //    {
                    //        bool tmpCutNA = cutNA;
                    //        bool tmpCutIV = cutIV;
                    //        if (cutZeroNAIV)
                    //        {
                    //            if (!tmpCutNA) tmpCutNA = datajagvalue[i][datajagvalue[i].GetUpperBound(0) - 1, c].NumValue == 0.0;
                    //            if (!tmpCutIV) tmpCutIV = datajagvalue[i][datajagvalue[i].GetUpperBound(0), c].NumValue == 0.0;
                    //        }
                    //        if (tmpCutNA)
                    //        {
                    //            cutRowIndexes.Add(r + datajagvalue[i].GetUpperBound(0) - 1);
                    //        }
                    //        if (tmpCutIV)
                    //        {
                    //            cutRowIndexes.Add(r + datajagvalue[i].GetUpperBound(0));
                    //        }
                    //    }
                    //    else    // 三重クロス
                    //    {
                    //        int d = axesgroups[i][1].SectorsCount + 2 + 1;
                    //        int y = AddRowCount;
                    //        for (int j = 0; j < axesgroups[i][0].SectorsCount; ++j)
                    //        {
                    //            y += d;
                    //            bool tmpCutNA = cutNA;
                    //            bool tmpCutIV = cutIV;
                    //            if (cutZeroNAIV)
                    //            {
                    //                if (!tmpCutNA) tmpCutNA = datajagvalue[i][y, c].NumValue == 0.0;
                    //                if (!tmpCutIV) tmpCutIV = datajagvalue[i][y + 1, c].NumValue == 0.0;
                    //            }
                    //            if (tmpCutNA) cutRowIndexes.Add(r + y);
                    //            if (tmpCutIV) cutRowIndexes.Add(r + y + 1);
                    //        }
                    //        // 二重クロス無回答部分
                    //        {
                    //            y += 2;
                    //            bool tmpCutNA = cutNA;
                    //            if (cutZeroNAIV)
                    //            {
                    //                if (!tmpCutNA) tmpCutNA = datajagvalue[i][y, c].NumValue == 0.0;
                    //            }
                    //            if (tmpCutNA)
                    //            {
                    //                --y;
                    //                for (int j = 0; j <= axesgroups[i][1].SectorsCount + 2; ++j)
                    //                {
                    //                    cutRowIndexes.Add(r + ++y);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                y += axesgroups[i][1].SectorsCount + 1;
                    //                bool tmpCutIV = cutIV;
                    //                if (cutZeroNAIV)
                    //                {
                    //                    tmpCutNA = datajagvalue[i][y, c].NumValue == 0.0;
                    //                    if (!tmpCutIV) tmpCutIV = datajagvalue[i][y + 1, c].NumValue == 0.0;
                    //                }
                    //                if (tmpCutNA) cutRowIndexes.Add(r + y);
                    //                ++y;
                    //                if (tmpCutIV) cutRowIndexes.Add(r + y);
                    //            }
                    //        }
                    //        // 二重クロス非該当部分
                    //        {
                    //            ++y;
                    //            bool tmpCutIV = cutIV;
                    //            if (cutZeroNAIV)
                    //            {
                    //                if (!tmpCutIV) tmpCutIV = datajagvalue[i][y, c].NumValue == 0.0;
                    //            }
                    //            if (tmpCutIV)
                    //            {
                    //                --y;
                    //                for (int j = 0; j <= axesgroups[i][1].SectorsCount + 2; ++j)
                    //                {
                    //                    cutRowIndexes.Add(r + ++y);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                y += axesgroups[i][1].SectorsCount + 1;
                    //                bool tmpCutNA = cutNA;
                    //                if (cutZeroNAIV)
                    //                {
                    //                    if (!tmpCutNA) tmpCutNA = datajagvalue[i][y, c].NumValue == 0.0;
                    //                    tmpCutIV = datajagvalue[i][y + 1, c].NumValue == 0.0;
                    //                }
                    //                if (tmpCutNA) cutRowIndexes.Add(r + y);
                    //                if (tmpCutIV) cutRowIndexes.Add(r + y + 1);
                    //            }
                    //        }
                    //    }
                    //    r += datajagvalue[i].GetLength(0);
                    //}
                    if (!cutNA && !cutIV) break;
                    int r = 0;
                    for (int i = 0; i < axesgroups.Count; ++i)
                    {
                        if (axesgroups[i].Count == 1)   // 二重クロス
                        {
                            if (cutNA)
                            {
                                // 無回答行
                                for (int j = 0; j < cutRowCount; ++j)
                                {
                                    cutRowIndexes.Add(r + datajagvalue[i].GetLength(0) - cutRowCount * 2 + j);
                                }
                            }
                            if (cutIV)
                            {
                                // 非該当行
                                for (int j = 0; j < cutRowCount; ++j)
                                {
                                    cutRowIndexes.Add(r + datajagvalue[i].GetLength(0) - cutRowCount + j);
                                }
                            }
                        }
                        else    // 三重クロス
                        {
                            int d = axesgroups[i][1].SectorsCount + 2 + 1;
                            d *= cutRowCount;
                            int y = 0;
                            for (int j = 0; j < axesgroups[i][0].SectorsCount; ++j)
                            {
                                y += d; // 無回答行の先頭インデックス
                                if (cutNA)
                                {
                                    for (int k = 0; k < cutRowCount; ++k)
                                    {
                                        cutRowIndexes.Add(r + y + k);
                                    }
                                }
                                if (cutIV)
                                {
                                    for (int k = 0; k < cutRowCount; ++k)
                                    {
                                        cutRowIndexes.Add(r + y + cutRowCount + k);
                                    }
                                }
                            }
                            y += cutRowCount * 2;   // 二重クロス無回答の先頭行インデックス
                            int z = y + (1 + axesgroups[i][1].SectorsCount + 2) * cutRowCount;  // 二重クロス非該当の先頭行インデックス
                            // 二重クロス無回答部分
                            if (cutNA)
                            {
                                for (int j = y; j < z; ++j)
                                {
                                    cutRowIndexes.Add(r + j);
                                }
                            }
                            else if (cutIV)
                            {
                                // 無回答×非該当部分
                                for (int j = z - cutRowCount; j < z; ++j)
                                {
                                    cutRowIndexes.Add(r + j);
                                }
                            }
                            // 二重クロス非該当部分
                            if (cutIV)
                            {
                                for (int j = z; j < datajagvalue[i].GetLength(0); ++j)
                                {
                                    cutRowIndexes.Add(r + j);
                                }
                            }
                            else if (cutNA)
                            {
                                // 非該当×無回答部分
                                for (int j = datajagvalue[i].GetLength(0) - cutRowCount * 2; j < datajagvalue[i].GetLength(0) - cutRowCount; ++j)
                                {
                                    cutRowIndexes.Add(r + j);
                                }
                            }
                        }
                        r += datajagvalue[i].GetLength(0);
                    }
                }
                {
                    // 2つ目以降のヘッダ部をカット
                    int r = 0;
                    for (int i = 0; i < axesgroups.Count; ++i)
                    {
                        if (i > 0)
                        {
                            for (int j = 0; j < cutRowCount; ++j)
                            {
                                cutRowIndexes.Add(r + j);
                            }
                        }
                        r += datajagvalue[i].GetLength(0);
                    }
                    // ソート
                    cutRowIndexes.Sort();
                    cutColumnIndexes.Sort();
                    // ジャグ配列を結合してdatavalueを生成
                    int AddColumnsCount = maxAxesCount - axesgroups[0].Count;
                    int c = datajagvalue[0].GetLength(1) + AddColumnsCount;
                    datavalue = new Tabulation.DataWithMarking[r - cutRowIndexes.Count, c - cutColumnIndexes.Count];
                    r = -1;
                    int y = -1;
                    for (int i = 0; i < axesgroups.Count; ++i)
                    {
                        for (int j = 0; j < datajagvalue[i].GetLength(0); ++j)
                        {
                            if (cutRowIndexes.BinarySearch(++r) >= 0) continue;
                            ++y;
                            int x = -1;
                            AddColumnsCount = maxAxesCount - axesgroups[i].Count;
                            for (int k = 0; k < 1 + axesgroups[i].Count; ++k)
                            {
                                datavalue[y, ++x] = datajagvalue[i][j, k];
                            }
                            for (int k = 0; k < AddColumnsCount; ++k)
                            {
                                datavalue[y, ++x] = new Tabulation.DataWithMarking(null);
                            }
                            c = x;
                            for (int k = 1 + axesgroups[i].Count; k < datajagvalue[i].GetLength(1); ++k)
                            {
                                if (cutColumnIndexes.BinarySearch(++c) >= 0) continue;
                                datavalue[y, ++x] = datajagvalue[i][j, k];
                            }
                        }
                    }
                }
                */
                #endregion

                bool isN = (questiontype & QuestionType.N) == QuestionType.N;
                // 最小ベースに応じたマーキング情報のカット
                //if (!isN)
                //{
                int minbase = (ParentOutput as Outputs.OutputCross).MinSamplesCountOnMarking;
                bool PreWbBase = (ParentOutput as Outputs.OutputCross).PreWbBase;
                int PreWbBaseIdxAdd = PreWbBase ? 0 : 1;
                for (int x = 0; x < datajagvalue.Length; ++x)
                {
                    for (int r = 2 + GlobalMethodClass.CInt(isN); r < datajagvalue[x].GetLength(0); ++r)
                    {
                        if (null == datajagvalueTotal)
                        {
                            if (datajagvalue[x][r, axesgroups[x].Count + 1 + PreWbBaseIdxAdd].NumValue < (double)minbase || datajagvalue[x][r, axesgroups[x].Count + 1 + PreWbBaseIdxAdd].NumValue == 0)
                            {
                                if (isN)
                                {
                                    int aveIdx = axesgroups[x].Count + 5;
                                    datajagvalue[x][r, aveIdx].ClearAllMarking();
                                    if (datajagvalue[x][r, aveIdx].SettedSectorInformation)
                                    {
                                        int sNo = datajagvalue[x][r, aveIdx].SectorNumber;
                                        int sCnt = datajagvalue[x][r, aveIdx].SectorsCount;
                                        for (int s = 1, j = r - sNo + 1; s <= sCnt; ++s, ++j)
                                        {
                                            datajagvalue[x][j, aveIdx].RemoveSignificanceSectorNumber(sNo);
                                        }
                                    }
                                }
                                else
                                {
                                    int wtaveIdx = datajagvalue[x].GetUpperBound(1);
                                    int naIdx = wtaveIdx - 3;
                                    for (int c = axesgroups[x].Count + 3; c <= wtaveIdx; ++c)
                                    {
                                        datajagvalue[x][r, c].ClearAllMarking();
                                        if (datajagvalue[x][r, c].SettedSectorInformation)
                                        {
                                            int sNo = datajagvalue[x][r, c].SectorNumber;
                                            int sCnt = datajagvalue[x][r, c].SectorsCount;
                                            for (int s = 1, n = r - sNo + 1; s <= sCnt; ++s, ++n)
                                            {
                                                if (s != sNo)
                                                {
                                                    datajagvalue[x][n, c].RemoveSignificanceSectorNumber(sNo);
                                                }
                                            }
                                        }
                                        if (c == naIdx - 1) c = wtaveIdx - 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int wtaveIdx = datajagvalue[x].GetUpperBound(1);
                            int naIdx = wtaveIdx - 3;
                            for (int c = axesgroups[x].Count + 3; c <= wtaveIdx; ++c)
                            {
                                if (datajagvalueUnweightedTotal[x][r, c].NumValue < (double)minbase || datajagvalueUnweightedTotal[x][r, c].NumValue == 0)
                                {
                                    datajagvalue[x][r, c].ClearAllMarking();
                                    if (datajagvalue[x][r, c].SettedSectorInformation)
                                    {
                                        int sNo = datajagvalue[x][r, c].SectorNumber;
                                        int sCnt = datajagvalue[x][r, c].SectorsCount;
                                        for (int s = 1, n = r - sNo + 1; s <= sCnt; ++s, ++n)
                                        {
                                            if (s != sNo)
                                            {
                                                datajagvalue[x][n, c].RemoveSignificanceSectorNumber(sNo);
                                            }
                                        }
                                    }
                                }
                                if (c == naIdx - 1) c = wtaveIdx - 1;
                            }
                        }

                        if (isN)
                        {
                            int aveIdx = axesgroups[x].Count + 5;
                            if (datajagvalue[x][r, aveIdx].SettedSectorInformation)
                            {
                                int sNo = datajagvalue[x][r, aveIdx].SectorNumber;
                                int sCnt = datajagvalue[x][r, aveIdx].SectorsCount;
                                string val = datajagvalue[x][r, aveIdx].Value;
                                if (val == "-")
                                {
                                    for (int s = 1, j = r - sNo + 1; s <= sCnt; ++s, ++j)
                                    {
                                        datajagvalue[x][j, aveIdx].RemoveSignificanceSectorNumber(sNo);
                                    }
                                }
                            }
                        }
                        else
                        {
                            int wtaveIdx = datajagvalue[x].GetUpperBound(1);
                            if (datajagvalue[x][r, axesgroups[x].Count + 3].SettedSectorInformation)
                            {
                                int sNo = datajagvalue[x][r, axesgroups[x].Count + 3].SectorNumber;
                                int sCnt = datajagvalue[x][r, axesgroups[x].Count + 3].SectorsCount;
                                string val = datajagvalue[x][r, wtaveIdx].Value;
                                if (val == "-")
                                {
                                    for (int s = 1, n = r - sNo + 1; s <= sCnt; ++s, ++n)
                                    {
                                        if (s != sNo)
                                        {
                                            datajagvalue[x][n, wtaveIdx].RemoveSignificanceSectorNumber(sNo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //}
                for (int x = 0; x < 1; ++x)
                {
                    if (!(ParentOutput as Outputs.OutputCross).ShowPreWBTotal) cutColumnIndexes.Add(1 + maxAxesCount);  // WB前全体列
                    bool cutNA = !(ParentOutput as Outputs.OutputCross).ShowNAAtItem;
                    bool cutIV = !(ParentOutput as Outputs.OutputCross).ShowIVAtItem;
                    if (isN)
                    {
                        if (!ParentRequest.ShowParameter) cutColumnIndexes.Add(1 + maxAxesCount + 2);   // 統計量母数列
                        if (!ParentRequest.ShowSummary) cutColumnIndexes.Add(1 + maxAxesCount + 3); // 合計列
                        if (!ParentRequest.ShowAverage) cutColumnIndexes.Add(1 + maxAxesCount + 4); // 平均列
                        if (!ParentRequest.ShowStdev) cutColumnIndexes.Add(1 + maxAxesCount + 5);   // 標準偏差列
                        if (!ParentRequest.ShowMinimum) cutColumnIndexes.Add(1 + maxAxesCount + 6); // 最小値列
                        if (!ParentRequest.ShowMaximum) cutColumnIndexes.Add(1 + maxAxesCount + 7); // 最大値列
                        if (!ParentRequest.ShowMedian) cutColumnIndexes.Add(1 + maxAxesCount + 8);  // 中央値列
                        if (cutNA) cutColumnIndexes.Add(1 + maxAxesCount + 9);  // 無回答列
                        if (cutIV) cutColumnIndexes.Add(1 + maxAxesCount + 10); // 非該当列
                    }
                    else    // SA/MA
                    {
                        totalIndex = 2;
                        sectorStartIndex = 1 + maxAxesCount + 2;
                        int AddColumnsCount = GlobalMethodClass.CInt(axesgroups[0].Count < maxAxesCount) & 1;
                        int NAIdx = datajagvalue[0].GetUpperBound(1) - 3 + AddColumnsCount;
                        sectorEndIndex = NAIdx - 1;
                        if (cutColumnIndexes.Count > 0) // WB前全体列がカットされる
                        {
                            --sectorStartIndex;
                            --sectorEndIndex;
                        }
                        if (cutNA) cutColumnIndexes.Add(NAIdx);    // 無回答列
                        if (cutIV) cutColumnIndexes.Add(NAIdx + 1);    // 非該当列
                        // ウエイト
                        bool cutWT = true;
                        for (int i = 1 + axesgroups[0].Count + 2; i < datajagvalue[0].GetLength(1) - 4; ++i)
                        {
                            string strWT = datajagvalue[0][1, i].Value;
                            double wt = 0.0;
                            if (double.TryParse(strWT, out wt))
                            {
                                cutWT = false;
                                break;
                            }
                        }
                        if (cutWT)
                        {
                            //cutRowIndexes.Add(1);   // ウエイト行
                            //--totalIndex;
                            cutColumnIndexes.Add(datajagvalue[0].GetUpperBound(1) - 1 + AddColumnsCount);   // 加重平均母数列
                            cutColumnIndexes.Add(datajagvalue[0].GetUpperBound(1) + AddColumnsCount);   // 加重平均列
                        }
                    }
                    // 三重クロスで出力しない行
                    int r = 0;
                    for (int i = 0; i < axesgroups.Count; ++i)
                    {
                        if (axesgroups[i].Count == 2)
                        {
                            int d = axesgroups[i][1].SectorsCount + 2 + 2;
                            //cutRowIndexes.Add(r + (isN ? 2 : 3));   // 小計行
                            for (int j = isN ? 3 : 4; j < datajagvalue[i].GetLength(0); j += d)
                            {
                                cutRowIndexes.Add(r + j);   // 表肩質問選択肢全体行
                            }
                        }
                        r += datajagvalue[i].GetLength(0);
                    }
                    // 軸の無回答/非該当
                    cutNA = !(ParentOutput as Outputs.OutputCross).ShowNAAtAxis;
                    cutIV = !(ParentOutput as Outputs.OutputCross).ShowIVAtAxis;
                    if (!cutNA && !cutIV) break;
                    r = 0;
                    for (int i = 0; i < axesgroups.Count; ++i)
                    {
                        if (axesgroups[i].Count == 1)   // 二重クロス
                        {
                            if (cutNA)
                            {
                                // 無回答行
                                cutRowIndexes.Add(r + datajagvalue[i].GetLength(0) - 2);
                            }
                            if (cutIV)
                            {
                                // 非該当行
                                cutRowIndexes.Add(r + datajagvalue[i].GetLength(0) - 1);
                            }
                        }
                        else    // 三重クロス
                        {
                            int d = axesgroups[i][1].SectorsCount + 2 + 2;
                            int y = isN ? 1 : 2;
                            for (int j = 0; j < axesgroups[i][0].SectorsCount; ++j)
                            {
                                y += d; // 無回答行のインデックス
                                if (cutNA) cutRowIndexes.Add(r + y);
                                if (cutIV) cutRowIndexes.Add(r + y + 1);
                            }
                            y += 3; // 二重クロス無回答の2行目インデックス
                            int z = y + d;  // 二重クロス非該当の2行目インデックス
                            // 二重クロス無回答部分
                            if (cutNA)
                            {
                                for (int j = 0; j < d - 1; ++j)
                                {
                                    cutRowIndexes.Add(r + y + j);
                                }
                            }
                            else if (cutIV)
                            {
                                // 無回答×非該当部分
                                cutRowIndexes.Add(r + z - 2);
                            }
                            // 二重クロス非該当部分
                            if (cutIV)
                            {
                                for (int j = 0; j < d - 1; ++j)
                                {
                                    cutRowIndexes.Add(r + z + j);
                                }
                            }
                            else if (cutNA)
                            {
                                // 非該当×無回答部分
                                cutRowIndexes.Add(r + z + d - 2);
                            }
                        }
                        r += datajagvalue[i].GetLength(0);
                    }
                }
                {
                    // 2つ目以降のヘッダ部をカット
                    int r = 0;
                    for (int i = 0; i < axesgroups.Count; ++i)
                    {
                        AxesInformation Ax = axesgroups[i];
                        if (i > 0)
                        {
                            int simpleAggrAdjust = 0;
                            if (Ax[0].SectorsCount == 0)
                            {
                                simpleAggrAdjust = 1;
                            }
                            cutRowIndexes.Add(r);
                            if (isN)
                                cutRowIndexes.Add(r + 1 + simpleAggrAdjust);   // ウエイト行(SA/MA)または全体行(N)
                            else
                                cutRowIndexes.Add(r + 1);
                            if (!isN) cutRowIndexes.Add(r + 2 + simpleAggrAdjust);   // 全体行(SA/MA)
                        }
                        r += datajagvalue[i].GetLength(0);
                    }
                    // ソート
                    cutRowIndexes.Sort();
                    cutColumnIndexes.Sort();
                    // ジャグ配列を結合してdatavalueを生成
                    int AddColumnsCount = maxAxesCount - axesgroups[0].Count;
                    int c = datajagvalue[0].GetLength(1) + AddColumnsCount;
                    datavalue = new Tabulation.DataWithMarking[r + (isN ? 1 : 0) - cutRowIndexes.Count, c - cutColumnIndexes.Count];
                    r = -1;
                    int y = -1;
                    for (int i = 0; i < axesgroups.Count; ++i)
                    {
                        string[] dupBuf = null;
                        bool[] dupIsCaption = null;
                        for (int j = 0; j < datajagvalue[i].GetLength(0); ++j)
                        {
                            if (cutRowIndexes.BinarySearch(++r) >= 0)
                            {
                                dupBuf = new string[1 + axesgroups[i].Count];
                                dupIsCaption = new bool[dupBuf.Length];
                                for (int k = 0; k < dupBuf.Length; ++k)
                                {
                                    dupBuf[k] = datajagvalue[i][j, k].Value;
                                    dupIsCaption[k] = datajagvalue[i][j, k].CellType == CellType.CaptionCell;
                                }
                                continue;
                            }
                            ++y;
                            int x = -1;
                            AddColumnsCount = maxAxesCount - axesgroups[i].Count;

                            if (isN && y == 1)
                            {
                                ++y;
                            }

                            for (int k = 0; k < 1 + axesgroups[i].Count; ++k)
                            {
                                if (dupBuf == null || !string.IsNullOrEmpty(datajagvalue[i][j, k].Value))
                                {
                                    datavalue[y, ++x] = datajagvalue[i][j, k];
                                }
                                else
                                {
                                    datavalue[y, ++x] = new Tabulation.DataWithMarking(dupBuf[k], !dupIsCaption[k]);
                                }
                            }
                            for (int k = 0; k < AddColumnsCount; ++k)
                            {
                                datavalue[y, ++x] = new Tabulation.DataWithMarking(null, false);
                            }
                            c = x;
                            for (int k = 1 + axesgroups[i].Count; k < datajagvalue[i].GetLength(1); ++k)
                            {
                                if (cutColumnIndexes.BinarySearch(++c) >= 0) continue;
                                datavalue[y, ++x] = datajagvalue[i][j, k];
                            }
                            dupBuf = null;
                            dupIsCaption = null;
                        }
                    }
                }
            }

            internal string OutputToTSV(string information, AxesGroupInformation axesgroups = null, int[] sortSectorIndexes = null)
            {
                switch (ParentOutput.OutputType)
                {
                    case OutputType.GT:
                        if (datavalue == null)
                        {
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullDataMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , "datavalue");
                        }
                        if (sortSectorIndexes != null)
                        {
                            if ((int)(questiontype & (QuestionType.SA | QuestionType.MA)) != 0)
                            {
                                if ((int)(questiontype & QuestionType.MatrixParent) == 0)
                                {
                                    if ((questiontype & QuestionType.Sort) == QuestionType.Sort)
                                    {
                                        GlobalMethodClass.SectorIncreaseDirection secIncDirection = GlobalMethodClass.SectorIncreaseDirection.TopToBottom;
                                        int dataColumnIndex = datavalue.GetUpperBound(1);
                                        datavalue.StableSort(sectorStartIndex, sectorEndIndex, totalIndex, sortSectorIndexes, secIncDirection, true, 0, 1, dataColumnIndex);
                                    }
                                }
                            }
                        }
                        break;
                    case OutputType.Cross:
                        if (datajagvalue == null || axesgroups == null)
                        {
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullDataMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , datajagvalue == null ? "datajagvalue" : "axesgroups");
                        }
                        if (datajagvalue.Length != axesgroups.Count)
                        {
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.ExistMismatchBetweenDatasMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL);
                        }
                        // カット行列算出→datavalue生成
                        JagToNormalDataValue(axesgroups);
                        if (sortSectorIndexes != null)
                        {
                            if ((int)(questiontype & (QuestionType.SA | QuestionType.MA)) != 0)
                            {
                                if ((questiontype & QuestionType.Sort) == QuestionType.Sort)
                                {
                                    datavalue.StableSort(sectorStartIndex, sectorEndIndex, totalIndex, sortSectorIndexes);
                                }
                            }
                        }
                        break;
                    case OutputType.FAList:
                    case OutputType.CheckList:
                    case OutputType.RawData:
                    case OutputType.QC3:
                        if (strvalue == null)
                        {
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullDataMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , "strvalue");
                        }
                        break;
                    case OutputType.Questionnaire:
                        if (information == null) return null;
                        break;
                    case OutputType.CheckTemplate:
                        return null;
                    default:
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustOutputTypeMessageIndex)
                                               , GlobalsCommonConstant.LogLevel.FATAL
                                               , ParentOutput.OutputType.ToString());
                }

                string tsvpath =
                    System.IO.Path.Combine(
                        (ParentRequest as Request).AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP)
                        , (ParentRequest as Request).QCWebID.ToString()
                        , (ParentRequest as Request).ID.ToString(), ParentReportset.ID.ToString()
                    );
                GlobalMethodClass.GuaranteeDirectoryExist(tsvpath);
                tsvpath = System.IO.Path.Combine(tsvpath, (ParentOutput as Outputs.Output).Order + "_" + this.index + ".tsv");
                int subTotalLenth = 0;

                bool showNA = false;
                if (ParentOutput.OutputType == OutputType.Cross)
                    showNA = (ParentOutput as Outputs.OutputCross).ShowNAAtItem;
                else if (ParentOutput.OutputType == OutputType.GT)
                    showNA = (ParentOutput as Outputs.OutputGT).ShowNAAtItem;

                if (this is CrossTable && showNA)
                {
                    CrossTable crTable = (CrossTable)this;
                    subTotalLenth = crTable.Question.SubTotalCnt;
                }
                else if (this.GetType() == typeof(GTTable) && showNA)
                {
                    GTTable gTTable = (GTTable)this;
                    subTotalLenth = gTTable.Question.SubTotalCnt;
                }
                try
                {
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(tsvpath, false, Encoding.UTF8))
                    {
                        if (information != null) writer.WriteLine(information);
                        if (datavalue != null)
                        {
                            for (int i = 0; i < datavalue.GetLength(0); ++i)
                            {
                                StringBuilder rowBuffer = new StringBuilder("");
                                if (subTotalLenth > 0)
                                {
                                    if (this.GetType() == typeof(GTTable))
                                    {
                                        int gtSectorEndIndex = sectorEndIndex; // There is issue with sectorEndIndex
                                        GTTable gTTable = (GTTable)this;
                                        if ((gTTable.Question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                                        {
                                            if ((gTTable.Question.QuestionType & QuestionType.MA) == QuestionType.MA || (gTTable.Question.QuestionType & QuestionType.SA) == QuestionType.SA || (gTTable.Question.QuestionType & QuestionType.Rank) == QuestionType.Rank)
                                            {
                                                int gtSubTotalStartIndex = gtSectorEndIndex - (subTotalLenth);
                                                int gtNAIndex = gtSectorEndIndex;

                                                int j = 0;
                                                for (; j <= (gtSubTotalStartIndex - 1); ++j) // All sectors
                                                {
                                                    rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                                    rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                                    rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());
                                                    rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());
                                                }

                                                j = gtNAIndex; // NA Data
                                                rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                                rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                                rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());
                                                rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());

                                                int headingRowIndex = 0;
                                                bool removedSlnoCol = false;
                                                for (j = gtSubTotalStartIndex; j <= gtNAIndex - 1; ++j) // Sub Total
                                                {
                                                    if (i == headingRowIndex && !removedSlnoCol)
                                                        rowBuffer.Append("\t" + string.Empty);
                                                    else
                                                        rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));

                                                    rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                                    rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());

                                                    if (gTTable.SignificancetestCode == SignificanceTestCode.BetweenChildQuestions)
                                                        rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());
                                                    else
                                                        rowBuffer.Append("\v" + string.Empty);
                                                }

                                                for (j = gtSectorEndIndex + 1; j < datavalue.GetLength(1); ++j) // Rest of the columns like mean, average
                                                {
                                                    rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                                    rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                                    rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());
                                                    rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());
                                                }
                                            }
                                            else
                                            {
                                                for (int j = 0; j < datavalue.GetLength(1); ++j)
                                                {
                                                    rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                                    rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                                    rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());
                                                    rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            int gtSubTotalStartIndex = gtSectorEndIndex - (subTotalLenth - 1);
                                            int gtNAIndex = gtSectorEndIndex + 1;
                                            if (i == gtSubTotalStartIndex)
                                            {
                                                for (int j = 0; j < datavalue.GetLength(1); ++j)// Bringing NA to Front
                                                {
                                                    rowBuffer.Append("\t" + (double.IsNaN(datavalue[gtNAIndex, j].NumValue) ? Regex.Escape(datavalue[gtNAIndex, j].Value + string.Empty) : datavalue[gtNAIndex, j].NumValue.ToString()));
                                                    rowBuffer.Append("\v" + datavalue[gtNAIndex, j].Percent.ToString());
                                                    rowBuffer.Append("\v" + ((int)datavalue[gtNAIndex, j].Marking).ToString());
                                                    rowBuffer.Append("\v" + datavalue[gtNAIndex, j].SignificanceCharacters());
                                                }
                                                writer.WriteLine(rowBuffer.ToString().Substring(1)); // Inserting NA Row to tsv
                                                rowBuffer = new StringBuilder("");

                                                bool removedSlnoCol = false;
                                                for (int j = 0; j < datavalue.GetLength(1); ++j)// Inserting SubTotal rows
                                                {
                                                    if (!removedSlnoCol)
                                                    {
                                                        rowBuffer.Append("\t" + string.Empty);
                                                        removedSlnoCol = true;
                                                    }
                                                    else
                                                    {
                                                        rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                                    }
                                                    rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                                    rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());
                                                    //rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());
                                                    rowBuffer.Append("\v" + string.Empty); //#OutputFormatting Forremoving char against subtotal
                                                }
                                            }
                                            else if (i == gtNAIndex)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                bool removedSlnoCol = false;
                                                for (int j = 0; j < datavalue.GetLength(1); ++j)
                                                {
                                                    if ((!removedSlnoCol) && (i >= gtSubTotalStartIndex && i < gtNAIndex))
                                                    {
                                                        rowBuffer.Append("\t" + string.Empty);
                                                        removedSlnoCol = true;
                                                    }
                                                    else
                                                    {
                                                        rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                                    }
                                                    rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                                    rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());

                                                    if (i >= gtSubTotalStartIndex && i <= gtSectorEndIndex)
                                                        rowBuffer.Append("\v" + string.Empty); //#OutputFormatting Forremoving char against subtotal
                                                    else
                                                        rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int j = 0;
                                        for (; j <= sectorEndIndex - subTotalLenth; ++j)
                                        {
                                            rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                            rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                            rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());
                                            rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());
                                        }
                                        j = sectorEndIndex + 1;
                                        rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                        rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                        rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());
                                        rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());

                                        for (j = sectorEndIndex + 1 - subTotalLenth; j <= sectorEndIndex; ++j)
                                        {
                                            rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                            rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                            rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());
                                            rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());
                                        }
                                        for (j = sectorEndIndex + 2; j < datavalue.GetLength(1); ++j)
                                        {
                                            rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                            rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                            rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());
                                            rowBuffer.Append("\v" + datavalue[i, j].SignificanceCharacters());
                                        }
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < datavalue.GetLength(1); ++j)
                                    {
                                        rowBuffer.Append("\t" + (double.IsNaN(datavalue[i, j].NumValue) ? Regex.Escape(datavalue[i, j].Value + string.Empty) : datavalue[i, j].NumValue.ToString()));
                                        rowBuffer.Append("\v" + datavalue[i, j].Percent.ToString());
                                        rowBuffer.Append("\v" + ((int)datavalue[i, j].Marking).ToString());
                                        rowBuffer.Append("\v" + (datavalue[i, j].SignificanceCharacters()));
                                    }
                                }
                                writer.WriteLine(rowBuffer.ToString().Substring(1));
                            }

                        }
                        else if (strvalue != null)
                        {
                            for (int i = 0; i < strvalue.GetLength(0); ++i)
                            {
                                StringBuilder rowBuffer = new StringBuilder("");
                                for (int j = 0; j < strvalue.GetLength(1); ++j)
                                {
                                    rowBuffer.Append("\t" + Regex.Escape(strvalue[i, j] + string.Empty));
                                }
                                writer.WriteLine(rowBuffer.ToString().Substring(1));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    try
                    {
                        System.IO.File.Delete(tsvpath);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", ex.GetType().ToString());
                        Debug.WriteLine("Description:{0}", ex.Message);
                        Debug.Unindent();
                    }
                    throw;
                }
                return tsvpath;
            }

            /// <summary>
            /// インデックス番号を返す読み取り専用プロパティ
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
            /// <param name="Unescape">アンエスケープ処理を行うかどうかを示すフラグ (省略可、既定値false)</param>
            /// <returns>
            /// 集計表データ内で<paramref name="RowIndex"/>と<paramref name="ColumnIndex"/>とで示されるセルの文字列データまたはN値の文字列表現
            /// <paramref name="Unescape"/>がtrueのときには、正規表現のアンエスケープ処理を行ってから返す
            /// </returns>
            public string TableValue(int RowIndex, int ColumnIndex, bool Unescape = false)
            {
                if (datavalue == null && strvalue == null) return null;
                string res = null;
                if (datavalue != null)
                {
                    if (RowIndex < datavalue.GetLowerBound(0) || RowIndex > datavalue.GetUpperBound(0)
                        || ColumnIndex < datavalue.GetLowerBound(1) || ColumnIndex > datavalue.GetUpperBound(1))
                    {
                        return null;
                    }
                    res = datavalue[RowIndex, ColumnIndex].Value;
                }
                else if (strvalue != null)
                {
                    if (RowIndex < strvalue.GetLowerBound(0) || RowIndex > strvalue.GetUpperBound(0)
                        || ColumnIndex < strvalue.GetLowerBound(1) || ColumnIndex > strvalue.GetUpperBound(1))
                    {
                        return null;
                    }
                    res = strvalue[RowIndex, ColumnIndex];
                }
                if (string.IsNullOrWhiteSpace(res)) return null;
                if (Unescape)
                {
                    return Regex.Unescape(res);
                }
                else
                {
                    return res;
                }
            }

            /// <summary>
            /// 集計表のセルの％値を返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内でRowIndexとColumnIndexとで示されるセルの％値</returns>
            public double PercentValue(int RowIndex, int ColumnIndex)
            {
                if (datavalue == null) return 0.0;
                if (RowIndex < datavalue.GetLowerBound(0) || RowIndex > datavalue.GetUpperBound(0)
                    || ColumnIndex < datavalue.GetLowerBound(1) || ColumnIndex > datavalue.GetUpperBound(1))
                {
                    return 0.0;
                }
                return datavalue[RowIndex, ColumnIndex].Percent;
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
                if (datavalue == null) return res0;
                if (RowIndex < datavalue.GetLowerBound(0) || RowIndex > datavalue.GetUpperBound(0)
                    || ColumnIndex < datavalue.GetLowerBound(1) || ColumnIndex > datavalue.GetUpperBound(1))
                {
                    return res0;
                }
                return datavalue[RowIndex, ColumnIndex].Marking;
            }

            /// <summary>
            /// 集計表のセルの項目間検定レターを返すメソッド
            /// </summary>
            /// <param name="RowIndex">行インデックス</param>
            /// <param name="ColumnIndex">列インデックス</param>
            /// <returns>集計表データ内でRowIndexとColumnIndexとで示されるデータの項目間検定レター</returns>
            public string SignificanceTestCharacters(int RowIndex, int ColumnIndex)
            {
                if (datavalue == null) return null;
                if (RowIndex < datavalue.GetLowerBound(0) || RowIndex > datavalue.GetUpperBound(0)
                    || ColumnIndex < datavalue.GetLowerBound(1) || ColumnIndex > datavalue.GetUpperBound(1))
                {
                    return null;
                }
                return datavalue[RowIndex, ColumnIndex].SignificanceCharacters();
            }

            /// <summary>
            /// 集計表データの行インデックスの最小値を返す読み取り専用プロパティ
            /// </summary>
            public int GetTableValueRowIndexMinimum
            {
                get
                {
                    if (datavalue == null && strvalue == null) return -1;
                    if (datavalue != null)
                    {
                        return datavalue.GetLowerBound(0);
                    }
                    else
                    {
                        return strvalue.GetLowerBound(0);
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
                    if (datavalue == null && strvalue == null) return -1;
                    if (datavalue != null)
                    {
                        return datavalue.GetUpperBound(0);
                    }
                    else
                    {
                        return strvalue.GetUpperBound(0);
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
                    if (datavalue == null && strvalue == null) return -1;
                    if (datavalue != null)
                    {
                        return datavalue.GetLowerBound(1);
                    }
                    else
                    {
                        return strvalue.GetLowerBound(1);
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
                    if (datavalue == null && strvalue == null) return -1;
                    if (datavalue != null)
                    {
                        return datavalue.GetUpperBound(1);
                    }
                    else
                    {
                        return strvalue.GetUpperBound(1);
                    }
                }
            }

            /// <summary>
            /// コメントを取得/設定するプロパティ<br />
            /// 設定は1度だけ可能
            /// </summary>
            public string Comment
            {
                get
                {
                    return comment;
                }
                protected set
                {
                    if (comment == null) comment = value;
                }
            }

            /// <summary>
            /// 自身のインスタンスが格納されているTablesコレクションクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public ITables ParentCollection
            {
                get
                {
                    return Collection;
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

            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                Collection = null;
            }
        }

        /// <summary>
        /// 出力命令側から扱うGT表の集計表を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("EF526519-DD00-45ce-A67D-285BD6441A79")]
        public class GTTable : Table, IGTTable
        {
            /// <summary>
            /// 分類アイテムの簡易情報を保持するprotected変数
            /// </summary>
            protected KeyItemInformation keyiteminformation = null;
            /// <summary>
            /// 集計対象質問の簡易情報を保持するprotected変数
            /// </summary>
            protected QuestionInformation questioninformation = null;
            /// <summary>
            /// 項目間検定の種類を表すコードを保持するprotected変数
            /// </summary>
            protected SignificanceTestCode significancetestcode = SignificanceTestCode.Off;
            private int childquestionscount = 0;
            /// <summary>
            /// グラフの簡易情報を保持するprotected変数
            /// </summary>
            protected ChartInformation chartinformation = null;
            /// <summary>
            /// 円グラフの出力時に、選択肢名を非表示にする最大パーセンテージを保持するprotected変数
            /// </summary>
            protected int hidechartdescriptionmaxpercent = -1;
            /// <summary>
            /// 集計対象質問の選択肢情報を保持するprotected変数
            /// </summary>
            protected SectorInformation[] sectorinformations = null;

            internal GTTable(Tables tables, Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[,] wholeArray) : base(tables, questiontype, wholeArray: wholeArray) { }

            internal GTTable(Tables tables, Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[][,] wholeJagArray) : base(tables, questiontype, wholeJagArray: wholeJagArray) { }

            internal GTTable(Tables tables, Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[][,] wholeJagArray,
                Tabulation.DataWithMarking[][,] wholeJagArrayTotal, Tabulation.DataWithMarking[][,] wholeJagArrayUnweightedTotal = null) : base(tables, questiontype, wholeJagArray: wholeJagArray, wholeJagArrayTotal: wholeJagArrayTotal, wholeJagArrayUnweightedTotal: wholeJagArrayUnweightedTotal) { }

            private string CreateInformationBuffer()
            {
                // TSVの集計表設定情報ブロック文字列を生成
                if (questioninformation == null) return null;
                if ((questioninformation.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent && childquestionscount == 0) return null;
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.N)) == 0) return null;
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0
                    && (sectorinformations == null || sectorinformations.Length == 0)) return null;
                StringBuilder infoBuf = new StringBuilder("");
                // 分類アイテム情報部
                string[] tmpBuf = new string[4];
                if (keyiteminformation == null)
                {
                    for (int i = 0; i < tmpBuf.Length; ++i)
                    {
                        tmpBuf[i] = "";
                    }
                }
                else
                {
                    tmpBuf[0] = keyiteminformation.Name;
                    tmpBuf[1] = Regex.Escape(keyiteminformation.Description + string.Empty);
                    tmpBuf[2] = keyiteminformation.SectorNumber.ToString();
                    tmpBuf[3] = Regex.Escape(keyiteminformation.SectorDescription + string.Empty);
                }
                infoBuf.Append(string.Join("\t", tmpBuf));
                // 集計アイテム情報部
                tmpBuf = new string[10];
                tmpBuf[0] = questioninformation.Name;
                tmpBuf[1] = Regex.Escape(questioninformation.Description + string.Empty);
                tmpBuf[2] = ((int)questioninformation.QuestionType).ToString();
                tmpBuf[3] = ((int)significancetestcode).ToString();
                tmpBuf[4] = questioninformation.HasCount.ToString();
                tmpBuf[5] = questioninformation.SubTotalCnt.ToString();
                tmpBuf[6] = questioninformation.HasWeight.ToString();
                tmpBuf[7] = questioninformation.WBValue;
                tmpBuf[8] = questioninformation.TabulateFullQuantity.ToString();
                tmpBuf[9] = Regex.Escape(questioninformation.TableHeading + string.Empty);

                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                // 子質問数部
                infoBuf.Append("\f" + childquestionscount.ToString());
                // コメント情報部
                infoBuf.Append("\f" + Regex.Escape(this.Comment + string.Empty));
                if ((questioninformation.QuestionType & Tabulation.QuestionType.N) == Tabulation.QuestionType.N
                    && (questioninformation.QuestionType & Tabulation.QuestionType.Ratio) != Tabulation.QuestionType.Ratio)
                {
                    return infoBuf.ToString();
                }
                // グラフ情報部
                tmpBuf = new string[4];
                if (chartinformation == null)
                {
                    tmpBuf[0] = "0";
                }
                else
                {
                    tmpBuf[0] = ((int)chartinformation.ChartType).ToString();
                }
                if (tmpBuf[0].Equals("0"))
                {
                    tmpBuf[1] = "";
                    tmpBuf[2] = "";
                    tmpBuf[3] = "";
                }
                else
                {
                    string[] colorindexBuf = new string[chartinformation.SeriesCount];
                    for (int i = 0; i < colorindexBuf.Length; ++i)
                    {
                        colorindexBuf[i] = chartinformation.SeriesColorIndex(i).ToString();
                    }
                    tmpBuf[1] = string.Join(" ", colorindexBuf);
                    tmpBuf[2] = ((int)chartinformation.GradientStyle).ToString() + " " + chartinformation.GradientVariant.ToString();
                    tmpBuf[3] = hidechartdescriptionmaxpercent.ToString();
                }
                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) == 0)
                {
                    return infoBuf.ToString();
                }
                // 選択肢情報部
                Array.Resize<string>(ref tmpBuf, sectorinformations.Length + 1);
                tmpBuf[0] = sectorinformations.Length.ToString();
                for (int i = 0; i < sectorinformations.Length; ++i)
                {
                    tmpBuf[i + 1] = sectorinformations[i].Weight + "\v" + (sectorinformations[i].IsUnsort ? "1" : "0");
                }
                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                return infoBuf.ToString();
            }

            internal string OutputToTSV()
            {
                string information = CreateInformationBuffer();
                if (information == null)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullDataMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "information");
                }
                if ((int)(questioninformation.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                {
                    if ((int)(questioninformation.QuestionType & QuestionType.MatrixParent) == 0)
                    {
                        if ((questioninformation.QuestionType & QuestionType.Sort) == QuestionType.Sort)
                        {
                            List<int> sortSectors = new List<int>();
                            for (int i = 0; i < sectorinformations.Length; ++i)
                            {
                                if (!sectorinformations[i].IsUnsort) sortSectors.Add(i);
                            }
                            if (sortSectors.Count > 1)
                            {
                                return this.OutputToTSV(information, null, sortSectors.ToArray());
                            }
                        }
                    }
                }
                return this.OutputToTSV(information);
            }

            /// <summary>
            /// 分類アイテム情報を設定するメソッド<br />
            /// 設定が有効なのは1度だけ
            /// </summary>
            public void SetKeyItemInformation(string name, string description, int sectornumber, string sectordescription)
            {
                if (keyiteminformation == null && !string.IsNullOrWhiteSpace(name))
                {
                    keyiteminformation = new KeyItemInformation(name, description, sectornumber, sectordescription);
                }
            }

            /// <summary>
            /// 集計対象質問情報を設定するメソッド<br />
            /// 設定が有効なのは1度だけ
            /// </summary>
            /// <param name="name">アイテム名</param>
            /// <param name="description">質問文</param>
            /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
            public void SetQuestionInformation(string name, string description, Tabulation.QuestionType questiontype, bool hasCount = false, string summaryTableName = null, int subTotalCnt = 0, bool hasWeight = false, string narrowingCondition = null, string tableHeading = null, string qNumber = null,string wBValue = null,bool tabulateFullQuantity = false)
            {
                if (questioninformation == null)
                {
                    questioninformation = new QuestionInformation(name, description, questiontype, hasCount: hasCount, summaryTableName: summaryTableName, subTotalCnt: subTotalCnt, hasWeight: hasWeight, narrowingCondition: narrowingCondition, tableHeading: tableHeading, qNumber: qNumber, wBValue: wBValue, tabulateFullQuantity: tabulateFullQuantity);
                }
            }

            /// <summary>
            /// グラフ情報を設定するメソッド<br />
            /// 設定が有効なのは1度だけ
            /// </summary>
            /// <param name="charttype">グラフの種類を表すXlChartType列挙型の値</param>
            /// <param name="joinedseriescolorindex">各系列色を表すインデックス番号を半角スペース区切りで連結した文字列</param>
            /// <param name="gradientstyle">グラデーションのスタイルを表すMsoGradientStyle列挙型の値</param>
            /// <param name="gradientvariant">グラデーションのバリエーションを表す1～4の整数値</param>
            /// <param name="isMatrix">マトリクスの場合trueを指定 (省略可、既定値false)</param>
            public void SetChartInformation(Common.XlChartType charttype, string joinedseriescolorindex, Common.MsoGradientStyle gradientstyle, int gradientvariant, bool isMatrix = false)
            {
                if (chartinformation == null)
                {
                    chartinformation = new ChartInformation(charttype, joinedseriescolorindex, gradientstyle, gradientvariant, isMatrix);
                }
            }

            /// <summary>
            /// 選択肢情報を追加するメソッド
            /// </summary>
            /// <param name="weight">ウエイト値</param>
            /// <param name="unsortflag">並べ替え対象外フラグ</param>
            public void AddSectorInformation(string weight, bool unsortflag)
            {
                if (sectorinformations == null)
                {
                    sectorinformations = new SectorInformation[1];
                }
                else
                {
                    Array.Resize<SectorInformation>(ref sectorinformations, sectorinformations.Length + 1);
                }
                sectorinformations[sectorinformations.GetUpperBound(0)] = new SectorInformation(weight, unsortflag);
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
            /// 項目間検定の種類を表すSignificanceTestCode列挙型の値を取得/設定するプロパティ
            /// <note>
            /// 設定の前にSetQuestionInformationメソッドを実行して、集計対象質問情報を設定しておく必要がある
            /// </note>
            /// </summary>
            public virtual SignificanceTestCode SignificancetestCode
            {
                get
                {
                    return significancetestcode;
                }
                set
                {
                    if (questioninformation == null) return;
                    if (value == SignificanceTestCode.Off)
                    {
                        significancetestcode = value;
                        return;
                    }
                    if ((questioninformation.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
                    {
                        switch (value)
                        {
                            case SignificanceTestCode.BetweenSectors:
                            case SignificanceTestCode.BetweenChildQuestions:
                                significancetestcode = value;
                                break;
                        }
                    }
                    else if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0)
                    {
                        if (value == SignificanceTestCode.BetweenSectors) significancetestcode = value;
                    }
                }
            }

            /// <summary>
            /// 集計対象アイテムの子質問数を取得/設定するプロパティ<br />
            /// マトリクス以外では0
            /// <note>
            /// 設定の前にSetQuestionInformationメソッドを実行して、集計対象質問情報を設定しておく必要がある
            /// </note>
            /// </summary>
            public int ChildQuestionsCount
            {
                get
                {
                    return childquestionscount;
                }
                set
                {
                    if (questioninformation == null) return;
                    if ((questioninformation.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
                    {
                        if (value > 0) childquestionscount = value;
                    }
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
            /// 円グラフの出力時に、選択肢名を非表示にする最大パーセンテージを、-1～50の整数で取得/設定するプロパティ<br />
            /// 既定値-1
            /// </summary>
            public int HideChartDescriptionMaxPercent
            {
                get
                {
                    return hidechartdescriptionmaxpercent;
                }
                set
                {
                    int upper = int.Parse(GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportHidePieChartLabelDescriptionPercentUpperLimitIndex));
                    if (value < -1 || value > upper) return;
                    hidechartdescriptionmaxpercent = value;
                }
            }

            /// <summary>
            /// 集計対象質問の選択肢の簡易情報を保持したSectorInformationクラスのインスタンスへの参照を返すメソッド
            /// </summary>
            /// <param name="index">選択肢のインデックス</param>
            /// <returns>インデックスが示す選択肢の簡易情報を保持したSectorInformationクラスのインスタンスへの参照</returns>
            public SectorInformation Sector(int index)
            {
                if (sectorinformations == null) return null;
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
            /// 選択肢数を返す読み取り専用プロパティ
            /// </summary>
            public int SectorsCount
            {
                get
                {
                    if (sectorinformations == null) return 0;
                    return sectorinformations.Length;
                }
                set { }
            }

            /// <summary>
            /// コメントを設定するメソッド
            /// <note>設定するコメントのエスケープ処理は、出力時に内部で行うので、設定時には行わないこと</note>
            /// </summary>
            public void SetComment(string comment)
            {
                if (ParentReportset == null) return;
                if ((ParentReportset.FileType & FileType.Report) == FileType.Report)
                {
                    this.Comment = comment;
                }
            }
        }

        /// <summary>
        /// 出力命令側から扱うクロス表の集計表を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("FDDE81E9-E259-4d57-AA8D-0827FC078B3E")]
        public class CrossTable : GTTable, ICrossTable
        {
            private AxesGroupInformation axesgroups = null;
            private int[] linechartrows = null;

            internal CrossTable(Tables tables, Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[,] wholeArray) : base(tables, questiontype, wholeArray: wholeArray)
            {
                axesgroups = new AxesGroupInformation();
            }

            internal CrossTable(Tables tables, Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[][,] wholeJagArray) : base(tables, questiontype, wholeJagArray)
            {
                axesgroups = new AxesGroupInformation();
            }

            internal CrossTable(Tables tables, Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[][,] wholeJagArray,
                Tabulation.DataWithMarking[][,] wholeJagArrayTotal, Tabulation.DataWithMarking[][,] wholeJagArrayUnweightedTotal=null) : base(tables, questiontype, wholeJagArray, wholeJagArrayTotal, wholeJagArrayUnweightedTotal)
            {
                axesgroups = new AxesGroupInformation();
            }

            private string CreateInformationBuffer()
            {
                // TSVの集計表設定情報ブロック文字列を生成
                if (questioninformation == null || axesgroups == null || axesgroups.Count == 0) return null;
                if ((questioninformation.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent) return null;
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.N)) == 0) return null;
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0
                    && (sectorinformations == null || sectorinformations.Length == 0)) return null;
                for (int i = 0; i < axesgroups.Count; ++i)
                {
                    if (axesgroups[i] == null) return null;
                    // 二重クロス or 三重クロス
                    if (axesgroups[i].Count != 1 && axesgroups[i].Count != 2) return null;
                    for (int j = 0; j < axesgroups[i].Count; ++j)
                    {
                        // if (axesgroups[i][j].SectorsCount < 1) return null;
                    }
                }
                StringBuilder infoBuf = new StringBuilder("");
                string[] tmpBuf = new string[4];
                if (keyiteminformation == null)
                {
                    for (int i = 0; i < tmpBuf.Length; ++i)
                    {
                        tmpBuf[i] = "";
                    }
                }
                else
                {
                    tmpBuf[0] = keyiteminformation.Name;
                    tmpBuf[1] = Regex.Escape(keyiteminformation.Description + string.Empty);
                    tmpBuf[2] = keyiteminformation.SectorNumber.ToString();
                    tmpBuf[3] = Regex.Escape(keyiteminformation.SectorDescription + string.Empty);
                }
                infoBuf.Append(string.Join("\t", tmpBuf));
                // 集計アイテム情報部
                Array.Resize<string>(ref tmpBuf, 14);
                tmpBuf[0] = questioninformation.Name;
                tmpBuf[1] = Regex.Escape(questioninformation.Description + string.Empty);
                tmpBuf[2] = ((int)questioninformation.QuestionType).ToString();
                tmpBuf[3] = ((int)significancetestcode).ToString();
                tmpBuf[4] = SetNo.ToString() + "_" + ParentQNo;
                tmpBuf[5] = questioninformation.HasCount.ToString();
                tmpBuf[6] = questioninformation.SummaryTableName;
                tmpBuf[7] = questioninformation.SubTotalCnt.ToString();
                tmpBuf[8] = questioninformation.HasWeight.ToString();
                tmpBuf[9] = Regex.Escape(questioninformation.NarrowingCondition + string.Empty);
                tmpBuf[10] = Regex.Escape(questioninformation.TableHeading + string.Empty);
                tmpBuf[11] = Regex.Escape(questioninformation.QNumber + string.Empty);
                tmpBuf[12] = questioninformation.WBValue;
                tmpBuf[13] = questioninformation.TabulateFullQuantity.ToString();
                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                // 集計軸情報部
                Array.Resize<string>(ref tmpBuf, axesgroups.Count + 1);
                tmpBuf[0] = axesgroups.Count.ToString();
                for (int i = 0; i < axesgroups.Count; ++i)
                {
                    string[] tmpBuf2 = new string[axesgroups[i].Count + 2];
                    tmpBuf2[0] = axesgroups[i].Count.ToString();
                    tmpBuf2[1] = axesgroups[i].ShowTotal.ToString();
                    for (int j = 0; j < axesgroups[i].Count; ++j)
                    {
                        tmpBuf2[j + 2] = axesgroups[i][j].SectorsCount.ToString();
                    }
                    tmpBuf[i + 1] = string.Join("\v", tmpBuf2);
                }
                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                // コメント情報部
                if (string.IsNullOrEmpty(this.Comment))
                {
                    infoBuf.Append("\f");
                }
                else
                {
                    infoBuf.Append("\f" + Regex.Escape(this.Comment));
                }
                // グラフ情報部
                Array.Resize<string>(ref tmpBuf, 6);
                if (chartinformation == null)
                {
                    tmpBuf[0] = "0";
                }
                else
                {
                    tmpBuf[0] = ((int)chartinformation.ChartType).ToString();
                }
                if (tmpBuf[0].Equals("0"))
                {
                    for (int i = 1; i < tmpBuf.Length; ++i)
                    {
                        tmpBuf[i] = "";
                    }
                }
                else
                {
                    string[] colorindexBuf = new string[chartinformation.SeriesCount];
                    for (int i = 0; i < colorindexBuf.Length; ++i)
                    {
                        colorindexBuf[i] = chartinformation.SeriesColorIndex(i).ToString();
                    }
                    tmpBuf[1] = string.Join(" ", colorindexBuf);
                    tmpBuf[2] = ((int)chartinformation.GradientStyle).ToString() + " " + chartinformation.GradientVariant.ToString();
                    tmpBuf[3] = chartinformation.InteriorColorIndex.ToString();
                    tmpBuf[4] = ((int)chartinformation.InteriorGradientStyle).ToString() + " " + chartinformation.InteriorGradientVariant.ToString();
                    tmpBuf[5] = hidechartdescriptionmaxpercent.ToString();
                }
                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) == 0)
                {
                    return infoBuf.ToString();
                }
                // 選択肢情報部
                Array.Resize<string>(ref tmpBuf, sectorinformations.Length + 1);
                tmpBuf[0] = sectorinformations.Length.ToString();
                for (int i = 0; i < sectorinformations.Length; ++i)
                {
                    tmpBuf[i + 1] = sectorinformations[i].Weight + "\v" + (sectorinformations[i].IsUnsort ? "1" : "0");
                }
                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                // 折れ線グラフ行情報部
                infoBuf.Append("\f");
                if (linechartrows != null) infoBuf.Append(string.Join(",", linechartrows));
                return infoBuf.ToString();
            }

            internal new string OutputToTSV()
            {
                string information = CreateInformationBuffer();
                if (information == null)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullDataMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "information");
                }
                if ((int)(questioninformation.QuestionType & (QuestionType.SA | QuestionType.MA)) != 0)
                {
                    if ((questioninformation.QuestionType & QuestionType.Sort) == QuestionType.Sort && enableSort)
                    {
                        List<int> sortSectors = new List<int>();
                        for (int i = 0; i < sectorinformations.Length; ++i)
                        {
                            if (!sectorinformations[i].IsUnsort) sortSectors.Add(i);
                        }
                        if (sortSectors.Count > 1)
                        {
                            return this.OutputToTSV(information, axesgroups, sortSectors.ToArray());
                        }
                    }
                }
                return this.OutputToTSV(CreateInformationBuffer(), axesgroups);
            }

            /// <summary>
            /// 項目間検定の種類を表すSignificanceTestCode列挙型の値を取得/設定するプロパティ
            /// <note>
            /// 設定の前にSetQuestionInformationメソッドを実行して、集計対象質問情報を設定しておく必要がある
            /// </note>
            /// </summary>
            public override SignificanceTestCode SignificancetestCode
            {
                get
                {
                    return significancetestcode;
                }
                set
                {
                    if (this.Question == null) return;
                    if (value == SignificanceTestCode.Off)
                    {
                        significancetestcode = value;
                        return;
                    }
                    if (value == SignificanceTestCode.BetweenSectors) significancetestcode = value;
                }
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
            /// 折れ線グラフとする行番号を追加設定するメソッド
            /// <note>行番号には全体行を0として自然数を指定 (％表イメージでの行番号を指定)</note>
            /// </summary>
            /// <param name="rowindex">折れ線グラフに指定する行番号</param>
            public void AddLineChartRow(int rowindex)
            {
                if (rowindex < 1) return;
                if (linechartrows == null)
                {
                    linechartrows = new int[1];
                }
                else if (!linechartrows.Contains<int>(rowindex))
                {
                    Array.Resize<int>(ref linechartrows, linechartrows.Length + 1);
                }
                else
                {
                    return;
                }
                linechartrows[linechartrows.GetUpperBound(0)] = rowindex;
            }

            /// <summary>
            /// セット番号を取得/設定するプロパティ (1シート1クロスのシナリオ出力時に有効)
            /// </summary>
            public int SetNo { get; set; }

            /// <summary>
            /// マトリクスの親質問番号を取得/設定するプロパティ (1シート1クロスのシナリオ出力時に有効)
            /// </summary>
            public string ParentQNo { get; set; }
            public bool enableSort = true;
        }

        /// <summary>
        /// 出力命令側から扱うFAリストの集計表を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("50141D6E-D274-47af-B737-470C0A63D542")]
        public class FAListTable : Table, IFAListTable
        {
            // 上限値はリソースから読み込み
            private static readonly int FAITEMSCOUNT_MAX = int.Parse(GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportFAListFAItemsCountLimitIndex));
            private static readonly int ADDEDITEMSCOUNT_MAX = int.Parse(GetResource.GetReportKeyword(Constants.ReportMessageIndex.ReportFAListAddedItemsCountLimitIndex));

            private KeyItemInformation keyiteminformation = null;
            private int faitemscount = 0;   // FAアイテム数
            private int addeditemscount = 0;  // 付加アイテム数
            private string addedItemsQTypeBuffer = null;    // 付加アイテムの質問タイプを表す数字の羅列
            private string topitemname = null;

            internal FAListTable(Tables tables, Tabulation.QuestionType questiontype, string[,] strArray) : base(tables, questiontype, strArray: strArray) { }

            private string CreateInformationBuffer()
            {
                // TSVの集計表設定情報ブロック文字列を生成
                if (faitemscount < 1 || topitemname == null) return null;
                StringBuilder infoBuf = new StringBuilder("");
                // 分類アイテム情報部
                string[] tmpBuf = new string[4];
                if (keyiteminformation == null)
                {
                    for (int i = 0; i < tmpBuf.Length; ++i)
                    {
                        tmpBuf[i] = "";
                    }
                }
                else
                {
                    tmpBuf[0] = keyiteminformation.Name;
                    tmpBuf[1] = Regex.Escape(keyiteminformation.Description + string.Empty);
                    tmpBuf[2] = keyiteminformation.SectorNumber.ToString();
                    tmpBuf[3] = Regex.Escape(keyiteminformation.SectorDescription + string.Empty);
                }
                infoBuf.Append(string.Join("\t", tmpBuf));
                // アイテム数情報部
                Array.Resize<string>(ref tmpBuf, 4);
                tmpBuf[0] = faitemscount.ToString();
                tmpBuf[1] = addeditemscount.ToString();
                tmpBuf[2] = addedItemsQTypeBuffer == null ? string.Empty : addedItemsQTypeBuffer;
                tmpBuf[3] = topitemname;
                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                // コメント情報部
                infoBuf.Append("\f" + Regex.Escape(string.IsNullOrEmpty(this.Comment) ? string.Empty : this.Comment));
                return infoBuf.ToString();
            }

            internal string OutputToTSV()
            {
                return this.OutputToTSV(CreateInformationBuffer());
            }

            /// <summary>
            /// 分類アイテム情報を設定するメソッド<br />
            /// 設定が有効なのは1度だけ
            /// </summary>
            public void SetKeyItemInformation(string name, string description, int sectornumber, string sectordescription)
            {
                if (keyiteminformation == null && !string.IsNullOrWhiteSpace(name))
                {
                    keyiteminformation = new KeyItemInformation(name, description, sectornumber, sectordescription);
                }
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
            /// FAアイテム数を取得/設定するプロパティ
            /// </summary>
            public int FAItemsCount
            {
                get
                {
                    return faitemscount;
                }
                set
                {
                    int max = int.Parse((ParentRequest as Request).AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_FALIST_FA_ITEM_MAX));

                    if (value >= 1 && value <= max)
                    {
                        faitemscount = value;
                    }
                }
            }

            /// <summary>
            /// 付加アイテム数を取得/設定するプロパティ
            /// </summary>
            public int AddedItemsCount
            {
                get
                {
                    return addeditemscount;
                }
                set
                {
                    int max = int.Parse((ParentRequest as Request).AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_FALIST_FA_ADD_ITEM_MAX));

                    if (value >= 1 && value <= max && addeditemscount != value)
                    {
                        addeditemscount = value;
                        char c = ((int)QuestionType.SA).ToString()[0];
                        addedItemsQTypeBuffer = new string(c, addeditemscount);
                    }
                }
            }

            /// <summary>
            /// 付加アイテムの質問タイプを表す数字の羅列を取得/設定するプロパティ
            /// </summary>
            public string AddedItemsQTypeBuffer
            {
                get
                {
                    return addedItemsQTypeBuffer;
                }
                set
                {
                    // if (string.IsNullOrEmpty(value)) value = string.Empty;
                    // value = value.Trim();
                    value = (value + string.Empty).Trim();
                    char c = ((int)QuestionType.SA).ToString()[0];
                    char[] cArray = value.ToCharArray();
                    int len = value.Length;
                    if (len != addeditemscount)
                    {
                        Array.Resize<char>(ref cArray, addeditemscount);
                    }
                    for (int i = 0; i < addeditemscount; ++i)
                    {
                        int tmp = 0;
                        if (i < len)
                        {
                            if (int.TryParse(value.Substring(i, 1), out tmp))
                            {
                                switch ((QuestionType)tmp)
                                {
                                    case QuestionType.SA:
                                    case QuestionType.MA:
                                    case QuestionType.FA:
                                    case QuestionType.N:
                                        break;
                                    default:
                                        tmp = 0;
                                        break;
                                }
                            }
                        }
                        if (tmp == 0) cArray[i] = c;
                    }
                    addedItemsQTypeBuffer = new string(cArray);
                }
            }

            /// <summary>
            /// 先頭のFAアイテム名を取得/設定するプロパティ<br />
            /// 設定が有効なのは1度だけ
            /// </summary>
            public string TopItemName
            {
                get
                {
                    return topitemname;
                }
                set
                {
                    if (topitemname == null)
                    {
                        topitemname = value;
                    }
                }
            }

            /// <summary>
            /// コメントを設定するメソッド<br />
            /// 設定が有効なのは1度だけ
            /// <note>設定するコメントのエスケープ処理は、出力時に内部で行うので、設定時には行わないこと</note>
            /// </summary>
            public void SetComment(string comment)
            {
                this.Comment = comment;
            }
        }

        /// <summary>
        /// 出力命令側から扱うチェックリストの集計表を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("84FA7AF6-2A71-4dd8-B541-7A2B3D82CD79")]
        public class CheckListTable : Table, ICheckListTable
        {
            private QuestionInformation questioninformation = null;
            private int sectorscount = 0;
            private bool ischanged = false;
            private bool isnewitem = false;

            internal CheckListTable(Tables tables, Tabulation.QuestionType questiontype, string[,] strArray) : base(tables, questiontype, strArray: strArray) { }

            private string CreateInformationBuffer()
            {
                // TSVの集計表設定情報ブロック文字列を生成
                if (questioninformation == null) return null;
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.N)) == 0) return null;
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0 && sectorscount == 0) return null;
                StringBuilder infoBuf = new StringBuilder("");
                // 集計アイテム情報部
                string[] tmpBuf = new string[4];
                tmpBuf[0] = questioninformation.Name;
                tmpBuf[1] = System.Text.RegularExpressions.Regex.Escape(questioninformation.Description);
                tmpBuf[2] = ((int)questioninformation.QuestionType).ToString();
                tmpBuf[3] = (questioninformation.QuestionType & Tabulation.QuestionType.N) == Tabulation.QuestionType.N ? "0" : sectorscount.ToString();
                infoBuf.Append(string.Join("\t", tmpBuf));
                // フラグ情報部
                Array.Resize<string>(ref tmpBuf, 2);
                tmpBuf[0] = ischanged ? "1" : "0";
                tmpBuf[1] = isnewitem ? "1" : "0";
                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                return infoBuf.ToString();
            }

            internal string OutputToTSV()
            {
                return this.OutputToTSV(CreateInformationBuffer());
            }

            /// <summary>
            /// 集計対象質問情報を設定するメソッド<br />
            /// 設定が有効なのは1度だけ
            /// </summary>
            /// <param name="name">アイテム名</param>
            /// <param name="description">質問文</param>
            /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
            public void SetQuestionInformation(string name, string description, Tabulation.QuestionType questiontype)
            {
                if (questioninformation == null)
                {
                    questioninformation = new QuestionInformation(name, description, questiontype);
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
            /// 集計アイテムの選択肢数を取得/設定するプロパティ
            /// </summary>
            public int SectorsCount
            {
                get
                {
                    return sectorscount;
                }
                set
                {
                    if (value > 0) sectorscount = value;
                }
            }

            /// <summary>
            /// 変更の有無を取得/設定するプロパティ
            /// </summary>
            public bool IsChanged
            {
                get
                {
                    return ischanged;
                }
                set
                {
                    ischanged = value;
                }
            }

            /// <summary>
            /// 新アイテムかどうかを取得/設定するプロパティ
            /// </summary>
            public bool IsNewItem
            {
                get
                {
                    return isnewitem;
                }
                set
                {
                    isnewitem = value;
                }
            }
        }

        /// <summary>
        /// 出力命令側から扱う調査票の質問情報を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("CCC41752-ABAF-40da-95E6-1EE6AA6CDC23")]
        public class QuestionnaireTable : Table, IQuestionnaireTable
        {
            private bool isSetted = false;
            private QuestionInformation2 questioninformation = null;
            private SectorInformation2[] sectorinformations = null;
            private QuestionInformation2[] childquestioninformation = null;

            internal QuestionnaireTable(Tables tables) : base(tables, (Tabulation.QuestionType)0) { }

            private string CreateInformationBuffer()
            {
                // TSVの集計表設定情報ブロック文字列を生成
                if (questioninformation == null) return null;
                if ((questioninformation.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent && (childquestioninformation == null || childquestioninformation.Length == 0)) return null;
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA | Tabulation.QuestionType.N | QuestionType.FA)) == 0) return null;
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0
                    && (sectorinformations == null || sectorinformations.Length == 0)) return null;
                StringBuilder infoBuf = new StringBuilder("");
                // 集計アイテム情報部
                string[] tmpBuf = new string[4];
                tmpBuf[0] = questioninformation.Name + (questioninformation.QuestionNo == null ? string.Empty : "\v" + questioninformation.QuestionNo);
                tmpBuf[1] = Regex.Escape(questioninformation.Description + string.Empty);
                tmpBuf[2] = ((int)questioninformation.QuestionType).ToString();
                tmpBuf[3] = Regex.Escape(questioninformation.RuleDescription + string.Empty);
                infoBuf.Append(string.Join("\t", tmpBuf));
                // 選択肢情報部
                if ((int)(questioninformation.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0)
                {
                    Array.Resize<string>(ref tmpBuf, sectorinformations.Length + 1);
                    tmpBuf[0] = sectorinformations.Length.ToString();
                    for (int i = 0; i < sectorinformations.Length; ++i)
                    {
                        tmpBuf[i + 1] = sectorinformations[i].Number.ToString() + "\v" + sectorinformations[i].AddedQuestionName
                                      + "\v" + Regex.Escape(sectorinformations[i].Description + string.Empty);
                    }
                }
                else
                {
                    Array.Resize<string>(ref tmpBuf, 1);
                    tmpBuf[0] = "0";
                }
                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                // 子質問情報部
                if ((questioninformation.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
                {
                    Array.Resize<string>(ref tmpBuf, childquestioninformation.Length + 1);
                    tmpBuf[0] = childquestioninformation.Length.ToString();
                    for (int i = 0; i < childquestioninformation.Length; ++i)
                    {
                        tmpBuf[i + 1] = childquestioninformation[i].Name
                                      + "\v" + ((int)childquestioninformation[i].QuestionType).ToString()
                                      + "\v" + Regex.Escape(childquestioninformation[i].Description + string.Empty);
                    }
                }
                else
                {
                    Array.Resize<string>(ref tmpBuf, 1);
                    tmpBuf[0] = "0";
                }
                infoBuf.Append("\f" + string.Join("\t", tmpBuf));
                return infoBuf.ToString();
            }

            internal string OutputToTSV()
            {
                return this.OutputToTSV(CreateInformationBuffer());
            }

            /// <summary>
            /// 集計対象質問の簡易情報を設定するメソッド<br />
            /// 設定が有効なのは1度だけ
            /// <note>
            /// このメソッドでは、セレクト条件の設定は行われない<br />
            /// このメソッドのRuleDescriptionプロパティを使って実行後設定すること
            /// </note>
            /// </summary>
            /// <param name="question">集計対象質問<note>トップレベルの質問のみ指定可能 (マトリクスの子質問や付加FA質問は不可)</note></param>
            public void SetQuestion(Question.Questions.Question question)
            {
                if (isSetted) return;
                if (question == null)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullParameterMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "question");
                }
                if ((int)(question.QuestionType & (Tabulation.QuestionType.MatrixChild | Tabulation.QuestionType.FA_Sub)) != 0)
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustQuestionTypeMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , question.QuestionType.ToString());
                }
                if ((int)(question.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0
                            && (question.Sectors == null || question.Sectors.Count == 0))
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptySectorInformationMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL);
                }
                if ((question.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent
                            && (question.ChildQuestions == null || question.ChildQuestions.Count == 0))
                {
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyChildQuestionInformationMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL);
                }
                isSetted = true;
                questioninformation = new QuestionInformation2(question.Name, question.Number, question.Description, question.QuestionType);
                if ((int)(question.QuestionType & (Tabulation.QuestionType.SA | Tabulation.QuestionType.MA)) != 0)
                {
                    sectorinformations = new SectorInformation2[question.Sectors.Count];
                    for (int i = 0; i < question.Sectors.Count; ++i)
                    {
                        QCWeb.Question.Sectors.Sector sector = (QCWeb.Question.Sectors.Sector)question.Sectors[i + 1];
                        string addedquestionname = null;
                        if (sector.AddedQuestion != null)
                        {
                            addedquestionname = sector.AddedQuestion.Name;
                        }
                        sectorinformations[i] = new SectorInformation2(sector.Number, addedquestionname, sector.Description);
                    }
                }
                if ((question.QuestionType & Tabulation.QuestionType.MatrixParent) == Tabulation.QuestionType.MatrixParent)
                {
                    childquestioninformation = new QuestionInformation2[question.ChildQuestions.Count];
                    for (int i = 0; i < question.ChildQuestions.Count; ++i)
                    {
                        QCWeb.Question.Questions.Question childQ = (QCWeb.Question.Questions.Question)question.ChildQuestions[i + 1];
                        childquestioninformation[i] = new QuestionInformation2(childQ.Name, null, childQ.Description, childQ.QuestionType);
                    }
                }
            }

            /*
            /// <summary>
            /// 集計対象質問情報を設定するメソッド<br />
            /// 設定が有効なのは1度だけ
            /// </summary>
            /// <param name="name">アイテム名</param>
            /// <param name="description">質問文</param>
            /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
            public void SetQuestionInformation(string name, string description, Tabulation.QuestionType questiontype)
            {
                if (questioninformation == null)
                {
                    questioninformation = new QuestionInformation(name, description, questiontype);
                }
            }

            /// <summary>
            /// 選択肢情報を追加するメソッド
            /// </summary>
            /// <param name="number">選択肢番号</param>
            /// <param name="addedquestionname">付加アイテム名</param>
            /// <param name="description">選択肢文</param>
            public void AddSectorInformation(int number, string addedquestionname, string description)
            {
                if (sectorinformations == null)
                {
                    sectorinformations = new SectorInformation2[1];
                }
                else
                {
                    Array.Resize<SectorInformation2>(ref sectorinformations, sectorinformations.Length + 1);
                }
                sectorinformations[sectorinformations.GetUpperBound(0)] = new SectorInformation2(number, addedquestionname, description);
            }

            /// <summary>
            /// 集計対象質問の子質問情報を追加するメソッド
            /// </summary>
            /// <param name="name">アイテム名</param>
            /// <param name="description">質問文</param>
            /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
            public void AddChildQuestionInformation(string name, string description, Tabulation.QuestionType questiontype)
            {

                if (childquestioninformation == null)
                {
                    childquestioninformation = new QuestionInformation[1];
                }
                else
                {
                    Array.Resize<QuestionInformation>(ref childquestioninformation, childquestioninformation.Length + 1);
                }
                childquestioninformation[childquestioninformation.GetUpperBound(0)] = new QuestionInformation(name, description, questiontype);
            }
            */

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
        /// 出力命令側から扱うローデータ情報を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("3C11EEEC-EF67-4070-A256-0F0EBB4CA435")]
        public class RawDataTable : Table, IRawDataTable
        {
            private const string RNAME = "RNAME";
            private const string RID = "RID";
            private const string RMETHOD = "RMETHOD";
            private const string RSERVICE = "RSERVICE";
            private const string RPERIOD = "RPERIOD";
            private const string ALLOCATION_CELL = "CELL\tNO\tTITLE\tREQNUMSAMPLE\tVALIDNUMSAMPLE";
            private const string CELL = "CELL";
            private const string SELECT_CONDITION = "RULE\tQNO\tSUBQNO\tEXPRESSION";
            private const string RULE = "RULE";

            private OutputDataType datatype = OutputDataType.Code;
            private LayoutOrientation orientation = (LayoutOrientation)0;
            private List<ResearchInformation> researchinformations = null;
            private List<CellInformation> cellinformations = null;
            private List<RuleInformation> ruleinformations = null;
            private string columnInfoBuf = null;
            private string[,] layoutArray = null;

            internal RawDataTable(Tables tables, string[,] strArray, bool[] divisiblePoint, Macromill.QCWeb.Question.QCAnswerType[] columnQCAnsType, string[,] layoutArray) : base(tables, strArray)
            {
                Array.Resize<bool>(ref divisiblePoint, strArray.GetLength(1));
                int[] columnInfo = Array.ConvertAll<bool, int>(divisiblePoint, x => GlobalMethodClass.CInt(x) & 1);
                if (columnQCAnsType != null)
                {
                    Array.Resize<Macromill.QCWeb.Question.QCAnswerType>(ref columnQCAnsType, columnInfo.Length);
                    for (int i = 0; i < columnInfo.Length; ++i)
                    {
                        columnInfo[i] |= (int)columnQCAnsType[i] << 1;
                    }
                }
                string[] columnInfoArray = Array.ConvertAll<int, string>(columnInfo, x => x.ToString("x1"));
                columnInfoBuf = string.Join(string.Empty, columnInfoArray);
                this.layoutArray = layoutArray;
            }

            private string CreateInformationBuffer()
            {
                // TSVの集計表設定情報ブロック文字列を生成
                switch (datatype)
                {
                    case OutputDataType.Code:
                    case OutputDataType.Flag:
                    case OutputDataType.Decode:
                        break;
                    case OutputDataType.QC3:
                        if (researchinformations == null || researchinformations.Count == 0)
                        {
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.NullOrEmptyDataMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , "researchinformations");
                        }
                        break;
                    default:
                        throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustDataMessageIndex)
                                               , GlobalsCommonConstant.LogLevel.FATAL
                                               , "datatype", datatype.ToString());
                }
                StringBuilder infoBuf = new StringBuilder("");
                // データタイプコード
                infoBuf.Append(((int)datatype).ToString());
                // 列情報文字列
                infoBuf.Append("\f" + columnInfoBuf);
                // レイアウト表イメージ文字列
                infoBuf.Append("\f");
                if (layoutArray != null)
                {
                    for (int i = 0; i < layoutArray.GetLength(0); ++i)
                    {
                        infoBuf.Append(i == 0 ? string.Empty : "\v");
                        for (int j = 0; j < layoutArray.GetLength(1); ++j)
                        {
                            infoBuf.Append(j == 0 ? string.Empty : "\t");
                            infoBuf.Append(Regex.Escape(layoutArray[i, j] + string.Empty));
                        }
                    }
                }
                if (datatype == OutputDataType.QC3)
                {
                    StringBuilder tmpBuf = new StringBuilder("");
                    tmpBuf.Append(RNAME);
                    for (int i = 0; i < researchinformations.Count; ++i)
                    {
                        tmpBuf.Append("\t" + researchinformations[i].Name);
                    }
                    tmpBuf.Append("\r\n" + RID);
                    for (int i = 0; i < researchinformations.Count; ++i)
                    {
                        tmpBuf.Append("\t" + researchinformations[i].ID.ToString());
                    }
                    tmpBuf.Append("\r\n" + RMETHOD);
                    for (int i = 0; i < researchinformations.Count; ++i)
                    {
                        tmpBuf.Append("\t" + researchinformations[i].Method);
                    }
                    tmpBuf.Append("\r\n" + RSERVICE);
                    for (int i = 0; i < researchinformations.Count; ++i)
                    {
                        tmpBuf.Append("\t" + researchinformations[i].Service);
                    }
                    tmpBuf.Append("\r\n" + RPERIOD);
                    for (int i = 0; i < researchinformations.Count; ++i)
                    {
                        tmpBuf.Append("\t" + researchinformations[i].Period);
                    }
                    infoBuf.Append("\f" + Regex.Escape(tmpBuf.ToString()));
                    tmpBuf.Clear();
                    tmpBuf.Append(ALLOCATION_CELL);
                    for (int i = 0; i < cellinformations.Count; ++i)
                    {
                        tmpBuf.Append("\r\n" + CELL);
                        tmpBuf.Append("\t" + cellinformations[i].Number);
                        tmpBuf.Append("\t" + cellinformations[i].Description);
                        tmpBuf.Append("\t" + cellinformations[i].RequestDataCount.ToString());
                        tmpBuf.Append("\t" + cellinformations[i].ValidDataCount.ToString());
                    }
                    infoBuf.Append("\f" + Regex.Escape(tmpBuf.ToString()));
                    tmpBuf.Clear();
                    tmpBuf.Append(SELECT_CONDITION);
                    for (int i = 0; i < ruleinformations.Count; ++i)
                    {
                        tmpBuf.Append("\r\n" + RULE);
                        tmpBuf.Append("\t" + ruleinformations[i].QuestionNo);
                        tmpBuf.Append("\t" + ruleinformations[i].ChildQuestionNo);
                        tmpBuf.Append("\t" + ruleinformations[i].Expression);
                    }
                    infoBuf.Append("\f" + Regex.Escape(tmpBuf.ToString()));
                }
                else
                {
                    infoBuf.Append("\f" + ((int)orientation).ToString());
                }
                return infoBuf.ToString();
            }

            internal string OutputToTSV()
            {
                return this.OutputToTSV(CreateInformationBuffer());
            }

            /// <summary>
            /// データ出力時のデータ形式を表すOutputDataType列挙型の値を取得/設定するプロパティ<br />
            /// 設定できる値はデータ形式を表す、以下に示すOutputDataType列挙型の値
            /// <list type="bullet">
            /// <item>
            /// <description>OutputDataType.Code</description>
            /// </item>
            /// <item>
            /// <description>OutputDataType.Flag</description>
            /// </item>
            /// <item>
            /// <description>OutputDataType.Decode</description>
            /// </item>
            /// <item>
            /// <description>OutputDataType.QC3</description>
            /// </item>
            /// </list>
            /// </summary>
            public OutputDataType DataType
            {
                get
                {
                    return datatype;
                }
                set
                {
                    switch (value)
                    {
                        case OutputDataType.Code:
                        case OutputDataType.Flag:
                        case OutputDataType.Decode:
                        case OutputDataType.QC3:
                            if (datatype == value) return;
                            datatype = value;
                            if (value == OutputDataType.QC3)
                            {
                                researchinformations = new List<ResearchInformation>();
                                cellinformations = new List<CellInformation>();
                                ruleinformations = new List<RuleInformation>();
                            }
                            else
                            {
                                researchinformations = null;
                                cellinformations = null;
                                ruleinformations = null;
                            }
                            break;
                    }
                }
            }

            /// <summary>
            /// レイアウト表の向きを表すLayoutOrientation列挙型の値を取得/設定するプロパティ<br />
            /// 設定できる値はデータ形式を表す、以下に示すLayoutOrientation列挙型の値
            /// <item>
            /// <description>LayoutOrientation.None</description>
            /// </item>
            /// <item>
            /// <description>LayoutOrientation.Landscape</description>
            /// </item>
            /// <item>
            /// <description>LayoutOrientation.Portrait</description>
            /// </item>
            /// </summary>
            public LayoutOrientation LayoutOrientation
            {
                get
                {
                    return orientation;
                }
                set
                {
                    switch (value)
                    {
                        case (LayoutOrientation)0:
                        case LayoutOrientation.Landscape:
                        case LayoutOrientation.Portrait:
                            orientation = value;
                            break;
                    }
                }
            }

            /// <summary>
            /// 調査概要の調査情報を保持したReserchInformation構造体の値をまとめたListクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public System.Collections.Generic.List<ResearchInformation> ResearchInformations
            {
                get
                {
                    return researchinformations;
                }
            }

            /// <summary>
            /// 調査概要の割付セル情報を保持したCellInformation構造体の値をまとめたListクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public System.Collections.Generic.List<CellInformation> CellInformations
            {
                get
                {
                    return cellinformations;
                }
            }

            /// <summary>
            /// 調査概要のセレクト条件情報を保持したRuleInformation構造体の値をまとめたListクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public System.Collections.Generic.List<RuleInformation> RuleInformations
            {
                get
                {
                    return ruleinformations;
                }
            }

            /// <summary>
            /// 調査概要の調査情報を追加するメソッド
            /// </summary>
            /// <param name="id">調査ID</param>
            /// <param name="name">調査名</param>
            /// <param name="method">調査方法</param>
            /// <param name="service">商品種別</param>
            /// <param name="period">実施期間</param>
            /// <returns>追加したResearchInformation構造体の値</returns>
            public ResearchInformation? AddResearchInformation(decimal id, string name, string method, string service, string period)
            {
                if (researchinformations == null) return null;
                ResearchInformation newinfo = new ResearchInformation(id);
                newinfo.Name = name;
                newinfo.Method = method;
                newinfo.Service = service;
                newinfo.Period = period;
                researchinformations.Add(newinfo);
                return newinfo;
            }

            /// <summary>
            /// 調査概要の割付セル情報を追加するメソッド
            /// </summary>
            /// <param name="number">セルNo.</param>
            /// <param name="description">セル名称</param>
            /// <param name="reqdatacount">希望サンプル数</param>
            /// <param name="validdatacount">有効サンプル数</param>
            /// <returns>追加したCellInformation構造体の値</returns>
//            public CellInformation? AddCellInformation(string number, string description, int? reqdatacount, int? validdatacount)
            public CellInformation? AddCellInformation(string number, string description, string reqdatacount, string validdatacount)
            {
                if (cellinformations == null) return null;
                CellInformation newinfo = new CellInformation(number);
                newinfo.Description = description;
                newinfo.RequestDataCount = reqdatacount;
                newinfo.ValidDataCount = validdatacount;
                cellinformations.Add(newinfo);
                return newinfo;
            }

            /// <summary>
            /// 調査概要のセレクト条件情報を追加するメソッド
            /// </summary>
            /// <param name="questionno">質問番号</param>
            /// <param name="childquestionno">子質問番号</param>
            /// <param name="expression">セレクト条件</param>
            /// <returns>追加したRuleInformation構造体の値</returns>
            public RuleInformation? AddRuleInformation(string questionno, string childquestionno, string expression)
            {
                if (ruleinformations == null) return null;
                RuleInformation newinfo = new RuleInformation(questionno);
                newinfo.ChildQuestionNo = childquestionno;
                newinfo.Expression = expression;
                ruleinformations.Add(newinfo);
                return newinfo;
            }

            /// <summary>
            /// 調査概要の調査情報を保持したResearchInformationsArray構造体の値の配列を返す読み取り専用プロパティ
            /// </summary>
            public ResearchInformation[] ResearchInformationsArray
            {
                get
                {
                    if (researchinformations == null) return null;
                    return researchinformations.ToArray();
                }
            }

            /// <summary>
            /// 調査概要の割付セル情報を保持したCellInformation構造体の値の配列を返す読み取り専用プロパティ
            /// </summary>
            public CellInformation[] CellInformationsArray
            {
                get
                {
                    if (cellinformations == null) return null;
                    return cellinformations.ToArray();
                }
            }

            /// <summary>
            /// 調査概要のセレクト条件情報を保持したRuleInformation構造体の値の配列を返す読み取り専用プロパティ
            /// </summary>
            public RuleInformation[] RuleInformationsArray
            {
                get
                {
                    if (ruleinformations == null) return null;
                    return ruleinformations.ToArray();
                }
            }
        }

        private Outputs.Output output = null;

        internal Tables(Outputs.Output output)
        {
            this.output = output;
        }

        /// <summary>
        /// Tableクラスのインスタンスを生成して、それへの参照を返す
        /// </summary>
        /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="wholeArray">集計結果全体のDataWithMarking型二次元配列</param>
        /// <param name="outputtype">
        /// 出力物の種類を表す、以下に示すOutputType列挙型の値
        /// <list type="bullet">
        /// <item>
        /// <description>OutputType.GT</description>
        /// </item>
        /// <item>
        /// <description>OutputType.Cross</description>
        /// </item>
        /// </list>
        /// </param>
        /// <returns>生成したインスタンスへの参照</returns>
        public Table Add(Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[,] wholeArray, OutputType outputtype)
        {
            string key = this.Count.ToString();
            Table newTable = null;
            switch (outputtype)
            {
                case OutputType.GT:
                    newTable = new GTTable(this, questiontype, wholeArray);
                    break;
                case OutputType.Cross:
                    if ((ParentOutput as Outputs.OutputCross).TablesOnOnesheet == TablesOnOneSheet.Multi)
                    {
                        newTable = new CrossTable(this, questiontype, wholeArray);
                    }
                    else
                    {
                        Tabulation.DataWithMarking[][,] wholeJagArray = new Tabulation.DataWithMarking[1][,];
                        wholeJagArray[0] = wholeArray;
                        newTable = new CrossTable(this, questiontype, wholeJagArray);
                    }
                    break;
                default:
                    if (wholeArray == null)
                    {
                        if (outputtype == OutputType.Questionnaire)
                        {
                            newTable = new QuestionnaireTable(this);
                            break;
                        }
                        else
                        {
                            throw new QCWebException(new Message(Constants.CommonMessageIndex.ReferNullParameterMessageIndex)
                                                   , GlobalsCommonConstant.LogLevel.FATAL
                                                   , "wholeArray");
                        }
                    }
                    string[,] strArray = new string[wholeArray.GetLength(0), wholeArray.GetLength(1)];
                    for (int i = 0; i < wholeArray.GetLength(0); ++i)
                    {
                        for (int j = 0; j < wholeArray.GetLength(1); ++j)
                        {
                            strArray[i, j] = wholeArray[i, j].Value;
                        }
                    }
                    return Add(questiontype, strArray, outputtype);
            }
            this.Add(key, newTable);
            return newTable;
        }

        /// <summary>
        /// Tableクラスのインスタンスを生成して、それへの参照を返す
        /// ※クロス1シート1クロス専用
        /// </summary>
        /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="wholeJagArray">集計結果全体のDataWithMarking型三次元配列</param>
        /// <returns>生成したインスタンスへの参照</returns>
        public Table Add(Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[][,] wholeJagArray)
        {
            string key = this.Count.ToString();
            Table newTable = new CrossTable(this, questiontype, wholeJagArray);
            this.Add(key, newTable);
            return newTable;
        }

        /// <summary>
        /// Tableクラスのインスタンスを生成して、それへの参照を返す
        /// ※クロス1シート1クロス専用
        /// </summary>
        /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="wholeJagArray">集計結果全体のDataWithMarking型三次元配列</param>
        /// <param name="wholeJagArrayTotal">Summary total table列</param>
        /// <returns>生成したインスタンスへの参照</returns>
        public Table Add(Tabulation.QuestionType questiontype, Tabulation.DataWithMarking[][,] wholeJagArray, Tabulation.DataWithMarking[][,] wholeJagArrayTotal, Tabulation.DataWithMarking[][,] wholeJagArrayUnweightedTotal=null)
        {
            string key = this.Count.ToString();
            Table newTable = new CrossTable(this, questiontype, wholeJagArray, wholeJagArrayTotal, wholeJagArrayUnweightedTotal);
            this.Add(key, newTable);
            return newTable;
        }
        /// <summary>
        /// Tableクラスのインスタンスを生成して、それへの参照を返す
        /// </summary>
        /// <param name="questiontype">質問タイプを表すQuestionType列挙型の値</param>
        /// <param name="strArray">集計結果全体の文字列型二次元配列</param>
        /// <param name="outputtype">
        /// 出力物の種類を表す、以下に示すOutputType列挙型の値
        /// <list type="bullet">
        /// <item>
        /// <description>OutputType.FAList</description>
        /// </item>
        /// <item>
        /// <description>OutputType.CheckList</description>
        /// </item>
        /// <item>
        /// <description>OutputType.Questionnaire</description>
        /// </item>
        /// </list>
        /// </param>
        /// <returns>生成したインスタンスへの参照</returns>
        public Table Add(Tabulation.QuestionType questiontype, string[,] strArray, OutputType outputtype)
        {
            string key = this.Count.ToString();
            Table newTable = null;
            switch (outputtype)
            {
                case OutputType.FAList:
                    newTable = new FAListTable(this, questiontype, strArray);
                    break;
                case OutputType.CheckList:
                    newTable = new CheckListTable(this, questiontype, strArray);
                    break;
                case OutputType.Questionnaire:
                    newTable = new QuestionnaireTable(this);
                    break;
                default:
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "outputtype", outputtype.ToString());
            }
            this.Add(key, newTable);
            return newTable;
        }

        /// <summary>
        /// RawDataTableクラスのインスタンスを生成して、それへの参照を返す
        /// </summary>
        /// <param name="strArray">ローデータ表の文字列型二次元配列</param>
        /// <param name="divisiblePoint"></param>
        /// <param name="columnQCAnsType"></param>
        /// <param name="layoutArray"></param>
        /// <param name="outputtype">
        /// 出力物の種類を表す、以下に示すOutputType列挙型の値
        /// <list type="bullet">
        /// <item>
        /// <description>OutputType.RawData</description>
        /// </item>
        /// <item>
        /// <description>OutputType.QC3</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="datatype">データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="orientation">レイアウト表の向きを表すLayoutOrientation列挙型の値</param>
        /// <returns>生成したインスタンスへの参照</returns>
        public RawDataTable Add(string[,] strArray, bool[] divisiblePoint, Macromill.QCWeb.Question.QCAnswerType[] columnQCAnsType, string[,] layoutArray, OutputType outputtype, OutputDataType datatype, LayoutOrientation orientation)
        {
            string key = this.Count.ToString();
            RawDataTable newTable = null;
            switch (outputtype)
            {
                case OutputType.RawData:
                case OutputType.QC3:
                    if (outputtype == OutputType.QC3) datatype = OutputDataType.QC3;
                    newTable = new RawDataTable(this, strArray, divisiblePoint, columnQCAnsType, layoutArray)
                    {
                        DataType = datatype,
                        LayoutOrientation = orientation
                    };
                    break;
                default:
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustParameterMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , "outputtype", outputtype.ToString());
            }
            this.Add(key, newTable);
            return newTable;
        }

        /// <summary>
        /// RawDataTableクラスのインスタンスを生成して、それへの参照を返す
        /// </summary>
        /// <param name="strArray">ローデータ表の文字列型二次元配列</param>
        /// <param name="divisiblePoint"></param>
        /// <param name="layoutArray"></param>
        /// <param name="outputtype">
        /// 出力物の種類を表す、以下に示すOutputType列挙型の値
        /// <list type="bullet">
        /// <item>
        /// <description>OutputType.RawData</description>
        /// </item>
        /// <item>
        /// <description>OutputType.QC3</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="datatype">データ形式を表すOutputDataType列挙型の値</param>
        /// <param name="orientation">レイアウト表の向きを表すLayoutOrientation列挙型の値</param>
        /// <returns>生成したインスタンスへの参照</returns>
        public RawDataTable Add(string[,] strArray, bool[] divisiblePoint, string[,] layoutArray, OutputType outputtype, OutputDataType datatype, LayoutOrientation orientation)
        {
            return Add(strArray, divisiblePoint, null, layoutArray, outputtype, datatype, orientation);
        }

        public string OutputToTSV()
        {
            string[] tsvpaths = new string[this.Count];
            for (int i = 0; i < this.Count; ++i)
            {
                switch (output.OutputType)
                {
                    case OutputType.GT:
                        tsvpaths[i] = ((GTTable)this[i]).OutputToTSV();
                        break;
                    case OutputType.Cross:
                        tsvpaths[i] = ((CrossTable)this[i]).OutputToTSV();
                        break;
                    case OutputType.FAList:
                        tsvpaths[i] = ((FAListTable)this[i]).OutputToTSV();
                        break;
                    case OutputType.CheckList:
                        tsvpaths[i] = ((CheckListTable)this[i]).OutputToTSV();
                        break;
                    case OutputType.Questionnaire:
                        tsvpaths[i] = ((QuestionnaireTable)this[i]).OutputToTSV();
                        break;
                    case OutputType.RawData:
                    case OutputType.QC3:
                        tsvpaths[i] = ((RawDataTable)this[i]).OutputToTSV();
                        break;
                }
            }
            return string.Join(";", tsvpaths);
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
        /// <param name="key">
        /// キーとなる文字列<br />
        /// この値は要素の順序を表す0ベースのインデックス番号を文字列化したもの
        /// </param>
        /// <returns>キーが示すコレクションの要素であるTableクラスのインスタンスへの参照</returns>
        public ITable this[string key]
        {
            get
            {
                return base[key] as ITable;
            }
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

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (DictionaryEntry de in this)
            {
                Table tbl = de.Value as Table;
                tbl.Dispose();
            }
            output = null;
        }
    }
}
