
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTQcwebSurveyDetailCQ : AbstractBsTQcwebSurveyDetailCQ {

        protected TQcwebSurveyDetailCIQ _inlineQuery;

        public BsTQcwebSurveyDetailCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TQcwebSurveyDetailCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TQcwebSurveyDetailCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TQcwebSurveyDetailCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TQcwebSurveyDetailCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _qcwebDetailId;
        public ConditionValue QcwebDetailId {
            get { if (_qcwebDetailId == null) { _qcwebDetailId = new ConditionValue(); } return _qcwebDetailId; }
        }
        protected override ConditionValue getCValueQcwebDetailId() { return this.QcwebDetailId; }


        public BsTQcwebSurveyDetailCQ AddOrderBy_QcwebDetailId_Asc() { regOBA("QCWEB_DETAIL_ID");return this; }
        public BsTQcwebSurveyDetailCQ AddOrderBy_QcwebDetailId_Desc() { regOBD("QCWEB_DETAIL_ID");return this; }

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

        public BsTQcwebSurveyDetailCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTQcwebSurveyDetailCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _surveyNo;
        public ConditionValue SurveyNo {
            get { if (_surveyNo == null) { _surveyNo = new ConditionValue(); } return _surveyNo; }
        }
        protected override ConditionValue getCValueSurveyNo() { return this.SurveyNo; }


        public BsTQcwebSurveyDetailCQ AddOrderBy_SurveyNo_Asc() { regOBA("SURVEY_NO");return this; }
        public BsTQcwebSurveyDetailCQ AddOrderBy_SurveyNo_Desc() { regOBD("SURVEY_NO");return this; }

        protected ConditionValue _surveyName;
        public ConditionValue SurveyName {
            get { if (_surveyName == null) { _surveyName = new ConditionValue(); } return _surveyName; }
        }
        protected override ConditionValue getCValueSurveyName() { return this.SurveyName; }


        public BsTQcwebSurveyDetailCQ AddOrderBy_SurveyName_Asc() { regOBA("SURVEY_NAME");return this; }
        public BsTQcwebSurveyDetailCQ AddOrderBy_SurveyName_Desc() { regOBD("SURVEY_NAME");return this; }

        protected ConditionValue _qc3uniqueId;
        public ConditionValue Qc3uniqueId {
            get { if (_qc3uniqueId == null) { _qc3uniqueId = new ConditionValue(); } return _qc3uniqueId; }
        }
        protected override ConditionValue getCValueQc3uniqueId() { return this.Qc3uniqueId; }


        public BsTQcwebSurveyDetailCQ AddOrderBy_Qc3uniqueId_Asc() { regOBA("QC3UNIQUE_ID");return this; }
        public BsTQcwebSurveyDetailCQ AddOrderBy_Qc3uniqueId_Desc() { regOBD("QC3UNIQUE_ID");return this; }

        protected ConditionValue _surveyMethod;
        public ConditionValue SurveyMethod {
            get { if (_surveyMethod == null) { _surveyMethod = new ConditionValue(); } return _surveyMethod; }
        }
        protected override ConditionValue getCValueSurveyMethod() { return this.SurveyMethod; }


        public BsTQcwebSurveyDetailCQ AddOrderBy_SurveyMethod_Asc() { regOBA("SURVEY_METHOD");return this; }
        public BsTQcwebSurveyDetailCQ AddOrderBy_SurveyMethod_Desc() { regOBD("SURVEY_METHOD");return this; }

        protected ConditionValue _serviceType;
        public ConditionValue ServiceType {
            get { if (_serviceType == null) { _serviceType = new ConditionValue(); } return _serviceType; }
        }
        protected override ConditionValue getCValueServiceType() { return this.ServiceType; }


        public BsTQcwebSurveyDetailCQ AddOrderBy_ServiceType_Asc() { regOBA("SERVICE_TYPE");return this; }
        public BsTQcwebSurveyDetailCQ AddOrderBy_ServiceType_Desc() { regOBD("SERVICE_TYPE");return this; }

        protected ConditionValue _surveyDate;
        public ConditionValue SurveyDate {
            get { if (_surveyDate == null) { _surveyDate = new ConditionValue(); } return _surveyDate; }
        }
        protected override ConditionValue getCValueSurveyDate() { return this.SurveyDate; }


        public BsTQcwebSurveyDetailCQ AddOrderBy_SurveyDate_Asc() { regOBA("SURVEY_DATE");return this; }
        public BsTQcwebSurveyDetailCQ AddOrderBy_SurveyDate_Desc() { regOBD("SURVEY_DATE");return this; }

        public BsTQcwebSurveyDetailCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTQcwebSurveyDetailCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TQcwebSurveyDetailCQ baseQuery = (TQcwebSurveyDetailCQ)baseQueryAsSuper;
            TQcwebSurveyDetailCQ unionQuery = (TQcwebSurveyDetailCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
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
            return resolveNextRelationPath("T_QCWEB_SURVEY_DETAIL", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TQcwebSurveyDetailCQ> _scalarSubQueryMap;
	    public Map<String, TQcwebSurveyDetailCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TQcwebSurveyDetailCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TQcwebSurveyDetailCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TQcwebSurveyDetailCQ> _myselfInScopeSubQueryMap;
        public Map<String, TQcwebSurveyDetailCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TQcwebSurveyDetailCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TQcwebSurveyDetailCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
