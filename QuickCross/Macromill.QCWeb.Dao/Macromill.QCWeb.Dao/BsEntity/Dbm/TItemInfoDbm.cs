
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

    public class TItemInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TItemInfo);

        private static readonly TItemInfoDbm _instance = new TItemInfoDbm();
        private TItemInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TItemInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_ITEM_INFO"; } }
        public override String TablePropertyName { get { return "TItemInfo"; } }
        public override String TableSqlName { get { return "T_ITEM_INFO"; } }

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
        protected ColumnInfo _columnTableName;
        protected ColumnInfo _columnColumnName;
        protected ColumnInfo _columnCategoryEditId;
        protected ColumnInfo _columnDataEditId;
        protected ColumnInfo _columnStatus;
        protected ColumnInfo _columnTableNameOrg;
        protected ColumnInfo _columnColumnNameOrg;
        protected ColumnInfo _columnCompelItemChangeFlag;
        protected ColumnInfo _columnSortFlag;
        protected ColumnInfo _columnSortRange;
        protected ColumnInfo _columnMultivariateFlag;
        protected ColumnInfo _columnTableNo;
        protected ColumnInfo _columnColumnNo;
        protected ColumnInfo _columnTableNoOrg;
        protected ColumnInfo _columnColumnNoOrg;
        protected ColumnInfo _columnLastUpdateUser;
        protected ColumnInfo _columnLastUpdateDatetime;
        protected ColumnInfo _columnNewAtQc3Flag;
        protected ColumnInfo _columnSortRangeOrg;

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
        public ColumnInfo ColumnTableName { get { return _columnTableName; } }
        public ColumnInfo ColumnColumnName { get { return _columnColumnName; } }
        public ColumnInfo ColumnCategoryEditId { get { return _columnCategoryEditId; } }
        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }
        public ColumnInfo ColumnStatus { get { return _columnStatus; } }
        public ColumnInfo ColumnTableNameOrg { get { return _columnTableNameOrg; } }
        public ColumnInfo ColumnColumnNameOrg { get { return _columnColumnNameOrg; } }
        public ColumnInfo ColumnCompelItemChangeFlag { get { return _columnCompelItemChangeFlag; } }
        public ColumnInfo ColumnSortFlag { get { return _columnSortFlag; } }
        public ColumnInfo ColumnSortRange { get { return _columnSortRange; } }
        public ColumnInfo ColumnMultivariateFlag { get { return _columnMultivariateFlag; } }
        public ColumnInfo ColumnTableNo { get { return _columnTableNo; } }
        public ColumnInfo ColumnColumnNo { get { return _columnColumnNo; } }
        public ColumnInfo ColumnTableNoOrg { get { return _columnTableNoOrg; } }
        public ColumnInfo ColumnColumnNoOrg { get { return _columnColumnNoOrg; } }
        public ColumnInfo ColumnLastUpdateUser { get { return _columnLastUpdateUser; } }
        public ColumnInfo ColumnLastUpdateDatetime { get { return _columnLastUpdateDatetime; } }
        public ColumnInfo ColumnNewAtQc3Flag { get { return _columnNewAtQc3Flag; } }
        public ColumnInfo ColumnSortRangeOrg { get { return _columnSortRangeOrg; } }

        protected void InitializeColumnInfo() {
            _columnItemInfoId = cci("ITEM_INFO_ID", "ITEM_INFO_ID", null, null, true, "ItemInfoId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TMatrixInfo,TFaListAddItem,TFaScenarioItem", "TCategoryInfoList,TMatrixInfoByItemInfoIdList,TMatrixInfoByChildItemInfoIdList,TScenarioQuerylistList,TGtScenarioItemList,TFaScenarioItemList,TFaListAddItemList,TGtMatrixChildList");
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo,TTableControl", null);
            _columnItemName = cci("ITEM_NAME", "ITEM_NAME", null, null, true, "ItemName", typeof(String), false, "NVARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSourceDiv = cci("SOURCE_DIV", "SOURCE_DIV", null, null, true, "SourceDiv", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemno = cci("ITEMNO", "ITEMNO", null, null, false, "Itemno", typeof(String), false, "NVARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemType = cci("ITEM_TYPE", "ITEM_TYPE", null, null, false, "ItemType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAnswerType = cci("ANSWER_TYPE", "ANSWER_TYPE", null, null, true, "AnswerType", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNumber = cci("SORT_NUMBER", "SORT_NUMBER", null, null, true, "SortNumber", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMatrixDiv = cci("MATRIX_DIV", "MATRIX_DIV", null, null, true, "MatrixDiv", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLv1title = cci("LV1TITLE", "LV1TITLE", null, null, false, "Lv1title", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLv2title = cci("LV2TITLE", "LV2TITLE", null, null, false, "Lv2title", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOriginalLv1title = cci("ORIGINAL_LV1TITLE", "ORIGINAL_LV1TITLE", null, null, false, "OriginalLv1title", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOriginalLv2title = cci("ORIGINAL_LV2TITLE", "ORIGINAL_LV2TITLE", null, null, false, "OriginalLv2title", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTableName = cci("TABLE_NAME", "TABLE_NAME", null, null, false, "TableName", typeof(String), false, "VARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColumnName = cci("COLUMN_NAME", "COLUMN_NAME", null, null, false, "ColumnName", typeof(String), false, "VARCHAR2", 30, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCategoryEditId = cci("CATEGORY_EDIT_ID", "CATEGORY_EDIT_ID", null, null, false, "CategoryEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TScenarioTotalization", null);
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, false, "DataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TDataEditList", null);
            _columnStatus = cci("STATUS", "STATUS", null, null, true, "Status", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTableNameOrg = cci("TABLE_NAME_ORG", "TABLE_NAME_ORG", null, null, false, "TableNameOrg", typeof(String), false, "VARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColumnNameOrg = cci("COLUMN_NAME_ORG", "COLUMN_NAME_ORG", null, null, false, "ColumnNameOrg", typeof(String), false, "VARCHAR2", 30, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCompelItemChangeFlag = cci("COMPEL_ITEM_CHANGE_FLAG", "COMPEL_ITEM_CHANGE_FLAG", null, null, true, "CompelItemChangeFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortFlag = cci("SORT_FLAG", "SORT_FLAG", null, null, true, "SortFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortRange = cci("SORT_RANGE", "SORT_RANGE", null, null, false, "SortRange", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMultivariateFlag = cci("MULTIVARIATE_FLAG", "MULTIVARIATE_FLAG", null, null, true, "MultivariateFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTableNo = cci("TABLE_NO", "TABLE_NO", null, null, false, "TableNo", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColumnNo = cci("COLUMN_NO", "COLUMN_NO", null, null, false, "ColumnNo", typeof(int?), false, "NUMBER", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTableNoOrg = cci("TABLE_NO_ORG", "TABLE_NO_ORG", null, null, false, "TableNoOrg", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColumnNoOrg = cci("COLUMN_NO_ORG", "COLUMN_NO_ORG", null, null, false, "ColumnNoOrg", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLastUpdateUser = cci("LAST_UPDATE_USER", "LAST_UPDATE_USER", null, null, false, "LastUpdateUser", typeof(String), false, "VARCHAR2", 1000, 0, true, OptimisticLockType.NONE, null, null, null);
            _columnLastUpdateDatetime = cci("LAST_UPDATE_DATETIME", "LAST_UPDATE_DATETIME", null, null, false, "LastUpdateDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, true, OptimisticLockType.NONE, null, null, null);
            _columnNewAtQc3Flag = cci("NEW_AT_QC3_FLAG", "NEW_AT_QC3_FLAG", null, null, true, "NewAtQc3Flag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortRangeOrg = cci("SORT_RANGE_ORG", "SORT_RANGE_ORG", null, null, false, "SortRangeOrg", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
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
            _columnInfoList.add(ColumnTableName);
            _columnInfoList.add(ColumnColumnName);
            _columnInfoList.add(ColumnCategoryEditId);
            _columnInfoList.add(ColumnDataEditId);
            _columnInfoList.add(ColumnStatus);
            _columnInfoList.add(ColumnTableNameOrg);
            _columnInfoList.add(ColumnColumnNameOrg);
            _columnInfoList.add(ColumnCompelItemChangeFlag);
            _columnInfoList.add(ColumnSortFlag);
            _columnInfoList.add(ColumnSortRange);
            _columnInfoList.add(ColumnMultivariateFlag);
            _columnInfoList.add(ColumnTableNo);
            _columnInfoList.add(ColumnColumnNo);
            _columnInfoList.add(ColumnTableNoOrg);
            _columnInfoList.add(ColumnColumnNoOrg);
            _columnInfoList.add(ColumnLastUpdateUser);
            _columnInfoList.add(ColumnLastUpdateDatetime);
            _columnInfoList.add(ColumnNewAtQc3Flag);
            _columnInfoList.add(ColumnSortRangeOrg);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnItemInfoId);
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
        public ForeignInfo ForeignTQcwebSurveyInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfo", this, TQcwebSurveyInfoDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTMatrixInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TMatrixInfoDbm.GetInstance().ColumnChildItemInfoId);
            return cfi("TMatrixInfo", this, TMatrixInfoDbm.GetInstance(), map, 1, true, false);
        }}
        public ForeignInfo ForeignTFaListAddItem { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TFaListAddItemDbm.GetInstance().ColumnItemInfoId);
            return cfi("TFaListAddItem", this, TFaListAddItemDbm.GetInstance(), map, 2, true, false);
        }}
        public ForeignInfo ForeignTFaScenarioItem { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TFaScenarioItemDbm.GetInstance().ColumnFaTargetItemId);
            return cfi("TFaScenarioItem", this, TFaScenarioItemDbm.GetInstance(), map, 3, true, false);
        }}
        public ForeignInfo ForeignTTableControl { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TTableControlDbm.GetInstance().ColumnQcwebid);
            return cfi("TTableControl", this, TTableControlDbm.GetInstance(), map, 4, false, false);
        }}
        public ForeignInfo ForeignTScenarioTotalization { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnCategoryEditId, TScenarioTotalizationDbm.GetInstance().ColumnScenarioTotalizationId);
            return cfi("TScenarioTotalization", this, TScenarioTotalizationDbm.GetInstance(), map, 5, false, false);
        }}
        public ForeignInfo ForeignTDataEditList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnDataEditId, TDataEditListDbm.GetInstance().ColumnDataEditId);
            return cfi("TDataEditList", this, TDataEditListDbm.GetInstance(), map, 6, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTCategoryInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TCategoryInfoDbm.GetInstance().ColumnItemInfoId);
            return cri("TCategoryInfoList", this, TCategoryInfoDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTMatrixInfoByItemInfoIdList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TMatrixInfoDbm.GetInstance().ColumnItemInfoId);
            return cri("TMatrixInfoByItemInfoIdList", this, TMatrixInfoDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTMatrixInfoByChildItemInfoIdList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TMatrixInfoDbm.GetInstance().ColumnChildItemInfoId);
            return cri("TMatrixInfoByChildItemInfoIdList", this, TMatrixInfoDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTScenarioQuerylistList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TScenarioQuerylistDbm.GetInstance().ColumnItemInfoId);
            return cri("TScenarioQuerylistList", this, TScenarioQuerylistDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTGtScenarioItemList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TGtScenarioItemDbm.GetInstance().ColumnItemInfoId);
            return cri("TGtScenarioItemList", this, TGtScenarioItemDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTFaScenarioItemList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TFaScenarioItemDbm.GetInstance().ColumnFaTargetItemId);
            return cri("TFaScenarioItemList", this, TFaScenarioItemDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTFaListAddItemList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TFaListAddItemDbm.GetInstance().ColumnItemInfoId);
            return cri("TFaListAddItemList", this, TFaListAddItemDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTGtMatrixChildList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnItemInfoId, TGtMatrixChildDbm.GetInstance().ColumnChildItemId);
            return cri("TGtMatrixChildList", this, TGtMatrixChildDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Item_Info_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Item_Info_SEQ_01.nextval from dual"; } }
        public override int? SequenceIncrementSize { get { return 1; } }
        public override int? SequenceCacheSize { get { return null; } }
        public override bool HasCommonColumn { get { return true; } }

        // ===============================================================================
        //                                                                 Name Definition
        //                                                                 ===============
        #region Name

        // -------------------------------------------------
        //                                             Table
        //                                             -----
        public static readonly String TABLE_DB_NAME = "T_ITEM_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TItemInfo";

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
        public static readonly String DB_NAME_TABLE_NAME = "TABLE_NAME";
        public static readonly String DB_NAME_COLUMN_NAME = "COLUMN_NAME";
        public static readonly String DB_NAME_CATEGORY_EDIT_ID = "CATEGORY_EDIT_ID";
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";
        public static readonly String DB_NAME_STATUS = "STATUS";
        public static readonly String DB_NAME_TABLE_NAME_ORG = "TABLE_NAME_ORG";
        public static readonly String DB_NAME_COLUMN_NAME_ORG = "COLUMN_NAME_ORG";
        public static readonly String DB_NAME_COMPEL_ITEM_CHANGE_FLAG = "COMPEL_ITEM_CHANGE_FLAG";
        public static readonly String DB_NAME_SORT_FLAG = "SORT_FLAG";
        public static readonly String DB_NAME_SORT_RANGE = "SORT_RANGE";
        public static readonly String DB_NAME_MULTIVARIATE_FLAG = "MULTIVARIATE_FLAG";
        public static readonly String DB_NAME_TABLE_NO = "TABLE_NO";
        public static readonly String DB_NAME_COLUMN_NO = "COLUMN_NO";
        public static readonly String DB_NAME_TABLE_NO_ORG = "TABLE_NO_ORG";
        public static readonly String DB_NAME_COLUMN_NO_ORG = "COLUMN_NO_ORG";
        public static readonly String DB_NAME_LAST_UPDATE_USER = "LAST_UPDATE_USER";
        public static readonly String DB_NAME_LAST_UPDATE_DATETIME = "LAST_UPDATE_DATETIME";
        public static readonly String DB_NAME_NEW_AT_QC3_FLAG = "NEW_AT_QC3_FLAG";
        public static readonly String DB_NAME_SORT_RANGE_ORG = "SORT_RANGE_ORG";

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
        public static readonly String PROPERTY_NAME_TABLE_NAME = "TableName";
        public static readonly String PROPERTY_NAME_COLUMN_NAME = "ColumnName";
        public static readonly String PROPERTY_NAME_CATEGORY_EDIT_ID = "CategoryEditId";
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";
        public static readonly String PROPERTY_NAME_STATUS = "Status";
        public static readonly String PROPERTY_NAME_TABLE_NAME_ORG = "TableNameOrg";
        public static readonly String PROPERTY_NAME_COLUMN_NAME_ORG = "ColumnNameOrg";
        public static readonly String PROPERTY_NAME_COMPEL_ITEM_CHANGE_FLAG = "CompelItemChangeFlag";
        public static readonly String PROPERTY_NAME_SORT_FLAG = "SortFlag";
        public static readonly String PROPERTY_NAME_SORT_RANGE = "SortRange";
        public static readonly String PROPERTY_NAME_MULTIVARIATE_FLAG = "MultivariateFlag";
        public static readonly String PROPERTY_NAME_TABLE_NO = "TableNo";
        public static readonly String PROPERTY_NAME_COLUMN_NO = "ColumnNo";
        public static readonly String PROPERTY_NAME_TABLE_NO_ORG = "TableNoOrg";
        public static readonly String PROPERTY_NAME_COLUMN_NO_ORG = "ColumnNoOrg";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_USER = "LastUpdateUser";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_DATETIME = "LastUpdateDatetime";
        public static readonly String PROPERTY_NAME_NEW_AT_QC3_FLAG = "NewAtQc3Flag";
        public static readonly String PROPERTY_NAME_SORT_RANGE_ORG = "SortRangeOrg";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TMatrixInfo = "TMatrixInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TFaListAddItem = "TFaListAddItem";
        public static readonly String FOREIGN_PROPERTY_NAME_TFaScenarioItem = "TFaScenarioItem";
        public static readonly String FOREIGN_PROPERTY_NAME_TTableControl = "TTableControl";
        public static readonly String FOREIGN_PROPERTY_NAME_TScenarioTotalization = "TScenarioTotalization";
        public static readonly String FOREIGN_PROPERTY_NAME_TDataEditList = "TDataEditList";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TCategoryInfoList = "TCategoryInfoList";
        public static readonly String REFERRER_PROPERTY_NAME_TMatrixInfoByItemInfoIdList = "TMatrixInfoByItemInfoIdList";
        public static readonly String REFERRER_PROPERTY_NAME_TMatrixInfoByChildItemInfoIdList = "TMatrixInfoByChildItemInfoIdList";
        public static readonly String REFERRER_PROPERTY_NAME_TScenarioQuerylistList = "TScenarioQuerylistList";
        public static readonly String REFERRER_PROPERTY_NAME_TGtScenarioItemList = "TGtScenarioItemList";
        public static readonly String REFERRER_PROPERTY_NAME_TFaScenarioItemList = "TFaScenarioItemList";
        public static readonly String REFERRER_PROPERTY_NAME_TFaListAddItemList = "TFaListAddItemList";
        public static readonly String REFERRER_PROPERTY_NAME_TGtMatrixChildList = "TGtMatrixChildList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TItemInfoDbm() {
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
                map.put(DB_NAME_TABLE_NAME.ToLower(), PROPERTY_NAME_TABLE_NAME);
                map.put(DB_NAME_COLUMN_NAME.ToLower(), PROPERTY_NAME_COLUMN_NAME);
                map.put(DB_NAME_CATEGORY_EDIT_ID.ToLower(), PROPERTY_NAME_CATEGORY_EDIT_ID);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                map.put(DB_NAME_STATUS.ToLower(), PROPERTY_NAME_STATUS);
                map.put(DB_NAME_TABLE_NAME_ORG.ToLower(), PROPERTY_NAME_TABLE_NAME_ORG);
                map.put(DB_NAME_COLUMN_NAME_ORG.ToLower(), PROPERTY_NAME_COLUMN_NAME_ORG);
                map.put(DB_NAME_COMPEL_ITEM_CHANGE_FLAG.ToLower(), PROPERTY_NAME_COMPEL_ITEM_CHANGE_FLAG);
                map.put(DB_NAME_SORT_FLAG.ToLower(), PROPERTY_NAME_SORT_FLAG);
                map.put(DB_NAME_SORT_RANGE.ToLower(), PROPERTY_NAME_SORT_RANGE);
                map.put(DB_NAME_MULTIVARIATE_FLAG.ToLower(), PROPERTY_NAME_MULTIVARIATE_FLAG);
                map.put(DB_NAME_TABLE_NO.ToLower(), PROPERTY_NAME_TABLE_NO);
                map.put(DB_NAME_COLUMN_NO.ToLower(), PROPERTY_NAME_COLUMN_NO);
                map.put(DB_NAME_TABLE_NO_ORG.ToLower(), PROPERTY_NAME_TABLE_NO_ORG);
                map.put(DB_NAME_COLUMN_NO_ORG.ToLower(), PROPERTY_NAME_COLUMN_NO_ORG);
                map.put(DB_NAME_LAST_UPDATE_USER.ToLower(), PROPERTY_NAME_LAST_UPDATE_USER);
                map.put(DB_NAME_LAST_UPDATE_DATETIME.ToLower(), PROPERTY_NAME_LAST_UPDATE_DATETIME);
                map.put(DB_NAME_NEW_AT_QC3_FLAG.ToLower(), PROPERTY_NAME_NEW_AT_QC3_FLAG);
                map.put(DB_NAME_SORT_RANGE_ORG.ToLower(), PROPERTY_NAME_SORT_RANGE_ORG);
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
                map.put(PROPERTY_NAME_TABLE_NAME.ToLower(), DB_NAME_TABLE_NAME);
                map.put(PROPERTY_NAME_COLUMN_NAME.ToLower(), DB_NAME_COLUMN_NAME);
                map.put(PROPERTY_NAME_CATEGORY_EDIT_ID.ToLower(), DB_NAME_CATEGORY_EDIT_ID);
                map.put(PROPERTY_NAME_DATA_EDIT_ID.ToLower(), DB_NAME_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_STATUS.ToLower(), DB_NAME_STATUS);
                map.put(PROPERTY_NAME_TABLE_NAME_ORG.ToLower(), DB_NAME_TABLE_NAME_ORG);
                map.put(PROPERTY_NAME_COLUMN_NAME_ORG.ToLower(), DB_NAME_COLUMN_NAME_ORG);
                map.put(PROPERTY_NAME_COMPEL_ITEM_CHANGE_FLAG.ToLower(), DB_NAME_COMPEL_ITEM_CHANGE_FLAG);
                map.put(PROPERTY_NAME_SORT_FLAG.ToLower(), DB_NAME_SORT_FLAG);
                map.put(PROPERTY_NAME_SORT_RANGE.ToLower(), DB_NAME_SORT_RANGE);
                map.put(PROPERTY_NAME_MULTIVARIATE_FLAG.ToLower(), DB_NAME_MULTIVARIATE_FLAG);
                map.put(PROPERTY_NAME_TABLE_NO.ToLower(), DB_NAME_TABLE_NO);
                map.put(PROPERTY_NAME_COLUMN_NO.ToLower(), DB_NAME_COLUMN_NO);
                map.put(PROPERTY_NAME_TABLE_NO_ORG.ToLower(), DB_NAME_TABLE_NO_ORG);
                map.put(PROPERTY_NAME_COLUMN_NO_ORG.ToLower(), DB_NAME_COLUMN_NO_ORG);
                map.put(PROPERTY_NAME_LAST_UPDATE_USER.ToLower(), DB_NAME_LAST_UPDATE_USER);
                map.put(PROPERTY_NAME_LAST_UPDATE_DATETIME.ToLower(), DB_NAME_LAST_UPDATE_DATETIME);
                map.put(PROPERTY_NAME_NEW_AT_QC3_FLAG.ToLower(), DB_NAME_NEW_AT_QC3_FLAG);
                map.put(PROPERTY_NAME_SORT_RANGE_ORG.ToLower(), DB_NAME_SORT_RANGE_ORG);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TItemInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TItemInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TItemInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TItemInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TItemInfo NewMyEntity() { return new TItemInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TItemInfoCB NewMyConditionBean() { return new TItemInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TItemInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TItemInfo>>();

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
            RegisterEntityPropertySetupper("TABLE_NAME", "TableName", new EntityPropertyTableNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLUMN_NAME", "ColumnName", new EntityPropertyColumnNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CATEGORY_EDIT_ID", "CategoryEditId", new EntityPropertyCategoryEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("STATUS", "Status", new EntityPropertyStatusSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TABLE_NAME_ORG", "TableNameOrg", new EntityPropertyTableNameOrgSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLUMN_NAME_ORG", "ColumnNameOrg", new EntityPropertyColumnNameOrgSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COMPEL_ITEM_CHANGE_FLAG", "CompelItemChangeFlag", new EntityPropertyCompelItemChangeFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_FLAG", "SortFlag", new EntityPropertySortFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_RANGE", "SortRange", new EntityPropertySortRangeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MULTIVARIATE_FLAG", "MultivariateFlag", new EntityPropertyMultivariateFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TABLE_NO", "TableNo", new EntityPropertyTableNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLUMN_NO", "ColumnNo", new EntityPropertyColumnNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TABLE_NO_ORG", "TableNoOrg", new EntityPropertyTableNoOrgSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLUMN_NO_ORG", "ColumnNoOrg", new EntityPropertyColumnNoOrgSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_USER", "LastUpdateUser", new EntityPropertyLastUpdateUserSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_DATETIME", "LastUpdateDatetime", new EntityPropertyLastUpdateDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NEW_AT_QC3_FLAG", "NewAtQc3Flag", new EntityPropertyNewAtQc3FlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_RANGE_ORG", "SortRangeOrg", new EntityPropertySortRangeOrgSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TItemInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TItemInfo)entity, value);
        }

        public class EntityPropertyItemInfoIdSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.ItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyItemNameSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.ItemName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySourceDivSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.SourceDiv = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyItemnoSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.Itemno = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyItemTypeSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.ItemType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyAnswerTypeSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.AnswerType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySortNumberSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.SortNumber = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMatrixDivSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.MatrixDiv = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLv1titleSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.Lv1title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLv2titleSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.Lv2title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOriginalLv1titleSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.OriginalLv1title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyOriginalLv2titleSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.OriginalLv2title = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyTableNameSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.TableName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyColumnNameSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.ColumnName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCategoryEditIdSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.CategoryEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyStatusSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.Status = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTableNameOrgSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.TableNameOrg = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyColumnNameOrgSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.ColumnNameOrg = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCompelItemChangeFlagSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.CompelItemChangeFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySortFlagSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.SortFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySortRangeSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.SortRange = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMultivariateFlagSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.MultivariateFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTableNoSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.TableNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyColumnNoSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.ColumnNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTableNoOrgSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.TableNoOrg = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyColumnNoOrgSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.ColumnNoOrg = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLastUpdateUserSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.LastUpdateUser = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLastUpdateDatetimeSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.LastUpdateDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyNewAtQc3FlagSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.NewAtQc3Flag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySortRangeOrgSetupper : EntityPropertySetupper<TItemInfo> {
            public void Setup(TItemInfo entity, Object value) { entity.SortRangeOrg = (value != null) ? (int?)value : null; }
        }
    }
}
