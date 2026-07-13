
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

    public class TCategoryInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TCategoryInfo);

        private static readonly TCategoryInfoDbm _instance = new TCategoryInfoDbm();
        private TCategoryInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TCategoryInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_CATEGORY_INFO"; } }
        public override String TablePropertyName { get { return "TCategoryInfo"; } }
        public override String TableSqlName { get { return "T_CATEGORY_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnCategoryInfoId;
        protected ColumnInfo _columnItemInfoId;
        protected ColumnInfo _columnCategoryNo;
        protected ColumnInfo _columnCategoryName;
        protected ColumnInfo _columnWeightValue;
        protected ColumnInfo _columnOriginalCategoryName;
        protected ColumnInfo _columnOriginalWeightValue;

        public ColumnInfo ColumnCategoryInfoId { get { return _columnCategoryInfoId; } }
        public ColumnInfo ColumnItemInfoId { get { return _columnItemInfoId; } }
        public ColumnInfo ColumnCategoryNo { get { return _columnCategoryNo; } }
        public ColumnInfo ColumnCategoryName { get { return _columnCategoryName; } }
        public ColumnInfo ColumnWeightValue { get { return _columnWeightValue; } }
        public ColumnInfo ColumnOriginalCategoryName { get { return _columnOriginalCategoryName; } }
        public ColumnInfo ColumnOriginalWeightValue { get { return _columnOriginalWeightValue; } }

        protected void InitializeColumnInfo() {
            _columnCategoryInfoId = cci("CATEGORY_INFO_ID", "CATEGORY_INFO_ID", null, null, true, "CategoryInfoId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TMatrixInfoList");
            _columnItemInfoId = cci("ITEM_INFO_ID", "ITEM_INFO_ID", null, null, true, "ItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TItemInfo", null);
            _columnCategoryNo = cci("CATEGORY_NO", "CATEGORY_NO", null, null, true, "CategoryNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCategoryName = cci("CATEGORY_NAME", "CATEGORY_NAME", null, null, false, "CategoryName", typeof(String), false, "NVARCHAR2", 200, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWeightValue = cci("WEIGHT_VALUE", "WEIGHT_VALUE", null, null, false, "WeightValue", typeof(String), false, "VARCHAR2", 17, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOriginalCategoryName = cci("ORIGINAL_CATEGORY_NAME", "ORIGINAL_CATEGORY_NAME", null, null, false, "OriginalCategoryName", typeof(String), false, "NVARCHAR2", 200, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOriginalWeightValue = cci("ORIGINAL_WEIGHT_VALUE", "ORIGINAL_WEIGHT_VALUE", null, null, false, "OriginalWeightValue", typeof(String), false, "VARCHAR2", 17, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnCategoryInfoId);
            _columnInfoList.add(ColumnItemInfoId);
            _columnInfoList.add(ColumnCategoryNo);
            _columnInfoList.add(ColumnCategoryName);
            _columnInfoList.add(ColumnWeightValue);
            _columnInfoList.add(ColumnOriginalCategoryName);
            _columnInfoList.add(ColumnOriginalWeightValue);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnCategoryInfoId);
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
        public ForeignInfo ForeignTItemInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TItemInfoDbm.GetInstance().ColumnItemInfoId);
            return cfi("TItemInfo", this, TItemInfoDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTMatrixInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCategoryInfoId, TMatrixInfoDbm.GetInstance().ColumnAddFaCategoryInfoId);
            return cri("TMatrixInfoList", this, TMatrixInfoDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Category_Info_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Category_Info_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_CATEGORY_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TCategoryInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_CATEGORY_INFO_ID = "CATEGORY_INFO_ID";
        public static readonly String DB_NAME_ITEM_INFO_ID = "ITEM_INFO_ID";
        public static readonly String DB_NAME_CATEGORY_NO = "CATEGORY_NO";
        public static readonly String DB_NAME_CATEGORY_NAME = "CATEGORY_NAME";
        public static readonly String DB_NAME_WEIGHT_VALUE = "WEIGHT_VALUE";
        public static readonly String DB_NAME_ORIGINAL_CATEGORY_NAME = "ORIGINAL_CATEGORY_NAME";
        public static readonly String DB_NAME_ORIGINAL_WEIGHT_VALUE = "ORIGINAL_WEIGHT_VALUE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_CATEGORY_INFO_ID = "CategoryInfoId";
        public static readonly String PROPERTY_NAME_ITEM_INFO_ID = "ItemInfoId";
        public static readonly String PROPERTY_NAME_CATEGORY_NO = "CategoryNo";
        public static readonly String PROPERTY_NAME_CATEGORY_NAME = "CategoryName";
        public static readonly String PROPERTY_NAME_WEIGHT_VALUE = "WeightValue";
        public static readonly String PROPERTY_NAME_ORIGINAL_CATEGORY_NAME = "OriginalCategoryName";
        public static readonly String PROPERTY_NAME_ORIGINAL_WEIGHT_VALUE = "OriginalWeightValue";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TItemInfo = "TItemInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TMatrixInfoList = "TMatrixInfoList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TCategoryInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_CATEGORY_INFO_ID.ToLower(), PROPERTY_NAME_CATEGORY_INFO_ID);
                map.put(DB_NAME_ITEM_INFO_ID.ToLower(), PROPERTY_NAME_ITEM_INFO_ID);
                map.put(DB_NAME_CATEGORY_NO.ToLower(), PROPERTY_NAME_CATEGORY_NO);
                map.put(DB_NAME_CATEGORY_NAME.ToLower(), PROPERTY_NAME_CATEGORY_NAME);
                map.put(DB_NAME_WEIGHT_VALUE.ToLower(), PROPERTY_NAME_WEIGHT_VALUE);
                map.put(DB_NAME_ORIGINAL_CATEGORY_NAME.ToLower(), PROPERTY_NAME_ORIGINAL_CATEGORY_NAME);
                map.put(DB_NAME_ORIGINAL_WEIGHT_VALUE.ToLower(), PROPERTY_NAME_ORIGINAL_WEIGHT_VALUE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_CATEGORY_INFO_ID.ToLower(), DB_NAME_CATEGORY_INFO_ID);
                map.put(PROPERTY_NAME_ITEM_INFO_ID.ToLower(), DB_NAME_ITEM_INFO_ID);
                map.put(PROPERTY_NAME_CATEGORY_NO.ToLower(), DB_NAME_CATEGORY_NO);
                map.put(PROPERTY_NAME_CATEGORY_NAME.ToLower(), DB_NAME_CATEGORY_NAME);
                map.put(PROPERTY_NAME_WEIGHT_VALUE.ToLower(), DB_NAME_WEIGHT_VALUE);
                map.put(PROPERTY_NAME_ORIGINAL_CATEGORY_NAME.ToLower(), DB_NAME_ORIGINAL_CATEGORY_NAME);
                map.put(PROPERTY_NAME_ORIGINAL_WEIGHT_VALUE.ToLower(), DB_NAME_ORIGINAL_WEIGHT_VALUE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TCategoryInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TCategoryInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TCategoryInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TCategoryInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TCategoryInfo NewMyEntity() { return new TCategoryInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TCategoryInfoCB NewMyConditionBean() { return new TCategoryInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TCategoryInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TCategoryInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("CATEGORY_INFO_ID", "CategoryInfoId", new EntityPropertyCategoryInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_INFO_ID", "ItemInfoId", new EntityPropertyItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CATEGORY_NO", "CategoryNo", new EntityPropertyCategoryNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CATEGORY_NAME", "CategoryName", new EntityPropertyCategoryNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WEIGHT_VALUE", "WeightValue", new EntityPropertyWeightValueSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ORIGINAL_CATEGORY_NAME", "OriginalCategoryName", new EntityPropertyOriginalCategoryNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ORIGINAL_WEIGHT_VALUE", "OriginalWeightValue", new EntityPropertyOriginalWeightValueSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TCategoryInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TCategoryInfo)entity, value);
        }

        public class EntityPropertyCategoryInfoIdSetupper : EntityPropertySetupper<TCategoryInfo> {
            public void Setup(TCategoryInfo entity, Object value) { entity.CategoryInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyItemInfoIdSetupper : EntityPropertySetupper<TCategoryInfo> {
            public void Setup(TCategoryInfo entity, Object value) { entity.ItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyCategoryNoSetupper : EntityPropertySetupper<TCategoryInfo> {
            public void Setup(TCategoryInfo entity, Object value) { entity.CategoryNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyCategoryNameSetupper : EntityPropertySetupper<TCategoryInfo> {
            public void Setup(TCategoryInfo entity, Object value) { entity.CategoryName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyWeightValueSetupper : EntityPropertySetupper<TCategoryInfo> {
            public void Setup(TCategoryInfo entity, Object value) { entity.WeightValue = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOriginalCategoryNameSetupper : EntityPropertySetupper<TCategoryInfo> {
            public void Setup(TCategoryInfo entity, Object value) { entity.OriginalCategoryName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOriginalWeightValueSetupper : EntityPropertySetupper<TCategoryInfo> {
            public void Setup(TCategoryInfo entity, Object value) { entity.OriginalWeightValue = (value != null) ? (String)value : null; }
        }
    }
}
