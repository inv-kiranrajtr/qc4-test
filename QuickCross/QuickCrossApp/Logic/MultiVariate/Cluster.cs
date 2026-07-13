using log4net;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.DataProcess;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic.FileIO;
using Qc4Launcher.DB;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Qc4Launcher.Forms.Multivariate_Analysis.CollectionClass;
using Constants = Qc4Launcher.Util.Constants;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelUtil = Qc4Launcher.Util.ExcelUtil;
using DBHelperCommon = QC4Common.DB.DBHelper;
using System.Data.SQLite;
using Macromill.QCWeb.Common;

namespace Qc4Launcher.Logic.MultiVariate
{
    class Cluster
    {
        private const string Cluster_Template = "Analysis.xlsx";//"Analysis.xlsx"//Analysis.xls
        private const string Cluster_Template_JaJp = "Analysis_JaJp.xlsx";//"Analysis.xlsx"//Analysis.xls
        private const string Cluster_Rawdata = "Sheet1";// Rawdata
        private const string Cluster_Analysis = "Sheet2";// //Cluster Analysis
        private const string Factor_GraphSheet = "Sheet3";

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

        private const int ResultSizeRow = 7;
        private const int ResultSizeColum = 6;

        private const int ResultCentersRow = 12;
        private const int ResultCentersColumn = 2;



        private const string Cluster_R_File_Name = "qcr_cluster.R";
        private const string Cluster_Input_File_Name = "inshicluster.txt";
        private const string Cluster_OutPut_ResultIter_File_Name = "result_iter.tsv";
        private const string Cluster_OutPut_ResultSize_File_Name = "result_size.tsv";
        private const string Cluster_OutPut_ResultCenters_File_Name = "result_centers.tsv";
        private const string Cluster_OutPut_ResultCluster_File_Name = "result_cluster.tsv";
        public const string ClusterAnalysis_temp_VariableName = "tAN1";
        public string AN_Variable_TableHeading = string.Empty;
        public const string An_Variable_QuestionText = "ClusterResults";
        public const string An_Process_Type = "Cluster";

        public static string strItemPrefix = "[";
        public static string strItemSuffix = "]";
        string strEdit01 = LocalResource.PSM_FILTER_VAL;// "...value of...";
        string strEdit_LG = LocalResource.PSM_FILTER_LG;//"  excluding";
        string strEdit_E = "";//"";
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

        public string seperator = "\t";
        public string FileEncoding = "UTF-8";

        public const int ClusterRowStart = 11;
        public const int ClusterRowEnd = 16;
        public const int MAX_MULTIVARIAT_COLUMN = 1040;
        public const int ClusterColumnStart = 2;
        public const int ClusterColumnEnd = 235;


        public const int Cluster_CriteriaFlag = 7;
        public const int Cluster_NoOfClusterValue = 8;
        public const int Cluster_SaveSettings = 9;
        public const int Cluster_Variables = 10;

        public const int MaxNoofIterations = 999;

        string CriteriaFlagValue = string.Empty;
        string ClusterValue = string.Empty;
        string SaveSettings = string.Empty;
        string CriteriaQuerystring = string.Empty;
        string OverallEvaluationCriteriaQuerystring = string.Empty;
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
        string clusterrawdatavariablename = string.Empty;
        Workbook workbook = null;
        List<string> variablenames;
        bool isdp = false;
        public bool ExecuteCluster(Workbook workBook, out string errmsg)
        {
            bool returnvalue = false;
            errmsg = string.Empty;
            string rscriptCmd = string.Empty;
            bool IsCriteria = false;
            ExcelOperate excelOperate = null;
            double currentProgress = 1;
            Application xlApp = null;
            workbook = workBook;
            variablenames = new List<string>();
            try
            {
                _log.Info("Cluster analysis started");
                OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
                Worksheet sht = ExcelUtil.GetWorkSheetBySheetName(workbook, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.MultiVariate);
                Excel.Range ClustrStart = sht.Cells[ClusterRowStart, ClusterColumnStart];
                Excel.Range Clusterlast = sht.Cells[ClusterRowEnd, ClusterColumnEnd];
                Excel.Range clstrrar = sht.get_Range(ClustrStart, Clusterlast);
                bool ismultivariatetable = false;
                object[,] values = (object[,])clstrrar.Value2;
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
                    else if (Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]).Equals(ProcessingMethod.CLUSTER_ANALYSIS))
                    {
                        try { CriteriaFlagValue = Convert.ToString(values[i, (Cluster_CriteriaFlag - 1)]); }
                        catch { }

                        try { ClusterValue = Convert.ToString(values[i, (Cluster_NoOfClusterValue - 1)]); }
                        catch { }
                        try { SaveSettings = Convert.ToString(values[i, (Cluster_SaveSettings - 1)]); }
                        catch { }
                        // Variables.Add(Convert.ToString(values[i, (Cluster_Variables - 1)]));

                        int startindex = Cluster_Variables - 1;
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
                            csp.FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, ClusterRowStart + i - 1, (CS_Portfolio.CriteriaVariableColumn), false, false, false, Convert.ToString(values[i, (CS_Portfolio.MultiVariateInstructionColumn - 1)]), sht);
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

                //_log.Info("Cluster analysis Fetching DT");
                using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                {
                    try
                    {// TODO //fetch each variable// string tableNameMv = DBHelperCommon.getTableName(workBook, columnNameChild, out isMv);
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
                        //_log.Warn(ex.Message); 
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
                if(validresponses<=int.Parse(ClusterValue))//#210775
                {
                    errmsg = LocalResource.CLUSTER_ERRMSG_TOO_MANY_CLUSTERS;
                    return false;
                }

                //_log.Info("Cluster analysis Processing starts");
                currentProgress = 40;
                OnWorkerComplete(currentProgress, "");



                dataTble = SetValidvalues(dataTble, filterringFlg);

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
                string filename = outputfilepath + Cluster_Input_File_Name;
                string header = string.Empty;
          
                Encoding enc = Encoding.GetEncoding(FileEncoding);
               // WriteData(filename, false, enc, header);
                string data = string.Empty;
                for (int i = 0; i < dataTble.Rows.Count; i++)
                {
                    data = string.Empty;
                    for (int j = 2; j < dataTble.Columns.Count; j++)
                    {
                        data += (!string.IsNullOrEmpty(data) ? seperator : string.Empty) + Convert.ToString(dataTble.Rows[i][j]);
                    }
                    data += "\r\n";
                    WriteData(filename, true, enc, data);
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
                string RScriptPath = "\"" + RFolderPath + @"R_FullSet\R-3.6.0\script\" + Cluster_R_File_Name + "\"";
                string OutputDirectory = "\"" + outputfilepath + "\"";//outputfilepath
                string InputFilePath = "\"" + outputfilepath + Cluster_Input_File_Name + "\"";
                string option = " " + ClusterValue + " ";

               

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

                    //_log.Info("Cluster analysis Outputing graph values");
                    xlApp.EnableEvents = false;
                    //xlApp.DisplayStatusBar = false;
                    xlApp.PrintCommunication = false;
                    xlApp.DisplayAlerts = false;
                    currentProgress = 60;
                    OnWorkerComplete(currentProgress, LocalResource.CSP_PB_GENERATING_OUTPUT);
                   // outputfilepath= outputfilepath + "\\";
                    this.generateOutPut(System.AppContext.BaseDirectory, xlApp, validresponses, Variables);

                    currentProgress = 70;
                    OnWorkerComplete(currentProgress, LocalResource.CSP_PB_GENERATING_OUTPUT);
                    returnvalue = Output_RawData(currentProgress, filterringFlg);
                    if (returnvalue)
                    {

                        if (SaveSettings.Equals("1"))
                        {

                            SaveToSheet(clusterrawdatavariablename, ClusterValue, totalrowcount - validresponses, Variables);

                        }


                        currentProgress = 90;
                        OnWorkerComplete(currentProgress, "");



                        analysisSheet.Select();
                        Excel.Range clustervaluerar = analysisSheet.get_Range(strSelectionCell_A_1, strSelectionCell_A_1);
                        clustervaluerar.Select();
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
               
                if (factorSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref factorSheet);
                }
                if (rawdataSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref rawdataSheet);
                }
                if (analysisSheet != null)
                {
                    COMWholeOperate.releaseComObject(ref analysisSheet);
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
                _log.Info("Cluster analysis completed");
                if (SaveSettings.Equals("1"))
                {
                    QC4Common.Common.CommonFlag.SetIsMultivariateUpdated(workBook, false);
                }

                try
                {
                    string File_Name = outputfilepath + Cluster_Input_File_Name;
                    if (File.Exists(File_Name))
                    {
                        File.Delete(File_Name);
                    }
                   
                }
                catch { }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            return returnvalue;
        }
        internal void generateOutPut(string templateDirectoryPath, Application xlApp, int validresponses, List<string> Variables)
        {
            // _log.Info("CSP analysis datasheet starts");
            this.templateDirectoryPath = templateDirectoryPath;
            this.xlApp = xlApp;

            string templatename = Cluster_Template;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
            {
                templatename = Cluster_Template_JaJp;
            }
            // xlApp.ScreenUpdating = true;
            //xlApp.Visible = true;
            wbs = xlApp.Workbooks;
            baseBook = wbs.Add(OutputUtil.getTemplatePath(this.templateDirectoryPath, templatename, xlApp.PathSeparator));
            analysisSheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, Cluster_Analysis);
            rawdataSheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, Cluster_Rawdata);
            factorSheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, Factor_GraphSheet);
            factorSheet.Delete();
            rawdataSheet.Cells.EntireColumn.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            analysisSheet.Cells.EntireColumn.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
            analysisSheet.Range["B11"].ColumnWidth = 15;

            Excel.Range clustervaluerar = analysisSheet.get_Range(strOutputCell_C_A, strOutputCell_C_A);
            clustervaluerar.Value = Convert.ToString(ClusterValue);

            Excel.Range MaxNoofIterationsrar = analysisSheet.get_Range(strOutputCell_C_B, strOutputCell_C_B);
            MaxNoofIterationsrar.Value = Convert.ToString(MaxNoofIterations);

            Excel.Range clustervalidcaserar = analysisSheet.get_Range(strOutputCell_C_C, strOutputCell_C_C);
            clustervalidcaserar.Value = Convert.ToString(validresponses);

            Excel.Range clusterinvalidcaserar = analysisSheet.get_Range(strOutputCell_C_D, strOutputCell_C_D);
            clusterinvalidcaserar.Value = Convert.ToString(totalrowcount - validresponses);

            Encoding enc = Encoding.GetEncoding(FileEncoding);
            string filename = outputfilepath + "\\" + Cluster_OutPut_ResultIter_File_Name;
            string[] OutPut_ResultIter = new string[totalrowcount];
            ReadFile(filename, enc, seperator, ref OutPut_ResultIter);
            Excel.Range iterationrar = analysisSheet.get_Range(strOutputCell_C_F, strOutputCell_C_F);
            iterationrar.Value = Convert.ToString(OutPut_ResultIter[0]);

            filename = outputfilepath + "\\" + Cluster_OutPut_ResultSize_File_Name;
            string[] OutPut_ResultSize = new string[totalrowcount];
            ReadFile(filename, enc, seperator, ref OutPut_ResultSize);
            //Excel.Range ResultSizerar = analysisSheet.get_Range(strOutputCell_C_G, strOutputCell_C_G);//strOutputCell_C_G
            Excel.Range ResultSizestart = analysisSheet.Cells[ResultSizeRow, ResultSizeColum];
            Excel.Range ResultSizelast = analysisSheet.Cells[ResultSizeRow + 2, ResultSizeColum + int.Parse(ClusterValue) - 1];
            Excel.Range ResultSizerar = analysisSheet.get_Range(ResultSizestart, ResultSizelast);
            var ResultSizeobj = ResultSizerar.Value;//OutPut_ResultSize[0]

            for (int i = 1; i <= int.Parse(ClusterValue); i++)
            {
                ResultSizeobj[1, i] = LocalResource.CLUSTER_HEADING + i;
                ResultSizeobj[2, i] = OutPut_ResultSize[i - 1];
                ResultSizeobj[3, i] = (double.Parse(Convert.ToString(OutPut_ResultSize[i - 1])) / validresponses) * 100;
            }
            ResultSizerar.Value = ResultSizeobj;
            ResultSizerar.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            ResultSizerar.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlThin;
            ResultSizerar.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            ResultSizerar.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            ResultSizerar.Borders.Color = 10921638;
            ResultSizerar.Borders.Weight = XlBorderWeight.xlThin;

            Excel.Range ResultSizeHstart = analysisSheet.Cells[ResultSizeRow, ResultSizeColum];
            Excel.Range ResultSizeHlast = analysisSheet.Cells[ResultSizeRow, ResultSizeColum + int.Parse(ClusterValue) - 1];
            Excel.Range ResultSizeHrar = analysisSheet.get_Range(ResultSizeHstart, ResultSizeHlast);
            ResultSizeHrar.Interior.Color = 15853276;


            filename = outputfilepath + "\\" + Cluster_OutPut_ResultCenters_File_Name;
            string[,] OutPut_ResultCenters = new string[int.Parse(ClusterValue) + 1, Variables.Count];
            ReadClusterFile(filename, enc, seperator, ref OutPut_ResultCenters);

            //Redmine id :200296 //https://app.gluemodel.com/#/project/task/4295063877
            Excel.Range Numberformatstart = analysisSheet.Cells[ResultCentersRow + 1, ResultCentersColumn];
            Excel.Range Numberformatlast = analysisSheet.Cells[ResultCentersRow + Variables.Count, ResultCentersColumn + 1];
            Excel.Range Numberformatrar = analysisSheet.get_Range(Numberformatstart, Numberformatlast);
            Numberformatrar.NumberFormat = "@";

            Excel.Range ResultCentersstart = analysisSheet.Cells[ResultCentersRow, ResultCentersColumn];
            Excel.Range ResultCenterslast = analysisSheet.Cells[ResultCentersRow + Variables.Count, ResultCentersColumn + int.Parse(ClusterValue) + 3];
            Excel.Range ResultCentersrar = analysisSheet.get_Range(ResultCentersstart, ResultCenterslast);
            var ResultCentersobj = ResultCentersrar.Value;
            int resultcentrrow = 1;
            int resultcentrcolumn = 0;
            int clusterno = 1;
            for (int i = 1; i <= int.Parse(ClusterValue) + 4; i++)
            {

                switch (i)
                {
                    case 1:
                        // ResultCentersobj[1, i] = LocalResource.CLUSTER_HEADING + i;//variable text
                        break;
                    case 2:
                        //  ResultCentersobj[1, i] = LocalResource.CLUSTER_HEADING + i;//question text
                        break;
                    default:
                        if (i > 4)
                        {
                            ResultCentersobj[1, i] = LocalResource.CLUSTER_HEADING + clusterno;
                            clusterno++;
                        }
                        break;
                }
            }
            for (int i = 2; i <= Variables.Count + 1; i++)
            {
                resultcentrrow = 1;
                for (int j = 1; j <= int.Parse(ClusterValue) + 4; j++)
                {
                    switch (j)
                    {
                        case 1:
                            ResultCentersobj[i, j] = Variables[i - 2];
                            break;
                        case 2:
                            ResultCentersobj[i, j] = Definiotion.VariableDictionary[Variables[i - 2]].Question;
                            break;
                        default:
                            if (j > 4)
                            {
                                ResultCentersobj[i, j] = OutPut_ResultCenters[resultcentrrow, resultcentrcolumn];
                                resultcentrrow++;
                            }
                            break;
                    }
                }
                resultcentrcolumn++;
            }
            //ResultCentersrar.EntireColumn.NumberFormat = "@";
            ResultCentersrar.Value = ResultCentersobj;

            Excel.Range Variablesidestart = analysisSheet.Cells[ResultCentersRow, ResultCentersColumn];
            Excel.Range Variablesidelast = analysisSheet.Cells[ResultCentersRow + Variables.Count, ResultCentersColumn];
            Excel.Range Variablerar = analysisSheet.get_Range(Variablesidestart, Variablesidelast);
            Variablerar.Borders.Color = 10921638;
            Variablerar.Borders.Weight = XlBorderWeight.xlThin;

            Excel.Range Clustersidestart = analysisSheet.Cells[ResultCentersRow, ResultSizeColum];
            Excel.Range Clustersidelast = analysisSheet.Cells[ResultCentersRow + Variables.Count, ResultSizeColum + int.Parse(ClusterValue) - 1];
            Excel.Range Clustersiderar = analysisSheet.get_Range(Clustersidestart, Clustersidelast);
            Clustersiderar.Borders.Color = 10921638;
            Clustersiderar.Borders.Weight = XlBorderWeight.xlThin;

           
            ResultCentersrar.Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;
            ResultCentersrar.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
            


            Excel.Range ResultCentersHeadingstart = analysisSheet.Cells[ResultCentersRow, ResultCentersColumn];
            Excel.Range ResultCentersHeadinglast = analysisSheet.Cells[ResultCentersRow, ResultCentersColumn + int.Parse(ClusterValue) + 3];
            Excel.Range ResultCenterHeadingsrar = analysisSheet.get_Range(ResultCentersHeadingstart, ResultCentersHeadinglast);


            Excel.Range ResultCenterhorizontalstart = analysisSheet.Cells[ResultCentersRow + 1, ResultCentersColumn];
            Excel.Range ResultCenterhorizontallast = analysisSheet.Cells[ResultCentersRow + Variables.Count, ResultCentersColumn + int.Parse(ClusterValue) + 3];
            Excel.Range ResultCenterhorizontalsrar = analysisSheet.get_Range(ResultCenterhorizontalstart, ResultCenterhorizontallast);
            ResultCenterhorizontalsrar.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            ResultCenterhorizontalsrar.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;


           

            Excel.Range ResultCentersHstart = analysisSheet.Cells[ResultCentersRow, ResultSizeColum];
            Excel.Range ResultCentersHlast = analysisSheet.Cells[ResultCentersRow, ResultSizeColum + int.Parse(ClusterValue) - 1];
            Excel.Range ResultCentersHrar = analysisSheet.get_Range(ResultCentersHstart, ResultCentersHlast);
            ResultCentersHrar.Interior.Color = 15853276;

            if (!string.IsNullOrEmpty(csp.EditString) && CriteriaFlagValue.Equals("1"))
            {
                int filterrow = ResultCentersRow + Variables.Count + 5;
                Excel.Range filterstart = analysisSheet.Cells[filterrow, ResultCentersColumn];
                Excel.Range filterstop = analysisSheet.Cells[filterrow + 1, ResultCentersColumn];


                Excel.Range filtermergestart = analysisSheet.Cells[filterrow, ResultCentersColumn];
                Excel.Range filtermergestop = analysisSheet.Cells[filterrow, ResultCentersColumn + 2];
                Excel.Range filtermergerar = analysisSheet.get_Range(filtermergestart, filtermergestop);
                filtermergerar.Interior.Color = 12611584;
                filtermergerar.Font.Color = 16777215;
                filtermergerar.Font.Bold = true;
                filtermergerar.Borders.Color = 10921638;
                filtermergerar.Borders.Weight = XlBorderWeight.xlThin;
                csp.PRV_MergeConditionalRange(filtermergerar);

                filtermergestart = analysisSheet.Cells[filterrow + 1, ResultCentersColumn];
                filtermergestop = analysisSheet.Cells[filterrow + 5, ResultCentersColumn + 2];
                filtermergerar = analysisSheet.get_Range(filtermergestart, filtermergestop);
                csp.PRV_WrapSelectedRange(filtermergerar);
                filtermergerar.Borders.Color = 10921638;
                filtermergerar.Borders.Weight = XlBorderWeight.xlThin;

                csp.PRV_VerticalAlignmentCenterRange(filtermergerar);
                csp.PRV_MergeConditionalRange(filtermergerar);

                Excel.Range filterrar = analysisSheet.get_Range(filterstart, filterstop);
                var filterobj = filterrar.Value;
                filterobj[1, 1] = LocalResource.CSP_FILTERCRITERIA;
                filterobj[2, 1] = csp.EditString;
                filterrar.Value = filterobj;

                Excel.Range filterfontstart = analysisSheet.Cells[filterrow + 1, ResultCentersColumn];
                Excel.Range filterfontstop = analysisSheet.Cells[filterrow + 1, ResultCentersColumn];
                Excel.Range filterfontrar = analysisSheet.get_Range(filterfontstart, filterfontstop);
                filterfontrar.Font.Color = 0;

            }
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
            //_log.Info("CSP analysis datasheet compltd");
        }
        private bool Output_RawData(double currentProgress, bool[] filterringFlg)
        {
            bool retvalue = true;
            string filename = outputfilepath + "\\" + Cluster_OutPut_ResultCluster_File_Name;
            Encoding enc = Encoding.GetEncoding(FileEncoding);
            string[] rawdata = new string[totalrowcount];
            ReadFile(filename, enc, seperator, ref rawdata);

            Excel.Range rawdatastart = rawdataSheet.Range["A3"];
            Excel.Range rawdatatempstart = rawdataSheet.Range["B3"];
            Excel.Range rawdataend = rawdatatempstart.End[Excel.XlDirection.xlDown];
            Excel.Range rawdatarange = rawdataSheet.get_Range(rawdatastart, rawdataend);
            var rawdataobj = rawdatarange.Value;
            rawdataobj[1, 1] = "SAMPLEID";

            if (SaveSettings.Equals("1"))
            {
                // _log.Info("Cluster analysis variable creation");
                currentProgress += 5;
                OnWorkerComplete(currentProgress, "");

                //create variable name
                clusterrawdatavariablename = "AN1";
                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                clusterrawdatavariablename = qsutil.GetANVariableName(clusterrawdatavariablename, Definiotion.VariableDictionary.Values.ToList());
                Worksheet qssht = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.QuestionSetting);
                Util.QS.AddNewQuestion addNew = new Util.QS.AddNewQuestion(workbook, string.Empty);
                System.Data.DataTable choicedt = new System.Data.DataTable();
                choicedt.Clear();
                choicedt.Columns.Add("SL");
                choicedt.Columns.Add("Choice");
                variablenames.Add(clusterrawdatavariablename);
                for (int i = 1; i <= int.Parse(ClusterValue); i++)
                {
                    DataRow dr = choicedt.NewRow();
                    dr["SL"] = i;
                    dr["Choice"] = LocalResource.CLUSTER_HEADING + i;
                    choicedt.Rows.Add(dr);
                }
                retvalue = addNew.Save_AN_SA_type(qssht, clusterrawdatavariablename, Constants.AnswerType.SA, AN_Variable_TableHeading, LocalResource.CLUSTER_QUESTIONTEXT, int.Parse(ClusterValue), -1, choicedt);
                //Save_AN_SA_type
                if (!retvalue)
                {
                    _log.Error("Variable creation failed");
                    return retvalue;
                }

                // _log.Info("Cluster analysis variable creation compltd");

            }

            rawdataobj[1, 2] = string.Equals(SaveSettings, "1") ? clusterrawdatavariablename : ClusterAnalysis_temp_VariableName;//variable anme tAn1 or An1 regarding save or not
            int resultrow = 0;
            for (int i = 0; i < rawdataTble.Rows.Count; i++)
            {
                rawdataobj[i + 2, 1] = rawdataTble.Rows[i][0];
                rawdataobj[i + 2, 2] = filterringFlg[i] == true ? rawdata[resultrow] : "*";
                if (filterringFlg[i] == true)
                {
                    resultrow++;
                }
            }

            if (SaveSettings.Equals("1"))
            {
                // _log.Info("Cluster analysis Multivariate sheet Filling");
                currentProgress += 10;
                OnWorkerComplete(currentProgress, "");

                string variablecode = Definiotion.VariableDictionary[clusterrawdatavariablename].Id.ToString();


                // MultivariateTable.Execute(workbook, "ALTER TABLE multivariate ADD q_" + variablecode + " VARCHAR ");
                retvalue = MultivariateTable.AlterMultivariateTable(workbook, ",q_" + variablecode + " TEXT ");
                if (!retvalue)
                {
                    _log.Error("DB variable creation failed");
                    return retvalue;
                }
                if (isdp)
                {
                    MultivariateTable.Execute(workbook, "UPDATE  multivariate SET q_" + variablecode + " ='**'  where sample_id not in ( select sample_id from data_after_process)");

                }
                List<string> colList = new List<string>();
                colList.Add("q_" + variablecode);
                MultivariateTable.SaveDataTable(workbook, null, rawdataobj, colList, filterringFlg.Length, "multivariate");

                // _log.Info("Cluster analysis Multivariate sheet Filling compltd");

            }
            //  else
            {
                for (int i = 0; i < rawdataTble.Rows.Count; i++)
                {
                    rawdataobj[i + 2, 1] = rawdataTble.Rows[i][1];

                }
            }
            rawdatarange.Value = rawdataobj;

            return retvalue;
        }
        public void WriteData(String filename, bool append, Encoding enc, string data)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, append, enc))
            {
                sw.Write(data);
                sw.Close();
            }
        }
        public void ReadFile(String filePath, Encoding encode, String deLimiter, ref string[] valuearray)
        {
            List<String[]> stringData = new List<string[]>();
            char deLimit = Convert.ToChar(deLimiter);
            int i = 0;
            using (TextFieldParser parser = new TextFieldParser(filePath, encode))
            {
                parser.TrimWhiteSpace = false;
                //  parser.Delimiters = new[] { deLimiter };
                try
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = null;
                        try
                        {
                            fields = parser.ReadLine().Split(deLimit);
                        }
                        catch (MalformedLineException ex)
                        {
                            if (parser.ErrorLine.Contains("\""))
                            {
                                int j = 1;
                                string[] line;
                                string fullLine = parser.ErrorLine;
                                if (parser.ErrorLine.Contains("\r\n"))
                                    fullLine = parser.ErrorLine.Replace("\r\n", "\n");
                                line = fullLine.Split('\n');
                                fields = line[0].Split(new string[] { deLimiter }, StringSplitOptions.None);
                                //fields = fields[0].Split(',');
                                while (line.Count() > j)
                                {
                                    valuearray[i] = fields[0].ToString();// stringData.Add(fields);
                                    i++;
                                    fields = line[j].Split(new string[] { deLimiter }, StringSplitOptions.None);
                                    //fields = fields[0].Split(',');
                                    j++;
                                }
                            }
                            else
                            {
                                throw;
                            }
                        }
                        valuearray[i] = fields[0].ToString();// stringData.Add(fields);
                        i++;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void GetMultivariateData(System.Data.SQLite.SQLiteConnection con, string tablename)
        {
            System.Data.DataTable dt = MultivariateTable.GetMultivariateData(con, tablename);

            //  Fill multivariate sheet

        }
        public System.Data.DataTable GetMultivariateData(System.Data.SQLite.SQLiteConnection con, string tablename, string qvariablenames)
        {
            System.Data.DataTable dt = MultivariateTable.GetMultivariateData(con, tablename, qvariablenames);

            //  Fill multivariate sheet
            return dt;
        }
        public void ReadClusterFile(String filePath, Encoding encode, String deLimiter, ref string[,] valuearray)
        {
            List<String[]> stringData = new List<string[]>();
            char deLimit = Convert.ToChar(deLimiter);
            int i = 0;
            int j = 0;
            using (TextFieldParser parser = new TextFieldParser(filePath, encode))
            {
                parser.TrimWhiteSpace = false;
                //  parser.Delimiters = new[] { deLimiter };
                try
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = null;
                        try
                        {
                            fields = parser.ReadLine().Split(deLimit);
                            j = 0;
                        }
                        catch (MalformedLineException ex)
                        {

                        }
                        foreach (string s in fields)
                        {
                            valuearray[i, j] = s;// stringData.Add(fields);
                            j++;
                        }
                        i++;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public System.Data.DataTable SetValidvalues(System.Data.DataTable dt, bool[] filterringFlg)
        {
            System.Data.DataTable newdt = new System.Data.DataTable();
            newdt.Clear();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                newdt.Columns.Add(dt.Columns[i].ColumnName);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (filterringFlg[i] == true)
                {
                    DataRow dr = newdt.NewRow();
                    for (int j = 0; j < dt.Columns.Count; j++)//foreach (DataColumn dc in dt.Columns)//
                    {
                        dr[dt.Columns[j].ColumnName] = dt.Rows[i][j];
                    }
                    newdt.Rows.Add(dr);
                }
            }
            return newdt;

        }

        public void SaveToSheet(string variablename, string clustercount, int noofinvalidcount, List<string> Variables)
        {
            var MultivariateSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.MultiVariateSheet);

            Excel.Range clusterinstructionstart = MultivariateSheet.Cells[5, 6];

            Excel.Range clusterinstructionend = MultivariateSheet.Cells[QC4Common.Common.Constants.STD_DP.DeleteCaseMAXRowCount, 6]; // clusterinstructionstart.End[Excel.XlDirection.xlDown];           
            Excel.Range clusterfullrow = MultivariateSheet.get_Range(clusterinstructionstart, clusterinstructionend);
            var rowdataobj = clusterfullrow.Value;
            int rowno = clusterinstructionend.Row;
            for (int i = 1; i < clusterinstructionend.Row; i++)
            {
                if (string.IsNullOrEmpty(Convert.ToString(rowdataobj[i, 1])))
                {
                    rowno = i;
                    break;
                }
            }
            rowno = rowno + 4;
            Excel.Range clustercellstart = MultivariateSheet.Cells[rowno, 1];
            Excel.Range clustercellend = MultivariateSheet.Cells[rowno, ClusterColumnEnd];

            Excel.Range clusterinsertrow = MultivariateSheet.get_Range(clustercellstart, clustercellend);
            var rawdataobj = clusterinsertrow.Value;

            rawdataobj[1, 2] = LocalResource.CELL_ON; // QC4Common.Common.Constants.DP.on
            rawdataobj[1, 6] = An_Process_Type;//ProcessingMethod.CLUSTER_ANALYSIS;
            rawdataobj[1, 7] = clustercount;
            rawdataobj[1, 8] = MaxNoofIterations;
            rawdataobj[1, 9] = noofinvalidcount;
            rawdataobj[1, 10] = Variables.Count;
            int j = 0;
            for (int i = 11; i < 11 + Variables.Count; i++)
            {
                rawdataobj[1, i] = Variables[j];
                j++;
            }
            Factor f = new Factor();

            rawdataobj[1, Factor.FactorAnalysisVariableColumnStart] = variablenames[0];
            clusterinsertrow.Value = rawdataobj;



        }
    }
}
