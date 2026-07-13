
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTEditConditionCQ : AbstractBsTEditConditionCQ {

        protected TEditConditionCIQ _inlineQuery;

        public BsTEditConditionCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TEditConditionCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TEditConditionCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TEditConditionCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TEditConditionCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _editConditionId;
        public ConditionValue EditConditionId {
            get { if (_editConditionId == null) { _editConditionId = new ConditionValue(); } return _editConditionId; }
        }
        protected override ConditionValue getCValueEditConditionId() { return this.EditConditionId; }


        public BsTEditConditionCQ AddOrderBy_EditConditionId_Asc() { regOBA("EDIT_CONDITION_ID");return this; }
        public BsTEditConditionCQ AddOrderBy_EditConditionId_Desc() { regOBD("EDIT_CONDITION_ID");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTEditConditionCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTEditConditionCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _itemId;
        public ConditionValue ItemId {
            get { if (_itemId == null) { _itemId = new ConditionValue(); } return _itemId; }
        }
        protected override ConditionValue getCValueItemId() { return this.ItemId; }


        public BsTEditConditionCQ AddOrderBy_ItemId_Asc() { regOBA("ITEM_ID");return this; }
        public BsTEditConditionCQ AddOrderBy_ItemId_Desc() { regOBD("ITEM_ID");return this; }

        protected ConditionValue _operationCode;
        public ConditionValue OperationCode {
            get { if (_operationCode == null) { _operationCode = new ConditionValue(); } return _operationCode; }
        }
        protected override ConditionValue getCValueOperationCode() { return this.OperationCode; }


        public BsTEditConditionCQ AddOrderBy_OperationCode_Asc() { regOBA("OPERATION_CODE");return this; }
        public BsTEditConditionCQ AddOrderBy_OperationCode_Desc() { regOBD("OPERATION_CODE");return this; }

        protected ConditionValue _operationValue;
        public ConditionValue OperationValue {
            get { if (_operationValue == null) { _operationValue = new ConditionValue(); } return _operationValue; }
        }
        protected override ConditionValue getCValueOperationValue() { return this.OperationValue; }


        public BsTEditConditionCQ AddOrderBy_OperationValue_Asc() { regOBA("OPERATION_VALUE");return this; }
        public BsTEditConditionCQ AddOrderBy_OperationValue_Desc() { regOBD("OPERATION_VALUE");return this; }

        protected ConditionValue _dataEditId;
        public ConditionValue DataEditId {
            get { if (_dataEditId == null) { _dataEditId = new ConditionValue(); } return _dataEditId; }
        }
        protected override ConditionValue getCValueDataEditId() { return this.DataEditId; }


        protected Map<String, TEditDataCQ> _dataEditId_InScopeSubQuery_TEditDataMap;
        public Map<String, TEditDataCQ> DataEditId_InScopeSubQuery_TEditData { get { return _dataEditId_InScopeSubQuery_TEditDataMap; }}
        public override String keepDataEditId_InScopeSubQuery_TEditData(TEditDataCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TEditDataMap == null) { _dataEditId_InScopeSubQuery_TEditDataMap = new LinkedHashMap<String, TEditDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TEditDataMap.size() + 1);
            _dataEditId_InScopeSubQuery_TEditDataMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TEditData." + key;
        }

        protected Map<String, TEditDataCQ> _dataEditId_NotInScopeSubQuery_TEditDataMap;
        public Map<String, TEditDataCQ> DataEditId_NotInScopeSubQuery_TEditData { get { return _dataEditId_NotInScopeSubQuery_TEditDataMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TEditData(TEditDataCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TEditDataMap == null) { _dataEditId_NotInScopeSubQuery_TEditDataMap = new LinkedHashMap<String, TEditDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TEditDataMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TEditDataMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TEditData." + key;
        }

        public BsTEditConditionCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTEditConditionCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        public BsTEditConditionCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTEditConditionCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TEditConditionCQ baseQuery = (TEditConditionCQ)baseQueryAsSuper;
            TEditConditionCQ unionQuery = (TEditConditionCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTEditData()) {
                unionQuery.QueryTEditData().reflectRelationOnUnionQuery(baseQuery.QueryTEditData(), unionQuery.QueryTEditData());
            }

        }
    
        protected TEditDataCQ _conditionQueryTEditData;
        public TEditDataCQ QueryTEditData() {
            return this.ConditionQueryTEditData;
        }
        public TEditDataCQ ConditionQueryTEditData {
            get {
                if (_conditionQueryTEditData == null) {
                    _conditionQueryTEditData = xcreateQueryTEditData();
                    xsetupOuterJoin_TEditData();
                }
                return _conditionQueryTEditData;
            }
        }
        protected TEditDataCQ xcreateQueryTEditData() {
            String nrp = resolveNextRelationPathTEditData();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TEditDataCQ cq = new TEditDataCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tEditData"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TEditData() {
            TEditDataCQ cq = ConditionQueryTEditData;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DATA_EDIT_ID", "DATA_EDIT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTEditData() {
            return resolveNextRelationPath("T_EDIT_CONDITION", "tEditData");
        }
        public bool hasConditionQueryTEditData() {
            return _conditionQueryTEditData != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TEditConditionCQ> _scalarSubQueryMap;
	    public Map<String, TEditConditionCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TEditConditionCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TEditConditionCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TEditConditionCQ> _myselfInScopeSubQueryMap;
        public Map<String, TEditConditionCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TEditConditionCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TEditConditionCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
