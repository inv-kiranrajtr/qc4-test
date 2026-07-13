
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

    public class TWeightbackValueDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TWeightbackValue);

        private static readonly TWeightbackValueDbm _instance = new TWeightbackValueDbm();
        private TWeightbackValueDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TWeightbackValueDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_WEIGHTBACK_VALUE"; } }
        public override String TablePropertyName { get { return "TWeightbackValue"; } }
        public override String TableSqlName { get { return "T_WEIGHTBACK_VALUE"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnWeightbackValueId;
        protected ColumnInfo _columnWeightbackItemNo;
        protected ColumnInfo _columnPercentValue;
        protected ColumnInfo _columnParameterNValue;
        protected ColumnInfo _columnWeightbackValue;
        protected ColumnInfo _columnWeightbackId;

        public ColumnInfo ColumnWeightbackValueId { get { return _columnWeightbackValueId; } }
        public ColumnInfo ColumnWeightbackItemNo { get { return _columnWeightbackItemNo; } }
        public ColumnInfo ColumnPercentValue { get { return _columnPercentValue; } }
        public ColumnInfo ColumnParameterNValue { get { return _columnParameterNValue; } }
        public ColumnInfo ColumnWeightbackValue { get { return _columnWeightbackValue; } }
        public ColumnInfo ColumnWeightbackId { get { return _columnWeightbackId; } }

        protected void InitializeColumnInfo() {
            _columnWeightbackValueId = cci("WEIGHTBACK_VALUE_ID", "WEIGHTBACK_VALUE_ID", null, null, true, "WeightbackValueId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWeightbackItemNo = cci("WEIGHTBACK_ITEM_NO", "WEIGHTBACK_ITEM_NO", null, null, true, "WeightbackItemNo", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPercentValue = cci("PERCENT_VALUE", "PERCENT_VALUE", null, null, false, "PercentValue", typeof(String), false, "BINARY_DOUBLE", 8, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnParameterNValue = cci("PARAMETER_N_VALUE", "PARAMETER_N_VALUE", null, null, false, "ParameterNValue", typeof(String), false, "BINARY_DOUBLE", 8, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWeightbackValue = cci("WEIGHTBACK_VALUE", "WEIGHTBACK_VALUE", null, null, false, "WeightbackValue", typeof(String), false, "BINARY_DOUBLE", 8, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWeightbackId = cci("WEIGHTBACK_ID", "WEIGHTBACK_ID", null, null, true, "WeightbackId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TWeightback", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnWeightbackValueId);
            _columnInfoList.add(ColumnWeightbackItemNo);
            _columnInfoList.add(ColumnPercentValue);
            _columnInfoList.add(ColumnParameterNValue);
            _columnInfoList.add(ColumnWeightbackValue);
            _columnInfoList.add(ColumnWeightbackId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnWeightbackValueId);
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
        public ForeignInfo ForeignTWeightback { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnWeightbackId, TWeightbackDbm.GetInstance().ColumnWeightbackId);
            return cfi("TWeightback", this, TWeightbackDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Weightback_Value_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Weightback_Value_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_WEIGHTBACK_VALUE";
        public static readonly String TABLE_PROPERTY_NAME = "TWeightbackValue";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_WEIGHTBACK_VALUE_ID = "WEIGHTBACK_VALUE_ID";
        public static readonly String DB_NAME_WEIGHTBACK_ITEM_NO = "WEIGHTBACK_ITEM_NO";
        public static readonly String DB_NAME_PERCENT_VALUE = "PERCENT_VALUE";
        public static readonly String DB_NAME_PARAMETER_N_VALUE = "PARAMETER_N_VALUE";
        public static readonly String DB_NAME_WEIGHTBACK_VALUE = "WEIGHTBACK_VALUE";
        public static readonly String DB_NAME_WEIGHTBACK_ID = "WEIGHTBACK_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_WEIGHTBACK_VALUE_ID = "WeightbackValueId";
        public static readonly String PROPERTY_NAME_WEIGHTBACK_ITEM_NO = "WeightbackItemNo";
        public static readonly String PROPERTY_NAME_PERCENT_VALUE = "PercentValue";
        public static readonly String PROPERTY_NAME_PARAMETER_N_VALUE = "ParameterNValue";
        public static readonly String PROPERTY_NAME_WEIGHTBACK_VALUE = "WeightbackValue";
        public static readonly String PROPERTY_NAME_WEIGHTBACK_ID = "WeightbackId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TWeightback = "TWeightback";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TWeightbackValueDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_WEIGHTBACK_VALUE_ID.ToLower(), PROPERTY_NAME_WEIGHTBACK_VALUE_ID);
                map.put(DB_NAME_WEIGHTBACK_ITEM_NO.ToLower(), PROPERTY_NAME_WEIGHTBACK_ITEM_NO);
                map.put(DB_NAME_PERCENT_VALUE.ToLower(), PROPERTY_NAME_PERCENT_VALUE);
                map.put(DB_NAME_PARAMETER_N_VALUE.ToLower(), PROPERTY_NAME_PARAMETER_N_VALUE);
                map.put(DB_NAME_WEIGHTBACK_VALUE.ToLower(), PROPERTY_NAME_WEIGHTBACK_VALUE);
                map.put(DB_NAME_WEIGHTBACK_ID.ToLower(), PROPERTY_NAME_WEIGHTBACK_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_WEIGHTBACK_VALUE_ID.ToLower(), DB_NAME_WEIGHTBACK_VALUE_ID);
                map.put(PROPERTY_NAME_WEIGHTBACK_ITEM_NO.ToLower(), DB_NAME_WEIGHTBACK_ITEM_NO);
                map.put(PROPERTY_NAME_PERCENT_VALUE.ToLower(), DB_NAME_PERCENT_VALUE);
                map.put(PROPERTY_NAME_PARAMETER_N_VALUE.ToLower(), DB_NAME_PARAMETER_N_VALUE);
                map.put(PROPERTY_NAME_WEIGHTBACK_VALUE.ToLower(), DB_NAME_WEIGHTBACK_VALUE);
                map.put(PROPERTY_NAME_WEIGHTBACK_ID.ToLower(), DB_NAME_WEIGHTBACK_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TWeightbackValue"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TWeightbackValueDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TWeightbackValueCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TWeightbackValueBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TWeightbackValue NewMyEntity() { return new TWeightbackValue(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TWeightbackValueCB NewMyConditionBean() { return new TWeightbackValueCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TWeightbackValue>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TWeightbackValue>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("WEIGHTBACK_VALUE_ID", "WeightbackValueId", new EntityPropertyWeightbackValueIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WEIGHTBACK_ITEM_NO", "WeightbackItemNo", new EntityPropertyWeightbackItemNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PERCENT_VALUE", "PercentValue", new EntityPropertyPercentValueSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PARAMETER_N_VALUE", "ParameterNValue", new EntityPropertyParameterNValueSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WEIGHTBACK_VALUE", "WeightbackValue", new EntityPropertyWeightbackValueSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WEIGHTBACK_ID", "WeightbackId", new EntityPropertyWeightbackIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TWeightbackValue> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TWeightbackValue)entity, value);
        }

        public class EntityPropertyWeightbackValueIdSetupper : EntityPropertySetupper<TWeightbackValue> {
            public void Setup(TWeightbackValue entity, Object value) { entity.WeightbackValueId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyWeightbackItemNoSetupper : EntityPropertySetupper<TWeightbackValue> {
            public void Setup(TWeightbackValue entity, Object value) { entity.WeightbackItemNo = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyPercentValueSetupper : EntityPropertySetupper<TWeightbackValue> {
            public void Setup(TWeightbackValue entity, Object value) { entity.PercentValue = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyParameterNValueSetupper : EntityPropertySetupper<TWeightbackValue> {
            public void Setup(TWeightbackValue entity, Object value) { entity.ParameterNValue = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyWeightbackValueSetupper : EntityPropertySetupper<TWeightbackValue> {
            public void Setup(TWeightbackValue entity, Object value) { entity.WeightbackValue = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyWeightbackIdSetupper : EntityPropertySetupper<TWeightbackValue> {
            public void Setup(TWeightbackValue entity, Object value) { entity.WeightbackId = (value != null) ? (decimal?)value : null; }
        }
    }
}
