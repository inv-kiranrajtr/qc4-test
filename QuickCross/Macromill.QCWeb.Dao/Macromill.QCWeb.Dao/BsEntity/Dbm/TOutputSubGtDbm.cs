
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

    public class TOutputSubGtDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputSubGt);

        private static readonly TOutputSubGtDbm _instance = new TOutputSubGtDbm();
        private TOutputSubGtDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputSubGtDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_GT"; } }
        public override String TablePropertyName { get { return "TOutputSubGt"; } }
        public override String TableSqlName { get { return "T_OUTPUT_SUB_GT"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputSubGtId;
        protected ColumnInfo _columnOutputCommonId;
        protected ColumnInfo _columnOutputTableType;
        protected ColumnInfo _columnOutputTableOrientation;
        protected ColumnInfo _columnPageSettingTableType;
        protected ColumnInfo _columnPageSettingPaperSize;
        protected ColumnInfo _columnPageSettingPaperOrientation;
        protected ColumnInfo _columnMarkingLevel;
        protected ColumnInfo _columnMarkingMinParameter;
        protected ColumnInfo _columnMarkingCode;
        protected ColumnInfo _columnFilteringExpression;

        public ColumnInfo ColumnOutputSubGtId { get { return _columnOutputSubGtId; } }
        public ColumnInfo ColumnOutputCommonId { get { return _columnOutputCommonId; } }
        public ColumnInfo ColumnOutputTableType { get { return _columnOutputTableType; } }
        public ColumnInfo ColumnOutputTableOrientation { get { return _columnOutputTableOrientation; } }
        public ColumnInfo ColumnPageSettingTableType { get { return _columnPageSettingTableType; } }
        public ColumnInfo ColumnPageSettingPaperSize { get { return _columnPageSettingPaperSize; } }
        public ColumnInfo ColumnPageSettingPaperOrientation { get { return _columnPageSettingPaperOrientation; } }
        public ColumnInfo ColumnMarkingLevel { get { return _columnMarkingLevel; } }
        public ColumnInfo ColumnMarkingMinParameter { get { return _columnMarkingMinParameter; } }
        public ColumnInfo ColumnMarkingCode { get { return _columnMarkingCode; } }
        public ColumnInfo ColumnFilteringExpression { get { return _columnFilteringExpression; } }

        protected void InitializeColumnInfo() {
            _columnOutputSubGtId = cci("OUTPUT_SUB_GT_ID", "OUTPUT_SUB_GT_ID", null, null, true, "OutputSubGtId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputCommonId = cci("OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", null, null, true, "OutputCommonId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputCommon", null);
            _columnOutputTableType = cci("OUTPUT_TABLE_TYPE", "OUTPUT_TABLE_TYPE", null, null, true, "OutputTableType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputTableOrientation = cci("OUTPUT_TABLE_ORIENTATION", "OUTPUT_TABLE_ORIENTATION", null, null, true, "OutputTableOrientation", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingTableType = cci("PAGE_SETTING_TABLE_TYPE", "PAGE_SETTING_TABLE_TYPE", null, null, true, "PageSettingTableType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperSize = cci("PAGE_SETTING_PAPER_SIZE", "PAGE_SETTING_PAPER_SIZE", null, null, false, "PageSettingPaperSize", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperOrientation = cci("PAGE_SETTING_PAPER_ORIENTATION", "PAGE_SETTING_PAPER_ORIENTATION", null, null, false, "PageSettingPaperOrientation", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMarkingLevel = cci("MARKING_LEVEL", "MARKING_LEVEL", null, null, false, "MarkingLevel", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMarkingMinParameter = cci("MARKING_MIN_PARAMETER", "MARKING_MIN_PARAMETER", null, null, false, "MarkingMinParameter", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMarkingCode = cci("MARKING_CODE", "MARKING_CODE", null, null, false, "MarkingCode", typeof(int?), false, "NUMBER", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFilteringExpression = cci("FILTERING_EXPRESSION", "FILTERING_EXPRESSION", null, null, false, "FilteringExpression", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputSubGtId);
            _columnInfoList.add(ColumnOutputCommonId);
            _columnInfoList.add(ColumnOutputTableType);
            _columnInfoList.add(ColumnOutputTableOrientation);
            _columnInfoList.add(ColumnPageSettingTableType);
            _columnInfoList.add(ColumnPageSettingPaperSize);
            _columnInfoList.add(ColumnPageSettingPaperOrientation);
            _columnInfoList.add(ColumnMarkingLevel);
            _columnInfoList.add(ColumnMarkingMinParameter);
            _columnInfoList.add(ColumnMarkingCode);
            _columnInfoList.add(ColumnFilteringExpression);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputSubGtId);
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
        public ForeignInfo ForeignTOutputCommon { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputCommonId, TOutputCommonDbm.GetInstance().ColumnOutputCommonId);
            return cfi("TOutputCommon", this, TOutputCommonDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Output_Sub_GT_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Output_Sub_GT_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_SUB_GT";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputSubGt";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_SUB_GT_ID = "OUTPUT_SUB_GT_ID";
        public static readonly String DB_NAME_OUTPUT_COMMON_ID = "OUTPUT_COMMON_ID";
        public static readonly String DB_NAME_OUTPUT_TABLE_TYPE = "OUTPUT_TABLE_TYPE";
        public static readonly String DB_NAME_OUTPUT_TABLE_ORIENTATION = "OUTPUT_TABLE_ORIENTATION";
        public static readonly String DB_NAME_PAGE_SETTING_TABLE_TYPE = "PAGE_SETTING_TABLE_TYPE";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_SIZE = "PAGE_SETTING_PAPER_SIZE";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PAGE_SETTING_PAPER_ORIENTATION";
        public static readonly String DB_NAME_MARKING_LEVEL = "MARKING_LEVEL";
        public static readonly String DB_NAME_MARKING_MIN_PARAMETER = "MARKING_MIN_PARAMETER";
        public static readonly String DB_NAME_MARKING_CODE = "MARKING_CODE";
        public static readonly String DB_NAME_FILTERING_EXPRESSION = "FILTERING_EXPRESSION";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_SUB_GT_ID = "OutputSubGtId";
        public static readonly String PROPERTY_NAME_OUTPUT_COMMON_ID = "OutputCommonId";
        public static readonly String PROPERTY_NAME_OUTPUT_TABLE_TYPE = "OutputTableType";
        public static readonly String PROPERTY_NAME_OUTPUT_TABLE_ORIENTATION = "OutputTableOrientation";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_TABLE_TYPE = "PageSettingTableType";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE = "PageSettingPaperSize";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PageSettingPaperOrientation";
        public static readonly String PROPERTY_NAME_MARKING_LEVEL = "MarkingLevel";
        public static readonly String PROPERTY_NAME_MARKING_MIN_PARAMETER = "MarkingMinParameter";
        public static readonly String PROPERTY_NAME_MARKING_CODE = "MarkingCode";
        public static readonly String PROPERTY_NAME_FILTERING_EXPRESSION = "FilteringExpression";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputCommon = "TOutputCommon";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputSubGtDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_SUB_GT_ID.ToLower(), PROPERTY_NAME_OUTPUT_SUB_GT_ID);
                map.put(DB_NAME_OUTPUT_COMMON_ID.ToLower(), PROPERTY_NAME_OUTPUT_COMMON_ID);
                map.put(DB_NAME_OUTPUT_TABLE_TYPE.ToLower(), PROPERTY_NAME_OUTPUT_TABLE_TYPE);
                map.put(DB_NAME_OUTPUT_TABLE_ORIENTATION.ToLower(), PROPERTY_NAME_OUTPUT_TABLE_ORIENTATION);
                map.put(DB_NAME_PAGE_SETTING_TABLE_TYPE.ToLower(), PROPERTY_NAME_PAGE_SETTING_TABLE_TYPE);
                map.put(DB_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(DB_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION);
                map.put(DB_NAME_MARKING_LEVEL.ToLower(), PROPERTY_NAME_MARKING_LEVEL);
                map.put(DB_NAME_MARKING_MIN_PARAMETER.ToLower(), PROPERTY_NAME_MARKING_MIN_PARAMETER);
                map.put(DB_NAME_MARKING_CODE.ToLower(), PROPERTY_NAME_MARKING_CODE);
                map.put(DB_NAME_FILTERING_EXPRESSION.ToLower(), PROPERTY_NAME_FILTERING_EXPRESSION);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_SUB_GT_ID.ToLower(), DB_NAME_OUTPUT_SUB_GT_ID);
                map.put(PROPERTY_NAME_OUTPUT_COMMON_ID.ToLower(), DB_NAME_OUTPUT_COMMON_ID);
                map.put(PROPERTY_NAME_OUTPUT_TABLE_TYPE.ToLower(), DB_NAME_OUTPUT_TABLE_TYPE);
                map.put(PROPERTY_NAME_OUTPUT_TABLE_ORIENTATION.ToLower(), DB_NAME_OUTPUT_TABLE_ORIENTATION);
                map.put(PROPERTY_NAME_PAGE_SETTING_TABLE_TYPE.ToLower(), DB_NAME_PAGE_SETTING_TABLE_TYPE);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), DB_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), DB_NAME_PAGE_SETTING_PAPER_ORIENTATION);
                map.put(PROPERTY_NAME_MARKING_LEVEL.ToLower(), DB_NAME_MARKING_LEVEL);
                map.put(PROPERTY_NAME_MARKING_MIN_PARAMETER.ToLower(), DB_NAME_MARKING_MIN_PARAMETER);
                map.put(PROPERTY_NAME_MARKING_CODE.ToLower(), DB_NAME_MARKING_CODE);
                map.put(PROPERTY_NAME_FILTERING_EXPRESSION.ToLower(), DB_NAME_FILTERING_EXPRESSION);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputSubGt"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputSubGtDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputSubGtCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputSubGtBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputSubGt NewMyEntity() { return new TOutputSubGt(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputSubGtCB NewMyConditionBean() { return new TOutputSubGtCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputSubGt>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputSubGt>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_SUB_GT_ID", "OutputSubGtId", new EntityPropertyOutputSubGtIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_COMMON_ID", "OutputCommonId", new EntityPropertyOutputCommonIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TABLE_TYPE", "OutputTableType", new EntityPropertyOutputTableTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TABLE_ORIENTATION", "OutputTableOrientation", new EntityPropertyOutputTableOrientationSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_TABLE_TYPE", "PageSettingTableType", new EntityPropertyPageSettingTableTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_SIZE", "PageSettingPaperSize", new EntityPropertyPageSettingPaperSizeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_ORIENTATION", "PageSettingPaperOrientation", new EntityPropertyPageSettingPaperOrientationSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MARKING_LEVEL", "MarkingLevel", new EntityPropertyMarkingLevelSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MARKING_MIN_PARAMETER", "MarkingMinParameter", new EntityPropertyMarkingMinParameterSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MARKING_CODE", "MarkingCode", new EntityPropertyMarkingCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FILTERING_EXPRESSION", "FilteringExpression", new EntityPropertyFilteringExpressionSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputSubGt> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputSubGt)entity, value);
        }

        public class EntityPropertyOutputSubGtIdSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.OutputSubGtId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputCommonIdSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.OutputCommonId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputTableTypeSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.OutputTableType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputTableOrientationSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.OutputTableOrientation = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingTableTypeSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.PageSettingTableType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperSizeSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.PageSettingPaperSize = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperOrientationSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.PageSettingPaperOrientation = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMarkingLevelSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.MarkingLevel = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMarkingMinParameterSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.MarkingMinParameter = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyMarkingCodeSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.MarkingCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFilteringExpressionSetupper : EntityPropertySetupper<TOutputSubGt> {
            public void Setup(TOutputSubGt entity, Object value) { entity.FilteringExpression = (value != null) ? (String)value : null; }
        }
    }
}
