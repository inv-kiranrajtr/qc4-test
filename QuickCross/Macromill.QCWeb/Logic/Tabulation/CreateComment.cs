using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Question;
using System.Diagnostics;
using System.IO;
using Macromill.QCWeb.ReportRequest;

namespace Macromill.QCWeb.Tabulation
{
    /// <summary>
    /// コメントを作成するメソッドを実装した静的クラス
    /// </summary>
    [ComVisible(false), Guid("0366C127-D904-4960-8AE3-5127C8047F5F")]
    public static class CreateComment
    {
        /// <summary>
        /// 全体との差の色付けのマーキング設定情報を保持する列挙型
        /// </summary>
        [Flags, ComVisible(true)]
        public enum ColoringSetting
        {
            /// <summary>
            /// 水準1高での色付けを表す (= 1)
            /// </summary>
            Level1High = 1,
            /// <summary>
            /// 水準2高での色付けを表す (= 2)
            /// </summary>
            Level2High = 2,
            /// <summary>
            /// 水準1低での色付けを表す (= 4)
            /// </summary>
            Level1Low = 4,
            /// <summary>
            /// 水準2低での色付けを表す (= 8)
            /// </summary>
            Level2Low = 8,
            /// <summary>
            /// ColoringSettin列挙型で使用するビットがすべて立った値 (= 15)
            /// </summary>
            AllBit = 15
        }

        private const string COMMENT_CAPTION_FORMAT_KEY = "CommentCaption";
        private const string RANKING_COMMENT_TOP_UNIT_FORMAT_KEY = "RankingCommentTopUnit";
        private const string RANKING_COMMENT_EXCEPT_TOP_UNIT_FORMAT_KEY = "RankingCommentExceptTopUnit";
        private const string RANKING_COMMENT_1_CATEGORY_FORMAT_KEY = "RankingComment1Category";
        private const string RANKING_COMMENT_2_CATEGORIES_FORMAT_KEY = "RankingComment2Categories";
        private const string RANKING_COMMENT_OVER_3_CATEGORIES_FORMAT_KEY = "RankingCommentOver3Categories";
        private const string RANKING_COMMENT_UNIT_DELIMITER_KEY = "RankingCommentUnitDelimiter";
        private const string COLORING_COMMENT_UNIT_FORMAT_KEY = "ColoringCommentUnit";
        private const string COLORING_COMMENT_LEVEL1_HIGH_KEY = "ColoringCommentLevel1High";
        private const string COLORING_COMMENT_LEVEL2_HIGH_KEY = "ColoringCommentLevel2High";
        private const string COLORING_COMMENT_LEVEL1_LOW_KEY = "ColoringCommentLevel1Low";
        private const string COLORING_COMMENT_LEVEL2_LOW_KEY = "ColoringCommentLevel2Low";
        private const string COLORING_COMMENT_LEVEL_DELIMITER_KEY = "ColoringCommentLevelDelimiter";
        private const string COLORING_COMMENT_UNIT_DELIMITER_KEY = "ColoringCommnetUnitDelimiter";
        private const string COLORING_COMMENT_FORMAT_KEY = "ColoringComment";

        private static string getWholeComment(string resxPath
                  , DataWithMarking[,] data, bool isMatrix
                  , int itemSectorsCount, out List<int> choicedSectorIndexes
                  , List<int> axesSectorsCount, bool plusSubTotal)
        {
            bool isCross = axesSectorsCount == null || axesSectorsCount.Count == 0;
            StringBuilder buf = new StringBuilder("");
            int orgChoiceSectorsCount = 5 - (GlobalMethodClass.CInt(itemSectorsCount <= 12) & (15 - itemSectorsCount) / 3);
            choicedSectorIndexes = new List<int>();
            string captionFormat = GetResource.GetResourceData(resxPath, COMMENT_CAPTION_FORMAT_KEY);
            TableOrientation orientation = isCross || isMatrix ? TableOrientation.Landscape : TableOrientation.Portrait;
            // 横％表用
            const int ROW_CAPTION_COLUMN_INDEX = 1;
            int wholeColumnIndex = 0;
            int dataStartColumnIndex = 0;
            int captionRowIndex = 0;
            int startRowIndex = 0;
            int lastRowIndex = 0;
            // 縦％表用
            const int WHOLE_ROW_INDEX = 2;
            const int DATA_START_ROW_INDEX = WHOLE_ROW_INDEX + 1;
            const int CAPTION_COLUMN_INDEX = 1;
            const int DATA_COLUMN_INDEX = CAPTION_COLUMN_INDEX + 2;
            if (orientation == TableOrientation.Landscape)
            {
                wholeColumnIndex = axesSectorsCount.Count + 2;
                dataStartColumnIndex = wholeColumnIndex + 1;
                if (isMatrix) captionRowIndex = 1;
                startRowIndex = captionRowIndex + 2;
                lastRowIndex = isMatrix ? data.GetUpperBound(0) : startRowIndex + (GlobalMethodClass.CInt(plusSubTotal) & 1);
            }
            int s = GlobalMethodClass.CInt(orientation == TableOrientation.Landscape) & startRowIndex;
            int e = GlobalMethodClass.CInt(orientation == TableOrientation.Landscape) & lastRowIndex;
            for (int x = s; x <= e; ++x)
            {
                if (x > s) buf.Append("\n\n");
                string captionBuf = null;
                if (orientation == TableOrientation.Landscape)
                {
                    captionBuf = string.Format(captionFormat, data[x, ROW_CAPTION_COLUMN_INDEX].Value);
                }
                else
                {
                    captionBuf = string.Format(captionFormat, data[WHOLE_ROW_INDEX, CAPTION_COLUMN_INDEX].Value);
                }
                buf.Append(captionBuf);
                List<int>[] rankedIndex = new List<int>[5];
                for (int i = 0; i < rankedIndex.Length; ++i)
                {
                    rankedIndex[i] = new List<int>();
                }
                for (int i = 0; i < itemSectorsCount; ++i)
                {
                    int rank = orientation == TableOrientation.Landscape
                                ? data[x, dataStartColumnIndex + i].Rank
                                : data[DATA_START_ROW_INDEX + i, DATA_COLUMN_INDEX].Rank;
                    if (rank > 0) rankedIndex[rank - 1].Add(i);
                }
                int choiceCount = 0;
                int lastRank = rankedIndex.Length;
                for (int i = 0; i < rankedIndex.Length; ++i)
                {
                    if ((choiceCount += rankedIndex[i].Count) >= orgChoiceSectorsCount)
                    {
                        lastRank = i + i;
                        break;
                    }
                }
                if (choiceCount == 0) continue;
                if (choicedSectorIndexes.Count == 0)
                {
                    for (int i = 0; i < lastRank; ++i)
                    {
                        for (int j = 0; j < rankedIndex[i].Count; ++j)
                        {
                            choicedSectorIndexes.Add(rankedIndex[i][j]);
                        }
                    }
                }
                string formatKey = null;
                switch (lastRank)
                {
                    case 1:
                        formatKey = RANKING_COMMENT_1_CATEGORY_FORMAT_KEY;
                        break;
                    case 2:
                        // formatKey = rankedIndex[1].Count == 1 ? RANKING_COMMENT_2_CATEGORIES_KEY : RANKING_COMMENT_OVER_3_CATEGORIES_KEY;
                        formatKey = RANKING_COMMENT_2_CATEGORIES_FORMAT_KEY;
                        break;
                    default:
                        formatKey = RANKING_COMMENT_OVER_3_CATEGORIES_FORMAT_KEY;
                        break;
                }
                buf.Append("\n");
                string commentFormat = GetResource.GetResourceData(resxPath, formatKey);
                string unitDelimiter = GetResource.GetResourceData(resxPath, RANKING_COMMENT_UNIT_DELIMITER_KEY);
                string topCommentFormat = GetResource.GetResourceData(resxPath, RANKING_COMMENT_TOP_UNIT_FORMAT_KEY);
                string[] topUnits = new string[rankedIndex[0].Count];
                for (int i = 0; i < rankedIndex[0].Count; ++i)
                {
                    if (orientation == TableOrientation.Landscape)
                    {
                        int c = dataStartColumnIndex + rankedIndex[0][i];
                        topUnits[i] = string.Format(topCommentFormat
                                , data[captionRowIndex, c].Value
                                , Function.RoundOff(data[x, c].Percent).ToString());
                    }
                    else
                    {
                        int r = DATA_START_ROW_INDEX + rankedIndex[0][i];
                        topUnits[i] = string.Format(topCommentFormat
                                , data[r, CAPTION_COLUMN_INDEX].Value
                                , Function.RoundOff(data[r, DATA_COLUMN_INDEX].Percent).ToString());
                    }
                }
                string topParam = string.Join(unitDelimiter, topUnits);
                if (lastRank == 1)
                {
                    buf.Append(string.Format(commentFormat, topParam));
                }
                else
                {
                    string exceptTopCommentFormat = GetResource.GetResourceData(resxPath, RANKING_COMMENT_EXCEPT_TOP_UNIT_FORMAT_KEY);
                    int exceptTopUnitsCount = choiceCount - rankedIndex[0].Count;
                    string[] exceptTopUnits = new string[exceptTopUnitsCount];
                    int n = -1;
                    for (int i = 1; i < lastRank; ++i)
                    {
                        for (int j = 0; j < rankedIndex[i].Count; ++j)
                        {
                            if (orientation == TableOrientation.Landscape)
                            {
                                int c = dataStartColumnIndex + rankedIndex[i][j];
                                exceptTopUnits[++n] = string.Format(exceptTopCommentFormat
                                        , data[captionRowIndex, c].Value
                                        , Function.RoundOff(data[n, c].Percent).ToString());
                            }
                            else
	                        {
                                int r = DATA_START_ROW_INDEX + rankedIndex[i][j];
                                exceptTopUnits[++n] = string.Format(exceptTopCommentFormat
                                        , data[r, CAPTION_COLUMN_INDEX].Value
                                        , Function.RoundOff(data[r, DATA_COLUMN_INDEX].Percent).ToString());
	                        }
                        }
                    }
                    string exceptTopParam = string.Join(unitDelimiter, exceptTopUnits);
                    buf.Append(string.Format(commentFormat, topParam, exceptTopParam));
                }
            }
            return buf.ToString();
        }

        private const int CAPTION_ROW_INDEX = 0;

        private static string getColoringComment(
                    DataWithMarking[,] data, List<int> itemSectorIndexes
                  , ColoringSetting coloringSetting, int minSamplesCount
                  , int rowCaptionLastColumnIndex, string[] rowCaptionBuf
                  , int preWBColumnIndex, int dataStartColumnIndex
                  , int startRowIndex, int lastRowIndex
                  , string rowCaptionDelimiter, string coloringFormat, string unitFormat
                  , string level1HighComment, string level2HighComment
                  , string level1LowComment, string level2LowComment
                  , string levelDelimiter, string unitDelimiter)
        {
            StringBuilder buffer = new StringBuilder(string.Empty);
            for (int r = startRowIndex; r <= lastRowIndex; ++r)
            {
                if (data[r, preWBColumnIndex].NumValue < (double)minSamplesCount) continue;
                rowCaptionBuf[rowCaptionBuf.GetUpperBound(0)] = data[r, rowCaptionLastColumnIndex].Value;
                string rowTitle = string.Join(rowCaptionDelimiter, rowCaptionBuf);
                List<int> level1HighIndexes = new List<int>();
                List<int> level2HighIndexes = new List<int>();
                List<int> level1LowIndexes = new List<int>();
                List<int> level2LowIndexes = new List<int>();
                for (int i = 0; i < itemSectorIndexes.Count; ++i)
                {
                    int c = dataStartColumnIndex + itemSectorIndexes[i];
                    if ((coloringSetting & ColoringSetting.Level2High) == ColoringSetting.Level2High)
                    {
                        if (data[r, c].ColoringLevel2High)
                        {
                            level2HighIndexes.Add(c);
                            continue;
                        }
                    }
                    if ((coloringSetting & ColoringSetting.Level1High) == ColoringSetting.Level1High)
                    {
                        if (data[r, c].ColoringLevel1High)
                        {
                            level1HighIndexes.Add(c);
                            continue;
                        }
                    }
                    if ((coloringSetting & ColoringSetting.Level2Low) == ColoringSetting.Level2Low)
                    {
                        if (data[r, c].ColoringLevel2Low)
                        {
                            level2LowIndexes.Add(c);
                            continue;
                        }
                    }
                    if ((coloringSetting & ColoringSetting.Level1Low) == ColoringSetting.Level1Low)
                    {
                        if (data[r, c].ColoringLevel1Low)
                        {
                            level1LowIndexes.Add(c);
                            continue;
                        }
                    }
                }
                if (level1HighIndexes.Count + level2HighIndexes.Count + level1LowIndexes.Count + level2LowIndexes.Count == 0) continue;
                StringBuilder buf = new StringBuilder("");
                List<List<int>> orderedIndexes = new List<List<int>>();
                orderedIndexes.Add(level2HighIndexes);
                orderedIndexes.Add(level1HighIndexes);
                orderedIndexes.Add(level1LowIndexes);
                orderedIndexes.Add(level2LowIndexes);
                List<string> orderedComment = new List<string>();
                orderedComment.Add(level2HighComment);
                orderedComment.Add(level1HighComment);
                orderedComment.Add(level1LowComment);
                orderedComment.Add(level2LowComment);
                for (int i = 0; i < orderedIndexes.Count; ++i)
                {
                    List<int> indexes = orderedIndexes[i];
                    string comment = orderedComment[i];
                    if (indexes.Count > 0)
                    {
                        string[] unitBuf = new string[indexes.Count];
                        for (int j = 0; j < indexes.Count; ++j)
                        {
                            int c = indexes[j];
                            unitBuf[j] = string.Format(unitFormat
                                                     , data[CAPTION_ROW_INDEX, c].Value
                                                     , Function.RoundOff(data[r, c].Percent).ToString());
                        }
                        if (buf.Length > 0) buf.Append(levelDelimiter);
                        buf.Append(string.Join(unitDelimiter, unitBuf));
                        buf.Append(comment);
                    }
                }
                buffer.Append(string.Format(coloringFormat, rowTitle, buf.ToString()));
            }
            return buffer.ToString();
        }

        private static string getColoringComment(string resxPath
                  , DataWithMarking[,] data, List<int> itemSectorIndexes
                  , List<int> axesSectorsCount, ColoringSetting coloringSetting
                  , int minSamplesCount)
        {
            const char MULTIPLY_LETTER = 'x';   // ×の代替文字
            const int CAPTION_COLUMN_INDEX = 0;
            const int ROW_CAPTION_START_COLUMN_INDEX = CAPTION_COLUMN_INDEX + 1;
            string rowCaptionDelimiter = " " + MULTIPLY_LETTER.ToString() + " ";
            int rowCaptionLastColumnIndex = ROW_CAPTION_START_COLUMN_INDEX + axesSectorsCount.Count - 1;
            string[] rowCaptionBuf = new string[axesSectorsCount.Count];
            int preWBColumnIndex = rowCaptionLastColumnIndex + 1;
            int wholeColumnIndex = preWBColumnIndex + 1;
            int dataStartColumnIndex = wholeColumnIndex + 1;
            string coloringFormat = GetResource.GetResourceData(resxPath, COLORING_COMMENT_FORMAT_KEY);
            string unitFormat = GetResource.GetResourceData(resxPath, COLORING_COMMENT_UNIT_FORMAT_KEY);
            string level1HighComment = GetResource.GetResourceData(resxPath, COLORING_COMMENT_LEVEL1_HIGH_KEY);
            string level2HighComment = GetResource.GetResourceData(resxPath, COLORING_COMMENT_LEVEL2_HIGH_KEY);
            string level1LowComment = GetResource.GetResourceData(resxPath, COLORING_COMMENT_LEVEL1_LOW_KEY);
            string level2LowComment = GetResource.GetResourceData(resxPath, COLORING_COMMENT_LEVEL2_LOW_KEY);
            string levelDelimiter = GetResource.GetResourceData(resxPath, COLORING_COMMENT_LEVEL_DELIMITER_KEY);
            string unitDelimiter = GetResource.GetResourceData(resxPath, COLORING_COMMENT_UNIT_DELIMITER_KEY);
            string captionFormat = GetResource.GetResourceData(resxPath, COMMENT_CAPTION_FORMAT_KEY);
            int startRowIndex = CAPTION_ROW_INDEX + 2 + 2;
            string rowDelimiter = "\n" + MULTIPLY_LETTER + "\n";
            string captionBuf = string.Format(captionFormat, data[startRowIndex, CAPTION_COLUMN_INDEX].Value.Replace(rowDelimiter, rowCaptionDelimiter));
            StringBuilder buffer = new StringBuilder(string.Empty);
            if (axesSectorsCount.Count == 2)    // 二重クロス
            {
                int lastRowIndex = startRowIndex + axesSectorsCount[0] - 1;
                string buf = getColoringComment(data, itemSectorIndexes, coloringSetting, minSamplesCount
                                              , rowCaptionLastColumnIndex, rowCaptionBuf
                                              , preWBColumnIndex, dataStartColumnIndex
                                              , startRowIndex, lastRowIndex
                                              , rowCaptionDelimiter, coloringFormat, unitFormat
                                              , level1HighComment, level2HighComment
                                              , level1LowComment, level2LowComment
                                              , levelDelimiter, unitDelimiter);
                buffer.Append(buf);
            }
            else
            {
                startRowIndex -= 2;
                for (int i = 0; i < axesSectorsCount[0]; ++i)
                {
                    startRowIndex += 2; // 表側の無回答、非該当分
                    rowCaptionBuf[0] = data[startRowIndex, ROW_CAPTION_START_COLUMN_INDEX].Value;
                    startRowIndex += 2; // 表側の全体、小計分
                    int lastRowIndex = startRowIndex + axesSectorsCount[1] - 1;
                    string buf = getColoringComment(data, itemSectorIndexes, coloringSetting, minSamplesCount
                                                  , rowCaptionLastColumnIndex, rowCaptionBuf
                                                  , preWBColumnIndex, dataStartColumnIndex
                                                  , startRowIndex, lastRowIndex
                                                  , rowCaptionDelimiter, coloringFormat, unitFormat
                                                  , level1HighComment, level2HighComment
                                                  , level1LowComment, level2LowComment
                                                  , levelDelimiter, unitDelimiter);
                    buffer.Append(buf);
                }
            }
            if (buffer.Length > 0) buffer.Insert(0, "\n");
            buffer.Insert(0, captionBuf);
            return buffer.ToString();
        }

        /// <summary>
        /// コメントを生成して返す静的メソッド
        /// </summary>
        /// <param name="resxPath">resxファイルのパス</param>
        /// <param name="data">データを保持したDataWithMarking型の二次元配列</param>
        /// <param name="question">集計対象質問を表すQuestionクラスのインスタンスへの参照</param>
        /// <param name="axesSectorsCount">
        /// 集計軸質問の選択肢数を保持したList&lt;int&gt;クラスのインスタンスへの参照
        /// <note>要素数最大2つまで。3つ目以降は無視される</note>
        /// </param>
        /// <param name="plusSubTotal">
        /// 小計行も全体コメント対象とする場合true
        /// <note><paramref name="cutWholeComment"/>がtrueの場合無視される</note>
        /// </param>
        /// <param name="coloringSetting">色付けマーキングの設定内容を表すDataMarking列挙型の値</param>
        /// <param name="minSamplesCount">最小ベース数</param>
        /// <param name="cutWholeComment">
        /// 全体コメントをカットする場合はtrue (省略可:既定値false)
        /// <note>1シート1クロス形式の集計表の場合、各表の集計結果ごとにコメントを得てマージする際に、2つ目以降の表ではtrueを指定する</note>
        /// </param>
        /// <returns></returns>
        public static string GetComment(string resxPath
                  , DataWithMarking[,] data, Questions.Question question
                  , List<int>axesSectorsCount
                  , bool plusSubTotal, ColoringSetting coloringSetting, int minSamplesCount
                  , bool cutWholeComment = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(resxPath) || !File.Exists(resxPath) || data == null || question == null) return null;
                QuestionType itemQuestionType = question.QuestionType;
                bool isMatrix = (itemQuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent;
                if ((int)(itemQuestionType &= QuestionType.SA | QuestionType.MA) == 0) return null;
                int itemSectorsCount = question.Sectors.Count;
                List<int> choicedSectorIndexes = null;
                if (axesSectorsCount != null)
                {
                    for (int i = axesSectorsCount.Count - 1; i >= 2; --i)
                    {
                        axesSectorsCount.RemoveAt(i);
                    }
                }
                StringBuilder buf = new StringBuilder(
                        cutWholeComment ? string.Empty : getWholeComment(resxPath, data, isMatrix, itemSectorsCount, out choicedSectorIndexes, axesSectorsCount, plusSubTotal));
                if (axesSectorsCount != null && axesSectorsCount.Count > 0 && choicedSectorIndexes != null && choicedSectorIndexes.Count > 0)
                {
                    coloringSetting &= ColoringSetting.AllBit;
                    if ((int)coloringSetting != 0)
                    {
                        buf.Append((cutWholeComment ? string.Empty : "\n\n") + getColoringComment(resxPath, data, choicedSectorIndexes, axesSectorsCount, coloringSetting, minSamplesCount));
                    }
                }
                return buf.ToString();
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
    }
}
