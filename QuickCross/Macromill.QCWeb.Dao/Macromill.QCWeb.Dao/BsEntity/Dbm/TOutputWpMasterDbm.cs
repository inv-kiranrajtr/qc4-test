
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

    public class TOutputWpMasterDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputWpMaster);

        private static readonly TOutputWpMasterDbm _instance = new TOutputWpMasterDbm();
        private TOutputWpMasterDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputWpMasterDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_WP_MASTER"; } }
        public override String TablePropertyName { get { return "TOutputWpMaster"; } }
        public override String TableSqlName { get { return "T_OUTPUT_WP_MASTER"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputWpMasterId;
        protected ColumnInfo _columnPoint;

        public ColumnInfo ColumnOutputWpMasterId { get { return _columnOutputWpMasterId; } }
        public ColumnInfo ColumnPoint { get { return _columnPoint; } }

        protected void InitializeColumnInfo() {
            _columnOutputWpMasterId = cci("OUTPUT_WP_MASTER_ID", "OUTPUT_WP_MASTER_ID", null, null, true, "OutputWpMasterId", typeof(String), true, "VARCHAR2", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPoint = cci("POINT", "POINT", null, null, true, "Point", typeof(int?), false, "NUMBER", 6, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputWpMasterId);
            _columnInfoList.add(ColumnPoint);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputWpMasterId);
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_WP_MASTER";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputWpMaster";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_WP_MASTER_ID = "OUTPUT_WP_MASTER_ID";
        public static readonly String DB_NAME_POINT = "POINT";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_WP_MASTER_ID = "OutputWpMasterId";
        public static readonly String PROPERTY_NAME_POINT = "Point";

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

        static TOutputWpMasterDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_WP_MASTER_ID.ToLower(), PROPERTY_NAME_OUTPUT_WP_MASTER_ID);
                map.put(DB_NAME_POINT.ToLower(), PROPERTY_NAME_POINT);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_WP_MASTER_ID.ToLower(), DB_NAME_OUTPUT_WP_MASTER_ID);
                map.put(PROPERTY_NAME_POINT.ToLower(), DB_NAME_POINT);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputWpMaster"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputWpMasterDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputWpMasterCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputWpMasterBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputWpMaster NewMyEntity() { return new TOutputWpMaster(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputWpMasterCB NewMyConditionBean() { return new TOutputWpMasterCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputWpMaster>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputWpMaster>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_WP_MASTER_ID", "OutputWpMasterId", new EntityPropertyOutputWpMasterIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("POINT", "Point", new EntityPropertyPointSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputWpMaster> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputWpMaster)entity, value);
        }

        public class EntityPropertyOutputWpMasterIdSetupper : EntityPropertySetupper<TOutputWpMaster> {
            public void Setup(TOutputWpMaster entity, Object value) { entity.OutputWpMasterId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyPointSetupper : EntityPropertySetupper<TOutputWpMaster> {
            public void Setup(TOutputWpMaster entity, Object value) { entity.Point = (value != null) ? (int?)value : null; }
        }
    }
}
