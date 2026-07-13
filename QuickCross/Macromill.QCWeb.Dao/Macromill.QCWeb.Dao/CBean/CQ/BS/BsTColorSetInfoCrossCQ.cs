
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTColorSetInfoCrossCQ : AbstractBsTColorSetInfoCrossCQ {

        protected TColorSetInfoCrossCIQ _inlineQuery;

        public BsTColorSetInfoCrossCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TColorSetInfoCrossCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TColorSetInfoCrossCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TColorSetInfoCrossCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TColorSetInfoCrossCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _colorSetInfoCrossId;
        public ConditionValue ColorSetInfoCrossId {
            get { if (_colorSetInfoCrossId == null) { _colorSetInfoCrossId = new ConditionValue(); } return _colorSetInfoCrossId; }
        }
        protected override ConditionValue getCValueColorSetInfoCrossId() { return this.ColorSetInfoCrossId; }


        protected Map<String, TColorInfoDetailCrossCQ> _colorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossListMap;
        public Map<String, TColorInfoDetailCrossCQ> ColorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossList { get { return _colorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossListMap; }}
        public override String keepColorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            if (_colorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossListMap == null) { _colorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossListMap = new LinkedHashMap<String, TColorInfoDetailCrossCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossListMap.size() + 1);
            _colorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossListMap.put(key, subQuery); return "ColorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossList." + key;
        }

        protected Map<String, TColorInfoDetailCrossCQ> _colorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossListMap;
        public Map<String, TColorInfoDetailCrossCQ> ColorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossList { get { return _colorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossListMap; }}
        public override String keepColorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            if (_colorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossListMap == null) { _colorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossListMap = new LinkedHashMap<String, TColorInfoDetailCrossCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossListMap.size() + 1);
            _colorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossListMap.put(key, subQuery); return "ColorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossList." + key;
        }

        protected Map<String, TColorInfoDetailCrossCQ> _colorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossListMap;
        public Map<String, TColorInfoDetailCrossCQ> ColorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossList { get { return _colorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossListMap; }}
        public override String keepColorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            if (_colorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossListMap == null) { _colorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossListMap = new LinkedHashMap<String, TColorInfoDetailCrossCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossListMap.size() + 1);
            _colorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossListMap.put(key, subQuery); return "ColorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossList." + key;
        }

        protected Map<String, TColorInfoDetailCrossCQ> _colorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossListMap;
        public Map<String, TColorInfoDetailCrossCQ> ColorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossList { get { return _colorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossListMap; }}
        public override String keepColorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            if (_colorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossListMap == null) { _colorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossListMap = new LinkedHashMap<String, TColorInfoDetailCrossCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossListMap.size() + 1);
            _colorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossListMap.put(key, subQuery); return "ColorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossList." + key;
        }

        protected Map<String, TColorInfoDetailCrossCQ> _colorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossListMap;
        public Map<String, TColorInfoDetailCrossCQ> ColorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossList { get { return _colorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossListMap; }}
        public override String keepColorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            if (_colorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossListMap == null) { _colorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossListMap = new LinkedHashMap<String, TColorInfoDetailCrossCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossListMap.size() + 1);
            _colorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossListMap.put(key, subQuery); return "ColorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossList." + key;
        }

        protected Map<String, TColorInfoDetailCrossCQ> _colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListMap;
        public Map<String, TColorInfoDetailCrossCQ> ColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossList { get { return _colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListMap; } }
        public override String keepColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery) {
            if (_colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListMap == null) { _colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListMap = new LinkedHashMap<String, TColorInfoDetailCrossCQ>(); }
            String key = "subQueryMapKey" + (_colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListMap.size() + 1);
            _colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListMap.put(key, subQuery); return "ColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossList." + key;
        }
        protected Map<String, Object> _colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameterMap;
        public Map<String, Object> ColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameter { get { return _colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameterMap; } }
        public override String keepColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameter(Object parameterValue) {
            if (_colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameterMap == null) { _colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameterMap.size() + 1);
            _colorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameterMap.put(key, parameterValue); return "ColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameter." + key;
        }

        public BsTColorSetInfoCrossCQ AddOrderBy_ColorSetInfoCrossId_Asc() { regOBA("COLOR_SET_INFO_CROSS_ID");return this; }
        public BsTColorSetInfoCrossCQ AddOrderBy_ColorSetInfoCrossId_Desc() { regOBD("COLOR_SET_INFO_CROSS_ID");return this; }

        protected ConditionValue _typeCode;
        public ConditionValue TypeCode {
            get { if (_typeCode == null) { _typeCode = new ConditionValue(); } return _typeCode; }
        }
        protected override ConditionValue getCValueTypeCode() { return this.TypeCode; }


        public BsTColorSetInfoCrossCQ AddOrderBy_TypeCode_Asc() { regOBA("TYPE_CODE");return this; }
        public BsTColorSetInfoCrossCQ AddOrderBy_TypeCode_Desc() { regOBD("TYPE_CODE");return this; }

        protected ConditionValue _gradationType;
        public ConditionValue GradationType {
            get { if (_gradationType == null) { _gradationType = new ConditionValue(); } return _gradationType; }
        }
        protected override ConditionValue getCValueGradationType() { return this.GradationType; }


        public BsTColorSetInfoCrossCQ AddOrderBy_GradationType_Asc() { regOBA("GRADATION_TYPE");return this; }
        public BsTColorSetInfoCrossCQ AddOrderBy_GradationType_Desc() { regOBD("GRADATION_TYPE");return this; }

        protected ConditionValue _crossScenarioTargetId;
        public ConditionValue CrossScenarioTargetId {
            get { if (_crossScenarioTargetId == null) { _crossScenarioTargetId = new ConditionValue(); } return _crossScenarioTargetId; }
        }
        protected override ConditionValue getCValueCrossScenarioTargetId() { return this.CrossScenarioTargetId; }


        protected Map<String, TCrossScenarioTargetCQ> _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap;
        public Map<String, TCrossScenarioTargetCQ> CrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget { get { return _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap; }}
        public override String keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget(TCrossScenarioTargetCQ subQuery) {
            if (_crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap == null) { _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap.size() + 1);
            _crossScenarioTargetId_InScopeSubQuery_TCrossScenarioTargetMap.put(key, subQuery); return "CrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget." + key;
        }

        protected Map<String, TCrossScenarioTargetCQ> _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap;
        public Map<String, TCrossScenarioTargetCQ> CrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget { get { return _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap; }}
        public override String keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget(TCrossScenarioTargetCQ subQuery) {
            if (_crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap == null) { _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap.size() + 1);
            _crossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTargetMap.put(key, subQuery); return "CrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget." + key;
        }

        public BsTColorSetInfoCrossCQ AddOrderBy_CrossScenarioTargetId_Asc() { regOBA("CROSS_SCENARIO_TARGET_ID");return this; }
        public BsTColorSetInfoCrossCQ AddOrderBy_CrossScenarioTargetId_Desc() { regOBD("CROSS_SCENARIO_TARGET_ID");return this; }

        public BsTColorSetInfoCrossCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTColorSetInfoCrossCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TColorSetInfoCrossCQ baseQuery = (TColorSetInfoCrossCQ)baseQueryAsSuper;
            TColorSetInfoCrossCQ unionQuery = (TColorSetInfoCrossCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTCrossScenarioTarget()) {
                unionQuery.QueryTCrossScenarioTarget().reflectRelationOnUnionQuery(baseQuery.QueryTCrossScenarioTarget(), unionQuery.QueryTCrossScenarioTarget());
            }

        }
    
        protected TCrossScenarioTargetCQ _conditionQueryTCrossScenarioTarget;
        public TCrossScenarioTargetCQ QueryTCrossScenarioTarget() {
            return this.ConditionQueryTCrossScenarioTarget;
        }
        public TCrossScenarioTargetCQ ConditionQueryTCrossScenarioTarget {
            get {
                if (_conditionQueryTCrossScenarioTarget == null) {
                    _conditionQueryTCrossScenarioTarget = xcreateQueryTCrossScenarioTarget();
                    xsetupOuterJoin_TCrossScenarioTarget();
                }
                return _conditionQueryTCrossScenarioTarget;
            }
        }
        protected TCrossScenarioTargetCQ xcreateQueryTCrossScenarioTarget() {
            String nrp = resolveNextRelationPathTCrossScenarioTarget();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TCrossScenarioTargetCQ cq = new TCrossScenarioTargetCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tCrossScenarioTarget"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TCrossScenarioTarget() {
            TCrossScenarioTargetCQ cq = ConditionQueryTCrossScenarioTarget;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTCrossScenarioTarget() {
            return resolveNextRelationPath("T_COLOR_SET_INFO_CROSS", "tCrossScenarioTarget");
        }
        public bool hasConditionQueryTCrossScenarioTarget() {
            return _conditionQueryTCrossScenarioTarget != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TColorSetInfoCrossCQ> _scalarSubQueryMap;
	    public Map<String, TColorSetInfoCrossCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TColorSetInfoCrossCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TColorSetInfoCrossCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TColorSetInfoCrossCQ> _myselfInScopeSubQueryMap;
        public Map<String, TColorSetInfoCrossCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TColorSetInfoCrossCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TColorSetInfoCrossCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
