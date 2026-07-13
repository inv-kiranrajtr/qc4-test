using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Tools = Microsoft.Office.Tools.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Office = Microsoft.Office.Core;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Vbe.Interop.Forms;
using ExcelAddIn.DB;
using System.Collections;

using ExcelAddIn.Sheets;
using QC4Common.QS;
using System.Runtime.InteropServices;

namespace ExcelAddIn.Common
{
    public static class Change
    {
        private static CommandButton QsCmdBtnMenu;
        private static CommandButton QsCmdBtnInsert;
        private static CommandButton QsCmdBtnCreate;
        private static CommandButton QsCmdBtnChk;

        private static bool BindFlag = true;

        internal static void ExecToggle(Worksheet sheet, Range target, bool dClickFlg = true)
        {
            Application application = sheet.Application;
            bool eEFlag = application.EnableEvents;
            application.EnableEvents = false;

            Range Exec_Cell = GetExecCell(target);


            if (Exec_Cell.End[XlDirection.xlToRight].Column == ExcelUtil.EndColumn(sheet))
            {

                if (Exec_Cell.Value2 != "")
                {
                    //Exec_Cell.ClearContents();
					QC4Common.Util.ExcelUtil.ClearContents(Exec_Cell);
                }
            }
            else
            {
                if (null == Exec_Cell.Value2 || string.IsNullOrEmpty(Exec_Cell.Value2.ToString())
                    || (AddinResource.MarkOFF == Exec_Cell.Value2))
                {
                    Exec_Cell.Value2 = AddinResource.MarkWhiteCircle;
                }
                else
                {
                    if (dClickFlg)
                    {
                        Exec_Cell.Value2 = AddinResource.MarkOFF;
                    }
                }
            }
            application.EnableEvents = eEFlag;
        }

        private static Range GetExecCell(Range target)
        {
            int col = 2;
            Worksheet targetSheet = target.Worksheet;
            if (targetSheet.CodeName.Equals(Common.Constants.SheetType.GTCounting))
            {
                col = 2;
            }
            return targetSheet.Cells[target.Row, col];
        }

        internal static void SheetBeforeDoubleClick(Worksheet sheet, Range target)
        {
            if (IsExecColum(sheet, target))
            {
                ExecToggle(sheet, target);
            }
        }

        private static bool IsExecColum(Worksheet sheet, Range target)
        {
            if (GetExecCell(target).Column == target.Column)
            {
                return true;
            }
            return false;
        }

        internal static void bindButtons(Workbook workbook)
        {
            if (!BindFlag)
            {
                return;
            }

            if (workbook != null)
            {
                foreach (Worksheet sht in workbook.Worksheets)
                {
                    switch (sht.CodeName)
                    {
                        case Constants.SheetType.GTCounting:
                            GTSheetChange.bindButtons(workbook, sht);
                            break;
                        case Constants.SheetType.sh_QuesSetting:
                            QSBindButton(sht);
                            break;
                        case Constants.SheetType.sh_CrossCounting:
                            CrossChange.bindButtons(sht);
                            break;
                        case Constants.SheetType.sh_SummaryList:
                            SLChange.bindButtons(sht);
                            break;
                        case Constants.SheetType.SH_FAList:
                            //FAList.bindButtons(sht);

                            break;
                        default:
                            break;
                    }

                }
                BindFlag = false;
            }
        }

        internal static void QSBindButton(Worksheet sht)
        {
            foreach (OLEObject oleObject in sht.OLEObjects())
            {
                switch (oleObject.Name)
                {
                    case Common.Constants.QS.QsButtonMenu:
                        QsCmdBtnMenu = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        QsCmdBtnMenu.Click += () => QsMenuClick(sht);
                        break;
                    case Common.Constants.QS.QSButtonInsert:
                        QsCmdBtnInsert = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        QsCmdBtnInsert.Click += () => QSInsertDeleteClick(sht);
                        break;
                    case Common.Constants.QS.QSButtonCreate:
                        QsCmdBtnCreate = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        QsCmdBtnCreate.Click += () => QsCreateClick(sht);
                        break;
                    case Common.Constants.QS.QSButtonCheck:
                        QsCmdBtnChk = (CommandButton)NewLateBinding.LateGet(sht, null, oleObject.Name, null, null, null, null);
                        QsCmdBtnChk.Click += () => QsValidateClick();
                        break;
                    default:
                        break;
                }
            }
        }



        internal static void InsertDeleteClick(Worksheet sheet, bool deleteMode = false)
        {
            Application application = sheet.Application;

            if (!(application.Selection is Range))
            {
                return;
            }

            Range range = application.Selection;

            if (!deleteMode && range.Areas.Count > 1)
            {
                MessageDialog.ErrorOk(AddinResource.CANNOT_SPECIFY_MULTIPLE_RANGE);
                return;
            }
            String preAddress = range.Address;
            Range targetRange = range.EntireRow;
            AppHelper appHelper = new AppHelper();
            appHelper.ExcelSet();

            switch (sheet.CodeName)
            {
                case Common.Constants.SheetType.GTCounting:
                    if (deleteMode == true)
                    {
                        RowDeleteExec(targetRange, Common.Constants.GT.GtRowDataStart);
                    }
                    else
                    {
                        RowInsertExec(targetRange, Common.Constants.GT.GtRowDataStart);
                    }
                    break;
                case Common.Constants.SheetType.sh_QuesSetting:
                    if (deleteMode == true)
                    {
                        RowDeleteExec(targetRange, Common.Constants.QS.QsRowDataStart);
                    }
                    else
                    {
                        RowInsertExec(targetRange, Common.Constants.QS.QsRowDataStart);
                    }
                    break;
                default:
                    break;
            }
            sheet.Range[preAddress].Select();
            appHelper.ExcelReset();
        }

        private static void RowInsertExec(Range targetRange, int minRow)
        {
            if (null == targetRange)
            {
                return;
            }
            if (targetRange.Row < minRow)
            {
                return;
            }
            targetRange.EntireRow.Insert(XlDirection.xlDown);
            Application application = targetRange.Application;
            dynamic targetParent = targetRange.Parent;
            int col = targetParent.Range["A1"].SpecialCells(XlCellType.xlCellTypeLastCell).Row + 1;
            col = (int)application.WorksheetFunction.Max(col, minRow);
            targetParent.Rows[col].Copy();
            targetRange.Offset[-(targetRange.Rows.Count)].PasteSpecial(XlPasteType.xlPasteAll);
            application.CutCopyMode = (XlCutCopyMode)0;

        }

        private static void RowDeleteExec(Range targetRange, int minRow)
        {

            if (null == targetRange)
            {
                return;
            }
            if (targetRange.Row < minRow)
            {
                return;
            }
            targetRange = RangeAbstract(targetRange, minRow);
            if (null == targetRange)
            {
                return;
            }
            targetRange.EntireRow.Delete();
        }
        //>>>>>>> Stashed changes

        private static Range RangeAbstract(Range targetRange, int minRow)
        {
            Worksheet targetSheet = targetRange.Worksheet;
            Application targetApp = Globals.ThisAddIn.Application;
            return targetApp.Intersect(targetRange, targetSheet.Range[minRow + ":" + ExcelUtil.EndRow(targetSheet)]);
        }

        internal static void ValidateGT()
        {
            ReturnClass result = GTValidate.validate();
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
            }
            else
            {
                MessageDialog.Info(AddinResource.VALIDATION_SUCCESS_SHEET);
            }
        }

        internal static void QSInsertDeleteClick(Worksheet sheet, bool delete = false)
        {
            Application application = sheet.Application;
            if (Definitions.VariableEditMode)
            {
                return;
            }
            if (!(application.Selection is Range))
            {
                return;
            }

            Range range = application.Selection;
            if (range.Row < Constants.QS.QsRowDataStart)
            {
                return;
            }

            for (int i = 0; i < range.Rows.Count; i++)
            {
                if (!delete)
                {
                    Definitions.RowVariableList.Insert(range.Offset[i].Row - 4, "");
                }
                else
                {
                    QuestionSettingsUtil.DeleteQuestionSetting(range.Offset[i].Row);
                }
            }
            InsertDeleteClick(sheet, delete);
        }

        private static void QsCreateClick(Worksheet sheet)
        {
            if (Definitions.VariableEditMode)
            {
                MessageDialog.Warning(AddinResource.ALERT_VARIABLE_EDIT_MODE);
                return;
            }
            QuestionnaireCreator qc = new QuestionnaireCreator();
            Console.WriteLine("---------- START " + DateTime.Now);
            qc.CreateQuesionnaire();
            Console.WriteLine("---------- END " + DateTime.Now);
            //MessageDialog.Info("Create Click");
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(int hWnd, int Msg, int wParam, int lParam);

        private static string msgstrExec = "MAINCLICK";
        private const int HWND_BROADCAST = 0xffff;
        private static int hWnd_Processhandle = 0;

        private static void QsMenuClick(Worksheet sheet)
        {
            sheet.Application.Cursor = XlMousePointer.xlWait;
            int msg = RegisterWindowMessage(msgstrExec);
            if (msg == 0)
            {
                MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
            }
            SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
            sheet.Application.Cursor = XlMousePointer.xlDefault;
        }

        internal static void QsValidateClick()
        {
            ReturnClass result = new QSValidate(Globals.ThisAddIn.Application.ActiveWorkbook).QuestionConfigurationCheck();
            if (!result.Result)
            {
                Range value = (Range)result.Value;
                value.Select();
                MessageDialog.ErrorOk(result.Msg);
            }
            else
            {
                MessageDialog.Info(AddinResource.VALIDATION_SUCCESS_SHEET);
            }
        }

        //internal static void InsertJumpAndWidth()
        //{
        //	Worksheet sheet = ExcelUtil.GetWorksheetByIndex(1);
        //	Range range = sheet.get_Range(Constants.QS.QsJumpCell);
        //	range.EntireColumn.ColumnWidth = 4.71;
        //	range.Value = Constants.QS.QsJump;
        //	range.NumberFormat = "@";
        //	range.Font.Underline = true;
        //	range.VerticalAlignment = XlVAlign.xlVAlignCenter;
        //	range.Font.Color = Constants.Color.Blue;

        //	Range r = sheet.get_Range(Constants.QS.QsColAfterJumpStart, Constants.QS.QsColAfterJumpEnd);
        //	r.EntireColumn.Hidden = false;

        //	sheet.Cells[sheet.Application.ActiveWindow.ScrollRow, 12].Select();
        //}

        //internal static void JumpButtonEvent(Worksheet sheet, Range target)
        //{
        //	if (sheet.CodeName != Constants.SheetType.sh_QuesSetting) return;
        //	if (target.Cells.Count != 1) return;
        //	if (target.Column != 10) return;
        //	if (target.Row != 2) return;

        //	if (sheet.Application.ActiveWindow.ScrollColumn < sheet.Application.Columns[1014].Column)
        //	{
        //		sheet.Application.ActiveWindow.ScrollColumn = sheet.Application.Columns[1014].Column;
        //		sheet.Application.EnableEvents = false;
        //		sheet.Cells[sheet.Application.ActiveWindow.ScrollRow, 1014].Select();
        //		sheet.Application.EnableEvents = true;
        //	}
        //	else
        //	{
        //		sheet.Application.ActiveWindow.ScrollColumn = 1;
        //		sheet.Application.EnableEvents = false;
        //		sheet.Cells[sheet.Application.ActiveWindow.ScrollRow, 11].Select();
        //		sheet.Application.EnableEvents = true;
        //	}
        //}


        public static void UpdateListView()
        {
            //question content check and dbcheck need to be here

            Worksheet sheet = ExcelUtil.GetWorksheetByName(Constants.SheetType.sh_ListView);
            sheet.Cells[1, 1].CurrentRegion.Offset[1].ClearContents();

            ArrayList saArray = new ArrayList();
            ArrayList maArray = new ArrayList();
            ArrayList nArray = new ArrayList();
            ArrayList samaArray = new ArrayList();
            ArrayList sanArray = new ArrayList();
            ArrayList manArray = new ArrayList();
            ArrayList samanArray = new ArrayList();
            ArrayList faArray = new ArrayList();
            ArrayList allArray = new ArrayList();
            ArrayList allDArray = new ArrayList();
            String[,] outPutArray = new string[Definitions.RowVariableList.Count, 10];

            foreach (string variable in Definitions.RowVariableList)
            {
                string ansType = "";
                if (Definitions.VariableDictionary.ContainsKey(variable))
                {
                    ansType = Definitions.VariableDictionary[variable].AnswerType;
                }

                switch (ansType)
                {
                    case Constants.AnswerType.SA:
                        saArray.Add(variable);
                        samaArray.Add(variable);
                        sanArray.Add(variable);
                        samanArray.Add(variable);
                        allArray.Add(variable);
                        allDArray.Add(variable);
                        break;

                    case Constants.AnswerType.MA:
                        maArray.Add(variable);
                        samaArray.Add(variable);
                        manArray.Add(variable);
                        samanArray.Add(variable);
                        allArray.Add(variable);
                        allDArray.Add(variable);
                        break;

                    case Constants.AnswerType.N:
                        nArray.Add(variable);
                        sanArray.Add(variable);
                        manArray.Add(variable);
                        samanArray.Add(variable);
                        allArray.Add(variable);
                        allDArray.Add(variable);
                        break;

                    case Constants.AnswerType.FA:
                        faArray.Add(variable);
                        allArray.Add(variable);
                        allDArray.Add(variable);
                        break;

                    case Constants.AnswerType.D:
                        allDArray.Add(variable);
                        break;
                }
            }
            ListOutPutAdd(outPutArray, saArray, 0);
            ListOutPutAdd(outPutArray, maArray, 1);
            ListOutPutAdd(outPutArray, nArray, 2);
            ListOutPutAdd(outPutArray, samaArray, 3);
            ListOutPutAdd(outPutArray, sanArray, 4);
            ListOutPutAdd(outPutArray, manArray, 5);
            ListOutPutAdd(outPutArray, samanArray, 6);
            ListOutPutAdd(outPutArray, faArray, 7);
            ListOutPutAdd(outPutArray, allArray, 8);
            ListOutPutAdd(outPutArray, allDArray, 9);

            sheet.get_Range("A2", "J" + (allDArray.Count + 1)).Value = outPutArray;
        }

        private static void ListOutPutAdd(String[,] outPutArray, ArrayList list, int n)
        {
            for (int i = 0; i < list.Count; i++)
            {
                outPutArray[i, n] = list[i].ToString();
            }
        }

        public static bool ValidateCRTab(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault, bool exec = false)
        {
            QC4Common.Common.ReturnClass result = QC4Common.Common.CRValidate.validate(CRValidate.getCRSheet(), Definitions.VariableDictionary);
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
                if (!exec)
                    MessageDialog.Info(AddinResource.VALIDATION_SUCCESS_SHEET);
                return true;
            }
        }

        public static bool ValidateGTTab(Microsoft.Office.Core.CommandBarButton Ctrl, bool exec = false, Microsoft.Office.Interop.Excel.Sheets sHeets = null,bool isFromSTD=false,IntPtr? ptr=null)
        {
            if (sHeets != null) // Call From Launcher
            {
                Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(sHeets.Application.ActiveWorkbook);
            }

            ReturnClass result = GTValidate.validate(sHeets, isFromSTD);
            if (null != result && !result.Result)
            {
                Range value = (Range)result.Value;
                if (null != result.Msg)
                {
                    if(isFromSTD)
                        MessageDialog.ErrorOk(result.Msg, (IntPtr)ptr);
                    else if (sHeets != null) // Call From Launcher
                        QC4Common.Common.MessageDialog.ShowMessageOnWorkBook(result.Msg, QC4Common.Common.Constants.MessageType.ErrorOk, sHeets.Application.ActiveWorkbook);
                    else
                        MessageDialog.ErrorOk(result.Msg);
                }
                else
                {
                    if (sHeets != null) // Call From Launcher
                        QC4Common.Common.MessageDialog.ShowMessageOnWorkBook(string.Format(AddinResource.VALIDATION_FAIL_SHEET, value.Row, value.Column), QC4Common.Common.Constants.MessageType.ErrorOk, sHeets.Application.ActiveWorkbook);
                    else
                        MessageDialog.ErrorOk(string.Format(AddinResource.VALIDATION_FAIL_SHEET, value.Row, value.Column));
                }
                if (!isFromSTD)
                    value.Select();
                return false;
            }
            else
            {
                if (!exec)
                    MessageDialog.Info(AddinResource.VALIDATION_SUCCESS_SHEET);
                return true;
            }
        }

    }
}
