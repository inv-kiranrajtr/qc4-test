
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputSettingCrossCQ : AbstractBsTOutputSettingCrossCQ {

        protected TOutputSettingCrossCIQ _inlineQuery;

        public BsTOutputSettingCrossCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputSettingCrossCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputSettingCrossCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputSettingCrossCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputSettingCrossCIQ inlineQuery = Inline();
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

        public BsTOutputSettingCrossCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTOutputSettingCrossCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _outputType;
        public ConditionValue OutputType {
            get { if (_outputType == null) { _outputType = new ConditionValue(); } return _outputType; }
        }
        protected override ConditionValue getCValueOutputType() { return this.OutputType; }


        public BsTOutputSettingCrossCQ AddOrderBy_OutputType_Asc() { regOBA("OUTPUT_TYPE");return this; }
        public BsTOutputSettingCrossCQ AddOrderBy_OutputType_Desc() { regOBD("OUTPUT_TYPE");return this; }

        protected ConditionValue _crossNpFlag;
        public ConditionValue CrossNpFlag {
            get { if (_crossNpFlag == null) { _crossNpFlag = new ConditionValue(); } return _crossNpFlag; }
        }
        protected override ConditionValue getCValueCrossNpFlag() { return this.CrossNpFlag; }


        public BsTOutputSettingCrossCQ AddOrderBy_CrossNpFlag_Asc() { regOBA("CROSS_NP_FLAG");return this; }
        public BsTOutputSettingCrossCQ AddOrderBy_CrossNpFlag_Desc() { regOBD("CROSS_NP_FLAG");return this; }

        protected ConditionValue _crossNFlag;
        public ConditionValue CrossNFlag {
            get { if (_crossNFlag == null) { _crossNFlag = new ConditionValue(); } return _crossNFlag; }
        }
        protected override ConditionValue getCValueCrossNFlag() { return this.CrossNFlag; }


        public BsTOutputSettingCrossCQ AddOrderBy_CrossNFlag_Asc() { regOBA("CROSS_N_FLAG");return this; }
        public BsTOutputSettingCrossCQ AddOrderBy_CrossNFlag_Desc() { regOBD("CROSS_N_FLAG");return this; }

        protected ConditionValue _crossPFlag;
        public ConditionValue CrossPFlag {
            get { if (_crossPFlag == null) { _crossPFlag = new ConditionValue(); } return _crossPFlag; }
        }
        protected override ConditionValue getCValueCrossPFlag() { return this.CrossPFlag; }


        public BsTOutputSettingCrossCQ AddOrderBy_CrossPFlag_Asc() { regOBA("CROSS_P_FLAG");return this; }
        public BsTOutputSettingCrossCQ AddOrderBy_CrossPFlag_Desc() { regOBD("CROSS_P_FLAG");return this; }

        protected ConditionValue _pageSettingNpFlag;
        public ConditionValue PageSettingNpFlag {
            get { if (_pageSettingNpFlag == null) { _pageSettingNpFlag = new ConditionValue(); } return _pageSettingNpFlag; }
        }
        protected override ConditionValue getCValuePageSettingNpFlag() { return this.PageSettingNpFlag; }


        public BsTOutputSettingCrossCQ AddOrderBy_PageSettingNpFlag_Asc() { regOBA("PAGE_SETTING_NP_FLAG");return this; }
        public BsTOutputSettingCrossCQ AddOrderBy_PageSettingNpFlag_Desc() { regOBD("PAGE_SETTING_NP_FLAG");return this; }

        protected ConditionValue _pageSettingNFlag;
        public ConditionValue PageSettingNFlag {
            get { if (_pageSettingNFlag == null) { _pageSettingNFlag = new ConditionValue(); } return _pageSettingNFlag; }
        }
        protected override ConditionValue getCValuePageSettingNFlag() { return this.PageSettingNFlag; }


        public BsTOutputSettingCrossCQ AddOrderBy_PageSettingNFlag_Asc() { regOBA("PAGE_SETTING_N_FLAG");return this; }
        public BsTOutputSettingCrossCQ AddOrderBy_PageSettingNFlag_Desc() { regOBD("PAGE_SETTING_N_FLAG");return this; }

        protected ConditionValue _pageSettingPFlag;
        public ConditionValue PageSettingPFlag {
            get { if (_pageSettingPFlag == null) { _pageSettingPFlag = new ConditionValue(); } return _pageSettingPFlag; }
        }
        protected override ConditionValue getCValuePageSettingPFlag() { return this.PageSettingPFlag; }


        public BsTOutputSettingCrossCQ AddOrderBy_PageSettingPFlag_Asc() { regOBA("PAGE_SETTING_P_FLAG");return this; }
        public BsTOutputSettingCrossCQ AddOrderBy_PageSettingPFlag_Desc() { regOBD("PAGE_SETTING_P_FLAG");return this; }

        protected ConditionValue _pageSettingPaperSize;
        public ConditionValue PageSettingPaperSize {
            get { if (_pageSettingPaperSize == null) { _pageSettingPaperSize = new ConditionValue(); } return _pageSettingPaperSize; }
        }
        protected override ConditionValue getCValuePageSettingPaperSize() { return this.PageSettingPaperSize; }


        public BsTOutputSettingCrossCQ AddOrderBy_PageSettingPaperSize_Asc() { regOBA("PAGE_SETTING_PAPER_SIZE");return this; }
        public BsTOutputSettingCrossCQ AddOrderBy_PageSettingPaperSize_Desc() { regOBD("PAGE_SETTING_PAPER_SIZE");return this; }

        protected ConditionValue _pageSettingPaperOrientation;
        public ConditionValue PageSettingPaperOrientation {
            get { if (_pageSettingPaperOrientation == null) { _pageSettingPaperOrientation = new ConditionValue(); } return _pageSettingPaperOrientation; }
        }
        protected override ConditionValue getCValuePageSettingPaperOrientation() { return this.PageSettingPaperOrientation; }


        public BsTOutputSettingCrossCQ AddOrderBy_PageSettingPaperOrientation_Asc() { regOBA("PAGE_SETTING_PAPER_ORIENTATION");return this; }
        public BsTOutputSettingCrossCQ AddOrderBy_PageSettingPaperOrientation_Desc() { regOBD("PAGE_SETTING_PAPER_ORIENTATION");return this; }

        public BsTOutputSettingCrossCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputSettingCrossCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputSettingCrossCQ baseQuery = (TOutputSettingCrossCQ)baseQueryAsSuper;
            TOutputSettingCrossCQ unionQuery = (TOutputSettingCrossCQ)unionQueryAsSuper;
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
            return resolveNextRelationPath("T_OUTPUT_SETTING_CROSS", "tQcwebSurveyInfo");
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
            return resolveNextRelationPath("T_OUTPUT_SETTING_CROSS", "tQcwebSurveyInfoAsOne");
        }
        public bool hasConditionQueryTQcwebSurveyInfoAsOne() {
            return _conditionQueryTQcwebSurveyInfoAsOne != null;
        }

	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputSettingCrossCQ> _scalarSubQueryMap;
	    public Map<String, TOutputSettingCrossCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputSettingCrossCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputSettingCrossCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputSettingCrossCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputSettingCrossCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputSettingCrossCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputSettingCrossCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
