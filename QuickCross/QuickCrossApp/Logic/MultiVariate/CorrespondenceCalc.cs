using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using log4net;
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
using Qc4Launcher.Logic.Gross_Tabulation;
using Qc4Launcher.Util;
using static Macromill.QCWeb.Question.Questions;
using static Qc4Launcher.Logic.CrossSettingsReader;
using DBHelperCommon = QC4Common.DB.DBHelper;

namespace Qc4Launcher.Logic.MultiVariate
{
    public class CorrespondenceCalc
    {
        int pItem1_CateCnt;
        int pItem2_CateCnt;
        bool HasCount = false;
        public delegate void OnWorkerMethodCompleteDelegate(double value, string status, bool isForceStop = false, bool retainThread = false, bool close = false, bool disableCancel = false);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static long MAX_PLOT = 10000;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        internal IntPtr childExcelApp = IntPtr.Zero;


        Macromill.QCWeb.ReportRequest.Reportsets.Reportset reportset = null;

        public void updateProgress(double currentProgress, string v, bool close = false)
        {
            OnWorkerComplete(currentProgress, v, close: close);
        }


        internal bool Tabulate(Workbook workBook, object worker, DoWorkEventArgs bgWorkerArg, System.Windows.Window window = null, IntPtr pb = default(IntPtr))
        {


            ExcelOperate excelOperate = null;
            Application xlApp = null;
            double currentProgress = 1;
            bool hasLower = true;
            string errorMsg;
            string result_eig_File_Name = "result_eig.tsv";
            string result_col_coord_File_Name = "result_col_coord.tsv";
            string result_row_coord_File_Name = "result_row_coord.tsv";
            string outputDir = "";
            string outputDir1 = "";
            string tsvPath = "";
            string tsvPath1 = "";
            string tsvPath2 = "";
            bool isMv = false;
            try
            {
                OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);

                Definiotion.VariableDictionary = DictionaryUtil.PopulateQSDictionary(workBook);

                Questions questions = DictUpdate.GetQuestions(workBook);
                //cSPSettings
                CorrespondenceSettings caSettings = MultiVariateSettingsReader.ReadCorrespondenceSettings(workBook, Definiotion.VariableDictionary);

                if (caSettings == null)
                {
                    OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.NO_VALID_SETTINGS_FOUND, Enums.MessageType.ErrorOk, workBook, pb);
                    return false;
                }
               

                string tableName = "answers";
                if (DBHelper.checkAfterProcess(workBook))
                {
                    tableName = "data_after_process";
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
                DataWithMarking[,] result = null;
                TabulationDescriptions tabulationDescriptions = new TabulationDescriptions(Qc4Launcher.Util.CommonFunction.SetDescriptionString());
                bool[] filterringFlag = null;
                Question qstn = null;

               
                currentProgress = 10;
                OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
                if (caSettings.HasFilter)
                {
                    if (!checkVariableFilter(caSettings.Filters, Definiotion.VariableDictionary))
                    {
                        OnWorkerComplete(currentProgress, LocalResource.PSM_PB_VERIFY_DATA);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.GT_INVALID_FILTER_SETTINGS, Enums.MessageType.ErrorOk, workBook, pb);
                        return false;
                    }
                    string filterExp = CriteriaDescProvider.CreateCriteriaDescriptions(caSettings.Filters, questions);
                    new Criteria(filterExp, "", questions, filterringFlag != null ? Operator.opAnd : Operator.opOr).Filtering(ref filterringFlag, DBHelper.GetConnectionString(workBook), tableName);
                    int count = filterringFlag.Count((y => y == true));
                }
                QCWebException ex;
                QCWebException ex2;
                if (caSettings.tabulationType == 2)
                {

                    List<List<Data>> matrixDatas = new List<List<Data>>();
                    var childDescriptions = new List<string>();
                    var childQuestionName = new List<string>();
                    List<QuestionType> childQuestionTypeList = new List<QuestionType>();
                    string targetVaraible = caSettings.gtVars[0];
                    QuestionSettings qstnDet = Definiotion.VariableDictionary[targetVaraible];
                    qstn = (Question)questions[qstnDet.Id];
                    QuestionType qType = OutputUtil.DeepClone(qstn.QuestionType);
                    QuestionType questionType;
                    QuestionType questionTypeOr;
                    if ((qstn.QuestionType & QuestionType.MA) == QuestionType.MA)
                    {
                        questionType = QuestionType.MA | QuestionType.MatrixParent;
                        questionTypeOr = QuestionType.MA | QuestionType.MatrixParent;
                    }
                    else
                    {
                        questionType = QuestionType.SA | QuestionType.MatrixParent;
                        questionTypeOr = QuestionType.SA | QuestionType.MatrixParent;

                    }
                    IQuestion parentQuestion = qstn.ParentQuestion;


                    string[] weightArray = null;
                    bool hasCount = false;
                    hasLower = true;
                    if ((qstn.QuestionType & QuestionType.MA) == QuestionType.MA && qstnDet.Count.Length != 0)
                    {
                        bool lower = false;
                        hasCount = true;
                        HasCount = true;
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
                        if (weightArray.Length == 0) { weightArray = null; }
                    }

                    using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                    {
                        con.Open();
                        for (int i = 0; i <= caSettings.gtVars.Count - 1; i++)
                        {
                            string childVariableName = caSettings.gtVars[i];

                            Question q;
                            QuestionType childQuestionType;
                            string columnNameChild;
                            QuestionSettings childQstnDet = Definiotion.VariableDictionary[childVariableName];
                            q = (Question)questions[childQstnDet.Id];
                            columnNameChild = Util.Constants.DBSettings.ColumnNamePreText + q.ID;

                            childQuestionType = q.QuestionType;
                            //#OutputFormatting #For Sub total implementation

                            System.Data.DataTable childDataTble = null;
                            QCWebException ex1;
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
                                            childDataTble = DBHelper.GetDataTable("Select NULL As " + columnNameChild + " from " + tableName, con);
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
                                    string tableNameMv = DBHelperCommon.getTableName(workBook, columnNameChild, out isMv);
                                    if (!isMv)
                                    {
                                        childDataTble = DBHelper.GetDataTable("Select " + columnNameChild + " from  " + tableNameMv + " order by sort_no ", con);

                                    }
                                    else
                                    {
                                        childDataTble = DBHelper.GetDataTable("Select " + columnNameChild + " from " + tableName + " a join "
                                            + tableNameMv + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                                    }
                                    //childDataTble = DBHelper.GetDataTable("Select " + columnNameChild + " from " + tableName + " order by sort_no", con);
                                }
                                catch (Exception exc)
                                {
                                    if (exc.Message.Contains("no such column")) // If no such column, load null data
                                    {
                                        newItem = true;
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
                        }

                        ex2 = GTTabulation.GetGTArrayMatrix(
                        questionType
                        , CrossTabulationQC.GetCategoryArray(qstn, true)
                        , matrixDatas
                        , childQuestionTypeList.ToArray()
                        , childDescriptions.ToArray()
                        , out result, translation
                        , tabulationDescriptions
                        , filterringFlag
                        , null
                        , weightArray
                        , false
                        , false
                        , "ja"
                        , false
                        , Macromill.QCWeb.ReportRequest.SignificanceTestCode.Off
                        , null
                        , null
                        , qstn.Name
                        , childQuestionName.ToArray()
                        , hasCount
                        , !String.IsNullOrEmpty(qstnDet.AddSubTotal) ? qstnDet.SubTotalCount : 0,
                        isMTS: (qstnDet.AnswerType == ExcelAddIn.Common.Constants.AnswerType.SA), qTypeOr: questionTypeOr,isLower:hasLower
                        );
                    }
#if DEBUG
                    //  logOutput(result);
#endif
                }
                else
                {
                    List<Data> dataList = null;
                    QuestionType targetQtype = 0;
                    int subTotalCount = 0;
                    Question axis1 = null;
                    string targetVaraible = caSettings.crColVar;
                    string axis1Varaible = caSettings.crRowVar;
                    bool hasCount = false;
                    using (System.Data.SQLite.SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workBook)))
                    {
                        con.Open();
                        QuestionSettings qstnDet = Definiotion.VariableDictionary[targetVaraible];
                        qstn = (Question)questions[qstnDet.Id];
                        tabulationDescriptions = new TabulationDescriptions(tabulationDescriptions);
                        tabulationDescriptions.TotalAxisDescription = qstnDet.Question;
                        bool TestFlag = false;
                        hasLower = true;
                        string[] weightArray = null;
                        if (qstnDet.Count.Length != 0)
                        {
                            bool lower = false;
                            hasCount = true;
                            HasCount = true;
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
                            if (weightArray.Length == 0) { weightArray = null; }
                        }

                        System.Data.DataTable dataTble = new System.Data.DataTable();

                        targetQtype = qstn.QuestionType;
                        if (!String.IsNullOrEmpty(qstnDet.AddSubTotal) && qstnDet.SubTotalCount > 0)
                        {
                            dataList = ReadSubTotalData(qstnDet, qstn, workBook, con, tableName);
                            targetQtype = qstn.QuestionType & ~QuestionType.SA;
                            targetQtype = targetQtype | QuestionType.MA;
                            subTotalCount = qstnDet.SubTotalCount;
                        }
                        else
                        {
                            try
                            {
                                string tableNameMv = DBHelperCommon.getTableName(workBook, qstn.ColumnName, out isMv);
                                if (!isMv)
                                {
                                    dataTble = DBHelper.GetDataTable("Select " + qstn.ColumnName + "  from  " + tableNameMv + " order by sort_no ", con);

                                }
                                else
                                {
                                    dataTble = DBHelper.GetDataTable("Select " + qstn.ColumnName + " from " + tableName + " a join "
                                        + tableNameMv + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                                }
                                dataList = ReadTextFile.ReadDataTable(dataTble, qstn.QuestionType, null, out ex);
                            }
                            catch (Exception exce)
                            {
                                if (exce.Message.Contains("no such column")) // If no such column, load null data
                                {
                                    dataTble = DBHelper.GetDataTable("Select NULL As " + qstn.ColumnName + " from " + tableName, con);
                                }
                                else
                                    throw;

                            }
                        }


                        QuestionSettings qstnDet1 = null;

                        qstnDet1 = Definiotion.VariableDictionary[axis1Varaible];
                        axis1 = (Question)questions[qstnDet1.Id];

                        List<List<Data>> axesDatga = new List<List<Data>>();
                        if (qstnDet1 != null && checkQuestion(qstnDet1, workBook) && (dataList == null ? dataList != null : dataList.Count > 0))//qstndet wrong
                        {
                            bool newItem = false;
                            try
                            {
                                string tableNameMv = DBHelperCommon.getTableName(workBook, axis1.ColumnName, out isMv);
                                if (!isMv)
                                {
                                    dataTble = DBHelper.GetDataTable("Select " + axis1.ColumnName + " from  " + tableNameMv + " order by sort_no ", con);

                                }
                                else
                                {
                                    dataTble = DBHelper.GetDataTable("Select " + axis1.ColumnName + " from " + tableName + " a join "
                                        + tableNameMv + " m on a.sort_no = m.sort_no order by a.sort_no ", con);
                                }
                            }
                            catch (Exception exc)
                            {
                                if (exc.Message.Contains("no such column")) 
                                {
                                    newItem = true;
                                    dataTble = DBHelper.GetDataTable("Select NULL As " + axis1.ColumnName + " from " + tableName, con);
                                }
                                else
                                    throw;
                            }
                            try
                            {
                                if (newItem)
                                {
                                    DPCheckList.DPCheckListHelper.setDeleteFlag(dataTble.Rows.Count);
                                }
                            }
                            finally
                            {
                                if (newItem)
                                {
                                    ReadTextFile.DeleteFlag = null;
                                }
                            }
                            //dataTble = DBHelper.GetDataTable("Select " + axis1.ColumnName + " from  " + tableName + " order by sort_no ", con);
                        }
                        else
                        {
                            dataTble = DBHelper.GetDataTable("Select NULL As tmpcol from " + tableName, con);
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
                        axesDatga.Add(dataList2);


                        ex2 = CrossTabulation.GetCrossArray(
                            targetQtype,
                            CrossTabulationQC.GetCategoryArray(qstn, true),
                            dataList,
                            CrossTabulationQC.GetAxisQTypeList(axis1, null),
                            CrossTabulationQC.GetAxisQuestionTitle(axis1, null),
                            CrossTabulationQC.GetAxisCategoryList(axis1, null),
                            axesDatga,
                            out result, translation,
                            tabulationDescriptions,
                            filterringFlag,
                            null,//crossOptions.WBDataList,
                            weightArray,
                            5,//crossOptions.Level1percent,
                            10,//crossOptions.Level2percent,
                            true, //crossOptions.ShowNoAnswerAxis || crossOptions.TabulateFullQuantity1,
                            false,
                            GlobalTabulation.MarkingTotal.Subtotal,
                            false, false, //crossOptions.TabulateFullQuantity1, IVtoNA: crossOptions.TabulateFullQuantity1, 
                            locale: "ja",
                            CutNA: false, //CutNA: !crossOptions.ShowNoAnswerItem, // need to cehck
                            SignificanceTestOn: TestFlag,
                            significanceTestCode: Macromill.QCWeb.ReportRequest.SignificanceTestCode.Off,
                            significanceTestLevel: null,
                            SignificanceTestLogFilePath: null,
                            qName: qstn.Name, axisQName: CrossTabulationQC.GetAxisQuestionName(axis1, null), hasCount: hasCount,
                            subTotalCnt: subTotalCount, qTypeOr: qstn.QuestionType,hasLower:hasLower);
#if DEBUG
                       
#endif

                    }
                }
                //sorting an values

                if (result != null)
                {
                    if (caSettings.tabulationType == 1)
                    {
                        string crRowVar = caSettings.crRowVar;
                        QuestionSettings crR = Definiotion.VariableDictionary[crRowVar];
                        string crColVar = caSettings.crColVar;
                        QuestionSettings crC = Definiotion.VariableDictionary[crColVar];
                        CrRowchoices = crR.Choices.ToArray();
                        CrColchoices = crC.Choices.ToArray();
                       
                    }
                    else
                    {
                        CrRowchoices = new string[caSettings.gtVars.Count];
                        for (int i = 0; i < caSettings.gtVars.Count; i++)
                        {
                            QuestionSettings crR = Definiotion.VariableDictionary[caSettings.gtVars[i]];
                            CrRowchoices[i] = frmutil.EscapeCRLF(crR.Question);
                            if (CrColchoices == null)
                            {
                                CrColchoices = crR.Choices.ToArray();
                            }
                        }
                    }
                }
                if (result != null)
                {
                    if (caSettings.tabulationType == 1)
                    {
                        string axis1data = caSettings.crColVar;
                        SectorInfo sectinfo;
                        bool issort = false;
                        int sortNo = 0;
                        QuestionSettings qstnDet1 = Definiotion.VariableDictionary[axis1data];
                        if (qstnDet1.Sort.Length > 0)
                        {
                            sortNo = Convert.ToInt32(qstnDet1.Sort);


                            Question axis1 = (Question)questions[qstnDet1.Id];
                            axis1.QuestionType = axis1.QuestionType | QuestionType.Sort;
                            issort = true;
                            if (axis1.Sectors != null)
                            {
                                for (int i = 1; i <= axis1.Sectors.Count; i++)
                                {
                                    Sectors.Sector sector = (Sectors.Sector)axis1.Sectors[i];
                                    AddSectorInformation(sector.Weight, i <= sortNo ? false : true);
                                }
                                if (!String.IsNullOrEmpty(qstnDet1.AddSubTotal) && qstnDet1.SubTotalCount > 0)
                                {
                                    foreach (QuestionSettings.SubTotal subTotal in qstnDet1.SubTotals)
                                    {
                                        AddSectorInformation("0", true);
                                    }
                                }


                                if ((axis1.QuestionType & QuestionType.Sort) == QuestionType.Sort)
                                {
                                    List<int> sortSectors = new List<int>();
                                    for (int i = 0; i < sectorinformations.Length; ++i)
                                    {
                                        if (!sectorinformations[i].IsUnsort) sortSectors.Add(i);
                                    }
                                    if (sortSectors != null)
                                    {
                                        int StartIndex = 4;
                                        int EndIndex = result.GetUpperBound(1) - 3;
                                        result.StableSort(StartIndex, EndIndex, 2, sortSectors.ToArray());
                                    }
                                }

                            }
                        }
                    }
                }
                tsvPath = makeCrossInput(result, caSettings, qstn, out outputDir);
                tsvPath1 = makeCrossInput1(result, caSettings, qstn);

                string Cluster_R_File_Name = "qcr_correspondence.R";
                string RFolderPath = CommonFunction.GetTemporaryDirectory();
                string RscriptExePath = string.Empty;
                if (Environment.Is64BitOperatingSystem)
                {
                    RscriptExePath = "\"" + RFolderPath + @"R_FullSet\R-3.6.0\bin\x64\Rscript" + "\"";
                }
                else
                {
                    RscriptExePath = "\"" + RFolderPath + @"R_FullSet\R-3.6.0\bin\i386\Rscript" + "\"";
                }
                string RScriptPath = "\"" + RFolderPath + @"R_FullSet\R-3.6.0\script\" + Cluster_R_File_Name + "\"";
                string OutputDirectory = "\"" + outputDir + "\"";//outputfilepath
                string InputFilePath = "\"" + tsvPath + "\"";
                string option = " " + caSettings.noOfDimension + " ";
                if (!MakeLimit(tsvPath, caSettings))
                {
                    if ((pItem1_CateCnt + pItem2_CateCnt) > 255)
                    {
                        OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
                        MessageDialog.ShowMessageOnWorkBook(LocalResource.PSM_CORRESS_TO_MANY, Enums.MessageType.ErrorOk, workBook, pb);
                        return false;
                    }
                    OnWorkerComplete(currentProgress, LocalResource.PB_READING_SETTINGS);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.MULTI_CORRES_VALIDCASES_FEW, Enums.MessageType.ErrorOk, workBook, pb);
                    return false;
                }

                if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")//Todo
                {
                    //language ="ja"
                }
                else
                {
                    //language ="en"
                }
                string rscriptCmd = string.Join(" ", RscriptExePath, RScriptPath, InputFilePath, OutputDirectory, option);
                rscriptCmd = " /c \"" + rscriptCmd + "\"";
                int errorcode = 0;
                string errmsg;
                errorcode = ExecteProcess.Execute(rscriptCmd, out errmsg);
                currentProgress = 50;
                OnWorkerComplete(currentProgress, LocalResource.PSM_PB_RESLT_OUT);
                if (errorcode == 0)
                {
                    excelOperate = new ExcelOperate();
                    xlApp = excelOperate.Excel;
                    xlApp.Visible = false;
                    xlApp.ScreenUpdating = false;


                    xlApp.EnableEvents = false;
                    xlApp.PrintCommunication = false;
                    xlApp.DisplayAlerts = false;

                    
                    CorrespondeceOutput GenerateOuput = new CorrespondeceOutput();
                    OnWorkerComplete(60, LocalResource.PSM_PB_RESLT_OUT);
                    GenerateOuput.GenerateOutput(workBook, xlApp, HasCount, questions, outputDir, tsvPath, tsvPath1, this,
                        caSettings, CrColchoices, CrRowchoices);
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


                }
                else
                {
                    errmsg = errmsg.TrimEnd('\r', '\n');//http://redmine.macromill.com/issues/210652#note-4 
                    OnWorkerComplete(currentProgress, LocalResource.PSM_PB_CALC_PSM);
                    string errmsg1 = string.Format("{0}\n{1}\n{2}\n{3}", LocalResource.R_RUNTIME_ERROR, errmsg, LocalResource.R_COMMAND, rscriptCmd);
                    _log.Error(errmsg1);//Redmine id:207795
                    MessageDialog.ShowMessageOnWorkBook(errmsg1, Enums.MessageType.ErrorOk, workBook, pb);
                    _log.Error(errmsg1);//Redmine id:207795
                    return false;
                }
            }



            catch (Exception ex)
            {
                string exMsg = LocalResource.FAILED_TO_GENE_EXCEL;
                try
                {
                    _log.Error(ex.Message);
                    _log.Error(ex.StackTrace);
                    _log.Error(ex.Source);
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
                try
                {
                    if (File.Exists(tsvPath))
                    {
                        File.Delete(tsvPath);
                    }
                    if (File.Exists(tsvPath1))
                    {
                        File.Delete(tsvPath1);
                    }
                    string File_Name = System.IO.Path.Combine(outputDir, result_eig_File_Name);
                    if (File.Exists(File_Name))
                    {
                        File.Delete(File_Name);
                    }
                    File_Name = System.IO.Path.Combine(outputDir, result_col_coord_File_Name);
                    if (File.Exists(File_Name))
                    {
                        File.Delete(File_Name);
                    }
                    File_Name = System.IO.Path.Combine(outputDir, result_row_coord_File_Name);
                    if (File.Exists(File_Name))
                    {
                        File.Delete(File_Name);
                    }
                    this.OnWorkerComplete(100, LocalResource.PSM_PB_RESLT_OUT, true);

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
                        xlApp.Visible = true;
                    }

                }
                catch (Exception)
                {

                }

                if (xlApp != null)
                {
                    try
                    { COMWholeOperate.releaseComObject<Application>(ref xlApp); }
                    catch { }
                }
               
            }
            return true;
        }
        public ChoiceOrder[] choiceArray = null;
        public void AddChoiceSector(string choice)
        {
            if (choiceArray == null)
            {
                choiceArray = new ChoiceOrder[1];
            }
            else
            {
                Array.Resize<ChoiceOrder>(ref choiceArray, choiceArray.Length + 1);
            }
        }
        public string EscapeCRLF(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                
                text = text.Replace("\t", QC4Common.Common.Constants.CRLFchar1);

            }
            return text;
        }
        public string UnEscapeCRLF(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace(QC4Common.Common.Constants.CRLFchar1, "\t");
            }
            return text;
        }
        protected SectorInfo[] sectorinformations = null;
        public void AddSectorInformation(string weight, bool unsortflag)
        {
            if (sectorinformations == null)
            {
                sectorinformations = new SectorInfo[1];
            }
            else
            {
                Array.Resize<SectorInfo>(ref sectorinformations, sectorinformations.Length + 1);
            }
            sectorinformations[sectorinformations.GetUpperBound(0)] = new SectorInfo(weight, unsortflag);
        }
        string[] CrRowchoices = null;
        string[] CrColchoices = null;
        private bool MakeLimit(string Path, CorrespondenceSettings caSettings)
        {
            string FileEncoding = "UTF-8";
            Encoding enc = Encoding.GetEncoding(FileEncoding);
            CorrespondeceOutput cst = new CorrespondeceOutput();
            string[,] InputArray;
            if (caSettings.tabulationType == 2)
            {
                InputArray = cst.ReadGTinputFile(Path, enc, "\t", caSettings);
            }
            else
            {
                InputArray = cst.ReadCorresFileResult(Path, enc, "\t", caSettings);
            }
            List<string> RowChoices = new List<string>();
            List<string> ColChoices = new List<string>();
            List<string> RowSum = new List<string>();
            List<string> ColSum = new List<string>();
            if (InputArray.GetLength(0) > 0 && InputArray.GetLength(1) > 0)
            {
                double sum = 0;
                for (int i = 0; i < InputArray.GetLength(1); i++)
                {
                    sum = 0;
                    double[] ar = new double[InputArray.GetLength(1)];
                    for (int j = 0; j < InputArray.GetLength(0); j++)
                    {
                        double isDouble = 0;
                        double.TryParse(InputArray[j, i], out isDouble);
                        sum = sum +isDouble;//column array

                    }
                    if (sum > 0)
                    {
                        // ColChoices.Add(InputArray[0, i]);
                        ColChoices.Add(CrColchoices[i]);
                    }
                    // ColChoices.Add(InputArray[0, i]);
                    ColSum.Add(sum.ToString());
                }


                for (int i = 0; i < InputArray.GetLength(0); i++)
                {
                    sum = 0;
                    double[] ar = new double[InputArray.GetLength(0)];
                    for (int j = 0; j < InputArray.GetLength(1); j++)
                    {
                        double isDouble = 0;
                        double.TryParse(InputArray[i, j], out isDouble);
                        sum = sum + isDouble;//row array

                    }
                    if (sum > 0)
                    {
                        RowChoices.Add(CrRowchoices[i]);
                    }
                    RowSum.Add(sum.ToString());
                }
               
            }
            pItem1_CateCnt = ColChoices.Count();
            pItem2_CateCnt = RowChoices.Count();
            if (ColChoices.Count < 3 || RowChoices.Count < 3)
            {

                return false;
            }
            if ((ColChoices.Count + RowChoices.Count) > 255)
            {
                return false;
            }
            return true;
        }

        private string makeCrossInput(DataWithMarking[,] result, CorrespondenceSettings caSettings, Question qstn, out string outputDir)
        {
            ApplicationConfig appConfig = new ApplicationConfig();
            outputDir =
    System.IO.Path.Combine(
        appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP)
        , "csa", "out");
            GlobalMethodClass.GuaranteeDirectoryExist(outputDir);

            string tsvpath =
    System.IO.Path.Combine(
        appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP)
        , "csa", "in");

            GlobalMethodClass.GuaranteeDirectoryExist(tsvpath);
            tsvpath = System.IO.Path.Combine(tsvpath, "1.tsv");


            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(tsvpath, false, Encoding.UTF8))
            {
                StringBuilder sb = new StringBuilder();
                if (result != null)
                {
                    for (int i = result.GetLowerBound(0); i <= (caSettings.tabulationType == 2 ? result.GetUpperBound(0) : 3 + caSettings.crRowChoiceCnt); i++)
                    {
                        if ((i == 1 || i == 2 || i == 3 || i == 0) && caSettings.tabulationType == 1 || (i == 0 || i == 2 || i == 0 || i == 1) && caSettings.tabulationType == 2) { continue; }
                        for (int j = result.GetLowerBound(1) + 1; j < (caSettings.tabulationType == 2 ? 5 + caSettings.gtChoiceCnt : 4 + caSettings.crColChoiceCnt); j++)
                        {
                            if ((j == 2 || j == 3 || j == 0 || j == 1) && caSettings.tabulationType == 1 || (j == 2 || j == 3 || j == 4 || j == 0 || j == 1) && caSettings.tabulationType == 2) { continue; }

                            if (caSettings.calcType == 2 || i == 0 && caSettings.tabulationType == 1 || i == 1 && caSettings.tabulationType == 2 || j == 1)
                            {
                                sb.Append(double.IsNaN(result[i, j].NumValue) ? ((result[i, j].Value == null) ? result[i, j].Value : frmutil.EscapeCRLF(result[i, j].Value))
                                    : result[i, j].NumValue.ToString()).Append("\t");
                            }
                            else
                            {
                                sb.Append(result[i, j].Percent.ToString()).Append("\t");
                            }
                        }
                        sb.Length = sb.Length - 1; // remove last \t
                        sb.Append("\n");
                    }
                    sb.Length = sb.Length - 1; // remove last \n
                }
                writer.WriteLine(sb);
            }

            return tsvpath;
        }
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        private string makeCrossInput1(DataWithMarking[,] result, CorrespondenceSettings caSettings, Question qstn)
        {
            ApplicationConfig appConfig = new ApplicationConfig();
            string tsvpath1 =
    System.IO.Path.Combine(
        appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP)
        , "csa", "in");
            GlobalMethodClass.GuaranteeDirectoryExist(tsvpath1);
            tsvpath1 = System.IO.Path.Combine(tsvpath1, "2.tsv");

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(tsvpath1, false, Encoding.UTF8))
            {
                StringBuilder sb = new StringBuilder();
                if (result != null)
                {
                    for (int i = result.GetLowerBound(0); i <= (caSettings.tabulationType == 2 ? result.GetUpperBound(0) : result.GetUpperBound(0) /*3 + caSettings.crRowChoiceCnt*/); i++)
                    {

                        for (int j = result.GetLowerBound(1);
                            j < (caSettings.tabulationType == 2 ? result.GetUpperBound(1) + 1 : result.GetUpperBound(1) + 1); j++)
                        {

                            if (caSettings.calcType == 2 && (caSettings.tabulationType == 1 || caSettings.tabulationType == 2))
                            {
                                sb.Append(double.IsNaN(result[i, j].NumValue) ? ((result[i, j].Value == null) ? result[i, j].Value : frmutil.EscapeCRLF(EscapeCRLF(result[i, j].Value)))
                                     : result[i, j].NumValue.ToString()).Append("\t");
                            }
                            else if (caSettings.calcType == 1 && (caSettings.tabulationType == 1))
                            {
                                sb.Append(double.IsNaN(result[i, j].NumValue) ? ((result[i, j].Value == null) ? result[i, j].Value : frmutil.EscapeCRLF(EscapeCRLF(result[i, j].Value)))
                                    : ((j == 3 || j == 0 || i == 0 || i == 1 || j == result.GetUpperBound(1) || j == result.GetUpperBound(1) - 1) ? result[i, j].NumValue.ToString() : result[i, j].Percent.ToString())).Append("\t");
                            }
                            else if (caSettings.calcType == 1 && (caSettings.tabulationType == 2))
                            {
                                sb.Append(double.IsNaN(result[i, j].NumValue) ? ((result[i, j].Value == null) ? result[i, j].Value : frmutil.EscapeCRLF(EscapeCRLF(result[i, j].Value)))
                                    : ((j == 4 || j == 0 || i == 0 || i == 2 || j == result.GetUpperBound(1) || j == result.GetUpperBound(1) - 1) ? result[i, j].NumValue.ToString() : result[i, j].Percent.ToString())).Append("\t");
                            }
                        }
                        sb.Length = sb.Length - 1; // remove last \t
                        sb.Append("\n");







                    }
                    sb.Length = sb.Length - 1; // remove last \n
                }
                writer.WriteLine(sb);
            }
            return tsvpath1;
        }

        private void removeUnknownandInvalid(Workbook workBook, CorrespondenceSettings caSettings, Questions questions, String tableName, ref bool[] filterringFlag)
        {
            List<FilterSettingsCr> FiltersDkAndStar = new List<FilterSettingsCr>();

            FilterSettingsCr fs11 = new FilterSettingsCr();
            fs11.variable = caSettings.crRowVar;
            fs11.operatorType = "<>";
            fs11.values = "*";
            fs11.conditionType = AND;
            FiltersDkAndStar.Add(fs11);

            FilterSettingsCr fs12 = new FilterSettingsCr();
            fs12.variable = caSettings.crRowVar;
            fs12.operatorType = "<>";
            fs12.values = "DK";
            fs12.conditionType = AND;
            FiltersDkAndStar.Add(fs12);

            FilterSettingsCr fs21 = new FilterSettingsCr();
            fs21.variable = caSettings.crColVar;
            fs21.operatorType = "<>";
            fs21.values = "*";
            fs21.conditionType = AND;
            FiltersDkAndStar.Add(fs21);

            FilterSettingsCr fs22 = new FilterSettingsCr();
            fs22.variable = caSettings.crColVar;
            fs22.operatorType = "<>";
            fs22.values = "DK";
            fs22.conditionType = AND;
            FiltersDkAndStar.Add(fs22);

            string filterExp = CriteriaDescProvider.CreateCriteriaDescriptions(FiltersDkAndStar, questions);
            filterringFlag = new Criteria(filterExp, "", questions).Filtering(DBHelper.GetConnectionString(workBook), tableName: tableName);
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
                    processedfile = dirPath + "\\" + newQuestion.ItemId + ".dp";
                    QCWebException ex;
                    List<Data> data = ReadTextFile.ReadData(processedfile, questionType, null, out ex);
                    return data;
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


    }
    public class ChoiceOrder
    {
        private string choice = null;
        public ChoiceOrder(string choice = null)
        {
            this.choice = choice;
        }
        public string Choice
        {
            get
            {
                return choice;
            }
        }
    }
    public class SectorInfo
    {
        private string weight = null;
        private bool unsortflag = false;
        public SectorInfo(string weight, bool unsortflag)
        {
            this.weight = weight;
            this.unsortflag = unsortflag;
        }
        public string Weight
        {
            get
            {
                return weight;
            }
        }
        public bool IsUnsort
        {
            get
            {
                return unsortflag;
            }
        }
    }

}
