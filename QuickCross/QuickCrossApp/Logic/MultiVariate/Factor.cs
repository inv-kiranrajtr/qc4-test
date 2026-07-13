using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.DataProcess;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using static Qc4Launcher.Forms.Multivariate_Analysis.CollectionClass;
using Constants = Qc4Launcher.Util.Constants;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelUtil = Qc4Launcher.Util.ExcelUtil;
using Qc4Launcher.Util;
using Qc4Launcher.DB;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using DBHelperCommon = QC4Common.DB.DBHelper;
using Macromill.QCWeb.Common;
using System.Data;

namespace Qc4Launcher.Logic.MultiVariate
{
    class Factor
    {
        private const string Factor_Template = "Analysis.xlsx";//"Analysis.xlsx"//Analysis.xls
        private const string Factor_Template_JaJp = "Analysis_JaJp.xlsx";//"Analysis.xlsx"//Analysis.xls
        private const string Factor_Rawdata = "Sheet1";// Rawdata
        private const string Factor_Analysis = "Sheet2";// //Cluster Analysis
        private const string Factor_GraphSheet = "Sheet3";//Factor Analysis

        private const string strSelectionCell_A_1 = "A1";
        private const string strOutputCell_C_A = "C3";
        private const string strOutputCell_C_B = "C4";
        private const string strOutputCell_C_C = "F3";
        private const string strOutputCell_C_D = "F4";
        private const string strOutputCell_C_F = "C8";
        private const string strOutputCell_C_G = "F8";
        private const string strOutputCell_C_H = "F9";
        private const string strOutputCell_C_I = "B13";
        private const string strOutputCell_C_J = "F7";
        private const string strOutputCell_C_K = "F12";
        private const string strOutputCell_C_L = "F13";
        private const string strOutputCell_C_M = "B:B";
        private const string strOutputCell_C_N = "I3";
        private const string strCenterBalance = "重心";

        private const string P_Str_Eigen1Over = "ŒÅ—L’l1ˆÈã";
        private const string P_Str_SMC = "SMC–@";
        private const string P_Str_RMax = "RMax–@";
        private const string P_Str_EstimationUse1 = "„’ès—ñŒvŽZ‚É";
        private const string P_Str_EstimationUse2 = "‚ðŽg—p‚µ‚Ü‚µ‚½B";
        private const string P_StrIn_Target = "•ªÍ‘ÎÛ";
        private const string P_StrIn_FactorCount = "";
        private const string P_StrIn_RotationType = "";
        private const string P_StrIn_BfFactorLodingMatCalcLim = "";
        private const string P_StrIn_BfFactorLodingMatEps = "";
        private const string P_StrIn_RotationCalcLim = "";
        private const string P_StrIn_RotationEps = "";
        private const string P_StrIn_EigenCalcLim = "";
        private const string P_StrIn_EigenEps = "";
        private const string P_StrCr_StandardScoreMat = "";
        private const string P_StrCr_R = "";
        private const string P_StrCr_MethodForRAster = "„’ès—ñ";
        private const string P_StrCr_RAster = "„’ès—ñ";
        private const string P_StrCr_RBtwFactor = "ˆöŽq‘ŠŠÖ";
        private const string P_StrCr_FirstEigenVals = "‰ŠúŒÅ—L’l";
        private const string P_StrCr_EigenVals = "ŒÅ—L’l";
        private const string P_StrCr_FirstEigenVecMat = "‰ŠúŒÅ—LƒxƒNƒgƒ‹";
        private const string P_StrCr_EigenVecMat = "ŒÅ—LƒxƒNƒgƒ‹";
        private const string P_StrCr_FactorCount = "";
        private const string P_StrCr_FirstFactorLoadings = "";
        private const string P_StrCr_FactorLoadings = "";
        private const string P_StrCr_Bf_FactorLoadings = "";
        private const string P_StrCr_Af_FactorLoadings = "";
        private const string P_StrCr_Af_FactorLoadings_Pattern = "";
        private const string P_StrCr_FirstCommunalities = "";
        private const string P_StrCr_Bf_Communalities = "";
        private const string P_StrCr_Af_Communalities = "";
        private const string P_StrCr_FirstContributionalAmounts = "";
        private const string P_StrCr_ContributionalAmounts = "";
        private const string P_StrCr_FirstContributions = "‰ŠúŠñ—^—¦";
        private const string P_StrCr_Contributions = "Šñ—^—¦";
        private const string P_StrCr_FirstContributionsTotal = "‰Šú—ÝÏŠñ—^—¦";
        private const string P_StrCr_ContributionsTotal = "—ÝÏŠñ—^—¦";
        private const string P_StrCr_FactorScoreCoefficient = "";
        private const string P_StrCr_FactorScore = "";
        private const string P_StrCr_Bf_FactorSumSquares = "’ŠoŒã‚Ì•‰‰×—Ê•½•û˜a";
        private const string P_StrCr_Af_FactorSumSquares = "";
        private const string P_StrCr_Rotation = "";
        private const double P_BfFactorLodingMatEps = 0.00001;
        private const double P_RotationEps = 0.00001;
        private const string strOutputBookName = "Analysis.xls";
        private const string strOutputBookName2007 = "Analysis.xlsx";
        private const string strOutputSheetName_R = "Rawdata";
        private const string strOutputSheetName_F = "ˆöŽq•ªÍ";
        private const string strOutputCell_E_B = "B5";
        private const string strOutputCell_F_A = "C3";
        private const string strOutputCell_F_B = "C4";
        private const string strOutputCell_F_BB = "C5";
        private const string strOutputCell_F_C = "G3";
        private const string strOutputCell_F_D = "G4";
        private const string strOutputCell_F_E = "B7";
        private const string strOutputCell_F_F = "B8";
        private const string strOutputCell_F_G = "B9";
        private const string strOutputCell_F_H = "B10";
        private const string strOutputCell_F_H11 = "B11";
        private const string strOutputCell_F_H12 = "B12";
        private const string strOutputCell_F_I = "B39";
        private const string strOutputCell_F_II = "B40";
        private const string strOutputCell_F_J = "J39";
        private const string strOutputCell_F_K = "G8";
        private const string strOutputCell_F_O = "I2";
        private bool P_RotateOffFlag;
        private const string strObjName = "ScreePlot";
        private const long ProMax_m_Cnt = 4;
        private const string strOutputCell_E_10 = "E10";
        private const string strOutputCell_E_11 = "E11";


        private const int ResultSizeRow = 7;
        private const int ResultSizeColum = 6;

        private const int ResultCentersRow = 12;
        private const int ResultCentersColumn = 2;

        private const int InitialEig_Start_Row = 38;
        private const int InitialEig_Start_Column = 2;
        private const int InitialEig_End_Row = 39;
        private const int InitialEig_End_Column = 8;
        private const int Communality_Start_Row = 38;
        private const int Communality_Start_Column = 10;
        private const int Communality_End_Row = 39;
        private const int Communality_End_Column = 12;
        private const int Squared_Start_Row = 42;
        private const int Squared_Start_Column = 2;
        private const int Squared_End_Row = 43;
        private const int Squared_End_Column = 5;
        private const int FactorLoading_Start_Row = 42;
        private const int FactorLoading_Start_Column = 10;
        private const int FactorLoading_End_Row = 43;
        private const int FactorLoading_End_Column = 11;
        private const string Note = "B46";
        private const int NoteRow = 46;
        private const int NoteColumn = 2;
        private const int FactorCorrel_Start_Row = 46;
        private const int FactorCorrel_Start_Column = 10;
        private const int FactorCorrel_End_Row = 47;
        private const int FactorCorrel_End_Column = 12;

        private const int InitialEig_Value_Row = 40;
        private const int InitialEig_Value_Column = 2;
        private const int InitialEig_Value_End_Column = 8;
        private const int Communality_Value_Row = 40;
        private const int Communality_Value_Column = 10;
        private const int Communality_Value_End_Row = 40;
        private const int Communality_Value_End_Column = 12;
        private const int Squared_Value_Row = 44;
        private const int Squared_Value_Column = 2;
        private const int Squared_Value_End_Row = 44;
        private const int Squared_Value_End_Column = 5;
        private const int FactorLoading_Value_Row = 44;
        private const int FactorLoading_Value_Column = 10;
        private const int FactorLoading_Value_End_Row = 44;
        private const int FactorLoading_Value_End_Column = 11;
        private const int FactorCorrel_Value_Row = 47;
        private const int FactorCorrel_Value_Column = 11;
        private const int FactorCorrel_Value_End_Row = 47;
        private const int FactorCorrel_Value_End_Column = 11;

        private const string Factor_R_File_Name = "qcr_factor.R";
        private const string Factor_Input_File_Name = "inshicluster.txt";
        private const string Factor_OutPut_resultScores_File_Name = "result_scores.tsv";
        private const string Factor_OutPut_result_Evalues_File_Name = "result_e-values.tsv";
        private const string Factor_OutPut_result_values_File_Name = "result_values.tsv";
        private const string Factor_OutPut_result_communality_File_Name = "result_communality.tsv";
        private const string Factor_OutPut_result_loadings_File_Name = "result_loadings.tsv";
        private const string Factor_OutPut_result_vaccounted_File_Name = "result_vaccounted.tsv";
        private const string Factor_OutPut_result_phi_File_Name = "result_phi.tsv";
        private const string Factor_OutPut_result_nfactors_File_Name = "result_nfactors.tsv";

        public const string FactorAnalysis_temp_VariableName = "tAN1";
        public const string FactorAnalysis_VariableName_Suffix = "S";
        public string AN_Variable_TableHeading = string.Empty;
        public const string An_Variable_QuestionText = "FactorResults";
        public const string An_Process_Type = "Factor";
        public const string FactorRotationNone = "None";
        public const string FactorRotationVarimax = "Varimax";
        public const string FactorRotationPromax = "Promax";
        public const string FactorExtractionml = "ml";//https://app.gluemodel.com/#/project/task/4295064637
        public const string FactorExtractionpa = "pa";//https://app.gluemodel.com/#/project/task/4295064637


        public static string D_AND = "AND";
        public static string D_OR = "OR";

        public string seperator = "\t";
        public string FileEncoding = "UTF-8";//"Windows-1252";// "UTF-8";

        public const int FactorRowStart = 5;
        public const int FactorRowEnd = 10;
        public const int MAX_MULTIVARIAT_COLUMN = 1040;
        public const int FactorColumnStart = 2;
        public const int FactorColumnEnd = 235;
        public const int FactorAnalysisVariableColumnStart = 137;

        public const int Factor_CriteriaFlag = 7;
        public const int Factor_Rotation = 8;
        //public const int Factor_Rotation_Method = 7;
        public const int Factor_Extraction = 9;
        public const int Factor_Extracted_Adopt = 10;
        public const int Factor_Nos = 11;
        public const int Factor_Assign_Score_Reverse = 12;
        public const int Factor_SaveSettings = 13;
        public const int Factor_Variables = 14;

        public const int MaxNoofIterations = 999;

        string CriteriaFlagValue = string.Empty;
        string FactorValue = string.Empty;
        string FactorRotation = string.Empty;
        string FactorExtraction = string.Empty;
        string FactorExtractedOrAdopt = string.Empty;
        string FactorAssignScores = string.Empty;
        string SaveSettings = string.Empty;
        string CriteriaQuerystring = string.Empty;
        string FilterSettingsQuerystring = string.Empty;
        string RemoveUnknownandInvalidQuerystring = string.Empty;
        public delegate void OnWorkerMethodCompleteDelegate(double value, string status, bool isForceStop = false, bool retainThread = false, bool close = false, bool disableCancel = false);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        public void updateProgress(double currentProgress, string v, bool close = false)
        {
            OnWorkerComplete(currentProgress, v, close: close);
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        CS_Portfolio csp = new CS_Portfolio();
        public Application xlApp;
        public Workbooks wbs;
        Workbook baseBook = null;
        Worksheet rawdataSheet = null;
        Worksheet analysisSheet = null;
        Worksheet factorSheet = null;
        public string templateDirectoryPath;
        public string RFolderPath = string.Empty;
        string outputfilepath = string.Empty;
        int totalrowcount = 0;
        System.Data.DataTable rawdataTble = null;
        string Factorrawdatavariablename = string.Empty;
        Workbook workbook = null;
        Cluster c = new Cluster();
        int insertrowno;
        List<string> variablenames;
        List<int> startpos;
        bool isdp = false;
        bool Iscomm = false;
        public bool ExecuteFactor(Workbook workBook, out string errmsg)
        {
            bool returnvalue = false;
            errmsg = string.Empty;
            string rscriptCmd = string.Empty;
            bool IsCriteria = false;
            ExcelOperate excelOperate = null;
            double currentProgress = 1;
            Application xlApp = null;
            workbook = workBook;
            csp = new CS_Portfolio();
            insertrowno = 0;
            startpos = new List<int>();
            variablenames = new List<string>();
            try
            {
                _log.Info("Factor analysis started");
                OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
                Worksheet sht = ExcelUtil.GetWorkSheetBySheetName(workBook, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.MultiVariate);
                Excel.Range ClustrStart = sht.Cells[FactorRowStart, FactorColumnStart];
                Excel.Range Clusterlast = sht.Cells[FactorRowEnd, FactorColumnEnd];
                Excel.Range clstrrar = sht.get_Range(ClustrStart, Clusterlast);

                object[,] values = (object[,])clstrrar.Value2;
                bool ismultivariatetable = false;
                int validresponses = 0;
                string varibleid = string.Empty;
                Questions questions = DictUpdate.GetQuestions(workBook);
                List<string> Variables = new List<string>();
                List<string> SelectedChoices = new List<string>();
                System.Data.DataTable dataTble = new System.Data.DataTable();
                List<string> Variableids = new List<string>();
                string tableName = "answers";
                string tableNameMV = "multivariate";
                if (DBHelper.checkAfterProcess(workBook))
                {
                    tableName = "data_after_process";
                    isdp = true;
                }

                currentProgress = 10;
                OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);
                csp.CriteriaQuerystring = string.Empty;
                for (int i = 1; i <= values.GetLength(0); i++)
                {
                    string CriteriaVariableText = Convert.ToString(values[i, (CS_Portfolio.CriteriaVariableColumn - 1)]);
                    string criteriaOperatorText = Convert.ToString(values[i, (CS_Portfolio.CriteriaOperatorColumn - 1)]);
                    string criteriaValueText = Convert.ToString(values[i, (CS_Portfolio.CriteriavalueColumn - 1)]);
                    if (Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]).Equals(QC4Common.Common.Constants.DP.InstructionAND) ||
                    Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]).Equals(QC4Common.Common.Constants.DP.InstructionOR))
                    {
                        IsCriteria = true;
                        if (!string.IsNullOrEmpty(CriteriaVariableText))
                        {
                            csp.FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, i, (CS_Portfolio.MultiVariateInstructionColumn - 1), false, false, false, Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]), sht);
                            //csp.Edit_SiborikomiAll(CriteriaVariableText, criteriaOperatorText, criteriaValueText, Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]), questions);
                            csp.EditString = Macromill.QCWeb.Logic.TabulationEx.Criteria.CriteriaDescProvider.Edit_SiborikomiAll_General(CriteriaVariableText, criteriaOperatorText, criteriaValueText, Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]), questions, csp.EditString);
                        }
                    }
                    else if (Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]).Equals(ProcessingMethod.FACTOR_ANALYSIS))
                    {
                        try { CriteriaFlagValue = Convert.ToString(values[i, (Factor_CriteriaFlag - 1)]); }
                        catch { }

                        try { FactorRotation = Convert.ToString(values[i, (Factor_Rotation - 1)]); }
                        catch { }
                        try { FactorExtraction = Convert.ToString(values[i, (Factor_Extraction - 1)]); }
                        catch { }
                        try { FactorExtractedOrAdopt = Convert.ToString(values[i, (Factor_Extracted_Adopt - 1)]); }
                        catch { }
                        //  Factor_Reverse
                        if (FactorExtractedOrAdopt.Equals("1"))
                        {
                            try { FactorValue = Convert.ToString(values[i, (Factor_Nos - 1)]); }
                            catch { }
                        }
                        else
                        {
                            FactorValue = "0";
                        }
                        try { FactorAssignScores = Convert.ToString(values[i, (Factor_Assign_Score_Reverse - 1)]); }
                        catch { }


                        try { SaveSettings = Convert.ToString(values[i, (Factor_SaveSettings - 1)]); }
                        catch { }

                        FactorRotation = (FactorRotation.Equals("3") ? FactorRotationNone : (FactorRotation.Equals("1") ? FactorRotationVarimax : FactorRotationPromax));
                        FactorExtraction = (FactorExtraction.Equals("2") ? FactorExtractionml : FactorExtractionpa);//https://app.gluemodel.com/#/project/task/4295064637

                        int startindex = Factor_Variables - 1;
                        int limit = startindex + 100;
                        for (int j = startindex; j < limit; j++)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(values[i, j])))
                            {
                                break;
                            }
                            Variables.Add(Convert.ToString(values[i, j]));
                        }


                        //TODO need validation for selected  variable choice count

                        if (!string.IsNullOrEmpty(CriteriaVariableText) && CriteriaFlagValue.Equals("1"))
                        {
                            IsCriteria = true;
                            csp.FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, FactorRowStart + i - 1, (CS_Portfolio.CriteriaVariableColumn), false, false, false, Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]), sht);
                            //csp.Edit_SiborikomiAll(CriteriaVariableText, criteriaOperatorText, criteriaValueText, Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]), questions);
                            csp.EditString = Macromill.QCWeb.Logic.TabulationEx.Criteria.CriteriaDescProvider.Edit_SiborikomiAll_General(CriteriaVariableText, criteriaOperatorText, criteriaValueText, Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]), questions, csp.EditString);
                        }


                        FilterSettingsQuerystring = csp.CriteriaQuerystring;
                        csp.CriteriaQuerystring = string.Empty;

                        foreach (string varnam in Variables)
                        {

                            csp.FetchCriteria(varnam, "!=", "*", i, (CS_Portfolio.MultiVariateInstructionColumn - 1), false, false, false, QC4Common.Common.Constants.DP.InstructionAND, sht);
                            csp.FetchCriteria(varnam, "!=", "DK", i, (CS_Portfolio.MultiVariateInstructionColumn - 1), false, false, false, QC4Common.Common.Constants.DP.InstructionAND, sht);
                            varibleid += string.IsNullOrEmpty(varibleid) ? ("q_" + (Definiotion.VariableDictionary[varnam].ItemId).ToString()) : (",q_" + (Definiotion.VariableDictionary[varnam].ItemId).ToString());
                            Variableids.Add(("q_" + (Definiotion.VariableDictionary[varnam].ItemId).ToString()));
                        }
                        RemoveUnknownandInvalidQuerystring = csp.CriteriaQuerystring;
                        if (!string.IsNullOrEmpty(RemoveUnknownandInvalidQuerystring))
                        {
                            RemoveUnknownandInvalidQuerystring = RemoveUnknownandInvalidQuerystring.Remove(RemoveUnknownandInvalidQuerystring.Length - 3);
                        }


                        string delim = string.Empty;
                        int opencount = RemoveUnknownandInvalidQuerystring.Count(f => f == '(');
                        int closecount = RemoveUnknownandInvalidQuerystring.Count(f => f == ')');
                        int diff = opencount - closecount;
                        if (diff != 0) delim = ")";
                        RemoveUnknownandInvalidQuerystring = (RemoveUnknownandInvalidQuerystring + delim);



                        break;
                    }
                }
                currentProgress = 20;
                OnWorkerComplete(currentProgress, "");

                // _log.Info("Factor analysis Fetching DT");
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                {
                    try
                    {
                        bool isMv = false;
                        string tableNameMv = string.Empty;
                        con.Open();
                        dataTble = new System.Data.DataTable();
                        foreach (string varnm in Variables)
                        {
                            if (Definiotion.VariableDictionary[varnm].QuestionFlag == Util.Constants.Variable_Type_An)
                            {
                                ismultivariatetable = true;
                                break;
                            }

                        }
                        if (!ismultivariatetable)
                        {
                            dataTble = DBHelper.GetDataTable("Select sort_no,sample_id," + varibleid + " from " + tableName + " order by sort_no ", con);
                        }
                        else
                        {
                            dataTble = DBHelper.GetDataTable("Select a.sort_no as sort_no,a.sample_id as sample_id," + varibleid + " from " + tableName + " a join " + tableNameMV + " m where a.sample_id=m.sample_id order by a.sort_no ", con);
                        }
                    }
                    catch (Exception ex)
                    {
                        errmsg = LocalResource.ERRMSG_MULTIVARIATE_FAILED_TO_GET_DATA;
                        return false;
                        // _log.Warn(ex.Message);
                    }
                }
                try
                {
                    validresponses = dataTble.Rows.Count;
                    totalrowcount = dataTble.Rows.Count;
                    rawdataTble = dataTble;
                }
                catch
                {
                    errmsg = LocalResource.CSP_ERRMSG_CANNOT_ANALYSE_VALID_CASES_IS_ZERO;
                    return false;
                }
                bool[] filterringFlg = new bool[validresponses];
                MultivariateTable mt = new MultivariateTable();
                filterringFlg = mt.SetFilterflag(filterringFlg, true);
                filterringFlg = mt.GetFilterForInvalidUnknow(dataTble, filterringFlg);
                // filterringFlg = new Criteria(RemoveUnknownandInvalidQuerystring, "", questions).Filtering(DBHelper.GetConnectionString(workBook), tableName: tableName, dt: dataTble);//, dt: dataTble
                if (!string.IsNullOrEmpty(FilterSettingsQuerystring) && CriteriaFlagValue.Equals("1"))
                {
                    new Criteria(FilterSettingsQuerystring, "", questions, filterringFlg != null ? Operator.opAnd : Operator.opOr).Filtering(ref filterringFlg, DBHelper.GetConnectionString(workBook), tableName);//-filtermay not hav  variables in table
                }
                validresponses = filterringFlg.Where(c => c).Count();
                if (validresponses < 2)
                {
                    errmsg = LocalResource.CSP_ERRMSG_CANNOT_ANALYSE_VALID_CASES_IS_ZERO;
                    return false;
                }


                // _log.Info("Factor analysis Processing starts");
                currentProgress = 40;
                OnWorkerComplete(currentProgress, "");



                dataTble = c.SetValidvalues(dataTble, filterringFlg);

                //write data into csv file.
                //  if (!System.IO.File.Exists(""))
                {
                    //create dir
                    outputfilepath = System.IO.Path.Combine(Path.GetTempPath(), "QC4\\");//+ Cluster_Input_File_Name
                    ApplicationConfig appConfig = new ApplicationConfig();
                    outputfilepath =
            System.IO.Path.Combine(
                appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP));
                    GlobalMethodClass.GuaranteeDirectoryExist(outputfilepath);

                }

                // Take header name from SAMPLEID and varnam
                string filename = outputfilepath + Factor_Input_File_Name;
                string header = string.Empty;
                //  header += "SAMPLEID";


                //   header += "\r\n";
                Encoding enc = Encoding.GetEncoding(FileEncoding);
                //  c.WriteData(filename, false, enc, header);
                string data = string.Empty;
                for (int i = 0; i < dataTble.Rows.Count; i++)
                {
                    data = string.Empty;
                    for (int j = 2; j < dataTble.Columns.Count; j++)
                    {
                        data += (!string.IsNullOrEmpty(data) ? seperator : string.Empty) + Convert.ToString(dataTble.Rows[i][j]);
                    }
                    data += "\r\n";
                    c.WriteData(filename, true, enc, data);
                }

                currentProgress = 60;
                OnWorkerComplete(currentProgress, LocalResource.CSP_PB_GENERATING_OUTPUT);
                RFolderPath = CommonFunction.GetTemporaryDirectory();
                string RscriptExePath = "\"" + RFolderPath + @"R_FullSet\R-3.6.0\bin\Rscript" + "\"";
                if (Environment.Is64BitOperatingSystem)
                {
                    RscriptExePath = "\"" + RFolderPath + @"R_FullSet\R-3.6.0\bin\x64\Rscript" + "\"";
                }
                else
                {
                    RscriptExePath = "\"" + RFolderPath + @"R_FullSet\R-3.6.0\bin\i386\Rscript" + "\"";
                }
                string RScriptPath = "\"" + RFolderPath + @"R_FullSet\R-3.6.0\script\" + Factor_R_File_Name + "\"";
                string OutputDirectory = "\"" + outputfilepath + "\"";//outputfilepath
                string InputFilePath = "\"" + outputfilepath + Factor_Input_File_Name + "\"";
                string option = FactorRotation.ToLower() + " " + FactorValue + " " + FactorExtraction;


                // option = "promax 2 ml";
                rscriptCmd = string.Join(" ", RscriptExePath, RScriptPath, InputFilePath, OutputDirectory, option);
                rscriptCmd = " /c \"" + rscriptCmd + "\"";
                int errorcode = 0;

                errorcode = ExecteProcess.Execute(rscriptCmd, out errmsg);

                if (errorcode == 0)
                {
                    excelOperate = new ExcelOperate();
                    xlApp = excelOperate.Excel;
                    xlApp.Visible = false;
                    xlApp.ScreenUpdating = false;

                    // _log.Info("Factor analysis Outputing graph values");
                    xlApp.EnableEvents = false;
                    //xlApp.DisplayStatusBar = false;
                    xlApp.PrintCommunication = false;
                    xlApp.DisplayAlerts = false;
                    currentProgress = 60;
                    OnWorkerComplete(currentProgress, LocalResource.CSP_PB_GENERATING_OUTPUT);

                    this.generateOutPut(outputfilepath + "\\", xlApp, validresponses, Variables);

                    currentProgress = 70;
                    OnWorkerComplete(currentProgress, LocalResource.CSP_PB_GENERATING_OUTPUT);
                    returnvalue = Output_RawData(currentProgress, filterringFlg);
                    if (returnvalue)
                    {

                        if (SaveSettings.Equals("1"))
                        {

                            SaveToSheet(Factorrawdatavariablename, FactorValue, totalrowcount - validresponses, Variables);

                        }

                        // _log.Info("Factor analysis finished");
                        if (Iscomm)
                        {
                            Excel.Range factorcomm = factorSheet.get_Range(strOutputCell_E_10, strOutputCell_E_10);
                            factorcomm.Value = LocalResource.FACTOR_E10;
                            factorcomm = factorSheet.get_Range(strOutputCell_E_11, strOutputCell_E_11);
                            factorcomm.Value = LocalResource.FACTOR_E11;
                        }
                        currentProgress = 90;
                        OnWorkerComplete(currentProgress, LocalResource.CSP_PB_CREATING_GRAPH);
                        SetGraph(Variables.Count);
                        factorSheet.Select();
                        Excel.Range factorvaluerar = factorSheet.get_Range(strSelectionCell_A_1, strSelectionCell_A_1);
                        factorvaluerar.Select();
                        this.OnWorkerComplete(100, LocalResource.PSM_PB_RESLT_OUT, retainThread: true);

                        xlApp.EnableEvents = true;
                        xlApp.PrintCommunication = true;
                        xlApp.WindowState = XlWindowState.xlMaximized;
                        xlApp.ScreenUpdating = true;
                        xlApp.DisplayAlerts = true;
                        xlApp.Visible = true;

                        try
                        {
                            SetForegroundWindow((IntPtr)xlApp.Hwnd);
                        }
                        catch { }

                        returnvalue = true;
                        if (Iscomm)
                        {
                            MessageDialog.ShowMessageOnWorkBook(string.Format("{0}\n{1}", LocalResource.FACTOR_E10, LocalResource.FACTOR_E11), Enums.MessageType.Warning, workBook, (IntPtr)xlApp.Hwnd);
                        }
                        //delete multivariate sheet
                        Excel.Worksheet ws = ExcelUtil.GetWorkSheetBySheetName(workBook, Constants.SheetType.sh_Data_AN);
                        if (ws != null)
                        {
                            QC4Common.Util.ExcelUtil.DeleteMultivariateSheets(workBook);
                        }
                        try//Redmine Id: 210036
                        {
                            ws = ExcelUtil.GetWorkSheetBySheetName(workBook, "Multivariate01");
                            if (ws != null)
                            {
                                QC4Common.Util.ExcelUtil.DeleteMultivariateSheets(workBook);
                            }
                        }
                        catch { }
                        var array = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.An || a.Value.Variable.Equals("SAMPLEID"))).Select(q => q.Value).ToList();
                        QC4Common.Util.ExcelUtil.GenerateNewDataSheet(workBook, array, "multivariate");

                        try
                        {
                            Directory.Delete(outputfilepath, true);
                        }
                        catch (Exception ex)
                        {

                        }


                    }
                    else
                    {
                        errmsg = LocalResource.FAILED_TO_GENE_EXCEL;
                        _log.Error(errmsg);//Redmine id:207795
                    }

                }
                else
                {
                    errmsg = errmsg.TrimEnd('\r', '\n');//http://redmine.macromill.com/issues/210652#note-4 
                    errmsg = string.Format("{0}\n{1}\n{2}\n{3}", LocalResource.R_RUNTIME_ERROR, errmsg, LocalResource.R_COMMAND, rscriptCmd);
                    _log.Error(errmsg);//Redmine id:207795
                    returnvalue = false;
                }
            }
            catch (Exception ex)
            {
                errmsg = LocalResource.FAILED_TO_GENE_EXCEL; _log.Error(ex.Message); returnvalue = false;
            }
            finally
            {

                if (analysisSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref analysisSheet);
                }
                if (factorSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref factorSheet);
                }
                if (rawdataSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref rawdataSheet);
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
                _log.Info("Factor analysis completed");
                try
                {

                    if (File.Exists(outputfilepath + Factor_Input_File_Name))
                    {
                        File.Delete(outputfilepath + Factor_Input_File_Name);
                    }
                    if (SaveSettings.Equals("1"))
                    {
                        QC4Common.Common.CommonFlag.SetIsMultivariateUpdated(workBook, false);
                    }
                }
                catch { }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            return returnvalue;
        }
        internal void generateOutPut(string tempPath, Application xlApp, int validresponses, List<string> Variables)
        {
            //_log.Info("Factor analysis datasheet starts");
            this.templateDirectoryPath = System.AppContext.BaseDirectory;
            this.xlApp = xlApp;
            Encoding enc = Encoding.GetEncoding(FileEncoding);

            // xlApp.ScreenUpdating = true;
            //xlApp.Visible = true;
            string templatename = Factor_Template;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
            {
                templatename = Factor_Template_JaJp;
            }
            wbs = xlApp.Workbooks;
            baseBook = wbs.Add(OutputUtil.getTemplatePath(this.templateDirectoryPath, templatename, xlApp.PathSeparator));
            analysisSheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, Factor_Analysis);
            rawdataSheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, Factor_Rawdata);
            factorSheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, Factor_GraphSheet);
            analysisSheet.Delete();


            rawdataSheet.Cells.EntireColumn.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            // factorSheet.Range["H38"].ColumnWidth = 6.50;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
            {
                factorSheet.Range["B5"].ColumnWidth = 19;
            }

            //TODO HEadinf need to set from resource file

            string[] OutPut_FactorValue = new string[totalrowcount];
            c.ReadFile(tempPath + Factor_OutPut_result_nfactors_File_Name, enc, seperator, ref OutPut_FactorValue);
            Excel.Range factorvaluerar = factorSheet.get_Range(strOutputCell_F_A, strOutputCell_F_A);
            FactorValue = OutPut_FactorValue[0];
            if (FactorExtractedOrAdopt.Equals("1"))
            {
                factorvaluerar.Value = Convert.ToString(FactorValue);
            }
            else
            {
                factorvaluerar.Value = LocalResource.FACTOR_NO_OF_FACTORS_ADOPT;
            }
            factorvaluerar.WrapText = true;

            Excel.Range FactorRotationrar = factorSheet.get_Range(strOutputCell_F_B, strOutputCell_F_B);

            if (FactorRotation.Equals(FactorRotationVarimax))
            {
                FactorRotationrar.Value = Convert.ToString(LocalResource.FACTOR_ROTATION_METHOD_VARIMAX);
            }
            else if (FactorRotation.Equals(FactorRotationPromax))
            {
                FactorRotationrar.Value = Convert.ToString(string.Format(LocalResource.FACTOR_ROTATION_METHOD_PROMAX, ProMax_m_Cnt));
            }
            else if (FactorRotation.Equals(FactorRotationNone))
            {
                FactorRotationrar.Value = Convert.ToString(LocalResource.FACTOR_ROTATION_METHOD_NONE);
            }
            FactorRotationrar.WrapText = true;
            //TODO extration in sheet
            Excel.Range FactorExtractionHeadingrar = factorSheet.get_Range(strOutputCell_E_B, strOutputCell_E_B);
            FactorExtractionHeadingrar.Value = LocalResource.FACTOR_EXTRACTION_METHOD_HEADING;


            Excel.Range FactorExtractionrar = factorSheet.get_Range(strOutputCell_F_BB, strOutputCell_F_BB);
            if (FactorExtraction.Equals(FactorExtractionml))
            {
                FactorExtractionrar.Value = Convert.ToString(LocalResource.MULTI_FACTOR_MAXIMUM_LIKEHOOD);
            }
            else if (FactorExtraction.Equals(FactorExtractionpa))
            {
                FactorExtractionrar.Value = Convert.ToString(LocalResource.MULTI_FACTOR_PRINCIPLE_COMPONENTS);
            }
            FactorExtractionrar.WrapText = true;


            Excel.Range factorvalidcaserar = factorSheet.get_Range(strOutputCell_F_C, strOutputCell_F_C);
            factorvalidcaserar.Value = Convert.ToString(validresponses);

            Excel.Range factorinvalidcaserar = factorSheet.get_Range(strOutputCell_F_D, strOutputCell_F_D);
            factorinvalidcaserar.Value = Convert.ToString(totalrowcount - validresponses);

            Excel.Range factorreversevaluerar = factorSheet.get_Range(strOutputCell_F_K, strOutputCell_F_K);
            if (FactorAssignScores.Equals("1"))
            {
                factorreversevaluerar.Value = Convert.ToString(LocalResource.FACTOR_REVERSE_VALUE_YES);
            }
            else
            {
                factorreversevaluerar.Value = Convert.ToString(LocalResource.FACTOR_REVERSE_VALUE_NO);
            }


            if (!string.IsNullOrEmpty(csp.EditString) && CriteriaFlagValue.Equals("1"))
            {
                int filterrow = 2;
                int filtercolumn = 9;
                Excel.Range filterstart = factorSheet.Cells[filterrow, filtercolumn];
                Excel.Range filterstop = factorSheet.Cells[filterrow + 1, filtercolumn];


                Excel.Range filtermergestart = factorSheet.Cells[filterrow, filtercolumn];
                Excel.Range filtermergestop = factorSheet.Cells[filterrow, filtercolumn + 2];
                Excel.Range filtermergerar = factorSheet.get_Range(filtermergestart, filtermergestop);
                filtermergerar.Interior.Color = 12611584;
                filtermergerar.Font.Color = 16777215;
                filtermergerar.Font.Bold = true;
                filtermergerar.Borders.Color = 10921638;
                filtermergerar.Borders.Weight = XlBorderWeight.xlThin;
                csp.PRV_MergeConditionalRange(filtermergerar);

                filtermergestart = factorSheet.Cells[filterrow + 1, filtercolumn];
                filtermergestop = factorSheet.Cells[filterrow + 5, filtercolumn + 2];
                filtermergerar = factorSheet.get_Range(filtermergestart, filtermergestop);
                csp.PRV_WrapSelectedRange(filtermergerar);
                filtermergerar.Borders.Color = 10921638;
                filtermergerar.Borders.Weight = XlBorderWeight.xlThin;

                csp.PRV_VerticalAlignmentCenterRange(filtermergerar);
                csp.PRV_MergeConditionalRange(filtermergerar);

                Excel.Range filterrar = factorSheet.get_Range(filterstart, filterstop);
                var filterobj = filterrar.Value;
                filterobj[1, 1] = LocalResource.CSP_FILTERCRITERIA;
                filterobj[2, 1] = csp.EditString;
                filterrar.Value = filterobj;

                Excel.Range filterfontstart = factorSheet.Cells[filterrow + 1, filtercolumn];
                Excel.Range filterfontstop = factorSheet.Cells[filterrow + 1, filtercolumn];
                Excel.Range filterfontrar = factorSheet.get_Range(filterfontstart, filterfontstop);
                filterfontrar.Font.Color = 0;

            }

            int rowcount = 0;


            Excel.Range Noterar = factorSheet.get_Range(Note, Note);
            if (FactorRotation.Equals(FactorRotationVarimax) || FactorRotation.Equals(FactorRotationNone) || int.Parse(FactorValue) < 2)
            {
                Noterar.Delete(XlDeleteShiftDirection.xlShiftUp);
            }


            Excel.Range factorCommunalitycellstart = factorSheet.Cells[Communality_Value_Row, Communality_Value_Column];
            Excel.Range factorCommunalitycellend = factorSheet.Cells[Communality_Value_End_Row, Communality_Value_End_Column];
            Excel.Range factorCommunalityrange = factorSheet.get_Range(factorCommunalitycellstart, factorCommunalitycellend);

            FNC_Cm_RowCol_Insert(factorSheet, factorCommunalityrange.Row + 1, Variables.Count);

            Excel.Range variablequestionstart = factorSheet.Cells[Communality_Value_Row, Communality_Value_Column];
            Excel.Range variablequestionend = factorSheet.Cells[Communality_Value_End_Row + Variables.Count - 1, Communality_Value_Column + 1];
            Excel.Range variablequestionrange = factorSheet.get_Range(variablequestionstart, variablequestionend);
            variablequestionrange.NumberFormat = "@";

            factorCommunalitycellstart = factorSheet.Cells[Communality_Value_Row, Communality_Value_Column];
            factorCommunalitycellend = factorSheet.Cells[Communality_Value_End_Row + Variables.Count - 1, Communality_Value_End_Column];
            factorCommunalityrange = factorSheet.get_Range(factorCommunalitycellstart, factorCommunalitycellend);


            Communality(factorCommunalitycellstart, factorCommunalitycellend, factorCommunalityrange, tempPath, enc, Variables);

            factorCommunalitycellstart = factorSheet.Cells[Communality_Start_Row, Communality_Value_Column];
            factorCommunalitycellend = factorSheet.Cells[Communality_Value_End_Row + Variables.Count - 1, Communality_Value_End_Column];
            factorCommunalityrange = factorSheet.get_Range(factorCommunalitycellstart, factorCommunalitycellend);

            SetHyperlink(factorCommunalityrange, LocalResource.FACTOR_P_StrCr_FirstCommunalities, strOutputCell_F_G);
            Excel.Range fontrange = factorSheet.get_Range(strOutputCell_F_G, strOutputCell_F_G);
            fontrange.Font.Size = 9;

            Excel.Range factorInitialcellstart = factorSheet.Cells[InitialEig_Value_Row, InitialEig_Value_Column];
            Excel.Range factorInitialcellend = factorSheet.Cells[InitialEig_Value_Row + Variables.Count - 1, InitialEig_Value_End_Column];
            Excel.Range factorInitialrange = factorSheet.get_Range(factorInitialcellstart, factorInitialcellend);


            InitialEig(factorInitialcellstart, factorInitialcellend, factorInitialrange, tempPath, enc, Variables);

            factorInitialcellstart = factorSheet.Cells[InitialEig_Start_Row, InitialEig_Value_Column];
            factorInitialcellend = factorSheet.Cells[InitialEig_Value_Row + Variables.Count - 1, InitialEig_Value_Column + 3];
            factorInitialrange = factorSheet.get_Range(factorInitialcellstart, factorInitialcellend);

            SetHyperlink(factorInitialrange, LocalResource.FACTOR_P_StrCr_FirstEigenVals, strOutputCell_F_F);
            fontrange = factorSheet.get_Range(strOutputCell_F_F, strOutputCell_F_F);
            fontrange.Font.Size = 9;


            rowcount += Variables.Count;



            Excel.Range factorLoadingHeadingstart = factorSheet.Cells[FactorLoading_End_Row + rowcount + 1, FactorLoading_Start_Column];
            Excel.Range factorLoadingHeadingend = factorSheet.Cells[FactorLoading_End_Row + rowcount + 1, FactorLoading_End_Column];
            Excel.Range factorLoadinHeadinggrange = factorSheet.get_Range(factorLoadingHeadingstart, factorLoadingHeadingend);

            FNC_Cm_RowCol_Insert(factorSheet, factorLoadinHeadinggrange.Row + 1, Variables.Count);

            variablequestionstart = factorSheet.Cells[FactorLoading_Start_Row + rowcount + 2, Communality_Value_Column];
            variablequestionend = factorSheet.Cells[FactorLoading_Start_Row + rowcount + 2 + Variables.Count - 1, Communality_Value_Column + 1];
            variablequestionrange = factorSheet.get_Range(variablequestionstart, variablequestionend);
            variablequestionrange.NumberFormat = "@";

            Excel.Range factorSquaredcellstart = factorSheet.Cells[Squared_Start_Row + rowcount, Squared_Start_Column];
            Excel.Range factorSquaredcellend = factorSheet.Cells[Squared_End_Row + rowcount, Squared_End_Column];
            Excel.Range factorSquaredrange = factorSheet.get_Range(factorSquaredcellstart, factorSquaredcellend);
            if (FactorRotation.Equals(FactorRotationNone) || !File.Exists(tempPath + Factor_OutPut_result_vaccounted_File_Name))
            {
                factorSquaredrange.Delete(XlDeleteShiftDirection.xlShiftUp);

                factorLoadingHeadingstart = factorSheet.Cells[FactorLoading_Start_Row + rowcount, FactorLoading_Start_Column];
                factorLoadingHeadingend = factorSheet.Cells[FactorLoading_End_Row + rowcount + Variables.Count, FactorLoading_End_Column + int.Parse(FactorValue)];
                factorLoadinHeadinggrange = factorSheet.get_Range(factorLoadingHeadingstart, factorLoadingHeadingend);
                //
                factorInitialcellstart = factorSheet.Cells[InitialEig_Start_Row, InitialEig_Value_Column + 3 + 1];
                factorInitialcellend = factorSheet.Cells[InitialEig_Value_Row + Variables.Count - 1, InitialEig_Value_End_Column];
                factorInitialrange = factorSheet.get_Range(factorInitialcellstart, factorInitialcellend);

                SetHyperlink(factorInitialrange, LocalResource.FACTOR_P_StrCr_Bf_FactorSumSquares, strOutputCell_F_H);
                fontrange = factorSheet.get_Range(strOutputCell_F_H, strOutputCell_F_H);
                fontrange.Font.Size = 9;
            }
            else
            {
                try
                {
                    if (File.Exists(tempPath + Factor_OutPut_result_vaccounted_File_Name))
                    {

                        factorSquaredcellstart = factorSheet.Cells[Squared_Value_Row + rowcount, Squared_Start_Column];
                        factorSquaredcellend = factorSheet.Cells[Squared_Value_End_Row + rowcount + int.Parse(FactorValue) - 1, Squared_End_Column];
                        factorSquaredrange = factorSheet.get_Range(factorSquaredcellstart, factorSquaredcellend);
                        Squared(factorSquaredcellstart, factorSquaredcellend, factorSquaredrange, tempPath, enc);
                        //
                        factorSquaredcellstart = factorSheet.Cells[Squared_Start_Row + rowcount, Squared_Start_Column];
                        factorSquaredcellend = factorSheet.Cells[Squared_End_Row + rowcount + int.Parse(FactorValue), Squared_End_Column];
                        factorSquaredrange = factorSheet.get_Range(factorSquaredcellstart, factorSquaredcellend);
                        SetHyperlink(factorSquaredrange, LocalResource.FACTOR_P_StrCr_Af_FactorSumSquares, strOutputCell_F_H);
                        fontrange = factorSheet.get_Range(strOutputCell_F_H, strOutputCell_F_H);
                        fontrange.Font.Size = 9;
                    }
                }
                catch { }
            }


            string headtext = LocalResource.FACTOR_P_StrCr_Af_FactorLoadings;
            if (FactorRotation.Equals(FactorRotationVarimax))
            {
                headtext = LocalResource.FACTOR_P_StrCr_Af_FactorLoadings;
            }
            else if (FactorRotation.Equals(FactorRotationPromax))
            {
                headtext = LocalResource.FACTOR_P_StrCr_Af_FactorLoadings_Pattern;
            }
            else
            {
                headtext = LocalResource.FACTOR_P_StrCr_Bf_FactorLoadings;
            }
            Excel.Range factorloadingheadingtexts = factorSheet.Cells[FactorLoading_Start_Row + rowcount, FactorLoading_Start_Column];
            Excel.Range factorloadingheadingtexte = factorSheet.Cells[FactorLoading_Start_Row + rowcount, FactorLoading_Start_Column];
            Excel.Range factorloadingheadingtextr = factorSheet.get_Range(factorloadingheadingtexts, factorloadingheadingtexte);
            factorloadingheadingtextr.Value = headtext;
            factorloadingheadingtextr.WrapText = false;

            factorLoadingHeadingstart = factorSheet.Cells[FactorLoading_Start_Row + rowcount, FactorLoading_Start_Column];
            factorLoadingHeadingend = factorSheet.Cells[FactorLoading_End_Row + rowcount + Variables.Count, FactorLoading_End_Column + int.Parse(FactorValue)];
            factorLoadinHeadinggrange = factorSheet.get_Range(factorLoadingHeadingstart, factorLoadingHeadingend);
            SetHyperlink(factorLoadinHeadinggrange, headtext, strOutputCell_F_H11);
            fontrange = factorSheet.get_Range(strOutputCell_F_H11, strOutputCell_F_H11);
            fontrange.Font.Size = 9;

            factorLoadingHeadingstart = factorSheet.Cells[FactorLoading_End_Row + rowcount, FactorLoading_Value_End_Column + 1];
            factorLoadingHeadingend = factorSheet.Cells[FactorLoading_End_Row + rowcount, FactorLoading_Value_End_Column + int.Parse(FactorValue)];
            factorLoadinHeadinggrange = factorSheet.get_Range(factorLoadingHeadingstart, factorLoadingHeadingend);
            if (factorLoadinHeadinggrange.Cells.Count == 1)
            {
                factorLoadinHeadinggrange.Value = LocalResource.FACTOR_ROTATION_HEADING_FACTOR + 1;
            }
            else
            {
                var objheading = factorLoadinHeadinggrange.Value;
                for (int i = 1; i <= int.Parse(FactorValue); i++)
                {
                    objheading[1, i] = LocalResource.FACTOR_ROTATION_HEADING_FACTOR + i;
                }
                factorLoadinHeadinggrange.Value = objheading;
            }

            factorLoadinHeadinggrange.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            factorLoadinHeadinggrange.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlThin;
            factorLoadinHeadinggrange.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            factorLoadinHeadinggrange.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            factorLoadinHeadinggrange.Borders.Color = 10921638;
            factorLoadinHeadinggrange.Borders.Weight = XlBorderWeight.xlThin;
            factorLoadinHeadinggrange.Interior.Color = 15853276;

            string[,] OutPut_Factorresultloading = new string[totalrowcount, int.Parse(FactorValue)];
            c.ReadClusterFile(tempPath + Factor_OutPut_result_loadings_File_Name, enc, seperator, ref OutPut_Factorresultloading);


            string[,] factorloadingarray = new string[Variables.Count, int.Parse(FactorValue) + 3];
            for (int i = 0; i < Variables.Count; i++)
            {
                int col = 0;
                for (int j = 0; j < int.Parse(FactorValue) + 3; j++)
                {
                    switch (j)
                    {
                        case 0:
                            factorloadingarray[i, j] = string.Empty;
                            break;
                        case 1:
                            factorloadingarray[i, j] = Variables[i];
                            break;
                        case 2:
                            factorloadingarray[i, j] = Definiotion.VariableDictionary[Variables[i]].Question;
                            break;
                        default:
                            factorloadingarray[i, j] = OutPut_Factorresultloading[i, col];
                            col++;
                            break;
                    }
                }
            }
            factorloadingarray = SortRow(factorloadingarray, Variables.Count, int.Parse(FactorValue) + 3);



            factorLoadingHeadingstart = factorSheet.Cells[FactorLoading_End_Row + rowcount + 1, FactorLoading_Start_Column];
            factorLoadingHeadingend = factorSheet.Cells[FactorLoading_End_Row + rowcount + Variables.Count, FactorLoading_Value_End_Column + int.Parse(FactorValue)];
            Excel.Range factorLoadingrange = factorSheet.get_Range(factorLoadingHeadingstart, factorLoadingHeadingend);

            var objarrayfactorLoadingrange = factorLoadingrange.Value;
            for (int i = 1; i <= Variables.Count; i++)
            {
                int col = 0;
                for (int j = 1; j <= int.Parse(FactorValue) + 2; j++)
                {
                    switch (j)
                    {
                        case 1:
                            objarrayfactorLoadingrange[i, j] = factorloadingarray[i - 1, j];// Variables[i - 1];
                            break;
                        case 2:
                            objarrayfactorLoadingrange[i, j] = factorloadingarray[i - 1, j];// Definiotion.VariableDictionary[Variables[i - 1]].Question;
                            break;
                        default:
                            objarrayfactorLoadingrange[i, j] = factorloadingarray[i - 1, j];// OutPut_Factorresultloading[i - 1, col];
                            col++;
                            break;
                    }
                }
            }




            factorLoadingrange.Value = objarrayfactorLoadingrange;



            factorLoadingrange.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            factorLoadingrange.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            factorLoadingrange.Borders.Color = 10921638;
            factorLoadingrange.Borders.Weight = XlBorderWeight.xlThin;
            factorLoadingrange.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            factorLoadingrange.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;

            factorLoadingHeadingstart = factorSheet.Cells[FactorLoading_End_Row + rowcount + 1, FactorLoading_End_Column + 1];
            factorLoadingHeadingend = factorSheet.Cells[FactorLoading_End_Row + rowcount + Variables.Count, FactorLoading_Value_End_Column + int.Parse(FactorValue)];
            factorLoadingrange = factorSheet.get_Range(factorLoadingHeadingstart, factorLoadingHeadingend);

            factorLoadingrange.FormatConditions.Add(Type: XlFormatConditionType.xlCellValue, Operator: XlFormatConditionOperator.xlNotBetween, Formula1: "0.4", Formula2: "-0.4");
            factorLoadingrange.FormatConditions[1].Interior.Color = 49407;
            factorLoadingrange.NumberFormat = "0.000";

            Excel.Range factorLoadingblockstart = factorSheet.Cells[FactorLoading_End_Row + rowcount + 1, FactorLoading_Start_Column];
            Excel.Range factorLoadingblockend = factorSheet.Cells[FactorLoading_End_Row + rowcount + Variables.Count, FactorLoading_Value_End_Column + int.Parse(FactorValue)];
            Excel.Range factorLoadingblockrange = factorSheet.get_Range(factorLoadingHeadingstart, factorLoadingHeadingend);
            for (int i = 0; i < startpos.Count; i++)
            {
                int start = startpos[i];
                int end = (i + 1 >= startpos.Count) ? Variables.Count : startpos[i + 1];
                factorLoadingblockstart = factorSheet.Cells[FactorLoading_End_Row + rowcount + start + 1, FactorLoading_Start_Column];
                factorLoadingblockend = factorSheet.Cells[FactorLoading_End_Row + rowcount + end, FactorLoading_Value_End_Column + int.Parse(FactorValue)];
                factorLoadingblockrange = factorSheet.get_Range(factorLoadingblockstart, factorLoadingblockend);

                factorLoadingblockrange.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
                factorLoadingblockrange.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
                factorLoadingblockrange.Borders.Color = 10921638;
                factorLoadingblockrange.Borders.Weight = XlBorderWeight.xlThin;
                factorLoadingblockrange.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
                factorLoadingblockrange.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;

            }


            rowcount += Variables.Count;



            Excel.Range factorCorrelcellstart = factorSheet.Cells[FactorCorrel_Start_Row + rowcount, FactorCorrel_Start_Column];
            Excel.Range factorCorrelcellend = factorSheet.Cells[FactorCorrel_End_Row + rowcount, FactorCorrel_End_Column];
            Excel.Range factorCorrelrange = factorSheet.get_Range(factorCorrelcellstart, factorCorrelcellend);



            if (FactorRotation.Equals(FactorRotationVarimax) || FactorRotation.Equals(FactorRotationNone) || int.Parse(FactorValue) < 2)
            {
                factorCorrelrange.Delete(XlDeleteShiftDirection.xlShiftUp);
            }
            else
            {
                //jajp-enus template so fixed text at Noterar
                factorCorrelcellstart = factorSheet.Cells[FactorCorrel_Start_Row + rowcount, FactorCorrel_Start_Column + 1];
                factorCorrelcellend = factorSheet.Cells[FactorCorrel_End_Row + int.Parse(FactorValue) + rowcount, FactorCorrel_End_Column + int.Parse(FactorValue) - 1];
                factorCorrelrange = factorSheet.get_Range(factorCorrelcellstart, factorCorrelcellend);
                SetHyperlink(factorCorrelrange, LocalResource.FACTOR_P_StrCr_RBtwFactor, strOutputCell_F_H12);
                fontrange = factorSheet.get_Range(strOutputCell_F_H12, strOutputCell_F_H12);
                fontrange.Font.Size = 9;


                Excel.Range factorCorrelhstart = factorSheet.Cells[FactorCorrel_End_Row + rowcount, FactorCorrel_End_Column];
                Excel.Range factorCorrelhend = factorSheet.Cells[FactorCorrel_End_Row + rowcount, FactorCorrel_End_Column + int.Parse(FactorValue) - 1];
                Excel.Range factorCorrelhrange = factorSheet.get_Range(factorCorrelhstart, factorCorrelhend);

                if (factorCorrelhrange.Cells.Count > 1)
                {
                    var objcorrelhead = factorCorrelhrange.Value;

                    for (int i = 1; i <= int.Parse(FactorValue); i++)
                    {
                        objcorrelhead[1, i] = LocalResource.FACTOR_ROTATION_HEADING_FACTOR + i;
                    }
                    factorCorrelhrange.Value = objcorrelhead;
                }
                else
                {
                    factorCorrelhrange.Value = LocalResource.FACTOR_ROTATION_HEADING_FACTOR + "1";
                }

                factorCorrelhrange.Interior.Color = 15853276;
                factorCorrelhrange.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
                factorCorrelhrange.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
                factorCorrelhrange.Borders.Color = 10921638;
                factorCorrelhrange.Borders.Weight = XlBorderWeight.xlThin;
                factorCorrelhrange.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;

                string[,] OutPut_Factorresultphi = new string[totalrowcount, int.Parse(FactorValue)];
                c.ReadClusterFile(tempPath + Factor_OutPut_result_phi_File_Name, enc, seperator, ref OutPut_Factorresultphi);

                factorCorrelcellstart = factorSheet.Cells[FactorCorrel_Value_Row + rowcount + 1, FactorCorrel_Value_Column];
                factorCorrelcellend = factorSheet.Cells[FactorCorrel_End_Row + rowcount + int.Parse(FactorValue), FactorCorrel_Value_Column + int.Parse(FactorValue)];
                factorCorrelrange = factorSheet.get_Range(factorCorrelcellstart, factorCorrelcellend);
                // factorCorrelrange = factorCorrelrange.Resize[];


                var objfactorcorrel = factorCorrelrange.Value;
                for (int i = 1; i <= int.Parse(FactorValue); i++)
                {
                    for (int j = 1; j <= int.Parse(FactorValue) + 1; j++)
                    {
                        switch (j)
                        {
                            case 1:
                                objfactorcorrel[i, j] = LocalResource.FACTOR_ROTATION_HEADING_FACTOR + i;
                                break;
                            default:
                                objfactorcorrel[i, j] = OutPut_Factorresultphi[i - 1, j - 2];
                                break;
                        }
                    }
                }
                factorCorrelrange.Value = objfactorcorrel;
                factorCorrelrange.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
                factorCorrelrange.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
                factorCorrelrange.Borders.Color = 10921638;
                factorCorrelrange.Borders.Weight = XlBorderWeight.xlThin;
                factorCorrelrange.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
                factorCorrelrange.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;

                factorCorrelcellstart = factorSheet.Cells[FactorCorrel_Value_Row + rowcount + 1, FactorCorrel_Value_Column];
                factorCorrelcellend = factorSheet.Cells[FactorCorrel_End_Row + rowcount + int.Parse(FactorValue), FactorCorrel_Value_Column];
                factorCorrelrange = factorSheet.get_Range(factorCorrelcellstart, factorCorrelcellend);
                factorCorrelrange.Interior.Color = 15853276;

                factorCorrelcellstart = factorSheet.Cells[FactorCorrel_Value_Row + rowcount + 1, FactorCorrel_End_Column];
                factorCorrelcellend = factorSheet.Cells[FactorCorrel_End_Row + rowcount + int.Parse(FactorValue), FactorCorrel_End_Column + int.Parse(FactorValue) - 1];
                factorCorrelrange = factorSheet.get_Range(factorCorrelcellstart, factorCorrelcellend);
                factorCorrelrange.NumberFormat = "0.000";
            }
            if (FactorRotation.Equals(FactorRotationVarimax) || FactorRotation.Equals(FactorRotationNone) || int.Parse(FactorValue) < 2)
            {
                Excel.Range correl = factorSheet.get_Range(strOutputCell_F_H12, strOutputCell_F_H12);
                correl.ClearContents();
                correl.EntireRow.Hidden = true;
                //  correl.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
            }
            factorSheet.Cells.EntireColumn.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];

            try
            {
                foreach (Excel.Shape shp in rawdataSheet.Shapes)
                {
                    foreach (Excel.Shape item in shp.GroupItems)
                    {
                        string str = item.TextFrame2.TextRange.Text;
                        item.TextFrame2.TextRange.Text = "";
                        item.TextFrame2.TextRange.Font.NameFarEast = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                        item.TextFrame2.TextRange.Characters.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                        item.TextFrame2.TextRange.Text = str;
                    }
                }
            }
            catch
            { }


        }
        public void SaveToSheet(string variablename, string factorcount, int noofinvalidcount, List<string> Variables)
        {
            var MultivariateSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.MultiVariateSheet);

            Excel.Range factorinstructionstart = MultivariateSheet.Cells[5, 6];

            Excel.Range factorinstructionend = MultivariateSheet.Cells[QC4Common.Common.Constants.STD_DP.DeleteCaseMAXRowCount, 6]; // clusterinstructionstart.End[Excel.XlDirection.xlDown];           
            Excel.Range factorfullrow = MultivariateSheet.get_Range(factorinstructionstart, factorinstructionend);
            var rowdataobj = factorfullrow.Value;
            int rowno = factorinstructionend.Row;
            for (int i = 1; i < factorinstructionend.Row; i++)
            {
                if (string.IsNullOrEmpty(Convert.ToString(rowdataobj[i, 1])))
                {
                    rowno = i;
                    break;
                }
            }
            rowno = rowno + 4;
            insertrowno = rowno;
            Excel.Range factorcellstart = MultivariateSheet.Cells[rowno, 1];
            Excel.Range factorcellend = MultivariateSheet.Cells[rowno, FactorColumnEnd];

            Excel.Range factorinsertrow = MultivariateSheet.get_Range(factorcellstart, factorcellend);
            var rawdataobj = factorinsertrow.Value;

            rawdataobj[1, 2] = LocalResource.CELL_ON; // QC4Common.Common.Constants.DP.on
            rawdataobj[1, 6] = An_Process_Type;
            rawdataobj[1, 7] = FactorRotation.Equals(FactorRotationVarimax) ? "1" : (FactorRotation.Equals(FactorRotationPromax) ? "2" : "3");//rotation  3 as per qc4  or 0 as per qc3
            rawdataobj[1, 8] = MaxNoofIterations;
            rawdataobj[1, 9] = FactorExtractedOrAdopt.Equals("1") ? FactorValue : "0";
            rawdataobj[1, 10] = FactorAssignScores.Equals("1") ? "1" : "0";
            rawdataobj[1, 11] = SaveSettings.Equals("1") ? "1" : "0";
            int j = 0;
            for (int i = 12; i < 12 + Variables.Count; i++)
            {
                rawdataobj[1, i] = Variables[j];
                j++;
            }



            j = 0;
            for (int i = FactorAnalysisVariableColumnStart; i < FactorAnalysisVariableColumnStart + variablenames.Count; i++)
            {
                rawdataobj[1, i] = variablenames[j];
                j++;
            }


            factorinsertrow.Value = rawdataobj;

        }
        private bool Output_RawData(double currentProgress, bool[] filterringFlg)
        {
            bool retvalue = true;
            string filename = outputfilepath + "\\" + Factor_OutPut_resultScores_File_Name;
            Encoding enc = Encoding.GetEncoding(FileEncoding);
            string[,] rawdata = new string[totalrowcount, int.Parse(FactorValue)];
            c.ReadClusterFile(filename, enc, seperator, ref rawdata);
            int columncount = int.Parse(FactorValue) + 1;
            Excel.Range rawdatastart = rawdataSheet.Range["A3"];
            Excel.Range rawdatatempstart = rawdataSheet.Cells[3, columncount];
            Excel.Range rawdataend = rawdatatempstart.End[Excel.XlDirection.xlDown];
            Excel.Range rawdatarange = rawdataSheet.get_Range(rawdatastart, rawdataend);
            var rawdataobj = rawdatarange.Value;
            rawdataobj[1, 1] = "SAMPLEID";

            if (SaveSettings.Equals("1"))
            {
                // _log.Info("Factor analysis variable creation");
                currentProgress += 5;
                OnWorkerComplete(currentProgress, "");

                //create variable name
                Factorrawdatavariablename = "AN1";
                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                Factorrawdatavariablename = qsutil.GetANVariableName(Factorrawdatavariablename, Definiotion.VariableDictionary.Values.ToList(), false, int.Parse(FactorValue), FactorAnalysis_VariableName_Suffix);//TODO********************
                Worksheet qssht = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.QuestionSetting);
                Util.QS.AddNewQuestion addNew = new Util.QS.AddNewQuestion(workbook, string.Empty);

                for (int i = 1; i <= int.Parse(FactorValue); i++)
                {
                    variablenames.Add(Factorrawdatavariablename + FactorAnalysis_VariableName_Suffix + i);
                    retvalue = addNew.Save_AN_FA_N_type(qssht, Factorrawdatavariablename + FactorAnalysis_VariableName_Suffix + i, Constants.AnswerType.N, AN_Variable_TableHeading, LocalResource.FACTOR_QUESTIONTEXT);
                    //Save_AN_SA_type
                    if (!retvalue)
                    {
                        _log.Error("Variable creation failed");
                        return retvalue;
                    }
                }

            }
            else
            {
                for (int i = 1; i <= int.Parse(FactorValue); i++)
                {
                    variablenames.Add(FactorAnalysis_temp_VariableName + FactorAnalysis_VariableName_Suffix + i);
                }
            }
            for (int i = 1; i <= int.Parse(FactorValue); i++)
            {
                rawdataobj[1, i + 1] = variablenames[i - 1];//variable anme tAn1s1 or An1S1 regarding save or not
            }
            int resultrow = 0;
            int reversevalue = -1;
            if (FactorAssignScores.Equals("0"))
            {
                reversevalue = 1;
            }
            for (int i = 0; i < rawdataTble.Rows.Count; i++)
            {
                for (int j = 0; j <= int.Parse(FactorValue); j++)
                {
                    if (j == 0)
                    {
                        rawdataobj[i + 2, j + 1] = rawdataTble.Rows[i][j];
                    }
                    else
                    {
                        rawdataobj[i + 2, j + 1] = filterringFlg[i] == true ? Convert.ToString(double.Parse(rawdata[resultrow, j - 1]) * reversevalue) : "*";
                    }
                }
                if (filterringFlg[i] == true)
                {
                    resultrow++;
                }
            }

            if (SaveSettings.Equals("1"))
            {
                // _log.Info("Factor analysis Multivariate sheet Filling");
                currentProgress += 10;
                OnWorkerComplete(currentProgress, "");
                List<string> colList = new List<string>();
                // string variablecodes = string.Empty;
                string variablequery = string.Empty;
                string newfields = string.Empty;
                for (int i = 1; i <= int.Parse(FactorValue); i++)
                {
                    string variablecode = Definiotion.VariableDictionary[variablenames[i - 1]].Id.ToString();


                    // MultivariateTable.Execute(workbook, "ALTER TABLE multivariate ADD q_" + variablecode + " VARCHAR ");

                    newfields += (",q_" + variablecode + " TEXT ");
                    variablequery += (string.IsNullOrEmpty(variablequery)) ? ("q_" + variablecode + " = '**'") : (",q_" + variablecode + " = '**'");
                    colList.Add("q_" + variablecode);
                    //variablecodes += string.IsNullOrEmpty(variablecodes) ? ("q_" + variablecode) : (",q_" + variablecode);
                }
                //alter table here
                retvalue = MultivariateTable.AlterMultivariateTable(workbook, newfields);
                if (!retvalue)
                {
                    return retvalue;
                }
                if (isdp)
                {
                    MultivariateTable.Execute(workbook, "UPDATE  multivariate SET " + variablequery + " where sample_id not in ( select sample_id from data_after_process)");
                }

                MultivariateTable.SaveDataTable(workbook, null, rawdataobj, colList, filterringFlg.Length, "multivariate");

                // _log.Info("Factor analysis Multivariate sheet Filling compltd");

            }
            // else
            {
                for (int i = 0; i < rawdataTble.Rows.Count; i++)
                {
                    //  for (int j = 1; j <= rawdataTble.Columns.Count; j++)
                    {
                        rawdataobj[i + 2, 1] = rawdataTble.Rows[i][1];
                    }
                }
            }
            rawdatarange.Value = rawdataobj;

            return retvalue;
        }
        public void FNC_Cm_RowCol_Insert(Worksheet Target_Sheet,/* XlRowCol Row_Col,*/ long Target_Place, long Insert_Num, long Min_Count = 0)
        {
            double Copy_RowCol;
            //  FNC_Cm_RowCol_Insert = false;
            Application tapp = Target_Sheet.Application;
            if ((Target_Place < Min_Count) & (Min_Count != 0))
                return;
            {

                // if (Row_Col == xlRows)
                {
                    Target_Sheet.Range[Target_Sheet.Rows[Target_Place], Target_Sheet.Rows[Target_Place + Insert_Num - 1]].Insert(Shift: XlDirection.xlDown);
                    Copy_RowCol = tapp.WorksheetFunction.Max(Target_Sheet.Range["A1"].SpecialCells(XlCellType.xlCellTypeLastCell).Row + 1, Min_Count);
                    Target_Sheet.Rows[Copy_RowCol].Copy();
                    Target_Sheet.Range[Target_Sheet.Rows[Target_Place], Target_Sheet.Rows[Target_Place + Insert_Num - 1]].PasteSpecial(XlPasteType.xlPasteAll);
                }

            }

            tapp.CutCopyMode = (XlCutCopyMode)1;

            if (tapp != null)
            {
                try
                { COMWholeOperate.releaseComObject<Application>(ref tapp); }
                catch { }
            }
            //  FNC_Cm_RowCol_Insert = true;
        }
        private void Squared(Excel.Range factorSquaredcellstart, Excel.Range factorSquaredcellend, Excel.Range factorSquaredrange, string tempPath, Encoding enc)
        {
            string[,] OutPut_Factorresultvaccounted = new string[totalrowcount, int.Parse(FactorValue)];
            c.ReadClusterFile(tempPath + Factor_OutPut_result_vaccounted_File_Name, enc, seperator, ref OutPut_Factorresultvaccounted);


            var objfactorsq = factorSquaredrange.Value;
            for (int i = 1; i <= int.Parse(FactorValue); i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    switch (j)
                    {
                        case 1:
                            objfactorsq[i, j] = LocalResource.FACTOR_ROTATION_HEADING_FACTOR + i;
                            break;
                        default:
                            objfactorsq[i, j] = OutPut_Factorresultvaccounted[j - 2, i - 1];
                            break;
                    }
                }
            }
            factorSquaredrange.Value = objfactorsq;
            factorSquaredrange.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            factorSquaredrange.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            factorSquaredrange.Borders.Color = 10921638;
            factorSquaredrange.Borders.Weight = XlBorderWeight.xlThin;
            factorSquaredrange.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            factorSquaredrange.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;

            factorSquaredcellstart = factorSheet.Cells[factorSquaredcellstart.Row, Squared_Start_Column];
            factorSquaredcellend = factorSheet.Cells[factorSquaredcellend.Row, Squared_Start_Column];
            factorSquaredrange = factorSheet.get_Range(factorSquaredcellstart, factorSquaredcellend);

            factorSquaredrange.Interior.Color = 15853276;

            factorSquaredcellstart = factorSheet.Cells[factorSquaredcellstart.Row, Squared_Start_Column + 1];
            factorSquaredcellend = factorSheet.Cells[factorSquaredcellend.Row, Squared_Start_Column + 1];
            factorSquaredrange = factorSheet.get_Range(factorSquaredcellstart, factorSquaredcellend);
            // factorSquaredrange = factorSquaredrange.Resize[int.Parse(FactorValue)];
            factorSquaredrange.NumberFormat = "0.000";

            factorSquaredcellstart = factorSheet.Cells[factorSquaredcellstart.Row, Squared_Start_Column + 2];
            factorSquaredcellend = factorSheet.Cells[factorSquaredcellend.Row, Squared_Start_Column + 3];
            factorSquaredrange = factorSheet.get_Range(factorSquaredcellstart, factorSquaredcellend);
            //factorSquaredrange = factorSquaredrange.Resize[int.Parse(FactorValue)];
            factorSquaredrange.NumberFormat = "0.0%";


        }
        private void SetHyperlink(Range range, string text, string cell)
        {
            Hyperlink cellHyperlink = factorSheet.Hyperlinks.Add(
               factorSheet.get_Range(cell, cell),
               string.Empty,
            "'" + factorSheet.Name + "'!" + range.Address,
               string.Empty,
               "[" + text + "]");
        }
        private void Communality(Excel.Range factorcommcellstart, Excel.Range factorcommcellend, Excel.Range factorcommrange, string tempPath, Encoding enc, List<string> variables)
        {
            string[] OutPut_Factorresultcommunality = new string[totalrowcount];
            c.ReadFile(tempPath + Factor_OutPut_result_communality_File_Name, enc, seperator, ref OutPut_Factorresultcommunality);

            // factorcommrange = factorcommrange.Resize[variables.Count];
            var objarrayfactorcommrange = factorcommrange.Value;
            for (int i = 1; i <= variables.Count; i++)
            {

                for (int j = 1; j <= 3; j++)
                {
                    switch (j)
                    {
                        case 1:
                            objarrayfactorcommrange[i, j] = variables[i - 1];
                            break;
                        case 2:
                            objarrayfactorcommrange[i, j] = Definiotion.VariableDictionary[variables[i - 1]].Question;
                            break;
                        default:
                            objarrayfactorcommrange[i, j] = OutPut_Factorresultcommunality[i - 1];
                            break;
                    }
                }
            }
            factorcommrange.Value = objarrayfactorcommrange;



            factorcommrange.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            factorcommrange.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            factorcommrange.Borders.Color = 10921638;
            factorcommrange.Borders.Weight = XlBorderWeight.xlThin;
            factorcommrange.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            factorcommrange.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;

            factorcommrange = factorcommrange.Offset[ColumnOffset: 2];
            factorcommrange.NumberFormat = "0.000";

            Excel.Range commstart = factorSheet.Cells[Communality_Value_Row, Communality_Value_End_Column];
            Excel.Range commend = factorSheet.Cells[Communality_Value_End_Row + factorcommrange.Rows.Count, Communality_Value_End_Column];
            Excel.Range commrang = factorSheet.get_Range(commstart, commend);

            FNC_CheckCommunality(commrang);
        }
        private void InitialEig(Excel.Range factoreigcellstart, Excel.Range factoreigcellend, Excel.Range factoreigrange, string tempPath, Encoding enc, List<string> variables)
        {
            string[] OutPut_Factorresultvalues = new string[totalrowcount];
            c.ReadFile(tempPath + Factor_OutPut_result_values_File_Name, enc, seperator, ref OutPut_Factorresultvalues);

            string[] OutPut_FactorEvalues = new string[totalrowcount];
            c.ReadFile(tempPath + Factor_OutPut_result_Evalues_File_Name, enc, seperator, ref OutPut_FactorEvalues);

            //  factoreigrange = factoreigrange.Resize[variables.Count];
            var objarrayfactoreigrange = factoreigrange.Value;
            double sumofeig = 0;
            double sumoftotal = 0;
            sumoftotal = OutPut_Factorresultvalues.Where(x => !string.IsNullOrEmpty(x)).Sum(x => double.Parse(x));
            sumofeig = OutPut_FactorEvalues.Where(x => !string.IsNullOrEmpty(x)).Sum(x => double.Parse(x));
            double eiggrandtotal = 0;
            double totgrandtotal = 0;
            double value = 0;
            for (int i = 1; i <= variables.Count; i++)
            {
                // for (int j = 1; j <= 7; j++)
                {
                    // switch (j)
                    {
                        //  case 1:
                        objarrayfactoreigrange[i, 1] = i;
                        //      break;
                        // case 2:
                        objarrayfactoreigrange[i, 2] = OutPut_FactorEvalues[i - 1];
                        //     break;
                        // case 3:

                        eiggrandtotal += value = double.Parse(Convert.ToString(OutPut_FactorEvalues[i - 1])) / sumofeig;
                        objarrayfactoreigrange[i, 3] = string.IsNullOrEmpty(OutPut_FactorEvalues[i - 1]) ? string.Empty : value.ToString();

                        // break;
                        //  case 4:
                        objarrayfactoreigrange[i, 4] = eiggrandtotal.ToString();
                        //    break;
                        //  case 5:
                        if (i > int.Parse(FactorValue))
                        {
                            objarrayfactoreigrange[i, 5] = string.Empty;
                            objarrayfactoreigrange[i, 6] = string.Empty;
                            objarrayfactoreigrange[i, 7] = string.Empty;
                        }
                        else
                        {
                            objarrayfactoreigrange[i, 5] = OutPut_Factorresultvalues[i - 1];
                            //     break;
                            //  case 6:
                            totgrandtotal += value = double.Parse(Convert.ToString(OutPut_Factorresultvalues[i - 1])) / sumofeig;
                            objarrayfactoreigrange[i, 6] = string.IsNullOrEmpty(OutPut_Factorresultvalues[i - 1]) ? string.Empty : value.ToString();
                            //    break;
                            //  case 7:
                            objarrayfactoreigrange[i, 7] = totgrandtotal.ToString();
                            //    break;
                        }
                    }
                }
            }
            factoreigrange.Value = objarrayfactoreigrange;

            factoreigrange.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            factoreigrange.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            factoreigrange.Borders.Color = 10921638;
            factoreigrange.Borders.Weight = XlBorderWeight.xlThin;
            factoreigrange.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            factoreigrange.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;


            Excel.Range eigvaluestart = factorSheet.Cells[InitialEig_Value_Row, InitialEig_Value_Column + 1];
            Excel.Range eigvalueend = factorSheet.Cells[InitialEig_Value_Row + variables.Count, InitialEig_Value_Column + 1];
            Excel.Range eigvaluerange = factorSheet.get_Range(eigvaluestart, eigvalueend);
            eigvaluerange.NumberFormat = "0.000";

            Excel.Range eigperstart = factorSheet.Cells[InitialEig_Value_Row, InitialEig_Value_Column + 2];
            Excel.Range eigperend = factorSheet.Cells[InitialEig_Value_Row + variables.Count, InitialEig_Value_Column + 3];
            Excel.Range eigperrange = factorSheet.get_Range(eigperstart, eigperend);
            eigperrange.NumberFormat = "0.0%";

            Excel.Range totvaluestart = factorSheet.Cells[InitialEig_Value_Row, InitialEig_Value_Column + 4];
            Excel.Range totvalueend = factorSheet.Cells[InitialEig_Value_Row + variables.Count, InitialEig_Value_Column + 4];
            Excel.Range totvaluerange = factorSheet.get_Range(totvaluestart, totvalueend);
            totvaluerange.NumberFormat = "0.000";

            Excel.Range totperstart = factorSheet.Cells[InitialEig_Value_Row, InitialEig_Value_Column + 5];
            Excel.Range totperend = factorSheet.Cells[InitialEig_Value_Row + variables.Count, InitialEig_Value_Column + 6];
            Excel.Range totperrange = factorSheet.get_Range(totperstart, totperend);
            totperrange.NumberFormat = "0.0%";

        }
        private void SetGraph(int ParaCount)
        {
            ChartObject ChartObj;
            ChartObj = factorSheet.ChartObjects(strObjName);
            ChartObj.Chart.ChartArea.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            ChartObj.Chart.ChartTitle.Font.Size = 14;
            ChartObj.Chart.ChartTitle.Font.Bold = true;
            // Set ChartObj = .ChartObjects(strObjName)
            Range XRange = factorSheet.get_Range(strOutputCell_F_II);
            Range YRange = factorSheet.get_Range(strOutputCell_F_II).Offset[0, 1];
            ChartObj.Chart.SeriesCollection(1).XValues = factorSheet.get_Range(XRange, XRange.Offset[ParaCount - 1, 0]);
            ChartObj.Chart.SeriesCollection(1).Values = factorSheet.get_Range(YRange, YRange.Offset[ParaCount - 1, 0]);
            ChartObj.Chart.Axes(XlAxisType.xlCategory).AxisTitle.Font.Size = 9;
            ChartObj.Chart.Axes(XlAxisType.xlValue).AxisTitle.Font.Size = 9;
            ChartObj.Chart.Axes(XlAxisType.xlCategory).AxisTitle.Font.Bold = true;
            ChartObj.Chart.Axes(XlAxisType.xlValue).AxisTitle.Font.Bold = true;

            // If C_App.Version >= Excel_2010 Then
            ChartObj.Chart.Axes(XlAxisType.xlCategory).AxisTitle.Top = 236;
            ChartObj.Chart.Axes(XlAxisType.xlValue).AxisTitle.Left = 0;
            ChartObj.Chart.PlotArea.Left = 24;
            ChartObj.Chart.PlotArea.Top = 40;
            ChartObj.Chart.PlotArea.Width = 410;
            ChartObj.Chart.PlotArea.Height = 188;

        }
        private string[,] SortRow(string[,] factorloadingarray, int rowcount, int columncount)
        {
            for (int i = 0; i < rowcount; i++)
            {
                int fcol = 0;
                double value = Math.Abs(double.Parse(factorloadingarray[i, 3]));
                for (int j = 3; j < columncount; j++)
                {
                    if (Math.Abs(double.Parse(factorloadingarray[i, j])) >= value)
                    {
                        fcol = j - 2;
                        value = Math.Abs(double.Parse(factorloadingarray[i, j]));
                    }
                }
                factorloadingarray[i, 0] = Convert.ToString(fcol);
            }

            // factorloadingarray.StableSort

            factorloadingarray = quicksort(factorloadingarray, 0, factorloadingarray.GetLength(0) - 1, 0);
            startpos = GetStartindex(factorloadingarray);
            int row = 0;
            // startpos.Add(factorloadingarray.GetLength(0) - 1);
            for (int i = 3; i < factorloadingarray.GetLength(1); i++)
            {
                if (row >= startpos.Count)
                {
                    break;
                }
                int start = startpos[row];
                int end = (row + 1 >= startpos.Count) ? factorloadingarray.GetLength(0) - 1 : startpos[row + 1] - 1;
                factorloadingarray = Quick_Sort_DESC(factorloadingarray, start, end, factorloadingarray.GetLength(0) - 1, i);
                factorloadingarray = SortAscValues(factorloadingarray, start, end, i);
                row++;
            }

            return factorloadingarray;
        }
        private List<int> GetStartindex(string[,] array)
        {
            List<int> startpos = new List<int>();
            int value = 1;
            startpos.Add(0);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (value != int.Parse(array[i, 0]))
                {
                    value = int.Parse(array[i, 0]);
                    startpos.Add(i);
                }
            }
            return startpos;
        }

        private string[,] SortAscValues(string[,] input, int low, int high, int column)
        {

            for (int i = low; i <= high; i++)
            {
                if (double.Parse(input[i, column]) <= 0.4)
                {

                    input = quicksort(input, i, high, column);
                    break;
                }
            }

            return input;
        }

        private string[,] quicksort(string[,] input, int low, int high, int column)
        {
            int pivot_loc = 0;

            if (low < high)
            {
                pivot_loc = partition(input, low, high, column);
                input = quicksort(input, low, pivot_loc - 1, column);
                input = quicksort(input, pivot_loc + 1, high, column);
            }
            return input;
        }
        private int partition(string[,] input, int low, int high, int column)
        {
            double pivot = double.Parse(input[high, column]);
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (double.Parse(input[j, column]) <= pivot)
                {
                    i++;
                    input = swap(input, i, j, column);
                }
            }
            input = swap(input, i + 1, high, column);
            return i + 1;
        }

        private string[,] swap(string[,] ar, int a, int b, int column)
        {
            for (int i = 0; i < ar.GetLength(1); i++)
            {
                string temp = ar[a, i];
                ar[a, i] = ar[b, i];
                ar[b, i] = temp;
            }
            return ar;
        }
        private string[,] Quick_Sort_DESC(string[,] data, int left, int right, int count, int column)
        {
            int i;
            int j;
            double pivot;
            string temp;
            i = left;
            j = right;
            pivot = double.Parse(data[(left + right) / 2, column]);
            do
            {
                while ((double.Parse(data[i, column]) > pivot) && (i < right)) i++;
                count++;
                while ((pivot > double.Parse(data[j, column])) && (j > left)) j--;
                count++;
                ;
                if (i <= j)
                {
                    data = swap(data, i, j, column);
                    i++;
                    j--;
                    count++;
                }
            } while (i <= j);
            if (left < j) Quick_Sort_DESC(data, left, j, count, column);
            if (i < right) Quick_Sort_DESC(data, i, right, count, column);

            return data;
        }

        private bool FNC_CheckCommunality(Range CommunalityArray)
        {
            Excel.Application rapp = CommunalityArray.Application;
            WorksheetFunction wf = rapp.WorksheetFunction;
            if (wf.Max(CommunalityArray) > 1)
            {
                Iscomm = true;
                if (wf != null)
                {
                    try
                    {
                        COMWholeOperate.releaseComObject(ref wf);
                    }
                    catch { }
                }
                return false;
            }
            else
            {
                if (wf != null)
                {
                    try
                    {
                        COMWholeOperate.releaseComObject(ref wf);
                    }
                    catch { }
                }
                return true;
            }

        }

    }
}

