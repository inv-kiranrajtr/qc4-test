
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class TDataEditListSelectDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDataEditListSelect);

        private static readonly TDataEditListSelectDbm _instance = new TDataEditListSelectDbm();
        private TDataEditListSelectDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDataEditListSelectDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "TDataEditListSelect"; } }
        public override String TablePropertyName { get { return "TDataEditListSelect"; } }
        public override String TableSqlName { get { return "TDataEditListSelect"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDataEditId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnExecuteNo;
        protected ColumnInfo _columnExecuteFlag;
        protected ColumnInfo _columnEditMenuMasterId;
        protected ColumnInfo _columnConditionItemViewName;
        protected ColumnInfo _columnTargetItemViewName;
        protected ColumnInfo _columnStatus;
        protected ColumnInfo _columnLatestFlag;
        protected ColumnInfo _columnDerivedDataEditId;
        protected ColumnInfo _columnDeleteReserveFlag;
        protected ColumnInfo _columnDescription;
        protected ColumnInfo _columnEditFlag;
        protected ColumnInfo _columnTargetCsv;
        protected ColumnInfo _columnConditionCsv;
        protected ColumnInfo _columnFormulaString;

        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnExecuteNo { get { return _columnExecuteNo; } }
        public ColumnInfo ColumnExecuteFlag { get { return _columnExecuteFlag; } }
        public ColumnInfo ColumnEditMenuMasterId { get { return _columnEditMenuMasterId; } }
        public ColumnInfo ColumnConditionItemViewName { get { return _columnConditionItemViewName; } }
        public ColumnInfo ColumnTargetItemViewName { get { return _columnTargetItemViewName; } }
        public ColumnInfo ColumnStatus { get { return _columnStatus; } }
        public ColumnInfo ColumnLatestFlag { get { return _columnLatestFlag; } }
        public ColumnInfo ColumnDerivedDataEditId { get { return _columnDerivedDataEditId; } }
        public ColumnInfo ColumnDeleteReserveFlag { get { return _columnDeleteReserveFlag; } }
        public ColumnInfo ColumnDescription { get { return _columnDescription; } }
        public ColumnInfo ColumnEditFlag { get { return _columnEditFlag; } }
        public ColumnInfo ColumnTargetCsv { get { return _columnTargetCsv; } }
        public ColumnInfo ColumnConditionCsv { get { return _columnConditionCsv; } }
        public ColumnInfo ColumnFormulaString { get { return _columnFormulaString; } }

        protected void InitializeColumnInfo() {
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, false, "DataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, false, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExecuteNo = cci("EXECUTE_NO", "EXECUTE_NO", null, null, false, "ExecuteNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExecuteFlag = cci("EXECUTE_FLAG", "EXECUTE_FLAG", null, null, false, "ExecuteFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnEditMenuMasterId = cci("EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", null, null, false, "EditMenuMasterId", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionItemViewName = cci("CONDITION_ITEM_VIEW_NAME", "CONDITION_ITEM_VIEW_NAME", null, null, false, "ConditionItemViewName", typeof(String), false, "VARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTargetItemViewName = cci("TARGET_ITEM_VIEW_NAME", "TARGET_ITEM_VIEW_NAME", null, null, false, "TargetItemViewName", typeof(String), false, "VARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnStatus = cci("STATUS", "STATUS", null, null, false, "Status", typeof(String), false, "VARCHAR2", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLatestFlag = cci("LATEST_FLAG", "LATEST_FLAG", null, null, false, "LatestFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDerivedDataEditId = cci("DERIVED_DATA_EDIT_ID", "DERIVED_DATA_EDIT_ID", null, null, false, "DerivedDataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDeleteReserveFlag = cci("DELETE_RESERVE_FLAG", "DELETE_RESERVE_FLAG", null, null, false, "DeleteReserveFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDescription = cci("DESCRIPTION", "DESCRIPTION", null, null, false, "Description", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnEditFlag = cci("EDIT_FLAG", "EDIT_FLAG", null, null, false, "EditFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTargetCsv = cci("TARGET_CSV", "TARGET_CSV", null, null, false, "TargetCsv", typeof(String), false, "VARCHAR2", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionCsv = cci("CONDITION_CSV", "CONDITION_CSV", null, null, false, "ConditionCsv", typeof(String), false, "VARCHAR2", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFormulaString = cci("FORMULA_STRING", "FORMULA_STRING", null, null, false, "FormulaString", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDataEditId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnExecuteNo);
            _columnInfoList.add(ColumnExecuteFlag);
            _columnInfoList.add(ColumnEditMenuMasterId);
            _columnInfoList.add(ColumnConditionItemViewName);
            _columnInfoList.add(ColumnTargetItemViewName);
            _columnInfoList.add(ColumnStatus);
            _columnInfoList.add(ColumnLatestFlag);
            _columnInfoList.add(ColumnDerivedDataEditId);
            _columnInfoList.add(ColumnDeleteReserveFlag);
            _columnInfoList.add(ColumnDescription);
            _columnInfoList.add(ColumnEditFlag);
            _columnInfoList.add(ColumnTargetCsv);
            _columnInfoList.add(ColumnConditionCsv);
            _columnInfoList.add(ColumnFormulaString);
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
        public static readonly String TABLE_DB_NAME = "TDataEditListSelect";
        public static readonly String TABLE_PROPERTY_NAME = "TDataEditListSelect";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_EXECUTE_NO = "EXECUTE_NO";
        public static readonly String DB_NAME_EXECUTE_FLAG = "EXECUTE_FLAG";
        public static readonly String DB_NAME_EDIT_MENU_MASTER_ID = "EDIT_MENU_MASTER_ID";
        public static readonly String DB_NAME_CONDITION_ITEM_VIEW_NAME = "CONDITION_ITEM_VIEW_NAME";
        public static readonly String DB_NAME_TARGET_ITEM_VIEW_NAME = "TARGET_ITEM_VIEW_NAME";
        public static readonly String DB_NAME_STATUS = "STATUS";
        public static readonly String DB_NAME_LATEST_FLAG = "LATEST_FLAG";
        public static readonly String DB_NAME_DERIVED_DATA_EDIT_ID = "DERIVED_DATA_EDIT_ID";
        public static readonly String DB_NAME_DELETE_RESERVE_FLAG = "DELETE_RESERVE_FLAG";
        public static readonly String DB_NAME_DESCRIPTION = "DESCRIPTION";
        public static readonly String DB_NAME_EDIT_FLAG = "EDIT_FLAG";
        public static readonly String DB_NAME_TARGET_CSV = "TARGET_CSV";
        public static readonly String DB_NAME_CONDITION_CSV = "CONDITION_CSV";
        public static readonly String DB_NAME_FORMULA_STRING = "FORMULA_STRING";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_EXECUTE_NO = "ExecuteNo";
        public static readonly String PROPERTY_NAME_EXECUTE_FLAG = "ExecuteFlag";
        public static readonly String PROPERTY_NAME_EDIT_MENU_MASTER_ID = "EditMenuMasterId";
        public static readonly String PROPERTY_NAME_CONDITION_ITEM_VIEW_NAME = "ConditionItemViewName";
        public static readonly String PROPERTY_NAME_TARGET_ITEM_VIEW_NAME = "TargetItemViewName";
        public static readonly String PROPERTY_NAME_STATUS = "Status";
        public static readonly String PROPERTY_NAME_LATEST_FLAG = "LatestFlag";
        public static readonly String PROPERTY_NAME_DERIVED_DATA_EDIT_ID = "DerivedDataEditId";
        public static readonly String PROPERTY_NAME_DELETE_RESERVE_FLAG = "DeleteReserveFlag";
        public static readonly String PROPERTY_NAME_DESCRIPTION = "Description";
        public static readonly String PROPERTY_NAME_EDIT_FLAG = "EditFlag";
        public static readonly String PROPERTY_NAME_TARGET_CSV = "TargetCsv";
        public static readonly String PROPERTY_NAME_CONDITION_CSV = "ConditionCsv";
        public static readonly String PROPERTY_NAME_FORMULA_STRING = "FormulaString";

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

        static TDataEditListSelectDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_EXECUTE_NO.ToLower(), PROPERTY_NAME_EXECUTE_NO);
                map.put(DB_NAME_EXECUTE_FLAG.ToLower(), PROPERTY_NAME_EXECUTE_FLAG);
                map.put(DB_NAME_EDIT_MENU_MASTER_ID.ToLower(), PROPERTY_NAME_EDIT_MENU_MASTER_ID);
                map.put(DB_NAME_CONDITION_ITEM_VIEW_NAME.ToLower(), PROPERTY_NAME_CONDITION_ITEM_VIEW_NAME);
                map.put(DB_NAME_TARGET_ITEM_VIEW_NAME.ToLower(), PROPERTY_NAME_TARGET_ITEM_VIEW_NAME);
                map.put(DB_NAME_STATUS.ToLower(), PROPERTY_NAME_STATUS);
                map.put(DB_NAME_LATEST_FLAG.ToLower(), PROPERTY_NAME_LATEST_FLAG);
                map.put(DB_NAME_DERIVED_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DERIVED_DATA_EDIT_ID);
                map.put(DB_NAME_DELETE_RESERVE_FLAG.ToLower(), PROPERTY_NAME_DELETE_RESERVE_FLAG);
                map.put(DB_NAME_DESCRIPTION.ToLower(), PROPERTY_NAME_DESCRIPTION);
                map.put(DB_NAME_EDIT_FLAG.ToLower(), PROPERTY_NAME_EDIT_FLAG);
                map.put(DB_NAME_TARGET_CSV.ToLower(), PROPERTY_NAME_TARGET_CSV);
                map.put(DB_NAME_CONDITION_CSV.ToLower(), PROPERTY_NAME_CONDITION_CSV);
                map.put(DB_NAME_FORMULA_STRING.ToLower(), PROPERTY_NAME_FORMULA_STRING);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DATA_EDIT_ID.ToLower(), DB_NAME_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_EXECUTE_NO.ToLower(), DB_NAME_EXECUTE_NO);
                map.put(PROPERTY_NAME_EXECUTE_FLAG.ToLower(), DB_NAME_EXECUTE_FLAG);
                map.put(PROPERTY_NAME_EDIT_MENU_MASTER_ID.ToLower(), DB_NAME_EDIT_MENU_MASTER_ID);
                map.put(PROPERTY_NAME_CONDITION_ITEM_VIEW_NAME.ToLower(), DB_NAME_CONDITION_ITEM_VIEW_NAME);
                map.put(PROPERTY_NAME_TARGET_ITEM_VIEW_NAME.ToLower(), DB_NAME_TARGET_ITEM_VIEW_NAME);
                map.put(PROPERTY_NAME_STATUS.ToLower(), DB_NAME_STATUS);
                map.put(PROPERTY_NAME_LATEST_FLAG.ToLower(), DB_NAME_LATEST_FLAG);
                map.put(PROPERTY_NAME_DERIVED_DATA_EDIT_ID.ToLower(), DB_NAME_DERIVED_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_DELETE_RESERVE_FLAG.ToLower(), DB_NAME_DELETE_RESERVE_FLAG);
                map.put(PROPERTY_NAME_DESCRIPTION.ToLower(), DB_NAME_DESCRIPTION);
                map.put(PROPERTY_NAME_EDIT_FLAG.ToLower(), DB_NAME_EDIT_FLAG);
                map.put(PROPERTY_NAME_TARGET_CSV.ToLower(), DB_NAME_TARGET_CSV);
                map.put(PROPERTY_NAME_CONDITION_CSV.ToLower(), DB_NAME_CONDITION_CSV);
                map.put(PROPERTY_NAME_FORMULA_STRING.ToLower(), DB_NAME_FORMULA_STRING);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.TDataEditListSelect"; } }
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
        public TDataEditListSelect NewMyEntity() { return new TDataEditListSelect(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDataEditListSelect>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDataEditListSelect>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXECUTE_NO", "ExecuteNo", new EntityPropertyExecuteNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXECUTE_FLAG", "ExecuteFlag", new EntityPropertyExecuteFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EDIT_MENU_MASTER_ID", "EditMenuMasterId", new EntityPropertyEditMenuMasterIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_ITEM_VIEW_NAME", "ConditionItemViewName", new EntityPropertyConditionItemViewNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TARGET_ITEM_VIEW_NAME", "TargetItemViewName", new EntityPropertyTargetItemViewNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("STATUS", "Status", new EntityPropertyStatusSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LATEST_FLAG", "LatestFlag", new EntityPropertyLatestFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DERIVED_DATA_EDIT_ID", "DerivedDataEditId", new EntityPropertyDerivedDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_RESERVE_FLAG", "DeleteReserveFlag", new EntityPropertyDeleteReserveFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DESCRIPTION", "Description", new EntityPropertyDescriptionSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EDIT_FLAG", "EditFlag", new EntityPropertyEditFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TARGET_CSV", "TargetCsv", new EntityPropertyTargetCsvSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_CSV", "ConditionCsv", new EntityPropertyConditionCsvSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FORMULA_STRING", "FormulaString", new EntityPropertyFormulaStringSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDataEditListSelect> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDataEditListSelect)entity, value);
        }

        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyExecuteNoSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.ExecuteNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyExecuteFlagSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.ExecuteFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyEditMenuMasterIdSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.EditMenuMasterId = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyConditionItemViewNameSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.ConditionItemViewName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyTargetItemViewNameSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.TargetItemViewName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyStatusSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.Status = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLatestFlagSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.LatestFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDerivedDataEditIdSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.DerivedDataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDeleteReserveFlagSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.DeleteReserveFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDescriptionSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.Description = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyEditFlagSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.EditFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTargetCsvSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.TargetCsv = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyConditionCsvSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.ConditionCsv = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFormulaStringSetupper : EntityPropertySetupper<TDataEditListSelect> {
            public void Setup(TDataEditListSelect entity, Object value) { entity.FormulaString = (value != null) ? (String)value : null; }
        }
    }
}
