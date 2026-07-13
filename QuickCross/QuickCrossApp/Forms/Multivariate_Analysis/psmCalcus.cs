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
using QC4Common.DB;
using QC4Common.Model;
using QC4Common.Util;
using Qc4Launcher.Logic.MultiVariate;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Macromill.QCWeb.Question.Questions;
using static Qc4Launcher.Logic.CrossSettingsReader;
using DBHelperCommon = QC4Common.DB.DBHelper;

namespace Qc4Launcher.Forms.Multivariate_Analysis
{
    class psmCalcus
    {

        Application xlApp = null;


        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static string AND = "&";
        public static string OR = "|";

        public bool psmTabulate(QuestionSettings pSMSettings, Workbook workBook, Application XlApp, 
            ref double avg, ref double meadian, ref double deviation, ref double mode, 
            ref double meanplusdev, ref double min, ref double max)
        {
            this.xlApp = XlApp;
           
            double? emptyDouble = null;
            avg = Convert.ToDouble(emptyDouble);
            meadian = Convert.ToDouble(emptyDouble);
            deviation = Convert.ToDouble(emptyDouble);
            mode = Convert.ToDouble(emptyDouble);
            meanplusdev = Convert.ToDouble(emptyDouble);
            min = Convert.ToDouble(emptyDouble);
            max = Convert.ToDouble(emptyDouble);
               
            Definiotion.VariableDictionary = DictionaryUtil.PopulateQSDictionary(workBook);
            Questions questions = DictUpdate.GetQuestions(workBook);
            string tableName = "answers";
            if (DBHelper.checkAfterProcess(workBook))
            {
                tableName = "data_after_process";
            }
            if (pSMSettings.QuestionFlag == "An")
            {
                tableName = "multivariate";
            }
            List<Data> DataList = null;
            double?[][] weightArray = null;
            if ((pSMSettings.AnswerType) == "SA")
            {
                getWeightArray(pSMSettings, ref weightArray);
            }
            bool res = ReadData(pSMSettings, questions, workBook, tableName, ref DataList);
            if (!res)
            {
                return false;
            }
           
            bool[] filterringFlag = null;
            filterringFlag = new bool[DataList.Count];
            filterringFlag = SetFilterflag(filterringFlag, true);
            filterringFlag = GetFilterForInvalidUnknow(dt, filterringFlag, pSMSettings);
            
            int count = filterringFlag.Count((y => y == true));
            removeInValidRange(ref filterringFlag, DataList, weightArray);
          
            extractData(filterringFlag, weightArray, pSMSettings, DataList, out double[] highDataArr);
            

            double median1 = 0;
            double mode1 = 0;

            if (highDataArr.Length > 0)
            {
                double count1 = Statastic.Count(highDataArr);
                Statastic.Medain(highDataArr, xlApp, ref median1, ref mode1);
                avg = Statastic.Average(highDataArr);


                min = Statastic.Min(highDataArr);

                max = Statastic.Max(highDataArr);
                meadian = median1;
                mode = mode1;
                double? stdDev = Statastic.StdDev(highDataArr, xlApp);
                if (!double.IsNaN((double)stdDev))
                {
                    deviation = workBook.Application.Evaluate("=ROUND(" + stdDev + ",1)");
                }
                else
                {
                    deviation = 0;
                }
                avg = workBook.Application.Evaluate("=ROUND(" + avg + ",1)");
                double meanplus = (2 * deviation) + avg;
                meanplusdev = (double)Math.Truncate(meanplus);
            }
            return true;
        }

        public void MOdeMean(double[] dataArr, ref double mode, ref double median)
        {
            median = 0.0;

            if (dataArr.Count() < 65000)
            {

                try
                {
                    mode = dataArr.GroupBy(x => x)
 .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
 .Select(x => x.Key)
 .FirstOrDefault();
                }
                catch (Exception)
                {
                    mode = double.NaN;
                }
            }

            double[] array = dataArr.OrderByDescending(c => c).ToArray();
            int arrLngth = array.Length;
            if (arrLngth % 2 == 0)
            {
                median = (array[arrLngth / 2] + array[arrLngth / 2 - 1]) / 2;
            }
            else
            {
                median = array[(arrLngth - 1) / 2];
            }
            if (mode == 0)
            {
                int NCounter = 1;
                int MaxCnt = 1;
                mode = array[0];
                bool invalid = true;

                for (int i = 1; i < arrLngth; i++)
                {
                    if (array[i] == array[i - 1])
                    {
                        NCounter += 1;
                        invalid = false;
                    }
                    else
                    {
                        if (NCounter > MaxCnt)
                        {
                            MaxCnt = NCounter;
                            mode = array[i - 1];
                        }
                        NCounter = 1;
                    }
                }
                if (NCounter > MaxCnt)
                {
                    mode = array[arrLngth - 1];
                }

                if (invalid)
                    mode = double.NaN;
            }

        }
        private void extractData(bool[] filterringFlag, double?[][] weightArray, QuestionSettings pSMSettings, List<Data> highDataList, out double[] highDataArr)
        {
            int count = filterringFlag.Count((y => y == true));
            highDataArr = new double[count];

            int k = 0;

            if ((pSMSettings.AnswerType) == "SA")
            {
                for (int i = 0; i < filterringFlag.Length; i++)
                {
                    if (filterringFlag[i] == true)
                    {
                        highDataArr[k] = Convert.ToDouble(weightArray[0][(highDataList[i] as SAData).Value - 1]);

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

                        k++;
                    }
                }
            }
        }
        public bool[] SetFilterflag(bool[] filterflag, bool value)
        {
            for (int i = 0; i < filterflag.Length; i++)
            {
                filterflag[i] = value;
            }
            return filterflag;
        }
        public bool[] GetFilterForInvalidUnknow(System.Data.DataTable dttable, bool[] filterflag, QuestionSettings psmSettings)
        {

            for (int i = 0; i < dttable.Rows.Count; i++)
            {
                if (filterflag[i] == true)
                {
                    for (int j = 0; j < dttable.Columns.Count; j++)
                    {
                        if (Convert.ToString(dttable.Rows[i][j]).Equals(string.Empty) || Convert.ToString(dttable.Rows[i][j]).Equals("*") || Convert.ToString(dttable.Rows[i][j]).Equals("**") || Convert.ToString(dttable.Rows[i][j]).Equals("DK"))
                        {
                            filterflag[i] = false;
                            break;
                        }
                    }
                }
            }

            return filterflag;
        }
        System.Data.DataTable dt = new System.Data.DataTable();
        private bool ReadData(QuestionSettings qstnDetHigh, Questions questions, Workbook workBook, string tableName, ref List<Data> highDataList)
        {
            QCWebException ex;
            bool isMv = false;
            string tableNameAnswer = tableName;
            using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
            {
                con.Open();
                try
                {

                    if (!checkQuestion(qstnDetHigh, workBook))
                    {

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
                    dt = dataTble;
                    if (ex != null)
                    {

                        return false;
                    }
                    if (ex != null)
                    {

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

        private void removeUnknownandInvalid(Workbook workBook, QuestionSettings pSMSettings, Questions questions, String tableName, ref bool[] filterringFlag)
        {
            List<FilterSettingsCr> FiltersDkAndStar = new List<FilterSettingsCr>();

            FilterSettingsCr fs11 = new FilterSettingsCr();
            fs11.variable = pSMSettings.Variable;
            fs11.operatorType = "<>";
            fs11.values = "*";
            fs11.conditionType = AND;
            FiltersDkAndStar.Add(fs11);

            FilterSettingsCr fs12 = new FilterSettingsCr();
            fs12.variable = pSMSettings.Variable;
            fs12.operatorType = "<>";
            fs12.values = "DK";
            fs12.conditionType = AND;
            FiltersDkAndStar.Add(fs12);
            string filterExp = CriteriaDescProvider.CreateCriteriaDescriptions(FiltersDkAndStar, questions);
            filterringFlag = new Criteria(filterExp, "", questions).Filtering(DBHelper.GetConnectionString(workBook), tableName: tableName);
        }


        private void getWeightArray(QuestionSettings qstnDetHigh, ref double?[][] weightArray)
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
                    else { weightDouble[i] = null; }
                }
                weightArray[0] = weightDouble;
            }
        }
        private void removeInValidRange(ref bool[] filterringFlag, List<Data> highDataList, double?[][] weightArray)
        {
            for (int i = 0; i < filterringFlag.Length; i++)
            {
                if (filterringFlag[i] == false) continue;
                double highValue = 0.0;

                if (highDataList[i].GetType() == typeof(SAData))
                {
                    if (weightArray[0][(highDataList[i] as SAData).Value - 1] == null) { filterringFlag[i] = false; continue; }
                    highValue = Convert.ToDouble(weightArray[0][(highDataList[i] as SAData).Value - 1]);

                }
                else if (highDataList[i].GetType() == typeof(NData))
                {
                    highValue = (highDataList[i] as NData).Value;

                }



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
    }
}
