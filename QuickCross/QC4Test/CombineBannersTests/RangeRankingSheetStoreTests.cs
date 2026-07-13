using NSubstitute;
using Qc4Launcher.Logic.CombineBanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QC4Test.CombineBannersTests
{
    public class RangeRankingSheetStoreTests
    {
        [Fact]
        public void ClearRangeRankingSheetData_ClearsAllData()
        {
            // Arrange

            RangeRankingSheetStore.NDataList.Add(new RangeRankingSheetData());

            // Act
            RangeRankingSheetStore.ClearRangeRankingSheetData();

            // Assert
            Assert.Empty(RangeRankingSheetStore.NDataList);
        }
    }
}