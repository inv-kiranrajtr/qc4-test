
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputSubGtCQ : AbstractBsTOutputSubGtCQ {

        protected TOutputSubGtCIQ _inlineQuery;

        public BsTOutputSubGtCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputSubGtCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputSubGtCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputSubGtCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputSubGtCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _outputSubGtId;
        public ConditionValue OutputSubGtId {
            get { if (_outputSubGtId == null) { _outputSubGtId = new ConditionValue(); } return _outputSubGtId; }
        }
        protected override ConditionValue getCValueOutputSubGtId() { return this.OutputSubGtId; }


        public BsTOutputSubGtCQ AddOrderBy_OutputSubGtId_Asc() { regOBA("OUTPUT_SUB_GT_ID");return this; }
        public BsTOutputSubGtCQ AddOrderBy_OutputSubGtId_Desc() { regOBD("OUTPUT_SUB_GT_ID");return this; }

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

        public BsTOutputSubGtCQ AddOrderBy_OutputCommonId_Asc() { regOBA("OUTPUT_COMMON_ID");return this; }
        public BsTOutputSubGtCQ AddOrderBy_OutputCommonId_Desc() { regOBD("OUTPUT_COMMON_ID");return this; }

        protected ConditionValue _outputTableType;
        public ConditionValue OutputTableType {
            get { if (_outputTableType == null) { _outputTableType = new ConditionValue(); } return _outputTableType; }
        }
        protected override ConditionValue getCValueOutputTableType() { return this.OutputTableType; }


        public BsTOutputSubGtCQ AddOrderBy_OutputTableType_Asc() { regOBA("OUTPUT_TABLE_TYPE");return this; }
        public BsTOutputSubGtCQ AddOrderBy_OutputTableType_Desc() { regOBD("OUTPUT_TABLE_TYPE");return this; }

        protected ConditionValue _outputTableOrientation;
        public ConditionValue OutputTableOrientation {
            get { if (_outputTableOrientation == null) { _outputTableOrientation = new ConditionValue(); } return _outputTableOrientation; }
        }
        protected override ConditionValue getCValueOutputTableOrientation() { return this.OutputTableOrientation; }


        public BsTOutputSubGtCQ AddOrderBy_OutputTableOrientation_Asc() { regOBA("OUTPUT_TABLE_ORIENTATION");return this; }
        public BsTOutputSubGtCQ AddOrderBy_OutputTableOrientation_Desc() { regOBD("OUTPUT_TABLE_ORIENTATION");return this; }

        protected ConditionValue _pageSettingTableType;
        public ConditionValue PageSettingTableType {
            get { if (_pageSettingTableType == null) { _pageSettingTableType = new ConditionValue(); } return _pageSettingTableType; }
        }
        protected override ConditionValue getCValuePageSettingTableType() { return this.PageSettingTableType; }


        public BsTOutputSubGtCQ AddOrderBy_PageSettingTableType_Asc() { regOBA("PAGE_SETTING_TABLE_TYPE");return this; }
        public BsTOutputSubGtCQ AddOrderBy_PageSettingTableType_Desc() { regOBD("PAGE_SETTING_TABLE_TYPE");return this; }

        protected ConditionValue _pageSettingPaperSize;
        public ConditionValue PageSettingPaperSize {
            get { if (_pageSettingPaperSize == null) { _pageSettingPaperSize = new ConditionValue(); } return _pageSettingPaperSize; }
        }
        protected override ConditionValue getCValuePageSettingPaperSize() { return this.PageSettingPaperSize; }


        public BsTOutputSubGtCQ AddOrderBy_PageSettingPaperSize_Asc() { regOBA("PAGE_SETTING_PAPER_SIZE");return this; }
        public BsTOutputSubGtCQ AddOrderBy_PageSettingPaperSize_Desc() { regOBD("PAGE_SETTING_PAPER_SIZE");return this; }

        protected ConditionValue _pageSettingPaperOrientation;
        public ConditionValue PageSettingPaperOrientation {
            get { if (_pageSettingPaperOrientation == null) { _pageSettingPaperOrientation = new ConditionValue(); } return _pageSettingPaperOrientation; }
        }
        protected override ConditionValue getCValuePageSettingPaperOrientation() { return this.PageSettingPaperOrientation; }


        public BsTOutputSubGtCQ AddOrderBy_PageSettingPaperOrientation_Asc() { regOBA("PAGE_SETTING_PAPER_ORIENTATION");return this; }
        public BsTOutputSubGtCQ AddOrderBy_PageSettingPaperOrientation_Desc() { regOBD("PAGE_SETTING_PAPER_ORIENTATION");return this; }

        protected ConditionValue _markingLevel;
        public ConditionValue MarkingLevel {
            get { if (_markingLevel == null) { _markingLevel = new ConditionValue(); } return _markingLevel; }
        }
        protected override ConditionValue getCValueMarkingLevel() { return this.MarkingLevel; }


        public BsTOutputSubGtCQ AddOrderBy_MarkingLevel_Asc() { regOBA("MARKING_LEVEL");return this; }
        public BsTOutputSubGtCQ AddOrderBy_MarkingLevel_Desc() { regOBD("MARKING_LEVEL");return this; }

        protected ConditionValue _markingMinParameter;
        public ConditionValue MarkingMinParameter {
            get { if (_markingMinParameter == null) { _markingMinParameter = new ConditionValue(); } return _markingMinParameter; }
        }
        protected override ConditionValue getCValueMarkingMinParameter() { return this.MarkingMinParameter; }


        public BsTOutputSubGtCQ AddOrderBy_MarkingMinParameter_Asc() { regOBA("MARKING_MIN_PARAMETER");return this; }
        public BsTOutputSubGtCQ AddOrderBy_MarkingMinParameter_Desc() { regOBD("MARKING_MIN_PARAMETER");return this; }

        protected ConditionValue _markingCode;
        public ConditionValue MarkingCode {
            get { if (_markingCode == null) { _markingCode = new ConditionValue(); } return _markingCode; }
        }
        protected override ConditionValue getCValueMarkingCode() { return this.MarkingCode; }


        public BsTOutputSubGtCQ AddOrderBy_MarkingCode_Asc() { regOBA("MARKING_CODE");return this; }
        public BsTOutputSubGtCQ AddOrderBy_MarkingCode_Desc() { regOBD("MARKING_CODE");return this; }

        protected ConditionValue _filteringExpression;
        public ConditionValue FilteringExpression {
            get { if (_filteringExpression == null) { _filteringExpression = new ConditionValue(); } return _filteringExpression; }
        }
        protected override ConditionValue getCValueFilteringExpression() { return this.FilteringExpression; }


        public BsTOutputSubGtCQ AddOrderBy_FilteringExpression_Asc() { regOBA("FILTERING_EXPRESSION");return this; }
        public BsTOutputSubGtCQ AddOrderBy_FilteringExpression_Desc() { regOBD("FILTERING_EXPRESSION");return this; }

        public BsTOutputSubGtCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputSubGtCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputSubGtCQ baseQuery = (TOutputSubGtCQ)baseQueryAsSuper;
            TOutputSubGtCQ unionQuery = (TOutputSubGtCQ)unionQueryAsSuper;
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
            return resolveNextRelationPath("T_OUTPUT_SUB_GT", "tOutputCommon");
        }
        public bool hasConditionQueryTOutputCommon() {
            return _conditionQueryTOutputCommon != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputSubGtCQ> _scalarSubQueryMap;
	    public Map<String, TOutputSubGtCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputSubGtCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputSubGtCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputSubGtCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputSubGtCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputSubGtCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputSubGtCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
