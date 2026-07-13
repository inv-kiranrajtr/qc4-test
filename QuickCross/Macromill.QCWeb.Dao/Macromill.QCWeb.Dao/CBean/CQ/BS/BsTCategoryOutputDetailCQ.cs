
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTCategoryOutputDetailCQ : AbstractBsTCategoryOutputDetailCQ {

        protected TCategoryOutputDetailCIQ _inlineQuery;

        public BsTCategoryOutputDetailCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TCategoryOutputDetailCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TCategoryOutputDetailCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TCategoryOutputDetailCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TCategoryOutputDetailCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _categoryOutputEditDetailId;
        public ConditionValue CategoryOutputEditDetailId {
            get { if (_categoryOutputEditDetailId == null) { _categoryOutputEditDetailId = new ConditionValue(); } return _categoryOutputEditDetailId; }
        }
        protected override ConditionValue getCValueCategoryOutputEditDetailId() { return this.CategoryOutputEditDetailId; }


        public BsTCategoryOutputDetailCQ AddOrderBy_CategoryOutputEditDetailId_Asc() { regOBA("CATEGORY_OUTPUT_EDIT_DETAIL_ID");return this; }
        public BsTCategoryOutputDetailCQ AddOrderBy_CategoryOutputEditDetailId_Desc() { regOBD("CATEGORY_OUTPUT_EDIT_DETAIL_ID");return this; }

        protected ConditionValue _categoryOutputEditId;
        public ConditionValue CategoryOutputEditId {
            get { if (_categoryOutputEditId == null) { _categoryOutputEditId = new ConditionValue(); } return _categoryOutputEditId; }
        }
        protected override ConditionValue getCValueCategoryOutputEditId() { return this.CategoryOutputEditId; }


        protected Map<String, TCategoryOutputEditCQ> _categoryOutputEditId_InScopeSubQuery_TCategoryOutputEditMap;
        public Map<String, TCategoryOutputEditCQ> CategoryOutputEditId_InScopeSubQuery_TCategoryOutputEdit { get { return _categoryOutputEditId_InScopeSubQuery_TCategoryOutputEditMap; }}
        public override String keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputEdit(TCategoryOutputEditCQ subQuery) {
            if (_categoryOutputEditId_InScopeSubQuery_TCategoryOutputEditMap == null) { _categoryOutputEditId_InScopeSubQuery_TCategoryOutputEditMap = new LinkedHashMap<String, TCategoryOutputEditCQ>(); }
            String key = "subQueryMapKey" + (_categoryOutputEditId_InScopeSubQuery_TCategoryOutputEditMap.size() + 1);
            _categoryOutputEditId_InScopeSubQuery_TCategoryOutputEditMap.put(key, subQuery); return "CategoryOutputEditId_InScopeSubQuery_TCategoryOutputEdit." + key;
        }

        protected Map<String, TCategoryOutputEditCQ> _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEditMap;
        public Map<String, TCategoryOutputEditCQ> CategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEdit { get { return _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEditMap; }}
        public override String keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEdit(TCategoryOutputEditCQ subQuery) {
            if (_categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEditMap == null) { _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEditMap = new LinkedHashMap<String, TCategoryOutputEditCQ>(); }
            String key = "subQueryMapKey" + (_categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEditMap.size() + 1);
            _categoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEditMap.put(key, subQuery); return "CategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEdit." + key;
        }

        public BsTCategoryOutputDetailCQ AddOrderBy_CategoryOutputEditId_Asc() { regOBA("CATEGORY_OUTPUT_EDIT_ID");return this; }
        public BsTCategoryOutputDetailCQ AddOrderBy_CategoryOutputEditId_Desc() { regOBD("CATEGORY_OUTPUT_EDIT_ID");return this; }

        protected ConditionValue _oldCategoryNo;
        public ConditionValue OldCategoryNo {
            get { if (_oldCategoryNo == null) { _oldCategoryNo = new ConditionValue(); } return _oldCategoryNo; }
        }
        protected override ConditionValue getCValueOldCategoryNo() { return this.OldCategoryNo; }


        public BsTCategoryOutputDetailCQ AddOrderBy_OldCategoryNo_Asc() { regOBA("OLD_CATEGORY_NO");return this; }
        public BsTCategoryOutputDetailCQ AddOrderBy_OldCategoryNo_Desc() { regOBD("OLD_CATEGORY_NO");return this; }

        protected ConditionValue _newCategoryNo;
        public ConditionValue NewCategoryNo {
            get { if (_newCategoryNo == null) { _newCategoryNo = new ConditionValue(); } return _newCategoryNo; }
        }
        protected override ConditionValue getCValueNewCategoryNo() { return this.NewCategoryNo; }


        public BsTCategoryOutputDetailCQ AddOrderBy_NewCategoryNo_Asc() { regOBA("NEW_CATEGORY_NO");return this; }
        public BsTCategoryOutputDetailCQ AddOrderBy_NewCategoryNo_Desc() { regOBD("NEW_CATEGORY_NO");return this; }

        public BsTCategoryOutputDetailCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTCategoryOutputDetailCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TCategoryOutputDetailCQ baseQuery = (TCategoryOutputDetailCQ)baseQueryAsSuper;
            TCategoryOutputDetailCQ unionQuery = (TCategoryOutputDetailCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTCategoryOutputEdit()) {
                unionQuery.QueryTCategoryOutputEdit().reflectRelationOnUnionQuery(baseQuery.QueryTCategoryOutputEdit(), unionQuery.QueryTCategoryOutputEdit());
            }

        }
    
        protected TCategoryOutputEditCQ _conditionQueryTCategoryOutputEdit;
        public TCategoryOutputEditCQ QueryTCategoryOutputEdit() {
            return this.ConditionQueryTCategoryOutputEdit;
        }
        public TCategoryOutputEditCQ ConditionQueryTCategoryOutputEdit {
            get {
                if (_conditionQueryTCategoryOutputEdit == null) {
                    _conditionQueryTCategoryOutputEdit = xcreateQueryTCategoryOutputEdit();
                    xsetupOuterJoin_TCategoryOutputEdit();
                }
                return _conditionQueryTCategoryOutputEdit;
            }
        }
        protected TCategoryOutputEditCQ xcreateQueryTCategoryOutputEdit() {
            String nrp = resolveNextRelationPathTCategoryOutputEdit();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TCategoryOutputEditCQ cq = new TCategoryOutputEditCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tCategoryOutputEdit"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TCategoryOutputEdit() {
            TCategoryOutputEditCQ cq = ConditionQueryTCategoryOutputEdit;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTCategoryOutputEdit() {
            return resolveNextRelationPath("T_CATEGORY_OUTPUT_DETAIL", "tCategoryOutputEdit");
        }
        public bool hasConditionQueryTCategoryOutputEdit() {
            return _conditionQueryTCategoryOutputEdit != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TCategoryOutputDetailCQ> _scalarSubQueryMap;
	    public Map<String, TCategoryOutputDetailCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TCategoryOutputDetailCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TCategoryOutputDetailCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TCategoryOutputDetailCQ> _myselfInScopeSubQueryMap;
        public Map<String, TCategoryOutputDetailCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TCategoryOutputDetailCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TCategoryOutputDetailCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
