
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTFaScenarioHeaderCQ : AbstractBsTFaScenarioHeaderCQ {

        protected TFaScenarioHeaderCIQ _inlineQuery;

        public BsTFaScenarioHeaderCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TFaScenarioHeaderCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TFaScenarioHeaderCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TFaScenarioHeaderCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TFaScenarioHeaderCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _faScenarioHeaderId;
        public ConditionValue FaScenarioHeaderId {
            get { if (_faScenarioHeaderId == null) { _faScenarioHeaderId = new ConditionValue(); } return _faScenarioHeaderId; }
        }
        protected override ConditionValue getCValueFaScenarioHeaderId() { return this.FaScenarioHeaderId; }


        protected Map<String, TFaListAddItemCQ> _faScenarioHeaderId_ExistsSubQuery_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> FaScenarioHeaderId_ExistsSubQuery_TFaListAddItemList { get { return _faScenarioHeaderId_ExistsSubQuery_TFaListAddItemListMap; }}
        public override String keepFaScenarioHeaderId_ExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_faScenarioHeaderId_ExistsSubQuery_TFaListAddItemListMap == null) { _faScenarioHeaderId_ExistsSubQuery_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_ExistsSubQuery_TFaListAddItemListMap.size() + 1);
            _faScenarioHeaderId_ExistsSubQuery_TFaListAddItemListMap.put(key, subQuery); return "FaScenarioHeaderId_ExistsSubQuery_TFaListAddItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _faScenarioHeaderId_ExistsSubQuery_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> FaScenarioHeaderId_ExistsSubQuery_TFaScenarioItemList { get { return _faScenarioHeaderId_ExistsSubQuery_TFaScenarioItemListMap; }}
        public override String keepFaScenarioHeaderId_ExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_faScenarioHeaderId_ExistsSubQuery_TFaScenarioItemListMap == null) { _faScenarioHeaderId_ExistsSubQuery_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_ExistsSubQuery_TFaScenarioItemListMap.size() + 1);
            _faScenarioHeaderId_ExistsSubQuery_TFaScenarioItemListMap.put(key, subQuery); return "FaScenarioHeaderId_ExistsSubQuery_TFaScenarioItemList." + key;
        }

        protected Map<String, TFaListAddItemCQ> _faScenarioHeaderId_NotExistsSubQuery_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> FaScenarioHeaderId_NotExistsSubQuery_TFaListAddItemList { get { return _faScenarioHeaderId_NotExistsSubQuery_TFaListAddItemListMap; }}
        public override String keepFaScenarioHeaderId_NotExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_faScenarioHeaderId_NotExistsSubQuery_TFaListAddItemListMap == null) { _faScenarioHeaderId_NotExistsSubQuery_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_NotExistsSubQuery_TFaListAddItemListMap.size() + 1);
            _faScenarioHeaderId_NotExistsSubQuery_TFaListAddItemListMap.put(key, subQuery); return "FaScenarioHeaderId_NotExistsSubQuery_TFaListAddItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _faScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> FaScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemList { get { return _faScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemListMap; }}
        public override String keepFaScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_faScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemListMap == null) { _faScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemListMap.size() + 1);
            _faScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemListMap.put(key, subQuery); return "FaScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemMap;
        public Map<String, TFaScenarioItemCQ> FaScenarioHeaderId_InScopeSubQuery_TFaScenarioItem { get { return _faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemMap; }}
        public override String keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioItem(TFaScenarioItemCQ subQuery) {
            if (_faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemMap == null) { _faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemMap.size() + 1);
            _faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemMap.put(key, subQuery); return "FaScenarioHeaderId_InScopeSubQuery_TFaScenarioItem." + key;
        }

        protected Map<String, TFaListAddItemCQ> _faScenarioHeaderId_InScopeSubQuery_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> FaScenarioHeaderId_InScopeSubQuery_TFaListAddItemList { get { return _faScenarioHeaderId_InScopeSubQuery_TFaListAddItemListMap; }}
        public override String keepFaScenarioHeaderId_InScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_faScenarioHeaderId_InScopeSubQuery_TFaListAddItemListMap == null) { _faScenarioHeaderId_InScopeSubQuery_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_InScopeSubQuery_TFaListAddItemListMap.size() + 1);
            _faScenarioHeaderId_InScopeSubQuery_TFaListAddItemListMap.put(key, subQuery); return "FaScenarioHeaderId_InScopeSubQuery_TFaListAddItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> FaScenarioHeaderId_InScopeSubQuery_TFaScenarioItemList { get { return _faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemListMap; }}
        public override String keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemListMap == null) { _faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemListMap.size() + 1);
            _faScenarioHeaderId_InScopeSubQuery_TFaScenarioItemListMap.put(key, subQuery); return "FaScenarioHeaderId_InScopeSubQuery_TFaScenarioItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemMap;
        public Map<String, TFaScenarioItemCQ> FaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItem { get { return _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemMap; }}
        public override String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItem(TFaScenarioItemCQ subQuery) {
            if (_faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemMap == null) { _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemMap.size() + 1);
            _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemMap.put(key, subQuery); return "FaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItem." + key;
        }

        protected Map<String, TFaListAddItemCQ> _faScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> FaScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemList { get { return _faScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemListMap; }}
        public override String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_faScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemListMap == null) { _faScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemListMap.size() + 1);
            _faScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemListMap.put(key, subQuery); return "FaScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> FaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemList { get { return _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemListMap; }}
        public override String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemListMap == null) { _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemListMap.size() + 1);
            _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemListMap.put(key, subQuery); return "FaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemList." + key;
        }

        protected Map<String, TFaListAddItemCQ> _faScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> FaScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemList { get { return _faScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemListMap; }}
        public override String keepFaScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_faScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemListMap == null) { _faScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemListMap.size() + 1);
            _faScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemListMap.put(key, subQuery); return "FaScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _faScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> FaScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemList { get { return _faScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemListMap; }}
        public override String keepFaScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_faScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemListMap == null) { _faScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemListMap.size() + 1);
            _faScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemListMap.put(key, subQuery); return "FaScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemList." + key;
        }

        protected Map<String, TFaListAddItemCQ> _faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> FaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemList { get { return _faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListMap; } }
        public override String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListMap == null) { _faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListMap.size() + 1);
            _faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListMap.put(key, subQuery); return "FaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemList." + key;
        }
        protected Map<String, Object> _faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameterMap;
        public Map<String, Object> FaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameter { get { return _faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameterMap; } }
        public override String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameter(Object parameterValue) {
            if (_faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameterMap == null) { _faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameterMap.size() + 1);
            _faScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameterMap.put(key, parameterValue); return "FaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameter." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> FaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemList { get { return _faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListMap; } }
        public override String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListMap == null) { _faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListMap.size() + 1);
            _faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListMap.put(key, subQuery); return "FaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemList." + key;
        }
        protected Map<String, Object> _faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameterMap;
        public Map<String, Object> FaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameter { get { return _faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameterMap; } }
        public override String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameter(Object parameterValue) {
            if (_faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameterMap == null) { _faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameterMap.size() + 1);
            _faScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameterMap.put(key, parameterValue); return "FaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameter." + key;
        }

        public BsTFaScenarioHeaderCQ AddOrderBy_FaScenarioHeaderId_Asc() { regOBA("FA_SCENARIO_HEADER_ID");return this; }
        public BsTFaScenarioHeaderCQ AddOrderBy_FaScenarioHeaderId_Desc() { regOBD("FA_SCENARIO_HEADER_ID");return this; }

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

        public BsTFaScenarioHeaderCQ AddOrderBy_ScenarioTotalizationId_Asc() { regOBA("SCENARIO_TOTALIZATION_ID");return this; }
        public BsTFaScenarioHeaderCQ AddOrderBy_ScenarioTotalizationId_Desc() { regOBD("SCENARIO_TOTALIZATION_ID");return this; }

        protected ConditionValue _scenarioComment;
        public ConditionValue ScenarioComment {
            get { if (_scenarioComment == null) { _scenarioComment = new ConditionValue(); } return _scenarioComment; }
        }
        protected override ConditionValue getCValueScenarioComment() { return this.ScenarioComment; }


        public BsTFaScenarioHeaderCQ AddOrderBy_ScenarioComment_Asc() { regOBA("SCENARIO_COMMENT");return this; }
        public BsTFaScenarioHeaderCQ AddOrderBy_ScenarioComment_Desc() { regOBD("SCENARIO_COMMENT");return this; }

        protected ConditionValue _viewName;
        public ConditionValue ViewName {
            get { if (_viewName == null) { _viewName = new ConditionValue(); } return _viewName; }
        }
        protected override ConditionValue getCValueViewName() { return this.ViewName; }


        public BsTFaScenarioHeaderCQ AddOrderBy_ViewName_Asc() { regOBA("VIEW_NAME");return this; }
        public BsTFaScenarioHeaderCQ AddOrderBy_ViewName_Desc() { regOBD("VIEW_NAME");return this; }

        public BsTFaScenarioHeaderCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTFaScenarioHeaderCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TFaScenarioHeaderCQ baseQuery = (TFaScenarioHeaderCQ)baseQueryAsSuper;
            TFaScenarioHeaderCQ unionQuery = (TFaScenarioHeaderCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTScenarioTotalization()) {
                unionQuery.QueryTScenarioTotalization().reflectRelationOnUnionQuery(baseQuery.QueryTScenarioTotalization(), unionQuery.QueryTScenarioTotalization());
            }
            if (baseQuery.hasConditionQueryTFaScenarioItem()) {
                unionQuery.QueryTFaScenarioItem().reflectRelationOnUnionQuery(baseQuery.QueryTFaScenarioItem(), unionQuery.QueryTFaScenarioItem());
            }
            if (baseQuery.hasConditionQueryTFaListAddItem()) {
                unionQuery.QueryTFaListAddItem().reflectRelationOnUnionQuery(baseQuery.QueryTFaListAddItem(), unionQuery.QueryTFaListAddItem());
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
            return resolveNextRelationPath("T_FA_SCENARIO_HEADER", "tScenarioTotalization");
        }
        public bool hasConditionQueryTScenarioTotalization() {
            return _conditionQueryTScenarioTotalization != null;
        }
        protected TFaScenarioItemCQ _conditionQueryTFaScenarioItem;
        public TFaScenarioItemCQ QueryTFaScenarioItem() {
            return this.ConditionQueryTFaScenarioItem;
        }
        public TFaScenarioItemCQ ConditionQueryTFaScenarioItem {
            get {
                if (_conditionQueryTFaScenarioItem == null) {
                    _conditionQueryTFaScenarioItem = xcreateQueryTFaScenarioItem();
                    xsetupOuterJoin_TFaScenarioItem();
                }
                return _conditionQueryTFaScenarioItem;
            }
        }
        protected TFaScenarioItemCQ xcreateQueryTFaScenarioItem() {
            String nrp = resolveNextRelationPathTFaScenarioItem();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TFaScenarioItemCQ cq = new TFaScenarioItemCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tFaScenarioItem"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TFaScenarioItem() {
            TFaScenarioItemCQ cq = ConditionQueryTFaScenarioItem;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("FA_SCENARIO_HEADER_ID", "FA_Scenario_Header_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTFaScenarioItem() {
            return resolveNextRelationPath("T_FA_SCENARIO_HEADER", "tFaScenarioItem");
        }
        public bool hasConditionQueryTFaScenarioItem() {
            return _conditionQueryTFaScenarioItem != null;
        }
        protected TFaListAddItemCQ _conditionQueryTFaListAddItem;
        public TFaListAddItemCQ QueryTFaListAddItem() {
            return this.ConditionQueryTFaListAddItem;
        }
        public TFaListAddItemCQ ConditionQueryTFaListAddItem {
            get {
                if (_conditionQueryTFaListAddItem == null) {
                    _conditionQueryTFaListAddItem = xcreateQueryTFaListAddItem();
                    xsetupOuterJoin_TFaListAddItem();
                }
                return _conditionQueryTFaListAddItem;
            }
        }
        protected TFaListAddItemCQ xcreateQueryTFaListAddItem() {
            String nrp = resolveNextRelationPathTFaListAddItem();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TFaListAddItemCQ cq = new TFaListAddItemCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tFaListAddItem"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TFaListAddItem() {
            TFaListAddItemCQ cq = ConditionQueryTFaListAddItem;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("FA_SCENARIO_HEADER_ID", "FA_Scenario_Header_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTFaListAddItem() {
            return resolveNextRelationPath("T_FA_SCENARIO_HEADER", "tFaListAddItem");
        }
        public bool hasConditionQueryTFaListAddItem() {
            return _conditionQueryTFaListAddItem != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TFaScenarioHeaderCQ> _scalarSubQueryMap;
	    public Map<String, TFaScenarioHeaderCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TFaScenarioHeaderCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TFaScenarioHeaderCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TFaScenarioHeaderCQ> _myselfInScopeSubQueryMap;
        public Map<String, TFaScenarioHeaderCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TFaScenarioHeaderCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TFaScenarioHeaderCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
