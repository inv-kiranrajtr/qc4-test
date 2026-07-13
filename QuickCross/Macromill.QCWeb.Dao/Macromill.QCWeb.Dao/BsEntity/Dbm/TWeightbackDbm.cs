
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

    public class TWeightbackDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TWeightback);

        private static readonly TWeightbackDbm _instance = new TWeightbackDbm();
        private TWeightbackDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TWeightbackDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_WEIGHTBACK"; } }
        public override String TablePropertyName { get { return "TWeightback"; } }
        public override String TableSqlName { get { return "T_WEIGHTBACK"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnWeightbackId;
        protected ColumnInfo _columnWeightbackItemId;
        protected ColumnInfo _columnAssistCalcFlag;
        protected ColumnInfo _columnAssistCalcType;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnLastUpdateUser;
        protected ColumnInfo _columnLastUpdateDatetime;

        public ColumnInfo ColumnWeightbackId { get { return _columnWeightbackId; } }
        public ColumnInfo ColumnWeightbackItemId { get { return _columnWeightbackItemId; } }
        public ColumnInfo ColumnAssistCalcFlag { get { return _columnAssistCalcFlag; } }
        public ColumnInfo ColumnAssistCalcType { get { return _columnAssistCalcType; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnLastUpdateUser { get { return _columnLastUpdateUser; } }
        public ColumnInfo ColumnLastUpdateDatetime { get { return _columnLastUpdateDatetime; } }

        protected void InitializeColumnInfo() {
            _columnWeightbackId = cci("WEIGHTBACK_ID", "WEIGHTBACK_ID", null, null, true, "WeightbackId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TWeightbackValue", "TWeightbackValueList");
            _columnWeightbackItemId = cci("WEIGHTBACK_ITEM_ID", "WEIGHTBACK_ITEM_ID", null, null, true, "WeightbackItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAssistCalcFlag = cci("ASSIST_CALC_FLAG", "ASSIST_CALC_FLAG", null, null, true, "AssistCalcFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAssistCalcType = cci("ASSIST_CALC_TYPE", "ASSIST_CALC_TYPE", null, null, true, "AssistCalcType", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo", null);
            _columnLastUpdateUser = cci("LAST_UPDATE_USER", "LAST_UPDATE_USER", null, null, false, "LastUpdateUser", typeof(String), false, "VARCHAR2", 1000, 0, true, OptimisticLockType.NONE, null, null, null);
            _columnLastUpdateDatetime = cci("LAST_UPDATE_DATETIME", "LAST_UPDATE_DATETIME", null, null, false, "LastUpdateDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, true, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnWeightbackId);
            _columnInfoList.add(ColumnWeightbackItemId);
            _columnInfoList.add(ColumnAssistCalcFlag);
            _columnInfoList.add(ColumnAssistCalcType);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnLastUpdateUser);
            _columnInfoList.add(ColumnLastUpdateDatetime);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnWeightbackId);
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
        public ForeignInfo ForeignTWeightbackValue { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnWeightbackId, TWeightbackValueDbm.GetInstance().ColumnWeightbackId);
            return cfi("TWeightbackValue", this, TWeightbackValueDbm.GetInstance(), map, 1, true, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTWeightbackValueList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnWeightbackId, TWeightbackValueDbm.GetInstance().ColumnWeightbackId);
            return cri("TWeightbackValueList", this, TWeightbackValueDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Weightback_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Weightback_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_WEIGHTBACK";
        public static readonly String TABLE_PROPERTY_NAME = "TWeightback";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_WEIGHTBACK_ID = "WEIGHTBACK_ID";
        public static readonly String DB_NAME_WEIGHTBACK_ITEM_ID = "WEIGHTBACK_ITEM_ID";
        public static readonly String DB_NAME_ASSIST_CALC_FLAG = "ASSIST_CALC_FLAG";
        public static readonly String DB_NAME_ASSIST_CALC_TYPE = "ASSIST_CALC_TYPE";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_LAST_UPDATE_USER = "LAST_UPDATE_USER";
        public static readonly String DB_NAME_LAST_UPDATE_DATETIME = "LAST_UPDATE_DATETIME";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_WEIGHTBACK_ID = "WeightbackId";
        public static readonly String PROPERTY_NAME_WEIGHTBACK_ITEM_ID = "WeightbackItemId";
        public static readonly String PROPERTY_NAME_ASSIST_CALC_FLAG = "AssistCalcFlag";
        public static readonly String PROPERTY_NAME_ASSIST_CALC_TYPE = "AssistCalcType";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_USER = "LastUpdateUser";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_DATETIME = "LastUpdateDatetime";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TWeightbackValue = "TWeightbackValue";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TWeightbackValueList = "TWeightbackValueList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TWeightbackDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_WEIGHTBACK_ID.ToLower(), PROPERTY_NAME_WEIGHTBACK_ID);
                map.put(DB_NAME_WEIGHTBACK_ITEM_ID.ToLower(), PROPERTY_NAME_WEIGHTBACK_ITEM_ID);
                map.put(DB_NAME_ASSIST_CALC_FLAG.ToLower(), PROPERTY_NAME_ASSIST_CALC_FLAG);
                map.put(DB_NAME_ASSIST_CALC_TYPE.ToLower(), PROPERTY_NAME_ASSIST_CALC_TYPE);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_LAST_UPDATE_USER.ToLower(), PROPERTY_NAME_LAST_UPDATE_USER);
                map.put(DB_NAME_LAST_UPDATE_DATETIME.ToLower(), PROPERTY_NAME_LAST_UPDATE_DATETIME);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_WEIGHTBACK_ID.ToLower(), DB_NAME_WEIGHTBACK_ID);
                map.put(PROPERTY_NAME_WEIGHTBACK_ITEM_ID.ToLower(), DB_NAME_WEIGHTBACK_ITEM_ID);
                map.put(PROPERTY_NAME_ASSIST_CALC_FLAG.ToLower(), DB_NAME_ASSIST_CALC_FLAG);
                map.put(PROPERTY_NAME_ASSIST_CALC_TYPE.ToLower(), DB_NAME_ASSIST_CALC_TYPE);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_LAST_UPDATE_USER.ToLower(), DB_NAME_LAST_UPDATE_USER);
                map.put(PROPERTY_NAME_LAST_UPDATE_DATETIME.ToLower(), DB_NAME_LAST_UPDATE_DATETIME);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TWeightback"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TWeightbackDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TWeightbackCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TWeightbackBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TWeightback NewMyEntity() { return new TWeightback(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TWeightbackCB NewMyConditionBean() { return new TWeightbackCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TWeightback>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TWeightback>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("WEIGHTBACK_ID", "WeightbackId", new EntityPropertyWeightbackIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WEIGHTBACK_ITEM_ID", "WeightbackItemId", new EntityPropertyWeightbackItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ASSIST_CALC_FLAG", "AssistCalcFlag", new EntityPropertyAssistCalcFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ASSIST_CALC_TYPE", "AssistCalcType", new EntityPropertyAssistCalcTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_USER", "LastUpdateUser", new EntityPropertyLastUpdateUserSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_DATETIME", "LastUpdateDatetime", new EntityPropertyLastUpdateDatetimeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TWeightback> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TWeightback)entity, value);
        }

        public class EntityPropertyWeightbackIdSetupper : EntityPropertySetupper<TWeightback> {
            public void Setup(TWeightback entity, Object value) { entity.WeightbackId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyWeightbackItemIdSetupper : EntityPropertySetupper<TWeightback> {
            public void Setup(TWeightback entity, Object value) { entity.WeightbackItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAssistCalcFlagSetupper : EntityPropertySetupper<TWeightback> {
            public void Setup(TWeightback entity, Object value) { entity.AssistCalcFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyAssistCalcTypeSetupper : EntityPropertySetupper<TWeightback> {
            public void Setup(TWeightback entity, Object value) { entity.AssistCalcType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TWeightback> {
            public void Setup(TWeightback entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyLastUpdateUserSetupper : EntityPropertySetupper<TWeightback> {
            public void Setup(TWeightback entity, Object value) { entity.LastUpdateUser = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLastUpdateDatetimeSetupper : EntityPropertySetupper<TWeightback> {
            public void Setup(TWeightback entity, Object value) { entity.LastUpdateDatetime = (value != null) ? (DateTime?)value : null; }
        }
    }
}
