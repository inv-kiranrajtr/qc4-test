
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

    public class TOutputTemplateDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputTemplate);

        private static readonly TOutputTemplateDbm _instance = new TOutputTemplateDbm();
        private TOutputTemplateDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputTemplateDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_TEMPLATE"; } }
        public override String TablePropertyName { get { return "TOutputTemplate"; } }
        public override String TableSqlName { get { return "T_OUTPUT_TEMPLATE"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputTemplateId;
        protected ColumnInfo _columnOutputTemplateMasterId;
        protected ColumnInfo _columnUploadPath;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnAlias;
        protected ColumnInfo _columnCreateDatetime;
        protected ColumnInfo _columnDeleteFlag;

        public ColumnInfo ColumnOutputTemplateId { get { return _columnOutputTemplateId; } }
        public ColumnInfo ColumnOutputTemplateMasterId { get { return _columnOutputTemplateMasterId; } }
        public ColumnInfo ColumnUploadPath { get { return _columnUploadPath; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnAlias { get { return _columnAlias; } }
        public ColumnInfo ColumnCreateDatetime { get { return _columnCreateDatetime; } }
        public ColumnInfo ColumnDeleteFlag { get { return _columnDeleteFlag; } }

        protected void InitializeColumnInfo() {
            _columnOutputTemplateId = cci("OUTPUT_TEMPLATE_ID", "OUTPUT_TEMPLATE_ID", null, null, true, "OutputTemplateId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TOutputReportsetInfoList");
            _columnOutputTemplateMasterId = cci("OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", null, null, false, "OutputTemplateMasterId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputTemplateMaster", null);
            _columnUploadPath = cci("UPLOAD_PATH", "UPLOAD_PATH", null, null, true, "UploadPath", typeof(String), false, "VARCHAR2", 780, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo", null);
            _columnAlias = cci("ALIAS", "ALIAS", null, null, true, "Alias", typeof(String), false, "VARCHAR2", 780, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCreateDatetime = cci("CREATE_DATETIME", "CREATE_DATETIME", null, null, true, "CreateDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnDeleteFlag = cci("DELETE_FLAG", "DELETE_FLAG", null, null, true, "DeleteFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputTemplateId);
            _columnInfoList.add(ColumnOutputTemplateMasterId);
            _columnInfoList.add(ColumnUploadPath);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnAlias);
            _columnInfoList.add(ColumnCreateDatetime);
            _columnInfoList.add(ColumnDeleteFlag);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputTemplateId);
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
        public ForeignInfo ForeignTOutputTemplateMaster { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputTemplateMasterId, TOutputTemplateMasterDbm.GetInstance().ColumnOutputTemplateMasterId);
            return cfi("TOutputTemplateMaster", this, TOutputTemplateMasterDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTQcwebSurveyInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfo", this, TQcwebSurveyInfoDbm.GetInstance(), map, 1, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTOutputReportsetInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputTemplateId, TOutputReportsetInfoDbm.GetInstance().ColumnOutputTemplateId);
            return cri("TOutputReportsetInfoList", this, TOutputReportsetInfoDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Output_Template_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Output_Template_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_TEMPLATE";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputTemplate";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_TEMPLATE_ID = "OUTPUT_TEMPLATE_ID";
        public static readonly String DB_NAME_OUTPUT_TEMPLATE_MASTER_ID = "OUTPUT_TEMPLATE_MASTER_ID";
        public static readonly String DB_NAME_UPLOAD_PATH = "UPLOAD_PATH";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_ALIAS = "ALIAS";
        public static readonly String DB_NAME_CREATE_DATETIME = "CREATE_DATETIME";
        public static readonly String DB_NAME_DELETE_FLAG = "DELETE_FLAG";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_TEMPLATE_ID = "OutputTemplateId";
        public static readonly String PROPERTY_NAME_OUTPUT_TEMPLATE_MASTER_ID = "OutputTemplateMasterId";
        public static readonly String PROPERTY_NAME_UPLOAD_PATH = "UploadPath";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_ALIAS = "Alias";
        public static readonly String PROPERTY_NAME_CREATE_DATETIME = "CreateDatetime";
        public static readonly String PROPERTY_NAME_DELETE_FLAG = "DeleteFlag";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputTemplateMaster = "TOutputTemplateMaster";
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TOutputReportsetInfoList = "TOutputReportsetInfoList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputTemplateDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_TEMPLATE_ID.ToLower(), PROPERTY_NAME_OUTPUT_TEMPLATE_ID);
                map.put(DB_NAME_OUTPUT_TEMPLATE_MASTER_ID.ToLower(), PROPERTY_NAME_OUTPUT_TEMPLATE_MASTER_ID);
                map.put(DB_NAME_UPLOAD_PATH.ToLower(), PROPERTY_NAME_UPLOAD_PATH);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_ALIAS.ToLower(), PROPERTY_NAME_ALIAS);
                map.put(DB_NAME_CREATE_DATETIME.ToLower(), PROPERTY_NAME_CREATE_DATETIME);
                map.put(DB_NAME_DELETE_FLAG.ToLower(), PROPERTY_NAME_DELETE_FLAG);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_TEMPLATE_ID.ToLower(), DB_NAME_OUTPUT_TEMPLATE_ID);
                map.put(PROPERTY_NAME_OUTPUT_TEMPLATE_MASTER_ID.ToLower(), DB_NAME_OUTPUT_TEMPLATE_MASTER_ID);
                map.put(PROPERTY_NAME_UPLOAD_PATH.ToLower(), DB_NAME_UPLOAD_PATH);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_ALIAS.ToLower(), DB_NAME_ALIAS);
                map.put(PROPERTY_NAME_CREATE_DATETIME.ToLower(), DB_NAME_CREATE_DATETIME);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputTemplate"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputTemplateDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputTemplateCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputTemplateBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputTemplate NewMyEntity() { return new TOutputTemplate(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputTemplateCB NewMyConditionBean() { return new TOutputTemplateCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputTemplate>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputTemplate>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_TEMPLATE_ID", "OutputTemplateId", new EntityPropertyOutputTemplateIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TEMPLATE_MASTER_ID", "OutputTemplateMasterId", new EntityPropertyOutputTemplateMasterIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UPLOAD_PATH", "UploadPath", new EntityPropertyUploadPathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ALIAS", "Alias", new EntityPropertyAliasSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CREATE_DATETIME", "CreateDatetime", new EntityPropertyCreateDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_FLAG", "DeleteFlag", new EntityPropertyDeleteFlagSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputTemplate> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputTemplate)entity, value);
        }

        public class EntityPropertyOutputTemplateIdSetupper : EntityPropertySetupper<TOutputTemplate> {
            public void Setup(TOutputTemplate entity, Object value) { entity.OutputTemplateId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputTemplateMasterIdSetupper : EntityPropertySetupper<TOutputTemplate> {
            public void Setup(TOutputTemplate entity, Object value) { entity.OutputTemplateMasterId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyUploadPathSetupper : EntityPropertySetupper<TOutputTemplate> {
            public void Setup(TOutputTemplate entity, Object value) { entity.UploadPath = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputTemplate> {
            public void Setup(TOutputTemplate entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAliasSetupper : EntityPropertySetupper<TOutputTemplate> {
            public void Setup(TOutputTemplate entity, Object value) { entity.Alias = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCreateDatetimeSetupper : EntityPropertySetupper<TOutputTemplate> {
            public void Setup(TOutputTemplate entity, Object value) { entity.CreateDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyDeleteFlagSetupper : EntityPropertySetupper<TOutputTemplate> {
            public void Setup(TOutputTemplate entity, Object value) { entity.DeleteFlag = (value != null) ? (int?)value : null; }
        }
    }
}
