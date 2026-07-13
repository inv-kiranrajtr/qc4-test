
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputSubFaCQ : AbstractBsTOutputSubFaCQ {

        protected TOutputSubFaCIQ _inlineQuery;

        public BsTOutputSubFaCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputSubFaCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputSubFaCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputSubFaCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputSubFaCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _outputSubFaId;
        public ConditionValue OutputSubFaId {
            get { if (_outputSubFaId == null) { _outputSubFaId = new ConditionValue(); } return _outputSubFaId; }
        }
        protected override ConditionValue getCValueOutputSubFaId() { return this.OutputSubFaId; }


        public BsTOutputSubFaCQ AddOrderBy_OutputSubFaId_Asc() { regOBA("OUTPUT_SUB_FA_ID");return this; }
        public BsTOutputSubFaCQ AddOrderBy_OutputSubFaId_Desc() { regOBD("OUTPUT_SUB_FA_ID");return this; }

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

        public BsTOutputSubFaCQ AddOrderBy_OutputCommonId_Asc() { regOBA("OUTPUT_COMMON_ID");return this; }
        public BsTOutputSubFaCQ AddOrderBy_OutputCommonId_Desc() { regOBD("OUTPUT_COMMON_ID");return this; }

        protected ConditionValue _pageSettingPaperSize;
        public ConditionValue PageSettingPaperSize {
            get { if (_pageSettingPaperSize == null) { _pageSettingPaperSize = new ConditionValue(); } return _pageSettingPaperSize; }
        }
        protected override ConditionValue getCValuePageSettingPaperSize() { return this.PageSettingPaperSize; }


        public BsTOutputSubFaCQ AddOrderBy_PageSettingPaperSize_Asc() { regOBA("PAGE_SETTING_PAPER_SIZE");return this; }
        public BsTOutputSubFaCQ AddOrderBy_PageSettingPaperSize_Desc() { regOBD("PAGE_SETTING_PAPER_SIZE");return this; }

        protected ConditionValue _pageSettingPaperOrientation;
        public ConditionValue PageSettingPaperOrientation {
            get { if (_pageSettingPaperOrientation == null) { _pageSettingPaperOrientation = new ConditionValue(); } return _pageSettingPaperOrientation; }
        }
        protected override ConditionValue getCValuePageSettingPaperOrientation() { return this.PageSettingPaperOrientation; }


        public BsTOutputSubFaCQ AddOrderBy_PageSettingPaperOrientation_Asc() { regOBA("PAGE_SETTING_PAPER_ORIENTATION");return this; }
        public BsTOutputSubFaCQ AddOrderBy_PageSettingPaperOrientation_Desc() { regOBD("PAGE_SETTING_PAPER_ORIENTATION");return this; }

        protected ConditionValue _filteringExpression;
        public ConditionValue FilteringExpression {
            get { if (_filteringExpression == null) { _filteringExpression = new ConditionValue(); } return _filteringExpression; }
        }
        protected override ConditionValue getCValueFilteringExpression() { return this.FilteringExpression; }


        public BsTOutputSubFaCQ AddOrderBy_FilteringExpression_Asc() { regOBA("FILTERING_EXPRESSION");return this; }
        public BsTOutputSubFaCQ AddOrderBy_FilteringExpression_Desc() { regOBD("FILTERING_EXPRESSION");return this; }

        public BsTOutputSubFaCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputSubFaCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputSubFaCQ baseQuery = (TOutputSubFaCQ)baseQueryAsSuper;
            TOutputSubFaCQ unionQuery = (TOutputSubFaCQ)unionQueryAsSuper;
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
            return resolveNextRelationPath("T_OUTPUT_SUB_FA", "tOutputCommon");
        }
        public bool hasConditionQueryTOutputCommon() {
            return _conditionQueryTOutputCommon != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputSubFaCQ> _scalarSubQueryMap;
	    public Map<String, TOutputSubFaCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputSubFaCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputSubFaCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputSubFaCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputSubFaCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputSubFaCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputSubFaCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
