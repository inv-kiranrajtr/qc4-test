
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

    public class TRawdataImportQueInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TRawdataImportQueInfo);

        private static readonly TRawdataImportQueInfoDbm _instance = new TRawdataImportQueInfoDbm();
        private TRawdataImportQueInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TRawdataImportQueInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_RAWDATA_IMPORT_QUE_INFO"; } }
        public override String TablePropertyName { get { return "TRawdataImportQueInfo"; } }
        public override String TableSqlName { get { return "T_RAWDATA_IMPORT_QUE_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnRawdataImportQueInfoId;
        protected ColumnInfo _columnQcwebJobNo;
        protected ColumnInfo _columnMainSurveyId;
        protected ColumnInfo _columnSurveyDataType;
        protected ColumnInfo _columnFilepath;
        protected ColumnInfo _columnFileName;
        protected ColumnInfo _columnImportStatus;
        protected ColumnInfo _columnMessage;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnAddDataNo;
        protected ColumnInfo _columnRequestDatetime;

        public ColumnInfo ColumnRawdataImportQueInfoId { get { return _columnRawdataImportQueInfoId; } }
        public ColumnInfo ColumnQcwebJobNo { get { return _columnQcwebJobNo; } }
        public ColumnInfo ColumnMainSurveyId { get { return _columnMainSurveyId; } }
        public ColumnInfo ColumnSurveyDataType { get { return _columnSurveyDataType; } }
        public ColumnInfo ColumnFilepath { get { return _columnFilepath; } }
        public ColumnInfo ColumnFileName { get { return _columnFileName; } }
        public ColumnInfo ColumnImportStatus { get { return _columnImportStatus; } }
        public ColumnInfo ColumnMessage { get { return _columnMessage; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnAddDataNo { get { return _columnAddDataNo; } }
        public ColumnInfo ColumnRequestDatetime { get { return _columnRequestDatetime; } }

        protected void InitializeColumnInfo() {
            _columnRawdataImportQueInfoId = cci("RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", null, null, true, "RawdataImportQueInfoId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TQcwebSurveyInfoList");
            _columnQcwebJobNo = cci("QCWEB_JOB_NO", "QCWEB_JOB_NO", null, null, true, "QcwebJobNo", typeof(String), false, "VARCHAR2", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMainSurveyId = cci("MAIN_SURVEY_ID", "MAIN_SURVEY_ID", null, null, true, "MainSurveyId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSurveyDataType = cci("SURVEY_DATA_TYPE", "SURVEY_DATA_TYPE", null, null, true, "SurveyDataType", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFilepath = cci("FILEPATH", "FILEPATH", null, null, true, "Filepath", typeof(String), false, "VARCHAR2", 260, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFileName = cci("FILE_NAME", "FILE_NAME", null, null, true, "FileName", typeof(String), false, "NVARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnImportStatus = cci("IMPORT_STATUS", "IMPORT_STATUS", null, null, true, "ImportStatus", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMessage = cci("MESSAGE", "MESSAGE", null, null, false, "Message", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, false, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo", null);
            _columnAddDataNo = cci("ADD_DATA_NO", "ADD_DATA_NO", null, null, false, "AddDataNo", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRequestDatetime = cci("REQUEST_DATETIME", "REQUEST_DATETIME", null, null, false, "RequestDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnRawdataImportQueInfoId);
            _columnInfoList.add(ColumnQcwebJobNo);
            _columnInfoList.add(ColumnMainSurveyId);
            _columnInfoList.add(ColumnSurveyDataType);
            _columnInfoList.add(ColumnFilepath);
            _columnInfoList.add(ColumnFileName);
            _columnInfoList.add(ColumnImportStatus);
            _columnInfoList.add(ColumnMessage);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnAddDataNo);
            _columnInfoList.add(ColumnRequestDatetime);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnRawdataImportQueInfoId);
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


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTQcwebSurveyInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnRawdataImportQueInfoId, TQcwebSurveyInfoDbm.GetInstance().ColumnRawdataImportQueInfoId);
            return cri("TQcwebSurveyInfoList", this, TQcwebSurveyInfoDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_RawData_Import_Que_Info_SEQ1"; } }
        public override String SequenceNextValSql { get { return "select T_RawData_Import_Que_Info_SEQ1.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_RAWDATA_IMPORT_QUE_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TRawdataImportQueInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_RAWDATA_IMPORT_QUE_INFO_ID = "RAWDATA_IMPORT_QUE_INFO_ID";
        public static readonly String DB_NAME_QCWEB_JOB_NO = "QCWEB_JOB_NO";
        public static readonly String DB_NAME_MAIN_SURVEY_ID = "MAIN_SURVEY_ID";
        public static readonly String DB_NAME_SURVEY_DATA_TYPE = "SURVEY_DATA_TYPE";
        public static readonly String DB_NAME_FILEPATH = "FILEPATH";
        public static readonly String DB_NAME_FILE_NAME = "FILE_NAME";
        public static readonly String DB_NAME_IMPORT_STATUS = "IMPORT_STATUS";
        public static readonly String DB_NAME_MESSAGE = "MESSAGE";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_ADD_DATA_NO = "ADD_DATA_NO";
        public static readonly String DB_NAME_REQUEST_DATETIME = "REQUEST_DATETIME";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_RAWDATA_IMPORT_QUE_INFO_ID = "RawdataImportQueInfoId";
        public static readonly String PROPERTY_NAME_QCWEB_JOB_NO = "QcwebJobNo";
        public static readonly String PROPERTY_NAME_MAIN_SURVEY_ID = "MainSurveyId";
        public static readonly String PROPERTY_NAME_SURVEY_DATA_TYPE = "SurveyDataType";
        public static readonly String PROPERTY_NAME_FILEPATH = "Filepath";
        public static readonly String PROPERTY_NAME_FILE_NAME = "FileName";
        public static readonly String PROPERTY_NAME_IMPORT_STATUS = "ImportStatus";
        public static readonly String PROPERTY_NAME_MESSAGE = "Message";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_ADD_DATA_NO = "AddDataNo";
        public static readonly String PROPERTY_NAME_REQUEST_DATETIME = "RequestDatetime";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TQcwebSurveyInfoList = "TQcwebSurveyInfoList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TRawdataImportQueInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_RAWDATA_IMPORT_QUE_INFO_ID.ToLower(), PROPERTY_NAME_RAWDATA_IMPORT_QUE_INFO_ID);
                map.put(DB_NAME_QCWEB_JOB_NO.ToLower(), PROPERTY_NAME_QCWEB_JOB_NO);
                map.put(DB_NAME_MAIN_SURVEY_ID.ToLower(), PROPERTY_NAME_MAIN_SURVEY_ID);
                map.put(DB_NAME_SURVEY_DATA_TYPE.ToLower(), PROPERTY_NAME_SURVEY_DATA_TYPE);
                map.put(DB_NAME_FILEPATH.ToLower(), PROPERTY_NAME_FILEPATH);
                map.put(DB_NAME_FILE_NAME.ToLower(), PROPERTY_NAME_FILE_NAME);
                map.put(DB_NAME_IMPORT_STATUS.ToLower(), PROPERTY_NAME_IMPORT_STATUS);
                map.put(DB_NAME_MESSAGE.ToLower(), PROPERTY_NAME_MESSAGE);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_ADD_DATA_NO.ToLower(), PROPERTY_NAME_ADD_DATA_NO);
                map.put(DB_NAME_REQUEST_DATETIME.ToLower(), PROPERTY_NAME_REQUEST_DATETIME);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_RAWDATA_IMPORT_QUE_INFO_ID.ToLower(), DB_NAME_RAWDATA_IMPORT_QUE_INFO_ID);
                map.put(PROPERTY_NAME_QCWEB_JOB_NO.ToLower(), DB_NAME_QCWEB_JOB_NO);
                map.put(PROPERTY_NAME_MAIN_SURVEY_ID.ToLower(), DB_NAME_MAIN_SURVEY_ID);
                map.put(PROPERTY_NAME_SURVEY_DATA_TYPE.ToLower(), DB_NAME_SURVEY_DATA_TYPE);
                map.put(PROPERTY_NAME_FILEPATH.ToLower(), DB_NAME_FILEPATH);
                map.put(PROPERTY_NAME_FILE_NAME.ToLower(), DB_NAME_FILE_NAME);
                map.put(PROPERTY_NAME_IMPORT_STATUS.ToLower(), DB_NAME_IMPORT_STATUS);
                map.put(PROPERTY_NAME_MESSAGE.ToLower(), DB_NAME_MESSAGE);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_ADD_DATA_NO.ToLower(), DB_NAME_ADD_DATA_NO);
                map.put(PROPERTY_NAME_REQUEST_DATETIME.ToLower(), DB_NAME_REQUEST_DATETIME);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TRawdataImportQueInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TRawdataImportQueInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TRawdataImportQueInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TRawdataImportQueInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TRawdataImportQueInfo NewMyEntity() { return new TRawdataImportQueInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TRawdataImportQueInfoCB NewMyConditionBean() { return new TRawdataImportQueInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TRawdataImportQueInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TRawdataImportQueInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("RAWDATA_IMPORT_QUE_INFO_ID", "RawdataImportQueInfoId", new EntityPropertyRawdataImportQueInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEB_JOB_NO", "QcwebJobNo", new EntityPropertyQcwebJobNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MAIN_SURVEY_ID", "MainSurveyId", new EntityPropertyMainSurveyIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SURVEY_DATA_TYPE", "SurveyDataType", new EntityPropertySurveyDataTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FILEPATH", "Filepath", new EntityPropertyFilepathSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FILE_NAME", "FileName", new EntityPropertyFileNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("IMPORT_STATUS", "ImportStatus", new EntityPropertyImportStatusSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MESSAGE", "Message", new EntityPropertyMessageSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ADD_DATA_NO", "AddDataNo", new EntityPropertyAddDataNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REQUEST_DATETIME", "RequestDatetime", new EntityPropertyRequestDatetimeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TRawdataImportQueInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TRawdataImportQueInfo)entity, value);
        }

        public class EntityPropertyRawdataImportQueInfoIdSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.RawdataImportQueInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebJobNoSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.QcwebJobNo = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyMainSurveyIdSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.MainSurveyId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySurveyDataTypeSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.SurveyDataType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFilepathSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.Filepath = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFileNameSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.FileName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyImportStatusSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.ImportStatus = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMessageSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.Message = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAddDataNoSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.AddDataNo = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyRequestDatetimeSetupper : EntityPropertySetupper<TRawdataImportQueInfo> {
            public void Setup(TRawdataImportQueInfo entity, Object value) { entity.RequestDatetime = (value != null) ? (DateTime?)value : null; }
        }
    }
}
