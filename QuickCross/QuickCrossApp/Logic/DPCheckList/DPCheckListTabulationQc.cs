using Macromill.QCWeb.Batch.Report;
using Macromill.QCWeb.Logic;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Tabulation;
using log4net;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Exceptions;
using Microsoft.Office.Interop.Excel;
using Qc4Launcher.DB;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Question.Questions;
using static Qc4Launcher.Util.Constants;
using DataTable = System.Data.DataTable;
using QuestionType = Macromill.QCWeb.Tabulation.QuestionType;
using ReportMessageIndex = Macromill.QCWeb.Common.Constants.ReportMessageIndex;
using System.Linq;
using Macromill.QCWeb.COMOperate;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Qc4Launcher.Logic.DPCheckList
{

    internal class DPCheckListTabulationQc
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void OnWorkerMethodCompleteDelegate(double value, string status, bool isForceStop = false, bool retainThread = false, bool displayCompletion = false, bool close = false, bool disableCancel = false, bool isprocessCancel = false);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;

        internal IntPtr childExcelApp = IntPtr.Zero;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern System.IntPtr FindWindow(string lpClassName, string lpWindowName);
        internal void Tabulate(Workbook workBook, object worker, bool checkCross, IntPtr pb = default(IntPtr))
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            BackgroundWorker bgWorker = worker as BackgroundWorker;
            Application xlApp = null;
            try
            {
                childExcelApp = IntPtr.Zero;

                int allocatedPer = 100;
                if (checkCross) allocatedPer = 50;

                double currentProgress = 5 * allocatedPer / 100;
                if (bgWorker.CancellationPending)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PB_GENE_CHECK_COMPLETE, true, true, true, false, false, true);
                    OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE, true);
                    return;
                }
                OnWorkerComplete(currentProgress, LocalResource.PB_CHECK_LIST_INIT);
                workBook.Application.Cursor = XlMousePointer.xlWait;

                if (!DBHelper.checkAfterProcess(workBook))
                {
                    _log.Info("Data process not found");
                    return;
                }

                _log.Info("Check list processing started");
                string lccd = "JA";
                string companyName = LocalResource.REPORT_SIGNATURE_KEYWORD;
                Questions questions = DictUpdate.GetQuestions(workBook);
                string surveyTitle = questions.SurveyTitle;
                Macromill.QCWeb.ReportRequest.ZeroNAIVShowCode zeroshowcd = (Macromill.QCWeb.ReportRequest.ZeroNAIVShowCode)3;
                bool mergeaxis = true;
                CheckListOptions checkListOptions = new CheckListOptions();

                currentProgress = 7 * allocatedPer / 100;
                OnWorkerComplete(currentProgress, LocalResource.PB_CHECK_LIST_INIT);

                if (bgWorker.CancellationPending)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PB_GENE_CHECK_COMPLETE, true, true, true, false, false, true);
                    OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE, true);
                    return;
                }
                int id = 1;
                Macromill.QCWeb.ReportRequest.Request req = new Macromill.QCWeb.ReportRequest.Request(id, surveyTitle, companyName, zeroshowcd,
                mergeaxis, checkListOptions.Reportprefix, checkListOptions.Xlbooknameprefix, 0 // set totalcount
                );
                Macromill.QCWeb.ReportRequest.Reportsets.Reportset reportset = (Macromill.QCWeb.ReportRequest.Reportsets.Reportset)req.Reportsets[Convert.ToString(id)];
                Macromill.QCWeb.ReportRequest.Outputs.Output other = (Macromill.QCWeb.ReportRequest.Outputs.Output)reportset.Outputs[0];

                //string[] sourceDivArray = new string[] { CDef.SourceDiv.Original.Code, CDef.SourceDiv.DataEdit.Code };
                //questions = new Questions(1, sourceDivArray);
                Macromill.QCWeb.ReportRequest.Outputs.OutputCheckList outputchecklist = other as Macromill.QCWeb.ReportRequest.Outputs.OutputCheckList;
                other.ExcelBookNamePrefix = LocalResource.REPORT_CHECK_LIST_BOOK_NAME_PREFIX;

                // サンプル数の取得
                List<string> tsvPaths = new List<string>();
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                {
                    con.Open();
                    //outputchecklist.TotalCount = tabulationLogic.GetSampleCount(id);

                    Question qstn = (Question)questions[0]; // need to change after question settings updations
                    string columnName = DBSettings.SampleIdColumnName;
                    QCWebException ex1;
                    DataTable dataTble = DBHelper.GetDataTable("Select " + columnName + " from data_after_process order by sort_no", con);
                    List<Data> dataList = ReadTextFile.ReadDataTable(dataTble, qstn.QuestionType, null, out ex1);
                    outputchecklist.TotalCount = dataList.Count;
                    if (ex1 != null)
                        throw ex1;

                    //データ加工の再実行が必要か判定する

                    List<int> KeyOrders = new List<int>();

                    foreach (string key in questions.Keys)
                        KeyOrders.Add(Convert.ToInt32(((Question)questions[Convert.ToDecimal(key)]).questionOrder));
                    KeyOrders.Sort();

                    List<int> sortedKeys = new List<int>();
                    foreach (int keyOrder in KeyOrders)
                    {
                        foreach (string key in questions.Keys)
                        {
                            Question question = ((Question)questions[Convert.ToDecimal(key)]);
                            if (keyOrder == question.questionOrder)
                                sortedKeys.Add(Convert.ToInt32(question.ID));
                        }
                    }

                    currentProgress = 10 * allocatedPer / 100;
                    OnWorkerComplete(currentProgress, LocalResource.PB_CHECK_LIST_INIT);
                    if (bgWorker.CancellationPending)
                    {
                        OnWorkerComplete(currentProgress, LocalResource.PB_GENE_CHECK_COMPLETE, true, true, true, false, false, true);
                        OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE, true);
                        return;
                    }
                    #region Progress Bar Implementation
                    double childProgress = 0; double UpdProgress = 0;
                    int allocatedProgress = 35 * allocatedPer / 100;
                    int pos = 1;
                    #endregion

                    foreach (int sortKey in sortedKeys)
                    {

                        #region Progress Bar Implementation
                        double progressChildPerc = (double)(pos) / sortedKeys.Count * 100;
                        childProgress = allocatedProgress * progressChildPerc / 100;
                        UpdProgress = currentProgress + childProgress;
                        if (bgWorker.CancellationPending)
                        {
                            OnWorkerComplete(Convert.ToInt32(UpdProgress), LocalResource.PB_GENE_CHECK_COMPLETE, true, true, true, false, false, true);
                            OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE, true);
                            return;
                        }
                        OnWorkerComplete(Convert.ToInt32(UpdProgress), String.Format(LocalResource.PB_CHECK_LIST_PROCESSING_QSTNS, (pos), sortedKeys.Count));
                        pos++;
                        #endregion

                        string key = Convert.ToString(sortKey);

                        Question question = (Question)(questions[Convert.ToDecimal(key)]);

                        if (question.Name == GlobalsConstant.SAMPLE_ID) continue;
                        if (question.QCAnswerType == QCAnswerType.D) continue;
                        if (question.IsTemporatyItem) continue;
                        if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                        {
                            Questions childQuestions = (Questions)question.ChildQuestions;
                            for (int j = 1; j <= childQuestions.Count; ++j)
                            {
                                Questions.Question childQuestion = (Questions.Question)childQuestions[j];
                                if (childQuestion.QCAnswerType == QCAnswerType.FA) continue;
                                DPCheckListHelper.SetCheckList(childQuestion, ref other, con);
                            }
                        }
                        else
                        {
                            if (question.QCAnswerType == QCAnswerType.FA) continue;
                            DPCheckListHelper.SetCheckList(question, ref other, con);
                        }


                    }
                    string tsvPathJoined = (other.Tables as Macromill.QCWeb.ReportRequest.Tables).OutputToTSV();
                    tsvPaths.Add(tsvPathJoined);

                    #region Progress Bar Implementation
                    currentProgress += allocatedProgress;
                    #endregion
                }

                currentProgress = 50 * allocatedPer / 100;
                OnWorkerComplete(currentProgress, LocalResource.PB_GENE_CHECK_LIST);
                if (bgWorker.CancellationPending)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PB_GENE_CHECK_COMPLETE, true, true, true, false, false, true);
                    OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE, true);
                    return;
                }
                Request request = new Request();
                request.MakeDPCheckListRequest(id, tsvPaths, surveyTitle, companyName, zeroshowcd, mergeaxis, checkListOptions.Reportprefix
                    , checkListOptions.Xlbooknameprefix, outputchecklist.TotalCount);

                xlApp = new Application();
                //#if DEBUG
                //                xlApp.Visible = true;
                //                xlApp.ScreenUpdating = true;
                //#else
                xlApp.Visible = false;
                xlApp.ScreenUpdating = false;
                //#endif
                xlApp.EnableEvents = false;
                xlApp.DisplayStatusBar = false;
                xlApp.PrintCommunication = false;
                xlApp.DisplayAlerts = false;

                //string[] tsvPathArr = tsvPaths[0].Split(';');

                int pbRSetAllocated = 48 * allocatedPer / 100;
                double pbREachAllocated = (double)pbRSetAllocated / request.Reportsets.Values.Count;

                foreach (Reportset rs in request.Reportsets.Values)
                {
                    double pbOEachAllocated = (double)pbREachAllocated / rs.Outputs.Values.Count;
                    foreach (OutputCheckList outputValue in rs.Outputs.Values)
                    {
                        string[] tsvPathArr = new string[outputValue.Tables.Keys.Count];
                        string selectedPath = null;
                        int i = 0;
                        foreach (string tableKey in outputValue.Tables.Keys)
                        {
                            List<string> selectedList = tsvPaths.Where(field => field.Contains(tableKey)).ToList();
                            if (selectedList != null && selectedList.Count > 0)
                            {
                                selectedPath = selectedList[0];
                                break;
                            }
                            tsvPathArr[i] = tableKey;
                            i++;
                        }
                        if (selectedPath != null)
                            tsvPathArr = selectedPath.Split(';');

                        CheckListCreator checkListCreator = new CheckListCreator();
                        checkListCreator.CreateCheckList(outputValue, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1), AppContext.BaseDirectory, lccd, xlApp, tsvPathArr, OnWorkerComplete, ref currentProgress, pbOEachAllocated,bgWorker);
                        if (bgWorker.CancellationPending)
                        {
                            OnWorkerComplete(currentProgress, LocalResource.PB_GENE_CHECK_COMPLETE, true, true, true, false, false, true);
                            OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE, true);
                            return;
                        }
                    }
                }

                foreach (string tsvPathJoined in tsvPaths)
                {
                    string[] tsvPathArr = tsvPathJoined.Split(';');
                    foreach (string tsvPath in tsvPathArr)
                    {
                        if (File.Exists(tsvPath))
                        {
                            File.Delete(tsvPath);
                        }
                    }
                }

                if (checkCross)// Executing check cross
                {
                    currentProgress = allocatedPer;
                    OnWorkerComplete(currentProgress, LocalResource.PB_GENE_CHECK_COMPLETE);
                    if (bgWorker.CancellationPending)
                    {
                        OnWorkerComplete(currentProgress, LocalResource.PB_GENE_CHECK_COMPLETE, true, true, true, false, false, true);
                        OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE, true);
                        return;
                    }
                    double data = 0;
                    new CheckCrossQC().ExecuteCheckCross(workBook, bgWorker, out data, xlApp, OnWorkerComplete, allocatedPer);
                    if (bgWorker.CancellationPending)
                    {
                        OnWorkerComplete(data, LocalResource.PB_GENE_CHECK_COMPLETE, true, true, true, false, false, true);
                        OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE, true);
                        return;
                    }
                }

                try
                {
                    Worksheet wSheet = xlApp.ActiveSheet;
                    xlApp.Goto(wSheet.Range["A1"]);
                    foreach (Worksheet sht in xlApp.ActiveWorkbook.Worksheets)
                    {
                        sht.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                        if (sht.Name == LocalResource.REPORT_CHECK_LIST_BOOK_NAME_PREFIX)
                        {
                            sht.Range["A4"].Value = LocalResource.REPORT_CHECK_LIST_CHANGED_INDEX;
                            sht.Range["A2"].Value = LocalResource.REPORT_CHECK_LIST_NUM_SAMPLE_BEFORE;
                            sht.Range["A3"].Value = LocalResource.REPORT_CHECK_LIST_NUM_SAMPLE_AFTER;
                            sht.Range["B4"].Value = LocalResource.REPORT_CHECK_LIST_BOOK_NAME_PREFIX;
                        }
                        if (sht.Name == "Check Cross")
                        {
                            sht.Name = LocalResource.REPORT_CHECK_CROSS_SHEET_NAME;
                        }
                    }

                    if (wSheet != null)//Redmine id :193454
                    {
                        try
                        {
                            COMWholeOperate.releaseComObject(ref wSheet);
                        }
                        catch { }
                    }
                }
                catch (Exception exc)
                {
                    _log.Error(exc.Message + "\n" + exc.StackTrace);
                }

                OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE, true, true, true);
                //  MessageDialog.ShowMessageOnWorkBook(LocalResource.DATAPROCESS_COMPLETED, Enums.MessageType.Info, workBook);
                OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE, true); // for stopping progressbar

                xlApp.EnableEvents = true;
                xlApp.DisplayStatusBar = true;
                xlApp.Calculation = XlCalculation.xlCalculationAutomatic;
                xlApp.ScreenUpdating = true;
                xlApp.DisplayAlerts = true;


                childExcelApp = (IntPtr)xlApp.Hwnd;
                SetForegroundWindow(childExcelApp);
                xlApp.WindowState = XlWindowState.xlMaximized;
                xlApp.Visible = true;




            }
            catch (Exception ex)
            {
                OnWorkerComplete(100, LocalResource.PB_GENE_CHECK_COMPLETE);
                _log.Error(ex.Message + "\n" + ex.StackTrace);
                // MessageDialog.ErrorOk(LocalResource.FAILED_TO_GENE_EXCEL);
                MessageDialog.ShowMessageOnWorkBook(LocalResource.FAILED_TO_GENE_EXCEL, Enums.MessageType.ErrorOk, workBook, pb);
            }
            finally
            {
                _log.Info("Check list processing completed");
                workBook.Application.Cursor = XlMousePointer.xlDefault;
                if (xlApp != null)
                {
                    if (bgWorker.CancellationPending)
                    {
                        xlApp.Quit();
                    }
                    //xlApp.Quit();
                    COMWholeOperate.releaseComObject<Application>(ref xlApp);
                }
            }
        }



    } // End Class
}
