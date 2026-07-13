
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

    public class TColorSetInfoCrossDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TColorSetInfoCross);

        private static readonly TColorSetInfoCrossDbm _instance = new TColorSetInfoCrossDbm();
        private TColorSetInfoCrossDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TColorSetInfoCrossDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_SET_INFO_CROSS"; } }
        public override String TablePropertyName { get { return "TColorSetInfoCross"; } }
        public override String TableSqlName { get { return "T_COLOR_SET_INFO_CROSS"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnColorSetInfoCrossId;
        protected ColumnInfo _columnTypeCode;
        protected ColumnInfo _columnGradationType;
        protected ColumnInfo _columnCrossScenarioTargetId;

        public ColumnInfo ColumnColorSetInfoCrossId { get { return _columnColorSetInfoCrossId; } }
        public ColumnInfo ColumnTypeCode { get { return _columnTypeCode; } }
        public ColumnInfo ColumnGradationType { get { return _columnGradationType; } }
        public ColumnInfo ColumnCrossScenarioTargetId { get { return _columnCrossScenarioTargetId; } }

        protected void InitializeColumnInfo() {
            _columnColorSetInfoCrossId = cci("COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", null, null, true, "ColorSetInfoCrossId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TColorInfoDetailCrossList");
            _columnTypeCode = cci("TYPE_CODE", "TYPE_CODE", null, null, true, "TypeCode", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGradationType = cci("GRADATION_TYPE", "GRADATION_TYPE", null, null, true, "GradationType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCrossScenarioTargetId = cci("CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", null, null, true, "CrossScenarioTargetId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TCrossScenarioTarget", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnColorSetInfoCrossId);
            _columnInfoList.add(ColumnTypeCode);
            _columnInfoList.add(ColumnGradationType);
            _columnInfoList.add(ColumnCrossScenarioTargetId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnColorSetInfoCrossId);
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
        public ForeignInfo ForeignTCrossScenarioTarget { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCrossScenarioTargetId, TCrossScenarioTargetDbm.GetInstance().ColumnCrossScenarioTargetId);
            return cfi("TCrossScenarioTarget", this, TCrossScenarioTargetDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTColorInfoDetailCrossList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnColorSetInfoCrossId, TColorInfoDetailCrossDbm.GetInstance().ColumnColorSetInfoCrossId);
            return cri("TColorInfoDetailCrossList", this, TColorInfoDetailCrossDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Color_Set_Info_Cross_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Color_Set_Info_Cross_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_COLOR_SET_INFO_CROSS";
        public static readonly String TABLE_PROPERTY_NAME = "TColorSetInfoCross";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_COLOR_SET_INFO_CROSS_ID = "COLOR_SET_INFO_CROSS_ID";
        public static readonly String DB_NAME_TYPE_CODE = "TYPE_CODE";
        public static readonly String DB_NAME_GRADATION_TYPE = "GRADATION_TYPE";
        public static readonly String DB_NAME_CROSS_SCENARIO_TARGET_ID = "CROSS_SCENARIO_TARGET_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_COLOR_SET_INFO_CROSS_ID = "ColorSetInfoCrossId";
        public static readonly String PROPERTY_NAME_TYPE_CODE = "TypeCode";
        public static readonly String PROPERTY_NAME_GRADATION_TYPE = "GradationType";
        public static readonly String PROPERTY_NAME_CROSS_SCENARIO_TARGET_ID = "CrossScenarioTargetId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TCrossScenarioTarget = "TCrossScenarioTarget";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TColorInfoDetailCrossList = "TColorInfoDetailCrossList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TColorSetInfoCrossDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_COLOR_SET_INFO_CROSS_ID.ToLower(), PROPERTY_NAME_COLOR_SET_INFO_CROSS_ID);
                map.put(DB_NAME_TYPE_CODE.ToLower(), PROPERTY_NAME_TYPE_CODE);
                map.put(DB_NAME_GRADATION_TYPE.ToLower(), PROPERTY_NAME_GRADATION_TYPE);
                map.put(DB_NAME_CROSS_SCENARIO_TARGET_ID.ToLower(), PROPERTY_NAME_CROSS_SCENARIO_TARGET_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_COLOR_SET_INFO_CROSS_ID.ToLower(), DB_NAME_COLOR_SET_INFO_CROSS_ID);
                map.put(PROPERTY_NAME_TYPE_CODE.ToLower(), DB_NAME_TYPE_CODE);
                map.put(PROPERTY_NAME_GRADATION_TYPE.ToLower(), DB_NAME_GRADATION_TYPE);
                map.put(PROPERTY_NAME_CROSS_SCENARIO_TARGET_ID.ToLower(), DB_NAME_CROSS_SCENARIO_TARGET_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TColorSetInfoCross"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TColorSetInfoCrossDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TColorSetInfoCrossCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TColorSetInfoCrossBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TColorSetInfoCross NewMyEntity() { return new TColorSetInfoCross(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TColorSetInfoCrossCB NewMyConditionBean() { return new TColorSetInfoCrossCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TColorSetInfoCross>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TColorSetInfoCross>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("COLOR_SET_INFO_CROSS_ID", "ColorSetInfoCrossId", new EntityPropertyColorSetInfoCrossIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TYPE_CODE", "TypeCode", new EntityPropertyTypeCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRADATION_TYPE", "GradationType", new EntityPropertyGradationTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CROSS_SCENARIO_TARGET_ID", "CrossScenarioTargetId", new EntityPropertyCrossScenarioTargetIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TColorSetInfoCross> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TColorSetInfoCross)entity, value);
        }

        public class EntityPropertyColorSetInfoCrossIdSetupper : EntityPropertySetupper<TColorSetInfoCross> {
            public void Setup(TColorSetInfoCross entity, Object value) { entity.ColorSetInfoCrossId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTypeCodeSetupper : EntityPropertySetupper<TColorSetInfoCross> {
            public void Setup(TColorSetInfoCross entity, Object value) { entity.TypeCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGradationTypeSetupper : EntityPropertySetupper<TColorSetInfoCross> {
            public void Setup(TColorSetInfoCross entity, Object value) { entity.GradationType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCrossScenarioTargetIdSetupper : EntityPropertySetupper<TColorSetInfoCross> {
            public void Setup(TColorSetInfoCross entity, Object value) { entity.CrossScenarioTargetId = (value != null) ? (decimal?)value : null; }
        }
    }
}
