
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

    public class TSessionControlerDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TSessionControler);

        private static readonly TSessionControlerDbm _instance = new TSessionControlerDbm();
        private TSessionControlerDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TSessionControlerDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_SESSION_CONTROLER"; } }
        public override String TablePropertyName { get { return "TSessionControler"; } }
        public override String TableSqlName { get { return "T_SESSION_CONTROLER"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnSessionId;
        protected ColumnInfo _columnGuid;
        protected ColumnInfo _columnProcessServerCode;
        protected ColumnInfo _columnQcwebid;

        public ColumnInfo ColumnSessionId { get { return _columnSessionId; } }
        public ColumnInfo ColumnGuid { get { return _columnGuid; } }
        public ColumnInfo ColumnProcessServerCode { get { return _columnProcessServerCode; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }

        protected void InitializeColumnInfo() {
            _columnSessionId = cci("SESSION_ID", "SESSION_ID", null, null, true, "SessionId", typeof(String), true, "CHAR", 24, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGuid = cci("GUID", "GUID", null, null, true, "Guid", typeof(String), true, "VARCHAR2", 36, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnProcessServerCode = cci("PROCESS_SERVER_CODE", "PROCESS_SERVER_CODE", null, null, true, "ProcessServerCode", typeof(String), false, "VARCHAR2", 24, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnSessionId);
            _columnInfoList.add(ColumnGuid);
            _columnInfoList.add(ColumnProcessServerCode);
            _columnInfoList.add(ColumnQcwebid);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            List<ColumnInfo> ls = new ArrayList<ColumnInfo>();
            ls.add(ColumnSessionId);
            ls.add(ColumnGuid);
            return cpui(ls);
        }}

        // -------------------------------------------------
        //                                   Primary Element
        //                                   ---------------
        public override bool HasPrimaryKey { get { return true; } }
        public override bool HasCompoundPrimaryKey { get { return true; } }

        // ===============================================================================
        //                                                                   Relation Info
        //                                                                   =============
        // -------------------------------------------------
        //                                   Foreign Element
        //                                   ---------------
        public ForeignInfo ForeignTQcwebSurveyInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfo", this, TQcwebSurveyInfoDbm.GetInstance(), map, 0, false, false);
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
        public static readonly String TABLE_DB_NAME = "T_SESSION_CONTROLER";
        public static readonly String TABLE_PROPERTY_NAME = "TSessionControler";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_SESSION_ID = "SESSION_ID";
        public static readonly String DB_NAME_GUID = "GUID";
        public static readonly String DB_NAME_PROCESS_SERVER_CODE = "PROCESS_SERVER_CODE";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_SESSION_ID = "SessionId";
        public static readonly String PROPERTY_NAME_GUID = "Guid";
        public static readonly String PROPERTY_NAME_PROCESS_SERVER_CODE = "ProcessServerCode";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TSessionControlerDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_SESSION_ID.ToLower(), PROPERTY_NAME_SESSION_ID);
                map.put(DB_NAME_GUID.ToLower(), PROPERTY_NAME_GUID);
                map.put(DB_NAME_PROCESS_SERVER_CODE.ToLower(), PROPERTY_NAME_PROCESS_SERVER_CODE);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_SESSION_ID.ToLower(), DB_NAME_SESSION_ID);
                map.put(PROPERTY_NAME_GUID.ToLower(), DB_NAME_GUID);
                map.put(PROPERTY_NAME_PROCESS_SERVER_CODE.ToLower(), DB_NAME_PROCESS_SERVER_CODE);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TSessionControler"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TSessionControlerDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TSessionControlerCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TSessionControlerBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TSessionControler NewMyEntity() { return new TSessionControler(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TSessionControlerCB NewMyConditionBean() { return new TSessionControlerCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TSessionControler>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TSessionControler>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("SESSION_ID", "SessionId", new EntityPropertySessionIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GUID", "Guid", new EntityPropertyGuidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PROCESS_SERVER_CODE", "ProcessServerCode", new EntityPropertyProcessServerCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TSessionControler> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TSessionControler)entity, value);
        }

        public class EntityPropertySessionIdSetupper : EntityPropertySetupper<TSessionControler> {
            public void Setup(TSessionControler entity, Object value) { entity.SessionId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGuidSetupper : EntityPropertySetupper<TSessionControler> {
            public void Setup(TSessionControler entity, Object value) { entity.Guid = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyProcessServerCodeSetupper : EntityPropertySetupper<TSessionControler> {
            public void Setup(TSessionControler entity, Object value) { entity.ProcessServerCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TSessionControler> {
            public void Setup(TSessionControler entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
    }
}
