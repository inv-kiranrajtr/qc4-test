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
    class ReportPortraitPerSigTable
    {
        List<uint[]> headerStyleIndexList = null;
        List<List<uint>> requiredColumns = null;
        List<uint> TotalColumn_P = null;
        List<uint> FirstColumn_Q = null;
        List<uint> MidColumn_R = null;
        List<uint> LastColumn_S = null;
        List<double> rowHeight = null;
        bool IsN = false;

        #region GenerateCrossPerTable
        public void GenerateCrossPerSigTable(OutputCross CurrentOutput, ref WorksheetPart worksheetPart, ref int rowNum, int maxAxisCnt,
                                    bool CutNAColumn, bool CutIVColumn, ref int AvgCol, ref bool isSideGraph, ref int tableStartRow, int ItemSectorsCount, CrossTable table, bool HasNARow, bool HasIVRow,
                                    Hashtable CutRowsCol, string FormatRangeNamePrefix, bool CutMedian, ref int grphColLmt, ref int lstCol, bool isSideTable)
        {
            int idx;
            bool threeWay = false;
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
            isSideTable = false;
            isSideGraph = false;
            if (FormatRangeNamePrefix == "SA_MA" || FormatRangeNamePrefix == "SA_MA_NP")
            {
                GenerateSAMATable(ref headerStyleIndexList, ref requiredColumns, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, table, threeWay);
                bool simpleAggr = NPOICrossCreator.checkSimpleAggr(table) ? true : false;
                GenerateTopRows(ref sheetData, ref rowNum, ref mergeCells, threeWay, FormatRangeNamePrefix, simpleAggr);
                tableStartRow = rowNum;
                CreateHeader(worksheetPart, sheetData, headerStyleIndexList, rowNum, rowNum + headerStyleIndexList.Count(), ItemSectorsCount, 3, 5, true);
                if (!threeWay)
                    CreateDoubleWayTable(sheetData, table, requiredColumns, rowNum, rowNum + headerStyleIndexList.Count, CutRowsCol, 6,mergeCells);
                else
                {
                    //implement 3 way here
                }
                if (!(CurrentOutput.ShowNAAtItem && (CutNAColumn == false)))
                {
                    DrawEdgeBottom(ReportPortraitHelper.GraphHelper.graphLastColumn, rowNum + headerStyleIndexList.Count, sheetData, 157, 3);
                }
                int tableDataStartRow = CurrentOutput.ShowPreWBTotal ? 12 : 11;
                while (tableDataStartRow < rowNum + headerStyleIndexList.Count)
                {
                    MergeCell mergeCell = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(3) + tableDataStartRow + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(3) + (tableDataStartRow + 1) };
                    mergeCells.Append(mergeCell);
                    tableDataStartRow += 2;
                }
            }
            else if (FormatRangeNamePrefix == "SA_MA_WT_NP" || FormatRangeNamePrefix == "SA_MA_WT")
            {
                GenerateSAMA_WTTable(ref headerStyleIndexList, ref requiredColumns, CurrentOutput, ItemSectorsCount, CutNAColumn, CutIVColumn, table, threeWay);
                bool simpleAggr = NPOICrossCreator.checkSimpleAggr(table) ? true : false;
                GenerateTopRows(ref sheetData, ref rowNum, ref mergeCells, threeWay, FormatRangeNamePrefix, simpleAggr);
                tableStartRow = rowNum;
                CreateHeader(worksheetPart, sheetData, headerStyleIndexList, rowNum, rowNum + headerStyleIndexList.Count(), ItemSectorsCount, 3, 5, true);
                if (!threeWay)
                    CreateDoubleWayTable(sheetData, table, requiredColumns, rowNum, rowNum + headerStyleIndexList.Count, CutRowsCol, 6,mergeCells);
                else
                {
                    //implement 3 way here
                }
                int tableDataStartRow = CurrentOutput.ShowPreWBTotal ? 12 : 11;
                while (tableDataStartRow < rowNum + headerStyleIndexList.Count)
                {
                    MergeCell mergeCell = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(3) + tableDataStartRow + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(3) + (tableDataStartRow + 1) };
                    mergeCells.Append(mergeCell);
                    tableDataStartRow += 2;
                }
            }
            else
            {
                IsN = true;
                GenerateNTable(ref headerStyleIndexList, ref requiredColumns, CurrentOutput, false, true, CutMedian, table, threeWay);
                bool simpleAggr = NPOICrossCreator.checkSimpleAggr(table) ? true : false;
                GenerateTopRows(ref sheetData, ref rowNum, ref mergeCells, threeWay, FormatRangeNamePrefix, simpleAggr);
                tableStartRow = rowNum;
                CreateHeader(worksheetPart, sheetData, headerStyleIndexList, rowNum, rowNum + headerStyleIndexList.Count(), ItemSectorsCount, 3, 5, true);
                if (!threeWay)
                    CreateDoubleWayTable(sheetData, table, requiredColumns, rowNum, rowNum + headerStyleIndexList.Count, CutRowsCol, 6,mergeCells);
                else
                {
                    //implement 3 way here
                }
                isSideGraph = false;
                isSideTable = false;
                if (!(CurrentOutput.ShowNAAtItem && (CutNAColumn == false)) && NPOICrossCreator.checkSimpleAggr(table))
                {
                    DrawEdgeBottom(5, rowNum + headerStyleIndexList.Count, sheetData, 122, 3);
                }
            }

            Row row = sheetData.Elements<Row>().Where(r => r.RowIndex == 8).First();
            Cell cell = row.Elements<Cell>().Where(p => p.CellReference == (OpenXmlHelper.ColumnIndexToColumnLetter(3) + 8)).FirstOrDefault();
            cell.CellValue = new CellValue((string)table.Question.NarrowingCondition);
            cell.DataType = CellValues.String;
            rowNum = rowNum + headerStyleIndexList.Count;
            worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }

        public void DrawEdgeBottom(int lstCol, int rowNum, SheetData sheetData, int styleIdx, int startCell)
        {
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, Height = 11.25D, CustomHeight = true };
            while (startCell <= lstCol)
            {
                Cell cell64 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIdx };
                row.Append(cell64); startCell++;
            }
            sheetData.Append(row);
        }
        #endregion

        #region SAMA_Style
        private void GenerateSAMATable(ref List<uint[]> headerStyleIndexList, ref List<List<uint>> requiredColumns, OutputCross output, int itemSectorsCount, bool CutNARow, bool CutIVRow, CrossTable table, bool threeWay)
        {
            headerStyleIndexList = new List<uint[]>();
            requiredColumns = new List<List<uint>>();
            TotalColumn_P = new List<uint>();
            FirstColumn_Q = new List<uint>();
            MidColumn_R = new List<uint>();
            LastColumn_S = new List<uint>();
            rowHeight = new List<double>();

            headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_TopRow);
            TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_TopRow[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_TopRow[1]);
            MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_TopRow[2]);
            LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_TopRow[3]);

            if (!NPOICrossCreator.checkSimpleAggr(table))
            {
                headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_SecondRow);
                TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SecondRow[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SecondRow[1]);
                MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SecondRow[2]);
                LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SecondRow[3]);
                rowHeight.Add(33.8);
                rowHeight.Add(22.5);
                headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_Triple);
                TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_Triple[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_Triple[1]);
                MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_Triple[2]);
                LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_Triple[3]);
                rowHeight.Add(56.3);
            }
            else
            {
                rowHeight.Add(56.3);
            }

            headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_SigStyleIndex);
            TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SigTest[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SigTest[1]);
            MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SigTest[2]);
            LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SigTest[3]);
            rowHeight.Add(11.25);

            if (output.ShowPreWBTotal)
            {
                headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_ShowPrewbTotal);
                TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_ShowPrewbTotal[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_ShowPrewbTotal[1]);
                MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_ShowPrewbTotal[2]);
                LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_ShowPrewbTotal[3]);
                rowHeight.Add(11.25);
            }

            headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_Total);
            TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_Total[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_Total[1]);
            MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_Total[2]);
            LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_Total[3]);
            rowHeight.Add(11.25);

            //Generate Sector Count
            int numberOfSectorCount_OtherThanSubTotalCount = itemSectorsCount - table.Question.SubTotalCnt;
            for (int i = 1; i <= numberOfSectorCount_OtherThanSubTotalCount; i++)
            {
                if (i != numberOfSectorCount_OtherThanSubTotalCount)
                {
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_SectorRow2_1);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[3]);
                    rowHeight.Add(11.25);
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_SectorRow2_2);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[3]);
                    rowHeight.Add(11.25);
                }
                else
                {
                    if (output.ShowNAAtItem && (CutNARow == false))
                    {
                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_SectorRow2_1);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[3]);
                        rowHeight.Add(11.25);
                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_SectorRow2_2);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[3]);
                        rowHeight.Add(11.25);

                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_NoAnswer1_1);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[3]);
                        rowHeight.Add(11.25);
                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Average1_2);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[3]);
                        rowHeight.Add(11.25);
                    }
                    else
                    {

                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_NoAnswer1_1);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[3]);
                        rowHeight.Add(11.25);
                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Average1_2);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[3]);
                        rowHeight.Add(11.25);
                    }
                }
            }

            //Generate Subtotal count
            for (int i = 1; i <= table.Question.SubTotalCnt; i++)
            {
                if (i != table.Question.SubTotalCnt)
                {
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_SectorRow2_1);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_1[3]);
                    rowHeight.Add(11.25);
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_SectorRow2_2);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_SectorRow2_2[3]);
                    rowHeight.Add(11.25);
                }
                else
                {
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Header_NoAnswer1_1);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_Sig_CellStyle.Double_NoAnswer1_1[3]);
                    rowHeight.Add(11.25);
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Average1_2);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[3]);
                    rowHeight.Add(11.25);
                }
            }


            requiredColumns.Add(TotalColumn_P);
            requiredColumns.Add(FirstColumn_Q);
            requiredColumns.Add(MidColumn_R);
            requiredColumns.Add(LastColumn_S);
        }
        #endregion

        #region SAMA_WT_Style
        private void GenerateSAMA_WTTable(ref List<uint[]> headerStyleIndexList, ref List<List<uint>> requiredColumns, OutputCross output, int itemSectorsCount, bool CutNARow, bool CutIVRow, CrossTable table, bool threeWay)
        {
            headerStyleIndexList = new List<uint[]>();
            requiredColumns = new List<List<uint>>();
            TotalColumn_P = new List<uint>();
            FirstColumn_Q = new List<uint>();
            MidColumn_R = new List<uint>();
            LastColumn_S = new List<uint>();
            rowHeight = new List<double>();

            headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_TopRow);
            TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_TopRow[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_TopRow[1]);
            MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_TopRow[2]);
            LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_TopRow[3]);

            if (!NPOICrossCreator.checkSimpleAggr(table))
            {
                headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_SecondRow);
                TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SecondRow[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SecondRow[1]);
                MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SecondRow[2]);
                LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SecondRow[3]);
                rowHeight.Add(33.8);
                rowHeight.Add(22.5);
                headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Triple);
                TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Triple[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Triple[1]);
                MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Triple[2]);
                LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Triple[3]);
                rowHeight.Add(56.3);
            }
            else
            {
                rowHeight.Add(56.3);
            }

            headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_SigStyleIndex);
            TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SigTest[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SigTest[1]);
            MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SigTest[2]);
            LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SigTest[3]);
            rowHeight.Add(11.25);

            if (output.ShowPreWBTotal)
            {
                headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_ShowPrewbTotal);
                TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_ShowPrewbTotal[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_ShowPrewbTotal[1]);
                MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_ShowPrewbTotal[2]);
                LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_ShowPrewbTotal[3]);
                rowHeight.Add(11.25);
            }

            headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Total);
            TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Total[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Total[1]);
            MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Total[2]);
            LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Total[3]);
            rowHeight.Add(11.25);

            //Generate Sector Count
            int numberOfSectorCount_OtherThanSubTotalCount = itemSectorsCount - table.Question.SubTotalCnt;
            for (int i = 1; i <= numberOfSectorCount_OtherThanSubTotalCount; i++)
            {
                if (i != (numberOfSectorCount_OtherThanSubTotalCount))
                {
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_SectorRow2_1);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[3]);
                    rowHeight.Add(11.25);
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_SectorRow2_2);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[3]);
                    rowHeight.Add(11.25);
                }
                else
                {
                    if (output.ShowNAAtItem && (CutNARow == false))
                    {
                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_SectorRow2_1);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[3]);
                        rowHeight.Add(11.25);
                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_SectorRow2_2);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[3]);
                        rowHeight.Add(11.25);

                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_NoAnswer1_1);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[3]);
                        rowHeight.Add(11.25);
                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Average1_2);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[3]);
                        rowHeight.Add(11.25);
                    }
                    else
                    {
                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_NoAnswer1_1);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[3]);
                        rowHeight.Add(11.25);
                        headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Average1_2);
                        TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[0]);
                        FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[1]);
                        MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[2]);
                        LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[3]);
                        rowHeight.Add(11.25);
                    }
                }
            }

            //Generate SubtotalCount
            for (int i = 1; i <= table.Question.SubTotalCnt; i++)
            {
                if (i != table.Question.SubTotalCnt)
                {
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_SectorRow2_1);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_1[3]);
                    rowHeight.Add(11.25);
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_SectorRow2_2);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_SectorRow2_2[3]);
                    rowHeight.Add(11.25);
                }
                else
                {
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_NoAnswer1_1);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_1[3]);
                    rowHeight.Add(11.25);
                    headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Average1_2);
                    TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[0]);
                    FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[1]);
                    MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[2]);
                    LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_NoAnswer1_2[3]);
                    rowHeight.Add(11.25);
                }
            }

            headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Population1_1);
            TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Population1_1[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Population1_1[1]);
            MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Population1_1[2]);
            LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Population1_1[3]);
            rowHeight.Add(11.25);
            headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Population1_2);
            TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Population1_2[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Population1_2[1]);
            MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Population1_2[2]);
            LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Population1_2[3]);
            rowHeight.Add(11.25);
            headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Average1_1);
            TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_1[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_1[1]);
            MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_1[2]);
            LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_1[3]);
            rowHeight.Add(11.25);
            headerStyleIndexList.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Header_Average1_2);
            TotalColumn_P.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[1]);
            MidColumn_R.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[2]);
            LastColumn_S.Add(ReportPortraitHelper.SAMA_WT_Sig_CellStyle.Double_Average1_2[3]);
            rowHeight.Add(11.25);

            requiredColumns.Add(TotalColumn_P);
            requiredColumns.Add(FirstColumn_Q);
            requiredColumns.Add(MidColumn_R);
            requiredColumns.Add(LastColumn_S);
        }
        #endregion

        #region N_Style
        public void GenerateNTable(ref List<uint[]> headerStyleIndexList, ref List<List<uint>> requiredColumns, OutputCross output, bool CutNARow, bool CutIVRow, bool CutMedian, CrossTable table, bool threeWay)
        {
            headerStyleIndexList = new List<uint[]>();
            requiredColumns = new List<List<uint>>();
            TotalColumn_P = new List<uint>();
            FirstColumn_Q = new List<uint>();
            MidColumn_R = new List<uint>();
            LastColumn_S = new List<uint>();
            rowHeight = new List<double>();

            headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_TopRowStyleIndex);
            TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_TopRowStyleIndex[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_TopRowStyleIndex[1]);
            MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_TopRowStyleIndex[2]);
            LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_TopRowStyleIndex[3]);

            if (!NPOICrossCreator.checkSimpleAggr(table))
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_SecondRowStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_SecondRowStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_SecondRowStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_SecondRowStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_SecondRowStyleIndex[3]);
                rowHeight.Add(33.8);
                rowHeight.Add(22.5);
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_TripleStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_TripleStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_TripleStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_TripleStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_TripleStyleIndex[3]);
                rowHeight.Add(56.3);
            }
            else
            {
                rowHeight.Add(56.3);
            }

            headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_SigStyleIndex);
            TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_SigStyleIndex[0]);
            FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_SigStyleIndex[1]);
            MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_SigStyleIndex[2]);
            LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_SigStyleIndex[3]);
            rowHeight.Add(11.25);

            if (output.ShowPreWBTotal)
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_ShowPrewbTotalStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_ShowPrewbTotalStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_ShowPrewbTotalStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_ShowPrewbTotalStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_ShowPrewbTotalStyleIndex[3]);
                rowHeight.Add(11.25);
            }
            if (output.ParentRequest.ShowParameter)
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_TotalStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_TotalStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_TotalStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_TotalStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_TotalStyleIndex[3]);
                rowHeight.Add(11.25);
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_PopulationStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_PopulationStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_PopulationStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_PopulationStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_PopulationStyleIndex[3]);
                rowHeight.Add(11.25);
            }
            if (output.ParentRequest.ShowSummary)
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_SummaryStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_SummaryStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_SummaryStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_SummaryStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_SummaryStyleIndex[3]);
                rowHeight.Add(11.25);
            }
            if (output.ParentRequest.ShowAverage)
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_AverageStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_AverageStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_AverageStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_AverageStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_AverageStyleIndex[3]);
                rowHeight.Add(11.25);
            }
            if (output.ParentRequest.ShowStdev)
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_DeviationStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_DeviationStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_DeviationStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_DeviationStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_DeviationStyleIndex[3]);
                rowHeight.Add(11.25);
            }
            if (output.ParentRequest.ShowMinimum)
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_MinimumStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_MinimumStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_MinimumStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_MinimumStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_MinimumStyleIndex[3]);
                rowHeight.Add(11.25);
            }
            if (output.ParentRequest.ShowMaximum)
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_MaximumStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_MaximumStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_MaximumStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_MaximumStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_MaximumStyleIndex[3]);
                rowHeight.Add(11.25);
            }
            if (output.ParentRequest.ShowMedian && !CutMedian)
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_MedianStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_MedianStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_MedianStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_MedianStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_MedianStyleIndex[3]);
                rowHeight.Add(11.25);
            }
            if (output.ShowNAAtItem && (CutNARow == false))
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_NoAnswerStyleIndex);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_NoAnswerStyleIndex[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_NoAnswerStyleIndex[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_NoAnswerStyleIndex[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_NoAnswerStyleIndex[3]);
                rowHeight.Add(11.25);
            }

            if (!NPOICrossCreator.checkSimpleAggr(table))
            {
                headerStyleIndexList.Add(ReportPortraitHelper.NSig_CellStyle.Header_SigTest);
                TotalColumn_P.Add(ReportPortraitHelper.NSig_CellStyle.Double_SigTest[0]);
                FirstColumn_Q.Add(ReportPortraitHelper.NSig_CellStyle.Double_SigTest[1]);
                MidColumn_R.Add(ReportPortraitHelper.NSig_CellStyle.Double_SigTest[2]);
                LastColumn_S.Add(ReportPortraitHelper.NSig_CellStyle.Double_SigTest[3]);
                rowHeight.Add(11.25);
            }

            requiredColumns.Add(TotalColumn_P);
            requiredColumns.Add(FirstColumn_Q);
            requiredColumns.Add(MidColumn_R);
            requiredColumns.Add(LastColumn_S);
        }
        #endregion

        #region CreateBarClusterTable
        private void CreateBarClusterTable(WorksheetPart worksheetPart, SheetData sheetData, int startRowNum, int itemSectorCount)
        {
            int startCell = 5;
            int endCell = 12;
            int cellIndex = 0;

            Columns columns = worksheetPart.Worksheet.Elements<Columns>().FirstOrDefault();
            SetColumnWidth(columns, startCell, endCell + 2, ReportPortraitHelper.BarClusterGraph.Bar_Cluster_Column_Width);
            Row row = sheetData.Elements<Row>().Where(r => r.RowIndex == startRowNum).First();
            CreateCells(row, startCell, endCell, startRowNum, ReportPortraitHelper.BarClusterGraph.Bar_ShowPrewbTotal, cellIndex); startRowNum++;
            row = sheetData.Elements<Row>().Where(r => r.RowIndex == startRowNum).First();
            CreateCells(row, startCell, endCell, startRowNum, ReportPortraitHelper.BarClusterGraph.Bar_Total, cellIndex); startRowNum++;
            row = sheetData.Elements<Row>().Where(r => r.RowIndex == startRowNum).First();
            CreateCells(row, startCell, endCell, startRowNum, ReportPortraitHelper.BarClusterGraph.Bar_Graph_FirstRow, cellIndex); startRowNum++;
            itemSectorCount = startRowNum + itemSectorCount;
            while (startRowNum < itemSectorCount)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == startRowNum).First();
                CreateCells(row, startCell, endCell, startRowNum, ReportPortraitHelper.BarClusterGraph.Bar_Graph_MiddleRow, cellIndex); startRowNum++;
            }
            row = sheetData.Elements<Row>().Where(r => r.RowIndex == startRowNum).First();
            CreateCells(row, startCell, endCell, startRowNum, ReportPortraitHelper.BarClusterGraph.Bar_Graph_LastRow, cellIndex);
            ReportPortraitHelper.BarClusterGraph.Bar_Cluster_End_Row = startRowNum;
        }
        #endregion

        #region 2WayTable
        private void CreateDoubleWayTable(SheetData sheetData, CrossTable Table, List<List<uint>> styleIndexArray, int startRowNum, int endRowNum, Hashtable CutRowsCol, int startColumn, MergeCells mergeCells)
        {
            int[] SectorsCount = new int[2];
            int y = 1;
            int idx;

            for (idx = 0; idx <= Table.AxesGroups.Count - 1; idx++)
            {
                y = y + 1;
                AxesInformation tmpAxes = Table.AxesGroups[idx];
                SectorsCount[0] = tmpAxes[0].SectorsCount;

                if (Table.AxesGroups[idx][0].SectorsCount > 0)
                {
                    if (!CutRowsCol.ContainsKey(y))
                    {
                        CreateColumn(sheetData, TotalColumn_P, startRowNum, endRowNum, ref startColumn);
                    }
                }

                int columnBegin = startColumn;
                y = idx == 0 ? y + SectorsCount[0] + 1 : y + SectorsCount[0];
                if (tmpAxes.Count == 1)
                {
                    if (SectorsCount[0] > 1)
                    {
                        //If sector count greter than 3 then copying the required number of columns
                        CreateColumn(sheetData, FirstColumn_Q, startRowNum, endRowNum, ref startColumn);
                        CreateColumn(sheetData, MidColumn_R, startRowNum, endRowNum, ref startColumn, SectorsCount[0] - 2);
                        MergeCell mergeCell = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(columnBegin) + startRowNum + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(startColumn) + startRowNum };
                        mergeCells.Append(mergeCell);
                        CreateColumn(sheetData, LastColumn_S, startRowNum, endRowNum, ref startColumn);
                        while (columnBegin < startColumn)
                        {
                            mergeCell = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(columnBegin) + (startRowNum + 1) + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(columnBegin) + (startRowNum + 2) };
                            mergeCells.Append(mergeCell);
                            columnBegin++;
                        }
                    }
                    else if (SectorsCount[0] == 1)
                    {
                        CreateColumn(sheetData, LastColumn_S, startRowNum, endRowNum, ref startColumn);
                        MergeCell mergeCell = new MergeCell() { Reference = OpenXmlHelper.ColumnIndexToColumnLetter(columnBegin) + (startRowNum + 1) + ":" + OpenXmlHelper.ColumnIndexToColumnLetter(columnBegin) + (startRowNum + 2) };
                        mergeCells.Append(mergeCell);
                    }
                    else if (idx > 0)
                    {
                        CreateColumn(sheetData, TotalColumn_P, startRowNum, endRowNum, ref startColumn);
                    }
                    ReportPortraitHelper.GraphHelper.graphLastColumn = startColumn - 1;
                    //Merging heading regions
                }
            }
        }
        #endregion

        #region CreateHeader
        private void CreateHeader(WorksheetPart worksheetPart, SheetData sheetData, List<uint[]> headerStyleIndexList, int startRowNum, int endRowNum, int ItemSectorsCount, int startCell, int endCell, bool createRow, int startColumnWidth = 0)
        {
            int ListStyleIndex = 0;
            int rowIndex = 0;
            int startRowNum1 = startRowNum;
            int cellIndex = 0;
            int startCell1 = 0;
            Row row = null;
            Columns columns = worksheetPart.Worksheet.Elements<Columns>().FirstOrDefault();
            SetColumnWidth(columns, 3, 3, 40.75);
            if (startColumnWidth != 0)
                SetColumnWidth(columns, startCell, startCell, startColumnWidth);

            while (startRowNum1 < endRowNum)
            {
                cellIndex = 0;
                startCell1 = startCell;
                if (createRow)
                {
                    row = new Row() { RowIndex = (uint)startRowNum1, Spans = new ListValue<StringValue>() { InnerText = "2:18" }, Height = rowHeight[rowIndex], CustomHeight = true };
                    Cell cell = new Cell() { CellReference = "A" + startRowNum1, StyleIndex = (UInt32Value)84U };
                    row.Append(cell);
                }
                else
                    row = sheetData.Elements<Row>().Where(r => r.RowIndex == startRowNum1).First();
                rowIndex++;
                while (startCell1 <= endCell)
                {
                    Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell1) + startRowNum1, StyleIndex = headerStyleIndexList[ListStyleIndex][cellIndex] };
                    row.Append(cell);
                    cellIndex++;
                    startCell1++;
                }
                if (createRow)
                    sheetData.Append(row);
                startRowNum1++;
                ListStyleIndex++;
            }

            //cellIndex = 0;
            //while (startRowNum < endRowNum)
            //{
            //    ListStyleIndex = 0;
            //    startCell = 5;
            //    endCell = startCell + 4;
            //    Row row = sheetData.Elements<Row>().Where(r => r.RowIndex == startRowNum).First();
            //    while (startCell < endCell)
            //    {

            //        Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + startRowNum, StyleIndex = requiredColumns[ListStyleIndex][cellIndex] };
            //        row.Append(cell);
            //        ListStyleIndex++;
            //        startCell++;
            //    }
            //    cellIndex++;
            //    startRowNum++;
            //}
        }
        #endregion

        #region GenerateTopRows
        private void GenerateTopRows(ref SheetData sheetData, ref int rowNum, ref MergeCells mergeCells, bool threeWay, string formatRangeNamePrefix, bool simpleAggr = false)
        {
            Row row = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:21" }, Height = 11.25D, CustomHeight = true };
            Cell cell = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)83U };
            row.Append(cell);
            CreateHeadingRow(ref sheetData, ref rowNum, 158, 2, 17, 11.25D, row);
            row = new Row() { RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "1:21" }, Height = 11.25D, CustomHeight = true };
            cell = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)84U };
            row.Append(cell);
            CreateHeadingRow(ref sheetData, ref rowNum, 158, 2, 17, 11.25D, row);
            row = new Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:21" }, Height = 22.5D, CustomHeight = true };
            cell = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)84U };
            row.Append(cell);
            CreateHeadingRow(ref sheetData, ref rowNum, 158, 2, 17, 22.5, row);
            row = new Row() { RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "1:21" }, Height = 24.9D, CustomHeight = true };
            cell = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)84U };
            row.Append(cell);
            CreateHeadingRow(ref sheetData, ref rowNum, 158, 2, 17, 24.9,row);
            row = new Row() { RowIndex = (UInt32Value)5U, Spans = new ListValue<StringValue>() { InnerText = "1:21" }, Height = 3D, CustomHeight = true ,Hidden = false};
            cell = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)84U };
            row.Append(cell);
            CreateHeadingRow(ref sheetData, ref rowNum, 158, 2, 17, 24.9, row);

            if (simpleAggr)
            {
                row = new Row() { RowIndex = (UInt32Value)6U, Spans = new ListValue<StringValue>() { InnerText = "1:21" }, Height = 11.25D, CustomHeight = true, Hidden = false };
                cell = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)84U };
                row.Append(cell);
                CreateHeadingRow(ref sheetData, ref rowNum, 146, 2, 17, 24.9, row);
                row = new Row() { RowIndex = (UInt32Value)7U, Spans = new ListValue<StringValue>() { InnerText = "1:21" }, Height = 11.25D, CustomHeight = true, Hidden = false };
                cell = new Cell() { CellReference = "A" + rowNum, StyleIndex = (UInt32Value)84U };
                row.Append(cell);
                CreateHeadingRow(ref sheetData, ref rowNum, 146, 2, 17, 24.9, row);
            }

            MergeCell mergeCell = new MergeCell() { Reference = "C" + 3 + ":" + "R" + 3 };
            mergeCells.Append(mergeCell);
            mergeCell = new MergeCell() { Reference = "C" + 4 + ":" + "R" + 4 };
            mergeCells.Append(mergeCell);
        }
        #endregion

        #region CreateHeadingRow
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
        #endregion

        #region CreateRowHidden
        private Row CreateRow(ref int rowNum, bool hidden, double height)
        {
            return new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:18" }, Height = height, Hidden = hidden, CustomHeight = true };
        }
        #endregion

        #region CreateColumn
        private void CreateColumn(SheetData sheetData, List<uint> ColumnStyle, int startRowNum, int endRowNum, ref int columnPosition, int createColumnCount = 1)
        {
            int cellIndex = 0;
            int startRow = startRowNum;
            while (createColumnCount >= 1)
            {
                startRow = startRowNum;
                cellIndex = 0;
                while (startRow < endRowNum)
                {
                    Row row = sheetData.Elements<Row>().Where(r => r.RowIndex == startRow).First();
                    Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(columnPosition) + startRow, StyleIndex = ColumnStyle[cellIndex] };
                    row.Append(cell);
                    cellIndex++;
                    startRow++;
                }
                createColumnCount--;
                columnPosition++;
            }
        }
        #endregion

        private void SetColumnWidth(Columns columns, int startCell, int endCell, double width)
        {
            while (startCell <= endCell)
            {
                CrossReportHelper.SetColumnWidth(columns, startCell, width);
                startCell++;
            }
        }
        private void CreateCells(Row row, int startCell, int endCell, int startRowNum, uint[] cellStyles, int cellIndex)
        {
            while (startCell <= endCell)
            {
                Cell cell = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + startRowNum, StyleIndex = cellStyles[cellIndex] };
                row.Append(cell);
                startCell++;
                cellIndex++;
            }
        }

    }
}

