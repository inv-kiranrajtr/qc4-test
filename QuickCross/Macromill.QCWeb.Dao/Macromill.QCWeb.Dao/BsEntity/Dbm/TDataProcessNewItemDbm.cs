
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

    public class TDataProcessNewItemDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDataProcessNewItem);

        private static readonly TDataProcessNewItemDbm _instance = new TDataProcessNewItemDbm();
        private TDataProcessNewItemDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDataProcessNewItemDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_PROCESS_NEW_ITEM"; } }
        public override String TablePropertyName { get { return "TDataProcessNewItem"; } }
        public override String TableSqlName { get { return "T_DATA_PROCESS_NEW_ITEM"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDataEditId;
        protected ColumnInfo _columnSrcItemId;
        protected ColumnInfo _columnNewItemId;
        protected ColumnInfo _columnNewItemName;
        protected ColumnInfo _columnNewLv1title;
        protected ColumnInfo _columnNewLv2title;
        protected ColumnInfo _columnNewAnswerType;
        protected ColumnInfo _columnNewCategoryCount;
        protected ColumnInfo _columnUnfitFlag;
        protected ColumnInfo _columnConditionDiv;
        protected ColumnInfo _columnSeriesFlag;
        protected ColumnInfo _columnUpperFlag;
        protected ColumnInfo _columnBottomFlag;
        protected ColumnInfo _columnNoanswerZeroFlag;
        protected ColumnInfo _columnSelectMethod;
        protected ColumnInfo _columnTargetCategoryCondition;
        protected ColumnInfo _columnCalcType;
        protected ColumnInfo _columnFormulaString;

        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }
        public ColumnInfo ColumnSrcItemId { get { return _columnSrcItemId; } }
        public ColumnInfo ColumnNewItemId { get { return _columnNewItemId; } }
        public ColumnInfo ColumnNewItemName { get { return _columnNewItemName; } }
        public ColumnInfo ColumnNewLv1title { get { return _columnNewLv1title; } }
        public ColumnInfo ColumnNewLv2title { get { return _columnNewLv2title; } }
        public ColumnInfo ColumnNewAnswerType { get { return _columnNewAnswerType; } }
        public ColumnInfo ColumnNewCategoryCount { get { return _columnNewCategoryCount; } }
        public ColumnInfo ColumnUnfitFlag { get { return _columnUnfitFlag; } }
        public ColumnInfo ColumnConditionDiv { get { return _columnConditionDiv; } }
        public ColumnInfo ColumnSeriesFlag { get { return _columnSeriesFlag; } }
        public ColumnInfo ColumnUpperFlag { get { return _columnUpperFlag; } }
        public ColumnInfo ColumnBottomFlag { get { return _columnBottomFlag; } }
        public ColumnInfo ColumnNoanswerZeroFlag { get { return _columnNoanswerZeroFlag; } }
        public ColumnInfo ColumnSelectMethod { get { return _columnSelectMethod; } }
        public ColumnInfo ColumnTargetCategoryCondition { get { return _columnTargetCategoryCondition; } }
        public ColumnInfo ColumnCalcType { get { return _columnCalcType; } }
        public ColumnInfo ColumnFormulaString { get { return _columnFormulaString; } }

        protected void InitializeColumnInfo() {
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, true, "DataEditId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDataEditList", "TDataProcessNewCategoryList,TDataProcessNewItemSrcList,TIntegConditionList");
            _columnSrcItemId = cci("SRC_ITEM_ID", "SRC_ITEM_ID", null, null, false, "SrcItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewItemId = cci("NEW_ITEM_ID", "NEW_ITEM_ID", null, null, true, "NewItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewItemName = cci("NEW_ITEM_NAME", "NEW_ITEM_NAME", null, null, true, "NewItemName", typeof(String), false, "NVARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewLv1title = cci("NEW_LV1TITLE", "NEW_LV1TITLE", null, null, false, "NewLv1title", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewLv2title = cci("NEW_LV2TITLE", "NEW_LV2TITLE", null, null, false, "NewLv2title", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewAnswerType = cci("NEW_ANSWER_TYPE", "NEW_ANSWER_TYPE", null, null, true, "NewAnswerType", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewCategoryCount = cci("NEW_CATEGORY_COUNT", "NEW_CATEGORY_COUNT", null, null, true, "NewCategoryCount", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUnfitFlag = cci("UNFIT_FLAG", "UNFIT_FLAG", null, null, true, "UnfitFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionDiv = cci("CONDITION_DIV", "CONDITION_DIV", null, null, true, "ConditionDiv", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSeriesFlag = cci("SERIES_FLAG", "SERIES_FLAG", null, null, true, "SeriesFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUpperFlag = cci("UPPER_FLAG", "UPPER_FLAG", null, null, true, "UpperFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnBottomFlag = cci("BOTTOM_FLAG", "BOTTOM_FLAG", null, null, true, "BottomFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNoanswerZeroFlag = cci("NOANSWER_ZERO_FLAG", "NOANSWER_ZERO_FLAG", null, null, true, "NoanswerZeroFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSelectMethod = cci("SELECT_METHOD", "SELECT_METHOD", null, null, true, "SelectMethod", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTargetCategoryCondition = cci("TARGET_CATEGORY_CONDITION", "TARGET_CATEGORY_CONDITION", null, null, false, "TargetCategoryCondition", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCalcType = cci("CALC_TYPE", "CALC_TYPE", null, null, false, "CalcType", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFormulaString = cci("FORMULA_STRING", "FORMULA_STRING", null, null, false, "FormulaString", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDataEditId);
            _columnInfoList.add(ColumnSrcItemId);
            _columnInfoList.add(ColumnNewItemId);
            _columnInfoList.add(ColumnNewItemName);
            _columnInfoList.add(ColumnNewLv1title);
            _columnInfoList.add(ColumnNewLv2title);
            _columnInfoList.add(ColumnNewAnswerType);
            _columnInfoList.add(ColumnNewCategoryCount);
            _columnInfoList.add(ColumnUnfitFlag);
            _columnInfoList.add(ColumnConditionDiv);
            _columnInfoList.add(ColumnSeriesFlag);
            _columnInfoList.add(ColumnUpperFlag);
            _columnInfoList.add(ColumnBottomFlag);
            _columnInfoList.add(ColumnNoanswerZeroFlag);
            _columnInfoList.add(ColumnSelectMethod);
            _columnInfoList.add(ColumnTargetCategoryCondition);
            _columnInfoList.add(ColumnCalcType);
            _columnInfoList.add(ColumnFormulaString);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnDataEditId);
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
        public ForeignInfo ForeignTDataEditList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDataEditListDbm.GetInstance().ColumnDataEditId);
            return cfi("TDataEditList", this, TDataEditListDbm.GetInstance(), map, 0, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTDataProcessNewCategoryList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDataProcessNewCategoryDbm.GetInstance().ColumnDataEditId);
            return cri("TDataProcessNewCategoryList", this, TDataProcessNewCategoryDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTDataProcessNewItemSrcList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDataProcessNewItemSrcDbm.GetInstance().ColumnDataEditId);
            return cri("TDataProcessNewItemSrcList", this, TDataProcessNewItemSrcDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTIntegConditionList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TIntegConditionDbm.GetInstance().ColumnDataEditId);
            return cri("TIntegConditionList", this, TIntegConditionDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasCommonColumn { get { return false; } }

        // ===============================================================================
        //                                                                 Name Definition
        //                                                                 ===============
        #region Name

        // -------------------------------------------------
        //                                             Table
        //                                             -----
        public static readonly String TABLE_DB_NAME = "T_DATA_PROCESS_NEW_ITEM";
        public static readonly String TABLE_PROPERTY_NAME = "TDataProcessNewItem";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";
        public static readonly String DB_NAME_SRC_ITEM_ID = "SRC_ITEM_ID";
        public static readonly String DB_NAME_NEW_ITEM_ID = "NEW_ITEM_ID";
        public static readonly String DB_NAME_NEW_ITEM_NAME = "NEW_ITEM_NAME";
        public static readonly String DB_NAME_NEW_LV1TITLE = "NEW_LV1TITLE";
        public static readonly String DB_NAME_NEW_LV2TITLE = "NEW_LV2TITLE";
        public static readonly String DB_NAME_NEW_ANSWER_TYPE = "NEW_ANSWER_TYPE";
        public static readonly String DB_NAME_NEW_CATEGORY_COUNT = "NEW_CATEGORY_COUNT";
        public static readonly String DB_NAME_UNFIT_FLAG = "UNFIT_FLAG";
        public static readonly String DB_NAME_CONDITION_DIV = "CONDITION_DIV";
        public static readonly String DB_NAME_SERIES_FLAG = "SERIES_FLAG";
        public static readonly String DB_NAME_UPPER_FLAG = "UPPER_FLAG";
        public static readonly String DB_NAME_BOTTOM_FLAG = "BOTTOM_FLAG";
        public static readonly String DB_NAME_NOANSWER_ZERO_FLAG = "NOANSWER_ZERO_FLAG";
        public static readonly String DB_NAME_SELECT_METHOD = "SELECT_METHOD";
        public static readonly String DB_NAME_TARGET_CATEGORY_CONDITION = "TARGET_CATEGORY_CONDITION";
        public static readonly String DB_NAME_CALC_TYPE = "CALC_TYPE";
        public static readonly String DB_NAME_FORMULA_STRING = "FORMULA_STRING";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";
        public static readonly String PROPERTY_NAME_SRC_ITEM_ID = "SrcItemId";
        public static readonly String PROPERTY_NAME_NEW_ITEM_ID = "NewItemId";
        public static readonly String PROPERTY_NAME_NEW_ITEM_NAME = "NewItemName";
        public static readonly String PROPERTY_NAME_NEW_LV1TITLE = "NewLv1title";
        public static readonly String PROPERTY_NAME_NEW_LV2TITLE = "NewLv2title";
        public static readonly String PROPERTY_NAME_NEW_ANSWER_TYPE = "NewAnswerType";
        public static readonly String PROPERTY_NAME_NEW_CATEGORY_COUNT = "NewCategoryCount";
        public static readonly String PROPERTY_NAME_UNFIT_FLAG = "UnfitFlag";
        public static readonly String PROPERTY_NAME_CONDITION_DIV = "ConditionDiv";
        public static readonly String PROPERTY_NAME_SERIES_FLAG = "SeriesFlag";
        public static readonly String PROPERTY_NAME_UPPER_FLAG = "UpperFlag";
        public static readonly String PROPERTY_NAME_BOTTOM_FLAG = "BottomFlag";
        public static readonly String PROPERTY_NAME_NOANSWER_ZERO_FLAG = "NoanswerZeroFlag";
        public static readonly String PROPERTY_NAME_SELECT_METHOD = "SelectMethod";
        public static readonly String PROPERTY_NAME_TARGET_CATEGORY_CONDITION = "TargetCategoryCondition";
        public static readonly String PROPERTY_NAME_CALC_TYPE = "CalcType";
        public static readonly String PROPERTY_NAME_FORMULA_STRING = "FormulaString";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TDataEditList = "TDataEditList";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TDataProcessNewCategoryList = "TDataProcessNewCategoryList";
        public static readonly String REFERRER_PROPERTY_NAME_TDataProcessNewItemSrcList = "TDataProcessNewItemSrcList";
        public static readonly String REFERRER_PROPERTY_NAME_TIntegConditionList = "TIntegConditionList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TDataProcessNewItemDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                map.put(DB_NAME_SRC_ITEM_ID.ToLower(), PROPERTY_NAME_SRC_ITEM_ID);
                map.put(DB_NAME_NEW_ITEM_ID.ToLower(), PROPERTY_NAME_NEW_ITEM_ID);
                map.put(DB_NAME_NEW_ITEM_NAME.ToLower(), PROPERTY_NAME_NEW_ITEM_NAME);
                map.put(DB_NAME_NEW_LV1TITLE.ToLower(), PROPERTY_NAME_NEW_LV1TITLE);
                map.put(DB_NAME_NEW_LV2TITLE.ToLower(), PROPERTY_NAME_NEW_LV2TITLE);
                map.put(DB_NAME_NEW_ANSWER_TYPE.ToLower(), PROPERTY_NAME_NEW_ANSWER_TYPE);
                map.put(DB_NAME_NEW_CATEGORY_COUNT.ToLower(), PROPERTY_NAME_NEW_CATEGORY_COUNT);
                map.put(DB_NAME_UNFIT_FLAG.ToLower(), PROPERTY_NAME_UNFIT_FLAG);
                map.put(DB_NAME_CONDITION_DIV.ToLower(), PROPERTY_NAME_CONDITION_DIV);
                map.put(DB_NAME_SERIES_FLAG.ToLower(), PROPERTY_NAME_SERIES_FLAG);
                map.put(DB_NAME_UPPER_FLAG.ToLower(), PROPERTY_NAME_UPPER_FLAG);
                map.put(DB_NAME_BOTTOM_FLAG.ToLower(), PROPERTY_NAME_BOTTOM_FLAG);
                map.put(DB_NAME_NOANSWER_ZERO_FLAG.ToLower(), PROPERTY_NAME_NOANSWER_ZERO_FLAG);
                map.put(DB_NAME_SELECT_METHOD.ToLower(), PROPERTY_NAME_SELECT_METHOD);
                map.put(DB_NAME_TARGET_CATEGORY_CONDITION.ToLower(), PROPERTY_NAME_TARGET_CATEGORY_CONDITION);
                map.put(DB_NAME_CALC_TYPE.ToLower(), PROPERTY_NAME_CALC_TYPE);
                map.put(DB_NAME_FORMULA_STRING.ToLower(), PROPERTY_NAME_FORMULA_STRING);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DATA_EDIT_ID.ToLower(), DB_NAME_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_SRC_ITEM_ID.ToLower(), DB_NAME_SRC_ITEM_ID);
                map.put(PROPERTY_NAME_NEW_ITEM_ID.ToLower(), DB_NAME_NEW_ITEM_ID);
                map.put(PROPERTY_NAME_NEW_ITEM_NAME.ToLower(), DB_NAME_NEW_ITEM_NAME);
                map.put(PROPERTY_NAME_NEW_LV1TITLE.ToLower(), DB_NAME_NEW_LV1TITLE);
                map.put(PROPERTY_NAME_NEW_LV2TITLE.ToLower(), DB_NAME_NEW_LV2TITLE);
                map.put(PROPERTY_NAME_NEW_ANSWER_TYPE.ToLower(), DB_NAME_NEW_ANSWER_TYPE);
                map.put(PROPERTY_NAME_NEW_CATEGORY_COUNT.ToLower(), DB_NAME_NEW_CATEGORY_COUNT);
                map.put(PROPERTY_NAME_UNFIT_FLAG.ToLower(), DB_NAME_UNFIT_FLAG);
                map.put(PROPERTY_NAME_CONDITION_DIV.ToLower(), DB_NAME_CONDITION_DIV);
                map.put(PROPERTY_NAME_SERIES_FLAG.ToLower(), DB_NAME_SERIES_FLAG);
                map.put(PROPERTY_NAME_UPPER_FLAG.ToLower(), DB_NAME_UPPER_FLAG);
                map.put(PROPERTY_NAME_BOTTOM_FLAG.ToLower(), DB_NAME_BOTTOM_FLAG);
                map.put(PROPERTY_NAME_NOANSWER_ZERO_FLAG.ToLower(), DB_NAME_NOANSWER_ZERO_FLAG);
                map.put(PROPERTY_NAME_SELECT_METHOD.ToLower(), DB_NAME_SELECT_METHOD);
                map.put(PROPERTY_NAME_TARGET_CATEGORY_CONDITION.ToLower(), DB_NAME_TARGET_CATEGORY_CONDITION);
                map.put(PROPERTY_NAME_CALC_TYPE.ToLower(), DB_NAME_CALC_TYPE);
                map.put(PROPERTY_NAME_FORMULA_STRING.ToLower(), DB_NAME_FORMULA_STRING);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDataProcessNewItem"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDataProcessNewItemDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDataProcessNewItemCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDataProcessNewItemBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDataProcessNewItem NewMyEntity() { return new TDataProcessNewItem(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDataProcessNewItemCB NewMyConditionBean() { return new TDataProcessNewItemCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDataProcessNewItem>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDataProcessNewItem>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SRC_ITEM_ID", "SrcItemId", new EntityPropertySrcItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_ITEM_ID", "NewItemId", new EntityPropertyNewItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_ITEM_NAME", "NewItemName", new EntityPropertyNewItemNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_LV1TITLE", "NewLv1title", new EntityPropertyNewLv1titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_LV2TITLE", "NewLv2title", new EntityPropertyNewLv2titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_ANSWER_TYPE", "NewAnswerType", new EntityPropertyNewAnswerTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_CATEGORY_COUNT", "NewCategoryCount", new EntityPropertyNewCategoryCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UNFIT_FLAG", "UnfitFlag", new EntityPropertyUnfitFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_DIV", "ConditionDiv", new EntityPropertyConditionDivSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SERIES_FLAG", "SeriesFlag", new EntityPropertySeriesFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UPPER_FLAG", "UpperFlag", new EntityPropertyUpperFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("BOTTOM_FLAG", "BottomFlag", new EntityPropertyBottomFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NOANSWER_ZERO_FLAG", "NoanswerZeroFlag", new EntityPropertyNoanswerZeroFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SELECT_METHOD", "SelectMethod", new EntityPropertySelectMethodSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TARGET_CATEGORY_CONDITION", "TargetCategoryCondition", new EntityPropertyTargetCategoryConditionSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CALC_TYPE", "CalcType", new EntityPropertyCalcTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FORMULA_STRING", "FormulaString", new EntityPropertyFormulaStringSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDataProcessNewItem> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDataProcessNewItem)entity, value);
        }

        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySrcItemIdSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.SrcItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyNewItemIdSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.NewItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyNewItemNameSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.NewItemName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNewLv1titleSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.NewLv1title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNewLv2titleSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.NewLv2title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNewAnswerTypeSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.NewAnswerType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNewCategoryCountSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.NewCategoryCount = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyUnfitFlagSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.UnfitFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyConditionDivSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.ConditionDiv = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySeriesFlagSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.SeriesFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyUpperFlagSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.UpperFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyBottomFlagSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.BottomFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNoanswerZeroFlagSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.NoanswerZeroFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySelectMethodSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.SelectMethod = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTargetCategoryConditionSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.TargetCategoryCondition = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCalcTypeSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.CalcType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFormulaStringSetupper : EntityPropertySetupper<TDataProcessNewItem> {
            public void Setup(TDataProcessNewItem entity, Object value) { entity.FormulaString = (value != null) ? (String)value : null; }
        }
    }
}
