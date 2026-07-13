using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Macromill.QCWeb.ReportRequest;
using Qc4Launcher.Summary.OpenXml;
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
    class ReportPerSigTable
    {
        int[] styleIndexArrayHeader = null;
        int[] styleIndexArrayDouble = null;
        int[] styleIndexArrayTriple = null;     
        int[] colIndex = null;
        int ColumCount = 0;
        int ColumIndex = 0;
        int copyCol = 0;
        int threeWayIdx = 0;
        int twoWayIdx = 0;

        ReportPerTable reportPerTable = new ReportPerTable();

        public void GenerateCrossPerSigTable(OutputCross CurrentOutput, ref WorksheetPart worksheetPart, ref int rowNum, int maxAxisCnt,
                                    bool CutNAColumn, bool CutIVColumn, int ItemSectorsCount, CrossTable table, bool HasNARow, bool HasIVRow,
                                    Hashtable CutRowsCol, string FormatRangeNamePrefix, bool CutMedian)
        {
            int i = 0, lstMergeLetter = 5, fRow, lmt = 0,j=0;
            int mergeRowNum = rowNum + 2, edgeleftCellIdx = 182, idx, startCell = 2, idxLmt = 0;
            bool threeWay = false,isN = false;          
            SummaryNPFormat summaryNPFormat = new SummaryNPFormat();

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
            if (FormatRangeNamePrefix == "SA_MA" || FormatRangeNamePrefix == "SA_MA_NP")
            {
               
                GenerateSAMATable(ref i, ref lstMergeLetter, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, table, threeWay);

                if (FormatRangeNamePrefix == "SA_MA")
                {
                    styleIndexArrayHeader = new int[] { 95, 75, 130, 130, 130, 131, 17, 18, 16, 16, 69, 95, 75, 75, 75, 157, 158, 159, 159, 160, 160, 161, 95, 4, 162, 163, 163, 164, 165, 165, 166, 166, 166 };
                    styleIndexArrayDouble = new int[] { 95, 4, 162, 163, 163, 164, 165, 165, 166, 166, 166,95,4,132,124,124,135,56,56,67,67,67,95,4,133,118,118,120,48,48,47,47,46,95,4,133,117,117,119,28,28,33,33,33,
                                                        95,4,133,118,118,120,48,48,47,47,46};
                    lmt = 0; ColumIndex = 33;
                }
                else
                {
                    styleIndexArrayHeader = new int[] { 95, 75, 130, 130, 130, 131, 17, 18, 16, 16, 69, 95, 75, 75, 75, 157, 158, 159, 159, 160, 160, 161, 95, 4, 208, 209, 163, 210, 212, 212, 213, 226, 226, 95, 4, 215, 216,163, 217, 218, 219, 220, 220, 221 };
                    styleIndexArrayDouble = new int[] { 95, 4, 208, 209, 163, 210, 212, 212, 213, 226, 226, 95, 4, 215, 216,163, 217, 218, 219, 220, 220, 221,95,4,132,124,124,135,56,56,67,67,64,95,4,133,118,118,120,48,48,47,47,46,95,4,133,117,117,119,28,28,33,33,32,
                                                        95,4,133,118,118,120,48,48,47,47,46};
                    lmt = 1; ColumIndex = 44;
                }
               
                styleIndexArrayTriple = new int[]  { 95, 4, 162, 163, 163, 164, 165, 165, 166, 166, 166,95,4,132,124,57,58,59,59,66,66,66,95,4,133,136,117,119,28,28,33,33,33,95,4,133,136,118,120,48,48,47,47,46,
                                                     95,4,133,136,117,119,28,28,33,33,33,95,4,133,136,118,120,48,48,47,47,46,95,4,133,136,117,119,28,28,33,33,33,95,4,133,137,121,122,45,45,44,44,43 };
            }
            else if (FormatRangeNamePrefix == "SA_MA_WT" || FormatRangeNamePrefix == "SA_MA_WT_NP")
            {
                GenerateSAMAWTTable(ref i, ref lstMergeLetter, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, table, threeWay);
                if (FormatRangeNamePrefix == "SA_MA_WT")
                {
                    styleIndexArrayHeader = new int[] { 95, 75, 130, 130, 130, 131, 17, 18, 16, 16, 141, 149, 69, 95, 75, 75, 75, 157, 158, 159, 159, 160, 160, 168, 169, 161, 95, 4, 162, 163, 163, 170, 165, 165, 166, 166, 171, 172, 167 };
                    styleIndexArrayDouble = new int[]  { 95, 4, 162, 163, 163, 170, 165, 165, 166, 166, 171, 172, 167,95,4,132,124,124,135,56,56,67,67,148,156,64,95,4,133,118,118,120,48,48,47,47,145,153,35,95,4,133,117,117,119,28,28,33,33,144,152,32,
                                                         95,4,133,118,118,120,48,48,47,47,145,153,35};
                    lmt = 0; ColumIndex = 39; 
                }
                else
                {
                    styleIndexArrayHeader = new int[] { 95, 75, 130, 130, 130, 131, 17, 18, 16, 16, 141, 149, 69, 95, 75, 75, 75, 157, 158, 159, 159, 160, 160, 168, 169, 161, 95, 4, 208, 209, 163, 210, 211, 212, 213, 226, 214, 223, 225, 95, 4, 215, 216, 163, 217, 218, 219, 220, 220, 221, 222, 224 };
                    styleIndexArrayDouble = new int[]  { 95, 4, 208, 209, 163, 210, 211, 212, 213, 226, 214, 223, 225, 95, 4, 215, 216, 163, 217, 218, 219, 220, 220, 221, 222, 224,95,4,132,124,124,135,56,56,67,67,148,156,64,95,4,133,118,118,120,48,48,47,47,145,153,35,95,4,133,117,117,119,28,28,33,33,144,152,32,
                                                         95,4,133,118,118,120,48,48,47,47,145,153,35};
                    lmt = 1; ColumIndex = 52; 
                }
              
                styleIndexArrayTriple = new int[]   {95, 4, 162, 163, 163, 170, 165, 165, 166, 166, 171, 172, 167,95,4,132,124,57,58,59,59,66,66,143,151,63,95,4,133,136,117,119,28,28,33,33,144,152,32,95,4,133,136,118,120,48,48,47,47,145,153,35,
                                                     95,4,133,136,117,119,28,28,33,33,144,152,32,95,4,133,136,118,120,48,48,47,47,145,153,35,95,4,133,136,117,119,28,28,33,33,144,152,32,
                                                     95,4,133,137,121,122,45,45,44,44,146,154,34};
            }
            else
            {
                GenerateNTable(ref i, ref lstMergeLetter, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, CutMedian, table,threeWay);
                styleIndexArrayHeader = new int[]{ 95,75,130,130,130,131,17,18,16,16,16,16,16,16,16,69,42,95,75,117,117,117,173,28,28,174,175,175,175,175,175,175,176,177,
                                                   95,4,162,163,163,164,165,165,178,184,185,186,187,188,189,180,181};
                styleIndexArrayDouble = new int[] {95,4,162,163,163,164,165,165,178,184,185,186,187,188,189,180,181,95, 4, 132, 139, 139, 65, 59, 59, 53, 190, 191, 192, 193, 194, 195, 55, 61,
                                                   95, 4, 133, 140, 140, 38, 12, 12, 11, 196, 197, 198, 199, 200, 201, 9, 35,96, 4, 134, 127, 127, 37, 8, 8, 7, 202, 203, 204, 205, 206, 207, 5, 34 };
                styleIndexArrayTriple = new int[] {95,4,162,163,163,164,165,165,178,184,185,186,187,188,189,180,181,95,4,132,124,57,58,59,59,53,190, 191, 192, 193, 194, 195,55,60,95,4,133,136,70,38,12,12,11,196, 197, 198, 199, 200, 201,9,35,
                                                   95,4,133,136,70,38,12,12,11,196, 197, 198, 199, 200, 201,9,35,95,4,133,137,72,37,8,8,7,202, 203, 204, 205, 206, 207,5,34};
                isN = true;
            }

            int[] indexArray = { 94, 75, 95, 2, 95, 129, 95, 129,95 };
            summaryNPFormat.CreateIndividualRows(sheetData, ref rowNum, indexArray);

            MergeCell mergeCell3 = new MergeCell() { Reference = "C" + mergeRowNum + ":R" + mergeRowNum };
            MergeCell mergeCell4 = new MergeCell() { Reference = "D" + (mergeRowNum + 1) + ":R" + (mergeRowNum + 1) };
            mergeCells.Append(mergeCell3);
            mergeCells.Append(mergeCell4);

            fRow = rowNum;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, CustomFormat = true, Height = 81D, CustomHeight = true };
            CreateRowWithCells(row6, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i, ColumCount,1);
            sheetData.Append(row6);
            MergeCell mergeCell6 = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
            mergeCells.Append(mergeCell6);

            rowNum++;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, CustomFormat = true, Height = 11.25D, CustomHeight = true };
            CreateRowWithCells(row7, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i, ColumCount,1);
            sheetData.Append(row7);

            rowNum++;

            while (j <= lmt)
            {
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:12" }, CustomFormat = true, Height = 11.25D, CustomHeight = true };
                CreateRowWithCells(row8, sheetData, ref colIndex, styleIndexArrayHeader, rowNum, ItemSectorsCount, i, ColumCount, 1);
                sheetData.Append(row8);j++; rowNum++;
            }

            MergeCell mergeCell7 = new MergeCell() { Reference = "C" + (rowNum - lmt -1) + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + (rowNum-1) };
            mergeCells.Append(mergeCell7);
          
            string rng = "";
            int y = 2; idxLmt = i;
            int startRow = fRow;
            int endrow = rowNum;
            bool isFirstTime = true;
            reportPerTable.SetColIndexLocation(i, ref colIndex, ColumIndex);
            bool isTriple = reportPerTable.IsDoubleTripleCombo(table);
            int sectorStartRow = 0;
            for (idx = 0; idx <= table.AxesGroups.Count - 1; idx++)
            {
                if (table.AxesGroups[idx].Count == 1 || table.AxesGroups[idx].Count == 2)
                {
                    y = y + 1;
                    rng = table.AxesGroups[idx].Count == 1 ? "_Double" : "_Triple";
                    if (idx > 0)
                        reportPerTable.SetColIndexLocation(i, ref colIndex, ColumCount);
                    sectorStartRow = y;
                    if (rng == "_Double")
                    {
                        Double(sheetData, ref rowNum, ref mergeCells, colIndex, i, ItemSectorsCount, table, idx, ColumCount, ref styleIndexArrayDouble, 1,
                                                      lstMergeLetter, ref y, CutRowsCol, idxLmt, isN, threeWay, lmt);
                    }
                    else
                    {
                       
                        Tripple(sheetData, ref rowNum, ref mergeCells, colIndex, i, ItemSectorsCount, table, idx, ColumCount, ref styleIndexArrayTriple, HasNARow, HasIVRow, ref y,
                            CutRowsCol, lstMergeLetter, 1, idxLmt, isN);
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
                        bool isTotal = false;
                        if (!CutRowsCol.ContainsKey(sectorStartRow) && idx > 0)
                        {
                            isTotal = true;
                        }
                        int tripleSectorLine = 1;
                        uint[] subtotalStyleIndex = new uint[] { 149, 227, 228, 232, 229, 230, 231 };
                        int subTotalCount = table.Question.SubTotalCnt;
                        int column = CurrentOutput.ShowPreWBTotal ? ItemSectorsCount - subTotalCount + (startCell + 6) : ItemSectorsCount - subTotalCount + (startCell + 5);
                        column = (isTriple) ? (column + 1) : column;
                        column = (CurrentOutput.ShowNAAtItem && (CutNAColumn == false)) ? column + 1 : column;
                        DrawVerticalLine(worksheetPart.Worksheet, subtotalStyleIndex, ItemSectorsCount, column, startRow, endrow, tripleSectorCount, tripleSectorLine, rng, isFirstTime, isTotal);
                        startRow = endrow;
                        isFirstTime = false;
                    }
                }
            }
            int lCol = FormatRangeNamePrefix == "N" ? i + 1 : (ItemSectorsCount == 1) ? i + 1 : i + (ItemSectorsCount - 2) + 1;
           
            reportPerTable.DrawEdgeBottom(lCol - 1, rowNum, sheetData, 183, 3);
            reportPerTable.DrawEdgeLeft(worksheetPart, sheetData, fRow, (rowNum - 1), lCol, edgeleftCellIdx);

            if (mergeRowNum == 3)
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }

        public void DrawVerticalLine(Worksheet workSheet, uint[] styleIndex, int ItemSectorsCount, int column, int startRow, int endrow, int sectorCount, int tripleSectorLine, string rng, bool firstTime, bool isTotalInBetween)
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
                if (sectorCount != 0)
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
            if (isTotalInBetween)
            {
                if (sectorCount != 0)
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
            if (sectorCount != 0)
            {
                if (rng != "_Double")
                {
                    row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                    if (row != null)
                    {
                        Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                        cell.StyleIndex = styleIndex[6];
                        startRow++;
                    }
                }
                row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                if (row != null)
                {
                    Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                    cell.StyleIndex = rng != "_Double" ? styleIndex[4] : styleIndex[3];
                    startRow++;
                }

                row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                if (row != null)
                {
                    Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                    cell.StyleIndex = styleIndex[5];
                    startRow++;
                }

                while (startRow < endrow)
                {
                    if (tripleSectorLine != sectorCount)
                    {
                        row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                        if (row != null)
                        {
                            Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                            cell.StyleIndex = styleIndex[4];
                            startRow++;
                        }

                        row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                        if (row != null)
                        {

                            Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                            cell.StyleIndex = styleIndex[5];
                            startRow++;
                        }
                    }
                    else
                    {
                        if (rng != "_Double")
                        {
                            row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                            if (row != null)
                            {
                                Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                                cell.StyleIndex = styleIndex[6];
                                startRow++;
                            }
                        }
                        else
                        {
                            row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                            if (row != null)
                            {
                                Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                                cell.StyleIndex = styleIndex[5];
                                startRow++;
                            }
                        }
                        tripleSectorLine = -1;
                    }
                    tripleSectorLine++;
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
                        cell.StyleIndex = 232;
                        startRow++;
                    }

                    row = OpenXmlHelper.GetRow(workSheet, (uint)startRow);
                    if (row != null)
                    {
                        Cell cell = OpenXmlHelper.GetCell(row, startRow, column);
                        cell.StyleIndex = 233;
                        startRow++;
                    }
                }
            }
        }

        public void GenerateSAMATable(ref int i, ref int lstMergeLetter, OutputCross CurrentOutput, int ItemSectorsCount
                                           , bool CutNAColumn, bool CutIVColumn, CrossTable table, bool threeWay)
        {
            ColumCount = 11;
            colIndex = new int[11];
            threeWayIdx = 55;
            twoWayIdx = 22;

            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }
            if (threeWay == true)
            {
                colIndex[i] = 4;
                i++;
                lstMergeLetter = 6;              
            }
            colIndex[i] = 5;
            i++;
            if (CurrentOutput.ShowPreWBTotal)
            {
                colIndex[i] = 6;
                i++;
            }
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
                i++; copyCol = i;
            }
            if (CurrentOutput.ShowNAAtItem && (CutNAColumn == false))
            {
                colIndex[i] = 10;
                i++;
            }          
        }
        public void GenerateSAMAWTTable(ref int i, ref int lstMergeLetter, OutputCross CurrentOutput, int ItemSectorsCount
                                           , bool CutNAColumn, bool CutIVColumn, CrossTable table, bool threeWay)
        {
            ColumCount = 13;
            twoWayIdx = 26;
            threeWayIdx = 65;
            colIndex = new int[13];

            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }
            if (threeWay == true)
            {
                colIndex[i] = 4;
                i++;
                lstMergeLetter = 6;             
            }
            colIndex[i] = 5;
            i++;
            if (CurrentOutput.ShowPreWBTotal)
            {
                colIndex[i] = 6;
                i++;
            }
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
                i++; copyCol = i;
            }
            if (CurrentOutput.ShowNAAtItem && (CutNAColumn == false))
            {
                colIndex[i] = 10;
                i++;
            }
            colIndex[i] = 11;
            i++;
            colIndex[i] = 12;
            i++;
        }
        public void GenerateNTable(ref int i, ref int lstMergeLetter, OutputCross output, int ItemSectorsCount
                                           , bool CutNAColumn, bool CutIVColumn, bool CutMedian, CrossTable table, bool threeWay)
        {
            ColumCount = 17;
            ColumIndex = 51;
            twoWayIdx = 17;
            threeWayIdx = 51;

            colIndex = new int[17];

            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }

            if (threeWay == true)
            {
                colIndex[i] = 4;
                i++;
                lstMergeLetter = 6;            
            }
            colIndex[i] = 5;
            i++;
            if (output.ShowPreWBTotal)
            {
                colIndex[i] = 6;
                i++;
            }
            colIndex[i] = 7;
            i++;
            if (output.ParentRequest.ShowParameter)
            {
                colIndex[i] = 8;
                i++;
            }
            if (output.ParentRequest.ShowSummary)
            {
                colIndex[i] = 9;
                i++;
            }
            if (output.ParentRequest.ShowAverage)
            {
                colIndex[i] = 10;
                i++;
            }
            if (output.ParentRequest.ShowStdev)
            {
                colIndex[i] = 11;
                i++;
            }
            if (output.ParentRequest.ShowMinimum)
            {
                colIndex[i] = 12;
                i++;
            }
            if (output.ParentRequest.ShowMaximum)
            {
                colIndex[i] = 13;
                i++;
            }
            if (output.ParentRequest.ShowMedian && !CutMedian)
            {
                colIndex[i] = 14;
                i++;
            }
            if (output.ShowNAAtItem && (CutNAColumn == false))
            {
                colIndex[i] = 15;
                i++;
            }
            if (!NPOICrossCreator.checkSimpleAggr(table))
            {
                colIndex[i] = 16;
                i++;
            }
        }
        public void Double(SheetData sheetData, ref int rowNum, ref MergeCells mergeCells, int[] colIndex, int lmt, int ItemSectorsCount, CrossTable table,
                              int idx, int colCnt, ref int[] styleIndexArray, uint rowStyleIdx, int lstMergeLetter, ref int y, Hashtable CutRowsCol, int idxLmt
                              ,bool isN,bool threeWay,int limt)
        {
            int strMrg1, strMrg2, j = 0,count,rowDiff;
            int[] SectorsCount = new int[2];
            if (isN) rowDiff = 0; else rowDiff = 1;
            if (table.AxesGroups[idx].Count == 1)
            {
                AxesInformation tmpAxes = table.AxesGroups[idx];
                SectorsCount[0] = tmpAxes[0].SectorsCount;
                if (!CutRowsCol.ContainsKey(y) && idx > 0)
                {
                    while (j <= limt)
                    {
                        Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, StyleIndex = rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
                        CreateRowWithCells(row, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, 1);
                        sheetData.Append(row); rowNum++;j++;
                    }
                    MergeCell mergeCell8 = new MergeCell() { Reference = "C" + (rowNum- limt - 1) + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + (rowNum-1) };
                    mergeCells.Append(mergeCell8);

                    if (limt == 1)
                        reportPerTable.SetColIndexLocation(lmt, ref colIndex, ColumCount);
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
                if (SectorsCount[0] > 0)
                {
                    while (j <= rowDiff)
                    {
                        Row row1 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, StyleIndex = rowStyleIdx, CustomFormat = true, Height = 11.25D, CustomHeight = true };
                        CreateRowWithCells(row1, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, 1);
                        sheetData.Append(row1);
                        rowNum++; j++;
                    }

                    if (threeWay)
                    {
                        MergeCell mergeCellD = new MergeCell() { Reference = "D" + strMrg1 + ":" + "E" + (strMrg1 + rowDiff) };
                        mergeCells.Append(mergeCellD);
                    }
                    else
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
                            CreateRowShiftDown(row3, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, 1, count);
                            sheetData.Append(row3);
                            rowNum++; j++; count = colCnt;
                        }

                        if (threeWay)
                        {
                            MergeCell mergeCellD1 = new MergeCell() { Reference = "D" + strMrg2 + ":" + "E" + (strMrg2 + rowDiff) };
                            mergeCells.Append(mergeCellD1);
                        }
                        else
                        {
                            MergeCell mergeCellD1 = new MergeCell() { Reference = "D" + strMrg2 + ":" + "D" + (strMrg2 + rowDiff) };
                            MergeCell mergeCellE1 = new MergeCell() { Reference = "E" + strMrg2 + ":" + "E" + (strMrg2 + rowDiff) };
                            mergeCells.Append(mergeCellD1);
                            mergeCells.Append(mergeCellE1);
                        }

                    }
                    reportPerTable.SetColIndexLocation(idxLmt, ref colIndex, twoWayIdx);
                    MergeCell mergeCellC = new MergeCell() { Reference = "C" + strMrg1 + ":" + "C" + (rowNum - 1) };
                    mergeCells.Append(mergeCellC);
                }
            }
        }
        private void Tripple(SheetData sheetData, ref int rowNum, ref MergeCells mergeCells, int[] colIndex, int lmt, int ItemSectorsCount, CrossTable table,
                              int idx, int colCnt, ref int[] styleIndexArray, bool HasNARow, bool HasIVRow, ref int y, Hashtable CutRowsCol, int lstMergeLetter, int startCell
                              , int idxLmt,bool isN)
        {
            int strMrgB, strMrgC, j = 0, n, count, rowDiff, cnt;
            int[] SectorsCount = new int[2];
            if (isN) rowDiff = 0; else rowDiff = 1;
            AxesInformation tmpAxes = table.AxesGroups[idx];
            SectorsCount[0] = tmpAxes[0].SectorsCount;
            n = tmpAxes[1].SectorsCount + (OpenXmlHelper.ToInt(HasNARow) & 1) + (OpenXmlHelper.ToInt(HasIVRow) & 1) + 1;
            SectorsCount[1] = n - 1;

            if (!CutRowsCol.ContainsKey(y) && idx > 0)
            {
                Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                CreateRowWithCells(row, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                sheetData.Append(row);
                MergeCell mergeCell = new MergeCell() { Reference = "C" + rowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(lstMergeLetter) + rowNum };
                mergeCells.Append(mergeCell);
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
                    j = 0;
                    while (j <= rowDiff)
                    {
                        Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                        CreateRowWithCells(row2, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                        sheetData.Append(row2);
                        rowNum++;j++;
                    }
                    MergeCell mergeCell1 = new MergeCell() { Reference = "E" + (rowNum - 1 - rowDiff) + ":" + "E" + (rowNum -1)};
                    mergeCells.Append(mergeCell1);
                    MergeCell mergeCell2 = new MergeCell() { Reference = "F" + (rowNum - 1 - rowDiff) + ":" + "F" + (rowNum -1)};
                    mergeCells.Append(mergeCell2);
                }
                else
                {
                    j = 0;
                    while (j <= rowDiff)
                    {
                        ResetColIndex(sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                        j++;
                    }
                }

                for (cnt = 2; cnt < SectorsCount[1]; cnt++)
                {
                    j = 0; count = 0;
                    while (j <= rowDiff)
                    {
                        Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                        CreateRowShiftDown(row3, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, startCell, count);
                        sheetData.Append(row3);
                        rowNum++; count = colCnt;j++;
                    }
                    MergeCell mergeCell3 = new MergeCell() { Reference = "E" + (rowNum - 1 - rowDiff) + ":" + "E" + (rowNum - 1) };
                    mergeCells.Append(mergeCell3);
                    MergeCell mergeCell4 = new MergeCell() { Reference = "F" + (rowNum - 1 - rowDiff) + ":" + "F" + (rowNum - 1) };
                    mergeCells.Append(mergeCell4);
                }

                j = 0;
                while (j <= rowDiff)
                {
                    Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:13" }, Height = 15D, CustomHeight = true };
                    CreateRowWithCells(row4, sheetData, ref colIndex, styleIndexArray, rowNum, ItemSectorsCount, lmt, colCnt, startCell);
                    sheetData.Append(row4);
                    rowNum++;j++;
                }
                MergeCell mergeCell5 = new MergeCell() { Reference = "E" + (rowNum - 1 - rowDiff) + ":" + "E" + (rowNum - 1) };
                mergeCells.Append(mergeCell5);
                MergeCell mergeCell6 = new MergeCell() { Reference = "F" + (rowNum - 1 - rowDiff) + ":" + "F" + (rowNum - 1) };
                mergeCells.Append(mergeCell6);
                reportPerTable.SetColIndexLocation(idxLmt, ref colIndex, threeWayIdx);

                MergeCell mergeCell7 = new MergeCell() { Reference = "D" + strMrgC + ":" + "D" + (rowNum - 1) };
                mergeCells.Append(mergeCell7);
            }
            MergeCell mergeCell8 = new MergeCell() { Reference = "C" + strMrgB + ":" + "C" + (rowNum - 1) };
            mergeCells.Append(mergeCell8);
        }
        public void CreateRowWithCells(Row row, SheetData sheetData, ref int[] colIndex, int[] styleIndexArray, int rowNum, int ItemSectorsCount, int lmt, int colCnt, int startCell)
        {
            int cellRef = startCell, styleIndex = 0, endCell;
            while (startCell <= lmt)
            {
                Cell cell63 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                row.Append(cell63);
                colIndex[styleIndex] += colCnt; styleIndex++; startCell++; cellRef++;
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
            }
        }
        public void ResetColIndex(SheetData sheetData, ref int[] colIndex, int[] styleIndexArray, int rowNum, int ItemSectorsCount, int lmt, int colCnt, int startCell)
        {
            int cellRef = startCell, styleIndex = 0, endCell;
            while (startCell <= lmt)
            {
                colIndex[styleIndex] += colCnt; styleIndex++; startCell++; cellRef++;
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
            }
        }
        public void CreateRowShiftDown(Row row, SheetData sheetData, ref int[] colIndex, int[] styleIndexArray, int rowNum, int ItemSectorsCount, int lmt, int startCell,int colCnt)
        {
            int cellRef = startCell, styleIndex = 0, endCell;
            while (startCell <= lmt)
            {
                Cell cell63 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex] + colCnt] };
                row.Append(cell63);
                styleIndex++; startCell++; cellRef++;
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
            }
            //int cellRef = startCell, styleIndex = 0, endCell;
            //while (startCell <= lmt)
            //{
            //    Cell cell63 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
            //    row.Append(cell63);
            //    styleIndex++; startCell++; cellRef++;
            //    if (startCell == copyCol && ItemSectorsCount > 2)
            //    {
            //        endCell = startCell + (ItemSectorsCount - 2);
            //        while (cellRef <= endCell)
            //        {
            //            Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(cellRef) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
            //            row.Append(cell64); cellRef++;
            //        }
            //        styleIndex++; startCell++;
            //    }
            //}
        }       
    }
}
