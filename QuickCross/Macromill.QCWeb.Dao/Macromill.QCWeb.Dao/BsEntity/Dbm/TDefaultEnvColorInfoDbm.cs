
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

    public class TDefaultEnvColorInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDefaultEnvColorInfo);

        private static readonly TDefaultEnvColorInfoDbm _instance = new TDefaultEnvColorInfoDbm();
        private TDefaultEnvColorInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDefaultEnvColorInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_INFO"; } }
        public override String TablePropertyName { get { return "TDefaultEnvColorInfo"; } }
        public override String TableSqlName { get { return "T_DEFAULT_ENV_COLOR_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDefEnvColorInfoId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnTypeCode;
        protected ColumnInfo _columnGradationType;

        public ColumnInfo ColumnDefEnvColorInfoId { get { return _columnDefEnvColorInfoId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnTypeCode { get { return _columnTypeCode; } }
        public ColumnInfo ColumnGradationType { get { return _columnGradationType; } }

        protected void InitializeColumnInfo() {
            _columnDefEnvColorInfoId = cci("DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", null, null, true, "DefEnvColorInfoId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDefaultEnvColorDtl", "TDefaultEnvColorDtlList");
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDefaultEnv", null);
            _columnTypeCode = cci("TYPE_CODE", "TYPE_CODE", null, null, true, "TypeCode", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGradationType = cci("GRADATION_TYPE", "GRADATION_TYPE", null, null, true, "GradationType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDefEnvColorInfoId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnTypeCode);
            _columnInfoList.add(ColumnGradationType);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnDefEnvColorInfoId);
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
        public ForeignInfo ForeignTDefaultEnv { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TDefaultEnvDbm.GetInstance().ColumnQcwebid);
            return cfi("TDefaultEnv", this, TDefaultEnvDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTDefaultEnvColorDtl { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDefEnvColorInfoId, TDefaultEnvColorDtlDbm.GetInstance().ColumnDefEnvColorInfoId);
            return cfi("TDefaultEnvColorDtl", this, TDefaultEnvColorDtlDbm.GetInstance(), map, 1, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTDefaultEnvColorDtlList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDefEnvColorInfoId, TDefaultEnvColorDtlDbm.GetInstance().ColumnDefEnvColorInfoId);
            return cri("TDefaultEnvColorDtlList", this, TDefaultEnvColorDtlDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Default_Env_Color_Info_SEQ_1"; } }
        public override String SequenceNextValSql { get { return "select T_Default_Env_Color_Info_SEQ_1.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_DEFAULT_ENV_COLOR_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TDefaultEnvColorInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DEF_ENV_COLOR_INFO_ID = "DEF_ENV_COLOR_INFO_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_TYPE_CODE = "TYPE_CODE";
        public static readonly String DB_NAME_GRADATION_TYPE = "GRADATION_TYPE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DEF_ENV_COLOR_INFO_ID = "DefEnvColorInfoId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_TYPE_CODE = "TypeCode";
        public static readonly String PROPERTY_NAME_GRADATION_TYPE = "GradationType";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TDefaultEnv = "TDefaultEnv";
        public static readonly String FOREIGN_PROPERTY_NAME_TDefaultEnvColorDtl = "TDefaultEnvColorDtl";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TDefaultEnvColorDtlList = "TDefaultEnvColorDtlList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TDefaultEnvColorInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DEF_ENV_COLOR_INFO_ID.ToLower(), PROPERTY_NAME_DEF_ENV_COLOR_INFO_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_TYPE_CODE.ToLower(), PROPERTY_NAME_TYPE_CODE);
                map.put(DB_NAME_GRADATION_TYPE.ToLower(), PROPERTY_NAME_GRADATION_TYPE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DEF_ENV_COLOR_INFO_ID.ToLower(), DB_NAME_DEF_ENV_COLOR_INFO_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_TYPE_CODE.ToLower(), DB_NAME_TYPE_CODE);
                map.put(PROPERTY_NAME_GRADATION_TYPE.ToLower(), DB_NAME_GRADATION_TYPE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDefaultEnvColorInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDefaultEnvColorInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDefaultEnvColorInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDefaultEnvColorInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDefaultEnvColorInfo NewMyEntity() { return new TDefaultEnvColorInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDefaultEnvColorInfoCB NewMyConditionBean() { return new TDefaultEnvColorInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDefaultEnvColorInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDefaultEnvColorInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DEF_ENV_COLOR_INFO_ID", "DefEnvColorInfoId", new EntityPropertyDefEnvColorInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TYPE_CODE", "TypeCode", new EntityPropertyTypeCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRADATION_TYPE", "GradationType", new EntityPropertyGradationTypeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDefaultEnvColorInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDefaultEnvColorInfo)entity, value);
        }

        public class EntityPropertyDefEnvColorInfoIdSetupper : EntityPropertySetupper<TDefaultEnvColorInfo> {
            public void Setup(TDefaultEnvColorInfo entity, Object value) { entity.DefEnvColorInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TDefaultEnvColorInfo> {
            public void Setup(TDefaultEnvColorInfo entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTypeCodeSetupper : EntityPropertySetupper<TDefaultEnvColorInfo> {
            public void Setup(TDefaultEnvColorInfo entity, Object value) { entity.TypeCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGradationTypeSetupper : EntityPropertySetupper<TDefaultEnvColorInfo> {
            public void Setup(TDefaultEnvColorInfo entity, Object value) { entity.GradationType = (value != null) ? (String)value : null; }
        }
    }
}
