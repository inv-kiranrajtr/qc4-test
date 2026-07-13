
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

    public class TDeleteSampleIdListDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDeleteSampleIdList);

        private static readonly TDeleteSampleIdListDbm _instance = new TDeleteSampleIdListDbm();
        private TDeleteSampleIdListDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDeleteSampleIdListDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DELETE_SAMPLE_ID_LIST"; } }
        public override String TablePropertyName { get { return "TDeleteSampleIdList"; } }
        public override String TableSqlName { get { return "T_DELETE_SAMPLE_ID_LIST"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDeleteSampleId;
        protected ColumnInfo _columnDataEditId;
        protected ColumnInfo _columnDeleteSampleIdText;

        public ColumnInfo ColumnDeleteSampleId { get { return _columnDeleteSampleId; } }
        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }
        public ColumnInfo ColumnDeleteSampleIdText { get { return _columnDeleteSampleIdText; } }

        protected void InitializeColumnInfo() {
            _columnDeleteSampleId = cci("DELETE_SAMPLE_ID", "DELETE_SAMPLE_ID", null, null, true, "DeleteSampleId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, true, "DataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDeleteData", null);
            _columnDeleteSampleIdText = cci("DELETE_SAMPLE_ID_TEXT", "DELETE_SAMPLE_ID_TEXT", null, null, false, "DeleteSampleIdText", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDeleteSampleId);
            _columnInfoList.add(ColumnDataEditId);
            _columnInfoList.add(ColumnDeleteSampleIdText);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnDeleteSampleId);
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
        public ForeignInfo ForeignTDeleteData { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDeleteDataDbm.GetInstance().ColumnDataEditId);
            return cfi("TDeleteData", this, TDeleteDataDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Delete_Sample_ID_List_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Delete_Sample_ID_List_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_DELETE_SAMPLE_ID_LIST";
        public static readonly String TABLE_PROPERTY_NAME = "TDeleteSampleIdList";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DELETE_SAMPLE_ID = "DELETE_SAMPLE_ID";
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";
        public static readonly String DB_NAME_DELETE_SAMPLE_ID_TEXT = "DELETE_SAMPLE_ID_TEXT";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DELETE_SAMPLE_ID = "DeleteSampleId";
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";
        public static readonly String PROPERTY_NAME_DELETE_SAMPLE_ID_TEXT = "DeleteSampleIdText";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TDeleteData = "TDeleteData";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TDeleteSampleIdListDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DELETE_SAMPLE_ID.ToLower(), PROPERTY_NAME_DELETE_SAMPLE_ID);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                map.put(DB_NAME_DELETE_SAMPLE_ID_TEXT.ToLower(), PROPERTY_NAME_DELETE_SAMPLE_ID_TEXT);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DELETE_SAMPLE_ID.ToLower(), DB_NAME_DELETE_SAMPLE_ID);
                map.put(PROPERTY_NAME_DATA_EDIT_ID.ToLower(), DB_NAME_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_DELETE_SAMPLE_ID_TEXT.ToLower(), DB_NAME_DELETE_SAMPLE_ID_TEXT);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDeleteSampleIdList"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDeleteSampleIdListDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDeleteSampleIdListCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDeleteSampleIdListBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDeleteSampleIdList NewMyEntity() { return new TDeleteSampleIdList(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDeleteSampleIdListCB NewMyConditionBean() { return new TDeleteSampleIdListCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDeleteSampleIdList>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDeleteSampleIdList>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DELETE_SAMPLE_ID", "DeleteSampleId", new EntityPropertyDeleteSampleIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_SAMPLE_ID_TEXT", "DeleteSampleIdText", new EntityPropertyDeleteSampleIdTextSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDeleteSampleIdList> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDeleteSampleIdList)entity, value);
        }

        public class EntityPropertyDeleteSampleIdSetupper : EntityPropertySetupper<TDeleteSampleIdList> {
            public void Setup(TDeleteSampleIdList entity, Object value) { entity.DeleteSampleId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TDeleteSampleIdList> {
            public void Setup(TDeleteSampleIdList entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDeleteSampleIdTextSetupper : EntityPropertySetupper<TDeleteSampleIdList> {
            public void Setup(TDeleteSampleIdList entity, Object value) { entity.DeleteSampleIdText = (value != null) ? (String)value : null; }
        }
    }
}
