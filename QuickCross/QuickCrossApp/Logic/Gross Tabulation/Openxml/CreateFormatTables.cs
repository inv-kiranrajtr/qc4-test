using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Macromill.QCWeb.ReportRequest;
using Microsoft.VisualBasic;
using Qc4Launcher.Util;
using static Macromill.QCWeb.Batch.Report.Outputs;

namespace Qc4Launcher.Logic.Gross_Tabulation.Openxml
{
    public class CreateFormatTables
    {

        //Standard
        public void CreateNperTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long withBlockSubT)
        {
            int[] styleIndexArray = { 278, 10, 11, 12, 279, 13, 14, 15, 16, 17, 18, 19 };
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);

            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            styleIndex = 3; startCell = 3;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };

            while (startCell <= 5)
            {
                Cell cell53 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                row5.Append(cell53);
                styleIndex++; startCell++;
            }
            sheetData.Append(row5);

            rowNum++;
            styleIndex = 6; startCell = 2;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };

            while (startCell <= 5)
            {
                Cell cell62 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                row6.Append(cell62);
                styleIndex++; startCell++;
            }
            sheetData.Append(row6);

            rowNum++;
            styleIndex = 0; startCell = 2;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, 5, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

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

            //No AnswerRow
            if (!CutNA && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                styleIndex = 16; startCell = 2;
                Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                while (startCell <= 5)
                {
                    Cell cell92 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                    row9.Append(cell92);
                    styleIndex++; startCell++;
                }
                sheetData.Append(row9);
            }

            //InvalidRow
            if (!CutIV && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                styleIndex = 20; startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                while (startCell <= 5)
                {
                    Cell cell102 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndex };
                    row10.Append(cell102);
                    styleIndex++; startCell++;
                }
                sheetData.Append(row10);
            }

            if (CutIV == true && CutNA == true && tableOrientation == TableOrientation.Portrait)
            {
                rowNum++;
                styleIndex = 370; startCell = 2;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };

                while (startCell <= 5)
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
                startCell = 2;
                int[] indexArray = { 377, 96, 98, 54 }; styleIndex = 0;
                int row = ((rowNum - 1) - (int)withBlockSubT);
                while (startCell <= 5)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    Cell cell = OpenXmlHelper.GetCell(r, row, startCell);
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
        public void CreateNTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long withBlockSubT)
        {
            int[] styleIndexArray = { 3, 24, 6, 25, 26, 278, 27, 28, 279, 29, 30, 16, 31, 32, 20, 33, 34 };
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);
            rowNum++;

            styleIndex = 0; startCell = 3;
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

            int numRows = 1, styIdx = 0;
            while (SectorsCount > numRows)
            {
                rowNum++;
                startCell = 2; styIdx = styleIndex;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row8, startCell, 4, ref styIdx, ref rowNum, styleIndexArray);
                sheetData.Append(row8);
                numRows++;
            }
            if (SectorsCount == 1)
                styleIndex = styleIndex + 3;

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
                styleIndex = 33; startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row10, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row10);
            }

            if (withBlockSubT > 0)
            {
                startCell = 2; int[] indexArray = { 377, 96, 32 }; styleIndex = 0;
                int row = ((rowNum) - (int)withBlockSubT);
                while (startCell <= 4)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    Cell cell = OpenXmlHelper.GetCell(r, row, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

            rowNum++;
            styleIndex = 370; startCell = 2;
            Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            while (startCell <= 4)
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
        public void CreatePTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long withBlockSubT)
        {
            int[] styleIndexArray = { 3, 24, 6, 25, 26, 278, 27, 35, 279, 29, 36, 16, 31, 37, 20, 33, 38 };
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);
            rowNum++;

            styleIndex = 0; startCell = 3;
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

            int numRows = 1, styIdx = 0;
            while (SectorsCount > numRows)
            {
                rowNum++;
                startCell = 2; styIdx = styleIndex;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row8, startCell, 4, ref styIdx, ref rowNum, styleIndexArray);
                sheetData.Append(row8);
                numRows++;
            }
            if (SectorsCount == 1)
                styleIndex = styleIndex + 3;

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
                styleIndex = 33; startCell = 2;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateSigleCell(row10, startCell, 4, ref styleIndex, ref rowNum, styleIndexArray);
                sheetData.Append(row10);
            }

            if (withBlockSubT > 0)
            {
                startCell = 2; int[] indexArray = { 377, 96, 387 }; styleIndex = 0;
                int row = ((rowNum) - (int)withBlockSubT);
                while (startCell <= 4)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    Cell cell = OpenXmlHelper.GetCell(r, row, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

            rowNum++;
            styleIndex = 370; startCell = 2;
            Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            while (startCell <= 4)
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
        public void CreateMatrixNPTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long ChildQuestionsCount, long withBlockSubT)
        {
            int[] styleIndexArray = { 39, 3, 42, 287, 288, 43, 44, 39, 3, 273, 276, 277, 274, 275, 39, 40, 267, 78, 80, 81, 82, 282, 327, 45, 46, 47, 48, 49, 50, 328, 51, 52, 53, 54, 55, 282, 327, 45, 46, 47, 48, 49, 50, 328, 51, 52, 53, 54, 55 };
            int[] colIndex = new int[7];
            int numColums = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0, sCell = 5;

            while (i < 3)
            {
                colIndex[i] = i;
                i++;
            }

            if (SectorsCount == 1)
            {
                colIndex[i] = 3;
                i++;
                colIndex[i] = 4;
                i++;
            }
            else if (SectorsCount > 1)
            {
                colIndex[i] = 3;
                i++;
                colIndex[i] = 4;
                i++; sCell = i;
            }
            if (!CutNA)
            {
                colIndex[i] = 5;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 6;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
            sheetData.Append(row7);


            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
            sheetData.Append(row8);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row9, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
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
                CreateCellsDown(row11, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
                sheetData.Append(row11);
                rowCount++;
                MergeCell mergeCell2 = new MergeCell() { Reference = "C" + (rowNum - 1) + ":C" + rowNum };
                mergeCells.Append(mergeCell2);
            }
            if (withBlockSubT > 0)
            {
                startCell = numColums - (int)withBlockSubT;
                int stIndx = 0;
                int[] indexArray = { 403, 402, 401, 399, 400 };
                int startRow = rowNum - (2 + (int)(ChildQuestionsCount * 2));
                while (startRow <= rowNum)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)startRow);
                    Cell cell = OpenXmlHelper.GetCell(r, startRow, startCell);
                    cell.StyleIndex = (uint)indexArray[stIndx];
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
            int[] styleIndexArray = { 39, 3, 42, 287, 288, 43, 44, 39, 3, 273, 276, 277, 274, 275, 39, 40, 267, 78, 80, 81, 82, 281, 56, 26, 22, 57, 58, 59, 281, 56, 26, 22, 57, 58, 59, 281, 56, 22, 57, 58, 59 };
            int[] colIndex = new int[7];
            int numColums = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0, sCell = 5;

            while (i < 3)
            {
                colIndex[i] = i;
                i++;
            }

            if (SectorsCount == 1)
            {
                colIndex[i] = 3;
                i++;
                colIndex[i] = 4;
                i++;
            }
            else if (SectorsCount > 1)
            {
                colIndex[i] = 3;
                i++;
                colIndex[i] = 4;
                i++; sCell = i;
            }
            if (!CutNA)
            {
                colIndex[i] = 5;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 6;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
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
            if (withBlockSubT > 0)
            {
                startCell = numColums - (int)withBlockSubT;
                int stIndx = 0;
                int[] indexArray = { 403, 402, 401, 404 };
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
            int[] styleIndexArray = { 39, 3, 42, 287, 288, 43, 60, 39, 3, 273, 276, 277, 274, 273, 39, 40, 267, 78, 80, 81, 268, 281, 56, 26, 62, 63, 64, 38, 281, 56, 26, 62, 63, 64, 38, 281, 56, 26, 62, 63, 64, 38 };
            int[] colIndex = new int[7];
            int numColums = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0, sCell = 5;

            while (i < 3)
            {
                colIndex[i] = i;
                i++;
            }

            if (SectorsCount == 1)
            {
                colIndex[i] = 3;
                i++;
                colIndex[i] = 4;
                i++;
            }
            else if (SectorsCount > 1)
            {
                colIndex[i] = 3;
                i++;
                colIndex[i] = 4;
                i++; sCell = i;
            }
            if (!CutNA)
            {
                colIndex[i] = 5;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 6;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 7);
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
            if (withBlockSubT > 0)
            {
                startCell = numColums - (int)withBlockSubT;
                int stIndx = 0;
                int[] indexArray = { 403, 402, 401, 405 };
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
        public void CreateNumericTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, bool CutMedian)
        {
            int[] styleIndexArray = { 0, 3, 65, 66, 67, 67, 67, 67, 67, 67, 68, 69, 281, 70, 26, 22, 83, 372, 373, 374, 375, 376, 58, 59 };
            int[] colIndex = new int[12];
            int numColums = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 12;
            i++;
            while (i < 3)
            {
                colIndex[i] = i;
                i++;
            }

            if (output.ParentRequest.ShowParameter)
            {
                colIndex[i] = 3;
                i++;
            }
            if (output.ParentRequest.ShowSummary)
            {
                colIndex[i] = 4;
                i++;
            }
            if (output.ParentRequest.ShowAverage)
            {
                colIndex[i] = 5;
                i++;
            }
            if (output.ParentRequest.ShowStdev)
            {
                colIndex[i] = 6;
                i++;
            }
            if (output.ParentRequest.ShowMinimum)
            {
                colIndex[i] = 7;
                i++;
            }
            if (output.ParentRequest.ShowMaximum)
            {
                colIndex[i] = 8;
                i++;
            }
            if (output.ParentRequest.ShowMedian && !CutMedian)
            {
                colIndex[i] = 9;
                i++;
            }
            if (!CutNA)
            {
                colIndex[i] = 10;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 11;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;
            startCell = 3; styleIndex = 1;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row5, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 12);
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
        public void CreateNumericMatrixTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, long ChildQuestionsCount, bool CutMedian)
        {
            int[] styleIndexArray = { 0, 3, 72, 73, 74, 75, 75, 75, 75, 75, 43, 44, 0, 76, 77, 78, 79, 80, 80, 80, 80, 80, 81, 82, 281, 56, 258, 83, 83, 372, 373, 374, 375, 376, 85, 59 };
            int[] colIndex = new int[12];
            int numRows = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 24;
            i++;
            while (i < 3)
            {
                colIndex[i] = i;
                i++;
            }

            if (output.ParentRequest.ShowParameter)
            {
                colIndex[i] = 3;
                i++;
            }
            if (output.ParentRequest.ShowSummary)
            {
                colIndex[i] = 4;
                i++;
            }
            if (output.ParentRequest.ShowAverage)
            {
                colIndex[i] = 5;
                i++;
            }
            if (output.ParentRequest.ShowStdev)
            {
                colIndex[i] = 6;
                i++;
            }
            if (output.ParentRequest.ShowMinimum)
            {
                colIndex[i] = 7;
                i++;
            }
            if (output.ParentRequest.ShowMaximum)
            {
                colIndex[i] = 8;
                i++;
            }
            if (output.ParentRequest.ShowMedian && !CutMedian)
            {
                colIndex[i] = 9;
                i++;
            }
            if (!CutNA)
            {
                colIndex[i] = 10;
                i++;
            }
            if (!CutIV)
            {
                colIndex[i] = 11;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;
            startCell = 3; styleIndex = 1;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row5, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 12);
            sheetData.Append(row5);

            rowNum++;
            startCell = 3; styleIndex = 1;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row6, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 12);
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
            int[] styleIndexArray = { 0, 3, 86, 4, 5, 87, 25, 88, 89, 9, 283, 90, 91, 92, 93, 284, 29, 94, 14, 15, 95, 393, 394, 395, 396, 99, 90, 100, 46, 101, 102, 27, 103, 104, 105, 106, 31, 107, 330, 331 };
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

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
                styleIndex = 370; startCell = 2;
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
                int[] indexArray = { 377, 96, 389, 390, 54 };
                int[] indexArray2 = { 377, 393, 394, 395, 396 };
                if (HasWeightColumn)
                    indexArray = indexArray2;
                startCell = 2; styleIndex = 0;
                int row = ((rowNum - 1) - (int)withBlockSubT);
                while (startCell <= eCell)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    Cell cell = OpenXmlHelper.GetCell(r, row, startCell);
                    cell.StyleIndex = (uint)indexArray[styleIndex];
                    styleIndex++; startCell++;
                }
            }

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
            int[] styleIndexArray = { 0, 3, 86, 108, 87, 25, 109, 26, 283, 90, 110, 111, 284, 29, 112, 30, 95, 398, 397, 32, 99, 90, 114, 34, 102, 27, 115, 116, 106, 31, 117, 118 };
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

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
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

            int numRows = 1, styIdx = 0;
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
                styleIndex = 33; startCell = 2;
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
                styleIndex = 370; startCell = 2;
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
                int[] indexArray = { 377, 96, 391, 32 };
                int[] indexArray2 = { 377, 398, 397, 32 };
                if (HasWeightColumn)
                    indexArray = indexArray2;
                startCell = 2; styleIndex = 0;
                int row = ((rowNum - 1) - (int)withBlockSubT);
                while (startCell <= eCell)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    Cell cell = OpenXmlHelper.GetCell(r, row, startCell);
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
            int[] styleIndexArray = { 0, 3, 86, 108, 87, 25, 109, 26, 283, 90, 110, 119, 284, 29, 112, 36, 95, 398, 397, 37, 99, 90, 114, 38, 102, 27, 115, 116, 106, 31, 117, 118 };

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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

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
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateSigleCell(row7, startCell, eCell, ref styleIndex, ref rowNum, styleIndexArray);
            sheetData.Append(row7);

            int numRows = 1, styIdx = 0;
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
                styleIndex = 33; startCell = 2;
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
                styleIndex = 370; startCell = 2;
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
                int[] indexArray = { 377, 96, 392, 37 };
                int[] indexArray2 = { 377, 398, 397, 37 };
                if (HasWeightColumn)
                    indexArray = indexArray2;
                startCell = 2; styleIndex = 0;
                int row = ((rowNum - 1) - (int)withBlockSubT);
                while (startCell <= eCell)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    Cell cell = OpenXmlHelper.GetCell(r, row, startCell);
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
            int[] styleIndexArray = { 39,3,122,123,302,303,304,305,306,304,39,40,122,128,307,308,309,310,307,309,39,134,134,135,311,312,313,314,315,313
                                      ,280,327,141,142,383,384,385,319,143,321,144,328,145,146,322,323,324,325,326,386,280,327,141,142,383,384,385,319,143,321
                                      ,144,328,145,146,322,323,324,325,326,386};
            int[] colIndex = new int[10];
            int numColums = 0, styleIndex = 0, startCell = 0, eCell = 5, mergeRowNum = rowNum + 1;
            int i = 0, cutNAIV = 0;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            if (HasWeightColumn)
            {
                colIndex[i] = 2;
                i++;
                eCell = 6;
            }
            colIndex[i] = 3;
            i++;
            if (SectorsCount > 0)
            {
                colIndex[i] = 4;
                i++;
                colIndex[i] = 5;
                i++;
            }
            if (!CutNA)
            {
                colIndex[i] = 6;
                i++; cutNAIV++;
            }
            if (!CutIV)
            {
                colIndex[i] = 7;
                i++; cutNAIV++;
            }
            if (!CutWT)
            {
                colIndex[i] = 8;
                i++;
                colIndex[i] = 9;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row8);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
            CreateCells(row9, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
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
                CreateCellsDown(row10, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 0);
                sheetData.Append(row10);

                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row11 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = true };
                CreateCellsDown(row11, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
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
                int sCount = 10;
                styleIndex = 4;
                //ChildQuestionsCount = ChildQuestionsCount == 1 ? 0 : d;
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
            int[] styleIndexArray = { 39,3,122,147,302,290,124,125,126,127,39,40,122,148,307,130,131,132,129,133,39,134,134,150,311,136,137,138,139,140,
                                       281,70,152,26,22,57,58,34,153,154,281,70,152,26,22,57,58,34,153,154 };
            int[] colIndex = new int[10];
            int numColums = 0, styleIndex = 0, startCell = 0, eCell = 5, mergeRowNum = rowNum + 1;
            int i = 0, cutNAIV = 0;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            if (HasWeightColumn)
            {
                colIndex[i] = 2;
                i++;
                eCell = 6;
            }
            colIndex[i] = 3;
            i++;
            if (SectorsCount > 0)
            {
                colIndex[i] = 4;
                i++;
                colIndex[i] = 5;
                i++;
            }
            if (!CutNA)
            {
                colIndex[i] = 6;
                i++; cutNAIV++;
            }
            if (!CutIV)
            {
                colIndex[i] = 7;
                i++; cutNAIV++;
            }
            if (!CutWT)
            {
                colIndex[i] = 8;
                i++;
                colIndex[i] = 9;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row8);

            int rowCount = 1;
            while (ChildQuestionsCount > rowCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row10, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 0);
                sheetData.Append(row10);
                rowCount++;
            }
            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            if (withBlockSubT > 0)
            {
                int sCount = 10;
                styleIndex = 4;
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
            int[] styleIndexArray = { 39,3,122,147,302,290,124,125,126,127,39,40,122,148,307,130,131,132,129,133,39,134,134,150,311,136,137,138,139,140,
                                       281,70,152,26,62,63,64,38,153,156,281,70,152,26,62,63,64,38,153,156 };
            int[] colIndex = new int[10];
            int numColums = 0, styleIndex = 0, startCell = 0, eCell = 5, mergeRowNum = rowNum + 1;
            int i = 0, cutNAIV = 0;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }
            if (HasWeightColumn)
            {
                colIndex[i] = 2;
                i++;
                eCell = 6;
            }
            colIndex[i] = 3;
            i++;
            if (SectorsCount > 0)
            {
                colIndex[i] = 4;
                i++;
                colIndex[i] = 5;
                i++;
            }
            if (!CutNA)
            {
                colIndex[i] = 6;
                i++; cutNAIV++;
            }
            if (!CutIV)
            {
                colIndex[i] = 7;
                i++; cutNAIV++;
            }
            if (!CutWT)
            {
                colIndex[i] = 8;
                i++;
                colIndex[i] = 9;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row5);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row6);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row7);

            rowNum++;
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 10);
            sheetData.Append(row8);

            int rowCount = 1;
            while (ChildQuestionsCount > rowCount)
            {
                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row10, startCell, ref colIndex, styleIndex, ref numColums, eCell, i, SectorsCount, ref rowNum, styleIndexArray, 0);
                sheetData.Append(row10);
                rowCount++;
            }
            rowNum++;
            Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row12);

            if (withBlockSubT > 0)
            {
                int sCount = 10;
                styleIndex = 4;
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
        public void CreateNumericWTTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, bool HasWeightColumn, bool CutMedian)
        {
            int[] styleIndexArray = { 0, 3, 122, 157, 66, 67, 67, 67, 67, 67, 67, 68, 69, 281, 70, 161, 26, 22, 388, 372, 373, 374, 375, 376, 58, 59 };
            int[] colIndex = new int[13];
            int numColums = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 13;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

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
        public void CreateNumericMatrixWTTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, long ChildQuestionsCount, bool HasWeightColumn, bool CutMedian)
        {
            int[] styleIndexArray = { 0, 3, 122, 72, 73, 74, 75, 75, 75, 75, 75, 43, 44, 0, 76, 122, 61, 78, 79, 80, 80, 80, 80, 80, 81, 82, 281, 70, 161, 26, 83, 388, 372, 373, 374, 375, 376, 85, 59 };
            int[] colIndex = new int[13];
            int numRows = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 26;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

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
        //Significance
        public void CreatePSigTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, long withBlockSubT)
        {
            int[] styleIndexArray = { 3, 86, 24, 178, 87, 162, 88, 26, 165, 278, 27, 166, 35, 168, 279, 29, 169, 36, 171, 172, 31, 107, 120, 174, 20, 175, 176, 38, 181 };
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

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
                styleIndex = 370; startCell = 2;
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
                startCell = 2; int[] indexArray = { 377, 219, 107, 299, 296 }; styleIndex = 0;
                int row = ((rowNum - 1) - (int)withBlockSubT);
                while (startCell <= 6)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    Cell cell = OpenXmlHelper.GetCell(r, row, startCell);
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
            int[] styleIndexArray = { 39,3,122,42,287,288,44,60,39,40,122,148,129,130,131,182,39,40,122,269,261,262,270,264,39,134,134,150,183,184,137,150,
                                      280,202,335,45,186,187,203,188,144,204,336,51,196,197,173,198,280,202,335,45,186,187,203,189,144,204,336,51,196,197,173,120};
            int[] colIndex = new int[8];
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

            if (SectorsCount > 0)
            {
                colIndex[i] = 4;
                i++;
                colIndex[i] = 5;
                i++;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;//1

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row5);

            rowNum++;//2
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row6);

            rowNum++;//3
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row7);

            if (!CutLetterRow)
            {
                rowNum++;//4
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
                sheetData.Append(row8);
            }
            else
            {
                startCell = 2; styleIndex = 0;
                while (startCell <= (i + 1))
                {
                    colIndex[styleIndex] += 8;
                    startCell++; styleIndex++;
                }
            }

            rowNum++;//5
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row9, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
            sheetData.Append(row9);

            rowNum++;//6
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row10, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
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

                rowNum++;//6
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row12, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 8);
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
                int[] indexArray = { 287, 129, 243, 183, 186, 196 };
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
        public void CreateNumericMatrixSigTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, long ChildQuestionsCount, bool CutMedian)
        {
            int[] styleIndexArray = { 0,3,122,72,73,75,75,75,75,75,75,43,241,259,0,76,122,260,261,262,262,262,262,262,262,263,300,264,
                                       281,70,161,258,83,388,372,373,374,375,376,85,265,266 };
            int[] colIndex = new int[15];
            int numRows = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 28;
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
            colIndex[i] = 13;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;//1
            startCell = 3; styleIndex = 1;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateNumericSigleCell(row5, startCell, (i + 1), ref styleIndex, ref rowNum, styleIndexArray, ref colIndex, 14);
            sheetData.Append(row5);

            rowNum++;//2
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
        //Hybrid
        public void CreatePHybridSigTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, bool CutWT, long SectorsCount, long d, int rowNum, bool HasWeightColumn, long withBlockSubT)
        {
            int[] styleIndexArray = {0,3,86,86,230,108,6,215,109,88,258,179,278,216,217,166,239,233,279,218,112,169,297,298,16,219,117,107,
                                      299,296,220,221,114,114,291,292,223,216,115,103,293,294,227,228,113,107,295,296};
            if(!HasWeightColumn)
            {
                int[] styleIndexArray1 = {
                    0,3,86,86,230,108,6,215,109,88,258,179,278,216,166,166,239,233,279,218,169,169,297,298,16,219,117,107,
                                      299,296,220,221,114,114,291,292,223,216,115,103,293,294,227,228,113,107,295,296};
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

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
                styleIndex = 370; startCell = 2;
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
                int[] indexArray = { 377, 219, 117, 107, 299, 296 };
                startCell = 2; styleIndex = 0;
                if (!HasWeightColumn)
                    indexArray = indexArray.Where((val, idx) => idx != 2).ToArray();
                int row = ((rowNum - 1) - (int)withBlockSubT);
                while (startCell <= eCell)
                {
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
                    Cell cell = OpenXmlHelper.GetCell(r, row, startCell);
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
            int[] styleIndexArray = { 39,3,122,122,42,287,288,43,60,379,44,39,40,122,122,148,129,130,133,182,380,131,39,134,134,134,254,
                                        243,244,245,254,381,248,39,134,134,134,150,183,184,140,150,382,137,280,202,141,332,45,186,187,203,
                                        189,143,188,144,204,145,334,51,196,197,173,120,251,212,280,202,141,332,45,186,187,203,189,143,188,
                                        144,204,145,334,51,196,197,173,120,251,212,};
            int[] colIndex = new int[12];
            int numColums = 0, sCell = 5, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0, cutNAIV = 0;

            while (i < 2)
            {
                colIndex[i] = i;
                i++;
            }

            if (HasWeightColumn)
            {
                colIndex[i] = 2;
                i++;
                sCell = 6;
            }
            if (!HasLetterColumn)
            {
                colIndex[i] = 3;
                i++;
                sCell = 6;
            }

            if (!HasLetterColumn && HasWeightColumn)
                sCell = 7;
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
                i++; cutNAIV++;
            }
            if (!CutIV)
            {
                colIndex[i] = 8;
                i++; cutNAIV++;
            }
            if (!CutWT)
            {
                colIndex[i] = 9;
                i++;
                colIndex[i] = 10;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

            row3.Append(cell32);
            sheetData.Append(row3);
            rowNum++;

            Row row4 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            sheetData.Append(row4);

            rowNum++;//1

            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row5 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row5, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row5);

            rowNum++;//2
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row6 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row6, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row6);

            rowNum++;//3
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row7 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row7, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row7);

            if (!CutLetterRow)
            {
                rowNum++;//4
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row8 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCells(row8, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
                sheetData.Append(row8);
            }
            else
            {
                startCell = 2; styleIndex = 0;
                while (startCell <= (i + 1))
                {
                    colIndex[styleIndex] += 11;
                    startCell++; styleIndex++;
                }
            }

            rowNum++;//5
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row9 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row9, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
            sheetData.Append(row9);

            rowNum++;//6
            startCell = 2; styleIndex = 0;
            numColums = startCell;
            Row row10 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
            CreateCells(row10, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
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
                CreateCellsDown(row11, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 0);
                sheetData.Append(row11);

                rowNum++;
                startCell = 2; styleIndex = 0;
                numColums = startCell;
                Row row12 = new Row() { RowIndex = (uint)rowNum, Spans = new ListValue<StringValue>() { InnerText = "2:13" }, Height = 11.25D, CustomHeight = false };
                CreateCellsDown(row12, startCell, ref colIndex, styleIndex, ref numColums, sCell, i, SectorsCount, ref rowNum, styleIndexArray, 11);
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
                int sCount = 11;
                styleIndex = 5;
                //ChildQuestionsCount += ChildQuestionsCount == 1 ? 0 : d;
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
        public void CreateNumericMatrixHybridSigTable(WorksheetPart worksheetPart, string formatSheet, TableOrientation tableOrientation, bool CutIV, bool CutNA, long SectorsCount, long d, int rowNum, OutputGT output, long ChildQuestionsCount, bool HasWeightColumn, bool CutMedian)
        {
            int[] styleIndexArray = { 0,3,122,122,72,73,75,75,75,75,75,75,43,241,259,0,76,122,122,260,261,262,262,262,262,262,262,263,300,264,
                                       281,70,152,161,258,83,388,372,373,374,375,376,85,265,266 };
            int[] colIndex = new int[15];
            int numRows = 0, styleIndex = 0, startCell = 0, mergeRowNum = rowNum + 1;
            int i = 0;

            colIndex[i] = i + 30;
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
            Cell cell32 = new Cell() { CellReference = "B" + rowNum, StyleIndex = (UInt32Value)329U };

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
        public void CreateCells(Row row, int startCell, ref int[] colIndex, int styleIndex, ref int numColums, int sCell, int lmt, long SectorsCount, ref int rowNum,
                              int[] styleIndexArray, int IdxNum)
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
        public void CreateCellsDown(Row row, int startCell, ref int[] colIndex, int styleIndex, ref int numColums, int sCell, int lmt, long SectorsCount, ref int rowNum,
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
        public void CreateSigleCell(Row row, int startCell, int lmt, ref int styleIndex, ref int rowNum, int[] styleIndexArray)
        {
            while (startCell <= lmt)
            {
                Cell cell53 = new Cell() { CellReference = OpenXmlHelper.ColumnIndexToColumnLetter(startCell) + rowNum, StyleIndex = (uint)styleIndexArray[styleIndex] };
                row.Append(cell53);
                styleIndex++; startCell++;
            }
        }
        public void CreateNumericSigleCell(Row row, int startCell, int lmt, ref int styleIndex, ref int rowNum, int[] styleIndexArray
                                           , ref int[] colIndex, int colIdx)
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
