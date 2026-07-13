using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using Microsoft.Vbe.Interop.Forms;
using Microsoft.VisualBasic.CompilerServices;
using QC4Common.Model;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace ExcelAddIn.Common
{
    class SLChange
    {
        private static CommandButton CmdBtnInsert;
        private static CommandButton CmdBtnDelete;
        private static CommandButton CmdBtnCheck;
        private static CommandButton CmdBtnExec;
        private static CommandButton CmdBtnOpt;
        internal static void SLValueChange(Worksheet sheet, Range target)
        {
            if (target.Columns.Count == ExcelUtil.EndColumn(target.Worksheet)) return;
            if (target.Rows.Count == ExcelUtil.EndRow(target.Worksheet)) return;
            AppHelper appHelper = new AppHelper();
            appHelper.ExcelSet(calculationMode: false);

            foreach (Range targetCell in target)
            {
                //For setting the selected value for all cells with in seleted range
                OnValueChange(sheet, targetCell);

                switch (targetCell.Column)
                {
                    case Constants.SL.SLColVariable:
                        FormatSettingVariable(targetCell);
                        break;
                }
                switch (targetCell.Row)
                {
                    case Constants.SL.SLRow2CRVariable:
                        FormatSettingAxesVariable(targetCell);
                        break;
                }
            }

            appHelper.ExcelReset(true);
        }

        private static void OnValueChange(Worksheet sheet, Range selectedCell)
        {
            if (selectedCell.Column <= Constants.SL.SLColInputStart)
            {
                return;
            }
            if (selectedCell.Row <= Constants.SL.SLRowInputStart)
            {
                return;
            }
			IgnoreError(selectedCell);
            if (!checkAxisVariable(selectedCell))
            {
                return;
            }
            if (!checkTargetVariable(selectedCell))
            {
                return;
            }

            Range startCell = findStartColumn(sheet, selectedCell);

            string setVal = null;
            if (null != selectedCell.Value2 && !string.IsNullOrEmpty(selectedCell.Value2.ToString()))
            {
                setVal = selectedCell.Value2.ToString();
            }

            Range sTSettingTargetRange = CRValidate.findMaxAllocatedRange(sheet, false, sheet.Range[Constants.SL.SLVariableStartAddress],
            sheet.Range[Constants.SL.SLVariableStartAddress].Offset[0, 1],
            sheet.Range[Constants.SL.SLVariableStartAddress].Offset[0, 2],
            sheet.Range[Constants.SL.SLVariableStartAddress].Offset[0, 3],
            sheet.Range[Constants.SL.SLVariableStartAddress].Offset[0, 4]);
            if (sTSettingTargetRange.Row <= Constants.SL.SLRowInputStart)
            {
                return;
            }
            sTSettingTargetRange = sheet.Range[sheet.Cells[startCell.Row, Constants.SL.SLColVariable], sTSettingTargetRange];
            bool breakSep = false;
            foreach (Range targetCell in sTSettingTargetRange.Cells)
            {
                if (checkSepPresent(targetCell))
                {
                    breakSep = true;
                }
                if (null == targetCell.Value2 || string.IsNullOrEmpty(targetCell.Value2.ToString()))
                {
                    if (breakSep)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                Range cRSettingFirst = sheet.Cells[targetCell.Row, startCell.Column];
                Range cRSettingEnd = CRValidate.findMaxAllocatedRange(sheet, true, sheet.Range[Constants.SL.SLAxesVariableStartAddress],
                    sheet.Range[Constants.SL.SLAxesVariableStartAddress].Offset[1, 0]);
                if (cRSettingEnd.Column <= Constants.SL.SLColInputStart)
                {
                    return;
                }
                Range cRSettingEndVal = sheet.Cells[cRSettingFirst.Row, cRSettingEnd.Column];
                Range cRSettingRange = sheet.Range[cRSettingFirst, cRSettingEndVal];

                bool breakDiv = false;
                foreach (Range targetCellSet in cRSettingRange.Cells)
                {
                    if (checkDivPresent(targetCellSet))
                    {
                        breakDiv = true;
                    }
                    if (!checkAxisVariable(targetCellSet))
                    {
                        if (breakDiv)
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    targetCellSet.Value2 = setVal;
					IgnoreError(targetCellSet);

					if (breakDiv)
                    {
                        break;
                    }
                }

                if (breakSep)
                {
                    break;
                }
            }
        }

		private static void IgnoreError(Range range)
		{
			if (range.Errors[XlErrorChecks.xlNumberAsText].Value)
			{
				try
				{
					range.Errors.Item[XlErrorChecks.xlNumberAsText].Ignore = true;
				}
				catch { }
			}
		}

		private static Range findStartColumn(Worksheet sheet, Range selectedCell)
        {
            int colNo = selectedCell.Column;
            int rowNo = selectedCell.Row;
            Range tmp = null;
            int i = -1;
            while (true)
            {
                tmp = selectedCell.Offset[0, i];
                if (tmp.Column <= Constants.SL.SLColInputStart)
                {
                    break;
                }
                if (checkDivPresent(tmp))
                {
                    colNo = tmp.Column + 1;
                    break;
                }
                colNo = tmp.Column;
                i--;
            }

            i = -1;
            while (true)
            {
                tmp = selectedCell.Offset[i, 0];
                if (tmp.Row <= Constants.SL.SLRowInputStart)
                {
                    break;
                }
                if (checkSepPresent(tmp))
                {
                    rowNo = tmp.Row + 1;
                    break;
                }
                rowNo = tmp.Row;
                i--;
            }

            return sheet.Cells[rowNo, colNo];
        }

        private static bool checkSepPresent(Range varSetting)
        {
            Range divCell = varSetting.Worksheet.Cells[varSetting.Row, Constants.SL.SLColInputStart];
            if (null == divCell.Value2)
            {
                return false;
            }
            string gTSettingStr = divCell.Value2.ToString();
            if (gTSettingStr == Constants.Mark.MarkSep)
            {
                return true;
            }
            return false;
        }

        private static bool checkDivPresent(Range varSetting)
        {
            Range divCell = varSetting.Worksheet.Cells[Constants.SL.SLRowInputStart, varSetting.Column];
            if (null == divCell.Value2)
            {
                return false;
            }
            string gTSettingStr = divCell.Value2.ToString();
            if (gTSettingStr == Constants.Mark.MarkDiv)
            {
                return true;
            }
            return false;
        }

        private static bool checkAxisVariable(Range varSetting)
        {
            Range divCell = varSetting.Worksheet.Cells[Constants.SL.SLRow2CRVariable, varSetting.Column];
            if (null == divCell.Value2 || string.IsNullOrEmpty(divCell.Value2.ToString()))
            {
                return false;
            }
            return true;
        }

        private static bool checkTargetVariable(Range varSetting)
        {
            Range divCell = varSetting.Worksheet.Cells[varSetting.Row, Constants.SL.SLColVariable];
            if (null == divCell.Value2 || string.IsNullOrEmpty(divCell.Value2.ToString()))
            {
                return false;
            }
            return true;
        }

        private static void FormatSettingAxesVariable(Range targetCell)
        {
            if (targetCell.Column <= Constants.SL.SLColInputStart)
            {
                return;
            }
            if (null == targetCell.Value2 || string.IsNullOrEmpty(targetCell.Value2.ToString()))
            {
                //targetCell.Worksheet.Range[targetCell.Offset[-1, 0], targetCell.Offset[2, 0]].ClearContents();
				QC4Common.Util.ExcelUtil.ClearContents(targetCell.Worksheet.Range[targetCell.Offset[-1, 0], targetCell.Offset[2, 0]]);
				Range endCell = targetCell.Worksheet.Cells[ExcelUtil.EndRow(targetCell.Worksheet), targetCell.Column];
                Range ItemArea = targetCell.Worksheet.Range[targetCell.Offset[4, 0], endCell];
                ItemArea.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;
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
                Range divRange = findDivRegionAxesVaraible(targetCell, targetCell);
                divRange = divRange.Offset[-4];
                Range disabled = null;
                if (divRange.Count == 1)
                {
                    if (AddinResource.MarkOFF == divRange.Text)
                    {
                        disabled = divRange;
                    }
                }
                else
                {
                    disabled = divRange.Find(AddinResource.MarkOFF, Type.Missing, Type.Missing, Type.Missing, XlSearchOrder.xlByRows,
                    XlSearchDirection.xlNext, Type.Missing, Type.Missing, Type.Missing);
                }
                Range endCell = targetCell.Worksheet.Cells[ExcelUtil.EndRow(targetCell.Worksheet), targetCell.Column];
                Range ItemArea = targetCell.Worksheet.Range[targetCell.Offset[4, 0], endCell];
                if (null == disabled)
                {
                    targetCell.Offset[-1].Value = AddinResource.MarkWhiteCircle;
                    ItemArea.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;
                }
                else
                {
                    targetCell.Offset[-1].Value = AddinResource.MarkOFF;
                    ItemArea.Interior.Color = System.Drawing.Color.FromArgb(217, 217, 217); ;
                }
            }
        }

        internal static void bindButtons(Worksheet sht)
        {
            foreach (OLEObject oleObject in sht.OLEObjects())
            {
                switch (oleObject.Name)
                {
                    case Constants.SL.SLBUTTONINSERT:
                        CmdBtnInsert = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        CmdBtnInsert.Click += () => SLInsertDel(sht);
                        break;
                    case Constants.SL.SLBUTTONDELETE:
                        CmdBtnDelete = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        CmdBtnDelete.Click += () => SLInsertDel(sht, true);
                        break;
                    case Constants.SL.SLBUTTONCHECK:
                        CmdBtnCheck = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        bool cancel = false;
                        CmdBtnCheck.Click += () => ValidateSL(null, ref cancel);
                        break;
                    case Constants.SL.SLBUTTONEXEC:
                        CmdBtnExec = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        CmdBtnExec.Click += () => crossExec(sht);
                        break;
                    case Constants.SL.SLBUTTONOPT:
                        CmdBtnOpt = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        CmdBtnOpt.Click += () => showOption(sht);
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
        private static string msgstr = "SUMMARYTEXEC";
        private static string msgstrOpt = "SUMMARYTOPT";


        private const int HWND_BROADCAST = 0xffff;

        private static void showOption(Worksheet sht)
        {
            //bool cancel = false;
            //if (ValidateSL(null, ref cancel, true))
            //{
                int msg = RegisterWindowMessage(msgstrOpt);
                if (msg == 0)
                {
                    MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                }

                //SendNotifyMessage(HWND_BROADCAST, msg, 4848484, 8484865);

                SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
            //}
        }

        private static void crossExec(Worksheet sht)
        {
            sht.Application.Cursor = XlMousePointer.xlWait;
            bool cancel = false;
            if (ValidateSL(null, ref cancel, true))
            {
                int msg = RegisterWindowMessage(msgstr);
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

        public static bool ValidateSL(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault, bool exec = false)
        {
            QC4Common.Common.ReturnClass result = QC4Common.Common.SLValidate.validate(SLValidate.getSLSheet(), Definitions.VariableDictionary);
            if (null != result && !result.Result)
            {
                Range value = (Range)result.Value;
                if (null != result.Msg)
                {
                    MessageDialog.ErrorOk(result.Msg);
                }
                else
                {
                    MessageDialog.ErrorOk(string.Format(AddinResource.VALIDATION_FAIL_SHEET, value.Row, value.Column));
                }
                value.Select();
                return false;
            }
            else
            {
                result = QC4Common.Common.SLValidateName.validateName(SLValidate.getSLSheet());
                if (null != result && !result.Result)
                {
                    Range value = (Range)result.Value;
                    value.Select();
                    return false;
                }
                else
                {
                    if (!exec)
                        MessageDialog.Info(AddinResource.VALIDATION_SUCCESS_SHEET);
                }
                return true;
            }
        }

        public static void SLInsertDel(Worksheet sheet, bool deleteMode = false)
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
                        if (SLRowColInsert(targetSheet, XlRowCol.xlColumns, range.Column, range.Columns.Count, 8) == true)
                        {
                            targetSheet.Range[targetAddress].Offset[0, offsetCount].Select();
                        }
                    }
                    else
                    {
                        SLRowColDelete(targetSheet, XlRowCol.xlColumns, range.Column, range.Columns.Count, 8);
                    }

                }
                else
                {
                    if (deleteMode == false)
                    {
                        int offsetCount = range.Rows.Count;
                        if (SLRowColInsert(targetSheet, XlRowCol.xlRows, range.Row, range.Rows.Count, 8) == true)
                        {
                            SLColumnAssign(targetSheet);
                            targetSheet.Range[targetAddress].Offset[offsetCount, 0].Select();
                        }
                    }
                    else
                    {
                        if (SLRowColDelete(targetSheet, XlRowCol.xlRows, range.Row, range.Rows.Count, 8) == true)
                        {
                            SLColumnAssign(targetSheet, true);
                        }
                    }
                }
            }
            finally
            {
                appHelper.ExcelReset();
            }


        }

        private static void SLColumnAssign(Worksheet targetSheet, bool emptyPermit = false)
        {

            int endRow = ExcelUtil.EndxlUp(targetSheet.Cells[1, 2]).Row;
            if (emptyPermit == false)
            {
                endRow = Math.Max(endRow, ExcelUtil.EndxlUp(targetSheet.Cells[1, 3]).Row);
            }

            if (endRow < Constants.SL.SLRowInputStart + 1) return;
            dynamic colArray = targetSheet.Range[targetSheet.Cells[Constants.SL.SLRowInputStart + 1, 2], targetSheet.Cells[endRow, 2]].Value2;
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
            targetSheet.Range[targetSheet.Cells[Constants.SL.SLRowInputStart + 1, 2], targetSheet.Cells[endRow, 2]].Value2 = colArray;
        }

        private static bool SLRowColDelete(Worksheet targetSheet, XlRowCol rowCol, int targetPlace, int deleteNum, int minCount = 0)
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

        private static bool SLRowColInsert(Worksheet targetSheet, XlRowCol rowCol, int targetPlace, int insertNum, int minCount = 0)
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

        private static void FormatSettingVariable(Range targetCell)
        {
            Range cellNo = CellNo(targetCell);
            if (targetCell.Row <= Constants.SL.SLRowInputStart)
            {
                return;
            }
            if (null == targetCell.Value2 || string.IsNullOrEmpty(targetCell.Value2.ToString()))
            {
                bool preDiv = false;
                if (null != targetCell.Offset[0, 3].Value2 && Constants.Mark.MarkSep.Equals(targetCell.Offset[0, 3].Value2.ToString()))
                {
                    preDiv = true;
                }
				//targetCell.EntireRow.ClearContents();
				QC4Common.Util.ExcelUtil.ClearContents(targetCell.EntireRow);
				Range endCell = targetCell.Worksheet.Cells[targetCell.Row, ExcelUtil.EndColumn(targetCell.Worksheet)];
                Range ItemArea = targetCell.Worksheet.Range[targetCell.Offset[0, 4], endCell];
                ItemArea.Validation.Delete();
                if (preDiv == true) targetCell.Offset[0, 3].Value2 = Constants.Mark.MarkSep;
            }
            else
            {
                string variableName = targetCell.Value2.ToString();
                if (!Definitions.VariableDictionary.ContainsKey(variableName))
                {
                    //MessageDialog.ErrorOk(AddinResource.FAILED_TO_GET_DATA);
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

                Range endCell = targetCell.Worksheet.Cells[targetCell.Row, ExcelUtil.EndColumn(targetCell.Worksheet)];
                Range ItemArea = targetCell.Worksheet.Range[targetCell.Offset[0, 4], endCell];
                CommonFunctions.CellFormatInitialize(ItemArea, false);
                String catList = SLValidate.getValueList(questionDetails);
                if (catList.Length > 255)//[Redmine id: 189027]
                {
                    int cateCnt = questionDetails.CategoryCount;
                    Microsoft.Office.Interop.Excel.Worksheet settingssheet = ExcelUtil.GetWorkSheetByCodeName(targetCell.Application.ActiveWorkbook, Constants.SheetCodeName.Setting);
                    catList = "= INDIRECT(" + "\"" + settingssheet.Name + "!$A$1:$A$" + cateCnt + "\")";
                }
                CommonFunctions.CellFormatSetting(ItemArea, catList, "");
                //Range firstSep = findFirstSepVariable(targetCell);

                //if (targetCell.Row == firstSep.Row &&
                //    (targetCell.Offset[0, -1].Value2 == null || String.IsNullOrEmpty(targetCell.Offset[0, -1].Value2.ToString())))
                //{
                //    string variable = questionDetails.Variable;
                //    variable = Regex.Replace(variable, @"s\d*$", "", RegexOptions.IgnoreCase);
                //    variable = "N" + variable + "_MT";
                //    targetCell.Offset[0, -1].Value2 = generateName(targetCell.Offset[0, -1], variable);
                //}

            }

            int preNo = targetCell.Row - Constants.SL.SLRowInputStart;
            cellNo.Value2 = preNo;
            setPreviousNumberIfEmpty(targetCell);
        }

        private static void setPreviousNumberIfEmpty(Range targetCell)
        {
            Range changeCell = targetCell.Worksheet.Cells[targetCell.Row, targetCell.Column];
            while (true)
            {
                Range prevTargetCell = changeCell.Offset[-1, 0];
                if (prevTargetCell.Row <= Constants.SL.SLRowInputStart)
                {
                    break;
                }
                int preNo = prevTargetCell.Row - Constants.SL.SLRowInputStart;
                Range cellNo = CellNo(prevTargetCell);
                if (null != cellNo.Value2 && !string.IsNullOrEmpty(cellNo.Value2.ToString()))
                {
                    break;
                }
                cellNo.Value2 = preNo;

                changeCell = prevTargetCell;
            }
        }

        private static string generateName(Range targetCell, string variable)
        {
            int i = 1;
            String finalVariable = variable;
            while (true)
            {
                Range startSetting = targetCell.EntireColumn.Find(finalVariable, targetCell, Type.Missing,
                    Type.Missing, XlSearchOrder.xlByColumns, XlSearchDirection.xlNext, Type.Missing, Type.Missing, Type.Missing);
                if (startSetting == null)
                {
                    break;
                }
                finalVariable = i + variable;
                i++;
            }
            return finalVariable;
        }

        private static Range CellNo(Range targetCell)
        {
            return targetCell.Worksheet.Cells[targetCell.Row, Constants.SL.SLColNo];
        }

        internal static void SheetBeforeDoubleClick(Worksheet sheet, Range target, ref bool cancel)
        {
            cancel = true;
            Range targetCell = target.Cells[1];
            if ((targetCell.Column == Constants.SL.SLColInputStart && targetCell.Row > Constants.SL.SLRowInputStart))
            {
                switch (targetCell.Value2)
                {
                    case Constants.Mark.MarkSep:
						//targetCell.ClearContents();
						QC4Common.Util.ExcelUtil.ClearContents(targetCell);
						break;
                    default:
                        targetCell.Value2 = Constants.Mark.MarkSep;
                        break;
                }
            }
            else if ((targetCell.Row == Constants.SL.SLRowInputStart && targetCell.Column > Constants.SL.SLColInputStart))
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
            else if (targetCell.Column > Constants.SL.SLColInputStart && targetCell.Row == Constants.SL.SLRowOuputEnable)
            {
                if (targetCell.Offset[1, 0].Value2 == null)
                {
                    return;
                }
                Range divRange = findDivRegionInpuEnable(targetCell, targetCell.Offset[1, 0]);
                Range divNonEmptyRange = null;
                Range ItemAreaNonEmptyRange = null;
                foreach (Range divCell in divRange.Cells)
                {
                    if (divCell.Offset[-3, 0].Value2 != null)
                    {
                        Range endCell = targetCell.Worksheet.Cells[ExcelUtil.EndRow(divCell.Worksheet), divCell.Column];
                        Range ItemArea = targetCell.Worksheet.Range[divCell.Offset[1, 0], endCell];
                        if (null == divNonEmptyRange)
                        {
                            divNonEmptyRange = divCell.Offset[-4, 0];
                            ItemAreaNonEmptyRange = ItemArea;
                        }
                        else
                        {
                            divNonEmptyRange = targetCell.Application.Union(divNonEmptyRange, divCell.Offset[-4, 0]);
                            ItemAreaNonEmptyRange = targetCell.Application.Union(ItemAreaNonEmptyRange, ItemArea);
                        }

                    }
                }

                if (AddinResource.MarkWhiteCircle == targetCell.Value2 && null != divNonEmptyRange)
                {
                    divNonEmptyRange.Value2 = AddinResource.MarkOFF;
                    ItemAreaNonEmptyRange.Interior.Color = System.Drawing.Color.FromArgb(217, 217, 217); ;
                }
                else if (null != divNonEmptyRange)
                {
                    divNonEmptyRange.Value2 = AddinResource.MarkWhiteCircle;
                    ItemAreaNonEmptyRange.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;
                }
            }
            else
            {
                cancel = false;
            }
        }

        private static Range findDivRegionInpuEnable(Range targetCell, Range variableCell)
        {
            targetCell = targetCell.Offset[4, 0];
            return findDivRegion(targetCell, variableCell);
        }

        public static Range findDivRegionAxesVaraible(Range targetCell, Range variableCell)
        {
            targetCell = targetCell.Offset[3, 0];
            return findDivRegion(targetCell, variableCell);
        }

        private static Range findDivRegion(Range targetCell, Range variableCell = null)
        {
            Range prevCell = targetCell.Offset[0, -1];

            Range endSetting = prevCell.Find(Constants.Mark.MarkDiv, prevCell, Type.Missing, Type.Missing, XlSearchOrder.xlByRows,
                XlSearchDirection.xlNext, Type.Missing, Type.Missing, Type.Missing);
            if (endSetting == null || endSetting.Row != targetCell.Row || endSetting.Column < targetCell.Column)
            {
                if (variableCell != null)
                {
                    Range endxlRight = ExcelUtil.EndxlRight(variableCell);
                    if (endxlRight.Column < targetCell.Column)
                    {
                        endxlRight = targetCell;
                    }
                    endSetting = targetCell.Worksheet.Cells[targetCell.Row, endxlRight.Column];
                }
                else
                {
                    endSetting = targetCell.Worksheet.Cells[targetCell.Row, ExcelUtil.EndColumn(targetCell.Worksheet)];
                }
            }


            Range startSetting = targetCell.Find(Constants.Mark.MarkDiv, targetCell, Type.Missing, Type.Missing, XlSearchOrder.xlByRows,
                XlSearchDirection.xlPrevious, Type.Missing, Type.Missing, Type.Missing);
            if (startSetting == null || startSetting.Row != targetCell.Row || startSetting.Column >= targetCell.Column)
            {
                startSetting = targetCell.Worksheet.Cells[targetCell.Row, Constants.SL.SLColInputStart + 1];
            }
            else
            {
                startSetting = startSetting.Offset[0, 1];
            }

            return targetCell.Worksheet.Range[startSetting, endSetting];
        }

        private static Range findFirstSepVariable(Range targetCell)
        {
            targetCell = targetCell.Offset[0, 3];
            Range startSetting = targetCell.Find(Constants.Mark.MarkSep, targetCell, Type.Missing, Type.Missing, XlSearchOrder.xlByColumns,
                XlSearchDirection.xlPrevious, Type.Missing, Type.Missing, Type.Missing);
            if (startSetting == null || startSetting.Column != targetCell.Column || startSetting.Row >= targetCell.Row)
            {
                startSetting = targetCell.Worksheet.Cells[Constants.SL.SLRowInputStart, targetCell.Column];
            }

            Range firstSep = startSetting.Offset[0, -3].Find("*", startSetting.Offset[0, -3], Type.Missing, Type.Missing, XlSearchOrder.xlByColumns,
               XlSearchDirection.xlNext, Type.Missing, Type.Missing, Type.Missing);
            if (null == firstSep || firstSep.Row >= targetCell.Row)
            {
                firstSep = targetCell;
            }
            return firstSep;

        }
    }
}
