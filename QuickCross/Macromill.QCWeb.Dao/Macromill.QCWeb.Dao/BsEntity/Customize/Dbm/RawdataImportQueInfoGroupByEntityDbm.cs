
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class RawdataImportQueInfoGroupByEntityDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(RawdataImportQueInfoGroupByEntity);

        private static readonly RawdataImportQueInfoGroupByEntityDbm _instance = new RawdataImportQueInfoGroupByEntityDbm();
        private RawdataImportQueInfoGroupByEntityDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static RawdataImportQueInfoGroupByEntityDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "RawdataImportQueInfoGroupByEntity"; } }
        public override String TablePropertyName { get { return "RawdataImportQueInfoGroupByEntity"; } }
        public override String TableSqlName { get { return "RawdataImportQueInfoGroupByEntity"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnMainSurveyId;

        public ColumnInfo ColumnMainSurveyId { get { return _columnMainSurveyId; } }

        protected void InitializeColumnInfo() {
            _columnMainSurveyId = cci("MAIN_SURVEY_ID", "MAIN_SURVEY_ID", null, null, false, "MainSurveyId", typeof(decimal?), false, "NUMBER", 22, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnMainSurveyId);
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
        public static readonly String TABLE_DB_NAME = "RawdataImportQueInfoGroupByEntity";
        public static readonly String TABLE_PROPERTY_NAME = "RawdataImportQueInfoGroupByEntity";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_MAIN_SURVEY_ID = "MAIN_SURVEY_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_MAIN_SURVEY_ID = "MainSurveyId";

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

        static RawdataImportQueInfoGroupByEntityDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_MAIN_SURVEY_ID.ToLower(), PROPERTY_NAME_MAIN_SURVEY_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_MAIN_SURVEY_ID.ToLower(), DB_NAME_MAIN_SURVEY_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.RawdataImportQueInfoGroupByEntity"; } }
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
        public RawdataImportQueInfoGroupByEntity NewMyEntity() { return new RawdataImportQueInfoGroupByEntity(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<RawdataImportQueInfoGroupByEntity>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<RawdataImportQueInfoGroupByEntity>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("MAIN_SURVEY_ID", "MainSurveyId", new EntityPropertyMainSurveyIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<RawdataImportQueInfoGroupByEntity> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((RawdataImportQueInfoGroupByEntity)entity, value);
        }

        public class EntityPropertyMainSurveyIdSetupper : EntityPropertySetupper<RawdataImportQueInfoGroupByEntity> {
            public void Setup(RawdataImportQueInfoGroupByEntity entity, Object value) { entity.MainSurveyId = (value != null) ? (decimal?)value : null; }
        }
    }
}
