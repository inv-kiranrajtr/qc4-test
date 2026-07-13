
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

    public class TOutputHistoryItemDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputHistoryItem);

        private static readonly TOutputHistoryItemDbm _instance = new TOutputHistoryItemDbm();
        private TOutputHistoryItemDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputHistoryItemDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_HISTORY_ITEM"; } }
        public override String TablePropertyName { get { return "TOutputHistoryItem"; } }
        public override String TableSqlName { get { return "T_OUTPUT_HISTORY_ITEM"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputHistoryItemId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnItemInfoId;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnOutputFlag;

        public ColumnInfo ColumnOutputHistoryItemId { get { return _columnOutputHistoryItemId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnItemInfoId { get { return _columnItemInfoId; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnOutputFlag { get { return _columnOutputFlag; } }

        protected void InitializeColumnInfo() {
            _columnOutputHistoryItemId = cci("OUTPUT_HISTORY_ITEM_ID", "OUTPUT_HISTORY_ITEM_ID", null, null, true, "OutputHistoryItemId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputSetting", null);
            _columnItemInfoId = cci("ITEM_INFO_ID", "ITEM_INFO_ID", null, null, true, "ItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputFlag = cci("OUTPUT_FLAG", "OUTPUT_FLAG", null, null, true, "OutputFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputHistoryItemId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnItemInfoId);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnOutputFlag);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputHistoryItemId);
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
        public ForeignInfo ForeignTOutputSetting { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputSettingDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputSetting", this, TOutputSettingDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Output_History_Item_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Output_History_Item_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_HISTORY_ITEM";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputHistoryItem";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_HISTORY_ITEM_ID = "OUTPUT_HISTORY_ITEM_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_ITEM_INFO_ID = "ITEM_INFO_ID";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_OUTPUT_FLAG = "OUTPUT_FLAG";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_HISTORY_ITEM_ID = "OutputHistoryItemId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_ITEM_INFO_ID = "ItemInfoId";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_OUTPUT_FLAG = "OutputFlag";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputSetting = "TOutputSetting";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputHistoryItemDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_HISTORY_ITEM_ID.ToLower(), PROPERTY_NAME_OUTPUT_HISTORY_ITEM_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_ITEM_INFO_ID.ToLower(), PROPERTY_NAME_ITEM_INFO_ID);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_OUTPUT_FLAG.ToLower(), PROPERTY_NAME_OUTPUT_FLAG);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_HISTORY_ITEM_ID.ToLower(), DB_NAME_OUTPUT_HISTORY_ITEM_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_ITEM_INFO_ID.ToLower(), DB_NAME_ITEM_INFO_ID);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_OUTPUT_FLAG.ToLower(), DB_NAME_OUTPUT_FLAG);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputHistoryItem"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputHistoryItemDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputHistoryItemCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputHistoryItemBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputHistoryItem NewMyEntity() { return new TOutputHistoryItem(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputHistoryItemCB NewMyConditionBean() { return new TOutputHistoryItemCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputHistoryItem>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputHistoryItem>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_HISTORY_ITEM_ID", "OutputHistoryItemId", new EntityPropertyOutputHistoryItemIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_INFO_ID", "ItemInfoId", new EntityPropertyItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_FLAG", "OutputFlag", new EntityPropertyOutputFlagSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputHistoryItem> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputHistoryItem)entity, value);
        }

        public class EntityPropertyOutputHistoryItemIdSetupper : EntityPropertySetupper<TOutputHistoryItem> {
            public void Setup(TOutputHistoryItem entity, Object value) { entity.OutputHistoryItemId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputHistoryItem> {
            public void Setup(TOutputHistoryItem entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyItemInfoIdSetupper : EntityPropertySetupper<TOutputHistoryItem> {
            public void Setup(TOutputHistoryItem entity, Object value) { entity.ItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TOutputHistoryItem> {
            public void Setup(TOutputHistoryItem entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputFlagSetupper : EntityPropertySetupper<TOutputHistoryItem> {
            public void Setup(TOutputHistoryItem entity, Object value) { entity.OutputFlag = (value != null) ? (int?)value : null; }
        }
    }
}
