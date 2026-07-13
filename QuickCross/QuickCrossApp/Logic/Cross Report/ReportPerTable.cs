using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Macromill.QCWeb.ReportRequest;
using Qc4Launcher.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Tables;

namespace Qc4Launcher.Logic.Cross_Report
{
    public class ReportPerTable
    {
        int[] styleIndexArrayHeader = null;
        int[] styleIndexArrayDouble = null;
        int[] styleIndexArrayTriple = null;
        int[] colIndex = null;
        int numHColumns = 0;
        int numLColumns = 0;
        int copyCol = 0;
        int GraphColLmt = 2;
        bool PercentCol = false;
        int TableStartRow = 0;
        bool IsN = false;

        public void GenerateCrossPerTable(OutputCross CurrentOutput, ref WorksheetPart worksheetPart, ref int rowNum, int maxAxisCnt,
                                    bool CutNAColumn, bool CutIVColumn,ref int AvgCol,bool isSideGraph,int tableStartRow, int ItemSectorsCount, CrossTable table, bool HasNARow, bool HasIVRow,
                                    Hashtable CutRowsCol, string FormatRangeNamePrefix, bool CutMedian, ref int grphColLmt, ref int lstCol)
        {
            int i = 0, lstMergeLetter = 3, fRow, graphRowNum = 0;
            int mergeRowNum = rowNum + 2, edgeleftCellIdx = 136, idx, startCell = 2, idxLmt = 0;
            bool threeWay = false;
            TableStartRow = tableStartRow;
            bool simpleAggr = NPOICrossCreator.checkSimpleAggr(table);
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            MergeCells mergeCells = null;
            mergeCells = worksheetPart.Worksheet.Descendants<MergeCells>().FirstOrDefault();
            if (mergeCells == null)
                mergeCells = new MergeCells();

            for (idx = 0; idx <= table.AxesGroups.Count - 1; idx++)
            {
                if (table.AxesGroups[idx].Count == 2)
                {
                    threeWay = true;
                    break;
                }
            }
            if (FormatRangeNamePrefix == "SA_MA")
            {
                GenerateSAMATable(ref i, ref lstMergeLetter, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, table, threeWay);
                styleIndexArrayHeader = new int[] { 5, 5, 1, 4, 40, 32, 32, 31, 31, 10, 2, 64, 7, 40, 75, 75, 65, 65, 117, 118, 118, 79, 80, 63, 63, 63, 57 };
                styleIndexArrayDouble = new int[] { 117, 118, 118, 79, 80, 63, 63, 63, 57, 121, 119, 119, 81, 82, 62, 62, 62, 56, 122, 128, 128, 83, 84, 45, 45, 45, 42 };
                styleIndexArrayTriple = new int[] { 117, 118, 118, 79, 80, 63, 63, 63, 57, 121, 124, 74, 81, 82, 62, 62, 62, 56, 122, 125, 72, 83, 84, 45, 45, 45, 42, 122, 125, 72, 83, 84, 45, 45, 45, 42, 122, 126, 73, 85, 86, 39, 39, 38, 38 };
            }
            else if (FormatRangeNamePrefix == "SA_MA_WT")
            {
                GenerateSAMAWTTable(ref i, ref lstMergeLetter, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, table, threeWay);
                styleIndexArrayHeader = new int[] { 2, 2, 9, 7, 40, 32, 32, 31, 31, 32, 31, 10, 2, 64, 48, 66, 75, 75, 65, 65, 68, 65, 117, 118, 118, 79, 80, 63, 63, 63, 57, 88, 57 };
                styleIndexArrayDouble = new int[] { 117, 118, 118, 79, 80, 63, 63, 63, 57, 88, 57, 121, 119, 119, 81, 82, 62, 62, 62, 56, 89, 56, 122, 128, 128, 83, 84, 45, 45, 45, 42, 90, 42 };
                styleIndexArrayTriple = new int[] { 117,118,118,79,80,63,63,63,57,88,57,121,124,74,81,82,62,62,62,56,89,56,122,125,72,83,84,45,45,45,42,90,42,122,125,72,83,84,45,45,45,42,90,42,
                                                    122,126,73,85,86,39,39,38,38,91,38 };
            }
            else
            {
                GenerateNTable(ref i, ref lstMergeLetter, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, CutMedian, table, ref AvgCol, threeWay);
                styleIndexArrayHeader = new int[] { 10, 2, 9, 7, 40, 32, 32, 32, 32, 32, 32, 32, 31, 31, 127, 127, 127, 48, 66, 44, 67, 67, 67, 67, 67, 67, 43, 43, 117, 118, 118, 79, 80, 28, 137, 138, 139, 140, 141, 142, 26, 26 };
                styleIndexArrayDouble = new int[] { 117, 118, 118, 79, 80, 28, 137, 138, 139, 140, 141, 142, 26, 26, 121, 119, 119, 81, 82, 23, 143, 144, 145, 146, 147, 148, 19, 19, 122, 128, 128, 83, 84, 22, 149, 150, 151, 152, 153, 154, 18, 18 };
                styleIndexArrayTriple = new int[] { 117,118,118,79,80,28,137,138,139,140,141,142,26,26,121,124,74,81,82,23,143,144,145,146,147,148,19,19,122,125,72,83,84,22,149,150,151,152,153,154,18,18,
                                                122,125,72,83,84,22,149,150,151,152,153,154,18,18,122,126,73,85,86,16,155,156,157,158,159,160,14,14 };
                IsN = true;
            }
            GenerateTopRows(ref sheetData, ref rowNum, ref mergeCells, threeWay, FormatRangeNamePrefix, simpleAggr);
            grphColLmt = GraphColLmt; 
            fRow = rowNum;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:11" }, Height = 120D, CustomHeight = true };
            CreateRowWithCells(row6, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i + 1, numLColumns, startCell);
            sheetData.Append(row6);

            rowNum++; graphRowNum = rowNum;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:11" }, Height = 11.25D, CustomHeight = true };
            CreateRowWithCells(row7, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i + 1, numLColumns, startCell);
            sheetData.Append(row7);

            rowNum++;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:11" }, Height = 15D, CustomHeight = true };
            CreateRowWithCells(row8, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i + 1, numLColumns, startCell);
            sheetData.Append(row8);

            MergeCell mergeCell8 = new MergeCell() { Reference = "B" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
            mergeCells.Append(mergeCell8);
            rowNum++;
            string rng = "";
            int y = 2; idxLmt = i;
            int startRow = fRow;
            int endrow = rowNum;
            bool firstTime = true;
            SetColIndexLocation(i, ref colIndex, numHColumns);
            bool isTriple = IsDoubleTripleCombo(table);
            int sectorCount = 0;
            for (idx = 0; idx <= table.AxesGroups.Count - 1; idx++)
            {
                if (table.AxesGroups[idx].Count == 1 || table.AxesGroups[idx].Count == 2)
                {
                    y = y + 1;
                    rng = table.AxesGroups[idx].Count == 1 ? "_Double" : "_Triple";
                    if (idx > 0)
                        SetColIndexLocation(i, ref colIndex, numLColumns);
                    sectorCount = y;
                    if (rng == "_Double")

                        Double(sheetData, ref rowNum, ref mergeCells, colIndex, i + 1, ItemSectorsCount, table, idx, numLColumns, ref styleIndexArrayDouble, ref y,
                            CutRowsCol, lstMergeLetter, startCell, idxLmt);
                    else
                        Tripple(sheetData, ref rowNum, ref mergeCells, colIndex, i + 1, ItemSectorsCount, table, idx, numLColumns, ref styleIndexArrayTriple, HasNARow, HasIVRow, ref y,
                            CutRowsCol, lstMergeLetter, startCell, idxLmt);

                    bool isExtraTotal = false;
                    if (!CutRowsCol.ContainsKey(sectorCount) && idx > 0)
                    {
                        isExtraTotal = true;
                    }
                    if (table.Question.SubTotalCnt > 0)
                    {
                        endrow = rowNum;
                        AxesInformation tmpAxes = null;
                        int tripleSectorCount = 0;
                        if ((rng != "_Double"))
                        {
                            tmpAxes = table.AxesGroups[idx];
                            tripleSectorCount = tmpAxes[1].SectorsCount;
                        }
                        else
                        {
                            tripleSectorCount = table.AxesGroups[idx][0].SectorsCount;
                        }
                       
                        int tripleSectorLine = 1;
                        uint[] subtotalStyleIndex = new uint[] { 166, 167, 168, 169, 170, 171 };
                        int subTotalCount = table.Question.SubTotalCnt;
                        int column = CurrentOutput.ShowPreWBTotal ? ItemSectorsCount - subTotalCount + (startCell + 4) : ItemSectorsCount - subTotalCount + (startCell + 3);
                        column = (isTriple) ? (column + 1) : column;
                        column = (CurrentOutput.ShowNAAtItem && (CutNAColumn == false)) ? column + 1 : column;
                        DrawVerticalLine(worksheetPart.Worksheet, subtotalStyleIndex, ItemSectorsCount, column, startRow, endrow, tripleSectorCount, tripleSectorLine, firstTime, isExtraTotal);
                    }

                    if (FormatRangeNamePrefix == "SA_MA_WT")
                    {
                        endrow = rowNum;
                        AxesInformation tmpAxes = null;
                        int tripleSectorCount = 0;
                        if ((rng != "_Double"))
                        {
                            tmpAxes = table.AxesGroups[idx];
                            tripleSectorCount = tmpAxes[1].SectorsCount;
                        }
                        else
                        {
                            tripleSectorCount = table.AxesGroups[idx][0].SectorsCount;
                        }
                        int tripleSectorLine = 1;
                        uint[] subtotalStyleIndex = new uint[] { 166, 167, 172, 173, 174, 175 };
                        int column = CurrentOutput.ShowPreWBTotal ? ItemSectorsCount + (startCell + 4) : ItemSectorsCount + (startCell + 3);
                        column = (isTriple) ? (column + 1) : column;
                        column = (CurrentOutput.ShowNAAtItem && (CutNAColumn == false)) ? column + 1 : column;
                        DrawVerticalLine(worksheetPart.Worksheet, subtotalStyleIndex, ItemSectorsCount, column, startRow, endrow, tripleSectorCount, tripleSectorLine, firstTime, isExtraTotal);
                    }
                    startRow = rowNum;
                    firstTime = false;
                }
            }

            int lCol = FormatRangeNamePrefix == "N" ? i + 1 : (ItemSectorsCount == 1) ? i + 1 : i + (ItemSectorsCount - 2) + 1;

            DrawEdgeBottom(lCol, rowNum, sheetData,135,2);
            DrawEdgeLeft(worksheetPart, sheetData, fRow, (rowNum - 1), lCol +1, edgeleftCellIdx);

            if (isSideGraph) // side chart
            {

                startCell = lCol + 4;
                lstCol = startCell;

                GraphColLmt += startCell;

                Row row9 = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)graphRowNum);
                CreateRowWithCells(row9, sheetData, ref colIndex, styleIndexArrayHeader, graphRowNum, ItemSectorsCount, GraphColLmt, numLColumns, startCell);

                graphRowNum++; fRow = graphRowNum;
                Row row10 = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)graphRowNum);
                CreateRowWithCells(row10, sheetData, ref colIndex, styleIndexArrayHeader, graphRowNum, ItemSectorsCount, GraphColLmt, numLColumns, startCell);
                MergeCell mergeCell9 = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + graphRowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(startCell + lstMergeLetter - 2) + graphRowNum };
                mergeCells.Append(mergeCell9);
                CreateRowWithGraphCells(row10, sheetData, ref colIndex, 161, graphRowNum, ItemSectorsCount, GraphColLmt + 10, numLColumns, GraphColLmt + 1);
                graphRowNum++;

                y = 2;
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

                            GraphDouble(sheetData, ref graphRowNum, ref mergeCells, colIndex, GraphColLmt, ItemSectorsCount, table, idx, numLColumns, ref styleIndexArrayDouble, ref y,
                                CutRowsCol, lstMergeLetter, startCell, idxLmt, worksheetPart.Worksheet);
                        else
                            GraphTripple(sheetData, ref graphRowNum, ref mergeCells, colIndex, GraphColLmt, ItemSectorsCount, table, idx, numLColumns, ref styleIndexArrayTriple, HasNARow, HasIVRow, ref y,
                                CutRowsCol, lstMergeLetter, startCell, idxLmt, worksheetPart.Worksheet);

                    }
                }

                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)(TableStartRow + 2));
                Cell cell = OpenXmlHelper.GetCell(row, TableStartRow + 2, GraphColLmt);
                cell.StyleIndex = 164;

                if (!IsN)
                {
                    Cell cellPer = new Cell()
                    {
                        CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(GraphColLmt + 10) + (TableStartRow + 2),
                        StyleIndex = 163,
                        DataType = CellValues.String,
                    }; row.Append(cellPer);
                    cellPer = new Cell()
                    {
                        CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(GraphColLmt + 11) + (TableStartRow + 2),
                        StyleIndex = 163,
                        DataType = CellValues.String,
                        CellValue = new CellValue("(%)")
                    }; row.Append(cellPer);
                }
                DrawEdgeBtm(GraphColLmt + 10, graphRowNum, worksheetPart.Worksheet, startCell, 135);
                DrawEdgeLft(worksheetPart, sheetData, fRow, (rowNum - 1), GraphColLmt + 10, edgeleftCellIdx);
            }
            if (mergeRowNum == 4)
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }

        public void DrawVerticalLine(Worksheet workSheet, uint[] styleIndex, int ItemSectorsCount, int column, int startRow, int endrow, int tripleSectorCount, int tripleSectorLine, bool firstTime, bool isExtraTotal = false)
        {

            Row row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
            if (firstTime)
            {
                if (row != null)
                {
                    Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                    cell.StyleIndex = styleIndex[0];
                    startRow++;
                }
                row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                if (row != null)
                {
                    Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                    cell.StyleIndex = styleIndex[1];
                    startRow++;

                }
                row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                if (row != null)
                {
                    Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                    cell.StyleIndex = styleIndex[2];
                    startRow++;
                }
            }
            if (isExtraTotal)
            {
                row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                if (row != null)
                {
                    Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                    cell.StyleIndex = styleIndex[2];
                    startRow++;
                }
            }
            if (tripleSectorCount != 0)
            {
                row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                if (row != null)
                {
                    Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                    cell.StyleIndex = styleIndex[3];
                    startRow++;
                }
                while (startRow < endrow)
                {
                    row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                    if (row != null)
                    {
                        if (tripleSectorLine != tripleSectorCount)
                        {
                            Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                            cell.StyleIndex = styleIndex[4];
                        }
                        else
                        {
                            Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                            cell.StyleIndex = styleIndex[5];
                            tripleSectorLine = -1;
                        }
                        startRow++;
                        tripleSectorLine++;
                    }
                }
            }
            else
            {
                while (startRow < endrow)
                {
                    row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                    if (row != null)
                    {
                        Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                        cell.StyleIndex = styleIndex[2];
                        startRow++;
                    }
                }
            }
        }

        public void GenerateSAMATable(ref int i, ref int lstMergeLetter, OutputCross CurrentOutput, int ItemSectorsCount
                                            , bool CutNAColumn, bool CutIVColumn, CrossTable table,bool threeWay)
        {
            colIndex = new int[11];
            numHColumns = 27;
            numLColumns = 9;
            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }

            if (threeWay == true)
            {
                colIndex[i] = 2;
                i++;
                lstMergeLetter = 4;
                GraphColLmt++;
                PercentCol = true;
            }
            if (CurrentOutput.ShowPreWBTotal)
            {
                colIndex[i] = 3;
                i++;
                GraphColLmt++;
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
                                            , bool CutNAColumn, bool CutIVColumn, CrossTable table,bool threeWay)
        {
            colIndex = new int[13];
            numHColumns = 33;
            numLColumns = 11;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }

            if (threeWay == true)
            {
                colIndex[i] = 2;
                i++;
                lstMergeLetter = 4;
                GraphColLmt++;
                PercentCol = true;
            }
            if (CurrentOutput.ShowPreWBTotal)
            {
                colIndex[i] = 3;
                i++;
                GraphColLmt++;
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
                                            , bool CutNAColumn, bool CutIVColumn, bool CutMedian, CrossTable table,ref int AvgCol,bool threeWay)
        {
            colIndex = new int[13];
            numHColumns = 42;
            numLColumns = 14;
            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }

            if (threeWay == true)
            {
                colIndex[i] = 2;
                i++;
                lstMergeLetter = 4;
                GraphColLmt++;
                PercentCol = true;
                AvgCol++;
            }
            if (output.ShowPreWBTotal)
            {
                colIndex[i] = 3;
                i++;
                GraphColLmt++;
                AvgCol++;
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
        public void Double(SheetData sheetData, ref int rowNum, ref MergeCells mergeCells, int[] colIndex, int lmt, int ItemSectorsCount, CrossTable table,
                                int idx, int colCnt, ref int[] styleIndexArray, ref int y, Hashtable CutRowsCol, int lstMergeLetter, int startCell, int idxLmt)
        {
            int strMrg, j = 0;
            int[] SectorsCount = new int[2];

            AxesInformation tmpAxes = table.AxesGroups[idx];
            SectorsCount[0] = tmpAxes[0].SectorsCount;
            if (!CutRowsCol.ContainsKey(y) && idx > 0)
            {
                Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                CreateRowWithCells(row, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                sheetData.Append(row);
                MergeCell mergeCell8 = new MergeCell() { Reference = "B" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
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
            strMrg = rowNum;

            if (SectorsCount[0] > 0)
            {
                Row row1 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                CreateRowWithCells(row1, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                sheetData.Append(row1);
                MergeCell mergeCell = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
                mergeCells.Append(mergeCell);
                rowNum++;

                for (int cnt = 1; cnt < SectorsCount[0]; cnt++)
                {
                    Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                    CreateRowShiftDown(row2, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, startCell);
                    sheetData.Append(row2);
                    MergeCell mergeCell1 = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
                    mergeCells.Append(mergeCell1);

                    rowNum++;
                }
                SetColIndexLocation(idxLmt, ref colIndex, numLColumns);
                MergeCell mergeCell2 = new MergeCell() { Reference = "B" + strMrg + ":" + "B" + (rowNum - 1) };
                mergeCells.Append(mergeCell2);
            }
        }
        private void Tripple(SheetData sheetData, ref int rowNum, ref MergeCells mergeCells, int[] colIndex, int lmt, int ItemSectorsCount, CrossTable table,
                              int idx, int colCnt, ref int[] styleIndexArray, bool HasNARow, bool HasIVRow, ref int y, Hashtable CutRowsCol, int lstMergeLetter, int startCell
                              , int idxLmt)
        {
            int strMrgB, strMrgC, j = 0, n;
            int[] SectorsCount = new int[2];

            AxesInformation tmpAxes = table.AxesGroups[idx];
            SectorsCount[0] = tmpAxes[0].SectorsCount;
            n = tmpAxes[1].SectorsCount + (OpenXmlHelper.ToInt(HasNARow) & 1) + (OpenXmlHelper.ToInt(HasIVRow) & 1) + 1;
            SectorsCount[1] = n - 1;

            if (!CutRowsCol.ContainsKey(y) && idx > 0)
            {
                Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                CreateRowWithCells(row, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                sheetData.Append(row);
                MergeCell mergeCell8 = new MergeCell() { Reference = "B" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
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
            strMrgB = rowNum;
            y = y + SectorsCount[0] * n;
            for (int i = 0; i < SectorsCount[0]; i++)
            {
                strMrgC = rowNum;

                Row row1 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                CreateRowWithCells(row1, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                sheetData.Append(row1);
                rowNum++;

                if (SectorsCount[1] > 1)
                {
                    Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                    CreateRowWithCells(row2, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                    sheetData.Append(row2);
                    rowNum++;
                }
                else
                {
                    ResetColIndex(sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                }

                for (int cnt = 2; cnt < SectorsCount[1]; cnt++)
                {
                    Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                    CreateRowShiftDown(row3, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, startCell);
                    sheetData.Append(row3);
                    rowNum++;
                }

                Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                CreateRowWithCells(row4, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                sheetData.Append(row4);
                rowNum++;

                SetColIndexLocation(idxLmt, ref colIndex, numHColumns);

                MergeCell mergeCell = new MergeCell() { Reference = "C" + strMrgC + ":" + "C" + (rowNum - 1) };
                mergeCells.Append(mergeCell);
            }
            MergeCell mergeCell1 = new MergeCell() { Reference = "B" + strMrgB + ":" + "B" + (rowNum - 1) };
            mergeCells.Append(mergeCell1);
        }
        private void GraphDouble(SheetData sheetData, ref int rowNum, ref MergeCells mergeCells, int[] colIndex, int lmt, int ItemSectorsCount, CrossTable table,
                          int idx, int colCnt, ref int[] styleIndexArray, ref int y, Hashtable CutRowsCol, int lstMergeLetter, int startCell, int idxLmt
                           , Worksheet worksheet)
        {
            int strMrg, j = 0;
            int[] SectorsCount = new int[2];

            AxesInformation tmpAxes = table.AxesGroups[idx];
            SectorsCount[0] = tmpAxes[0].SectorsCount;
            if (!CutRowsCol.ContainsKey(y) && idx > 0)
            {
                Row row = OpenXmlHelper.GetRow(worksheet, (uint)rowNum);
                CreateRowWithCells(row, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, GraphColLmt, colCnt, startCell);
                MergeCell mergeCell8 = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(startCell + lstMergeLetter - 2) + rowNum };
                mergeCells.Append(mergeCell8);
                CreateRowWithGraphCells(row, sheetData, ref colIndex, 161, rowNum, ItemSectorsCount, GraphColLmt + 10, colCnt, GraphColLmt+1);
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
            strMrg = rowNum;

            if (SectorsCount[0] > 0)
            {
                Row row1 = OpenXmlHelper.GetRow(worksheet, (uint)rowNum);
                CreateRowWithCells(row1, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, GraphColLmt, colCnt, startCell);
                CreateRowWithGraphCells(row1, sheetData, ref colIndex, 162, rowNum, ItemSectorsCount, GraphColLmt + 10, colCnt, GraphColLmt + 1);
                MergeCell mergeCell = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell + 1) + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(startCell + lstMergeLetter - 2) + rowNum };
                mergeCells.Append(mergeCell);
                rowNum++;

                for (int cnt = 1; cnt < SectorsCount[0]; cnt++)
                {
                    Row row2 = OpenXmlHelper.GetRow(worksheet, (uint)rowNum);
                    CreateRowShiftDown(row2, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, GraphColLmt, startCell);

                    MergeCell mergeCell1 = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell + 1) + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(startCell + lstMergeLetter - 2) + rowNum };
                    mergeCells.Append(mergeCell1);
                    rowNum++;
                }
                SetColIndexLocation(idxLmt, ref colIndex, numLColumns);
                MergeCell mergeCell2 = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + strMrg + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + (rowNum - 1) };
                mergeCells.Append(mergeCell2);
            }
        }
        private void GraphTripple(SheetData sheetData, ref int rowNum, ref MergeCells mergeCells, int[] colIndex, int lmt, int ItemSectorsCount, CrossTable table,
                             int idx, int colCnt, ref int[] styleIndexArray, bool HasNARow, bool HasIVRow, ref int y, Hashtable CutRowsCol, int lstMergeLetter, int startCell
                             , int idxLmt, Worksheet worksheet)
        {
            int strMrgB, strMrgC, j = 0, n;
            int[] SectorsCount = new int[2];

            AxesInformation tmpAxes = table.AxesGroups[idx];
            SectorsCount[0] = tmpAxes[0].SectorsCount;
            n = tmpAxes[1].SectorsCount + (OpenXmlHelper.ToInt(HasNARow) & 1) + (OpenXmlHelper.ToInt(HasIVRow) & 1) + 1;
            SectorsCount[1] = n - 1;

            if (!CutRowsCol.ContainsKey(y) && idx > 0)
            {
                Row row = OpenXmlHelper.GetRow(worksheet, (uint)rowNum);
                CreateRowWithCells(row, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                CreateRowWithGraphCells(row, sheetData, ref colIndex, 161, rowNum, ItemSectorsCount, GraphColLmt + 10, colCnt, GraphColLmt + 1);
                MergeCell mergeCell8 = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(startCell + lstMergeLetter - 2) + rowNum };
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
            strMrgB = rowNum;
            y = y + SectorsCount[0] * n;
            for (int i = 0; i < SectorsCount[0]; i++)
            {
                strMrgC = rowNum;

                Row row1 = OpenXmlHelper.GetRow(worksheet, (uint)rowNum);
                CreateRowWithCells(row1, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                CreateRowWithGraphCells(row1, sheetData, ref colIndex, 162, rowNum, ItemSectorsCount, GraphColLmt + 10, colCnt, GraphColLmt + 1);
                rowNum++;
            
                if (SectorsCount[1] > 1)
                {
                    Row row2 = OpenXmlHelper.GetRow(worksheet, (uint)rowNum);
                    CreateRowWithCells(row2, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                    rowNum++;
                }
                else
                {
                    ResetColIndex(sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                }

                for (int cnt = 2; cnt < SectorsCount[1]; cnt++)
                {
                    Row row3 = OpenXmlHelper.GetRow(worksheet, (uint)rowNum);
                    CreateRowShiftDown(row3, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, startCell);
                    rowNum++;
                }

                Row row4 = OpenXmlHelper.GetRow(worksheet, (uint)rowNum);
                CreateRowWithCells(row4, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);

                rowNum++;

                SetColIndexLocation(idxLmt, ref colIndex, numHColumns);

                MergeCell mergeCell2 = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell + 1) + strMrgC + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(startCell + 1) + (rowNum - 1) };
                mergeCells.Append(mergeCell2);
            }
            MergeCell mergeCell3 = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + strMrgB + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + (rowNum - 1) };
            mergeCells.Append(mergeCell3);
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

        public bool IsDoubleTripleCombo(CrossTable table)
        {
            for (int idx = 0; idx <= table.AxesGroups.Count - 1; idx++)
            {
                if (table.AxesGroups[idx].Count == 2)
                    return true;
            }
            return false;
        }

        private void DrawEdgeBtm(int lstCol, int rowNum, Worksheet worksheet, int startCell,uint styleIdx)
        {
            Row row = OpenXmlHelper.GetRow(worksheet, (uint)rowNum);
            while (startCell <= lstCol)
            {
                Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = styleIdx };
                row.Append(cell64); startCell++;
            }
        }
        public void DrawEdgeLft(WorksheetPart worksheetPart, SheetData sheetData, int fRow, int lRow, int lCol, int styleIdx)
        {
            lCol += 1;
            for (int rowIdx = fRow; rowIdx <= lRow; rowIdx++)
            {
                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowIdx);
                Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(lCol) + rowIdx, StyleIndex = (uint)styleIdx };
                row.Append(cell);
            }
        }
        public void DrawEdgeBottom(int lstCol, int rowNum, SheetData sheetData, int styleIdx, int startCell)
        {
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, Height = 11.25D, CustomHeight = false };
            while (startCell <= lstCol)
            {
                Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIdx };
                row.Append(cell64); startCell++;
            }
            sheetData.Append(row);
        }
        public void DrawEdgeLeft(WorksheetPart worksheetPart, SheetData sheetData, int fRow, int lRow, int lCol, int styleIdx)
        {          
            for (int rowIdx = fRow; rowIdx <= lRow; rowIdx++)
            {
                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowIdx);//worksheetPart.Worksheet.Descendants<Row>().Where(p => p.RowIndex == rowIdx).FirstOrDefault();
                Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(lCol) + rowIdx, StyleIndex = (uint)styleIdx };
                row.Append(cell);
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
                Cell cell63 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                row.Append(cell63);
                colIndex[styleIndex] += colCnt; styleIndex++; startCell++; cellRef++;
            }
        }

        public void ResetColIndex(SheetData sheetData, ref int[] colIndex, int[] styleIndexArray, int rowNum, int ItemSectorsCount, int lmt, int colCnt, int startCell)
        {
            int cellRef = startCell, styleIndex = 0, endCell;
            while (startCell <= lmt)
            {
                if (startCell == copyCol && ItemSectorsCount > 2)
                {
                    endCell = startCell + (ItemSectorsCount - 2);
                    while (cellRef <= endCell)
                    {
                        cellRef++;
                    }
                    colIndex[styleIndex] += colCnt;
                    styleIndex++; startCell++;
                }
                colIndex[styleIndex] += colCnt; styleIndex++; startCell++; cellRef++;
            }
        }

        private void CreateRowShiftDown(Row row, SheetData sheetData, ref int[] colIndex, int[] styleIndexArray, int rowNum, int ItemSectorsCount, int lmt, int startCell)
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
                    styleIndex++; startCell++;
                }
                Cell cell63 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                row.Append(cell63);
                styleIndex++; startCell++; cellRef++;
            }
        }

        private void CreateRowWithGraphCells(Row row, SheetData sheetData, ref int[] colIndex, uint styleIndex, int rowNum, int ItemSectorsCount, int lmt, int colCnt, int startCell)
        {
            int cellRef = startCell;
            while (cellRef <= lmt)
            {
                Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = styleIndex };
                row.Append(cell64); cellRef++; 
            }
        }
        private void GenerateTopRows(ref SheetData sheetData, ref int rowNum, ref MergeCells mergeCells, bool threeWay, string formatRangeNamePrefix,bool isSimpleAggr)
        {
            int[] col = null;
            int sRow = 3, i = 0, rowHeight = 15, lmt = 11;
            bool hidden = false;

            if (IsN)
            {
                hidden = true; lmt = 26;
            }
            if (threeWay)
            {
                col = new int[] { 18, 18, 15, 15, 15 };
            }
            else
            {
                col = new int[] { 18, 18, 14, 14, 14 };
            }
            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:18" }, Height = 18.75D };
            Cell cell = new Cell() { CellReference = "B2", StyleIndex = (UInt32Value)134U };
            row2.Append(cell);
            CreateHeadingRow(ref sheetData, ref rowNum, 133, 3, 18, 18.75, row2);
            CreateHeadingRow(ref sheetData, ref rowNum, 131, 2, 18, 33.75);
            CreateHeadingRow(ref sheetData, ref rowNum, 131, 2, 18, 33.75);
            CreateHeadingRow(ref sheetData, ref rowNum, 165, 2, 2, 30);
            CreateHeadingRow(ref sheetData, ref rowNum, 49, 2, 2, 11.25);
            CreateHeadingRow(ref sheetData, ref rowNum, 49, 2, 2, 11.25);

            for (int stRow = 8; stRow <= lmt; stRow++, rowNum++)
            {
                Row row = CreateRow(ref rowNum, true, 11.25);
                sheetData.Append(row);
            }
            rowNum++;

            if (!IsN)
            {
                if (!isSimpleAggr)
                {
                    rowNum = 12;
                    Row row = CreateRow(ref rowNum, hidden, rowHeight);
                    sheetData.Append(row);
                    rowNum++;
                }
                else
                {
                    rowNum = 12;
                    Row row = CreateRow(ref rowNum, hidden, 11.25);
                    sheetData.Append(row);
                    rowNum++;
                }

                for (int stRow = 13; stRow <= TableStartRow; stRow++, rowNum++)
                {
                    if (stRow == 13)
                        rowHeight = 9;
                    Row row = CreateRow(ref rowNum, hidden, rowHeight);
                    sheetData.Append(row);
                }
            }

            while (sRow <= 7)
            {
                MergeCell mergeCell3 = new MergeCell() { Reference = "B" + sRow + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(col[i]) + sRow };
                mergeCells.Append(mergeCell3);
                i++; sRow++;
            }
        }
        private void CreateHeadingRow(ref SheetData sheetData, ref int rowNum, int styleIdx, int startCell, int endCell, double rowHeight, Row row = null)
        {
            if (row == null)
                row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:18" }, Height = rowHeight, CustomHeight = true };

            while (startCell <= endCell)
            {
                Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIdx };
                row.Append(cell64); startCell++;
            }
            sheetData.Append(row);
            rowNum++;
        }
        private Row CreateRow(ref int rowNum, bool hidden, double height)
        {
            return new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:18" }, Height = height, Hidden = hidden, CustomHeight = true };
        }
    }
}
