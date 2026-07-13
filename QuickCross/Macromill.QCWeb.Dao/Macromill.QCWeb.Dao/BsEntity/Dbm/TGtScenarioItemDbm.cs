
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

    public class TGtScenarioItemDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TGtScenarioItem);

        private static readonly TGtScenarioItemDbm _instance = new TGtScenarioItemDbm();
        private TGtScenarioItemDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TGtScenarioItemDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_GT_SCENARIO_ITEM"; } }
        public override String TablePropertyName { get { return "TGtScenarioItem"; } }
        public override String TableSqlName { get { return "T_GT_SCENARIO_ITEM"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnGtScenarioItemId;
        protected ColumnInfo _columnScenarioTotalizationId;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnItemInfoId;
        protected ColumnInfo _columnScenarioName;
        protected ColumnInfo _columnGraphType;
        protected ColumnInfo _columnReportType;
        protected ColumnInfo _columnViewItemString;
        protected ColumnInfo _columnScenarioComment;
        protected ColumnInfo _columnSurveyType;
        protected ColumnInfo _columnGraphTypeReport;
        protected ColumnInfo _columnTestTargetType;

        public ColumnInfo ColumnGtScenarioItemId { get { return _columnGtScenarioItemId; } }
        public ColumnInfo ColumnScenarioTotalizationId { get { return _columnScenarioTotalizationId; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnItemInfoId { get { return _columnItemInfoId; } }
        public ColumnInfo ColumnScenarioName { get { return _columnScenarioName; } }
        public ColumnInfo ColumnGraphType { get { return _columnGraphType; } }
        public ColumnInfo ColumnReportType { get { return _columnReportType; } }
        public ColumnInfo ColumnViewItemString { get { return _columnViewItemString; } }
        public ColumnInfo ColumnScenarioComment { get { return _columnScenarioComment; } }
        public ColumnInfo ColumnSurveyType { get { return _columnSurveyType; } }
        public ColumnInfo ColumnGraphTypeReport { get { return _columnGraphTypeReport; } }
        public ColumnInfo ColumnTestTargetType { get { return _columnTestTargetType; } }

        protected void InitializeColumnInfo() {
            _columnGtScenarioItemId = cci("GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", null, null, true, "GtScenarioItemId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TColorSetInfoGtList");
            _columnScenarioTotalizationId = cci("SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", null, null, true, "ScenarioTotalizationId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TScenarioTotalization", null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemInfoId = cci("ITEM_INFO_ID", "ITEM_INFO_ID", null, null, true, "ItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TItemInfo", null);
            _columnScenarioName = cci("SCENARIO_NAME", "SCENARIO_NAME", null, null, true, "ScenarioName", typeof(String), false, "NVARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphType = cci("GRAPH_TYPE", "GRAPH_TYPE", null, null, false, "GraphType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnReportType = cci("REPORT_TYPE", "REPORT_TYPE", null, null, true, "ReportType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnViewItemString = cci("VIEW_ITEM_STRING", "VIEW_ITEM_STRING", null, null, false, "ViewItemString", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenarioComment = cci("SCENARIO_COMMENT", "SCENARIO_COMMENT", null, null, false, "ScenarioComment", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSurveyType = cci("SURVEY_TYPE", "SURVEY_TYPE", null, null, false, "SurveyType", typeof(int?), false, "NUMBER", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphTypeReport = cci("GRAPH_TYPE_REPORT", "GRAPH_TYPE_REPORT", null, null, false, "GraphTypeReport", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTestTargetType = cci("TEST_TARGET_TYPE", "TEST_TARGET_TYPE", null, null, false, "TestTargetType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnGtScenarioItemId);
            _columnInfoList.add(ColumnScenarioTotalizationId);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnItemInfoId);
            _columnInfoList.add(ColumnScenarioName);
            _columnInfoList.add(ColumnGraphType);
            _columnInfoList.add(ColumnReportType);
            _columnInfoList.add(ColumnViewItemString);
            _columnInfoList.add(ColumnScenarioComment);
            _columnInfoList.add(ColumnSurveyType);
            _columnInfoList.add(ColumnGraphTypeReport);
            _columnInfoList.add(ColumnTestTargetType);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnGtScenarioItemId);
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
        public ForeignInfo ForeignTItemInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TItemInfoDbm.GetInstance().ColumnItemInfoId);
            return cfi("TItemInfo", this, TItemInfoDbm.GetInstance(), map, 1, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTColorSetInfoGtList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnGtScenarioItemId, TColorSetInfoGtDbm.GetInstance().ColumnGtScenarioItemId);
            return cri("TColorSetInfoGtList", this, TColorSetInfoGtDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_GT_Scenario_Item_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_GT_Scenario_Item_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_GT_SCENARIO_ITEM";
        public static readonly String TABLE_PROPERTY_NAME = "TGtScenarioItem";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_GT_SCENARIO_ITEM_ID = "GT_SCENARIO_ITEM_ID";
        public static readonly String DB_NAME_SCENARIO_TOTALIZATION_ID = "SCENARIO_TOTALIZATION_ID";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_ITEM_INFO_ID = "ITEM_INFO_ID";
        public static readonly String DB_NAME_SCENARIO_NAME = "SCENARIO_NAME";
        public static readonly String DB_NAME_GRAPH_TYPE = "GRAPH_TYPE";
        public static readonly String DB_NAME_REPORT_TYPE = "REPORT_TYPE";
        public static readonly String DB_NAME_VIEW_ITEM_STRING = "VIEW_ITEM_STRING";
        public static readonly String DB_NAME_SCENARIO_COMMENT = "SCENARIO_COMMENT";
        public static readonly String DB_NAME_SURVEY_TYPE = "SURVEY_TYPE";
        public static readonly String DB_NAME_GRAPH_TYPE_REPORT = "GRAPH_TYPE_REPORT";
        public static readonly String DB_NAME_TEST_TARGET_TYPE = "TEST_TARGET_TYPE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_GT_SCENARIO_ITEM_ID = "GtScenarioItemId";
        public static readonly String PROPERTY_NAME_SCENARIO_TOTALIZATION_ID = "ScenarioTotalizationId";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_ITEM_INFO_ID = "ItemInfoId";
        public static readonly String PROPERTY_NAME_SCENARIO_NAME = "ScenarioName";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE = "GraphType";
        public static readonly String PROPERTY_NAME_REPORT_TYPE = "ReportType";
        public static readonly String PROPERTY_NAME_VIEW_ITEM_STRING = "ViewItemString";
        public static readonly String PROPERTY_NAME_SCENARIO_COMMENT = "ScenarioComment";
        public static readonly String PROPERTY_NAME_SURVEY_TYPE = "SurveyType";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE_REPORT = "GraphTypeReport";
        public static readonly String PROPERTY_NAME_TEST_TARGET_TYPE = "TestTargetType";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TScenarioTotalization = "TScenarioTotalization";
        public static readonly String FOREIGN_PROPERTY_NAME_TItemInfo = "TItemInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TColorSetInfoGtList = "TColorSetInfoGtList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TGtScenarioItemDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_GT_SCENARIO_ITEM_ID.ToLower(), PROPERTY_NAME_GT_SCENARIO_ITEM_ID);
                map.put(DB_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), PROPERTY_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_ITEM_INFO_ID.ToLower(), PROPERTY_NAME_ITEM_INFO_ID);
                map.put(DB_NAME_SCENARIO_NAME.ToLower(), PROPERTY_NAME_SCENARIO_NAME);
                map.put(DB_NAME_GRAPH_TYPE.ToLower(), PROPERTY_NAME_GRAPH_TYPE);
                map.put(DB_NAME_REPORT_TYPE.ToLower(), PROPERTY_NAME_REPORT_TYPE);
                map.put(DB_NAME_VIEW_ITEM_STRING.ToLower(), PROPERTY_NAME_VIEW_ITEM_STRING);
                map.put(DB_NAME_SCENARIO_COMMENT.ToLower(), PROPERTY_NAME_SCENARIO_COMMENT);
                map.put(DB_NAME_SURVEY_TYPE.ToLower(), PROPERTY_NAME_SURVEY_TYPE);
                map.put(DB_NAME_GRAPH_TYPE_REPORT.ToLower(), PROPERTY_NAME_GRAPH_TYPE_REPORT);
                map.put(DB_NAME_TEST_TARGET_TYPE.ToLower(), PROPERTY_NAME_TEST_TARGET_TYPE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_GT_SCENARIO_ITEM_ID.ToLower(), DB_NAME_GT_SCENARIO_ITEM_ID);
                map.put(PROPERTY_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), DB_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_ITEM_INFO_ID.ToLower(), DB_NAME_ITEM_INFO_ID);
                map.put(PROPERTY_NAME_SCENARIO_NAME.ToLower(), DB_NAME_SCENARIO_NAME);
                map.put(PROPERTY_NAME_GRAPH_TYPE.ToLower(), DB_NAME_GRAPH_TYPE);
                map.put(PROPERTY_NAME_REPORT_TYPE.ToLower(), DB_NAME_REPORT_TYPE);
                map.put(PROPERTY_NAME_VIEW_ITEM_STRING.ToLower(), DB_NAME_VIEW_ITEM_STRING);
                map.put(PROPERTY_NAME_SCENARIO_COMMENT.ToLower(), DB_NAME_SCENARIO_COMMENT);
                map.put(PROPERTY_NAME_SURVEY_TYPE.ToLower(), DB_NAME_SURVEY_TYPE);
                map.put(PROPERTY_NAME_GRAPH_TYPE_REPORT.ToLower(), DB_NAME_GRAPH_TYPE_REPORT);
                map.put(PROPERTY_NAME_TEST_TARGET_TYPE.ToLower(), DB_NAME_TEST_TARGET_TYPE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TGtScenarioItem"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TGtScenarioItemDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TGtScenarioItemCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TGtScenarioItemBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TGtScenarioItem NewMyEntity() { return new TGtScenarioItem(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TGtScenarioItemCB NewMyConditionBean() { return new TGtScenarioItemCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TGtScenarioItem>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TGtScenarioItem>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("GT_SCENARIO_ITEM_ID", "GtScenarioItemId", new EntityPropertyGtScenarioItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_TOTALIZATION_ID", "ScenarioTotalizationId", new EntityPropertyScenarioTotalizationIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_INFO_ID", "ItemInfoId", new EntityPropertyItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_NAME", "ScenarioName", new EntityPropertyScenarioNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE", "GraphType", new EntityPropertyGraphTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REPORT_TYPE", "ReportType", new EntityPropertyReportTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VIEW_ITEM_STRING", "ViewItemString", new EntityPropertyViewItemStringSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_COMMENT", "ScenarioComment", new EntityPropertyScenarioCommentSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SURVEY_TYPE", "SurveyType", new EntityPropertySurveyTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE_REPORT", "GraphTypeReport", new EntityPropertyGraphTypeReportSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_TARGET_TYPE", "TestTargetType", new EntityPropertyTestTargetTypeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TGtScenarioItem> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TGtScenarioItem)entity, value);
        }

        public class EntityPropertyGtScenarioItemIdSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.GtScenarioItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenarioTotalizationIdSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.ScenarioTotalizationId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyItemInfoIdSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.ItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenarioNameSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.ScenarioName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGraphTypeSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.GraphType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyReportTypeSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.ReportType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyViewItemStringSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.ViewItemString = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyScenarioCommentSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.ScenarioComment = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySurveyTypeSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.SurveyType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyGraphTypeReportSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.GraphTypeReport = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyTestTargetTypeSetupper : EntityPropertySetupper<TGtScenarioItem> {
            public void Setup(TGtScenarioItem entity, Object value) { entity.TestTargetType = (value != null) ? (int?)value : null; }
        }
    }
}
