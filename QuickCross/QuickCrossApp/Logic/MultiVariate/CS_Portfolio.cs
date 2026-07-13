using ExcelAddIn.Common;
using log4net;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.DataProcess;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using Qc4Launcher.DB;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Qc4Launcher.Forms.Multivariate_Analysis.CollectionClass;
using Constants = Qc4Launcher.Util.Constants;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelUtil = Qc4Launcher.Util.ExcelUtil;
using DBHelperCommon = QC4Common.DB.DBHelper;

namespace Qc4Launcher.Logic.MultiVariate
{
    class CS_Portfolio
    {
        private const string CSPortfolio_Template = "CSPortfolio_Template.xltx";// "CSPortfolio_Template.xls";
        private const string CSPortfolio_Template_JaJp = "CSPortfolio_Template_JaJp.xltx";// "CSPortfolio_Template_JaJp.xls";
        private const string CSPortfolio_GraphSheet = "Sheet1";// "CSƒ|[ƒgƒtƒHƒŠƒI";//CSポートフォリオ
        private const string CSPortfolio_DataSheet = "Sheet2";// "Data";

        private const long GraphRow_DataStart = 11;
        private const long DataRow_DataStart = 3;
        private const long DataCol_HeadStart = 3;
        private const long DataCol_DataStart = 5;
        private const long MaxOutputCategoryCount = 200;
        private const string strTopBoxHeader = "iTOP";
        private const string strTopBoxFooter = "BOXj";
        private const string strMax = "Å‘å";
        private const string strMin = "Å¬";
        private const string strAverage = "•½‹Ï";
        private const string strSynthesis = "‘‡";
        private const string strSatisfaction = "";//"–ž‘«“x"
        private const string strCorrelation = "";// "‘ŠŠÖŒW”"
        private const string strVerticalAxis = "(cŽ²)";
        private const string strHorizonAxis = "(‰¡Ž²)";
        private const string strNextTable = @"(Œã•\‚Ö)";
        private const string strContinueTable = @"(‘O•\‚©‚ç)";
        private const string strSynEstimate = "‘‡•]‰¿€–Ú";
        private const string strIndEstimate = "ŒÂ•Ê•]‰¿€–Ú";
        private const string strOutputCell_AllCount = "J2";
        private const string strOutputCell_SynthesisCs = "N7";
        private const string strOutputCell_Condition = "E:E";


        string strItemPrefix = "[";
        public string strItemSuffix = "]";
        string strEdit01 = LocalResource.PSM_FILTER_VAL;//"...value of...";
        string strEdit_LG = LocalResource.PSM_FILTER_LG;//"  excluding";
        string strEdit_E = "";
        string strEdit_LE = LocalResource.PSM_FILTER_LE;//" or under";
        string strEdit_GE = LocalResource.PSM_FILTER_GE;//" or over";
        string strEdit_L = LocalResource.PSM_FILTER_L;//" under";
        string strEdit_G = LocalResource.PSM_FILTER_G;//" over";
        string strEdit_And = LocalResource.PSM_FILTER_AND;//"  AND";
        string strEdit_Or = LocalResource.PSM_FILTER_OR;//"  OR";
        string strNoAnswer = LocalResource.PSM_FILTER_NO_ANSWER;//"'No Answer'";
        string strNotApply = LocalResource.PSM_FILTER_NOT_APPLY;//"'Excluded'";

        public static string u_DK = "DK";
        public static string u_Asterisk = "*";


        public static string Sign_LG = "<>";
        public static string Sign_E = "=";
        public static string Sign_LE = "<=";
        public static string Sign_GE = ">=";
        public static string Sign_G = ">";
        public static string Sign_L = "<";
        public static string Sign_EX = "!";

        public static string D_AND = "AND";
        public static string D_OR = "OR";

        public const int CSPRowStart = 29;
        public const int CSPRowEnd = 34;
        public const int MAX_MULTIVARIAT_COLUMN = 1040;
        public const int CSPColumnStart = 2;

        public const int OnOffColumn = 2;

        public const int CriteriaVariableColumn = 3;
        public const int CriteriaOperatorColumn = 4;
        public const int CriteriavalueColumn = 5;

        public const int MultiVariateInstructionColumn = 6;

        public const int CSP_CSP_CriteriaFlag = 7;

       

        public const int CSP_OverallEvaluationVariable = 8;
        public const int CSP_OverallEvaluationChoices = 9;
        public const int CSP_IndividualEvaluationChoices = 10;
        public const int CSP_IndividualEvaluationVariableStart = 11;

        public string CriteriaQuerystring = string.Empty;
        string OverallEvaluationCriteriaQuerystring = string.Empty;
        string FilterSettingsQuerystring = string.Empty;
        string RemoveUnknownandInvalidQuerystring = string.Empty;
        CS_Portfolio_Settings csps = new CS_Portfolio_Settings();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string templateDirectoryPath;
        public Application xlApp;
        public Workbooks wbs;
        Workbook baseBook = null;
        Worksheet dataSheet = null;
        Worksheet grpahSheet = null;

        private const long IndividualevaluationvariableheadingRow = 2;
        private const long IndividualevaluationvariableheadingColumn = 3;
        private const int IndividualevaluationvariablevalueRow = 3;


        private const int IndividualSatisfactionCorrelationRow = 11;
        private const long IndividualSatisfactionCorrelationColumn = 14;
        private const long Graph_IndividualSatisfactionCorrelationColumn = 16;
        private const long Graph_IndividualSatisfactionCorrelationPercent = 15;
        private const long Graph_IndividualSatisfactionCorrelationQuestion = 14;
        private const string Columnname = "N";
        int Row_Min = 0;
        int Row_Max = 0;
        int Row_Average = 0;
        double mean = 0;
        double min = 0;
        double max = 0;
        double sum = 0;
        public string EditString = string.Empty;
        string CriteriaFlagValue = string.Empty;
        public delegate void OnWorkerMethodCompleteDelegate(double value, string status, bool isForceStop = false, bool retainThread = false, bool close = false, bool disableCancel = false);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        public void updateProgress(double currentProgress, string v, bool close = false)
        {
            OnWorkerComplete(currentProgress, v, close: close);
        }
        public bool ExecutePortfolio(Workbook workBook, out string errmsg)
        {
            errmsg = "";
            bool retval = false;
            bool IsCriteria = false;
            ExcelOperate excelOperate = null;
            double currentProgress = 1;
            Application xlApp = null;
            try
            {
                _log.Info("CSP analysis started");
                OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
                Worksheet sht = ExcelUtil.GetWorkSheetBySheetName(workBook, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.MultiVariate);
                Excel.Range CSPStart = sht.Cells[CSPRowStart, CSPColumnStart];
                Excel.Range CSPlast = sht.Cells[34, 235];
                Excel.Range rar = sht.get_Range(CSPStart, CSPlast);
               
                object[,] values = (object[,])rar.Value2;

                int validresponses = 0;
                int verticalvalidresponses = 0;
                bool ismultivariatetableoverall = false;
                bool ismultivariatetableindividual = false;

                Questions questions = DictUpdate.GetQuestions(workBook);
                List<string> Variables = new List<string>();
                List<string> SelectedChoices = new List<string>();
                List<string> Variableids = new List<string>();
                System.Data.DataTable dataTble = new System.Data.DataTable();
                System.Data.DataTable overallevaluationdataTble = new System.Data.DataTable();
                string varibleid = string.Empty;
                string overallevaluationvariableid = string.Empty;
                string overallevaluationvariablename = string.Empty;
                // TODO table name can be multivaraite 
                string tableName = "answers";
                string tableNameMV = "multivariate";
                string overalltableNameMV = "multivariate";
                string individualtableNameMV = "multivariate";
                if (DBHelper.checkAfterProcess(workBook))
                {
                    tableName = "data_after_process";
                }
                
               
                currentProgress = 10;
                OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);
                for (int i = 1; i <= values.GetLength(0); i++)
                {
                    string CriteriaVariableText = Convert.ToString(values[i, (CriteriaVariableColumn - 1)]);
                    string criteriaOperatorText = Convert.ToString(values[i, (CriteriaOperatorColumn - 1)]);
                    string criteriaValueText = Convert.ToString(values[i, (CriteriavalueColumn - 1)]);
                    if (Convert.ToString(values[i, (MultiVariateInstructionColumn - 1)]).Equals(QC4Common.Common.Constants.DP.InstructionAND) ||
                    Convert.ToString(values[i, (MultiVariateInstructionColumn - 1)]).Equals(QC4Common.Common.Constants.DP.InstructionOR))
                    {
                        IsCriteria = true;
                        if (!string.IsNullOrEmpty(CriteriaVariableText))
                        {
                            FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, i, (MultiVariateInstructionColumn - 1), false, false, false, Convert.ToString(values[i, (MultiVariateInstructionColumn - 1)]), sht);
                            //this.Edit_SiborikomiAll(CriteriaVariableText, criteriaOperatorText, criteriaValueText, Convert.ToString(values[i, (MultiVariateInstructionColumn - 1)]), questions);
                            this.EditString = Macromill.QCWeb.Logic.TabulationEx.Criteria.CriteriaDescProvider.Edit_SiborikomiAll_General(CriteriaVariableText, criteriaOperatorText, criteriaValueText, Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]), questions, this.EditString);
                        }
                    }
                    else if (Convert.ToString(values[i, (MultiVariateInstructionColumn - 1)]).Equals(ProcessingMethod.CSPORTFOLIO_ANALYSIS))
                    {
                        try { CriteriaFlagValue = Convert.ToString(values[i, (CSP_CSP_CriteriaFlag - 1)]); }
                        catch { }

                        Variables.Add(Convert.ToString(values[i, (CSP_OverallEvaluationVariable - 1)]));
                        
                        int startindex = CSP_IndividualEvaluationVariableStart - 1;
                        int limit = startindex + 100;
                        for (int j = startindex; j < limit; j++)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(values[i, j])))
                            {
                                break;
                            }
                            Variables.Add(Convert.ToString(values[i, j]));
                        }

                        SelectedChoices.Add(Convert.ToString(values[i, (CSP_OverallEvaluationChoices - 1)]));
                        SelectedChoices.Add(Convert.ToString(values[i, (CSP_IndividualEvaluationChoices - 1)]));



                        if (!string.IsNullOrEmpty(CriteriaVariableText) && CriteriaFlagValue.Equals("1"))
                        {
                            IsCriteria = true;
                            FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, CSPRowStart + i - 1, (CriteriaVariableColumn), false, false, false, Convert.ToString(values[i, (MultiVariateInstructionColumn - 1)]), sht);
                            //this.Edit_SiborikomiAll(CriteriaVariableText, criteriaOperatorText, criteriaValueText, Convert.ToString(values[i, (MultiVariateInstructionColumn - 1)]), questions);
                            this.EditString = Macromill.QCWeb.Logic.TabulationEx.Criteria.CriteriaDescProvider.Edit_SiborikomiAll_General(CriteriaVariableText, criteriaOperatorText, criteriaValueText, Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]), questions, this.EditString);
                        }


                        FilterSettingsQuerystring = CriteriaQuerystring;
                        CriteriaQuerystring = string.Empty;
                        bool overall = true;
                        foreach (string varnam in Variables)
                        {

                            FetchCriteria(varnam, "!=", "*", i, (MultiVariateInstructionColumn - 1), false, false, false, QC4Common.Common.Constants.DP.InstructionAND, sht);
                            FetchCriteria(varnam, "!=", "DK", i, (MultiVariateInstructionColumn - 1), false, false, false, QC4Common.Common.Constants.DP.InstructionAND, sht);
                            varibleid += string.IsNullOrEmpty(varibleid) ? ("q_" + (Definiotion.VariableDictionary[varnam].ItemId).ToString()) : (",q_" + (Definiotion.VariableDictionary[varnam].ItemId).ToString());
                            if (overall)
                            {
                                OverallEvaluationCriteriaQuerystring = CriteriaQuerystring;
                                CriteriaQuerystring = string.Empty;
                                overallevaluationvariableid = varibleid;
                                overallevaluationvariablename = varnam;
                                varibleid = string.Empty;
                                overall = false;
                                Variableids.Clear();
                            }
                            Variableids.Add(("q_" + (Definiotion.VariableDictionary[varnam].ItemId).ToString()));
                        }
                        if (!string.IsNullOrEmpty(CriteriaQuerystring))
                            CriteriaQuerystring = CriteriaQuerystring.Remove(CriteriaQuerystring.Length - 3);


                        string delim = string.Empty;
                        int opencount = CriteriaQuerystring.Count(f => f == '(');
                        int closecount = CriteriaQuerystring.Count(f => f == ')');
                        int diff = opencount - closecount;
                        if (diff != 0) delim = ")";
                        CriteriaQuerystring = (CriteriaQuerystring + delim);
                        RemoveUnknownandInvalidQuerystring = CriteriaQuerystring;

                        if (!string.IsNullOrEmpty(OverallEvaluationCriteriaQuerystring))
                            OverallEvaluationCriteriaQuerystring = OverallEvaluationCriteriaQuerystring.Remove(OverallEvaluationCriteriaQuerystring.Length - 3);


                        delim = string.Empty;
                        opencount = OverallEvaluationCriteriaQuerystring.Count(f => f == '(');
                        closecount = OverallEvaluationCriteriaQuerystring.Count(f => f == ')');
                        diff = opencount - closecount;
                        if (diff != 0) delim = ")";
                        OverallEvaluationCriteriaQuerystring = (OverallEvaluationCriteriaQuerystring + delim);

                        break;
                    }
                }
              
                currentProgress = 15;
                OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                {
                    try
                    {
                        bool isMv = false;
                        string overalltableNameMv = string.Empty;
                        string individualtableNameMv = string.Empty;
                        con.Open();
                        dataTble = new System.Data.DataTable();
                        overallevaluationdataTble = new System.Data.DataTable();

                      

                        if (Definiotion.VariableDictionary[overallevaluationvariablename].QuestionFlag == Util.Constants.Variable_Type_An)
                        {
                            ismultivariatetableoverall = true;
                        }

                        foreach (string varnm in Variables)
                        {
                            if (Definiotion.VariableDictionary[varnm].QuestionFlag == Util.Constants.Variable_Type_An)
                            {
                                ismultivariatetableindividual = true;
                                break;
                            }
                           
                        }
                        if (!ismultivariatetableoverall)
                        {
                            overallevaluationdataTble = DBHelper.GetDataTable("Select " + overallevaluationvariableid + " from " + tableName + " order by sort_no ", con);//TODO join answers or dataafterprocess  
                        }
                        else
                        {
                            overallevaluationdataTble = DBHelper.GetDataTable("Select  " + overallevaluationvariableid + " from " + tableName + " a join " + tableNameMV + " m where a.sample_id=m.sample_id order by a.sort_no ", con);
                        }
                        //
                        if (!ismultivariatetableindividual)
                        {
                            dataTble = DBHelper.GetDataTable("Select " + varibleid + " from " + tableName + " order by sort_no ", con);//TODO  join answers or dataafterprocess 
                        }
                        else
                        {
                            dataTble = DBHelper.GetDataTable("Select  " + varibleid + " from " + tableName + " a join " + tableNameMV + " m where a.sample_id=m.sample_id order by a.sort_no ", con);
                        }
                        // Util.DataTableHelper.ListToDataTable();
                    }
                    catch (Exception ex)
                    {
                        errmsg = LocalResource.ERRMSG_MULTIVARIATE_FAILED_TO_GET_DATA;
                        return false;
                        //_log.Warn(ex.Message);
                    }
                }

                try
                {
                    validresponses = overallevaluationdataTble.Rows.Count;
                    verticalvalidresponses = dataTble.Rows.Count;
                }
                catch
                {
                    errmsg = LocalResource.CSP_ERRMSG_CANNOT_ANALYSE_VALID_CASES_IS_ZERO;
                    return false;
                }
                // _log.Info("CSP criteria filter setting");
                bool[] overallfilterringFlg = new bool[validresponses];
                bool[] filterringFlg = new bool[verticalvalidresponses];//add total count  
                overallfilterringFlg = new Criteria(OverallEvaluationCriteriaQuerystring, "", questions).Filtering(DBHelper.GetConnectionString(workBook), tableName: tableName);//, dt: dataTble
                //_log.Info("CSP criteria filter 1 compltd");
                if (!string.IsNullOrEmpty(FilterSettingsQuerystring) && CriteriaFlagValue.Equals("1"))
                {
                    new Criteria(FilterSettingsQuerystring, "", questions, overallfilterringFlg != null ? Operator.opAnd : Operator.opOr).Filtering(ref overallfilterringFlg, DBHelper.GetConnectionString(workBook), tableName);
                }
                _log.Info("CSP criteria filter 2 strt ");
                MultivariateTable mt = new MultivariateTable();
                filterringFlg = mt.CopyOrgFilterflag(overallfilterringFlg, filterringFlg);// mt.SetFilterflag(filterringFlg, true);// mt.CopyOrgFilterflag(overallfilterringFlg, filterringFlg);
                                                                                          // filterringFlg = mt.GetFilterForInvalidUnknow(dataTble, filterringFlg);
                                                                                          //  filterringFlg = new Criteria(RemoveUnknownandInvalidQuerystring, "", questions).Filtering(DBHelper.GetConnectionString(workBook), tableName: tableName, dt: dataTble);//, dt: dataTble
                if (!string.IsNullOrEmpty(FilterSettingsQuerystring) && CriteriaFlagValue.Equals("1"))
                {
                    new Criteria(FilterSettingsQuerystring, "", questions, filterringFlg != null ? Operator.opAnd : Operator.opOr).Filtering(ref filterringFlg, DBHelper.GetConnectionString(workBook), tableName);//-filtermay not hav  variables in table
                }
                _log.Info("CSP criteria filter 2 compltd");
                //_log.Info("CSP valid response");
                currentProgress = 20;
                OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);
                validresponses = overallfilterringFlg.Where(c => c).Count();
                if (validresponses == 1)//#198084,https://app.gluemodel.com/#/project/task/4295063624
                {
                    errmsg = LocalResource.CSP_ERRMSG_1_VALIDCASE_FAILED_TO_CALCULATE_CORREL_COEFF;
                    return false;
                }
                if (validresponses < 2)
                {
                    errmsg = LocalResource.CSP_ERRMSG_CANNOT_ANALYSE_VALID_CASES_IS_ZERO;
                    return false;
                }
                verticalvalidresponses = filterringFlg.Where(c => c).Count();
                if (verticalvalidresponses < 2)
                {
                    errmsg = LocalResource.CSP_ERRMSG_FAILED_TO_CALCULATE_CORREL_COEFF;
                    return false;
                }
                // Util.MessageDialog.ErrorOk(rowcount.ToString());
                // _log.Info("CSP analysis Checksame choices");
                currentProgress = 30;
                OnWorkerComplete(currentProgress, LocalResource.CSP_PB_CHECKIN_SAME_CHOICES);//CSP_PB_CHECKIN_SAME_CHOICES
                if (CheckSameChoice(overallevaluationdataTble, overallfilterringFlg))
                {
                    errmsg = LocalResource.CSP_ERRMSG_FAILED_TO_CALCULATE_CORREL_COEFF;
                    return false;//same choices in variable return error msg also
                }
                if (CheckSameChoiceIndividual(dataTble, filterringFlg))//https://redmine.macromill.com/issues/207118#note-4
                {
                    errmsg = LocalResource.CSP_ERRMSG_CANNOT_ANALYSE_VALID_CASES_IS_ZERO;
                    return false;//same choices in variable return error msg also
                }
                //_log.Info("CSP chioce count sum array");
                string variablename = Variables[0];
                int selectedvariablecount = Variables.Count;
                double[,] ChoiceCountArray = new double[Definiotion.VariableDictionary[variablename].Choices.Count, selectedvariablecount + 1];
                double[,] ChoicePercentageArray = new double[Definiotion.VariableDictionary[variablename].Choices.Count, selectedvariablecount + 1];
                double[,] SumArrayOverall = new double[dataTble.Columns.Count, 2];
                double[,] SumArray = new double[selectedvariablecount, 2];
               
                // _log.Info("CSP analysis correl/Percentage cal");
                currentProgress = 40;
                OnWorkerComplete(currentProgress, LocalResource.CSP_PB_CALCULATING_CORREL_PER);
                if (OverallCorrelationChoicesAndPercentage(overallevaluationdataTble, overallfilterringFlg, filterringFlg, dataTble, Definiotion.VariableDictionary[variablename].Choices.Count, selectedvariablecount, validresponses, ref ChoiceCountArray, ref ChoicePercentageArray, ref SumArrayOverall))
                {
                    //exception retrn err
                    errmsg = LocalResource.CSP_ERRMSG_CANNOT_ANALYSE_VALID_CASES_IS_ZERO;
                    return false;
                }
                if (CorrelationChoicesAndPercentage(dataTble, filterringFlg, overallfilterringFlg, Definiotion.VariableDictionary[variablename].Choices.Count, selectedvariablecount, validresponses, ref ChoiceCountArray, ref ChoicePercentageArray, ref SumArray))
                {
                    //exception retrn err
                    errmsg = LocalResource.CSP_ERRMSG_CANNOT_ANALYSE_VALID_CASES_IS_ZERO;
                    return false;
                }

                // _log.Info("CSP sum array compltd");

                string[] Overallselchoices = (SelectedChoices[0]).Split(',');
                string[] Individualselchoices = (SelectedChoices[1]).Split(',');
                List<int> Overallselectedvalues = new List<int>();
                List<int> Individualselectedvalues = new List<int>();
                foreach (string val in Overallselchoices)
                {
                    if (!string.IsNullOrEmpty(val))
                    {
                        Overallselectedvalues.Add(int.Parse(val));
                    }
                }

                foreach (string val in Individualselchoices)
                {
                    if (!string.IsNullOrEmpty(val))
                    {
                        Individualselectedvalues.Add(int.Parse(val));
                    }
                }
                //_log.Info("CSP choice percentage");
                string[,] OverallTotalPercentage = new string[Overallselectedvalues.Count, 3];
                string[,] IndividualTotalPercentage = new string[(Individualselectedvalues.Count), (2 * (selectedvariablecount - 1)) + 1];
                //find toat percentage of seleted choices altogthr
                int pos = 0;
                double percentage = 0;
                double choicetotalcount = 0;
                int overalllastpos = 0;
                int x = 0;
                for (int i = 0; i < ChoicePercentageArray.GetLength(0); i++)
                {
                    if (Overallselectedvalues.Contains(int.Parse(Convert.ToString(ChoicePercentageArray[i, 0]))))
                    {
                        OverallTotalPercentage[x, 0] = (Convert.ToString(ChoicePercentageArray[i, 0] * 1.00));
                        percentage += double.Parse(Convert.ToString(ChoicePercentageArray[i, 1]));
                        choicetotalcount += double.Parse(Convert.ToString(ChoiceCountArray[i, 1]));
                        pos = x;
                        x++;
                    }
                }
                OverallTotalPercentage[pos, 1] = choicetotalcount.ToString();
                OverallTotalPercentage[pos, 2] = (percentage * 1.00).ToString();//# 206987
                overalllastpos = pos;
                percentage = 0;
                choicetotalcount = 0;
                int individuallastpos = 0;
                int row = 0;
                int column = 1;
                for (int i = 2; i < ChoicePercentageArray.GetLength(1); i++)
                {
                    percentage = 0;
                    choicetotalcount = 0;
                    row = 0;
                    for (int j = 0; j < ChoicePercentageArray.GetLength(0); j++)
                    {
                        if (Individualselectedvalues.Contains(int.Parse(Convert.ToString(ChoicePercentageArray[j, 0]))))
                        {
                            IndividualTotalPercentage[row, 0] = (Convert.ToString((ChoicePercentageArray[j, 0] * 1.00).ToString()));//"#.##"
                           
                            {
                                //double percent = string.IsNullOrEmpty(Convert.ToString(IndividualTotalPercentage[i, j])) ? 0 : double.Parse(Convert.ToString(IndividualTotalPercentage[i, j]));
                                percentage += (double.Parse(Convert.ToString(ChoicePercentageArray[j, i])));
                            }
                            // else
                            {

                                // double choicecount = string.IsNullOrEmpty(Convert.ToString(IndividualTotalPercentage[i, j])) ? 0 : double.Parse(Convert.ToString(IndividualTotalPercentage[i, j]));
                                choicetotalcount += (double.Parse(Convert.ToString(ChoiceCountArray[j, i])));
                            }
                            row++;
                        }

                    }
                    row = row - 1;
                    IndividualTotalPercentage[row, column] = choicetotalcount.ToString();//"#.##"
                    column++;
                    IndividualTotalPercentage[row, column] = (percentage * 1.00).ToString();//# 206987      // percentage.ToString();
                    column++;
                    individuallastpos = row;
                }
                // _log.Info("CSP choice percentage compltyd");

                //_log.Info("CSP correl");
                //corelation
                currentProgress = 50;
                OnWorkerComplete(currentProgress, LocalResource.CSP_PB_ANALYSING_RESULT);
                double[] correlresult = new double[selectedvariablecount - 1];
                int result;
                int rowcount = (dataTble.Rows.Count > overallevaluationdataTble.Rows.Count) ? dataTble.Rows.Count : overallevaluationdataTble.Rows.Count;
                for (int i = 0; i < dataTble.Columns.Count; i++)// dataTble.Columns.Count//dataTble, filterringFlg,SumArray
                {
                    double[] ave1 = new double[overallevaluationdataTble.Rows.Count];
                    double[] ave2 = new double[dataTble.Rows.Count];
                    double[] Ave1Ave2 = new double[overallevaluationdataTble.Rows.Count];
                    double[] sqrtave1 = new double[overallevaluationdataTble.Rows.Count];
                    double[] sqrtave2 = new double[dataTble.Rows.Count];
                    double ave1value;
                    double ave2value;
                    double sqrtave1value;
                    double sqrtave2value;
                    double SumofAve1Ave2 = 0;
                    double Sumofsqrtave1 = 0;
                    double Sumofsqrtave2 = 0;
                    CalculateSumCount(overallevaluationdataTble, overallfilterringFlg, filterringFlg, dataTble, i, ref SumArray);
                    for (int j = 0; j < rowcount && (j<= overallfilterringFlg.Length-1) && (j <= filterringFlg.Length - 1); j++)// dataTble.Rows.Count
                    {
                        ave1value = 0.00;
                        ave2value = 0.00;
                        if (overallfilterringFlg[j] == true && filterringFlg[j] == true && (int.TryParse(Convert.ToString(dataTble.Rows[j][i]), out result)))
                        {
                            ave1value = Convert.ToDouble(Convert.ToString(overallevaluationdataTble.Rows[j][0])) - (SumArray[0, 0] / SumArray[0, 1]);//ave1[j] =Convert.ToDouble(Convert.ToString( dataTble.Rows[j][0])) - (SumArray[0]/ validresponses);
                            double dval = 0.00;
                            if (!double.TryParse(Convert.ToString(dataTble.Rows[j][i]), out dval))
                            {
                                ave1value = 0.00;
                            }
                            //}
                            //if (filterringFlg[j] == true)
                            //{
                            ave2value = Convert.ToDouble(Convert.ToString(dataTble.Rows[j][i])) - (SumArray[i + 1, 0] / SumArray[i + 1, 1]);

                            SumofAve1Ave2 += ave1value * ave2value;//Ave1Ave2[j] = ave1value * ave2value;
                            sqrtave1value = (ave1value * ave1value);
                            sqrtave2value = (ave2value * ave2value);
                            Sumofsqrtave1 += sqrtave1value;
                            Sumofsqrtave2 += sqrtave2value;
                        }

                    }
                    if (Sumofsqrtave1 == 0 || Sumofsqrtave2 == 0)
                    {
                        errmsg = LocalResource.CSP_ERRMSG_FAILED_TO_CALCULATE_CORREL_COEFF;
                        return false;
                    }
                    correlresult[i] = SumofAve1Ave2 / (Math.Sqrt(Sumofsqrtave1) * Math.Sqrt(Sumofsqrtave2));

                }

                //_log.Info("CSP correl ok");
                int times = 0;
                mean = correlresult[0];
                min = correlresult[0];
                max = correlresult[0];
                for (int i = 0; i < correlresult.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(correlresult[i])))
                    {
                        if (min > correlresult[i])
                        {
                            min = correlresult[i];
                        }
                        if (max < correlresult[i])
                        {
                            max = correlresult[i];
                        }
                        sum += correlresult[i];
                        times++;
                    }
                }
                mean = sum / times;

                //Graph generation
                excelOperate = new ExcelOperate();
                xlApp = excelOperate.Excel;
                xlApp.Visible = false;
                xlApp.ScreenUpdating = false;



                xlApp.EnableEvents = false;
                //xlApp.DisplayStatusBar = false;
                xlApp.PrintCommunication = false;
                xlApp.DisplayAlerts = false;
                currentProgress = 60;
                OnWorkerComplete(currentProgress, LocalResource.CSP_PB_GENERATING_OUTPUT);
                // _log.Info("CSP generate out put");
                this.generateOutPut(System.AppContext.BaseDirectory, xlApp, validresponses, correlresult, OverallTotalPercentage, mean, min, max, overalllastpos, IndividualTotalPercentage, individuallastpos, Variables, ChoicePercentageArray);
                // _log.Info("CSP analysis Outputing graph values");
                currentProgress = 70;
                OnWorkerComplete(currentProgress, LocalResource.CSP_PB_CREATING_GRAPH);
                Output_GraphSheet(currentProgress);
                //_log.Info("CSP analysis finished");
                xlApp.EnableEvents = true;
                xlApp.PrintCommunication = true;
                xlApp.WindowState = XlWindowState.xlMaximized;
                xlApp.ScreenUpdating = true;
                xlApp.DisplayAlerts = true;
                this.OnWorkerComplete(100, LocalResource.PSM_PB_RESLT_OUT, retainThread: true);
                xlApp.Visible = true;
                try
                {
                    SetForegroundWindow((IntPtr)xlApp.Hwnd);
                }
                catch { }

                retval = true;
            }
            catch (Exception ex)
            { errmsg = LocalResource.FAILED_TO_GENE_EXCEL; _log.Error(ex.Message); retval = false; }
            finally
            {
                if (grpahSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref grpahSheet);
                }
                if (dataSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref dataSheet);
                }   
                if (baseBook != null)
                {
                    COMWholeOperate.releaseComObject(ref baseBook);
                }
                if (wbs != null)
                {
                    COMWholeOperate.releaseComObject(ref wbs);
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
                _log.Info("CSP analysis completed");
            }

            return retval;
        }


        public void FetchCriteria(string CriteriaVariableText, string criteriaOperatorText, string criteriaValueText,
         int row, int column, bool isnotcriteriavalue, bool isivcriteria, bool isnacriteria, string instruction, Excel.Worksheet worksheet)
        {
            string delim = string.Empty;
            int opencount = 0;
            int closecount = 0;
            int diff = 0;

            string cellitemid = (Definiotion.VariableDictionary[CriteriaVariableText].ItemId).ToString();// (Definitions.VariableDictionary[CriteriaVariableText].ItemId).ToString();
            string cellopertor = criteriaOperatorText;
            string cellvalue = criteriaValueText;

            if (Definiotion.VariableDictionary[CriteriaVariableText].AnswerType != Constants.AnswerType.FA)
            {
                cellvalue = CS_Portfolio_Settings.MinMaxAppendWithMinus(cellvalue, row, column, worksheet);
            }
            else if (Definiotion.VariableDictionary[CriteriaVariableText].AnswerType == Constants.AnswerType.FA)
            {
                cellvalue = Regex.Escape(cellvalue);
            }
            //Redmine id: 176455
            try
            {
                cellvalue = cellvalue.Replace("(", "");
                cellvalue = cellvalue.Replace(")", "");
            }
            catch { }
            // GetCommaSeperated(cellvalue, CriteriaVariableText)//Redmine id: 170984
            if (cellvalue.StartsWith("!") || cellvalue.StartsWith("<>"))//Redmine id: 170984//if not check * Dk is there if not ,add to cell value
            {
                isnotcriteriavalue = true;
                if (!cellvalue.Contains("*"))
                {
                    isivcriteria = true;
                }
                if (!cellvalue.Contains("DK"))
                {
                    isnacriteria = true;
                }
            }
            if (Definiotion.VariableDictionary[CriteriaVariableText].AnswerType != Constants.AnswerType.FA && (cellvalue.StartsWith("!") || cellvalue.StartsWith("<>")))//Redmine id: 170984//if not check * Dk is there if not ,add to cell value
            {
                cellopertor = "<>";
                cellvalue = cellvalue.TrimStart('!');
            }
            if (Definiotion.VariableDictionary[CriteriaVariableText].AnswerType != Constants.AnswerType.N && Definiotion.VariableDictionary[CriteriaVariableText].AnswerType != Constants.AnswerType.FA)//IL_JP_MAM_007:4295056906
            {
                cellvalue = csps.GetCommaSeperated(cellvalue, CriteriaVariableText);//Redmine id: 170984
            }
            //cellvalue = GetCommaSeperated(cellvalue, CriteriaVariableText);//Redmine id: 170984
            if (isnotcriteriavalue)//Redmine id: 170984//add *,Dk to value 
            {
                if (isnacriteria)
                {
                    // cellvalue += ",DK";
                    isnacriteria = false;
                }
                if (isivcriteria)
                {
                    //cellvalue += ",*";
                    isivcriteria = false;
                }
            }
            if (instruction.Equals(QC4Common.Common.Constants.DP.InstructionAND))
            {
                delim = string.Empty;//for adding ( 
                opencount = CriteriaQuerystring.Count(f => f == '(');
                closecount = CriteriaQuerystring.Count(f => f == ')');
                diff = opencount - closecount;
                if (diff == 0) delim = "(";
                CriteriaQuerystring += (delim + cellitemid + cellopertor + cellvalue + " & ");
            }
            else if (instruction.Equals(QC4Common.Common.Constants.DP.InstructionOR))
            {
                delim = string.Empty;//for adding ( 
                opencount = CriteriaQuerystring.Count(f => f == '(');
                closecount = CriteriaQuerystring.Count(f => f == ')');
                diff = opencount - closecount;
                if (diff > 0) delim = ")";
                CriteriaQuerystring += (cellitemid + cellopertor + cellvalue + delim + " | ");
            }
            else
            {
                delim = string.Empty;//for adding ( 
                opencount = CriteriaQuerystring.Count(f => f == '(');
                closecount = CriteriaQuerystring.Count(f => f == ')');
                diff = opencount - closecount;
                if (diff > 0) delim = ")"; // if (diff != 1) { delim = "))"; } else { delim = ")"; }
                CriteriaQuerystring += (cellitemid + cellopertor + cellvalue + delim);
            }
        }
        private bool CheckSameChoice(System.Data.DataTable dt, bool[] filterringFlg)
        {
            bool[] flagarray = new bool[dt.Columns.Count];
            bool setfirstvalue = false;
            int value = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                //int value = Convert.ToInt32(dt.Rows[0][i]);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (filterringFlg[j] == true)
                    {
                        if (!setfirstvalue)
                        {
                            value = Convert.ToInt32(dt.Rows[j][i]);
                            setfirstvalue = true;
                        }
                        if (value != Convert.ToInt32(dt.Rows[j][i]))
                        {
                            flagarray[i] = true;
                            break;
                        }
                    }
                }
                setfirstvalue = false;
            }
            for (int i = 0; i < flagarray.Length; i++)
            {
                if (flagarray[i] == false)
                {
                    return true;
                }
            }
            return false; ;
        }
        private bool CheckSameChoiceIndividual(System.Data.DataTable dt, bool[] filterringFlg)
        {
            bool[] flagarray = new bool[dt.Columns.Count];
            bool setfirstvalue = false;
            int value = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                //int value = Convert.ToInt32(dt.Rows[0][i]);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (filterringFlg[j] == true)
                    {
                        if (!setfirstvalue)
                        {
                            if (int.TryParse(Convert.ToString(dt.Rows[j][i]), out value))
                            {
                                setfirstvalue = true;
                            }
                        }
                        if (int.TryParse(Convert.ToString(dt.Rows[j][i]), out value))
                        {
                            flagarray[i] = true;
                            break;
                        }
                    }
                }
                setfirstvalue = false;
            }
            for (int i = 0; i < flagarray.Length; i++)
            {
                if (flagarray[i] == false)
                {
                    return true;
                }
            }
            return false; ;
        }
        private bool OverallCorrelationChoicesAndPercentage(System.Data.DataTable dt, bool[] filterringFlg, bool[] individualfilterringFlg, System.Data.DataTable dtindividual, int choicecount, int totalvariablecount, int validresponses, ref double[,] ChoiceCountArray, ref double[,] ChoicePercentageArray, ref double[,] SumArrayOverall)
        {
           
            for (int i = 0; i < choicecount; i++)
            {
                ChoiceCountArray[i, 0] = i + 1;
                ChoicePercentageArray[i, 0] = i + 1;
            }
            int result;
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((filterringFlg[i] == true) && (individualfilterringFlg[i] == true))
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (int.TryParse(Convert.ToString(dt.Rows[i][j]), out result))
                            {
                                ChoiceCountArray[int.Parse(Convert.ToString(dt.Rows[i][j])) - 1, j + 1] += 1;

                            }
                        }
                    }
                    // }
                }
                double totalsum = double.Parse(validresponses.ToString());
                for (int i = 0; i < choicecount; i++)
                {
                    for (int j = 1; j <= totalvariablecount; j++)
                    {
                        ChoicePercentageArray[i, j] = (ChoiceCountArray[i, j] / totalsum) * 100.00;//Ratio = number of responses for each option / number of valid responses																					
                    }

                }
            }
            catch { return true; }

            return false; ;
        }

        private bool CorrelationChoicesAndPercentage(System.Data.DataTable dt, bool[] filterringFlg, bool[] overallfilterringFlg, int choicecount, int totalvariablecount, int validresponses, ref double[,] ChoiceCountArray, ref double[,] ChoicePercentageArray, ref double[,] SumArray)
        {
            bool isna = true;
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((filterringFlg[i] == true) && (overallfilterringFlg[i] == true))
                    {
                        int result = 0;
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][j])) && int.TryParse(Convert.ToString(dt.Rows[i][j]), out result))
                            {
                                ChoiceCountArray[int.Parse(Convert.ToString(dt.Rows[i][j])) - 1, j + 2] += 1;
                                isna = false;
                            }
                        }
                    }
                }
                if (isna)//Redmine id:207118
                {
                    return true;
                }
                double totalsum = double.Parse(validresponses.ToString());
                for (int i = 0; i < choicecount; i++)
                {
                    for (int j = 2; j <= totalvariablecount; j++)
                    {
                        ChoicePercentageArray[i, j] = (ChoiceCountArray[i, j] / totalsum) * 100.00;//Ratio = number of responses for each option / number of valid responses																					
                    }

                }
            }
            catch { return true; }

            return false; ;
        }



        internal void generateOutPut(string templateDirectoryPath, Application xlApp, int validresponses, double[] correlresult, string[,] OverallTotalPercentage, double mean, double min, double max,
            int overalllastpos, string[,] IndividualTotalPercentage, int individuallastpos, List<string> Variables, double[,] ChoicePercentageArray)
        {
            this.templateDirectoryPath = templateDirectoryPath;
            this.xlApp = xlApp;
            string templatename = CSPortfolio_Template;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
            {
                templatename = CSPortfolio_Template_JaJp;
            }

            // xlApp.ScreenUpdating = true;
            //xlApp.Visible = true;
            wbs = xlApp.Workbooks;
            baseBook = wbs.Add(OutputUtil.getTemplatePath(this.templateDirectoryPath, templatename, xlApp.PathSeparator));
            dataSheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, CSPortfolio_DataSheet);
            grpahSheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, CSPortfolio_GraphSheet);
            dataSheet.Cells.EntireColumn.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            grpahSheet.Cells.EntireColumn.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            // _log.Info("CSP analysis datasheet");
            Excel.Range individualevaluationheadingstart = dataSheet.Cells[IndividualevaluationvariableheadingRow, IndividualevaluationvariableheadingColumn];
            Excel.Range individualevaluationheadinglast = dataSheet.Cells[IndividualevaluationvariableheadingRow, IndividualevaluationvariableheadingColumn];
            Excel.Range rar = dataSheet.get_Range(individualevaluationheadingstart, individualevaluationheadinglast);
            rar.Value = LocalResource.CSP_INDIVIDUAL_EVALUATION_VARIABLE;

            string variablename = Variables[0];

            Excel.Range variablequestionstart = dataSheet.Cells[IndividualevaluationvariablevalueRow, IndividualevaluationvariableheadingColumn + 2];
            Excel.Range variablequestionend = dataSheet.Cells[IndividualevaluationvariablevalueRow, IndividualevaluationvariableheadingColumn + 2 + Definiotion.VariableDictionary[variablename].Choices.Count - 1];
            Excel.Range variablequestionrange = dataSheet.get_Range(variablequestionstart, variablequestionend);
            variablequestionrange.NumberFormat = "@";

            variablequestionstart = dataSheet.Cells[IndividualevaluationvariablevalueRow + 1, IndividualevaluationvariableheadingColumn + 1];
            variablequestionend = dataSheet.Cells[IndividualevaluationvariablevalueRow + (Variables.Count - 1), IndividualevaluationvariableheadingColumn + 1];
            variablequestionrange = dataSheet.get_Range(variablequestionstart, variablequestionend);
            variablequestionrange.NumberFormat = "@";

            Excel.Range individualevaluationValuesstart = dataSheet.Cells[IndividualevaluationvariablevalueRow, IndividualevaluationvariableheadingColumn];
            int lastcolum = IndividualevaluationvariablevalueRow + Definiotion.VariableDictionary[variablename].Choices.Count + 3;
            int datasheetlastrow = (IndividualevaluationvariablevalueRow + (Variables.Count - 1));
            Excel.Range individualevaluationvalueslast = dataSheet.Cells[datasheetlastrow, lastcolum];
            Excel.Range individualevaluationvaluerar = dataSheet.get_Range(individualevaluationValuesstart, individualevaluationvalueslast);

            var individualevaluationvalueobj = individualevaluationvaluerar.Value;
            int choiceno = 0;
            for (int i = 3; i < Definiotion.VariableDictionary[Variables[1]].Choices.Count + 3; i++)
            {
                individualevaluationvalueobj[1, i] = Definiotion.VariableDictionary[Variables[1]].Choices[choiceno];
                // overallevaluationvaluesobj[2, i] = ChoicePercentageArray[x, 1].ToString() + "%";
                choiceno++;
            }

            individualevaluationvalueobj[1, lastcolum - IndividualevaluationvariablevalueRow] = LocalResource.CSP_SATISFACTION_VERTICALDIMENSION;
            individualevaluationvalueobj[1, lastcolum - IndividualevaluationvariablevalueRow + 1] = LocalResource.CSP_CORRELATION_COEFFICIENT_HORIZONTAL_DIMENSION;

            int rowno = 1;
            int colpos = 2;
            int individualposition = 2;
            int rowpos = 0;
            for (int row = 1; row <= Variables.Count - 1; row++)
            {
                rowpos = 0;


                for (int column = 1; column <= (lastcolum - IndividualevaluationvariableheadingColumn) + 1; column++)
                {
                    switch (column)
                    {
                        case 1:
                            individualevaluationvalueobj[row + 1, column] = rowno;
                            break;
                        case 2:
                            individualevaluationvalueobj[row + 1, column] = Definiotion.VariableDictionary[Variables[rowno]].Question;
                            break;
                        default:
                            if (column == lastcolum - int.Parse(IndividualevaluationvariableheadingColumn.ToString()))
                            {
                                individualevaluationvalueobj[row + 1, column] = (IndividualTotalPercentage[individuallastpos, individualposition].ToString()) + "%";
                            }
                            else if (column == (lastcolum - int.Parse(IndividualevaluationvariableheadingColumn.ToString())) + 1)
                            {
                                individualevaluationvalueobj[row + 1, column] = (correlresult[row - 1].ToString());//"#.##"
                            }
                            else
                            {
                                individualevaluationvalueobj[row + 1, column] = ChoicePercentageArray[rowpos, colpos].ToString() + "%";//"#.##"
                                rowpos++;
                            }
                            break;
                    }
                }
                rowno++;
                colpos++;
                individualposition = individualposition + 2;
            }


            individualevaluationvaluerar.Value = individualevaluationvalueobj;

            Excel.Range individualevaluationformatstart = dataSheet.Cells[IndividualevaluationvariablevalueRow + 1, lastcolum];
            Excel.Range individualevaluationformatlast = dataSheet.Cells[datasheetlastrow, lastcolum];
            Excel.Range individualevaluationformatrar = dataSheet.get_Range(individualevaluationformatstart, individualevaluationformatlast);
            individualevaluationformatrar.NumberFormat = @"#0.00";// "0.00_";
                                                                  /*cell.Formatting = new XlCellFormatting();
                                                              cell.Formatting.NumberFormat = "# ???/???";*/


            Excel.Range individualevaluationformatindividualstart = dataSheet.Cells[IndividualevaluationvariablevalueRow + 1, IndividualevaluationvariablevalueRow + 2];
            Excel.Range individualevaluationformatindividuallast = dataSheet.Cells[datasheetlastrow, lastcolum - 1];
            Excel.Range individualevaluationformatindividualrar = dataSheet.get_Range(individualevaluationformatindividualstart, individualevaluationformatindividuallast);
            individualevaluationformatindividualrar.NumberFormat = @"0.00%"; //XlCalcMemNumberFormatType.xlNumberFormatTypePercent;// "0.00_";

            int lastslnopos = datasheetlastrow;
           

            individualevaluationvaluerar.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            individualevaluationvaluerar.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            individualevaluationvaluerar.Borders.Color = 10921638;
            individualevaluationvaluerar.Borders.Weight = XlBorderWeight.xlThin;
            individualevaluationvaluerar.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            individualevaluationvaluerar.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
            // individualevaluationvaluerar.BorderAround(XlLineStyle.xlContinuous, Weight: XlBorderWeight.xlThin, Color: 10921638);

            Excel.Range headingborderstart = dataSheet.Cells[IndividualevaluationvariablevalueRow, IndividualevaluationvariableheadingColumn];
            Excel.Range headingborderlast = dataSheet.Cells[IndividualevaluationvariablevalueRow, lastcolum];
            Excel.Range headingborderrar = dataSheet.get_Range(headingborderstart, headingborderlast);

            headingborderrar.Borders.Color = 10921638;
            headingborderrar.Borders.Weight = XlBorderWeight.xlThin;

            Excel.Range alignmentindividualevaluationheadingstart = dataSheet.Cells[IndividualevaluationvariablevalueRow, IndividualevaluationvariableheadingColumn + 2];
            Excel.Range alignmentindividualevaluationheadinglast = dataSheet.Cells[IndividualevaluationvariablevalueRow, lastcolum];
            Excel.Range alignmentindividualrar = dataSheet.get_Range(alignmentindividualevaluationheadingstart, alignmentindividualevaluationheadinglast);
            PRV_WrapRange(alignmentindividualrar);
            //  PRV_AutoFitRange(alignmentindividualrar);
            PRV_HorizontalAlignmentCenterRange(alignmentindividualrar);
            PRV_VerticalAlignmentCenterRange(alignmentindividualrar);


            Excel.Range individualevaluationmergesstart = dataSheet.Cells[IndividualevaluationvariablevalueRow, IndividualevaluationvariableheadingColumn];
            Excel.Range individualevaluationmergesstop = dataSheet.Cells[IndividualevaluationvariablevalueRow, IndividualevaluationvariableheadingColumn + 1];
            Excel.Range individualevaluationmergeheading = dataSheet.get_Range(individualevaluationmergesstart, individualevaluationmergesstop);
            this.PRV_MergeConditionalRange(individualevaluationmergeheading);





            int headingcol = IndividualevaluationvariablevalueRow + Definiotion.VariableDictionary[variablename].Choices.Count + 3;
            int headinglastrow = (IndividualevaluationvariablevalueRow + (Variables.Count - 1));
            Excel.Range individualevaluationheadingcorrelstart = dataSheet.Cells[IndividualevaluationvariableheadingColumn, headingcol];
            Excel.Range individualevaluationheadingcorrellast = dataSheet.Cells[headinglastrow, headingcol];
            Excel.Range individualevaluationheadingcorrelrange = dataSheet.get_Range(individualevaluationheadingcorrelstart, individualevaluationheadingcorrellast);
            individualevaluationheadingcorrelrange.Interior.Color = 15986394;


            Excel.Range individualevaluationheadingsatisfactionstart = dataSheet.Cells[IndividualevaluationvariableheadingColumn, headingcol - 1];
            Excel.Range individualevaluationheadingsatisfactionlast = dataSheet.Cells[headinglastrow, headingcol - 1];
            Excel.Range individualevaluationheadingsatisfactionrange = dataSheet.get_Range(individualevaluationheadingsatisfactionstart, individualevaluationheadingsatisfactionlast);
            individualevaluationheadingsatisfactionrange.Interior.Color = 14610923;


            datasheetlastrow = datasheetlastrow + 2;
            Excel.Range overallevaluationheadingstart = dataSheet.Cells[datasheetlastrow, IndividualevaluationvariableheadingColumn];
            Excel.Range overallevaluationheadinglast = dataSheet.Cells[datasheetlastrow, IndividualevaluationvariableheadingColumn];
            Excel.Range overallevaluationheadingrar = dataSheet.get_Range(overallevaluationheadingstart, overallevaluationheadinglast);
            overallevaluationheadingrar.Value = LocalResource.CSP_OVERALL_EVALUATION_VARIABLE;

            datasheetlastrow = datasheetlastrow + 1;


            variablequestionstart = dataSheet.Cells[datasheetlastrow, IndividualevaluationvariableheadingColumn + 2];
            variablequestionend = dataSheet.Cells[datasheetlastrow, IndividualevaluationvariableheadingColumn + 2 + Definiotion.VariableDictionary[variablename].Choices.Count - 1];
            variablequestionrange = dataSheet.get_Range(variablequestionstart, variablequestionend);
            variablequestionrange.NumberFormat = "@";

            variablequestionstart = dataSheet.Cells[datasheetlastrow + 1, IndividualevaluationvariableheadingColumn + 1];
            variablequestionend = dataSheet.Cells[IndividualevaluationvariablevalueRow + 1 + (Variables.Count - 1), IndividualevaluationvariableheadingColumn + 1];
            variablequestionrange = dataSheet.get_Range(variablequestionstart, variablequestionend);
            variablequestionrange.NumberFormat = "@";

            Excel.Range overallevaluationvaluesstart = dataSheet.Cells[datasheetlastrow, IndividualevaluationvariableheadingColumn];
            Excel.Range overallevaluationvalueslast = dataSheet.Cells[datasheetlastrow + 1, lastcolum - 1];
            Excel.Range overallevaluationvaluesrar = dataSheet.get_Range(overallevaluationvaluesstart, overallevaluationvalueslast);

            var overallevaluationvaluesobj = overallevaluationvaluesrar.Value;
            choiceno = 0;
            int variablno = 2;
            for (int i = 3; i < Definiotion.VariableDictionary[Variables[0]].Choices.Count + 3; i++)
            {
                overallevaluationvaluesobj[1, i] = Definiotion.VariableDictionary[Variables[0]].Choices[choiceno];
                overallevaluationvaluesobj[variablno, i] = ChoicePercentageArray[choiceno, 1].ToString() + "%";
                choiceno++;
            }
            overallevaluationvaluesobj[1, lastcolum - IndividualevaluationvariablevalueRow] = LocalResource.CSP_OVERALL_SATISFACTION;

            overallevaluationvaluesobj[2, 1] = 1;
            overallevaluationvaluesobj[2, 2] = Definiotion.VariableDictionary[Variables[0]].Question;


            overallevaluationvaluesobj[2, lastcolum - IndividualevaluationvariablevalueRow] = OverallTotalPercentage[overalllastpos, 2] + "%";

            overallevaluationvaluesrar.Value = overallevaluationvaluesobj;



            Excel.Range overallevaluationformatindividualstart = dataSheet.Cells[datasheetlastrow + 1, IndividualevaluationvariablevalueRow + 2];
            Excel.Range overallevaluationformatindividuallast = dataSheet.Cells[datasheetlastrow + 1, lastcolum - 1];
            Excel.Range overallevaluationformatindividualrar = dataSheet.get_Range(overallevaluationformatindividualstart, overallevaluationformatindividuallast);
            overallevaluationformatindividualrar.NumberFormat = @"0.00%";


            Excel.Range alignmentoverallevaluationheadingstart = dataSheet.Cells[datasheetlastrow, IndividualevaluationvariableheadingColumn + 2];
            Excel.Range alignmentoverallevaluationheadinglast = dataSheet.Cells[datasheetlastrow, lastcolum - 1];
            Excel.Range alignmentoverallrar = dataSheet.get_Range(alignmentoverallevaluationheadingstart, alignmentoverallevaluationheadinglast);
            PRV_WrapRange(alignmentoverallrar);
            //  PRV_AutoFitRange(alignmentoverallrar);
            PRV_HorizontalAlignmentCenterRange(alignmentoverallrar);
            PRV_VerticalAlignmentCenterRange(alignmentoverallrar);

            overallevaluationvaluesrar.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            overallevaluationvaluesrar.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlThin;
            overallevaluationvaluesrar.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            overallevaluationvaluesrar.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            overallevaluationvaluesrar.Borders.Color = 10921638;
            overallevaluationvaluesrar.Borders.Weight = XlBorderWeight.xlThin;
            // overallevaluationvaluesrar.BorderAround(XlLineStyle.xlContinuous, Weight: XlBorderWeight.xlThin, Color: 10921638);

            Excel.Range overallevaluationmergesstart = dataSheet.Cells[datasheetlastrow, IndividualevaluationvariableheadingColumn];
            Excel.Range overallevaluationmergesstop = dataSheet.Cells[datasheetlastrow, IndividualevaluationvariableheadingColumn + 1];
            Excel.Range overallevaluationmergeheading = dataSheet.get_Range(overallevaluationmergesstart, overallevaluationmergesstop);
            this.PRV_MergeConditionalRange(overallevaluationmergeheading);

            Excel.Range overallevaluationheadingsatisfactionstart = dataSheet.Cells[datasheetlastrow, lastcolum - 1];
            Excel.Range overallevaluationheadingsatisfactionlast = dataSheet.Cells[datasheetlastrow + 1, lastcolum - 1];
            Excel.Range overallevaluationheadingsatisfactionrange = dataSheet.get_Range(overallevaluationheadingsatisfactionstart, overallevaluationheadingsatisfactionlast);
            overallevaluationheadingsatisfactionrange.Interior.Color = 14610923;

            overallevaluationheadingsatisfactionrange.Worksheet.Columns.AutoFit();

            Excel.Range individualevaluationslnofitstart = dataSheet.Cells[lastslnopos, IndividualevaluationvariablevalueRow];
            Excel.Range individualevaluationslnofitlast = dataSheet.Cells[lastslnopos, IndividualevaluationvariablevalueRow];
            Excel.Range individualevaluationslnofitformatrar = dataSheet.get_Range(individualevaluationslnofitstart, individualevaluationslnofitlast);
            individualevaluationslnofitformatrar.Columns.AutoFit();

            //_log.Info("CSP analysis filter if any");
            if (!string.IsNullOrEmpty(EditString) && CriteriaFlagValue.Equals("1"))
            {
                int filterrow = datasheetlastrow + 6;
                Excel.Range filterstart = dataSheet.Cells[filterrow, CriteriavalueColumn];
                Excel.Range filterstop = dataSheet.Cells[filterrow + 1, CriteriavalueColumn];


                Excel.Range filtermergestart = dataSheet.Cells[filterrow, CriteriavalueColumn];
                Excel.Range filtermergestop = dataSheet.Cells[filterrow, CriteriavalueColumn + 3];
                Excel.Range filtermergerar = dataSheet.get_Range(filtermergestart, filtermergestop);
                filtermergerar.Interior.Color = 12611584;
                filtermergerar.Font.Color = 16777215;
                filtermergerar.Font.Bold = true;
                filtermergerar.Borders.Color = 10921638;
                filtermergerar.Borders.Weight = XlBorderWeight.xlThin;
                PRV_MergeConditionalRange(filtermergerar);

                filtermergestart = dataSheet.Cells[filterrow + 1, CriteriavalueColumn];
                filtermergestop = dataSheet.Cells[filterrow + 5, CriteriavalueColumn + 3];
                filtermergerar = dataSheet.get_Range(filtermergestart, filtermergestop);
                PRV_WrapRange(filtermergerar);
                filtermergerar.Borders.Color = 10921638;
                filtermergerar.Borders.Weight = XlBorderWeight.xlThin;

                PRV_VerticalAlignmentCenterRange(filtermergerar);
                PRV_MergeConditionalRange(filtermergerar);

                Excel.Range filterrar = dataSheet.get_Range(filterstart, filterstop);
                var filterobj = filterrar.Value;
                filterobj[1, 1] = LocalResource.CSP_FILTERCRITERIA;
                filterobj[2, 1] = EditString;
                filterrar.Value = filterobj;

            }

            // individualatisfactioncorelationrar.BorderAround(XlLineStyle.xlContinuous, Weight: XlBorderWeight.xlThin, Color: 4);
            //grpahSheet 

            //CSP_TOTAL_VALID_CASES
            //_log.Info("CSP analysis CSportfolio/Graph");
            long xlRows = 1;
            long xlColumns = 2;
            Excel.Range totalvalidcasesrar = grpahSheet.get_Range(strOutputCell_AllCount, strOutputCell_AllCount);
            totalvalidcasesrar.Value = string.Format(LocalResource.CSP_TOTAL_VALID_CASES, validresponses.ToString());

            Excel.Range CSrar = grpahSheet.get_Range(strOutputCell_SynthesisCs, strOutputCell_SynthesisCs);
            CSrar.Value = "=Data!" +
                    GetAddress(dataSheet.Cells[datasheetlastrow + 1, IndividualevaluationvariablevalueRow + Definiotion.VariableDictionary[variablename].Choices.Count + 2], xlColumns) +
                    GetAddress(dataSheet.Cells[datasheetlastrow + 1, IndividualevaluationvariablevalueRow + Definiotion.VariableDictionary[variablename].Choices.Count + 2], xlRows);// OverallTotalPercentage[overalllastpos, 2].ToString() + "%";// Math.Round(double.Parse(OverallTotalPercentage[overalllastpos, 2])) // correlresult[0].ToString()
                                                                                                                                                                                     //Math.Round(double.Parse(IndividualTotalPercentage[individuallastpos, i]));
            int lastrow = (IndividualSatisfactionCorrelationRow + Variables.Count + 3) - 2;
            Excel.Range individualatisfactioncorelationstart = grpahSheet.Cells[IndividualSatisfactionCorrelationRow, IndividualSatisfactionCorrelationColumn];
            Excel.Range individualatisfactioncorelationlast = grpahSheet.Cells[lastrow, IndividualSatisfactionCorrelationColumn + 2];
            Excel.Range individualatisfactioncorelationrar = grpahSheet.get_Range(individualatisfactioncorelationstart, individualatisfactioncorelationlast);

            var individualsatisfactionobj = individualatisfactioncorelationrar.Value;


            int rowcount = Variables.Count;// IndividualTotalPercentage.GetLength(1);
            int pos = 1;
            int individualpos = 2;
            double permean = 0;
            double permin = 0;
            double permax = 0;
            double persum = 0;
            int times = 0;
           

            for (int i = 1; i < rowcount; i++)
            {

                individualsatisfactionobj[i, 1] = Definiotion.VariableDictionary[Variables[i]].Question;
                individualsatisfactionobj[i, 2] = "=Data!" +
                    GetAddress(dataSheet.Cells[IndividualevaluationvariablevalueRow + i, IndividualevaluationvariablevalueRow + Definiotion.VariableDictionary[variablename].Choices.Count + 2], xlColumns) +
                    GetAddress(dataSheet.Cells[IndividualevaluationvariablevalueRow + i, IndividualevaluationvariablevalueRow + Definiotion.VariableDictionary[variablename].Choices.Count + 2], xlRows);// (double.Parse(IndividualTotalPercentage[individuallastpos, individualpos]) / 100.00).ToString();//Math.Round(double.Parse(IndividualTotalPercentage[individuallastpos, individualpos]));
                individualsatisfactionobj[i, 3] = "=Data!" +
                    GetAddress(dataSheet.Cells[IndividualevaluationvariablevalueRow + i, IndividualevaluationvariablevalueRow + Definiotion.VariableDictionary[variablename].Choices.Count + 3], xlColumns) +
                    GetAddress(dataSheet.Cells[IndividualevaluationvariablevalueRow + i, IndividualevaluationvariablevalueRow + Definiotion.VariableDictionary[variablename].Choices.Count + 3], xlRows);//(correlresult[i - 1]).ToString();//=Data!G4 =Data!H4//"#.##" //Math.Round(correlresult[i - 1]);
                                                                                                                                                                                                         //GetAddress(ResultDataSheet.Cells(PasteRow + 1, LastCell.Column), xlRows   
                                                                                                                                                                                                         //GetAddress(ResultDataSheet.Cells(PasteRow + 1, LastCell.Column), xlColumns)
                                                                                                                                                                                                         //"=Data!R" + OR_PercentTitle + "C" + (T_Col + 1);
                                                                                                                                                                                                         //if (permin > double.Parse(IndividualTotalPercentage[individuallastpos, individualpos]))
                                                                                                                                                                                                         //{
                                                                                                                                                                                                         //    permin = double.Parse(IndividualTotalPercentage[individuallastpos, individualpos]);
                                                                                                                                                                                                         //}
                                                                                                                                                                                                         //if (permax < double.Parse(IndividualTotalPercentage[individuallastpos, individualpos]))
                                                                                                                                                                                                         //{
                                                                                                                                                                                                         //    permax = double.Parse(IndividualTotalPercentage[individuallastpos, individualpos]);
                                                                                                                                                                                                         //}
                                                                                                                                                                                                         //persum += double.Parse(IndividualTotalPercentage[individuallastpos, individualpos]);
                individualpos = individualpos + 2;
                pos = i;
                // times++;
            }
            // permean = persum / times;
            pos = pos + 1;
            int formulalastrow = ((rowcount - 1) + 11) - 1;
            individualsatisfactionobj[pos, 1] = LocalResource.CSP_AVG;
            individualsatisfactionobj[pos, 2] = "= AVERAGE(O11: O" + formulalastrow + ")";// permean / 100;
            individualsatisfactionobj[pos, 3] = "= AVERAGE(P11: P" + formulalastrow + ")";// mean;
            Row_Average = IndividualSatisfactionCorrelationRow + pos - 1;

            pos = pos + 1;
            individualsatisfactionobj[pos, 1] = LocalResource.CSP_MIN;
            individualsatisfactionobj[pos, 2] = "= MIN(O11: O" + formulalastrow + ")";// permin / 100;
            individualsatisfactionobj[pos, 3] = "= MIN(P11: P" + formulalastrow + ")";// min;
            Row_Min = IndividualSatisfactionCorrelationRow + pos - 1;

            pos = pos + 1;
            individualsatisfactionobj[pos, 1] = LocalResource.CSP_MAX;
            individualsatisfactionobj[pos, 2] = "= MAX(O11: O" + formulalastrow + ")";//permax / 100;
            individualsatisfactionobj[pos, 3] = "= MAX(P11: P" + formulalastrow + ")";// max;
            Row_Max = IndividualSatisfactionCorrelationRow + pos - 1;

            individualatisfactioncorelationrar.Value = individualsatisfactionobj;

            this.Output_SheetVisibleControl(individualatisfactioncorelationrar);

            individualatisfactioncorelationrar.Borders.Color = 10921638;
            individualatisfactioncorelationrar.Borders.Weight = XlBorderWeight.xlThin;

            individualatisfactioncorelationrar.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            individualatisfactioncorelationrar.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
            individualatisfactioncorelationrar.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            individualatisfactioncorelationrar.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;

            Excel.Range seperatestart = grpahSheet.Cells[IndividualSatisfactionCorrelationRow, IndividualSatisfactionCorrelationColumn];
            Excel.Range seperatestop = grpahSheet.Cells[Row_Average - 1, IndividualSatisfactionCorrelationColumn + 2];
            Excel.Range sepearterar = grpahSheet.get_Range(seperatestart, seperatestop);
            sepearterar.Borders.Color = 10921638;
            sepearterar.Borders.Weight = XlBorderWeight.xlThin;
            sepearterar.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            sepearterar.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
            sepearterar.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            sepearterar.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;

        }

        public void PRV_MergeConditionalRange(Range TargetRange)
        {
            if (TargetRange.Cells.Count > 1)
            {
                TargetRange.Merge();
            }
        }
        public void PRV_WrapRange(Range TargetRange)
        {
            if (TargetRange.Cells.Count > 1)
            {
                TargetRange.EntireColumn.WrapText = true;
            }
        }
        public void PRV_WrapSelectedRange(Range TargetRange)
        {
            if (TargetRange.Cells.Count > 1)
            {
                TargetRange.WrapText = true;
            }
        }
        private void PRV_AutoFitRange(Range TargetRange)
        {
            if (TargetRange.Cells.Count > 1)
            {
                TargetRange.EntireColumn.AutoFit();
            }
        }

        private void PRV_HorizontalAlignmentCenterRange(Range TargetRange)
        {
            if (TargetRange.Cells.Count > 1)
            {
                TargetRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;// Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenterAcrossSelection;
            }
        }
        public void PRV_VerticalAlignmentCenterRange(Range TargetRange)
        {
            if (TargetRange.Cells.Count > 1)
            {
                TargetRange.VerticalAlignment = XlHAlign.xlHAlignCenter;
            }
        }
        private void Output_GraphSheet(double currentProgress)
        {
            ChartObject T_ChartObj;
            T_ChartObj = grpahSheet.ChartObjects("CSPortfolio_Graph");
            T_ChartObj.Chart.ChartArea.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            T_ChartObj.Chart.ChartArea.Font.Size = 12;
            T_ChartObj.Activate();
            currentProgress = 80;
            OnWorkerComplete(currentProgress, LocalResource.CSP_PB_CREATING_GRAPH);
            grpahSheet.Cells[1, 1].Select();
            //  Output_GraphSheet_Init(T_ChartObj);
            //_log.Info("CSP analysis adding series in graph");
            currentProgress = 85;
            OnWorkerComplete(currentProgress, LocalResource.CSP_PB_CREATING_GRAPH);
            Output_GraphSheet_AddSeries(T_ChartObj);
            //_log.Info("CSP analysis setting axis in graph");
            currentProgress = 90;
            OnWorkerComplete(currentProgress, LocalResource.CSP_PB_CREATING_GRAPH);
            Output_GraphSheet_SetAxis(T_ChartObj);
            currentProgress = 95;
            OnWorkerComplete(currentProgress, LocalResource.CSP_PB_CREATING_GRAPH);
            //_log.Info("CSP analysis setting colors in graph");
            Output_GraphSheet_SetColors(T_ChartObj);

            foreach (Series T_Series in T_ChartObj.Chart.SeriesCollection())
            {
               
                    T_Series.DataLabels().Font.Size = 9;
               
            }
        }

        private void Output_GraphSheet_Init(ChartObject T_ChartObj)
        {
            Series T_Series;
            // foreach (var T_Series in T_ChartObj.Chart.SeriesCollection)
            //    T_Series.Delete();
        }

        private void Output_GraphSheet_AddSeries(ChartObject T_ChartObj)
        {
            long T_Row;
            Series T_Series;
            long i;
            i = 1;
            for (T_Row = GraphRow_DataStart; T_Row < Row_Average; T_Row++)
            {
                
                T_Series = T_ChartObj.Chart.SeriesCollection().NewSeries;
                T_Series.XValues = grpahSheet.Cells[T_Row, Graph_IndividualSatisfactionCorrelationColumn];
                T_Series.Values = grpahSheet.Cells[T_Row, Graph_IndividualSatisfactionCorrelationPercent];
                T_Series.Name = "='" + grpahSheet.Name + "'!" + grpahSheet.Cells[T_Row, Graph_IndividualSatisfactionCorrelationQuestion].Address;//Redmine id:207119   //grpahSheet.Cells[T_Row, Graph_IndividualSatisfactionCorrelationQuestion].Text;
              


                {
                    var withBlock = T_Series;
                  
                    withBlock.MarkerBackgroundColor = 12611584;// FNC_Color_Info_Get(CP_CSP_MarkerBack, 1);
                    withBlock.MarkerForegroundColor = 12611584;// FNC_Color_Info_Get(CP_CSP_MarkerFore, 1);
                    withBlock.MarkerStyle = XlMarkerStyle.xlMarkerStyleDiamond;
                    withBlock.Smooth = false;
                    withBlock.MarkerSize = 5;
                    withBlock.Shadow = false;
                    withBlock.ApplyDataLabels(HasLeaderLines: true, ShowSeriesName: true, ShowPercentage: false, ShowCategoryName: false, Type: XlDataLabelsType.xlDataLabelsShowLabel);
                   
                    i = i + 1;
                }
            }
        }
        private void Output_GraphSheet_SetAxis(ChartObject T_ChartObj)
        {
            double AxisMax;
            double AxisMin;
            double AxisLength;
            WorksheetFunction wsf = xlApp.WorksheetFunction;
            AxisMin = grpahSheet.Cells[Row_Min, Graph_IndividualSatisfactionCorrelationColumn].Value;
            AxisMax = grpahSheet.Cells[Row_Max, Graph_IndividualSatisfactionCorrelationColumn].Value;
            AxisLength = AxisMax - AxisMin;
            if (AxisMin - AxisLength / 100 < 0)
                AxisMin = wsf.RoundUp(AxisMin - AxisLength / 100, 2);
            else
                AxisMin = wsf.RoundDown(AxisMin - AxisLength / 100, 2);
            AxisMax = wsf.RoundUp(AxisMax + AxisLength / 100, 2);
            AxisLength = AxisMax - AxisMin;
            {
                var withBlock = T_ChartObj.Chart.Axes(XlAxisType.xlCategory);
                withBlock.MinimumScale = AxisMin;
                withBlock.MaximumScale = AxisMax;
                if (AxisLength > 0)
                {
                    withBlock.MinorUnit = AxisLength / 100;
                    withBlock.MajorUnit = AxisLength / 10;
                }

                withBlock.CrossesAt = grpahSheet.Cells[Row_Average, Graph_IndividualSatisfactionCorrelationColumn].Value;
            }

           
            AxisMin = wsf.RoundDown(grpahSheet.Cells[Row_Min, Graph_IndividualSatisfactionCorrelationPercent].Value, 1);
            AxisMax = wsf.RoundUp(grpahSheet.Cells[Row_Max, Graph_IndividualSatisfactionCorrelationPercent].Value, 1);
            AxisLength = AxisMax - AxisMin;
            {
                var withBlock = T_ChartObj.Chart.Axes(XlAxisType.xlValue);
                withBlock.MinimumScale = AxisMin;
                withBlock.MaximumScale = AxisMax;
                withBlock.MinorUnit = 0.01;
                withBlock.MajorUnit = 0.1;
                withBlock.CrossesAt = grpahSheet.Cells[Row_Average, Graph_IndividualSatisfactionCorrelationPercent].Value;
            }
            T_ChartObj.Chart.ChartTitle.Font.Size = 12;
            T_ChartObj.Chart.Axes(XlAxisType.xlCategory).AxisTitle.Font.Size = 12;
            T_ChartObj.Chart.Axes(XlAxisType.xlValue).AxisTitle.Font.Size = 12;

            if(wsf!=null)
            {
                try { COMWholeOperate.releaseComObject(ref wsf); } catch { }
            }
        }
        private void Output_GraphSheet_SetColors(ChartObject T_ChartObj)
        {
            Microsoft.Office.Core.MsoGradientStyle Style_G;
            Style_G = 0;// FNC_Get_GradientStyle(CP_CSP_GraphBack);
            T_ChartObj.Chart.PlotArea.Interior.Pattern = XlPattern.xlPatternSolid;
            T_ChartObj.Chart.PlotArea.Interior.Color = 16777215;// FNC_Color_Info_Get(CP_CSP_GraphBack, 1);
            
        }
        private void Output_SheetVisibleControl(Range TargetRange)
        {
            if (TargetRange.Cells.Count > 1)
            {
                TargetRange.EntireColumn.AutoFit();
            }
            
        }
        public string Edit_SiborikomiAll(string variable, string operatorType, string value, string conditionType, Questions questions)
        {
            //Questions questions;
            // string EditString = "";
            string space = " ";
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
            {
                space = "";
            }
            if (string.IsNullOrEmpty(variable) || string.IsNullOrEmpty(operatorType) || string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }


            if (variable == null || variable.Length == 0)
            {
                return string.Empty;
            }
            
            if (EditString.Length > 0)
            {
                EditString += "\n";
            }
            QuestionSettings qstnDet = Qc4Launcher.Util.Definiotion.VariableDictionary[variable];
            Questions.Question itemInfo = (Questions.Question)questions[qstnDet.Id];
            EditString += itemInfo.Name2 + space; // itemInfo.Name2;

            if (value == u_DK)
            {
                EditString += strEdit01 + strNoAnswer;
            }
            else if (value == u_Asterisk)
            {
                EditString += strEdit01 + strNotApply;
            }
            else
            {
                EditString += strEdit01 + value + space;
            }


            if (operatorType == Sign_LG)
            {
                EditString += strEdit_LG;
            }
            else if (operatorType == Sign_E)
            {
                EditString += strEdit_E;
            }
            else if (operatorType == Sign_LE)
            {
                EditString += strEdit_LE;
            }
            else if (operatorType == Sign_GE)
            {
                EditString += strEdit_GE;
            }
            else if (operatorType == Sign_L)
            {
                EditString += strEdit_L;
            }
            else if (operatorType == Sign_G)
            {
                EditString += strEdit_G;
            }


            if (conditionType == D_AND)
            {
                EditString += strEdit_And;
            }
            else if (conditionType == D_OR)
            {
                EditString += strEdit_Or;
            }


            return EditString;
        }

        public string GetAddress(Excel.Range range, long RowCol)
        {
            var vArr = range.Address.Split('$');
            return vArr[3 - RowCol];
        }
        private bool CalculateSumCount(System.Data.DataTable dt, bool[] filterringFlg, bool[] individualfilterringFlg, System.Data.DataTable dtindividual, int column, ref double[,] SumArray)
        {
            try
            {
                SumArray[0, 0] = 0;
                SumArray[0, 1] = 0;
                int result = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((filterringFlg[i] == true) && (individualfilterringFlg[i] == true))
                    {
                        if (int.TryParse(Convert.ToString(dt.Rows[i][0]), out result) && (int.TryParse(Convert.ToString(dtindividual.Rows[i][column]), out result)))
                        {
                            SumArray[0, 0] += double.Parse(Convert.ToString(dt.Rows[i][0]));
                            SumArray[0, 1] += 1;
                        }
                    }
                }
                for (int i = 0; i < dtindividual.Rows.Count; i++)
                {

                    if ((filterringFlg[i] == true) && (individualfilterringFlg[i] == true))
                    {
                        if (int.TryParse(Convert.ToString(dtindividual.Rows[i][column]), out result))
                        {
                            SumArray[column + 1, 0] += double.Parse(Convert.ToString(dtindividual.Rows[i][column]));
                            SumArray[column + 1, 1] += 1;
                        }
                    }
                }
            }
            catch { return false; }
            return true;
        }
    }
}
