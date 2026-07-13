
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputSettingReportCQ : AbstractBsTOutputSettingReportCQ {

        protected TOutputSettingReportCIQ _inlineQuery;

        public BsTOutputSettingReportCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputSettingReportCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputSettingReportCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputSettingReportCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputSettingReportCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        public BsTOutputSettingReportCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTOutputSettingReportCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _fileTypeExcelFlag;
        public ConditionValue FileTypeExcelFlag {
            get { if (_fileTypeExcelFlag == null) { _fileTypeExcelFlag = new ConditionValue(); } return _fileTypeExcelFlag; }
        }
        protected override ConditionValue getCValueFileTypeExcelFlag() { return this.FileTypeExcelFlag; }


        public BsTOutputSettingReportCQ AddOrderBy_FileTypeExcelFlag_Asc() { regOBA("FILE_TYPE_EXCEL_FLAG");return this; }
        public BsTOutputSettingReportCQ AddOrderBy_FileTypeExcelFlag_Desc() { regOBD("FILE_TYPE_EXCEL_FLAG");return this; }

        protected ConditionValue _fileTypePpFlag;
        public ConditionValue FileTypePpFlag {
            get { if (_fileTypePpFlag == null) { _fileTypePpFlag = new ConditionValue(); } return _fileTypePpFlag; }
        }
        protected override ConditionValue getCValueFileTypePpFlag() { return this.FileTypePpFlag; }


        public BsTOutputSettingReportCQ AddOrderBy_FileTypePpFlag_Asc() { regOBA("FILE_TYPE_PP_FLAG");return this; }
        public BsTOutputSettingReportCQ AddOrderBy_FileTypePpFlag_Desc() { regOBD("FILE_TYPE_PP_FLAG");return this; }

        protected ConditionValue _fileTypePdfFlag;
        public ConditionValue FileTypePdfFlag {
            get { if (_fileTypePdfFlag == null) { _fileTypePdfFlag = new ConditionValue(); } return _fileTypePdfFlag; }
        }
        protected override ConditionValue getCValueFileTypePdfFlag() { return this.FileTypePdfFlag; }


        public BsTOutputSettingReportCQ AddOrderBy_FileTypePdfFlag_Asc() { regOBA("FILE_TYPE_PDF_FLAG");return this; }
        public BsTOutputSettingReportCQ AddOrderBy_FileTypePdfFlag_Desc() { regOBD("FILE_TYPE_PDF_FLAG");return this; }

        protected ConditionValue _reportType;
        public ConditionValue ReportType {
            get { if (_reportType == null) { _reportType = new ConditionValue(); } return _reportType; }
        }
        protected override ConditionValue getCValueReportType() { return this.ReportType; }


        public BsTOutputSettingReportCQ AddOrderBy_ReportType_Asc() { regOBA("REPORT_TYPE");return this; }
        public BsTOutputSettingReportCQ AddOrderBy_ReportType_Desc() { regOBD("REPORT_TYPE");return this; }

        protected ConditionValue _graphOutputFlag;
        public ConditionValue GraphOutputFlag {
            get { if (_graphOutputFlag == null) { _graphOutputFlag = new ConditionValue(); } return _graphOutputFlag; }
        }
        protected override ConditionValue getCValueGraphOutputFlag() { return this.GraphOutputFlag; }


        public BsTOutputSettingReportCQ AddOrderBy_GraphOutputFlag_Asc() { regOBA("GRAPH_OUTPUT_FLAG");return this; }
        public BsTOutputSettingReportCQ AddOrderBy_GraphOutputFlag_Desc() { regOBD("GRAPH_OUTPUT_FLAG");return this; }

        protected ConditionValue _ascFlag;
        public ConditionValue AscFlag {
            get { if (_ascFlag == null) { _ascFlag = new ConditionValue(); } return _ascFlag; }
        }
        protected override ConditionValue getCValueAscFlag() { return this.AscFlag; }


        public BsTOutputSettingReportCQ AddOrderBy_AscFlag_Asc() { regOBA("ASC_FLAG");return this; }
        public BsTOutputSettingReportCQ AddOrderBy_AscFlag_Desc() { regOBD("ASC_FLAG");return this; }

        protected ConditionValue _commentVisibleFlag;
        public ConditionValue CommentVisibleFlag {
            get { if (_commentVisibleFlag == null) { _commentVisibleFlag = new ConditionValue(); } return _commentVisibleFlag; }
        }
        protected override ConditionValue getCValueCommentVisibleFlag() { return this.CommentVisibleFlag; }


        public BsTOutputSettingReportCQ AddOrderBy_CommentVisibleFlag_Asc() { regOBA("COMMENT_VISIBLE_FLAG");return this; }
        public BsTOutputSettingReportCQ AddOrderBy_CommentVisibleFlag_Desc() { regOBD("COMMENT_VISIBLE_FLAG");return this; }

        protected ConditionValue _surveyReportFlag;
        public ConditionValue SurveyReportFlag {
            get { if (_surveyReportFlag == null) { _surveyReportFlag = new ConditionValue(); } return _surveyReportFlag; }
        }
        protected override ConditionValue getCValueSurveyReportFlag() { return this.SurveyReportFlag; }


        public BsTOutputSettingReportCQ AddOrderBy_SurveyReportFlag_Asc() { regOBA("SURVEY_REPORT_FLAG");return this; }
        public BsTOutputSettingReportCQ AddOrderBy_SurveyReportFlag_Desc() { regOBD("SURVEY_REPORT_FLAG");return this; }

        protected ConditionValue _outputTemplateId;
        public ConditionValue OutputTemplateId {
            get { if (_outputTemplateId == null) { _outputTemplateId = new ConditionValue(); } return _outputTemplateId; }
        }
        protected override ConditionValue getCValueOutputTemplateId() { return this.OutputTemplateId; }


        public BsTOutputSettingReportCQ AddOrderBy_OutputTemplateId_Asc() { regOBA("OUTPUT_TEMPLATE_ID");return this; }
        public BsTOutputSettingReportCQ AddOrderBy_OutputTemplateId_Desc() { regOBD("OUTPUT_TEMPLATE_ID");return this; }

        public BsTOutputSettingReportCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputSettingReportCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputSettingReportCQ baseQuery = (TOutputSettingReportCQ)baseQueryAsSuper;
            TOutputSettingReportCQ unionQuery = (TOutputSettingReportCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
            }
            if (baseQuery.hasConditionQueryTQcwebSurveyInfoAsOne()) {
                unionQuery.QueryTQcwebSurveyInfoAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfoAsOne(), unionQuery.QueryTQcwebSurveyInfoAsOne());
            }

        }
    
        protected TQcwebSurveyInfoCQ _conditionQueryTQcwebSurveyInfo;
        public TQcwebSurveyInfoCQ QueryTQcwebSurveyInfo() {
            return this.ConditionQueryTQcwebSurveyInfo;
        }
        public TQcwebSurveyInfoCQ ConditionQueryTQcwebSurveyInfo {
            get {
                if (_conditionQueryTQcwebSurveyInfo == null) {
                    _conditionQueryTQcwebSurveyInfo = xcreateQueryTQcwebSurveyInfo();
                    xsetupOuterJoin_TQcwebSurveyInfo();
                }
                return _conditionQueryTQcwebSurveyInfo;
            }
        }
        protected TQcwebSurveyInfoCQ xcreateQueryTQcwebSurveyInfo() {
            String nrp = resolveNextRelationPathTQcwebSurveyInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TQcwebSurveyInfoCQ cq = new TQcwebSurveyInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tQcwebSurveyInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TQcwebSurveyInfo() {
            TQcwebSurveyInfoCQ cq = ConditionQueryTQcwebSurveyInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTQcwebSurveyInfo() {
            return resolveNextRelationPath("T_OUTPUT_SETTING_REPORT", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }


        protected TQcwebSurveyInfoCQ _conditionQueryTQcwebSurveyInfoAsOne;
        public TQcwebSurveyInfoCQ ConditionQueryTQcwebSurveyInfoAsOne {
            get {
                if (_conditionQueryTQcwebSurveyInfoAsOne == null) {
                    _conditionQueryTQcwebSurveyInfoAsOne = createQueryTQcwebSurveyInfoAsOne();
                    xsetupOuterJoin_TQcwebSurveyInfoAsOne();
                }
                return _conditionQueryTQcwebSurveyInfoAsOne;
            }
        }
        public TQcwebSurveyInfoCQ QueryTQcwebSurveyInfoAsOne() { return this.ConditionQueryTQcwebSurveyInfoAsOne; }
        protected TQcwebSurveyInfoCQ createQueryTQcwebSurveyInfoAsOne() {
            String nrp = resolveNextRelationPathTQcwebSurveyInfoAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TQcwebSurveyInfoCQ cq = new TQcwebSurveyInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tQcwebSurveyInfoAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TQcwebSurveyInfoAsOne() {
            TQcwebSurveyInfoCQ cq = ConditionQueryTQcwebSurveyInfoAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTQcwebSurveyInfoAsOne() {
            return resolveNextRelationPath("T_OUTPUT_SETTING_REPORT", "tQcwebSurveyInfoAsOne");
        }
        public bool hasConditionQueryTQcwebSurveyInfoAsOne() {
            return _conditionQueryTQcwebSurveyInfoAsOne != null;
        }

	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputSettingReportCQ> _scalarSubQueryMap;
	    public Map<String, TOutputSettingReportCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputSettingReportCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputSettingReportCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputSettingReportCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputSettingReportCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputSettingReportCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputSettingReportCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
