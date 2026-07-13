
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

    public class TAccessPermissionsInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TAccessPermissionsInfo);

        private static readonly TAccessPermissionsInfoDbm _instance = new TAccessPermissionsInfoDbm();
        private TAccessPermissionsInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TAccessPermissionsInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_ACCESS_PERMISSIONS_INFO"; } }
        public override String TablePropertyName { get { return "TAccessPermissionsInfo"; } }
        public override String TableSqlName { get { return "T_ACCESS_PERMISSIONS_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnAccessDatetime;
        protected ColumnInfo _columnClientId;
        protected ColumnInfo _columnAdminId;
        protected ColumnInfo _columnGuestId;

        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnAccessDatetime { get { return _columnAccessDatetime; } }
        public ColumnInfo ColumnClientId { get { return _columnClientId; } }
        public ColumnInfo ColumnAdminId { get { return _columnAdminId; } }
        public ColumnInfo ColumnGuestId { get { return _columnGuestId; } }

        protected void InitializeColumnInfo() {
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo,TQcwebSurveyInfoAsOne", "");
            _columnAccessDatetime = cci("ACCESS_DATETIME", "ACCESS_DATETIME", null, null, true, "AccessDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnClientId = cci("CLIENT_ID", "CLIENT_ID", null, null, true, "ClientId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAdminId = cci("ADMIN_ID", "ADMIN_ID", null, null, false, "AdminId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGuestId = cci("GUEST_ID", "GUEST_ID", null, null, false, "GuestId", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnAccessDatetime);
            _columnInfoList.add(ColumnClientId);
            _columnInfoList.add(ColumnAdminId);
            _columnInfoList.add(ColumnGuestId);
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
        public static readonly String TABLE_DB_NAME = "T_ACCESS_PERMISSIONS_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TAccessPermissionsInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_ACCESS_DATETIME = "ACCESS_DATETIME";
        public static readonly String DB_NAME_CLIENT_ID = "CLIENT_ID";
        public static readonly String DB_NAME_ADMIN_ID = "ADMIN_ID";
        public static readonly String DB_NAME_GUEST_ID = "GUEST_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_ACCESS_DATETIME = "AccessDatetime";
        public static readonly String PROPERTY_NAME_CLIENT_ID = "ClientId";
        public static readonly String PROPERTY_NAME_ADMIN_ID = "AdminId";
        public static readonly String PROPERTY_NAME_GUEST_ID = "GuestId";

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

        static TAccessPermissionsInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_ACCESS_DATETIME.ToLower(), PROPERTY_NAME_ACCESS_DATETIME);
                map.put(DB_NAME_CLIENT_ID.ToLower(), PROPERTY_NAME_CLIENT_ID);
                map.put(DB_NAME_ADMIN_ID.ToLower(), PROPERTY_NAME_ADMIN_ID);
                map.put(DB_NAME_GUEST_ID.ToLower(), PROPERTY_NAME_GUEST_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_ACCESS_DATETIME.ToLower(), DB_NAME_ACCESS_DATETIME);
                map.put(PROPERTY_NAME_CLIENT_ID.ToLower(), DB_NAME_CLIENT_ID);
                map.put(PROPERTY_NAME_ADMIN_ID.ToLower(), DB_NAME_ADMIN_ID);
                map.put(PROPERTY_NAME_GUEST_ID.ToLower(), DB_NAME_GUEST_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TAccessPermissionsInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TAccessPermissionsInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TAccessPermissionsInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TAccessPermissionsInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TAccessPermissionsInfo NewMyEntity() { return new TAccessPermissionsInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TAccessPermissionsInfoCB NewMyConditionBean() { return new TAccessPermissionsInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TAccessPermissionsInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TAccessPermissionsInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ACCESS_DATETIME", "AccessDatetime", new EntityPropertyAccessDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CLIENT_ID", "ClientId", new EntityPropertyClientIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ADMIN_ID", "AdminId", new EntityPropertyAdminIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GUEST_ID", "GuestId", new EntityPropertyGuestIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TAccessPermissionsInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TAccessPermissionsInfo)entity, value);
        }

        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TAccessPermissionsInfo> {
            public void Setup(TAccessPermissionsInfo entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAccessDatetimeSetupper : EntityPropertySetupper<TAccessPermissionsInfo> {
            public void Setup(TAccessPermissionsInfo entity, Object value) { entity.AccessDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyClientIdSetupper : EntityPropertySetupper<TAccessPermissionsInfo> {
            public void Setup(TAccessPermissionsInfo entity, Object value) { entity.ClientId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAdminIdSetupper : EntityPropertySetupper<TAccessPermissionsInfo> {
            public void Setup(TAccessPermissionsInfo entity, Object value) { entity.AdminId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyGuestIdSetupper : EntityPropertySetupper<TAccessPermissionsInfo> {
            public void Setup(TAccessPermissionsInfo entity, Object value) { entity.GuestId = (value != null) ? (String)value : null; }
        }
    }
}
