
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

    public class TGtMatrixChildDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TGtMatrixChild);

        private static readonly TGtMatrixChildDbm _instance = new TGtMatrixChildDbm();
        private TGtMatrixChildDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TGtMatrixChildDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_GT_MATRIX_CHILD"; } }
        public override String TablePropertyName { get { return "TGtMatrixChild"; } }
        public override String TableSqlName { get { return "T_GT_MATRIX_CHILD"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnGtMatrixChildid;
        protected ColumnInfo _columnGtMatrixInfoId;
        protected ColumnInfo _columnChildItemId;

        public ColumnInfo ColumnGtMatrixChildid { get { return _columnGtMatrixChildid; } }
        public ColumnInfo ColumnGtMatrixInfoId { get { return _columnGtMatrixInfoId; } }
        public ColumnInfo ColumnChildItemId { get { return _columnChildItemId; } }

        protected void InitializeColumnInfo() {
            _columnGtMatrixChildid = cci("GT_MATRIX_CHILDID", "GT_MATRIX_CHILDID", null, null, true, "GtMatrixChildid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGtMatrixInfoId = cci("GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", null, null, true, "GtMatrixInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TGtMatrixInfo", null);
            _columnChildItemId = cci("CHILD_ITEM_ID", "CHILD_ITEM_ID", null, null, true, "ChildItemId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TItemInfo", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnGtMatrixChildid);
            _columnInfoList.add(ColumnGtMatrixInfoId);
            _columnInfoList.add(ColumnChildItemId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnGtMatrixChildid);
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
        public ForeignInfo ForeignTGtMatrixInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnGtMatrixInfoId, TGtMatrixInfoDbm.GetInstance().ColumnGtMatrixInfoId);
            return cfi("TGtMatrixInfo", this, TGtMatrixInfoDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTItemInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnChildItemId, TItemInfoDbm.GetInstance().ColumnItemInfoId);
            return cfi("TItemInfo", this, TItemInfoDbm.GetInstance(), map, 1, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_GT_Matrix_Child_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_GT_Matrix_Child_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_GT_MATRIX_CHILD";
        public static readonly String TABLE_PROPERTY_NAME = "TGtMatrixChild";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_GT_MATRIX_CHILDID = "GT_MATRIX_CHILDID";
        public static readonly String DB_NAME_GT_MATRIX_INFO_ID = "GT_MATRIX_INFO_ID";
        public static readonly String DB_NAME_CHILD_ITEM_ID = "CHILD_ITEM_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_GT_MATRIX_CHILDID = "GtMatrixChildid";
        public static readonly String PROPERTY_NAME_GT_MATRIX_INFO_ID = "GtMatrixInfoId";
        public static readonly String PROPERTY_NAME_CHILD_ITEM_ID = "ChildItemId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TGtMatrixInfo = "TGtMatrixInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TItemInfo = "TItemInfo";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TGtMatrixChildDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_GT_MATRIX_CHILDID.ToLower(), PROPERTY_NAME_GT_MATRIX_CHILDID);
                map.put(DB_NAME_GT_MATRIX_INFO_ID.ToLower(), PROPERTY_NAME_GT_MATRIX_INFO_ID);
                map.put(DB_NAME_CHILD_ITEM_ID.ToLower(), PROPERTY_NAME_CHILD_ITEM_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_GT_MATRIX_CHILDID.ToLower(), DB_NAME_GT_MATRIX_CHILDID);
                map.put(PROPERTY_NAME_GT_MATRIX_INFO_ID.ToLower(), DB_NAME_GT_MATRIX_INFO_ID);
                map.put(PROPERTY_NAME_CHILD_ITEM_ID.ToLower(), DB_NAME_CHILD_ITEM_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TGtMatrixChild"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TGtMatrixChildDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TGtMatrixChildCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TGtMatrixChildBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TGtMatrixChild NewMyEntity() { return new TGtMatrixChild(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TGtMatrixChildCB NewMyConditionBean() { return new TGtMatrixChildCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TGtMatrixChild>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TGtMatrixChild>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("GT_MATRIX_CHILDID", "GtMatrixChildid", new EntityPropertyGtMatrixChildidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GT_MATRIX_INFO_ID", "GtMatrixInfoId", new EntityPropertyGtMatrixInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CHILD_ITEM_ID", "ChildItemId", new EntityPropertyChildItemIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TGtMatrixChild> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TGtMatrixChild)entity, value);
        }

        public class EntityPropertyGtMatrixChildidSetupper : EntityPropertySetupper<TGtMatrixChild> {
            public void Setup(TGtMatrixChild entity, Object value) { entity.GtMatrixChildid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyGtMatrixInfoIdSetupper : EntityPropertySetupper<TGtMatrixChild> {
            public void Setup(TGtMatrixChild entity, Object value) { entity.GtMatrixInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyChildItemIdSetupper : EntityPropertySetupper<TGtMatrixChild> {
            public void Setup(TGtMatrixChild entity, Object value) { entity.ChildItemId = (value != null) ? (decimal?)value : null; }
        }
    }
}
