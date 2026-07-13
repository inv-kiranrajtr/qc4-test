
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

    public class TAllocationCellInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TAllocationCellInfo);

        private static readonly TAllocationCellInfoDbm _instance = new TAllocationCellInfoDbm();
        private TAllocationCellInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TAllocationCellInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_ALLOCATION_CELL_INFO"; } }
        public override String TablePropertyName { get { return "TAllocationCellInfo"; } }
        public override String TableSqlName { get { return "T_ALLOCATION_CELL_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnAllocationCellId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnCellNo;
        protected ColumnInfo _columnCellName;
        protected ColumnInfo _columnExpectationSampleCount;
        protected ColumnInfo _columnValidSampleCount;

        public ColumnInfo ColumnAllocationCellId { get { return _columnAllocationCellId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnCellNo { get { return _columnCellNo; } }
        public ColumnInfo ColumnCellName { get { return _columnCellName; } }
        public ColumnInfo ColumnExpectationSampleCount { get { return _columnExpectationSampleCount; } }
        public ColumnInfo ColumnValidSampleCount { get { return _columnValidSampleCount; } }

        protected void InitializeColumnInfo() {
            _columnAllocationCellId = cci("ALLOCATION_CELL_ID", "ALLOCATION_CELL_ID", null, null, true, "AllocationCellId", typeof(long?), true, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo,TQcwebSurveyInfoAsOne", "");
            _columnCellNo = cci("CELL_NO", "CELL_NO", null, null, false, "CellNo", typeof(String), false, "NVARCHAR2", 8, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCellName = cci("CELL_NAME", "CELL_NAME", null, null, false, "CellName", typeof(String), false, "NVARCHAR2", 1000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnExpectationSampleCount = cci("EXPECTATION_SAMPLE_COUNT", "EXPECTATION_SAMPLE_COUNT", null, null, false, "ExpectationSampleCount", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnValidSampleCount = cci("VALID_SAMPLE_COUNT", "VALID_SAMPLE_COUNT", null, null, false, "ValidSampleCount", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnAllocationCellId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnCellNo);
            _columnInfoList.add(ColumnCellName);
            _columnInfoList.add(ColumnExpectationSampleCount);
            _columnInfoList.add(ColumnValidSampleCount);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            List<ColumnInfo> ls = new ArrayList<ColumnInfo>();
            ls.add(ColumnAllocationCellId);
            ls.add(ColumnQcwebid);
            return cpui(ls);
        }}

        // -------------------------------------------------
        //                                   Primary Element
        //                                   ---------------
        public override bool HasPrimaryKey { get { return true; } }
        public override bool HasCompoundPrimaryKey { get { return true; } }

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

        public ForeignInfo ForeignTQcwebSurveyInfoAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfoAsOne", this, TQcwebSurveyInfoDbm.GetInstance(), map, 1, true, false);
        }}

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
        public static readonly String TABLE_DB_NAME = "T_ALLOCATION_CELL_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TAllocationCellInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_ALLOCATION_CELL_ID = "ALLOCATION_CELL_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_CELL_NO = "CELL_NO";
        public static readonly String DB_NAME_CELL_NAME = "CELL_NAME";
        public static readonly String DB_NAME_EXPECTATION_SAMPLE_COUNT = "EXPECTATION_SAMPLE_COUNT";
        public static readonly String DB_NAME_VALID_SAMPLE_COUNT = "VALID_SAMPLE_COUNT";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_ALLOCATION_CELL_ID = "AllocationCellId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_CELL_NO = "CellNo";
        public static readonly String PROPERTY_NAME_CELL_NAME = "CellName";
        public static readonly String PROPERTY_NAME_EXPECTATION_SAMPLE_COUNT = "ExpectationSampleCount";
        public static readonly String PROPERTY_NAME_VALID_SAMPLE_COUNT = "ValidSampleCount";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfoAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TAllocationCellInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_ALLOCATION_CELL_ID.ToLower(), PROPERTY_NAME_ALLOCATION_CELL_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_CELL_NO.ToLower(), PROPERTY_NAME_CELL_NO);
                map.put(DB_NAME_CELL_NAME.ToLower(), PROPERTY_NAME_CELL_NAME);
                map.put(DB_NAME_EXPECTATION_SAMPLE_COUNT.ToLower(), PROPERTY_NAME_EXPECTATION_SAMPLE_COUNT);
                map.put(DB_NAME_VALID_SAMPLE_COUNT.ToLower(), PROPERTY_NAME_VALID_SAMPLE_COUNT);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_ALLOCATION_CELL_ID.ToLower(), DB_NAME_ALLOCATION_CELL_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_CELL_NO.ToLower(), DB_NAME_CELL_NO);
                map.put(PROPERTY_NAME_CELL_NAME.ToLower(), DB_NAME_CELL_NAME);
                map.put(PROPERTY_NAME_EXPECTATION_SAMPLE_COUNT.ToLower(), DB_NAME_EXPECTATION_SAMPLE_COUNT);
                map.put(PROPERTY_NAME_VALID_SAMPLE_COUNT.ToLower(), DB_NAME_VALID_SAMPLE_COUNT);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TAllocationCellInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TAllocationCellInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TAllocationCellInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TAllocationCellInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TAllocationCellInfo NewMyEntity() { return new TAllocationCellInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TAllocationCellInfoCB NewMyConditionBean() { return new TAllocationCellInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TAllocationCellInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TAllocationCellInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("ALLOCATION_CELL_ID", "AllocationCellId", new EntityPropertyAllocationCellIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CELL_NO", "CellNo", new EntityPropertyCellNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CELL_NAME", "CellName", new EntityPropertyCellNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("EXPECTATION_SAMPLE_COUNT", "ExpectationSampleCount", new EntityPropertyExpectationSampleCountSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VALID_SAMPLE_COUNT", "ValidSampleCount", new EntityPropertyValidSampleCountSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TAllocationCellInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TAllocationCellInfo)entity, value);
        }

        public class EntityPropertyAllocationCellIdSetupper : EntityPropertySetupper<TAllocationCellInfo> {
            public void Setup(TAllocationCellInfo entity, Object value) { entity.AllocationCellId = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TAllocationCellInfo> {
            public void Setup(TAllocationCellInfo entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyCellNoSetupper : EntityPropertySetupper<TAllocationCellInfo> {
            public void Setup(TAllocationCellInfo entity, Object value) { entity.CellNo = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCellNameSetupper : EntityPropertySetupper<TAllocationCellInfo> {
            public void Setup(TAllocationCellInfo entity, Object value) { entity.CellName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyExpectationSampleCountSetupper : EntityPropertySetupper<TAllocationCellInfo> {
            public void Setup(TAllocationCellInfo entity, Object value) { entity.ExpectationSampleCount = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyValidSampleCountSetupper : EntityPropertySetupper<TAllocationCellInfo> {
            public void Setup(TAllocationCellInfo entity, Object value) { entity.ValidSampleCount = (value != null) ? (decimal?)value : null; }
        }
    }
}
