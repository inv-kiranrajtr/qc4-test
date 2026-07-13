
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTAccessPermissionsInfoCQ : AbstractBsTAccessPermissionsInfoCQ {

        protected TAccessPermissionsInfoCIQ _inlineQuery;

        public BsTAccessPermissionsInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TAccessPermissionsInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TAccessPermissionsInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TAccessPermissionsInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TAccessPermissionsInfoCIQ inlineQuery = Inline();
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

        public BsTAccessPermissionsInfoCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTAccessPermissionsInfoCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _accessDatetime;
        public ConditionValue AccessDatetime {
            get { if (_accessDatetime == null) { _accessDatetime = new ConditionValue(); } return _accessDatetime; }
        }
        protected override ConditionValue getCValueAccessDatetime() { return this.AccessDatetime; }


        public BsTAccessPermissionsInfoCQ AddOrderBy_AccessDatetime_Asc() { regOBA("ACCESS_DATETIME");return this; }
        public BsTAccessPermissionsInfoCQ AddOrderBy_AccessDatetime_Desc() { regOBD("ACCESS_DATETIME");return this; }

        protected ConditionValue _clientId;
        public ConditionValue ClientId {
            get { if (_clientId == null) { _clientId = new ConditionValue(); } return _clientId; }
        }
        protected override ConditionValue getCValueClientId() { return this.ClientId; }


        public BsTAccessPermissionsInfoCQ AddOrderBy_ClientId_Asc() { regOBA("CLIENT_ID");return this; }
        public BsTAccessPermissionsInfoCQ AddOrderBy_ClientId_Desc() { regOBD("CLIENT_ID");return this; }

        protected ConditionValue _adminId;
        public ConditionValue AdminId {
            get { if (_adminId == null) { _adminId = new ConditionValue(); } return _adminId; }
        }
        protected override ConditionValue getCValueAdminId() { return this.AdminId; }


        public BsTAccessPermissionsInfoCQ AddOrderBy_AdminId_Asc() { regOBA("ADMIN_ID");return this; }
        public BsTAccessPermissionsInfoCQ AddOrderBy_AdminId_Desc() { regOBD("ADMIN_ID");return this; }

        protected ConditionValue _guestId;
        public ConditionValue GuestId {
            get { if (_guestId == null) { _guestId = new ConditionValue(); } return _guestId; }
        }
        protected override ConditionValue getCValueGuestId() { return this.GuestId; }


        public BsTAccessPermissionsInfoCQ AddOrderBy_GuestId_Asc() { regOBA("GUEST_ID");return this; }
        public BsTAccessPermissionsInfoCQ AddOrderBy_GuestId_Desc() { regOBD("GUEST_ID");return this; }

        public BsTAccessPermissionsInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTAccessPermissionsInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TAccessPermissionsInfoCQ baseQuery = (TAccessPermissionsInfoCQ)baseQueryAsSuper;
            TAccessPermissionsInfoCQ unionQuery = (TAccessPermissionsInfoCQ)unionQueryAsSuper;
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
            return resolveNextRelationPath("T_ACCESS_PERMISSIONS_INFO", "tQcwebSurveyInfo");
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
            return resolveNextRelationPath("T_ACCESS_PERMISSIONS_INFO", "tQcwebSurveyInfoAsOne");
        }
        public bool hasConditionQueryTQcwebSurveyInfoAsOne() {
            return _conditionQueryTQcwebSurveyInfoAsOne != null;
        }

	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TAccessPermissionsInfoCQ> _scalarSubQueryMap;
	    public Map<String, TAccessPermissionsInfoCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TAccessPermissionsInfoCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TAccessPermissionsInfoCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TAccessPermissionsInfoCQ> _myselfInScopeSubQueryMap;
        public Map<String, TAccessPermissionsInfoCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TAccessPermissionsInfoCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TAccessPermissionsInfoCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
