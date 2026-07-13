
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

    public class TMaintenanceDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TMaintenance);

        private static readonly TMaintenanceDbm _instance = new TMaintenanceDbm();
        private TMaintenanceDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TMaintenanceDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_MAINTENANCE"; } }
        public override String TablePropertyName { get { return "TMaintenance"; } }
        public override String TableSqlName { get { return "T_MAINTENANCE"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnMaintenanceId;
        protected ColumnInfo _columnLimitTime;

        public ColumnInfo ColumnMaintenanceId { get { return _columnMaintenanceId; } }
        public ColumnInfo ColumnLimitTime { get { return _columnLimitTime; } }

        protected void InitializeColumnInfo() {
            _columnMaintenanceId = cci("MAINTENANCE_ID", "MAINTENANCE_ID", null, null, true, "MaintenanceId", typeof(String), true, "VARCHAR2", 20, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLimitTime = cci("LIMIT_TIME", "LIMIT_TIME", null, null, true, "LimitTime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnMaintenanceId);
            _columnInfoList.add(ColumnLimitTime);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnMaintenanceId);
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
        public static readonly String TABLE_DB_NAME = "T_MAINTENANCE";
        public static readonly String TABLE_PROPERTY_NAME = "TMaintenance";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_MAINTENANCE_ID = "MAINTENANCE_ID";
        public static readonly String DB_NAME_LIMIT_TIME = "LIMIT_TIME";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_MAINTENANCE_ID = "MaintenanceId";
        public static readonly String PROPERTY_NAME_LIMIT_TIME = "LimitTime";

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

        static TMaintenanceDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_MAINTENANCE_ID.ToLower(), PROPERTY_NAME_MAINTENANCE_ID);
                map.put(DB_NAME_LIMIT_TIME.ToLower(), PROPERTY_NAME_LIMIT_TIME);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_MAINTENANCE_ID.ToLower(), DB_NAME_MAINTENANCE_ID);
                map.put(PROPERTY_NAME_LIMIT_TIME.ToLower(), DB_NAME_LIMIT_TIME);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TMaintenance"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TMaintenanceDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TMaintenanceCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TMaintenanceBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TMaintenance NewMyEntity() { return new TMaintenance(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TMaintenanceCB NewMyConditionBean() { return new TMaintenanceCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TMaintenance>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TMaintenance>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("MAINTENANCE_ID", "MaintenanceId", new EntityPropertyMaintenanceIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LIMIT_TIME", "LimitTime", new EntityPropertyLimitTimeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TMaintenance> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TMaintenance)entity, value);
        }

        public class EntityPropertyMaintenanceIdSetupper : EntityPropertySetupper<TMaintenance> {
            public void Setup(TMaintenance entity, Object value) { entity.MaintenanceId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLimitTimeSetupper : EntityPropertySetupper<TMaintenance> {
            public void Setup(TMaintenance entity, Object value) { entity.LimitTime = (value != null) ? (DateTime?)value : null; }
        }
    }
}
