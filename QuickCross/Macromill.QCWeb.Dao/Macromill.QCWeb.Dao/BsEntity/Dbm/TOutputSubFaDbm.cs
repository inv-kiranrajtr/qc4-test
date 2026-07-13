
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

    public class TOutputSubFaDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputSubFa);

        private static readonly TOutputSubFaDbm _instance = new TOutputSubFaDbm();
        private TOutputSubFaDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputSubFaDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_FA"; } }
        public override String TablePropertyName { get { return "TOutputSubFa"; } }
        public override String TableSqlName { get { return "T_OUTPUT_SUB_FA"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputSubFaId;
        protected ColumnInfo _columnOutputCommonId;
        protected ColumnInfo _columnPageSettingPaperSize;
        protected ColumnInfo _columnPageSettingPaperOrientation;
        protected ColumnInfo _columnFilteringExpression;

        public ColumnInfo ColumnOutputSubFaId { get { return _columnOutputSubFaId; } }
        public ColumnInfo ColumnOutputCommonId { get { return _columnOutputCommonId; } }
        public ColumnInfo ColumnPageSettingPaperSize { get { return _columnPageSettingPaperSize; } }
        public ColumnInfo ColumnPageSettingPaperOrientation { get { return _columnPageSettingPaperOrientation; } }
        public ColumnInfo ColumnFilteringExpression { get { return _columnFilteringExpression; } }

        protected void InitializeColumnInfo() {
            _columnOutputSubFaId = cci("OUTPUT_SUB_FA_ID", "OUTPUT_SUB_FA_ID", null, null, true, "OutputSubFaId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputCommonId = cci("OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", null, null, true, "OutputCommonId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputCommon", null);
            _columnPageSettingPaperSize = cci("PAGE_SETTING_PAPER_SIZE", "PAGE_SETTING_PAPER_SIZE", null, null, false, "PageSettingPaperSize", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperOrientation = cci("PAGE_SETTING_PAPER_ORIENTATION", "PAGE_SETTING_PAPER_ORIENTATION", null, null, false, "PageSettingPaperOrientation", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFilteringExpression = cci("FILTERING_EXPRESSION", "FILTERING_EXPRESSION", null, null, false, "FilteringExpression", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputSubFaId);
            _columnInfoList.add(ColumnOutputCommonId);
            _columnInfoList.add(ColumnPageSettingPaperSize);
            _columnInfoList.add(ColumnPageSettingPaperOrientation);
            _columnInfoList.add(ColumnFilteringExpression);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputSubFaId);
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
        public override String SequenceName { get { return "T_Output_Sub_FA_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Output_Sub_FA_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_SUB_FA";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputSubFa";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_SUB_FA_ID = "OUTPUT_SUB_FA_ID";
        public static readonly String DB_NAME_OUTPUT_COMMON_ID = "OUTPUT_COMMON_ID";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_SIZE = "PAGE_SETTING_PAPER_SIZE";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PAGE_SETTING_PAPER_ORIENTATION";
        public static readonly String DB_NAME_FILTERING_EXPRESSION = "FILTERING_EXPRESSION";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_SUB_FA_ID = "OutputSubFaId";
        public static readonly String PROPERTY_NAME_OUTPUT_COMMON_ID = "OutputCommonId";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE = "PageSettingPaperSize";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PageSettingPaperOrientation";
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

        static TOutputSubFaDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_SUB_FA_ID.ToLower(), PROPERTY_NAME_OUTPUT_SUB_FA_ID);
                map.put(DB_NAME_OUTPUT_COMMON_ID.ToLower(), PROPERTY_NAME_OUTPUT_COMMON_ID);
                map.put(DB_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(DB_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION);
                map.put(DB_NAME_FILTERING_EXPRESSION.ToLower(), PROPERTY_NAME_FILTERING_EXPRESSION);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_SUB_FA_ID.ToLower(), DB_NAME_OUTPUT_SUB_FA_ID);
                map.put(PROPERTY_NAME_OUTPUT_COMMON_ID.ToLower(), DB_NAME_OUTPUT_COMMON_ID);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), DB_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), DB_NAME_PAGE_SETTING_PAPER_ORIENTATION);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputSubFa"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputSubFaDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputSubFaCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputSubFaBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputSubFa NewMyEntity() { return new TOutputSubFa(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputSubFaCB NewMyConditionBean() { return new TOutputSubFaCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputSubFa>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputSubFa>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_SUB_FA_ID", "OutputSubFaId", new EntityPropertyOutputSubFaIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_COMMON_ID", "OutputCommonId", new EntityPropertyOutputCommonIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_SIZE", "PageSettingPaperSize", new EntityPropertyPageSettingPaperSizeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_ORIENTATION", "PageSettingPaperOrientation", new EntityPropertyPageSettingPaperOrientationSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FILTERING_EXPRESSION", "FilteringExpression", new EntityPropertyFilteringExpressionSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputSubFa> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputSubFa)entity, value);
        }

        public class EntityPropertyOutputSubFaIdSetupper : EntityPropertySetupper<TOutputSubFa> {
            public void Setup(TOutputSubFa entity, Object value) { entity.OutputSubFaId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputCommonIdSetupper : EntityPropertySetupper<TOutputSubFa> {
            public void Setup(TOutputSubFa entity, Object value) { entity.OutputCommonId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyPageSettingPaperSizeSetupper : EntityPropertySetupper<TOutputSubFa> {
            public void Setup(TOutputSubFa entity, Object value) { entity.PageSettingPaperSize = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperOrientationSetupper : EntityPropertySetupper<TOutputSubFa> {
            public void Setup(TOutputSubFa entity, Object value) { entity.PageSettingPaperOrientation = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFilteringExpressionSetupper : EntityPropertySetupper<TOutputSubFa> {
            public void Setup(TOutputSubFa entity, Object value) { entity.FilteringExpression = (value != null) ? (String)value : null; }
        }
    }
}
