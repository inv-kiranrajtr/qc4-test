
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

    public class TColorSetInfoGtDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TColorSetInfoGt);

        private static readonly TColorSetInfoGtDbm _instance = new TColorSetInfoGtDbm();
        private TColorSetInfoGtDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TColorSetInfoGtDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_SET_INFO_GT"; } }
        public override String TablePropertyName { get { return "TColorSetInfoGt"; } }
        public override String TableSqlName { get { return "T_COLOR_SET_INFO_GT"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnColorSetInfoGtId;
        protected ColumnInfo _columnTypeCode;
        protected ColumnInfo _columnGradationType;
        protected ColumnInfo _columnGtScenarioItemId;

        public ColumnInfo ColumnColorSetInfoGtId { get { return _columnColorSetInfoGtId; } }
        public ColumnInfo ColumnTypeCode { get { return _columnTypeCode; } }
        public ColumnInfo ColumnGradationType { get { return _columnGradationType; } }
        public ColumnInfo ColumnGtScenarioItemId { get { return _columnGtScenarioItemId; } }

        protected void InitializeColumnInfo() {
            _columnColorSetInfoGtId = cci("COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", null, null, true, "ColorSetInfoGtId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TColorInfoDetailGtList");
            _columnTypeCode = cci("TYPE_CODE", "TYPE_CODE", null, null, true, "TypeCode", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGradationType = cci("GRADATION_TYPE", "GRADATION_TYPE", null, null, true, "GradationType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGtScenarioItemId = cci("GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", null, null, true, "GtScenarioItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TGtScenarioItem", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnColorSetInfoGtId);
            _columnInfoList.add(ColumnTypeCode);
            _columnInfoList.add(ColumnGradationType);
            _columnInfoList.add(ColumnGtScenarioItemId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnColorSetInfoGtId);
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
        public ForeignInfo ForeignTGtScenarioItem { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnGtScenarioItemId, TGtScenarioItemDbm.GetInstance().ColumnGtScenarioItemId);
            return cfi("TGtScenarioItem", this, TGtScenarioItemDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTColorInfoDetailGtList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnColorSetInfoGtId, TColorInfoDetailGtDbm.GetInstance().ColumnColorSetInfoGtId);
            return cri("TColorInfoDetailGtList", this, TColorInfoDetailGtDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Color_Set_Info_GT_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Color_Set_Info_GT_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_COLOR_SET_INFO_GT";
        public static readonly String TABLE_PROPERTY_NAME = "TColorSetInfoGt";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_COLOR_SET_INFO_GT_ID = "COLOR_SET_INFO_GT_ID";
        public static readonly String DB_NAME_TYPE_CODE = "TYPE_CODE";
        public static readonly String DB_NAME_GRADATION_TYPE = "GRADATION_TYPE";
        public static readonly String DB_NAME_GT_SCENARIO_ITEM_ID = "GT_SCENARIO_ITEM_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_COLOR_SET_INFO_GT_ID = "ColorSetInfoGtId";
        public static readonly String PROPERTY_NAME_TYPE_CODE = "TypeCode";
        public static readonly String PROPERTY_NAME_GRADATION_TYPE = "GradationType";
        public static readonly String PROPERTY_NAME_GT_SCENARIO_ITEM_ID = "GtScenarioItemId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TGtScenarioItem = "TGtScenarioItem";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TColorInfoDetailGtList = "TColorInfoDetailGtList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TColorSetInfoGtDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_COLOR_SET_INFO_GT_ID.ToLower(), PROPERTY_NAME_COLOR_SET_INFO_GT_ID);
                map.put(DB_NAME_TYPE_CODE.ToLower(), PROPERTY_NAME_TYPE_CODE);
                map.put(DB_NAME_GRADATION_TYPE.ToLower(), PROPERTY_NAME_GRADATION_TYPE);
                map.put(DB_NAME_GT_SCENARIO_ITEM_ID.ToLower(), PROPERTY_NAME_GT_SCENARIO_ITEM_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_COLOR_SET_INFO_GT_ID.ToLower(), DB_NAME_COLOR_SET_INFO_GT_ID);
                map.put(PROPERTY_NAME_TYPE_CODE.ToLower(), DB_NAME_TYPE_CODE);
                map.put(PROPERTY_NAME_GRADATION_TYPE.ToLower(), DB_NAME_GRADATION_TYPE);
                map.put(PROPERTY_NAME_GT_SCENARIO_ITEM_ID.ToLower(), DB_NAME_GT_SCENARIO_ITEM_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TColorSetInfoGt"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TColorSetInfoGtDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TColorSetInfoGtCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TColorSetInfoGtBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TColorSetInfoGt NewMyEntity() { return new TColorSetInfoGt(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TColorSetInfoGtCB NewMyConditionBean() { return new TColorSetInfoGtCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TColorSetInfoGt>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TColorSetInfoGt>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("COLOR_SET_INFO_GT_ID", "ColorSetInfoGtId", new EntityPropertyColorSetInfoGtIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TYPE_CODE", "TypeCode", new EntityPropertyTypeCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRADATION_TYPE", "GradationType", new EntityPropertyGradationTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GT_SCENARIO_ITEM_ID", "GtScenarioItemId", new EntityPropertyGtScenarioItemIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TColorSetInfoGt> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TColorSetInfoGt)entity, value);
        }

        public class EntityPropertyColorSetInfoGtIdSetupper : EntityPropertySetupper<TColorSetInfoGt> {
            public void Setup(TColorSetInfoGt entity, Object value) { entity.ColorSetInfoGtId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTypeCodeSetupper : EntityPropertySetupper<TColorSetInfoGt> {
            public void Setup(TColorSetInfoGt entity, Object value) { entity.TypeCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGradationTypeSetupper : EntityPropertySetupper<TColorSetInfoGt> {
            public void Setup(TColorSetInfoGt entity, Object value) { entity.GradationType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGtScenarioItemIdSetupper : EntityPropertySetupper<TColorSetInfoGt> {
            public void Setup(TColorSetInfoGt entity, Object value) { entity.GtScenarioItemId = (value != null) ? (decimal?)value : null; }
        }
    }
}
