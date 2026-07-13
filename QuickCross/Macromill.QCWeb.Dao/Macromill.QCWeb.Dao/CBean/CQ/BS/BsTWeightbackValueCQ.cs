
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTWeightbackValueCQ : AbstractBsTWeightbackValueCQ {

        protected TWeightbackValueCIQ _inlineQuery;

        public BsTWeightbackValueCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TWeightbackValueCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TWeightbackValueCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TWeightbackValueCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TWeightbackValueCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _weightbackValueId;
        public ConditionValue WeightbackValueId {
            get { if (_weightbackValueId == null) { _weightbackValueId = new ConditionValue(); } return _weightbackValueId; }
        }
        protected override ConditionValue getCValueWeightbackValueId() { return this.WeightbackValueId; }


        public BsTWeightbackValueCQ AddOrderBy_WeightbackValueId_Asc() { regOBA("WEIGHTBACK_VALUE_ID");return this; }
        public BsTWeightbackValueCQ AddOrderBy_WeightbackValueId_Desc() { regOBD("WEIGHTBACK_VALUE_ID");return this; }

        protected ConditionValue _weightbackItemNo;
        public ConditionValue WeightbackItemNo {
            get { if (_weightbackItemNo == null) { _weightbackItemNo = new ConditionValue(); } return _weightbackItemNo; }
        }
        protected override ConditionValue getCValueWeightbackItemNo() { return this.WeightbackItemNo; }


        public BsTWeightbackValueCQ AddOrderBy_WeightbackItemNo_Asc() { regOBA("WEIGHTBACK_ITEM_NO");return this; }
        public BsTWeightbackValueCQ AddOrderBy_WeightbackItemNo_Desc() { regOBD("WEIGHTBACK_ITEM_NO");return this; }

        protected ConditionValue _percentValue;
        public ConditionValue PercentValue {
            get { if (_percentValue == null) { _percentValue = new ConditionValue(); } return _percentValue; }
        }
        protected override ConditionValue getCValuePercentValue() { return this.PercentValue; }


        public BsTWeightbackValueCQ AddOrderBy_PercentValue_Asc() { regOBA("PERCENT_VALUE");return this; }
        public BsTWeightbackValueCQ AddOrderBy_PercentValue_Desc() { regOBD("PERCENT_VALUE");return this; }

        protected ConditionValue _parameterNValue;
        public ConditionValue ParameterNValue {
            get { if (_parameterNValue == null) { _parameterNValue = new ConditionValue(); } return _parameterNValue; }
        }
        protected override ConditionValue getCValueParameterNValue() { return this.ParameterNValue; }


        public BsTWeightbackValueCQ AddOrderBy_ParameterNValue_Asc() { regOBA("PARAMETER_N_VALUE");return this; }
        public BsTWeightbackValueCQ AddOrderBy_ParameterNValue_Desc() { regOBD("PARAMETER_N_VALUE");return this; }

        protected ConditionValue _weightbackValue;
        public ConditionValue WeightbackValue {
            get { if (_weightbackValue == null) { _weightbackValue = new ConditionValue(); } return _weightbackValue; }
        }
        protected override ConditionValue getCValueWeightbackValue() { return this.WeightbackValue; }


        public BsTWeightbackValueCQ AddOrderBy_WeightbackValue_Asc() { regOBA("WEIGHTBACK_VALUE");return this; }
        public BsTWeightbackValueCQ AddOrderBy_WeightbackValue_Desc() { regOBD("WEIGHTBACK_VALUE");return this; }

        protected ConditionValue _weightbackId;
        public ConditionValue WeightbackId {
            get { if (_weightbackId == null) { _weightbackId = new ConditionValue(); } return _weightbackId; }
        }
        protected override ConditionValue getCValueWeightbackId() { return this.WeightbackId; }


        protected Map<String, TWeightbackCQ> _weightbackId_InScopeSubQuery_TWeightbackMap;
        public Map<String, TWeightbackCQ> WeightbackId_InScopeSubQuery_TWeightback { get { return _weightbackId_InScopeSubQuery_TWeightbackMap; }}
        public override String keepWeightbackId_InScopeSubQuery_TWeightback(TWeightbackCQ subQuery) {
            if (_weightbackId_InScopeSubQuery_TWeightbackMap == null) { _weightbackId_InScopeSubQuery_TWeightbackMap = new LinkedHashMap<String, TWeightbackCQ>(); }
            String key = "subQueryMapKey" + (_weightbackId_InScopeSubQuery_TWeightbackMap.size() + 1);
            _weightbackId_InScopeSubQuery_TWeightbackMap.put(key, subQuery); return "WeightbackId_InScopeSubQuery_TWeightback." + key;
        }

        protected Map<String, TWeightbackCQ> _weightbackId_NotInScopeSubQuery_TWeightbackMap;
        public Map<String, TWeightbackCQ> WeightbackId_NotInScopeSubQuery_TWeightback { get { return _weightbackId_NotInScopeSubQuery_TWeightbackMap; }}
        public override String keepWeightbackId_NotInScopeSubQuery_TWeightback(TWeightbackCQ subQuery) {
            if (_weightbackId_NotInScopeSubQuery_TWeightbackMap == null) { _weightbackId_NotInScopeSubQuery_TWeightbackMap = new LinkedHashMap<String, TWeightbackCQ>(); }
            String key = "subQueryMapKey" + (_weightbackId_NotInScopeSubQuery_TWeightbackMap.size() + 1);
            _weightbackId_NotInScopeSubQuery_TWeightbackMap.put(key, subQuery); return "WeightbackId_NotInScopeSubQuery_TWeightback." + key;
        }

        public BsTWeightbackValueCQ AddOrderBy_WeightbackId_Asc() { regOBA("WEIGHTBACK_ID");return this; }
        public BsTWeightbackValueCQ AddOrderBy_WeightbackId_Desc() { regOBD("WEIGHTBACK_ID");return this; }

        public BsTWeightbackValueCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTWeightbackValueCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TWeightbackValueCQ baseQuery = (TWeightbackValueCQ)baseQueryAsSuper;
            TWeightbackValueCQ unionQuery = (TWeightbackValueCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTWeightback()) {
                unionQuery.QueryTWeightback().reflectRelationOnUnionQuery(baseQuery.QueryTWeightback(), unionQuery.QueryTWeightback());
            }

        }
    
        protected TWeightbackCQ _conditionQueryTWeightback;
        public TWeightbackCQ QueryTWeightback() {
            return this.ConditionQueryTWeightback;
        }
        public TWeightbackCQ ConditionQueryTWeightback {
            get {
                if (_conditionQueryTWeightback == null) {
                    _conditionQueryTWeightback = xcreateQueryTWeightback();
                    xsetupOuterJoin_TWeightback();
                }
                return _conditionQueryTWeightback;
            }
        }
        protected TWeightbackCQ xcreateQueryTWeightback() {
            String nrp = resolveNextRelationPathTWeightback();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TWeightbackCQ cq = new TWeightbackCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tWeightback"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TWeightback() {
            TWeightbackCQ cq = ConditionQueryTWeightback;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("WEIGHTBACK_ID", "WEIGHTBACK_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTWeightback() {
            return resolveNextRelationPath("T_WEIGHTBACK_VALUE", "tWeightback");
        }
        public bool hasConditionQueryTWeightback() {
            return _conditionQueryTWeightback != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TWeightbackValueCQ> _scalarSubQueryMap;
	    public Map<String, TWeightbackValueCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TWeightbackValueCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TWeightbackValueCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TWeightbackValueCQ> _myselfInScopeSubQueryMap;
        public Map<String, TWeightbackValueCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TWeightbackValueCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TWeightbackValueCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
