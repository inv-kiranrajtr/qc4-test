
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTGtMatrixInfoCQ : AbstractBsTGtMatrixInfoCQ {

        protected TGtMatrixInfoCIQ _inlineQuery;

        public BsTGtMatrixInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TGtMatrixInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TGtMatrixInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TGtMatrixInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TGtMatrixInfoCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _gtMatrixInfoId;
        public ConditionValue GtMatrixInfoId {
            get { if (_gtMatrixInfoId == null) { _gtMatrixInfoId = new ConditionValue(); } return _gtMatrixInfoId; }
        }
        protected override ConditionValue getCValueGtMatrixInfoId() { return this.GtMatrixInfoId; }


        protected Map<String, TGtMatrixChildCQ> _gtMatrixInfoId_ExistsSubQuery_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> GtMatrixInfoId_ExistsSubQuery_TGtMatrixChildList { get { return _gtMatrixInfoId_ExistsSubQuery_TGtMatrixChildListMap; }}
        public override String keepGtMatrixInfoId_ExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_gtMatrixInfoId_ExistsSubQuery_TGtMatrixChildListMap == null) { _gtMatrixInfoId_ExistsSubQuery_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_gtMatrixInfoId_ExistsSubQuery_TGtMatrixChildListMap.size() + 1);
            _gtMatrixInfoId_ExistsSubQuery_TGtMatrixChildListMap.put(key, subQuery); return "GtMatrixInfoId_ExistsSubQuery_TGtMatrixChildList." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _gtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> GtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildList { get { return _gtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildListMap; }}
        public override String keepGtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_gtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildListMap == null) { _gtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_gtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildListMap.size() + 1);
            _gtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildListMap.put(key, subQuery); return "GtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildList." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildMap;
        public Map<String, TGtMatrixChildCQ> GtMatrixInfoId_InScopeSubQuery_TGtMatrixChild { get { return _gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildMap; }}
        public override String keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixChild(TGtMatrixChildCQ subQuery) {
            if (_gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildMap == null) { _gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildMap.size() + 1);
            _gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildMap.put(key, subQuery); return "GtMatrixInfoId_InScopeSubQuery_TGtMatrixChild." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> GtMatrixInfoId_InScopeSubQuery_TGtMatrixChildList { get { return _gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildListMap; }}
        public override String keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildListMap == null) { _gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildListMap.size() + 1);
            _gtMatrixInfoId_InScopeSubQuery_TGtMatrixChildListMap.put(key, subQuery); return "GtMatrixInfoId_InScopeSubQuery_TGtMatrixChildList." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildMap;
        public Map<String, TGtMatrixChildCQ> GtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChild { get { return _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildMap; }}
        public override String keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChild(TGtMatrixChildCQ subQuery) {
            if (_gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildMap == null) { _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildMap.size() + 1);
            _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildMap.put(key, subQuery); return "GtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChild." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> GtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildList { get { return _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildListMap; }}
        public override String keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildListMap == null) { _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildListMap.size() + 1);
            _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildListMap.put(key, subQuery); return "GtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildList." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _gtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> GtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildList { get { return _gtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap; }}
        public override String keepGtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_gtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap == null) { _gtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_gtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap.size() + 1);
            _gtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap.put(key, subQuery); return "GtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildList." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> GtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildList { get { return _gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListMap; } }
        public override String keepGtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListMap == null) { _gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListMap.size() + 1);
            _gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListMap.put(key, subQuery); return "GtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildList." + key;
        }
        protected Map<String, Object> _gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap;
        public Map<String, Object> GtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter { get { return _gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap; } }
        public override String keepGtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter(Object parameterValue) {
            if (_gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap == null) { _gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap.size() + 1);
            _gtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap.put(key, parameterValue); return "GtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter." + key;
        }

        public BsTGtMatrixInfoCQ AddOrderBy_GtMatrixInfoId_Asc() { regOBA("GT_MATRIX_INFO_ID");return this; }
        public BsTGtMatrixInfoCQ AddOrderBy_GtMatrixInfoId_Desc() { regOBD("GT_MATRIX_INFO_ID");return this; }

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

        public BsTGtMatrixInfoCQ AddOrderBy_ScenarioTotalizationId_Asc() { regOBA("SCENARIO_TOTALIZATION_ID");return this; }
        public BsTGtMatrixInfoCQ AddOrderBy_ScenarioTotalizationId_Desc() { regOBD("SCENARIO_TOTALIZATION_ID");return this; }

        protected ConditionValue _baseItemId;
        public ConditionValue BaseItemId {
            get { if (_baseItemId == null) { _baseItemId = new ConditionValue(); } return _baseItemId; }
        }
        protected override ConditionValue getCValueBaseItemId() { return this.BaseItemId; }


        public BsTGtMatrixInfoCQ AddOrderBy_BaseItemId_Asc() { regOBA("BASE_ITEM_ID");return this; }
        public BsTGtMatrixInfoCQ AddOrderBy_BaseItemId_Desc() { regOBD("BASE_ITEM_ID");return this; }

        protected ConditionValue _newItemId;
        public ConditionValue NewItemId {
            get { if (_newItemId == null) { _newItemId = new ConditionValue(); } return _newItemId; }
        }
        protected override ConditionValue getCValueNewItemId() { return this.NewItemId; }


        public BsTGtMatrixInfoCQ AddOrderBy_NewItemId_Asc() { regOBA("NEW_ITEM_ID");return this; }
        public BsTGtMatrixInfoCQ AddOrderBy_NewItemId_Desc() { regOBD("NEW_ITEM_ID");return this; }

        protected ConditionValue _totalizationType;
        public ConditionValue TotalizationType {
            get { if (_totalizationType == null) { _totalizationType = new ConditionValue(); } return _totalizationType; }
        }
        protected override ConditionValue getCValueTotalizationType() { return this.TotalizationType; }


        public BsTGtMatrixInfoCQ AddOrderBy_TotalizationType_Asc() { regOBA("TOTALIZATION_TYPE");return this; }
        public BsTGtMatrixInfoCQ AddOrderBy_TotalizationType_Desc() { regOBD("TOTALIZATION_TYPE");return this; }

        protected ConditionValue _lv1title;
        public ConditionValue Lv1title {
            get { if (_lv1title == null) { _lv1title = new ConditionValue(); } return _lv1title; }
        }
        protected override ConditionValue getCValueLv1title() { return this.Lv1title; }


        public BsTGtMatrixInfoCQ AddOrderBy_Lv1title_Asc() { regOBA("LV1TITLE");return this; }
        public BsTGtMatrixInfoCQ AddOrderBy_Lv1title_Desc() { regOBD("LV1TITLE");return this; }

        protected ConditionValue _itemName;
        public ConditionValue ItemName {
            get { if (_itemName == null) { _itemName = new ConditionValue(); } return _itemName; }
        }
        protected override ConditionValue getCValueItemName() { return this.ItemName; }


        public BsTGtMatrixInfoCQ AddOrderBy_ItemName_Asc() { regOBA("ITEM_NAME");return this; }
        public BsTGtMatrixInfoCQ AddOrderBy_ItemName_Desc() { regOBD("ITEM_NAME");return this; }

        public BsTGtMatrixInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTGtMatrixInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TGtMatrixInfoCQ baseQuery = (TGtMatrixInfoCQ)baseQueryAsSuper;
            TGtMatrixInfoCQ unionQuery = (TGtMatrixInfoCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTScenarioTotalization()) {
                unionQuery.QueryTScenarioTotalization().reflectRelationOnUnionQuery(baseQuery.QueryTScenarioTotalization(), unionQuery.QueryTScenarioTotalization());
            }
            if (baseQuery.hasConditionQueryTGtMatrixChild()) {
                unionQuery.QueryTGtMatrixChild().reflectRelationOnUnionQuery(baseQuery.QueryTGtMatrixChild(), unionQuery.QueryTGtMatrixChild());
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
            return resolveNextRelationPath("T_GT_MATRIX_INFO", "tScenarioTotalization");
        }
        public bool hasConditionQueryTScenarioTotalization() {
            return _conditionQueryTScenarioTotalization != null;
        }
        protected TGtMatrixChildCQ _conditionQueryTGtMatrixChild;
        public TGtMatrixChildCQ QueryTGtMatrixChild() {
            return this.ConditionQueryTGtMatrixChild;
        }
        public TGtMatrixChildCQ ConditionQueryTGtMatrixChild {
            get {
                if (_conditionQueryTGtMatrixChild == null) {
                    _conditionQueryTGtMatrixChild = xcreateQueryTGtMatrixChild();
                    xsetupOuterJoin_TGtMatrixChild();
                }
                return _conditionQueryTGtMatrixChild;
            }
        }
        protected TGtMatrixChildCQ xcreateQueryTGtMatrixChild() {
            String nrp = resolveNextRelationPathTGtMatrixChild();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TGtMatrixChildCQ cq = new TGtMatrixChildCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tGtMatrixChild"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TGtMatrixChild() {
            TGtMatrixChildCQ cq = ConditionQueryTGtMatrixChild;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("GT_MATRIX_INFO_ID", "GT_Matrix_Info_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTGtMatrixChild() {
            return resolveNextRelationPath("T_GT_MATRIX_INFO", "tGtMatrixChild");
        }
        public bool hasConditionQueryTGtMatrixChild() {
            return _conditionQueryTGtMatrixChild != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TGtMatrixInfoCQ> _scalarSubQueryMap;
	    public Map<String, TGtMatrixInfoCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TGtMatrixInfoCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TGtMatrixInfoCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TGtMatrixInfoCQ> _myselfInScopeSubQueryMap;
        public Map<String, TGtMatrixInfoCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TGtMatrixInfoCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TGtMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
