
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

    public class TOutputReportsetInfoDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputReportsetInfo);

        private static readonly TOutputReportsetInfoDbm _instance = new TOutputReportsetInfoDbm();
        private TOutputReportsetInfoDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputReportsetInfoDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_REPORTSET_INFO"; } }
        public override String TablePropertyName { get { return "TOutputReportsetInfo"; } }
        public override String TableSqlName { get { return "T_OUTPUT_REPORTSET_INFO"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnOutputReportsetInfoId;
        protected ColumnInfo _columnOutputFileTypeCode;
        protected ColumnInfo _columnReportFilenNamePrefix;
        protected ColumnInfo _columnCommentOutputFlag;
        protected ColumnInfo _columnPowerpointType;
        protected ColumnInfo _columnOutputTemplateId;

        public ColumnInfo ColumnOutputReportsetInfoId { get { return _columnOutputReportsetInfoId; } }
        public ColumnInfo ColumnOutputFileTypeCode { get { return _columnOutputFileTypeCode; } }
        public ColumnInfo ColumnReportFilenNamePrefix { get { return _columnReportFilenNamePrefix; } }
        public ColumnInfo ColumnCommentOutputFlag { get { return _columnCommentOutputFlag; } }
        public ColumnInfo ColumnPowerpointType { get { return _columnPowerpointType; } }
        public ColumnInfo ColumnOutputTemplateId { get { return _columnOutputTemplateId; } }

        protected void InitializeColumnInfo() {
            _columnOutputReportsetInfoId = cci("OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", null, null, true, "OutputReportsetInfoId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, "TOutputRequestList");
            _columnOutputFileTypeCode = cci("OUTPUT_FILE_TYPE_CODE", "OUTPUT_FILE_TYPE_CODE", null, null, true, "OutputFileTypeCode", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnReportFilenNamePrefix = cci("REPORT_FILEN_NAME_PREFIX", "REPORT_FILEN_NAME_PREFIX", null, null, false, "ReportFilenNamePrefix", typeof(String), false, "VARCHAR2", 100, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCommentOutputFlag = cci("COMMENT_OUTPUT_FLAG", "COMMENT_OUTPUT_FLAG", null, null, true, "CommentOutputFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPowerpointType = cci("POWERPOINT_TYPE", "POWERPOINT_TYPE", null, null, false, "PowerpointType", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputTemplateId = cci("OUTPUT_TEMPLATE_ID", "OUTPUT_TEMPLATE_ID", null, null, false, "OutputTemplateId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TOutputTemplate", null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnOutputReportsetInfoId);
            _columnInfoList.add(ColumnOutputFileTypeCode);
            _columnInfoList.add(ColumnReportFilenNamePrefix);
            _columnInfoList.add(ColumnCommentOutputFlag);
            _columnInfoList.add(ColumnPowerpointType);
            _columnInfoList.add(ColumnOutputTemplateId);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnOutputReportsetInfoId);
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
        public ForeignInfo ForeignTOutputTemplate { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputTemplateId, TOutputTemplateDbm.GetInstance().ColumnOutputTemplateId);
            return cfi("TOutputTemplate", this, TOutputTemplateDbm.GetInstance(), map, 0, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTOutputRequestList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnOutputReportsetInfoId, TOutputRequestDbm.GetInstance().ColumnOutputReportsetInfoId);
            return cri("TOutputRequestList", this, TOutputRequestDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Output_Reportset_Info_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Output_Reportset_Info_SEQ_01.nextval from dual"; } }
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_REPORTSET_INFO";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputReportsetInfo";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_OUTPUT_REPORTSET_INFO_ID = "OUTPUT_REPORTSET_INFO_ID";
        public static readonly String DB_NAME_OUTPUT_FILE_TYPE_CODE = "OUTPUT_FILE_TYPE_CODE";
        public static readonly String DB_NAME_REPORT_FILEN_NAME_PREFIX = "REPORT_FILEN_NAME_PREFIX";
        public static readonly String DB_NAME_COMMENT_OUTPUT_FLAG = "COMMENT_OUTPUT_FLAG";
        public static readonly String DB_NAME_POWERPOINT_TYPE = "POWERPOINT_TYPE";
        public static readonly String DB_NAME_OUTPUT_TEMPLATE_ID = "OUTPUT_TEMPLATE_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID = "OutputReportsetInfoId";
        public static readonly String PROPERTY_NAME_OUTPUT_FILE_TYPE_CODE = "OutputFileTypeCode";
        public static readonly String PROPERTY_NAME_REPORT_FILEN_NAME_PREFIX = "ReportFilenNamePrefix";
        public static readonly String PROPERTY_NAME_COMMENT_OUTPUT_FLAG = "CommentOutputFlag";
        public static readonly String PROPERTY_NAME_POWERPOINT_TYPE = "PowerpointType";
        public static readonly String PROPERTY_NAME_OUTPUT_TEMPLATE_ID = "OutputTemplateId";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TOutputTemplate = "TOutputTemplate";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TOutputRequestList = "TOutputRequestList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TOutputReportsetInfoDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_OUTPUT_REPORTSET_INFO_ID.ToLower(), PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID);
                map.put(DB_NAME_OUTPUT_FILE_TYPE_CODE.ToLower(), PROPERTY_NAME_OUTPUT_FILE_TYPE_CODE);
                map.put(DB_NAME_REPORT_FILEN_NAME_PREFIX.ToLower(), PROPERTY_NAME_REPORT_FILEN_NAME_PREFIX);
                map.put(DB_NAME_COMMENT_OUTPUT_FLAG.ToLower(), PROPERTY_NAME_COMMENT_OUTPUT_FLAG);
                map.put(DB_NAME_POWERPOINT_TYPE.ToLower(), PROPERTY_NAME_POWERPOINT_TYPE);
                map.put(DB_NAME_OUTPUT_TEMPLATE_ID.ToLower(), PROPERTY_NAME_OUTPUT_TEMPLATE_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_OUTPUT_REPORTSET_INFO_ID.ToLower(), DB_NAME_OUTPUT_REPORTSET_INFO_ID);
                map.put(PROPERTY_NAME_OUTPUT_FILE_TYPE_CODE.ToLower(), DB_NAME_OUTPUT_FILE_TYPE_CODE);
                map.put(PROPERTY_NAME_REPORT_FILEN_NAME_PREFIX.ToLower(), DB_NAME_REPORT_FILEN_NAME_PREFIX);
                map.put(PROPERTY_NAME_COMMENT_OUTPUT_FLAG.ToLower(), DB_NAME_COMMENT_OUTPUT_FLAG);
                map.put(PROPERTY_NAME_POWERPOINT_TYPE.ToLower(), DB_NAME_POWERPOINT_TYPE);
                map.put(PROPERTY_NAME_OUTPUT_TEMPLATE_ID.ToLower(), DB_NAME_OUTPUT_TEMPLATE_ID);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputReportsetInfo"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputReportsetInfoDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputReportsetInfoCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputReportsetInfoBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputReportsetInfo NewMyEntity() { return new TOutputReportsetInfo(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputReportsetInfoCB NewMyConditionBean() { return new TOutputReportsetInfoCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputReportsetInfo>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputReportsetInfo>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("OUTPUT_REPORTSET_INFO_ID", "OutputReportsetInfoId", new EntityPropertyOutputReportsetInfoIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_FILE_TYPE_CODE", "OutputFileTypeCode", new EntityPropertyOutputFileTypeCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REPORT_FILEN_NAME_PREFIX", "ReportFilenNamePrefix", new EntityPropertyReportFilenNamePrefixSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COMMENT_OUTPUT_FLAG", "CommentOutputFlag", new EntityPropertyCommentOutputFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("POWERPOINT_TYPE", "PowerpointType", new EntityPropertyPowerpointTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TEMPLATE_ID", "OutputTemplateId", new EntityPropertyOutputTemplateIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputReportsetInfo> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputReportsetInfo)entity, value);
        }

        public class EntityPropertyOutputReportsetInfoIdSetupper : EntityPropertySetupper<TOutputReportsetInfo> {
            public void Setup(TOutputReportsetInfo entity, Object value) { entity.OutputReportsetInfoId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyOutputFileTypeCodeSetupper : EntityPropertySetupper<TOutputReportsetInfo> {
            public void Setup(TOutputReportsetInfo entity, Object value) { entity.OutputFileTypeCode = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyReportFilenNamePrefixSetupper : EntityPropertySetupper<TOutputReportsetInfo> {
            public void Setup(TOutputReportsetInfo entity, Object value) { entity.ReportFilenNamePrefix = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCommentOutputFlagSetupper : EntityPropertySetupper<TOutputReportsetInfo> {
            public void Setup(TOutputReportsetInfo entity, Object value) { entity.CommentOutputFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPowerpointTypeSetupper : EntityPropertySetupper<TOutputReportsetInfo> {
            public void Setup(TOutputReportsetInfo entity, Object value) { entity.PowerpointType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputTemplateIdSetupper : EntityPropertySetupper<TOutputReportsetInfo> {
            public void Setup(TOutputReportsetInfo entity, Object value) { entity.OutputTemplateId = (value != null) ? (decimal?)value : null; }
        }
    }
}
