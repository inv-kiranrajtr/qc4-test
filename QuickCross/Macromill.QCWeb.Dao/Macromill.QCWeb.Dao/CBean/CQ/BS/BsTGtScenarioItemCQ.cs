
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTGtScenarioItemCQ : AbstractBsTGtScenarioItemCQ {

        protected TGtScenarioItemCIQ _inlineQuery;

        public BsTGtScenarioItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TGtScenarioItemCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TGtScenarioItemCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TGtScenarioItemCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TGtScenarioItemCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _gtScenarioItemId;
        public ConditionValue GtScenarioItemId {
            get { if (_gtScenarioItemId == null) { _gtScenarioItemId = new ConditionValue(); } return _gtScenarioItemId; }
        }
        protected override ConditionValue getCValueGtScenarioItemId() { return this.GtScenarioItemId; }


        protected Map<String, TColorSetInfoGtCQ> _gtScenarioItemId_ExistsSubQuery_TColorSetInfoGtListMap;
        public Map<String, TColorSetInfoGtCQ> GtScenarioItemId_ExistsSubQuery_TColorSetInfoGtList { get { return _gtScenarioItemId_ExistsSubQuery_TColorSetInfoGtListMap; }}
        public override String keepGtScenarioItemId_ExistsSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            if (_gtScenarioItemId_ExistsSubQuery_TColorSetInfoGtListMap == null) { _gtScenarioItemId_ExistsSubQuery_TColorSetInfoGtListMap = new LinkedHashMap<String, TColorSetInfoGtCQ>(); }
            String key = "subQueryMapKey" + (_gtScenarioItemId_ExistsSubQuery_TColorSetInfoGtListMap.size() + 1);
            _gtScenarioItemId_ExistsSubQuery_TColorSetInfoGtListMap.put(key, subQuery); return "GtScenarioItemId_ExistsSubQuery_TColorSetInfoGtList." + key;
        }

        protected Map<String, TColorSetInfoGtCQ> _gtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtListMap;
        public Map<String, TColorSetInfoGtCQ> GtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtList { get { return _gtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtListMap; }}
        public override String keepGtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            if (_gtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtListMap == null) { _gtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtListMap = new LinkedHashMap<String, TColorSetInfoGtCQ>(); }
            String key = "subQueryMapKey" + (_gtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtListMap.size() + 1);
            _gtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtListMap.put(key, subQuery); return "GtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtList." + key;
        }

        protected Map<String, TColorSetInfoGtCQ> _gtScenarioItemId_InScopeSubQuery_TColorSetInfoGtListMap;
        public Map<String, TColorSetInfoGtCQ> GtScenarioItemId_InScopeSubQuery_TColorSetInfoGtList { get { return _gtScenarioItemId_InScopeSubQuery_TColorSetInfoGtListMap; }}
        public override String keepGtScenarioItemId_InScopeSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            if (_gtScenarioItemId_InScopeSubQuery_TColorSetInfoGtListMap == null) { _gtScenarioItemId_InScopeSubQuery_TColorSetInfoGtListMap = new LinkedHashMap<String, TColorSetInfoGtCQ>(); }
            String key = "subQueryMapKey" + (_gtScenarioItemId_InScopeSubQuery_TColorSetInfoGtListMap.size() + 1);
            _gtScenarioItemId_InScopeSubQuery_TColorSetInfoGtListMap.put(key, subQuery); return "GtScenarioItemId_InScopeSubQuery_TColorSetInfoGtList." + key;
        }

        protected Map<String, TColorSetInfoGtCQ> _gtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtListMap;
        public Map<String, TColorSetInfoGtCQ> GtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtList { get { return _gtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtListMap; }}
        public override String keepGtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            if (_gtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtListMap == null) { _gtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtListMap = new LinkedHashMap<String, TColorSetInfoGtCQ>(); }
            String key = "subQueryMapKey" + (_gtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtListMap.size() + 1);
            _gtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtListMap.put(key, subQuery); return "GtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtList." + key;
        }

        protected Map<String, TColorSetInfoGtCQ> _gtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtListMap;
        public Map<String, TColorSetInfoGtCQ> GtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtList { get { return _gtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtListMap; }}
        public override String keepGtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            if (_gtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtListMap == null) { _gtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtListMap = new LinkedHashMap<String, TColorSetInfoGtCQ>(); }
            String key = "subQueryMapKey" + (_gtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtListMap.size() + 1);
            _gtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtListMap.put(key, subQuery); return "GtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtList." + key;
        }

        protected Map<String, TColorSetInfoGtCQ> _gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListMap;
        public Map<String, TColorSetInfoGtCQ> GtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtList { get { return _gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListMap; } }
        public override String keepGtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            if (_gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListMap == null) { _gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListMap = new LinkedHashMap<String, TColorSetInfoGtCQ>(); }
            String key = "subQueryMapKey" + (_gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListMap.size() + 1);
            _gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListMap.put(key, subQuery); return "GtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtList." + key;
        }
        protected Map<String, Object> _gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameterMap;
        public Map<String, Object> GtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameter { get { return _gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameterMap; } }
        public override String keepGtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameter(Object parameterValue) {
            if (_gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameterMap == null) { _gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameterMap.size() + 1);
            _gtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameterMap.put(key, parameterValue); return "GtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameter." + key;
        }

        public BsTGtScenarioItemCQ AddOrderBy_GtScenarioItemId_Asc() { regOBA("GT_SCENARIO_ITEM_ID");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_GtScenarioItemId_Desc() { regOBD("GT_SCENARIO_ITEM_ID");return this; }

        protected ConditionValue _scenarioTotalizationId;
        public ConditionValue ScenarioTotalizationId {
            get { if (_scenarioTotalizationId == null) { _scenarioTotalizationId = new ConditionValue(); } return _scenarioTotalizationId; }
        }
        protected override ConditionValue getCValueScenarioTotalizationId() { return this.ScenarioTotalizationId; }


        protected Map<String, TScenarioTotalizationCQ> _scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap;
        public Map<String, TScenarioTotalizationCQ> ScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization { get { return _scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap; }}
        public override String keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            if (_scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap == null) { _scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap.size() + 1);
            _scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap.put(key, subQuery); return "ScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap;
        public Map<String, TScenarioTotalizationCQ> ScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization { get { return _scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap; }}
        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            if (_scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap == null) { _scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap.size() + 1);
            _scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap.put(key, subQuery); return "ScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization." + key;
        }

        public BsTGtScenarioItemCQ AddOrderBy_ScenarioTotalizationId_Asc() { regOBA("SCENARIO_TOTALIZATION_ID");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_ScenarioTotalizationId_Desc() { regOBD("SCENARIO_TOTALIZATION_ID");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTGtScenarioItemCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _itemInfoId;
        public ConditionValue ItemInfoId {
            get { if (_itemInfoId == null) { _itemInfoId = new ConditionValue(); } return _itemInfoId; }
        }
        protected override ConditionValue getCValueItemInfoId() { return this.ItemInfoId; }


        protected Map<String, TItemInfoCQ> _itemInfoId_InScopeSubQuery_TItemInfoMap;
        public Map<String, TItemInfoCQ> ItemInfoId_InScopeSubQuery_TItemInfo { get { return _itemInfoId_InScopeSubQuery_TItemInfoMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TItemInfoMap == null) { _itemInfoId_InScopeSubQuery_TItemInfoMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TItemInfoMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TItemInfoMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TItemInfo." + key;
        }

        protected Map<String, TItemInfoCQ> _itemInfoId_NotInScopeSubQuery_TItemInfoMap;
        public Map<String, TItemInfoCQ> ItemInfoId_NotInScopeSubQuery_TItemInfo { get { return _itemInfoId_NotInScopeSubQuery_TItemInfoMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TItemInfoMap == null) { _itemInfoId_NotInScopeSubQuery_TItemInfoMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TItemInfoMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TItemInfoMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TItemInfo." + key;
        }

        public BsTGtScenarioItemCQ AddOrderBy_ItemInfoId_Asc() { regOBA("ITEM_INFO_ID");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_ItemInfoId_Desc() { regOBD("ITEM_INFO_ID");return this; }

        protected ConditionValue _scenarioName;
        public ConditionValue ScenarioName {
            get { if (_scenarioName == null) { _scenarioName = new ConditionValue(); } return _scenarioName; }
        }
        protected override ConditionValue getCValueScenarioName() { return this.ScenarioName; }


        public BsTGtScenarioItemCQ AddOrderBy_ScenarioName_Asc() { regOBA("SCENARIO_NAME");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_ScenarioName_Desc() { regOBD("SCENARIO_NAME");return this; }

        protected ConditionValue _graphType;
        public ConditionValue GraphType {
            get { if (_graphType == null) { _graphType = new ConditionValue(); } return _graphType; }
        }
        protected override ConditionValue getCValueGraphType() { return this.GraphType; }


        public BsTGtScenarioItemCQ AddOrderBy_GraphType_Asc() { regOBA("GRAPH_TYPE");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_GraphType_Desc() { regOBD("GRAPH_TYPE");return this; }

        protected ConditionValue _reportType;
        public ConditionValue ReportType {
            get { if (_reportType == null) { _reportType = new ConditionValue(); } return _reportType; }
        }
        protected override ConditionValue getCValueReportType() { return this.ReportType; }


        public BsTGtScenarioItemCQ AddOrderBy_ReportType_Asc() { regOBA("REPORT_TYPE");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_ReportType_Desc() { regOBD("REPORT_TYPE");return this; }

        protected ConditionValue _viewItemString;
        public ConditionValue ViewItemString {
            get { if (_viewItemString == null) { _viewItemString = new ConditionValue(); } return _viewItemString; }
        }
        protected override ConditionValue getCValueViewItemString() { return this.ViewItemString; }


        public BsTGtScenarioItemCQ AddOrderBy_ViewItemString_Asc() { regOBA("VIEW_ITEM_STRING");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_ViewItemString_Desc() { regOBD("VIEW_ITEM_STRING");return this; }

        protected ConditionValue _scenarioComment;
        public ConditionValue ScenarioComment {
            get { if (_scenarioComment == null) { _scenarioComment = new ConditionValue(); } return _scenarioComment; }
        }
        protected override ConditionValue getCValueScenarioComment() { return this.ScenarioComment; }


        public BsTGtScenarioItemCQ AddOrderBy_ScenarioComment_Asc() { regOBA("SCENARIO_COMMENT");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_ScenarioComment_Desc() { regOBD("SCENARIO_COMMENT");return this; }

        protected ConditionValue _surveyType;
        public ConditionValue SurveyType {
            get { if (_surveyType == null) { _surveyType = new ConditionValue(); } return _surveyType; }
        }
        protected override ConditionValue getCValueSurveyType() { return this.SurveyType; }


        public BsTGtScenarioItemCQ AddOrderBy_SurveyType_Asc() { regOBA("SURVEY_TYPE");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_SurveyType_Desc() { regOBD("SURVEY_TYPE");return this; }

        protected ConditionValue _graphTypeReport;
        public ConditionValue GraphTypeReport {
            get { if (_graphTypeReport == null) { _graphTypeReport = new ConditionValue(); } return _graphTypeReport; }
        }
        protected override ConditionValue getCValueGraphTypeReport() { return this.GraphTypeReport; }


        public BsTGtScenarioItemCQ AddOrderBy_GraphTypeReport_Asc() { regOBA("GRAPH_TYPE_REPORT");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_GraphTypeReport_Desc() { regOBD("GRAPH_TYPE_REPORT");return this; }

        protected ConditionValue _testTargetType;
        public ConditionValue TestTargetType {
            get { if (_testTargetType == null) { _testTargetType = new ConditionValue(); } return _testTargetType; }
        }
        protected override ConditionValue getCValueTestTargetType() { return this.TestTargetType; }


        public BsTGtScenarioItemCQ AddOrderBy_TestTargetType_Asc() { regOBA("TEST_TARGET_TYPE");return this; }
        public BsTGtScenarioItemCQ AddOrderBy_TestTargetType_Desc() { regOBD("TEST_TARGET_TYPE");return this; }

        public BsTGtScenarioItemCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTGtScenarioItemCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TGtScenarioItemCQ baseQuery = (TGtScenarioItemCQ)baseQueryAsSuper;
            TGtScenarioItemCQ unionQuery = (TGtScenarioItemCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTScenarioTotalization()) {
                unionQuery.QueryTScenarioTotalization().reflectRelationOnUnionQuery(baseQuery.QueryTScenarioTotalization(), unionQuery.QueryTScenarioTotalization());
            }
            if (baseQuery.hasConditionQueryTItemInfo()) {
                unionQuery.QueryTItemInfo().reflectRelationOnUnionQuery(baseQuery.QueryTItemInfo(), unionQuery.QueryTItemInfo());
            }

        }
    
        protected TScenarioTotalizationCQ _conditionQueryTScenarioTotalization;
        public TScenarioTotalizationCQ QueryTScenarioTotalization() {
            return this.ConditionQueryTScenarioTotalization;
        }
        public TScenarioTotalizationCQ ConditionQueryTScenarioTotalization {
            get {
                if (_conditionQueryTScenarioTotalization == null) {
                    _conditionQueryTScenarioTotalization = xcreateQueryTScenarioTotalization();
                    xsetupOuterJoin_TScenarioTotalization();
                }
                return _conditionQueryTScenarioTotalization;
            }
        }
        protected TScenarioTotalizationCQ xcreateQueryTScenarioTotalization() {
            String nrp = resolveNextRelationPathTScenarioTotalization();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TScenarioTotalizationCQ cq = new TScenarioTotalizationCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tScenarioTotalization"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TScenarioTotalization() {
            TScenarioTotalizationCQ cq = ConditionQueryTScenarioTotalization;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTScenarioTotalization() {
            return resolveNextRelationPath("T_GT_SCENARIO_ITEM", "tScenarioTotalization");
        }
        public bool hasConditionQueryTScenarioTotalization() {
            return _conditionQueryTScenarioTotalization != null;
        }
        protected TItemInfoCQ _conditionQueryTItemInfo;
        public TItemInfoCQ QueryTItemInfo() {
            return this.ConditionQueryTItemInfo;
        }
        public TItemInfoCQ ConditionQueryTItemInfo {
            get {
                if (_conditionQueryTItemInfo == null) {
                    _conditionQueryTItemInfo = xcreateQueryTItemInfo();
                    xsetupOuterJoin_TItemInfo();
                }
                return _conditionQueryTItemInfo;
            }
        }
        protected TItemInfoCQ xcreateQueryTItemInfo() {
            String nrp = resolveNextRelationPathTItemInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TItemInfoCQ cq = new TItemInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tItemInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TItemInfo() {
            TItemInfoCQ cq = ConditionQueryTItemInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("ITEM_INFO_ID", "ITEM_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTItemInfo() {
            return resolveNextRelationPath("T_GT_SCENARIO_ITEM", "tItemInfo");
        }
        public bool hasConditionQueryTItemInfo() {
            return _conditionQueryTItemInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TGtScenarioItemCQ> _scalarSubQueryMap;
	    public Map<String, TGtScenarioItemCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TGtScenarioItemCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TGtScenarioItemCQ> _myselfInScopeSubQueryMap;
        public Map<String, TGtScenarioItemCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TGtScenarioItemCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
