
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

    public class TCodeMasterDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TCodeMaster);

        private static readonly TCodeMasterDbm _instance = new TCodeMasterDbm();
        private TCodeMasterDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TCodeMasterDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_CODE_MASTER"; } }
        public override String TablePropertyName { get { return "TCodeMaster"; } }
        public override String TableSqlName { get { return "T_CODE_MASTER"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnCodeMasterId;
        protected ColumnInfo _columnGroupKey;
        protected ColumnInfo _columnCodeValue;
        protected ColumnInfo _columnMessageId;
        protected ColumnInfo _columnSortNo;

        public ColumnInfo ColumnCodeMasterId { get { return _columnCodeMasterId; } }
        public ColumnInfo ColumnGroupKey { get { return _columnGroupKey; } }
        public ColumnInfo ColumnCodeValue { get { return _columnCodeValue; } }
        public ColumnInfo ColumnMessageId { get { return _columnMessageId; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }

        protected void InitializeColumnInfo() {
            _columnCodeMasterId = cci("CODE_MASTER_ID", "CODE_MASTER_ID", null, null, true, "CodeMasterId", typeof(String), true, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGroupKey = cci("GROUP_KEY", "GROUP_KEY", null, null, true, "GroupKey", typeof(String), false, "VARCHAR2", 30, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCodeValue = cci("CODE_VALUE", "CODE_VALUE", null, null, true, "CodeValue", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMessageId = cci("MESSAGE_ID", "MESSAGE_ID", null, null, true, "MessageId", typeof(String), false, "VARCHAR2", 60, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnCodeMasterId);
            _columnInfoList.add(ColumnGroupKey);
            _columnInfoList.add(ColumnCodeValue);
            _columnInfoList.add(ColumnMessageId);
            _columnInfoList.add(ColumnSortNo);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnCodeMasterId);
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
        public static readonly String TABLE_DB_NAME = "T_CODE_MASTER";
        public static readonly String TABLE_PROPERTY_NAME = "TCodeMaster";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_CODE_MASTER_ID = "CODE_MASTER_ID";
        public static readonly String DB_NAME_GROUP_KEY = "GROUP_KEY";
        public static readonly String DB_NAME_CODE_VALUE = "CODE_VALUE";
        public static readonly String DB_NAME_MESSAGE_ID = "MESSAGE_ID";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_CODE_MASTER_ID = "CodeMasterId";
        public static readonly String PROPERTY_NAME_GROUP_KEY = "GroupKey";
        public static readonly String PROPERTY_NAME_CODE_VALUE = "CodeValue";
        public static readonly String PROPERTY_NAME_MESSAGE_ID = "MessageId";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";

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

        static TCodeMasterDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_CODE_MASTER_ID.ToLower(), PROPERTY_NAME_CODE_MASTER_ID);
                map.put(DB_NAME_GROUP_KEY.ToLower(), PROPERTY_NAME_GROUP_KEY);
                map.put(DB_NAME_CODE_VALUE.ToLower(), PROPERTY_NAME_CODE_VALUE);
                map.put(DB_NAME_MESSAGE_ID.ToLower(), PROPERTY_NAME_MESSAGE_ID);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_CODE_MASTER_ID.ToLower(), DB_NAME_CODE_MASTER_ID);
                map.put(PROPERTY_NAME_GROUP_KEY.ToLower(), DB_NAME_GROUP_KEY);
                map.put(PROPERTY_NAME_CODE_VALUE.ToLower(), DB_NAME_CODE_VALUE);
                map.put(PROPERTY_NAME_MESSAGE_ID.ToLower(), DB_NAME_MESSAGE_ID);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TCodeMaster"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TCodeMasterDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TCodeMasterCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TCodeMasterBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TCodeMaster NewMyEntity() { return new TCodeMaster(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TCodeMasterCB NewMyConditionBean() { return new TCodeMasterCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TCodeMaster>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TCodeMaster>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("CODE_MASTER_ID", "CodeMasterId", new EntityPropertyCodeMasterIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GROUP_KEY", "GroupKey", new EntityPropertyGroupKeySetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CODE_VALUE", "CodeValue", new EntityPropertyCodeValueSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MESSAGE_ID", "MessageId", new EntityPropertyMessageIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TCodeMaster> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TCodeMaster)entity, value);
        }

        public class EntityPropertyCodeMasterIdSetupper : EntityPropertySetupper<TCodeMaster> {
            public void Setup(TCodeMaster entity, Object value) { entity.CodeMasterId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGroupKeySetupper : EntityPropertySetupper<TCodeMaster> {
            public void Setup(TCodeMaster entity, Object value) { entity.GroupKey = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCodeValueSetupper : EntityPropertySetupper<TCodeMaster> {
            public void Setup(TCodeMaster entity, Object value) { entity.CodeValue = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyMessageIdSetupper : EntityPropertySetupper<TCodeMaster> {
            public void Setup(TCodeMaster entity, Object value) { entity.MessageId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TCodeMaster> {
            public void Setup(TCodeMaster entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
    }
}
