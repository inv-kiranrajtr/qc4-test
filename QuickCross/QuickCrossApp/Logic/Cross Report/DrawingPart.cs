using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Common.Constants;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using Qc4Launcher.Util;
using static Macromill.QCWeb.Batch.Report.Tables;
using C14 = DocumentFormat.OpenXml.Office2010.Drawing.Charts;
using C15 = DocumentFormat.OpenXml.Office2013.Drawing.Chart;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Qc4Launcher.Logic.Cross_Report
{
    public class DrawingPart
    {
        private const double PlotLeftX = 0.08;
        private const double PlotFullWidth = 0.90;

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

        public static void GenerateTitleBox(DrawingsPart drawingsPart, string title)
        {
            Xdr.WorksheetDrawing worksheetDrawing1 = new Xdr.WorksheetDrawing();

            Xdr.TwoCellAnchor twoCellAnchor1 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.Absolute };

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
            columnId2.Text = "5";
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
            A.RunProperties runProperties2 = new A.RunProperties() { Language = QC4Common.Common.Constants.GlobalMode.Split(',')[0], AlternativeLanguage = QC4Common.Common.Constants.GlobalMode.Split(',')[0], FontSize = 1000 };
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
        public static void GenerateDrawingShapeOval(DrawingsPart drawingsPart,string row,string col,string colur)
        {
            Xdr.WorksheetDrawing worksheetDrawing = null;//new Xdr.WorksheetDrawing();
            
            Xdr.TwoCellAnchor twoCellAnchor1 = new Xdr.TwoCellAnchor();

            Xdr.FromMarker fromMarker1 = new Xdr.FromMarker();
            Xdr.ColumnId columnId1 = new Xdr.ColumnId();
            columnId1.Text = col;
            Xdr.ColumnOffset columnOffset1 = new Xdr.ColumnOffset();
            columnOffset1.Text = "28575";
            Xdr.RowId rowId1 = new Xdr.RowId();
            rowId1.Text = row;
            Xdr.RowOffset rowOffset1 = new Xdr.RowOffset();
            rowOffset1.Text = "28575";

            fromMarker1.Append(columnId1);
            fromMarker1.Append(columnOffset1);
            fromMarker1.Append(rowId1);
            fromMarker1.Append(rowOffset1);

            Xdr.ToMarker toMarker1 = new Xdr.ToMarker();
            Xdr.ColumnId columnId2 = new Xdr.ColumnId();
            columnId2.Text = col;
            Xdr.ColumnOffset columnOffset2 = new Xdr.ColumnOffset();
            columnOffset2.Text = "104775";
            Xdr.RowId rowId2 = new Xdr.RowId();
            rowId2.Text = row;
            Xdr.RowOffset rowOffset2 = new Xdr.RowOffset();
            rowOffset2.Text = "104775";

            toMarker1.Append(columnId2);
            toMarker1.Append(columnOffset2);
            toMarker1.Append(rowId2);
            toMarker1.Append(rowOffset2);

            Xdr.Shape shape1 = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.NonVisualShapeProperties nonVisualShapeProperties1 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)22U, Name = "Oval 21" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList1 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension1 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement2 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{86296C6A-1F04-49EB-98A3-6C99549EABEB}\" />");

            nonVisualDrawingPropertiesExtension1.Append(openXmlUnknownElement2);

            nonVisualDrawingPropertiesExtensionList1.Append(nonVisualDrawingPropertiesExtension1);

            nonVisualDrawingProperties1.Append(nonVisualDrawingPropertiesExtensionList1);
            Xdr.NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties1 = new Xdr.NonVisualShapeDrawingProperties();

            nonVisualShapeProperties1.Append(nonVisualDrawingProperties1);
            nonVisualShapeProperties1.Append(nonVisualShapeDrawingProperties1);

            Xdr.ShapeProperties shapeProperties1 = new Xdr.ShapeProperties();

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset1 = new A.Offset() { X = 4036695L, Y = 5431155L };
            A.Extents extents1 = new A.Extents() { Cx = 76200L, Cy = 76200L };

            transform2D1.Append(offset1);
            transform2D1.Append(extents1);

            A.PresetGeometry presetGeometry1 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();

            presetGeometry1.Append(adjustValueList1);

            A.SolidFill solidFill6 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex14 = new A.RgbColorModelHex() { Val = colur };

            solidFill6.Append(rgbColorModelHex14);

            A.Outline outline4 = new A.Outline() { Width = 9525 };

            A.SolidFill solidFill7 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex15 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            solidFill7.Append(rgbColorModelHex15);

            outline4.Append(solidFill7);

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(presetGeometry1);
            shapeProperties1.Append(solidFill6);
            shapeProperties1.Append(outline4);

            Xdr.TextBody textBody1 = new Xdr.TextBody();
            A.BodyProperties bodyProperties1 = new A.BodyProperties() { VerticalOverflow = A.TextVerticalOverflowValues.Clip, HorizontalOverflow = A.TextHorizontalOverflowValues.Clip, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Top };
            A.ListStyle listStyle1 = new A.ListStyle();

            A.Paragraph paragraph1 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Left };
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = "en-IN", FontSize = 1100 };

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(endParagraphRunProperties1);

            textBody1.Append(bodyProperties1);
            textBody1.Append(listStyle1);
            textBody1.Append(paragraph1);

            shape1.Append(nonVisualShapeProperties1);
            shape1.Append(shapeProperties1);
            shape1.Append(textBody1);
            Xdr.ClientData clientData1 = new Xdr.ClientData();

            twoCellAnchor1.Append(fromMarker1);
            twoCellAnchor1.Append(toMarker1);
            twoCellAnchor1.Append(shape1);
            twoCellAnchor1.Append(clientData1);
            if (drawingsPart.WorksheetDrawing == null)
            {
                worksheetDrawing = new Xdr.WorksheetDrawing();
                worksheetDrawing.Append(twoCellAnchor1);

                drawingsPart.WorksheetDrawing = worksheetDrawing;
            }
            else
            {
                drawingsPart.WorksheetDrawing.Append(twoCellAnchor1);
            }
        }
        public static void GenerateMinBaseTextShape(DrawingsPart drawingsPart,string text,string rowOffset,string row,string srcColumn = "1",string srcColumnOffset = "0")
        {
            Xdr.OneCellAnchor oneCellAnchor1 = new Xdr.OneCellAnchor();

            Xdr.FromMarker fromMarker1 = new Xdr.FromMarker();
            Xdr.ColumnId columnId1 = new Xdr.ColumnId();
            columnId1.Text = srcColumn;
            Xdr.ColumnOffset columnOffset1 = new Xdr.ColumnOffset();
            columnOffset1.Text = srcColumnOffset;
            Xdr.RowId rowId1 = new Xdr.RowId();
            rowId1.Text = row;
            Xdr.RowOffset rowOffset1 = new Xdr.RowOffset();
            rowOffset1.Text = rowOffset;

            fromMarker1.Append(columnId1);
            fromMarker1.Append(columnOffset1);
            fromMarker1.Append(rowId1);
            fromMarker1.Append(rowOffset1);
            Xdr.Extent extent1 = new Xdr.Extent() { Cx = 653256L, Cy = 168508L };
            Xdr.ClientData clientData1 = new Xdr.ClientData();
            oneCellAnchor1.Append(fromMarker1);
            oneCellAnchor1.Append(extent1);
            oneCellAnchor1.Append(CreateTextShapeRectangle(text, 121920, 2293466, 653256, 168508));
            oneCellAnchor1.Append(clientData1);

            Xdr.WorksheetDrawing worksheetDrawing = null;
            if (drawingsPart.WorksheetDrawing == null)
            {
                worksheetDrawing = new Xdr.WorksheetDrawing();
                worksheetDrawing.Append(oneCellAnchor1);

                drawingsPart.WorksheetDrawing = worksheetDrawing;
            }
            else
            {
                drawingsPart.WorksheetDrawing.Append(oneCellAnchor1);
            }

        }
        public static void GenerateSignificanceTestLegend(DrawingsPart drawingsPart, string srcRowOfst, string srcRow, string dstRowOfst, string dstRow, string value, int rowNum,string srcCol = "1",string srcColOfst = "0",string dstCol= "2",string dstColOfst = "260747")
        {
            Xdr.TwoCellAnchor twoCellAnchor2 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.Absolute };

            Xdr.FromMarker fromMarker3 = new Xdr.FromMarker();
            Xdr.ColumnId columnId4 = new Xdr.ColumnId();
            columnId4.Text = srcCol;
            Xdr.ColumnOffset columnOffset4 = new Xdr.ColumnOffset();
            columnOffset4.Text = srcColOfst;
            Xdr.RowId rowId4 = new Xdr.RowId();
            rowId4.Text = srcRow;
            Xdr.RowOffset rowOffset4 = new Xdr.RowOffset();
            rowOffset4.Text = srcRowOfst;

            fromMarker3.Append(columnId4);
            fromMarker3.Append(columnOffset4);
            fromMarker3.Append(rowId4);
            fromMarker3.Append(rowOffset4);

            Xdr.ToMarker toMarker2 = new Xdr.ToMarker();
            Xdr.ColumnId columnId5 = new Xdr.ColumnId();
            columnId5.Text = dstCol;
            Xdr.ColumnOffset columnOffset5 = new Xdr.ColumnOffset();
            columnOffset5.Text = dstColOfst;
            Xdr.RowId rowId5 = new Xdr.RowId();
            rowId5.Text = dstRow;
            Xdr.RowOffset rowOffset5 = new Xdr.RowOffset();
            rowOffset5.Text = dstRowOfst;

            toMarker2.Append(columnId5);
            toMarker2.Append(columnOffset5);
            toMarker2.Append(rowId5);
            toMarker2.Append(rowOffset5);

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

            twoCellAnchor2.Append(fromMarker3);
            twoCellAnchor2.Append(toMarker2);
            twoCellAnchor2.Append(shape2);
            twoCellAnchor2.Append(clientData2);

            Xdr.WorksheetDrawing worksheetDrawing = null;
            if (drawingsPart.WorksheetDrawing == null)
            {
                worksheetDrawing = new Xdr.WorksheetDrawing();
                worksheetDrawing.Append(twoCellAnchor2);

                drawingsPart.WorksheetDrawing = worksheetDrawing;
            }
            else
            {
                drawingsPart.WorksheetDrawing.Append(twoCellAnchor2);
            }
        }
        public static void GenerateMarkingColoring(DrawingsPart drawingsPart, OutputCross CurrentOutput, string value, int rowNum, string srcCol = "1", string srcColOfst = "0", string srcRow = "27", string srcRowOfst = "609600", string dstCol = "2", string dstColOfst = "190500", string dstRow = "27", string dstRowOfst = "1447800")
        {
            long x, y, cx, cy, clrX, clrY, clrCx, clrCy;
            string clrIdx;          
            NPOICrossCreator nPOICrossCreator = new NPOICrossCreator();
            Xdr.TwoCellAnchor twoCellAnchor3 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.Absolute };

            Xdr.FromMarker fromMarker2 = new Xdr.FromMarker();
            Xdr.ColumnId columnId2 = new Xdr.ColumnId();
            columnId2.Text = srcCol;
            Xdr.ColumnOffset columnOffset2 = new Xdr.ColumnOffset();
            columnOffset2.Text =srcColOfst;
            Xdr.RowId rowId2 = new Xdr.RowId();
            rowId2.Text = srcRow;
            Xdr.RowOffset rowOffset2 = new Xdr.RowOffset();
            rowOffset2.Text = srcRowOfst;

            fromMarker2.Append(columnId2);
            fromMarker2.Append(columnOffset2);
            fromMarker2.Append(rowId2);
            fromMarker2.Append(rowOffset2);

            Xdr.ToMarker toMarker1 = new Xdr.ToMarker();
            Xdr.ColumnId columnId3 = new Xdr.ColumnId();
            columnId3.Text =dstCol;
            Xdr.ColumnOffset columnOffset3 = new Xdr.ColumnOffset();
            columnOffset3.Text = dstColOfst;
            Xdr.RowId rowId3 = new Xdr.RowId();
            rowId3.Text = dstRow;
            Xdr.RowOffset rowOffset3 = new Xdr.RowOffset();
            rowOffset3.Text = dstRowOfst;

            toMarker1.Append(columnId3);
            toMarker1.Append(columnOffset3);
            toMarker1.Append(rowId3);
            toMarker1.Append(rowOffset3);

            Xdr.GroupShape groupShape = new Xdr.GroupShape();

            Xdr.NonVisualGroupShapeProperties nonVisualGroupShapeProperties1 = new Xdr.NonVisualGroupShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties3 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)31U, Name = "RateDifferenceLegend" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList3 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension3 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement4 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{00000000-0008-0000-0000-00001F000000}\" />");

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
            A.Offset offset3 = new A.Offset() { X = 4954905L, Y = 685800L };
            A.Extents extents3 = new A.Extents() { Cx = 990600L, Cy = 838200L };
            A.ChildOffset childOffset1 = new A.ChildOffset() { X = 93L, Y = 83L };
            A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = 104L, Cy = 88L };

            transformGroup1.Append(offset3);
            transformGroup1.Append(extents3);
            transformGroup1.Append(childOffset1);
            transformGroup1.Append(childExtents1);

            groupShapeProperties1.Append(transformGroup1);

            Xdr.Shape shape = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.NonVisualShapeProperties nonVisualShapeProperties3 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties4 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)32U, Name = "RateDifferenceLegend" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList4 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension4 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement5 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{00000000-0008-0000-0000-000020000000}\" />");

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
            A.Offset offset4 = new A.Offset() { X = 93L, Y = 83L };
            A.Extents extents4 = new A.Extents() { Cx = 104L, Cy = 88L };

            transform2D3.Append(offset4);
            transform2D3.Append(extents4);

            A.PresetGeometry presetGeometry3 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList3 = new A.AdjustValueList();

            presetGeometry3.Append(adjustValueList3);

            A.SolidFill solidFill16 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex25 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            solidFill16.Append(rgbColorModelHex25);

            A.Outline outline6 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill17 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex26 = new A.RgbColorModelHex() { Val = "A6A6A6" };

            solidFill17.Append(rgbColorModelHex26);
            A.PresetDash presetDash5 = new A.PresetDash() { Val = A.PresetLineDashValues.SystemDot };
            A.Miter miter2 = new A.Miter() { Limit = 800000 };
            A.HeadEnd headEnd3 = new A.HeadEnd();
            A.TailEnd tailEnd3 = new A.TailEnd();

            outline6.Append(solidFill17);
            outline6.Append(presetDash5);
            outline6.Append(miter2);
            outline6.Append(headEnd3);
            outline6.Append(tailEnd3);

            shapeProperties3.Append(transform2D3);
            shapeProperties3.Append(presetGeometry3);
            shapeProperties3.Append(solidFill16);
            shapeProperties3.Append(outline6);

            shape.Append(nonVisualShapeProperties3);
            shape.Append(shapeProperties3);

            Xdr.TextBody textBody = new Xdr.TextBody();
            A.BodyProperties bodyProperties3 = new A.BodyProperties() { VerticalOverflow = A.TextVerticalOverflowValues.Clip, Wrap = A.TextWrappingValues.Square, LeftInset = 0, TopInset = 46800, RightInset = 0, BottomInset = 46800, Anchor = A.TextAnchoringTypeValues.Top };
            A.ListStyle listStyle3 = new A.ListStyle();

            A.Paragraph paragraph3 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Left, RightToLeft = false };
            A.DefaultRunProperties defaultRunProperties3 = new A.DefaultRunProperties() { FontSize = 1000 };

            paragraphProperties3.Append(defaultRunProperties3);

            A.RunProperties runProperties10 = new A.RunProperties() { Language = "en-US", AlternativeLanguage = "ja-JP", FontSize = 900, Bold = false, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Baseline = 0 };

            A.SolidFill solidFill20 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex29 = new A.RgbColorModelHex() { Val = "000000" };

            solidFill20.Append(rgbColorModelHex29);
            A.LatinFont latinFont12 = new A.LatinFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };
            A.EastAsianFont eastAsianFont12 = new A.EastAsianFont() { Typeface = QC4Common.Common.Constants.GlobalMode.Split(',')[1] };

            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = "ja-JP", AlternativeLanguage = "en-US" };

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(SetValues(value));
            paragraph3.Append(endParagraphRunProperties2);

            textBody.Append(bodyProperties3);
            textBody.Append(listStyle3);
            textBody.Append(paragraph3);

            shape.Append(textBody);
            groupShape.Append(nonVisualGroupShapeProperties1);
            groupShape.Append(groupShapeProperties1);
            groupShape.Append(shape);
            //
            //bool showLevel1High = true;
            //bool showLevel1Low = true;
            bool showLevel1High = !CurrentOutput.MarkingColoringLevel2High && CurrentOutput.MarkingColoringLevel1High ? false : true;
            bool showLevel1Low = !CurrentOutput.MarkingColoringLevel2Low ? false : true;
            bool showLevel2High = !CurrentOutput.MarkingColoringLevel2High && CurrentOutput.MarkingColoringLevel1High ? true : false;
            bool showlevel2Low = !CurrentOutput.MarkingColoringLevel2Low && CurrentOutput.MarkingColoringLevel1Low ? true : false;
            int clrPos = 0;
            if (CurrentOutput.Level1Percent == CurrentOutput.Level2Percent)
            {
                if (CurrentOutput.MarkingColoringLevel2High)
                {
                    showLevel1High = false;
                }
                if (CurrentOutput.MarkingColoringLevel2Low)
                {
                    showLevel1Low = false;
                }
            }
            bool globalMode = QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? false : true;
            if (CurrentOutput.MarkingColoringLevel2High)
            {
                string Text;
                if (!globalMode)
                {
                    Text = string.Format(LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION
                                 , (" " + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                          , ((CurrentOutput.Level2Percent.ToString().Length > 1 ? " +" : "  +") + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                }
                x = 133; y = 106; cx = 61; cy = 16;
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level2HighColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");              
                clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
                clrPos++;
            }
            else if (showLevel2High)
            {
                string Text;
                if (!globalMode)
                {
                    Text = string.Format(Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION, (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                         , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? " +" : "  +") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }

                x = 133; y = 106; cx = 61; cy = 16;
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level2HighColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
                clrPos++;

            }
            if (CurrentOutput.MarkingColoringLevel1High && showLevel1High)
            {
                string Text;
                if (!globalMode)
                {
                    Text = string.Format(Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_HIGH_CAPTION, (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                         , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? " +" : "  +") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                if (clrPos == 1)
                {
                    x = 133; y = 122; cx = 61; cy = 16;
                    clrX = 97; clrY = 121; clrCx = 33; clrCy = 17;
                }
                else
                {
                    x = 133; y = 106; cx = 61; cy = 16;
                    clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                }
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level1HighColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
                clrPos++;
            }
            if (CurrentOutput.MarkingColoringLevel1Low && showLevel1Low)
            {
                string Text;
                if (!globalMode)
                {
                    Text = string.Format(
                      Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                       , (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                           , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? "  -" : "   -") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                if (clrPos == 1)
                {
                    x = 133; y = 122; cx = 61; cy = 16;
                    clrX = 97; clrY = 121; clrCx = 33; clrCy = 17;
                }
                else if (clrPos == 2)
                {
                    x = 133; y = 138; cx = 61; cy = 16;
                    clrX = 97; clrY = 137; clrCx = 33; clrCy = 16;
                }
                else
                {
                    x = 133; y = 106; cx = 61; cy = 16;
                    clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                }
               
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level1LowColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
                clrPos++;
            }
            if (CurrentOutput.MarkingColoringLevel2Low)
            {
                string Text;
                if (!globalMode)
                {
                    Text = string.Format(
                       Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                       , (" " + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                           , ((CurrentOutput.Level2Percent.ToString().Length > 1 ? "  -" : "   -") + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                }
                if (clrPos == 1)
                {
                    x = 133; y = 122; cx = 61; cy = 16;
                    clrX = 97; clrY = 121; clrCx = 33; clrCy = 17;
                }
                else if (clrPos == 2)
                {
                    x = 133; y = 138; cx = 61; cy = 16;
                    clrX = 97; clrY = 137; clrCx = 33; clrCy = 16;
                }
                else if (clrPos == 3)
                {
                    x = 133; y = 154; cx = 61; cy = 16;
                    clrX = 97; clrY = 153; clrCx = 33; clrCy = 16;
                }
                else
                {
                    x = 133; y = 106; cx = 61; cy = 16;
                    clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                }             
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level2LowColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));
            }
            else if (showlevel2Low)
            {
                string Text;
                if (!globalMode)
                {
                    Text = string.Format(
                      Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RATE_DIFFERENCE_LOW_CAPTION
                       , (" " + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }
                else
                {
                    Text = string.Format("Total {0} Points"
                           , ((CurrentOutput.Level1Percent.ToString().Length > 1 ? "  -" : "   -") + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                }

                if (clrPos == 1)
                {
                    x = 133; y = 122; cx = 61; cy = 16;
                    clrX = 97; clrY = 121; clrCx = 33; clrCy = 17;
                }
                else if (clrPos == 2)
                {
                    x = 133; y = 138; cx = 61; cy = 16;
                    clrX = 97; clrY = 137; clrCx = 33; clrCy = 16;
                }
                else if (clrPos == 3)
                {
                    x = 133; y = 154; cx = 61; cy = 16;
                    clrX = 97; clrY = 153; clrCx = 33; clrCy = 16;
                }
                else
                {
                    x = 133; y = 106; cx = 61; cy = 16;
                    clrX = 97; clrY = 105; clrCx = 33; clrCy = 16;
                }
                groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));
                var clr = System.Drawing.Color.FromArgb((int)CurrentOutput.Level2LowColorIndex);
                clrIdx = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");
                groupShape.Append(CreateTextShapeRectangle(null, clrX, clrY, clrCx, clrCy, clrIdx));

            }
            Xdr.ClientData clientData3 = new Xdr.ClientData();

            twoCellAnchor3.Append(fromMarker2);
            twoCellAnchor3.Append(toMarker1);
            twoCellAnchor3.Append(groupShape);
            twoCellAnchor3.Append(clientData3);

            Xdr.WorksheetDrawing worksheetDrawing = null;
            if (drawingsPart.WorksheetDrawing == null)
            {
                worksheetDrawing = new Xdr.WorksheetDrawing();
                worksheetDrawing.Append(twoCellAnchor3);

                drawingsPart.WorksheetDrawing = worksheetDrawing;
            }
            else
            {
                drawingsPart.WorksheetDrawing.Append(twoCellAnchor3);
            }
        }
        public static void GenerateMarkingRanking(DrawingsPart drawingsPart,string srcRowOfst,string srcRow,string dstRowOfst,string dstRow
                                                  ,string srcColOfst, string srcCol, string dstColOfst, string dstCol)
        {
            long x, y, cx, cy;
            string Text;
            NPOICrossCreator nPOICrossCreator = new NPOICrossCreator();
            Xdr.TwoCellAnchor twoCellAnchor3 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.OneCell };

            Xdr.FromMarker fromMarker4 = new Xdr.FromMarker();
            Xdr.ColumnId columnId6 = new Xdr.ColumnId();
            columnId6.Text = srcCol;
            Xdr.ColumnOffset columnOffset6 = new Xdr.ColumnOffset();
            columnOffset6.Text = srcColOfst;
            Xdr.RowId rowId6 = new Xdr.RowId();
            rowId6.Text = srcRow;
            Xdr.RowOffset rowOffset6 = new Xdr.RowOffset();
            rowOffset6.Text = srcRowOfst;

            fromMarker4.Append(columnId6);
            fromMarker4.Append(columnOffset6);
            fromMarker4.Append(rowId6);
            fromMarker4.Append(rowOffset6);

            Xdr.ToMarker toMarker3 = new Xdr.ToMarker();
            Xdr.ColumnId columnId7 = new Xdr.ColumnId();
            columnId7.Text = dstCol;
            Xdr.ColumnOffset columnOffset7 = new Xdr.ColumnOffset();
            columnOffset7.Text = dstColOfst;
            Xdr.RowId rowId7 = new Xdr.RowId();
            rowId7.Text = dstRow;
            Xdr.RowOffset rowOffset7 = new Xdr.RowOffset();
            rowOffset7.Text = dstRowOfst;

            toMarker3.Append(columnId7);
            toMarker3.Append(columnOffset7);
            toMarker3.Append(rowId7);
            toMarker3.Append(rowOffset7);

            Xdr.GroupShape groupShape = new Xdr.GroupShape();

            Xdr.NonVisualGroupShapeProperties nonVisualGroupShapeProperties1 = new Xdr.NonVisualGroupShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties2 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)14U, Name = "RankingMarkingLegend" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList2 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension2 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement3 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{70887B14-902B-4572-9ADE-E455BB0B2DF5}\" />");

            nonVisualDrawingPropertiesExtension2.Append(openXmlUnknownElement3);

            nonVisualDrawingPropertiesExtensionList2.Append(nonVisualDrawingPropertiesExtension2);

            nonVisualDrawingProperties2.Append(nonVisualDrawingPropertiesExtensionList2);

            Xdr.NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties1 = new Xdr.NonVisualGroupShapeDrawingProperties();
            A.GroupShapeLocks groupShapeLocks1 = new A.GroupShapeLocks();

            nonVisualGroupShapeDrawingProperties1.Append(groupShapeLocks1);

            nonVisualGroupShapeProperties1.Append(nonVisualDrawingProperties2);
            nonVisualGroupShapeProperties1.Append(nonVisualGroupShapeDrawingProperties1);

            Xdr.GroupShapeProperties groupShapeProperties1 = new Xdr.GroupShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.TransformGroup transformGroup1 = new A.TransformGroup();
            A.Offset offset2 = new A.Offset() { X = 121920L, Y = 4351020L };
            A.Extents extents2 = new A.Extents() { Cx = 695325L, Cy = 838200L };
            A.ChildOffset childOffset1 = new A.ChildOffset() { X = 412L, Y = 75L };
            A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = 73L, Cy = 88L };

            transformGroup1.Append(offset2);
            transformGroup1.Append(extents2);
            transformGroup1.Append(childOffset1);
            transformGroup1.Append(childExtents1);

            groupShapeProperties1.Append(transformGroup1);

            Xdr.Shape shape = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.ShapeProperties shapeProperties2 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D2 = new A.Transform2D();
            A.Offset offset3 = new A.Offset() { X = 412L, Y = 75L };
            A.Extents extents3 = new A.Extents() { Cx = 73L, Cy = 88L };

            transform2D2.Append(offset3);
            transform2D2.Append(extents3);

            A.PresetGeometry presetGeometry2 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList2 = new A.AdjustValueList();

            presetGeometry2.Append(adjustValueList2);

            A.SolidFill solidFill10 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex18 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            solidFill10.Append(rgbColorModelHex18);

            A.Outline outline5 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill11 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex19 = new A.RgbColorModelHex() { Val = "A6A6A6" };

            solidFill11.Append(rgbColorModelHex19);
            A.PresetDash presetDash4 = new A.PresetDash() { Val = A.PresetLineDashValues.SystemDot };
            A.Miter miter2 = new A.Miter() { Limit = 800000 };
            A.HeadEnd headEnd2 = new A.HeadEnd();
            A.TailEnd tailEnd2 = new A.TailEnd();

            outline5.Append(solidFill11);
            outline5.Append(presetDash4);
            outline5.Append(miter2);
            outline5.Append(headEnd2);
            outline5.Append(tailEnd2);

            shapeProperties2.Append(transform2D2);
            shapeProperties2.Append(presetGeometry2);
            shapeProperties2.Append(solidFill10);
            shapeProperties2.Append(outline5);

            Xdr.TextBody textBody2 = new Xdr.TextBody();
            A.BodyProperties bodyProperties2 = new A.BodyProperties() { VerticalOverflow = A.TextVerticalOverflowValues.Clip, Wrap = A.TextWrappingValues.Square, LeftInset = 0, TopInset = 46800, RightInset = 0, BottomInset = 46800, Anchor = A.TextAnchoringTypeValues.Top };
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Left, RightToLeft = false };
            A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties() { FontSize = 1000 };

            paragraphProperties2.Append(defaultRunProperties2);

            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = "ja-JP", AlternativeLanguage = "en-US" };

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(SetValues(Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RANKING_CAPTION));
            paragraph2.Append(endParagraphRunProperties2);

            textBody2.Append(bodyProperties2);
            textBody2.Append(listStyle2);
            textBody2.Append(paragraph2);

            shape.Append(NonVisualShapeProperty());
            shape.Append(shapeProperties2);
            shape.Append(textBody2);

            groupShape.Append(nonVisualGroupShapeProperties1);
            groupShape.Append(groupShapeProperties1);
            groupShape.Append(shape);
          
            x = 427; y = 109; cx = 6; cy = 7;
            groupShape.Append(CreateTextShapeOval(null, x, y, cx, cy, "FF0000"));

            Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_1ST_CAPTION;
            x = 444; y = 105; cx = 18; cy = 16;
            groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));

            x = 427; y = 125; cx = 6; cy = 7;
            groupShape.Append(CreateTextShapeOval(null, x, y, cx, cy, "0000FF"));

            Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_2ND_CAPTION;
            x = 444; y = 121; cx = 18; cy = 16;
            groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));

            x = 427; y = 140; cx = 6; cy = 7;
            groupShape.Append(CreateTextShapeOval(null, x, y, cx, cy, "008000"));

            Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_3RD_CAPTION;
            x = 444; y = 136; cx = 18; cy = 16;
            groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));

            Xdr.ClientData clientData2 = new Xdr.ClientData();
            twoCellAnchor3.Append(fromMarker4);
            twoCellAnchor3.Append(toMarker3);
            twoCellAnchor3.Append(groupShape);
            twoCellAnchor3.Append(clientData2);           

            Xdr.WorksheetDrawing worksheetDrawing = null;
            if (drawingsPart.WorksheetDrawing == null)
            {
                worksheetDrawing = new Xdr.WorksheetDrawing();
                worksheetDrawing.Append(twoCellAnchor3);

                drawingsPart.WorksheetDrawing = worksheetDrawing;
            }
            else
            {
                drawingsPart.WorksheetDrawing.Append(twoCellAnchor3);
            }
        }

        public static void GenerateMarkingRankingGlobal(DrawingsPart drawingsPart, string srcRowOfst, string srcRow, string dstRowOfst, string dstRow
                                                  , string srcColOfst, string srcCol, string dstColOfst, string dstCol)
        {
            long x, y, cx, cy;
            string Text;
            NPOICrossCreator nPOICrossCreator = new NPOICrossCreator();
            Xdr.TwoCellAnchor twoCellAnchor3 = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.OneCell };

            Xdr.FromMarker fromMarker4 = new Xdr.FromMarker();
            Xdr.ColumnId columnId6 = new Xdr.ColumnId();
            columnId6.Text = srcCol;
            Xdr.ColumnOffset columnOffset6 = new Xdr.ColumnOffset();
            columnOffset6.Text = srcColOfst;
            Xdr.RowId rowId6 = new Xdr.RowId();
            rowId6.Text = srcRow;
            Xdr.RowOffset rowOffset6 = new Xdr.RowOffset();
            rowOffset6.Text = srcRowOfst;

            fromMarker4.Append(columnId6);
            fromMarker4.Append(columnOffset6);
            fromMarker4.Append(rowId6);
            fromMarker4.Append(rowOffset6);

            Xdr.ToMarker toMarker3 = new Xdr.ToMarker();
            Xdr.ColumnId columnId7 = new Xdr.ColumnId();
            columnId7.Text = dstCol;
            Xdr.ColumnOffset columnOffset7 = new Xdr.ColumnOffset();
            columnOffset7.Text = dstColOfst;
            Xdr.RowId rowId7 = new Xdr.RowId();
            rowId7.Text = dstRow;
            Xdr.RowOffset rowOffset7 = new Xdr.RowOffset();
            rowOffset7.Text = dstRowOfst;

            toMarker3.Append(columnId7);
            toMarker3.Append(columnOffset7);
            toMarker3.Append(rowId7);
            toMarker3.Append(rowOffset7);

            Xdr.GroupShape groupShape = new Xdr.GroupShape();

            Xdr.NonVisualGroupShapeProperties nonVisualGroupShapeProperties1 = new Xdr.NonVisualGroupShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties2 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)14U, Name = "RankingMarkingLegend" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList2 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension2 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement3 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{70887B14-902B-4572-9ADE-E455BB0B2DF5}\" />");

            nonVisualDrawingPropertiesExtension2.Append(openXmlUnknownElement3);

            nonVisualDrawingPropertiesExtensionList2.Append(nonVisualDrawingPropertiesExtension2);

            nonVisualDrawingProperties2.Append(nonVisualDrawingPropertiesExtensionList2);

            Xdr.NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties1 = new Xdr.NonVisualGroupShapeDrawingProperties();
            A.GroupShapeLocks groupShapeLocks1 = new A.GroupShapeLocks();

            nonVisualGroupShapeDrawingProperties1.Append(groupShapeLocks1);

            nonVisualGroupShapeProperties1.Append(nonVisualDrawingProperties2);
            nonVisualGroupShapeProperties1.Append(nonVisualGroupShapeDrawingProperties1);

            Xdr.GroupShapeProperties groupShapeProperties1 = new Xdr.GroupShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            //A.TransformGroup transformGroup1 = new A.TransformGroup();
            //A.Offset offset2 = new A.Offset() { X = 121920L, Y = 4351020L };
            //A.Extents extents2 = new A.Extents() { Cx = 695325L, Cy = 838200L };
            //A.ChildOffset childOffset1 = new A.ChildOffset() { X = 412L, Y = 75L };
            //A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = 73L, Cy = 88L };

            A.TransformGroup transformGroup1 = new A.TransformGroup();
            A.Offset offset2 = new A.Offset() { X = 7764780L, Y = 2400300L };
            A.Extents extents2 = new A.Extents() { Cx = 1095325L, Cy = 868680L };
            A.ChildOffset childOffset1 = new A.ChildOffset() { X = 5362575L, Y = 5588000L };
            A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = 1076325L, Cy = 840843L };



            transformGroup1.Append(offset2);
            transformGroup1.Append(extents2);
            transformGroup1.Append(childOffset1);
            transformGroup1.Append(childExtents1);

            groupShapeProperties1.Append(transformGroup1);

            Xdr.Shape shape = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.ShapeProperties shapeProperties2 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            //A.Transform2D transform2D2 = new A.Transform2D();
            //A.Offset offset3 = new A.Offset() { X = 412L, Y = 75L };
            //A.Extents extents3 = new A.Extents() { Cx = 73L, Cy = 88L };

            A.Transform2D transform2D2 = new A.Transform2D();
            A.Offset offset3 = new A.Offset() { X = 5362575L, Y = 5588000L };
            A.Extents extents3 = new A.Extents() { Cx = 1076325L, Cy = 840843L };


            transform2D2.Append(offset3);
            transform2D2.Append(extents3);

            A.PresetGeometry presetGeometry2 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList2 = new A.AdjustValueList();

            presetGeometry2.Append(adjustValueList2);

            A.SolidFill solidFill10 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex18 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            solidFill10.Append(rgbColorModelHex18);

            A.Outline outline5 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill11 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex19 = new A.RgbColorModelHex() { Val = "A6A6A6" };

            solidFill11.Append(rgbColorModelHex19);
            A.PresetDash presetDash4 = new A.PresetDash() { Val = A.PresetLineDashValues.SystemDot };
            A.Miter miter2 = new A.Miter() { Limit = 800000 };
            A.HeadEnd headEnd2 = new A.HeadEnd();
            A.TailEnd tailEnd2 = new A.TailEnd();

            outline5.Append(solidFill11);
            outline5.Append(presetDash4);
            outline5.Append(miter2);
            outline5.Append(headEnd2);
            outline5.Append(tailEnd2);

            shapeProperties2.Append(transform2D2);
            shapeProperties2.Append(presetGeometry2);
            shapeProperties2.Append(solidFill10);
            shapeProperties2.Append(outline5);
            //-----------------------------------------
            Xdr.TextBody textBody2 = new Xdr.TextBody();
            A.BodyProperties bodyProperties2 = new A.BodyProperties() { VerticalOverflow = A.TextVerticalOverflowValues.Clip, Wrap = A.TextWrappingValues.Square, LeftInset = 0, TopInset = 46800, RightInset = 0, BottomInset = 46800, Anchor = A.TextAnchoringTypeValues.Top };
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Left, RightToLeft = false };
            A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties() { FontSize = 1000 };

            paragraphProperties2.Append(defaultRunProperties2);

            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = "ja-JP", AlternativeLanguage = "en-US" };

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(SetValues(Qc4Launcher.LocalResource.REPORT_MARKING_LEGEND_RANKING_CAPTION));
            paragraph2.Append(endParagraphRunProperties2);

            textBody2.Append(bodyProperties2);
            textBody2.Append(listStyle2);
            textBody2.Append(paragraph2);

            shape.Append(NonVisualShapeProperty());
            shape.Append(shapeProperties2);
            shape.Append(textBody2);

            groupShape.Append(nonVisualGroupShapeProperties1);
            groupShape.Append(groupShapeProperties1);
            groupShape.Append(shape);

            // x = 427; y = 109; cx = 6; cy = 7;
            x = 5400675L; y = 6005459L; cx = 75911L; cy = 77791L;
            groupShape.Append(CreateTextShapeOval(null, x, y, cx, cy, "FF0000"));

            Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_1ST_CAPTION;
            //x = 444; y = 105; cx = 18; cy = 16;
            x = 5519653L; y = 5961704L; cx = 285822L; cy = 175029L;
            groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));

            //x = 427; y = 125; cx = 6; cy = 7;
            x = 5746926L; y = 6005459L; cx = 75911L; cy = 77791L;
            groupShape.Append(CreateTextShapeOval(null, x, y, cx, cy, "0000FF"));

            Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_2ND_CAPTION;
            //x = 444; y = 121; cx = 18; cy = 16;
            x = 5884918L; y = 5961704L; cx = 284400L; cy = 184752L;
            groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));


            //x = 427; y = 140; cx = 6; cy = 7;
            x = 6116957L; y = 6005459L; cx = 75911L; cy = 77791L;
            groupShape.Append(CreateTextShapeOval(null, x, y, cx, cy, "008000"));

            Text = LocalResource.REPORT_MARKING_LEGEND_RANKING_3RD_CAPTION;
            //x = 444; y = 134; cx = 18; cy = 16;
            x = 6245459L; y = 5961704L; cx = 284400L; cy = 184752L;
            groupShape.Append(CreateTextShapeRectangle(Text, x, y, cx, cy));

            Xdr.ClientData clientData2 = new Xdr.ClientData();
            twoCellAnchor3.Append(fromMarker4);
            twoCellAnchor3.Append(toMarker3);
            twoCellAnchor3.Append(groupShape);
            twoCellAnchor3.Append(clientData2);

            Xdr.WorksheetDrawing worksheetDrawing = null;
            if (drawingsPart.WorksheetDrawing == null)
            {
                worksheetDrawing = new Xdr.WorksheetDrawing();
                worksheetDrawing.Append(twoCellAnchor3);

                drawingsPart.WorksheetDrawing = worksheetDrawing;
            }
            else
            {
                drawingsPart.WorksheetDrawing.Append(twoCellAnchor3);
            }
        }

        public static Xdr.NonVisualShapeProperties NonVisualShapeProperty()
        {
            Xdr.NonVisualShapeProperties nonVisualShapeProperties2 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties3 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)15U, Name = "RankingMarkingLegend" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList3 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension3 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement4 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{D6CA9AF0-614B-4142-97B2-817CE2ECCD61}\" />");

            nonVisualDrawingPropertiesExtension3.Append(openXmlUnknownElement4);

            nonVisualDrawingPropertiesExtensionList3.Append(nonVisualDrawingPropertiesExtension3);

            nonVisualDrawingProperties3.Append(nonVisualDrawingPropertiesExtensionList3);

            Xdr.NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties2 = new Xdr.NonVisualShapeDrawingProperties() { TextBox = true };
            A.ShapeLocks shapeLocks2 = new A.ShapeLocks() { NoChangeArrowheads = true };

            nonVisualShapeDrawingProperties2.Append(shapeLocks2);

            nonVisualShapeProperties2.Append(nonVisualDrawingProperties3);
            nonVisualShapeProperties2.Append(nonVisualShapeDrawingProperties2);
            return nonVisualShapeProperties2;
        }
        public static Xdr.ShapeProperties CreateOvalShapeProperty()
        {
            Xdr.ShapeProperties shapeProperties2 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D2 = new A.Transform2D();
            A.Offset offset3 = new A.Offset() { X = 412L, Y = 75L };
            A.Extents extents3 = new A.Extents() { Cx = 73L, Cy = 88L };

            transform2D2.Append(offset3);
            transform2D2.Append(extents3);

            A.PresetGeometry presetGeometry2 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList2 = new A.AdjustValueList();

            presetGeometry2.Append(adjustValueList2);

            A.SolidFill solidFill10 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex18 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            solidFill10.Append(rgbColorModelHex18);

            A.Outline outline5 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill11 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex19 = new A.RgbColorModelHex() { Val = "A6A6A6" };

            solidFill11.Append(rgbColorModelHex19);
            A.PresetDash presetDash4 = new A.PresetDash() { Val = A.PresetLineDashValues.SystemDot };
            A.Miter miter2 = new A.Miter() { Limit = 800000 };
            A.HeadEnd headEnd2 = new A.HeadEnd();
            A.TailEnd tailEnd2 = new A.TailEnd();

            outline5.Append(solidFill11);
            outline5.Append(presetDash4);
            outline5.Append(miter2);
            outline5.Append(headEnd2);
            outline5.Append(tailEnd2);

            shapeProperties2.Append(transform2D2);
            shapeProperties2.Append(presetGeometry2);
            shapeProperties2.Append(solidFill10);
            shapeProperties2.Append(outline5);
            return shapeProperties2;
        }
        public static A.Run SetValues(string value)
        {
            bool isGlobalMode = QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP" ? false : true;
            A.Run run1 = new A.Run();

            A.RunProperties runProperties1 = new A.RunProperties() { Language = "ja-JP", AlternativeLanguage = "en-US", FontSize = !isGlobalMode ? 900 : 800, Bold = false, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Baseline = 0 };

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
        public static A.ParagraphProperties SetParagraphProperty()
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
        public static Xdr.Shape CreateTextShapeRectangle(string value, long x, long y, long cx, long cy, string clrIdx = "000000")
        {

            Xdr.Shape shape4 = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.NonVisualShapeProperties nonVisualShapeProperties4 = new Xdr.NonVisualShapeProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties5 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)33U, Name = "Level2HighLabel" };

            A.NonVisualDrawingPropertiesExtensionList nonVisualDrawingPropertiesExtensionList5 = new A.NonVisualDrawingPropertiesExtensionList();

            A.NonVisualDrawingPropertiesExtension nonVisualDrawingPropertiesExtension5 = new A.NonVisualDrawingPropertiesExtension() { Uri = "{FF2B5EF4-FFF2-40B4-BE49-F238E27FC236}" };

            OpenXmlUnknownElement openXmlUnknownElement6 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<a16:creationId xmlns:a16=\"http://schemas.microsoft.com/office/drawing/2014/main\" id=\"{00000000-0008-0000-0000-000021000000}\" />");

            nonVisualDrawingPropertiesExtension5.Append(openXmlUnknownElement6);

            nonVisualDrawingPropertiesExtensionList5.Append(nonVisualDrawingPropertiesExtension5);

            nonVisualDrawingProperties5.Append(nonVisualDrawingPropertiesExtensionList5);

            Xdr.NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties4 = new Xdr.NonVisualShapeDrawingProperties() { TextBox = true };
            A.ShapeLocks shapeLocks4 = new A.ShapeLocks() { NoChangeArrowheads = true };

            nonVisualShapeDrawingProperties4.Append(shapeLocks4);

            nonVisualShapeProperties4.Append(nonVisualDrawingProperties5);
            nonVisualShapeProperties4.Append(nonVisualShapeDrawingProperties4);

            Xdr.ShapeProperties shapeProperties4 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D4 = new A.Transform2D();
            A.Offset offset5 = new A.Offset() { X = x, Y = y };
            A.Extents extents5 = new A.Extents() { Cx = cx, Cy = cy };

            transform2D4.Append(offset5);
            transform2D4.Append(extents5);

            A.PresetGeometry presetGeometry4 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList4 = new A.AdjustValueList();

            presetGeometry4.Append(adjustValueList4);
            shapeProperties4.Append(transform2D4);
            shapeProperties4.Append(presetGeometry4);
            if (value == null)
            {
                A.SolidFill solidFill1 = new A.SolidFill();
                A.RgbColorModelHex rgbColorModelHex35 = new A.RgbColorModelHex() { Val = clrIdx };

                solidFill1.Append(rgbColorModelHex35);

                A.Outline outline = new A.Outline() { Width = 19050 };

                A.SolidFill solidFill2 = new A.SolidFill();

                A.RgbColorModelHex rgbColorModelHex36 = new A.RgbColorModelHex() { Val = "FFFFFF", LegacySpreadsheetColorIndex = 9, MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "a14" } };

                solidFill2.Append(rgbColorModelHex36);
                A.Miter miter4 = new A.Miter() { Limit = 800000 };
                A.HeadEnd headEnd5 = new A.HeadEnd();
                A.TailEnd tailEnd5 = new A.TailEnd();

                outline.Append(solidFill2);
                outline.Append(miter4);
                outline.Append(headEnd5);
                outline.Append(tailEnd5);
                shapeProperties4.Append(solidFill1);
                shapeProperties4.Append(outline);
            }
            shape4.Append(nonVisualShapeProperties4);
            shape4.Append(shapeProperties4);
            if (value != null)
                shape4.Append(GenerateTextBody(value));
            return shape4;
        }
        public static Xdr.Shape CreateTextShapeOval(string value, long x, long y, long cx, long cy, string clrIdx = "000000")
        {
            Xdr.Shape shape3 = new Xdr.Shape() { Macro = "", TextLink = "" };

            Xdr.ShapeProperties shapeProperties3 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D3 = new A.Transform2D();
            A.Offset offset4 = new A.Offset() { X = x, Y = y };
            A.Extents extents4 = new A.Extents() { Cx = cx, Cy = cy };

            transform2D3.Append(offset4);
            transform2D3.Append(extents4);

            A.PresetGeometry presetGeometry3 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList3 = new A.AdjustValueList();

            presetGeometry3.Append(adjustValueList3);
            if (value == null)
            {
                A.SolidFill solidFill15 = new A.SolidFill();

                A.RgbColorModelHex rgbColorModelHex23 = new A.RgbColorModelHex() { Val = clrIdx };

                solidFill15.Append(rgbColorModelHex23);

                A.Outline outline6 = new A.Outline() { Width = 9525 };

                A.SolidFill solidFill16 = new A.SolidFill();

                A.RgbColorModelHex rgbColorModelHex24 = new A.RgbColorModelHex() { Val = clrIdx };

                solidFill16.Append(rgbColorModelHex24);
                A.Round round1 = new A.Round();
                A.HeadEnd headEnd3 = new A.HeadEnd();
                A.TailEnd tailEnd3 = new A.TailEnd();

                outline6.Append(solidFill16);
                outline6.Append(round1);
                outline6.Append(headEnd3);
                outline6.Append(tailEnd3);

                shapeProperties3.Append(transform2D3);
                shapeProperties3.Append(presetGeometry3);
                shapeProperties3.Append(solidFill15);
                shapeProperties3.Append(outline6);
            }
            shape3.Append(NonVisualShapeProperty());
            shape3.Append(shapeProperties3);
            if (value != null)
                shape3.Append(GenerateTextBody(value));
            return shape3;
        }
        public static Xdr.TextBody GenerateTextBody(string value)
        {

            Xdr.TextBody textBody = new Xdr.TextBody();

            A.BodyProperties bodyProperties4 = new A.BodyProperties() { Wrap = A.TextWrappingValues.None, LeftInset = 0, TopInset = 0, RightInset = 0, BottomInset = 0, Anchor = A.TextAnchoringTypeValues.Top, UpRight = true };
            A.ShapeAutoFit shapeAutoFit1 = new A.ShapeAutoFit();

            bodyProperties4.Append(shapeAutoFit1);
            A.ListStyle listStyle4 = new A.ListStyle();

            A.Paragraph paragraph4 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties4 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Left, RightToLeft = false };
            A.DefaultRunProperties defaultRunProperties4 = new A.DefaultRunProperties() { FontSize = 1000 };

            paragraphProperties4.Append(defaultRunProperties4);

            A.EndParagraphRunProperties endParagraphRunProperties3 = new A.EndParagraphRunProperties() { Language = "ja-JP", AlternativeLanguage = "en-US" };

            paragraph4.Append(paragraphProperties4);
            paragraph4.Append(SetValues(value));
            paragraph4.Append(endParagraphRunProperties3);

            textBody.Append(bodyProperties4);
            textBody.Append(listStyle4);
            textBody.Append(paragraph4);
            return textBody;
        }

        //Chart Area
        public static void GenerateGraphDrawingsPart(DrawingsPart drawingsPart, string firstRow, string lastRow,string firstCol,string lastCol, string id
                                                     ,string graphType,bool hasWeightBack = false, bool isGlobalMode = false)
        {

            string srcColOfst = null, srcRowOfst = null, dstColOfst = null,dstRowOfst = null;

            if(graphType == "ColCluster")
            {
                if (isGlobalMode)
                    srcColOfst = !hasWeightBack ? "105775" : "125775";
                else
                    srcColOfst = !hasWeightBack ? "175775" : "195775";
                srcRowOfst = "28575"; dstColOfst = "152400"; dstRowOfst = "142875";
            }
            else if(graphType == "BarStacked")
            {
                srcColOfst = "0"; srcRowOfst = "85725"; dstColOfst = "142875"; dstRowOfst = "57150";
            }
            else if(graphType == "BarStackedLegend")
            {
                srcColOfst = "396875"; srcRowOfst = "1219199"; dstColOfst = "142875"; dstRowOfst = "57151";
            }
            else if(graphType == "LegendLine")
            {
                srcColOfst = "423849"; srcRowOfst = "0"; dstColOfst = "185750"; dstRowOfst = "0";
            }
            else if (graphType == "BarClusterPotrait")
            {
                srcColOfst = "410051"; srcRowOfst = "57150"; dstColOfst = "133351"; dstRowOfst = "142875";
            }
            else
            {
                srcColOfst = "0"; srcRowOfst = isGlobalMode ? "1323975" : "1355975"; dstColOfst = "142875"; dstRowOfst = "57151";
            }

            Xdr.WorksheetDrawing worksheetDrawing = null;

            Xdr.TwoCellAnchor twoCellAnchor = new Xdr.TwoCellAnchor();

            Xdr.FromMarker fromMarker1 = new Xdr.FromMarker();
            Xdr.ColumnId columnId1 = new Xdr.ColumnId();
            columnId1.Text = firstCol;
            Xdr.ColumnOffset columnOffset1 = new Xdr.ColumnOffset();
            columnOffset1.Text = srcColOfst;
            Xdr.RowId rowId1 = new Xdr.RowId();
            rowId1.Text = firstRow;
            Xdr.RowOffset rowOffset1 = new Xdr.RowOffset();
            rowOffset1.Text = srcRowOfst;

            fromMarker1.Append(columnId1);
            fromMarker1.Append(columnOffset1);
            fromMarker1.Append(rowId1);
            fromMarker1.Append(rowOffset1);

            Xdr.ToMarker toMarker1 = new Xdr.ToMarker();
            Xdr.ColumnId columnId2 = new Xdr.ColumnId();
            columnId2.Text = lastCol;
            Xdr.ColumnOffset columnOffset2 = new Xdr.ColumnOffset();
            columnOffset2.Text = dstColOfst;
            Xdr.RowId rowId2 = new Xdr.RowId();
            rowId2.Text = lastRow;
            Xdr.RowOffset rowOffset2 = new Xdr.RowOffset();
            rowOffset2.Text = dstRowOfst;

            toMarker1.Append(columnId2);
            toMarker1.Append(columnOffset2);
            toMarker1.Append(rowId2);
            toMarker1.Append(rowOffset2);

            Xdr.GraphicFrame graphicFrame1 = new Xdr.GraphicFrame() { Macro = "" };

            Xdr.NonVisualGraphicFrameProperties nonVisualGraphicFrameProperties1 = new Xdr.NonVisualGraphicFrameProperties();

            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties2 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)2U, Name = "Chart 1" };
            Xdr.NonVisualGraphicFrameDrawingProperties nonVisualGraphicFrameDrawingProperties1 = new Xdr.NonVisualGraphicFrameDrawingProperties();

            nonVisualGraphicFrameProperties1.Append(nonVisualDrawingProperties2);
            nonVisualGraphicFrameProperties1.Append(nonVisualGraphicFrameDrawingProperties1);

            Xdr.Transform transform1 = new Xdr.Transform();
            A.Offset offset2 = new A.Offset() { X = 0L, Y = 0L };
            A.Extents extents2 = new A.Extents() { Cx = 0L, Cy = 0L };

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

            twoCellAnchor.Append(fromMarker1);
            twoCellAnchor.Append(toMarker1);
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
        //Legend
        public static void GenerateBarStackedLegend(WorksheetPart worksheetPart, CrossTable tmpTable, string sheetName, string lineColour, ref string LegendSeriesText,
                                       ChartPart chartPart, int firstRow, int lastRow, int firstCol, int lastCol, bool isN, string tempTable)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = "en-US" };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            C.ColorMapOverride colorMapOverride1 = new C.ColorMapOverride() { Background1 = A.ColorSchemeIndexValues.Light1, Text1 = A.ColorSchemeIndexValues.Dark1, Background2 = A.ColorSchemeIndexValues.Light2, Text2 = A.ColorSchemeIndexValues.Dark2, Accent1 = A.ColorSchemeIndexValues.Accent1, Accent2 = A.ColorSchemeIndexValues.Accent2, Accent3 = A.ColorSchemeIndexValues.Accent3, Accent4 = A.ColorSchemeIndexValues.Accent4, Accent5 = A.ColorSchemeIndexValues.Accent5, Accent6 = A.ColorSchemeIndexValues.Accent6, Hyperlink = A.ColorSchemeIndexValues.Hyperlink, FollowedHyperlink = A.ColorSchemeIndexValues.FollowedHyperlink };
            C.Chart chart1 = new C.Chart();
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = true };

            C.PlotArea plotArea1 = new C.PlotArea();
            C.Layout layout1 = new C.Layout();

            C.ManualLayout manualLayout1 = new C.ManualLayout();
            C.LayoutTarget layoutTarget1 = new C.LayoutTarget() { Val = C.LayoutTargetValues.Inner };
            C.LeftMode leftMode1 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode1 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left1 = new C.Left() { Val = 2.4886877828054297E-2D };
            C.Top top1 = new C.Top() { Val = 0.98084445699076506D };
            C.Width width1 = new C.Width() { Val = 0.95022624434389136D };
            C.Height height1 = new C.Height() { Val = 1.5122797112553939E-2D };

            manualLayout1.Append(layoutTarget1);
            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(top1);
            manualLayout1.Append(width1);
            manualLayout1.Append(height1);

            layout1.Append(manualLayout1);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Bar };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.PercentStacked };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);

            int indexer = 0, i = 1;
            int fCol = firstCol;
            string[] strArray = new string[lastCol - fCol + 1];
            Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet,(uint)firstRow);

            while (fCol <= lastCol)
            {
                Cell cell = OpenXmlHelper.GetCell(row,firstRow,fCol);
                if (cell.CellValue != null)
                {
                    var clr = isN ? System.Drawing.Color.FromArgb(0xF2F2F2) : System.Drawing.Color.FromArgb(ColorPallet.colorIndex[tmpTable.Chart.SeriesColorIndex((i - 1) % tmpTable.Chart.SeriesCount)]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                    C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
                    C.Index index1 = new C.Index() { Val = (uint)indexer };
                    C.Order order1 = new C.Order() { Val = (uint)indexer };
                    C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };

                    barChartSeries1.Append(index1);
                    barChartSeries1.Append(order1);
                    barChartSeries1.Append(SetSeriesText(tempTable, firstRow, fCol));
                    strArray[indexer] = GetSeriesTextValue(worksheetPart, firstRow, fCol);
                    barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                    barChartSeries1.Append(invertIfNegative1);
                    barChart1.Append(barChartSeries1);
                    i++; indexer++;
                }
                fCol++;
            }

            var sorted = strArray.Where(value => !string.IsNullOrEmpty(value)).ToArray().OrderBy(n => n.Length);
            var longest = sorted.LastOrDefault();
            int arrLmt = sorted.Count();
            for (int cnt = 0; cnt < arrLmt; cnt++)
            {
                LegendSeriesText += longest;
            }

            C.DataLabels dataLabels1 = new C.DataLabels();
            C.ShowLegendKey showLegendKey1 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue1 = new C.ShowValue() { Val = false };
            C.ShowCategoryName showCategoryName1 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName1 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent1 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize1 = new C.ShowBubbleSize() { Val = false };

            dataLabels1.Append(showLegendKey1);
            dataLabels1.Append(showValue1);
            dataLabels1.Append(showCategoryName1);
            dataLabels1.Append(showSeriesName1);
            dataLabels1.Append(showPercent1);
            dataLabels1.Append(showBubbleSize1);

            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)30U };
            C.Overlap overlap1 = new C.Overlap() { Val = 100 };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)1268929327U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)1266268511U };

            barChart1.Append(dataLabels1);
            barChart1.Append(gapWidth1);
            barChart1.Append(overlap1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)1268929327U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MaxMin };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.None };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.None };
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)1266268511U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId3);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)1266268511U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 1D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Top };
            C.NumberingFormat numberingFormat6 = new C.NumberingFormat() { FormatCode = "0%", SourceLinked = true };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.None };
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)1268929327U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 0.2D };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(numberingFormat6);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill1 = new A.NoFill();

            A.Outline outline6 = new A.Outline() { Width = 25400 };
            A.NoFill noFill2 = new A.NoFill();

            outline6.Append(noFill2);

            shapeProperties1.Append(noFill1);
            shapeProperties1.Append(outline6);

            plotArea1.Append(layout1);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = false };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Zero };
            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(SetLegend());
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            C.ShapeProperties shapeProperties2 = new C.ShapeProperties();
            A.NoFill noFill7 = new A.NoFill();

            A.Outline outline9 = new A.Outline() { Width = 9525 };
            A.NoFill noFill8 = new A.NoFill();

            outline9.Append(noFill8);

            shapeProperties2.Append(noFill7);
            shapeProperties2.Append(outline9);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(chart1);
            chartSpace1.Append(shapeProperties2);
            chartSpace1.Append(SetTextPrperty(800));
            //chartSpace1.Append(SetPrintSetting());

            chartPart.ChartSpace = chartSpace1;
        }
        //Side Chart
        public static void GenerateBarStackedGraph(WorksheetPart worksheetPart,CrossTable tmpTable, string sheetName, string lineColour,
                                                    ChartPart chartPart,int firstRow,int lastRow,int firstCol,int lastCol,bool isN,string tempTable)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = "en-US" };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            AlternateContent alternateContent2 = new AlternateContent();
            alternateContent2.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

            AlternateContentChoice alternateContentChoice2 = new AlternateContentChoice() { Requires = "c14" };
            alternateContentChoice2.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
            C14.Style style1 = new C14.Style() { Val = 102 };

            alternateContentChoice2.Append(style1);

            AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();
            C.Style style2 = new C.Style() { Val = 2 };

            alternateContentFallback1.Append(style2);

            alternateContent2.Append(alternateContentChoice2);
            alternateContent2.Append(alternateContentFallback1);

            C.Chart chart1 = new C.Chart();
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();

            C.Layout layout1 = new C.Layout();

            C.ManualLayout manualLayout1 = new C.ManualLayout();
            C.LeftMode leftMode1 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode1 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left1 = new C.Left() { Val = 2.5109855618330196E-3D };
            C.Width width1 = new C.Width() { Val = 0.96986817325800379D };

            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(width1);

            layout1.Append(manualLayout1);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Bar };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.PercentStacked };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);

            int indexer = 0, i = 1;
            bool showCategoryName = false, showLeaderLines = false;
            int fCol = firstCol;
            Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)firstRow);
            while (fCol <= lastCol)
            {
                Cell cell = OpenXmlHelper.GetCell(row, firstRow, fCol);
                if (cell.CellValue != null)
                {
                    var clr = isN ? System.Drawing.Color.FromArgb(0xF2F2F2) : System.Drawing.Color.FromArgb(ColorPallet.colorIndex[tmpTable.Chart.SeriesColorIndex((i - 1) % tmpTable.Chart.SeriesCount)]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                    C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
                    C.Index index1 = new C.Index() { Val = (uint)indexer };
                    C.Order order1 = new C.Order() { Val = (uint)indexer };
                    C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };

                    barChartSeries1.Append(index1);
                    barChartSeries1.Append(order1);
                    barChartSeries1.Append(ApplyFillColour(lineColour, rgb));
                    barChartSeries1.Append(invertIfNegative1);
                    barChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines, "0.0;;", false));
                    barChartSeries1.Append(SetNumericDataLinkValues(worksheetPart, tempTable, firstRow + 2, lastRow, fCol, fCol));
                    barChart1.Append(barChartSeries1);
                   i++; indexer++;
                }
                fCol++;
            }

            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)30U };
            C.Overlap overlap1 = new C.Overlap() { Val = 100 };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)1268929327U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)1266268511U };

            barChart1.Append(gapWidth1);
            barChart1.Append(overlap1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)1268929327U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MaxMin };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.None };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.None };
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)1266268511U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId3);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)1266268511U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 1D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Top };
            C.NumberingFormat numberingFormat6 = new C.NumberingFormat() { FormatCode = "0%", SourceLinked = true };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.None };
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)1268929327U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 0.2D };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(numberingFormat6);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill5 = new A.NoFill();

            shapeProperties1.Append(noFill5);

            plotArea1.Append(layout1);
            plotArea1.Append(barChart1); 
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };
            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            C.ShapeProperties shapeProperties2 = new C.ShapeProperties();
            A.NoFill noFill7 = new A.NoFill();

            A.Outline outline9 = new A.Outline() { Width = 9525 };
            A.NoFill noFill8 = new A.NoFill();

            outline9.Append(noFill8);

            shapeProperties2.Append(noFill7);
            shapeProperties2.Append(outline9);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(alternateContent2);
            chartSpace1.Append(chart1);
            chartSpace1.Append(shapeProperties2);
            chartSpace1.Append(SetTextPrperty(800));
            //chartSpace1.Append(SetPrintSetting());

            chartPart.ChartSpace = chartSpace1;
        }
        //Top legend line
        public static void GenerateLegendLineGraph(WorksheetPart worksheetPart, CrossTable tmpTable, string sheetName, string lineColour, List<int> LinesIndexList, ref Array v, bool HasLines,
                                             ChartPart chartPart, int firstRow, int lastRow, int firstCol, int lastCol, bool isN, string tempTable, int i, int[] MaxAxesCountArray)
        {
            string[] tmpBuf;
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = "en-US" };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            AlternateContent alternateContent2 = new AlternateContent();
            alternateContent2.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

            AlternateContentChoice alternateContentChoice2 = new AlternateContentChoice() { Requires = "c14" };
            alternateContentChoice2.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
            C14.Style style1 = new C14.Style() { Val = 102 };

            alternateContentChoice2.Append(style1);

            AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();
            C.Style style2 = new C.Style() { Val = 2 };

            alternateContentFallback1.Append(style2);

            alternateContent2.Append(alternateContentChoice2);
            alternateContent2.Append(alternateContentFallback1);

            C.Chart chart1 = new C.Chart();
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();

            C.Layout layout1 = new C.Layout();

            C.ManualLayout manualLayout1 = new C.ManualLayout();
            C.LayoutTarget layoutTarget1 = new C.LayoutTarget() { Val = C.LayoutTargetValues.Inner };
            C.LeftMode leftMode1 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode1 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left1 = new C.Left() { Val = 0.90927786780147446D };
            C.Top top1 = new C.Top() { Val = 0D };
            C.Width width1 = new C.Width() { Val = 3.2204973986935587E-2D };
            C.Height height1 = new C.Height() { Val = 0.89215686274509809D };

            manualLayout1.Append(layoutTarget1);
            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(top1);
            manualLayout1.Append(width1);
            manualLayout1.Append(height1);

            layout1.Append(manualLayout1);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Column };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Clustered };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
            C.Index index1 = new C.Index() { Val = (UInt32Value)0U };
            C.Order order1 = new C.Order() { Val = (UInt32Value)0U };

            C.SeriesText seriesText1 = new C.SeriesText();
            C.NumericValue numericValue1 = new C.NumericValue();

            Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet,30);
            Cell cell = OpenXmlHelper.GetCell(row,30,2);
            numericValue1.Text = cell == null ? "" : cell.CellValue.InnerText;

            seriesText1.Append(numericValue1);
        
            C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
           
            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            barChartSeries1.Append(seriesText1);
            barChartSeries1.Append(ApplyFillColour("BFBFBF", "F2F2F2"));
            barChartSeries1.Append(invertIfNegative1);

            C.DataLabels dataLabels1 = new C.DataLabels();
            C.ShowLegendKey showLegendKey1 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue1 = new C.ShowValue() { Val = false };
            C.ShowCategoryName showCategoryName1 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName1 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent1 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize1 = new C.ShowBubbleSize() { Val = false };

            dataLabels1.Append(showLegendKey1);
            dataLabels1.Append(showValue1);
            dataLabels1.Append(showCategoryName1);
            dataLabels1.Append(showSeriesName1);
            dataLabels1.Append(showPercent1);
            dataLabels1.Append(showBubbleSize1);
            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)60U };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)2145344767U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)2145223903U };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);
            barChart1.Append(barChartSeries1);
            barChart1.Append(gapWidth1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.LineChart lineChart1 = new C.LineChart();
            C.Grouping grouping1 = new C.Grouping() { Val = C.GroupingValues.Standard };
            C.VaryColors varyColors2 = new C.VaryColors() { Val = false };

            lineChart1.Append(grouping1);
            lineChart1.Append(varyColors2);
           
            int x, fRow = firstRow + 1;
            int lineCount = LinesIndexList.Count;
            for (int j = 0; j < LinesIndexList.Count; j++)
            {
                if (Convert.ToInt32(LinesIndexList[j]) >= 2 || Convert.ToInt32(LinesIndexList[j]) <= v.GetUpperBound(0) - (1 + 1))
                {
                    x = Convert.ToInt32(LinesIndexList[j]) + 1 + 1;
                    if (MaxAxesCountArray[i] == 2)
                    {
                        if ((v.GetValue(x, 3)) != null)
                        {
                            tmpBuf = new string[1];
                            tmpBuf[0] = Convert.ToString(v.GetValue(x, 2));
                        }
                        else
                        {
                            tmpBuf = new string[2];
                            tmpBuf[1] = Convert.ToString(v.GetValue(x, 3));
                            for (x = x; x >= 1 + 1 + 1; x--)
                            {
                                if (v.GetValue(x, 2) != null)
                                { // to do length cehck
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
                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[ColorPallet.colorLineIndex[(j) % ColorPallet.colorLineIndex.Length]]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                    C.LineChartSeries lineChartSeries1 = new C.LineChartSeries();
                    C.Index index2 = new C.Index() { Val = (uint)j + 1};
                    C.Order order2 = new C.Order() { Val = (uint)j + 1 };

                    C.SeriesText seriesText2 = new C.SeriesText();
                    C.NumericValue numericValue4 = new C.NumericValue();
                    numericValue4.Text = OutputUtil.RemoveLeadingSpclChar(String.Join(" - ", tmpBuf)); ;

                    seriesText2.Append(numericValue4);

                    C.Marker marker1 = new C.Marker();
                    C.Symbol symbol1 = new C.Symbol() { Val = C.MarkerStyleValues.Square };
                    C.Size size1 = new C.Size() { Val = 6 };

                    marker1.Append(symbol1);
                    marker1.Append(size1);
                    marker1.Append(SetMarkerProperty(rgb));
                    C.Smooth smooth1 = new C.Smooth() { Val = false };

                    lineChartSeries1.Append(index2);
                    lineChartSeries1.Append(order2);
                    lineChartSeries1.Append(seriesText2);
                    lineChartSeries1.Append(SetLineChartProperty(rgb));
                    lineChartSeries1.Append(marker1);
                    lineChartSeries1.Append(smooth1);

                    lineChart1.Append(lineChartSeries1);
                }
            }
       
            C.DataLabels dataLabels2 = new C.DataLabels();
            C.ShowLegendKey showLegendKey2 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue2 = new C.ShowValue() { Val = false };
            C.ShowCategoryName showCategoryName2 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName2 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent2 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize2 = new C.ShowBubbleSize() { Val = false };

            dataLabels2.Append(showLegendKey2);
            dataLabels2.Append(showValue2);
            dataLabels2.Append(showCategoryName2);
            dataLabels2.Append(showSeriesName2);
            dataLabels2.Append(showPercent2);
            dataLabels2.Append(showBubbleSize2);
            C.ShowMarker showMarker1 = new C.ShowMarker() { Val = true };
            C.Smooth smooth3 = new C.Smooth() { Val = false };
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)2145344767U };
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)2145223903U };

            lineChart1.Append(showMarker1);
            lineChart1.Append(smooth3);
            lineChart1.Append(axisId3);
            lineChart1.Append(axisId4);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId5 = new C.AxisId() { Val = (UInt32Value)2145344767U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MinMax };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Bottom };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.None };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.None };
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)2145223903U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId5);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId6 = new C.AxisId() { Val = (UInt32Value)2145223903U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 100D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.NumberingFormat numberingFormat6 = new C.NumberingFormat() { FormatCode = "0\"%\"", SourceLinked = false };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)2145344767U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 20D };

            valueAxis1.Append(axisId6);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(numberingFormat6);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill1 = new A.NoFill();

            A.Outline outline9 = new A.Outline() { Width = 25400 };
            A.NoFill noFill2 = new A.NoFill();

            outline9.Append(noFill2);

            shapeProperties1.Append(noFill1);
            shapeProperties1.Append(outline9);

            plotArea1.Append(layout1);
            plotArea1.Append(barChart1);
            plotArea1.Append(lineChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            C.Legend legend1 = new C.Legend();
            C.LegendPosition legendPosition1 = new C.LegendPosition() { Val = C.LegendPositionValues.Left };

            C.Layout layout2 = new C.Layout();

            C.ManualLayout manualLayout2 = new C.ManualLayout();
            C.LeftMode leftMode2 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode2 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left2 = new C.Left() { Val = 4.3478260869565216E-2D };
             C.Top top2 = new C.Top() { Val = 0.39764705882352941D };
            C.Width width2 = new C.Width() { Val = 0.9126172900262467D };
            C.Height height2 = new C.Height() { Val = 0.70470588235294118D };

            manualLayout2.Append(leftMode2);
            manualLayout2.Append(topMode2);
            manualLayout2.Append(left2);
            manualLayout2.Append(top2);
            manualLayout2.Append(width2);
            manualLayout2.Append(height2);

            layout2.Append(manualLayout2);
            C.Overlay overlay1 = new C.Overlay() { Val = false };

            legend1.Append(legendPosition1);
            legend1.Append(layout2);
            legend1.Append(overlay1);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };
          
            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            C.ShapeProperties shapeProperties2 = new C.ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            A.Outline outline10 = new A.Outline() { Width = 9525 };
            A.NoFill noFill4 = new A.NoFill();

            outline10.Append(noFill4);

            shapeProperties2.Append(noFill3);
            shapeProperties2.Append(outline10);
                  
            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(alternateContent2);
            chartSpace1.Append(chart1);
            chartSpace1.Append(shapeProperties2);
            chartSpace1.Append(SetTextPrperty(800));

            chartPart.ChartSpace = chartSpace1;
        }

        public static void GenerateLegendLineGraphPortrait(WorksheetPart worksheetPart, CrossTable tmpTable, string sheetName, string lineColour, List<int> LinesIndexList, ref Array v, bool HasLines,
                                             ChartPart chartPart, int firstRow, int lastRow, int firstCol, int lastCol, bool isN, string tempTable, int i, int[] MaxAxesCountArray)
        {
            string[] tmpBuf;
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = "en-US" };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            AlternateContent alternateContent2 = new AlternateContent();
            alternateContent2.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

            AlternateContentChoice alternateContentChoice2 = new AlternateContentChoice() { Requires = "c14" };
            alternateContentChoice2.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
            C14.Style style1 = new C14.Style() { Val = 102 };

            alternateContentChoice2.Append(style1);

            AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();
            C.Style style2 = new C.Style() { Val = 2 };

            alternateContentFallback1.Append(style2);

            alternateContent2.Append(alternateContentChoice2);
            alternateContent2.Append(alternateContentFallback1);

            C.Chart chart1 = new C.Chart();
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();

            C.Layout layout1 = new C.Layout();

            C.ManualLayout manualLayout1 = new C.ManualLayout();
            C.LayoutTarget layoutTarget1 = new C.LayoutTarget() { Val = C.LayoutTargetValues.Inner };
            C.LeftMode leftMode1 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode1 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left1 = new C.Left() { Val = 0.90927786780147446D };
            C.Top top1 = new C.Top() { Val = 0D };
            C.Width width1 = new C.Width() { Val = 3.2204973986935587E-2D };
            C.Height height1 = new C.Height() { Val = 0.89215686274509809D };

            manualLayout1.Append(layoutTarget1);
            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(top1);
            manualLayout1.Append(width1);
            manualLayout1.Append(height1);

            layout1.Append(manualLayout1);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Bar };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Clustered };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
            C.Index index1 = new C.Index() { Val = (UInt32Value)0U };
            C.Order order1 = new C.Order() { Val = (UInt32Value)0U };

            C.SeriesText seriesText1 = new C.SeriesText();
            C.NumericValue numericValue1 = new C.NumericValue();

            Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, Convert.ToUInt32((ReportPortraitHelper.BarClusterGraph.Bar_Cluster_Start_Row - 1)));
            Cell cell = OpenXmlHelper.GetCell(row, (ReportPortraitHelper.BarClusterGraph.Bar_Cluster_Start_Row - 1), 2);
            numericValue1.Text = cell == null ? "" : cell.CellValue.InnerText;

            seriesText1.Append(numericValue1);

            C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };

            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            barChartSeries1.Append(seriesText1);
            barChartSeries1.Append(ApplyFillColour("BFBFBF", "F2F2F2"));
            barChartSeries1.Append(invertIfNegative1);

            C.DataLabels dataLabels1 = new C.DataLabels();
            C.ShowLegendKey showLegendKey1 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue1 = new C.ShowValue() { Val = false };
            C.ShowCategoryName showCategoryName1 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName1 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent1 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize1 = new C.ShowBubbleSize() { Val = false };

            dataLabels1.Append(showLegendKey1);
            dataLabels1.Append(showValue1);
            dataLabels1.Append(showCategoryName1);
            dataLabels1.Append(showSeriesName1);
            dataLabels1.Append(showPercent1);
            dataLabels1.Append(showBubbleSize1);
            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)60U };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)2145344767U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)2145223903U };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);
            barChart1.Append(barChartSeries1);
            barChart1.Append(gapWidth1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.LineChart lineChart1 = new C.LineChart();
            C.Grouping grouping1 = new C.Grouping() { Val = C.GroupingValues.Standard };
            C.VaryColors varyColors2 = new C.VaryColors() { Val = false };

            lineChart1.Append(grouping1);
            lineChart1.Append(varyColors2);



            int choiceFirstRow = 28;
            for (int j = 0; j < LinesIndexList.Count; j++)
            {
                if (Convert.ToInt32(LinesIndexList[j]) >= 2 || Convert.ToInt32(LinesIndexList[j]) <= v.GetUpperBound(0) - (1 + 1))
                {
                    int choiceFirstCol = 16;
                    choiceFirstCol += LinesIndexList[j];
                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[ColorPallet.colorLineIndex[(j) % ColorPallet.colorLineIndex.Length]]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                    C.LineChartSeries lineChartSeries1 = new C.LineChartSeries();
                    C.Index index2 = new C.Index() { Val = (uint)j + 1 };
                    C.Order order2 = new C.Order() { Val = (uint)j + 1 };

                    C.SeriesText seriesText2 = new C.SeriesText();
                    C.StringReference stringReference1 = new C.StringReference();
                    C.Formula formula5 = new C.Formula();
                    formula5.Text = "\'" + tempTable + "\'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(choiceFirstCol) + "$" + choiceFirstRow;
                    stringReference1.Append(formula5);
                    seriesText2.Append(stringReference1);

                    C.Marker marker1 = new C.Marker();
                    C.Symbol symbol1 = new C.Symbol() { Val = C.MarkerStyleValues.Square };
                    C.Size size1 = new C.Size() { Val = 6 };

                    marker1.Append(symbol1);
                    marker1.Append(size1);
                    marker1.Append(SetMarkerProperty(rgb));
                    C.Smooth smooth1 = new C.Smooth() { Val = false };

                    lineChartSeries1.Append(index2);
                    lineChartSeries1.Append(order2);
                    lineChartSeries1.Append(seriesText2);
                    lineChartSeries1.Append(SetLineChartProperty(rgb));
                    lineChartSeries1.Append(marker1);
                    lineChartSeries1.Append(smooth1);

                    lineChart1.Append(lineChartSeries1);
                    choiceFirstCol++;
                }
            }

            C.DataLabels dataLabels2 = new C.DataLabels();
            C.ShowLegendKey showLegendKey2 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue2 = new C.ShowValue() { Val = false };
            C.ShowCategoryName showCategoryName2 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName2 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent2 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize2 = new C.ShowBubbleSize() { Val = false };

            dataLabels2.Append(showLegendKey2);
            dataLabels2.Append(showValue2);
            dataLabels2.Append(showCategoryName2);
            dataLabels2.Append(showSeriesName2);
            dataLabels2.Append(showPercent2);
            dataLabels2.Append(showBubbleSize2);
            C.ShowMarker showMarker1 = new C.ShowMarker() { Val = true };
            C.Smooth smooth3 = new C.Smooth() { Val = false };
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)2145344767U };
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)2145223903U };

            lineChart1.Append(showMarker1);
            lineChart1.Append(smooth3);
            lineChart1.Append(axisId3);
            lineChart1.Append(axisId4);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId5 = new C.AxisId() { Val = (UInt32Value)2145344767U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MinMax };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Bottom };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.None };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.None };
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)2145223903U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId5);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId6 = new C.AxisId() { Val = (UInt32Value)2145223903U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 100D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.NumberingFormat numberingFormat6 = new C.NumberingFormat() { FormatCode = "0\"%\"", SourceLinked = false };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)2145344767U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 20D };

            valueAxis1.Append(axisId6);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(numberingFormat6);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill1 = new A.NoFill();

            A.Outline outline9 = new A.Outline() { Width = 25400 };
            A.NoFill noFill2 = new A.NoFill();

            outline9.Append(noFill2);

            shapeProperties1.Append(noFill1);
            shapeProperties1.Append(outline9);

            plotArea1.Append(layout1);
            plotArea1.Append(barChart1);
            plotArea1.Append(lineChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            C.Legend legend1 = new C.Legend();
            C.LegendPosition legendPosition1 = new C.LegendPosition() { Val = C.LegendPositionValues.Left };

            C.Layout layout2 = new C.Layout();

            C.ManualLayout manualLayout2 = new C.ManualLayout();
            C.LeftMode leftMode2 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode2 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left2 = new C.Left() { Val = 4.3478260869565216E-2D };
            C.Top top2 = new C.Top() { Val = 0.39764705882352941D };
            C.Width width2 = new C.Width() { Val = 0.9126172900262467D };
            C.Height height2 = new C.Height() { Val = 0.20470588235294118D };

            manualLayout2.Append(leftMode2);
            manualLayout2.Append(topMode2);
            manualLayout2.Append(left2);
            manualLayout2.Append(top2);
            manualLayout2.Append(width2);
            //manualLayout2.Append(height2);

            layout2.Append(manualLayout2);
            C.Overlay overlay1 = new C.Overlay() { Val = false };

            legend1.Append(legendPosition1);
            legend1.Append(layout2);
            legend1.Append(overlay1);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };

            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            C.ShapeProperties shapeProperties2 = new C.ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            A.Outline outline10 = new A.Outline() { Width = 9525 };
            A.NoFill noFill4 = new A.NoFill();

            outline10.Append(noFill4);

            shapeProperties2.Append(noFill3);
            shapeProperties2.Append(outline10);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(alternateContent2);
            chartSpace1.Append(chart1);
            chartSpace1.Append(shapeProperties2);
            chartSpace1.Append(SetTextPrperty(800));

            chartPart.ChartSpace = chartSpace1;
        }
        //Numeric chart
        public static void GenerateBarClusterGraph(WorksheetPart worksheetPart, CrossTable tmpTable, string sheetName, string lineColour,string seriesText,
                                                   ChartPart chartPart, int firstRow, int lastRow, int firstCol, int lastCol, bool isN, string tempTable)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            chartSpace1.AddNamespaceDeclaration("c16r2", "http://schemas.microsoft.com/office/drawing/2015/06/chart");
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = "en-US" };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            AlternateContent alternateContent2 = new AlternateContent();
            alternateContent2.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

            AlternateContentChoice alternateContentChoice2 = new AlternateContentChoice() { Requires = "c14" };
            alternateContentChoice2.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
            C14.Style style1 = new C14.Style() { Val = 102 };

            alternateContentChoice2.Append(style1);

            AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();
            C.Style style2 = new C.Style() { Val = 2 };

            alternateContentFallback1.Append(style2);

            alternateContent2.Append(alternateContentChoice2);
            alternateContent2.Append(alternateContentFallback1);

            C.Chart chart1 = new C.Chart();
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = true };

            C.PlotArea plotArea1 = new C.PlotArea();

            C.Layout layout1 = new C.Layout();

            C.ManualLayout manualLayout1 = new C.ManualLayout();
            C.LeftMode leftMode1 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.Left left1 = new C.Left() { Val = 2.5109855618330196E-3D };
            C.Width width1 = new C.Width() { Val = 0.96986817325800379D };

            manualLayout1.Append(leftMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(width1);

            layout1.Append(manualLayout1);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Bar };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Clustered };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);

            C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
            C.Index index1 = new C.Index() { Val = (UInt32Value)0U };
            C.Order order1 = new C.Order() { Val = (UInt32Value)0U };

            C.SeriesText seriesText1 = new C.SeriesText();
            C.NumericValue numericValue1 = new C.NumericValue();
            numericValue1.Text = seriesText;

            seriesText1.Append(numericValue1);

            C.ShowLeaderLines showLeaderLines1 = new C.ShowLeaderLines() { Val = false };

            bool showCategoryName = false, showLeaderLines = false;
            C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };

            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            barChartSeries1.Append(seriesText1);
            barChartSeries1.Append(ApplyFillColour("BFBFBF", "F2F2F2"));
            barChartSeries1.Append(invertIfNegative1);
            barChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines, "0.0;;", false));
            barChartSeries1.Append(SetNumericDataLinkValues(worksheetPart, tempTable, firstRow, lastRow, lastCol, firstCol));

            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)30U };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)489143992U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)489144320U };

            barChart1.Append(barChartSeries1);
            barChart1.Append(gapWidth1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)489143992U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MaxMin };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.None };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.None };
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)489144320U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId3);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)489144320U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Top };
            C.NumberingFormat numberingFormat3 = new C.NumberingFormat() { FormatCode = "0.00", SourceLinked = true };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.None };
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)489143992U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(numberingFormat3);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            shapeProperties1.Append(noFill3);

            plotArea1.Append(layout1);
            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);

            C.Legend legend1 = new C.Legend();
            C.LegendPosition legendPosition1 = new C.LegendPosition() { Val = C.LegendPositionValues.Top };
            C.Overlay overlay1 = new C.Overlay() { Val = false };

            C.ChartShapeProperties chartShapeProperties3 = new C.ChartShapeProperties();

            A.Outline outline6 = new A.Outline() { Width = 25400 };
            A.NoFill noFill4 = new A.NoFill();

            outline6.Append(noFill4);

            chartShapeProperties3.Append(outline6);

            legend1.Append(legendPosition1);
            legend1.Append(overlay1);
            legend1.Append(chartShapeProperties3);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };

            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(legend1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            C.ShapeProperties shapeProperties2 = new C.ShapeProperties();
            A.NoFill noFill5 = new A.NoFill();

            A.Outline outline7 = new A.Outline() { Width = 9525 };
            A.NoFill noFill6 = new A.NoFill();

            outline7.Append(noFill6);

            shapeProperties2.Append(noFill5);
            shapeProperties2.Append(outline7);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(alternateContent2);
            chartSpace1.Append(chart1);
            chartSpace1.Append(shapeProperties2);
            chartSpace1.Append(SetTextPrperty(800));
            //chartSpace1.Append(SetPrintSetting());

            chartPart.ChartSpace = chartSpace1;
        }
        //Top bar cluster and Line Chart
        public static void GenerateColumClusterAndLineGraph(WorksheetPart worksheetPart, CrossTable tmpTable, string sheetName, string lineColour, List<int> LinesIndexList, ref Array v,bool HasLines,
                                             ChartPart chartPart, int firstRow, int lastRow, int firstCol, int lastCol, bool isN, string tempTable,int i, int[] MaxAxesCountArray)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = "en-US" };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            AlternateContent alternateContent2 = new AlternateContent();
            alternateContent2.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

            AlternateContentChoice alternateContentChoice2 = new AlternateContentChoice() { Requires = "c14" };
            alternateContentChoice2.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
            C14.Style style1 = new C14.Style() { Val = 102 };

            alternateContentChoice2.Append(style1);

            AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();
            C.Style style2 = new C.Style() { Val = 2 };

            alternateContentFallback1.Append(style2);

            alternateContent2.Append(alternateContentChoice2);
            alternateContent2.Append(alternateContentFallback1);

            C.Chart chart1 = new C.Chart();
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();

            C.Layout layout1 = CreateAlignedPlotAreaLayout(0.10, 0.84);

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Column };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Clustered };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
            C.Index index1 = new C.Index() { Val = (UInt32Value)0U };
            C.Order order1 = new C.Order() { Val = (UInt32Value)0U };

            C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };
            bool showCategoryName = false, showLeaderLines = false;
            int categoryRow = firstRow - 1;

            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            if (HasLines)
            {
                barChartSeries1.Append(SetSeriesText(tempTable, firstRow, 1));
            }
            int col = firstCol;
            int subTotalCnt = lastCol - tmpTable.Question.SubTotalCnt;
            int indexer = 0;
            string barChartColor = "F2F2F2";
            string lineColor = "BFBFBF";
            while (col <= lastCol)
            {
                if (col > subTotalCnt)
                    barChartColor = "E4DFEC";
                else
                    barChartColor = "F2F2F2";

                barChartSeries1.Append(ApplyFillXlClustered(lineColor, barChartColor, indexer));
                indexer++;
                col++;
            }
            //barChartSeries1.Append(ApplyFillColour("BFBFBF", "F2F2F2"));
            barChartSeries1.Append(invertIfNegative1);
            barChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines, "0.0;;"));
            barChartSeries1.Append(SetStringDataLinkValues(worksheetPart, tempTable, categoryRow, categoryRow, lastCol, firstCol));
            barChartSeries1.Append(SetNumericDataLinkValues(worksheetPart, tempTable, firstRow, lastRow, lastCol, firstCol));

            C.DataLabels dataLabels2 = new C.DataLabels();
            C.ShowLegendKey showLegendKey2 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue2 = new C.ShowValue() { Val = false };
            C.ShowCategoryName showCategoryName2 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName2 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent2 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize2 = new C.ShowBubbleSize() { Val = false };

            dataLabels2.Append(showLegendKey2);
            dataLabels2.Append(showValue2);
            dataLabels2.Append(showCategoryName2);
            dataLabels2.Append(showSeriesName2);
            dataLabels2.Append(showPercent2);
            dataLabels2.Append(showBubbleSize2);
            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)60U };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)2145344767U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)2145223903U };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);
            barChart1.Append(barChartSeries1);
            barChart1.Append(dataLabels2);
            barChart1.Append(gapWidth1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.LineChart lineChart1 = new C.LineChart();

            if (HasLines)
            {
                string[] tmpBuf;
                C.Grouping grouping1 = new C.Grouping() { Val = C.GroupingValues.Standard };
                C.VaryColors varyColors2 = new C.VaryColors() { Val = false };

                lineChart1.Append(grouping1);
                lineChart1.Append(varyColors2);

                int x, fRow = firstRow + 1;
                int lineCount = LinesIndexList.Count;
                for (int j = 0; j < LinesIndexList.Count; j++)
                {
                    if (Convert.ToInt32(LinesIndexList[j]) >= 2 || Convert.ToInt32(LinesIndexList[j]) <= v.GetUpperBound(0) - (1 + 1))
                    {
                        fRow = firstRow - 1;
                        fRow += LinesIndexList[j];
                        x = Convert.ToInt32(LinesIndexList[j]) + 1 + 1;
                        if (MaxAxesCountArray[i] == 2)
                        {
                            if ((v.GetValue(x, 3)) != null)
                            {
                                tmpBuf = new string[1];
                                tmpBuf[0] = Convert.ToString(v.GetValue(x, 2));
                            }
                            else
                            {
                                tmpBuf = new string[2];
                                tmpBuf[1] = Convert.ToString(v.GetValue(x, 3));
                                for (x = x; x >= 1 + 1 + 1; x--)
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

                        C.LineChartSeries lineChartSeries1 = new C.LineChartSeries();
                        C.Index index2 = new C.Index() { Val = (uint)j + 1U };
                        C.Order order2 = new C.Order() { Val = (uint)j + 1U };

                        C.SeriesText seriesText2 = new C.SeriesText();
                        C.NumericValue numericValue4 = new C.NumericValue();
                        numericValue4.Text = OutputUtil.RemoveLeadingSpclChar(String.Join(" - ", tmpBuf));
                        seriesText2.Append(numericValue4);

                        var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[ColorPallet.colorLineIndex[(j) % ColorPallet.colorLineIndex.Length]]);
                        var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                        C.Marker marker1 = new C.Marker();
                        C.Symbol symbol1 = new C.Symbol() { Val = C.MarkerStyleValues.Square };
                        C.Size size1 = new C.Size() { Val = 6 };

                        marker1.Append(symbol1);
                        marker1.Append(size1);
                        marker1.Append(SetMarkerProperty(rgb));
                        C.Smooth smooth1 = new C.Smooth() { Val = false };

                        lineChartSeries1.Append(index2);
                        lineChartSeries1.Append(order2);
                        lineChartSeries1.Append(seriesText2);
                        lineChartSeries1.Append(SetLineChartProperty(rgb));
                        lineChartSeries1.Append(marker1);
                        lineChartSeries1.Append(SetStringDataLinkValues(worksheetPart, tempTable, categoryRow, categoryRow, lastCol, firstCol));
                        lineChartSeries1.Append(SetNumericDataLinkValues(worksheetPart, tempTable, fRow, fRow, lastCol, firstCol));
                        lineChartSeries1.Append(smooth1);

                        lineChart1.Append(lineChartSeries1);
                    }
                    fRow++;
                }

                C.ShowMarker showMarker1 = new C.ShowMarker() { Val = true };
                C.Smooth smooth3 = new C.Smooth() { Val = false };
                C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)2145344767U };
                C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)2145223903U };

                lineChart1.Append(showMarker1);
                lineChart1.Append(smooth3);
                lineChart1.Append(axisId3);
                lineChart1.Append(axisId4);
            }

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId5 = new C.AxisId() { Val = (UInt32Value)2145344767U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MinMax };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Bottom };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.None };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.None };

            C.ChartShapeProperties chartShapeProperties7 = new C.ChartShapeProperties();

            A.Outline outline10 = new A.Outline();

            A.SolidFill solidFill14 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex22 = new A.RgbColorModelHex() { Val = "BFBFBF" };

            solidFill14.Append(rgbColorModelHex22);
            A.PresetDash presetDash8 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline10.Append(solidFill14);
            outline10.Append(presetDash8);

            chartShapeProperties7.Append(outline10);
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)2145223903U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId5);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(chartShapeProperties7);
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId6 = new C.AxisId() { Val = (UInt32Value)2145223903U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 100D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };

            C.MajorGridlines majorGridlines1 = new C.MajorGridlines();

            C.ChartShapeProperties chartShapeProperties8 = new C.ChartShapeProperties();

            A.Outline outline11 = new A.Outline() { Width = 3175 };

            A.SolidFill solidFill15 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex23 = new A.RgbColorModelHex() { Val = "C0C0C0" };

            solidFill15.Append(rgbColorModelHex23);
            A.PresetDash presetDash9 = new A.PresetDash() { Val = A.PresetLineDashValues.SystemDash };

            outline11.Append(solidFill15);
            outline11.Append(presetDash9);

            chartShapeProperties8.Append(outline11);

            majorGridlines1.Append(chartShapeProperties8);
            C.NumberingFormat numberingFormat6 = new C.NumberingFormat() { FormatCode = "0\"%\"", SourceLinked = false };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };

            C.ChartShapeProperties chartShapeProperties9 = new C.ChartShapeProperties();

            A.Outline outline12 = new A.Outline();

            A.SolidFill solidFill16 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex24 = new A.RgbColorModelHex() { Val = "BFBFBF" };

            solidFill16.Append(rgbColorModelHex24);
            A.PresetDash presetDash10 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline12.Append(solidFill16);
            outline12.Append(presetDash10);

            chartShapeProperties9.Append(outline12);
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)2145344767U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 20D };

            valueAxis1.Append(axisId6);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(majorGridlines1);
            valueAxis1.Append(numberingFormat6);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(chartShapeProperties9);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties1 = new C.ShapeProperties();

            A.Outline outline13 = new A.Outline() { Width = 25400 };
            A.NoFill noFill3 = new A.NoFill();

            outline13.Append(noFill3);

            shapeProperties1.Append(outline13);

            plotArea1.Append(layout1);
            plotArea1.Append(barChart1);
            if(HasLines) plotArea1.Append(lineChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties1);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };

            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            C.ShapeProperties shapeProperties2 = new C.ShapeProperties();
            A.NoFill noFill4 = new A.NoFill();

            A.Outline outline14 = new A.Outline() { Width = 9525 };
            A.NoFill noFill5 = new A.NoFill();

            outline14.Append(noFill5);

            shapeProperties2.Append(noFill4);
            shapeProperties2.Append(outline14);
                   
            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(alternateContent2);
            chartSpace1.Append(chart1);
            chartSpace1.Append(shapeProperties2);
            chartSpace1.Append(SetTextPrperty(800));

            chartPart.ChartSpace = chartSpace1;
        }

        //PotraintChart
        public static void GenerateBarClusterGraphPotrait(WorksheetPart worksheetPart, CrossTable tmpTable, string sheetName, string lineColour, string seriesText,
                                                  ChartPart chartPart, int firstRow, int lastRow, int firstCol, int lastCol, bool isN, string tempTable)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            chartSpace1.AddNamespaceDeclaration("c16r2", "http://schemas.microsoft.com/office/drawing/2015/06/chart");
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = "en-US" };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            AlternateContent alternateContent2 = new AlternateContent();
            alternateContent2.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

            AlternateContentChoice alternateContentChoice2 = new AlternateContentChoice() { Requires = "c14" };
            alternateContentChoice2.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
            C14.Style style1 = new C14.Style() { Val = 102 };

            alternateContentChoice2.Append(style1);

            AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();
            C.Style style2 = new C.Style() { Val = 2 };

            alternateContentFallback1.Append(style2);

            alternateContent2.Append(alternateContentChoice2);
            alternateContent2.Append(alternateContentFallback1);

            C.Chart chart1 = new C.Chart();
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Bar };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Clustered };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);

            C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
            C.Index index1 = new C.Index() { Val = (UInt32Value)0U };
            C.Order order1 = new C.Order() { Val = (UInt32Value)0U };

            bool showCategoryName = false, showLeaderLines = false;
            C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };

            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            barChartSeries1.Append(ApplyFillColour("BFBFBF", "F2F2F2"));
            barChartSeries1.Append(invertIfNegative1);
            barChartSeries1.Append(ApplyDataLabels(showCategoryName, showLeaderLines, "0.0;;"));
            barChartSeries1.Append(SetStringDataLinkValues(worksheetPart, tempTable, firstRow, lastRow, 2, 2));
            barChartSeries1.Append(SetNumericDataLinkValues(worksheetPart, tempTable, firstRow, lastRow, firstCol, firstCol));

            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)30U };
            C.Overlap overlap1 = new C.Overlap() { Val = -50 };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)182559488U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)182561024U };

            barChart1.Append(barChartSeries1);
            barChart1.Append(gapWidth1);
            barChart1.Append(overlap1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)182559488U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MaxMin };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.NumberingFormat numberingFormat4 = new C.NumberingFormat() { FormatCode = "General", SourceLinked = false };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.Outside };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)182561024U };
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
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)182561024U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 100D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = false };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Top };
            C.NumberingFormat numberingFormat5 = new C.NumberingFormat() { FormatCode = "0\"%\"", SourceLinked = false };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.None };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.None };

            C.ChartShapeProperties chartShapeProperties4 = new C.ChartShapeProperties();

            A.Outline outline9 = new A.Outline();

            A.SolidFill solidFill14 = new A.SolidFill();

            A.SchemeColor schemeColor20 = new A.SchemeColor() { Val = A.SchemeColorValues.Background1 };
            A.LuminanceModulation luminanceModulation4 = new A.LuminanceModulation() { Val = 65000 };

            schemeColor20.Append(luminanceModulation4);

            solidFill14.Append(schemeColor20);

            outline9.Append(solidFill14);

            chartShapeProperties4.Append(outline9);

            C.TextProperties textProperties2 = new C.TextProperties();
            A.BodyProperties bodyProperties4 = new A.BodyProperties();
            A.ListStyle listStyle4 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties() { Language = "ja-JP" };

            paragraphProperties2.Append(defaultRunProperties2);
            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = "en-US" };

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(endParagraphRunProperties2);

            textProperties2.Append(bodyProperties4);
            textProperties2.Append(listStyle4);
            textProperties2.Append(paragraph2);
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)182559488U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 20D };

            valueAxis1.Append(axisId4);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(numberingFormat5);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(chartShapeProperties4);
            valueAxis1.Append(textProperties2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ShapeProperties shapeProperties3 = new C.ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            A.Outline outline10 = new A.Outline();
            A.NoFill noFill4 = new A.NoFill();

            outline10.Append(noFill4);

            shapeProperties3.Append(noFill3);
            shapeProperties3.Append(outline10);

            plotArea1.Append(barChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            plotArea1.Append(shapeProperties3);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };
            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            C.ShapeProperties shapeProperties4 = new C.ShapeProperties();
            A.NoFill noFill5 = new A.NoFill();

            A.Outline outline11 = new A.Outline();
            A.NoFill noFill6 = new A.NoFill();

            outline11.Append(noFill6);

            shapeProperties4.Append(noFill5);
            shapeProperties4.Append(outline11);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(alternateContent2);
            chartSpace1.Append(chart1);
            chartSpace1.Append(shapeProperties4);
            chartSpace1.Append(SetTextPrperty(800));

            chartPart.ChartSpace = chartSpace1;
        }

        public static void GenerateBarClusterLineGraphPotrait(WorksheetPart worksheetPart, CrossTable tmpTable, string sheetName, string lineColour, string seriesText,
                                                 ChartPart chartPart, int firstRow, int lastRow, int firstCol, int lastCol, bool isN, string tempTable, bool HasLines, List<int> LinesIndexList)
        {
            C.ChartSpace chartSpace1 = new C.ChartSpace();
            chartSpace1.AddNamespaceDeclaration("c16r2", "http://schemas.microsoft.com/office/drawing/2015/06/chart");
            C.Date1904 date19041 = new C.Date1904() { Val = false };
            C.EditingLanguage editingLanguage1 = new C.EditingLanguage() { Val = "en-US" };
            C.RoundedCorners roundedCorners1 = new C.RoundedCorners() { Val = false };

            AlternateContent alternateContent2 = new AlternateContent();
            alternateContent2.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

            AlternateContentChoice alternateContentChoice2 = new AlternateContentChoice() { Requires = "c14" };
            alternateContentChoice2.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
            C14.Style style1 = new C14.Style() { Val = 102 };

            alternateContentChoice2.Append(style1);

            AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();
            C.Style style2 = new C.Style() { Val = 2 };

            alternateContentFallback1.Append(style2);

            alternateContent2.Append(alternateContentChoice2);
            alternateContent2.Append(alternateContentFallback1);

            C.Chart chart1 = new C.Chart();
            C.AutoTitleDeleted autoTitleDeleted1 = new C.AutoTitleDeleted() { Val = false };

            C.PlotArea plotArea1 = new C.PlotArea();

            C.Layout layout1 = new C.Layout();

            C.BarChart barChart1 = new C.BarChart();
            C.BarDirection barDirection1 = new C.BarDirection() { Val = C.BarDirectionValues.Bar };
            C.BarGrouping barGrouping1 = new C.BarGrouping() { Val = C.BarGroupingValues.Clustered };
            C.VaryColors varyColors1 = new C.VaryColors() { Val = false };

            barChart1.Append(barDirection1);
            barChart1.Append(barGrouping1);
            barChart1.Append(varyColors1);

            C.BarChartSeries barChartSeries1 = new C.BarChartSeries();
            C.Index index1 = new C.Index() { Val = (UInt32Value)1U };
            C.Order order1 = new C.Order() { Val = (UInt32Value)0U };

            C.SeriesText seriesText1 = new C.SeriesText();

            C.StringReference stringReference1 = new C.StringReference();
            C.Formula formula5 = new C.Formula();
            formula5.Text = "\'" + tempTable + "\'!$B$" + (firstRow - 1);

            C.StringCache stringCache1 = new C.StringCache();
            C.PointCount pointCount1 = new C.PointCount() { Val = (UInt32Value)1U };

            C.StringPoint stringPoint1 = new C.StringPoint() { Index = (UInt32Value)0U };
            C.NumericValue numericValue1 = new C.NumericValue();
            numericValue1.Text = "全体";

            stringPoint1.Append(numericValue1);

            stringCache1.Append(pointCount1);
            stringCache1.Append(stringPoint1);

            stringReference1.Append(formula5);
            stringReference1.Append(stringCache1);

            seriesText1.Append(stringReference1);

            bool showCategoryName = false, showLeaderLines = false;
            C.InvertIfNegative invertIfNegative1 = new C.InvertIfNegative() { Val = false };

            barChartSeries1.Append(index1);
            barChartSeries1.Append(order1);
            if (HasLines) barChartSeries1.Append(seriesText1);

            int row = firstRow;
            int subTotalCnt = lastRow - tmpTable.Question.SubTotalCnt;
            int indexer = 0;
            string barChartColor = "F2F2F2";
            string lineColor = "BFBFBF";
            while (row <= lastRow)
            {
                if (row > subTotalCnt)
                    barChartColor = "E4DFEC";
                else
                    barChartColor = "F2F2F2";

                barChartSeries1.Append(ApplyFillXlClustered(lineColor, barChartColor, indexer));
                indexer++;
                row++;
            }
            //barChartSeries1.Append(ApplyFillColour("BFBFBF", "F2F2F2"));
            barChartSeries1.Append(invertIfNegative1);
            barChartSeries1.Append(ApplyDataLabels_Portrait(showCategoryName, showLeaderLines, firstRow, lastRow, firstCol, lastCol, "0.0;;", sheetName: tempTable));
            barChartSeries1.Append(SetStringDataLinkValues(worksheetPart, tempTable, firstRow, lastRow, 2, 2));
            barChartSeries1.Append(SetNumericDataLinkValues(worksheetPart, tempTable, firstRow, lastRow, firstCol, firstCol));

            C.GapWidth gapWidth1 = new C.GapWidth() { Val = (UInt16Value)30U };
            C.AxisId axisId1 = new C.AxisId() { Val = (UInt32Value)184588544U };
            C.AxisId axisId2 = new C.AxisId() { Val = (UInt32Value)184598912U };

            barChart1.Append(barChartSeries1);
            barChart1.Append(gapWidth1);
            barChart1.Append(axisId1);
            barChart1.Append(axisId2);

            //scatter chart start
            C.ScatterChart scatterChart1 = new C.ScatterChart();

            if (HasLines)
            {
                C.ScatterStyle scatterStyle1 = new C.ScatterStyle() { Val = C.ScatterStyleValues.LineMarker };
                C.VaryColors varyColors2 = new C.VaryColors() { Val = false };
                int sideTableChoiceRow = 28;
                List<String> values = new List<string>();


                for (int j = 0; j < LinesIndexList.Count; j++)
                {
                    int sideTableFirstCol = 16;
                    C.ScatterChartSeries scatterChartSeries1 = new C.ScatterChartSeries();
                    C.Index index2 = new C.Index() { Val = (UInt32Value)(j + 2U) };
                    C.Order order2 = new C.Order() { Val = (UInt32Value)(j + 1U) };
                    sideTableFirstCol += LinesIndexList[j];
                    scatterChartSeries1.Append(index2);
                    scatterChartSeries1.Append(order2);

                    C.SeriesText seriesText2 = new C.SeriesText();
                    C.StringReference stringReference3 = new C.StringReference();
                    C.Formula formula8 = new C.Formula();
                    formula8.Text = "\'" + tempTable + "\'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(sideTableFirstCol) + "$" + sideTableChoiceRow;
                    C.StringCache stringCache2 = new C.StringCache();
                    C.PointCount pointCount4 = new C.PointCount() { Val = (UInt32Value)1U };
                    C.StringPoint stringPoint2 = new C.StringPoint() { Index = (UInt32Value)0U };
                    C.NumericValue numericValue2 = new C.NumericValue();
                    numericValue2.Text = "";
                    stringPoint2.Append(numericValue2);
                    stringCache2.Append(pointCount4);
                    stringCache2.Append(stringPoint2);
                    stringReference3.Append(formula8);
                    stringReference3.Append(stringCache2);
                    seriesText2.Append(stringReference3);
                    scatterChartSeries1.Append(seriesText2);

                    var clr = System.Drawing.Color.FromArgb(ColorPallet.colorIndex[ColorPallet.colorLineIndex[(j) % ColorPallet.colorLineIndex.Length]]);
                    var rgb = clr.B.ToString("X2") + clr.G.ToString("X2") + clr.R.ToString("X2");

                    //scatterChartSeries1.Append(SetLineChartProperty(rgb));
                    C.ChartShapeProperties chartShapeProperties3 = new C.ChartShapeProperties();
                    A.Outline outline14 = new A.Outline() { Width = 3175 };
                    A.SolidFill solidFill24 = new A.SolidFill();
                    A.RgbColorModelHex rgbColorModelHex30 = new A.RgbColorModelHex() { Val = rgb };
                    solidFill24.Append(rgbColorModelHex30);
                    outline14.Append(solidFill24);
                    chartShapeProperties3.Append(outline14);
                    scatterChartSeries1.Append(chartShapeProperties3);

                    C.Marker marker1 = new C.Marker();
                    C.Symbol symbol1 = new C.Symbol() { Val = C.MarkerStyleValues.Square };
                    C.Size size1 = new C.Size() { Val = 6 };

                    C.ChartShapeProperties chartShapeProperties4 = new C.ChartShapeProperties();
                    A.SolidFill solidFill25 = new A.SolidFill();
                    A.RgbColorModelHex rgbColorModelHex31 = new A.RgbColorModelHex() { Val = rgb };
                    solidFill25.Append(rgbColorModelHex31);
                    A.Outline outline15 = new A.Outline();
                    A.SolidFill solidFill26 = new A.SolidFill();
                    A.SchemeColor schemeColor19 = new A.SchemeColor() { Val = A.SchemeColorValues.Background1 };
                    solidFill26.Append(schemeColor19);
                    outline15.Append(solidFill26);
                    chartShapeProperties4.Append(solidFill25);
                    chartShapeProperties4.Append(outline15);

                    marker1.Append(symbol1);
                    marker1.Append(size1);
                    marker1.Append(chartShapeProperties4);
                    //marker1.Append(SetMarkerProperty(rgb));

                    UInt32Value numberOfPoints = Convert.ToUInt32(lastRow - firstRow + 1);
                    scatterChartSeries1.Append(SetNumericXDataLinkValues(worksheetPart, tempTable, firstRow, lastRow, sideTableFirstCol, sideTableFirstCol, numberOfPoints));
                    C.YValues yValues1 = new C.YValues();
                    C.NumberLiteral numberLiteral1 = new C.NumberLiteral();
                    C.FormatCode formatCode3 = new C.FormatCode();
                    formatCode3.Text = "General";
                    C.PointCount pointCount6 = new C.PointCount() { Val = (UInt32Value)(numberOfPoints) };
                    numberLiteral1.Append(formatCode3);
                    numberLiteral1.Append(pointCount6);

                    double pointValue = 0.5;
                    UInt32Value index = 0;
                    for (int i = firstRow; i <= lastRow; i++, pointValue += 1)
                    {
                        C.NumericPoint numericPoint = new C.NumericPoint() { Index = index };
                        C.NumericValue numericValue = new C.NumericValue();
                        numericValue.Text = pointValue.ToString();
                        numericPoint.Append(numericValue);
                        numberLiteral1.Append(numericPoint);
                        index++;
                    }

                    yValues1.Append(numberLiteral1);

                    C.Smooth smooth1 = new C.Smooth() { Val = false };
                    scatterChartSeries1.Append(marker1);
                    scatterChartSeries1.Append(yValues1);
                    scatterChartSeries1.Append(smooth1);
                    scatterChart1.Append(scatterChartSeries1);
                    sideTableFirstCol++;
                }

                C.DataLabels dataLabels3 = new C.DataLabels();
                C.ShowLegendKey showLegendKey3 = new C.ShowLegendKey() { Val = false };
                C.ShowValue showValue3 = new C.ShowValue() { Val = false };
                C.ShowCategoryName showCategoryName3 = new C.ShowCategoryName() { Val = false };
                C.ShowSeriesName showSeriesName3 = new C.ShowSeriesName() { Val = false };
                C.ShowPercent showPercent3 = new C.ShowPercent() { Val = false };
                C.ShowBubbleSize showBubbleSize3 = new C.ShowBubbleSize() { Val = false };

                dataLabels3.Append(showLegendKey3);
                dataLabels3.Append(showValue3);
                dataLabels3.Append(showCategoryName3);
                dataLabels3.Append(showSeriesName3);
                dataLabels3.Append(showPercent3);
                dataLabels3.Append(showBubbleSize3);

                C.AxisId axisId3 = new C.AxisId() { Val = (UInt32Value)184601984U };
                C.AxisId axisId4 = new C.AxisId() { Val = (UInt32Value)184600448U };

                scatterChart1.Append(scatterStyle1);
                scatterChart1.Append(varyColors2);
                scatterChart1.Append(dataLabels3);
                scatterChart1.Append(axisId3);
                scatterChart1.Append(axisId4);
            }

            C.CategoryAxis categoryAxis1 = new C.CategoryAxis();
            C.AxisId axisId5 = new C.AxisId() { Val = (UInt32Value)184588544U };

            C.Scaling scaling1 = new C.Scaling();
            C.Orientation orientation1 = new C.Orientation() { Val = C.OrientationValues.MaxMin };

            scaling1.Append(orientation1);
            C.Delete delete1 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition1 = new C.AxisPosition() { Val = C.AxisPositionValues.Left };
            C.NumberingFormat numberingFormat11 = new C.NumberingFormat() { FormatCode = "General", SourceLinked = false };
            C.MajorTickMark majorTickMark1 = new C.MajorTickMark() { Val = C.TickMarkValues.None };
            C.MinorTickMark minorTickMark1 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition1 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };
            C.CrossingAxis crossingAxis1 = new C.CrossingAxis() { Val = (UInt32Value)184598912U };
            C.Crosses crosses1 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.AutoLabeled autoLabeled1 = new C.AutoLabeled() { Val = true };
            C.LabelAlignment labelAlignment1 = new C.LabelAlignment() { Val = C.LabelAlignmentValues.Center };
            C.LabelOffset labelOffset1 = new C.LabelOffset() { Val = (UInt16Value)100U };
            C.NoMultiLevelLabels noMultiLevelLabels1 = new C.NoMultiLevelLabels() { Val = false };

            categoryAxis1.Append(axisId5);
            categoryAxis1.Append(scaling1);
            categoryAxis1.Append(delete1);
            categoryAxis1.Append(axisPosition1);
            categoryAxis1.Append(numberingFormat11);
            categoryAxis1.Append(majorTickMark1);
            categoryAxis1.Append(minorTickMark1);
            categoryAxis1.Append(tickLabelPosition1);
            categoryAxis1.Append(crossingAxis1);
            categoryAxis1.Append(crosses1);
            categoryAxis1.Append(autoLabeled1);
            categoryAxis1.Append(labelAlignment1);
            categoryAxis1.Append(labelOffset1);
            categoryAxis1.Append(noMultiLevelLabels1);

            C.ValueAxis valueAxis1 = new C.ValueAxis();
            C.AxisId axisId6 = new C.AxisId() { Val = (UInt32Value)184598912U };

            C.Scaling scaling2 = new C.Scaling();
            C.Orientation orientation2 = new C.Orientation() { Val = C.OrientationValues.MinMax };
            C.MaxAxisValue maxAxisValue1 = new C.MaxAxisValue() { Val = 100D };
            C.MinAxisValue minAxisValue1 = new C.MinAxisValue() { Val = 0D };

            scaling2.Append(orientation2);
            scaling2.Append(maxAxisValue1);
            scaling2.Append(minAxisValue1);
            C.Delete delete2 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition2 = new C.AxisPosition() { Val = C.AxisPositionValues.Top };
            C.NumberingFormat numberingFormat12 = new C.NumberingFormat() { FormatCode = "0.0_ ", SourceLinked = true };
            C.MajorTickMark majorTickMark2 = new C.MajorTickMark() { Val = C.TickMarkValues.Inside };
            C.MinorTickMark minorTickMark2 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition2 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.High };
            C.CrossingAxis crossingAxis2 = new C.CrossingAxis() { Val = (UInt32Value)184588544U };
            C.Crosses crosses2 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween1 = new C.CrossBetween() { Val = C.CrossBetweenValues.Between };
            C.MajorUnit majorUnit1 = new C.MajorUnit() { Val = 20D };

            valueAxis1.Append(axisId6);
            valueAxis1.Append(scaling2);
            valueAxis1.Append(delete2);
            valueAxis1.Append(axisPosition2);
            valueAxis1.Append(numberingFormat12);
            valueAxis1.Append(majorTickMark2);
            valueAxis1.Append(minorTickMark2);
            valueAxis1.Append(tickLabelPosition2);
            valueAxis1.Append(crossingAxis2);
            valueAxis1.Append(crosses2);
            valueAxis1.Append(crossBetween1);
            valueAxis1.Append(majorUnit1);

            C.ValueAxis valueAxis2 = new C.ValueAxis();
            C.AxisId axisId7 = new C.AxisId() { Val = (UInt32Value)184600448U };

            C.Scaling scaling3 = new C.Scaling();
            C.Orientation orientation3 = new C.Orientation() { Val = C.OrientationValues.MaxMin };
            C.MaxAxisValue maxAxisValue2 = new C.MaxAxisValue() { Val = (lastRow - firstRow + 1) };
            C.MinAxisValue minAxisValue2 = new C.MinAxisValue() { Val = 0D };

            scaling3.Append(orientation3);
            scaling3.Append(maxAxisValue2);
            scaling3.Append(minAxisValue2);
            C.Delete delete3 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition3 = new C.AxisPosition() { Val = C.AxisPositionValues.Right };
            C.NumberingFormat numberingFormat13 = new C.NumberingFormat() { FormatCode = "General", SourceLinked = true };
            C.MajorTickMark majorTickMark3 = new C.MajorTickMark() { Val = C.TickMarkValues.Outside };
            C.MinorTickMark minorTickMark3 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition3 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };
            C.CrossingAxis crossingAxis3 = new C.CrossingAxis() { Val = (UInt32Value)184601984U };
            C.Crosses crosses3 = new C.Crosses() { Val = C.CrossesValues.Maximum };
            C.CrossBetween crossBetween2 = new C.CrossBetween() { Val = C.CrossBetweenValues.MidpointCategory };

            valueAxis2.Append(axisId7);
            valueAxis2.Append(scaling3);
            valueAxis2.Append(delete3);
            valueAxis2.Append(axisPosition3);
            valueAxis2.Append(numberingFormat13);
            valueAxis2.Append(majorTickMark3);
            valueAxis2.Append(minorTickMark3);
            valueAxis2.Append(tickLabelPosition3);
            valueAxis2.Append(crossingAxis3);
            valueAxis2.Append(crosses3);
            valueAxis2.Append(crossBetween2);

            C.ValueAxis valueAxis3 = new C.ValueAxis();
            C.AxisId axisId8 = new C.AxisId() { Val = (UInt32Value)184601984U };

            C.Scaling scaling4 = new C.Scaling();
            C.Orientation orientation4 = new C.Orientation() { Val = C.OrientationValues.MinMax };

            scaling4.Append(orientation4);
            C.Delete delete4 = new C.Delete() { Val = true };
            C.AxisPosition axisPosition4 = new C.AxisPosition() { Val = C.AxisPositionValues.Top };
            C.NumberingFormat numberingFormat14 = new C.NumberingFormat() { FormatCode = "\"▲\"0.0", SourceLinked = true };
            C.MajorTickMark majorTickMark4 = new C.MajorTickMark() { Val = C.TickMarkValues.Outside };
            C.MinorTickMark minorTickMark4 = new C.MinorTickMark() { Val = C.TickMarkValues.None };
            C.TickLabelPosition tickLabelPosition4 = new C.TickLabelPosition() { Val = C.TickLabelPositionValues.NextTo };
            C.CrossingAxis crossingAxis4 = new C.CrossingAxis() { Val = (UInt32Value)184600448U };
            C.Crosses crosses4 = new C.Crosses() { Val = C.CrossesValues.AutoZero };
            C.CrossBetween crossBetween3 = new C.CrossBetween() { Val = C.CrossBetweenValues.MidpointCategory };

            valueAxis3.Append(axisId8);
            valueAxis3.Append(scaling4);
            valueAxis3.Append(delete4);
            valueAxis3.Append(axisPosition4);
            valueAxis3.Append(numberingFormat14);
            valueAxis3.Append(majorTickMark4);
            valueAxis3.Append(minorTickMark4);
            valueAxis3.Append(tickLabelPosition4);
            valueAxis3.Append(crossingAxis4);
            valueAxis3.Append(crosses4);
            valueAxis3.Append(crossBetween3);

            C.ShapeProperties shapeProperties3 = new C.ShapeProperties();
            A.NoFill noFill3 = new A.NoFill();

            A.Outline outline12 = new A.Outline();
            A.NoFill noFill4 = new A.NoFill();

            outline12.Append(noFill4);

            shapeProperties3.Append(noFill3);
            shapeProperties3.Append(outline12);

            plotArea1.Append(barChart1);
            if (HasLines) plotArea1.Append(scatterChart1);
            plotArea1.Append(categoryAxis1);
            plotArea1.Append(valueAxis1);
            if (HasLines)
            {
                plotArea1.Append(valueAxis2);
                plotArea1.Append(valueAxis3);
            }
            plotArea1.Append(shapeProperties3);
            C.PlotVisibleOnly plotVisibleOnly1 = new C.PlotVisibleOnly() { Val = true };
            C.DisplayBlanksAs displayBlanksAs1 = new C.DisplayBlanksAs() { Val = C.DisplayBlanksAsValues.Gap };
            C.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new C.ShowDataLabelsOverMaximum() { Val = false };

            chart1.Append(autoTitleDeleted1);
            chart1.Append(plotArea1);
            chart1.Append(plotVisibleOnly1);
            chart1.Append(displayBlanksAs1);
            chart1.Append(showDataLabelsOverMaximum1);

            C.ShapeProperties shapeProperties4 = new C.ShapeProperties();
            A.NoFill noFill5 = new A.NoFill();

            A.Outline outline13 = new A.Outline();
            A.NoFill noFill6 = new A.NoFill();

            outline13.Append(noFill6);

            shapeProperties4.Append(noFill5);
            shapeProperties4.Append(outline13);

            chartSpace1.Append(date19041);
            chartSpace1.Append(editingLanguage1);
            chartSpace1.Append(roundedCorners1);
            chartSpace1.Append(alternateContent2);
            chartSpace1.Append(chart1);
            chartSpace1.Append(shapeProperties4);
            chartSpace1.Append(SetTextPrperty(800));

            chartPart.ChartSpace = chartSpace1;
        }

        public static C.ChartShapeProperties SetMarkerProperty(string colur)
        {
            C.ChartShapeProperties chartShapeProperties4 = new C.ChartShapeProperties();

            A.SolidFill solidFill9 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex17 = new A.RgbColorModelHex() { Val = colur};

            solidFill9.Append(rgbColorModelHex17);

            A.Outline outline7 = new A.Outline() { Width = 9525 };

            A.SolidFill solidFill10 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex18 = new A.RgbColorModelHex() { Val = "FFFFFF" };

            solidFill10.Append(rgbColorModelHex18);
            A.PresetDash presetDash5 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline7.Append(solidFill10);
            outline7.Append(presetDash5);

            chartShapeProperties4.Append(solidFill9);
            chartShapeProperties4.Append(outline7);

            return chartShapeProperties4;
        }
        public static C.ChartShapeProperties SetLineChartProperty(string colur)
        {
            C.ChartShapeProperties chartShapeProperties3 = new C.ChartShapeProperties();

            A.Outline outline6 = new A.Outline() { Width = 3175 };

            A.SolidFill solidFill8 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex16 = new A.RgbColorModelHex() { Val = colur};

            solidFill8.Append(rgbColorModelHex16);
            A.PresetDash presetDash4 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline6.Append(solidFill8);
            outline6.Append(presetDash4);
            A.EffectList effectList5 = new A.EffectList();

            chartShapeProperties3.Append(outline6);
            chartShapeProperties3.Append(effectList5);

            return chartShapeProperties3;
        }
        public static C.Legend SetLegend()
        {
            C.Legend legend1 = new C.Legend();
            C.LegendPosition legendPosition1 = new C.LegendPosition() { Val = C.LegendPositionValues.Bottom };
            C.Overlay overlay2 = new C.Overlay() { Val = false };
            C.Layout layout2 = new C.Layout();

            C.ManualLayout manualLayout2 = new C.ManualLayout();
            C.LeftMode leftMode2 = new C.LeftMode() { Val = C.LayoutModeValues.Edge };
            C.TopMode topMode2 = new C.TopMode() { Val = C.LayoutModeValues.Edge };
            C.Left left2 = new C.Left() { Val = 4.3478260869565216E-2D };
            C.Top top2 = new C.Top() { Val = 0.29764705882352941D };
            C.Width width2 = new C.Width() { Val = 0.9126172900262467D };
            C.Height height2 = new C.Height() { Val = 0.9470588235294118D };

            manualLayout2.Append(leftMode2);
            manualLayout2.Append(topMode2);
            manualLayout2.Append(left2);
            manualLayout2.Append(top2);
            manualLayout2.Append(width2);
            manualLayout2.Append(height2);

            layout2.Append(manualLayout2);

            C.ChartShapeProperties chartShapeProperties38 = new C.ChartShapeProperties();

            A.Outline outline42 = new A.Outline() { Width = 25400 };
            A.NoFill noFill37 = new A.NoFill();

            outline42.Append(noFill37);

            chartShapeProperties38.Append(outline42);

            legend1.Append(legendPosition1);
            legend1.Append(layout2);
            legend1.Append(overlay2);
            legend1.Append(chartShapeProperties38);
            return legend1;
        }
        public static C.TextProperties SetTextPrperty(int fontSize)
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
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = "en-US" };

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(endParagraphRunProperties1);

            textProperties.Append(bodyProperties3);
            textProperties.Append(listStyle3);
            textProperties.Append(paragraph3);
            return textProperties;
        }
        public static C.SeriesText SetSeriesText(string sheetName, int row, int col)
        {          
            C.SeriesText seriesText = new C.SeriesText();

            C.StringReference stringReference1 = new C.StringReference();
            C.Formula formula1 = new C.Formula();
            formula1.Text = "\'" + sheetName + "\'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(col) + "$" + row;

            stringReference1.Append(formula1);
            seriesText.Append(stringReference1);
            return seriesText;
        }
        public static C.PrintSettings SetPrintSetting()
        {
            C.PrintSettings printSettings = new C.PrintSettings();
            C.HeaderFooter headerFooter5 = new C.HeaderFooter();
            C.PageMargins pageMargins5 = new C.PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };
            C.PageSetup pageSetup5 = new C.PageSetup();

            printSettings.Append(headerFooter5);
            printSettings.Append(pageMargins5);
            printSettings.Append(pageSetup5);

            return printSettings;
        }
        public static C.ChartShapeProperties ApplyFillColour(string outLneclr, string inLineClr = null)
        {
            C.ChartShapeProperties chartShapeProperties = new C.ChartShapeProperties();

            if (inLineClr != null)
            {
                A.SolidFill solidFill1 = new A.SolidFill();
                A.RgbColorModelHex rgbColorModelHex18 = new A.RgbColorModelHex() { Val = inLineClr };
                solidFill1.Append(rgbColorModelHex18);
                chartShapeProperties.Append(solidFill1);
            }
            A.Outline outline6 = new A.Outline() { Width = 6700 };

            A.SolidFill solidFill2 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex19 = new A.RgbColorModelHex() { Val = outLneclr };

            solidFill2.Append(rgbColorModelHex19);

            outline6.Append(solidFill2);
            chartShapeProperties.Append(outline6);
            return chartShapeProperties;
        }

        public static C.DataPoint ApplyFillXlClustered(string outLineClr, string innerClr, int indexer)
        {
            C.DataPoint dataPoint1 = new C.DataPoint();
            C.Index index2 = new C.Index() { Val = (uint)indexer };
            C.InvertIfNegative invertIfNegative2 = new C.InvertIfNegative() { Val = false };
            C.Bubble3D bubble3D1 = new C.Bubble3D() { Val = false };

            C.ChartShapeProperties chartShapeProperties2 = new C.ChartShapeProperties();

            A.SolidFill solidFill8 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex16 = new A.RgbColorModelHex() { Val = innerClr };

            solidFill8.Append(rgbColorModelHex16);

            A.Outline outline5 = new A.Outline() { Width = 6700 };

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

        public static C.DataLabels ApplyDataLabels(bool showCategoryName, bool showLeaderLines, string NumberFormat = null,bool showPercent = true)
        {
            C.DataLabels dataLabels = new C.DataLabels();

            C.TextProperties textProperties = new C.TextProperties();


            A.BodyProperties bodyProperties = new A.BodyProperties() { Wrap = A.TextWrappingValues.None, Anchor = A.TextAnchoringTypeValues.Center };

            A.ListStyle listStyle = new A.ListStyle();

            A.Paragraph paragraph = new A.Paragraph();

            A.ParagraphProperties paragraphProperties = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties = new A.DefaultRunProperties();

            paragraphProperties.Append(defaultRunProperties);
            paragraph.Append(paragraphProperties);

            textProperties.Append(bodyProperties);
            textProperties.Append(listStyle);
            textProperties.Append(paragraph);
        
            C.NumberingFormat numberingFormat4 = new C.NumberingFormat() { FormatCode = NumberFormat, SourceLinked = false };

            C.ChartShapeProperties chartShapeProperties2 = new C.ChartShapeProperties();
            A.NoFill noFill1 = new A.NoFill();

            A.Outline outline5 = new A.Outline();
            A.NoFill noFill2 = new A.NoFill();

            outline5.Append(noFill2);
            A.EffectList effectList4 = new A.EffectList();

            chartShapeProperties2.Append(noFill1);
            chartShapeProperties2.Append(outline5);
            chartShapeProperties2.Append(effectList4);
            C.ShowLegendKey showLegendKey1 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue1 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName1 = new C.ShowCategoryName() { Val = showCategoryName };
            C.ShowSeriesName showSeriesName1 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent1 = new C.ShowPercent() { Val = showPercent };
            C.ShowBubbleSize showBubbleSize1 = new C.ShowBubbleSize() { Val = false };
            C.Separator separator1 = new C.Separator();
            separator1.Text = Space(1);
            C.ShowLeaderLines showLeaderLines1 = new C.ShowLeaderLines() { Val = showLeaderLines };

            dataLabels.Append(numberingFormat4);
            dataLabels.Append(chartShapeProperties2);
            dataLabels.Append(textProperties);
            dataLabels.Append(showLegendKey1);
            dataLabels.Append(showValue1);
            dataLabels.Append(showCategoryName1);
            dataLabels.Append(showSeriesName1);
            dataLabels.Append(showPercent1);
            dataLabels.Append(showBubbleSize1);
            dataLabels.Append(separator1);
            dataLabels.Append(showLeaderLines1);
            return dataLabels;
        }

        public static C.DataLabels ApplyDataLabels_Portrait(bool showCategoryName, bool showLeaderLines, int firstRow, int lastRow, int firstCol, int lastCol, string NumberFormat = null, string sheetName = null)
        {
            C.DataLabels dataLabels = new C.DataLabels();

            UInt32Value index = 0;
            for (int r = firstRow; r <= lastRow; r++)
            {
                C.DataLabel dataLabel1 = new C.DataLabel();

                C.Index index2 = new C.Index() { Val = index };

                C.Layout layout2 = new C.Layout();

                C.ManualLayout manualLayout1 = new C.ManualLayout();
                C.Left left1 = new C.Left() { Val = -2.3753881748072134E-2D };//-2.5003881748072134E-2D

                manualLayout1.Append(left1);

                layout2.Append(manualLayout1);
                C.ShowLegendKey showLegendKey2 = new C.ShowLegendKey() { Val = false };
                C.ShowValue showValue2 = new C.ShowValue() { Val = true };
                C.ShowCategoryName showCategoryName2 = new C.ShowCategoryName() { Val = false };
                C.ShowSeriesName showSeriesName2 = new C.ShowSeriesName() { Val = false };
                C.ShowPercent showPercent2 = new C.ShowPercent() { Val = true };
                C.ShowBubbleSize showBubbleSize2 = new C.ShowBubbleSize() { Val = false };
                C.Separator separator2 = new C.Separator();
                separator2.Text = "";

                dataLabel1.Append(index2);
                dataLabel1.Append(layout2);
                dataLabel1.Append(showLegendKey2);
                dataLabel1.Append(showValue2);
                dataLabel1.Append(showCategoryName2);
                dataLabel1.Append(showSeriesName2);
                dataLabel1.Append(showPercent2);
                dataLabel1.Append(showBubbleSize2);
                dataLabel1.Append(separator2);
                dataLabels.Append(dataLabel1);
                index++;
            }

            C.TextProperties textProperties = new C.TextProperties();


            A.BodyProperties bodyProperties = new A.BodyProperties() { Wrap = A.TextWrappingValues.None, Anchor = A.TextAnchoringTypeValues.Center };

            A.ListStyle listStyle = new A.ListStyle();

            A.Paragraph paragraph = new A.Paragraph();

            A.ParagraphProperties paragraphProperties = new A.ParagraphProperties();
            A.DefaultRunProperties defaultRunProperties = new A.DefaultRunProperties();

            paragraphProperties.Append(defaultRunProperties);
            paragraph.Append(paragraphProperties);

            textProperties.Append(bodyProperties);
            textProperties.Append(listStyle);
            textProperties.Append(paragraph);

            C.NumberingFormat numberingFormat4 = new C.NumberingFormat() { FormatCode = NumberFormat, SourceLinked = false };

            C.ChartShapeProperties chartShapeProperties2 = new C.ChartShapeProperties();
            A.NoFill noFill1 = new A.NoFill();

            A.Outline outline5 = new A.Outline();
            A.NoFill noFill2 = new A.NoFill();

            outline5.Append(noFill2);
            A.EffectList effectList4 = new A.EffectList();

            chartShapeProperties2.Append(noFill1);
            chartShapeProperties2.Append(outline5);
            chartShapeProperties2.Append(effectList4);
            C.ShowLegendKey showLegendKey1 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue1 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName1 = new C.ShowCategoryName() { Val = showCategoryName };
            C.ShowSeriesName showSeriesName1 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent1 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize1 = new C.ShowBubbleSize() { Val = false };
            C.Separator separator1 = new C.Separator();
            separator1.Text = Space(1);
            C.ShowLeaderLines showLeaderLines1 = new C.ShowLeaderLines() { Val = showLeaderLines };

            dataLabels.Append(numberingFormat4);
            dataLabels.Append(chartShapeProperties2);
            dataLabels.Append(textProperties);
            dataLabels.Append(showLegendKey1);
            dataLabels.Append(showValue1);
            dataLabels.Append(showCategoryName1);
            dataLabels.Append(showSeriesName1);
            dataLabels.Append(showPercent1);
            dataLabels.Append(showBubbleSize1);
            dataLabels.Append(separator1);
            dataLabels.Append(showLeaderLines1);
            return dataLabels;
        }

        public static C.DataLabel DataLabel()
        {
            C.DataLabel dataLabel1 = new C.DataLabel();
            C.Index index2 = new C.Index() { Val = (UInt32Value)0U };

            C.NumberingFormat numberingFormat6 = new C.NumberingFormat() { FormatCode = "0.0;;", SourceLinked = false };

            C.ChartShapeProperties chartShapeProperties2 = new C.ChartShapeProperties();
            A.NoFill noFill1 = new A.NoFill();

            A.Outline outline5 = new A.Outline();
            A.NoFill noFill2 = new A.NoFill();

            outline5.Append(noFill2);
            A.EffectList effectList4 = new A.EffectList();

            chartShapeProperties2.Append(noFill1);
            chartShapeProperties2.Append(outline5);
            chartShapeProperties2.Append(effectList4);

            C.DataLabelPosition dataLabelPosition1 = new C.DataLabelPosition() { Val = C.DataLabelPositionValues.OutsideEnd };
            C.ShowLegendKey showLegendKey1 = new C.ShowLegendKey() { Val = false };
            C.ShowValue showValue1 = new C.ShowValue() { Val = true };
            C.ShowCategoryName showCategoryName1 = new C.ShowCategoryName() { Val = false };
            C.ShowSeriesName showSeriesName1 = new C.ShowSeriesName() { Val = false };
            C.ShowPercent showPercent1 = new C.ShowPercent() { Val = false };
            C.ShowBubbleSize showBubbleSize1 = new C.ShowBubbleSize() { Val = false };
            C.Separator separator1 = new C.Separator();
            separator1.Text = "";

            C.DLblExtensionList dLblExtensionList1 = new C.DLblExtensionList();

            C.DLblExtension dLblExtension1 = new C.DLblExtension() { Uri = "{CE6537A1-D6FC-4f65-9D91-7224C49458BB}" };

            C15.Layout layout2 = new C15.Layout();

            C.ManualLayout manualLayout1 = new C.ManualLayout();
            C.Width width1 = new C.Width() { Val = 0.20883520928741151D };
            C.Height height1 = new C.Height() { Val = 6.7647058823529407E-2D };

            manualLayout1.Append(width1);
            manualLayout1.Append(height1);

            layout2.Append(manualLayout1);
            C15.ShowDataLabelsRange showDataLabelsRange1 = new C15.ShowDataLabelsRange() { Val = false };

            dLblExtension1.Append(layout2);
            dLblExtension1.Append(showDataLabelsRange1);

            C.DLblExtension dLblExtension2 = new C.DLblExtension() { Uri = "{C3380CC4-5D6E-409C-BE32-E72D297353CC}" };

            OpenXmlUnknownElement openXmlUnknownElement3 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<c16:uniqueId val=\"{00000002-362A-4725-9794-DDBE50250524}\" xmlns:c16=\"http://schemas.microsoft.com/office/drawing/2014/chart\" />");

            //dLblExtension2.Append(openXmlUnknownElement3);

            dLblExtensionList1.Append(dLblExtension1);
            dLblExtensionList1.Append(dLblExtension2);

            dataLabel1.Append(index2);
            //dataLabel1.Append(chartShapeProperties2);
            dataLabel1.Append(dataLabelPosition1);
            dataLabel1.Append(showLegendKey1);
            dataLabel1.Append(showValue1);
            dataLabel1.Append(showCategoryName1);
            dataLabel1.Append(showSeriesName1);
            dataLabel1.Append(showPercent1);
            dataLabel1.Append(showBubbleSize1);
            dataLabel1.Append(separator1);

            return dataLabel1;
        }
        public static C.CategoryAxisData SetStringDataLinkValues(WorksheetPart worksheetPart, string perSheetName, int perStartRow, int endRow, int perLastCol, int perFirstCol = 0)
        {
            C.CategoryAxisData categoryAxisData = new C.CategoryAxisData();

            C.StringReference stringReference1 = new C.StringReference();
            C.Formula formula1 = new C.Formula();
            formula1.Text = "\'" + perSheetName + "\'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(perFirstCol) + "$" + perStartRow + ":$" + OpenXmlHelper.ColumnIndexToColumnLetter(perLastCol) + "$" + endRow;

            stringReference1.Append(formula1);
            categoryAxisData.Append(stringReference1);
            return categoryAxisData;
        }
        public static C.Values SetNumericDataLinkValues(WorksheetPart worksheetPart, string sheetName, int firstRow, int lastRow, int lastCol, int firstCol = 0)
        {
            C.Values values = new C.Values();

            C.NumberReference numberReference = new C.NumberReference();
            C.Formula formula2 = new C.Formula();
            formula2.Text = "\'" + sheetName + "\'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(firstCol) + "$" + firstRow + ":$" + OpenXmlHelper.ColumnIndexToColumnLetter(lastCol) + "$" + lastRow;
            C.NumberingCache numberingCache1 = new C.NumberingCache();
            numberReference.Append(formula2);
            values.Append(numberReference);
            return values;
        }

        public static C.XValues SetNumericXDataLinkValues(WorksheetPart worksheetPart, string sheetName, int firstRow, int lastRow, int lastCol, int firstCol = 0, UInt32 numberOfPoints = 0)
        {
            C.XValues values = new C.XValues();

            C.NumberReference numberReference = new C.NumberReference();
            C.Formula formula2 = new C.Formula();
            formula2.Text = "\'" + sheetName + "\'!$" + OpenXmlHelper.ColumnIndexToColumnLetter(firstCol) + "$" + firstRow + ":$" + OpenXmlHelper.ColumnIndexToColumnLetter(lastCol) + "$" + lastRow;
            C.NumberingCache numberingCache3 = new C.NumberingCache();
            C.FormatCode formatCode3 = new C.FormatCode();
            formatCode3.Text = "0.0";
            C.PointCount pointCount5 = new C.PointCount() { Val = (UInt32Value)numberOfPoints };
            numberingCache3.Append(formatCode3);
            numberingCache3.Append(pointCount5);
            numberReference.Append(formula2);
            numberReference.Append(numberingCache3);
            values.Append(numberReference);

            return values;
        }

        private static string Space(int count = 1)
        {
            string space = " ";
            string retVal = string.Empty;
            for (int i = 1; i <= count; i++)
            {
                retVal = retVal + space;
            }
            return retVal;
        }
        public static string GetSeriesTextValue(WorksheetPart worksheetPart, int row, int col)
        {
            Row row1 = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)row);
            Cell cell = OpenXmlHelper.GetCell(row1, row, col);
            if (cell == null)
                return null;
            else
                return cell.CellValue == null ? "" : (cell.CellValue.InnerText);
        }
    }
}
