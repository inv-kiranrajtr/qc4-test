
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

    public class TEditConditionDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TEditCondition);

        private static readonly TEditConditionDbm _instance = new TEditConditionDbm();
        private TEditConditionDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TEditConditionDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_EDIT_CONDITION"; } }
        public override String TablePropertyName { get { return "TEditCondition"; } }
        public override String TableSqlName { get { return "T_EDIT_CONDITION"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnEditConditionId;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnItemId;
        protected ColumnInfo _columnOperationCode;
        protected ColumnInfo _columnOperationValue;
        protected ColumnInfo _columnDataEditId;

        public ColumnInfo ColumnEditConditionId { get { return _columnEditConditionId; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnItemId { get { return _columnItemId; } }
        public ColumnInfo ColumnOperationCode { get { return _columnOperationCode; } }
        public ColumnInfo ColumnOperationValue { get { return _columnOperationValue; } }
        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }

        protected void InitializeColumnInfo() {
            _columnEditConditionId = cci("EDIT_CONDITION_ID", "EDIT_CONDITION_ID", null, null, true, "EditConditionId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemId = cci("ITEM_ID", "ITEM_ID", null, null, true, "ItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOperationCode = cci("OPERATION_CODE", "OPERATION_CODE", null, null, true, "OperationCode", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOperationValue = cci("OPERATION_VALUE", "OPERATION_VALUE", null, null, true, "OperationValue", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, true, "DataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TEditData", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnEditConditionId);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnItemId);
            _columnInfoList.add(ColumnOperationCode);
            _columnInfoList.add(ColumnOperationValue);
            _columnInfoList.add(ColumnDataEditId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnEditConditionId);
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
        public ForeignInfo ForeignTEditData { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TEditDataDbm.GetInstance().ColumnDataEditId);
            return cfi("TEditData", this, TEditDataDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Edit_Condition_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Edit_Condition_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_EDIT_CONDITION";
        public static readonly String TABLE_PROPERTY_NAME = "TEditCondition";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_EDIT_CONDITION_ID = "EDIT_CONDITION_ID";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_ITEM_ID = "ITEM_ID";
        public static readonly String DB_NAME_OPERATION_CODE = "OPERATION_CODE";
        public static readonly String DB_NAME_OPERATION_VALUE = "OPERATION_VALUE";
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_EDIT_CONDITION_ID = "EditConditionId";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_ITEM_ID = "ItemId";
        public static readonly String PROPERTY_NAME_OPERATION_CODE = "OperationCode";
        public static readonly String PROPERTY_NAME_OPERATION_VALUE = "OperationValue";
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TEditData = "TEditData";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TEditConditionDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_EDIT_CONDITION_ID.ToLower(), PROPERTY_NAME_EDIT_CONDITION_ID);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_ITEM_ID.ToLower(), PROPERTY_NAME_ITEM_ID);
                map.put(DB_NAME_OPERATION_CODE.ToLower(), PROPERTY_NAME_OPERATION_CODE);
                map.put(DB_NAME_OPERATION_VALUE.ToLower(), PROPERTY_NAME_OPERATION_VALUE);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_EDIT_CONDITION_ID.ToLower(), DB_NAME_EDIT_CONDITION_ID);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_ITEM_ID.ToLower(), DB_NAME_ITEM_ID);
                map.put(PROPERTY_NAME_OPERATION_CODE.ToLower(), DB_NAME_OPERATION_CODE);
                map.put(PROPERTY_NAME_OPERATION_VALUE.ToLower(), DB_NAME_OPERATION_VALUE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TEditCondition"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TEditConditionDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TEditConditionCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TEditConditionBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TEditCondition NewMyEntity() { return new TEditCondition(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TEditConditionCB NewMyConditionBean() { return new TEditConditionCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TEditCondition>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TEditCondition>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("EDIT_CONDITION_ID", "EditConditionId", new EntityPropertyEditConditionIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_ID", "ItemId", new EntityPropertyItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OPERATION_CODE", "OperationCode", new EntityPropertyOperationCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OPERATION_VALUE", "OperationValue", new EntityPropertyOperationValueSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TEditCondition> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TEditCondition)entity, value);
        }

        public class EntityPropertyEditConditionIdSetupper : EntityPropertySetupper<TEditCondition> {
            public void Setup(TEditCondition entity, Object value) { entity.EditConditionId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TEditCondition> {
            public void Setup(TEditCondition entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyItemIdSetupper : EntityPropertySetupper<TEditCondition> {
            public void Setup(TEditCondition entity, Object value) { entity.ItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOperationCodeSetupper : EntityPropertySetupper<TEditCondition> {
            public void Setup(TEditCondition entity, Object value) { entity.OperationCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOperationValueSetupper : EntityPropertySetupper<TEditCondition> {
            public void Setup(TEditCondition entity, Object value) { entity.OperationValue = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TEditCondition> {
            public void Setup(TEditCondition entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
    }
}
