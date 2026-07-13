
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTColorInfoDetailGtCQ : AbstractBsTColorInfoDetailGtCQ {

        protected TColorInfoDetailGtCIQ _inlineQuery;

        public BsTColorInfoDetailGtCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TColorInfoDetailGtCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TColorInfoDetailGtCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TColorInfoDetailGtCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TColorInfoDetailGtCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _colorInfoDetailGtId;
        public ConditionValue ColorInfoDetailGtId {
            get { if (_colorInfoDetailGtId == null) { _colorInfoDetailGtId = new ConditionValue(); } return _colorInfoDetailGtId; }
        }
        protected override ConditionValue getCValueColorInfoDetailGtId() { return this.ColorInfoDetailGtId; }


        public BsTColorInfoDetailGtCQ AddOrderBy_ColorInfoDetailGtId_Asc() { regOBA("COLOR_INFO_DETAIL_GT_ID");return this; }
        public BsTColorInfoDetailGtCQ AddOrderBy_ColorInfoDetailGtId_Desc() { regOBD("COLOR_INFO_DETAIL_GT_ID");return this; }

        protected ConditionValue _graphColorNo;
        public ConditionValue GraphColorNo {
            get { if (_graphColorNo == null) { _graphColorNo = new ConditionValue(); } return _graphColorNo; }
        }
        protected override ConditionValue getCValueGraphColorNo() { return this.GraphColorNo; }


        public BsTColorInfoDetailGtCQ AddOrderBy_GraphColorNo_Asc() { regOBA("GRAPH_COLOR_NO");return this; }
        public BsTColorInfoDetailGtCQ AddOrderBy_GraphColorNo_Desc() { regOBD("GRAPH_COLOR_NO");return this; }

        protected ConditionValue _colorCode;
        public ConditionValue ColorCode {
            get { if (_colorCode == null) { _colorCode = new ConditionValue(); } return _colorCode; }
        }
        protected override ConditionValue getCValueColorCode() { return this.ColorCode; }


        public BsTColorInfoDetailGtCQ AddOrderBy_ColorCode_Asc() { regOBA("COLOR_CODE");return this; }
        public BsTColorInfoDetailGtCQ AddOrderBy_ColorCode_Desc() { regOBD("COLOR_CODE");return this; }

        protected ConditionValue _patternCode;
        public ConditionValue PatternCode {
            get { if (_patternCode == null) { _patternCode = new ConditionValue(); } return _patternCode; }
        }
        protected override ConditionValue getCValuePatternCode() { return this.PatternCode; }


        public BsTColorInfoDetailGtCQ AddOrderBy_PatternCode_Asc() { regOBA("PATTERN_CODE");return this; }
        public BsTColorInfoDetailGtCQ AddOrderBy_PatternCode_Desc() { regOBD("PATTERN_CODE");return this; }

        protected ConditionValue _colorSetInfoGtId;
        public ConditionValue ColorSetInfoGtId {
            get { if (_colorSetInfoGtId == null) { _colorSetInfoGtId = new ConditionValue(); } return _colorSetInfoGtId; }
        }
        protected override ConditionValue getCValueColorSetInfoGtId() { return this.ColorSetInfoGtId; }


        protected Map<String, TColorSetInfoGtCQ> _colorSetInfoGtId_InScopeSubQuery_TColorSetInfoGtMap;
        public Map<String, TColorSetInfoGtCQ> ColorSetInfoGtId_InScopeSubQuery_TColorSetInfoGt { get { return _colorSetInfoGtId_InScopeSubQuery_TColorSetInfoGtMap; }}
        public override String keepColorSetInfoGtId_InScopeSubQuery_TColorSetInfoGt(TColorSetInfoGtCQ subQuery) {
            if (_colorSetInfoGtId_InScopeSubQuery_TColorSetInfoGtMap == null) { _colorSetInfoGtId_InScopeSubQuery_TColorSetInfoGtMap = new LinkedHashMap<String, TColorSetInfoGtCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoGtId_InScopeSubQuery_TColorSetInfoGtMap.size() + 1);
            _colorSetInfoGtId_InScopeSubQuery_TColorSetInfoGtMap.put(key, subQuery); return "ColorSetInfoGtId_InScopeSubQuery_TColorSetInfoGt." + key;
        }

        protected Map<String, TColorSetInfoGtCQ> _colorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGtMap;
        public Map<String, TColorSetInfoGtCQ> ColorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGt { get { return _colorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGtMap; }}
        public override String keepColorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGt(TColorSetInfoGtCQ subQuery) {
            if (_colorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGtMap == null) { _colorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGtMap = new LinkedHashMap<String, TColorSetInfoGtCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGtMap.size() + 1);
            _colorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGtMap.put(key, subQuery); return "ColorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGt." + key;
        }

        public BsTColorInfoDetailGtCQ AddOrderBy_ColorSetInfoGtId_Asc() { regOBA("COLOR_SET_INFO_GT_ID");return this; }
        public BsTColorInfoDetailGtCQ AddOrderBy_ColorSetInfoGtId_Desc() { regOBD("COLOR_SET_INFO_GT_ID");return this; }

        public BsTColorInfoDetailGtCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTColorInfoDetailGtCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TColorInfoDetailGtCQ baseQuery = (TColorInfoDetailGtCQ)baseQueryAsSuper;
            TColorInfoDetailGtCQ unionQuery = (TColorInfoDetailGtCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTColorSetInfoGt()) {
                unionQuery.QueryTColorSetInfoGt().reflectRelationOnUnionQuery(baseQuery.QueryTColorSetInfoGt(), unionQuery.QueryTColorSetInfoGt());
            }

        }
    
        protected TColorSetInfoGtCQ _conditionQueryTColorSetInfoGt;
        public TColorSetInfoGtCQ QueryTColorSetInfoGt() {
            return this.ConditionQueryTColorSetInfoGt;
        }
        public TColorSetInfoGtCQ ConditionQueryTColorSetInfoGt {
            get {
                if (_conditionQueryTColorSetInfoGt == null) {
                    _conditionQueryTColorSetInfoGt = xcreateQueryTColorSetInfoGt();
                    xsetupOuterJoin_TColorSetInfoGt();
                }
                return _conditionQueryTColorSetInfoGt;
            }
        }
        protected TColorSetInfoGtCQ xcreateQueryTColorSetInfoGt() {
            String nrp = resolveNextRelationPathTColorSetInfoGt();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TColorSetInfoGtCQ cq = new TColorSetInfoGtCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tColorSetInfoGt"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TColorSetInfoGt() {
            TColorSetInfoGtCQ cq = ConditionQueryTColorSetInfoGt;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTColorSetInfoGt() {
            return resolveNextRelationPath("T_COLOR_INFO_DETAIL_GT", "tColorSetInfoGt");
        }
        public bool hasConditionQueryTColorSetInfoGt() {
            return _conditionQueryTColorSetInfoGt != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TColorInfoDetailGtCQ> _scalarSubQueryMap;
	    public Map<String, TColorInfoDetailGtCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TColorInfoDetailGtCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TColorInfoDetailGtCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TColorInfoDetailGtCQ> _myselfInScopeSubQueryMap;
        public Map<String, TColorInfoDetailGtCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TColorInfoDetailGtCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TColorInfoDetailGtCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
