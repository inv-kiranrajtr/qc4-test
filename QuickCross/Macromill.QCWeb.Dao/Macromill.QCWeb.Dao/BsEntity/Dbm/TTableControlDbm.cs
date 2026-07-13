
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

    public class TTableControlDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TTableControl);

        private static readonly TTableControlDbm _instance = new TTableControlDbm();
        private TTableControlDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TTableControlDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_TABLE_CONTROL"; } }
        public override String TablePropertyName { get { return "TTableControl"; } }
        public override String TableSqlName { get { return "T_TABLE_CONTROL"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnBaseTableName;
        protected ColumnInfo _columnActiveTableNo;
        protected ColumnInfo _columnMaxNo;

        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnBaseTableName { get { return _columnBaseTableName; } }
        public ColumnInfo ColumnActiveTableNo { get { return _columnActiveTableNo; } }
        public ColumnInfo ColumnMaxNo { get { return _columnMaxNo; } }

        protected void InitializeColumnInfo() {
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TTableDetailInfoList,TItemInfoList");
            _columnBaseTableName = cci("BASE_TABLE_NAME", "BASE_TABLE_NAME", null, null, true, "BaseTableName", typeof(String), false, "VARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnActiveTableNo = cci("ACTIVE_TABLE_NO", "ACTIVE_TABLE_NO", null, null, true, "ActiveTableNo", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMaxNo = cci("MAX_NO", "MAX_NO", null, null, true, "MaxNo", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnBaseTableName);
            _columnInfoList.add(ColumnActiveTableNo);
            _columnInfoList.add(ColumnMaxNo);
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

        public ForeignInfo ForeignTQcwebSurveyInfoAsOne { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfoAsOne", this, TQcwebSurveyInfoDbm.GetInstance(), map, 0, true, false);
        }}

        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTTableDetailInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TTableDetailInfoDbm.GetInstance().ColumnQcwebid);
            return cri("TTableDetailInfoList", this, TTableDetailInfoDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTItemInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TItemInfoDbm.GetInstance().ColumnQcwebid);
            return cri("TItemInfoList", this, TItemInfoDbm.GetInstance(), map, false);
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
        public static readonly String TABLE_DB_NAME = "T_TABLE_CONTROL";
        public static readonly String TABLE_PROPERTY_NAME = "TTableControl";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_BASE_TABLE_NAME = "BASE_TABLE_NAME";
        public static readonly String DB_NAME_ACTIVE_TABLE_NO = "ACTIVE_TABLE_NO";
        public static readonly String DB_NAME_MAX_NO = "MAX_NO";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_BASE_TABLE_NAME = "BaseTableName";
        public static readonly String PROPERTY_NAME_ACTIVE_TABLE_NO = "ActiveTableNo";
        public static readonly String PROPERTY_NAME_MAX_NO = "MaxNo";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfoAsOne = "$foreignKeys.foreignPropertyNameInitCap";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TTableDetailInfoList = "TTableDetailInfoList";
        public static readonly String REFERRER_PROPERTY_NAME_TItemInfoList = "TItemInfoList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TTableControlDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_BASE_TABLE_NAME.ToLower(), PROPERTY_NAME_BASE_TABLE_NAME);
                map.put(DB_NAME_ACTIVE_TABLE_NO.ToLower(), PROPERTY_NAME_ACTIVE_TABLE_NO);
                map.put(DB_NAME_MAX_NO.ToLower(), PROPERTY_NAME_MAX_NO);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_BASE_TABLE_NAME.ToLower(), DB_NAME_BASE_TABLE_NAME);
                map.put(PROPERTY_NAME_ACTIVE_TABLE_NO.ToLower(), DB_NAME_ACTIVE_TABLE_NO);
                map.put(PROPERTY_NAME_MAX_NO.ToLower(), DB_NAME_MAX_NO);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TTableControl"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TTableControlDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TTableControlCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TTableControlBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TTableControl NewMyEntity() { return new TTableControl(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TTableControlCB NewMyConditionBean() { return new TTableControlCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TTableControl>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TTableControl>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("BASE_TABLE_NAME", "BaseTableName", new EntityPropertyBaseTableNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ACTIVE_TABLE_NO", "ActiveTableNo", new EntityPropertyActiveTableNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MAX_NO", "MaxNo", new EntityPropertyMaxNoSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TTableControl> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TTableControl)entity, value);
        }

        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TTableControl> {
            public void Setup(TTableControl entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyBaseTableNameSetupper : EntityPropertySetupper<TTableControl> {
            public void Setup(TTableControl entity, Object value) { entity.BaseTableName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyActiveTableNoSetupper : EntityPropertySetupper<TTableControl> {
            public void Setup(TTableControl entity, Object value) { entity.ActiveTableNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMaxNoSetupper : EntityPropertySetupper<TTableControl> {
            public void Setup(TTableControl entity, Object value) { entity.MaxNo = (value != null) ? (int?)value : null; }
        }
    }
}
