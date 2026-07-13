
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

    public class TEditMenuMasterDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TEditMenuMaster);

        private static readonly TEditMenuMasterDbm _instance = new TEditMenuMasterDbm();
        private TEditMenuMasterDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TEditMenuMasterDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_EDIT_MENU_MASTER"; } }
        public override String TablePropertyName { get { return "TEditMenuMaster"; } }
        public override String TableSqlName { get { return "T_EDIT_MENU_MASTER"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnEditMenuMasterId;
        protected ColumnInfo _columnEditClassification;
        protected ColumnInfo _columnProcessType;
        protected ColumnInfo _columnExplanation;
        protected ColumnInfo _columnExample;
        protected ColumnInfo _columnDetailedexplanation;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnTypeBitUnion;

        public ColumnInfo ColumnEditMenuMasterId { get { return _columnEditMenuMasterId; } }
        public ColumnInfo ColumnEditClassification { get { return _columnEditClassification; } }
        public ColumnInfo ColumnProcessType { get { return _columnProcessType; } }
        public ColumnInfo ColumnExplanation { get { return _columnExplanation; } }
        public ColumnInfo ColumnExample { get { return _columnExample; } }
        public ColumnInfo ColumnDetailedexplanation { get { return _columnDetailedexplanation; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnTypeBitUnion { get { return _columnTypeBitUnion; } }

        protected void InitializeColumnInfo() {
            _columnEditMenuMasterId = cci("EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", null, null, true, "EditMenuMasterId", typeof(int?), true, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, "TDataEditListList");
            _columnEditClassification = cci("EDIT_CLASSIFICATION", "EDIT_CLASSIFICATION", null, null, false, "EditClassification", typeof(String), false, "VARCHAR2", 60, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnProcessType = cci("PROCESS_TYPE", "PROCESS_TYPE", null, null, false, "ProcessType", typeof(String), false, "VARCHAR2", 60, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExplanation = cci("EXPLANATION", "EXPLANATION", null, null, false, "Explanation", typeof(String), false, "VARCHAR2", 60, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExample = cci("EXAMPLE", "EXAMPLE", null, null, false, "Example", typeof(String), false, "VARCHAR2", 60, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDetailedexplanation = cci("DETAILEDEXPLANATION", "DETAILEDEXPLANATION", null, null, false, "Detailedexplanation", typeof(String), false, "VARCHAR2", 60, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTypeBitUnion = cci("TYPE_BIT_UNION", "TYPE_BIT_UNION", null, null, false, "TypeBitUnion", typeof(String), false, "VARCHAR2", 10, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnEditMenuMasterId);
            _columnInfoList.add(ColumnEditClassification);
            _columnInfoList.add(ColumnProcessType);
            _columnInfoList.add(ColumnExplanation);
            _columnInfoList.add(ColumnExample);
            _columnInfoList.add(ColumnDetailedexplanation);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnTypeBitUnion);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnEditMenuMasterId);
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


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTDataEditListList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnEditMenuMasterId, TDataEditListDbm.GetInstance().ColumnEditMenuMasterId);
            return cri("TDataEditListList", this, TDataEditListDbm.GetInstance(), map, false);
        }}

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
        public static readonly String TABLE_DB_NAME = "T_EDIT_MENU_MASTER";
        public static readonly String TABLE_PROPERTY_NAME = "TEditMenuMaster";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_EDIT_MENU_MASTER_ID = "EDIT_MENU_MASTER_ID";
        public static readonly String DB_NAME_EDIT_CLASSIFICATION = "EDIT_CLASSIFICATION";
        public static readonly String DB_NAME_PROCESS_TYPE = "PROCESS_TYPE";
        public static readonly String DB_NAME_EXPLANATION = "EXPLANATION";
        public static readonly String DB_NAME_EXAMPLE = "EXAMPLE";
        public static readonly String DB_NAME_DETAILEDEXPLANATION = "DETAILEDEXPLANATION";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_TYPE_BIT_UNION = "TYPE_BIT_UNION";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_EDIT_MENU_MASTER_ID = "EditMenuMasterId";
        public static readonly String PROPERTY_NAME_EDIT_CLASSIFICATION = "EditClassification";
        public static readonly String PROPERTY_NAME_PROCESS_TYPE = "ProcessType";
        public static readonly String PROPERTY_NAME_EXPLANATION = "Explanation";
        public static readonly String PROPERTY_NAME_EXAMPLE = "Example";
        public static readonly String PROPERTY_NAME_DETAILEDEXPLANATION = "Detailedexplanation";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_TYPE_BIT_UNION = "TypeBitUnion";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TDataEditListList = "TDataEditListList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TEditMenuMasterDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_EDIT_MENU_MASTER_ID.ToLower(), PROPERTY_NAME_EDIT_MENU_MASTER_ID);
                map.put(DB_NAME_EDIT_CLASSIFICATION.ToLower(), PROPERTY_NAME_EDIT_CLASSIFICATION);
                map.put(DB_NAME_PROCESS_TYPE.ToLower(), PROPERTY_NAME_PROCESS_TYPE);
                map.put(DB_NAME_EXPLANATION.ToLower(), PROPERTY_NAME_EXPLANATION);
                map.put(DB_NAME_EXAMPLE.ToLower(), PROPERTY_NAME_EXAMPLE);
                map.put(DB_NAME_DETAILEDEXPLANATION.ToLower(), PROPERTY_NAME_DETAILEDEXPLANATION);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_TYPE_BIT_UNION.ToLower(), PROPERTY_NAME_TYPE_BIT_UNION);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_EDIT_MENU_MASTER_ID.ToLower(), DB_NAME_EDIT_MENU_MASTER_ID);
                map.put(PROPERTY_NAME_EDIT_CLASSIFICATION.ToLower(), DB_NAME_EDIT_CLASSIFICATION);
                map.put(PROPERTY_NAME_PROCESS_TYPE.ToLower(), DB_NAME_PROCESS_TYPE);
                map.put(PROPERTY_NAME_EXPLANATION.ToLower(), DB_NAME_EXPLANATION);
                map.put(PROPERTY_NAME_EXAMPLE.ToLower(), DB_NAME_EXAMPLE);
                map.put(PROPERTY_NAME_DETAILEDEXPLANATION.ToLower(), DB_NAME_DETAILEDEXPLANATION);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_TYPE_BIT_UNION.ToLower(), DB_NAME_TYPE_BIT_UNION);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TEditMenuMaster"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TEditMenuMasterDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TEditMenuMasterCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TEditMenuMasterBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TEditMenuMaster NewMyEntity() { return new TEditMenuMaster(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TEditMenuMasterCB NewMyConditionBean() { return new TEditMenuMasterCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TEditMenuMaster>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TEditMenuMaster>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("EDIT_MENU_MASTER_ID", "EditMenuMasterId", new EntityPropertyEditMenuMasterIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EDIT_CLASSIFICATION", "EditClassification", new EntityPropertyEditClassificationSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PROCESS_TYPE", "ProcessType", new EntityPropertyProcessTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXPLANATION", "Explanation", new EntityPropertyExplanationSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXAMPLE", "Example", new EntityPropertyExampleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DETAILEDEXPLANATION", "Detailedexplanation", new EntityPropertyDetailedexplanationSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TYPE_BIT_UNION", "TypeBitUnion", new EntityPropertyTypeBitUnionSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TEditMenuMaster> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TEditMenuMaster)entity, value);
        }

        public class EntityPropertyEditMenuMasterIdSetupper : EntityPropertySetupper<TEditMenuMaster> {
            public void Setup(TEditMenuMaster entity, Object value) { entity.EditMenuMasterId = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyEditClassificationSetupper : EntityPropertySetupper<TEditMenuMaster> {
            public void Setup(TEditMenuMaster entity, Object value) { entity.EditClassification = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyProcessTypeSetupper : EntityPropertySetupper<TEditMenuMaster> {
            public void Setup(TEditMenuMaster entity, Object value) { entity.ProcessType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyExplanationSetupper : EntityPropertySetupper<TEditMenuMaster> {
            public void Setup(TEditMenuMaster entity, Object value) { entity.Explanation = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyExampleSetupper : EntityPropertySetupper<TEditMenuMaster> {
            public void Setup(TEditMenuMaster entity, Object value) { entity.Example = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDetailedexplanationSetupper : EntityPropertySetupper<TEditMenuMaster> {
            public void Setup(TEditMenuMaster entity, Object value) { entity.Detailedexplanation = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TEditMenuMaster> {
            public void Setup(TEditMenuMaster entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTypeBitUnionSetupper : EntityPropertySetupper<TEditMenuMaster> {
            public void Setup(TEditMenuMaster entity, Object value) { entity.TypeBitUnion = (value != null) ? (String)value : null; }
        }
    }
}
