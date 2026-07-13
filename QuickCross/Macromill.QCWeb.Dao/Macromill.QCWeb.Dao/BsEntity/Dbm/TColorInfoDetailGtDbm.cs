
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

    public class TColorInfoDetailGtDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TColorInfoDetailGt);

        private static readonly TColorInfoDetailGtDbm _instance = new TColorInfoDetailGtDbm();
        private TColorInfoDetailGtDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TColorInfoDetailGtDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_INFO_DETAIL_GT"; } }
        public override String TablePropertyName { get { return "TColorInfoDetailGt"; } }
        public override String TableSqlName { get { return "T_COLOR_INFO_DETAIL_GT"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnColorInfoDetailGtId;
        protected ColumnInfo _columnGraphColorNo;
        protected ColumnInfo _columnColorCode;
        protected ColumnInfo _columnPatternCode;
        protected ColumnInfo _columnColorSetInfoGtId;

        public ColumnInfo ColumnColorInfoDetailGtId { get { return _columnColorInfoDetailGtId; } }
        public ColumnInfo ColumnGraphColorNo { get { return _columnGraphColorNo; } }
        public ColumnInfo ColumnColorCode { get { return _columnColorCode; } }
        public ColumnInfo ColumnPatternCode { get { return _columnPatternCode; } }
        public ColumnInfo ColumnColorSetInfoGtId { get { return _columnColorSetInfoGtId; } }

        protected void InitializeColumnInfo() {
            _columnColorInfoDetailGtId = cci("COLOR_INFO_DETAIL_GT_ID", "COLOR_INFO_DETAIL_GT_ID", null, null, true, "ColorInfoDetailGtId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphColorNo = cci("GRAPH_COLOR_NO", "GRAPH_COLOR_NO", null, null, true, "GraphColorNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColorCode = cci("COLOR_CODE", "COLOR_CODE", null, null, true, "ColorCode", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPatternCode = cci("PATTERN_CODE", "PATTERN_CODE", null, null, false, "PatternCode", typeof(String), false, "VARCHAR2", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnColorSetInfoGtId = cci("COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", null, null, true, "ColorSetInfoGtId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TColorSetInfoGt", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnColorInfoDetailGtId);
            _columnInfoList.add(ColumnGraphColorNo);
            _columnInfoList.add(ColumnColorCode);
            _columnInfoList.add(ColumnPatternCode);
            _columnInfoList.add(ColumnColorSetInfoGtId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnColorInfoDetailGtId);
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
        public ForeignInfo ForeignTColorSetInfoGt { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnColorSetInfoGtId, TColorSetInfoGtDbm.GetInstance().ColumnColorSetInfoGtId);
            return cfi("TColorSetInfoGt", this, TColorSetInfoGtDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Color_Info_Detail_GT_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Color_Info_Detail_GT_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_COLOR_INFO_DETAIL_GT";
        public static readonly String TABLE_PROPERTY_NAME = "TColorInfoDetailGt";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_COLOR_INFO_DETAIL_GT_ID = "COLOR_INFO_DETAIL_GT_ID";
        public static readonly String DB_NAME_GRAPH_COLOR_NO = "GRAPH_COLOR_NO";
        public static readonly String DB_NAME_COLOR_CODE = "COLOR_CODE";
        public static readonly String DB_NAME_PATTERN_CODE = "PATTERN_CODE";
        public static readonly String DB_NAME_COLOR_SET_INFO_GT_ID = "COLOR_SET_INFO_GT_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_COLOR_INFO_DETAIL_GT_ID = "ColorInfoDetailGtId";
        public static readonly String PROPERTY_NAME_GRAPH_COLOR_NO = "GraphColorNo";
        public static readonly String PROPERTY_NAME_COLOR_CODE = "ColorCode";
        public static readonly String PROPERTY_NAME_PATTERN_CODE = "PatternCode";
        public static readonly String PROPERTY_NAME_COLOR_SET_INFO_GT_ID = "ColorSetInfoGtId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TColorSetInfoGt = "TColorSetInfoGt";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TColorInfoDetailGtDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_COLOR_INFO_DETAIL_GT_ID.ToLower(), PROPERTY_NAME_COLOR_INFO_DETAIL_GT_ID);
                map.put(DB_NAME_GRAPH_COLOR_NO.ToLower(), PROPERTY_NAME_GRAPH_COLOR_NO);
                map.put(DB_NAME_COLOR_CODE.ToLower(), PROPERTY_NAME_COLOR_CODE);
                map.put(DB_NAME_PATTERN_CODE.ToLower(), PROPERTY_NAME_PATTERN_CODE);
                map.put(DB_NAME_COLOR_SET_INFO_GT_ID.ToLower(), PROPERTY_NAME_COLOR_SET_INFO_GT_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_COLOR_INFO_DETAIL_GT_ID.ToLower(), DB_NAME_COLOR_INFO_DETAIL_GT_ID);
                map.put(PROPERTY_NAME_GRAPH_COLOR_NO.ToLower(), DB_NAME_GRAPH_COLOR_NO);
                map.put(PROPERTY_NAME_COLOR_CODE.ToLower(), DB_NAME_COLOR_CODE);
                map.put(PROPERTY_NAME_PATTERN_CODE.ToLower(), DB_NAME_PATTERN_CODE);
                map.put(PROPERTY_NAME_COLOR_SET_INFO_GT_ID.ToLower(), DB_NAME_COLOR_SET_INFO_GT_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TColorInfoDetailGt"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TColorInfoDetailGtDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TColorInfoDetailGtCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TColorInfoDetailGtBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TColorInfoDetailGt NewMyEntity() { return new TColorInfoDetailGt(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TColorInfoDetailGtCB NewMyConditionBean() { return new TColorInfoDetailGtCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TColorInfoDetailGt>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TColorInfoDetailGt>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("COLOR_INFO_DETAIL_GT_ID", "ColorInfoDetailGtId", new EntityPropertyColorInfoDetailGtIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_COLOR_NO", "GraphColorNo", new EntityPropertyGraphColorNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLOR_CODE", "ColorCode", new EntityPropertyColorCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PATTERN_CODE", "PatternCode", new EntityPropertyPatternCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COLOR_SET_INFO_GT_ID", "ColorSetInfoGtId", new EntityPropertyColorSetInfoGtIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TColorInfoDetailGt> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TColorInfoDetailGt)entity, value);
        }

        public class EntityPropertyColorInfoDetailGtIdSetupper : EntityPropertySetupper<TColorInfoDetailGt> {
            public void Setup(TColorInfoDetailGt entity, Object value) { entity.ColorInfoDetailGtId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyGraphColorNoSetupper : EntityPropertySetupper<TColorInfoDetailGt> {
            public void Setup(TColorInfoDetailGt entity, Object value) { entity.GraphColorNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyColorCodeSetupper : EntityPropertySetupper<TColorInfoDetailGt> {
            public void Setup(TColorInfoDetailGt entity, Object value) { entity.ColorCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPatternCodeSetupper : EntityPropertySetupper<TColorInfoDetailGt> {
            public void Setup(TColorInfoDetailGt entity, Object value) { entity.PatternCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyColorSetInfoGtIdSetupper : EntityPropertySetupper<TColorInfoDetailGt> {
            public void Setup(TColorInfoDetailGt entity, Object value) { entity.ColorSetInfoGtId = (value != null) ? (decimal?)value : null; }
        }
    }
}
