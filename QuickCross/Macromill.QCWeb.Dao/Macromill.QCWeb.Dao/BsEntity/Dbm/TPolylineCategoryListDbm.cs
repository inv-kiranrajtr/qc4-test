
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

    public class TPolylineCategoryListDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TPolylineCategoryList);

        private static readonly TPolylineCategoryListDbm _instance = new TPolylineCategoryListDbm();
        private TPolylineCategoryListDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TPolylineCategoryListDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_POLYLINE_CATEGORY_LIST"; } }
        public override String TablePropertyName { get { return "TPolylineCategoryList"; } }
        public override String TableSqlName { get { return "T_POLYLINE_CATEGORY_LIST"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnPolylineCategoryListId;
        protected ColumnInfo _columnCrossScenarioItemId;
        protected ColumnInfo _columnAxisCategoryNo;
        protected ColumnInfo _columnAxis2CategoryNo;
        protected ColumnInfo _columnArrayNoSingular;
        protected ColumnInfo _columnArrayNoPlural;

        public ColumnInfo ColumnPolylineCategoryListId { get { return _columnPolylineCategoryListId; } }
        public ColumnInfo ColumnCrossScenarioItemId { get { return _columnCrossScenarioItemId; } }
        public ColumnInfo ColumnAxisCategoryNo { get { return _columnAxisCategoryNo; } }
        public ColumnInfo ColumnAxis2CategoryNo { get { return _columnAxis2CategoryNo; } }
        public ColumnInfo ColumnArrayNoSingular { get { return _columnArrayNoSingular; } }
        public ColumnInfo ColumnArrayNoPlural { get { return _columnArrayNoPlural; } }

        protected void InitializeColumnInfo() {
            _columnPolylineCategoryListId = cci("POLYLINE_CATEGORY_LIST_ID", "POLYLINE_CATEGORY_LIST_ID", null, null, true, "PolylineCategoryListId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCrossScenarioItemId = cci("CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", null, null, true, "CrossScenarioItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TCrossScenarioItem", null);
            _columnAxisCategoryNo = cci("AXIS_CATEGORY_NO", "AXIS_CATEGORY_NO", null, null, false, "AxisCategoryNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAxis2CategoryNo = cci("AXIS2_CATEGORY_NO", "AXIS2_CATEGORY_NO", null, null, false, "Axis2CategoryNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnArrayNoSingular = cci("ARRAY_NO_SINGULAR", "ARRAY_NO_SINGULAR", null, null, false, "ArrayNoSingular", typeof(int?), false, "NUMBER", 6, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnArrayNoPlural = cci("ARRAY_NO_PLURAL", "ARRAY_NO_PLURAL", null, null, false, "ArrayNoPlural", typeof(int?), false, "NUMBER", 6, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnPolylineCategoryListId);
            _columnInfoList.add(ColumnCrossScenarioItemId);
            _columnInfoList.add(ColumnAxisCategoryNo);
            _columnInfoList.add(ColumnAxis2CategoryNo);
            _columnInfoList.add(ColumnArrayNoSingular);
            _columnInfoList.add(ColumnArrayNoPlural);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnPolylineCategoryListId);
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
        public ForeignInfo ForeignTCrossScenarioItem { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCrossScenarioItemId, TCrossScenarioItemDbm.GetInstance().ColumnCrossScenarioItemId);
            return cfi("TCrossScenarioItem", this, TCrossScenarioItemDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Polyline_Category_List_SEQ01"; } }
        public override String SequenceNextValSql { get { return "select T_Polyline_Category_List_SEQ01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_POLYLINE_CATEGORY_LIST";
        public static readonly String TABLE_PROPERTY_NAME = "TPolylineCategoryList";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_POLYLINE_CATEGORY_LIST_ID = "POLYLINE_CATEGORY_LIST_ID";
        public static readonly String DB_NAME_CROSS_SCENARIO_ITEM_ID = "CROSS_SCENARIO_ITEM_ID";
        public static readonly String DB_NAME_AXIS_CATEGORY_NO = "AXIS_CATEGORY_NO";
        public static readonly String DB_NAME_AXIS2_CATEGORY_NO = "AXIS2_CATEGORY_NO";
        public static readonly String DB_NAME_ARRAY_NO_SINGULAR = "ARRAY_NO_SINGULAR";
        public static readonly String DB_NAME_ARRAY_NO_PLURAL = "ARRAY_NO_PLURAL";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_POLYLINE_CATEGORY_LIST_ID = "PolylineCategoryListId";
        public static readonly String PROPERTY_NAME_CROSS_SCENARIO_ITEM_ID = "CrossScenarioItemId";
        public static readonly String PROPERTY_NAME_AXIS_CATEGORY_NO = "AxisCategoryNo";
        public static readonly String PROPERTY_NAME_AXIS2_CATEGORY_NO = "Axis2CategoryNo";
        public static readonly String PROPERTY_NAME_ARRAY_NO_SINGULAR = "ArrayNoSingular";
        public static readonly String PROPERTY_NAME_ARRAY_NO_PLURAL = "ArrayNoPlural";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TCrossScenarioItem = "TCrossScenarioItem";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TPolylineCategoryListDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_POLYLINE_CATEGORY_LIST_ID.ToLower(), PROPERTY_NAME_POLYLINE_CATEGORY_LIST_ID);
                map.put(DB_NAME_CROSS_SCENARIO_ITEM_ID.ToLower(), PROPERTY_NAME_CROSS_SCENARIO_ITEM_ID);
                map.put(DB_NAME_AXIS_CATEGORY_NO.ToLower(), PROPERTY_NAME_AXIS_CATEGORY_NO);
                map.put(DB_NAME_AXIS2_CATEGORY_NO.ToLower(), PROPERTY_NAME_AXIS2_CATEGORY_NO);
                map.put(DB_NAME_ARRAY_NO_SINGULAR.ToLower(), PROPERTY_NAME_ARRAY_NO_SINGULAR);
                map.put(DB_NAME_ARRAY_NO_PLURAL.ToLower(), PROPERTY_NAME_ARRAY_NO_PLURAL);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_POLYLINE_CATEGORY_LIST_ID.ToLower(), DB_NAME_POLYLINE_CATEGORY_LIST_ID);
                map.put(PROPERTY_NAME_CROSS_SCENARIO_ITEM_ID.ToLower(), DB_NAME_CROSS_SCENARIO_ITEM_ID);
                map.put(PROPERTY_NAME_AXIS_CATEGORY_NO.ToLower(), DB_NAME_AXIS_CATEGORY_NO);
                map.put(PROPERTY_NAME_AXIS2_CATEGORY_NO.ToLower(), DB_NAME_AXIS2_CATEGORY_NO);
                map.put(PROPERTY_NAME_ARRAY_NO_SINGULAR.ToLower(), DB_NAME_ARRAY_NO_SINGULAR);
                map.put(PROPERTY_NAME_ARRAY_NO_PLURAL.ToLower(), DB_NAME_ARRAY_NO_PLURAL);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TPolylineCategoryList"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TPolylineCategoryListDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TPolylineCategoryListCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TPolylineCategoryListBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TPolylineCategoryList NewMyEntity() { return new TPolylineCategoryList(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TPolylineCategoryListCB NewMyConditionBean() { return new TPolylineCategoryListCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TPolylineCategoryList>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TPolylineCategoryList>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("POLYLINE_CATEGORY_LIST_ID", "PolylineCategoryListId", new EntityPropertyPolylineCategoryListIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CROSS_SCENARIO_ITEM_ID", "CrossScenarioItemId", new EntityPropertyCrossScenarioItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("AXIS_CATEGORY_NO", "AxisCategoryNo", new EntityPropertyAxisCategoryNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("AXIS2_CATEGORY_NO", "Axis2CategoryNo", new EntityPropertyAxis2CategoryNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ARRAY_NO_SINGULAR", "ArrayNoSingular", new EntityPropertyArrayNoSingularSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ARRAY_NO_PLURAL", "ArrayNoPlural", new EntityPropertyArrayNoPluralSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TPolylineCategoryList> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TPolylineCategoryList)entity, value);
        }

        public class EntityPropertyPolylineCategoryListIdSetupper : EntityPropertySetupper<TPolylineCategoryList> {
            public void Setup(TPolylineCategoryList entity, Object value) { entity.PolylineCategoryListId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyCrossScenarioItemIdSetupper : EntityPropertySetupper<TPolylineCategoryList> {
            public void Setup(TPolylineCategoryList entity, Object value) { entity.CrossScenarioItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAxisCategoryNoSetupper : EntityPropertySetupper<TPolylineCategoryList> {
            public void Setup(TPolylineCategoryList entity, Object value) { entity.AxisCategoryNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyAxis2CategoryNoSetupper : EntityPropertySetupper<TPolylineCategoryList> {
            public void Setup(TPolylineCategoryList entity, Object value) { entity.Axis2CategoryNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyArrayNoSingularSetupper : EntityPropertySetupper<TPolylineCategoryList> {
            public void Setup(TPolylineCategoryList entity, Object value) { entity.ArrayNoSingular = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyArrayNoPluralSetupper : EntityPropertySetupper<TPolylineCategoryList> {
            public void Setup(TPolylineCategoryList entity, Object value) { entity.ArrayNoPlural = (value != null) ? (int?)value : null; }
        }
    }
}
