using Macromill.QCWeb.Tabulation;
using Qc4Launcher.Summary;
using System;
using System.Collections.Generic;
using Xunit;

namespace QC4Test.SummaryTable
{
    public class SummaryTableTests
    {
        #region Initial setup
        readonly SummaryTabulation tabulation = new SummaryTabulation();
        private DataWithMarking[][,] GenearteTabulationData()
        {
            DataWithMarking[][,] data3dArray = new DataWithMarking[1][,];
            DataWithMarking[,] data2dArray = new DataWithMarking[5, 6];
            for (int i = 2; i < 5; i++)
            {
                DataWithMarking newItem = new DataWithMarking(i.ToString());
                newItem.Percent = Convert.ToDouble(i);
                data2dArray.SetValue(newItem, i, 3);
            }
            data3dArray.SetValue(data2dArray, 0);
            return data3dArray;
        }
        private Dictionary<string, DataWithMarking[,]> GenearteValidCasesData()
        {
            Dictionary<string, DataWithMarking[,]> data3dArray = new Dictionary<string, DataWithMarking[,]>();
            data3dArray.Add("key1", GetData());
            return data3dArray;
        }
        private Dictionary<string, DataWithMarking[][,]> GenearteValidCasesDataForGroupisNotnull()
        {
            Dictionary<string, DataWithMarking[][,]> dataDictionary = new Dictionary<string, DataWithMarking[][,]>();
            DataWithMarking[][,] data3dArray = new DataWithMarking[1][,];
            data3dArray.SetValue(GetData(), 0);
            dataDictionary.Add("key1", data3dArray);
            return dataDictionary;
        }
        private DataWithMarking[,] GetData()
        {
            DataWithMarking[,] data2dArray = new DataWithMarking[5, 6];
            for (int i = 2; i < 5; i++)
            {
                DataWithMarking newItem = new DataWithMarking((i + 2).ToString());
                newItem.Percent = Convert.ToDouble(i + 2);
                data2dArray.SetValue(newItem, i, 4);
            }
            return data2dArray;
        }
        #endregion

        #region Test cases for Group is null
        [Fact]
        public void ValidCaseSetTrueWithCheckNullIfGroupIsNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[,]> summaryListValidTotalDict = GenearteValidCasesData();
            Assert.NotNull(tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, true));
        }
        [Fact]
        public void ValidCaseSetFalseWithCheckNullIfGroupIsNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[,]> summaryListValidTotalDict = GenearteValidCasesData();
            Assert.NotNull(tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, false));
        }
        [Fact]
        public void ValidCaseSetTrueWithCheckArraySizeIfGroupIsNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[,]> summaryListValidTotalDict = GenearteValidCasesData();
            Assert.Equal(5, tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, true)[0].GetLength(0));
        }
        [Fact]
        public void ValidCaseSetFalseWithCheckArraySizeIfGroupIsNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[,]> summaryListValidTotalDict = GenearteValidCasesData();
            Assert.Equal(5, tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, false)[0].GetLength(0));
        }
        [Fact]
        public void ValidCaseSetTrueWithCheckArrayValuesIfGroupIsNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[,]> summaryListValidTotalDict = GenearteValidCasesData();
            DataWithMarking[][,] tabulationArrayOut = tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, true);
            for (int i = 0; i < tabulationArrayOut.GetLength(0); i++)
            {
                Assert.Equal(((DataWithMarking)tabulationArrayOut[0].GetValue(i, 3)).NumValue, ((DataWithMarking)summaryListValidTotalDict["key1"].GetValue(i, 4)).NumValue);
                Assert.Equal(((DataWithMarking)tabulationArrayOut[0].GetValue(i, 3)).Percent, ((DataWithMarking)summaryListValidTotalDict["key1"].GetValue(i, 4)).Percent);
            }
        }
        [Fact]
        public void ValidCaseSetFalseWithCheckArrayValuesIfGroupIsNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[,]> summaryListValidTotalDict = GenearteValidCasesData();
            DataWithMarking[][,] tabulationArrayOut = tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, false);
            for (int i = 0; i < tabulationArrayOut.GetLength(0); i++)
            {
                Assert.Equal(((DataWithMarking)tabulationArray[0].GetValue(i, 3)).NumValue, ((DataWithMarking)tabulationArrayOut[0].GetValue(i, 3)).NumValue);
                Assert.Equal(((DataWithMarking)tabulationArray[0].GetValue(i, 3)).Percent, ((DataWithMarking)tabulationArrayOut[0].GetValue(i, 3)).Percent);
            }
        }
        #endregion

        #region Test cases for Group is not null
        [Fact]
        public void ValidCaseSetTrueWithCheckNullIfGroupIsNotNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[][,]> summaryListValidTotalDict = GenearteValidCasesDataForGroupisNotnull();
            Assert.NotNull(tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, true, 1));
        }
        [Fact]
        public void ValidCaseSetFalseWithCheckNullIfGroupIsNotNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[][,]> summaryListValidTotalDict = GenearteValidCasesDataForGroupisNotnull();
            Assert.NotNull(tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, false, 1));
        }
        [Fact]
        public void ValidCaseSetTrueWithCheckArraySizeIfGroupIsNotNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[][,]> summaryListValidTotalDict = GenearteValidCasesDataForGroupisNotnull();
            Assert.Equal(5, tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, true, 1)[0].GetLength(0));
        }
        [Fact]
        public void ValidCaseSetFalseWithCheckArraySizeIfGroupIsNotNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[][,]> summaryListValidTotalDict = GenearteValidCasesDataForGroupisNotnull();
            Assert.Equal(5, tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, false, 1)[0].GetLength(0));
        }
        [Fact]
        public void ValidCaseSetTrueWithCheckArrayValuesIfGroupIsNotNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[][,]> summaryListValidTotalDict = GenearteValidCasesDataForGroupisNotnull();
            DataWithMarking[][,] tabulationArrayOut = tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, true, 1);
            for (int i = 0; i < tabulationArrayOut.GetLength(0); i++)
            {
                Assert.Equal(((DataWithMarking)tabulationArrayOut[0].GetValue(i, 3)).NumValue, ((DataWithMarking)summaryListValidTotalDict["key1"][0].GetValue(i, 4)).NumValue);
                Assert.Equal(((DataWithMarking)tabulationArrayOut[0].GetValue(i, 3)).Percent, ((DataWithMarking)summaryListValidTotalDict["key1"][0].GetValue(i, 4)).Percent);
            }
        }
        [Fact]
        public void ValidCaseSetFalseWithCheckArrayValuesIfGroupIsNotNull()
        {
            DataWithMarking[][,] tabulationArray = GenearteTabulationData();
            Dictionary<string, DataWithMarking[][,]> summaryListValidTotalDict = GenearteValidCasesDataForGroupisNotnull();
            DataWithMarking[][,] tabulationArrayOut = tabulation.ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, false, 1);
            for (int i = 0; i < tabulationArrayOut.GetLength(0); i++)
            {
                Assert.Equal(((DataWithMarking)tabulationArray[0].GetValue(i, 3)).NumValue, ((DataWithMarking)tabulationArrayOut[0].GetValue(i, 3)).NumValue);
                Assert.Equal(((DataWithMarking)tabulationArray[0].GetValue(i, 3)).Percent, ((DataWithMarking)tabulationArrayOut[0].GetValue(i, 3)).Percent);
            }
        }
        #endregion

        #region Test Replace main method
        [Fact]
        public void TestWithTabulationArrayIsNull()
        {
            DataWithMarking[,] tabulationArray = GenearteTabulationData()[0];
            DataWithMarking[,] summaryListValidTotalDict = GenearteValidCasesData()["key1"];
            Assert.NotNull(tabulation.ReplaceTotalValues(tabulationArray, summaryListValidTotalDict));
        }
        [Fact]
        public void TestWithTabulationArraySize()
        {
            DataWithMarking[,] tabulationArray = GenearteTabulationData()[0];
            DataWithMarking[,] summaryListValidTotalDict = GenearteValidCasesData()["key1"];
            Assert.Equal(5, tabulation.ReplaceTotalValues(tabulationArray, summaryListValidTotalDict).GetLength(0));
        }
        [Fact]
        public void TestWithTabulationArrayValues()
        {
            DataWithMarking[,] tabulationArray = GenearteTabulationData()[0];
            DataWithMarking[,] summaryListValidTotalDict = GenearteValidCasesData()["key1"];
            DataWithMarking[,] tabulationArrayOut = tabulation.ReplaceTotalValues(summaryListValidTotalDict, tabulationArray);
            for (int i = 0; i < tabulationArrayOut.GetLength(0); i++)
            {
                Assert.Equal(((DataWithMarking)tabulationArrayOut.GetValue(i, 3)).NumValue, ((DataWithMarking)summaryListValidTotalDict.GetValue(i, 4)).NumValue);
                Assert.Equal(((DataWithMarking)tabulationArrayOut.GetValue(i, 3)).Percent, ((DataWithMarking)summaryListValidTotalDict.GetValue(i, 4)).Percent);
            }
        }
        #endregion
    }
}
