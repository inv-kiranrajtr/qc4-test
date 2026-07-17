using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Packaging;
using Qc4Launcher.Logic;
using Qc4Launcher.Util;
using static Macromill.QCWeb.Batch.Report.Tables;
using A = DocumentFormat.OpenXml.Drawing;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using C14 = DocumentFormat.OpenXml.Office2010.Drawing.Charts;

namespace Qc4Launcher.Logic.Cross_Report
{
    /// <summary>
    /// Google Sheets combo chart: vertical hidden data + strRef/numRef with embedded caches (sample-compatible).
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
        }

        public static void BuildLandscapeColumnLineChart(
            WorksheetPart worksheetPart,
            CrossTable tmpTable,
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
            int dataStartRow = HiddenDataStartRow + 1;
            int dataEndRow = dataStartRow + pointCount - 1;
            int categoryCol = HiddenDataStartColumn;
            int barCol = categoryCol + 1;

            var categories = new List<string>(pointCount);
            var barValues = new List<double>(pointCount);
            for (int col = firstCol; col <= lastCol; col++)
            {
                categories.Add(DrawingPart.GetChartCellTextPublic(worksheetPart, categoryRow, col));
                barValues.Add(ParseNumber(DrawingPart.GetChartCellTextPublic(worksheetPart, firstRow, col)));
            }

            string barLabel = DrawingPart.GetChartCellTextPublic(worksheetPart, firstRow, 1);
            if (string.IsNullOrWhiteSpace(barLabel))
                barLabel = DrawingPart.GetChartCellTextPublic(worksheetPart, categoryRow, firstCol);
            if (string.IsNullOrWhiteSpace(barLabel))
                barLabel = "全体";

            var lineSeries = new List<LineSeriesPayload>();
            if (hasLines && linesIndexList != null)
            {
                for (int j = 0; j < linesIndexList.Count; j++)
                {
                    int lineIndex = Convert.ToInt32(linesIndexList[j]);
                    if (!(lineIndex >= 2 || lineIndex <= v.GetUpperBound(0) - (1 + 1)))
                        continue;

                    int fRow = firstRow - 1 + lineIndex;
                    string label = ResolveLineLabel(v, linesIndexList, j, tableAxisIndex, maxAxesCountArray);
                    if (string.IsNullOrEmpty(label))
                        label = "系列" + (j + 1);

                    var values = new List<double>(pointCount);
                    for (int col = firstCol; col <= lastCol; col++)
                        values.Add(ParseNumber(DrawingPart.GetChartCellTextPublic(worksheetPart, fRow, col)));

                    var clr = System.Drawing.Color.FromArgb(
                        ColorPallet.colorIndex[ColorPallet.colorLineIndex[j % ColorPallet.colorLineIndex.Length]]);
                    // TEMP VERIFY: force neon line colors so overlay is unmistakable in GWS.
                    string rgb = (j == 0) ? "00E5FF" : (j == 1) ? "39FF14" : (j == 2) ? "FFD600" :
                        clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                    lineSeries.Add(new LineSeriesPayload { Label = "[GWS]" + label, Values = values, Rgb = rgb });
                }
            }

            WriteHiddenChartData(worksheetPart, categories, barLabel, barValues, lineSeries, categoryCol, HiddenDataStartRow);

            // TEMP VERIFY markers — remove after GWS confirms this is the correct top chart.
            const string verifyTag = "GWS-COMBO-BUILDER-v3";
            string verifyTitle = verifyTag + " bars=" + pointCount + " lines=" + lineSeries.Count
                + " hasLines=" + hasLines
                + (linesIndexList == null ? " idx=null" : " idx=" + linesIndexList.Count);
            int markerRow = Math.Max(1, firstRow - 4);
            int markerCol = Math.Max(1, firstCol - 1);
            CrossReportHelper.InserStringValue(worksheetPart, verifyTitle, markerRow, markerCol);
            System.Diagnostics.Debug.WriteLine("[GwsComboChartBuilder] " + verifyTitle + " sheet=" + sheetName);

            C.ChartSpace chartSpace = CreateChartSpaceShell();
            C.Chart chart = new C.Chart();
            chart.Append(CreateVerifyChartTitle(verifyTitle));
            chart.Append(new C.AutoTitleDeleted() { Val = false });

            C.PlotArea plotArea = new C.PlotArea();
            plotArea.Append(new C.Layout());

            C.BarChart barChart = new C.BarChart();
            barChart.Append(new C.BarDirection() { Val = C.BarDirectionValues.Column });
            barChart.Append(new C.BarGrouping() { Val = C.BarGroupingValues.Clustered });
            barChart.Append(new C.VaryColors() { Val = false });

            C.BarChartSeries barSeries = new C.BarChartSeries();
            barSeries.Append(new C.Index() { Val = 0U });
            barSeries.Append(new C.Order() { Val = 0U });
            // Bright magenta bars = unmistakable proof this builder ran (revert after verify).
            barSeries.Append(CreateCachedSeriesText(sheetName, HiddenDataStartRow, barCol, "★" + barLabel + "★"));
            barSeries.Append(ApplyBarFill("FF00AA", "000000"));
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
            barChart.Append(new C.GapWidth() { Val = (UInt16Value)100U });
            barChart.Append(new C.AxisId() { Val = CategoryAxisId });
            barChart.Append(new C.AxisId() { Val = ValueAxisId });
            plotArea.Append(barChart);

            if (lineSeries.Count > 0)
            {
                C.LineChart lineChart = new C.LineChart();
                lineChart.Append(new C.Grouping() { Val = C.GroupingValues.Standard });
                lineChart.Append(new C.VaryColors() { Val = false });

                for (int j = 0; j < lineSeries.Count; j++)
                {
                    LineSeriesPayload payload = lineSeries[j];
                    int lineCol = barCol + 1 + j;

                    C.LineChartSeries lineSer = new C.LineChartSeries();
                    lineSer.Append(new C.Index() { Val = (UInt32Value)(uint)(j + 1) });
                    lineSer.Append(new C.Order() { Val = (UInt32Value)(uint)(j + 1) });
                    lineSer.Append(CreateCachedSeriesText(sheetName, HiddenDataStartRow, lineCol, payload.Label));
                    lineSer.Append(CreateLineStroke(payload.Rgb));
                    C.Marker marker = new C.Marker();
                    marker.Append(new C.Symbol() { Val = C.MarkerStyleValues.Square });
                    marker.Append(new C.Size() { Val = 7 });
                    marker.Append(DrawingPart.SetMarkerProperty(payload.Rgb));
                    lineSer.Append(marker);
                    lineSer.Append(DrawingPart.SetStringDataLinkValues(worksheetPart, sheetName, dataStartRow, dataEndRow, categoryCol, categoryCol));
                    lineSer.Append(DrawingPart.SetNumericDataLinkValues(worksheetPart, sheetName, dataStartRow, dataEndRow, lineCol, lineCol));
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

            if (lineSeries.Count > 0)
            {
                C.Legend legend = new C.Legend();
                legend.Append(new C.LegendPosition() { Val = C.LegendPositionValues.Bottom });
                legend.Append(new C.Overlay() { Val = false });
                chart.Append(legend);
            }

            chart.Append(new C.PlotVisibleOnly() { Val = true });
            chart.Append(new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap });
            chart.Append(new C.ShowDataLabelsOverMaximum() { Val = false });

            chartSpace.Append(chart);
            chartSpace.Append(CreateChartAreaShape());
            chartSpace.Append(DrawingPart.SetTextPrperty(800));
            chartPart.ChartSpace = chartSpace;
        }

        private static C.Title CreateVerifyChartTitle(string text)
        {
            C.Title title = new C.Title();
            C.ChartText chartText = new C.ChartText();
            C.RichText richText = new C.RichText();
            richText.Append(new A.BodyProperties());
            richText.Append(new A.ListStyle());
            A.Paragraph paragraph = new A.Paragraph();
            A.ParagraphProperties paragraphProperties = new A.ParagraphProperties();
            paragraphProperties.Append(new A.DefaultRunProperties() { FontSize = 1400 });
            paragraph.Append(paragraphProperties);
            A.Run run = new A.Run();
            run.Append(new A.RunProperties() { Language = "en-US", FontSize = 1400, Bold = true });
            run.Append(new A.Text(text));
            paragraph.Append(run);
            richText.Append(paragraph);
            chartText.Append(richText);
            title.Append(chartText);
            title.Append(new C.Overlay() { Val = false });
            return title;
        }

        private static C.ChartSpace CreateChartSpaceShell()
        {
            C.ChartSpace chartSpace = new C.ChartSpace();
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
            return chartSpace;
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
            axis.Append(new C.NumberingFormat() { FormatCode = "0", SourceLinked = false });
            axis.Append(new C.MajorTickMark() { Val = C.TickMarkValues.Outside });
            axis.Append(new C.MinorTickMark() { Val = C.TickMarkValues.None });
            axis.Append(new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo });
            axis.Append(new C.CrossingAxis() { Val = CategoryAxisId });
            axis.Append(new C.Crosses() { Val = C.CrossesValues.AutoZero });
            axis.Append(new C.CrossBetween() { Val = C.CrossBetweenValues.Between });
            axis.Append(new C.MajorUnit() { Val = 20D });
            return axis;
        }

        private static C.SeriesText CreateCachedSeriesText(string sheetName, int row, int col, string cachedText)
        {
            C.SeriesText seriesText = new C.SeriesText();
            C.StringReference stringReference = new C.StringReference();
            C.Formula formula = new C.Formula();
            formula.Text = "'" + sheetName + "'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(col) + "$" + row;
            stringReference.Append(formula);

            C.StringCache stringCache = new C.StringCache();
            stringCache.Append(new C.PointCount() { Val = 1U });
            C.StringPoint stringPoint = new C.StringPoint() { Index = 0U };
            stringPoint.Append(new C.NumericValue(cachedText ?? string.Empty));
            stringCache.Append(stringPoint);
            stringReference.Append(stringCache);

            seriesText.Append(stringReference);
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
            A.Outline outline = new A.Outline() { Width = 25000 };
            A.SolidFill fill = new A.SolidFill();
            fill.Append(new A.RgbColorModelHex() { Val = rgb });
            outline.Append(fill);
            props.Append(outline);
            return props;
        }

        private static C.ShapeProperties CreateChartAreaShape()
        {
            C.ShapeProperties shape = new C.ShapeProperties();
            shape.Append(new A.NoFill());
            A.Outline outline = new A.Outline() { Width = 9525 };
            outline.Append(new A.NoFill());
            shape.Append(outline);
            return shape;
        }

        private static void WriteHiddenChartData(
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
                CrossReportHelper.InserStringValue(worksheetPart, categories[i], row, categoryCol);
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

        private static string ResolveLineLabel(Array v, List<int> linesIndexList, int j, int tableAxisIndex, int[] maxAxesCountArray)
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
