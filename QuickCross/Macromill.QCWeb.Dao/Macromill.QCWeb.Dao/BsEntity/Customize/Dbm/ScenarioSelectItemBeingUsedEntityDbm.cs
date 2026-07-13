
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class ScenarioSelectItemBeingUsedEntityDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(ScenarioSelectItemBeingUsedEntity);

        private static readonly ScenarioSelectItemBeingUsedEntityDbm _instance = new ScenarioSelectItemBeingUsedEntityDbm();
        private ScenarioSelectItemBeingUsedEntityDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static ScenarioSelectItemBeingUsedEntityDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "ScenarioSelectItemBeingUsedEntity"; } }
        public override String TablePropertyName { get { return "ScenarioSelectItemBeingUsedEntity"; } }
        public override String TableSqlName { get { return "ScenarioSelectItemBeingUsedEntity"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnScenarioid;
        protected ColumnInfo _columnScenariotype;
        protected ColumnInfo _columnScenarioname;
        protected ColumnInfo _columnWb;
        protected ColumnInfo _columnWbcode;
        protected ColumnInfo _columnQuerylist;
        protected ColumnInfo _columnTarget;
        protected ColumnInfo _columnAxis;
        protected ColumnInfo _columnFaadd;

        public ColumnInfo ColumnScenarioid { get { return _columnScenarioid; } }
        public ColumnInfo ColumnScenariotype { get { return _columnScenariotype; } }
        public ColumnInfo ColumnScenarioname { get { return _columnScenarioname; } }
        public ColumnInfo ColumnWb { get { return _columnWb; } }
        public ColumnInfo ColumnWbcode { get { return _columnWbcode; } }
        public ColumnInfo ColumnQuerylist { get { return _columnQuerylist; } }
        public ColumnInfo ColumnTarget { get { return _columnTarget; } }
        public ColumnInfo ColumnAxis { get { return _columnAxis; } }
        public ColumnInfo ColumnFaadd { get { return _columnFaadd; } }

        protected void InitializeColumnInfo() {
            _columnScenarioid = cci("SCENARIOID", "SCENARIOID", null, null, false, "Scenarioid", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenariotype = cci("SCENARIOTYPE", "SCENARIOTYPE", null, null, false, "Scenariotype", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenarioname = cci("SCENARIONAME", "SCENARIONAME", null, null, false, "Scenarioname", typeof(String), false, "VARCHAR2", 50, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWb = cci("WB", "WB", null, null, false, "Wb", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWbcode = cci("WBCODE", "WBCODE", null, null, false, "Wbcode", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQuerylist = cci("QUERYLIST", "QUERYLIST", null, null, false, "Querylist", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTarget = cci("TARGET", "TARGET", null, null, false, "Target", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAxis = cci("AXIS", "AXIS", null, null, false, "Axis", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFaadd = cci("FAADD", "FAADD", null, null, false, "Faadd", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnScenarioid);
            _columnInfoList.add(ColumnScenariotype);
            _columnInfoList.add(ColumnScenarioname);
            _columnInfoList.add(ColumnWb);
            _columnInfoList.add(ColumnWbcode);
            _columnInfoList.add(ColumnQuerylist);
            _columnInfoList.add(ColumnTarget);
            _columnInfoList.add(ColumnAxis);
            _columnInfoList.add(ColumnFaadd);
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
        public static readonly String TABLE_DB_NAME = "ScenarioSelectItemBeingUsedEntity";
        public static readonly String TABLE_PROPERTY_NAME = "ScenarioSelectItemBeingUsedEntity";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_SCENARIOID = "SCENARIOID";
        public static readonly String DB_NAME_SCENARIOTYPE = "SCENARIOTYPE";
        public static readonly String DB_NAME_SCENARIONAME = "SCENARIONAME";
        public static readonly String DB_NAME_WB = "WB";
        public static readonly String DB_NAME_WBCODE = "WBCODE";
        public static readonly String DB_NAME_QUERYLIST = "QUERYLIST";
        public static readonly String DB_NAME_TARGET = "TARGET";
        public static readonly String DB_NAME_AXIS = "AXIS";
        public static readonly String DB_NAME_FAADD = "FAADD";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_SCENARIOID = "Scenarioid";
        public static readonly String PROPERTY_NAME_SCENARIOTYPE = "Scenariotype";
        public static readonly String PROPERTY_NAME_SCENARIONAME = "Scenarioname";
        public static readonly String PROPERTY_NAME_WB = "Wb";
        public static readonly String PROPERTY_NAME_WBCODE = "Wbcode";
        public static readonly String PROPERTY_NAME_QUERYLIST = "Querylist";
        public static readonly String PROPERTY_NAME_TARGET = "Target";
        public static readonly String PROPERTY_NAME_AXIS = "Axis";
        public static readonly String PROPERTY_NAME_FAADD = "Faadd";

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

        static ScenarioSelectItemBeingUsedEntityDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_SCENARIOID.ToLower(), PROPERTY_NAME_SCENARIOID);
                map.put(DB_NAME_SCENARIOTYPE.ToLower(), PROPERTY_NAME_SCENARIOTYPE);
                map.put(DB_NAME_SCENARIONAME.ToLower(), PROPERTY_NAME_SCENARIONAME);
                map.put(DB_NAME_WB.ToLower(), PROPERTY_NAME_WB);
                map.put(DB_NAME_WBCODE.ToLower(), PROPERTY_NAME_WBCODE);
                map.put(DB_NAME_QUERYLIST.ToLower(), PROPERTY_NAME_QUERYLIST);
                map.put(DB_NAME_TARGET.ToLower(), PROPERTY_NAME_TARGET);
                map.put(DB_NAME_AXIS.ToLower(), PROPERTY_NAME_AXIS);
                map.put(DB_NAME_FAADD.ToLower(), PROPERTY_NAME_FAADD);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_SCENARIOID.ToLower(), DB_NAME_SCENARIOID);
                map.put(PROPERTY_NAME_SCENARIOTYPE.ToLower(), DB_NAME_SCENARIOTYPE);
                map.put(PROPERTY_NAME_SCENARIONAME.ToLower(), DB_NAME_SCENARIONAME);
                map.put(PROPERTY_NAME_WB.ToLower(), DB_NAME_WB);
                map.put(PROPERTY_NAME_WBCODE.ToLower(), DB_NAME_WBCODE);
                map.put(PROPERTY_NAME_QUERYLIST.ToLower(), DB_NAME_QUERYLIST);
                map.put(PROPERTY_NAME_TARGET.ToLower(), DB_NAME_TARGET);
                map.put(PROPERTY_NAME_AXIS.ToLower(), DB_NAME_AXIS);
                map.put(PROPERTY_NAME_FAADD.ToLower(), DB_NAME_FAADD);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.ScenarioSelectItemBeingUsedEntity"; } }
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
        public ScenarioSelectItemBeingUsedEntity NewMyEntity() { return new ScenarioSelectItemBeingUsedEntity(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("SCENARIOID", "Scenarioid", new EntityPropertyScenarioidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIOTYPE", "Scenariotype", new EntityPropertyScenariotypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIONAME", "Scenarioname", new EntityPropertyScenarionameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WB", "Wb", new EntityPropertyWbSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WBCODE", "Wbcode", new EntityPropertyWbcodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QUERYLIST", "Querylist", new EntityPropertyQuerylistSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TARGET", "Target", new EntityPropertyTargetSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("AXIS", "Axis", new EntityPropertyAxisSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FAADD", "Faadd", new EntityPropertyFaaddSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((ScenarioSelectItemBeingUsedEntity)entity, value);
        }

        public class EntityPropertyScenarioidSetupper : EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity> {
            public void Setup(ScenarioSelectItemBeingUsedEntity entity, Object value) { entity.Scenarioid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenariotypeSetupper : EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity> {
            public void Setup(ScenarioSelectItemBeingUsedEntity entity, Object value) { entity.Scenariotype = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyScenarionameSetupper : EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity> {
            public void Setup(ScenarioSelectItemBeingUsedEntity entity, Object value) { entity.Scenarioname = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyWbSetupper : EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity> {
            public void Setup(ScenarioSelectItemBeingUsedEntity entity, Object value) { entity.Wb = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyWbcodeSetupper : EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity> {
            public void Setup(ScenarioSelectItemBeingUsedEntity entity, Object value) { entity.Wbcode = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQuerylistSetupper : EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity> {
            public void Setup(ScenarioSelectItemBeingUsedEntity entity, Object value) { entity.Querylist = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTargetSetupper : EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity> {
            public void Setup(ScenarioSelectItemBeingUsedEntity entity, Object value) { entity.Target = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAxisSetupper : EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity> {
            public void Setup(ScenarioSelectItemBeingUsedEntity entity, Object value) { entity.Axis = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyFaaddSetupper : EntityPropertySetupper<ScenarioSelectItemBeingUsedEntity> {
            public void Setup(ScenarioSelectItemBeingUsedEntity entity, Object value) { entity.Faadd = (value != null) ? (decimal?)value : null; }
        }
    }
}
