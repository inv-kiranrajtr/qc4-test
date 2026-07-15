using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.VisualBasic;
using static Macromill.QCWeb.Batch.Report.Tables;
using DocumentFormat.OpenXml.Spreadsheet;
using A14 = DocumentFormat.OpenXml.Office2010.Drawing;
using static Macromill.QCWeb.Common.Constants;
using Qc4Launcher.Util;
using Constants = Microsoft.VisualBasic.Constants;

namespace Qc4Launcher.Logic.Gross_Tabulation.Openxml
{
    public class DrawingPart
    {
        private const double PlotLeftX = 0.08;
        private const double PlotFullWidth = 0.90;
        private const double PlotPercentStackedWidth = 0.86;

        public void GenerateDrawingsPart(DrawingsPart drawingsPart, string fromRow, string toRow, string id)
        {
            Xdr.WorksheetDrawing worksheetDrawing = null;

            // Google Sheets requires editAs="twoCell" with non-zero frame extents (Absolute + 0x0 fails open)
            Xdr.TwoCellAnchor twoCellAnchor = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.TwoCell };

            Xdr.FromMarker fromMarker2 = new Xdr.FromMarker();
            Xdr.ColumnId columnId3 = new Xdr.ColumnId();
            columnId3.Text = "1";
            Xdr.ColumnOffset columnOffset3 = new Xdr.ColumnOffset();
            columnOffset3.Text = "0";
            Xdr.RowId rowId3 = new Xdr.RowId();
            rowId3.Text = fromRow;
            Xdr.RowOffset rowOffset3 = new Xdr.RowOffset();
            rowOffset3.Text = "0";

            fromMarker2.Append(columnId3);
            fromMarker2.Append(columnOffset3);
            fromMarker2.Append(rowId3);
            fromMarker2.Append(rowOffset3);

            Xdr.ToMarker toMarker2 = new Xdr.ToMarker();
            Xdr.ColumnId columnId4 = new Xdr.ColumnId();
            columnId4.Text = "2";
            Xdr.ColumnOffset columnOffset4 = new Xdr.ColumnOffset();
            columnOffset4.Text = "0";
            Xdr.RowId rowId4 = new Xdr.RowId();
            rowId4.Text = toRow;
            Xdr.RowOffset rowOffset4 = new Xdr.RowOffset();
            rowOffset4.Text = "0";

            toMarker2.Append(columnId4);
            toMarker2.Append(columnOffset4);
            toMarker2.Append(rowId4);
            toMarker2.Append(rowOffset4);

            // Do not set Macro="" — empty macro + a16:creationId both appear in failing QC4 draws; QCSV omits them
            Xdr.GraphicFrame graphicFrame1 = new Xdr.GraphicFrame();

            Xdr.NonVisualGraphicFrameProperties nonVisualGraphicFrameProperties1 = new Xdr.NonVisualGraphicFrameProperties();

            uint shapeId = 2U;
            string chartName = "Chart 1";
            if (!string.IsNullOrEmpty(id) && id.StartsWith("rId", StringComparison.OrdinalIgnoreCase)
                && uint.TryParse(id.Substring(3), out uint chartNum))
            {
                shapeId = chartNum;
                chartName = "Chart " + chartNum;
            }
            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties2 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)shapeId, Name = chartName };
            Xdr.NonVisualGraphicFrameDrawingProperties nonVisualGraphicFrameDrawingProperties1 = new Xdr.NonVisualGraphicFrameDrawingProperties();

            nonVisualGraphicFrameProperties1.Append(nonVisualDrawingProperties2);
            nonVisualGraphicFrameProperties1.Append(nonVisualGraphicFrameDrawingProperties1);

            Xdr.Transform transform1 = new Xdr.Transform();
            A.Offset offset2 = new A.Offset() { X = 100L, Y = 100L };
            A.Extents extents2 = new A.Extents() { Cx = 8534400L, Cy = 4572000L };

            transform1.Append(offset2);
            transform1.Append(extents2);

            A.Graphic graphic1 = new A.Graphic();

            A.GraphicData graphicData1 = new A.GraphicData() { Uri = "http://schemas.openxmlformats.org/drawingml/2006/chart" };

            C.ChartReference chartReference1 = new C.ChartReference() { Id = id };

            graphicData1.Append(chartReference1);

            graphic1.Append(graphicData1);

            graphicFrame1.Append(nonVisualGraphicFrameProperties1);
            graphicFrame1.Append(transform1);
            graphicFrame1.Append(graphic1);
            Xdr.ClientData clientData2 = new Xdr.ClientData();

            twoCellAnchor.Append(fromMarker2);
            twoCellAnchor.Append(toMarker2);
            twoCellAnchor.Append(graphicFrame1);
            twoCellAnchor.Append(clientData2);

            if (drawingsPart.WorksheetDrawing == null)
            {
                worksheetDrawing = new Xdr.WorksheetDrawing();
                worksheetDrawing.Append(twoCellAnchor);

                drawingsPart.WorksheetDrawing = worksheetDrawing;
            }
            else
            {
                drawingsPart.WorksheetDrawing.Append(twoCellAnchor);
            }

        }
        public ChartPart GeneratePieChart(WorksheetPart worksheetPart, ChartPart chartPart1,double HideChartDescriptionMaxPercent, int perStartRow, int endRow, int perFirstCol, int PerLastCol, string lineColour, string perSheetName, string Title,
                                           string SubTitle, string num, string NumberFormat, GTTable Table, long GapWidth, bool isMatrix = false, bool isQCM = false)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val =  QC4Common.Common.Constants.GlobalMode.Split(',')[0] };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            //Add text for Chart Area 
            C.Chart chart1 = new C.Chart();

            C.Title title1 = new C.Title();

            C.ChartText chartText1 = new C.ChartText();

            C.RichText richText1 = new C.RichText();
            A.BodyProperties bodyProperties2 = new A.BodyProperties();
            A.ListStyle listStyle2 = new A.ListStyle();

            richText1.Append(bodyProperties2);
            richText1.Append(listStyle2);
            richText1.Append(SetChartTitle(Title, SubTitle, num, 1000));

            chartText1.Append(richText1);

            C.Overlay overlay1 = new C.Overlay() { Val = false };

            title1.Append(chartText1);
            title1.Append(SetChartAreaTextPosition());
            title1.Append(overlay1);

            //Chart Position
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();
            C.Layout layout2 = new C.Layout();

            C.ManualLayout manualLayout2 = new C.ManualLayout();
            C.LayoutTarget layoutTarget1 = new C.LayoutTarget() { Val = C.LayoutTargetValues.Inner };
            C.LeftMode leftMode2 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode2 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left2 = new C.Left() { Val = 0.33911978654035435D };
            C.Top top2 = new C.Top() { Val = 0.27114615959071303D };
            C.Width width1 = new C.Width() { Val = 0.32176042691929135D };
            C.Height height1 = new C.Height() { Val = 0.55968389491400738D };

            manualLayout2.Append(layoutTarget1);
            manualLayout2.Append(leftMode2);
            manualLayout2.Append(topMode2);
            manualLayout2.Append(left2);
            manualLayout2.Append(top2);
            manualLayout2.Append(width1);
            manualLayout2.Append(height1);

            layout2.Append(manualLayout2);

            //Create Pie Chart
            C.PieChart pieChart1 = new C.PieChart();
            C.VaryColors varyColors1 = new C.VaryColors() { Val = true };

            C.PieChartSeries pieChartSeries1 = new C.PieChartSeries();
            C.Index index1 = new C.Index() { Val = (UInt32Value)0U };
            C.Order order1 = new C.Order() { Val = (UInt32Value)0U };

            pieChartSeries1.Append(index1);
            pieChartSeries1.Append(order1);
            pieChartSeries1.Append(ApplyFillColour(lineColour));
            C.DataLabels dataLabels = new C.DataLabels();
            int flag = 0;

            if (isMatrix && isQCM)
            {
                int i = 1, indexer = 0;
                int fCol = perFirstCol;
                int lCol = PerLastCol - Table.Question.SubTotalCnt;
                Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)endRow);

                while (fCol <= lCol)
                {
                    C.DataPoint dataPoint1 = new C.DataPoint();
                    C.Index index2 = new C.Index() { Val = (uint)indexer };
                    C.Bubble3D bubble3D1 = new C.Bubble3D() { Val = false };
                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                    dataPoint1.Append(index2);
                    dataPoint1.Append(bubble3D1);
                    dataPoint1.Append(ApplyFillColour(lineColour, rgb));
                    pieChartSeries1.Append(dataPoint1);
                    var value = OpenXmlHelper.GetCell(r, endRow, fCol).CellValue.InnerText;
                    if (OpenXmlHelper.GetCell(r, endRow, fCol).CellValue.InnerText == "0")
                    {
                        C.DataLabel dataLabel = new C.DataLabel();
                        C.Index index = new C.Index() { Val = (uint)indexer };
                        C.Delete delete = new C.Delete() { Val = true };
                        dataLabel.Append(index);
                        dataLabel.Append(delete);
                        dataLabels.Append(dataLabel);
                    }
                    else
                    {
                        C.DataLabel dataLabel = new C.DataLabel();
                        C.Index index = new C.Index() { Val = (uint)indexer };
                        dataLabel.Append(index);
                        // Leader lines are unsupported / cause open errors in Google Sheets
                        bool showCategoryName = true, showLeaderLines = false; bool showValue = true;
                        double pasrsedValue = 0;
                        bool isDouble = Double.TryParse(value, out pasrsedValue);
                        if (isDouble && (pasrsedValue <= HideChartDescriptionMaxPercent))
                        {
                            showValue = false;
                            showCategoryName = false;
                        }
                        dataLabels.Append(ApplyDataLabel(showCategoryName, showValue, showLeaderLines, NumberFormat, dataLabel));
                        flag = 1;
                    }
                    indexer++; i++;
                    fCol++;
                }
                if (flag == 0) { dataLabels = null; }
                //bool showCategoryName = true, showLeaderLines = false;
                //pieChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines, NumberFormat));//Apply Labels
                if (dataLabels != null)
                    pieChartSeries1.Append(dataLabels);
                pieChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, perStartRow + 1, perStartRow + 1, lCol, perFirstCol));
                pieChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, lCol, endRow, endRow, perFirstCol));
            }
            else
            {
                int sRow = isMatrix ? (endRow - (Table.ChildQuestionsCount - 1)) : perStartRow;
                int rowCount = sRow; int i = 1, indexer = 0;
                while (rowCount <= endRow)
                {

                    C.DataPoint dataPoint1 = new C.DataPoint();
                    C.Index index2 = new C.Index() { Val = (uint)indexer };
                    C.Bubble3D bubble3D1 = new C.Bubble3D() { Val = false };
                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                    dataPoint1.Append(index2);
                    dataPoint1.Append(bubble3D1);
                    dataPoint1.Append(ApplyFillColour(lineColour, rgb));
                    pieChartSeries1.Append(dataPoint1);
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet,(uint)rowCount);
                    var value = OpenXmlHelper.GetCell(r, rowCount, PerLastCol).CellValue.InnerText;
                    if (OpenXmlHelper.GetCell(r, rowCount, PerLastCol).CellValue.InnerText == "0")
                    {
                        C.DataLabel dataLabel = new C.DataLabel();
                        C.Index index = new C.Index() { Val = (uint)indexer };
                        C.Delete delete = new C.Delete() { Val = true };
                        dataLabel.Append(index);
                        dataLabel.Append(delete);
                        dataLabels.Append(dataLabel);
                    }
                    else
                    {
                        C.DataLabel dataLabel = new C.DataLabel();
                        C.Index index = new C.Index() { Val = (uint)indexer };
                        dataLabel.Append(index);
                        // Leader lines are unsupported / cause open errors in Google Sheets
                        bool showCategoryName = true, showLeaderLines = false; bool showValue = true;
                        double parsedValue = 0;
                        bool isDouble = Double.TryParse(value, out parsedValue);
                        if (isDouble && (parsedValue <= HideChartDescriptionMaxPercent))
                        {
                            showValue = false;
                            showCategoryName = false;
                        }
                        dataLabels.Append(ApplyDataLabel(showCategoryName, showValue, showLeaderLines, NumberFormat, dataLabel));
                        flag = 1;
                    }
                    indexer++; i++;
                    rowCount++;
                }
                if (flag == 0) { dataLabels = null; }
                // bool showCategoryName = true, showLeaderLines = false;
                // pieChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines, NumberFormat, dataLabels));//Apply Labels
                if (dataLabels != null)
                    pieChartSeries1.Append(dataLabels);
                pieChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, sRow, endRow, perFirstCol + 1, perFirstCol + 1));
                pieChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, PerLastCol, sRow, endRow, PerLastCol));
            }
            pieChart1.Append(varyColors1);
            pieChart1.Append(pieChartSeries1);

            C.ShapeProperties shapeProperties2 = new C.ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            A.Outline outline9 = new A.Outline() { Width = 25400 };
            A.NoFill noFill4 = new A.NoFill();

            outline9.Append(noFill4);

            shapeProperties2.Append(noFill3);
            shapeProperties2.Append(outline9);

            plotArea1.Append(layout2);
            plotArea1.Append(pieChart1);
            plotArea1.Append(shapeProperties2);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };

            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(title1);
            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(chart1);
            chartSpace1.Append(SetChartAreaLineColour(lineColour));
            chartSpace1.Append(SetTextPrperty(1000));
            //chartSpace1.Append(SetPrintSetting());

            chartPart1.ChartSpace = chartSpace1;
            return chartPart1;
        }
        public ChartPart GenerateBarClusteredChart(WorksheetPart worksheetPart, ChartPart chartPart1, int perStartRow, int endRow, int perFirstCol, int PerLastCol, string lineColour, string perSheetName, string Title,
                                                    string SubTitle, string num, string ValueFormat, GTTable Table, long GapWidth, string ChartType, bool isMatrix, bool isQCM = false, bool isNumeric = false)
        {
            int sRow = perStartRow;
            int fCol = perFirstCol + 1;
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = QC4Common.Common.Constants.GlobalMode.Split(',')[0] };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            C.Chart chart1 = new C.Chart();

            C.Title title1 = new C.Title();

            C.ChartText chartText1 = new C.ChartText();

            C.RichText richText1 = new C.RichText();
            A.BodyProperties bodyProperties1 = new A.BodyProperties();
            A.ListStyle listStyle1 = new A.ListStyle();

            richText1.Append(bodyProperties1);
            richText1.Append(listStyle1);
            richText1.Append(SetChartTitle(Title, SubTitle, num, 1000));
            chartText1.Append(richText1);
            C.Overlay overlay1 = new C.Overlay() { Val = false };
            title1.Append(chartText1);
            title1.Append(SetChartAreaTextPosition());
            title1.Append(overlay1);
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };
            C.PlotArea plotArea1 = new C.PlotArea();
            C.Layout layout2 = CreateAlignedPlotAreaLayout(0.16, 0.78);

            //Create BarChart
            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Bar };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Clustered };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            C.BarChartSeries barChartSeries1 = null;
            C.Index index1 = null;
            C.Order order1 = null;

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);

            int row = 0, subTotalCnt = 0, col = 0, indexer = 0, i = 1;
            string rgb;

            if ((isMatrix && !isQCM && !isNumeric))
            {
                col = perFirstCol;
                row = endRow - Table.ChildQuestionsCount + 1;
                while (col <= PerLastCol)
                {
                    barChartSeries1 = new C.BarChartSeries();
                    index1 = new C.Index() { Val = (uint)indexer };
                    order1 = new C.Order() { Val = (uint)indexer };
                    barChartSeries1.Append(index1);
                    barChartSeries1.Append(order1);

                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)]);
                    rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                    // OOXML series order: idx, order, tx, spPr, invertIfNegative, dLbls, cat, val
                    barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, perStartRow + 1, col));
                    barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                    C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
                    bool showCategoryName = false, showLeaderLines = false;
                    barChartSeries1.Append(invertIfNegative1);
                    barChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines));
                    barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, row, endRow, 3, 3));
                    barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, col, row, endRow, col));
                    barChart1.Append(barChartSeries1);
                    col++; i++; indexer++;
                }
            }
            else if ((isMatrix && isNumeric))
            {
                barChartSeries1 = new C.BarChartSeries();
                index1 = new C.Index() { Val = (uint)indexer };
                order1 = new C.Order() { Val = (uint)indexer };
                barChartSeries1.Append(index1);
                barChartSeries1.Append(order1);

                var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((1 - 1) % Table.Chart.SeriesCount)]);
                rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                // OOXML series order: idx, order, tx, spPr, invertIfNegative, dLbls, cat, val
                barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, perStartRow + 1, PerLastCol));
                barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
                bool showCategoryName = false, showLeaderLines = false;
                barChartSeries1.Append(invertIfNegative1);
                barChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines));
                barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, perStartRow + 2, endRow, perFirstCol + 1, perFirstCol + 1));
                barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, PerLastCol, perStartRow + 2, endRow, PerLastCol));
                barChart1.Append(barChartSeries1);
            }
            else
            {
                barChartSeries1 = new C.BarChartSeries();
                index1 = new C.Index() { Val = (UInt32Value)0U };
                order1 = new C.Order() { Val = (UInt32Value)0U };

                barChartSeries1.Append(index1);
                barChartSeries1.Append(order1);
                // Series title required by Google Sheets (matches QCSV chart XML)
                barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, perStartRow + 1, PerLastCol));

                int clusterChartColor = Convert.ToInt32(Util.Constants.GTGraphColorIndex.WIDTH_STICK_M);
                if (Table.Chart.SeriesColorIndex((1 - 1) % Table.Chart.SeriesCount) == clusterChartColor &&
                                          (ChartType == "xlBarClustered" || ChartType == "xlColumnClustered") &&
                                          Table.Question.SubTotalCnt > 0)
                {
                    if (isQCM)
                    {
                        col = perFirstCol;
                        row = endRow;
                        subTotalCnt = PerLastCol - Table.Question.SubTotalCnt;
                        while (col <= PerLastCol)
                        {
                            if (col > subTotalCnt)
                                rgb = "E4DFEC";
                            else
                                rgb = "F2F2F2";

                            barChartSeries1.Append(ApplyFillXlClustered(lineColour, rgb, indexer));
                            indexer++;
                            col++;
                        }
                    }
                    else
                    {
                        col = PerLastCol;
                        row = sRow;
                        subTotalCnt = endRow - Table.Question.SubTotalCnt;
                        while (row <= endRow)
                        {
                            if (row > subTotalCnt)
                                rgb = "E4DFEC";
                            else
                                rgb = "F2F2F2";

                            barChartSeries1.Append(ApplyFillXlClustered(lineColour, rgb, indexer));
                            indexer++;
                            row++;
                        }
                    }
                }
                else
                {
                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((1 - 1) % Table.Chart.SeriesCount)]);
                    rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                    barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                }

                C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };

                bool showCategoryName = false, showLeaderLines = false;
                barChartSeries1.Append(invertIfNegative1);
                barChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines));

                if (isQCM)
                {
                    barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, perStartRow + 1, perStartRow + 1, PerLastCol, perFirstCol));
                    barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, PerLastCol, endRow, endRow, perFirstCol));
                }
                else
                {
                    barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, sRow, endRow, fCol, fCol));
                    barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, PerLastCol, sRow, endRow, PerLastCol));
                }
                barChart1.Append(barChartSeries1);
            }
            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)GapWidth };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)558956272U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)558959224U };

            barChart1.Append(gapWidth1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)558956272U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MaxMin };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.NumberingFormat numberingFormat1 = new C.NumberingFormat() { FormatCode = "General", SourceLinked = true };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };

            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)558959224U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId3);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(numberingFormat1);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(ApplyFillColour(lineColour));
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);
            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)558959224U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 100D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            if (!isNumeric)
            {
                scaling2.Append(maxAxisValue1);
                scaling2.Append(minAxisValue1);
            }

            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Top };

            C.NumberingFormat numberingFormat2 = new C.NumberingFormat() { FormatCode = ValueFormat, SourceLinked = false };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };

            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)558956272U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 0.2 * 100D };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(MajorGuidLineColour(lineColour));
            valueAxis1.Append(numberingFormat2);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(ApplyFillColour(lineColour));
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            if (!isNumeric)
                valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            A.Outline outline9 = new A.Outline() { Width = 25400 };
            A.NoFill noFill4 = new A.NoFill();

            outline9.Append(noFill4);

            shapeProperties1.Append(noFill3);
            shapeProperties1.Append(outline9);

            plotArea1.Append(layout2);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };
            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(title1);
            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            if ((isMatrix && !isQCM && !isNumeric))
                chart1.Append(SetLegend());
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(chart1);
            chartSpace1.Append(SetChartAreaLineColour(lineColour));
            chartSpace1.Append(SetTextPrperty(800));
            //chartSpace1.Append(SetPrintSetting());

            chartPart1.ChartSpace = chartSpace1;
            return chartPart1;
        }
        public void GenerateColumnClusteredChart(WorksheetPart worksheetPart, ChartPart chartPart1, int perStartRow, int endRow, int perFirstCol, int PerLastCol, string lineColour, string perSheetName, string Title,
                                                   string SubTitle, string num, string ValueFormat, GTTable Table, long GapWidth, string ChartType, bool isMatrix = false, bool isQCM = false, bool isNumeric = false)
        {
            int sRow = perStartRow;
            int fCol = perFirstCol + 1;
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = QC4Common.Common.Constants.GlobalMode.Split(',')[0] };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            C.Chart chart1 = new C.Chart();

            C.Title title1 = new C.Title();

            C.ChartText chartText1 = new C.ChartText();

            C.RichText richText1 = new C.RichText();
            A.BodyProperties bodyProperties1 = new A.BodyProperties();
            A.ListStyle listStyle1 = new A.ListStyle();

            richText1.Append(bodyProperties1);
            richText1.Append(listStyle1);
            richText1.Append(SetChartTitle(Title, SubTitle, num, 1000));
            chartText1.Append(richText1);
            C.Overlay overlay1 = new C.Overlay() { Val = false };

            title1.Append(chartText1);
            title1.Append(SetChartAreaTextPosition());
            title1.Append(overlay1);
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();
            C.Layout layout2 = CreateAlignedPlotAreaLayout(0.16, 0.78);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Column };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Clustered };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            C.BarChartSeries barChartSeries1 = null;
            C.Index index1 = null;
            C.Order order1 = null;

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);

            int row = 0, subTotalCnt = 0, col = 0, indexer = 0, i = 1;
            string rgb;

            barChartSeries1 = new C.BarChartSeries();
            index1 = new C.Index() { Val = (UInt32Value)0U };
            order1 = new C.Order() { Val = (UInt32Value)0U };
            C.SeriesText seriesText1 = new C.SeriesText();
            C.NumericValue numericValue1 = new C.NumericValue();
            numericValue1.Text = "";

            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            seriesText1.Append(numericValue1);

            if ((isMatrix && !isQCM && !isNumeric))
            {
                col = perFirstCol;
                row = endRow - Table.ChildQuestionsCount + 1;
                while (col <= PerLastCol)
                {
                    barChartSeries1 = new C.BarChartSeries();
                    index1 = new C.Index() { Val = (uint)indexer };
                    order1 = new C.Order() { Val = (uint)indexer };
                    barChartSeries1.Append(index1);
                    barChartSeries1.Append(order1);

                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)]);
                    rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                    // OOXML series order: idx, order, tx, spPr, invertIfNegative, dLbls, cat, val
                    barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, perStartRow + 1, col));
                    barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                    C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
                    bool showCategoryName = false, showLeaderLines = false;
                    barChartSeries1.Append(invertIfNegative1);
                    barChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines));
                    barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, row, endRow, 3, 3));
                    barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, col, row, endRow, col));
                    barChart1.Append(barChartSeries1);
                    col++; i++; indexer++;
                }
            }
            else if ((isMatrix && isNumeric))
            {
                barChartSeries1 = new C.BarChartSeries();
                index1 = new C.Index() { Val = (uint)indexer };
                order1 = new C.Order() { Val = (uint)indexer };
                barChartSeries1.Append(index1);
                barChartSeries1.Append(order1);

                var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((1 - 1) % Table.Chart.SeriesCount)]);
                rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, perStartRow + 1, PerLastCol));
                barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
                bool showCategoryName = false, showLeaderLines = false;
                barChartSeries1.Append(invertIfNegative1);
                barChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines));
                barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, perStartRow + 2, endRow, perFirstCol + 1, perFirstCol + 1));
                barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, PerLastCol, perStartRow + 2, endRow, PerLastCol));
                barChart1.Append(barChartSeries1);
            }
            else
            {
                // Series title required by Google Sheets (matches QCSV chart XML)
                barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, perStartRow + 1, PerLastCol));
                int clusterChartColor = Convert.ToInt32(Util.Constants.GTGraphColorIndex.WIDTH_STICK_M);
                if (Table.Chart.SeriesColorIndex((1 - 1) % Table.Chart.SeriesCount) == clusterChartColor &&
                                          (ChartType == "xlBarClustered" || ChartType == "xlColumnClustered") &&
                                          Table.Question.SubTotalCnt > 0)
                {
                    if (isQCM)
                    {
                        col = perFirstCol;
                        row = endRow;
                        subTotalCnt = PerLastCol - Table.Question.SubTotalCnt;
                        while (col <= PerLastCol)
                        {
                            if (col > subTotalCnt)
                                rgb = "E4DFEC";
                            else
                                rgb = "F2F2F2";

                            barChartSeries1.Append(ApplyFillXlClustered(lineColour, rgb, indexer));
                            indexer++;
                            col++;
                        }
                    }
                    else
                    {
                        col = PerLastCol;
                        row = sRow;
                        subTotalCnt = endRow - Table.Question.SubTotalCnt;
                        while (row <= endRow)
                        {
                            if (row > subTotalCnt)
                                rgb = "E4DFEC";
                            else
                                rgb = "F2F2F2";

                            barChartSeries1.Append(ApplyFillXlClustered(lineColour, rgb, indexer));
                            indexer++;
                            row++;
                        }
                    }
                }
                else
                {
                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((1 - 1) % Table.Chart.SeriesCount)]);
                    rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                    barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                }

                C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };

                bool showCategoryName = false, showLeaderLines = false;
                barChartSeries1.Append(invertIfNegative1);
                barChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines));

                if (isQCM)
                {
                    barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, perStartRow + 1, perStartRow + 1, PerLastCol, perFirstCol));
                    barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, PerLastCol, endRow, endRow, perFirstCol));
                }
                else
                {
                    barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, sRow, endRow, fCol, fCol));
                    barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, PerLastCol, sRow, endRow, PerLastCol));
                }
                barChart1.Append(barChartSeries1);
            }
            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)GapWidth };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)561883960U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)561891504U };

            barChart1.Append(gapWidth1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)561883960U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MinMax };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Bottom };
            C.NumberingFormat numberingFormat1 = new C.NumberingFormat() { FormatCode = "General", SourceLinked = true };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };

            C.ChartShapeProperties chartShapeProperties3 = new C.ChartShapeProperties();

            A.Outline outline6 = new A.Outline() { Width = 12700 };

            A.SolidFill solidFill8 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex16 = new A.RgbColorModelHex() { Val = "BFBFBF" };

            solidFill8.Append(rgbColorModelHex16);

            outline6.Append(solidFill8);

            chartShapeProperties3.Append(outline6);

            C.TextProperties textProperties1 = new C.TextProperties();
            A.BodyProperties bodyProperties2 = new A.BodyProperties()
            {
                Rotation = 0,
                Vertical = QC4Common.Common.Constants.GlobalMode != "ja-JP," + Util.Constants.QCFont.MS_Gothic ? A.TextVerticalValues.Horizontal : A.TextVerticalValues.WordArtLeftToRight
            };
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties();

            paragraphProperties2.Append(defaultRunProperties2);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = QC4Common.Common.Constants.GlobalMode.Split(',')[0] };

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(endParagraphRunProperties1);

            textProperties1.Append(bodyProperties2);
            textProperties1.Append(listStyle2);
            textProperties1.Append(paragraph2);
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)561891504U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId3);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(numberingFormat1);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(chartShapeProperties3);
            categoryAxis1.Append(textProperties1);
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)561891504U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 100D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            if (!isNumeric)
            {
                scaling2.Append(maxAxisValue1);
                scaling2.Append(minAxisValue1);
            }

            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };

            C.NumberingFormat numberingFormat2 = new C.NumberingFormat() { FormatCode = ValueFormat, SourceLinked = false };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };

            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)561883960U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 0.2 * 100D };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(MajorGuidLineColour(lineColour));
            valueAxis1.Append(numberingFormat2);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(ApplyFillColour(lineColour));
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            if (!isNumeric)
                valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            A.Outline outline9 = new A.Outline() { Width = 25400 };
            A.NoFill noFill4 = new A.NoFill();

            outline9.Append(noFill4);

            shapeProperties1.Append(noFill3);
            shapeProperties1.Append(outline9);

            plotArea1.Append(layout2);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };
            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(title1);
            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            if ((isMatrix && !isQCM && !isNumeric))
                chart1.Append(SetLegend());
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(chart1);
            chartSpace1.Append(SetChartAreaLineColour(lineColour));
            chartSpace1.Append(SetTextPrperty(800));
            //chartSpace1.Append(SetPrintSetting());

            chartPart1.ChartSpace = chartSpace1;
        }
        public ChartPart GenerateBarStackedChart(WorksheetPart worksheetPart, ChartPart chartPart1, int perStartRow, int endRow, int perFirstCol, int PerLastCol, string lineColour, string perSheetName, string Title,
                                                    string SubTitle, string num, string ValueFormat, GTTable Table, long GapWidth, string ChartType,ref string LegendSeriesText)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = QC4Common.Common.Constants.GlobalMode.Split(',')[0] };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            //Add text for Chart Area 
            C.Chart chart1 = new C.Chart();

            C.Title title1 = new C.Title();

            C.ChartText chartText1 = new C.ChartText();

            C.RichText richText1 = new C.RichText();
            A.BodyProperties bodyProperties2 = new A.BodyProperties();
            A.ListStyle listStyle2 = new A.ListStyle();
            C.Overlay overlay1 = new C.Overlay() { Val = false };

            richText1.Append(bodyProperties2);
            richText1.Append(listStyle2);
            richText1.Append(SetChartTitle(Title, SubTitle, num, 1000));

            chartText1.Append(richText1);
            title1.Append(chartText1);
            title1.Append(SetChartAreaTextPosition());
            title1.Append(overlay1);

            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();
            C.Layout layout2 = CreateAlignedPlotAreaLayout(0.10, 0.82);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Bar };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Stacked };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);

            //Loop
            int fRow = endRow - Table.ChildQuestionsCount + 1;
            int indexer = 0, i = 1;
            int lCol = PerLastCol - Table.Question.SubTotalCnt;
            string[] strArray = new string[endRow - fRow + 1];
   
            while (fRow <= endRow)
            {
                var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)]);
                var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
                C.Index index1 = new C.Index() { Val = (uint)indexer };
                C.Order order1 = new C.Order() { Val = (uint)indexer };

                C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
                bool showCategoryName = false, showLeaderLines = false;
                barChartSeries1.Append(index1);
                barChartSeries1.Append(order1);
                barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, fRow, 3));
                strArray[indexer] = GetSeriesTextValue(worksheetPart, fRow, 3);
                barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                barChartSeries1.Append(invertIfNegative1);
                barChartSeries1.Append(ApplyStackedBarDataLabels("0.0;;"));
                barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, perStartRow + 1, perStartRow + 1, lCol, perFirstCol));
                barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, lCol, fRow, fRow, perFirstCol));
                barChart1.Append(barChartSeries1);
                fRow++; indexer++; i++;
            }

            var sorted = strArray.OrderBy(n => n.Length);
            var longest = sorted.LastOrDefault();
            int arrLmt = strArray.Count();
            for (int cnt = 0; cnt <= arrLmt; cnt++)
            {
                LegendSeriesText += longest;
            }

            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)GapWidth };
            C.Overlap overlap1 = new C.Overlap() { Val = 100 };

            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)538334424U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)365623696U };

            barChart1.Append(gapWidth1);
            barChart1.Append(overlap1);
            barChart1.Append(SetSeriesLine());
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)538334424U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MaxMin };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.NumberingFormat numberingFormat4 = new C.NumberingFormat() { FormatCode = "General", SourceLinked = true };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };

            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)365623696U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId3);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(numberingFormat4);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(ApplyFillColour(lineColour));
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)365623696U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 100D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Top };

            C.NumberingFormat numberingFormat5 = new C.NumberingFormat() { FormatCode = ValueFormat, SourceLinked = false };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };

            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)538334424U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 20D };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(MajorGuidLineColour(lineColour));
            valueAxis1.Append(numberingFormat5);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(ApplyFillColour(lineColour));
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill7 = new A.NoFill();

            A.Outline outline14 = new A.Outline() { Width = 25400 };
            A.NoFill noFill8 = new A.NoFill();

            outline14.Append(noFill8);

            shapeProperties1.Append(noFill7);
            shapeProperties1.Append(outline14);

            plotArea1.Append(layout2);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };
            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(title1);
            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(SetLegend());
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(chart1);
            chartSpace1.Append(SetChartAreaLineColour(lineColour));
            chartSpace1.Append(SetTextPrperty(800));
            //chartSpace1.Append(SetPrintSetting());

            chartPart1.ChartSpace = chartSpace1;
            return chartPart1;
        }
        public ChartPart GenerateBarStacked100Chart(WorksheetPart worksheetPart, ChartPart chartPart1, int perStartRow, int endRow, int perFirstCol, int PerLastCol, string lineColour, string perSheetName, string Title,
                                                       string SubTitle, string num, string ValueFormat, GTTable Table, long GapWidth, string ChartType, ref string LegendSeriesText, ref string legendSeriesOriginal, ref string longest, ref int arrLmt, bool isMatrix = false)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = QC4Common.Common.Constants.GlobalMode.Split(',')[0] };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            //Add text for Chart Area 
            C.Chart chart1 = new C.Chart();

            C.Title title1 = new C.Title();

            C.ChartText chartText1 = new C.ChartText();

            C.RichText richText1 = new C.RichText();
            A.BodyProperties bodyProperties2 = new A.BodyProperties();
            A.ListStyle listStyle2 = new A.ListStyle();
            C.Overlay overlay1 = new C.Overlay() { Val = false };

            richText1.Append(bodyProperties2);
            richText1.Append(listStyle2);
            richText1.Append(SetChartTitle(Title, SubTitle, num, 1000));

            chartText1.Append(richText1);
            title1.Append(chartText1);
            title1.Append(SetChartAreaTextPosition());
            title1.Append(overlay1);

            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();
            C.Layout layout2 = CreateAlignedPlotAreaLayout(0.10, 0.84, PlotPercentStackedWidth);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Bar };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.PercentStacked };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);

            //Loop  
            string[] strArray = null;
            int col = perFirstCol, indexer = 0, i = 1;
            if (isMatrix)
            {              
                int fRow = endRow - Table.ChildQuestionsCount + 1;
                int limit = PerLastCol - Table.Question.SubTotalCnt;
                strArray = new string[(limit - col)+1];
                while (col <= limit)
                {
                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                    C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
                    C.Index index1 = new C.Index() { Val = (uint)indexer };
                    C.Order order1 = new C.Order() { Val = (uint)indexer };

                    C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
                    bool showCategoryName = false, showLeaderLines = false;
                    barChartSeries1.Append(index1);
                    barChartSeries1.Append(order1);
                    barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, (perStartRow + 1), col));
                    strArray[indexer] = GetSeriesTextValue(worksheetPart, (perStartRow + 1), col);
                    //LegendSeriesText += GetSeriesTextValue(worksheetPart, (perStartRow + 1), col);
                    barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                    barChartSeries1.Append(invertIfNegative1);
                    barChartSeries1.Append(ApplyStackedBarDataLabels("0.0;;"));
                    barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, fRow, endRow, 3, 3));
                    barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, col, fRow, endRow, col));
                    barChart1.Append(barChartSeries1);
                    col++; indexer++; i++;
                }
            }
            else
            {
                int fRow = perStartRow;
                int limit = endRow;
                strArray = new string[(limit - fRow) + 1];
                while (fRow <= limit)
                {
                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                    C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
                    C.Index index1 = new C.Index() { Val = (uint)indexer };
                    C.Order order1 = new C.Order() { Val = (uint)indexer };

                    C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
                    bool showCategoryName = false, showLeaderLines = false;
                    barChartSeries1.Append(index1);
                    barChartSeries1.Append(order1);
                    barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, fRow, 3));
                    strArray[indexer] = GetSeriesTextValue(worksheetPart, fRow, 3);
                    barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                    barChartSeries1.Append(invertIfNegative1);
                    barChartSeries1.Append(ApplyStackedBarDataLabels("0.0;;"));
                    barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, PerLastCol, fRow, fRow, PerLastCol));
                    barChart1.Append(barChartSeries1);
                    fRow++; indexer++; i++;
                }
            }

            var sorted = strArray.OrderBy(n => n.Length);
            longest = sorted.LastOrDefault();

            //string longest = strArray.Max();
            arrLmt = strArray.Count();
            for(int cnt = 0;cnt <= arrLmt;cnt++)
            {
                LegendSeriesText += " " + longest + " "; //Redmine Id:224914
                if (cnt < arrLmt)
                    legendSeriesOriginal += " " + strArray[cnt] + " ";
            }
            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)GapWidth };
            C.Overlap overlap1 = new C.Overlap() { Val = 100 };

            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)557501728U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)557497464U };

            barChart1.Append(gapWidth1);
            barChart1.Append(overlap1);
            barChart1.Append(SetSeriesLine());
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)557501728U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MaxMin };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.NumberingFormat numberingFormat6 = new C.NumberingFormat() { FormatCode = "General", SourceLinked = true };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = ((isMatrix) ? C.TickLabelPositionValues.NextTo : C.TickLabelPositionValues.None) };
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)557497464U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId3);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(numberingFormat6);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(ApplyFillColour(lineColour));
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)557497464U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 1D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Top };

            C.NumberingFormat numberingFormat7 = new C.NumberingFormat() { FormatCode = ValueFormat, SourceLinked = false };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };

            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)557501728U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 0.2D };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(MajorGuidLineColour(lineColour));
            valueAxis1.Append(numberingFormat7);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(ApplyFillColour(lineColour));
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill11 = new A.NoFill();

            A.Outline outline18 = new A.Outline() { Width = 25400 };
            A.NoFill noFill12 = new A.NoFill();

            outline18.Append(noFill12);

            shapeProperties1.Append(noFill11);
            shapeProperties1.Append(outline18);

            plotArea1.Append(layout2);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };
            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(title1);
            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(SetLegend());
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(chart1);
            chartSpace1.Append(SetChartAreaLineColour(lineColour));
            chartSpace1.Append(SetTextPrperty(800));
            //chartSpace1.Append(SetPrintSetting());

            chartPart1.ChartSpace = chartSpace1;
            return chartPart1;
        }
        public void GenerateColumnStackedChart(WorksheetPart worksheetPart, ChartPart chartPart1, int perStartRow, int endRow, int perFirstCol, int PerLastCol, string lineColour, string perSheetName, string Title,
                                                string SubTitle, string num, string ValueFormat, GTTable Table, long GapWidth, string ChartType)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = QC4Common.Common.Constants.GlobalMode.Split(',')[0] };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            //Add text for Chart Area 
            C.Chart chart1 = new C.Chart();

            C.Title title1 = new C.Title();

            C.ChartText chartText1 = new C.ChartText();

            C.RichText richText1 = new C.RichText();
            A.BodyProperties bodyProperties2 = new A.BodyProperties();
            A.ListStyle listStyle2 = new A.ListStyle();
            C.Overlay overlay1 = new C.Overlay() { Val = false };

            richText1.Append(bodyProperties2);
            richText1.Append(listStyle2);
            richText1.Append(SetChartTitle(Title, SubTitle, num, 1000));

            chartText1.Append(richText1);
            title1.Append(chartText1);
            title1.Append(SetChartAreaTextPosition());
            title1.Append(overlay1);

            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();
            C.Layout layout2 = CreateAlignedPlotAreaLayout(0.10, 0.82);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Column };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Stacked };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);
            //Loop
            int fRow = endRow - Table.ChildQuestionsCount + 1;
            int indexer = 0, i = 1;
            int lCol = PerLastCol - Table.Question.SubTotalCnt;
            while (fRow <= endRow)
            {
                var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)]);
                var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
                C.Index index1 = new C.Index() { Val = (uint)indexer };
                C.Order order1 = new C.Order() { Val = (uint)indexer };

                C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
                barChartSeries1.Append(index1);
                barChartSeries1.Append(order1);
                barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, fRow, 3));
                barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                barChartSeries1.Append(invertIfNegative1);
                barChartSeries1.Append(ApplyStackedBarDataLabels("0.0;;"));
                barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, perStartRow + 1, perStartRow + 1, lCol, perFirstCol));
                barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, lCol, fRow, fRow, perFirstCol));
                barChart1.Append(barChartSeries1);
                fRow++; indexer++; i++;
            }

            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)GapWidth };
            C.Overlap overlap1 = new C.Overlap() { Val = 100 };

            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)561473216U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)561472888U };

            barChart1.Append(gapWidth1);
            barChart1.Append(overlap1);
            barChart1.Append(SetSeriesLine());
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)561473216U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MinMax };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Bottom };
            C.NumberingFormat numberingFormat4 = new C.NumberingFormat() { FormatCode = "General", SourceLinked = true };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };

            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)561472888U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId3);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(numberingFormat4);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(ApplyFillColour(lineColour));
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)561472888U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 100D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };

            C.NumberingFormat numberingFormat5 = new C.NumberingFormat() { FormatCode = ValueFormat, SourceLinked = false };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)561473216U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 20D };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(MajorGuidLineColour(lineColour));
            valueAxis1.Append(numberingFormat5);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(ApplyFillColour(lineColour));
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill7 = new A.NoFill();

            A.Outline outline14 = new A.Outline() { Width = 25400 };
            A.NoFill noFill8 = new A.NoFill();

            outline14.Append(noFill8);

            shapeProperties1.Append(noFill7);
            shapeProperties1.Append(outline14);

            plotArea1.Append(layout2);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };

            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(title1);
            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(SetLegend());
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(chart1);
            chartSpace1.Append(SetChartAreaLineColour(lineColour));
            chartSpace1.Append(SetTextPrperty(800));

            chartPart1.ChartSpace = chartSpace1;
        }
        public void GenerateColumnStacked100Chart(WorksheetPart worksheetPart, ChartPart chartPart1, int perStartRow, int endRow, int perFirstCol, int PerLastCol, string lineColour, string perSheetName, string Title,
                                                     string SubTitle, string num, string ValueFormat, GTTable Table, long GapWidth, string ChartType, bool isMatrix = false)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = QC4Common.Common.Constants.GlobalMode.Split(',')[0] };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            //Add text for Chart Area 
            C.Chart chart1 = new C.Chart();

            C.Title title1 = new C.Title();

            C.ChartText chartText1 = new C.ChartText();

            C.RichText richText1 = new C.RichText();
            A.BodyProperties bodyProperties2 = new A.BodyProperties();
            A.ListStyle listStyle2 = new A.ListStyle();
            C.Overlay overlay1 = new C.Overlay() { Val = false };

            richText1.Append(bodyProperties2);
            richText1.Append(listStyle2);
            richText1.Append(SetChartTitle(Title, SubTitle, num, 1000));

            chartText1.Append(richText1);
            title1.Append(chartText1);
            title1.Append(SetChartAreaTextPosition());
            title1.Append(overlay1);
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();
            C.Layout layout2 = CreateAlignedPlotAreaLayout(0.10, 0.84, PlotPercentStackedWidth);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Column };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.PercentStacked };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);

            //Loop
            int col = perFirstCol, indexer = 0, i = 1;
            if (isMatrix)
            {
                int fRow = endRow - Table.ChildQuestionsCount + 1;
                int limit = PerLastCol - Table.Question.SubTotalCnt;
                while (col <= limit)
                {
                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                    C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
                    C.Index index1 = new C.Index() { Val = (uint)indexer };
                    C.Order order1 = new C.Order() { Val = (uint)indexer };

                    C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
                    barChartSeries1.Append(index1);
                    barChartSeries1.Append(order1);
                    barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, (perStartRow + 1), col));
                    barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                    barChartSeries1.Append(invertIfNegative1);
                    barChartSeries1.Append(ApplyStackedBarDataLabels("0.0;;"));
                    barChartSeries1.Append(SetStringDataLink(worksheetPart, perSheetName, fRow, endRow, 3, 3));
                    barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, col, fRow, endRow, col));
                    barChart1.Append(barChartSeries1);
                    col++; indexer++; i++;
                }
            }
            else
            {
                int fRow = perStartRow;
                int limit = endRow;
                while (fRow <= limit)
                {
                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[Table.Chart.SeriesColorIndex((i - 1) % Table.Chart.SeriesCount)]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                    C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
                    C.Index index1 = new C.Index() { Val = (uint)indexer };
                    C.Order order1 = new C.Order() { Val = (uint)indexer };

                    C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
                    barChartSeries1.Append(index1);
                    barChartSeries1.Append(order1);
                    barChartSeries1.Append(SetSeriesText(worksheetPart, perSheetName, fRow, 3));
                    barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                    barChartSeries1.Append(invertIfNegative1);
                    barChartSeries1.Append(ApplyStackedBarDataLabels("0.0;;"));
                    barChartSeries1.Append(SetNumericDataLink(worksheetPart, perSheetName, PerLastCol, fRow, fRow, PerLastCol));
                    barChart1.Append(barChartSeries1);
                    fRow++; indexer++; i++;
                }
            }
            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)GapWidth };
            C.Overlap overlap1 = new C.Overlap() { Val = 100 };

            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)563111192U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)65736744U };

            barChart1.Append(gapWidth1);
            barChart1.Append(overlap1);
            barChart1.Append(SetSeriesLine());
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)563111192U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MinMax };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Bottom };
            C.NumberingFormat numberingFormat6 = new C.NumberingFormat() { FormatCode = "General", SourceLinked = true };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = ((isMatrix) ? C.TickLabelPositionValues.NextTo : C.TickLabelPositionValues.None) };

            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)65736744U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId3);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(numberingFormat6);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(ApplyFillColour(lineColour));
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)65736744U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 1D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };

            C.NumberingFormat numberingFormat7 = new C.NumberingFormat() { FormatCode = "0%", SourceLinked = false };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };

            C.ChartShapeProperties chartShapeProperties14 = new C.ChartShapeProperties();

            A.Outline outline17 = new A.Outline() { Width = 12700 };

            A.SolidFill solidFill18 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex28 = new A.RgbColorModelHex() { Val = "BFBFBF" };

            solidFill18.Append(rgbColorModelHex28);

            outline17.Append(solidFill18);

            chartShapeProperties14.Append(outline17);
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)563111192U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 0.2D };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(MajorGuidLineColour(lineColour));
            valueAxis1.Append(numberingFormat7);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(chartShapeProperties14);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill11 = new A.NoFill();

            A.Outline outline18 = new A.Outline() { Width = 25400 };
            A.NoFill noFill12 = new A.NoFill();

            outline18.Append(noFill12);

            shapeProperties1.Append(noFill11);
            shapeProperties1.Append(outline18);

            plotArea1.Append(layout2);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };

            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(title1);
            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(SetLegend());
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(chart1);
            chartSpace1.Append(SetChartAreaLineColour(lineColour));
            chartSpace1.Append(SetTextPrperty(800));

            chartPart1.ChartSpace = chartSpace1;

        }
        public C.SeriesLines SetSeriesLine()
        {
            C.SeriesLines seriesLines1 = new C.SeriesLines();

            C.ChartShapeProperties chartShapeProperties7 = new C.ChartShapeProperties();

            A.Outline outline10 = new A.Outline();

            A.PatternFill patternFill1 = new A.PatternFill() { Preset = A.PresetPatternValues.Percent50 };

            A.ForegroundColor foregroundColor1 = new A.ForegroundColor();
            A.RgbColorModelHex rgbColorModelHex20 = new A.RgbColorModelHex() { Val = "000000" };

            foregroundColor1.Append(rgbColorModelHex20);

            A.BackgroundColor backgroundColor1 = new A.BackgroundColor();
            A.RgbColorModelHex rgbColorModelHex21 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            backgroundColor1.Append(rgbColorModelHex21);

            patternFill1.Append(foregroundColor1);
            patternFill1.Append(backgroundColor1);
            A.PresetDash presetDash4 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline10.Append(patternFill1);
            outline10.Append(presetDash4);

            chartShapeProperties7.Append(outline10);

            seriesLines1.Append(chartShapeProperties7);
            return seriesLines1;
        }
        public C.SeriesText SetSeriesText(string sheetName, int row, int col)
        {
            C.SeriesText seriesText = new C.SeriesText();

            C.StringReference stringReference1 = new C.StringReference();
            C.Formula formula1 = new C.Formula();
            formula1.Text = "\'" + sheetName + "\'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(col) + "$" + row;
            stringReference1.Append(formula1);

            // Cache required for Google Sheets chart import
            C.StringCache stringCache = new C.StringCache();
            C.PointCount pointCount = new C.PointCount() { Val = 1U };
            stringCache.Append(pointCount);
            C.StringPoint stringPoint = new C.StringPoint() { Index = 0U };
            C.NumericValue numericValue = new C.NumericValue();
            numericValue.Text = string.Empty;
            stringPoint.Append(numericValue);
            stringCache.Append(stringPoint);
            stringReference1.Append(stringCache);

            seriesText.Append(stringReference1);
            return seriesText;
        }

        public C.SeriesText SetSeriesText(WorksheetPart worksheetPart, string sheetName, int row, int col)
        {
            C.SeriesText seriesText = new C.SeriesText();

            C.StringReference stringReference1 = new C.StringReference();
            C.Formula formula1 = new C.Formula();
            formula1.Text = "\'" + sheetName + "\'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(col) + "$" + row;
            stringReference1.Append(formula1);

            C.StringCache stringCache = new C.StringCache();
            C.PointCount pointCount = new C.PointCount() { Val = 1U };
            stringCache.Append(pointCount);
            C.StringPoint stringPoint = new C.StringPoint() { Index = 0U };
            C.NumericValue numericValue = new C.NumericValue();
            numericValue.Text = GetChartCellText(worksheetPart, row, col);
            stringPoint.Append(numericValue);
            stringCache.Append(stringPoint);
            stringReference1.Append(stringCache);

            seriesText.Append(stringReference1);
            return seriesText;
        }
        /// <summary>
        /// Method to generate the chart title
        /// </summary>
        /// <param name="Title">Title</param>
        /// <param name="SubTitle">Sub Title</param>
        /// <param name="num">Data count</param>
        /// <param name="fontSize">Font Size</param>
        /// <returns>Paragraph class for chart to set the chart title</returns>
        private A.Paragraph SetChartTitle(string Title, string SubTitle, string num, int fontSize)
        {
            string numBuf = null;
            A.Paragraph paragraph = new A.Paragraph();
            if (num != "0" && num != "-1")
                numBuf = Constants.vbLf + "(n=" + num + ")";
            
            if (Strings.Len(SubTitle) > 0)
                SubTitle = "[" + SubTitle + "]";
            {
                A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Left };
                A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties() { FontSize = fontSize };

                paragraphProperties2.Append(defaultRunProperties2);

                A.Run run2 = new A.Run();
                A.RunProperties runProperties2 = new A.RunProperties() { Language = QC4Common.Common.Constants.GlobalMode.Split(',')[0], FontSize = fontSize };
                A.Text text21 = new A.Text();
                text21.Text = Title + SubTitle + numBuf;
                run2.Append(runProperties2);
                run2.Append(text21);
                paragraph.Append(paragraphProperties2);
                paragraph.Append(run2);
            }
            return paragraph;
        }
        public C.DataLabels ApplyDataLabels(bool showCategoryName, bool showLeaderLines, string NumberFormat = null, C.DataLabels dataLabel = null)
        {
            C.DataLabels dataLabels = dataLabel == null ? new C.DataLabels() : dataLabel;
            if (NumberFormat != null)
            {
                C.NumberingFormat numberingFormat6 = new C.NumberingFormat() { FormatCode = NumberFormat, SourceLinked = false };
                dataLabels.Append(numberingFormat6);
            }
            C.ChartShapeProperties chartShapeProperties4 = new C.ChartShapeProperties();
            A.NoFill noFill1 = new A.NoFill();

            A.Outline outline8 = new A.Outline();
            A.NoFill noFill2 = new A.NoFill();

            outline8.Append(noFill2);
            A.EffectList effectList5 = new A.EffectList();

            chartShapeProperties4.Append(noFill1);
            chartShapeProperties4.Append(outline8);
            chartShapeProperties4.Append(effectList5);

            C.ShowLegendKey showLegendKey1 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue1 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName1 = new C.ShowCategoryName() { Val = showCategoryName };
            C.ShowSeriesName showSeriesName1 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent1 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize1 = new C.ShowBubbleSize() { Val = false };

            dataLabels.Append(chartShapeProperties4);
            dataLabels.Append(showLegendKey1);
            dataLabels.Append(showValue1);
            dataLabels.Append(showCategoryName1);
            dataLabels.Append(showSeriesName1);
            dataLabels.Append(showPercent1);
            dataLabels.Append(showBubbleSize1);

            if (NumberFormat != null)
            {
                C.Separator separator1 = new C.Separator();
                separator1.Text = Space(1);
                dataLabels.Append(separator1);
            }
            C.ShowLeaderLines showLeaderLines1 = new C.ShowLeaderLines() { Val = showLeaderLines };
            dataLabels.Append(showLeaderLines1);
            return dataLabels;
        }

        public C.DataLabel ApplyDataLabel(bool showCategoryName, bool showValue, bool showLeaderLines, string NumberFormat = null, C.DataLabel dataLabel = null)
        {
            C.DataLabel dataLabels = dataLabel == null ? new C.DataLabel() : dataLabel;
            if (NumberFormat != null)
            {
                C.NumberingFormat numberingFormat6 = new C.NumberingFormat() { FormatCode = NumberFormat, SourceLinked = false };
                dataLabels.Append(numberingFormat6);
            }
            C.ChartShapeProperties chartShapeProperties4 = new C.ChartShapeProperties();
            A.NoFill noFill1 = new A.NoFill();

            A.Outline outline8 = new A.Outline();
            A.NoFill noFill2 = new A.NoFill();

            outline8.Append(noFill2);
            A.EffectList effectList5 = new A.EffectList();

            chartShapeProperties4.Append(noFill1);
            chartShapeProperties4.Append(outline8);
            chartShapeProperties4.Append(effectList5);

            C.ShowLegendKey showLegendKey1 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue1 = new C.ShowValue() { Val = showValue };
            C.ShowCategoryName showCategoryName1 = new C.ShowCategoryName() { Val = showCategoryName };
            C.ShowSeriesName showSeriesName1 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent1 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize1 = new C.ShowBubbleSize() { Val = false };

            dataLabels.Append(chartShapeProperties4);
            dataLabels.Append(showLegendKey1);
            dataLabels.Append(showValue1);
            dataLabels.Append(showCategoryName1);
            dataLabels.Append(showSeriesName1);
            dataLabels.Append(showPercent1);
            dataLabels.Append(showBubbleSize1);

            if (NumberFormat != null)
            {
                C.Separator separator1 = new C.Separator();
                separator1.Text = Space(1);
                dataLabels.Append(separator1);
            }
            C.ShowLeaderLines showLeaderLines1 = new C.ShowLeaderLines() { Val = showLeaderLines };
            dataLabels.Append(showLeaderLines1);
            return dataLabels;
        }

        /// <summary>
        /// Left-aligned plot area layout for Google Sheets compatibility (mirrors QCSV GraphGenerator).
        /// </summary>
        private static C.Layout CreateAlignedPlotAreaLayout(double y, double height, double width = PlotFullWidth)
        {
            C.Layout layout = new C.Layout();
            C.ManualLayout manualLayout = new C.ManualLayout();
            C.LayoutTarget layoutTarget = new C.LayoutTarget() { Val = C.LayoutTargetValues.Inner };
            C.LeftMode leftMode = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left = new C.Left() { Val = PlotLeftX };
            C.Top top = new C.Top() { Val = y };
            C.Width plotWidth = new C.Width() { Val = width };
            C.Height plotHeight = new C.Height() { Val = height };

            manualLayout.Append(layoutTarget);
            manualLayout.Append(leftMode);
            manualLayout.Append(topMode);
            manualLayout.Append(left);
            manualLayout.Append(top);
            manualLayout.Append(plotWidth);
            manualLayout.Append(plotHeight);
            layout.Append(manualLayout);
            return layout;
        }

        /// <summary>
        /// Centered in-segment labels without leader lines — required for stacked bars in Google Sheets.
        /// </summary>
        public C.DataLabels ApplyStackedBarDataLabels(string NumberFormat = null)
        {
            C.DataLabels dataLabels = ApplyDataLabels(false, false, NumberFormat);
            C.DataLabelPosition labelPosition = new C.DataLabelPosition() { Val = C.DataLabelPositionValues.Center };
            dataLabels.Append(labelPosition);
            return dataLabels;
        }

        public C.Legend SetLegend()
        {
            C.Legend legend1 = new C.Legend();
            C.LegendPosition legendPosition1 = new C.LegendPosition() { Val = C.LegendPositionValues.Top };
            C.Overlay overlay2 = new C.Overlay() { Val = false };

            C.ChartShapeProperties chartShapeProperties38 = new C.ChartShapeProperties();

            A.Outline outline42 = new A.Outline() { Width = 25400 };
            A.NoFill noFill37 = new A.NoFill();

            outline42.Append(noFill37);

            chartShapeProperties38.Append(outline42);

            legend1.Append(legendPosition1);
            legend1.Append(overlay2);
            legend1.Append(chartShapeProperties38);
            return legend1;
        }
        public C.TextProperties SetTextPrperty(int fontSize)
        {
            C.TextProperties textProperties = new C.TextProperties();
            A.BodyProperties bodyProperties3 = new A.BodyProperties();
            A.ListStyle listStyle3 = new A.ListStyle();

            A.Paragraph paragraph3 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties();

            A.DefaultRunProperties defaultRunProperties3 = new A.DefaultRunProperties() { FontSize = fontSize, Bold = false };
            A.LatinFont latinFont3 = new A.LatinFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };
            A.EastAsianFont eastAsianFont3 = new A.EastAsianFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };
            A.ComplexScriptFont complexScriptFont3 = new A.ComplexScriptFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };

            defaultRunProperties3.Append(latinFont3);
            defaultRunProperties3.Append(eastAsianFont3);
            defaultRunProperties3.Append(complexScriptFont3);

            paragraphProperties3.Append(defaultRunProperties3);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = QC4Common.Common.Constants.GlobalMode.Split(',')[0] };

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(endParagraphRunProperties1);

            textProperties.Append(bodyProperties3);
            textProperties.Append(listStyle3);
            textProperties.Append(paragraph3);
            return textProperties;
        }
        public C.ShapeProperties SetChartAreaLineColour(string lineColour)
        {
            C.ShapeProperties shapeProperties = new C.ShapeProperties();
            A.Outline outline10 = new A.Outline() { Width = 12700 };
            A.SolidFill solidFill13 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex22 = new A.RgbColorModelHex() { Val = lineColour };
            solidFill13.Append(rgbColorModelHex22);
            outline10.Append(solidFill13);
            shapeProperties.Append(outline10);
            return shapeProperties;
        }
        public C.Layout SetChartAreaTextPosition()
        {
            C.Layout layout = new C.Layout();

            C.ManualLayout manualLayout1 = new C.ManualLayout();
            C.LeftMode leftMode1 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode1 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left1 = new C.Left() { Val = 9.765625E-3D };
            C.Top top1 = new C.Top() { Val = 1.7481675212498708E-2D };

            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(top1);

            layout.Append(manualLayout1);
            return layout;
        }
        public C.MajorGridlines MajorGuidLineColour(string lineColour)
        {
            C.MajorGridlines majorGridlines1 = new C.MajorGridlines();

            C.ChartShapeProperties chartShapeProperties4 = new C.ChartShapeProperties();

            A.Outline outline7 = new A.Outline();

            A.SolidFill solidFill9 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex17 = new A.RgbColorModelHex() { Val = lineColour };

            solidFill9.Append(rgbColorModelHex17);
            A.PresetDash presetDash4 = new A.PresetDash() { Val = A.PresetLineDashValues.SystemDash };

            outline7.Append(solidFill9);
            outline7.Append(presetDash4);

            chartShapeProperties4.Append(outline7);

            majorGridlines1.Append(chartShapeProperties4);
            return majorGridlines1;
        }
        public C.DataPoint ApplyFillXlClustered(string outLineClr, string innerClr, int indexer)
        {

            C.DataPoint dataPoint1 = new C.DataPoint();
            C.Index index2 = new C.Index() { Val = (uint)indexer };
            C.InvertIfNegative invertIfNegative2 = new C.InvertIfNegative() { Val = false };
            C.Bubble3D bubble3D1 = new C.Bubble3D() { Val = false };

            C.ChartShapeProperties chartShapeProperties2 = new C.ChartShapeProperties();

            A.SolidFill solidFill8 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex16 = new A.RgbColorModelHex() { Val = innerClr };

            solidFill8.Append(rgbColorModelHex16);

            A.Outline outline5 = new A.Outline() { Width = 12700 };

            A.SolidFill solidFill9 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex17 = new A.RgbColorModelHex() { Val = outLineClr };

            solidFill9.Append(rgbColorModelHex17);

            outline5.Append(solidFill9);

            chartShapeProperties2.Append(solidFill8);
            chartShapeProperties2.Append(outline5);

            dataPoint1.Append(index2);
            dataPoint1.Append(invertIfNegative2);
            dataPoint1.Append(bubble3D1);
            dataPoint1.Append(chartShapeProperties2);
            return dataPoint1;
        }
        public C.ChartShapeProperties ApplyFillColour(string outLneclr, string inLineClr = null)
        {
            C.ChartShapeProperties chartShapeProperties = new C.ChartShapeProperties();

            if (inLineClr != null)
            {
                A.SolidFill solidFill1 = new A.SolidFill();
                A.RgbColorModelHex rgbColorModelHex18 = new A.RgbColorModelHex() { Val = inLineClr };
                solidFill1.Append(rgbColorModelHex18);
                chartShapeProperties.Append(solidFill1);
            }
            A.Outline outline6 = new A.Outline() { Width = 12700 };

            A.SolidFill solidFill2 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex19 = new A.RgbColorModelHex() { Val = outLneclr };

            solidFill2.Append(rgbColorModelHex19);

            outline6.Append(solidFill2);
            chartShapeProperties.Append(outline6);
            return chartShapeProperties;
        }
       
        public C.CategoryAxisData SetStringDataLink(WorksheetPart worksheetPart, string perSheetName, int perStartRow, int endRow, int perLastCol, int perFirstCol = 0)
        {
            C.CategoryAxisData categoryAxisData1 = new C.CategoryAxisData();

            C.StringReference stringReference1 = new C.StringReference();
            C.Formula formula1 = new C.Formula();
           
            formula1.Text = "\'" + perSheetName + "\'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(perFirstCol) + "$" + perStartRow + ":$" + OpenXmlHelper.ColumnIndexToColumnLetter(perLastCol) + "$" + endRow;

            stringReference1.Append(formula1);
            stringReference1.Append(BuildStringCache(worksheetPart, perStartRow, endRow, perFirstCol, perLastCol));
            categoryAxisData1.Append(stringReference1);
            return categoryAxisData1;
        }
        public C.StringCache InsertStringDataLabel(WorksheetPart worksheetPart, int perStartRow, int endRow, int lastCol)
        {
            C.StringCache stringCache = new C.StringCache();

            int rowCount = perStartRow, cnt = 0;
            while (rowCount < endRow)
            {
                Row row1 = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowCount);
                Cell cell = OpenXmlHelper.GetCell(row1, rowCount, lastCol);
                if (cell.CellValue.InnerText != "0")
                {
                    C.StringPoint stringPoint1 = new C.StringPoint() { Index = (uint)cnt };
                    C.NumericValue numericValue1 = new C.NumericValue();
                    Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowCount);
                    numericValue1.Text = OpenXmlHelper.GetCell(r, rowCount, 3).CellValue.InnerText;
                    stringPoint1.Append(numericValue1);
                    stringCache.Append(stringPoint1);
                    cnt++;
                }
                rowCount++;
            }
            return stringCache;
        }
        public C.NumberingCache InsertNumberDataLabel(WorksheetPart worksheetPart, int perStartRow, int endRow, int lastCol)
        {
            C.NumberingCache numberingCache = new C.NumberingCache();

            C.FormatCode formatCode = new C.FormatCode();
            formatCode.Text = "0.0";
            numberingCache.Append(formatCode);
            int cnt = 0;
            int rowCount = perStartRow;
            while (rowCount < endRow)
            {
                Row r = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowCount);
                Cell cell = OpenXmlHelper.GetCell(r, rowCount, lastCol);
                if (cell.CellValue.InnerText != "0")
                {
                    C.NumericPoint numericPoint = new C.NumericPoint() { Index = (uint)cnt };
                    C.NumericValue numericValue1 = new C.NumericValue();
                    numericValue1.Text = cell.CellValue.InnerText;
                    numericPoint.Append(numericValue1);
                    numberingCache.Append(numericPoint);
                    cnt++;
                }
                rowCount++;
            }
            return numberingCache;
        }
        public C.Values SetNumericDataLink(WorksheetPart worksheetPart, string perSheetName, int PerLastCol, int perStartRow, int endRow, int perFirstCol = 0)
        {
            C.Values values = new C.Values();

            C.NumberReference numberReference = new C.NumberReference();
            C.Formula formula2 = new C.Formula();
            formula2.Text = "\'" + perSheetName + "\'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(perFirstCol) + "$" + perStartRow + ":$" + OpenXmlHelper.ColumnIndexToColumnLetter(PerLastCol) + "$" + endRow;
            numberReference.Append(formula2);
            numberReference.Append(BuildNumberingCache(worksheetPart, perStartRow, endRow, perFirstCol, PerLastCol));
            values.Append(numberReference);
            return values;
        }

        /// <summary>
        /// Builds cached category/series string values. Google Sheets requires strCache on chart refs.
        /// </summary>
        private static C.StringCache BuildStringCache(WorksheetPart worksheetPart, int startRow, int endRow, int startCol, int endCol)
        {
            C.StringCache stringCache = new C.StringCache();
            List<string> values = CollectRangeValues(worksheetPart, startRow, endRow, startCol, endCol);
            C.PointCount pointCount = new C.PointCount() { Val = (UInt32Value)(uint)values.Count };
            stringCache.Append(pointCount);
            for (int i = 0; i < values.Count; i++)
            {
                C.StringPoint stringPoint = new C.StringPoint() { Index = (UInt32Value)(uint)i };
                C.NumericValue numericValue = new C.NumericValue();
                numericValue.Text = values[i] ?? string.Empty;
                stringPoint.Append(numericValue);
                stringCache.Append(stringPoint);
            }
            return stringCache;
        }

        /// <summary>
        /// Builds cached numeric series values. Google Sheets requires numCache on chart refs.
        /// </summary>
        private static C.NumberingCache BuildNumberingCache(WorksheetPart worksheetPart, int startRow, int endRow, int startCol, int endCol)
        {
            C.NumberingCache numberingCache = new C.NumberingCache();
            C.FormatCode formatCode = new C.FormatCode();
            formatCode.Text = "General";
            numberingCache.Append(formatCode);

            List<string> values = CollectRangeValues(worksheetPart, startRow, endRow, startCol, endCol);
            C.PointCount pointCount = new C.PointCount() { Val = (UInt32Value)(uint)values.Count };
            numberingCache.Append(pointCount);
            for (int i = 0; i < values.Count; i++)
            {
                C.NumericPoint numericPoint = new C.NumericPoint() { Index = (UInt32Value)(uint)i };
                C.NumericValue numericValue = new C.NumericValue();
                string raw = values[i];
                if (string.IsNullOrEmpty(raw) || !double.TryParse(raw, out _))
                    numericValue.Text = "0";
                else
                    numericValue.Text = raw;
                numericPoint.Append(numericValue);
                numberingCache.Append(numericPoint);
            }
            return numberingCache;
        }

        private static List<string> CollectRangeValues(WorksheetPart worksheetPart, int startRow, int endRow, int startCol, int endCol)
        {
            List<string> values = new List<string>();
            if (worksheetPart == null)
                return values;

            int rowFrom = Math.Min(startRow, endRow);
            int rowTo = Math.Max(startRow, endRow);
            int colFrom = Math.Min(startCol, endCol);
            int colTo = Math.Max(startCol, endCol);

            for (int r = rowFrom; r <= rowTo; r++)
            {
                for (int c = colFrom; c <= colTo; c++)
                {
                    values.Add(GetChartCellText(worksheetPart, r, c));
                }
            }
            return values;
        }

        private static string GetChartCellText(WorksheetPart worksheetPart, int rowIdx, int colIdx)
        {
            try
            {
                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowIdx);
                if (row == null)
                    return string.Empty;
                Cell cell = OpenXmlHelper.GetCell(row, rowIdx, colIdx);
                if (cell == null || cell.CellValue == null)
                    return string.Empty;

                string text = cell.CellValue.InnerText ?? string.Empty;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    try
                    {
                        WorkbookPart workbookPart = ((SpreadsheetDocument)worksheetPart.OpenXmlPackage).WorkbookPart;
                        SharedStringTablePart sstPart = workbookPart?.SharedStringTablePart;
                        if (sstPart?.SharedStringTable != null && int.TryParse(text, out int sstIndex))
                        {
                            SharedStringItem item = sstPart.SharedStringTable.Elements<SharedStringItem>().ElementAtOrDefault(sstIndex);
                            if (item != null)
                                return item.InnerText ?? string.Empty;
                        }
                    }
                    catch
                    {
                        // fall through to raw text
                    }
                }
                return text;
            }
            catch
            {
                return string.Empty;
            }
        }
        public C.Layout SetPlotAreaLayout(int rowCount)
        {
            C.Layout layout2 = new C.Layout();

            C.ManualLayout manualLayout2 = new C.ManualLayout();
            C.LeftMode leftMode2 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode2 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left2 = new C.Left() { Val = 1.7903645833333332E-2D };
            C.Top top2 = new C.Top() { Val = 0.18242817381277718D };
            C.Width width1 = new C.Width() { Val = 0.96419270833333337D };
            C.Height height1 = new C.Height() { Val = 0.77247019762210711D };

            manualLayout2.Append(leftMode2);
            manualLayout2.Append(topMode2);
            manualLayout2.Append(left2);
            manualLayout2.Append(top2);
            manualLayout2.Append(width1);
            manualLayout2.Append(height1);

            layout2.Append(manualLayout2);
            return layout2;
        }
        private string Space(int count = 1)
        {
            string space = " ";
            string retVal = string.Empty;
            for (int i = 1; i <= count; i++)
            {
                retVal = retVal + space;
            }
            return retVal;
        }
        //Index
        public void GenerateTitleBox(DrawingsPart drawingsPart, string title)
        {
            Xdr.WorksheetDrawing worksheetDrawing1 = new Xdr.WorksheetDrawing();

            Xdr.TwoCellAnchor twoCellAnchor1 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.TwoCell };

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
            columnId2.Text = "9";
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
            A.RunProperties runProperties2 = new A.RunProperties() {  FontSize = 1000 };//Language = QC4Common.Common.Constants.GlobalMode.Split(',')[0], AlternativeLanguage = QC4Common.Common.Constants.GlobalMode.Split(',')[0],

            //DocumentFormat.OpenXml.Wordprocessing.RunFonts rFont = new DocumentFormat.OpenXml.Wordprocessing.RunFonts();
            //rFont.Ascii = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            //rFont.ComplexScript = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            //rFont.EastAsia = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            //runProperties2.Append(rFont);
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
        public void GenerateSignificanceTestLegend(DrawingsPart drawingsPart, string value,int rowNum)
        {
            Xdr.TwoCellAnchor twoCellAnchor2 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.TwoCell };

            Xdr.FromMarker fromMarker2 = new Xdr.FromMarker();
            Xdr.ColumnId columnId3 = new Xdr.ColumnId();
            columnId3.Text = "7";
            Xdr.ColumnOffset columnOffset3 = new Xdr.ColumnOffset();
            columnOffset3.Text = "100025";
            Xdr.RowId rowId3 = new Xdr.RowId();
            rowId3.Text = rowNum.ToString();
            Xdr.RowOffset rowOffset3 = new Xdr.RowOffset();
            rowOffset3.Text = "0";

            fromMarker2.Append(columnId3);
            fromMarker2.Append(columnOffset3);
            fromMarker2.Append(rowId3);
            fromMarker2.Append(rowOffset3);

            Xdr.ToMarker toMarker2 = new Xdr.ToMarker();
            Xdr.ColumnId columnId4 = new Xdr.ColumnId();
            columnId4.Text = "9";
            Xdr.ColumnOffset columnOffset4 = new Xdr.ColumnOffset();
            columnOffset4.Text = "0";
            Xdr.RowId rowId4 = new Xdr.RowId();
            rowId4.Text = (rowNum +1).ToString();
            Xdr.RowOffset rowOffset4 = new Xdr.RowOffset();
            rowOffset4.Text = "0";

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
        public void GenerateRankingMarkingLegend(DrawingsPart drawingsPart, int rowNum)
        {
            CrossCreator crossCreator = new CrossCreator();
            Xdr.TwoCellAnchor twoCellAnchor3 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.TwoCell };

            Xdr.FromMarker fromMarker3 = new Xdr.FromMarker();
            Xdr.ColumnId columnId5 = new Xdr.ColumnId();
            columnId5.Text = "5";
            Xdr.ColumnOffset columnOffset5 = new Xdr.ColumnOffset();
            columnOffset5.Text = "57150";
            Xdr.RowId rowId5 = new Xdr.RowId();
            rowId5.Text = rowNum.ToString();
            Xdr.RowOffset rowOffset5 = new Xdr.RowOffset();
            rowOffset5.Text = "0";

            fromMarker3.Append(columnId5);
            fromMarker3.Append(columnOffset5);
            fromMarker3.Append(rowId5);
            fromMarker3.Append(rowOffset5);

            Xdr.ToMarker toMarker3 = new Xdr.ToMarker();
            Xdr.ColumnId columnId6 = new Xdr.ColumnId();
            columnId6.Text = "6";
            Xdr.ColumnOffset columnOffset6 = new Xdr.ColumnOffset();
            columnOffset6.Text = "123825";
            Xdr.RowId rowId6 = new Xdr.RowId();
            rowId6.Text = (rowNum +1).ToString();
            Xdr.RowOffset rowOffset6 = new Xdr.RowOffset();
            rowOffset6.Text = "0";

            toMarker3.Append(columnId6);
            toMarker3.Append(columnOffset6);
            toMarker3.Append(rowId6);
            toMarker3.Append(rowOffset6);

            Xdr.GroupShape groupShape1 = new Xdr.GroupShape();

            Xdr.NonVisualGroupShapeProperties nonVisualGroupShapeProperties1 = new Xdr.NonVisualGroupShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties3 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)4U, Name = "RankingMarkingLegend" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList3 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension3 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement4 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{781DC870-BC0F-4524-ACD3-68B212C281BC}\" />");

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
            A.Offset offset3 = new A.Offset() { X = 5322570L, Y = 1036320L };
            A.Extents extents3 = new A.Extents() { Cx = 737235L, Cy = 838200L };
            A.ChildOffset childOffset1 = new A.ChildOffset() { X = 412L, Y = 75L };
            A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = 73L, Cy = 88L };

            transformGroup1.Append(offset3);
            transformGroup1.Append(extents3);
            transformGroup1.Append(childOffset1);
            transformGroup1.Append(childExtents1);

            groupShapeProperties1.Append(transformGroup1);

            Xdr.Shape shape3 = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.NonVisualShapeProperties nonVisualShapeProperties3 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties4 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)5U, Name = "RankingMarkingLegend" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList4 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension4 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement5 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{BE26165D-5188-45CF-8E69-4112DE633EBD}\" />");

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
            A.Offset offset4 = new A.Offset() { X = 412L, Y = 75L };
            A.Extents extents4 = new A.Extents() { Cx = 73L, Cy = 88L };

            transform2D3.Append(offset4);
            transform2D3.Append(extents4);

            A.PresetGeometry presetGeometry3 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList3 = new A.AdjustValueList();

            presetGeometry3.Append(adjustValueList3);

            A.SolidFill solidFill10 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            solidFill10.Append(rgbColorModelHex11);

            A.Outline outline3 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill11 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex12 = new A.RgbColorModelHex() { Val = "A6A6A6" };

            solidFill11.Append(rgbColorModelHex12);
            A.PresetDash presetDash2 = new A.PresetDash() { Val = A.PresetLineDashValues.SystemDot };
            A.Miter miter2 = new A.Miter() { Limit = 800000 };
            A.HeadEnd headEnd3 = new A.HeadEnd();
            A.TailEnd tailEnd3 = new A.TailEnd();

            outline3.Append(solidFill11);
            outline3.Append(presetDash2);
            outline3.Append(miter2);
            outline3.Append(headEnd3);
            outline3.Append(tailEnd3);

            shapeProperties3.Append(transform2D3);
            shapeProperties3.Append(presetGeometry3);
            shapeProperties3.Append(solidFill10);
            shapeProperties3.Append(outline3);

            Xdr.TextBody textBody3 = new Xdr.TextBody();
            A.BodyProperties bodyProperties3 = new A.BodyProperties() { VerticalOverflow = A.TextVerticalOverflowValues.Clip, Wrap = A.TextWrappingValues.Square, LeftInset = 0, TopInset = 46800, RightInset = 0, BottomInset = 46800, Anchor = A.TextAnchoringTypeValues.Top };
            A.ListStyle listStyle3 = new A.ListStyle();

            A.Paragraph paragraph6 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties6 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Left, RightToLeft = false };
            A.DefaultRunProperties defaultRunProperties6 = new A.DefaultRunProperties() { FontSize = 1000 };

            paragraphProperties6.Append(defaultRunProperties6);

            A.EndParagraphRunProperties endParagraphRunProperties4 = new A.EndParagraphRunProperties() { Language = QC4Common.Common.Constants.GlobalMode.Split(',')[0]};

            paragraph6.Append(paragraphProperties6);
            paragraph6.Append(SetValues(LocalResource.REPORT_MARKING_LEGEND_RANKING_CAPTION));
            paragraph6.Append(endParagraphRunProperties4);

            textBody3.Append(bodyProperties3);
            textBody3.Append(listStyle3);
            textBody3.Append(paragraph6);

            shape3.Append(nonVisualShapeProperties3);
            shape3.Append(shapeProperties3);
            shape3.Append(textBody3);

            groupShape1.Append(nonVisualGroupShapeProperties1);
            groupShape1.Append(groupShapeProperties1);
            groupShape1.Append(shape3);
            groupShape1.Append(SetRankingOval(6, "Rank1Oval", "FF0000", 109, 10));
            groupShape1.Append(SetRankingLabel(7, "Rank1Label", LocalResource.REPORT_MARKING_LEGEND_RANKING_1ST_CAPTION, 105));
            groupShape1.Append(SetRankingOval(8, "Rank2Oval", "0000FF", 125, 12));
            groupShape1.Append(SetRankingLabel(9, "Rank2Label", LocalResource.REPORT_MARKING_LEGEND_RANKING_2ND_CAPTION, 121));
            groupShape1.Append(SetRankingOval(10, "Rank3Oval", "008000", 140, 17));
            groupShape1.Append(SetRankingLabel(11, "Rank3Label", LocalResource.REPORT_MARKING_LEGEND_RANKING_3RD_CAPTION, 136));
            Xdr.ClientData clientData3 = new Xdr.ClientData();

            twoCellAnchor3.Append(fromMarker3);
            twoCellAnchor3.Append(toMarker3);
            twoCellAnchor3.Append(groupShape1);
            twoCellAnchor3.Append(clientData3);
            drawingsPart.WorksheetDrawing.Append(twoCellAnchor3);
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
            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = QC4Common.Common.Constants.GlobalMode.Split(',')[0], FontSize = 900, Bold = false, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Baseline = 0 };

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
            A.Run run1 = new A.Run();

            A.RunProperties runProperties1 = new A.RunProperties() { Language = QC4Common.Common.Constants.GlobalMode.Split(',')[0], FontSize = 900, Bold = false, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Baseline = 0 };

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
        public Xdr.Shape SetRankingOval(int id, string labelName, string clr, long offsetY, int clrIdx)
        {
            Xdr.Shape shape = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.NonVisualShapeProperties nonVisualShapeProperties4 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties5 = new Xdr.NonVisualDrawingProperties() { Id = (uint)id, Name = labelName };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList5 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension5 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement6 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{580ACE60-9D8A-41E7-982D-31516B6EF7C6}\" />");

            nonVisualDrawingPropertiesExtension5.Append(openXmlUnknownElement6);

            nonVisualDrawingPropertiesExtensionList5.Append(nonVisualDrawingPropertiesExtension5);

            nonVisualDrawingProperties5.Append(nonVisualDrawingPropertiesExtensionList5);

            Xdr.NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties4 = new Xdr.NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks4 = new A.ShapeLocks() { NoChangeArrowheads = true };

            nonVisualShapeDrawingProperties4.Append(shapeLocks4);

            nonVisualShapeProperties4.Append(nonVisualDrawingProperties5);
            nonVisualShapeProperties4.Append(nonVisualShapeDrawingProperties4);

            Xdr.ShapeProperties shapeProperties4 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D4 = new A.Transform2D();
            A.Offset offset5 = new A.Offset() { X = 427L, Y = offsetY };
            A.Extents extents5 = new A.Extents() { Cx = 6L, Cy = 7L };

            transform2D4.Append(offset5);
            transform2D4.Append(extents5);

            A.PresetGeometry presetGeometry4 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList4 = new A.AdjustValueList();

            presetGeometry4.Append(adjustValueList4);

            A.Outline outline4 = new A.Outline() { Width = 9525 };

            A.Round round2 = new A.Round();
            A.HeadEnd headEnd4 = new A.HeadEnd();
            A.TailEnd tailEnd4 = new A.TailEnd();

            outline4.Append(SetSolidFill(clr, clrIdx));
            outline4.Append(round2);
            outline4.Append(headEnd4);
            outline4.Append(tailEnd4);

            shapeProperties4.Append(transform2D4);
            shapeProperties4.Append(presetGeometry4);
            shapeProperties4.Append(SetSolidFill(clr, clrIdx));
            shapeProperties4.Append(outline4);

            shape.Append(nonVisualShapeProperties4);
            shape.Append(shapeProperties4);
            return shape;
        }
        public Xdr.Shape SetRankingLabel(int id, string labelName, string val, long offsetY)
        {
            Xdr.Shape shape = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.NonVisualShapeProperties nonVisualShapeProperties5 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties6 = new Xdr.NonVisualDrawingProperties() { Id = (uint)id, Name = labelName };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList6 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension6 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement7 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{ED436DA1-F7AD-45CD-BA3E-64185A7F5926}\" />");

            nonVisualDrawingPropertiesExtension6.Append(openXmlUnknownElement7);

            nonVisualDrawingPropertiesExtensionList6.Append(nonVisualDrawingPropertiesExtension6);

            nonVisualDrawingProperties6.Append(nonVisualDrawingPropertiesExtensionList6);

            Xdr.NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties5 = new Xdr.NonVisualShapeDrawingProperties() { TextBox = true };
            A.ShapeLocks shapeLocks5 = new A.ShapeLocks() { NoChangeArrowheads = true };

            nonVisualShapeDrawingProperties5.Append(shapeLocks5);

            nonVisualShapeProperties5.Append(nonVisualDrawingProperties6);
            nonVisualShapeProperties5.Append(nonVisualShapeDrawingProperties5);

            Xdr.ShapeProperties shapeProperties5 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D5 = new A.Transform2D();
            A.Offset offset6 = new A.Offset() { X = 444L, Y = offsetY };
            A.Extents extents6 = new A.Extents() { Cx = 18L, Cy = 16L };

            transform2D5.Append(offset6);
            transform2D5.Append(extents6);

            A.PresetGeometry presetGeometry5 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList5 = new A.AdjustValueList();

            presetGeometry5.Append(adjustValueList5);
            A.NoFill noFill1 = new A.NoFill();

            A.Outline outline5 = new A.Outline();
            A.NoFill noFill2 = new A.NoFill();

            outline5.Append(noFill2);

            A.ShapePropertiesExtensionList shapePropertiesExtensionList1 = new A.ShapePropertiesExtensionList();

            A.ShapePropertiesExtension shapePropertiesExtension1 = new A.ShapePropertiesExtension() { Uri = "{909E8E84-426E-40DD-AFC4-6F175D3DCCD1}" };

            A14.HiddenFillProperties hiddenFillProperties1 = new A14.HiddenFillProperties();

            hiddenFillProperties1.Append(SetSolidFill("FFFFFF", 65));

            shapePropertiesExtension1.Append(hiddenFillProperties1);

            A.ShapePropertiesExtension shapePropertiesExtension2 = new A.ShapePropertiesExtension() { Uri = "{91240B29-F687-4F45-9708-019B960494DF}" };

            A14.HiddenLineProperties hiddenLineProperties1 = new A14.HiddenLineProperties() { Width = 9525 };

            A.Miter miter3 = new A.Miter() { Limit = 800000 };
            A.HeadEnd headEnd5 = new A.HeadEnd();
            A.TailEnd tailEnd5 = new A.TailEnd();

            hiddenLineProperties1.Append(SetSolidFill("000000", 64));
            hiddenLineProperties1.Append(miter3);
            hiddenLineProperties1.Append(headEnd5);
            hiddenLineProperties1.Append(tailEnd5);

            shapePropertiesExtension2.Append(hiddenLineProperties1);

            shapePropertiesExtensionList1.Append(shapePropertiesExtension1);
            shapePropertiesExtensionList1.Append(shapePropertiesExtension2);

            shapeProperties5.Append(transform2D5);
            shapeProperties5.Append(presetGeometry5);
            shapeProperties5.Append(noFill1);
            shapeProperties5.Append(outline5);
            shapeProperties5.Append(shapePropertiesExtensionList1);

            Xdr.TextBody textBody = new Xdr.TextBody();

            A.BodyProperties bodyProperties4 = new A.BodyProperties() { Wrap = A.TextWrappingValues.None, LeftInset = 0, TopInset = 0, RightInset = 0, BottomInset = 0, Anchor = A.TextAnchoringTypeValues.Top, UpRight = true };
            A.ShapeAutoFit shapeAutoFit1 = new A.ShapeAutoFit();

            bodyProperties4.Append(shapeAutoFit1);
            A.ListStyle listStyle4 = new A.ListStyle();

            A.Paragraph paragraph = new A.Paragraph();
            A.EndParagraphRunProperties endParagraphRunProperties5 = new A.EndParagraphRunProperties() { Language = QC4Common.Common.Constants.GlobalMode.Split(',')[0] };
            paragraph.Append(SetParagraphProperty());
            paragraph.Append(SetValues(val));
            paragraph.Append(endParagraphRunProperties5);

            textBody.Append(bodyProperties4);
            textBody.Append(listStyle4);
            textBody.Append(paragraph);

            shape.Append(nonVisualShapeProperties5);
            shape.Append(shapeProperties5);
            shape.Append(textBody);
            return shape;
        }
        public A.SolidFill SetSolidFill(string clr, int clrIdx)
        {
            A.SolidFill solidFill1 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex = new A.RgbColorModelHex() { Val = clr, LegacySpreadsheetColorIndex = clrIdx, MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "a14" } };

            solidFill1.Append(rgbColorModelHex);
            return solidFill1;
        }
        public string GetSeriesTextValue(WorksheetPart worksheetPart,int row,int col)
        {
            Row row1 = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
            Cell cell = OpenXmlHelper.GetCell(row1, row,col);
            if (cell == null)
                return null;
            else
            return cell.CellValue == null ? "" : (cell.CellValue.InnerText);
        }
    }
}
