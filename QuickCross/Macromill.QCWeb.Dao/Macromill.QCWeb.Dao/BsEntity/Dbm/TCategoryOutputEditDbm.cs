
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

    public class TCategoryOutputEditDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TCategoryOutputEdit);

        private static readonly TCategoryOutputEditDbm _instance = new TCategoryOutputEditDbm();
        private TCategoryOutputEditDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TCategoryOutputEditDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_CATEGORY_OUTPUT_EDIT"; } }
        public override String TablePropertyName { get { return "TCategoryOutputEdit"; } }
        public override String TableSqlName { get { return "T_CATEGORY_OUTPUT_EDIT"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnCategoryOutputEditId;
        protected ColumnInfo _columnScenarioTotalizationId;
        protected ColumnInfo _columnOldItemId;
        protected ColumnInfo _columnNewItemId;
        protected ColumnInfo _columnTopFlag;
        protected ColumnInfo _columnTopCount;
        protected ColumnInfo _columnTopName;
        protected ColumnInfo _columnBottomFlag;
        protected ColumnInfo _columnBottomCount;
        protected ColumnInfo _columnBottomName;

        public ColumnInfo ColumnCategoryOutputEditId { get { return _columnCategoryOutputEditId; } }
        public ColumnInfo ColumnScenarioTotalizationId { get { return _columnScenarioTotalizationId; } }
        public ColumnInfo ColumnOldItemId { get { return _columnOldItemId; } }
        public ColumnInfo ColumnNewItemId { get { return _columnNewItemId; } }
        public ColumnInfo ColumnTopFlag { get { return _columnTopFlag; } }
        public ColumnInfo ColumnTopCount { get { return _columnTopCount; } }
        public ColumnInfo ColumnTopName { get { return _columnTopName; } }
        public ColumnInfo ColumnBottomFlag { get { return _columnBottomFlag; } }
        public ColumnInfo ColumnBottomCount { get { return _columnBottomCount; } }
        public ColumnInfo ColumnBottomName { get { return _columnBottomName; } }

        protected void InitializeColumnInfo() {
            _columnCategoryOutputEditId = cci("CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", null, null, true, "CategoryOutputEditId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TCategoryOutputDetail", "TCategoryOutputDetailList");
            _columnScenarioTotalizationId = cci("SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", null, null, true, "ScenarioTotalizationId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TScenarioTotalization", null);
            _columnOldItemId = cci("OLD_ITEM_ID", "OLD_ITEM_ID", null, null, true, "OldItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewItemId = cci("NEW_ITEM_ID", "NEW_ITEM_ID", null, null, true, "NewItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTopFlag = cci("TOP_FLAG", "TOP_FLAG", null, null, true, "TopFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTopCount = cci("TOP_COUNT", "TOP_COUNT", null, null, false, "TopCount", typeof(int?), false, "NUMBER", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTopName = cci("TOP_NAME", "TOP_NAME", null, null, false, "TopName", typeof(String), false, "NVARCHAR2", 100, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnBottomFlag = cci("BOTTOM_FLAG", "BOTTOM_FLAG", null, null, true, "BottomFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnBottomCount = cci("BOTTOM_COUNT", "BOTTOM_COUNT", null, null, false, "BottomCount", typeof(int?), false, "NUMBER", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnBottomName = cci("BOTTOM_NAME", "BOTTOM_NAME", null, null, false, "BottomName", typeof(String), false, "NVARCHAR2", 100, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnCategoryOutputEditId);
            _columnInfoList.add(ColumnScenarioTotalizationId);
            _columnInfoList.add(ColumnOldItemId);
            _columnInfoList.add(ColumnNewItemId);
            _columnInfoList.add(ColumnTopFlag);
            _columnInfoList.add(ColumnTopCount);
            _columnInfoList.add(ColumnTopName);
            _columnInfoList.add(ColumnBottomFlag);
            _columnInfoList.add(ColumnBottomCount);
            _columnInfoList.add(ColumnBottomName);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnCategoryOutputEditId);
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
        public ForeignInfo ForeignTCategoryOutputDetail { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCategoryOutputEditId, TCategoryOutputDetailDbm.GetInstance().ColumnCategoryOutputEditId);
            return cfi("TCategoryOutputDetail", this, TCategoryOutputDetailDbm.GetInstance(), map, 1, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTCategoryOutputDetailList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCategoryOutputEditId, TCategoryOutputDetailDbm.GetInstance().ColumnCategoryOutputEditId);
            return cri("TCategoryOutputDetailList", this, TCategoryOutputDetailDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Category_Output_Edit_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Category_Output_Edit_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_CATEGORY_OUTPUT_EDIT";
        public static readonly String TABLE_PROPERTY_NAME = "TCategoryOutputEdit";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_CATEGORY_OUTPUT_EDIT_ID = "CATEGORY_OUTPUT_EDIT_ID";
        public static readonly String DB_NAME_SCENARIO_TOTALIZATION_ID = "SCENARIO_TOTALIZATION_ID";
        public static readonly String DB_NAME_OLD_ITEM_ID = "OLD_ITEM_ID";
        public static readonly String DB_NAME_NEW_ITEM_ID = "NEW_ITEM_ID";
        public static readonly String DB_NAME_TOP_FLAG = "TOP_FLAG";
        public static readonly String DB_NAME_TOP_COUNT = "TOP_COUNT";
        public static readonly String DB_NAME_TOP_NAME = "TOP_NAME";
        public static readonly String DB_NAME_BOTTOM_FLAG = "BOTTOM_FLAG";
        public static readonly String DB_NAME_BOTTOM_COUNT = "BOTTOM_COUNT";
        public static readonly String DB_NAME_BOTTOM_NAME = "BOTTOM_NAME";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_CATEGORY_OUTPUT_EDIT_ID = "CategoryOutputEditId";
        public static readonly String PROPERTY_NAME_SCENARIO_TOTALIZATION_ID = "ScenarioTotalizationId";
        public static readonly String PROPERTY_NAME_OLD_ITEM_ID = "OldItemId";
        public static readonly String PROPERTY_NAME_NEW_ITEM_ID = "NewItemId";
        public static readonly String PROPERTY_NAME_TOP_FLAG = "TopFlag";
        public static readonly String PROPERTY_NAME_TOP_COUNT = "TopCount";
        public static readonly String PROPERTY_NAME_TOP_NAME = "TopName";
        public static readonly String PROPERTY_NAME_BOTTOM_FLAG = "BottomFlag";
        public static readonly String PROPERTY_NAME_BOTTOM_COUNT = "BottomCount";
        public static readonly String PROPERTY_NAME_BOTTOM_NAME = "BottomName";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TScenarioTotalization = "TScenarioTotalization";
        public static readonly String FOREIGN_PROPERTY_NAME_TCategoryOutputDetail = "TCategoryOutputDetail";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TCategoryOutputDetailList = "TCategoryOutputDetailList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TCategoryOutputEditDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_CATEGORY_OUTPUT_EDIT_ID.ToLower(), PROPERTY_NAME_CATEGORY_OUTPUT_EDIT_ID);
                map.put(DB_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), PROPERTY_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(DB_NAME_OLD_ITEM_ID.ToLower(), PROPERTY_NAME_OLD_ITEM_ID);
                map.put(DB_NAME_NEW_ITEM_ID.ToLower(), PROPERTY_NAME_NEW_ITEM_ID);
                map.put(DB_NAME_TOP_FLAG.ToLower(), PROPERTY_NAME_TOP_FLAG);
                map.put(DB_NAME_TOP_COUNT.ToLower(), PROPERTY_NAME_TOP_COUNT);
                map.put(DB_NAME_TOP_NAME.ToLower(), PROPERTY_NAME_TOP_NAME);
                map.put(DB_NAME_BOTTOM_FLAG.ToLower(), PROPERTY_NAME_BOTTOM_FLAG);
                map.put(DB_NAME_BOTTOM_COUNT.ToLower(), PROPERTY_NAME_BOTTOM_COUNT);
                map.put(DB_NAME_BOTTOM_NAME.ToLower(), PROPERTY_NAME_BOTTOM_NAME);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_CATEGORY_OUTPUT_EDIT_ID.ToLower(), DB_NAME_CATEGORY_OUTPUT_EDIT_ID);
                map.put(PROPERTY_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), DB_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(PROPERTY_NAME_OLD_ITEM_ID.ToLower(), DB_NAME_OLD_ITEM_ID);
                map.put(PROPERTY_NAME_NEW_ITEM_ID.ToLower(), DB_NAME_NEW_ITEM_ID);
                map.put(PROPERTY_NAME_TOP_FLAG.ToLower(), DB_NAME_TOP_FLAG);
                map.put(PROPERTY_NAME_TOP_COUNT.ToLower(), DB_NAME_TOP_COUNT);
                map.put(PROPERTY_NAME_TOP_NAME.ToLower(), DB_NAME_TOP_NAME);
                map.put(PROPERTY_NAME_BOTTOM_FLAG.ToLower(), DB_NAME_BOTTOM_FLAG);
                map.put(PROPERTY_NAME_BOTTOM_COUNT.ToLower(), DB_NAME_BOTTOM_COUNT);
                map.put(PROPERTY_NAME_BOTTOM_NAME.ToLower(), DB_NAME_BOTTOM_NAME);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TCategoryOutputEdit"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TCategoryOutputEditDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TCategoryOutputEditCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TCategoryOutputEditBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TCategoryOutputEdit NewMyEntity() { return new TCategoryOutputEdit(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TCategoryOutputEditCB NewMyConditionBean() { return new TCategoryOutputEditCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TCategoryOutputEdit>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TCategoryOutputEdit>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("CATEGORY_OUTPUT_EDIT_ID", "CategoryOutputEditId", new EntityPropertyCategoryOutputEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_TOTALIZATION_ID", "ScenarioTotalizationId", new EntityPropertyScenarioTotalizationIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OLD_ITEM_ID", "OldItemId", new EntityPropertyOldItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_ITEM_ID", "NewItemId", new EntityPropertyNewItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TOP_FLAG", "TopFlag", new EntityPropertyTopFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TOP_COUNT", "TopCount", new EntityPropertyTopCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TOP_NAME", "TopName", new EntityPropertyTopNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("BOTTOM_FLAG", "BottomFlag", new EntityPropertyBottomFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("BOTTOM_COUNT", "BottomCount", new EntityPropertyBottomCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("BOTTOM_NAME", "BottomName", new EntityPropertyBottomNameSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TCategoryOutputEdit> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TCategoryOutputEdit)entity, value);
        }

        public class EntityPropertyCategoryOutputEditIdSetupper : EntityPropertySetupper<TCategoryOutputEdit> {
            public void Setup(TCategoryOutputEdit entity, Object value) { entity.CategoryOutputEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenarioTotalizationIdSetupper : EntityPropertySetupper<TCategoryOutputEdit> {
            public void Setup(TCategoryOutputEdit entity, Object value) { entity.ScenarioTotalizationId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOldItemIdSetupper : EntityPropertySetupper<TCategoryOutputEdit> {
            public void Setup(TCategoryOutputEdit entity, Object value) { entity.OldItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyNewItemIdSetupper : EntityPropertySetupper<TCategoryOutputEdit> {
            public void Setup(TCategoryOutputEdit entity, Object value) { entity.NewItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTopFlagSetupper : EntityPropertySetupper<TCategoryOutputEdit> {
            public void Setup(TCategoryOutputEdit entity, Object value) { entity.TopFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTopCountSetupper : EntityPropertySetupper<TCategoryOutputEdit> {
            public void Setup(TCategoryOutputEdit entity, Object value) { entity.TopCount = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTopNameSetupper : EntityPropertySetupper<TCategoryOutputEdit> {
            public void Setup(TCategoryOutputEdit entity, Object value) { entity.TopName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyBottomFlagSetupper : EntityPropertySetupper<TCategoryOutputEdit> {
            public void Setup(TCategoryOutputEdit entity, Object value) { entity.BottomFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyBottomCountSetupper : EntityPropertySetupper<TCategoryOutputEdit> {
            public void Setup(TCategoryOutputEdit entity, Object value) { entity.BottomCount = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyBottomNameSetupper : EntityPropertySetupper<TCategoryOutputEdit> {
            public void Setup(TCategoryOutputEdit entity, Object value) { entity.BottomName = (value != null) ? (String)value : null; }
        }
    }
}
