
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTGtMatrixChildCQ : AbstractBsTGtMatrixChildCQ {

        protected TGtMatrixChildCIQ _inlineQuery;

        public BsTGtMatrixChildCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TGtMatrixChildCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TGtMatrixChildCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TGtMatrixChildCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TGtMatrixChildCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _gtMatrixChildid;
        public ConditionValue GtMatrixChildid {
            get { if (_gtMatrixChildid == null) { _gtMatrixChildid = new ConditionValue(); } return _gtMatrixChildid; }
        }
        protected override ConditionValue getCValueGtMatrixChildid() { return this.GtMatrixChildid; }


        public BsTGtMatrixChildCQ AddOrderBy_GtMatrixChildid_Asc() { regOBA("GT_MATRIX_CHILDID");return this; }
        public BsTGtMatrixChildCQ AddOrderBy_GtMatrixChildid_Desc() { regOBD("GT_MATRIX_CHILDID");return this; }

        protected ConditionValue _gtMatrixInfoId;
        public ConditionValue GtMatrixInfoId {
            get { if (_gtMatrixInfoId == null) { _gtMatrixInfoId = new ConditionValue(); } return _gtMatrixInfoId; }
        }
        protected override ConditionValue getCValueGtMatrixInfoId() { return this.GtMatrixInfoId; }


        protected Map<String, TGtMatrixInfoCQ> _gtMatrixInfoId_InScopeSubQuery_TGtMatrixInfoMap;
        public Map<String, TGtMatrixInfoCQ> GtMatrixInfoId_InScopeSubQuery_TGtMatrixInfo { get { return _gtMatrixInfoId_InScopeSubQuery_TGtMatrixInfoMap; }}
        public override String keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixInfo(TGtMatrixInfoCQ subQuery) {
            if (_gtMatrixInfoId_InScopeSubQuery_TGtMatrixInfoMap == null) { _gtMatrixInfoId_InScopeSubQuery_TGtMatrixInfoMap = new LinkedHashMap<String, TGtMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_gtMatrixInfoId_InScopeSubQuery_TGtMatrixInfoMap.size() + 1);
            _gtMatrixInfoId_InScopeSubQuery_TGtMatrixInfoMap.put(key, subQuery); return "GtMatrixInfoId_InScopeSubQuery_TGtMatrixInfo." + key;
        }

        protected Map<String, TGtMatrixInfoCQ> _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfoMap;
        public Map<String, TGtMatrixInfoCQ> GtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfo { get { return _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfoMap; }}
        public override String keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfo(TGtMatrixInfoCQ subQuery) {
            if (_gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfoMap == null) { _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfoMap = new LinkedHashMap<String, TGtMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfoMap.size() + 1);
            _gtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfoMap.put(key, subQuery); return "GtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfo." + key;
        }

        public BsTGtMatrixChildCQ AddOrderBy_GtMatrixInfoId_Asc() { regOBA("GT_MATRIX_INFO_ID");return this; }
        public BsTGtMatrixChildCQ AddOrderBy_GtMatrixInfoId_Desc() { regOBD("GT_MATRIX_INFO_ID");return this; }

        protected ConditionValue _childItemId;
        public ConditionValue ChildItemId {
            get { if (_childItemId == null) { _childItemId = new ConditionValue(); } return _childItemId; }
        }
        protected override ConditionValue getCValueChildItemId() { return this.ChildItemId; }


        protected Map<String, TItemInfoCQ> _childItemId_InScopeSubQuery_TItemInfoMap;
        public Map<String, TItemInfoCQ> ChildItemId_InScopeSubQuery_TItemInfo { get { return _childItemId_InScopeSubQuery_TItemInfoMap; }}
        public override String keepChildItemId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            if (_childItemId_InScopeSubQuery_TItemInfoMap == null) { _childItemId_InScopeSubQuery_TItemInfoMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_childItemId_InScopeSubQuery_TItemInfoMap.size() + 1);
            _childItemId_InScopeSubQuery_TItemInfoMap.put(key, subQuery); return "ChildItemId_InScopeSubQuery_TItemInfo." + key;
        }

        protected Map<String, TItemInfoCQ> _childItemId_NotInScopeSubQuery_TItemInfoMap;
        public Map<String, TItemInfoCQ> ChildItemId_NotInScopeSubQuery_TItemInfo { get { return _childItemId_NotInScopeSubQuery_TItemInfoMap; }}
        public override String keepChildItemId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            if (_childItemId_NotInScopeSubQuery_TItemInfoMap == null) { _childItemId_NotInScopeSubQuery_TItemInfoMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_childItemId_NotInScopeSubQuery_TItemInfoMap.size() + 1);
            _childItemId_NotInScopeSubQuery_TItemInfoMap.put(key, subQuery); return "ChildItemId_NotInScopeSubQuery_TItemInfo." + key;
        }

        public BsTGtMatrixChildCQ AddOrderBy_ChildItemId_Asc() { regOBA("CHILD_ITEM_ID");return this; }
        public BsTGtMatrixChildCQ AddOrderBy_ChildItemId_Desc() { regOBD("CHILD_ITEM_ID");return this; }

        public BsTGtMatrixChildCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTGtMatrixChildCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TGtMatrixChildCQ baseQuery = (TGtMatrixChildCQ)baseQueryAsSuper;
            TGtMatrixChildCQ unionQuery = (TGtMatrixChildCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTGtMatrixInfo()) {
                unionQuery.QueryTGtMatrixInfo().reflectRelationOnUnionQuery(baseQuery.QueryTGtMatrixInfo(), unionQuery.QueryTGtMatrixInfo());
            }
            if (baseQuery.hasConditionQueryTItemInfo()) {
                unionQuery.QueryTItemInfo().reflectRelationOnUnionQuery(baseQuery.QueryTItemInfo(), unionQuery.QueryTItemInfo());
            }

        }
    
        protected TGtMatrixInfoCQ _conditionQueryTGtMatrixInfo;
        public TGtMatrixInfoCQ QueryTGtMatrixInfo() {
            return this.ConditionQueryTGtMatrixInfo;
        }
        public TGtMatrixInfoCQ ConditionQueryTGtMatrixInfo {
            get {
                if (_conditionQueryTGtMatrixInfo == null) {
                    _conditionQueryTGtMatrixInfo = xcreateQueryTGtMatrixInfo();
                    xsetupOuterJoin_TGtMatrixInfo();
                }
                return _conditionQueryTGtMatrixInfo;
            }
        }
        protected TGtMatrixInfoCQ xcreateQueryTGtMatrixInfo() {
            String nrp = resolveNextRelationPathTGtMatrixInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TGtMatrixInfoCQ cq = new TGtMatrixInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tGtMatrixInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TGtMatrixInfo() {
            TGtMatrixInfoCQ cq = ConditionQueryTGtMatrixInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTGtMatrixInfo() {
            return resolveNextRelationPath("T_GT_MATRIX_CHILD", "tGtMatrixInfo");
        }
        public bool hasConditionQueryTGtMatrixInfo() {
            return _conditionQueryTGtMatrixInfo != null;
        }
        protected TItemInfoCQ _conditionQueryTItemInfo;
        public TItemInfoCQ QueryTItemInfo() {
            return this.ConditionQueryTItemInfo;
        }
        public TItemInfoCQ ConditionQueryTItemInfo {
            get {
                if (_conditionQueryTItemInfo == null) {
                    _conditionQueryTItemInfo = xcreateQueryTItemInfo();
                    xsetupOuterJoin_TItemInfo();
                }
                return _conditionQueryTItemInfo;
            }
        }
        protected TItemInfoCQ xcreateQueryTItemInfo() {
            String nrp = resolveNextRelationPathTItemInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TItemInfoCQ cq = new TItemInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tItemInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TItemInfo() {
            TItemInfoCQ cq = ConditionQueryTItemInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("CHILD_ITEM_ID", "ITEM_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTItemInfo() {
            return resolveNextRelationPath("T_GT_MATRIX_CHILD", "tItemInfo");
        }
        public bool hasConditionQueryTItemInfo() {
            return _conditionQueryTItemInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TGtMatrixChildCQ> _scalarSubQueryMap;
	    public Map<String, TGtMatrixChildCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TGtMatrixChildCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TGtMatrixChildCQ> _myselfInScopeSubQueryMap;
        public Map<String, TGtMatrixChildCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TGtMatrixChildCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
