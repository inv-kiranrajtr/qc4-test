
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class TItemInfoSimpleDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TItemInfoSimple);

        private static readonly TItemInfoSimpleDbm _instance = new TItemInfoSimpleDbm();
        private TItemInfoSimpleDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TItemInfoSimpleDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "TItemInfoSimple"; } }
        public override String TablePropertyName { get { return "TItemInfoSimple"; } }
        public override String TableSqlName { get { return "TItemInfoSimple"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnItemInfoId;
        protected ColumnInfo _columnItemName;
        protected ColumnInfo _columnDataEditId;
        protected ColumnInfo _columnStatus;

        public ColumnInfo ColumnItemInfoId { get { return _columnItemInfoId; } }
        public ColumnInfo ColumnItemName { get { return _columnItemName; } }
        public ColumnInfo ColumnDataEditId { get { return _columnDataEditId; } }
        public ColumnInfo ColumnStatus { get { return _columnStatus; } }

        protected void InitializeColumnInfo() {
            _columnItemInfoId = cci("ITEM_INFO_ID", "ITEM_INFO_ID", null, null, false, "ItemInfoId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnItemName = cci("ITEM_NAME", "ITEM_NAME", null, null, false, "ItemName", typeof(String), false, "VARCHAR2", 26, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDataEditId = cci("DATA_EDIT_ID", "DATA_EDIT_ID", null, null, false, "DataEditId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnStatus = cci("STATUS", "STATUS", null, null, false, "Status", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnItemInfoId);
            _columnInfoList.add(ColumnItemName);
            _columnInfoList.add(ColumnDataEditId);
            _columnInfoList.add(ColumnStatus);
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
        public static readonly String TABLE_DB_NAME = "TItemInfoSimple";
        public static readonly String TABLE_PROPERTY_NAME = "TItemInfoSimple";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_ITEM_INFO_ID = "ITEM_INFO_ID";
        public static readonly String DB_NAME_ITEM_NAME = "ITEM_NAME";
        public static readonly String DB_NAME_DATA_EDIT_ID = "DATA_EDIT_ID";
        public static readonly String DB_NAME_STATUS = "STATUS";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_ITEM_INFO_ID = "ItemInfoId";
        public static readonly String PROPERTY_NAME_ITEM_NAME = "ItemName";
        public static readonly String PROPERTY_NAME_DATA_EDIT_ID = "DataEditId";
        public static readonly String PROPERTY_NAME_STATUS = "Status";

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

        static TItemInfoSimpleDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_ITEM_INFO_ID.ToLower(), PROPERTY_NAME_ITEM_INFO_ID);
                map.put(DB_NAME_ITEM_NAME.ToLower(), PROPERTY_NAME_ITEM_NAME);
                map.put(DB_NAME_DATA_EDIT_ID.ToLower(), PROPERTY_NAME_DATA_EDIT_ID);
                map.put(DB_NAME_STATUS.ToLower(), PROPERTY_NAME_STATUS);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_ITEM_INFO_ID.ToLower(), DB_NAME_ITEM_INFO_ID);
                map.put(PROPERTY_NAME_ITEM_NAME.ToLower(), DB_NAME_ITEM_NAME);
                map.put(PROPERTY_NAME_DATA_EDIT_ID.ToLower(), DB_NAME_DATA_EDIT_ID);
                map.put(PROPERTY_NAME_STATUS.ToLower(), DB_NAME_STATUS);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.TItemInfoSimple"; } }
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
        public TItemInfoSimple NewMyEntity() { return new TItemInfoSimple(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TItemInfoSimple>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TItemInfoSimple>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("ITEM_INFO_ID", "ItemInfoId", new EntityPropertyItemInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ITEM_NAME", "ItemName", new EntityPropertyItemNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DATA_EDIT_ID", "DataEditId", new EntityPropertyDataEditIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("STATUS", "Status", new EntityPropertyStatusSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TItemInfoSimple> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TItemInfoSimple)entity, value);
        }

        public class EntityPropertyItemInfoIdSetupper : EntityPropertySetupper<TItemInfoSimple> {
            public void Setup(TItemInfoSimple entity, Object value) { entity.ItemInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyItemNameSetupper : EntityPropertySetupper<TItemInfoSimple> {
            public void Setup(TItemInfoSimple entity, Object value) { entity.ItemName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDataEditIdSetupper : EntityPropertySetupper<TItemInfoSimple> {
            public void Setup(TItemInfoSimple entity, Object value) { entity.DataEditId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyStatusSetupper : EntityPropertySetupper<TItemInfoSimple> {
            public void Setup(TItemInfoSimple entity, Object value) { entity.Status = (value != null) ? (int?)value : null; }
        }
    }
}
