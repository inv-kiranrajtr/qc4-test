using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Qc4Launcher.Logic;
using static Macromill.QCWeb.Batch.Report.Tables;
using A = DocumentFormat.OpenXml.Drawing;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using C14 = DocumentFormat.OpenXml.Office2010.Drawing.Charts;

namespace Qc4Launcher.Logic.Cross_Report
{
    /// <summary>
    /// Production GWS column + line combo chart (same structure as the working sample).
    /// Vertical category layout + strRef/numRef with embedded caches + shared axes 100/200.
    /// </summary>
    internal static class GwsComboChartBuilder
    {
        private const uint CategoryAxisId = 100U;
        private const uint ValueAxisId = 200U;
        private const int HiddenDataStartColumn = 200;
        private const int HiddenDataStartRow = 1;

        private sealed class LineSeriesPayload
        {
            public string Label;
            public List<double> Values;
            public string Rgb;
            public int DataColumn;
        }

        /// <summary>
        /// Landscape top chart: report data is horizontal — transpose to vertical hidden table, then build sample-compatible combo.
        /// </summary>
        public static void BuildLandscapeFromReportData(
            WorksheetPart worksheetPart,
            ChartPart chartPart,
            List<int> linesIndexList,
            ref Array v,
            bool hasLines,
            int firstRow,
            int firstCol,
            int lastCol,
            int tableAxisIndex,
            int[] maxAxesCountArray,
            string sheetName)
        {
            int categoryRow = Math.Max(1, firstRow - 1);
            int pointCount = lastCol - firstCol + 1;
            int categoryCol = HiddenDataStartColumn;
            int barCol = categoryCol + 1;
            int dataStartRow = HiddenDataStartRow + 1;
            int dataEndRow = dataStartRow + pointCount - 1;

            var categories = new List<string>(pointCount);
            var barValues = new List<double>(pointCount);
            for (int col = firstCol; col <= lastCol; col++)
            {
                categories.Add(GetCellText(worksheetPart, categoryRow, col));
                barValues.Add(ParseNumber(GetCellText(worksheetPart, firstRow, col)));
            }

            string barLabel = GetCellText(worksheetPart, firstRow, 1);
            if (string.IsNullOrWhiteSpace(barLabel))
                barLabel = "全体";

            var lineSeries = new List<LineSeriesPayload>();
            if (hasLines && linesIndexList != null)
            {
                for (int j = 0; j < linesIndexList.Count; j++)
                {
                    int lineIndex = Convert.ToInt32(linesIndexList[j]);
                    if (!(lineIndex >= 2 || lineIndex <= v.GetUpperBound(0) - 2))
                        continue;

                    int fRow = firstRow - 1 + lineIndex;
                    string label = ResolveLandscapeLineLabel(v, linesIndexList, j, tableAxisIndex, maxAxesCountArray);
                    if (string.IsNullOrEmpty(label))
                        label = "系列" + (j + 1);

                    var values = new List<double>(pointCount);
                    for (int col = firstCol; col <= lastCol; col++)
                        values.Add(ParseNumber(GetCellText(worksheetPart, fRow, col)));

                    lineSeries.Add(new LineSeriesPayload
                    {
                        Label = label,
                        Values = values,
                        Rgb = LineRgb(j),
                        DataColumn = barCol + 1 + lineSeries.Count
                    });
                }
            }

            WriteVerticalTable(worksheetPart, categories, barLabel, barValues, lineSeries, categoryCol, HiddenDataStartRow);
            BuildComboChart(worksheetPart, chartPart, sheetName, dataStartRow, dataEndRow, categoryCol, barCol, barLabel, lineSeries);
        }

        /// <summary>
        /// Portrait side chart: data is already vertical (categories in col 2, bar in firstCol, lines in side-table cols).
        /// </summary>
        public static void BuildPortraitFromReportData(
            WorksheetPart worksheetPart,
            ChartPart chartPart,
            int firstRow,
            int lastRow,
            int firstCol,
            string sheetName,
            bool hasLines,
            List<int> linesIndexList)
        {
            const int categoryCol = 2;
            int barCol = firstCol;
            var lineSeries = new List<LineSeriesPayload>();

            string barLabel = GetCellText(worksheetPart, firstRow - 1, barCol);
            if (string.IsNullOrWhiteSpace(barLabel))
                barLabel = "全体";

            if (hasLines && linesIndexList != null)
            {
                const int sideTableChoiceRow = 28;
                for (int j = 0; j < linesIndexList.Count; j++)
                {
                    int sideTableCol = 16 + linesIndexList[j];
                    string seriesLabel = GetCellText(worksheetPart, sideTableChoiceRow, sideTableCol);
                    if (string.IsNullOrEmpty(seriesLabel))
                        seriesLabel = "系列" + (j + 1);

                    lineSeries.Add(new LineSeriesPayload
                    {
                        Label = seriesLabel,
                        Values = null,
                        Rgb = LineRgb(j),
                        DataColumn = sideTableCol
                    });
                }
            }

            BuildComboChart(worksheetPart, chartPart, sheetName, firstRow, lastRow, categoryCol, barCol, barLabel, lineSeries);
        }

        private static void BuildComboChart(
            WorksheetPart worksheetPart,
            ChartPart chartPart,
            string sheetName,
            int dataStartRow,
            int dataEndRow,
            int categoryCol,
            int barCol,
            string barLabel,
            List<LineSeriesPayload> lineSeries)
        {
            C.ChartSpace chartSpace = new C.ChartSpace();
            chartSpace.AddNamespaceDeclaration("c16r2", "http://schemas.microsoft.com/office/drawing/2015/06/chart");
            chartSpace.Append(new C.Date1904() { Val = false });
            chartSpace.Append(new C.EditingLanguage() { Val = "en-US" });
            chartSpace.Append(new C.RoundedCorners() { Val = false });

            AlternateContent alternateContent = new AlternateContent();
            alternateContent.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            AlternateContentChoice choice = new AlternateContentChoice() { Requires = "c14" };
            choice.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
            choice.Append(new C14.Style() { Val = 102 });
            AlternateContentFallback fallback = new AlternateContentFallback();
            fallback.Append(new C.Style() { Val = 2 });
            alternateContent.Append(choice);
            alternateContent.Append(fallback);
            chartSpace.Append(alternateContent);

            C.Chart chart = new C.Chart();
            chart.Append(new C.AutoTitleDeleted() { Val = true });

            C.PlotArea plotArea = new C.PlotArea();
            plotArea.Append(new C.Layout());

            // Column series
            C.BarChart barChart = new C.BarChart();
            barChart.Append(new C.BarDirection() { Val = C.BarDirectionValues.Column });
            barChart.Append(new C.BarGrouping() { Val = C.BarGroupingValues.Clustered });
            barChart.Append(new C.VaryColors() { Val = false });

            C.BarChartSeries barSeries = new C.BarChartSeries();
            barSeries.Append(new C.Index() { Val = 0U });
            barSeries.Append(new C.Order() { Val = 0U });
            barSeries.Append(CreateSeriesText(barLabel));
            barSeries.Append(ApplyBarFill("D9D9D9", "BFBFBF"));
            barSeries.Append(new C.InvertIfNegative() { Val = false });
            barSeries.Append(DrawingPart.SetStringDataLinkValues(worksheetPart, sheetName, dataStartRow, dataEndRow, categoryCol, categoryCol));
            barSeries.Append(DrawingPart.SetNumericDataLinkValues(worksheetPart, sheetName, dataStartRow, dataEndRow, barCol, barCol));
            barChart.Append(barSeries);

            C.DataLabels barLabels = new C.DataLabels();
            barLabels.Append(new C.ShowLegendKey() { Val = false });
            barLabels.Append(new C.ShowValue() { Val = true });
            barLabels.Append(new C.ShowCategoryName() { Val = false });
            barLabels.Append(new C.ShowSeriesName() { Val = false });
            barLabels.Append(new C.ShowPercent() { Val = false });
            barLabels.Append(new C.ShowBubbleSize() { Val = false });
            barChart.Append(barLabels);
            barChart.Append(new C.GapWidth() { Val = (UInt16Value)80U });
            barChart.Append(new C.AxisId() { Val = CategoryAxisId });
            barChart.Append(new C.AxisId() { Val = ValueAxisId });
            plotArea.Append(barChart);

            // Line series (shared axes = GWS combo)
            if (lineSeries != null && lineSeries.Count > 0)
            {
                C.LineChart lineChart = new C.LineChart();
                lineChart.Append(new C.Grouping() { Val = C.GroupingValues.Standard });
                lineChart.Append(new C.VaryColors() { Val = false });

                for (int j = 0; j < lineSeries.Count; j++)
                {
                    LineSeriesPayload payload = lineSeries[j];
                    C.LineChartSeries lineSer = new C.LineChartSeries();
                    lineSer.Append(new C.Index() { Val = (UInt32Value)(uint)(j + 1) });
                    lineSer.Append(new C.Order() { Val = (UInt32Value)(uint)(j + 1) });
                    lineSer.Append(CreateSeriesText(payload.Label));
                    lineSer.Append(CreateLineStroke(payload.Rgb));

                    C.Marker marker = new C.Marker();
                    marker.Append(new C.Symbol() { Val = C.MarkerStyleValues.Square });
                    marker.Append(new C.Size() { Val = 7 });
                    marker.Append(DrawingPart.SetMarkerProperty(payload.Rgb));
                    lineSer.Append(marker);

                    lineSer.Append(DrawingPart.SetStringDataLinkValues(worksheetPart, sheetName, dataStartRow, dataEndRow, categoryCol, categoryCol));
                    lineSer.Append(DrawingPart.SetNumericDataLinkValues(worksheetPart, sheetName, dataStartRow, dataEndRow, payload.DataColumn, payload.DataColumn));
                    lineSer.Append(new C.Smooth() { Val = false });
                    lineChart.Append(lineSer);
                }

                lineChart.Append(new C.ShowMarker() { Val = true });
                lineChart.Append(new C.Smooth() { Val = false });
                lineChart.Append(new C.AxisId() { Val = CategoryAxisId });
                lineChart.Append(new C.AxisId() { Val = ValueAxisId });
                plotArea.Append(lineChart);
            }

            plotArea.Append(CreateCategoryAxis());
            plotArea.Append(CreateValueAxis());
            chart.Append(plotArea);

            if (lineSeries != null && lineSeries.Count > 0)
            {
                C.Legend legend = new C.Legend();
                legend.Append(new C.LegendPosition() { Val = C.LegendPositionValues.Bottom });
                legend.Append(new C.Overlay() { Val = false });
                chart.Append(legend);
            }

            chart.Append(new C.PlotVisibleOnly() { Val = true });
            chart.Append(new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Zero });
            chart.Append(new C.ShowDataLabelsOverMaximum() { Val = false });

            chartSpace.Append(chart);
            chartSpace.Append(DrawingPart.SetTextPrperty(800));
            chartPart.ChartSpace = chartSpace;
        }

        private static void WriteVerticalTable(
            WorksheetPart worksheetPart,
            List<string> categories,
            string barLabel,
            List<double> barValues,
            List<LineSeriesPayload> lineSeries,
            int categoryCol,
            int headerRow)
        {
            CrossReportHelper.InserStringValue(worksheetPart, "Category", headerRow, categoryCol);
            CrossReportHelper.InserStringValue(worksheetPart, barLabel, headerRow, categoryCol + 1);
            for (int j = 0; j < lineSeries.Count; j++)
                CrossReportHelper.InserStringValue(worksheetPart, lineSeries[j].Label, headerRow, categoryCol + 2 + j);

            for (int i = 0; i < categories.Count; i++)
            {
                int row = headerRow + 1 + i;
                CrossReportHelper.InserStringValue(worksheetPart, categories[i] ?? string.Empty, row, categoryCol);
                CrossReportHelper.InserStringValue(worksheetPart, barValues[i].ToString(CultureInfo.InvariantCulture), row, categoryCol + 1);
                for (int j = 0; j < lineSeries.Count; j++)
                {
                    CrossReportHelper.InserStringValue(
                        worksheetPart,
                        lineSeries[j].Values[i].ToString(CultureInfo.InvariantCulture),
                        row,
                        categoryCol + 2 + j);
                }
            }
        }

        private static C.CategoryAxis CreateCategoryAxis()
        {
            C.CategoryAxis axis = new C.CategoryAxis();
            axis.Append(new C.AxisId() { Val = CategoryAxisId });
            C.Scaling scaling = new C.Scaling();
            scaling.Append(new C.Orientation() { Val = C.OrientationValues.MinMax });
            axis.Append(scaling);
            axis.Append(new C.Delete() { Val = false });
            axis.Append(new C.AxisPosition() { Val = C.AxisPositionValues.Bottom });
            axis.Append(new C.MajorTickMark() { Val = C.TickMarkValues.Outside });
            axis.Append(new C.MinorTickMark() { Val = C.TickMarkValues.None });
            axis.Append(new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo });
            axis.Append(new C.CrossingAxis() { Val = ValueAxisId });
            axis.Append(new C.Crosses() { Val = C.CrossesValues.AutoZero });
            axis.Append(new C.AutoLabeled() { Val = true });
            axis.Append(new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center });
            axis.Append(new C.LabelOffset() { Val = (UInt16Value)100U });
            return axis;
        }

        private static C.ValueAxis CreateValueAxis()
        {
            C.ValueAxis axis = new C.ValueAxis();
            axis.Append(new C.AxisId() { Val = ValueAxisId });
            C.Scaling scaling = new C.Scaling();
            scaling.Append(new C.Orientation() { Val = C.OrientationValues.MinMax });
            scaling.Append(new C.MaxAxisValue() { Val = 100D });
            scaling.Append(new C.MinAxisValue() { Val = 0D });
            axis.Append(scaling);
            axis.Append(new C.Delete() { Val = false });
            axis.Append(new C.AxisPosition() { Val = C.AxisPositionValues.Left });
            axis.Append(new C.MajorGridlines());
            // Literal %" — cell values are already 0–100
            axis.Append(new C.NumberingFormat() { FormatCode = "0\"%\"", SourceLinked = false });
            axis.Append(new C.MajorTickMark() { Val = C.TickMarkValues.Outside });
            axis.Append(new C.MinorTickMark() { Val = C.TickMarkValues.None });
            axis.Append(new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo });
            axis.Append(new C.CrossingAxis() { Val = CategoryAxisId });
            axis.Append(new C.Crosses() { Val = C.CrossesValues.AutoZero });
            axis.Append(new C.CrossBetween() { Val = C.CrossBetweenValues.Between });
            axis.Append(new C.MajorUnit() { Val = 20D });
            return axis;
        }

        private static C.SeriesText CreateSeriesText(string text)
        {
            C.SeriesText seriesText = new C.SeriesText();
            seriesText.Append(new C.NumericValue(text ?? string.Empty));
            return seriesText;
        }

        private static C.ChartShapeProperties ApplyBarFill(string innerClr, string outlineClr)
        {
            C.ChartShapeProperties props = new C.ChartShapeProperties();
            A.SolidFill fill = new A.SolidFill();
            fill.Append(new A.RgbColorModelHex() { Val = innerClr });
            A.Outline outline = new A.Outline() { Width = 12700 };
            A.SolidFill outlineFill = new A.SolidFill();
            outlineFill.Append(new A.RgbColorModelHex() { Val = outlineClr });
            outline.Append(outlineFill);
            props.Append(fill);
            props.Append(outline);
            return props;
        }

        private static C.ChartShapeProperties CreateLineStroke(string rgb)
        {
            C.ChartShapeProperties props = new C.ChartShapeProperties();
            A.Outline outline = new A.Outline() { Width = 25400 };
            A.SolidFill fill = new A.SolidFill();
            fill.Append(new A.RgbColorModelHex() { Val = rgb });
            outline.Append(fill);
            outline.Append(new A.PresetDash() { Val = A.PresetLineDashValues.Solid });
            props.Append(outline);
            return props;
        }

        private static string LineRgb(int index)
        {
            var clr = System.Drawing.Color.FromArgb(
                ColorPallet.colorIndex[ColorPallet.colorLineIndex[index % ColorPallet.colorLineIndex.Length]]);
            return clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
        }

        private static string ResolveLandscapeLineLabel(Array v, List<int> linesIndexList, int j, int tableAxisIndex, int[] maxAxesCountArray)
        {
            int x = Convert.ToInt32(linesIndexList[j]) + 1 + 1;
            string[] tmpBuf;
            if (maxAxesCountArray[tableAxisIndex] == 2)
            {
                if (v.GetValue(x, 3) != null)
                {
                    tmpBuf = new string[1];
                    tmpBuf[0] = Convert.ToString(v.GetValue(x, 2));
                }
                else
                {
                    tmpBuf = new string[2];
                    tmpBuf[1] = Convert.ToString(v.GetValue(x, 3));
                    for (; x >= 1 + 1 + 1; x--)
                    {
                        if (v.GetValue(x, 2) != null)
                        {
                            tmpBuf[0] = Convert.ToString(v.GetValue(x, 2));
                            break;
                        }
                    }
                }
            }
            else
            {
                tmpBuf = new string[1];
                tmpBuf[0] = Convert.ToString(v.GetValue(x, 2));
            }

            return OutputUtil.RemoveLeadingSpclChar(string.Join(" - ", tmpBuf.Where(s => !string.IsNullOrEmpty(s))));
        }

        private static string GetCellText(WorksheetPart worksheetPart, int rowIdx, int colIdx)
        {
            return DrawingPart.GetChartCellTextPublic(worksheetPart, rowIdx, colIdx);
        }

        private static double ParseNumber(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return 0D;
            double value;
            if (double.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                return value;
            if (double.TryParse(raw, out value))
                return value;
            return 0D;
        }
    }
}
