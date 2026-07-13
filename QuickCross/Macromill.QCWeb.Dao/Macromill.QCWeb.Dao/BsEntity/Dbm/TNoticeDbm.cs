
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

    public class TNoticeDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TNotice);

        private static readonly TNoticeDbm _instance = new TNoticeDbm();
        private TNoticeDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TNoticeDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_NOTICE"; } }
        public override String TablePropertyName { get { return "TNotice"; } }
        public override String TableSqlName { get { return "T_NOTICE"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnNoticeId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnUserId;
        protected ColumnInfo _columnDeleteFlag;
        protected ColumnInfo _columnNoticeInfo;
        protected ColumnInfo _columnNoticeType;
        protected ColumnInfo _columnLinkUrl;
        protected ColumnInfo _columnExpirationStartdate;
        protected ColumnInfo _columnExpirationEnddate;

        public ColumnInfo ColumnNoticeId { get { return _columnNoticeId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnUserId { get { return _columnUserId; } }
        public ColumnInfo ColumnDeleteFlag { get { return _columnDeleteFlag; } }
        public ColumnInfo ColumnNoticeInfo { get { return _columnNoticeInfo; } }
        public ColumnInfo ColumnNoticeType { get { return _columnNoticeType; } }
        public ColumnInfo ColumnLinkUrl { get { return _columnLinkUrl; } }
        public ColumnInfo ColumnExpirationStartdate { get { return _columnExpirationStartdate; } }
        public ColumnInfo ColumnExpirationEnddate { get { return _columnExpirationEnddate; } }

        protected void InitializeColumnInfo() {
            _columnNoticeId = cci("NOTICE_ID", "NOTICE_ID", null, null, true, "NoticeId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo", null);
            _columnUserId = cci("USER_ID", "USER_ID", null, null, true, "UserId", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDeleteFlag = cci("DELETE_FLAG", "DELETE_FLAG", null, null, true, "DeleteFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNoticeInfo = cci("NOTICE_INFO", "NOTICE_INFO", null, null, true, "NoticeInfo", typeof(String), false, "NVARCHAR2", 1024, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNoticeType = cci("NOTICE_TYPE", "NOTICE_TYPE", null, null, false, "NoticeType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLinkUrl = cci("LINK_URL", "LINK_URL", null, null, false, "LinkUrl", typeof(String), false, "VARCHAR2", 1024, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExpirationStartdate = cci("EXPIRATION_STARTDATE", "EXPIRATION_STARTDATE", null, null, false, "ExpirationStartdate", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnExpirationEnddate = cci("EXPIRATION_ENDDATE", "EXPIRATION_ENDDATE", null, null, false, "ExpirationEnddate", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnNoticeId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnUserId);
            _columnInfoList.add(ColumnDeleteFlag);
            _columnInfoList.add(ColumnNoticeInfo);
            _columnInfoList.add(ColumnNoticeType);
            _columnInfoList.add(ColumnLinkUrl);
            _columnInfoList.add(ColumnExpirationStartdate);
            _columnInfoList.add(ColumnExpirationEnddate);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnNoticeId);
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
        public override String SequenceName { get { return "T_Notice_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Notice_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_NOTICE";
        public static readonly String TABLE_PROPERTY_NAME = "TNotice";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_NOTICE_ID = "NOTICE_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_USER_ID = "USER_ID";
        public static readonly String DB_NAME_DELETE_FLAG = "DELETE_FLAG";
        public static readonly String DB_NAME_NOTICE_INFO = "NOTICE_INFO";
        public static readonly String DB_NAME_NOTICE_TYPE = "NOTICE_TYPE";
        public static readonly String DB_NAME_LINK_URL = "LINK_URL";
        public static readonly String DB_NAME_EXPIRATION_STARTDATE = "EXPIRATION_STARTDATE";
        public static readonly String DB_NAME_EXPIRATION_ENDDATE = "EXPIRATION_ENDDATE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_NOTICE_ID = "NoticeId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_USER_ID = "UserId";
        public static readonly String PROPERTY_NAME_DELETE_FLAG = "DeleteFlag";
        public static readonly String PROPERTY_NAME_NOTICE_INFO = "NoticeInfo";
        public static readonly String PROPERTY_NAME_NOTICE_TYPE = "NoticeType";
        public static readonly String PROPERTY_NAME_LINK_URL = "LinkUrl";
        public static readonly String PROPERTY_NAME_EXPIRATION_STARTDATE = "ExpirationStartdate";
        public static readonly String PROPERTY_NAME_EXPIRATION_ENDDATE = "ExpirationEnddate";

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

        static TNoticeDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_NOTICE_ID.ToLower(), PROPERTY_NAME_NOTICE_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_USER_ID.ToLower(), PROPERTY_NAME_USER_ID);
                map.put(DB_NAME_DELETE_FLAG.ToLower(), PROPERTY_NAME_DELETE_FLAG);
                map.put(DB_NAME_NOTICE_INFO.ToLower(), PROPERTY_NAME_NOTICE_INFO);
                map.put(DB_NAME_NOTICE_TYPE.ToLower(), PROPERTY_NAME_NOTICE_TYPE);
                map.put(DB_NAME_LINK_URL.ToLower(), PROPERTY_NAME_LINK_URL);
                map.put(DB_NAME_EXPIRATION_STARTDATE.ToLower(), PROPERTY_NAME_EXPIRATION_STARTDATE);
                map.put(DB_NAME_EXPIRATION_ENDDATE.ToLower(), PROPERTY_NAME_EXPIRATION_ENDDATE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_NOTICE_ID.ToLower(), DB_NAME_NOTICE_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_USER_ID.ToLower(), DB_NAME_USER_ID);
                map.put(PROPERTY_NAME_DELETE_FLAG.ToLower(), DB_NAME_DELETE_FLAG);
                map.put(PROPERTY_NAME_NOTICE_INFO.ToLower(), DB_NAME_NOTICE_INFO);
                map.put(PROPERTY_NAME_NOTICE_TYPE.ToLower(), DB_NAME_NOTICE_TYPE);
                map.put(PROPERTY_NAME_LINK_URL.ToLower(), DB_NAME_LINK_URL);
                map.put(PROPERTY_NAME_EXPIRATION_STARTDATE.ToLower(), DB_NAME_EXPIRATION_STARTDATE);
                map.put(PROPERTY_NAME_EXPIRATION_ENDDATE.ToLower(), DB_NAME_EXPIRATION_ENDDATE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TNotice"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TNoticeDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TNoticeCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TNoticeBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TNotice NewMyEntity() { return new TNotice(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TNoticeCB NewMyConditionBean() { return new TNoticeCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TNotice>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TNotice>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("NOTICE_ID", "NoticeId", new EntityPropertyNoticeIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("USER_ID", "UserId", new EntityPropertyUserIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_FLAG", "DeleteFlag", new EntityPropertyDeleteFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NOTICE_INFO", "NoticeInfo", new EntityPropertyNoticeInfoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NOTICE_TYPE", "NoticeType", new EntityPropertyNoticeTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LINK_URL", "LinkUrl", new EntityPropertyLinkUrlSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXPIRATION_STARTDATE", "ExpirationStartdate", new EntityPropertyExpirationStartdateSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXPIRATION_ENDDATE", "ExpirationEnddate", new EntityPropertyExpirationEnddateSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TNotice> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TNotice)entity, value);
        }

        public class EntityPropertyNoticeIdSetupper : EntityPropertySetupper<TNotice> {
            public void Setup(TNotice entity, Object value) { entity.NoticeId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TNotice> {
            public void Setup(TNotice entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyUserIdSetupper : EntityPropertySetupper<TNotice> {
            public void Setup(TNotice entity, Object value) { entity.UserId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDeleteFlagSetupper : EntityPropertySetupper<TNotice> {
            public void Setup(TNotice entity, Object value) { entity.DeleteFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNoticeInfoSetupper : EntityPropertySetupper<TNotice> {
            public void Setup(TNotice entity, Object value) { entity.NoticeInfo = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNoticeTypeSetupper : EntityPropertySetupper<TNotice> {
            public void Setup(TNotice entity, Object value) { entity.NoticeType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLinkUrlSetupper : EntityPropertySetupper<TNotice> {
            public void Setup(TNotice entity, Object value) { entity.LinkUrl = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyExpirationStartdateSetupper : EntityPropertySetupper<TNotice> {
            public void Setup(TNotice entity, Object value) { entity.ExpirationStartdate = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyExpirationEnddateSetupper : EntityPropertySetupper<TNotice> {
            public void Setup(TNotice entity, Object value) { entity.ExpirationEnddate = (value != null) ? (DateTime?)value : null; }
        }
    }
}
