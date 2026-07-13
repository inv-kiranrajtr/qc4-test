using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using Qc4Launcher.Util;
using System.IO;
using static Qc4Launcher.Summary.SummaryReader;
using Macromill.QCWeb.Tabulation;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using static Macromill.QCWeb.Question.Questions;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Logic.TabulationEx.Criteria;
using Qc4Launcher.DB;
using Macromill.QCWeb.Batch.Report;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using QC4Common.Model;
using Macromill.QCWeb.DataProcess;
using QC4Common.Util;
using Qc4Launcher.Logic;
using System.ComponentModel;
using Qc4Launcher.Summary.OpenXml;
using Macromill.QCWeb.Model;

namespace Qc4Launcher.Summary
{
    public class SummaryTabulation
    {
        public delegate void OnWorkerMethodCompleteDelegate(double value, string status, bool isForceStop = false, bool retainThread = false, bool close = false, bool disableCancel = false);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //public static ExcelOperate excelOperate = new ExcelOperate();
        private string ST_STR_LIST = " [" + LocalResource.TITLE_SUMMARY_FORM + "]";

        internal IntPtr childExcelApp = IntPtr.Zero;
        public void updateProgress(double currentProgress, string v, bool close = false)
        {
            OnWorkerComplete(currentProgress, v, close: close);
        }

        /// <summary>
        /// Method to create Summary Tabulation output.
        /// </summary>
        /// <param name="workBook">QC4 Workbook</param>
        /// <param name="workSheet">Summary Tabulation Wroksheet</param>
        /// <param name="worker">Sender object</param>
        /// <param name="bgWorkerArg">DoWorkEventArgs object</param>
        /// <param name="def"></param>
        internal void Tabulate(Workbook workBook, Worksheet workSheet, object worker, DoWorkEventArgs bgWorkerArg, bool def = false)
        {
            bool hasValidCases = false;
            int MAX_TARGET_COUNT = 10;
            double currentProgress = 1;
            Definiotion.VariableDictionary = DictionaryUtil.PopulateQSDictionary(workBook);
            Application xlApp = null;
            Question group = null;
            bool hasDiv = false;
            bool errorDiv = false;
            Dictionary<string, int> excludeCnt = new Dictionary<string, int>();
            ExcelOperate excelOperate = null;
            BackgroundWorker bgWorker = worker as BackgroundWorker;
            List<string> outputFiles = new List<string>();
            SummaryOptions summaryOptions = null;

            try
            {
                OpenXmlHelper.RemoveOutPutFiles("SummaryOutput", true);
                DBHelper.CreateMultivariateTempTable(workBook);
                childExcelApp = IntPtr.Zero;
                _log.Info("==================================================");
                _log.Info("ST Tabulate started");
                _log.Info("Executing validations");

                OnWorkerComplete(currentProgress, LocalResource.ST_VALIDATION_CHECKS);

                string errMsg = "";
                QC4Common.Common.SLValidate.ValidateSLTab(workSheet, Definiotion.VariableDictionary, ref errMsg, false);

                currentProgress = 5;
                _log.Info("Reading settings");
                OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
                string lccd = "JA";
                Dictionary<String, List<List<SummaryTableInput>>> stTableSetsDict = SummaryReader.ReadSummarySettings(workSheet, ref hasDiv);

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
                if (errMsg.Length > 0)
                {
                    if ("DND" == errMsg)
                    {
                        return;
                    }
                    else if (hasDiv)
                    {
                        errorDiv = true;
                        //writeErrorFile(errMsg, workBook);
                        //return;
                    }
                    else
                    {
                        OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(errMsg, Enums.MessageType.ErrorOk, workBook);
                        return;
                    }
                }
                if (stTableSetsDict.Count == 0)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.NO_VALID_SETTINGS_FOUND, Enums.MessageType.ErrorOk, workBook);
                    return;
                }

                string tableName = "answers";
                if (DBHelper.checkAfterProcess(workBook))
                {
                    tableName = "data_after_process";
                }
                //string sigLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QC4", "sig.log");
                string sigLog = OutputUtil.GetSigLogPath(workBook.Application.PathSeparator);
                summaryOptions = SummaryReader.ReadOptions(workBook);
                _log.Info("Reading settings completed");
                _log.Info("Summary table calculation started");
                currentProgress = 10;
                OnWorkerComplete(currentProgress, LocalResource.ST_SUMMARY_CALC);
                TabulationDescriptions tabulationDescriptions = new TabulationDescriptions(Qc4Launcher.Util.CommonFunction.SetDescriptionString());
                string companyName = LocalResource.REPORT_SIGNATURE_KEYWORD;
                Questions questions = DictUpdate.GetQuestions(workBook);
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

                if (!summaryOptions.IsWeightListValid)
                {
                    OnWorkerComplete(100, LocalResource.ST_FINISHED_SUMMARY);
                    return;
                }

                if (summaryOptions.GroupVariable != null)
                {
                    if (!System.IO.Directory.Exists(summaryOptions.GroupFolderPath))
                    {
                        string msg = LocalResource.DIR_NOT_EXIST;
                        OnWorkerComplete(currentProgress, LocalResource.ST_SUMMARY_CALC, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(msg, Enums.MessageType.ErrorOk, workBook);
                        return;
                    }
                    if (!CrossTabulationQC.checkVariableDivision(summaryOptions.GroupVariable, Definiotion.VariableDictionary))
                    {
                        OnWorkerComplete(currentProgress, LocalResource.ST_SUMMARY_CALC, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.INVALID_CLASSIFICATION_VARIABLE, Enums.MessageType.ErrorOk, workBook);
                        return;
                    }
                    using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                    {
                        con.Open();
                        QuestionSettings qstnDet = Definiotion.VariableDictionary[summaryOptions.GroupVariable];
                        try
                        {
                            group = (Question)questions[qstnDet.Id];
                            //System.Data.DataTable dataTble = DBHelper.GetDataTable("Select q_" + qstnDet.Id + " from " + tableName + " order by sort_no ", con);
                            System.Data.DataTable dataTble = new System.Data.DataTable();
                            groupDataListEmpty = ReadTextFile.ReadDataTable(dataTble, group.QuestionType, null, out ex);
                            if (checkQuestion(qstnDet, workBook))
                            {
                                if (qstnDet.QuestionFlag == "An")
                                    dataTble = DBHelper.GetDataTable("Select m." + group.ColumnName + " from multivariate m join " + tableName + " a on a.sort_no=m.sort_no order by a.sort_no ", con);
                                else
                                    dataTble = DBHelper.GetDataTable("Select " + group.ColumnName + " from " + tableName + " order by sort_no ", con);
                            }
                            else
                            {
                                dataTble = new System.Data.DataTable();
                            }
                            groupDataList = ReadTextFile.ReadDataTable(dataTble, group.QuestionType, null, out ex);
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
                int colj = 0;
                int maxTarget = 0;
                int ttDivCnt = 0;
                foreach (KeyValuePair<String, List<List<SummaryTableInput>>> dct in stTableSetsDict)
                {
                    if (dct.Key.StartsWith("div", StringComparison.OrdinalIgnoreCase))
                    {
                        string col = Qc4Launcher.Logic.CrossTabulationQC.numberToAlpha(colj);
                        divNameLis.Add(col + "1_");
                        colj++;
                    }
                    else
                    {
                        ttDivCnt++;
                        if (dct.Value.Count > maxTarget)
                        {
                            maxTarget = dct.Value.Count;
                        }
                    }
                }
                if (maxTarget > 0)
                {
                    MAX_TARGET_COUNT += maxTarget;
                }
                if (colj > 1 || hasDiv)
                {
                    hasDiv = true;
                    summaryOptions.HasDiv = true;
                    string currDateTimeFmt = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string dir = System.IO.Path.Combine(Definiotion.SelectedDir, currDateTimeFmt);
                    GlobalMethodClass.GuaranteeDirectoryExist(dir);
                    summaryOptions.GroupFolderPath = dir;
                }

                Macromill.QCWeb.ReportRequest.Request req = new Macromill.QCWeb.ReportRequest.Request(id, divNameLis, groupingSectorCount, surveyTitle, companyName, zeroshowcd,
                mergeaxis, summaryOptions.Reportprefix, summaryOptions.Xlbooknameprefix, summaryOptions.Tabletype, summaryOptions.Tableorientation,
                summaryOptions.Pagesetuptabletype, summaryOptions.Minsamplescountonmarking, summaryOptions.Markingtype, summaryOptions.Significancetestlevel,
                (Macromill.QCWeb.Common.XlPaperSize)summaryOptions.Papersize, (Macromill.QCWeb.Common.XlPageOrientation)summaryOptions.Paperorientation,
                summaryOptions.Tablesononesheet, summaryOptions.Level2highcolorindex, summaryOptions.Level1highcolorindex,
                summaryOptions.Level1lowcolorindex, summaryOptions.Level2lowcolorindex, summaryOptions.Level1percent, summaryOptions.Level2percent,
                summaryOptions.ShowNACode1, summaryOptions.ShowIVCode1, Macromill.QCWeb.ReportRequest.WBSettingCode.WBOff, summaryOptions.FilteringExpression1, true, false);

                //Macromill.QCWeb.ReportRequest.Reportsets.Reportset reportset = (Macromill.QCWeb.ReportRequest.Reportsets.Reportset)req.Reportsets[Convert.ToString(id)];
                //Macromill.QCWeb.ReportRequest.Outputs.OutputCross cross = (Macromill.QCWeb.ReportRequest.Outputs.OutputCross)reportset.Outputs[0];

                bool[] filterringFlag = null;
                if (summaryOptions.HasFilter)
                {
                    if (!CrossTabulationQC.checkVariableFilter(summaryOptions.Filters, Definiotion.VariableDictionary))
                    {
                        OnWorkerComplete(currentProgress, LocalResource.ST_SUMMARY_CALC, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.GT_INVALID_FILTER_SETTINGS, Enums.MessageType.ErrorOk, workBook);
                        return;
                    }
                    string filterExp = CriteriaDescProvider.CreateCriteriaDescriptions(summaryOptions.Filters, questions);
                    filterringFlag = new Criteria(filterExp, "", questions).Filtering(DBHelper.GetConnectionString(workBook), tableName: tableName);
                    summaryOptions.FilteringExpression1 = filterExp;
                    filterExp = CriteriaDescProvider.CreateCriteriaDescriptionsForLocalExp(summaryOptions.Filters, questions);
                    summaryOptions.LocalizedFilteringExpression1 = CriteriaDescProvider.LocalizeFilteringExpression(filterExp, req, questions, true);
                }

                List<string> tsvPaths = new List<string>();
                List<string> tsvPathsTmp = new List<string>();
                List<List<string>> tsvPathsDiv = new List<List<string>>();

                double prgrsCalc1 = 30 / ttDivCnt;
                double currentPrgrsCalc = currentProgress;
                int idReportSet = 1;
                var NTableDict = new Dictionary<string, int>()
                {
                    ["SUM"] = 5,
                    ["AVG"] = 6,
                    ["SD"] = 7,
                    ["MIN"] = 8,
                    ["MAX"] = 9,
                    ["MED"] = 10
                };
                var NTableItemNames = new Dictionary<string, string>()
                {
                    ["SUM"] = LocalResource.REPORT_SUMMARY_DESCRIPTION_DEFAULT,
                    ["AVG"] = LocalResource.REPORT_AVERAGE_DESCRIPTION_DEFAULT,
                    ["SD"] = LocalResource.REPORT_DEVIATION_DESCRIPTION_DEFAULT,
                    ["MIN"] = LocalResource.REPORT_MINIMUM_DESCRIPTION_DEFAULT,
                    ["MAX"] = LocalResource.LABEL_MAX_INTEGRATE,
                    ["MED"] = LocalResource.MULTI_PSM_MEDIAN
                };

                if (errorDiv)
                {
                    List<string> tsvPath = new List<string>();
                    tsvPathsDiv.Add(tsvPath);
                    writeErrorFile("div", errMsg, workBook, summaryOptions.GroupFolderPath);
                }
                else
                {
                    foreach (KeyValuePair<String, List<List<SummaryTableInput>>> dct in stTableSetsDict)
                    {
                        Macromill.QCWeb.ReportRequest.Reportsets.Reportset reportset = null;

                        if (dct.Key.StartsWith("div", StringComparison.OrdinalIgnoreCase))
                        {
                            idReportSet++;

                            if (null == group)
                            {
                                tsvPaths.Add(tsvPathJoined);
                            }
                            else
                            {
                                foreach (string s in tsvPathsTmp)
                                {
                                    tsvPaths.Add(s);
                                }
                            }
                            if (tsvPaths.Count == 0)
                            {
                                tsvPaths.Add("");
                            }

                            tsvPathsDiv.Add(tsvPaths);
                            tsvPaths = new List<string>();

                            tsvPathJoined = "";
                            continue;
                        }
                        tsvPathsTmp.Clear();
                        List<List<SummaryTableInput>> stTableSets = dct.Value;

                        Dictionary<String, DataWithMarking[,]> summaryDict = new Dictionary<String, DataWithMarking[,]>();
                        Dictionary<String, DataWithMarking[,]> summaryTotalDict = new Dictionary<String, DataWithMarking[,]>();
                        Dictionary<String, DataWithMarking[,]> summaryUnweightedTotalDict = new Dictionary<String, DataWithMarking[,]>();
                        Dictionary<String, DataWithMarking[,]> summaryValidlDict = new Dictionary<String, DataWithMarking[,]>();
                        Dictionary<string, DataWithMarking[][,]> summaryListDict = new Dictionary<string, DataWithMarking[][,]>();
                        Dictionary<string, DataWithMarking[][,]> summaryListTotalDict = new Dictionary<string, DataWithMarking[][,]>();
                        Dictionary<string, DataWithMarking[][,]> summaryListUnweightedTotalDict = new Dictionary<string, DataWithMarking[][,]>();
                        Dictionary<string, DataWithMarking[][,]> summaryListValidTotalDict = new Dictionary<string, DataWithMarking[][,]>();

                        Dictionary<String, double> totalValDict = new Dictionary<String, double>();
                        Dictionary<String, double> numberofValidDict = new Dictionary<String, double>();

                        Dictionary<String, Question> qstnDict = new Dictionary<String, Question>();
                        Dictionary<String, Question> axisDict = new Dictionary<String, Question>();
                        List<SummaryReader.SummaryTableInput> stTableSetItemsTmp = null;
                        Question qstnTmp = null;
                        string tableTitle = null;
                        int dataStartIndex = 4;
                        int addColumnCount = 4;
                        bool isNQ = false;
                        bool hasWeight = false;
                        int addRows = 0;
                        string summaryName = "";
                        bool hasCount = false;
                        bool hasLower = true;
                        bool totalCntDiff = false;
                        bool[] totalCntDiffGrp = new bool[groupingSectorCount];
                        double totalCnt = -1;
                        double validnumCnt = -1;
                        double totalCntPrev = -1;
                        double validnumCntPrev = -1;

                        DataWithMarking dmNAnwer = new DataWithMarking(LocalResource.REPORT_NA_DESCRIPTION_KEYWORD, false);
                        DataWithMarking dmNA = new DataWithMarking(LocalResource.REPORT_IV_DESCRIPTION_KEYWORD, false);
                        DataWithMarking dmWAP = new DataWithMarking(LocalResource.ST_WEIGHTED_AVERAGE_NUMBER, false);
                        DataWithMarking dmWA = new DataWithMarking(LocalResource.ST_AGGRAVATED_AVERAGE, false);
                        DataWithMarking dmZ = new DataWithMarking("0", false);
                        DataWithMarking dmN = new DataWithMarking(null, false);
                        DataWithMarking dmHf = new DataWithMarking("-", false);

                        double prgrsCalc2 = prgrsCalc1 / stTableSets.Count;

                        foreach (List<SummaryReader.SummaryTableInput> stTableSetItems in stTableSets)
                        {
                            stTableSetItemsTmp = stTableSetItems;
                            if (stTableSetItems.Count == 0)
                            {
                                continue;
                            }

                            if (reportset == null)
                            {
                                reportset = (Macromill.QCWeb.ReportRequest.Reportsets.Reportset)req.Reportsets[Convert.ToString(idReportSet)];
                            }

                            //DataWithMarking[][,] tabulationArray = new DataWithMarking[stTableSetItems.Count][,];
                            Question qstn = null;
                            int keyIncr = 0;
                            List<Data> dataList = null;
                            QuestionType targetQtype = 0;
                            int subTotalCount = 0;
                            foreach (SummaryReader.SummaryTableInput stTableSet in stTableSetItems)
                            {
                                hasLower = true;
                                if (bgWorker.CancellationPending) return;
                                keyIncr++;
                                DataWithMarking[,] result;
                                DataWithMarking[,] resultST;
                                DataWithMarking[,] resultTotalST;
                                DataWithMarking[,] resultUnweightedTotalST;
                                DataWithMarking[,] resultValidCasesST;
                                DataWithMarking[][,] resultList = null;
                                DataWithMarking[][,] resultListST = null;
                                DataWithMarking[][,] resultListTotalST = null;
                                DataWithMarking[][,] resultListUnweightedTotalST = null;
                                DataWithMarking[][,] resultListValidCasesST = null;
                                //DataWithMarking dmHf = new DataWithMarking("-", false);
                                qstn = null;
                                Question axis1 = null;
                                Question axis2 = null;
                                string targetVaraible = stTableSet.target;
                                string axis1Varaible = stTableSet.axis1;
                                int answerIndex = 3;
                                int answerPosition;
                                summaryName = stTableSet.summaryName;
                                bool isNumeric = int.TryParse(stTableSet.position, out answerPosition);

                                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                                {
                                    con.Open();
                                    QuestionSettings qstnDet = Definiotion.VariableDictionary[targetVaraible];
                                    qstn = (Question)questions[qstnDet.Id];
                                    if (null == qstnTmp)
                                    {
                                        qstnTmp = qstn;
                                    }
                                    //if (qstn == null) {
                                    //    throw 
                                    //}

                                    if (null == tableTitle)
                                    {
                                        if ((qstn.QuestionType & QuestionType.N) != QuestionType.N && isNumeric)
                                        {
                                            if (qstnDet.Choices.Count > 0)
                                            {
                                                tableTitle = qstnDet.Choices[answerPosition - 1] + ST_STR_LIST;
                                            }
                                        }
                                    }

                                    if (isNumeric)
                                    {
                                        isNQ = false;
                                        hasWeight = false;
                                        answerIndex += answerPosition;
                                        summaryName = "DEF&" + summaryName;
                                    }
                                    else
                                    {
                                        string sk = stTableSet.position;
                                        if (sk.StartsWith("ST"))
                                        {
                                            isNQ = false;
                                            hasWeight = false;
                                            int stAddCl = 1;
                                            if ("ST2" == sk) { stAddCl = 2; }
                                            else if ("ST3" == sk) { stAddCl = 3; }
                                            else if ("ST4" == sk) { stAddCl = 4; }
                                            else if ("ST5" == sk) { stAddCl = 5; }
                                            else if ("ST6" == sk) { stAddCl = 6; }
                                            else if ("ST7" == sk) { stAddCl = 7; }
                                            else if ("ST8" == sk) { stAddCl = 8; }
                                            else if ("ST9" == sk) { stAddCl = 9; }
                                            else if ("ST10" == sk) { stAddCl = 10; }
                                            answerIndex = answerIndex + qstnDet.CategoryCount + stAddCl;
                                            summaryName = "ST&" + summaryName;
                                            //tableTitle = qstnTmp.Name + "\nの小計" + stAddCl;
                                            if (null == tableTitle && qstnDet.SubTotals.Count >= stAddCl && null != qstnDet.SubTotals[stAddCl - 1])
                                            {
                                                tableTitle = qstnDet.SubTotals[stAddCl - 1].Subtotal + ST_STR_LIST;
                                            }
                                        }
                                        else if ("WT" == sk || "CT" == sk)
                                        {
                                            isNQ = false;
                                            hasWeight = true;
                                            int stCntTmp = !String.IsNullOrEmpty(qstnDet.AddSubTotal) ? qstnDet.SubTotalCount : 0;
                                            answerIndex = answerIndex + qstnDet.CategoryCount + stCntTmp + 4;
                                            summaryName = "CNT&" + summaryName;
                                            tableTitle = LocalResource.REPORT_COUNT_AVERAGE_KEYWORD + ST_STR_LIST;
                                            if ("WT" == sk)
                                            {
                                                tableTitle = LocalResource.REPORT_WEIGHT_AVERAGE_KEYWORD + ST_STR_LIST;
                                            }
                                        }
                                        else
                                        {
                                            isNQ = true;
                                            hasWeight = false;
                                            addRows = 1;
                                            answerIndex = NTableDict[stTableSet.position];
                                            summaryName = stTableSet.position + "&" + summaryName;
                                            tableTitle = NTableItemNames[stTableSet.position] + ST_STR_LIST;
                                        }
                                    }

                                    bool TestFlag = summaryOptions.TestFlag1;
                                    if (((qstn.QuestionType & QuestionType.N) == QuestionType.N || summaryName.StartsWith("CNT"))
                                        && summaryOptions.TestCode == Macromill.QCWeb.ReportRequest.SignificanceTestCode.Off)
                                    {
                                        TestFlag = false;
                                    }

                                    string[] weightArray = null;
                                    hasLower = true;
                                    if (qstnDet.Count.Length != 0)
                                    {
                                        bool lower = false;
                                        hasCount = true;
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
                                            if (criteriaSectorList.Contains(ct))
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
                                        if (weightArray.Length == 0) weightArray = null;
                                    }

                                    System.Data.DataTable dataTble = new System.Data.DataTable();

                                    if (dataList == null || dataList.Count == 0)
                                    {
                                        targetQtype = qstn.QuestionType;
                                        if (checkQuestion(qstnDet, workBook) &&
                                            (groupDataList == null || groupDataList != null && groupDataList.Count > 0))
                                        {
                                            if (!String.IsNullOrEmpty(qstnDet.AddSubTotal) && qstnDet.SubTotalCount > 0)
                                            {
                                                Qc4Launcher.Logic.CrossTabulationQC crossTabulationQC = new Qc4Launcher.Logic.CrossTabulationQC();
                                                dataList = crossTabulationQC.ReadSubTotalData(qstnDet, qstn, workBook, con, tableName);
                                                targetQtype = qstn.QuestionType & ~QuestionType.SA;
                                                targetQtype = targetQtype | QuestionType.MA;
                                                subTotalCount = qstnDet.SubTotalCount;
                                            }
                                            else
                                            {
                                                if (qstnDet.QuestionFlag == "An")
                                                    dataTble = DBHelper.GetDataTable("Select m." + qstn.ColumnName + " from multivariate m join " + tableName + " a on m.sort_no = a.sort_no order by a.sort_no ", con);
                                                else
                                                    dataTble = DBHelper.GetDataTable("Select " + qstn.ColumnName + " from  " + tableName + " order by sort_no ", con);
                                                dataList = ReadTextFile.ReadDataTable(dataTble, qstn.QuestionType, null, out ex);
                                            }
                                        }
                                        else
                                        {
                                            dataList = ReadTextFile.ReadDataTable(dataTble, qstn.QuestionType, null, out ex);
                                            subTotalCount = !String.IsNullOrEmpty(qstnDet.AddSubTotal) ? qstnDet.SubTotalCount : 0;
                                        }
                                    }

                                    //System.Data.DataTable dataTble = DBHelper.GetDataTable("Select q_" + qstnDet.Id + " from " + tableName, con);
                                    //List<Data> dataList = ReadTextFile.ReadDataTable(dataTble, qstn.QuestionType, null, out ex);

                                    qstnDet = Definiotion.VariableDictionary[axis1Varaible];
                                    axis1 = (Question)questions[qstnDet.Id];

                                    List<List<Data>> axesDatga = new List<List<Data>>();
                                    if (checkQuestion(qstnDet, workBook) && dataList.Count > 0)//qstndet wrong
                                    {
                                        if (qstnDet.QuestionFlag == "An")
                                            dataTble = DBHelper.GetDataTable("Select m." + axis1.ColumnName + " from multivariate m join " + tableName + " a on m.sort_no = a.sort_no order by a.sort_no ", con);
                                        else
                                            dataTble = DBHelper.GetDataTable("Select " + axis1.ColumnName + " from  " + tableName + " order by sort_no ", con);
                                    }
                                    else
                                    {
                                        dataTble = new System.Data.DataTable();
                                        dataList = ReadTextFile.ReadDataTable(dataTble, qstn.QuestionType, null, out ex);
                                    }

                                    List<Data> dataList2 = ReadTextFile.ReadDataTable(dataTble, axis1.QuestionType, null, out ex);
                                    axesDatga.Add(dataList2);

                                    if (group == null)
                                    {
                                        QCWebException ex2 = CrossTabulation.GetCrossArray(
                                            targetQtype,
                                            CrossTabulationQC.GetCategoryArray(qstn, true),
                                            dataList,
                                            CrossTabulationQC.GetAxisQTypeList(axis1, axis2),
                                            CrossTabulationQC.GetAxisQuestionTitle(axis1, axis2),
                                            CrossTabulationQC.GetAxisCategoryList(axis1, axis2),
                                            axesDatga,
                                            out result, translation,
                                            summaryOptions.TabulationDescriptions,
                                            filterringFlag,
                                            summaryOptions.WBDataList,
                                            weightArray,
                                            summaryOptions.Level1percent,
                                            summaryOptions.Level2percent,
                                            true, //summaryOptions.ShowNoAnswerAxis || summaryOptions.TabulateFullQuantity1, 
                                            false,
                                            GlobalTabulation.MarkingTotal.Subtotal,
                                            summaryOptions.TabulateFullQuantity1, IVtoNA: summaryOptions.TabulateFullQuantity1, locale: lccd, CutNA: summaryOptions.ShowNoAnswerItem, // need to cehck
                                            SignificanceTestOn: TestFlag, significanceTestCode: summaryOptions.TestCode, significanceTestLevel: summaryOptions.TestLevels.ToArray(),
                                            SignificanceTestLogFilePath: sigLog, qName: qstn.Name,
                                            axisQName: CrossTabulationQC.GetAxisQuestionName(axis1, axis2),
                                            hasCount: hasCount, subTotalCnt: subTotalCount, qTypeOr: qstn.QuestionType, hasLower: hasLower);

                                        //DataWithMarking dmTC = (DataWithMarking)result.GetValue(3, 3);
                                        //totalCnt = dmTC.NumValue;
                                        //if (-1 != totalCntPrev)
                                        //{
                                        //    if (totalCnt != totalCntPrev)
                                        //    {
                                        //        totalCntDiff = true;
                                        //    }
                                        //}
                                        //totalCntPrev = totalCnt;

                                        string keyName = axis1Varaible + keyIncr;
                                        hasValidCases = false;
                                        int validCaseIndex = 0;
                                        if (stTableSet.position == "AVG" || stTableSet.position == "SUM" ||
                                                    stTableSet.position == "MIN" || stTableSet.position == "MAX" ||
                                                    stTableSet.position == "SD" || stTableSet.position == "MED")
                                        {
                                            validCaseIndex = 4;
                                            hasValidCases = true;
                                        }
                                        else if (stTableSet.position == "WT" || stTableSet.position == "CT")
                                        {
                                            validCaseIndex = result.GetLength(1) - 2;
                                            hasValidCases = true;
                                        }

                                        if (!totalCntDiff)
                                        {
                                            for (int r = 3; r < result.GetUpperBound(0) - 1; r++)
                                            {
                                                DataWithMarking dmTC = (stTableSet.position == "AVG" || stTableSet.position == "SUM" ||
                                                    stTableSet.position == "MIN" || stTableSet.position == "MAX" ||
                                                    stTableSet.position == "SD" || stTableSet.position == "MED")
                                                    ? (DataWithMarking)result.GetValue(r, 4)
                                                    : (DataWithMarking)result.GetValue(r, 3);
                                                totalCnt = dmTC.NumValue;
                                                string keyName2 = keyName + "_" + r;
                                                if (stTableSet.position == "WT" || stTableSet.position == "CT")
                                                {
                                                    DataWithMarking dmTCnnumValid = (DataWithMarking)result.GetValue(r, result.GetLength(1) - 2);
                                                    validnumCnt = dmTCnnumValid.NumValue;
                                                    if (numberofValidDict.ContainsKey(keyName2))
                                                    {
                                                        validnumCntPrev = numberofValidDict[keyName2];
                                                        if (validnumCnt != validnumCntPrev)
                                                        {
                                                            totalCntDiff = true;
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        numberofValidDict[keyName2] = validnumCnt;
                                                    }
                                                }
                                                else if (totalValDict.ContainsKey(keyName2))
                                                {
                                                    totalCntPrev = totalValDict[keyName2];
                                                    if (totalCnt != totalCntPrev)
                                                    {
                                                        totalCntDiff = true;
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    totalValDict[keyName2] = totalCnt;
                                                }
                                            }
                                        }

                                        if (summaryDict.ContainsKey(keyName))
                                        {
                                            // TODO dont copy if both position are same.
                                            DataWithMarking[,] dt = summaryDict[keyName];
                                            for (int k = 0; k < result.GetLength(0); k++)
                                            {
                                                if (0 == k)
                                                {
                                                    DataWithMarking dmAxis = new DataWithMarking(qstn.Description, false);
                                                    dt.SetValue(dmAxis, k, dataStartIndex);
                                                }
                                                else if (1 == k)
                                                {
                                                    dt.SetValue(dmN, k, dataStartIndex);
                                                }
                                                else
                                                {
                                                    dt.SetValue(result.GetValue(k, answerIndex), k, dataStartIndex);
                                                }
                                            }

                                            // ***** Create total table data *****
                                            DataWithMarking[,] dtTotal = summaryTotalDict[keyName];
                                            DataWithMarking[,] dtUnweightedTotal = summaryUnweightedTotalDict[keyName];
                                            DataWithMarking[,] dtValidCase = summaryValidlDict[keyName];
                                            for (int k = 0; k < result.GetLength(0); k++)
                                            {
                                                if (0 == k)
                                                {
                                                    DataWithMarking dmAxis = new DataWithMarking(qstn.Description, false);
                                                    dtTotal.SetValue(dmAxis, k, dataStartIndex);
                                                    dtUnweightedTotal.SetValue(dmAxis, k, dataStartIndex);
                                                    dtValidCase.SetValue(dmAxis, k, dataStartIndex);
                                                }
                                                else
                                                {
                                                    DataWithMarking tp = (DataWithMarking)result.GetValue(k, 3);
                                                    tp.Percent = tp.NumValue;
                                                    dtTotal.SetValue(tp, k, dataStartIndex);
                                                    DataWithMarking wtp = (DataWithMarking)result.GetValue(k, 2);
                                                    wtp.Percent = wtp.NumValue;
                                                    dtUnweightedTotal.SetValue(wtp, k, dataStartIndex);
                                                    if (hasValidCases)
                                                    {
                                                        DataWithMarking vlp = (DataWithMarking)result.GetValue(k, validCaseIndex);
                                                        vlp.Percent = vlp.NumValue;
                                                        dtValidCase.SetValue(vlp, k, dataStartIndex);
                                                    }
                                                }
                                            }
                                            //dataStartIndex++;
                                        }
                                        else
                                        {
                                            // TODO dont copy if both position are same.
                                            resultST = new DataWithMarking[result.GetLength(0), result.GetLength(1) > MAX_TARGET_COUNT ? result.GetLength(1) : MAX_TARGET_COUNT];
                                            for (int r = 0; r < result.GetLength(0); r++)
                                            {
                                                for (int s = 0; s < result.GetLength(1); s++)
                                                {
                                                    resultST.SetValue(result.GetValue(r, s), r, s);
                                                }
                                            }
                                            //resultST = result.Clone() as DataWithMarking[,];
                                            for (int k = 0; k < result.GetLength(0); k++)
                                            {
                                                if (0 == k)
                                                {
                                                    DataWithMarking dmAxis = new DataWithMarking(qstn.Description, false);
                                                    resultST.SetValue(dmAxis, k, dataStartIndex);
                                                }
                                                else if (1 == k)
                                                {
                                                    resultST.SetValue(dmN, k, dataStartIndex);
                                                }
                                                else
                                                {
                                                    resultST.SetValue(result.GetValue(k, answerIndex), k, dataStartIndex);
                                                }
                                            }
                                            //dataStartIndex++;
                                            summaryDict[keyName] = resultST;
                                            qstnDict[keyName] = qstn;
                                            axisDict[keyName] = axis1;

                                            // ***** Create total table data *****
                                            resultTotalST = new DataWithMarking[result.GetLength(0), result.GetLength(1) > MAX_TARGET_COUNT ? result.GetLength(1) : MAX_TARGET_COUNT];
                                            resultUnweightedTotalST = new DataWithMarking[result.GetLength(0), result.GetLength(1) > MAX_TARGET_COUNT ? result.GetLength(1) : MAX_TARGET_COUNT];
                                            resultValidCasesST = new DataWithMarking[result.GetLength(0), result.GetLength(1) > MAX_TARGET_COUNT ? result.GetLength(1) : MAX_TARGET_COUNT];
                                            for (int r = 0; r < result.GetLength(0); r++)
                                            {
                                                for (int s = 0; s < 4; s++)
                                                {
                                                    //DataWithMarking tm = (DataWithMarking)result.GetValue(r, s);
                                                    //if (r > 1 && s > 1)
                                                    //{
                                                    //    tm = dmHf;
                                                    //}
                                                    resultTotalST.SetValue(result.GetValue(r, s), r, s);
                                                    resultUnweightedTotalST.SetValue(result.GetValue(r, s), r, s);
                                                    resultValidCasesST.SetValue(result.GetValue(r, s), r, s);
                                                }
                                            }
                                            for (int k = 0; k < result.GetLength(0); k++)
                                            {
                                                if (0 == k)
                                                {
                                                    DataWithMarking dmAxis = new DataWithMarking(qstn.Description, false);
                                                    resultTotalST.SetValue(dmAxis, k, dataStartIndex);
                                                    resultUnweightedTotalST.SetValue(dmAxis, k, dataStartIndex);
                                                    resultValidCasesST.SetValue(dmAxis, k, dataStartIndex);
                                                }
                                                else
                                                {
                                                    DataWithMarking tp = (DataWithMarking)result.GetValue(k, 3);
                                                    tp.Percent = tp.NumValue;
                                                    resultTotalST.SetValue(tp, k, dataStartIndex);
                                                    DataWithMarking wtp = (DataWithMarking)result.GetValue(k, 2);
                                                    wtp.Percent = wtp.NumValue;
                                                    resultUnweightedTotalST.SetValue(wtp, k, dataStartIndex);
                                                    if (hasValidCases)
                                                    {
                                                        DataWithMarking vlp = (DataWithMarking)result.GetValue(k, validCaseIndex);
                                                        vlp.Percent = vlp.NumValue;
                                                        resultValidCasesST.SetValue(vlp, k, dataStartIndex);
                                                    }
                                                }
                                            }
                                            summaryTotalDict[keyName] = resultTotalST;

                                            summaryUnweightedTotalDict[keyName] = resultUnweightedTotalST;
                                            summaryValidlDict[keyName] = resultValidCasesST;

                                        }
                                    }
                                    else
                                    {
                                        QCWebException ex2 = CrossTabulation.GetCrossArray(
                                            targetQtype,
                                            CrossTabulationQC.GetCategoryArray(qstn, true),
                                            dataList,
                                            CrossTabulationQC.GetAxisQTypeList(axis1, axis2),
                                            CrossTabulationQC.GetAxisQuestionTitle(axis1, axis2),
                                            CrossTabulationQC.GetAxisCategoryList(axis1, axis2),
                                            axesDatga,
                                            group.QuestionType,
                                            CrossTabulationQC.GetCategoryArray(group),
                                            dataList.Count > 0 ? groupDataList : groupDataListEmpty,
                                            out resultList, translation,
                                            summaryOptions.TabulationDescriptions,
                                            filterringFlag,
                                            summaryOptions.WBDataList,
                                            weightArray,
                                            summaryOptions.Level1percent,
                                            summaryOptions.Level2percent,
                                            true, //summaryOptions.ShowNoAnswerAxis || summaryOptions.TabulateFullQuantity1, 
                                            false,
                                            GlobalTabulation.MarkingTotal.Subtotal,
                                            summaryOptions.TabulateFullQuantity1, IVtoNA: summaryOptions.TabulateFullQuantity1, locale: lccd, CutNA: summaryOptions.ShowNoAnswerItem, // need to cehck
                                            SignificanceTestOn: TestFlag, significanceTestCode: summaryOptions.TestCode, significanceTestLevel: summaryOptions.TestLevels.ToArray(),
                                            SignificanceTestLogFilePath: sigLog, qName: qstn.Name, keyQName: group.Name,
                                            axisQName: CrossTabulationQC.GetAxisQuestionName(axis1, axis2),
                                            hasCount: hasCount, subTotalCnt: subTotalCount, qTypeOr: qstn.QuestionType, hasLower: hasLower);

                                        string keyName = axis1Varaible + keyIncr;
                                        hasValidCases = false;
                                        int validCaseIndex = 0;
                                       

                                        if (resultList.Length > 0)
                                        {
                                            for (int g = 0; g < resultList.Length; g++)
                                            {
                                                if (stTableSet.position == "AVG" || stTableSet.position == "SUM" ||
                                                   stTableSet.position == "MIN" || stTableSet.position == "MAX" ||
                                                   stTableSet.position == "SD" || stTableSet.position == "MED")
                                                {
                                                    validCaseIndex = 4;
                                                    hasValidCases = true;
                                                }
                                                else if (stTableSet.position == "WT" || stTableSet.position == "CT")
                                                {
                                                    validCaseIndex = resultList[g].GetLength(1) - 2;
                                                    hasValidCases = true;
                                                }
                                                if (!totalCntDiffGrp[g])
                                                {
                                                    for (int r = 3; r < resultList[g].GetUpperBound(0) - 1; r++)
                                                    {
                                                        DataWithMarking dmTC = (stTableSet.position == "AVG" || stTableSet.position == "SUM" ||
                                                            stTableSet.position == "MIN" || stTableSet.position == "MAX" ||
                                                            stTableSet.position == "SD" || stTableSet.position == "MED")
                                                            ? (DataWithMarking)resultList[g].GetValue(r, 4)
                                                            : (DataWithMarking)resultList[g].GetValue(r, 3);
                                                        totalCnt = dmTC.NumValue;
                                                        string keyName2 = keyName + "_" + g + "_" + r;
                                                        if (stTableSet.position == "WT" || stTableSet.position == "CT")
                                                        {
                                                            DataWithMarking dmTCnnumValid = (DataWithMarking)resultList[g].GetValue(r, resultList[g].GetLength(1) - 2);
                                                            validnumCnt = dmTCnnumValid.NumValue;
                                                            if (numberofValidDict.ContainsKey(keyName2))
                                                            {
                                                                validnumCntPrev = numberofValidDict[keyName2];
                                                                if (validnumCnt != validnumCntPrev)
                                                                {
                                                                    totalCntDiffGrp[g] = true;
                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                numberofValidDict[keyName2] = validnumCnt;
                                                            }
                                                        }
                                                        else if (totalValDict.ContainsKey(keyName2))
                                                        {
                                                            totalCntPrev = totalValDict[keyName2];
                                                            if (totalCnt != totalCntPrev)
                                                            {
                                                                totalCntDiffGrp[g] = true;
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            totalValDict[keyName2] = totalCnt;
                                                        }
                                                    }
                                                }
                                            }
                                        }


                                        if (summaryListDict.ContainsKey(keyName))
                                        {
                                            resultListST = summaryListDict[keyName];
                                            for (int x = 0; x < resultListST.Length; x++)
                                            {
                                                DataWithMarking[,] dt = resultListST[x];
                                                DataWithMarking[,] dtNew = resultList[x];
                                                for (int k = 0; k < dtNew.GetLength(0); k++)
                                                {
                                                    if (0 == k)
                                                    {
                                                        DataWithMarking dmAxis = new DataWithMarking(qstn.Description, false);
                                                        dt.SetValue(dmAxis, k, dataStartIndex);
                                                    }
                                                    else if (1 == k)
                                                    {
                                                        dt.SetValue(dmN, k, dataStartIndex);
                                                    }
                                                    else
                                                    {
                                                        dt.SetValue(dtNew.GetValue(k, answerIndex), k, dataStartIndex);
                                                    }
                                                }
                                            }

                                            // ***** Create total table data *****
                                            resultListTotalST = summaryListTotalDict[keyName];
                                            resultListUnweightedTotalST = summaryListUnweightedTotalDict[keyName];
                                            resultListValidCasesST = summaryListValidTotalDict[keyName];
                                            for (int x = 0; x < resultListTotalST.Length; x++)
                                            {
                                                DataWithMarking[,] dtTotal = resultListTotalST[x];
                                                DataWithMarking[,] dtUnweightedTotal = resultListUnweightedTotalST[x];
                                                DataWithMarking[,] dtTotalNew = resultList[x];
                                                DataWithMarking[,] dtvalid = resultListValidCasesST[x];
                                                for (int k = 0; k < dtTotalNew.GetLength(0); k++)
                                                {
                                                    if (0 == k)
                                                    {
                                                        DataWithMarking dmAxis = new DataWithMarking(qstn.Description, false);
                                                        dtTotal.SetValue(dmAxis, k, dataStartIndex);
                                                        dtUnweightedTotal.SetValue(dmAxis, k, dataStartIndex);
                                                        dtvalid.SetValue(dmAxis, k, dataStartIndex);
                                                    }
                                                    else
                                                    {
                                                        DataWithMarking tp = (DataWithMarking)dtTotalNew.GetValue(k, 3);
                                                        tp.Percent = tp.NumValue;
                                                        dtTotal.SetValue(tp, k, dataStartIndex);
                                                        DataWithMarking wtp = (DataWithMarking)dtTotalNew.GetValue(k, 2);
                                                        wtp.Percent = wtp.NumValue;
                                                        dtUnweightedTotal.SetValue(wtp, k, dataStartIndex);
                                                        if (hasValidCases)
                                                        {
                                                            DataWithMarking wtp1 = (DataWithMarking)dtTotalNew.GetValue(k, validCaseIndex);
                                                            wtp1.Percent = wtp1.NumValue;
                                                            dtvalid.SetValue(wtp1, k, dataStartIndex);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DataWithMarking[][,] stFinalResult = new DataWithMarking[resultList.Length][,];
                                            for (int x = 0; x < resultList.Length; x++)
                                            {
                                                DataWithMarking[,] rst = resultList[x];
                                                resultST = new DataWithMarking[rst.GetLength(0), rst.GetLength(1) > MAX_TARGET_COUNT ? rst.GetLength(1) : MAX_TARGET_COUNT];
                                                for (int r = 0; r < rst.GetLength(0); r++)
                                                {
                                                    for (int s = 0; s < rst.GetLength(1); s++)
                                                    {
                                                        resultST.SetValue(rst.GetValue(r, s), r, s);
                                                    }
                                                }
                                                //resultST = result.Clone() as DataWithMarking[,];
                                                for (int k = 0; k < rst.GetLength(0); k++)
                                                {
                                                    if (0 == k)
                                                    {
                                                        DataWithMarking dmAxis = new DataWithMarking(qstn.Description, false);
                                                        resultST.SetValue(dmAxis, k, dataStartIndex);
                                                    }
                                                    else if (1 == k)
                                                    {
                                                        resultST.SetValue(dmN, k, dataStartIndex);
                                                    }
                                                    else
                                                    {
                                                        resultST.SetValue(rst.GetValue(k, answerIndex), k, dataStartIndex);
                                                    }
                                                }

                                                stFinalResult[x] = resultST;
                                            }
                                            summaryListDict[keyName] = stFinalResult;

                                            // ***** Create total table data *****
                                            resultListTotalST = new DataWithMarking[resultList.Length][,];
                                            resultListUnweightedTotalST = new DataWithMarking[resultList.Length][,];
                                            resultListValidCasesST = new DataWithMarking[resultList.Length][,];
                                            for (int x = 0; x < resultList.Length; x++)
                                            {
                                                DataWithMarking[,] rst = resultList[x];
                                                resultTotalST = new DataWithMarking[rst.GetLength(0), rst.GetLength(1) > MAX_TARGET_COUNT ? rst.GetLength(1) : MAX_TARGET_COUNT];
                                                resultUnweightedTotalST = new DataWithMarking[rst.GetLength(0), rst.GetLength(1) > MAX_TARGET_COUNT ? rst.GetLength(1) : MAX_TARGET_COUNT];
                                                resultValidCasesST = new DataWithMarking[rst.GetLength(0), rst.GetLength(1) > MAX_TARGET_COUNT ? rst.GetLength(1) : MAX_TARGET_COUNT];
                                                for (int r = 0; r < rst.GetLength(0); r++)
                                                {
                                                    for (int s = 0; s < 4; s++)
                                                    {
                                                        //DataWithMarking tm = (DataWithMarking)rst.GetValue(r, s);
                                                        //if (r > 1 && s > 1)
                                                        //{
                                                        //    tm = dmHf;
                                                        //}
                                                        resultTotalST.SetValue(rst.GetValue(r, s), r, s);
                                                        resultUnweightedTotalST.SetValue(rst.GetValue(r, s), r, s);
                                                        resultValidCasesST.SetValue(rst.GetValue(r, s), r, s);
                                                    }
                                                }
                                                for (int k = 0; k < rst.GetLength(0); k++)
                                                {
                                                    if (0 == k)
                                                    {
                                                        DataWithMarking dmAxis = new DataWithMarking(qstn.Description, false);
                                                        resultTotalST.SetValue(dmAxis, k, dataStartIndex);
                                                        resultUnweightedTotalST.SetValue(dmAxis, k, dataStartIndex);
                                                        resultValidCasesST.SetValue(dmAxis, k, dataStartIndex);
                                                    }
                                                    else
                                                    {
                                                        DataWithMarking tp = (DataWithMarking)rst.GetValue(k, 3);
                                                        tp.Percent = tp.NumValue;
                                                        resultTotalST.SetValue(tp, k, dataStartIndex);
                                                        DataWithMarking wtp = (DataWithMarking)rst.GetValue(k, 2);
                                                        wtp.Percent = wtp.NumValue;
                                                        resultUnweightedTotalST.SetValue(wtp, k, dataStartIndex);
                                                        if (hasValidCases)
                                                        {
                                                            DataWithMarking wtp1 = (DataWithMarking)rst.GetValue(k, validCaseIndex);
                                                            wtp1.Percent = wtp1.NumValue;
                                                            resultValidCasesST.SetValue(wtp1, k, dataStartIndex);
                                                        }
                                                    }
                                                }

                                                resultListTotalST[x] = resultTotalST;
                                                resultListUnweightedTotalST[x] = resultUnweightedTotalST;
                                                resultListValidCasesST[x] = resultValidCasesST;
                                            }
                                            summaryListTotalDict[keyName] = resultListTotalST;
                                            summaryListUnweightedTotalDict[keyName] = resultListUnweightedTotalST;
                                            summaryListValidTotalDict[keyName] = resultListValidCasesST;
                                        }
                                    }
                                }
                            }
                            dataStartIndex++;

                            currentProgress += prgrsCalc2;
                            OnWorkerComplete(currentProgress, LocalResource.ST_SUMMARY_CALC);
                        }

                        if (null == reportset)
                        {
                            continue;
                        }

                        DataWithMarking[][,] tabulationArray;
                        DataWithMarking[][,] tabulationArrayTotal = null;
                        DataWithMarking[][,] tabulationArrayWeigtedTotal = null;

                        //List<string> tsvPaths = new List<string>();

                        int ai = 0;
                        if (group == null)
                        {
                            if (totalCntDiff)
                            {
                                summaryName = "1&" + summaryName;
                            }
                            else
                            {
                                summaryName = "0&" + summaryName;
                            }
                            Macromill.QCWeb.ReportRequest.Outputs.OutputCross cross = (Macromill.QCWeb.ReportRequest.Outputs.OutputCross)reportset.Outputs[0];
                            ai = 0;
                            tabulationArray = new DataWithMarking[summaryDict.Count][,];
                            tabulationArrayWeigtedTotal = new DataWithMarking[summaryUnweightedTotalDict.Count][,];
                            foreach (KeyValuePair<string, DataWithMarking[,]> entry in summaryDict)
                            {
                                if (bgWorker.CancellationPending) return;
                                DataWithMarking[,] dw = entry.Value;
                                DataWithMarking[,] newDM = new DataWithMarking[dw.GetLength(0) + addRows, dataStartIndex + addColumnCount];

                                createCrossArrayST(ref dw, ref newDM, isNQ, addColumnCount, ref dmNA, ref dmN, ref dmZ,
                                    ref dmWAP, ref dmWA, ref dmNAnwer, hasWeight, ref dmHf, totalCntDiff);
                                tabulationArray[ai++] = newDM;
                                logOutput(newDM);
                            }

                            if (bgWorker.CancellationPending) return;
                            tabulationArray = ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryValidlDict, hasValidCases);
                            if (bgWorker.CancellationPending) return;

                            ai = 0;
                            foreach (KeyValuePair<string, DataWithMarking[,]> entry in summaryUnweightedTotalDict)
                            {
                                if (bgWorker.CancellationPending) return;
                                DataWithMarking[,] dw = entry.Value;
                                DataWithMarking[,] newDM = new DataWithMarking[dw.GetLength(0) + addRows, dataStartIndex + addColumnCount];
                                createCrossArrayST(ref dw, ref newDM, isNQ, addColumnCount, ref dmNA, ref dmN,
                                    ref dmZ, ref dmWAP, ref dmWA, ref dmNAnwer, hasWeight, ref dmHf, totalCntDiff);
                                tabulationArrayWeigtedTotal[ai++] = newDM;
                                logOutput(dw);
                                logOutput(newDM);
                            }

                            // if (totalCntDiff)
                            {
                                ai = 0;
                                tabulationArrayTotal = new DataWithMarking[summaryTotalDict.Count][,];
                                if (!hasValidCases)
                                {
                                    foreach (KeyValuePair<string, DataWithMarking[,]> entry in summaryTotalDict)
                                    {
                                        if (bgWorker.CancellationPending) return;
                                        DataWithMarking[,] dw = entry.Value;
                                        DataWithMarking[,] newDM = new DataWithMarking[dw.GetLength(0) + addRows, dataStartIndex + addColumnCount];

                                        createCrossArrayST(ref dw, ref newDM, isNQ, addColumnCount, ref dmNA, ref dmN,
                                            ref dmZ, ref dmWAP, ref dmWA, ref dmNAnwer, hasWeight, ref dmHf, totalCntDiff);

                                        tabulationArrayTotal[ai++] = newDM;

                                        logOutput(newDM);
                                        logOutput(dw);
                                    }
                                }
                                else
                                {
                                    foreach (KeyValuePair<string, DataWithMarking[,]> entry in summaryValidlDict)
                                    {
                                        if (bgWorker.CancellationPending) return;
                                        DataWithMarking[,] dw = entry.Value;
                                        DataWithMarking[,] newDM = new DataWithMarking[dw.GetLength(0) + addRows, dataStartIndex + addColumnCount];

                                        createCrossArrayST(ref dw, ref newDM, isNQ, addColumnCount, ref dmNA, ref dmN,
                                            ref dmZ, ref dmWAP, ref dmWA, ref dmNAnwer, hasWeight, ref dmHf, totalCntDiff);

                                        tabulationArrayTotal[ai++] = newDM;

                                        logOutput(newDM);
                                        logOutput(dw);
                                    }
                                }
                            }

                            using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                            {
                                con.Open();
                                Macromill.QCWeb.ReportRequest.Tables.CrossTable table = STableUtil.SetOutputRequestTableCross(cross, qstnTmp, tabulationArray, totalCntDiff ? tabulationArrayTotal : tabulationArrayTotal,
                                    stTableSetItemsTmp, questions, con, excludeCnt, summaryOptions.TabulateFullQuantity1, dataStartIndex - 4, summaryName, tableName, tableTitle, hasCount, summaryOptions.WBVariable, tabulationArrayWeigtedTotal);


                                if (totalCntDiff)
                                {
                                    Macromill.QCWeb.ReportRequest.Tables.CrossTable table2 = STableUtil.SetOutputRequestTableCross(cross, qstnTmp, tabulationArrayTotal, null,
                                        stTableSetItemsTmp, questions, con, excludeCnt, summaryOptions.TabulateFullQuantity1, dataStartIndex - 4, summaryName + "_TT", tableName, tableTitle, hasCount, summaryOptions.WBVariable, tabulationArrayWeigtedTotal);
                                }
                            }

                            tsvPathJoined = (cross.Tables as Macromill.QCWeb.ReportRequest.Tables).OutputToTSV();
                            //tsvPaths.Add(tsvPathJoined);
                        }
                        else
                        {
                            for (int loop = 1; loop <= groupingSectorCount; loop++)
                            {
                                string summaryNameGrp;
                                if (totalCntDiffGrp[loop - 1])
                                {
                                    summaryNameGrp = "1&" + summaryName;
                                }
                                else
                                {
                                    summaryNameGrp = "0&" + summaryName;
                                }
                                ai = 0;
                                Macromill.QCWeb.ReportRequest.Outputs.OutputCross cross = (Macromill.QCWeb.ReportRequest.Outputs.OutputCross)reportset.Outputs[loop - 1];
                                tabulationArray = new DataWithMarking[summaryListDict.Count][,];
                                foreach (KeyValuePair<string, DataWithMarking[][,]> entry in summaryListDict)
                                {
                                    if (bgWorker.CancellationPending) return;
                                    DataWithMarking[][,] rstArr = entry.Value;
                                    DataWithMarking[,] dw = rstArr[loop - 1];
                                    DataWithMarking[,] newDM = new DataWithMarking[dw.GetLength(0) + addRows, dataStartIndex + addColumnCount];

                                    createCrossArrayST(ref dw, ref newDM, isNQ, addColumnCount, ref dmNA, ref dmN,
                                        ref dmZ, ref dmWAP, ref dmWA, ref dmNAnwer, hasWeight, ref dmHf, totalCntDiffGrp[loop - 1]);
                                    tabulationArray[ai++] = newDM;
                                }

                                if (bgWorker.CancellationPending) return;
                                tabulationArray = ReplaceTotalValuesToValidCasesValues(tabulationArray, summaryListValidTotalDict, hasValidCases, loop);
                                if (bgWorker.CancellationPending) return;

                                ai = 0;
                                tabulationArrayWeigtedTotal = new DataWithMarking[summaryListUnweightedTotalDict.Count][,];

                                foreach (KeyValuePair<string, DataWithMarking[][,]> entry in summaryListUnweightedTotalDict)
                                {
                                    if (bgWorker.CancellationPending) return;
                                    DataWithMarking[][,] rstArr = entry.Value;
                                    DataWithMarking[,] dw = rstArr[loop - 1];
                                    DataWithMarking[,] newDM = new DataWithMarking[dw.GetLength(0) + addRows, dataStartIndex + addColumnCount];
                                    createCrossArrayST(ref dw, ref newDM, isNQ, addColumnCount, ref dmNA, ref dmN,
                                        ref dmZ, ref dmWAP, ref dmWA, ref dmNAnwer, hasWeight, ref dmHf, totalCntDiffGrp[loop - 1]);
                                    tabulationArrayWeigtedTotal[ai++] = newDM;
                                }


                                if (totalCntDiffGrp[loop - 1])
                                {
                                    ai = 0;
                                    tabulationArrayTotal = new DataWithMarking[summaryListTotalDict.Count][,];
                                    if (!hasValidCases)
                                    {
                                        foreach (KeyValuePair<string, DataWithMarking[][,]> entry in summaryListTotalDict)
                                        {
                                            if (bgWorker.CancellationPending) return;
                                            DataWithMarking[][,] rstArr = entry.Value;
                                            DataWithMarking[,] dw = rstArr[loop - 1];
                                            DataWithMarking[,] newDM = new DataWithMarking[dw.GetLength(0) + addRows, dataStartIndex + addColumnCount];

                                            createCrossArrayST(ref dw, ref newDM, isNQ, addColumnCount, ref dmNA, ref dmN,
                                                ref dmZ, ref dmWAP, ref dmWA, ref dmNAnwer, hasWeight, ref dmHf, totalCntDiffGrp[loop - 1]);
                                            tabulationArrayTotal[ai++] = newDM;
                                        }
                                    }
                                    else
                                    {
                                        foreach (KeyValuePair<string, DataWithMarking[][,]> entry in summaryListValidTotalDict)
                                        {
                                            if (bgWorker.CancellationPending) return;
                                            DataWithMarking[][,] rstArr = entry.Value;
                                            DataWithMarking[,] dw = rstArr[loop - 1];
                                            DataWithMarking[,] newDM = new DataWithMarking[dw.GetLength(0) + addRows, dataStartIndex + addColumnCount];

                                            createCrossArrayST(ref dw, ref newDM, isNQ, addColumnCount, ref dmNA, ref dmN,
                                                ref dmZ, ref dmWAP, ref dmWA, ref dmNAnwer, hasWeight, ref dmHf, totalCntDiffGrp[loop - 1]);
                                            tabulationArrayTotal[ai++] = newDM;
                                        }
                                    }
                                }

                                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                                {
                                    con.Open();
                                    Macromill.QCWeb.ReportRequest.Tables.CrossTable table = STableUtil.SetOutputRequestTableCross(cross, qstnTmp, tabulationArray, totalCntDiffGrp[loop - 1] ? tabulationArrayTotal : null,
                                       stTableSetItemsTmp, questions, con, excludeCnt, summaryOptions.TabulateFullQuantity1, dataStartIndex - 4, summaryNameGrp, tableName, tableTitle, hasCount, summaryOptions.WBVariable, tabulationArrayWeigtedTotal);
                                    if (group != null)
                                    {
                                        Sectors.Sector sector = (Sectors.Sector)group.Sectors[loop];
                                        table.SetKeyItemInformation(group.Name, group.Description2(), sector.Number, sector.Description);
                                    }

                                    if (totalCntDiffGrp[loop - 1])
                                    {
                                        Macromill.QCWeb.ReportRequest.Tables.CrossTable table2 = STableUtil.SetOutputRequestTableCross(cross, qstnTmp, tabulationArrayTotal, null,
                                       stTableSetItemsTmp, questions, con, excludeCnt, summaryOptions.TabulateFullQuantity1, dataStartIndex - 4, summaryNameGrp + "_TT", tableName, tableTitle, hasCount, summaryOptions.WBVariable, tabulationArrayWeigtedTotal);
                                        if (group != null)
                                        {
                                            Sectors.Sector sector = (Sectors.Sector)group.Sectors[loop];
                                            table2.SetKeyItemInformation(group.Name, group.Description2(), sector.Number, sector.Description);
                                        }
                                    }

                                    tsvPathJoined = (cross.Tables as Macromill.QCWeb.ReportRequest.Tables).OutputToTSV();
                                    //tsvPaths.Add(tsvPathJoined);
                                    tsvPathsTmp.Add(tsvPathJoined);
                                }
                            }
                        }
                        currentPrgrsCalc += prgrsCalc1;
                        currentProgress = currentPrgrsCalc;
                    }
                }

                currentProgress = 42;
                OnWorkerComplete(currentProgress, LocalResource.PB_EXCEL_GEN);

                //Request request = new Request();
                //request.MakeRequeszt(id, tsvPathsDiv, hasDiv ? divNameLis : null, surveyTitle, companyName, zeroshowcd, mergeaxis, summaryOptions.Reportprefix,
                //    summaryOptions.Xlbooknameprefix, summaryOptions.Tabletype, summaryOptions.Tableorientation, summaryOptions.Pagesetuptabletype, summaryOptions.Minsamplescountonmarking,
                //    summaryOptions.Markingtype, summaryOptions.Significancetestlevel, summaryOptions.Papersize,
                //    summaryOptions.Paperorientation, summaryOptions.Tablesononesheet, summaryOptions.Level2highcolorindex, summaryOptions.Level1highcolorindex,
                //    summaryOptions.Level1lowcolorindex, summaryOptions.Level2lowcolorindex, summaryOptions.Level1percent, summaryOptions.Level2percent,
                //    summaryOptions.ShowNACode1, summaryOptions.ShowIVCode1, summaryOptions.WBOn1, summaryOptions.LocalizedFilteringExpression1, false, false);
                _log.Info("Summary list calculation complted");
                _log.Info("Summary list excel creation started");

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

                int rptCnt = tsvPathsDiv.Count;
                double progressStepDiv = 57 / rptCnt;
                double currentProgressDiv = currentProgress;

                idReportSet = 0;
                foreach (List<string> tsvPathList in tsvPathsDiv) // div. Both row and col
                {
                    string divName = "";
                    if (hasDiv)
                    {
                        divName = divNameLis[idReportSet];
                    }
                    idReportSet++;

                    int i = 0;
                    double progressStepGrp = progressStepDiv / tsvPathList.Count;

                    foreach (string tsvPath in tsvPathList) // group
                    {
                        if (null == tsvPath || "" == tsvPath)
                        {
                            continue;
                        }
                        Request request = new Request();
                        request.MakeRequeszt(id, idReportSet, i, tsvPath, divName, surveyTitle, companyName, zeroshowcd, mergeaxis, summaryOptions.Reportprefix,
                            summaryOptions.Xlbooknameprefix, summaryOptions.Tabletype, summaryOptions.Tableorientation, summaryOptions.Pagesetuptabletype, summaryOptions.Minsamplescountonmarking,
                            summaryOptions.Markingtype, summaryOptions.Significancetestlevel, summaryOptions.Papersize,
                            summaryOptions.Paperorientation, summaryOptions.Tablesononesheet, summaryOptions.Level2highcolorindex, summaryOptions.Level1highcolorindex,
                            summaryOptions.Level1lowcolorindex, summaryOptions.Level2lowcolorindex, summaryOptions.Level1percent, summaryOptions.Level2percent,
                            summaryOptions.ShowNACode1, summaryOptions.ShowIVCode1, summaryOptions.WBOn1, summaryOptions.LocalizedFilteringExpression1, true, false);
                        i++;
                        foreach (Reportset rs in request.Reportsets.Values)
                        {
                            foreach (Output ouput in rs.Outputs.Values)
                            {
                                //int flag = 0;
                                //if (flag == 1)
                                //{
                                //    SummaryCreator crossCreator = new SummaryCreator();
                                //    crossCreator.CreateCross(ouput, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1), summaryOptions.GroupFolderPath,
                                //        System.AppContext.BaseDirectory, xlApp, bgWorker, QC: this,
                                //        progressAvailable: progressStepGrp, currentProgress: currentProgress, outputFiles: outputFiles);
                                //    if (bgWorker.CancellationPending) return;
                                //    OnWorkerComplete(currentProgress, LocalResource.PB_EXCEL_GEN);
                                //}
                                //else
                                //{
                                SummaryCreatorXml summaryCreator = new SummaryCreatorXml();
                                summaryCreator.CreateSummary(ouput, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1), summaryOptions.GroupFolderPath,
                                    System.AppContext.BaseDirectory, xlApp, bgWorker, QC: this,
                                    progressAvailable: progressStepGrp, currentProgress: currentProgress, outputFiles: outputFiles);
                                if (bgWorker.CancellationPending) return;
                                OnWorkerComplete(currentProgress, LocalResource.PB_EXCEL_GEN);
                                //}
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
                    OnWorkerComplete(100, LocalResource.ST_FINISHED_SUMMARY, true, true);
                    xlApp.EnableEvents = true;
                    xlApp.WindowState = XlWindowState.xlMaximized;// CrossTabulationQC.maximizeExcel(xlApp);
                    xlApp.Calculation = XlCalculation.xlCalculationAutomatic;
                    xlApp.ScreenUpdating = true;
                    xlApp.DisplayAlerts = true;
                    childExcelApp = (IntPtr)xlApp.Hwnd;
                    xlApp.Visible = true;
                }
                else
                {
                    OnWorkerComplete(100, LocalResource.ST_FINISHED_SUMMARY, true, true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.COMPLETED, Enums.MessageType.Info, workBook);
                    Process.Start(summaryOptions.GroupFolderPath);
                }

                _log.Info("Summary tab excel creation completed");
            }
            catch (Exception ex)
            {
                string exMsg = LocalResource.FAILED_TO_GENE_EXCEL;
                try
                {
                    _log.LogError(ex.Message);
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
                        if (summaryOptions != null && !String.IsNullOrEmpty(summaryOptions.WBVariable) && ex.Message.Contains("パラメータ '{0}' が不正です。:{1}"))
                            exMsg = LocalResource.QC_WEB_WEIGHTBACK_EXCEPTION + "\n" + summaryOptions.WBVariable;
                    }
                }
                finally
                {
                    MessageDialog.ShowMessageOnWorkBook(exMsg, Enums.MessageType.ErrorOk, workBook);
                    if (group == null && !hasDiv && excelOperate != null)
                    {
                        excelOperate.Dispose();
                    }
                }
            }
            finally
            {
                if (workSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref workSheet);
                }

                if (xlApp != null && group == null && !hasDiv || xlApp != null && bgWorker.CancellationPending)
                {
                    try
                    { COMWholeOperate.releaseComObject<Application>(ref xlApp); }
                    catch { }
                }
                deleteFiles();
                if ((group != null || hasDiv || bgWorker.CancellationPending) && excelOperate != null)
                {
                    if (excelOperate != null)
                    {
                        COMWholeOperate.releaseComObject(ref excelOperate);
                    }
                    //  excelOperate.Dispose();
                }
                if (bgWorker.CancellationPending)
                {
                    CrossTabulationQC.deleteOutPutFiles(outputFiles, summaryOptions == null ? false : summaryOptions.HasDiv, summaryOptions == null ? null : summaryOptions.GroupFolderPath);
                    OnWorkerComplete(100, LocalResource.ST_FINISHED_SUMMARY, true, true, true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.MSG_OUTPUT_ABORTED, Enums.MessageType.Info, workBook);
                    OnWorkerComplete(100, LocalResource.ST_FINISHED_SUMMARY, true, close: true);
                }
                else
                {
                    OnWorkerComplete(100, LocalResource.ST_FINISHED_SUMMARY);

                    try//Redmine id :193454
                    {


                        if (excelOperate != null)
                        {
                            COMWholeOperate.releaseComObject(ref excelOperate);
                        }
                        if (xlApp != null)
                        {
                            try
                            {
                                COMWholeOperate.releaseComObject(ref xlApp);
                            }
                            catch { }
                        }

                    }
                    catch (Exception e)
                    {
                        _log.LogError(e.Message + "\n" + e.StackTrace);
                    }
                }
                _log.Info("Summary Tabulate completed");
            }
        }
        /// <summary>
        /// Method to call the common method to replace the Total values as Valid cases values while the group is null
        /// </summary>
        /// <param name="tabulationArray">Summary Tabulation Array</param>
        /// <param name="summaryValidlDict">Summary valid case values Array</param>
        /// <param name="hasValidCases">bool value that specify the valid case exist or not</param>
        /// <returns>Replaced to Valid cases Array of Summary Tabulation</returns>
        /// <remarks>The implementation as per Redmine Id:234918</remarks>
        public DataWithMarking[][,] ReplaceTotalValuesToValidCasesValues(DataWithMarking[][,] tabulationArray, Dictionary<string, DataWithMarking[,]> summaryValidlDict, bool hasValidCases)
        {
            int ai = 0;
            if (hasValidCases)
            {
                foreach (KeyValuePair<string, DataWithMarking[,]> entry in summaryValidlDict)
                {
                    tabulationArray[ai] = ReplaceTotalValues(entry.Value, tabulationArray[ai]);
                    ai++;
                }
            }
            return tabulationArray;
        }
        /// <summary>
        /// Common method to replace the Total values as Valid cases values
        /// </summary>
        /// <param name="summaryValidlDict">Summary valid case values Array</param>
        /// <param name="tabulationArray">Summary Tabulation Array</param>
        /// <returns>Replaced to Valid cases Array of Summary Tabulation</returns>
        /// <remarks>The implementation as per Redmine Id:234918</remarks>
        public DataWithMarking[,] ReplaceTotalValues(DataWithMarking[,] summaryValidlDict, DataWithMarking[,] tabulationArray)
        {
            DataWithMarking[,] dw = summaryValidlDict;
            int tabuLen = tabulationArray.GetLength(0);
            int kv = tabuLen - dw.GetLength(0);
            for (int k = kv; k < dw.GetLength(0); k++)
            {
                if (k < 2)
                    continue;
                DataWithMarking tp = (DataWithMarking)dw.GetValue(k, 4);
                DataWithMarking NewtDM = new DataWithMarking(tp.NumValue.ToString());
                NewtDM.Percent = tp.Percent;
                tabulationArray.SetValue(NewtDM, k + kv, 3);
            }
            return tabulationArray;
        }
        /// <summary>
        /// Method to call the common method to replace the Total values as Valid cases values while the group is not null
        /// </summary>
        /// <param name="tabulationArray">Summary Tabulation Array</param>
        /// <param name="summaryValidlDict">Summary valid case values Array</param>
        /// <param name="hasValidCases">bool value that specify the valid case exist or not</param>
        /// <returns>Replaced to Valid cases Array of Summary Tabulation</returns>
        /// <remarks>The implementation as per Redmine Id:234918</remarks>
        public DataWithMarking[][,] ReplaceTotalValuesToValidCasesValues(DataWithMarking[][,] tabulationArray, Dictionary<string, DataWithMarking[][,]> summaryListValidTotalDict, bool hasValidCases, int loop)
        {
            int ai = 0;
            if (hasValidCases)
            {
                foreach (KeyValuePair<string, DataWithMarking[][,]> entry in summaryListValidTotalDict)
                {
                    DataWithMarking[][,] rstArr = entry.Value;
                    tabulationArray[ai] = ReplaceTotalValues(rstArr[loop - 1], tabulationArray[ai]);
                    ai++;
                }
            }
            return tabulationArray;
        }

        private void deleteFiles()
        {
            try
            {
                ApplicationConfig appConfig = new ApplicationConfig();
                System.IO.DirectoryInfo di = new DirectoryInfo(appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP));
                di.Delete(true);
            }
            catch (Exception) { }
        }

        private void createCrossArrayST(ref DataWithMarking[,] dw, ref DataWithMarking[,] newDM, bool isNQ,
                int addColumnCount, ref DataWithMarking dmNA, ref DataWithMarking dmN, ref DataWithMarking dmZ,
                ref DataWithMarking dmWAP, ref DataWithMarking dmWA, ref DataWithMarking dmNAnswer,
                bool hasWeight, ref DataWithMarking dmHf, bool totalCntDiff)
        {
            int ii = 0;
            for (int k = 0; k < newDM.GetLength(0); k++)
            {
                if (isNQ && 1 == ii)
                {
                    for (int jj = 0; jj < newDM.GetLength(1); jj++)
                    {
                        newDM.SetValue(dmN, k, jj);
                    }
                    k++;
                }
                int l;
                for (l = 0; l < newDM.GetLength(1) - addColumnCount; l++)
                {
                    DataWithMarking tp = (DataWithMarking)dw.GetValue(ii, l);
                    if (hasWeight || isNQ)
                    {
                        tp.Percent = tp.NumValue;
                    }

                    //if (totalCntDiff)
                    //{
                    //    if (k > 1 && (l == 3))
                    //    {
                    //        tp = dmHf;
                    //    }
                    //}

                    newDM.SetValue(tp, k, l);
                }
                int n = dw.GetLength(1) - addColumnCount;
                for (int m = 0; m < addColumnCount; m++)
                {
                    if (0 == m)
                    {
                        if (0 == k)//k is first
                        {
                            newDM.SetValue(dmNAnswer, k, l++);
                        }
                        else if (1 == k)//k is second
                        {
                            newDM.SetValue(dmN, k, l++);
                        }
                        else //k is other
                        {
                            newDM.SetValue(dmZ, k, l++);
                        }
                    }
                    else if (1 == m)
                    {
                        if (0 == k)//k is first
                        {
                            newDM.SetValue(dmNA, k, l++);
                        }
                        else if (1 == k)//k is second
                        {
                            newDM.SetValue(dmN, k, l++);
                        }
                        else //k is other
                        {
                            newDM.SetValue(dmZ, k, l++);
                        }
                    }
                    else if (2 == m)
                    {
                        if (0 == k)//k is first
                        {
                            newDM.SetValue(dmWAP, k, l++);
                        }
                        else if (1 == k)//k is second
                        {
                            newDM.SetValue(dmN, k, l++);
                        }
                        else //k is other
                        {
                            newDM.SetValue(dmZ, k, l++);
                        }
                    }
                    else if (3 == m)
                    {
                        if (0 == k)//k is first
                        {
                            newDM.SetValue(dmWA, k, l++);
                        }
                        else //k is other
                        {
                            newDM.SetValue(dmN, k, l++);
                        }
                    }
                    else
                    {
                        newDM.SetValue(dw.GetValue(ii, n), k, l++);
                    }
                    n++;
                }
                ii++;
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

        private void writeErrorFile(string divName, string errMsg, Workbook workBook, string dir)
        {
            string errorFile = System.IO.Path.Combine(dir, "error.txt");
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(errorFile, true, Encoding.UTF8))
            {
                writer.WriteLine(divName.TrimEnd('_') + "\t" + errMsg);
            }
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
        public static void logOutput(DataWithMarking[,] result)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            for (int i = result.GetLowerBound(0); i <= result.GetUpperBound(0); i++)
            {

                for (int j = result.GetLowerBound(1); j < result.GetUpperBound(1); j++)
                {

                    sb.Append(result[i, j].Value).Append("\t");
                }
                sb.Append("\n");
            }
            int a = 00;
        }
    }
}
