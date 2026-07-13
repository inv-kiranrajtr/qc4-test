
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputReportsetInfoCQ : AbstractBsTOutputReportsetInfoCQ {

        protected TOutputReportsetInfoCIQ _inlineQuery;

        public BsTOutputReportsetInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputReportsetInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputReportsetInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputReportsetInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputReportsetInfoCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _outputReportsetInfoId;
        public ConditionValue OutputReportsetInfoId {
            get { if (_outputReportsetInfoId == null) { _outputReportsetInfoId = new ConditionValue(); } return _outputReportsetInfoId; }
        }
        protected override ConditionValue getCValueOutputReportsetInfoId() { return this.OutputReportsetInfoId; }


        protected Map<String, TOutputRequestCQ> _outputReportsetInfoId_ExistsSubQuery_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> OutputReportsetInfoId_ExistsSubQuery_TOutputRequestList { get { return _outputReportsetInfoId_ExistsSubQuery_TOutputRequestListMap; }}
        public override String keepOutputReportsetInfoId_ExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_outputReportsetInfoId_ExistsSubQuery_TOutputRequestListMap == null) { _outputReportsetInfoId_ExistsSubQuery_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_outputReportsetInfoId_ExistsSubQuery_TOutputRequestListMap.size() + 1);
            _outputReportsetInfoId_ExistsSubQuery_TOutputRequestListMap.put(key, subQuery); return "OutputReportsetInfoId_ExistsSubQuery_TOutputRequestList." + key;
        }

        protected Map<String, TOutputRequestCQ> _outputReportsetInfoId_NotExistsSubQuery_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> OutputReportsetInfoId_NotExistsSubQuery_TOutputRequestList { get { return _outputReportsetInfoId_NotExistsSubQuery_TOutputRequestListMap; }}
        public override String keepOutputReportsetInfoId_NotExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_outputReportsetInfoId_NotExistsSubQuery_TOutputRequestListMap == null) { _outputReportsetInfoId_NotExistsSubQuery_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_outputReportsetInfoId_NotExistsSubQuery_TOutputRequestListMap.size() + 1);
            _outputReportsetInfoId_NotExistsSubQuery_TOutputRequestListMap.put(key, subQuery); return "OutputReportsetInfoId_NotExistsSubQuery_TOutputRequestList." + key;
        }

        protected Map<String, TOutputRequestCQ> _outputReportsetInfoId_InScopeSubQuery_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> OutputReportsetInfoId_InScopeSubQuery_TOutputRequestList { get { return _outputReportsetInfoId_InScopeSubQuery_TOutputRequestListMap; }}
        public override String keepOutputReportsetInfoId_InScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_outputReportsetInfoId_InScopeSubQuery_TOutputRequestListMap == null) { _outputReportsetInfoId_InScopeSubQuery_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_outputReportsetInfoId_InScopeSubQuery_TOutputRequestListMap.size() + 1);
            _outputReportsetInfoId_InScopeSubQuery_TOutputRequestListMap.put(key, subQuery); return "OutputReportsetInfoId_InScopeSubQuery_TOutputRequestList." + key;
        }

        protected Map<String, TOutputRequestCQ> _outputReportsetInfoId_NotInScopeSubQuery_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> OutputReportsetInfoId_NotInScopeSubQuery_TOutputRequestList { get { return _outputReportsetInfoId_NotInScopeSubQuery_TOutputRequestListMap; }}
        public override String keepOutputReportsetInfoId_NotInScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_outputReportsetInfoId_NotInScopeSubQuery_TOutputRequestListMap == null) { _outputReportsetInfoId_NotInScopeSubQuery_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_outputReportsetInfoId_NotInScopeSubQuery_TOutputRequestListMap.size() + 1);
            _outputReportsetInfoId_NotInScopeSubQuery_TOutputRequestListMap.put(key, subQuery); return "OutputReportsetInfoId_NotInScopeSubQuery_TOutputRequestList." + key;
        }

        protected Map<String, TOutputRequestCQ> _outputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> OutputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestList { get { return _outputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestListMap; }}
        public override String keepOutputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_outputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestListMap == null) { _outputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_outputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestListMap.size() + 1);
            _outputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestListMap.put(key, subQuery); return "OutputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestList." + key;
        }

        protected Map<String, TOutputRequestCQ> _outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> OutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestList { get { return _outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListMap; } }
        public override String keepOutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListMap == null) { _outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListMap.size() + 1);
            _outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListMap.put(key, subQuery); return "OutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestList." + key;
        }
        protected Map<String, Object> _outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameterMap;
        public Map<String, Object> OutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameter { get { return _outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameterMap; } }
        public override String keepOutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameter(Object parameterValue) {
            if (_outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameterMap == null) { _outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameterMap.size() + 1);
            _outputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameterMap.put(key, parameterValue); return "OutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameter." + key;
        }

        public BsTOutputReportsetInfoCQ AddOrderBy_OutputReportsetInfoId_Asc() { regOBA("OUTPUT_REPORTSET_INFO_ID");return this; }
        public BsTOutputReportsetInfoCQ AddOrderBy_OutputReportsetInfoId_Desc() { regOBD("OUTPUT_REPORTSET_INFO_ID");return this; }

        protected ConditionValue _outputFileTypeCode;
        public ConditionValue OutputFileTypeCode {
            get { if (_outputFileTypeCode == null) { _outputFileTypeCode = new ConditionValue(); } return _outputFileTypeCode; }
        }
        protected override ConditionValue getCValueOutputFileTypeCode() { return this.OutputFileTypeCode; }


        public BsTOutputReportsetInfoCQ AddOrderBy_OutputFileTypeCode_Asc() { regOBA("OUTPUT_FILE_TYPE_CODE");return this; }
        public BsTOutputReportsetInfoCQ AddOrderBy_OutputFileTypeCode_Desc() { regOBD("OUTPUT_FILE_TYPE_CODE");return this; }

        protected ConditionValue _reportFilenNamePrefix;
        public ConditionValue ReportFilenNamePrefix {
            get { if (_reportFilenNamePrefix == null) { _reportFilenNamePrefix = new ConditionValue(); } return _reportFilenNamePrefix; }
        }
        protected override ConditionValue getCValueReportFilenNamePrefix() { return this.ReportFilenNamePrefix; }


        public BsTOutputReportsetInfoCQ AddOrderBy_ReportFilenNamePrefix_Asc() { regOBA("REPORT_FILEN_NAME_PREFIX");return this; }
        public BsTOutputReportsetInfoCQ AddOrderBy_ReportFilenNamePrefix_Desc() { regOBD("REPORT_FILEN_NAME_PREFIX");return this; }

        protected ConditionValue _commentOutputFlag;
        public ConditionValue CommentOutputFlag {
            get { if (_commentOutputFlag == null) { _commentOutputFlag = new ConditionValue(); } return _commentOutputFlag; }
        }
        protected override ConditionValue getCValueCommentOutputFlag() { return this.CommentOutputFlag; }


        public BsTOutputReportsetInfoCQ AddOrderBy_CommentOutputFlag_Asc() { regOBA("COMMENT_OUTPUT_FLAG");return this; }
        public BsTOutputReportsetInfoCQ AddOrderBy_CommentOutputFlag_Desc() { regOBD("COMMENT_OUTPUT_FLAG");return this; }

        protected ConditionValue _powerpointType;
        public ConditionValue PowerpointType {
            get { if (_powerpointType == null) { _powerpointType = new ConditionValue(); } return _powerpointType; }
        }
        protected override ConditionValue getCValuePowerpointType() { return this.PowerpointType; }


        public BsTOutputReportsetInfoCQ AddOrderBy_PowerpointType_Asc() { regOBA("POWERPOINT_TYPE");return this; }
        public BsTOutputReportsetInfoCQ AddOrderBy_PowerpointType_Desc() { regOBD("POWERPOINT_TYPE");return this; }

        protected ConditionValue _outputTemplateId;
        public ConditionValue OutputTemplateId {
            get { if (_outputTemplateId == null) { _outputTemplateId = new ConditionValue(); } return _outputTemplateId; }
        }
        protected override ConditionValue getCValueOutputTemplateId() { return this.OutputTemplateId; }


        protected Map<String, TOutputTemplateCQ> _outputTemplateId_InScopeSubQuery_TOutputTemplateMap;
        public Map<String, TOutputTemplateCQ> OutputTemplateId_InScopeSubQuery_TOutputTemplate { get { return _outputTemplateId_InScopeSubQuery_TOutputTemplateMap; }}
        public override String keepOutputTemplateId_InScopeSubQuery_TOutputTemplate(TOutputTemplateCQ subQuery) {
            if (_outputTemplateId_InScopeSubQuery_TOutputTemplateMap == null) { _outputTemplateId_InScopeSubQuery_TOutputTemplateMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateId_InScopeSubQuery_TOutputTemplateMap.size() + 1);
            _outputTemplateId_InScopeSubQuery_TOutputTemplateMap.put(key, subQuery); return "OutputTemplateId_InScopeSubQuery_TOutputTemplate." + key;
        }

        protected Map<String, TOutputTemplateCQ> _outputTemplateId_NotInScopeSubQuery_TOutputTemplateMap;
        public Map<String, TOutputTemplateCQ> OutputTemplateId_NotInScopeSubQuery_TOutputTemplate { get { return _outputTemplateId_NotInScopeSubQuery_TOutputTemplateMap; }}
        public override String keepOutputTemplateId_NotInScopeSubQuery_TOutputTemplate(TOutputTemplateCQ subQuery) {
            if (_outputTemplateId_NotInScopeSubQuery_TOutputTemplateMap == null) { _outputTemplateId_NotInScopeSubQuery_TOutputTemplateMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateId_NotInScopeSubQuery_TOutputTemplateMap.size() + 1);
            _outputTemplateId_NotInScopeSubQuery_TOutputTemplateMap.put(key, subQuery); return "OutputTemplateId_NotInScopeSubQuery_TOutputTemplate." + key;
        }

        public BsTOutputReportsetInfoCQ AddOrderBy_OutputTemplateId_Asc() { regOBA("OUTPUT_TEMPLATE_ID");return this; }
        public BsTOutputReportsetInfoCQ AddOrderBy_OutputTemplateId_Desc() { regOBD("OUTPUT_TEMPLATE_ID");return this; }

        public BsTOutputReportsetInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputReportsetInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputReportsetInfoCQ baseQuery = (TOutputReportsetInfoCQ)baseQueryAsSuper;
            TOutputReportsetInfoCQ unionQuery = (TOutputReportsetInfoCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTOutputTemplate()) {
                unionQuery.QueryTOutputTemplate().reflectRelationOnUnionQuery(baseQuery.QueryTOutputTemplate(), unionQuery.QueryTOutputTemplate());
            }

        }
    
        protected TOutputTemplateCQ _conditionQueryTOutputTemplate;
        public TOutputTemplateCQ QueryTOutputTemplate() {
            return this.ConditionQueryTOutputTemplate;
        }
        public TOutputTemplateCQ ConditionQueryTOutputTemplate {
            get {
                if (_conditionQueryTOutputTemplate == null) {
                    _conditionQueryTOutputTemplate = xcreateQueryTOutputTemplate();
                    xsetupOuterJoin_TOutputTemplate();
                }
                return _conditionQueryTOutputTemplate;
            }
        }
        protected TOutputTemplateCQ xcreateQueryTOutputTemplate() {
            String nrp = resolveNextRelationPathTOutputTemplate();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputTemplateCQ cq = new TOutputTemplateCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputTemplate"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputTemplate() {
            TOutputTemplateCQ cq = ConditionQueryTOutputTemplate;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("OUTPUT_TEMPLATE_ID", "Output_Template_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputTemplate() {
            return resolveNextRelationPath("T_OUTPUT_REPORTSET_INFO", "tOutputTemplate");
        }
        public bool hasConditionQueryTOutputTemplate() {
            return _conditionQueryTOutputTemplate != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputReportsetInfoCQ> _scalarSubQueryMap;
	    public Map<String, TOutputReportsetInfoCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputReportsetInfoCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputReportsetInfoCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputReportsetInfoCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputReportsetInfoCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputReportsetInfoCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputReportsetInfoCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
