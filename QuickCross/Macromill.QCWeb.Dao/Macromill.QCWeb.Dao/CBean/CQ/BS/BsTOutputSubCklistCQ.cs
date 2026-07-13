
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputSubCklistCQ : AbstractBsTOutputSubCklistCQ {

        protected TOutputSubCklistCIQ _inlineQuery;

        public BsTOutputSubCklistCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputSubCklistCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputSubCklistCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputSubCklistCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputSubCklistCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _outputSubCklistId;
        public ConditionValue OutputSubCklistId {
            get { if (_outputSubCklistId == null) { _outputSubCklistId = new ConditionValue(); } return _outputSubCklistId; }
        }
        protected override ConditionValue getCValueOutputSubCklistId() { return this.OutputSubCklistId; }


        public BsTOutputSubCklistCQ AddOrderBy_OutputSubCklistId_Asc() { regOBA("OUTPUT_SUB_CKLIST_ID");return this; }
        public BsTOutputSubCklistCQ AddOrderBy_OutputSubCklistId_Desc() { regOBD("OUTPUT_SUB_CKLIST_ID");return this; }

        protected ConditionValue _outputCommonId;
        public ConditionValue OutputCommonId {
            get { if (_outputCommonId == null) { _outputCommonId = new ConditionValue(); } return _outputCommonId; }
        }
        protected override ConditionValue getCValueOutputCommonId() { return this.OutputCommonId; }


        protected Map<String, TOutputCommonCQ> _outputCommonId_InScopeSubQuery_TOutputCommonMap;
        public Map<String, TOutputCommonCQ> OutputCommonId_InScopeSubQuery_TOutputCommon { get { return _outputCommonId_InScopeSubQuery_TOutputCommonMap; }}
        public override String keepOutputCommonId_InScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery) {
            if (_outputCommonId_InScopeSubQuery_TOutputCommonMap == null) { _outputCommonId_InScopeSubQuery_TOutputCommonMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_InScopeSubQuery_TOutputCommonMap.size() + 1);
            _outputCommonId_InScopeSubQuery_TOutputCommonMap.put(key, subQuery); return "OutputCommonId_InScopeSubQuery_TOutputCommon." + key;
        }

        protected Map<String, TOutputCommonCQ> _outputCommonId_NotInScopeSubQuery_TOutputCommonMap;
        public Map<String, TOutputCommonCQ> OutputCommonId_NotInScopeSubQuery_TOutputCommon { get { return _outputCommonId_NotInScopeSubQuery_TOutputCommonMap; }}
        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery) {
            if (_outputCommonId_NotInScopeSubQuery_TOutputCommonMap == null) { _outputCommonId_NotInScopeSubQuery_TOutputCommonMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_NotInScopeSubQuery_TOutputCommonMap.size() + 1);
            _outputCommonId_NotInScopeSubQuery_TOutputCommonMap.put(key, subQuery); return "OutputCommonId_NotInScopeSubQuery_TOutputCommon." + key;
        }

        public BsTOutputSubCklistCQ AddOrderBy_OutputCommonId_Asc() { regOBA("OUTPUT_COMMON_ID");return this; }
        public BsTOutputSubCklistCQ AddOrderBy_OutputCommonId_Desc() { regOBD("OUTPUT_COMMON_ID");return this; }

        protected ConditionValue _totalCount;
        public ConditionValue TotalCount {
            get { if (_totalCount == null) { _totalCount = new ConditionValue(); } return _totalCount; }
        }
        protected override ConditionValue getCValueTotalCount() { return this.TotalCount; }


        public BsTOutputSubCklistCQ AddOrderBy_TotalCount_Asc() { regOBA("TOTAL_COUNT");return this; }
        public BsTOutputSubCklistCQ AddOrderBy_TotalCount_Desc() { regOBD("TOTAL_COUNT");return this; }

        public BsTOutputSubCklistCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputSubCklistCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputSubCklistCQ baseQuery = (TOutputSubCklistCQ)baseQueryAsSuper;
            TOutputSubCklistCQ unionQuery = (TOutputSubCklistCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTOutputCommon()) {
                unionQuery.QueryTOutputCommon().reflectRelationOnUnionQuery(baseQuery.QueryTOutputCommon(), unionQuery.QueryTOutputCommon());
            }

        }
    
        protected TOutputCommonCQ _conditionQueryTOutputCommon;
        public TOutputCommonCQ QueryTOutputCommon() {
            return this.ConditionQueryTOutputCommon;
        }
        public TOutputCommonCQ ConditionQueryTOutputCommon {
            get {
                if (_conditionQueryTOutputCommon == null) {
                    _conditionQueryTOutputCommon = xcreateQueryTOutputCommon();
                    xsetupOuterJoin_TOutputCommon();
                }
                return _conditionQueryTOutputCommon;
            }
        }
        protected TOutputCommonCQ xcreateQueryTOutputCommon() {
            String nrp = resolveNextRelationPathTOutputCommon();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputCommonCQ cq = new TOutputCommonCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputCommon"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputCommon() {
            TOutputCommonCQ cq = ConditionQueryTOutputCommon;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputCommon() {
            return resolveNextRelationPath("T_OUTPUT_SUB_CKLIST", "tOutputCommon");
        }
        public bool hasConditionQueryTOutputCommon() {
            return _conditionQueryTOutputCommon != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputSubCklistCQ> _scalarSubQueryMap;
	    public Map<String, TOutputSubCklistCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputSubCklistCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputSubCklistCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputSubCklistCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputSubCklistCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputSubCklistCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputSubCklistCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
