using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using Microsoft.Vbe.Interop.Forms;
using Microsoft.VisualBasic.CompilerServices;
using QC4Common.Model;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace ExcelAddIn.Common
{
    class CrossChange
    {
        private static CommandButton CmdBtnChng;
        private static CommandButton CmdBtnInsert;
        private static CommandButton CmdBtnDelete;
        private static CommandButton CmdBtnCheck;
        private static CommandButton CmdBtnOption;
        private static CommandButton CmdBtnExec;
        private static CommandButton CmdBtnRPExec;
        internal static void CRValueChange(Worksheet sheet, Range target)
        {
            if (target.Columns.Count == ExcelUtil.EndColumn(target.Worksheet)) return;
            if (target.Rows.Count == ExcelUtil.EndRow(target.Worksheet)) return;
            AppHelper appHelper = new AppHelper();
            appHelper.ExcelSet(calculationMode: false);

            foreach (Range targetCell in target)
            {
                switch (targetCell.Column)
                {
                    case Constants.CR.CRColVariable:
                        FormatSettingVariable(targetCell);
                        break;
                }

                switch (targetCell.Row)
                {
                    case Constants.CR.CRRow2CRVariable:
                    case Constants.CR.CRRow3CRVariable:
                        FormatSettingAxesVariable(targetCell);
                        break;
                    case Constants.CR.CRRowNarrowVariable:
                        FormatSettingNarrowVariable(targetCell);
                        break;
                    case Constants.CR.CRRowLineSettings:
                    case Constants.CR.CRRowNarrowSettings:
                        try
                        {
                            if (targetCell.Errors[XlErrorChecks.xlNumberAsText].Value)
                            {
                                targetCell.Errors.Item[XlErrorChecks.xlNumberAsText].Ignore = true;
                            }
                        }
                        catch { }
                        break;
                }

            }

            appHelper.ExcelReset(true);
        }

        private static void FormatSettingNarrowVariable(Range targetCell)
        {
            if (targetCell.Column <= Constants.CR.CRColInputStart)
            {
                return;
            }
            CommonFunctions.CellFormatInitialize(targetCell.Offset[1, 0], false);
            if (null == targetCell.Value2 || string.IsNullOrEmpty(targetCell.Value2.ToString()))
            {
                return;
            }
            string variableName = targetCell.Value2.ToString();
            if (!Definitions.VariableDictionary.ContainsKey(variableName))
            {
                //MessageDialog.ErrorOk(AddinResource.FAILED_TO_GET_DATA);
                return;
            }

            QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];
            int cateCnt = questionDetails.CategoryCount;
            string catList = CommonFunctions.CateList(cateCnt, true);
            if (catList.Length > 255)//[Redmine id: 189027]
            {
                Microsoft.Office.Interop.Excel.Worksheet settingssheet = ExcelUtil.GetWorkSheetByCodeName(targetCell.Application.ActiveWorkbook, Constants.SheetCodeName.Setting);
                catList = "= INDIRECT(" + "\"" + settingssheet.Name + "!$A$1:$A$" + cateCnt + "\")";
            }
            CommonFunctions.CellFormatSetting(targetCell.Offset[1, 0], catList, "");
        }

        private static void FormatSettingAxesVariable(Range targetCell)
        {
            if (targetCell.Column <= Constants.CR.CRColInputStart)
            {
                return;
            }

            if (null == targetCell.Value2 || string.IsNullOrEmpty(targetCell.Value2.ToString()))
            {
				QC4Common.Util.ExcelUtil.ClearContents(targetCell.Worksheet.Range[targetCell.Offset[1, 0], targetCell.Offset[2, 0]]);
				//targetCell.Worksheet.Range[targetCell.Offset[1, 0], targetCell.Offset[2, 0]].ClearContents();
            }
            else
            {

                string variableName = targetCell.Value2.ToString();
                if (!Definitions.VariableDictionary.ContainsKey(variableName))
                {
                    //MessageDialog.ErrorOk(AddinResource.FAILED_TO_GET_DATA );
                    return;
                }
                QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];
                targetCell.Offset[1, 0].Value2 = questionDetails.AnswerType;
                if (questionDetails.CategoryCount == 0)
                {
                    targetCell.Offset[2, 0].Value2 = "";
                }
                else
                {
                    targetCell.Offset[2, 0].Value2 = questionDetails.CategoryCount;
                }

            }
        }


        internal static void bindButtons(Worksheet sht)
        {
            foreach (OLEObject oleObject in sht.OLEObjects())
            {
                switch (oleObject.Name)
                {
                    case Constants.CR.CRBUTTONCHANGE:
                        CmdBtnChng = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        CmdBtnChng.Click += () => crossCheck(sht);
                        break;
                    case Constants.CR.CRBUTTONINSERT:
                        CmdBtnInsert = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        CmdBtnInsert.Click += () => crossInsertDel(sht);
                        break;
                    case Constants.CR.CRBUTTONDELETE:
                        CmdBtnDelete = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        CmdBtnDelete.Click += () => crossInsertDel(sht, true);
                        break;
                    case Constants.CR.CRBUTTONCHECK:
                        CmdBtnCheck = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        bool cancel = false;
                        CmdBtnCheck.Click += () => Change.ValidateCRTab(null, ref cancel);
                        break;
                    case Constants.CR.CRBUTTONOPTION:
                        // CmdBtnOption = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        //  CmdBtnOption.Click += () => showOption(sht);
                        break;
                    case Constants.CR.CRBUTTONEXEC:
                        // CmdBtnExec = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        // CmdBtnExec.Click += () => crossExec(sht);
                        break;
                    case Constants.CR.CRBUTTONRPEXEC:
                        //  CmdBtnRPExec = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        // CmdBtnRPExec.Click += () => crossRPExec(sht);
                        break;
                    default:
                        break;
                }
            }
        }


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(int hWnd, int Msg, int wParam, int lParam);
        private static string msgstrOption = "CROSSOPTION";
        private static string msgstrExec = "CROSSEXEC";
        private static string msgstrRPExec = "CROSSRPEXEC";


        private const int HWND_BROADCAST = 0xffff;
        private static void showOption(Worksheet sht)
        {

            int msg = RegisterWindowMessage(msgstrOption);
            if (msg == 0)
            {
                MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
            }

            //SendNotifyMessage(HWND_BROADCAST, msg, 4848484, 8484865);

            SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
        }

        private static void crossExec(Worksheet sht)
        {
            sht.Application.Cursor = XlMousePointer.xlWait;
            bool cancel = false;
            if (Change.ValidateCRTab(null, ref cancel, true))
            {
                int msg = RegisterWindowMessage(msgstrExec);
                if (msg == 0)
                {
                    MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                }

                //SendNotifyMessage(HWND_BROADCAST, msg, 4848484, 8484865);

                SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
            }
            else
            {
                sht.Application.Cursor = XlMousePointer.xlDefault;
            }
        }

        private static void crossRPExec(Worksheet sht)
        {
            sht.Application.Cursor = XlMousePointer.xlWait;
            bool cancel = false;
            if (Change.ValidateCRTab(null, ref cancel, true))
            {
                int msg = RegisterWindowMessage(msgstrRPExec);
                if (msg == 0)
                {
                    MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                }

                //SendNotifyMessage(HWND_BROADCAST, msg, 4848484, 8484865);

                SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
            }
            else
            {
                sht.Application.Cursor = XlMousePointer.xlDefault;
            }
        }

        internal static void crossInsertDel(Worksheet sheet, bool deleteMode = false)
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
                        if (CmRowColInsert(targetSheet, XlRowCol.xlRows, range.Row, range.Rows.Count, 14) == true)
                        {
                            CrColumnAssign(targetSheet);
                            targetSheet.Range[targetAddress].Offset[offsetCount, 0].Select();
                        }
                    }
                    else
                    {
                        if (CmRowColDelete(targetSheet, XlRowCol.xlRows, range.Row, range.Rows.Count, 14) == true)
                        {
                            CrColumnAssign(targetSheet, true);
                        }
                    }
                }
            }
            finally
            {
                appHelper.ExcelReset();
            }
        }

        private static void CrColumnAssign(Worksheet targetSheet, bool emptyPermit = false)
        {

            int endRow = ExcelUtil.EndxlUp(targetSheet.Cells[1, 2]).Row;
            if (emptyPermit == false)
            {
                endRow = Math.Max(endRow, ExcelUtil.EndxlUp(targetSheet.Cells[1, 3]).Row);
            }
            if (endRow < Constants.CR.CRRowInputStart + 1) return;
            dynamic colArray = targetSheet.Range[targetSheet.Cells[Constants.CR.CRRowInputStart + 1, 2], targetSheet.Cells[endRow, 2]].Value2;
            if (colArray.GetType().IsArray)
            {
                for (int i = 1; i <= colArray.GetLength(0); i++)
                {
                    if (!(emptyPermit == true && (null == colArray[i, 1] || string.IsNullOrEmpty(colArray[i, 1].ToString()))))
                    {
                        colArray[i, 1] = i;
                    }
                }
            }
            targetSheet.Range[targetSheet.Cells[Constants.CR.CRRowInputStart + 1, 2], targetSheet.Cells[endRow, 2]].Value2 = colArray;
        }

        private static bool CmRowColDelete(Worksheet targetSheet, XlRowCol rowCol, int targetPlace, int deleteNum, int minCount = 0)
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

        private static bool CmRowColInsert(Worksheet targetSheet, XlRowCol rowCol, int targetPlace, int insertNum, int minCount = 0)
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
                targetSheet.Range[targetSheet.Cells[2, 13], targetSheet.Cells[2, 17]].UnMerge();//Redmine ID : 195701 Merge M2Q2 was non requirement so to unmerge it in already existing file
                targetSheet.Range[targetSheet.Columns[targetPlace], targetSheet.Columns[targetPlace + insertNum - 1]].Insert(Shift: XlDirection.xlToRight);
                int copyRowCol = Math.Max(targetSheet.Range["A1"].SpecialCells(XlCellType.xlCellTypeLastCell).Column + 1, minCount);
                targetSheet.Columns[copyRowCol].Copy();
                targetSheet.Range[targetSheet.Columns[targetPlace], targetSheet.Columns[targetPlace + insertNum - 1]].PasteSpecial(XlPasteType.xlPasteAll);

            }
            targetSheet.Application.CutCopyMode = (XlCutCopyMode)0;
            return true;
        }

        public static void crossCheck(Worksheet sht)
        {

            Application application = sht.Application;

            if (!(application.Selection is Range))
            {
                return;
            }

            Range range = application.Selection;
            if (range.Rows.Count == ExcelUtil.EndRow(sht) || range.Columns.Count == ExcelUtil.EndColumn(sht))
            {
                return;
            }

            AppHelper appHelper = new AppHelper();
            appHelper.ExcelSet();
            foreach (Range targetCell in range)
            {
                if (targetCell.Column > Constants.CR.CRColInputStart && targetCell.Row > Constants.CR.CRRowInputStart)
                {
                    if (AddinResource.MarkBlackCircle == targetCell.Value2)
                    {
                        targetCell.Value2 = AddinResource.MarkWhiteCircle;
                    }
                    else if (AddinResource.MarkWhiteCircle == targetCell.Value2)
                    {
                        targetCell.Value2 = AddinResource.MarkBlackCircle;
                    }
                }
            }
            appHelper.ExcelReset();
        }

        private static void FormatSettingVariable(Range targetCell)
        {
            Range cellNo = CellNo(targetCell);

            if (targetCell.Row <= Constants.CR.CRRowInputStart)
            {
                return;
            }

            if (null == targetCell.Value2 || string.IsNullOrEmpty(targetCell.Value2.ToString()))
            {
                bool preDiv = false;
                if (null != targetCell.Offset[0, 3].Value2 && Constants.Mark.MarkDiv.Equals(targetCell.Offset[0, 3].Value2.ToString()))
                {
                    preDiv = true;
                }
				QC4Common.Util.ExcelUtil.ClearContents(targetCell.EntireRow);
				//targetCell.EntireRow.ClearContents();
                if (preDiv == true) targetCell.Offset[0, 3].Value2 = Constants.Mark.MarkDiv;
            }
            else
            {
                string variableName = targetCell.Value2.ToString();
                if (!Definitions.VariableDictionary.ContainsKey(variableName))
                {
                    //MessageDialog.ErrorOk(AddinResource.FAILED_TO_GET_DATA + "1" );
                    return;
                }
                QuestionSettings questionDetails = Definitions.VariableDictionary[variableName];

                targetCell.Offset[0, 1].Value2 = questionDetails.AnswerType;
                if (questionDetails.CategoryCount == 0)
                {
                    targetCell.Offset[0, 2].Value2 = "";
                }
                else
                {
                    targetCell.Offset[0, 2].Value2 = questionDetails.CategoryCount;
                }
            }

            int preNo = targetCell.Row - Constants.CR.CRRowInputStart;
            cellNo.Value2 = preNo;
        }

        private static Range CellNo(Range targetCell)
        {
            return targetCell.Worksheet.Cells[targetCell.Row, Constants.CR.CRColNo];
        }

        internal static void SheetBeforeDoubleClick(Worksheet sheet, Range target, ref bool cancel)
        {
            cancel = true;
            Range targetCell = target.Cells[1];
            if ((targetCell.Column == Constants.CR.CRColInputStart && targetCell.Row > Constants.CR.CRRowInputStart) ||
          (targetCell.Row == Constants.CR.CRRowInputStart && targetCell.Column > Constants.CR.CRColInputStart))
            {
                switch (targetCell.Value2)
                {
                    case Constants.Mark.MarkDiv:
						//targetCell.ClearContents();
						QC4Common.Util.ExcelUtil.ClearContents(targetCell);
						break;
                    default:
                        targetCell.Value2 = Constants.Mark.MarkDiv;
                        break;
                }
            }
            else if ((targetCell.Row == Constants.CR.CRRowGT) && (targetCell.Column > Constants.CR.CRColInputStart))
            {
                if (AddinResource.MarkBlackCircle == targetCell.Value2)
                {
					//targetCell.ClearContents();
					QC4Common.Util.ExcelUtil.ClearContents(targetCell);
				}
                else
                {
                    targetCell.Value2 = AddinResource.MarkBlackCircle;
                }
            }
            else if (targetCell.Column > Constants.CR.CRColInputStart && targetCell.Row > Constants.CR.CRRowGT)
            {
                if (AddinResource.MarkBlackCircle == targetCell.Value2)
                {
                    targetCell.Value2 = AddinResource.MarkOffCr;
                }
                else
                {
                    targetCell.Value2 = AddinResource.MarkBlackCircle;
                }
            }
            else
            {
                cancel = false;
            }
        }
    }
}
