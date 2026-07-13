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

namespace Qc4Launcher.Logic.MultiVariate
{
    public class PSMCalc
    {
        public delegate void OnWorkerMethodCompleteDelegate(double value, string status, bool isForceStop = false, bool retainThread = false, bool close = false, bool disableCancel = false);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static long MAX_PLOT = 10000;

        internal IntPtr childExcelApp = IntPtr.Zero;
        public void updateProgress(double currentProgress, string v, bool close = false)
        {
            OnWorkerComplete(currentProgress, v, close: close);
        }


        internal bool Tabulate(Workbook workBook, object worker, DoWorkEventArgs bgWorkerArg, System.Windows.Window window = null, IntPtr pb = default(IntPtr))
        {

            _log.Info("PSM analysis started");
            ExcelOperate excelOperate = null;
            Application xlApp = null;
            double currentProgress = 1;
            string errorMsg;
            Workbook baseBook=null;
            try
            {
                OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
                _log.Info("Populating dictionary");
                Definiotion.VariableDictionary = DictionaryUtil.PopulateQSDictionary(workBook);
                _log.Info("Populating dictionary completed");
                Questions questions = DictUpdate.GetQuestions(workBook);

                PSMSettings pSMSettings = MultiVariateSettingsReader.ReadPSMSettings(workBook, Definiotion.VariableDictionary);

                if (pSMSettings == null)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.NO_VALID_SETTINGS_FOUND, Enums.MessageType.ErrorOk, workBook, pb);
                    return false;
                }


                // TODO table name can be multivaraite 
                string tableName = "answers";
                if (DBHelper.checkAfterProcess(workBook))
                {
                    tableName = "data_after_process";
                }
                QCWebException ex;
                List<Data> highDataList = null;
                List<Data> cheapDataList = null;
                List<Data> tooHighDataList = null;
                List<Data> tooCheapDataList = null;
                QuestionSettings qstnDetHigh = Definiotion.VariableDictionary[pSMSettings.high];
                QuestionSettings qstnDetCheap = Definiotion.VariableDictionary[pSMSettings.cheap];
                QuestionSettings qstnDetTooHigh = Definiotion.VariableDictionary[pSMSettings.tooHigh];
                QuestionSettings qstnDetTooCheap = Definiotion.VariableDictionary[pSMSettings.tooCheap];
                double?[][] weightArray = null;
                currentProgress = 10;
                OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);
                if ((pSMSettings.questionType & QuestionType.SA) == QuestionType.SA)
                {
                    getWeightArray(qstnDetHigh, qstnDetCheap, qstnDetTooHigh, qstnDetTooCheap, ref weightArray);
                }

                bool res = ReadData(qstnDetHigh, qstnDetCheap, qstnDetTooHigh, qstnDetTooCheap, questions, workBook, tableName
                    , ref highDataList, ref cheapDataList, ref tooHighDataList, ref tooCheapDataList);

                if (!res)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.NO_VALID_SETTINGS_FOUND, Enums.MessageType.ErrorOk, workBook, pb);
                    return false;
                }

                currentProgress = 20;
                OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);

                _log.Info("Filter started");
                PSMOutputData outputData = new PSMOutputData();
                bool[] filterringFlag = null;
                removeUnknownandInvalid(workBook, pSMSettings, questions, tableName, ref filterringFlag);
                int count = filterringFlag.Count((y => y == true));

                if (pSMSettings.HasFilter)
                {
                    if (!checkVariableFilter(pSMSettings.Filters, Definiotion.VariableDictionary))
                    {
                        OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.GT_INVALID_FILTER_SETTINGS, Enums.MessageType.ErrorOk, workBook, pb);
                        return false;
                    }
                    string filterExp = CriteriaDescProvider.CreateCriteriaDescriptions(pSMSettings.Filters, questions);
                    new Criteria(filterExp, "", questions, filterringFlag != null ? Operator.opAnd : Operator.opOr).Filtering(ref filterringFlag, DBHelper.GetConnectionString(workBook), tableName);
                }

                count = filterringFlag.Count((y => y == true));
                outputData.dataCount = count;
                if (count == 0)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.PSM_VLD_CASE_IS_ZERO, Enums.MessageType.ErrorOk, workBook, pb);
                    return false;
                }

                removeInValidRange(ref filterringFlag, highDataList, cheapDataList, tooHighDataList, tooCheapDataList, pSMSettings, weightArray);

                extractData(filterringFlag, weightArray, pSMSettings, highDataList, cheapDataList, tooHighDataList, tooCheapDataList,
                    out double[] highDataArr, out double[] cheapDataArr
            , out double[] tooHighDataArr, out double[] tooCheapDataArr);
                count = filterringFlag.Count((y => y == true));
                outputData.validCount = count;
                _log.Info("Filter completed");

                if (count == 0)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.PSM_NO_DATA, Enums.MessageType.ErrorOk, workBook, pb);
                    return false;
                }

                currentProgress = 30;
                OnWorkerComplete(currentProgress, LocalResource.PSM_PB_CALC_STAT);

                double[][] extractedData = new double[4][];
                extractedData[0] = highDataArr;
                extractedData[1] = cheapDataArr;
                extractedData[2] = tooHighDataArr;
                extractedData[3] = tooCheapDataArr;

                excelOperate = new ExcelOperate();
                xlApp = excelOperate.Excel;

                double?[,] statasticData = new double?[4, 13];

                string strStatisticsErrorMsg = calcStastics(pSMSettings, xlApp, extractedData, ref statasticData);
                outputData.statasticData = statasticData;

                currentProgress = 40;
                OnWorkerComplete(currentProgress, LocalResource.PSM_PB_CALC_PSM);

                double[][] psmData;
                double[] allPric;
                errorMsg = priceRangeCalcInitData(pSMSettings, extractedData, out psmData, out allPric);
                if (errorMsg.Length > 0)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PSM_PB_CALC_PSM);
                    MessageDialog.ShowMessageOnWorkBook(errorMsg, Enums.MessageType.ErrorOk, workBook, pb);
                    return false;
                }
                double[][] psmDataPercentage = new double[4][];
                double[][] psmDataPercentageBefore = new double[4][];
                double[][] allPricPercentage = new double[4][];
                priceRangeCalcSetData(pSMSettings, extractedData, psmData, allPric, statasticData, ref psmDataPercentage, ref psmDataPercentageBefore, ref allPricPercentage);
                outputData.psmData = psmData;
                outputData.psmDataPercentage = psmDataPercentage;
                outputData.psmDataPercentageBefore = psmDataPercentageBefore;
                double[,] NumericalArray;
                PSM_Init_GetNumericalArray(pSMSettings, extractedData, statasticData, out NumericalArray);
                outputData.NumericalArray = NumericalArray;
                double[] ResultPrices;
                errorMsg = PSM_Main(pSMSettings, allPricPercentage, allPric, out ResultPrices);

                if (errorMsg.Length > 0)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PSM_PB_CALC_PSM);
                    MessageDialog.ShowMessageOnWorkBook(errorMsg, Enums.MessageType.ErrorOk, workBook, pb);
                    return false;
                }

                outputData.ResultPrices = ResultPrices;
                currentProgress = 50;
                OnWorkerComplete(currentProgress, LocalResource.PSM_PB_RESLT_OUT);


                childExcelApp = (IntPtr)xlApp.Hwnd;
                xlApp.Visible = false;
                xlApp.ScreenUpdating = false;
                //xlApp.Calculation = XlCalculation.xlCalculationManual;
                xlApp.EnableEvents = false;
                //xlApp.DisplayStatusBar = false;
                xlApp.PrintCommunication = false;
                xlApp.DisplayAlerts = false;

                PSMOutput pSMOutput = new PSMOutput();
               
                pSMOutput.generateOutPut(System.AppContext.BaseDirectory, xlApp, workBook, outputData, pSMSettings, questions, this, out baseBook);
                childExcelApp = (IntPtr)xlApp.Hwnd;

                this.OnWorkerComplete(100, LocalResource.PSM_PB_RESLT_OUT, true, true, true);
                xlApp.EnableEvents = true;
                //xlApp.DisplayStatusBar = true;
                xlApp.PrintCommunication = true;
                //xlApp.Calculation = XlCalculation.xlCalculationAutomatic;
                xlApp.WindowState = XlWindowState.xlMaximized;
                xlApp.ScreenUpdating = true;
                xlApp.DisplayAlerts = true;
                this.OnWorkerComplete(100, LocalResource.PSM_PB_RESLT_OUT, true);
                xlApp.Visible = true;
                if (strStatisticsErrorMsg.Length > 0)
                    MessageDialog.ShowMessageOnWorkBook(strStatisticsErrorMsg, Enums.MessageType.ErrorOk, baseBook);


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
                }
                finally
                {
                    MessageDialog.ShowMessageOnWorkBook(exMsg, Enums.MessageType.ErrorOk, workBook, pb);
                    if (excelOperate != null)
                    {
                        excelOperate.Dispose();
                    }
                }
            }
            finally
            {
              
                if (baseBook != null)
                {
                    COMWholeOperate.releaseComObject(ref baseBook);
                }
                if (excelOperate != null)
                {
                    try
                    {
                        COMWholeOperate.releaseComObject(ref excelOperate);
                    }
                    catch { }
                }
                if (xlApp != null)
                {
                    try
                    { COMWholeOperate.releaseComObject<Application>(ref xlApp); }
                    catch { }
                }
                this.OnWorkerComplete(100, LocalResource.PSM_PB_RESLT_OUT, true);
                _log.Info("PSM analysis completed");
            }
            return true;
        }

        private bool ReadData(QuestionSettings qstnDetHigh, QuestionSettings qstnDetCheap, QuestionSettings qstnDetTooHigh, QuestionSettings qstnDetTooCheap,
            Questions questions, Workbook workBook, string tableName, ref List<Data> highDataList, ref List<Data> cheapDataList, ref List<Data> tooHighDataList,
            ref List<Data> tooCheapDataList)
        {
            QCWebException ex;
            bool isMv = false;
            string tableNameAnswer = tableName;
            using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
            {
                con.Open();
                try
                {

                    if (!checkQuestion(qstnDetHigh, workBook) || !checkQuestion(qstnDetCheap, workBook)
                        || !checkQuestion(qstnDetTooHigh, workBook) || !checkQuestion(qstnDetTooCheap, workBook))
                    {
                        _log.Warn("some questins not exist");
                        return false;
                    }

                    Question high = (Question)questions[qstnDetHigh.Id];
                    System.Data.DataTable dataTble = new System.Data.DataTable();
                    tableName = DBHelperCommon.getTableName(workBook, high.ColumnName, out isMv);
                    if (!isMv)
                    {
                        dataTble = DBHelper.GetDataTable("Select " + high.ColumnName + " from " + tableName + " order by sort_no ", con);
                    }
                    else
                    {
                        dataTble = DBHelper.GetDataTable("Select " + high.ColumnName + " from " + tableNameAnswer + " a join "
                            + tableName + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                    }
                    highDataList = ReadTextFile.ReadDataTable(dataTble, high.QuestionType, null, out ex);
                    if (ex != null)
                    {
                        _log.Warn(ex.Message);
                        return false;
                    }

                    Question cheap = (Question)questions[qstnDetCheap.Id];
                    dataTble = new System.Data.DataTable();
                    tableName = DBHelperCommon.getTableName(workBook, cheap.ColumnName, out isMv);
                    if (!isMv)
                    {
                        dataTble = DBHelper.GetDataTable("Select " + cheap.ColumnName + " from " + tableName + " order by sort_no ", con);
                    }
                    else
                    {
                        dataTble = DBHelper.GetDataTable("Select " + cheap.ColumnName + " from " + tableNameAnswer + " a join "
                            + tableName + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                    }
                    cheapDataList = ReadTextFile.ReadDataTable(dataTble, cheap.QuestionType, null, out ex);
                    if (ex != null)
                    {
                        _log.Warn(ex.Message);
                        return false;
                    }

                    Question tooHigh = (Question)questions[qstnDetTooHigh.Id];
                    dataTble = new System.Data.DataTable();
                    tableName = DBHelperCommon.getTableName(workBook, tooHigh.ColumnName, out isMv);
                    if (!isMv)
                    {
                        dataTble = DBHelper.GetDataTable("Select " + tooHigh.ColumnName + " from " + tableName + " order by sort_no ", con);
                    }
                    else
                    {
                        dataTble = DBHelper.GetDataTable("Select " + tooHigh.ColumnName + " from " + tableNameAnswer + " a join "
                            + tableName + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                    }
                    tooHighDataList = ReadTextFile.ReadDataTable(dataTble, tooHigh.QuestionType, null, out ex);
                    if (ex != null)
                    {
                        _log.Warn(ex.Message);
                        return false;
                    }

                    Question tooCheap = (Question)questions[qstnDetTooCheap.Id];
                    dataTble = new System.Data.DataTable();
                    tableName = DBHelperCommon.getTableName(workBook, tooCheap.ColumnName, out isMv);
                    if (!isMv)
                    {
                        dataTble = DBHelper.GetDataTable("Select " + tooCheap.ColumnName + " from " + tableName + " order by sort_no ", con);
                    }
                    else
                    {
                        dataTble = DBHelper.GetDataTable("Select " + tooCheap.ColumnName + " from " + tableNameAnswer + " a join "
                            + tableName + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                    }
                    tooCheapDataList = ReadTextFile.ReadDataTable(dataTble, tooCheap.QuestionType, null, out ex);
                    if (ex != null)
                    {
                        _log.Warn(ex.Message);
                        return false;
                    }
                }
                catch (Exception sql)
                {
                    _log.Warn(sql.Message);
                    return false;
                }
                return true;
            }
        }

        private void getWeightArray(QuestionSettings qstnDetHigh, QuestionSettings qstnDetCheap, QuestionSettings qstnDetTooHigh,
            QuestionSettings qstnDetTooCheap, ref double?[][] weightArray)
        {
            weightArray = new double?[4][];
            if (qstnDetHigh.Score.Length != 0)
            {
                double?[] weightDouble = new double?[qstnDetHigh.CategoryCount];
                string[] weight = qstnDetHigh.Score.Split(new char[] { ',' });
                for (int i = 0; i < weight.Length; i++)
                {
                    if (!string.IsNullOrEmpty(weight[i]))
                    {
                        double val = 0;
                        Double.TryParse(weight[i], out val);
                        weightDouble[i] = val;
                    }
                    else
                    {
                        weightDouble[i] = null;
                    }
                }
                weightArray[0] = weightDouble;
            }
            if (qstnDetCheap.Score.Length != 0)
            {
                double?[] weightDouble = new double?[qstnDetCheap.CategoryCount];
                string[] weight = qstnDetCheap.Score.Split(new char[] { ',' });
                for (int i = 0; i < weight.Length; i++)
                {
                    if (!string.IsNullOrEmpty(weight[i]))
                    {
                        double val = 0;
                        Double.TryParse(weight[i], out val);
                        weightDouble[i] = val;
                    }
                    else
                    {
                        weightDouble[i] = null;
                    }
                }
                weightArray[1] = weightDouble;
            }
            if (qstnDetTooHigh.Score.Length != 0)
            {
                double?[] weightDouble = new double?[qstnDetTooHigh.CategoryCount];
                string[] weight = qstnDetTooHigh.Score.Split(new char[] { ',' });
                for (int i = 0; i < weight.Length; i++)
                {
                    if (!string.IsNullOrEmpty(weight[i]))
                    {
                        double val = 0;
                        Double.TryParse(weight[i], out val);
                        weightDouble[i] = val;
                    }
                    else
                    {
                        weightDouble[i] = null;
                    }
                }
                weightArray[2] = weightDouble;
            }
            if (qstnDetTooCheap.Score.Length != 0)
            {
                double?[] weightDouble = new double?[qstnDetTooCheap.CategoryCount];
                string[] weight = qstnDetTooCheap.Score.Split(new char[] { ',' });
                for (int i = 0; i < weight.Length; i++)
                {
                    if (!string.IsNullOrEmpty(weight[i]))
                    {
                        double val = 0;
                        Double.TryParse(weight[i], out val);
                        weightDouble[i] = val;
                    }
                    else
                    {
                        weightDouble[i] = null;
                    }
                }
                weightArray[3] = weightDouble;
            }
        }

        private string PSM_Main(PSMSettings pSMSettings, double[][] allPricPercentage, double[] allPrice, out double[] ResultPrices)
        {
            ResultPrices = new double[4];
            int priceType;
            long Cheaper_i = 0;
            long Expensive_i = 0;
            long T_Col;
            long T_Col_End;
            string errorMsg = "";
            for (priceType = 0; priceType < 4; priceType++)
            {
                switch (priceType)
                {
                    case 0:
                        {
                            Cheaper_i = 1;// ePSMCalc_Cheap;
                            Expensive_i = 2;//ePSMCalc_TooHigh;
                            break;
                        }

                    case 1:
                        {
                            Cheaper_i = 3;//ePSMCalc_TooCheap;
                            Expensive_i = 0;//ePSMCalc_High;
                            break;
                        }

                    case 2:
                        {
                            Cheaper_i = 3;//ePSMCalc_TooCheap;
                            Expensive_i = 2;// ePSMCalc_TooHigh;
                            break;
                        }

                    case 3:
                        {
                            Cheaper_i = 1;//ePSMCalc_Cheap;
                            Expensive_i = 0;//ePSMCalc_High;
                            break;
                        }
                }

                for (T_Col = 0; T_Col < allPricPercentage[0].Length; T_Col++)
                {
                    if (allPricPercentage[Cheaper_i][T_Col] <= allPricPercentage[Expensive_i][T_Col])
                    {
                        break;
                    }
                }

                if (T_Col == 0 || T_Col >= allPricPercentage[0].Length)
                {
                    errorMsg = LocalResource.PSM_NO_INTERSECTION;
                    return errorMsg;
                }

                for (T_Col_End = allPricPercentage[0].Length - 1; T_Col_End >= 0; T_Col_End -= 1)
                {
                    if (allPricPercentage[Cheaper_i][T_Col_End] >= allPricPercentage[Expensive_i][T_Col_End])
                    {
                        break;
                    }
                }

                switch (priceType)
                {
                    case 0:
                        {
                            if (allPricPercentage[Expensive_i][T_Col] == allPricPercentage[Expensive_i][T_Col - 1] && pSMSettings.invertHighAndCheap == false)
                            {
                                ResultPrices[priceType] = allPrice[T_Col - 1];
                            }
                            else
                            {
                                ResultPrices[priceType] = allPrice[T_Col];
                            }

                            break;
                        }

                    case 1:
                        {
                            if (allPricPercentage[Cheaper_i][T_Col_End] == allPricPercentage[Cheaper_i][T_Col_End + 1] && pSMSettings.invertHighAndCheap == false)
                            {
                                ResultPrices[priceType] = allPrice[T_Col_End + 1];
                            }
                            else
                            {
                                ResultPrices[priceType] = allPrice[T_Col_End];
                            }

                            break;
                        }

                    default:
                        {
                            if (allPricPercentage[Cheaper_i][T_Col] == allPricPercentage[Expensive_i][T_Col])
                            {
                                if (T_Col == T_Col_End)
                                {
                                    ResultPrices[priceType] = allPrice[T_Col];
                                }
                                else
                                {
                                    ResultPrices[priceType] = (allPrice[T_Col] + allPrice[T_Col_End]) / 2;
                                }
                            }
                            else
                            {
                                if (priceType == 2 || pSMSettings.invertHighAndCheap == false)
                                {
                                    if (allPricPercentage[Cheaper_i][T_Col] == allPricPercentage[Cheaper_i][T_Col - 1])
                                    {
                                        ResultPrices[priceType] = allPrice[T_Col];
                                    }
                                    else
                                    {
                                        ResultPrices[priceType] = allPrice[T_Col - 1];
                                    }
                                }
                                else
                                {
                                    if (allPricPercentage[Expensive_i][T_Col] == allPricPercentage[Expensive_i][T_Col - 1])
                                    {
                                        ResultPrices[priceType] = allPrice[T_Col];
                                    }
                                    else
                                    {
                                        ResultPrices[priceType] = allPrice[T_Col - 1];
                                    }
                                }
                            }
                            break;
                        }
                }
            }
            return errorMsg;
        }



        private void PSM_Init_GetNumericalArray(PSMSettings pSMSettings, double[][] extractedData, double?[,] statasticData, out double[,] NumericalArray)
        {
            Double T_Price;
            long NumericalCnt;
            long IntervalCnt;
            if (Math.Floor((pSMSettings.maxPrice - pSMSettings.minPrice) / pSMSettings.scaleInterval)
                == (pSMSettings.maxPrice - pSMSettings.minPrice) / pSMSettings.scaleInterval)
            {
                NumericalCnt = Convert.ToInt64((pSMSettings.maxPrice - pSMSettings.minPrice) / pSMSettings.scaleInterval) + 1;
            }
            else
            {
                NumericalCnt = Convert.ToInt64(Math.Ceiling((pSMSettings.maxPrice - pSMSettings.minPrice) / pSMSettings.scaleInterval)) + 1;
            }

            NumericalArray = new double[5, NumericalCnt];
            for (IntervalCnt = 0; IntervalCnt < NumericalCnt; IntervalCnt++)
            {

                T_Price = pSMSettings.minPrice + (IntervalCnt) * pSMSettings.scaleInterval;
                if (T_Price > pSMSettings.maxPrice) T_Price = pSMSettings.maxPrice;
                NumericalArray[0, IntervalCnt] = T_Price;
                if (pSMSettings.invertHighAndCheap == true)
                {
                    NumericalArray[1, IntervalCnt] = 1 - PSM_CalcPercent(extractedData[1], 1, T_Price, statasticData[1, 12]);
                    NumericalArray[2, IntervalCnt] = 1 - PSM_CalcPercent(extractedData[0], 0, T_Price, statasticData[0, 12]);
                }
                else
                {
                    NumericalArray[1, IntervalCnt] = PSM_CalcPercent(extractedData[0], 0, T_Price, statasticData[0, 12]);
                    NumericalArray[2, IntervalCnt] = PSM_CalcPercent(extractedData[1], 1, T_Price, statasticData[1, 12]);
                }

                NumericalArray[3, IntervalCnt] = PSM_CalcPercent(extractedData[2], 0, T_Price, statasticData[2, 12]);
                NumericalArray[4, IntervalCnt] = PSM_CalcPercent(extractedData[3], 1, T_Price, statasticData[3, 12]);
            }
        }

        private string calcStastics(PSMSettings pSMSettings, Application xlApp, double[][] extractedData, ref double?[,] statasticData)
        {
            string strStatisticsErrorMsg = "";
            string[] StatisticedErrorAry = new string[8];
            string StatisticedPrefix = LocalResource.PSM_CANNOT_CALC_STASTICS;
            StatisticedErrorAry[1] = LocalResource.PSM_CANNOT_CALC_STASTICS_STD_ERR;//  "Standard error of [ {0} ] variable: could not be calculated because the standard deviation cannot be calculated.";
            StatisticedErrorAry[3] = LocalResource.PSM_CANNOT_CALC_STASTICS_MOD;// "Mode of [ {0} ] variable: could not be calculated because all the cases are different.";
            StatisticedErrorAry[4] = LocalResource.PSM_CANNOT_CALC_STASTICS_STD_DEV; // "Standard deviation of [ {0} ] variable: could not be calculated because the Number of Valid Cases was equal to or less than 1.";
            StatisticedErrorAry[5] = LocalResource.PSM_CANNOT_CALC_STASTICS_VARNCE; //"Variance of [ {0} ] variable: could not be calculated because the standard deviation cannot be calculated.";
            StatisticedErrorAry[6] = LocalResource.PSM_CANNOT_CALC_STASTICS_KURT;// "Kurtosis of [ {0} ] variable: could not be calculated because the Number of Valid Cases was equal to or less than 3 or the standard deviation is 0.";
            StatisticedErrorAry[7] = LocalResource.PSM_CANNOT_CALC_STASTICS_SKEW; //"Skewness of [ {0} ] variable: could not be calculated because the Number of Valid Cases was equal to or less than 2 or the standard deviation is 0.";
            string[] DataPositionAry = new string[4];
            DataPositionAry[0] = LocalResource.PSM_EXPENSIVE;// "Expensive";
            DataPositionAry[1] = LocalResource.PSM_CHEAP;//"Cheap";
            DataPositionAry[2] = LocalResource.PSM_TOO_EXPENSIEVE;//"Too Expensive";
            DataPositionAry[3] = LocalResource.PSM_TOO_CHEAP;//"Too Cheap";

            for (int i = 0; i < extractedData.Length; i++)
            {
                double[] dataArr = extractedData[i];
                double avg = Statastic.Average(dataArr);
                statasticData[i, 0] = avg;
                double min = Statastic.Min(dataArr);
                statasticData[i, 9] = min;
                double max = Statastic.Max(dataArr);
                statasticData[i, 10] = max;
                double range = Statastic.Range(dataArr);
                statasticData[i, 8] = range;
                double sum = Statastic.Sum(dataArr);
                statasticData[i, 11] = sum;
                double count = Statastic.Count(dataArr);
                statasticData[i, 12] = count;
                double median = 0;
                double mode = 0;
                Statastic.Medain(dataArr, xlApp, ref median, ref mode);
                statasticData[i, 2] = median;
                statasticData[i, 3] = mode;
                double? stdDev = Statastic.StdDev(dataArr, xlApp);


                statasticData[i, 4] = double.NaN;
                statasticData[i, 5] = double.NaN;
                statasticData[i, 1] = double.NaN;
                statasticData[i, 6] = double.NaN;
                statasticData[i, 7] = double.NaN;
                if (stdDev != null)
                {
                    //if (double.IsNaN(Convert.ToDouble(stdDev)))
                    //{
                    //    stdDev = 0.0;
                    //}
                    double stdDevC = Convert.ToDouble(stdDev);
                    statasticData[i, 4] = stdDevC;
                    double dispersionVariance = stdDevC * stdDevC;
                    statasticData[i, 5] = dispersionVariance;
                    double stdErr = stdDevC / Math.Sqrt(count);
                    statasticData[i, 1] = stdErr;
                    if (stdDevC > 0)
                    {
                        double kurtosis = Statastic.Kurtosis(dataArr, count, avg, stdDevC);
                        double distortionSkewnes = Statastic.SkewValue(dataArr, count, avg, stdDevC);
                        statasticData[i, 6] = kurtosis;
                        statasticData[i, 7] = distortionSkewnes;
                    }
                }
                for (int j = 1; j < 8; j++)
                {
                    double val = Convert.ToDouble(statasticData[i, j]);
                    if (double.IsNaN(val) || double.IsInfinity(val))
                    {
                        strStatisticsErrorMsg = strStatisticsErrorMsg + string.Format(StatisticedErrorAry[j], DataPositionAry[i]) + "\n";
                    }
                }
            }
            if (strStatisticsErrorMsg.Length > 0) strStatisticsErrorMsg = StatisticedPrefix + strStatisticsErrorMsg;
            return strStatisticsErrorMsg;
        }

        private void priceRangeCalcSetData(PSMSettings pSMSettings, double[][] extractedData, double[][] psmData, double[] allPric, double?[,] statasticData,
           ref double[][] psmDataPercentage, ref double[][] psmDataPercentageBefore, ref double[][] allPricPercentage)
        {
            double T_Price;
            long PriceCnt;
            long CalcArrayCnt;
            double[] TmpPSMArray;
            double TmpPercent;
            double[] TmpCalcAry;
            long i;
            int compType = 0;
            for (i = 0; i < extractedData.Length; i++)
            {
                TmpPSMArray = psmData[i];
                double[] TmpPSMArrayPer = new double[TmpPSMArray.Length];
                psmDataPercentage[i] = TmpPSMArrayPer;
                double[] TmpAllPricPer = new double[allPric.Length];
                allPricPercentage[i] = TmpAllPricPer;
                CalcArrayCnt = 0; //LBound(PSMArray_For_Calc, 2);
                switch (i)
                {
                    case 0:
                    case 2:
                        {
                            compType = 0;
                            break;
                        }

                    case 1:
                    case 3:
                        {
                            compType = 1;
                            break;
                        }
                }
                for (PriceCnt = 0; PriceCnt < TmpPSMArray.Length; PriceCnt++)
                {
                    T_Price = TmpPSMArray[PriceCnt];
                    TmpPercent = PSM_CalcPercent(extractedData[i], compType, T_Price, statasticData[i, 12]);
                    TmpPSMArrayPer[PriceCnt] = TmpPercent;
                    for (; CalcArrayCnt < allPric.Length; CalcArrayCnt++)
                    {
                        if (allPric[CalcArrayCnt] <= T_Price)
                            TmpAllPricPer[CalcArrayCnt] = TmpPercent;
                        if (allPric[CalcArrayCnt] >= T_Price)
                        {
                            CalcArrayCnt += 1;
                            break;
                        }
                    }
                }
            }

            if (pSMSettings.invertHighAndCheap == true)
            {
                for (i = 0; i < 2; i++)
                {
                    psmDataPercentageBefore[i] = psmDataPercentage[i].ToArray();
                }
                for (i = 0; i < 2; i++)
                {
                    TmpPSMArray = psmDataPercentage[i];
                    TmpCalcAry = allPricPercentage[i];

                    // if (i == 0)
                    //  TmpPSMArray(2, LBound(TmpPSMArray, 2)) = str_NotHigh;
                    // else
                    // TmpPSMArray(2, LBound(TmpPSMArray, 2)) = str_NotCheap;
                    for (PriceCnt = 0; PriceCnt < TmpPSMArray.Length; PriceCnt++)
                        TmpPSMArray[PriceCnt] = 1 - TmpPSMArray[PriceCnt];
                    psmDataPercentage[i] = TmpPSMArray;
                    for (CalcArrayCnt = 0; CalcArrayCnt < allPric.Length; CalcArrayCnt++)
                        TmpCalcAry[CalcArrayCnt] = 1 - TmpCalcAry[CalcArrayCnt];
                    allPricPercentage[i] = TmpCalcAry;
                }

                TmpPSMArray = psmDataPercentage[0];
                psmDataPercentage[0] = psmDataPercentage[1];
                psmDataPercentage[1] = TmpPSMArray;

                TmpPSMArray = psmData[0];
                psmData[0] = psmData[1];
                psmData[1] = TmpPSMArray;

                TmpCalcAry = allPricPercentage[0];
                allPricPercentage[0] = allPricPercentage[1];
                allPricPercentage[1] = TmpCalcAry;
            }
        }

        private double PSM_CalcPercent(double[] TargetArray, int compType, double TargetValue, double? AggregateAmount)
        {
            long RetCnt;
            RetCnt = 0;
            foreach (double TmpValue in TargetArray)
            {
                if (compType == 0)
                {
                    if (TmpValue <= TargetValue)
                        RetCnt = RetCnt + 1;
                }
                else if (TmpValue >= TargetValue)
                    RetCnt = RetCnt + 1;
            }

            return RetCnt / Convert.ToDouble(AggregateAmount);
        }

        private string priceRangeCalcInitData(PSMSettings pSMSettings, double[][] extractedData, out double[][] psmData, out double[] allPrice)
        {
            double[] minMax = { pSMSettings.minPrice, pSMSettings.maxPrice };
            psmData = new double[4][];
            allPrice = new double[2] { pSMSettings.minPrice, pSMSettings.maxPrice };

            for (int i = 0; i < extractedData.Length; i++)
            {
                IDictionary<double, double> PricesDic = new Dictionary<double, double>();
                double[] extData = extractedData[i];
                extData = extData.Concat(minMax).ToArray();
                extData = extData.Distinct().ToArray();
                for (int j = 0; j < extData.Length; j++)
                {
                    double price = extData[j];
                    if (price >= pSMSettings.minPrice & price <= pSMSettings.maxPrice)
                    {
                        if (PricesDic.ContainsKey(price) == false)
                            PricesDic.Add(price, price);
                        switch (i)
                        {
                            case 0:
                            case 2:
                                {
                                    price = price - 0.00001;
                                    break;
                                }

                            case 1:
                            case 3:
                                {
                                    price = price + 0.00001;
                                    break;
                                }
                        }

                        if (price >= pSMSettings.minPrice & price <= pSMSettings.maxPrice)
                        {
                            if (PricesDic.ContainsKey(price) == false)
                                PricesDic.Add(price, price);
                        }
                    }
                }

                if (PricesDic.Count > MAX_PLOT)
                {
                    return LocalResource.PSM_RETRY_AFTER_NARROW_RANGE;
                }

                extData = PricesDic.Keys.ToArray();
                allPrice = allPrice.Concat(extData).ToArray();
                extData = extData.OrderBy(c => c).ToArray();
                psmData[i] = extData;
            }

            allPrice = allPrice.Distinct().ToArray();
            allPrice = allPrice.OrderBy(c => c).ToArray();
            return "";
        }

        private void removeUnknownandInvalid(Workbook workBook, PSMSettings pSMSettings, Questions questions, String tableName, ref bool[] filterringFlag)
        {
            List<FilterSettingsCr> FiltersDkAndStar = new List<FilterSettingsCr>();

            FilterSettingsCr fs11 = new FilterSettingsCr();
            fs11.variable = pSMSettings.high;
            fs11.operatorType = "<>";
            fs11.values = "*";
            fs11.conditionType = AND;
            FiltersDkAndStar.Add(fs11);

            FilterSettingsCr fs12 = new FilterSettingsCr();
            fs12.variable = pSMSettings.high;
            fs12.operatorType = "<>";
            fs12.values = "DK";
            fs12.conditionType = AND;
            FiltersDkAndStar.Add(fs12);

            FilterSettingsCr fs21 = new FilterSettingsCr();
            fs21.variable = pSMSettings.cheap;
            fs21.operatorType = "<>";
            fs21.values = "*";
            fs21.conditionType = AND;
            FiltersDkAndStar.Add(fs21);

            FilterSettingsCr fs22 = new FilterSettingsCr();
            fs22.variable = pSMSettings.cheap;
            fs22.operatorType = "<>";
            fs22.values = "DK";
            fs22.conditionType = AND;
            FiltersDkAndStar.Add(fs22);

            FilterSettingsCr fs31 = new FilterSettingsCr();
            fs31.variable = pSMSettings.tooHigh;
            fs31.operatorType = "<>";
            fs31.values = "*";
            fs31.conditionType = AND;
            FiltersDkAndStar.Add(fs31);

            FilterSettingsCr fs32 = new FilterSettingsCr();
            fs32.variable = pSMSettings.tooHigh;
            fs32.operatorType = "<>";
            fs32.values = "DK";
            fs32.conditionType = AND;
            FiltersDkAndStar.Add(fs32);

            FilterSettingsCr fs41 = new FilterSettingsCr();
            fs41.variable = pSMSettings.tooCheap;
            fs41.operatorType = "<>";
            fs41.values = "*";
            fs41.conditionType = AND;
            FiltersDkAndStar.Add(fs41);

            FilterSettingsCr fs42 = new FilterSettingsCr();
            fs42.variable = pSMSettings.tooCheap;
            fs42.operatorType = "<>";
            fs42.values = "DK";
            fs42.conditionType = AND;
            FiltersDkAndStar.Add(fs42);

            string filterExp = CriteriaDescProvider.CreateCriteriaDescriptions(FiltersDkAndStar, questions);
            filterringFlag = new Criteria(filterExp, "", questions).Filtering(DBHelper.GetConnectionString(workBook), tableName: tableName);
        }

        private void extractData(bool[] filterringFlag, double?[][] weightArray, PSMSettings pSMSettings, List<Data> highDataList,
            List<Data> cheapDataList, List<Data> tooHighDataList, List<Data> tooCheapDataList, out double[] highDataArr, out double[] cheapDataArr
            , out double[] tooHighDataArr, out double[] tooCheapDataArr)
        {
            int count = filterringFlag.Count((y => y == true));
            highDataArr = new double[count];
            cheapDataArr = new double[count];
            tooHighDataArr = new double[count];
            tooCheapDataArr = new double[count];
            int k = 0;

            if ((pSMSettings.questionType & QuestionType.SA) == QuestionType.SA)
            {
                for (int i = 0; i < filterringFlag.Length; i++)
                {
                    if (filterringFlag[i] == true)
                    {
                        highDataArr[k] = Convert.ToDouble( weightArray[0][(highDataList[i] as SAData).Value - 1]);
                        cheapDataArr[k] = Convert.ToDouble(weightArray[1][(cheapDataList[i] as SAData).Value - 1]);
                        tooHighDataArr[k] = Convert.ToDouble(weightArray[2][(tooHighDataList[i] as SAData).Value - 1]);
                        tooCheapDataArr[k] = Convert.ToDouble(weightArray[3][(tooCheapDataList[i] as SAData).Value - 1]);
                        k++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < filterringFlag.Length; i++)
                {
                    if (filterringFlag[i] == true)
                    {
                        highDataArr[k] = (highDataList[i] as NData).Value;
                        cheapDataArr[k] = (cheapDataList[i] as NData).Value;
                        tooHighDataArr[k] = (tooHighDataList[i] as NData).Value;
                        tooCheapDataArr[k] = (tooCheapDataList[i] as NData).Value;
                        k++;
                    }
                }
            }
        }

        private void removeInValidRange(ref bool[] filterringFlag, List<Data> highDataList, List<Data> cheapDataList,
            List<Data> tooHighDataList, List<Data> tooCheapDataList, PSMSettings pSMSettings, double?[][] weightArray)
        {
            for (int i = 0; i < filterringFlag.Length; i++)
            {
                if (filterringFlag[i] == false) continue;
                double highValue = 0.0;
                double cheapValue = 0.0;
                double tooHighValue = 0.0;
                double tooCheapValue = 0.0;
                if (highDataList[i].GetType() == typeof(SAData))
                {
                    if (weightArray[0][(highDataList[i] as SAData).Value - 1] == null)
                    {
                        filterringFlag[i] = false;
                    }
                    else
                    {
                        highValue = Convert.ToDouble(weightArray[0][(highDataList[i] as SAData).Value - 1]);
                    }
                    if (weightArray[1][(cheapDataList[i] as SAData).Value - 1] == null)
                    {
                        filterringFlag[i] = false;
                    }
                    else
                    {
                        cheapValue = Convert.ToDouble(weightArray[1][(cheapDataList[i] as SAData).Value - 1]);
                    }
                    if (weightArray[2][(tooHighDataList[i] as SAData).Value - 1] == null)
                    {
                        filterringFlag[i] = false;
                    }
                    else
                    {
                        tooHighValue = Convert.ToDouble(weightArray[2][(tooHighDataList[i] as SAData).Value - 1]);
                    }
                    if (weightArray[3][(tooCheapDataList[i] as SAData).Value - 1] == null)
                    {
                        filterringFlag[i] = false;
                    }
                    else
                    {
                        tooCheapValue = Convert.ToDouble(weightArray[3][(tooCheapDataList[i] as SAData).Value - 1]);
                    }
                }
                else if (highDataList[i].GetType() == typeof(NData))
                {
                    highValue = (highDataList[i] as NData).Value;
                    cheapValue = (cheapDataList[i] as NData).Value;
                    tooHighValue = (tooHighDataList[i] as NData).Value;
                    tooCheapValue = (tooCheapDataList[i] as NData).Value;
                }

                if (tooCheapValue >= cheapValue)
                {
                    filterringFlag[i] = false;
                }
                else if (cheapValue >= highValue)
                {
                    filterringFlag[i] = false;
                }
                else if (highValue >= tooHighValue)
                {
                    filterringFlag[i] = false;
                }
                else if (pSMSettings.effValHighStart >= 0 && pSMSettings.effValHighStart > highValue)
                {
                    filterringFlag[i] = false;
                }
                else if (pSMSettings.effValHighEnd >= 0 && pSMSettings.effValHighEnd < highValue)
                {
                    filterringFlag[i] = false;
                }

                else if (pSMSettings.effValCheapStart >= 0 && pSMSettings.effValCheapStart > cheapValue)
                {
                    filterringFlag[i] = false;
                }
                else if (pSMSettings.effValCheapEnd >= 0 && pSMSettings.effValCheapEnd < cheapValue)
                {
                    filterringFlag[i] = false;
                }

                else if (pSMSettings.effValTooHighStart >= 0 && pSMSettings.effValTooHighStart > tooHighValue)
                {
                    filterringFlag[i] = false;
                }
                else if (pSMSettings.effValTooHighEnd >= 0 && pSMSettings.effValTooHighEnd < tooHighValue)
                {
                    filterringFlag[i] = false;
                }

                else if (pSMSettings.effValTooCheapStart >= 0 && pSMSettings.effValTooCheapStart > tooCheapValue)
                {
                    filterringFlag[i] = false;
                }
                else if (pSMSettings.effValTooCheapEnd >= 0 && pSMSettings.effValTooCheapEnd < tooCheapValue)
                {
                    filterringFlag[i] = false;
                }

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

        private bool checkQuestion(QuestionSettings qstnDet, Workbook workBook)
        {
            List<QuestionSettings> qlist = new List<QuestionSettings>();
            qlist.Add(qstnDet);
            QC4Common.DB.DBHelper.CheckIfColumnExists(workBook, qlist, out List<string> variables, out List<string> columns, out List<decimal> idss);
            if (variables.Count == 0) { return true; }
            return false;
        }
    }
}
