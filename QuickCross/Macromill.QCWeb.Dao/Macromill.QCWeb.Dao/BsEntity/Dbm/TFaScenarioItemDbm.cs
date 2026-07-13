
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

    public class TFaScenarioItemDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TFaScenarioItem);

        private static readonly TFaScenarioItemDbm _instance = new TFaScenarioItemDbm();
        private TFaScenarioItemDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TFaScenarioItemDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_FA_SCENARIO_ITEM"; } }
        public override String TablePropertyName { get { return "TFaScenarioItem"; } }
        public override String TableSqlName { get { return "T_FA_SCENARIO_ITEM"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnFaScenarioItemId;
        protected ColumnInfo _columnFaScenarioHeaderId;
        protected ColumnInfo _columnFaTargetItemId;
        protected ColumnInfo _columnTitleString;
        protected ColumnInfo _columnSortNo;

        public ColumnInfo ColumnFaScenarioItemId { get { return _columnFaScenarioItemId; } }
        public ColumnInfo ColumnFaScenarioHeaderId { get { return _columnFaScenarioHeaderId; } }
        public ColumnInfo ColumnFaTargetItemId { get { return _columnFaTargetItemId; } }
        public ColumnInfo ColumnTitleString { get { return _columnTitleString; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }

        protected void InitializeColumnInfo() {
            _columnFaScenarioItemId = cci("FA_SCENARIO_ITEM_ID", "FA_SCENARIO_ITEM_ID", null, null, true, "FaScenarioItemId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFaScenarioHeaderId = cci("FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", null, null, true, "FaScenarioHeaderId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TFaScenarioHeader", null);
            _columnFaTargetItemId = cci("FA_TARGET_ITEM_ID", "FA_TARGET_ITEM_ID", null, null, true, "FaTargetItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TItemInfo", null);
            _columnTitleString = cci("TITLE_STRING", "TITLE_STRING", null, null, false, "TitleString", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnFaScenarioItemId);
            _columnInfoList.add(ColumnFaScenarioHeaderId);
            _columnInfoList.add(ColumnFaTargetItemId);
            _columnInfoList.add(ColumnTitleString);
            _columnInfoList.add(ColumnSortNo);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnFaScenarioItemId);
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
        public ForeignInfo ForeignTFaScenarioHeader { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnFaScenarioHeaderId, TFaScenarioHeaderDbm.GetInstance().ColumnFaScenarioHeaderId);
            return cfi("TFaScenarioHeader", this, TFaScenarioHeaderDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTItemInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnFaTargetItemId, TItemInfoDbm.GetInstance().ColumnItemInfoId);
            return cfi("TItemInfo", this, TItemInfoDbm.GetInstance(), map, 1, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_FA_Scenario_Item_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_FA_Scenario_Item_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_FA_SCENARIO_ITEM";
        public static readonly String TABLE_PROPERTY_NAME = "TFaScenarioItem";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_FA_SCENARIO_ITEM_ID = "FA_SCENARIO_ITEM_ID";
        public static readonly String DB_NAME_FA_SCENARIO_HEADER_ID = "FA_SCENARIO_HEADER_ID";
        public static readonly String DB_NAME_FA_TARGET_ITEM_ID = "FA_TARGET_ITEM_ID";
        public static readonly String DB_NAME_TITLE_STRING = "TITLE_STRING";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_FA_SCENARIO_ITEM_ID = "FaScenarioItemId";
        public static readonly String PROPERTY_NAME_FA_SCENARIO_HEADER_ID = "FaScenarioHeaderId";
        public static readonly String PROPERTY_NAME_FA_TARGET_ITEM_ID = "FaTargetItemId";
        public static readonly String PROPERTY_NAME_TITLE_STRING = "TitleString";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TFaScenarioHeader = "TFaScenarioHeader";
        public static readonly String FOREIGN_PROPERTY_NAME_TItemInfo = "TItemInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TFaScenarioItemDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_FA_SCENARIO_ITEM_ID.ToLower(), PROPERTY_NAME_FA_SCENARIO_ITEM_ID);
                map.put(DB_NAME_FA_SCENARIO_HEADER_ID.ToLower(), PROPERTY_NAME_FA_SCENARIO_HEADER_ID);
                map.put(DB_NAME_FA_TARGET_ITEM_ID.ToLower(), PROPERTY_NAME_FA_TARGET_ITEM_ID);
                map.put(DB_NAME_TITLE_STRING.ToLower(), PROPERTY_NAME_TITLE_STRING);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_FA_SCENARIO_ITEM_ID.ToLower(), DB_NAME_FA_SCENARIO_ITEM_ID);
                map.put(PROPERTY_NAME_FA_SCENARIO_HEADER_ID.ToLower(), DB_NAME_FA_SCENARIO_HEADER_ID);
                map.put(PROPERTY_NAME_FA_TARGET_ITEM_ID.ToLower(), DB_NAME_FA_TARGET_ITEM_ID);
                map.put(PROPERTY_NAME_TITLE_STRING.ToLower(), DB_NAME_TITLE_STRING);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TFaScenarioItem"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TFaScenarioItemDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TFaScenarioItemCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TFaScenarioItemBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TFaScenarioItem NewMyEntity() { return new TFaScenarioItem(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TFaScenarioItemCB NewMyConditionBean() { return new TFaScenarioItemCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TFaScenarioItem>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TFaScenarioItem>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("FA_SCENARIO_ITEM_ID", "FaScenarioItemId", new EntityPropertyFaScenarioItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA_SCENARIO_HEADER_ID", "FaScenarioHeaderId", new EntityPropertyFaScenarioHeaderIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA_TARGET_ITEM_ID", "FaTargetItemId", new EntityPropertyFaTargetItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_STRING", "TitleString", new EntityPropertyTitleStringSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TFaScenarioItem> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TFaScenarioItem)entity, value);
        }

        public class EntityPropertyFaScenarioItemIdSetupper : EntityPropertySetupper<TFaScenarioItem> {
            public void Setup(TFaScenarioItem entity, Object value) { entity.FaScenarioItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyFaScenarioHeaderIdSetupper : EntityPropertySetupper<TFaScenarioItem> {
            public void Setup(TFaScenarioItem entity, Object value) { entity.FaScenarioHeaderId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyFaTargetItemIdSetupper : EntityPropertySetupper<TFaScenarioItem> {
            public void Setup(TFaScenarioItem entity, Object value) { entity.FaTargetItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTitleStringSetupper : EntityPropertySetupper<TFaScenarioItem> {
            public void Setup(TFaScenarioItem entity, Object value) { entity.TitleString = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TFaScenarioItem> {
            public void Setup(TFaScenarioItem entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
    }
}
