using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using ExcelAddIn.Sheets;
using ExcelAddIn.Common;
using System.Windows.Forms;
using System.Globalization;
using System.Data;
using System.IO;
using QC4Common.Model;
using microsft = Microsoft.Office.Interop.Excel;
using MessageDialog = QC4Common.Common.MessageDialog;
using System.Runtime.InteropServices;
using Qc4CommonConstants = QC4Common.Common.Constants;
using log4net;
using System.Reflection;
using ProgressBar = QC4Common.Forms.ProgressBar;
using System.Windows.Interop;
using System.Threading;
using Macromill.QCWeb.COMOperate;
using System.Windows.Input;
using System.ComponentModel;
using System.Data.SQLite;
using QC4Common.Global;
using QC4Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ExcelAddIn
{
    public partial class ThisAddIn
    {
        public static QC4Common.CustomTaskPaneControl CustomControlPane;
        private Microsoft.Office.Tools.CustomTaskPane myCustomTaskPane;
        private Excel.Workbook workBook;
        static Excel.Worksheet wsData_Process;


        public static QC4Common.Sheets.DataProcess DataProcessSheet;//191 changed from -private to Public
        private LDel LDelSheet;
        //private FAList FASheet;
        public static bool VariableEditMode = false;
        //public static List<string> RowVariableList;
        List<int> ListAdd1 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        private Excel.Worksheet PreviousSheet;
        private bool datachanged = false;
        public bool IsDataBrowse = false;
        public object FASheet { get; private set; }
        private DataAfterProcess dataafterprocess_Sheet { get; set; }
        private bool isFirst = false;
        private bool isDataFirst = false;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(int hWnd, int Msg, int wParam, int lParam);
        private const int HWND_BROADCAST = 0xffff;
        public static int HWND_MAIN = 0;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static bool isClose = false;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        static extern bool EnableWindow(IntPtr hWnd, bool enable);

        /// <summary>
        /// Actions take place on various events
        /// </summary>
        /// 
        #region Actions
        /// <summary>
        /// Initialise the custom taskpane to be displayed 
        /// 
        /// </summary>
        private void InitTaskPane()
        {
            CustomControlPane = new QC4Common.CustomTaskPaneControl();
            myCustomTaskPane = CustomTaskPanes.Add(CustomControlPane,
                    "   ");

            myCustomTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionFloating;
            myCustomTaskPane.Height = Convert.ToInt32(this.Application.Height);
            myCustomTaskPane.Width = 203;
            myCustomTaskPane.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;
            //foreach (Control lbxControl in CustomControlPane.Controls)//Phase3
            //{
            //    if (lbxControl is ListBox)
            //    {
            //        ((ListBox)lbxControl).SelectionMode = SelectionMode.MultiExtended;
            //        ((ListBox)lbxControl).PreviewKeyDown += new PreviewKeyDownEventHandler(myCustomTaskPane_PreviewKeyDown);
            //    }
            //}
            //// myCustomTaskPane.Control.Click += new EventHandler(myCustomTaskPane_Click);
        }
        void myCustomTaskPane_PreviewKeyDown(object sender, PreviewKeyDownEventArgs /*KeyEventArgs*/ e)//(object sender, System.EventArgs e)//Phase3
        {
            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
            if (_altModifierPressed && Keyboard.IsKeyDown(Key.C))
            {

                // HandleCopyPaste(sender, e);

                List<string> list = new List<string>();
                string copiedvariable = string.Empty;
                foreach (Control lbxControl in CustomControlPane.Controls)
                {
                    if (lbxControl is ListBox)
                    {

                        foreach (string s in ((ListBox)lbxControl).SelectedItems)
                        {
                            list.Add(s);
                            copiedvariable += (copiedvariable.Length > 0) ? "\r\n" + s : s;
                            System.Diagnostics.Debug.WriteLine(s);
                        }
                    }
                }
                Clipboard.SetText(copiedvariable);
            }
        }
        /// <summary>
        /// Enable or disable the visibility of the task pane
        /// </summary>
        /// <param name="SetVisibility">The bool flag to set the visibility of task pane</param>
        private void EnableTaskPane(bool SetVisibility)
        {
            try
            {
                if (myCustomTaskPane != null)
                    myCustomTaskPane.Visible = SetVisibility;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        /// <summary>
        /// Populate the variables under category SA and N to determine the operators to be loaded
        /// </summary>
        /// <returns>True if populated properly False in case of any exception</returns>
        private bool PopulateSANVariableList()
        {
            Excel.Range Range = ExcelUtil.GetNamedRange("List", "List_Item_SAN");
            if (Range == null) return false;
            QC4Common.Global.Global.SANvariables.Clear();
            if (Range.Cells.Count == 1)
            {
                QC4Common.Global.Global.SANvariables.Add(Range.Value.ToString());
                return true;
            }
            Object[,] objAry = Range.Value;
            int max = objAry.GetLength(0);
            for (int i = 1; i <= max; i++)
            {
                if (objAry[i, 1] != null)
                {
                    QC4Common.Global.Global.SANvariables.Add(objAry[i, 1].ToString());
                }
            }

            //foreach (Excel.Range xlCell in Range.Cells.Cells)
            //{
            //	SANvariables.Add(xlCell[1, 1].Text);
            //}
            return true;
        }
        /// <summary>
        /// Add event listener for getting notified on cell changes
        /// </summary>
        private void SetCellChangeEvent()
        {
            wsData_Process.Change += DataProcessSheet.DataProcess_Cell_Change_Listener;
        }

        /// <summary>
        /// Populate the possible values of variables from the Question Settings sheet to the specified cell
        /// </summary>
        /// <param name="Target">The changed variable cell of which the values has to be populated</param>                 
        private Office.CommandBar GetCellContextMenu()
        {
            return this.Application.CommandBars["Cell"];
        }
        private void ResetCellMenu()
        {
            GetCellContextMenu().Reset(); // reset the cell context menu back to the default
        }
        #endregion
        /// <summary>
        /// The Events fired/Listened from Excel Application
        /// </summary>
        /// 

        #region ExcelEvents
        public void OnWorkbook_Open(Excel.Workbook Wb)
        {
            string FileName = ExcelUtil.GetActiveWorkBookName();
            if (!Definitions.ExtentionList.Contains(Path.GetExtension(FileName.ToLower())))
            {
                bool isQc4App = false;
                string fileName = Wb.FullName;
                foreach (Excel.Workbook wb in Wb.Application.Workbooks)
                {
                    if (Definitions.ExtentionList.Contains(Path.GetExtension(wb.FullName)))
                    {
                        isQc4App = true;
                        break;
                    }
                }

                if (!Definitions.FileList.Contains(Path.GetFileName(fileName)) && isQc4App && QC4Common.Logic.DataProcessExecute.islistup == false)//191 added for listup comming blank from second listup
                {
                    Excel.Application eApp = Wb.Application;
                    Excel.Application nextApp = new Excel.Application();
                    eApp.DisplayAlerts = false;
                    Wb.Close();
                    Wb = null;
                    eApp.DisplayAlerts = true;
                    nextApp.Workbooks.Open(fileName);
                    nextApp.Visible = true;
                    nextApp = null;
                }
                return;
            }

            ((Excel.AppEvents_Event)this.Application).NewWorkbook += new Excel.AppEvents_NewWorkbookEventHandler(ThisWorkbook_NewWorkbook);
            this.Application.WorkbookActivate += OnWorkbook_Activate;
            this.Application.SheetActivate += OnSheet_Activated; //new Excel.AppEvents_SheetActivateEventHandler(OnSheet_Activated);
            this.Application.SheetDeactivate += OnSheet_Deactivated;
            this.Application.SheetBeforeRightClick += Application_SheetBeforeRightClick;
            this.Application.SheetBeforeDoubleClick += new Excel.AppEvents_SheetBeforeDoubleClickEventHandler(Application_SheetBeforeDoubleClick);
            this.Application.SheetChange += new Excel.AppEvents_SheetChangeEventHandler(Application_SheetChange);
            this.Application.WorkbookBeforeSave += Application_WorkbookBeforeSave;
            this.Application.WorkbookBeforeClose += Application_WorkbookBeforeClose;
            this.Application.SheetSelectionChange += new Excel.AppEvents_SheetSelectionChangeEventHandler(Application_SheetSelectionChange);
            workBook = Wb;
            PreviousSheet = Wb.ActiveSheet;
            QC4Ribbon.isRibbonLoaded = true;
            Globals.Ribbons.qc4Ribbon.QC4.Visible = true;
            //Util.RibbonUtil.HideQC4Groups();
            Util.RibbonUtil.RibbonChange(PreviousSheet);
            InterceptKeys.SetHook();
        }

        void ThisWorkbook_NewWorkbook(Excel.Workbook Wb)
        {
            Excel.Application nextApp = new Excel.Application();
            Wb.Close();
            Wb = null;
            nextApp.Workbooks.Add();
            nextApp.Visible = true;
            nextApp = null;
        }

        private void Application_WorkbookBeforeClose(Excel.Workbook Wb, ref bool Cancel)
        {
            if (!Definitions.ExtentionList.Contains(Path.GetExtension(Wb.Name.ToLower())))
            {
                return;
            }
            // Definitions.outputApplication.Quit();
            string fileName = QC4Common.Util.QCUtil.GetFileName(Wb);
            Cancel = true;
            DialogResult res;
            int msg;
            if (!Definitions.VariableEditMode)
            {
                res = MessageDialog.WarningYesNoCancel(string.Format(AddinResource.SAVE_ALERT, fileName));
                if (res == DialogResult.Cancel)
                {
                    Excel.Application excelAppx = Wb.Application;
                    excelAppx.EnableEvents = true;
                    excelAppx = null;
                    return;
                }
                else if (res == DialogResult.Yes)
                {

                    isClose = true;
                    Application_WorkbookBeforeSave(Wb, false, ref Cancel);
                    if (!isClose)
                    {
                        Wb.Application.DisplayAlerts = true;
                        Wb.Application.EnableEvents = true;
                        return;
                    }
                }

            }
            else
            {
                res = MessageDialog.WarningYesNoCancel(string.Format(AddinResource.CLOSE_ALERT, fileName));
                if (res != DialogResult.Yes)
                {
                    return;
                }
            }

            Wb.Application.DisplayAlerts = false;
            Wb.Application.EnableEvents = false;
            Wb.Application.UserControl = false;
            Excel.Application excelApp = Wb.Application;
            Wb.Close(0);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Wb);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            COMWholeOperate.releaseComObject(ref Wb);
            COMWholeOperate.releaseComObject(ref excelApp);
            GC.Collect();
            msg = RegisterWindowMessage(Constants.RibbonMessage.msg_Close);
            if (msg == 0)
            {
                MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
            }
            SendNotifyMessage(ThisAddIn.HWND_MAIN, msg, 0, 0);

        }

        private void Application_WorkbookBeforeSave(Excel.Workbook Wb, bool SaveAsUI, ref bool Cancel)
        {
            if (!Definitions.ExtentionList.Contains(Path.GetExtension(Wb.Name.ToLower())))
            {
                isClose = false;
                return;
            }

            Cancel = true;
            if (Definitions.VariableEditMode)
            {
                MessageDialog.ErrorOk(AddinResource.SAVE_ALERT_VARIBLE_EDIT_MODE);
                isClose = false;
                return;
            }

            Excel.Worksheet sheet = Wb.ActiveSheet;
            try
            {
                if (sheet.CodeName == Constants.SheetCodeName.QuestionSetting)
                {
                    ReturnClass result = QuestionSettingsUtil.QsSheetChangeValidate(workBook);
                    if (!result.Result)
                    {
                        Excel.Range r = (Excel.Range)result.Value;
                        r.Select();
                        MessageDialog.ErrorOk(result.Msg);
                        isClose = false;
                        return;
                    }

                    result = new QS.IntegrityCheck(workBook).Check(out List<QuestionSettings> variableChanges, out List<QuestionSettings> answerChanges,
                        out List<QuestionSettings> countChanges, out List<QuestionSettings> deleteList, out List<QuestionSettings> updateList, false);
                    if (!result.Result)
                    {
                        if (result.Value != null)
                        {
                            Excel.Range r = (Excel.Range)result.Value;
                            r.Select();
                        }
                        MessageDialog.ErrorOk(result.Msg);
                        isClose = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

            string tempPath;
            string targetPath;
            string fileName = QC4Common.Util.QCUtil.GetFileName(workBook);
            QC4Common.Util.FileHelper fileHelper = new QC4Common.Util.FileHelper();
            if (SaveAsUI)
            {
                Object obj = Application.GetSaveAsFilename(FileFilter: "QC4 files(*.qc4),*.qc4");
                if (obj == null || obj.Equals(false))
                {
                    isClose = false;
                    return;
                }
                targetPath = obj.ToString();
                FileInfo Qcfile = new FileInfo(targetPath);
                bool File_read_only;
                if (QC4Common.Util.ExcelUtil.GetReadonlyMode(Wb) == "1")
                {
                    File_read_only = true;
                }
                else
                {
                    File_read_only = false;
                }
                if (File_read_only && fileHelper.IsFileLocked(Qcfile))
                {
                    MessageDialog.WarningYesNoCancel(string.Format(AddinResource.FILE_READONLY_SAVE_WARNING_MSG, fileName));

                    Wb.Application.EnableEvents = true;
                    return;

                }
                if (fileHelper.IsFileLocked(Qcfile) && QC4Common.Util.QCUtil.GetTargetPath(Wb) != targetPath)
                {
                    MessageDialog.WarningYesNoCancel(string.Format(AddinResource.FILE_READONLY_SAVE_WARNING_MSG, fileName));
                    isClose = false;
                    return;
                }
            }
            else
            {
                targetPath = QC4Common.Util.QCUtil.GetTargetPath(Wb);
                if (QC4Common.Util.ExcelUtil.GetReadonlyMode(Wb) == "1")
                {
                    DialogResult dialogResult = MessageDialog.WarningOkCancel(string.Format(AddinResource.FILE_READONLY_SAVE_WARNING_MSG, fileName));
                    isClose = false;
                    return;
                }
            }

            {
                int msg = RegisterWindowMessage(Constants.RibbonMessage.msg_File_lockRelease);
                if (msg == 0)
                {
                    MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                }
                SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
            }

            tempPath = QC4Common.Util.QCUtil.GetTempPath(Wb);
            if (SaveAsUI)
                QC4Common.Util.QCUtil.SetReadOnly(Wb, false);
            fileHelper.SaveFile(ref targetPath, ref tempPath, Wb, Wb.Application, SaveAsUI, !SaveAsUI);
            Application.Caption = "Macromill - Quick-CROSS";
            Wb.Windows[1].Caption = Path.GetFileName(targetPath);
            if (isClose)
            {
                return;
            }

            if (SaveAsUI)
            {
                int msg = RegisterWindowMessage(Constants.RibbonMessage.msg_SaveAs);
                if (msg == 0)
                {
                    MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                }
                SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
            }
            else
            {

                int msg = RegisterWindowMessage(Constants.RibbonMessage.msg_File_lockCreate);
                if (msg == 0)
                {
                    MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                }
                SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
            }
            try
            {
                if (sheet.CodeName == Constants.SheetCodeName.Data01)
                {
                    UpdateDataSheet(workBook, sheet);
                }
                else if (sheet.Name == Constants.SheetType.sh_Data01 + "(Processed)")
                {
                    UpdateDataAfterSheet(workBook, sheet);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private static void UpdateDataSheet(Excel.Workbook workBook, Excel.Worksheet sheet, bool DataBrowse = false)
        {
            try
            {

                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlWait;
                workBook.Application.EnableEvents = false;
                workBook.Application.ScreenUpdating = false;
                if (!QC4Common.Common.CommonFlag.IsDataUpdated(Globals.ThisAddIn.Application.ActiveWorkbook))
                {
                    DataSheetUpdate du = new DataSheetUpdate(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.ActiveWorkbook, Globals.ThisAddIn.Application.ActiveSheet);
                    var array = Definitions.VariableDictionary.Where(a => (a.Value.QuestionFlag == "Org") || (a.Value.QuestionFlag == "Imp")).Select(q => q.Value).ToList();
                    du.LoadDataWithPB("answers", array, DataBrowse);
                    QC4Common.Common.CommonFlag.SetIsDataUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, true);


                }
                Common.Definitions.isSheetUpdating = false;
                workBook.Application.ScreenUpdating = true;
                workBook.Application.EnableEvents = true;

                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlDefault;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlDefault;
            }
        }

        private static void UpdateDataAfterSheet(Excel.Workbook workBook, Excel.Worksheet sheet, bool DataBrowse = false)
        {
            try
            {

                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlWait;
                workBook.Application.EnableEvents = false;
                workBook.Application.ScreenUpdating = false;
                if (!QC4Common.Common.CommonFlag.IsDataAfterUpdated(Globals.ThisAddIn.Application.ActiveWorkbook))
                {
                    DataSheetUpdate du = new DataSheetUpdate(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.ActiveWorkbook, Globals.ThisAddIn.Application.ActiveSheet);
                    var array = Definitions.VariableDictionary.Where(a => (a.Value.QuestionFlag == "Org") || (a.Value.QuestionFlag == "Imp") || a.Value.QuestionFlag == "New").Select(q => q.Value).ToList();
                    du.LoadDataWithPB("data_after_process", array, DataBrowse);
                    QC4Common.Common.CommonFlag.SetIsDataAfterUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, true);
                }
                Common.Definitions.isSheetUpdating = false;
                workBook.Application.ScreenUpdating = true;
                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlDefault;
                workBook.Application.EnableEvents = true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlDefault;
            }
        }


        private void Application_SheetSelectionChange(object Sh, Excel.Range Target)
        {
            Util.RibbonUtil.EnableRibbon(!QC4Common.Util.ExcelUtil.IsEditing(Globals.ThisAddIn.Application));
        }

        public void OnWorkbook_Activate(Excel.Workbook Wb)
        {
            if (!Definitions.ExtentionList.Contains(Path.GetExtension(Wb.Name.ToLower())))//191 added for listup
            {
                QC4Ribbon.isRibbonLoaded = false;
                Globals.Ribbons.qc4Ribbon.QC4.Visible = false;
                Application.Caption = "";
                return;
            }
            QC4Ribbon.isRibbonLoaded = true;
            Globals.Ribbons.qc4Ribbon.QC4.Visible = true;
            Application.Caption = "Macromill - Quick-CROSS";
            Globals.Ribbons.qc4Ribbon.RibbonUI.ActivateTab("QC4");
            if (Wb.Name.Contains("Comparison GT")) { return; }//SetForegroundWindow((IntPtr)Wb.Application.Hwnd);
            Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(Wb);
            QC4Common.Common.Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(Wb);
            QuestionSettingsUtil.UpdateIdFlag();

            Common.Change.bindButtons(Wb);

            object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
            if (activeSheet != null)
            {
                Excel.Worksheet worksheet1 = (Excel.Worksheet)this.Application.ActiveSheet;
                Util.RibbonUtil.RibbonChange(worksheet1);
            }
            InterceptKeys.SetHook();
        }
        public void OnSheet_Deactivated(Object workSheet)
        {
            Excel.Worksheet temp = (Excel.Worksheet)workSheet;
            if (!Definitions.ExtentionList.Contains(Path.GetExtension(temp.Application.ActiveWorkbook.Name.ToLower())))
            {
                return;
            }
            PreviousSheet = temp;
        }

        private void InitDPProSheet()
        {
            DataProcessSheet.ClearVariableListColumn();
            InitTaskPane();
            DataProcessSheet.SetCriteriaVariableColumn();
            if (PopulateSANVariableList())
            {
                SetCellChangeEvent();
            }
            DataProcessSheet.ProInitialized = true;

        }
        private void DataProcess_Sheet_BeforeDoubleClick(Microsoft.Office.Interop.Excel.Range Target, ref bool Cancel)
        {
            if (Target.Row < 5) return;
            switch (Target.Column)
            {
                case Constants.DP.CheckCrossColumn:
                case Constants.DP.OnOffColumn:
                    ToggleOnOff(Target);
                    Cancel = true;
                    break;
            }


        }
        public void ToggleOnOff(Excel.Range cell)
        {
            string content = cell.Text;
            if (!string.IsNullOrEmpty(content))
            {
                if (content.Equals(CommonResource.CELL_ON))
                {
                    cell.Value = CommonResource.CELL_OFF;
                }
                else
                {
                    cell.Value = CommonResource.CELL_ON;
                }
            }
            else//new bug 07-01-2020
            {
                Excel.Range range = cell.Offset[0, 5];
                if (!String.IsNullOrEmpty(range.Text))
                {
                    cell.Value = CommonResource.CELL_ON;
                }
            }
        }
        public void OnSheet_Activated(Object workSheet)
        {
            var clipboarddata = Clipboard.GetDataObject();//Redmine id : 192270
            Excel.Worksheet temp = (Excel.Worksheet)workSheet;
            if (!Definitions.ExtentionList.Contains(Path.GetExtension(temp.Application.ActiveWorkbook.Name.ToLower())))
            {
                return;
            }
            try
            {


                if (Globals.Ribbons.qc4Ribbon.RibbonUI != null)
                    Globals.Ribbons.qc4Ribbon.RibbonUI.ActivateTab("QC4");
                Util.RibbonUtil.HideQC4Groups();
            }
            catch (Exception ex) { _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }//191
            string FileName = ExcelUtil.GetActiveWorkBookName();

            Excel.Worksheet sheet = temp;// (Excel.Worksheet)workSheet;
            try
            {
                // Application.OnKey("^v", Type.Missing);//191  enabling paste
                if (Definitions.ExtentionList.Contains(Path.GetExtension(FileName.ToLower())))
                {

                    if (PreviousSheet.CodeName == Constants.SheetType.sh_QuesSetting)
                    {
                        Excel.Worksheet worksheet = null;
                        if (Definitions.isQsUpdated)
                            worksheet = ExcelUtil.setLoading(); //1
                        Application.Cursor = Excel.XlMousePointer.xlWait;
                        ReturnClass result = QuestionSettingsUtil.MoveFromQs(workBook, PreviousSheet);// new ReturnClass(true);//2
                        Application.Cursor = Excel.XlMousePointer.xlDefault;
                        if (!result.Result)
                        {
                            RestrictSheetChange(result, PreviousSheet);
                            ExcelUtil.unsetLoading();
                            return;
                        }

                        if (Definitions.isQsUpdated)
                        {
                            Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(Application.ActiveWorkbook);//3 
                            QC4Common.Common.Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(Application.ActiveWorkbook);
                            PopulateSANVariableList();
                            int msg = RegisterWindowMessage(QC4Common.Common.Constants.CommonMessage.msg_UpdVarDict);
                            if (msg == 0)
                                MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                            worksheet.Application.EnableEvents = false;
                            worksheet.Application.Interactive = false;
                            SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
                        }
                        try
                        {
                            var array = QC4Common.Common.Definitions.VariableDictionary.Where(a => (a.Value.QuestionFlag == "Org") || (a.Value.QuestionFlag == "Imp")).Select(q => q.Value).ToList();
                            QC4Common.Util.ExcelUtil.Data02validation(Application.ActiveWorkbook, array);//4 

                        }
                        catch
                        {

                            Excel.Worksheet dsheet = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(Application.ActiveWorkbook, QC4Common.Common.Constants.SheetCodeName.Data01);
                            RestrictSheetChangedata02Validation(dsheet);
                            ExcelUtil.unsetLoading();
                            Application.Interactive = true;
                            UpdateDataSheet(workBook, dsheet);

                        }
                        Definitions.isQsUpdated = false;

                        //TODO pass window message to update dict in launcher
                    }
                    try
                    {
                        Util.RibbonUtil.RibbonChange(sheet);

                        if (sheet.CodeName == Constants.SheetCodeName.DataProcess)
                        {
                            wsData_Process = (Excel.Worksheet)workSheet;

                            if (DataProcessSheet == null)
                            {
                                wsData_Process.BeforeDoubleClick += DataProcess_Sheet_BeforeDoubleClick;
                                DataProcessSheet = new QC4Common.Sheets.DataProcess(wsData_Process);


                            }
                            if (!DataProcessSheet.ProInitialized)
                            {
                                InitDPProSheet();
                                DataProcessSheet.FillDataInDocpane(CustomControlPane);
                            }

                            //for chechking the status of checkboxes
                            Util.RibbonUtil.GetCheckboxStatus(sheet);

                            //DataProcessSheet.FillDataInDocpane(CustomControlPane);
                            myCustomTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
                            EnableTaskPane(true);
                        }
                        else
                        {
                            EnableTaskPane(false);
                        }
                        if (sheet.CodeName == Constants.SheetType.sh_QuesSetting)
                        {
                            Definitions.isQsChanged = false;
                            EnableTaskPane(false);
                            QuestionSettingsUtil.UpdateIdFlag();
                            Definitions.QsRowCount = ExcelUtil.GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.QuestionSetting).UsedRange.Rows.Count;
                        }
                        else if (sheet.CodeName == Constants.SheetType.Sh_LDEL)
                        {
                            //No Action required
                        }
                        else if (sheet.CodeName == Constants.SheetType.SH_FAList)
                        {

                        }
                        else if (sheet.Name.Contains("(Processed)") && sheet.Visible == Excel.XlSheetVisibility.xlSheetVisible)
                        {
                            if (workBook.Application.Visible)
                            {
                                DataTable dt = QC4Common.Util.ExcelUtil.GetDataAfterSheetNamesAndPosition(workBook);
                                bool found = false;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][1].ToString() == sheet.Name)
                                    {

                                        found = true;
                                        break;
                                    }
                                }
                                if (found)
                                {
                                    UpdateDataAfterSheet(workBook, sheet);
                                }
                            }

                        }
                        else if (sheet.Name.StartsWith("Data") && sheet.Visible == Excel.XlSheetVisibility.xlSheetVisible)
                        {
                            DataTable dt = QC4Common.Util.ExcelUtil.GetDataSheetNamesAndPosition(workBook);
                            bool found = false;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i][1].ToString() == sheet.Name)
                                {

                                    found = true;
                                    break;
                                }
                            }
                            if (found)
                            {
                                UpdateDataSheet(workBook, sheet);
                            }
                        }
                        else if (sheet.Name == "Comparison GT")
                        {
                            SetForegroundWindow((IntPtr)sheet.Application.Hwnd);
                        }
                        else if (sheet.CodeName == Common.Constants.SheetType.sh_CrossCounting)
                        {

                            Util.RibbonUtil.GetCrossCheckedBoxStatus(sheet);

                        }
                        else if (sheet.Name == Constants.SheetType.sh_Data_AN2 && sheet.Visible == Excel.XlSheetVisibility.xlSheetVisible)
                        {
                            if (!QC4Common.Common.CommonFlag.IsMultivariateUpdated(Globals.ThisAddIn.Application.ActiveWorkbook))
                            {
                                UpdateMultivariateSheet();
                            }
                        }
                        //else if (sheet.Name.Contains("Multivariate") && sheet.Visible == Excel.XlSheetVisibility.xlSheetVisible)
                        //{
                        //    if (workBook.Application.Visible)
                        //    {
                        //        DataTable dt = QC4Common.Util.ExcelUtil.GetDataMultivariateNamesAndPosition(workBook);
                        //        bool found = false;
                        //        for (int i = 0; i < dt.Rows.Count; i++)
                        //        {
                        //            if (dt.Rows[i][1].ToString() == sheet.Name)
                        //            {

                        //                found = true;
                        //                break;
                        //            }
                        //        }
                        //        if (found)
                        //        {
                        //            UpdateMultivariateSheet(workBook, sheet);
                        //        }
                        //    }

                        //}
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                }
                else if (sheet.Name.Contains("Comparison GT")) { SetForegroundWindow((IntPtr)sheet.Application.Hwnd); return; }//SetForegroundWindow((IntPtr)Wb.Application.Hwnd);

            }
            catch (Exception ex)
            {
                // _log.Info(ex.StackTrace);
                // _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            try
            {
                if (!Clipboard.ContainsText())
                    Clipboard.SetDataObject(clipboarddata);//Redmine id : 192270
            }
            catch { }
        }

        private void UpdateMultivariateSheet()
        {
            try
            {
                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlWait;
                workBook.Application.EnableEvents = false;
                workBook.Application.ScreenUpdating = false;
                DataSheetUpdate du = new DataSheetUpdate(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.ActiveWorkbook, Globals.ThisAddIn.Application.ActiveSheet);
                List<QuestionSettings> qst = QC4Common.Common.Definitions.VariableDictionary.Where(x => (x.Value.QuestionFlag == "An" || x.Value.Variable.Equals("SAMPLEID"))).Select(q => q.Value).ToList();
                du.LoadDataWithPB("", qst, false, true);
                Common.Definitions.isSheetUpdating = false;
                workBook.Application.ScreenUpdating = true;
                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlDefault;
                workBook.Application.EnableEvents = true; Common.Definitions.isSheetUpdating = false;
                workBook.Application.ScreenUpdating = true;
                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlDefault;
                workBook.Application.EnableEvents = true;
                QC4Common.Common.CommonFlag.SetIsMultivariateUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, true);
            }
            catch { Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlDefault; }
        }

        private void RestrictSheetChange(ReturnClass result, Excel.Worksheet sheet)
        {
            PreviousSheet.Application.EnableEvents = false;
            PreviousSheet.Activate();
            PreviousSheet.Application.EnableEvents = true;
            EnableTaskPane(false);
            Util.RibbonUtil.HideQC4Groups();
            Util.RibbonUtil.RibbonChange(sheet);
            if (result.Value != null)
            {
                Excel.Range r = (Excel.Range)result.Value;
                r.Select();
            }
            MessageDialog.ErrorOk(result.Msg);
            return;
        }
        private void RestrictSheetChangedata02Validation(Excel.Worksheet sheet)
        {
            sheet.Application.EnableEvents = false;
            sheet.Activate();
            sheet.Application.EnableEvents = true;
            EnableTaskPane(false);
            Util.RibbonUtil.HideQC4Groups();
            Util.RibbonUtil.RibbonChange(sheet);
            return;
        }

        private void Application_SheetBeforeRightClick(object sh, Excel.Range Target, ref bool cancel)
        {
            if (Definitions.VariableEditMode)
            {
                cancel = true;
            }
            ResetCellMenu();  // reset the cell context menu back to the default
            CommandBar.AddMenuItem(sh, Target, cancel);
        }
        #endregion

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        #region VSTO generated code

        #region Startup and ShutDown
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                string location = assembly.CodeBase;
                string fullPath = new Uri(location).LocalPath; // path including the dll 
                string directoryPath = Path.GetDirectoryName(fullPath); // directory path
                                                                        // MessageBox.Show(directoryPath);
                string templatesPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)); // directory path
                templatesPath = Path.Combine(templatesPath, @"Roaming\QC4\Templates\");

                Directory.CreateDirectory(Path.Combine(directoryPath, "x64"));
                Directory.CreateDirectory(Path.Combine(directoryPath, "x86"));

                string x64Interop = Path.Combine(directoryPath, "x64\\SQLite.Interop.dll");
                string x86Interop = Path.Combine(directoryPath, "x86\\SQLite.Interop.dll");
                //MessageBox.Show(x64Interop + "\n" + x86Interop);
                if (File.Exists(Path.Combine(templatesPath, @"x64\Sqlite.Interop.dll")) && !File.Exists(x64Interop))
                {
                    File.Copy(Path.Combine(templatesPath, @"x64\Sqlite.Interop.dll"), x64Interop, false);
                }
                if (File.Exists(Path.Combine(templatesPath, @"x86\Sqlite.Interop.dll")) && !File.Exists(x86Interop))
                {
                    File.Copy(Path.Combine(templatesPath, @"x86\Sqlite.Interop.dll"), x86Interop, false);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);  // MessageBox.Show(ex.Message);
            }
            Definitions.RowVariableList = new List<string>();
            Definitions.VariableDictionary = new Dictionary<string, QuestionSettings>(StringComparer.InvariantCultureIgnoreCase);
            QC4Common.Common.Definitions.VariableDictionary = new Dictionary<string, QuestionSettings>(StringComparer.InvariantCultureIgnoreCase);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            InterceptKeys.ReleaseHook();
        }

        #endregion
        private void InternalStartup()
        {
            Startup += new System.EventHandler(ThisAddIn_Startup);
            Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
            //AddinResource.Culture = new CultureInfo("ja-JP");
            this.Application.WorkbookOpen += OnWorkbook_Open;
        }


        private void Application_SheetChange(object targetSheet, Excel.Range target)
        {
            string FileName = ExcelUtil.GetActiveWorkBookName();
            Excel.Worksheet sheet = (Excel.Worksheet)targetSheet;
            if (!Definitions.ExtentionList.Contains(System.IO.Path.GetExtension(FileName.ToLower())))
            {
                //sheet.Application.EnableEvents = false;
                return;
            }
            if (workBook != null)
            {
                Excel.Worksheet seSheet = ExcelUtil.GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.DetailsSetting);
                Excel.Range start = seSheet.Range[Constants.AdvancedSettings.StartCell];
                Excel.Range end = ExcelUtil.EndxlUp(start);
                Excel.Range total = seSheet.get_Range(start, end);
                Excel.Range gmCheck = total.Find("F_Global_Mode_Settings", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                            Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);

                QC4Common.Common.Constants.GlobalMode = gmCheck.Offset[0, 1].Text;

                AddinResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
                QC4Common.CommonResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
                if (QC4Ribbon.Rbn != null)
                {
                    QC4Ribbon.Rbn.Menu.Label = AddinResource.Menu;
                    QC4Ribbon.Rbn.buttonMenu.Label = AddinResource.buttonMenu;
                    QC4Ribbon.Rbn.Common.Label = AddinResource.Common;
                    QC4Ribbon.Rbn.buttonInsert.Label = AddinResource.buttonInsert;
                    QC4Ribbon.Rbn.buttonDelete.Label = AddinResource.buttonDelete;
                    QC4Ribbon.Rbn.buttonCheck.Label = AddinResource.buttonCheck;
                    QC4Ribbon.Rbn.QSettings1.Label = AddinResource.QSettings1;
                    QC4Ribbon.Rbn.buttonVEM.Image = AddinResource.edit_start_hover;
                    if (QC4Ribbon.IsEditMode)
                    {
                        QC4Ribbon.Rbn.buttonVEM.Label = AddinResource.QS_STOP_VARIABLE_EDIT_MODE;
                        QC4Ribbon.Rbn.buttonVEM.Image = AddinResource.edit_stop_hover;
                    }
                    else
                        QC4Ribbon.Rbn.buttonVEM.Label = AddinResource.QS_START_VARIABLE_EDIT_MODE;
                    QC4Ribbon.Rbn.buttonEQ.Label = AddinResource.buttonEQ;
                    QC4Ribbon.Rbn.QSettings2.Label = AddinResource.QSettings2;
                    QC4Ribbon.Rbn.buttonCheckQS.Label = AddinResource.buttonCheckQS;
                    QC4Ribbon.Rbn.buttonJump.Label = AddinResource.buttonJump;
                    QC4Ribbon.Rbn.DProcess1.Label = AddinResource.DProcess1;
                    QC4Ribbon.Rbn.buttonUp.Label = AddinResource.buttonUp;
                    QC4Ribbon.Rbn.buttonDown.Label = AddinResource.buttonDown;
                    QC4Ribbon.Rbn.buttonCopy.Label = AddinResource.buttonCopy;
                    QC4Ribbon.Rbn.buttonPaste.Label = AddinResource.buttonPaste;
                    QC4Ribbon.Rbn.buttonExecuteDP.Label = AddinResource.buttonExecuteDP;
                    QC4Ribbon.Rbn.DProcess2.Label = AddinResource.DProcess2;
                    QC4Ribbon.Rbn.checkBoxCross.Label = AddinResource.checkBoxCross;
                    QC4Ribbon.Rbn.checkBoxList.Label = AddinResource.checkBoxList;
                    QC4Ribbon.Rbn.CTab1.Label = AddinResource.CTab1;
                    QC4Ribbon.Rbn.buttonOptionsCT.Label = AddinResource.buttonOptionsCT;
                    QC4Ribbon.Rbn.buttonCT.Label = AddinResource.buttonCT;
                    QC4Ribbon.Rbn.buttonChart.Label = AddinResource.buttonChart;
                    QC4Ribbon.Rbn.CTab2.Label = AddinResource.CTab2;
                    QC4Ribbon.Rbn.buttonStatus.Label = AddinResource.buttonStatus;
                    QC4Ribbon.Rbn.labelOutput.Label = AddinResource.labelOutput;
                    QC4Ribbon.Rbn.checkBoxCols.Label = AddinResource.checkBoxCols;
                    QC4Ribbon.Rbn.checkBoxRows.Label = AddinResource.checkBoxRows;
                    QC4Ribbon.Rbn.Summary.Label = AddinResource.Summary;
                    QC4Ribbon.Rbn.buttonOptionsSummary.Label = AddinResource.buttonOptionsSummary;
                    QC4Ribbon.Rbn.buttonOutput.Label = AddinResource.buttonOutput;
                    QC4Ribbon.Rbn.GT1.Label = AddinResource.GT1;
                    QC4Ribbon.Rbn.buttonExecuteGT.Label = AddinResource.buttonExecuteGT;
                    QC4Ribbon.Rbn.buttonOptionsGT.Label = AddinResource.buttonOptionsGT;
                    QC4Ribbon.Rbn.buttonAutoSettings.Label = AddinResource.buttonAutoSettings;
                    QC4Ribbon.Rbn.GT2.Label = AddinResource.GT2;
                    QC4Ribbon.Rbn.checkBoxLevel1.Label = AddinResource.checkBoxLevel1;
                    QC4Ribbon.Rbn.checkBoxLevel5.Label = AddinResource.checkBoxLevel5;
                    QC4Ribbon.Rbn.checkBoxLevel10.Label = AddinResource.checkBoxLevel10;
                    QC4Ribbon.Rbn.labelAlert.Label = AddinResource.labelAlert;
                    QC4Ribbon.Rbn.FA1.Label = AddinResource.FA1;
                    QC4Ribbon.Rbn.buttonExecute.Label = AddinResource.buttonExecute;
                    QC4Ribbon.Rbn.FA2.Label = AddinResource.FA2;
                    QC4Ribbon.Rbn.checkBoxSort.Label = AddinResource.checkBoxSort;
                    QC4Ribbon.Rbn.buttonUndoDP.Label = AddinResource.BTN_UNDO;
                }
            }
            switch (sheet.CodeName)
            {
                case Common.Constants.SheetType.GTCounting:
                    if (target.Row <= 4)
                    {
                        Application.EnableEvents = false;
                        try
                        {
                            Application.Undo();
                        }
                        catch
                        {
                            target.ClearContents();
                        }
                        Application.EnableEvents = true;
                        MessageDialog.ErrorOk(AddinResource.ALERT_HEADER_DELETE);
                        break;
                    }
                    if (((target.Row + target.Rows.Count) - 1) > Qc4CommonConstants.ExcelRowColumnMax.ExcelCommonMaxRowLimit) //Redmine Id: 279106
                    {
                        Application.EnableEvents = false;
                        target.ClearContents();
                        Excel.Range usedRange = sheet.UsedRange; // UsedRange再評価
                        Application.EnableEvents = true;

                        MessageDialog.ErrorOk(AddinResource.SHEET_MAX_ROW_NO_EXCEED);
                        break;
                    }
                    ExcelAddIn.Common.ExcelUtil.pastespecialcheck(target);//for resolve right clik paste issue
                    Common.GTSheetChange.GTValueChange(sheet, target);
                    SetFontDefault(target);
                    break;
                case Common.Constants.SheetCodeName.DataProcess:
                    if (target.Row <= 4)
                    {
                        Application.EnableEvents = false;
                        try
                        {
                            Application.Undo();
                        }
                        catch
                        {
                            target.ClearContents();
                        }
                        Application.EnableEvents = true;
                        MessageDialog.ErrorOk(AddinResource.ALERT_HEADER_DELETE);
                        break;
                    }

                    if (((target.Row + target.Rows.Count) - 1) > Qc4CommonConstants.ExcelRowColumnMax.ExcelCommonMaxRowLimit) //Redmine Id: 279106
                    {
                        Application.EnableEvents = false;
                        target.ClearContents();
                        Excel.Range usedRange = sheet.UsedRange; // UsedRange再評価
                        Application.EnableEvents = true;

                        MessageDialog.ErrorOk(AddinResource.SHEET_MAX_ROW_NO_EXCEED);
                        break;
                    }
                    ExcelAddIn.Common.ExcelUtil.pastespecialcheck(target);
                    foreach (Excel.Range x in target)
                    {
                        if (x.Errors[Excel.XlErrorChecks.xlNumberAsText].Value)
                        {
                            try
                            {
                                x.Errors.Item[Excel.XlErrorChecks.xlNumberAsText].Ignore = true;
                            }
                            catch { }
                        }
                    }
                    SetFontDefault(target);
                    break;
                case Common.Constants.SheetType.sh_QuesSetting:
                    if (target.Row == 1 || target.Row == 3 || Application.Intersect(target, sheet.Range["A2:K2"]) != null || Application.Intersect(target, sheet.Range["M2:XFD2"]) != null)
                    {
                        Application.EnableEvents = false;
                        try
                        {
                            Application.Undo();
                        }
                        catch
                        {
                            target.ClearContents();
                        }
                        Application.EnableEvents = true;
                        MessageDialog.ErrorOk(AddinResource.ALERT_HEADER_DELETE);
                        break;
                    }
                    if (target.Row > QC4Common.Common.Constants.ExcelRowColumnMax.ExcelQsMaxRowLimit)
                    {
                        Application.EnableEvents = false;
                        try
                        {
                            Application.Undo();
                        }
                        catch
                        {
                            target.ClearContents();
                        }
                        Application.EnableEvents = true;
                        MessageDialog.ErrorOk(AddinResource.QS_MAX_NO_EXCEED);
                        break;
                    }
                    ExcelAddIn.Common.ExcelUtil.pastespecialcheck(target);
                    Common.QSSheetChange.QSValueChange(sheet, target);
                    if (DataProcessSheet != null)
                        TriggerUpdateTaskPane();

                    SetFontDefault(target);
                    break;
                case Common.Constants.SheetType.sh_CrossCounting:
                    if (target.Row <= 2 || Application.Intersect(target, sheet.Range["A3:F13"]) != null)
                    {
                        Application.EnableEvents = false;
                        try
                        {
                            Application.Undo();
                        }
                        catch
                        {
                            target.ClearContents();
                        }
                        Application.EnableEvents = true;
                        MessageDialog.ErrorOk(AddinResource.ALERT_HEADER_DELETE);
                        break;
                    }
                    if (((target.Row + target.Rows.Count) - 1) > Qc4CommonConstants.ExcelRowColumnMax.ExcelCommonMaxRowLimit) //Redmine Id: 279106
                    {
                        Application.EnableEvents = false;
                        target.ClearContents();
                        Excel.Range usedRange = sheet.UsedRange; // UsedRange再評価
                        Application.EnableEvents = true;

                        MessageDialog.ErrorOk(AddinResource.SHEET_MAX_ROW_NO_EXCEED);
                        break;
                    }
                    ExcelAddIn.Common.ExcelUtil.pastespecialcheck(target);
                    Common.CrossChange.CRValueChange(sheet, target);
                    SetFontDefault(target);
                    break;
                case Common.Constants.SheetType.sh_SummaryList:
                    if (target.Row <= 2 || Application.Intersect(target, sheet.Range["A3:G7"]) != null)
                    {
                        Application.EnableEvents = false;
                        try
                        {
                            Application.Undo();
                        }
                        catch
                        {
                            target.ClearContents();
                        }
                        Application.EnableEvents = true;
                        MessageDialog.ErrorOk(AddinResource.ALERT_HEADER_DELETE);
                        break;
                    }
                    if (((target.Row + target.Rows.Count) - 1) > Qc4CommonConstants.ExcelRowColumnMax.ExcelCommonMaxRowLimit) //Redmine Id: 279106
                    {
                        Application.EnableEvents = false;
                        target.ClearContents();
                        Excel.Range usedRange = sheet.UsedRange; // UsedRange再評価
                        Application.EnableEvents = true;

                        MessageDialog.ErrorOk(AddinResource.SHEET_MAX_ROW_NO_EXCEED);
                        break;
                    }
                    ExcelAddIn.Common.ExcelUtil.pastespecialcheck(target);
                    Common.SLChange.SLValueChange(sheet, target);
                    SetFontDefault(target);
                    break;
                case Common.Constants.SheetType.SH_FAList:
                    if (target.Row <= 4)
                    {
                        Application.EnableEvents = false;
                        try
                        {
                            Application.Undo();
                        }
                        catch
                        {
                            target.ClearContents();
                        }
                        Application.EnableEvents = true;
                        MessageDialog.ErrorOk(AddinResource.ALERT_HEADER_DELETE);
                        break;
                    }
                    if (((target.Row + target.Rows.Count) - 1) > Qc4CommonConstants.ExcelRowColumnMax.ExcelCommonMaxRowLimit) //Redmine Id: 279106
                    {
                        Application.EnableEvents = false;
                        target.ClearContents();
                        Excel.Range usedRange = sheet.UsedRange; // UsedRange再評価
                        Application.EnableEvents = true;

                        MessageDialog.ErrorOk(AddinResource.SHEET_MAX_ROW_NO_EXCEED);
                        break;
                    }
                    ExcelAddIn.Common.ExcelUtil.pastespecialcheck(target);
                    Common.FAChanges.FAValueChange(sheet, target);
                    SetFontDefault(target);
                    break;
                case Common.Constants.SheetCodeName.Setting:
                    if (target.Row == QC4Common.Common.Constants.Setting.ModeRow && target.Column == QC4Common.Common.Constants.Setting.ModeCol)
                    {
                        if (Constants.Setting.MODE_PRO == target.Value)
                        {
                            target.Value = Constants.Setting.MODE_STD;
                            Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(workBook);
                            QuestionSettingsUtil.UpdateIdFlag();
                            Util.RibbonUtil.RibbonChange(null, Constants.SheetCodeName.QuestionSetting);
                            if (Globals.Ribbons.qc4Ribbon.RibbonUI != null)
                                Globals.Ribbons.qc4Ribbon.RibbonUI.ActivateTab("QC4");
                            Definitions.isQsUpdated = true;
                            Application.ScreenUpdating = true;
                            Application.EnableEvents = true;
                            Definitions.QsRowCount = ExcelUtil.GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.QuestionSetting).UsedRange.Rows.Count;
                            EnableTaskPane(false);
                        }
                        if (Constants.Setting.MODE_DATABROWSE == target.Value)
                        {
                            target.Value = Constants.Setting.MODE_STD;
                            Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(workBook);
                            QuestionSettingsUtil.UpdateIdFlag();
                            Util.RibbonUtil.RibbonChange(null, Constants.SheetCodeName.Data01);
                            IsDataBrowse = true;
                            if (Globals.Ribbons.qc4Ribbon.RibbonUI != null)
                                Globals.Ribbons.qc4Ribbon.RibbonUI.ActivateTab("QC4");
                            Definitions.isQsUpdated = true;
                            Definitions.QsRowCount = ExcelUtil.GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.QuestionSetting).UsedRange.Rows.Count;
                            EnableTaskPane(false);

                        }
                    }
                    if (target.Row == QC4Common.Common.Constants.Databrowsing.row && target.Column == QC4Common.Common.Constants.Databrowsing.colunm)
                    {
                        if (target != null)
                        {
                            bool errorinqs = false;
                            Util.RibbonUtil.RibbonChange(null, Constants.SheetCodeName.Data01);
                            Excel.Range range = sheet.Cells[QC4Common.Common.Constants.Databrowsing.row + 1, QC4Common.Common.Constants.Databrowsing.colunm];

                            if (!errorinqs)
                            {
                                if (target.Value == "datasheet")
                                {
                                    Excel.Worksheet s = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(Globals.ThisAddIn.workBook, Common.Constants.SheetCodeName.Data01);
                                    UpdateDataSheet(target.Application.ActiveWorkbook, s, true);

                                    target.Value = null;

                                }
                                if (target.Value == "dataaftersheet")
                                {

                                    Excel.Worksheet s = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(Globals.ThisAddIn.workBook, Constants.SheetType.sh_Data01 + "(Processed)");
                                    UpdateDataAfterSheet(target.Application.ActiveWorkbook, s, true);

                                    target.Value = null;

                                }
                            }
                        }

                    }
                    /*if (target.Row == QC4Common.Common.Constants.STD_DP.Execute_Row && target.Column == QC4Common.Common.Constants.STD_DP.Execute_Column)
                    {
                        if (target != null)
                        {
                            if (target.Value == QC4Common.Common.Constants.STD_DP.Execute_KeyWord)
                            {
                                try
                                {
                                    QC4Common.DP.CallDP calldp = new QC4Common.DP.CallDP();
                                    calldp.DPExecute(Globals.ThisAddIn.Application.ActiveWorkbook, true);
                                    Globals.ThisAddIn.DPExecute(true);
                                    target.Value = "";
                                    Excel.Worksheet settingsSheet = ExcelUtil.GetWorkSheetByCodeName(Globals.ThisAddIn.Application.ActiveWorkbook, Constants.SheetCodeName.Setting);
                                    Excel.Range listupCell = settingsSheet.Cells[QC4Common.Common.Constants.STD_DP.isListUP_Row, QC4Common.Common.Constants.STD_DP.isListUP_Col];
                                    listupCell.Value = false;
                                }
                                catch { }
                                finally
                                {

                                }
                            }
                        }

                    }*/
                    break;
                case Common.Constants.SheetType.Sh_LDEL:
                    if (Application.Intersect(target, sheet.Range["A1:A3"]) != null)
                    {

                        try
                        {
                            Application.EnableEvents = false;
                            try
                            {
                                Application.Undo();
                            }
                            catch
                            {
                                target.ClearContents();
                            }
                            Application.EnableEvents = true;
                            MessageDialog.ErrorOk(AddinResource.ALERT_HEADER_DELETE);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {

                            Application.EnableEvents = true;
                        }
                    }
                    else
                    {
                        ExcelAddIn.Common.ExcelUtil.pastespecialcheck(target);
                    }
                    SetFontDefault(target);
                    break;
                case Common.Constants.SheetCodeName.Data01:
                    DataHeaderDelete(target);
                    break;
                case Common.Constants.SheetCodeName.Multivariate01:
                    DataHeaderDelete(target);
                    break;
                default:
                    if (sheet.Name == Constants.SheetType.sh_Data01 + "(Processed)")
                    {
                        DataHeaderDelete(target);
                    }
                    break;
            }
        }


        private void SetFontDefault(Excel.Range target)
        {
            target.Font.Size = 9;
            target.Font.Name = "ＭＳ Ｐゴシック";
        }
        private void DataHeaderDelete(Excel.Range target)
        {
            if (target.Row <= 3)
            {
                try
                {
                    Application.EnableEvents = false;
                    Application.Undo();
                    Application.EnableEvents = true;
                    MessageDialog.ErrorOk(AddinResource.ALERT_HEADER_DELETE);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Application.EnableEvents = true;
                }
            }
            else
            {
                if (!Common.Definitions.isSheetUpdating)
                {
                    MessageDialog.ErrorOk(AddinResource.ALERT_DATA_SHEET_CHANGE);
                    datachanged = true;

                }
            }
        }

        void Application_SheetBeforeDoubleClick(object targetSheet, Excel.Range target, ref bool cancel)
        {
            Excel.Worksheet sheet = (Excel.Worksheet)targetSheet;
            switch (sheet.CodeName)
            {
                case Common.Constants.SheetType.GTCounting:
                    Common.Change.SheetBeforeDoubleClick(sheet, target);
                    cancel = true;
                    break;
                case Common.Constants.SheetType.sh_CrossCounting:
                    Common.CrossChange.SheetBeforeDoubleClick(sheet, target, ref cancel);
                    break;
                case Common.Constants.SheetType.sh_SummaryList:
                    Common.SLChange.SheetBeforeDoubleClick(sheet, target, ref cancel);
                    break;
                case Common.Constants.SheetType.SH_FAList:
                    Common.FAList.SheetBeforeDoubleClick(sheet, target);
                    cancel = true;
                    break;
                default:
                    break;
            }

            Util.RibbonUtil.EnableRibbon(cancel);
        }
        #endregion

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        BackgroundWorker backgroundWorker;
        public static bool iscancelled;
        private static AutoResetEvent _wait = new AutoResetEvent(false);
        /// <summary>
        /// Initiation of data process execution
        /// </summary>
        /// <param name="fromSTD">Bool value indicate whether the execution event come from STD or PRO sheet</param>
        public void DPExecute(bool fromSTD = false)//191  made for 
        {

            try
            {
                object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;//191  coded for dpexecute 
                if (activeSheet != null)
                {
                    IntPtr freezeparentwindow = fromSTD ? ExcelAddIn.Common.Util.GetIntPtrWIndowHandleFromSettings() : GetForegroundWindow();
                    //Logic.DataProcessExecute.islistup = false;//for listup
                    Excel.Worksheet settingsSheet = ExcelUtil.GetWorkSheetByCodeName(Globals.ThisAddIn.Application.ActiveWorkbook, Constants.SheetCodeName.Setting);
                    Excel.Range listupCell = settingsSheet.Cells[QC4Common.Common.Constants.STD_DP.isListUP_Row, QC4Common.Common.Constants.STD_DP.isListUP_Col];
                    if (listupCell.Value == null)
                        QC4Common.Logic.DataProcessExecute.islistup = false;
                    else
                        QC4Common.Logic.DataProcessExecute.islistup = listupCell.Value;
                    Excel.Worksheet worksheet = (Excel.Worksheet)activeSheet;
                    Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(workBook);

                    if (DataProcessSheet == null)
                    {
                        worksheet = ExcelUtil.GetWorkSheetByCodeName(Constants.SheetCodeName.DataProcess);
                        DataProcessSheet = new QC4Common.Sheets.DataProcess(worksheet);
                    }
                    if (fromSTD)
                    {
                        ExcelOperate excelOperate = null;
                        Excel.Application xlApp = null;
                        try
                        {
                            if (ThisAddIn.HWND_MAIN == 0)
                            {
                                ThisAddIn.HWND_MAIN = ExcelAddIn.Common.Util.GetWIndowHandleFromSettings();
                            }
                            worksheet = ExcelUtil.GetWorkSheetByCodeName(Constants.SheetCodeName.DataProcessS);
                            backgroundWorker = new BackgroundWorker();
                            ProgressBar progress = new ProgressBar(worksheet, backgroundWorker);
                            WindowInteropHelper wih = new WindowInteropHelper(progress);
                            wih.Owner = new IntPtr(worksheet.Application.Hwnd);
                            worksheet.Application.Interactive = false;
                            if (fromSTD)
                            {
                                WindowInteropHelper wihprogress = new WindowInteropHelper(progress);
                                IntPtr hForeGroundWindw = fromSTD ? ExcelAddIn.Common.Util.GetIntPtrWIndowHandleFromSettings() : GetForegroundWindow();
                                wihprogress.Owner = hForeGroundWindw;
                                SetParent(progress.hWnd, hForeGroundWindw);
                                EnableWindow(freezeparentwindow, false);
                            }
                            QC4Common.Global.Global.CheckOperationFlag = 0;
                            Microsoft.Office.Interop.Excel.Worksheet settingssheet = ExcelUtil.GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.Setting);
                            Microsoft.Office.Interop.Excel.Range dpcell = settingssheet.Cells[QC4Common.Common.Constants.STD_DP.CheckList_Row, QC4Common.Common.Constants.STD_DP.CheckList_Column];

                            if (Convert.ToString(dpcell.Value) == QC4Common.Common.Constants.STD_DP.CheckList_ON)
                            {
                                QC4Common.Global.Global.CheckOperationFlag |= QC4Common.Common.Constants.CheckCrossFlag.ChecklistSTD;
                            }
                            // new Thread(() => DataProcessSheet.DataProcessExecute(worksheet, progress, fromSTD)).Start();
                            IntPtr hForeGroundWnd = ExcelAddIn.Common.Util.GetIntPtrWIndowHandleFromSettings();// GetForegroundWindow();
                            backgroundWorker.WorkerReportsProgress = true;
                            backgroundWorker.WorkerSupportsCancellation = true;
                            backgroundWorker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                            {

                                try
                                {
                                    excelOperate = new ExcelOperate();
                                    xlApp = excelOperate.Excel;
                                    DataProcessSheet.DataProcessExecute(worksheet, ref xlApp, sender, e, _wait, progress, fromSTD);
                                    //  IntPtr hForeGroundWnd = ExcelAddIn.Common.Util.GetIntPtrWIndowHandleFromSettings();// GetForegroundWindow();

                                    // progress.Dispatcher.Invoke(() =>
                                    //   QC4Common.Common.MessageDialog.ShowMessageOnParent(AddinResource.DATAPROCESS_COMPLETED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));

                                    if (backgroundWorker.CancellationPending)
                                    {

                                        progress.Dispatcher.Invoke(() =>
                                           QC4Common.Common.MessageDialog.ShowMessageOnParent(AddinResource.MSG_OUTPUT_ABORTED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));

                                        progress.OnWorkerMethodComplete(100, "", IsForceStop: true);
                                        int msg = RegisterWindowMessage(QC4Common.Common.Constants.RibbonMessage.msg_cancelled);
                                        if (msg == 0)
                                        {
                                            MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                                        }
                                        SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, 0, 0);
                                        if (workBook != null)
                                        {
                                            QC4Common.Util.ExcelUtil.DeleteDataAfterSheets(workBook);

                                        }
                                        string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + "Comparison GT.xlsx"; //GetTemplatePath(TEMPLATE_NAME);  
                                        string targetPath = QC4Common.Util.QCUtil.GetTargetPath(QC4Common.Sheets.DataProcess.currworkbook);
                                        targetPath = Path.GetDirectoryName(targetPath) + "\\";
                                        targetPath.Replace("\\\\", "\\");

                                        string fileopenPath = QC4Common.Util.QCUtil.GetTempPath(QC4Common.Sheets.DataProcess.currworkbook);
                                        fileopenPath = Path.GetDirectoryName(fileopenPath) + "\\";
                                        fileopenPath.Replace("\\\\", "\\");

                                        string saveAsPath = targetPath + AddinResource.COMPARISON_FILE_NAME + ".xlsx";
                                        string savefileopenPath = fileopenPath + AddinResource.COMPARISON_FILE_NAME + ".xlsx";

                                        if (File.Exists(saveAsPath))
                                        {
                                            File.Delete(saveAsPath);
                                        }
                                        if (File.Exists(savefileopenPath))
                                        {
                                            File.Delete(savefileopenPath);
                                        }

                                        using (SQLiteConnection dbSource = DB.DBHelper.GetConnection(DB.DBHelper.GetConnectionString(QC4Common.Sheets.DataProcess.Sheet.Application.ActiveWorkbook)))
                                        {
                                            dbSource.Open();
                                            DB.DBHelper.DeleteDataAfterProcessTable(dbSource);
                                            dbSource.Close();
                                        }

                                        if (xlApp != null)
                                        {
                                            xlApp.EnableEvents = false;
                                            xlApp.DisplayAlerts = false;
                                            xlApp.Visible = false;
                                            xlApp.Workbooks.Close();
                                            xlApp.Quit();
                                        }

                                    }
                                    if (!backgroundWorker.CancellationPending)
                                    {

                                        if (QC4Common.Global.Global.CheckOperationFlag != 0)
                                        {
                                            int msg1 = RegisterWindowMessage(QC4Common.Common.Constants.RibbonMessage.msg_CheckCross);
                                            if (msg1 == 0)
                                            {
                                                MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                                            }
                                            SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg1, QC4Common.Global.Global.CheckOperationFlag, 0);
                                        }


                                        else
                                        {
                                            progress.Dispatcher.Invoke(() =>
                                           QC4Common.Common.MessageDialog.ShowMessageOnParent(AddinResource.DATAPROCESS_COMPLETED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));
                                        }
                                        progress.OnWorkerMethodComplete(100, AddinResource.DP_PROGRESS_MSG_95, retainThread: true, IsForceStop: true);
                                        if (xlApp != null && QC4Common.Logic.DataProcessExecute.islistup)
                                        {
                                            xlApp.Visible = true;
                                            xlApp.Application.WindowState = Excel.XlWindowState.xlMaximized;

                                        }
                                    }

                                }
                                catch { }
                                finally
                                {

                                }
                            }
                           );
                            backgroundWorker.RunWorkerCompleted += (sender, e) =>
                            {
                                if (excelOperate != null)
                                {
                                    COMWholeOperate.releaseComObject(ref excelOperate);
                                }
                                if (xlApp != null)
                                {
                                    COMWholeOperate.releaseComObject(ref xlApp);
                                }
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                            };

                            backgroundWorker.RunWorkerAsync();

                            progress.changeWIndowPos = true;
                            progress.ShowDialog();
                            try { worksheet.Application.Cursor = microsft.XlMousePointer.xlDefault; } catch { }
                            QC4Common.Sheets.DataProcess.isexecute = false;//Redmine id: 175141


                        }
                        catch { }
                        finally
                        {
                            EnableWindow(freezeparentwindow, true); SetForegroundWindow(freezeparentwindow);
                            iscancelled = true;
                            if (backgroundWorker.CancellationPending)
                            {
                                iscancelled = false;
                                // isExecuted = false;
                                //  iscancelled = true;
                            }

                        }

                    }
                    else if (worksheet.CodeName == Constants.SheetCodeName.DataProcess)
                    {


                        try { worksheet.Application.Cursor = microsft.XlMousePointer.xlDefault; } catch (Exception ex) { _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
                        QC4Common.Sheets.DataProcess.isexecute = true;//Redmine id: 175141
                                                                      //Progress bar changes
                        if (!DataProcessSheet.DPCheck())
                        {
                            return;
                        }
                        IntPtr hForeGroundWnd = ExcelAddIn.Common.Util.GetIntPtrWIndowHandleFromSettings();// GetForegroundWindow();
                        backgroundWorker = new BackgroundWorker();
                        ProgressBar progress = new ProgressBar(worksheet, backgroundWorker);
                        backgroundWorker.WorkerReportsProgress = true;
                        backgroundWorker.WorkerSupportsCancellation = true;
                        backgroundWorker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                        {
                            ExcelOperate excelOperate = null;
                            Excel.Application xlApp = null;

                            try
                            {
                                excelOperate = new ExcelOperate();
                                xlApp = excelOperate.Excel;
                                DataProcessSheet.DataProcessExecute(worksheet, ref xlApp, sender, e, _wait, progress);
                                //  IntPtr hForeGroundWnd = ExcelAddIn.Common.Util.GetIntPtrWIndowHandleFromSettings();// GetForegroundWindow();

                                // progress.Dispatcher.Invoke(() =>
                                //QC4Common.Common.MessageDialog.ShowMessageOnParent(AddinResource.DATAPROCESS_COMPLETED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));

                                if (backgroundWorker.CancellationPending)
                                {

                                    progress.Dispatcher.Invoke(() =>
                                       QC4Common.Common.MessageDialog.ShowMessageOnParent(AddinResource.MSG_OUTPUT_ABORTED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));

                                    progress.OnWorkerMethodComplete(100, "", IsForceStop: true);

                                    if (workBook != null)
                                    {

                                        QC4Common.Util.ExcelUtil.DeleteDataAfterSheets(workBook);
                                        string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + "Comparison GT.xlsx"; //GetTemplatePath(TEMPLATE_NAME);  
                                        string targetPath = QC4Common.Util.QCUtil.GetTargetPath(QC4Common.Sheets.DataProcess.currworkbook);
                                        targetPath = Path.GetDirectoryName(targetPath) + "\\";
                                        targetPath.Replace("\\\\", "\\");

                                        string fileopenPath = QC4Common.Util.QCUtil.GetTempPath(QC4Common.Sheets.DataProcess.currworkbook);
                                        fileopenPath = Path.GetDirectoryName(fileopenPath) + "\\";
                                        fileopenPath.Replace("\\\\", "\\");

                                        string saveAsPath = targetPath + AddinResource.COMPARISON_FILE_NAME + ".xlsx";
                                        string savefileopenPath = fileopenPath + AddinResource.COMPARISON_FILE_NAME + ".xlsx";

                                        if (File.Exists(saveAsPath))
                                        {
                                            File.Delete(saveAsPath);
                                        }
                                        if (File.Exists(savefileopenPath))
                                        {
                                            File.Delete(savefileopenPath);
                                        }

                                        if (xlApp != null)
                                        {
                                            xlApp.EnableEvents = false;
                                            xlApp.DisplayAlerts = false;
                                            xlApp.Visible = false;
                                            xlApp.Workbooks.Close();
                                            xlApp.Quit();
                                        }
                                    }
                                    using (SQLiteConnection dbSource = DB.DBHelper.GetConnection(DB.DBHelper.GetConnectionString(QC4Common.Sheets.DataProcess.Sheet.Application.ActiveWorkbook)))
                                    {
                                        dbSource.Open();
                                        DB.DBHelper.DeleteDataAfterProcessTable(dbSource);
                                        dbSource.Close();
                                    }
                                }

                                if (!backgroundWorker.CancellationPending)
                                {
                                    if (QC4Common.Global.Global.CheckOperationFlag != 0)
                                    {
                                        _log.Info("DP Ending");
                                        _log.Info("CheckCross Starting");
                                        int msg = RegisterWindowMessage(QC4Common.Common.Constants.RibbonMessage.msg_CheckCross);
                                        if (msg == 0)
                                        {
                                            MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                                        }
                                        SendNotifyMessage(/*HWND_BROADCAST*/ThisAddIn.HWND_MAIN, msg, QC4Common.Global.Global.CheckOperationFlag, 0);
                                    }
                                    else
                                    {
                                        _log.Info("DP Ending");
                                        progress.Dispatcher.Invoke(() =>
                                      QC4Common.Common.MessageDialog.ShowMessageOnParent(AddinResource.DATAPROCESS_COMPLETED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));
                                        //  QC4Common.Common.MessageDialog.ShowMessageOnParent(AddinResource.DATAPROCESS_COMPLETED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd);//Redmine id:209279
                                    }
                                    progress.OnWorkerMethodComplete(100, AddinResource.DP_PROGRESS_MSG_95, retainThread: true, IsForceStop: true);
                                    if (xlApp != null && QC4Common.Logic.DataProcessExecute.islistup)
                                    {
                                        xlApp.Visible = true;
                                        xlApp.Application.WindowState = Excel.XlWindowState.xlMaximized;

                                    }
                                }

                            }
                            catch { }
                            finally
                            {
                                if (excelOperate != null)
                                {
                                    COMWholeOperate.releaseComObject(ref excelOperate);
                                }
                                if (xlApp != null)
                                {
                                    COMWholeOperate.releaseComObject(ref xlApp);
                                }
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                            }

                        }
                       );
                        backgroundWorker.RunWorkerAsync();
                        progress.changeWIndowPos = false;
                        progress.ShowDialog();
                        try { worksheet.Application.Cursor = microsft.XlMousePointer.xlDefault; } catch { }
                        QC4Common.Sheets.DataProcess.isexecute = false;//Redmine id: 175141
                    }
                }

                Excel.Worksheet ws = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workBook, Constants.SheetType.sh_Data01 + "(Processed)");
                if (ws == null)
                {
                    Globals.Ribbons.qc4Ribbon.buttonUndoDP.Enabled = false;
                }
                else
                {
                    Globals.Ribbons.qc4Ribbon.buttonUndoDP.Enabled = true;
                }
            }
            catch (Exception ex) { }
            finally
            {

            }
        }
        public bool isExecuted { get; set; }
        public void DPCheck()//191  made for check impl
        {
            object activeSheet = Globals.ThisAddIn.Application.ActiveWindow.ActiveSheet;
            if (activeSheet != null)
            {
                Excel.Worksheet worksheet = (Excel.Worksheet)activeSheet;
                if (worksheet.CodeName == Constants.SheetCodeName.DataProcess)
                {
                    if (DataProcessSheet == null)
                    {
                        DataProcessSheet = new QC4Common.Sheets.DataProcess(worksheet);
                    }
                    if (DataProcessSheet.DPCheck()) MessageDialog.Info(AddinResource.CHECK_VALIDATION_SUCCESS); ;
                    worksheet.Application.Cursor = microsft.XlMousePointer.xlDefault;
                }
            }
        }
        private static void UpdateMultivariateSheet(Excel.Workbook workBook, Excel.Worksheet sheet, bool DataBrowse = false)
        {
            try
            {

                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlWait;
                workBook.Application.EnableEvents = false;
                workBook.Application.ScreenUpdating = false;
                // if (!QC4Common.Common.CommonFlag.IsDataAfterUpdated(Globals.ThisAddIn.Application.ActiveWorkbook))
                {
                    DataSheetUpdate du = new DataSheetUpdate(Globals.ThisAddIn.Application, Globals.ThisAddIn.Application.ActiveWorkbook, Globals.ThisAddIn.Application.ActiveSheet);
                    var array = Definitions.VariableDictionary.Where(a => (a.Value.QuestionFlag == "An" || a.Value.Variable.Equals("SAMPLEID"))).Select(q => q.Value).ToList();
                    du.LoadDataWithPB("multivariate", array, DataBrowse, true);
                    //  QC4Common.Common.CommonFlag.SetIsDataAfterUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, true);
                }
                // Common.Definitions.isSheetUpdating = false;
                workBook.Application.ScreenUpdating = true;
                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlDefault;
                workBook.Application.EnableEvents = true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                Globals.ThisAddIn.Application.Cursor = Excel.XlMousePointer.xlDefault;
            }
        }
        /* Excel.Range dpstart = DataProcess.Sheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.OnOffColumn];
            Excel.Range lastcell = ExcelUtil.EndxlUp(DataProcess.Sheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.OnOffColumn]);
            this.crit_inst_operator_Dict = dpExecObject.GetinstructionsByRow(dpstart, lastcell);
            if (ValidateCellValuesBforBtnClick(this.crit_inst_operator_Dict) == false) return;*/

        public static void TriggerUpdateTaskPane()
        {
            DataProcessSheet = new QC4Common.Sheets.DataProcess(wsData_Process);
            DataProcessSheet.FillDataInDocpane(CustomControlPane);
        }

    }
}
