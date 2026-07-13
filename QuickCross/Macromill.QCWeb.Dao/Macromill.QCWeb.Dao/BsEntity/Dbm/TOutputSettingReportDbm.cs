
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

    public class TOutputSettingReportDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TOutputSettingReport);

        private static readonly TOutputSettingReportDbm _instance = new TOutputSettingReportDbm();
        private TOutputSettingReportDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TOutputSettingReportDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SETTING_REPORT"; } }
        public override String TablePropertyName { get { return "TOutputSettingReport"; } }
        public override String TableSqlName { get { return "T_OUTPUT_SETTING_REPORT"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnFileTypeExcelFlag;
        protected ColumnInfo _columnFileTypePpFlag;
        protected ColumnInfo _columnFileTypePdfFlag;
        protected ColumnInfo _columnReportType;
        protected ColumnInfo _columnGraphOutputFlag;
        protected ColumnInfo _columnAscFlag;
        protected ColumnInfo _columnCommentVisibleFlag;
        protected ColumnInfo _columnSurveyReportFlag;
        protected ColumnInfo _columnOutputTemplateId;

        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnFileTypeExcelFlag { get { return _columnFileTypeExcelFlag; } }
        public ColumnInfo ColumnFileTypePpFlag { get { return _columnFileTypePpFlag; } }
        public ColumnInfo ColumnFileTypePdfFlag { get { return _columnFileTypePdfFlag; } }
        public ColumnInfo ColumnReportType { get { return _columnReportType; } }
        public ColumnInfo ColumnGraphOutputFlag { get { return _columnGraphOutputFlag; } }
        public ColumnInfo ColumnAscFlag { get { return _columnAscFlag; } }
        public ColumnInfo ColumnCommentVisibleFlag { get { return _columnCommentVisibleFlag; } }
        public ColumnInfo ColumnSurveyReportFlag { get { return _columnSurveyReportFlag; } }
        public ColumnInfo ColumnOutputTemplateId { get { return _columnOutputTemplateId; } }

        protected void InitializeColumnInfo() {
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo,TQcwebSurveyInfoAsOne", "");
            _columnFileTypeExcelFlag = cci("FILE_TYPE_EXCEL_FLAG", "FILE_TYPE_EXCEL_FLAG", null, null, true, "FileTypeExcelFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFileTypePpFlag = cci("FILE_TYPE_PP_FLAG", "FILE_TYPE_PP_FLAG", null, null, true, "FileTypePpFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFileTypePdfFlag = cci("FILE_TYPE_PDF_FLAG", "FILE_TYPE_PDF_FLAG", null, null, true, "FileTypePdfFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnReportType = cci("REPORT_TYPE", "REPORT_TYPE", null, null, true, "ReportType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphOutputFlag = cci("GRAPH_OUTPUT_FLAG", "GRAPH_OUTPUT_FLAG", null, null, true, "GraphOutputFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAscFlag = cci("ASC_FLAG", "ASC_FLAG", null, null, true, "AscFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCommentVisibleFlag = cci("COMMENT_VISIBLE_FLAG", "COMMENT_VISIBLE_FLAG", null, null, true, "CommentVisibleFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSurveyReportFlag = cci("SURVEY_REPORT_FLAG", "SURVEY_REPORT_FLAG", null, null, true, "SurveyReportFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnOutputTemplateId = cci("OUTPUT_TEMPLATE_ID", "OUTPUT_TEMPLATE_ID", null, null, false, "OutputTemplateId", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnFileTypeExcelFlag);
            _columnInfoList.add(ColumnFileTypePpFlag);
            _columnInfoList.add(ColumnFileTypePdfFlag);
            _columnInfoList.add(ColumnReportType);
            _columnInfoList.add(ColumnGraphOutputFlag);
            _columnInfoList.add(ColumnAscFlag);
            _columnInfoList.add(ColumnCommentVisibleFlag);
            _columnInfoList.add(ColumnSurveyReportFlag);
            _columnInfoList.add(ColumnOutputTemplateId);
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
        public ForeignInfo ForeignTQcwebSurveyInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfo", this, TQcwebSurveyInfoDbm.GetInstance(), map, 0, true, false);
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
        public static readonly String TABLE_DB_NAME = "T_OUTPUT_SETTING_REPORT";
        public static readonly String TABLE_PROPERTY_NAME = "TOutputSettingReport";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_FILE_TYPE_EXCEL_FLAG = "FILE_TYPE_EXCEL_FLAG";
        public static readonly String DB_NAME_FILE_TYPE_PP_FLAG = "FILE_TYPE_PP_FLAG";
        public static readonly String DB_NAME_FILE_TYPE_PDF_FLAG = "FILE_TYPE_PDF_FLAG";
        public static readonly String DB_NAME_REPORT_TYPE = "REPORT_TYPE";
        public static readonly String DB_NAME_GRAPH_OUTPUT_FLAG = "GRAPH_OUTPUT_FLAG";
        public static readonly String DB_NAME_ASC_FLAG = "ASC_FLAG";
        public static readonly String DB_NAME_COMMENT_VISIBLE_FLAG = "COMMENT_VISIBLE_FLAG";
        public static readonly String DB_NAME_SURVEY_REPORT_FLAG = "SURVEY_REPORT_FLAG";
        public static readonly String DB_NAME_OUTPUT_TEMPLATE_ID = "OUTPUT_TEMPLATE_ID";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_FILE_TYPE_EXCEL_FLAG = "FileTypeExcelFlag";
        public static readonly String PROPERTY_NAME_FILE_TYPE_PP_FLAG = "FileTypePpFlag";
        public static readonly String PROPERTY_NAME_FILE_TYPE_PDF_FLAG = "FileTypePdfFlag";
        public static readonly String PROPERTY_NAME_REPORT_TYPE = "ReportType";
        public static readonly String PROPERTY_NAME_GRAPH_OUTPUT_FLAG = "GraphOutputFlag";
        public static readonly String PROPERTY_NAME_ASC_FLAG = "AscFlag";
        public static readonly String PROPERTY_NAME_COMMENT_VISIBLE_FLAG = "CommentVisibleFlag";
        public static readonly String PROPERTY_NAME_SURVEY_REPORT_FLAG = "SurveyReportFlag";
        public static readonly String PROPERTY_NAME_OUTPUT_TEMPLATE_ID = "OutputTemplateId";

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

        static TOutputSettingReportDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_FILE_TYPE_EXCEL_FLAG.ToLower(), PROPERTY_NAME_FILE_TYPE_EXCEL_FLAG);
                map.put(DB_NAME_FILE_TYPE_PP_FLAG.ToLower(), PROPERTY_NAME_FILE_TYPE_PP_FLAG);
                map.put(DB_NAME_FILE_TYPE_PDF_FLAG.ToLower(), PROPERTY_NAME_FILE_TYPE_PDF_FLAG);
                map.put(DB_NAME_REPORT_TYPE.ToLower(), PROPERTY_NAME_REPORT_TYPE);
                map.put(DB_NAME_GRAPH_OUTPUT_FLAG.ToLower(), PROPERTY_NAME_GRAPH_OUTPUT_FLAG);
                map.put(DB_NAME_ASC_FLAG.ToLower(), PROPERTY_NAME_ASC_FLAG);
                map.put(DB_NAME_COMMENT_VISIBLE_FLAG.ToLower(), PROPERTY_NAME_COMMENT_VISIBLE_FLAG);
                map.put(DB_NAME_SURVEY_REPORT_FLAG.ToLower(), PROPERTY_NAME_SURVEY_REPORT_FLAG);
                map.put(DB_NAME_OUTPUT_TEMPLATE_ID.ToLower(), PROPERTY_NAME_OUTPUT_TEMPLATE_ID);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_FILE_TYPE_EXCEL_FLAG.ToLower(), DB_NAME_FILE_TYPE_EXCEL_FLAG);
                map.put(PROPERTY_NAME_FILE_TYPE_PP_FLAG.ToLower(), DB_NAME_FILE_TYPE_PP_FLAG);
                map.put(PROPERTY_NAME_FILE_TYPE_PDF_FLAG.ToLower(), DB_NAME_FILE_TYPE_PDF_FLAG);
                map.put(PROPERTY_NAME_REPORT_TYPE.ToLower(), DB_NAME_REPORT_TYPE);
                map.put(PROPERTY_NAME_GRAPH_OUTPUT_FLAG.ToLower(), DB_NAME_GRAPH_OUTPUT_FLAG);
                map.put(PROPERTY_NAME_ASC_FLAG.ToLower(), DB_NAME_ASC_FLAG);
                map.put(PROPERTY_NAME_COMMENT_VISIBLE_FLAG.ToLower(), DB_NAME_COMMENT_VISIBLE_FLAG);
                map.put(PROPERTY_NAME_SURVEY_REPORT_FLAG.ToLower(), DB_NAME_SURVEY_REPORT_FLAG);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TOutputSettingReport"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TOutputSettingReportDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TOutputSettingReportCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TOutputSettingReportBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TOutputSettingReport NewMyEntity() { return new TOutputSettingReport(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TOutputSettingReportCB NewMyConditionBean() { return new TOutputSettingReportCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TOutputSettingReport>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TOutputSettingReport>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FILE_TYPE_EXCEL_FLAG", "FileTypeExcelFlag", new EntityPropertyFileTypeExcelFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FILE_TYPE_PP_FLAG", "FileTypePpFlag", new EntityPropertyFileTypePpFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FILE_TYPE_PDF_FLAG", "FileTypePdfFlag", new EntityPropertyFileTypePdfFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("REPORT_TYPE", "ReportType", new EntityPropertyReportTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_OUTPUT_FLAG", "GraphOutputFlag", new EntityPropertyGraphOutputFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ASC_FLAG", "AscFlag", new EntityPropertyAscFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("COMMENT_VISIBLE_FLAG", "CommentVisibleFlag", new EntityPropertyCommentVisibleFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SURVEY_REPORT_FLAG", "SurveyReportFlag", new EntityPropertySurveyReportFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("OUTPUT_TEMPLATE_ID", "OutputTemplateId", new EntityPropertyOutputTemplateIdSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TOutputSettingReport> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TOutputSettingReport)entity, value);
        }

        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TOutputSettingReport> {
            public void Setup(TOutputSettingReport entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyFileTypeExcelFlagSetupper : EntityPropertySetupper<TOutputSettingReport> {
            public void Setup(TOutputSettingReport entity, Object value) { entity.FileTypeExcelFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFileTypePpFlagSetupper : EntityPropertySetupper<TOutputSettingReport> {
            public void Setup(TOutputSettingReport entity, Object value) { entity.FileTypePpFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFileTypePdfFlagSetupper : EntityPropertySetupper<TOutputSettingReport> {
            public void Setup(TOutputSettingReport entity, Object value) { entity.FileTypePdfFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyReportTypeSetupper : EntityPropertySetupper<TOutputSettingReport> {
            public void Setup(TOutputSettingReport entity, Object value) { entity.ReportType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyGraphOutputFlagSetupper : EntityPropertySetupper<TOutputSettingReport> {
            public void Setup(TOutputSettingReport entity, Object value) { entity.GraphOutputFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyAscFlagSetupper : EntityPropertySetupper<TOutputSettingReport> {
            public void Setup(TOutputSettingReport entity, Object value) { entity.AscFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyCommentVisibleFlagSetupper : EntityPropertySetupper<TOutputSettingReport> {
            public void Setup(TOutputSettingReport entity, Object value) { entity.CommentVisibleFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySurveyReportFlagSetupper : EntityPropertySetupper<TOutputSettingReport> {
            public void Setup(TOutputSettingReport entity, Object value) { entity.SurveyReportFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyOutputTemplateIdSetupper : EntityPropertySetupper<TOutputSettingReport> {
            public void Setup(TOutputSettingReport entity, Object value) { entity.OutputTemplateId = (value != null) ? (decimal?)value : null; }
        }
    }
}
