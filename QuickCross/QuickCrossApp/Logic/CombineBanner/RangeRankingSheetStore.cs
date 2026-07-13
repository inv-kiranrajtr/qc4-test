using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;

namespace Qc4Launcher.Logic.CombineBanner
{
    public static class RangeRankingSheetStore
    {
        /// <summary>
        /// Represents a list of <see cref="RangeRankingSheetData"/> for  N data.
        /// </summary>
        public static List<RangeRankingSheetData> NDataList { get; } = new List<RangeRankingSheetData>();

        /// <summary>
        /// Represents a list of <see cref="RangeRankingSheetData"/> for % data.
        /// </summary>
        public static List<RangeRankingSheetData> ModDataList { get; } = new List<RangeRankingSheetData>();

        /// <summary>
        /// Represents a list of <see cref="RangeRankingSheetData"/> for N% data.
        /// </summary>
        public static List<RangeRankingSheetData> NModeDataList { get; } = new List<RangeRankingSheetData>();

        /// <summary>
        /// Clears all data in the range ranking sheet lists.
        /// </summary>
        public static void ClearRangeRankingSheetData()
        {
            NModeDataList.Clear();
            ModDataList.Clear();
            NDataList.Clear();
        }

    }
    public class RangeRankingSheetData
    {
        public CellRangeAddress Range { get; set; }
        public Array Ranking { get; set; }
        public ISheet Sheet { get; set; }
    }

}
