
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputTemplateCQ : AbstractBsTOutputTemplateCQ {

        protected TOutputTemplateCIQ _inlineQuery;

        public BsTOutputTemplateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputTemplateCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputTemplateCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputTemplateCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputTemplateCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _outputTemplateId;
        public ConditionValue OutputTemplateId {
            get { if (_outputTemplateId == null) { _outputTemplateId = new ConditionValue(); } return _outputTemplateId; }
        }
        protected override ConditionValue getCValueOutputTemplateId() { return this.OutputTemplateId; }


        protected Map<String, TOutputReportsetInfoCQ> _outputTemplateId_ExistsSubQuery_TOutputReportsetInfoListMap;
        public Map<String, TOutputReportsetInfoCQ> OutputTemplateId_ExistsSubQuery_TOutputReportsetInfoList { get { return _outputTemplateId_ExistsSubQuery_TOutputReportsetInfoListMap; }}
        public override String keepOutputTemplateId_ExistsSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            if (_outputTemplateId_ExistsSubQuery_TOutputReportsetInfoListMap == null) { _outputTemplateId_ExistsSubQuery_TOutputReportsetInfoListMap = new LinkedHashMap<String, TOutputReportsetInfoCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateId_ExistsSubQuery_TOutputReportsetInfoListMap.size() + 1);
            _outputTemplateId_ExistsSubQuery_TOutputReportsetInfoListMap.put(key, subQuery); return "OutputTemplateId_ExistsSubQuery_TOutputReportsetInfoList." + key;
        }

        protected Map<String, TOutputReportsetInfoCQ> _outputTemplateId_NotExistsSubQuery_TOutputReportsetInfoListMap;
        public Map<String, TOutputReportsetInfoCQ> OutputTemplateId_NotExistsSubQuery_TOutputReportsetInfoList { get { return _outputTemplateId_NotExistsSubQuery_TOutputReportsetInfoListMap; }}
        public override String keepOutputTemplateId_NotExistsSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            if (_outputTemplateId_NotExistsSubQuery_TOutputReportsetInfoListMap == null) { _outputTemplateId_NotExistsSubQuery_TOutputReportsetInfoListMap = new LinkedHashMap<String, TOutputReportsetInfoCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateId_NotExistsSubQuery_TOutputReportsetInfoListMap.size() + 1);
            _outputTemplateId_NotExistsSubQuery_TOutputReportsetInfoListMap.put(key, subQuery); return "OutputTemplateId_NotExistsSubQuery_TOutputReportsetInfoList." + key;
        }

        protected Map<String, TOutputReportsetInfoCQ> _outputTemplateId_InScopeSubQuery_TOutputReportsetInfoListMap;
        public Map<String, TOutputReportsetInfoCQ> OutputTemplateId_InScopeSubQuery_TOutputReportsetInfoList { get { return _outputTemplateId_InScopeSubQuery_TOutputReportsetInfoListMap; }}
        public override String keepOutputTemplateId_InScopeSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            if (_outputTemplateId_InScopeSubQuery_TOutputReportsetInfoListMap == null) { _outputTemplateId_InScopeSubQuery_TOutputReportsetInfoListMap = new LinkedHashMap<String, TOutputReportsetInfoCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateId_InScopeSubQuery_TOutputReportsetInfoListMap.size() + 1);
            _outputTemplateId_InScopeSubQuery_TOutputReportsetInfoListMap.put(key, subQuery); return "OutputTemplateId_InScopeSubQuery_TOutputReportsetInfoList." + key;
        }

        protected Map<String, TOutputReportsetInfoCQ> _outputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoListMap;
        public Map<String, TOutputReportsetInfoCQ> OutputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoList { get { return _outputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoListMap; }}
        public override String keepOutputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            if (_outputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoListMap == null) { _outputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoListMap = new LinkedHashMap<String, TOutputReportsetInfoCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoListMap.size() + 1);
            _outputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoListMap.put(key, subQuery); return "OutputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoList." + key;
        }

        protected Map<String, TOutputReportsetInfoCQ> _outputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoListMap;
        public Map<String, TOutputReportsetInfoCQ> OutputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoList { get { return _outputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoListMap; }}
        public override String keepOutputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            if (_outputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoListMap == null) { _outputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoListMap = new LinkedHashMap<String, TOutputReportsetInfoCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoListMap.size() + 1);
            _outputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoListMap.put(key, subQuery); return "OutputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoList." + key;
        }

        protected Map<String, TOutputReportsetInfoCQ> _outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListMap;
        public Map<String, TOutputReportsetInfoCQ> OutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoList { get { return _outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListMap; } }
        public override String keepOutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            if (_outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListMap == null) { _outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListMap = new LinkedHashMap<String, TOutputReportsetInfoCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListMap.size() + 1);
            _outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListMap.put(key, subQuery); return "OutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoList." + key;
        }
        protected Map<String, Object> _outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameterMap;
        public Map<String, Object> OutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameter { get { return _outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameterMap; } }
        public override String keepOutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameter(Object parameterValue) {
            if (_outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameterMap == null) { _outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameterMap.size() + 1);
            _outputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameterMap.put(key, parameterValue); return "OutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameter." + key;
        }

        public BsTOutputTemplateCQ AddOrderBy_OutputTemplateId_Asc() { regOBA("OUTPUT_TEMPLATE_ID");return this; }
        public BsTOutputTemplateCQ AddOrderBy_OutputTemplateId_Desc() { regOBD("OUTPUT_TEMPLATE_ID");return this; }

        protected ConditionValue _outputTemplateMasterId;
        public ConditionValue OutputTemplateMasterId {
            get { if (_outputTemplateMasterId == null) { _outputTemplateMasterId = new ConditionValue(); } return _outputTemplateMasterId; }
        }
        protected override ConditionValue getCValueOutputTemplateMasterId() { return this.OutputTemplateMasterId; }


        protected Map<String, TOutputTemplateMasterCQ> _outputTemplateMasterId_InScopeSubQuery_TOutputTemplateMasterMap;
        public Map<String, TOutputTemplateMasterCQ> OutputTemplateMasterId_InScopeSubQuery_TOutputTemplateMaster { get { return _outputTemplateMasterId_InScopeSubQuery_TOutputTemplateMasterMap; }}
        public override String keepOutputTemplateMasterId_InScopeSubQuery_TOutputTemplateMaster(TOutputTemplateMasterCQ subQuery) {
            if (_outputTemplateMasterId_InScopeSubQuery_TOutputTemplateMasterMap == null) { _outputTemplateMasterId_InScopeSubQuery_TOutputTemplateMasterMap = new LinkedHashMap<String, TOutputTemplateMasterCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateMasterId_InScopeSubQuery_TOutputTemplateMasterMap.size() + 1);
            _outputTemplateMasterId_InScopeSubQuery_TOutputTemplateMasterMap.put(key, subQuery); return "OutputTemplateMasterId_InScopeSubQuery_TOutputTemplateMaster." + key;
        }

        protected Map<String, TOutputTemplateMasterCQ> _outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMasterMap;
        public Map<String, TOutputTemplateMasterCQ> OutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMaster { get { return _outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMasterMap; }}
        public override String keepOutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMaster(TOutputTemplateMasterCQ subQuery) {
            if (_outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMasterMap == null) { _outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMasterMap = new LinkedHashMap<String, TOutputTemplateMasterCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMasterMap.size() + 1);
            _outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMasterMap.put(key, subQuery); return "OutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMaster." + key;
        }

        public BsTOutputTemplateCQ AddOrderBy_OutputTemplateMasterId_Asc() { regOBA("OUTPUT_TEMPLATE_MASTER_ID");return this; }
        public BsTOutputTemplateCQ AddOrderBy_OutputTemplateMasterId_Desc() { regOBD("OUTPUT_TEMPLATE_MASTER_ID");return this; }

        protected ConditionValue _uploadPath;
        public ConditionValue UploadPath {
            get { if (_uploadPath == null) { _uploadPath = new ConditionValue(); } return _uploadPath; }
        }
        protected override ConditionValue getCValueUploadPath() { return this.UploadPath; }


        public BsTOutputTemplateCQ AddOrderBy_UploadPath_Asc() { regOBA("UPLOAD_PATH");return this; }
        public BsTOutputTemplateCQ AddOrderBy_UploadPath_Desc() { regOBD("UPLOAD_PATH");return this; }

        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        public BsTOutputTemplateCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTOutputTemplateCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _alias;
        public ConditionValue Alias {
            get { if (_alias == null) { _alias = new ConditionValue(); } return _alias; }
        }
        protected override ConditionValue getCValueAlias() { return this.Alias; }


        public BsTOutputTemplateCQ AddOrderBy_Alias_Asc() { regOBA("ALIAS");return this; }
        public BsTOutputTemplateCQ AddOrderBy_Alias_Desc() { regOBD("ALIAS");return this; }

        protected ConditionValue _createDatetime;
        public ConditionValue CreateDatetime {
            get { if (_createDatetime == null) { _createDatetime = new ConditionValue(); } return _createDatetime; }
        }
        protected override ConditionValue getCValueCreateDatetime() { return this.CreateDatetime; }


        public BsTOutputTemplateCQ AddOrderBy_CreateDatetime_Asc() { regOBA("CREATE_DATETIME");return this; }
        public BsTOutputTemplateCQ AddOrderBy_CreateDatetime_Desc() { regOBD("CREATE_DATETIME");return this; }

        protected ConditionValue _deleteFlag;
        public ConditionValue DeleteFlag {
            get { if (_deleteFlag == null) { _deleteFlag = new ConditionValue(); } return _deleteFlag; }
        }
        protected override ConditionValue getCValueDeleteFlag() { return this.DeleteFlag; }


        public BsTOutputTemplateCQ AddOrderBy_DeleteFlag_Asc() { regOBA("DELETE_FLAG");return this; }
        public BsTOutputTemplateCQ AddOrderBy_DeleteFlag_Desc() { regOBD("DELETE_FLAG");return this; }

        public BsTOutputTemplateCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputTemplateCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputTemplateCQ baseQuery = (TOutputTemplateCQ)baseQueryAsSuper;
            TOutputTemplateCQ unionQuery = (TOutputTemplateCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTOutputTemplateMaster()) {
                unionQuery.QueryTOutputTemplateMaster().reflectRelationOnUnionQuery(baseQuery.QueryTOutputTemplateMaster(), unionQuery.QueryTOutputTemplateMaster());
            }
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
            }

        }
    
        protected TOutputTemplateMasterCQ _conditionQueryTOutputTemplateMaster;
        public TOutputTemplateMasterCQ QueryTOutputTemplateMaster() {
            return this.ConditionQueryTOutputTemplateMaster;
        }
        public TOutputTemplateMasterCQ ConditionQueryTOutputTemplateMaster {
            get {
                if (_conditionQueryTOutputTemplateMaster == null) {
                    _conditionQueryTOutputTemplateMaster = xcreateQueryTOutputTemplateMaster();
                    xsetupOuterJoin_TOutputTemplateMaster();
                }
                return _conditionQueryTOutputTemplateMaster;
            }
        }
        protected TOutputTemplateMasterCQ xcreateQueryTOutputTemplateMaster() {
            String nrp = resolveNextRelationPathTOutputTemplateMaster();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputTemplateMasterCQ cq = new TOutputTemplateMasterCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputTemplateMaster"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputTemplateMaster() {
            TOutputTemplateMasterCQ cq = ConditionQueryTOutputTemplateMaster;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputTemplateMaster() {
            return resolveNextRelationPath("T_OUTPUT_TEMPLATE", "tOutputTemplateMaster");
        }
        public bool hasConditionQueryTOutputTemplateMaster() {
            return _conditionQueryTOutputTemplateMaster != null;
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
            return resolveNextRelationPath("T_OUTPUT_TEMPLATE", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputTemplateCQ> _scalarSubQueryMap;
	    public Map<String, TOutputTemplateCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputTemplateCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputTemplateCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputTemplateCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputTemplateCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
