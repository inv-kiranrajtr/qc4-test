
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

    public class TScenarioQuerylistDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TScenarioQuerylist);

        private static readonly TScenarioQuerylistDbm _instance = new TScenarioQuerylistDbm();
        private TScenarioQuerylistDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TScenarioQuerylistDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_SCENARIO_QUERYLIST"; } }
        public override String TablePropertyName { get { return "TScenarioQuerylist"; } }
        public override String TableSqlName { get { return "T_SCENARIO_QUERYLIST"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnScenarioQuerylistId;
        protected ColumnInfo _columnScenarioTotalizationId;
        protected ColumnInfo _columnSeqNo;
        protected ColumnInfo _columnItemInfoId;
        protected ColumnInfo _columnOperationCode;
        protected ColumnInfo _columnConditionString;

        public ColumnInfo ColumnScenarioQuerylistId { get { return _columnScenarioQuerylistId; } }
        public ColumnInfo ColumnScenarioTotalizationId { get { return _columnScenarioTotalizationId; } }
        public ColumnInfo ColumnSeqNo { get { return _columnSeqNo; } }
        public ColumnInfo ColumnItemInfoId { get { return _columnItemInfoId; } }
        public ColumnInfo ColumnOperationCode { get { return _columnOperationCode; } }
        public ColumnInfo ColumnConditionString { get { return _columnConditionString; } }

        protected void InitializeColumnInfo() {
            _columnScenarioQuerylistId = cci("SCENARIO_QUERYLIST_ID", "SCENARIO_QUERYLIST_ID", null, null, true, "ScenarioQuerylistId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenarioTotalizationId = cci("SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", null, null, true, "ScenarioTotalizationId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TScenarioTotalization", null);
            _columnSeqNo = cci("SEQ_NO", "SEQ_NO", null, null, true, "SeqNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemInfoId = cci("ITEM_INFO_ID", "ITEM_INFO_ID", null, null, true, "ItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TItemInfo", null);
            _columnOperationCode = cci("OPERATION_CODE", "OPERATION_CODE", null, null, true, "OperationCode", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionString = cci("CONDITION_STRING", "CONDITION_STRING", null, null, true, "ConditionString", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnScenarioQuerylistId);
            _columnInfoList.add(ColumnScenarioTotalizationId);
            _columnInfoList.add(ColumnSeqNo);
            _columnInfoList.add(ColumnItemInfoId);
            _columnInfoList.add(ColumnOperationCode);
            _columnInfoList.add(ColumnConditionString);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnScenarioQuerylistId);
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
        public ForeignInfo ForeignTItemInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TItemInfoDbm.GetInstance().ColumnItemInfoId);
            return cfi("TItemInfo", this, TItemInfoDbm.GetInstance(), map, 1, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Scenario_QueryList_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Scenario_QueryList_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_SCENARIO_QUERYLIST";
        public static readonly String TABLE_PROPERTY_NAME = "TScenarioQuerylist";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_SCENARIO_QUERYLIST_ID = "SCENARIO_QUERYLIST_ID";
        public static readonly String DB_NAME_SCENARIO_TOTALIZATION_ID = "SCENARIO_TOTALIZATION_ID";
        public static readonly String DB_NAME_SEQ_NO = "SEQ_NO";
        public static readonly String DB_NAME_ITEM_INFO_ID = "ITEM_INFO_ID";
        public static readonly String DB_NAME_OPERATION_CODE = "OPERATION_CODE";
        public static readonly String DB_NAME_CONDITION_STRING = "CONDITION_STRING";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_SCENARIO_QUERYLIST_ID = "ScenarioQuerylistId";
        public static readonly String PROPERTY_NAME_SCENARIO_TOTALIZATION_ID = "ScenarioTotalizationId";
        public static readonly String PROPERTY_NAME_SEQ_NO = "SeqNo";
        public static readonly String PROPERTY_NAME_ITEM_INFO_ID = "ItemInfoId";
        public static readonly String PROPERTY_NAME_OPERATION_CODE = "OperationCode";
        public static readonly String PROPERTY_NAME_CONDITION_STRING = "ConditionString";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TScenarioTotalization = "TScenarioTotalization";
        public static readonly String FOREIGN_PROPERTY_NAME_TItemInfo = "TItemInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TScenarioQuerylistDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_SCENARIO_QUERYLIST_ID.ToLower(), PROPERTY_NAME_SCENARIO_QUERYLIST_ID);
                map.put(DB_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), PROPERTY_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(DB_NAME_SEQ_NO.ToLower(), PROPERTY_NAME_SEQ_NO);
                map.put(DB_NAME_ITEM_INFO_ID.ToLower(), PROPERTY_NAME_ITEM_INFO_ID);
                map.put(DB_NAME_OPERATION_CODE.ToLower(), PROPERTY_NAME_OPERATION_CODE);
                map.put(DB_NAME_CONDITION_STRING.ToLower(), PROPERTY_NAME_CONDITION_STRING);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_SCENARIO_QUERYLIST_ID.ToLower(), DB_NAME_SCENARIO_QUERYLIST_ID);
                map.put(PROPERTY_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), DB_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(PROPERTY_NAME_SEQ_NO.ToLower(), DB_NAME_SEQ_NO);
                map.put(PROPERTY_NAME_ITEM_INFO_ID.ToLower(), DB_NAME_ITEM_INFO_ID);
                map.put(PROPERTY_NAME_OPERATION_CODE.ToLower(), DB_NAME_OPERATION_CODE);
                map.put(PROPERTY_NAME_CONDITION_STRING.ToLower(), DB_NAME_CONDITION_STRING);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TScenarioQuerylist"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TScenarioQuerylistDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TScenarioQuerylistCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TScenarioQuerylistBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TScenarioQuerylist NewMyEntity() { return new TScenarioQuerylist(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TScenarioQuerylistCB NewMyConditionBean() { return new TScenarioQuerylistCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TScenarioQuerylist>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TScenarioQuerylist>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("SCENARIO_QUERYLIST_ID", "ScenarioQuerylistId", new EntityPropertyScenarioQuerylistIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_TOTALIZATION_ID", "ScenarioTotalizationId", new EntityPropertyScenarioTotalizationIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SEQ_NO", "SeqNo", new EntityPropertySeqNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_INFO_ID", "ItemInfoId", new EntityPropertyItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OPERATION_CODE", "OperationCode", new EntityPropertyOperationCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_STRING", "ConditionString", new EntityPropertyConditionStringSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TScenarioQuerylist> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TScenarioQuerylist)entity, value);
        }

        public class EntityPropertyScenarioQuerylistIdSetupper : EntityPropertySetupper<TScenarioQuerylist> {
            public void Setup(TScenarioQuerylist entity, Object value) { entity.ScenarioQuerylistId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenarioTotalizationIdSetupper : EntityPropertySetupper<TScenarioQuerylist> {
            public void Setup(TScenarioQuerylist entity, Object value) { entity.ScenarioTotalizationId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySeqNoSetupper : EntityPropertySetupper<TScenarioQuerylist> {
            public void Setup(TScenarioQuerylist entity, Object value) { entity.SeqNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyItemInfoIdSetupper : EntityPropertySetupper<TScenarioQuerylist> {
            public void Setup(TScenarioQuerylist entity, Object value) { entity.ItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOperationCodeSetupper : EntityPropertySetupper<TScenarioQuerylist> {
            public void Setup(TScenarioQuerylist entity, Object value) { entity.OperationCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyConditionStringSetupper : EntityPropertySetupper<TScenarioQuerylist> {
            public void Setup(TScenarioQuerylist entity, Object value) { entity.ConditionString = (value != null) ? (String)value : null; }
        }
    }
}
