
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity.Customize;
namespace Macromill.QCWeb.Dao.BsEntity.Customize.Dbm {

    public class RawDataItemViewFAListEntityDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(RawDataItemViewFAListEntity);

        private static readonly RawDataItemViewFAListEntityDbm _instance = new RawDataItemViewFAListEntityDbm();
        private RawDataItemViewFAListEntityDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static RawDataItemViewFAListEntityDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "RawDataItemViewFAListEntity"; } }
        public override String TablePropertyName { get { return "RawDataItemViewFAListEntity"; } }
        public override String TableSqlName { get { return "RawDataItemViewFAListEntity"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnSampleId;
        protected ColumnInfo _columnRawdata;

        public ColumnInfo ColumnSampleId { get { return _columnSampleId; } }
        public ColumnInfo ColumnRawdata { get { return _columnRawdata; } }

        protected void InitializeColumnInfo() {
            _columnSampleId = cci("SAMPLE_ID", "SAMPLE_ID", null, null, false, "SampleId", typeof(String), false, "VARCHAR2", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRawdata = cci("RAWDATA", "RAWDATA", null, null, false, "Rawdata", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnSampleId);
            _columnInfoList.add(ColumnRawdata);
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
        public static readonly String TABLE_DB_NAME = "RawDataItemViewFAListEntity";
        public static readonly String TABLE_PROPERTY_NAME = "RawDataItemViewFAListEntity";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_SAMPLE_ID = "SAMPLE_ID";
        public static readonly String DB_NAME_RAWDATA = "RAWDATA";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_SAMPLE_ID = "SampleId";
        public static readonly String PROPERTY_NAME_RAWDATA = "Rawdata";

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

        static RawDataItemViewFAListEntityDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_SAMPLE_ID.ToLower(), PROPERTY_NAME_SAMPLE_ID);
                map.put(DB_NAME_RAWDATA.ToLower(), PROPERTY_NAME_RAWDATA);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_SAMPLE_ID.ToLower(), DB_NAME_SAMPLE_ID);
                map.put(PROPERTY_NAME_RAWDATA.ToLower(), DB_NAME_RAWDATA);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.Customize.RawDataItemViewFAListEntity"; } }
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
        public RawDataItemViewFAListEntity NewMyEntity() { return new RawDataItemViewFAListEntity(); }
        public override ConditionBean NewConditionBean() {
            String msg = "The entity does not have condition-bean. So this method is invalid.";
            throw new SystemException(msg + " dbmeta=" + ToString());
        }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<RawDataItemViewFAListEntity>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<RawDataItemViewFAListEntity>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("SAMPLE_ID", "SampleId", new EntityPropertySampleIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RAWDATA", "Rawdata", new EntityPropertyRawdataSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<RawDataItemViewFAListEntity> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((RawDataItemViewFAListEntity)entity, value);
        }

        public class EntityPropertySampleIdSetupper : EntityPropertySetupper<RawDataItemViewFAListEntity> {
            public void Setup(RawDataItemViewFAListEntity entity, Object value) { entity.SampleId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyRawdataSetupper : EntityPropertySetupper<RawDataItemViewFAListEntity> {
            public void Setup(RawDataItemViewFAListEntity entity, Object value) { entity.Rawdata = (value != null) ? (String)value : null; }
        }
    }
}
