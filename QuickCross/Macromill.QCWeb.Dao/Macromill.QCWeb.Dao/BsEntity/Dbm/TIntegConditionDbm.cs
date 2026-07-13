
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

    public class TIntegConditionDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TIntegCondition);

        private static readonly TIntegConditionDbm _instance = new TIntegConditionDbm();
        private TIntegConditionDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TIntegConditionDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_INTEG_CONDITION"; } }
        public override String TablePropertyName { get { return "TIntegCondition"; } }
        public override String TableSqlName { get { return "T_INTEG_CONDITION"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnIntegConditionId;
        protected ColumnInfo _columnConditionNo;
        protected ColumnInfo _columnSrcItemId;
        protected ColumnInfo _columnSourceItemNo;
        protected ColumnInfo _columnOperationCode;
        protected ColumnInfo _columnConditionString;
        protected ColumnInfo _columnDataEditId;

        public ColumnInfo ColumnIntegConditionId { get { return _columnIntegConditionId; } }
        public ColumnInfo ColumnConditionNo { get { return _columnConditionNo; } }
        public ColumnInfo ColumnSrcItemId { get { return _columnSrcItemId; } }
        public ColumnInfo ColumnSourceItemNo { get { return _columnSourceItemNo; } }
        public ColumnInfo ColumnOperationCode { get { return _columnOperationCode; } }
        public ColumnInfo ColumnConditionString { get { return _columnConditionString; } }
        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }

        protected void InitializeColumnInfo() {
            _columnIntegConditionId = cci("INTEG_CONDITION_ID", "INTEG_CONDITION_ID", null, null, true, "IntegConditionId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionNo = cci("CONDITION_NO", "CONDITION_NO", null, null, true, "ConditionNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSrcItemId = cci("SRC_ITEM_ID", "SRC_ITEM_ID", null, null, true, "SrcItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSourceItemNo = cci("SOURCE_ITEM_NO", "SOURCE_ITEM_NO", null, null, true, "SourceItemNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOperationCode = cci("OPERATION_CODE", "OPERATION_CODE", null, null, false, "OperationCode", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionString = cci("CONDITION_STRING", "CONDITION_STRING", null, null, false, "ConditionString", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, true, "DataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDataProcessNewItem", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnIntegConditionId);
            _columnInfoList.add(ColumnConditionNo);
            _columnInfoList.add(ColumnSrcItemId);
            _columnInfoList.add(ColumnSourceItemNo);
            _columnInfoList.add(ColumnOperationCode);
            _columnInfoList.add(ColumnConditionString);
            _columnInfoList.add(ColumnDataEditId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnIntegConditionId);
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
        public ForeignInfo ForeignTDataProcessNewItem { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDataProcessNewItemDbm.GetInstance().ColumnDataEditId);
            return cfi("TDataProcessNewItem", this, TDataProcessNewItemDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Integ_Condition_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Integ_Condition_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_INTEG_CONDITION";
        public static readonly String TABLE_PROPERTY_NAME = "TIntegCondition";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_INTEG_CONDITION_ID = "INTEG_CONDITION_ID";
        public static readonly String DB_NAME_CONDITION_NO = "CONDITION_NO";
        public static readonly String DB_NAME_SRC_ITEM_ID = "SRC_ITEM_ID";
        public static readonly String DB_NAME_SOURCE_ITEM_NO = "SOURCE_ITEM_NO";
        public static readonly String DB_NAME_OPERATION_CODE = "OPERATION_CODE";
        public static readonly String DB_NAME_CONDITION_STRING = "CONDITION_STRING";
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_INTEG_CONDITION_ID = "IntegConditionId";
        public static readonly String PROPERTY_NAME_CONDITION_NO = "ConditionNo";
        public static readonly String PROPERTY_NAME_SRC_ITEM_ID = "SrcItemId";
        public static readonly String PROPERTY_NAME_SOURCE_ITEM_NO = "SourceItemNo";
        public static readonly String PROPERTY_NAME_OPERATION_CODE = "OperationCode";
        public static readonly String PROPERTY_NAME_CONDITION_STRING = "ConditionString";
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TDataProcessNewItem = "TDataProcessNewItem";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TIntegConditionDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_INTEG_CONDITION_ID.ToLower(), PROPERTY_NAME_INTEG_CONDITION_ID);
                map.put(DB_NAME_CONDITION_NO.ToLower(), PROPERTY_NAME_CONDITION_NO);
                map.put(DB_NAME_SRC_ITEM_ID.ToLower(), PROPERTY_NAME_SRC_ITEM_ID);
                map.put(DB_NAME_SOURCE_ITEM_NO.ToLower(), PROPERTY_NAME_SOURCE_ITEM_NO);
                map.put(DB_NAME_OPERATION_CODE.ToLower(), PROPERTY_NAME_OPERATION_CODE);
                map.put(DB_NAME_CONDITION_STRING.ToLower(), PROPERTY_NAME_CONDITION_STRING);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_INTEG_CONDITION_ID.ToLower(), DB_NAME_INTEG_CONDITION_ID);
                map.put(PROPERTY_NAME_CONDITION_NO.ToLower(), DB_NAME_CONDITION_NO);
                map.put(PROPERTY_NAME_SRC_ITEM_ID.ToLower(), DB_NAME_SRC_ITEM_ID);
                map.put(PROPERTY_NAME_SOURCE_ITEM_NO.ToLower(), DB_NAME_SOURCE_ITEM_NO);
                map.put(PROPERTY_NAME_OPERATION_CODE.ToLower(), DB_NAME_OPERATION_CODE);
                map.put(PROPERTY_NAME_CONDITION_STRING.ToLower(), DB_NAME_CONDITION_STRING);
                map.put(PROPERTY_NAME_DATA_EDIT_ID.ToLower(), DB_NAME_DATA_EDIT_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TIntegCondition"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TIntegConditionDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TIntegConditionCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TIntegConditionBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TIntegCondition NewMyEntity() { return new TIntegCondition(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TIntegConditionCB NewMyConditionBean() { return new TIntegConditionCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TIntegCondition>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TIntegCondition>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("INTEG_CONDITION_ID", "IntegConditionId", new EntityPropertyIntegConditionIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_NO", "ConditionNo", new EntityPropertyConditionNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SRC_ITEM_ID", "SrcItemId", new EntityPropertySrcItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SOURCE_ITEM_NO", "SourceItemNo", new EntityPropertySourceItemNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OPERATION_CODE", "OperationCode", new EntityPropertyOperationCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_STRING", "ConditionString", new EntityPropertyConditionStringSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TIntegCondition> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TIntegCondition)entity, value);
        }

        public class EntityPropertyIntegConditionIdSetupper : EntityPropertySetupper<TIntegCondition> {
            public void Setup(TIntegCondition entity, Object value) { entity.IntegConditionId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyConditionNoSetupper : EntityPropertySetupper<TIntegCondition> {
            public void Setup(TIntegCondition entity, Object value) { entity.ConditionNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySrcItemIdSetupper : EntityPropertySetupper<TIntegCondition> {
            public void Setup(TIntegCondition entity, Object value) { entity.SrcItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySourceItemNoSetupper : EntityPropertySetupper<TIntegCondition> {
            public void Setup(TIntegCondition entity, Object value) { entity.SourceItemNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOperationCodeSetupper : EntityPropertySetupper<TIntegCondition> {
            public void Setup(TIntegCondition entity, Object value) { entity.OperationCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyConditionStringSetupper : EntityPropertySetupper<TIntegCondition> {
            public void Setup(TIntegCondition entity, Object value) { entity.ConditionString = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TIntegCondition> {
            public void Setup(TIntegCondition entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
    }
}
