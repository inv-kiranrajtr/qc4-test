
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

    public class TCategoryOutputDetailDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TCategoryOutputDetail);

        private static readonly TCategoryOutputDetailDbm _instance = new TCategoryOutputDetailDbm();
        private TCategoryOutputDetailDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TCategoryOutputDetailDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_CATEGORY_OUTPUT_DETAIL"; } }
        public override String TablePropertyName { get { return "TCategoryOutputDetail"; } }
        public override String TableSqlName { get { return "T_CATEGORY_OUTPUT_DETAIL"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnCategoryOutputEditDetailId;
        protected ColumnInfo _columnCategoryOutputEditId;
        protected ColumnInfo _columnOldCategoryNo;
        protected ColumnInfo _columnNewCategoryNo;

        public ColumnInfo ColumnCategoryOutputEditDetailId { get { return _columnCategoryOutputEditDetailId; } }
        public ColumnInfo ColumnCategoryOutputEditId { get { return _columnCategoryOutputEditId; } }
        public ColumnInfo ColumnOldCategoryNo { get { return _columnOldCategoryNo; } }
        public ColumnInfo ColumnNewCategoryNo { get { return _columnNewCategoryNo; } }

        protected void InitializeColumnInfo() {
            _columnCategoryOutputEditDetailId = cci("CATEGORY_OUTPUT_EDIT_DETAIL_ID", "CATEGORY_OUTPUT_EDIT_DETAIL_ID", null, null, true, "CategoryOutputEditDetailId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCategoryOutputEditId = cci("CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", null, null, true, "CategoryOutputEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TCategoryOutputEdit", null);
            _columnOldCategoryNo = cci("OLD_CATEGORY_NO", "OLD_CATEGORY_NO", null, null, true, "OldCategoryNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewCategoryNo = cci("NEW_CATEGORY_NO", "NEW_CATEGORY_NO", null, null, true, "NewCategoryNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnCategoryOutputEditDetailId);
            _columnInfoList.add(ColumnCategoryOutputEditId);
            _columnInfoList.add(ColumnOldCategoryNo);
            _columnInfoList.add(ColumnNewCategoryNo);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnCategoryOutputEditDetailId);
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
        public ForeignInfo ForeignTCategoryOutputEdit { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCategoryOutputEditId, TCategoryOutputEditDbm.GetInstance().ColumnCategoryOutputEditId);
            return cfi("TCategoryOutputEdit", this, TCategoryOutputEditDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Category_Output_Detail_SEQ01"; } }
        public override String SequenceNextValSql { get { return "select T_Category_Output_Detail_SEQ01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_CATEGORY_OUTPUT_DETAIL";
        public static readonly String TABLE_PROPERTY_NAME = "TCategoryOutputDetail";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_CATEGORY_OUTPUT_EDIT_DETAIL_ID = "CATEGORY_OUTPUT_EDIT_DETAIL_ID";
        public static readonly String DB_NAME_CATEGORY_OUTPUT_EDIT_ID = "CATEGORY_OUTPUT_EDIT_ID";
        public static readonly String DB_NAME_OLD_CATEGORY_NO = "OLD_CATEGORY_NO";
        public static readonly String DB_NAME_NEW_CATEGORY_NO = "NEW_CATEGORY_NO";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_CATEGORY_OUTPUT_EDIT_DETAIL_ID = "CategoryOutputEditDetailId";
        public static readonly String PROPERTY_NAME_CATEGORY_OUTPUT_EDIT_ID = "CategoryOutputEditId";
        public static readonly String PROPERTY_NAME_OLD_CATEGORY_NO = "OldCategoryNo";
        public static readonly String PROPERTY_NAME_NEW_CATEGORY_NO = "NewCategoryNo";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TCategoryOutputEdit = "TCategoryOutputEdit";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TCategoryOutputDetailDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_CATEGORY_OUTPUT_EDIT_DETAIL_ID.ToLower(), PROPERTY_NAME_CATEGORY_OUTPUT_EDIT_DETAIL_ID);
                map.put(DB_NAME_CATEGORY_OUTPUT_EDIT_ID.ToLower(), PROPERTY_NAME_CATEGORY_OUTPUT_EDIT_ID);
                map.put(DB_NAME_OLD_CATEGORY_NO.ToLower(), PROPERTY_NAME_OLD_CATEGORY_NO);
                map.put(DB_NAME_NEW_CATEGORY_NO.ToLower(), PROPERTY_NAME_NEW_CATEGORY_NO);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_CATEGORY_OUTPUT_EDIT_DETAIL_ID.ToLower(), DB_NAME_CATEGORY_OUTPUT_EDIT_DETAIL_ID);
                map.put(PROPERTY_NAME_CATEGORY_OUTPUT_EDIT_ID.ToLower(), DB_NAME_CATEGORY_OUTPUT_EDIT_ID);
                map.put(PROPERTY_NAME_OLD_CATEGORY_NO.ToLower(), DB_NAME_OLD_CATEGORY_NO);
                map.put(PROPERTY_NAME_NEW_CATEGORY_NO.ToLower(), DB_NAME_NEW_CATEGORY_NO);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TCategoryOutputDetail"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TCategoryOutputDetailDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TCategoryOutputDetailCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TCategoryOutputDetailBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TCategoryOutputDetail NewMyEntity() { return new TCategoryOutputDetail(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TCategoryOutputDetailCB NewMyConditionBean() { return new TCategoryOutputDetailCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TCategoryOutputDetail>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TCategoryOutputDetail>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("CATEGORY_OUTPUT_EDIT_DETAIL_ID", "CategoryOutputEditDetailId", new EntityPropertyCategoryOutputEditDetailIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CATEGORY_OUTPUT_EDIT_ID", "CategoryOutputEditId", new EntityPropertyCategoryOutputEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OLD_CATEGORY_NO", "OldCategoryNo", new EntityPropertyOldCategoryNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_CATEGORY_NO", "NewCategoryNo", new EntityPropertyNewCategoryNoSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TCategoryOutputDetail> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TCategoryOutputDetail)entity, value);
        }

        public class EntityPropertyCategoryOutputEditDetailIdSetupper : EntityPropertySetupper<TCategoryOutputDetail> {
            public void Setup(TCategoryOutputDetail entity, Object value) { entity.CategoryOutputEditDetailId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyCategoryOutputEditIdSetupper : EntityPropertySetupper<TCategoryOutputDetail> {
            public void Setup(TCategoryOutputDetail entity, Object value) { entity.CategoryOutputEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOldCategoryNoSetupper : EntityPropertySetupper<TCategoryOutputDetail> {
            public void Setup(TCategoryOutputDetail entity, Object value) { entity.OldCategoryNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNewCategoryNoSetupper : EntityPropertySetupper<TCategoryOutputDetail> {
            public void Setup(TCategoryOutputDetail entity, Object value) { entity.NewCategoryNo = (value != null) ? (int?)value : null; }
        }
    }
}
