
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

    public class TReportDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TReport);

        private static readonly TReportDbm _instance = new TReportDbm();
        private TReportDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TReportDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_REPORT"; } }
        public override String TablePropertyName { get { return "TReport"; } }
        public override String TableSqlName { get { return "T_REPORT"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnReportId;
        protected ColumnInfo _columnReportsetId;
        protected ColumnInfo _columnTargetScenarioItemId;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnChildDiv;
        protected ColumnInfo _columnScenarioType;

        public ColumnInfo ColumnReportId { get { return _columnReportId; } }
        public ColumnInfo ColumnReportsetId { get { return _columnReportsetId; } }
        public ColumnInfo ColumnTargetScenarioItemId { get { return _columnTargetScenarioItemId; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnChildDiv { get { return _columnChildDiv; } }
        public ColumnInfo ColumnScenarioType { get { return _columnScenarioType; } }

        protected void InitializeColumnInfo() {
            _columnReportId = cci("REPORT_ID", "REPORT_ID", null, null, true, "ReportId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TReportChild", "TReportChildList");
            _columnReportsetId = cci("REPORTSET_ID", "REPORTSET_ID", null, null, true, "ReportsetId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TReportset", null);
            _columnTargetScenarioItemId = cci("TARGET_SCENARIO_ITEM_ID", "TARGET_SCENARIO_ITEM_ID", null, null, true, "TargetScenarioItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnChildDiv = cci("CHILD_DIV", "CHILD_DIV", null, null, true, "ChildDiv", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenarioType = cci("SCENARIO_TYPE", "SCENARIO_TYPE", null, null, true, "ScenarioType", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnReportId);
            _columnInfoList.add(ColumnReportsetId);
            _columnInfoList.add(ColumnTargetScenarioItemId);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnChildDiv);
            _columnInfoList.add(ColumnScenarioType);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnReportId);
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
        public ForeignInfo ForeignTReportset { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnReportsetId, TReportsetDbm.GetInstance().ColumnReportsetId);
            return cfi("TReportset", this, TReportsetDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTReportChild { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnReportId, TReportChildDbm.GetInstance().ColumnParentReportId);
            return cfi("TReportChild", this, TReportChildDbm.GetInstance(), map, 1, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTReportChildList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnReportId, TReportChildDbm.GetInstance().ColumnParentReportId);
            return cri("TReportChildList", this, TReportChildDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Report_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Report_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_REPORT";
        public static readonly String TABLE_PROPERTY_NAME = "TReport";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_REPORT_ID = "REPORT_ID";
        public static readonly String DB_NAME_REPORTSET_ID = "REPORTSET_ID";
        public static readonly String DB_NAME_TARGET_SCENARIO_ITEM_ID = "TARGET_SCENARIO_ITEM_ID";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_CHILD_DIV = "CHILD_DIV";
        public static readonly String DB_NAME_SCENARIO_TYPE = "SCENARIO_TYPE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_REPORT_ID = "ReportId";
        public static readonly String PROPERTY_NAME_REPORTSET_ID = "ReportsetId";
        public static readonly String PROPERTY_NAME_TARGET_SCENARIO_ITEM_ID = "TargetScenarioItemId";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_CHILD_DIV = "ChildDiv";
        public static readonly String PROPERTY_NAME_SCENARIO_TYPE = "ScenarioType";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TReportset = "TReportset";
        public static readonly String FOREIGN_PROPERTY_NAME_TReportChild = "TReportChild";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TReportChildList = "TReportChildList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TReportDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_REPORT_ID.ToLower(), PROPERTY_NAME_REPORT_ID);
                map.put(DB_NAME_REPORTSET_ID.ToLower(), PROPERTY_NAME_REPORTSET_ID);
                map.put(DB_NAME_TARGET_SCENARIO_ITEM_ID.ToLower(), PROPERTY_NAME_TARGET_SCENARIO_ITEM_ID);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_CHILD_DIV.ToLower(), PROPERTY_NAME_CHILD_DIV);
                map.put(DB_NAME_SCENARIO_TYPE.ToLower(), PROPERTY_NAME_SCENARIO_TYPE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_REPORT_ID.ToLower(), DB_NAME_REPORT_ID);
                map.put(PROPERTY_NAME_REPORTSET_ID.ToLower(), DB_NAME_REPORTSET_ID);
                map.put(PROPERTY_NAME_TARGET_SCENARIO_ITEM_ID.ToLower(), DB_NAME_TARGET_SCENARIO_ITEM_ID);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_CHILD_DIV.ToLower(), DB_NAME_CHILD_DIV);
                map.put(PROPERTY_NAME_SCENARIO_TYPE.ToLower(), DB_NAME_SCENARIO_TYPE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TReport"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TReportDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TReportCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TReportBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TReport NewMyEntity() { return new TReport(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TReportCB NewMyConditionBean() { return new TReportCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TReport>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TReport>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("REPORT_ID", "ReportId", new EntityPropertyReportIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REPORTSET_ID", "ReportsetId", new EntityPropertyReportsetIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TARGET_SCENARIO_ITEM_ID", "TargetScenarioItemId", new EntityPropertyTargetScenarioItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CHILD_DIV", "ChildDiv", new EntityPropertyChildDivSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_TYPE", "ScenarioType", new EntityPropertyScenarioTypeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TReport> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TReport)entity, value);
        }

        public class EntityPropertyReportIdSetupper : EntityPropertySetupper<TReport> {
            public void Setup(TReport entity, Object value) { entity.ReportId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyReportsetIdSetupper : EntityPropertySetupper<TReport> {
            public void Setup(TReport entity, Object value) { entity.ReportsetId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTargetScenarioItemIdSetupper : EntityPropertySetupper<TReport> {
            public void Setup(TReport entity, Object value) { entity.TargetScenarioItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TReport> {
            public void Setup(TReport entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyChildDivSetupper : EntityPropertySetupper<TReport> {
            public void Setup(TReport entity, Object value) { entity.ChildDiv = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyScenarioTypeSetupper : EntityPropertySetupper<TReport> {
            public void Setup(TReport entity, Object value) { entity.ScenarioType = (value != null) ? (String)value : null; }
        }
    }
}
