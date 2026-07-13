
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class TOutputTemplateCheckOKTemplateDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputTemplateCheckOKTemplate);

        private static readonly TOutputTemplateCheckOKTemplateDbm _instance = new TOutputTemplateCheckOKTemplateDbm();
        private TOutputTemplateCheckOKTemplateDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputTemplateCheckOKTemplateDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "TOutputTemplateCheckOKTemplate"; } }
        public override String TablePropertyName { get { return "TOutputTemplateCheckOKTemplate"; } }
        public override String TableSqlName { get { return "TOutputTemplateCheckOKTemplate"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputTemplateId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnOutputTemplateMasterId;
        protected ColumnInfo _columnUploadPath;
        protected ColumnInfo _columnAlias;

        public ColumnInfo ColumnOutputTemplateId { get { return _columnOutputTemplateId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnOutputTemplateMasterId { get { return _columnOutputTemplateMasterId; } }
        public ColumnInfo ColumnUploadPath { get { return _columnUploadPath; } }
        public ColumnInfo ColumnAlias { get { return _columnAlias; } }

        protected void InitializeColumnInfo() {
            _columnOutputTemplateId = cci("OUTPUT_TEMPLATE_ID", "OUTPUT_TEMPLATE_ID", null, null, false, "OutputTemplateId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, false, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputTemplateMasterId = cci("OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", null, null, false, "OutputTemplateMasterId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUploadPath = cci("UPLOAD_PATH", "UPLOAD_PATH", null, null, false, "UploadPath", typeof(String), false, "VARCHAR2", 780, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAlias = cci("ALIAS", "ALIAS", null, null, false, "Alias", typeof(String), false, "VARCHAR2", 780, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputTemplateId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnOutputTemplateMasterId);
            _columnInfoList.add(ColumnUploadPath);
            _columnInfoList.add(ColumnAlias);
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
        public static readonly String TABLE_DB_NAME = "TOutputTemplateCheckOKTemplate";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputTemplateCheckOKTemplate";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_TEMPLATE_ID = "OUTPUT_TEMPLATE_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_OUTPUT_TEMPLATE_MASTER_ID = "OUTPUT_TEMPLATE_MASTER_ID";
        public static readonly String DB_NAME_UPLOAD_PATH = "UPLOAD_PATH";
        public static readonly String DB_NAME_ALIAS = "ALIAS";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_TEMPLATE_ID = "OutputTemplateId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_OUTPUT_TEMPLATE_MASTER_ID = "OutputTemplateMasterId";
        public static readonly String PROPERTY_NAME_UPLOAD_PATH = "UploadPath";
        public static readonly String PROPERTY_NAME_ALIAS = "Alias";

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

        static TOutputTemplateCheckOKTemplateDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_TEMPLATE_ID.ToLower(), PROPERTY_NAME_OUTPUT_TEMPLATE_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_OUTPUT_TEMPLATE_MASTER_ID.ToLower(), PROPERTY_NAME_OUTPUT_TEMPLATE_MASTER_ID);
                map.put(DB_NAME_UPLOAD_PATH.ToLower(), PROPERTY_NAME_UPLOAD_PATH);
                map.put(DB_NAME_ALIAS.ToLower(), PROPERTY_NAME_ALIAS);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_TEMPLATE_ID.ToLower(), DB_NAME_OUTPUT_TEMPLATE_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_OUTPUT_TEMPLATE_MASTER_ID.ToLower(), DB_NAME_OUTPUT_TEMPLATE_MASTER_ID);
                map.put(PROPERTY_NAME_UPLOAD_PATH.ToLower(), DB_NAME_UPLOAD_PATH);
                map.put(PROPERTY_NAME_ALIAS.ToLower(), DB_NAME_ALIAS);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.TOutputTemplateCheckOKTemplate"; } }
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
        public TOutputTemplateCheckOKTemplate NewMyEntity() { return new TOutputTemplateCheckOKTemplate(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputTemplateCheckOKTemplate>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputTemplateCheckOKTemplate>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_TEMPLATE_ID", "OutputTemplateId", new EntityPropertyOutputTemplateIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TEMPLATE_MASTER_ID", "OutputTemplateMasterId", new EntityPropertyOutputTemplateMasterIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UPLOAD_PATH", "UploadPath", new EntityPropertyUploadPathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ALIAS", "Alias", new EntityPropertyAliasSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputTemplateCheckOKTemplate> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputTemplateCheckOKTemplate)entity, value);
        }

        public class EntityPropertyOutputTemplateIdSetupper : EntityPropertySetupper<TOutputTemplateCheckOKTemplate> {
            public void Setup(TOutputTemplateCheckOKTemplate entity, Object value) { entity.OutputTemplateId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputTemplateCheckOKTemplate> {
            public void Setup(TOutputTemplateCheckOKTemplate entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputTemplateMasterIdSetupper : EntityPropertySetupper<TOutputTemplateCheckOKTemplate> {
            public void Setup(TOutputTemplateCheckOKTemplate entity, Object value) { entity.OutputTemplateMasterId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyUploadPathSetupper : EntityPropertySetupper<TOutputTemplateCheckOKTemplate> {
            public void Setup(TOutputTemplateCheckOKTemplate entity, Object value) { entity.UploadPath = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyAliasSetupper : EntityPropertySetupper<TOutputTemplateCheckOKTemplate> {
            public void Setup(TOutputTemplateCheckOKTemplate entity, Object value) { entity.Alias = (value != null) ? (String)value : null; }
        }
    }
}
