
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTReportChildCQ : AbstractBsTReportChildCQ {

        protected TReportChildCIQ _inlineQuery;

        public BsTReportChildCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TReportChildCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TReportChildCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TReportChildCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TReportChildCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _reportChildId;
        public ConditionValue ReportChildId {
            get { if (_reportChildId == null) { _reportChildId = new ConditionValue(); } return _reportChildId; }
        }
        protected override ConditionValue getCValueReportChildId() { return this.ReportChildId; }


        public BsTReportChildCQ AddOrderBy_ReportChildId_Asc() { regOBA("REPORT_CHILD_ID");return this; }
        public BsTReportChildCQ AddOrderBy_ReportChildId_Desc() { regOBD("REPORT_CHILD_ID");return this; }

        protected ConditionValue _parentReportId;
        public ConditionValue ParentReportId {
            get { if (_parentReportId == null) { _parentReportId = new ConditionValue(); } return _parentReportId; }
        }
        protected override ConditionValue getCValueParentReportId() { return this.ParentReportId; }


        protected Map<String, TReportCQ> _parentReportId_InScopeSubQuery_TReportMap;
        public Map<String, TReportCQ> ParentReportId_InScopeSubQuery_TReport { get { return _parentReportId_InScopeSubQuery_TReportMap; }}
        public override String keepParentReportId_InScopeSubQuery_TReport(TReportCQ subQuery) {
            if (_parentReportId_InScopeSubQuery_TReportMap == null) { _parentReportId_InScopeSubQuery_TReportMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_parentReportId_InScopeSubQuery_TReportMap.size() + 1);
            _parentReportId_InScopeSubQuery_TReportMap.put(key, subQuery); return "ParentReportId_InScopeSubQuery_TReport." + key;
        }

        protected Map<String, TReportCQ> _parentReportId_NotInScopeSubQuery_TReportMap;
        public Map<String, TReportCQ> ParentReportId_NotInScopeSubQuery_TReport { get { return _parentReportId_NotInScopeSubQuery_TReportMap; }}
        public override String keepParentReportId_NotInScopeSubQuery_TReport(TReportCQ subQuery) {
            if (_parentReportId_NotInScopeSubQuery_TReportMap == null) { _parentReportId_NotInScopeSubQuery_TReportMap = new LinkedHashMap<String, TReportCQ>(); }
            String key = "subQueryMapKey" + (_parentReportId_NotInScopeSubQuery_TReportMap.size() + 1);
            _parentReportId_NotInScopeSubQuery_TReportMap.put(key, subQuery); return "ParentReportId_NotInScopeSubQuery_TReport." + key;
        }

        public BsTReportChildCQ AddOrderBy_ParentReportId_Asc() { regOBA("PARENT_REPORT_ID");return this; }
        public BsTReportChildCQ AddOrderBy_ParentReportId_Desc() { regOBD("PARENT_REPORT_ID");return this; }

        protected ConditionValue _targetScenarioItemId;
        public ConditionValue TargetScenarioItemId {
            get { if (_targetScenarioItemId == null) { _targetScenarioItemId = new ConditionValue(); } return _targetScenarioItemId; }
        }
        protected override ConditionValue getCValueTargetScenarioItemId() { return this.TargetScenarioItemId; }


        public BsTReportChildCQ AddOrderBy_TargetScenarioItemId_Asc() { regOBA("TARGET_SCENARIO_ITEM_ID");return this; }
        public BsTReportChildCQ AddOrderBy_TargetScenarioItemId_Desc() { regOBD("TARGET_SCENARIO_ITEM_ID");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTReportChildCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTReportChildCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        public BsTReportChildCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTReportChildCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TReportChildCQ baseQuery = (TReportChildCQ)baseQueryAsSuper;
            TReportChildCQ unionQuery = (TReportChildCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTReport()) {
                unionQuery.QueryTReport().reflectRelationOnUnionQuery(baseQuery.QueryTReport(), unionQuery.QueryTReport());
            }

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
            joinOnMap.put("PARENT_REPORT_ID", "REPORT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTReport() {
            return resolveNextRelationPath("T_REPORT_CHILD", "tReport");
        }
        public bool hasConditionQueryTReport() {
            return _conditionQueryTReport != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TReportChildCQ> _scalarSubQueryMap;
	    public Map<String, TReportChildCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TReportChildCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TReportChildCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TReportChildCQ> _myselfInScopeSubQueryMap;
        public Map<String, TReportChildCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TReportChildCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TReportChildCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
