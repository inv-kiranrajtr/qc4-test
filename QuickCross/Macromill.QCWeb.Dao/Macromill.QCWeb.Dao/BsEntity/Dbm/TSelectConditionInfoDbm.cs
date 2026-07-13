
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

    public class TSelectConditionInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TSelectConditionInfo);

        private static readonly TSelectConditionInfoDbm _instance = new TSelectConditionInfoDbm();
        private TSelectConditionInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TSelectConditionInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_SELECT_CONDITION_INFO"; } }
        public override String TablePropertyName { get { return "TSelectConditionInfo"; } }
        public override String TableSqlName { get { return "T_SELECT_CONDITION_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnSelectNo;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnQuestionNo;
        protected ColumnInfo _columnChildQuestionNo;
        protected ColumnInfo _columnSelectCondition;

        public ColumnInfo ColumnSelectNo { get { return _columnSelectNo; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnQuestionNo { get { return _columnQuestionNo; } }
        public ColumnInfo ColumnChildQuestionNo { get { return _columnChildQuestionNo; } }
        public ColumnInfo ColumnSelectCondition { get { return _columnSelectCondition; } }

        protected void InitializeColumnInfo() {
            _columnSelectNo = cci("SELECT_NO", "SELECT_NO", null, null, true, "SelectNo", typeof(long?), true, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo,TQcwebSurveyInfoAsOne", "");
            _columnQuestionNo = cci("QUESTION_NO", "QUESTION_NO", null, null, false, "QuestionNo", typeof(String), false, "NVARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnChildQuestionNo = cci("CHILD_QUESTION_NO", "CHILD_QUESTION_NO", null, null, false, "ChildQuestionNo", typeof(String), false, "NVARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSelectCondition = cci("SELECT_CONDITION", "SELECT_CONDITION", null, null, false, "SelectCondition", typeof(String), false, "NCLOB", 4000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnSelectNo);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnQuestionNo);
            _columnInfoList.add(ColumnChildQuestionNo);
            _columnInfoList.add(ColumnSelectCondition);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            List<ColumnInfo> ls = new ArrayList<ColumnInfo>();
            ls.add(ColumnSelectNo);
            ls.add(ColumnQcwebid);
            return cpui(ls);
        }}

        // -------------------------------------------------
        //                                   Primary Element
        //                                   ---------------
        public override bool HasPrimaryKey { get { return true; } }
        public override bool HasCompoundPrimaryKey { get { return true; } }

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

        public ForeignInfo ForeignTQcwebSurveyInfoAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfoAsOne", this, TQcwebSurveyInfoDbm.GetInstance(), map, 1, true, false);
        }}

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
        public static readonly String TABLE_DB_NAME = "T_SELECT_CONDITION_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TSelectConditionInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_SELECT_NO = "SELECT_NO";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_QUESTION_NO = "QUESTION_NO";
        public static readonly String DB_NAME_CHILD_QUESTION_NO = "CHILD_QUESTION_NO";
        public static readonly String DB_NAME_SELECT_CONDITION = "SELECT_CONDITION";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_SELECT_NO = "SelectNo";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_QUESTION_NO = "QuestionNo";
        public static readonly String PROPERTY_NAME_CHILD_QUESTION_NO = "ChildQuestionNo";
        public static readonly String PROPERTY_NAME_SELECT_CONDITION = "SelectCondition";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfoAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TSelectConditionInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_SELECT_NO.ToLower(), PROPERTY_NAME_SELECT_NO);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_QUESTION_NO.ToLower(), PROPERTY_NAME_QUESTION_NO);
                map.put(DB_NAME_CHILD_QUESTION_NO.ToLower(), PROPERTY_NAME_CHILD_QUESTION_NO);
                map.put(DB_NAME_SELECT_CONDITION.ToLower(), PROPERTY_NAME_SELECT_CONDITION);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_SELECT_NO.ToLower(), DB_NAME_SELECT_NO);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_QUESTION_NO.ToLower(), DB_NAME_QUESTION_NO);
                map.put(PROPERTY_NAME_CHILD_QUESTION_NO.ToLower(), DB_NAME_CHILD_QUESTION_NO);
                map.put(PROPERTY_NAME_SELECT_CONDITION.ToLower(), DB_NAME_SELECT_CONDITION);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TSelectConditionInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TSelectConditionInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TSelectConditionInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TSelectConditionInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TSelectConditionInfo NewMyEntity() { return new TSelectConditionInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TSelectConditionInfoCB NewMyConditionBean() { return new TSelectConditionInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TSelectConditionInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TSelectConditionInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("SELECT_NO", "SelectNo", new EntityPropertySelectNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QUESTION_NO", "QuestionNo", new EntityPropertyQuestionNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CHILD_QUESTION_NO", "ChildQuestionNo", new EntityPropertyChildQuestionNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SELECT_CONDITION", "SelectCondition", new EntityPropertySelectConditionSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TSelectConditionInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TSelectConditionInfo)entity, value);
        }

        public class EntityPropertySelectNoSetupper : EntityPropertySetupper<TSelectConditionInfo> {
            public void Setup(TSelectConditionInfo entity, Object value) { entity.SelectNo = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TSelectConditionInfo> {
            public void Setup(TSelectConditionInfo entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQuestionNoSetupper : EntityPropertySetupper<TSelectConditionInfo> {
            public void Setup(TSelectConditionInfo entity, Object value) { entity.QuestionNo = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyChildQuestionNoSetupper : EntityPropertySetupper<TSelectConditionInfo> {
            public void Setup(TSelectConditionInfo entity, Object value) { entity.ChildQuestionNo = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySelectConditionSetupper : EntityPropertySetupper<TSelectConditionInfo> {
            public void Setup(TSelectConditionInfo entity, Object value) { entity.SelectCondition = (value != null) ? (String)value : null; }
        }
    }
}
