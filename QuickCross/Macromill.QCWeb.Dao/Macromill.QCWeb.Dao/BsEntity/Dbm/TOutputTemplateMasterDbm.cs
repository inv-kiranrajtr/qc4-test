
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

    public class TOutputTemplateMasterDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputTemplateMaster);

        private static readonly TOutputTemplateMasterDbm _instance = new TOutputTemplateMasterDbm();
        private TOutputTemplateMasterDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputTemplateMasterDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_TEMPLATE_MASTER"; } }
        public override String TablePropertyName { get { return "TOutputTemplateMaster"; } }
        public override String TableSqlName { get { return "T_OUTPUT_TEMPLATE_MASTER"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputTemplateMasterId;
        protected ColumnInfo _columnPath;
        protected ColumnInfo _columnMd5Hash;

        public ColumnInfo ColumnOutputTemplateMasterId { get { return _columnOutputTemplateMasterId; } }
        public ColumnInfo ColumnPath { get { return _columnPath; } }
        public ColumnInfo ColumnMd5Hash { get { return _columnMd5Hash; } }

        protected void InitializeColumnInfo() {
            _columnOutputTemplateMasterId = cci("OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", null, null, true, "OutputTemplateMasterId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TOutputTemplateList");
            _columnPath = cci("PATH", "PATH", null, null, true, "Path", typeof(String), false, "VARCHAR2", 780, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMd5Hash = cci("MD5_HASH", "MD5_HASH", null, null, true, "Md5Hash", typeof(String), false, "CHAR", 128, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputTemplateMasterId);
            _columnInfoList.add(ColumnPath);
            _columnInfoList.add(ColumnMd5Hash);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputTemplateMasterId);
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
        public ReferrerInfo ReferrerTOutputTemplateList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputTemplateMasterId, TOutputTemplateDbm.GetInstance().ColumnOutputTemplateMasterId);
            return cri("TOutputTemplateList", this, TOutputTemplateDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Output_Template_Master_SEQ01"; } }
        public override String SequenceNextValSql { get { return "select T_Output_Template_Master_SEQ01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_TEMPLATE_MASTER";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputTemplateMaster";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_TEMPLATE_MASTER_ID = "OUTPUT_TEMPLATE_MASTER_ID";
        public static readonly String DB_NAME_PATH = "PATH";
        public static readonly String DB_NAME_MD5_HASH = "MD5_HASH";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_TEMPLATE_MASTER_ID = "OutputTemplateMasterId";
        public static readonly String PROPERTY_NAME_PATH = "Path";
        public static readonly String PROPERTY_NAME_MD5_HASH = "Md5Hash";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TOutputTemplateList = "TOutputTemplateList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputTemplateMasterDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_TEMPLATE_MASTER_ID.ToLower(), PROPERTY_NAME_OUTPUT_TEMPLATE_MASTER_ID);
                map.put(DB_NAME_PATH.ToLower(), PROPERTY_NAME_PATH);
                map.put(DB_NAME_MD5_HASH.ToLower(), PROPERTY_NAME_MD5_HASH);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_TEMPLATE_MASTER_ID.ToLower(), DB_NAME_OUTPUT_TEMPLATE_MASTER_ID);
                map.put(PROPERTY_NAME_PATH.ToLower(), DB_NAME_PATH);
                map.put(PROPERTY_NAME_MD5_HASH.ToLower(), DB_NAME_MD5_HASH);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputTemplateMaster"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputTemplateMasterDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputTemplateMasterCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputTemplateMasterBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputTemplateMaster NewMyEntity() { return new TOutputTemplateMaster(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputTemplateMasterCB NewMyConditionBean() { return new TOutputTemplateMasterCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputTemplateMaster>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputTemplateMaster>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_TEMPLATE_MASTER_ID", "OutputTemplateMasterId", new EntityPropertyOutputTemplateMasterIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PATH", "Path", new EntityPropertyPathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MD5_HASH", "Md5Hash", new EntityPropertyMd5HashSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputTemplateMaster> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputTemplateMaster)entity, value);
        }

        public class EntityPropertyOutputTemplateMasterIdSetupper : EntityPropertySetupper<TOutputTemplateMaster> {
            public void Setup(TOutputTemplateMaster entity, Object value) { entity.OutputTemplateMasterId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyPathSetupper : EntityPropertySetupper<TOutputTemplateMaster> {
            public void Setup(TOutputTemplateMaster entity, Object value) { entity.Path = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyMd5HashSetupper : EntityPropertySetupper<TOutputTemplateMaster> {
            public void Setup(TOutputTemplateMaster entity, Object value) { entity.Md5Hash = (value != null) ? (String)value : null; }
        }
    }
}
