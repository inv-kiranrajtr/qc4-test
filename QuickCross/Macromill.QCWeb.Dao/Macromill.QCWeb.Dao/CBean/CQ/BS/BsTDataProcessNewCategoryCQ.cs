
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDataProcessNewCategoryCQ : AbstractBsTDataProcessNewCategoryCQ {

        protected TDataProcessNewCategoryCIQ _inlineQuery;

        public BsTDataProcessNewCategoryCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDataProcessNewCategoryCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDataProcessNewCategoryCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDataProcessNewCategoryCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDataProcessNewCategoryCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _dataProcessNewCategoryId;
        public ConditionValue DataProcessNewCategoryId {
            get { if (_dataProcessNewCategoryId == null) { _dataProcessNewCategoryId = new ConditionValue(); } return _dataProcessNewCategoryId; }
        }
        protected override ConditionValue getCValueDataProcessNewCategoryId() { return this.DataProcessNewCategoryId; }


        public BsTDataProcessNewCategoryCQ AddOrderBy_DataProcessNewCategoryId_Asc() { regOBA("DATA_PROCESS_NEW_CATEGORY_ID");return this; }
        public BsTDataProcessNewCategoryCQ AddOrderBy_DataProcessNewCategoryId_Desc() { regOBD("DATA_PROCESS_NEW_CATEGORY_ID");return this; }

        protected ConditionValue _newCategoryNo;
        public ConditionValue NewCategoryNo {
            get { if (_newCategoryNo == null) { _newCategoryNo = new ConditionValue(); } return _newCategoryNo; }
        }
        protected override ConditionValue getCValueNewCategoryNo() { return this.NewCategoryNo; }


        public BsTDataProcessNewCategoryCQ AddOrderBy_NewCategoryNo_Asc() { regOBA("NEW_CATEGORY_NO");return this; }
        public BsTDataProcessNewCategoryCQ AddOrderBy_NewCategoryNo_Desc() { regOBD("NEW_CATEGORY_NO");return this; }

        protected ConditionValue _newCategoryName;
        public ConditionValue NewCategoryName {
            get { if (_newCategoryName == null) { _newCategoryName = new ConditionValue(); } return _newCategoryName; }
        }
        protected override ConditionValue getCValueNewCategoryName() { return this.NewCategoryName; }


        public BsTDataProcessNewCategoryCQ AddOrderBy_NewCategoryName_Asc() { regOBA("NEW_CATEGORY_NAME");return this; }
        public BsTDataProcessNewCategoryCQ AddOrderBy_NewCategoryName_Desc() { regOBD("NEW_CATEGORY_NAME");return this; }

        protected ConditionValue _srcItemId;
        public ConditionValue SrcItemId {
            get { if (_srcItemId == null) { _srcItemId = new ConditionValue(); } return _srcItemId; }
        }
        protected override ConditionValue getCValueSrcItemId() { return this.SrcItemId; }


        public BsTDataProcessNewCategoryCQ AddOrderBy_SrcItemId_Asc() { regOBA("SRC_ITEM_ID");return this; }
        public BsTDataProcessNewCategoryCQ AddOrderBy_SrcItemId_Desc() { regOBD("SRC_ITEM_ID");return this; }

        protected ConditionValue _operationCode;
        public ConditionValue OperationCode {
            get { if (_operationCode == null) { _operationCode = new ConditionValue(); } return _operationCode; }
        }
        protected override ConditionValue getCValueOperationCode() { return this.OperationCode; }


        public BsTDataProcessNewCategoryCQ AddOrderBy_OperationCode_Asc() { regOBA("OPERATION_CODE");return this; }
        public BsTDataProcessNewCategoryCQ AddOrderBy_OperationCode_Desc() { regOBD("OPERATION_CODE");return this; }

        protected ConditionValue _conditionString;
        public ConditionValue ConditionString {
            get { if (_conditionString == null) { _conditionString = new ConditionValue(); } return _conditionString; }
        }
        protected override ConditionValue getCValueConditionString() { return this.ConditionString; }


        public BsTDataProcessNewCategoryCQ AddOrderBy_ConditionString_Asc() { regOBA("CONDITION_STRING");return this; }
        public BsTDataProcessNewCategoryCQ AddOrderBy_ConditionString_Desc() { regOBD("CONDITION_STRING");return this; }

        protected ConditionValue _bottomValue;
        public ConditionValue BottomValue {
            get { if (_bottomValue == null) { _bottomValue = new ConditionValue(); } return _bottomValue; }
        }
        protected override ConditionValue getCValueBottomValue() { return this.BottomValue; }


        public BsTDataProcessNewCategoryCQ AddOrderBy_BottomValue_Asc() { regOBA("BOTTOM_VALUE");return this; }
        public BsTDataProcessNewCategoryCQ AddOrderBy_BottomValue_Desc() { regOBD("BOTTOM_VALUE");return this; }

        protected ConditionValue _upperValue;
        public ConditionValue UpperValue {
            get { if (_upperValue == null) { _upperValue = new ConditionValue(); } return _upperValue; }
        }
        protected override ConditionValue getCValueUpperValue() { return this.UpperValue; }


        public BsTDataProcessNewCategoryCQ AddOrderBy_UpperValue_Asc() { regOBA("UPPER_VALUE");return this; }
        public BsTDataProcessNewCategoryCQ AddOrderBy_UpperValue_Desc() { regOBD("UPPER_VALUE");return this; }

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

        public BsTDataProcessNewCategoryCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTDataProcessNewCategoryCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        public BsTDataProcessNewCategoryCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDataProcessNewCategoryCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDataProcessNewCategoryCQ baseQuery = (TDataProcessNewCategoryCQ)baseQueryAsSuper;
            TDataProcessNewCategoryCQ unionQuery = (TDataProcessNewCategoryCQ)unionQueryAsSuper;
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
            return resolveNextRelationPath("T_DATA_PROCESS_NEW_CATEGORY", "tDataProcessNewItem");
        }
        public bool hasConditionQueryTDataProcessNewItem() {
            return _conditionQueryTDataProcessNewItem != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDataProcessNewCategoryCQ> _scalarSubQueryMap;
	    public Map<String, TDataProcessNewCategoryCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDataProcessNewCategoryCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDataProcessNewCategoryCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDataProcessNewCategoryCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDataProcessNewCategoryCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDataProcessNewCategoryCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDataProcessNewCategoryCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
