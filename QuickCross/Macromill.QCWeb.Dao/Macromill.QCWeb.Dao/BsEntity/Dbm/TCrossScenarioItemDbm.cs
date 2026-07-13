
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

    public class TCrossScenarioItemDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TCrossScenarioItem);

        private static readonly TCrossScenarioItemDbm _instance = new TCrossScenarioItemDbm();
        private TCrossScenarioItemDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TCrossScenarioItemDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_CROSS_SCENARIO_ITEM"; } }
        public override String TablePropertyName { get { return "TCrossScenarioItem"; } }
        public override String TableSqlName { get { return "T_CROSS_SCENARIO_ITEM"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnCrossScenarioItemId;
        protected ColumnInfo _columnCrossScenarioTargetId;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnAxis1ItemId;
        protected ColumnInfo _columnAxis2ItemId;
        protected ColumnInfo _columnViewItemName;
        protected ColumnInfo _columnGraphType;
        protected ColumnInfo _columnReportType;
        protected ColumnInfo _columnTitleString;
        protected ColumnInfo _columnScenarioComment;

        public ColumnInfo ColumnCrossScenarioItemId { get { return _columnCrossScenarioItemId; } }
        public ColumnInfo ColumnCrossScenarioTargetId { get { return _columnCrossScenarioTargetId; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnAxis1ItemId { get { return _columnAxis1ItemId; } }
        public ColumnInfo ColumnAxis2ItemId { get { return _columnAxis2ItemId; } }
        public ColumnInfo ColumnViewItemName { get { return _columnViewItemName; } }
        public ColumnInfo ColumnGraphType { get { return _columnGraphType; } }
        public ColumnInfo ColumnReportType { get { return _columnReportType; } }
        public ColumnInfo ColumnTitleString { get { return _columnTitleString; } }
        public ColumnInfo ColumnScenarioComment { get { return _columnScenarioComment; } }

        protected void InitializeColumnInfo() {
            _columnCrossScenarioItemId = cci("CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", null, null, true, "CrossScenarioItemId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TPolylineCategoryList", "TPolylineCategoryListList");
            _columnCrossScenarioTargetId = cci("CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", null, null, true, "CrossScenarioTargetId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TCrossScenarioTarget", null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAxis1ItemId = cci("AXIS1_ITEM_ID", "AXIS1_ITEM_ID", null, null, false, "Axis1ItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAxis2ItemId = cci("AXIS2_ITEM_ID", "AXIS2_ITEM_ID", null, null, false, "Axis2ItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnViewItemName = cci("VIEW_ITEM_NAME", "VIEW_ITEM_NAME", null, null, false, "ViewItemName", typeof(String), false, "NVARCHAR2", 100, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphType = cci("GRAPH_TYPE", "GRAPH_TYPE", null, null, false, "GraphType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnReportType = cci("REPORT_TYPE", "REPORT_TYPE", null, null, true, "ReportType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleString = cci("TITLE_STRING", "TITLE_STRING", null, null, false, "TitleString", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenarioComment = cci("SCENARIO_COMMENT", "SCENARIO_COMMENT", null, null, false, "ScenarioComment", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnCrossScenarioItemId);
            _columnInfoList.add(ColumnCrossScenarioTargetId);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnAxis1ItemId);
            _columnInfoList.add(ColumnAxis2ItemId);
            _columnInfoList.add(ColumnViewItemName);
            _columnInfoList.add(ColumnGraphType);
            _columnInfoList.add(ColumnReportType);
            _columnInfoList.add(ColumnTitleString);
            _columnInfoList.add(ColumnScenarioComment);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnCrossScenarioItemId);
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
        public ForeignInfo ForeignTCrossScenarioTarget { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCrossScenarioTargetId, TCrossScenarioTargetDbm.GetInstance().ColumnCrossScenarioTargetId);
            return cfi("TCrossScenarioTarget", this, TCrossScenarioTargetDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTPolylineCategoryList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCrossScenarioItemId, TPolylineCategoryListDbm.GetInstance().ColumnCrossScenarioItemId);
            return cfi("TPolylineCategoryList", this, TPolylineCategoryListDbm.GetInstance(), map, 1, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTPolylineCategoryListList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCrossScenarioItemId, TPolylineCategoryListDbm.GetInstance().ColumnCrossScenarioItemId);
            return cri("TPolylineCategoryListList", this, TPolylineCategoryListDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Cross_Scenario_Item_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Cross_Scenario_Item_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_CROSS_SCENARIO_ITEM";
        public static readonly String TABLE_PROPERTY_NAME = "TCrossScenarioItem";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_CROSS_SCENARIO_ITEM_ID = "CROSS_SCENARIO_ITEM_ID";
        public static readonly String DB_NAME_CROSS_SCENARIO_TARGET_ID = "CROSS_SCENARIO_TARGET_ID";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_AXIS1_ITEM_ID = "AXIS1_ITEM_ID";
        public static readonly String DB_NAME_AXIS2_ITEM_ID = "AXIS2_ITEM_ID";
        public static readonly String DB_NAME_VIEW_ITEM_NAME = "VIEW_ITEM_NAME";
        public static readonly String DB_NAME_GRAPH_TYPE = "GRAPH_TYPE";
        public static readonly String DB_NAME_REPORT_TYPE = "REPORT_TYPE";
        public static readonly String DB_NAME_TITLE_STRING = "TITLE_STRING";
        public static readonly String DB_NAME_SCENARIO_COMMENT = "SCENARIO_COMMENT";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_CROSS_SCENARIO_ITEM_ID = "CrossScenarioItemId";
        public static readonly String PROPERTY_NAME_CROSS_SCENARIO_TARGET_ID = "CrossScenarioTargetId";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_AXIS1_ITEM_ID = "Axis1ItemId";
        public static readonly String PROPERTY_NAME_AXIS2_ITEM_ID = "Axis2ItemId";
        public static readonly String PROPERTY_NAME_VIEW_ITEM_NAME = "ViewItemName";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE = "GraphType";
        public static readonly String PROPERTY_NAME_REPORT_TYPE = "ReportType";
        public static readonly String PROPERTY_NAME_TITLE_STRING = "TitleString";
        public static readonly String PROPERTY_NAME_SCENARIO_COMMENT = "ScenarioComment";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TCrossScenarioTarget = "TCrossScenarioTarget";
        public static readonly String FOREIGN_PROPERTY_NAME_TPolylineCategoryList = "TPolylineCategoryList";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TPolylineCategoryListList = "TPolylineCategoryListList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TCrossScenarioItemDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_CROSS_SCENARIO_ITEM_ID.ToLower(), PROPERTY_NAME_CROSS_SCENARIO_ITEM_ID);
                map.put(DB_NAME_CROSS_SCENARIO_TARGET_ID.ToLower(), PROPERTY_NAME_CROSS_SCENARIO_TARGET_ID);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_AXIS1_ITEM_ID.ToLower(), PROPERTY_NAME_AXIS1_ITEM_ID);
                map.put(DB_NAME_AXIS2_ITEM_ID.ToLower(), PROPERTY_NAME_AXIS2_ITEM_ID);
                map.put(DB_NAME_VIEW_ITEM_NAME.ToLower(), PROPERTY_NAME_VIEW_ITEM_NAME);
                map.put(DB_NAME_GRAPH_TYPE.ToLower(), PROPERTY_NAME_GRAPH_TYPE);
                map.put(DB_NAME_REPORT_TYPE.ToLower(), PROPERTY_NAME_REPORT_TYPE);
                map.put(DB_NAME_TITLE_STRING.ToLower(), PROPERTY_NAME_TITLE_STRING);
                map.put(DB_NAME_SCENARIO_COMMENT.ToLower(), PROPERTY_NAME_SCENARIO_COMMENT);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_CROSS_SCENARIO_ITEM_ID.ToLower(), DB_NAME_CROSS_SCENARIO_ITEM_ID);
                map.put(PROPERTY_NAME_CROSS_SCENARIO_TARGET_ID.ToLower(), DB_NAME_CROSS_SCENARIO_TARGET_ID);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_AXIS1_ITEM_ID.ToLower(), DB_NAME_AXIS1_ITEM_ID);
                map.put(PROPERTY_NAME_AXIS2_ITEM_ID.ToLower(), DB_NAME_AXIS2_ITEM_ID);
                map.put(PROPERTY_NAME_VIEW_ITEM_NAME.ToLower(), DB_NAME_VIEW_ITEM_NAME);
                map.put(PROPERTY_NAME_GRAPH_TYPE.ToLower(), DB_NAME_GRAPH_TYPE);
                map.put(PROPERTY_NAME_REPORT_TYPE.ToLower(), DB_NAME_REPORT_TYPE);
                map.put(PROPERTY_NAME_TITLE_STRING.ToLower(), DB_NAME_TITLE_STRING);
                map.put(PROPERTY_NAME_SCENARIO_COMMENT.ToLower(), DB_NAME_SCENARIO_COMMENT);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TCrossScenarioItem"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TCrossScenarioItemDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TCrossScenarioItemCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TCrossScenarioItemBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TCrossScenarioItem NewMyEntity() { return new TCrossScenarioItem(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TCrossScenarioItemCB NewMyConditionBean() { return new TCrossScenarioItemCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TCrossScenarioItem>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TCrossScenarioItem>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("CROSS_SCENARIO_ITEM_ID", "CrossScenarioItemId", new EntityPropertyCrossScenarioItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CROSS_SCENARIO_TARGET_ID", "CrossScenarioTargetId", new EntityPropertyCrossScenarioTargetIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("AXIS1_ITEM_ID", "Axis1ItemId", new EntityPropertyAxis1ItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("AXIS2_ITEM_ID", "Axis2ItemId", new EntityPropertyAxis2ItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VIEW_ITEM_NAME", "ViewItemName", new EntityPropertyViewItemNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE", "GraphType", new EntityPropertyGraphTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REPORT_TYPE", "ReportType", new EntityPropertyReportTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_STRING", "TitleString", new EntityPropertyTitleStringSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_COMMENT", "ScenarioComment", new EntityPropertyScenarioCommentSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TCrossScenarioItem> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TCrossScenarioItem)entity, value);
        }

        public class EntityPropertyCrossScenarioItemIdSetupper : EntityPropertySetupper<TCrossScenarioItem> {
            public void Setup(TCrossScenarioItem entity, Object value) { entity.CrossScenarioItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyCrossScenarioTargetIdSetupper : EntityPropertySetupper<TCrossScenarioItem> {
            public void Setup(TCrossScenarioItem entity, Object value) { entity.CrossScenarioTargetId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TCrossScenarioItem> {
            public void Setup(TCrossScenarioItem entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyAxis1ItemIdSetupper : EntityPropertySetupper<TCrossScenarioItem> {
            public void Setup(TCrossScenarioItem entity, Object value) { entity.Axis1ItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAxis2ItemIdSetupper : EntityPropertySetupper<TCrossScenarioItem> {
            public void Setup(TCrossScenarioItem entity, Object value) { entity.Axis2ItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyViewItemNameSetupper : EntityPropertySetupper<TCrossScenarioItem> {
            public void Setup(TCrossScenarioItem entity, Object value) { entity.ViewItemName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGraphTypeSetupper : EntityPropertySetupper<TCrossScenarioItem> {
            public void Setup(TCrossScenarioItem entity, Object value) { entity.GraphType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyReportTypeSetupper : EntityPropertySetupper<TCrossScenarioItem> {
            public void Setup(TCrossScenarioItem entity, Object value) { entity.ReportType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTitleStringSetupper : EntityPropertySetupper<TCrossScenarioItem> {
            public void Setup(TCrossScenarioItem entity, Object value) { entity.TitleString = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyScenarioCommentSetupper : EntityPropertySetupper<TCrossScenarioItem> {
            public void Setup(TCrossScenarioItem entity, Object value) { entity.ScenarioComment = (value != null) ? (String)value : null; }
        }
    }
}
