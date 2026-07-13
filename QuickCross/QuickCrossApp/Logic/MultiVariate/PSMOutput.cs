using ExcelAddIn.Common;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.Question;
using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using System;
using System.Linq;

namespace Qc4Launcher.Logic.MultiVariate
{
    internal class PSMOutput
    {
        public static string TEMPLATE_NAME_JA = "PSM_Template_ja.xltx";
        public static string TEMPLATE_NAME = "PSM_Template.xltx";
        public static string DATA_SHEET = "Sheet5";
        public static string STAT_SHEET = "Sheet4";
        public static string GRAPH_SHEET = "Sheet3";


        public static long OutCol = 3;
        public static long OR_Cnt = 2;
        public static long OR_Valid = 3;
        public static long OR_CalcPrice = 5;
        public static long OR_Numerical = 10;
        public static long OR_Satistics = 18;
        public static long OR_PercentTitle = 33;
        public static long OR_PercentData = 34;

        public static string strOutputCell_S = "D9:G21";
        public static string strOutputCell_C = "C26";

        public static string CopyFromFormula = "D49:D53";
        public static string PasteToFormula = "D49";
        public static string CopyFromRange = "E49:E53";
        public static string PasteToRangeS = "E49";
        public static string PasteToRangeE = "E53";



        public static string strItemPrefix = "[";
        string strItemSuffix = "]";
        string strEdit01 = LocalResource.PSM_FILTER_VAL;// "...value of...";
        string strEdit_LG = LocalResource.PSM_FILTER_LG;//"  excluding";
        string strEdit_E = "";//"";
        string strEdit_LE = LocalResource.PSM_FILTER_LE;//" or under";
        string strEdit_GE = LocalResource.PSM_FILTER_GE;//" or over";
        string strEdit_L = LocalResource.PSM_FILTER_L;//" under";
        string strEdit_G = LocalResource.PSM_FILTER_G;//" over";
        string strEdit_And = LocalResource.PSM_FILTER_AND;//"  AND";
        string strEdit_Or = LocalResource.PSM_FILTER_OR;//"  OR";
        string strNoAnswer = LocalResource.PSM_FILTER_NO_ANSWER;//"'No Answer'";
        string strNotApply = LocalResource.PSM_FILTER_NOT_APPLY;//"'Excluded'";

        public static string u_DK = "DK";
        public static string u_Asterisk = "*";


        public static string Sign_LG = "<>";
        public static string Sign_E = "=";
        public static string Sign_LE = "<=";
        public static string Sign_GE = ">=";
        public static string Sign_G = ">";
        public static string Sign_L = "<";
        public static string Sign_EX = "!";

        public static string D_AND = CrossSettingsReader.AND;
        public static string D_OR = CrossSettingsReader.OR;

        public static string ErrorStr = "Error";


        public static string Str_Siborikomi = LocalResource.PSM_FILTER_CRITREA;

        public string templateDirectoryPath;
        public Application xlApp;
        public Workbooks wbs;
        Workbook workBook = null;
        Workbook baseBook = null;
        Worksheet dataSheet = null;
        Worksheet grpahSheet = null;
        Worksheet stasticsShett = null;
        PSMOutputData outputData = null;
        PSMSettings pSMSettings = null;
        Questions questions;
        internal void generateOutPut(string templateDirectoryPath, Application xlApp, Workbook workBook, PSMOutputData outputData,
            PSMSettings pSMSettings, Questions questions, PSMCalc pSMCalc, out Workbook baseBook)
        {
            this.templateDirectoryPath = templateDirectoryPath;
            this.xlApp = xlApp;
            this.outputData = outputData;
            this.pSMSettings = pSMSettings;
            this.workBook = workBook;
            this.questions = questions;

           
            wbs = xlApp.Workbooks;
            string templateName = TEMPLATE_NAME;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
            {
                templateName = TEMPLATE_NAME_JA;
            }
            baseBook = wbs.Add(OutputUtil.getTemplatePath(this.templateDirectoryPath, templateName, xlApp.PathSeparator));
            try
            {
              
                dataSheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, DATA_SHEET);
                grpahSheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, GRAPH_SHEET);
                stasticsShett = ExcelUtil.GetWorkSheetByCodeName(baseBook, STAT_SHEET);
                System.Collections.Generic.List<Worksheet> shets = new System.Collections.Generic.List<Worksheet>();
                shets.Add(dataSheet);
                shets.Add(grpahSheet);
                shets.Add(stasticsShett);

                dataSheet.Cells.EntireColumn.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                grpahSheet.Cells.EntireColumn.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                stasticsShett.Cells.EntireColumn.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];

                double currentProgress = 60;
                pSMCalc.updateProgress(currentProgress, LocalResource.PSM_PB_RESLT_OUT);
                outputDataSheet();
                currentProgress = 70;
                pSMCalc.updateProgress(currentProgress, LocalResource.PSM_PB_RESLT_OUT);
                outputGraphSheet();
                currentProgress = 80;
                pSMCalc.updateProgress(currentProgress, LocalResource.PSM_PB_RESLT_OUT);
                outputStockGraph();
                currentProgress = 90;
                pSMCalc.updateProgress(currentProgress, LocalResource.PSM_PB_RESLT_OUT);
                outputStatisticsSheet();
                dataSheet.Visible = XlSheetVisibility.xlSheetVeryHidden;
                for (int i = 0; i < shets.Count; i++)
                {
                    foreach (Microsoft.Office.Interop.Excel.Shape shp in shets[i].Shapes)
                    {
                        try
                        {
                            string str = shp.TextFrame2.TextRange.Text;
                            shp.TextFrame2.TextRange.Font.NameFarEast = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                            shp.TextFrame2.TextRange.Characters.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                            shp.TextFrame2.TextRange.Text = str;
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }
            catch { }
            finally
            {

                if (stasticsShett != null)
                {
                    COMWholeOperate.releaseComObject(ref stasticsShett);
                }
                if (grpahSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref grpahSheet);
                }
                if (dataSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref dataSheet);
                }
               
                if (wbs != null)
                {
                    COMWholeOperate.releaseComObject(ref wbs);
                }
            }
        }

        private void outputStatisticsSheet()
        {
            Range TargetRange = stasticsShett.Range[strOutputCell_S];
            FormatConditions fc = TargetRange.FormatConditions;
            fc.Add(Type: XlFormatConditionType.xlCellValue, Operator: XlFormatConditionOperator.xlEqual, Formula1: ErrorStr);
            fc[1].Font.ColorIndex = 3;
            if (pSMSettings.HasFilter)
            {
                TargetRange = stasticsShett.Range[strOutputCell_C];
                FNC_An_ConditionalOutput(TargetRange, 3);
            }
        }

        private void FNC_An_ConditionalOutput(Range StartCell, long ColumnCount, long HeaderInteriorColor = 8421504, long HeaderFontColor = 16777215)
        {
            long pConditionalCount = 5;
            //string ConditionalWord = Edit_SiborikomiAll(pSMSettings.Filters);
            string ConditionalWord = Macromill.QCWeb.Logic.TabulationEx.Criteria.CriteriaDescProvider.Edit_SiborikomiAll_General(pSMSettings.Filters, questions);
            if (ConditionalWord != "")
            {
                Worksheet TargetSheet = StartCell.Worksheet;
                //	With TargetSheet
                Range HeaderRange = TargetSheet.Range[StartCell, StartCell.Offset[0, ColumnCount - 1]];
                Range ValueRange = TargetSheet.Range[StartCell.Offset[1, 0], StartCell.Offset[pConditionalCount, ColumnCount - 1]];
                PRV_MergeConditionalRange(HeaderRange);
                PRV_MergeConditionalRange(ValueRange);
                HeaderRange.BorderAround(XlLineStyle.xlContinuous, Weight: XlBorderWeight.xlThin, Color: ColorPallet.colorIndex[15]);
                ValueRange.BorderAround(XlLineStyle.xlContinuous, Weight: XlBorderWeight.xlThin, Color: ColorPallet.colorIndex[15]);
                HeaderRange.Cells[1, 1].Value = Str_Siborikomi;
                ValueRange.Cells[1, 1].Value = ConditionalWord;
                HeaderRange.Interior.Color = HeaderInteriorColor;
                HeaderRange.Font.Color = HeaderFontColor;
                HeaderRange.Font.Bold = true;
                ValueRange.Font.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                ValueRange.Font.Bold = false;
                ValueRange.WrapText = true;
            }
        }

        private void PRV_MergeConditionalRange(Range TargetRange)
        {
            if (TargetRange.Cells.Count > 1)
            {
                TargetRange.Merge();
            }
        }

        public string Edit_SiborikomiAll(System.Collections.Generic.List<FilterSettingsCr> filters)
        {
            string EditString = "";
            string space = " ";
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
            {
                space = "";
            }
            if (filters == null || filters.Count == 0)
            {
                return EditString;
            }

            foreach (FilterSettingsCr filter in filters)
            {
                if (filter.variable == null || filter.variable.Length == 0)
                {
                    continue;
                }
                QuestionSettings qstnDet = Qc4Launcher.Util.Definiotion.VariableDictionary[filter.variable];
                Questions.Question itemInfo = (Questions.Question)questions[qstnDet.Id];
                if (filter.conditionType == D_AND)
                {
                    EditString += strEdit_And;
                }
                else if (filter.conditionType == D_OR)
                {
                    EditString += strEdit_Or;
                }
                if (EditString.Length > 0)
                {
                    EditString += "\n";
                }
                EditString += itemInfo.Name2 + space;

                if (filter.values == u_DK)
                {
                    EditString += strEdit01 + strNoAnswer;
                }
                else if (filter.values == u_Asterisk)
                {
                    EditString += strEdit01 + strNotApply;
                }
                else
                {
                    EditString += strEdit01 + filter.values + space;
                }


                if (filter.operatorType == Sign_LG)
                {
                    EditString += strEdit_LG;
                }
                else if (filter.operatorType == Sign_E)
                {
                    EditString += strEdit_E;
                }
                else if (filter.operatorType == Sign_LE)
                {
                    EditString += strEdit_LE;
                }
                else if (filter.operatorType == Sign_GE)
                {
                    EditString += strEdit_GE;
                }
                else if (filter.operatorType == Sign_L)
                {
                    EditString += strEdit_L;
                }
                else if (filter.operatorType == Sign_G)
                {
                    EditString += strEdit_G;
                }



            }
            return EditString;
        }

        private void outputStockGraph()
        {
            ChartObject T_ChartObj;
            long Digits;
            double TopPrice;
            double EndPrice;
            T_ChartObj = grpahSheet.ChartObjects("Stock_Graph");
            T_ChartObj.Chart.ChartArea.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            T_ChartObj.Activate();
            grpahSheet.Cells[1, 1].Select();
            TopPrice = outputData.ResultPrices.Max();
            EndPrice = outputData.ResultPrices.Min();
            Digits = GetDigits(TopPrice, EndPrice);
            WorksheetFunction wf = xlApp.WorksheetFunction;
            TopPrice = wf.RoundUp(TopPrice, Digits * -1);
            EndPrice = wf.RoundDown(EndPrice, Digits * -1);
            if (TopPrice == EndPrice)
            {
                return;
            }

            dynamic ax = T_ChartObj.Chart.Axes(XlAxisType.xlValue);
            T_ChartObj.Chart.Axes(XlAxisType.xlValue).MinimumScale = EndPrice;
            T_ChartObj.Chart.Axes(XlAxisType.xlValue).MaximumScale = TopPrice;
            T_ChartObj.Chart.Axes(XlAxisType.xlValue).MinorUnit = Math.Pow(10, Digits);
            T_ChartObj.Chart.Axes(XlAxisType.xlValue).MajorUnit = Math.Pow(10, Digits);
            OutputResult_StockGraph_SetSeries(T_ChartObj);
            OutputResult_StockGraph_SetShapes(TopPrice, EndPrice);
        }

        private void OutputResult_StockGraph_SetShapes(double TopPrice, double EndPrice)
        {
            double GraphHeight;
            Shape ArrowShape1;
            Shape ArrowShape2;
            Shape ArrowShape3;
            Shape ArrowShape4;
            Shape ArrowShape5; ;
            double Top_Pos;
            double End_Pos;
            double T_Price1;
            double T_Price2;
            double T_Price3;
            double T_Price4;
            Shape TextShape1;
            Shape TextShape2;
            Shape TextShape3;
            Shape TextShape4;
            Shape TextShape5;
            Shape Shape_Acceptable;
            Shape Text_Acceptable;
            WorksheetFunction wf = xlApp.WorksheetFunction;
            T_Price1 = wf.Large(outputData.ResultPrices, 1);
            T_Price2 = wf.Large(outputData.ResultPrices, 2);
            T_Price3 = wf.Large(outputData.ResultPrices, 3);
            T_Price4 = wf.Large(outputData.ResultPrices, 4);
            ArrowShape1 = grpahSheet.Shapes.Item("Arrow_TooExp");
            ArrowShape2 = grpahSheet.Shapes.Item("Arrow_Exp");
            ArrowShape3 = grpahSheet.Shapes.Item("Arrow_Fair");
            ArrowShape4 = grpahSheet.Shapes.Item("Arrow_Cheap");
            ArrowShape5 = grpahSheet.Shapes.Item("Arrow_TooCheap");
            TextShape1 = grpahSheet.Shapes.Item("Text_TooExp");
            TextShape2 = grpahSheet.Shapes.Item("Text_Exp");
            TextShape3 = grpahSheet.Shapes.Item("Text_Fair");
            TextShape4 = grpahSheet.Shapes.Item("Text_Cheap");
            TextShape5 = grpahSheet.Shapes.Item("Text_TooCheap");
            Shape_Acceptable = grpahSheet.Shapes.Item("Acceptable_Price");
            Text_Acceptable = grpahSheet.Shapes.Item("Text_Acceptable");

            Top_Pos = ArrowShape1.Top;
            End_Pos = ArrowShape5.Top + ArrowShape5.Height;
            GraphHeight = End_Pos - Top_Pos;
            ArrowShape1.Top = (float)Top_Pos;
            ArrowShape1.Height = (float)((TopPrice - T_Price1) / (TopPrice - EndPrice) * GraphHeight);
            ArrowShape2.Top = ArrowShape1.Top + ArrowShape1.Height;
            ArrowShape2.Height = (float)((T_Price1 - T_Price2) / (TopPrice - EndPrice) * GraphHeight);
            ArrowShape3.Top = ArrowShape2.Top + ArrowShape2.Height;
            ArrowShape3.Height = (float)((T_Price2 - T_Price3) / (TopPrice - EndPrice) * GraphHeight);
            ArrowShape4.Top = ArrowShape3.Top + ArrowShape3.Height;
            ArrowShape4.Height = (float)((T_Price3 - T_Price4) / (TopPrice - EndPrice) * GraphHeight);
            ArrowShape5.Top = ArrowShape4.Top + ArrowShape4.Height;
            ArrowShape5.Height = (float)End_Pos - ArrowShape5.Top;
            TextShape1.Top = (ArrowShape1.Top + (ArrowShape1.Height / 2)) - (TextShape1.Height / 2);
            TextShape2.Top = (ArrowShape2.Top + (ArrowShape2.Height / 2)) - (TextShape2.Height / 2);
            if (TextShape2.Top < TextShape1.Top + TextShape1.Height) TextShape2.Top = TextShape1.Top + TextShape1.Height;
            TextShape3.Top = (ArrowShape3.Top + (ArrowShape3.Height / 2)) - (TextShape3.Height / 2);
            if (TextShape3.Top < TextShape2.Top + TextShape2.Height) TextShape3.Top = TextShape2.Top + TextShape2.Height;
            TextShape4.Top = (ArrowShape4.Top + (ArrowShape4.Height / 2)) - (TextShape4.Height / 2);
            if (TextShape4.Top < TextShape3.Top + TextShape3.Height) TextShape4.Top = TextShape3.Top + TextShape3.Height;
            TextShape5.Top = (ArrowShape5.Top + (ArrowShape5.Height / 2)) - (TextShape5.Height / 2);
            if (TextShape5.Top < TextShape4.Top + TextShape4.Height) TextShape5.Top = TextShape4.Top + TextShape4.Height;
            Shape_Acceptable.Top = ArrowShape2.Top;
            Shape_Acceptable.Height = ArrowShape5.Top - ArrowShape2.Top;
            Text_Acceptable.Top = (Shape_Acceptable.Top + (Shape_Acceptable.Height / 2)) - (Text_Acceptable.Height / 2);

        }

        private void OutputResult_StockGraph_SetSeries(ChartObject T_ChartObj)
        {
            double T_Left;
            int i = 0;
            WorksheetFunction wf = xlApp.WorksheetFunction;
            T_Left = T_ChartObj.Chart.SeriesCollection(1).Points(1).DataLabel.Left;
            foreach (Series T_Series in T_ChartObj.Chart.SeriesCollection())
            {
                if (i == 0)
                    T_Series.DataLabels().Font.Size = 9;
                else
                    T_Series.DataLabels().Font.Size = 10;
                i++;

                T_Series.DataLabels().Position = XlDataLabelPosition.xlLabelPositionLeft;
                T_Left = wf.Min(T_Left, T_Series.Points(1).DataLabel.Left);
            }
            T_ChartObj.Activate();
            foreach (Series T_Series in T_ChartObj.Chart.SeriesCollection())
            {
                foreach (Series O_Series in T_ChartObj.Chart.SeriesCollection())
                {
                    if (T_Series.Name != O_Series.Name)
                    {
                        if (T_Series.Points(1).DataLabel.Top > O_Series.Points(1).DataLabel.Top)
                        {
                            if (T_Series.Points(1).DataLabel.Top < O_Series.Points(1).DataLabel.Top + 25)
                            {
                                T_Series.Points(1).DataLabel.Top = O_Series.Points(1).DataLabel.Top + 25;
                                T_Series.Points(1).DataLabel.Left = T_Left;
                            }
                        }
                    }
                }
            }
            grpahSheet.Cells[1, 1].Select();
            T_ChartObj.ShapeRange.ZOrder(Microsoft.Office.Core.MsoZOrderCmd.msoSendToBack);
        }

        private long GetDigits(double TopPrice, double EndPrice)
        {
            double DiffValue;
            long i;
            DiffValue = Math.Abs(TopPrice - EndPrice);
            for (i = 1; i <= 100; i++)
            {
                if (DiffValue < 10)
                    break;
                DiffValue = DiffValue / 10;
            }

            return i - 1;
        }

        private void outputGraphSheet()
        {

            Range Percent_Area;
            Series NSeries;
            long T_Col;
            long E_Row;


            if (outputData.NumericalArray.GetUpperBound(1) < 2)
            {
                grpahSheet.Range[CopyFromFormula].Delete(XlDeleteShiftDirection.xlShiftToLeft);
            }
            else
            {
                grpahSheet.Range[CopyFromRange].Copy();
                grpahSheet.Range[grpahSheet.Range[PasteToRangeS], grpahSheet.Range[PasteToRangeE].Offset[ColumnOffset: outputData.NumericalArray.GetUpperBound(1) - 3]].Insert(XlInsertShiftDirection.xlShiftToRight);
                xlApp.CutCopyMode = (XlCutCopyMode)1;
            }
            grpahSheet.Range[CopyFromFormula].Copy();
            grpahSheet.Range[grpahSheet.Range[PasteToFormula], grpahSheet.Range[PasteToFormula].End[XlDirection.xlToRight]].PasteSpecial(Paste: XlPasteType.xlPasteFormulas);
            xlApp.CutCopyMode = (XlCutCopyMode)1;


            ChartObject T_ChartObj = grpahSheet.ChartObjects("PSM_Graph");
            T_ChartObj.Activate();
            for (int i = 0; i < 4; i++)
            {

                T_Col = (i + 1) * 2;
                E_Row = OR_PercentTitle + outputData.psmDataPercentage[i].GetUpperBound(0) + 1;
                NSeries = T_ChartObj.Chart.SeriesCollection().NewSeries;
                NSeries.XValues = "=Data!R" + OR_PercentData + "C" + T_Col + ":R" + E_Row + "C" + T_Col;
                NSeries.Values = "=Data!R" + OR_PercentData + "C" + (T_Col + 1) + ":R" + E_Row + "C" + (T_Col + 1);
                NSeries.Name = "=Data!R" + OR_PercentTitle + "C" + (T_Col + 1);
            }
            Chart WithchtObjChart = T_ChartObj.Chart;
            var fsize = WithchtObjChart.Legend.Font.Size;
            WithchtObjChart.ChartArea.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            WithchtObjChart.Axes(XlAxisType.xlCategory).MaximumScale = pSMSettings.maxPrice;
            WithchtObjChart.Axes(XlAxisType.xlCategory).MinimumScale = pSMSettings.minPrice;
            WithchtObjChart.Axes(XlAxisType.xlCategory).MajorUnit = pSMSettings.scaleInterval;
            grpahSheet.Cells[1, 1].Select();
            Percent_Area = grpahSheet.Range["PercentArea"];
            OutputResult_ChartObject(T_ChartObj, Percent_Area);
            OutputResult_PlotArea(T_ChartObj, Percent_Area);
            OutputResult_GraphColors(T_ChartObj);
            T_ChartObj.Chart.Legend.Position = XlLegendPosition.xlLegendPositionLeft;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
                WithchtObjChart.ChartArea.Font.Size = 11;
            T_ChartObj.Chart.Legend.Font.Size = fsize;
            grpahSheet.Activate();
            grpahSheet.Cells[1, 1].Select();
            OutputResult_PlotArea(T_ChartObj, Percent_Area);
        }

        private void OutputResult_GraphColors(ChartObject T_ChartObj)
        {
            Microsoft.Office.Core.MsoGradientStyle Style_G;
            int TargetType = 0;
            for (int i = 0; i < 4; i++)
            {

                switch (i)
                {
                    case 0:
                        {
                            TargetType = 9737946;//todo color implimenatation
                            break;
                        }
                    case 1:
                        {
                            TargetType = 14136213;
                            break;
                        }

                    case 2:
                        {
                            TargetType = 5066944;
                            break;
                        }
                    case 3:
                        {
                            TargetType = 12419407;
                            break;
                        }
                }

                Series sc = T_ChartObj.Chart.SeriesCollection(i + 1);
                
                sc.Border.Color = TargetType;
                sc.MarkerStyle = XlMarkerStyle.xlMarkerStyleNone;
                try
                {
                    sc.Format.Line.Weight = 2.25f;
                }
                catch (Exception) { }
            }
            T_ChartObj.Chart.PlotArea.Interior.Pattern = XlPattern.xlPatternSolid;
            T_ChartObj.Chart.PlotArea.Interior.Color = 16777215;//todo color implimenatation
            
        }

        private void OutputResult_PlotArea(ChartObject T_ChartObj, Range Percent_Area)
        {
            Double PlotAreaLeft;
            Double EachColWidth;
            PlotAreaLeft = Percent_Area.Left - T_ChartObj.Left;
            EachColWidth = Percent_Area.Width / (outputData.NumericalArray.GetUpperBound(1) + 1) + 0.25;
            T_ChartObj.Activate();
            Chart WithchtObjChart = T_ChartObj.Chart;
            WithchtObjChart.PlotArea.Select();
            xlApp.Selection.Width = EachColWidth * (outputData.NumericalArray.GetUpperBound(1) + 1);
            xlApp.Selection.Left = PlotAreaLeft - 2.5;
            xlApp.Selection.Height = xlApp.Selection.Height - 20;
        }

        private void OutputResult_ChartObject(ChartObject T_ChartObj, Range Percent_Area)
        {
            Range LeftArea;
            Range RightArea;
            LeftArea = Percent_Area.Cells[1, 1].Offset[ColumnOffset: -1];
            RightArea = Percent_Area.Cells[Percent_Area.Cells.Count].Offset[ColumnOffset: 1];
            T_ChartObj.Left = LeftArea.Left;
            T_ChartObj.Width = grpahSheet.Range[LeftArea, RightArea].Width;
        }

        private void outputDataSheet()
        {
            dataSheet.Cells[OR_Cnt, OutCol].Value = outputData.dataCount;
            dataSheet.Cells[OR_Valid, OutCol].Value = outputData.validCount;

            double[,] res = transpose(outputData.ResultPrices);
            Range WithshtResize = dataSheet.Cells[OR_CalcPrice, OutCol].Resize[res.GetUpperBound(0) + 1, res.GetUpperBound(1) + 1];
            WithshtResize.Cells.Value = res;
            WithshtResize = dataSheet.Cells[OR_Numerical, OutCol].Resize[outputData.NumericalArray.GetUpperBound(0) + 1, outputData.NumericalArray.GetUpperBound(1) + 1];
            WithshtResize.Cells.Value = outputData.NumericalArray;

            Range DS_Cell = dataSheet.Cells[OR_PercentTitle, OutCol];

            for (int i = 0; i < 4; i++)
            {
                string header = "";
                switch (i)
                {
                    case 0:
                        {
                            header = LocalResource.PSM_EXPENSIVE;
                            break;
                        }
                    case 1:
                        {
                            header = LocalResource.PSM_CHEAP;
                            break;
                        }

                    case 2:
                        {
                            header = LocalResource.PSM_TOO_EXPENSIEVE;
                            break;
                        }
                    case 3:
                        {
                            header = LocalResource.PSM_TOO_CHEAP;
                            break;
                        }
                }
                DS_Cell = dataSheet.Cells[OR_PercentTitle, OutCol + i * 2];
                DS_Cell.Value = header;
                double[,] res1 = transpose(outputData.psmData[i]);
                double[,] res2 = transpose(outputData.psmDataPercentage[i]);
                WithshtResize = dataSheet.Cells[OR_PercentTitle + 1, (OutCol - 1) + i * 2].Resize[res1.GetUpperBound(0) + 1, res1.GetUpperBound(1) + 1];
                WithshtResize.Cells.Value = res1;
                WithshtResize = dataSheet.Cells[OR_PercentTitle + 1, OutCol + i * 2].Resize[res2.GetUpperBound(0) + 1, res2.GetUpperBound(1) + 1];
                WithshtResize.Cells.Value = res2;
            }
            if (pSMSettings.invertHighAndCheap)
            {
                for (int i = 0; i < 2; i++)
                {
                    string header = "";
                    string header2 = "";
                    switch (i)
                    {
                        case 0:
                            {
                                header = LocalResource.PSM_EXPENSIVE;
                                header2 = LocalResource.PSM_NOT_CHEAP;
                                break;
                            }
                        case 1:
                            {
                                header = LocalResource.PSM_CHEAP;
                                header2 = LocalResource.PSM_NOT_HIGH;
                                break;
                            }

                    }
                    DS_Cell = dataSheet.Cells[OR_PercentTitle, OutCol + i * 2];
                    DS_Cell.Value = header2;
                    DS_Cell = dataSheet.Cells[OR_PercentTitle, OutCol + (i + 4) * 2];
                    DS_Cell.Value = header;
                    double[,] res2 = transpose(outputData.psmDataPercentageBefore[i]);
                    WithshtResize = dataSheet.Cells[OR_PercentTitle + 1, OutCol + (i + 4) * 2].Resize[res2.GetUpperBound(0) + 1, res2.GetUpperBound(1) + 1];
                    WithshtResize.Cells.Value = res2;
                    WithshtResize = dataSheet.Cells[OR_PercentTitle, OutCol + (i + 4) * 2].Resize[res2.GetUpperBound(0) + 2, res2.GetUpperBound(1) + 1];
                  
                    WithshtResize.BorderAround2(LineStyle: XlLineStyle.xlContinuous, Weight: XlBorderWeight.xlHairline, ColorIndex: XlColorIndex.xlColorIndexAutomatic);

                }
            }
            WithshtResize = dataSheet.Cells[OR_Satistics, OutCol].Resize[outputData.statasticData.GetUpperBound(1) + 1, outputData.statasticData.GetUpperBound(0) + 1];
            object[,] res3 = transpose(outputData.statasticData);
            WithshtResize.Cells.Value = res3;
        }

        private double[,] transpose(double[] resultPrices)
        {
            double[,] transpose = new double[resultPrices.Length, 1];
            for (int i = 0; i < resultPrices.Length; i++)
            {
                transpose[i, 0] = resultPrices[i];
            }
            return transpose;
        }

        private object[,] transpose(double?[,] resultPrices)
        {
            object[,] transpose = new object[resultPrices.GetUpperBound(1) + 1, resultPrices.GetUpperBound(0) + 1];
            for (int i = 0; i <= resultPrices.GetUpperBound(1); i++)
                for (int j = 0; j <= resultPrices.GetUpperBound(0); j++)
                {
                    double val = Convert.ToDouble(resultPrices[j, i]);
                    if (double.IsNaN(val) || double.IsInfinity(val))
                    {
                        transpose[i, j] = ErrorStr;
                    }
                    else
                    {
                        transpose[i, j] = val;
                    }
                }
            return transpose;
        }
    }
}