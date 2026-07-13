
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

    public class TDefaultEnvColorDtlDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDefaultEnvColorDtl);

        private static readonly TDefaultEnvColorDtlDbm _instance = new TDefaultEnvColorDtlDbm();
        private TDefaultEnvColorDtlDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDefaultEnvColorDtlDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_DTL"; } }
        public override String TablePropertyName { get { return "TDefaultEnvColorDtl"; } }
        public override String TableSqlName { get { return "T_DEFAULT_ENV_COLOR_DTL"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDefEnvColorDtlId;
        protected ColumnInfo _columnDefEnvColorInfoId;
        protected ColumnInfo _columnGraphColorNo;
        protected ColumnInfo _columnColorCode;
        protected ColumnInfo _columnPatternCode;

        public ColumnInfo ColumnDefEnvColorDtlId { get { return _columnDefEnvColorDtlId; } }
        public ColumnInfo ColumnDefEnvColorInfoId { get { return _columnDefEnvColorInfoId; } }
        public ColumnInfo ColumnGraphColorNo { get { return _columnGraphColorNo; } }
        public ColumnInfo ColumnColorCode { get { return _columnColorCode; } }
        public ColumnInfo ColumnPatternCode { get { return _columnPatternCode; } }

        protected void InitializeColumnInfo() {
            _columnDefEnvColorDtlId = cci("DEF_ENV_COLOR_DTL_ID", "DEF_ENV_COLOR_DTL_ID", null, null, true, "DefEnvColorDtlId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDefEnvColorInfoId = cci("DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", null, null, true, "DefEnvColorInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDefaultEnvColorInfo", null);
            _columnGraphColorNo = cci("GRAPH_COLOR_NO", "GRAPH_COLOR_NO", null, null, true, "GraphColorNo", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColorCode = cci("COLOR_CODE", "COLOR_CODE", null, null, true, "ColorCode", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPatternCode = cci("PATTERN_CODE", "PATTERN_CODE", null, null, false, "PatternCode", typeof(String), false, "VARCHAR2", 2, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDefEnvColorDtlId);
            _columnInfoList.add(ColumnDefEnvColorInfoId);
            _columnInfoList.add(ColumnGraphColorNo);
            _columnInfoList.add(ColumnColorCode);
            _columnInfoList.add(ColumnPatternCode);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnDefEnvColorDtlId);
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
        public ForeignInfo ForeignTDefaultEnvColorInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDefEnvColorInfoId, TDefaultEnvColorInfoDbm.GetInstance().ColumnDefEnvColorInfoId);
            return cfi("TDefaultEnvColorInfo", this, TDefaultEnvColorInfoDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Default_Env_Color_Dtl_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Default_Env_Color_Dtl_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_DEFAULT_ENV_COLOR_DTL";
        public static readonly String TABLE_PROPERTY_NAME = "TDefaultEnvColorDtl";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DEF_ENV_COLOR_DTL_ID = "DEF_ENV_COLOR_DTL_ID";
        public static readonly String DB_NAME_DEF_ENV_COLOR_INFO_ID = "DEF_ENV_COLOR_INFO_ID";
        public static readonly String DB_NAME_GRAPH_COLOR_NO = "GRAPH_COLOR_NO";
        public static readonly String DB_NAME_COLOR_CODE = "COLOR_CODE";
        public static readonly String DB_NAME_PATTERN_CODE = "PATTERN_CODE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DEF_ENV_COLOR_DTL_ID = "DefEnvColorDtlId";
        public static readonly String PROPERTY_NAME_DEF_ENV_COLOR_INFO_ID = "DefEnvColorInfoId";
        public static readonly String PROPERTY_NAME_GRAPH_COLOR_NO = "GraphColorNo";
        public static readonly String PROPERTY_NAME_COLOR_CODE = "ColorCode";
        public static readonly String PROPERTY_NAME_PATTERN_CODE = "PatternCode";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TDefaultEnvColorInfo = "TDefaultEnvColorInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TDefaultEnvColorDtlDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DEF_ENV_COLOR_DTL_ID.ToLower(), PROPERTY_NAME_DEF_ENV_COLOR_DTL_ID);
                map.put(DB_NAME_DEF_ENV_COLOR_INFO_ID.ToLower(), PROPERTY_NAME_DEF_ENV_COLOR_INFO_ID);
                map.put(DB_NAME_GRAPH_COLOR_NO.ToLower(), PROPERTY_NAME_GRAPH_COLOR_NO);
                map.put(DB_NAME_COLOR_CODE.ToLower(), PROPERTY_NAME_COLOR_CODE);
                map.put(DB_NAME_PATTERN_CODE.ToLower(), PROPERTY_NAME_PATTERN_CODE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DEF_ENV_COLOR_DTL_ID.ToLower(), DB_NAME_DEF_ENV_COLOR_DTL_ID);
                map.put(PROPERTY_NAME_DEF_ENV_COLOR_INFO_ID.ToLower(), DB_NAME_DEF_ENV_COLOR_INFO_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDefaultEnvColorDtl"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDefaultEnvColorDtlDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDefaultEnvColorDtlCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDefaultEnvColorDtlBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDefaultEnvColorDtl NewMyEntity() { return new TDefaultEnvColorDtl(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDefaultEnvColorDtlCB NewMyConditionBean() { return new TDefaultEnvColorDtlCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDefaultEnvColorDtl>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDefaultEnvColorDtl>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DEF_ENV_COLOR_DTL_ID", "DefEnvColorDtlId", new EntityPropertyDefEnvColorDtlIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DEF_ENV_COLOR_INFO_ID", "DefEnvColorInfoId", new EntityPropertyDefEnvColorInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_COLOR_NO", "GraphColorNo", new EntityPropertyGraphColorNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLOR_CODE", "ColorCode", new EntityPropertyColorCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PATTERN_CODE", "PatternCode", new EntityPropertyPatternCodeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDefaultEnvColorDtl> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDefaultEnvColorDtl)entity, value);
        }

        public class EntityPropertyDefEnvColorDtlIdSetupper : EntityPropertySetupper<TDefaultEnvColorDtl> {
            public void Setup(TDefaultEnvColorDtl entity, Object value) { entity.DefEnvColorDtlId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDefEnvColorInfoIdSetupper : EntityPropertySetupper<TDefaultEnvColorDtl> {
            public void Setup(TDefaultEnvColorDtl entity, Object value) { entity.DefEnvColorInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyGraphColorNoSetupper : EntityPropertySetupper<TDefaultEnvColorDtl> {
            public void Setup(TDefaultEnvColorDtl entity, Object value) { entity.GraphColorNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyColorCodeSetupper : EntityPropertySetupper<TDefaultEnvColorDtl> {
            public void Setup(TDefaultEnvColorDtl entity, Object value) { entity.ColorCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPatternCodeSetupper : EntityPropertySetupper<TDefaultEnvColorDtl> {
            public void Setup(TDefaultEnvColorDtl entity, Object value) { entity.PatternCode = (value != null) ? (String)value : null; }
        }
    }
}
