using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Macromill.QCWeb.ReportRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;
using A14 = DocumentFormat.OpenXml.Office2010.Drawing;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Common.Constants;
using Qc4Launcher.Util;
using System.Collections;

namespace Qc4Launcher.Summary.OpenXml
{
    public class SummaryNPFormat
    {
        public void CreateNperTable(OutputCross CurrentOutput, WorksheetPart worksheetPart,ref int rowNum,int maxAxisCnt,bool CurrentOutputShowPreWBTotal,
                                    bool CutNAColumn,bool CutIVColumn,int ItemSectorsCount,CrossTable table,int numDigit, Hashtable CutRowsCol)
        {
            bool useSameSheet = false;
            int[] styleIndexArrayHeader = null;
            int[] styleIndexArrayDouble = null;
            int mergeRowNum = rowNum + 2,rowStyleIdx,edgeleftCellIdx;
            int[] colIndex = new int[11];
            int i = 0,lstMergeLetter = 4,idx = 0,fRow,cellH=7, cellH1 = 50, cellH2 = 11;
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            MergeCells mergeCells = null;           
            mergeCells = worksheetPart.Worksheet.Descendants<MergeCells>().FirstOrDefault();
            if (mergeCells == null)
                mergeCells = new MergeCells();
            if(numDigit == 2)
            {
                cellH = 205; cellH1 = 206; cellH2 = 207;
            }
            if (rowNum == 1)
            {
                rowStyleIdx = 1; edgeleftCellIdx = 203;
                styleIndexArrayHeader = new int[] { 9, 1, 178, 178, 179, 17, 17, 16, 16, 15, 15, 9, 1, 19, 19, 43, 22, 22, 60, 60, 29, 29, 9, 4, 168, 168, 169, 14, 14, cellH, cellH, 6, 6 };
                styleIndexArrayDouble = new int[] { 9, 4, 168, 168, 169, 14, 14, cellH, cellH, 6, 6,9, 4, 170, 180, 181, 49, 49, cellH1, cellH1, 51, 51, 9, 4, 171, 182, 183, 33, 33, cellH2, cellH2, 10, 10 };
            }
            else
            {
                useSameSheet = true;
                rowStyleIdx = 79; edgeleftCellIdx = 204;
                styleIndexArrayHeader = new int[] { 105, 78, 151, 151, 152, 82, 82, 83, 83, 84, 84, 105, 78, 85, 85, 86, 87, 87, 88, 88, 89, 89, 105, 90, 153, 153, 154, 91, 91, 92, 92, 93, 93 };
                styleIndexArrayDouble = new int[] { 105, 90, 153, 153, 154, 91, 91, 92, 92, 93, 93, 105, 90, 155, 162, 163, 96, 96, 97, 97, 98, 98, 105, 90, 156, 164, 165, 100, 100, 101, 101, 102, 102 };
            }
            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }

            if ((maxAxisCnt & 2) == 2)
            {
                colIndex[i] = 4;
                i++;
                lstMergeLetter = 5;
            }

            colIndex[i] = 6;
            i++;
            if (ItemSectorsCount == 1)
            {
                colIndex[i] = 7;
                i++;
            }
            if (ItemSectorsCount > 1)
            {
                colIndex[i] = 7;
                i++;
                colIndex[i] = 8;
                i++;
            }
            if (CurrentOutput.ShowIVAtItem && (CutIVColumn == false))
            {
                colIndex[i] = 9;
                i++;
            }
            if (CurrentOutput.ShowNAAtItem && (CutNAColumn == false))
            {
                colIndex[i] = 10;
                i++;
            }
            //Header
            if (rowNum > 1)
                CreateShadedRows(sheetData, ref rowNum);
            else
            {
                int[] indexArray =  { 9,1,9,2,9,177,9,177,9,215};
                CreateIndividualRows(sheetData, ref rowNum, indexArray);
            }

            fRow = rowNum;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (uint)rowStyleIdx, CustomFormat = true, Height = 81D, CustomHeight = true };
            CreateRow(row6,sheetData,ref colIndex, styleIndexArrayHeader, rowNum,ItemSectorsCount,i,11);
            MergeCell mergeCell6 = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
            mergeCells.Append(mergeCell6);

            rowNum++; 
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (uint)rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
            CreateRow(row7, sheetData,ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i,11);    

            rowNum++;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (uint)rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
            CreateRow(row8, sheetData,ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i,11);
            MergeCell mergeCell8 = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
            mergeCells.Append(mergeCell8);
            rowNum++;
            var rng = table.AxesGroups[0].Count == 1 ? "_Double" : "_Triple";
            int y = 2, idxLmt = i;
            //Double
            for (idx = 0; idx <= table.AxesGroups.Count - 1; idx++)
            {
                int j = 0; y = y + 1;
                while (j < i)
                {
                    colIndex[j] -= (idx == 0) ? 33 : 22;
                    j++;
                }
                if (rng == "_Double")
                    Double(sheetData,ref rowNum, ref mergeCells, colIndex, i, ItemSectorsCount, table, idx,11,ref styleIndexArrayDouble, (uint)rowStyleIdx
                            , lstMergeLetter, ref y, CutRowsCol, idxLmt,0);
            }
            int lCol = (ItemSectorsCount == 1) ? i : i + (ItemSectorsCount - 2);
            DrawEdgeBottom(lCol,rowNum,sheetData, useSameSheet);
            DrawEdgeLeft(worksheetPart,sheetData, fRow, (rowNum -1), lCol, edgeleftCellIdx);

            MergeCell mergeCell3 = new MergeCell() { Reference = "C" + mergeRowNum + ":R" + mergeRowNum };
            MergeCell mergeCell4 = new MergeCell() { Reference = "D" + (mergeRowNum + 1) + ":R" + (mergeRowNum + 1) };
            mergeCells.Append(mergeCell3);
            mergeCells.Append(mergeCell4);
          
            if (mergeRowNum == 3)
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }
        public void CreatePerTable(OutputCross CurrentOutput, WorksheetPart worksheetPart,ref int rowNum, int maxAxisCnt, bool CurrentOutputShowPreWBTotal,
                                     bool CutNAColumn, bool CutIVColumn, int ItemSectorsCount, CrossTable table, int numDigit,string summaryType
                                   , Hashtable CutRowsCol)
        {
            bool useSameSheet = false;
            int[] styleIndexArrayHeader = null;
            int[] styleIndexArrayDouble = null;
            int mergeRowNum = rowNum + 2, rowStyleIdx, edgeleftCellIdx;
            int[] colIndex = new int[11];
            int i = 0, lstMergeLetter = 4, idx = 0, fRow, cellH = 21, cellH1 = 58, cellH2 = 27;
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            MergeCells mergeCells = null;
            mergeCells = worksheetPart.Worksheet.Descendants<MergeCells>().FirstOrDefault();
            if (mergeCells == null)
                mergeCells = new MergeCells();
            if (numDigit == 2)
            {
                cellH = 205; cellH1 = 206; cellH2 = 207;
            }
            else if(numDigit == 0 && summaryType == "SUM")
            {
                cellH = 7; cellH1 = 50; cellH2 = 11;
            }
            else if(numDigit == 0)
            {
                cellH = 92; cellH1 = 97; cellH2 = 101;
            }
            if (rowNum == 1)
            {
                rowStyleIdx = 1; edgeleftCellIdx = 203;
                styleIndexArrayHeader = new int[] { 9, 1, 178, 178, 179, 17, 17, 16, 16, 15, 15, 9, 1, 19, 19, 43, 22, 22, 60, 60, 29, 29, 9, 4, 168,168, 169, 14, 14, cellH, cellH, 20, 20 };
                styleIndexArrayDouble = new int[] { 9, 4, 168, 168, 169, 14, 14, cellH, cellH, 20, 20,9, 4, 170, 180, 181, 49, 49, cellH1, cellH1, 56, 56, 9, 4, 171, 182, 183, 33, 33, cellH2, cellH2, 24, 24 };
            }
            else
            {
                useSameSheet = true;
                rowStyleIdx = 79; edgeleftCellIdx = 204;
                styleIndexArrayHeader = new int[] { 105, 78, 151, 151, 152, 82, 82, 83, 83, 84, 84, 105, 78, 85, 85, 86, 87, 87, 88, 88, 89, 89, 105, 90, 153, 153, 154, 91, 91, cellH, cellH, 127, 127};
                styleIndexArrayDouble = new int[] { 105, 90, 153, 153, 154, 91, 91, cellH, cellH, 127, 127,105, 90, 155, 162, 163, 96, 96, cellH1, cellH1, 130, 130, 105, 90, 156, 164, 165, 100, 100, cellH2, cellH2, 132, 132 };
            }
            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }

            if ((maxAxisCnt & 2) == 2)
            {
                colIndex[i] = 4;
                i++;
                lstMergeLetter = 5;
            }
           
            colIndex[i] = 6;
            i++;
            if (ItemSectorsCount == 1)
            {
                colIndex[i] = 7;
                i++;
            }
            if (ItemSectorsCount > 1)
            {
                colIndex[i] = 7;
                i++;
                colIndex[i] = 8;
                i++;
            }
            if (CurrentOutput.ShowIVAtItem && (CutIVColumn == false))
            {
                colIndex[i] = 9;
                i++;
            }
            if (CurrentOutput.ShowNAAtItem && (CutNAColumn == false))
            {
                colIndex[i] = 10;
                i++;
            }
            //Header
            if (rowNum > 1)
                CreateShadedRows(sheetData, ref rowNum);
            else
            {
                int[] indexArray = { 9, 1, 9, 2, 9, 177, 9, 177, 9, 215 };
                CreateIndividualRows(sheetData, ref rowNum, indexArray);
            }

            fRow = rowNum;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (uint)rowStyleIdx, CustomFormat = true, Height = 81D, CustomHeight = true };
            CreateRow(row6, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i, 11);
            MergeCell mergeCell6 = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
            mergeCells.Append(mergeCell6);

            rowNum++;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (uint)rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
            CreateRow(row7, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i, 11);

            rowNum++;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (uint)rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
            CreateRow(row8, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i, 11);
            MergeCell mergeCell8 = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
            mergeCells.Append(mergeCell8);
            rowNum++;
            var rng = table.AxesGroups[0].Count == 1 ? "_Double" : "_Triple";
            int y = 2, idxLmt = i;
            //Double
            for (idx = 0; idx <= table.AxesGroups.Count - 1; idx++)
            {
                int j = 0; y = y + 1;
                while (j < i)
                {
                    colIndex[j] -= (idx == 0) ? 33 : 22;
                    j++;
                }
                if (rng == "_Double")
                    Double(sheetData, ref rowNum, ref mergeCells, colIndex, i, ItemSectorsCount, table, idx, 11, ref styleIndexArrayDouble, (uint)rowStyleIdx
                            , lstMergeLetter, ref y, CutRowsCol, idxLmt,0);
            }
            int lCol = (ItemSectorsCount == 1) ? i : i + (ItemSectorsCount - 2);
            DrawEdgeBottom(lCol, rowNum, sheetData,useSameSheet);
            DrawEdgeLeft(worksheetPart, sheetData, fRow, (rowNum - 1), lCol, edgeleftCellIdx);

            MergeCell mergeCell3 = new MergeCell() { Reference = "C" + mergeRowNum + ":R" + mergeRowNum };
            MergeCell mergeCell4 = new MergeCell() { Reference = "D" + (mergeRowNum + 1) + ":R" + (mergeRowNum + 1) };
            mergeCells.Append(mergeCell3);
            mergeCells.Append(mergeCell4);
            if (mergeRowNum == 3)
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }
        public void CreateSigTable(OutputCross CurrentOutput, WorksheetPart worksheetPart, ref int rowNum, int maxAxisCnt, bool CurrentOutputShowPreWBTotal,
                                    bool CutNAColumn, bool CutIVColumn, int ItemSectorsCount, CrossTable table, int numDigit, string summaryType
                                    , Hashtable CutRowsCol)
        {
            bool useSameSheet = false;
            int[] styleIndexArrayHeader = null;
            int[] styleIndexArrayDouble = null;
            int mergeRowNum = rowNum + 2, rowStyleIdx, edgeleftCellIdx;
            int[] colIndex = new int[12];
            int i = 0, lstMergeLetter = 5, idx = 0, fRow, cellI = 21, cellI1 = 59, cellI2 = 32, idxLmt=0;
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            MergeCells mergeCells = null;
            mergeCells = worksheetPart.Worksheet.Descendants<MergeCells>().FirstOrDefault();
            if (mergeCells == null)
                mergeCells = new MergeCells();
           
            if (numDigit == 0 && summaryType == "SUM")
            {
                cellI = 208; cellI1 = 209; cellI2 = 210;
            }
            else if (numDigit == 0)
            {
                cellI = 128; cellI1 = 200; cellI2 = 198;
            }
            else if (numDigit == 2)
            {
                cellI = 205; cellI1 = 206; cellI2 = 207;
            }
           
            if (rowNum == 1)
            {
                rowStyleIdx = 1; edgeleftCellIdx = 203;
                styleIndexArrayHeader = new int[] { 9, 1, 178, 178, 178,179, 17, 18, 16, 16, 15, 15, 9, 1, 19, 19, 42, 30,23, 23, 60, 60, 29, 29, 9, 4, 168, 168, 168,194, 8, 8, cellI, cellI, 20, 20 };
                styleIndexArrayDouble = new int[] { 9, 4, 168, 168, 168, 194, 8, 8, cellI, cellI, 20, 20 ,9, 4, 170, 173, 173,197, 52, 52, cellI1, cellI1, 57, 57, 9, 4, 171, 191, 191,193, 41, 41, 40, 40, 39, 39, 9, 4, 171, 176, 176,192, 28, 28, cellI2, cellI2, 31, 31, 9, 4, 171, 191, 191,193, 41, 41, 40, 40, 39, 39 };
            }
            else
            {
                useSameSheet = true;
                rowStyleIdx = 79; edgeleftCellIdx = 204;
                styleIndexArrayHeader = new int[] { 105, 78, 151, 151, 151, 152, 94, 84, 83, 83, 84, 84, 105, 78, 85, 85, 107, 108, 89, 89, 88, 88, 89, 89, 105, 90, 153, 153, 153, 154, 109, 109, cellI, cellI, 127, 127 };
                styleIndexArrayDouble = new int[] { 105, 90, 153, 153, 153, 154, 109, 109, cellI, cellI, 127, 127, 105, 90, 155, 158, 158, 190, 123, 123, cellI1, cellI1, 201, 201, 105, 90, 156, 186, 186, 188, 116, 116, 117, 117, 116, 116, 105, 90, 156, 161, 161, 187, 113, 113, cellI2, cellI2, 199, 199, 105, 90, 156, 186, 186, 188, 116, 116, 117, 117, 116, 116 };
            }
            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }

            if ((maxAxisCnt & 2) == 2)
            {
                colIndex[i] = 4;
                i++;
                lstMergeLetter = 6;
            }
            colIndex[i] = 5;
            i++;
           
            colIndex[i] = 7;
            i++;
            if (ItemSectorsCount == 1)
            {
                colIndex[i] = 8;
                i++;
            }
            if (ItemSectorsCount > 1)
            {
                colIndex[i] = 8;
                i++;
                colIndex[i] = 9;
                i++;
            }
            if (CurrentOutput.ShowIVAtItem && (CutIVColumn == false))
            {
                colIndex[i] = 10;
                i++;
            }
            if (CurrentOutput.ShowNAAtItem && (CutNAColumn == false))
            {
                colIndex[i] = 11;
                i++;
            }
            //Header
            if (rowNum > 1)
                CreateShadedRows(sheetData, ref rowNum);
            else
            {
                int[] indexArray = { 9, 1, 9, 2, 9, 177, 9, 177, 9, 218 };
                CreateIndividualRows(sheetData, ref rowNum, indexArray);
            }

            fRow = rowNum;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (uint)rowStyleIdx, CustomFormat = true, Height = 81D, CustomHeight = true };
            CreateRow(row6, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i, 12);
            MergeCell mergeCell6 = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
            mergeCells.Append(mergeCell6);

            rowNum++;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (uint)rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
            CreateRow(row7, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i, 12);

            rowNum++;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (uint)rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
            CreateRow(row8, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i, 12);
            MergeCell mergeCell8 = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
            mergeCells.Append(mergeCell8);
            rowNum++;
            var rng = table.AxesGroups[0].Count == 1 ? "_Double" : "_Triple";
            int y = 2; idxLmt = i;
            //Double
            for (idx = 0; idx <= table.AxesGroups.Count - 1; idx++)
            {
                int j = 0; y = y + 1;
                while (j < i)
                {
                    colIndex[j] -= (idx == 0) ? 36 : 36;
                    j++;
                }
                if (rng == "_Double")
                    Double(sheetData, ref rowNum, ref mergeCells, colIndex, i, ItemSectorsCount, table, idx, 12, ref styleIndexArrayDouble, (uint)rowStyleIdx
                                ,lstMergeLetter,ref y,CutRowsCol,idxLmt,1);
            }
            int lCol = (ItemSectorsCount == 1) ? i : i + (ItemSectorsCount - 2);
            DrawEdgeBottom(lCol, rowNum, sheetData, useSameSheet);
            DrawEdgeLeft(worksheetPart, sheetData, fRow, (rowNum - 1), lCol, edgeleftCellIdx);

            MergeCell mergeCell3 = new MergeCell() { Reference = "C" + mergeRowNum + ":R" + mergeRowNum };
            MergeCell mergeCell4 = new MergeCell() { Reference = "D" + (mergeRowNum + 1) + ":R" + (mergeRowNum + 1) };
            mergeCells.Append(mergeCell3);
            mergeCells.Append(mergeCell4);
            if (mergeRowNum == 3)
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }

        public void Double(SheetData sheetData, ref int rowNum, ref MergeCells mergeCells, int[] colIndex, int lmt, int ItemSectorsCount, CrossTable table,
                               int idx, int colCnt, ref int[] styleIndexArray, uint rowStyleIdx,int lstMergeLetter, ref int y, Hashtable CutRowsCol,int idxLmt
                               ,int rowDiff)
        {
            int strMrg1, strMrg2,j, count;
            int[] SectorsCount = new int[2];
            if (table.AxesGroups[idx].Count == 1)
            {
                AxesInformation tmpAxes = table.AxesGroups[idx];
                SectorsCount[0] = tmpAxes[0].SectorsCount;
                if (!CutRowsCol.ContainsKey(y) && idx > 0)
                {
                    Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, StyleIndex = rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
                    CreateRow(row, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt);
                    MergeCell mergeCell8 = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
                    mergeCells.Append(mergeCell8);
                    rowNum++;
                }
                else
                {
                    j = 0;
                    while (j < idxLmt)
                    {
                        colIndex[j] += colCnt;
                        j++;
                    }
                }
                y = y + SectorsCount[0];
                strMrg1 = rowNum;

                j = 0;
                while (j <= rowDiff)
                {
                    Row row1 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, StyleIndex = rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
                    CreateRow(row1, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt);
                    rowNum++;j++;
                }

                if (rowDiff > 0)
                {
                    MergeCell mergeCellD = new MergeCell() { Reference = "D" + strMrg1 + ":" + "D" + (strMrg1 + rowDiff) };
                    MergeCell mergeCellE = new MergeCell() { Reference = "E" + strMrg1 + ":" + "E" + (strMrg1 + rowDiff) };
                    mergeCells.Append(mergeCellD);
                    mergeCells.Append(mergeCellE);
                }

                for (int cnt = 1; cnt < SectorsCount[0]; cnt++)
                {
                    strMrg2 = rowNum;
                    j = 0; count = 0;
                    while (j <= rowDiff)
                    {
                        Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, StyleIndex = rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
                        CreateRowShiftDown(row3, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, count);
                        rowNum++; count = colCnt;j++;
                    }

                    if (rowDiff > 0)
                    {
                        MergeCell mergeCellD1 = new MergeCell() { Reference = "D" + strMrg2 + ":" + "D" + (strMrg2 + rowDiff) };
                        MergeCell mergeCellE1 = new MergeCell() { Reference = "E" + strMrg2 + ":" + "E" + (strMrg2 + rowDiff) };
                        mergeCells.Append(mergeCellD1);
                        mergeCells.Append(mergeCellE1);
                    }
                }
                MergeCell mergeCellC = new MergeCell() { Reference = "C" + strMrg1 + ":" + "C" + (rowNum - 1) };
                mergeCells.Append(mergeCellC);
            }
        }
        public void CreateRow(Row row,SheetData sheetData,ref int [] colIndex,int [] styleIndexArray,int rowNum,int ItemSectorsCount,int lmt,int colCnt)
        {
            int startCell = 1, styleIndex=0, endCell;
            while (startCell <= lmt)
            {
                Cell cell63 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                row.Append(cell63);
                colIndex[styleIndex] += colCnt; styleIndex++; startCell++;
                if (startCell == lmt && ItemSectorsCount > 2)
                {
                    endCell = startCell + (ItemSectorsCount - 2);
                    while (startCell <= endCell)
                    {
                        Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                        row.Append(cell64); startCell++;
                    }
                    colIndex[styleIndex] += colCnt;
                }
            }
            sheetData.Append(row);
        }
        private void CreateShadedRows(SheetData sheetData,ref int rowNum)
        {
            //Header
            Row row1 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (UInt32Value)79U, CustomFormat = true, Height = 11.25D, CustomHeight = true };
            Cell cell1 = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)105U };
            Cell cell2 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)78U };

            row1.Append(cell1);
            row1.Append(cell2);
            sheetData.Append(row1);
            rowNum++;

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (UInt32Value)79U, CustomFormat = true, Height = 11.25D, CustomHeight = true };
            Cell cell21 = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)105U };
            Cell cell22 = new Cell() { CellReference = "C" + rowNum, StyleIndex = (UInt32Value)80U };

            row2.Append(cell21);
            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (UInt32Value)79U, CustomFormat = true, Height = 22.5D, CustomHeight = true };
            Cell cell31 = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)105U };
            Cell cell33 = new Cell() { CellReference = "C" + rowNum, StyleIndex = (UInt32Value)150U };

            row3.Append(cell31);
            row3.Append(cell33);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (UInt32Value)79U, CustomFormat = true, Height = 24.95D, CustomHeight = true };
            Cell cell41 = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)105U };
            Cell cell42 = new Cell() { CellReference = "D" + rowNum, StyleIndex = (UInt32Value)150U };
            row4.Append(cell41);
            row4.Append(cell42);
            sheetData.Append(row4);
            rowNum++;

            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, StyleIndex = (UInt32Value)79U, CustomFormat = true, Height = 3D, CustomHeight = true };
            Cell cell51 = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)105U };
            row5.Append(cell51);
            sheetData.Append(row5);
            rowNum++;
        }
        public void CreateIndividualRows(SheetData sheetData, ref int rowNum, int[] indexArray)
        {
            //Header
            int sylIdx = 1;
            int stIndx = indexArray.Length > 9 ? indexArray[9] : indexArray[0];
            Row row1 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, Height = 11.25D, CustomHeight = true };
            Cell cell1 = new Cell() { CellReference = "A" + rowNum, StyleIndex = (uint)stIndx };
            Cell cell2 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (uint)indexArray[sylIdx++] };
           
            row1.Append(cell1);
            row1.Append(cell2);
            sheetData.Append(row1);
            rowNum++;

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, Height = 11.25D, CustomHeight = true };
            Cell cell21 = new Cell() { CellReference = "A" + rowNum, StyleIndex = (uint)indexArray[sylIdx++] };
            Cell cell22 = new Cell() { CellReference = "C" + rowNum, StyleIndex = (uint)indexArray[sylIdx++] };
           
            row2.Append(cell21);
            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, Height = 22.5D, CustomHeight = true };
            Cell cell31 = new Cell() { CellReference = "A" + rowNum, StyleIndex = (uint)indexArray[sylIdx++] };
            Cell cell33 = new Cell() { CellReference = "C" + rowNum, StyleIndex = (uint)indexArray[sylIdx++] };
            
            row3.Append(cell31);
            row3.Append(cell33);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, Height = 24.95D, CustomHeight = true };
            Cell cell41 = new Cell() { CellReference = "A" + rowNum, StyleIndex = (uint)indexArray[sylIdx++] };
            Cell cell42 = new Cell() { CellReference = "D" + rowNum, StyleIndex = (uint)indexArray[sylIdx++] };
            row4.Append(cell41);
            row4.Append(cell42);
            sheetData.Append(row4);
            rowNum++;
           
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, Height = 3D, CustomHeight = true };
            Cell cell51 = new Cell() { CellReference = "A" + rowNum, StyleIndex = (uint)indexArray[sylIdx++] };
            row5.Append(cell51);
            sheetData.Append(row5);
            rowNum++;
            
        }
        private void CreateRowShiftDown(Row row, SheetData sheetData, ref int[] colIndex, int[] styleIndexArray, int rowNum, int ItemSectorsCount, int lmt,int colCnt)
        {
            int startCell = 1, styleIndex = 0, endCell;
            while (startCell <= lmt)
            {
                Cell cell63 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]+ colCnt] };
                row.Append(cell63);
                styleIndex++; startCell++;
                if (startCell == lmt && ItemSectorsCount > 2)
                {
                    endCell = startCell + (ItemSectorsCount - 2);
                    while (startCell <= endCell)
                    {
                        Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]+ colCnt] };
                        row.Append(cell64); startCell++;
                    }
                }
            }
            sheetData.Append(row);
        }       
        private void DrawEdgeBottom(int lstCol,int rowNum,SheetData sheetData,bool useSameSheet)
        {
            int startCell = 3;
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, Height = 11.25D, CustomHeight = true };
            if (!useSameSheet)
            {       
                Cell cell6 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(1) + rowNum, StyleIndex = (uint)202 };
                row.Append(cell6);
            }
            while (startCell <= lstCol)
            {
                Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)202 };
                row.Append(cell64); startCell++;
            }
            sheetData.Append(row);
        }
        public void DrawEdgeLeft(WorksheetPart worksheetPart, SheetData sheetData, int fRow, int lRow, int lCol, int styleIdx)
        {
            lCol += 1;
            for (int rowIdx = fRow; rowIdx <= lRow; rowIdx++)
            {
                Row row = worksheetPart.Worksheet.Descendants<Row>().Where(p => p.RowIndex == rowIdx).FirstOrDefault();
                Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(lCol) + rowIdx, StyleIndex = (uint)styleIdx };
                row.Append(cell);
            }           
        }
        //Index
        public void GenerateTitleBox(DrawingsPart drawingsPart, string title)
        {
            Xdr.WorksheetDrawing worksheetDrawing1 = new Xdr.WorksheetDrawing();

            Xdr.TwoCellAnchor twoCellAnchor1 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.Absolute };

            Xdr.FromMarker fromMarker1 = new Xdr.FromMarker();
            Xdr.ColumnId columnId1 = new Xdr.ColumnId();
            columnId1.Text = "1";
            Xdr.ColumnOffset columnOffset1 = new Xdr.ColumnOffset();
            columnOffset1.Text = "0";
            Xdr.RowId rowId1 = new Xdr.RowId();
            rowId1.Text = "1";
            Xdr.RowOffset rowOffset1 = new Xdr.RowOffset();
            rowOffset1.Text = "0";

            fromMarker1.Append(columnId1);
            fromMarker1.Append(columnOffset1);
            fromMarker1.Append(rowId1);
            fromMarker1.Append(rowOffset1);

            Xdr.ToMarker toMarker1 = new Xdr.ToMarker();
            Xdr.ColumnId columnId2 = new Xdr.ColumnId();
            columnId2.Text = "5";
            Xdr.ColumnOffset columnOffset2 = new Xdr.ColumnOffset();
            columnOffset2.Text = "0";
            Xdr.RowId rowId2 = new Xdr.RowId();
            rowId2.Text = "4";
            Xdr.RowOffset rowOffset2 = new Xdr.RowOffset();
            rowOffset2.Text = "0";

            toMarker1.Append(columnId2);
            toMarker1.Append(columnOffset2);
            toMarker1.Append(rowId2);
            toMarker1.Append(rowOffset2);

            Xdr.Shape shape1 = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.NonVisualShapeProperties nonVisualShapeProperties1 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)2U, Name = "TitleBox" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList1 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension1 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement2 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{7EFDF2C9-B4AF-48D0-95AD-079AC0CD8844}\" />");

            nonVisualDrawingPropertiesExtension1.Append(openXmlUnknownElement2);

            nonVisualDrawingPropertiesExtensionList1.Append(nonVisualDrawingPropertiesExtension1);

            nonVisualDrawingProperties1.Append(nonVisualDrawingPropertiesExtensionList1);

            Xdr.NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties1 = new Xdr.NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks1 = new A.ShapeLocks() { NoChangeArrowheads = true };

            nonVisualShapeDrawingProperties1.Append(shapeLocks1);

            nonVisualShapeProperties1.Append(nonVisualDrawingProperties1);
            nonVisualShapeProperties1.Append(nonVisualShapeDrawingProperties1);

            Xdr.ShapeProperties shapeProperties1 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset1 = new A.Offset() { X = 243840L, Y = 137160L };
            A.Extents extents1 = new A.Extents() { Cx = 7033260L, Cy = 411480L };

            transform2D1.Append(offset1);
            transform2D1.Append(extents1);

            A.PresetGeometry presetGeometry1 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.RoundRectangle };

            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();
            A.ShapeGuide shapeGuide1 = new A.ShapeGuide() { Name = "adj", Formula = "val 16667" };

            adjustValueList1.Append(shapeGuide1);

            presetGeometry1.Append(adjustValueList1);

            A.SolidFill solidFill1 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "FFFFFF", LegacySpreadsheetColorIndex = 9, MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "a14" } };
            rgbColorModelHex1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            rgbColorModelHex1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");

            solidFill1.Append(rgbColorModelHex1);

            A.Outline outline1 = new A.Outline() { Width = 9525 };

            A.SolidFill solidFill2 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "0070C0" };

            solidFill2.Append(rgbColorModelHex2);
            A.Round round1 = new A.Round();
            A.HeadEnd headEnd1 = new A.HeadEnd();
            A.TailEnd tailEnd1 = new A.TailEnd();

            outline1.Append(solidFill2);
            outline1.Append(round1);
            outline1.Append(headEnd1);
            outline1.Append(tailEnd1);

            A.EffectList effectList1 = new A.EffectList();

            A.OuterShadow outerShadow1 = new A.OuterShadow() { Distance = 107763L, Direction = 2700000, Alignment = A.RectangleAlignmentValues.Center, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex() { Val = "808080" };
            A.Alpha alpha1 = new A.Alpha() { Val = 50000 };

            rgbColorModelHex3.Append(alpha1);

            outerShadow1.Append(rgbColorModelHex3);

            effectList1.Append(outerShadow1);

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(presetGeometry1);
            shapeProperties1.Append(solidFill1);
            shapeProperties1.Append(outline1);
            shapeProperties1.Append(effectList1);

            Xdr.TextBody textBody1 = new Xdr.TextBody();
            A.BodyProperties bodyProperties1 = new A.BodyProperties() { VerticalOverflow = A.TextVerticalOverflowValues.Clip, Wrap = A.TextWrappingValues.Square, LeftInset = 18288, TopInset = 0, RightInset = 0, BottomInset = 0, Anchor = A.TextAnchoringTypeValues.Center, UpRight = true };
            A.ListStyle listStyle1 = new A.ListStyle();

            A.Paragraph paragraph = new A.Paragraph();
            A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Center, RightToLeft = false };
            A.Run run2 = new A.Run();
            A.RunProperties runProperties2 = new A.RunProperties() { Language = QC4Common.Common.Constants.GlobalMode.Split(',')[0], AlternativeLanguage = QC4Common.Common.Constants.GlobalMode.Split(',')[0], FontSize = 1000 };
            A.LatinFont latinFont1 = new A.LatinFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };
            A.EastAsianFont eastAsianFont1 = new A.EastAsianFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };
            runProperties2.Append(latinFont1);
            runProperties2.Append(eastAsianFont1);
            A.Text text = new A.Text();
            text.Text = title;
            run2.Append(runProperties2);
            run2.Append(text);
            paragraph.Append(paragraphProperties1);
            paragraph.Append(run2);

            textBody1.Append(bodyProperties1);
            textBody1.Append(listStyle1);
            textBody1.Append(paragraph);

            shape1.Append(nonVisualShapeProperties1);
            shape1.Append(shapeProperties1);
            shape1.Append(textBody1);
            Xdr.ClientData clientData1 = new Xdr.ClientData();

            twoCellAnchor1.Append(fromMarker1);
            twoCellAnchor1.Append(toMarker1);
            twoCellAnchor1.Append(shape1);
            twoCellAnchor1.Append(clientData1);

            worksheetDrawing1.Append(twoCellAnchor1);

            drawingsPart.WorksheetDrawing = worksheetDrawing1;
        }
        public void GenerateSignificanceTestLegend(DrawingsPart drawingsPart, string value, int rowNum,bool crossSignificance = false)
        {
            bool isGlobalMode = QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? false : true;
            string frmCol, frmColOffSet, frmRow, frmRowOffset, toCol, toColOffSet, toRow, toRowOffset;
            if (crossSignificance)
            {
                frmCol = "3"; frmColOffSet = "2794525"; frmRow = rowNum.ToString(); ; frmRowOffset = "0";
                toCol = "5"; toColOffSet = "0"; toRow = (rowNum + 1).ToString(); toRowOffset = "0";
            }
            else
            {
                frmCol = "3"; frmColOffSet = "2614525"; frmRow = rowNum.ToString(); ; frmRowOffset = "0";
                toCol = "5"; toColOffSet = !isGlobalMode ? "0" : "38100"; toRow = (rowNum + 1).ToString(); toRowOffset = "0";
            }

           
            Xdr.TwoCellAnchor twoCellAnchor2 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.Absolute };

            Xdr.FromMarker fromMarker2 = new Xdr.FromMarker();
            Xdr.ColumnId columnId3 = new Xdr.ColumnId();
            columnId3.Text = frmCol;
            Xdr.ColumnOffset columnOffset3 = new Xdr.ColumnOffset();
            columnOffset3.Text = frmColOffSet;
            Xdr.RowId rowId3 = new Xdr.RowId();
            rowId3.Text = frmRow; 
            Xdr.RowOffset rowOffset3 = new Xdr.RowOffset();
            rowOffset3.Text = frmRowOffset;

            fromMarker2.Append(columnId3);
            fromMarker2.Append(columnOffset3);
            fromMarker2.Append(rowId3);
            fromMarker2.Append(rowOffset3);

            Xdr.ToMarker toMarker2 = new Xdr.ToMarker();
            Xdr.ColumnId columnId4 = new Xdr.ColumnId();
            columnId4.Text = toCol;
            Xdr.ColumnOffset columnOffset4 = new Xdr.ColumnOffset();
            columnOffset4.Text = toColOffSet;
            Xdr.RowId rowId4 = new Xdr.RowId();
            rowId4.Text = toRow; 
            Xdr.RowOffset rowOffset4 = new Xdr.RowOffset();
            rowOffset4.Text = toRowOffset;

            toMarker2.Append(columnId4);
            toMarker2.Append(columnOffset4);
            toMarker2.Append(rowId4);
            toMarker2.Append(rowOffset4);


            Xdr.Shape shape2 = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.NonVisualShapeProperties nonVisualShapeProperties2 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties2 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)3U, Name = "SignificanceTestLegend" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList2 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension2 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement3 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{FC657D47-6E43-4C91-9AA0-F159B1CD3FD9}\" />");

            nonVisualDrawingPropertiesExtension2.Append(openXmlUnknownElement3);

            nonVisualDrawingPropertiesExtensionList2.Append(nonVisualDrawingPropertiesExtension2);

            nonVisualDrawingProperties2.Append(nonVisualDrawingPropertiesExtensionList2);

            Xdr.NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties2 = new Xdr.NonVisualShapeDrawingProperties() { TextBox = true };
            A.ShapeLocks shapeLocks2 = new A.ShapeLocks() { NoChangeArrowheads = true };

            nonVisualShapeDrawingProperties2.Append(shapeLocks2);

            nonVisualShapeProperties2.Append(nonVisualDrawingProperties2);
            nonVisualShapeProperties2.Append(nonVisualShapeDrawingProperties2);

            Xdr.ShapeProperties shapeProperties2 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D2 = new A.Transform2D();
            A.Offset offset2 = new A.Offset() { X = 6136005L, Y = 1036320L };
            A.Extents extents2 = new A.Extents() { Cx = 1141095L, Cy = 838200L };

            transform2D2.Append(offset2);
            transform2D2.Append(extents2);

            A.PresetGeometry presetGeometry2 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList2 = new A.AdjustValueList();

            presetGeometry2.Append(adjustValueList2);

            A.SolidFill solidFill3 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            solidFill3.Append(rgbColorModelHex4);

            A.Outline outline2 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill4 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "A6A6A6" };

            solidFill4.Append(rgbColorModelHex5);
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.SystemDot };
            A.Miter miter1 = new A.Miter() { Limit = 800000 };
            A.HeadEnd headEnd2 = new A.HeadEnd();
            A.TailEnd tailEnd2 = new A.TailEnd();

            outline2.Append(solidFill4);
            outline2.Append(presetDash1);
            outline2.Append(miter1);
            outline2.Append(headEnd2);
            outline2.Append(tailEnd2);

            shapeProperties2.Append(transform2D2);
            shapeProperties2.Append(presetGeometry2);
            shapeProperties2.Append(solidFill3);
            shapeProperties2.Append(outline2);

            Xdr.TextBody textBody2 = new Xdr.TextBody();
            A.BodyProperties bodyProperties2 = new A.BodyProperties() { VerticalOverflow = A.TextVerticalOverflowValues.Clip, Wrap = A.TextWrappingValues.Square, LeftInset = 0, TopInset = 46800, RightInset = 0, BottomInset = 46800, Anchor = A.TextAnchoringTypeValues.Top };
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();
            paragraph2.Append(SetParagraphProperty());
            paragraph2.Append(SetValues(value));

            textBody2.Append(bodyProperties2);
            textBody2.Append(listStyle2);
            textBody2.Append(paragraph2);

            shape2.Append(nonVisualShapeProperties2);
            shape2.Append(shapeProperties2);
            shape2.Append(textBody2);
            Xdr.ClientData clientData2 = new Xdr.ClientData();

            twoCellAnchor2.Append(fromMarker2);
            twoCellAnchor2.Append(toMarker2);
            twoCellAnchor2.Append(shape2);
            twoCellAnchor2.Append(clientData2);
            drawingsPart.WorksheetDrawing.Append(twoCellAnchor2);
        }
        public void GenerateMarkingColoring(DrawingsPart drawingsPart,OutputCross CurrentOutput,string value,int rowNum,bool multiShape,bool crossSignificance = false)
        {
            bool isGlobalMode = QC4Common.Common.Constants.GlobalMode != "ja-JP," + Util.Constants.QCFont.MS_Gothic;
            long x, y, cx, cy, clrX, clrY, clrCx, clrCy;
            string clrIdx;
            string frmCol, frmColOffSet, frmRow, frmRowOffset, toCol, toColOffSet, toRow, toRowOffset;
            if (crossSignificance)
            {
                if (multiShape)
                {
                    frmCol = "3"; frmColOffSet = !isGlobalMode ? "847725" : "1570684"; frmRow = !isGlobalMode ? rowNum.ToString() : "8"; frmRowOffset = "0";
                    toCol = "3"; toColOffSet = !isGlobalMode ? "1838325" : "3427019"; toRow = !isGlobalMode ? (rowNum + 1).ToString() : "9"; toRowOffset = "0";
                }
                else
                {
                    frmCol = "3"; frmColOffSet = !isGlobalMode ? "1981200" : "1670684"; frmRow = !isGlobalMode ? rowNum.ToString() : "8"; ; frmRowOffset = "0";
                    toCol = "5"; toColOffSet = !isGlobalMode ? "0" : "2827019"; toRow = !isGlobalMode ? (rowNum + 1).ToString() : "9"; toRowOffset = "0";
                }
            }
            else
            {
                if (multiShape)
                {
                    frmCol = "3"; frmColOffSet = isGlobalMode ? "1147725" : "1547725"; frmRow = rowNum.ToString(); frmRowOffset = "0";
                    toCol = "3"; toColOffSet = "2538325"; toRow = (rowNum + 1).ToString(); toRowOffset = "0";
                }
                else
                {
                    frmCol = "3"; frmColOffSet = isGlobalMode ? "2481200" : "2681200"; frmRow = rowNum.ToString(); frmRowOffset = "0";//"1981200"
                    toCol = "5"; toColOffSet = "0"; toRow = (rowNum + 1).ToString(); toRowOffset = "0";
                }
            }
            SummaryCreatorXml summaryCreatorXml = new SummaryCreatorXml();
            Xdr.TwoCellAnchor twoCellAnchor3 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.Absolute };

            Xdr.FromMarker fromMarker3 = new Xdr.FromMarker();
            Xdr.ColumnId columnId5 = new Xdr.ColumnId();
            columnId5.Text = frmCol;
            Xdr.ColumnOffset columnOffset5 = new Xdr.ColumnOffset();
            columnOffset5.Text = frmColOffSet;
            Xdr.RowId rowId5 = new Xdr.RowId();
            rowId5.Text = frmRow;
            Xdr.RowOffset rowOffset5 = new Xdr.RowOffset();
            rowOffset5.Text =frmRowOffset;

            fromMarker3.Append(columnId5);
            fromMarker3.Append(columnOffset5);
            fromMarker3.Append(rowId5);
            fromMarker3.Append(rowOffset5);

            Xdr.ToMarker toMarker3 = new Xdr.ToMarker();
            Xdr.ColumnId columnId6 = new Xdr.ColumnId();
            columnId6.Text = toCol;
            Xdr.ColumnOffset columnOffset6 = new Xdr.ColumnOffset();
            columnOffset6.Text = toColOffSet;
            Xdr.RowId rowId6 = new Xdr.RowId();
            rowId6.Text = toRow;
            Xdr.RowOffset rowOffset6 = new Xdr.RowOffset();
            rowOffset6.Text = toRowOffset;

            toMarker3.Append(columnId6);
            toMarker3.Append(columnOffset6);
            toMarker3.Append(rowId6);
            toMarker3.Append(rowOffset6);

            Xdr.GroupShape groupShape = new Xdr.GroupShape();

            Xdr.NonVisualGroupShapeProperties nonVisualGroupShapeProperties1 = new Xdr.NonVisualGroupShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties3 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)31U, Name = "RateDifferenceLegend" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList3 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension3 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement4 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{00000000-0008-0000-0000-00001F000000}\" />");

            nonVisualDrawingPropertiesExtension3.Append(openXmlUnknownElement4);

            nonVisualDrawingPropertiesExtensionList3.Append(nonVisualDrawingPropertiesExtension3);

            nonVisualDrawingProperties3.Append(nonVisualDrawingPropertiesExtensionList3);

            Xdr.NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties1 = new Xdr.NonVisualGroupShapeDrawingProperties();
            A.GroupShapeLocks groupShapeLocks1 = new A.GroupShapeLocks();

            nonVisualGroupShapeDrawingProperties1.Append(groupShapeLocks1);

            nonVisualGroupShapeProperties1.Append(nonVisualDrawingProperties3);
            nonVisualGroupShapeProperties1.Append(nonVisualGroupShapeDrawingProperties1);

            Xdr.GroupShapeProperties groupShapeProperties1 = new Xdr.GroupShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.TransformGroup transformGroup1 = new A.TransformGroup();
            A.Offset offset3 = new A.Offset() { X = 4954905L, Y = 685800L };
            A.Extents extents3 = new A.Extents() { Cx = 990600L, Cy = 838200L };
            A.ChildOffset childOffset1 = new A.ChildOffset() { X = 93L, Y = 83L };
            A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = 104L, Cy = 88L };

            transformGroup1.Append(offset3);
            transformGroup1.Append(extents3);
            transformGroup1.Append(childOffset1);
            transformGroup1.Append(childExtents1);

            groupShapeProperties1.Append(transformGroup1);

            Xdr.Shape shape = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.NonVisualShapeProperties nonVisualShapeProperties3 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties4 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)32U, Name = "RateDifferenceLegend" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList4 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension4 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement5 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{00000000-0008-0000-0000-000020000000}\" />");

            nonVisualDrawingPropertiesExtension4.Append(openXmlUnknownElement5);

            nonVisualDrawingPropertiesExtensionList4.Append(nonVisualDrawingPropertiesExtension4);

            nonVisualDrawingProperties4.Append(nonVisualDrawingPropertiesExtensionList4);

            Xdr.NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties3 = new Xdr.NonVisualShapeDrawingProperties() { TextBox = true };
            A.ShapeLocks shapeLocks3 = new A.ShapeLocks() { NoChangeArrowheads = true };

            nonVisualShapeDrawingProperties3.Append(shapeLocks3);

            nonVisualShapeProperties3.Append(nonVisualDrawingProperties4);
            nonVisualShapeProperties3.Append(nonVisualShapeDrawingProperties3);

            Xdr.ShapeProperties shapeProperties3 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D3 = new A.Transform2D();
            A.Offset offset4 = new A.Offset() { X = 93L, Y = 83L };
            A.Extents extents4 = new A.Extents() { Cx = 104L, Cy = 88L };

            transform2D3.Append(offset4);
            transform2D3.Append(extents4);

            A.PresetGeometry presetGeometry3 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList3 = new A.AdjustValueList();

            presetGeometry3.Append(adjustValueList3);

            A.SolidFill solidFill16 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex25 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            solidFill16.Append(rgbColorModelHex25);

            A.Outline outline6 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill17 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex26 = new A.RgbColorModelHex() { Val = "A6A6A6" };

            solidFill17.Append(rgbColorModelHex26);
            A.PresetDash presetDash5 = new A.PresetDash() { Val = A.PresetLineDashValues.SystemDot };
            A.Miter miter2 = new A.Miter() { Limit = 800000 };
            A.HeadEnd headEnd3 = new A.HeadEnd();
            A.TailEnd tailEnd3 = new A.TailEnd();

            outline6.Append(solidFill17);
            outline6.Append(presetDash5);
            outline6.Append(miter2);
            outline6.Append(headEnd3);
            outline6.Append(tailEnd3);

            shapeProperties3.Append(transform2D3);
            shapeProperties3.Append(presetGeometry3);
            shapeProperties3.Append(solidFill16);
            shapeProperties3.Append(outline6);
           
            shape.Append(nonVisualShapeProperties3);
            shape.Append(shapeProperties3);

            Xdr.TextBody textBody = new Xdr.TextBody();
            A.BodyProperties bodyProperties3 = new A.BodyProperties() { VerticalOverflow = A.TextVerticalOverflowValues.Clip, Wrap = A.TextWrappingValues.Square, LeftInset = 0, TopInset = 46800, RightInset = 0, BottomInset = 46800, Anchor = A.TextAnchoringTypeValues.Top };
            A.ListStyle listStyle3 = new A.ListStyle();

            A.Paragraph paragraph3 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Left, RightToLeft = false };
            A.DefaultRunProperties defaultRunProperties3 = new A.DefaultRunProperties() { FontSize = 1000 };

            paragraphProperties3.Append(defaultRunProperties3);

            A.RunProperties runProperties10 = new A.RunProperties() { Language = "en-US", AlternativeLanguage = "ja-JP", FontSize = 900, Bold = false, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Baseline = 0 };

            A.SolidFill solidFill20 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex29 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill20.Append(rgbColorModelHex29);
            A.LatinFont latinFont12 = new A.LatinFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };
            A.EastAsianFont eastAsianFont12 = new A.EastAsianFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };

            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = "ja-JP", AlternativeLanguage = "en-US" };

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(SetValues(value));
            paragraph3.Append(endParagraphRunProperties2);

            textBody.Append(bodyProperties3);
            textBody.Append(listStyle3);
            textBody.Append(paragraph3);

            shape.Append(textBody);
            groupShape.Append(nonVisualGroupShapeProperties1);
            groupShape.Append(groupShapeProperties1);
            groupShape.Append(shape);
            
            bool showLevel1High = !CurrentOutput.MarkingColoringLevel2High && CurrentOutput.MarkingColoringLevel1High ? false : true;
            bool showLevel1Low = !CurrentOutput.MarkingColoringLevel2Low ? false : true;
            bool showLevel2High = !CurrentOutput.MarkingColoringLevel2High && CurrentOutput.MarkingColoringLevel1High ? true : false;
            bool showlevel2Low = !CurrentOutput.MarkingColoringLevel2Low && CurrentOutput.MarkingColoringLevel1Low ? true : false;
            int clrPos = 0;
            if (CurrentOutput.Level1Percent == CurrentOutput.Level2Percent)
            {
                if (CurrentOutput.MarkingColoringLevel2High)
                {
                    showLevel1High = false;
                }
                if (CurrentOutput.MarkingColoringLevel2Low)
                {
                    showLevel1Low = false;
                }
            }
            if (CurrentOutput.MarkingColoringLevel2High)
            {
                string Text;
                if (!isGlobalMode)
                {
                    Text = string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
                                 , (" " + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                          , ((CurrentOutput.Level2Percent.ToString().Length > 1 ? " +" : "  +") + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                }
                x = 133; y = 106; cx = 61; cy = 16;
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level2HighColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
                clrPos++;
            }
            else if (showLevel2High)
            {
                string Text;
                if (!isGlobalMode)
                {
                    Text = string.Format(Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION, (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                         , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? " +" : "  +") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                x = 133; y = 106; cx = 61; cy = 16;
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level2HighColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
                clrPos++;
            }
            if (CurrentOutput.MarkingColoringLevel1High && showLevel1High)
            {
                string Text;
                if (!isGlobalMode)
                {
                    Text = string.Format(Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION, (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                         , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? " +" : "  +") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                if (clrPos == 1)
                {
                    x = 133; y = 122; cx = 61; cy = 16;
                    clrX = 97; clrY = 121; clrCx = 33; clrCy = 17;
                }
                else
                {
                    x = 133; y = 106; cx = 61; cy = 16;
                    clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                }
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level1HighColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
                clrPos++;
            }
            if (CurrentOutput.MarkingColoringLevel1Low && showLevel1Low)
            {
                string Text;
                if (!isGlobalMode)
                {
                    Text = string.Format(
                      Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                       , (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                           , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? "  -" : "   -") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                if (clrPos == 1)
                {
                    x = 133; y = 122; cx = 61; cy = 16;
                    clrX = 97; clrY = 121; clrCx = 33; clrCy = 17;
                }
                else if (clrPos == 2)
                {
                    x = 133; y = 138; cx = 61; cy = 16;
                    clrX = 97; clrY = 137; clrCx = 33; clrCy = 16;
                }
                else
                {
                    x = 133; y = 106; cx = 61; cy = 16;
                    clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                }

                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level1LowColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
                clrPos++;
            }
            if (CurrentOutput.MarkingColoringLevel2Low)
            {
                string Text;
                if (!isGlobalMode)
                {
                    Text = string.Format(
                       Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                       , (" " + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                           , ((CurrentOutput.Level2Percent.ToString().Length > 1 ? "  -" : "   -") + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                }
                if (clrPos == 1)
                {
                    x = 133; y = 122; cx = 61; cy = 16;
                    clrX = 97; clrY = 121; clrCx = 33; clrCy = 17;
                }
                else if (clrPos == 2)
                {
                    x = 133; y = 138; cx = 61; cy = 16;
                    clrX = 97; clrY = 137; clrCx = 33; clrCy = 16;
                }
                else if (clrPos == 3)
                {
                    x = 133; y = 154; cx = 61; cy = 16;
                    clrX = 97; clrY = 153; clrCx = 33; clrCy = 16;
                }
                else
                {
                    x = 133; y = 106; cx = 61; cy = 16;
                    clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                }
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level2LowColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
            }
            else if (showlevel2Low)
            {
                string Text;
                if (!isGlobalMode)
                {
                    Text = string.Format(
                      Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                       , (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                           , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? "  -" : "   -") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                if (clrPos == 1)
                {
                    x = 133; y = 122; cx = 61; cy = 16;
                    clrX = 97; clrY = 121; clrCx = 33; clrCy = 17;
                }
                else if (clrPos == 2)
                {
                    x = 133; y = 138; cx = 61; cy = 16;
                    clrX = 97; clrY = 137; clrCx = 33; clrCy = 16;
                }
                else if (clrPos == 3)
                {
                    x = 133; y = 154; cx = 61; cy = 16;
                    clrX = 97; clrY = 153; clrCx = 33; clrCy = 16;
                }
                else
                {
                    x = 133; y = 106; cx = 61; cy = 16;
                    clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                }
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level2LowColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
            }
            Xdr.ClientData clientData3 = new Xdr.ClientData();

            twoCellAnchor3.Append(fromMarker3);
            twoCellAnchor3.Append(toMarker3);
            twoCellAnchor3.Append(groupShape);
            twoCellAnchor3.Append(clientData3);

            drawingsPart.WorksheetDrawing.Append(twoCellAnchor3);
        }
        public Xdr.Shape CreateTextShapeRectangle(string value,long x,long y,long cx,long cy,string clrIdx = "000000")
        {

            Xdr.Shape shape4 = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.NonVisualShapeProperties nonVisualShapeProperties4 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties5 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)33U, Name = "Level2HighLabel" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList5 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension5 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement6 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{00000000-0008-0000-0000-000021000000}\" />");

            nonVisualDrawingPropertiesExtension5.Append(openXmlUnknownElement6);

            nonVisualDrawingPropertiesExtensionList5.Append(nonVisualDrawingPropertiesExtension5);

            nonVisualDrawingProperties5.Append(nonVisualDrawingPropertiesExtensionList5);

            Xdr.NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties4 = new Xdr.NonVisualShapeDrawingProperties() { TextBox = true };
            A.ShapeLocks shapeLocks4 = new A.ShapeLocks() { NoChangeArrowheads = true };

            nonVisualShapeDrawingProperties4.Append(shapeLocks4);

            nonVisualShapeProperties4.Append(nonVisualDrawingProperties5);
            nonVisualShapeProperties4.Append(nonVisualShapeDrawingProperties4);

            Xdr.ShapeProperties shapeProperties4 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D4 = new A.Transform2D();
            A.Offset offset5 = new A.Offset() { X = x, Y = y };
            A.Extents extents5 = new A.Extents() { Cx = cx, Cy = cy };

            transform2D4.Append(offset5);
            transform2D4.Append(extents5);

            A.PresetGeometry presetGeometry4 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList4 = new A.AdjustValueList();

            presetGeometry4.Append(adjustValueList4);
            shapeProperties4.Append(transform2D4);
            shapeProperties4.Append(presetGeometry4);
            if (value == null)
            {
                A.SolidFill solidFill1 = new A.SolidFill();
                A.RgbColorModelHex rgbColorModelHex35 = new A.RgbColorModelHex() { Val = clrIdx };

                solidFill1.Append(rgbColorModelHex35);

                A.Outline outline = new A.Outline() { Width = 19050 };

                A.SolidFill solidFill2 = new A.SolidFill();

                A.RgbColorModelHex rgbColorModelHex36 = new A.RgbColorModelHex() { Val = "FFFFFF", LegacySpreadsheetColorIndex = 9, MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "a14" } };

                solidFill2.Append(rgbColorModelHex36);
                A.Miter miter4 = new A.Miter() { Limit = 800000 };
                A.HeadEnd headEnd5 = new A.HeadEnd();
                A.TailEnd tailEnd5 = new A.TailEnd();

                outline.Append(solidFill2);
                outline.Append(miter4);
                outline.Append(headEnd5);
                outline.Append(tailEnd5);
                shapeProperties4.Append(solidFill1);
                shapeProperties4.Append(outline);
            }
            shape4.Append(nonVisualShapeProperties4);
            shape4.Append(shapeProperties4);
            if (value != null)
                shape4.Append(GenerateTextBody(value));
            return shape4;
        }      
        public Xdr.TextBody GenerateTextBody(string value)
        {

            Xdr.TextBody textBody = new Xdr.TextBody();

            A.BodyProperties bodyProperties4 = new A.BodyProperties() { Wrap = A.TextWrappingValues.None, LeftInset = 0, TopInset = 0, RightInset = 0, BottomInset = 0, Anchor = A.TextAnchoringTypeValues.Top, UpRight = true };
            A.ShapeAutoFit shapeAutoFit1 = new A.ShapeAutoFit();

            bodyProperties4.Append(shapeAutoFit1);
            A.ListStyle listStyle4 = new A.ListStyle();

            A.Paragraph paragraph4 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties4 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Left, RightToLeft = false };
            A.DefaultRunProperties defaultRunProperties4 = new A.DefaultRunProperties() { FontSize = 1000 };

            paragraphProperties4.Append(defaultRunProperties4);
        
            A.EndParagraphRunProperties endParagraphRunProperties3 = new A.EndParagraphRunProperties() { Language = "ja-JP", AlternativeLanguage = "en-US" };

            paragraph4.Append(paragraphProperties4);
            paragraph4.Append(SetValues(value));
            paragraph4.Append(endParagraphRunProperties3);

            textBody.Append(bodyProperties4);
            textBody.Append(listStyle4);
            textBody.Append(paragraph4);
            return textBody;
        }
        public A.ParagraphProperties SetParagraphProperty()
        {
            A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Left, RightToLeft = false };

            A.LineSpacing lineSpacing2 = new A.LineSpacing();
            A.SpacingPoints spacingPoints2 = new A.SpacingPoints() { Val = 1100 };

            lineSpacing2.Append(spacingPoints2);
            A.DefaultRunProperties defaultRunProperties3 = new A.DefaultRunProperties() { FontSize = 1000 };

            paragraphProperties3.Append(lineSpacing2);
            paragraphProperties3.Append(defaultRunProperties3);
            return paragraphProperties3;
        }
        public A.EndParagraphRunProperties SetEndingProperty()
        {
            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = "en-US", AlternativeLanguage = "ja-JP", FontSize = 900, Bold = false, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Baseline = 0 };

            A.SolidFill solidFill7 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill7.Append(rgbColorModelHex8);
            A.LatinFont latinFont3 = new A.LatinFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };
            A.EastAsianFont eastAsianFont3 = new A.EastAsianFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };

            endParagraphRunProperties2.Append(solidFill7);
            endParagraphRunProperties2.Append(latinFont3);
            endParagraphRunProperties2.Append(eastAsianFont3);
            return endParagraphRunProperties2;
        }
        public A.Run SetValues(string value)
        {
            bool isGlobalMode = QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? false : true;
            A.Run run1 = new A.Run();

            A.RunProperties runProperties1 = new A.RunProperties() { Language = "ja-JP", AlternativeLanguage = "en-US", FontSize = !isGlobalMode ? 900 : 800, Bold = false, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Baseline = 0 };

            A.SolidFill solidFill5 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill5.Append(rgbColorModelHex6);
            A.LatinFont latinFont1 = new A.LatinFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };
            A.EastAsianFont eastAsianFont1 = new A.EastAsianFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };

            runProperties1.Append(solidFill5);
            runProperties1.Append(latinFont1);
            runProperties1.Append(eastAsianFont1);
            A.Text text31 = new A.Text();
            text31.Text = value;

            run1.Append(runProperties1);
            run1.Append(text31);
            return run1;
        }
    }
}
