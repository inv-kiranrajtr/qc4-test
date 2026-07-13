using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExcelAddIn.Sheets;
using log4net;
using Macromill.QCWeb.Batch.Report;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.DataProcess;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Logic.TabulationEx.Criteria;
using Macromill.QCWeb.Model;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using QC4Common.Util;
using Qc4Launcher.DB;
using Qc4Launcher.Logic.DPCheckCross;
using Qc4Launcher.Util;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Question.Questions;
using static Qc4Launcher.Logic.CrossSettingsReader;
using static Qc4Launcher.Logic.DPCheckList.DPCheckListTabulationQc;
using Constants = Qc4Launcher.Util.Constants;
using ExcelUtil = QC4Common.Util.ExcelUtil;

namespace Qc4Launcher.Logic
{

    public class CheckCrossQC
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static string checkcrossFile = "CheckCross.cross";
        public delegate void OnWorkerMethodCompleteDelegate(double value, string status, bool isForceStop = false, bool retainThread = false, bool displayMessage = false, bool close = false, bool disableCancel = false,bool isProcessCancel=false);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        DPCheckList.DPCheckListTabulationQc.OnWorkerMethodCompleteDelegate OnWorkerCompleteDp;
        internal IntPtr childExcelApp = IntPtr.Zero;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern System.IntPtr FindWindow(string lpClassName, string lpWindowName);
        private List<int> checkCrsLnLst = new List<int>();

        public void updateProgress(double currentProgress, string v)
        {
            if (OnWorkerCompleteDp != null)
            {
                OnWorkerCompleteDp(currentProgress, v);
            }
            else
            {
                OnWorkerComplete(currentProgress, v);

            }
        }

        public CheckCrossQC()
        {
        }
        BackgroundWorker bgworker;
        internal void ExecuteCheckCross(Workbook excelWorkbook,object worker,out double ProgressBarMovement, Application excelApp = null, DPCheckList.DPCheckListTabulationQc.OnWorkerMethodCompleteDelegate OnWorkerComplete = null, int allocatedPer = 100,bool checklist=true)
        {
            ProgressBarMovement = 0;
           bgworker = worker as BackgroundWorker;
            //excelWorkbook.Application.Cursor = XlMousePointer.xlWait;
           
                OnWorkerCompleteDp = OnWorkerComplete;
           
            Worksheet CrossSheet = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.CrossTabulation);
            Worksheet DataProcess = ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.DataProcess);
            Tabulate(excelWorkbook, CrossSheet, DataProcess, excelApp, OnWorkerComplete, allocatedPer);
            ProgressBarMovement = this.ProgressBarMovement;
            excelWorkbook.Application.Cursor = XlMousePointer.xlDefault;
            if (bgworker.CancellationPending) {
                if (checklist)
                {
                    //OnWorkerCompleteDp(currentProgress, LocalResource.PB_CROSS_TAB_COMPLETED, true, true, true, false, false, true);
                }
                if (!checklist)
                {
                    this.OnWorkerComplete(ProgressBarMovement, LocalResource.PB_CROSS_TAB_COMPLETED, true, true, true, false, false, true);
                    this.OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED, true);
                }
               
                return;
            };

        }
        double currentProgress = 0;
        double ProgressBarMovement = 0;
        internal void Tabulate(Workbook workBook, Worksheet workSheet, Worksheet dataProcesSheet, Application excelApp = null, DPCheckList.DPCheckListTabulationQc.OnWorkerMethodCompleteDelegate OnWorkerComplete = null, int allocatedPer = 100, IntPtr pb = default(IntPtr))
        {
            Definiotion.VariableDictionary = DictionaryUtil.PopulateQSDictionary(workBook);
            Application xlApp = null;
            ExcelOperate excelOperate = null;
            bool hasDiv = false;
             currentProgress= 100 - allocatedPer;
            ProgressBarMovement = currentProgress;
            List<List<string>> tsvPathsDiv = new List<List<string>>();
            Dictionary<string, int> excludeCnt = new Dictionary<string, int>();
            OnWorkerCompleteDp = OnWorkerComplete;
            List<CossTableInput> crTableSets = null;
            try
            {
                if (bgworker.CancellationPending) return;
                childExcelApp = IntPtr.Zero;
                _log.Info("*************************:Cross Tabulate started:*************************");
                _log.Info("Reading settings");
                OpenXmlHelper.RemoveOutPutFiles("CheckCrossOutput");
                string chkCrsfile = Path.Combine(Path.GetTempPath(), "QC4", checkcrossFile);
                crTableSets = CheckCrossReader.readSettings(chkCrsfile, workBook);
                if (crTableSets.Count == 0) { return; }
                string tableName = "data_after_process";
                if (!DBHelper.checkAfterProcess(workBook))
                {
                    return;
                }

                string lccd = "JA";
                //string sigLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QC4", "sig.log");
                if (excelApp == null)
                {
                    excelOperate = new ExcelOperate();
                    xlApp = excelOperate.Excel;
                }
                else
                {
                    xlApp = excelApp;
                }
                string sigLog = OutputUtil.GetSigLogPath(xlApp.PathSeparator);
                Questions questions = DictUpdate.GetQuestions(workBook);
                CrossOptions crossOptions = new CrossOptions();
                crossOptions.Tabletype = Macromill.QCWeb.ReportRequest.TableType.NPer;
                _log.Info("Reading settings completed");
                _log.Info("Cross tab calculation started");
                if (!crossOptions.IsWeightListValid)
                {
                    return;
                }
                if (bgworker.CancellationPending) return;
                TabulationDescriptions tabulationDescriptions = new TabulationDescriptions(Qc4Launcher.Util.CommonFunction.SetDescriptionString());
                string companyName = LocalResource.REPORT_SIGNATURE_KEYWORD;
                string surveyTitle = questions.SurveyTitle;
                Macromill.QCWeb.ReportRequest.ZeroNAIVShowCode zeroshowcd = (Macromill.QCWeb.ReportRequest.ZeroNAIVShowCode)0;
                bool mergeaxis = true;
                string tsvPathJoined = String.Empty;
                Random random = new Random();
                int id = random.Next(1000);
                id = 1;
                QCWebException ex;
                int groupingSectorCount = 1;
                List<string> divNameLis = new List<string>();
                if (bgworker.CancellationPending) return;
                divNameLis.Add("1");
                Macromill.QCWeb.ReportRequest.Request req = new Macromill.QCWeb.ReportRequest.Request(id, divNameLis, groupingSectorCount, surveyTitle, companyName, zeroshowcd,
                    mergeaxis, crossOptions.Reportprefix, crossOptions.Xlbooknameprefix, crossOptions.Tabletype, crossOptions.Tableorientation,
                    crossOptions.Pagesetuptabletype, crossOptions.Minsamplescountonmarking, crossOptions.Markingtype, crossOptions.Significancetestlevel,
                    (Macromill.QCWeb.Common.XlPaperSize)crossOptions.Papersize, (Macromill.QCWeb.Common.XlPageOrientation)crossOptions.Paperorientation,
                    crossOptions.Tablesononesheet, crossOptions.Level2highcolorindex, crossOptions.Level1highcolorindex,
                    crossOptions.Level1lowcolorindex, crossOptions.Level2lowcolorindex, crossOptions.Level1percent, crossOptions.Level2percent,
                    crossOptions.ShowNACode1, crossOptions.ShowIVCode1, crossOptions.WBOn1, crossOptions.FilteringExpression1, crossOptions.PreWbBase, false);
                if (bgworker.CancellationPending) return;
                bool[] filterringFlag = null;
                Macromill.QCWeb.ReportRequest.Reportsets.Reportset reportset = null;
                Dictionary<string, DataWithMarking[,]> crossArrayMap = new Dictionary<string, DataWithMarking[,]>();
                if (bgworker.CancellationPending) return;
                Translation translation = new Translation()
                {
                    REPORT_SA_DESCRIPTION_KEYWORD = LocalResource.REPORT_SA_DESCRIPTION_KEYWORD,
                    REPORT_MA_DESCRIPTION_KEYWORD = LocalResource.REPORT_MA_DESCRIPTION_KEYWORD,
                    REPORT_GT_N_COLUMN_CAPTION = LocalResource.REPORT_GT_N_COLUMN_CAPTION,
                    REPORT_COUNT_AVERAGE_DENOMINATOR_KEYWORD = LocalResource.REPORT_COUNT_AVERAGE_DENOMINATOR_KEYWORD,
                    REPORT_COUNT_AVERAGE_KEYWORD = LocalResource.REPORT_COUNT_AVERAGE_KEYWORD,
                    REPORT_WEIGHT_AVERAGE_DENOMINATOR_KEYWORD = LocalResource.REPORT_WEIGHT_AVERAGE_DENOMINATOR_KEYWORD,
                    REPORT_WEIGHT_AVERAGE_KEYWORD = LocalResource.REPORT_WEIGHT_AVERAGE_KEYWORD,
                    REPORT_MATRIX_DESCRIPTION_KEYWORD = LocalResource.REPORT_MATRIX_DESCRIPTION_KEYWORD,
                    REPORT_COMPLEX_DESCRIPTION_KEYWORD = LocalResource.REPORT_COMPLEX_DESCRIPTION_KEYWORD,
                    REPORT_N_MATRIX_DESCRIPTION_KEYWORD = LocalResource.REPORT_N_MATRIX_DESCRIPTION_KEYWORD,
                    REPORT_N_DESCRIPTION_KEYWORD = LocalResource.REPORT_N_DESCRIPTION_KEYWORD
                };
                int crTableSetItemCnt = 1;
                if (reportset == null)
                {
                    reportset = (Macromill.QCWeb.ReportRequest.Reportsets.Reportset)req.Reportsets[Convert.ToString(1)];
                }
                if (bgworker.CancellationPending) return;
                //DataWithMarking[][,] tabulationArray = new DataWithMarking[crTableSetItems.Count][,];
                Question qstn = null;
                int crTableSetCnt = 1;
                double porgressStep = (allocatedPer * .3) / (crTableSets.Count == 0 ? 1 : crTableSets.Count);
                foreach (CossTableInput crTableSet in crTableSets)
                {
                    
                    updateProgress(currentProgress, LocalResource.PB_CHECKCROSS_CALC);
                    ProgressBarMovement = currentProgress;
                    if (bgworker.CancellationPending) return;
                    currentProgress += porgressStep;
                    DataWithMarking[,] result = null;
                    qstn = null;
                    Question axis1 = null;
                    Question axis2 = null;
                    string targetVaraible = crTableSet.target;
                    string axis1Varaible = crTableSet.axis1;
                    string axis2Varaible = crTableSet.axis2;
                    bool hasCount = false;
                    List<Data> dataList = null;
                    QuestionType targetQtype = 0;
                    using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                    {
                        con.Open();
                        QuestionSettings qstnDet = Definiotion.VariableDictionary[targetVaraible];
                        qstn = (Question)questions[qstnDet.Id];
                        bool TestFlag = crossOptions.TestFlag1;
                        if ((qstn.QuestionType & QuestionType.N) == QuestionType.N && crossOptions.TestCode == Macromill.QCWeb.ReportRequest.SignificanceTestCode.Off)
                        {
                            TestFlag = false;
                        }
                        //if (qstn == null) {
                        //    throw 
                        //}

                        string[] weightArray = null;
                        if (qstnDet.Count.Length != 0)
                        {
                            bool lower = false;
                            hasCount = true;
                            crTableSet.HasCount = true;
                            if (qstnDet.CountBase.Length != 0)
                            {
                                lower = true;
                            }
                            int[] criteriaSectorList;
                            weightArray = new string[qstnDet.CategoryCount];
                            GlobalTabulation.CriteriaValueDescriptionToValueList<int>(QuestionType.MA, qstnDet.Count, out criteriaSectorList, qstnDet.CategoryCount);
                            for (int ct = 1; ct <= qstnDet.CategoryCount; ct++)
                            {
                                if (bgworker.CancellationPending) return;
                                if (criteriaSectorList != null && criteriaSectorList.Contains(ct))
                                {
                                    weightArray[ct - 1] = "1";
                                }
                                else
                                {
                                    if (!lower)
                                    {
                                        weightArray[ct - 1] = "0";
                                    }
                                }

                            }
                        }
                        else if (qstnDet.Score.Length != 0)
                        {
                            weightArray = qstnDet.Score.Split(new char[] { ',' });
                            crTableSet.HasWeight = false;
                            if (weightArray.Length == 0) { weightArray = null; }
                            for (int cnt = 0; cnt < weightArray.Length; cnt++)
                            {
                                if (bgworker.CancellationPending) return;
                                if (weightArray[cnt] != null && weightArray[cnt].Length != 0)
                                {
                                    crTableSet.HasWeight = true; break;
                                }
                            }
                        }

                        System.Data.DataTable dataTble = null;
                        if (dataList == null)
                        {
                            targetQtype = qstn.QuestionType;
                            if (string.IsNullOrEmpty(crTableSet.filePathTarget))
                            {
                                dataTble = DBHelper.GetDataTable("Select " + qstn.ColumnName + " from " + tableName + " order by sort_no ", con);
                                dataList = ReadTextFile.ReadDataTable(dataTble, qstn.QuestionType, null, out ex);
                            }
                            else
                            {
                                dataList = ReadTextFile.ReadData(crTableSet.filePathTarget, qstn.QuestionType, null, out ex);
                                if (null != ex)
                                {
                                    dataList = new List<Data>();
                                }
                            }
                        }
                        qstnDet = Definiotion.VariableDictionary[axis1Varaible];
                        axis1 = (Question)questions[qstnDet.Id];
                        if (null != axis2Varaible)
                        {
                            qstnDet = Definiotion.VariableDictionary[axis2Varaible];
                            axis2 = (Question)questions[qstnDet.Id];
                        }

                        List<List<Data>> axesDatga = new List<List<Data>>();
                        List<Data> dataList2 = new List<Data>();
                        if (string.IsNullOrEmpty(crTableSet.filePathAxis1))
                        {
                            dataTble = DBHelper.GetDataTable("Select " + axis1.ColumnName + " from " + tableName + " order by sort_no ", con);
                            dataList2 = ReadTextFile.ReadDataTable(dataTble, axis1.QuestionType, null, out ex);
                        }
                        else
                        {
                            dataList2 = ReadTextFile.ReadData(crTableSet.filePathAxis1, axis1.QuestionType, null, out ex);
                            if (null != ex)
                            {
                                dataList2 = new List<Data>();
                            }
                        }
                        axesDatga.Add(dataList2);
                        if (null != axis2)
                        {
                            // not using
                            dataTble = DBHelper.GetDataTable("Select " + axis2.ColumnName + " from " + tableName + " order by sort_no ", con);
                            List<Data> dataList3 = ReadTextFile.ReadDataTable(dataTble, axis2.QuestionType, null, out ex);
                            axesDatga.Add(dataList3);
                        }

                        QCWebException ex2 = CrossTabulation.GetCrossArray(
                            targetQtype,
                            CrossTabulationQC.GetCategoryArray(qstn),
                            dataList,
                            CrossTabulationQC.GetAxisQTypeList(axis1, axis2),
                            CrossTabulationQC.GetAxisQuestionTitle(axis1, axis2),
                            CrossTabulationQC.GetAxisCategoryList(axis1, axis2),
                            axesDatga,
                            out result, translation,
                            crossOptions.TabulationDescriptions,
                            filterringFlag,
                            crossOptions.WBDataList,
                            weightArray,
                            crossOptions.Level1percent,
                            crossOptions.Level2percent,
                            true, //crossOptions.ShowNoAnswerAxis || crossOptions.TabulateFullQuantity1,
                            false,
                            GlobalTabulation.MarkingTotal.Subtotal,
                            crossOptions.TabulateFullQuantity1, IVtoNA: crossOptions.TabulateFullQuantity1, locale: lccd,
                            CutNA: false, //CutNA: !crossOptions.ShowNoAnswerItem, // need to cehck
                            SignificanceTestOn: TestFlag,
                            significanceTestCode: crossOptions.TestCode, significanceTestLevel: crossOptions.TestLevels.ToArray(),
                            SignificanceTestLogFilePath: sigLog, qName: qstn.Name, axisQName: CrossTabulationQC.GetAxisQuestionName(axis1, axis2), hasCount: hasCount, qTypeOr: qstn.QuestionType);
                        crossArrayMap.Add(crTableSetItemCnt + ":" + crTableSetCnt, result);
                        checkCrsLnLst.Add(crTableSet.lineNo);
#if DEBUG
                        //CrossTabulationQC.logOutput(result);
#endif
                    }
                    crTableSetCnt++;
                }
                if (reportset == null)
                {
                    return;
                }
                List<string> tsvPaths = new List<string>();
                if (bgworker.CancellationPending) return;
                Macromill.QCWeb.ReportRequest.Outputs.OutputCross cross = (Macromill.QCWeb.ReportRequest.Outputs.OutputCross)reportset.Outputs[0];
                crTableSetItemCnt = 1;

                List<DataWithMarking[,]> tabulationArray = new List<DataWithMarking[,]>();
                List<CossTableInput> crTableSetItemsNarrow = new List<CossTableInput>();
                qstn = null;
                crTableSetCnt = 1;
                bool hasWeight = false;
                foreach (CossTableInput crTableSet in crTableSets)
                {
                    if (bgworker.CancellationPending) return;
                    DataWithMarking[,] result = null;
                    qstn = null;
                    Question axis1 = null;
                    Question axis2 = null;
                    string targetVaraible = crTableSet.target;
                    string axis1Varaible = crTableSet.axis1;
                    string axis2Varaible = crTableSet.axis2;

                    QuestionSettings qstnDet = Definiotion.VariableDictionary[targetVaraible];
                    bool hasCount = crTableSet.HasCount;
                    hasWeight = crTableSet.HasWeight;
                    qstn = (Question)questions[qstnDet.Id];

                    qstnDet = Definiotion.VariableDictionary[axis1Varaible];
                    axis1 = (Question)questions[qstnDet.Id];
                    if (null != axis2Varaible)
                    {
                        qstnDet = Definiotion.VariableDictionary[axis2Varaible];
                        axis2 = (Question)questions[qstnDet.Id];
                    }

                    result = crossArrayMap[crTableSetItemCnt + ":" + crTableSetCnt];
                    tabulationArray.Add(result);
                    crTableSetItemsNarrow.Add(crTableSet);

                    using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                    {
                        con.Open();
                        Macromill.QCWeb.ReportRequest.Tables.CrossTable table = TableUtil.SetOutputRequestTableCross(workBook, cross, qstn, tabulationArray.ToArray(),
                        crTableSetItemsNarrow, questions, con, excludeCnt, crossOptions.TabulateFullQuantity1, tableName, false, crossOptions.ShowNoAnswerAxis, hasCount, hasWeight, true);
                    }
                    crTableSetCnt++;
                    tabulationArray = new List<DataWithMarking[,]>();
                    crTableSetItemsNarrow = new List<CossTableInput>();
                }
                crTableSetItemCnt++;
                tsvPathJoined = (cross.Tables as Macromill.QCWeb.ReportRequest.Tables).OutputToTSV();
                tsvPaths.Add(tsvPathJoined);
                if (tsvPaths.Count > 0)
                {
                    tsvPathsDiv.Add(tsvPaths);
                }

                if (bgworker.CancellationPending) return;
                Request request = new Request();
                request.MakeRequeszt(id, tsvPathsDiv, hasDiv ? divNameLis : null, surveyTitle, companyName, zeroshowcd, mergeaxis, crossOptions.Reportprefix,
                    crossOptions.Xlbooknameprefix, crossOptions.Tabletype, crossOptions.Tableorientation, crossOptions.Pagesetuptabletype, crossOptions.Minsamplescountonmarking,
                    crossOptions.Markingtype, crossOptions.Significancetestlevel, crossOptions.Papersize,
                    crossOptions.Paperorientation, crossOptions.Tablesononesheet, crossOptions.Level2highcolorindex, crossOptions.Level1highcolorindex,
                    crossOptions.Level1lowcolorindex, crossOptions.Level2lowcolorindex, crossOptions.Level1percent, crossOptions.Level2percent,
                    crossOptions.ShowNACode1, crossOptions.ShowIVCode1, crossOptions.WBOn1, crossOptions.LocalizedFilteringExpression1, crossOptions.PreWbBase, false);
                _log.Info("Cross tab calculation completed");
                _log.Info("Cross tab excel creation started");
                if (bgworker.CancellationPending) return;
#if DEBUG
                //xlApp.Visible = true;
                //xlApp.ScreenUpdating = true;
                //xlApp.Calculation = XlCalculation.xlCalculationManual;
#endif
                xlApp.EnableEvents = false;
                //xlApp.DisplayStatusBar = false;
                xlApp.PrintCommunication = false;
                xlApp.DisplayAlerts = false;
                xlApp.Visible = false;
                currentProgress = (100 - allocatedPer) + (allocatedPer * .3);
                ProgressBarMovement = currentProgress;
                double porgressStepRpt = (allocatedPer * .7) / (request.Reportsets.Count == 0 ? 1 : request.Reportsets.Count);
                foreach (Reportset rs in request.Reportsets.Values)
                {
                    if (bgworker.CancellationPending) return;
                    double porgressStepOutput = porgressStepRpt / (rs.Outputs.Count == 0 ? 1 : rs.Outputs.Count);
                    foreach (Output ouput in rs.Outputs.Values)
                    {
                       
                        updateProgress(currentProgress, LocalResource.PB_EXCEL_GEN);
                        ProgressBarMovement = currentProgress;
                        if (bgworker.CancellationPending) return;

                        if (excelApp != null)
                        {
                            CrossCreator crossCreator = new CrossCreator();
                            crossCreator.CreateCross(ouput, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1), crossOptions.GroupFolderPath,
                                System.AppContext.BaseDirectory, xlApp, bgworker, null,out ProgressBarMovement, checkCrossP: true, checkListP: excelApp != null,
                                currentProgress: currentProgress, progressAvailable: (allocatedPer * .7), CQC: this,
                                checkCrsLnLstP: checkCrsLnLst);
                            if (bgworker.CancellationPending) return;

                        }
                        else
                        {
                            CheckCrossCreatorXML checkCrossCreatorXML = new CheckCrossCreatorXML();
                            checkCrossCreatorXML.CreateCheckCross(ouput, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1), crossOptions.GroupFolderPath,
                                System.AppContext.BaseDirectory, xlApp,bgworker, null,out ProgressBarMovement, checkCrossP: true, checkListP: excelApp != null,
                                currentProgress: currentProgress, progressAvailable: (allocatedPer * .7), CQC: this,
                                checkCrsLnLstP: checkCrsLnLst);
                           
                            if (bgworker.CancellationPending)
                                return;

                        }
                        currentProgress =currentProgress+ porgressStepOutput;
                        ProgressBarMovement = currentProgress;
                        if (bgworker.CancellationPending) return;
                    }

                    updateProgress(currentProgress, LocalResource.PB_EXCEL_GEN);
                    if (bgworker.CancellationPending) return;
                }
                if (bgworker.CancellationPending) return;
                xlApp.Visible = false;
                xlApp.EnableEvents = false;
                if (excelApp == null)
                {
                   
                    foreach (Worksheet sh in xlApp.ActiveWorkbook.Worksheets)
                    {
                        sh.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                        if (sh.Name == "Check Cross")
                        {
                            sh.Name = LocalResource.REPORT_CHECK_CROSS_SHEET_NAME;
                        }
                    }
                   
                    this.OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED, true, true, true);
                    this.OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED, true);
                    //  MessageDialog.ShowMessageOnWorkBook(LocalResource.DATAPROCESS_COMPLETED, Enums.MessageType.Info, workBook,pb);
                    xlApp.EnableEvents = true;
                    xlApp.DisplayStatusBar = true;
                    xlApp.Calculation = XlCalculation.xlCalculationAutomatic;
                    xlApp.ScreenUpdating = true;
                    xlApp.DisplayAlerts = true;
                   // xlApp.Visible = true;

                    childExcelApp = (IntPtr)xlApp.Hwnd;
                    SetForegroundWindow(childExcelApp);
                    xlApp.Visible = true;
                    xlApp.WindowState = XlWindowState.xlMaximized;
                   

                }
                _log.Info("Cross tab excel creation completed");

            }
            catch (Exception ex)
            {
                try
                {
                    _log.Error(ex.Message);
                    _log.Error(ex.StackTrace);
                    if (!ex.Message.Contains("OutOfMemoryException"))
                    {
                        _log.LogError(ex.Message);
                    }
                }
                finally
                {
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.FAILED_TO_GENE_EXCEL, Enums.MessageType.ErrorOk, workBook,pb);
                }
            }
            finally
            {
                try
                {
                    foreach (CossTableInput crTableSet in crTableSets)
                    {
                        System.IO.File.Delete(crTableSet.filePathTarget);
                        System.IO.File.Delete(crTableSet.filePathAxis1);
                    }
                }
                catch { }
                if (excelOperate != null)
                {
                    COMWholeOperate.releaseComObject(ref excelOperate);
                }
                if (excelApp == null)
                {
                    if (xlApp != null)
                    {
                        //xlApp.Quit();
                        if (bgworker.CancellationPending)
                        {
                            xlApp.Quit();
                        }
                        COMWholeOperate.releaseComObject<Application>(ref xlApp);
                    }
                    if (!bgworker.CancellationPending)
                    {
                        updateProgress(100, LocalResource.PB_EXCEL_GEN);
                    }
                }
               

              
                
                GC.Collect();
                GC.WaitForPendingFinalizers();
                CrossTabulationQC.deleteFiles();
                _log.Info("Cross Tabulate completed");
                _log.Info("CheckCross End");
            }
        }
    }
}