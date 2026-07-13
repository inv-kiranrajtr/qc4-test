
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

    public class TGtMatrixInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TGtMatrixInfo);

        private static readonly TGtMatrixInfoDbm _instance = new TGtMatrixInfoDbm();
        private TGtMatrixInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TGtMatrixInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_GT_MATRIX_INFO"; } }
        public override String TablePropertyName { get { return "TGtMatrixInfo"; } }
        public override String TableSqlName { get { return "T_GT_MATRIX_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnGtMatrixInfoId;
        protected ColumnInfo _columnScenarioTotalizationId;
        protected ColumnInfo _columnBaseItemId;
        protected ColumnInfo _columnNewItemId;
        protected ColumnInfo _columnTotalizationType;
        protected ColumnInfo _columnLv1title;
        protected ColumnInfo _columnItemName;

        public ColumnInfo ColumnGtMatrixInfoId { get { return _columnGtMatrixInfoId; } }
        public ColumnInfo ColumnScenarioTotalizationId { get { return _columnScenarioTotalizationId; } }
        public ColumnInfo ColumnBaseItemId { get { return _columnBaseItemId; } }
        public ColumnInfo ColumnNewItemId { get { return _columnNewItemId; } }
        public ColumnInfo ColumnTotalizationType { get { return _columnTotalizationType; } }
        public ColumnInfo ColumnLv1title { get { return _columnLv1title; } }
        public ColumnInfo ColumnItemName { get { return _columnItemName; } }

        protected void InitializeColumnInfo() {
            _columnGtMatrixInfoId = cci("GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", null, null, true, "GtMatrixInfoId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TGtMatrixChild", "TGtMatrixChildList");
            _columnScenarioTotalizationId = cci("SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", null, null, true, "ScenarioTotalizationId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TScenarioTotalization", null);
            _columnBaseItemId = cci("BASE_ITEM_ID", "BASE_ITEM_ID", null, null, true, "BaseItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewItemId = cci("NEW_ITEM_ID", "NEW_ITEM_ID", null, null, true, "NewItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTotalizationType = cci("TOTALIZATION_TYPE", "TOTALIZATION_TYPE", null, null, true, "TotalizationType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLv1title = cci("LV1TITLE", "LV1TITLE", null, null, false, "Lv1title", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemName = cci("ITEM_NAME", "ITEM_NAME", null, null, false, "ItemName", typeof(String), false, "NVARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnGtMatrixInfoId);
            _columnInfoList.add(ColumnScenarioTotalizationId);
            _columnInfoList.add(ColumnBaseItemId);
            _columnInfoList.add(ColumnNewItemId);
            _columnInfoList.add(ColumnTotalizationType);
            _columnInfoList.add(ColumnLv1title);
            _columnInfoList.add(ColumnItemName);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnGtMatrixInfoId);
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
        public ForeignInfo ForeignTGtMatrixChild { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnGtMatrixInfoId, TGtMatrixChildDbm.GetInstance().ColumnGtMatrixInfoId);
            return cfi("TGtMatrixChild", this, TGtMatrixChildDbm.GetInstance(), map, 1, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTGtMatrixChildList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnGtMatrixInfoId, TGtMatrixChildDbm.GetInstance().ColumnGtMatrixInfoId);
            return cri("TGtMatrixChildList", this, TGtMatrixChildDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_GT_Matrix_Info_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_GT_Matrix_Info_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_GT_MATRIX_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TGtMatrixInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_GT_MATRIX_INFO_ID = "GT_MATRIX_INFO_ID";
        public static readonly String DB_NAME_SCENARIO_TOTALIZATION_ID = "SCENARIO_TOTALIZATION_ID";
        public static readonly String DB_NAME_BASE_ITEM_ID = "BASE_ITEM_ID";
        public static readonly String DB_NAME_NEW_ITEM_ID = "NEW_ITEM_ID";
        public static readonly String DB_NAME_TOTALIZATION_TYPE = "TOTALIZATION_TYPE";
        public static readonly String DB_NAME_LV1TITLE = "LV1TITLE";
        public static readonly String DB_NAME_ITEM_NAME = "ITEM_NAME";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_GT_MATRIX_INFO_ID = "GtMatrixInfoId";
        public static readonly String PROPERTY_NAME_SCENARIO_TOTALIZATION_ID = "ScenarioTotalizationId";
        public static readonly String PROPERTY_NAME_BASE_ITEM_ID = "BaseItemId";
        public static readonly String PROPERTY_NAME_NEW_ITEM_ID = "NewItemId";
        public static readonly String PROPERTY_NAME_TOTALIZATION_TYPE = "TotalizationType";
        public static readonly String PROPERTY_NAME_LV1TITLE = "Lv1title";
        public static readonly String PROPERTY_NAME_ITEM_NAME = "ItemName";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TScenarioTotalization = "TScenarioTotalization";
        public static readonly String FOREIGN_PROPERTY_NAME_TGtMatrixChild = "TGtMatrixChild";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TGtMatrixChildList = "TGtMatrixChildList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TGtMatrixInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_GT_MATRIX_INFO_ID.ToLower(), PROPERTY_NAME_GT_MATRIX_INFO_ID);
                map.put(DB_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), PROPERTY_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(DB_NAME_BASE_ITEM_ID.ToLower(), PROPERTY_NAME_BASE_ITEM_ID);
                map.put(DB_NAME_NEW_ITEM_ID.ToLower(), PROPERTY_NAME_NEW_ITEM_ID);
                map.put(DB_NAME_TOTALIZATION_TYPE.ToLower(), PROPERTY_NAME_TOTALIZATION_TYPE);
                map.put(DB_NAME_LV1TITLE.ToLower(), PROPERTY_NAME_LV1TITLE);
                map.put(DB_NAME_ITEM_NAME.ToLower(), PROPERTY_NAME_ITEM_NAME);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_GT_MATRIX_INFO_ID.ToLower(), DB_NAME_GT_MATRIX_INFO_ID);
                map.put(PROPERTY_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), DB_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(PROPERTY_NAME_BASE_ITEM_ID.ToLower(), DB_NAME_BASE_ITEM_ID);
                map.put(PROPERTY_NAME_NEW_ITEM_ID.ToLower(), DB_NAME_NEW_ITEM_ID);
                map.put(PROPERTY_NAME_TOTALIZATION_TYPE.ToLower(), DB_NAME_TOTALIZATION_TYPE);
                map.put(PROPERTY_NAME_LV1TITLE.ToLower(), DB_NAME_LV1TITLE);
                map.put(PROPERTY_NAME_ITEM_NAME.ToLower(), DB_NAME_ITEM_NAME);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TGtMatrixInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TGtMatrixInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TGtMatrixInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TGtMatrixInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TGtMatrixInfo NewMyEntity() { return new TGtMatrixInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TGtMatrixInfoCB NewMyConditionBean() { return new TGtMatrixInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TGtMatrixInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TGtMatrixInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("GT_MATRIX_INFO_ID", "GtMatrixInfoId", new EntityPropertyGtMatrixInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_TOTALIZATION_ID", "ScenarioTotalizationId", new EntityPropertyScenarioTotalizationIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("BASE_ITEM_ID", "BaseItemId", new EntityPropertyBaseItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_ITEM_ID", "NewItemId", new EntityPropertyNewItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TOTALIZATION_TYPE", "TotalizationType", new EntityPropertyTotalizationTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LV1TITLE", "Lv1title", new EntityPropertyLv1titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_NAME", "ItemName", new EntityPropertyItemNameSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TGtMatrixInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TGtMatrixInfo)entity, value);
        }

        public class EntityPropertyGtMatrixInfoIdSetupper : EntityPropertySetupper<TGtMatrixInfo> {
            public void Setup(TGtMatrixInfo entity, Object value) { entity.GtMatrixInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenarioTotalizationIdSetupper : EntityPropertySetupper<TGtMatrixInfo> {
            public void Setup(TGtMatrixInfo entity, Object value) { entity.ScenarioTotalizationId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyBaseItemIdSetupper : EntityPropertySetupper<TGtMatrixInfo> {
            public void Setup(TGtMatrixInfo entity, Object value) { entity.BaseItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyNewItemIdSetupper : EntityPropertySetupper<TGtMatrixInfo> {
            public void Setup(TGtMatrixInfo entity, Object value) { entity.NewItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTotalizationTypeSetupper : EntityPropertySetupper<TGtMatrixInfo> {
            public void Setup(TGtMatrixInfo entity, Object value) { entity.TotalizationType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLv1titleSetupper : EntityPropertySetupper<TGtMatrixInfo> {
            public void Setup(TGtMatrixInfo entity, Object value) { entity.Lv1title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyItemNameSetupper : EntityPropertySetupper<TGtMatrixInfo> {
            public void Setup(TGtMatrixInfo entity, Object value) { entity.ItemName = (value != null) ? (String)value : null; }
        }
    }
}
