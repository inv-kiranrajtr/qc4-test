
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

    public class TOutputSubCklistDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputSubCklist);

        private static readonly TOutputSubCklistDbm _instance = new TOutputSubCklistDbm();
        private TOutputSubCklistDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputSubCklistDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_CKLIST"; } }
        public override String TablePropertyName { get { return "TOutputSubCklist"; } }
        public override String TableSqlName { get { return "T_OUTPUT_SUB_CKLIST"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputSubCklistId;
        protected ColumnInfo _columnOutputCommonId;
        protected ColumnInfo _columnTotalCount;

        public ColumnInfo ColumnOutputSubCklistId { get { return _columnOutputSubCklistId; } }
        public ColumnInfo ColumnOutputCommonId { get { return _columnOutputCommonId; } }
        public ColumnInfo ColumnTotalCount { get { return _columnTotalCount; } }

        protected void InitializeColumnInfo() {
            _columnOutputSubCklistId = cci("OUTPUT_SUB_CKLIST_ID", "OUTPUT_SUB_CKLIST_ID", null, null, true, "OutputSubCklistId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputCommonId = cci("OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", null, null, true, "OutputCommonId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputCommon", null);
            _columnTotalCount = cci("TOTAL_COUNT", "TOTAL_COUNT", null, null, true, "TotalCount", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputSubCklistId);
            _columnInfoList.add(ColumnOutputCommonId);
            _columnInfoList.add(ColumnTotalCount);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputSubCklistId);
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
        public ForeignInfo ForeignTOutputCommon { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputCommonId, TOutputCommonDbm.GetInstance().ColumnOutputCommonId);
            return cfi("TOutputCommon", this, TOutputCommonDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Output_Sub_CKList_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Output_Sub_CKList_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_SUB_CKLIST";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputSubCklist";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_SUB_CKLIST_ID = "OUTPUT_SUB_CKLIST_ID";
        public static readonly String DB_NAME_OUTPUT_COMMON_ID = "OUTPUT_COMMON_ID";
        public static readonly String DB_NAME_TOTAL_COUNT = "TOTAL_COUNT";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_SUB_CKLIST_ID = "OutputSubCklistId";
        public static readonly String PROPERTY_NAME_OUTPUT_COMMON_ID = "OutputCommonId";
        public static readonly String PROPERTY_NAME_TOTAL_COUNT = "TotalCount";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputCommon = "TOutputCommon";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputSubCklistDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_SUB_CKLIST_ID.ToLower(), PROPERTY_NAME_OUTPUT_SUB_CKLIST_ID);
                map.put(DB_NAME_OUTPUT_COMMON_ID.ToLower(), PROPERTY_NAME_OUTPUT_COMMON_ID);
                map.put(DB_NAME_TOTAL_COUNT.ToLower(), PROPERTY_NAME_TOTAL_COUNT);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_SUB_CKLIST_ID.ToLower(), DB_NAME_OUTPUT_SUB_CKLIST_ID);
                map.put(PROPERTY_NAME_OUTPUT_COMMON_ID.ToLower(), DB_NAME_OUTPUT_COMMON_ID);
                map.put(PROPERTY_NAME_TOTAL_COUNT.ToLower(), DB_NAME_TOTAL_COUNT);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputSubCklist"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputSubCklistDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputSubCklistCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputSubCklistBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputSubCklist NewMyEntity() { return new TOutputSubCklist(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputSubCklistCB NewMyConditionBean() { return new TOutputSubCklistCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputSubCklist>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputSubCklist>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_SUB_CKLIST_ID", "OutputSubCklistId", new EntityPropertyOutputSubCklistIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_COMMON_ID", "OutputCommonId", new EntityPropertyOutputCommonIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TOTAL_COUNT", "TotalCount", new EntityPropertyTotalCountSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputSubCklist> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputSubCklist)entity, value);
        }

        public class EntityPropertyOutputSubCklistIdSetupper : EntityPropertySetupper<TOutputSubCklist> {
            public void Setup(TOutputSubCklist entity, Object value) { entity.OutputSubCklistId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputCommonIdSetupper : EntityPropertySetupper<TOutputSubCklist> {
            public void Setup(TOutputSubCklist entity, Object value) { entity.OutputCommonId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTotalCountSetupper : EntityPropertySetupper<TOutputSubCklist> {
            public void Setup(TOutputSubCklist entity, Object value) { entity.TotalCount = (value != null) ? (long?)value : null; }
        }
    }
}
