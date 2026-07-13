
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDefaultEnvColorDtlCCQ : AbstractBsTDefaultEnvColorDtlCCQ {

        protected TDefaultEnvColorDtlCCIQ _inlineQuery;

        public BsTDefaultEnvColorDtlCCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDefaultEnvColorDtlCCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDefaultEnvColorDtlCCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDefaultEnvColorDtlCCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDefaultEnvColorDtlCCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _defEnvColorDtlCId;
        public ConditionValue DefEnvColorDtlCId {
            get { if (_defEnvColorDtlCId == null) { _defEnvColorDtlCId = new ConditionValue(); } return _defEnvColorDtlCId; }
        }
        protected override ConditionValue getCValueDefEnvColorDtlCId() { return this.DefEnvColorDtlCId; }


        public BsTDefaultEnvColorDtlCCQ AddOrderBy_DefEnvColorDtlCId_Asc() { regOBA("DEF_ENV_COLOR_DTL_C_ID");return this; }
        public BsTDefaultEnvColorDtlCCQ AddOrderBy_DefEnvColorDtlCId_Desc() { regOBD("DEF_ENV_COLOR_DTL_C_ID");return this; }

        protected ConditionValue _defEnvColorInfoCId;
        public ConditionValue DefEnvColorInfoCId {
            get { if (_defEnvColorInfoCId == null) { _defEnvColorInfoCId = new ConditionValue(); } return _defEnvColorInfoCId; }
        }
        protected override ConditionValue getCValueDefEnvColorInfoCId() { return this.DefEnvColorInfoCId; }


        protected Map<String, TDefaultEnvColorInfoCCQ> _defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoCMap;
        public Map<String, TDefaultEnvColorInfoCCQ> DefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoC { get { return _defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoCMap; }}
        public override String keepDefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoC(TDefaultEnvColorInfoCCQ subQuery) {
            if (_defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoCMap == null) { _defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoCMap = new LinkedHashMap<String, TDefaultEnvColorInfoCCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoCMap.size() + 1);
            _defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoCMap.put(key, subQuery); return "DefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoC." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCCQ> _defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoCMap;
        public Map<String, TDefaultEnvColorInfoCCQ> DefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoC { get { return _defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoCMap; }}
        public override String keepDefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoC(TDefaultEnvColorInfoCCQ subQuery) {
            if (_defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoCMap == null) { _defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoCMap = new LinkedHashMap<String, TDefaultEnvColorInfoCCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoCMap.size() + 1);
            _defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoCMap.put(key, subQuery); return "DefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoC." + key;
        }

        public BsTDefaultEnvColorDtlCCQ AddOrderBy_DefEnvColorInfoCId_Asc() { regOBA("DEF_ENV_COLOR_INFO_C_ID");return this; }
        public BsTDefaultEnvColorDtlCCQ AddOrderBy_DefEnvColorInfoCId_Desc() { regOBD("DEF_ENV_COLOR_INFO_C_ID");return this; }

        protected ConditionValue _graphColorNo;
        public ConditionValue GraphColorNo {
            get { if (_graphColorNo == null) { _graphColorNo = new ConditionValue(); } return _graphColorNo; }
        }
        protected override ConditionValue getCValueGraphColorNo() { return this.GraphColorNo; }


        public BsTDefaultEnvColorDtlCCQ AddOrderBy_GraphColorNo_Asc() { regOBA("GRAPH_COLOR_NO");return this; }
        public BsTDefaultEnvColorDtlCCQ AddOrderBy_GraphColorNo_Desc() { regOBD("GRAPH_COLOR_NO");return this; }

        protected ConditionValue _colorCode;
        public ConditionValue ColorCode {
            get { if (_colorCode == null) { _colorCode = new ConditionValue(); } return _colorCode; }
        }
        protected override ConditionValue getCValueColorCode() { return this.ColorCode; }


        public BsTDefaultEnvColorDtlCCQ AddOrderBy_ColorCode_Asc() { regOBA("COLOR_CODE");return this; }
        public BsTDefaultEnvColorDtlCCQ AddOrderBy_ColorCode_Desc() { regOBD("COLOR_CODE");return this; }

        protected ConditionValue _patternCode;
        public ConditionValue PatternCode {
            get { if (_patternCode == null) { _patternCode = new ConditionValue(); } return _patternCode; }
        }
        protected override ConditionValue getCValuePatternCode() { return this.PatternCode; }


        public BsTDefaultEnvColorDtlCCQ AddOrderBy_PatternCode_Asc() { regOBA("PATTERN_CODE");return this; }
        public BsTDefaultEnvColorDtlCCQ AddOrderBy_PatternCode_Desc() { regOBD("PATTERN_CODE");return this; }

        public BsTDefaultEnvColorDtlCCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDefaultEnvColorDtlCCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDefaultEnvColorDtlCCQ baseQuery = (TDefaultEnvColorDtlCCQ)baseQueryAsSuper;
            TDefaultEnvColorDtlCCQ unionQuery = (TDefaultEnvColorDtlCCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTDefaultEnvColorInfoC()) {
                unionQuery.QueryTDefaultEnvColorInfoC().reflectRelationOnUnionQuery(baseQuery.QueryTDefaultEnvColorInfoC(), unionQuery.QueryTDefaultEnvColorInfoC());
            }

        }
    
        protected TDefaultEnvColorInfoCCQ _conditionQueryTDefaultEnvColorInfoC;
        public TDefaultEnvColorInfoCCQ QueryTDefaultEnvColorInfoC() {
            return this.ConditionQueryTDefaultEnvColorInfoC;
        }
        public TDefaultEnvColorInfoCCQ ConditionQueryTDefaultEnvColorInfoC {
            get {
                if (_conditionQueryTDefaultEnvColorInfoC == null) {
                    _conditionQueryTDefaultEnvColorInfoC = xcreateQueryTDefaultEnvColorInfoC();
                    xsetupOuterJoin_TDefaultEnvColorInfoC();
                }
                return _conditionQueryTDefaultEnvColorInfoC;
            }
        }
        protected TDefaultEnvColorInfoCCQ xcreateQueryTDefaultEnvColorInfoC() {
            String nrp = resolveNextRelationPathTDefaultEnvColorInfoC();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDefaultEnvColorInfoCCQ cq = new TDefaultEnvColorInfoCCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDefaultEnvColorInfoC"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDefaultEnvColorInfoC() {
            TDefaultEnvColorInfoCCQ cq = ConditionQueryTDefaultEnvColorInfoC;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDefaultEnvColorInfoC() {
            return resolveNextRelationPath("T_DEFAULT_ENV_COLOR_DTL_C", "tDefaultEnvColorInfoC");
        }
        public bool hasConditionQueryTDefaultEnvColorInfoC() {
            return _conditionQueryTDefaultEnvColorInfoC != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDefaultEnvColorDtlCCQ> _scalarSubQueryMap;
	    public Map<String, TDefaultEnvColorDtlCCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDefaultEnvColorDtlCCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDefaultEnvColorDtlCCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDefaultEnvColorDtlCCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDefaultEnvColorDtlCCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDefaultEnvColorDtlCCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDefaultEnvColorDtlCCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
