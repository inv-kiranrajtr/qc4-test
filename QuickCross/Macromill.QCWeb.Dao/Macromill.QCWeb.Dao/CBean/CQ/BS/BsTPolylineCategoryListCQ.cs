
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTPolylineCategoryListCQ : AbstractBsTPolylineCategoryListCQ {

        protected TPolylineCategoryListCIQ _inlineQuery;

        public BsTPolylineCategoryListCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TPolylineCategoryListCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TPolylineCategoryListCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TPolylineCategoryListCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TPolylineCategoryListCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _polylineCategoryListId;
        public ConditionValue PolylineCategoryListId {
            get { if (_polylineCategoryListId == null) { _polylineCategoryListId = new ConditionValue(); } return _polylineCategoryListId; }
        }
        protected override ConditionValue getCValuePolylineCategoryListId() { return this.PolylineCategoryListId; }


        public BsTPolylineCategoryListCQ AddOrderBy_PolylineCategoryListId_Asc() { regOBA("POLYLINE_CATEGORY_LIST_ID");return this; }
        public BsTPolylineCategoryListCQ AddOrderBy_PolylineCategoryListId_Desc() { regOBD("POLYLINE_CATEGORY_LIST_ID");return this; }

        protected ConditionValue _crossScenarioItemId;
        public ConditionValue CrossScenarioItemId {
            get { if (_crossScenarioItemId == null) { _crossScenarioItemId = new ConditionValue(); } return _crossScenarioItemId; }
        }
        protected override ConditionValue getCValueCrossScenarioItemId() { return this.CrossScenarioItemId; }


        protected Map<String, TCrossScenarioItemCQ> _crossScenarioItemId_InScopeSubQuery_TCrossScenarioItemMap;
        public Map<String, TCrossScenarioItemCQ> CrossScenarioItemId_InScopeSubQuery_TCrossScenarioItem { get { return _crossScenarioItemId_InScopeSubQuery_TCrossScenarioItemMap; }}
        public override String keepCrossScenarioItemId_InScopeSubQuery_TCrossScenarioItem(TCrossScenarioItemCQ subQuery) {
            if (_crossScenarioItemId_InScopeSubQuery_TCrossScenarioItemMap == null) { _crossScenarioItemId_InScopeSubQuery_TCrossScenarioItemMap = new LinkedHashMap<String, TCrossScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioItemId_InScopeSubQuery_TCrossScenarioItemMap.size() + 1);
            _crossScenarioItemId_InScopeSubQuery_TCrossScenarioItemMap.put(key, subQuery); return "CrossScenarioItemId_InScopeSubQuery_TCrossScenarioItem." + key;
        }

        protected Map<String, TCrossScenarioItemCQ> _crossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItemMap;
        public Map<String, TCrossScenarioItemCQ> CrossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItem { get { return _crossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItemMap; }}
        public override String keepCrossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItem(TCrossScenarioItemCQ subQuery) {
            if (_crossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItemMap == null) { _crossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItemMap = new LinkedHashMap<String, TCrossScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItemMap.size() + 1);
            _crossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItemMap.put(key, subQuery); return "CrossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItem." + key;
        }

        public BsTPolylineCategoryListCQ AddOrderBy_CrossScenarioItemId_Asc() { regOBA("CROSS_SCENARIO_ITEM_ID");return this; }
        public BsTPolylineCategoryListCQ AddOrderBy_CrossScenarioItemId_Desc() { regOBD("CROSS_SCENARIO_ITEM_ID");return this; }

        protected ConditionValue _axisCategoryNo;
        public ConditionValue AxisCategoryNo {
            get { if (_axisCategoryNo == null) { _axisCategoryNo = new ConditionValue(); } return _axisCategoryNo; }
        }
        protected override ConditionValue getCValueAxisCategoryNo() { return this.AxisCategoryNo; }


        public BsTPolylineCategoryListCQ AddOrderBy_AxisCategoryNo_Asc() { regOBA("AXIS_CATEGORY_NO");return this; }
        public BsTPolylineCategoryListCQ AddOrderBy_AxisCategoryNo_Desc() { regOBD("AXIS_CATEGORY_NO");return this; }

        protected ConditionValue _axis2CategoryNo;
        public ConditionValue Axis2CategoryNo {
            get { if (_axis2CategoryNo == null) { _axis2CategoryNo = new ConditionValue(); } return _axis2CategoryNo; }
        }
        protected override ConditionValue getCValueAxis2CategoryNo() { return this.Axis2CategoryNo; }


        public BsTPolylineCategoryListCQ AddOrderBy_Axis2CategoryNo_Asc() { regOBA("AXIS2_CATEGORY_NO");return this; }
        public BsTPolylineCategoryListCQ AddOrderBy_Axis2CategoryNo_Desc() { regOBD("AXIS2_CATEGORY_NO");return this; }

        protected ConditionValue _arrayNoSingular;
        public ConditionValue ArrayNoSingular {
            get { if (_arrayNoSingular == null) { _arrayNoSingular = new ConditionValue(); } return _arrayNoSingular; }
        }
        protected override ConditionValue getCValueArrayNoSingular() { return this.ArrayNoSingular; }


        public BsTPolylineCategoryListCQ AddOrderBy_ArrayNoSingular_Asc() { regOBA("ARRAY_NO_SINGULAR");return this; }
        public BsTPolylineCategoryListCQ AddOrderBy_ArrayNoSingular_Desc() { regOBD("ARRAY_NO_SINGULAR");return this; }

        protected ConditionValue _arrayNoPlural;
        public ConditionValue ArrayNoPlural {
            get { if (_arrayNoPlural == null) { _arrayNoPlural = new ConditionValue(); } return _arrayNoPlural; }
        }
        protected override ConditionValue getCValueArrayNoPlural() { return this.ArrayNoPlural; }


        public BsTPolylineCategoryListCQ AddOrderBy_ArrayNoPlural_Asc() { regOBA("ARRAY_NO_PLURAL");return this; }
        public BsTPolylineCategoryListCQ AddOrderBy_ArrayNoPlural_Desc() { regOBD("ARRAY_NO_PLURAL");return this; }

        public BsTPolylineCategoryListCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTPolylineCategoryListCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TPolylineCategoryListCQ baseQuery = (TPolylineCategoryListCQ)baseQueryAsSuper;
            TPolylineCategoryListCQ unionQuery = (TPolylineCategoryListCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTCrossScenarioItem()) {
                unionQuery.QueryTCrossScenarioItem().reflectRelationOnUnionQuery(baseQuery.QueryTCrossScenarioItem(), unionQuery.QueryTCrossScenarioItem());
            }

        }
    
        protected TCrossScenarioItemCQ _conditionQueryTCrossScenarioItem;
        public TCrossScenarioItemCQ QueryTCrossScenarioItem() {
            return this.ConditionQueryTCrossScenarioItem;
        }
        public TCrossScenarioItemCQ ConditionQueryTCrossScenarioItem {
            get {
                if (_conditionQueryTCrossScenarioItem == null) {
                    _conditionQueryTCrossScenarioItem = xcreateQueryTCrossScenarioItem();
                    xsetupOuterJoin_TCrossScenarioItem();
                }
                return _conditionQueryTCrossScenarioItem;
            }
        }
        protected TCrossScenarioItemCQ xcreateQueryTCrossScenarioItem() {
            String nrp = resolveNextRelationPathTCrossScenarioItem();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TCrossScenarioItemCQ cq = new TCrossScenarioItemCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tCrossScenarioItem"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TCrossScenarioItem() {
            TCrossScenarioItemCQ cq = ConditionQueryTCrossScenarioItem;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTCrossScenarioItem() {
            return resolveNextRelationPath("T_POLYLINE_CATEGORY_LIST", "tCrossScenarioItem");
        }
        public bool hasConditionQueryTCrossScenarioItem() {
            return _conditionQueryTCrossScenarioItem != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TPolylineCategoryListCQ> _scalarSubQueryMap;
	    public Map<String, TPolylineCategoryListCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TPolylineCategoryListCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TPolylineCategoryListCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TPolylineCategoryListCQ> _myselfInScopeSubQueryMap;
        public Map<String, TPolylineCategoryListCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TPolylineCategoryListCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TPolylineCategoryListCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
