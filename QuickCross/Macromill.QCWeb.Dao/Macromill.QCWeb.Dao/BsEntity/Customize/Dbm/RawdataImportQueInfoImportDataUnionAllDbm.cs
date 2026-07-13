
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class RawdataImportQueInfoImportDataUnionAllDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(RawdataImportQueInfoImportDataUnionAll);

        private static readonly RawdataImportQueInfoImportDataUnionAllDbm _instance = new RawdataImportQueInfoImportDataUnionAllDbm();
        private RawdataImportQueInfoImportDataUnionAllDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static RawdataImportQueInfoImportDataUnionAllDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "RawdataImportQueInfoImportDataUnionAll"; } }
        public override String TablePropertyName { get { return "RawdataImportQueInfoImportDataUnionAll"; } }
        public override String TableSqlName { get { return "RawdataImportQueInfoImportDataUnionAll"; } }

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

        protected void InitializeColumnInfo() {
            _columnRawdataImportQueInfoId = cci("RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", null, null, false, "RawdataImportQueInfoId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebJobNo = cci("QCWEB_JOB_NO", "QCWEB_JOB_NO", null, null, false, "QcwebJobNo", typeof(String), false, "VARCHAR2", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMainSurveyId = cci("MAIN_SURVEY_ID", "MAIN_SURVEY_ID", null, null, false, "MainSurveyId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSurveyDataType = cci("SURVEY_DATA_TYPE", "SURVEY_DATA_TYPE", null, null, false, "SurveyDataType", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFilepath = cci("FILEPATH", "FILEPATH", null, null, false, "Filepath", typeof(String), false, "VARCHAR2", 260, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFileName = cci("FILE_NAME", "FILE_NAME", null, null, false, "FileName", typeof(String), false, "VARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnImportStatus = cci("IMPORT_STATUS", "IMPORT_STATUS", null, null, false, "ImportStatus", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMessage = cci("MESSAGE", "MESSAGE", null, null, false, "Message", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, false, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAddDataNo = cci("ADD_DATA_NO", "ADD_DATA_NO", null, null, false, "AddDataNo", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
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
        public static readonly String TABLE_DB_NAME = "RawdataImportQueInfoImportDataUnionAll";
        public static readonly String TABLE_PROPERTY_NAME = "RawdataImportQueInfoImportDataUnionAll";

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

        static RawdataImportQueInfoImportDataUnionAllDbm() {
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.RawdataImportQueInfoImportDataUnionAll"; } }
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
        public RawdataImportQueInfoImportDataUnionAll NewMyEntity() { return new RawdataImportQueInfoImportDataUnionAll(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll>>();

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
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((RawdataImportQueInfoImportDataUnionAll)entity, value);
        }

        public class EntityPropertyRawdataImportQueInfoIdSetupper : EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> {
            public void Setup(RawdataImportQueInfoImportDataUnionAll entity, Object value) { entity.RawdataImportQueInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebJobNoSetupper : EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> {
            public void Setup(RawdataImportQueInfoImportDataUnionAll entity, Object value) { entity.QcwebJobNo = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyMainSurveyIdSetupper : EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> {
            public void Setup(RawdataImportQueInfoImportDataUnionAll entity, Object value) { entity.MainSurveyId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySurveyDataTypeSetupper : EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> {
            public void Setup(RawdataImportQueInfoImportDataUnionAll entity, Object value) { entity.SurveyDataType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFilepathSetupper : EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> {
            public void Setup(RawdataImportQueInfoImportDataUnionAll entity, Object value) { entity.Filepath = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFileNameSetupper : EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> {
            public void Setup(RawdataImportQueInfoImportDataUnionAll entity, Object value) { entity.FileName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyImportStatusSetupper : EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> {
            public void Setup(RawdataImportQueInfoImportDataUnionAll entity, Object value) { entity.ImportStatus = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMessageSetupper : EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> {
            public void Setup(RawdataImportQueInfoImportDataUnionAll entity, Object value) { entity.Message = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> {
            public void Setup(RawdataImportQueInfoImportDataUnionAll entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAddDataNoSetupper : EntityPropertySetupper<RawdataImportQueInfoImportDataUnionAll> {
            public void Setup(RawdataImportQueInfoImportDataUnionAll entity, Object value) { entity.AddDataNo = (value != null) ? (long?)value : null; }
        }
    }
}
