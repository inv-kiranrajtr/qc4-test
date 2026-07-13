
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

    public class TQcwebSurveyInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TQcwebSurveyInfo);

        private static readonly TQcwebSurveyInfoDbm _instance = new TQcwebSurveyInfoDbm();
        private TQcwebSurveyInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TQcwebSurveyInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_QCWEB_SURVEY_INFO"; } }
        public override String TablePropertyName { get { return "TQcwebSurveyInfo"; } }
        public override String TableSqlName { get { return "T_QCWEB_SURVEY_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnAddDataNo;
        protected ColumnInfo _columnSurveyNameOrg;
        protected ColumnInfo _columnImportDatetime;
        protected ColumnInfo _columnImportFileName;
        protected ColumnInfo _columnDeleteFlag;
        protected ColumnInfo _columnViewSurveyName;
        protected ColumnInfo _columnGtCount;
        protected ColumnInfo _columnCrossCount;
        protected ColumnInfo _columnFaCount;
        protected ColumnInfo _columnVersionNo;
        protected ColumnInfo _columnLastUpdateUser;
        protected ColumnInfo _columnLastUpdateDatetime;
        protected ColumnInfo _columnSurveyInfoId;
        protected ColumnInfo _columnRawdataImportQueInfoId;
        protected ColumnInfo _columnUtf8Flag;

        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnAddDataNo { get { return _columnAddDataNo; } }
        public ColumnInfo ColumnSurveyNameOrg { get { return _columnSurveyNameOrg; } }
        public ColumnInfo ColumnImportDatetime { get { return _columnImportDatetime; } }
        public ColumnInfo ColumnImportFileName { get { return _columnImportFileName; } }
        public ColumnInfo ColumnDeleteFlag { get { return _columnDeleteFlag; } }
        public ColumnInfo ColumnViewSurveyName { get { return _columnViewSurveyName; } }
        public ColumnInfo ColumnGtCount { get { return _columnGtCount; } }
        public ColumnInfo ColumnCrossCount { get { return _columnCrossCount; } }
        public ColumnInfo ColumnFaCount { get { return _columnFaCount; } }
        public ColumnInfo ColumnVersionNo { get { return _columnVersionNo; } }
        public ColumnInfo ColumnLastUpdateUser { get { return _columnLastUpdateUser; } }
        public ColumnInfo ColumnLastUpdateDatetime { get { return _columnLastUpdateDatetime; } }
        public ColumnInfo ColumnSurveyInfoId { get { return _columnSurveyInfoId; } }
        public ColumnInfo ColumnRawdataImportQueInfoId { get { return _columnRawdataImportQueInfoId; } }
        public ColumnInfo ColumnUtf8Flag { get { return _columnUtf8Flag; } }

        protected void InitializeColumnInfo() {
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TAllocationCellInfo,TSelectConditionInfo,TItemInfo,TTableControl,TDefaultEnv,TDefaultEnvColorInfo,TScenarioTotalization,TReportset,TDataEditList,TOutputSetting,TOutputRequest,TAccessPermissionsInfo,TSessionControler,TNotice,TOutputSettingGt,TOutputSettingCross,TOutputSettingFa,TOutputSettingReport,TQcwebSurveyDetail,TAccessPermissionsInfoAsOne,TOutputSettingAsOne,TOutputSettingCrossAsOne,TOutputSettingFaAsOne,TOutputSettingGtAsOne,TOutputSettingReportAsOne", "TAllocationCellInfoList,TDataEditListList,TItemInfoList,TNoticeList,TOutputRequestList,TOutputTemplateList,TQcwebSurveyDetailList,TRawdataImportQueInfoList,TReportsetList,TScenarioTotalizationList,TSelectConditionInfoList,TSessionControlerList,TWeightbackList");
            _columnAddDataNo = cci("ADD_DATA_NO", "ADD_DATA_NO", null, null, false, "AddDataNo", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSurveyNameOrg = cci("SURVEY_NAME_ORG", "SURVEY_NAME_ORG", null, null, true, "SurveyNameOrg", typeof(String), false, "NVARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnImportDatetime = cci("IMPORT_DATETIME", "IMPORT_DATETIME", null, null, true, "ImportDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnImportFileName = cci("IMPORT_FILE_NAME", "IMPORT_FILE_NAME", null, null, true, "ImportFileName", typeof(String), false, "VARCHAR2", 255, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDeleteFlag = cci("DELETE_FLAG", "DELETE_FLAG", null, null, true, "DeleteFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnViewSurveyName = cci("VIEW_SURVEY_NAME", "VIEW_SURVEY_NAME", null, null, true, "ViewSurveyName", typeof(String), false, "NVARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGtCount = cci("GT_COUNT", "GT_COUNT", null, null, true, "GtCount", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCrossCount = cci("CROSS_COUNT", "CROSS_COUNT", null, null, true, "CrossCount", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFaCount = cci("FA_COUNT", "FA_COUNT", null, null, true, "FaCount", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnVersionNo = cci("VERSION_NO", "VERSION_NO", null, null, true, "VersionNo", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.VERSION_NO, null, null, null);
            _columnLastUpdateUser = cci("LAST_UPDATE_USER", "LAST_UPDATE_USER", null, null, false, "LastUpdateUser", typeof(String), false, "VARCHAR2", 1000, 0, true, OptimisticLockType.NONE, null, null, null);
            _columnLastUpdateDatetime = cci("LAST_UPDATE_DATETIME", "LAST_UPDATE_DATETIME", null, null, false, "LastUpdateDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, true, OptimisticLockType.NONE, null, null, null);
            _columnSurveyInfoId = cci("SURVEY_INFO_ID", "SURVEY_INFO_ID", null, null, true, "SurveyInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TSurveyInfo", null);
            _columnRawdataImportQueInfoId = cci("RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", null, null, true, "RawdataImportQueInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TRawdataImportQueInfo", null);
            _columnUtf8Flag = cci("UTF8_FLAG", "UTF8_FLAG", null, null, true, "Utf8Flag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnAddDataNo);
            _columnInfoList.add(ColumnSurveyNameOrg);
            _columnInfoList.add(ColumnImportDatetime);
            _columnInfoList.add(ColumnImportFileName);
            _columnInfoList.add(ColumnDeleteFlag);
            _columnInfoList.add(ColumnViewSurveyName);
            _columnInfoList.add(ColumnGtCount);
            _columnInfoList.add(ColumnCrossCount);
            _columnInfoList.add(ColumnFaCount);
            _columnInfoList.add(ColumnVersionNo);
            _columnInfoList.add(ColumnLastUpdateUser);
            _columnInfoList.add(ColumnLastUpdateDatetime);
            _columnInfoList.add(ColumnSurveyInfoId);
            _columnInfoList.add(ColumnRawdataImportQueInfoId);
            _columnInfoList.add(ColumnUtf8Flag);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnQcwebid);
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
        public ForeignInfo ForeignTSurveyInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnSurveyInfoId, TSurveyInfoDbm.GetInstance().ColumnSurveyInfoId);
            return cfi("TSurveyInfo", this, TSurveyInfoDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTRawdataImportQueInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnRawdataImportQueInfoId, TRawdataImportQueInfoDbm.GetInstance().ColumnRawdataImportQueInfoId);
            return cfi("TRawdataImportQueInfo", this, TRawdataImportQueInfoDbm.GetInstance(), map, 1, false, false);
        }}
        public ForeignInfo ForeignTAllocationCellInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TAllocationCellInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TAllocationCellInfo", this, TAllocationCellInfoDbm.GetInstance(), map, 2, true, false);
        }}
        public ForeignInfo ForeignTSelectConditionInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TSelectConditionInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TSelectConditionInfo", this, TSelectConditionInfoDbm.GetInstance(), map, 3, true, false);
        }}
        public ForeignInfo ForeignTItemInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TItemInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TItemInfo", this, TItemInfoDbm.GetInstance(), map, 4, true, false);
        }}
        public ForeignInfo ForeignTTableControl { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TTableControlDbm.GetInstance().ColumnQcwebid);
            return cfi("TTableControl", this, TTableControlDbm.GetInstance(), map, 5, true, false);
        }}
        public ForeignInfo ForeignTDefaultEnv { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TDefaultEnvDbm.GetInstance().ColumnQcwebid);
            return cfi("TDefaultEnv", this, TDefaultEnvDbm.GetInstance(), map, 6, true, false);
        }}
        public ForeignInfo ForeignTDefaultEnvColorInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TDefaultEnvColorInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TDefaultEnvColorInfo", this, TDefaultEnvColorInfoDbm.GetInstance(), map, 7, true, false);
        }}
        public ForeignInfo ForeignTScenarioTotalization { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TScenarioTotalizationDbm.GetInstance().ColumnQcwebid);
            return cfi("TScenarioTotalization", this, TScenarioTotalizationDbm.GetInstance(), map, 8, true, false);
        }}
        public ForeignInfo ForeignTReportset { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TReportsetDbm.GetInstance().ColumnQcwebid);
            return cfi("TReportset", this, TReportsetDbm.GetInstance(), map, 9, true, false);
        }}
        public ForeignInfo ForeignTDataEditList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TDataEditListDbm.GetInstance().ColumnQcwebid);
            return cfi("TDataEditList", this, TDataEditListDbm.GetInstance(), map, 10, true, false);
        }}
        public ForeignInfo ForeignTOutputSetting { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSetting", this, TOutputSettingDbm.GetInstance(), map, 11, true, false);
        }}
        public ForeignInfo ForeignTOutputRequest { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputRequestDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputRequest", this, TOutputRequestDbm.GetInstance(), map, 12, true, false);
        }}
        public ForeignInfo ForeignTAccessPermissionsInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TAccessPermissionsInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TAccessPermissionsInfo", this, TAccessPermissionsInfoDbm.GetInstance(), map, 13, true, false);
        }}
        public ForeignInfo ForeignTSessionControler { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TSessionControlerDbm.GetInstance().ColumnQcwebid);
            return cfi("TSessionControler", this, TSessionControlerDbm.GetInstance(), map, 14, true, false);
        }}
        public ForeignInfo ForeignTNotice { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TNoticeDbm.GetInstance().ColumnQcwebid);
            return cfi("TNotice", this, TNoticeDbm.GetInstance(), map, 15, true, false);
        }}
        public ForeignInfo ForeignTOutputSettingGt { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingGtDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSettingGt", this, TOutputSettingGtDbm.GetInstance(), map, 16, true, false);
        }}
        public ForeignInfo ForeignTOutputSettingCross { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingCrossDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSettingCross", this, TOutputSettingCrossDbm.GetInstance(), map, 17, true, false);
        }}
        public ForeignInfo ForeignTOutputSettingFa { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingFaDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSettingFa", this, TOutputSettingFaDbm.GetInstance(), map, 18, true, false);
        }}
        public ForeignInfo ForeignTOutputSettingReport { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingReportDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSettingReport", this, TOutputSettingReportDbm.GetInstance(), map, 19, true, false);
        }}
        public ForeignInfo ForeignTQcwebSurveyDetail { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyDetailDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyDetail", this, TQcwebSurveyDetailDbm.GetInstance(), map, 20, true, false);
        }}

        public ForeignInfo ForeignTAccessPermissionsInfoAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TAccessPermissionsInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TAccessPermissionsInfoAsOne", this, TAccessPermissionsInfoDbm.GetInstance(), map, 21, true, false);
        }}
        public ForeignInfo ForeignTOutputSettingAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSettingAsOne", this, TOutputSettingDbm.GetInstance(), map, 22, true, false);
        }}
        public ForeignInfo ForeignTOutputSettingCrossAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingCrossDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSettingCrossAsOne", this, TOutputSettingCrossDbm.GetInstance(), map, 23, true, false);
        }}
        public ForeignInfo ForeignTOutputSettingFaAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingFaDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSettingFaAsOne", this, TOutputSettingFaDbm.GetInstance(), map, 24, true, false);
        }}
        public ForeignInfo ForeignTOutputSettingGtAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingGtDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSettingGtAsOne", this, TOutputSettingGtDbm.GetInstance(), map, 25, true, false);
        }}
        public ForeignInfo ForeignTOutputSettingReportAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingReportDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSettingReportAsOne", this, TOutputSettingReportDbm.GetInstance(), map, 26, true, false);
        }}

        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTAllocationCellInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TAllocationCellInfoDbm.GetInstance().ColumnQcwebid);
            return cri("TAllocationCellInfoList", this, TAllocationCellInfoDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTDataEditListList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TDataEditListDbm.GetInstance().ColumnQcwebid);
            return cri("TDataEditListList", this, TDataEditListDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTItemInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TItemInfoDbm.GetInstance().ColumnQcwebid);
            return cri("TItemInfoList", this, TItemInfoDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTNoticeList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TNoticeDbm.GetInstance().ColumnQcwebid);
            return cri("TNoticeList", this, TNoticeDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTOutputRequestList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputRequestDbm.GetInstance().ColumnQcwebid);
            return cri("TOutputRequestList", this, TOutputRequestDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTOutputTemplateList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputTemplateDbm.GetInstance().ColumnQcwebid);
            return cri("TOutputTemplateList", this, TOutputTemplateDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTQcwebSurveyDetailList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyDetailDbm.GetInstance().ColumnQcwebid);
            return cri("TQcwebSurveyDetailList", this, TQcwebSurveyDetailDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTRawdataImportQueInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TRawdataImportQueInfoDbm.GetInstance().ColumnQcwebid);
            return cri("TRawdataImportQueInfoList", this, TRawdataImportQueInfoDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTReportsetList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TReportsetDbm.GetInstance().ColumnQcwebid);
            return cri("TReportsetList", this, TReportsetDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTScenarioTotalizationList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TScenarioTotalizationDbm.GetInstance().ColumnQcwebid);
            return cri("TScenarioTotalizationList", this, TScenarioTotalizationDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTSelectConditionInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TSelectConditionInfoDbm.GetInstance().ColumnQcwebid);
            return cri("TSelectConditionInfoList", this, TSelectConditionInfoDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTSessionControlerList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TSessionControlerDbm.GetInstance().ColumnQcwebid);
            return cri("TSessionControlerList", this, TSessionControlerDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTWeightbackList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TWeightbackDbm.GetInstance().ColumnQcwebid);
            return cri("TWeightbackList", this, TWeightbackDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_QCWeb_Survey_Info_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_QCWeb_Survey_Info_SEQ_01.nextval from dual"; } }
        public override int? SequenceIncrementSize { get { return 1; } }
        public override int? SequenceCacheSize { get { return null; } }
        public override bool HasVersionNo { get { return true; } }
        public override ColumnInfo VersionNoColumnInfo { get { return _columnVersionNo; } }
        public override bool HasCommonColumn { get { return true; } }

        // ===============================================================================
        //                                                                 Name Definition
        //                                                                 ===============
        #region Name

        // -------------------------------------------------
        //                                             Table
        //                                             -----
        public static readonly String TABLE_DB_NAME = "T_QCWEB_SURVEY_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TQcwebSurveyInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_ADD_DATA_NO = "ADD_DATA_NO";
        public static readonly String DB_NAME_SURVEY_NAME_ORG = "SURVEY_NAME_ORG";
        public static readonly String DB_NAME_IMPORT_DATETIME = "IMPORT_DATETIME";
        public static readonly String DB_NAME_IMPORT_FILE_NAME = "IMPORT_FILE_NAME";
        public static readonly String DB_NAME_DELETE_FLAG = "DELETE_FLAG";
        public static readonly String DB_NAME_VIEW_SURVEY_NAME = "VIEW_SURVEY_NAME";
        public static readonly String DB_NAME_GT_COUNT = "GT_COUNT";
        public static readonly String DB_NAME_CROSS_COUNT = "CROSS_COUNT";
        public static readonly String DB_NAME_FA_COUNT = "FA_COUNT";
        public static readonly String DB_NAME_VERSION_NO = "VERSION_NO";
        public static readonly String DB_NAME_LAST_UPDATE_USER = "LAST_UPDATE_USER";
        public static readonly String DB_NAME_LAST_UPDATE_DATETIME = "LAST_UPDATE_DATETIME";
        public static readonly String DB_NAME_SURVEY_INFO_ID = "SURVEY_INFO_ID";
        public static readonly String DB_NAME_RAWDATA_IMPORT_QUE_INFO_ID = "RAWDATA_IMPORT_QUE_INFO_ID";
        public static readonly String DB_NAME_UTF8_FLAG = "UTF8_FLAG";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_ADD_DATA_NO = "AddDataNo";
        public static readonly String PROPERTY_NAME_SURVEY_NAME_ORG = "SurveyNameOrg";
        public static readonly String PROPERTY_NAME_IMPORT_DATETIME = "ImportDatetime";
        public static readonly String PROPERTY_NAME_IMPORT_FILE_NAME = "ImportFileName";
        public static readonly String PROPERTY_NAME_DELETE_FLAG = "DeleteFlag";
        public static readonly String PROPERTY_NAME_VIEW_SURVEY_NAME = "ViewSurveyName";
        public static readonly String PROPERTY_NAME_GT_COUNT = "GtCount";
        public static readonly String PROPERTY_NAME_CROSS_COUNT = "CrossCount";
        public static readonly String PROPERTY_NAME_FA_COUNT = "FaCount";
        public static readonly String PROPERTY_NAME_VERSION_NO = "VersionNo";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_USER = "LastUpdateUser";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_DATETIME = "LastUpdateDatetime";
        public static readonly String PROPERTY_NAME_SURVEY_INFO_ID = "SurveyInfoId";
        public static readonly String PROPERTY_NAME_RAWDATA_IMPORT_QUE_INFO_ID = "RawdataImportQueInfoId";
        public static readonly String PROPERTY_NAME_UTF8_FLAG = "Utf8Flag";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TSurveyInfo = "TSurveyInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TRawdataImportQueInfo = "TRawdataImportQueInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TAllocationCellInfo = "TAllocationCellInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TSelectConditionInfo = "TSelectConditionInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TItemInfo = "TItemInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TTableControl = "TTableControl";
        public static readonly String FOREIGN_PROPERTY_NAME_TDefaultEnv = "TDefaultEnv";
        public static readonly String FOREIGN_PROPERTY_NAME_TDefaultEnvColorInfo = "TDefaultEnvColorInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TScenarioTotalization = "TScenarioTotalization";
        public static readonly String FOREIGN_PROPERTY_NAME_TReportset = "TReportset";
        public static readonly String FOREIGN_PROPERTY_NAME_TDataEditList = "TDataEditList";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSetting = "TOutputSetting";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputRequest = "TOutputRequest";
        public static readonly String FOREIGN_PROPERTY_NAME_TAccessPermissionsInfo = "TAccessPermissionsInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TSessionControler = "TSessionControler";
        public static readonly String FOREIGN_PROPERTY_NAME_TNotice = "TNotice";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSettingGt = "TOutputSettingGt";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSettingCross = "TOutputSettingCross";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSettingFa = "TOutputSettingFa";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSettingReport = "TOutputSettingReport";
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyDetail = "TQcwebSurveyDetail";
        public static readonly String FOREIGN_PROPERTY_NAME_TAccessPermissionsInfoAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSettingAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSettingCrossAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSettingFaAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSettingGtAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSettingReportAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TAllocationCellInfoList = "TAllocationCellInfoList";
        public static readonly String REFERRER_PROPERTY_NAME_TDataEditListList = "TDataEditListList";
        public static readonly String REFERRER_PROPERTY_NAME_TItemInfoList = "TItemInfoList";
        public static readonly String REFERRER_PROPERTY_NAME_TNoticeList = "TNoticeList";
        public static readonly String REFERRER_PROPERTY_NAME_TOutputRequestList = "TOutputRequestList";
        public static readonly String REFERRER_PROPERTY_NAME_TOutputTemplateList = "TOutputTemplateList";
        public static readonly String REFERRER_PROPERTY_NAME_TQcwebSurveyDetailList = "TQcwebSurveyDetailList";
        public static readonly String REFERRER_PROPERTY_NAME_TRawdataImportQueInfoList = "TRawdataImportQueInfoList";
        public static readonly String REFERRER_PROPERTY_NAME_TReportsetList = "TReportsetList";
        public static readonly String REFERRER_PROPERTY_NAME_TScenarioTotalizationList = "TScenarioTotalizationList";
        public static readonly String REFERRER_PROPERTY_NAME_TSelectConditionInfoList = "TSelectConditionInfoList";
        public static readonly String REFERRER_PROPERTY_NAME_TSessionControlerList = "TSessionControlerList";
        public static readonly String REFERRER_PROPERTY_NAME_TWeightbackList = "TWeightbackList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TQcwebSurveyInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_ADD_DATA_NO.ToLower(), PROPERTY_NAME_ADD_DATA_NO);
                map.put(DB_NAME_SURVEY_NAME_ORG.ToLower(), PROPERTY_NAME_SURVEY_NAME_ORG);
                map.put(DB_NAME_IMPORT_DATETIME.ToLower(), PROPERTY_NAME_IMPORT_DATETIME);
                map.put(DB_NAME_IMPORT_FILE_NAME.ToLower(), PROPERTY_NAME_IMPORT_FILE_NAME);
                map.put(DB_NAME_DELETE_FLAG.ToLower(), PROPERTY_NAME_DELETE_FLAG);
                map.put(DB_NAME_VIEW_SURVEY_NAME.ToLower(), PROPERTY_NAME_VIEW_SURVEY_NAME);
                map.put(DB_NAME_GT_COUNT.ToLower(), PROPERTY_NAME_GT_COUNT);
                map.put(DB_NAME_CROSS_COUNT.ToLower(), PROPERTY_NAME_CROSS_COUNT);
                map.put(DB_NAME_FA_COUNT.ToLower(), PROPERTY_NAME_FA_COUNT);
                map.put(DB_NAME_VERSION_NO.ToLower(), PROPERTY_NAME_VERSION_NO);
                map.put(DB_NAME_LAST_UPDATE_USER.ToLower(), PROPERTY_NAME_LAST_UPDATE_USER);
                map.put(DB_NAME_LAST_UPDATE_DATETIME.ToLower(), PROPERTY_NAME_LAST_UPDATE_DATETIME);
                map.put(DB_NAME_SURVEY_INFO_ID.ToLower(), PROPERTY_NAME_SURVEY_INFO_ID);
                map.put(DB_NAME_RAWDATA_IMPORT_QUE_INFO_ID.ToLower(), PROPERTY_NAME_RAWDATA_IMPORT_QUE_INFO_ID);
                map.put(DB_NAME_UTF8_FLAG.ToLower(), PROPERTY_NAME_UTF8_FLAG);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_ADD_DATA_NO.ToLower(), DB_NAME_ADD_DATA_NO);
                map.put(PROPERTY_NAME_SURVEY_NAME_ORG.ToLower(), DB_NAME_SURVEY_NAME_ORG);
                map.put(PROPERTY_NAME_IMPORT_DATETIME.ToLower(), DB_NAME_IMPORT_DATETIME);
                map.put(PROPERTY_NAME_IMPORT_FILE_NAME.ToLower(), DB_NAME_IMPORT_FILE_NAME);
                map.put(PROPERTY_NAME_DELETE_FLAG.ToLower(), DB_NAME_DELETE_FLAG);
                map.put(PROPERTY_NAME_VIEW_SURVEY_NAME.ToLower(), DB_NAME_VIEW_SURVEY_NAME);
                map.put(PROPERTY_NAME_GT_COUNT.ToLower(), DB_NAME_GT_COUNT);
                map.put(PROPERTY_NAME_CROSS_COUNT.ToLower(), DB_NAME_CROSS_COUNT);
                map.put(PROPERTY_NAME_FA_COUNT.ToLower(), DB_NAME_FA_COUNT);
                map.put(PROPERTY_NAME_VERSION_NO.ToLower(), DB_NAME_VERSION_NO);
                map.put(PROPERTY_NAME_LAST_UPDATE_USER.ToLower(), DB_NAME_LAST_UPDATE_USER);
                map.put(PROPERTY_NAME_LAST_UPDATE_DATETIME.ToLower(), DB_NAME_LAST_UPDATE_DATETIME);
                map.put(PROPERTY_NAME_SURVEY_INFO_ID.ToLower(), DB_NAME_SURVEY_INFO_ID);
                map.put(PROPERTY_NAME_RAWDATA_IMPORT_QUE_INFO_ID.ToLower(), DB_NAME_RAWDATA_IMPORT_QUE_INFO_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TQcwebSurveyInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TQcwebSurveyInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TQcwebSurveyInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TQcwebSurveyInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TQcwebSurveyInfo NewMyEntity() { return new TQcwebSurveyInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TQcwebSurveyInfoCB NewMyConditionBean() { return new TQcwebSurveyInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TQcwebSurveyInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TQcwebSurveyInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ADD_DATA_NO", "AddDataNo", new EntityPropertyAddDataNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SURVEY_NAME_ORG", "SurveyNameOrg", new EntityPropertySurveyNameOrgSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("IMPORT_DATETIME", "ImportDatetime", new EntityPropertyImportDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("IMPORT_FILE_NAME", "ImportFileName", new EntityPropertyImportFileNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_FLAG", "DeleteFlag", new EntityPropertyDeleteFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VIEW_SURVEY_NAME", "ViewSurveyName", new EntityPropertyViewSurveyNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GT_COUNT", "GtCount", new EntityPropertyGtCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CROSS_COUNT", "CrossCount", new EntityPropertyCrossCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA_COUNT", "FaCount", new EntityPropertyFaCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VERSION_NO", "VersionNo", new EntityPropertyVersionNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_USER", "LastUpdateUser", new EntityPropertyLastUpdateUserSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_DATETIME", "LastUpdateDatetime", new EntityPropertyLastUpdateDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SURVEY_INFO_ID", "SurveyInfoId", new EntityPropertySurveyInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RAWDATA_IMPORT_QUE_INFO_ID", "RawdataImportQueInfoId", new EntityPropertyRawdataImportQueInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UTF8_FLAG", "Utf8Flag", new EntityPropertyUtf8FlagSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TQcwebSurveyInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TQcwebSurveyInfo)entity, value);
        }

        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAddDataNoSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.AddDataNo = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertySurveyNameOrgSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.SurveyNameOrg = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyImportDatetimeSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.ImportDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyImportFileNameSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.ImportFileName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDeleteFlagSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.DeleteFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyViewSurveyNameSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.ViewSurveyName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGtCountSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.GtCount = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyCrossCountSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.CrossCount = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFaCountSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.FaCount = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyVersionNoSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.VersionNo = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyLastUpdateUserSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.LastUpdateUser = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLastUpdateDatetimeSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.LastUpdateDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertySurveyInfoIdSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.SurveyInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyRawdataImportQueInfoIdSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.RawdataImportQueInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyUtf8FlagSetupper : EntityPropertySetupper<TQcwebSurveyInfo> {
            public void Setup(TQcwebSurveyInfo entity, Object value) { entity.Utf8Flag = (value != null) ? (int?)value : null; }
        }
    }
}
