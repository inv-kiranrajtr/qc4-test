
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

    public class TOutputSubCrossDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputSubCross);

        private static readonly TOutputSubCrossDbm _instance = new TOutputSubCrossDbm();
        private TOutputSubCrossDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputSubCrossDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_CROSS"; } }
        public override String TablePropertyName { get { return "TOutputSubCross"; } }
        public override String TableSqlName { get { return "T_OUTPUT_SUB_CROSS"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputSubCrossId;
        protected ColumnInfo _columnOutputCommonId;
        protected ColumnInfo _columnOutputType;
        protected ColumnInfo _columnOutputTableType;
        protected ColumnInfo _columnOutputTableOrientation;
        protected ColumnInfo _columnPageSettingTableType;
        protected ColumnInfo _columnPageSettingPaperSize;
        protected ColumnInfo _columnPageSettingPaperOrientation;
        protected ColumnInfo _columnMarkingMinParameter;
        protected ColumnInfo _columnMarkingCode;
        protected ColumnInfo _columnMarkingLevel;
        protected ColumnInfo _columnLevel2pluscolor;
        protected ColumnInfo _columnLevel1pluscolor;
        protected ColumnInfo _columnLevel1minuscolor;
        protected ColumnInfo _columnLevel2minuscolor;
        protected ColumnInfo _columnLevel2percent;
        protected ColumnInfo _columnLevel1percent;
        protected ColumnInfo _columnFilteringExpression;

        public ColumnInfo ColumnOutputSubCrossId { get { return _columnOutputSubCrossId; } }
        public ColumnInfo ColumnOutputCommonId { get { return _columnOutputCommonId; } }
        public ColumnInfo ColumnOutputType { get { return _columnOutputType; } }
        public ColumnInfo ColumnOutputTableType { get { return _columnOutputTableType; } }
        public ColumnInfo ColumnOutputTableOrientation { get { return _columnOutputTableOrientation; } }
        public ColumnInfo ColumnPageSettingTableType { get { return _columnPageSettingTableType; } }
        public ColumnInfo ColumnPageSettingPaperSize { get { return _columnPageSettingPaperSize; } }
        public ColumnInfo ColumnPageSettingPaperOrientation { get { return _columnPageSettingPaperOrientation; } }
        public ColumnInfo ColumnMarkingMinParameter { get { return _columnMarkingMinParameter; } }
        public ColumnInfo ColumnMarkingCode { get { return _columnMarkingCode; } }
        public ColumnInfo ColumnMarkingLevel { get { return _columnMarkingLevel; } }
        public ColumnInfo ColumnLevel2pluscolor { get { return _columnLevel2pluscolor; } }
        public ColumnInfo ColumnLevel1pluscolor { get { return _columnLevel1pluscolor; } }
        public ColumnInfo ColumnLevel1minuscolor { get { return _columnLevel1minuscolor; } }
        public ColumnInfo ColumnLevel2minuscolor { get { return _columnLevel2minuscolor; } }
        public ColumnInfo ColumnLevel2percent { get { return _columnLevel2percent; } }
        public ColumnInfo ColumnLevel1percent { get { return _columnLevel1percent; } }
        public ColumnInfo ColumnFilteringExpression { get { return _columnFilteringExpression; } }

        protected void InitializeColumnInfo() {
            _columnOutputSubCrossId = cci("OUTPUT_SUB_CROSS_ID", "OUTPUT_SUB_CROSS_ID", null, null, true, "OutputSubCrossId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputCommonId = cci("OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", null, null, true, "OutputCommonId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputCommon", null);
            _columnOutputType = cci("OUTPUT_TYPE", "OUTPUT_TYPE", null, null, true, "OutputType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputTableType = cci("OUTPUT_TABLE_TYPE", "OUTPUT_TABLE_TYPE", null, null, true, "OutputTableType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputTableOrientation = cci("OUTPUT_TABLE_ORIENTATION", "OUTPUT_TABLE_ORIENTATION", null, null, true, "OutputTableOrientation", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingTableType = cci("PAGE_SETTING_TABLE_TYPE", "PAGE_SETTING_TABLE_TYPE", null, null, false, "PageSettingTableType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperSize = cci("PAGE_SETTING_PAPER_SIZE", "PAGE_SETTING_PAPER_SIZE", null, null, false, "PageSettingPaperSize", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperOrientation = cci("PAGE_SETTING_PAPER_ORIENTATION", "PAGE_SETTING_PAPER_ORIENTATION", null, null, false, "PageSettingPaperOrientation", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMarkingMinParameter = cci("MARKING_MIN_PARAMETER", "MARKING_MIN_PARAMETER", null, null, false, "MarkingMinParameter", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMarkingCode = cci("MARKING_CODE", "MARKING_CODE", null, null, false, "MarkingCode", typeof(int?), false, "NUMBER", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMarkingLevel = cci("MARKING_LEVEL", "MARKING_LEVEL", null, null, false, "MarkingLevel", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLevel2pluscolor = cci("LEVEL2PLUSCOLOR", "LEVEL2PLUSCOLOR", null, null, false, "Level2pluscolor", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLevel1pluscolor = cci("LEVEL1PLUSCOLOR", "LEVEL1PLUSCOLOR", null, null, false, "Level1pluscolor", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLevel1minuscolor = cci("LEVEL1MINUSCOLOR", "LEVEL1MINUSCOLOR", null, null, false, "Level1minuscolor", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLevel2minuscolor = cci("LEVEL2MINUSCOLOR", "LEVEL2MINUSCOLOR", null, null, false, "Level2minuscolor", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLevel2percent = cci("LEVEL2PERCENT", "LEVEL2PERCENT", null, null, false, "Level2percent", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLevel1percent = cci("LEVEL1PERCENT", "LEVEL1PERCENT", null, null, false, "Level1percent", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFilteringExpression = cci("FILTERING_EXPRESSION", "FILTERING_EXPRESSION", null, null, false, "FilteringExpression", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputSubCrossId);
            _columnInfoList.add(ColumnOutputCommonId);
            _columnInfoList.add(ColumnOutputType);
            _columnInfoList.add(ColumnOutputTableType);
            _columnInfoList.add(ColumnOutputTableOrientation);
            _columnInfoList.add(ColumnPageSettingTableType);
            _columnInfoList.add(ColumnPageSettingPaperSize);
            _columnInfoList.add(ColumnPageSettingPaperOrientation);
            _columnInfoList.add(ColumnMarkingMinParameter);
            _columnInfoList.add(ColumnMarkingCode);
            _columnInfoList.add(ColumnMarkingLevel);
            _columnInfoList.add(ColumnLevel2pluscolor);
            _columnInfoList.add(ColumnLevel1pluscolor);
            _columnInfoList.add(ColumnLevel1minuscolor);
            _columnInfoList.add(ColumnLevel2minuscolor);
            _columnInfoList.add(ColumnLevel2percent);
            _columnInfoList.add(ColumnLevel1percent);
            _columnInfoList.add(ColumnFilteringExpression);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputSubCrossId);
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
        public override String SequenceName { get { return "T_Output_Sub_Cross_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Output_Sub_Cross_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_SUB_CROSS";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputSubCross";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_SUB_CROSS_ID = "OUTPUT_SUB_CROSS_ID";
        public static readonly String DB_NAME_OUTPUT_COMMON_ID = "OUTPUT_COMMON_ID";
        public static readonly String DB_NAME_OUTPUT_TYPE = "OUTPUT_TYPE";
        public static readonly String DB_NAME_OUTPUT_TABLE_TYPE = "OUTPUT_TABLE_TYPE";
        public static readonly String DB_NAME_OUTPUT_TABLE_ORIENTATION = "OUTPUT_TABLE_ORIENTATION";
        public static readonly String DB_NAME_PAGE_SETTING_TABLE_TYPE = "PAGE_SETTING_TABLE_TYPE";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_SIZE = "PAGE_SETTING_PAPER_SIZE";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PAGE_SETTING_PAPER_ORIENTATION";
        public static readonly String DB_NAME_MARKING_MIN_PARAMETER = "MARKING_MIN_PARAMETER";
        public static readonly String DB_NAME_MARKING_CODE = "MARKING_CODE";
        public static readonly String DB_NAME_MARKING_LEVEL = "MARKING_LEVEL";
        public static readonly String DB_NAME_LEVEL2PLUSCOLOR = "LEVEL2PLUSCOLOR";
        public static readonly String DB_NAME_LEVEL1PLUSCOLOR = "LEVEL1PLUSCOLOR";
        public static readonly String DB_NAME_LEVEL1MINUSCOLOR = "LEVEL1MINUSCOLOR";
        public static readonly String DB_NAME_LEVEL2MINUSCOLOR = "LEVEL2MINUSCOLOR";
        public static readonly String DB_NAME_LEVEL2PERCENT = "LEVEL2PERCENT";
        public static readonly String DB_NAME_LEVEL1PERCENT = "LEVEL1PERCENT";
        public static readonly String DB_NAME_FILTERING_EXPRESSION = "FILTERING_EXPRESSION";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_SUB_CROSS_ID = "OutputSubCrossId";
        public static readonly String PROPERTY_NAME_OUTPUT_COMMON_ID = "OutputCommonId";
        public static readonly String PROPERTY_NAME_OUTPUT_TYPE = "OutputType";
        public static readonly String PROPERTY_NAME_OUTPUT_TABLE_TYPE = "OutputTableType";
        public static readonly String PROPERTY_NAME_OUTPUT_TABLE_ORIENTATION = "OutputTableOrientation";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_TABLE_TYPE = "PageSettingTableType";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE = "PageSettingPaperSize";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PageSettingPaperOrientation";
        public static readonly String PROPERTY_NAME_MARKING_MIN_PARAMETER = "MarkingMinParameter";
        public static readonly String PROPERTY_NAME_MARKING_CODE = "MarkingCode";
        public static readonly String PROPERTY_NAME_MARKING_LEVEL = "MarkingLevel";
        public static readonly String PROPERTY_NAME_LEVEL2PLUSCOLOR = "Level2pluscolor";
        public static readonly String PROPERTY_NAME_LEVEL1PLUSCOLOR = "Level1pluscolor";
        public static readonly String PROPERTY_NAME_LEVEL1MINUSCOLOR = "Level1minuscolor";
        public static readonly String PROPERTY_NAME_LEVEL2MINUSCOLOR = "Level2minuscolor";
        public static readonly String PROPERTY_NAME_LEVEL2PERCENT = "Level2percent";
        public static readonly String PROPERTY_NAME_LEVEL1PERCENT = "Level1percent";
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

        static TOutputSubCrossDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_SUB_CROSS_ID.ToLower(), PROPERTY_NAME_OUTPUT_SUB_CROSS_ID);
                map.put(DB_NAME_OUTPUT_COMMON_ID.ToLower(), PROPERTY_NAME_OUTPUT_COMMON_ID);
                map.put(DB_NAME_OUTPUT_TYPE.ToLower(), PROPERTY_NAME_OUTPUT_TYPE);
                map.put(DB_NAME_OUTPUT_TABLE_TYPE.ToLower(), PROPERTY_NAME_OUTPUT_TABLE_TYPE);
                map.put(DB_NAME_OUTPUT_TABLE_ORIENTATION.ToLower(), PROPERTY_NAME_OUTPUT_TABLE_ORIENTATION);
                map.put(DB_NAME_PAGE_SETTING_TABLE_TYPE.ToLower(), PROPERTY_NAME_PAGE_SETTING_TABLE_TYPE);
                map.put(DB_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(DB_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION);
                map.put(DB_NAME_MARKING_MIN_PARAMETER.ToLower(), PROPERTY_NAME_MARKING_MIN_PARAMETER);
                map.put(DB_NAME_MARKING_CODE.ToLower(), PROPERTY_NAME_MARKING_CODE);
                map.put(DB_NAME_MARKING_LEVEL.ToLower(), PROPERTY_NAME_MARKING_LEVEL);
                map.put(DB_NAME_LEVEL2PLUSCOLOR.ToLower(), PROPERTY_NAME_LEVEL2PLUSCOLOR);
                map.put(DB_NAME_LEVEL1PLUSCOLOR.ToLower(), PROPERTY_NAME_LEVEL1PLUSCOLOR);
                map.put(DB_NAME_LEVEL1MINUSCOLOR.ToLower(), PROPERTY_NAME_LEVEL1MINUSCOLOR);
                map.put(DB_NAME_LEVEL2MINUSCOLOR.ToLower(), PROPERTY_NAME_LEVEL2MINUSCOLOR);
                map.put(DB_NAME_LEVEL2PERCENT.ToLower(), PROPERTY_NAME_LEVEL2PERCENT);
                map.put(DB_NAME_LEVEL1PERCENT.ToLower(), PROPERTY_NAME_LEVEL1PERCENT);
                map.put(DB_NAME_FILTERING_EXPRESSION.ToLower(), PROPERTY_NAME_FILTERING_EXPRESSION);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_SUB_CROSS_ID.ToLower(), DB_NAME_OUTPUT_SUB_CROSS_ID);
                map.put(PROPERTY_NAME_OUTPUT_COMMON_ID.ToLower(), DB_NAME_OUTPUT_COMMON_ID);
                map.put(PROPERTY_NAME_OUTPUT_TYPE.ToLower(), DB_NAME_OUTPUT_TYPE);
                map.put(PROPERTY_NAME_OUTPUT_TABLE_TYPE.ToLower(), DB_NAME_OUTPUT_TABLE_TYPE);
                map.put(PROPERTY_NAME_OUTPUT_TABLE_ORIENTATION.ToLower(), DB_NAME_OUTPUT_TABLE_ORIENTATION);
                map.put(PROPERTY_NAME_PAGE_SETTING_TABLE_TYPE.ToLower(), DB_NAME_PAGE_SETTING_TABLE_TYPE);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), DB_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), DB_NAME_PAGE_SETTING_PAPER_ORIENTATION);
                map.put(PROPERTY_NAME_MARKING_MIN_PARAMETER.ToLower(), DB_NAME_MARKING_MIN_PARAMETER);
                map.put(PROPERTY_NAME_MARKING_CODE.ToLower(), DB_NAME_MARKING_CODE);
                map.put(PROPERTY_NAME_MARKING_LEVEL.ToLower(), DB_NAME_MARKING_LEVEL);
                map.put(PROPERTY_NAME_LEVEL2PLUSCOLOR.ToLower(), DB_NAME_LEVEL2PLUSCOLOR);
                map.put(PROPERTY_NAME_LEVEL1PLUSCOLOR.ToLower(), DB_NAME_LEVEL1PLUSCOLOR);
                map.put(PROPERTY_NAME_LEVEL1MINUSCOLOR.ToLower(), DB_NAME_LEVEL1MINUSCOLOR);
                map.put(PROPERTY_NAME_LEVEL2MINUSCOLOR.ToLower(), DB_NAME_LEVEL2MINUSCOLOR);
                map.put(PROPERTY_NAME_LEVEL2PERCENT.ToLower(), DB_NAME_LEVEL2PERCENT);
                map.put(PROPERTY_NAME_LEVEL1PERCENT.ToLower(), DB_NAME_LEVEL1PERCENT);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputSubCross"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputSubCrossDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputSubCrossCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputSubCrossBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputSubCross NewMyEntity() { return new TOutputSubCross(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputSubCrossCB NewMyConditionBean() { return new TOutputSubCrossCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputSubCross>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputSubCross>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_SUB_CROSS_ID", "OutputSubCrossId", new EntityPropertyOutputSubCrossIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_COMMON_ID", "OutputCommonId", new EntityPropertyOutputCommonIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TYPE", "OutputType", new EntityPropertyOutputTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TABLE_TYPE", "OutputTableType", new EntityPropertyOutputTableTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TABLE_ORIENTATION", "OutputTableOrientation", new EntityPropertyOutputTableOrientationSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_TABLE_TYPE", "PageSettingTableType", new EntityPropertyPageSettingTableTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_SIZE", "PageSettingPaperSize", new EntityPropertyPageSettingPaperSizeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_ORIENTATION", "PageSettingPaperOrientation", new EntityPropertyPageSettingPaperOrientationSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MARKING_MIN_PARAMETER", "MarkingMinParameter", new EntityPropertyMarkingMinParameterSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MARKING_CODE", "MarkingCode", new EntityPropertyMarkingCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MARKING_LEVEL", "MarkingLevel", new EntityPropertyMarkingLevelSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LEVEL2PLUSCOLOR", "Level2pluscolor", new EntityPropertyLevel2pluscolorSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LEVEL1PLUSCOLOR", "Level1pluscolor", new EntityPropertyLevel1pluscolorSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LEVEL1MINUSCOLOR", "Level1minuscolor", new EntityPropertyLevel1minuscolorSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LEVEL2MINUSCOLOR", "Level2minuscolor", new EntityPropertyLevel2minuscolorSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LEVEL2PERCENT", "Level2percent", new EntityPropertyLevel2percentSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LEVEL1PERCENT", "Level1percent", new EntityPropertyLevel1percentSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FILTERING_EXPRESSION", "FilteringExpression", new EntityPropertyFilteringExpressionSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputSubCross> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputSubCross)entity, value);
        }

        public class EntityPropertyOutputSubCrossIdSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.OutputSubCrossId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputCommonIdSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.OutputCommonId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputTypeSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.OutputType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputTableTypeSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.OutputTableType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputTableOrientationSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.OutputTableOrientation = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingTableTypeSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.PageSettingTableType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperSizeSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.PageSettingPaperSize = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperOrientationSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.PageSettingPaperOrientation = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMarkingMinParameterSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.MarkingMinParameter = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyMarkingCodeSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.MarkingCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMarkingLevelSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.MarkingLevel = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLevel2pluscolorSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.Level2pluscolor = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLevel1pluscolorSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.Level1pluscolor = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLevel1minuscolorSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.Level1minuscolor = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLevel2minuscolorSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.Level2minuscolor = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLevel2percentSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.Level2percent = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLevel1percentSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.Level1percent = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFilteringExpressionSetupper : EntityPropertySetupper<TOutputSubCross> {
            public void Setup(TOutputSubCross entity, Object value) { entity.FilteringExpression = (value != null) ? (String)value : null; }
        }
    }
}
