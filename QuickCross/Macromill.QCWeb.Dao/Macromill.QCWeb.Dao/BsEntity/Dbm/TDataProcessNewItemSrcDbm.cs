
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

    public class TDataProcessNewItemSrcDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDataProcessNewItemSrc);

        private static readonly TDataProcessNewItemSrcDbm _instance = new TDataProcessNewItemSrcDbm();
        private TDataProcessNewItemSrcDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDataProcessNewItemSrcDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_PROCESS_NEW_ITEM_SRC"; } }
        public override String TablePropertyName { get { return "TDataProcessNewItemSrc"; } }
        public override String TableSqlName { get { return "T_DATA_PROCESS_NEW_ITEM_SRC"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDataProcessNewItemSrcId;
        protected ColumnInfo _columnSrcItemId;
        protected ColumnInfo _columnNewItemId;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnTargetFlag;
        protected ColumnInfo _columnDataEditId;

        public ColumnInfo ColumnDataProcessNewItemSrcId { get { return _columnDataProcessNewItemSrcId; } }
        public ColumnInfo ColumnSrcItemId { get { return _columnSrcItemId; } }
        public ColumnInfo ColumnNewItemId { get { return _columnNewItemId; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnTargetFlag { get { return _columnTargetFlag; } }
        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }

        protected void InitializeColumnInfo() {
            _columnDataProcessNewItemSrcId = cci("DATA_PROCESS_NEW_ITEM_SRC_ID", "DATA_PROCESS_NEW_ITEM_SRC_ID", null, null, true, "DataProcessNewItemSrcId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSrcItemId = cci("SRC_ITEM_ID", "SRC_ITEM_ID", null, null, false, "SrcItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewItemId = cci("NEW_ITEM_ID", "NEW_ITEM_ID", null, null, false, "NewItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTargetFlag = cci("TARGET_FLAG", "TARGET_FLAG", null, null, true, "TargetFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, true, "DataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDataProcessNewItem", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDataProcessNewItemSrcId);
            _columnInfoList.add(ColumnSrcItemId);
            _columnInfoList.add(ColumnNewItemId);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnTargetFlag);
            _columnInfoList.add(ColumnDataEditId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnDataProcessNewItemSrcId);
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
        public ForeignInfo ForeignTDataProcessNewItem { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDataProcessNewItemDbm.GetInstance().ColumnDataEditId);
            return cfi("TDataProcessNewItem", this, TDataProcessNewItemDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Data_Process_New_Item_SrcSEQ"; } }
        public override String SequenceNextValSql { get { return "select T_Data_Process_New_Item_SrcSEQ.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_DATA_PROCESS_NEW_ITEM_SRC";
        public static readonly String TABLE_PROPERTY_NAME = "TDataProcessNewItemSrc";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DATA_PROCESS_NEW_ITEM_SRC_ID = "DATA_PROCESS_NEW_ITEM_SRC_ID";
        public static readonly String DB_NAME_SRC_ITEM_ID = "SRC_ITEM_ID";
        public static readonly String DB_NAME_NEW_ITEM_ID = "NEW_ITEM_ID";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_TARGET_FLAG = "TARGET_FLAG";
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DATA_PROCESS_NEW_ITEM_SRC_ID = "DataProcessNewItemSrcId";
        public static readonly String PROPERTY_NAME_SRC_ITEM_ID = "SrcItemId";
        public static readonly String PROPERTY_NAME_NEW_ITEM_ID = "NewItemId";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_TARGET_FLAG = "TargetFlag";
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TDataProcessNewItem = "TDataProcessNewItem";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TDataProcessNewItemSrcDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DATA_PROCESS_NEW_ITEM_SRC_ID.ToLower(), PROPERTY_NAME_DATA_PROCESS_NEW_ITEM_SRC_ID);
                map.put(DB_NAME_SRC_ITEM_ID.ToLower(), PROPERTY_NAME_SRC_ITEM_ID);
                map.put(DB_NAME_NEW_ITEM_ID.ToLower(), PROPERTY_NAME_NEW_ITEM_ID);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_TARGET_FLAG.ToLower(), PROPERTY_NAME_TARGET_FLAG);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DATA_PROCESS_NEW_ITEM_SRC_ID.ToLower(), DB_NAME_DATA_PROCESS_NEW_ITEM_SRC_ID);
                map.put(PROPERTY_NAME_SRC_ITEM_ID.ToLower(), DB_NAME_SRC_ITEM_ID);
                map.put(PROPERTY_NAME_NEW_ITEM_ID.ToLower(), DB_NAME_NEW_ITEM_ID);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_TARGET_FLAG.ToLower(), DB_NAME_TARGET_FLAG);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDataProcessNewItemSrc"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDataProcessNewItemSrcDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDataProcessNewItemSrcCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDataProcessNewItemSrcBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDataProcessNewItemSrc NewMyEntity() { return new TDataProcessNewItemSrc(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDataProcessNewItemSrcCB NewMyConditionBean() { return new TDataProcessNewItemSrcCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDataProcessNewItemSrc>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDataProcessNewItemSrc>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DATA_PROCESS_NEW_ITEM_SRC_ID", "DataProcessNewItemSrcId", new EntityPropertyDataProcessNewItemSrcIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SRC_ITEM_ID", "SrcItemId", new EntityPropertySrcItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_ITEM_ID", "NewItemId", new EntityPropertyNewItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TARGET_FLAG", "TargetFlag", new EntityPropertyTargetFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDataProcessNewItemSrc> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDataProcessNewItemSrc)entity, value);
        }

        public class EntityPropertyDataProcessNewItemSrcIdSetupper : EntityPropertySetupper<TDataProcessNewItemSrc> {
            public void Setup(TDataProcessNewItemSrc entity, Object value) { entity.DataProcessNewItemSrcId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySrcItemIdSetupper : EntityPropertySetupper<TDataProcessNewItemSrc> {
            public void Setup(TDataProcessNewItemSrc entity, Object value) { entity.SrcItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyNewItemIdSetupper : EntityPropertySetupper<TDataProcessNewItemSrc> {
            public void Setup(TDataProcessNewItemSrc entity, Object value) { entity.NewItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TDataProcessNewItemSrc> {
            public void Setup(TDataProcessNewItemSrc entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTargetFlagSetupper : EntityPropertySetupper<TDataProcessNewItemSrc> {
            public void Setup(TDataProcessNewItemSrc entity, Object value) { entity.TargetFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TDataProcessNewItemSrc> {
            public void Setup(TDataProcessNewItemSrc entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
    }
}
