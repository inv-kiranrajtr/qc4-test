
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

    public class TReportChildDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TReportChild);

        private static readonly TReportChildDbm _instance = new TReportChildDbm();
        private TReportChildDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TReportChildDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_REPORT_CHILD"; } }
        public override String TablePropertyName { get { return "TReportChild"; } }
        public override String TableSqlName { get { return "T_REPORT_CHILD"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnReportChildId;
        protected ColumnInfo _columnParentReportId;
        protected ColumnInfo _columnTargetScenarioItemId;
        protected ColumnInfo _columnSortNo;

        public ColumnInfo ColumnReportChildId { get { return _columnReportChildId; } }
        public ColumnInfo ColumnParentReportId { get { return _columnParentReportId; } }
        public ColumnInfo ColumnTargetScenarioItemId { get { return _columnTargetScenarioItemId; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }

        protected void InitializeColumnInfo() {
            _columnReportChildId = cci("REPORT_CHILD_ID", "REPORT_CHILD_ID", null, null, true, "ReportChildId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnParentReportId = cci("PARENT_REPORT_ID", "PARENT_REPORT_ID", null, null, true, "ParentReportId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TReport", null);
            _columnTargetScenarioItemId = cci("TARGET_SCENARIO_ITEM_ID", "TARGET_SCENARIO_ITEM_ID", null, null, true, "TargetScenarioItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnReportChildId);
            _columnInfoList.add(ColumnParentReportId);
            _columnInfoList.add(ColumnTargetScenarioItemId);
            _columnInfoList.add(ColumnSortNo);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnReportChildId);
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
        public ForeignInfo ForeignTReport { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnParentReportId, TReportDbm.GetInstance().ColumnReportId);
            return cfi("TReport", this, TReportDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Report_Child_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Report_Child_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_REPORT_CHILD";
        public static readonly String TABLE_PROPERTY_NAME = "TReportChild";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_REPORT_CHILD_ID = "REPORT_CHILD_ID";
        public static readonly String DB_NAME_PARENT_REPORT_ID = "PARENT_REPORT_ID";
        public static readonly String DB_NAME_TARGET_SCENARIO_ITEM_ID = "TARGET_SCENARIO_ITEM_ID";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_REPORT_CHILD_ID = "ReportChildId";
        public static readonly String PROPERTY_NAME_PARENT_REPORT_ID = "ParentReportId";
        public static readonly String PROPERTY_NAME_TARGET_SCENARIO_ITEM_ID = "TargetScenarioItemId";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TReport = "TReport";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TReportChildDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_REPORT_CHILD_ID.ToLower(), PROPERTY_NAME_REPORT_CHILD_ID);
                map.put(DB_NAME_PARENT_REPORT_ID.ToLower(), PROPERTY_NAME_PARENT_REPORT_ID);
                map.put(DB_NAME_TARGET_SCENARIO_ITEM_ID.ToLower(), PROPERTY_NAME_TARGET_SCENARIO_ITEM_ID);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_REPORT_CHILD_ID.ToLower(), DB_NAME_REPORT_CHILD_ID);
                map.put(PROPERTY_NAME_PARENT_REPORT_ID.ToLower(), DB_NAME_PARENT_REPORT_ID);
                map.put(PROPERTY_NAME_TARGET_SCENARIO_ITEM_ID.ToLower(), DB_NAME_TARGET_SCENARIO_ITEM_ID);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TReportChild"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TReportChildDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TReportChildCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TReportChildBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TReportChild NewMyEntity() { return new TReportChild(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TReportChildCB NewMyConditionBean() { return new TReportChildCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TReportChild>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TReportChild>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("REPORT_CHILD_ID", "ReportChildId", new EntityPropertyReportChildIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PARENT_REPORT_ID", "ParentReportId", new EntityPropertyParentReportIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TARGET_SCENARIO_ITEM_ID", "TargetScenarioItemId", new EntityPropertyTargetScenarioItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TReportChild> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TReportChild)entity, value);
        }

        public class EntityPropertyReportChildIdSetupper : EntityPropertySetupper<TReportChild> {
            public void Setup(TReportChild entity, Object value) { entity.ReportChildId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyParentReportIdSetupper : EntityPropertySetupper<TReportChild> {
            public void Setup(TReportChild entity, Object value) { entity.ParentReportId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTargetScenarioItemIdSetupper : EntityPropertySetupper<TReportChild> {
            public void Setup(TReportChild entity, Object value) { entity.TargetScenarioItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TReportChild> {
            public void Setup(TReportChild entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
    }
}
