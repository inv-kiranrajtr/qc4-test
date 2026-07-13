
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

    public class TDataProcessNewCategoryDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDataProcessNewCategory);

        private static readonly TDataProcessNewCategoryDbm _instance = new TDataProcessNewCategoryDbm();
        private TDataProcessNewCategoryDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDataProcessNewCategoryDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_PROCESS_NEW_CATEGORY"; } }
        public override String TablePropertyName { get { return "TDataProcessNewCategory"; } }
        public override String TableSqlName { get { return "T_DATA_PROCESS_NEW_CATEGORY"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDataProcessNewCategoryId;
        protected ColumnInfo _columnNewCategoryNo;
        protected ColumnInfo _columnNewCategoryName;
        protected ColumnInfo _columnSrcItemId;
        protected ColumnInfo _columnOperationCode;
        protected ColumnInfo _columnConditionString;
        protected ColumnInfo _columnBottomValue;
        protected ColumnInfo _columnUpperValue;
        protected ColumnInfo _columnDataEditId;

        public ColumnInfo ColumnDataProcessNewCategoryId { get { return _columnDataProcessNewCategoryId; } }
        public ColumnInfo ColumnNewCategoryNo { get { return _columnNewCategoryNo; } }
        public ColumnInfo ColumnNewCategoryName { get { return _columnNewCategoryName; } }
        public ColumnInfo ColumnSrcItemId { get { return _columnSrcItemId; } }
        public ColumnInfo ColumnOperationCode { get { return _columnOperationCode; } }
        public ColumnInfo ColumnConditionString { get { return _columnConditionString; } }
        public ColumnInfo ColumnBottomValue { get { return _columnBottomValue; } }
        public ColumnInfo ColumnUpperValue { get { return _columnUpperValue; } }
        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }

        protected void InitializeColumnInfo() {
            _columnDataProcessNewCategoryId = cci("DATA_PROCESS_NEW_CATEGORY_ID", "DATA_PROCESS_NEW_CATEGORY_ID", null, null, true, "DataProcessNewCategoryId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewCategoryNo = cci("NEW_CATEGORY_NO", "NEW_CATEGORY_NO", null, null, true, "NewCategoryNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewCategoryName = cci("NEW_CATEGORY_NAME", "NEW_CATEGORY_NAME", null, null, true, "NewCategoryName", typeof(String), false, "NVARCHAR2", 200, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSrcItemId = cci("SRC_ITEM_ID", "SRC_ITEM_ID", null, null, false, "SrcItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOperationCode = cci("OPERATION_CODE", "OPERATION_CODE", null, null, false, "OperationCode", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionString = cci("CONDITION_STRING", "CONDITION_STRING", null, null, false, "ConditionString", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnBottomValue = cci("BOTTOM_VALUE", "BOTTOM_VALUE", null, null, false, "BottomValue", typeof(decimal?), false, "NUMBER", 10, 2, false, OptimisticLockType.NONE, null, null, null);
            _columnUpperValue = cci("UPPER_VALUE", "UPPER_VALUE", null, null, false, "UpperValue", typeof(decimal?), false, "NUMBER", 10, 2, false, OptimisticLockType.NONE, null, null, null);
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, true, "DataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDataProcessNewItem", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDataProcessNewCategoryId);
            _columnInfoList.add(ColumnNewCategoryNo);
            _columnInfoList.add(ColumnNewCategoryName);
            _columnInfoList.add(ColumnSrcItemId);
            _columnInfoList.add(ColumnOperationCode);
            _columnInfoList.add(ColumnConditionString);
            _columnInfoList.add(ColumnBottomValue);
            _columnInfoList.add(ColumnUpperValue);
            _columnInfoList.add(ColumnDataEditId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnDataProcessNewCategoryId);
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
        public override String SequenceName { get { return "T_Data_Process_New_CategorySEQ"; } }
        public override String SequenceNextValSql { get { return "select T_Data_Process_New_CategorySEQ.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_DATA_PROCESS_NEW_CATEGORY";
        public static readonly String TABLE_PROPERTY_NAME = "TDataProcessNewCategory";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DATA_PROCESS_NEW_CATEGORY_ID = "DATA_PROCESS_NEW_CATEGORY_ID";
        public static readonly String DB_NAME_NEW_CATEGORY_NO = "NEW_CATEGORY_NO";
        public static readonly String DB_NAME_NEW_CATEGORY_NAME = "NEW_CATEGORY_NAME";
        public static readonly String DB_NAME_SRC_ITEM_ID = "SRC_ITEM_ID";
        public static readonly String DB_NAME_OPERATION_CODE = "OPERATION_CODE";
        public static readonly String DB_NAME_CONDITION_STRING = "CONDITION_STRING";
        public static readonly String DB_NAME_BOTTOM_VALUE = "BOTTOM_VALUE";
        public static readonly String DB_NAME_UPPER_VALUE = "UPPER_VALUE";
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DATA_PROCESS_NEW_CATEGORY_ID = "DataProcessNewCategoryId";
        public static readonly String PROPERTY_NAME_NEW_CATEGORY_NO = "NewCategoryNo";
        public static readonly String PROPERTY_NAME_NEW_CATEGORY_NAME = "NewCategoryName";
        public static readonly String PROPERTY_NAME_SRC_ITEM_ID = "SrcItemId";
        public static readonly String PROPERTY_NAME_OPERATION_CODE = "OperationCode";
        public static readonly String PROPERTY_NAME_CONDITION_STRING = "ConditionString";
        public static readonly String PROPERTY_NAME_BOTTOM_VALUE = "BottomValue";
        public static readonly String PROPERTY_NAME_UPPER_VALUE = "UpperValue";
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

        static TDataProcessNewCategoryDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DATA_PROCESS_NEW_CATEGORY_ID.ToLower(), PROPERTY_NAME_DATA_PROCESS_NEW_CATEGORY_ID);
                map.put(DB_NAME_NEW_CATEGORY_NO.ToLower(), PROPERTY_NAME_NEW_CATEGORY_NO);
                map.put(DB_NAME_NEW_CATEGORY_NAME.ToLower(), PROPERTY_NAME_NEW_CATEGORY_NAME);
                map.put(DB_NAME_SRC_ITEM_ID.ToLower(), PROPERTY_NAME_SRC_ITEM_ID);
                map.put(DB_NAME_OPERATION_CODE.ToLower(), PROPERTY_NAME_OPERATION_CODE);
                map.put(DB_NAME_CONDITION_STRING.ToLower(), PROPERTY_NAME_CONDITION_STRING);
                map.put(DB_NAME_BOTTOM_VALUE.ToLower(), PROPERTY_NAME_BOTTOM_VALUE);
                map.put(DB_NAME_UPPER_VALUE.ToLower(), PROPERTY_NAME_UPPER_VALUE);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DATA_PROCESS_NEW_CATEGORY_ID.ToLower(), DB_NAME_DATA_PROCESS_NEW_CATEGORY_ID);
                map.put(PROPERTY_NAME_NEW_CATEGORY_NO.ToLower(), DB_NAME_NEW_CATEGORY_NO);
                map.put(PROPERTY_NAME_NEW_CATEGORY_NAME.ToLower(), DB_NAME_NEW_CATEGORY_NAME);
                map.put(PROPERTY_NAME_SRC_ITEM_ID.ToLower(), DB_NAME_SRC_ITEM_ID);
                map.put(PROPERTY_NAME_OPERATION_CODE.ToLower(), DB_NAME_OPERATION_CODE);
                map.put(PROPERTY_NAME_CONDITION_STRING.ToLower(), DB_NAME_CONDITION_STRING);
                map.put(PROPERTY_NAME_BOTTOM_VALUE.ToLower(), DB_NAME_BOTTOM_VALUE);
                map.put(PROPERTY_NAME_UPPER_VALUE.ToLower(), DB_NAME_UPPER_VALUE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDataProcessNewCategory"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDataProcessNewCategoryDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDataProcessNewCategoryCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDataProcessNewCategoryBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDataProcessNewCategory NewMyEntity() { return new TDataProcessNewCategory(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDataProcessNewCategoryCB NewMyConditionBean() { return new TDataProcessNewCategoryCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDataProcessNewCategory>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDataProcessNewCategory>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DATA_PROCESS_NEW_CATEGORY_ID", "DataProcessNewCategoryId", new EntityPropertyDataProcessNewCategoryIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_CATEGORY_NO", "NewCategoryNo", new EntityPropertyNewCategoryNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_CATEGORY_NAME", "NewCategoryName", new EntityPropertyNewCategoryNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SRC_ITEM_ID", "SrcItemId", new EntityPropertySrcItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OPERATION_CODE", "OperationCode", new EntityPropertyOperationCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_STRING", "ConditionString", new EntityPropertyConditionStringSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("BOTTOM_VALUE", "BottomValue", new EntityPropertyBottomValueSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UPPER_VALUE", "UpperValue", new EntityPropertyUpperValueSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDataProcessNewCategory> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDataProcessNewCategory)entity, value);
        }

        public class EntityPropertyDataProcessNewCategoryIdSetupper : EntityPropertySetupper<TDataProcessNewCategory> {
            public void Setup(TDataProcessNewCategory entity, Object value) { entity.DataProcessNewCategoryId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyNewCategoryNoSetupper : EntityPropertySetupper<TDataProcessNewCategory> {
            public void Setup(TDataProcessNewCategory entity, Object value) { entity.NewCategoryNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNewCategoryNameSetupper : EntityPropertySetupper<TDataProcessNewCategory> {
            public void Setup(TDataProcessNewCategory entity, Object value) { entity.NewCategoryName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySrcItemIdSetupper : EntityPropertySetupper<TDataProcessNewCategory> {
            public void Setup(TDataProcessNewCategory entity, Object value) { entity.SrcItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOperationCodeSetupper : EntityPropertySetupper<TDataProcessNewCategory> {
            public void Setup(TDataProcessNewCategory entity, Object value) { entity.OperationCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyConditionStringSetupper : EntityPropertySetupper<TDataProcessNewCategory> {
            public void Setup(TDataProcessNewCategory entity, Object value) { entity.ConditionString = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyBottomValueSetupper : EntityPropertySetupper<TDataProcessNewCategory> {
            public void Setup(TDataProcessNewCategory entity, Object value) { entity.BottomValue = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyUpperValueSetupper : EntityPropertySetupper<TDataProcessNewCategory> {
            public void Setup(TDataProcessNewCategory entity, Object value) { entity.UpperValue = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TDataProcessNewCategory> {
            public void Setup(TDataProcessNewCategory entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
    }
}
