
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class TOutputRequestEntityDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputRequestEntity);

        private static readonly TOutputRequestEntityDbm _instance = new TOutputRequestEntityDbm();
        private TOutputRequestEntityDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputRequestEntityDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "TOutputRequestEntity"; } }
        public override String TablePropertyName { get { return "TOutputRequestEntity"; } }
        public override String TableSqlName { get { return "TOutputRequestEntity"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputReportsetInfoId;
        protected ColumnInfo _columnDownloadPath;
        protected ColumnInfo _columnOutputRequestId;

        public ColumnInfo ColumnOutputReportsetInfoId { get { return _columnOutputReportsetInfoId; } }
        public ColumnInfo ColumnDownloadPath { get { return _columnDownloadPath; } }
        public ColumnInfo ColumnOutputRequestId { get { return _columnOutputRequestId; } }

        protected void InitializeColumnInfo() {
            _columnOutputReportsetInfoId = cci("OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", null, null, false, "OutputReportsetInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDownloadPath = cci("DOWNLOAD_PATH", "DOWNLOAD_PATH", null, null, false, "DownloadPath", typeof(String), false, "VARCHAR2", 260, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputRequestId = cci("OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", null, null, false, "OutputRequestId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputReportsetInfoId);
            _columnInfoList.add(ColumnDownloadPath);
            _columnInfoList.add(ColumnOutputRequestId);
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
        public static readonly String TABLE_DB_NAME = "TOutputRequestEntity";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputRequestEntity";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_REPORTSET_INFO_ID = "OUTPUT_REPORTSET_INFO_ID";
        public static readonly String DB_NAME_DOWNLOAD_PATH = "DOWNLOAD_PATH";
        public static readonly String DB_NAME_OUTPUT_REQUEST_ID = "OUTPUT_REQUEST_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID = "OutputReportsetInfoId";
        public static readonly String PROPERTY_NAME_DOWNLOAD_PATH = "DownloadPath";
        public static readonly String PROPERTY_NAME_OUTPUT_REQUEST_ID = "OutputRequestId";

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

        static TOutputRequestEntityDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_REPORTSET_INFO_ID.ToLower(), PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID);
                map.put(DB_NAME_DOWNLOAD_PATH.ToLower(), PROPERTY_NAME_DOWNLOAD_PATH);
                map.put(DB_NAME_OUTPUT_REQUEST_ID.ToLower(), PROPERTY_NAME_OUTPUT_REQUEST_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID.ToLower(), DB_NAME_OUTPUT_REPORTSET_INFO_ID);
                map.put(PROPERTY_NAME_DOWNLOAD_PATH.ToLower(), DB_NAME_DOWNLOAD_PATH);
                map.put(PROPERTY_NAME_OUTPUT_REQUEST_ID.ToLower(), DB_NAME_OUTPUT_REQUEST_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.TOutputRequestEntity"; } }
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
        public TOutputRequestEntity NewMyEntity() { return new TOutputRequestEntity(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputRequestEntity>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputRequestEntity>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_REPORTSET_INFO_ID", "OutputReportsetInfoId", new EntityPropertyOutputReportsetInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DOWNLOAD_PATH", "DownloadPath", new EntityPropertyDownloadPathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_REQUEST_ID", "OutputRequestId", new EntityPropertyOutputRequestIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputRequestEntity> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputRequestEntity)entity, value);
        }

        public class EntityPropertyOutputReportsetInfoIdSetupper : EntityPropertySetupper<TOutputRequestEntity> {
            public void Setup(TOutputRequestEntity entity, Object value) { entity.OutputReportsetInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDownloadPathSetupper : EntityPropertySetupper<TOutputRequestEntity> {
            public void Setup(TOutputRequestEntity entity, Object value) { entity.DownloadPath = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOutputRequestIdSetupper : EntityPropertySetupper<TOutputRequestEntity> {
            public void Setup(TOutputRequestEntity entity, Object value) { entity.OutputRequestId = (value != null) ? (decimal?)value : null; }
        }
    }
}
