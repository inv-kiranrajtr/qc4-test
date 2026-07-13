
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

    public class TSurveyInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TSurveyInfo);

        private static readonly TSurveyInfoDbm _instance = new TSurveyInfoDbm();
        private TSurveyInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TSurveyInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_SURVEY_INFO"; } }
        public override String TablePropertyName { get { return "TSurveyInfo"; } }
        public override String TableSqlName { get { return "T_SURVEY_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnSurveyInfoId;
        protected ColumnInfo _columnMainSurveyId;
        protected ColumnInfo _columnScheduleDeleteDate;
        protected ColumnInfo _columnDeleteFlag;

        public ColumnInfo ColumnSurveyInfoId { get { return _columnSurveyInfoId; } }
        public ColumnInfo ColumnMainSurveyId { get { return _columnMainSurveyId; } }
        public ColumnInfo ColumnScheduleDeleteDate { get { return _columnScheduleDeleteDate; } }
        public ColumnInfo ColumnDeleteFlag { get { return _columnDeleteFlag; } }

        protected void InitializeColumnInfo() {
            _columnSurveyInfoId = cci("SURVEY_INFO_ID", "SURVEY_INFO_ID", null, null, true, "SurveyInfoId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TQcwebSurveyInfoList");
            _columnMainSurveyId = cci("MAIN_SURVEY_ID", "MAIN_SURVEY_ID", null, null, true, "MainSurveyId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScheduleDeleteDate = cci("SCHEDULE_DELETE_DATE", "SCHEDULE_DELETE_DATE", null, null, true, "ScheduleDeleteDate", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnDeleteFlag = cci("DELETE_FLAG", "DELETE_FLAG", null, null, true, "DeleteFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnSurveyInfoId);
            _columnInfoList.add(ColumnMainSurveyId);
            _columnInfoList.add(ColumnScheduleDeleteDate);
            _columnInfoList.add(ColumnDeleteFlag);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnSurveyInfoId);
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


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTQcwebSurveyInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnSurveyInfoId, TQcwebSurveyInfoDbm.GetInstance().ColumnSurveyInfoId);
            return cri("TQcwebSurveyInfoList", this, TQcwebSurveyInfoDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Survey_Info_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Survey_Info_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_SURVEY_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TSurveyInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_SURVEY_INFO_ID = "SURVEY_INFO_ID";
        public static readonly String DB_NAME_MAIN_SURVEY_ID = "MAIN_SURVEY_ID";
        public static readonly String DB_NAME_SCHEDULE_DELETE_DATE = "SCHEDULE_DELETE_DATE";
        public static readonly String DB_NAME_DELETE_FLAG = "DELETE_FLAG";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_SURVEY_INFO_ID = "SurveyInfoId";
        public static readonly String PROPERTY_NAME_MAIN_SURVEY_ID = "MainSurveyId";
        public static readonly String PROPERTY_NAME_SCHEDULE_DELETE_DATE = "ScheduleDeleteDate";
        public static readonly String PROPERTY_NAME_DELETE_FLAG = "DeleteFlag";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TQcwebSurveyInfoList = "TQcwebSurveyInfoList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TSurveyInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_SURVEY_INFO_ID.ToLower(), PROPERTY_NAME_SURVEY_INFO_ID);
                map.put(DB_NAME_MAIN_SURVEY_ID.ToLower(), PROPERTY_NAME_MAIN_SURVEY_ID);
                map.put(DB_NAME_SCHEDULE_DELETE_DATE.ToLower(), PROPERTY_NAME_SCHEDULE_DELETE_DATE);
                map.put(DB_NAME_DELETE_FLAG.ToLower(), PROPERTY_NAME_DELETE_FLAG);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_SURVEY_INFO_ID.ToLower(), DB_NAME_SURVEY_INFO_ID);
                map.put(PROPERTY_NAME_MAIN_SURVEY_ID.ToLower(), DB_NAME_MAIN_SURVEY_ID);
                map.put(PROPERTY_NAME_SCHEDULE_DELETE_DATE.ToLower(), DB_NAME_SCHEDULE_DELETE_DATE);
                map.put(PROPERTY_NAME_DELETE_FLAG.ToLower(), DB_NAME_DELETE_FLAG);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TSurveyInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TSurveyInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TSurveyInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TSurveyInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TSurveyInfo NewMyEntity() { return new TSurveyInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TSurveyInfoCB NewMyConditionBean() { return new TSurveyInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TSurveyInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TSurveyInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("SURVEY_INFO_ID", "SurveyInfoId", new EntityPropertySurveyInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MAIN_SURVEY_ID", "MainSurveyId", new EntityPropertyMainSurveyIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCHEDULE_DELETE_DATE", "ScheduleDeleteDate", new EntityPropertyScheduleDeleteDateSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_FLAG", "DeleteFlag", new EntityPropertyDeleteFlagSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TSurveyInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TSurveyInfo)entity, value);
        }

        public class EntityPropertySurveyInfoIdSetupper : EntityPropertySetupper<TSurveyInfo> {
            public void Setup(TSurveyInfo entity, Object value) { entity.SurveyInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyMainSurveyIdSetupper : EntityPropertySetupper<TSurveyInfo> {
            public void Setup(TSurveyInfo entity, Object value) { entity.MainSurveyId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScheduleDeleteDateSetupper : EntityPropertySetupper<TSurveyInfo> {
            public void Setup(TSurveyInfo entity, Object value) { entity.ScheduleDeleteDate = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyDeleteFlagSetupper : EntityPropertySetupper<TSurveyInfo> {
            public void Setup(TSurveyInfo entity, Object value) { entity.DeleteFlag = (value != null) ? (int?)value : null; }
        }
    }
}
