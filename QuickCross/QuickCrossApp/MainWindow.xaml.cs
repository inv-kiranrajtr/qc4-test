using Microsoft.Win32;
using Qc4Launcher.Classes;
using Qc4Launcher.Forms;
using Qc4Launcher.Logic;
using Qc4Launcher.Summary;
using Qc4Launcher.Logic.Gross_Tabulation;
using Qc4Launcher.Util;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using static Qc4Launcher.Util.Constants;
using Excel = Microsoft.Office.Interop.Excel;
using ProgressBar = Qc4Launcher.Forms.ProgressBar;
using Constant = QC4Common.Common.Constants;
using Qc4Launcher.Logic.DPCheckList;
using QC4Common.Util;
using ExcelUtil = QC4Common.Util.ExcelUtil;
using dbHelper = QC4Common.DB.DBHelper;
using log4net;
using System.Reflection;
using excel = Microsoft.Office.Interop.Excel;
using Macromill.QCWeb.COMOperate;
using System.Collections.Generic;
using QC4Common.Model;
using CommonFunctions = QC4Common.Common.CommonFunctions;
using QC4Common.Logic;
using Qc4Launcher.Forms.GrossTabulationSetting;
using Qc4Launcher.Forms.DP_Main;
using Qc4Launcher.Forms.Cross_Tabulation;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Qc4Launcher.Forms.FA;
using System.ComponentModel;
using NPOI.SS.Formula.Functions;
using Qc4Launcher.Forms.Multivariate_Analysis;
using ExcelAddIn;
using NPOI.SS.Formula;
using Qc4Launcher.Forms.GrossTabulationSetting.Common;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using QC4Common.Classes.HatchColor;
using Microsoft.Office.Core;

namespace Qc4Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static FileStream fileloack = null;
        public static bool File_read_only = false;
        public static bool crossflag = false;
        private string SelectedFile = "";
        Excel.Worksheet ActivatedSheet;
        private Excel.Application excelApp;
        private Excel.Workbook excelWorkbook;
        private ProgressBar progress = null;
        BackgroundWorker CheckListCheckCrossBgWorkser;
        private string TempPath = null;
        private bool firsttime = true;
        private static Excel.Range modeCell;
        private Logger.Log LogObj;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static ExcelOperate excelOperate;// = new ExcelOperate();
        public static bool processRunning = false;
        bool isExit = false;
        IntPtr checkcrossintptr = default(IntPtr);
        public bool isExecutedDataProcess = false;
        public static bool isExecuteClicked = false;
        public static bool ismultivariate = false;
        private string OpendfileHeading = string.Empty;
        public static bool qc4FileActivestate = false;
        public string appinfo = string.Empty;
        bool IsUpdatevariabledic = false;
        public static bool integritycheck = true;
        bool MWDLoaded = false;
        Excel.Application xlapp1;
        public MainWindow()
        {
            MWDLoaded = true;
            LogObj = new Logger.Log();

            InitializeComponent();
            ChangeButtonState(false);

            xlapp1 = new excel.Application();
            FileVersionInfo ver = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            CopyTemplates();
            LogObj.WriteLog("APP-START", "Starting application");
            string sysinfo = "OS Version-- " + Environment.OSVersion.VersionString;

            sysinfo += Environment.Is64BitOperatingSystem ? "\t64 Bit " : "\t32 Bit";
            sysinfo += "\tSystemName--" + Environment.MachineName;
            LogObj.WriteLog("APP-START", sysinfo);

            LogObj.WriteLog("APP-START", "Process ID :" + Process.GetCurrentProcess().Id.ToString());
            LogObj.WriteLog("APP-START", "App Version:" + ver.ProductVersion);

            btn_Swap_Data.Visibility = Visibility.Hidden;
            btn_ToSheet.Visibility = Visibility.Hidden;

            ChangeVersionText();

            ParseColorPresetXml();
        }
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        public static bool isprocesssed = false;
        BackgroundWorker newThread;
        public void MulivariateNewVariableCheck()
        {
            newThread = new BackgroundWorker();
            newThread.WorkerReportsProgress = true;
            newThread.WorkerSupportsCancellation = true;
            newThread.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
            {
                if (Definiotion.VariableDictionary.Values.ToList().Any(q => q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.New))
                {
                    if (Definiotion.VariableDictionary.Values.Count() > 0)
                    {
                        List<QuestionSettings> questions = new List<QuestionSettings>();
                        questions = Definiotion.VariableDictionary.Values.ToList();
                        isprocesssed = false;
                        questions = questions.Where(x => x.QuestionFlag != QC4Common.Common.Constants.QuestionFlag.An).ToList();
                        DataTable dt = new DataTable();
                        List<string> DataAftrVar = new List<string>();

                        int rowCount = frmutil.IsDataProcessedMulti(excelWorkbook);
                        int qscount = Definiotion.VariableDictionary.Values.Count();
                    }
                }
            }
            );
            newThread.RunWorkerAsync();
        }
        private void ChangeVersionText()
        {
            txt_version.Text = string.Format("v{0}.{1}.{2}", Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(), Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString(), Assembly.GetExecutingAssembly().GetName().Version.Build.ToString());
            //txt_version.Text = string.Format("v{0}", Assembly.GetExecutingAssembly().GetName().Version.Major.ToString());
            if (!CommonFunction.ActivationKeyChecking())
            {
                txtExpiry.Visibility = Visibility.Hidden;
                txtVersion.Visibility = Visibility.Hidden;

            }
            else
            {
                txtExpiry.Visibility = Visibility.Visible;
                txtExpiry.Text = LocalResource.LABEL_VERSION_DATE + ": " + CommonFunction.ExpiryDate.ToString("yyyy/MM/dd");
                txtVersion.Visibility = Visibility.Visible;
                txtVersion.Text = LocalResource.LABEL_VERSION_TYPE;
            }
        }

        #region Functions

        private void ChangeButtonState(Boolean state)
        {
            try
            {
                btn_ToSheet.IsEnabled = state;
                btn_Close_File.IsEnabled = state;
                btn_Swap_Data.IsEnabled = state;
                btn_Save.IsEnabled = state;
                btn_Save_As.IsEnabled = state;
                btn_Open.IsEnabled = !state;
                txt_FileName.IsReadOnly = state;
                //new buttons
                btn_Question_setting.IsEnabled = state;
                btn_Data_Browsing.IsEnabled = state;
                btn_Data_Processing.IsEnabled = state;
                btn_DataOutput.IsEnabled = state;

                btn_Multivaries.IsEnabled = state;
                btn_Gttable.IsEnabled = state;
                btn_Cross_table_report.IsEnabled = state;
                btn_Falist.IsEnabled = state;
                qc4FileActivestate = state;
                if (state == false)
                {
                    isExecutedDataProcess = false;
                }
            }
            catch
            {
                Dispatcher.InvokeAsync(() =>
                {
                    btn_ToSheet.IsEnabled = state;
                    btn_Close_File.IsEnabled = state;
                    btn_Swap_Data.IsEnabled = state;
                    btn_Save.IsEnabled = state;
                    btn_Save_As.IsEnabled = state;
                    btn_Open.IsEnabled = !state;
                    txt_FileName.IsReadOnly = state;
                    //new buttons
                    btn_Question_setting.IsEnabled = state;
                    btn_Data_Browsing.IsEnabled = state;
                    btn_Data_Processing.IsEnabled = state;
                    btn_DataOutput.IsEnabled = state;
                    btn_Multivaries.IsEnabled = state;
                    btn_Gttable.IsEnabled = state;
                    btn_Cross_table_report.IsEnabled = state;
                    btn_Falist.IsEnabled = state;
                    qc4FileActivestate = state;
                    if (state == false)
                    {
                        isExecutedDataProcess = false;
                    }
                }, DispatcherPriority.ApplicationIdle);


                //
                //btn_Quesion_Setting.IsEnabled = state;
            }

        }

        #endregion Functions

        #region Events
        private void btn_Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_FileName.Text == Constants.HiddenAuthKey)
                {
                    ShowACTWindow();
                }
                else
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Multiselect = false;
                    LogObj.WriteLog("Open_Click", "Starting application");
                    ofd.Filter = FileSettings.MainWindowFileFilter;
                    if (ofd.ShowDialog() == true)
                    {
                        if (string.IsNullOrEmpty(ofd.FileName))
                        {
                            MessageDialog.Warning(LocalResource.MSG_FILE_CANNOT_PROCESS);
                        }
                        else
                        {
                            SelectedFile = ofd.FileName;
                            Qc4FileName = SelectedFile;
                            FileOpen();
                            if (xlapp1 == null)
                            {

                                xlapp1 = new excel.Application();
                            }
                        }

                    }
                }
            }
            catch { }
        }

        private void FileOpen()
        {
            txt_FileName.Text = SelectedFile;
            LogObj.WriteLog("Open_Click", "Selected File - " + SelectedFile);
            string targetFile = QcFileHelper.GetTempPath() + Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4";
            string fileName = Path.GetFileNameWithoutExtension(SelectedFile);
            if (fileName.StartsWith(";"))
            {
                MessageDialog.Warning(LocalResource.INVALID_FILENAME_START_SEMICOLEN);
                txt_FileName.Text = string.Empty;
                SelectedFile = string.Empty;
                return;
            }
            if (targetFile.Length > 260)
            {
                MessageDialog.Warning(LocalResource.MSG_PATH_TOO_LONG);
                txt_FileName.Text = string.Empty;
                SelectedFile = string.Empty;
                return;
            }

            FileInfo Qcfile = new FileInfo(SelectedFile);
            QC4Common.Util.FileHelper fileHelper = new QC4Common.Util.FileHelper();
            if (fileHelper.IsFileLocked(Qcfile) && Path.GetExtension(SelectedFile).ToLower().Equals(Constant.Qc4Extension) || (Qcfile.IsReadOnly && Path.GetExtension(SelectedFile).ToLower().Equals(Constant.Qc4Extension)))
            {
                if (Directory.Exists(targetFile))
                {
                    if (Path.GetExtension(SelectedFile).ToLower() == Constant.Qc4Extension)
                    {
                        MessageDialog.ErrorOk(LocalResource.MW_ALERT_ALREADY_OPENED);
                        CloseFile(false);
                        this.Cursor = Cursors.Arrow;
                        return;
                    }
                }

                string userName = Environment.UserName;
                MessageBoxResult result;
                if (!Qcfile.IsReadOnly)
                    result = MessageBox.Show(String.Format(LocalResource.FILE_READONLY_OPEN_MSG, fileName + ".qc4", userName), "QuickCross", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
                else
                    result = MessageBox.Show(LocalResource.FILE_READONLY_ATTRIBUTE_FILE_OPEN_MSG, "QuickCross", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.No || result == MessageBoxResult.Cancel)
                {
                    txt_FileName.Text = string.Empty;
                    SelectedFile = string.Empty;
                    return;
                }
                File_read_only = true;
            }

            if (Directory.Exists(targetFile))
            {
                LogObj.WriteLog("Open ", " Temp path already found : " + targetFile);
                try
                {
                    string excelFile = targetFile + "\\" + Constant.TemplateFile.QC4_Template;
                    excelFile = excelFile.Replace("\\\\", "\\");
                    File.Delete(excelFile);
                    Directory.Delete(targetFile, true);
                    LogObj.WriteLog("Open ", "Temp path deleted : " + targetFile);
                }
                catch (Exception ex)
                {
                    if (Path.GetExtension(SelectedFile).ToLower() == Constant.Qc4Extension)
                    {
                        MessageDialog.ErrorOk(LocalResource.MW_ALERT_ALREADY_OPENED);
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        CloseFile(false);
                        this.Cursor = Cursors.Arrow;
                        return;
                    }
                    else
                    {
                        targetFile = QcFileHelper.GenerateFileName(1, ".qc4", Path.GetFileNameWithoutExtension(SelectedFile), Path.GetDirectoryName(QcFileHelper.GetTempPath()));
                    }

                }
            }

            Directory.CreateDirectory(targetFile);
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
            if (myKey != null)
            {
                var value = (int)myKey.GetValue("LoadBehavior", RegistryValueKind.DWord);
                if (value != 3)
                {
                    if (System.Windows.Forms.DialogResult.Yes == MessageDialog.InfoYesNo(LocalResource.MAIN_MSG_APP_RESET))
                    {
                        myKey.SetValue("LoadBehavior", "3", RegistryValueKind.DWord);// to enable excel add-in, enable ribbon
                        myKey.Close();
                    }
                }
            }
            Thread thread = null;
            if (Path.GetExtension(SelectedFile).ToLower() == Constant.Qc4Extension)
            {
                QC4FileHelper fh = new QC4FileHelper();
                fh.OnWorkerComplete += new QC4FileHelper.OnWorkerMethodCompleteDelegate(OnWorkerMethodComplete);

                progress = new ProgressBar(LocalResource.FILE_OPEN);
                progress.Owner = this;

                thread = new Thread(() => fh.OpenFile(SelectedFile, ref excelApp, ref excelWorkbook, ref TempPath, ref excelOperate, this, switchedLang: SwitchedLang));
                thread.Start();
                progress.ShowDialog();
                if (TempPath == null)
                {

                    CloseFile(false);
                    try
                    {
                        Directory.Delete(targetFile, true);
                    }
                    catch { }
                    return;
                }
            }
            else
            {
                TempPath = targetFile;
                Qc3Parse parse = new Qc3Parse(this, SelectedFile, targetFile);
                excelWorkbook = parse.StartParsing(ref excelOperate);
                if (null == excelWorkbook)
                {
                    try
                    {
                        Directory.Delete(targetFile);
                    }
                    catch { }
                    CloseFile(false);
                    return;
                }
                excelApp = excelWorkbook.Application;
                SelectedFile = parse.TargetPath;
                txt_FileName.Text = parse.TargetPath;
                TempPath = parse.TempPath;
            }

            Definiotion.SelectedDir = Path.GetDirectoryName(SelectedFile);
            Definiotion.SelectedFile = Path.GetFileNameWithoutExtension(SelectedFile);
            ChangeButtonState(true);
            if (!CommonFunction.ActivationKeyChecking())
            {
                btn_Swap_Data.Visibility = Visibility.Hidden;
                btn_ToSheet.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_Swap_Data.Visibility = Visibility.Visible;
                btn_ToSheet.Visibility = Visibility.Visible;
            }

            Excel.Worksheet sSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.Setting);
            modeCell = sSheet.Cells[QC4Common.Common.Constants.Setting.ModeRow, QC4Common.Common.Constants.Setting.ModeCol];
            QC4Common.Common.CommonFlag.SetIsDataUpdated(excelWorkbook, false);
            QC4Common.Common.CommonFlag.SetIsDataAfterUpdated(excelWorkbook, false);
            QC4Common.Common.CommonFlag.SetIsMultivariateUpdated(excelWorkbook, false);
            excelApp.SheetActivate += ExcelApp_SheetActivate;
            Excel.Worksheet setting = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Util.Constants.SheetCodeName.Setting);
            Excel.Range servay = setting.Cells[QC4Common.Common.Constants.SourceSarvaytitile.servay_Row, QC4Common.Common.Constants.SourceSarvaytitile.servay_col];
            string data = Convert.ToString(servay.Value);
            if (data == null)
            {
                Excel.Worksheet qs = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Util.Constants.SheetCodeName.QuestionSetting);
                Excel.Range qstitle = qs.Cells[2, 12];
                servay.NumberFormat = "@";
                servay.Value = qstitle.Value;
            }
            ExcelUtil.SetChoices(excelWorkbook);
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Util.Constants.SheetCodeName.QuestionSetting);
            OpendfileHeading = s.Cells[2, 12].Value;

            //Redmine Id: 210036 start
            bool appevents = excelWorkbook.Application.EnableEvents;
            bool appdispalert = excelWorkbook.Application.DisplayAlerts;
            excelWorkbook.Application.EnableEvents = false;
            excelWorkbook.Application.DisplayAlerts = false;

            try//Redmine Id: 210036
            {
                Excel.Worksheet ws = ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, "Multivariate01");
                if (ws != null)
                {
                    excelWorkbook.Unprotect(Constants.Password);
                    ws.Name = Constants.SheetType.sh_Data_AN2;
                    excelWorkbook.Protect(Constants.Password);
                }
            }
            catch (Exception ex) { }

            try//Redmine ID 206014]
            {
                if (QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.MultiVariateSheet) != null && QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, Constants.SheetType.sh_Sheet2) == null)
                {
                    try
                    {
                        Excel.Worksheet multivariateSheet1 = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.MultiVariateSheet);
                        excelWorkbook.Unprotect(Constants.Password);
                        multivariateSheet1.Visible = Excel.XlSheetVisibility.xlSheetVisible;
                        multivariateSheet1.Copy(excelWorkbook.Sheets[1], Type.Missing);
                        excelWorkbook.Sheets[1].Name = Constants.SheetType.sh_Sheet2;// Constants.SheetType.sh_Sheet2;
                        excelWorkbook.Sheets[1].Move(Type.Missing, multivariateSheet1);
                        multivariateSheet1.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
                        // workbook.Save();
                        // multivariateSheet1 = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.MultiVariate);
                        Excel.Worksheet multivariateSheetMAS = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, Constants.SheetType.sh_Sheet2);
                        Excel.Range MASstart = multivariateSheetMAS.Cells[Qc4Launcher.Util.Constants.StartRow, Qc4Launcher.Util.Constants.StartCol];
                        Excel.Range MASlast = multivariateSheetMAS.Cells[Qc4Launcher.Util.Constants.EndRow, Qc4Launcher.Util.Constants.EndCol];
                        Excel.Range MASrar = multivariateSheetMAS.get_Range(MASstart, MASlast);
                        MASrar.Cells.ClearContents();
                        multivariateSheetMAS.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
                        excelWorkbook.Protect(Constants.Password);
                    }
                    catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
                    finally
                    {

                    }
                }

            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }




            excelWorkbook.Application.EnableEvents = appevents;
            excelWorkbook.Application.DisplayAlerts = appdispalert;
            //Redmine Id: 210036  end

            ExcelUtil.SetReadonlyMode(excelWorkbook, File_read_only);
            int indx = Combo_Language.SelectedIndex;
            if (QC4Common.Common.Constants.GlobalMode != "ja-JP," + Util.Constants.QCFont.MS_Gothic)
            {
                if (indx != 0)
                {
                    IsUpdateSheetLanguage = true;
                    Combo_Language.SelectedIndex = 0;
                }
                else
                    Combo_Language_SelectionChanged(null, null);
            }
            else
            {
                if (indx != 1)
                {
                    IsUpdateSheetLanguage = true;
                    Combo_Language.SelectedIndex = 1;
                }
                else
                    Combo_Language_SelectionChanged(null, null);
            }

            System.Threading.Tasks.Task.Run(() =>
            {
                if (thread != null)
                {
                    thread.Join();
                    if (!QC4FileHelper.IsSuccess)
                    {
                        CloseFile(false);
                        QC4FileHelper.IsSuccess = true;
                    }
                }
            });
        }

        private void ExcelApp_SheetActivate(object sheet)
        {
            ActivatedSheet = (Excel.Worksheet)sheet;
        }

        private void ShowACTWindow()
        {
            GenerateKey keyWindow = new GenerateKey();
            this.Hide();
            keyWindow.ShowDialog();
            this.Show();
            txt_FileName.Text = String.Empty;
            ChangeVersionText();
        }

        internal void EnableWindow()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                btn_ToSheet.IsEnabled = true;
                btn_Data_Browsing.IsEnabled = true;
                this.Show();
                this.WindowState = WindowState.Normal;
                this.Activate();
                txt_FileName.Text = SelectedFile;
            }));
        }

        private void btn_ToSheet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QC4Common.Common.CommonFlag.SetIsMultivariateUpdated(excelWorkbook, ismultivariate);
            }
            catch { }
            ProgressBar progress = new ProgressBar(true, "");
            progress.Owner = this;
            new Thread(() => ToSheet(progress)).Start();
            progress.ShowDialog();
        }
        private void btn_DataBrowsing(object sender, RoutedEventArgs e)
        {
            try
            {
                QC4Common.Common.CommonFlag.SetIsMultivariateUpdated(excelWorkbook, ismultivariate);
            }
            catch { }
            if (CommonFunction.ActivationKeyChecking())
            {
                ToSheetforDatabrowsing();
            }
            else
            {
                if (integritycheck)
                {
                    Thread thread = null;
                    Util.DataBrowseStd.GenerateDataSheet generateData = new Util.DataBrowseStd.GenerateDataSheet(excelWorkbook);
                    generateData.OnWorkerComplete += new Util.DataBrowseStd.GenerateDataSheet.OnWorkerMethodCompleteDelegate(OnWorkerMethodComplete);
                    progress = new ProgressBar(LocalResource.LOADING);
                    progress.Owner = this;
                    thread = new Thread(() => generateData.LoadData());
                    thread.Start();
                    progress.ShowDialog();
                }
                else
                {
                    MessageDialog.ErrorOk(LocalResource.QC4_INTEGRITYCHECK_FAIL);
                }
            }

        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(int hWnd, int Msg, int wParam, int lParam);

        private const int HWND_BROADCAST = 0xffff;

        private void ToSheet(ProgressBar pb)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                try
                {
                    LogObj.WriteLog("Button-ToSheet-Click", "Opening Qc4 Pro UI");
                    btn_ToSheet.IsEnabled = false;
                    excelApp.EnableEvents = true;

                    //excelApp.ScreenUpdating = false;
                    visibleallsheet(excelWorkbook);
                    modeCell.Value = Constant.Setting.MODE_PRO;
                    excelApp.EnableEvents = false;
                    try
                    {
                        var array = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Org) || (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Imp)).Select(q => q.Value).ToList();
                        QC4Common.Util.ExcelUtil.Data02validation(excelWorkbook, array);
                    }
                    catch { }
                    this.WindowState = WindowState.Minimized;
                    excelWorkbook.Windows[1].Caption = Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4";
                    excelApp.Caption = "Macromill - Quick-CROSS";

                    Excel.Worksheet qsSheet;
                    Excel.Worksheet dataAfterProcess = null;

                    qsSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.QuestionSetting);

                    if (isExecutedDataProcess)
                    {

                        dataAfterProcess = ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, Constants.SheetType.sh_Data01 + "(Processed)");
                        Excel.Worksheet sheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Util.Constants.SheetCodeName.Setting);
                        Excel.Range range = sheet.Cells[QC4Common.Common.Constants.Databrowsing.row + 1, QC4Common.Common.Constants.Databrowsing.colunm];
                        range.Value = Constant.Setting.MODE_PRO;
                    }
                    else
                    {
                        qsSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.QuestionSetting);
                    }
                    //excelApp.EnableEvents = false;
                    qsSheet.Activate();
                    qsSheet.Range["F4"].Select();
                    if (dataAfterProcess != null)
                    {
                        dataAfterProcess.Activate();
                        isExecutedDataProcess = false;
                    }
                    excelApp.Visible = true;
                    excelApp.WindowState = Excel.XlWindowState.xlMaximized;
                    SetForegroundWindow((IntPtr)excelApp.Hwnd);
                    excelApp.EnableEvents = true;
                    this.Hide();
                    this.Cursor = Cursors.Arrow;

                    pb.Close();
                    if (dataAfterProcess != null)
                    {
                        isExecutedDataProcess = false;
                        Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Util.Constants.SheetCodeName.Setting);
                        Excel.Range r = s.Cells[QC4Common.Common.Constants.Databrowsing.row, QC4Common.Common.Constants.Databrowsing.colunm];
                        r.Value = "dataaftersheet";
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    LogObj.WriteLog("Button-ToSheet-Click", "EXCEPTION in Opening Qc4 Pro UI - message-" + ex.Message, Logger.Log.Level.Error);
                    txt_FileName.Text = "";
                    SelectedFile = "";
                    excelApp = null;
                    ChangeButtonState(false);
                    progress.Close();
                }
            }));
        }
        private void ToSheetforDatabrowsing()
        {
            bool stdflag = false;
            bool dataafterpro = false;
            string value = string.Empty;
            Dispatcher.BeginInvoke(new Action(delegate
            {
                try
                {
                    btn_Data_Browsing.IsEnabled = false;
                    excelApp.EnableEvents = true;
                    visibleallsheet(excelWorkbook);
                    modeCell.Value = Constant.Setting.MODE_DATABROWSE;
                    excelApp.EnableEvents = false;
                    try
                    {
                        var array = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Org) || (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Imp)).Select(q => q.Value).ToList();
                        QC4Common.Util.ExcelUtil.Data02validation(excelWorkbook, array);
                    }
                    catch { }
                    this.WindowState = WindowState.Minimized;
                    excelWorkbook.Windows[1].Caption = Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4";
                    excelApp.Caption = "Macromill - Quick-CROSS";
                    Excel.Worksheet targetSheet;
                    if (CommonFunction.ActivationKeyChecking())
                    {
                        visibleallsheet(excelWorkbook);
                        if (ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, Constants.SheetType.sh_Data01 + "(Processed)") == null)
                        {
                            value = "datasheet";
                        }
                        else
                        {
                            dataafterpro = true;
                            value = "dataaftersheet";
                        }
                        Excel.Worksheet sheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Util.Constants.SheetCodeName.Setting);
                        Excel.Range range = sheet.Cells[QC4Common.Common.Constants.Databrowsing.row + 1, QC4Common.Common.Constants.Databrowsing.colunm];
                        range.Value = Constant.Setting.MODE_PRO;
                    }
                    else
                    {
                        stdflag = true;
                        if (ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, Constants.SheetType.sh_Data01 + "(Processed)") == null)
                        {
                            value = "datasheet";
                        }
                        else
                        {
                            dataafterpro = true;
                            value = "dataaftersheet";
                        }
                        Hideallsheet(excelWorkbook);
                        Excel.Worksheet sheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Util.Constants.SheetCodeName.Setting);
                        Excel.Range range = sheet.Cells[QC4Common.Common.Constants.Databrowsing.row + 1, QC4Common.Common.Constants.Databrowsing.colunm];
                        range.Value = Constant.Setting.MODE_STD;
                    }
                    if (!integritycheck)
                    {
                        targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.QuestionSetting);
                        targetSheet.Activate();
                    }
                    else
                    {
                        if (dataafterpro)
                        {
                            targetSheet = ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, Constants.SheetType.sh_Data01 + "(Processed)");
                            targetSheet.Activate();

                        }
                        else
                        {
                            targetSheet = ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, Constants.SheetType.sh_Data01);
                            targetSheet.Activate();
                        }
                    }
                    excelApp.Visible = true;

                    excelApp.WindowState = Excel.XlWindowState.xlMaximized;
                    SetForegroundWindow((IntPtr)excelApp.Hwnd);
                    this.Hide();
                    this.Cursor = Cursors.Arrow;

                    excelApp.EnableEvents = true;
                    excelApp.ScreenUpdating = true;
                    if (!integritycheck)
                    {
                        targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.Data01);
                        targetSheet.Activate();
                    }
                    else
                    {

                        Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Util.Constants.SheetCodeName.Setting);
                        Excel.Range r = s.Cells[QC4Common.Common.Constants.Databrowsing.row, QC4Common.Common.Constants.Databrowsing.colunm];
                        r.Value = value;
                    }

                }

                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    LogObj.WriteLog("Button-Data browsing-Click", "EXCEPTION in Opening Qc4 Pro UI - message-" + ex.Message, Logger.Log.Level.Error);
                    txt_FileName.Text = "";
                    SelectedFile = "";
                    excelApp = null;
                    ChangeButtonState(false);

                }
            }));
        }

        private void DeleteFileCancelation(Excel.Workbook workbook)
        {
            try
            {
                if (workbook != null)
                {
                    ExcelUtil.DeleteDataAfterSheets(workbook);
                }

                using (SQLiteConnection dbSource = dbHelper.GetConnection(DB.DBHelper.GetConnectionString(workbook)))
                {
                    dbSource.Open();
                    DB.DBHelper.DeleteDataAfterProcessTable(dbSource);
                    dbSource.Close();
                }

                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + "Comparison GT.xlsx"; //GetTemplatePath(TEMPLATE_NAME);  
                string targetPath = QC4Common.Util.QCUtil.GetTargetPath(workbook);
                targetPath = Path.GetDirectoryName(targetPath) + "\\";
                targetPath.Replace("\\\\", "\\");

                string fileopenPath = QC4Common.Util.QCUtil.GetTempPath(workbook);
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
            }
            catch
            {

            }
        }
        private void Hideallsheet(Excel.Workbook excelWorkbook)
        {
            excelWorkbook.Unprotect(Constants.Password);
            Excel.Worksheet targetSheet = null;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.QuestionSetting);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.DataProcess);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.CrossTabulation);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.SummaryTable);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.GTTabulation);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.FACreation);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.LDEL);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
            excelWorkbook.Protect(Constants.Password);

        }
        private void visibleallsheet(Excel.Workbook excelWorkbook)
        {
            excelWorkbook.Unprotect(Constants.Password);
            Excel.Worksheet targetSheet = null;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.QuestionSetting);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.DataProcess);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.CrossTabulation);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.SummaryTable);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.GTTabulation);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.FACreation);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
            targetSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.LDEL);
            targetSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
            excelWorkbook.Protect(Constants.Password);

        }
        private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btn_Close_File_Click(object sender, RoutedEventArgs e)
        {
            CloseFile();
        }

        public void CloseFile(bool alert = true)
        {
            try
            {
                LogObj.WriteLog("CloseFile", "Closing excel");
                if (excelApp == null)
                {
                    _log.Logappinformation(appinfo);
                    LogObj.WriteLog("CloseFile", "Resetting values");
                    txt_FileName.Text = "";
                    SelectedFile = "";
                    LogObj.WriteLog("CloseFile", "Changing button state to default");

                    return;
                }
                else if (null != excelWorkbook)
                {
                    if (alert)
                    {


                        MessageBoxResult result = MessageBox.Show(String.Format(LocalResource.ALERT_WANT_TO_SAVE, Path.GetFileNameWithoutExtension(SelectedFile)), "QuickCross", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.Cancel)
                        {
                            return;
                        }
                        else if (result == MessageBoxResult.Yes)
                        {
                            if (File_read_only)
                            {
                                result = MessageBox.Show(String.Format(LocalResource.FILE_READONLY_SAVE_WARNING_MSG, Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4"), "QuickCross", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                            string destPath = Path.GetDirectoryName(SelectedFile) + "\\" + Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4";
                            QcFileHelper.SaveFileWithPB(this, ref destPath, ref TempPath, excelWorkbook, true, false);
                            if (!QC4Common.Util.FileHelper.isFileSaveSuccess)
                                return;
                            firsttime = true;
                            isExecutedDataProcess = false;
                        }
                        else
                        {
                            if (Qc4Launcher.MainWindow.fileloack != null)
                            {
                                Qc4Launcher.MainWindow.fileloack.Dispose();
                                Qc4Launcher.MainWindow.fileloack.Close();
                                Qc4Launcher.MainWindow.fileloack = null;
                            }
                            LogObj.WriteLog("CloseFile", "Messagebox Result is No/Cancel");
                            firsttime = true;

                            isExecutedDataProcess = false;
                        }
                    }
                    LogObj.WriteLog("CloseFile", "Closing excel after user confirmation");
                    try
                    {

                        LogObj.WriteLog("Close file ", "before workb0ok release");
                        excelApp.EnableEvents = false;
                        excelApp.DisplayAlerts = false;

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }

                    try
                    {
                        _log.Logappinformation(appinfo);
                        excelWorkbook.Close();
                        COMWholeOperate.releaseComObject(ref excelWorkbook);
                        excelOperate.Dispose();
                        COMWholeOperate.releaseComObject(ref excelOperate);
                        excelApp = null;
                        if (xlapp1 != null)
                        {
                            xlapp1.Quit();
                        }


                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }

                    LogObj.WriteLog("CloseFile", "Quit Excel application fail");
                    try
                    {
                        LogObj.WriteLog("Close file ", "before excelApp release");
                        if (excelApp != null && excelApp.Workbooks.Count == 0)
                        {
                            excelApp.Quit();

                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Warn(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                }

                QcFileHelper.CloseQCFile(TempPath, false);

                btn_Swap_Data.Visibility = Visibility.Hidden;
                btn_ToSheet.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            try
            {
                txt_FileName.Text = "";
            }
            catch
            {
                Dispatcher.InvokeAsync(() =>
                {
                    txt_FileName.Text = "";
                }, DispatcherPriority.ApplicationIdle);
            }
            SelectedFile = "";
            excelApp = null;
            ChangeButtonState(false);
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            if (!String.IsNullOrEmpty(SelectedFile))
            {
                result = MessageBox.Show(String.Format(LocalResource.ALERT_WANT_TO_SAVE, Path.GetFileName(SelectedFile)), "QuickCross", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    if (File_read_only)
                    {
                        result = MessageBox.Show(String.Format(LocalResource.FILE_READONLY_SAVE_WARNING_MSG, Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4"), "QuickCross", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    SaveFile();
                    if (!QC4Common.Util.FileHelper.isFileSaveSuccess)
                        return;
                }

            }
            else
            {
                result = MessageBox.Show(String.Format(LocalResource.ALERT_CLOSE_QC4), "QuickCross", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result != MessageBoxResult.Yes)
                {
                    _log.Logappinformation(appinfo);
                    return;

                }
            }

            CloseFile(false);
            try
            {
                if (excelApp != null)
                {
                    newThread.CancelAsync();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    COMWholeOperate.releaseComObject(ref excelApp);
                    GC.Collect();
                }
                DestroyWindow(IntPtr.Zero);//to stop thread of HWND
            }
            catch { }
            isExit = true;
            try
            {

                this.Close();
            }
            catch { }
            Environment.Exit(1);
        }

        private void btn_Save_As_Click(object sender, RoutedEventArgs e)
        {
            if (excelApp == null)
            {
                return;
            }
            Util.Definiotion.SaveAs = false;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "QC4 files(*.qc4)|*.qc4";
            if (dlg.ShowDialog() == true)
            {
                string fName = dlg.FileName;
                if (Path.GetExtension(fName) != Constant.Qc4Extension)
                {
                    fName += Constant.Qc4Extension;
                }

                FileInfo Qcfile = new FileInfo(fName);
                QC4Common.Util.FileHelper fileHelper = new QC4Common.Util.FileHelper();
                if (File_read_only && fileHelper.IsFileLocked(Qcfile))
                {
                    MessageBoxResult result = MessageBox.Show(String.Format(LocalResource.FILE_READONLY_SAVE_WARNING_MSG, Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4"), "QuickCross", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (fileHelper.IsFileLocked(Qcfile) && fName != SelectedFile)
                {
                    MessageBoxResult result = MessageBox.Show(String.Format(LocalResource.FILE_READONLY_SAVE_WARNING_MSG, Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4"), "QuickCross", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                QcFileHelper.SaveFileWithPB(this, ref fName, ref TempPath, excelWorkbook, true, true);
                txt_FileName.Text = fName;
                SelectedFile = fName;
                Definiotion.SelectedDir = Path.GetDirectoryName(SelectedFile);
                Definiotion.SelectedFile = Path.GetFileNameWithoutExtension(SelectedFile);
                if (Definiotion.SaveAs)
                {
                    File_read_only = false;
                    ExcelUtil.SetReadonlyMode(excelWorkbook, File_read_only);
                    MessageDialog.Info(LocalResource.FILE_SAVED);
                }
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFile(true);
        }

        private void SaveFile(bool alert = false)
        {
            FileInfo Qcfile = new FileInfo(SelectedFile);
            QC4Common.Util.FileHelper fileHelper = new QC4Common.Util.FileHelper();
            if (alert)
            {
                MessageBoxResult result;
                if (ExcelUtil.GetReadonlyMode(excelWorkbook) == "1")
                {
                    result = MessageBox.Show(String.Format(LocalResource.FILE_READONLY_SAVE_WARNING_MSG, Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4"), "QuickCross", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                result = MessageBox.Show(String.Format(LocalResource.ALERT_WANT_TO_SAVE, Path.GetFileNameWithoutExtension(SelectedFile)), "Quick-CROSS", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result != MessageBoxResult.Yes)
                {
                    return;
                }
            }
            try
            {
                string destPath = Path.GetDirectoryName(SelectedFile) + "\\" + System.IO.Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4";

                QcFileHelper.SaveFileWithPB(this, ref destPath, ref TempPath, excelWorkbook, true, false);
                if (QC4Common.Util.FileHelper.isFileSaveSuccess)
                    MessageDialog.Info(LocalResource.FILE_SAVED);

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void btn_Swap_Data_Click(object sender, RoutedEventArgs e)
        {
            SwapData swap = new SwapData(this);
            swap.SwapDataMain(excelWorkbook, TempPath, SelectedFile);
            excelWorkbook.Activate();
        }



        private void btn_DataOutput_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                TabExportFilterSetting tabWindow = new TabExportFilterSetting(excelWorkbook, SelectedFile, main: this);
                if (tabWindow.IsNewExist)
                {
                    System.Windows.Forms.DialogResult res = MessageDialog.ShowMessageOnForm(LocalResource.DO_OUTPUT_CHECK_NEW_VARIABLE, Enums.MessageType.InfoYesNo, new System.Windows.Interop.WindowInteropHelper(this).Handle, icon: System.Windows.Forms.MessageBoxIcon.Question);
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        tabWindow = new TabExportFilterSetting(excelWorkbook, SelectedFile, false, this);
                        tabWindow.ShowDialog();
                    }
                    else
                        MessageDialog.ShowMessageOnForm(LocalResource.DO_OUTPUT_NEW_VARIABLE, Enums.MessageType.Info, new System.Windows.Interop.WindowInteropHelper(this).Handle, icon: System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                    tabWindow.ShowDialog();

            }
            catch (Exception ex)
            {
                string exs = ex.Message;
            }


        }

        private void btn_DataMerge_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DataMerge dataMerge = new DataMerge(this, txt_FileName.Text, excelWorkbook, TempPath);
            dataMerge.Owner = this;
            dataMerge.ShowDialog();
        }
        #endregion Events


        private void OnWorkerMethodComplete(double value, string status)
        {
            progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                progress.UpdateProgressBar(value, status);
            }
            ));
        }

        private void OnWorkerMethodCompleteGT(double value, string status, bool isForceStop = false, bool retainThread = false, bool displayMessage = false, bool close = false, bool disableCancel = false, bool isproceesCancel = false)
        {
            progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                if (close)
                {
                    if (retainThread)
                        progress.UpdateProgressBar(progress.getPbValue(), status, isForceStop, retainThread, disableCancel);
                    else
                        progress.Close();
                }
                else
                {
                    progress.UpdateProgressBar(value, status, isForceStop, retainThread, disableCancel);
                    if (isproceesCancel)
                    {
                        IntPtr pb = default(IntPtr);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.MSG_OUTPUT_ABORTED, Enums.MessageType.Info, excelWorkbook.Application.ActiveWorkbook, checkcrossintptr);

                    }
                    else if (displayMessage)
                    {
                        IntPtr pb = default(IntPtr);
                        int ProcesId = Convert.ToInt32(QC4Common.Util.QCUtil.GetProcessId(excelWorkbook));
                        Process hostProcess = Process.GetProcessById(ProcesId);

                        pb = hostProcess.MainWindowHandle;
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.DATAPROCESS_COMPLETED, Enums.MessageType.Info, excelWorkbook.Application.ActiveWorkbook, pb);
                    }
                }
            }
            ));
        }

        private void OnWorkerMethodCompleteCR(double value, string status, bool isForceStop = false, bool retainThread = false, bool close = false, bool disableCancel = false)
        {
            progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                if (close)
                {
                    if (retainThread)
                        progress.UpdateProgressBar(progress.getPbValue(), status, isForceStop, retainThread, disableCancel);
                    else
                        progress.Close();
                }
                else
                {
                    progress.UpdateProgressBar(value, status, isForceStop, retainThread, disableCancel);
                }
            }
            ));
        }

        private void CopyTemplates()
        {
            string str = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            str += "\\QC4\\Templates";
            string source = System.AppContext.BaseDirectory + "Templates";
            FileUtil.DirectoryCopy(source, str, true);
        }

        #region qc4_ribboncontrols
        /// <summary>
        /// Window message  from excel ribbon buttons
        /// </summary>
        /// <param name="e"></param>
        [DllImport("user32.dll")]
        static extern bool DestroyWindow(IntPtr hWnd);
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);

        static readonly int menumsg = RegisterWindowMessage(Constants.RibbonMessage.msg_Menu);
        static readonly int crossoptionmsg = RegisterWindowMessage(Constants.RibbonMessage.msg_CrossOptions);
        static readonly int crossexecmsg = RegisterWindowMessage(Constants.RibbonMessage.msg_CrossExecute);
        static readonly int crossRPexecmsg = RegisterWindowMessage(Constants.RibbonMessage.msg_CrossChart);
        static readonly int menumegwithout = RegisterWindowMessage(Constants.RibbonMessage.msg_Menu_other);
        static readonly int releaseLock = RegisterWindowMessage(Constants.RibbonMessage.msg_File_lockRelease);
        static readonly int craeteLock = RegisterWindowMessage(Constants.RibbonMessage.msg_File_lockCreate);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        static readonly int gtexecmsg = RegisterWindowMessage(Constants.RibbonMessage.msg_GTExecute);
        static readonly int gtoptionmsg = RegisterWindowMessage(Constants.RibbonMessage.msg_GTOptions);

        static readonly int summaryexecmsg = RegisterWindowMessage(Constants.RibbonMessage.msg_SummaryExecute);
        static readonly int summaryoptmsg = RegisterWindowMessage(Constants.RibbonMessage.msg_SummaryOptions);
        static readonly int saveFileMsg = RegisterWindowMessage(Constants.RibbonMessage.msg_Save);
        static readonly int closeFileMsg = RegisterWindowMessage(Constants.RibbonMessage.msg_Close);
        static readonly int checkCross = RegisterWindowMessage(QC4Common.Common.Constants.RibbonMessage.msg_CheckCross);
        static readonly int updVarDict = RegisterWindowMessage(Constant.CommonMessage.msg_UpdVarDict);
        static readonly int saveAsMsg = RegisterWindowMessage(Constants.RibbonMessage.msg_SaveAs);
        static readonly int CancelMessage = RegisterWindowMessage(Constants.RibbonMessage.msg_cancelled);

        string ProcessingSheetCodeName = "";
        string ProcessingSheetName = "";
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == menumsg)
            {
                try
                {

                    qc4UpdateAfterMenuClick();
                    this.EnableWindow();
                    excelApp.Visible = false;
                    excelApp.Interactive = true;
                    excelApp.Cursor = Excel.XlMousePointer.xlDefault;

                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }

            else if (msg == crossoptionmsg)
            {
                try
                {
                    string CrossTitle = LocalResource.TITLE_CROSS_TAB;
                    var existingCrossWindow = System.Windows.Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals(CrossTitle));
                    if (existingCrossWindow == null)
                    {
                        CrossTabulation ct = new CrossTabulation(this, excelWorkbook, "tmp");
                        ct.Title = CrossTitle;
                        Excel.Worksheet excelWorkbook1 = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constant.SheetCodeName.CrossTabulation);

                        foreach (Excel.Shape shp in excelWorkbook1.Shapes)
                        {
                            if (shp.Name == "TextBox_Messge" && shp.Type == Microsoft.Office.Core.MsoShapeType.msoTextBox)
                            {
                                ct.OptionSettingsMsg = shp;

                            }
                        }
                        WindowInteropHelper wih = new WindowInteropHelper(ct);
                        wih.Owner = new IntPtr(excelWorkbook.Application.Hwnd);
                        Excel.XlWindowState winstate = excelWorkbook.Application.WindowState;
                        SetParent(wih.Handle, (IntPtr)excelWorkbook.Application.Hwnd);
                        excelWorkbook.Application.Interactive = false;
                        ct.ShowDialog();
                        excelWorkbook.Application.Interactive = true;
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }

            }
            else if (msg == gtoptionmsg)
            {
                try
                {
                    string GrossTitle = LocalResource.TITLE_GROSS_TAB;
                    var existingGrossWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals(GrossTitle));
                    if (existingGrossWindow == null)
                    {
                        GrossTabulation gt = new GrossTabulation(excelWorkbook, "tmp");
                        gt.Title = GrossTitle;
                        Excel.Worksheet excelWorkbook1 = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constant.SheetCodeName.GTTabulation);

                        foreach (Excel.Shape shp in excelWorkbook1.Shapes)
                        {
                            if (shp.Name == "TextBox_Messge" && shp.Type == Microsoft.Office.Core.MsoShapeType.msoTextBox)
                            {
                                gt.OptionSettingsMsg = shp;

                            }
                        }
                        WindowInteropHelper wih = new WindowInteropHelper(gt);
                        wih.Owner = new IntPtr(excelWorkbook.Application.Hwnd);
                        SetParent(wih.Handle, (IntPtr)excelWorkbook.Application.Hwnd);
                        excelWorkbook.Application.Interactive = false;
                        gt.ShowDialog();
                        excelWorkbook.Application.Interactive = true;
                        //SetForegroundWindow((IntPtr)excelWorkbook.Application.Hwnd);

                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else if (msg == crossexecmsg)
            {
                try
                {
                    crossTabulate();
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else if (msg == crossRPexecmsg)
            {
                try
                {
                    crossTabulate(true);
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else if (msg == gtexecmsg)
            {
                try
                {
                    Excel.Worksheet gtSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, SheetCodeName.GTTabulation);
                    GrossTabulate(excelWorkbook, gtSheet, "P");
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else if (msg == summaryexecmsg)
            {
                try
                {
                    summaryTabulate();
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else if (msg == summaryoptmsg)
            {
                try
                {
                    string title = LocalResource.TITLE_SUMMARY_FORM;
                    var existingWindow = System.Windows.Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals(title));
                    if (existingWindow == null)
                    {
                        SummaryTableForm ct = new SummaryTableForm(excelWorkbook, "tmp");
                        ct.Title = title;
                        Excel.Worksheet excelWorkbook1 = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constant.SheetCodeName.SummaryT);
                        foreach (Excel.Shape shp in excelWorkbook1.Shapes)
                        {
                            if (shp.Name == "TextBox_Messge" && shp.Type == Microsoft.Office.Core.MsoShapeType.msoTextBox)
                            {
                                ct.OptionSettingsMsg = shp;

                            }
                        }
                        WindowInteropHelper wih = new WindowInteropHelper(ct);
                        wih.Owner = new IntPtr(excelWorkbook.Application.Hwnd);
                        Excel.XlWindowState winstate = excelWorkbook.Application.WindowState;
                        SetParent(wih.Handle, (IntPtr)excelWorkbook.Application.Hwnd);
                        excelApp.WindowState = winstate;
                        excelWorkbook.Application.Interactive = false;
                        ct.ShowDialog();
                        excelWorkbook.Application.Interactive = true;

                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else if (msg == checkCross)
            {
                try
                {
                    using (new SingleGlobalInstance(10000, excelWorkbook))
                    {
                        try { excelWorkbook.Application.Interactive = false; } catch { }

                        long wParamL = (long)wParam;
                        bool checkList = (wParamL & Constant.CheckCrossFlag.CheckList) == Constant.CheckCrossFlag.CheckList;
                        bool checkListSTD = (wParamL & Constant.CheckCrossFlag.ChecklistSTD) == Constant.CheckCrossFlag.ChecklistSTD;
                        bool checkCross = (wParamL & Constant.CheckCrossFlag.CheckCross) == Constant.CheckCrossFlag.CheckCross;
                        if (checkList || checkCross || checkListSTD)
                        {
                            CheckListCheckCrossBgWorkser = new BackgroundWorker();
                            DB.DBHelper.CreateMultivariateTempTable(excelWorkbook);
                            DPCheckListTabulationQc dPCheckListTabulationQc = new DPCheckListTabulationQc();
                            CheckCrossQC checkCrossQC = new CheckCrossQC();
                            progress = new ProgressBar(LocalResource.TITLE_DP_CHECK_CROSS, CheckListCheckCrossBgWorkser);
                            WindowInteropHelper wih = new WindowInteropHelper(progress);
                            wih.Owner = new IntPtr(excelWorkbook.Application.Hwnd);
                            SetParent(wih.Handle, (IntPtr)excelWorkbook.Application.Hwnd);
                            checkcrossintptr = (IntPtr)excelWorkbook.Application.Hwnd;
                            if (checkListSTD)
                            {
                                WindowInteropHelper wihMain = new WindowInteropHelper(this);
                                wih.Owner = wihMain.Handle;
                                SetParent(wih.Handle, wihMain.Handle);
                                checkcrossintptr = wihMain.Handle;
                            }

                            excelWorkbook.Application.Interactive = false;
                            dPCheckListTabulationQc.OnWorkerComplete += new DPCheckListTabulationQc.OnWorkerMethodCompleteDelegate(OnWorkerMethodCompleteGT);
                            checkCrossQC.OnWorkerComplete += new CheckCrossQC.OnWorkerMethodCompleteDelegate(OnWorkerMethodCompleteGT);
                            CheckListCheckCrossBgWorkser.WorkerReportsProgress = true;
                            CheckListCheckCrossBgWorkser.WorkerSupportsCancellation = true;


                            if (checkList || checkListSTD)
                            {
                                CheckListCheckCrossBgWorkser.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                                {
                                    dPCheckListTabulationQc.Tabulate(excelWorkbook, sender, checkCross);

                                    if (CheckListCheckCrossBgWorkser.CancellationPending)
                                    {
                                        DeleteFileCancelation(excelWorkbook);
                                    }
                                });
                                CheckListCheckCrossBgWorkser.RunWorkerAsync();
                                progress.Cursor = Cursors.Wait;
                                if (checkListSTD)
                                    progress.Owner = this;

                                progress.ShowDialog();

                            }
                            else if (checkCross)
                            {
                                double currentProgress = 0;
                                string chkCrsfile = Path.Combine(Path.GetTempPath(), "QC4", CheckCrossQC.checkcrossFile);
                                List<CossTableInput> crTableSets = CheckCrossReader.readSettings(chkCrsfile, excelWorkbook, false);
                                if (crTableSets.Count > 0)
                                {
                                    CheckListCheckCrossBgWorkser.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                                    {
                                        checkCrossQC.ExecuteCheckCross(excelWorkbook, sender, out currentProgress, null, null, 100, false);
                                        if (CheckListCheckCrossBgWorkser.CancellationPending)
                                        {
                                            DeleteFileCancelation(excelWorkbook);
                                        }
                                    });
                                    CheckListCheckCrossBgWorkser.RunWorkerAsync();
                                    progress.Cursor = Cursors.Wait;
                                    progress.WindowState = WindowState.Maximized;
                                    progress.ShowDialog();



                                }
                                else
                                {
                                    if (!checkList)
                                    {
                                        IntPtr pb = default(IntPtr);
                                        MessageDialog.ShowMessageOnWorkBook(LocalResource.DATAPROCESS_COMPLETED, Enums.MessageType.Info, excelWorkbook.Application.ActiveWorkbook, pb);
                                    }
                                    try { System.IO.File.Delete(chkCrsfile); } catch (Exception e) { }
                                }
                            }

                            if (CheckListCheckCrossBgWorkser.CancellationPending)
                            {
                                //IntPtr pb = default(IntPtr);
                                //MessageDialog.ShowMessageOnWorkBook(LocalResource.MSG_OUTPUT_ABORTED, Enums.MessageType.Info, excelWorkbook.Application.ActiveWorkbook, pb);
                                //OnWorkerMethodCompleteGT(100,isForceStop=true)
                            }


                            try
                            {


                                SetForegroundWindow((IntPtr)excelWorkbook.Application.Hwnd);
                            }
                            catch { }

                            if (dPCheckListTabulationQc.childExcelApp != IntPtr.Zero)
                            {
                                try
                                {
                                    SetForegroundWindow(dPCheckListTabulationQc.childExcelApp);
                                }
                                catch { }
                            }
                            else if (checkCrossQC.childExcelApp != IntPtr.Zero)
                            {
                                try
                                {
                                    SetForegroundWindow(checkCrossQC.childExcelApp);
                                }
                                catch { }
                            }

                            GC.Collect();
                            excelWorkbook.Application.Interactive = true;
                            if (!excelWorkbook.Application.ScreenUpdating)
                            {
                                excelWorkbook.Application.ScreenUpdating = true;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else if (msg == saveFileMsg)
            {
                try
                {
                    SaveFile();
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else if (msg == closeFileMsg)
            {
                try
                {
                    if (fileloack != null)
                    {
                        fileloack.Dispose();
                        fileloack.Close();
                        fileloack = null;
                    }
                    Directory.Delete(TempPath, true);
                }
                catch (Exception ex)
                {
                    _log.Warn(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
                try
                {
                    Dispatcher.BeginInvoke(new Action(delegate
                    {
                        LogObj.WriteLog("WorkbookBeforeClose", "Resetting values");
                        txt_FileName.Text = "";
                        SelectedFile = "";
                        LogObj.WriteLog("WorkbookBeforeClose", "Changing button state to default");
                        ChangeButtonState(false);
                        try
                        {
                            excelApp.DisplayAlerts = false;
                            excelApp.EnableEvents = false;
                            excelApp.Visible = false;
                            excelApp.Quit();
                            excelOperate.Dispose();
                        }
                        catch { }
                        this.Show();
                        this.WindowState = WindowState.Normal;
                        this.Activate();
                    }));
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else if (msg == updVarDict)
            {
                try
                {
                    if (ProcessingSheetCodeName == "" && ActivatedSheet != null)
                    {
                        ProcessingSheetCodeName = ActivatedSheet.CodeName == "" ? "NoCodeName" : ActivatedSheet.CodeName;
                        ProcessingSheetName = ActivatedSheet.Name;
                        Definiotion.VariableDictionary = DictionaryUtil.PopulateQSDictionary(excelWorkbook);
                        string range = "L2";
                        if (ProcessingSheetCodeName != "" && ProcessingSheetCodeName != Constant.SheetType.sh_QuesSetting)
                        {
                            switch (ProcessingSheetCodeName)
                            {
                                case Constant.SheetType.GTCounting:
                                    range = "I2";
                                    break;
                                case Constant.SheetType.sh_CrossCounting:
                                    range = "J2";
                                    break;
                                case Constant.SheetType.sh_SummaryList:
                                    range = "J2";
                                    break;
                                case Constant.SheetType.DataProcess:
                                    range = "L2";
                                    break;
                            }
                            Excel.Worksheet excelWorkbook1 = ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, ProcessingSheetName);
                            excelWorkbook1.Range[range].Value2 = "";
                            excelWorkbook1.Application.EnableEvents = true;
                            excelWorkbook1.Application.Interactive = true;
                            ProcessingSheetCodeName = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else if (msg == saveAsMsg)
            {
                try
                {
                    if (fileloack != null)
                    {
                        fileloack.Dispose();
                        fileloack.Close();
                        fileloack = null;
                    }
                    QCUtil.SetApplicationPaths(excelWorkbook, ref TempPath, ref SelectedFile);
                    Definiotion.SelectedDir = Path.GetDirectoryName(SelectedFile);
                    Definiotion.SelectedFile = Path.GetFileNameWithoutExtension(SelectedFile);
                    if (fileloack == null)
                    {
                        fileloack = new FileStream(SelectedFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    }
                }
                catch { }
            }
            else if (msg == releaseLock)
            {
                if (fileloack != null)
                {
                    fileloack.Dispose();
                    fileloack.Close();
                    fileloack = null;
                }
            }
            else if (msg == craeteLock)
            {
                try
                {
                    if (fileloack != null)
                    {
                        fileloack.Dispose();
                        fileloack.Close();
                        fileloack = null;
                    }
                    QCUtil.SetApplicationPaths(excelWorkbook, ref TempPath, ref SelectedFile);
                    Definiotion.SelectedDir = Path.GetDirectoryName(SelectedFile);
                    Definiotion.SelectedFile = Path.GetFileNameWithoutExtension(SelectedFile);
                    if (fileloack == null)
                    {
                        fileloack = new FileStream(SelectedFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    }
                }
                catch (Exception ex) { }
            }
            else if (msg == CancelMessage)
            {
                isExecutedDataProcess = false;
            }
            return IntPtr.Zero;
        }

        public void summaryTabulate()
        {
            if (processRunning)
            {
                return;
            }
            try
            {
                processRunning = true;
                //using (new SingleGlobalInstance(10, excelWorkbook))
                {

                    if (!CrossTabulation.checkUnprocessedNewQuestionDialog(excelWorkbook))
                    {

                        Excel.Worksheet CrossSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.SummaryTabulation);

                        SummaryTabulation summaryTabulation = new SummaryTabulation();
                        System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
                        progress = new ProgressBar(LocalResource.TITLE_CROSS_TAB, backgroundWorker1);
                        WindowInteropHelper wih = new WindowInteropHelper(progress);
                        wih.Owner = new IntPtr(excelWorkbook.Application.Hwnd);
                        SetParent(wih.Handle, (IntPtr)excelWorkbook.Application.Hwnd);
                        excelWorkbook.Application.Interactive = false;
                        summaryTabulation.OnWorkerComplete += new SummaryTabulation.OnWorkerMethodCompleteDelegate(OnWorkerMethodCompleteCR);
                        backgroundWorker1.WorkerReportsProgress = true;
                        backgroundWorker1.WorkerSupportsCancellation = true;
                        backgroundWorker1.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                            summaryTabulation.Tabulate(excelWorkbook, CrossSheet, sender, e)
                        );
                        backgroundWorker1.RunWorkerAsync();
                        progress.ShowDialog();
                        if (summaryTabulation.childExcelApp != IntPtr.Zero)
                        {
                            try
                            {
                                SetForegroundWindow(summaryTabulation.childExcelApp);
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                SetForegroundWindow((IntPtr)excelWorkbook.Application.Hwnd);
                            }
                            catch { }
                        }
                    }
                }
            }
            finally
            {
                processRunning = false;
                excelWorkbook.Application.Interactive = true;
            }

        }

        private bool validateSummaryOption(Dictionary<string, string> advancedSetting)
        {
            if (checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Output_Cross_N_One + "_P", "false")
                && checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Output_Cross_Par_One + "_P", "false"))
            {
                MessageDialog.ShowMessageOnWorkBook(LocalResource.OUTPUT_SHEET_IS_NOT_SET, Enums.MessageType.ErrorOk, excelWorkbook);
                return false;
            }
            return true;
        }

        public void crossTabulate(bool report = false)
        {
            if (processRunning)
            {
                return;
            }
            try
            {
                processRunning = true;
                //using (new SingleGlobalInstance(10, excelWorkbook))
                {
                    Excel.Worksheet CrossSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.CrossTabulation);
                    System.Collections.Generic.Dictionary<string, string> AdvancedSetting = CrossSettingsReader.getAdvacedSettings(CrossSettingsReader.getASSheet(excelWorkbook));
                    if (!CrossTabulation.checkUnprocessedNewQuestionDialog(excelWorkbook))
                    {
                        System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
                        CrossTabulationQC crossTabulationQC = new CrossTabulationQC();
                        progress = new ProgressBar(LocalResource.TITLE_CROSS_TAB, backgroundWorker1);
                        WindowInteropHelper wih = new WindowInteropHelper(progress);
                        wih.Owner = new IntPtr(excelWorkbook.Application.Hwnd);
                        SetParent(wih.Handle, (IntPtr)excelWorkbook.Application.Hwnd);
                        excelWorkbook.Application.Interactive = false;
                        crossTabulationQC.OnWorkerComplete += new CrossTabulationQC.OnWorkerMethodCompleteDelegate(OnWorkerMethodCompleteCR);
                        backgroundWorker1.WorkerReportsProgress = true;
                        backgroundWorker1.WorkerSupportsCancellation = true;
                        backgroundWorker1.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                            crossTabulationQC.Tabulate(excelWorkbook, CrossSheet, sender, e, report)
                        );
                        backgroundWorker1.RunWorkerAsync();
                        progress.ShowDialog();
                        if (crossTabulationQC.childExcelApp != IntPtr.Zero)
                        {
                            try
                            {
                                SetForegroundWindow(crossTabulationQC.childExcelApp);
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                SetForegroundWindow((IntPtr)excelWorkbook.Application.Hwnd);
                            }
                            catch { }
                        }
                    }
                }
            }
            finally
            {
                processRunning = false;
                excelWorkbook.Application.Interactive = true;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private bool validateCrossOption(Dictionary<string, string> advancedSetting)
        {
            if (checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Output_Cross_N_Par_One + "_P", "false")
                && checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Output_Cross_N_One + "_P", "false")
                && checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Output_Cross_Par_One + "_P", "false"))
            {
                MessageDialog.ShowMessageOnWorkBook(LocalResource.OUTPUT_SHEET_IS_NOT_SET, Enums.MessageType.ErrorOk, excelWorkbook);
                return false;
            }
            if (checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Par_99 + "_P", "true")
                && checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Par_95 + "_P", "true")
                && checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Par_90 + "_P", "true"))
            {
                MessageDialog.ShowMessageOnWorkBook(LocalResource.ERR_MSG_CROSS_SIGNIFICANCE_CHECK_ALL, Enums.MessageType.ErrorOk, excelWorkbook);
                return false;
            }


            if (checkSettingExist(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Combo_Classify_Item + "_P")
                       && !checkSettingExist(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Combo_Classify_FolderPath + "_P"))
            {
                MessageDialog.ShowMessageOnWorkBook(LocalResource.ERR_MSG_CROSS_PATH_NULL, Enums.MessageType.ErrorOk, excelWorkbook);
                return false;
            }

            if (checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Summary_Mark_Ratio1 + "_P", "true"))
            {
                if (!checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Summary_Rate_Difference1 + "_P", "true")
                      && !checkAdvancedSettingValue(advancedSetting, CrossSettingsReader.F_Cr_Cross_AddUp_Check_Summary_Rate_Difference2 + "_P", "true")
                        )
                {
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.ERR_MSG_CROSS_MARKING_DIFFERENCE, Enums.MessageType.ErrorOk, excelWorkbook);
                    return false;
                }
            }
            return true;
        }

        public static bool checkSettingExist(Dictionary<string, string> settings, string key)
        {
            return (settings.ContainsKey(key) && settings[key] != null && settings[key] != string.Empty);
        }

        private bool checkAdvancedSettingValue(Dictionary<string, string> advancedSetting, string key, string value)
        {
            if (advancedSetting.ContainsKey(key) && advancedSetting[key] != null)
            {
                if (advancedSetting[key].ToLower() == value)
                {
                    return true;
                }
            }
            return false;
        }

        public void qc4UpdateAfterMenuClick()
        {
            IsUpdatevariabledic = true;
            Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(excelWorkbook);
            Qc4Launcher.MainWindow.integritycheck = true;
            IsUpdatevariabledic = false;
        }

        #endregion qc4_ribboncontrols

        public void GrossTabulate(Excel.Workbook workbook, Excel.Worksheet gtSheet, string version = null, bool IsReqFromOptions = false, bool IsReqFromGTSTD = false)
        {
            if (processRunning)
            {
                return;
            }
            try
            {
                if (version != "S")
                {
                    if (IsReqFromOptions && !ExcelAddIn.Common.Change.ValidateGTTab(null, true, workbook.Worksheets))
                        return;
                }
                processRunning = true;
                //using (new SingleGlobalInstance(10, workbook))
                {
                    if (IsReqFromOptions || (!IsReqFromOptions && !CrossTabulation.checkUnprocessedNewQuestionDialog(excelWorkbook)))
                    {
                        GrossTabulationQc grossTabulationQC = new GrossTabulationQc();
                        System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
                        progress = new ProgressBar("GT", backgroundWorker1);
                        if (IsReqFromGTSTD)
                            progress.Owner = this;
                        else
                        {
                            WindowInteropHelper wih = new WindowInteropHelper(progress);
                            wih.Owner = new IntPtr(workbook.Application.Hwnd);
                            SetParent(wih.Handle, (IntPtr)workbook.Application.Hwnd);
                        }
                        workbook.Application.Interactive = false;
                        grossTabulationQC.OnWorkerComplete += new GrossTabulationQc.OnWorkerMethodCompleteDelegate(OnWorkerMethodCompleteGT);
                        backgroundWorker1.WorkerReportsProgress = true;
                        backgroundWorker1.WorkerSupportsCancellation = true;
                        backgroundWorker1.DoWork += new DoWorkEventHandler(
                            (object sender, DoWorkEventArgs e) => grossTabulationQC.Tabulate(workbook, gtSheet, sender, e, version, IsReqFromOptions, this, isFromSTD: IsReqFromGTSTD)
                        );
                        backgroundWorker1.RunWorkerAsync();
                        progress.ShowDialog();
                        workbook.Application.Interactive = true;

                        if (grossTabulationQC.childExcelApp != IntPtr.Zero)
                        {
                            try
                            {
                                SetForegroundWindow(grossTabulationQC.childExcelApp);
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                SetForegroundWindow((IntPtr)excelWorkbook.Application.Hwnd);
                            }
                            catch { }
                        }
                    }
                }
            }
            finally
            {
                processRunning = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && txt_FileName.Focus() && txt_FileName.Text == Constants.HiddenAuthKey)
            {
                ShowACTWindow();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string culture = CultureInfo.CurrentCulture.Name;
                string dsep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                string tsep = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
                string lsep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                if (dsep != "." || tsep != "," || lsep != ",")
                {
                    MessageBox.Show(LocalResource.ALERT_CULTURE, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(1);
                }
                //Renaming Templates
                string executablePath = Assembly.GetExecutingAssembly().Location;
                RenameTemplates(Directory.GetParent(executablePath)?.FullName);
                string appdatapath= Environment.GetEnvironmentVariable("USERPROFILE")+ @"\AppData\Roaming\QC4";
                RenameTemplates(appdatapath);


            }
            catch { }
            try
            {
                QC4FileHelper.AddInsOnOrOff(true);
                if (QC4Common.Common.Constants.GlobalMode != "ja-JP," + Util.Constants.QCFont.MS_Gothic)
                {
                    lbl_cross.Text = "\x00A0\x00A0\x00A0Cross/\x00A0\x00A0\x00A0\x00A0\x00A0\x00A0\x00A0\x00A0Charts";
                    Combo_Language.SelectedIndex = 0;
                }
                else
                {
                    lbl_cross.Text = LocalResource.MAINWINDOW_BUTTON_CROSS_TABLE;
                    Combo_Language.SelectedIndex = 1;
                }
                new Thread(() =>
                {
                    try
                    {
                        appinfo = _log.Loginfo("");
                    }
                    catch (Exception ex) { _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
                }).Start();
                if (!String.IsNullOrEmpty(Definiotion.commandLineArg))
                {
                    if (File.Exists(Definiotion.commandLineArg))
                    {
                        SelectedFile = Definiotion.commandLineArg;
                        try
                        {
                            FileOpen();
                        }
                        catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }//Redmine id : 200469
                    }
                }
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }//Redmine id : 200469
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;

                if (!isExit)
                {
                    btn_Exit_Click(null, null);
                }

                QC4FileHelper.AddInsOnOrOff(false);
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }//Redmine id : 200469
        }




        private void Qs_Click(object sender, RoutedEventArgs e)
        {


            if (integritycheck)
            {
                this.Hide();
                Qc4Launcher.Forms.QuestionSetting.QuestionSettingMainWindow question = new Forms.QuestionSetting.QuestionSettingMainWindow(this, excelWorkbook, OpendfileHeading, TempPath);
                question.Owner = this;
                question.ShowDialog();
            }
            else
            {
                MessageDialog.ErrorOk(LocalResource.QC4_INTEGRITYCHECK_FAIL);
            }
        }
        private void help_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(QC4Common.Classes.Help.GetHelpLink(QC4Common.Common.Constants.HelpButtonType.MAINWINDOW));
        }

        private void Btn_Cross_table_report_Click(object sender, RoutedEventArgs e)
        {


            if (integritycheck)
            {
                this.Hide();
                string path = txt_FileName.Text;
                crossflag = true;

                CrossTabulationStd crossTabulation = new CrossTabulationStd(this, excelWorkbook, path);
                crossTabulation.Owner = this;
                crossTabulation.ShowDialog();
            }
            else
            {
                MessageDialog.ErrorOk(LocalResource.QC4_INTEGRITYCHECK_FAIL);
            }

        }

        private void Btn_Data_Processing_Click(object sender, RoutedEventArgs e)
        {


            if (integritycheck)
            {
                this.Hide();
                try

                {
                    DP_Main dpWindow = new DP_Main(this, excelWorkbook);
                    dpWindow.Owner = this;
                    dpWindow.ShowDialog();


                }
                catch (Exception ex)
                {
                    string exs = ex.Message;
                }
                finally
                {

                }
            }
            else
            {
                MessageDialog.ErrorOk(LocalResource.QC4_INTEGRITYCHECK_FAIL);
            }
        }

        private void Btn_QuestionSetting_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Qc4Launcher.Forms.QuestionSetting.QuestionSettingMainWindow question = new Forms.QuestionSetting.QuestionSettingMainWindow(this, excelWorkbook, OpendfileHeading, TempPath);
            question.Owner = this;
            question.ShowDialog();
        }

        private void Btn_Gttable_Click(object sender, RoutedEventArgs e)
        {


            if (integritycheck)
            {
                GrossTabulationSetting grossTabulationSetting = new GrossTabulationSetting(excelWorkbook, this, "tmp");
                grossTabulationSetting.ShowDialog();
            }
            else
            {
                MessageDialog.ErrorOk(LocalResource.QC4_INTEGRITYCHECK_FAIL);
            }
        }

        private void Qs_mouse_Enter(object sender, MouseEventArgs e)
        {
            img_qs.Source = new BitmapImage(new Uri(@"img/qs_hover.png", UriKind.RelativeOrAbsolute));
            lbl_qs.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
        }
        private void Qs_mouse_Leave(object sender, MouseEventArgs e)
        {
            img_qs.Source = new BitmapImage(new Uri(@"img/qs.png", UriKind.RelativeOrAbsolute));
            lbl_qs.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF3467b0"));
        }

        private void Data_Browse_mouse_enter(object sender, MouseEventArgs e)
        {
            img_data_browse.Source = new BitmapImage(new Uri(@"img/databrowse_hover.png", UriKind.RelativeOrAbsolute));
            lbl_data_browse.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
        }

        private void Data_Browse_mouse_leave(object sender, MouseEventArgs e)
        {
            img_data_browse.Source = new BitmapImage(new Uri(@"img/databrowse.png", UriKind.RelativeOrAbsolute));
            lbl_data_browse.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF3467b0"));

        }

        private void Data_process_mouse_enter(object sender, MouseEventArgs e)
        {
            img_data_process.Source = new BitmapImage(new Uri(@"img/dataprocess_hover.png", UriKind.RelativeOrAbsolute));
            lbl_data_process.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
        }

        private void Data_process_mouse_leave(object sender, MouseEventArgs e)
        {
            img_data_process.Source = new BitmapImage(new Uri(@"img/dataprocess.png", UriKind.RelativeOrAbsolute));
            lbl_data_process.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF3467b0"));
        }

        private void Gttable_mouse_enter(object sender, MouseEventArgs e)
        {
            img_gt_table.Source = new BitmapImage(new Uri(@"img/gttable_hover.png", UriKind.RelativeOrAbsolute));
            lbl_gt_table.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
        }

        private void Gttable_mouse_leave(object sender, MouseEventArgs e)
        {
            img_gt_table.Source = new BitmapImage(new Uri(@"img/gttable.png", UriKind.RelativeOrAbsolute));
            lbl_gt_table.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF3467b0"));
        }

        private void Crosstable_mouse_enter(object sender, MouseEventArgs e)
        {
            img_cross.Source = new BitmapImage(new Uri(@"img/crosschart_hover.png", UriKind.RelativeOrAbsolute));
            lbl_cross.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
        }
        private void Crosstable_mouse_leave(object sender, MouseEventArgs e)
        {
            img_cross.Source = new BitmapImage(new Uri(@"img/crosschrt.png", UriKind.RelativeOrAbsolute));
            lbl_cross.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF3467b0"));
        }
        private void falist_mouse_enter(object sender, MouseEventArgs e)
        {
            img_falist.Source = new BitmapImage(new Uri(@"img/falist_hover.png", UriKind.RelativeOrAbsolute));
            lbl_falist.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
        }

        private void falist_mouse_leave(object sender, MouseEventArgs e)
        {
            img_falist.Source = new BitmapImage(new Uri(@"img/falist.png", UriKind.RelativeOrAbsolute));
            lbl_falist.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF3467b0"));
        }

        private void Data_output_mouse_enter(object sender, MouseEventArgs e)
        {
            img_data_export.Source = new BitmapImage(new Uri(@"img/dataexport_hover.png", UriKind.RelativeOrAbsolute));
            lbl_data_export.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
        }

        private void Data_output_mouse_leave(object sender, MouseEventArgs e)
        {
            img_data_export.Source = new BitmapImage(new Uri(@"img/dataexport.png", UriKind.RelativeOrAbsolute));
            lbl_data_export.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF3467b0"));
        }

        private void Multi_mouse_enter(object sender, MouseEventArgs e)
        {
            img_multi.Source = new BitmapImage(new Uri(@"img/mutivarint_hover.png", UriKind.RelativeOrAbsolute));
            lbl_damulti.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
        }

        private void Multi_mouse_leave(object sender, MouseEventArgs e)
        {
            img_multi.Source = new BitmapImage(new Uri(@"img/multivarint.png", UriKind.RelativeOrAbsolute));
            lbl_damulti.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF3467b0"));
        }

        private void MainWindowOnActivated(object sender, EventArgs e)
        {

            if (isExecuteClicked)
            {
                isExecutedDataProcess = true;

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                excel.Worksheet settingssheet = null;
                excel.Range dpcell = null;
                new Thread(() =>
                {
                    settingssheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Qc4Launcher.Util.Constants.SheetCodeName.Setting);
                    bool events = settingssheet.Application.EnableEvents;
                    settingssheet.Application.EnableEvents = true;
                    dpcell = settingssheet.Cells[QC4Common.Common.Constants.STD_DP.Execute_Row, QC4Common.Common.Constants.STD_DP.Execute_Column];
                    dpcell.Value = QC4Common.Common.Constants.STD_DP.Execute_KeyWord;
                }
                ).Start();

                isExecuteClicked = false;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
            //MulivariateNewVariableCheck();
        }

        private void btn_Falist_Click(object sender, RoutedEventArgs e)
        {


            if (integritycheck)
            {
                FA_List grossTabulationSetting = new FA_List(excelWorkbook, this);
                grossTabulationSetting.ShowDialog();
            }
            else
            {
                MessageDialog.ErrorOk(LocalResource.QC4_INTEGRITYCHECK_FAIL);
            }
        }

        private void Btn_Multivaries_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            string path = txt_FileName.Text;
            crossflag = true;

            MV_Main crossTabulation = new MV_Main(this, excelWorkbook, path, xlapp1);
            crossTabulation.Owner = this;
            crossTabulation.ShowDialog();
        }

        string SwitchedLang = "";

        public void ChangeWorkbookLanguage(excel.Workbook Workbook, ProgressBar prog = null)
        {
            try
            {
                double p = 0;
                Workbook.Application.EnableEvents = false;
                Excel.XlCalculation xlCalculation = Workbook.Application.Calculation;
                Workbook.Application.Calculation = Excel.XlCalculation.xlCalculationManual;
                Workbook.Application.ScreenUpdating = false;
                Workbook.Application.DisplayAlerts = false;
                int index = 0;
                foreach (Excel.Worksheet sht in Workbook.Worksheets)
                {
                    switch (sht.CodeName)
                    {
                        case QC4Common.Common.Constants.SheetCodeName.QuestionSetting:
                            foreach (Excel.Shape shp in sht.Shapes)
                            {
                                if (QC4Common.Common.Constants.QS.QsTopLabelName == shp.Name)
                                {
                                    shp.TextFrame2.TextRange.Text = LocalResource.QS_TOP_LABEL_TEXT;
                                }
                                else
                                {
                                    index = 0;
                                    if (shp.Type != MsoShapeType.msoGroup)
                                    {
                                        continue;
                                    }
                                    if (!shp.Name.Contains("Drop Down"))
                                        foreach (Excel.Shape item in shp.GroupItems)
                                        {
                                            if (index == 0)
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.LBL_QUESTION_SETTING;
                                                item.TextFrame2.TextRange.Characters.Font.Size = 12;
                                            }
                                            else
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.QS_TOP_BOX_TEXT;
                                            }
                                            index++;
                                        }
                                }
                            }
                            sht.Range["C3"].RowHeight = 54;
                            sht.Range["C3"].Value = LocalResource.REPORT_LAYOUT_QUESTION_NUMBER_COLUMN_CAPTION;
                            sht.Range["D3"].Value = LocalResource.LBL_QUESTION_TYPE;
                            sht.Range["E3"].Value = LocalResource.QS_SHEET_HEADING_NO_OF_QUESTIONS;
                            sht.Range["F3"].Value = LocalResource.LBL_VARIABLE;
                            sht.Range["G3"].Value = LocalResource.ADDQS_ANSWERTYPE_LABEL;
                            sht.Range["H3"].Value = LocalResource.LBL_CATEGORY_COUNT;
                            sht.Range["J3"].Value = LocalResource.QS_SHEET_HEADING_COLUMN;
                            sht.Range["K3"].Value = LocalResource.QS_TXT_LABEL_TABLEHEADING;
                            sht.Range["L3"].Value = LocalResource.ADDQS_QUESTION_LABEL;
                            sht.Range["AMB3"].Value = LocalResource.QS_SHEET_HEADING_ADD_SUBTOTALS;
                            sht.Range["AMC3"].Value = LocalResource.QS_SHEET_HEADING_NO_SUBTOTALS;
                            sht.Range["AMD3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_SUBTOTALS, 1);
                            sht.Range["AME3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_CRITERION, 1);
                            sht.Range["AMF3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_SUBTOTALS, 2);
                            sht.Range["AMG3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_CRITERION, 2);
                            sht.Range["AMH3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_SUBTOTALS, 3);
                            sht.Range["AMI3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_CRITERION, 3);
                            sht.Range["AMJ3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_SUBTOTALS, 4);
                            sht.Range["AMK3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_CRITERION, 4);
                            sht.Range["AML3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_SUBTOTALS, 5);
                            sht.Range["AMM3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_CRITERION, 5);
                            sht.Range["AMN3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_SUBTOTALS, 6);
                            sht.Range["AMO3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_CRITERION, 6);
                            sht.Range["AMP3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_SUBTOTALS, 7);
                            sht.Range["AMQ3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_CRITERION, 7);
                            sht.Range["AMR3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_SUBTOTALS, 8);
                            sht.Range["AMS3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_CRITERION, 8);
                            sht.Range["AMT3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_SUBTOTALS, 9);
                            sht.Range["AMU3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_CRITERION, 9);
                            sht.Range["AMV3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_SUBTOTALS, 10);
                            sht.Range["AMW3"].Value = string.Format(LocalResource.QS_SHEET_HEADING_CRITERION, 10);
                            p += 5;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            Excel.Range startCell = sht.Cells[4, 7];
                            Excel.Range endCell = sht.Cells[1048576, 7];
                            Excel.Range paramRange = sht.Range[startCell, endCell];
                            SetToolTipforselectedcells(paramRange, LocalResource.QS_SHEET_ANS_TYPE_TOOLTIP_HEADER, LocalResource.QS_SHEET_ANS_TYPE_TOOLTIP_DESC);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.DataProcess:
                            foreach (Excel.Shape shp in sht.Shapes)
                            {
                                index = 0;
                                if (shp.Type != MsoShapeType.msoGroup)
                                {
                                    continue;
                                }
                                if (!shp.Name.Contains("Drop Down"))
                                    foreach (Excel.Shape item in shp.GroupItems)
                                    {
                                        if (index == 0)
                                        {
                                            item.TextFrame2.TextRange.Text = LocalResource.TITLE_DATA_PROCESSING;
                                            item.TextFrame2.TextRange.Characters.Font.Size = 12;
                                        }
                                        else
                                        {
                                            item.TextFrame2.TextRange.Text = LocalResource.DP_TOP_BOX_TEXT2;
                                        }
                                        index++;
                                    }
                            }
                            sht.Range["B3"].Value = LocalResource.LBL_VARIABLE;
                            sht.Range["C3"].Value = LocalResource.GT_TABULATION_ON_OFF;
                            sht.Range["C3"].WrapText = true;
                            sht.Range["D3"].Value = AddinResource.checkBoxCross;
                            sht.Range["E3"].Value = LocalResource.LABEL_CRITERIA;
                            sht.Range["E4"].Value = LocalResource.LBL_VARIABLE;
                            sht.Range["F4"].Value = LocalResource.LABEL_OPERATOR;
                            sht.Range["F4"].ColumnWidth = 8;
                            sht.Range["G4"].Value = LocalResource.LABEL_VALUE;
                            sht.Range["H3"].Value = AddinResource.LISTUP__INSTRUCTION;
                            sht.Range["H3"].WrapText = true;
                            sht.Range["I3"].Value = LocalResource.DP_SHEET_HEADING_SUBSTITUTE;
                            sht.Range["I4"].Value = LocalResource.LBL_VARIABLE;
                            sht.Range["J4"].Value = LocalResource.LABEL_OPERATOR;
                            Excel.Range rng = sht.get_Range(sht.Range["K3", "AMZ3"], sht.Range["K4", "AMZ4"]);
                            rng.WrapText = true;
                            string param = LocalResource.HEADING_PARAMETER;
                            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
                                param = param.Replace(" ", "");
                            string[,] arry = new string[1, 1000];
                            for (int i = 0; i < 1000; i++)
                            {
                                arry[0, i] = string.Format(param, i + 1);
                            }
                            rng.Value = arry;

                            p += 5;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            SetToolTip(sht);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.CrossTabulation:
                            foreach (Excel.Shape shp in sht.Shapes)
                            {
                                if (QC4Common.Common.Constants.CR.CrTopTextboxName == shp.Name)
                                {
                                    string txt = shp.TextFrame2.TextRange.Text;
                                    if (!string.IsNullOrWhiteSpace(txt))
                                    {
                                        shp.TextFrame2.TextRange.Text = Qc4Launcher.Util.CommonFunction.TranslateOptionMessage(txt);
                                    }
                                }
                                else
                                {
                                    index = 0;
                                    if (shp.Type != MsoShapeType.msoGroup)
                                    {
                                        continue;
                                    }
                                    if (!shp.Name.Contains("Drop Down"))
                                        foreach (Excel.Shape item in shp.GroupItems)
                                        {
                                            if (index == 0)
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.CR_SHEET_HEADING_CROSS_TABULATE;
                                                item.TextFrame2.TextRange.Characters.Font.Size = 12;
                                            }
                                            else
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.Cr_TOP_BOX_TEXT;
                                            }
                                            index++;
                                        }
                                }
                            }
                            sht.Range["B3"].Value = LocalResource.CR_SHEET_HEADING_FILTER;
                            sht.Range["B5"].Value = LocalResource.CR_SHEET_HEADING_THREE_WAY_CROSS;
                            sht.Range["B8"].Value = LocalResource.CR_SHEET_HEADING_BREAKDOWN;
                            sht.Range["B11"].Value = LocalResource.CR_SHEET_HEADING_LINE_GRAPH;
                            sht.Range["B12"].Value = "GT";
                            sht.Range["D4"].Value = LocalResource.CR_SHEET_HEADING_CRITERIA_VALUE;
                            sht.Range["D12"].Value = LocalResource.CR_SHEET_HEADING_COMBINATION;

                            p += 4;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.GTTabulation:
                            foreach (Excel.Shape shp in sht.Shapes)
                            {
                                if (QC4Common.Common.Constants.CR.CrTopTextboxName == shp.Name)
                                {
                                    string txt = shp.TextFrame2.TextRange.Text;
                                    if (!string.IsNullOrWhiteSpace(txt))
                                    {
                                        shp.TextFrame2.TextRange.Text = Qc4Launcher.Util.CommonFunction.TranslateOptionMessage(txt);
                                    }
                                }
                                else
                                {
                                    index = 0;
                                    if (shp.Type != MsoShapeType.msoGroup)
                                    {
                                        continue;
                                    }
                                    if (!shp.Name.Contains("Drop Down"))
                                        foreach (Excel.Shape item in shp.GroupItems)
                                        {
                                            if (index == 0)
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.TITLE_GT_SHEET;
                                            }
                                            else
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.GT_TOP_BOX_TEXT;
                                            }
                                            index++;
                                        }
                                }
                            }
                            sht.Range["B3"].Value = LocalResource.GT_TABULATION_ON_OFF;
                            sht.Range["B3"].WrapText = true;
                            sht.Range["C3"].Value = LocalResource.GT_TABLE_TYPE;
                            sht.Range["C3"].ColumnWidth = 9;
                            sht.Range["D3"].Value = LocalResource.GT_SHEET_HEADING_TEST;
                            sht.Range["E3"].ColumnWidth = 10;
                            sht.Range["E3"].Value = LocalResource.GT_TABULATION_GRAPH;
                            sht.Range["F3"].Value = LocalResource.QS_TXT_LABEL_TABLEHEADING;
                            sht.Range["G3"].Value = LocalResource.GT_TABULATION_VARIABLE;
                            p += 3;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);

                            Excel.Range targetCell = sht.Cells[Constants.GT.GtRowDataStart, Constants.GT.GtColChartType];
                            Excel.Range gtE = ExcelUtil.EndxlUp(targetCell);
                            Excel.Range gtTotal = sht.get_Range(targetCell, gtE);
                            int row = gtTotal.Rows.Count;
                            Excel.Range range = sht.Cells[Constants.GT.GtRowDataStart, Constants.GT.GtColGraphType].Resize[row];
                            string graph = "";
                            int rst = 1;
                            int cst = 1;
                            object[,] val = null;
                            if (range.Value is string)
                            {
                                graph = range.Value;
                                rst = 0;
                                cst = 0;
                                if (graph == "")
                                    break;
                                val = new object[row, 1];
                                val[0, 0] = graph;
                            }
                            else
                                val = range.Value;
                            object[,] val2 = new object[row, 1];
                            for (int g = 0; g < row; g++)
                            {
                                val2[g, 0] = CommonFunc.GetGraphByLanguage(Convert.ToString(val[g + rst, cst]));
                            }
                            QC4Common.Common.GTAutoSetting.FNCCellFormatInitialize(range);
                            p += 6;
                            if (prog != null)
                                OnWorkerMethodComplete(p, "Switching language.");
                            NewChangeChart(row, Constants.GT.GtRowDataStart, sht, prog, ref p);
                            range.Value = val2;
                            p += 3;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.FACreation:
                            foreach (Excel.Shape shp in sht.Shapes)
                            {
                                if (QC4Common.Common.Constants.CR.CrTopTextboxName == shp.Name)
                                {
                                }
                                else
                                {
                                    index = 0;
                                    if (shp.Type != MsoShapeType.msoGroup)
                                    {
                                        continue;
                                    }
                                    if (!shp.Name.Contains("Drop Down"))
                                        foreach (Excel.Shape item in shp.GroupItems)
                                        {
                                            if (index == 0)
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.TITLE_FA_SHEET;
                                            }
                                            else
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.FA_TOP_BOX_TEXT;
                                            }
                                            index++;
                                        }
                                }
                            }
                            sht.Range["B3"].Value = LocalResource.GT_TABULATION_ON_OFF;
                            sht.Range["B3"].WrapText = true;
                            sht.Range["C3"].Value = LocalResource.FA_SHEET_HEADING_CRITERION_DEVIDE;
                            sht.Range["C3"].WrapText = true;
                            sht.Range["C3"].ColumnWidth = 10;
                            sht.Range["C3"].Font.Size = 6;
                            sht.Range["D3"].Value = LocalResource.FALIST_FA_VARIABLE;
                            sht.Range["AH3"].Value = LocalResource.FALIST_ADDITIONAL_VARIABLE;

                            p += 4;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.LDEL:
                            sht.Unprotect(Constants.Password);
                            sht.Range["A1"].Value = LocalResource.LDEL_SHEET_HEADING_FILE_NAME;
                            sht.Range["A1"].ColumnWidth = 14.5;
                            sht.Range["A2"].Value = LocalResource.LDEL_SHEET_HEADING_PARAMETER_NAME;
                            sht.Range["A3"].Value = LocalResource.LDEL_SHEET_HEADING_SUBJECT_DELETE;
                            sht.Protect(Constants.Password);
                            p += 2;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.DataProcessS:
                            p += 2;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.GTTabulationS:
                            ChangeOnOffStatus(sht.Range["B5"].EntireColumn);
                            p += 2;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.CrossTabulationS:
                            p += 3;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.FACreationS:
                            p += 2;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.AnalysisSettingS:
                            p += 2;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.Data01:
                            foreach (Excel.Shape shp in sht.Shapes)
                            {
                                if (QC4Common.Common.Constants.Data.DataTopShapeName1 == shp.Name)
                                {
                                    shp.TextFrame2.TextRange.Text = LocalResource.TITLE_DATA_SHEET;
                                }
                                else if (QC4Common.Common.Constants.Data.DataTopShapeName2 == shp.Name)
                                {
                                    shp.TextFrame2.TextRange.Text = "";
                                }
                                else
                                {
                                    index = 0;
                                    if (shp.Type != MsoShapeType.msoGroup)
                                    {
                                        continue;
                                    }
                                    if (!shp.Name.Contains("Drop Down"))
                                        foreach (Excel.Shape item in shp.GroupItems)
                                        {
                                            if (index == 0)
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.DATA_TOP_BOX_TEXT;
                                                item.TextFrame2.TextRange.Characters.Font.Size = 11;
                                            }
                                            else
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.TITLE_DATA_SHEET;
                                            }
                                            index++;
                                        }
                                }
                            }
                            p += 2;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetCodeName.SummaryT:
                            foreach (Excel.Shape shp in sht.Shapes)
                            {
                                if (QC4Common.Common.Constants.CR.CrTopTextboxName == shp.Name)
                                {
                                    string txt = shp.TextFrame2.TextRange.Text;
                                    if (!string.IsNullOrWhiteSpace(txt))
                                    {
                                        shp.TextFrame2.TextRange.Text = Qc4Launcher.Util.CommonFunction.TranslateOptionMessage(txt);
                                    }
                                }
                                else
                                {
                                    index = 0;
                                    if (shp.Type != MsoShapeType.msoGroup)
                                    {
                                        continue;
                                    }
                                    if (!shp.Name.Contains("Drop Down"))
                                        foreach (Excel.Shape item in shp.GroupItems)
                                        {
                                            if (index == 0)
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.TITLE_SUMMARY_FORM;
                                            }
                                            else
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.SL_TOP_BOX_TEXT;
                                            }
                                            index++;
                                        }
                                }
                            }
                            sht.Range["B4"].Value = LocalResource.CR_SHEET_HEADING_BREAKDOWN;
                            sht.Range["C7"].Value = LocalResource.ST_SHEET_HEADING_OUTPUT_NAME;
                            sht.Range["E3"].Value = LocalResource.ST_SHEET_HEADING_OUTPUT;
                            p += 2;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                    }
                    switch (sht.Name)
                    {
                        case QC4Common.Common.Constants.SheetType.sh_Data_After:
                            foreach (Excel.Shape shp in sht.Shapes)
                            {
                                if (QC4Common.Common.Constants.Data.DataTopShapeName1 == shp.Name)
                                {
                                    shp.TextFrame2.TextRange.Text = LocalResource.TITLE_DATA_SHEET;
                                }
                                else if (QC4Common.Common.Constants.Data.DataTopShapeName2 == shp.Name)
                                {
                                    shp.TextFrame2.TextRange.Text = "";
                                }
                                else
                                {
                                    index = 0;
                                    if (shp.Type != MsoShapeType.msoGroup)
                                    {
                                        continue;
                                    }
                                    if (!shp.Name.Contains("Drop Down"))
                                        foreach (Excel.Shape item in shp.GroupItems)
                                        {
                                            if (index == 0)
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.DATA_TOP_BOX_TEXT;
                                                item.TextFrame2.TextRange.Characters.Font.Size = 11;
                                            }
                                            else
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.TITLE_DATA_SHEET;
                                            }
                                            index++;
                                        }
                                }
                            }
                            p += 1;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetType.sh_Data_After2:
                            foreach (Excel.Shape shp in sht.Shapes)
                            {
                                if (QC4Common.Common.Constants.Data.DataTopShapeName1 == shp.Name)
                                {
                                    shp.TextFrame2.TextRange.Text = LocalResource.TITLE_DATA_SHEET;
                                }
                                else if (QC4Common.Common.Constants.Data.DataTopShapeName2 == shp.Name)
                                {
                                    shp.TextFrame2.TextRange.Text = "";
                                }
                                else
                                {
                                    index = 0;
                                    if (shp.Type != MsoShapeType.msoGroup)
                                    {
                                        continue;
                                    }
                                    if (!shp.Name.Contains("Drop Down"))
                                        foreach (Excel.Shape item in shp.GroupItems)
                                        {
                                            if (index == 0)
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.DATA_TOP_BOX_TEXT;
                                                item.TextFrame2.TextRange.Characters.Font.Size = 11;
                                            }
                                            else
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.TITLE_DATA_SHEET;
                                            }
                                            index++;
                                        }
                                }
                            }
                            p += 1;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                        case QC4Common.Common.Constants.SheetType.sh_Data02:
                            foreach (Excel.Shape shp in sht.Shapes)
                            {
                                if (QC4Common.Common.Constants.Data.DataTopShapeName1 == shp.Name)
                                {
                                    shp.TextFrame2.TextRange.Text = LocalResource.TITLE_DATA_SHEET;
                                }
                                else if (QC4Common.Common.Constants.Data.DataTopShapeName2 == shp.Name)
                                {
                                    shp.TextFrame2.TextRange.Text = "";
                                }
                                else
                                {
                                    index = 0;
                                    if (shp.Type != MsoShapeType.msoGroup)
                                    {
                                        continue;
                                    }
                                    if (!shp.Name.Contains("Drop Down"))
                                        foreach (Excel.Shape item in shp.GroupItems)
                                        {
                                            if (index == 0)
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.DATA_TOP_BOX_TEXT;
                                                item.TextFrame2.TextRange.Characters.Font.Size = 11;
                                            }
                                            else
                                            {
                                                item.TextFrame2.TextRange.Text = LocalResource.TITLE_DATA_SHEET;
                                            }
                                            index++;
                                        }
                                }
                            }
                            p += 1;
                            if (prog != null)
                                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
                            break;
                    }
                }
                Workbook.Application.EnableEvents = true;
                Workbook.Application.Calculation = xlCalculation;
                Workbook.Application.ScreenUpdating = true;
                Workbook.Application.DisplayAlerts = true;
                if (prog != null)
                {
                    OnWorkerMethodComplete(100, LocalResource.PB_SWITCH_COMPLETED);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                OnWorkerMethodComplete(100, LocalResource.PB_SWITCH_LANGUAGE);
            }
        }
        private void ChangeOnOffStatus(excel.Range r, string jpCellOn = "○", string jpCelloff = "×")
        {
            object m = Type.Missing;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
            {
                r.Replace(
"On",
jpCellOn,
Excel.XlLookAt.xlWhole,
Excel.XlSearchOrder.xlByRows,
true, m, m, m);
                r.Replace(
"Off",
jpCelloff,
Excel.XlLookAt.xlWhole,
Excel.XlSearchOrder.xlByRows,
true, m, m, m);
            }
            else
            {
                r.Replace(
jpCelloff,
"Off",
Excel.XlLookAt.xlWhole,
Excel.XlSearchOrder.xlByRows,
true, m, m, m);
                r.Replace(
jpCellOn,
"On",
Excel.XlLookAt.xlWhole,
Excel.XlSearchOrder.xlByRows,
true, m, m, m);
            }
        }
        private void SetToolTip(Excel.Worksheet sht)
        {
            Excel.Range usedRange = sht.UsedRange;
            object[,] dpval = usedRange.Value;
            for (int i = 5; i <= dpval.GetLength(0); i++)
            {
                try
                {
                    if (dpval[i, 8] != null)
                    {
                        switch (dpval[i, 8].ToString())
                        {
                            case ExcelAddIn.Common.Constants.DP.InstructionLDEL:
                                SetToolTipforselectedcells(sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.LDEL_PARAM1_TOOLTIPHEADER, AddinResource.LDEL_PARAM1_TOOLTIPDESC);
                                break;
                            case ExcelAddIn.Common.Constants.DP.InstructionTHEN:
                                if (dpval[i, 10] != null)
                                {
                                    SetToolTipForSub(dpval[i, 10].ToString(), i, sht, dpval);
                                }
                                break;
                            case ExcelAddIn.Common.Constants.DP.InstructionOMIT:
                                //for (int j = ExcelAddIn.Common.Constants.DP.SubstituteParam1Column; j <= ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN; j++)
                                //{
                                Excel.Range CurrentCell = sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column];
                                Excel.Range startCell = sht.Cells[CurrentCell.Row, Constants.DP.SubstituteParam1Column];
                                Excel.Range endCell = sht.Cells[CurrentCell.Row, Constants.DP.MAX_DP_COLUMN];
                                Excel.Range paramRange = sht.Range[startCell, endCell];
                                SetToolTipforselectedcells(paramRange, AddinResource.OMIT_PARAM1_TOOLTIPHEADER, AddinResource.OMIT_PARAM1_TOOLTIPDESC);
                                //}
                                break;
                            case ExcelAddIn.Common.Constants.DP.InstructionOMIT2:
                                SetToolTipforselectedcells(sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.OMIT2_PARAM1_TOOLTIPHEADER, AddinResource.OMIT2_PARAM1_TOOLTIPDESC);
                                SetToolTipforselectedcells(sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column], AddinResource.OMIT2_PARAM2_TOOLTIPHEADER1, AddinResource.OMIT2_PARAM2_TOOLTIPDESC1);
                                break;
                            case ExcelAddIn.Common.Constants.DP.InstructionDECST:
                                SetToolTipforselectedcells(sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.DECST_PARAM1_TOOLTIPHEADER, AddinResource.DECST_PARAM1_TOOLTIPDESC);
                                SetToolTipforselectedcells(sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column], AddinResource.DECST_PARAM2_TOOLTIPHEADER, AddinResource.DECST_PARAM2_TOOLTIPDESC);
                                for (int j = 13; j < dpval.GetLength(1); j++)
                                {
                                    if (dpval[i, j] == null)
                                        break;
                                    else
                                        SetToolTipforselectedcells(sht.Cells[i, j], AddinResource.DECST_PARAMN_TOOLTIPHEADER, AddinResource.DECST_PARAMN_TOOLTIPDESC);
                                }
                                break;
                            case ExcelAddIn.Common.Constants.DP.InstructionCALL:
                                SetToolTipforselectedcells(sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.CALL_PARAM1_TOOLTIP_HEADER, AddinResource.CALL_PARAM1_TOOLTIP_DESC);
                                int c = 0;
                                for (int j = ExcelAddIn.Common.Constants.DP.SubstituteParam2Column; j < dpval.GetLength(1); j++)
                                {
                                    c++;
                                    bool isSuccess = SetToolTipforselectedcells(sht.Cells[i, j], string.Format(AddinResource.CALL_PARAMN_TOOLTIP_HEADER, c), string.Format(AddinResource.CALL_PARAMN_TOOLTIP_DESC, c));
                                    if (!isSuccess)
                                        break;
                                }
                                break;
                            case ExcelAddIn.Common.Constants.DP.InstructionFOR:
                                SetToolTipforselectedcells(sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.VAL_PM1_ERROR_TITLE, AddinResource.VAL_PM1_ERROR_MESSAGE);
                                SetToolTipforselectedcells(sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column], AddinResource.VAL_PM2_ERROR_TITLE, AddinResource.VAL_PM2_ERROR_MESSAGE);
                                SetToolTipforselectedcells(sht.Cells[i, 13], AddinResource.VAL_PM3_ERROR_TITLE, AddinResource.VAL_PM3_ERROR_MESSAGE);
                                break;
                            case ExcelAddIn.Common.Constants.DP.InstructionLISTUP:
                                Excel.Range param1column = sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column];
                                Excel.Range paramncolumn = sht.Cells[i, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column + 80];
                                Excel.Range paramrange = sht.Range[param1column, paramncolumn];
                                SetToolTipforselectedcells(paramrange, AddinResource.LISTUP_TOOLTIP_HEADER, AddinResource.LISTUP_TOOLTIP_DESC);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
        }
        /// <summary>
        /// Set tooltip for substitute operators
        /// </summary>
        /// <param name="operatr">Operator name</param>
        /// <param name="row">row number</param>
        /// <param name="sht">Data process worksheet</param>
        /// <param name="dpval">list of columns</param>
        private void SetToolTipForSub(string operatr, int row, Excel.Worksheet sht, object[,] dpval)
        {
            switch (operatr)
            {
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorRECODE:
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.RECODE_PARAM1_TOOLTIPHEADER, AddinResource.RECODE_PARAM1_TOOLTIPDESC);
                    if (dpval[row, 9] != null && Definiotion.VariableDictionary.ContainsKey(dpval[row, 9].ToString()))
                    {
                        List<string> choices = Definiotion.VariableDictionary[dpval[row, 9].ToString()].Choices;
                        for (int i = 0; i < choices.Count; i++)
                        {
                            string tooltipmessage = QC4Common.Common.CommonFunctions.FormatMsg(AddinResource.RECODE_PARM_N_TOOLTIPDESC, choices[i]);
                            SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column + i], AddinResource.RECODE_PARM_N_TOOLTIPHEADER, tooltipmessage);
                        }
                    }
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorMIN:
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorMAX:
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorAVG:
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorSUM:
                    Excel.Range startCell = sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column];
                    Excel.Range endCell = sht.Cells[row, ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN];
                    Excel.Range paramRange = sht.Range[startCell, endCell];
                    SetToolTipforselectedcells(paramRange, operatr, AddinResource.AGGREGATE_PARAMS_TOOTTIPDESC_);
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorMTOS:
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.MTOS_PARAM1_TOOTIPHEADER, AddinResource.MTOS_PARAM1_TOOTIPDESC);
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column], AddinResource.MTOS_PARAM2_TOOTIPHEADER, AddinResource.MTOS_PARAM2_TOOTIPDESC);
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD1:
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorMINUS1:
                    string tooltipheader = string.Equals(operatr, ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD1) ? AddinResource.ADD1_PARAM1_TOOLTIPHEADER : AddinResource.MINUS1_PARAM1_TOOLTIPHEADER;
                    string tooltipdesc = string.Equals(operatr, ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD1) ? AddinResource.ADD1_PARAM1_TOOLTIPDESC : AddinResource.MINUS1_PARAM1_TOOLTIPDESC;
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], tooltipheader, tooltipdesc);
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column], AddinResource.ADD1MINUS1_PARAM2_TOOLTIPHEADER, AddinResource.ADD1MINUS1_PARAM2_TOOLTIPDESC);
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD2:
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorMINUS2:
                    tooltipheader = AddinResource.MINUS2_PARAM1_TOOLTIPHEADER;
                    tooltipdesc = AddinResource.MINUS2_PARAM1_TOOLTIPDESC;
                    if (string.Equals(operatr, ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD2))
                    {
                        tooltipheader = AddinResource.ADD2_PARAM1_TOOLTIPHEADER;
                        tooltipdesc = AddinResource.ADD2_PARAM1_TOOLTIPDESC;
                    }
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], tooltipheader, tooltipdesc);
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD3:
                    tooltipheader = AddinResource.ADD3_PARAM1_TOOLTIPHEADER;
                    tooltipdesc = AddinResource.ADD3_PARAM1_TOOLTIPDESC;
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], tooltipheader, tooltipdesc);
                    startCell = sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column];
                    endCell = sht.Cells[row, ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN];
                    paramRange = sht.Range[startCell, endCell];
                    SetToolTipforselectedcells(paramRange, AddinResource.ADD3_PARAM_N_TOOLTIPHEADER, AddinResource.AGGREGATE_PARAMS_TOOTTIPDESC_ADD3);
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorEQUAL:
                    QuestionSettings questionsetting = new QuestionSettings();
                    tooltipheader = string.Empty;
                    if (dpval[row, 9] != null && Definiotion.VariableDictionary.ContainsKey(dpval[row, 9].ToString()))
                    {
                        questionsetting = Definiotion.VariableDictionary[dpval[row, 9].ToString()];
                        switch (questionsetting.AnswerType)
                        {
                            case Constants.AnswerType.N:
                                tooltipheader = "SAN";
                                break;
                            case Constants.AnswerType.SA:
                                tooltipheader = "SA";
                                break;
                            case Constants.AnswerType.MA:
                                tooltipheader = "SAMA";
                                break;
                            case Constants.AnswerType.FA:
                                tooltipheader = "FA";
                                break;
                        }
                    }
                    else
                    {
                        tooltipheader = AddinResource.INFO_TOOLTIP_TARGET_VARIABLE;
                    }
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], string.Format(AddinResource.EQUAL_PARAM1_TOOLTIPHEADER, tooltipheader), AddinResource.EQUAL_PARAM1_TOOLTIPDESC);
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorCOUNT:
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.COUNT_PARAM1_TOOLTIPHEADER, AddinResource.COUNT_PARAM1_TOOLTIPDESC);
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column], AddinResource.COUNT_PARAM2_TOOLTIPHEADER, AddinResource.COUNT_PARAM2_TOOLTIPDESC);
                    if (dpval[row, 9] != null && Definiotion.VariableDictionary.ContainsKey(dpval[row, 9].ToString()))
                    {
                        List<string> choices = Definiotion.VariableDictionary[dpval[row, 9].ToString()].Choices;
                        for (int i = 1; i <= choices.Count; i++)
                        {
                            string tooltipmessage = QC4Common.Common.CommonFunctions.FormatMsg(AddinResource.RECODE_PARM_N_TOOLTIPDESC, choices[i - 1]);
                            SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column + i], AddinResource.COUNT_PARAM_N_TOOLTIPHEADER, tooltipmessage);
                        }
                    }
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorCLASS:
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.CLASS_PARAM1_TOOLTIPHEADER, AddinResource.CLASS_PARAM1_TOOLTIPDESC);
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column], AddinResource.CLASS_PARAM2_TOOLTIPHEADER, AddinResource.CLASS_PARAM2_TOOLTIPDESC);
                    SetToolTipforselectedcells(sht.Cells[row, 13], AddinResource.CLASS_PARAM3_TOOLTIPHEADER, AddinResource.CLASS_PARAM3_TOOLTIPDESC);
                    if (dpval[row, 9] != null && Definiotion.VariableDictionary.ContainsKey(dpval[row, 9].ToString()))
                    {
                        List<string> choices = Definiotion.VariableDictionary[dpval[row, 9].ToString()].Choices;
                        for (int i = 1; i <= choices.Count; i++)
                        {
                            string value = QC4Common.Common.CommonFunctions.FormatMsg(AddinResource.CLASS_PARAM_N_TOOLTIPDESC, choices[i - 1]);
                            SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam3Column + i], AddinResource.CLASS_PARAM_N_TOOLTIPHEADER, value);
                        }
                    }
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorMCONVERT:
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.MCONVERT_PARAM1_TOOLTIPHEADER, AddinResource.MCONVERT_PARAM1_TOOLTIPDESC);
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column], AddinResource.MCONVERT_PARAM2_TOOLTIPHEADER, AddinResource.MCONVERT_PARAM2_TOOLTIPDESC);

                    string VariableName = sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteVariableColumn].Text;
                    if (Definiotion.VariableDictionary.ContainsKey(VariableName))
                    {
                        QuestionSettings MconvertQst = Definiotion.VariableDictionary[VariableName];
                        for (int i = 0; i < MconvertQst.CategoryCount; i++)
                        {
                            Excel.Range MconvertRow = sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam3Column + i];
                            string tooltipHeader = AddinResource.MCONVERT_ARG_TOOLTIP_HEADER;
                            string tooltipDesc = QC4Common.Common.CommonFunctions.FormatMsg(AddinResource.MCONVERT_ARG_TOOLTIP_DESC, Definiotion.VariableDictionary[VariableName].Choices[i]);
                            SetToolTipforselectedcells(MconvertRow, tooltipHeader, tooltipDesc);
                        }
                    }

                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorINTEGRATE:
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.INTEGRATE_PARAM1_TOOLTIPHEADER, AddinResource.INTEGRATE_PARAM1_TOOLTIPDESC);
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column], AddinResource.INTEGRATE_PARAM2_TOOLTIPHEADER, AddinResource.INTEGRATE_PARAM2_TOOLTIPDESC);
                    int param2value = 0;
                    int totalvariableparams = 0;
                    excel.Range changedCell = sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column];
                    if (!string.IsNullOrEmpty(changedCell.Text))
                    {
                        param2value = Convert.ToInt32(changedCell.Text);
                        totalvariableparams = ExcelAddIn.Common.Constants.DP.SubstituteParam3Column + (param2value * 2) - 1;

                    }
                    for (int i = ExcelAddIn.Common.Constants.DP.SubstituteParam3Column; i <= totalvariableparams; i += 2)
                    {
                        Excel.Range variableparamcell = sht.Cells[changedCell.Row, i];
                        Excel.Range criteriaparamcell = sht.Cells[changedCell.Row, i + 1];
                        SetToolTipforselectedcells(variableparamcell, AddinResource.INTEGRATE_SRC_PARAMN_TOOLTIPHEADER, AddinResource.INTEGRATE_SRC_PARAMN_TOOLTIPDESC);

                        if (i + 1 < totalvariableparams)
                        {
                            SetToolTipforselectedcells(criteriaparamcell, AddinResource.INTEGRATE_CRITERIA_PARAMN_TOOLTIPHEADER, AddinResource.INTEGRATE_CRITERIA_PARAMN_TOOLTIPDESC);
                        }
                    }
                    Excel.Range variablecell = sht.Cells[changedCell.Row, ExcelAddIn.Common.Constants.DP.SubstituteVariableColumn];
                    int integratecategorycount = 0;
                    if (Definiotion.VariableDictionary.ContainsKey(variablecell.Text))
                    {
                        integratecategorycount = Definiotion.VariableDictionary[variablecell.Text].CategoryCount;
                        integratecategorycount = integratecategorycount - 1;
                    }
                    else
                    {
                        integratecategorycount = ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN - totalvariableparams;
                    }
                    int start = totalvariableparams;
                    int stop = start + integratecategorycount;
                    Excel.Range colstart = sht.Cells[changedCell.Row, start];
                    Excel.Range colend = sht.Cells[changedCell.Row, stop];
                    Excel.Range rngrow = sht.Range[colstart, colend];
                    int paramindex = 0;
                    foreach (Excel.Range cell in rngrow)
                    {
                        if (!Definiotion.VariableDictionary.ContainsKey(variablecell.Text))
                            break;
                        string tooltip = QC4Common.Common.CommonFunctions.FormatMsg(AddinResource.INTEGRATE_PARAMN_TOOLTIPDESC, Definiotion.VariableDictionary[variablecell.Text].Choices[paramindex]);
                        SetToolTipforselectedcells(cell, AddinResource.INTEGRATE_PARAMN_TOOLTIPHEADER, tooltip);
                        paramindex++;
                    }
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorJOINT:
                    Excel.Range variableRange = sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column];
                    Excel.Range ValueRange = sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam2Column];
                    for (int i = ExcelAddIn.Common.Constants.DP.SubstituteParam1Column; i <= ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN; i += 2)
                    {
                        Excel.Range paramcolumn = sht.Cells[row, i];
                        variableRange = variableRange.Application.Union(variableRange, paramcolumn);

                        Excel.Range valuecell = sht.Cells[row, i + 1];
                        ValueRange = ValueRange.Application.Union(ValueRange, valuecell);
                    }
                    SetToolTipforselectedcells(variableRange, AddinResource.JOINT_PARAM1_TOOLTIPHEADER, AddinResource.JOINT_PARAM1_TOOLTIPDESC);
                    SetToolTipforselectedcells(ValueRange, AddinResource.JOINT_PARAM2_TOOLTIPHEADER, AddinResource.JOINT_PARAM2_TOOLTIPDESC);
                    break;
                case ExcelAddIn.Common.Constants.DP.SubstituteOperatorCOMPUTE:
                    SetToolTipforselectedcells(sht.Cells[row, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column], AddinResource.COMPUTE_TOOLTIP_HEADER, AddinResource.COMPUTE_TOOLTIP_DESC_);
                    break;
            }
        }

        private bool SetToolTipforselectedcells(Excel.Range range, string InputTitle, string InputMessage)
        {
            /*if (range.Validation.InputTitle != null)
            {
                range.Validation.InputTitle = InputTitle;
                range.Validation.InputMessage = InputMessage;
                return true;
            }
            return false;*/
            try
            {
                string StrTitle = InputTitle;
                string StrMessage = InputMessage;
                bool result1 = String.IsNullOrEmpty(StrTitle);
                bool result2 = String.IsNullOrEmpty(StrMessage);
                if (result1 == false && result2 == false)
                {
                    range.Validation.InputTitle = InputTitle;
                    range.Validation.InputMessage = InputMessage;
                    return true;
                }

                return false;
            }
            catch { return false; }
        }

        private void NewChangeChart(int row, int outRow, Excel.Worksheet gtSheet, ProgressBar prog, ref double p)
        {
            Excel.Range saRange = null;
            Excel.Range maRange = null;
            Excel.Range nRange = null;
            Excel.Range ratRange = null;
            Excel.Range mtnRange = null;
            Excel.Range rnkRange = null;
            Excel.Range mtsRange = null;
            Excel.Range mtmRange = null;

            double inc = (double)40 / row;

            for (int i = 0; i < row; i++, outRow++)
            {
                Excel.Range tCell = gtSheet.Cells[outRow, Constants.GT.GtColChartType];

                Excel.Application xlApp = gtSheet.Application;

                string chartType = tCell.Value2;
                switch (chartType)
                {
                    case Constants.GT.GTSA:
                        saRange = saRange == null ? tCell : xlApp.Union(saRange, tCell);
                        break;
                    case Constants.GT.GTMA:
                        maRange = maRange == null ? tCell : xlApp.Union(maRange, tCell);
                        break;
                    case Constants.GT.GTN:
                        nRange = nRange == null ? tCell : xlApp.Union(nRange, tCell);
                        break;
                    case Constants.GT.GTRAT:
                        ratRange = ratRange == null ? tCell : xlApp.Union(ratRange, tCell);
                        break;
                    case Constants.GT.GTMTN:
                        mtnRange = mtnRange == null ? tCell : xlApp.Union(mtnRange, tCell);
                        break;
                    case Constants.GT.GTRNK:
                        rnkRange = rnkRange == null ? tCell : xlApp.Union(rnkRange, tCell);
                        break;
                    case Constants.GT.GTMTS:
                        mtsRange = mtsRange == null ? tCell : xlApp.Union(mtsRange, tCell);
                        break;
                    case Constants.GT.GTMTM:
                        mtmRange = mtmRange == null ? tCell : xlApp.Union(mtmRange, tCell);
                        break;
                }

                p += inc;
                if (prog != null)
                    OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
            }

            Excel.Range graphCell;

            if (saRange != null)
            {
                graphCell = saRange.Offset[0, 2];
                QC4Common.Common.GTAutoSetting.FNCCellFormatSetting(graphCell, QC4Common.Common.GTAutoSetting.GetGraphList(Constants.GT.GTSA), "", 19);
                graphCell.Validation.ShowInput = false;
            }

            if (maRange != null)
            {
                graphCell = maRange.Offset[0, 2];
                QC4Common.Common.GTAutoSetting.FNCCellFormatSetting(graphCell, QC4Common.Common.GTAutoSetting.GetGraphList(Constants.GT.GTMA), "", 19);
                graphCell.Validation.ShowInput = false;
            }

            if (nRange != null)
            {
                graphCell = nRange.Offset[0, 2];
                QC4Common.Common.GTAutoSetting.FNCCellFormatSetting(graphCell, "", "", 19);
                graphCell.Validation.ShowInput = false;
            }

            if (ratRange != null)
            {
                graphCell = ratRange.Offset[0, 2];
                QC4Common.Common.GTAutoSetting.FNCCellFormatSetting(graphCell, QC4Common.Common.GTAutoSetting.GetGraphList(Constants.GT.GTRAT), "", 19);
                graphCell.Validation.ShowInput = false;
            }

            if (rnkRange != null)
            {
                graphCell = rnkRange.Offset[0, 2];
                QC4Common.Common.GTAutoSetting.FNCCellFormatSetting(graphCell, QC4Common.Common.GTAutoSetting.GetGraphList(Constants.GT.GTRNK), "", 19);
                graphCell.Validation.ShowInput = false;
            }

            if (mtsRange != null)
            {
                graphCell = mtsRange.Offset[0, 2];
                QC4Common.Common.GTAutoSetting.FNCCellFormatSetting(graphCell, QC4Common.Common.GTAutoSetting.GetGraphList(Constants.GT.GTMTS), "", 19);
                graphCell.Validation.ShowInput = false;
            }

            if (mtmRange != null)
            {
                graphCell = mtmRange.Offset[0, 2];
                QC4Common.Common.GTAutoSetting.FNCCellFormatSetting(graphCell, QC4Common.Common.GTAutoSetting.GetGraphList(Constants.GT.GTMTM), "", 19);
                graphCell.Validation.ShowInput = false;
            }
            p += 2;
            if (prog != null)
                OnWorkerMethodComplete(p, LocalResource.PB_SWITCH_LANGUAGE);
        }

        private void ChangeUILanguageManually()
        {
            if (QC4Common.Common.Constants.GlobalMode != "ja-JP," + Util.Constants.QCFont.MS_Gothic)
            {
                lbl_cross.Text = "\x00A0\x00A0\x00A0Cross/\x00A0\x00A0\x00A0\x00A0\x00A0\x00A0\x00A0\x00A0Charts";
                lbl_falist.Text = "\x00A0\x00A0\x00A0" + LocalResource.MAINWINDOW_BUTTON_FA_LIST;
            }
            else
            {
                lbl_cross.Text = LocalResource.MAINWINDOW_BUTTON_CROSS_TABLE;
                lbl_falist.Text = LocalResource.MAINWINDOW_BUTTON_FA_LIST;
            }
            btn_help.Content = LocalResource.BTN_HELP;
            txtblk_merge.Text = LocalResource.BTN_DATA_MERGE;
            txtblk_file_name.Text = LocalResource.LBL_FILE_NAME;
            btn_Open.Content = LocalResource.BTN_OPEN;
            txtblk_group1.Text = LocalResource.MAINWINDOW_GROUP_HEADER1;
            txtblk_group2.Text = LocalResource.MAINWINDOW_GROUP_HEADER2;
            lbl_qs.Text = LocalResource.MAINWINDOW_BUTTON_QS;
            lbl_data_browse.Text = LocalResource.MAINWINDOW_BUTTON_DATABROWSE;
            lbl_data_process.Text = LocalResource.MAINWINDOW_BUTTON_DATAPROCESSING;
            lbl_gt_table.Text = LocalResource.MAINWINDOW_BUTTON_GT_TABLE;
            txtblk_group3.Text = LocalResource.MAINWINDOW_GROUP_HEADER3;
            txtblk_group4.Text = LocalResource.MAINWINDOW_GROUP_HEADER4;
            lbl_data_export.Text = LocalResource.BTN_DATA_OUTPUT;
            lbl_damulti.Text = LocalResource.MAINWINDOW_BUTTON_MULTI_VARIOUS;
            btn_Save.Content = LocalResource.BTN_SAVE2;
            btn_Save_As.Content = LocalResource.BTN_SAVE_AS;
            btn_Close_File.Content = LocalResource.BTN_CLOSE_FILE;
            btn_Exit.Content = LocalResource.BTN_EXIT;
            btn_Swap_Data.Content = LocalResource.BTN_SWAP_DATA;
            btn_ToSheet.Content = LocalResource.BTN_TOSHEET;
            txtExpiry.Text = LocalResource.LABEL_VERSION_DATE + ": " + CommonFunction.ExpiryDate.ToString("yyyy/MM/dd");
            txtVersion.Text = LocalResource.LABEL_VERSION_TYPE;
        }

        public void SaveGlobalSetting(excel.Workbook excelWorkbook)
        {
            try
            {
                Excel.Worksheet seSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.DetailsSetting);
                Excel.Range start = seSheet.Range[Constants.AdvancedSetting.AdvSettingStartCell];
                Excel.Range end = ExcelUtil.EndxlUp(start);
                Excel.Range total = seSheet.get_Range(start, end);
                Excel.Range gmCheck = total.Find("F_Global_Mode_Settings", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                            Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                string[,] valAry = new string[1, 2];
                valAry[0, 0] = "F_Global_Mode_Settings";
                valAry[0, 1] = QC4Common.Common.Constants.GlobalMode;
                if (gmCheck != null)
                {
                    gmCheck = gmCheck.Offset[0, 0];
                    gmCheck.Resize[1, 2].Value2 = valAry;
                }
                else
                {
                    end = end.Offset[1, 0];
                    end.Resize[1, 2].Value2 = valAry;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public bool IsUpdateSheetLanguage = false;
        private void Combo_Language_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                string RFolderPath = CommonFunction.GetTemporaryDirectory() + @"R_FullSet\R-3.6.0\etc\";
                string text = File.ReadAllText(RFolderPath + "Rconsole");
                if (Combo_Language.SelectedIndex == 0)
                {
                    SwitchedLang = "en-US," + Util.Constants.QCFont.Segoe_UI;
                    btn_help.Visibility = Visibility.Hidden;
                    text = text.Replace("language = ja", "language = en");
                }
                else if (Combo_Language.SelectedIndex == 1)
                {
                    SwitchedLang = "ja-JP," + Util.Constants.QCFont.MS_Gothic;
                    btn_help.Visibility = Visibility.Visible;
                    text = text.Replace("language = en", "language = ja");
                }
                File.WriteAllText(RFolderPath + "Rconsole", text);

                if (!MWDLoaded && sender != null)
                {
                    string configPath = CommonFunction.GetTemporaryDirectory() + @"language.config";
                    File.WriteAllText(configPath, SwitchedLang);
                }
                MWDLoaded = false;

                QC4Common.Common.Constants.GlobalMode = SwitchedLang;
                ExcelAddIn.AddinResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
                LocalResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
                QC4Common.CommonResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
                FilterSettingsView.LocalResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
                ChangeUILanguageManually();
                if (excelWorkbook != null)
                {
                    if (!IsUpdateSheetLanguage)
                    {
                        progress = new ProgressBar(LocalResource.PB_SWITCH_LANGUAGE);
                        progress.Owner = this;

                        Thread thread = new Thread(() =>
                        {
                            SaveGlobalSetting(excelWorkbook);
                            ChangeWorkbookLanguage(excelWorkbook, progress);
                        });
                        thread.Start();
                        progress.ShowDialog();
                    }
                    IsUpdateSheetLanguage = false;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        /// <summary>
        /// Parses a color preset XML file. If the file exists in the temporary directory,
        /// it is parsed; otherwise, an attempt is made to restore it from the template folder.
        /// </summary>
        private void ParseColorPresetXml()
        {
            // Create a new Helper instance
            var helper = new Helper();

            // Combine the path to the temporary directory with the color preset XML file name
            string xmlFilePath = Path.Combine(
                CommonFunction.GetTemporaryDirectory(),
                Constant.ColorPresetXmlFileName
            );

            // Check if the XML file exists in the temporary directory or if restoration is successful
            if (File.Exists(xmlFilePath) || TryRestoreXml(xmlFilePath))
            {
                helper.ParseColorPresetXml(xmlFilePath);
            }
        }

        /// <summary>
        /// Tries to restore the color preset XML file from the default template.
        /// </summary>
        /// <param name="destinationPath">The destination path for the restored XML file.</param>
        /// <returns>True if restoration is successful; otherwise, false.</returns>
        private bool TryRestoreXml(string destinationPath)
        {
            string sourcePath = Path.Combine(AppContext.BaseDirectory, "Templates", Constant.ColorPresetXmlFileName);

            try
            {
                File.Copy(sourcePath, destinationPath);
                return true;
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Error restoring the XML file: {0}. Proceeding with default color preset configuration.", ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Tries to rename the Template file in Templates Folder 
        /// </summary>
        /// <param name="installedpath">The source path where application is installed.</param>
        /// <returns>No return value</returns>
        public void RenameTemplates(string installedpath)
        {
            string fullpath = installedpath + @"\Templates";
            try
            {
                if (File.Exists(fullpath + @"\Questionnaire_STD.xlsx"))
                {
                    File.Move(fullpath + @"\Questionnaire_STD.xlsx", fullpath + @"\Questionnaire_Std.xlsx");

                }
                if (File.Exists(fullpath + @"\DataBrowse_STD.xlsx"))
                {
                    File.Move(fullpath + @"\DataBrowse_STD.xlsx", fullpath + @"\DataBrowse_Std.xlsx");

                }
            }
            catch { }
        }
    }
}
