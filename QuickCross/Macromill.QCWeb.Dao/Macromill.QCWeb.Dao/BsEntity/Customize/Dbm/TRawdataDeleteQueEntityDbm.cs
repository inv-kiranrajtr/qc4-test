
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class TRawdataDeleteQueEntityDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TRawdataDeleteQueEntity);

        private static readonly TRawdataDeleteQueEntityDbm _instance = new TRawdataDeleteQueEntityDbm();
        private TRawdataDeleteQueEntityDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TRawdataDeleteQueEntityDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "TRawdataDeleteQueEntity"; } }
        public override String TablePropertyName { get { return "TRawdataDeleteQueEntity"; } }
        public override String TableSqlName { get { return "TRawdataDeleteQueEntity"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnDeleteKbn;
        protected ColumnInfo _columnRawdataDeleteQueId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnMainSurveyId;
        protected ColumnInfo _columnSurveyInfoId;

        public ColumnInfo ColumnDeleteKbn { get { return _columnDeleteKbn; } }
        public ColumnInfo ColumnRawdataDeleteQueId { get { return _columnRawdataDeleteQueId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnMainSurveyId { get { return _columnMainSurveyId; } }
        public ColumnInfo ColumnSurveyInfoId { get { return _columnSurveyInfoId; } }

        protected void InitializeColumnInfo() {
            _columnDeleteKbn = cci("DELETE_KBN", "DELETE_KBN", null, null, false, "DeleteKbn", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRawdataDeleteQueId = cci("RAWDATA_DELETE_QUE_ID", "RAWDATA_DELETE_QUE_ID", null, null, false, "RawdataDeleteQueId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, false, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMainSurveyId = cci("MAIN_SURVEY_ID", "MAIN_SURVEY_ID", null, null, false, "MainSurveyId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSurveyInfoId = cci("SURVEY_INFO_ID", "SURVEY_INFO_ID", null, null, false, "SurveyInfoId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnDeleteKbn);
            _columnInfoList.add(ColumnRawdataDeleteQueId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnMainSurveyId);
            _columnInfoList.add(ColumnSurveyInfoId);
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
        public static readonly String TABLE_DB_NAME = "TRawdataDeleteQueEntity";
        public static readonly String TABLE_PROPERTY_NAME = "TRawdataDeleteQueEntity";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_DELETE_KBN = "DELETE_KBN";
        public static readonly String DB_NAME_RAWDATA_DELETE_QUE_ID = "RAWDATA_DELETE_QUE_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_MAIN_SURVEY_ID = "MAIN_SURVEY_ID";
        public static readonly String DB_NAME_SURVEY_INFO_ID = "SURVEY_INFO_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_DELETE_KBN = "DeleteKbn";
        public static readonly String PROPERTY_NAME_RAWDATA_DELETE_QUE_ID = "RawdataDeleteQueId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_MAIN_SURVEY_ID = "MainSurveyId";
        public static readonly String PROPERTY_NAME_SURVEY_INFO_ID = "SurveyInfoId";

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

        static TRawdataDeleteQueEntityDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_DELETE_KBN.ToLower(), PROPERTY_NAME_DELETE_KBN);
                map.put(DB_NAME_RAWDATA_DELETE_QUE_ID.ToLower(), PROPERTY_NAME_RAWDATA_DELETE_QUE_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_MAIN_SURVEY_ID.ToLower(), PROPERTY_NAME_MAIN_SURVEY_ID);
                map.put(DB_NAME_SURVEY_INFO_ID.ToLower(), PROPERTY_NAME_SURVEY_INFO_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_DELETE_KBN.ToLower(), DB_NAME_DELETE_KBN);
                map.put(PROPERTY_NAME_RAWDATA_DELETE_QUE_ID.ToLower(), DB_NAME_RAWDATA_DELETE_QUE_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_MAIN_SURVEY_ID.ToLower(), DB_NAME_MAIN_SURVEY_ID);
                map.put(PROPERTY_NAME_SURVEY_INFO_ID.ToLower(), DB_NAME_SURVEY_INFO_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.TRawdataDeleteQueEntity"; } }
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
        public TRawdataDeleteQueEntity NewMyEntity() { return new TRawdataDeleteQueEntity(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TRawdataDeleteQueEntity>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TRawdataDeleteQueEntity>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("DELETE_KBN", "DeleteKbn", new EntityPropertyDeleteKbnSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RAWDATA_DELETE_QUE_ID", "RawdataDeleteQueId", new EntityPropertyRawdataDeleteQueIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MAIN_SURVEY_ID", "MainSurveyId", new EntityPropertyMainSurveyIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SURVEY_INFO_ID", "SurveyInfoId", new EntityPropertySurveyInfoIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TRawdataDeleteQueEntity> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TRawdataDeleteQueEntity)entity, value);
        }

        public class EntityPropertyDeleteKbnSetupper : EntityPropertySetupper<TRawdataDeleteQueEntity> {
            public void Setup(TRawdataDeleteQueEntity entity, Object value) { entity.DeleteKbn = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyRawdataDeleteQueIdSetupper : EntityPropertySetupper<TRawdataDeleteQueEntity> {
            public void Setup(TRawdataDeleteQueEntity entity, Object value) { entity.RawdataDeleteQueId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TRawdataDeleteQueEntity> {
            public void Setup(TRawdataDeleteQueEntity entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyMainSurveyIdSetupper : EntityPropertySetupper<TRawdataDeleteQueEntity> {
            public void Setup(TRawdataDeleteQueEntity entity, Object value) { entity.MainSurveyId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySurveyInfoIdSetupper : EntityPropertySetupper<TRawdataDeleteQueEntity> {
            public void Setup(TRawdataDeleteQueEntity entity, Object value) { entity.SurveyInfoId = (value != null) ? (decimal?)value : null; }
        }
    }
}
