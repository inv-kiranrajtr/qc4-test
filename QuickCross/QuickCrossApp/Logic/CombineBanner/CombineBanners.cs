using log4net;
using NPOI.SS.Util;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using Qc4Launcher.Logic.CombineBanner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Tables;

namespace Qc4Launcher.Logic
{
    public static class CombineBanners
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Array of hyperlink addresses of source sheet tables.
        /// </summary>
        private static Array _hyperlinkAddresses;
        private static Dictionary<string, List<RangeRankingSheetData>> _dataMap;
        private static Dictionary<string, int> _hyperlinkIndexMap;
        private static readonly double _banner_Row_1_Height = 33.60;
        private static readonly double _banner_Row_2_Height = 22.20;
        private static readonly double _banner_Row_3_Height = 33.60;

        /// <summary>
        ///  background color used in banners.
        /// </summary>
        private static readonly Color _cellBackgroudColor = Color.FromArgb(255, 218, 238, 243);//light blue

        /// <summary>
        /// starting row number of hyperlink table in index sheet.
        /// </summary>
        private static readonly int _indexTableRow = 13;

        /// <summary>
        /// returns true if current sheet is a significance sheet.
        /// </summary>
        private static bool _isSignificanceSheet = false;

        /// <summary>
        /// starting column number of combined output sheet  (2= column B).
        /// </summary>
        private static readonly int _column = 2;

        /// <summary>
        /// Sets up a data map, associating sheet names with corresponding lists of <see cref="RangeRankingSheetData"/>.
        /// </summary>
        public static void SetDataMap()
        {
            _dataMap = new Dictionary<string, List<RangeRankingSheetData>>
            {
                { LocalResource.REPORT_CROSS_NP_SHEET_NAME, RangeRankingSheetStore.NModeDataList },
                { LocalResource.REPORT_CROSS_N_SHEET_NAME, RangeRankingSheetStore.NDataList },
                { LocalResource.REPORT_CROSS_P_SHEET_NAME, RangeRankingSheetStore.ModDataList }
            };
        }

        //function for cleaning static datas
        public static void ClearDatas()
        {
            _hyperlinkAddresses = null;
            _dataMap = null;
            _hyperlinkIndexMap = null;

        }

        /// <summary>
        /// Combines consequtive tables with same banners from each worksheet into a new Excel workbook using a template.
        /// </summary>
        /// <param name="hyperlinkTargetCells">The array of hyperlink target cells of source file worksheets.</param>
        /// <param name="outputPath">The path where the source Excel workbook will be saved.</param>
        /// <param name="currentOutput">Output Cross setting of current source file.</param>
        public static void Combine(Array hyperlinkTargetCells, string outputPath, OutputCross currentOutput)
        {
            _hyperlinkAddresses = hyperlinkTargetCells;

            _hyperlinkIndexMap = new Dictionary<string, int>
            {
                { LocalResource.REPORT_CROSS_NP_SHEET_NAME, 4 },
                { LocalResource.REPORT_CROSS_N_SHEET_NAME, 5},
                { LocalResource.REPORT_CROSS_P_SHEET_NAME, 6},
            };

            // Set the license context for the ExcelPackage.
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;

            // Initialize FileInfo for source and template files.
            FileInfo sourceFile = new FileInfo(outputPath);
            string templatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"QC4\Templates\CombineBannerTemplate.xlsx");
            FileInfo templateFile = new FileInfo(templatePath);

            // Load Excel packages for source, template, and target workbooks.
            ExcelPackage sourcePackage = new ExcelPackage(sourceFile);
            ExcelPackage templatePackage = new ExcelPackage(templateFile);
            ExcelPackage targetPackage = new ExcelPackage(templateFile);

            ExcelWorkbook sourceWorkbook = sourcePackage.Workbook;
            ExcelWorkbook templateWorkbook = templatePackage.Workbook;
            ExcelWorkbook targetWorkbook = targetPackage.Workbook;

            // Get the template sheet.
            ExcelWorksheet templateSheet = templateWorkbook.Worksheets[0];

            ExcelWorksheet indexSheet = null;

            foreach (ExcelWorksheet currentWorksheet in sourceWorkbook.Worksheets)
            {
                // Check if the current worksheet is the "INDEX" sheet.
                //index sheet is copied to new workbook and later the hyperlinks are updated.
                if (currentWorksheet.Name == "INDEX")
                {
                    targetWorkbook.Worksheets.Add(currentWorksheet.Name, currentWorksheet);
                    indexSheet = targetWorkbook.Worksheets[currentWorksheet.Name];
                }
                else
                {
                    try
                    {
                        ExcelRange prevBanner = null;
                        int row = 0;
                        int i;
                        _isSignificanceSheet = false;

                        // Check if the sheet is a significance sheet.
                        if (currentWorksheet.Name == LocalResource.GT_STATISTICAL_TEST || currentWorksheet.Name == LocalResource.GT_SHEET_HEADING_TEST)
                            _isSignificanceSheet = true;
                        else
                            _isSignificanceSheet = false;

                        //Adding the new sheet to new outputfile
                        targetWorkbook.Worksheets.Add(currentWorksheet.Name, templateSheet);
                        ExcelWorksheet targetWorksheet = targetWorkbook.Worksheets[currentWorksheet.Name];

                        //looping through the HyperlinkTargetCells array for finding the source file table ranges.
                        for (i = _hyperlinkAddresses.GetLowerBound(0); i <= _hyperlinkAddresses.GetUpperBound(0); i++)
                        {
                            AddTables(i, currentOutput, indexSheet, currentWorksheet, ref prevBanner, ref row, ref targetWorksheet);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.Message);
                    }
                }
            }

            // clearing all list after the execution
            RangeRankingSheetStore.NModeDataList.Clear();
            RangeRankingSheetStore.ModDataList.Clear();
            RangeRankingSheetStore.NDataList.Clear();

            targetWorkbook.Worksheets.Delete("template");

            indexSheet = targetWorkbook.Worksheets[0];
            indexSheet.Select();
            targetPackage.SaveAs(outputPath);
        }

        /// <summary>
        /// Adds tables to the target worksheet based on specified parameters and data.
        /// </summary>
        /// <param name="i">Index used within the method.</param>
        /// <param name="currentOutput">Output Cross setting of current source file.</param>
        /// <param name="indexSheet">Worksheet where the index is located.</param>
        /// <param name="currentWorksheet">Current ExcelWorksheet</param>
        /// <param name="prevBanner">Reference to the previous banner used.</param>
        /// <param name="row">Current row index.</param>
        /// <param name="targetWorksheet">The target worksheet where tables will be added.</param>
        private static void AddTables(int i, OutputCross currentOutput, ExcelWorksheet indexSheet, ExcelWorksheet currentWorksheet, ref ExcelRange prevBanner, ref int row, ref ExcelWorksheet targetWorksheet)
        {
            int columnNum = _hyperlinkIndexMap.TryGetValue(currentWorksheet.Name, out columnNum) ? columnNum : 7;

            if (_hyperlinkAddresses.GetValue(i, columnNum) != null)
            {
                int lastRow;
                int lastColumn;

                CrossTable tmpTable = (CrossTable)currentOutput.Tables[i - 1];
                //check if the current table is a GT or Twi-way  cross table 
                bool isGt = NPOICrossCreator.checkSimpleAggr(tmpTable);

                CellRangeAddress sourceTableRange = (CellRangeAddress)_hyperlinkAddresses.GetValue(i, columnNum);
                ExcelRange sourceTableNumber = currentWorksheet.Cells[sourceTableRange.FirstRow, sourceTableRange.FirstColumn + 1];
                ExcelRange sourceVariable = currentWorksheet.Cells[sourceTableRange.FirstRow + 1, sourceTableRange.FirstColumn + 1];
                ExcelRange sourceBanner = null;
                ExcelRange sourceTableData = null;
                ExcelRange filteringValue = null;

                SetSourceRanges(currentWorksheet, sourceTableRange, ref sourceBanner, ref sourceTableData, ref filteringValue, isGt, _isSignificanceSheet);

                ExcelRange targetBanner;
                ExcelRange targetTableNumber;
                ExcelRange targetFilterValue = null;
                ExcelRange targetVariable;
                ExcelRange targetTable;
                ExcelRange varibleMergeRange;
                ExcelRange focusRange;
                ExcelRange targetCell;

                //condition to check wheather the current table banner is same as the previous one,
                //if it is diffrent new banner will be added to the target sheet
                //else the table will be merged with the previous table
                if (prevBanner == null || CheckDiffrence(sourceBanner, prevBanner))
                {
                    row = ((targetWorksheet.Dimension?.End?.Row ?? 0) + 3);
                    prevBanner = sourceBanner;
                    targetWorksheet.Row(row).Height = _banner_Row_1_Height;

                    if (!isGt)
                    {
                        targetWorksheet.Row(row + 1).Height = _banner_Row_2_Height;
                        targetWorksheet.Row(row + 2).Height = _banner_Row_3_Height;
                    }

                    targetBanner = targetWorksheet.Cells[row, _column + 4, row + sourceBanner.Rows, _column + 4 + sourceBanner.Columns];
                    sourceBanner.Copy(targetBanner);
                }
                //increase row index by 1 to add table
                row = targetWorksheet.Dimension.End.Row + 1;

                //selecting target range for table number and adding table number and its styles to the target range
                targetTableNumber = targetWorksheet.Cells[row, _column];
                targetTableNumber.Value = sourceTableNumber.Value;
                sourceTableNumber.Copy(targetTableNumber);

                SetFilteringValue(row, _column, targetWorksheet, filteringValue, ref targetFilterValue);

                //selecting target range for variable name and adding variable name and its styles to the target range
                targetVariable = targetWorksheet.Cells[row, _column + 1];
                sourceVariable.Copy(targetVariable, ExcelRangeCopyOptionFlags.ExcludeMergedCells as ExcelRangeCopyOptionFlags?);

                varibleMergeRange = targetWorksheet.Cells[row, _column + 1, (row + sourceTableData.Rows) - 1, _column + 1];
                varibleMergeRange.Merge = true;
                varibleMergeRange.Style.Fill.SetBackground(_cellBackgroudColor);
                varibleMergeRange.Style.Border.BorderAround(ExcelBorderStyle.Thin, HexToColor(sourceBanner?.Style?.Border?.Right?.Color?.Rgb));

                //selecting target range for data table and adding data table and its styles to the target range
                targetTable = targetWorksheet.Cells[row, _column + 2, (row + sourceTableData.Rows) - 1, _column + 1 + sourceTableData.Columns];
                sourceTableData.Copy(targetTable);

                if (currentOutput.MarkingRanking)
                    SetRanking(ref targetWorksheet, targetTable, sourceTableRange);

                //creating new link for the current table and updating it in the index sheet. 
                lastRow = targetWorksheet.Dimension.End.Row;
                lastColumn = targetTable.End.Column;
                focusRange = targetWorksheet.Cells[row, _column, lastRow, lastColumn];
                string link = "#'" + currentWorksheet.Name + "'" + "!" + focusRange;
                targetCell = indexSheet.Cells[_indexTableRow + i, columnNum + 1];

                SetHyperlinks(targetCell, link);

            }
        }


        /// <summary>
        /// Sets the source ranges for an Excel worksheet based on table type and source sheet type.
        /// </summary>
        /// <param name="item">The ExcelWorksheet containing the data.</param>
        /// <param name="sourceTableRange">The range of the table in the worksheet.</param>
        /// <param name="sourceBanner">A reference to the ExcelRange for the banner section.</param>
        /// <param name="sourceTableData">A reference to the ExcelRange for the table data section.</param>
        /// <param name="filteringValue">A reference to the ExcelRange for filtering values.</param>
        /// <param name="isGt">A boolean indicating whether the table is of type GT.</param>
        /// <param name="isSignificanceSheet">A boolean indicating whether the source sheet is a significance sheet.</param>
        /// <remarks>
        /// This function calculates and sets the sourceBanner and sourceTableData ranges based on the table type (GT or multicross)
        /// and the source sheet type (significanceSheet or not). It also determines the filteringValue range for GT or multicross tables.
        /// </remarks>
        private static void SetSourceRanges(ExcelWorksheet item, CellRangeAddress sourceTableRange, ref ExcelRange sourceBanner, ref ExcelRange sourceTableData, ref ExcelRange filteringValue, bool isGt = false, bool isSignificanceSheet = false)
        {
            // Set sourceBanner and sourceTableData ranges based on table type (GT or multicross) and source sheet (significanceSheet or not).
            if (isSignificanceSheet)
            {
                if (isGt)
                {
                    // If it's a GT table and a significance sheet.
                    sourceBanner = item.Cells[sourceTableRange.FirstRow + 3, sourceTableRange.FirstColumn + 3, sourceTableRange.FirstRow + 4, sourceTableRange.LastColumn + 1];
                    sourceTableData = item.Cells[sourceTableRange.FirstRow + 5, sourceTableRange.FirstColumn + 1, sourceTableRange.LastRow + 1, sourceTableRange.LastColumn + 1];
                }
                else
                {
                    // If it's a multicross (not GT) table and a significance sheet.
                    sourceBanner = item.Cells[sourceTableRange.FirstRow + 3, sourceTableRange.FirstColumn + 3, sourceTableRange.FirstRow + 6, sourceTableRange.LastColumn + 1];
                    sourceTableData = item.Cells[sourceTableRange.FirstRow + 7, sourceTableRange.FirstColumn + 1, sourceTableRange.LastRow + 1, sourceTableRange.LastColumn + 1];
                }
            }
            else
            {
                if (isGt)
                {
                    // If it's a GT table and not a significance sheet.
                    sourceBanner = item.Cells[sourceTableRange.FirstRow + 3, sourceTableRange.FirstColumn + 3, sourceTableRange.FirstRow + 3, sourceTableRange.LastColumn + 1];
                    sourceTableData = item.Cells[sourceTableRange.FirstRow + 4, sourceTableRange.FirstColumn + 1, sourceTableRange.LastRow + 1, sourceTableRange.LastColumn + 1];
                }
                else
                {
                    // If it's a multicross (not GT) table and not a significance sheet.
                    sourceBanner = item.Cells[sourceTableRange.FirstRow + 3, sourceTableRange.FirstColumn + 3, sourceTableRange.FirstRow + 5, sourceTableRange.LastColumn + 1];
                    sourceTableData = item.Cells[sourceTableRange.FirstRow + 6, sourceTableRange.FirstColumn + 1, sourceTableRange.LastRow + 1, sourceTableRange.LastColumn + 1];
                }
            }

            // Change the filteringValue source range based on whether it's a GT or multicross table.
            if (isGt)
                filteringValue = item.Cells[sourceTableRange.FirstRow + 3, sourceTableRange.FirstColumn + 1];
            else
                filteringValue = item.Cells[sourceTableRange.FirstRow + 5, sourceTableRange.FirstColumn + 1];
        }


        /// <summary>
        /// Compares two ExcelRange objects, 'sourceBanner' and 'prevBanner', to check if their values are different.
        /// </summary>
        /// <param name="sourceBanner">The ExcelRange to compare as the source banner.</param>
        /// <param name="prevBanner">The ExcelRange to compare as the previous banner.</param>
        /// <returns>
        /// Returns 'true' if the values in the 'sourceBanner' and 'prevBanner' are different or if any errors occur during comparison.
        /// Returns 'false' if the values in both ranges are the same.
        /// </returns>
        /// <remarks>
        /// This function checks whether the values in 'sourceBanner' and 'prevBanner' are different.
        /// It handles cases where the ranges are either single cells or multi-cell arrays.
        /// </remarks>
        private static bool CheckDiffrence(ExcelRange sourceBanner, ExcelRange prevBanner)
        {
            // Declare arrays to store cell values.
            Array array1;
            Array array2;
            // Check if both 'sourceBanner' and 'prevBanner' are single cells(1x1).
            if (sourceBanner.Rows == 1 && sourceBanner.Columns == 1 && prevBanner.Rows == 1 && prevBanner.Columns == 1)
            {
                // Compare the values of single cells. If they are equal, return false; otherwise, return true.
                if (sourceBanner.Value == prevBanner.Value)
                {
                    return false;
                }
                return true;
            }
            try
            {
                // Attempt to cast 'sourceBanner' and 'prevBanner' to arrays.
                array1 = (Array)sourceBanner.Value;
                array2 = (Array)prevBanner.Value;
            }
            catch
            {
                // An error occurred during array casting, return true (indicating a difference).
                return true;
            }
            // Check if the array sizes are different. If any dimension differs, return true.
            if (array1.GetLowerBound(0) != array2.GetLowerBound(0) || array1.GetUpperBound(0) != array2.GetUpperBound(0) || array1.GetLowerBound(1) != array2.GetLowerBound(1) || array1.GetUpperBound(1) != array2.GetUpperBound(1))
                return true;

            // Check if the array values are different. If any values differ, return true.
            int i, j;
            for (i = array1.GetLowerBound(0); i <= array1.GetUpperBound(0); i++)
            {

                for (j = array1.GetLowerBound(1); j <= array1.GetUpperBound(1); j++)
                {
                    if (array1.GetValue(i, j) != array2.GetValue(i, j))
                        return true;
                }
            }
            // All values in the arrays are the same, return false (indicating no difference).
            return false;
        }


        /// <summary>
        /// Converts an ARGB hex string to a Color in WPF.
        /// </summary>
        /// <param name="argbHex">The ARGB hex value in the format "#AARRGGBB".</param>
        /// <returns>A Color object representing the ARGB hex value.</returns>
        public static Color HexToColor(string argbHex)
        {
            if (argbHex.StartsWith("#"))
            {
                argbHex = argbHex.TrimStart('#');
            }

            if (argbHex.Length != 8)
            {
                throw new ArgumentException("ARGB hex value should be 8 characters long (e.g., #AARRGGBB).");
            }

            byte a = Convert.ToByte(argbHex.Substring(0, 2), 16);
            byte r = Convert.ToByte(argbHex.Substring(2, 2), 16);
            byte g = Convert.ToByte(argbHex.Substring(4, 2), 16);
            byte b = Convert.ToByte(argbHex.Substring(6, 2), 16);

            return Color.FromArgb(a, r, g, b);
        }


        /// <summary>
        /// Sets a hyperlink in the specified ExcelRange with the provided link URL.
        /// </summary>
        /// <param name="targetCell">The ExcelRange where the hyperlink will be set.</param>
        /// <param name="link">The URL or path for the hyperlink.</param>
        /// <remarks>
        /// This function sets a hyperlink in the specified ExcelRange (cell) with the provided link URL.
        /// It sets the link to be displayed in blue color with underlined text.
        /// </remarks>
        private static void SetHyperlinks(ExcelRange targetCell, string link)
        {
            targetCell.Hyperlink = new Uri(link, UriKind.Relative);
            targetCell.Style.Font.UnderLine = true;
        }


        /// <summary>
        /// Sets the filtering value in the target worksheet at a specified row and column.
        /// </summary>
        /// <param name="row">The row index where the filtering value will be set.</param>
        /// <param name="column">The column index where the filtering value will be set.</param>
        /// <param name="targetWorksheet">The ExcelWorksheet where the filtering value will be set.</param>
        /// <param name="filteringValue">The ExcelRange containing the filtering value.</param>
        /// <param name="targetFilterValue">The reference to the ExcelRange where the filtering value will be assigned.</param>
        private static void SetFilteringValue(int row, int column, ExcelWorksheet targetWorksheet, ExcelRange filteringValue, ref ExcelRange targetFilterValue)
        {
            // Check if the filtering value is not null
            if (!string.IsNullOrEmpty(filteringValue?.Value?.ToString()))
            {
                //select the target cell for FilterValue
                targetFilterValue = targetWorksheet.Cells[row + 1, column];
                // Copy the filteringValue and the styles of that cell to the targetFilterValue cell
                filteringValue.Copy(targetFilterValue);
            }
        }


        /// <summary>
        /// Sets the ranking for a given Excel worksheet based on its name.
        /// </summary>
        /// <param name="targetWorksheet">The target Excel worksheet to set the ranking for.</param>
        /// <param name="targetTable">The Excel range representing the target table within the worksheet.</param>
        private static void SetRanking(ref ExcelWorksheet targetWorksheet, ExcelRange targetTable, CellRangeAddress sourceTableRange)
        {
            // Check if the worksheet name exists in the data map and if the associated data list has items.
            if (_dataMap.ContainsKey(targetWorksheet.Name) && _dataMap[targetWorksheet.Name].Count > 0)
            {
                List<RangeRankingSheetData> dataList = _dataMap[targetWorksheet.Name];
                RangeRankingSheetData rankingData = dataList[0];

                if (rankingData.Range.Equals(sourceTableRange))
                {
                    // Define the range of cells for the  table data within the worksheet for rank marking.
                    ExcelRange tableData = targetWorksheet.Cells[targetTable.Start.Row, targetTable.Start.Column, targetTable.End.Row, targetTable.End.Column];

                    // Add ellipse shapes to the worksheet based on the ranking data.
                    AddEllipseShapesToWorksheet(ref tableData, rankingData.Ranking, ref targetWorksheet);

                    // Remove the processed data from the data list.
                    dataList.RemoveAt(0);
                }
            }
        }


        /// <summary>
        /// Adds ellipse shapes to an Excel worksheet based on a ranking array.
        /// </summary>
        /// <param name="targetTable">The Excel range of target table for positioning the shapes.</param>
        /// <param name="ranking">The ranking array that determines the shape colors and positions.</param>
        /// <param name="targetWorksheet">The target Excel worksheet where the shapes will be added.</param>
        public static void AddEllipseShapesToWorksheet(ref ExcelRange targetTable, Array ranking, ref ExcelWorksheet targetWorksheet)
        {
            int rangeRow = targetTable.Start.Row;
            int rangeCol = targetTable.Start.Column - 2; // =2 :: in excel column C =2  
            int red, green, blue;


            for (int rowNum = ranking.GetLowerBound(0); rowNum <= ranking.GetUpperBound(0); rowNum++)
            {
                for (int col = ranking.GetLowerBound(1); col <= ranking.GetUpperBound(1); col++)
                {
                    switch (Convert.ToInt32(ranking.GetValue(rowNum, col)))
                    {
                        case 1:
                            red = 255;    // Red
                            green = 0;
                            blue = 0;
                            break;
                        case 2:
                            red = 0;
                            green = 0;
                            blue = 255;  // Blue
                            break;
                        case 3:
                            red = 102;
                            green = 153;
                            blue = 51; // Green
                            break;
                        default:
                            continue;// Skip to the next iteration if the value is not 1, 2, or 3.
                    }

                    // Calculate the position and size for the ellipse
                    int rowPosition = (rangeRow + rowNum) - ranking.GetLowerBound(0);
                    int colPosition = rangeCol + col;

                    //for a table containing score of mean value mark the colPosition may become less than 5
                    //so set it to 5 because the marking column starts at 5.
                    int rowOffsetPixels = 4;
                    int colOffsetPixels = 4;
                    int shapeSize = 8; //  size as needed

                    // Add an ellipse shape to the worksheet
                    ExcelShape shape = targetWorksheet.Drawings.AddShape($"Shape 1          {rowPosition}_{colPosition}", eShapeStyle.Ellipse);

                    // Set the position and size of the shape
                    shape.SetPosition(rowPosition - 1, rowOffsetPixels, colPosition, colOffsetPixels);
                    shape.SetSize(shapeSize, shapeSize);

                    // Set the fill color of the shape
                    shape.Fill.Color = Color.FromArgb(255, red, green, blue);
                    // Set the border color and width of the shape
                    shape.Border.Fill.Color = Color.White;
                    shape.Border.Width = 0.50;
                }
            }
        }

    }
}
