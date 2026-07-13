
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

    public class TEditTargetItemDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TEditTargetItem);

        private static readonly TEditTargetItemDbm _instance = new TEditTargetItemDbm();
        private TEditTargetItemDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TEditTargetItemDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_EDIT_TARGET_ITEM"; } }
        public override String TablePropertyName { get { return "TEditTargetItem"; } }
        public override String TableSqlName { get { return "T_EDIT_TARGET_ITEM"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnEditTargetItemId;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnTargetItemId;
        protected ColumnInfo _columnDataEditId;

        public ColumnInfo ColumnEditTargetItemId { get { return _columnEditTargetItemId; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnTargetItemId { get { return _columnTargetItemId; } }
        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }

        protected void InitializeColumnInfo() {
            _columnEditTargetItemId = cci("EDIT_TARGET_ITEM_ID", "EDIT_TARGET_ITEM_ID", null, null, true, "EditTargetItemId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTargetItemId = cci("TARGET_ITEM_ID", "TARGET_ITEM_ID", null, null, true, "TargetItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, true, "DataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TEditData", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnEditTargetItemId);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnTargetItemId);
            _columnInfoList.add(ColumnDataEditId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnEditTargetItemId);
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
        public ForeignInfo ForeignTEditData { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TEditDataDbm.GetInstance().ColumnDataEditId);
            return cfi("TEditData", this, TEditDataDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Edit_Target_Item_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Edit_Target_Item_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_EDIT_TARGET_ITEM";
        public static readonly String TABLE_PROPERTY_NAME = "TEditTargetItem";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_EDIT_TARGET_ITEM_ID = "EDIT_TARGET_ITEM_ID";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_TARGET_ITEM_ID = "TARGET_ITEM_ID";
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_EDIT_TARGET_ITEM_ID = "EditTargetItemId";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_TARGET_ITEM_ID = "TargetItemId";
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TEditData = "TEditData";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TEditTargetItemDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_EDIT_TARGET_ITEM_ID.ToLower(), PROPERTY_NAME_EDIT_TARGET_ITEM_ID);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_TARGET_ITEM_ID.ToLower(), PROPERTY_NAME_TARGET_ITEM_ID);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_EDIT_TARGET_ITEM_ID.ToLower(), DB_NAME_EDIT_TARGET_ITEM_ID);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_TARGET_ITEM_ID.ToLower(), DB_NAME_TARGET_ITEM_ID);
                map.put(PROPERTY_NAME_DATA_EDIT_ID.ToLower(), DB_NAME_DATA_EDIT_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TEditTargetItem"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TEditTargetItemDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TEditTargetItemCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TEditTargetItemBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TEditTargetItem NewMyEntity() { return new TEditTargetItem(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TEditTargetItemCB NewMyConditionBean() { return new TEditTargetItemCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TEditTargetItem>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TEditTargetItem>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("EDIT_TARGET_ITEM_ID", "EditTargetItemId", new EntityPropertyEditTargetItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TARGET_ITEM_ID", "TargetItemId", new EntityPropertyTargetItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TEditTargetItem> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TEditTargetItem)entity, value);
        }

        public class EntityPropertyEditTargetItemIdSetupper : EntityPropertySetupper<TEditTargetItem> {
            public void Setup(TEditTargetItem entity, Object value) { entity.EditTargetItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TEditTargetItem> {
            public void Setup(TEditTargetItem entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTargetItemIdSetupper : EntityPropertySetupper<TEditTargetItem> {
            public void Setup(TEditTargetItem entity, Object value) { entity.TargetItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TEditTargetItem> {
            public void Setup(TEditTargetItem entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
    }
}
