
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDeleteConditionCQ : AbstractBsTDeleteConditionCQ {

        protected TDeleteConditionCIQ _inlineQuery;

        public BsTDeleteConditionCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDeleteConditionCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDeleteConditionCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDeleteConditionCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDeleteConditionCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _deleteConditionId;
        public ConditionValue DeleteConditionId {
            get { if (_deleteConditionId == null) { _deleteConditionId = new ConditionValue(); } return _deleteConditionId; }
        }
        protected override ConditionValue getCValueDeleteConditionId() { return this.DeleteConditionId; }


        public BsTDeleteConditionCQ AddOrderBy_DeleteConditionId_Asc() { regOBA("DELETE_CONDITION_ID");return this; }
        public BsTDeleteConditionCQ AddOrderBy_DeleteConditionId_Desc() { regOBD("DELETE_CONDITION_ID");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTDeleteConditionCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTDeleteConditionCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _itemId;
        public ConditionValue ItemId {
            get { if (_itemId == null) { _itemId = new ConditionValue(); } return _itemId; }
        }
        protected override ConditionValue getCValueItemId() { return this.ItemId; }


        public BsTDeleteConditionCQ AddOrderBy_ItemId_Asc() { regOBA("ITEM_ID");return this; }
        public BsTDeleteConditionCQ AddOrderBy_ItemId_Desc() { regOBD("ITEM_ID");return this; }

        protected ConditionValue _operationCode;
        public ConditionValue OperationCode {
            get { if (_operationCode == null) { _operationCode = new ConditionValue(); } return _operationCode; }
        }
        protected override ConditionValue getCValueOperationCode() { return this.OperationCode; }


        public BsTDeleteConditionCQ AddOrderBy_OperationCode_Asc() { regOBA("OPERATION_CODE");return this; }
        public BsTDeleteConditionCQ AddOrderBy_OperationCode_Desc() { regOBD("OPERATION_CODE");return this; }

        protected ConditionValue _operationValue;
        public ConditionValue OperationValue {
            get { if (_operationValue == null) { _operationValue = new ConditionValue(); } return _operationValue; }
        }
        protected override ConditionValue getCValueOperationValue() { return this.OperationValue; }


        public BsTDeleteConditionCQ AddOrderBy_OperationValue_Asc() { regOBA("OPERATION_VALUE");return this; }
        public BsTDeleteConditionCQ AddOrderBy_OperationValue_Desc() { regOBD("OPERATION_VALUE");return this; }

        protected ConditionValue _dataEditId;
        public ConditionValue DataEditId {
            get { if (_dataEditId == null) { _dataEditId = new ConditionValue(); } return _dataEditId; }
        }
        protected override ConditionValue getCValueDataEditId() { return this.DataEditId; }


        protected Map<String, TDeleteDataCQ> _dataEditId_InScopeSubQuery_TDeleteDataMap;
        public Map<String, TDeleteDataCQ> DataEditId_InScopeSubQuery_TDeleteData { get { return _dataEditId_InScopeSubQuery_TDeleteDataMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDeleteData(TDeleteDataCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDeleteDataMap == null) { _dataEditId_InScopeSubQuery_TDeleteDataMap = new LinkedHashMap<String, TDeleteDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDeleteDataMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDeleteDataMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDeleteData." + key;
        }

        protected Map<String, TDeleteDataCQ> _dataEditId_NotInScopeSubQuery_TDeleteDataMap;
        public Map<String, TDeleteDataCQ> DataEditId_NotInScopeSubQuery_TDeleteData { get { return _dataEditId_NotInScopeSubQuery_TDeleteDataMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDeleteData(TDeleteDataCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDeleteDataMap == null) { _dataEditId_NotInScopeSubQuery_TDeleteDataMap = new LinkedHashMap<String, TDeleteDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDeleteDataMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDeleteDataMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDeleteData." + key;
        }

        public BsTDeleteConditionCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTDeleteConditionCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        public BsTDeleteConditionCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDeleteConditionCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDeleteConditionCQ baseQuery = (TDeleteConditionCQ)baseQueryAsSuper;
            TDeleteConditionCQ unionQuery = (TDeleteConditionCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTDeleteData()) {
                unionQuery.QueryTDeleteData().reflectRelationOnUnionQuery(baseQuery.QueryTDeleteData(), unionQuery.QueryTDeleteData());
            }

        }
    
        protected TDeleteDataCQ _conditionQueryTDeleteData;
        public TDeleteDataCQ QueryTDeleteData() {
            return this.ConditionQueryTDeleteData;
        }
        public TDeleteDataCQ ConditionQueryTDeleteData {
            get {
                if (_conditionQueryTDeleteData == null) {
                    _conditionQueryTDeleteData = xcreateQueryTDeleteData();
                    xsetupOuterJoin_TDeleteData();
                }
                return _conditionQueryTDeleteData;
            }
        }
        protected TDeleteDataCQ xcreateQueryTDeleteData() {
            String nrp = resolveNextRelationPathTDeleteData();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDeleteDataCQ cq = new TDeleteDataCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDeleteData"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDeleteData() {
            TDeleteDataCQ cq = ConditionQueryTDeleteData;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DATA_EDIT_ID", "DATA_EDIT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDeleteData() {
            return resolveNextRelationPath("T_DELETE_CONDITION", "tDeleteData");
        }
        public bool hasConditionQueryTDeleteData() {
            return _conditionQueryTDeleteData != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDeleteConditionCQ> _scalarSubQueryMap;
	    public Map<String, TDeleteConditionCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDeleteConditionCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDeleteConditionCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDeleteConditionCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDeleteConditionCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDeleteConditionCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDeleteConditionCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
