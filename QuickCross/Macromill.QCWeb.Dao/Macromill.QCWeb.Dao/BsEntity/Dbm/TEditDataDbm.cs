
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

    public class TEditDataDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TEditData);

        private static readonly TEditDataDbm _instance = new TEditDataDbm();
        private TEditDataDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TEditDataDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_EDIT_DATA"; } }
        public override String TablePropertyName { get { return "TEditData"; } }
        public override String TableSqlName { get { return "T_EDIT_DATA"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDataEditId;
        protected ColumnInfo _columnConditionFlag;
        protected ColumnInfo _columnEditMethod;
        protected ColumnInfo _columnEditValueType;
        protected ColumnInfo _columnEditValue;
        protected ColumnInfo _columnConditionDiv;

        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }
        public ColumnInfo ColumnConditionFlag { get { return _columnConditionFlag; } }
        public ColumnInfo ColumnEditMethod { get { return _columnEditMethod; } }
        public ColumnInfo ColumnEditValueType { get { return _columnEditValueType; } }
        public ColumnInfo ColumnEditValue { get { return _columnEditValue; } }
        public ColumnInfo ColumnConditionDiv { get { return _columnConditionDiv; } }

        protected void InitializeColumnInfo() {
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, true, "DataEditId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDataEditList", "TEditConditionList,TEditTargetItemList");
            _columnConditionFlag = cci("CONDITION_FLAG", "CONDITION_FLAG", null, null, true, "ConditionFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnEditMethod = cci("EDIT_METHOD", "EDIT_METHOD", null, null, false, "EditMethod", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnEditValueType = cci("EDIT_VALUE_TYPE", "EDIT_VALUE_TYPE", null, null, false, "EditValueType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnEditValue = cci("EDIT_VALUE", "EDIT_VALUE", null, null, false, "EditValue", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionDiv = cci("CONDITION_DIV", "CONDITION_DIV", null, null, true, "ConditionDiv", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDataEditId);
            _columnInfoList.add(ColumnConditionFlag);
            _columnInfoList.add(ColumnEditMethod);
            _columnInfoList.add(ColumnEditValueType);
            _columnInfoList.add(ColumnEditValue);
            _columnInfoList.add(ColumnConditionDiv);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnDataEditId);
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
        public ForeignInfo ForeignTDataEditList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDataEditListDbm.GetInstance().ColumnDataEditId);
            return cfi("TDataEditList", this, TDataEditListDbm.GetInstance(), map, 0, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTEditConditionList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TEditConditionDbm.GetInstance().ColumnDataEditId);
            return cri("TEditConditionList", this, TEditConditionDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTEditTargetItemList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TEditTargetItemDbm.GetInstance().ColumnDataEditId);
            return cri("TEditTargetItemList", this, TEditTargetItemDbm.GetInstance(), map, false);
        }}

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
        public static readonly String TABLE_DB_NAME = "T_EDIT_DATA";
        public static readonly String TABLE_PROPERTY_NAME = "TEditData";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";
        public static readonly String DB_NAME_CONDITION_FLAG = "CONDITION_FLAG";
        public static readonly String DB_NAME_EDIT_METHOD = "EDIT_METHOD";
        public static readonly String DB_NAME_EDIT_VALUE_TYPE = "EDIT_VALUE_TYPE";
        public static readonly String DB_NAME_EDIT_VALUE = "EDIT_VALUE";
        public static readonly String DB_NAME_CONDITION_DIV = "CONDITION_DIV";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";
        public static readonly String PROPERTY_NAME_CONDITION_FLAG = "ConditionFlag";
        public static readonly String PROPERTY_NAME_EDIT_METHOD = "EditMethod";
        public static readonly String PROPERTY_NAME_EDIT_VALUE_TYPE = "EditValueType";
        public static readonly String PROPERTY_NAME_EDIT_VALUE = "EditValue";
        public static readonly String PROPERTY_NAME_CONDITION_DIV = "ConditionDiv";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TDataEditList = "TDataEditList";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TEditConditionList = "TEditConditionList";
        public static readonly String REFERRER_PROPERTY_NAME_TEditTargetItemList = "TEditTargetItemList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TEditDataDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                map.put(DB_NAME_CONDITION_FLAG.ToLower(), PROPERTY_NAME_CONDITION_FLAG);
                map.put(DB_NAME_EDIT_METHOD.ToLower(), PROPERTY_NAME_EDIT_METHOD);
                map.put(DB_NAME_EDIT_VALUE_TYPE.ToLower(), PROPERTY_NAME_EDIT_VALUE_TYPE);
                map.put(DB_NAME_EDIT_VALUE.ToLower(), PROPERTY_NAME_EDIT_VALUE);
                map.put(DB_NAME_CONDITION_DIV.ToLower(), PROPERTY_NAME_CONDITION_DIV);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DATA_EDIT_ID.ToLower(), DB_NAME_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_CONDITION_FLAG.ToLower(), DB_NAME_CONDITION_FLAG);
                map.put(PROPERTY_NAME_EDIT_METHOD.ToLower(), DB_NAME_EDIT_METHOD);
                map.put(PROPERTY_NAME_EDIT_VALUE_TYPE.ToLower(), DB_NAME_EDIT_VALUE_TYPE);
                map.put(PROPERTY_NAME_EDIT_VALUE.ToLower(), DB_NAME_EDIT_VALUE);
                map.put(PROPERTY_NAME_CONDITION_DIV.ToLower(), DB_NAME_CONDITION_DIV);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TEditData"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TEditDataDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TEditDataCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TEditDataBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TEditData NewMyEntity() { return new TEditData(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TEditDataCB NewMyConditionBean() { return new TEditDataCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TEditData>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TEditData>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_FLAG", "ConditionFlag", new EntityPropertyConditionFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EDIT_METHOD", "EditMethod", new EntityPropertyEditMethodSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EDIT_VALUE_TYPE", "EditValueType", new EntityPropertyEditValueTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EDIT_VALUE", "EditValue", new EntityPropertyEditValueSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_DIV", "ConditionDiv", new EntityPropertyConditionDivSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TEditData> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TEditData)entity, value);
        }

        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TEditData> {
            public void Setup(TEditData entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyConditionFlagSetupper : EntityPropertySetupper<TEditData> {
            public void Setup(TEditData entity, Object value) { entity.ConditionFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyEditMethodSetupper : EntityPropertySetupper<TEditData> {
            public void Setup(TEditData entity, Object value) { entity.EditMethod = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyEditValueTypeSetupper : EntityPropertySetupper<TEditData> {
            public void Setup(TEditData entity, Object value) { entity.EditValueType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyEditValueSetupper : EntityPropertySetupper<TEditData> {
            public void Setup(TEditData entity, Object value) { entity.EditValue = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyConditionDivSetupper : EntityPropertySetupper<TEditData> {
            public void Setup(TEditData entity, Object value) { entity.ConditionDiv = (value != null) ? (String)value : null; }
        }
    }
}
