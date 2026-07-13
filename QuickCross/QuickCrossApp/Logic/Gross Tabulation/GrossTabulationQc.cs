using log4net;
using Macromill.QCWeb.Batch.Report;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using Qc4Launcher.Classes.Gross_Tabulation;
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
using System.Linq;
using Macromill.QCWeb.Logic.TabulationEx.Criteria;
using System.Diagnostics;
using Macromill.QCWeb.COMOperate;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Dao.AllCommon.CDef;
using QC4Common.Model;
using DialogResult = System.Windows.Forms.DialogResult;
using AnswerType = Qc4Launcher.Util.Constants.AnswerType;
using System.ComponentModel;
using Qc4Launcher.Logic.Gross_Tabulation.Openxml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using Macromill.QCWeb.Model;
using System.Globalization;


namespace Qc4Launcher.Logic.Gross_Tabulation
{
    public class GrossTabulationQc
    {
        public delegate void OnWorkerMethodCompleteDelegate(double value, string status, bool isForceStop = false, bool retainThread = false, bool displayMessage = false, bool close = false, bool disableCancel = false,bool isproceescancel=false);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal IntPtr childExcelApp = IntPtr.Zero;

        public void Tabulate(Workbook workBook, Worksheet workSheet, object worker, DoWorkEventArgs bgWorkerArg, string version = null, bool IsReqFromOptions = false, MainWindow mainW = null, bool isFromSTD = false)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Application xlApp = new Application();
            List<string> tsvPaths = new List<string>();
            BackgroundWorker bgWorker = worker as BackgroundWorker;
            List<string> outputFiles = new List<string>();
            GTSettings gTSettings = null;
            String dsep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            String gsep = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            String lsep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;


            try
            {
                bool isChart = false;
                _log.Info("Gross Tabulation started");
                Stopwatch stopwatch = new Stopwatch(); // Stopwatch for tracking
                stopwatch.Start();
                OpenXmlHelper.RemoveOutPutFiles("gtoutput",true);
                childExcelApp = IntPtr.Zero;
                DBHelper.CreateMultivariateTempTable(workBook);

                SigSettings sigSettings = null;
                if (version == "P")
                    sigSettings = new QC4Common.Common.CommonFunctions().GetGTSigSettings(workBook);
                else
                {
                    sigSettings = new SigSettings();
                    Worksheet sht = CrossSettingsReader.getASSheet(workBook);
                    Dictionary<string, string> Adsettings = CrossSettingsReader.getAdvacedSettings(sht);
                    string True = "TRUE";

                    string F_Gt_GT_AddUp_Check_Advantage_99_S = "F_Gt_GT_AddUp_Check_Advantage_99_S";
                    string F_Gt_GT_AddUp_Check_Advantage_95_S = "F_Gt_GT_AddUp_Check_Advantage_95_S";
                    string F_Gt_GT_AddUp_Check_Advantage_90_S = "F_Gt_GT_AddUp_Check_Advantage_90_S";

                    if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Advantage_99_S))
                    {
                        if (Adsettings[F_Gt_GT_AddUp_Check_Advantage_99_S].ToUpper() == True)
                            sigSettings.IsSig1Checked = true;
                        else
                            sigSettings.IsSig1Checked = false;
                    }

                    if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Advantage_95_S))
                    {
                        if (Adsettings[F_Gt_GT_AddUp_Check_Advantage_95_S].ToUpper() == True)
                            sigSettings.IsSig5Checked = true;
                        else
                            sigSettings.IsSig5Checked = false;
                    }

                    if (Adsettings.ContainsKey(F_Gt_GT_AddUp_Check_Advantage_90_S))
                    {
                        if (Adsettings[F_Gt_GT_AddUp_Check_Advantage_90_S].ToUpper() == True)
                            sigSettings.IsSig10Checked = true;
                        else
                            sigSettings.IsSig10Checked = false;

                    }
                }

                //bool cancel = false;

                //if (!ExcelAddIn.Common.Change.ValidateGTTab(null, ref cancel, true, workBook.Worksheets,isFromSTD: isFromSTD))
                //{
                //    OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                //    return;
                //}

                int updateProgress = 1;
                OnWorkerComplete(updateProgress, LocalResource.PB_GT_PROCESSING);

                bool ShowDPNEMsg = false; if (IsReqFromOptions) ShowDPNEMsg = false;
                string lccd = "JA";
                string companyName = LocalResource.REPORT_SIGNATURE_KEYWORD;
                Questions questions = DictUpdate.GetQuestions(workBook);
                string surveyTitle = questions.SurveyTitle;
                //string sigLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QC4", "sig.log");
                string sigLog = OutputUtil.GetSigLogPath(xlApp.PathSeparator);

                _log.Info("Reading settings");
                updateProgress = 3;
                OnWorkerComplete(updateProgress, LocalResource.PB_READING_SETTINGS);
                GTSettingsReader gTSettingsReader = new GTSettingsReader();
                if (sigSettings.IsSig1Checked && sigSettings.IsSig5Checked && sigSettings.IsSig10Checked)
                {
                    OnWorkerComplete(updateProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.GT_VALIDATION_SIGNTEST_MAX, Enums.MessageType.Warning, workBook);
                    OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                    return;
                }
                bool isStopExecution = false;
                string errorMsg = string.Empty;
                gTSettings = gTSettingsReader.ReadGrossSettings(workBook, workSheet, questions, sigSettings, ref isStopExecution, ref errorMsg, version);

                if (isStopExecution)
                {
                    OnWorkerComplete(updateProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                    MessageDialog.ShowMessageOnWorkBook(errorMsg, Enums.MessageType.Warning, workBook);
                    OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                    return;
                }

                List<GrossTableInput> grossTableInputs = gTSettings.GTInputs;
                GTOptions gTOptions = gTSettings.GtOptions;
                TabulationDescriptions tabulationDescriptions = gTOptions.TabulationDescriptions;
                _log.Info("Reading settings completed");

                if (!gTOptions.IsDataValid)
                {
                    OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                    return;
                }

                if (grossTableInputs.Count == 0)
                {
                    OnWorkerComplete(updateProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.GT_EMPTY, Enums.MessageType.ErrorOk, workBook);
                    OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                    return;
                }

                _log.Info("Gross tab calculation started");

                //Macromill.QCWeb.ReportRequest.ZeroNAIVShowCode zeroshowcd = (Macromill.QCWeb.ReportRequest.ZeroNAIVShowCode)3;
                Macromill.QCWeb.ReportRequest.ZeroNAIVShowCode zeroshowcd = (Macromill.QCWeb.ReportRequest.ZeroNAIVShowCode)0;
                bool mergeaxis = true;
                int id = 1;

                string tableName = "answers";
                if (DBHelper.checkAfterProcess(workBook))
                {
                    tableName = "data_after_process";
                }

                // Division - Grouping
                QCWebException ex;
                List<Data> groupDataList = null;
                List<Data> groupDataListEmpty = null;
                int groupingSectorCount = 1;
                Question group = null;

                #region Grouping - Division
                if (gTOptions.GroupVariable != null)
                {
                    if (!System.IO.Directory.Exists(gTOptions.GroupFolderPath))
                    {
                        //string msg = "Directory not exist";
                        OnWorkerComplete(updateProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.DIR_NOT_EXIST, Enums.MessageType.ErrorOk, workBook);
                        return;
                    }
                    if (!CrossTabulationQC.checkVariableDivision(gTOptions.GroupVariable, Definiotion.VariableDictionary))
                    {
                        OnWorkerComplete(updateProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.INVALID_CLASSIFICATION_VARIABLE, Enums.MessageType.ErrorOk, workBook);
                        OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                        return;
                    }
                    using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                    {
                        con.Open();
                        QuestionSettings qstnDet = Definiotion.VariableDictionary[gTOptions.GroupVariable];
                        group = (Question)questions[qstnDet.Id];
                        DataTable dataTble = new DataTable();
                        groupDataListEmpty = ReadTextFile.ReadDataTable(dataTble, group.QuestionType, null, out ex);
                        try
                        {
                            if (qstnDet.QuestionFlag == "An")
                                dataTble = DBHelper.GetDataTable("Select m.q_" + qstnDet.Id + " from multivariate m join " + tableName + " a on a.sort_no = m.sort_no order by a.sort_no ", con);
                            else
                                dataTble = DBHelper.GetDataTable("Select q_" + qstnDet.Id + " from " + tableName + " order by sort_no ", con);
                        }
                        catch (Exception exc)
                        {
                            if (exc.Message.Contains("no such column")) // If no such column, load null data
                            {
                                if (IsReqFromOptions)
                                {
                                    if (ShowDPNEMsg)
                                    {
                                        DialogResult dialogResult = MessageDialog.InfoYesNo(LocalResource.ALERT_UN_PROCESSED_VARIABLE_EXIST);
                                        if (dialogResult == DialogResult.No)
                                        {
                                            OnWorkerComplete(updateProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                                            MessageDialog.ShowMessageOnWorkBook(LocalResource.ALERT_EXECUTE_DP_OR_DELET_VAR, Enums.MessageType.Info, workBook);
                                            OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                                            return;
                                        }
                                        ShowDPNEMsg = false;
                                    }
                                }
                                //dataTble = DBHelper.GetDataTable("Select NULL AS q_" + qstnDet.Id + " from " + tableName + " LIMIT 0 ", con);
                                if (qstnDet.QuestionFlag == "An")
                                    dataTble = DBHelper.GetDataTable("Select NULL AS m.q_" + qstnDet.Id + " from multivariate m join " + tableName + " a on a.sort_no = m.sort_no ", con);
                                else
                                    dataTble = DBHelper.GetDataTable("Select NULL AS q_" + qstnDet.Id + " from " + tableName + " ", con);

                            }
                            else
                                throw;
                        }
                        groupDataList = ReadTextFile.ReadDataTable(dataTble, group.QuestionType, null, out ex);
                        groupingSectorCount = group.Sectors.Count;
                    }
                }
                #endregion

                Macromill.QCWeb.ReportRequest.Request req = new Macromill.QCWeb.ReportRequest.Request(id, groupingSectorCount, surveyTitle, companyName, zeroshowcd,
                    mergeaxis, gTOptions.Reportprefix, gTOptions.Xlbooknameprefix, gTOptions.Tabletype, gTOptions.Tableorientation,
                    gTOptions.Pagesetuptabletype, gTOptions.Minsamplescountonmarking, gTSettings.Markingtype, gTSettings.Significancetestlevel,
                    (Macromill.QCWeb.Common.XlPaperSize)gTOptions.Papersize, (Macromill.QCWeb.Common.XlPageOrientation)gTOptions.Paperorientation,
                    gTOptions.Tablesononesheet, gTOptions.ShowNACode1, gTOptions.ShowIVCode1, gTOptions.WBOn1, gTOptions.FilteringExpression1, gTOptions.PreWbBase
                    );

                Macromill.QCWeb.ReportRequest.Reportsets.Reportset reportset = (Macromill.QCWeb.ReportRequest.Reportsets.Reportset)req.Reportsets[Convert.ToString(id)];
                Macromill.QCWeb.ReportRequest.Outputs.OutputGT outputGT = (Macromill.QCWeb.ReportRequest.Outputs.OutputGT)reportset.Outputs[0];

                #region - Filter
                bool[] filterringFlag = null;
                if (gTOptions.HasFilter)
                {
                    if (!CrossTabulationQC.checkVariableFilter(gTOptions.Filters, Definiotion.VariableDictionary))
                    {
                        OnWorkerComplete(updateProgress, LocalResource.PB_READING_SETTINGS, disableCancel: true);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.GT_INVALID_FILTER_SETTINGS, Enums.MessageType.ErrorOk, workBook);
                        OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                        return;
                    }
                    string filterExp = CriteriaDescProvider.CreateCriteriaDescriptions(gTOptions.Filters, questions);
                    filterringFlag = new Criteria(filterExp, "", questions).Filtering(DBHelper.GetConnectionString(workBook), tableName: tableName);
                    gTOptions.FilteringExpression1 = filterExp;
                    filterExp = CriteriaDescProvider.CreateCriteriaDescriptionsForLocalExp(gTOptions.Filters, questions);
                    gTOptions.LocalizedFilteringExpression1 = CriteriaDescProvider.LocalizeFilteringExpression(filterExp, req, questions,true);
                }
                #endregion

                Dictionary<string, DataWithMarking[,]> gtArrayMap = new Dictionary<string, DataWithMarking[,]>();
                Dictionary<string, DataWithMarking[][,]> gtArrayListMap = new Dictionary<string, DataWithMarking[][,]>();
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

                List<Question> questionProcessed = new List<Question>();

                int gtTableSetItemCnt = 1;

                updateProgress = 10;
                OnWorkerComplete(updateProgress, LocalResource.PB_PROCESS_INPUTS);

                int inputProcessed = 1;
                int childProgress = 0;
                foreach (GrossTableInput grossTableInput in grossTableInputs) //20 - allocated for progress bar
                {
                    if (bgWorker.CancellationPending) return;
                    double progressChildPerc = (double)inputProcessed / grossTableInputs.Count * 100;
                    childProgress = Convert.ToInt32(20 * progressChildPerc / 100);
                    var ProgressVal = updateProgress + childProgress;
                    OnWorkerComplete(ProgressVal, String.Format(LocalResource.PB_GT_PROCESSING_INPUTS, inputProcessed, grossTableInputs.Count));
                    inputProcessed++;

                    Question qstn = null;
                    DataWithMarking[,] result = null;
                    DataWithMarking[][,] resultList = null;

                    QCWebException ex1;
                    bool IsCustomVariable = false;
                    string targetVaraible = grossTableInput.VariableName;
                    
                    bool hasCount = false;
                    bool hasLower = true;

                    using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                    {
                        con.Open();
                        QuestionSettings qstnDet;
                        string columnName = null;
                        qstnDet = Definiotion.VariableDictionary[targetVaraible];
                        if (targetVaraible == QuestionVariableValue.QuestionVariableItem)
                        {
                            qstn = (Question)questions[0]; // need to change after question settings updations
                            columnName = DBSettings.SampleIdColumnName;
                        }
                        else
                        {
                            qstn = (Question)questions[qstnDet.Id];
                            columnName = DBSettings.ColumnNamePreText + qstnDet.Id;
                        }

                        if (grossTableInput.TableHeading == null || grossTableInput.TableHeading.Trim().Length == 0)
                        {
                            grossTableInput.TableHeading = qstnDet.TableHeading;
                        }

                        #region Weight & Score                      
                        string[] weightArray = null;
                        hasLower = true;
                        if ((qstn.QuestionType & QuestionType.MA) == QuestionType.MA && qstnDet.Count.Length != 0)
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
                                        hasLower = false;
                                        weightArray[ct - 1] = "0";
                                    }
                                }

                            }
                        }
                        else if (qstnDet.Score.Length != 0)
                        {
                            weightArray = qstnDet.Score.Split(new char[] { ',' });
                            if (weightArray.Length == 0) { weightArray = null; } else { grossTableInput.HasWeight = true; };
                        }
                        #endregion

                        List<Data> dataList = null;
                        DataTable dataTble = null;

                        grossTableInput.QuestionType2 = qstn.QuestionType;

                        QuestionType qType = OutputUtil.DeepClone(qstn.QuestionType);

                        if ((!grossTableInput.IsMatrixPatent) && !String.IsNullOrEmpty(qstnDet.AddSubTotal) && qstnDet.SubTotalCount > 0)
                        {
                            qType = QuestionType.MA;
                            CrossTabulationQC crossTabulationQC = new CrossTabulationQC();
                            try
                            {
                                dataList = crossTabulationQC.ReadSubTotalData(qstnDet, qstn, workBook, con, questionType: qType, tableName: tableName);
                            }
                            catch (Exception exc)
                            {
                                if (exc.Message.Contains("no such column")) // If no such column, load null data
                                {
                                    dataTble = new System.Data.DataTable();
                                    dataList = ReadTextFile.ReadDataTable(dataTble, qType, null, out ex1);
                                }
                                else
                                    throw;
                            }
                        }
                        else
                        {
                            try
                            {
                                if (qstnDet.QuestionFlag == "An")
                                    dataTble = DBHelper.GetDataTable("Select m." + columnName + " from multivariate m join " + tableName + " a on m.sort_no = a.sort_no order by m.sort_no", con);
                                else
                                    dataTble = DBHelper.GetDataTable("Select " + columnName + " from " + tableName + " order by sort_no", con);
                            }
                            catch (Exception exc)
                            {
                                if (exc.Message.Contains("no such column")) // If no such column, load null data
                                {
                                    if (IsReqFromOptions)
                                    {
                                        if (ShowDPNEMsg)
                                        {
                                            DialogResult dialogResult = MessageDialog.InfoYesNo(LocalResource.ALERT_UN_PROCESSED_VARIABLE_EXIST);
                                            if (dialogResult == DialogResult.No)
                                            {
                                                OnWorkerComplete(ProgressVal, String.Format(LocalResource.PB_GT_PROCESSING_INPUTS, inputProcessed, grossTableInputs.Count), disableCancel: true);
                                                MessageDialog.ShowMessageOnWorkBook(LocalResource.ALERT_EXECUTE_DP_OR_DELET_VAR, Enums.MessageType.Info, workBook);
                                                OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                                                return;
                                            }
                                            ShowDPNEMsg = false;
                                        }
                                    }
                                    dataTble = new System.Data.DataTable();
                                }
                                else
                                    throw;
                            }

                            dataList = ReadTextFile.ReadDataTable(dataTble, qType, null, out ex1);
                        }


                        QCWebException ex2 = null;
                        var childQuestionIds = new List<string>();
                        var childVariableNames = new List<string>();

                        if (grossTableInput.IsMatrixPatent)
                        {
                            List<List<Data>> matrixDatas = new List<List<Data>>();
                            var childDescriptions = new List<string>();
                            var childQuestionName = new List<string>();
                            List<QuestionType> childQuestionTypeList = new List<QuestionType>();
                            QuestionType questionType = OutputUtil.DeepClone(qType);
                            QuestionType questionTypeOr = OutputUtil.DeepClone(qType);
                            IQuestion parentQuestion = qstn.ParentQuestion;
                            questionType = GetCustomQType(grossTableInput.GTTableType, true);
                            if (parentQuestion != null)
                            {
                                if (grossTableInput.GTTableType != GetCustomQcQuestionType2(qType))
                                {
                                    IsCustomVariable = true;
                                }
                            }
                            else
                            {
                                IsCustomVariable = true; // set
                            }

                            grossTableInput.QuestionType2 = questionType;
                            questionTypeOr = OutputUtil.DeepClone(questionType);

                            for (int i = 0; i <= grossTableInput.MatrixColumns.Count - 1; i++)
                            {
                                string childVariableName = grossTableInput.MatrixColumns[i];

                                Question q;
                                QuestionType childQuestionType;
                                string columnNameChild;
                                if (childVariableName == QuestionVariableValue.QuestionVariableItem)
                                {
                                    q = (Question)questions[0]; // need to change after question settings updations
                                    columnNameChild = DBSettings.SampleIdColumnName;
                                }
                                else
                                {
                                    QuestionSettings childQstnDet = Definiotion.VariableDictionary[childVariableName];
                                    q = (Question)questions[childQstnDet.Id];
                                    columnNameChild = DBSettings.ColumnNamePreText + q.ID;
                                }

                                childQuestionType = q.QuestionType;
                                //#OutputFormatting #For Sub total implementation

                                DataTable childDataTble = null;
                                List<Data> childDatas = null;

                                qstnDet = Definiotion.VariableDictionary[q.Name];
                                if (!String.IsNullOrEmpty(qstnDet.AddSubTotal) && qstnDet.SubTotalCount > 0)
                                {
                                    try
                                    {
                                        CrossTabulationQC crossTabulationQC = new CrossTabulationQC();
                                        childDatas = crossTabulationQC.ReadSubTotalData(qstnDet, q, workBook, con, tableName: tableName);
                                    }
                                    catch (Exception exc)
                                    {
                                        if (exc.Message.Contains("no such column")) // If no such column, load null data
                                        {
                                            try
                                            {
                                                if (qstnDet.QuestionFlag == "An")
                                                    childDataTble = DBHelper.GetDataTable("Select NULL As m." + columnName + " from multivariate m join " + tableName + " a on m.sort_no = a.sort_no ", con);
                                                else
                                                    childDataTble = DBHelper.GetDataTable("Select NULL As " + columnName + " from " + tableName, con);
                                                DPCheckList.DPCheckListHelper.setDeleteFlag(childDataTble.Rows.Count);
                                                childDatas = ReadTextFile.ReadDataTable(childDataTble, childQuestionType, null, out ex1);
                                            }
                                            finally
                                            {
                                                ReadTextFile.DeleteFlag = null;
                                            }
                                        }
                                        else
                                            throw;
                                    }
                                    questionType = QuestionType.MA | QuestionType.MatrixParent;
                                    childQuestionType = QuestionType.MA;
                                }
                                else
                                {
                                    bool newItem = false;
                                    try
                                    {
                                        if (qstnDet.QuestionFlag == "An")
                                            childDataTble = DBHelper.GetDataTable("Select m." + columnNameChild + " from multivariate m join " + tableName + " a on m.sort_no = a.sort_no order by m.sort_no", con);
                                        else
                                            childDataTble = DBHelper.GetDataTable("Select " + columnNameChild + " from " + tableName + " order by sort_no", con);
                                    }
                                    catch (Exception exc)
                                    {
                                        if (exc.Message.Contains("no such column")) // If no such column, load null data
                                        {
                                            if (IsReqFromOptions)
                                            {
                                                if (ShowDPNEMsg)
                                                {
                                                    DialogResult dialogResult = MessageDialog.InfoYesNo(LocalResource.ALERT_UN_PROCESSED_VARIABLE_EXIST);
                                                    if (dialogResult == DialogResult.No)
                                                    {
                                                        OnWorkerComplete(ProgressVal, String.Format(LocalResource.PB_GT_PROCESSING_INPUTS, inputProcessed, grossTableInputs.Count), disableCancel: true);
                                                        MessageDialog.ShowMessageOnWorkBook(LocalResource.ALERT_EXECUTE_DP_OR_DELET_VAR, Enums.MessageType.Info, workBook);
                                                        OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                                                        return;
                                                    }
                                                    ShowDPNEMsg = false;
                                                }
                                            }
                                            newItem = true;
                                            if (qstnDet.QuestionFlag == "An")
                                                childDataTble = DBHelper.GetDataTable("Select NULL As m." + columnNameChild + " from multivariate m join " + tableName + " a on m.sort_no = a.sort_no order ", con);
                                            else
                                                childDataTble = DBHelper.GetDataTable("Select NULL As " + columnNameChild + " from " + tableName, con);
                                        }
                                        else
                                            throw;
                                    }
                                    try
                                    {
                                        if (newItem)
                                        {
                                            DPCheckList.DPCheckListHelper.setDeleteFlag(childDataTble.Rows.Count);
                                        }
                                        childDatas = ReadTextFile.ReadDataTable(childDataTble, childQuestionType, null, out ex);
                                    }
                                    finally
                                    {
                                        if (newItem)
                                        {
                                            ReadTextFile.DeleteFlag = null;
                                        }
                                    }
                                }

                                childDescriptions.Add(q.Description);
                                matrixDatas.Add(childDatas);
                                childQuestionName.Add(q.Name);
                                childQuestionTypeList.Add(childQuestionType);

                                if (!IsCustomVariable)
                                {
                                    // childQuestionTypeList.Add(childQuestionType);
                                }
                                else
                                {
                                    qstn = copyQuestion(qstn);
                                    qstn.QuestionType = GetCustomQType(grossTableInput.GTTableType, true);
                                    qstn.QCAnswerType = GetCustomQCAnswerType(grossTableInput.GTTableType);
                                    qstn.QCQuestionType = GetCustomQcQuestionType(grossTableInput.GTTableType);
                                }
                                childQuestionIds.Add(q.ID.ToString());
                                childVariableNames.Add(childVariableName);
                            }

                            if (group == null)
                            {
                                ex2 = GTTabulation.GetGTArrayMatrix(
                                questionType
                                , CrossTabulationQC.GetCategoryArray(qstn, true)
                                , matrixDatas
                                , childQuestionTypeList.ToArray()
                                , childDescriptions.ToArray()
                                , out result, translation
                                , tabulationDescriptions
                                , filterringFlag
                                , gTOptions.WBDataList
                                , weightArray
                                , gTOptions.TabulateFullQuantity1
                                , gTOptions.TabulateFullQuantity1 && gTOptions.VisibleUnfitFlagAsFlag
                                , lccd
                                , gTOptions.NoAnswerDenominatorFlag &&
                                  (!gTOptions.TabulateFullQuantity1) &&
                                  (!gTOptions.TargetNoAnswerOnOff)
                                , grossTableInput.SigTestCode
                                , gTSettings.TestLevels.ToArray()
                                , sigLog
                                , qstn.Name
                                , childQuestionName.ToArray()
                                , hasCount
                                , !String.IsNullOrEmpty(qstnDet.AddSubTotal) ? qstnDet.SubTotalCount : 0, isMTS: (qstnDet.AnswerType == AnswerType.SA), qTypeOr: questionTypeOr,isLower:hasLower
                                );
                                gtArrayMap.Add(gtTableSetItemCnt.ToString(), result);
                            }
                            else
                            {
                                ex2 = GTTabulation.GetGTArrayMatrix(
                                   questionType
                                   , (group.QuestionType & QuestionType.SA) == QuestionType.SA ? QuestionType.SA : (group.QuestionType & QuestionType.MA) == QuestionType.MA ? QuestionType.MA : QuestionType.SA
                                   , CrossTabulationQC.GetCategoryArray(qstn, true)
                                   , CrossTabulationQC.GetCategoryArray(group)
                                   , matrixDatas
                                   , groupDataList
                                   , childQuestionTypeList.ToArray()
                                   , childDescriptions.ToArray()
                                   , out resultList, translation
                                   , tabulationDescriptions
                                   , filterringFlag
                                   , gTOptions.WBDataList
                                   , weightArray
                                   , gTOptions.TabulateFullQuantity1
                                   , lccd
                                   , gTOptions.TabulateFullQuantity1 && gTOptions.VisibleUnfitFlagAsFlag
                                   , gTOptions.NoAnswerDenominatorFlag &&
                                     (!gTOptions.TabulateFullQuantity1) &&
                                     (!gTOptions.TargetNoAnswerOnOff)
                                   , grossTableInput.SigTestCode
                                   , gTSettings.TestLevels.ToArray()
                                   , sigLog
                                   , qstn.Name
                                   , childQuestionName.ToArray()
                                   , group.Name
                                   , hasCount
                                   , !String.IsNullOrEmpty(qstnDet.AddSubTotal) ? qstnDet.SubTotalCount : 0, isMTS: (qstnDet.AnswerType == AnswerType.SA), qTypeOr: questionTypeOr,isLower:hasLower
                                   );
                                gtArrayListMap.Add(gtTableSetItemCnt.ToString(), resultList);
                            }
                        }
                        else if ((qType & QuestionType.N) == QuestionType.N)
                        {

                            if (group == null)
                            {
                                ex2 =
                            GTTabulation.GetGTArrayN(
                                  qType
                                , qstn.Name
                                , dataList
                                , out result, translation
                                , tabulationDescriptions
                                , filterringFlag
                                , gTOptions.WBDataList
                                , weightArray
                                , gTOptions.TabulateFullQuantity1
                                , gTOptions.TabulateFullQuantity1 && gTOptions.VisibleUnfitFlagAsFlag
                                , lccd
                                , gTOptions.NoAnswerDenominatorFlag &&
                                  (!gTOptions.TabulateFullQuantity1) &&
                                  (!gTOptions.TargetNoAnswerOnOff)
                                );
                                gtArrayMap.Add(gtTableSetItemCnt.ToString(), result);
                            }
                            else// grouping
                            {
                                ex2 =
                      GTTabulation.GetGTArrayN(
                                  qType
                          , qstn.Name
                          , dataList
                          , group.QuestionType
                          , CrossTabulationQC.GetCategoryArray(group)
                          , dataList.Count > 0 ? groupDataList : groupDataListEmpty
                          , out resultList, translation
                          , tabulationDescriptions
                          , filterringFlag
                          , gTOptions.WBDataList
                          , weightArray
                          , gTOptions.TabulateFullQuantity1
                          , gTOptions.TabulateFullQuantity1 && gTOptions.VisibleUnfitFlagAsFlag
                          , lccd
                          , gTOptions.NoAnswerDenominatorFlag &&
                            (!gTOptions.TabulateFullQuantity1) &&
                            (!gTOptions.TargetNoAnswerOnOff)
                              );
                                gtArrayListMap.Add(gtTableSetItemCnt.ToString(), resultList);
                            }
                        }
                        else if ((qType & QuestionType.SA) == QuestionType.SA || (qType & QuestionType.MA) == QuestionType.MA)
                        {

                            if (group == null)
                            {
                                ex2 =
                            GTTabulation.GetGTArraySAMA(
                                  qType
                                , CrossTabulationQC.GetCategoryArray(qstn, true)
                                , dataList
                                , out result, translation
                                , tabulationDescriptions
                                , filterringFlag
                                , gTOptions.WBDataList
                                , weightArray
                                , gTOptions.TabulateFullQuantity1
                                , gTOptions.TabulateFullQuantity1 && gTOptions.VisibleUnfitFlagAsFlag
                                , lccd
                                , gTOptions.NoAnswerDenominatorFlag &&
                                  (!gTOptions.TabulateFullQuantity1) &&
                                  (!gTOptions.TargetNoAnswerOnOff)
                                , grossTableInput.SigTestCode
                                , gTSettings.TestLevels.ToArray()
                                , sigLog
                                , qstn.Name
                                , hasCount
                                , !String.IsNullOrEmpty(qstnDet.AddSubTotal) ? qstnDet.SubTotalCount : 0, qTypeOr: qstn.QuestionType,isLower:hasLower
                                );
                                gtArrayMap.Add(gtTableSetItemCnt.ToString(), result);
                            }
                            else //Division
                            {
                                ex2 =
                           GTTabulation.GetGTArraySAMA(
                                  qType
                               , CrossTabulationQC.GetCategoryArray(qstn, true)
                               , dataList
                               , group.QuestionType
                               , CrossTabulationQC.GetCategoryArray(group)
                               , dataList.Count > 0 ? groupDataList : groupDataListEmpty
                               , out resultList, translation
                               , tabulationDescriptions
                               , filterringFlag
                               , gTOptions.WBDataList
                               , weightArray
                               , gTOptions.TabulateFullQuantity1
                               , gTOptions.TabulateFullQuantity1 && gTOptions.VisibleUnfitFlagAsFlag
                               , lccd
                               , gTOptions.NoAnswerDenominatorFlag &&
                                 (!gTOptions.TabulateFullQuantity1) &&
                                 (!gTOptions.TargetNoAnswerOnOff)
                               , grossTableInput.SigTestCode
                               , gTSettings.TestLevels.ToArray()
                               , sigLog
                               , qstn.Name
                               , group.Name
                               , hasCount
                               , !String.IsNullOrEmpty(qstnDet.AddSubTotal) ? qstnDet.SubTotalCount : 0, qTypeOr: qstn.QuestionType,isLower:hasLower
                               );
                                gtArrayListMap.Add(gtTableSetItemCnt.ToString(), resultList);
                            }
                        }
                        else
                        {
                            OnWorkerComplete(ProgressVal, String.Format(LocalResource.PB_GT_PROCESSING_INPUTS, inputProcessed, grossTableInputs.Count), disableCancel: true);
                            MessageDialog.ShowMessageOnWorkBook(LocalResource.GT_INVALID_TABLE_TYPE, Enums.MessageType.Warning, workBook);
                            OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true);
                            return;
                        }

                        if (ex2 != null)
                        {
                            throw ex2;
                        }

                        if (grossTableInput.IsMatrixPatent)
                        {
                            Question parentQuestion = null;
                            Questions childQuestions = null;

                            if (!IsCustomVariable)
                            {
                                parentQuestion = (Question)qstn.ParentQuestion;
                            }
                            else
                            {
                                parentQuestion = qstn;
                            }
                            childQuestions = new Questions();
                            foreach (string childId in childVariableNames)
                            {
                                Question chQstn = GTSettingsReader.getQuestion(questions, childId);
                                childQuestions.Add(chQstn.ID.ToString(), chQstn);
                            }
                            //}
                            QuestionType parentQtype = parentQuestion.QuestionType;
                            List<string> removeList = new List<string>();
                            Questions newQuestions = new Questions();
                            if (childQuestions != null)
                            {
                                foreach (Question question in childQuestions.Values)
                                {
                                    if (!childQuestionIds.Contains(question.ID.ToString()))
                                        removeList.Add(question.ID.ToString());

                                    Question newQuestion = question;
                                    newQuestions.Add(question.ID.ToString(), newQuestion);
                                }

                                childQuestions = newQuestions;

                                foreach (string removeItem in removeList)
                                    childQuestions.Remove(removeItem);
                            }

                            Question dummyQuestion = new Question()
                            {
                                CategoryEditID = parentQuestion.CategoryEditID,
                                ChildQuestions = childQuestions,
                                ColumnName = parentQuestion.ColumnName,
                                DataProcessType = parentQuestion.DataProcessType,
                                Description = parentQuestion.Description,
                                DoSort = parentQuestion.DoSort,
                                GtMatrixBaseItemId = parentQuestion.GtMatrixBaseItemId,
                                GtMatrixBaseQuestion = parentQuestion.GtMatrixBaseQuestion,
                                ID = parentQuestion.ID,
                                Index = parentQuestion.Index,
                                IsDataEdit = parentQuestion.IsDataEdit,
                                IsQC3BlankNumber = parentQuestion.IsQC3BlankNumber,
                                Name = qstn.Name,
                                Number = parentQuestion.Number,
                                OriginalDescription = parentQuestion.OriginalDescription, //need to modify
                                ParentCollection = parentQuestion.ParentCollection,
                                ParentSector = parentQuestion.ParentSector,
                                QCQuestionType = parentQuestion.QCQuestionType,
                                QCWebID = parentQuestion.QCWebID,
                                QuestionType = parentQtype,
                                TemporaryDataProcess = parentQuestion.TemporaryDataProcess,
                                TopTableName = parentQuestion.TopTableName,
                                Sectors = parentQuestion.Sectors,
                                QCAnswerType = parentQuestion.QCAnswerType
                            };

                            qstn = dummyQuestion; // New implementation at the time of division
                        }
                    }
                    questionProcessed.Add(qstn);
                    gtTableSetItemCnt++;

                }

                updateProgress = updateProgress + childProgress;
                OnWorkerComplete(updateProgress, LocalResource.PB_CREATING_REPORT);
                inputProcessed = 1; childProgress = 0;
                bool ShowMessage = false;
                bool IsBandGraphShow = true;
                for (int loop = 1; loop <= groupingSectorCount; loop++)
                {
                    if (bgWorker.CancellationPending) return;
                    double progressChildPerc = (double)inputProcessed / groupingSectorCount * 100;
                    childProgress = Convert.ToInt32(30 * progressChildPerc / 100);
                    var ProgressVal = updateProgress + childProgress;
                    OnWorkerComplete(ProgressVal, LocalResource.PB_GENERATING_TSV);
                    inputProcessed++;

                    Macromill.QCWeb.ReportRequest.Outputs.OutputGT grossT = (Macromill.QCWeb.ReportRequest.Outputs.OutputGT)reportset.Outputs[loop - 1];
                    DataWithMarking[][,] tabulationArray = new DataWithMarking[grossTableInputs.Count][,];
                    Question qstn = null;
                    int i = 0;
                    int crTableSetCnt = 1;
                    foreach (GrossTableInput grossTableInput in grossTableInputs)
                    {
                        bool hasCount = false;
                        DataWithMarking[,] result = null;
                        DataWithMarking[][,] resultList = null;

                        qstn = questionProcessed[crTableSetCnt - 1];

                        if ((qstn.QuestionType & QuestionType.MA) == QuestionType.MA)
                        {
                            string targetVaraible = grossTableInput.VariableName;
                            QuestionSettings qstnDet = Definiotion.VariableDictionary[targetVaraible];
                            hasCount = qstnDet.Count.Length != 0;
                        }

                        //Graph Settings
                        string graphType = null;
                        string graphTypeOrg = null;
                        string[] colorIndexArray = null;
                        string gradationType = null;
                        if ((qstn.QCQuestionType == QCQuestionType.MTS || qstn.QCQuestionType == QCQuestionType.MTM || qstn.QCQuestionType == QCQuestionType.RNK) && qstn.Sectors != null)
                            if (qstn.Sectors.Count > 200)
                                grossTableInput.HasGraph = false;
                        if (grossTableInput.HasGraph)
                        {
                            graphTypeOrg = grossTableInput.GraphType;
                            graphType = ConvertToGraphTypeOfMtm(graphTypeOrg, qstn.QCQuestionType, grossTableInput.IsMatrixPatent); ;
                            UpdateColorSettings(ref graphType, ref gradationType, ref colorIndexArray);
                        }

                        if (group == null)
                        {
                            result = gtArrayMap[crTableSetCnt.ToString()];
                            if (gTOptions.Tablesononesheet == Macromill.QCWeb.ReportRequest.TablesOnOneSheet.Single || isChart)
                                tabulationArray[i++] = result;
                            else
                            {
                                Macromill.QCWeb.ReportRequest.Tables.GTTable table = TableUtil.SetOutputRequestTableGT(result, grossT,
                            qstn, grossTableInput.QuestionType2, gradationType, null, null, colorIndexArray, graphTypeOrg, grossTableInput.SigTestCode, grossTableInput.TableHeading, hasCount, isMatrix: grossTableInput.IsMatrixPatent, hasWeight: grossTableInput.HasWeight, wbVariable: gTOptions.WBVariable, tabulateFullQuantity: gTOptions.TabulateFullQuantity1);
                                table.HideChartDescriptionMaxPercent = gTOptions.HideChartDescriptionMaxPercent;
                                if ((qstn.QCQuestionType == QCQuestionType.MTS || qstn.QCQuestionType == QCQuestionType.MTM || qstn.QCQuestionType == QCQuestionType.RNK) && table.SectorsCount > 200 && !ShowMessage)
                                {
                                    ShowMessage = true;
                                    DialogResult res = MessageDialog.WarningOkCancel(LocalResource.GT_WARNING_CHOICE_EXCEEDED);
                                    if (res == DialogResult.Cancel)
                                        throw new Exception("Choice Exceeded");
                                }
                                else if (grossTableInput.GTTableType == "GT-SA" && (grossTableInput.GraphType == "002" || grossTableInput.GraphType == "003") && table.SectorsCount > 255)
                                {
                                    DialogResult res = DialogResult.None;
                                    if (IsBandGraphShow)
                                        res = MessageDialog.WarningOkCancel(LocalResource.GTSA_WARNING_CHOICE_EXCEEDED);
                                    IsBandGraphShow = false;
                                    if (res == DialogResult.Cancel)
                                        throw new Exception("Choice Exceeded");
                                }
                            }
                        }
                        else
                        {
                            resultList = gtArrayListMap[crTableSetCnt.ToString()];
                            DataWithMarking[,] tabulation = resultList[loop - 1];
                            if (gTOptions.Tablesononesheet == Macromill.QCWeb.ReportRequest.TablesOnOneSheet.Single || isChart)
                                tabulationArray[i++] = tabulation;
                            else
                            {
                                Macromill.QCWeb.ReportRequest.Tables.GTTable table = TableUtil.SetOutputRequestTableGT(tabulation, grossT,
                            qstn, grossTableInput.QuestionType2, gradationType, null, null, colorIndexArray, graphTypeOrg, grossTableInput.SigTestCode, grossTableInput.TableHeading, hasCount, isMatrix: grossTableInput.IsMatrixPatent, hasWeight: grossTableInput.HasWeight, wbVariable: gTOptions.WBVariable, tabulateFullQuantity: gTOptions.TabulateFullQuantity1);
                                Sectors.Sector sector = (Sectors.Sector)group.Sectors[loop];
                                table.SetKeyItemInformation(group.Name, group.Description2(), sector.Number, sector.Description);
                                table.HideChartDescriptionMaxPercent = gTOptions.HideChartDescriptionMaxPercent;
                                if ((qstn.QCQuestionType == QCQuestionType.MTS || qstn.QCQuestionType == QCQuestionType.MTM || qstn.QCQuestionType == QCQuestionType.RNK) && table.SectorsCount > 200 && !ShowMessage)
                                {
                                    ShowMessage = true;
                                    DialogResult res = MessageDialog.WarningOkCancel(LocalResource.GT_WARNING_CHOICE_EXCEEDED);
                                    if (res == DialogResult.Cancel)
                                        throw new Exception("Choice Exceeded");
                                }
                                else if (grossTableInput.GTTableType == "GT-SA" && (grossTableInput.GraphType == "002" || grossTableInput.GraphType == "003") && table.SectorsCount > 255)
                                {
                                    DialogResult res = DialogResult.None;
                                    if (IsBandGraphShow)
                                        res = MessageDialog.WarningOkCancel(LocalResource.GTSA_WARNING_CHOICE_EXCEEDED);
                                    IsBandGraphShow = false;
                                    if (res == DialogResult.Cancel)
                                        throw new Exception("Choice Exceeded");
                                }
                            }
                        }
                        crTableSetCnt++;
                    }
                    string tsvPathJoined = (grossT.Tables as Macromill.QCWeb.ReportRequest.Tables).OutputToTSV();
                    tsvPaths.Add(tsvPathJoined);

                }

                updateProgress = updateProgress + childProgress;
                OnWorkerComplete(updateProgress, LocalResource.PB_GENERATING_REPORT);

                Request request = new Request();
                request.MakeGTRequest(id, tsvPaths, surveyTitle, companyName, zeroshowcd, mergeaxis, gTOptions.Reportprefix
                    , gTOptions.Xlbooknameprefix, gTOptions.Tabletype, gTOptions.Tableorientation, gTOptions.Pagesetuptabletype, gTOptions.Minsamplescountonmarking
                    , gTSettings.Markingtype, gTSettings.Significancetestlevel, gTOptions.Papersize, gTOptions.Paperorientation, gTOptions.ShowNACode1
                    , gTOptions.ShowIVCode1, gTOptions.WBOn1, gTOptions.LocalizedFilteringExpression1, gTOptions.PreWbBase);
                _log.Info("Gross tab calculation completed");

                _log.Info("Gross tab excel creation started");
                xlApp.Visible = false;
                xlApp.ScreenUpdating = false;
                xlApp.EnableEvents = false;
                xlApp.DisplayStatusBar = false;
                xlApp.PrintCommunication = false;
                xlApp.DisplayAlerts = false;

                inputProcessed = 1; childProgress = 0;
                double currentProgress = 60;
                OnWorkerComplete(currentProgress, LocalResource.PB_GENERATING_REPORT);

                int pbRSetAllocated = 38;
                double pbREachAllocated = (double)pbRSetAllocated / request.Reportsets.Values.Count;
                string path = "";
                int fileNo = 1;
                foreach (Reportset rs in request.Reportsets.Values)
                {
                    double pbOEachAllocated = (double)pbREachAllocated / rs.Outputs.Values.Count;
                    foreach (OutputGT outputValue in rs.Outputs.Values)
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

                        path = OpenXmlHelper.GetDefaultPath(xlApp, "GT", "gtoutput", "GTOutputForSTD");
                        var filePath = GetFileName(outputValue, gTOptions.GroupFolderPath, isChart, xlApp);
                        using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath == null ? path : filePath, SpreadsheetDocumentType.Workbook))
                        {
                            GTCreatorXML gTCreatorXML = new GTCreatorXML();
                            gTCreatorXML.CreateGT(package, outputValue, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1), AppContext.BaseDirectory, lccd, GetFileName(outputValue, gTOptions.GroupFolderPath, isChart, xlApp), tsvPathArr, OnWorkerComplete, fileNo, ref currentProgress, pbOEachAllocated, Convert.ToDouble(gTOptions.HideChartDescriptionMaxPercent));
                        }
                        if(filePath==null)
                            OpenXmlHelper.SaveWorkBook(path, ("Cj_PWhxRo7Q8" + (char)2), xlApp);


                        //GTCreator gTCreator = new GTCreator();
                        //gTCreator.CreateGT(outputValue, ("Cj_PWhxRo7Q8" + (char)2), ("U5_fMcyDDcX2" + (char)1), AppContext.BaseDirectory, lccd, xlApp, bgWorker,
                        //    GetFileName(outputValue, gTOptions.GroupFolderPath, isChart, xlApp), tsvPathArr, OnWorkerComplete, fileNo, ref currentProgress,
                        //    pbOEachAllocated, outputFiles);
                        fileNo++;
                        if (bgWorker.CancellationPending) return;
                    }
                }

                if (group == null)
                {
                    OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true, true);
                    xlApp.EnableEvents = true;
                    CrossTabulationQC.maximizeExcel(xlApp);
                    xlApp.Calculation = XlCalculation.xlCalculationAutomatic;
                    xlApp.ScreenUpdating = true;
                    xlApp.DisplayAlerts = true;
                    xlApp.Visible = true;
                    childExcelApp = (IntPtr)xlApp.Hwnd;
                    //OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED);
                    //xlApp.EnableEvents = true;
                    //xlApp.WindowState = XlWindowState.xlMaximized;
                    //xlApp.DisplayStatusBar = true;
                    //xlApp.Calculation = XlCalculation.xlCalculationAutomatic;
                    //xlApp.ScreenUpdating = true;
                    //xlApp.DisplayAlerts = true;
                    //xlApp.Visible = true;
                    //childExcelApp = (IntPtr)xlApp.Hwnd;
                    //COMWholeOperate.releaseComObject(ref xlApp);
                    try
                    {
                        String directoryPath = Path.GetDirectoryName(path);
                        if (Directory.Exists(directoryPath))
                        {
                            var di = new DirectoryInfo(directoryPath);
                            if (di.Attributes.HasFlag(FileAttributes.ReadOnly))
                                di.Attributes &= ~FileAttributes.ReadOnly;
                        }
                    }
                    catch { }
                }
                else
                {
                    OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true, true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.COMPLETED, Enums.MessageType.Info, window: mainW, gtOption: gTOptions);
                    try
                    {
                        xlApp.Quit();
                        COMWholeOperate.releaseComObject(ref xlApp);
                    }
                    catch (Exception e)
                    {
                        _log.LogError(e.Message + "\n" + e.StackTrace);
                    }

                }

                _log.Info("Elapsed " + stopwatch.Elapsed.TotalSeconds + " seconds to execute GT tabulation");
                stopwatch.Stop();
                _log.Info("Gross tab excel creation completed");
                
            }
            catch (Exception ex)
            {
                try
                {
                    xlApp.Quit();
                    COMWholeOperate.releaseComObject(ref xlApp);
                }
                catch (Exception e)
                {
                    _log.LogError(e.Message + "\n" + e.StackTrace);
                }

                string exMsg = LocalResource.EXCEL_GENERATION_FAILED;
                if (ex.Message.Contains("0x800AC472"))
                {
                    exMsg = LocalResource.MSO_LICENCE_FAILED;
                }
                if (ex is FileNotFoundException)
                {
                    exMsg = LocalResource.EX_PATH_NOT_FOUND;
                }
                if (ex.Message == "The file could not be accessed. Try one of the following:\n\n• Make sure the specified folder exists. \n• Make sure the folder that contains the file is not read-only.\n• Make sure the filename and folder path do not contain any of the following characters:  <  >  ?  [  ]  :  | or  *\n• Make sure the filename and folder path do not contain more than 218 characters.")
                {
                    exMsg = LocalResource.WRONG_FILEPATH;
                    _log.LogError(exMsg + "\n" + ex.StackTrace);
                }
                else
                    _log.LogError(ex.Message + "\n" + ex.StackTrace);

                if (ex.Source.Contains("QCWebCommon"))
                {
                    if (gTSettings != null && !String.IsNullOrEmpty(gTSettings.GtOptions.WBVariable) && ex.Message.Contains("パラメータ '{0}' が不正です。:{1}"))
                        exMsg = LocalResource.QC_WEB_WEIGHTBACK_EXCEPTION + "\n" + gTSettings.GtOptions.WBVariable;
                }

                try
                {
                    string outPath = Path.Combine(Path.GetTempPath(), "QC4", "gtoutput", Path.GetFileNameWithoutExtension(Util.Constants.Qc4FileName));
                    if (Directory.Exists(outPath))
                    {
                        var di = new DirectoryInfo(outPath);
                        if (di.Attributes.HasFlag(FileAttributes.ReadOnly))
                            di.Attributes &= ~FileAttributes.ReadOnly;
                    }
                }
                catch { }

                if (ex.Message != "Choice Exceeded")
                {
                    OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true, true, close: true, disableCancel: true);
                    if (isFromSTD)
                        MessageDialog.ShowMessageOnForm(exMsg, mainW);
                    else
                        MessageDialog.ShowMessageOnWorkBook(exMsg, Enums.MessageType.ErrorOk, workBook);
                }
                OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED);
            }
            finally
            {
                foreach (string tsvPathJoined in tsvPaths)
                {
                    string[] tsvPathArr = tsvPathJoined.Split(';');
                    foreach (string tsvPath in tsvPathArr)
                    {
                        try
                        {
                            if (File.Exists(tsvPath))
                            {
                                File.Delete(tsvPath);
                            }
                        }
                        catch (Exception) { }
                    }
                }

                if (bgWorker.CancellationPending)
                {
                    try
                    {
                        xlApp.Quit();
                        COMWholeOperate.releaseComObject(ref xlApp);
                    }
                    catch (Exception) { }
                    CrossTabulationQC.deleteOutPutFiles(outputFiles);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.MSG_OUTPUT_ABORTED, Enums.MessageType.Info, window: mainW, gtOption: null);
                    OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED, true, close: true);
                }
                else
                {
                    OnWorkerComplete(100, LocalResource.PB_GT_COMPLETED);

                    if (workSheet != null)//Redmine id :193454
                    {
                        try
                        {
                            COMWholeOperate.releaseComObject(ref workSheet);
                        }
                        catch { }
                    }
                    if (xlApp != null)//Redmine id :193454
                    {
                        try
                        {
                            COMWholeOperate.releaseComObject(ref xlApp);
                        }
                        catch { }
                    }

                    GC.Collect();//Redmine id :193454
                    GC.WaitForPendingFinalizers();//Redmine id :193454
                }
                _log.Info("Cross Tabulate completed");
                //code
            }
        }

        private string GetFileName(Output output, string OutputDirectoryPath, bool isChart, Application excelApp)
        {
            string fileName = null;
            if (OutputDirectoryPath != null)
            {
                GTTable tmpTable = (GTTable)output.Tables[0];
                Macromill.QCWeb.ReportRequest.KeyItemInformation KeyItemInfo = tmpTable.KeyItem;
                string KeyItemName = string.Empty;
                string filenameSuffix = null;
                if (KeyItemInfo != null)
                    KeyItemName = KeyItemInfo.Name;
                if (KeyItemName.Length > 0)
                {
                    string fmt = new string('0', 4);
                    filenameSuffix = "_" + KeyItemName + "_" + KeyItemInfo.SectorNumber.ToString(fmt);
                    string prefix;
                    if (isChart)
                    {
                        prefix = output.ParentReportset.FileNamePrefix;
                    }
                    else
                    {
                        prefix = output.ExcelBookNamePrefix;
                    }

                    // new implemetation for file duplication
                    string n;
                    int i = 0;
                    string ext = "xlsx";
                    string filename = "";
                    if (!Util.CommonFunction.ActivationKeyChecking())
                    {
                        filename = Util.Constants.Qc4FileName.Substring(Util.Constants.Qc4FileName.LastIndexOf('\\') + 1).Replace(".qc4", "");
                        filename = filename.Split('_')[0];
                        filename = filename + "_" + (DateTime.Now.ToString("yyyyMMdd_HHmm")) + "_";
                    }
                    do
                    {
                        n = filename + prefix + (i > 0 ? "_" + i : "") + filenameSuffix + "." + ext;
                        i = i + 1;
                        fileName = OutputUtil.BuildPath(OutputDirectoryPath, n, excelApp.PathSeparator);
                    } while (File.Exists(fileName));
                }
            }
            return fileName;
        }

        private QuestionType GetCustomQType(string GTTableType, bool reqParentFlag = false)
        {
            QuestionType questionType = QuestionType.N;
            switch (GTTableType)
            {
                case GT.GTMTM:
                    questionType = QuestionType.MA;
                    break;
                case GT.GTMTN:
                    questionType = QuestionType.N;
                    break;
                case GT.GTMTS:
                    questionType = QuestionType.SA;
                    break;
                case GT.GTRAT:
                    questionType = QuestionType.N | QuestionType.Ratio;
                    break;
                case GT.GTRNK:
                    questionType = QuestionType.SA | QuestionType.Rank;
                    break;
            }
            if (reqParentFlag) questionType = questionType | QuestionType.MatrixParent;
            return questionType;
        }

        private QCQuestionType GetCustomQcQuestionType(string GTTableType)
        {
            QCQuestionType questionType = QCQuestionType.MTM;
            switch (GTTableType)
            {
                case GT.GTMTM:
                    questionType = QCQuestionType.MTM;
                    break;
                case GT.GTMTN:
                    questionType = QCQuestionType.MTM;
                    break;
                case GT.GTMTS:
                    questionType = QCQuestionType.MTS;
                    break;
                case GT.GTRAT:
                    questionType = QCQuestionType.MTM | QCQuestionType.RAT;
                    break;
                case GT.GTRNK:
                    questionType = QCQuestionType.MTS | QCQuestionType.RNK;
                    break;
            }
            return questionType;
        }

        private string GetCustomQcQuestionType2(QuestionType qtype)
        {
            string questionType = GT.GTMTS;
            if ((qtype & QuestionType.SA) == QuestionType.SA)
            {
                questionType = GT.GTMTS;
            }
            else if ((qtype & QuestionType.N) == QuestionType.N)
            {
                questionType = GT.GTMTN;
            }
            else if ((qtype & QuestionType.MA) == QuestionType.MA)
            {
                questionType = GT.GTMTM;
            }
            return questionType;
        }

        private Question copyQuestion(Question qstn)
        {
            Question dummyQuestion = new Question()
            {
                questionOrder = qstn.questionOrder,
                isOrg = qstn.isOrg,
                ChildQuestions = qstn.ChildQuestions,
                ColumnName = qstn.ColumnName,
                DataProcessType = qstn.DataProcessType,
                Description = qstn.Description,
                DoSort = qstn.DoSort,
                GtMatrixBaseItemId = qstn.GtMatrixBaseItemId,
                GtMatrixBaseQuestion = qstn.GtMatrixBaseQuestion,
                ID = qstn.ID,
                Index = qstn.Index,
                IsDataEdit = qstn.IsDataEdit,
                IsQC3BlankNumber = qstn.IsQC3BlankNumber,
                Name = qstn.Name,
                Number = qstn.Number,
                OriginalDescription = qstn.OriginalDescription, //need to modify
                ParentCollection = qstn.ParentCollection,
                ParentSector = qstn.ParentSector,
                QCQuestionType = qstn.QCQuestionType,
                QCWebID = qstn.QCWebID,
                QuestionType = qstn.QuestionType,
                TemporaryDataProcess = qstn.TemporaryDataProcess,
                TopTableName = qstn.TopTableName,
                Sectors = qstn.Sectors,
                QCAnswerType = qstn.QCAnswerType
            };
            return dummyQuestion;
        }

        private QCAnswerType GetCustomQCAnswerType(string GTTableType)
        {
            QCAnswerType answerType = QCAnswerType.N;
            switch (GTTableType)
            {
                case GT.GTMTM:
                    answerType = QCAnswerType.MA;
                    break;
                case GT.GTMTN:
                    answerType = QCAnswerType.N;
                    break;
                case GT.GTMTS:
                    answerType = QCAnswerType.SA;
                    break;
                case GT.GTRAT:
                    answerType = QCAnswerType.N;
                    break;
                case GT.GTRNK:
                    answerType = QCAnswerType.SA;
                    break;
            }
            return answerType;
        }

        private string MaintainTableType(QuestionType questionType, string tableType)
        {
            if ((questionType & QuestionType.SA) == QuestionType.SA && tableType == GT.GTMTM)
            {
                tableType = GT.GTMTS;
            }

            return tableType;
        }

        private string ConvertToGraphTypeOfMtm(string graphType, QCQuestionType qcQuestionType, bool IsMatrixParent)
        {
            if (!IsMatrixParent)
                return graphType;

            if (!((qcQuestionType == QCQuestionType.MTM) || (qcQuestionType == QCQuestionType.MTS) || (qcQuestionType == QCQuestionType.RNK)))
                return graphType;

            return ConvertToGraphTypeOfMtmSub(graphType, ItemType.MTM);
        }

        private string ConvertToGraphTypeOfMtmSub(string graphType, ItemType itemType)
        {
            if (!(itemType == ItemType.MTM) || (itemType == null))
            {
                return graphType;
            }

            switch (graphType)
            {
                case GraphType.GRAPH_TYPE_QCWIDTHSTICK:
                    return GraphType.GRAPH_TYPE_QCWIDTHBELT;

                case GraphType.GRAPH_TYPE_QCHEIGHTSTICK:
                    return GraphType.GRAPH_TYPE_QCHEIGHTBELT;

                default:
                    return graphType;
            }
        }

        private void UpdateColorSettings(ref string graphType, ref string gradationType, ref string[] colorIndexArray)
        {
            colorIndexArray = null;
            gradationType = null;
            switch (graphType)
            {
                case null:
                    colorIndexArray = null;
                    gradationType = null;
                    break;
                case GraphType.GRAPH_TYPE_QCCIRCLE:
                case GraphType.GRAPH_TYPE_QCMCIRCLERAT: //RAT
                case GraphType.GRAPH_TYPE_QCMCIRCLE: //MTS
                    colorIndexArray = GTGraphColorIndex.CIRCLE.Split(',');
                    gradationType = GradationType.GRADATION_TYPE_NONE;
                    break;
                case GraphType.GRAPH_TYPE_QCWIDTHBELT:
                case GraphType.GRAPH_TYPE_QCWIDTHONSTICK:
                case GraphType.GRAPH_TYPE_SA_MATRIX_QCM_WIDTH_BELT_CODE_VALUE: //MTS
                    colorIndexArray = GTGraphColorIndex.BELT.Split(',');
                    gradationType = GradationType.GRADATION_TYPE_NONE;
                    break;
                case GraphType.GRAPH_TYPE_QCHEIGHTBELT:
                case GraphType.GRAPH_TYPE_QCHEIGHTONSTICK:
                case GraphType.GRAPH_TYPE_SA_MATRIX_QCM_HEIGHT_BELT_CODE_VALUE: //MTS
                    colorIndexArray = GTGraphColorIndex.BELT.Split(',');
                    gradationType = GradationType.GRADATION_TYPE_NONE;
                    break;
                //case GraphType.GRAPH_TYPE_QCWIDTHSTICK:
                //case GraphType.GRAPH_TYPE_QCHEIGHTSTICK:
                //    colorIndexArray = GTGraphColorIndex.STICK.Split(',');
                //    gradationType = GradationType.GRADATION_TYPE_NONE;
                //    break;
                case GraphType.GRAPH_TYPE_QCWIDTHSTICK:
                case GraphType.GRAPH_TYPE_QCMWIDTHSTICK:
                case GraphType.GRAPH_TYPE_QCWIDTHSTICKRAT: //RAT
                    colorIndexArray = GTGraphColorIndex.WIDTH_STICK_M.Split(',');
                    gradationType = GradationType.GRADATION_TYPE_NONE;
                    break;
                case GraphType.GRAPH_TYPE_QCHEIGHTSTICK:
                case GraphType.GRAPH_TYPE_QCMHEIGHTSTICK: //MTM
                case GraphType.GRAPH_TYPE_QCHEIGHTSTICKRAT: //RAT
                    colorIndexArray = GTGraphColorIndex.HEIGHT_STICK_M.Split(',');
                    gradationType = GradationType.GRADATION_TYPE_NONE;
                    break;
                case GraphType.GRAPH_TYPE_QCLINE: //MTM
                    colorIndexArray = GTGraphColorIndex.LINE.Split(',');
                    gradationType = GradationType.GRADATION_TYPE_NONE;
                    break;
                default:
                    graphType = null;
                    colorIndexArray = null;
                    gradationType = null;
                    break;
            }
        }

    } //- End Class
}
