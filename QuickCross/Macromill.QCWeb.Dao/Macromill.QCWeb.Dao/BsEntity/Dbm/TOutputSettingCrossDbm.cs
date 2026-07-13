
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

    public class TOutputSettingCrossDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputSettingCross);

        private static readonly TOutputSettingCrossDbm _instance = new TOutputSettingCrossDbm();
        private TOutputSettingCrossDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputSettingCrossDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SETTING_CROSS"; } }
        public override String TablePropertyName { get { return "TOutputSettingCross"; } }
        public override String TableSqlName { get { return "T_OUTPUT_SETTING_CROSS"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnOutputType;
        protected ColumnInfo _columnCrossNpFlag;
        protected ColumnInfo _columnCrossNFlag;
        protected ColumnInfo _columnCrossPFlag;
        protected ColumnInfo _columnPageSettingNpFlag;
        protected ColumnInfo _columnPageSettingNFlag;
        protected ColumnInfo _columnPageSettingPFlag;
        protected ColumnInfo _columnPageSettingPaperSize;
        protected ColumnInfo _columnPageSettingPaperOrientation;

        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnOutputType { get { return _columnOutputType; } }
        public ColumnInfo ColumnCrossNpFlag { get { return _columnCrossNpFlag; } }
        public ColumnInfo ColumnCrossNFlag { get { return _columnCrossNFlag; } }
        public ColumnInfo ColumnCrossPFlag { get { return _columnCrossPFlag; } }
        public ColumnInfo ColumnPageSettingNpFlag { get { return _columnPageSettingNpFlag; } }
        public ColumnInfo ColumnPageSettingNFlag { get { return _columnPageSettingNFlag; } }
        public ColumnInfo ColumnPageSettingPFlag { get { return _columnPageSettingPFlag; } }
        public ColumnInfo ColumnPageSettingPaperSize { get { return _columnPageSettingPaperSize; } }
        public ColumnInfo ColumnPageSettingPaperOrientation { get { return _columnPageSettingPaperOrientation; } }

        protected void InitializeColumnInfo() {
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo,TQcwebSurveyInfoAsOne", "");
            _columnOutputType = cci("OUTPUT_TYPE", "OUTPUT_TYPE", null, null, true, "OutputType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCrossNpFlag = cci("CROSS_NP_FLAG", "CROSS_NP_FLAG", null, null, true, "CrossNpFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCrossNFlag = cci("CROSS_N_FLAG", "CROSS_N_FLAG", null, null, true, "CrossNFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCrossPFlag = cci("CROSS_P_FLAG", "CROSS_P_FLAG", null, null, true, "CrossPFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingNpFlag = cci("PAGE_SETTING_NP_FLAG", "PAGE_SETTING_NP_FLAG", null, null, true, "PageSettingNpFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingNFlag = cci("PAGE_SETTING_N_FLAG", "PAGE_SETTING_N_FLAG", null, null, true, "PageSettingNFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPFlag = cci("PAGE_SETTING_P_FLAG", "PAGE_SETTING_P_FLAG", null, null, true, "PageSettingPFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperSize = cci("PAGE_SETTING_PAPER_SIZE", "PAGE_SETTING_PAPER_SIZE", null, null, false, "PageSettingPaperSize", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperOrientation = cci("PAGE_SETTING_PAPER_ORIENTATION", "PAGE_SETTING_PAPER_ORIENTATION", null, null, false, "PageSettingPaperOrientation", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnOutputType);
            _columnInfoList.add(ColumnCrossNpFlag);
            _columnInfoList.add(ColumnCrossNFlag);
            _columnInfoList.add(ColumnCrossPFlag);
            _columnInfoList.add(ColumnPageSettingNpFlag);
            _columnInfoList.add(ColumnPageSettingNFlag);
            _columnInfoList.add(ColumnPageSettingPFlag);
            _columnInfoList.add(ColumnPageSettingPaperSize);
            _columnInfoList.add(ColumnPageSettingPaperOrientation);
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_SETTING_CROSS";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputSettingCross";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_OUTPUT_TYPE = "OUTPUT_TYPE";
        public static readonly String DB_NAME_CROSS_NP_FLAG = "CROSS_NP_FLAG";
        public static readonly String DB_NAME_CROSS_N_FLAG = "CROSS_N_FLAG";
        public static readonly String DB_NAME_CROSS_P_FLAG = "CROSS_P_FLAG";
        public static readonly String DB_NAME_PAGE_SETTING_NP_FLAG = "PAGE_SETTING_NP_FLAG";
        public static readonly String DB_NAME_PAGE_SETTING_N_FLAG = "PAGE_SETTING_N_FLAG";
        public static readonly String DB_NAME_PAGE_SETTING_P_FLAG = "PAGE_SETTING_P_FLAG";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_SIZE = "PAGE_SETTING_PAPER_SIZE";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PAGE_SETTING_PAPER_ORIENTATION";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_OUTPUT_TYPE = "OutputType";
        public static readonly String PROPERTY_NAME_CROSS_NP_FLAG = "CrossNpFlag";
        public static readonly String PROPERTY_NAME_CROSS_N_FLAG = "CrossNFlag";
        public static readonly String PROPERTY_NAME_CROSS_P_FLAG = "CrossPFlag";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_NP_FLAG = "PageSettingNpFlag";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_N_FLAG = "PageSettingNFlag";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_P_FLAG = "PageSettingPFlag";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE = "PageSettingPaperSize";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PageSettingPaperOrientation";

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

        static TOutputSettingCrossDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_OUTPUT_TYPE.ToLower(), PROPERTY_NAME_OUTPUT_TYPE);
                map.put(DB_NAME_CROSS_NP_FLAG.ToLower(), PROPERTY_NAME_CROSS_NP_FLAG);
                map.put(DB_NAME_CROSS_N_FLAG.ToLower(), PROPERTY_NAME_CROSS_N_FLAG);
                map.put(DB_NAME_CROSS_P_FLAG.ToLower(), PROPERTY_NAME_CROSS_P_FLAG);
                map.put(DB_NAME_PAGE_SETTING_NP_FLAG.ToLower(), PROPERTY_NAME_PAGE_SETTING_NP_FLAG);
                map.put(DB_NAME_PAGE_SETTING_N_FLAG.ToLower(), PROPERTY_NAME_PAGE_SETTING_N_FLAG);
                map.put(DB_NAME_PAGE_SETTING_P_FLAG.ToLower(), PROPERTY_NAME_PAGE_SETTING_P_FLAG);
                map.put(DB_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(DB_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_OUTPUT_TYPE.ToLower(), DB_NAME_OUTPUT_TYPE);
                map.put(PROPERTY_NAME_CROSS_NP_FLAG.ToLower(), DB_NAME_CROSS_NP_FLAG);
                map.put(PROPERTY_NAME_CROSS_N_FLAG.ToLower(), DB_NAME_CROSS_N_FLAG);
                map.put(PROPERTY_NAME_CROSS_P_FLAG.ToLower(), DB_NAME_CROSS_P_FLAG);
                map.put(PROPERTY_NAME_PAGE_SETTING_NP_FLAG.ToLower(), DB_NAME_PAGE_SETTING_NP_FLAG);
                map.put(PROPERTY_NAME_PAGE_SETTING_N_FLAG.ToLower(), DB_NAME_PAGE_SETTING_N_FLAG);
                map.put(PROPERTY_NAME_PAGE_SETTING_P_FLAG.ToLower(), DB_NAME_PAGE_SETTING_P_FLAG);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), DB_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), DB_NAME_PAGE_SETTING_PAPER_ORIENTATION);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputSettingCross"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputSettingCrossDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputSettingCrossCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputSettingCrossBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputSettingCross NewMyEntity() { return new TOutputSettingCross(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputSettingCrossCB NewMyConditionBean() { return new TOutputSettingCrossCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputSettingCross>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputSettingCross>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TYPE", "OutputType", new EntityPropertyOutputTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CROSS_NP_FLAG", "CrossNpFlag", new EntityPropertyCrossNpFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CROSS_N_FLAG", "CrossNFlag", new EntityPropertyCrossNFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CROSS_P_FLAG", "CrossPFlag", new EntityPropertyCrossPFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_NP_FLAG", "PageSettingNpFlag", new EntityPropertyPageSettingNpFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_N_FLAG", "PageSettingNFlag", new EntityPropertyPageSettingNFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_P_FLAG", "PageSettingPFlag", new EntityPropertyPageSettingPFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_SIZE", "PageSettingPaperSize", new EntityPropertyPageSettingPaperSizeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_ORIENTATION", "PageSettingPaperOrientation", new EntityPropertyPageSettingPaperOrientationSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputSettingCross> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputSettingCross)entity, value);
        }

        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputSettingCross> {
            public void Setup(TOutputSettingCross entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputTypeSetupper : EntityPropertySetupper<TOutputSettingCross> {
            public void Setup(TOutputSettingCross entity, Object value) { entity.OutputType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyCrossNpFlagSetupper : EntityPropertySetupper<TOutputSettingCross> {
            public void Setup(TOutputSettingCross entity, Object value) { entity.CrossNpFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyCrossNFlagSetupper : EntityPropertySetupper<TOutputSettingCross> {
            public void Setup(TOutputSettingCross entity, Object value) { entity.CrossNFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyCrossPFlagSetupper : EntityPropertySetupper<TOutputSettingCross> {
            public void Setup(TOutputSettingCross entity, Object value) { entity.CrossPFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingNpFlagSetupper : EntityPropertySetupper<TOutputSettingCross> {
            public void Setup(TOutputSettingCross entity, Object value) { entity.PageSettingNpFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingNFlagSetupper : EntityPropertySetupper<TOutputSettingCross> {
            public void Setup(TOutputSettingCross entity, Object value) { entity.PageSettingNFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPFlagSetupper : EntityPropertySetupper<TOutputSettingCross> {
            public void Setup(TOutputSettingCross entity, Object value) { entity.PageSettingPFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperSizeSetupper : EntityPropertySetupper<TOutputSettingCross> {
            public void Setup(TOutputSettingCross entity, Object value) { entity.PageSettingPaperSize = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperOrientationSetupper : EntityPropertySetupper<TOutputSettingCross> {
            public void Setup(TOutputSettingCross entity, Object value) { entity.PageSettingPaperOrientation = (value != null) ? (int?)value : null; }
        }
    }
}
