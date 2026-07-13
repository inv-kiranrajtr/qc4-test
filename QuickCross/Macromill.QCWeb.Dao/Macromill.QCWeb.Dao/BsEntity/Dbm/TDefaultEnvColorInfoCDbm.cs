
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

    public class TDefaultEnvColorInfoCDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDefaultEnvColorInfoC);

        private static readonly TDefaultEnvColorInfoCDbm _instance = new TDefaultEnvColorInfoCDbm();
        private TDefaultEnvColorInfoCDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDefaultEnvColorInfoCDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_INFO_C"; } }
        public override String TablePropertyName { get { return "TDefaultEnvColorInfoC"; } }
        public override String TableSqlName { get { return "T_DEFAULT_ENV_COLOR_INFO_C"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDefEnvColorInfoCId;
        protected ColumnInfo _columnLanguage;
        protected ColumnInfo _columnTypeCode;
        protected ColumnInfo _columnGradationType;

        public ColumnInfo ColumnDefEnvColorInfoCId { get { return _columnDefEnvColorInfoCId; } }
        public ColumnInfo ColumnLanguage { get { return _columnLanguage; } }
        public ColumnInfo ColumnTypeCode { get { return _columnTypeCode; } }
        public ColumnInfo ColumnGradationType { get { return _columnGradationType; } }

        protected void InitializeColumnInfo() {
            _columnDefEnvColorInfoCId = cci("DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", null, null, true, "DefEnvColorInfoCId", typeof(int?), true, "NUMBER", 8, 0, false, OptimisticLockType.NONE, null, null, "TDefaultEnvColorDtlCList");
            _columnLanguage = cci("LANGUAGE", "LANGUAGE", null, null, true, "Language", typeof(String), false, "VARCHAR2", 5, 0, false, OptimisticLockType.NONE, null, "TDefaultEnvBase", null);
            _columnTypeCode = cci("TYPE_CODE", "TYPE_CODE", null, null, true, "TypeCode", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGradationType = cci("GRADATION_TYPE", "GRADATION_TYPE", null, null, true, "GradationType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDefEnvColorInfoCId);
            _columnInfoList.add(ColumnLanguage);
            _columnInfoList.add(ColumnTypeCode);
            _columnInfoList.add(ColumnGradationType);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnDefEnvColorInfoCId);
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
        public ForeignInfo ForeignTDefaultEnvBase { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnLanguage, TDefaultEnvBaseDbm.GetInstance().ColumnLanguage);
            return cfi("TDefaultEnvBase", this, TDefaultEnvBaseDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTDefaultEnvColorDtlCList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDefEnvColorInfoCId, TDefaultEnvColorDtlCDbm.GetInstance().ColumnDefEnvColorInfoCId);
            return cri("TDefaultEnvColorDtlCList", this, TDefaultEnvColorDtlCDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Default_Env_Color_Info_CSEQ1"; } }
        public override String SequenceNextValSql { get { return "select T_Default_Env_Color_Info_CSEQ1.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_DEFAULT_ENV_COLOR_INFO_C";
        public static readonly String TABLE_PROPERTY_NAME = "TDefaultEnvColorInfoC";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DEF_ENV_COLOR_INFO_C_ID = "DEF_ENV_COLOR_INFO_C_ID";
        public static readonly String DB_NAME_LANGUAGE = "LANGUAGE";
        public static readonly String DB_NAME_TYPE_CODE = "TYPE_CODE";
        public static readonly String DB_NAME_GRADATION_TYPE = "GRADATION_TYPE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DEF_ENV_COLOR_INFO_C_ID = "DefEnvColorInfoCId";
        public static readonly String PROPERTY_NAME_LANGUAGE = "Language";
        public static readonly String PROPERTY_NAME_TYPE_CODE = "TypeCode";
        public static readonly String PROPERTY_NAME_GRADATION_TYPE = "GradationType";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TDefaultEnvBase = "TDefaultEnvBase";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TDefaultEnvColorDtlCList = "TDefaultEnvColorDtlCList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TDefaultEnvColorInfoCDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DEF_ENV_COLOR_INFO_C_ID.ToLower(), PROPERTY_NAME_DEF_ENV_COLOR_INFO_C_ID);
                map.put(DB_NAME_LANGUAGE.ToLower(), PROPERTY_NAME_LANGUAGE);
                map.put(DB_NAME_TYPE_CODE.ToLower(), PROPERTY_NAME_TYPE_CODE);
                map.put(DB_NAME_GRADATION_TYPE.ToLower(), PROPERTY_NAME_GRADATION_TYPE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DEF_ENV_COLOR_INFO_C_ID.ToLower(), DB_NAME_DEF_ENV_COLOR_INFO_C_ID);
                map.put(PROPERTY_NAME_LANGUAGE.ToLower(), DB_NAME_LANGUAGE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDefaultEnvColorInfoC"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDefaultEnvColorInfoCDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDefaultEnvColorInfoCCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDefaultEnvColorInfoCBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDefaultEnvColorInfoC NewMyEntity() { return new TDefaultEnvColorInfoC(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDefaultEnvColorInfoCCB NewMyConditionBean() { return new TDefaultEnvColorInfoCCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDefaultEnvColorInfoC>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDefaultEnvColorInfoC>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DEF_ENV_COLOR_INFO_C_ID", "DefEnvColorInfoCId", new EntityPropertyDefEnvColorInfoCIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LANGUAGE", "Language", new EntityPropertyLanguageSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TYPE_CODE", "TypeCode", new EntityPropertyTypeCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRADATION_TYPE", "GradationType", new EntityPropertyGradationTypeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDefaultEnvColorInfoC> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDefaultEnvColorInfoC)entity, value);
        }

        public class EntityPropertyDefEnvColorInfoCIdSetupper : EntityPropertySetupper<TDefaultEnvColorInfoC> {
            public void Setup(TDefaultEnvColorInfoC entity, Object value) { entity.DefEnvColorInfoCId = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLanguageSetupper : EntityPropertySetupper<TDefaultEnvColorInfoC> {
            public void Setup(TDefaultEnvColorInfoC entity, Object value) { entity.Language = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyTypeCodeSetupper : EntityPropertySetupper<TDefaultEnvColorInfoC> {
            public void Setup(TDefaultEnvColorInfoC entity, Object value) { entity.TypeCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGradationTypeSetupper : EntityPropertySetupper<TDefaultEnvColorInfoC> {
            public void Setup(TDefaultEnvColorInfoC entity, Object value) { entity.GradationType = (value != null) ? (String)value : null; }
        }
    }
}
