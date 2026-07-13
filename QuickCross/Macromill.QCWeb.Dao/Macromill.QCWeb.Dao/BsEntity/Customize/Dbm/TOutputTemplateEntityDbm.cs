
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class TOutputTemplateEntityDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputTemplateEntity);

        private static readonly TOutputTemplateEntityDbm _instance = new TOutputTemplateEntityDbm();
        private TOutputTemplateEntityDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputTemplateEntityDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "TOutputTemplateEntity"; } }
        public override String TablePropertyName { get { return "TOutputTemplateEntity"; } }
        public override String TableSqlName { get { return "TOutputTemplateEntity"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnOutputTemplateId;
        protected ColumnInfo _columnUploadPath;

        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnOutputTemplateId { get { return _columnOutputTemplateId; } }
        public ColumnInfo ColumnUploadPath { get { return _columnUploadPath; } }

        protected void InitializeColumnInfo() {
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, false, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputTemplateId = cci("OUTPUT_TEMPLATE_ID", "OUTPUT_TEMPLATE_ID", null, null, false, "OutputTemplateId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUploadPath = cci("UPLOAD_PATH", "UPLOAD_PATH", null, null, false, "UploadPath", typeof(String), false, "VARCHAR2", 780, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnOutputTemplateId);
            _columnInfoList.add(ColumnUploadPath);
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
        public static readonly String TABLE_DB_NAME = "TOutputTemplateEntity";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputTemplateEntity";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_OUTPUT_TEMPLATE_ID = "OUTPUT_TEMPLATE_ID";
        public static readonly String DB_NAME_UPLOAD_PATH = "UPLOAD_PATH";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_OUTPUT_TEMPLATE_ID = "OutputTemplateId";
        public static readonly String PROPERTY_NAME_UPLOAD_PATH = "UploadPath";

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

        static TOutputTemplateEntityDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_OUTPUT_TEMPLATE_ID.ToLower(), PROPERTY_NAME_OUTPUT_TEMPLATE_ID);
                map.put(DB_NAME_UPLOAD_PATH.ToLower(), PROPERTY_NAME_UPLOAD_PATH);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_OUTPUT_TEMPLATE_ID.ToLower(), DB_NAME_OUTPUT_TEMPLATE_ID);
                map.put(PROPERTY_NAME_UPLOAD_PATH.ToLower(), DB_NAME_UPLOAD_PATH);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.TOutputTemplateEntity"; } }
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
        public TOutputTemplateEntity NewMyEntity() { return new TOutputTemplateEntity(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputTemplateEntity>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputTemplateEntity>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TEMPLATE_ID", "OutputTemplateId", new EntityPropertyOutputTemplateIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UPLOAD_PATH", "UploadPath", new EntityPropertyUploadPathSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputTemplateEntity> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputTemplateEntity)entity, value);
        }

        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputTemplateEntity> {
            public void Setup(TOutputTemplateEntity entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputTemplateIdSetupper : EntityPropertySetupper<TOutputTemplateEntity> {
            public void Setup(TOutputTemplateEntity entity, Object value) { entity.OutputTemplateId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyUploadPathSetupper : EntityPropertySetupper<TOutputTemplateEntity> {
            public void Setup(TOutputTemplateEntity entity, Object value) { entity.UploadPath = (value != null) ? (String)value : null; }
        }
    }
}
