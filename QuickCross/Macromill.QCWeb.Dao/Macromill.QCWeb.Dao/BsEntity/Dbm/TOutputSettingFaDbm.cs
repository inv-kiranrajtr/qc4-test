
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

    public class TOutputSettingFaDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputSettingFa);

        private static readonly TOutputSettingFaDbm _instance = new TOutputSettingFaDbm();
        private TOutputSettingFaDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputSettingFaDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SETTING_FA"; } }
        public override String TablePropertyName { get { return "TOutputSettingFa"; } }
        public override String TableSqlName { get { return "T_OUTPUT_SETTING_FA"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnPageSettingFlag;
        protected ColumnInfo _columnPageSettingPaperSize;
        protected ColumnInfo _columnPageSettingPaperOrientation;
        protected ColumnInfo _columnAscFlag;

        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnPageSettingFlag { get { return _columnPageSettingFlag; } }
        public ColumnInfo ColumnPageSettingPaperSize { get { return _columnPageSettingPaperSize; } }
        public ColumnInfo ColumnPageSettingPaperOrientation { get { return _columnPageSettingPaperOrientation; } }
        public ColumnInfo ColumnAscFlag { get { return _columnAscFlag; } }

        protected void InitializeColumnInfo() {
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo,TQcwebSurveyInfoAsOne", "");
            _columnPageSettingFlag = cci("PAGE_SETTING_FLAG", "PAGE_SETTING_FLAG", null, null, true, "PageSettingFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperSize = cci("PAGE_SETTING_PAPER_SIZE", "PAGE_SETTING_PAPER_SIZE", null, null, false, "PageSettingPaperSize", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPageSettingPaperOrientation = cci("PAGE_SETTING_PAPER_ORIENTATION", "PAGE_SETTING_PAPER_ORIENTATION", null, null, false, "PageSettingPaperOrientation", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAscFlag = cci("ASC_FLAG", "ASC_FLAG", null, null, true, "AscFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnPageSettingFlag);
            _columnInfoList.add(ColumnPageSettingPaperSize);
            _columnInfoList.add(ColumnPageSettingPaperOrientation);
            _columnInfoList.add(ColumnAscFlag);
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_SETTING_FA";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputSettingFa";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_PAGE_SETTING_FLAG = "PAGE_SETTING_FLAG";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_SIZE = "PAGE_SETTING_PAPER_SIZE";
        public static readonly String DB_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PAGE_SETTING_PAPER_ORIENTATION";
        public static readonly String DB_NAME_ASC_FLAG = "ASC_FLAG";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_FLAG = "PageSettingFlag";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE = "PageSettingPaperSize";
        public static readonly String PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION = "PageSettingPaperOrientation";
        public static readonly String PROPERTY_NAME_ASC_FLAG = "AscFlag";

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

        static TOutputSettingFaDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_PAGE_SETTING_FLAG.ToLower(), PROPERTY_NAME_PAGE_SETTING_FLAG);
                map.put(DB_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(DB_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION);
                map.put(DB_NAME_ASC_FLAG.ToLower(), PROPERTY_NAME_ASC_FLAG);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_PAGE_SETTING_FLAG.ToLower(), DB_NAME_PAGE_SETTING_FLAG);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_SIZE.ToLower(), DB_NAME_PAGE_SETTING_PAPER_SIZE);
                map.put(PROPERTY_NAME_PAGE_SETTING_PAPER_ORIENTATION.ToLower(), DB_NAME_PAGE_SETTING_PAPER_ORIENTATION);
                map.put(PROPERTY_NAME_ASC_FLAG.ToLower(), DB_NAME_ASC_FLAG);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputSettingFa"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputSettingFaDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputSettingFaCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputSettingFaBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputSettingFa NewMyEntity() { return new TOutputSettingFa(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputSettingFaCB NewMyConditionBean() { return new TOutputSettingFaCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputSettingFa>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputSettingFa>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_FLAG", "PageSettingFlag", new EntityPropertyPageSettingFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_SIZE", "PageSettingPaperSize", new EntityPropertyPageSettingPaperSizeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PAGE_SETTING_PAPER_ORIENTATION", "PageSettingPaperOrientation", new EntityPropertyPageSettingPaperOrientationSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ASC_FLAG", "AscFlag", new EntityPropertyAscFlagSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputSettingFa> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputSettingFa)entity, value);
        }

        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputSettingFa> {
            public void Setup(TOutputSettingFa entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyPageSettingFlagSetupper : EntityPropertySetupper<TOutputSettingFa> {
            public void Setup(TOutputSettingFa entity, Object value) { entity.PageSettingFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperSizeSetupper : EntityPropertySetupper<TOutputSettingFa> {
            public void Setup(TOutputSettingFa entity, Object value) { entity.PageSettingPaperSize = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPageSettingPaperOrientationSetupper : EntityPropertySetupper<TOutputSettingFa> {
            public void Setup(TOutputSettingFa entity, Object value) { entity.PageSettingPaperOrientation = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyAscFlagSetupper : EntityPropertySetupper<TOutputSettingFa> {
            public void Setup(TOutputSettingFa entity, Object value) { entity.AscFlag = (value != null) ? (int?)value : null; }
        }
    }
}
