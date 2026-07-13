
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

    public class TOutputSettingGtDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputSettingGt);

        private static readonly TOutputSettingGtDbm _instance = new TOutputSettingGtDbm();
        private TOutputSettingGtDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputSettingGtDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SETTING_GT"; } }
        public override String TablePropertyName { get { return "TOutputSettingGt"; } }
        public override String TableSqlName { get { return "T_OUTPUT_SETTING_GT"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnGtNpFlag;
        protected ColumnInfo _columnGtNFlag;
        protected ColumnInfo _columnGtPFlag;
        protected ColumnInfo _columnPageSettingNpFlag;
        protected ColumnInfo _columnPageSettingNFlag;
        protected ColumnInfo _columnPageSettingPFlag;
        protected ColumnInfo _columnPageSettingPaperSize;
        protected ColumnInfo _columnPageSettingPaperOrientation;
        protected ColumnInfo _columnOutputGraphFlag;

        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnGtNpFlag { get { return _columnGtNpFlag; } }
        public ColumnInfo ColumnGtNFlag { get { return _columnGtNFlag; } }
        public ColumnInfo ColumnGtPFlag { get { return _columnGtPFlag; } }
        public ColumnInfo ColumnPageSettingNpFlag { get { return _columnPageSettingNpFlag; } }
        public ColumnInfo ColumnPageSettingNFlag { get { return _columnPageSettingNFlag; } }
        public ColumnInfo ColumnPageSettingPFlag { get { return _columnPageSettingPFlag; } }
        public ColumnInfo ColumnPageSettingPaperSize { get { return _columnPageSettingPaperSize; } }
        public ColumnInfo ColumnPageSettingPaperOrientation { get { return _columnPageSettingPaperOrientation; } }
        public ColumnInfo ColumnOutputGraphFlag { get { return _columnOutputGraphFlag; } }

        protected void InitializeColumnInfo() {
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo,TQcwebSurveyInfoAsOne", "");
            _columnGtNpFlag = cci("GT_NP_FLAG", "GT_NP_FLAG", null, null, true, "GtNpFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGtNFlag = cci("GT_N_FLAG", "GT_N_FLAG", null, null, true, "GtNFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGtPFlag = cci("GT_P_FLAG", "GT_P_FLAG", null, null, true, "GtPFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingNpFlag = cci("PAGE_SETTING_NP_FLAG", "PAGE_SETTING_NP_FLAG", null, null, true, "PageSettingNpFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingNFlag = cci("PAGE_SETTING_N_FLAG", "PAGE_SETTING_N_FLAG", null, null, true, "PageSettingNFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPFlag = cci("PAGE_SETTING_P_FLAG", "PAGE_SETTING_P_FLAG", null, null, true, "PageSettingPFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperSize = cci("PAGE_SETTING_PAPER_SIZE", "PAGE_SETTING_PAPER_SIZE", null, null, false, "PageSettingPaperSize", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperOrientation = cci("PAGE_SETTING_PAPER_ORIENTATION", "PAGE_SETTING_PAPER_ORIENTATION", null, null, false, "PageSettingPaperOrientation", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputGraphFlag = cci("OUTPUT_GRAPH_FLAG", "OUTPUT_GRAPH_FLAG", null, null, true, "OutputGraphFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnGtNpFlag);
            _columnInfoList.add(ColumnGtNFlag);
            _columnInfoList.add(ColumnGtPFlag);
            _columnInfoList.add(ColumnPageSettingNpFlag);
            _columnInfoList.add(ColumnPageSettingNFlag);
            _columnInfoList.add(ColumnPageSettingPFlag);
            _columnInfoList.add(ColumnPageSettingPaperSize);
            _columnInfoList.add(ColumnPageSettingPaperOrientation);
            _columnInfoList.add(ColumnOutputGraphFlag);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnQcwebid);
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
        public ForeignInfo ForeignTQcwebSurveyInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfo", this, TQcwebSurveyInfoDbm.GetInstance(), map, 0, true, false);
        }}

        public ForeignInfo ForeignTQcwebSurveyInfoAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfoAsOne", this, TQcwebSurveyInfoDbm.GetInstance(), map, 1, true, false);
        }}

        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_SETTING_GT";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputSettingGt";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_GT_NP_FLAG = "GT_NP_FLAG";
        public static readonly String DB_NAME_GT_N_FLAG = "GT_N_FLAG";
        public static readonly String DB_NAME_GT_P_FLAG = "GT_P_FLAG";
        public static readonly String DB_NAME_PAGE_SETTING_NP_FLAG = "PAGE_SETTING_NP_FLAG";
        public static readonly String DB_NAME_PAGE_SETTING_N_FLAG = "PAGE_SETTING_N_FLAG";
        public static readonly String DB_NAME_PAGE_SETTING_P_FLAG = "PAGE_SETTING_P_FLAG";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_SIZE = "PAGE_SETTING_PAPER_SIZE";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PAGE_SETTING_PAPER_ORIENTATION";
        public static readonly String DB_NAME_OUTPUT_GRAPH_FLAG = "OUTPUT_GRAPH_FLAG";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_GT_NP_FLAG = "GtNpFlag";
        public static readonly String PROPERTY_NAME_GT_N_FLAG = "GtNFlag";
        public static readonly String PROPERTY_NAME_GT_P_FLAG = "GtPFlag";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_NP_FLAG = "PageSettingNpFlag";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_N_FLAG = "PageSettingNFlag";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_P_FLAG = "PageSettingPFlag";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE = "PageSettingPaperSize";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PageSettingPaperOrientation";
        public static readonly String PROPERTY_NAME_OUTPUT_GRAPH_FLAG = "OutputGraphFlag";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfoAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputSettingGtDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_GT_NP_FLAG.ToLower(), PROPERTY_NAME_GT_NP_FLAG);
                map.put(DB_NAME_GT_N_FLAG.ToLower(), PROPERTY_NAME_GT_N_FLAG);
                map.put(DB_NAME_GT_P_FLAG.ToLower(), PROPERTY_NAME_GT_P_FLAG);
                map.put(DB_NAME_PAGE_SETTING_NP_FLAG.ToLower(), PROPERTY_NAME_PAGE_SETTING_NP_FLAG);
                map.put(DB_NAME_PAGE_SETTING_N_FLAG.ToLower(), PROPERTY_NAME_PAGE_SETTING_N_FLAG);
                map.put(DB_NAME_PAGE_SETTING_P_FLAG.ToLower(), PROPERTY_NAME_PAGE_SETTING_P_FLAG);
                map.put(DB_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(DB_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION);
                map.put(DB_NAME_OUTPUT_GRAPH_FLAG.ToLower(), PROPERTY_NAME_OUTPUT_GRAPH_FLAG);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_GT_NP_FLAG.ToLower(), DB_NAME_GT_NP_FLAG);
                map.put(PROPERTY_NAME_GT_N_FLAG.ToLower(), DB_NAME_GT_N_FLAG);
                map.put(PROPERTY_NAME_GT_P_FLAG.ToLower(), DB_NAME_GT_P_FLAG);
                map.put(PROPERTY_NAME_PAGE_SETTING_NP_FLAG.ToLower(), DB_NAME_PAGE_SETTING_NP_FLAG);
                map.put(PROPERTY_NAME_PAGE_SETTING_N_FLAG.ToLower(), DB_NAME_PAGE_SETTING_N_FLAG);
                map.put(PROPERTY_NAME_PAGE_SETTING_P_FLAG.ToLower(), DB_NAME_PAGE_SETTING_P_FLAG);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), DB_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), DB_NAME_PAGE_SETTING_PAPER_ORIENTATION);
                map.put(PROPERTY_NAME_OUTPUT_GRAPH_FLAG.ToLower(), DB_NAME_OUTPUT_GRAPH_FLAG);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputSettingGt"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputSettingGtDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputSettingGtCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputSettingGtBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputSettingGt NewMyEntity() { return new TOutputSettingGt(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputSettingGtCB NewMyConditionBean() { return new TOutputSettingGtCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputSettingGt>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputSettingGt>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GT_NP_FLAG", "GtNpFlag", new EntityPropertyGtNpFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GT_N_FLAG", "GtNFlag", new EntityPropertyGtNFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GT_P_FLAG", "GtPFlag", new EntityPropertyGtPFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_NP_FLAG", "PageSettingNpFlag", new EntityPropertyPageSettingNpFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_N_FLAG", "PageSettingNFlag", new EntityPropertyPageSettingNFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_P_FLAG", "PageSettingPFlag", new EntityPropertyPageSettingPFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_SIZE", "PageSettingPaperSize", new EntityPropertyPageSettingPaperSizeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_ORIENTATION", "PageSettingPaperOrientation", new EntityPropertyPageSettingPaperOrientationSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_GRAPH_FLAG", "OutputGraphFlag", new EntityPropertyOutputGraphFlagSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputSettingGt> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputSettingGt)entity, value);
        }

        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputSettingGt> {
            public void Setup(TOutputSettingGt entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyGtNpFlagSetupper : EntityPropertySetupper<TOutputSettingGt> {
            public void Setup(TOutputSettingGt entity, Object value) { entity.GtNpFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyGtNFlagSetupper : EntityPropertySetupper<TOutputSettingGt> {
            public void Setup(TOutputSettingGt entity, Object value) { entity.GtNFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyGtPFlagSetupper : EntityPropertySetupper<TOutputSettingGt> {
            public void Setup(TOutputSettingGt entity, Object value) { entity.GtPFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingNpFlagSetupper : EntityPropertySetupper<TOutputSettingGt> {
            public void Setup(TOutputSettingGt entity, Object value) { entity.PageSettingNpFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingNFlagSetupper : EntityPropertySetupper<TOutputSettingGt> {
            public void Setup(TOutputSettingGt entity, Object value) { entity.PageSettingNFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPFlagSetupper : EntityPropertySetupper<TOutputSettingGt> {
            public void Setup(TOutputSettingGt entity, Object value) { entity.PageSettingPFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperSizeSetupper : EntityPropertySetupper<TOutputSettingGt> {
            public void Setup(TOutputSettingGt entity, Object value) { entity.PageSettingPaperSize = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperOrientationSetupper : EntityPropertySetupper<TOutputSettingGt> {
            public void Setup(TOutputSettingGt entity, Object value) { entity.PageSettingPaperOrientation = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputGraphFlagSetupper : EntityPropertySetupper<TOutputSettingGt> {
            public void Setup(TOutputSettingGt entity, Object value) { entity.OutputGraphFlag = (value != null) ? (int?)value : null; }
        }
    }
}
