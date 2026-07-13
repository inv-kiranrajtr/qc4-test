using log4net;
using Microsoft.Office.Interop.Excel;
using QC4Common.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QC4Common.Common
{
    public class GTAutoSetting
    {
        public static Range CheckDataInQS(Worksheet sht)
        {
            Range targetCell = sht.Cells[Constants.GT.GtRowDataStart, Constants.GT.GtColChartType];
            Range lastCell = ExcelUtil.EndxlUp(targetCell);
            return lastCell;
        }
        public static object[,] GetDefAry(Worksheet gtSheet)
        {
            Range targetCell = gtSheet.Cells[Constants.GT.GtRowDataStart, Constants.GT.GtColChartType];
            Range lastDataCell = ExcelUtil.EndxlUp(targetCell);
            Range Search_Range = null;
            if (lastDataCell.Row >= Constants.GT.GtRowDataStart)
            {
                Search_Range = gtSheet.Range[gtSheet.Cells[Constants.GT.GtRowDataStart, 1], gtSheet.Cells[lastDataCell.Row, Constants.GT.GtColItem + Constants.GT.GtMaxItemNo - 1]];
                return Search_Range.Value2;
            }
            return null;
        }

        public static string GetGraphList(string chartType)
        {
            string sep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            string graphList = null;
            switch (chartType)
            {
                case Constants.GT.GTMTS:
                    return CommonResource.GTMTSGraph.Replace(",",sep);
                case Constants.GT.GTMTM:
                    return CommonResource.GTMTMGraph.Replace(",", sep);
                case Constants.GT.GTRAT:
                    return CommonResource.GTRATGraph.Replace(",", sep);
                case Constants.GT.GTRNK:
                    return CommonResource.GTRNKGraph.Replace(",", sep);
                case Constants.GT.GTSA:
                    return CommonResource.GTSAGraph.Replace(",", sep);
                case Constants.GT.GTMA:
                    return CommonResource.GTMAGraph.Replace(",", sep);
                default:
                    return graphList;
            }
        }
        static int GtRowCount = 0;
        public static void LoadDefaultDataToGTHiddenSheet(Worksheet gtSheet)
        {
            Worksheet GtHiddenSheet = ExcelUtil.GetWorkSheetByCodeName(gtSheet.Application.ActiveWorkbook, Constants.SheetCodeName.GTTabulationS);
            GtHiddenSheet.Cells[Constants.GT.GtRowDataStart, 1].Resize[GtRowCount + 1, 206].Value = gtSheet.Cells[Constants.GT.GtRowDataStart, 1].Resize[GtRowCount + 1, 206].Value;
        }
        public static void LoadDefaultDataToGTHiddenSheet(Workbook _workbook)
        {
            string sep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            Worksheet GtHiddenSheet = ExcelUtil.GetWorkSheetByCodeName(_workbook, Constants.SheetCodeName.GTTabulationS);
            Range gtStart = GtHiddenSheet.Cells[Constants.GT.GtRowDataStart, Constants.GT.GtColChartType];
            Range gtEnd = ExcelUtil.EndxlUp(gtStart);
            Range gtData = GtHiddenSheet.get_Range(gtStart, gtEnd);
            int hdValLen = 1;
            object[,] gtDataAry = null;
            if (gtData.Cells.Count > 0)
            {
                gtDataAry = GtHiddenSheet.Cells[Constants.GT.GtRowDataStart, 1].Resize[gtData.Value is string?1: gtData.Value.GetLength(0), 206].Value;
                hdValLen = gtDataAry.GetLength(0);
            }

            Worksheet qsSheet = ExcelUtil.GetWorkSheetByCodeName(_workbook, Constants.SheetCodeName.QuestionSetting);
            Range qsStart = qsSheet.Cells[Constants.QS.QsRowDataStart, Constants.QS.QsColStart];
            Range qsEnd = ExcelUtil.EndxlUp(qsStart);
            Range qsTotal = qsSheet.get_Range(qsStart.Offset[0, -4], qsEnd.Offset[0, 10]);
            Object[,] objAry = qsTotal.Value2;
            int len = objAry.GetLength(0);
            object[,] myAry = new object[len, 206];
            int row = 0;
            Dictionary<string, string> tableType = GetTableType();
            for (int i = 0; i < len; i++)
            {
                if ((objAry[i + 1, 5] != null && objAry[i + 1, 5].ToString() == "SAMPLEID") || (objAry[i + 1, 6] != null && objAry[i + 1, 6].ToString() == "D"))
                    continue;
                string qsCount = objAry[i + 1, 4] == null ? null : objAry[i + 1, 4].ToString() == "" ? null : objAry[i + 1, 4].ToString();
                int? c = Convert.ToInt32(qsCount);
                int col = 6;
                string tbType = "";
                if (objAry[i + 1, 3] == null || objAry[i + 1, 3].ToString() == "")
                {
                    tableType.TryGetValue(objAry[i + 1, 6].ToString(), out tbType);
                    if (tbType == null || tbType == "")
                    {
                        i = i + (c == null || c == 0 ? 0 : ((int)c) - 1);
                        continue;
                    }
                }
                else
                {
                    tableType.TryGetValue(objAry[i + 1, 3].ToString(), out tbType);
                    if (objAry[i + 1, 3].ToString() == "FAS" && (objAry[i + 1, 6].ToString() == "N" || objAry[i + 1, 6].ToString() == "FA"))
                    {
                        objAry[i + 1, 3] = "FASN";
                        tbType = c > 1 ? "GT-MTN" : "GT-N";
                    }
                    if (tbType == null || tbType == "")
                    {
                        i = i + (c == null || c == 0 ? 0 : ((int)c) - 1);
                        continue;
                    }
                }
                if (c != null && c > 0)
                {
                    myAry[row, 0] = CommonResource.CELL_ON;//"○";//"On";
                    myAry[row, 5] = "1";
                    myAry[row, 2] = "";
                    myAry[row, 4] = "";
                    myAry[row, 1] = tbType;
                    string graphType = GetGraphList(myAry[row, 1].ToString());
                    myAry[row, 3] = graphType != null && graphType.Length > 0 ? graphType.Split(char.Parse(sep))[0] : "";
                    for (int j = 0; j < c; j++)
                    {
                        if (objAry[i + 1, 6].ToString() != "FA")
                        {
                            myAry[row, col] = objAry[i + 1, 5];
                            col++;
                        }
                        i++;
                    }
                    i--;
                    if (col == 6)
                    {
                        row--;
                    }
                }
                else
                {
                    if (objAry[i + 1, 6].ToString() != "FA")
                    {
                        myAry[row, 2] = "";
                        myAry[row, 4] = "";
                        myAry[row, 0] = CommonResource.CELL_ON;//"×";//"On";
                        myAry[row, 1] = tbType;
                        string graphType = "";
                        myAry[row, 5] = "1";
                        graphType = GetGraphList(myAry[row, 1].ToString());
                        myAry[row, 3] = graphType != null && graphType.Length > 0 ? graphType.Split(char.Parse(sep))[0] : "";
                        myAry[row, col] = objAry[i + 1, 5];
                    }
                    else
                    {
                        row--;
                    }
                }
                row++;
            }
            int leng = row;
            if (hdValLen > leng)
                leng = hdValLen;
            else
            {
                for (int i = 0; i < gtDataAry.GetLength(0); i++)
                {
                    if (gtDataAry[i + 1, 1] != null && (gtDataAry[i + 1, 1].ToString() == "" || gtDataAry[i + 1, 1].ToString() == "0"))
                    {
                        leng++;
                    }
                }
            }
            Object[,] val = new object[leng, 206];
            for (int i = 0; i < row; i++)
            {
                val[i, 0] = myAry[i, 5];
                val[i, 1] = myAry[i, 0];
                val[i, 2] = myAry[i, 1];
                val[i, 3] = myAry[i, 2];
                val[i, 4] = myAry[i, 3];
                val[i, 5] = myAry[i, 4];
                for (int j = Constants.GT.GtColItem; j <= 206; j++)
                {
                    if (myAry[i, j - 1] == null || myAry[i, j - 1].ToString() == "")
                        break;
                    val[i, (j - 1)] = myAry[i, j - 1];
                }
            }
            //for (int i = 0; i < gtDataAry.GetLength(0); i++)
            //{
            //    if (gtDataAry[i + 1, 1] != null && (gtDataAry[i + 1, 1].ToString() == ""|| gtDataAry[i + 1, 1].ToString() == "0"))
            //    {
            //        val[row, 0] = gtDataAry[i + 1, 1];
            //        val[row, 1] = gtDataAry[i + 1, 2];
            //        val[row, 2] = gtDataAry[i + 1, 3];
            //        val[row, 3] = gtDataAry[i + 1, 4];
            //        val[row, 4] = gtDataAry[i + 1, 5];
            //        val[row, 5] = gtDataAry[i + 1, 6];
            //        for (int j = Constants.GT.GtColItem; j < 206; j++)
            //        {
            //            if (gtDataAry[i + 1, j] == null || gtDataAry[i + 1, j].ToString() == "")
            //                break;
            //            val[row, (j - 1)] = gtDataAry[i+1, j];
            //        }
            //        row++;
            //    }
            //}
            bool EEFlg = GtHiddenSheet.Application.EnableEvents;
            GtHiddenSheet.Application.EnableEvents = false;
            GtHiddenSheet.Cells[Constants.GT.GtRowDataStart, 1].Resize[val.GetLength(0), 206].Value = val;
            GtHiddenSheet.Application.EnableEvents = EEFlg;
        }
        public class VariableDT
        {
            public string Variable { get; set; }
            public string Type { get; set; }
        }
        public static void LoadNewDataToGTHiddenSheet(Workbook _workbook,List<VariableDT> variables)
        {
            string sep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            int hdValLen = 0;
            object[,] gtDataAry = null;
            Worksheet GtHiddenSheet = ExcelUtil.GetWorkSheetByCodeName(_workbook, Constants.SheetCodeName.GTTabulationS);
            if (GtHiddenSheet.Range["E6"].Value != null)
            {
                Range gtStart = GtHiddenSheet.Cells[Constants.GT.GtRowDataStart, Constants.GT.GtColChartType];
                Range gtEnd = ExcelUtil.EndxlUp(gtStart);
                Range gtData = GtHiddenSheet.get_Range(gtStart, gtEnd);
                if (gtData.Cells.Count > 0)
                {
                    gtDataAry = GtHiddenSheet.Cells[Constants.GT.GtRowDataStart, 1].Resize[gtData.Value is string ? 1 : gtData.Value.GetLength(0), 206].Value;
                    hdValLen = gtDataAry.GetLength(0);
                }
            }
            int cou = variables.Count;
            object[,] val = new object[cou, 206];
            Dictionary<string, string> tableType = GetTableType();
            int row = 0;
            for (int i=0;i< cou; i++)
            {
                if (variables[i].Variable == "SAMPLEID" || variables[i].Type=="D"|| variables[i].Type=="FA")
                    continue;
                string tblType = tableType[variables[i].Type];
                val[row, 0] = "1";
                val[row, 1] = CommonResource.CELL_ON;
                val[row, 2] = tblType;
                val[row, 3] = "";
                string graphType = GetGraphList(tblType);
                val[row, 4] = graphType != null && graphType.Length > 0 ? graphType.Split(char.Parse(sep))[0] : "";
                val[row, 5] = "";
                val[row, 6] = variables[i].Variable;
                row++;
            }

            bool EEFlg = GtHiddenSheet.Application.EnableEvents;
            GtHiddenSheet.Application.EnableEvents = false;
            GtHiddenSheet.Cells[hdValLen + Constants.GT.GtRowDataStart, 1].Resize[val.GetLength(0), 206].Value = val;
            GtHiddenSheet.Application.EnableEvents = EEFlg;
        }

        private static Dictionary<string, string> GetTableType()
        {
            Dictionary<string, string> tableType = new Dictionary<string, string>()
            {
                {"SA","GT-SA"},
                {"MA","GT-MA"},
                {"N","GT-N"},
                {"FA",""},
                {"SAR","GT-SA"},
                {"SAS","GT-SA"},
                {"SAP","GT-SA"},
                {"MAC","GT-MA"},
                {"MTS","GT-MTS"},
                {"MTM","GT-MTM"},
                {"MTT","GT-MTS"},
                {"RAT","GT-RAT"},
                {"RNK","GT-RNK"},
                {"FAS",""},
                {"FASN","GT-MTN"},
                {"NUM","GT-N"},
                {"FAL",""}
            };
            return tableType;
        }

        public static void FNCGetQuesData(Worksheet gtSheet, object[,] defAry, Worksheet _qsSheet)
        {
            string sep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            Range targetCell = gtSheet.Cells[Constants.GT.GtRowDataStart, Constants.GT.GtColChartType];
            Range gtE = ExcelUtil.EndxlUp(targetCell);
            Worksheet qsSheet = _qsSheet;
            Range qsStart = qsSheet.Cells[Constants.QS.QsRowDataStart, Constants.QS.QsColStart];
            Range qsEnd = ExcelUtil.EndxlUp(qsStart);
            Range qsTotal = qsSheet.get_Range(qsStart.Offset[0, -4], qsEnd.Offset[0, 10]);
            Object[,] objAry = qsTotal.Value2;
            int len = objAry.GetLength(0);
            object[,] myAry = new object[len, 206];
            int row = 0;
            Dictionary<string, string> tableType = GetTableType();
            for (int i = 0; i < len; i++)
            {
                if ((objAry[i + 1, 5] != null && objAry[i + 1, 5].ToString() == "SAMPLEID") || (objAry[i + 1, 6] != null && objAry[i + 1, 6].ToString() == "D"))
                    continue;
                string qsCount = objAry[i + 1, 4] == null ? null : objAry[i + 1, 4].ToString() == "" ? null : objAry[i + 1, 4].ToString();
                int? c = Convert.ToInt32(qsCount);
                int col = 6;
                string tbType = "";
                if (objAry[i + 1, 3] == null || objAry[i + 1, 3].ToString() == "")
                {
                    tableType.TryGetValue(objAry[i + 1, 6].ToString(), out tbType);
                    if (tbType == null || tbType == "")
                    {
                        i = i + (c == null || c == 0 ? 0 : ((int)c) - 1);
                        continue;
                    }
                }
                else
                {
                    tableType.TryGetValue(objAry[i + 1, 3].ToString(), out tbType);
                    if (objAry[i + 1, 3].ToString() == "FAS" && (objAry[i + 1, 6].ToString() == "N" || objAry[i + 1, 6].ToString() == "FA"))
                    {
                        objAry[i + 1, 3] = "FASN";
                        tbType = c > 1 ? "GT-MTN" : "GT-N";
                    }
                    if (tbType == null || tbType == "")
                    {
                        i = i + (c == null || c == 0 ? 0 : ((int)c) - 1);
                        continue;
                    }
                }
                if (c != null && c > 0)
                {
                    myAry[row, 0] = CommonResource.CELL_ON;//"○";//"On";
                    myAry[row, 5] = "1";
                    myAry[row, 2] = "";
                    myAry[row, 4] = "";
                    myAry[row, 1] = tbType;//"GT-" + objAry[i + 1, 1];
                    string graphType = GetGraphList(myAry[row, 1].ToString());
                    myAry[row, 3] = graphType != null && graphType.Length > 0 ? graphType.Split(char.Parse(sep))[0] : "";
                    // myAry[row, col] = objAry[i + 1, 5];
                    for (int j = 0; j < c; j++)
                    {
                        if (objAry[i + 1, 6].ToString() != "FA")
                        {
                            myAry[row, col] = objAry[i + 1, 5];
                            col++;
                        }
                        i++;
                    }
                    i--;
                    if (col == 6) {
                        row--;
                    }
                }
                else
                {
                    if (objAry[i + 1, 6].ToString() != "FA")
                    {
                        myAry[row, 2] = "";
                        myAry[row, 4] = "";
                        myAry[row, 0] = CommonResource.CELL_ON;//"×";//"On";
                        myAry[row, 1] = tbType;
                        string graphType = "";
                        myAry[row, 5] = "1";
                        graphType = GetGraphList(myAry[row, 1].ToString());
                        myAry[row, 3] = graphType != null && graphType.Length > 0 ? graphType.Split(char.Parse(sep))[0] : "";//myAry[row, 1].ToString() == "GT-N" || myAry[row, 1].ToString() == "GT-MTN" ? "" : "QC Pie Chart";
                        myAry[row, col] = objAry[i + 1, 5];
                    }
                    else
                    {
                        row--;
                    }
                }
                row++;
            }
            int outRow = Constants.GT.GtRowDataStart;
            int extraRow = 0;
            Object[,] dfAry = new object[extraRow, 206];
            int defAryLen = defAry == null ? 0 : defAry.GetLength(0);
            for (int i = 1; i <= defAryLen; i++)
            {
                if ((defAry[i, 1] == null || defAry[i, 1].ToString() == "") && (defAry[i, 3] != null && defAry[i, 3].ToString() != ""))
                {
                    extraRow++; row++;
                    dfAry = ResizeArray(dfAry, extraRow , 205); 
                    dfAry[extraRow - 1, 0] = CommonResource.CELL_ON;//defAry[i, 2];
                    dfAry[extraRow - 1, 1] = defAry[i, 3];
                    dfAry[extraRow - 1, 2] = defAry[i, 4];
                    dfAry[extraRow - 1, 3] = defAry[i, 5];
                    dfAry[extraRow - 1, 4] = defAry[i, 6];
                    //dfAry[extraRow - 1, 5] = defAry[i, 7];
                    //int k = 4;
                    for (int j = Constants.GT.GtColItem; j <= defAry.GetLength(1); j++)
                    {
                        if (defAry[i, j] == null || defAry[i, j].ToString() == "")
                            break;
                        dfAry[extraRow - 1, j - 2] = defAry[i, j];
                    }
                }
            }
            int defStart = 0;
            Object[,] val = new object[row, 3];
            Object[,] val2 = new object[row, 203];
            for (int i = 0; i < row; i++, outRow++)
            {
                if (i < (row - extraRow))
                {
                    val[i, 1 - 1] = myAry[i, 5];
                    val[i, Constants.GT.GtColExec - 1] = myAry[i, 0];
                    val[i, Constants.GT.GtColChartType - 1] = myAry[i, 1];
                    val2[i, Constants.GT.GtColTest - 4] = myAry[i, 2];
                    val2[i, Constants.GT.GtColGraphType - 4] = myAry[i, 3];
                    val2[i, Constants.GT.GtColTableHeading - 4] = myAry[i, 4];
                    for (int j = Constants.GT.GtColItem; j <= 206; j++)
                    {
                        if (myAry[i, j - 1] == null || myAry[i, j - 1].ToString() == "")
                            break;
                        val2[i, j - 4] = myAry[i, j - 1];
                    }
                }
                else
                {
                    val[i, Constants.GT.GtColExec - 1] = dfAry[defStart, 0];
                    val[i, Constants.GT.GtColChartType - 1] = dfAry[defStart, 1];
                    val2[i, Constants.GT.GtColTest - 4] = dfAry[defStart, 2];
                    val2[i, Constants.GT.GtColGraphType - 4] = dfAry[defStart, 3];
                    val2[i, Constants.GT.GtColTableHeading - 4] = dfAry[defStart, 4];
                    for (int j = Constants.GT.GtColItem; j <= 206; j++)
                    {
                        if (dfAry[defStart, j - 2] == null || dfAry[defStart, j - 2].ToString() == "")
                            break;
                        val2[i, j - 4] = dfAry[defStart, j - 2];
                    }
                    defStart++;
                }
            }
            Range GtRange = gtSheet.Cells[Constants.GT.GtRowDataStart, 1].Resize[val.GetUpperBound(0) + 1 , 3];
            GtRange.Value = val;

            outRow = Constants.GT.GtRowDataStart;
            bool EEFlg = gtSheet.Application.EnableEvents;
            gtSheet.Application.EnableEvents = false;
            Range range =  gtSheet.Cells[outRow, Constants.GT.GtColChartType].Resize[row] ;
            FNCCellFormatInitializeGt(range);
            NewChangeChart(row, Constants.GT.GtRowDataStart, gtSheet);
            GtRange = gtSheet.Cells[Constants.GT.GtRowDataStart, 4].Resize[val.GetUpperBound(0) + 1, 203];
            GtRange.Value = val2;
            GtRowCount = val.GetUpperBound(0);
            gtSheet.Application.EnableEvents = EEFlg;
        }

        private static void NewChangeChart(int row, int outRow, Worksheet gtSheet)
        {
            Range saRange = null;
            Range maRange = null;
            Range nRange = null;
            Range ratRange = null;
            Range mtnRange = null;
            Range rnkRange = null;
            Range mtsRange = null;
            Range mtmRange = null;

            Range saRangeItem = null;
            Range maRangeItem = null;
            Range nRangeItem = null;
            Range ratRangeItem = null;
            Range mtnRangeItem = null;
            Range rnkRangeItem = null;
            Range mtsRangeItem = null;
            Range mtmRangeItem = null;

            for (int i = 0; i < row; i++, outRow++)
            {
                Range tCell = gtSheet.Cells[outRow, Constants.GT.GtColChartType];

                Range startCell = gtSheet.Cells[tCell.Row, Constants.GT.GtColItem];
                Range endCell;
                Range itemArea;
                Application xlApp = gtSheet.Application;

                string chartType = tCell.Value2;
                switch (chartType)
                {
                    case Constants.GT.GTSA:
                        saRange = saRange == null ? tCell : xlApp.Union(saRange, tCell);
                        saRangeItem = saRangeItem == null ? startCell : xlApp.Union(saRangeItem, startCell);
                        break;
                    case Constants.GT.GTMA:
                        maRange = maRange == null ? tCell : xlApp.Union(maRange, tCell);
                        maRangeItem = maRangeItem == null ? startCell : xlApp.Union(maRangeItem, startCell);
                        break;
                    case Constants.GT.GTN:
                        nRange = nRange == null ? tCell : xlApp.Union(nRange, tCell);
                        nRangeItem = nRangeItem == null ? startCell : xlApp.Union(nRangeItem, startCell);
                        break;
                    case Constants.GT.GTRAT:
                        endCell = gtSheet.Cells[tCell.Row, Constants.GT.GtColItem + Constants.GT.GtMaxItemNo - 1];
                        itemArea = gtSheet.get_Range(startCell, endCell);
                        ratRange = ratRange == null ? tCell : xlApp.Union(ratRange, tCell);
                        ratRangeItem = ratRangeItem == null ? itemArea : xlApp.Union(ratRangeItem, itemArea);
                        break;
                    case Constants.GT.GTMTN:
                        endCell = gtSheet.Cells[tCell.Row, Constants.GT.GtColItem + Constants.GT.GtMaxItemNo - 1];
                        itemArea = gtSheet.get_Range(startCell, endCell);
                        mtnRange = mtnRange == null ? tCell : xlApp.Union(mtnRange, tCell);
                        mtnRangeItem = mtnRangeItem == null ? itemArea : xlApp.Union(mtnRangeItem, itemArea);
                        break;
                    case Constants.GT.GTRNK:
                        endCell = gtSheet.Cells[tCell.Row, Constants.GT.GtColItem + Constants.GT.GtMaxItemNo - 1];
                        itemArea = gtSheet.get_Range(startCell, endCell);
                        rnkRangeItem = rnkRangeItem == null ? itemArea : xlApp.Union(rnkRangeItem, itemArea);
                        rnkRange = rnkRange == null ? tCell : xlApp.Union(rnkRange, tCell);
                        break;
                    case Constants.GT.GTMTS:
                        endCell = gtSheet.Cells[tCell.Row, Constants.GT.GtColItem + Constants.GT.GtMaxItemNo - 1];
                        itemArea = gtSheet.get_Range(startCell, endCell);
                        mtsRangeItem = mtsRangeItem == null ? itemArea : xlApp.Union(mtsRangeItem, itemArea);
                        mtsRange = mtsRange == null ? tCell : xlApp.Union(mtsRange, tCell);
                        break;
                    case Constants.GT.GTMTM:
                        endCell = gtSheet.Cells[tCell.Row, Constants.GT.GtColItem + Constants.GT.GtMaxItemNo - 1];
                        itemArea = gtSheet.get_Range(startCell, endCell);
                        mtmRangeItem = mtmRangeItem == null ? itemArea : xlApp.Union(mtmRangeItem, itemArea);
                        mtmRange = mtmRange == null ? tCell : xlApp.Union(mtmRange, tCell);
                        break;
                }
                //FNCCheckExecEnable(tCell.Worksheet, tCell, false);

            }
            string  listName;
            Range hyodaiCell;
            Range graphCell;
            string sep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;

            if (saRange != null){
                FNCCellFormatSetting(saRange.Offset[0, 1], "1", "", 19);
                graphCell = saRange.Offset[0, 2];
                FNCCellFormatSetting(graphCell, GetGraphList(Constants.GT.GTSA), "", 19);
                graphCell.Validation.ShowInput = false;
                listName = Constants.EqEqual + Constants.VariableList.ListItemSA;
                FNCCellFormatSetting(saRangeItem, listName, "", 19);
            }

            if(maRange != null){
                FNCCellFormatSetting(maRange.Offset[0, 1], "1", "", 19);
                graphCell = maRange.Offset[0, 2];
                FNCCellFormatSetting(graphCell, GetGraphList(Constants.GT.GTMA), "", 19);
                graphCell.Validation.ShowInput = false;
                listName = Constants.EqEqual + Constants.VariableList.ListItemMA;
                FNCCellFormatSetting(maRangeItem, listName, "", 19); 
            }
            
            if(nRange != null){
                graphCell = nRange.Offset[0, 2];
                FNCCellFormatSetting(graphCell, "", "", 19);
                graphCell.Validation.ShowInput = false;
                listName = Constants.EqEqual + Constants.VariableList.ListItemN;
                FNCCellFormatSetting(nRangeItem, listName, "", 19); 
            }

            if(ratRange != null){
                FNCCellFormatSetting(ratRange.Offset[0, 1], "2", "", 19);
                graphCell = ratRange.Offset[0, 2];
                FNCCellFormatSetting(graphCell, GetGraphList(Constants.GT.GTRAT), "", 19);
                graphCell.Validation.ShowInput = false;
                hyodaiCell = ratRange.Offset[0, 3];
                FNCCellFormatSetting(hyodaiCell, "", "", 19);
                hyodaiCell.Validation.ShowInput = false;
                listName = Constants.EqEqual + Constants.VariableList.ListItemN;
                FNCCellFormatSetting(ratRangeItem, listName, "", 19); 
            }

            if(mtnRange != null){
                FNCCellFormatSetting(mtnRange.Offset[0, 1], "2", "", 19);
                hyodaiCell = mtnRange.Offset[0, 3];
                FNCCellFormatSetting(hyodaiCell, "", "", 19);
                hyodaiCell.Validation.ShowInput = false;
                listName = Constants.EqEqual + Constants.VariableList.ListItemN;
                FNCCellFormatSetting(mtnRangeItem, listName, "", 19); 
            }

            if(rnkRange != null){
                FNCCellFormatSetting(rnkRange.Offset[0, 1], "1"+sep+"2", "", 19);
                graphCell = rnkRange.Offset[0, 2];
                FNCCellFormatSetting(graphCell, GetGraphList(Constants.GT.GTRNK), "", 19);
                graphCell.Validation.ShowInput = false;
                hyodaiCell = rnkRange.Offset[0, 3];
                FNCCellFormatSetting(hyodaiCell, "", "", 19);
                hyodaiCell.Validation.ShowInput = false;
                listName = Constants.EqEqual + Constants.VariableList.ListItemSA;
                FNCCellFormatSetting(rnkRangeItem, listName, "", 19); 
            }

            if(mtsRange != null){
                FNCCellFormatSetting(mtsRange.Offset[0, 1], "1"+sep+"2", "", 19);
                graphCell = mtsRange.Offset[0, 2];
                FNCCellFormatSetting(graphCell, GetGraphList(Constants.GT.GTMTS), "", 19);
                graphCell.Validation.ShowInput = false;
                hyodaiCell = mtsRange.Offset[0, 3];
                FNCCellFormatSetting(hyodaiCell, "", "", 19);
                hyodaiCell.Validation.ShowInput = false;
                listName = Constants.EqEqual + Constants.VariableList.ListItemSA;
                FNCCellFormatSetting(mtsRangeItem, listName, "", 19); 
            }

            if(mtmRange != null){
                FNCCellFormatSetting(mtmRange.Offset[0, 1], "1"+sep+"2", "", 19);
                graphCell = mtmRange.Offset[0, 2];
                FNCCellFormatSetting(graphCell, GetGraphList(Constants.GT.GTMTM), "", 19);
                graphCell.Validation.ShowInput = false;
                hyodaiCell = mtmRange.Offset[0, 3];
                FNCCellFormatSetting(hyodaiCell, "", "", 19);
                hyodaiCell.Validation.ShowInput = false;
                listName = Constants.EqEqual + Constants.VariableList.ListItemSAMA;
                FNCCellFormatSetting(mtmRangeItem, listName, "", 19); 
            }
        }

        public static void ExcelReset(Application application)
        {
            if (FNSVisibleBookCount(application) > 0)
                if (application.Calculation != XlCalculation.xlCalculationManual)
                    application.Calculation = XlCalculation.xlCalculationManual;

            if (application.EnableEvents != true)
                application.EnableEvents = true;

            if (application.ScreenUpdating != true)
                application.ScreenUpdating = true;

            if (application.EnableCancelKey != XlEnableCancelKey.xlInterrupt)
                application.EnableCancelKey = XlEnableCancelKey.xlInterrupt;

            if (application.DisplayAlerts == false)
                application.DisplayAlerts = true;
        }

        public static void FNCQCSheetUnProtect(Worksheet targetSheet)
        {
            targetSheet.Unprotect(Constants.Password);
        }

        public static void ExcelSet(Application application)
        {
            application.EnableCancelKey = XlEnableCancelKey.xlDisabled;
            application.EnableEvents = false;
            application.ScreenUpdating = false;
            if (FNSVisibleBookCount(application) > 0)
                application.Calculation = XlCalculation.xlCalculationManual;
        }

        public static int FNSVisibleBookCount(Application application)
        {
            int i = 0;
            return i;
        }

        public static void FNCGTAutoSettingMainIni(Worksheet gtSheet, bool fromSheet = false)
        {
            Range targetCell = gtSheet.Cells[Constants.GT.GtRowDataStart, Constants.GT.GtColChartType];
            Range lastDataCell = ExcelUtil.EndxlUp(targetCell);
            Range range = gtSheet.Range[targetCell, lastDataCell];
            if (lastDataCell.Row >= Constants.GT.GtRowDataStart)
            {
                range.ClearContents();
                FNCCellFormatInitializeGt(range);
                if(fromSheet){
                    Range execCell = range.Offset[0, Constants.GT.GtColExec - targetCell.Column];
                    execCell.ClearContents(); 
                }else{
                    foreach (Range tmpCell in range)
                    {
                        FNCGtChangeChartType(tmpCell);
                    }
                }
            }
        }

        public static ItemList GetItemList(string chartType, Range itemArea)
        {
            String listName = null;
            Range formatRange = null;
            switch (chartType)
            {
                case Constants.GT.GTSA:
                    listName = Constants.EqEqual + Constants.VariableList.ListItemSA;
                    formatRange = itemArea.Cells[1];
                    break;
                case Constants.GT.GTMA:
                    listName = Constants.EqEqual + Constants.VariableList.ListItemMA;
                    formatRange = itemArea.Cells[1];
                    break;
                case Constants.GT.GTN:
                    listName = Constants.EqEqual + Constants.VariableList.ListItemN;
                    formatRange = itemArea.Cells[1];
                    break;
                case Constants.GT.GTMTS:
                case Constants.GT.GTRNK:
                    listName = Constants.EqEqual + Constants.VariableList.ListItemSA;
                    formatRange = itemArea;
                    break;
                case Constants.GT.GTMTM:
                    listName = Constants.EqEqual + Constants.VariableList.ListItemSAMA;
                    formatRange = itemArea;
                    break;
                case Constants.GT.GTMTN:
                case Constants.GT.GTRAT:
                    listName = Constants.EqEqual + Constants.VariableList.ListItemN;
                    formatRange = itemArea;
                    break;
            }
            if (null == listName)
            {
                return null;
            }
            else
            {
                return new ItemList(listName, formatRange);
            }

        }

        private static void FNCCellFormatInitializeGt(Range targetCell)
        {
            Worksheet targetSheet = targetCell.Worksheet;
            Range graphCell = targetCell.Offset[0, Constants.GT.GtColGraphType - targetCell.Column];
            Range kenteiCell = targetCell.Offset[0, Constants.GT.GtColTest - targetCell.Column];
            Range hyodaiCell = targetCell.Offset[0, Constants.GT.GtColTableHeading - targetCell.Column];

            Range startCell = targetCell.Offset[0, Constants.GT.GtColItem - targetCell.Column];
            Range endCell = targetCell.Offset[0, (Constants.GT.GtColItem + Constants.GT.GtMaxItemNo - 1) - targetCell.Column];
            Range itemArea = targetSheet.get_Range(startCell, endCell);


            FNCCellFormatInitialize(kenteiCell);
            FNCCellFormatInitialize(graphCell);
            FNCCellFormatInitialize(hyodaiCell);
            FNCCellFormatInitialize(itemArea);
        }


        public static void FNCGtChangeChartType(Range targetCell)
        {
            Worksheet targetSheet = targetCell.Worksheet;
            Range graphCell = targetSheet.Cells[targetCell.Row, Constants.GT.GtColGraphType];
            Range kenteiCell = targetSheet.Cells[targetCell.Row, Constants.GT.GtColTest];
            Range hyodaiCell = targetSheet.Cells[targetCell.Row, Constants.GT.GtColTableHeading];

            Range startCell = targetSheet.Cells[targetCell.Row, Constants.GT.GtColItem];
            Range endCell = targetSheet.Cells[targetCell.Row, Constants.GT.GtColItem + Constants.GT.GtMaxItemNo - 1];
            Range itemArea = targetSheet.get_Range(startCell, endCell);

            String listName = "";

            ReturnClass cRet = FNCGetKenteiList(targetCell.Value);

            if (cRet.Result)
                FNCCellFormatSetting(kenteiCell, cRet.Value, "", 19);

            cRet = FNCGetGraphList(targetCell);

            if (cRet.Result)
            {
                FNCCellFormatSetting(graphCell, cRet.Value, "", 19);
                graphCell.Validation.ShowInput = false;
            }

            cRet = FNCGetHyodaiList(targetCell.Value);

            if (cRet.Result)
            {
                FNCCellFormatSetting(hyodaiCell, cRet.Value, "", 19);
                hyodaiCell.Validation.ShowInput = false;
            }

            ItemList list = GetItemList(targetCell.Value, itemArea);

            if (list != null)
            {
                listName = list.ListName;
                FNCCellFormatSetting(list.FormatRange, listName, "", 19);
            }

            FNCCheckExecEnable(targetCell.Worksheet, targetCell, false);
        }

        public static void FNCCheckExecEnable(Worksheet worksheet, Range targetCell, bool dClickFlg)
        {
            Range execCell;
            Boolean EE_Flg;

            EE_Flg = worksheet.Application.EnableEvents;
            worksheet.Application.EnableEvents = false;

            execCell = worksheet.Cells[targetCell.Row, 2];

            if (execCell.End[XlDirection.xlToRight].Column == ExcelUtil.EndColumn(worksheet))
            {
                if (execCell.Value != null || execCell.Value != "")
                    execCell.ClearContents();
            }
            else
            {
                string str = execCell.Value;
                switch (str)
                {
                    case "Off":
                        execCell.Value = "On";
                        break;
                    case "":
                        execCell.Value = "On";
                        break;
                    default:
                        if (dClickFlg == true)
                            execCell.Value = "Off";
                        break;
                }
            }

            worksheet.Application.EnableEvents = EE_Flg;
        }

        //public static ReturnClass FNCGetItemList(Range targetCell, Range itemArea, out Range item)
        //{
        //    String listName="";
        //    Range formatRange = itemArea;
        //    ReturnClass cRet = new ReturnClass();
        //    string chartType = targetCell.Value;
        //    cRet.Result = false;

        //    switch (chartType) {
        //        case "GT-SA":
        //            listName = "=" + "List_Item_SA";
        //            formatRange = itemArea.Cells[1];
        //            break;
        //        case "GT-MA":
        //            listName = "=" + "List_Item_MA";
        //            formatRange = itemArea.Cells[1];
        //            break;
        //        case "GT-N":
        //            listName = "=" + "List_Item_N";
        //            formatRange = itemArea.Cells[1];
        //            break;
        //        case "GT-MTS":
        //            listName = "=" + "List_Item_SA";
        //            formatRange = itemArea;
        //            break;
        //        case "GT-RNK":
        //            listName = "=" + "List_Item_SA";
        //            formatRange = itemArea;
        //            break;
        //        case "GT-MTM":
        //            listName = "=" + "List_Item_SAMA";
        //            formatRange = itemArea;
        //            break;
        //        case "GT-MTN":
        //            listName = "=" + "List_Item_N";
        //            formatRange = itemArea;
        //            break;
        //        case "GT-RAT":
        //            listName = "=" + "List_Item_N";
        //            formatRange = itemArea;
        //            break;
        //    }

        //    if (listName != "")
        //        cRet.Result = true;

        //    item = formatRange;            
        //    cRet.Value = listName;

        //    return cRet;
        //}

        public static ReturnClass FNCGetHyodaiList(dynamic chartType)
        {
            ReturnClass cRet = new ReturnClass
            {
                Result = true
            };

            switch (chartType)
            {
                case "GT-SA":
                    cRet.Result = false;
                    break;
                case "GT-MA":
                    cRet.Result = false;
                    break;
                case "GT-N":
                    cRet.Result = false;
                    break;
                case "":
                    cRet.Result = false;
                    break;
                case null:
                    cRet.Result = false;
                    break;
                default:
                    cRet.Result = true;
                    break;
            }
            return cRet;
        }

        public static ReturnClass FNCGetGraphList(Range targetCell)
        {
            string chartType = targetCell.Value;
            String graphList;

            ReturnClass cRet = new ReturnClass();

            graphList = "";
            cRet.Result = false;
            if (chartType == null || chartType == "" || chartType == Constants.GT.GTMTN)
                return cRet;
            else
                cRet.Result = true;


            //findCell = qcSheet.Range[Constants.GT.GtQCMainChartType];//ExcelUtil.Find(rng, chartType);
            //if (findCell != null)
            //{
            //    for (int j = 0; j <= 9; j++)
            //    {
            //        if (findCell.Offset[targetCell.Row, j + 3].Value != null || findCell.Offset[targetCell.Row, j + 3].Value != "")
            //            graphList = graphList + "," + findCell.Offset[targetCell.Row, j + 3].Value;
            //    }
            //    cRet.Result = true;
            //}
            graphList = GetGraphList(chartType);
            cRet.Value = graphList;

            return cRet;
        }

        public static void FNCCellFormatSetting(Range targetRange, object strList, string inputTitle, int colorIndex)
        {
            targetRange.Locked = false;
            targetRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            targetRange.ShrinkToFit = XlHAlign.xlHAlignCenter;
            if (colorIndex != 0)
                targetRange.Interior.ColorIndex = colorIndex;
            if (strList == null || strList.ToString() == "")
            {
                targetRange.Validation.Delete();
                targetRange.Validation.Add(XlDVType.xlValidateInputOnly);
            }
            else
            {
                ExcelUtil.AddValidation(targetRange, XlDVType.xlValidateList, Type.Missing, Type.Missing, strList.ToString(), Type.Missing);
                //targetRange.Validation.Delete();
                //targetRange.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertStop, null, strList);
                //targetRange.Validation.InCellDropdown = true;

                //    If Len(Input_Title) > 30 Then   '2012/11/5
                //   .InputTitle = Str_LeftB & Left(Input_Title, 25) & Str_RightB_Pattern1
                //Else
                //    .InputTitle = Str_LeftB & Input_Title & Str_RightB
                //End If
                //.InputMessage = IIf(Input_Message = "", Str_Please_Select & Input_Title, Input_Message) 'Global対応
                //.ShowInput = (Input_Title <> "")
                //    If Len(Input_Title) > 32 Then   '2012/11/5
                //        .ErrorTitle = Left(Error_Title, 32)
                //Else
                //    .ErrorTitle = IIf(Error_Title = "", str_Input_Error, Error_Title)
                //End If
                //.ErrorMessage = IIf(Error_Message = "", Str_MitourokuNo & Input_Title & Str_Desu, Error_Message)
                //.ShowError = Show_Error
                targetRange.Validation.IMEMode = 3;
            }
        }

        public static ReturnClass FNCGetKenteiList(string chartType)
        {
            object kenteiList;
            ReturnClass cRet = new ReturnClass();

            kenteiList = "";
            cRet.Result = false;
            if (chartType == null || chartType == "" || chartType == Constants.GT.GTN)
                return cRet;
            string sep=CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            switch (chartType)
            {
                case Constants.GT.GTSA:
                    kenteiList = "1";
                    break;
                case Constants.GT.GTMA:
                    kenteiList = "1";
                    break;
                case Constants.GT.GTRAT:
                    kenteiList = "2";
                    break;
                case Constants.GT.GTMTN:
                    kenteiList = "2";
                    break;
                case Constants.GT.GTRNK:
                    kenteiList = "1,2";
                    break;
                case Constants.GT.GTMTS:
                    kenteiList = "1,2";
                    break;
                case Constants.GT.GTMTM:
                    kenteiList = "1,2";
                    break;
            }

            cRet.Result = true;
            cRet.Value = kenteiList;

            return cRet;
        }

        public static void FNCCellFormatInitialize(Range targetRange)
        {
            bool EEFlg = targetRange.Worksheet.Application.EnableEvents;
            targetRange.Worksheet.Application.EnableEvents = false;

            //If Color_Flag = True Then Target_Range.Interior.ColorIndex = xlNone;

            targetRange.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;

            targetRange.ClearContents();

            targetRange.Validation.Delete();

            //targetRange.Locked = true;

            targetRange.Worksheet.Application.EnableEvents = EEFlg;
        }

        public static T[,] ResizeArray<T>(T[,] original, int rows, int cols)
        {
            var newArray = new T[rows,cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for(int i = 0; i < minRows; i++)
                for(int j = 0; j < minCols; j++)
                   newArray[i, j] = original[i, j];
            return newArray;
        }
    }

    public class ItemList
    {
        string listName;
        Range formatRange;

        public ItemList()
        {
        }

        public ItemList(string listName, Range formatRange)
        {
            this.ListName = listName;
            this.FormatRange = formatRange;
        }

        public string ListName { get => listName; set => listName = value; }
        public Range FormatRange { get => formatRange; set => formatRange = value; }
    }


}
