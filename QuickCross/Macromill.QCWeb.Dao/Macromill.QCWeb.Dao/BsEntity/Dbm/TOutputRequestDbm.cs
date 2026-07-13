
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity;

using Macromill.QCWeb.Dao.ExDao;
using Macromill.QCWeb.Dao.CBean;

namespace Macromill.QCWeb.Dao.BsEntity.Dbm {

    public class TOutputRequestDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputRequest);

        private static readonly TOutputRequestDbm _instance = new TOutputRequestDbm();
        private TOutputRequestDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputRequestDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_REQUEST"; } }
        public override String TablePropertyName { get { return "TOutputRequest"; } }
        public override String TableSqlName { get { return "T_OUTPUT_REQUEST"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputRequestId;
        protected ColumnInfo _columnRequestServerCode;
        protected ColumnInfo _columnRequestUserId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnLastDownloadUserid;
        protected ColumnInfo _columnRequestDatetime;
        protected ColumnInfo _columnDownloadPath;
        protected ColumnInfo _columnProcServerCode;
        protected ColumnInfo _columnStatusCode;
        protected ColumnInfo _columnDescription;
        protected ColumnInfo _columnEndDatetime;
        protected ColumnInfo _columnLastDownloadDatetime;
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
        protected ColumnInfo _columnProcWeight;
        protected ColumnInfo _columnOutputReportsetInfoId;
        protected ColumnInfo _columnDeleteFlag;
        protected ColumnInfo _columnViewSurveyName;
        protected ColumnInfo _columnLanguage;
        protected ColumnInfo _columnShowZeroNaIvCode;
        protected ColumnInfo _columnMergeAxisCellsFlag;
        protected ColumnInfo _columnScenarioName;
        protected ColumnInfo _columnStartDatetime;
        protected ColumnInfo _columnTestLogFlag;
        protected ColumnInfo _columnTsvFileSizeGt;
        protected ColumnInfo _columnTsvFileSizeCross;
        protected ColumnInfo _columnTsvFileSizeFa;
        protected ColumnInfo _columnTsvFileSizeDataOutput;

        public ColumnInfo ColumnOutputRequestId { get { return _columnOutputRequestId; } }
        public ColumnInfo ColumnRequestServerCode { get { return _columnRequestServerCode; } }
        public ColumnInfo ColumnRequestUserId { get { return _columnRequestUserId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnLastDownloadUserid { get { return _columnLastDownloadUserid; } }
        public ColumnInfo ColumnRequestDatetime { get { return _columnRequestDatetime; } }
        public ColumnInfo ColumnDownloadPath { get { return _columnDownloadPath; } }
        public ColumnInfo ColumnProcServerCode { get { return _columnProcServerCode; } }
        public ColumnInfo ColumnStatusCode { get { return _columnStatusCode; } }
        public ColumnInfo ColumnDescription { get { return _columnDescription; } }
        public ColumnInfo ColumnEndDatetime { get { return _columnEndDatetime; } }
        public ColumnInfo ColumnLastDownloadDatetime { get { return _columnLastDownloadDatetime; } }
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
        public ColumnInfo ColumnProcWeight { get { return _columnProcWeight; } }
        public ColumnInfo ColumnOutputReportsetInfoId { get { return _columnOutputReportsetInfoId; } }
        public ColumnInfo ColumnDeleteFlag { get { return _columnDeleteFlag; } }
        public ColumnInfo ColumnViewSurveyName { get { return _columnViewSurveyName; } }
        public ColumnInfo ColumnLanguage { get { return _columnLanguage; } }
        public ColumnInfo ColumnShowZeroNaIvCode { get { return _columnShowZeroNaIvCode; } }
        public ColumnInfo ColumnMergeAxisCellsFlag { get { return _columnMergeAxisCellsFlag; } }
        public ColumnInfo ColumnScenarioName { get { return _columnScenarioName; } }
        public ColumnInfo ColumnStartDatetime { get { return _columnStartDatetime; } }
        public ColumnInfo ColumnTestLogFlag { get { return _columnTestLogFlag; } }
        public ColumnInfo ColumnTsvFileSizeGt { get { return _columnTsvFileSizeGt; } }
        public ColumnInfo ColumnTsvFileSizeCross { get { return _columnTsvFileSizeCross; } }
        public ColumnInfo ColumnTsvFileSizeFa { get { return _columnTsvFileSizeFa; } }
        public ColumnInfo ColumnTsvFileSizeDataOutput { get { return _columnTsvFileSizeDataOutput; } }

        protected void InitializeColumnInfo() {
            _columnOutputRequestId = cci("OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", null, null, true, "OutputRequestId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputCommon", "TOutputCommonList");
            _columnRequestServerCode = cci("REQUEST_SERVER_CODE", "REQUEST_SERVER_CODE", null, null, true, "RequestServerCode", typeof(String), false, "VARCHAR2", 24, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRequestUserId = cci("REQUEST_USER_ID", "REQUEST_USER_ID", null, null, true, "RequestUserId", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo", null);
            _columnLastDownloadUserid = cci("LAST_DOWNLOAD_USERID", "LAST_DOWNLOAD_USERID", null, null, false, "LastDownloadUserid", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRequestDatetime = cci("REQUEST_DATETIME", "REQUEST_DATETIME", null, null, true, "RequestDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnDownloadPath = cci("DOWNLOAD_PATH", "DOWNLOAD_PATH", null, null, false, "DownloadPath", typeof(String), false, "VARCHAR2", 260, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnProcServerCode = cci("PROC_SERVER_CODE", "PROC_SERVER_CODE", null, null, false, "ProcServerCode", typeof(String), false, "VARCHAR2", 24, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnStatusCode = cci("STATUS_CODE", "STATUS_CODE", null, null, true, "StatusCode", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDescription = cci("DESCRIPTION", "DESCRIPTION", null, null, false, "Description", typeof(String), false, "NVARCHAR2", 256, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnEndDatetime = cci("END_DATETIME", "END_DATETIME", null, null, false, "EndDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnLastDownloadDatetime = cci("LAST_DOWNLOAD_DATETIME", "LAST_DOWNLOAD_DATETIME", null, null, false, "LastDownloadDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
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
            _columnProcWeight = cci("PROC_WEIGHT", "PROC_WEIGHT", null, null, false, "ProcWeight", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputReportsetInfoId = cci("OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", null, null, false, "OutputReportsetInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputReportsetInfo", null);
            _columnDeleteFlag = cci("DELETE_FLAG", "DELETE_FLAG", null, null, true, "DeleteFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnViewSurveyName = cci("VIEW_SURVEY_NAME", "VIEW_SURVEY_NAME", null, null, false, "ViewSurveyName", typeof(String), false, "NVARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLanguage = cci("LANGUAGE", "LANGUAGE", null, null, true, "Language", typeof(String), false, "VARCHAR2", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnShowZeroNaIvCode = cci("SHOW_ZERO_NA_IV_CODE", "SHOW_ZERO_NA_IV_CODE", null, null, false, "ShowZeroNaIvCode", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMergeAxisCellsFlag = cci("MERGE_AXIS_CELLS_FLAG", "MERGE_AXIS_CELLS_FLAG", null, null, false, "MergeAxisCellsFlag", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenarioName = cci("SCENARIO_NAME", "SCENARIO_NAME", null, null, false, "ScenarioName", typeof(String), false, "NVARCHAR2", 200, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnStartDatetime = cci("START_DATETIME", "START_DATETIME", null, null, false, "StartDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnTestLogFlag = cci("TEST_LOG_FLAG", "TEST_LOG_FLAG", null, null, false, "TestLogFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTsvFileSizeGt = cci("TSV_FILE_SIZE_GT", "TSV_FILE_SIZE_GT", null, null, true, "TsvFileSizeGt", typeof(long?), false, "NUMBER", 14, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTsvFileSizeCross = cci("TSV_FILE_SIZE_CROSS", "TSV_FILE_SIZE_CROSS", null, null, true, "TsvFileSizeCross", typeof(long?), false, "NUMBER", 14, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTsvFileSizeFa = cci("TSV_FILE_SIZE_FA", "TSV_FILE_SIZE_FA", null, null, true, "TsvFileSizeFa", typeof(long?), false, "NUMBER", 14, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTsvFileSizeDataOutput = cci("TSV_FILE_SIZE_DATA_OUTPUT", "TSV_FILE_SIZE_DATA_OUTPUT", null, null, true, "TsvFileSizeDataOutput", typeof(long?), false, "NUMBER", 14, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputRequestId);
            _columnInfoList.add(ColumnRequestServerCode);
            _columnInfoList.add(ColumnRequestUserId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnLastDownloadUserid);
            _columnInfoList.add(ColumnRequestDatetime);
            _columnInfoList.add(ColumnDownloadPath);
            _columnInfoList.add(ColumnProcServerCode);
            _columnInfoList.add(ColumnStatusCode);
            _columnInfoList.add(ColumnDescription);
            _columnInfoList.add(ColumnEndDatetime);
            _columnInfoList.add(ColumnLastDownloadDatetime);
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
            _columnInfoList.add(ColumnProcWeight);
            _columnInfoList.add(ColumnOutputReportsetInfoId);
            _columnInfoList.add(ColumnDeleteFlag);
            _columnInfoList.add(ColumnViewSurveyName);
            _columnInfoList.add(ColumnLanguage);
            _columnInfoList.add(ColumnShowZeroNaIvCode);
            _columnInfoList.add(ColumnMergeAxisCellsFlag);
            _columnInfoList.add(ColumnScenarioName);
            _columnInfoList.add(ColumnStartDatetime);
            _columnInfoList.add(ColumnTestLogFlag);
            _columnInfoList.add(ColumnTsvFileSizeGt);
            _columnInfoList.add(ColumnTsvFileSizeCross);
            _columnInfoList.add(ColumnTsvFileSizeFa);
            _columnInfoList.add(ColumnTsvFileSizeDataOutput);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputRequestId);
        }}

        // -------------------------------------------------
        //                                   Primary Element
        //                                   ---------------
        public override bool HasPrimaryKey { get { return true; } }
        public override bool HasCompoundPrimaryKey { get { return false; } }

        // ===============================================================================
        //                                                                   Relation Info
        //                                                                   =============
        // -------------------------------------------------
        //                                   Foreign Element
        //                                   ---------------
        public ForeignInfo ForeignTQcwebSurveyInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfo", this, TQcwebSurveyInfoDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTOutputReportsetInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputReportsetInfoId, TOutputReportsetInfoDbm.GetInstance().ColumnOutputReportsetInfoId);
            return cfi("TOutputReportsetInfo", this, TOutputReportsetInfoDbm.GetInstance(), map, 1, false, false);
        }}
        public ForeignInfo ForeignTOutputCommon { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputRequestId, TOutputCommonDbm.GetInstance().ColumnOutputRequestId);
            return cfi("TOutputCommon", this, TOutputCommonDbm.GetInstance(), map, 2, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTOutputCommonList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputRequestId, TOutputCommonDbm.GetInstance().ColumnOutputRequestId);
            return cri("TOutputCommonList", this, TOutputCommonDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Output_Request_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Output_Request_SEQ_01.nextval from dual"; } }
        public override int? SequenceIncrementSize { get { return 1; } }
        public override int? SequenceCacheSize { get { return null; } }
        public override bool HasCommonColumn { get { return false; } }

        // ===============================================================================
        //                                                                 Name Definition
        //                                                                 ===============
        #region Name

        // -------------------------------------------------
        //                                             Table
        //                                             -----
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_REQUEST";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputRequest";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_REQUEST_ID = "OUTPUT_REQUEST_ID";
        public static readonly String DB_NAME_REQUEST_SERVER_CODE = "REQUEST_SERVER_CODE";
        public static readonly String DB_NAME_REQUEST_USER_ID = "REQUEST_USER_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_LAST_DOWNLOAD_USERID = "LAST_DOWNLOAD_USERID";
        public static readonly String DB_NAME_REQUEST_DATETIME = "REQUEST_DATETIME";
        public static readonly String DB_NAME_DOWNLOAD_PATH = "DOWNLOAD_PATH";
        public static readonly String DB_NAME_PROC_SERVER_CODE = "PROC_SERVER_CODE";
        public static readonly String DB_NAME_STATUS_CODE = "STATUS_CODE";
        public static readonly String DB_NAME_DESCRIPTION = "DESCRIPTION";
        public static readonly String DB_NAME_END_DATETIME = "END_DATETIME";
        public static readonly String DB_NAME_LAST_DOWNLOAD_DATETIME = "LAST_DOWNLOAD_DATETIME";
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
        public static readonly String DB_NAME_PROC_WEIGHT = "PROC_WEIGHT";
        public static readonly String DB_NAME_OUTPUT_REPORTSET_INFO_ID = "OUTPUT_REPORTSET_INFO_ID";
        public static readonly String DB_NAME_DELETE_FLAG = "DELETE_FLAG";
        public static readonly String DB_NAME_VIEW_SURVEY_NAME = "VIEW_SURVEY_NAME";
        public static readonly String DB_NAME_LANGUAGE = "LANGUAGE";
        public static readonly String DB_NAME_SHOW_ZERO_NA_IV_CODE = "SHOW_ZERO_NA_IV_CODE";
        public static readonly String DB_NAME_MERGE_AXIS_CELLS_FLAG = "MERGE_AXIS_CELLS_FLAG";
        public static readonly String DB_NAME_SCENARIO_NAME = "SCENARIO_NAME";
        public static readonly String DB_NAME_START_DATETIME = "START_DATETIME";
        public static readonly String DB_NAME_TEST_LOG_FLAG = "TEST_LOG_FLAG";
        public static readonly String DB_NAME_TSV_FILE_SIZE_GT = "TSV_FILE_SIZE_GT";
        public static readonly String DB_NAME_TSV_FILE_SIZE_CROSS = "TSV_FILE_SIZE_CROSS";
        public static readonly String DB_NAME_TSV_FILE_SIZE_FA = "TSV_FILE_SIZE_FA";
        public static readonly String DB_NAME_TSV_FILE_SIZE_DATA_OUTPUT = "TSV_FILE_SIZE_DATA_OUTPUT";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_REQUEST_ID = "OutputRequestId";
        public static readonly String PROPERTY_NAME_REQUEST_SERVER_CODE = "RequestServerCode";
        public static readonly String PROPERTY_NAME_REQUEST_USER_ID = "RequestUserId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_LAST_DOWNLOAD_USERID = "LastDownloadUserid";
        public static readonly String PROPERTY_NAME_REQUEST_DATETIME = "RequestDatetime";
        public static readonly String PROPERTY_NAME_DOWNLOAD_PATH = "DownloadPath";
        public static readonly String PROPERTY_NAME_PROC_SERVER_CODE = "ProcServerCode";
        public static readonly String PROPERTY_NAME_STATUS_CODE = "StatusCode";
        public static readonly String PROPERTY_NAME_DESCRIPTION = "Description";
        public static readonly String PROPERTY_NAME_END_DATETIME = "EndDatetime";
        public static readonly String PROPERTY_NAME_LAST_DOWNLOAD_DATETIME = "LastDownloadDatetime";
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
        public static readonly String PROPERTY_NAME_PROC_WEIGHT = "ProcWeight";
        public static readonly String PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID = "OutputReportsetInfoId";
        public static readonly String PROPERTY_NAME_DELETE_FLAG = "DeleteFlag";
        public static readonly String PROPERTY_NAME_VIEW_SURVEY_NAME = "ViewSurveyName";
        public static readonly String PROPERTY_NAME_LANGUAGE = "Language";
        public static readonly String PROPERTY_NAME_SHOW_ZERO_NA_IV_CODE = "ShowZeroNaIvCode";
        public static readonly String PROPERTY_NAME_MERGE_AXIS_CELLS_FLAG = "MergeAxisCellsFlag";
        public static readonly String PROPERTY_NAME_SCENARIO_NAME = "ScenarioName";
        public static readonly String PROPERTY_NAME_START_DATETIME = "StartDatetime";
        public static readonly String PROPERTY_NAME_TEST_LOG_FLAG = "TestLogFlag";
        public static readonly String PROPERTY_NAME_TSV_FILE_SIZE_GT = "TsvFileSizeGt";
        public static readonly String PROPERTY_NAME_TSV_FILE_SIZE_CROSS = "TsvFileSizeCross";
        public static readonly String PROPERTY_NAME_TSV_FILE_SIZE_FA = "TsvFileSizeFa";
        public static readonly String PROPERTY_NAME_TSV_FILE_SIZE_DATA_OUTPUT = "TsvFileSizeDataOutput";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputReportsetInfo = "TOutputReportsetInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputCommon = "TOutputCommon";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TOutputCommonList = "TOutputCommonList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputRequestDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_REQUEST_ID.ToLower(), PROPERTY_NAME_OUTPUT_REQUEST_ID);
                map.put(DB_NAME_REQUEST_SERVER_CODE.ToLower(), PROPERTY_NAME_REQUEST_SERVER_CODE);
                map.put(DB_NAME_REQUEST_USER_ID.ToLower(), PROPERTY_NAME_REQUEST_USER_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_LAST_DOWNLOAD_USERID.ToLower(), PROPERTY_NAME_LAST_DOWNLOAD_USERID);
                map.put(DB_NAME_REQUEST_DATETIME.ToLower(), PROPERTY_NAME_REQUEST_DATETIME);
                map.put(DB_NAME_DOWNLOAD_PATH.ToLower(), PROPERTY_NAME_DOWNLOAD_PATH);
                map.put(DB_NAME_PROC_SERVER_CODE.ToLower(), PROPERTY_NAME_PROC_SERVER_CODE);
                map.put(DB_NAME_STATUS_CODE.ToLower(), PROPERTY_NAME_STATUS_CODE);
                map.put(DB_NAME_DESCRIPTION.ToLower(), PROPERTY_NAME_DESCRIPTION);
                map.put(DB_NAME_END_DATETIME.ToLower(), PROPERTY_NAME_END_DATETIME);
                map.put(DB_NAME_LAST_DOWNLOAD_DATETIME.ToLower(), PROPERTY_NAME_LAST_DOWNLOAD_DATETIME);
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
                map.put(DB_NAME_PROC_WEIGHT.ToLower(), PROPERTY_NAME_PROC_WEIGHT);
                map.put(DB_NAME_OUTPUT_REPORTSET_INFO_ID.ToLower(), PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID);
                map.put(DB_NAME_DELETE_FLAG.ToLower(), PROPERTY_NAME_DELETE_FLAG);
                map.put(DB_NAME_VIEW_SURVEY_NAME.ToLower(), PROPERTY_NAME_VIEW_SURVEY_NAME);
                map.put(DB_NAME_LANGUAGE.ToLower(), PROPERTY_NAME_LANGUAGE);
                map.put(DB_NAME_SHOW_ZERO_NA_IV_CODE.ToLower(), PROPERTY_NAME_SHOW_ZERO_NA_IV_CODE);
                map.put(DB_NAME_MERGE_AXIS_CELLS_FLAG.ToLower(), PROPERTY_NAME_MERGE_AXIS_CELLS_FLAG);
                map.put(DB_NAME_SCENARIO_NAME.ToLower(), PROPERTY_NAME_SCENARIO_NAME);
                map.put(DB_NAME_START_DATETIME.ToLower(), PROPERTY_NAME_START_DATETIME);
                map.put(DB_NAME_TEST_LOG_FLAG.ToLower(), PROPERTY_NAME_TEST_LOG_FLAG);
                map.put(DB_NAME_TSV_FILE_SIZE_GT.ToLower(), PROPERTY_NAME_TSV_FILE_SIZE_GT);
                map.put(DB_NAME_TSV_FILE_SIZE_CROSS.ToLower(), PROPERTY_NAME_TSV_FILE_SIZE_CROSS);
                map.put(DB_NAME_TSV_FILE_SIZE_FA.ToLower(), PROPERTY_NAME_TSV_FILE_SIZE_FA);
                map.put(DB_NAME_TSV_FILE_SIZE_DATA_OUTPUT.ToLower(), PROPERTY_NAME_TSV_FILE_SIZE_DATA_OUTPUT);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_REQUEST_ID.ToLower(), DB_NAME_OUTPUT_REQUEST_ID);
                map.put(PROPERTY_NAME_REQUEST_SERVER_CODE.ToLower(), DB_NAME_REQUEST_SERVER_CODE);
                map.put(PROPERTY_NAME_REQUEST_USER_ID.ToLower(), DB_NAME_REQUEST_USER_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_LAST_DOWNLOAD_USERID.ToLower(), DB_NAME_LAST_DOWNLOAD_USERID);
                map.put(PROPERTY_NAME_REQUEST_DATETIME.ToLower(), DB_NAME_REQUEST_DATETIME);
                map.put(PROPERTY_NAME_DOWNLOAD_PATH.ToLower(), DB_NAME_DOWNLOAD_PATH);
                map.put(PROPERTY_NAME_PROC_SERVER_CODE.ToLower(), DB_NAME_PROC_SERVER_CODE);
                map.put(PROPERTY_NAME_STATUS_CODE.ToLower(), DB_NAME_STATUS_CODE);
                map.put(PROPERTY_NAME_DESCRIPTION.ToLower(), DB_NAME_DESCRIPTION);
                map.put(PROPERTY_NAME_END_DATETIME.ToLower(), DB_NAME_END_DATETIME);
                map.put(PROPERTY_NAME_LAST_DOWNLOAD_DATETIME.ToLower(), DB_NAME_LAST_DOWNLOAD_DATETIME);
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
                map.put(PROPERTY_NAME_PROC_WEIGHT.ToLower(), DB_NAME_PROC_WEIGHT);
                map.put(PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID.ToLower(), DB_NAME_OUTPUT_REPORTSET_INFO_ID);
                map.put(PROPERTY_NAME_DELETE_FLAG.ToLower(), DB_NAME_DELETE_FLAG);
                map.put(PROPERTY_NAME_VIEW_SURVEY_NAME.ToLower(), DB_NAME_VIEW_SURVEY_NAME);
                map.put(PROPERTY_NAME_LANGUAGE.ToLower(), DB_NAME_LANGUAGE);
                map.put(PROPERTY_NAME_SHOW_ZERO_NA_IV_CODE.ToLower(), DB_NAME_SHOW_ZERO_NA_IV_CODE);
                map.put(PROPERTY_NAME_MERGE_AXIS_CELLS_FLAG.ToLower(), DB_NAME_MERGE_AXIS_CELLS_FLAG);
                map.put(PROPERTY_NAME_SCENARIO_NAME.ToLower(), DB_NAME_SCENARIO_NAME);
                map.put(PROPERTY_NAME_START_DATETIME.ToLower(), DB_NAME_START_DATETIME);
                map.put(PROPERTY_NAME_TEST_LOG_FLAG.ToLower(), DB_NAME_TEST_LOG_FLAG);
                map.put(PROPERTY_NAME_TSV_FILE_SIZE_GT.ToLower(), DB_NAME_TSV_FILE_SIZE_GT);
                map.put(PROPERTY_NAME_TSV_FILE_SIZE_CROSS.ToLower(), DB_NAME_TSV_FILE_SIZE_CROSS);
                map.put(PROPERTY_NAME_TSV_FILE_SIZE_FA.ToLower(), DB_NAME_TSV_FILE_SIZE_FA);
                map.put(PROPERTY_NAME_TSV_FILE_SIZE_DATA_OUTPUT.ToLower(), DB_NAME_TSV_FILE_SIZE_DATA_OUTPUT);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputRequest"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputRequestDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputRequestCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputRequestBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputRequest NewMyEntity() { return new TOutputRequest(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputRequestCB NewMyConditionBean() { return new TOutputRequestCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputRequest>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputRequest>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_REQUEST_ID", "OutputRequestId", new EntityPropertyOutputRequestIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REQUEST_SERVER_CODE", "RequestServerCode", new EntityPropertyRequestServerCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REQUEST_USER_ID", "RequestUserId", new EntityPropertyRequestUserIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_DOWNLOAD_USERID", "LastDownloadUserid", new EntityPropertyLastDownloadUseridSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REQUEST_DATETIME", "RequestDatetime", new EntityPropertyRequestDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DOWNLOAD_PATH", "DownloadPath", new EntityPropertyDownloadPathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PROC_SERVER_CODE", "ProcServerCode", new EntityPropertyProcServerCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("STATUS_CODE", "StatusCode", new EntityPropertyStatusCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DESCRIPTION", "Description", new EntityPropertyDescriptionSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("END_DATETIME", "EndDatetime", new EntityPropertyEndDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_DOWNLOAD_DATETIME", "LastDownloadDatetime", new EntityPropertyLastDownloadDatetimeSetupper(), _entityPropertySetupperMap);
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
            RegisterEntityPropertySetupper("PROC_WEIGHT", "ProcWeight", new EntityPropertyProcWeightSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_REPORTSET_INFO_ID", "OutputReportsetInfoId", new EntityPropertyOutputReportsetInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_FLAG", "DeleteFlag", new EntityPropertyDeleteFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VIEW_SURVEY_NAME", "ViewSurveyName", new EntityPropertyViewSurveyNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LANGUAGE", "Language", new EntityPropertyLanguageSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SHOW_ZERO_NA_IV_CODE", "ShowZeroNaIvCode", new EntityPropertyShowZeroNaIvCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MERGE_AXIS_CELLS_FLAG", "MergeAxisCellsFlag", new EntityPropertyMergeAxisCellsFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_NAME", "ScenarioName", new EntityPropertyScenarioNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("START_DATETIME", "StartDatetime", new EntityPropertyStartDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_LOG_FLAG", "TestLogFlag", new EntityPropertyTestLogFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TSV_FILE_SIZE_GT", "TsvFileSizeGt", new EntityPropertyTsvFileSizeGtSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TSV_FILE_SIZE_CROSS", "TsvFileSizeCross", new EntityPropertyTsvFileSizeCrossSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TSV_FILE_SIZE_FA", "TsvFileSizeFa", new EntityPropertyTsvFileSizeFaSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TSV_FILE_SIZE_DATA_OUTPUT", "TsvFileSizeDataOutput", new EntityPropertyTsvFileSizeDataOutputSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputRequest> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputRequest)entity, value);
        }

        public class EntityPropertyOutputRequestIdSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.OutputRequestId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyRequestServerCodeSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.RequestServerCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyRequestUserIdSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.RequestUserId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyLastDownloadUseridSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.LastDownloadUserid = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyRequestDatetimeSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.RequestDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyDownloadPathSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.DownloadPath = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyProcServerCodeSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.ProcServerCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyStatusCodeSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.StatusCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDescriptionSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.Description = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyEndDatetimeSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.EndDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyLastDownloadDatetimeSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.LastDownloadDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyExcelbookTypeSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.ExcelbookType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNumericAnswerViewCodeSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.NumericAnswerViewCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpTotalSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.DpTotal = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpAverageSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.DpAverage = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpStandardDivSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.DpStandardDiv = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpMinSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.DpMin = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpMaxSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.DpMax = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpMedianSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.DpMedian = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpWeightSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.DpWeight = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpWeightavrSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.DpWeightavr = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyProcWeightSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.ProcWeight = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputReportsetInfoIdSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.OutputReportsetInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDeleteFlagSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.DeleteFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyViewSurveyNameSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.ViewSurveyName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLanguageSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.Language = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyShowZeroNaIvCodeSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.ShowZeroNaIvCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMergeAxisCellsFlagSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.MergeAxisCellsFlag = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyScenarioNameSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.ScenarioName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyStartDatetimeSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.StartDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyTestLogFlagSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.TestLogFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTsvFileSizeGtSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.TsvFileSizeGt = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyTsvFileSizeCrossSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.TsvFileSizeCross = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyTsvFileSizeFaSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.TsvFileSizeFa = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyTsvFileSizeDataOutputSetupper : EntityPropertySetupper<TOutputRequest> {
            public void Setup(TOutputRequest entity, Object value) { entity.TsvFileSizeDataOutput = (value != null) ? (long?)value : null; }
        }
    }
}
