
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

    public class TColorInfoDetailCrossDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TColorInfoDetailCross);

        private static readonly TColorInfoDetailCrossDbm _instance = new TColorInfoDetailCrossDbm();
        private TColorInfoDetailCrossDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TColorInfoDetailCrossDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_INFO_DETAIL_CROSS"; } }
        public override String TablePropertyName { get { return "TColorInfoDetailCross"; } }
        public override String TableSqlName { get { return "T_COLOR_INFO_DETAIL_CROSS"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnColorInfoDetailCrossId;
        protected ColumnInfo _columnGraphColorNo;
        protected ColumnInfo _columnColorCode;
        protected ColumnInfo _columnPatternCode;
        protected ColumnInfo _columnColorSetInfoCrossId;

        public ColumnInfo ColumnColorInfoDetailCrossId { get { return _columnColorInfoDetailCrossId; } }
        public ColumnInfo ColumnGraphColorNo { get { return _columnGraphColorNo; } }
        public ColumnInfo ColumnColorCode { get { return _columnColorCode; } }
        public ColumnInfo ColumnPatternCode { get { return _columnPatternCode; } }
        public ColumnInfo ColumnColorSetInfoCrossId { get { return _columnColorSetInfoCrossId; } }

        protected void InitializeColumnInfo() {
            _columnColorInfoDetailCrossId = cci("COLOR_INFO_DETAIL_CROSS_ID", "COLOR_INFO_DETAIL_CROSS_ID", null, null, true, "ColorInfoDetailCrossId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphColorNo = cci("GRAPH_COLOR_NO", "GRAPH_COLOR_NO", null, null, true, "GraphColorNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColorCode = cci("COLOR_CODE", "COLOR_CODE", null, null, true, "ColorCode", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPatternCode = cci("PATTERN_CODE", "PATTERN_CODE", null, null, false, "PatternCode", typeof(String), false, "VARCHAR2", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColorSetInfoCrossId = cci("COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", null, null, true, "ColorSetInfoCrossId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TColorSetInfoCross", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnColorInfoDetailCrossId);
            _columnInfoList.add(ColumnGraphColorNo);
            _columnInfoList.add(ColumnColorCode);
            _columnInfoList.add(ColumnPatternCode);
            _columnInfoList.add(ColumnColorSetInfoCrossId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnColorInfoDetailCrossId);
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
        public ForeignInfo ForeignTColorSetInfoCross { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnColorSetInfoCrossId, TColorSetInfoCrossDbm.GetInstance().ColumnColorSetInfoCrossId);
            return cfi("TColorSetInfoCross", this, TColorSetInfoCrossDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Color_Info_Detail_CrossSEQ1"; } }
        public override String SequenceNextValSql { get { return "select T_Color_Info_Detail_CrossSEQ1.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_COLOR_INFO_DETAIL_CROSS";
        public static readonly String TABLE_PROPERTY_NAME = "TColorInfoDetailCross";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_COLOR_INFO_DETAIL_CROSS_ID = "COLOR_INFO_DETAIL_CROSS_ID";
        public static readonly String DB_NAME_GRAPH_COLOR_NO = "GRAPH_COLOR_NO";
        public static readonly String DB_NAME_COLOR_CODE = "COLOR_CODE";
        public static readonly String DB_NAME_PATTERN_CODE = "PATTERN_CODE";
        public static readonly String DB_NAME_COLOR_SET_INFO_CROSS_ID = "COLOR_SET_INFO_CROSS_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_COLOR_INFO_DETAIL_CROSS_ID = "ColorInfoDetailCrossId";
        public static readonly String PROPERTY_NAME_GRAPH_COLOR_NO = "GraphColorNo";
        public static readonly String PROPERTY_NAME_COLOR_CODE = "ColorCode";
        public static readonly String PROPERTY_NAME_PATTERN_CODE = "PatternCode";
        public static readonly String PROPERTY_NAME_COLOR_SET_INFO_CROSS_ID = "ColorSetInfoCrossId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TColorSetInfoCross = "TColorSetInfoCross";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TColorInfoDetailCrossDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_COLOR_INFO_DETAIL_CROSS_ID.ToLower(), PROPERTY_NAME_COLOR_INFO_DETAIL_CROSS_ID);
                map.put(DB_NAME_GRAPH_COLOR_NO.ToLower(), PROPERTY_NAME_GRAPH_COLOR_NO);
                map.put(DB_NAME_COLOR_CODE.ToLower(), PROPERTY_NAME_COLOR_CODE);
                map.put(DB_NAME_PATTERN_CODE.ToLower(), PROPERTY_NAME_PATTERN_CODE);
                map.put(DB_NAME_COLOR_SET_INFO_CROSS_ID.ToLower(), PROPERTY_NAME_COLOR_SET_INFO_CROSS_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_COLOR_INFO_DETAIL_CROSS_ID.ToLower(), DB_NAME_COLOR_INFO_DETAIL_CROSS_ID);
                map.put(PROPERTY_NAME_GRAPH_COLOR_NO.ToLower(), DB_NAME_GRAPH_COLOR_NO);
                map.put(PROPERTY_NAME_COLOR_CODE.ToLower(), DB_NAME_COLOR_CODE);
                map.put(PROPERTY_NAME_PATTERN_CODE.ToLower(), DB_NAME_PATTERN_CODE);
                map.put(PROPERTY_NAME_COLOR_SET_INFO_CROSS_ID.ToLower(), DB_NAME_COLOR_SET_INFO_CROSS_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TColorInfoDetailCross"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TColorInfoDetailCrossDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TColorInfoDetailCrossCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TColorInfoDetailCrossBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TColorInfoDetailCross NewMyEntity() { return new TColorInfoDetailCross(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TColorInfoDetailCrossCB NewMyConditionBean() { return new TColorInfoDetailCrossCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TColorInfoDetailCross>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TColorInfoDetailCross>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("COLOR_INFO_DETAIL_CROSS_ID", "ColorInfoDetailCrossId", new EntityPropertyColorInfoDetailCrossIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_COLOR_NO", "GraphColorNo", new EntityPropertyGraphColorNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLOR_CODE", "ColorCode", new EntityPropertyColorCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PATTERN_CODE", "PatternCode", new EntityPropertyPatternCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLOR_SET_INFO_CROSS_ID", "ColorSetInfoCrossId", new EntityPropertyColorSetInfoCrossIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TColorInfoDetailCross> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TColorInfoDetailCross)entity, value);
        }

        public class EntityPropertyColorInfoDetailCrossIdSetupper : EntityPropertySetupper<TColorInfoDetailCross> {
            public void Setup(TColorInfoDetailCross entity, Object value) { entity.ColorInfoDetailCrossId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyGraphColorNoSetupper : EntityPropertySetupper<TColorInfoDetailCross> {
            public void Setup(TColorInfoDetailCross entity, Object value) { entity.GraphColorNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyColorCodeSetupper : EntityPropertySetupper<TColorInfoDetailCross> {
            public void Setup(TColorInfoDetailCross entity, Object value) { entity.ColorCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPatternCodeSetupper : EntityPropertySetupper<TColorInfoDetailCross> {
            public void Setup(TColorInfoDetailCross entity, Object value) { entity.PatternCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyColorSetInfoCrossIdSetupper : EntityPropertySetupper<TColorInfoDetailCross> {
            public void Setup(TColorInfoDetailCross entity, Object value) { entity.ColorSetInfoCrossId = (value != null) ? (decimal?)value : null; }
        }
    }
}
