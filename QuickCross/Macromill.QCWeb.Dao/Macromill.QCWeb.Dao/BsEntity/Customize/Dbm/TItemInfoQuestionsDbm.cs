
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class TItemInfoQuestionsDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TItemInfoQuestions);

        private static readonly TItemInfoQuestionsDbm _instance = new TItemInfoQuestionsDbm();
        private TItemInfoQuestionsDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TItemInfoQuestionsDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "TItemInfoQuestions"; } }
        public override String TablePropertyName { get { return "TItemInfoQuestions"; } }
        public override String TableSqlName { get { return "TItemInfoQuestions"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnItemInfoId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnItemName;
        protected ColumnInfo _columnSourceDiv;
        protected ColumnInfo _columnItemno;
        protected ColumnInfo _columnItemType;
        protected ColumnInfo _columnAnswerType;
        protected ColumnInfo _columnMatrixDiv;
        protected ColumnInfo _columnLv1title;
        protected ColumnInfo _columnLv2title;
        protected ColumnInfo _columnOriginalLv1title;
        protected ColumnInfo _columnOriginalLv2title;
        protected ColumnInfo _columnTableName;
        protected ColumnInfo _columnColumnName;
        protected ColumnInfo _columnStatus;
        protected ColumnInfo _columnSortFlag;
        protected ColumnInfo _columnSortRange;
        protected ColumnInfo _columnLastUpdateDatetime;
        protected ColumnInfo _columnParentIteminfoId;
        protected ColumnInfo _columnAddFaCategoryInfoId;
        protected ColumnInfo _columnBaseTableName;

        public ColumnInfo ColumnItemInfoId { get { return _columnItemInfoId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnItemName { get { return _columnItemName; } }
        public ColumnInfo ColumnSourceDiv { get { return _columnSourceDiv; } }
        public ColumnInfo ColumnItemno { get { return _columnItemno; } }
        public ColumnInfo ColumnItemType { get { return _columnItemType; } }
        public ColumnInfo ColumnAnswerType { get { return _columnAnswerType; } }
        public ColumnInfo ColumnMatrixDiv { get { return _columnMatrixDiv; } }
        public ColumnInfo ColumnLv1title { get { return _columnLv1title; } }
        public ColumnInfo ColumnLv2title { get { return _columnLv2title; } }
        public ColumnInfo ColumnOriginalLv1title { get { return _columnOriginalLv1title; } }
        public ColumnInfo ColumnOriginalLv2title { get { return _columnOriginalLv2title; } }
        public ColumnInfo ColumnTableName { get { return _columnTableName; } }
        public ColumnInfo ColumnColumnName { get { return _columnColumnName; } }
        public ColumnInfo ColumnStatus { get { return _columnStatus; } }
        public ColumnInfo ColumnSortFlag { get { return _columnSortFlag; } }
        public ColumnInfo ColumnSortRange { get { return _columnSortRange; } }
        public ColumnInfo ColumnLastUpdateDatetime { get { return _columnLastUpdateDatetime; } }
        public ColumnInfo ColumnParentIteminfoId { get { return _columnParentIteminfoId; } }
        public ColumnInfo ColumnAddFaCategoryInfoId { get { return _columnAddFaCategoryInfoId; } }
        public ColumnInfo ColumnBaseTableName { get { return _columnBaseTableName; } }

        protected void InitializeColumnInfo() {
            _columnItemInfoId = cci("ITEM_INFO_ID", "ITEM_INFO_ID", null, null, false, "ItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, false, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemName = cci("ITEM_NAME", "ITEM_NAME", null, null, false, "ItemName", typeof(String), false, "VARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSourceDiv = cci("SOURCE_DIV", "SOURCE_DIV", null, null, false, "SourceDiv", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemno = cci("ITEMNO", "ITEMNO", null, null, false, "Itemno", typeof(String), false, "VARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemType = cci("ITEM_TYPE", "ITEM_TYPE", null, null, false, "ItemType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAnswerType = cci("ANSWER_TYPE", "ANSWER_TYPE", null, null, false, "AnswerType", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMatrixDiv = cci("MATRIX_DIV", "MATRIX_DIV", null, null, false, "MatrixDiv", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLv1title = cci("LV1TITLE", "LV1TITLE", null, null, false, "Lv1title", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLv2title = cci("LV2TITLE", "LV2TITLE", null, null, false, "Lv2title", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOriginalLv1title = cci("ORIGINAL_LV1TITLE", "ORIGINAL_LV1TITLE", null, null, false, "OriginalLv1title", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOriginalLv2title = cci("ORIGINAL_LV2TITLE", "ORIGINAL_LV2TITLE", null, null, false, "OriginalLv2title", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTableName = cci("TABLE_NAME", "TABLE_NAME", null, null, false, "TableName", typeof(String), false, "VARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColumnName = cci("COLUMN_NAME", "COLUMN_NAME", null, null, false, "ColumnName", typeof(String), false, "VARCHAR2", 30, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnStatus = cci("STATUS", "STATUS", null, null, false, "Status", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortFlag = cci("SORT_FLAG", "SORT_FLAG", null, null, false, "SortFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortRange = cci("SORT_RANGE", "SORT_RANGE", null, null, false, "SortRange", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLastUpdateDatetime = cci("LAST_UPDATE_DATETIME", "LAST_UPDATE_DATETIME", null, null, false, "LastUpdateDatetime", typeof(DateTime?), false, "TIMESTAMP", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnParentIteminfoId = cci("PARENT_ITEMINFO_ID", "PARENT_ITEMINFO_ID", null, null, false, "ParentIteminfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAddFaCategoryInfoId = cci("ADD_FA_CATEGORY_INFO_ID", "ADD_FA_CATEGORY_INFO_ID", null, null, false, "AddFaCategoryInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnBaseTableName = cci("BASE_TABLE_NAME", "BASE_TABLE_NAME", null, null, false, "BaseTableName", typeof(String), false, "VARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnItemInfoId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnItemName);
            _columnInfoList.add(ColumnSourceDiv);
            _columnInfoList.add(ColumnItemno);
            _columnInfoList.add(ColumnItemType);
            _columnInfoList.add(ColumnAnswerType);
            _columnInfoList.add(ColumnMatrixDiv);
            _columnInfoList.add(ColumnLv1title);
            _columnInfoList.add(ColumnLv2title);
            _columnInfoList.add(ColumnOriginalLv1title);
            _columnInfoList.add(ColumnOriginalLv2title);
            _columnInfoList.add(ColumnTableName);
            _columnInfoList.add(ColumnColumnName);
            _columnInfoList.add(ColumnStatus);
            _columnInfoList.add(ColumnSortFlag);
            _columnInfoList.add(ColumnSortRange);
            _columnInfoList.add(ColumnLastUpdateDatetime);
            _columnInfoList.add(ColumnParentIteminfoId);
            _columnInfoList.add(ColumnAddFaCategoryInfoId);
            _columnInfoList.add(ColumnBaseTableName);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            throw new NotSupportedException("The table does not have primary key: " + TableDbName);
        }}

        // -------------------------------------------------
        //                                   Primary Element
        //                                   ---------------
        public override bool HasPrimaryKey { get { return false; } }
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
        public static readonly String TABLE_DB_NAME = "TItemInfoQuestions";
        public static readonly String TABLE_PROPERTY_NAME = "TItemInfoQuestions";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_ITEM_INFO_ID = "ITEM_INFO_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_ITEM_NAME = "ITEM_NAME";
        public static readonly String DB_NAME_SOURCE_DIV = "SOURCE_DIV";
        public static readonly String DB_NAME_ITEMNO = "ITEMNO";
        public static readonly String DB_NAME_ITEM_TYPE = "ITEM_TYPE";
        public static readonly String DB_NAME_ANSWER_TYPE = "ANSWER_TYPE";
        public static readonly String DB_NAME_MATRIX_DIV = "MATRIX_DIV";
        public static readonly String DB_NAME_LV1TITLE = "LV1TITLE";
        public static readonly String DB_NAME_LV2TITLE = "LV2TITLE";
        public static readonly String DB_NAME_ORIGINAL_LV1TITLE = "ORIGINAL_LV1TITLE";
        public static readonly String DB_NAME_ORIGINAL_LV2TITLE = "ORIGINAL_LV2TITLE";
        public static readonly String DB_NAME_TABLE_NAME = "TABLE_NAME";
        public static readonly String DB_NAME_COLUMN_NAME = "COLUMN_NAME";
        public static readonly String DB_NAME_STATUS = "STATUS";
        public static readonly String DB_NAME_SORT_FLAG = "SORT_FLAG";
        public static readonly String DB_NAME_SORT_RANGE = "SORT_RANGE";
        public static readonly String DB_NAME_LAST_UPDATE_DATETIME = "LAST_UPDATE_DATETIME";
        public static readonly String DB_NAME_PARENT_ITEMINFO_ID = "PARENT_ITEMINFO_ID";
        public static readonly String DB_NAME_ADD_FA_CATEGORY_INFO_ID = "ADD_FA_CATEGORY_INFO_ID";
        public static readonly String DB_NAME_BASE_TABLE_NAME = "BASE_TABLE_NAME";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_ITEM_INFO_ID = "ItemInfoId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_ITEM_NAME = "ItemName";
        public static readonly String PROPERTY_NAME_SOURCE_DIV = "SourceDiv";
        public static readonly String PROPERTY_NAME_ITEMNO = "Itemno";
        public static readonly String PROPERTY_NAME_ITEM_TYPE = "ItemType";
        public static readonly String PROPERTY_NAME_ANSWER_TYPE = "AnswerType";
        public static readonly String PROPERTY_NAME_MATRIX_DIV = "MatrixDiv";
        public static readonly String PROPERTY_NAME_LV1TITLE = "Lv1title";
        public static readonly String PROPERTY_NAME_LV2TITLE = "Lv2title";
        public static readonly String PROPERTY_NAME_ORIGINAL_LV1TITLE = "OriginalLv1title";
        public static readonly String PROPERTY_NAME_ORIGINAL_LV2TITLE = "OriginalLv2title";
        public static readonly String PROPERTY_NAME_TABLE_NAME = "TableName";
        public static readonly String PROPERTY_NAME_COLUMN_NAME = "ColumnName";
        public static readonly String PROPERTY_NAME_STATUS = "Status";
        public static readonly String PROPERTY_NAME_SORT_FLAG = "SortFlag";
        public static readonly String PROPERTY_NAME_SORT_RANGE = "SortRange";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_DATETIME = "LastUpdateDatetime";
        public static readonly String PROPERTY_NAME_PARENT_ITEMINFO_ID = "ParentIteminfoId";
        public static readonly String PROPERTY_NAME_ADD_FA_CATEGORY_INFO_ID = "AddFaCategoryInfoId";
        public static readonly String PROPERTY_NAME_BASE_TABLE_NAME = "BaseTableName";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TItemInfoQuestionsDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_ITEM_INFO_ID.ToLower(), PROPERTY_NAME_ITEM_INFO_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_ITEM_NAME.ToLower(), PROPERTY_NAME_ITEM_NAME);
                map.put(DB_NAME_SOURCE_DIV.ToLower(), PROPERTY_NAME_SOURCE_DIV);
                map.put(DB_NAME_ITEMNO.ToLower(), PROPERTY_NAME_ITEMNO);
                map.put(DB_NAME_ITEM_TYPE.ToLower(), PROPERTY_NAME_ITEM_TYPE);
                map.put(DB_NAME_ANSWER_TYPE.ToLower(), PROPERTY_NAME_ANSWER_TYPE);
                map.put(DB_NAME_MATRIX_DIV.ToLower(), PROPERTY_NAME_MATRIX_DIV);
                map.put(DB_NAME_LV1TITLE.ToLower(), PROPERTY_NAME_LV1TITLE);
                map.put(DB_NAME_LV2TITLE.ToLower(), PROPERTY_NAME_LV2TITLE);
                map.put(DB_NAME_ORIGINAL_LV1TITLE.ToLower(), PROPERTY_NAME_ORIGINAL_LV1TITLE);
                map.put(DB_NAME_ORIGINAL_LV2TITLE.ToLower(), PROPERTY_NAME_ORIGINAL_LV2TITLE);
                map.put(DB_NAME_TABLE_NAME.ToLower(), PROPERTY_NAME_TABLE_NAME);
                map.put(DB_NAME_COLUMN_NAME.ToLower(), PROPERTY_NAME_COLUMN_NAME);
                map.put(DB_NAME_STATUS.ToLower(), PROPERTY_NAME_STATUS);
                map.put(DB_NAME_SORT_FLAG.ToLower(), PROPERTY_NAME_SORT_FLAG);
                map.put(DB_NAME_SORT_RANGE.ToLower(), PROPERTY_NAME_SORT_RANGE);
                map.put(DB_NAME_LAST_UPDATE_DATETIME.ToLower(), PROPERTY_NAME_LAST_UPDATE_DATETIME);
                map.put(DB_NAME_PARENT_ITEMINFO_ID.ToLower(), PROPERTY_NAME_PARENT_ITEMINFO_ID);
                map.put(DB_NAME_ADD_FA_CATEGORY_INFO_ID.ToLower(), PROPERTY_NAME_ADD_FA_CATEGORY_INFO_ID);
                map.put(DB_NAME_BASE_TABLE_NAME.ToLower(), PROPERTY_NAME_BASE_TABLE_NAME);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_ITEM_INFO_ID.ToLower(), DB_NAME_ITEM_INFO_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_ITEM_NAME.ToLower(), DB_NAME_ITEM_NAME);
                map.put(PROPERTY_NAME_SOURCE_DIV.ToLower(), DB_NAME_SOURCE_DIV);
                map.put(PROPERTY_NAME_ITEMNO.ToLower(), DB_NAME_ITEMNO);
                map.put(PROPERTY_NAME_ITEM_TYPE.ToLower(), DB_NAME_ITEM_TYPE);
                map.put(PROPERTY_NAME_ANSWER_TYPE.ToLower(), DB_NAME_ANSWER_TYPE);
                map.put(PROPERTY_NAME_MATRIX_DIV.ToLower(), DB_NAME_MATRIX_DIV);
                map.put(PROPERTY_NAME_LV1TITLE.ToLower(), DB_NAME_LV1TITLE);
                map.put(PROPERTY_NAME_LV2TITLE.ToLower(), DB_NAME_LV2TITLE);
                map.put(PROPERTY_NAME_ORIGINAL_LV1TITLE.ToLower(), DB_NAME_ORIGINAL_LV1TITLE);
                map.put(PROPERTY_NAME_ORIGINAL_LV2TITLE.ToLower(), DB_NAME_ORIGINAL_LV2TITLE);
                map.put(PROPERTY_NAME_TABLE_NAME.ToLower(), DB_NAME_TABLE_NAME);
                map.put(PROPERTY_NAME_COLUMN_NAME.ToLower(), DB_NAME_COLUMN_NAME);
                map.put(PROPERTY_NAME_STATUS.ToLower(), DB_NAME_STATUS);
                map.put(PROPERTY_NAME_SORT_FLAG.ToLower(), DB_NAME_SORT_FLAG);
                map.put(PROPERTY_NAME_SORT_RANGE.ToLower(), DB_NAME_SORT_RANGE);
                map.put(PROPERTY_NAME_LAST_UPDATE_DATETIME.ToLower(), DB_NAME_LAST_UPDATE_DATETIME);
                map.put(PROPERTY_NAME_PARENT_ITEMINFO_ID.ToLower(), DB_NAME_PARENT_ITEMINFO_ID);
                map.put(PROPERTY_NAME_ADD_FA_CATEGORY_INFO_ID.ToLower(), DB_NAME_ADD_FA_CATEGORY_INFO_ID);
                map.put(PROPERTY_NAME_BASE_TABLE_NAME.ToLower(), DB_NAME_BASE_TABLE_NAME);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.TItemInfoQuestions"; } }
        public override String DaoTypeName { get { return null; } }
        public override String ConditionBeanTypeName { get { return null; } }
        public override String BehaviorTypeName { get { return null; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TItemInfoQuestions NewMyEntity() { return new TItemInfoQuestions(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TItemInfoQuestions>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TItemInfoQuestions>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("ITEM_INFO_ID", "ItemInfoId", new EntityPropertyItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_NAME", "ItemName", new EntityPropertyItemNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SOURCE_DIV", "SourceDiv", new EntityPropertySourceDivSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEMNO", "Itemno", new EntityPropertyItemnoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_TYPE", "ItemType", new EntityPropertyItemTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ANSWER_TYPE", "AnswerType", new EntityPropertyAnswerTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MATRIX_DIV", "MatrixDiv", new EntityPropertyMatrixDivSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LV1TITLE", "Lv1title", new EntityPropertyLv1titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LV2TITLE", "Lv2title", new EntityPropertyLv2titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ORIGINAL_LV1TITLE", "OriginalLv1title", new EntityPropertyOriginalLv1titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ORIGINAL_LV2TITLE", "OriginalLv2title", new EntityPropertyOriginalLv2titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TABLE_NAME", "TableName", new EntityPropertyTableNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLUMN_NAME", "ColumnName", new EntityPropertyColumnNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("STATUS", "Status", new EntityPropertyStatusSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_FLAG", "SortFlag", new EntityPropertySortFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_RANGE", "SortRange", new EntityPropertySortRangeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_DATETIME", "LastUpdateDatetime", new EntityPropertyLastUpdateDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PARENT_ITEMINFO_ID", "ParentIteminfoId", new EntityPropertyParentIteminfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ADD_FA_CATEGORY_INFO_ID", "AddFaCategoryInfoId", new EntityPropertyAddFaCategoryInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("BASE_TABLE_NAME", "BaseTableName", new EntityPropertyBaseTableNameSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TItemInfoQuestions> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TItemInfoQuestions)entity, value);
        }

        public class EntityPropertyItemInfoIdSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.ItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyItemNameSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.ItemName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySourceDivSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.SourceDiv = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyItemnoSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.Itemno = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyItemTypeSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.ItemType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyAnswerTypeSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.AnswerType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyMatrixDivSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.MatrixDiv = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLv1titleSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.Lv1title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLv2titleSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.Lv2title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOriginalLv1titleSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.OriginalLv1title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOriginalLv2titleSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.OriginalLv2title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyTableNameSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.TableName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyColumnNameSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.ColumnName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyStatusSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.Status = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySortFlagSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.SortFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySortRangeSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.SortRange = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLastUpdateDatetimeSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.LastUpdateDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyParentIteminfoIdSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.ParentIteminfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyAddFaCategoryInfoIdSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.AddFaCategoryInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyBaseTableNameSetupper : EntityPropertySetupper<TItemInfoQuestions> {
            public void Setup(TItemInfoQuestions entity, Object value) { entity.BaseTableName = (value != null) ? (String)value : null; }
        }
    }
}
