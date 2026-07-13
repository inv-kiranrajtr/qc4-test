
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class ScenarioNodesByQCWebIDEntityDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(ScenarioNodesByQCWebIDEntity);

        private static readonly ScenarioNodesByQCWebIDEntityDbm _instance = new ScenarioNodesByQCWebIDEntityDbm();
        private ScenarioNodesByQCWebIDEntityDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static ScenarioNodesByQCWebIDEntityDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "ScenarioNodesByQCWebIDEntity"; } }
        public override String TablePropertyName { get { return "ScenarioNodesByQCWebIDEntity"; } }
        public override String TableSqlName { get { return "ScenarioNodesByQCWebIDEntity"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnScenarioTotalizationId;
        protected ColumnInfo _columnScenarioName;
        protected ColumnInfo _columnScenarioType;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnWeightbackFlag;
        protected ColumnInfo _columnItemCount;
        protected ColumnInfo _columnReportCount;

        public ColumnInfo ColumnScenarioTotalizationId { get { return _columnScenarioTotalizationId; } }
        public ColumnInfo ColumnScenarioName { get { return _columnScenarioName; } }
        public ColumnInfo ColumnScenarioType { get { return _columnScenarioType; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnWeightbackFlag { get { return _columnWeightbackFlag; } }
        public ColumnInfo ColumnItemCount { get { return _columnItemCount; } }
        public ColumnInfo ColumnReportCount { get { return _columnReportCount; } }

        protected void InitializeColumnInfo() {
            _columnScenarioTotalizationId = cci("SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", null, null, false, "ScenarioTotalizationId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenarioName = cci("SCENARIO_NAME", "SCENARIO_NAME", null, null, false, "ScenarioName", typeof(String), false, "VARCHAR2", 50, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenarioType = cci("SCENARIO_TYPE", "SCENARIO_TYPE", null, null, false, "ScenarioType", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, false, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWeightbackFlag = cci("WEIGHTBACK_FLAG", "WEIGHTBACK_FLAG", null, null, false, "WeightbackFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemCount = cci("ITEM_COUNT", "ITEM_COUNT", null, null, false, "ItemCount", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnReportCount = cci("REPORT_COUNT", "REPORT_COUNT", null, null, false, "ReportCount", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnScenarioTotalizationId);
            _columnInfoList.add(ColumnScenarioName);
            _columnInfoList.add(ColumnScenarioType);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnWeightbackFlag);
            _columnInfoList.add(ColumnItemCount);
            _columnInfoList.add(ColumnReportCount);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            throw new NotSupportedException("The table does not have primary key: " + TableDbName);
        }}

        // -------------------------------------------------
        //                                   Primary Element
        //                                   ---------------
        public override bool HasPrimaryKey { get { return false; } }
        public override bool HasCompoundPrimaryKey { get { return false; } }

        // ===============================================================================
        //                                                                   Relation Info
        //                                                                   =============
        // -------------------------------------------------
        //                                   Foreign Element
        //                                   ---------------


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
        public static readonly String TABLE_DB_NAME = "ScenarioNodesByQCWebIDEntity";
        public static readonly String TABLE_PROPERTY_NAME = "ScenarioNodesByQCWebIDEntity";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_SCENARIO_TOTALIZATION_ID = "SCENARIO_TOTALIZATION_ID";
        public static readonly String DB_NAME_SCENARIO_NAME = "SCENARIO_NAME";
        public static readonly String DB_NAME_SCENARIO_TYPE = "SCENARIO_TYPE";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_WEIGHTBACK_FLAG = "WEIGHTBACK_FLAG";
        public static readonly String DB_NAME_ITEM_COUNT = "ITEM_COUNT";
        public static readonly String DB_NAME_REPORT_COUNT = "REPORT_COUNT";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_SCENARIO_TOTALIZATION_ID = "ScenarioTotalizationId";
        public static readonly String PROPERTY_NAME_SCENARIO_NAME = "ScenarioName";
        public static readonly String PROPERTY_NAME_SCENARIO_TYPE = "ScenarioType";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_WEIGHTBACK_FLAG = "WeightbackFlag";
        public static readonly String PROPERTY_NAME_ITEM_COUNT = "ItemCount";
        public static readonly String PROPERTY_NAME_REPORT_COUNT = "ReportCount";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static ScenarioNodesByQCWebIDEntityDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), PROPERTY_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(DB_NAME_SCENARIO_NAME.ToLower(), PROPERTY_NAME_SCENARIO_NAME);
                map.put(DB_NAME_SCENARIO_TYPE.ToLower(), PROPERTY_NAME_SCENARIO_TYPE);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_WEIGHTBACK_FLAG.ToLower(), PROPERTY_NAME_WEIGHTBACK_FLAG);
                map.put(DB_NAME_ITEM_COUNT.ToLower(), PROPERTY_NAME_ITEM_COUNT);
                map.put(DB_NAME_REPORT_COUNT.ToLower(), PROPERTY_NAME_REPORT_COUNT);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), DB_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(PROPERTY_NAME_SCENARIO_NAME.ToLower(), DB_NAME_SCENARIO_NAME);
                map.put(PROPERTY_NAME_SCENARIO_TYPE.ToLower(), DB_NAME_SCENARIO_TYPE);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_WEIGHTBACK_FLAG.ToLower(), DB_NAME_WEIGHTBACK_FLAG);
                map.put(PROPERTY_NAME_ITEM_COUNT.ToLower(), DB_NAME_ITEM_COUNT);
                map.put(PROPERTY_NAME_REPORT_COUNT.ToLower(), DB_NAME_REPORT_COUNT);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.ScenarioNodesByQCWebIDEntity"; } }
        public override String DaoTypeName { get { return null; } }
        public override String ConditionBeanTypeName { get { return null; } }
        public override String BehaviorTypeName { get { return null; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public ScenarioNodesByQCWebIDEntity NewMyEntity() { return new ScenarioNodesByQCWebIDEntity(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<ScenarioNodesByQCWebIDEntity>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<ScenarioNodesByQCWebIDEntity>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("SCENARIO_TOTALIZATION_ID", "ScenarioTotalizationId", new EntityPropertyScenarioTotalizationIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_NAME", "ScenarioName", new EntityPropertyScenarioNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_TYPE", "ScenarioType", new EntityPropertyScenarioTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WEIGHTBACK_FLAG", "WeightbackFlag", new EntityPropertyWeightbackFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_COUNT", "ItemCount", new EntityPropertyItemCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REPORT_COUNT", "ReportCount", new EntityPropertyReportCountSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<ScenarioNodesByQCWebIDEntity> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((ScenarioNodesByQCWebIDEntity)entity, value);
        }

        public class EntityPropertyScenarioTotalizationIdSetupper : EntityPropertySetupper<ScenarioNodesByQCWebIDEntity> {
            public void Setup(ScenarioNodesByQCWebIDEntity entity, Object value) { entity.ScenarioTotalizationId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenarioNameSetupper : EntityPropertySetupper<ScenarioNodesByQCWebIDEntity> {
            public void Setup(ScenarioNodesByQCWebIDEntity entity, Object value) { entity.ScenarioName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyScenarioTypeSetupper : EntityPropertySetupper<ScenarioNodesByQCWebIDEntity> {
            public void Setup(ScenarioNodesByQCWebIDEntity entity, Object value) { entity.ScenarioType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<ScenarioNodesByQCWebIDEntity> {
            public void Setup(ScenarioNodesByQCWebIDEntity entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyWeightbackFlagSetupper : EntityPropertySetupper<ScenarioNodesByQCWebIDEntity> {
            public void Setup(ScenarioNodesByQCWebIDEntity entity, Object value) { entity.WeightbackFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyItemCountSetupper : EntityPropertySetupper<ScenarioNodesByQCWebIDEntity> {
            public void Setup(ScenarioNodesByQCWebIDEntity entity, Object value) { entity.ItemCount = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyReportCountSetupper : EntityPropertySetupper<ScenarioNodesByQCWebIDEntity> {
            public void Setup(ScenarioNodesByQCWebIDEntity entity, Object value) { entity.ReportCount = (value != null) ? (decimal?)value : null; }
        }
    }
}
