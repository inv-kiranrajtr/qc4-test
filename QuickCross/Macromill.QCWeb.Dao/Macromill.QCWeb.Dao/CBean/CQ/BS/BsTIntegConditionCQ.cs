
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTIntegConditionCQ : AbstractBsTIntegConditionCQ {

        protected TIntegConditionCIQ _inlineQuery;

        public BsTIntegConditionCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TIntegConditionCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TIntegConditionCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TIntegConditionCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TIntegConditionCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _integConditionId;
        public ConditionValue IntegConditionId {
            get { if (_integConditionId == null) { _integConditionId = new ConditionValue(); } return _integConditionId; }
        }
        protected override ConditionValue getCValueIntegConditionId() { return this.IntegConditionId; }


        public BsTIntegConditionCQ AddOrderBy_IntegConditionId_Asc() { regOBA("INTEG_CONDITION_ID");return this; }
        public BsTIntegConditionCQ AddOrderBy_IntegConditionId_Desc() { regOBD("INTEG_CONDITION_ID");return this; }

        protected ConditionValue _conditionNo;
        public ConditionValue ConditionNo {
            get { if (_conditionNo == null) { _conditionNo = new ConditionValue(); } return _conditionNo; }
        }
        protected override ConditionValue getCValueConditionNo() { return this.ConditionNo; }


        public BsTIntegConditionCQ AddOrderBy_ConditionNo_Asc() { regOBA("CONDITION_NO");return this; }
        public BsTIntegConditionCQ AddOrderBy_ConditionNo_Desc() { regOBD("CONDITION_NO");return this; }

        protected ConditionValue _srcItemId;
        public ConditionValue SrcItemId {
            get { if (_srcItemId == null) { _srcItemId = new ConditionValue(); } return _srcItemId; }
        }
        protected override ConditionValue getCValueSrcItemId() { return this.SrcItemId; }


        public BsTIntegConditionCQ AddOrderBy_SrcItemId_Asc() { regOBA("SRC_ITEM_ID");return this; }
        public BsTIntegConditionCQ AddOrderBy_SrcItemId_Desc() { regOBD("SRC_ITEM_ID");return this; }

        protected ConditionValue _sourceItemNo;
        public ConditionValue SourceItemNo {
            get { if (_sourceItemNo == null) { _sourceItemNo = new ConditionValue(); } return _sourceItemNo; }
        }
        protected override ConditionValue getCValueSourceItemNo() { return this.SourceItemNo; }


        public BsTIntegConditionCQ AddOrderBy_SourceItemNo_Asc() { regOBA("SOURCE_ITEM_NO");return this; }
        public BsTIntegConditionCQ AddOrderBy_SourceItemNo_Desc() { regOBD("SOURCE_ITEM_NO");return this; }

        protected ConditionValue _operationCode;
        public ConditionValue OperationCode {
            get { if (_operationCode == null) { _operationCode = new ConditionValue(); } return _operationCode; }
        }
        protected override ConditionValue getCValueOperationCode() { return this.OperationCode; }


        public BsTIntegConditionCQ AddOrderBy_OperationCode_Asc() { regOBA("OPERATION_CODE");return this; }
        public BsTIntegConditionCQ AddOrderBy_OperationCode_Desc() { regOBD("OPERATION_CODE");return this; }

        protected ConditionValue _conditionString;
        public ConditionValue ConditionString {
            get { if (_conditionString == null) { _conditionString = new ConditionValue(); } return _conditionString; }
        }
        protected override ConditionValue getCValueConditionString() { return this.ConditionString; }


        public BsTIntegConditionCQ AddOrderBy_ConditionString_Asc() { regOBA("CONDITION_STRING");return this; }
        public BsTIntegConditionCQ AddOrderBy_ConditionString_Desc() { regOBD("CONDITION_STRING");return this; }

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

        public BsTIntegConditionCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTIntegConditionCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        public BsTIntegConditionCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTIntegConditionCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TIntegConditionCQ baseQuery = (TIntegConditionCQ)baseQueryAsSuper;
            TIntegConditionCQ unionQuery = (TIntegConditionCQ)unionQueryAsSuper;
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
            return resolveNextRelationPath("T_INTEG_CONDITION", "tDataProcessNewItem");
        }
        public bool hasConditionQueryTDataProcessNewItem() {
            return _conditionQueryTDataProcessNewItem != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TIntegConditionCQ> _scalarSubQueryMap;
	    public Map<String, TIntegConditionCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TIntegConditionCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TIntegConditionCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TIntegConditionCQ> _myselfInScopeSubQueryMap;
        public Map<String, TIntegConditionCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TIntegConditionCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TIntegConditionCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
