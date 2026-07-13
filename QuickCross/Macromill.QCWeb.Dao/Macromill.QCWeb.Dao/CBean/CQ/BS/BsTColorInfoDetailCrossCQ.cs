
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTColorInfoDetailCrossCQ : AbstractBsTColorInfoDetailCrossCQ {

        protected TColorInfoDetailCrossCIQ _inlineQuery;

        public BsTColorInfoDetailCrossCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TColorInfoDetailCrossCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TColorInfoDetailCrossCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TColorInfoDetailCrossCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TColorInfoDetailCrossCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _colorInfoDetailCrossId;
        public ConditionValue ColorInfoDetailCrossId {
            get { if (_colorInfoDetailCrossId == null) { _colorInfoDetailCrossId = new ConditionValue(); } return _colorInfoDetailCrossId; }
        }
        protected override ConditionValue getCValueColorInfoDetailCrossId() { return this.ColorInfoDetailCrossId; }


        public BsTColorInfoDetailCrossCQ AddOrderBy_ColorInfoDetailCrossId_Asc() { regOBA("COLOR_INFO_DETAIL_CROSS_ID");return this; }
        public BsTColorInfoDetailCrossCQ AddOrderBy_ColorInfoDetailCrossId_Desc() { regOBD("COLOR_INFO_DETAIL_CROSS_ID");return this; }

        protected ConditionValue _graphColorNo;
        public ConditionValue GraphColorNo {
            get { if (_graphColorNo == null) { _graphColorNo = new ConditionValue(); } return _graphColorNo; }
        }
        protected override ConditionValue getCValueGraphColorNo() { return this.GraphColorNo; }


        public BsTColorInfoDetailCrossCQ AddOrderBy_GraphColorNo_Asc() { regOBA("GRAPH_COLOR_NO");return this; }
        public BsTColorInfoDetailCrossCQ AddOrderBy_GraphColorNo_Desc() { regOBD("GRAPH_COLOR_NO");return this; }

        protected ConditionValue _colorCode;
        public ConditionValue ColorCode {
            get { if (_colorCode == null) { _colorCode = new ConditionValue(); } return _colorCode; }
        }
        protected override ConditionValue getCValueColorCode() { return this.ColorCode; }


        public BsTColorInfoDetailCrossCQ AddOrderBy_ColorCode_Asc() { regOBA("COLOR_CODE");return this; }
        public BsTColorInfoDetailCrossCQ AddOrderBy_ColorCode_Desc() { regOBD("COLOR_CODE");return this; }

        protected ConditionValue _patternCode;
        public ConditionValue PatternCode {
            get { if (_patternCode == null) { _patternCode = new ConditionValue(); } return _patternCode; }
        }
        protected override ConditionValue getCValuePatternCode() { return this.PatternCode; }


        public BsTColorInfoDetailCrossCQ AddOrderBy_PatternCode_Asc() { regOBA("PATTERN_CODE");return this; }
        public BsTColorInfoDetailCrossCQ AddOrderBy_PatternCode_Desc() { regOBD("PATTERN_CODE");return this; }

        protected ConditionValue _colorSetInfoCrossId;
        public ConditionValue ColorSetInfoCrossId {
            get { if (_colorSetInfoCrossId == null) { _colorSetInfoCrossId = new ConditionValue(); } return _colorSetInfoCrossId; }
        }
        protected override ConditionValue getCValueColorSetInfoCrossId() { return this.ColorSetInfoCrossId; }


        protected Map<String, TColorSetInfoCrossCQ> _colorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCrossMap;
        public Map<String, TColorSetInfoCrossCQ> ColorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCross { get { return _colorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCrossMap; }}
        public override String keepColorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCross(TColorSetInfoCrossCQ subQuery) {
            if (_colorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCrossMap == null) { _colorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCrossMap = new LinkedHashMap<String, TColorSetInfoCrossCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCrossMap.size() + 1);
            _colorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCrossMap.put(key, subQuery); return "ColorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCross." + key;
        }

        protected Map<String, TColorSetInfoCrossCQ> _colorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCrossMap;
        public Map<String, TColorSetInfoCrossCQ> ColorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCross { get { return _colorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCrossMap; }}
        public override String keepColorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCross(TColorSetInfoCrossCQ subQuery) {
            if (_colorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCrossMap == null) { _colorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCrossMap = new LinkedHashMap<String, TColorSetInfoCrossCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCrossMap.size() + 1);
            _colorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCrossMap.put(key, subQuery); return "ColorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCross." + key;
        }

        public BsTColorInfoDetailCrossCQ AddOrderBy_ColorSetInfoCrossId_Asc() { regOBA("COLOR_SET_INFO_CROSS_ID");return this; }
        public BsTColorInfoDetailCrossCQ AddOrderBy_ColorSetInfoCrossId_Desc() { regOBD("COLOR_SET_INFO_CROSS_ID");return this; }

        public BsTColorInfoDetailCrossCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTColorInfoDetailCrossCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TColorInfoDetailCrossCQ baseQuery = (TColorInfoDetailCrossCQ)baseQueryAsSuper;
            TColorInfoDetailCrossCQ unionQuery = (TColorInfoDetailCrossCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTColorSetInfoCross()) {
                unionQuery.QueryTColorSetInfoCross().reflectRelationOnUnionQuery(baseQuery.QueryTColorSetInfoCross(), unionQuery.QueryTColorSetInfoCross());
            }

        }
    
        protected TColorSetInfoCrossCQ _conditionQueryTColorSetInfoCross;
        public TColorSetInfoCrossCQ QueryTColorSetInfoCross() {
            return this.ConditionQueryTColorSetInfoCross;
        }
        public TColorSetInfoCrossCQ ConditionQueryTColorSetInfoCross {
            get {
                if (_conditionQueryTColorSetInfoCross == null) {
                    _conditionQueryTColorSetInfoCross = xcreateQueryTColorSetInfoCross();
                    xsetupOuterJoin_TColorSetInfoCross();
                }
                return _conditionQueryTColorSetInfoCross;
            }
        }
        protected TColorSetInfoCrossCQ xcreateQueryTColorSetInfoCross() {
            String nrp = resolveNextRelationPathTColorSetInfoCross();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TColorSetInfoCrossCQ cq = new TColorSetInfoCrossCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tColorSetInfoCross"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TColorSetInfoCross() {
            TColorSetInfoCrossCQ cq = ConditionQueryTColorSetInfoCross;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTColorSetInfoCross() {
            return resolveNextRelationPath("T_COLOR_INFO_DETAIL_CROSS", "tColorSetInfoCross");
        }
        public bool hasConditionQueryTColorSetInfoCross() {
            return _conditionQueryTColorSetInfoCross != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TColorInfoDetailCrossCQ> _scalarSubQueryMap;
	    public Map<String, TColorInfoDetailCrossCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TColorInfoDetailCrossCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TColorInfoDetailCrossCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TColorInfoDetailCrossCQ> _myselfInScopeSubQueryMap;
        public Map<String, TColorInfoDetailCrossCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TColorInfoDetailCrossCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TColorInfoDetailCrossCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
