
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

    public class TApplicationDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TApplication);

        private static readonly TApplicationDbm _instance = new TApplicationDbm();
        private TApplicationDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TApplicationDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_APPLICATION"; } }
        public override String TablePropertyName { get { return "TApplication"; } }
        public override String TableSqlName { get { return "T_APPLICATION"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnIdentifier;
        protected ColumnInfo _columnSettingValue;
        protected ColumnInfo _columnDescription;

        public ColumnInfo ColumnIdentifier { get { return _columnIdentifier; } }
        public ColumnInfo ColumnSettingValue { get { return _columnSettingValue; } }
        public ColumnInfo ColumnDescription { get { return _columnDescription; } }

        protected void InitializeColumnInfo() {
            _columnIdentifier = cci("IDENTIFIER", "IDENTIFIER", null, null, true, "Identifier", typeof(String), true, "VARCHAR2", 35, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSettingValue = cci("SETTING_VALUE", "SETTING_VALUE", null, null, true, "SettingValue", typeof(String), false, "VARCHAR2", 90, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDescription = cci("DESCRIPTION", "DESCRIPTION", null, null, false, "Description", typeof(String), false, "VARCHAR2", 150, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnIdentifier);
            _columnInfoList.add(ColumnSettingValue);
            _columnInfoList.add(ColumnDescription);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnIdentifier);
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
        public static readonly String TABLE_DB_NAME = "T_APPLICATION";
        public static readonly String TABLE_PROPERTY_NAME = "TApplication";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_IDENTIFIER = "IDENTIFIER";
        public static readonly String DB_NAME_SETTING_VALUE = "SETTING_VALUE";
        public static readonly String DB_NAME_DESCRIPTION = "DESCRIPTION";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_IDENTIFIER = "Identifier";
        public static readonly String PROPERTY_NAME_SETTING_VALUE = "SettingValue";
        public static readonly String PROPERTY_NAME_DESCRIPTION = "Description";

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

        static TApplicationDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_IDENTIFIER.ToLower(), PROPERTY_NAME_IDENTIFIER);
                map.put(DB_NAME_SETTING_VALUE.ToLower(), PROPERTY_NAME_SETTING_VALUE);
                map.put(DB_NAME_DESCRIPTION.ToLower(), PROPERTY_NAME_DESCRIPTION);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_IDENTIFIER.ToLower(), DB_NAME_IDENTIFIER);
                map.put(PROPERTY_NAME_SETTING_VALUE.ToLower(), DB_NAME_SETTING_VALUE);
                map.put(PROPERTY_NAME_DESCRIPTION.ToLower(), DB_NAME_DESCRIPTION);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TApplication"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TApplicationDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TApplicationCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TApplicationBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TApplication NewMyEntity() { return new TApplication(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TApplicationCB NewMyConditionBean() { return new TApplicationCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TApplication>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TApplication>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("IDENTIFIER", "Identifier", new EntityPropertyIdentifierSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SETTING_VALUE", "SettingValue", new EntityPropertySettingValueSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DESCRIPTION", "Description", new EntityPropertyDescriptionSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TApplication> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TApplication)entity, value);
        }

        public class EntityPropertyIdentifierSetupper : EntityPropertySetupper<TApplication> {
            public void Setup(TApplication entity, Object value) { entity.Identifier = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySettingValueSetupper : EntityPropertySetupper<TApplication> {
            public void Setup(TApplication entity, Object value) { entity.SettingValue = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDescriptionSetupper : EntityPropertySetupper<TApplication> {
            public void Setup(TApplication entity, Object value) { entity.Description = (value != null) ? (String)value : null; }
        }
    }
}
