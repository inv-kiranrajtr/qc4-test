using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;
using ExcelAddIn.Common;
using ExcelAddIn.Sheets;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using log4net;
using System.Reflection;

using CommonFunctions = QC4Common.Common.CommonFunctions;
using Macromill.QCWeb.COMOperate;
using System.Diagnostics;

namespace ExcelAddIn
{
    public partial class QC4Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        public static QC4Ribbon Rbn = null;
        public static bool isRibbonLoaded;
        public static string sheetName;
        public static bool IsEditMode = false;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private void QC4Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            if (isRibbonLoaded)
            {
                Rbn = this;
                QC4.Visible = true;
                QC4.RibbonUI.ActivateTab("QC4");//TabID
                checkBoxSort.Checked = true;
                Menu.Label = AddinResource.Menu;
                buttonMenu.Label = AddinResource.buttonMenu;
                Common.Label = AddinResource.Common;
                buttonInsert.Label = AddinResource.buttonInsert;
                buttonDelete.Label = AddinResource.buttonDelete;
                buttonCheck.Label = AddinResource.buttonCheck;
                QSettings1.Label = AddinResource.QSettings1;
                buttonVEM.Image = AddinResource.edit_start_hover;
                buttonVEM.Label = AddinResource.QS_START_VARIABLE_EDIT_MODE;
                buttonEQ.Label = AddinResource.buttonEQ;
                QSettings2.Label = AddinResource.QSettings2;
                buttonCheckQS.Label = AddinResource.buttonCheckQS;
                buttonJump.Label = AddinResource.buttonJump;
                DProcess1.Label = AddinResource.DProcess1;
                buttonUp.Label = AddinResource.buttonUp;
                buttonDown.Label = AddinResource.buttonDown;
                buttonCopy.Label = AddinResource.buttonCopy;
                buttonPaste.Label = AddinResource.buttonPaste;
                buttonExecuteDP.Label = AddinResource.buttonExecuteDP;
                DProcess2.Label = AddinResource.DProcess2;
                checkBoxCross.Label = AddinResource.checkBoxCross;
                checkBoxList.Label = AddinResource.checkBoxList;
                CTab1.Label = AddinResource.CTab1;
                buttonOptionsCT.Label = AddinResource.buttonOptionsCT;
                buttonCT.Label = AddinResource.buttonCT;
                buttonChart.Label = AddinResource.buttonChart;
                CTab2.Label = AddinResource.CTab2;
                buttonStatus.Label = AddinResource.buttonStatus;
                labelOutput.Label = AddinResource.labelOutput;
                checkBoxCols.Label = AddinResource.checkBoxCols;
                checkBoxRows.Label = AddinResource.checkBoxRows;
                checkBoxRows.Checked = true;
                Summary.Label = AddinResource.Summary;
                buttonOptionsSummary.Label = AddinResource.buttonOptionsSummary;
                buttonOutput.Label = AddinResource.buttonOutput;
                GT1.Label = AddinResource.GT1;
                buttonExecuteGT.Label = AddinResource.buttonExecuteGT;
                buttonOptionsGT.Label = AddinResource.buttonOptionsGT;
                buttonAutoSettings.Label = AddinResource.buttonAutoSettings;
                GT2.Label = AddinResource.GT2;
                checkBoxLevel1.Label = AddinResource.checkBoxLevel1;
                checkBoxLevel5.Label = AddinResource.checkBoxLevel5;
                checkBoxLevel10.Label = AddinResource.checkBoxLevel10;
                labelAlert.Label = AddinResource.labelAlert;
                FA1.Label = AddinResource.FA1;
                buttonExecute.Label = AddinResource.buttonExecute;
                FA2.Label = AddinResource.FA2;
                checkBoxSort.Label = AddinResource.checkBoxSort; 
                buttonUndoDP.Label = AddinResource.BTN_UNDO;
                InitSigCheckBoxValues();

                if (ThisAddIn.HWND_MAIN == 0)
                {
                    ThisAddIn.HWND_MAIN = ExcelAddIn.Common.Util.GetWIndowHandleFromSettings();
                }
            }
            else QC4.Visible = false;
        }



        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(int hWnd, int Msg, int wParam, int lParam);
        private const int HWND_BROADCAST = 0xffff;

        private void buttonMenu_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {

                int msg;
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    Excel.Worksheet sheet = (Excel.Worksheet)Globals.ThisAddIn.Application.ActiveSheet;
                    sheet.Application.Cursor = Excel.XlMousePointer.xlWait;
                    if (sheet.CodeName == Constants.SheetCodeName.QuestionSetting)
                    {
                        ReturnClass result = QuestionSettingsUtil.MoveFromQs(Globals.ThisAddIn.Application.ActiveWorkbook, sheet, false, false);
                        if (!result.Result)
                        {
                            sheet.Application.Cursor = Excel.XlMousePointer.xlDefault;
                            if (result.Value != null)
                            {
                                Excel.Range range = (Excel.Range)result.Value;
                                range.Select();
                            }
                            MessageDialog.ErrorOk(result.Msg);
                            return;
                        }
                        if (Definitions.isQsUpdated)
                        {
                            try
                            {
                                new QC4Common.Sheets.ListUpdate((Excel.Workbook)Globals.ThisAddIn.Application.ActiveWorkbook).UpdateListSheet(ExcelAddIn.Common.Definitions.VariableDictionary.Select(q => q.Value).ToList());
                                Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(Globals.ThisAddIn.Application.ActiveWorkbook);
                            }
                            catch { }
                        }

                    }

                    msg = RegisterWindowMessage(Constants.RibbonMessage.msg_Menu);

                    if (msg == 0)
                    {
                        MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                    }
                    sheet.Application.Interactive = false;
                    SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
                }


            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonExecute_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
                    if (activeSheet != null)
                    {
                        Excel.Worksheet worksheet = (Excel.Worksheet)activeSheet;
                        if (worksheet.CodeName == Constants.SheetType.SH_FAList)
                        {
                            FAList.FAExecClick(worksheet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        public bool IsEditing(Excel.Application excelApp)
        {
            Microsoft.Office.Core.CommandBars cBars = excelApp.CommandBars;
            bool res = !cBars.GetEnabledMso("FileNewDefault");
            Marshal.ReleaseComObject(cBars);
            if (res)
            {
                MessageDialog.Info(AddinResource.ALERT_CELL_EDIT);
            }
            return res;
        }


        private void buttonOptionsCT_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    int msg = RegisterWindowMessage(Constants.RibbonMessage.msg_CrossOptions);
                    if (msg == 0)
                    {
                        MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                    }
                    SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonCT_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    int msg = RegisterWindowMessage(Constants.RibbonMessage.msg_CrossExecute);
                    if (msg == 0)
                    {
                        MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                    }
                    SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonChart_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    int msg = RegisterWindowMessage(Constants.RibbonMessage.msg_CrossChart);
                    if (msg == 0)
                    {
                        MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                    }
                    SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonExecuteGT_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    if (Change.ValidateGTTab(null, true))
                    {
                        int msg = RegisterWindowMessage(Constants.RibbonMessage.msg_GTExecute);
                        if (msg == 0)
                        {
                            MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                        }
                        SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, GetSigSettings(), 0);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonOptionsGT_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    int msg = RegisterWindowMessage(Constants.RibbonMessage.msg_GTOptions);
                    if (msg == 0)
                    {
                        MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                    }
                    SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, GetSigSettings(), 0);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonAutoSettings_Click(object sender, RibbonControlEventArgs e)
        {
            // code for Auto Settings in GT here
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
                    if (activeSheet != null)
                    {
                        Excel.Worksheet worksheet1 = (Excel.Worksheet)activeSheet;
                        GTSheetChange.FNCGTAutoSettingMain(worksheet1);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonOutput_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    int msg = RegisterWindowMessage(Constants.RibbonMessage.msg_SummaryExecute);
                    if (msg == 0)
                    {
                        MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                    }
                    SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonOptionsSummary_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    int msg = RegisterWindowMessage(Constants.RibbonMessage.msg_SummaryOptions);
                    if (msg == 0)
                    {
                        MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                    }
                    SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }


        private void buttonVEM_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    if (!QuestionSettingsUtil.VariableEditModeChange(Globals.ThisAddIn.Application.ActiveWorkbook))
                    {
                        return;
                    }
                    if (Definitions.VariableEditMode)
                    {
                        IsEditMode = true;
                        buttonVEM.Label = AddinResource.QS_STOP_VARIABLE_EDIT_MODE;
                        buttonVEM.Image = AddinResource.edit_stop_hover;
                        buttonEQ.Enabled = false;
                        buttonMenu.Enabled = false;
                        buttonCheckQS.Enabled = false;
                    }
                    else
                    {
                        IsEditMode = false;
                        buttonVEM.Label = AddinResource.QS_START_VARIABLE_EDIT_MODE;
                        buttonVEM.Image = AddinResource.edit_start_hover;
                        buttonEQ.Enabled = true;
                        buttonMenu.Enabled = true;
                        buttonCheckQS.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);

            }
        }

        private void checkBoxRows_Click(object sender, RibbonControlEventArgs e)
        {
            checkBoxCols.Checked = false;
            checkBoxRows.Checked = true;
            SettingTableOrientationStatus();
        }

        private void checkBoxCols_Click(object sender, RibbonControlEventArgs e)
        {
            checkBoxRows.Checked = false;
            checkBoxCols.Checked = true;
            SettingTableOrientationStatus();
        }


        private void checkBoxLevel1_Click(object sender, RibbonControlEventArgs e)
        {
            ValidateSigCheckBox();
        }

        private void checkBoxLevel5_Click(object sender, RibbonControlEventArgs e)
        {
            ValidateSigCheckBox();
        }

        private void checkBoxLevel10_Click(object sender, RibbonControlEventArgs e)
        {
            ValidateSigCheckBox();
        }

        private void ValidateSigCheckBox()
        {
            if (checkBoxLevel1.Checked && checkBoxLevel5.Checked && checkBoxLevel10.Checked)
                labelAlert.Visible = true;
            else
                labelAlert.Visible = false;

            UpdateSettingValues();
        }

        private void buttonInsert_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
                    if (activeSheet != null)
                    {
                        Excel.Worksheet worksheet1 = (Excel.Worksheet)activeSheet;
                        if (worksheet1.CodeName == Constants.SheetType.sh_CrossCounting)
                        {
                            //Insert function for CT
                            CrossChange.crossInsertDel(worksheet1);
                        }
                        else if (worksheet1.CodeName == Constants.SheetCodeName.DataProcess)
                        {
                            //Insert function for Data Processing
                            QC4Common.Sheets.DataProcess.DpInsertDel(worksheet1);
                            QC4Common.Sheets.DataProcess.FillDataProcessColumnB(worksheet1);
                        }
                        else if (worksheet1.CodeName == Constants.SheetType.sh_SummaryList)
                        {
                            //Insert function for Summary table
                            SLChange.SLInsertDel(worksheet1);
                        }
                        else if (worksheet1.CodeName == Constants.SheetType.GTCounting)
                        {
                            //Insert function for GT
                            GTSheetChange.GTInsertDel(worksheet1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonDelete_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
                    if (activeSheet != null)
                    {
                        Excel.Worksheet worksheet1 = (Excel.Worksheet)activeSheet;
                        if (worksheet1.CodeName == Constants.SheetType.sh_CrossCounting)
                        {
                            //Delete function for CT
                            CrossChange.crossInsertDel(worksheet1, true);
                        }
                        else if (worksheet1.CodeName == Constants.SheetCodeName.DataProcess)
                        {
                            //Delete function for Data Processing
                            QC4Common.Sheets.DataProcess.DpInsertDel(worksheet1, true);
                            QC4Common.Sheets.DataProcess.FillDataProcessColumnB(worksheet1);
                        }
                        else if (worksheet1.CodeName == Constants.SheetType.sh_SummaryList)
                        {
                            //delete function for Summary table
                            SLChange.SLInsertDel(worksheet1, true);
                        }
                        else if (worksheet1.CodeName == Constants.SheetType.GTCounting)
                        {
                            GTSheetChange.GTInsertDel(worksheet1, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonCheck_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
                    if (activeSheet != null)
                    {
                        Excel.Worksheet worksheet1 = (Excel.Worksheet)activeSheet;
                        if (worksheet1.CodeName == Constants.SheetType.sh_CrossCounting)
                        {
                            bool cancel = false;
                            Change.ValidateCRTab(null, ref cancel);
                        }
                        else if (worksheet1.CodeName == Constants.SheetCodeName.DataProcess)
                        {
                            //Check function for Data Processing
                            QC4Common.Sheets.DataProcess.isexecute = false;//Redmine id: 175141
                            Globals.ThisAddIn.DPCheck();
                        }
                        else if (worksheet1.CodeName == Constants.SheetType.sh_SummaryList)
                        {
                            //Check function for Summary table
                            bool cancel = false;
                            SLChange.ValidateSL(null, ref cancel);
                        }
                        else if (worksheet1.CodeName == Constants.SheetType.GTCounting)
                        {
                            //Check function for GT
                            Change.ValidateGTTab(null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonEQ_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    if (Definitions.VariableEditMode)
                    {
                        MessageDialog.Warning(AddinResource.ALERT_VARIABLE_EDIT_MODE);
                        return;
                    }

                    QC4Common.QS.QuestionnaireCreator qc = new QC4Common.QS.QuestionnaireCreator();
                    qc.CreateQuesionnaire(Globals.ThisAddIn.Application.ActiveWorkbook);
                    
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonCheckQS_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    Change.QsValidateClick();
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonJump_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    Excel.Worksheet sheet = ExcelUtil.GetWorksheetByIndex(1);
                    if (sheet.Application.ActiveWindow.ScrollColumn < sheet.Application.Columns[1014].Column)
                    {
                        sheet.Application.ActiveWindow.ScrollColumn = sheet.Application.Columns[1014].Column;
                        sheet.Application.EnableEvents = false;
                        sheet.Cells[sheet.Application.ActiveWindow.ScrollRow, 1014].Select();
                        sheet.Application.EnableEvents = true;
                    }
                    else
                    {
                        sheet.Application.ActiveWindow.ScrollColumn = 1;
                        sheet.Application.EnableEvents = false;
                        sheet.Cells[sheet.Application.ActiveWindow.ScrollRow, 11].Select();
                        sheet.Application.EnableEvents = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonUp_Click(object sender, RibbonControlEventArgs e)
        {
            //Write call for Up function of Data Processing here
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
                    if (activeSheet != null)
                    {
                        Excel.Worksheet worksheet1 = (Excel.Worksheet)activeSheet;
                        if (worksheet1.CodeName == Constants.SheetCodeName.DataProcess)
                        {
                            QC4Common.Sheets.DataProcess.DP_Up_Down_Row(worksheet1, false);
                            QC4Common.Sheets.DataProcess.FillDataProcessColumnB(worksheet1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonDown_Click(object sender, RibbonControlEventArgs e)
        {
            //Write call for Down function of Data Processing here
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
                    if (activeSheet != null)
                    {
                        Excel.Worksheet worksheet1 = (Excel.Worksheet)activeSheet;
                        if (worksheet1.CodeName == Constants.SheetCodeName.DataProcess)
                        {
                            QC4Common.Sheets.DataProcess.DP_Up_Down_Row(worksheet1, true);
                            QC4Common.Sheets.DataProcess.FillDataProcessColumnB(worksheet1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonCopy_Click(object sender, RibbonControlEventArgs e)
        {
            //Write call for Copy function of Data Processing here
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
                    if (activeSheet != null)
                    {
                        Excel.Worksheet worksheet1 = (Excel.Worksheet)activeSheet;
                        if (worksheet1.CodeName == Constants.SheetCodeName.DataProcess)
                        {
                            QC4Common.Sheets.DataProcess.Dp_Copy(worksheet1);
                            QC4Common.Sheets.DataProcess.FillDataProcessColumnB(worksheet1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonPaste_Click(object sender, RibbonControlEventArgs e)
        {
            //Write call for Paste function of Data Processing here
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
                    if (activeSheet != null)
                    {
                        Excel.Worksheet worksheet1 = (Excel.Worksheet)activeSheet;
                        if (worksheet1.CodeName == Constants.SheetCodeName.DataProcess)
                        {
                            QC4Common.Sheets.DataProcess.Dp_Paste(worksheet1);
                            QC4Common.Sheets.DataProcess.FillDataProcessColumnB(worksheet1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonExecuteDP_Click(object sender, RibbonControlEventArgs e)
        {
            ////Write call for Execute function of Data Processing here
            try
            {
                _log.Info("DP Starting");
                //#212092
                using ((checkBoxCross.Checked  || checkBoxList.Checked)? new QC4Common.Logic.SingleGlobalInstance(10, Globals.ThisAddIn.Application.ActiveWorkbook):null) //10ms timeout on global lock
                {
                    if (!IsEditing(Globals.ThisAddIn.Application))
                    {
                        QC4Common.Global.Global.CheckOperationFlag = 0;

                        if (checkBoxCross.Checked)
                        {
                            QC4Common.Global.Global.CheckOperationFlag |= QC4Common.Common.Constants.CheckCrossFlag.CheckCross;
                        }
                        if (checkBoxList.Checked)
                        {
                            QC4Common.Global.Global.CheckOperationFlag |= QC4Common.Common.Constants.CheckCrossFlag.CheckList;
                        }
                        //try { Globals.ThisAddIn.Application.ScreenUpdating = false; } catch { }
                        QC4Common.DP.CallDP calldp = new QC4Common.DP.CallDP();
                        bool undoButtonEnable = calldp.DPExecute(Globals.ThisAddIn.Application.ActiveWorkbook, false,ThisAddIn.HWND_MAIN, ThisAddIn.DataProcessSheet);
                        try
                        {
                            Process P = Process.GetProcessById(ThisAddIn.HWND_MAIN);
                            P.Kill();
                        }
                        catch (Exception ex)
                        {
                            //When the process Doesnot exist Do nothing
                        }
                        //Enabling and disabling undobutton in ribbon

                        if (!undoButtonEnable)
                            Globals.Ribbons.qc4Ribbon.buttonUndoDP.Enabled = false;
                        else
                            Globals.Ribbons.qc4Ribbon.buttonUndoDP.Enabled = true;

                        //try { Globals.ThisAddIn.Application.ScreenUpdating = true; } catch { }

                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            finally
            {
                try
                {
                   
                    
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                catch { }
                //  COMWholeOperate.releaseComObject(ref Logic.DataProcessExecute.lworkbook);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }


        private void buttonStatus_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (!IsEditing(Globals.ThisAddIn.Application))
                {
                    object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
                    if (activeSheet != null)
                    {
                        Excel.Worksheet worksheet = (Excel.Worksheet)activeSheet;
                        CrossChange.crossCheck(worksheet);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void buttonFormat_Click(object sender, RibbonControlEventArgs e)
        {
            if (checkBoxRows.Checked)
            {
                // code for Rows selection for Output format in Cross here
            }
            if (checkBoxCols.Checked)
            {
                // code for Column selection for Output format in Cross here
            }
        }


        private int GetSigSettings()
        {
            string strVal = "";
            if (checkBoxLevel1.Checked)
                strVal = strVal + "1";
            if (checkBoxLevel5.Checked)
                strVal = strVal + "2";
            if (checkBoxLevel10.Checked)
                strVal = strVal + "3";

            if (strVal == "") strVal = "0";

            return Convert.ToInt32(strVal);
        }

        private void InitSigCheckBoxValues()
        {
            try
            {
                SigSettings sigSettings = new CommonFunctions().GetGTSigSettings(Globals.ThisAddIn.Application.ActiveWorkbook);
                checkBoxLevel1.Checked = sigSettings.IsSig1Checked;
                checkBoxLevel5.Checked = sigSettings.IsSig5Checked;
                checkBoxLevel10.Checked = sigSettings.IsSig10Checked;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
            }
            ValidateSigCheckBox();
        }

        private void UpdateSettingValues()
        {
            try
            {
                new CommonFunctions().UpdateSigSettings(Globals.ThisAddIn.Application.ActiveWorkbook, new SigSettings()
                {
                    IsSig1Checked = checkBoxLevel1.Checked,
                    IsSig5Checked = checkBoxLevel5.Checked,
                    IsSig10Checked = checkBoxLevel10.Checked,
                });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void checkBoxCross_Click(object sender, RibbonControlEventArgs e)
        {
            SettingCheckboxStatus();
        }

        private void checkBoxList_Click(object sender, RibbonControlEventArgs e)
        {
            SettingCheckboxStatus();
        }

        private void SettingCheckboxStatus()
        {
            Excel.Worksheet settingssheet = ExcelUtil.GetWorkSheetByCodeName(Constants.SheetCodeName.Setting);
            Excel.Range cell = settingssheet.Cells[QC4Common.Common.Constants.PRO_DP.PRO_CCross_CList_Row, QC4Common.Common.Constants.PRO_DP.PRO_CCross_CList_Column];

            if (checkBoxCross.Checked == false && checkBoxList.Checked == false)
                cell.Value = QC4Common.Common.Constants.DP_Checkbox_Value.BothUnchecked;
            else if (checkBoxCross.Checked == false && checkBoxList.Checked == true)
                cell.Value = QC4Common.Common.Constants.DP_Checkbox_Value.OnlyCheckListChecked;
            else if (checkBoxCross.Checked == true && checkBoxList.Checked == false)
                cell.Value = QC4Common.Common.Constants.DP_Checkbox_Value.OnlyCheckCrossChecked;
            else if (checkBoxCross.Checked == true && checkBoxList.Checked == true)
                cell.Value = QC4Common.Common.Constants.DP_Checkbox_Value.BothChecked;
        }

        private void SettingTableOrientationStatus()
        {
            Excel.Worksheet settingssheet = ExcelUtil.GetWorkSheetByCodeName(Constants.SheetCodeName.Setting);
            Excel.Range cell = settingssheet.Cells[QC4Common.Common.Constants.Cross_TableOrientation.PRO_Cross_TableOrientation_Row, QC4Common.Common.Constants.Cross_TableOrientation.PRO_Cross_TableOrientation_Column];

            if (checkBoxRows.Checked == true)
                cell.Value = 0;
            else if (checkBoxCols.Checked == true)
                cell.Value = 1;
            //else
            //    cell.Value = String.Empty;
        }

        private void buttonUndoDP_Click(object sender, RibbonControlEventArgs e)
        {
            Excel.Workbook Workbook = Globals.ThisAddIn.Application.ActiveWorkbook;
            if (QC4Common.Common.DataProcessCommonFunctions.RestoreDataAfterProcess(Workbook))
            {
                buttonUndoDP.Enabled = false;
            }
        }
    }
}
