
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTCrossScenarioItemCQ : AbstractBsTCrossScenarioItemCQ {

        protected TCrossScenarioItemCIQ _inlineQuery;

        public BsTCrossScenarioItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TCrossScenarioItemCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TCrossScenarioItemCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TCrossScenarioItemCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TCrossScenarioItemCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _crossScenarioItemId;
        public ConditionValue CrossScenarioItemId {
            get { if (_crossScenarioItemId == null) { _crossScenarioItemId = new ConditionValue(); } return _crossScenarioItemId; }
        }
        protected override ConditionValue getCValueCrossScenarioItemId() { return this.CrossScenarioItemId; }


        protected Map<String, TPolylineCategoryListCQ> _crossScenarioItemId_ExistsSubQuery_TPolylineCategoryListListMap;
        public Map<String, TPolylineCategoryListCQ> CrossScenarioItemId_ExistsSubQuery_TPolylineCategoryListList { get { return _crossScenarioItemId_ExistsSubQuery_TPolylineCategoryListListMap; }}
        public override String keepCrossScenarioItemId_ExistsSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            if (_crossScenarioItemId_ExistsSubQuery_TPolylineCategoryListListMap == null) { _crossScenarioItemId_ExistsSubQuery_TPolylineCategoryListListMap = new LinkedHashMap<String, TPolylineCategoryListCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioItemId_ExistsSubQuery_TPolylineCategoryListListMap.size() + 1);
            _crossScenarioItemId_ExistsSubQuery_TPolylineCategoryListListMap.put(key, subQuery); return "CrossScenarioItemId_ExistsSubQuery_TPolylineCategoryListList." + key;
        }

        protected Map<String, TPolylineCategoryListCQ> _crossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListListMap;
        public Map<String, TPolylineCategoryListCQ> CrossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListList { get { return _crossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListListMap; }}
        public override String keepCrossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            if (_crossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListListMap == null) { _crossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListListMap = new LinkedHashMap<String, TPolylineCategoryListCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListListMap.size() + 1);
            _crossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListListMap.put(key, subQuery); return "CrossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListList." + key;
        }

        protected Map<String, TPolylineCategoryListCQ> _crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListMap;
        public Map<String, TPolylineCategoryListCQ> CrossScenarioItemId_InScopeSubQuery_TPolylineCategoryList { get { return _crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListMap; }}
        public override String keepCrossScenarioItemId_InScopeSubQuery_TPolylineCategoryList(TPolylineCategoryListCQ subQuery) {
            if (_crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListMap == null) { _crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListMap = new LinkedHashMap<String, TPolylineCategoryListCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListMap.size() + 1);
            _crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListMap.put(key, subQuery); return "CrossScenarioItemId_InScopeSubQuery_TPolylineCategoryList." + key;
        }

        protected Map<String, TPolylineCategoryListCQ> _crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListListMap;
        public Map<String, TPolylineCategoryListCQ> CrossScenarioItemId_InScopeSubQuery_TPolylineCategoryListList { get { return _crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListListMap; }}
        public override String keepCrossScenarioItemId_InScopeSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            if (_crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListListMap == null) { _crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListListMap = new LinkedHashMap<String, TPolylineCategoryListCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListListMap.size() + 1);
            _crossScenarioItemId_InScopeSubQuery_TPolylineCategoryListListMap.put(key, subQuery); return "CrossScenarioItemId_InScopeSubQuery_TPolylineCategoryListList." + key;
        }

        protected Map<String, TPolylineCategoryListCQ> _crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListMap;
        public Map<String, TPolylineCategoryListCQ> CrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryList { get { return _crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListMap; }}
        public override String keepCrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryList(TPolylineCategoryListCQ subQuery) {
            if (_crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListMap == null) { _crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListMap = new LinkedHashMap<String, TPolylineCategoryListCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListMap.size() + 1);
            _crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListMap.put(key, subQuery); return "CrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryList." + key;
        }

        protected Map<String, TPolylineCategoryListCQ> _crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListListMap;
        public Map<String, TPolylineCategoryListCQ> CrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListList { get { return _crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListListMap; }}
        public override String keepCrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            if (_crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListListMap == null) { _crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListListMap = new LinkedHashMap<String, TPolylineCategoryListCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListListMap.size() + 1);
            _crossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListListMap.put(key, subQuery); return "CrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListList." + key;
        }

        protected Map<String, TPolylineCategoryListCQ> _crossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListListMap;
        public Map<String, TPolylineCategoryListCQ> CrossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListList { get { return _crossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListListMap; }}
        public override String keepCrossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            if (_crossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListListMap == null) { _crossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListListMap = new LinkedHashMap<String, TPolylineCategoryListCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListListMap.size() + 1);
            _crossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListListMap.put(key, subQuery); return "CrossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListList." + key;
        }

        protected Map<String, TPolylineCategoryListCQ> _crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListMap;
        public Map<String, TPolylineCategoryListCQ> CrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListList { get { return _crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListMap; } }
        public override String keepCrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            if (_crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListMap == null) { _crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListMap = new LinkedHashMap<String, TPolylineCategoryListCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListMap.size() + 1);
            _crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListMap.put(key, subQuery); return "CrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListList." + key;
        }
        protected Map<String, Object> _crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameterMap;
        public Map<String, Object> CrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameter { get { return _crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameterMap; } }
        public override String keepCrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameter(Object parameterValue) {
            if (_crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameterMap == null) { _crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameterMap.size() + 1);
            _crossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameterMap.put(key, parameterValue); return "CrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameter." + key;
        }

        public BsTCrossScenarioItemCQ AddOrderBy_CrossScenarioItemId_Asc() { regOBA("CROSS_SCENARIO_ITEM_ID");return this; }
        public BsTCrossScenarioItemCQ AddOrderBy_CrossScenarioItemId_Desc() { regOBD("CROSS_SCENARIO_ITEM_ID");return this; }

        protected ConditionValue _crossScenarioTargetId;
        public ConditionValue CrossScenarioTargetId {
            get { if (_crossScenarioTargetId == null) { _crossScenarioTargetId = new ConditionValue(); } return _crossScenarioTargetId; }
        }
        protected override ConditionValue getCValueCrossScenarioTargetId() { return this.CrossScenarioTargetId; }


        protected Map<String, TCrossScenarioTargetCQ> _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap;
        public Map<String, TCrossScenarioTargetCQ> CrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget { get { return _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap; }}
        public override String keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget(TCrossScenarioTargetCQ subQuery) {
            if (_crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap == null) { _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap.size() + 1);
            _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap.put(key, subQuery); return "CrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget." + key;
        }

        protected Map<String, TCrossScenarioTargetCQ> _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap;
        public Map<String, TCrossScenarioTargetCQ> CrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget { get { return _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap; }}
        public override String keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget(TCrossScenarioTargetCQ subQuery) {
            if (_crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap == null) { _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap.size() + 1);
            _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap.put(key, subQuery); return "CrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget." + key;
        }

        public BsTCrossScenarioItemCQ AddOrderBy_CrossScenarioTargetId_Asc() { regOBA("CROSS_SCENARIO_TARGET_ID");return this; }
        public BsTCrossScenarioItemCQ AddOrderBy_CrossScenarioTargetId_Desc() { regOBD("CROSS_SCENARIO_TARGET_ID");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTCrossScenarioItemCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTCrossScenarioItemCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _axis1ItemId;
        public ConditionValue Axis1ItemId {
            get { if (_axis1ItemId == null) { _axis1ItemId = new ConditionValue(); } return _axis1ItemId; }
        }
        protected override ConditionValue getCValueAxis1ItemId() { return this.Axis1ItemId; }


        public BsTCrossScenarioItemCQ AddOrderBy_Axis1ItemId_Asc() { regOBA("AXIS1_ITEM_ID");return this; }
        public BsTCrossScenarioItemCQ AddOrderBy_Axis1ItemId_Desc() { regOBD("AXIS1_ITEM_ID");return this; }

        protected ConditionValue _axis2ItemId;
        public ConditionValue Axis2ItemId {
            get { if (_axis2ItemId == null) { _axis2ItemId = new ConditionValue(); } return _axis2ItemId; }
        }
        protected override ConditionValue getCValueAxis2ItemId() { return this.Axis2ItemId; }


        public BsTCrossScenarioItemCQ AddOrderBy_Axis2ItemId_Asc() { regOBA("AXIS2_ITEM_ID");return this; }
        public BsTCrossScenarioItemCQ AddOrderBy_Axis2ItemId_Desc() { regOBD("AXIS2_ITEM_ID");return this; }

        protected ConditionValue _viewItemName;
        public ConditionValue ViewItemName {
            get { if (_viewItemName == null) { _viewItemName = new ConditionValue(); } return _viewItemName; }
        }
        protected override ConditionValue getCValueViewItemName() { return this.ViewItemName; }


        public BsTCrossScenarioItemCQ AddOrderBy_ViewItemName_Asc() { regOBA("VIEW_ITEM_NAME");return this; }
        public BsTCrossScenarioItemCQ AddOrderBy_ViewItemName_Desc() { regOBD("VIEW_ITEM_NAME");return this; }

        protected ConditionValue _graphType;
        public ConditionValue GraphType {
            get { if (_graphType == null) { _graphType = new ConditionValue(); } return _graphType; }
        }
        protected override ConditionValue getCValueGraphType() { return this.GraphType; }


        public BsTCrossScenarioItemCQ AddOrderBy_GraphType_Asc() { regOBA("GRAPH_TYPE");return this; }
        public BsTCrossScenarioItemCQ AddOrderBy_GraphType_Desc() { regOBD("GRAPH_TYPE");return this; }

        protected ConditionValue _reportType;
        public ConditionValue ReportType {
            get { if (_reportType == null) { _reportType = new ConditionValue(); } return _reportType; }
        }
        protected override ConditionValue getCValueReportType() { return this.ReportType; }


        public BsTCrossScenarioItemCQ AddOrderBy_ReportType_Asc() { regOBA("REPORT_TYPE");return this; }
        public BsTCrossScenarioItemCQ AddOrderBy_ReportType_Desc() { regOBD("REPORT_TYPE");return this; }

        protected ConditionValue _titleString;
        public ConditionValue TitleString {
            get { if (_titleString == null) { _titleString = new ConditionValue(); } return _titleString; }
        }
        protected override ConditionValue getCValueTitleString() { return this.TitleString; }


        public BsTCrossScenarioItemCQ AddOrderBy_TitleString_Asc() { regOBA("TITLE_STRING");return this; }
        public BsTCrossScenarioItemCQ AddOrderBy_TitleString_Desc() { regOBD("TITLE_STRING");return this; }

        protected ConditionValue _scenarioComment;
        public ConditionValue ScenarioComment {
            get { if (_scenarioComment == null) { _scenarioComment = new ConditionValue(); } return _scenarioComment; }
        }
        protected override ConditionValue getCValueScenarioComment() { return this.ScenarioComment; }


        public BsTCrossScenarioItemCQ AddOrderBy_ScenarioComment_Asc() { regOBA("SCENARIO_COMMENT");return this; }
        public BsTCrossScenarioItemCQ AddOrderBy_ScenarioComment_Desc() { regOBD("SCENARIO_COMMENT");return this; }

        public BsTCrossScenarioItemCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTCrossScenarioItemCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TCrossScenarioItemCQ baseQuery = (TCrossScenarioItemCQ)baseQueryAsSuper;
            TCrossScenarioItemCQ unionQuery = (TCrossScenarioItemCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTCrossScenarioTarget()) {
                unionQuery.QueryTCrossScenarioTarget().reflectRelationOnUnionQuery(baseQuery.QueryTCrossScenarioTarget(), unionQuery.QueryTCrossScenarioTarget());
            }
            if (baseQuery.hasConditionQueryTPolylineCategoryList()) {
                unionQuery.QueryTPolylineCategoryList().reflectRelationOnUnionQuery(baseQuery.QueryTPolylineCategoryList(), unionQuery.QueryTPolylineCategoryList());
            }

        }
    
        protected TCrossScenarioTargetCQ _conditionQueryTCrossScenarioTarget;
        public TCrossScenarioTargetCQ QueryTCrossScenarioTarget() {
            return this.ConditionQueryTCrossScenarioTarget;
        }
        public TCrossScenarioTargetCQ ConditionQueryTCrossScenarioTarget {
            get {
                if (_conditionQueryTCrossScenarioTarget == null) {
                    _conditionQueryTCrossScenarioTarget = xcreateQueryTCrossScenarioTarget();
                    xsetupOuterJoin_TCrossScenarioTarget();
                }
                return _conditionQueryTCrossScenarioTarget;
            }
        }
        protected TCrossScenarioTargetCQ xcreateQueryTCrossScenarioTarget() {
            String nrp = resolveNextRelationPathTCrossScenarioTarget();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TCrossScenarioTargetCQ cq = new TCrossScenarioTargetCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tCrossScenarioTarget"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TCrossScenarioTarget() {
            TCrossScenarioTargetCQ cq = ConditionQueryTCrossScenarioTarget;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTCrossScenarioTarget() {
            return resolveNextRelationPath("T_CROSS_SCENARIO_ITEM", "tCrossScenarioTarget");
        }
        public bool hasConditionQueryTCrossScenarioTarget() {
            return _conditionQueryTCrossScenarioTarget != null;
        }
        protected TPolylineCategoryListCQ _conditionQueryTPolylineCategoryList;
        public TPolylineCategoryListCQ QueryTPolylineCategoryList() {
            return this.ConditionQueryTPolylineCategoryList;
        }
        public TPolylineCategoryListCQ ConditionQueryTPolylineCategoryList {
            get {
                if (_conditionQueryTPolylineCategoryList == null) {
                    _conditionQueryTPolylineCategoryList = xcreateQueryTPolylineCategoryList();
                    xsetupOuterJoin_TPolylineCategoryList();
                }
                return _conditionQueryTPolylineCategoryList;
            }
        }
        protected TPolylineCategoryListCQ xcreateQueryTPolylineCategoryList() {
            String nrp = resolveNextRelationPathTPolylineCategoryList();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TPolylineCategoryListCQ cq = new TPolylineCategoryListCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tPolylineCategoryList"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TPolylineCategoryList() {
            TPolylineCategoryListCQ cq = ConditionQueryTPolylineCategoryList;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("CROSS_SCENARIO_ITEM_ID", "Cross_Scenario_Item_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTPolylineCategoryList() {
            return resolveNextRelationPath("T_CROSS_SCENARIO_ITEM", "tPolylineCategoryList");
        }
        public bool hasConditionQueryTPolylineCategoryList() {
            return _conditionQueryTPolylineCategoryList != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TCrossScenarioItemCQ> _scalarSubQueryMap;
	    public Map<String, TCrossScenarioItemCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TCrossScenarioItemCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TCrossScenarioItemCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TCrossScenarioItemCQ> _myselfInScopeSubQueryMap;
        public Map<String, TCrossScenarioItemCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TCrossScenarioItemCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TCrossScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
