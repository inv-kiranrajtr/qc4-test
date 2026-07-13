
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

    public class TDeleteDataDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDeleteData);

        private static readonly TDeleteDataDbm _instance = new TDeleteDataDbm();
        private TDeleteDataDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDeleteDataDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DELETE_DATA"; } }
        public override String TablePropertyName { get { return "TDeleteData"; } }
        public override String TableSqlName { get { return "T_DELETE_DATA"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDataEditId;
        protected ColumnInfo _columnDeleteType;
        protected ColumnInfo _columnConditionDiv;

        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }
        public ColumnInfo ColumnDeleteType { get { return _columnDeleteType; } }
        public ColumnInfo ColumnConditionDiv { get { return _columnConditionDiv; } }

        protected void InitializeColumnInfo() {
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, true, "DataEditId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDataEditList", "TDeleteConditionList,TDeleteSampleIdListList");
            _columnDeleteType = cci("DELETE_TYPE", "DELETE_TYPE", null, null, true, "DeleteType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionDiv = cci("CONDITION_DIV", "CONDITION_DIV", null, null, true, "ConditionDiv", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDataEditId);
            _columnInfoList.add(ColumnDeleteType);
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
        public ReferrerInfo ReferrerTDeleteConditionList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDeleteConditionDbm.GetInstance().ColumnDataEditId);
            return cri("TDeleteConditionList", this, TDeleteConditionDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTDeleteSampleIdListList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDeleteSampleIdListDbm.GetInstance().ColumnDataEditId);
            return cri("TDeleteSampleIdListList", this, TDeleteSampleIdListDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Delete_Data_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Delete_Data_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_DELETE_DATA";
        public static readonly String TABLE_PROPERTY_NAME = "TDeleteData";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";
        public static readonly String DB_NAME_DELETE_TYPE = "DELETE_TYPE";
        public static readonly String DB_NAME_CONDITION_DIV = "CONDITION_DIV";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";
        public static readonly String PROPERTY_NAME_DELETE_TYPE = "DeleteType";
        public static readonly String PROPERTY_NAME_CONDITION_DIV = "ConditionDiv";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TDataEditList = "TDataEditList";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TDeleteConditionList = "TDeleteConditionList";
        public static readonly String REFERRER_PROPERTY_NAME_TDeleteSampleIdListList = "TDeleteSampleIdListList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TDeleteDataDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                map.put(DB_NAME_DELETE_TYPE.ToLower(), PROPERTY_NAME_DELETE_TYPE);
                map.put(DB_NAME_CONDITION_DIV.ToLower(), PROPERTY_NAME_CONDITION_DIV);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DATA_EDIT_ID.ToLower(), DB_NAME_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_DELETE_TYPE.ToLower(), DB_NAME_DELETE_TYPE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDeleteData"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDeleteDataDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDeleteDataCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDeleteDataBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDeleteData NewMyEntity() { return new TDeleteData(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDeleteDataCB NewMyConditionBean() { return new TDeleteDataCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDeleteData>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDeleteData>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_TYPE", "DeleteType", new EntityPropertyDeleteTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_DIV", "ConditionDiv", new EntityPropertyConditionDivSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDeleteData> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDeleteData)entity, value);
        }

        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TDeleteData> {
            public void Setup(TDeleteData entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDeleteTypeSetupper : EntityPropertySetupper<TDeleteData> {
            public void Setup(TDeleteData entity, Object value) { entity.DeleteType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyConditionDivSetupper : EntityPropertySetupper<TDeleteData> {
            public void Setup(TDeleteData entity, Object value) { entity.ConditionDiv = (value != null) ? (String)value : null; }
        }
    }
}
