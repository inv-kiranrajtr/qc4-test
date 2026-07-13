
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

    public class TTableDetailInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TTableDetailInfo);

        private static readonly TTableDetailInfoDbm _instance = new TTableDetailInfoDbm();
        private TTableDetailInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TTableDetailInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_TABLE_DETAIL_INFO"; } }
        public override String TablePropertyName { get { return "TTableDetailInfo"; } }
        public override String TableSqlName { get { return "T_TABLE_DETAIL_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnTableNo;
        protected ColumnInfo _columnUsedNo;

        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnTableNo { get { return _columnTableNo; } }
        public ColumnInfo ColumnUsedNo { get { return _columnUsedNo; } }

        protected void InitializeColumnInfo() {
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TTableControl", null);
            _columnTableNo = cci("TABLE_NO", "TABLE_NO", null, null, true, "TableNo", typeof(int?), true, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUsedNo = cci("USED_NO", "USED_NO", null, null, true, "UsedNo", typeof(int?), false, "NUMBER", 3, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnTableNo);
            _columnInfoList.add(ColumnUsedNo);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            List<ColumnInfo> ls = new ArrayList<ColumnInfo>();
            ls.add(ColumnQcwebid);
            ls.add(ColumnTableNo);
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
        public ForeignInfo ForeignTTableControl { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TTableControlDbm.GetInstance().ColumnQcwebid);
            return cfi("TTableControl", this, TTableControlDbm.GetInstance(), map, 0, false, false);
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
        public static readonly String TABLE_DB_NAME = "T_TABLE_DETAIL_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TTableDetailInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_TABLE_NO = "TABLE_NO";
        public static readonly String DB_NAME_USED_NO = "USED_NO";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_TABLE_NO = "TableNo";
        public static readonly String PROPERTY_NAME_USED_NO = "UsedNo";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TTableControl = "TTableControl";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TTableDetailInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_TABLE_NO.ToLower(), PROPERTY_NAME_TABLE_NO);
                map.put(DB_NAME_USED_NO.ToLower(), PROPERTY_NAME_USED_NO);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_TABLE_NO.ToLower(), DB_NAME_TABLE_NO);
                map.put(PROPERTY_NAME_USED_NO.ToLower(), DB_NAME_USED_NO);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TTableDetailInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TTableDetailInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TTableDetailInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TTableDetailInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TTableDetailInfo NewMyEntity() { return new TTableDetailInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TTableDetailInfoCB NewMyConditionBean() { return new TTableDetailInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TTableDetailInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TTableDetailInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TABLE_NO", "TableNo", new EntityPropertyTableNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("USED_NO", "UsedNo", new EntityPropertyUsedNoSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TTableDetailInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TTableDetailInfo)entity, value);
        }

        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TTableDetailInfo> {
            public void Setup(TTableDetailInfo entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTableNoSetupper : EntityPropertySetupper<TTableDetailInfo> {
            public void Setup(TTableDetailInfo entity, Object value) { entity.TableNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyUsedNoSetupper : EntityPropertySetupper<TTableDetailInfo> {
            public void Setup(TTableDetailInfo entity, Object value) { entity.UsedNo = (value != null) ? (int?)value : null; }
        }
    }
}
