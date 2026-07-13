
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class TItemInfoQuestionsDPDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TItemInfoQuestionsDP);

        private static readonly TItemInfoQuestionsDPDbm _instance = new TItemInfoQuestionsDPDbm();
        private TItemInfoQuestionsDPDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TItemInfoQuestionsDPDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "TItemInfoQuestionsDP"; } }
        public override String TablePropertyName { get { return "TItemInfoQuestionsDP"; } }
        public override String TableSqlName { get { return "TItemInfoQuestionsDP"; } }

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
        protected ColumnInfo _columnSortNumber;
        protected ColumnInfo _columnMatrixDiv;
        protected ColumnInfo _columnLv1title;
        protected ColumnInfo _columnLv2title;
        protected ColumnInfo _columnOriginalLv1title;
        protected ColumnInfo _columnOriginalLv2title;
        protected ColumnInfo _columnCategoryEditId;
        protected ColumnInfo _columnDataEditId;
        protected ColumnInfo _columnStatus;
        protected ColumnInfo _columnCompelItemChangeFlag;
        protected ColumnInfo _columnSortFlag;
        protected ColumnInfo _columnSortRange;
        protected ColumnInfo _columnMultivariateFlag;
        protected ColumnInfo _columnCategoryCount;
        protected ColumnInfo _columnChildItemCount;
        protected ColumnInfo _columnDpDataEditId;
        protected ColumnInfo _columnExecuteNo;
        protected ColumnInfo _columnDpStatus;
        protected ColumnInfo _columnNewItemName;
        protected ColumnInfo _columnNewAnswerType;
        protected ColumnInfo _columnNewLv1title;
        protected ColumnInfo _columnNewLv2title;
        protected ColumnInfo _columnDpCategoryCount;

        public ColumnInfo ColumnItemInfoId { get { return _columnItemInfoId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnItemName { get { return _columnItemName; } }
        public ColumnInfo ColumnSourceDiv { get { return _columnSourceDiv; } }
        public ColumnInfo ColumnItemno { get { return _columnItemno; } }
        public ColumnInfo ColumnItemType { get { return _columnItemType; } }
        public ColumnInfo ColumnAnswerType { get { return _columnAnswerType; } }
        public ColumnInfo ColumnSortNumber { get { return _columnSortNumber; } }
        public ColumnInfo ColumnMatrixDiv { get { return _columnMatrixDiv; } }
        public ColumnInfo ColumnLv1title { get { return _columnLv1title; } }
        public ColumnInfo ColumnLv2title { get { return _columnLv2title; } }
        public ColumnInfo ColumnOriginalLv1title { get { return _columnOriginalLv1title; } }
        public ColumnInfo ColumnOriginalLv2title { get { return _columnOriginalLv2title; } }
        public ColumnInfo ColumnCategoryEditId { get { return _columnCategoryEditId; } }
        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }
        public ColumnInfo ColumnStatus { get { return _columnStatus; } }
        public ColumnInfo ColumnCompelItemChangeFlag { get { return _columnCompelItemChangeFlag; } }
        public ColumnInfo ColumnSortFlag { get { return _columnSortFlag; } }
        public ColumnInfo ColumnSortRange { get { return _columnSortRange; } }
        public ColumnInfo ColumnMultivariateFlag { get { return _columnMultivariateFlag; } }
        public ColumnInfo ColumnCategoryCount { get { return _columnCategoryCount; } }
        public ColumnInfo ColumnChildItemCount { get { return _columnChildItemCount; } }
        public ColumnInfo ColumnDpDataEditId { get { return _columnDpDataEditId; } }
        public ColumnInfo ColumnExecuteNo { get { return _columnExecuteNo; } }
        public ColumnInfo ColumnDpStatus { get { return _columnDpStatus; } }
        public ColumnInfo ColumnNewItemName { get { return _columnNewItemName; } }
        public ColumnInfo ColumnNewAnswerType { get { return _columnNewAnswerType; } }
        public ColumnInfo ColumnNewLv1title { get { return _columnNewLv1title; } }
        public ColumnInfo ColumnNewLv2title { get { return _columnNewLv2title; } }
        public ColumnInfo ColumnDpCategoryCount { get { return _columnDpCategoryCount; } }

        protected void InitializeColumnInfo() {
            _columnItemInfoId = cci("ITEM_INFO_ID", "ITEM_INFO_ID", null, null, false, "ItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, false, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemName = cci("ITEM_NAME", "ITEM_NAME", null, null, false, "ItemName", typeof(String), false, "VARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSourceDiv = cci("SOURCE_DIV", "SOURCE_DIV", null, null, false, "SourceDiv", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemno = cci("ITEMNO", "ITEMNO", null, null, false, "Itemno", typeof(String), false, "VARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemType = cci("ITEM_TYPE", "ITEM_TYPE", null, null, false, "ItemType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAnswerType = cci("ANSWER_TYPE", "ANSWER_TYPE", null, null, false, "AnswerType", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNumber = cci("SORT_NUMBER", "SORT_NUMBER", null, null, false, "SortNumber", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMatrixDiv = cci("MATRIX_DIV", "MATRIX_DIV", null, null, false, "MatrixDiv", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLv1title = cci("LV1TITLE", "LV1TITLE", null, null, false, "Lv1title", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLv2title = cci("LV2TITLE", "LV2TITLE", null, null, false, "Lv2title", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOriginalLv1title = cci("ORIGINAL_LV1TITLE", "ORIGINAL_LV1TITLE", null, null, false, "OriginalLv1title", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOriginalLv2title = cci("ORIGINAL_LV2TITLE", "ORIGINAL_LV2TITLE", null, null, false, "OriginalLv2title", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCategoryEditId = cci("CATEGORY_EDIT_ID", "CATEGORY_EDIT_ID", null, null, false, "CategoryEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, false, "DataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnStatus = cci("STATUS", "STATUS", null, null, false, "Status", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCompelItemChangeFlag = cci("COMPEL_ITEM_CHANGE_FLAG", "COMPEL_ITEM_CHANGE_FLAG", null, null, false, "CompelItemChangeFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortFlag = cci("SORT_FLAG", "SORT_FLAG", null, null, false, "SortFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortRange = cci("SORT_RANGE", "SORT_RANGE", null, null, false, "SortRange", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMultivariateFlag = cci("MULTIVARIATE_FLAG", "MULTIVARIATE_FLAG", null, null, false, "MultivariateFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCategoryCount = cci("CATEGORY_COUNT", "CATEGORY_COUNT", null, null, false, "CategoryCount", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnChildItemCount = cci("CHILD_ITEM_COUNT", "CHILD_ITEM_COUNT", null, null, false, "ChildItemCount", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpDataEditId = cci("DP_DATA_EDIT_ID", "DP_DATA_EDIT_ID", null, null, false, "DpDataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExecuteNo = cci("EXECUTE_NO", "EXECUTE_NO", null, null, false, "ExecuteNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpStatus = cci("DP_STATUS", "DP_STATUS", null, null, false, "DpStatus", typeof(String), false, "VARCHAR2", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewItemName = cci("NEW_ITEM_NAME", "NEW_ITEM_NAME", null, null, false, "NewItemName", typeof(String), false, "VARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewAnswerType = cci("NEW_ANSWER_TYPE", "NEW_ANSWER_TYPE", null, null, false, "NewAnswerType", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewLv1title = cci("NEW_LV1TITLE", "NEW_LV1TITLE", null, null, false, "NewLv1title", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNewLv2title = cci("NEW_LV2TITLE", "NEW_LV2TITLE", null, null, false, "NewLv2title", typeof(String), false, "VARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpCategoryCount = cci("DP_CATEGORY_COUNT", "DP_CATEGORY_COUNT", null, null, false, "DpCategoryCount", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
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
            _columnInfoList.add(ColumnSortNumber);
            _columnInfoList.add(ColumnMatrixDiv);
            _columnInfoList.add(ColumnLv1title);
            _columnInfoList.add(ColumnLv2title);
            _columnInfoList.add(ColumnOriginalLv1title);
            _columnInfoList.add(ColumnOriginalLv2title);
            _columnInfoList.add(ColumnCategoryEditId);
            _columnInfoList.add(ColumnDataEditId);
            _columnInfoList.add(ColumnStatus);
            _columnInfoList.add(ColumnCompelItemChangeFlag);
            _columnInfoList.add(ColumnSortFlag);
            _columnInfoList.add(ColumnSortRange);
            _columnInfoList.add(ColumnMultivariateFlag);
            _columnInfoList.add(ColumnCategoryCount);
            _columnInfoList.add(ColumnChildItemCount);
            _columnInfoList.add(ColumnDpDataEditId);
            _columnInfoList.add(ColumnExecuteNo);
            _columnInfoList.add(ColumnDpStatus);
            _columnInfoList.add(ColumnNewItemName);
            _columnInfoList.add(ColumnNewAnswerType);
            _columnInfoList.add(ColumnNewLv1title);
            _columnInfoList.add(ColumnNewLv2title);
            _columnInfoList.add(ColumnDpCategoryCount);
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
        public static readonly String TABLE_DB_NAME = "TItemInfoQuestionsDP";
        public static readonly String TABLE_PROPERTY_NAME = "TItemInfoQuestionsDP";

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
        public static readonly String DB_NAME_SORT_NUMBER = "SORT_NUMBER";
        public static readonly String DB_NAME_MATRIX_DIV = "MATRIX_DIV";
        public static readonly String DB_NAME_LV1TITLE = "LV1TITLE";
        public static readonly String DB_NAME_LV2TITLE = "LV2TITLE";
        public static readonly String DB_NAME_ORIGINAL_LV1TITLE = "ORIGINAL_LV1TITLE";
        public static readonly String DB_NAME_ORIGINAL_LV2TITLE = "ORIGINAL_LV2TITLE";
        public static readonly String DB_NAME_CATEGORY_EDIT_ID = "CATEGORY_EDIT_ID";
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";
        public static readonly String DB_NAME_STATUS = "STATUS";
        public static readonly String DB_NAME_COMPEL_ITEM_CHANGE_FLAG = "COMPEL_ITEM_CHANGE_FLAG";
        public static readonly String DB_NAME_SORT_FLAG = "SORT_FLAG";
        public static readonly String DB_NAME_SORT_RANGE = "SORT_RANGE";
        public static readonly String DB_NAME_MULTIVARIATE_FLAG = "MULTIVARIATE_FLAG";
        public static readonly String DB_NAME_CATEGORY_COUNT = "CATEGORY_COUNT";
        public static readonly String DB_NAME_CHILD_ITEM_COUNT = "CHILD_ITEM_COUNT";
        public static readonly String DB_NAME_DP_DATA_EDIT_ID = "DP_DATA_EDIT_ID";
        public static readonly String DB_NAME_EXECUTE_NO = "EXECUTE_NO";
        public static readonly String DB_NAME_DP_STATUS = "DP_STATUS";
        public static readonly String DB_NAME_NEW_ITEM_NAME = "NEW_ITEM_NAME";
        public static readonly String DB_NAME_NEW_ANSWER_TYPE = "NEW_ANSWER_TYPE";
        public static readonly String DB_NAME_NEW_LV1TITLE = "NEW_LV1TITLE";
        public static readonly String DB_NAME_NEW_LV2TITLE = "NEW_LV2TITLE";
        public static readonly String DB_NAME_DP_CATEGORY_COUNT = "DP_CATEGORY_COUNT";

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
        public static readonly String PROPERTY_NAME_SORT_NUMBER = "SortNumber";
        public static readonly String PROPERTY_NAME_MATRIX_DIV = "MatrixDiv";
        public static readonly String PROPERTY_NAME_LV1TITLE = "Lv1title";
        public static readonly String PROPERTY_NAME_LV2TITLE = "Lv2title";
        public static readonly String PROPERTY_NAME_ORIGINAL_LV1TITLE = "OriginalLv1title";
        public static readonly String PROPERTY_NAME_ORIGINAL_LV2TITLE = "OriginalLv2title";
        public static readonly String PROPERTY_NAME_CATEGORY_EDIT_ID = "CategoryEditId";
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";
        public static readonly String PROPERTY_NAME_STATUS = "Status";
        public static readonly String PROPERTY_NAME_COMPEL_ITEM_CHANGE_FLAG = "CompelItemChangeFlag";
        public static readonly String PROPERTY_NAME_SORT_FLAG = "SortFlag";
        public static readonly String PROPERTY_NAME_SORT_RANGE = "SortRange";
        public static readonly String PROPERTY_NAME_MULTIVARIATE_FLAG = "MultivariateFlag";
        public static readonly String PROPERTY_NAME_CATEGORY_COUNT = "CategoryCount";
        public static readonly String PROPERTY_NAME_CHILD_ITEM_COUNT = "ChildItemCount";
        public static readonly String PROPERTY_NAME_DP_DATA_EDIT_ID = "DpDataEditId";
        public static readonly String PROPERTY_NAME_EXECUTE_NO = "ExecuteNo";
        public static readonly String PROPERTY_NAME_DP_STATUS = "DpStatus";
        public static readonly String PROPERTY_NAME_NEW_ITEM_NAME = "NewItemName";
        public static readonly String PROPERTY_NAME_NEW_ANSWER_TYPE = "NewAnswerType";
        public static readonly String PROPERTY_NAME_NEW_LV1TITLE = "NewLv1title";
        public static readonly String PROPERTY_NAME_NEW_LV2TITLE = "NewLv2title";
        public static readonly String PROPERTY_NAME_DP_CATEGORY_COUNT = "DpCategoryCount";

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

        static TItemInfoQuestionsDPDbm() {
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
                map.put(DB_NAME_SORT_NUMBER.ToLower(), PROPERTY_NAME_SORT_NUMBER);
                map.put(DB_NAME_MATRIX_DIV.ToLower(), PROPERTY_NAME_MATRIX_DIV);
                map.put(DB_NAME_LV1TITLE.ToLower(), PROPERTY_NAME_LV1TITLE);
                map.put(DB_NAME_LV2TITLE.ToLower(), PROPERTY_NAME_LV2TITLE);
                map.put(DB_NAME_ORIGINAL_LV1TITLE.ToLower(), PROPERTY_NAME_ORIGINAL_LV1TITLE);
                map.put(DB_NAME_ORIGINAL_LV2TITLE.ToLower(), PROPERTY_NAME_ORIGINAL_LV2TITLE);
                map.put(DB_NAME_CATEGORY_EDIT_ID.ToLower(), PROPERTY_NAME_CATEGORY_EDIT_ID);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                map.put(DB_NAME_STATUS.ToLower(), PROPERTY_NAME_STATUS);
                map.put(DB_NAME_COMPEL_ITEM_CHANGE_FLAG.ToLower(), PROPERTY_NAME_COMPEL_ITEM_CHANGE_FLAG);
                map.put(DB_NAME_SORT_FLAG.ToLower(), PROPERTY_NAME_SORT_FLAG);
                map.put(DB_NAME_SORT_RANGE.ToLower(), PROPERTY_NAME_SORT_RANGE);
                map.put(DB_NAME_MULTIVARIATE_FLAG.ToLower(), PROPERTY_NAME_MULTIVARIATE_FLAG);
                map.put(DB_NAME_CATEGORY_COUNT.ToLower(), PROPERTY_NAME_CATEGORY_COUNT);
                map.put(DB_NAME_CHILD_ITEM_COUNT.ToLower(), PROPERTY_NAME_CHILD_ITEM_COUNT);
                map.put(DB_NAME_DP_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DP_DATA_EDIT_ID);
                map.put(DB_NAME_EXECUTE_NO.ToLower(), PROPERTY_NAME_EXECUTE_NO);
                map.put(DB_NAME_DP_STATUS.ToLower(), PROPERTY_NAME_DP_STATUS);
                map.put(DB_NAME_NEW_ITEM_NAME.ToLower(), PROPERTY_NAME_NEW_ITEM_NAME);
                map.put(DB_NAME_NEW_ANSWER_TYPE.ToLower(), PROPERTY_NAME_NEW_ANSWER_TYPE);
                map.put(DB_NAME_NEW_LV1TITLE.ToLower(), PROPERTY_NAME_NEW_LV1TITLE);
                map.put(DB_NAME_NEW_LV2TITLE.ToLower(), PROPERTY_NAME_NEW_LV2TITLE);
                map.put(DB_NAME_DP_CATEGORY_COUNT.ToLower(), PROPERTY_NAME_DP_CATEGORY_COUNT);
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
                map.put(PROPERTY_NAME_SORT_NUMBER.ToLower(), DB_NAME_SORT_NUMBER);
                map.put(PROPERTY_NAME_MATRIX_DIV.ToLower(), DB_NAME_MATRIX_DIV);
                map.put(PROPERTY_NAME_LV1TITLE.ToLower(), DB_NAME_LV1TITLE);
                map.put(PROPERTY_NAME_LV2TITLE.ToLower(), DB_NAME_LV2TITLE);
                map.put(PROPERTY_NAME_ORIGINAL_LV1TITLE.ToLower(), DB_NAME_ORIGINAL_LV1TITLE);
                map.put(PROPERTY_NAME_ORIGINAL_LV2TITLE.ToLower(), DB_NAME_ORIGINAL_LV2TITLE);
                map.put(PROPERTY_NAME_CATEGORY_EDIT_ID.ToLower(), DB_NAME_CATEGORY_EDIT_ID);
                map.put(PROPERTY_NAME_DATA_EDIT_ID.ToLower(), DB_NAME_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_STATUS.ToLower(), DB_NAME_STATUS);
                map.put(PROPERTY_NAME_COMPEL_ITEM_CHANGE_FLAG.ToLower(), DB_NAME_COMPEL_ITEM_CHANGE_FLAG);
                map.put(PROPERTY_NAME_SORT_FLAG.ToLower(), DB_NAME_SORT_FLAG);
                map.put(PROPERTY_NAME_SORT_RANGE.ToLower(), DB_NAME_SORT_RANGE);
                map.put(PROPERTY_NAME_MULTIVARIATE_FLAG.ToLower(), DB_NAME_MULTIVARIATE_FLAG);
                map.put(PROPERTY_NAME_CATEGORY_COUNT.ToLower(), DB_NAME_CATEGORY_COUNT);
                map.put(PROPERTY_NAME_CHILD_ITEM_COUNT.ToLower(), DB_NAME_CHILD_ITEM_COUNT);
                map.put(PROPERTY_NAME_DP_DATA_EDIT_ID.ToLower(), DB_NAME_DP_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_EXECUTE_NO.ToLower(), DB_NAME_EXECUTE_NO);
                map.put(PROPERTY_NAME_DP_STATUS.ToLower(), DB_NAME_DP_STATUS);
                map.put(PROPERTY_NAME_NEW_ITEM_NAME.ToLower(), DB_NAME_NEW_ITEM_NAME);
                map.put(PROPERTY_NAME_NEW_ANSWER_TYPE.ToLower(), DB_NAME_NEW_ANSWER_TYPE);
                map.put(PROPERTY_NAME_NEW_LV1TITLE.ToLower(), DB_NAME_NEW_LV1TITLE);
                map.put(PROPERTY_NAME_NEW_LV2TITLE.ToLower(), DB_NAME_NEW_LV2TITLE);
                map.put(PROPERTY_NAME_DP_CATEGORY_COUNT.ToLower(), DB_NAME_DP_CATEGORY_COUNT);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.TItemInfoQuestionsDP"; } }
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
        public TItemInfoQuestionsDP NewMyEntity() { return new TItemInfoQuestionsDP(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TItemInfoQuestionsDP>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TItemInfoQuestionsDP>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("ITEM_INFO_ID", "ItemInfoId", new EntityPropertyItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_NAME", "ItemName", new EntityPropertyItemNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SOURCE_DIV", "SourceDiv", new EntityPropertySourceDivSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEMNO", "Itemno", new EntityPropertyItemnoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_TYPE", "ItemType", new EntityPropertyItemTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ANSWER_TYPE", "AnswerType", new EntityPropertyAnswerTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NUMBER", "SortNumber", new EntityPropertySortNumberSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MATRIX_DIV", "MatrixDiv", new EntityPropertyMatrixDivSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LV1TITLE", "Lv1title", new EntityPropertyLv1titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LV2TITLE", "Lv2title", new EntityPropertyLv2titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ORIGINAL_LV1TITLE", "OriginalLv1title", new EntityPropertyOriginalLv1titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ORIGINAL_LV2TITLE", "OriginalLv2title", new EntityPropertyOriginalLv2titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CATEGORY_EDIT_ID", "CategoryEditId", new EntityPropertyCategoryEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("STATUS", "Status", new EntityPropertyStatusSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COMPEL_ITEM_CHANGE_FLAG", "CompelItemChangeFlag", new EntityPropertyCompelItemChangeFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_FLAG", "SortFlag", new EntityPropertySortFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_RANGE", "SortRange", new EntityPropertySortRangeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MULTIVARIATE_FLAG", "MultivariateFlag", new EntityPropertyMultivariateFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CATEGORY_COUNT", "CategoryCount", new EntityPropertyCategoryCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CHILD_ITEM_COUNT", "ChildItemCount", new EntityPropertyChildItemCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_DATA_EDIT_ID", "DpDataEditId", new EntityPropertyDpDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXECUTE_NO", "ExecuteNo", new EntityPropertyExecuteNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_STATUS", "DpStatus", new EntityPropertyDpStatusSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_ITEM_NAME", "NewItemName", new EntityPropertyNewItemNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_ANSWER_TYPE", "NewAnswerType", new EntityPropertyNewAnswerTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_LV1TITLE", "NewLv1title", new EntityPropertyNewLv1titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_LV2TITLE", "NewLv2title", new EntityPropertyNewLv2titleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_CATEGORY_COUNT", "DpCategoryCount", new EntityPropertyDpCategoryCountSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TItemInfoQuestionsDP> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TItemInfoQuestionsDP)entity, value);
        }

        public class EntityPropertyItemInfoIdSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.ItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyItemNameSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.ItemName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySourceDivSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.SourceDiv = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyItemnoSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.Itemno = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyItemTypeSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.ItemType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyAnswerTypeSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.AnswerType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySortNumberSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.SortNumber = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMatrixDivSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.MatrixDiv = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLv1titleSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.Lv1title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLv2titleSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.Lv2title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOriginalLv1titleSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.OriginalLv1title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOriginalLv2titleSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.OriginalLv2title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCategoryEditIdSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.CategoryEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyStatusSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.Status = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyCompelItemChangeFlagSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.CompelItemChangeFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySortFlagSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.SortFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySortRangeSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.SortRange = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMultivariateFlagSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.MultivariateFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyCategoryCountSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.CategoryCount = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyChildItemCountSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.ChildItemCount = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDpDataEditIdSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.DpDataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyExecuteNoSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.ExecuteNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpStatusSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.DpStatus = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNewItemNameSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.NewItemName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNewAnswerTypeSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.NewAnswerType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNewLv1titleSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.NewLv1title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNewLv2titleSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.NewLv2title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDpCategoryCountSetupper : EntityPropertySetupper<TItemInfoQuestionsDP> {
            public void Setup(TItemInfoQuestionsDP entity, Object value) { entity.DpCategoryCount = (value != null) ? (decimal?)value : null; }
        }
    }
}
