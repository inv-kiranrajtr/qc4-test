
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDefaultEnvColorDtlCQ : AbstractBsTDefaultEnvColorDtlCQ {

        protected TDefaultEnvColorDtlCIQ _inlineQuery;

        public BsTDefaultEnvColorDtlCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDefaultEnvColorDtlCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDefaultEnvColorDtlCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDefaultEnvColorDtlCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDefaultEnvColorDtlCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _defEnvColorDtlId;
        public ConditionValue DefEnvColorDtlId {
            get { if (_defEnvColorDtlId == null) { _defEnvColorDtlId = new ConditionValue(); } return _defEnvColorDtlId; }
        }
        protected override ConditionValue getCValueDefEnvColorDtlId() { return this.DefEnvColorDtlId; }


        public BsTDefaultEnvColorDtlCQ AddOrderBy_DefEnvColorDtlId_Asc() { regOBA("DEF_ENV_COLOR_DTL_ID");return this; }
        public BsTDefaultEnvColorDtlCQ AddOrderBy_DefEnvColorDtlId_Desc() { regOBD("DEF_ENV_COLOR_DTL_ID");return this; }

        protected ConditionValue _defEnvColorInfoId;
        public ConditionValue DefEnvColorInfoId {
            get { if (_defEnvColorInfoId == null) { _defEnvColorInfoId = new ConditionValue(); } return _defEnvColorInfoId; }
        }
        protected override ConditionValue getCValueDefEnvColorInfoId() { return this.DefEnvColorInfoId; }


        protected Map<String, TDefaultEnvColorInfoCQ> _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfoMap;
        public Map<String, TDefaultEnvColorInfoCQ> DefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfo { get { return _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfoMap; }}
        public override String keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfo(TDefaultEnvColorInfoCQ subQuery) {
            if (_defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfoMap == null) { _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfoMap = new LinkedHashMap<String, TDefaultEnvColorInfoCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfoMap.size() + 1);
            _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfoMap.put(key, subQuery); return "DefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfo." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCQ> _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfoMap;
        public Map<String, TDefaultEnvColorInfoCQ> DefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfo { get { return _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfoMap; }}
        public override String keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfo(TDefaultEnvColorInfoCQ subQuery) {
            if (_defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfoMap == null) { _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfoMap = new LinkedHashMap<String, TDefaultEnvColorInfoCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfoMap.size() + 1);
            _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfoMap.put(key, subQuery); return "DefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfo." + key;
        }

        public BsTDefaultEnvColorDtlCQ AddOrderBy_DefEnvColorInfoId_Asc() { regOBA("DEF_ENV_COLOR_INFO_ID");return this; }
        public BsTDefaultEnvColorDtlCQ AddOrderBy_DefEnvColorInfoId_Desc() { regOBD("DEF_ENV_COLOR_INFO_ID");return this; }

        protected ConditionValue _graphColorNo;
        public ConditionValue GraphColorNo {
            get { if (_graphColorNo == null) { _graphColorNo = new ConditionValue(); } return _graphColorNo; }
        }
        protected override ConditionValue getCValueGraphColorNo() { return this.GraphColorNo; }


        public BsTDefaultEnvColorDtlCQ AddOrderBy_GraphColorNo_Asc() { regOBA("GRAPH_COLOR_NO");return this; }
        public BsTDefaultEnvColorDtlCQ AddOrderBy_GraphColorNo_Desc() { regOBD("GRAPH_COLOR_NO");return this; }

        protected ConditionValue _colorCode;
        public ConditionValue ColorCode {
            get { if (_colorCode == null) { _colorCode = new ConditionValue(); } return _colorCode; }
        }
        protected override ConditionValue getCValueColorCode() { return this.ColorCode; }


        public BsTDefaultEnvColorDtlCQ AddOrderBy_ColorCode_Asc() { regOBA("COLOR_CODE");return this; }
        public BsTDefaultEnvColorDtlCQ AddOrderBy_ColorCode_Desc() { regOBD("COLOR_CODE");return this; }

        protected ConditionValue _patternCode;
        public ConditionValue PatternCode {
            get { if (_patternCode == null) { _patternCode = new ConditionValue(); } return _patternCode; }
        }
        protected override ConditionValue getCValuePatternCode() { return this.PatternCode; }


        public BsTDefaultEnvColorDtlCQ AddOrderBy_PatternCode_Asc() { regOBA("PATTERN_CODE");return this; }
        public BsTDefaultEnvColorDtlCQ AddOrderBy_PatternCode_Desc() { regOBD("PATTERN_CODE");return this; }

        public BsTDefaultEnvColorDtlCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDefaultEnvColorDtlCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDefaultEnvColorDtlCQ baseQuery = (TDefaultEnvColorDtlCQ)baseQueryAsSuper;
            TDefaultEnvColorDtlCQ unionQuery = (TDefaultEnvColorDtlCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTDefaultEnvColorInfo()) {
                unionQuery.QueryTDefaultEnvColorInfo().reflectRelationOnUnionQuery(baseQuery.QueryTDefaultEnvColorInfo(), unionQuery.QueryTDefaultEnvColorInfo());
            }

        }
    
        protected TDefaultEnvColorInfoCQ _conditionQueryTDefaultEnvColorInfo;
        public TDefaultEnvColorInfoCQ QueryTDefaultEnvColorInfo() {
            return this.ConditionQueryTDefaultEnvColorInfo;
        }
        public TDefaultEnvColorInfoCQ ConditionQueryTDefaultEnvColorInfo {
            get {
                if (_conditionQueryTDefaultEnvColorInfo == null) {
                    _conditionQueryTDefaultEnvColorInfo = xcreateQueryTDefaultEnvColorInfo();
                    xsetupOuterJoin_TDefaultEnvColorInfo();
                }
                return _conditionQueryTDefaultEnvColorInfo;
            }
        }
        protected TDefaultEnvColorInfoCQ xcreateQueryTDefaultEnvColorInfo() {
            String nrp = resolveNextRelationPathTDefaultEnvColorInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDefaultEnvColorInfoCQ cq = new TDefaultEnvColorInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDefaultEnvColorInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDefaultEnvColorInfo() {
            TDefaultEnvColorInfoCQ cq = ConditionQueryTDefaultEnvColorInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDefaultEnvColorInfo() {
            return resolveNextRelationPath("T_DEFAULT_ENV_COLOR_DTL", "tDefaultEnvColorInfo");
        }
        public bool hasConditionQueryTDefaultEnvColorInfo() {
            return _conditionQueryTDefaultEnvColorInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDefaultEnvColorDtlCQ> _scalarSubQueryMap;
	    public Map<String, TDefaultEnvColorDtlCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDefaultEnvColorDtlCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDefaultEnvColorDtlCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDefaultEnvColorDtlCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDefaultEnvColorDtlCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDefaultEnvColorDtlCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDefaultEnvColorDtlCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
