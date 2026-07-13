using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using log4net;
using Microsoft.Office.Interop.Excel;
using Microsoft.Vbe.Interop.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using static ExcelAddIn.Common.Constants;
using Application = Microsoft.Office.Interop.Excel.Application;
//using MouseKeyboardActivityMonitor;
//using MouseKeyboardActivityMonitor.WinApi;
using ProgressBar = Qc4Launcher.Forms.ProgressBar;
using System.Globalization;

namespace ExcelAddIn.Common
{
    internal class GTSheetChange
    {
        private static Workbook Workbook;

        private static CommandButton CmdBtnExec;
        private static CommandButton CmdBtnCheck;
        private static CommandButton CmdBtnOption;
        private static CommandButton CmdBtnInsert;
        private static CommandButton CmdBtnDelete;
        private static CommandButton CmdBtnAuto;

        internal static void GTValueChange(Worksheet sheet, Range target)
        {
            AppHelper appHelper = new AppHelper();
            appHelper.ExcelSet(calculationMode: false, targetApp: sheet.Application);
            if (target.Columns.Count == 1
                && target.Rows.Count != target.EntireColumn.Rows.Count)
            {
                foreach (Range targetCell in target)
                {
                    switch (targetCell.Column)
                    {
                        case Common.Constants.GT.GtColChartType:
                            GtChangeChartType(targetCell);
                            break;
                        case Common.Constants.GT.GtColGraphType:
                            break;
                        default:
                            if (targetCell.Column >= Common.Constants.GT.GtColItem)
                            {
                                GtChangeItem(targetCell);
                            }
                            break;
                    }

                }
            }

            appHelper.ExcelReset(true, targetApp: sheet.Application);

        }

        private static void GtChangeItem(Range targetCell)
        {
            if (targetCell.Row < Constants.GT.GtRowDataStart)
            {
                return;
            }
            Range ItemArea = GetItemRange(targetCell);
            // TO DO : check with qc3 implimentation
            //Call FNC_CloseUp_Range(Item_Range)
            CommonFunctions.CloseUpRange(ItemArea);

        }

        private static void GtChangeChartType(Range targetCell)
        {
            if (targetCell.Row < Constants.GT.GtRowDataStart)
            {
                return;
            }
            Range graphCell = GetGraphCell(targetCell);
            Range KenteiCell = GetKenteiCell(targetCell);
            Range HyodaiCell = GetHyodaiCell(targetCell);
            Range ItemArea = GetItemRange(targetCell);

            CommonFunctions.CellFormatInitialize(KenteiCell);
            CommonFunctions.CellFormatInitialize(graphCell);
            CommonFunctions.CellFormatInitialize(HyodaiCell);
            CommonFunctions.CellFormatInitialize(ItemArea);
            string targetCellVal = targetCell.Value == null || targetCell.Value=="" ? null : targetCell.Value.ToString();
            string list = GetKenteiList(targetCellVal);
            if (list != null)
            {
                CommonFunctions.CellFormatSetting(KenteiCell, list, "", Constants.Color.yellow, SheetProtect: false);
            }
            
            list = GetGraphList(targetCellVal);
            if (list != null)
            {
                CommonFunctions.CellFormatSetting(graphCell, list, "", Constants.Color.yellow, SheetProtect: false);
                graphCell.Validation.ShowInput = false;
            }

            list = GetHyodaiList(targetCellVal);
            if (list != null)
            {
                CommonFunctions.CellFormatSetting(HyodaiCell, "", "", Constants.Color.yellow, iMEMode: XlIMEMode.xlIMEModeNoControl, SheetProtect: false);
                HyodaiCell.Validation.ShowInput = false;
            }

            QC4Common.Common.ItemList itemList = QC4Common.Common.GTAutoSetting.GetItemList(targetCellVal, ItemArea);
            if (itemList != null)
            {
                CommonFunctions.CellFormatSetting(itemList.FormatRange, itemList.ListName, "", Constants.Color.yellow, SheetProtect: false);
            }

            Change.ExecToggle(targetCell.Worksheet, targetCell, false);
        }


        private static Range GetItemRange(Range targetCell)
        {
            Range startCell = targetCell.Worksheet.Cells[targetCell.Row, Constants.GT.GtColItem];
            Range endCell = targetCell.Worksheet.Cells[targetCell.Row, Constants.GT.GtColItem + Constants.GT.GtMaxItemNo - 1];
            return targetCell.Worksheet.Range[startCell, endCell];

        }

        private static Range GetHyodaiCell(Range targetCell)
        {
            return targetCell.Worksheet.Cells[targetCell.Row, 6];
        }

        private static Range GetKenteiCell(Range targetCell)
        {
            return targetCell.Worksheet.Cells[targetCell.Row, 4];
        }

        private static Range GetGraphCell(Range targetCell)
        {
            return targetCell.Worksheet.Cells[targetCell.Row, Constants.GT.GtColGraphType];
        }

        private static string GetKenteiList(string chartType)
        {
            string kenteiList = null;
            if (chartType == "" || chartType == Constants.GT.GTN)
            {
                return kenteiList;
            }
            String sep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            switch (chartType)
            {
                case Constants.GT.GTSA:
                case Constants.GT.GTMA:
                    kenteiList = "1";
                    break;
                case Constants.GT.GTRAT:
                case Constants.GT.GTMTN:
                    kenteiList = "2";
                    break;
                case Constants.GT.GTRNK:
                case Constants.GT.GTMTS:
                case Constants.GT.GTMTM:
                    kenteiList = "1"+sep+"2";
                    break;
            }
            return kenteiList;
        }

        private static string GetGraphList(string chartType)
        {
            string graphList = null;
            string graph;
            String sep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            if (chartType == "" || chartType == Constants.GT.GTMTN || chartType == Constants.GT.GTN)
            {
                graphList = (sep == ",") ? graphList : graphList.Replace(",", sep);
                return graphList;
            }
            else if (chartType == Constants.GT.GTMTS)
            {
                graph = (sep == ",") ? AddinResource.GTMTSGraph : AddinResource.GTMTSGraph.Replace(",", sep);
                return graph;
            }
            else if (chartType == Constants.GT.GTMTM)
            {
                graph = (sep == ",") ? AddinResource.GTMTMGraph : AddinResource.GTMTMGraph.Replace(",", sep);
                return graph;
            }
            else if (chartType == Constants.GT.GTRAT)
            {
                graph = (sep == ",") ? AddinResource.GTRATGraph : AddinResource.GTRATGraph.Replace(",", sep);
                return graph;
            }
            else if (chartType == Constants.GT.GTRNK)
            {
                graph = (sep == ",") ? AddinResource.GTRNKGraph : AddinResource.GTRNKGraph.Replace(",", sep);
                return graph;
            }
            else if (chartType == Constants.GT.GTSA)
            {
                graph = (sep == ",") ? AddinResource.GTSAGraph : AddinResource.GTSAGraph.Replace(",", sep);
                return graph;
            }
            else if (chartType == Constants.GT.GTMA)
            {
                graph = (sep == ",") ? AddinResource.GTMAGraph : AddinResource.GTMAGraph.Replace(",", sep);
                return graph;
            }
            return graphList; // TO DO
        }
        private static string GetHyodaiList(string chartType)
        {
            if (chartType == "")
            {
                return null;
            }

            switch (chartType)
            {
                case Constants.GT.GTSA:
                case Constants.GT.GTMA:
                case Constants.GT.GTN:
                    return null;
            }
            return "";
        }


        

        internal static void bindButtons(Workbook workBook,Worksheet sht)
        {
            Workbook = workBook;
            //foreach (OLEObject oleObject in sht.OLEObjects())
            //{
            //    switch (oleObject.Name)
            //    {
            //        case Constants.GT.GTBUTTONINSERT:
            //            CmdBtnInsert = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
            //            CmdBtnInsert.Click += () => GTInsertDel(sht);
            //            break;
            //        case Constants.GT.GTBUTTONDELETE:
            //            CmdBtnDelete = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
            //            CmdBtnDelete.Click += () => GTInsertDel(sht, true);
            //            break;
            //        case Constants.GT.GTBUTTONCHECK:
            //            CmdBtnCheck = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
            //            bool cancel = false;
            //            CmdBtnCheck.Click += () => Change.ValidateGTTab(null, ref cancel);
            //            break;
            //        case Constants.GT.GTBUTTONOPTION:
            //            CmdBtnOption = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
            //            CmdBtnOption.Click += () => showOption(sht);
            //            break;
            //        case Constants.GT.GTBUTTONEXEC:
            //            CmdBtnExec = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
            //            CmdBtnExec.Click += () => GTExec(sht);
            //            break;
            //        case Constants.GT.GTBUTTONAUTO:
            //            CmdBtnAuto = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
            //            CmdBtnAuto.Click += () => FNCGTAutoSettingMain(sht);
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }
        static ProgressBar progress;
        internal static void FNCGTAutoSettingMain(Worksheet sht)
        {
            Worksheet gtSheet = ExcelUtil.GetWorkSheetByCodeName(Globals.ThisAddIn.Application.ActiveWorkbook, Constants.SheetType.GTCounting);
            Range lastCell = QC4Common.Common.GTAutoSetting.CheckDataInQS(gtSheet);
            if (lastCell != null)
            {                
                if (MessageDialog.InfoYesNo(AddinResource.GT_AUTO_SETTNG_CONFIRM, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    progress = new ProgressBar(gtSheet);
                    new Thread(() => FNCGTAutoSettingExec(gtSheet)).Start();
                    progress.ShowDialog();
                }
            }
        }

        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static void FNCGTAutoSettingExec(Worksheet gtSheet)
        {
            try
            {
                progress.OnWorkerMethodComplete(0, AddinResource.GT_PROGRESS_MSG1);
                QC4Common.Common.GTAutoSetting.ExcelSet(gtSheet.Application);
                progress.OnWorkerMethodComplete(10, "", false);
                QC4Common.Common.GTAutoSetting.FNCQCSheetUnProtect(gtSheet);
                progress.OnWorkerMethodComplete(20, AddinResource.GT_PROGRESS_MSG2);
                Object[,] defaAry = QC4Common.Common.GTAutoSetting.GetDefAry(gtSheet);
                progress.OnWorkerMethodComplete(30, AddinResource.GT_PROGRESS_MSG3);
                progress.OnWorkerMethodComplete(40, AddinResource.GT_PROGRESS_MSG4);
                QC4Common.Common.GTAutoSetting.FNCGTAutoSettingMainIni(gtSheet, true);
                progress.OnWorkerMethodComplete(50, "", false);
                QC4Common.Common.GTAutoSetting.FNCGetQuesData(gtSheet, defaAry, ExcelUtil.GetWorksheetByIndex(1));
                progress.OnWorkerMethodComplete(60, AddinResource.GT_PROGRESS_MSG5);
                QC4Common.Common.GTAutoSetting.ExcelReset(gtSheet.Application);
                progress.OnWorkerMethodComplete(100, AddinResource.GT_PROGRESS_MSG6, IsForceStop:true);
            }
            catch(Exception ex)
            {
                progress.OnWorkerMethodComplete(0, "Error Occured", true);
                _log.LogError(ex.Message);
            }
        }
        public static ReturnClass FNCGetHyodaiList(dynamic chartType)
        {
            ReturnClass cRet = new ReturnClass();
            cRet.Result = true;

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
        //public static void GTExec(Worksheet sht)
        //{
        //    sht.Application.Cursor = XlMousePointer.xlWait;
        //    bool cancel = false;
        //    if (Change.ValidateGTTab((Microsoft.Office.Interop.Excel.Worksheets)Globals.ThisAddIn.Application.Worksheets, null, ref cancel, true))
        //    {
        //        int msg = RegisterWindowMessage(msgstrExec);
        //        if (msg == 0)
        //        {
        //            MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
        //        }

        //        SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
        //    }
        //    else
        //    {
        //        sht.Application.Cursor = XlMousePointer.xlDefault;
        //    }
        //}

        //public static void showOption(Worksheet sht)
        //{
        //    sht.Application.Cursor = XlMousePointer.xlWait;
        //    bool cancel = false;
        //    if (Change.ValidateGTTab((Worksheets)Globals.ThisAddIn.Application.Worksheets, null, ref cancel, true))
        //    {
        //        int msg = RegisterWindowMessage(msgstrOption);
        //        if (msg == 0)
        //        {
        //            MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
        //        }
        //        SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
        //    }
        //    else
        //    {
        //        sht.Application.Cursor = XlMousePointer.xlDefault;
        //    }
        //}

        internal static void GTInsertDel(Worksheet sheet, bool deleteMode = false)
        {
            Application application = sheet.Application;

            if (!(application.Selection is Range))
            {
                return;
            }

            Range range = application.Selection;
            Worksheet targetSheet = range.Worksheet;
            if (range.Areas.Count > 1)
            {
                MessageDialog.ErrorOk(AddinResource.CANNOT_SPECIFY_MULTIPLE_RANGE);
                return;
            }

            if (range.Rows.Count == ExcelUtil.EndRow(targetSheet) && range.Columns.Count == ExcelUtil.EndColumn(targetSheet))
            {
                return;
            }

            AppHelper appHelper = new AppHelper();
            appHelper.ExcelSet();
            try
            {
                string targetAddress = "";
                if (deleteMode == false)
                {
                    targetAddress = range.Address;
                }

                if (range.Rows.Count == ExcelUtil.EndRow(targetSheet))
                {
                    if (deleteMode == false)
                    {
                        int offsetCount = range.Columns.Count;
                        if (CmRowColInsert(targetSheet, XlRowCol.xlColumns, range.Column, range.Columns.Count, 7) == true)
                        {
                            targetSheet.Range[targetAddress].Offset[0, offsetCount].Select();
                        }
                    }
                    else
                    {
                        CmRowColDelete(targetSheet, XlRowCol.xlColumns, range.Column, range.Columns.Count, 7);
                    }

                }
                else
                {
                    if (deleteMode == false)
                    {
                        int offsetCount = range.Rows.Count;
                        if (CmRowColInsert(targetSheet, XlRowCol.xlRows, range.Row, range.Rows.Count, GT.GtRowDataStart) == true)
                        {
                            GtColumnAssign(targetSheet);
                            targetSheet.Range[targetAddress].Offset[offsetCount, 0].Select();
                        }
                    }
                    else
                    {
                        if (CmRowColDelete(targetSheet, XlRowCol.xlRows, range.Row, range.Rows.Count, GT.GtRowDataStart) == true)
                        {
                            GtColumnAssign(targetSheet, true);
                        }
                    }
                }
            }
            finally
            {
                appHelper.ExcelReset();
            }
        }

        public static bool CmRowColInsert(Worksheet targetSheet, XlRowCol rowCol, int targetPlace, int insertNum, int minCount = 0)
        {
            if (targetPlace < minCount && minCount != 0)
            {
                return false;
            }

            if (rowCol == XlRowCol.xlRows)
            {
                targetSheet.Range[targetSheet.Rows[targetPlace], targetSheet.Rows[targetPlace + insertNum - 1]].Insert(Shift: XlDirection.xlDown);
                int copyRowCol = Math.Max(targetSheet.Range["A1"].SpecialCells(XlCellType.xlCellTypeLastCell).Row + 1, minCount);
                targetSheet.Rows[copyRowCol].Copy();
                targetSheet.Range[targetSheet.Rows[targetPlace], targetSheet.Rows[targetPlace + insertNum - 1]].PasteSpecial(XlPasteType.xlPasteAll);

            }
            else
            {
                targetSheet.Range[targetSheet.Columns[targetPlace], targetSheet.Columns[targetPlace + insertNum - 1]].Insert(Shift: XlDirection.xlToRight);
                int copyRowCol = Math.Max(targetSheet.Range["A1"].SpecialCells(XlCellType.xlCellTypeLastCell).Column + 1, minCount);
                targetSheet.Columns[copyRowCol].Copy();
                targetSheet.Range[targetSheet.Columns[targetPlace], targetSheet.Columns[targetPlace + insertNum - 1]].PasteSpecial(XlPasteType.xlPasteAll);

            }
            targetSheet.Application.CutCopyMode = (XlCutCopyMode)0;
            return true;
        }

        public static void GtColumnAssign(Worksheet targetSheet, bool emptyPermit = false)
        {
            int endRow = ExcelUtil.EndxlUp(targetSheet.Cells[1, 2]).Row;
            if (emptyPermit == false)
            {
                endRow = Math.Max(endRow, ExcelUtil.EndxlUp(targetSheet.Cells[1, 3]).Row);
            }
            if (endRow < GT.GtRowDataStart) return;
            dynamic colArray = targetSheet.Range[targetSheet.Cells[GT.GtRowDataStart, 2], targetSheet.Cells[endRow, 2]].Value2;
            if (colArray.GetType().IsArray)
            {
                for (int i = 1; i <= colArray.GetLength(0); i++)
                {
                    if (!(emptyPermit == true && (null == colArray[i, 1] || string.IsNullOrEmpty(colArray[i, 1].ToString()))))
                    {
                        //colArray[i, 1] = i;
                        if (null == colArray[i, 1] || string.IsNullOrEmpty(colArray[i, 1]))
                        {
                            colArray[i, 1] = AddinResource.MarkOFF; //MarkWhiteCircle
                        }
                    }
                }
            }
            targetSheet.Range[targetSheet.Cells[GT.GtRowDataStart, 2], targetSheet.Cells[endRow, 2]].Value2 = colArray;
        }

        public static bool CmRowColDelete(Worksheet targetSheet, XlRowCol rowCol, int targetPlace, int deleteNum, int minCount = 0)
        {

            if (targetPlace < minCount && minCount != 0)
            {
                return false;
            }
            if (rowCol == XlRowCol.xlRows)
            {
                targetSheet.Range[targetSheet.Rows[targetPlace], targetSheet.Rows[targetPlace + deleteNum - 1]].Delete(Shift: XlDirection.xlUp);
            }
            else
            {
                targetSheet.Range[targetSheet.Columns[targetPlace], targetSheet.Columns[targetPlace + deleteNum - 1]].Delete(Shift: XlDirection.xlToLeft);

            }
            return true;
        }

        public bool CheckActivationKey()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software/QC4"))
            {
                key.SetValue("qc4Key", "keyyyyy");
                var c = key.GetValue("qc4Key");
                key.SetValue("qc4Key", "");
                key.Close();
            }
            return true;
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(int hWnd, int Msg, int wParam, int lParam);

        private static string msgstrExec = "GTEXEC";
        private static string msgstrOption = "GTOPTION";
        private const int HWND_BROADCAST = 0xffff;

    }

    
}