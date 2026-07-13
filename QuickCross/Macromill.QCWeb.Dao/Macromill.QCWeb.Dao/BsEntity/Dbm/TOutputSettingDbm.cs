
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

    public class TOutputSettingDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputSetting);

        private static readonly TOutputSettingDbm _instance = new TOutputSettingDbm();
        private TOutputSettingDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputSettingDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SETTING"; } }
        public override String TablePropertyName { get { return "TOutputSetting"; } }
        public override String TableSqlName { get { return "T_OUTPUT_SETTING"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnOutputFileType;
        protected ColumnInfo _columnPartitionFlag;
        protected ColumnInfo _columnLayoutFlag;
        protected ColumnInfo _columnOutputType;
        protected ColumnInfo _columnNoAnswerChar;
        protected ColumnInfo _columnUnmacthChar;
        protected ColumnInfo _columnMultiItemType;
        protected ColumnInfo _columnNumberType;

        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnOutputFileType { get { return _columnOutputFileType; } }
        public ColumnInfo ColumnPartitionFlag { get { return _columnPartitionFlag; } }
        public ColumnInfo ColumnLayoutFlag { get { return _columnLayoutFlag; } }
        public ColumnInfo ColumnOutputType { get { return _columnOutputType; } }
        public ColumnInfo ColumnNoAnswerChar { get { return _columnNoAnswerChar; } }
        public ColumnInfo ColumnUnmacthChar { get { return _columnUnmacthChar; } }
        public ColumnInfo ColumnMultiItemType { get { return _columnMultiItemType; } }
        public ColumnInfo ColumnNumberType { get { return _columnNumberType; } }

        protected void InitializeColumnInfo() {
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo,TOutputHistoryItem,TQcwebSurveyInfoAsOne", "TOutputHistoryItemList");
            _columnOutputFileType = cci("OUTPUT_FILE_TYPE", "OUTPUT_FILE_TYPE", null, null, true, "OutputFileType", typeof(String), false, "VARCHAR2", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPartitionFlag = cci("PARTITION_FLAG", "PARTITION_FLAG", null, null, true, "PartitionFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLayoutFlag = cci("LAYOUT_FLAG", "LAYOUT_FLAG", null, null, true, "LayoutFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputType = cci("OUTPUT_TYPE", "OUTPUT_TYPE", null, null, false, "OutputType", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNoAnswerChar = cci("NO_ANSWER_CHAR", "NO_ANSWER_CHAR", null, null, true, "NoAnswerChar", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUnmacthChar = cci("UNMACTH_CHAR", "UNMACTH_CHAR", null, null, true, "UnmacthChar", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMultiItemType = cci("MULTI_ITEM_TYPE", "MULTI_ITEM_TYPE", null, null, true, "MultiItemType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNumberType = cci("NUMBER_TYPE", "NUMBER_TYPE", null, null, true, "NumberType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnOutputFileType);
            _columnInfoList.add(ColumnPartitionFlag);
            _columnInfoList.add(ColumnLayoutFlag);
            _columnInfoList.add(ColumnOutputType);
            _columnInfoList.add(ColumnNoAnswerChar);
            _columnInfoList.add(ColumnUnmacthChar);
            _columnInfoList.add(ColumnMultiItemType);
            _columnInfoList.add(ColumnNumberType);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnQcwebid);
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
            return cfi("TQcwebSurveyInfo", this, TQcwebSurveyInfoDbm.GetInstance(), map, 0, true, false);
        }}
        public ForeignInfo ForeignTOutputHistoryItem { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputHistoryItemDbm.GetInstance().ColumnQcwebid);
            return cfi("TOutputHistoryItem", this, TOutputHistoryItemDbm.GetInstance(), map, 1, true, false);
        }}

        public ForeignInfo ForeignTQcwebSurveyInfoAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfoAsOne", this, TQcwebSurveyInfoDbm.GetInstance(), map, 2, true, false);
        }}

        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTOutputHistoryItemList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TOutputHistoryItemDbm.GetInstance().ColumnQcwebid);
            return cri("TOutputHistoryItemList", this, TOutputHistoryItemDbm.GetInstance(), map, false);
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_SETTING";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputSetting";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_OUTPUT_FILE_TYPE = "OUTPUT_FILE_TYPE";
        public static readonly String DB_NAME_PARTITION_FLAG = "PARTITION_FLAG";
        public static readonly String DB_NAME_LAYOUT_FLAG = "LAYOUT_FLAG";
        public static readonly String DB_NAME_OUTPUT_TYPE = "OUTPUT_TYPE";
        public static readonly String DB_NAME_NO_ANSWER_CHAR = "NO_ANSWER_CHAR";
        public static readonly String DB_NAME_UNMACTH_CHAR = "UNMACTH_CHAR";
        public static readonly String DB_NAME_MULTI_ITEM_TYPE = "MULTI_ITEM_TYPE";
        public static readonly String DB_NAME_NUMBER_TYPE = "NUMBER_TYPE";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_OUTPUT_FILE_TYPE = "OutputFileType";
        public static readonly String PROPERTY_NAME_PARTITION_FLAG = "PartitionFlag";
        public static readonly String PROPERTY_NAME_LAYOUT_FLAG = "LayoutFlag";
        public static readonly String PROPERTY_NAME_OUTPUT_TYPE = "OutputType";
        public static readonly String PROPERTY_NAME_NO_ANSWER_CHAR = "NoAnswerChar";
        public static readonly String PROPERTY_NAME_UNMACTH_CHAR = "UnmacthChar";
        public static readonly String PROPERTY_NAME_MULTI_ITEM_TYPE = "MultiItemType";
        public static readonly String PROPERTY_NAME_NUMBER_TYPE = "NumberType";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputHistoryItem = "TOutputHistoryItem";
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfoAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TOutputHistoryItemList = "TOutputHistoryItemList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputSettingDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_OUTPUT_FILE_TYPE.ToLower(), PROPERTY_NAME_OUTPUT_FILE_TYPE);
                map.put(DB_NAME_PARTITION_FLAG.ToLower(), PROPERTY_NAME_PARTITION_FLAG);
                map.put(DB_NAME_LAYOUT_FLAG.ToLower(), PROPERTY_NAME_LAYOUT_FLAG);
                map.put(DB_NAME_OUTPUT_TYPE.ToLower(), PROPERTY_NAME_OUTPUT_TYPE);
                map.put(DB_NAME_NO_ANSWER_CHAR.ToLower(), PROPERTY_NAME_NO_ANSWER_CHAR);
                map.put(DB_NAME_UNMACTH_CHAR.ToLower(), PROPERTY_NAME_UNMACTH_CHAR);
                map.put(DB_NAME_MULTI_ITEM_TYPE.ToLower(), PROPERTY_NAME_MULTI_ITEM_TYPE);
                map.put(DB_NAME_NUMBER_TYPE.ToLower(), PROPERTY_NAME_NUMBER_TYPE);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_OUTPUT_FILE_TYPE.ToLower(), DB_NAME_OUTPUT_FILE_TYPE);
                map.put(PROPERTY_NAME_PARTITION_FLAG.ToLower(), DB_NAME_PARTITION_FLAG);
                map.put(PROPERTY_NAME_LAYOUT_FLAG.ToLower(), DB_NAME_LAYOUT_FLAG);
                map.put(PROPERTY_NAME_OUTPUT_TYPE.ToLower(), DB_NAME_OUTPUT_TYPE);
                map.put(PROPERTY_NAME_NO_ANSWER_CHAR.ToLower(), DB_NAME_NO_ANSWER_CHAR);
                map.put(PROPERTY_NAME_UNMACTH_CHAR.ToLower(), DB_NAME_UNMACTH_CHAR);
                map.put(PROPERTY_NAME_MULTI_ITEM_TYPE.ToLower(), DB_NAME_MULTI_ITEM_TYPE);
                map.put(PROPERTY_NAME_NUMBER_TYPE.ToLower(), DB_NAME_NUMBER_TYPE);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputSetting"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputSettingDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputSettingCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputSettingBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputSetting NewMyEntity() { return new TOutputSetting(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputSettingCB NewMyConditionBean() { return new TOutputSettingCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputSetting>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputSetting>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_FILE_TYPE", "OutputFileType", new EntityPropertyOutputFileTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PARTITION_FLAG", "PartitionFlag", new EntityPropertyPartitionFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAYOUT_FLAG", "LayoutFlag", new EntityPropertyLayoutFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TYPE", "OutputType", new EntityPropertyOutputTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NO_ANSWER_CHAR", "NoAnswerChar", new EntityPropertyNoAnswerCharSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UNMACTH_CHAR", "UnmacthChar", new EntityPropertyUnmacthCharSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MULTI_ITEM_TYPE", "MultiItemType", new EntityPropertyMultiItemTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NUMBER_TYPE", "NumberType", new EntityPropertyNumberTypeSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputSetting> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputSetting)entity, value);
        }

        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputSetting> {
            public void Setup(TOutputSetting entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputFileTypeSetupper : EntityPropertySetupper<TOutputSetting> {
            public void Setup(TOutputSetting entity, Object value) { entity.OutputFileType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyPartitionFlagSetupper : EntityPropertySetupper<TOutputSetting> {
            public void Setup(TOutputSetting entity, Object value) { entity.PartitionFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLayoutFlagSetupper : EntityPropertySetupper<TOutputSetting> {
            public void Setup(TOutputSetting entity, Object value) { entity.LayoutFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputTypeSetupper : EntityPropertySetupper<TOutputSetting> {
            public void Setup(TOutputSetting entity, Object value) { entity.OutputType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNoAnswerCharSetupper : EntityPropertySetupper<TOutputSetting> {
            public void Setup(TOutputSetting entity, Object value) { entity.NoAnswerChar = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyUnmacthCharSetupper : EntityPropertySetupper<TOutputSetting> {
            public void Setup(TOutputSetting entity, Object value) { entity.UnmacthChar = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyMultiItemTypeSetupper : EntityPropertySetupper<TOutputSetting> {
            public void Setup(TOutputSetting entity, Object value) { entity.MultiItemType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNumberTypeSetupper : EntityPropertySetupper<TOutputSetting> {
            public void Setup(TOutputSetting entity, Object value) { entity.NumberType = (value != null) ? (int?)value : null; }
        }
    }
}
