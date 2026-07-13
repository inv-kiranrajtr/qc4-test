using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using QC4Common.Sheets;
using QC4Common.Common;
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

namespace QC4Common.DP
{
    public class CallDP
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public QC4Common.Sheets.DataProcess DataProcessSheet;
        private const int HWND_BROADCAST = 0xffff;
        public static int HWND_MAIN = 0;
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        static extern bool EnableWindow(IntPtr hWnd, bool enable);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);
        private static AutoResetEvent _wait = new AutoResetEvent(false);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(int hWnd, int Msg, int wParam, int lParam);
        BackgroundWorker backgroundWorker;
        public static bool iscancelled;
        public bool DPExecute(Excel.Workbook wb, bool fromSTD = false, int HWND=0, DataProcess dataProcess=null)//191  made for 
        {

            try
            {
                DataProcessSheet = dataProcess;
                object activeSheet = wb.Application.ActiveWindow.ActiveSheet;//191  coded for dpexecute 
                if (activeSheet != null)
                {
                    IntPtr freezeparentwindow = fromSTD ? QC4Common.Common.Util.GetIntPtrWIndowHandleFromSettings(wb) : GetForegroundWindow();
                    //Logic.DataProcessExecute.islistup = false;//for listup
                    Excel.Worksheet settingsSheet = ExcelUtilForAddIn.GetWorkSheetByCodeName(wb.Application.ActiveWorkbook, Constants.SheetCodeName.Setting);
                    Excel.Range listupCell = settingsSheet.Cells[QC4Common.Common.Constants.STD_DP.isListUP_Row, QC4Common.Common.Constants.STD_DP.isListUP_Col];
                    if (listupCell.Value == null)
                        QC4Common.Logic.DataProcessExecute.islistup = false;
                    else
                        QC4Common.Logic.DataProcessExecute.islistup = listupCell.Value;
                    Excel.Worksheet worksheet = (Excel.Worksheet)activeSheet;
                    Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(wb);

                    if (DataProcessSheet == null)
                    {
                        worksheet = ExcelUtilForAddIn.GetWorkSheetByCodeName(wb,Constants.SheetCodeName.DataProcess);
                        DataProcessSheet = new QC4Common.Sheets.DataProcess(worksheet);
                    }
                    if (fromSTD)
                    {
                        ExcelOperate excelOperate = null;
                        Excel.Application xlApp = null;
                        try
                        {
                            if (HWND_MAIN == 0)
                            {
                                HWND_MAIN = QC4Common.Common.Util.GetWIndowHandleFromSettings(wb);
                            }
                            worksheet = ExcelUtilForAddIn.GetWorkSheetByCodeName(wb,Constants.SheetCodeName.DataProcessS);
                            backgroundWorker = new BackgroundWorker();
                            ProgressBar progress = new ProgressBar(worksheet, backgroundWorker);
                            WindowInteropHelper wih = new WindowInteropHelper(progress);
                            wih.Owner = new IntPtr(worksheet.Application.Hwnd);
                            worksheet.Application.Interactive = false;
                            if (fromSTD)
                            {
                                WindowInteropHelper wihprogress = new WindowInteropHelper(progress);
                                IntPtr hForeGroundWindw = fromSTD ? QC4Common.Common.Util.GetIntPtrWIndowHandleFromSettings(wb) : GetForegroundWindow();
                                wihprogress.Owner = hForeGroundWindw;
                                SetParent(progress.hWnd, hForeGroundWindw);
                                EnableWindow(freezeparentwindow, false);
                            }
                            QC4Common.Global.Global.CheckOperationFlag = 0;
                            Microsoft.Office.Interop.Excel.Worksheet settingssheet = ExcelUtilForAddIn.GetWorkSheetByCodeName(wb, Constants.SheetCodeName.Setting);
                            Microsoft.Office.Interop.Excel.Range dpcell = settingssheet.Cells[QC4Common.Common.Constants.STD_DP.CheckList_Row, QC4Common.Common.Constants.STD_DP.CheckList_Column];

                            if (Convert.ToString(dpcell.Value) == QC4Common.Common.Constants.STD_DP.CheckList_ON)
                            {
                                QC4Common.Global.Global.CheckOperationFlag |= QC4Common.Common.Constants.CheckCrossFlag.ChecklistSTD;
                            }
                            // new Thread(() => DataProcessSheet.DataProcessExecute(worksheet, progress, fromSTD)).Start();
                            IntPtr hForeGroundWnd = QC4Common.Common.Util.GetIntPtrWIndowHandleFromSettings(wb);// GetForegroundWindow();
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
                                           QC4Common.Common.MessageDialog.ShowMessageOnParent(CommonResource.MSG_OUTPUT_ABORTED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));

                                        progress.OnWorkerMethodComplete(100, "", IsForceStop: true);
                                        int msg = RegisterWindowMessage(QC4Common.Common.Constants.RibbonMessage.msg_cancelled);
                                        if (msg == 0)
                                        {
                                            MessageBox.Show(Marshal.GetLastWin32Error().ToString(), "QuickCross");
                                        }
                                        SendNotifyMessage(/*HWND_BROADCAST*/HWND_MAIN, msg, 0, 0);
                                        if (wb != null)
                                        {
                                            QC4Common.Util.ExcelUtil.DeleteDataAfterSheets(wb);

                                        }
                                        string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + "Comparison GT.xlsx"; //GetTemplatePath(TEMPLATE_NAME);  
                                        string targetPath = QC4Common.Util.QCUtil.GetTargetPath(QC4Common.Sheets.DataProcess.currworkbook);
                                        targetPath = Path.GetDirectoryName(targetPath) + "\\";
                                        targetPath.Replace("\\\\", "\\");

                                        string fileopenPath = QC4Common.Util.QCUtil.GetTempPath(QC4Common.Sheets.DataProcess.currworkbook);
                                        fileopenPath = Path.GetDirectoryName(fileopenPath) + "\\";
                                        fileopenPath.Replace("\\\\", "\\");

                                        string saveAsPath = targetPath + CommonResource.COMPARISON_FILE_NAME + ".xlsx";
                                        string savefileopenPath = fileopenPath + CommonResource.COMPARISON_FILE_NAME + ".xlsx";

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
                                            SendNotifyMessage(/*HWND_BROADCAST*/HWND_MAIN, msg1, QC4Common.Global.Global.CheckOperationFlag, 0);
                                        }


                                        else
                                        {
                                            progress.Dispatcher.Invoke(() =>
                                           QC4Common.Common.MessageDialog.ShowMessageOnParent(CommonResource.DATAPROCESS_COMPLETED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));
                                        }
                                        progress.OnWorkerMethodComplete(100, CommonResource.DP_PROGRESS_MSG_95, retainThread: true, IsForceStop: true);
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
                            return false;
                        }
                        IntPtr hForeGroundWnd = QC4Common.Common.Util.GetIntPtrWIndowHandleFromSettings(wb);// GetForegroundWindow();
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
                                       QC4Common.Common.MessageDialog.ShowMessageOnParent(CommonResource.MSG_OUTPUT_ABORTED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));

                                    progress.OnWorkerMethodComplete(100, "", IsForceStop: true);

                                    if (wb != null)
                                    {

                                        QC4Common.Util.ExcelUtil.DeleteDataAfterSheets(wb);
                                        string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + "Comparison GT.xlsx"; //GetTemplatePath(TEMPLATE_NAME);  
                                        string targetPath = QC4Common.Util.QCUtil.GetTargetPath(QC4Common.Sheets.DataProcess.currworkbook);
                                        targetPath = Path.GetDirectoryName(targetPath) + "\\";
                                        targetPath.Replace("\\\\", "\\");

                                        string fileopenPath = QC4Common.Util.QCUtil.GetTempPath(QC4Common.Sheets.DataProcess.currworkbook);
                                        fileopenPath = Path.GetDirectoryName(fileopenPath) + "\\";
                                        fileopenPath.Replace("\\\\", "\\");

                                        string saveAsPath = targetPath + CommonResource.COMPARISON_FILE_NAME + ".xlsx";
                                        string savefileopenPath = fileopenPath + CommonResource.COMPARISON_FILE_NAME + ".xlsx";

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
                                        SendNotifyMessage(/*HWND_BROADCAST*/HWND, msg, QC4Common.Global.Global.CheckOperationFlag, 0);
                                        //HWND_MAIN = QC4Common.Common.Util.GetWIndowHandleFromSettings(wb);
                                    }
                                    else
                                    {
                                        _log.Info("DP Ending");
                                        progress.Dispatcher.Invoke(() =>
                                      QC4Common.Common.MessageDialog.ShowMessageOnParent(CommonResource.DATAPROCESS_COMPLETED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));
                                        //  QC4Common.Common.MessageDialog.ShowMessageOnParent(AddinResource.DATAPROCESS_COMPLETED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd);//Redmine id:209279
                                    }
                                    progress.OnWorkerMethodComplete(100, CommonResource.DP_PROGRESS_MSG_95, retainThread: true, IsForceStop: true);
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

                Excel.Worksheet ws = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(wb, Constants.SheetType.sh_Data01 + "(Processed)");
                if (ws == null)
                {                    
                    return false; //return false if worksheet is empty so that buttonUndoDp can be disabled from callerside
                }
                else
                {                    
                    return true;//return false if worksheet is empty so that buttonUndoDp can be enabled from callerside
                }
            }
            catch (Exception ex) { return false; }
            finally
            {

            }
        }
    }
}
