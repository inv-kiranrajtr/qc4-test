
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

    public class TCrossScenarioTargetDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TCrossScenarioTarget);

        private static readonly TCrossScenarioTargetDbm _instance = new TCrossScenarioTargetDbm();
        private TCrossScenarioTargetDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TCrossScenarioTargetDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_CROSS_SCENARIO_TARGET"; } }
        public override String TablePropertyName { get { return "TCrossScenarioTarget"; } }
        public override String TableSqlName { get { return "T_CROSS_SCENARIO_TARGET"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnCrossScenarioTargetId;
        protected ColumnInfo _columnScenarioTotalizationId;
        protected ColumnInfo _columnScenariosetNo;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnScItemId;
        protected ColumnInfo _columnViewName;
        protected ColumnInfo _columnGraphType;
        protected ColumnInfo _columnReportType;
        protected ColumnInfo _columnViewItemString;
        protected ColumnInfo _columnScenarioComment;
        protected ColumnInfo _columnPolylineFlag;
        protected ColumnInfo _columnGraphTypeReport;

        public ColumnInfo ColumnCrossScenarioTargetId { get { return _columnCrossScenarioTargetId; } }
        public ColumnInfo ColumnScenarioTotalizationId { get { return _columnScenarioTotalizationId; } }
        public ColumnInfo ColumnScenariosetNo { get { return _columnScenariosetNo; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnScItemId { get { return _columnScItemId; } }
        public ColumnInfo ColumnViewName { get { return _columnViewName; } }
        public ColumnInfo ColumnGraphType { get { return _columnGraphType; } }
        public ColumnInfo ColumnReportType { get { return _columnReportType; } }
        public ColumnInfo ColumnViewItemString { get { return _columnViewItemString; } }
        public ColumnInfo ColumnScenarioComment { get { return _columnScenarioComment; } }
        public ColumnInfo ColumnPolylineFlag { get { return _columnPolylineFlag; } }
        public ColumnInfo ColumnGraphTypeReport { get { return _columnGraphTypeReport; } }

        protected void InitializeColumnInfo() {
            _columnCrossScenarioTargetId = cci("CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", null, null, true, "CrossScenarioTargetId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TColorSetInfoCrossList,TCrossScenarioItemList");
            _columnScenarioTotalizationId = cci("SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", null, null, true, "ScenarioTotalizationId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TScenarioTotalization", null);
            _columnScenariosetNo = cci("SCENARIOSET_NO", "SCENARIOSET_NO", null, null, true, "ScenariosetNo", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScItemId = cci("SC_ITEM_ID", "SC_ITEM_ID", null, null, true, "ScItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnViewName = cci("VIEW_NAME", "VIEW_NAME", null, null, true, "ViewName", typeof(String), false, "NVARCHAR2", 50, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphType = cci("GRAPH_TYPE", "GRAPH_TYPE", null, null, false, "GraphType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnReportType = cci("REPORT_TYPE", "REPORT_TYPE", null, null, true, "ReportType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnViewItemString = cci("VIEW_ITEM_STRING", "VIEW_ITEM_STRING", null, null, false, "ViewItemString", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenarioComment = cci("SCENARIO_COMMENT", "SCENARIO_COMMENT", null, null, false, "ScenarioComment", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPolylineFlag = cci("POLYLINE_FLAG", "POLYLINE_FLAG", null, null, true, "PolylineFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphTypeReport = cci("GRAPH_TYPE_REPORT", "GRAPH_TYPE_REPORT", null, null, false, "GraphTypeReport", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnCrossScenarioTargetId);
            _columnInfoList.add(ColumnScenarioTotalizationId);
            _columnInfoList.add(ColumnScenariosetNo);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnScItemId);
            _columnInfoList.add(ColumnViewName);
            _columnInfoList.add(ColumnGraphType);
            _columnInfoList.add(ColumnReportType);
            _columnInfoList.add(ColumnViewItemString);
            _columnInfoList.add(ColumnScenarioComment);
            _columnInfoList.add(ColumnPolylineFlag);
            _columnInfoList.add(ColumnGraphTypeReport);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnCrossScenarioTargetId);
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


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTColorSetInfoCrossList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCrossScenarioTargetId, TColorSetInfoCrossDbm.GetInstance().ColumnCrossScenarioTargetId);
            return cri("TColorSetInfoCrossList", this, TColorSetInfoCrossDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTCrossScenarioItemList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCrossScenarioTargetId, TCrossScenarioItemDbm.GetInstance().ColumnCrossScenarioTargetId);
            return cri("TCrossScenarioItemList", this, TCrossScenarioItemDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Cross_Scenario_Target_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Cross_Scenario_Target_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_CROSS_SCENARIO_TARGET";
        public static readonly String TABLE_PROPERTY_NAME = "TCrossScenarioTarget";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_CROSS_SCENARIO_TARGET_ID = "CROSS_SCENARIO_TARGET_ID";
        public static readonly String DB_NAME_SCENARIO_TOTALIZATION_ID = "SCENARIO_TOTALIZATION_ID";
        public static readonly String DB_NAME_SCENARIOSET_NO = "SCENARIOSET_NO";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_SC_ITEM_ID = "SC_ITEM_ID";
        public static readonly String DB_NAME_VIEW_NAME = "VIEW_NAME";
        public static readonly String DB_NAME_GRAPH_TYPE = "GRAPH_TYPE";
        public static readonly String DB_NAME_REPORT_TYPE = "REPORT_TYPE";
        public static readonly String DB_NAME_VIEW_ITEM_STRING = "VIEW_ITEM_STRING";
        public static readonly String DB_NAME_SCENARIO_COMMENT = "SCENARIO_COMMENT";
        public static readonly String DB_NAME_POLYLINE_FLAG = "POLYLINE_FLAG";
        public static readonly String DB_NAME_GRAPH_TYPE_REPORT = "GRAPH_TYPE_REPORT";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_CROSS_SCENARIO_TARGET_ID = "CrossScenarioTargetId";
        public static readonly String PROPERTY_NAME_SCENARIO_TOTALIZATION_ID = "ScenarioTotalizationId";
        public static readonly String PROPERTY_NAME_SCENARIOSET_NO = "ScenariosetNo";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_SC_ITEM_ID = "ScItemId";
        public static readonly String PROPERTY_NAME_VIEW_NAME = "ViewName";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE = "GraphType";
        public static readonly String PROPERTY_NAME_REPORT_TYPE = "ReportType";
        public static readonly String PROPERTY_NAME_VIEW_ITEM_STRING = "ViewItemString";
        public static readonly String PROPERTY_NAME_SCENARIO_COMMENT = "ScenarioComment";
        public static readonly String PROPERTY_NAME_POLYLINE_FLAG = "PolylineFlag";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE_REPORT = "GraphTypeReport";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TScenarioTotalization = "TScenarioTotalization";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TColorSetInfoCrossList = "TColorSetInfoCrossList";
        public static readonly String REFERRER_PROPERTY_NAME_TCrossScenarioItemList = "TCrossScenarioItemList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TCrossScenarioTargetDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_CROSS_SCENARIO_TARGET_ID.ToLower(), PROPERTY_NAME_CROSS_SCENARIO_TARGET_ID);
                map.put(DB_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), PROPERTY_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(DB_NAME_SCENARIOSET_NO.ToLower(), PROPERTY_NAME_SCENARIOSET_NO);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_SC_ITEM_ID.ToLower(), PROPERTY_NAME_SC_ITEM_ID);
                map.put(DB_NAME_VIEW_NAME.ToLower(), PROPERTY_NAME_VIEW_NAME);
                map.put(DB_NAME_GRAPH_TYPE.ToLower(), PROPERTY_NAME_GRAPH_TYPE);
                map.put(DB_NAME_REPORT_TYPE.ToLower(), PROPERTY_NAME_REPORT_TYPE);
                map.put(DB_NAME_VIEW_ITEM_STRING.ToLower(), PROPERTY_NAME_VIEW_ITEM_STRING);
                map.put(DB_NAME_SCENARIO_COMMENT.ToLower(), PROPERTY_NAME_SCENARIO_COMMENT);
                map.put(DB_NAME_POLYLINE_FLAG.ToLower(), PROPERTY_NAME_POLYLINE_FLAG);
                map.put(DB_NAME_GRAPH_TYPE_REPORT.ToLower(), PROPERTY_NAME_GRAPH_TYPE_REPORT);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_CROSS_SCENARIO_TARGET_ID.ToLower(), DB_NAME_CROSS_SCENARIO_TARGET_ID);
                map.put(PROPERTY_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), DB_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(PROPERTY_NAME_SCENARIOSET_NO.ToLower(), DB_NAME_SCENARIOSET_NO);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_SC_ITEM_ID.ToLower(), DB_NAME_SC_ITEM_ID);
                map.put(PROPERTY_NAME_VIEW_NAME.ToLower(), DB_NAME_VIEW_NAME);
                map.put(PROPERTY_NAME_GRAPH_TYPE.ToLower(), DB_NAME_GRAPH_TYPE);
                map.put(PROPERTY_NAME_REPORT_TYPE.ToLower(), DB_NAME_REPORT_TYPE);
                map.put(PROPERTY_NAME_VIEW_ITEM_STRING.ToLower(), DB_NAME_VIEW_ITEM_STRING);
                map.put(PROPERTY_NAME_SCENARIO_COMMENT.ToLower(), DB_NAME_SCENARIO_COMMENT);
                map.put(PROPERTY_NAME_POLYLINE_FLAG.ToLower(), DB_NAME_POLYLINE_FLAG);
                map.put(PROPERTY_NAME_GRAPH_TYPE_REPORT.ToLower(), DB_NAME_GRAPH_TYPE_REPORT);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TCrossScenarioTarget"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TCrossScenarioTargetDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TCrossScenarioTargetCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TCrossScenarioTargetBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TCrossScenarioTarget NewMyEntity() { return new TCrossScenarioTarget(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TCrossScenarioTargetCB NewMyConditionBean() { return new TCrossScenarioTargetCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TCrossScenarioTarget>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TCrossScenarioTarget>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("CROSS_SCENARIO_TARGET_ID", "CrossScenarioTargetId", new EntityPropertyCrossScenarioTargetIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_TOTALIZATION_ID", "ScenarioTotalizationId", new EntityPropertyScenarioTotalizationIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIOSET_NO", "ScenariosetNo", new EntityPropertyScenariosetNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SC_ITEM_ID", "ScItemId", new EntityPropertyScItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VIEW_NAME", "ViewName", new EntityPropertyViewNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE", "GraphType", new EntityPropertyGraphTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REPORT_TYPE", "ReportType", new EntityPropertyReportTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VIEW_ITEM_STRING", "ViewItemString", new EntityPropertyViewItemStringSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_COMMENT", "ScenarioComment", new EntityPropertyScenarioCommentSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("POLYLINE_FLAG", "PolylineFlag", new EntityPropertyPolylineFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE_REPORT", "GraphTypeReport", new EntityPropertyGraphTypeReportSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TCrossScenarioTarget> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TCrossScenarioTarget)entity, value);
        }

        public class EntityPropertyCrossScenarioTargetIdSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.CrossScenarioTargetId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenarioTotalizationIdSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.ScenarioTotalizationId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenariosetNoSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.ScenariosetNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyScItemIdSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.ScItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyViewNameSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.ViewName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGraphTypeSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.GraphType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyReportTypeSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.ReportType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyViewItemStringSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.ViewItemString = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyScenarioCommentSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.ScenarioComment = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyPolylineFlagSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.PolylineFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyGraphTypeReportSetupper : EntityPropertySetupper<TCrossScenarioTarget> {
            public void Setup(TCrossScenarioTarget entity, Object value) { entity.GraphTypeReport = (value != null) ? (String)value : null; }
        }
    }
}
