
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputSettingGtCQ : AbstractBsTOutputSettingGtCQ {

        protected TOutputSettingGtCIQ _inlineQuery;

        public BsTOutputSettingGtCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputSettingGtCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputSettingGtCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputSettingGtCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputSettingGtCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        public BsTOutputSettingGtCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTOutputSettingGtCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _gtNpFlag;
        public ConditionValue GtNpFlag {
            get { if (_gtNpFlag == null) { _gtNpFlag = new ConditionValue(); } return _gtNpFlag; }
        }
        protected override ConditionValue getCValueGtNpFlag() { return this.GtNpFlag; }


        public BsTOutputSettingGtCQ AddOrderBy_GtNpFlag_Asc() { regOBA("GT_NP_FLAG");return this; }
        public BsTOutputSettingGtCQ AddOrderBy_GtNpFlag_Desc() { regOBD("GT_NP_FLAG");return this; }

        protected ConditionValue _gtNFlag;
        public ConditionValue GtNFlag {
            get { if (_gtNFlag == null) { _gtNFlag = new ConditionValue(); } return _gtNFlag; }
        }
        protected override ConditionValue getCValueGtNFlag() { return this.GtNFlag; }


        public BsTOutputSettingGtCQ AddOrderBy_GtNFlag_Asc() { regOBA("GT_N_FLAG");return this; }
        public BsTOutputSettingGtCQ AddOrderBy_GtNFlag_Desc() { regOBD("GT_N_FLAG");return this; }

        protected ConditionValue _gtPFlag;
        public ConditionValue GtPFlag {
            get { if (_gtPFlag == null) { _gtPFlag = new ConditionValue(); } return _gtPFlag; }
        }
        protected override ConditionValue getCValueGtPFlag() { return this.GtPFlag; }


        public BsTOutputSettingGtCQ AddOrderBy_GtPFlag_Asc() { regOBA("GT_P_FLAG");return this; }
        public BsTOutputSettingGtCQ AddOrderBy_GtPFlag_Desc() { regOBD("GT_P_FLAG");return this; }

        protected ConditionValue _pageSettingNpFlag;
        public ConditionValue PageSettingNpFlag {
            get { if (_pageSettingNpFlag == null) { _pageSettingNpFlag = new ConditionValue(); } return _pageSettingNpFlag; }
        }
        protected override ConditionValue getCValuePageSettingNpFlag() { return this.PageSettingNpFlag; }


        public BsTOutputSettingGtCQ AddOrderBy_PageSettingNpFlag_Asc() { regOBA("PAGE_SETTING_NP_FLAG");return this; }
        public BsTOutputSettingGtCQ AddOrderBy_PageSettingNpFlag_Desc() { regOBD("PAGE_SETTING_NP_FLAG");return this; }

        protected ConditionValue _pageSettingNFlag;
        public ConditionValue PageSettingNFlag {
            get { if (_pageSettingNFlag == null) { _pageSettingNFlag = new ConditionValue(); } return _pageSettingNFlag; }
        }
        protected override ConditionValue getCValuePageSettingNFlag() { return this.PageSettingNFlag; }


        public BsTOutputSettingGtCQ AddOrderBy_PageSettingNFlag_Asc() { regOBA("PAGE_SETTING_N_FLAG");return this; }
        public BsTOutputSettingGtCQ AddOrderBy_PageSettingNFlag_Desc() { regOBD("PAGE_SETTING_N_FLAG");return this; }

        protected ConditionValue _pageSettingPFlag;
        public ConditionValue PageSettingPFlag {
            get { if (_pageSettingPFlag == null) { _pageSettingPFlag = new ConditionValue(); } return _pageSettingPFlag; }
        }
        protected override ConditionValue getCValuePageSettingPFlag() { return this.PageSettingPFlag; }


        public BsTOutputSettingGtCQ AddOrderBy_PageSettingPFlag_Asc() { regOBA("PAGE_SETTING_P_FLAG");return this; }
        public BsTOutputSettingGtCQ AddOrderBy_PageSettingPFlag_Desc() { regOBD("PAGE_SETTING_P_FLAG");return this; }

        protected ConditionValue _pageSettingPaperSize;
        public ConditionValue PageSettingPaperSize {
            get { if (_pageSettingPaperSize == null) { _pageSettingPaperSize = new ConditionValue(); } return _pageSettingPaperSize; }
        }
        protected override ConditionValue getCValuePageSettingPaperSize() { return this.PageSettingPaperSize; }


        public BsTOutputSettingGtCQ AddOrderBy_PageSettingPaperSize_Asc() { regOBA("PAGE_SETTING_PAPER_SIZE");return this; }
        public BsTOutputSettingGtCQ AddOrderBy_PageSettingPaperSize_Desc() { regOBD("PAGE_SETTING_PAPER_SIZE");return this; }

        protected ConditionValue _pageSettingPaperOrientation;
        public ConditionValue PageSettingPaperOrientation {
            get { if (_pageSettingPaperOrientation == null) { _pageSettingPaperOrientation = new ConditionValue(); } return _pageSettingPaperOrientation; }
        }
        protected override ConditionValue getCValuePageSettingPaperOrientation() { return this.PageSettingPaperOrientation; }


        public BsTOutputSettingGtCQ AddOrderBy_PageSettingPaperOrientation_Asc() { regOBA("PAGE_SETTING_PAPER_ORIENTATION");return this; }
        public BsTOutputSettingGtCQ AddOrderBy_PageSettingPaperOrientation_Desc() { regOBD("PAGE_SETTING_PAPER_ORIENTATION");return this; }

        protected ConditionValue _outputGraphFlag;
        public ConditionValue OutputGraphFlag {
            get { if (_outputGraphFlag == null) { _outputGraphFlag = new ConditionValue(); } return _outputGraphFlag; }
        }
        protected override ConditionValue getCValueOutputGraphFlag() { return this.OutputGraphFlag; }


        public BsTOutputSettingGtCQ AddOrderBy_OutputGraphFlag_Asc() { regOBA("OUTPUT_GRAPH_FLAG");return this; }
        public BsTOutputSettingGtCQ AddOrderBy_OutputGraphFlag_Desc() { regOBD("OUTPUT_GRAPH_FLAG");return this; }

        public BsTOutputSettingGtCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputSettingGtCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputSettingGtCQ baseQuery = (TOutputSettingGtCQ)baseQueryAsSuper;
            TOutputSettingGtCQ unionQuery = (TOutputSettingGtCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
            }
            if (baseQuery.hasConditionQueryTQcwebSurveyInfoAsOne()) {
                unionQuery.QueryTQcwebSurveyInfoAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfoAsOne(), unionQuery.QueryTQcwebSurveyInfoAsOne());
            }

        }
    
        protected TQcwebSurveyInfoCQ _conditionQueryTQcwebSurveyInfo;
        public TQcwebSurveyInfoCQ QueryTQcwebSurveyInfo() {
            return this.ConditionQueryTQcwebSurveyInfo;
        }
        public TQcwebSurveyInfoCQ ConditionQueryTQcwebSurveyInfo {
            get {
                if (_conditionQueryTQcwebSurveyInfo == null) {
                    _conditionQueryTQcwebSurveyInfo = xcreateQueryTQcwebSurveyInfo();
                    xsetupOuterJoin_TQcwebSurveyInfo();
                }
                return _conditionQueryTQcwebSurveyInfo;
            }
        }
        protected TQcwebSurveyInfoCQ xcreateQueryTQcwebSurveyInfo() {
            String nrp = resolveNextRelationPathTQcwebSurveyInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TQcwebSurveyInfoCQ cq = new TQcwebSurveyInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tQcwebSurveyInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TQcwebSurveyInfo() {
            TQcwebSurveyInfoCQ cq = ConditionQueryTQcwebSurveyInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTQcwebSurveyInfo() {
            return resolveNextRelationPath("T_OUTPUT_SETTING_GT", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }


        protected TQcwebSurveyInfoCQ _conditionQueryTQcwebSurveyInfoAsOne;
        public TQcwebSurveyInfoCQ ConditionQueryTQcwebSurveyInfoAsOne {
            get {
                if (_conditionQueryTQcwebSurveyInfoAsOne == null) {
                    _conditionQueryTQcwebSurveyInfoAsOne = createQueryTQcwebSurveyInfoAsOne();
                    xsetupOuterJoin_TQcwebSurveyInfoAsOne();
                }
                return _conditionQueryTQcwebSurveyInfoAsOne;
            }
        }
        public TQcwebSurveyInfoCQ QueryTQcwebSurveyInfoAsOne() { return this.ConditionQueryTQcwebSurveyInfoAsOne; }
        protected TQcwebSurveyInfoCQ createQueryTQcwebSurveyInfoAsOne() {
            String nrp = resolveNextRelationPathTQcwebSurveyInfoAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TQcwebSurveyInfoCQ cq = new TQcwebSurveyInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tQcwebSurveyInfoAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TQcwebSurveyInfoAsOne() {
            TQcwebSurveyInfoCQ cq = ConditionQueryTQcwebSurveyInfoAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTQcwebSurveyInfoAsOne() {
            return resolveNextRelationPath("T_OUTPUT_SETTING_GT", "tQcwebSurveyInfoAsOne");
        }
        public bool hasConditionQueryTQcwebSurveyInfoAsOne() {
            return _conditionQueryTQcwebSurveyInfoAsOne != null;
        }

	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputSettingGtCQ> _scalarSubQueryMap;
	    public Map<String, TOutputSettingGtCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputSettingGtCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputSettingGtCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputSettingGtCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputSettingGtCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputSettingGtCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputSettingGtCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
