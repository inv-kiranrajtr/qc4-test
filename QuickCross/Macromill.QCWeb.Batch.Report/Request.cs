#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Request.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/2/23
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/8
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Macromill.QCWeb.Dao.ExDao.PmBean;
using Macromill.QCWeb.Dao.ExEntity.Customize;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Common;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Diagnostics;
using Macromill.QCWeb.Exceptions;
using System.IO;
using System.Collections.Generic;
#if FOR_UNIT_TEST
using System.Data;
using System.Data.OleDb;
#endif

namespace Macromill.QCWeb.Batch.Report
{
    /// <summary>
    /// 出力リクエストクラス
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual), Guid("A3FDC6B6-E958-41bc-BCD5-6CCE518B2F74")]
    public class Request : IRequest
    {
        #region プロパティ
        private decimal id = 0; // ID
        private decimal qcwebid = 0;    // 調査ID
        private string reqsvcd = null;    // リクエストサーバコード
        private string dlpath = null; // ダウンロードパス
        private string title = null;    // 調査タイトル
        private Excel.XlFileFormat xlfmt = Excel.XlFileFormat.xlOpenXMLWorkbook; // Excelブック形式
        private ZeroNAIVShowCode zeroshowcd = (ZeroNAIVShowCode)0;  // 0件表示コード
        private NumericContentsCode showncd = NumericContentsCode.All;  // 数値回答表示項目コード
        private bool mergeaxis = true;  // 集計軸セル結合フラグ
        private int sumnumdigits = 2;   // 合計小数点以下桁数
        private int avgnumdigits = 2;   // 平均小数点以下桁数
        private int stdevnumdigits = 2;   // 標準偏差小数点以下桁数
        private int minnumdigits = 2;   // 最小値小数点以下桁数
        private int maxnumdigits = 2;   // 最大値小数点以下桁数
        private int mednumdigits = 2;   // 中央値小数点以下桁数
        private int wtnumdigits = 1;   // ウエイト値小数点以下桁数
        private int wtavgnumdigits = 1;   // 加重平均小数点以下桁数
        private string lccd = "JA"; // ロケーションコード

        private bool retry_flg = false;   // リトライフラグ

        private Reportsets reportsets = null;   // レポートセットコレクションへの参照

        string pptTemplateCheckFilePath = null; // PPTテンプレートチェックファイルパス
        #endregion

        /// <summary>割付セル情報TBLアクセス</summary>
        protected internal TAllocationCellInfoBhv tAllocationCellInfoBhv = null;

        /// <summary>出力物レポートセットTBLアクセス</summary>
        protected TOutputReportsetInfoBhv tOutputReportsetInfoBhv = null;
        /// <summary>リクエストTBLアクセス</summary>
        protected internal TOutputRequestBhv tOutputRequestBhv = null;
        /// <summary>出力物共通TBLアクセス</summary>
        protected internal TOutputCommonBhv tOutputCommonBhv = null;
        /// <summary>出力物サブ-GTTBLアクセス</summary>
        protected internal TOutputSubGtBhv tOutputSubGTBhv = null;
        /// <summary>出力物サブ-クロスTBLアクセス</summary>
        protected internal TOutputSubCrossBhv tOutputSubCrossBhv = null;
        /// <summary>出力サブ-FATBLアクセス</summary>
        protected internal TOutputSubFaBhv tOutputSubFaBhv = null;
        /// <summary>出力物サブ-チェックリストTBLアクセス</summary>
        protected internal TOutputSubCklistBhv tOutputSubCklistBhv = null;
        /// <summary>アプリ環境設定情報</summary>
        protected internal ApplicationConfig appConfig = null;
        /// <summary>QCWeb調査管理詳細</summary>
        protected internal TQcwebSurveyDetailBhv tQcwebSurveyDetailBhv = null;

        /// <summary>
        /// Requestクラスの作成
        /// </summary>
        /// <param name="id">リクエストTBLのID</param>
        [ComVisible(false)]
        public void MakeRequest(decimal id)
        {
#if FOR_UNIT_TEST
            // 単体テストではなんちゃってDB(Excelブック)を使う
            const string DBBOOK_NAME = "ForUTDB.xls";

            // テーブル名の定数定義
            const string REQUEST_TABLE_NAME = "T_Output_Request";
            const string REPORTSET_TABLE_NAME = "T_Output_Reportset_Info";
            const string PP_TEMPLATE_MASTER_TABLE_NAME = "T_Output_Template_Master";
            const string PP_TEMPLATE_TABLE_NAME = "T_Output_Template";
            const string OUTPUT_TABLE_NAME = "T_Output_Common";
            const string OUTPUT_GT_SUB_TABLE_NAME = "T_Output_Sub_GT";
            const string OUTPUT_CROSS_SUB_TABLE_NAME = "T_Output_Sub_Cross";
            const string OUTPUT_FALIST_SUB_TABLE_NAME = "T_Output_Sub_FA";
            const string OUTPUT_CHECKLIST_SUB_TABLE_NAME = "T_Output_Sub_CKList";

            // カラム名の定数定義
            const string REQUEST_TABLE_ID_COLUMN_NAME = "Output_Request_ID";
            const string REQUEST_SERVER_CODE_COLUMN_NAME = "Request_Server_Code";
            const string REQUEST_USER_ID_COLUMN_NAME = "Request_User_ID";
            const string QCWEB_ID_COLUMN_NAME = "QCWebID";
            const string LAST_DOWNLOAD_USER_ID_COLUMN_NAME = "Last_Download_UserID";
            const string REQUEST_DATETIME_COLUMN_NAME = "Request_DateTime";
            const string DOWNLOAD_PATH_COLUMN_NAME = "Download_Path";
            const string REPORT_SERVER_CODE_COLUMN_NAME = "Proc_Server_Code";
            const string STATUS_CODE_COLUMN_NAME = "Status_Code";
            const string END_DATETIME_COLUMN_NAME = "End_DateTime";
            const string LAST_DOWNLOAD_DATETIME_COLUMN_NAME = "Last_Download_DateTime";
            const string SERVEY_TITLE_COLUMN_NAME = "View_Survey_Name";
            const string XL_FILE_FORMAT_COLUMN_NAME = "Excelbook_Type";
            const string NA_SHOW_CODE_COLUMN_NAME = "NoAnswer_Visible_Code";
            const string IV_SHOW_CODE_COLUMN_NAME = "Unmacth_Visible_Code";
            const string ZERO_NA_IV_SHOW_CODE_COLUMN_NAME = "Show_Zero_NA_IV_Code";
            const string N_SHOW_ITEM_CODE_COLUMN_NAME = "Numeric_Answer_View_Code";
            const string WB_SETTING_CODE_COLUMN_NAME = "WB_Setting_Code";
            const string MERGE_AXES_CELLS_FLAG_COLUMN_NAME = "Merge_Axis_Cells_Flag";
            const string SUMMARY_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME = "DP_Total";
            const string AVERAGE_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME = "DP_Average";
            const string STDEV_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME = "DP_Standard_Div";
            const string MIN_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME = "DP_Min";
            const string MAX_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME = "DP_Max";
            const string MEDIAN_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME = "DP_Median";
            const string WT_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME = "DP_Weight";
            const string WT_AVERAGE_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME = "DP_WeightAvr";
            const string WEIGHT_COLUMN_NAME = "Proc_Weight";
            const string REPORTSET_TABLE_ID_COLUMN_NAME = "Output_Reportset_Info_ID";
            const string OUTPUT_FILE_TYPE_CODE_COLUMN_NAME = "Output_File_Type_Code";
            const string PP_FILE_FORMAT_COLUMN_NAME = "PowerPoint_Type";
            const string PP_TEMPLATE_TABLE_ID_COLUMN_NAME = "T_Output_Template_ID";
            const string PP_FILE_NAME_PREFIX_COLUMN_NAME = "Report_Filen_Name_prefix";
            const string COMMENT_OUTPUT_FLAG_COLUMN_NAME = "Comment_Output_Flag";
            const string SURVEY_PURPOSE_COLUMN_NAME = "Survey_Purpose";
            const string SURVEY_METHOD_COLUMN_NAME = "Survey_Method";
            const string SURVEY_TERM_COLUMN_NAME = "Survey_Date";
            const string CELL_SAMPLE_COUNT_COLUMN_NAME = "Cell_Name_Valid_Sample_Count";
            const string SURVEY_ORGANIZATION_COLUMN_NAME = "Survey_Organization";
            const string PP_TEMPLATE_MASTER_TABLE_ID_COLUMN_NAME = "Output_Template_Master_ID";
            const string PP_TEMPLATE_PATH_COLUMN_NAME = "Path";
            const string PP_TEMPLATE_HASH_COLUMN_NAME = "MD5_HASH";
            const string PP_TEMPLATE_UPLOAD_PATH_COLUMN_NAME = "Upload_Path";
            const string PP_TEMPLATE_ALIAS_COLUMN_NAME = "Alias";
            const string REGIST_DATETIME_COLUMN_NAME = "Create_DateTime";
            const string OUTPUT_TABLE_ID_COLUMN_NAME = "Output_Common_ID";
            const string OUTPUT_ORDER_COLUMN_NAME = "Order_Count";
            const string TSV_PATH_COLUMN_NAME = "TSV_File_Path";
            const string XL_FILE_NAME_PREFIX_COLUMN_NAME = "Excelbook_Name_Prefix";
            const string PROC_START_DATETIME_COLUMN_NAME = "Porcess_Start_DateTime";
            const string PROC_FORECAST_END_DATETIME_COLUMN_NAME = "Process_Forecast_End_DateTime";
            const string PROC_END_DATETIME_COLUMN_NAME = "Process_End_DateTime";
            const string STATUS_DESCRIPTION_COLUMN_NAME = "Description";
            const string OUTPUT_TYPE_COLUMN_NAME = "Output_Type";
            const string OUTPUT_GT_SUB_TABLE_ID_COLUMN_NAME = "Output_Sub_GT_ID";
            const string OUTPUT_TABLE_TYPE_COLUMN_NAME = "Output_Table_Type";
            const string TABLE_ORIENTATION_COLUMN_NAME = "Output_Table_Orientation";
            const string PAGESETUP_TABLE_TYPE_COLUMN_NAME = "Page_Setting_Table_Type";
            const string PAPER_SIZE_COLUMN_NAME = "Page_Setting_Paper_Size";
            const string PAPER_ORIENTATION_COLUMN_NAME = "Page_Setting_Paper_Orientation";
            const string MIN_BASE_COLUMN_NAME = "Marking_Min_Parameter";
            const string MARKING_CODE_COLUMN_NAME = "Marking_Code";
            const string SIGNIFICANCE_TEST_MARKING_LEVEL_COLUMN_NAME = "Marking_Level";
            const string OUTPUT_CROSS_SUB_TABLE_ID_COLUMN_NAME = "Output_Sub_Cross_ID";
            const string OUTPUT_METHOD_COLUMN_NAME = "Output_Type";
            const string LEVEL2_PLUS_COLOR_INDEX_COLUMN_NAME = "Level2PlusColor";
            const string LEVEL1_PLUS_COLOR_INDEX_COLUMN_NAME = "Level1PlusColor";
            const string LEVEL1_MINUS_COLOR_INDEX_COLUMN_NAME = "Level1MinusColor";
            const string LEVEL2_MINUS_COLOR_INDEX_COLUMN_NAME = "Level2MinusColor";
            const string LEVEL2_CUSTOM_PERCENT_COLUMN_NAME = "Level2Percent";
            const string LEVEL1_CUSTOM_PERCENT_COLUMN_NAME = "Level1Percent";
            const string OUTPUT_FALIST_SUB_TABLE_ID_COLUMN_NAME = "Output_Sub_FA_ID";
            const string OUTPUT_CHECKLIST_SUB_TABLE_ID_COLUMN_NAME = "Output_Sub_CKList_ID";
            const string TOTAL_COUNT_COLUMN_NAME = "Total_Count";

            System.Reflection.Assembly asbl = System.Reflection.Assembly.GetExecutingAssembly();
            string dbPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(asbl.Location), DBBOOK_NAME);
            if (!System.IO.File.Exists(dbPath))
            {
                throw new System.IO.FileNotFoundException("DBブックが見つかりません。", dbPath);
            }
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";Extended Properties = \"Excel 8.0;HDR=YES;\"";
            using (OleDbConnection conn = new OleDbConnection(connstr))
            {
                string sql = string.Format(
                        "SELECT {0}.{6}, {0}.{7}, {0}.{8}, {0}.{9}, {0}.{10}, {0}.{11}, {0}.{12}"
                           + ", {0}.{13}, {0}.{14}, {0}.{15}, {0}.{16}, {0}.{17}, {0}.{18}, {0}.{19}"
                           + ", {0}.{20}, {0}.{21}, {0}.{22}, {0}.{23}, {0}.{24}, {0}.{25}"
                           + ", {1}.{26}, {1}.{27}, {1}.{28}, {1}.{29}, {1}.{30}"
                           + ", {1}.{31}, {1}.{32}, {1}.{33}, {1}.{34}, {1}.{35}"
                           + ", {2}.{37}, {3}.{39}"
                           + ", {4}.{40}, {4}.{41}, {4}.{42}, {4}.{43} "
                      + "FROM ((({0} INNER JOIN {1} ON {0}.{26} = {1}.{26}) "
                            + "INNER JOIN {4} ON {4}.{5} = {0}.{5}) "
                            + "LEFT JOIN {3} ON {1}.{38} = {3}.{38}) "
                            + "LEFT JOIN {2} ON {3}.{36} = {2}.{36} "
                      + "WHERE {0}.{5} = " + id.ToString() + " "
                      + "ORDER BY {4}.{44}"
                      + ";"
                        , "[" + REQUEST_TABLE_NAME + "$]"
                        , "[" + REPORTSET_TABLE_NAME + "$]"
                        , "[" + PP_TEMPLATE_MASTER_TABLE_NAME + "$]"
                        , "[" + PP_TEMPLATE_TABLE_NAME + "$]"
                        , "[" + OUTPUT_TABLE_NAME + "$]"
                        , REQUEST_TABLE_ID_COLUMN_NAME, QCWEB_ID_COLUMN_NAME, REQUEST_SERVER_CODE_COLUMN_NAME
                        , DOWNLOAD_PATH_COLUMN_NAME, SERVEY_TITLE_COLUMN_NAME, XL_FILE_FORMAT_COLUMN_NAME
                        , NA_SHOW_CODE_COLUMN_NAME, IV_SHOW_CODE_COLUMN_NAME, ZERO_NA_IV_SHOW_CODE_COLUMN_NAME
                        , N_SHOW_ITEM_CODE_COLUMN_NAME, WB_SETTING_CODE_COLUMN_NAME, MERGE_AXES_CELLS_FLAG_COLUMN_NAME
                        , SUMMARY_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME, AVERAGE_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME
                        , STDEV_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME, MIN_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME
                        , MAX_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME, MEDIAN_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME
                        , WT_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME, WT_AVERAGE_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME
                        , REPORT_SERVER_CODE_COLUMN_NAME
                        , REPORTSET_TABLE_ID_COLUMN_NAME, OUTPUT_FILE_TYPE_CODE_COLUMN_NAME, PP_FILE_NAME_PREFIX_COLUMN_NAME
                        , PP_FILE_FORMAT_COLUMN_NAME, COMMENT_OUTPUT_FLAG_COLUMN_NAME, SURVEY_PURPOSE_COLUMN_NAME
                        , SURVEY_METHOD_COLUMN_NAME, SURVEY_TERM_COLUMN_NAME, CELL_SAMPLE_COUNT_COLUMN_NAME
                        , SURVEY_ORGANIZATION_COLUMN_NAME
                        , PP_TEMPLATE_MASTER_TABLE_ID_COLUMN_NAME, PP_TEMPLATE_PATH_COLUMN_NAME
                        , PP_TEMPLATE_TABLE_ID_COLUMN_NAME, PP_TEMPLATE_UPLOAD_PATH_COLUMN_NAME
                        , OUTPUT_TABLE_ID_COLUMN_NAME, TSV_PATH_COLUMN_NAME, XL_FILE_NAME_PREFIX_COLUMN_NAME
                        , OUTPUT_TYPE_COLUMN_NAME, OUTPUT_ORDER_COLUMN_NAME
                        );
                Console.WriteLine(sql);
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    throw;
                }
                try
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn))
                    {
                        using (DataTable table = new DataTable())
                        {
                            adapter.Fill(table);
                            if (table.Rows.Count == 0)
                            {
                                // エラースロー
                                throw new Exception("Data Not Found.");
                            }
                            DataRow row = table.Rows[0];
                            if (!decimal.TryParse(row[QCWEB_ID_COLUMN_NAME].ToString(), out qcwebid))
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            reqsvcd = row[REQUEST_SERVER_CODE_COLUMN_NAME].ToString();
                            dlpath = row[DOWNLOAD_PATH_COLUMN_NAME].ToString();
                            title = row[SERVEY_TITLE_COLUMN_NAME].ToString();
                            int tmpValue = 0;
                            if (!int.TryParse(row[XL_FILE_FORMAT_COLUMN_NAME].ToString(), out tmpValue))
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            xlfmt = (Excel.XlFileFormat)tmpValue;
                            if (!int.TryParse(row[NA_SHOW_CODE_COLUMN_NAME].ToString(), out tmpValue))
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            shownacd = (ShowCode)tmpValue;
                            if (!int.TryParse(row[IV_SHOW_CODE_COLUMN_NAME].ToString(), out tmpValue))
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            showivcd = (ShowCode)tmpValue;
                            if (!int.TryParse(row[ZERO_NA_IV_SHOW_CODE_COLUMN_NAME].ToString(), out tmpValue))
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            zeroshowcd = (ZeroNAIVShowCode)tmpValue;
                            if (!int.TryParse(row[N_SHOW_ITEM_CODE_COLUMN_NAME].ToString(), out tmpValue))
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            showncd = (NumericContentsCode)tmpValue;
                            if (!int.TryParse(row[WB_SETTING_CODE_COLUMN_NAME].ToString(), out tmpValue))
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            wbcd = (WBSettingCode)tmpValue;
                            mergeaxis = row[MERGE_AXES_CELLS_FLAG_COLUMN_NAME].ToString() != "0";
                            if (!int.TryParse(row[SUMMARY_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME].ToString(), out sumnumdigits)
                             || !int.TryParse(row[AVERAGE_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME].ToString(), out avgnumdigits)
                             || !int.TryParse(row[STDEV_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME].ToString(), out stdevnumdigits)
                             || !int.TryParse(row[MIN_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME].ToString(), out minnumdigits)
                             || !int.TryParse(row[MAX_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME].ToString(), out maxnumdigits)
                             || !int.TryParse(row[MEDIAN_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME].ToString(), out mednumdigits)
                             || !int.TryParse(row[WT_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME].ToString(), out wtnumdigits)
                             || !int.TryParse(row[WT_AVERAGE_NUM_DIGITS_AFTER_DECIMAL_COLUMN_NAME].ToString(), out wtavgnumdigits)
                                )
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            object preRunServer = row[REPORT_SERVER_CODE_COLUMN_NAME];
                            retry_flg = preRunServer != null && preRunServer.ToString().Length > 0;

                            decimal reportsetid = (decimal)0;
                            if (!decimal.TryParse(row[REPORTSET_TABLE_ID_COLUMN_NAME].ToString(), out reportsetid))
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            if (!int.TryParse(row[OUTPUT_FILE_TYPE_CODE_COLUMN_NAME].ToString(), out tmpValue))
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            FileType outputfiletype = (FileType)tmpValue;
                            string templatepath = row[PP_TEMPLATE_PATH_COLUMN_NAME].ToString();
                            string ppfilenameprefix = row[PP_FILE_NAME_PREFIX_COLUMN_NAME].ToString();
                            if (!int.TryParse(row[PP_FILE_FORMAT_COLUMN_NAME].ToString(), out tmpValue))
                            {
                                // エラースロー
                                throw new Exception("Data is invalid.");
                            }
                            PowerPoint.PpSaveAsFileType ppfileformat = (PowerPoint.PpSaveAsFileType)tmpValue;
                            bool outputcomment = row[COMMENT_OUTPUT_FLAG_COLUMN_NAME].ToString() == "1";
                            string questionnairepurpose = row[SURVEY_PURPOSE_COLUMN_NAME].ToString();
                            string questionnairemethod = row[SURVEY_METHOD_COLUMN_NAME].ToString();
                            string questionnaireterm = row[SURVEY_TERM_COLUMN_NAME].ToString();
                            string questionnaireassignment = row[CELL_SAMPLE_COUNT_COLUMN_NAME].ToString();
                            string questionnaireorganization = row[SURVEY_ORGANIZATION_COLUMN_NAME].ToString();
                            reportsets = new Reportsets(this);
                            Reportsets.Reportset reportset = reportsets.Add(
                                    reportsetid, outputfiletype, templatepath, ppfilenameprefix, ppfileformat
                                  , outputcomment, questionnairepurpose, questionnairemethod
                                  , questionnaireterm, questionnaireassignment, questionnaireorganization);

                            for (int i = 0; i < table.Rows.Count; ++i)
                            {
                                row = table.Rows[i];
                                decimal outputid = (decimal)0;
                                if (!decimal.TryParse(row[OUTPUT_TABLE_ID_COLUMN_NAME].ToString(), out outputid))
                                {
                                    // エラースロー
                                    throw new Exception("Data is invalid.");
                                }
                                string tsvpaths = row[TSV_PATH_COLUMN_NAME].ToString();
                                string xlfilenameprefix = row[XL_FILE_NAME_PREFIX_COLUMN_NAME].ToString();
                                if (!int.TryParse(row[OUTPUT_TYPE_COLUMN_NAME].ToString(), out tmpValue))
                                {
                                    // エラースロー
                                    throw new Exception("Data is invalid.");
                                }
                                OutputType outputtype = (OutputType)tmpValue;
                                OleDbDataAdapter subadapter = null;
                                DataTable subtable = null;
                                DataRow subrow = null;
                                switch (outputtype)
                                {
                                    case OutputType.CheckTemplate:
                                        {
                                            reportset.UploadPath = row[PP_TEMPLATE_UPLOAD_PATH_COLUMN_NAME].ToString();
                                            (reportset.Outputs as Outputs).Add(outputid, null, null, outputtype);
                                            break;
                                        }
                                    case OutputType.GT:
                                        {
                                            sql = string.Format(
                                                    "SELECT {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} "
                                                  + "FROM {0} "
                                                  + "WHERE {1} = " + outputid.ToString() + ";"
                                                    , "[" + OUTPUT_GT_SUB_TABLE_NAME + "$]"
                                                    , OUTPUT_TABLE_ID_COLUMN_NAME
                                                    , OUTPUT_TABLE_TYPE_COLUMN_NAME, TABLE_ORIENTATION_COLUMN_NAME
                                                    , PAGESETUP_TABLE_TYPE_COLUMN_NAME, PAPER_SIZE_COLUMN_NAME
                                                    , PAPER_ORIENTATION_COLUMN_NAME, MIN_BASE_COLUMN_NAME
                                                    , MARKING_CODE_COLUMN_NAME, SIGNIFICANCE_TEST_MARKING_LEVEL_COLUMN_NAME);
                                            using (subadapter = new OleDbDataAdapter(sql, conn))
                                            {
                                                using (subtable = new DataTable())
                                                {
                                                    subadapter.Fill(subtable);
                                                    if (subtable.Rows.Count != 1)
                                                    {
                                                        // エラースロー
                                                        throw new Exception("Data is invalid.");
                                                    }
                                                    subrow = subtable.Rows[0];
                                                    if (!int.TryParse(subrow[OUTPUT_TABLE_TYPE_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        // エラースロー
                                                        throw new Exception("Data is invalid.");
                                                    }
                                                    TableType tabletype = (TableType)tmpValue;
                                                    if (!int.TryParse(subrow[TABLE_ORIENTATION_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        // エラースロー
                                                        throw new Exception("Data is invalid.");
                                                    }
                                                    TableOrientation tableorientation = (TableOrientation)tmpValue;
                                                    if (!int.TryParse(subrow[PAGESETUP_TABLE_TYPE_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = 0;
                                                    }
                                                    TableType pagesetuptabletype = (TableType)tmpValue & tabletype;
                                                    if (!int.TryParse(subrow[PAPER_SIZE_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = (int)Excel.XlPaperSize.xlPaperA4;
                                                    }
                                                    Excel.XlPaperSize papersize = (Excel.XlPaperSize)tmpValue;
                                                    if (!int.TryParse(subrow[PAPER_ORIENTATION_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = (int)Excel.XlPageOrientation.xlPortrait;
                                                    }
                                                    Excel.XlPageOrientation paperorientation = (Excel.XlPageOrientation)tmpValue;
                                                    int minbase = 0;
                                                    int.TryParse(subrow[MIN_BASE_COLUMN_NAME].ToString(), out minbase);
                                                    if (!int.TryParse(subrow[MARKING_CODE_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = 0;
                                                    }
                                                    MarkingType markingcode = (MarkingType)tmpValue;
                                                    if (!int.TryParse(subrow[SIGNIFICANCE_TEST_MARKING_LEVEL_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = 0;
                                                    }
                                                    SignificanceTestLevel siglevel = (SignificanceTestLevel)tmpValue;
                                                    (reportset.Outputs as Outputs).Add(outputid, tsvpaths, xlfilenameprefix
                                                            , tabletype, tableorientation, pagesetuptabletype, papersize
                                                            , paperorientation, markingcode, minbase, siglevel);
                                                }
                                            }
                                            break;
                                        }
                                    case OutputType.Cross:
                                        {
                                            sql = string.Format(
                                                    "SELECT {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}"
                                                         + "{10}, {11}, {12}, {13}, {14}, {15}, {16} "
                                                  + "FROM {0} "
                                                  + "WHERE {1} = " + outputid.ToString() + ";"
                                                    , "[" + OUTPUT_CROSS_SUB_TABLE_NAME + "$]"
                                                    , OUTPUT_TABLE_ID_COLUMN_NAME
                                                    , OUTPUT_METHOD_COLUMN_NAME, OUTPUT_TABLE_TYPE_COLUMN_NAME
                                                    , TABLE_ORIENTATION_COLUMN_NAME, PAGESETUP_TABLE_TYPE_COLUMN_NAME
                                                    , PAPER_SIZE_COLUMN_NAME, PAPER_ORIENTATION_COLUMN_NAME
                                                    , MIN_BASE_COLUMN_NAME, MARKING_CODE_COLUMN_NAME
                                                    , SIGNIFICANCE_TEST_MARKING_LEVEL_COLUMN_NAME
                                                    , LEVEL2_PLUS_COLOR_INDEX_COLUMN_NAME, LEVEL1_PLUS_COLOR_INDEX_COLUMN_NAME
                                                    , LEVEL1_MINUS_COLOR_INDEX_COLUMN_NAME, LEVEL2_MINUS_COLOR_INDEX_COLUMN_NAME
                                                    , LEVEL2_CUSTOM_PERCENT_COLUMN_NAME, LEVEL1_CUSTOM_PERCENT_COLUMN_NAME);
                                            using (subadapter = new OleDbDataAdapter(sql, conn))
                                            {
                                                using (subtable = new DataTable())
                                                {
                                                    subadapter.Fill(subtable);
                                                    if (subtable.Rows.Count != 1)
                                                    {
                                                        // エラースロー
                                                        throw new Exception("Data is invalid.");
                                                    }
                                                    subrow = subtable.Rows[0];
                                                    if (!int.TryParse(subrow[OUTPUT_METHOD_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        // エラースロー
                                                        throw new Exception("Data is invalid.");
                                                    }
                                                    TablesOnOneSheet outputmethod = (TablesOnOneSheet)tmpValue;
                                                    if (!int.TryParse(subrow[OUTPUT_TABLE_TYPE_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        // エラースロー
                                                        throw new Exception("Data is invalid.");
                                                    }
                                                    TableType tabletype = (TableType)tmpValue;
                                                    if (!int.TryParse(subrow[TABLE_ORIENTATION_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        // エラースロー
                                                        throw new Exception("Data is invalid.");
                                                    }
                                                    TableOrientation tableorientation = (TableOrientation)tmpValue;
                                                    if (!int.TryParse(subrow[PAGESETUP_TABLE_TYPE_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = 0;
                                                    }
                                                    TableType pagesetuptabletype = (TableType)tmpValue & tabletype;
                                                    if (!int.TryParse(subrow[PAPER_SIZE_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = (int)Excel.XlPaperSize.xlPaperA4;
                                                    }
                                                    Excel.XlPaperSize papersize = (Excel.XlPaperSize)tmpValue;
                                                    if (!int.TryParse(subrow[PAPER_ORIENTATION_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = (int)Excel.XlPageOrientation.xlPortrait;
                                                    }
                                                    Excel.XlPageOrientation paperorientation = (Excel.XlPageOrientation)tmpValue;
                                                    int minbase = 0;
                                                    int.TryParse(subrow[MIN_BASE_COLUMN_NAME].ToString(), out minbase);
                                                    if (!int.TryParse(subrow[MARKING_CODE_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = 0;
                                                    }
                                                    MarkingType markingcode = (MarkingType)tmpValue;
                                                    if (!int.TryParse(subrow[SIGNIFICANCE_TEST_MARKING_LEVEL_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = 0;
                                                    }
                                                    SignificanceTestLevel siglevel = (SignificanceTestLevel)tmpValue;
                                                    int level2highcolorindex = 6;
                                                    int.TryParse(subrow[LEVEL2_PLUS_COLOR_INDEX_COLUMN_NAME].ToString(), out level2highcolorindex);
                                                    int level1highcolorindex = 36;
                                                    int.TryParse(subrow[LEVEL1_PLUS_COLOR_INDEX_COLUMN_NAME].ToString(), out level1highcolorindex);
                                                    int level1lowcolorindex = 34;
                                                    int.TryParse(subrow[LEVEL1_MINUS_COLOR_INDEX_COLUMN_NAME].ToString(), out level1lowcolorindex);
                                                    int level2lowcolorindex = 37;
                                                    int.TryParse(subrow[LEVEL2_MINUS_COLOR_INDEX_COLUMN_NAME].ToString(), out level2lowcolorindex);
                                                    int level1percent = 5;
                                                    int.TryParse(subrow[LEVEL1_CUSTOM_PERCENT_COLUMN_NAME].ToString(), out level1percent);
                                                    int level2percent = 10;
                                                    int.TryParse(subrow[LEVEL2_CUSTOM_PERCENT_COLUMN_NAME].ToString(), out level2percent);
                                                    (reportset.Outputs as Outputs).Add(outputid, tsvpaths, xlfilenameprefix
                                                            , tabletype, tableorientation, outputmethod
                                                            , pagesetuptabletype, papersize, paperorientation
                                                            , siglevel, markingcode, minbase
                                                            , level2highcolorindex, level1highcolorindex, level1lowcolorindex, level2lowcolorindex
                                                            , level1percent, level2percent);
                                                }
                                            }
                                            break;
                                        }
                                    case OutputType.FAList:
                                        {
                                            sql = string.Format(
                                                    "SELECT {2}, {3} "
                                                  + "FROM {0} "
                                                  + "WHERE {1} = " + outputid.ToString() + ";"
                                                    , "[" + OUTPUT_GT_SUB_TABLE_NAME + "$]"
                                                    , OUTPUT_TABLE_ID_COLUMN_NAME
                                                    , PAPER_SIZE_COLUMN_NAME, PAPER_ORIENTATION_COLUMN_NAME);
                                            using (subadapter = new OleDbDataAdapter(sql, conn))
                                            {
                                                using (subtable = new DataTable())
                                                {
                                                    subadapter.Fill(subtable);
                                                    if (subtable.Rows.Count != 1)
                                                    {
                                                        // エラースロー
                                                        throw new Exception("Data is invalid.");
                                                    }
                                                    subrow = subtable.Rows[0];
                                                    if (!int.TryParse(subrow[PAPER_SIZE_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = 0;
                                                    }
                                                    Excel.XlPaperSize papersize = (Excel.XlPaperSize)tmpValue;
                                                    if (!int.TryParse(subrow[PAPER_ORIENTATION_COLUMN_NAME].ToString(), out tmpValue))
                                                    {
                                                        tmpValue = (int)Excel.XlPageOrientation.xlPortrait;
                                                    }
                                                    Excel.XlPageOrientation paperorientation = (Excel.XlPageOrientation)tmpValue;
                                                    (reportset.Outputs as Outputs).Add(outputid, tsvpaths, xlfilenameprefix
                                                            , papersize, paperorientation);
                                                }
                                            }
                                            break;
                                        }
                                    case OutputType.CheckList:
                                        {
                                            sql = string.Format(
                                                    "SELECT {2} "
                                                  + "FROM {0} "
                                                  + "WHERE {1} = " + outputid.ToString() + ";"
                                                    , "[" + OUTPUT_GT_SUB_TABLE_NAME + "$]"
                                                    , OUTPUT_TABLE_ID_COLUMN_NAME
                                                    , TOTAL_COUNT_COLUMN_NAME);
                                            using (subadapter = new OleDbDataAdapter(sql, conn))
                                            {
                                                using (subtable = new DataTable())
                                                {
                                                    subadapter.Fill(subtable);
                                                    if (subtable.Rows.Count != 1)
                                                    {
                                                        // エラースロー
                                                        throw new Exception("Data is invalid.");
                                                    }
                                                    subrow = subtable.Rows[0];
                                                    int totalcount = 0;
                                                    if (!int.TryParse(subrow[TOTAL_COUNT_COLUMN_NAME].ToString(), out totalcount)
                                                        || totalcount < 0)
                                                    {
                                                        // エラースロー
                                                        throw new Exception("Data is invalid.");
                                                    }
                                                    (reportset.Outputs as Outputs).Add(outputid, tsvpaths, xlfilenameprefix
                                                            , totalcount);
                                                }
                                            }
                                            break;
                                        }
                                    case OutputType.Questionnaire:
                                        {
                                            (reportset.Outputs as Outputs).Add(outputid, tsvpaths, xlfilenameprefix, outputtype);
                                            break;
                                        }
                                    case OutputType.RawData:
                                    case OutputType.QC3:
                                        {
                                            (reportset.Outputs as Outputs).Add(outputid, tsvpaths, xlfilenameprefix, new Question.Questions(qcwebid));
                                            break;
                                        }
                                    default:
                                        {
                                            // エラースロー
                                            throw new Exception("Output Type is invalid.");
                                        }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                    Debug.Indent();
                    Debug.WriteLine("Type:{0}", e.GetType().ToString());
                    Debug.WriteLine("Description:{0}", e.Message);
                    Debug.Unindent();
                    throw;
                }
                finally
                {
                    try
                    {
                        conn.Close();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                        Debug.Indent();
                        Debug.WriteLine("Type:{0}", e.GetType().ToString());
                        Debug.WriteLine("Description:{0}", e.Message);
                        Debug.Unindent();                        
                        throw;
                    }
                }
            }
            
#else
            QuillInjector.GetInstance().Inject(this);
            TOutputReportsetInfoRequestPmb pmb = new TOutputReportsetInfoRequestPmb();
            pmb.OutputRequestId = id;
            ListResultBean<TOutputReportsetInfoRequest> list =
                tOutputReportsetInfoBhv.OutsideSql().SelectList<TOutputReportsetInfoRequest>(
                                                                        TOutputReportsetInfoBhv.PATH_SelectRequestInfo
                                                                        , pmb);
            if (list.Count <= 0)
            {
                throw new QCWebException(Constants.GET_OUTPUT_INFORMATION_FATAL_MESSAGE_ID
                            , new string[] { id.ToString() }, GlobalsCommonConstant.LogLevel.FATAL, null);
            }
            TOutputReportsetInfoRequest request = list[0];

            // レコード走査
            this.id = (decimal)request.OutputRequestId;
            decimal qcwebid = (decimal)request.Qcwebid;
            reqsvcd = request.RequestServerCode;
            dlpath = request.DownloadPath;
            title = request.ViewSurveyName;
            if (request.ExcelbookType != null)
            {
                xlfmt = (Excel.XlFileFormat)request.ExcelbookType;
            }
            // shownacd = (ShowCode)request.NoanswerVisibleCode;
            // showivcd = (ShowCode)request.UnmacthVisibleCode;
            // 0件表示コードをzeroshowcdに代入
            zeroshowcd = (ZeroNAIVShowCode)request.ShowZeroNaIvCode;
            if (request.NumericAnswerViewCode != null)
            {
                showncd = (NumericContentsCode)request.NumericAnswerViewCode;
            }
            // showtotalflg = tOutputRequest.WeightbackVisibleFlag != 0;    // WB集計表示コードをwbcdに代入
            // 集計軸セル結合フラグをmergeaxisに代入
            mergeaxis = request.MergeAxisCellsFlag == "1";
            if (request.DpTotal != null) sumnumdigits = (int)request.DpTotal;
            if (request.DpAverage != null) avgnumdigits = (int)request.DpAverage;
            if (request.DpStandardDiv != null) stdevnumdigits = (int)request.DpStandardDiv;
            if (request.DpMin != null) minnumdigits = (int)request.DpMin;
            if (request.DpMax != null) maxnumdigits = (int)request.DpMax;
            if (request.DpMedian != null) mednumdigits = (int)request.DpMedian;
            if (request.DpWeight != null) wtnumdigits = (int)request.DpWeight;
            if (request.DpWeightavr != null) wtavgnumdigits = (int)request.DpWeightavr;
            string lccd = request.Language;
            // 対応外のロケーションコードは、入口で止めている(既定値「ja_JP」に変えている)ので、ここでの考慮は不要
            if (lccd != null && lccd.Length >= 2) this.lccd = lccd.Substring(0, 2).ToUpper();
            retry_flg = request.ProcServerCode != null;

            // SFTP接続情報
            string fileSV = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_AIRS_FILE_SV);
            string user = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_AIRS_SFTP_USER);
            string pass = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_AIRS_SFTP_PASSWORD);
            string sshHostKey = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_SSH_HOST_KEY);
            string winscp = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_WINSCP_COMMAND);
            string scriptPath = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_SFTP_SCRIPT_PATH);
            #region
            // いったんコメント。処理途中出力物は取得しない
            //if (retry_flg) {
            //    // 作成済みの出力物を取得する
            //    string tempFilePath = Path.Combine(tempPath, Path.GetFileNameWithoutExtension(request.DownloadPath));
            //    SFTP sftp = new SFTP(fileSV, user, pass, sshHostKey, winscp, scriptPath);
            //    sftp.GetFile(request.DownloadPath, tempFilePath);
            //}
            #endregion

            // 割付有効回答数文字列作成
            StringBuilder assignmentSB = new StringBuilder();
            TAllocationCellInfoCB tAllocationCellInfoCB = new TAllocationCellInfoCB();
            tAllocationCellInfoCB.Query().SetQcwebid_Equal(qcwebid);
            tAllocationCellInfoCB.Query().AddOrderBy_AllocationCellId_Asc();
            ListResultBean<TAllocationCellInfo> tAllocationCellInfoList = tAllocationCellInfoBhv.SelectList(tAllocationCellInfoCB);
            for (int i = 0; i < tAllocationCellInfoList.Count; ++i)
            {
                TAllocationCellInfo tAllocationCellInfo = tAllocationCellInfoList[i];
                if (i > 0) assignmentSB.Append("\r\n");
                assignmentSB.Append(tAllocationCellInfo.CellNo + "\t");
                assignmentSB.Append(tAllocationCellInfo.CellName + "\t");
                assignmentSB.Append(tAllocationCellInfo.ExpectationSampleCount + "\t");
                assignmentSB.Append(tAllocationCellInfo.ValidSampleCount);
            }

            // 調査方法、調査時期文字列作成
            StringBuilder surveyMethodSB = new StringBuilder();
            StringBuilder surveyDateSB = new StringBuilder();
            TQcwebSurveyDetailCB tQcwebSurveyDetailCB = new TQcwebSurveyDetailCB();
            tQcwebSurveyDetailCB.Query().SetQcwebid_Equal(qcwebid);
            tQcwebSurveyDetailCB.Query().AddOrderBy_QcwebDetailId_Asc();
            ListResultBean<TQcwebSurveyDetail> tQcwebSurveyDetailList = tQcwebSurveyDetailBhv.SelectList(tQcwebSurveyDetailCB);
            for (int i = 0; i < tQcwebSurveyDetailList.Count; ++i)
            {
                TQcwebSurveyDetail entity = tQcwebSurveyDetailList[i];
                if (i > 0)
                {
                    surveyMethodSB.Append("\t");
                    surveyDateSB.Append("\t");
                }
                else
                {
                    this.qcwebid = (decimal)entity.Qc3uniqueId;
                }
                surveyMethodSB.Append(entity.SurveyMethod);
                surveyDateSB.Append(entity.SurveyDate);
            }

            reportsets = new Reportsets(this);
            // TODO:[QCR0000018]Const化
            // string companyName = GetResource.GetCommonResourceData("QCR0000018", lccd);
            string companyName = GetResource.GetReportKeyword(QCWeb.Common.Constants.ReportMessageIndex.ReportSignatureKeywordIndex, lccd);

            // PPTテンプレートファイルの取得
            string localPath = null;
            string path = appConfig.GetValue(reqsvcd) + appConfig.GetValue(GlobalsCommonConstant.COMMON_ACCUMULATE_NETWORK_PATH);
            string extension = null;

            if (request.ProcWeight == (int)GlobalsCommonConstant.ReportProcLevel.TEMPLATE_CHK)
            {
                extension = Path.GetExtension(request.UploadPath);
                localPath = Path.Combine(path, request.OutputTemplateId + extension);
            }
            else if (request.Path != null)
            {
                extension = Path.GetExtension(request.Path);
                localPath = Path.Combine(path, Path.GetFileName(request.Path));
            }

            if (localPath != null)
            {
                if (!File.Exists(localPath))
                {
                    SFTP sftp = new SFTP(fileSV, user, pass, sshHostKey, winscp, scriptPath);
                    string airsCurrentDir = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_AIRS_CURRENT_DIRECTORY);
                    string uploadDir = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_PPT_SFTP_UPLOAD_DIR);
                    string svPath = null;
                    if (request.ProcWeight == (int)GlobalsCommonConstant.ReportProcLevel.TEMPLATE_CHK)
                    {
                        svPath = string.Format("{0}{1}{2}/{3}", airsCurrentDir, uploadDir, qcwebid, request.OutputTemplateId + extension);
                    }
                    else
                    {
                        svPath = request.Path;
                    }
                    sftp.GetFile(svPath, localPath);
                    pptTemplateCheckFilePath = svPath;
                }
            }

            Reportsets.Reportset reportset = reportsets.Add(
                (decimal)request.OutputReportsetInfoId
                , (FileType)Enum.Parse(typeof(FileType), request.OutputFileTypeCode.ToString())
                , localPath
                , request.ReportFilenNamePrefix
                , request.PowerpointType != null ? (PowerPoint.PpSaveAsFileType)request.PowerpointType : (PowerPoint.PpSaveAsFileType)0
                , request.CommentOutputFlag == 1
                , ""                                            // 調査目的
                , surveyMethodSB.ToString()                     // 調査方法
                , surveyDateSB.ToString()                       // 調査時期
                , assignmentSB.ToString()                       // 割付有効回答数
                , companyName                                   // 調査実施機関
            );

            foreach (TOutputReportsetInfoRequest common in list)
            {
                switch ((OutputType)common.OutputType)
                {
                    case OutputType.GT:
                        #region GT
                        // GT出力詳細情報の取得
                        TOutputSubGtCB tOutputSubGtCB = new TOutputSubGtCB();
                        tOutputSubGtCB.SetupSelect_TOutputCommon();
                        tOutputSubGtCB.Query().QueryTOutputCommon().InnerJoin();
                        tOutputSubGtCB.Query().SetOutputCommonId_Equal(common.OutputCommonId);
                        TOutputSubGt tOutputSubGt = tOutputSubGTBhv.SelectEntityWithDeletedCheck(tOutputSubGtCB);

                        Outputs.OutputGT gt = (reportset.Outputs as Outputs).Add(
                            (decimal)common.OutputCommonId
                            , ConvertTSVPath(tOutputSubGt.TOutputCommon.TsvFilePath)
                            , tOutputSubGt.TOutputCommon.ExcelbookNamePrefix
                            , (TableType)Enum.Parse(typeof(TableType), tOutputSubGt.OutputTableType.ToString())
                            , (TableOrientation)tOutputSubGt.OutputTableOrientation
                            , (TableType)Enum.Parse(typeof(TableType), tOutputSubGt.PageSettingTableType.ToString())
                            , (Excel.XlPaperSize)tOutputSubGt.PageSettingPaperSize
                            , (Excel.XlPageOrientation)tOutputSubGt.PageSettingPaperOrientation
                            , (MarkingType)tOutputSubGt.MarkingCode
                            , (int)tOutputSubGt.MarkingMinParameter
                            , (SignificanceTestLevel)tOutputSubGt.MarkingLevel
                        );
                        gt.ShowNACode = (ShowCode)common.NoanswerVisibleCode;       // 無回答表示コード
                        gt.ShowIVCode = (ShowCode)common.UnmatchVisibleCode;        // 非該当表示コード
                        gt.WBSettingCode = (WBSettingCode)common.WbSettingCode;     // WB設定コード
                        gt.FilteringExpression = tOutputSubGt.FilteringExpression;  // 絞込み条件
                        break;
                    #endregion
                    case OutputType.Cross:
                        #region クロス
                        TOutputSubCrossCB tOutputSubCrossCB = new TOutputSubCrossCB();
                        tOutputSubCrossCB.SetupSelect_TOutputCommon();
                        tOutputSubCrossCB.Query().QueryTOutputCommon().InnerJoin();
                        tOutputSubCrossCB.Query().SetOutputCommonId_Equal(common.OutputCommonId);
                        TOutputSubCross tOutputSubCross = tOutputSubCrossBhv.SelectEntityWithDeletedCheck(tOutputSubCrossCB);

                        Outputs.OutputCross cross = (reportset.Outputs as Outputs).Add(
                            (decimal)common.OutputCommonId
                            , ConvertTSVPath(tOutputSubCross.TOutputCommon.TsvFilePath)
                            , tOutputSubCross.TOutputCommon.ExcelbookNamePrefix
                            , (TableType)Enum.Parse(typeof(TableType), tOutputSubCross.OutputTableType.ToString())
                            , (TableOrientation)tOutputSubCross.OutputTableOrientation
                            , (TablesOnOneSheet)tOutputSubCross.OutputType
                            , (TableType)Enum.Parse(typeof(TableType), tOutputSubCross.PageSettingTableType.ToString())
                            , (Excel.XlPaperSize)tOutputSubCross.PageSettingPaperSize
                            , (Excel.XlPageOrientation)tOutputSubCross.PageSettingPaperOrientation
                            , (SignificanceTestLevel)tOutputSubCross.MarkingLevel
                            , (MarkingType)tOutputSubCross.MarkingCode
                            , (int)tOutputSubCross.MarkingMinParameter
                            , (int)tOutputSubCross.Level2pluscolor
                            , (int)tOutputSubCross.Level1pluscolor
                            , (int)tOutputSubCross.Level1minuscolor
                            , (int)tOutputSubCross.Level2minuscolor
                            , (int)tOutputSubCross.Level1percent
                            , (int)tOutputSubCross.Level2percent
                        );
                        cross.ShowNACode = (ShowCode)common.NoanswerVisibleCode;            // 無回答表示コード
                        cross.ShowIVCode = (ShowCode)common.UnmatchVisibleCode;             // 非該当表示コード
                        cross.WBSettingCode = (WBSettingCode)common.WbSettingCode;          // WB設定コード
                        cross.FilteringExpression = tOutputSubCross.FilteringExpression;    // 絞込み条件
                        break;
                    #endregion
                    case OutputType.FAList:
                        #region FAリスト
                        TOutputSubFaCB tOutputSubFaCB = new TOutputSubFaCB();
                        tOutputSubFaCB.SetupSelect_TOutputCommon();
                        tOutputSubFaCB.Query().QueryTOutputCommon().InnerJoin();
                        tOutputSubFaCB.Query().SetOutputCommonId_Equal(common.OutputCommonId);
                        TOutputSubFa tOutputSubFa = tOutputSubFaBhv.SelectEntityWithDeletedCheck(tOutputSubFaCB);

                        Outputs.OutputFA fa = (reportset.Outputs as Outputs).Add(
                            (decimal)common.OutputCommonId
                            , ConvertTSVPath(tOutputSubFa.TOutputCommon.TsvFilePath)
                            , tOutputSubFa.TOutputCommon.ExcelbookNamePrefix
                            , (Excel.XlPaperSize)tOutputSubFa.PageSettingPaperSize
                            , (Excel.XlPageOrientation)tOutputSubFa.PageSettingPaperOrientation
                        );
                        fa.FilteringExpression = tOutputSubFa.FilteringExpression;  // 絞込み条件
                        break;
                    #endregion
                    case OutputType.CheckList:
                        #region チェックリスト
                        TOutputSubCklistCB tOutputSubCklistCB = new TOutputSubCklistCB();
                        tOutputSubCklistCB.SetupSelect_TOutputCommon();
                        tOutputSubCklistCB.Query().QueryTOutputCommon().InnerJoin();
                        tOutputSubCklistCB.Query().SetOutputCommonId_Equal(common.OutputCommonId);
                        TOutputSubCklist tOutputSubCklist = tOutputSubCklistBhv.SelectEntityWithDeletedCheck(tOutputSubCklistCB);

                        Outputs.OutputCheckList checkList = (reportset.Outputs as Outputs).Add(
                            (decimal)common.OutputCommonId
                            , ConvertTSVPath(tOutputSubCklist.TOutputCommon.TsvFilePath)
                            , tOutputSubCklist.TOutputCommon.ExcelbookNamePrefix
                            , (int)tOutputSubCklist.TotalCount
                        );
                        break;
                    #endregion
                    case OutputType.Questionnaire:
                        #region 調査票
                        {
                            decimal outputid = (decimal)common.OutputCommonId;
                            string joinedtsvpaths = ConvertTSVPath(common.TsvFilePath);
                            string xlbooknameprefix = common.ExcelbookNamePrefix;
                            Outputs.Output questVote = (reportset.Outputs as Outputs).Add(
                                                outputid, joinedtsvpaths, xlbooknameprefix, OutputType.Questionnaire);
                        }
                        break;
                    #endregion
                    case OutputType.RawData:
                    case OutputType.QC3:
                        #region ローデータ、QC3
                        {
                            decimal outputid = (decimal)common.OutputCommonId;
                            string tsvpath = ConvertTSVPath(common.TsvFilePath);
                            string xlbooknameprefix = common.ExcelbookNamePrefix;
                            // Question.Questions questions = new Question.Questions(qcwebid);
                            // Outputs.OutputRawData excel = (reportset.Outputs as Outputs).Add(
                            //                             outputid, tsvpath, xlbooknameprefix, questions);
                            bool isQC3 = (OutputType)common.OutputType == OutputType.QC3;
                            Outputs.OutputRawData excel = (reportset.Outputs as Outputs).Add(
                                                        outputid, tsvpath, xlbooknameprefix, isQC3);
                            if (isQC3)
                            {
                                excel.IsUTF8 = common.Utf8Flag == 1;
                            }
                        }
                        break;
                    #endregion
                    case OutputType.CheckTemplate:
                        #region PPテンプレートチェック
                        {
                            reportset.UploadPath = common.UploadPath;
                            decimal outputid = (decimal)common.OutputCommonId;
                            Outputs.Output checkTemplate = (reportset.Outputs as Outputs).Add(outputid, null, null, OutputType.CheckTemplate);
                        }
                        break;
                    #endregion
                    default:
                        {
                            throw new QCWebException(Constants.UNKNOWN_OUTPUT_TYPE_FATAL_MESSAGE_ID
                                    , new string[] { common.OutputCommonId.ToString() }
                                    , GlobalsCommonConstant.LogLevel.FATAL, null);
                        }
                }
            }
#endif
        }

        private string ConvertTSVPath(string tsvpaths)
        {
            if (string.IsNullOrWhiteSpace(tsvpaths))
                return null;
            string oldPath = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP);
            string newPath = appConfig.GetValue(reqsvcd) + appConfig.GetValue(GlobalsCommonConstant.COMMON_ACCUMULATE_NETWORK_PATH);
            string[] tsvpath = tsvpaths.Split(';');
            string[] newTsvpath = new string[tsvpath.Length];

            for (int i = 0; i < tsvpath.Length; ++i)
            {
                newTsvpath[i] = tsvpath[i].Replace(oldPath, newPath);
            }

            return string.Join(";", newTsvpath);
        }


        /// <summary>
        /// リクエストに紐づくレポートセットコレクションへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IReportsets Reportsets
        {
            get
            {
                return reportsets;
            }
        }

        /// <summary>
        /// リクエストIDを返す読み取り専用プロパティ
        /// </summary>
        [ComVisible(false)]
        public decimal ID
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// リクエストIDを返す読み取り専用プロパティ
        /// <note>VBAから呼べるようにdouble</note>
        /// </summary>
        public double ID2
        {
            get
            {
                return (double)id;
            }
        }

        /// <summary>
        /// 調査IDを返す読み取り専用プロパティ
        /// </summary>
        [ComVisible(false)]
        public decimal QCWebID
        {
            get
            {
                return qcwebid;
            }
        }

        /// <summary>
        /// 調査IDを返す読み取り専用プロパティ
        /// <note>VBAから呼べるようにdouble</note>
        /// </summary>
        public double QCWebID2
        {
            get
            {
                return (double)qcwebid;
            }
        }

        /// <summary>
        /// リクエストを発行したサーバのサーバコードを返す読み取り専用プロパティ
        /// </summary>
        public string RequestServerCode
        {
            get
            {
                return reqsvcd;
            }
        }

        /// <summary>
        /// 出力物のダウンロードパスを返す読み取り専用プロパティ
        /// </summary>
        public string DownloadPath
        {
            get
            {
                return dlpath;
            }
        }

        /// <summary>
        /// 調査タイトルを返す読み取り専用プロパティ
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
        }

        /// <summary>
        /// 出力するExcelブックのファイル形式を表すXlFileFormat列挙型の値を返す読み取り専用プロパティ
        /// </summary>
        public XlFileFormat ExcelFileFormat
        {
            get
            {
                return (XlFileFormat)xlfmt;
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、統計量母数を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ShowParameter
        {
            get
            {
                return (showncd & NumericContentsCode.Parameter) == NumericContentsCode.Parameter;
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、合計を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ShowSummary
        {
            get
            {
                return (showncd & NumericContentsCode.Summary) == NumericContentsCode.Summary;
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、平均を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ShowAverage
        {
            get
            {
                return (showncd & NumericContentsCode.Average) == NumericContentsCode.Average;
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、標準偏差を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ShowStdev
        {
            get
            {
                return (showncd & NumericContentsCode.Stdev) == NumericContentsCode.Stdev;
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、最小値を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ShowMinimum
        {
            get
            {
                return (showncd & NumericContentsCode.Minimum) == NumericContentsCode.Minimum;
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、最大値を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ShowMaximum
        {
            get
            {
                return (showncd & NumericContentsCode.Maximum) == NumericContentsCode.Maximum;
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、中央値を表示するかどうかを返す読み取り専用プロパティ
        /// <note>WB集計時には無視される</note>
        /// </summary>
        public bool ShowMedian
        {
            get
            {
                return (showncd & NumericContentsCode.Median) == NumericContentsCode.Median;
            }
        }

        /// <summary>
        /// 数値回答質問の集計項目において、表示する小数点以下の桁数を返すメソッド<br />
        /// </summary>
        /// <param name="ncontentscode">
        /// 数値回答質問の集計項目を表すNumericContentsCode列挙型の以下の値のいずれか
        /// <list type="bullet">
        /// <item>
        /// <description>NumericContentsCode.Summary</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Average</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Stdev</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Minimum</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Maximum</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Median</description>
        /// </item>
        /// </list>
        /// </param>
        /// <returns>ncontentscodeが表す数値回答質問の集計項目で、表示する小数点以下の桁数</returns>
        public int NumDigitsAfterDecimal(NumericContentsCode ncontentscode)
        {
            switch (ncontentscode)
            {
                case NumericContentsCode.Summary:
                    return sumnumdigits;
                case NumericContentsCode.Average:
                    return avgnumdigits;
                case NumericContentsCode.Stdev:
                    return stdevnumdigits;
                case NumericContentsCode.Minimum:
                    return minnumdigits;
                case NumericContentsCode.Maximum:
                    return maxnumdigits;
                case NumericContentsCode.Median:
                    return mednumdigits;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// ウエイト値設定時に、表示する小数点以下の桁数を返す読み取り専用プロパティ
        /// </summary>
        public int WeightNumDigitsAfterDecimal
        {
            get
            {
                return wtnumdigits;
            }
        }

        /// <summary>
        /// 加重平均算出時に、表示する小数点以下の桁数を返す読み取り専用プロパティ
        /// </summary>
        public int WeightAverageNumDigitsAfterDecimal
        {
            get
            {
                return wtavgnumdigits;
            }
        }

        /// <summary>
        /// ロケーションを表す2文字のコードを返す読み取り専用プロパティ
        /// </summary>
        public string LocationCode
        {
            get
            {
                return lccd;
            }
        }

        /// <summary>
        /// リトライ実行であるかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool RetryFlag
        {
            get
            {
                return retry_flg;
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            if (reportsets != null)
                reportsets.Dispose();
        }

        /// <summary>
        /// 集計軸のセルを結合するかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool MergeAxis
        {
            get
            {
                return mergeaxis;
            }
        }

        /// <summary>
        /// 0件の無回答/非該当を表示するかどうかを返す読み取り専用プロパティ
        /// </summary>
        public bool ShowZeroNAIV
        {
            get
            {
                return zeroshowcd == (ZeroNAIVShowCode.NA | ZeroNAIVShowCode.IV);
            }
        }

        private string statusdescription = null;
        /// <summary>
        /// ステータスコードの説明を取得/設定するプロパティ
        /// </summary>
        public string StatusDescription
        {
            get
            {
                return statusdescription;
            }
            set
            {
                statusdescription = value;
            }
        }

        /// <summary>
        /// PPTテンプレートチェックファイルパスを取得するプロパティ
        /// </summary>
        public string PptTemplateCheckFilePath
        {
            get { return pptTemplateCheckFilePath; }
        }


        public void MakeRequeszt(decimal id, List<List<string>> tsvPathDiv, List<string> divNameLis, string title, string companyName, ZeroNAIVShowCode zeroshowcd, bool mergeaxis
            , string reportPrefix, string xlbooknameprefix, TableType tabletype, TableOrientation tableorientation, TableType pagesetuptabletype
            , int minsamplescountonmarking, MarkingType markingtype, SignificanceTestLevel significancetestlevel, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
            , TablesOnOneSheet tablesononesheet, int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex
            , int level2lowcolorindex, int level1percent, int level2percent, ShowCode ShowNACode, ShowCode ShowIVCode
            , WBSettingCode WBOn, string FilteringExpression, bool PreWbBase, bool isChart)
        {
            // レコード走査
            this.id = id;
            this.qcwebid = id;
            this.title = title;
            this.mergeaxis = mergeaxis;
            this.zeroshowcd = zeroshowcd;
            this.wtavgnumdigits = 2;
            this.sumnumdigits = 0;
            reportsets = new Reportsets(this);
            int idReportSet = 0;
            foreach (List<string> tsvPaths in tsvPathDiv)
            {
                string divName = "";
                if (divNameLis != null)
                {
                    divName = divNameLis[idReportSet];
                }
                idReportSet++;
                Reportsets.Reportset reportset = reportsets.Add(
                    (decimal)idReportSet
                    // , (FileType)Enum.Parse(typeof(FileType), request.OutputFileTypeCode.ToString())
                    , isChart ? (FileType.Excel | FileType.Report) : FileType.Excel
                    , null
                    , reportPrefix
                    , (PowerPoint.PpSaveAsFileType)0
                    , false
                    , ""                                            // 調査目的
                    , ""                     // 調査方法
                    , ""                       // 調査時期
                    , ""                     // 割付有効回答数
                    , companyName                                   // 調査実施機関
                    , divName
                );
                int i = 0;
                foreach (string tsvPath in tsvPaths)
                {
                    Outputs.OutputCross cross = (reportset.Outputs as Outputs).Add(
                        (decimal)i++
                        , tsvPath
                        , xlbooknameprefix
                        , isChart ? TableType.Per : tabletype
                        , tableorientation
                        , isChart ? TablesOnOneSheet.Single : tablesononesheet
                        , pagesetuptabletype
                        , papersize
                        , paperorientation
                        , significancetestlevel
                        , markingtype
                        , minsamplescountonmarking
                        , level2highcolorindex
                        , level1highcolorindex
                        , level1lowcolorindex
                        , level2lowcolorindex
                        , level1percent
                        , level2percent
                    );
                    cross.ShowNACode = ShowNACode;            // 無回答表示コード
                    cross.ShowIVCode = ShowIVCode;             // 非該当表示コード
                    cross.WBSettingCode = WBOn;          // WB設定コード
                    cross.FilteringExpression = FilteringExpression;    // 絞込み条件
                    cross.PreWbBase = PreWbBase;
                }
            }
        }


        public void MakeRequeszt(decimal id, int idReportSet, List<string> tsvPaths, string divName, string title, string companyName, ZeroNAIVShowCode zeroshowcd, bool mergeaxis
            , string reportPrefix, string xlbooknameprefix, TableType tabletype, TableOrientation tableorientation, TableType pagesetuptabletype
            , int minsamplescountonmarking, MarkingType markingtype, SignificanceTestLevel significancetestlevel, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
            , TablesOnOneSheet tablesononesheet, int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex
            , int level2lowcolorindex, int level1percent, int level2percent, ShowCode ShowNACode, ShowCode ShowIVCode
            , WBSettingCode WBOn, string FilteringExpression, bool PreWbBase, bool isChart)
        {
            // レコード走査
            this.id = id;
            this.qcwebid = id;
            this.title = title;
            this.mergeaxis = mergeaxis;
            this.zeroshowcd = zeroshowcd;
            this.wtavgnumdigits = 2;
            this.sumnumdigits = 0;
            reportsets = new Reportsets(this);
            Reportsets.Reportset reportset = reportsets.Add(
                (decimal)idReportSet
                // , (FileType)Enum.Parse(typeof(FileType), request.OutputFileTypeCode.ToString())
                , isChart ? (FileType.Excel | FileType.Report) : FileType.Excel
                , null
                , reportPrefix
                , (PowerPoint.PpSaveAsFileType)0
                , false
                , ""                                            // 調査目的
                , ""                     // 調査方法
                , ""                       // 調査時期
                , ""                     // 割付有効回答数
                , companyName                                   // 調査実施機関
                , divName
            );
            int i = 0;
            foreach (string tsvPath in tsvPaths)
            {
                Outputs.OutputCross cross = (reportset.Outputs as Outputs).Add(
                    (decimal)i++
                    , tsvPath
                    , xlbooknameprefix
                    , isChart ? TableType.Per : tabletype
                    , tableorientation
                    , isChart ? TablesOnOneSheet.Single : tablesononesheet
                    , pagesetuptabletype
                    , papersize
                    , paperorientation
                    , significancetestlevel
                    , markingtype
                    , minsamplescountonmarking
                    , level2highcolorindex
                    , level1highcolorindex
                    , level1lowcolorindex
                    , level2lowcolorindex
                    , level1percent
                    , level2percent
                );
                cross.ShowNACode = ShowNACode;            // 無回答表示コード
                cross.ShowIVCode = ShowIVCode;             // 非該当表示コード
                cross.WBSettingCode = WBOn;          // WB設定コード
                cross.FilteringExpression = FilteringExpression;    // 絞込み条件
                cross.PreWbBase = PreWbBase;
            }
        }


        public void MakeRequeszt(decimal id, int idReportSet, int i, string tsvPath, string divName, string title, string companyName, ZeroNAIVShowCode zeroshowcd, bool mergeaxis
            , string reportPrefix, string xlbooknameprefix, TableType tabletype, TableOrientation tableorientation, TableType pagesetuptabletype
            , int minsamplescountonmarking, MarkingType markingtype, SignificanceTestLevel significancetestlevel, Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation
            , TablesOnOneSheet tablesononesheet, int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex
            , int level2lowcolorindex, int level1percent, int level2percent, ShowCode ShowNACode, ShowCode ShowIVCode
            , WBSettingCode WBOn, string FilteringExpression, bool PreWbBase, bool isChart)
        {
            // レコード走査
            this.id = id;
            this.qcwebid = id;
            this.title = title;
            this.mergeaxis = mergeaxis;
            this.zeroshowcd = zeroshowcd;
            this.wtavgnumdigits = 2;
            this.sumnumdigits = 0;
            reportsets = new Reportsets(this);
            Reportsets.Reportset reportset = reportsets.Add(
                (decimal)idReportSet
                // , (FileType)Enum.Parse(typeof(FileType), request.OutputFileTypeCode.ToString())
                , isChart ? (FileType.Excel | FileType.Report) : FileType.Excel
                , null
                , reportPrefix
                , (PowerPoint.PpSaveAsFileType)0
                , false
                , ""                                            // 調査目的
                , ""                     // 調査方法
                , ""                       // 調査時期
                , ""                     // 割付有効回答数
                , companyName                                   // 調査実施機関
                , divName
            );
            Outputs.OutputCross cross = (reportset.Outputs as Outputs).Add(
                (decimal)i
                , tsvPath
                , xlbooknameprefix
                , isChart ? TableType.Per : tabletype
                , tableorientation
                , isChart ? TablesOnOneSheet.Single : tablesononesheet
                , pagesetuptabletype
                , papersize
                , paperorientation
                , significancetestlevel
                , markingtype
                , minsamplescountonmarking
                , level2highcolorindex
                , level1highcolorindex
                , level1lowcolorindex
                , level2lowcolorindex
                , level1percent
                , level2percent
            );
            cross.ShowNACode = ShowNACode;            // 無回答表示コード
            cross.ShowIVCode = ShowIVCode;             // 非該当表示コード
            cross.WBSettingCode = WBOn;          // WB設定コード
            cross.FilteringExpression = FilteringExpression;    // 絞込み条件
            cross.PreWbBase = PreWbBase;
        }

        public void MakeGTRequest(
          decimal id, List<string> tsvPaths, string title, string companyName, ZeroNAIVShowCode zeroshowcd,
          bool mergeaxis, string reportPrefix, string xlbooknameprefix, TableType tabletype, TableOrientation tableorientation
          , TableType pagesetuptabletype
          , int minsamplescountonmarking, MarkingType markingtype, SignificanceTestLevel significancetestlevel
          , Excel.XlPaperSize papersize, Excel.XlPageOrientation paperorientation, ShowCode ShowNACode, ShowCode ShowIVCode
          , WBSettingCode WBOn, string FilteringExpression, bool PreWbBase
           )
        {
            // レコード走査
            this.id = id;
            this.qcwebid = id;
            this.title = title;
            this.mergeaxis = mergeaxis;
            this.zeroshowcd = zeroshowcd;
            this.showncd = NumericContentsCode.All;
            this.wtavgnumdigits = 2;
            this.wtnumdigits = 2;
            this.sumnumdigits = 0;

            reportsets = new Reportsets(this);
            Reportsets.Reportset reportset = reportsets.Add(
                id
                // , (FileType)Enum.Parse(typeof(FileType), request.OutputFileTypeCode.ToString())
                , FileType.Excel
                , null
                , reportPrefix
                , (PowerPoint.PpSaveAsFileType)0
                , false
                , ""                                            // 調査目的
                , ""                     // 調査方法
                , ""                       // 調査時期
                , ""                     // 割付有効回答数
                , companyName                                   // 調査実施機関
            );

            int i = 0;
            foreach (string tsvPath in tsvPaths)
            {
                Outputs.OutputGT outputGT = (reportset.Outputs as Outputs).Add(
                ++i, tsvPath, xlbooknameprefix, tabletype, tableorientation, pagesetuptabletype, papersize, paperorientation,
                markingtype, minsamplescountonmarking, significancetestlevel);
                outputGT.ShowNACode = ShowNACode;       // 無回答表示コード
                outputGT.ShowIVCode = ShowIVCode;        // 非該当表示コード
                outputGT.FilteringExpression = FilteringExpression;  // 絞込み条件                                                                  // WB集計表示コード
                outputGT.WBSettingCode = WBOn;     // WB設定コード
                                                   //outputGT.ShowNAAtAxis = false;
                outputGT.PreWbBase = PreWbBase;
            }

        }

        public void MakeDPCheckListRequest(
        decimal id, List<string> tsvPaths, string title, string companyName, ZeroNAIVShowCode zeroshowcd,
        bool mergeaxis, string reportPrefix, string xlbooknameprefix, int TotalCount
        )
        {
            // レコード走査
            this.id = id;
            this.qcwebid = id;
            this.title = title;
            this.mergeaxis = mergeaxis;
            this.zeroshowcd = zeroshowcd;
            this.showncd = NumericContentsCode.All;

            reportsets = new Reportsets(this);
            Reportsets.Reportset reportset = reportsets.Add(
                id
                // , (FileType)Enum.Parse(typeof(FileType), request.OutputFileTypeCode.ToString())
                , FileType.Excel
                , null
                , reportPrefix
                , (PowerPoint.PpSaveAsFileType)0
                , false
                , ""                                            // 調査目的
                , ""                     // 調査方法
                , ""                       // 調査時期
                , ""                     // 割付有効回答数
                , companyName                                   // 調査実施機関
            );

            int i = 0;
            foreach (string tsvPath in tsvPaths)
            {
                #region チェックリスト
                Outputs.OutputCheckList checkList = (reportset.Outputs as Outputs).Add(
                    ++i
                    , tsvPath
                    , xlbooknameprefix
                    , TotalCount
                );
                #endregion
            }

        }
    }
}
