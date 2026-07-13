
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

    public class TDefaultEnvColorDtlCDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDefaultEnvColorDtlC);

        private static readonly TDefaultEnvColorDtlCDbm _instance = new TDefaultEnvColorDtlCDbm();
        private TDefaultEnvColorDtlCDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDefaultEnvColorDtlCDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_DTL_C"; } }
        public override String TablePropertyName { get { return "TDefaultEnvColorDtlC"; } }
        public override String TableSqlName { get { return "T_DEFAULT_ENV_COLOR_DTL_C"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDefEnvColorDtlCId;
        protected ColumnInfo _columnDefEnvColorInfoCId;
        protected ColumnInfo _columnGraphColorNo;
        protected ColumnInfo _columnColorCode;
        protected ColumnInfo _columnPatternCode;

        public ColumnInfo ColumnDefEnvColorDtlCId { get { return _columnDefEnvColorDtlCId; } }
        public ColumnInfo ColumnDefEnvColorInfoCId { get { return _columnDefEnvColorInfoCId; } }
        public ColumnInfo ColumnGraphColorNo { get { return _columnGraphColorNo; } }
        public ColumnInfo ColumnColorCode { get { return _columnColorCode; } }
        public ColumnInfo ColumnPatternCode { get { return _columnPatternCode; } }

        protected void InitializeColumnInfo() {
            _columnDefEnvColorDtlCId = cci("DEF_ENV_COLOR_DTL_C_ID", "DEF_ENV_COLOR_DTL_C_ID", null, null, true, "DefEnvColorDtlCId", typeof(int?), true, "NUMBER", 8, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDefEnvColorInfoCId = cci("DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", null, null, true, "DefEnvColorInfoCId", typeof(int?), false, "NUMBER", 8, 0, false, OptimisticLockType.NONE, null, "TDefaultEnvColorInfoC", null);
            _columnGraphColorNo = cci("GRAPH_COLOR_NO", "GRAPH_COLOR_NO", null, null, true, "GraphColorNo", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColorCode = cci("COLOR_CODE", "COLOR_CODE", null, null, true, "ColorCode", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPatternCode = cci("PATTERN_CODE", "PATTERN_CODE", null, null, false, "PatternCode", typeof(String), false, "VARCHAR2", 2, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDefEnvColorDtlCId);
            _columnInfoList.add(ColumnDefEnvColorInfoCId);
            _columnInfoList.add(ColumnGraphColorNo);
            _columnInfoList.add(ColumnColorCode);
            _columnInfoList.add(ColumnPatternCode);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnDefEnvColorDtlCId);
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
        public ForeignInfo ForeignTDefaultEnvColorInfoC { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDefEnvColorInfoCId, TDefaultEnvColorInfoCDbm.GetInstance().ColumnDefEnvColorInfoCId);
            return cfi("TDefaultEnvColorInfoC", this, TDefaultEnvColorInfoCDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Default_Env_Color_Dtl_C_SEQ1"; } }
        public override String SequenceNextValSql { get { return "select T_Default_Env_Color_Dtl_C_SEQ1.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_DEFAULT_ENV_COLOR_DTL_C";
        public static readonly String TABLE_PROPERTY_NAME = "TDefaultEnvColorDtlC";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DEF_ENV_COLOR_DTL_C_ID = "DEF_ENV_COLOR_DTL_C_ID";
        public static readonly String DB_NAME_DEF_ENV_COLOR_INFO_C_ID = "DEF_ENV_COLOR_INFO_C_ID";
        public static readonly String DB_NAME_GRAPH_COLOR_NO = "GRAPH_COLOR_NO";
        public static readonly String DB_NAME_COLOR_CODE = "COLOR_CODE";
        public static readonly String DB_NAME_PATTERN_CODE = "PATTERN_CODE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DEF_ENV_COLOR_DTL_C_ID = "DefEnvColorDtlCId";
        public static readonly String PROPERTY_NAME_DEF_ENV_COLOR_INFO_C_ID = "DefEnvColorInfoCId";
        public static readonly String PROPERTY_NAME_GRAPH_COLOR_NO = "GraphColorNo";
        public static readonly String PROPERTY_NAME_COLOR_CODE = "ColorCode";
        public static readonly String PROPERTY_NAME_PATTERN_CODE = "PatternCode";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TDefaultEnvColorInfoC = "TDefaultEnvColorInfoC";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TDefaultEnvColorDtlCDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DEF_ENV_COLOR_DTL_C_ID.ToLower(), PROPERTY_NAME_DEF_ENV_COLOR_DTL_C_ID);
                map.put(DB_NAME_DEF_ENV_COLOR_INFO_C_ID.ToLower(), PROPERTY_NAME_DEF_ENV_COLOR_INFO_C_ID);
                map.put(DB_NAME_GRAPH_COLOR_NO.ToLower(), PROPERTY_NAME_GRAPH_COLOR_NO);
                map.put(DB_NAME_COLOR_CODE.ToLower(), PROPERTY_NAME_COLOR_CODE);
                map.put(DB_NAME_PATTERN_CODE.ToLower(), PROPERTY_NAME_PATTERN_CODE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DEF_ENV_COLOR_DTL_C_ID.ToLower(), DB_NAME_DEF_ENV_COLOR_DTL_C_ID);
                map.put(PROPERTY_NAME_DEF_ENV_COLOR_INFO_C_ID.ToLower(), DB_NAME_DEF_ENV_COLOR_INFO_C_ID);
                map.put(PROPERTY_NAME_GRAPH_COLOR_NO.ToLower(), DB_NAME_GRAPH_COLOR_NO);
                map.put(PROPERTY_NAME_COLOR_CODE.ToLower(), DB_NAME_COLOR_CODE);
                map.put(PROPERTY_NAME_PATTERN_CODE.ToLower(), DB_NAME_PATTERN_CODE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDefaultEnvColorDtlC"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDefaultEnvColorDtlCDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDefaultEnvColorDtlCCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDefaultEnvColorDtlCBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDefaultEnvColorDtlC NewMyEntity() { return new TDefaultEnvColorDtlC(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDefaultEnvColorDtlCCB NewMyConditionBean() { return new TDefaultEnvColorDtlCCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDefaultEnvColorDtlC>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDefaultEnvColorDtlC>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DEF_ENV_COLOR_DTL_C_ID", "DefEnvColorDtlCId", new EntityPropertyDefEnvColorDtlCIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DEF_ENV_COLOR_INFO_C_ID", "DefEnvColorInfoCId", new EntityPropertyDefEnvColorInfoCIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_COLOR_NO", "GraphColorNo", new EntityPropertyGraphColorNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLOR_CODE", "ColorCode", new EntityPropertyColorCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PATTERN_CODE", "PatternCode", new EntityPropertyPatternCodeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDefaultEnvColorDtlC> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDefaultEnvColorDtlC)entity, value);
        }

        public class EntityPropertyDefEnvColorDtlCIdSetupper : EntityPropertySetupper<TDefaultEnvColorDtlC> {
            public void Setup(TDefaultEnvColorDtlC entity, Object value) { entity.DefEnvColorDtlCId = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDefEnvColorInfoCIdSetupper : EntityPropertySetupper<TDefaultEnvColorDtlC> {
            public void Setup(TDefaultEnvColorDtlC entity, Object value) { entity.DefEnvColorInfoCId = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyGraphColorNoSetupper : EntityPropertySetupper<TDefaultEnvColorDtlC> {
            public void Setup(TDefaultEnvColorDtlC entity, Object value) { entity.GraphColorNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyColorCodeSetupper : EntityPropertySetupper<TDefaultEnvColorDtlC> {
            public void Setup(TDefaultEnvColorDtlC entity, Object value) { entity.ColorCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPatternCodeSetupper : EntityPropertySetupper<TDefaultEnvColorDtlC> {
            public void Setup(TDefaultEnvColorDtlC entity, Object value) { entity.PatternCode = (value != null) ? (String)value : null; }
        }
    }
}
