using System;
using Excel = Microsoft.Office.Interop.Excel;
using Constant = QC4Common.Common.Constants;
using Macromill.QCWeb.COMOperate;
using System.Diagnostics;
using log4net;
using System.Reflection;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using System.IO;
using ExcelAddIn.Common;
using System.Runtime.InteropServices;
using System.Threading;

namespace Qc4Launcher.Util
{
    class QC4FileHelper
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void OnWorkerMethodCompleteDelegate(double value, string status);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        public static bool IsSuccess = true;
        public QC4FileHelper()
        {
        }

        public void OpenFile(string filePath, ref Excel.Application application, ref Excel.Workbook workbook, ref String targetPath, ref ExcelOperate excelOperate, MainWindow main, string tempFolder = Constants.PathName.FileOpenTemp, string switchedLang = "")
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string temp = Constants.PathName.FileOpenTemp;

                OnWorkerComplete(1, LocalResource.LOADING_FILE);
                OnWorkerComplete(2, LocalResource.FILE_OPEN_EXTRACTING);
                if (tempFolder.Length < 1)
                {
                    temp = tempFolder;
                }
                //string tempPath = Path.GetTempPath() + temp + fileName + ".qc4";
                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + temp + fileName + ".qc4";
                if (fileName.Length > 143 || tempPath.Length > 260)
                {
                    OnWorkerComplete(100, LocalResource.FILE_OPEN_FAILED);
                    MessageDialog.ErrorOk(LocalResource.FILE_LENGTH_TOO_LARGE);
                    return;
                }
                targetPath = QcFileHelper.ExtractFile(filePath, tempFolder);

                if (null == targetPath)
                {
                    OnWorkerComplete(100, LocalResource.FILE_OPEN_FAILED);
                    MessageDialog.ErrorOk(LocalResource.FILE_CORRUPTED);
                    return;
                }
                OnWorkerComplete(20, LocalResource.FILE_OPEN_CREATE_EXCEL_APP);
                excelOperate = new ExcelOperate();
                application = excelOperate.Excel;
                OnWorkerComplete(22, "");
                application.DisplayAlerts = false;
                OnWorkerComplete(24, LocalResource.FILE_OPEN_EXCEL);
                workbook = application.Workbooks.Open(targetPath + "\\" + Constant.TemplateFile.QC4_Template, 0, false, 5, Constant.Password, "", true,
                       Excel.XlPlatform.xlWindows, "\t", false, false, 0, false, 1, 0);
                try
                {
                    Excel.Worksheet infosheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.Setting);
                    Excel.Range s = infosheet.Cells[Constant.FileProperties.Source_Row, Constant.FileProperties.Source_Column];
                    Excel.Range u = infosheet.Cells[Constant.FileProperties.Updated_Source_Row, Constant.FileProperties.Updated_Source_Column];
                    if (s.Value.ToString() == Constant.FileProperties.QCSV.ToString() && u.Value.ToString() != Constant.FileProperties.QC4.ToString())
                    {
                        foreach (Excel.Name name in workbook.Names)
                        {
                            if (name.Visible == true && name.NameLocal.Contains(QC4Common.Common.Constants.QS.RowNamePrefix))
                            {
                                name.Visible = false;
                            }
                        }
                    }
                }
                catch (Exception ex) { }
                OnWorkerComplete(65, LocalResource.FILE_OPEN_EXCEL_UNPROTECT);
                workbook.Saved = true;
                OnWorkerComplete(70, LocalResource.FILE_OPEN_UPDATING_DB_SETTING);
                Process currentProcess = Process.GetCurrentProcess();

                QC4Common.DB.DBHelper.SetConnectionString(workbook, targetPath, System.IO.Path.GetFileName(filePath), filePath, currentProcess.Id.ToString());
                OnWorkerComplete(77, LocalResource.FILE_OPEN_UPDATE_SETTING);

                Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(workbook);

                OnWorkerComplete(80, LocalResource.FILE_OPEN_UPDATE_SETTING);
                try
                {
                    Excel.Worksheet QSheet = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.QuestionSetting);
                    ReturnClass result = QuestionSettingsUtil.MoveFromQs(workbook, QSheet);
                    OnWorkerComplete(85, LocalResource.FILE_OPEN_UPDATE_SETTING);
                    if (!result.Result)
                    {
                        Qc4Launcher.MainWindow.integritycheck = false;
                    }
                    else
                    {
                        Qc4Launcher.MainWindow.integritycheck = true;
                    }
                }
                catch (Exception ex) { }
                if (Definiotion.VariableDictionary != null && Definiotion.VariableDictionary.Count > 0 && Definiotion.VariableDictionary.ElementAt(0).Key != "SAMPLEID")
                {
                    IsSuccess = false;
                    OnWorkerComplete(100, LocalResource.FILE_OPEN_FAILED);
                    MessageDialog.ErrorOk(LocalResource.QC3PARSE_ALERT_CONTAIN_INVALID_VARIABLE);
                    return;
                }
                OnWorkerComplete(95, LocalResource.FILE_OPEN_UPDATE_SETTING);
                var array = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Org) || (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Imp)).Select(q => q.Value).ToList();
                QC4Common.Util.ExcelUtil.GenerateNewDataSheet(workbook, array);
                if (QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, Constants.SheetType.sh_Data01 + "(Processed)") != null)
                {
                    QC4Common.Util.ExcelUtil.DeleteDataAfterSheets(workbook);
                    array = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == "Org") || (a.Value.QuestionFlag == "Imp") || a.Value.QuestionFlag == "New").Select(q => q.Value).ToList();
                    QC4Common.Util.ExcelUtil.GenerateNewDataSheet(workbook, array, "data_after_process");
                }
/*
                try//Redmine ID 206014]
                {
                    if (QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.MultiVariate) == null)
                    {
                        try
                        {
                            Excel.Worksheet multivariateSheet1 = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.MultiVariateSheet);
                            workbook.Unprotect(Constants.Password);
                            multivariateSheet1.Visible = Excel.XlSheetVisibility.xlSheetVisible;
                            multivariateSheet1.Copy(workbook.Sheets[1], Type.Missing);
                            workbook.Sheets[1].Name = Constants.SheetType.sh_Sheet2;// Constants.SheetType.sh_Sheet2;
                            workbook.Sheets[1].Move(Type.Missing, multivariateSheet1);
                            multivariateSheet1.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
                            // workbook.Save();
                            // multivariateSheet1 = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.MultiVariate);
                            Excel.Worksheet multivariateSheetMAS = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, Constants.SheetType.sh_Sheet2);
                          //  Excel.Range MASstart = multivariateSheetMAS.Cells[Qc4Launcher.Util.Constants.StartRow, Qc4Launcher.Util.Constants.StartCol];
                          //  Excel.Range MASlast = multivariateSheetMAS.Cells[Qc4Launcher.Util.Constants.EndRow, Qc4Launcher.Util.Constants.EndCol];
                          //  Excel.Range MASrar = multivariateSheetMAS.get_Range(MASstart, MASlast);
                          //  MASrar.Cells.ClearContents();
                          //  multivariateSheetMAS.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
                            workbook.Protect(Constants.Password);
                        }
                        catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
                        finally
                        {
                            
                        }
                    }

                }
                catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
                */

                OnWorkerComplete(97, LocalResource.PB_SWITCH_LANGUAGE);
                Excel.Worksheet seSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.DetailsSetting);
                //Excel.Range start = seSheet.Range[Constants.AdvancedSetting.AdvSettingStartCell];
                //Excel.Range end = ExcelUtil.EndxlUp(start);
                //Excel.Range total = seSheet.get_Range(start, end);
                //Excel.Range gmCheck = total.Find("F_Global_Mode_Settings", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                //			Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                //if (gmCheck != null)
                //	QC4Common.Common.Constants.GlobalMode = gmCheck.Offset[0, 1].Text;
                ExcelAddIn.AddinResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
                LocalResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
                QC4Common.CommonResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
                FilterSettingsView.LocalResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
                main.IsUpdateSheetLanguage = true;
                main.ChangeWorkbookLanguage(workbook);
                main.SaveGlobalSetting(workbook);

                OnWorkerComplete(99, LocalResource.FILE_OPEN_COMPLETE);
                OnWorkerComplete(100, LocalResource.TEXT_COMPLETED);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                try
                {
                    excelOperate.Excel.EnableEvents = false;
                    excelOperate.Excel.DisplayAlerts = false;
                }
                catch { }
                try
                {
                    workbook.Close();
                    excelOperate.Excel.Quit();
                }
                catch { }
                finally
                {
                    excelOperate.Dispose();
                }
                targetPath = null;
                OnWorkerComplete(100, LocalResource.FILE_OPEN_FAILED);
                MessageDialog.ErrorOk(LocalResource.FILE_CORRUPTED);
            }
        }

        [DllImport("user32.dll")] static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll")] static extern bool SetForegroundWindow(IntPtr hWnd);

        // This function endlessly waits for a window with the given
        //  title and when found sends it a WM_CLOSE message (16)
        /// <summary>
        /// This function endlessly waits for a window with the given title and set this window to the top
        /// </summary>
        /// <param name="windowTitle">Window title as string</param>
        public static void BringWindowToTop(object windowTitle)
        {
            for (int h = 0; h == 0;)
            {
                Thread.Sleep(500);
                h = FindWindow(null, windowTitle.ToString());
                if (h != 0)
                {
                    SetForegroundWindow((IntPtr)h);
                    //Microsoft.VisualBasic.Interaction.AppActivate("Microsoft Excel"); //another method to bring the window to top
                }
                h = FindWindow(null, "マイクロソフトエクセル");
                if (h != 0)
                {
                    SetForegroundWindow((IntPtr)h);
                }
            }
        }

        /// <summary>
        /// Call a Macro function to disable all Addins other than QC4 Addin in startup and enable the disabled items on Application close. 
        /// </summary>
        /// <param name="isStart">bool value that represents application starts or ends</param>
        public static void AddInsOnOrOff(bool isStart)
        {
            try
            {
                string appDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Macromill\QuickCross\Addin_ON-OFF.xlsm");
                if (File.Exists(appDirectory))
                {
                    var xlApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbooks xlBooks;

                    xlBooks = xlApp.Workbooks;
                    xlBooks.Open(appDirectory);
                    xlApp.Visible = false;
                    Thread mboxKiller = new Thread(BringWindowToTop);
                    mboxKiller.Start("Microsoft Excel");
                    if (isStart)
                    {
                        // Macro execution (turn off other COM add-ins): Processing when QC starts
                        xlApp.Run("RunMacro_CallFrom_QC4App", "OFF");
                    }
                    else
                    {
                        // Macro execution (turn on other COM add-ins): Processing at the end of QC
                        xlApp.Run("RunMacro_CallFrom_QC4App", "ON");
                    }

                    mboxKiller.Abort();
                    // quit excel
                    xlApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                }
            }
            catch(Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        public void OpenFileDI(string filePath, ref Excel.Application application, ref Excel.Workbook workbook, ref String targetPath, ref ExcelOperate excelOperate, string tempFolder = Constants.PathName.FileOpenTemp)
        {
            try
            {
                OnWorkerComplete(1, LocalResource.LOADING_FILE);
                OnWorkerComplete(2, LocalResource.FILE_OPEN_EXTRACTING);
                targetPath = QcFileHelper.ExtractFile(filePath, tempFolder);

                if (null == targetPath)
                {
                    OnWorkerComplete(100, LocalResource.FILE_OPEN_FAILED);
                    MessageDialog.ErrorOk(LocalResource.FILE_CORRUPTED);
                    return;
                }
                OnWorkerComplete(20, LocalResource.FILE_OPEN_CREATE_EXCEL_APP);
                excelOperate = new ExcelOperate();
                application = excelOperate.Excel;
                OnWorkerComplete(22, "");
                application.DisplayAlerts = false;
                OnWorkerComplete(24, LocalResource.FILE_OPEN_EXCEL);
                workbook = application.Workbooks.Open(targetPath + "\\" + Constant.TemplateFile.QC4_Template, 0, false, 5, Constant.Password, "", true,
                       Excel.XlPlatform.xlWindows, "\t", false, false, 0, false, 1, 0);
                if (CheckSampleId(workbook))
                {
                    IsSuccess = false;
                    OnWorkerComplete(100, LocalResource.FILE_OPEN_FAILED);
                    MessageDialog.ErrorOk(LocalResource.QC3PARSE_ALERT_CONTAIN_INVALID_VARIABLE);
                    return;
                }
                Process currentProcess = Process.GetCurrentProcess();

                QC4Common.DB.DBHelper.SetConnectionString(workbook, targetPath, System.IO.Path.GetFileName(filePath), filePath, currentProcess.Id.ToString());
                OnWorkerComplete(75, LocalResource.FILE_OPEN_EXCEL_UNPROTECT);
                workbook.Unprotect(Constant.Password);
                workbook.Saved = true;
                OnWorkerComplete(99, LocalResource.FILE_OPEN_COMPLETE);
                OnWorkerComplete(100, LocalResource.TEXT_COMPLETED);
            }
            catch (Exception ex)
            {
                OnWorkerComplete(100, LocalResource.FILE_OPEN_FAILED);
                MessageDialog.ErrorOk("Error : " + ex.Message);
            }
        }

        private bool CheckSampleId(Workbook workbook)
        {
            Excel.Worksheet worksheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.QuestionSetting);
            Excel.Range start = worksheet.Cells[QC4Common.Common.Constants.QS.QsRowDataStart, QC4Common.Common.Constants.QS.QsColItem];
            Excel.Range end = ExcelUtil.EndxlUp(start);
            start = worksheet.Range[QC4Common.Common.Constants.QS.QsFirstCell];
            end = worksheet.Range[QC4Common.Common.Constants.QS.QsLastColumn + end.Row];
            Range total = worksheet.get_Range(start, end);
            Object[,] objAry = total.Value2;
            int max = objAry.GetLength(0);
            for (int i = 1; i <= 1; i++)
            {
                string qs = objAry[i, Constants.QS.QsColItem].ToString();
                if (qs != "SAMPLEID")
                    return true;
            }
            return false;
        }
    }

}
