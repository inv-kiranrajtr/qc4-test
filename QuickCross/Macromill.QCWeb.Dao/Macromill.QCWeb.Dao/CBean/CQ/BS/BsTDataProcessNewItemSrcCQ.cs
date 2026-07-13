
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDataProcessNewItemSrcCQ : AbstractBsTDataProcessNewItemSrcCQ {

        protected TDataProcessNewItemSrcCIQ _inlineQuery;

        public BsTDataProcessNewItemSrcCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDataProcessNewItemSrcCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDataProcessNewItemSrcCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDataProcessNewItemSrcCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDataProcessNewItemSrcCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _dataProcessNewItemSrcId;
        public ConditionValue DataProcessNewItemSrcId {
            get { if (_dataProcessNewItemSrcId == null) { _dataProcessNewItemSrcId = new ConditionValue(); } return _dataProcessNewItemSrcId; }
        }
        protected override ConditionValue getCValueDataProcessNewItemSrcId() { return this.DataProcessNewItemSrcId; }


        public BsTDataProcessNewItemSrcCQ AddOrderBy_DataProcessNewItemSrcId_Asc() { regOBA("DATA_PROCESS_NEW_ITEM_SRC_ID");return this; }
        public BsTDataProcessNewItemSrcCQ AddOrderBy_DataProcessNewItemSrcId_Desc() { regOBD("DATA_PROCESS_NEW_ITEM_SRC_ID");return this; }

        protected ConditionValue _srcItemId;
        public ConditionValue SrcItemId {
            get { if (_srcItemId == null) { _srcItemId = new ConditionValue(); } return _srcItemId; }
        }
        protected override ConditionValue getCValueSrcItemId() { return this.SrcItemId; }


        public BsTDataProcessNewItemSrcCQ AddOrderBy_SrcItemId_Asc() { regOBA("SRC_ITEM_ID");return this; }
        public BsTDataProcessNewItemSrcCQ AddOrderBy_SrcItemId_Desc() { regOBD("SRC_ITEM_ID");return this; }

        protected ConditionValue _newItemId;
        public ConditionValue NewItemId {
            get { if (_newItemId == null) { _newItemId = new ConditionValue(); } return _newItemId; }
        }
        protected override ConditionValue getCValueNewItemId() { return this.NewItemId; }


        public BsTDataProcessNewItemSrcCQ AddOrderBy_NewItemId_Asc() { regOBA("NEW_ITEM_ID");return this; }
        public BsTDataProcessNewItemSrcCQ AddOrderBy_NewItemId_Desc() { regOBD("NEW_ITEM_ID");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTDataProcessNewItemSrcCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTDataProcessNewItemSrcCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _targetFlag;
        public ConditionValue TargetFlag {
            get { if (_targetFlag == null) { _targetFlag = new ConditionValue(); } return _targetFlag; }
        }
        protected override ConditionValue getCValueTargetFlag() { return this.TargetFlag; }


        public BsTDataProcessNewItemSrcCQ AddOrderBy_TargetFlag_Asc() { regOBA("TARGET_FLAG");return this; }
        public BsTDataProcessNewItemSrcCQ AddOrderBy_TargetFlag_Desc() { regOBD("TARGET_FLAG");return this; }

        protected ConditionValue _dataEditId;
        public ConditionValue DataEditId {
            get { if (_dataEditId == null) { _dataEditId = new ConditionValue(); } return _dataEditId; }
        }
        protected override ConditionValue getCValueDataEditId() { return this.DataEditId; }


        protected Map<String, TDataProcessNewItemCQ> _dataEditId_InScopeSubQuery_TDataProcessNewItemMap;
        public Map<String, TDataProcessNewItemCQ> DataEditId_InScopeSubQuery_TDataProcessNewItem { get { return _dataEditId_InScopeSubQuery_TDataProcessNewItemMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDataProcessNewItem(TDataProcessNewItemCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDataProcessNewItemMap == null) { _dataEditId_InScopeSubQuery_TDataProcessNewItemMap = new LinkedHashMap<String, TDataProcessNewItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDataProcessNewItemMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDataProcessNewItemMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDataProcessNewItem." + key;
        }

        protected Map<String, TDataProcessNewItemCQ> _dataEditId_NotInScopeSubQuery_TDataProcessNewItemMap;
        public Map<String, TDataProcessNewItemCQ> DataEditId_NotInScopeSubQuery_TDataProcessNewItem { get { return _dataEditId_NotInScopeSubQuery_TDataProcessNewItemMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDataProcessNewItem(TDataProcessNewItemCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDataProcessNewItemMap == null) { _dataEditId_NotInScopeSubQuery_TDataProcessNewItemMap = new LinkedHashMap<String, TDataProcessNewItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDataProcessNewItemMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDataProcessNewItemMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDataProcessNewItem." + key;
        }

        public BsTDataProcessNewItemSrcCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTDataProcessNewItemSrcCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        public BsTDataProcessNewItemSrcCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDataProcessNewItemSrcCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDataProcessNewItemSrcCQ baseQuery = (TDataProcessNewItemSrcCQ)baseQueryAsSuper;
            TDataProcessNewItemSrcCQ unionQuery = (TDataProcessNewItemSrcCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTDataProcessNewItem()) {
                unionQuery.QueryTDataProcessNewItem().reflectRelationOnUnionQuery(baseQuery.QueryTDataProcessNewItem(), unionQuery.QueryTDataProcessNewItem());
            }

        }
    
        protected TDataProcessNewItemCQ _conditionQueryTDataProcessNewItem;
        public TDataProcessNewItemCQ QueryTDataProcessNewItem() {
            return this.ConditionQueryTDataProcessNewItem;
        }
        public TDataProcessNewItemCQ ConditionQueryTDataProcessNewItem {
            get {
                if (_conditionQueryTDataProcessNewItem == null) {
                    _conditionQueryTDataProcessNewItem = xcreateQueryTDataProcessNewItem();
                    xsetupOuterJoin_TDataProcessNewItem();
                }
                return _conditionQueryTDataProcessNewItem;
            }
        }
        protected TDataProcessNewItemCQ xcreateQueryTDataProcessNewItem() {
            String nrp = resolveNextRelationPathTDataProcessNewItem();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDataProcessNewItemCQ cq = new TDataProcessNewItemCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDataProcessNewItem"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDataProcessNewItem() {
            TDataProcessNewItemCQ cq = ConditionQueryTDataProcessNewItem;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DATA_EDIT_ID", "DATA_EDIT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDataProcessNewItem() {
            return resolveNextRelationPath("T_DATA_PROCESS_NEW_ITEM_SRC", "tDataProcessNewItem");
        }
        public bool hasConditionQueryTDataProcessNewItem() {
            return _conditionQueryTDataProcessNewItem != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDataProcessNewItemSrcCQ> _scalarSubQueryMap;
	    public Map<String, TDataProcessNewItemSrcCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDataProcessNewItemSrcCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDataProcessNewItemSrcCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDataProcessNewItemSrcCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDataProcessNewItemSrcCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDataProcessNewItemSrcCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDataProcessNewItemSrcCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
