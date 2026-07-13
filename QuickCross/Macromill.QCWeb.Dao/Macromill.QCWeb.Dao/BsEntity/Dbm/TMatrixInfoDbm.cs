
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

    public class TMatrixInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TMatrixInfo);

        private static readonly TMatrixInfoDbm _instance = new TMatrixInfoDbm();
        private TMatrixInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TMatrixInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_MATRIX_INFO"; } }
        public override String TablePropertyName { get { return "TMatrixInfo"; } }
        public override String TableSqlName { get { return "T_MATRIX_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnMatrixInfoId;
        protected ColumnInfo _columnItemInfoId;
        protected ColumnInfo _columnChildItemInfoId;
        protected ColumnInfo _columnAddFaItemInfoId;
        protected ColumnInfo _columnAddFaCategoryInfoId;

        public ColumnInfo ColumnMatrixInfoId { get { return _columnMatrixInfoId; } }
        public ColumnInfo ColumnItemInfoId { get { return _columnItemInfoId; } }
        public ColumnInfo ColumnChildItemInfoId { get { return _columnChildItemInfoId; } }
        public ColumnInfo ColumnAddFaItemInfoId { get { return _columnAddFaItemInfoId; } }
        public ColumnInfo ColumnAddFaCategoryInfoId { get { return _columnAddFaCategoryInfoId; } }

        protected void InitializeColumnInfo() {
            _columnMatrixInfoId = cci("MATRIX_INFO_ID", "MATRIX_INFO_ID", null, null, true, "MatrixInfoId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemInfoId = cci("ITEM_INFO_ID", "ITEM_INFO_ID", null, null, true, "ItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TItemInfoByItemInfoId", null);
            _columnChildItemInfoId = cci("CHILD_ITEM_INFO_ID", "CHILD_ITEM_INFO_ID", null, null, true, "ChildItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TItemInfoByChildItemInfoId", null);
            _columnAddFaItemInfoId = cci("ADD_FA_ITEM_INFO_ID", "ADD_FA_ITEM_INFO_ID", null, null, false, "AddFaItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAddFaCategoryInfoId = cci("ADD_FA_CATEGORY_INFO_ID", "ADD_FA_CATEGORY_INFO_ID", null, null, false, "AddFaCategoryInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TCategoryInfo", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnMatrixInfoId);
            _columnInfoList.add(ColumnItemInfoId);
            _columnInfoList.add(ColumnChildItemInfoId);
            _columnInfoList.add(ColumnAddFaItemInfoId);
            _columnInfoList.add(ColumnAddFaCategoryInfoId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnMatrixInfoId);
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
        public ForeignInfo ForeignTItemInfoByItemInfoId { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TItemInfoDbm.GetInstance().ColumnItemInfoId);
            return cfi("TItemInfoByItemInfoId", this, TItemInfoDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTItemInfoByChildItemInfoId { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnChildItemInfoId, TItemInfoDbm.GetInstance().ColumnItemInfoId);
            return cfi("TItemInfoByChildItemInfoId", this, TItemInfoDbm.GetInstance(), map, 1, false, false);
        }}
        public ForeignInfo ForeignTCategoryInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnAddFaCategoryInfoId, TCategoryInfoDbm.GetInstance().ColumnCategoryInfoId);
            return cfi("TCategoryInfo", this, TCategoryInfoDbm.GetInstance(), map, 2, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Matrix_Info_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Matrix_Info_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_MATRIX_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TMatrixInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_MATRIX_INFO_ID = "MATRIX_INFO_ID";
        public static readonly String DB_NAME_ITEM_INFO_ID = "ITEM_INFO_ID";
        public static readonly String DB_NAME_CHILD_ITEM_INFO_ID = "CHILD_ITEM_INFO_ID";
        public static readonly String DB_NAME_ADD_FA_ITEM_INFO_ID = "ADD_FA_ITEM_INFO_ID";
        public static readonly String DB_NAME_ADD_FA_CATEGORY_INFO_ID = "ADD_FA_CATEGORY_INFO_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_MATRIX_INFO_ID = "MatrixInfoId";
        public static readonly String PROPERTY_NAME_ITEM_INFO_ID = "ItemInfoId";
        public static readonly String PROPERTY_NAME_CHILD_ITEM_INFO_ID = "ChildItemInfoId";
        public static readonly String PROPERTY_NAME_ADD_FA_ITEM_INFO_ID = "AddFaItemInfoId";
        public static readonly String PROPERTY_NAME_ADD_FA_CATEGORY_INFO_ID = "AddFaCategoryInfoId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TItemInfoByItemInfoId = "TItemInfoByItemInfoId";
        public static readonly String FOREIGN_PROPERTY_NAME_TItemInfoByChildItemInfoId = "TItemInfoByChildItemInfoId";
        public static readonly String FOREIGN_PROPERTY_NAME_TCategoryInfo = "TCategoryInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TMatrixInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_MATRIX_INFO_ID.ToLower(), PROPERTY_NAME_MATRIX_INFO_ID);
                map.put(DB_NAME_ITEM_INFO_ID.ToLower(), PROPERTY_NAME_ITEM_INFO_ID);
                map.put(DB_NAME_CHILD_ITEM_INFO_ID.ToLower(), PROPERTY_NAME_CHILD_ITEM_INFO_ID);
                map.put(DB_NAME_ADD_FA_ITEM_INFO_ID.ToLower(), PROPERTY_NAME_ADD_FA_ITEM_INFO_ID);
                map.put(DB_NAME_ADD_FA_CATEGORY_INFO_ID.ToLower(), PROPERTY_NAME_ADD_FA_CATEGORY_INFO_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_MATRIX_INFO_ID.ToLower(), DB_NAME_MATRIX_INFO_ID);
                map.put(PROPERTY_NAME_ITEM_INFO_ID.ToLower(), DB_NAME_ITEM_INFO_ID);
                map.put(PROPERTY_NAME_CHILD_ITEM_INFO_ID.ToLower(), DB_NAME_CHILD_ITEM_INFO_ID);
                map.put(PROPERTY_NAME_ADD_FA_ITEM_INFO_ID.ToLower(), DB_NAME_ADD_FA_ITEM_INFO_ID);
                map.put(PROPERTY_NAME_ADD_FA_CATEGORY_INFO_ID.ToLower(), DB_NAME_ADD_FA_CATEGORY_INFO_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TMatrixInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TMatrixInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TMatrixInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TMatrixInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TMatrixInfo NewMyEntity() { return new TMatrixInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TMatrixInfoCB NewMyConditionBean() { return new TMatrixInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TMatrixInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TMatrixInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("MATRIX_INFO_ID", "MatrixInfoId", new EntityPropertyMatrixInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_INFO_ID", "ItemInfoId", new EntityPropertyItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CHILD_ITEM_INFO_ID", "ChildItemInfoId", new EntityPropertyChildItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ADD_FA_ITEM_INFO_ID", "AddFaItemInfoId", new EntityPropertyAddFaItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ADD_FA_CATEGORY_INFO_ID", "AddFaCategoryInfoId", new EntityPropertyAddFaCategoryInfoIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TMatrixInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TMatrixInfo)entity, value);
        }

        public class EntityPropertyMatrixInfoIdSetupper : EntityPropertySetupper<TMatrixInfo> {
            public void Setup(TMatrixInfo entity, Object value) { entity.MatrixInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyItemInfoIdSetupper : EntityPropertySetupper<TMatrixInfo> {
            public void Setup(TMatrixInfo entity, Object value) { entity.ItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyChildItemInfoIdSetupper : EntityPropertySetupper<TMatrixInfo> {
            public void Setup(TMatrixInfo entity, Object value) { entity.ChildItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAddFaItemInfoIdSetupper : EntityPropertySetupper<TMatrixInfo> {
            public void Setup(TMatrixInfo entity, Object value) { entity.AddFaItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAddFaCategoryInfoIdSetupper : EntityPropertySetupper<TMatrixInfo> {
            public void Setup(TMatrixInfo entity, Object value) { entity.AddFaCategoryInfoId = (value != null) ? (decimal?)value : null; }
        }
    }
}
