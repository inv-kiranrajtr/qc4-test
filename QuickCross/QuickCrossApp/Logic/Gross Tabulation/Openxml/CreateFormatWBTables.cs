using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Macromill.QCWeb.ReportRequest;
using Qc4Launcher.Util;
using static Macromill.QCWeb.Batch.Report.Outputs;

namespace Qc4Launcher.Logic.Gross_Tabulation.Openxml
{
    public class CreateFormatWBTables
    {
        //Standard
        public void CreateNperTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long withBlockSubT)
        {
            int[] styleIndexArray = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 259, 14, 15, 16, 260, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27 };
            SectorsCount = SectorsCount > 2 ? SectorsCount * d : SectorsCount;

            int styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);

            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;
            startCell = 3;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row5, startCell, 5, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row6, startCell, 5, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, 5, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2;
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row, startCell, 5, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row);

            int numRows = 1, styIndex = 0;
            while (SectorsCount > numRows)
            {
                rowNum++; styIndex = styleIndex;
                startCell = 2;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row8, startCell, 5, ref styIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row8);
                numRows++;
            }
            if(SectorsCount==1)
            {
                styIndex = styleIndex;
                styIndex += 4;
            }
            styleIndex = styIndex;
            //No AnswerRow
            if (!CutNA && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row9, startCell, 5, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row9);
            }
            //InvalidRow
            if (!CutIV && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row10, startCell, 5, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row10);
            }

            if (withBlockSubT > 0)
            {
                startCell = 2; int[] indexArray = { 313, 114, 116, 117 }; styleIndex = 0;
                int sRow = ((rowNum) - (int)withBlockSubT);
                while (startCell <= 5)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                    Cell cell = OpenXmlHelper.GetCell(r, sRow, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

            rowNum++;
            styleIndex = 312; startCell = 2;
            Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };

            while (startCell <= 5)
            {
                Cell cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                row11.Append(cell112);
                startCell++;
            }
            sheetData.Append(row11);
        
            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateNTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long withBlockSubT)
        {
            int[] styleIndexArray = { 3, 28, 29, 30, 31, 32, 33, 34, 261, 35, 36, 262, 37, 38, 39, 40, 41, 42, 43, 44 };
            SectorsCount = SectorsCount > 2 ? SectorsCount * d : SectorsCount;

            int styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);

            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;
            startCell = 3;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row5, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row6, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2;
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row);

            int numRows = 1, styIndex = 0;
            while (SectorsCount > numRows)
            {
                rowNum++; styIndex = styleIndex;
                startCell = 2;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row8, startCell, 4, ref styIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row8);
                numRows++;
            }
            if (SectorsCount == 1)
            {
                styIndex = styleIndex;
                styIndex += 3;
            }
            styleIndex = styIndex;
            //No AnswerRow
            if (!CutNA && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row9, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row9);
            }
            //InvalidRow
            if (!CutIV && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row10, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row10);
            }

            if (CutIV == true && CutNA == true && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                styleIndex = 312; startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };

                while (startCell <= 4)
                {
                    Cell cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                    row11.Append(cell112);
                    startCell++;
                }
                sheetData.Append(row11);
            }
            else
            {
                rowNum++;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                sheetData.Append(row11);
            }

            if (withBlockSubT > 0)
            {
                startCell = 2; int[] indexArray = { 313, 114, 41 }; styleIndex = 0;
                int sRow = ((rowNum - 1) - (int)withBlockSubT);
                while (startCell <= 4)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                    Cell cell = OpenXmlHelper.GetCell(r, sRow, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreatePTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long withBlockSubT)
        {
            int[] styleIndexArray = { 3, 28, 29, 30, 31, 32, 33, 34, 261, 35, 45, 262, 37, 46, 39, 40, 47, 42, 43, 48, 49, 50, 51, 1, 50, 51 };
            SectorsCount = SectorsCount > 2 ? SectorsCount * d : SectorsCount;

            int styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);

            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;
            startCell = 3;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row5, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row6, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2;
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row);

            int numRows = 1, styIndex = 0;
            while (SectorsCount > numRows)
            {
                rowNum++; styIndex = styleIndex;
                startCell = 2;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row8, startCell, 4, ref styIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row8);
                numRows++;
            }
            if (SectorsCount == 1)
            {
                styIndex = styleIndex;
                styIndex += 3;
            }
            styleIndex = styIndex;
            //No AnswerRow
            if (!CutNA && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row9, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row9);
            }
            //InvalidRow
            if (!CutIV && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                sheetData.Append(row10);
            }

            if (CutIV == true && CutNA == true && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                styleIndex = 312; startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };

                while (startCell <= 4)
                {
                    Cell cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                    row11.Append(cell112);
                    startCell++;
                }
                sheetData.Append(row11);
            }
            else
            {
                rowNum++;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                sheetData.Append(row11);
            }

            if (withBlockSubT > 0)
            {
                startCell = 2; int[] indexArray = { 313, 114, 47 }; styleIndex = 0;
                int sRow = ((rowNum - 1) - (int)withBlockSubT);
                while (startCell <= 4)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                    Cell cell = OpenXmlHelper.GetCell(r, sRow, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateMatrixNPTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long ChildQuestionsCount, long withBlockSubT)
        {
            int[] styleIndexArray =  {49,3,52,53,268,269,54,55,49,3,271,253,257,258,254,255,49,50,248,249,250,251,62,252,263,274,56,31,57,58,59,60,61,275,63,64,65,66,324,68,
                                       263,274,56,31,57,58,59,60,61,275,63,64,65,66,324,68};
            int[] colIndex = new int[8];
            int numColums = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0, sCell = 6;

            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }

            if (SectorsCount == 1)
            {
                colIndex[i] = 4;
                i++;
                colIndex[i] = 5;
                i++;
            }
            else if (SectorsCount > 1)
            {
                colIndex[i] = 4;
                i++;
                colIndex[i] = 5;
                i++; sCell = i;
            }
            if (!CutNA)
            {
                colIndex[i] = 6;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 7;
                i++;
            }

            MergeCells mergeCells = null;
            mergeCells = worksheetPart.Worksheet.Descendants<MergeCells>().FirstOrDefault();
            if (mergeCells == null)
                mergeCells = new MergeCells();

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row8);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row9, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row9);

            MergeCell mergeCell1 = new MergeCell() { Reference = "C" + (rowNum -1) + ":C" + rowNum };
            mergeCells.Append(mergeCell1);

            int rowCount = 1;
            while (ChildQuestionsCount > rowCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
                CreateCellsDown(row10, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 0);
                sheetData.Append(row10);

                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
                CreateCellsDown(row11, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
                sheetData.Append(row11);
                rowCount++;
                MergeCell mergeCell2 = new MergeCell() { Reference = "C" + (rowNum - 1) + ":C" + rowNum };
                mergeCells.Append(mergeCell2);
            }
            if (withBlockSubT > 0)
            {
                int stIndx = 0;
                int[] indexArray = { 397, 396, 395, 393, 394 };
                int startRow = rowNum - (2 + (int)(ChildQuestionsCount * 2));
                while (startRow <= rowNum)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)startRow);
                    startCell = numColums - (int)withBlockSubT;
                    while (startCell < numColums)
                    {
                        Cell cell = OpenXmlHelper.GetCell(r, startRow, startCell);
                        cell.StyleIndex = (uint)indexArray[stIndx];
                        startCell++;
                    }
                    startRow++;
                    if (stIndx > 3)
                        stIndx -= 2;
                    stIndx++;
                }
            }
            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            sheetData.Append(row12);

            MergeCell mergeCellHd = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
            mergeCells.Append(mergeCellHd);
            if (mergeRowNum == 3)
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }
        public void CreateMatrixNTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long ChildQuestionsCount, long withBlockSubT)
        {
            int[] styleIndexArray = { 49, 3, 69, 53, 268, 269, 54, 55, 49, 3, 256, 253, 257, 258, 254, 255, 49, 50, 70, 71, 72, 73, 74, 75, 264, 76, 77, 34, 26, 78, 79, 80, 264, 76, 77, 34, 26, 78, 79, 80 };
            int[] colIndex = new int[8];
            int numColums = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0, sCell=6;

            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }

            if (SectorsCount == 1)
            {
                colIndex[i] = 4;
                i++;
                colIndex[i] = 5;
                i++;
            }
            else if (SectorsCount > 1)
            {
                colIndex[i] = 4;
                i++;
                colIndex[i] = 5;
                i++; sCell = i;
            }
            if (!CutNA)
            {
                colIndex[i] = 6;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 7;
                i++;
            }
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCellsDown(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row8);

            int rowCount = 1;
            while (ChildQuestionsCount > rowCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row10, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
                sheetData.Append(row10);
                rowCount++;
            }
            if (withBlockSubT > 0)
            {
                startCell = numColums - (int)withBlockSubT;
                int stIndx = 0;
                int[] indexArray = { 397, 396, 395, 398 };
                int startRow = rowNum - (2 + (int)ChildQuestionsCount);
                while (startRow <= rowNum)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)startRow);
                    Cell cell = OpenXmlHelper.GetCell(r, startRow, startCell);
                    cell.StyleIndex = (uint)indexArray[stIndx];
                    startRow++;
                    if (stIndx > 2)
                        stIndx--;
                    stIndx++;
                }
            }
            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateMatrixPTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long ChildQuestionsCount, long withBlockSubT)
        {
            int[] styleIndexArray = { 49, 3, 53, 53, 268, 269, 54, 55, 49, 3, 253, 253, 257, 258, 254, 255, 49, 50, 71, 71, 72, 73, 74, 75, 265, 81, 34, 34, 82, 83, 84, 85, 265, 81, 34, 34, 82, 83, 84, 85 };
            int[] colIndex = new int[8];
            int numColums = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0, sCell =6;

            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }

            if (SectorsCount == 1)
            {
                colIndex[i] = 4;
                i++;
                colIndex[i] = 5;
                i++;
            }
            else if (SectorsCount > 1)
            {
                colIndex[i] = 4;
                i++;
                colIndex[i] = 5;
                i++; sCell = i;
            }
            if (!CutNA)
            {
                colIndex[i] = 6;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 7;
                i++;
            }
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);
            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCellsDown(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row8);

            int rowCount = 1;
            while (ChildQuestionsCount > rowCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row10, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
                sheetData.Append(row10);
                rowCount++;
            }
            if (withBlockSubT > 0)
            {
                startCell = numColums - (int)withBlockSubT;
                int stIndx = 0;
                int[] indexArray = { 397, 396, 395, 399 };
                int startRow = rowNum - (2 + (int)ChildQuestionsCount);
                while (startRow <= rowNum)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)startRow);
                    Cell cell = OpenXmlHelper.GetCell(r, startRow, startCell);
                    cell.StyleIndex = (uint)indexArray[stIndx];
                    startRow++;
                    if (stIndx > 2)
                        stIndx--;
                    stIndx++;
                }
            }
            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateNumericTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output,bool CutMedian)
        {
            int[] styleIndexArray = {0, 3, 86, 86, 87, 88, 88, 88, 88, 88, 88, 89, 90,264, 76, 34, 34, 91, 316, 317, 318, 319, 320, 321, 79, 80 };
            int[] colIndex = new int[13];
            int numColums = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 13;
            i++;
            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }

            if (output.ParentRequest.ShowParameter)
            {
                colIndex[i] = 4;
                i++;
            }
            if (output.ParentRequest.ShowSummary)
            {
                colIndex[i] = 5;
                i++;
            }
            if (output.ParentRequest.ShowAverage)
            {
                colIndex[i] = 6;
                i++;
            }
            if (output.ParentRequest.ShowStdev)
            {
                colIndex[i] = 7;
                i++;
            }
            if (output.ParentRequest.ShowMinimum)
            {
                colIndex[i] = 8;
                i++;
            }
            if (output.ParentRequest.ShowMaximum)
            {
                colIndex[i] = 9;
                i++;
            }
            if (output.ParentRequest.ShowMedian && !CutMedian)
            {
                colIndex[i] = 10;
                i++;
            }
            if (!CutNA)
            {
                colIndex[i] = 11;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 12;
                i++;
            }

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;
            startCell = 3; styleIndex = 1;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row5, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 13);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            while (startCell <= (i + 1))
            {
                Cell cell62 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                row6.Append(cell62);
                styleIndex++; startCell++;
            }
            sheetData.Append(row6);

            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateNumericMatrixTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, long ChildQuestionsCount,bool CutMedian)
        {
            int[] styleIndexArray = { 0, 3, 93, 94, 95, 96, 96, 96, 96, 96, 96, 54, 55, 0, 97, 98, 99, 72, 73, 73, 73, 73, 73, 73, 74, 75, 265, 81, 77, 34, 100, 316, 317, 318, 319, 320, 321, 102, 80 };
            int[] colIndex = new int[13];
            int numRows = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 26;
            i++;
            while (i < 4)
            {
                colIndex[i] = i;
                i++;
            }

            if (output.ParentRequest.ShowParameter)
            {
                colIndex[i] = 4;
                i++;
            }
            if (output.ParentRequest.ShowSummary)
            {
                colIndex[i] = 5;
                i++;
            }
            if (output.ParentRequest.ShowAverage)
            {
                colIndex[i] = 6;
                i++;
            }
            if (output.ParentRequest.ShowStdev)
            {
                colIndex[i] = 7;
                i++;
            }
            if (output.ParentRequest.ShowMinimum)
            {
                colIndex[i] = 8;
                i++;
            }
            if (output.ParentRequest.ShowMaximum)
            {
                colIndex[i] = 9;
                i++;
            }
            if (output.ParentRequest.ShowMedian && !CutMedian)
            {
                colIndex[i] = 10;
                i++;
            }
            if (!CutNA)
            {
                colIndex[i] = 11;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 12;
                i++;
            }

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;
            startCell = 3; styleIndex = 1;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row5, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray,ref colIndex,13);
            sheetData.Append(row5);

            rowNum++;
            startCell = 3; styleIndex = 1;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row6, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 13);
            sheetData.Append(row6);

            while (numRows < ChildQuestionsCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                while (startCell <= (i + 1))
                {
                    Cell cell72 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                    row7.Append(cell72);
                    styleIndex++; startCell++;
                }
                numRows++;
                sheetData.Append(row7);
            }

            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        //Weight
        public void CreateNperWTTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, bool CutWT, long SectorsCount, long d, int rowNum, bool HasWeightColumn, long withBlockSubT)
        {
            int[] styleIndexArray = { 0,3,103,4,104,6,7,105,8,106,10,11,107,12,108,259,14,109,15,110,260,17,111,18,112,113,114,115,116,117,
                                       118,119,120,121,122,123,14,124,125,126,127,114,115,278,279,};
            SectorsCount = SectorsCount > 2 ? SectorsCount * d : SectorsCount;
            int i = 0, remIdx = 2, eCell = 6;

            if (!HasWeightColumn)
            {
                while (i < 8)
                {
                    styleIndexArray = styleIndexArray.Where((val, idx) => idx != remIdx).ToArray();
                    remIdx += 4; i++;
                }
                eCell = 5;
            }

            int styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
            MergeCells mergeCells = null;

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);

            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            styleIndex = 1; startCell = 3;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row5, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row6, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2;
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row);

            rowNum++;
            startCell = 2;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

            int numRows = 1, styIndex = 0;
            while (SectorsCount > numRows)
            {
                rowNum++; styIndex = styleIndex;
                startCell = 2;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row8, startCell, eCell, ref styIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row8);
                numRows++;
            }
            styleIndex = styleIndex + eCell - 1;
            //No AnswerRow
            if (!CutNA && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row9, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row9);
            }
            else
                styleIndex = styleIndex + eCell - 1;

            //InvalidRow
            if (!CutIV && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row10, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row10);
            }
            else
                styleIndex = styleIndex + eCell - 1;

            if (!CutWT && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row11, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row11);

                rowNum++;
                startCell = 2;
                Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row12, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row12);

                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().FirstOrDefault();
                if (mergeCells == null)
                    mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "E" + rowNum + ":F" + rowNum };
                mergeCells.Append(mergeCell13);
            }

            if (CutIV == true && CutNA == true && CutWT == true && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                styleIndex = 312; startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };

                while (startCell <= eCell)
                {
                    Cell cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                    row11.Append(cell112);
                    startCell++;
                }
                sheetData.Append(row11);
            }
            else
            {
                rowNum++;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                sheetData.Append(row11);
            }

            if (withBlockSubT > 0)
            {
                int[] indexArray = { 313, 114, 389, 390, 117 };
                int[] indexArray2 = { 313, 114, 314, 116, 117 };
                if (HasWeightColumn)
                    indexArray = indexArray2;
                startCell = 2; styleIndex = 0;
                int sRow = ((rowNum - 1) - (int)withBlockSubT);
                if (!HasWeightColumn)
                    styleIndexArray = indexArray.Where((val, idx) => idx != 2).ToArray();

                while (startCell <= eCell)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                    Cell cell = OpenXmlHelper.GetCell(r, sRow, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

            //MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                if (mergeCells == null)
                    mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateNWTTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, bool CutWT, long SectorsCount, long d, int rowNum, bool HasWeightColumn, long withBlockSubT)
        {
            int[] styleIndexArray = {0,3,103,28,6,7,105,31,10,11,107,34,259,14,109,36,260,17,111,38,113,114,115,41,118,119,120,128,123,14
                                       ,124,129,127,114,115,130};
            SectorsCount = SectorsCount > 2 ? SectorsCount * d : SectorsCount;
            int i = 0, remIdx = 2, eCell = 5;

            if (!HasWeightColumn)
            {
                while (i < 8)
                {
                    styleIndexArray = styleIndexArray.Where((val, idx) => idx != remIdx).ToArray();
                    remIdx += 3; i++;
                }
                eCell = 4;
            }

            int styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);

            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            styleIndex = 1; startCell = 3;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row5, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row6, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2;
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row);

            rowNum++;
            startCell = 2;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

            int numRows = 1, styIndex = 0;
            while (SectorsCount > numRows)
            {
                rowNum++; styIndex = styleIndex;
                startCell = 2;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row8, startCell, eCell, ref styIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row8);
                numRows++;
            }
            styleIndex = styleIndex + eCell - 1;
            //No AnswerRow
            if (!CutNA && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row9, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row9);
            }
            else
                styleIndex = styleIndex + eCell - 1;

            //InvalidRow
            if (!CutIV && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row10, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row10);
            }
            else
                styleIndex = styleIndex + eCell - 1;

            if (!CutWT && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row11, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row11);

                rowNum++;
                startCell = 2;
                Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row12, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row12);
            }

            if (CutIV == true && CutNA == true && CutWT == true && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                styleIndex = 312; startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };

                while (startCell <= eCell)
                {
                    Cell cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                    row11.Append(cell112);
                    startCell++;
                }
                sheetData.Append(row11);
            }
            else
            {
                rowNum++;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                sheetData.Append(row11);
            }

            if (withBlockSubT > 0)
            {
                int[] indexArray = { 313, 114, 391, 41 };
                int[] indexArray2 = { 313, 114, 314, 41 };
                startCell = 2; styleIndex = 0;
                int sRow = ((rowNum - 1) - (int)withBlockSubT);
                if (HasWeightColumn)
                    indexArray = indexArray2;

                while (startCell <= eCell)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                    Cell cell = OpenXmlHelper.GetCell(r, sRow, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreatePWTTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, bool CutWT, long SectorsCount, long d, int rowNum, bool HasWeightColumn, long withBlockSubT)
        {
            int[] styleIndexArray = {0,3,103,28,6,7,105,31,10,11,107,34,259,14,109,45,260,17,111,46,113,114,115,47,118,119,120,131,123,14,124,129,
                                       127,114,115,130,};
            SectorsCount = SectorsCount > 2 ? SectorsCount * d : SectorsCount;
            int i = 0, remIdx = 2, eCell = 5;

            if (!HasWeightColumn)
            {
                while (i < 8)
                {
                    styleIndexArray = styleIndexArray.Where((val, idx) => idx != remIdx).ToArray();
                    remIdx += 3; i++;
                }
                eCell = 4;
            }

            int styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);

            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            styleIndex = 1; startCell = 3;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row5, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row6, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2;
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row);

            rowNum++;
            startCell = 2;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

            int numRows = 1, styIndex = 0;
            while (SectorsCount > numRows)
            {
                rowNum++; styIndex = styleIndex;
                startCell = 2;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row8, startCell, eCell, ref styIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row8);
                numRows++;
            }
            styleIndex = styleIndex + eCell - 1;
            //No AnswerRow
            if (!CutNA && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row9, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row9);
            }
            else
                styleIndex = styleIndex + eCell - 1;

            //InvalidRow
            if (!CutIV && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row10, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row10);
            }
            else
                styleIndex = styleIndex + eCell - 1;

            if (!CutWT && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row11, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row11);

                rowNum++;
                startCell = 2;
                Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row12, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row12);
            }

            if (CutIV == true && CutNA == true && CutWT == true && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                styleIndex = 312; startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };

                while (startCell <= eCell)
                {
                    Cell cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                    row11.Append(cell112);
                    startCell++;
                }
                sheetData.Append(row11);
            }
            else
            {
                rowNum++;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                sheetData.Append(row11);
            }

            if (withBlockSubT > 0)
            {
                int[] indexArray = { 313, 114, 392, 47 };
                int[] indexArray2 = { 313, 114, 314, 47 };
                if (HasWeightColumn)
                    indexArray = indexArray2;
                startCell = 2; styleIndex = 0;
                int sRow = ((rowNum - 1) - (int)withBlockSubT);

                if (!HasWeightColumn)
                    styleIndexArray = indexArray.Where((val, idx) => idx != 2).ToArray();

                while (startCell <= eCell)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                    Cell cell = OpenXmlHelper.GetCell(r, sRow, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateMatrixNPWTTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, bool CutWT, long SectorsCount, long d, int rowNum, long ChildQuestionsCount, bool HasWeightColumn, long withBlockSubT)
        {
            int[] styleIndexArray = {49,3,132,53,53,268,269,54,133,95,54,49,50,132,134,134,135,136,137,138,135,137,49,139,139,140,140,141,142,143,144,145,143,266,276,146,31,31,57,58,59,147,148,149
                                     ,150,277,151,64,64,65,66,324,152,153,67,266,276,146,31,31,57,58,59,147,148,149,150,277,151,64,64,65,66,324,152,153,67};
            int[] colIndex = new int[10];
            int numColums = 0, styleIndex = 0, startCell = 0, sCell = 6, mergeRowNum = rowNum + 1;
            int i = 0;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            if (HasWeightColumn)
            {
                colIndex[i] = 2;
                i++;
                sCell = 7;
            }
            colIndex[i] = 3;
            i++;
            colIndex[i] = 4;
            i++;
            if (SectorsCount > 0)
            {
                colIndex[i] = 5;
                i++;
                colIndex[i] = 6;
                i++; 
            }
            if (!CutNA)
            {
                colIndex[i] = 7;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 8;
                i++;
            }
            if (!CutWT)
            {
                colIndex[i] = 9;
                i++;
                colIndex[i] = 10;
                i++;
            }

            MergeCells mergeCells = null;
            mergeCells = worksheetPart.Worksheet.Descendants<MergeCells>().FirstOrDefault();
            if (mergeCells == null)
                mergeCells = new MergeCells();

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row8);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row9, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row9);

            MergeCell mergeCell1 = new MergeCell() { Reference = "C" + (rowNum - 1) + ":C" + rowNum };
            mergeCells.Append(mergeCell1);

            int rowCount = 1;
            while (ChildQuestionsCount > rowCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
                CreateCellsDown(row10, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 0);
                sheetData.Append(row10);

                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
                CreateCellsDown(row11, startCell, ref colIndex, styleIndex,ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
                sheetData.Append(row11);
                rowCount++;
                MergeCell mergeCell2 = new MergeCell() { Reference = "C" + (rowNum - 1) + ":C" + rowNum };
                mergeCells.Append(mergeCell2);
            }
            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            sheetData.Append(row12);

            if (withBlockSubT > 0)
            {
                int sCount = 11;
                styleIndex = 5;
                startCell = numColums - (int)withBlockSubT;
                int row = mergeRowNum + 2;
                int eRow = row + 2;
                MWTQuestionLeftLineDraw(worksheetPart, row, eRow, styleIndex, (int)d, (int)ChildQuestionsCount, startCell, styleIndexArray, sCount);
            }

            MergeCell mergeCellHd = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
            mergeCells.Append(mergeCellHd);
            if (mergeRowNum == 3)
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
        }
        public void CreateMatrixNWTTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, bool CutWT, long SectorsCount, long d, int rowNum, long ChildQuestionsCount, bool HasWeightColumn, long withBlockSubT)
        {
            int[] styleIndexArray =  { 49,3,132,53,53,268,269,54,94,95,55,49,50,132,71,71,72,73,74,99,72,75,49,139,139,154,154,155,156,157,154,158,159,264,76,160,34,34,26,78,79,161,162,163,
                                       264,76,160,34,34,26,78,79,161,162,163};
            int[] colIndex = new int[10];
            int numColums = 0, styleIndex = 0, startCell = 0, sCell = 6, mergeRowNum = rowNum + 1;
            int i = 0;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            if (HasWeightColumn)
            {
                colIndex[i] = 2;
                i++;
                sCell = 7;
            }
            colIndex[i] = 3;
            i++;
            colIndex[i] = 4;
            i++;
            if (SectorsCount > 0)
            {
                colIndex[i] = 5;
                i++;
                colIndex[i] = 6;
                i++;
            }
            if (!CutNA)
            {
                colIndex[i] = 7;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 8;
                i++;
            }
            if (!CutWT)
            {
                colIndex[i] = 9;
                i++;
                colIndex[i] = 10;
                i++;
            }
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row8);

            int rowCount = 1;
            while (ChildQuestionsCount > rowCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row10, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 0);
                sheetData.Append(row10);
                rowCount++;
            }
            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            if (withBlockSubT > 0)
            {
                int sCount = 11;
                styleIndex = 5;
                //ChildQuestionsCount -= d;
                startCell = numColums - (int)withBlockSubT;
                int row = mergeRowNum + 2;
                int eRow = row + 2;
                MWTQuestionLeftLineDraw(worksheetPart, row, eRow, styleIndex, (int)d, (int)ChildQuestionsCount, startCell, styleIndexArray, sCount);
            }

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateMatrixPWTTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, bool CutWT, long SectorsCount, long d, int rowNum, long ChildQuestionsCount, bool HasWeightColumn, long withBlockSubT)
        {
            int[] styleIndexArray = { 49,3,132,69,53,268,269,54,94,95,55,49,50,132,70,71,72,73,74,99,72,75,49,139,139,165,154,155,156,157,154,158,159,264,76,160,77,34,323,83,84,168,162,85
                                      ,264,76,160,77,34,323,83,84,168,162,85};
            int[] colIndex = new int[10];
            int numColums = 0, styleIndex = 0, startCell = 0, sCell = 6, mergeRowNum = rowNum + 1;
            int i = 0;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            if (HasWeightColumn)
            {
                colIndex[i] = 2;
                i++;
                sCell = 7;
            }
            colIndex[i] = 3;
            i++;
            colIndex[i] = 4;
            i++;
            if (SectorsCount > 0)
            {
                colIndex[i] = 5;
                i++;
                colIndex[i] = 6;
                i++;
            }
            if (!CutNA)
            {
                colIndex[i] = 7;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 8;
                i++;
            }
            if (!CutWT)
            {
                colIndex[i] = 9;
                i++;
                colIndex[i] = 10;
                i++;
            }
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row8);

            int rowCount = 1;
            while (ChildQuestionsCount > rowCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row10, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 0);
                sheetData.Append(row10);
                rowCount++;
            }
            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            if (withBlockSubT > 0)
            {
                int sCount = 11;
                styleIndex = 5;
                //ChildQuestionsCount -= d;
                startCell = numColums - (int)withBlockSubT;
                int row = mergeRowNum + 2;
                int eRow = row + 2;
                MWTQuestionLeftLineDraw(worksheetPart, row, eRow, styleIndex, (int)d, (int)ChildQuestionsCount, startCell, styleIndexArray, sCount);
            }

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateNumericWTTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, bool HasWeightColumn,bool CutMedian)
        {
            int[] styleIndexArray = { 0,3, 132, 86, 86, 169, 88, 88, 88, 88, 88, 88, 89, 90,264, 76, 160, 34, 34, 26, 316, 317, 318, 319, 320, 321, 79, 80 };
            int[] colIndex = new int[14];
            int numColums = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 14;
            i++;
            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            if (HasWeightColumn)
            {
                colIndex[i] = 2;
                i++;
            }
            colIndex[i] = 3;
            i++;
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
            if (!CutNA)
            {
                colIndex[i] = 12;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 13;
                i++;
            }

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;
            startCell = 3; styleIndex = 1;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row5, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 14);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
          
            while (startCell <= (i + 1))
            {
                Cell cell62 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                row6.Append(cell62);
                styleIndex++; startCell++;
            }
            sheetData.Append(row6);

            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateNumericMatrixWTTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, long ChildQuestionsCount, bool HasWeightColumn,bool CutMedian)
        {
            int[] styleIndexArray = { 0, 3, 132, 93, 94, 95, 96, 96, 96, 96, 96, 96, 54, 55, 0, 97, 132, 98, 99, 72, 73, 73, 73, 73, 73, 73, 74, 75, 264, 76, 160, 77, 34, 100, 316, 317, 318, 319, 320, 321, 102, 80 };
            int[] colIndex = new int[14];
            int numRows = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 28;
            i++;
            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            if (HasWeightColumn)
            {
                colIndex[i] = 2;
                i++;
            }
            colIndex[i] = 3;
            i++;
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
            if (!CutNA)
            {
                colIndex[i] = 12;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 13;
                i++;
            }

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;
            startCell = 3; styleIndex = 1;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row5, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 14);
            sheetData.Append(row5);

            rowNum++;
            startCell = 3; styleIndex = 1;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row6, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 14);
            sheetData.Append(row6);

            while (numRows < ChildQuestionsCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                while (startCell <= (i + 1))
                {
                    Cell cell72 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                    row7.Append(cell72);
                    styleIndex++; startCell++;
                }
                numRows++;
                sheetData.Append(row7);
            }

            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        //Significance
        public void CreatePSigTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long withBlockSubT)
        {
            int[] styleIndexArray = { 3,103,28,28,6,7,105,31,185,10,11,107,34,186,259,14,171,45,187,260,17,174,46,188,113,114,115,47,189,
                                        180,181,182,48,190};
            SectorsCount = SectorsCount > 2 ? SectorsCount * d : SectorsCount;

            int styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);
            rowNum++;//1

            styleIndex = 0; startCell = 3;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row5, startCell, 6, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row5);

            rowNum++;//2
            startCell = 2;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row6, startCell, 6, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2;
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row, startCell, 6, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row);

            rowNum++;//3
            startCell = 2;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, 6, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

            int numRows = 1, styIdx = 0;//4
            while (SectorsCount > numRows)
            {
                rowNum++;
                startCell = 2; styIdx = styleIndex;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row8, startCell, 6, ref styIdx, ref rowNum, styleIndexArray);
                sheetData.Append(row8);
                numRows++;
            }
            styleIndex = styleIndex + 5;

            //No AnswerRow
            if (!CutNA && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row9, startCell, 6, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row9);
            }
            else
                styleIndex = styleIndex + 5;

            //InvalidRow
            if (!CutIV && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row10, startCell, 6, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row10);
            }

            if (CutIV == true && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                styleIndex = 312; startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                while (startCell <= 6)
                {
                    Cell cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                    row11.Append(cell112);
                    startCell++;
                }
                sheetData.Append(row11);
            }
            else
            {
                rowNum++;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                sheetData.Append(row11);
            }

            if (withBlockSubT > 0)
            {
                startCell = 2; int[] indexArray = { 313, 114, 315, 47, 189 }; styleIndex = 0;
                int sRow = ((rowNum - 1) - (int)withBlockSubT);
                while (startCell <= 6)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                    Cell cell = OpenXmlHelper.GetCell(r, sRow, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateMatrixPSigTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long ChildQuestionsCount, bool HasLetterColumn, bool CutLetterRow, long withBlockSubT, bool hasWeightColumn)
        {
            int[] styleIndexArray =  {49,3,132,53,53,268,269,54,55,49,50,132,134,134,135,136,137,191,49,50,132,71,71,72,73,74,75,49,139,139,140,140,192,193,143,194,266,276,281,31,31,207,208,400,210
                                      ,145,277,283,204,204,211,212,213,214,266,276,281,31,31,207,208,400,210,145,277,283,204,204,211,212,213,214};
            int[] colIndex = new int[9];
            int numColums = 0, sCell = 5, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            if (!HasLetterColumn)
            {
                colIndex[i] = 2;
                i++;
                sCell = 6;
            }

            colIndex[i] = 3;
            i++;
            colIndex[i] = 4;
            i++;

            if (SectorsCount == 1)
            {
                colIndex[i] = 5;
                i++;
                colIndex[i] = 6;
                i++;
            }
            else if (SectorsCount > 1)
            {
                colIndex[i] = 5;
                i++;
                colIndex[i] = 6;
                i++; sCell = i;
            }
            if (!CutNA)
            {
                colIndex[i] = 7;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 8;
                i++;
            }

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
            MergeCells mergeCells = null;
            MergeCell mergeCell = null;

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;//1

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5,startCell,ref colIndex,styleIndex, ref numColums, sCell,i,SectorsCount,ref rowNum,styleIndexArray,9);
            sheetData.Append(row5);

            rowNum++;//2
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell,ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 9);
            sheetData.Append(row6);

            rowNum++;//3
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell,ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 9);
            sheetData.Append(row7);

            if (!CutLetterRow)
            {
                rowNum++;//4
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCells(row8, startCell,ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 9);
                sheetData.Append(row8);
            }
            else
            {
                startCell = 2; styleIndex = 0;
                while (startCell <= (i + 1))
                {
                    colIndex[styleIndex] += 9;
                    startCell++; styleIndex++;
                }
            }

            rowNum++;//5
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row9, startCell,ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 9);
            sheetData.Append(row9);

            rowNum++;//6
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row10, startCell,ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 9);
            sheetData.Append(row10);

            mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().FirstOrDefault();
            if (mergeCells == null)
            {
                mergeCells = new MergeCells();
            }

            String mergeVariable = !HasLetterColumn ? hasWeightColumn ? "E" : "D" : "";
            if (mergeVariable != "")
            {
                mergeCell = new MergeCell() { Reference = mergeVariable + (rowNum - 1) + ":" + mergeVariable + rowNum };
                mergeCells.Append(mergeCell);
            }
            mergeCell = new MergeCell() { Reference = "C" + (rowNum - 1) + ":" + "C" + rowNum };
            mergeCells.Append(mergeCell);

            int rowCount = 1;
            while (ChildQuestionsCount > rowCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row11, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 0);
                sheetData.Append(row11);

                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row12, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 9);
                sheetData.Append(row12);
                rowCount++;

                if (mergeVariable != "")
                {
                    mergeCell = new MergeCell() { Reference = mergeVariable + (rowNum - 1) + ":" + mergeVariable + rowNum };
                    mergeCells.Append(mergeCell);
                }
                mergeCell = new MergeCell() { Reference = "C" + (rowNum - 1) + ":" + "C" + rowNum };
                mergeCells.Append(mergeCell);
            }
            if (withBlockSubT > 0)
            {
                startCell = numColums - (int)withBlockSubT;
                int stIndx = 0; int headrRow = 4;
                int[] indexArray = { 268, 135, 155, 192, 207, 211 };
                int startRow = rowNum - (3 + (int)(ChildQuestionsCount * 2));
                if (CutLetterRow)
                {
                    indexArray = indexArray.Where((val, idx) => idx != 3).ToArray();
                    headrRow = 3;
                    startRow = rowNum - (2 + (int)(ChildQuestionsCount * 2));
                }
                while (startRow <= rowNum)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)startRow);
                    Cell cell = OpenXmlHelper.GetCell(r, startRow, startCell);
                    cell.StyleIndex = (uint)indexArray[stIndx];
                    startRow++;
                    if (stIndx > headrRow)
                        stIndx -= 2;
                    stIndx++;
                }
            }
            rowNum++;
            Row row13 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row13);

            //MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                if (mergeCells == null)
                    mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateNumericMatrixSigTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, long ChildQuestionsCount,bool CutMedian)
        {
            int[] styleIndexArray = { 0, 3, 132, 94, 94, 95, 96, 96, 96, 96, 96, 96, 54, 94, 55, 0, 97, 132, 99,99, 72, 73, 73, 73, 73, 73, 73, 74, 99, 75, 264, 76, 160, 34,34, 100, 316, 317, 318, 319, 320, 321, 102, 161, 222 };
            int[] colIndex = new int[16];
            int numRows = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 30;
            i++;
            while (i < 5)
            {
                colIndex[i] = i;
                i++;
            }

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
            if (!CutNA)
            {
                colIndex[i] = 12;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 13;
                i++;
            }
            colIndex[i] = 14;
            i++;

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;//1
            startCell = 3; styleIndex = 1;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row5, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 15);
            sheetData.Append(row5);

            rowNum++;//2
            startCell = 3; styleIndex = 1;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row6, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 15);
            sheetData.Append(row6);

            while (numRows < ChildQuestionsCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                while (startCell <= (i + 1))
                {
                    Cell cell72 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                    row7.Append(cell72);
                    styleIndex++; startCell++;
                }
                numRows++;
                sheetData.Append(row7);
            }

            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        //Hybrid
        public void CreatePHybridSigTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, bool CutWT, long SectorsCount, long d, int rowNum, bool HasWeightColumn, long withBlockSubT)
        {
            int[] styleIndexArray = {0,3,103,103,28,28,6,7,105,105,31,185,10,11,107,107,34,186,259,14,109,171,45,187,260,17,111,174,46,188
                                       ,113,114,115,115,47,189,118,119,120,120,131,231,123,14,124,124,129,233,127,114,115,115,236,189};
            if(!HasWeightColumn)
            {
                int[] styleIndexArray1 = {0,3,103,103,28,28,6,7,105,105,31,185,10,11,107,107,34,186,259,14,171,171,45,187,260,17,174,174,46,188
                                       ,113,114,115,115,47,189,118,119,120,120,131,231,123,14,124,124,129,233,127,114,115,115,236,189};
                styleIndexArray = styleIndexArray1;
            }
            SectorsCount = SectorsCount > 2 ? SectorsCount * d : SectorsCount;

            int styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0, remIdx = 3, eCell = 7;

            if (!HasWeightColumn)
            {
                while (i < 8)
                {
                    styleIndexArray = styleIndexArray.Where((val, idx) => idx != remIdx).ToArray();
                    remIdx += 5; i++;
                }
                eCell = 6;
            }

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row1 = new Row() { RowIndex = (uint)(rowNum - 1), Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell1 = new Cell() { CellReference = "A" + (rowNum - 1), StyleIndex = (UInt32Value)1U };
            row1.Append(cell1);
            sheetData.Append(row1);

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);
            rowNum++;//1

            styleIndex = 1; startCell = 3;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row5, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row5);

            rowNum++;//2
            startCell = 2;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row6, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2;
            Row row = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row);

            rowNum++;//3
            startCell = 2;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

            int numRows = 1, styIdx = 0;//4
            while (SectorsCount > numRows)
            {
                rowNum++;
                startCell = 2; styIdx = styleIndex;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row8, startCell, eCell, ref styIdx, ref rowNum, styleIndexArray);
                sheetData.Append(row8);
                numRows++;
            }
            styleIndex = styleIndex + eCell - 1;

            //No AnswerRow
            if (!CutNA && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row9, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row9);
            }
            else
                styleIndex = styleIndex + eCell - 1;

            //InvalidRow
            if (!CutIV && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row10, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row10);
            }
            else
                styleIndex = styleIndex + eCell - 1;

            if (!CutWT && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row11, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row11);

                rowNum++;
                startCell = 2;
                Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row12, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row12);
            }

            if (CutIV == true && CutNA == true && CutWT == true && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                styleIndex = 312; startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };

                while (startCell <= eCell)
                {
                    Cell cell112 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                    row11.Append(cell112);
                    startCell++;
                }
                sheetData.Append(row11);
            }
            else
            {
                rowNum++;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                sheetData.Append(row11);
            }

            if (withBlockSubT > 0)
            {
                startCell = 2; int[] indexArray = { 313, 114, 314, 315, 47, 189 }; styleIndex = 0;
                if (!HasWeightColumn)
                    indexArray = indexArray.Where((val, idx) => idx != 2).ToArray();
                int sRow = ((rowNum - 1) - (int)withBlockSubT);
                while (startCell <= eCell)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)sRow);
                    Cell cell = OpenXmlHelper.GetCell(r, sRow, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateMatrixPHybridSigTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, bool CutWT, long SectorsCount, long d, int rowNum, long ChildQuestionsCount, bool HasLetterColumn, bool CutLetterRow, bool HasWeightColumn, long withBlockSubT)
        {
            int[] styleIndexArray = { 49,3,132,132,53,53,268,269,54,133,95,54,49,50,132,139,134,134,135,136,137,138,135,137,49,139,139,139,154,154,155,156,157,237,158,157,49,139,139,139,140,140,192,193,143,144,145,143
                                    ,266,276,146,281,31,31,207,208,325,243,148,209,150,277,151,283,64,64,211,212,244,245,246,247,266,276,146,281,31,31,207,208,325,243,148,209
                                    ,150,277,151,283,64,64,211,212,244,245,246,247};
            int[] colIndex = new int[12];
            int numColums = 0, sCell = 6, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }

            if (HasWeightColumn)
            {
                colIndex[i] = 2;
                i++;
                sCell = 7;
            }
            if (!HasLetterColumn)
            {
                colIndex[i] = 3;
                i++;
                sCell = 7;
            }

            if (!HasLetterColumn && HasWeightColumn)
                sCell = 8;
            colIndex[i] = 4;
            i++;
            colIndex[i] = 5;
            i++;
            if (SectorsCount == 1)
            {
                colIndex[i] = 6;
                i++;
                colIndex[i] = 7;
                i++;
            }
            else if (SectorsCount > 1)
            {
                colIndex[i] = 6;
                i++;
                colIndex[i] = 7;
                i++; sCell = sCell == 8 ? sCell : i;
            }
            if (!CutNA)
            {
                colIndex[i] = 8;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 9;
                i++;
            }
            if (!CutWT)
            {
                colIndex[i] = 10;
                i++;
                colIndex[i] = 11;
                i++;
            }

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
            MergeCells mergeCells = null;
            MergeCell mergeCell = null;

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;//1

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 12);
            sheetData.Append(row5);

            rowNum++;//2
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 12);
            sheetData.Append(row6);

            rowNum++;//3
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 12);
            sheetData.Append(row7);

            if (!CutLetterRow)
            {
                rowNum++;//4
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 12);
                sheetData.Append(row8);
            }
            else
            {
                startCell = 2; styleIndex = 0;
                while (startCell <= (i + 1))
                {
                    colIndex[styleIndex] += 12;
                    startCell++; styleIndex++;
                }
            }

            rowNum++;//5
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row9, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 12);
            sheetData.Append(row9);

            rowNum++;//6
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row10, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 12);
            sheetData.Append(row10);

            mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().FirstOrDefault();
            if (mergeCells == null)
            {
                mergeCells = new MergeCells();
            }

            String mergeVariable = !HasLetterColumn ? HasWeightColumn ? "E" : "D" : "";
            if (mergeVariable != "")
            {
                mergeCell = new MergeCell() { Reference = mergeVariable + (rowNum - 1) + ":" + mergeVariable + rowNum };
                mergeCells.Append(mergeCell);
            }
            mergeCell = new MergeCell() { Reference = "C" + (rowNum - 1) + ":" + "C" + rowNum };
            mergeCells.Append(mergeCell);

            int rowCount = 1;
            while (ChildQuestionsCount > rowCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row11, startCell, ref colIndex, styleIndex,ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 0);
                sheetData.Append(row11);

                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row12, startCell, ref colIndex, styleIndex,ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 12);
                sheetData.Append(row12);
                rowCount++;

                if (mergeVariable != "")
                {
                    mergeCell = new MergeCell() { Reference = mergeVariable + (rowNum - 1) + ":" + mergeVariable + rowNum };
                    mergeCells.Append(mergeCell);
                }
                mergeCell = new MergeCell() { Reference = "C" + (rowNum - 1) + ":" + "C" + rowNum };
                mergeCells.Append(mergeCell);
            }

            rowNum++;
            Row row13 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row13);

            if (withBlockSubT > 0)
            {
                int sCount = 12;
                styleIndex = 6;
                //ChildQuestionsCount += ChildQuestionsCount < 2 ? 0 : d;
                startCell = numColums - (int)withBlockSubT;
                int row = mergeRowNum + 2;
                int eRow = row + (CutLetterRow == true ? 2 : 3);
                MWTQuestionLeftLineDraw(worksheetPart, row, eRow, styleIndex, (int)d, (int)ChildQuestionsCount, startCell, styleIndexArray, sCount, CutLetterRow);
            }

            //MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                if (mergeCells == null)
                    mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        public void CreateNumericMatrixHybridSigTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, long ChildQuestionsCount, bool HasWeightColumn,bool CutMedian)
        {
            int[] styleIndexArray = { 0, 3, 132, 132, 94, 94, 95, 96, 96, 96, 96, 96, 96, 54, 94, 55, 0, 97, 132, 132, 99, 99, 72, 73, 73, 73, 73, 73, 73, 74, 99, 75, 264, 76, 160, 160, 34, 34, 100, 316, 317, 318, 319, 320, 321, 102, 161, 222 };
            int[] colIndex = new int[16];
            int numRows = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 32;
            i++;
            while (i < 3)
            {
                colIndex[i] = i;
                i++;
            }
            if (HasWeightColumn)
            {
                colIndex[i] = 3;
                i++;
            }
            colIndex[i] = 4;
            i++;
            colIndex[i] = 5;
            i++;
            if (output.ParentRequest.ShowParameter)
            {
                colIndex[i] = 6;
                i++;
            }
            if (output.ParentRequest.ShowSummary)
            {
                colIndex[i] = 7;
                i++;
            }
            if (output.ParentRequest.ShowAverage)
            {
                colIndex[i] = 8;
                i++;
            }
            if (output.ParentRequest.ShowStdev)
            {
                colIndex[i] = 9;
                i++;
            }
            if (output.ParentRequest.ShowMinimum)
            {
                colIndex[i] = 10;
                i++;
            }
            if (output.ParentRequest.ShowMaximum)
            {
                colIndex[i] = 11;
                i++;
            }
            if (output.ParentRequest.ShowMedian && !CutMedian)
            {
                colIndex[i] = 12;
                i++;
            }
            if (!CutNA)
            {
                colIndex[i] = 13;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 14;
                i++;
            }
            colIndex[i] = 15;
            i++;

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            Row row2 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            Cell cell22 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)1U };

            row2.Append(cell22);
            sheetData.Append(row2);
            rowNum++;

            Row row3 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 22.5D, CustomHeight = true };
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)273U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;//1
            startCell = 3; styleIndex = 1;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row5, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 16);
            sheetData.Append(row5);

            rowNum++;//2
            startCell = 3; styleIndex = 1;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row6, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 16);
            sheetData.Append(row6);

            while (numRows < ChildQuestionsCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                while (startCell <= (i + 1))
                {
                    Cell cell72 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                    row7.Append(cell72);
                    styleIndex++; startCell++;
                }
                numRows++;
                sheetData.Append(row7);
            }

            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            MergeCells mergeCells = null;
            if (mergeRowNum > 3)
            {
                mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
            }
            else
            {
                mergeCells = new MergeCells();
                MergeCell mergeCell13 = new MergeCell() { Reference = "B" + mergeRowNum + ":O" + mergeRowNum };
                mergeCells.Append(mergeCell13);
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }
        }
        //DrawLine
        public void MWTQuestionLeftLineDraw(WorksheetPart worksheetPart, int row, int eRow, int styleIndex, int d, int ChildQuestionsCount, int startCell, int[] styleIndexArray, int sCount, bool cutLetterRow = false)
        {
            while (row <= eRow)
            {
                Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                Cell cell = OpenXmlHelper.GetCell(r, row, startCell);
                cell.StyleIndex = (uint)styleIndexArray[styleIndex];
                styleIndex += sCount; row++;
            }
            eRow = (ChildQuestionsCount * d) + row;
            if (cutLetterRow == true)
                styleIndex += sCount;

            while (row <= eRow)
            {
                Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                Cell cell = OpenXmlHelper.GetCell(r, row, startCell);
                if (cell == null)
                    break;
                cell.StyleIndex = (uint)styleIndexArray[styleIndex];
                row++;
                if (d > 1)
                {
                    r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    cell = OpenXmlHelper.GetCell(r, row, startCell);
                    cell.StyleIndex = (uint)styleIndexArray[(styleIndex + sCount)];
                    row++;
                }
            }
        }
        public void CreateCells(Row row,int startCell,ref int[] colIndex,int styleIndex,ref int numColums,int sCell,int lmt,long SectorsCount,ref int rowNum,
                                int[] styleIndexArray,int IdxNum)
        {
            while (startCell <= lmt)
            {
                Cell cell52 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(numColums) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                row.Append(cell52);
                colIndex[styleIndex] += IdxNum;
                styleIndex++; numColums++;
                if (startCell == sCell && SectorsCount > 1)
                {
                    numColums = (startCell + 1);
                    while (numColums < (SectorsCount + (long)startCell))
                    {
                        Cell cell53 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter((int)numColums) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                        row.Append(cell53);
                        numColums++;
                    }
                    colIndex[styleIndex] += IdxNum;
                    styleIndex++;
                }
                startCell++;
            }
        }
        public void CreateCellsDown(Row row, int startCell, ref int[] colIndex, int styleIndex,ref int numColums, int sCell, int lmt, long SectorsCount, ref int rowNum,
                               int[] styleIndexArray, int IdxNum)
        {
            while (startCell <= lmt)
            {
                Cell cell52 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(numColums) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex] + IdxNum] };
                row.Append(cell52);
                styleIndex++; numColums++;
                if (startCell == sCell && SectorsCount > 1)
                {
                    numColums = (startCell + 1);
                    while (numColums < (SectorsCount + (long)startCell))
                    {
                        Cell cell53 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter((int)numColums) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex] + IdxNum] };
                        row.Append(cell53);
                        numColums++;
                    }
                    styleIndex++;
                }
                startCell++;
            }
        }
        public void CreateSigleCell(Row row,int startCell,int lmt,ref int styleIndex,ref int rowNum,int[] styleIndexArray)
        {
            while (startCell <= lmt)
            {
                Cell cell53 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex] };
                row.Append(cell53);
                styleIndex++; startCell++;
            }
        }
        public void CreateNumericSigleCell(Row row, int startCell, int lmt, ref int styleIndex, ref int rowNum, int[] styleIndexArray
                                           ,ref int[] colIndex,int colIdx)
        {
            while (startCell <= lmt)
            {
                Cell cell52 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[colIndex[styleIndex]] };
                row.Append(cell52);
                colIndex[styleIndex] += colIdx;
                styleIndex++; startCell++;
            }
        }
    }
}
