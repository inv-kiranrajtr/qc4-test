using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Macromill.QCWeb.Common;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Batch.Report.Tables;

namespace Qc4Launcher.Logic.Cross_Report
{
    class CrossReportHelper
    {
        public static void RemoveOutPutFiles()
        {
            string filePath = @"E:\OfficeDocuments\Cross Report\sig\output\";//Path.Combine(Path.GetTempPath(), "QC4", folderName);
            GlobalMethodClass.GuaranteeDirectoryExist(filePath);
            System.IO.DirectoryInfo di = new DirectoryInfo(filePath);

            if (di.GetFiles() != null)
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        file.Delete();

                    }
                    catch (Exception ex)
                    {
                       // _log.Error(ex.Message);
                    }
                }
            }
        }
        public static string GetPath(string OutputDirectoryPath, Output Output, string folderName
             , Reportset ThisReportset, Microsoft.Office.Interop.Excel.Application xlApp, string qc4FileName, string Suffix = "", string Prefix = null)
        {
            string ext;
            string outPath;
            string n;
            string p = null;
            int i = 0;
            int j = 0;
            //string Prefix = ThisReportset.DivName + ThisReportset.FileNamePrefix;
            string prfix = NPOICrossCreator.getPrefix(qc4FileName);


            if (!string.IsNullOrEmpty(prfix))
            {
                Prefix = prfix + Prefix;
            }

            ext = "xlsx";
            j = 0;

            if (OutputDirectoryPath == null)
            {
                outPath = Path.Combine(Path.GetTempPath(), "QC4", folderName);
                outPath = Path.Combine(outPath, Path.GetFileNameWithoutExtension(Util.Constants.Qc4FileName));
                GlobalMethodClass.GuaranteeDirectoryExist(outPath);
                DirectoryInfo dir = new DirectoryInfo(outPath);
                dir.Attributes = FileAttributes.ReadOnly;
            }
            else
            {
                outPath = OutputDirectoryPath;
            }

            do
            {
                n = Prefix + (j > 0 ? "_" + j : "") + Suffix + "." + ext;
                j = j + 1;
                p = OutputUtil.BuildPath(outPath, n, xlApp.PathSeparator);
            } while (File.Exists(p));
        
            return p;
        }
        public static void InserStringValue(WorksheetPart worksheetPart,string value, int rowIdx,int ColIdx)
        {
            Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowIdx);
            Cell cell = OpenXmlHelper.GetCell(row, rowIdx, ColIdx);
            cell.CellValue = new CellValue(value);
            cell.DataType = CellValues.String;
        }
        internal static void PutValue(WorksheetPart worksheetPart, int rowIdx, int col, ref Array arrValue, bool NotRevise = false)
        {
            int u = 0;
            int i, j;
            int d = 0;
            double isNum = 0;
            string tmp;
            Type VarType = arrValue.GetType().GetElementType();
            if (worksheetPart == null) { return; }
            //VarType = VarType.GetElementType();
            Array tmpArray = (Array)arrValue;
          
            for (i = arrValue.GetLowerBound(0); i <= arrValue.GetUpperBound(0); i++, rowIdx++)
            {
                int colLocal = col;
                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet,(uint)rowIdx);
                for (j = arrValue.GetLowerBound(1); j <= arrValue.GetUpperBound(1); j++, colLocal++)
                {
                    if (arrValue.GetValue(i, j) != null)
                    {
                        Cell cell = row.Elements<Cell>().Where(p=>p.CellReference == (OpenXmlHelper.ColumnIndexToColumnLetter(colLocal) + rowIdx)).FirstOrDefault();
                        object val = arrValue.GetValue(i, j);
                        if (val.GetType() == typeof(string))
                        {
                            cell.CellValue = new CellValue((string)val);
                            if (!double.TryParse((string)val,out isNum))
                                cell.DataType = CellValues.String;
                            else if (double.TryParse((string)val, out isNum) && val.ToString().Contains(","))
                                cell.DataType = CellValues.String;
                        }
                        else if (VarType == typeof(object))
                        {
                            cell.CellValue = new CellValue(val.ToString());
                        }
                    }
                }
            }
        }

        internal static void PutGraphTableValues(WorksheetPart worksheetPart, int rowIdx, int col, ref Array arrValue,int endCol, bool NotRevise = false)
        {
            int u = 0;
            int i, j;
            int d = 0;
            double isNum = 0;
            string tmp;
            Type VarType = arrValue.GetType().GetElementType();
            if (worksheetPart == null) { return; }
            //VarType = VarType.GetElementType();
            Array tmpArray = (Array)arrValue;

            for (i = arrValue.GetLowerBound(0); i <= arrValue.GetUpperBound(0); i++, rowIdx++)
            {
                int colLocal = col;
                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowIdx);
                for (j = arrValue.GetLowerBound(1); j <= endCol; j++, colLocal++)
                {
                    if (arrValue.GetValue(i, j) != null)
                    {
                        Cell cell = row.Elements<Cell>().Where(p => p.CellReference == (OpenXmlHelper.ColumnIndexToColumnLetter(colLocal) + rowIdx)).FirstOrDefault();
                        object val = arrValue.GetValue(i, j);
                        if (val.GetType() == typeof(string))
                        {
                            cell.CellValue = new CellValue((string)val);
                            if (!double.TryParse((string)val, out isNum))
                                cell.DataType = CellValues.String;
                        }
                        else if (VarType == typeof(object))
                        {
                            cell.CellValue = new CellValue(val.ToString());
                        }
                    }
                }
            }
        }

        internal static void PutPortraitTableValue(WorksheetPart worksheetPart, int rowIdx, int col, ref Array arrValue, bool NotRevise = false, bool simpleAggr = false, bool isN = false, bool isSideTable = false, bool thisIsSideGraph = false,bool isSigTest = false)
        {
            int u = 0;
            int i, j;
            int d = 0;
            double isNum = 0;
            string tmp;
            Type VarType = arrValue.GetType().GetElementType();
            if (worksheetPart == null) { return; }
            //VarType = VarType.GetElementType();
            Array tmpArray = (Array)arrValue;

            for (i = arrValue.GetLowerBound(0); i <= arrValue.GetUpperBound(0); i++, rowIdx++)
            {
                int colLocal = col;
                if (simpleAggr && i == 2  && !isSigTest)
                    i++;
                if (!simpleAggr && i == 3 && isN && (rowIdx == 28 || rowIdx == 29) && !isSigTest)
                    rowIdx = 30;
                int JLowerBound = arrValue.GetLowerBound(1);
                int JUpperBound = arrValue.GetUpperBound(1);
                if (!isN && !simpleAggr && isSideTable && !thisIsSideGraph && !isSigTest)
                {
                    JLowerBound = 1;
                    JUpperBound = 3;
                    if (rowIdx == 28)
                        rowIdx += 1;
                }
                else if (!isN && !simpleAggr && isSideTable && thisIsSideGraph && !isSigTest)
                {
                    if (rowIdx == 29)
                        rowIdx += 1;
                }
                if (isSigTest)
                {
                    if (isN && !simpleAggr)
                    {
                        if (rowIdx == 5)
                            continue;
                        else if (rowIdx == 8)
                            rowIdx++;
                    }
                    else if(isN && simpleAggr)
                    {
                        if (i == 4)
                            i++;
                        if (rowIdx == 5)
                            rowIdx += 3;
                        if (i == 7)
                            i++;
                    }

                    if (!isN && simpleAggr)
                    {
                        if (i == 4)
                            i++;
                        if (rowIdx == 5)
                            rowIdx += 3;
                        if (i == 7)
                            i++;
                    }
                    else if (!isN && !simpleAggr)
                    {
                        if (rowIdx == 8)
                            rowIdx++;
                    }
                }

                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowIdx);
                if (row == null)
                    continue;

                for (j = JLowerBound; j <= JUpperBound; j++, colLocal++)
                {
                    if (arrValue.GetValue(i, j) != null)
                    {
                        Cell cell = row.Elements<Cell>().Where(p => p.CellReference == (OpenXmlHelper.ColumnIndexToColumnLetter(colLocal) + rowIdx)).FirstOrDefault();
                        object val = arrValue.GetValue(i, j);
                        if (val.GetType() == typeof(string))
                        {
                            cell.CellValue = new CellValue((string)val);
                            if (!double.TryParse((string)val, out isNum))
                                cell.DataType = CellValues.String;
                            else if (double.TryParse((string)val, out isNum) && val.ToString().Contains(","))
                                cell.DataType = CellValues.String;
                        }
                        else if (VarType == typeof(object))
                        {
                            cell.CellValue = new CellValue(val.ToString());
                        }
                    }
                }
            }
        }

        internal static void PutDataValue(WorksheetPart worksheetPart, int rowIdx, int col, ref Array arrValue, bool NotRevise = false, bool thisHasSideGraph = false)
        {
            int u = 0;
            int i, j;
            int d = 0;
            double isNum = 0;
            string tmp;
            Type VarType = arrValue.GetType().GetElementType();
            if (worksheetPart == null) { return; }
            //VarType = VarType.GetElementType();
            Array tmpArray = (Array)arrValue;

            for (i = arrValue.GetLowerBound(0); i <= arrValue.GetUpperBound(0); i++, rowIdx++)
            {
                int colLocal = col;
                int JLowerBound = arrValue.GetLowerBound(1);
                int JUpperBound = arrValue.GetUpperBound(1);
                if (thisHasSideGraph)
                {
                    JLowerBound = 3;
                    JUpperBound = 3;

                }
                Row row = OpenXmlHelper.GetRow(worksheetPart.Worksheet, (uint)rowIdx);
                for (j = JLowerBound; j <= JUpperBound; j++, colLocal++)
                {
                    if (arrValue.GetValue(i, j) != null)
                    {
                        Cell cell = row.Elements<Cell>().Where(p => p.CellReference == (OpenXmlHelper.ColumnIndexToColumnLetter(colLocal) + rowIdx)).FirstOrDefault();
                        object val = arrValue.GetValue(i, j);
                        if (val.GetType() == typeof(string))
                        {
                            cell.CellValue = new CellValue((string)val);
                            if (!double.TryParse((string)val, out isNum))
                                cell.DataType = CellValues.String;
                        }
                        else if (VarType == typeof(object))
                        {
                            cell.CellValue = new CellValue(val.ToString());
                        }
                    }
                }
            }
        }

        internal static void SetColumnWidth(Columns columns, int col,double width)
        {
            Column columnDestination = new Column() { Min = (uint)col, Max = (uint)col, Width = width, CustomWidth = true };
            columns.Append(columnDestination);
        }
    }
}
