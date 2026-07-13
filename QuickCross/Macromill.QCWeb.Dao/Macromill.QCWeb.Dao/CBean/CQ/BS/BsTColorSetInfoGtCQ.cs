
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTColorSetInfoGtCQ : AbstractBsTColorSetInfoGtCQ {

        protected TColorSetInfoGtCIQ _inlineQuery;

        public BsTColorSetInfoGtCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TColorSetInfoGtCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TColorSetInfoGtCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TColorSetInfoGtCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TColorSetInfoGtCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _colorSetInfoGtId;
        public ConditionValue ColorSetInfoGtId {
            get { if (_colorSetInfoGtId == null) { _colorSetInfoGtId = new ConditionValue(); } return _colorSetInfoGtId; }
        }
        protected override ConditionValue getCValueColorSetInfoGtId() { return this.ColorSetInfoGtId; }


        protected Map<String, TColorInfoDetailGtCQ> _colorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtListMap;
        public Map<String, TColorInfoDetailGtCQ> ColorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtList { get { return _colorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtListMap; }}
        public override String keepColorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            if (_colorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtListMap == null) { _colorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtListMap = new LinkedHashMap<String, TColorInfoDetailGtCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtListMap.size() + 1);
            _colorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtListMap.put(key, subQuery); return "ColorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtList." + key;
        }

        protected Map<String, TColorInfoDetailGtCQ> _colorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtListMap;
        public Map<String, TColorInfoDetailGtCQ> ColorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtList { get { return _colorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtListMap; }}
        public override String keepColorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            if (_colorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtListMap == null) { _colorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtListMap = new LinkedHashMap<String, TColorInfoDetailGtCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtListMap.size() + 1);
            _colorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtListMap.put(key, subQuery); return "ColorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtList." + key;
        }

        protected Map<String, TColorInfoDetailGtCQ> _colorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtListMap;
        public Map<String, TColorInfoDetailGtCQ> ColorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtList { get { return _colorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtListMap; }}
        public override String keepColorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            if (_colorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtListMap == null) { _colorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtListMap = new LinkedHashMap<String, TColorInfoDetailGtCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtListMap.size() + 1);
            _colorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtListMap.put(key, subQuery); return "ColorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtList." + key;
        }

        protected Map<String, TColorInfoDetailGtCQ> _colorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtListMap;
        public Map<String, TColorInfoDetailGtCQ> ColorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtList { get { return _colorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtListMap; }}
        public override String keepColorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            if (_colorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtListMap == null) { _colorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtListMap = new LinkedHashMap<String, TColorInfoDetailGtCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtListMap.size() + 1);
            _colorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtListMap.put(key, subQuery); return "ColorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtList." + key;
        }

        protected Map<String, TColorInfoDetailGtCQ> _colorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtListMap;
        public Map<String, TColorInfoDetailGtCQ> ColorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtList { get { return _colorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtListMap; }}
        public override String keepColorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            if (_colorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtListMap == null) { _colorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtListMap = new LinkedHashMap<String, TColorInfoDetailGtCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtListMap.size() + 1);
            _colorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtListMap.put(key, subQuery); return "ColorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtList." + key;
        }

        protected Map<String, TColorInfoDetailGtCQ> _colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListMap;
        public Map<String, TColorInfoDetailGtCQ> ColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtList { get { return _colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListMap; } }
        public override String keepColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            if (_colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListMap == null) { _colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListMap = new LinkedHashMap<String, TColorInfoDetailGtCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListMap.size() + 1);
            _colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListMap.put(key, subQuery); return "ColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtList." + key;
        }
        protected Map<String, Object> _colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameterMap;
        public Map<String, Object> ColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameter { get { return _colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameterMap; } }
        public override String keepColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameter(Object parameterValue) {
            if (_colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameterMap == null) { _colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameterMap.size() + 1);
            _colorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameterMap.put(key, parameterValue); return "ColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameter." + key;
        }

        public BsTColorSetInfoGtCQ AddOrderBy_ColorSetInfoGtId_Asc() { regOBA("COLOR_SET_INFO_GT_ID");return this; }
        public BsTColorSetInfoGtCQ AddOrderBy_ColorSetInfoGtId_Desc() { regOBD("COLOR_SET_INFO_GT_ID");return this; }

        protected ConditionValue _typeCode;
        public ConditionValue TypeCode {
            get { if (_typeCode == null) { _typeCode = new ConditionValue(); } return _typeCode; }
        }
        protected override ConditionValue getCValueTypeCode() { return this.TypeCode; }


        public BsTColorSetInfoGtCQ AddOrderBy_TypeCode_Asc() { regOBA("TYPE_CODE");return this; }
        public BsTColorSetInfoGtCQ AddOrderBy_TypeCode_Desc() { regOBD("TYPE_CODE");return this; }

        protected ConditionValue _gradationType;
        public ConditionValue GradationType {
            get { if (_gradationType == null) { _gradationType = new ConditionValue(); } return _gradationType; }
        }
        protected override ConditionValue getCValueGradationType() { return this.GradationType; }


        public BsTColorSetInfoGtCQ AddOrderBy_GradationType_Asc() { regOBA("GRADATION_TYPE");return this; }
        public BsTColorSetInfoGtCQ AddOrderBy_GradationType_Desc() { regOBD("GRADATION_TYPE");return this; }

        protected ConditionValue _gtScenarioItemId;
        public ConditionValue GtScenarioItemId {
            get { if (_gtScenarioItemId == null) { _gtScenarioItemId = new ConditionValue(); } return _gtScenarioItemId; }
        }
        protected override ConditionValue getCValueGtScenarioItemId() { return this.GtScenarioItemId; }


        protected Map<String, TGtScenarioItemCQ> _gtScenarioItemId_InScopeSubQuery_TGtScenarioItemMap;
        public Map<String, TGtScenarioItemCQ> GtScenarioItemId_InScopeSubQuery_TGtScenarioItem { get { return _gtScenarioItemId_InScopeSubQuery_TGtScenarioItemMap; }}
        public override String keepGtScenarioItemId_InScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery) {
            if (_gtScenarioItemId_InScopeSubQuery_TGtScenarioItemMap == null) { _gtScenarioItemId_InScopeSubQuery_TGtScenarioItemMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_gtScenarioItemId_InScopeSubQuery_TGtScenarioItemMap.size() + 1);
            _gtScenarioItemId_InScopeSubQuery_TGtScenarioItemMap.put(key, subQuery); return "GtScenarioItemId_InScopeSubQuery_TGtScenarioItem." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _gtScenarioItemId_NotInScopeSubQuery_TGtScenarioItemMap;
        public Map<String, TGtScenarioItemCQ> GtScenarioItemId_NotInScopeSubQuery_TGtScenarioItem { get { return _gtScenarioItemId_NotInScopeSubQuery_TGtScenarioItemMap; }}
        public override String keepGtScenarioItemId_NotInScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery) {
            if (_gtScenarioItemId_NotInScopeSubQuery_TGtScenarioItemMap == null) { _gtScenarioItemId_NotInScopeSubQuery_TGtScenarioItemMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_gtScenarioItemId_NotInScopeSubQuery_TGtScenarioItemMap.size() + 1);
            _gtScenarioItemId_NotInScopeSubQuery_TGtScenarioItemMap.put(key, subQuery); return "GtScenarioItemId_NotInScopeSubQuery_TGtScenarioItem." + key;
        }

        public BsTColorSetInfoGtCQ AddOrderBy_GtScenarioItemId_Asc() { regOBA("GT_SCENARIO_ITEM_ID");return this; }
        public BsTColorSetInfoGtCQ AddOrderBy_GtScenarioItemId_Desc() { regOBD("GT_SCENARIO_ITEM_ID");return this; }

        public BsTColorSetInfoGtCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTColorSetInfoGtCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TColorSetInfoGtCQ baseQuery = (TColorSetInfoGtCQ)baseQueryAsSuper;
            TColorSetInfoGtCQ unionQuery = (TColorSetInfoGtCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTGtScenarioItem()) {
                unionQuery.QueryTGtScenarioItem().reflectRelationOnUnionQuery(baseQuery.QueryTGtScenarioItem(), unionQuery.QueryTGtScenarioItem());
            }

        }
    
        protected TGtScenarioItemCQ _conditionQueryTGtScenarioItem;
        public TGtScenarioItemCQ QueryTGtScenarioItem() {
            return this.ConditionQueryTGtScenarioItem;
        }
        public TGtScenarioItemCQ ConditionQueryTGtScenarioItem {
            get {
                if (_conditionQueryTGtScenarioItem == null) {
                    _conditionQueryTGtScenarioItem = xcreateQueryTGtScenarioItem();
                    xsetupOuterJoin_TGtScenarioItem();
                }
                return _conditionQueryTGtScenarioItem;
            }
        }
        protected TGtScenarioItemCQ xcreateQueryTGtScenarioItem() {
            String nrp = resolveNextRelationPathTGtScenarioItem();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TGtScenarioItemCQ cq = new TGtScenarioItemCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tGtScenarioItem"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TGtScenarioItem() {
            TGtScenarioItemCQ cq = ConditionQueryTGtScenarioItem;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTGtScenarioItem() {
            return resolveNextRelationPath("T_COLOR_SET_INFO_GT", "tGtScenarioItem");
        }
        public bool hasConditionQueryTGtScenarioItem() {
            return _conditionQueryTGtScenarioItem != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TColorSetInfoGtCQ> _scalarSubQueryMap;
	    public Map<String, TColorSetInfoGtCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TColorSetInfoGtCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TColorSetInfoGtCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TColorSetInfoGtCQ> _myselfInScopeSubQueryMap;
        public Map<String, TColorSetInfoGtCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TColorSetInfoGtCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TColorSetInfoGtCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
