
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

    public class TDataEditListDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDataEditList);

        private static readonly TDataEditListDbm _instance = new TDataEditListDbm();
        private TDataEditListDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDataEditListDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_EDIT_LIST"; } }
        public override String TablePropertyName { get { return "TDataEditList"; } }
        public override String TableSqlName { get { return "T_DATA_EDIT_LIST"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDataEditId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnExecuteNo;
        protected ColumnInfo _columnExecuteFlag;
        protected ColumnInfo _columnEditMenuMasterId;
        protected ColumnInfo _columnDescription;
        protected ColumnInfo _columnConditionItemViewName;
        protected ColumnInfo _columnTargetItemViewName;
        protected ColumnInfo _columnStatus;
        protected ColumnInfo _columnLatestFlag;
        protected ColumnInfo _columnDerivedDataEditId;
        protected ColumnInfo _columnDeleteReserveFlag;
        protected ColumnInfo _columnLastUpdateUser;
        protected ColumnInfo _columnLastUpdateDatetime;
        protected ColumnInfo _columnEditFlag;

        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnExecuteNo { get { return _columnExecuteNo; } }
        public ColumnInfo ColumnExecuteFlag { get { return _columnExecuteFlag; } }
        public ColumnInfo ColumnEditMenuMasterId { get { return _columnEditMenuMasterId; } }
        public ColumnInfo ColumnDescription { get { return _columnDescription; } }
        public ColumnInfo ColumnConditionItemViewName { get { return _columnConditionItemViewName; } }
        public ColumnInfo ColumnTargetItemViewName { get { return _columnTargetItemViewName; } }
        public ColumnInfo ColumnStatus { get { return _columnStatus; } }
        public ColumnInfo ColumnLatestFlag { get { return _columnLatestFlag; } }
        public ColumnInfo ColumnDerivedDataEditId { get { return _columnDerivedDataEditId; } }
        public ColumnInfo ColumnDeleteReserveFlag { get { return _columnDeleteReserveFlag; } }
        public ColumnInfo ColumnLastUpdateUser { get { return _columnLastUpdateUser; } }
        public ColumnInfo ColumnLastUpdateDatetime { get { return _columnLastUpdateDatetime; } }
        public ColumnInfo ColumnEditFlag { get { return _columnEditFlag; } }

        protected void InitializeColumnInfo() {
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, true, "DataEditId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TItemInfoList");
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo", null);
            _columnExecuteNo = cci("EXECUTE_NO", "EXECUTE_NO", null, null, true, "ExecuteNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExecuteFlag = cci("EXECUTE_FLAG", "EXECUTE_FLAG", null, null, true, "ExecuteFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnEditMenuMasterId = cci("EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", null, null, true, "EditMenuMasterId", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, "TEditMenuMaster", null);
            _columnDescription = cci("DESCRIPTION", "DESCRIPTION", null, null, false, "Description", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionItemViewName = cci("CONDITION_ITEM_VIEW_NAME", "CONDITION_ITEM_VIEW_NAME", null, null, false, "ConditionItemViewName", typeof(String), false, "NVARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTargetItemViewName = cci("TARGET_ITEM_VIEW_NAME", "TARGET_ITEM_VIEW_NAME", null, null, false, "TargetItemViewName", typeof(String), false, "NVARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnStatus = cci("STATUS", "STATUS", null, null, true, "Status", typeof(String), false, "VARCHAR2", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLatestFlag = cci("LATEST_FLAG", "LATEST_FLAG", null, null, true, "LatestFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDerivedDataEditId = cci("DERIVED_DATA_EDIT_ID", "DERIVED_DATA_EDIT_ID", null, null, false, "DerivedDataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDeleteReserveFlag = cci("DELETE_RESERVE_FLAG", "DELETE_RESERVE_FLAG", null, null, true, "DeleteReserveFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLastUpdateUser = cci("LAST_UPDATE_USER", "LAST_UPDATE_USER", null, null, false, "LastUpdateUser", typeof(String), false, "VARCHAR2", 1000, 0, true, OptimisticLockType.NONE, null, null, null);
            _columnLastUpdateDatetime = cci("LAST_UPDATE_DATETIME", "LAST_UPDATE_DATETIME", null, null, false, "LastUpdateDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, true, OptimisticLockType.NONE, null, null, null);
            _columnEditFlag = cci("EDIT_FLAG", "EDIT_FLAG", null, null, true, "EditFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDataEditId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnExecuteNo);
            _columnInfoList.add(ColumnExecuteFlag);
            _columnInfoList.add(ColumnEditMenuMasterId);
            _columnInfoList.add(ColumnDescription);
            _columnInfoList.add(ColumnConditionItemViewName);
            _columnInfoList.add(ColumnTargetItemViewName);
            _columnInfoList.add(ColumnStatus);
            _columnInfoList.add(ColumnLatestFlag);
            _columnInfoList.add(ColumnDerivedDataEditId);
            _columnInfoList.add(ColumnDeleteReserveFlag);
            _columnInfoList.add(ColumnLastUpdateUser);
            _columnInfoList.add(ColumnLastUpdateDatetime);
            _columnInfoList.add(ColumnEditFlag);
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
        public ForeignInfo ForeignTQcwebSurveyInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfo", this, TQcwebSurveyInfoDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTEditMenuMaster { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnEditMenuMasterId, TEditMenuMasterDbm.GetInstance().ColumnEditMenuMasterId);
            return cfi("TEditMenuMaster", this, TEditMenuMasterDbm.GetInstance(), map, 1, false, false);
        }}

        public ForeignInfo ForeignTDataProcessNewItemAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDataProcessNewItemDbm.GetInstance().ColumnDataEditId);
            return cfi("TDataProcessNewItemAsOne", this, TDataProcessNewItemDbm.GetInstance(), map, 2, true, false);
        }}
        public ForeignInfo ForeignTDeleteDataAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDeleteDataDbm.GetInstance().ColumnDataEditId);
            return cfi("TDeleteDataAsOne", this, TDeleteDataDbm.GetInstance(), map, 3, true, false);
        }}
        public ForeignInfo ForeignTEditDataAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TEditDataDbm.GetInstance().ColumnDataEditId);
            return cfi("TEditDataAsOne", this, TEditDataDbm.GetInstance(), map, 4, true, false);
        }}

        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTItemInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TItemInfoDbm.GetInstance().ColumnDataEditId);
            return cri("TItemInfoList", this, TItemInfoDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Data_Edit_List_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Data_Edit_List_SEQ_01.nextval from dual"; } }
        public override int? SequenceIncrementSize { get { return 1; } }
        public override int? SequenceCacheSize { get { return null; } }
        public override bool HasCommonColumn { get { return true; } }

        // ===============================================================================
        //                                                                 Name Definition
        //                                                                 ===============
        #region Name

        // -------------------------------------------------
        //                                             Table
        //                                             -----
        public static readonly String TABLE_DB_NAME = "T_DATA_EDIT_LIST";
        public static readonly String TABLE_PROPERTY_NAME = "TDataEditList";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_EXECUTE_NO = "EXECUTE_NO";
        public static readonly String DB_NAME_EXECUTE_FLAG = "EXECUTE_FLAG";
        public static readonly String DB_NAME_EDIT_MENU_MASTER_ID = "EDIT_MENU_MASTER_ID";
        public static readonly String DB_NAME_DESCRIPTION = "DESCRIPTION";
        public static readonly String DB_NAME_CONDITION_ITEM_VIEW_NAME = "CONDITION_ITEM_VIEW_NAME";
        public static readonly String DB_NAME_TARGET_ITEM_VIEW_NAME = "TARGET_ITEM_VIEW_NAME";
        public static readonly String DB_NAME_STATUS = "STATUS";
        public static readonly String DB_NAME_LATEST_FLAG = "LATEST_FLAG";
        public static readonly String DB_NAME_DERIVED_DATA_EDIT_ID = "DERIVED_DATA_EDIT_ID";
        public static readonly String DB_NAME_DELETE_RESERVE_FLAG = "DELETE_RESERVE_FLAG";
        public static readonly String DB_NAME_LAST_UPDATE_USER = "LAST_UPDATE_USER";
        public static readonly String DB_NAME_LAST_UPDATE_DATETIME = "LAST_UPDATE_DATETIME";
        public static readonly String DB_NAME_EDIT_FLAG = "EDIT_FLAG";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_EXECUTE_NO = "ExecuteNo";
        public static readonly String PROPERTY_NAME_EXECUTE_FLAG = "ExecuteFlag";
        public static readonly String PROPERTY_NAME_EDIT_MENU_MASTER_ID = "EditMenuMasterId";
        public static readonly String PROPERTY_NAME_DESCRIPTION = "Description";
        public static readonly String PROPERTY_NAME_CONDITION_ITEM_VIEW_NAME = "ConditionItemViewName";
        public static readonly String PROPERTY_NAME_TARGET_ITEM_VIEW_NAME = "TargetItemViewName";
        public static readonly String PROPERTY_NAME_STATUS = "Status";
        public static readonly String PROPERTY_NAME_LATEST_FLAG = "LatestFlag";
        public static readonly String PROPERTY_NAME_DERIVED_DATA_EDIT_ID = "DerivedDataEditId";
        public static readonly String PROPERTY_NAME_DELETE_RESERVE_FLAG = "DeleteReserveFlag";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_USER = "LastUpdateUser";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_DATETIME = "LastUpdateDatetime";
        public static readonly String PROPERTY_NAME_EDIT_FLAG = "EditFlag";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TEditMenuMaster = "TEditMenuMaster";
        public static readonly String FOREIGN_PROPERTY_NAME_TDataProcessNewItemAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        public static readonly String FOREIGN_PROPERTY_NAME_TDeleteDataAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        public static readonly String FOREIGN_PROPERTY_NAME_TEditDataAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TItemInfoList = "TItemInfoList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TDataEditListDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_EXECUTE_NO.ToLower(), PROPERTY_NAME_EXECUTE_NO);
                map.put(DB_NAME_EXECUTE_FLAG.ToLower(), PROPERTY_NAME_EXECUTE_FLAG);
                map.put(DB_NAME_EDIT_MENU_MASTER_ID.ToLower(), PROPERTY_NAME_EDIT_MENU_MASTER_ID);
                map.put(DB_NAME_DESCRIPTION.ToLower(), PROPERTY_NAME_DESCRIPTION);
                map.put(DB_NAME_CONDITION_ITEM_VIEW_NAME.ToLower(), PROPERTY_NAME_CONDITION_ITEM_VIEW_NAME);
                map.put(DB_NAME_TARGET_ITEM_VIEW_NAME.ToLower(), PROPERTY_NAME_TARGET_ITEM_VIEW_NAME);
                map.put(DB_NAME_STATUS.ToLower(), PROPERTY_NAME_STATUS);
                map.put(DB_NAME_LATEST_FLAG.ToLower(), PROPERTY_NAME_LATEST_FLAG);
                map.put(DB_NAME_DERIVED_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DERIVED_DATA_EDIT_ID);
                map.put(DB_NAME_DELETE_RESERVE_FLAG.ToLower(), PROPERTY_NAME_DELETE_RESERVE_FLAG);
                map.put(DB_NAME_LAST_UPDATE_USER.ToLower(), PROPERTY_NAME_LAST_UPDATE_USER);
                map.put(DB_NAME_LAST_UPDATE_DATETIME.ToLower(), PROPERTY_NAME_LAST_UPDATE_DATETIME);
                map.put(DB_NAME_EDIT_FLAG.ToLower(), PROPERTY_NAME_EDIT_FLAG);
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
                map.put(PROPERTY_NAME_DESCRIPTION.ToLower(), DB_NAME_DESCRIPTION);
                map.put(PROPERTY_NAME_CONDITION_ITEM_VIEW_NAME.ToLower(), DB_NAME_CONDITION_ITEM_VIEW_NAME);
                map.put(PROPERTY_NAME_TARGET_ITEM_VIEW_NAME.ToLower(), DB_NAME_TARGET_ITEM_VIEW_NAME);
                map.put(PROPERTY_NAME_STATUS.ToLower(), DB_NAME_STATUS);
                map.put(PROPERTY_NAME_LATEST_FLAG.ToLower(), DB_NAME_LATEST_FLAG);
                map.put(PROPERTY_NAME_DERIVED_DATA_EDIT_ID.ToLower(), DB_NAME_DERIVED_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_DELETE_RESERVE_FLAG.ToLower(), DB_NAME_DELETE_RESERVE_FLAG);
                map.put(PROPERTY_NAME_LAST_UPDATE_USER.ToLower(), DB_NAME_LAST_UPDATE_USER);
                map.put(PROPERTY_NAME_LAST_UPDATE_DATETIME.ToLower(), DB_NAME_LAST_UPDATE_DATETIME);
                map.put(PROPERTY_NAME_EDIT_FLAG.ToLower(), DB_NAME_EDIT_FLAG);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDataEditList"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDataEditListDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDataEditListCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDataEditListBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDataEditList NewMyEntity() { return new TDataEditList(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDataEditListCB NewMyConditionBean() { return new TDataEditListCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDataEditList>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDataEditList>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXECUTE_NO", "ExecuteNo", new EntityPropertyExecuteNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXECUTE_FLAG", "ExecuteFlag", new EntityPropertyExecuteFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EDIT_MENU_MASTER_ID", "EditMenuMasterId", new EntityPropertyEditMenuMasterIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DESCRIPTION", "Description", new EntityPropertyDescriptionSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_ITEM_VIEW_NAME", "ConditionItemViewName", new EntityPropertyConditionItemViewNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TARGET_ITEM_VIEW_NAME", "TargetItemViewName", new EntityPropertyTargetItemViewNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("STATUS", "Status", new EntityPropertyStatusSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LATEST_FLAG", "LatestFlag", new EntityPropertyLatestFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DERIVED_DATA_EDIT_ID", "DerivedDataEditId", new EntityPropertyDerivedDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_RESERVE_FLAG", "DeleteReserveFlag", new EntityPropertyDeleteReserveFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_USER", "LastUpdateUser", new EntityPropertyLastUpdateUserSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_DATETIME", "LastUpdateDatetime", new EntityPropertyLastUpdateDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EDIT_FLAG", "EditFlag", new EntityPropertyEditFlagSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDataEditList> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDataEditList)entity, value);
        }

        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyExecuteNoSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.ExecuteNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyExecuteFlagSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.ExecuteFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyEditMenuMasterIdSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.EditMenuMasterId = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDescriptionSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.Description = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyConditionItemViewNameSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.ConditionItemViewName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyTargetItemViewNameSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.TargetItemViewName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyStatusSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.Status = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLatestFlagSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.LatestFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDerivedDataEditIdSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.DerivedDataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDeleteReserveFlagSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.DeleteReserveFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLastUpdateUserSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.LastUpdateUser = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLastUpdateDatetimeSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.LastUpdateDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyEditFlagSetupper : EntityPropertySetupper<TDataEditList> {
            public void Setup(TDataEditList entity, Object value) { entity.EditFlag = (value != null) ? (int?)value : null; }
        }
    }
}
