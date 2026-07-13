
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTCategoryOutputEditCQ : AbstractBsTCategoryOutputEditCQ {

        protected TCategoryOutputEditCIQ _inlineQuery;

        public BsTCategoryOutputEditCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TCategoryOutputEditCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TCategoryOutputEditCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TCategoryOutputEditCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TCategoryOutputEditCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _categoryOutputEditId;
        public ConditionValue CategoryOutputEditId {
            get { if (_categoryOutputEditId == null) { _categoryOutputEditId = new ConditionValue(); } return _categoryOutputEditId; }
        }
        protected override ConditionValue getCValueCategoryOutputEditId() { return this.CategoryOutputEditId; }


        protected Map<String, TCategoryOutputDetailCQ> _categoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailListMap;
        public Map<String, TCategoryOutputDetailCQ> CategoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailList { get { return _categoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailListMap; }}
        public override String keepCategoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            if (_categoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailListMap == null) { _categoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailListMap = new LinkedHashMap<String, TCategoryOutputDetailCQ>(); }
            String key = "subQueryMapKey" + (_categoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailListMap.size() + 1);
            _categoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailListMap.put(key, subQuery); return "CategoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailList." + key;
        }

        protected Map<String, TCategoryOutputDetailCQ> _categoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailListMap;
        public Map<String, TCategoryOutputDetailCQ> CategoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailList { get { return _categoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailListMap; }}
        public override String keepCategoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            if (_categoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailListMap == null) { _categoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailListMap = new LinkedHashMap<String, TCategoryOutputDetailCQ>(); }
            String key = "subQueryMapKey" + (_categoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailListMap.size() + 1);
            _categoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailListMap.put(key, subQuery); return "CategoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailList." + key;
        }

        protected Map<String, TCategoryOutputDetailCQ> _categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailMap;
        public Map<String, TCategoryOutputDetailCQ> CategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetail { get { return _categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailMap; }}
        public override String keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetail(TCategoryOutputDetailCQ subQuery) {
            if (_categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailMap == null) { _categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailMap = new LinkedHashMap<String, TCategoryOutputDetailCQ>(); }
            String key = "subQueryMapKey" + (_categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailMap.size() + 1);
            _categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailMap.put(key, subQuery); return "CategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetail." + key;
        }

        protected Map<String, TCategoryOutputDetailCQ> _categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailListMap;
        public Map<String, TCategoryOutputDetailCQ> CategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailList { get { return _categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailListMap; }}
        public override String keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            if (_categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailListMap == null) { _categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailListMap = new LinkedHashMap<String, TCategoryOutputDetailCQ>(); }
            String key = "subQueryMapKey" + (_categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailListMap.size() + 1);
            _categoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailListMap.put(key, subQuery); return "CategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailList." + key;
        }

        protected Map<String, TCategoryOutputDetailCQ> _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailMap;
        public Map<String, TCategoryOutputDetailCQ> CategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetail { get { return _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailMap; }}
        public override String keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetail(TCategoryOutputDetailCQ subQuery) {
            if (_categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailMap == null) { _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailMap = new LinkedHashMap<String, TCategoryOutputDetailCQ>(); }
            String key = "subQueryMapKey" + (_categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailMap.size() + 1);
            _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailMap.put(key, subQuery); return "CategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetail." + key;
        }

        protected Map<String, TCategoryOutputDetailCQ> _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailListMap;
        public Map<String, TCategoryOutputDetailCQ> CategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailList { get { return _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailListMap; }}
        public override String keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            if (_categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailListMap == null) { _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailListMap = new LinkedHashMap<String, TCategoryOutputDetailCQ>(); }
            String key = "subQueryMapKey" + (_categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailListMap.size() + 1);
            _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailListMap.put(key, subQuery); return "CategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailList." + key;
        }

        protected Map<String, TCategoryOutputDetailCQ> _categoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailListMap;
        public Map<String, TCategoryOutputDetailCQ> CategoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailList { get { return _categoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailListMap; }}
        public override String keepCategoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            if (_categoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailListMap == null) { _categoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailListMap = new LinkedHashMap<String, TCategoryOutputDetailCQ>(); }
            String key = "subQueryMapKey" + (_categoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailListMap.size() + 1);
            _categoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailListMap.put(key, subQuery); return "CategoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailList." + key;
        }

        protected Map<String, TCategoryOutputDetailCQ> _categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListMap;
        public Map<String, TCategoryOutputDetailCQ> CategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailList { get { return _categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListMap; } }
        public override String keepCategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            if (_categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListMap == null) { _categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListMap = new LinkedHashMap<String, TCategoryOutputDetailCQ>(); }
            String key = "subQueryMapKey" + (_categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListMap.size() + 1);
            _categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListMap.put(key, subQuery); return "CategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailList." + key;
        }
        protected Map<String, Object> _categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameterMap;
        public Map<String, Object> CategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameter { get { return _categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameterMap; } }
        public override String keepCategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameter(Object parameterValue) {
            if (_categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameterMap == null) { _categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameterMap.size() + 1);
            _categoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameterMap.put(key, parameterValue); return "CategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameter." + key;
        }

        public BsTCategoryOutputEditCQ AddOrderBy_CategoryOutputEditId_Asc() { regOBA("CATEGORY_OUTPUT_EDIT_ID");return this; }
        public BsTCategoryOutputEditCQ AddOrderBy_CategoryOutputEditId_Desc() { regOBD("CATEGORY_OUTPUT_EDIT_ID");return this; }

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

        public BsTCategoryOutputEditCQ AddOrderBy_ScenarioTotalizationId_Asc() { regOBA("SCENARIO_TOTALIZATION_ID");return this; }
        public BsTCategoryOutputEditCQ AddOrderBy_ScenarioTotalizationId_Desc() { regOBD("SCENARIO_TOTALIZATION_ID");return this; }

        protected ConditionValue _oldItemId;
        public ConditionValue OldItemId {
            get { if (_oldItemId == null) { _oldItemId = new ConditionValue(); } return _oldItemId; }
        }
        protected override ConditionValue getCValueOldItemId() { return this.OldItemId; }


        public BsTCategoryOutputEditCQ AddOrderBy_OldItemId_Asc() { regOBA("OLD_ITEM_ID");return this; }
        public BsTCategoryOutputEditCQ AddOrderBy_OldItemId_Desc() { regOBD("OLD_ITEM_ID");return this; }

        protected ConditionValue _newItemId;
        public ConditionValue NewItemId {
            get { if (_newItemId == null) { _newItemId = new ConditionValue(); } return _newItemId; }
        }
        protected override ConditionValue getCValueNewItemId() { return this.NewItemId; }


        public BsTCategoryOutputEditCQ AddOrderBy_NewItemId_Asc() { regOBA("NEW_ITEM_ID");return this; }
        public BsTCategoryOutputEditCQ AddOrderBy_NewItemId_Desc() { regOBD("NEW_ITEM_ID");return this; }

        protected ConditionValue _topFlag;
        public ConditionValue TopFlag {
            get { if (_topFlag == null) { _topFlag = new ConditionValue(); } return _topFlag; }
        }
        protected override ConditionValue getCValueTopFlag() { return this.TopFlag; }


        public BsTCategoryOutputEditCQ AddOrderBy_TopFlag_Asc() { regOBA("TOP_FLAG");return this; }
        public BsTCategoryOutputEditCQ AddOrderBy_TopFlag_Desc() { regOBD("TOP_FLAG");return this; }

        protected ConditionValue _topCount;
        public ConditionValue TopCount {
            get { if (_topCount == null) { _topCount = new ConditionValue(); } return _topCount; }
        }
        protected override ConditionValue getCValueTopCount() { return this.TopCount; }


        public BsTCategoryOutputEditCQ AddOrderBy_TopCount_Asc() { regOBA("TOP_COUNT");return this; }
        public BsTCategoryOutputEditCQ AddOrderBy_TopCount_Desc() { regOBD("TOP_COUNT");return this; }

        protected ConditionValue _topName;
        public ConditionValue TopName {
            get { if (_topName == null) { _topName = new ConditionValue(); } return _topName; }
        }
        protected override ConditionValue getCValueTopName() { return this.TopName; }


        public BsTCategoryOutputEditCQ AddOrderBy_TopName_Asc() { regOBA("TOP_NAME");return this; }
        public BsTCategoryOutputEditCQ AddOrderBy_TopName_Desc() { regOBD("TOP_NAME");return this; }

        protected ConditionValue _bottomFlag;
        public ConditionValue BottomFlag {
            get { if (_bottomFlag == null) { _bottomFlag = new ConditionValue(); } return _bottomFlag; }
        }
        protected override ConditionValue getCValueBottomFlag() { return this.BottomFlag; }


        public BsTCategoryOutputEditCQ AddOrderBy_BottomFlag_Asc() { regOBA("BOTTOM_FLAG");return this; }
        public BsTCategoryOutputEditCQ AddOrderBy_BottomFlag_Desc() { regOBD("BOTTOM_FLAG");return this; }

        protected ConditionValue _bottomCount;
        public ConditionValue BottomCount {
            get { if (_bottomCount == null) { _bottomCount = new ConditionValue(); } return _bottomCount; }
        }
        protected override ConditionValue getCValueBottomCount() { return this.BottomCount; }


        public BsTCategoryOutputEditCQ AddOrderBy_BottomCount_Asc() { regOBA("BOTTOM_COUNT");return this; }
        public BsTCategoryOutputEditCQ AddOrderBy_BottomCount_Desc() { regOBD("BOTTOM_COUNT");return this; }

        protected ConditionValue _bottomName;
        public ConditionValue BottomName {
            get { if (_bottomName == null) { _bottomName = new ConditionValue(); } return _bottomName; }
        }
        protected override ConditionValue getCValueBottomName() { return this.BottomName; }


        public BsTCategoryOutputEditCQ AddOrderBy_BottomName_Asc() { regOBA("BOTTOM_NAME");return this; }
        public BsTCategoryOutputEditCQ AddOrderBy_BottomName_Desc() { regOBD("BOTTOM_NAME");return this; }

        public BsTCategoryOutputEditCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTCategoryOutputEditCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TCategoryOutputEditCQ baseQuery = (TCategoryOutputEditCQ)baseQueryAsSuper;
            TCategoryOutputEditCQ unionQuery = (TCategoryOutputEditCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTScenarioTotalization()) {
                unionQuery.QueryTScenarioTotalization().reflectRelationOnUnionQuery(baseQuery.QueryTScenarioTotalization(), unionQuery.QueryTScenarioTotalization());
            }
            if (baseQuery.hasConditionQueryTCategoryOutputDetail()) {
                unionQuery.QueryTCategoryOutputDetail().reflectRelationOnUnionQuery(baseQuery.QueryTCategoryOutputDetail(), unionQuery.QueryTCategoryOutputDetail());
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
            return resolveNextRelationPath("T_CATEGORY_OUTPUT_EDIT", "tScenarioTotalization");
        }
        public bool hasConditionQueryTScenarioTotalization() {
            return _conditionQueryTScenarioTotalization != null;
        }
        protected TCategoryOutputDetailCQ _conditionQueryTCategoryOutputDetail;
        public TCategoryOutputDetailCQ QueryTCategoryOutputDetail() {
            return this.ConditionQueryTCategoryOutputDetail;
        }
        public TCategoryOutputDetailCQ ConditionQueryTCategoryOutputDetail {
            get {
                if (_conditionQueryTCategoryOutputDetail == null) {
                    _conditionQueryTCategoryOutputDetail = xcreateQueryTCategoryOutputDetail();
                    xsetupOuterJoin_TCategoryOutputDetail();
                }
                return _conditionQueryTCategoryOutputDetail;
            }
        }
        protected TCategoryOutputDetailCQ xcreateQueryTCategoryOutputDetail() {
            String nrp = resolveNextRelationPathTCategoryOutputDetail();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TCategoryOutputDetailCQ cq = new TCategoryOutputDetailCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tCategoryOutputDetail"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TCategoryOutputDetail() {
            TCategoryOutputDetailCQ cq = ConditionQueryTCategoryOutputDetail;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("CATEGORY_OUTPUT_EDIT_ID", "Category_Output_Edit_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTCategoryOutputDetail() {
            return resolveNextRelationPath("T_CATEGORY_OUTPUT_EDIT", "tCategoryOutputDetail");
        }
        public bool hasConditionQueryTCategoryOutputDetail() {
            return _conditionQueryTCategoryOutputDetail != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TCategoryOutputEditCQ> _scalarSubQueryMap;
	    public Map<String, TCategoryOutputEditCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TCategoryOutputEditCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TCategoryOutputEditCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TCategoryOutputEditCQ> _myselfInScopeSubQueryMap;
        public Map<String, TCategoryOutputEditCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TCategoryOutputEditCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TCategoryOutputEditCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
