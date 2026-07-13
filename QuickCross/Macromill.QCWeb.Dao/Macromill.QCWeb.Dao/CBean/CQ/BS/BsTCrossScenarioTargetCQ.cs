
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTCrossScenarioTargetCQ : AbstractBsTCrossScenarioTargetCQ {

        protected TCrossScenarioTargetCIQ _inlineQuery;

        public BsTCrossScenarioTargetCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TCrossScenarioTargetCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TCrossScenarioTargetCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TCrossScenarioTargetCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TCrossScenarioTargetCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _crossScenarioTargetId;
        public ConditionValue CrossScenarioTargetId {
            get { if (_crossScenarioTargetId == null) { _crossScenarioTargetId = new ConditionValue(); } return _crossScenarioTargetId; }
        }
        protected override ConditionValue getCValueCrossScenarioTargetId() { return this.CrossScenarioTargetId; }


        protected Map<String, TColorSetInfoCrossCQ> _crossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossListMap;
        public Map<String, TColorSetInfoCrossCQ> CrossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossList { get { return _crossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossListMap; }}
        public override String keepCrossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            if (_crossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossListMap == null) { _crossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossListMap = new LinkedHashMap<String, TColorSetInfoCrossCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossListMap.size() + 1);
            _crossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossListMap.put(key, subQuery); return "CrossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossList." + key;
        }

        protected Map<String, TCrossScenarioItemCQ> _crossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemListMap;
        public Map<String, TCrossScenarioItemCQ> CrossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemList { get { return _crossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemListMap; }}
        public override String keepCrossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            if (_crossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemListMap == null) { _crossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemListMap = new LinkedHashMap<String, TCrossScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemListMap.size() + 1);
            _crossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemListMap.put(key, subQuery); return "CrossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemList." + key;
        }

        protected Map<String, TColorSetInfoCrossCQ> _crossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossListMap;
        public Map<String, TColorSetInfoCrossCQ> CrossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossList { get { return _crossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossListMap; }}
        public override String keepCrossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            if (_crossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossListMap == null) { _crossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossListMap = new LinkedHashMap<String, TColorSetInfoCrossCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossListMap.size() + 1);
            _crossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossListMap.put(key, subQuery); return "CrossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossList." + key;
        }

        protected Map<String, TCrossScenarioItemCQ> _crossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemListMap;
        public Map<String, TCrossScenarioItemCQ> CrossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemList { get { return _crossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemListMap; }}
        public override String keepCrossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            if (_crossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemListMap == null) { _crossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemListMap = new LinkedHashMap<String, TCrossScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemListMap.size() + 1);
            _crossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemListMap.put(key, subQuery); return "CrossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemList." + key;
        }

        protected Map<String, TColorSetInfoCrossCQ> _crossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossListMap;
        public Map<String, TColorSetInfoCrossCQ> CrossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossList { get { return _crossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossListMap; }}
        public override String keepCrossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            if (_crossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossListMap == null) { _crossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossListMap = new LinkedHashMap<String, TColorSetInfoCrossCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossListMap.size() + 1);
            _crossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossListMap.put(key, subQuery); return "CrossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossList." + key;
        }

        protected Map<String, TCrossScenarioItemCQ> _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemListMap;
        public Map<String, TCrossScenarioItemCQ> CrossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemList { get { return _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemListMap; }}
        public override String keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            if (_crossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemListMap == null) { _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemListMap = new LinkedHashMap<String, TCrossScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemListMap.size() + 1);
            _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemListMap.put(key, subQuery); return "CrossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemList." + key;
        }

        protected Map<String, TColorSetInfoCrossCQ> _crossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossListMap;
        public Map<String, TColorSetInfoCrossCQ> CrossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossList { get { return _crossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossListMap; }}
        public override String keepCrossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            if (_crossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossListMap == null) { _crossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossListMap = new LinkedHashMap<String, TColorSetInfoCrossCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossListMap.size() + 1);
            _crossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossListMap.put(key, subQuery); return "CrossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossList." + key;
        }

        protected Map<String, TCrossScenarioItemCQ> _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemListMap;
        public Map<String, TCrossScenarioItemCQ> CrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemList { get { return _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemListMap; }}
        public override String keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            if (_crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemListMap == null) { _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemListMap = new LinkedHashMap<String, TCrossScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemListMap.size() + 1);
            _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemListMap.put(key, subQuery); return "CrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemList." + key;
        }

        protected Map<String, TColorSetInfoCrossCQ> _crossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossListMap;
        public Map<String, TColorSetInfoCrossCQ> CrossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossList { get { return _crossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossListMap; }}
        public override String keepCrossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            if (_crossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossListMap == null) { _crossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossListMap = new LinkedHashMap<String, TColorSetInfoCrossCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossListMap.size() + 1);
            _crossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossListMap.put(key, subQuery); return "CrossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossList." + key;
        }

        protected Map<String, TCrossScenarioItemCQ> _crossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemListMap;
        public Map<String, TCrossScenarioItemCQ> CrossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemList { get { return _crossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemListMap; }}
        public override String keepCrossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            if (_crossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemListMap == null) { _crossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemListMap = new LinkedHashMap<String, TCrossScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemListMap.size() + 1);
            _crossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemListMap.put(key, subQuery); return "CrossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemList." + key;
        }

        protected Map<String, TColorSetInfoCrossCQ> _crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListMap;
        public Map<String, TColorSetInfoCrossCQ> CrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossList { get { return _crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListMap; } }
        public override String keepCrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            if (_crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListMap == null) { _crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListMap = new LinkedHashMap<String, TColorSetInfoCrossCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListMap.size() + 1);
            _crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListMap.put(key, subQuery); return "CrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossList." + key;
        }
        protected Map<String, Object> _crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameterMap;
        public Map<String, Object> CrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameter { get { return _crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameterMap; } }
        public override String keepCrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameter(Object parameterValue) {
            if (_crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameterMap == null) { _crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameterMap.size() + 1);
            _crossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameterMap.put(key, parameterValue); return "CrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameter." + key;
        }

        protected Map<String, TCrossScenarioItemCQ> _crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListMap;
        public Map<String, TCrossScenarioItemCQ> CrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemList { get { return _crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListMap; } }
        public override String keepCrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            if (_crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListMap == null) { _crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListMap = new LinkedHashMap<String, TCrossScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListMap.size() + 1);
            _crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListMap.put(key, subQuery); return "CrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemList." + key;
        }
        protected Map<String, Object> _crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameterMap;
        public Map<String, Object> CrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameter { get { return _crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameterMap; } }
        public override String keepCrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameter(Object parameterValue) {
            if (_crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameterMap == null) { _crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameterMap.size() + 1);
            _crossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameterMap.put(key, parameterValue); return "CrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameter." + key;
        }

        public BsTCrossScenarioTargetCQ AddOrderBy_CrossScenarioTargetId_Asc() { regOBA("CROSS_SCENARIO_TARGET_ID");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_CrossScenarioTargetId_Desc() { regOBD("CROSS_SCENARIO_TARGET_ID");return this; }

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

        public BsTCrossScenarioTargetCQ AddOrderBy_ScenarioTotalizationId_Asc() { regOBA("SCENARIO_TOTALIZATION_ID");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_ScenarioTotalizationId_Desc() { regOBD("SCENARIO_TOTALIZATION_ID");return this; }

        protected ConditionValue _scenariosetNo;
        public ConditionValue ScenariosetNo {
            get { if (_scenariosetNo == null) { _scenariosetNo = new ConditionValue(); } return _scenariosetNo; }
        }
        protected override ConditionValue getCValueScenariosetNo() { return this.ScenariosetNo; }


        public BsTCrossScenarioTargetCQ AddOrderBy_ScenariosetNo_Asc() { regOBA("SCENARIOSET_NO");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_ScenariosetNo_Desc() { regOBD("SCENARIOSET_NO");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTCrossScenarioTargetCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _scItemId;
        public ConditionValue ScItemId {
            get { if (_scItemId == null) { _scItemId = new ConditionValue(); } return _scItemId; }
        }
        protected override ConditionValue getCValueScItemId() { return this.ScItemId; }


        public BsTCrossScenarioTargetCQ AddOrderBy_ScItemId_Asc() { regOBA("SC_ITEM_ID");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_ScItemId_Desc() { regOBD("SC_ITEM_ID");return this; }

        protected ConditionValue _viewName;
        public ConditionValue ViewName {
            get { if (_viewName == null) { _viewName = new ConditionValue(); } return _viewName; }
        }
        protected override ConditionValue getCValueViewName() { return this.ViewName; }


        public BsTCrossScenarioTargetCQ AddOrderBy_ViewName_Asc() { regOBA("VIEW_NAME");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_ViewName_Desc() { regOBD("VIEW_NAME");return this; }

        protected ConditionValue _graphType;
        public ConditionValue GraphType {
            get { if (_graphType == null) { _graphType = new ConditionValue(); } return _graphType; }
        }
        protected override ConditionValue getCValueGraphType() { return this.GraphType; }


        public BsTCrossScenarioTargetCQ AddOrderBy_GraphType_Asc() { regOBA("GRAPH_TYPE");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_GraphType_Desc() { regOBD("GRAPH_TYPE");return this; }

        protected ConditionValue _reportType;
        public ConditionValue ReportType {
            get { if (_reportType == null) { _reportType = new ConditionValue(); } return _reportType; }
        }
        protected override ConditionValue getCValueReportType() { return this.ReportType; }


        public BsTCrossScenarioTargetCQ AddOrderBy_ReportType_Asc() { regOBA("REPORT_TYPE");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_ReportType_Desc() { regOBD("REPORT_TYPE");return this; }

        protected ConditionValue _viewItemString;
        public ConditionValue ViewItemString {
            get { if (_viewItemString == null) { _viewItemString = new ConditionValue(); } return _viewItemString; }
        }
        protected override ConditionValue getCValueViewItemString() { return this.ViewItemString; }


        public BsTCrossScenarioTargetCQ AddOrderBy_ViewItemString_Asc() { regOBA("VIEW_ITEM_STRING");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_ViewItemString_Desc() { regOBD("VIEW_ITEM_STRING");return this; }

        protected ConditionValue _scenarioComment;
        public ConditionValue ScenarioComment {
            get { if (_scenarioComment == null) { _scenarioComment = new ConditionValue(); } return _scenarioComment; }
        }
        protected override ConditionValue getCValueScenarioComment() { return this.ScenarioComment; }


        public BsTCrossScenarioTargetCQ AddOrderBy_ScenarioComment_Asc() { regOBA("SCENARIO_COMMENT");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_ScenarioComment_Desc() { regOBD("SCENARIO_COMMENT");return this; }

        protected ConditionValue _polylineFlag;
        public ConditionValue PolylineFlag {
            get { if (_polylineFlag == null) { _polylineFlag = new ConditionValue(); } return _polylineFlag; }
        }
        protected override ConditionValue getCValuePolylineFlag() { return this.PolylineFlag; }


        public BsTCrossScenarioTargetCQ AddOrderBy_PolylineFlag_Asc() { regOBA("POLYLINE_FLAG");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_PolylineFlag_Desc() { regOBD("POLYLINE_FLAG");return this; }

        protected ConditionValue _graphTypeReport;
        public ConditionValue GraphTypeReport {
            get { if (_graphTypeReport == null) { _graphTypeReport = new ConditionValue(); } return _graphTypeReport; }
        }
        protected override ConditionValue getCValueGraphTypeReport() { return this.GraphTypeReport; }


        public BsTCrossScenarioTargetCQ AddOrderBy_GraphTypeReport_Asc() { regOBA("GRAPH_TYPE_REPORT");return this; }
        public BsTCrossScenarioTargetCQ AddOrderBy_GraphTypeReport_Desc() { regOBD("GRAPH_TYPE_REPORT");return this; }

        public BsTCrossScenarioTargetCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTCrossScenarioTargetCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TCrossScenarioTargetCQ baseQuery = (TCrossScenarioTargetCQ)baseQueryAsSuper;
            TCrossScenarioTargetCQ unionQuery = (TCrossScenarioTargetCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTScenarioTotalization()) {
                unionQuery.QueryTScenarioTotalization().reflectRelationOnUnionQuery(baseQuery.QueryTScenarioTotalization(), unionQuery.QueryTScenarioTotalization());
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
            return resolveNextRelationPath("T_CROSS_SCENARIO_TARGET", "tScenarioTotalization");
        }
        public bool hasConditionQueryTScenarioTotalization() {
            return _conditionQueryTScenarioTotalization != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TCrossScenarioTargetCQ> _scalarSubQueryMap;
	    public Map<String, TCrossScenarioTargetCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TCrossScenarioTargetCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TCrossScenarioTargetCQ> _myselfInScopeSubQueryMap;
        public Map<String, TCrossScenarioTargetCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TCrossScenarioTargetCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
