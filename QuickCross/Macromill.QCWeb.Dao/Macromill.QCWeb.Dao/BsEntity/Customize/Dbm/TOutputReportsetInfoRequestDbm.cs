
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class TOutputReportsetInfoRequestDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputReportsetInfoRequest);

        private static readonly TOutputReportsetInfoRequestDbm _instance = new TOutputReportsetInfoRequestDbm();
        private TOutputReportsetInfoRequestDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputReportsetInfoRequestDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "TOutputReportsetInfoRequest"; } }
        public override String TablePropertyName { get { return "TOutputReportsetInfoRequest"; } }
        public override String TableSqlName { get { return "TOutputReportsetInfoRequest"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputRequestId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnRequestServerCode;
        protected ColumnInfo _columnDownloadPath;
        protected ColumnInfo _columnProcServerCode;
        protected ColumnInfo _columnViewSurveyName;
        protected ColumnInfo _columnExcelbookType;
        protected ColumnInfo _columnNumericAnswerViewCode;
        protected ColumnInfo _columnDpTotal;
        protected ColumnInfo _columnDpAverage;
        protected ColumnInfo _columnDpStandardDiv;
        protected ColumnInfo _columnDpMin;
        protected ColumnInfo _columnDpMax;
        protected ColumnInfo _columnDpMedian;
        protected ColumnInfo _columnDpWeight;
        protected ColumnInfo _columnDpWeightavr;
        protected ColumnInfo _columnLanguage;
        protected ColumnInfo _columnShowZeroNaIvCode;
        protected ColumnInfo _columnMergeAxisCellsFlag;
        protected ColumnInfo _columnProcWeight;
        protected ColumnInfo _columnOutputReportsetInfoId;
        protected ColumnInfo _columnOutputFileTypeCode;
        protected ColumnInfo _columnReportFilenNamePrefix;
        protected ColumnInfo _columnCommentOutputFlag;
        protected ColumnInfo _columnPowerpointType;
        protected ColumnInfo _columnOutputTemplateId;
        protected ColumnInfo _columnUploadPath;
        protected ColumnInfo _columnPath;
        protected ColumnInfo _columnOutputCommonId;
        protected ColumnInfo _columnOutputType;
        protected ColumnInfo _columnTsvFilePath;
        protected ColumnInfo _columnExcelbookNamePrefix;
        protected ColumnInfo _columnWbSettingCode;
        protected ColumnInfo _columnNoanswerVisibleCode;
        protected ColumnInfo _columnUnmatchVisibleCode;
        protected ColumnInfo _columnUtf8Flag;

        public ColumnInfo ColumnOutputRequestId { get { return _columnOutputRequestId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnRequestServerCode { get { return _columnRequestServerCode; } }
        public ColumnInfo ColumnDownloadPath { get { return _columnDownloadPath; } }
        public ColumnInfo ColumnProcServerCode { get { return _columnProcServerCode; } }
        public ColumnInfo ColumnViewSurveyName { get { return _columnViewSurveyName; } }
        public ColumnInfo ColumnExcelbookType { get { return _columnExcelbookType; } }
        public ColumnInfo ColumnNumericAnswerViewCode { get { return _columnNumericAnswerViewCode; } }
        public ColumnInfo ColumnDpTotal { get { return _columnDpTotal; } }
        public ColumnInfo ColumnDpAverage { get { return _columnDpAverage; } }
        public ColumnInfo ColumnDpStandardDiv { get { return _columnDpStandardDiv; } }
        public ColumnInfo ColumnDpMin { get { return _columnDpMin; } }
        public ColumnInfo ColumnDpMax { get { return _columnDpMax; } }
        public ColumnInfo ColumnDpMedian { get { return _columnDpMedian; } }
        public ColumnInfo ColumnDpWeight { get { return _columnDpWeight; } }
        public ColumnInfo ColumnDpWeightavr { get { return _columnDpWeightavr; } }
        public ColumnInfo ColumnLanguage { get { return _columnLanguage; } }
        public ColumnInfo ColumnShowZeroNaIvCode { get { return _columnShowZeroNaIvCode; } }
        public ColumnInfo ColumnMergeAxisCellsFlag { get { return _columnMergeAxisCellsFlag; } }
        public ColumnInfo ColumnProcWeight { get { return _columnProcWeight; } }
        public ColumnInfo ColumnOutputReportsetInfoId { get { return _columnOutputReportsetInfoId; } }
        public ColumnInfo ColumnOutputFileTypeCode { get { return _columnOutputFileTypeCode; } }
        public ColumnInfo ColumnReportFilenNamePrefix { get { return _columnReportFilenNamePrefix; } }
        public ColumnInfo ColumnCommentOutputFlag { get { return _columnCommentOutputFlag; } }
        public ColumnInfo ColumnPowerpointType { get { return _columnPowerpointType; } }
        public ColumnInfo ColumnOutputTemplateId { get { return _columnOutputTemplateId; } }
        public ColumnInfo ColumnUploadPath { get { return _columnUploadPath; } }
        public ColumnInfo ColumnPath { get { return _columnPath; } }
        public ColumnInfo ColumnOutputCommonId { get { return _columnOutputCommonId; } }
        public ColumnInfo ColumnOutputType { get { return _columnOutputType; } }
        public ColumnInfo ColumnTsvFilePath { get { return _columnTsvFilePath; } }
        public ColumnInfo ColumnExcelbookNamePrefix { get { return _columnExcelbookNamePrefix; } }
        public ColumnInfo ColumnWbSettingCode { get { return _columnWbSettingCode; } }
        public ColumnInfo ColumnNoanswerVisibleCode { get { return _columnNoanswerVisibleCode; } }
        public ColumnInfo ColumnUnmatchVisibleCode { get { return _columnUnmatchVisibleCode; } }
        public ColumnInfo ColumnUtf8Flag { get { return _columnUtf8Flag; } }

        protected void InitializeColumnInfo() {
            _columnOutputRequestId = cci("OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", null, null, false, "OutputRequestId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, false, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRequestServerCode = cci("REQUEST_SERVER_CODE", "REQUEST_SERVER_CODE", null, null, false, "RequestServerCode", typeof(String), false, "VARCHAR2", 24, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDownloadPath = cci("DOWNLOAD_PATH", "DOWNLOAD_PATH", null, null, false, "DownloadPath", typeof(String), false, "VARCHAR2", 260, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnProcServerCode = cci("PROC_SERVER_CODE", "PROC_SERVER_CODE", null, null, false, "ProcServerCode", typeof(String), false, "VARCHAR2", 24, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnViewSurveyName = cci("VIEW_SURVEY_NAME", "VIEW_SURVEY_NAME", null, null, false, "ViewSurveyName", typeof(String), false, "VARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExcelbookType = cci("EXCELBOOK_TYPE", "EXCELBOOK_TYPE", null, null, false, "ExcelbookType", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNumericAnswerViewCode = cci("NUMERIC_ANSWER_VIEW_CODE", "NUMERIC_ANSWER_VIEW_CODE", null, null, false, "NumericAnswerViewCode", typeof(int?), false, "NUMBER", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpTotal = cci("DP_TOTAL", "DP_TOTAL", null, null, false, "DpTotal", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpAverage = cci("DP_AVERAGE", "DP_AVERAGE", null, null, false, "DpAverage", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpStandardDiv = cci("DP_STANDARD_DIV", "DP_STANDARD_DIV", null, null, false, "DpStandardDiv", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpMin = cci("DP_MIN", "DP_MIN", null, null, false, "DpMin", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpMax = cci("DP_MAX", "DP_MAX", null, null, false, "DpMax", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpMedian = cci("DP_MEDIAN", "DP_MEDIAN", null, null, false, "DpMedian", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpWeight = cci("DP_WEIGHT", "DP_WEIGHT", null, null, false, "DpWeight", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpWeightavr = cci("DP_WEIGHTAVR", "DP_WEIGHTAVR", null, null, false, "DpWeightavr", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLanguage = cci("LANGUAGE", "LANGUAGE", null, null, false, "Language", typeof(String), false, "VARCHAR2", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnShowZeroNaIvCode = cci("SHOW_ZERO_NA_IV_CODE", "SHOW_ZERO_NA_IV_CODE", null, null, false, "ShowZeroNaIvCode", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMergeAxisCellsFlag = cci("MERGE_AXIS_CELLS_FLAG", "MERGE_AXIS_CELLS_FLAG", null, null, false, "MergeAxisCellsFlag", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnProcWeight = cci("PROC_WEIGHT", "PROC_WEIGHT", null, null, false, "ProcWeight", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputReportsetInfoId = cci("OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", null, null, false, "OutputReportsetInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputFileTypeCode = cci("OUTPUT_FILE_TYPE_CODE", "OUTPUT_FILE_TYPE_CODE", null, null, false, "OutputFileTypeCode", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnReportFilenNamePrefix = cci("REPORT_FILEN_NAME_PREFIX", "REPORT_FILEN_NAME_PREFIX", null, null, false, "ReportFilenNamePrefix", typeof(String), false, "VARCHAR2", 100, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCommentOutputFlag = cci("COMMENT_OUTPUT_FLAG", "COMMENT_OUTPUT_FLAG", null, null, false, "CommentOutputFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPowerpointType = cci("POWERPOINT_TYPE", "POWERPOINT_TYPE", null, null, false, "PowerpointType", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputTemplateId = cci("OUTPUT_TEMPLATE_ID", "OUTPUT_TEMPLATE_ID", null, null, false, "OutputTemplateId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUploadPath = cci("UPLOAD_PATH", "UPLOAD_PATH", null, null, false, "UploadPath", typeof(String), false, "VARCHAR2", 780, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPath = cci("PATH", "PATH", null, null, false, "Path", typeof(String), false, "VARCHAR2", 780, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputCommonId = cci("OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", null, null, false, "OutputCommonId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputType = cci("OUTPUT_TYPE", "OUTPUT_TYPE", null, null, false, "OutputType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTsvFilePath = cci("TSV_FILE_PATH", "TSV_FILE_PATH", null, null, false, "TsvFilePath", typeof(String), false, "CLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExcelbookNamePrefix = cci("EXCELBOOK_NAME_PREFIX", "EXCELBOOK_NAME_PREFIX", null, null, false, "ExcelbookNamePrefix", typeof(String), false, "VARCHAR2", 100, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWbSettingCode = cci("WB_SETTING_CODE", "WB_SETTING_CODE", null, null, false, "WbSettingCode", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNoanswerVisibleCode = cci("NOANSWER_VISIBLE_CODE", "NOANSWER_VISIBLE_CODE", null, null, false, "NoanswerVisibleCode", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUnmatchVisibleCode = cci("UNMATCH_VISIBLE_CODE", "UNMATCH_VISIBLE_CODE", null, null, false, "UnmatchVisibleCode", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUtf8Flag = cci("UTF8_FLAG", "UTF8_FLAG", null, null, false, "Utf8Flag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputRequestId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnRequestServerCode);
            _columnInfoList.add(ColumnDownloadPath);
            _columnInfoList.add(ColumnProcServerCode);
            _columnInfoList.add(ColumnViewSurveyName);
            _columnInfoList.add(ColumnExcelbookType);
            _columnInfoList.add(ColumnNumericAnswerViewCode);
            _columnInfoList.add(ColumnDpTotal);
            _columnInfoList.add(ColumnDpAverage);
            _columnInfoList.add(ColumnDpStandardDiv);
            _columnInfoList.add(ColumnDpMin);
            _columnInfoList.add(ColumnDpMax);
            _columnInfoList.add(ColumnDpMedian);
            _columnInfoList.add(ColumnDpWeight);
            _columnInfoList.add(ColumnDpWeightavr);
            _columnInfoList.add(ColumnLanguage);
            _columnInfoList.add(ColumnShowZeroNaIvCode);
            _columnInfoList.add(ColumnMergeAxisCellsFlag);
            _columnInfoList.add(ColumnProcWeight);
            _columnInfoList.add(ColumnOutputReportsetInfoId);
            _columnInfoList.add(ColumnOutputFileTypeCode);
            _columnInfoList.add(ColumnReportFilenNamePrefix);
            _columnInfoList.add(ColumnCommentOutputFlag);
            _columnInfoList.add(ColumnPowerpointType);
            _columnInfoList.add(ColumnOutputTemplateId);
            _columnInfoList.add(ColumnUploadPath);
            _columnInfoList.add(ColumnPath);
            _columnInfoList.add(ColumnOutputCommonId);
            _columnInfoList.add(ColumnOutputType);
            _columnInfoList.add(ColumnTsvFilePath);
            _columnInfoList.add(ColumnExcelbookNamePrefix);
            _columnInfoList.add(ColumnWbSettingCode);
            _columnInfoList.add(ColumnNoanswerVisibleCode);
            _columnInfoList.add(ColumnUnmatchVisibleCode);
            _columnInfoList.add(ColumnUtf8Flag);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            throw new NotSupportedException("The table does not have primary key: " + TableDbName);
        }}

        // -------------------------------------------------
        //                                   Primary Element
        //                                   ---------------
        public override bool HasPrimaryKey { get { return false; } }
        public override bool HasCompoundPrimaryKey { get { return false; } }

        // ===============================================================================
        //                                                                   Relation Info
        //                                                                   =============
        // -------------------------------------------------
        //                                   Foreign Element
        //                                   ---------------


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasCommonColumn { get { return false; } }

        // ===============================================================================
        //                                                                 Name Definition
        //                                                                 ===============
        #region Name

        // -------------------------------------------------
        //                                             Table
        //                                             -----
        public static readonly String TABLE_DB_NAME = "TOutputReportsetInfoRequest";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputReportsetInfoRequest";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_REQUEST_ID = "OUTPUT_REQUEST_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_REQUEST_SERVER_CODE = "REQUEST_SERVER_CODE";
        public static readonly String DB_NAME_DOWNLOAD_PATH = "DOWNLOAD_PATH";
        public static readonly String DB_NAME_PROC_SERVER_CODE = "PROC_SERVER_CODE";
        public static readonly String DB_NAME_VIEW_SURVEY_NAME = "VIEW_SURVEY_NAME";
        public static readonly String DB_NAME_EXCELBOOK_TYPE = "EXCELBOOK_TYPE";
        public static readonly String DB_NAME_NUMERIC_ANSWER_VIEW_CODE = "NUMERIC_ANSWER_VIEW_CODE";
        public static readonly String DB_NAME_DP_TOTAL = "DP_TOTAL";
        public static readonly String DB_NAME_DP_AVERAGE = "DP_AVERAGE";
        public static readonly String DB_NAME_DP_STANDARD_DIV = "DP_STANDARD_DIV";
        public static readonly String DB_NAME_DP_MIN = "DP_MIN";
        public static readonly String DB_NAME_DP_MAX = "DP_MAX";
        public static readonly String DB_NAME_DP_MEDIAN = "DP_MEDIAN";
        public static readonly String DB_NAME_DP_WEIGHT = "DP_WEIGHT";
        public static readonly String DB_NAME_DP_WEIGHTAVR = "DP_WEIGHTAVR";
        public static readonly String DB_NAME_LANGUAGE = "LANGUAGE";
        public static readonly String DB_NAME_SHOW_ZERO_NA_IV_CODE = "SHOW_ZERO_NA_IV_CODE";
        public static readonly String DB_NAME_MERGE_AXIS_CELLS_FLAG = "MERGE_AXIS_CELLS_FLAG";
        public static readonly String DB_NAME_PROC_WEIGHT = "PROC_WEIGHT";
        public static readonly String DB_NAME_OUTPUT_REPORTSET_INFO_ID = "OUTPUT_REPORTSET_INFO_ID";
        public static readonly String DB_NAME_OUTPUT_FILE_TYPE_CODE = "OUTPUT_FILE_TYPE_CODE";
        public static readonly String DB_NAME_REPORT_FILEN_NAME_PREFIX = "REPORT_FILEN_NAME_PREFIX";
        public static readonly String DB_NAME_COMMENT_OUTPUT_FLAG = "COMMENT_OUTPUT_FLAG";
        public static readonly String DB_NAME_POWERPOINT_TYPE = "POWERPOINT_TYPE";
        public static readonly String DB_NAME_OUTPUT_TEMPLATE_ID = "OUTPUT_TEMPLATE_ID";
        public static readonly String DB_NAME_UPLOAD_PATH = "UPLOAD_PATH";
        public static readonly String DB_NAME_PATH = "PATH";
        public static readonly String DB_NAME_OUTPUT_COMMON_ID = "OUTPUT_COMMON_ID";
        public static readonly String DB_NAME_OUTPUT_TYPE = "OUTPUT_TYPE";
        public static readonly String DB_NAME_TSV_FILE_PATH = "TSV_FILE_PATH";
        public static readonly String DB_NAME_EXCELBOOK_NAME_PREFIX = "EXCELBOOK_NAME_PREFIX";
        public static readonly String DB_NAME_WB_SETTING_CODE = "WB_SETTING_CODE";
        public static readonly String DB_NAME_NOANSWER_VISIBLE_CODE = "NOANSWER_VISIBLE_CODE";
        public static readonly String DB_NAME_UNMATCH_VISIBLE_CODE = "UNMATCH_VISIBLE_CODE";
        public static readonly String DB_NAME_UTF8_FLAG = "UTF8_FLAG";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_REQUEST_ID = "OutputRequestId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_REQUEST_SERVER_CODE = "RequestServerCode";
        public static readonly String PROPERTY_NAME_DOWNLOAD_PATH = "DownloadPath";
        public static readonly String PROPERTY_NAME_PROC_SERVER_CODE = "ProcServerCode";
        public static readonly String PROPERTY_NAME_VIEW_SURVEY_NAME = "ViewSurveyName";
        public static readonly String PROPERTY_NAME_EXCELBOOK_TYPE = "ExcelbookType";
        public static readonly String PROPERTY_NAME_NUMERIC_ANSWER_VIEW_CODE = "NumericAnswerViewCode";
        public static readonly String PROPERTY_NAME_DP_TOTAL = "DpTotal";
        public static readonly String PROPERTY_NAME_DP_AVERAGE = "DpAverage";
        public static readonly String PROPERTY_NAME_DP_STANDARD_DIV = "DpStandardDiv";
        public static readonly String PROPERTY_NAME_DP_MIN = "DpMin";
        public static readonly String PROPERTY_NAME_DP_MAX = "DpMax";
        public static readonly String PROPERTY_NAME_DP_MEDIAN = "DpMedian";
        public static readonly String PROPERTY_NAME_DP_WEIGHT = "DpWeight";
        public static readonly String PROPERTY_NAME_DP_WEIGHTAVR = "DpWeightavr";
        public static readonly String PROPERTY_NAME_LANGUAGE = "Language";
        public static readonly String PROPERTY_NAME_SHOW_ZERO_NA_IV_CODE = "ShowZeroNaIvCode";
        public static readonly String PROPERTY_NAME_MERGE_AXIS_CELLS_FLAG = "MergeAxisCellsFlag";
        public static readonly String PROPERTY_NAME_PROC_WEIGHT = "ProcWeight";
        public static readonly String PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID = "OutputReportsetInfoId";
        public static readonly String PROPERTY_NAME_OUTPUT_FILE_TYPE_CODE = "OutputFileTypeCode";
        public static readonly String PROPERTY_NAME_REPORT_FILEN_NAME_PREFIX = "ReportFilenNamePrefix";
        public static readonly String PROPERTY_NAME_COMMENT_OUTPUT_FLAG = "CommentOutputFlag";
        public static readonly String PROPERTY_NAME_POWERPOINT_TYPE = "PowerpointType";
        public static readonly String PROPERTY_NAME_OUTPUT_TEMPLATE_ID = "OutputTemplateId";
        public static readonly String PROPERTY_NAME_UPLOAD_PATH = "UploadPath";
        public static readonly String PROPERTY_NAME_PATH = "Path";
        public static readonly String PROPERTY_NAME_OUTPUT_COMMON_ID = "OutputCommonId";
        public static readonly String PROPERTY_NAME_OUTPUT_TYPE = "OutputType";
        public static readonly String PROPERTY_NAME_TSV_FILE_PATH = "TsvFilePath";
        public static readonly String PROPERTY_NAME_EXCELBOOK_NAME_PREFIX = "ExcelbookNamePrefix";
        public static readonly String PROPERTY_NAME_WB_SETTING_CODE = "WbSettingCode";
        public static readonly String PROPERTY_NAME_NOANSWER_VISIBLE_CODE = "NoanswerVisibleCode";
        public static readonly String PROPERTY_NAME_UNMATCH_VISIBLE_CODE = "UnmatchVisibleCode";
        public static readonly String PROPERTY_NAME_UTF8_FLAG = "Utf8Flag";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputReportsetInfoRequestDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_REQUEST_ID.ToLower(), PROPERTY_NAME_OUTPUT_REQUEST_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_REQUEST_SERVER_CODE.ToLower(), PROPERTY_NAME_REQUEST_SERVER_CODE);
                map.put(DB_NAME_DOWNLOAD_PATH.ToLower(), PROPERTY_NAME_DOWNLOAD_PATH);
                map.put(DB_NAME_PROC_SERVER_CODE.ToLower(), PROPERTY_NAME_PROC_SERVER_CODE);
                map.put(DB_NAME_VIEW_SURVEY_NAME.ToLower(), PROPERTY_NAME_VIEW_SURVEY_NAME);
                map.put(DB_NAME_EXCELBOOK_TYPE.ToLower(), PROPERTY_NAME_EXCELBOOK_TYPE);
                map.put(DB_NAME_NUMERIC_ANSWER_VIEW_CODE.ToLower(), PROPERTY_NAME_NUMERIC_ANSWER_VIEW_CODE);
                map.put(DB_NAME_DP_TOTAL.ToLower(), PROPERTY_NAME_DP_TOTAL);
                map.put(DB_NAME_DP_AVERAGE.ToLower(), PROPERTY_NAME_DP_AVERAGE);
                map.put(DB_NAME_DP_STANDARD_DIV.ToLower(), PROPERTY_NAME_DP_STANDARD_DIV);
                map.put(DB_NAME_DP_MIN.ToLower(), PROPERTY_NAME_DP_MIN);
                map.put(DB_NAME_DP_MAX.ToLower(), PROPERTY_NAME_DP_MAX);
                map.put(DB_NAME_DP_MEDIAN.ToLower(), PROPERTY_NAME_DP_MEDIAN);
                map.put(DB_NAME_DP_WEIGHT.ToLower(), PROPERTY_NAME_DP_WEIGHT);
                map.put(DB_NAME_DP_WEIGHTAVR.ToLower(), PROPERTY_NAME_DP_WEIGHTAVR);
                map.put(DB_NAME_LANGUAGE.ToLower(), PROPERTY_NAME_LANGUAGE);
                map.put(DB_NAME_SHOW_ZERO_NA_IV_CODE.ToLower(), PROPERTY_NAME_SHOW_ZERO_NA_IV_CODE);
                map.put(DB_NAME_MERGE_AXIS_CELLS_FLAG.ToLower(), PROPERTY_NAME_MERGE_AXIS_CELLS_FLAG);
                map.put(DB_NAME_PROC_WEIGHT.ToLower(), PROPERTY_NAME_PROC_WEIGHT);
                map.put(DB_NAME_OUTPUT_REPORTSET_INFO_ID.ToLower(), PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID);
                map.put(DB_NAME_OUTPUT_FILE_TYPE_CODE.ToLower(), PROPERTY_NAME_OUTPUT_FILE_TYPE_CODE);
                map.put(DB_NAME_REPORT_FILEN_NAME_PREFIX.ToLower(), PROPERTY_NAME_REPORT_FILEN_NAME_PREFIX);
                map.put(DB_NAME_COMMENT_OUTPUT_FLAG.ToLower(), PROPERTY_NAME_COMMENT_OUTPUT_FLAG);
                map.put(DB_NAME_POWERPOINT_TYPE.ToLower(), PROPERTY_NAME_POWERPOINT_TYPE);
                map.put(DB_NAME_OUTPUT_TEMPLATE_ID.ToLower(), PROPERTY_NAME_OUTPUT_TEMPLATE_ID);
                map.put(DB_NAME_UPLOAD_PATH.ToLower(), PROPERTY_NAME_UPLOAD_PATH);
                map.put(DB_NAME_PATH.ToLower(), PROPERTY_NAME_PATH);
                map.put(DB_NAME_OUTPUT_COMMON_ID.ToLower(), PROPERTY_NAME_OUTPUT_COMMON_ID);
                map.put(DB_NAME_OUTPUT_TYPE.ToLower(), PROPERTY_NAME_OUTPUT_TYPE);
                map.put(DB_NAME_TSV_FILE_PATH.ToLower(), PROPERTY_NAME_TSV_FILE_PATH);
                map.put(DB_NAME_EXCELBOOK_NAME_PREFIX.ToLower(), PROPERTY_NAME_EXCELBOOK_NAME_PREFIX);
                map.put(DB_NAME_WB_SETTING_CODE.ToLower(), PROPERTY_NAME_WB_SETTING_CODE);
                map.put(DB_NAME_NOANSWER_VISIBLE_CODE.ToLower(), PROPERTY_NAME_NOANSWER_VISIBLE_CODE);
                map.put(DB_NAME_UNMATCH_VISIBLE_CODE.ToLower(), PROPERTY_NAME_UNMATCH_VISIBLE_CODE);
                map.put(DB_NAME_UTF8_FLAG.ToLower(), PROPERTY_NAME_UTF8_FLAG);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_REQUEST_ID.ToLower(), DB_NAME_OUTPUT_REQUEST_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_REQUEST_SERVER_CODE.ToLower(), DB_NAME_REQUEST_SERVER_CODE);
                map.put(PROPERTY_NAME_DOWNLOAD_PATH.ToLower(), DB_NAME_DOWNLOAD_PATH);
                map.put(PROPERTY_NAME_PROC_SERVER_CODE.ToLower(), DB_NAME_PROC_SERVER_CODE);
                map.put(PROPERTY_NAME_VIEW_SURVEY_NAME.ToLower(), DB_NAME_VIEW_SURVEY_NAME);
                map.put(PROPERTY_NAME_EXCELBOOK_TYPE.ToLower(), DB_NAME_EXCELBOOK_TYPE);
                map.put(PROPERTY_NAME_NUMERIC_ANSWER_VIEW_CODE.ToLower(), DB_NAME_NUMERIC_ANSWER_VIEW_CODE);
                map.put(PROPERTY_NAME_DP_TOTAL.ToLower(), DB_NAME_DP_TOTAL);
                map.put(PROPERTY_NAME_DP_AVERAGE.ToLower(), DB_NAME_DP_AVERAGE);
                map.put(PROPERTY_NAME_DP_STANDARD_DIV.ToLower(), DB_NAME_DP_STANDARD_DIV);
                map.put(PROPERTY_NAME_DP_MIN.ToLower(), DB_NAME_DP_MIN);
                map.put(PROPERTY_NAME_DP_MAX.ToLower(), DB_NAME_DP_MAX);
                map.put(PROPERTY_NAME_DP_MEDIAN.ToLower(), DB_NAME_DP_MEDIAN);
                map.put(PROPERTY_NAME_DP_WEIGHT.ToLower(), DB_NAME_DP_WEIGHT);
                map.put(PROPERTY_NAME_DP_WEIGHTAVR.ToLower(), DB_NAME_DP_WEIGHTAVR);
                map.put(PROPERTY_NAME_LANGUAGE.ToLower(), DB_NAME_LANGUAGE);
                map.put(PROPERTY_NAME_SHOW_ZERO_NA_IV_CODE.ToLower(), DB_NAME_SHOW_ZERO_NA_IV_CODE);
                map.put(PROPERTY_NAME_MERGE_AXIS_CELLS_FLAG.ToLower(), DB_NAME_MERGE_AXIS_CELLS_FLAG);
                map.put(PROPERTY_NAME_PROC_WEIGHT.ToLower(), DB_NAME_PROC_WEIGHT);
                map.put(PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID.ToLower(), DB_NAME_OUTPUT_REPORTSET_INFO_ID);
                map.put(PROPERTY_NAME_OUTPUT_FILE_TYPE_CODE.ToLower(), DB_NAME_OUTPUT_FILE_TYPE_CODE);
                map.put(PROPERTY_NAME_REPORT_FILEN_NAME_PREFIX.ToLower(), DB_NAME_REPORT_FILEN_NAME_PREFIX);
                map.put(PROPERTY_NAME_COMMENT_OUTPUT_FLAG.ToLower(), DB_NAME_COMMENT_OUTPUT_FLAG);
                map.put(PROPERTY_NAME_POWERPOINT_TYPE.ToLower(), DB_NAME_POWERPOINT_TYPE);
                map.put(PROPERTY_NAME_OUTPUT_TEMPLATE_ID.ToLower(), DB_NAME_OUTPUT_TEMPLATE_ID);
                map.put(PROPERTY_NAME_UPLOAD_PATH.ToLower(), DB_NAME_UPLOAD_PATH);
                map.put(PROPERTY_NAME_PATH.ToLower(), DB_NAME_PATH);
                map.put(PROPERTY_NAME_OUTPUT_COMMON_ID.ToLower(), DB_NAME_OUTPUT_COMMON_ID);
                map.put(PROPERTY_NAME_OUTPUT_TYPE.ToLower(), DB_NAME_OUTPUT_TYPE);
                map.put(PROPERTY_NAME_TSV_FILE_PATH.ToLower(), DB_NAME_TSV_FILE_PATH);
                map.put(PROPERTY_NAME_EXCELBOOK_NAME_PREFIX.ToLower(), DB_NAME_EXCELBOOK_NAME_PREFIX);
                map.put(PROPERTY_NAME_WB_SETTING_CODE.ToLower(), DB_NAME_WB_SETTING_CODE);
                map.put(PROPERTY_NAME_NOANSWER_VISIBLE_CODE.ToLower(), DB_NAME_NOANSWER_VISIBLE_CODE);
                map.put(PROPERTY_NAME_UNMATCH_VISIBLE_CODE.ToLower(), DB_NAME_UNMATCH_VISIBLE_CODE);
                map.put(PROPERTY_NAME_UTF8_FLAG.ToLower(), DB_NAME_UTF8_FLAG);
                _propertyNameDbNameKeyToLowerMap = map;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                        Name Map
        //                                                                        ========
        #region Name Map
        public override Map<String, String> DbNamePropertyNameKeyToLowerMap { get { return _dbNamePropertyNameKeyToLowerMap; } }
        public override Map<String, String> PropertyNameDbNameKeyToLowerMap { get { return _propertyNameDbNameKeyToLowerMap; } }
        #endregion

        // ===============================================================================
        //                                                                       Type Name
        //                                                                       =========
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.TOutputReportsetInfoRequest"; } }
        public override String DaoTypeName { get { return null; } }
        public override String ConditionBeanTypeName { get { return null; } }
        public override String BehaviorTypeName { get { return null; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputReportsetInfoRequest NewMyEntity() { return new TOutputReportsetInfoRequest(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputReportsetInfoRequest>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputReportsetInfoRequest>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_REQUEST_ID", "OutputRequestId", new EntityPropertyOutputRequestIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REQUEST_SERVER_CODE", "RequestServerCode", new EntityPropertyRequestServerCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DOWNLOAD_PATH", "DownloadPath", new EntityPropertyDownloadPathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PROC_SERVER_CODE", "ProcServerCode", new EntityPropertyProcServerCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VIEW_SURVEY_NAME", "ViewSurveyName", new EntityPropertyViewSurveyNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXCELBOOK_TYPE", "ExcelbookType", new EntityPropertyExcelbookTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NUMERIC_ANSWER_VIEW_CODE", "NumericAnswerViewCode", new EntityPropertyNumericAnswerViewCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_TOTAL", "DpTotal", new EntityPropertyDpTotalSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_AVERAGE", "DpAverage", new EntityPropertyDpAverageSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_STANDARD_DIV", "DpStandardDiv", new EntityPropertyDpStandardDivSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_MIN", "DpMin", new EntityPropertyDpMinSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_MAX", "DpMax", new EntityPropertyDpMaxSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_MEDIAN", "DpMedian", new EntityPropertyDpMedianSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_WEIGHT", "DpWeight", new EntityPropertyDpWeightSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_WEIGHTAVR", "DpWeightavr", new EntityPropertyDpWeightavrSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LANGUAGE", "Language", new EntityPropertyLanguageSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SHOW_ZERO_NA_IV_CODE", "ShowZeroNaIvCode", new EntityPropertyShowZeroNaIvCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MERGE_AXIS_CELLS_FLAG", "MergeAxisCellsFlag", new EntityPropertyMergeAxisCellsFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PROC_WEIGHT", "ProcWeight", new EntityPropertyProcWeightSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_REPORTSET_INFO_ID", "OutputReportsetInfoId", new EntityPropertyOutputReportsetInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_FILE_TYPE_CODE", "OutputFileTypeCode", new EntityPropertyOutputFileTypeCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REPORT_FILEN_NAME_PREFIX", "ReportFilenNamePrefix", new EntityPropertyReportFilenNamePrefixSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COMMENT_OUTPUT_FLAG", "CommentOutputFlag", new EntityPropertyCommentOutputFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("POWERPOINT_TYPE", "PowerpointType", new EntityPropertyPowerpointTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TEMPLATE_ID", "OutputTemplateId", new EntityPropertyOutputTemplateIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UPLOAD_PATH", "UploadPath", new EntityPropertyUploadPathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PATH", "Path", new EntityPropertyPathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_COMMON_ID", "OutputCommonId", new EntityPropertyOutputCommonIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TYPE", "OutputType", new EntityPropertyOutputTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TSV_FILE_PATH", "TsvFilePath", new EntityPropertyTsvFilePathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXCELBOOK_NAME_PREFIX", "ExcelbookNamePrefix", new EntityPropertyExcelbookNamePrefixSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WB_SETTING_CODE", "WbSettingCode", new EntityPropertyWbSettingCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NOANSWER_VISIBLE_CODE", "NoanswerVisibleCode", new EntityPropertyNoanswerVisibleCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UNMATCH_VISIBLE_CODE", "UnmatchVisibleCode", new EntityPropertyUnmatchVisibleCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UTF8_FLAG", "Utf8Flag", new EntityPropertyUtf8FlagSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputReportsetInfoRequest> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputReportsetInfoRequest)entity, value);
        }

        public class EntityPropertyOutputRequestIdSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.OutputRequestId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyRequestServerCodeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.RequestServerCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDownloadPathSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.DownloadPath = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyProcServerCodeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.ProcServerCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyViewSurveyNameSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.ViewSurveyName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyExcelbookTypeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.ExcelbookType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNumericAnswerViewCodeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.NumericAnswerViewCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpTotalSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.DpTotal = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpAverageSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.DpAverage = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpStandardDivSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.DpStandardDiv = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpMinSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.DpMin = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpMaxSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.DpMax = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpMedianSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.DpMedian = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpWeightSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.DpWeight = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpWeightavrSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.DpWeightavr = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLanguageSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.Language = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyShowZeroNaIvCodeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.ShowZeroNaIvCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMergeAxisCellsFlagSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.MergeAxisCellsFlag = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyProcWeightSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.ProcWeight = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputReportsetInfoIdSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.OutputReportsetInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputFileTypeCodeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.OutputFileTypeCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyReportFilenNamePrefixSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.ReportFilenNamePrefix = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCommentOutputFlagSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.CommentOutputFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPowerpointTypeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.PowerpointType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputTemplateIdSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.OutputTemplateId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyUploadPathSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.UploadPath = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyPathSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.Path = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOutputCommonIdSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.OutputCommonId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputTypeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.OutputType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTsvFilePathSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.TsvFilePath = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyExcelbookNamePrefixSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.ExcelbookNamePrefix = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyWbSettingCodeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.WbSettingCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNoanswerVisibleCodeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.NoanswerVisibleCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyUnmatchVisibleCodeSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.UnmatchVisibleCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyUtf8FlagSetupper : EntityPropertySetupper<TOutputReportsetInfoRequest> {
            public void Setup(TOutputReportsetInfoRequest entity, Object value) { entity.Utf8Flag = (value != null) ? (int?)value : null; }
        }
    }
}
