
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTReportCQ : AbstractBsTReportCQ {

        protected TReportCIQ _inlineQuery;

        public BsTReportCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TReportCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TReportCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TReportCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TReportCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _reportId;
        public ConditionValue ReportId {
            get { if (_reportId == null) { _reportId = new ConditionValue(); } return _reportId; }
        }
        protected override ConditionValue getCValueReportId() { return this.ReportId; }


        protected Map<String, TReportChildCQ> _reportId_ExistsSubQuery_TReportChildListMap;
        public Map<String, TReportChildCQ> ReportId_ExistsSubQuery_TReportChildList { get { return _reportId_ExistsSubQuery_TReportChildListMap; }}
        public override String keepReportId_ExistsSubQuery_TReportChildList(TReportChildCQ subQuery) {
            if (_reportId_ExistsSubQuery_TReportChildListMap == null) { _reportId_ExistsSubQuery_TReportChildListMap = new LinkedHashMap<String, TReportChildCQ>(); }
            String key = "subQueryMapKey" + (_reportId_ExistsSubQuery_TReportChildListMap.size() + 1);
            _reportId_ExistsSubQuery_TReportChildListMap.put(key, subQuery); return "ReportId_ExistsSubQuery_TReportChildList." + key;
        }

        protected Map<String, TReportChildCQ> _reportId_NotExistsSubQuery_TReportChildListMap;
        public Map<String, TReportChildCQ> ReportId_NotExistsSubQuery_TReportChildList { get { return _reportId_NotExistsSubQuery_TReportChildListMap; }}
        public override String keepReportId_NotExistsSubQuery_TReportChildList(TReportChildCQ subQuery) {
            if (_reportId_NotExistsSubQuery_TReportChildListMap == null) { _reportId_NotExistsSubQuery_TReportChildListMap = new LinkedHashMap<String, TReportChildCQ>(); }
            String key = "subQueryMapKey" + (_reportId_NotExistsSubQuery_TReportChildListMap.size() + 1);
            _reportId_NotExistsSubQuery_TReportChildListMap.put(key, subQuery); return "ReportId_NotExistsSubQuery_TReportChildList." + key;
        }

        protected Map<String, TReportChildCQ> _reportId_InScopeSubQuery_TReportChildMap;
        public Map<String, TReportChildCQ> ReportId_InScopeSubQuery_TReportChild { get { return _reportId_InScopeSubQuery_TReportChildMap; }}
        public override String keepReportId_InScopeSubQuery_TReportChild(TReportChildCQ subQuery) {
            if (_reportId_InScopeSubQuery_TReportChildMap == null) { _reportId_InScopeSubQuery_TReportChildMap = new LinkedHashMap<String, TReportChildCQ>(); }
            String key = "subQueryMapKey" + (_reportId_InScopeSubQuery_TReportChildMap.size() + 1);
            _reportId_InScopeSubQuery_TReportChildMap.put(key, subQuery); return "ReportId_InScopeSubQuery_TReportChild." + key;
        }

        protected Map<String, TReportChildCQ> _reportId_InScopeSubQuery_TReportChildListMap;
        public Map<String, TReportChildCQ> ReportId_InScopeSubQuery_TReportChildList { get { return _reportId_InScopeSubQuery_TReportChildListMap; }}
        public override String keepReportId_InScopeSubQuery_TReportChildList(TReportChildCQ subQuery) {
            if (_reportId_InScopeSubQuery_TReportChildListMap == null) { _reportId_InScopeSubQuery_TReportChildListMap = new LinkedHashMap<String, TReportChildCQ>(); }
            String key = "subQueryMapKey" + (_reportId_InScopeSubQuery_TReportChildListMap.size() + 1);
            _reportId_InScopeSubQuery_TReportChildListMap.put(key, subQuery); return "ReportId_InScopeSubQuery_TReportChildList." + key;
        }

        protected Map<String, TReportChildCQ> _reportId_NotInScopeSubQuery_TReportChildMap;
        public Map<String, TReportChildCQ> ReportId_NotInScopeSubQuery_TReportChild { get { return _reportId_NotInScopeSubQuery_TReportChildMap; }}
        public override String keepReportId_NotInScopeSubQuery_TReportChild(TReportChildCQ subQuery) {
            if (_reportId_NotInScopeSubQuery_TReportChildMap == null) { _reportId_NotInScopeSubQuery_TReportChildMap = new LinkedHashMap<String, TReportChildCQ>(); }
            String key = "subQueryMapKey" + (_reportId_NotInScopeSubQuery_TReportChildMap.size() + 1);
            _reportId_NotInScopeSubQuery_TReportChildMap.put(key, subQuery); return "ReportId_NotInScopeSubQuery_TReportChild." + key;
        }

        protected Map<String, TReportChildCQ> _reportId_NotInScopeSubQuery_TReportChildListMap;
        public Map<String, TReportChildCQ> ReportId_NotInScopeSubQuery_TReportChildList { get { return _reportId_NotInScopeSubQuery_TReportChildListMap; }}
        public override String keepReportId_NotInScopeSubQuery_TReportChildList(TReportChildCQ subQuery) {
            if (_reportId_NotInScopeSubQuery_TReportChildListMap == null) { _reportId_NotInScopeSubQuery_TReportChildListMap = new LinkedHashMap<String, TReportChildCQ>(); }
            String key = "subQueryMapKey" + (_reportId_NotInScopeSubQuery_TReportChildListMap.size() + 1);
            _reportId_NotInScopeSubQuery_TReportChildListMap.put(key, subQuery); return "ReportId_NotInScopeSubQuery_TReportChildList." + key;
        }

        protected Map<String, TReportChildCQ> _reportId_SpecifyDerivedReferrer_TReportChildListMap;
        public Map<String, TReportChildCQ> ReportId_SpecifyDerivedReferrer_TReportChildList { get { return _reportId_SpecifyDerivedReferrer_TReportChildListMap; }}
        public override String keepReportId_SpecifyDerivedReferrer_TReportChildList(TReportChildCQ subQuery) {
            if (_reportId_SpecifyDerivedReferrer_TReportChildListMap == null) { _reportId_SpecifyDerivedReferrer_TReportChildListMap = new LinkedHashMap<String, TReportChildCQ>(); }
            String key = "subQueryMapKey" + (_reportId_SpecifyDerivedReferrer_TReportChildListMap.size() + 1);
            _reportId_SpecifyDerivedReferrer_TReportChildListMap.put(key, subQuery); return "ReportId_SpecifyDerivedReferrer_TReportChildList." + key;
        }

        protected Map<String, TReportChildCQ> _reportId_QueryDerivedReferrer_TReportChildListMap;
        public Map<String, TReportChildCQ> ReportId_QueryDerivedReferrer_TReportChildList { get { return _reportId_QueryDerivedReferrer_TReportChildListMap; } }
        public override String keepReportId_QueryDerivedReferrer_TReportChildList(TReportChildCQ subQuery) {
            if (_reportId_QueryDerivedReferrer_TReportChildListMap == null) { _reportId_QueryDerivedReferrer_TReportChildListMap = new LinkedHashMap<String, TReportChildCQ>(); }
            String key = "subQueryMapKey" + (_reportId_QueryDerivedReferrer_TReportChildListMap.size() + 1);
            _reportId_QueryDerivedReferrer_TReportChildListMap.put(key, subQuery); return "ReportId_QueryDerivedReferrer_TReportChildList." + key;
        }
        protected Map<String, Object> _reportId_QueryDerivedReferrer_TReportChildListParameterMap;
        public Map<String, Object> ReportId_QueryDerivedReferrer_TReportChildListParameter { get { return _reportId_QueryDerivedReferrer_TReportChildListParameterMap; } }
        public override String keepReportId_QueryDerivedReferrer_TReportChildListParameter(Object parameterValue) {
            if (_reportId_QueryDerivedReferrer_TReportChildListParameterMap == null) { _reportId_QueryDerivedReferrer_TReportChildListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_reportId_QueryDerivedReferrer_TReportChildListParameterMap.size() + 1);
            _reportId_QueryDerivedReferrer_TReportChildListParameterMap.put(key, parameterValue); return "ReportId_QueryDerivedReferrer_TReportChildListParameter." + key;
        }

        public BsTReportCQ AddOrderBy_ReportId_Asc() { regOBA("REPORT_ID");return this; }
        public BsTReportCQ AddOrderBy_ReportId_Desc() { regOBD("REPORT_ID");return this; }

        protected ConditionValue _reportsetId;
        public ConditionValue ReportsetId {
            get { if (_reportsetId == null) { _reportsetId = new ConditionValue(); } return _reportsetId; }
        }
        protected override ConditionValue getCValueReportsetId() { return this.ReportsetId; }


        protected Map<String, TReportsetCQ> _reportsetId_InScopeSubQuery_TReportsetMap;
        public Map<String, TReportsetCQ> ReportsetId_InScopeSubQuery_TReportset { get { return _reportsetId_InScopeSubQuery_TReportsetMap; }}
        public override String keepReportsetId_InScopeSubQuery_TReportset(TReportsetCQ subQuery) {
            if (_reportsetId_InScopeSubQuery_TReportsetMap == null) { _reportsetId_InScopeSubQuery_TReportsetMap = new LinkedHashMap<String, TReportsetCQ>(); }
            String key = "subQueryMapKey" + (_reportsetId_InScopeSubQuery_TReportsetMap.size() + 1);
            _reportsetId_InScopeSubQuery_TReportsetMap.put(key, subQuery); return "ReportsetId_InScopeSubQuery_TReportset." + key;
        }

        protected Map<String, TReportsetCQ> _reportsetId_NotInScopeSubQuery_TReportsetMap;
        public Map<String, TReportsetCQ> ReportsetId_NotInScopeSubQuery_TReportset { get { return _reportsetId_NotInScopeSubQuery_TReportsetMap; }}
        public override String keepReportsetId_NotInScopeSubQuery_TReportset(TReportsetCQ subQuery) {
            if (_reportsetId_NotInScopeSubQuery_TReportsetMap == null) { _reportsetId_NotInScopeSubQuery_TReportsetMap = new LinkedHashMap<String, TReportsetCQ>(); }
            String key = "subQueryMapKey" + (_reportsetId_NotInScopeSubQuery_TReportsetMap.size() + 1);
            _reportsetId_NotInScopeSubQuery_TReportsetMap.put(key, subQuery); return "ReportsetId_NotInScopeSubQuery_TReportset." + key;
        }

        public BsTReportCQ AddOrderBy_ReportsetId_Asc() { regOBA("REPORTSET_ID");return this; }
        public BsTReportCQ AddOrderBy_ReportsetId_Desc() { regOBD("REPORTSET_ID");return this; }

        protected ConditionValue _targetScenarioItemId;
        public ConditionValue TargetScenarioItemId {
            get { if (_targetScenarioItemId == null) { _targetScenarioItemId = new ConditionValue(); } return _targetScenarioItemId; }
        }
        protected override ConditionValue getCValueTargetScenarioItemId() { return this.TargetScenarioItemId; }


        public BsTReportCQ AddOrderBy_TargetScenarioItemId_Asc() { regOBA("TARGET_SCENARIO_ITEM_ID");return this; }
        public BsTReportCQ AddOrderBy_TargetScenarioItemId_Desc() { regOBD("TARGET_SCENARIO_ITEM_ID");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTReportCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTReportCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _childDiv;
        public ConditionValue ChildDiv {
            get { if (_childDiv == null) { _childDiv = new ConditionValue(); } return _childDiv; }
        }
        protected override ConditionValue getCValueChildDiv() { return this.ChildDiv; }


        public BsTReportCQ AddOrderBy_ChildDiv_Asc() { regOBA("CHILD_DIV");return this; }
        public BsTReportCQ AddOrderBy_ChildDiv_Desc() { regOBD("CHILD_DIV");return this; }

        protected ConditionValue _scenarioType;
        public ConditionValue ScenarioType {
            get { if (_scenarioType == null) { _scenarioType = new ConditionValue(); } return _scenarioType; }
        }
        protected override ConditionValue getCValueScenarioType() { return this.ScenarioType; }


        public BsTReportCQ AddOrderBy_ScenarioType_Asc() { regOBA("SCENARIO_TYPE");return this; }
        public BsTReportCQ AddOrderBy_ScenarioType_Desc() { regOBD("SCENARIO_TYPE");return this; }

        public BsTReportCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTReportCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TReportCQ baseQuery = (TReportCQ)baseQueryAsSuper;
            TReportCQ unionQuery = (TReportCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTReportset()) {
                unionQuery.QueryTReportset().reflectRelationOnUnionQuery(baseQuery.QueryTReportset(), unionQuery.QueryTReportset());
            }
            if (baseQuery.hasConditionQueryTReportChild()) {
                unionQuery.QueryTReportChild().reflectRelationOnUnionQuery(baseQuery.QueryTReportChild(), unionQuery.QueryTReportChild());
            }

        }
    
        protected TReportsetCQ _conditionQueryTReportset;
        public TReportsetCQ QueryTReportset() {
            return this.ConditionQueryTReportset;
        }
        public TReportsetCQ ConditionQueryTReportset {
            get {
                if (_conditionQueryTReportset == null) {
                    _conditionQueryTReportset = xcreateQueryTReportset();
                    xsetupOuterJoin_TReportset();
                }
                return _conditionQueryTReportset;
            }
        }
        protected TReportsetCQ xcreateQueryTReportset() {
            String nrp = resolveNextRelationPathTReportset();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TReportsetCQ cq = new TReportsetCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tReportset"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TReportset() {
            TReportsetCQ cq = ConditionQueryTReportset;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("REPORTSET_ID", "REPORTSET_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTReportset() {
            return resolveNextRelationPath("T_REPORT", "tReportset");
        }
        public bool hasConditionQueryTReportset() {
            return _conditionQueryTReportset != null;
        }
        protected TReportChildCQ _conditionQueryTReportChild;
        public TReportChildCQ QueryTReportChild() {
            return this.ConditionQueryTReportChild;
        }
        public TReportChildCQ ConditionQueryTReportChild {
            get {
                if (_conditionQueryTReportChild == null) {
                    _conditionQueryTReportChild = xcreateQueryTReportChild();
                    xsetupOuterJoin_TReportChild();
                }
                return _conditionQueryTReportChild;
            }
        }
        protected TReportChildCQ xcreateQueryTReportChild() {
            String nrp = resolveNextRelationPathTReportChild();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TReportChildCQ cq = new TReportChildCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tReportChild"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TReportChild() {
            TReportChildCQ cq = ConditionQueryTReportChild;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("REPORT_ID", "Parent_Report_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTReportChild() {
            return resolveNextRelationPath("T_REPORT", "tReportChild");
        }
        public bool hasConditionQueryTReportChild() {
            return _conditionQueryTReportChild != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TReportCQ> _scalarSubQueryMap;
	    public Map<String, TReportCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TReportCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TReportCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TReportCQ> _myselfInScopeSubQueryMap;
        public Map<String, TReportCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TReportCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
