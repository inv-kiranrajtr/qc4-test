
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTMatrixInfoCQ : AbstractBsTMatrixInfoCQ {

        protected TMatrixInfoCIQ _inlineQuery;

        public BsTMatrixInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TMatrixInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TMatrixInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TMatrixInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TMatrixInfoCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _matrixInfoId;
        public ConditionValue MatrixInfoId {
            get { if (_matrixInfoId == null) { _matrixInfoId = new ConditionValue(); } return _matrixInfoId; }
        }
        protected override ConditionValue getCValueMatrixInfoId() { return this.MatrixInfoId; }


        public BsTMatrixInfoCQ AddOrderBy_MatrixInfoId_Asc() { regOBA("MATRIX_INFO_ID");return this; }
        public BsTMatrixInfoCQ AddOrderBy_MatrixInfoId_Desc() { regOBD("MATRIX_INFO_ID");return this; }

        protected ConditionValue _itemInfoId;
        public ConditionValue ItemInfoId {
            get { if (_itemInfoId == null) { _itemInfoId = new ConditionValue(); } return _itemInfoId; }
        }
        protected override ConditionValue getCValueItemInfoId() { return this.ItemInfoId; }


        protected Map<String, TItemInfoCQ> _itemInfoId_InScopeSubQuery_TItemInfoByItemInfoIdMap;
        public Map<String, TItemInfoCQ> ItemInfoId_InScopeSubQuery_TItemInfoByItemInfoId { get { return _itemInfoId_InScopeSubQuery_TItemInfoByItemInfoIdMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TItemInfoByItemInfoId(TItemInfoCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TItemInfoByItemInfoIdMap == null) { _itemInfoId_InScopeSubQuery_TItemInfoByItemInfoIdMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TItemInfoByItemInfoIdMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TItemInfoByItemInfoIdMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TItemInfoByItemInfoId." + key;
        }

        protected Map<String, TItemInfoCQ> _itemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoIdMap;
        public Map<String, TItemInfoCQ> ItemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoId { get { return _itemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoIdMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoId(TItemInfoCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoIdMap == null) { _itemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoIdMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoIdMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoIdMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoId." + key;
        }

        public BsTMatrixInfoCQ AddOrderBy_ItemInfoId_Asc() { regOBA("ITEM_INFO_ID");return this; }
        public BsTMatrixInfoCQ AddOrderBy_ItemInfoId_Desc() { regOBD("ITEM_INFO_ID");return this; }

        protected ConditionValue _childItemInfoId;
        public ConditionValue ChildItemInfoId {
            get { if (_childItemInfoId == null) { _childItemInfoId = new ConditionValue(); } return _childItemInfoId; }
        }
        protected override ConditionValue getCValueChildItemInfoId() { return this.ChildItemInfoId; }


        protected Map<String, TItemInfoCQ> _childItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoIdMap;
        public Map<String, TItemInfoCQ> ChildItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoId { get { return _childItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoIdMap; }}
        public override String keepChildItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoId(TItemInfoCQ subQuery) {
            if (_childItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoIdMap == null) { _childItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoIdMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_childItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoIdMap.size() + 1);
            _childItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoIdMap.put(key, subQuery); return "ChildItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoId." + key;
        }

        protected Map<String, TItemInfoCQ> _childItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoIdMap;
        public Map<String, TItemInfoCQ> ChildItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoId { get { return _childItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoIdMap; }}
        public override String keepChildItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoId(TItemInfoCQ subQuery) {
            if (_childItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoIdMap == null) { _childItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoIdMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_childItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoIdMap.size() + 1);
            _childItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoIdMap.put(key, subQuery); return "ChildItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoId." + key;
        }

        public BsTMatrixInfoCQ AddOrderBy_ChildItemInfoId_Asc() { regOBA("CHILD_ITEM_INFO_ID");return this; }
        public BsTMatrixInfoCQ AddOrderBy_ChildItemInfoId_Desc() { regOBD("CHILD_ITEM_INFO_ID");return this; }

        protected ConditionValue _addFaItemInfoId;
        public ConditionValue AddFaItemInfoId {
            get { if (_addFaItemInfoId == null) { _addFaItemInfoId = new ConditionValue(); } return _addFaItemInfoId; }
        }
        protected override ConditionValue getCValueAddFaItemInfoId() { return this.AddFaItemInfoId; }


        public BsTMatrixInfoCQ AddOrderBy_AddFaItemInfoId_Asc() { regOBA("ADD_FA_ITEM_INFO_ID");return this; }
        public BsTMatrixInfoCQ AddOrderBy_AddFaItemInfoId_Desc() { regOBD("ADD_FA_ITEM_INFO_ID");return this; }

        protected ConditionValue _addFaCategoryInfoId;
        public ConditionValue AddFaCategoryInfoId {
            get { if (_addFaCategoryInfoId == null) { _addFaCategoryInfoId = new ConditionValue(); } return _addFaCategoryInfoId; }
        }
        protected override ConditionValue getCValueAddFaCategoryInfoId() { return this.AddFaCategoryInfoId; }


        protected Map<String, TCategoryInfoCQ> _addFaCategoryInfoId_InScopeSubQuery_TCategoryInfoMap;
        public Map<String, TCategoryInfoCQ> AddFaCategoryInfoId_InScopeSubQuery_TCategoryInfo { get { return _addFaCategoryInfoId_InScopeSubQuery_TCategoryInfoMap; }}
        public override String keepAddFaCategoryInfoId_InScopeSubQuery_TCategoryInfo(TCategoryInfoCQ subQuery) {
            if (_addFaCategoryInfoId_InScopeSubQuery_TCategoryInfoMap == null) { _addFaCategoryInfoId_InScopeSubQuery_TCategoryInfoMap = new LinkedHashMap<String, TCategoryInfoCQ>(); }
            String key = "subQueryMapKey" + (_addFaCategoryInfoId_InScopeSubQuery_TCategoryInfoMap.size() + 1);
            _addFaCategoryInfoId_InScopeSubQuery_TCategoryInfoMap.put(key, subQuery); return "AddFaCategoryInfoId_InScopeSubQuery_TCategoryInfo." + key;
        }

        protected Map<String, TCategoryInfoCQ> _addFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfoMap;
        public Map<String, TCategoryInfoCQ> AddFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfo { get { return _addFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfoMap; }}
        public override String keepAddFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfo(TCategoryInfoCQ subQuery) {
            if (_addFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfoMap == null) { _addFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfoMap = new LinkedHashMap<String, TCategoryInfoCQ>(); }
            String key = "subQueryMapKey" + (_addFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfoMap.size() + 1);
            _addFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfoMap.put(key, subQuery); return "AddFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfo." + key;
        }

        public BsTMatrixInfoCQ AddOrderBy_AddFaCategoryInfoId_Asc() { regOBA("ADD_FA_CATEGORY_INFO_ID");return this; }
        public BsTMatrixInfoCQ AddOrderBy_AddFaCategoryInfoId_Desc() { regOBD("ADD_FA_CATEGORY_INFO_ID");return this; }

        public BsTMatrixInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTMatrixInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TMatrixInfoCQ baseQuery = (TMatrixInfoCQ)baseQueryAsSuper;
            TMatrixInfoCQ unionQuery = (TMatrixInfoCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTItemInfoByItemInfoId()) {
                unionQuery.QueryTItemInfoByItemInfoId().reflectRelationOnUnionQuery(baseQuery.QueryTItemInfoByItemInfoId(), unionQuery.QueryTItemInfoByItemInfoId());
            }
            if (baseQuery.hasConditionQueryTItemInfoByChildItemInfoId()) {
                unionQuery.QueryTItemInfoByChildItemInfoId().reflectRelationOnUnionQuery(baseQuery.QueryTItemInfoByChildItemInfoId(), unionQuery.QueryTItemInfoByChildItemInfoId());
            }
            if (baseQuery.hasConditionQueryTCategoryInfo()) {
                unionQuery.QueryTCategoryInfo().reflectRelationOnUnionQuery(baseQuery.QueryTCategoryInfo(), unionQuery.QueryTCategoryInfo());
            }

        }
    
        protected TItemInfoCQ _conditionQueryTItemInfoByItemInfoId;
        public TItemInfoCQ QueryTItemInfoByItemInfoId() {
            return this.ConditionQueryTItemInfoByItemInfoId;
        }
        public TItemInfoCQ ConditionQueryTItemInfoByItemInfoId {
            get {
                if (_conditionQueryTItemInfoByItemInfoId == null) {
                    _conditionQueryTItemInfoByItemInfoId = xcreateQueryTItemInfoByItemInfoId();
                    xsetupOuterJoin_TItemInfoByItemInfoId();
                }
                return _conditionQueryTItemInfoByItemInfoId;
            }
        }
        protected TItemInfoCQ xcreateQueryTItemInfoByItemInfoId() {
            String nrp = resolveNextRelationPathTItemInfoByItemInfoId();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TItemInfoCQ cq = new TItemInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tItemInfoByItemInfoId"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TItemInfoByItemInfoId() {
            TItemInfoCQ cq = ConditionQueryTItemInfoByItemInfoId;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("ITEM_INFO_ID", "ITEM_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTItemInfoByItemInfoId() {
            return resolveNextRelationPath("T_MATRIX_INFO", "tItemInfoByItemInfoId");
        }
        public bool hasConditionQueryTItemInfoByItemInfoId() {
            return _conditionQueryTItemInfoByItemInfoId != null;
        }
        protected TItemInfoCQ _conditionQueryTItemInfoByChildItemInfoId;
        public TItemInfoCQ QueryTItemInfoByChildItemInfoId() {
            return this.ConditionQueryTItemInfoByChildItemInfoId;
        }
        public TItemInfoCQ ConditionQueryTItemInfoByChildItemInfoId {
            get {
                if (_conditionQueryTItemInfoByChildItemInfoId == null) {
                    _conditionQueryTItemInfoByChildItemInfoId = xcreateQueryTItemInfoByChildItemInfoId();
                    xsetupOuterJoin_TItemInfoByChildItemInfoId();
                }
                return _conditionQueryTItemInfoByChildItemInfoId;
            }
        }
        protected TItemInfoCQ xcreateQueryTItemInfoByChildItemInfoId() {
            String nrp = resolveNextRelationPathTItemInfoByChildItemInfoId();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TItemInfoCQ cq = new TItemInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tItemInfoByChildItemInfoId"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TItemInfoByChildItemInfoId() {
            TItemInfoCQ cq = ConditionQueryTItemInfoByChildItemInfoId;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("CHILD_ITEM_INFO_ID", "ITEM_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTItemInfoByChildItemInfoId() {
            return resolveNextRelationPath("T_MATRIX_INFO", "tItemInfoByChildItemInfoId");
        }
        public bool hasConditionQueryTItemInfoByChildItemInfoId() {
            return _conditionQueryTItemInfoByChildItemInfoId != null;
        }
        protected TCategoryInfoCQ _conditionQueryTCategoryInfo;
        public TCategoryInfoCQ QueryTCategoryInfo() {
            return this.ConditionQueryTCategoryInfo;
        }
        public TCategoryInfoCQ ConditionQueryTCategoryInfo {
            get {
                if (_conditionQueryTCategoryInfo == null) {
                    _conditionQueryTCategoryInfo = xcreateQueryTCategoryInfo();
                    xsetupOuterJoin_TCategoryInfo();
                }
                return _conditionQueryTCategoryInfo;
            }
        }
        protected TCategoryInfoCQ xcreateQueryTCategoryInfo() {
            String nrp = resolveNextRelationPathTCategoryInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TCategoryInfoCQ cq = new TCategoryInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tCategoryInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TCategoryInfo() {
            TCategoryInfoCQ cq = ConditionQueryTCategoryInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("ADD_FA_CATEGORY_INFO_ID", "CATEGORY_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTCategoryInfo() {
            return resolveNextRelationPath("T_MATRIX_INFO", "tCategoryInfo");
        }
        public bool hasConditionQueryTCategoryInfo() {
            return _conditionQueryTCategoryInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TMatrixInfoCQ> _scalarSubQueryMap;
	    public Map<String, TMatrixInfoCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TMatrixInfoCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TMatrixInfoCQ> _myselfInScopeSubQueryMap;
        public Map<String, TMatrixInfoCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TMatrixInfoCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
