
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

    public class TRawdataDeleteQueDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TRawdataDeleteQue);

        private static readonly TRawdataDeleteQueDbm _instance = new TRawdataDeleteQueDbm();
        private TRawdataDeleteQueDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TRawdataDeleteQueDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_RAWDATA_DELETE_QUE"; } }
        public override String TablePropertyName { get { return "TRawdataDeleteQue"; } }
        public override String TableSqlName { get { return "T_RAWDATA_DELETE_QUE"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnRawdataDeleteQueId;
        protected ColumnInfo _columnAddDataNo;
        protected ColumnInfo _columnQcwebJobNo;
        protected ColumnInfo _columnMainSurveyId;
        protected ColumnInfo _columnDeleteOrderDate;
        protected ColumnInfo _columnDeleteStatus;

        public ColumnInfo ColumnRawdataDeleteQueId { get { return _columnRawdataDeleteQueId; } }
        public ColumnInfo ColumnAddDataNo { get { return _columnAddDataNo; } }
        public ColumnInfo ColumnQcwebJobNo { get { return _columnQcwebJobNo; } }
        public ColumnInfo ColumnMainSurveyId { get { return _columnMainSurveyId; } }
        public ColumnInfo ColumnDeleteOrderDate { get { return _columnDeleteOrderDate; } }
        public ColumnInfo ColumnDeleteStatus { get { return _columnDeleteStatus; } }

        protected void InitializeColumnInfo() {
            _columnRawdataDeleteQueId = cci("RAWDATA_DELETE_QUE_ID", "RAWDATA_DELETE_QUE_ID", null, null, true, "RawdataDeleteQueId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAddDataNo = cci("ADD_DATA_NO", "ADD_DATA_NO", null, null, true, "AddDataNo", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebJobNo = cci("QCWEB_JOB_NO", "QCWEB_JOB_NO", null, null, true, "QcwebJobNo", typeof(String), false, "VARCHAR2", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMainSurveyId = cci("MAIN_SURVEY_ID", "MAIN_SURVEY_ID", null, null, true, "MainSurveyId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDeleteOrderDate = cci("DELETE_ORDER_DATE", "DELETE_ORDER_DATE", null, null, true, "DeleteOrderDate", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnDeleteStatus = cci("DELETE_STATUS", "DELETE_STATUS", null, null, true, "DeleteStatus", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnRawdataDeleteQueId);
            _columnInfoList.add(ColumnAddDataNo);
            _columnInfoList.add(ColumnQcwebJobNo);
            _columnInfoList.add(ColumnMainSurveyId);
            _columnInfoList.add(ColumnDeleteOrderDate);
            _columnInfoList.add(ColumnDeleteStatus);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnRawdataDeleteQueId);
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
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_RawData_Delete_Que_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_RawData_Delete_Que_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_RAWDATA_DELETE_QUE";
        public static readonly String TABLE_PROPERTY_NAME = "TRawdataDeleteQue";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_RAWDATA_DELETE_QUE_ID = "RAWDATA_DELETE_QUE_ID";
        public static readonly String DB_NAME_ADD_DATA_NO = "ADD_DATA_NO";
        public static readonly String DB_NAME_QCWEB_JOB_NO = "QCWEB_JOB_NO";
        public static readonly String DB_NAME_MAIN_SURVEY_ID = "MAIN_SURVEY_ID";
        public static readonly String DB_NAME_DELETE_ORDER_DATE = "DELETE_ORDER_DATE";
        public static readonly String DB_NAME_DELETE_STATUS = "DELETE_STATUS";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_RAWDATA_DELETE_QUE_ID = "RawdataDeleteQueId";
        public static readonly String PROPERTY_NAME_ADD_DATA_NO = "AddDataNo";
        public static readonly String PROPERTY_NAME_QCWEB_JOB_NO = "QcwebJobNo";
        public static readonly String PROPERTY_NAME_MAIN_SURVEY_ID = "MainSurveyId";
        public static readonly String PROPERTY_NAME_DELETE_ORDER_DATE = "DeleteOrderDate";
        public static readonly String PROPERTY_NAME_DELETE_STATUS = "DeleteStatus";

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

        static TRawdataDeleteQueDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_RAWDATA_DELETE_QUE_ID.ToLower(), PROPERTY_NAME_RAWDATA_DELETE_QUE_ID);
                map.put(DB_NAME_ADD_DATA_NO.ToLower(), PROPERTY_NAME_ADD_DATA_NO);
                map.put(DB_NAME_QCWEB_JOB_NO.ToLower(), PROPERTY_NAME_QCWEB_JOB_NO);
                map.put(DB_NAME_MAIN_SURVEY_ID.ToLower(), PROPERTY_NAME_MAIN_SURVEY_ID);
                map.put(DB_NAME_DELETE_ORDER_DATE.ToLower(), PROPERTY_NAME_DELETE_ORDER_DATE);
                map.put(DB_NAME_DELETE_STATUS.ToLower(), PROPERTY_NAME_DELETE_STATUS);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_RAWDATA_DELETE_QUE_ID.ToLower(), DB_NAME_RAWDATA_DELETE_QUE_ID);
                map.put(PROPERTY_NAME_ADD_DATA_NO.ToLower(), DB_NAME_ADD_DATA_NO);
                map.put(PROPERTY_NAME_QCWEB_JOB_NO.ToLower(), DB_NAME_QCWEB_JOB_NO);
                map.put(PROPERTY_NAME_MAIN_SURVEY_ID.ToLower(), DB_NAME_MAIN_SURVEY_ID);
                map.put(PROPERTY_NAME_DELETE_ORDER_DATE.ToLower(), DB_NAME_DELETE_ORDER_DATE);
                map.put(PROPERTY_NAME_DELETE_STATUS.ToLower(), DB_NAME_DELETE_STATUS);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TRawdataDeleteQue"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TRawdataDeleteQueDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TRawdataDeleteQueCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TRawdataDeleteQueBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TRawdataDeleteQue NewMyEntity() { return new TRawdataDeleteQue(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TRawdataDeleteQueCB NewMyConditionBean() { return new TRawdataDeleteQueCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TRawdataDeleteQue>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TRawdataDeleteQue>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("RAWDATA_DELETE_QUE_ID", "RawdataDeleteQueId", new EntityPropertyRawdataDeleteQueIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ADD_DATA_NO", "AddDataNo", new EntityPropertyAddDataNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEB_JOB_NO", "QcwebJobNo", new EntityPropertyQcwebJobNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MAIN_SURVEY_ID", "MainSurveyId", new EntityPropertyMainSurveyIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_ORDER_DATE", "DeleteOrderDate", new EntityPropertyDeleteOrderDateSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_STATUS", "DeleteStatus", new EntityPropertyDeleteStatusSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TRawdataDeleteQue> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TRawdataDeleteQue)entity, value);
        }

        public class EntityPropertyRawdataDeleteQueIdSetupper : EntityPropertySetupper<TRawdataDeleteQue> {
            public void Setup(TRawdataDeleteQue entity, Object value) { entity.RawdataDeleteQueId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAddDataNoSetupper : EntityPropertySetupper<TRawdataDeleteQue> {
            public void Setup(TRawdataDeleteQue entity, Object value) { entity.AddDataNo = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyQcwebJobNoSetupper : EntityPropertySetupper<TRawdataDeleteQue> {
            public void Setup(TRawdataDeleteQue entity, Object value) { entity.QcwebJobNo = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyMainSurveyIdSetupper : EntityPropertySetupper<TRawdataDeleteQue> {
            public void Setup(TRawdataDeleteQue entity, Object value) { entity.MainSurveyId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDeleteOrderDateSetupper : EntityPropertySetupper<TRawdataDeleteQue> {
            public void Setup(TRawdataDeleteQue entity, Object value) { entity.DeleteOrderDate = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyDeleteStatusSetupper : EntityPropertySetupper<TRawdataDeleteQue> {
            public void Setup(TRawdataDeleteQue entity, Object value) { entity.DeleteStatus = (value != null) ? (int?)value : null; }
        }
    }
}
