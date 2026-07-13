
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTNoticeCQ : AbstractBsTNoticeCQ {

        protected TNoticeCIQ _inlineQuery;

        public BsTNoticeCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TNoticeCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TNoticeCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TNoticeCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TNoticeCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _noticeId;
        public ConditionValue NoticeId {
            get { if (_noticeId == null) { _noticeId = new ConditionValue(); } return _noticeId; }
        }
        protected override ConditionValue getCValueNoticeId() { return this.NoticeId; }


        public BsTNoticeCQ AddOrderBy_NoticeId_Asc() { regOBA("NOTICE_ID");return this; }
        public BsTNoticeCQ AddOrderBy_NoticeId_Desc() { regOBD("NOTICE_ID");return this; }

        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        public BsTNoticeCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTNoticeCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _userId;
        public ConditionValue UserId {
            get { if (_userId == null) { _userId = new ConditionValue(); } return _userId; }
        }
        protected override ConditionValue getCValueUserId() { return this.UserId; }


        public BsTNoticeCQ AddOrderBy_UserId_Asc() { regOBA("USER_ID");return this; }
        public BsTNoticeCQ AddOrderBy_UserId_Desc() { regOBD("USER_ID");return this; }

        protected ConditionValue _deleteFlag;
        public ConditionValue DeleteFlag {
            get { if (_deleteFlag == null) { _deleteFlag = new ConditionValue(); } return _deleteFlag; }
        }
        protected override ConditionValue getCValueDeleteFlag() { return this.DeleteFlag; }


        public BsTNoticeCQ AddOrderBy_DeleteFlag_Asc() { regOBA("DELETE_FLAG");return this; }
        public BsTNoticeCQ AddOrderBy_DeleteFlag_Desc() { regOBD("DELETE_FLAG");return this; }

        protected ConditionValue _noticeInfo;
        public ConditionValue NoticeInfo {
            get { if (_noticeInfo == null) { _noticeInfo = new ConditionValue(); } return _noticeInfo; }
        }
        protected override ConditionValue getCValueNoticeInfo() { return this.NoticeInfo; }


        public BsTNoticeCQ AddOrderBy_NoticeInfo_Asc() { regOBA("NOTICE_INFO");return this; }
        public BsTNoticeCQ AddOrderBy_NoticeInfo_Desc() { regOBD("NOTICE_INFO");return this; }

        protected ConditionValue _noticeType;
        public ConditionValue NoticeType {
            get { if (_noticeType == null) { _noticeType = new ConditionValue(); } return _noticeType; }
        }
        protected override ConditionValue getCValueNoticeType() { return this.NoticeType; }


        public BsTNoticeCQ AddOrderBy_NoticeType_Asc() { regOBA("NOTICE_TYPE");return this; }
        public BsTNoticeCQ AddOrderBy_NoticeType_Desc() { regOBD("NOTICE_TYPE");return this; }

        protected ConditionValue _linkUrl;
        public ConditionValue LinkUrl {
            get { if (_linkUrl == null) { _linkUrl = new ConditionValue(); } return _linkUrl; }
        }
        protected override ConditionValue getCValueLinkUrl() { return this.LinkUrl; }


        public BsTNoticeCQ AddOrderBy_LinkUrl_Asc() { regOBA("LINK_URL");return this; }
        public BsTNoticeCQ AddOrderBy_LinkUrl_Desc() { regOBD("LINK_URL");return this; }

        protected ConditionValue _expirationStartdate;
        public ConditionValue ExpirationStartdate {
            get { if (_expirationStartdate == null) { _expirationStartdate = new ConditionValue(); } return _expirationStartdate; }
        }
        protected override ConditionValue getCValueExpirationStartdate() { return this.ExpirationStartdate; }


        public BsTNoticeCQ AddOrderBy_ExpirationStartdate_Asc() { regOBA("EXPIRATION_STARTDATE");return this; }
        public BsTNoticeCQ AddOrderBy_ExpirationStartdate_Desc() { regOBD("EXPIRATION_STARTDATE");return this; }

        protected ConditionValue _expirationEnddate;
        public ConditionValue ExpirationEnddate {
            get { if (_expirationEnddate == null) { _expirationEnddate = new ConditionValue(); } return _expirationEnddate; }
        }
        protected override ConditionValue getCValueExpirationEnddate() { return this.ExpirationEnddate; }


        public BsTNoticeCQ AddOrderBy_ExpirationEnddate_Asc() { regOBA("EXPIRATION_ENDDATE");return this; }
        public BsTNoticeCQ AddOrderBy_ExpirationEnddate_Desc() { regOBD("EXPIRATION_ENDDATE");return this; }

        public BsTNoticeCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTNoticeCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TNoticeCQ baseQuery = (TNoticeCQ)baseQueryAsSuper;
            TNoticeCQ unionQuery = (TNoticeCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
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
            return resolveNextRelationPath("T_NOTICE", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TNoticeCQ> _scalarSubQueryMap;
	    public Map<String, TNoticeCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TNoticeCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TNoticeCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TNoticeCQ> _myselfInScopeSubQueryMap;
        public Map<String, TNoticeCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TNoticeCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TNoticeCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
