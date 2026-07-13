using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
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
using Qc4Launcher.Logic.Cross_Report;
using Qc4Launcher.Util;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Question.Questions;
using static Qc4Launcher.Logic.CrossSettingsReader;
using DBHelperCommon = QC4Common.DB.DBHelper;

namespace Qc4Launcher.Logic
{
    public class CrossTabulationQC
    {
        public delegate void OnWorkerMethodCompleteDelegate(double value, string status, bool isForceStop = false, bool retainThread = false, bool close = false, bool disableCancel = false);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //public static ExcelOperate excelOperate = new ExcelOperate();
        public string tableNameAnswer = "answers";

        internal IntPtr childExcelApp = IntPtr.Zero;
        public void updateProgress(double currentProgress, string v, bool close = false)
        {
            OnWorkerComplete(currentProgress, v, close: close);
        }


        internal void Tabulate(Workbook workBook, Worksheet workSheet, object worker, DoWorkEventArgs bgWorkerArg, bool isChart = false,
            System.Windows.Window window = null, IntPtr pb = default(IntPtr))
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            OpenXmlHelper.RemoveOutPutFiles("CrossReportOutput",true);
            //CrossReportHelper.RemoveOutPutFiles();
            _log.Info("*************************:Cross Tabulate started:*************************");
            double currentProgress = 1;
            OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
            _log.Info("Populating dictionary");
            Definiotion.VariableDictionary = DictionaryUtil.PopulateQSDictionary(workBook);
            DBHelper.CreateMultivariateTempTable(workBook);
            _log.Info("Populating dictionary completed");
            NPOICrossCreator.RemoveOutPutFiles();
            Application xlApp = null;
            Question group = null;
            bool hasDiv = false;
            List<List<string>> tsvPathsDiv = new List<List<string>>();
            Dictionary<string, int> excludeCnt = new Dictionary<string, int>();
            List<string> outputFiles = new List<string>();
            ExcelOperate excelOperate = null;
            BackgroundWorker bgWorker = worker as BackgroundWorker;
            CrossOptions crossOptions = null;
            bool ThreeWay = false;
            try
            {
                childExcelApp = IntPtr.Zero;
                _log.Info("Reading settings");

                string lccd = "JA";
                Questions questions = DictUpdate.GetQuestions(workBook);
                List<List<CrossTableDiv>> crossTableDivList = CrossSettingsReader.ReadCrossSettings(workSheet, ref hasDiv, workBook, Definiotion.VariableDictionary, questions, ref ThreeWay, std: window != null);
                _log.Info("Reading div settings completed");
                //List<List<CrossSettingsReader.CossTableInput>> crTableSets = CrossSettingsReader.ReadCrossSettings(workSheet);
                if (!hasDiv)
                {
                    string errMsg = "";
                    QC4Common.Common.CRValidate.ValidateCRTab(workSheet, Definiotion.VariableDictionary, ref errMsg, false,
                        pro: window == null, report: isChart);
                    _log.Info("Validate settings completed");
                    if (errMsg.Length > 0)
                    {
                        OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(errMsg, Enums.MessageType.ErrorOk, workBook, pb);
                        return;
                    }
                }
                if (crossTableDivList.Count == 0)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.NO_VALID_SETTINGS_FOUND, Enums.MessageType.ErrorOk, workBook, pb);
                    return;
                }
                string tableName = tableNameAnswer;
                if (DBHelper.checkAfterProcess(workBook))
                {
                    tableName = "data_after_process";
                }
                tableNameAnswer = tableName;

                //string sigLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QC4", "sig.log");
                string sigLog = OutputUtil.GetSigLogPath(workBook.Application.PathSeparator);
                crossOptions = CrossSettingsReader.ReadOptions(workBook, isChart, std: window != null);
                if (crossOptions.Tableorientation == Macromill.QCWeb.ReportRequest.TableOrientation.Portrait && ThreeWay)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.CROSS_COLUMN_PERCENTAGE_CANNOT_SET_THREEWAY, Enums.MessageType.ErrorOk, workBook, pb);
                    return;
                }


                _log.Info("Reading settings completed");
                currentProgress = 10;
                //crossOptions.
                OnWorkerComplete(currentProgress, LocalResource.PB_CROSS_CALC);
                _log.Info("Cross tab calculation started");
                TabulationDescriptions tabulationDescriptions = new TabulationDescriptions(crossOptions.TabulationDescriptions);
                string companyName = LocalResource.REPORT_SIGNATURE_KEYWORD;
                string surveyTitle = questions.SurveyTitle;
                Macromill.QCWeb.ReportRequest.ZeroNAIVShowCode zeroshowcd = (Macromill.QCWeb.ReportRequest.ZeroNAIVShowCode)0;
                bool mergeaxis = true;
                string tsvPathJoined = String.Empty;
                Random random = new Random();
                int id = random.Next(1000);
                id = 1;
                QCWebException ex;
                List<Data> groupDataList = null;
                List<Data> groupDataListEmpty = null;
                int groupingSectorCount = 1;

                if (!crossOptions.IsWeightListValid)
                {
                    OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED);
                    return;
                }

                bool isPro = CommonFunction.ActivationKeyChecking();
                bool isMv = false;
                if (crossOptions.GroupVariable != null)
                {
                    if (!System.IO.Directory.Exists(crossOptions.GroupFolderPath))
                    {
                        //string msg = "Directory not exist";
                        OnWorkerComplete(currentProgress, LocalResource.PB_CROSS_CALC, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.DIR_NOT_EXIST, Enums.MessageType.ErrorOk, workBook, pb);
                        return;
                    }
                    if (!checkVariableDivision(crossOptions.GroupVariable, Definiotion.VariableDictionary))
                    {
                        OnWorkerComplete(currentProgress, LocalResource.PB_CROSS_CALC, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.INVALID_CLASSIFICATION_VARIABLE, Enums.MessageType.ErrorOk, workBook, pb);
                        return;
                    }
                    using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                    {
                        con.Open();
                        QuestionSettings qstnDet = Definiotion.VariableDictionary[crossOptions.GroupVariable];
                        try
                        {
                            group = (Question)questions[qstnDet.Id];
                            System.Data.DataTable dataTble = new System.Data.DataTable();
                            groupDataListEmpty = ReadTextFile.ReadDataTable(dataTble, group.QuestionType, null, out ex);
                            if (qstnDet != null && qstnDet.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.An)
                                tableName = DBHelperCommon.getTableName(con, group.ColumnName, out isMv);
                            else
                            {
                                tableName = tableNameAnswer;
                                isMv = false;
                            }
                            if (checkQuestion(qstnDet, workBook))
                            {
                                if (!isMv)
                                {
                                    dataTble = DBHelper.GetDataTable("Select " + group.ColumnName + " from " + tableName + " order by sort_no ", con);
                                }
                                else
                                {
                                    dataTble = DBHelper.GetDataTable("Select " + group.ColumnName + " from " + tableNameAnswer + " a join "
                                        + tableName + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                                }
                            }
                            else
                            {
                                dataTble = DBHelper.GetDataTable("Select NULL As tmpcol from " + tableNameAnswer, con);
                                DPCheckList.DPCheckListHelper.setDeleteFlag(dataTble.Rows.Count);
                            }
                            try
                            {
                                groupDataList = ReadTextFile.ReadDataTable(dataTble, group.QuestionType, null, out ex);
                            }
                            finally
                            {
                                ReadTextFile.DeleteFlag = null;
                            }
                            groupingSectorCount = group.Sectors.Count;
                        }
                        catch (Exception sql)
                        {
                            _log.Warn(sql.Message);
                            group = null;
                        }
                    }
                }

                List<string> divNameLis = new List<string>();
                int rowi = 0;
                int divCnt = 0;
                foreach (List<CrossTableDiv> crossTableDivListRow in crossTableDivList)
                {
                    rowi++;
                    int colj = 0;
                    foreach (CrossTableDiv crossTableDivListCol in crossTableDivListRow)
                    {
                        string col = numberToAlpha(colj);
                        colj++;
                        divCnt++;
                        List<List<CossTableInput>> ct = crossTableDivListCol.CossTableInputs;
                        foreach (List<CossTableInput> crTableSetItems in ct)
                        {
                            if (crTableSetItems.Count > 0)
                            {
                                divNameLis.Add(col + rowi + "_");
                                break;
                            }
                        }
                    }
                }
                if (divCnt > 1 || hasDiv)
                {
                    hasDiv = true;
                    crossOptions.HasDiv = true;
                    string currDateTimeFmt = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string dir = System.IO.Path.Combine(Definiotion.SelectedDir, currDateTimeFmt);
                    GlobalMethodClass.GuaranteeDirectoryExist(dir);
                    crossOptions.GroupFolderPath = dir;
                }

                Macromill.QCWeb.ReportRequest.Request req = new Macromill.QCWeb.ReportRequest.Request(id, divNameLis, groupingSectorCount, surveyTitle, companyName, zeroshowcd,
                    mergeaxis, crossOptions.Reportprefix, crossOptions.Xlbooknameprefix, crossOptions.Tabletype, crossOptions.Tableorientation,
                    crossOptions.Pagesetuptabletype, crossOptions.Minsamplescountonmarking, crossOptions.Markingtype, crossOptions.Significancetestlevel,
                    (Macromill.QCWeb.Common.XlPaperSize)crossOptions.Papersize, (Macromill.QCWeb.Common.XlPageOrientation)crossOptions.Paperorientation,
                    crossOptions.Tablesononesheet, crossOptions.Level2highcolorindex, crossOptions.Level1highcolorindex,
                    crossOptions.Level1lowcolorindex, crossOptions.Level2lowcolorindex, crossOptions.Level1percent, crossOptions.Level2percent,
                    crossOptions.ShowNACode1, crossOptions.ShowIVCode1, crossOptions.WBOn1, crossOptions.FilteringExpression1, crossOptions.PreWbBase, isChart);

                //bool[] conditionArray  =      new Criteria(criteriaDescProvider.Create((decimal)scenario.ScenarioTotalizationId), surveyRootPath).Filtering((decimal)scenario.ScenarioTotalizationId);

                _log.Info("Filter started");
                bool[] filterringFlag = null;
                if (crossOptions.HasFilter)
                {
                    if (!checkVariableFilter(crossOptions.Filters, Definiotion.VariableDictionary))
                    {
                        OnWorkerComplete(currentProgress, LocalResource.PB_CROSS_CALC, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.GT_INVALID_FILTER_SETTINGS, Enums.MessageType.ErrorOk, workBook, pb);
                        return;
                    }
                    string filterExp = CriteriaDescProvider.CreateCriteriaDescriptions(crossOptions.Filters, questions);
                    filterringFlag = new Criteria(filterExp, "", questions).Filtering(DBHelper.GetConnectionString(workBook), tableName: tableNameAnswer);
                    crossOptions.FilteringExpression1 = filterExp;
                    filterExp = CriteriaDescProvider.CreateCriteriaDescriptionsForLocalExp(crossOptions.Filters, questions);
                    crossOptions.LocalizedFilteringExpression1 = CriteriaDescProvider.LocalizeFilteringExpression(filterExp, req, questions, true);
                }
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
                    REPORT_N_MATRIX_DESCRIPTION_KEYWORD = LocalResource.REPORT_N_MATRIX_DESCRIPTION_KEYWORD
                };
                _log.Info("Filter completed");
                int idReportSet = 1;
                divCnt = -1;
                double porgressDivRowStep = 20 / crossTableDivList.Count;
                foreach (List<CrossTableDiv> crossTableDivListRow in crossTableDivList)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PB_CROSS_CALC);
                    double porgressDivColStep = porgressDivRowStep / crossTableDivListRow.Count;
                    foreach (CrossTableDiv crossTableDivListCol in crossTableDivListRow)
                    {
                        OnWorkerComplete(currentProgress, LocalResource.PB_CROSS_CALC);
                        Macromill.QCWeb.ReportRequest.Reportsets.Reportset reportset = null;
                        List<List<CossTableInput>> crTableSets = crossTableDivListCol.CossTableInputs;
                        foreach (List<CossTableInput> crTableSetItems in crTableSets)
                        {
                            if (crTableSetItems.Count > 0)
                            {
                                divCnt++;
                                break;
                            }
                        }
                        Dictionary<string, DataWithMarking[,]> crossArrayMap = new Dictionary<string, DataWithMarking[,]>();
                        Dictionary<string, DataWithMarking[][,]> crossArrayListMap = new Dictionary<string, DataWithMarking[][,]>();

                        int crTableSetItemCnt = 1;
                        double porgresscrTableSetItemStep = porgressDivColStep / crTableSets.Count;
                        if (hasDiv)
                        {
                            QC4Common.Common.ReturnClass returnClass = QC4Common.Common.CRValidate.ValidateDiv(crTableSets, Definiotion.VariableDictionary, isChart);
                            if (null != returnClass && returnClass.Result == false)
                            {
                                string divName = divNameLis[divCnt];
                                writeErrorFile(divName, returnClass.Msg, workBook, crossOptions.GroupFolderPath);
                                List<string> tsvPath = new List<string>();
                                tsvPathsDiv.Add(tsvPath);
                                continue;
                            }
                        }

                        foreach (List<CossTableInput> crTableSetItems in crTableSets)
                        {
                            OnWorkerComplete(currentProgress, String.Format(LocalResource.PB_CROSS_CALC_TABLE, crTableSetItemCnt, crTableSets.Count));
                            currentProgress += porgresscrTableSetItemStep;
                            if (crTableSetItems.Count == 0) { continue; }
                            if (reportset == null)
                            {
                                reportset = (Macromill.QCWeb.ReportRequest.Reportsets.Reportset)req.Reportsets[Convert.ToString(idReportSet)];
                                idReportSet++;
                            }
                            DataWithMarking[][,] tabulationArray = new DataWithMarking[crTableSetItems.Count][,];
                            Question qstn = null;
                            int i = 0;
                            int crTableSetCnt = 1;
                            List<Data> dataList = null;
                            QuestionType targetQtype = 0;
                            int subTotalCount = 0;
                            foreach (CossTableInput crTableSet in crTableSetItems)
                            {
                                if (bgWorker.CancellationPending) return;
                                DataWithMarking[,] result = null;
                                DataWithMarking[][,] resultList = null;
                                qstn = null;
                                Question axis1 = null;
                                Question axis2 = null;
                                string targetVaraible = crTableSet.target;
                                string axis1Varaible = crTableSet.axis1;
                                string axis2Varaible = crTableSet.axis2;
                                bool hasCount = false;
                                bool hasLower = true;
                                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                                {
                                    con.Open();
                                    QuestionSettings qstnDet = Definiotion.VariableDictionary[targetVaraible];
                                    qstn = (Question)questions[qstnDet.Id];
                                    //if (qstn == null) {
                                    //    throw 
                                    //}
                                    //crossOptions.TabulationDescriptions = tabulationDescriptions;
                                    tabulationDescriptions = crossOptions.TabulationDescriptions;
                                    if (String.IsNullOrEmpty(axis1Varaible) && crTableSetItems.Count > 1)
                                    {
                                        tabulationDescriptions = new TabulationDescriptions(tabulationDescriptions);
                                        tabulationDescriptions.TotalAxisDescription = qstnDet.Question;
                                    }
                                    bool TestFlag = crossOptions.TestFlag1;
                                    if ((qstn.QuestionType & QuestionType.N) == QuestionType.N && crossOptions.TestCode == Macromill.QCWeb.ReportRequest.SignificanceTestCode.Off)
                                    {
                                        TestFlag = false;
                                    }
                                    hasLower = true;
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
                                        else
                                        {
                                            hasLower = false;
                                        }

                                        int[] criteriaSectorList;
                                        weightArray = new string[qstnDet.CategoryCount];
                                        GlobalTabulation.CriteriaValueDescriptionToValueList<int>(QuestionType.MA, qstnDet.Count, out criteriaSectorList, qstnDet.CategoryCount);
                                        for (int ct = 1; ct <= qstnDet.CategoryCount; ct++)
                                        {
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
                                            if (weightArray[cnt] != null && weightArray[cnt].Length != 0)
                                            {
                                                crTableSet.HasWeight = true; break;
                                            }
                                        }
                                    }

                                    System.Data.DataTable dataTble = new System.Data.DataTable();

                                    if (dataList == null || dataList.Count == 0 || String.IsNullOrEmpty(axis1Varaible))
                                    {
                                        targetQtype = qstn.QuestionType;
                                        if (qstnDet != null && qstnDet.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.An)
                                            tableName = DBHelperCommon.getTableName(con, qstn.ColumnName, out isMv);
                                        else
                                        {
                                            tableName = tableNameAnswer;
                                            isMv = false;
                                        }
                                        if (checkQuestion(qstnDet, workBook) &&
                                            (groupDataList == null || groupDataList != null && groupDataList.Count > 0))
                                        {
                                            if (!String.IsNullOrEmpty(qstnDet.AddSubTotal) && qstnDet.SubTotalCount > 0)
                                            {
                                                dataList = ReadSubTotalData(qstnDet, qstn, workBook, con, tableNameAnswer);
                                                targetQtype = qstn.QuestionType & ~QuestionType.SA;
                                                targetQtype = targetQtype | QuestionType.MA;
                                                subTotalCount = qstnDet.SubTotalCount;
                                            }
                                            else
                                            {
                                                if (!isMv)
                                                {
                                                    dataTble = DBHelper.GetDataTable("Select " + qstn.ColumnName + " from  " + tableName + " order by sort_no ", con);

                                                }
                                                else
                                                {
                                                    dataTble = DBHelper.GetDataTable("Select " + qstn.ColumnName + " from " + tableNameAnswer + " a join "
                                                        + tableName + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                                                }
                                                dataList = ReadTextFile.ReadDataTable(dataTble, qstn.QuestionType, null, out ex);
                                            }
                                        }
                                        else
                                        {
                                            dataTble = DBHelper.GetDataTable("Select NULL As tmpcol from " + tableNameAnswer, con);
                                            DPCheckList.DPCheckListHelper.setDeleteFlag(dataTble.Rows.Count);
                                            try
                                            {
                                                dataList = ReadTextFile.ReadDataTable(dataTble, qstn.QuestionType, null, out ex);
                                            }
                                            finally
                                            {
                                                ReadTextFile.DeleteFlag = null;
                                            }
                                            subTotalCount = !String.IsNullOrEmpty(qstnDet.AddSubTotal) ? qstnDet.SubTotalCount : 0;
                                        }
                                    }

                                    QuestionSettings qstnDet1 = null;
                                    if (String.IsNullOrEmpty(axis1Varaible))
                                    {
                                        axis1 = new Question();
                                        axis1.QuestionType = QuestionType.SA;
                                    }
                                    else
                                    {
                                        qstnDet1 = Definiotion.VariableDictionary[axis1Varaible];
                                        axis1 = (Question)questions[qstnDet1.Id];
                                    }
                                    if (null != axis2Varaible)
                                    {
                                        qstnDet = Definiotion.VariableDictionary[axis2Varaible];
                                        axis2 = (Question)questions[qstnDet.Id];
                                    }

                                    List<List<Data>> axesDatga = new List<List<Data>>();
                                    if (qstnDet1 != null && qstnDet1.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.An)
                                        tableName = DBHelperCommon.getTableName(con, axis1.ColumnName, out isMv);
                                    else
                                    {
                                        tableName = tableNameAnswer;
                                        isMv = false;
                                    }
                                    if (qstnDet1 != null && checkQuestion(qstnDet1, workBook) && dataList.Count > 0)//qstndet wrong
                                    {
                                        if (!isMv)
                                        {
                                            dataTble = DBHelper.GetDataTable("Select " + axis1.ColumnName + " from  " + tableName + " order by sort_no ", con);
                                        }
                                        else
                                        {
                                            dataTble = DBHelper.GetDataTable("Select " + axis1.ColumnName + " from " + tableNameAnswer + " a join "
                                                + tableName + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                                        }
                                    }
                                    else
                                    {
                                        dataTble = DBHelper.GetDataTable("Select NULL As tmpcol from " + tableNameAnswer, con);
                                        DPCheckList.DPCheckListHelper.setDeleteFlag(dataTble.Rows.Count);
                                    }
                                    List<Data> dataList2;
                                    try
                                    {
                                        dataList2 = ReadTextFile.ReadDataTable(dataTble, axis1.QuestionType, null, out ex);
                                    }
                                    finally
                                    {
                                        ReadTextFile.DeleteFlag = null;
                                    }
                                    if (null != axis2)
                                    {
                                        if (qstnDet != null && qstnDet.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.An)
                                            tableName = DBHelperCommon.getTableName(con, axis2.ColumnName, out isMv);
                                        else
                                        {
                                            tableName = tableNameAnswer;
                                            isMv = false;
                                        }
                                        if (checkQuestion(qstnDet, workBook) && dataList.Count > 0 && dataList2.Count > 0)
                                        {
                                            if (!isMv)
                                            {
                                                dataTble = DBHelper.GetDataTable("Select " + axis2.ColumnName + " from  " + tableName + " order by sort_no ", con);

                                            }
                                            else
                                            {
                                                dataTble = DBHelper.GetDataTable("Select " + axis2.ColumnName + " from " + tableNameAnswer + " a join "
                                                    + tableName + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                                            }
                                        }
                                        else
                                        {
                                            dataTble = new System.Data.DataTable();
                                            dataList = ReadTextFile.ReadDataTable(dataTble, qstn.QuestionType, null, out ex);
                                            dataList2 = ReadTextFile.ReadDataTable(dataTble, axis1.QuestionType, null, out ex);
                                        }
                                        List<Data> dataList3 = ReadTextFile.ReadDataTable(dataTble, axis2.QuestionType, null, out ex);
                                        axesDatga.Add(dataList2);
                                        axesDatga.Add(dataList3);
                                    }
                                    else
                                    {
                                        axesDatga.Add(dataList2);
                                    }
                                    bool[] filterringFlagCr = null;
                                    if (crTableSet.filter != null)
                                    {
                                        if (filterringFlag != null && filterringFlag.Count() > 0) filterringFlagCr = (bool[])filterringFlag.Clone();
                                        string filterExp = CriteriaDescProvider.CreateCriteriaDescriptions(crTableSet.filter, questions);
                                        new Criteria(filterExp, "", questions, filterringFlag != null && filterringFlag.Count() > 0 ? Operator.opAnd : Operator.opOr).Filtering(ref filterringFlagCr, DBHelper.GetConnectionString(workBook), tableNameAnswer);
                                    }
                                    _log.Info("calculation started");
                                    if (group == null)
                                    {
                                        QCWebException ex2 = CrossTabulation.GetCrossArray(
                                            targetQtype,
                                            GetCategoryArray(qstn, true),
                                            dataList,
                                            GetAxisQTypeList(axis1, axis2),
                                            GetAxisQuestionTitle(axis1, axis2),
                                            GetAxisCategoryList(axis1, axis2),
                                            axesDatga,
                                            out result, translation,
                                            tabulationDescriptions,
                                            crTableSet.filter != null ? filterringFlagCr : filterringFlag,
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
                                            significanceTestCode: crossOptions.TestCode,
                                            significanceTestLevel: crossOptions.TestLevels.ToArray(),
                                            SignificanceTestLogFilePath: sigLog,
                                            qName: qstn.Name, axisQName: GetAxisQuestionName(axis1, axis2), hasCount: hasCount, subTotalCnt: subTotalCount, qTypeOr: qstn.QuestionType,hasLower:hasLower);
                                        crossArrayMap.Add(crTableSetItemCnt + ":" + crTableSetCnt, result);
#if DEBUG
                                        //logOutput(result);
#endif
                                    }
                                    else
                                    {
                                        QCWebException ex2 = CrossTabulation.GetCrossArray(
                                            targetQtype,
                                            GetCategoryArray(qstn, true),
                                            dataList,
                                            GetAxisQTypeList(axis1, axis2),
                                            GetAxisQuestionTitle(axis1, axis2),
                                            GetAxisCategoryList(axis1, axis2),
                                            axesDatga,
                                            group.QuestionType,
                                            GetCategoryArray(group),
                                            dataList.Count > 0 ? groupDataList : groupDataListEmpty,
                                            out resultList, translation,
                                            tabulationDescriptions,
                                            crTableSet.filter != null ? filterringFlagCr : filterringFlag,
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
                                            significanceTestCode: crossOptions.TestCode,
                                            significanceTestLevel: crossOptions.TestLevels.ToArray(),
                                            SignificanceTestLogFilePath: sigLog, qName: qstn.Name, keyQName: group.Name, axisQName: GetAxisQuestionName(axis1, axis2), hasCount: hasCount, subTotalCnt: subTotalCount, qTypeOr: qstn.QuestionType,hasLower:hasLower);
                                        crossArrayListMap.Add(crTableSetItemCnt + ":" + crTableSetCnt, resultList);
                                    }
                                    _log.Info("calculation completed");
                                }
                                crTableSetCnt++;
                            }
                            //Microsoft.VisualBasic.Devices.ComputerInfo ci = new Microsoft.VisualBasic.Devices.ComputerInfo();
                            //_log.Info("************ MEM ************");
                            //_log.Info(ci.TotalPhysicalMemory);
                            //_log.Info(ci.AvailablePhysicalMemory);
                            //_log.Info(ci.TotalVirtualMemory);
                            //_log.Info(ci.AvailableVirtualMemory);
                            //_log.Info("Table calculation completed");
                            crTableSetItemCnt++;
                        }

                        if (reportset == null)
                        {
                            continue;
                        }
                        List<string> tsvPaths = new List<string>();
                        for (int loop = 1; loop <= groupingSectorCount; loop++)
                        {

                            Macromill.QCWeb.ReportRequest.Outputs.OutputCross cross = (Macromill.QCWeb.ReportRequest.Outputs.OutputCross)reportset.Outputs[loop - 1];
                            crTableSetItemCnt = 1;
                            foreach (List<CossTableInput> crTableSetItems in crTableSets)
                            {

                                if (crTableSetItems.Count == 0) { continue; }
                                List<DataWithMarking[,]> tabulationArray = new List<DataWithMarking[,]>();
                                List<CossTableInput> crTableSetItemsNarrow = new List<CossTableInput>();
                                Question qstn = null;
                                Question qstnPrev = null;
                                int crTableSetCnt = 1;
                                bool hasCount = false;
                                bool hasWeight = false;
                                string prevFilterExp = "";
                                string narrowingCondition = "";
                                bool enableSort = true;
                                foreach (CossTableInput crTableSet in crTableSetItems)
                                {
                                    if (bgWorker.CancellationPending) return;
                                    DataWithMarking[,] result = null;
                                    DataWithMarking[][,] resultList = null;
                                    //qstn = null;
                                    Question axis1 = null;
                                    Question axis2 = null;
                                    string targetVaraible = crTableSet.target;
                                    string axis1Varaible = crTableSet.axis1;
                                    string axis2Varaible = crTableSet.axis2;
                                    if (string.IsNullOrEmpty(axis1Varaible) && crTableSet.combine && crTableSetItems.Count > 1)
                                    {
                                        enableSort = false;
                                    }

                                    QuestionSettings qstnDet = Definiotion.VariableDictionary[targetVaraible];
                                    hasCount = crTableSet.HasCount;
                                    hasWeight = crTableSet.HasWeight;
                                    if (qstn == null)
                                    {
                                        qstn = (Question)questions[qstnDet.Id];
                                        qstnPrev = qstn;
                                    }
                                    //if (qstn == null) {
                                    //    throw 
                                    //}

                                    //qstnDet = Definiotion.VariableDictionary[axis1Varaible];
                                    //axis1 = (Question)questions[qstnDet.Id];
                                    if (null != axis2Varaible)
                                    {
                                        qstnDet = Definiotion.VariableDictionary[axis2Varaible];
                                        axis2 = (Question)questions[qstnDet.Id];
                                    }

                                    string filterExp = "";
                                    string LocalizedFilteringExpression = "";
                                    if (crTableSet.filter != null)
                                    {
                                        List<FilterSettingsCr> narrowLst = new List<FilterSettingsCr>();
                                        narrowLst.Add(crTableSet.filter);
                                        filterExp = CriteriaDescProvider.CreateCriteriaDescriptionsForLocalExp(narrowLst, questions);
                                        LocalizedFilteringExpression = CriteriaDescProvider.LocalizeFilteringExpression(filterExp, req, questions, true);
                                        if (LocalizedFilteringExpression != null)
                                        {
                                            LocalizedFilteringExpression = LocalResource.CR_FILTER_PREFIX + LocalizedFilteringExpression;
                                        }
                                    }


                                    if (group == null)
                                    {
                                        result = crossArrayMap[crTableSetItemCnt + ":" + crTableSetCnt];
                                        if (prevFilterExp == filterExp)
                                        {
                                            tabulationArray.Add(result);
                                            crTableSetItemsNarrow.Add(crTableSet);
                                        }
                                        else
                                        {
                                            using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                                            {
                                                con.Open();
                                                if (tabulationArray.Count > 0)
                                                {
                                                    Macromill.QCWeb.ReportRequest.Tables.CrossTable table = TableUtil.SetOutputRequestTableCross(workBook, cross, qstn, tabulationArray.ToArray(),
                                                    crTableSetItemsNarrow, questions, con, excludeCnt, crossOptions.TabulateFullQuantity1,
                                                    tableNameAnswer, isChart, crossOptions.ShowNoAnswerAxis, hasCount, hasWeight,
                                                    narrowingCondition: narrowingCondition, enableSort: enableSort, wBValue: crossOptions.WBVariable);
                                                    qstn = null;
                                                    tabulationArray = new List<DataWithMarking[,]>();
                                                    crTableSetItemsNarrow = new List<CossTableInput>();
                                                }
                                                tabulationArray.Add(result);
                                                crTableSetItemsNarrow.Add(crTableSet);
                                                prevFilterExp = filterExp;
                                                narrowingCondition = LocalizedFilteringExpression;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        resultList = crossArrayListMap[crTableSetItemCnt + ":" + crTableSetCnt];
                                        DataWithMarking[,] tabulation = resultList[loop - 1];
                                        if (prevFilterExp == filterExp)
                                        {
                                            tabulationArray.Add(tabulation);
                                            crTableSetItemsNarrow.Add(crTableSet);
                                        }
                                        else
                                        {
                                            using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                                            {
                                                con.Open();
                                                if (tabulationArray.Count > 0)
                                                {
                                                    Macromill.QCWeb.ReportRequest.Tables.CrossTable table = TableUtil.SetOutputRequestTableCross(
                                                        workBook, cross, qstn, tabulationArray.ToArray(), crTableSetItemsNarrow, questions, con, excludeCnt,
                                                        crossOptions.TabulateFullQuantity1, tableNameAnswer, isChart, crossOptions.ShowNoAnswerAxis,
                                                        hasCount, hasWeight, narrowingCondition: narrowingCondition, enableSort: enableSort, wBValue: crossOptions.WBVariable);
                                                    if (group != null)
                                                    {
                                                        Sectors.Sector sector = (Sectors.Sector)group.Sectors[loop];
                                                        table.SetKeyItemInformation(group.Name, group.Description2(), sector.Number, sector.Description);
                                                    }
                                                    qstn = null;
                                                    tabulationArray = new List<DataWithMarking[,]>();
                                                    crTableSetItemsNarrow = new List<CossTableInput>();
                                                }
                                                tabulationArray.Add(tabulation);
                                                crTableSetItemsNarrow.Add(crTableSet);
                                                prevFilterExp = filterExp;
                                                narrowingCondition = LocalizedFilteringExpression;
                                            }
                                        }
                                    }
                                    crTableSetCnt++;
                                }
                                if (tabulationArray.Count > 0)
                                {
                                    using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                                    {
                                        con.Open();
                                        Macromill.QCWeb.ReportRequest.Tables.CrossTable table = TableUtil.SetOutputRequestTableCross(workBook, cross,
                                            qstnPrev, tabulationArray.ToArray(), crTableSetItemsNarrow, questions, con, excludeCnt,
                                            crossOptions.TabulateFullQuantity1, tableNameAnswer, isChart, crossOptions.ShowNoAnswerAxis, hasCount,
                                            hasWeight, narrowingCondition: narrowingCondition, enableSort: enableSort, wBValue: crossOptions.WBVariable);
                                        if (group != null)
                                        {
                                            Sectors.Sector sector = (Sectors.Sector)group.Sectors[loop];
                                            table.SetKeyItemInformation(group.Name, group.Description2(), sector.Number, sector.Description);
                                        }
                                        qstn = null;
                                        qstnPrev = null;
                                    }
                                }
                                crTableSetItemCnt++;
                            }
                            tsvPathJoined = (cross.Tables as Macromill.QCWeb.ReportRequest.Tables).OutputToTSV();
                            tsvPaths.Add(tsvPathJoined);
                        }
                        //if (tsvPaths.Count > 0)
                        //{
                        tsvPathsDiv.Add(tsvPaths);
                        //}
                    }
                }


                //Request request = new Request();
                //request.MakeRequeszt(id, tsvPathsDiv, hasDiv ? divNameLis : null, surveyTitle, companyName, zeroshowcd, mergeaxis, crossOptions.Reportprefix,
                //    crossOptions.Xlbooknameprefix, crossOptions.Tabletype, crossOptions.Tableorientation, crossOptions.Pagesetuptabletype, crossOptions.Minsamplescountonmarking,
                //    crossOptions.Markingtype, crossOptions.Significancetestlevel, crossOptions.Papersize,
                //    crossOptions.Paperorientation, crossOptions.Tablesononesheet, crossOptions.Level2highcolorindex, crossOptions.Level1highcolorindex,
                //    crossOptions.Level1lowcolorindex, crossOptions.Level2lowcolorindex, crossOptions.Level1percent, crossOptions.Level2percent,
                //    crossOptions.ShowNACode1, crossOptions.ShowIVCode1, crossOptions.WBOn1, crossOptions.LocalizedFilteringExpression1, crossOptions.PreWbBase, isChart);
                _log.Info("Cross tab calculation completed");
                _log.Info("Cross tab excel creation started");

                if (bgWorker.CancellationPending) return;
                OnWorkerComplete(30, LocalResource.PB_EXCEL_GEN);
                excelOperate = new ExcelOperate();
                xlApp = excelOperate.Excel;
                xlApp.Visible = false;
                xlApp.ScreenUpdating = false;
#if DEBUG
                //xlApp.Visible = true;
                //xlApp.ScreenUpdating = true;
                //xlApp.Calculation = XlCalculation.xlCalculationManual;
#endif
                xlApp.EnableEvents = false;
                //xlApp.DisplayStatusBar = false;
                xlApp.PrintCommunication = false;
                xlApp.DisplayAlerts = false;
                currentProgress = 30;
                int rptCnt = tsvPathsDiv.Count;
                double progressStepDiv = 69 / (rptCnt == 0 ? 1 : rptCnt);
                double currentProgressDiv = currentProgress;

                idReportSet = 0;
                OutputCross CurrentOutput = null;
                if (bgWorker.CancellationPending) return;
                foreach (List<string> tsvPaths in tsvPathsDiv) // div. Both row and col
                {
                    string divName = "";
                    if (hasDiv)
                    {
                        divName = divNameLis[idReportSet];
                    }
                    idReportSet++;

                    int i = 0;
                    double progressStepGrp = progressStepDiv / (tsvPaths.Count == 0 ? 1 : tsvPaths.Count);
                    foreach (string tsvPath in tsvPaths) // group
                    {
                        Request request = new Request();
                        request.MakeRequeszt(id, idReportSet, i, tsvPath, divName, surveyTitle, companyName, zeroshowcd, mergeaxis, crossOptions.Reportprefix,
                                            crossOptions.Xlbooknameprefix, crossOptions.Tabletype, crossOptions.Tableorientation, crossOptions.Pagesetuptabletype, crossOptions.Minsamplescountonmarking,
                                            crossOptions.Markingtype, crossOptions.Significancetestlevel, crossOptions.Papersize,
                                            crossOptions.Paperorientation, crossOptions.Tablesononesheet, crossOptions.Level2highcolorindex, crossOptions.Level1highcolorindex,
                                            crossOptions.Level1lowcolorindex, crossOptions.Level2lowcolorindex, crossOptions.Level1percent, crossOptions.Level2percent,
                                            crossOptions.ShowNACode1, crossOptions.ShowIVCode1, crossOptions.WBOn1, crossOptions.LocalizedFilteringExpression1, crossOptions.PreWbBase, isChart);
                        i++;
                        foreach (Reportset rs in request.Reportsets.Values)
                        {
                            if (!isChart)
                            {
                                foreach (Output ouput in rs.Outputs.Values)
                                {
                                    CurrentOutput = (OutputCross)ouput;
                                    //if (CurrentOutput.TablesOnOnesheet == Macromill.QCWeb.ReportRequest.TablesOnOneSheet.Multi)
                                    //{
                                    NPOICrossCreator nPOICrossCreator = new NPOICrossCreator();
                                    nPOICrossCreator.CreateCross(ouput, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1),
                                        crossOptions.GroupFolderPath, System.AppContext.BaseDirectory, xlApp, bgWorker, bgWorkerArg, QC: this,
                                        progressAvailable: progressStepGrp, currentProgress: currentProgress,
                                        qc4FileName: window != null && !isPro ? Definiotion.SelectedFile : null, outputFiles: outputFiles, combineBanners: crossOptions.CombineBanner);
                                    if (bgWorker.CancellationPending) return;
                                    OnWorkerComplete(currentProgress, LocalResource.PB_EXCEL_GEN);
                                    //}
                                    //else
                                    //{
                                    //    CrossCreator crossCreator = new CrossCreator();
                                    //    crossCreator.CreateCross(ouput, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1),
                                    //        crossOptions.GroupFolderPath, System.AppContext.BaseDirectory, xlApp, QC: this,
                                    //        progressAvailable: progressStepGrp, currentProgress: currentProgress);
                                    //    OnWorkerComplete(currentProgress, LocalResource.PB_EXCEL_GEN);
                                    //}
                                }
                            }
                            else
                            {

                                //int flag = 1;
                                //if (flag == 0)
                                //{
                                //    ReportCreator reportCreator = new ReportCreator();
                                //    reportCreator.CreateReport((Outputs)rs.Outputs, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1),
                                //        crossOptions.GroupFolderPath, System.AppContext.BaseDirectory, xlApp, bgWorker, bgWorkerArg, rs,
                                //        QC: this, progressAvailable: progressStepGrp, curProgres: currentProgress,
                                //            qc4FileName: window != null && !isPro ? Definiotion.SelectedFile : null, outputFiles: outputFiles);
                                //}
                                //else
                                //{

                                ReportCreatorXML reportCreator = new ReportCreatorXML();
                                reportCreator.CreateReport((Outputs)rs.Outputs, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1),
                                crossOptions.GroupFolderPath, crossOptions.IsCheckRefineCondition, System.AppContext.BaseDirectory, xlApp, bgWorker, bgWorkerArg, rs,
                                QC: this, progressAvailable: progressStepGrp, curProgres: currentProgress,
                                    qc4FileName: window != null && !isPro ? Definiotion.SelectedFile : null, outputFiles: outputFiles);

                                //}
                                if (bgWorker.CancellationPending) return;
                            }
                            currentProgress += progressStepGrp;
                            OnWorkerComplete(currentProgress, LocalResource.PB_EXCEL_GEN);
                        }
                    }
                    currentProgressDiv += progressStepDiv;
                    currentProgress = currentProgressDiv;
                }
                if (group == null && !hasDiv)
                {
                    this.OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED, true, true);
                    xlApp.EnableEvents = true;
                    // xlApp.WindowState = XlWindowState.xlMaximized;
                    maximizeExcel(xlApp);
                    xlApp.ScreenUpdating = true;
                    xlApp.DisplayAlerts = true;
                    childExcelApp = (IntPtr)xlApp.Hwnd;
                    //this.OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED, true);
                    xlApp.Visible = true;

                    string outPath = Path.Combine(Path.GetTempPath(), "QC4", "output", Path.GetFileNameWithoutExtension(Util.Constants.Qc4FileName));
                    if (Directory.Exists(outPath))
                    {
                        var di = new DirectoryInfo(outPath);
                        if (di.Attributes.HasFlag(FileAttributes.ReadOnly))
                            di.Attributes &= ~FileAttributes.ReadOnly;
                    }
                }
                else
                {
                    OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED, true, true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.COMPLETED, Enums.MessageType.Info, workBook, pb);
                    OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED, true);
                    Process.Start(crossOptions.GroupFolderPath);
                }

                _log.Info("Cross tab excel creation completed");

            }
            catch (Exception ex)
            {
                string exMsg = LocalResource.FAILED_TO_GENE_EXCEL;
                try
                {
                    _log.Error(ex.Message);
                    _log.Error(ex.StackTrace);
                    if (!ex.Message.Contains("OutOfMemoryException"))
                    {
                        _log.LogError(ex.Message);
                    }
                    if (ex.Message.Contains("0x800AC472"))
                    {
                        exMsg = "Execution failed due to un licenced MS Office.";
                    }
                    if (ex.Source.Contains("QCWebCommon"))
                    {
                        if (crossOptions != null && !String.IsNullOrEmpty(crossOptions.WBVariable) && ex.Message.Contains("パラメータ '{0}' が不正です。:{1}"))
                            exMsg = LocalResource.QC_WEB_WEIGHTBACK_EXCEPTION + "\n" + crossOptions.WBVariable;
                    }
                }
                finally
                {
                    MessageDialog.ShowMessageOnWorkBook(exMsg, Enums.MessageType.ErrorOk, workBook, pb);
                    if (group == null && !hasDiv && excelOperate != null)
                    {
                        excelOperate.Dispose();
                    }

                    try
                    {
                        string outPath = Path.Combine(Path.GetTempPath(), "QC4", "output", Path.GetFileNameWithoutExtension(Util.Constants.Qc4FileName));
                        if (Directory.Exists(outPath))
                        {
                            var di = new DirectoryInfo(outPath);
                            if (di.Attributes.HasFlag(FileAttributes.ReadOnly))
                                di.Attributes &= ~FileAttributes.ReadOnly;
                        }
                    }
                    catch { }
                }
            }
            finally
            {
                if (xlApp != null && group == null && !hasDiv || xlApp != null && bgWorker.CancellationPending)
                {
                    try
                    { COMWholeOperate.releaseComObject<Application>(ref xlApp); }
                    catch { }
                }
                deleteFiles();
                if ((group != null || hasDiv || bgWorker.CancellationPending) && excelOperate != null)
                {
                    excelOperate.Dispose();
                }
                if (bgWorker.CancellationPending)
                {
                    deleteOutPutFiles(outputFiles, crossOptions == null ? false : crossOptions.HasDiv, crossOptions == null ? null : crossOptions.GroupFolderPath);
                    OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED, true, true, true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.MSG_OUTPUT_ABORTED, Enums.MessageType.Info, workBook, pb);
                    OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED, true, close: true);
                }
                else
                {
                    OnWorkerComplete(100, LocalResource.PB_CROSS_TAB_COMPLETED);
                }
                _log.Info("Cross Tabulate completed");
            }
        }

        public static bool checkVariableFilter(List<FilterSettingsCr> filters, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            for (int i = 0; i < filters.Count; i++)
            {
                FilterSettingsCr criterion = filters[i];
                QuestionSettings x = null;
                bool val = VariableDictionary.TryGetValue(criterion.variable, out x);
                if (!val)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool checkVariableDivision(string groupVariable, Dictionary<string, QuestionSettings> VariableDictionary)
        {
            QuestionSettings x = null;
            return VariableDictionary.TryGetValue(groupVariable, out x);
        }

        public static void maximizeExcel(Application xlApp)
        {
            xlApp.WindowState = XlWindowState.xlMaximized;
            Workbooks wbs = xlApp.Workbooks;
            foreach (Workbook wb in wbs)
            {
                wb.Windows[1].WindowState = XlWindowState.xlMaximized;
            }
        }

        public static void deleteFiles()
        {
            try
            {
                ApplicationConfig appConfig = new ApplicationConfig();
                System.IO.DirectoryInfo di = new DirectoryInfo(appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP));
                di.Delete(true);
            }
            catch (Exception ex) { }
        }

        public static void deleteOutPutFiles(List<string> outputFiles, bool HasDiv = false, string folderPath = null)
        {
            if (null == outputFiles) return;
            foreach (string path in outputFiles)
            {
                try
                {
                    FileInfo file = new FileInfo(path);
                    file.Delete();

                }
                catch (Exception ex)
                {
                    _log.Info(ex.Message);
                }
            }
            if (HasDiv == false) return;
            try
            {
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                dir.Delete(true);
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message);
            }
        }

        private bool checkQuestion(QuestionSettings qstnDet, Workbook workBook)
        {
            List<QuestionSettings> qlist = new List<QuestionSettings>();
            qlist.Add(qstnDet);
            QC4Common.DB.DBHelper.CheckIfColumnExists(workBook, qlist, out List<string> variables, out List<string> columns, out List<decimal> idss);
            if (variables.Count == 0) { return true; }
            return false;
        }

        public static bool checkUnprocessedNewQuestion(Workbook workBook)
        {
            //if (QC4Common.DB.DBHelper.checkAfterProcess(workBook))
            //{
            //    return false;
            //}
            List<QuestionSettings> qlist = Definiotion.VariableDictionary.Values.ToList();
            QC4Common.DB.DBHelper.CheckIfColumnExists(workBook, qlist, out List<string> variables, out List<string> columns, out List<decimal> idss);
            if (variables.Count > 0) { return true; }
            return false;
        }

        private void writeErrorFile(string divName, string errMsg, Workbook workBook, string dir)
        {
            string errorFile = System.IO.Path.Combine(dir, "error.txt");
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(errorFile, true, Encoding.UTF8))
            {
                writer.WriteLine(divName.TrimEnd('_') + "\t" + errMsg);
            }
        }

        public List<Data> ReadSubTotalData(QuestionSettings qstnDet, Question qstn, Workbook workbook, System.Data.SQLite.SQLiteConnection con, string tableName = "answers", QuestionType questionType = QuestionType.MA)
        {
            string cellitemid = "";
            string cellopertor = "";
            string cellvalue = "";
            DataProcesses dp = new DataProcesses();
            string processedfile = null;
            try
            {
                using (dp)
                {
                    var dirPath = Path.Combine(Path.GetTempPath(), "QC4");  //@"c:\qcweb\txt\7";
                    dp.DataDirectoryPath = dirPath;
                    int rowCnt = DBHelper.ExecuteScalar("select count(*) from " + tableName, con);
                    dp.SamplesCount = rowCnt;
                    dp.ConnectionString = DBHelper.GetConnectionString(workbook);
                    dp.FromSubTotal = true;
                    dp.TableName = tableName;
                    string newVariable = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                    string NewQstnVariable = newVariable;
                    IDataProcess dpOperator;
                    _INewQuestion newQuestion;
                    INewQuestionSectors sectors;

                    int sourceItemId = Convert.ToInt32(qstnDet.Id);
                    dpOperator = dp.Add(DataProcessCode.Recode);
                    dpOperator.RunFlag = true;
                    newQuestion = dpOperator.Questions.Add();
                    newQuestion.ItemId = newVariable;
                    newQuestion.Name = NewQstnVariable;// Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn].Text;//"NGID";
                    newQuestion.SourceQuestionType = qstn.QuestionType;
                    newQuestion.QuestionType = questionType;

                    ((INewQuestion)newQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                    newQuestion.SourceItemId = sourceItemId.ToString();

                    sectors = newQuestion.Sectors;
                    for (int i = 1; i <= qstnDet.CategoryCount; i++)
                    {
                        sectors.Add(newQuestion.SourceItemId + "=" + i);
                    }
                    foreach (QuestionSettings.SubTotal subTotal in qstnDet.SubTotals)
                    {
                        string firstVal = subTotal.Criteria;
                        firstVal = firstVal.Replace(" ", "");
                        string op = "=";
                        if (firstVal.StartsWith("=") || firstVal.StartsWith("!") || firstVal.StartsWith("<>"))
                        {
                            if (firstVal.StartsWith("<>"))
                            {
                                firstVal = firstVal.Substring("<>".Length);
                                op = "!=";
                            }
                            else if (firstVal.StartsWith("!"))
                            {
                                firstVal = firstVal.TrimStart('!');
                                op = "!=";
                            }
                            else
                            {
                                firstVal = firstVal.Substring(1);
                            }

                            if (firstVal.StartsWith("<>") || firstVal.StartsWith("!"))
                            {
                                if (firstVal.StartsWith("<>"))
                                {
                                    firstVal = firstVal.Substring("<>".Length);
                                    firstVal = "!" + firstVal;
                                }
                                int[] criteriaSectorList;
                                GlobalTabulation.CriteriaValueDescriptionToValueList<int>(
                                                            QuestionType.SA, firstVal, out criteriaSectorList, qstnDet.CategoryCount);
                                if (criteriaSectorList.Length < 1)
                                {
                                    firstVal = Convert.ToString(qstnDet.CategoryCount + 1); // hack for no category  
                                }
                                else
                                {
                                    firstVal = string.Join(",", criteriaSectorList);
                                }
                            }
                            firstVal += "&" + newQuestion.SourceItemId + "!=DK";
                            firstVal += "&" + newQuestion.SourceItemId + "!=*";
                        }
                        sectors.Add(newQuestion.SourceItemId + op + firstVal);
                    }
                    dp.Execute();
                    processedfile = dirPath;
                    ApplicationConfig appConfig = new ApplicationConfig();
                    processedfile = System.IO.Path.Combine(appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP));
                    processedfile = processedfile + "\\" + newQuestion.ItemId + ".dp";
                    //int itemid = Convert.ToInt32(newQuestion.ItemId);
                    QCWebException ex;
                    List<Data> data = ReadTextFile.ReadData(processedfile, questionType, null, out ex);
                    return data;
                    //break;
                }
            }
            finally
            {
                if (processedfile != null && File.Exists(processedfile))
                {
                    File.Delete(processedfile);
                }
            }
        }

        public string GetProcessIdPath()//#212092 
        {
            string outputfilepath = System.IO.Path.Combine(Path.GetTempPath(), "QC4\\");
            ApplicationConfig appConfig = new ApplicationConfig();
            outputfilepath =
    System.IO.Path.Combine(
        appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP));
            GlobalMethodClass.GuaranteeDirectoryExist(outputfilepath);

            return outputfilepath;
        }

        private string checkFilesExist(ICollection values, Application xlApp, string OutputDirectoryPath, bool isChart)
        {
            foreach (Output ouput in values)
            {
                CrossTable tmpTable = (CrossTable)ouput.Tables[0];
                Macromill.QCWeb.ReportRequest.KeyItemInformation KeyItemInfo = tmpTable.KeyItem;
                string KeyItemName = string.Empty;
                string filenameSuffix = null;
                if (KeyItemInfo != null)
                    KeyItemName = KeyItemInfo.Name;
                if (KeyItemName.Length > 0)
                {
                    string fmt = new string('0', 3);
                    filenameSuffix = "_" + KeyItemName + "_" + KeyItemInfo.SectorNumber.ToString(fmt);
                    string prefix;
                    if (isChart)
                    {
                        prefix = ouput.ParentReportset.DivName + ouput.ParentReportset.FileNamePrefix;
                    }
                    else
                    {
                        prefix = ouput.ParentReportset.DivName + ouput.ExcelBookNamePrefix;
                    }
                    string n = prefix + filenameSuffix + ".xlsx";
                    string p = OutputUtil.BuildPath(OutputDirectoryPath, n, xlApp.PathSeparator);
                    if (File.Exists(p))
                    {
                        return p;
                    }
                }
            }
            return null;
        }

        public static void logOutput(DataWithMarking[,] result)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            for (int i = result.GetLowerBound(0); i < result.GetUpperBound(0); i++)
            {
                _log.Info("*********ROW**********" + " : " + i);
                for (int j = result.GetLowerBound(1); j < result.GetUpperBound(1); j++)
                {
                    _log.Info(result[i, j].CellType + " : " + result[i, j].Value);
                    sb.Append(result[i, j].Value).Append("\t");
                }
                sb.Append("\n");
            }
            _log.Info(sb);
        }

        public static List<QuestionType> GetAxisQTypeList(Question Q1, Question Q2)
        {
            List<QuestionType> retList = new List<QuestionType>();
            // 軸1
            retList.Add(Q1.QuestionType);
            // 軸2
            if (Q2 != null)
            {
                retList.Add(Q2.QuestionType);
            }
            return retList;
        }


        public static string[] GetCategoryArray(Question q, bool subtotal = false)
        {
            string[] retArray = new string[0];
            Sectors category = (Sectors)q.Sectors;
            int i = 1;
            if (category != null)
            {
                retArray = new string[category.Count];
                for (i = 1; i <= category.Count; i++)
                {
                    retArray[i - 1] = category[i].Description;
                }
            }
            if (subtotal)
            {
                QuestionSettings qstnDet = Definiotion.VariableDictionary[q.Name];
                if (!String.IsNullOrEmpty(qstnDet.AddSubTotal) && qstnDet.SubTotalCount > 0)
                {
                    Array.Resize(ref retArray, category.Count + qstnDet.SubTotalCount);
                    foreach (QuestionSettings.SubTotal subTotal in qstnDet.SubTotals)
                    {
                        retArray[i - 1] = subTotal.Subtotal;
                        i++;
                    }
                }
            }
            return retArray;
        }

        public static List<string> GetAxisQuestionName(Question Q1, Question Q2)
        {
            List<string> retList = new List<string>();
            // 軸1
            retList.Add(Q1.Name);
            // 軸2
            if (Q2 != null)
            {
                retList.Add(Q2.Name);
            }
            return retList;
        }

        public static List<string> GetAxisQuestionTitle(Question Q1, Question Q2)
        {
            List<string> retList = new List<string>();
            // 軸1
            retList.Add(ConvertNullToEmpty(Q1.Number2 == null ? Q1.Description : Q1.Number2 + " " + Q1.Description)); // MANTIS#0002107
                                                                                                                      // 軸2
            if (Q2 != null)
            {
                retList.Add(ConvertNullToEmpty(Q2.Number2 == null ? Q2.Description : Q2.Number2 + " " + Q2.Description)); // MANTIS#0002107
            }
            return retList;
        }
        // ⑥集計軸の質問文の配列のリストを返す
        public static List<string[]> GetAxisCategoryList(Question Q1, Question Q2)
        {
            List<string[]> retList = new List<string[]>();
            // 軸1
            retList.Add(GetCategoryArray(Q1));
            // 軸2
            if (Q2 != null)
            {
                retList.Add(GetCategoryArray(Q2));
            }
            return retList;
        }

        public static string ConvertNullToEmpty(string key)
        {
            string ret = String.Empty;

            if (String.IsNullOrEmpty(key))
            {
                ret = String.Empty;
            }
            else
            {
                ret = key;
            }
            return ret;
        }

        public static string numberToAlpha(long number, bool isLower = false)
        {
            string returnVal = "";
            char c = isLower ? 'a' : 'A';
            while (number >= 0)
            {
                returnVal = (char)(c + number % 26) + returnVal;
                number /= 26;
                number--;
            }

            return returnVal;
        }
    }
}
