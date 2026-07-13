
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

    public class TOutputCommonDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputCommon);

        private static readonly TOutputCommonDbm _instance = new TOutputCommonDbm();
        private TOutputCommonDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputCommonDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_COMMON"; } }
        public override String TablePropertyName { get { return "TOutputCommon"; } }
        public override String TableSqlName { get { return "T_OUTPUT_COMMON"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputCommonId;
        protected ColumnInfo _columnOrderCount;
        protected ColumnInfo _columnTsvFilePath;
        protected ColumnInfo _columnExcelbookNamePrefix;
        protected ColumnInfo _columnProcessStartDatetime;
        protected ColumnInfo _columnProcessForecastEndDatetime;
        protected ColumnInfo _columnProcessEndDatetime;
        protected ColumnInfo _columnStatusCode;
        protected ColumnInfo _columnDescription;
        protected ColumnInfo _columnOutputType;
        protected ColumnInfo _columnOutputRequestId;
        protected ColumnInfo _columnWbSettingCode;
        protected ColumnInfo _columnNoanswerVisibleCode;
        protected ColumnInfo _columnUnmatchVisibleCode;

        public ColumnInfo ColumnOutputCommonId { get { return _columnOutputCommonId; } }
        public ColumnInfo ColumnOrderCount { get { return _columnOrderCount; } }
        public ColumnInfo ColumnTsvFilePath { get { return _columnTsvFilePath; } }
        public ColumnInfo ColumnExcelbookNamePrefix { get { return _columnExcelbookNamePrefix; } }
        public ColumnInfo ColumnProcessStartDatetime { get { return _columnProcessStartDatetime; } }
        public ColumnInfo ColumnProcessForecastEndDatetime { get { return _columnProcessForecastEndDatetime; } }
        public ColumnInfo ColumnProcessEndDatetime { get { return _columnProcessEndDatetime; } }
        public ColumnInfo ColumnStatusCode { get { return _columnStatusCode; } }
        public ColumnInfo ColumnDescription { get { return _columnDescription; } }
        public ColumnInfo ColumnOutputType { get { return _columnOutputType; } }
        public ColumnInfo ColumnOutputRequestId { get { return _columnOutputRequestId; } }
        public ColumnInfo ColumnWbSettingCode { get { return _columnWbSettingCode; } }
        public ColumnInfo ColumnNoanswerVisibleCode { get { return _columnNoanswerVisibleCode; } }
        public ColumnInfo ColumnUnmatchVisibleCode { get { return _columnUnmatchVisibleCode; } }

        protected void InitializeColumnInfo() {
            _columnOutputCommonId = cci("OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", null, null, true, "OutputCommonId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputSubGt,TOutputSubCross,TOutputSubFa,TOutputSubCklist", "TOutputSubCklistList,TOutputSubCrossList,TOutputSubFaList,TOutputSubGtList");
            _columnOrderCount = cci("ORDER_COUNT", "ORDER_COUNT", null, null, true, "OrderCount", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTsvFilePath = cci("TSV_FILE_PATH", "TSV_FILE_PATH", null, null, false, "TsvFilePath", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExcelbookNamePrefix = cci("EXCELBOOK_NAME_PREFIX", "EXCELBOOK_NAME_PREFIX", null, null, false, "ExcelbookNamePrefix", typeof(String), false, "VARCHAR2", 100, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnProcessStartDatetime = cci("PROCESS_START_DATETIME", "PROCESS_START_DATETIME", null, null, false, "ProcessStartDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnProcessForecastEndDatetime = cci("PROCESS_FORECAST_END_DATETIME", "PROCESS_FORECAST_END_DATETIME", null, null, false, "ProcessForecastEndDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnProcessEndDatetime = cci("PROCESS_END_DATETIME", "PROCESS_END_DATETIME", null, null, false, "ProcessEndDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnStatusCode = cci("STATUS_CODE", "STATUS_CODE", null, null, true, "StatusCode", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDescription = cci("DESCRIPTION", "DESCRIPTION", null, null, false, "Description", typeof(String), false, "NVARCHAR2", 256, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputType = cci("OUTPUT_TYPE", "OUTPUT_TYPE", null, null, true, "OutputType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputRequestId = cci("OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", null, null, true, "OutputRequestId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputRequest", null);
            _columnWbSettingCode = cci("WB_SETTING_CODE", "WB_SETTING_CODE", null, null, true, "WbSettingCode", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNoanswerVisibleCode = cci("NOANSWER_VISIBLE_CODE", "NOANSWER_VISIBLE_CODE", null, null, false, "NoanswerVisibleCode", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUnmatchVisibleCode = cci("UNMATCH_VISIBLE_CODE", "UNMATCH_VISIBLE_CODE", null, null, false, "UnmatchVisibleCode", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputCommonId);
            _columnInfoList.add(ColumnOrderCount);
            _columnInfoList.add(ColumnTsvFilePath);
            _columnInfoList.add(ColumnExcelbookNamePrefix);
            _columnInfoList.add(ColumnProcessStartDatetime);
            _columnInfoList.add(ColumnProcessForecastEndDatetime);
            _columnInfoList.add(ColumnProcessEndDatetime);
            _columnInfoList.add(ColumnStatusCode);
            _columnInfoList.add(ColumnDescription);
            _columnInfoList.add(ColumnOutputType);
            _columnInfoList.add(ColumnOutputRequestId);
            _columnInfoList.add(ColumnWbSettingCode);
            _columnInfoList.add(ColumnNoanswerVisibleCode);
            _columnInfoList.add(ColumnUnmatchVisibleCode);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputCommonId);
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
        public ForeignInfo ForeignTOutputRequest { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputRequestId, TOutputRequestDbm.GetInstance().ColumnOutputRequestId);
            return cfi("TOutputRequest", this, TOutputRequestDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTOutputSubGt { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputCommonId, TOutputSubGtDbm.GetInstance().ColumnOutputCommonId);
            return cfi("TOutputSubGt", this, TOutputSubGtDbm.GetInstance(), map, 1, true, false);
        }}
        public ForeignInfo ForeignTOutputSubCross { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputCommonId, TOutputSubCrossDbm.GetInstance().ColumnOutputCommonId);
            return cfi("TOutputSubCross", this, TOutputSubCrossDbm.GetInstance(), map, 2, true, false);
        }}
        public ForeignInfo ForeignTOutputSubFa { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputCommonId, TOutputSubFaDbm.GetInstance().ColumnOutputCommonId);
            return cfi("TOutputSubFa", this, TOutputSubFaDbm.GetInstance(), map, 3, true, false);
        }}
        public ForeignInfo ForeignTOutputSubCklist { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputCommonId, TOutputSubCklistDbm.GetInstance().ColumnOutputCommonId);
            return cfi("TOutputSubCklist", this, TOutputSubCklistDbm.GetInstance(), map, 4, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTOutputSubCklistList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputCommonId, TOutputSubCklistDbm.GetInstance().ColumnOutputCommonId);
            return cri("TOutputSubCklistList", this, TOutputSubCklistDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTOutputSubCrossList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputCommonId, TOutputSubCrossDbm.GetInstance().ColumnOutputCommonId);
            return cri("TOutputSubCrossList", this, TOutputSubCrossDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTOutputSubFaList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputCommonId, TOutputSubFaDbm.GetInstance().ColumnOutputCommonId);
            return cri("TOutputSubFaList", this, TOutputSubFaDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTOutputSubGtList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputCommonId, TOutputSubGtDbm.GetInstance().ColumnOutputCommonId);
            return cri("TOutputSubGtList", this, TOutputSubGtDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Output_Common_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Output_Common_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_COMMON";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputCommon";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_COMMON_ID = "OUTPUT_COMMON_ID";
        public static readonly String DB_NAME_ORDER_COUNT = "ORDER_COUNT";
        public static readonly String DB_NAME_TSV_FILE_PATH = "TSV_FILE_PATH";
        public static readonly String DB_NAME_EXCELBOOK_NAME_PREFIX = "EXCELBOOK_NAME_PREFIX";
        public static readonly String DB_NAME_PROCESS_START_DATETIME = "PROCESS_START_DATETIME";
        public static readonly String DB_NAME_PROCESS_FORECAST_END_DATETIME = "PROCESS_FORECAST_END_DATETIME";
        public static readonly String DB_NAME_PROCESS_END_DATETIME = "PROCESS_END_DATETIME";
        public static readonly String DB_NAME_STATUS_CODE = "STATUS_CODE";
        public static readonly String DB_NAME_DESCRIPTION = "DESCRIPTION";
        public static readonly String DB_NAME_OUTPUT_TYPE = "OUTPUT_TYPE";
        public static readonly String DB_NAME_OUTPUT_REQUEST_ID = "OUTPUT_REQUEST_ID";
        public static readonly String DB_NAME_WB_SETTING_CODE = "WB_SETTING_CODE";
        public static readonly String DB_NAME_NOANSWER_VISIBLE_CODE = "NOANSWER_VISIBLE_CODE";
        public static readonly String DB_NAME_UNMATCH_VISIBLE_CODE = "UNMATCH_VISIBLE_CODE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_COMMON_ID = "OutputCommonId";
        public static readonly String PROPERTY_NAME_ORDER_COUNT = "OrderCount";
        public static readonly String PROPERTY_NAME_TSV_FILE_PATH = "TsvFilePath";
        public static readonly String PROPERTY_NAME_EXCELBOOK_NAME_PREFIX = "ExcelbookNamePrefix";
        public static readonly String PROPERTY_NAME_PROCESS_START_DATETIME = "ProcessStartDatetime";
        public static readonly String PROPERTY_NAME_PROCESS_FORECAST_END_DATETIME = "ProcessForecastEndDatetime";
        public static readonly String PROPERTY_NAME_PROCESS_END_DATETIME = "ProcessEndDatetime";
        public static readonly String PROPERTY_NAME_STATUS_CODE = "StatusCode";
        public static readonly String PROPERTY_NAME_DESCRIPTION = "Description";
        public static readonly String PROPERTY_NAME_OUTPUT_TYPE = "OutputType";
        public static readonly String PROPERTY_NAME_OUTPUT_REQUEST_ID = "OutputRequestId";
        public static readonly String PROPERTY_NAME_WB_SETTING_CODE = "WbSettingCode";
        public static readonly String PROPERTY_NAME_NOANSWER_VISIBLE_CODE = "NoanswerVisibleCode";
        public static readonly String PROPERTY_NAME_UNMATCH_VISIBLE_CODE = "UnmatchVisibleCode";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputRequest = "TOutputRequest";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSubGt = "TOutputSubGt";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSubCross = "TOutputSubCross";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSubFa = "TOutputSubFa";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSubCklist = "TOutputSubCklist";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TOutputSubCklistList = "TOutputSubCklistList";
        public static readonly String REFERRER_PROPERTY_NAME_TOutputSubCrossList = "TOutputSubCrossList";
        public static readonly String REFERRER_PROPERTY_NAME_TOutputSubFaList = "TOutputSubFaList";
        public static readonly String REFERRER_PROPERTY_NAME_TOutputSubGtList = "TOutputSubGtList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputCommonDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_COMMON_ID.ToLower(), PROPERTY_NAME_OUTPUT_COMMON_ID);
                map.put(DB_NAME_ORDER_COUNT.ToLower(), PROPERTY_NAME_ORDER_COUNT);
                map.put(DB_NAME_TSV_FILE_PATH.ToLower(), PROPERTY_NAME_TSV_FILE_PATH);
                map.put(DB_NAME_EXCELBOOK_NAME_PREFIX.ToLower(), PROPERTY_NAME_EXCELBOOK_NAME_PREFIX);
                map.put(DB_NAME_PROCESS_START_DATETIME.ToLower(), PROPERTY_NAME_PROCESS_START_DATETIME);
                map.put(DB_NAME_PROCESS_FORECAST_END_DATETIME.ToLower(), PROPERTY_NAME_PROCESS_FORECAST_END_DATETIME);
                map.put(DB_NAME_PROCESS_END_DATETIME.ToLower(), PROPERTY_NAME_PROCESS_END_DATETIME);
                map.put(DB_NAME_STATUS_CODE.ToLower(), PROPERTY_NAME_STATUS_CODE);
                map.put(DB_NAME_DESCRIPTION.ToLower(), PROPERTY_NAME_DESCRIPTION);
                map.put(DB_NAME_OUTPUT_TYPE.ToLower(), PROPERTY_NAME_OUTPUT_TYPE);
                map.put(DB_NAME_OUTPUT_REQUEST_ID.ToLower(), PROPERTY_NAME_OUTPUT_REQUEST_ID);
                map.put(DB_NAME_WB_SETTING_CODE.ToLower(), PROPERTY_NAME_WB_SETTING_CODE);
                map.put(DB_NAME_NOANSWER_VISIBLE_CODE.ToLower(), PROPERTY_NAME_NOANSWER_VISIBLE_CODE);
                map.put(DB_NAME_UNMATCH_VISIBLE_CODE.ToLower(), PROPERTY_NAME_UNMATCH_VISIBLE_CODE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_COMMON_ID.ToLower(), DB_NAME_OUTPUT_COMMON_ID);
                map.put(PROPERTY_NAME_ORDER_COUNT.ToLower(), DB_NAME_ORDER_COUNT);
                map.put(PROPERTY_NAME_TSV_FILE_PATH.ToLower(), DB_NAME_TSV_FILE_PATH);
                map.put(PROPERTY_NAME_EXCELBOOK_NAME_PREFIX.ToLower(), DB_NAME_EXCELBOOK_NAME_PREFIX);
                map.put(PROPERTY_NAME_PROCESS_START_DATETIME.ToLower(), DB_NAME_PROCESS_START_DATETIME);
                map.put(PROPERTY_NAME_PROCESS_FORECAST_END_DATETIME.ToLower(), DB_NAME_PROCESS_FORECAST_END_DATETIME);
                map.put(PROPERTY_NAME_PROCESS_END_DATETIME.ToLower(), DB_NAME_PROCESS_END_DATETIME);
                map.put(PROPERTY_NAME_STATUS_CODE.ToLower(), DB_NAME_STATUS_CODE);
                map.put(PROPERTY_NAME_DESCRIPTION.ToLower(), DB_NAME_DESCRIPTION);
                map.put(PROPERTY_NAME_OUTPUT_TYPE.ToLower(), DB_NAME_OUTPUT_TYPE);
                map.put(PROPERTY_NAME_OUTPUT_REQUEST_ID.ToLower(), DB_NAME_OUTPUT_REQUEST_ID);
                map.put(PROPERTY_NAME_WB_SETTING_CODE.ToLower(), DB_NAME_WB_SETTING_CODE);
                map.put(PROPERTY_NAME_NOANSWER_VISIBLE_CODE.ToLower(), DB_NAME_NOANSWER_VISIBLE_CODE);
                map.put(PROPERTY_NAME_UNMATCH_VISIBLE_CODE.ToLower(), DB_NAME_UNMATCH_VISIBLE_CODE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputCommon"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputCommonDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputCommonCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputCommonBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputCommon NewMyEntity() { return new TOutputCommon(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputCommonCB NewMyConditionBean() { return new TOutputCommonCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputCommon>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputCommon>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_COMMON_ID", "OutputCommonId", new EntityPropertyOutputCommonIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ORDER_COUNT", "OrderCount", new EntityPropertyOrderCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TSV_FILE_PATH", "TsvFilePath", new EntityPropertyTsvFilePathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXCELBOOK_NAME_PREFIX", "ExcelbookNamePrefix", new EntityPropertyExcelbookNamePrefixSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PROCESS_START_DATETIME", "ProcessStartDatetime", new EntityPropertyProcessStartDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PROCESS_FORECAST_END_DATETIME", "ProcessForecastEndDatetime", new EntityPropertyProcessForecastEndDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PROCESS_END_DATETIME", "ProcessEndDatetime", new EntityPropertyProcessEndDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("STATUS_CODE", "StatusCode", new EntityPropertyStatusCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DESCRIPTION", "Description", new EntityPropertyDescriptionSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TYPE", "OutputType", new EntityPropertyOutputTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_REQUEST_ID", "OutputRequestId", new EntityPropertyOutputRequestIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WB_SETTING_CODE", "WbSettingCode", new EntityPropertyWbSettingCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NOANSWER_VISIBLE_CODE", "NoanswerVisibleCode", new EntityPropertyNoanswerVisibleCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UNMATCH_VISIBLE_CODE", "UnmatchVisibleCode", new EntityPropertyUnmatchVisibleCodeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputCommon> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputCommon)entity, value);
        }

        public class EntityPropertyOutputCommonIdSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.OutputCommonId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOrderCountSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.OrderCount = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyTsvFilePathSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.TsvFilePath = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyExcelbookNamePrefixSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.ExcelbookNamePrefix = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyProcessStartDatetimeSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.ProcessStartDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyProcessForecastEndDatetimeSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.ProcessForecastEndDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyProcessEndDatetimeSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.ProcessEndDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyStatusCodeSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.StatusCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDescriptionSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.Description = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOutputTypeSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.OutputType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputRequestIdSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.OutputRequestId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyWbSettingCodeSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.WbSettingCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNoanswerVisibleCodeSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.NoanswerVisibleCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyUnmatchVisibleCodeSetupper : EntityPropertySetupper<TOutputCommon> {
            public void Setup(TOutputCommon entity, Object value) { entity.UnmatchVisibleCode = (value != null) ? (int?)value : null; }
        }
    }
}
