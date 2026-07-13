using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Macromill.QCWeb.ReportRequest;
using Qc4Launcher.Logic.Cross_Report;
using Qc4Launcher.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Tables;

namespace Qc4Launcher.Logic.DPCheckCross
{
    public class CheckCrossNPTable
    {
        int[] styleIndexArrayHeader = null;
        int[] styleIndexArrayDouble = null;
        int[] colIndex = null;
        int numHColumns = 0;
        int numLColumns = 0;
        int copyCol = 0;

        public void GenerateCrossPerTable(OutputCross CurrentOutput, ref WorksheetPart worksheetPart, ref int rowNum, int maxAxisCnt,
                                    bool CutNAColumn, bool CutIVColumn, int ItemSectorsCount, CrossTable table, bool HasNARow, bool HasIVRow,
                                    Hashtable CutRowsCol, string FormatRangeNamePrefix, bool CutMedian)
        {
            int i = 0, lstMergeLetter = 3, fRow,rowDiff = 0,j=0,mrgLmt=0;
            int mergeRowNum = rowNum +1, edgeleftCellIdx = 136, idx, startCell = 2, idxLmt = 0;
            ReportPerTable reportPerTable = new ReportPerTable();

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            MergeCells mergeCells = null;
            mergeCells = worksheetPart.Worksheet.Descendants<MergeCells>().FirstOrDefault();
            if (mergeCells == null)
                mergeCells = new MergeCells();

            if (FormatRangeNamePrefix == "SA_MA_NP")
            {
                GenerateSAMATable(ref i, ref lstMergeLetter, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, table);
                styleIndexArrayHeader = new int[] { 45, 45, 45, 14, 14, 16, 16, 15, 15, 13, 13, 31, 11, 11, 47, 47, 12, 12, 70, 60, 60, 35, 35, 36, 36, 37, 37, 54, 55, 55, 5, 5, 4, 4, 3, 3 };
                styleIndexArrayDouble = new int[] { 70, 60, 60, 35, 35, 36, 36, 37, 37, 54, 55, 55, 5, 5, 4, 4, 3, 3, 51, 60, 60, 35, 35, 36, 36, 37, 37, 52, 65, 65, 10, 10, 9, 9, 7, 7, 52, 63, 63, 28, 28, 8, 8, 6, 6, 52, 65, 65, 10, 10, 9, 9, 7, 7 };
                rowDiff = 1;
            }
            else if (FormatRangeNamePrefix == "SA_MA_NP_WT")
            {
                GenerateSAMAWTTable(ref i, ref lstMergeLetter, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, table);
                styleIndexArrayHeader = new int[] { 45, 45, 45, 14, 14, 16, 16, 15, 15, 71, 15, 13, 13, 31, 11, 11, 47, 47, 12, 12, 72, 12, 70, 60, 60, 35, 35, 36, 36, 37, 37, 73, 38, 54, 55, 55, 5, 5, 4, 4, 3, 3, 74, 3 };
                styleIndexArrayDouble = new int[] { 70, 60, 60, 35, 35, 36, 36, 37, 37, 73, 38, 54, 55, 55, 5, 5, 4, 4, 3, 3, 74, 3, 51, 60, 60, 35, 35, 36, 36, 37, 37, 73, 38, 52, 65, 65, 10, 10, 9, 9, 7, 7, 75, 7,
                                                    52, 63, 63, 28, 28, 8, 8, 6, 6, 76,17, 52, 65, 65, 10, 10, 9, 9, 7, 7, 75, 7 };
                rowDiff = 1;
            }
            else
            {
                GenerateNTable(ref i, ref lstMergeLetter, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, CutMedian, table);
                styleIndexArrayHeader = new int[] { 45, 45, 45, 14, 14, 16, 16, 16, 16, 16, 16, 16, 15, 48, 69, 69, 69, 27, 27, 26, 25, 25, 25, 25, 25, 25, 24, 83, 77, 78, 78, 79,79, 80, 125, 126, 127, 128, 129, 130, 82, 84 };
                styleIndexArrayDouble = new int[] { 77, 78, 78, 79, 79, 80, 125, 126, 127, 128, 129, 130, 82, 84, 51, 56, 57, 39, 39, 40, 131, 132, 133, 134, 135, 136, 41, 85, 52, 58, 59, 30, 30, 21, 137, 138, 139, 140, 141, 142, 20, 86 };
                rowDiff = 0;
            }

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, Height = 11.25D, CustomHeight = true };
            Cell cell21 = new Cell() { CellReference = "B" + rowNum, StyleIndex = 64 };
            row2.Append(cell21);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, Height = 24.95D, CustomHeight = true };
            Cell cell31 = new Cell() { CellReference = "B" + rowNum, StyleIndex = 144 };
            row3.Append(cell31);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:10" }, Height = 3D, CustomHeight = true };
            sheetData.Append(row4);
            rowNum++;

            fRow = rowNum;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:11" }, Height = 33.75D, CustomHeight = true };
            Cell cell51 = new Cell() { CellReference = "A" + rowNum, StyleIndex = 143};
            row5.Append(cell51);
            CreateRowWithCells(row5, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i + 1, numLColumns, startCell);
            sheetData.Append(row5);
            rowNum++;

            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:11" }, Height = 11.25D, CustomHeight = true };
            CreateRowWithCells(row6, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i + 1, numLColumns, startCell);
            sheetData.Append(row6);
            rowNum++; mrgLmt = rowNum;
            while (j <= rowDiff)
            {
                Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:11" },Height=11.25D,CustomHeight=true };
                CreateRowWithCells(row7, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i + 1, numLColumns, startCell);
                sheetData.Append(row7);
                rowNum++;j++;
            }
            MergeCell mergeCell8 = new MergeCell() { Reference = "B" + mrgLmt + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + (mrgLmt + rowDiff)};
            mergeCells.Append(mergeCell8);

            MergeCell mergeCell3 = new MergeCell() { Reference = "B" + mergeRowNum + ":R" + mergeRowNum };
            mergeCells.Append(mergeCell3);
          
           // rowNum++;
            string rng;
            int y = 2; idxLmt = i;

            SetColIndexLocation(i, ref colIndex, numHColumns);
            for (idx = 0; idx <= table.AxesGroups.Count - 1; idx++)
            {
                if (table.AxesGroups[idx].Count == 1 || table.AxesGroups[idx].Count == 2)
                {
                    y = y + 1;
                    rng = table.AxesGroups[idx].Count == 1 ? "_Double" : "_Triple";
                    if (idx > 0)
                        SetColIndexLocation(i, ref colIndex, numLColumns);

                    if (rng == "_Double")
                        Double(sheetData, ref rowNum, ref mergeCells, colIndex, i + 1, ItemSectorsCount, table, idx, numLColumns, ref styleIndexArrayDouble, ref y,
                                                   CutRowsCol, lstMergeLetter, startCell, idxLmt, HasNARow, rowDiff);
                }
            }
            int lCol = FormatRangeNamePrefix == "N" ? i + 1 : (ItemSectorsCount == 1) ? i + 1 : i + (ItemSectorsCount - 2) + 1;
            reportPerTable.DrawEdgeBottom(lCol, rowNum, sheetData, 123, 2);
            reportPerTable.DrawEdgeLeft(worksheetPart, sheetData, fRow, (rowNum - 1), lCol + 1, 124);
            if (mergeRowNum == 3)
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }
        public void Double(SheetData sheetData, ref int rowNum, ref MergeCells mergeCells, int[] colIndex, int lmt, int ItemSectorsCount, CrossTable table,
                                int idx, int colCnt, ref int[] styleIndexArray, ref int y, Hashtable CutRowsCol, int lstMergeLetter, int startCell, int idxLmt
                                ,bool HasNARow,int rowDiff)
        {
            int strMrg, j = 0, count, mrgLmt = 0; 
            int[] SectorsCount = new int[2];

            AxesInformation tmpAxes = table.AxesGroups[idx];
            SectorsCount[0] = tmpAxes[0].SectorsCount;
            y = y + SectorsCount[0];
            if (HasNARow)
            {
                y = y + 1;
                if (!(CutRowsCol.ContainsKey(y)))
                { //' 無回答出力
                    SectorsCount[0] = SectorsCount[0] + 1;
                }
            }
            if (!CutRowsCol.ContainsKey(y) && idx > 0)
            {
                mrgLmt = rowNum;
                while (j <= rowDiff)
                {
                    Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" },  Height = 11.25D, CustomHeight = false };
                    CreateRowWithCells(row, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                    sheetData.Append(row); rowNum++; j++;
                }

                MergeCell mergeCell1 = new MergeCell() { Reference = "B" + mrgLmt + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + (mrgLmt + rowDiff) };
                mergeCells.Append(mergeCell1);
            }
            else
            {
                j = 0;
                while (j < idxLmt)
                {
                    colIndex[j] += rowDiff == 1 ? colCnt *2 : colCnt;
                    j++;
                }
            }
            y = y + SectorsCount[0];
            strMrg = rowNum;

            if (SectorsCount[0] > 0)
            {
                j = 0; mrgLmt = rowNum;
                while (j <= rowDiff)
                {
                    Row row1 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, CustomFormat = true, Height = 11.25D, CustomHeight = true };
                    CreateRowWithCells(row1, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                    sheetData.Append(row1);
                    rowNum++; j++;
                }
                if (mrgLmt != mrgLmt + rowDiff)
                {
                    MergeCell mergeCell2 = new MergeCell() { Reference = "C" + mrgLmt + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + (mrgLmt + rowDiff) };
                    mergeCells.Append(mergeCell2);
                }

                for (int cnt = 1; cnt < SectorsCount[0]; cnt++)
                {
                    j = 0; count = 0; mrgLmt = rowNum;
                    while (j <= rowDiff)
                    {
                        Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:14" },  Height = 11.25D, CustomHeight = true };
                        CreateRowShiftDown(row3, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, startCell, count);
                        sheetData.Append(row3);
                        rowNum++; j++; count = colCnt;
                    }
                    if (mrgLmt != mrgLmt + rowDiff)
                    {
                        MergeCell mergeCell3 = new MergeCell() { Reference = "C" + mrgLmt + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + (mrgLmt + rowDiff) };
                        mergeCells.Append(mergeCell3);
                    }
                }

                //SetColIndexLocation(idxLmt, ref colIndex, numLColumns);
                MergeCell mergeCell4 = new MergeCell() { Reference = "B" + strMrg + ":" + "B" + (rowNum - 1) };
                mergeCells.Append(mergeCell4);
            }
        }
        private void CreateRowShiftDown(Row row, SheetData sheetData, ref int[] colIndex, int[] styleIndexArray, int rowNum, int ItemSectorsCount, int lmt, int startCell,int colCnt)
        {
            int cellRef = startCell, styleIndex = 0, endCell;
            while (startCell <= lmt)
            {
                if (startCell == copyCol && ItemSectorsCount > 2)
                {
                    endCell = startCell + (ItemSectorsCount - 2);
                    while (cellRef <= endCell)
                    {
                        Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex] + colCnt] };
                        row.Append(cell64); cellRef++;
                    }
                    styleIndex++; startCell++;
                }
                Cell cell63 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex] + colCnt] };
               
                row.Append(cell63);
                styleIndex++; startCell++; cellRef++;
            }
        }
        public void SetColIndexLocation(int i, ref int[] colIndex, int idx)
        {
            int j = 0;
            while (j < i)
            {
                colIndex[j] -= idx;
                j++;
            }
        }
        public void GenerateSAMATable(ref int i, ref int lstMergeLetter, OutputCross CurrentOutput, int ItemSectorsCount
                                            , bool CutNAColumn, bool CutIVColumn, CrossTable table)
        {
            colIndex = new int[10];
            numHColumns = 36;
            numLColumns = 9;
            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            colIndex[i] = 4;
            i++;
            if (ItemSectorsCount == 1)
            {
                colIndex[i] = 5;
                i++;
            }
            if (ItemSectorsCount > 1)
            {
                colIndex[i] = 5;
                i++;
                colIndex[i] = 6;
                i++;
                copyCol = i;
            }
            if (CurrentOutput.ShowNAAtItem && (CutNAColumn == false))
            {
                colIndex[i] = 7;
                i++;
            }
            if (CurrentOutput.ShowIVAtItem && (CutIVColumn == false))
            {
                colIndex[i] = 8;
                i++;
            }
        }
        public void GenerateSAMAWTTable(ref int i, ref int lstMergeLetter, OutputCross CurrentOutput, int ItemSectorsCount
                                         , bool CutNAColumn, bool CutIVColumn, CrossTable table)
        {
            colIndex = new int[13];
            numHColumns = 44;
            numLColumns = 11;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }

            colIndex[i] = 4;
            i++;
            if (ItemSectorsCount == 1)
            {
                colIndex[i] = 5;
                i++;

            }
            if (ItemSectorsCount > 1)
            {
                colIndex[i] = 5;
                i++;
                colIndex[i] = 6;
                i++; copyCol = i;
            }
            if (CurrentOutput.ShowNAAtItem && (CutNAColumn == false))
            {
                colIndex[i] = 7;
                i++;
            }
            if (CurrentOutput.ShowIVAtItem && (CutIVColumn == false))
            {
                colIndex[i] = 8;
                i++;
            }
            colIndex[i] = 9;
            i++;
            colIndex[i] = 10;
            i++;
        }
        public void GenerateNTable(ref int i, ref int lstMergeLetter, OutputCross output, int ItemSectorsCount
                                            , bool CutNAColumn, bool CutIVColumn,bool CutMedian, CrossTable table)
        {
            colIndex = new int[13];
            numHColumns = 42;
            numLColumns = 14;
            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            colIndex[i] = 4;
            i++;
            if (output.ParentRequest.ShowParameter)
            {
                colIndex[i] = 5;
                i++;
            }
            if (output.ParentRequest.ShowSummary)
            {
                colIndex[i] = 6;
                i++;
            }
            if (output.ParentRequest.ShowAverage)
            {
                colIndex[i] = 7;
                i++;
            }
            if (output.ParentRequest.ShowStdev)
            {
                colIndex[i] = 8;
                i++;
            }
            if (output.ParentRequest.ShowMinimum)
            {
                colIndex[i] = 9;
                i++;
            }
            if (output.ParentRequest.ShowMaximum)
            {
                colIndex[i] = 10;
                i++;
            }
            if (output.ParentRequest.ShowMedian && !CutMedian)
            {
                colIndex[i] = 11;
                i++;
            }
            if (output.ShowNAAtItem && (CutNAColumn == false))
            {
                colIndex[i] = 12;
                i++;
            }
            if (output.ShowIVAtItem && (CutIVColumn == false))
            {
                colIndex[i] = 13;
                i++;
            }
        }
        public void CreateRowWithCells(Row row, SheetData sheetData, ref int[] colIndex, int[] styleIndexArray, int rowNum, int ItemSectorsCount, int lmt, int colCnt, int startCell)
        {
            int cellRef = startCell, styleIndex = 0, endCell;
            while (startCell <= lmt)
            {
                if (startCell == copyCol && ItemSectorsCount > 2)
                {
                    endCell = startCell + (ItemSectorsCount - 2);
                    while (cellRef <= endCell)
                    {
                        Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                       
                        row.Append(cell64); cellRef++;
                    }
                    colIndex[styleIndex] += colCnt;
                    styleIndex++; startCell++;
                }
                Cell cell63 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]]};
                
                row.Append(cell63);
                colIndex[styleIndex] += colCnt; styleIndex++; startCell++; cellRef++;
            }
        }
    }
}
