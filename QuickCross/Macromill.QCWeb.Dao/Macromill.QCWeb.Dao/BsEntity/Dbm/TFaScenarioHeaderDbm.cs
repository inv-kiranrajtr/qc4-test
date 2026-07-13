
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

    public class TFaScenarioHeaderDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TFaScenarioHeader);

        private static readonly TFaScenarioHeaderDbm _instance = new TFaScenarioHeaderDbm();
        private TFaScenarioHeaderDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TFaScenarioHeaderDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_FA_SCENARIO_HEADER"; } }
        public override String TablePropertyName { get { return "TFaScenarioHeader"; } }
        public override String TableSqlName { get { return "T_FA_SCENARIO_HEADER"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnFaScenarioHeaderId;
        protected ColumnInfo _columnScenarioTotalizationId;
        protected ColumnInfo _columnScenarioComment;
        protected ColumnInfo _columnViewName;

        public ColumnInfo ColumnFaScenarioHeaderId { get { return _columnFaScenarioHeaderId; } }
        public ColumnInfo ColumnScenarioTotalizationId { get { return _columnScenarioTotalizationId; } }
        public ColumnInfo ColumnScenarioComment { get { return _columnScenarioComment; } }
        public ColumnInfo ColumnViewName { get { return _columnViewName; } }

        protected void InitializeColumnInfo() {
            _columnFaScenarioHeaderId = cci("FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", null, null, true, "FaScenarioHeaderId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TFaScenarioItem,TFaListAddItem", "TFaListAddItemList,TFaScenarioItemList");
            _columnScenarioTotalizationId = cci("SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", null, null, true, "ScenarioTotalizationId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TScenarioTotalization", null);
            _columnScenarioComment = cci("SCENARIO_COMMENT", "SCENARIO_COMMENT", null, null, false, "ScenarioComment", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnViewName = cci("VIEW_NAME", "VIEW_NAME", null, null, true, "ViewName", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnFaScenarioHeaderId);
            _columnInfoList.add(ColumnScenarioTotalizationId);
            _columnInfoList.add(ColumnScenarioComment);
            _columnInfoList.add(ColumnViewName);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnFaScenarioHeaderId);
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
        public ForeignInfo ForeignTScenarioTotalization { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TScenarioTotalizationDbm.GetInstance().ColumnScenarioTotalizationId);
            return cfi("TScenarioTotalization", this, TScenarioTotalizationDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTFaScenarioItem { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnFaScenarioHeaderId, TFaScenarioItemDbm.GetInstance().ColumnFaScenarioHeaderId);
            return cfi("TFaScenarioItem", this, TFaScenarioItemDbm.GetInstance(), map, 1, true, false);
        }}
        public ForeignInfo ForeignTFaListAddItem { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnFaScenarioHeaderId, TFaListAddItemDbm.GetInstance().ColumnFaScenarioHeaderId);
            return cfi("TFaListAddItem", this, TFaListAddItemDbm.GetInstance(), map, 2, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTFaListAddItemList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnFaScenarioHeaderId, TFaListAddItemDbm.GetInstance().ColumnFaScenarioHeaderId);
            return cri("TFaListAddItemList", this, TFaListAddItemDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTFaScenarioItemList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnFaScenarioHeaderId, TFaScenarioItemDbm.GetInstance().ColumnFaScenarioHeaderId);
            return cri("TFaScenarioItemList", this, TFaScenarioItemDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_FA_Scenario_Header_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_FA_Scenario_Header_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_FA_SCENARIO_HEADER";
        public static readonly String TABLE_PROPERTY_NAME = "TFaScenarioHeader";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_FA_SCENARIO_HEADER_ID = "FA_SCENARIO_HEADER_ID";
        public static readonly String DB_NAME_SCENARIO_TOTALIZATION_ID = "SCENARIO_TOTALIZATION_ID";
        public static readonly String DB_NAME_SCENARIO_COMMENT = "SCENARIO_COMMENT";
        public static readonly String DB_NAME_VIEW_NAME = "VIEW_NAME";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_FA_SCENARIO_HEADER_ID = "FaScenarioHeaderId";
        public static readonly String PROPERTY_NAME_SCENARIO_TOTALIZATION_ID = "ScenarioTotalizationId";
        public static readonly String PROPERTY_NAME_SCENARIO_COMMENT = "ScenarioComment";
        public static readonly String PROPERTY_NAME_VIEW_NAME = "ViewName";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TScenarioTotalization = "TScenarioTotalization";
        public static readonly String FOREIGN_PROPERTY_NAME_TFaScenarioItem = "TFaScenarioItem";
        public static readonly String FOREIGN_PROPERTY_NAME_TFaListAddItem = "TFaListAddItem";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TFaListAddItemList = "TFaListAddItemList";
        public static readonly String REFERRER_PROPERTY_NAME_TFaScenarioItemList = "TFaScenarioItemList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TFaScenarioHeaderDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_FA_SCENARIO_HEADER_ID.ToLower(), PROPERTY_NAME_FA_SCENARIO_HEADER_ID);
                map.put(DB_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), PROPERTY_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(DB_NAME_SCENARIO_COMMENT.ToLower(), PROPERTY_NAME_SCENARIO_COMMENT);
                map.put(DB_NAME_VIEW_NAME.ToLower(), PROPERTY_NAME_VIEW_NAME);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_FA_SCENARIO_HEADER_ID.ToLower(), DB_NAME_FA_SCENARIO_HEADER_ID);
                map.put(PROPERTY_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), DB_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(PROPERTY_NAME_SCENARIO_COMMENT.ToLower(), DB_NAME_SCENARIO_COMMENT);
                map.put(PROPERTY_NAME_VIEW_NAME.ToLower(), DB_NAME_VIEW_NAME);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TFaScenarioHeader"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TFaScenarioHeaderDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TFaScenarioHeaderCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TFaScenarioHeaderBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TFaScenarioHeader NewMyEntity() { return new TFaScenarioHeader(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TFaScenarioHeaderCB NewMyConditionBean() { return new TFaScenarioHeaderCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TFaScenarioHeader>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TFaScenarioHeader>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("FA_SCENARIO_HEADER_ID", "FaScenarioHeaderId", new EntityPropertyFaScenarioHeaderIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_TOTALIZATION_ID", "ScenarioTotalizationId", new EntityPropertyScenarioTotalizationIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_COMMENT", "ScenarioComment", new EntityPropertyScenarioCommentSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VIEW_NAME", "ViewName", new EntityPropertyViewNameSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TFaScenarioHeader> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TFaScenarioHeader)entity, value);
        }

        public class EntityPropertyFaScenarioHeaderIdSetupper : EntityPropertySetupper<TFaScenarioHeader> {
            public void Setup(TFaScenarioHeader entity, Object value) { entity.FaScenarioHeaderId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenarioTotalizationIdSetupper : EntityPropertySetupper<TFaScenarioHeader> {
            public void Setup(TFaScenarioHeader entity, Object value) { entity.ScenarioTotalizationId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenarioCommentSetupper : EntityPropertySetupper<TFaScenarioHeader> {
            public void Setup(TFaScenarioHeader entity, Object value) { entity.ScenarioComment = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyViewNameSetupper : EntityPropertySetupper<TFaScenarioHeader> {
            public void Setup(TFaScenarioHeader entity, Object value) { entity.ViewName = (value != null) ? (String)value : null; }
        }
    }
}
