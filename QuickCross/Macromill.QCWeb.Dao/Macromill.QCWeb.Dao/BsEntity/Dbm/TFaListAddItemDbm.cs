
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

    public class TFaListAddItemDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TFaListAddItem);

        private static readonly TFaListAddItemDbm _instance = new TFaListAddItemDbm();
        private TFaListAddItemDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TFaListAddItemDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_FA_LIST_ADD_ITEM"; } }
        public override String TablePropertyName { get { return "TFaListAddItem"; } }
        public override String TableSqlName { get { return "T_FA_LIST_ADD_ITEM"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnFaListAddItemId;
        protected ColumnInfo _columnFaScenarioHeaderId;
        protected ColumnInfo _columnItemInfoId;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnLv2title;

        public ColumnInfo ColumnFaListAddItemId { get { return _columnFaListAddItemId; } }
        public ColumnInfo ColumnFaScenarioHeaderId { get { return _columnFaScenarioHeaderId; } }
        public ColumnInfo ColumnItemInfoId { get { return _columnItemInfoId; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnLv2title { get { return _columnLv2title; } }

        protected void InitializeColumnInfo() {
            _columnFaListAddItemId = cci("FA_LIST_ADD_ITEM_ID", "FA_LIST_ADD_ITEM_ID", null, null, true, "FaListAddItemId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFaScenarioHeaderId = cci("FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", null, null, true, "FaScenarioHeaderId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TFaScenarioHeader", null);
            _columnItemInfoId = cci("ITEM_INFO_ID", "ITEM_INFO_ID", null, null, true, "ItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TItemInfo", null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLv2title = cci("LV2TITLE", "LV2TITLE", null, null, false, "Lv2title", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnFaListAddItemId);
            _columnInfoList.add(ColumnFaScenarioHeaderId);
            _columnInfoList.add(ColumnItemInfoId);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnLv2title);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnFaListAddItemId);
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
        public override String SequenceName { get { return "T_FA_List_Add_Item_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_FA_List_Add_Item_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_FA_LIST_ADD_ITEM";
        public static readonly String TABLE_PROPERTY_NAME = "TFaListAddItem";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_FA_LIST_ADD_ITEM_ID = "FA_LIST_ADD_ITEM_ID";
        public static readonly String DB_NAME_FA_SCENARIO_HEADER_ID = "FA_SCENARIO_HEADER_ID";
        public static readonly String DB_NAME_ITEM_INFO_ID = "ITEM_INFO_ID";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_LV2TITLE = "LV2TITLE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_FA_LIST_ADD_ITEM_ID = "FaListAddItemId";
        public static readonly String PROPERTY_NAME_FA_SCENARIO_HEADER_ID = "FaScenarioHeaderId";
        public static readonly String PROPERTY_NAME_ITEM_INFO_ID = "ItemInfoId";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_LV2TITLE = "Lv2title";

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

        static TFaListAddItemDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_FA_LIST_ADD_ITEM_ID.ToLower(), PROPERTY_NAME_FA_LIST_ADD_ITEM_ID);
                map.put(DB_NAME_FA_SCENARIO_HEADER_ID.ToLower(), PROPERTY_NAME_FA_SCENARIO_HEADER_ID);
                map.put(DB_NAME_ITEM_INFO_ID.ToLower(), PROPERTY_NAME_ITEM_INFO_ID);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_LV2TITLE.ToLower(), PROPERTY_NAME_LV2TITLE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_FA_LIST_ADD_ITEM_ID.ToLower(), DB_NAME_FA_LIST_ADD_ITEM_ID);
                map.put(PROPERTY_NAME_FA_SCENARIO_HEADER_ID.ToLower(), DB_NAME_FA_SCENARIO_HEADER_ID);
                map.put(PROPERTY_NAME_ITEM_INFO_ID.ToLower(), DB_NAME_ITEM_INFO_ID);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_LV2TITLE.ToLower(), DB_NAME_LV2TITLE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TFaListAddItem"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TFaListAddItemDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TFaListAddItemCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TFaListAddItemBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TFaListAddItem NewMyEntity() { return new TFaListAddItem(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TFaListAddItemCB NewMyConditionBean() { return new TFaListAddItemCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TFaListAddItem>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TFaListAddItem>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("FA_LIST_ADD_ITEM_ID", "FaListAddItemId", new EntityPropertyFaListAddItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA_SCENARIO_HEADER_ID", "FaScenarioHeaderId", new EntityPropertyFaScenarioHeaderIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_INFO_ID", "ItemInfoId", new EntityPropertyItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LV2TITLE", "Lv2title", new EntityPropertyLv2titleSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TFaListAddItem> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TFaListAddItem)entity, value);
        }

        public class EntityPropertyFaListAddItemIdSetupper : EntityPropertySetupper<TFaListAddItem> {
            public void Setup(TFaListAddItem entity, Object value) { entity.FaListAddItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyFaScenarioHeaderIdSetupper : EntityPropertySetupper<TFaListAddItem> {
            public void Setup(TFaListAddItem entity, Object value) { entity.FaScenarioHeaderId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyItemInfoIdSetupper : EntityPropertySetupper<TFaListAddItem> {
            public void Setup(TFaListAddItem entity, Object value) { entity.ItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TFaListAddItem> {
            public void Setup(TFaListAddItem entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLv2titleSetupper : EntityPropertySetupper<TFaListAddItem> {
            public void Setup(TFaListAddItem entity, Object value) { entity.Lv2title = (value != null) ? (String)value : null; }
        }
    }
}
