
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTReportsetCQ : AbstractBsTReportsetCQ {

        protected TReportsetCIQ _inlineQuery;

        public BsTReportsetCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TReportsetCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TReportsetCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TReportsetCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TReportsetCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _reportsetId;
        public ConditionValue ReportsetId {
            get { if (_reportsetId == null) { _reportsetId = new ConditionValue(); } return _reportsetId; }
        }
        protected override ConditionValue getCValueReportsetId() { return this.ReportsetId; }


        protected Map<String, TReportCQ> _reportsetId_ExistsSubQuery_TReportListMap;
        public Map<String, TReportCQ> ReportsetId_ExistsSubQuery_TReportList { get { return _reportsetId_ExistsSubQuery_TReportListMap; }}
        public override String keepReportsetId_ExistsSubQuery_TReportList(TReportCQ subQuery) {
            if (_reportsetId_ExistsSubQuery_TReportListMap == null) { _reportsetId_ExistsSubQuery_TReportListMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_reportsetId_ExistsSubQuery_TReportListMap.size() + 1);
            _reportsetId_ExistsSubQuery_TReportListMap.put(key, subQuery); return "ReportsetId_ExistsSubQuery_TReportList." + key;
        }

        protected Map<String, TReportCQ> _reportsetId_NotExistsSubQuery_TReportListMap;
        public Map<String, TReportCQ> ReportsetId_NotExistsSubQuery_TReportList { get { return _reportsetId_NotExistsSubQuery_TReportListMap; }}
        public override String keepReportsetId_NotExistsSubQuery_TReportList(TReportCQ subQuery) {
            if (_reportsetId_NotExistsSubQuery_TReportListMap == null) { _reportsetId_NotExistsSubQuery_TReportListMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_reportsetId_NotExistsSubQuery_TReportListMap.size() + 1);
            _reportsetId_NotExistsSubQuery_TReportListMap.put(key, subQuery); return "ReportsetId_NotExistsSubQuery_TReportList." + key;
        }

        protected Map<String, TReportCQ> _reportsetId_InScopeSubQuery_TReportMap;
        public Map<String, TReportCQ> ReportsetId_InScopeSubQuery_TReport { get { return _reportsetId_InScopeSubQuery_TReportMap; }}
        public override String keepReportsetId_InScopeSubQuery_TReport(TReportCQ subQuery) {
            if (_reportsetId_InScopeSubQuery_TReportMap == null) { _reportsetId_InScopeSubQuery_TReportMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_reportsetId_InScopeSubQuery_TReportMap.size() + 1);
            _reportsetId_InScopeSubQuery_TReportMap.put(key, subQuery); return "ReportsetId_InScopeSubQuery_TReport." + key;
        }

        protected Map<String, TReportCQ> _reportsetId_InScopeSubQuery_TReportListMap;
        public Map<String, TReportCQ> ReportsetId_InScopeSubQuery_TReportList { get { return _reportsetId_InScopeSubQuery_TReportListMap; }}
        public override String keepReportsetId_InScopeSubQuery_TReportList(TReportCQ subQuery) {
            if (_reportsetId_InScopeSubQuery_TReportListMap == null) { _reportsetId_InScopeSubQuery_TReportListMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_reportsetId_InScopeSubQuery_TReportListMap.size() + 1);
            _reportsetId_InScopeSubQuery_TReportListMap.put(key, subQuery); return "ReportsetId_InScopeSubQuery_TReportList." + key;
        }

        protected Map<String, TReportCQ> _reportsetId_NotInScopeSubQuery_TReportMap;
        public Map<String, TReportCQ> ReportsetId_NotInScopeSubQuery_TReport { get { return _reportsetId_NotInScopeSubQuery_TReportMap; }}
        public override String keepReportsetId_NotInScopeSubQuery_TReport(TReportCQ subQuery) {
            if (_reportsetId_NotInScopeSubQuery_TReportMap == null) { _reportsetId_NotInScopeSubQuery_TReportMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_reportsetId_NotInScopeSubQuery_TReportMap.size() + 1);
            _reportsetId_NotInScopeSubQuery_TReportMap.put(key, subQuery); return "ReportsetId_NotInScopeSubQuery_TReport." + key;
        }

        protected Map<String, TReportCQ> _reportsetId_NotInScopeSubQuery_TReportListMap;
        public Map<String, TReportCQ> ReportsetId_NotInScopeSubQuery_TReportList { get { return _reportsetId_NotInScopeSubQuery_TReportListMap; }}
        public override String keepReportsetId_NotInScopeSubQuery_TReportList(TReportCQ subQuery) {
            if (_reportsetId_NotInScopeSubQuery_TReportListMap == null) { _reportsetId_NotInScopeSubQuery_TReportListMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_reportsetId_NotInScopeSubQuery_TReportListMap.size() + 1);
            _reportsetId_NotInScopeSubQuery_TReportListMap.put(key, subQuery); return "ReportsetId_NotInScopeSubQuery_TReportList." + key;
        }

        protected Map<String, TReportCQ> _reportsetId_SpecifyDerivedReferrer_TReportListMap;
        public Map<String, TReportCQ> ReportsetId_SpecifyDerivedReferrer_TReportList { get { return _reportsetId_SpecifyDerivedReferrer_TReportListMap; }}
        public override String keepReportsetId_SpecifyDerivedReferrer_TReportList(TReportCQ subQuery) {
            if (_reportsetId_SpecifyDerivedReferrer_TReportListMap == null) { _reportsetId_SpecifyDerivedReferrer_TReportListMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_reportsetId_SpecifyDerivedReferrer_TReportListMap.size() + 1);
            _reportsetId_SpecifyDerivedReferrer_TReportListMap.put(key, subQuery); return "ReportsetId_SpecifyDerivedReferrer_TReportList." + key;
        }

        protected Map<String, TReportCQ> _reportsetId_QueryDerivedReferrer_TReportListMap;
        public Map<String, TReportCQ> ReportsetId_QueryDerivedReferrer_TReportList { get { return _reportsetId_QueryDerivedReferrer_TReportListMap; } }
        public override String keepReportsetId_QueryDerivedReferrer_TReportList(TReportCQ subQuery) {
            if (_reportsetId_QueryDerivedReferrer_TReportListMap == null) { _reportsetId_QueryDerivedReferrer_TReportListMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_reportsetId_QueryDerivedReferrer_TReportListMap.size() + 1);
            _reportsetId_QueryDerivedReferrer_TReportListMap.put(key, subQuery); return "ReportsetId_QueryDerivedReferrer_TReportList." + key;
        }
        protected Map<String, Object> _reportsetId_QueryDerivedReferrer_TReportListParameterMap;
        public Map<String, Object> ReportsetId_QueryDerivedReferrer_TReportListParameter { get { return _reportsetId_QueryDerivedReferrer_TReportListParameterMap; } }
        public override String keepReportsetId_QueryDerivedReferrer_TReportListParameter(Object parameterValue) {
            if (_reportsetId_QueryDerivedReferrer_TReportListParameterMap == null) { _reportsetId_QueryDerivedReferrer_TReportListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_reportsetId_QueryDerivedReferrer_TReportListParameterMap.size() + 1);
            _reportsetId_QueryDerivedReferrer_TReportListParameterMap.put(key, parameterValue); return "ReportsetId_QueryDerivedReferrer_TReportListParameter." + key;
        }

        public BsTReportsetCQ AddOrderBy_ReportsetId_Asc() { regOBA("REPORTSET_ID");return this; }
        public BsTReportsetCQ AddOrderBy_ReportsetId_Desc() { regOBD("REPORTSET_ID");return this; }

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

        public BsTReportsetCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTReportsetCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _reportsetName;
        public ConditionValue ReportsetName {
            get { if (_reportsetName == null) { _reportsetName = new ConditionValue(); } return _reportsetName; }
        }
        protected override ConditionValue getCValueReportsetName() { return this.ReportsetName; }


        public BsTReportsetCQ AddOrderBy_ReportsetName_Asc() { regOBA("REPORTSET_NAME");return this; }
        public BsTReportsetCQ AddOrderBy_ReportsetName_Desc() { regOBD("REPORTSET_NAME");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTReportsetCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTReportsetCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        public BsTReportsetCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTReportsetCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TReportsetCQ baseQuery = (TReportsetCQ)baseQueryAsSuper;
            TReportsetCQ unionQuery = (TReportsetCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
            }
            if (baseQuery.hasConditionQueryTReport()) {
                unionQuery.QueryTReport().reflectRelationOnUnionQuery(baseQuery.QueryTReport(), unionQuery.QueryTReport());
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
            return resolveNextRelationPath("T_REPORTSET", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }
        protected TReportCQ _conditionQueryTReport;
        public TReportCQ QueryTReport() {
            return this.ConditionQueryTReport;
        }
        public TReportCQ ConditionQueryTReport {
            get {
                if (_conditionQueryTReport == null) {
                    _conditionQueryTReport = xcreateQueryTReport();
                    xsetupOuterJoin_TReport();
                }
                return _conditionQueryTReport;
            }
        }
        protected TReportCQ xcreateQueryTReport() {
            String nrp = resolveNextRelationPathTReport();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TReportCQ cq = new TReportCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tReport"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TReport() {
            TReportCQ cq = ConditionQueryTReport;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("REPORTSET_ID", "Reportset_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTReport() {
            return resolveNextRelationPath("T_REPORTSET", "tReport");
        }
        public bool hasConditionQueryTReport() {
            return _conditionQueryTReport != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TReportsetCQ> _scalarSubQueryMap;
	    public Map<String, TReportsetCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TReportsetCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TReportsetCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TReportsetCQ> _myselfInScopeSubQueryMap;
        public Map<String, TReportsetCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TReportsetCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TReportsetCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
