
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

    public class TQcwebSurveyDetailDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TQcwebSurveyDetail);

        private static readonly TQcwebSurveyDetailDbm _instance = new TQcwebSurveyDetailDbm();
        private TQcwebSurveyDetailDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TQcwebSurveyDetailDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_QCWEB_SURVEY_DETAIL"; } }
        public override String TablePropertyName { get { return "TQcwebSurveyDetail"; } }
        public override String TableSqlName { get { return "T_QCWEB_SURVEY_DETAIL"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebDetailId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnSurveyNo;
        protected ColumnInfo _columnSurveyName;
        protected ColumnInfo _columnQc3uniqueId;
        protected ColumnInfo _columnSurveyMethod;
        protected ColumnInfo _columnServiceType;
        protected ColumnInfo _columnSurveyDate;

        public ColumnInfo ColumnQcwebDetailId { get { return _columnQcwebDetailId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnSurveyNo { get { return _columnSurveyNo; } }
        public ColumnInfo ColumnSurveyName { get { return _columnSurveyName; } }
        public ColumnInfo ColumnQc3uniqueId { get { return _columnQc3uniqueId; } }
        public ColumnInfo ColumnSurveyMethod { get { return _columnSurveyMethod; } }
        public ColumnInfo ColumnServiceType { get { return _columnServiceType; } }
        public ColumnInfo ColumnSurveyDate { get { return _columnSurveyDate; } }

        protected void InitializeColumnInfo() {
            _columnQcwebDetailId = cci("QCWEB_DETAIL_ID", "QCWEB_DETAIL_ID", null, null, true, "QcwebDetailId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo", null);
            _columnSurveyNo = cci("SURVEY_NO", "SURVEY_NO", null, null, false, "SurveyNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSurveyName = cci("SURVEY_NAME", "SURVEY_NAME", null, null, false, "SurveyName", typeof(String), false, "NVARCHAR2", 500, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQc3uniqueId = cci("QC3UNIQUE_ID", "QC3UNIQUE_ID", null, null, false, "Qc3uniqueId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSurveyMethod = cci("SURVEY_METHOD", "SURVEY_METHOD", null, null, false, "SurveyMethod", typeof(String), false, "NVARCHAR2", 30, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnServiceType = cci("SERVICE_TYPE", "SERVICE_TYPE", null, null, false, "ServiceType", typeof(String), false, "VARCHAR2", 15, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSurveyDate = cci("SURVEY_DATE", "SURVEY_DATE", null, null, false, "SurveyDate", typeof(String), false, "NVARCHAR2", 44, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebDetailId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnSurveyNo);
            _columnInfoList.add(ColumnSurveyName);
            _columnInfoList.add(ColumnQc3uniqueId);
            _columnInfoList.add(ColumnSurveyMethod);
            _columnInfoList.add(ColumnServiceType);
            _columnInfoList.add(ColumnSurveyDate);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnQcwebDetailId);
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

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_QCWeb_Survey_Detail_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_QCWeb_Survey_Detail_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_QCWEB_SURVEY_DETAIL";
        public static readonly String TABLE_PROPERTY_NAME = "TQcwebSurveyDetail";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEB_DETAIL_ID = "QCWEB_DETAIL_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_SURVEY_NO = "SURVEY_NO";
        public static readonly String DB_NAME_SURVEY_NAME = "SURVEY_NAME";
        public static readonly String DB_NAME_QC3UNIQUE_ID = "QC3UNIQUE_ID";
        public static readonly String DB_NAME_SURVEY_METHOD = "SURVEY_METHOD";
        public static readonly String DB_NAME_SERVICE_TYPE = "SERVICE_TYPE";
        public static readonly String DB_NAME_SURVEY_DATE = "SURVEY_DATE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEB_DETAIL_ID = "QcwebDetailId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_SURVEY_NO = "SurveyNo";
        public static readonly String PROPERTY_NAME_SURVEY_NAME = "SurveyName";
        public static readonly String PROPERTY_NAME_QC3UNIQUE_ID = "Qc3uniqueId";
        public static readonly String PROPERTY_NAME_SURVEY_METHOD = "SurveyMethod";
        public static readonly String PROPERTY_NAME_SERVICE_TYPE = "ServiceType";
        public static readonly String PROPERTY_NAME_SURVEY_DATE = "SurveyDate";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TQcwebSurveyDetailDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEB_DETAIL_ID.ToLower(), PROPERTY_NAME_QCWEB_DETAIL_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_SURVEY_NO.ToLower(), PROPERTY_NAME_SURVEY_NO);
                map.put(DB_NAME_SURVEY_NAME.ToLower(), PROPERTY_NAME_SURVEY_NAME);
                map.put(DB_NAME_QC3UNIQUE_ID.ToLower(), PROPERTY_NAME_QC3UNIQUE_ID);
                map.put(DB_NAME_SURVEY_METHOD.ToLower(), PROPERTY_NAME_SURVEY_METHOD);
                map.put(DB_NAME_SERVICE_TYPE.ToLower(), PROPERTY_NAME_SERVICE_TYPE);
                map.put(DB_NAME_SURVEY_DATE.ToLower(), PROPERTY_NAME_SURVEY_DATE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEB_DETAIL_ID.ToLower(), DB_NAME_QCWEB_DETAIL_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_SURVEY_NO.ToLower(), DB_NAME_SURVEY_NO);
                map.put(PROPERTY_NAME_SURVEY_NAME.ToLower(), DB_NAME_SURVEY_NAME);
                map.put(PROPERTY_NAME_QC3UNIQUE_ID.ToLower(), DB_NAME_QC3UNIQUE_ID);
                map.put(PROPERTY_NAME_SURVEY_METHOD.ToLower(), DB_NAME_SURVEY_METHOD);
                map.put(PROPERTY_NAME_SERVICE_TYPE.ToLower(), DB_NAME_SERVICE_TYPE);
                map.put(PROPERTY_NAME_SURVEY_DATE.ToLower(), DB_NAME_SURVEY_DATE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TQcwebSurveyDetail"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TQcwebSurveyDetailDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TQcwebSurveyDetailCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TQcwebSurveyDetailBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TQcwebSurveyDetail NewMyEntity() { return new TQcwebSurveyDetail(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TQcwebSurveyDetailCB NewMyConditionBean() { return new TQcwebSurveyDetailCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TQcwebSurveyDetail>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TQcwebSurveyDetail>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEB_DETAIL_ID", "QcwebDetailId", new EntityPropertyQcwebDetailIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SURVEY_NO", "SurveyNo", new EntityPropertySurveyNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SURVEY_NAME", "SurveyName", new EntityPropertySurveyNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QC3UNIQUE_ID", "Qc3uniqueId", new EntityPropertyQc3uniqueIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SURVEY_METHOD", "SurveyMethod", new EntityPropertySurveyMethodSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SERVICE_TYPE", "ServiceType", new EntityPropertyServiceTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SURVEY_DATE", "SurveyDate", new EntityPropertySurveyDateSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TQcwebSurveyDetail> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TQcwebSurveyDetail)entity, value);
        }

        public class EntityPropertyQcwebDetailIdSetupper : EntityPropertySetupper<TQcwebSurveyDetail> {
            public void Setup(TQcwebSurveyDetail entity, Object value) { entity.QcwebDetailId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TQcwebSurveyDetail> {
            public void Setup(TQcwebSurveyDetail entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySurveyNoSetupper : EntityPropertySetupper<TQcwebSurveyDetail> {
            public void Setup(TQcwebSurveyDetail entity, Object value) { entity.SurveyNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySurveyNameSetupper : EntityPropertySetupper<TQcwebSurveyDetail> {
            public void Setup(TQcwebSurveyDetail entity, Object value) { entity.SurveyName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQc3uniqueIdSetupper : EntityPropertySetupper<TQcwebSurveyDetail> {
            public void Setup(TQcwebSurveyDetail entity, Object value) { entity.Qc3uniqueId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySurveyMethodSetupper : EntityPropertySetupper<TQcwebSurveyDetail> {
            public void Setup(TQcwebSurveyDetail entity, Object value) { entity.SurveyMethod = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyServiceTypeSetupper : EntityPropertySetupper<TQcwebSurveyDetail> {
            public void Setup(TQcwebSurveyDetail entity, Object value) { entity.ServiceType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySurveyDateSetupper : EntityPropertySetupper<TQcwebSurveyDetail> {
            public void Setup(TQcwebSurveyDetail entity, Object value) { entity.SurveyDate = (value != null) ? (String)value : null; }
        }
    }
}
